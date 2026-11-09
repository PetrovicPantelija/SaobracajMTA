using Saobracaj.Theme;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Servis
{
    // Deo: osnovni podaci, primenljivost, kartica dela (kretanje sa stanjem) i dobavljač
    public class frmSERDeo : Form
    {
        private int id;
        private readonly SERNaslovnaTraka naslov;
        private readonly FlowLayoutPanel bedzevi;
        private readonly SERKartice kartice;
        private readonly SERObrazac osnovni, dobavljac;
        private readonly GridGroupingControl gridPrimena, gridKartica, gridNabavke;
        private readonly Label statusna;

        public frmSERDeo(int id)
        {
            this.id = id;
            SERStil.PripremiFormu(this, "Deo");

            osnovni = new SERObrazac(new List<SERPolje>
            {
                SERPolje.Tekst("Sifra", "Šifra", 20, true),
                new SERPolje("Naziv", "Naziv", SERTip.Tekst, true, 150) { Raspon = 2 },
                SERPolje.Lista("GrupaID", "Grupa", SERListe.Grupe),
                SERPolje.Tekst("JM", "Jedinica mere", 10, true),
                SERPolje.Tekst("Lokacija", "Lokacija (polica)", 30),
                SERPolje.Decimalni("MinZaliha", "Minimalna zaliha", true),
                SERPolje.Decimalni("MaxZaliha", "Maksimalna zaliha"),
                SERPolje.DaNe("Kritican", "Kritičan deo (sredstvo ne sme da stoji)"),
                SERPolje.DaNe("Aktivno", "Aktivan", true),
                new SERPolje("Napomena", "Napomena", SERTip.VelikiTekst, false, 500) { Raspon = 3 }
            }, 3);
            dobavljac = new SERObrazac(new List<SERPolje>
            {
                new SERPolje("DobavljacID", "Dobavljač", SERTip.Lista) { ListaUpit = SERListe.Partneri, Raspon = 2 },
                SERPolje.Broj("RokIsporukeDana", "Rok isporuke (dana)", false, 0, 365)
            }, 3);

            gridPrimena = SERGrid.Novi();
            SERGrid.Kolone(gridPrimena,
                new SERKolona("VrstaSredstva", "Vrsta sredstva", 240),
                new SERKolona("Sredstvo", "Sredstvo (ako važi samo za jedno)", 300));
            gridKartica = SERGrid.Novi();
            SERGrid.Kolone(gridKartica,
                new SERKolona("Datum", "Datum", 95, "datum"),
                new SERKolona("TipNaziv", "Vrsta", 110),
                new SERKolona("DokumentBroj", "Dokument", 110),
                new SERKolona("Ulaz", "Ulaz", 85, "kolicina"),
                new SERKolona("Izlaz", "Izlaz", 85, "kolicina"),
                new SERKolona("StanjePosle", "Stanje", 85, "kolicina"),
                new SERKolona("Cena", "Cena RSD", 100, "iznos"),
                new SERKolona("Vrednost", "Vrednost RSD", 110, "iznos"),
                new SERKolona("RadniNalog", "Radni nalog", 115),
                new SERKolona("Narudzbenica", "Narudžbenica", 120),
                new SERKolona("KreiraoKorisnik", "Korisnik", 100),
                new SERKolona("Napomena", "Napomena", 200));
            gridNabavke = SERGrid.Novi();
            SERGrid.Kolone(gridNabavke,
                new SERKolona("Broj", "Narudžbenica", 130),
                new SERKolona("Datum", "Datum", 95, "datum"),
                new SERKolona("Dobavljac", "Dobavljač", 220),
                new SERKolona("Kolicina", "Količina", 85, "kolicina"),
                new SERKolona("Cena", "Cena RSD", 100, "iznos"),
                new SERKolona("Primljeno", "Primljeno", 85, "kolicina"),
                new SERKolona("StatusTekst", "Status", 150));

            kartice = new SERKartice();
            kartice.Dodaj("Osnovni podaci", Skrol(osnovni.Panel));
            kartice.Dodaj("Primenljivost", SaAlatima(gridPrimena,
                SERStil.Dugme("Dodaj", null), (s, e) => DodajPrimenu(),
                SERStil.Dugme("Ukloni", "danger"), (s, e) => UkloniPrimenu()));
            kartice.Dodaj("Kartica dela", Okvir(gridKartica));
            var dobPanel = new Panel { BackColor = SavaTheme.Surface };
            var nabavke = SERStil.Sekcija("Nabavke ovog dela", Okvir(gridNabavke));
            nabavke.Dock = DockStyle.Fill;
            dobPanel.Controls.Add(nabavke);
            dobPanel.Controls.Add(dobavljac.Panel);
            kartice.Dodaj("Dobavljač", dobPanel);
            kartice.Promenjena += k =>
            {
                if (this.id == 0 && k != "Osnovni podaci" && k != "Dobavljač")
                {
                    MessageBox.Show(this, "Prvo sačuvajte deo.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BeginInvoke((Action)(() => kartice.Izaberi(0)));
                }
            };

            bedzevi = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 48, Padding = new Padding(20, 10, 20, 0), BackColor = SavaTheme.White, WrapContents = false };

            naslov = SERStil.NaslovnaTraka(this, "Zalihe › Novi deo");
            naslov.DodajDugme("Sačuvaj", true, (s, e) => Sacuvaj());
            statusna = SERStil.StatusnaLinija(this);

            Controls.Add(kartice);
            Controls.Add(bedzevi);
            kartice.BringToFront();
            naslov.SendToBack();
            statusna.Parent.SendToBack();
            SERStil.PrimeniTemu(this);
            VisibleChanged += (s, e) => { if (Visible) Ucitaj(); };
        }

        private static Control Skrol(Control sadrzaj)
        {
            var p = new Panel { AutoScroll = true, BackColor = SavaTheme.Surface };
            sadrzaj.Dock = DockStyle.Top;
            p.Controls.Add(sadrzaj);
            return p;
        }

        private static Control Okvir(Control grid)
        {
            var okvir = SERStil.BeliPanel();
            grid.Dock = DockStyle.Fill;
            okvir.Controls.Add(grid);
            return okvir;
        }

        private static Control SaAlatima(Control grid, Button b1, EventHandler k1, Button b2, EventHandler k2)
        {
            var p = new Panel { BackColor = SavaTheme.Surface };
            var traka = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 44, BackColor = SavaTheme.Surface };
            b1.Click += k1;
            b2.Click += k2;
            traka.Controls.Add(b1);
            traka.Controls.Add(b2);
            var okvir = Okvir(grid);
            okvir.Dock = DockStyle.Fill;
            p.Controls.Add(okvir);
            p.Controls.Add(traka);
            return p;
        }

        private void Ucitaj()
        {
            bedzevi.Controls.Clear();
            if (id == 0)
            {
                osnovni.PostaviSve(new SERVrednosti { ["JM"] = "kom", ["MinZaliha"] = 0m, ["Aktivno"] = true });
                dobavljac.PostaviSve(null);
                statusna.Text = "Novi deo · stanje se unosi prijemom ili početnim stanjem";
                return;
            }
            DataRow r;
            try
            {
                r = SERBaza.Red(@"
                    SELECT d.*, s.Stanje, s.Rezervisano, s.Raspolozivo, s.Naruceno, s.Vrednost, s.StatusTekst
                    FROM dbo.SERDeo d JOIN dbo.vSERDeoStanje s ON s.ID = d.ID WHERE d.ID = @ID", SERBaza.P("@ID", id));
            }
            catch (Exception ex)
            {
                SERDijalog.Greska(this, Text, ex);
                return;
            }
            if (r == null)
                return;
            var v = new SERVrednosti(SERBaza.RedUMapu(r));
            osnovni.PostaviSve(v);
            dobavljac.PostaviSve(v);
            naslov.Naslov.Text = "Zalihe › " + v.Tekst("Sifra") + " " + v.Tekst("Naziv");
            Text = v.Tekst("Sifra");

            string jm = " " + v.Tekst("JM");
            Bedz("Stanje " + SERBaza.Kolicina(SERBaza.Decimalni(r["Stanje"])) + jm, SavaTheme.White, SavaTheme.Text);
            Bedz("Rezervisano " + SERBaza.Kolicina(SERBaza.Decimalni(r["Rezervisano"])) + jm, SavaTheme.White, SavaTheme.Text);
            Bedz("Raspoloživo " + SERBaza.Kolicina(SERBaza.Decimalni(r["Raspolozivo"])) + jm, SavaTheme.White, SavaTheme.Text);
            Bedz("Naručeno " + SERBaza.Kolicina(SERBaza.Decimalni(r["Naruceno"])) + jm, SavaTheme.White, SavaTheme.Text);
            Bedz("Prosečna cena " + SERBaza.Iznos(SERBaza.Decimalni(r["ProsecnaCena"])) + " RSD", SavaTheme.White, SavaTheme.Text);
            Bedz("Vrednost " + SERBaza.Iznos(SERBaza.Decimalni(r["Vrednost"])) + " RSD", SavaTheme.White, SavaTheme.Text);
            string status = Convert.ToString(r["StatusTekst"]);
            Bedz(status, status == "Na stanju" ? SERStil.PozadinaVazi : status == "Ispod minimuma" ? SERStil.PozadinaIstice : SERStil.PozadinaIsteklo,
                 status == "Na stanju" ? SERStil.StatusVazi : status == "Ispod minimuma" ? SERStil.StatusIstice : SERStil.StatusIsteklo);
            if (Convert.ToBoolean(r["Kritican"]))
                Bedz("Kritičan", SERStil.PozadinaIsteklo, SavaTheme.Danger);

            statusna.Text = "Kreirao " + v.Tekst("KreiraoKorisnik") + " " + SERBaza.DatumTekst(r["DatumKreiranja"])
                + (r["DatumIzmene"] == DBNull.Value ? "" : " · izmenio " + v.Tekst("IzmenioKorisnik") + " " + SERBaza.DatumTekst(r["DatumIzmene"]));

            try
            {
                SERGrid.Napuni(gridPrimena, SERBaza.Tabela(@"
                    SELECT p.ID, v.Naziv AS VrstaSredstva, ISNULL(s.Oznaka + N' – ' + s.Naziv, N'sva sredstva ove vrste') AS Sredstvo
                    FROM dbo.SERDeoPrimenljivost p
                    JOIN dbo.SERVrstaSredstva v ON v.ID = p.VrstaSredstvaID
                    LEFT JOIN dbo.SERSredstvo s ON s.ID = p.SredstvoID
                    WHERE p.DeoID = @ID ORDER BY v.Naziv, s.Oznaka", SERBaza.P("@ID", id)));
                SERGrid.Napuni(gridKartica, SERBaza.Tabela(@"
                    SELECT ID, Datum, TipNaziv, DokumentBroj,
                           CASE WHEN Kolicina > 0 THEN Kolicina END AS Ulaz, CASE WHEN Kolicina < 0 THEN -Kolicina END AS Izlaz,
                           StanjePosle, Cena, Vrednost, RadniNalog, Narudzbenica, KreiraoKorisnik, Napomena
                    FROM dbo.vSERKarticaDela WHERE DeoID = @ID ORDER BY Datum DESC, ID DESC", SERBaza.P("@ID", id)));
                SERGrid.Napuni(gridNabavke, SERBaza.Tabela(@"
                    SELECT s.ID, n.Broj, n.Datum, n.Dobavljac, s.Kolicina, s.Cena, s.Primljeno, n.StatusTekst
                    FROM dbo.vSERNarudzbenicaStavka s JOIN dbo.vSERNarudzbenica n ON n.ID = s.NarudzbenicaID
                    WHERE s.DeoID = @ID ORDER BY n.Datum DESC", SERBaza.P("@ID", id)));
            }
            catch (Exception ex)
            {
                SERDijalog.Greska(this, Text, ex);
            }
        }

        private void Bedz(string tekst, Color pozadina, Color boja)
        {
            bedzevi.Controls.Add(new SERBedz(tekst, pozadina, boja));
        }

        private void Sacuvaj()
        {
            if (!SERPrava.Proveri(this, SERPrava.Magacioner))
                return;
            SERVrednosti d;
            string greska = osnovni.Ocitaj(out SERVrednosti v);
            if (greska == null)
                greska = dobavljac.Ocitaj(out d);
            else
                d = null;
            if (greska == null && v.Uzmi("MaxZaliha") != null && SERBaza.Decimalni(v.Uzmi("MaxZaliha")) < SERBaza.Decimalni(v.Uzmi("MinZaliha")))
            {
                osnovni.Kontrola("MaxZaliha").Focus();
                greska = "Maksimalna zaliha mora biti veća ili jednaka minimalnoj.";
            }
            if (greska != null)
            {
                kartice.Izaberi("Osnovni podaci");
                MessageBox.Show(this, greska, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var pId = SERBaza.Izlaz("@ID", SqlDbType.Int);
            pId.Direction = ParameterDirection.InputOutput;
            pId.Value = id;
            bool ok = SERDijalog.Pokusaj(this, Text, () => SERBaza.Procedura("dbo.SER_DeoSnimi", pId,
                SERBaza.P("@Sifra", v.Tekst("Sifra")), SERBaza.P("@Naziv", v.Tekst("Naziv")), SERBaza.P("@GrupaID", v.Uzmi("GrupaID")),
                SERBaza.P("@JM", v.Tekst("JM")), SERBaza.P("@MinZaliha", v.Uzmi("MinZaliha")), SERBaza.P("@MaxZaliha", v.Uzmi("MaxZaliha")),
                SERBaza.P("@Lokacija", SERBaza.Tekst(v.Tekst("Lokacija"))), SERBaza.P("@Kritican", v.Uzmi("Kritican")),
                SERBaza.P("@DobavljacID", d.Uzmi("DobavljacID")), SERBaza.P("@RokIsporukeDana", d.Uzmi("RokIsporukeDana")),
                SERBaza.P("@Napomena", SERBaza.Tekst(v.Tekst("Napomena"))), SERBaza.P("@Aktivno", v.Uzmi("Aktivno")),
                SERBaza.P("@Korisnik", SERBaza.Korisnik)));
            if (!ok)
                return;
            id = SERBaza.CeoBroj(pId.Value);
            Ucitaj();
            statusna.Text = "✔ Sačuvano u " + DateTime.Now.ToString("HH:mm:ss") + " · " + statusna.Text;
        }

        private void DodajPrimenu()
        {
            if (!SERPrava.Proveri(this, SERPrava.Magacioner))
                return;
            var polja = new List<SERPolje>
            {
                SERPolje.Lista("VrstaSredstvaID", "Vrsta sredstva", SERListe.VrsteSredstava, true),
                SERPolje.Lista("SredstvoID", "Samo za sredstvo (prazno = sva sredstva ove vrste)", SERListe.Sredstva)
            };
            if (SERDijalog.Unos(this, "Primenljivost dela", polja, null, v =>
                {
                    if (v.Uzmi("SredstvoID") != null && SERBaza.CeoBroj(SERBaza.Skalar(
                            "SELECT VrstaSredstvaID FROM dbo.SERSredstvo WHERE ID = @S", SERBaza.P("@S", v.Uzmi("SredstvoID")))) != v.Broj("VrstaSredstvaID"))
                        throw new InvalidOperationException("Izabrano sredstvo nije te vrste.");
                    SERBaza.Izvrsi(@"INSERT dbo.SERDeoPrimenljivost (DeoID, VrstaSredstvaID, SredstvoID, KreiraoKorisnik)
                                     VALUES (@D, @V, @S, @K)",
                        SERBaza.P("@D", id), SERBaza.P("@V", v.Uzmi("VrstaSredstvaID")), SERBaza.P("@S", v.Uzmi("SredstvoID")), SERBaza.P("@K", SERBaza.Korisnik));
                }, 1))
                Ucitaj();
        }

        private void UkloniPrimenu()
        {
            int p = SERGrid.IzabraniId(gridPrimena);
            if (p <= 0 || !SERPrava.Proveri(this, SERPrava.Magacioner))
                return;
            if (SERDijalog.Potvrdi(this, "Ukloniti izabranu primenljivost?", Text)
                && SERDijalog.Pokusaj(this, Text, () => SERBaza.Izvrsi("DELETE dbo.SERDeoPrimenljivost WHERE ID = @ID", SERBaza.P("@ID", p))))
                Ucitaj();
        }
    }
}
