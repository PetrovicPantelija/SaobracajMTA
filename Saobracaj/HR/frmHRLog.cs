using Saobracaj.Theme;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Saobracaj.HR
{
    // Pregled loga HR podataka (isti obrazac kao Pregled log u Terminalu privremenom, uz korisnika i brisanje)
    public class frmHRLog : Form
    {
        private readonly GridGroupingControl grid;
        private readonly ComboBox cboTabela, cboPeriod;
        private readonly TextBox pretraga;

        public frmHRLog()
        {
            HRStil.PripremiFormu(this, "HR log");
            var telo = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 12, 20, 0), BackColor = SavaTheme.Surface };
            var okvir = HRStil.BeliPanel();
            okvir.Dock = DockStyle.Fill;
            grid = HRGrid.Novi();
            okvir.Controls.Add(grid);
            telo.Controls.Add(okvir);
            HRGrid.Kolone(grid,
                new HRKolona("Datum", "Datum", 140, "datumvreme"),
                new HRKolona("Tabela", "Tabela", 150),
                new HRKolona("ZapisID", "Zapis", 70, "broj"),
                new HRKolona("Akcija", "Akcija", 100),
                new HRKolona("Opis", "Promena", 560),
                new HRKolona("Korisnik", "Korisnik", 110),
                new HRKolona("Racunar", "Računar", 130));

            var alati = HRStil.AlatnaTraka(this);
            pretraga = alati.Pretraga("Pretraga u opisu…", 220);
            cboTabela = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 180, Margin = new Padding(0, 2, 8, 0) };
            cboTabela.Items.AddRange(new object[] { "Sve tabele", "EvidencijaRada", "EvidencijaMesec", "GODopust", "GODopustStavke", "ZaposleniOsnovGO", "BeleskaORadu", "Zaposleni", "Podesavanja" });
            cboTabela.SelectedIndex = 0;
            cboPeriod = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 140, Margin = new Padding(0, 2, 8, 0) };
            cboPeriod.Items.AddRange(new object[] { "Poslednjih 7 dana", "Poslednjih 30 dana", "Poslednjih 365 dana", "Sve" });
            cboPeriod.SelectedIndex = 1;
            alati.Levo.Controls.Add(cboTabela);
            alati.Levo.Controls.Add(cboPeriod);
            alati.DugmeDesno("Prikaži", (s, e) => Ucitaj());
            cboTabela.SelectedIndexChanged += (s, e) => Ucitaj();
            cboPeriod.SelectedIndexChanged += (s, e) => Ucitaj();
            pretraga.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) Ucitaj(); };

            HRStil.NaslovnaTraka(this, "HR modul › Log");
            HRStil.StatusnaLinija(this).Text = "Loguju se evidencija rada (ručni unos i GO), godišnji odmori, osnovi, beleške o radu, promena nadređenog, status meseca i podešavanja (lozinke bez vrednosti).";
            Controls.Add(telo);
            telo.BringToFront();
            HRStil.PrimeniTemu(this);
            VisibleChanged += (s, e) => { if (Visible) Ucitaj(); };
        }

        private void Ucitaj()
        {
            int[] dani = { 7, 30, 365, 0 };
            int d = dani[Math.Max(0, cboPeriod.SelectedIndex)];
            var uslovi = new List<string> { "1 = 1" };
            var p = new List<System.Data.SqlClient.SqlParameter>();
            if (cboTabela.SelectedIndex > 0)
            {
                uslovi.Add("Tabela = @T");
                p.Add(HRBaza.P("@T", cboTabela.SelectedItem.ToString()));
            }
            if (d > 0)
            {
                uslovi.Add("Datum >= DATEADD(DAY, -@D, GETDATE())");
                p.Add(HRBaza.P("@D", d));
            }
            if (pretraga.Text.Trim() != "")
            {
                uslovi.Add("(Opis LIKE @S OR Korisnik LIKE @S)");
                p.Add(HRBaza.P("@S", "%" + pretraga.Text.Trim() + "%"));
            }
            try
            {
                HRGrid.Napuni(grid, HRBaza.Tabela("SELECT TOP (5000) LogID, Datum, Tabela, ZapisID, Akcija, Opis, Korisnik, Racunar FROM dbo.HRLog WHERE "
                                                  + string.Join(" AND ", uslovi) + " ORDER BY LogID DESC", p.ToArray()));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Log nije učitan. Proverite da li je na bazi pokrenuta skripta HR_Modul_Faza2_2026_10.sql.\n\n" + ex.Message,
                    "HR log", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
