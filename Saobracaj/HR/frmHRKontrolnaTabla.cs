using Saobracaj.Theme;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.HR
{
    // Kontrolna tabla rokova (izgled formi, strana 7): dokumenti, zdravstveni pregledi, HTZ i ugovori na određeno
    // koji ističu u narednih 30 dana ili su već istekli. Klik na karticu filtrira tabelu po vrsti.
    public class frmHRKontrolnaTabla : Form
    {
        private const int Dana = 30;
        private readonly HRBrojKartica kDokumenti, kPregledi, kHTZ, kUgovori;
        private readonly HRChip chipVrste, chipJedinica;
        private readonly GridGroupingControl grid;
        private DataTable podaci;
        private string vrsta = "";         // "" = sve vrste
        private bool mojaJedinica;

        public frmHRKontrolnaTabla()
        {
            HRStil.PripremiFormu(this, "Kontrolna tabla rokova");

            var kartice = new TableLayoutPanel { Dock = DockStyle.Top, Height = 126, ColumnCount = 4, RowCount = 1, BackColor = Color.Transparent };
            for (int i = 0; i < 4; i++)
                kartice.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            kDokumenti = Kartica(kartice, 0, ColorTranslator.FromHtml("#E07B1A"), "dokumenata ističe u 30 dana ili je isteklo", "Dokument");
            kPregledi = Kartica(kartice, 1, SavaTheme.Danger, "zdravstvenih pregleda kasni", "Pregled");
            kHTZ = Kartica(kartice, 2, ColorTranslator.FromHtml("#E07B1A"), "HTZ stavki za zamenu", "HTZ");
            kUgovori = Kartica(kartice, 3, SavaTheme.Primary, "ugovora na određeno ističe", "Ugovor");

            var sekcija = HRStil.BeliPanel();
            sekcija.Dock = DockStyle.Fill;
            sekcija.Padding = new Padding(16, 10, 16, 16);
            var vrh = new Panel { Dock = DockStyle.Top, Height = 46, BackColor = Color.Transparent };
            var komande = new FlowLayoutPanel { Dock = DockStyle.Right, FlowDirection = FlowDirection.RightToLeft, WrapContents = false, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, BackColor = Color.Transparent, Padding = new Padding(0, 4, 0, 0) };
            var izvoz = HRStil.Dugme("Izvoz u Excel");
            izvoz.Margin = new Padding(8, 0, 0, 0);
            izvoz.Click += (s, e) => HRGrid.IzvozUExcel(grid, "Rokovi_" + DateTime.Today.ToString("yyyyMMdd"));
            chipJedinica = new HRChip { Text = "Moja jedinica", Margin = new Padding(8, 0, 0, 0) };
            chipJedinica.Click += (s, e) => { mojaJedinica = !mojaJedinica; Ucitaj(); };
            chipVrste = new HRChip { Text = "Sve vrste", Aktivan = true, Margin = new Padding(8, 0, 0, 0) };
            chipVrste.Click += (s, e) => { vrsta = ""; Prikazi(); };
            komande.Controls.Add(izvoz);
            komande.Controls.Add(chipJedinica);
            komande.Controls.Add(chipVrste);
            vrh.Controls.Add(new HRTekst { Text = "Narednih " + Dana + " dana", Dock = DockStyle.Fill, Font = SavaTheme.FormTitleFont, ForeColor = SavaTheme.Text });
            vrh.Controls.Add(komande);

            grid = HRGrid.Novi();
            HRGrid.Kolone(grid,
                new HRKolona("ZaposleniNaziv", "Zaposleni", 220),
                new HRKolona("OrgJedinica", "Org. jedinica", 200),
                new HRKolona("StaIstice", "Šta ističe", 260),
                new HRKolona("Datum", "Datum", 110, "datum"),
                new HRKolona("Preostalo", "Preostalo", 150));
            HRGrid.BojaTeksta(grid, (r, k) =>
            {
                if (k != "Preostalo")
                    return null;
                int d = HRBaza.CeoBroj(r.GetValue("DanaDo"));
                return d < 0 ? HRStil.StatusIsteklo : d <= 7 ? HRStil.StatusIstice : (Color?)null;
            });
            HRGrid.DuplikKlik(grid, () =>
            {
                int z = HRGrid.IzabraniId(grid, "Zaposleni");
                if (z > 0)
                    HRStil.Otvori(this, new frmHRZaposleni(z));
            });
            var okvirGrida = new Panel { Dock = DockStyle.Fill, BackColor = SavaTheme.White };
            okvirGrida.Paint += (s, e) => ControlPaint.DrawBorder(e.Graphics, okvirGrida.ClientRectangle, SavaTheme.Line, ButtonBorderStyle.Solid);
            okvirGrida.Padding = new Padding(1);
            okvirGrida.Controls.Add(grid);
            sekcija.Controls.Add(okvirGrida);
            sekcija.Controls.Add(vrh);

            var telo = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 16, 20, 16), BackColor = SavaTheme.Surface };
            telo.Controls.Add(sekcija);
            telo.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 16 });
            telo.Controls.Add(kartice);

            Controls.Add(telo);
            HRStil.NaslovnaTraka(this, "Zaposleni › Kontrolna tabla rokova").DodajDugme("Osveži", false, (s, e) => Ucitaj());
            HRStil.StatusnaLinija(this).Text = "Dupli klik na red otvara formu zaposlenog · Upozorenje za dokument počinje onoliko dana pre isteka koliko je zadato u vrsti dokumenta";
            HRStil.PrimeniTemu(this);

            // prvo otvaranje i povratak sa forme zaposlenog
            VisibleChanged += (s, e) => { if (Visible) Ucitaj(); };
        }

        private HRBrojKartica Kartica(TableLayoutPanel t, int kolona, Color boja, string opis, string filter)
        {
            var k = new HRBrojKartica { Traka = boja, Opis = opis, Dock = DockStyle.Fill, Margin = new Padding(kolona == 0 ? 0 : 8, 0, kolona == 3 ? 0 : 8, 0) };
            k.Click += (s, e) => { vrsta = vrsta == filter ? "" : filter; Prikazi(); };
            t.Controls.Add(k, kolona, 0);
            return k;
        }

        private void Ucitaj()
        {
            object jedinica = DBNull.Value;
            if (mojaJedinica)
            {
                int ja = HRPrava.TrenutniZaposleni();
                jedinica = ja > 0 ? HRBaza.Skalar("SELECT OrganizacionaJedinica FROM dbo.vZaposleniTrenutno WHERE Zaposleni = @Z", HRBaza.P("@Z", ja)) : null;
                if (jedinica == null)
                {
                    MessageBox.Show("Prijavljeni korisnik nije povezan sa zaposlenim ili zaposleni nema organizacionu jedinicu.\n" +
                                    "Vezu upišite na formi zaposlenog, u polje Korisničko ime u aplikaciji.",
                        "Moja jedinica", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mojaJedinica = false;
                    jedinica = DBNull.Value;
                }
            }
            chipJedinica.Aktivan = mojaJedinica;
            try
            {
                podaci = HRBaza.ProceduraTabela("dbo.HR_KontrolnaTablaRokova", HRBaza.P("@Dana", Dana), HRBaza.P("@OrganizacionaJedinica", jedinica));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Rokovi nisu učitani. Proverite da li je na bazi pokrenuta skripta HR_Modul_2026_10.sql.\n\n" + ex.Message,
                    "Kontrolna tabla rokova", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                podaci = new DataTable();
                return;
            }
            podaci.Columns.Add("Preostalo", typeof(string));
            foreach (DataRow r in podaci.Rows)
            {
                int d = HRBaza.CeoBroj(r["DanaDo"]);
                if (Convert.ToString(r["Vrsta"]) == "Nedostaje")
                    r["Preostalo"] = "Nedostaje";
                else
                    r["Preostalo"] = d < 0 ? "Isteklo (" + (-d) + " " + HRStil.Dana(-d) + ")" : d == 0 ? "Danas" : d + " " + HRStil.Dana(d);
            }

            var redovi = podaci.Rows.Cast<DataRow>();
            kDokumenti.Broj = redovi.Count(r => Convert.ToString(r["Vrsta"]) == "Dokument");
            kPregledi.Broj = redovi.Count(r => Convert.ToString(r["Vrsta"]) == "Pregled" && HRBaza.CeoBroj(r["DanaDo"]) < 0);
            kHTZ.Broj = redovi.Count(r => Convert.ToString(r["Vrsta"]) == "HTZ");
            kUgovori.Broj = redovi.Count(r => Convert.ToString(r["Vrsta"]) == "Ugovor");
            Prikazi();
        }

        private void Prikazi()
        {
            if (podaci == null)
                return;
            chipVrste.Aktivan = vrsta == "";
            foreach (var k in new[] { kDokumenti, kPregledi, kHTZ, kUgovori })
            {
                k.Izabrana = false;
                k.Invalidate();
            }
            HRBrojKartica izabrana = vrsta == "Dokument" ? kDokumenti : vrsta == "Pregled" ? kPregledi : vrsta == "HTZ" ? kHTZ : vrsta == "Ugovor" ? kUgovori : null;
            if (izabrana != null)
                izabrana.Izabrana = true;
            var dv = new DataView(podaci) { RowFilter = vrsta == "" ? "" : "Vrsta = '" + vrsta + "'" };
            HRGrid.Napuni(grid, dv.ToTable());
        }
    }
}
