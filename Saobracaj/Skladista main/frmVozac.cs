using Microsoft.ReportingServices.Diagnostics.Internal;
using Saobracaj.Izvoz;
using Saobracaj.Skladista;
using Saobracaj.Skladista_main.Dokumenta;
using Saobracaj.Uvoz;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main
{
   
    public partial class frmVozac: Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        int IdNaloga = 0;
        public frmVozac(int idNaloga)
        {
            InitializeComponent();
            IdNaloga = idNaloga;
            ChangeTextBox();
            this.Text = "Vozač";
        }

        private void ChangeTextBox()
        {
            this.BackColor = Color.White;
            this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
            this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
            Office2010Colors.ApplyManagedColors(this, Color.White);
            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
            meniHeader.Visible = false;
            this.ControlBox = true;
            // this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
                meniHeader.Visible = true;
                meniHeader.Visible = false;
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = Color.White;
                Office2010Colors.ApplyManagedColors(this, Color.White);

                //foreach (Control control in groupBox1.Controls)
                //{
                //    if (control is System.Windows.Forms.Button buttons)
                //    {

                //        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                //        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                //        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                //        buttons.FlatStyle = FlatStyle.Flat;
                //    }
                //}


                foreach (System.Windows.Forms.Control control in this.Controls)
                {

                    if (control is System.Windows.Forms.TextBox textBox)
                    {

                        textBox.BackColor = Color.White;// Example: Change background color
                        textBox.ForeColor = Color.FromArgb(51, 51, 54); //Boja slova u kvadratu
                        textBox.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                        // Example: Change font
                    }


                    if (control is System.Windows.Forms.Label label)
                    {
                        // Change properties here
                        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        label.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);  // Example: Change font

                        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                    }

                    if (control is DateTimePicker dtp)
                    {
                        dtp.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        dtp.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.CheckBox chk)
                    {
                        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        chk.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ListBox lb)
                    {
                        lb.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        lb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ComboBox cb)
                    {
                        cb.ForeColor = Color.FromArgb(51, 51, 54);
                        cb.BackColor = Color.White;// Example: Change background color
                        cb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.NumericUpDown nu)
                    {
                        nu.ForeColor = Color.FromArgb(51, 51, 54);
                        nu.BackColor = Color.White;// Example: Change background color
                        nu.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }
                }
            }
            else
            {
                meniHeader.Visible = false;
                meniHeader.Visible = true;
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }


        private void FillCombo()
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                // Pomoćna funkcija za brzo punjenje preko jedne konekcije
                void NapuniCombo(ComboBox cbo, string sqlQuery, string displayMember, string valueMember)
                {
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            cbo.DataSource = dt;
                            cbo.DisplayMember = displayMember;
                            cbo.ValueMember = valueMember;
                        }
                    }
                }

              
                NapuniCombo(cboVrstaKamionaPrijem, "SELECT ID, LTRIM(RTRIM(Naziv)) as Naziv  FROM VrstaVozila ORDER BY ID ASC", "Naziv", "ID"); 


            }
        }

        private void btnSnimi_Click(object sender, EventArgs e)
        {
            //tipTransporta, vrstaKamionaPrijem,
            //                  voziloPrijem, vozacPrijem, lKPrijem, telefonPrijem,
            bool potvrdjenNalog = proveriDaLiJePotvrdioLO();

            if (potvrdjenNalog == true)
            {
                MessageBox.Show("Nalog je već potvrđen i njije moguće snimiti izmene!");
                return;
            }

            int? vrstaKamionaPrijem = null;
            if (int.TryParse(cboVrstaKamionaPrijem.SelectedValue.ToString(), out int vrstaKamionaPrijemInt))
            {
                vrstaKamionaPrijem = vrstaKamionaPrijemInt;
            }

            string voziloPrijem = string.IsNullOrWhiteSpace(txtVoziloPrijem.Text) ? null : txtVoziloPrijem.Text.Trim();
            string vozacPrijem = string.IsNullOrWhiteSpace(txtVozacPrijem.Text) ? null : txtVozacPrijem.Text.Trim();
            string lKPrijem = string.IsNullOrWhiteSpace(txtLKPrijem.Text) ? null : txtLKPrijem.Text.Trim();
            string telefonPrijem = string.IsNullOrWhiteSpace(txtTelefonPrijem.Text) ? null : txtTelefonPrijem.Text.Trim();

            InsertCarinskoSkladiste insc = new InsertCarinskoSkladiste();
            insc.UpdateRadniNalogDirektniVozilo(IdNaloga, vrstaKamionaPrijem, voziloPrijem, vozacPrijem, lKPrijem, telefonPrijem);

            MessageBox.Show("Podaci su uspešno snimljeni! ");
        }

        private void frmVozac_Load(object sender, EventArgs e)
        {
            FillCombo();
            VratiPodatke(IdNaloga);
        }


        private void VratiPodatke(int id)
        {
            var conn = new SqlConnection(connection);
            conn.Open();
            var cmd = new SqlCommand(@"SELECT rns.ID
                                  ,rns.VrstaPrevoznogSredstvaPrijem
                                  ,rns.VrstaKamionaPrijem
                                  ,rns.VoziloPrijem
                                  ,rns.VozacPrijem
                                  ,rns.BrojLKPrijem
                                  ,rns.BrojTelefonaPrijem
                                FROM RadniNalogSkladista rns
                                Where rns.ID=" + id, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                if (cboVrstaKamionaPrijem != null && dr["VrstaKamionaPrijem"] != DBNull.Value)
                    cboVrstaKamionaPrijem.SelectedValue = Convert.ToInt32(dr["VrstaKamionaPrijem"].ToString());
                txtVoziloPrijem.Text = dr["VoziloPrijem"].ToString();
                txtVozacPrijem.Text = dr["VozacPrijem"].ToString();
                txtLKPrijem.Text = dr["BrojLKPrijem"].ToString();
                txtTelefonPrijem.Text = dr["BrojTelefonaPrijem"].ToString();
    


            }
            conn.Close();
        }

        private bool proveriDaLiJePotvrdioLO()
        {
            bool potvrdjenNalog = false;
            int VratiNalogID = VratiNalogIDF();
            using (var connection = new SqlConnection(Sifarnici.frmLogovanje.connectionString))
            {
                connection.Open();
                var cmd = new SqlCommand(@"
                                    SELECT TOP(1) idNaloga
                                    FROM RadniNalogInterniPotvrda                                
                                    WHERE idNaloga = @ID AND ISNull(LOSkladiste,0) = 1 ", connection);
                cmd.Parameters.AddWithValue("@ID", VratiNalogID);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int? loPotvrdjen = reader["idNaloga"] != DBNull.Value ? (int?)Convert.ToInt32(reader["idNaloga"]) : null;


                        if (loPotvrdjen.HasValue || loPotvrdjen.Value > 0)
                        {
                            potvrdjenNalog = true;
                        }
                    }
                }


            }
            return potvrdjenNalog;
        }

        int VratiNalogIDF()
        {
            int nalogID = 0;
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("select ID from RadniNalogInterni where TipRN = 'RN20' and IDManipulacijaJED = 74 and BrojRN =" + IdNaloga, conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        nalogID = Convert.ToInt32(dr["ID"].ToString());
                    }
                }
                conn.Close();
            }

            return nalogID;
        }
    }
}
