using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main
{
    // RAZ-07: dugme „Novi vlasnik robe“ na traci (posle „Mesto Istovara“): unos naziva i PIB-a u VlasniciRobe
    // (frmVlasnikRobeNovi); novi vlasnik se odmah bira u listi Vlasnik robe i popunjava PIB, šifru i naziv
    // kao pri izboru iz liste (cboVlasnikRobe_Leave). Vlasnik se na nalog i magacinski predmet upisuje na Snimi.
    public partial class frmNalogPrijem
    {
        // poziva se iz InitMagacinskiPredmet
        private void InitNoviVlasnik()
        {
            var btn = new Button
            {
                Text = "Novi vlasnik robe",
                AutoSize = true,
                Dock = DockStyle.Left,
                Font = button2.Font,
                ForeColor = button2.ForeColor,
                FlatStyle = button2.FlatStyle,
                Margin = button2.Margin,
                UseVisualStyleBackColor = true
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseDownBackColor = button2.FlatAppearance.MouseDownBackColor;
            btn.FlatAppearance.MouseOverBackColor = button2.FlatAppearance.MouseOverBackColor;
            new ToolTip().SetToolTip(btn, "Unos novog vlasnika robe (naziv i PIB) i izbor na ovom nalogu");
            btn.Click += (s, e) => UnesiNovogVlasnika();

            // dokuje se posle „Mesto Istovara“ (manji indeks = dokuje se kasnije, desno)
            var traka = button2.Parent;
            traka.Controls.Add(btn);
            traka.Controls.SetChildIndex(btn, traka.Controls.GetChildIndex(button2));
        }

        private void UnesiNovogVlasnika()
        {
            using (var f = new frmVlasnikRobeNovi())
            {
                if (f.ShowDialog(this) != DialogResult.OK)
                    return;
                OsveziVlasnike(f.VlasnikID);
            }
        }

        // lista vlasnika iznova (isti upit kao pri otvaranju forme), pa izbor novog
        private void OsveziVlasnike(int vlasnik)
        {
            try
            {
                var dt = new DataTable();
                using (var da = new SqlDataAdapter("SELECT id, Naziv FROM VlasniciRobe ORDER BY id ASC", connection))
                    da.Fill(dt);
                cboVlasnikRobe.DataSource = dt;
                cboVlasnikRobe.DisplayMember = "Naziv";
                cboVlasnikRobe.ValueMember = "id";
                cboVlasnikRobe.SelectedValue = vlasnik;
                cboVlasnikRobe_Leave(cboVlasnikRobe, EventArgs.Empty);   // PIB, šifra i naziv vlasnika
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lista vlasnika nije osvežena: " + ex.Message);
            }
        }
    }
}
