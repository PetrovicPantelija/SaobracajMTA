using Saobracaj.HR;
using Saobracaj.Theme;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.Administracija
{
    // Administracija > Podešavanja: tabela Podesavanja grupisana po grupi; lozinke se prikazuju kao zvezdice.
    // Forma se otvara preko kartice 'Podešavanja' (posebno pravo).
    public class frmPodesavanja : Form
    {
        private readonly GridGroupingControl grid;

        public frmPodesavanja()
        {
            HRStil.PripremiFormu(this, "Podešavanja");

            var telo = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 16, 20, 0), BackColor = SavaTheme.Surface };
            var okvir = HRStil.BeliPanel();
            okvir.Dock = DockStyle.Fill;
            grid = HRGrid.Novi();
            okvir.Controls.Add(grid);
            telo.Controls.Add(okvir);
            HRGrid.Kolone(grid,
                new HRKolona("Kljuc", "Ključ", 240),
                new HRKolona("Prikaz", "Vrednost", 420),
                new HRKolona("TipVrednosti", "Tip", 90),
                new HRKolona("Opis", "Opis", 420),
                new HRKolona("DatumIzmene", "Izmenjeno", 130, "datumvreme"),
                new HRKolona("NameOper", "Izmenio", 100));
            HRGrid.BojaTeksta(grid, (r, k) => k == "Prikaz" && Convert.ToString(r.GetValue("Prikaz")) == "" ? HRStil.StatusIsteklo : (Color?)null);
            HRGrid.DuplikKlik(grid, Izmeni);

            var alati = HRStil.AlatnaTraka(this);
            alati.Dugme("Izmeni", (s, e) => Izmeni(), "primary");
            alati.Dugme("Osveži", (s, e) => Ucitaj());
            alati.Dugme("Probni mail", (s, e) => ProbniMail());

            HRStil.NaslovnaTraka(this, "Administracija › Podešavanja");
            HRStil.StatusnaLinija(this).Text = "Prazna vrednost je označena crveno; program tada javlja „Nedostaje podešavanje …“. Lozinke se čuvaju šifrovane. Dupli klik menja vrednost.";
            Controls.Add(telo);
            telo.BringToFront();
            HRStil.PrimeniTemu(this);
            VisibleChanged += (s, e) => { if (Visible) Ucitaj(); };
        }

        private void Ucitaj()
        {
            DataTable dt;
            try
            {
                Podesavanja.Reload();
                dt = HRBaza.Tabela("SELECT ID, Grupa, Kljuc, Vrednost, TipVrednosti, Opis, DatumIzmene, NameOper FROM dbo.Podesavanja ORDER BY Grupa, Kljuc");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Podešavanja nisu učitana. Proverite da li je na bazi pokrenuta skripta HR_Modul_Faza2_2026_10.sql.\n\n" + ex.Message,
                    "Podešavanja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dt.Columns.Add("Prikaz", typeof(string));
            foreach (DataRow r in dt.Rows)
            {
                string v = Convert.ToString(r["Vrednost"]);
                bool lozinka = Convert.ToString(r["TipVrednosti"]).Trim().ToUpperInvariant() == "PASSWORD";
                r["Prikaz"] = lozinka && v != "" ? "********" : v.Replace("\r", " ").Replace("\n", " ");
            }
            HRGrid.Napuni(grid, dt);
            grid.TableDescriptor.GroupedColumns.Clear();
            grid.TableDescriptor.GroupedColumns.Add("Grupa");
            grid.Table.ExpandAllGroups();
        }

        private void Izmeni()
        {
            string kljuc = Convert.ToString(HRGrid.Vrednost(grid, "Kljuc"));
            if (kljuc == "")
                return;
            Podesavanja.Stavka s = Podesavanja.Sve.FirstOrDefault(x => x.Kljuc == kljuc);
            if (s == null)
                return;

            HRPolje polje;
            switch (s.Tip)
            {
                case "BOOL":
                    polje = HRPolje.DaNe("Vrednost", "Uključeno");
                    break;
                case "PASSWORD":
                    polje = HRPolje.Tekst("Vrednost", "Lozinka (prazno = bez promene)", 200);
                    break;
                default:
                    bool dugo = (s.Vrednost ?? "").Length > 80 || kljuc.StartsWith("HR.Resenje");
                    polje = new HRPolje("Vrednost", "Vrednost", dugo ? HRTip.VelikiTekst : HRTip.Tekst);
                    break;
            }
            object pocetno = s.Tip == "PASSWORD" ? null
                           : s.Tip == "BOOL" ? (object)(s.Vrednost == "1" || string.Equals(s.Vrednost, "true", StringComparison.OrdinalIgnoreCase))
                           : s.Vrednost;
            using (var dlg = new frmHRStavka(kljuc, new[] { polje }, new HRVrednosti { ["Vrednost"] = pocetno }, 1))
            {
                if (s.Tip == "PASSWORD" && dlg.Kontrola("Vrednost") is TextBox tb)
                    tb.UseSystemPasswordChar = true;
                if (polje.Tip == HRTip.VelikiTekst && dlg.Kontrola("Vrednost") is TextBox veliki)
                {
                    veliki.Height = kljuc == "HR.ResenjeSablon" ? 420 : 140;
                    veliki.Parent.Height = veliki.Height + 26;
                    dlg.ClientSize = new Size(dlg.ClientSize.Width + 260, veliki.Height + 160);
                }
                dlg.Validacija = v => Proveri(s, v.Uzmi("Vrednost"));
                dlg.Snimanje = v =>
                {
                    object x = v.Uzmi("Vrednost");
                    if (s.Tip == "PASSWORD" && x == null)
                        return null;   // bez promene
                    string upis = s.Tip == "BOOL" ? (Convert.ToBoolean(x ?? false) ? "1" : "0") : (x == null ? null : Convert.ToString(x));
                    Podesavanja.Set(kljuc, upis);
                    return null;
                };
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    Podesavanja.Primeni();
                    Ucitaj();
                }
            }
        }

        private static string Proveri(Podesavanja.Stavka s, object vrednost)
        {
            string v = vrednost == null ? "" : Convert.ToString(vrednost).Trim();
            if (v == "")
                return null;
            switch (s.Tip)
            {
                case "INT":
                    return int.TryParse(v, out _) ? null : "Unesite ceo broj.";
                case "DECIMAL":
                    return HRObrazac.ParsirajDecimalni(v, out _) ? null : "Unesite broj.";
                case "DATE":
                    return DateTime.TryParse(v, out _) ? null : "Unesite datum (dd.MM.yyyy).";
            }
            if (s.Kljuc == "HR.GOZaokruzivanje" && v != "NAVISE" && v != "MATEMATICKI")
                return "Dozvoljene vrednosti su NAVISE i MATEMATICKI.";
            return null;
        }

        private void ProbniMail()
        {
            string adresa = HRPrilozi.Unos(this, "Probni mail", "Adresa primaoca", Podesavanja.Get("Mail.Posiljalac", ""), 100, false);
            if (string.IsNullOrWhiteSpace(adresa))
                return;
            try
            {
                HRMail.Posalji(adresa, "Probni mail iz aplikacije", "Ovo je probna poruka. Podešavanja za slanje maila su ispravna.", null);
                MessageBox.Show("Mail je poslat na " + adresa + ".", "Probni mail", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mail nije poslat: " + ex.Message, "Probni mail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
