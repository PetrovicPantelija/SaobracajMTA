using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main
{
    // RAZ-07: unos novog vlasnika robe (VlasniciRobe: naziv i PIB) sa naloga za prijem.
    // Procedura sp_VlasnikRobe_Novi (Carina_VlasnikRobeUnos_2026_11.sql): isti PIB ne pravi duplikat, vraća postojećeg.
    // Posle OK, VlasnikID je izabrani vlasnik (nov ili postojeći).
    public class frmVlasnikRobeNovi : Form
    {
        readonly string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

        TextBox txtNaziv, txtPIB;

        public int VlasnikID { get; private set; }

        static readonly Color Plava = Color.FromArgb(0x0A, 0x66, 0xB8);
        static readonly Color Oznaka = Color.FromArgb(0x4C, 0x5D, 0x6E);

        public frmVlasnikRobeNovi()
        {
            Text = "Novi vlasnik robe";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowIcon = false;
            ShowInTaskbar = false;
            Font = new Font("Segoe UI", 9f);
            BackColor = Color.White;
            ClientSize = new Size(420, 196);

            Controls.Add(new Label { Text = "Naziv", AutoSize = true, ForeColor = Oznaka, Location = new Point(20, 20) });
            txtNaziv = new TextBox { Location = new Point(20, 38), Width = 380, MaxLength = 200, Font = new Font("Segoe UI", 10f) };
            Controls.Add(txtNaziv);

            Controls.Add(new Label { Text = "PIB", AutoSize = true, ForeColor = Oznaka, Location = new Point(20, 76) });
            txtPIB = new TextBox { Location = new Point(20, 94), Width = 160, MaxLength = 20, Font = new Font("Segoe UI", 10f) };
            Controls.Add(txtPIB);
            new ToolTip().SetToolTip(txtPIB, "PIB primaoca iz C7 (polje 8), npr. 102135221");

            var btnSacuvaj = new Button
            {
                Text = "Sačuvaj",
                Size = new Size(110, 32),
                Location = new Point(174, 146),
                BackColor = Plava,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Tag = "primary"
            };
            btnSacuvaj.FlatAppearance.BorderSize = 0;
            btnSacuvaj.Click += (s, e) => Sacuvaj();
            var btnOdustani = new Button { Text = "Odustani", Size = new Size(110, 32), Location = new Point(290, 146), UseVisualStyleBackColor = true, DialogResult = DialogResult.Cancel };
            Controls.Add(btnSacuvaj);
            Controls.Add(btnOdustani);
            AcceptButton = btnSacuvaj;
            CancelButton = btnOdustani;
        }

        private void Sacuvaj()
        {
            string naziv = txtNaziv.Text.Trim();
            string pib = txtPIB.Text.Trim();
            if (naziv == "")
            {
                MessageBox.Show("Unesite naziv vlasnika robe.", Text);
                txtNaziv.Focus();
                return;
            }
            if (pib == "")
            {
                MessageBox.Show("Unesite PIB vlasnika robe.", Text);
                txtPIB.Focus();
                return;
            }
            if ((pib.Length != 9 || !pib.All(char.IsDigit)) &&
                MessageBox.Show("PIB pravnog lica u Srbiji ima 9 cifara, a uneto je „" + pib + "“. Sačuvati ipak?", Text,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                txtPIB.Focus();
                return;
            }

            try
            {
                using (var conn = new SqlConnection(connection))
                using (var cmd = new SqlCommand("dbo.sp_VlasnikRobe_Novi", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@Naziv", naziv);
                    cmd.Parameters.AddWithValue("@PIB", pib);
                    var pID = cmd.Parameters.Add("@ID", SqlDbType.Int);
                    pID.Direction = ParameterDirection.Output;
                    var pPostojao = cmd.Parameters.Add("@Postojao", SqlDbType.Bit);
                    pPostojao.Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    VlasnikID = Convert.ToInt32(pID.Value);
                    if (Convert.ToBoolean(pPostojao.Value))
                    {
                        using (var c = new SqlCommand("SELECT RTRIM(CONVERT(nvarchar(200), Naziv)) FROM dbo.VlasniciRobe WHERE id = @id", conn))
                        {
                            c.Parameters.AddWithValue("@id", VlasnikID);
                            MessageBox.Show("Vlasnik sa PIB-om " + pib + " već postoji: " + Convert.ToString(c.ExecuteScalar()) +
                                ".\nNa nalogu je izabran postojeći vlasnik.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                DialogResult = DialogResult.OK;
            }
            catch (SqlException ex) when (ex.Number == 2812)   // procedura ne postoji
            {
                MessageBox.Show("Unos vlasnika nije moguć: na bazi nije puštena skripta Carina_VlasnikRobeUnos_2026_11.sql.", Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vlasnik robe nije sačuvan: " + ex.Message, Text);
            }
        }
    }
}
