using Saobracaj.Theme;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Servis
{
    // Radni nalog → Delovi iz magacina: rezervacija (MD-R01), izdavanje (MD-R02), čekanje delova (MD-R03), povrat (MD-R04)
    public class frmSERNalog : Form
    {
        private readonly int id;
        private readonly SERNaslovnaTraka naslov;
        private readonly FlowLayoutPanel bedzevi;
        private readonly GridGroupingControl grid;
        private readonly Label statusna;
        private int status;
        private Button btnDodaj, btnStornoStavke, btnPovrat, btnIzdaj, btnStornoNaloga, btnZavrsi, btnOtvori;

        public frmSERNalog(int id)
        {
            this.id = id;
            SERStil.PripremiFormu(this, "Radni nalog");

            var telo = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 16, 20, 0), BackColor = SavaTheme.Surface };
            var okvir = SERStil.BeliPanel();
            okvir.Dock = DockStyle.Fill;
            grid = SERGrid.Novi();
            okvir.Controls.Add(grid);
            var sekcija = SERStil.Sekcija("Delovi iz magacina", okvir);
            sekcija.Dock = DockStyle.Fill;
            telo.Controls.Add(sekcija);
            SERGrid.Kolone(grid,
                new SERKolona("Sifra", "Šifra", 100),
                new SERKolona("Naziv", "Deo", 260),
                new SERKolona("Kolicina", "Količina", 85, "kolicina"),
                new SERKolona("JM", "JM", 50),
                new SERKolona("StatusTekst", "Status", 130),
                new SERKolona("Raspolozivo", "Raspoloživo", 95, "kolicina"),
                new SERKolona("Izdato", "Izdato", 80, "kolicina"),
                new SERKolona("Vraceno", "Vraćeno", 80, "kolicina"),
                new SERKolona("Trosak", "Trošak RSD", 110, "iznos"),
                new SERKolona("Stavka", "Stavka naloga", 160),
                new SERKolona("Napomena", "Napomena", 180));
            SERGrid.BojaTeksta(grid, (r, k) => k == "StatusTekst" ? SERGrid.BojaStatusa("Rezervacija", SERBaza.CeoBroj(r.GetValue("Status"))) : null);

            var alati = SERStil.AlatnaTraka(this);
            btnDodaj = alati.Dugme("Dodaj deo", (s, e) => DodajDeo());
            btnStornoStavke = alati.Dugme("Storniraj stavku", (s, e) => StornirajStavku(), "danger");
            btnPovrat = alati.Dugme("Povrat", (s, e) => Povrat());
            alati.Dugme("Osveži", (s, e) => Ucitaj());
            btnIzdaj = alati.DugmeDesno("Izdaj rezervisane delove", (s, e) => Izdaj());

            bedzevi = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 48, Padding = new Padding(20, 10, 20, 0), BackColor = SavaTheme.White, WrapContents = false };

            naslov = SERStil.NaslovnaTraka(this, "Radni nalog");
            btnStornoNaloga = naslov.DodajDugme("Storniraj nalog", false, (s, e) => StornirajNalog());
            btnZavrsi = naslov.DodajDugme("Završi nalog", false, (s, e) => Zavrsi());
            btnOtvori = naslov.DodajDugme("Ponovo otvori", false, (s, e) => PonovoOtvori());
            statusna = SERStil.StatusnaLinija(this);

            Controls.Add(bedzevi);
            Controls.Add(telo);
            telo.BringToFront();
            bedzevi.SendToBack();
            alati.SendToBack();
            naslov.SendToBack();
            statusna.Parent.SendToBack();
            SERStil.PrimeniTemu(this);
            VisibleChanged += (s, e) => { if (Visible) Ucitaj(); };
        }

        private void Ucitaj()
        {
            DataRow r;
            try
            {
                r = SERBaza.Red(@"
                    SELECT t.*, rn.Opis, rn.DatumZavrsetka, rn.Redovni, s.Naziv AS SredstvoNaziv, ISNULL(p.Naziv, N'') AS Servis,
                           rn.KreiraoKorisnik, rn.DatumKreiranja
                    FROM dbo.vSERRadniNalogTrosak t
                    JOIN dbo.SERRadniNalog rn ON rn.ID = t.RadniNalogID
                    JOIN dbo.SERSredstvo s ON s.ID = rn.SredstvoID
                    LEFT JOIN dbo.SERPlanStavka p ON p.ID = rn.PlanStavkaID
                    WHERE t.RadniNalogID = @ID", SERBaza.P("@ID", id));
                if (r == null)
                    return;
                SERGrid.Napuni(grid, SERBaza.Tabela(@"
                    SELECT RezervacijaID AS ID, Sifra, Naziv, Kolicina, JM, Status, StatusTekst, Raspolozivo, Izdato, Vraceno, Trosak,
                           Stavka, ISNULL(Napomena, N'') AS Napomena
                    FROM dbo.vSERRadniNalogDelovi WHERE RadniNalogID = @ID ORDER BY RezervacijaID", SERBaza.P("@ID", id)));
            }
            catch (Exception ex)
            {
                SERDijalog.Greska(this, Text, ex);
                return;
            }
            status = SERBaza.CeoBroj(r["Status"]);
            // završen i storniran nalog ne dozvoljava izmene delova; završen se može ponovo otvoriti uz razlog
            bool otvoren = status >= 1 && status <= 3;
            btnDodaj.Enabled = btnStornoStavke.Enabled = btnPovrat.Enabled = btnIzdaj.Enabled = btnStornoNaloga.Enabled = btnZavrsi.Enabled = otvoren;
            btnOtvori.Visible = status == 4;
            string broj = Convert.ToString(r["Broj"]);
            Text = broj;
            naslov.Naslov.Text = "Radni nalozi › " + broj + " · " + r["Sredstvo"] + " " + r["SredstvoNaziv"];

            bedzevi.Controls.Clear();
            Color? boja = SERGrid.BojaStatusa("RadniNalog", status);
            bedzevi.Controls.Add(new SERBedz(Convert.ToString(r["StatusTekst"]), status == 3 ? SERStil.PozadinaIsteklo : SavaTheme.SelectedRow, boja ?? SavaTheme.Primary));
            bedzevi.Controls.Add(new SERBedz("Datum " + SERBaza.DatumTekst(r["Datum"]), SavaTheme.White, SavaTheme.Text));
            bedzevi.Controls.Add(new SERBedz(Convert.ToBoolean(r["Redovni"]) ? "Redovni: " + r["Servis"] : "Vanredni", SavaTheme.White, SavaTheme.Text));
            bedzevi.Controls.Add(new SERBedz("Trošak delova " + SERBaza.Iznos(SERBaza.Decimalni(r["TrosakDelova"])) + " RSD", SavaTheme.White, SavaTheme.Text));
            if (r["DatumZavrsetka"] != DBNull.Value)
                bedzevi.Controls.Add(new SERBedz("Završen " + SERBaza.DatumTekst(r["DatumZavrsetka"]), SavaTheme.White, SavaTheme.Text));

            statusna.Text = (r["Opis"] == DBNull.Value ? "" : Convert.ToString(r["Opis"]) + " · ")
                + "Otvorio " + r["KreiraoKorisnik"] + " " + SERBaza.DatumTekst(r["DatumKreiranja"])
                + " · U trošak ulaze samo izdati delovi, umanjeni za povrat";
        }

        private bool Otvoren()
        {
            if (status >= 1 && status <= 3)
                return true;
            MessageBox.Show(this, status == 4
                ? "Nalog je završen. Ponovo ga otvorite („Ponovo otvori“, uz razlog) da biste menjali delove."
                : "Nalog je storniran i ne može da se menja.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }

        private void PonovoOtvori()
        {
            if (status != 4 || !SERPrava.Proveri(this, SERPrava.Magacioner))
                return;
            string razlog = SERDijalog.Razlog(this, "Ponovo otvori nalog", "Razlog ponovnog otvaranja", true);
            if (razlog != null && SERDijalog.Pokusaj(this, Text, () => SERBaza.Procedura("dbo.SER_RadniNalogOtvori",
                    SERBaza.P("@RadniNalogID", id), SERBaza.P("@Razlog", razlog), SERBaza.P("@Korisnik", SERBaza.Korisnik))))
                Ucitaj();
        }

        private void DodajDeo()
        {
            if (!Otvoren() || !SERPrava.Proveri(this, SERPrava.Magacioner))
                return;
            // delovi primenljivi na vrstu sredstva sa naloga su na vrhu liste
            string upit = @"
                SELECT s.ID, s.Sifra + N' – ' + s.Naziv + N' (raspoloživo ' + FORMAT(s.Raspolozivo, N'0.###') + N' ' + s.JM + N')' AS Naziv
                FROM dbo.vSERDeoStanje s
                CROSS JOIN (SELECT sr.VrstaSredstvaID, sr.ID AS SredstvoID FROM dbo.SERRadniNalog rn JOIN dbo.SERSredstvo sr ON sr.ID = rn.SredstvoID WHERE rn.ID = " + id + @") n
                WHERE s.Aktivno = 1
                ORDER BY CASE WHEN EXISTS (SELECT 1 FROM dbo.SERDeoPrimenljivost p WHERE p.DeoID = s.ID AND p.VrstaSredstvaID = n.VrstaSredstvaID
                                           AND (p.SredstvoID IS NULL OR p.SredstvoID = n.SredstvoID)) THEN 0 ELSE 1 END, s.Sifra";
            var polja = new List<SERPolje>
            {
                new SERPolje("DeoID", "Deo", SERTip.Lista, true) { ListaUpit = upit, Raspon = 2 },
                SERPolje.Kolicina("Kolicina", "Količina"),
                SERPolje.Tekst("Napomena", "Napomena", 500)
            };
            string poruka = null;
            if (SERDijalog.Unos(this, "Dodaj deo na nalog", polja, null, v =>
                {
                    var rez = SERBaza.Izlaz("@RezervacijaID", SqlDbType.Int);
                    SERBaza.Procedura("dbo.SER_RezervacijaDodaj",
                        SERBaza.P("@RadniNalogID", id), SERBaza.P("@RadniNalogStavkaID", null), SERBaza.P("@DeoID", v.Uzmi("DeoID")),
                        SERBaza.P("@Kolicina", v.Uzmi("Kolicina")), SERBaza.P("@Napomena", SERBaza.Tekst(v.Tekst("Napomena"))),
                        SERBaza.P("@Korisnik", SERBaza.Korisnik), rez);
                    if (SERBaza.CeoBroj(SERBaza.Skalar("SELECT Status FROM dbo.SERDeoRezervacija WHERE ID = @ID", SERBaza.P("@ID", rez.Value))) == 3)
                        poruka = "Deo nema dovoljno na stanju. Stavka je upisana kao „Nema na stanju“, a nalog čeka delove.";
                }))
            {
                Ucitaj();
                if (poruka != null)
                    MessageBox.Show(this, poruka, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void StornirajStavku()
        {
            int rez = SERGrid.IzabraniId(grid);
            if (rez <= 0 || !SERPrava.Proveri(this, SERPrava.Magacioner))
                return;
            if (SERDijalog.Potvrdi(this, "Stornirati izabranu stavku?", Text)
                && SERDijalog.Pokusaj(this, Text, () => SERBaza.Procedura("dbo.SER_RezervacijaStorno",
                       SERBaza.P("@RezervacijaID", rez), SERBaza.P("@Korisnik", SERBaza.Korisnik))))
                Ucitaj();
        }

        private void Izdaj()
        {
            if (!Otvoren() || !SERPrava.Proveri(this, SERPrava.Magacioner))
                return;
            var broj = SERBaza.Izlaz("@Broj", SqlDbType.NVarChar, 20);
            if (!SERDijalog.Potvrdi(this, "Izdati sve rezervisane delove sa naloga?", Text))
                return;
            // izdavanje po ceni 0 ne sme tiho da prođe: trošak naloga bi bio 0 za te delove
            var bezCene = new List<string>();
            foreach (DataRow r in SERBaza.Tabela(@"
                    SELECT d.Sifra FROM dbo.vSERRadniNalogDelovi v JOIN dbo.SERDeo d ON d.ID = v.DeoID
                    WHERE v.RadniNalogID = @N AND v.Status = 1 AND d.ProsecnaCena = 0", SERBaza.P("@N", id)).Rows)
                bezCene.Add(Convert.ToString(r["Sifra"]));
            if (bezCene.Count > 0 && !SERDijalog.Potvrdi(this,
                    "Prosečna cena je 0 za: " + string.Join(", ", bezCene) + ".\nTrošak naloga za te delove biće 0 RSD. Izdati svejedno?", Text))
                return;
            if (SERDijalog.Pokusaj(this, Text, () => SERBaza.Procedura("dbo.SER_Izdaj",
                    SERBaza.P("@RadniNalogID", id), SERBaza.P("@RezervacijaID", null), SERBaza.P("@Datum", DateTime.Today),
                    SERBaza.P("@Napomena", null), SERBaza.P("@Korisnik", SERBaza.Korisnik),
                    SERBaza.Izlaz("@DokumentID", SqlDbType.Int), broj)))
            {
                Ucitaj();
                MessageBox.Show(this, "Izdatnica " + broj.Value + " je proknjižena.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Povrat()
        {
            if (!Otvoren())
                return;
            int rez = SERGrid.IzabraniId(grid);
            if (rez <= 0)
                return;
            int kretanje = SERBaza.CeoBroj(SERBaza.Skalar("SELECT ID FROM dbo.SERDeoKretanje WHERE RezervacijaID = @R AND Tip = 'IZD'", SERBaza.P("@R", rez)));
            if (kretanje <= 0)
            {
                MessageBox.Show(this, "Vraća se samo izdat deo.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (PovratStavke(this, kretanje))
                Ucitaj();
        }

        // Povrat nepotrošenog dela sa stavke izdatnice (koristi i forma prometa)
        public static bool PovratStavke(IWin32Window vlasnik, int izdavanjeKretanjeID)
        {
            if (!SERPrava.Proveri(vlasnik, SERPrava.Magacioner))
                return false;
            DataRow r = SERBaza.Red(@"
                SELECT d.Sifra + N' – ' + d.Naziv AS Deo, -k.Kolicina - ISNULL((SELECT SUM(p.Kolicina) FROM dbo.SERDeoKretanje p
                                                                              WHERE p.IzvorKretanjeID = k.ID AND p.Tip = 'POV'), 0) AS Moze
                FROM dbo.SERDeoKretanje k JOIN dbo.SERDeo d ON d.ID = k.DeoID WHERE k.ID = @ID", SERBaza.P("@ID", izdavanjeKretanjeID));
            decimal moze = SERBaza.Decimalni(r?["Moze"]);
            if (moze <= 0)
            {
                MessageBox.Show(vlasnik, "Izdata količina je već vraćena.", "Povrat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            var polja = new List<SERPolje>
            {
                new SERPolje("Deo", "Deo", SERTip.Tekst) { SamoCitanje = true, Raspon = 2 },
                new SERPolje("Kolicina", "Količina (najviše " + SERBaza.Kolicina(moze) + ")", SERTip.Decimalni, true) { Pozitivan = true, Max = moze, NazivUPoruci = "Količina" },
                SERPolje.Datum("Datum", "Datum", true),
                new SERPolje("Napomena", "Razlog povrata", SERTip.Tekst, false, 500) { Raspon = 2 }
            };
            var pocetne = new SERVrednosti { ["Deo"] = r["Deo"], ["Kolicina"] = moze, ["Datum"] = DateTime.Today };
            return SERDijalog.Unos(vlasnik, "Povrat dela na zalihu", polja, pocetne, v =>
                SERBaza.Procedura("dbo.SER_Povrat",
                    SERBaza.P("@IzdavanjeKretanjeID", izdavanjeKretanjeID), SERBaza.P("@Kolicina", v.Uzmi("Kolicina")),
                    SERBaza.P("@Datum", v.Uzmi("Datum")), SERBaza.P("@Napomena", SERBaza.Tekst(v.Tekst("Napomena"))),
                    SERBaza.P("@Korisnik", SERBaza.Korisnik),
                    SERBaza.Izlaz("@DokumentID", SqlDbType.Int), SERBaza.Izlaz("@Broj", SqlDbType.NVarChar, 20)));
        }

        private void Zavrsi()
        {
            if (!Otvoren() || !SERPrava.Proveri(this, SERPrava.Magacioner))
                return;
            if (SERDijalog.Potvrdi(this, "Završiti nalog? Rezervisani a neizdati delovi i stavke kojih nema na stanju se storniraju.", Text)
                && SERDijalog.Pokusaj(this, Text, () => SERBaza.Procedura("dbo.SER_RadniNalogZavrsi",
                       SERBaza.P("@RadniNalogID", id), SERBaza.P("@Datum", DateTime.Today), SERBaza.P("@Korisnik", SERBaza.Korisnik))))
                Ucitaj();
        }

        private void StornirajNalog()
        {
            if (!Otvoren() || !SERPrava.Proveri(this, SERPrava.Magacioner))
                return;
            string razlog = SERDijalog.Razlog(this, "Storniraj nalog", "Razlog storniranja", true);
            if (razlog != null && SERDijalog.Pokusaj(this, Text, () => SERBaza.Procedura("dbo.SER_RadniNalogStorniraj",
                    SERBaza.P("@RadniNalogID", id), SERBaza.P("@Razlog", razlog), SERBaza.P("@Korisnik", SERBaza.Korisnik))))
                Ucitaj();
        }
    }
}
