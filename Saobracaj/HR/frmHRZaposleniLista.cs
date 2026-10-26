using Saobracaj.Theme;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.HR
{
    // Lista zaposlenih (izgled formi, strana 3)
    public class frmHRZaposleniLista : Form
    {
        private readonly HRNaslovnaTraka naslov;
        private readonly HRTekst info;
        private readonly TextBox pretraga;
        private readonly HRChip chipStatus, chipJedinica, chipUpozorenje;
        private readonly GridGroupingControl grid;
        private readonly Timer odlozenaPretraga = new Timer { Interval = 300 };
        private DataTable podaci;
        private string status = "A";          // A, P ili "" (svi)
        private int jedinica;                  // 0 = sve
        private string jedinicaNaziv = "sve";
        private bool samoUpozorenja;

        public frmHRZaposleniLista()
        {
            HRStil.PripremiFormu(this, "Zaposleni");

            var telo = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 16, 20, 0), BackColor = SavaTheme.Surface };
            var okvir = HRStil.BeliPanel();
            okvir.Dock = DockStyle.Fill;
            grid = HRGrid.Novi();
            okvir.Controls.Add(grid);
            telo.Controls.Add(okvir);
            HRGrid.Kolone(grid,
                new HRKolona("Code", "Code", 110),
                new HRKolona("PrezimeIme", "Prezime i ime", 220),
                new HRKolona("RadnoMestoNaziv", "Radno mesto", 200),
                new HRKolona("OrganizacionaJedinicaNaziv", "Org. jedinica", 200),
                new HRKolona("StatusNaziv", "Status", 100),
                new HRKolona("Upozorenja", "Upozorenja", 260));
            HRGrid.BojaTeksta(grid, BojaCelije);
            HRGrid.DuplikKlik(grid, OtvoriIzabranog);

            var alati = HRStil.AlatnaTraka(this);
            pretraga = alati.Pretraga("Pretraga: ime, Code, JMBG…", 300);
            pretraga.TextChanged += (s, e) => { odlozenaPretraga.Stop(); odlozenaPretraga.Start(); };
            odlozenaPretraga.Tick += (s, e) => { odlozenaPretraga.Stop(); Filtriraj(); };
            chipStatus = alati.Chip("Status: Aktivni", true, (s, e) => PromeniStatus());
            chipJedinica = alati.Chip("Org. jedinica: sve", false, (s, e) => IzaberiJedinicu());
            chipUpozorenje = alati.Chip("Sa upozorenjem", false, (s, e) => { samoUpozorenja = !samoUpozorenja; Filtriraj(); });
            alati.DugmeDesno("+ Novi zaposleni", (s, e) => HRStil.Otvori(this, new frmHRZaposleni(0)));
            alati.Dugme("Osveži", (s, e) => Ucitaj());

            naslov = HRStil.NaslovnaTraka(this, "Zaposleni › Lista");
            info = naslov.DodajInfo("");
            var statusna = HRStil.StatusnaLinija(this);
            statusna.Text = "Dupli klik na red otvara formu zaposlenog · Kolona Upozorenja sabira rokove iz dokumenata, pregleda, ugovora i HTZ";

            Controls.Add(telo);
            telo.BringToFront();

            HRStil.PrimeniTemu(this);

            // prvo otvaranje i povratak sa forme zaposlenog (glavni prozor ponovo prikazuje ovu formu)
            VisibleChanged += (s, e) => { if (Visible) Ucitaj(); };
        }

        private void Ucitaj()
        {
            try
            {
                podaci = HRBaza.Tabela(@"
                    SELECT t.Zaposleni AS ID, t.Code, t.PrezimeIme, t.RadnoMestoNaziv, t.OrganizacionaJedinica, t.OrganizacionaJedinicaNaziv,
                           t.Status, t.StatusNaziv, ISNULL(t.JMBG, N'') AS JMBG, ISNULL(t.SifraERP, N'') AS SifraERP,
                           ISNULL(u.BrojUpozorenja, 0) AS BrojUpozorenja, ISNULL(u.NajmanjeDana, 9999) AS NajmanjeDana,
                           CASE WHEN t.Status = 'P' THEN ISNULL(N'Izašao ' + FORMAT(t.DatumIzlaska, 'dd.MM.yyyy.'), N'Pasivan')
                                WHEN u.Opis IS NULL THEN N'Nema'
                                WHEN u.BrojUpozorenja > 1 THEN u.Opis + N' (+' + CAST(u.BrojUpozorenja - 1 AS nvarchar(10)) + N')'
                                ELSE u.Opis END AS Upozorenja
                    FROM dbo.vZaposleniTrenutno t
                    LEFT JOIN dbo.vZaposleniUpozorenja u ON u.Zaposleni = t.Zaposleni
                    ORDER BY t.PrezimeIme");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lista zaposlenih nije učitana. Proverite da li je na bazi pokrenuta skripta HR_Modul_2026_10.sql.\n\n" + ex.Message,
                    "Zaposleni", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                podaci = new DataTable();
                return;
            }
            int aktivnih = 0;
            foreach (DataRow r in podaci.Rows)
                if (Convert.ToString(r["Status"]) == "A")
                    aktivnih++;
            info.Text = podaci.Rows.Count + " " + Zaposlenih(podaci.Rows.Count) + " · " + aktivnih + " aktivnih";
            Filtriraj();
        }

        private static string Zaposlenih(int n)
        {
            int d = n % 10, s = n % 100;
            if (d == 1 && s != 11) return "zaposleni";
            if (d >= 2 && d <= 4 && (s < 12 || s > 14)) return "zaposlena";
            return "zaposlenih";
        }

        private void Filtriraj()
        {
            if (podaci == null)
                return;
            chipStatus.Text = "Status: " + (status == "A" ? "Aktivni" : status == "P" ? "Pasivni" : "svi");
            chipStatus.Aktivan = status != "";
            chipJedinica.Text = "Org. jedinica: " + jedinicaNaziv;
            chipJedinica.Aktivan = jedinica != 0;
            chipUpozorenje.Aktivan = samoUpozorenja;

            var uslovi = new System.Collections.Generic.List<string>();
            if (status != "")
                uslovi.Add("Status = '" + status + "'");
            if (jedinica != 0)
                uslovi.Add("OrganizacionaJedinica = " + jedinica);
            if (samoUpozorenja)
                uslovi.Add("BrojUpozorenja > 0 AND Status = 'A'");
            string t = pretraga.Text.Trim();
            if (t != "")
            {
                string e = t.Replace("'", "''").Replace("[", "[[]").Replace("*", "[*]").Replace("%", "[%]");
                uslovi.Add("(Code LIKE '%" + e + "%' OR PrezimeIme LIKE '%" + e + "%' OR JMBG LIKE '%" + e + "%' OR SifraERP LIKE '%" + e + "%')");
            }
            var dv = new DataView(podaci) { RowFilter = string.Join(" AND ", uslovi) };
            HRGrid.Napuni(grid, dv.ToTable());
        }

        private void PromeniStatus()
        {
            status = status == "A" ? "P" : status == "P" ? "" : "A";
            Filtriraj();
        }

        private void IzaberiJedinicu()
        {
            var meni = new ContextMenuStrip { Font = SavaTheme.FieldFont };
            meni.Items.Add("sve", null, (s, e) => { jedinica = 0; jedinicaNaziv = "sve"; Filtriraj(); });
            try
            {
                foreach (DataRow r in HRBaza.Tabela(HRListe.OrganizacioneJedinice).Rows)
                {
                    int id = HRBaza.CeoBroj(r["ID"]);
                    string naziv = Convert.ToString(r["Naziv"]).Trim();
                    meni.Items.Add(naziv, null, (s, e) => { jedinica = id; jedinicaNaziv = naziv; Filtriraj(); });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Org. jedinica", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            meni.Show(chipJedinica, new Point(0, chipJedinica.Height));
        }

        private Color? BojaCelije(Syncfusion.Grouping.Record r, string kolona)
        {
            bool pasivan = Convert.ToString(r.GetValue("Status")) == "P";
            if (kolona == "StatusNaziv")
                return pasivan ? SavaTheme.Label : (Color?)null;
            if (kolona != "Upozorenja")
                return null;
            if (pasivan)
                return SavaTheme.Label;
            if (HRBaza.CeoBroj(r.GetValue("BrojUpozorenja")) == 0)
                return HRStil.StatusVazi;
            return HRBaza.CeoBroj(r.GetValue("NajmanjeDana")) < 0 ? HRStil.StatusIsteklo : HRStil.StatusIstice;
        }

        private void OtvoriIzabranog()
        {
            int id = HRGrid.IzabraniId(grid, "ID");
            if (id > 0)
                HRStil.Otvori(this, new frmHRZaposleni(id));
        }
    }
}
