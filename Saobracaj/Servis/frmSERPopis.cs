using Saobracaj.Theme;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Servis
{
    // Popis zaliha (MD-R10): razlika se vidi čim se unese prebrojano; zaliha se menja tek kad šef tehničke službe odobri
    public class frmSERPopis : Form
    {
        private readonly SERTekst info;
        private readonly SERChip chipRazlika;
        private readonly GridGroupingControl gridPopisi, gridStavke;
        private bool samoRazlika;

        public frmSERPopis()
        {
            SERStil.PripremiFormu(this, "Popis zaliha");

            gridPopisi = SERGrid.Novi();
            SERGrid.Kolone(gridPopisi,
                new SERKolona("Broj", "Broj", 120),
                new SERKolona("Datum", "Datum", 95, "datum"),
                new SERKolona("Grupa", "Grupa delova", 160),
                new SERKolona("StatusTekst", "Status", 140),
                new SERKolona("BrojStavki", "Stavki", 70, "broj"),
                new SERKolona("Prebrojano", "Prebrojano", 90, "broj"),
                new SERKolona("SaRazlikom", "Sa razlikom", 95, "broj"),
                new SERKolona("VrednostManjka", "Manjak RSD", 115, "iznos"),
                new SERKolona("VrednostViska", "Višak RSD", 115, "iznos"),
                new SERKolona("OdobrioKorisnik", "Odobrio", 100));
            SERGrid.BojaTeksta(gridPopisi, (r, k) => k == "StatusTekst" ? SERGrid.BojaStatusa("Popis", SERBaza.CeoBroj(r.GetValue("Status"))) : null);
            gridPopisi.SelectedRecordsChanged += (s, e) => UcitajStavke();

            gridStavke = SERGrid.Novi();
            SERGrid.Kolone(gridStavke,
                new SERKolona("Sifra", "Šifra", 100),
                new SERKolona("Naziv", "Deo", 260),
                new SERKolona("Lokacija", "Lokacija", 90),
                new SERKolona("JM", "JM", 50),
                new SERKolona("KnjigovodstvenaKolicina", "Knjigovodstveno", 120, "kolicina"),
                new SERKolona("PrebrojanaKolicina", "Prebrojano", 100, "kolicina"),
                new SERKolona("Razlika", "Razlika", 85, "kolicina"),
                new SERKolona("Cena", "Cena RSD", 100, "iznos"),
                new SERKolona("VrednostRazlike", "Vrednost razlike RSD", 150, "iznos"),
                new SERKolona("Napomena", "Napomena", 200));
            SERGrid.BojaTeksta(gridStavke, (r, k) =>
            {
                if (k != "Razlika" && k != "VrednostRazlike")
                    return null;
                decimal raz = SERBaza.Decimalni(r.GetValue("Razlika"));
                return raz < 0 ? SERStil.StatusIsteklo : raz > 0 ? SERStil.StatusVazi : (Color?)null;
            });
            SERGrid.DuplikKlik(gridStavke, UnesiPrebrojano);

            var telo = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 16, 20, 0), BackColor = SavaTheme.Surface };
            var podela = SERStil.Podeljeno("Popisi", Okvir(gridPopisi), "Popisna lista (dupli klik: unos prebrojane količine)", Okvir(gridStavke), 38);
            podela.Dock = DockStyle.Fill;
            telo.Controls.Add(podela);

            var alati = SERStil.AlatnaTraka(this);
            alati.Dugme("Unesi prebrojano", (s, e) => UnesiPrebrojano());
            chipRazlika = alati.Chip("Samo sa razlikom", false, (s, e) => { samoRazlika = !samoRazlika; UcitajStavke(); });
            alati.Dugme("Pošalji na odobrenje", (s, e) => Posalji());
            alati.Dugme("Odobri razlike", (s, e) => Odobri(true));
            alati.Dugme("Vrati na brojanje", (s, e) => Odobri(false));
            alati.Dugme("Storniraj", (s, e) => Storniraj(), "danger");
            alati.Dugme("Excel", (s, e) => SERGrid.IzvozUExcel(gridStavke, "Popisna lista"));
            alati.DugmeDesno("+ Novi popis", (s, e) => NoviPopis());

            var naslov = SERStil.NaslovnaTraka(this, "Magacin › Popis zaliha");
            info = naslov.DodajInfo("");
            SERStil.StatusnaLinija(this).Text = "Razlika se vrednuje po prosečnoj ceni kad je popis otvoren · Neprebrojana stavka ne menja zalihu · Odobrava šef tehničke službe";

            Controls.Add(telo);
            telo.BringToFront();
            SERStil.PrimeniTemu(this);
            VisibleChanged += (s, e) => { if (Visible) Ucitaj(); };
        }

        private static Control Okvir(Control grid)
        {
            var okvir = SERStil.BeliPanel();
            grid.Dock = DockStyle.Fill;
            okvir.Controls.Add(grid);
            return okvir;
        }

        private int Popis => SERGrid.IzabraniId(gridPopisi);
        private int Status => SERBaza.CeoBroj(SERGrid.Vrednost(gridPopisi, "Status"));

        private void Ucitaj()
        {
            try
            {
                SERGrid.Napuni(gridPopisi, SERBaza.Tabela(@"
                    SELECT ID, Broj, Datum, ISNULL(Grupa, N'svi delovi') AS Grupa, Status, StatusTekst, BrojStavki, Prebrojano, SaRazlikom,
                           VrednostManjka, VrednostViska, ISNULL(OdobrioKorisnik, N'') AS OdobrioKorisnik
                    FROM dbo.vSERPopis ORDER BY Datum DESC, ID DESC"));
            }
            catch (Exception ex)
            {
                SERDijalog.Greska(this, Text, ex);
                return;
            }
            UcitajStavke();
        }

        private void UcitajStavke()
        {
            chipRazlika.Aktivan = samoRazlika;
            object manjak = SERGrid.Vrednost(gridPopisi, "VrednostManjka"), visak = SERGrid.Vrednost(gridPopisi, "VrednostViska");
            info.Text = Popis > 0
                ? SERGrid.Vrednost(gridPopisi, "Broj") + " · manjak " + SERBaza.Iznos(SERBaza.Decimalni(manjak)) + " RSD · višak " + SERBaza.Iznos(SERBaza.Decimalni(visak)) + " RSD"
                : "";
            try
            {
                SERGrid.Napuni(gridStavke, SERBaza.Tabela(@"
                    SELECT s.ID, d.Sifra, d.Naziv, ISNULL(d.Lokacija, N'') AS Lokacija, d.JM, s.KnjigovodstvenaKolicina, s.PrebrojanaKolicina,
                           s.Razlika, s.Cena, s.VrednostRazlike, ISNULL(s.Napomena, N'') AS Napomena
                    FROM dbo.SERPopisStavka s JOIN dbo.SERDeo d ON d.ID = s.DeoID
                    WHERE s.PopisID = @P AND (@Razlika = 0 OR s.Razlika <> 0)
                    ORDER BY d.Lokacija, d.Sifra", SERBaza.P("@P", Popis), SERBaza.P("@Razlika", samoRazlika ? 1 : 0)));
            }
            catch (Exception ex)
            {
                SERDijalog.Greska(this, Text, ex);
            }
        }

        private void NoviPopis()
        {
            if (!SERPrava.Proveri(this, SERPrava.Magacioner))
                return;
            var polja = new List<SERPolje>
            {
                SERPolje.Datum("Datum", "Datum popisa", true),
                SERPolje.Lista("GrupaID", "Grupa delova (prazno = svi delovi)", SERListe.Grupe),
                SERPolje.Napomena()
            };
            if (SERDijalog.Unos(this, "Novi popis", polja, new SERVrednosti { ["Datum"] = DateTime.Today }, v =>
                    SERBaza.Procedura("dbo.SER_PopisNovi",
                        SERBaza.P("@Datum", v.Uzmi("Datum")), SERBaza.P("@GrupaID", v.Uzmi("GrupaID")),
                        SERBaza.P("@Napomena", SERBaza.Tekst(v.Tekst("Napomena"))), SERBaza.P("@Korisnik", SERBaza.Korisnik),
                        SERBaza.Izlaz("@ID", SqlDbType.Int), SERBaza.Izlaz("@Broj", SqlDbType.NVarChar, 20))))
                Ucitaj();
        }

        private void UnesiPrebrojano()
        {
            int stavka = SERGrid.IzabraniId(gridStavke);
            if (stavka <= 0)
                return;
            if (Status != 1)
            {
                MessageBox.Show(this, "Prebrojano se unosi samo dok je popis u toku.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!SERPrava.Proveri(this, SERPrava.Magacioner))
                return;
            var polja = new List<SERPolje>
            {
                new SERPolje("Deo", "Deo", SERTip.Tekst) { SamoCitanje = true, Raspon = 2 },
                new SERPolje("Knjigovodstveno", "Knjigovodstveno", SERTip.Decimalni) { SamoCitanje = true },
                SERPolje.Decimalni("Prebrojano", "Prebrojano", true),
                SERPolje.Napomena()
            };
            var pocetne = new SERVrednosti
            {
                ["Deo"] = SERGrid.Vrednost(gridStavke, "Sifra") + " – " + SERGrid.Vrednost(gridStavke, "Naziv"),
                ["Knjigovodstveno"] = SERGrid.Vrednost(gridStavke, "KnjigovodstvenaKolicina"),
                ["Prebrojano"] = SERGrid.Vrednost(gridStavke, "PrebrojanaKolicina"),
                ["Napomena"] = SERGrid.Vrednost(gridStavke, "Napomena")
            };
            if (SERDijalog.Unos(this, "Prebrojana količina", polja, pocetne, v =>
                    SERBaza.Procedura("dbo.SER_PopisUnesi",
                        SERBaza.P("@StavkaID", stavka), SERBaza.P("@Prebrojano", v.Uzmi("Prebrojano")),
                        SERBaza.P("@Napomena", SERBaza.Tekst(v.Tekst("Napomena"))), SERBaza.P("@Korisnik", SERBaza.Korisnik))))
                Osvezi();
        }

        // Posle izmene: ponovo učitava popise, a izabrani popis ostaje izabran
        private void Osvezi()
        {
            int izabran = Popis;
            Ucitaj();
            if (gridPopisi.Table == null)
                return;
            foreach (Syncfusion.Grouping.Record r in gridPopisi.Table.Records)
                if (SERBaza.CeoBroj(r.GetValue("ID")) == izabran)
                {
                    gridPopisi.Table.SelectedRecords.Clear();
                    gridPopisi.Table.SelectedRecords.Add(r);
                    r.SetCurrent();
                    break;
                }
        }

        private void Posalji()
        {
            if (Popis <= 0 || !SERPrava.Proveri(this, SERPrava.Magacioner))
                return;
            if (SERDijalog.Pokusaj(this, Text, () => SERBaza.Procedura("dbo.SER_PopisPosalji", SERBaza.P("@ID", Popis), SERBaza.P("@Korisnik", SERBaza.Korisnik))))
                Osvezi();
        }

        private void Odobri(bool odobri)
        {
            if (Popis <= 0 || !SERPrava.ProveriOdobravanje(this))
                return;
            if (odobri && !SERDijalog.Potvrdi(this, "Odobriti razlike? Zaliha se menja za sve prebrojane stavke sa razlikom.", Text))
                return;
            string komentar = odobri ? "" : SERDijalog.Razlog(this, "Vrati na brojanje", "Šta treba ponovo prebrojati", true);
            if (komentar == null)
                return;
            if (SERDijalog.Pokusaj(this, Text, () => SERBaza.Procedura("dbo.SER_PopisOdobri",
                    SERBaza.P("@ID", Popis), SERBaza.P("@Odobri", odobri), SERBaza.P("@Komentar", SERBaza.Tekst(komentar)),
                    SERBaza.P("@Korisnik", SERBaza.Korisnik))))
                Osvezi();
        }

        private void Storniraj()
        {
            if (Popis <= 0 || !SERPrava.Proveri(this, SERPrava.Magacioner))
                return;
            string razlog = SERDijalog.Razlog(this, "Storniraj popis", "Razlog storniranja", true);
            if (razlog != null && SERDijalog.Pokusaj(this, Text, () => SERBaza.Procedura("dbo.SER_PopisStorniraj",
                    SERBaza.P("@ID", Popis), SERBaza.P("@Razlog", razlog), SERBaza.P("@Korisnik", SERBaza.Korisnik))))
                Osvezi();
        }
    }
}
