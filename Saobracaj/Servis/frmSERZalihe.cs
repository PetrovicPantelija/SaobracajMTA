using Saobracaj.Theme;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Servis
{
    // Zalihe i šifarnik: tabela delova sa stanjem, zbirna traka (vrednost zaliha, ispod minimuma, kritični)
    public class frmSERZalihe : Form
    {
        private readonly SERTekst info;
        private readonly TextBox pretraga;
        private readonly SERChip chipIspod, chipKriticni, chipNeaktivni;
        private readonly SERBrojKartica kVrednost, kIspod, kKriticni;
        private readonly GridGroupingControl grid;
        private readonly Timer odlozenaPretraga = new Timer { Interval = 300 };
        private DataTable podaci;
        private bool samoIspod, samoKriticni, iNeaktivni;
        private int pocetnoStanjeDokument;     // isti PST dokument za sve delove unete u ovom otvaranju forme

        public frmSERZalihe()
        {
            SERStil.PripremiFormu(this, "Zalihe i šifarnik");

            var telo = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 16, 20, 0), BackColor = SavaTheme.Surface };
            var okvir = SERStil.BeliPanel();
            okvir.Dock = DockStyle.Fill;
            grid = SERGrid.Novi();
            okvir.Controls.Add(grid);
            telo.Controls.Add(okvir);

            var zbir = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 126, BackColor = SavaTheme.Surface, WrapContents = false };
            kVrednost = Kartica(zbir, "Vrednost zaliha (RSD)", SavaTheme.Primary, null);
            kIspod = Kartica(zbir, "Delova ispod minimuma", SERStil.StatusIstice, () => { samoIspod = !samoIspod; Filtriraj(); });
            kKriticni = Kartica(zbir, "Kritičnih ispod minimuma", SavaTheme.Danger, () => { samoKriticni = !samoKriticni; Filtriraj(); });
            telo.Controls.Add(zbir);
            okvir.BringToFront();

            SERGrid.Kolone(grid,
                new SERKolona("Sifra", "Šifra", 100),
                new SERKolona("Naziv", "Naziv", 260),
                new SERKolona("Grupa", "Grupa", 140),
                new SERKolona("JM", "JM", 50),
                new SERKolona("Stanje", "Stanje", 80, "kolicina"),
                new SERKolona("Rezervisano", "Rezervisano", 95, "kolicina"),
                new SERKolona("Raspolozivo", "Raspoloživo", 95, "kolicina"),
                new SERKolona("MinZaliha", "Min", 70, "kolicina"),
                new SERKolona("Naruceno", "Naručeno", 85, "kolicina"),
                new SERKolona("ProsecnaCena", "Cena RSD", 100, "iznos"),
                new SERKolona("Vrednost", "Vrednost RSD", 115, "iznos"),
                new SERKolona("Lokacija", "Lokacija", 90),
                new SERKolona("KriticanTekst", "Kritičan", 75),
                new SERKolona("StatusTekst", "Status", 125));
            SERGrid.BojaTeksta(grid, BojaCelije);
            SERGrid.DuplikKlik(grid, OtvoriIzabrani);

            var alati = SERStil.AlatnaTraka(this);
            pretraga = alati.Pretraga("Pretraga: šifra, naziv, lokacija…", 260);
            pretraga.TextChanged += (s, e) => { odlozenaPretraga.Stop(); odlozenaPretraga.Start(); };
            odlozenaPretraga.Tick += (s, e) => { odlozenaPretraga.Stop(); Filtriraj(); };
            chipIspod = alati.Chip("Ispod minimuma", false, (s, e) => { samoIspod = !samoIspod; Filtriraj(); });
            chipKriticni = alati.Chip("Kritični", false, (s, e) => { samoKriticni = !samoKriticni; Filtriraj(); });
            chipNeaktivni = alati.Chip("I neaktivni", false, (s, e) => { iNeaktivni = !iNeaktivni; Filtriraj(); });
            alati.Dugme("Osveži", (s, e) => Ucitaj());
            alati.Dugme("Početno stanje", (s, e) => PocetnoStanje());
            alati.Dugme("Korekcija", (s, e) => NovaKorekcija());
            alati.Dugme("Excel", (s, e) => SERGrid.IzvozUExcel(grid, "Zalihe"));
            alati.DugmeDesno("+ Novi deo", (s, e) => SERStil.Otvori(this, new frmSERDeo(0)));

            var naslov = SERStil.NaslovnaTraka(this, "Magacin › Zalihe i šifarnik");
            info = naslov.DodajInfo("");
            var statusna = SERStil.StatusnaLinija(this);
            statusna.Text = "Dupli klik na red otvara deo · Raspoloživo = stanje − rezervisano na otvorenim nalozima · Stanje se menja samo dokumentom";

            Controls.Add(telo);
            telo.BringToFront();
            SERStil.PrimeniTemu(this);
            VisibleChanged += (s, e) => { if (Visible) Ucitaj(); };
        }

        private SERBrojKartica Kartica(Control roditelj, string opis, Color traka, Action klik)
        {
            var k = new SERBrojKartica { Opis = opis, Traka = traka, Width = 270, Margin = new Padding(0, 0, 14, 16), Cursor = klik == null ? Cursors.Default : Cursors.Hand };
            if (klik != null)
                k.Click += (s, e) => klik();
            roditelj.Controls.Add(k);
            return k;
        }

        private void Ucitaj()
        {
            try
            {
                podaci = SERBaza.Tabela(@"
                    SELECT ID, Sifra, Naziv, ISNULL(Grupa, N'') AS Grupa, JM, Stanje, Rezervisano, Raspolozivo, Naruceno, MinZaliha,
                           ProsecnaCena, Vrednost, ISNULL(Lokacija, N'') AS Lokacija, Kritican,
                           CASE WHEN Kritican = 1 THEN N'Da' ELSE N'' END AS KriticanTekst,
                           Aktivno, IspodMinimuma, StatusTekst
                    FROM dbo.vSERDeoStanje ORDER BY Sifra");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Zalihe nisu učitane. Proverite da li je na bazi pokrenuta skripta SER_MagacinDelova_2026_11.sql.\n\n" + ex.Message,
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                podaci = new DataTable();
                return;
            }
            decimal vrednost = 0;
            int ispod = 0, kriticni = 0, aktivnih = 0;
            foreach (DataRow r in podaci.Rows)
            {
                if (!Convert.ToBoolean(r["Aktivno"]))
                    continue;
                aktivnih++;
                vrednost += SERBaza.Decimalni(r["Vrednost"]);
                if (Convert.ToBoolean(r["IspodMinimuma"]))
                {
                    ispod++;
                    if (Convert.ToBoolean(r["Kritican"]))
                        kriticni++;
                }
            }
            kVrednost.Vrednost = SERBaza.Iznos(vrednost);
            kIspod.Broj = ispod;
            kKriticni.Broj = kriticni;
            info.Text = aktivnih + " aktivnih delova";
            Filtriraj();
        }

        private void Filtriraj()
        {
            if (podaci == null)
                return;
            chipIspod.Aktivan = kIspod.Izabrana = samoIspod;
            chipKriticni.Aktivan = kKriticni.Izabrana = samoKriticni;
            chipNeaktivni.Aktivan = iNeaktivni;
            kIspod.Invalidate();
            kKriticni.Invalidate();

            var uslovi = new List<string>();
            if (!iNeaktivni)
                uslovi.Add("Aktivno = true");
            if (samoIspod)
                uslovi.Add("IspodMinimuma = true");
            if (samoKriticni)
                uslovi.Add("Kritican = true");
            string t = pretraga.Text.Trim();
            if (t != "")
            {
                string e = t.Replace("'", "''").Replace("[", "[[]").Replace("*", "[*]").Replace("%", "[%]");
                uslovi.Add("(Sifra LIKE '%" + e + "%' OR Naziv LIKE '%" + e + "%' OR Lokacija LIKE '%" + e + "%' OR Grupa LIKE '%" + e + "%')");
            }
            var dv = new DataView(podaci) { RowFilter = string.Join(" AND ", uslovi) };
            SERGrid.Napuni(grid, dv.ToTable());
        }

        private Color? BojaCelije(Syncfusion.Grouping.Record r, string kolona)
        {
            if (!Convert.ToBoolean(r.GetValue("Aktivno")))
                return SavaTheme.Label;
            if (kolona == "KriticanTekst")
                return SavaTheme.Danger;
            if (kolona == "StatusTekst" || kolona == "Raspolozivo")
            {
                string status = Convert.ToString(r.GetValue("StatusTekst"));
                if (status == "Nema na stanju")
                    return SERStil.StatusIsteklo;
                if (status == "Ispod minimuma")
                    return SERStil.StatusIstice;
            }
            return null;
        }

        private void OtvoriIzabrani()
        {
            int id = SERGrid.IzabraniId(grid);
            if (id > 0)
                SERStil.Otvori(this, new frmSERDeo(id));
        }

        // Početno stanje (prelazak sa postojećeg magacina): samo za delove bez kretanja
        private void PocetnoStanje()
        {
            if (!SERPrava.Proveri(this, SERPrava.Magacioner))
                return;
            var polja = new List<SERPolje>
            {
                SERPolje.Lista("DeoID", "Deo", "SELECT ID, Sifra + N' – ' + Naziv AS Naziv FROM dbo.SERDeo d WHERE Aktivno = 1 AND NOT EXISTS (SELECT 1 FROM dbo.SERDeoKretanje k WHERE k.DeoID = d.ID) ORDER BY Sifra", true),
                SERPolje.Datum("Datum", "Datum", true),
                SERPolje.Kolicina("Kolicina", "Količina"),
                SERPolje.Decimalni("Cena", "Nabavna cena RSD", true)
            };
            polja[0].Raspon = 2;
            var pocetne = new SERVrednosti { ["Datum"] = DateTime.Today };
            bool uneto = SERDijalog.Unos(this, "Početno stanje dela", polja, pocetne, v =>
            {
                var dok = SERBaza.Izlaz("@DokumentID", SqlDbType.Int);
                dok.Direction = ParameterDirection.InputOutput;
                dok.Value = pocetnoStanjeDokument > 0 ? (object)pocetnoStanjeDokument : DBNull.Value;
                SERBaza.Procedura("dbo.SER_PocetnoStanje",
                    SERBaza.P("@DeoID", v.Broj("DeoID")), SERBaza.P("@Kolicina", v.Uzmi("Kolicina")), SERBaza.P("@Cena", v.Uzmi("Cena")),
                    SERBaza.P("@Datum", v.Uzmi("Datum")), SERBaza.P("@Korisnik", SERBaza.Korisnik), dok);
                pocetnoStanjeDokument = SERBaza.CeoBroj(dok.Value);
            }, 2, null, v =>
            {
                // cena 0 obara vrednost zaliha i prosečnu cenu, pa se traži potvrda (Ne = dijalog ostaje otvoren)
                if (SERBaza.Decimalni(v.Uzmi("Cena")) <= 0
                    && !SERDijalog.Potvrdi(this, "Cena je 0, vrednost zaliha neće biti tačna, a delovi će se izdavati po ceni 0. Nastaviti?", "Početno stanje"))
                    throw new SERPrekid();
                return null;
            });
            if (uneto)
                Ucitaj();
        }

        private void NovaKorekcija()
        {
            if (!SERPrava.Proveri(this, SERPrava.Korekcija))
                return;
            int izabran = SERGrid.IzabraniId(grid);
            if (frmSERKorekcije.Nova(this, izabran))
                MessageBox.Show(this, "Zahtev za korekciju je upisan. Zaliha se menja kad ga odobri šef tehničke službe.", Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
