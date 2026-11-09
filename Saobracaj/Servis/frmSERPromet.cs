using Saobracaj.Theme;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Servis
{
    // Izdatnice i prijemi: sav promet sa filterom po vrsti i periodu, detalj dokumenta, zbir ulaza i izlaza
    public class frmSERPromet : Form
    {
        private static readonly string[] Vrste = { "", "IZD", "PRM", "POV", "POP" };
        private static readonly string[] VrsteNaziv = { "Sve", "Izdavanje", "Prijem", "Povrat", "Popis i korekcija" };
        private static readonly int[] Periodi = { 30, 90, 365, 0 };

        private readonly SERTekst info;
        private readonly TextBox pretraga;
        private readonly List<SERChip> chipVrste = new List<SERChip>();
        private readonly SERChip chipPeriod;
        private readonly GridGroupingControl grid, gridStavke;
        private readonly Timer odlozenaPretraga = new Timer { Interval = 300 };
        private DataTable podaci;
        private int vrsta, period;

        public frmSERPromet()
        {
            SERStil.PripremiFormu(this, "Izdatnice i prijemi");

            grid = SERGrid.Novi();
            SERGrid.Kolone(grid,
                new SERKolona("Datum", "Datum", 95, "datum"),
                new SERKolona("TipNaziv", "Vrsta", 120),
                new SERKolona("Broj", "Dokument", 120),
                new SERKolona("RadniNalog", "Radni nalog", 120),
                new SERKolona("Narudzbenica", "Narudžbenica", 125),
                new SERKolona("Dobavljac", "Dobavljač", 200),
                new SERKolona("BrojStavki", "Stavki", 65, "broj"),
                new SERKolona("VrednostUlaza", "Ulaz RSD", 115, "iznos"),
                new SERKolona("VrednostIzlaza", "Izlaz RSD", 115, "iznos"),
                new SERKolona("Razlog", "Razlog / napomena", 220),
                new SERKolona("KreiraoKorisnik", "Korisnik", 100));
            grid.SelectedRecordsChanged += (s, e) => UcitajStavke();

            gridStavke = SERGrid.Novi();
            SERGrid.Kolone(gridStavke,
                new SERKolona("Sifra", "Šifra", 100),
                new SERKolona("Naziv", "Deo", 260),
                new SERKolona("Kolicina", "Količina", 90, "kolicina"),
                new SERKolona("JM", "JM", 50),
                new SERKolona("Cena", "Cena RSD", 100, "iznos"),
                new SERKolona("Vrednost", "Vrednost RSD", 115, "iznos"),
                new SERKolona("Vraceno", "Vraćeno", 85, "kolicina"),
                new SERKolona("Napomena", "Napomena", 240));

            var telo = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 16, 20, 0), BackColor = SavaTheme.Surface };
            var podela = SERStil.Podeljeno("Dokumenti", Okvir(grid), "Stavke izabranog dokumenta", Okvir(gridStavke), 58);
            podela.Dock = DockStyle.Fill;
            telo.Controls.Add(podela);

            var alati = SERStil.AlatnaTraka(this);
            pretraga = alati.Pretraga("Pretraga: broj, nalog, dobavljač…", 240);
            pretraga.TextChanged += (s, e) => { odlozenaPretraga.Stop(); odlozenaPretraga.Start(); };
            odlozenaPretraga.Tick += (s, e) => { odlozenaPretraga.Stop(); Filtriraj(); };
            for (int i = 0; i < Vrste.Length; i++)
            {
                int k = i;
                chipVrste.Add(alati.Chip(VrsteNaziv[i], i == 0, (s, e) => { vrsta = k; Filtriraj(); }));
            }
            chipPeriod = alati.Chip("", true, (s, e) => { period = (period + 1) % Periodi.Length; Ucitaj(); });
            alati.Dugme("Osveži", (s, e) => Ucitaj());
            alati.Dugme("Povrat", (s, e) => Povrat());
            alati.Dugme("Excel", (s, e) => SERGrid.IzvozUExcel(grid, "Promet delova"));
            alati.DugmeDesno("+ Novi prijem", (s, e) => NoviPrijem());

            var naslov = SERStil.NaslovnaTraka(this, "Magacin › Izdatnice i prijemi");
            info = naslov.DodajInfo("");
            var statusna = SERStil.StatusnaLinija(this);
            statusna.Text = "Izdavanje se radi sa radnog naloga · Povrat: izaberite stavku izdatnice pa „Povrat“ · Prijem bez narudžbenice traži razlog";

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

        private void Ucitaj()
        {
            int dana = Periodi[period];
            chipPeriod.Text = dana == 0 ? "Period: sve" : dana == 365 ? "Period: godina" : "Period: " + dana + " dana";
            try
            {
                podaci = SERBaza.Tabela(@"
                    SELECT ID, Tip, TipNaziv, Broj, Datum, ISNULL(RadniNalog, N'') AS RadniNalog, ISNULL(Narudzbenica, N'') AS Narudzbenica,
                           ISNULL(Dobavljac, N'') AS Dobavljac, BrojStavki, VrednostUlaza, VrednostIzlaza,
                           ISNULL(Razlog, ISNULL(Napomena, N'')) AS Razlog, KreiraoKorisnik
                    FROM dbo.vSERPromet
                    WHERE @Dana = 0 OR Datum >= DATEADD(DAY, -@Dana, CAST(GETDATE() AS date))
                    ORDER BY Datum DESC, ID DESC", SERBaza.P("@Dana", dana));
            }
            catch (Exception ex)
            {
                SERDijalog.Greska(this, Text, ex);
                podaci = new DataTable();
                return;
            }
            Filtriraj();
        }

        private void Filtriraj()
        {
            if (podaci == null || podaci.Columns.Count == 0)
                return;
            for (int i = 0; i < chipVrste.Count; i++)
                chipVrste[i].Aktivan = i == vrsta;

            var uslovi = new List<string>();
            if (Vrste[vrsta] == "POP")
                uslovi.Add("Tip IN ('POP', 'KOR', 'PST')");
            else if (Vrste[vrsta] != "")
                uslovi.Add("Tip = '" + Vrste[vrsta] + "'");
            string t = pretraga.Text.Trim();
            if (t != "")
            {
                string e = t.Replace("'", "''").Replace("[", "[[]").Replace("*", "[*]").Replace("%", "[%]");
                uslovi.Add("(Broj LIKE '%" + e + "%' OR RadniNalog LIKE '%" + e + "%' OR Narudzbenica LIKE '%" + e + "%' OR Dobavljac LIKE '%" + e + "%' OR Razlog LIKE '%" + e + "%')");
            }
            DataTable prikaz = new DataView(podaci) { RowFilter = string.Join(" AND ", uslovi) }.ToTable();
            decimal ulaz = 0, izlaz = 0;
            foreach (DataRow r in prikaz.Rows)
            {
                ulaz += SERBaza.Decimalni(r["VrednostUlaza"]);
                izlaz += SERBaza.Decimalni(r["VrednostIzlaza"]);
            }
            info.Text = prikaz.Rows.Count + " dokumenata · ulaz " + SERBaza.Iznos(ulaz) + " RSD · izlaz " + SERBaza.Iznos(izlaz) + " RSD";
            SERGrid.Napuni(grid, prikaz);
            UcitajStavke();
        }

        private void UcitajStavke()
        {
            int dok = SERGrid.IzabraniId(grid);
            try
            {
                SERGrid.Napuni(gridStavke, SERBaza.Tabela(@"
                    SELECT k.ID, d.Sifra, d.Naziv, d.JM, k.Kolicina, k.Cena, k.Vrednost, k.Tip,
                           (SELECT SUM(p.Kolicina) FROM dbo.SERDeoKretanje p WHERE p.IzvorKretanjeID = k.ID AND p.Tip = 'POV') AS Vraceno,
                           ISNULL(k.Napomena, N'') AS Napomena
                    FROM dbo.SERDeoKretanje k JOIN dbo.SERDeo d ON d.ID = k.DeoID
                    WHERE k.DokumentID = @Dok ORDER BY k.ID", SERBaza.P("@Dok", dok)));
            }
            catch (Exception ex)
            {
                SERDijalog.Greska(this, Text, ex);
            }
        }

        private void NoviPrijem()
        {
            if (!SERPrava.Proveri(this, SERPrava.Magacioner))
                return;
            using (var f = new frmSERPrijem(0))
                if (f.ShowDialog(this) == DialogResult.OK)
                    Ucitaj();
        }

        // Povrat nepotrošenog dela: stavka izdatnice izabrana u donjoj tabeli
        private void Povrat()
        {
            int kretanje = SERGrid.IzabraniId(gridStavke);
            if (kretanje <= 0 || Convert.ToString(SERGrid.Vrednost(gridStavke, "Tip")) != "IZD")
            {
                MessageBox.Show(this, "Izaberite izdatnicu, pa u donjoj tabeli stavku koja se vraća.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (frmSERNalog.PovratStavke(this, kretanje))
                Ucitaj();
        }
    }
}
