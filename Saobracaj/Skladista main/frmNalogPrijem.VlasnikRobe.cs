using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main
{
    // RAZ-07: dugme „Novi vlasnik robe“ na traci (posle „Mesto Istovara“): unos naziva i PIB-a u VlasniciRobe
    // (frmVlasnikRobeNovi); novi vlasnik se odmah bira u listi Vlasnik robe i popunjava PIB, šifru i naziv
    // kao pri izboru iz liste (cboVlasnikRobe_Leave). Vlasnik se na nalog i magacinski predmet upisuje na Snimi.
    //
    // Komercijalni nalog (status NOVI i KOMERCIJALNINALOG) sada pokazuje Vlasnik robe i PIB (ranije sakriveni).
    // Procedure za snimanje komercijalnog naloga vlasnika ne upisuju (InsertRadniNalogDirektni ga nema), pa vlasnika
    // koga je korisnik izabrao upisuje SnimiIzabranogVlasnika. Lista sama bira prvi red pri punjenju; taj red se ne
    // snima, nego samo vlasnik koga je korisnik izabrao, a nalog bez vlasnika pokazuje prazno polje.
    public partial class frmNalogPrijem
    {
        int? vlasnikIzabran;   // vlasnik koga je korisnik izabrao iz liste ili uneo kao novog

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

            cboVlasnikRobe.SelectionChangeCommitted += (s, e) =>
            {
                vlasnikIzabran = cboVlasnikRobe.SelectedValue == null ? (int?)null : Convert.ToInt32(cboVlasnikRobe.SelectedValue);
                cboVlasnikRobe_Leave(cboVlasnikRobe, EventArgs.Empty);   // PIB, šifra i naziv odmah, ne tek po izlasku iz polja
            };
            Load += (s, e) => PrikaziVlasnikaKomercijalnog();   // posle frmNalogPrijem_Load (liste i podaci naloga)
        }

        // komercijalni nalog: status NOVI ili KOMERCIJALNINALOG (LO nalog ima magacinski predmet)
        private bool KomercijalniPrikaz()
        {
            string s = (Status ?? "").ToUpper();
            return s == "NOVI" || s == "KOMERCIJALNINALOG";
        }

        // komercijalni nalog: vlasnik sa naloga ili prazno polje (ne prvi vlasnik iz liste); PIB samo za čitanje
        private void PrikaziVlasnikaKomercijalnog()
        {
            if (!KomercijalniPrikaz())
                return;
            txtPIB.ReadOnly = true;

            int vlasnik = 0;
            int rn;
            if (int.TryParse(txtID.Text, out rn) && rn > 0)
            {
                try
                {
                    using (var conn = new SqlConnection(connection))
                    using (var cmd = new SqlCommand(@"SELECT rns.VlasnikRobe FROM RadniNalogSkladista rns
                                                      WHERE rns.ID = @ID AND rns.VlasnikRobe IN (SELECT id FROM VlasniciRobe)", conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", rn);
                        conn.Open();
                        object o = cmd.ExecuteScalar();
                        vlasnik = o == null || o == DBNull.Value ? 0 : Convert.ToInt32(o);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Vlasnik robe nije učitan: " + ex.Message);
                }
            }

            if (vlasnik > 0)
            {
                cboVlasnikRobe.SelectedValue = vlasnik;
                cboVlasnikRobe_Leave(cboVlasnikRobe, EventArgs.Empty);
            }
            else
            {
                cboVlasnikRobe.SelectedIndex = -1;
                txtPIB.Text = "";
            }
        }

        // Snimi: izabrani vlasnik na nalog (komercijalni nalog ga inače ne bi imao); bez izbora se ništa ne menja
        private void SnimiIzabranogVlasnika()
        {
            int rn;
            if (!vlasnikIzabran.HasValue || !int.TryParse(txtID.Text, out rn) || rn <= 0)
                return;
            try
            {
                using (var conn = new SqlConnection(connection))
                using (var cmd = new SqlCommand("UPDATE RadniNalogSkladista SET VlasnikRobe = @V WHERE ID = @ID AND ISNULL(VlasnikRobe, 0) <> @V", conn))
                {
                    cmd.Parameters.AddWithValue("@V", vlasnikIzabran.Value);
                    cmd.Parameters.AddWithValue("@ID", rn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vlasnik robe nije upisan na nalog: " + ex.Message);
            }
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
                vlasnikIzabran = vlasnik;
                cboVlasnikRobe_Leave(cboVlasnikRobe, EventArgs.Empty);   // PIB, šifra i naziv vlasnika
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lista vlasnika nije osvežena: " + ex.Message);
            }
        }
    }
}
