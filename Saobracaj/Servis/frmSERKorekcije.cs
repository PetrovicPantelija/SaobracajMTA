using Saobracaj.Theme;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Servis
{
    // Korekcija zaliha (MD-R11): zahtev sa razlogom; zaliha se menja tek kad ga odobri šef tehničke službe
    public class frmSERKorekcije : Form
    {
        private readonly SERChip chipCekaju;
        private readonly GridGroupingControl grid;
        private bool samoCekaju = true;

        public frmSERKorekcije()
        {
            SERStil.PripremiFormu(this, "Korekcije zaliha");

            var telo = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 16, 20, 0), BackColor = SavaTheme.Surface };
            var okvir = SERStil.BeliPanel();
            okvir.Dock = DockStyle.Fill;
            grid = SERGrid.Novi();
            okvir.Controls.Add(grid);
            telo.Controls.Add(okvir);
            SERGrid.Kolone(grid,
                new SERKolona("DatumKreiranja", "Zahtev", 130, "datumvreme"),
                new SERKolona("Sifra", "Šifra", 100),
                new SERKolona("Naziv", "Deo", 240),
                new SERKolona("Kolicina", "Korekcija", 90, "kolicina"),
                new SERKolona("JM", "JM", 50),
                new SERKolona("Stanje", "Stanje sada", 100, "kolicina"),
                new SERKolona("Razlog", "Razlog", 260),
                new SERKolona("StatusTekst", "Status", 130),
                new SERKolona("KreiraoKorisnik", "Tražio", 100),
                new SERKolona("OdobrioKorisnik", "Odlučio", 100),
                new SERKolona("Dokument", "Dokument", 110));
            SERGrid.BojaTeksta(grid, (r, k) => k == "StatusTekst" ? SERGrid.BojaStatusa("Korekcija", SERBaza.CeoBroj(r.GetValue("Status")))
                : k == "Kolicina" ? (SERBaza.Decimalni(r.GetValue("Kolicina")) < 0 ? SERStil.StatusIsteklo : SERStil.StatusVazi) : (Color?)null);

            SERGrid.TekstUTooltipu(grid, "Razlog");

            var alati = SERStil.AlatnaTraka(this);
            chipCekaju = alati.Chip("Čekaju odobrenje", true, (s, e) => { samoCekaju = !samoCekaju; Ucitaj(); });
            alati.Dugme("Osveži", (s, e) => Ucitaj());
            alati.Dugme("Odobri", (s, e) => Odluka(true));
            alati.Dugme("Odbij", (s, e) => Odluka(false), "danger");
            alati.DugmeDesno("+ Nova korekcija", (s, e) => { if (Nova(this, 0)) Ucitaj(); });

            SERStil.NaslovnaTraka(this, "Magacin › Korekcije zaliha");
            SERStil.StatusnaLinija(this).Text = "Korekcija traži razlog i odobrenje · Odobrena korekcija ide u karticu dela po prosečnoj ceni";

            Controls.Add(telo);
            telo.BringToFront();
            SERStil.PrimeniTemu(this);
            VisibleChanged += (s, e) => { if (Visible) Ucitaj(); };
        }

        private void Ucitaj()
        {
            chipCekaju.Aktivan = samoCekaju;
            try
            {
                SERGrid.Napuni(grid, SERBaza.Tabela(@"
                    SELECT k.ID, k.DatumKreiranja, d.Sifra, d.Naziv, k.Kolicina, d.JM, s.Stanje, k.Razlog, k.Status, st.Naziv AS StatusTekst,
                           k.KreiraoKorisnik, ISNULL(k.OdobrioKorisnik, N'') AS OdobrioKorisnik, ISNULL(dok.Broj, N'') AS Dokument
                    FROM dbo.SERKorekcija k
                    JOIN dbo.SERDeo d ON d.ID = k.DeoID
                    CROSS APPLY dbo.fnSER_Stanje(k.DeoID) s
                    JOIN dbo.SERStatus st ON st.Oblast = 'Korekcija' AND st.Status = k.Status
                    LEFT JOIN dbo.SERDokument dok ON dok.ID = k.DokumentID
                    WHERE @Sve = 1 OR k.Status = 1
                    ORDER BY k.DatumKreiranja DESC", SERBaza.P("@Sve", samoCekaju ? 0 : 1)));
            }
            catch (Exception ex)
            {
                SERDijalog.Greska(this, Text, ex);
            }
        }

        // Zahtev za korekciju; deoID > 0 predlaže deo (izabran u zalihama)
        public static bool Nova(IWin32Window vlasnik, int deoID)
        {
            if (!SERPrava.Proveri(vlasnik, SERPrava.Korekcija))
                return false;
            var polja = new List<SERPolje>
            {
                new SERPolje("DeoID", "Deo", SERTip.Lista, true) { ListaUpit = SERListe.AktivniDelovi, Raspon = 2 },
                new SERPolje("Kolicina", "Korekcija (+ višak, − manjak)", SERTip.Decimalni, true) { NeNula = true, Min = -999999999 },
                new SERPolje("Razlog", "Razlog", SERTip.VelikiTekst, true, 250) { Raspon = 2 }
            };
            var pocetne = new SERVrednosti();
            if (deoID > 0)
                pocetne["DeoID"] = deoID;
            return SERDijalog.Unos(vlasnik, "Zahtev za korekciju zalihe", polja, pocetne, v =>
                SERBaza.Procedura("dbo.SER_KorekcijaNova",
                    SERBaza.P("@DeoID", v.Uzmi("DeoID")), SERBaza.P("@Kolicina", v.Uzmi("Kolicina")), SERBaza.P("@Razlog", v.Tekst("Razlog")),
                    SERBaza.P("@Korisnik", SERBaza.Korisnik), SERBaza.Izlaz("@ID", SqlDbType.Int)));
        }

        private void Odluka(bool odobri)
        {
            int id = SERGrid.IzabraniId(grid);
            if (id <= 0 || !SERPrava.ProveriOdobravanje(this))
                return;
            string komentar = SERDijalog.Razlog(this, odobri ? "Odobri korekciju" : "Odbij korekciju", "Komentar", !odobri);
            if (komentar != null && SERDijalog.Pokusaj(this, Text, () => SERBaza.Procedura("dbo.SER_KorekcijaOdobri",
                    SERBaza.P("@ID", id), SERBaza.P("@Odobri", odobri), SERBaza.P("@Komentar", SERBaza.Tekst(komentar)),
                    SERBaza.P("@Korisnik", SERBaza.Korisnik))))
                Ucitaj();
        }
    }
}
