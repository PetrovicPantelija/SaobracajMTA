using Microsoft.ReportingServices.Diagnostics.Internal;
using Saobracaj.Skladista;
using Saobracaj.Skladista_main.Dokumenta;
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
    public partial class frmMestoIsporuke: Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        int IdNaloga = 0;
        public frmMestoIsporuke(int idNaloga)
        {
            InitializeComponent();
            IdNaloga = idNaloga;
            ChangeTextBox();
            this.Text = "Mesto Istovara";
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

        private void cboMestoIstovaraPrijem_Leave(object sender, EventArgs e)
        {
            PopuniAdresu(cboMestoIstovaraPrijem, cboAdresaPrijem);
            PopuniKontaktOsobu(cboMestoIstovaraPrijem, cboKontaktOsobaPrijem);
        }

        private void PopuniAdresu(ComboBox cboIzvor, ComboBox cboCilj)
        {
            // Provera da li je vrednost validan broj (da izbegnemo grešku pri konverziji)
            if (cboIzvor.SelectedValue == null || !int.TryParse(cboIzvor.SelectedValue.ToString(), out int sifra))
            {
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    // Upit sa parametrom @Sifra
                    string sql = "SELECT PaKoZapSt, LTRIM(Rtrim(PaKOOpomba)) as Naziv FROM partnerjiKontOsebaMU WHERE PaKOSifra = @Sifra ORDER BY PaKOIme";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Sifra", sifra);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        cboCilj.DataSource = dt;
                        cboCilj.DisplayMember = "Naziv";
                        cboCilj.ValueMember = "PaKoZapSt";
                        cboCilj.SelectedIndex = 0;
                    }
                    else
                    {
                        // Ako nema rezultata null
                        cboCilj.DataSource = null;
                        cboCilj.Items.Clear();
                        cboCilj.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška: " + ex.Message);
            }
        }

        private void PopuniKontaktOsobu(ComboBox cboMesto, ComboBox cboKontakt)
        {
            // Provera selekcije
            if (cboMesto.SelectedValue == null || cboMesto.SelectedValue == DBNull.Value)
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {

                    string sql = @"SELECT PaKoZapSt, 
                           (Rtrim(PaKOIme) + ' ' + Rtrim(PaKoPriimek)) as Naziv 
                           FROM partnerjiKontOsebaMU 
                           WHERE PaKOSifra = @Sifra 
                           ORDER BY PaKOIme";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Sifra", Convert.ToInt32(cboMesto.SelectedValue));

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            cboKontakt.DataSource = dt;
                            cboKontakt.DisplayMember = "Naziv";
                            cboKontakt.ValueMember = "PaKoZapSt";
                            cboKontakt.SelectedIndex = 0;
                        }
                        else
                        {
                            // Ako nema rezultata null
                            cboKontakt.DataSource = null;
                            cboKontakt.Items.Clear();
                            cboKontakt.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška kod kontakta: " + ex.Message);
            }
        }

        private void VratiPodatke(int id)
        {
            var conn = new SqlConnection(connection);
            conn.Open();
            var cmd = new SqlCommand(@"SELECT rns.ID
                                  ,rns.MestoIstovaraPrijem
                                  ,rns.AdresaIntPrijem
                                  ,rns.KontaktOsobaIstovarIntPrijem
                                  ,rns.PlaniraniDatumPrijem
                                  ,rns.PlaniraniDatum2Prijem

                                FROM RadniNalogSkladista rns
                                       LEFT JOIN RadniNalogInterni rni on rns.ID = rni.BrojRN AND rni.IDManipulacijaJED = 74
                                       LEFT JOIN RadniNalogInterniPotvrda rnp ON  rnp.IDNaloga = rni.ID
                                       LEFT JOIN VlasniciRobe vlr ON  vlr.id = rns.VlasnikRobe
                                Where rns.ID=" + id, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {

               
                if (cboMestoIstovaraPrijem != null && dr["MestoIstovaraPrijem"] != DBNull.Value)
                {
                    cboMestoIstovaraPrijem.SelectedValue = Convert.ToInt32(dr["MestoIstovaraPrijem"].ToString());
                    PopuniAdresu(cboMestoIstovaraPrijem, cboAdresaPrijem);
                    if (dr["AdresaIntPrijem"] != DBNull.Value)
                        cboAdresaPrijem.SelectedValue = Convert.ToInt32(dr["AdresaIntPrijem"].ToString());
                    PopuniKontaktOsobu(cboMestoIstovaraPrijem, cboKontaktOsobaPrijem);
                    if (dr["KontaktOsobaIstovarIntPrijem"] != DBNull.Value)
                        cboKontaktOsobaPrijem.SelectedValue = Convert.ToInt32(dr["KontaktOsobaIstovarIntPrijem"].ToString());

                }
            }
            conn.Close();
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

               

               
                // 9. Mesta utovara
                NapuniCombo(cboMestoIstovaraPrijem, "SELECT ID, Naziv FROM MestaUtovara ORDER BY Naziv", "Naziv", "ID");
                cboMestoIstovaraPrijem.SelectedValue = 2;


            }
        }

        private void frmMestoIsporuke_Load(object sender, EventArgs e)
        {
            FillCombo();
            VratiPodatke(IdNaloga);
        }

        private void btnSnimi_Click(object sender, EventArgs e)
        {
            int? mestoIstovara = null;

            if (int.TryParse(cboMestoIstovaraPrijem.SelectedValue.ToString(), out int mestoIstovaraInt))
            {

                mestoIstovara = mestoIstovaraInt;
            }
            int? adresaPrijem = null;
            if (cboAdresaPrijem.SelectedValue != null)
            {
                adresaPrijem = Convert.ToInt32(cboAdresaPrijem.SelectedValue);
            }
            int? kontaktOsobaPrijem = null;
            if (cboKontaktOsobaPrijem.SelectedValue != null)
            {
                kontaktOsobaPrijem = Convert.ToInt32(cboKontaktOsobaPrijem.SelectedValue);
            }

            InsertCarinskoSkladiste insc = new InsertCarinskoSkladiste();
            insc.UpdateRadniNalogDirektniIstovar(IdNaloga, mestoIstovara, adresaPrijem, kontaktOsobaPrijem);

            MessageBox.Show("Podaci su uspešno snimljeni! ");
        }
    }
    

    
}
