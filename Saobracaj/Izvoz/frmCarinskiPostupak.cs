using Microsoft.Office.Interop.Excel;
using Org.BouncyCastle.Crypto;
using Saobracaj.MainLeget.LegNew;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Izvoz
{
    public partial class frmCarinskiPostupak: Form
    {

        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        int drumski = 0;
        int scenario = 0;
        List<int> noviIDs;

        public frmCarinskiPostupak(List<int> ids, int _drumski, int _scenarioID)
        {
            InitializeComponent();
            ChangeTextBox();
            drumski = _drumski;
            scenario = _scenarioID;
            noviIDs = ids;
            FillCombo();
        }

        private void ChangeTextBox()
        {

            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);



            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
                //  meniHeader.Visible = false;
                this.BackColor = Color.White;
                this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
                this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
                this.ControlBox = true;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                Office2010Colors.ApplyManagedColors(this, Color.White);
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = Color.White;


                foreach (Control control in this.Controls)
                {
                    if (control is System.Windows.Forms.Button buttons)
                    {

                        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                        buttons.FlatStyle = FlatStyle.Flat;
                    }
                }


                foreach (Control control in this.Controls)
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
                // meniHeader.Visible = true;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connection);

          

            var carp = "Select ID, Naziv From Carinarnice order by Naziv";
            var carpAD = new SqlDataAdapter(carp, conn);
            var carpDS = new DataSet();
            carpAD.Fill(carpDS);
            cboPolaznaCarinarnica.DataSource = carpDS.Tables[0];
            cboPolaznaCarinarnica.DisplayMember = "Naziv";
            cboPolaznaCarinarnica.ValueMember = "ID";
            cboPolaznaCarinarnica.SelectedIndex = -1;

            //Spediter
            var partner3 = "Select PaSifra,PaNaziv From Partnerji where Spediter =1 order by PaNaziv";
            var partAD3 = new SqlDataAdapter(partner3, conn);
            var partDS3 = new DataSet();
            partAD3.Fill(partDS3);
            cboSpediterPolazna.DataSource = partDS3.Tables[0];
            cboSpediterPolazna.DisplayMember = "PaNaziv";
            cboSpediterPolazna.ValueMember = "PaSifra";
            cboSpediterPolazna.SelectedIndex = -1;

            var caro = "Select ID, Naziv From Carinarnice order by Naziv";
            var caroAD = new SqlDataAdapter(caro, conn);
            var caroDS = new DataSet();
            caroAD.Fill(caroDS);
            cboOdredisnaCarinarnica.DataSource = caroDS.Tables[0];
            cboOdredisnaCarinarnica.DisplayMember = "Naziv";
            cboOdredisnaCarinarnica.ValueMember = "ID";
            cboOdredisnaCarinarnica.SelectedIndex = -1;

            //Spediter
            var partner4 = "Select PaSifra,PaNaziv From Partnerji where Spediter =1 order by PaNaziv";
            var partAD4 = new SqlDataAdapter(partner4, conn);
            var partDS4 = new DataSet();
            partAD4.Fill(partDS4);
            cboSpediterOdredisna.DataSource = partDS4.Tables[0];
            cboSpediterOdredisna.DisplayMember = "PaNaziv";
            cboSpediterOdredisna.ValueMember = "PaSifra";
            cboSpediterOdredisna.SelectedIndex = -1;

         

            //carinski postupak
            var dir2 = "Select ID, (Oznaka + ' ' + Naziv) as Naziv from VrstaCarinskogPostupka order by Naziv";
            var dirAD2 = new SqlDataAdapter(dir2, conn);
            var dirDS2 = new DataSet();
            dirAD2.Fill(dirDS2);
            cboCarinskiPUnutrasniTransport.DataSource = dirDS2.Tables[0];
            cboCarinskiPUnutrasniTransport.DisplayMember = "Naziv";
            cboCarinskiPUnutrasniTransport.ValueMember = "ID";


        
        }

        private void button21_Click(object sender, EventArgs e)
        {
            InsertIzvoz ins = new InsertIzvoz();

            if (!ValidacijaSaIkonama())
            {
                MessageBox.Show("Molimo popunite označena polja.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Prekida se izvršavanje ako nije validno
            }

            int? carinskiPostupakUUnutrasnjemTranzitu = null;
            int? polaznaCarinarnica = null;
            int? spediterPolazna = null;
            string kontaktOsobaPolazna = null;
            int? odredisnaCarinarnica = null;
            int? spediterOdredisna = null;
            string kontaktOsobaOdrdisna = null;


            if (int.TryParse(cboCarinskiPUnutrasniTransport.SelectedValue.ToString(), out int cpostupak))
            {
                carinskiPostupakUUnutrasnjemTranzitu = cpostupak;
            }

            if (int.TryParse(cboPolaznaCarinarnica.SelectedValue.ToString(), out int polazna))
            {
                polaznaCarinarnica = polazna;
            }

            if (int.TryParse(cboSpediterPolazna.SelectedValue.ToString(), out int polaznaS))
            {
                spediterPolazna = polaznaS;
            }

            kontaktOsobaPolazna = string.IsNullOrWhiteSpace(txtKontaktSpeditera.Text) ? null : txtKontaktSpeditera.Text.Trim();

            if (int.TryParse(cboOdredisnaCarinarnica.SelectedValue.ToString(), out int odredisna))
            {
                odredisnaCarinarnica = odredisna;
            }

            if (int.TryParse(cboSpediterOdredisna.SelectedValue.ToString(), out int odredisnaS))
            {
                spediterOdredisna = odredisnaS;
            }
            kontaktOsobaOdrdisna = string.IsNullOrWhiteSpace(txtKontaktSpediteraOdredisna.Text) ? null : txtKontaktSpediteraOdredisna.Text.Trim();



            ins.UpdateIzvozPorudzbenicaCarina(noviIDs, carinskiPostupakUUnutrasnjemTranzitu, polaznaCarinarnica, spediterPolazna, kontaktOsobaPolazna, odredisnaCarinarnica, spediterOdredisna, kontaktOsobaOdrdisna);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var detailForm = new Dokumenta.frmKontaktOsobe(Convert.ToInt32(cboSpediterPolazna.SelectedValue)))
            {
                detailForm.ShowDialog();
                txtKontaktSpeditera.Text = detailForm.GetKontakt(Convert.ToInt32(cboSpediterPolazna.SelectedValue));
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (var detailForm = new Dokumenta.frmKontaktOsobe(Convert.ToInt32(cboSpediterOdredisna.SelectedValue)))
            {
                detailForm.ShowDialog();
                txtKontaktSpediteraOdredisna.Text = detailForm.GetKontakt(Convert.ToInt32(cboSpediterOdredisna.SelectedValue));
            }
        }

        private void frmCarinskiPostupak_Load(object sender, EventArgs e)
        {
            VratiPodatkeSelect();
            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        }

        private void VratiPodatkeSelect()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            if (noviIDs.Count == 0) return;

            string idsZaUpit = string.Join(",", noviIDs);

            con.Open();

            SqlCommand cmd = new SqlCommand($@"
                                     SELECT TOP 1 
                                         CarinskiPostupakUnutrasnji, 
                                         MestoCarinjenja, 
                                         Spedicija, 
                                         KontaktSpeditera,
                                         OdredisnaCarinarnica, 
                                         SpediterOdredisna, 
                                         KontaktSpediteraOdredisna
                                     FROM [dbo].[Izvoz] 
                                     WHERE ID IN ({idsZaUpit})", con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                if (dr["CarinskiPostupakUnutrasnji"] != DBNull.Value)
                    cboCarinskiPUnutrasniTransport.SelectedValue = Convert.ToInt32(dr["CarinskiPostupakUnutrasnji"].ToString());

                if (dr["MestoCarinjenja"] != DBNull.Value)
                    cboPolaznaCarinarnica.SelectedValue = Convert.ToInt32(dr["MestoCarinjenja"].ToString());

                if (dr["Spedicija"] != DBNull.Value)
                    cboSpediterPolazna.SelectedValue = Convert.ToInt32(dr["Spedicija"].ToString());

                if (dr["OdredisnaCarinarnica"] != DBNull.Value)
                    cboOdredisnaCarinarnica.SelectedValue = Convert.ToInt32(dr["OdredisnaCarinarnica"].ToString());

                if (dr["SpediterOdredisna"] != DBNull.Value)
                    cboSpediterOdredisna.SelectedValue = Convert.ToInt32(dr["SpediterOdredisna"].ToString());

                txtKontaktSpediteraOdredisna.Text = dr["KontaktSpediteraOdredisna"].ToString().Trim();
                txtKontaktSpeditera.Text = dr["KontaktSpeditera"].ToString().Trim(); ; 

            }
        }

        private bool ValidacijaSaIkonama()
        {
            bool uspesno = true;
            errorProvider1.Clear(); // Obavezno prvo očisti stare greške

           

            if (cboCarinskiPUnutrasniTransport.SelectedIndex < 1)
            {
                errorProvider1.SetError(cboCarinskiPUnutrasniTransport, "Morate izabrati neku vrednost!");
                uspesno = false;
            }

            if (cboPolaznaCarinarnica.SelectedIndex < 1)
            {
                errorProvider1.SetError(cboPolaznaCarinarnica, "Morate izabrati neku vrednost!");
                uspesno = false;
            }
            if (cboSpediterPolazna.SelectedIndex < 1)
            {
                errorProvider1.SetError(cboSpediterPolazna, "Morate izabrati neku vrednost!");
                uspesno = false;
            }
            if (cboOdredisnaCarinarnica.SelectedIndex < 1)
            {
                errorProvider1.SetError(cboOdredisnaCarinarnica, "Morate izabrati neku vrednost!");
                uspesno = false;
            }
            if (cboSpediterOdredisna.SelectedIndex < 1)
            {
                errorProvider1.SetError(cboSpediterOdredisna, "Morate izabrati neku vrednost!");
                uspesno = false;
            }
            return uspesno;
        }

    }
}
