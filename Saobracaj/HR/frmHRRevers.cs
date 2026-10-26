using Saobracaj.Theme;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.HR
{
    // HTZ revers (7.4): jedan revers ima više stavki, broj se dodeljuje automatski po godini.
    // Posle štampe revers je zaključan (stavke se ne menjaju), ponovna štampa nosi oznaku KOPIJA,
    // a greška se ispravlja storniranjem i novim reversom.
    public class frmHRRevers : Form
    {
        private readonly int zaposleni;
        private int revers;
        private bool zakljucen, storniran;
        private readonly HRNaslovnaTraka naslov;
        private readonly HRObrazac zaglavlje;
        private readonly HRTabelaKartica stavke;
        private readonly Button sacuvaj, stampaj, storniraj;
        private readonly Label statusna;
        private readonly string konfekcija, obuca;

        public frmHRRevers(int zaposleni, int revers)
        {
            this.zaposleni = zaposleni;
            this.revers = revers;
            Text = "HTZ revers";
            Size = new Size(1000, 640);
            StartPosition = FormStartPosition.CenterParent;
            BackColor = SavaTheme.Surface;
            ShowInTaskbar = false;
            AutoScaleMode = AutoScaleMode.None;

            DataRow z = HRBaza.Red("SELECT KonfekcijskiBroj, BrojObuce FROM dbo.Zaposleni WHERE ID = @Z", HRBaza.P("@Z", zaposleni));
            konfekcija = z == null ? null : z["KonfekcijskiBroj"] as string;
            obuca = z == null ? null : z["BrojObuce"] as string;

            zaglavlje = new HRObrazac(new List<HRPolje>
            {
                HRPolje.Datum("Datum", "Datum reversa", true),
                HRPolje.Lista("Izdao", "Izdao", HRListe.AktivniZaposleni),
                new HRPolje("Napomena", "Napomena", HRTip.Tekst, false, 500)
            }, 3);

            stavke = new HRTabelaKartica(DefStavke());
            var telo = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 12, 20, 12) };
            telo.Controls.Add(stavke);
            telo.Controls.Add(zaglavlje.Panel);

            var alati = HRStil.AlatnaTraka(this);
            sacuvaj = alati.Dugme("Sačuvaj", (s, e) => SacuvajZaglavlje(), "primary");
            stampaj = alati.Dugme("Štampaj", (s, e) => Stampaj());
            storniraj = alati.Dugme("Storniraj", (s, e) => Storniraj(), "danger");

            naslov = HRStil.NaslovnaTraka(this, "HTZ › Novi revers");
            statusna = HRStil.StatusnaLinija(this);
            Controls.Add(telo);
            telo.BringToFront();
            HRStil.PrimeniTemu(this);

            Load += (s, e) => Ucitaj();
        }

        private HRTabelaDef DefStavke()
        {
            return new HRTabelaDef
            {
                Naslov = "Stavka reversa",
                Tabela = "ZaposleniHTZZaduzenja",
                RoditeljKolona = "Revers",
                PorukaBezRoditelja = "Prvo sačuvajte revers (datum i izdavaoca).",
                Dozvoljeno = () => !zakljucen && !storniran,
                PorukaZabranjeno = "Revers je odštampan ili storniran; stavke se ne menjaju. Grešku ispravite storniranjem i novim reversom.",
                Upit = @"SELECT s.Sifra, o.Naziv AS Oprema, o.JedinicaMere, s.Velicina, s.Kolicina, s.DatumZaduzenja, s.RokZamene, s.Napomena
                         FROM dbo.ZaposleniHTZZaduzenja s JOIN dbo.HTZOprema o ON o.Sifra = s.HTZOprema
                         WHERE s.Revers = @Roditelj ORDER BY s.Sifra",
                Kolone = new[]
                {
                    new HRKolona("Oprema", "Oprema", 260), new HRKolona("JedinicaMere", "JM", 60), new HRKolona("Velicina", "Vel.", 70),
                    new HRKolona("Kolicina", "Kol.", 70, "kolicina"), new HRKolona("DatumZaduzenja", "Zaduženo", 100, "datum"),
                    new HRKolona("RokZamene", "Rok zamene", 110, "datum"), new HRKolona("Napomena", "Napomena", 200)
                },
                Polja = new List<HRPolje>
                {
                    HRPolje.Lista("HTZOprema", "Oprema", "SELECT Sifra, Naziv FROM dbo.HTZOprema WHERE Aktivan = 1 ORDER BY Naziv", true),
                    HRPolje.Tekst("Velicina", "Veličina", 10),
                    new HRPolje("Kolicina", "Količina", HRTip.Decimalni, true) { Min = 0.01m, Max = 9999, Podrazumevano = 1m },
                    HRPolje.Datum("DatumZaduzenja", "Datum zaduženja", true),
                    HRPolje.Datum("RokZamene", "Rok zamene"),
                    HRPolje.Napomena()
                },
                NoviPodrazumevano = () => new HRVrednosti { ["Kolicina"] = 1m, ["DatumZaduzenja"] = zaglavlje.Vrednost("Datum") ?? DateTime.Today },
                PriPromeni = PriPromeniStavke,
                Validacija = v =>
                {
                    DataRow o = Oprema(v.Uzmi("HTZOprema"));
                    if (o != null && Convert.ToBoolean(o["ImaVelicinu"]) && v.Tekst("Velicina") == "")
                        return "Za ovu opremu unesite veličinu.";
                    return null;
                },
                Snimi = (v, sifra) =>
                {
                    HRBaza.Procedura("dbo.HR_HTZStavkaSnimi",
                        HRBaza.P("@Sifra", sifra), HRBaza.P("@Revers", revers), HRBaza.P("@HTZOprema", v.Uzmi("HTZOprema")),
                        HRBaza.P("@Kolicina", v.Uzmi("Kolicina")), HRBaza.P("@Velicina", v.Uzmi("Velicina")),
                        HRBaza.P("@DatumZaduzenja", v.Uzmi("DatumZaduzenja")), HRBaza.P("@RokZamene", v.Uzmi("RokZamene")),
                        HRBaza.P("@Napomena", v.Uzmi("Napomena")), HRBaza.Izlaz("@NovaSifra", SqlDbType.Int));
                    return null;
                },
                Obrisi = sifra =>
                {
                    HRBaza.Procedura("dbo.HR_HTZStavkaObrisi", HRBaza.P("@Sifra", sifra));
                    return null;
                }
            };
        }

        private static DataRow Oprema(object sifra)
        {
            if (sifra == null || sifra == DBNull.Value)
                return null;
            return HRBaza.Red(@"SELECT o.ImaVelicinu, o.RokTrajanjaMeseci, g.Naziv AS Grupa
                                FROM dbo.HTZOprema o LEFT JOIN dbo.HTZGrupe g ON g.Sifra = o.GrupaHTZ WHERE o.Sifra = @S", HRBaza.P("@S", sifra));
        }

        // Veličina se predlaže iz podataka zaposlenog (obuća ili konfekcija), rok zamene iz roka trajanja opreme
        private void PriPromeniStavke(frmHRStavka dlg, string kolona)
        {
            if (kolona != "HTZOprema" && kolona != "DatumZaduzenja")
                return;
            DataRow o = Oprema(dlg.Vrednost("HTZOprema"));
            if (o == null)
                return;
            bool velicina = Convert.ToBoolean(o["ImaVelicinu"]);
            dlg.PostaviOznaku("Velicina", velicina ? "Veličina *" : "Veličina");
            if (kolona == "HTZOprema" && velicina && dlg.Vrednost("Velicina") == null)
            {
                string grupa = Convert.ToString(o["Grupa"]);
                string predlog = grupa.StartsWith("Obuć", StringComparison.OrdinalIgnoreCase) ? obuca : konfekcija;
                if (!string.IsNullOrEmpty(predlog))
                    dlg.Postavi("Velicina", predlog);
            }
            int meseci = HRBaza.CeoBroj(o["RokTrajanjaMeseci"]);
            DateTime? datum = HRBaza.Datum(dlg.Vrednost("DatumZaduzenja"));
            object trenutni = dlg.Vrednost("RokZamene");
            if (meseci > 0 && datum.HasValue && (trenutni == null || Equals(trenutni, dlg.Tag)))
            {
                DateTime rok = datum.Value.AddMonths(meseci);
                dlg.Postavi("RokZamene", rok);
                dlg.Tag = rok;
            }
        }

        private void Ucitaj()
        {
            DataRow ime = HRBaza.Red("SELECT PrezimeIme, Code FROM dbo.vZaposleniTrenutno WHERE Zaposleni = @Z", HRBaza.P("@Z", zaposleni));
            string zaposleniNaziv = ime == null ? "" : ime["PrezimeIme"] + " (" + ime["Code"] + ")";
            if (revers == 0)
            {
                int ja = HRPrava.TrenutniZaposleni();
                zaglavlje.PostaviSve(new HRVrednosti { ["Datum"] = DateTime.Today, ["Izdao"] = ja > 0 ? (object)ja : null });
                naslov.Naslov.Text = "HTZ › Novi revers › " + zaposleniNaziv;
                zakljucen = storniran = false;
            }
            else
            {
                DataRow r = HRBaza.Red("SELECT * FROM dbo.HTZReversi WHERE Sifra = @R", HRBaza.P("@R", revers));
                zaglavlje.PostaviSve(new HRVrednosti { ["Datum"] = r["Datum"], ["Izdao"] = r["Izdao"], ["Napomena"] = r["Napomena"] });
                zakljucen = Convert.ToBoolean(r["Zakljucen"]);
                storniran = Convert.ToBoolean(r["Storniran"]);
                naslov.Naslov.Text = "HTZ › Revers " + r["BrojReversa"] + " › " + zaposleniNaziv;
            }
            stavke.Ucitaj(revers);
            PrikaziStanje();
        }

        private void PrikaziStanje()
        {
            bool menja = !zakljucen && !storniran;
            stavke.Komande.Visible = menja && revers > 0;
            foreach (HRPolje p in zaglavlje.Polja)
                zaglavlje.Omoguci(p.Kolona, menja);
            sacuvaj.Enabled = menja;
            stampaj.Enabled = revers > 0 && !storniran;
            storniraj.Enabled = revers > 0 && !storniran;
            statusna.Text = revers == 0 ? "Sačuvajte revers, pa dodajte stavke. Broj reversa se dodeljuje automatski po godini."
                : storniran ? "Revers je storniran."
                : zakljucen ? "Revers je odštampan i zaključan; stavke se ne menjaju. Ponovna štampa nosi oznaku KOPIJA. Grešku ispravite storniranjem i novim reversom."
                : "Revers je otvoren. Prva štampa ga zaključava.";
        }

        private void SacuvajZaglavlje()
        {
            string greska = zaglavlje.Ocitaj(out HRVrednosti v);
            if (greska != null)
            {
                MessageBox.Show(greska, "Revers", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                if (revers == 0)
                {
                    var sifra = HRBaza.Izlaz("@Sifra", SqlDbType.Int);
                    var broj = HRBaza.Izlaz("@BrojReversa", SqlDbType.NVarChar, 20);
                    HRBaza.Procedura("dbo.HR_HTZReversNovi", HRBaza.P("@Zaposleni", zaposleni), HRBaza.P("@Datum", v.Uzmi("Datum")),
                        HRBaza.P("@Izdao", v.Uzmi("Izdao")), HRBaza.P("@Napomena", v.Uzmi("Napomena")), HRBaza.P("@Korisnik", HRBaza.Korisnik), sifra, broj);
                    revers = HRBaza.CeoBroj(sifra.Value);
                }
                else
                    HRBaza.Izvrsi("UPDATE dbo.HTZReversi SET Datum = @D, Izdao = @I, Napomena = @N, IzmenioKorisnik = @K, DatumIzmene = sysdatetime() WHERE Sifra = @R AND Zakljucen = 0",
                        HRBaza.P("@D", v.Uzmi("Datum")), HRBaza.P("@I", v.Uzmi("Izdao")), HRBaza.P("@N", v.Uzmi("Napomena")), HRBaza.P("@K", HRBaza.Korisnik), HRBaza.P("@R", revers));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Revers", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Ucitaj();
        }

        private void Stampaj()
        {
            if (Stampa(this, revers))
                Ucitaj();
        }

        // Štampa reversa; prva štampa zaključava, sledeće su KOPIJA
        public static bool Stampa(IWin32Window vlasnik, int revers)
        {
            try
            {
                var kopija = HRBaza.Izlaz("@Kopija", SqlDbType.Bit);
                HRBaza.Procedura("dbo.HR_HTZReversStampa", HRBaza.P("@Revers", revers), HRBaza.P("@Korisnik", HRBaza.Korisnik), kopija);
                HRStampa.Revers(vlasnik, revers, Convert.ToBoolean(kopija.Value));
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(vlasnik, ex.Message, "Štampa reversa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private void Storniraj()
        {
            if (MessageBox.Show("Stornirati revers? Oprema sa ovog reversa više se ne vodi kao zadužena.", "Revers",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            HRBaza.Procedura("dbo.HR_HTZReversStorniraj", HRBaza.P("@Revers", revers), HRBaza.P("@Korisnik", HRBaza.Korisnik));
            Ucitaj();
        }

        // Lista reversa zaposlenog: otvaranje, štampa, storniranje
        public static void Lista(IWin32Window vlasnik, int zaposleni)
        {
            var def = new HRTabelaDef
            {
                Naslov = "Revers",
                Tabela = "HTZReversi",
                RoditeljKolona = "Zaposleni",
                SamoCitanje = true,
                Upit = @"SELECT r.Sifra, r.BrojReversa, r.Datum, LTRIM(RTRIM(ISNULL(i.Prezime, N'') + N' ' + ISNULL(i.Ime, N''))) AS Izdao,
                                (SELECT COUNT(*) FROM dbo.ZaposleniHTZZaduzenja s WHERE s.Revers = r.Sifra) AS Stavki,
                                CASE WHEN r.Storniran = 1 THEN N'Storniran' WHEN r.Zakljucen = 1 THEN N'Zaključen' ELSE N'Otvoren' END AS Stanje
                         FROM dbo.HTZReversi r LEFT JOIN dbo.Zaposleni i ON i.ID = r.Izdao
                         WHERE r.Zaposleni = @Roditelj ORDER BY r.Datum DESC, r.Sifra DESC",
                Kolone = new[]
                {
                    new HRKolona("BrojReversa", "Broj", 110), new HRKolona("Datum", "Datum", 100, "datum"), new HRKolona("Izdao", "Izdao", 200),
                    new HRKolona("Stavki", "Stavki", 70, "broj"), new HRKolona("Stanje", "Stanje", 120)
                },
                BojaTeksta = (r, k) => k == "Stanje" && Convert.ToString(r.GetValue("Stanje")) == "Storniran" ? SavaTheme.Danger : (Color?)null
            };
            var t = new HRTabelaKartica(def);
            using (var f = new Form { Text = "HTZ reversi", Size = new Size(760, 480), StartPosition = FormStartPosition.CenterParent, BackColor = SavaTheme.Surface, ShowInTaskbar = false })
            {
                Action otvori = () =>
                {
                    if (t.IzabraniId() <= 0) return;
                    using (var r = new frmHRRevers(zaposleni, t.IzabraniId()))
                        r.ShowDialog(f);
                    t.Osvezi();
                };
                t.DodajKomandu("Štampaj", (s, e) => { if (t.IzabraniId() > 0 && Stampa(f, t.IzabraniId())) t.Osvezi(); });
                t.DodajKomandu("Otvori", (s, e) => otvori());
                HRGrid.DuplikKlik(t.Grid, otvori);
                var telo = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 12, 20, 12) };
                telo.Controls.Add(t);
                f.Controls.Add(telo);
                HRStil.NaslovnaTraka(f, "HTZ › Reversi");
                HRStil.PrimeniTemu(f);
                t.Ucitaj(zaposleni);
                f.ShowDialog(vlasnik);
            }
        }
    }
}
