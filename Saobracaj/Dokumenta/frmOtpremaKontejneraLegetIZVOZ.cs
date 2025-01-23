using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using Syncfusion.Windows.Forms;

namespace Saobracaj.Dokumenta
{
    public partial class frmOtpremaKontejneraLegetIZVOZ : Form
    {

        string KorisnikCene;
        bool status = false;
        MailMessage mailMessage;
        int usao = 0;

        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

        private void ChangeTextBox()
        {


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
                panelHeader.Visible = true;
                meniHeader.Visible = false;
                this.BackColor = Color.White;
                this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
                this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
                this.ControlBox = true;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                Office2010Colors.ApplyManagedColors(this, Color.White);
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;



                foreach (Control control in this.Controls)
                {

                }


                foreach (Control control in this.Controls)
                {
                    if (control is System.Windows.Forms.Button buttons)
                    {
                        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                        buttons.FlatStyle = FlatStyle.Flat;
                    }

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
                panelHeader.Visible = false;
                meniHeader.Visible = true;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        private void ChangeTextBoxPanel1()
        {


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;


                foreach (Control control in panel1.Controls)
                {
                    if (control is System.Windows.Forms.Button buttons)
                    {
                        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                        buttons.FlatStyle = FlatStyle.Flat;
                    }

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

                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        private void ChangeTextBoxPanelSplit1()
        {


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;


                foreach (Control control in splitContainer1.Panel1.Controls)
                {
                    if (control is System.Windows.Forms.Button buttons)
                    {
                        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                        buttons.FlatStyle = FlatStyle.Flat;
                    }

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

                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        private void ChangeTextBoxPanelSplit2()
        {


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;


                foreach (Control control in splitContainer1.Panel2.Controls)
                {
                    if (control is System.Windows.Forms.Button buttons)
                    {
                        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                        buttons.FlatStyle = FlatStyle.Flat;
                    }

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

                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        private void PodesiDatagridView(DataGridView dgv)
        {

            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(90, 199, 249); // Selektovana boja
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.BackgroundColor = Color.White;

            dgv.DefaultCellStyle.Font = new Font("Helvetica", 12F, GraphicsUnit.Pixel);
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(51, 51, 54);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);
            dgv.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);


            //Header
            dgv.EnableHeadersVisualStyles = false;
            //   header.Style.Font = new Font("Arial", 12F, FontStyle.Bold);
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 54);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv.ColumnHeadersHeight = 30;
        }

        public frmOtpremaKontejneraLegetIZVOZ()
        {
            InitializeComponent();
            ChangeTextBox();
            ChangeTextBoxPanel1();
            ChangeTextBoxPanelSplit1();
            ChangeTextBoxPanelSplit2();
        }

        public frmOtpremaKontejneraLegetIZVOZ(string Korisnik)
        {
            InitializeComponent();
            KorisnikCene = Korisnik;
            ChangeTextBox();
            ChangeTextBoxPanel1();
            ChangeTextBoxPanelSplit1();
            ChangeTextBoxPanelSplit2();
        }

        public frmOtpremaKontejneraLegetIZVOZ(string Korisnik, int Vozom)
        {
            InitializeComponent();
            ChangeTextBox();
            ChangeTextBoxPanel1();
            ChangeTextBoxPanelSplit1();
            ChangeTextBoxPanelSplit2();

            KorisnikCene = Korisnik;
            if (Vozom == 1)
            {
                chkVoz.Checked = true;
                txtRegBrKamiona.Enabled = false;
                txtImeVozaca.Enabled = false;
                cboVozBuking.Enabled = true;
                // toolStripButton3.Visible = false;

                toolStripLabel1.Visible = true;
            }
            else
            {
                chkVoz.Checked = false;
                txtRegBrKamiona.Enabled = true;
                txtImeVozaca.Enabled = true;
                cboVozBuking.Enabled = false;
                //   toolStripButton3.Visible = false;
                toolStripLabel1.Visible = false;

                dtpDatumOtpreme.Value = DateTime.Now;
                dtpVremeOdlaska.Value = DateTime.Now;
            }
        }

        public frmOtpremaKontejneraLegetIZVOZ(int sifra, string Korisnik)
        {
            InitializeComponent();
            ChangeTextBox();
            ChangeTextBoxPanel1();
            ChangeTextBoxPanelSplit1();
            ChangeTextBoxPanelSplit2();
            KorisnikCene = Korisnik;
            txtSifra.Text = sifra.ToString();
            VratiPodatke(sifra);
            RefreshDataGrid2();
            if (chkVoz.Checked == true)
            {
                //  toolStripButton3.Visible = false;
                toolStripLabel1.Visible = true;
            }
            else
            {
                //  toolStripButton3.Visible = true;
                toolStripLabel1.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtSifra.Text == "")
            {
                MessageBox.Show("Niste uneli zaglavlje");
            }
            else
            {
                InsertOtpremaKontejneraStavke ins = new InsertOtpremaKontejneraStavke();

                //Insert izvoz 
                ins.InsertOtpremaKontejneraStav(Convert.ToInt32(txtSifra.Text), txtBrojKontejnera.Text, txtVagon.Text, Convert.ToDouble(txtGranica.Value), Convert.ToDouble(txtBrojOsovina.Value), Convert.ToDouble(txtSopstvenaMasa.Value), Convert.ToDouble(txtTara.Value), Convert.ToDouble(txtNeto.Value), Convert.ToInt32(cboPosiljalac.SelectedValue), Convert.ToInt32(cboPrimalac.SelectedValue), Convert.ToInt32(cboVlasnikKontejnera.SelectedValue), Convert.ToInt32(cboTipKontejnera.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue), txtBukingBrodar.Text, Convert.ToInt32(cboStatusKontejnera.SelectedValue), txtBrojPlombe.Text, Convert.ToInt32(txtPlaniraniLager.Text), 0, Convert.ToDateTime(dtpVremePripremljen.Value), Convert.ToDateTime(dtpVremeOdlaska.Value), Convert.ToDateTime(DateTime.Now), KorisnikCene, txtBrojPlombe2.Text, Convert.ToInt32(cboOrganizator.SelectedValue), txtNapomenaS.Text, DateTime.Now, DateTime.Now, 0,0,0,"",0, Convert.ToDouble(bttoRobeFaktura.Value),
Convert.ToDouble(bttoRobeOtpremnica.Value),Convert.ToDouble(bttoRobeOdvaga.Value),Convert.ToDouble(bttoRobeKontejner.Value),txtPLOMBAVLASN.Text,txtCBMOTP.Text,txtKOLETAOTP.Text, 0);
                RefreshDataGrid2();
            }

           
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtSifra.Enabled = false;
            dtpVremeOdlaska.Value = DateTime.Now;
            dtpDatumOtpreme.Value = DateTime.Now;
            dtpDatumOtpreme.Enabled = true;
        }

        private void VratiPodatkeMax()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Max([ID]) as ID from OtpremaKontejnera", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtSifra.Text = dr["ID"].ToString();
            }

            con.Close();
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            int otpremaVozom = 0;
            if (chkVoz.Checked == true)
            {
                otpremaVozom = 1;
            }
            else
            { otpremaVozom = 0; };
            if (status == true)
            {

                InsertOtprema ins = new InsertOtprema();
                ins.InsertOtp(Convert.ToDateTime(dtpDatumOtpreme.Text), Convert.ToInt32(cboStatusOtpreme.SelectedIndex), Convert.ToInt32(cboVozBuking.SelectedValue), txtRegBrKamiona.Text, txtImeVozaca.Text, Convert.ToDateTime(dtpVremeOdlaska.Value), otpremaVozom, Convert.ToDateTime(DateTime.Now), KorisnikCene, txtNapomena.Text, Convert.ToInt32(cboPredefinisanePoruke.SelectedValue),0,0,0);
                status = false;
                VratiPodatkeMax();
            }
            else
            {
                //int TipCenovnika ,int Komitent, double Cena , int VrstaManipulacije ,DateTime  Datum , string Korisnik
                InsertOtprema upd = new InsertOtprema();
                upd.UpdOtpremaKontejnera(Convert.ToInt32(txtSifra.Text), Convert.ToDateTime(dtpDatumOtpreme.Text), Convert.ToInt32(cboStatusOtpreme.SelectedIndex), Convert.ToInt32(cboVozBuking.SelectedValue), txtRegBrKamiona.Text, txtImeVozaca.Text, Convert.ToDateTime(dtpVremeOdlaska.Value), otpremaVozom, Convert.ToDateTime(DateTime.Now), KorisnikCene, txtNapomena.Text, Convert.ToInt32(cboPredefinisanePoruke.SelectedValue),0,0,0);
                status = false;
            }
        }

        private void RefreshDataGrid()
        {
            var select = "  SELECT OtpremaKontejneraVozStavke.ID, OtpremaKontejneraVozStavke.RB, OtpremaKontejneraVozStavke.IDNadredjenog,  OtpremaKontejneraVozStavke.BrojKontejnera, OtpremaKontejneraVozStavke.Granica, "
                        + " OtpremaKontejneraVozStavke.BrojOsovina, OtpremaKontejneraVozStavke.SopstvenaMasa, OtpremaKontejneraVozStavke.Tara, OtpremaKontejneraVozStavke.Neto, Komitenti.Naziv AS Posiljalac, Komitenti_1.Naziv AS primalac, "
                        + " Komitenti_2.Naziv AS Vlasnikkontejnera, " +
                          " Komitenti_3.Naziv AS Organizator, " +
                        "  TipKontenjera.Naziv AS TipKontejnera, VrstaRobe.Naziv AS VrstaRobe, OtpremaKontejneraVozStavke.Buking , OtpremaKontejneraVozStavke.StatusKontejnera, "
                        + " OtpremaKontejneraVozStavke.BrojPlombe, OtpremaKontejneraVozStavke.BrojPlombe2, OtpremaKontejneraVozStavke.PlaniraniLager,"
                         + " OtpremaKontejneraVozStavke.BrojVagona, "
                        + " OtpremaKontejneraVozStavke.Datum, OtpremaKontejneraVozStavke.Korisnik, OtpremaKontejneraVozStavke.NapomenaS "
                        + "FROM  Komitenti INNER JOIN "
                        + " OtpremaKontejneraVozStavke ON Komitenti.ID = OtpremaKontejneraVozStavke.Posiljalac INNER JOIN "
                        + " Komitenti AS Komitenti_1 ON OtpremaKontejneraVozStavke.Primalac = Komitenti_1.ID INNER JOIN "
                        + " Komitenti AS Komitenti_2 ON OtpremaKontejneraVozStavke.VlasnikKontejnera = Komitenti_2.ID INNER JOIN "
                          + " Komitenti AS Komitenti_3 ON OtpremaKontejneraVozStavke.Organizator = Komitenti_3.ID INNER JOIN "
                         + "TipKontenjera ON OtpremaKontejneraVozStavke.TipKontejnera = TipKontenjera.ID INNER JOIN "
                        + " VrstaRobe ON OtpremaKontejneraVozStavke.VrstaRobe = VrstaRobe.ID "
                          + " where IdNadredjenog = " + txtSifra.Text + " order by RB";

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = false;
            dataGridView1.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView1);

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[0].Visible = false;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "RB";
            dataGridView1.Columns[1].Width = 30;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Br otp";
            dataGridView1.Columns[2].Width = 30;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Broj kontejnera";
            dataGridView1.Columns[3].Width = 90;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Granica";
            dataGridView1.Columns[4].Width = 40;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Broj osovina";
            dataGridView1.Columns[5].Width = 20;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Sopstvena masa";
            dataGridView1.Columns[6].Width = 30;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Tara";
            dataGridView1.Columns[7].Width = 50;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Neto";
            dataGridView1.Columns[8].Width = 50;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Posiljalac";
            dataGridView1.Columns[9].Width = 50;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Primalac";
            dataGridView1.Columns[10].Width = 50;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Vlasnik kontejnera";
            dataGridView1.Columns[11].Width = 40;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Organizator";
            dataGridView1.Columns[12].Width = 40;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Tip kontejnera";
            dataGridView1.Columns[13].Width = 30;

            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Vrsta robe";
            dataGridView1.Columns[14].Width = 30;

            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Buking";
            dataGridView1.Columns[15].Width = 30;

            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = " Status Kontejnera";
            dataGridView1.Columns[16].Width = 90;

            DataGridViewColumn column18 = dataGridView1.Columns[17];
            dataGridView1.Columns[17].HeaderText = "Broj plombe";
            dataGridView1.Columns[17].Width = 90;

            DataGridViewColumn column19 = dataGridView1.Columns[18];
            dataGridView1.Columns[18].HeaderText = "Br plombe 2";
            dataGridView1.Columns[18].Width = 70;

            DataGridViewColumn column20 = dataGridView1.Columns[19];
            dataGridView1.Columns[19].HeaderText = "Pl lager";
            dataGridView1.Columns[19].Width = 70;

            DataGridViewColumn column21 = dataGridView1.Columns[20];
            dataGridView1.Columns[20].HeaderText = "Vagon";
            dataGridView1.Columns[20].Width = 70;


            DataGridViewColumn column22 = dataGridView1.Columns[21];
            dataGridView1.Columns[21].HeaderText = "Datum";
            dataGridView1.Columns[21].Width = 70;

            DataGridViewColumn column23 = dataGridView1.Columns[22];
            dataGridView1.Columns[22].HeaderText = "Korisnik";
            dataGridView1.Columns[22].Width = 70;

            DataGridViewColumn column24 = dataGridView1.Columns[23];
            dataGridView1.Columns[23].HeaderText = "Napomena";
            dataGridView1.Columns[23].Width = 70;



        }


        private void RefreshDataGrid2()
        {

            var select = " SELECT OtpremaKontejneraVozStavke.ID, OtpremaKontejneraVozStavke.RB, OtpremaKontejneraVozStavke.IDNadredjenog, " +
 " OtpremaKontejneraVozStavke.BrojKontejnera, OtpremaKontejneraVozStavke.BrojVagona, OtpremaKontejneraVozStavke.Granica,  " +
 " OtpremaKontejneraVozStavke.BrojOsovina, OtpremaKontejneraVozStavke.SopstvenaMasa, OtpremaKontejneraVozStavke.Tara, OtpremaKontejneraVozStavke.Neto, " +
 " Partnerji.PaNaziv AS Posiljalac, Komitenti_1.PaNaziv AS primalac,  Komitenti_2.PaNaziv AS Vlasnikkontejnera,  Komitenti_3.PaNaziv AS Organizator, " +
 " TipKontenjera.Naziv AS TipKontejnera, VrstaRobe.Naziv AS VrstaRobe, OtpremaKontejneraVozStavke.Buking , " +
 " OtpremaKontejneraVozStavke.StatusKontejnera,  OtpremaKontejneraVozStavke.BrojPlombe, OtpremaKontejneraVozStavke.BrojPlombe2, " +
 " OtpremaKontejneraVozStavke.PlaniraniLager, OtpremaKontejneraVozStavke.Datum, OtpremaKontejneraVozStavke.Korisnik, OtpremaKontejneraVozStavke.NapomenaS FROM  Partnerji " +
 " INNER JOIN OtpremaKontejneraVozStavke ON Partnerji.Pasifra = OtpremaKontejneraVozStavke.Posiljalac " +
 " INNER JOIN  Partnerji AS Komitenti_1 ON OtpremaKontejneraVozStavke.Primalac = Komitenti_1.PaSifra INNER JOIN  Partnerji AS Komitenti_2 ON OtpremaKontejneraVozStavke.VlasnikKontejnera = Komitenti_2.PaSifra INNER JOIN  Partnerji AS Komitenti_3 ON OtpremaKontejneraVozStavke.Organizator = Komitenti_3.PaSifra " +
 " INNER JOIN TipKontenjera ON OtpremaKontejneraVozStavke.TipKontejnera = TipKontenjera.ID " +
 " INNER JOIN  VrstaRobe ON OtpremaKontejneraVozStavke.VrstaRobe = VrstaRobe.ID  " +
                        " where IdNadredjenog = " + txtSifra.Text + " order by RB";


            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = false;
            dataGridView1.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView1);

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[0].Visible = false;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "RB";
            dataGridView1.Columns[1].Width = 30;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Br otp";
            dataGridView1.Columns[2].Width = 30;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Broj kontejnera";
            dataGridView1.Columns[3].Width = 90;



            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Vagon";
            dataGridView1.Columns[4].Width = 70;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Granica";
            dataGridView1.Columns[5].Width = 40;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Broj osovina";
            dataGridView1.Columns[6].Width = 20;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Sopstvena masa";
            dataGridView1.Columns[7].Width = 30;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Tara";
            dataGridView1.Columns[8].Width = 50;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Neto";
            dataGridView1.Columns[9].Width = 50;



            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Posiljalac";
            dataGridView1.Columns[10].Width = 50;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Primalac";

            dataGridView1.Columns[11].Width = 50;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Vlasnik kontejnera";
            dataGridView1.Columns[12].Width = 40;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Organizator";
            dataGridView1.Columns[13].Width = 40;



            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Tip kontejnera";
            dataGridView1.Columns[14].Width = 30;

            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Vrsta robe";
            dataGridView1.Columns[15].Width = 30;

            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Buking";
            dataGridView1.Columns[16].Width = 30;


            DataGridViewColumn column18 = dataGridView1.Columns[17];
            dataGridView1.Columns[17].HeaderText = " Status Kontejnera";
            dataGridView1.Columns[17].Width = 90;

            DataGridViewColumn column19 = dataGridView1.Columns[18];
            dataGridView1.Columns[18].HeaderText = "Broj plombe";
            dataGridView1.Columns[18].Width = 90;

            DataGridViewColumn column20 = dataGridView1.Columns[19];
            dataGridView1.Columns[19].HeaderText = "Br plombe 2";
            dataGridView1.Columns[19].Width = 70;

            DataGridViewColumn column21 = dataGridView1.Columns[20];
            dataGridView1.Columns[20].HeaderText = "Pl lager";
            dataGridView1.Columns[20].Width = 70;



            DataGridViewColumn column22 = dataGridView1.Columns[21];
            dataGridView1.Columns[21].HeaderText = "Datum";
            dataGridView1.Columns[21].Width = 70;

            DataGridViewColumn column23 = dataGridView1.Columns[22];
            dataGridView1.Columns[22].HeaderText = "Korisnik";
            dataGridView1.Columns[22].Width = 70;

            DataGridViewColumn column24 = dataGridView1.Columns[23];
            dataGridView1.Columns[23].HeaderText = "Napomena";
            dataGridView1.Columns[23].Width = 70;







        }
        private void VratiPodatke(int ID)
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            // SqlCommand cmd = new SqlCommand("select [ID] ,[DatumOtpreme],[StatusOtpreme],[IdVoza],[VremeOdlaska], [RegBrKamiona], [ImeVozaca], NacinOtpreme, Napomena, NajavaEmail, OtpremaEmail, Zatvoren, CIRUradjen, PredefinisanePorukeID from OtpremaKontejnera where ID = " + ID, con);

            SqlCommand cmd = new SqlCommand("select [ID] ,[DatumOtpreme],[StatusOtpreme],[IdVoza],[VremeOdlaska], [RegBrKamiona], [ImeVozaca], NacinOtpreme, Napomena, NajavaEmail, OtpremaEmail, Zatvoren, CIRUradjen, Operater from OtpremaKontejnera where ID = " + ID, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                dtpDatumOtpreme.Value = Convert.ToDateTime(dr["DatumOtpreme"].ToString());
                dtpVremeOdlaska.Value = Convert.ToDateTime(dr["VremeOdlaska"].ToString());
                cboVozBuking.SelectedValue = Convert.ToInt32(dr["IDVoza"].ToString());
                // cboPredefinisanePoruke.SelectedValue = Convert.ToInt32(dr["PredefinisanePorukeID"].ToString());
                cboStatusOtpreme.SelectedIndex = Convert.ToInt32(dr["StatusOtpreme"].ToString());
                txtRegBrKamiona.Text = dr["RegBrKamiona"].ToString();
                txtImeVozaca.Text = dr["ImeVozaca"].ToString();
                txtNapomena.Text = dr["Napomena"].ToString();
                cboOperater.SelectedValue = Convert.ToInt32(dr["Operater"].ToString());
                cboVozBuking.SelectedValue = Convert.ToInt32(dr["IDVoza"].ToString());
                if (Convert.ToInt32(dr["NacinOtpreme"].ToString()) == 1)
                {
                    chkVoz.Checked = true;
                }
                else
                {
                    chkVoz.Checked = false;
                }

                if (Convert.ToInt32(dr["Zatvoren"].ToString()) == 1)
                {
                    chkZatvoren.Checked = true;
                }
                else
                {
                    chkZatvoren.Checked = false;
                }

                if (Convert.ToInt32(dr["NajavaEmail"].ToString()) == 1)
                {
                    chkNajava.Checked = true;
                }
                else
                {
                    chkNajava.Checked = false;
                }


                if (Convert.ToInt32(dr["OtpremaEmail"].ToString()) == 1)
                {
                    chkOtprema.Checked = true;
                }
                else
                {
                    chkOtprema.Checked = false;
                }
                if (Convert.ToInt32(dr["CIRUradjen"].ToString()) == 0)
                {
                    chkCIRUradjen.Checked = false;
                }
                else
                {
                    chkCIRUradjen.Checked = true;
                }

            }

            con.Close();

        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da želite da brišete?", "Izbor", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                InsertOtprema upd = new InsertOtprema();
                upd.DeleteOtpremaKontejnera(Convert.ToInt32(txtSifra.Text));
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InsertOtpremaKontejneraStavke ins = new InsertOtpremaKontejneraStavke();
            ins.UpdOtpremaKontejneraVozStav(Convert.ToInt32(txtStavka.Text), Convert.ToInt32(txtSifra.Text), txtBrojKontejnera.Text, txtVagon.Text, Convert.ToDouble(txtGranica.Value), Convert.ToDouble(txtBrojOsovina.Value), Convert.ToDouble(txtSopstvenaMasa.Value), Convert.ToDouble(txtTara.Value), Convert.ToDouble(txtNeto.Value), Convert.ToInt32(cboPosiljalac.SelectedValue), Convert.ToInt32(cboPrimalac.SelectedValue), Convert.ToInt32(cboVlasnikKontejnera.SelectedValue), Convert.ToInt32(cboTipKontejnera.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue), txtBukingBrodar.Text, Convert.ToInt32(cboStatusKontejnera.SelectedValue), txtBrojPlombe.Text, Convert.ToInt32(txtPlaniraniLager.Text), 0, Convert.ToDateTime(dtpVremePripremljen.Value), Convert.ToDateTime(dtpVremeOdlaska.Value), Convert.ToDateTime(DateTime.Now), KorisnikCene, Convert.ToInt32(txtRB.Text), txtBrojPlombe2.Text, Convert.ToInt32(cboOrganizator.SelectedValue), txtNapomenaS.Text, DateTime.Now, DateTime.Now, 0,0,0,"",0, Convert.ToDouble(bttoRobeFaktura.Value),
Convert.ToDouble(bttoRobeOtpremnica.Value), Convert.ToDouble(bttoRobeOdvaga.Value), Convert.ToDouble(bttoRobeKontejner.Value), txtPLOMBAVLASN.Text, txtCBMOTP.Text, txtKOLETAOTP.Text);
            RefreshDataGrid2();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da želite da brišete?", "Izbor", MessageBoxButtons.YesNo);

            InsertOtpremaKontejneraStavke dels = new InsertOtpremaKontejneraStavke();


            if (dialogResult == DialogResult.Yes)
            {
                dels.DeleteOtpremaKontejneraVozStav(Convert.ToInt32(txtStavka.Text));
                RefreshDataGrid2();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    Saobracaj.Dokumeta.InsertPrijemKontejneraVozStavke insTer = new Saobracaj.Dokumeta.InsertPrijemKontejneraVozStavke();
                    insTer.UpdateOtpremaKontejneraVozStavkeRB(Convert.ToInt32(row.Cells[1].Value.ToString()), Convert.ToInt32(row.Cells[0].Value.ToString()));
                    //Row.CellS
                }
                RefreshDataGrid2();
            }
            catch
            {
                MessageBox.Show("Nije uspela promena rednog broja");
            }
        }

        private void ProveraKontrolnogBroja()
        {
            string KontrolniBroj = txtBrojKontejnera.Text.TrimEnd();
            string CheckDigit = "100";
            CheckDigit = KontrolniBroj.Substring(KontrolniBroj.Length - 1, 1);
            int kontrolni = 0;
            /*
            string A = "10"; string B = "11"; string C = "12"; string D = "13"; string E = "14"; string F = "15";
            string G = "16"; string H = "17"; string I = "18"; string J = "19"; string K = "20"; string L = "21";
            string M = "22"; string N = "23"; string O = "24"; string P = "25"; string Q = "26"; string R = "27";
            string S = "28"; string T= "29"; string U = "30"; string V = "31"; string W = "32"; string X = "33";
            string Y = "34"; string Z = "35";
            */
            string foo = KontrolniBroj.ToUpper();
            int ukupno = 0;
            int korak = 1;
            int Broj1 = 0;
            int korak1 = 0;
            int Ukupno1 = 0;
            foreach (char c in foo)
            {
                switch (c)
                {
                    case 'A':
                        // Some code here
                        Broj1 = 10;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 10;
                        korak = korak * 2;
                        break; // break that closes the case

                    case 'B':
                        Broj1 = 12;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 12;
                        korak = korak * 2;
                        break; // break that closes the case
                    case 'C':
                        Broj1 = 13;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 13;
                        korak = korak * 2;
                        break; // 
                    case 'D':
                        Broj1 = 14;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 14;
                        korak = korak * 2;
                        break; // 
                    case 'E':
                        Broj1 = 15;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 15;
                        korak = korak * 2;
                        break; // 
                    case 'F':
                        Broj1 = 16;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 16;
                        korak = korak * 2;
                        break; // 
                    case 'G':
                        Broj1 = 17;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 17;
                        korak = korak * 2;
                        break; // 
                    case 'H':
                        Broj1 = 18;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 18;
                        korak = korak * 2;
                        break; // 
                    case 'I':
                        Broj1 = 19;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 19;
                        korak = korak * 2;
                        break; // 
                    case 'J':
                        Broj1 = 20;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 20;
                        korak = korak * 2;
                        break; // 
                    case 'K':
                        Broj1 = 21;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 21;
                        korak = korak * 2;
                        break; // 
                    case 'L':
                        Broj1 = 23;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 23;
                        korak = korak * 2;
                        break; // 
                    case 'M':
                        Broj1 = 24;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 24;
                        korak = korak * 2;
                        break; // 
                    case 'N':
                        Broj1 = 25;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 25;
                        korak = korak * 2;
                        break; // 
                    case 'O':
                        Broj1 = 26;
                        korak1 = korak + 1;
                        ukupno = ukupno + korak * 26;
                        korak = korak * 2;
                        break; // 
                    case 'P':
                        Broj1 = 27;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 27;
                        korak = korak * 2;
                        break; // 
                    case 'Q':
                        Broj1 = 28;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 28;
                        korak = korak * 2;
                        break; // 
                    case 'R':
                        Broj1 = 29;
                        korak1 = korak + 1;
                        ukupno = ukupno + korak * 29;
                        korak = korak * 2;
                        break; // 
                    case 'S':
                        Broj1 = 30;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 30;
                        korak = korak * 2;
                        break; // 
                    case 'T':
                        Broj1 = 31;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 31;
                        korak = korak * 2;
                        break; // 
                    case 'U':
                        Broj1 = 32;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 32;
                        korak = korak * 2;
                        break; // 
                    case 'V':
                        Broj1 = 34;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 34;
                        korak = korak * 2;
                        break; // 
                    case 'W':
                        Broj1 = 35;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 35;
                        korak = korak * 2;
                        break; // 
                    case 'X':
                        Broj1 = 36;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 36;
                        korak = korak * 2;
                        break; // 
                    case 'Y':
                        Broj1 = 37;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 37;
                        korak = korak * 2;
                        break; // 
                    case 'Z':
                        Broj1 = 38;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 38;
                        korak = korak * 2;
                        break; // 
                    case '0':
                        Broj1 = 0;
                        korak1 = korak1 + 1;

                        break; // 
                    case '1':
                        Broj1 = 1;
                        korak1 = korak1 + 1;

                        break; // 
                    case '2':
                        Broj1 = 2;
                        korak1 = korak1 + 1;

                        break; //
                    case '3':
                        Broj1 = 3;
                        korak1 = korak1 + 1;

                        break; //
                    case '4':
                        Broj1 = 4;
                        korak1 = korak1 + 1;

                        break; //
                    case '5':
                        Broj1 = 5;
                        korak1 = korak1 + 1;
                        break; //
                    case '6':
                        Broj1 = 6;
                        korak1 = korak1 + 1;
                        break; //
                    case '7':
                        Broj1 = 7;
                        korak1 = korak1 + 1;
                        break; //
                    case '8':
                        Broj1 = 8;
                        korak1 = korak1 + 1;
                        break; //
                    case '9':
                        Broj1 = 9;
                        korak1 = korak1 + 1;
                        break; //
                    default:
                        {

                            break;
                        }
                }

                switch (korak1)
                {
                    case 1:
                        Ukupno1 = Ukupno1 + Broj1 * 1;
                        break; //
                    case 2:
                        Ukupno1 = Ukupno1 + Broj1 * 2;
                        break; //
                    case 3:
                        Ukupno1 = Ukupno1 + Broj1 * 4;
                        break; //
                    case 4:
                        Ukupno1 = Ukupno1 + Broj1 * 8;
                        break; //
                    case 5:
                        Ukupno1 = Ukupno1 + Broj1 * 16;
                        break; //
                    case 6:
                        Ukupno1 = Ukupno1 + Broj1 * 32;
                        break; //
                    case 7:
                        Ukupno1 = Ukupno1 + Broj1 * 64;
                        break; //
                    case 8:
                        Ukupno1 = Ukupno1 + Broj1 * 128;
                        break; //
                    case 9:
                        Ukupno1 = Ukupno1 + Broj1 * 256;
                        break;
                    case 10:
                        Ukupno1 = Ukupno1 + Broj1 * 512;
                        break;
                    default:
                        break;


                }

            }
            double kolicnik = Ukupno1 / 11;
            var Bez = int.Parse(kolicnik.ToString().Split('.').First());
            int Ukupno2 = Bez * 11;
            kontrolni = Ukupno1 - Ukupno2;


            int pomUkupno = ukupno / 11;
            pomUkupno = pomUkupno * 11;

            int ProveraJed = ukupno - pomUkupno;

            if (kontrolni.ToString() == CheckDigit)
            {
                // MessageBox.Show("Ispravan kontrolni broj");
            }
            else
            {
                MessageBox.Show("Pogrešan kontrolni broj");
            }
            /*
            if (ProveraJed.ToString() == CheckDigit)
            {
                MessageBox.Show("Ispravan kontrolni broj");
            }
            else
            {
                MessageBox.Show("Pogrešan kontrolni broj");
            }
            */

        }

        private void txtBrojKontejnera_Leave(object sender, EventArgs e)
        {
            ProveraKontrolnogBroja();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            VratiPodatkeStavkeKontejnerSaPrijemnice(txtBrojKontejnera.Text);
        }

        private void VratiPodatkeStavkeKontejnerSaPrijemnice(string BrojKontejnera)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT  Top 1      [Granica],[BrojOsovina] " +
      " ,[SopstvenaMasa],[Tara],[Neto]      ,[Posiljalac],[Primalac],[VlasnikKontejnera] " +
      " ,[TipKontejnera],[VrstaRobe],[Buking]      ,[StatusKontejnera],[BrojPlombe],[PlaniraniLager], " +
        " [Organizator], BrojPlombe, BrojPlombe2 " +
    "  ,[BukingBrodar]  FROM[dbo].[PrijemKontejneraVozStavke] " +
     " where BrojKontejnera = '" + BrojKontejnera.Trim() + "' order by id desc ", con);


            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                txtTara.Value = Convert.ToDecimal(dr["Tara"].ToString());
                txtNeto.Value = Convert.ToDecimal(dr["Neto"].ToString());
                txtGranica.Value = Convert.ToDecimal(dr["Granica"].ToString());
                txtBrojOsovina.Value = Convert.ToDecimal(dr["BrojOsovina"].ToString());
                txtSopstvenaMasa.Value = Convert.ToDecimal(dr["SopstvenaMasa"].ToString());
                cboPosiljalac.SelectedValue = Convert.ToInt32(dr["Posiljalac"].ToString());
                cboPrimalac.SelectedValue = Convert.ToInt32(dr["Primalac"].ToString());
                cboVlasnikKontejnera.SelectedValue = Convert.ToInt32(dr["VlasnikKontejnera"].ToString());
                cboTipKontejnera.SelectedValue = Convert.ToInt32(dr["TipKontejnera"].ToString());
                cboVrstaRobe.SelectedValue = Convert.ToInt32(dr["VrstaRobe"].ToString());
                cboStatusKontejnera.SelectedValue = Convert.ToInt32(dr["StatusKontejnera"].ToString());
                txtBukingBrodar.Text = dr["Buking"].ToString();
                cboOrganizator.SelectedValue = Convert.ToInt32(dr["Organizator"].ToString());
                txtBrojPlombe.Text = dr["BrojPlombe"].ToString();
                txtBrojPlombe2.Text = dr["BrojPlombe2"].ToString();
            }

            con.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            VratiPodatkeStavkeVagonSaPrijemnice(txtVagon.Text);
        }

        private void VratiPodatkeStavkeVagonSaPrijemnice(string BrojVagona)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT  Top 1      [Granica],[BrojOsovina] " +
            " ,[SopstvenaMasa] FROM[dbo].[PrijemKontejneraVozStavke] " +
            " where BrojKontejnera = '" + BrojVagona.Trim() + "' order by id desc ", con);


            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                txtGranica.Value = Convert.ToDecimal(dr["Granica"].ToString());
                txtBrojOsovina.Value = Convert.ToDecimal(dr["BrojOsovina"].ToString());
                txtSopstvenaMasa.Value = Convert.ToDecimal(dr["SopstvenaMasa"].ToString());


                con.Close();
            }
        }

        private void VratiPodatkeStavke(string IdNadredjenog, int RB)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT [ID],[IDNadredjenog]       ,[BrojKontejnera],[BrojVagona] " +
             " ,[Granica],[BrojOsovina],[SopstvenaMasa],[Tara] " +
             " ,[Neto],[Posiljalac],[Primalac],[VlasnikKontejnera] " +
             " ,[TipKontejnera],[VrstaRobe],[Buking],[StatusKontejnera] " +
             " ,[BrojPlombe],[PlaniraniLager],[IdVoza] " +
             " ,[VremePripremljen],[VremeOdlaska],[Datum],[Korisnik] " +
             " ,[RB],[BrojPlombe2],[Organizator], NapomenaS, KontejnerID, NalogID " +
             " FROM [dbo].[OtpremaKontejneraVozStavke] " +
             " where IdNadredjenog = " + txtSifra.Text + " and RB = " + RB, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtStavka.Text = dr["ID"].ToString();
                txtRB.Text = dr["RB"].ToString();
                txtVagon.Text = dr["BrojVagona"].ToString();
                txtBrojKontejnera.Text = dr["BrojKontejnera"].ToString();
               /*
                txtVagon.Text = dr["BrojVagona"].ToString();
                txtTara.Value = Convert.ToDecimal(dr["Tara"].ToString());
                txtNeto.Value = Convert.ToDecimal(dr["Neto"].ToString());
                txtGranica.Value = Convert.ToDecimal(dr["Granica"].ToString());
                txtBrojOsovina.Value = Convert.ToDecimal(dr["BrojOsovina"].ToString());
                txtSopstvenaMasa.Value = Convert.ToDecimal(dr["SopstvenaMasa"].ToString());
                cboPosiljalac.SelectedValue = Convert.ToInt32(dr["Posiljalac"].ToString());
                cboPrimalac.SelectedValue = Convert.ToInt32(dr["Primalac"].ToString());
                cboVlasnikKontejnera.SelectedValue = Convert.ToInt32(dr["VlasnikKontejnera"].ToString());
                cboTipKontejnera.SelectedValue = Convert.ToInt32(dr["TipKontejnera"].ToString());
                cboVrstaRobe.SelectedValue = Convert.ToInt32(dr["VrstaRobe"].ToString());
                cboStatusKontejnera.SelectedValue = Convert.ToInt32(dr["StatusKontejnera"].ToString());
                txtBukingBrodar.Text = dr["Buking"].ToString();
                // cboBukingOtpreme.SelectedValue = Convert.ToInt32(dr["IDVoza"].ToString());
                cboOrganizator.SelectedValue = Convert.ToInt32(dr["Organizator"].ToString());
                //txtBukingBrodar.Text = dr["Buking"].ToString();
                // dtpVremeDolaska.Value = Convert.ToDateTime(dr["VremeDolaska"].ToString());
                dtpVremePripremljen.Value = Convert.ToDateTime(dr["VremePripremljen"].ToString());
                dtpVremeOdlaska.Value = Convert.ToDateTime(dr["VremeOdlaska"].ToString());
                txtBrojPlombe.Text = dr["BrojPlombe"].ToString();
                txtBrojPlombe2.Text = dr["BrojPlombe2"].ToString();
                txtNapomenaS.Text = dr["NapomenaS"].ToString();
               */
                txtKOntejnerID.Text = dr["KontejnerID"].ToString();
                txtNalogID.Text = dr["NalogID"].ToString();
            }

            con.Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txtSifra.Text = row.Cells[2].Value.ToString();
                        VratiPodatkeStavke(txtSifra.Text, Convert.ToInt32(row.Cells[1].Value.ToString()));
                        VratiPodatkeSaIzvozKonacna();
                        VratiNaloge();
                        FillDGUsluge();
                        FillDG2();
                       
                        RefreshDataGridRN();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //Zatvaranje kontejnera
            //
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                Saobracaj.Dokumenta.InsertPromet ins = new Saobracaj.Dokumenta.InsertPromet();


                ins.UpdateZatvorenOtprema(row.Cells[1].Value.ToString(), Convert.ToDateTime(dtpVremeOdlaska.Value), Convert.ToInt32(txtSifra.Text));
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Saobracaj.Dokumenta.frmDokumentaOtpremaKontejnera dokotp = new frmDokumentaOtpremaKontejnera(txtSifra.Text, KorisnikCene);
            dokotp.Show();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            int broj = Convert.ToInt32(txtSifra.Text);
            Izvestaji.frmDodatniList dodList = new Izvestaji.frmDodatniList(broj);
            dodList.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (txtStavka.Text.Trim() == "")
            {
                MessageBox.Show("Niste izabrali stavku");
                return;
            }
            Saobracaj.Dokumenta.frmCIR cir = new Saobracaj.Dokumenta.frmCIR(KorisnikCene, Convert.ToInt32(txtStavka.Text), 1, txtBrojKontejnera.Text, txtVagon.Text, Convert.ToDouble(txtTara.Value), txtRegBrKamiona.Text, Convert.ToDouble(txtNeto.Value), Convert.ToInt32(cboTipKontejnera.SelectedValue), dtpVremeOdlaska.Value, txtBrojPlombe.Text, txtBrojPlombe2.Text);
            cir.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            int PostojiPrvi = 0;
           int PostojiDrugi = 0;
            string BrojKontejnera1 = "";
            string BrojKontejnera2 = "";
            string VrstaRobe1 = "";
            string VrstaRobe2 = "";
            double ukupnaMasa = 0;
            double ukupnaMasa2 = 0;
            string TipKontejnera = "";
            string TipKontejnera2 = "";
            ///Panta nadji
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        if (PostojiPrvi == 1)
                        {
                            //
                            // BrojKontejnera1 = row.Cells[3].Value.ToString();


                            BrojKontejnera2 = row.Cells[3].Value.ToString();
                            PostojiDrugi = 1;
                            VrstaRobe2 = row.Cells[15].Value.ToString();
                            ukupnaMasa2 = ukupnaMasa2 + Convert.ToDouble(row.Cells[8].Value.ToString()) + (Convert.ToDouble(row.Cells[9].Value.ToString()) / 1000);
                            TipKontejnera2 = row.Cells[14].Value.ToString();
                            //Panta
                            //14,7,13   Tara - 7 13 - Tip kontejnera 14 - Vrsta robe 8 - Neto
                        }
                        if (PostojiPrvi == 0)
                        {
                            BrojKontejnera1 = row.Cells[3].Value.ToString();
                            BrojKontejnera2 = "";
                            PostojiPrvi = 1;
                            VrstaRobe1 = row.Cells[15].Value.ToString();
                            ukupnaMasa = ukupnaMasa + Convert.ToDouble(row.Cells[8].Value.ToString()) + (Convert.ToDouble(row.Cells[9].Value.ToString()) / 1000);
                            TipKontejnera = row.Cells[14].Value.ToString();
                        }


                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }


            Saobracaj.Dokumenta.frmNalogZaPrevoz prevoz = new Saobracaj.Dokumenta.frmNalogZaPrevoz(BrojKontejnera1, BrojKontejnera2, VrstaRobe1, VrstaRobe2, ukupnaMasa, KorisnikCene, TipKontejnera, TipKontejnera2, ukupnaMasa2);
            prevoz.Show();
        }

        private void VratiPodatkeTara()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT Top 1 Tara " +
            " FROM [dbo].[TipKontenjera] " +
            " where ID = " + Convert.ToInt32(cboTipKontejnera.SelectedValue) + " order by id desc ", con);


            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtTara.Value = Convert.ToDecimal(dr["Tara"].ToString());


            }
            con.Close();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (chkVoz.Checked == true)
            {
                Saobracaj.Dokumenta.frmManipulacije pnm = new Saobracaj.Dokumenta.frmManipulacije(KorisnikCene, Convert.ToInt32(txtSifra.Text), 1, 0);
                pnm.Show();
            }
            else
            {
                Saobracaj.Dokumenta.frmManipulacije pnm = new Saobracaj.Dokumenta.frmManipulacije(KorisnikCene, Convert.ToInt32(txtSifra.Text), 0, 0);
                pnm.Show();
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Saobracaj.Testiranje.frmTovarniList tl = new Saobracaj.Testiranje.frmTovarniList();
            tl.Show();
        }

        private void cboTipKontejnera_Leave(object sender, EventArgs e)
        {
            VratiPodatkeTara();
        }

        private void frmOtpremaKontejneraLegetIZVOZ_Load(object sender, EventArgs e)
        {
            var select4 = " Select Distinct ID, Naziv From TipKontenjera order by Naziv";
            var s_connection4 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection4 = new SqlConnection(s_connection4);
            var c4 = new SqlConnection(s_connection4);
            var dataAdapter4 = new SqlDataAdapter(select4, c4);

            var commandBuilder4 = new SqlCommandBuilder(dataAdapter4);
            var ds4 = new DataSet();
            dataAdapter4.Fill(ds4);
            cboTipKontejnera.DataSource = ds4.Tables[0];
            cboTipKontejnera.DisplayMember = "Naziv";
            cboTipKontejnera.ValueMember = "ID";

            var select5 = " Select Distinct ID, (Cast(id as nvarchar(5)) + '-' + Cast(BrVoza as nvarchar(10)) + '-' + Cast(Relacija as nvarchar(100)) + '- '  + Cast(Convert(nvarchar(10),VremeDolaskaO,105) as Nvarchar(10))) as Naziv  From Voz where dolazeci = 0 ";
            var s_connection5 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection5 = new SqlConnection(s_connection5);
            var c5 = new SqlConnection(s_connection5);
            var dataAdapter5 = new SqlDataAdapter(select5, c5);

            var commandBuilder5 = new SqlCommandBuilder(dataAdapter5);
            var ds5 = new DataSet();
            dataAdapter5.Fill(ds5);
            cboVozBuking.DataSource = ds5.Tables[0];
            cboVozBuking.DisplayMember = "Naziv";
            cboVozBuking.ValueMember = "ID";

            var select9 = " Select Distinct PaSifra as ID, PaNaziv as Naziv From Partnerji  order by PaNaziv";
            var s_connection9 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection9 = new SqlConnection(s_connection9);
            var c9 = new SqlConnection(s_connection9);
            var dataAdapter9 = new SqlDataAdapter(select9, c9);

            var commandBuilder9 = new SqlCommandBuilder(dataAdapter9);
            var ds9 = new DataSet();
            dataAdapter9.Fill(ds9);
            cboOperater.DataSource = ds9.Tables[0];
            cboOperater.DisplayMember = "Naziv";
            cboOperater.ValueMember = "ID";


            var select3 = " Select Distinct PaSifra as ID, PaNaziv as Naziv From Partnerji  order by PaNaziv";
            var s_connection3 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboVlasnikKontejnera.DataSource = ds3.Tables[0];
            cboVlasnikKontejnera.DisplayMember = "Naziv";
            cboVlasnikKontejnera.ValueMember = "ID";


            var select6 = " Select ID,Naziv from DirigacijaKontejneraZa order by ID ";
            var s_connection6 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection6 = new SqlConnection(s_connection6);
            var c6 = new SqlConnection(s_connection6);
            var dataAdapter6 = new SqlDataAdapter(select6, c6);

            var commandBuilder6 = new SqlCommandBuilder(dataAdapter6);
            var ds6 = new DataSet();
            dataAdapter6.Fill(ds6);
            cboStatusKontejnera.DataSource = ds6.Tables[0];
            cboStatusKontejnera.DisplayMember = "Naziv";
            cboStatusKontejnera.ValueMember = "ID";


            var select7 = " Select ID,Naziv from VrstePostupakaUvoz order by ID ";
            var s_connection7 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection7 = new SqlConnection(s_connection7);
            var c7 = new SqlConnection(s_connection7);
            var dataAdapter7 = new SqlDataAdapter(select7, c7);

            var commandBuilder37 = new SqlCommandBuilder(dataAdapter7);
            var ds7 = new DataSet();
            dataAdapter7.Fill(ds7);
            cboPostupak.DataSource = ds7.Tables[0];
            cboPostupak.DisplayMember = "Naziv";
            cboPostupak.ValueMember = "ID";

            var kd = "Select ID, Naziv as Naziv From KrajnjaDestinacija order by ID";
            var kdSAD = new SqlDataAdapter(kd, connection);
            var kdSDS = new DataSet();
            kdSAD.Fill(kdSDS);
            cboKrajnjaDestinacija.DataSource = kdSDS.Tables[0];
            cboKrajnjaDestinacija.DisplayMember = "Naziv";
            cboKrajnjaDestinacija.ValueMember = "ID";
            /*
            var dir4 = "Select ID,Naziv from InspekciskiTretman order by Naziv";
            var dirAD4 = new SqlDataAdapter(dir4, connection);
            var dirDS4 = new DataSet();
            dirAD4.Fill(dirDS4);
            cboInspekciskiTretman.DataSource = dirDS4.Tables[0];
            cboInspekciskiTretman.DisplayMember = "Naziv";
            cboInspekciskiTretman.ValueMember = "ID";
            */

            var car = "Select ID, Naziv From Carinarnice order by Naziv";
            var carAD = new SqlDataAdapter(car, connection);
            var carDS = new DataSet();
            carAD.Fill(carDS);
            cboCarina.DataSource = carDS.Tables[0];
            cboCarina.DisplayMember = "Naziv";
            cboCarina.ValueMember = "ID";


            var partner3 = "Select PaSifra,PaNaziv From Partnerji where Spediter =1 order by PaNaziv";
            var partAD3 = new SqlDataAdapter(partner3, connection);
            var partDS3 = new DataSet();
            partAD3.Fill(partDS3);
            cboSpedicija.DataSource = partDS3.Tables[0];
            cboSpedicija.DisplayMember = "PaNaziv";
            cboSpedicija.ValueMember = "PaSifra";

            var dir2 = "Select ID, (Oznaka + ' ' + Naziv) as Naziv from VrstaCarinskogPostupka order by Naziv";
            var dirAD2 = new SqlDataAdapter(dir2, connection);
            var dirDS2 = new DataSet();
            dirAD2.Fill(dirDS2);
            cboReexport.DataSource = dirDS2.Tables[0];
            cboReexport.DisplayMember = "Naziv";
            cboReexport.ValueMember = "ID";

            var adr = "Select ID, (Naziv + ' - ' + UNKod) as Naziv From VrstaRobeADR order by (UNKod + ' ' + Naziv)";
            var adrSAD = new SqlDataAdapter(adr, connection);
            var adrSDS = new DataSet();
            adrSAD.Fill(adrSDS);
            txtADR.DataSource = adrSDS.Tables[0];
            txtADR.DisplayMember = "Naziv";
            txtADR.ValueMember = "ID";

            var nalogodavac1 = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var nal1AD = new SqlDataAdapter(nalogodavac1, connection);
            var nal1DS = new DataSet();
            nal1AD.Fill(nal1DS);
            cboNalogodavac1.DataSource = nal1DS.Tables[0];
            cboNalogodavac1.DisplayMember = "PaNaziv";
            cboNalogodavac1.ValueMember = "PaSifra";

            var nalogodavac2 = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var nal2AD = new SqlDataAdapter(nalogodavac2, connection);
            var nal2DS = new DataSet();
            nal2AD.Fill(nal2DS);
            cboNalogodavac2.DataSource = nal2DS.Tables[0];
            cboNalogodavac2.DisplayMember = "PaNaziv";
            cboNalogodavac2.ValueMember = "PaSifra";

        }
        private void RefreshDataGridRN()
        {
            //PANTA DATAGRID


            var select = "SELECT RNOtpremaVoza.ID, RNOtpremaVoza.DatumRasporeda, RNOtpremaVoza.BrojKontejnera, RNOtpremaVoza.VrstaKontejnera," +
                " TipKontenjera.ID AS TKID, TipKontenjera.Naziv, RNOtpremaVoza.NalogIzdao, " +
                " RNOtpremaVoza.DatumRealizacije, RNOtpremaVoza.NaVoznoSredstvo, Voz.BrVoza, Voz.Relacija, RNOtpremaVoza.BrojPlombe, RNOtpremaVoza.BrojVagona, " +
                "RNOtpremaVoza.Zavrsen,   RNOtpremaVoza.IdUsluge, RNOtpremaVoza.NalogRealizovao, RNOtpremaVoza.Napomena, RNOtpremaVoza.OtpremaID, " +
                "RNOtpremaVoza.NalogID, RNOtpremaVoza.SaSkladista, Skladista.Naziv AS SkladisteNaziv FROM           RNOtpremaVoza INNER JOIN" +
                "  TipKontenjera ON RNOtpremaVoza.VrstaKontejnera = TipKontenjera.ID INNER JOIN   " +
                "  Voz ON RNOtpremaVoza.NaVoznoSredstvo = Voz.ID INNER JOIN  Skladista ON RNOtpremaVoza.SaSkladista = Skladista.ID " +
               " where OtpremaID = " + txtSifra.Text;

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = false;
            dataGridView2.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView2);

            //Panta refresh


            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 40;
            dataGridView2.Columns[0].Visible = false;

            /*
            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "RB";
            dataGridView1.Columns[1].Width = 30;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "NAdr";
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[2].Width = 30;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "KontID";
            dataGridView1.Columns[3].Width = 40;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Br kont";
            dataGridView1.Columns[4].Width = 110;
           */
        }


        private void VratiPodatkeSaIzvozKonacna()
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("  SELECT OtpremaKontejneraVozStavke.ID as ID, OtpremaKontejneraVozStavke.RB,  OtpremaKontejneraVozStavke.IDNadredjenog," +
                " OtpremaKontejneraVozStavke.KontejnerID, OtpremaKontejneraVozStavke.BrojKontejnera, OtpremaKontejneraVozStavke.BrojVagona as BV, TipKontenjera.Naziv AS TipKontejnera,  OtpremaKontejneraVozStavke.NalogID, " +
                "  IzvozKOnacna.VrstaKontejnera, Partnerji_1.PaSifra as Brodar, IzvozKOnacna.BookingBrodara, " +
                " IzvozKOnacna.BrodskaPlomba, "+
                " IzvozKonacna.OstalePlombe,  IzvozKOnacna.BrojKoletaO, IzvozKOnacna.BrutoRobe, "+
                " IzvozKOnacna.BrutoRobeO, IzvozKOnacna.CBMO, IzvozKOnacna.Tara, "+
                " IzvozKOnacna.NetoRobe, IzvozKOnacna.PeriodSkladistenjaOd,  IzvozKOnacna.PeriodSkladistenjaDo, Partnerji_2.PaSifra as Izvoznik, INSTret.ID as InspekciskiTretman, "+ 
                " IZvozKonacna.Spedicija,IzvozKonacna.KontaktSpeditera, IzvozKonacna.MestoPreuzimanja as PICKUPCNT,Carinarnice.ID as Carina, IzvozKonacna.ADR as ADR, " +
                " IzvozKonacna.NapomenaReexport as Reexport, IzvozKonacna.NapomenaZaRobu, IzvozKonacna.VGMBrod, IzvozKonacna.Klijent1, IzvozKonacna.Klijent2, IzvozKonacna.KrajnaDestinacija  FROM OtpremaKontejneraVozStavke " +
                " inner join IzvozKonacna on IzvozKonacna.ID = OtpremaKontejneraVozStavke.KontejnerID "+
                " INNER JOIN  Partnerji AS Partnerji_1 ON IzvozKonacna.Brodar = Partnerji_1.PaSifra  "+
                " INNER JOIN  Partnerji AS Partnerji_2 ON IzvozKonacna.Izvoznik = Partnerji_2.PaSifra "+
                " INNER JOIN  InspekciskiTretman AS INSTret ON IZvozKOnacna.Inspekcija = INSTret.ID  "+
                " INNER JOIN TipKontenjera ON OtpremaKontejneraVozStavke.TipKontejnera = TipKontenjera.ID  " +
                " INNER JOIN  Carinarnice ON IzvozKonacna.MestoCarinjenja = Carinarnice.ID  "+
                " INNER JOIN  Partnerji AS Partnerji_3 ON IzvozKonacna.Spedicija = Partnerji_3.PaSifra "+
                " inner join KontejnerskiTerminali on KontejnerskiTerminali.ID = IzvozKonacna.MestoPreuzimanja "+
                " inner join VrstaRobeADR on VrstaRobeADR.ID = IzvozKonacna.ADR " +
               "  inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = IzvozKonacna.NapomenaReexport  where OtpremaKontejneraVozStavke.Id  =  " + txtStavka.Text , con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {



                txtNalogID.Text = dr["NalogID"].ToString();
                txtStavka.Text = dr["ID"].ToString();
                txtRB.Text = dr["RB"].ToString();
                txtKOntejnerID.Text = dr["KontejnerID"].ToString();
                txtBrojKontejnera.Text = dr["BrojKontejnera"].ToString();
                cboTipKontejnera.SelectedValue = Convert.ToInt32(dr["VrstaKontejnera"].ToString());
              
                txtVagon.Text = dr["BV"].ToString(); ;
                cboVlasnikKontejnera.SelectedValue = Convert.ToInt32(dr["Brodar"].ToString());
                txtTara.Value = Convert.ToDecimal(dr["Tara"].ToString());
                txtBukingBrodar.Text = dr["BookingBrodara"].ToString();
                txtBrojPlombe.Text = dr["BrodskaPlomba"].ToString();
                txtBrojPlombe2.Text = dr["OstalePlombe"].ToString();
                cboIzvoznik.SelectedValue = Convert.ToInt32(dr["Izvoznik"].ToString());
                bttoRobeFaktura.Value = Convert.ToDecimal(dr["BrutoRobe"].ToString());
                txtTara.Value  = Convert.ToDecimal(dr["Tara"].ToString());
                //Napomena

                // cboPPCNT.SelectedValue = Convert.ToInt32(dr["PICKUPCNT"].ToString());
                cboCarina.SelectedValue = Convert.ToInt32(dr["Carina"].ToString());
                cboSpedicija.SelectedValue = Convert.ToInt32(dr["Spedicija"].ToString());
                txtKontaktSpeditera.Text = dr["KontaktSpeditera"].ToString();
                txtNapomenaZaRobu.Text = dr["NapomenaZaRobu"].ToString();
                cboReexport.SelectedValue = Convert.ToInt32(dr["Reexport"].ToString());
                txtADR.SelectedValue = Convert.ToInt32(dr["ADR"].ToString());
                //Ovde videti da li sam pogodio dobro polje VGMBrod ima i VGMBrod2

                cboNalogodavac1.SelectedValue = Convert.ToInt32(dr["Klijent1"].ToString());
                cboNalogodavac2.SelectedValue = Convert.ToInt32(dr["Klijent2"].ToString());
                cboKrajnjaDestinacija.SelectedValue = Convert.ToInt32(dr["KrajnaDestinacija"].ToString());
               // dtpEtaLeget.Value = Convert.ToDateTime(dr["EtaLeget"].ToString());
            }

            con.Close();

        }

        private void FillDGUsluge()
        {
            if (txtKOntejnerID.Text == "")
            { return; }
            var select = "";

            select = "select  IzvozKonacnaVrstaManipulacije.ID as ID, IzvozKonacnaVrstaManipulacije.IDNadredjena as KontejnerID," +
                " IzvozKonacna.BrojKontejnera," +
" IzvozKonacnaVrstaManipulacije.Kolicina,  VrstaManipulacije.ID as ManipulacijaID,VrstaManipulacije.Naziv as ManipulacijaNaziv, " +
" IzvozKonacnaVrstaManipulacije.Cena,OrganizacioneJedinice.ID,   OrganizacioneJedinice.Naziv as OrganizacionaJedinica,  " +
 "  Partnerji.PaSifra as NalogodavacID,PArtnerji.PaNaziv as Platilac, UvozniRNI" +
" from IzvozKonacnaVrstaManipulacije Inner    join VrstaManipulacije on VrstaManipulacije.ID = IzvozKonacnaVrstaManipulacije.IDVrstaManipulacije" +
" inner" +
" join PArtnerji on IzvozKonacnaVrstaManipulacije.Platilac = PArtnerji.PaSifra  inner " +
" join OrganizacioneJedinice on OrganizacioneJedinice.ID = IzvozKonacnaVrstaManipulacije.OrgJed " +
" inner " +
" join IzvozKonacna on IzvozKonacnaVrstaManipulacije.IDNadredjena = IzvozKonacna.ID where IzvozKonacna.ID = " + Convert.ToInt32(txtKOntejnerID.Text);



            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView8.ReadOnly = true;
            dataGridView8.DataSource = ds.Tables[0];


            PodesiDatagridView(dataGridView8);

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView8.Columns[0];
            dataGridView8.Columns[0].HeaderText = "ID";
            dataGridView8.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView8.Columns[1];
            dataGridView8.Columns[1].HeaderText = "IDU";
            dataGridView8.Columns[1].Width = 30;

            DataGridViewColumn column3 = dataGridView8.Columns[2];
            dataGridView8.Columns[2].HeaderText = "Kontejner";
            dataGridView8.Columns[2].Width = 50;

        }

        private void FillDGIK()
        {
            var select = "select IzvozKonacnaNapomenePozicioniranja.ID, IzvozKonacnaNapomenePozicioniranja.IDNapomene, PredefinisanePoruke.Naziv " +
" from IzvozKonacnaNapomenePozicioniranja inner join PredefinisanePoruke on PredefinisanePoruke.ID = IzvozKonacnaNapomenePozicioniranja.IDNapomene " +
" where IzvozKonacnaNapomenePozicioniranja.IdNadredjena = " + Convert.ToInt32(txtKOntejnerID.Text) + " order by IzvozKOnacnaNapomenePozicioniranja.ID desc ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView4.ReadOnly = true;
            dataGridView4.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView4);

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView4.Columns[0];
            dataGridView4.Columns[0].HeaderText = "ID";
            dataGridView4.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView4.Columns[1];
            dataGridView4.Columns[1].HeaderText = "NapomenaID";
            dataGridView4.Columns[1].Width = 20;

            DataGridViewColumn column3 = dataGridView4.Columns[2];
            dataGridView4.Columns[2].HeaderText = "Napomena";
            dataGridView4.Columns[2].Width = 160;

        }

        private void FillDG2()
        {

            var select = " SELECT     IzvozKonacnaNHM.ID, NHM.Broj, IzvozKonacnaNHM.IDNHM, NHM.Naziv FROM NHM INNER JOIN " +
                      " IzvozKonacnaNHM ON NHM.ID = IzvozKonacnaNHM.IDNHM where IzvozKonacnanhm.idnadredjena = " + Convert.ToInt32(txtKOntejnerID.Text) + " order by IzvozKonacnanhm.ID desc ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds.Tables[0];


            PodesiDatagridView(dataGridView3);

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView3.Columns[0];
            dataGridView3.Columns[0].HeaderText = "ID";
            dataGridView3.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView3.Columns[1];
            dataGridView3.Columns[1].HeaderText = "Broj";
            dataGridView3.Columns[1].Width = 80;

            DataGridViewColumn column3 = dataGridView3.Columns[2];
            dataGridView3.Columns[2].HeaderText = "ID";
            dataGridView3.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView3.Columns[3];
            dataGridView3.Columns[3].HeaderText = "NHM";
            dataGridView3.Columns[3].Width = 150;



        }

        private void VratiNaloge()
        {
        
        
        }

        private void txtKOntejnerID_TextChanged(object sender, EventArgs e)
        {
            VratiPodatkeSaIzvozKonacna();
            VratiNaloge();
            FillDGUsluge();
            FillDG2();
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void rasdniNalogOtpremaVozaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string IDUsluge = "0";
            foreach (DataGridViewRow row in dataGridView8.Rows)
            {

                if (row.Selected == true)
                {
                    IDUsluge = row.Cells[4].Value.ToString();

                }


            }
            RadniNalozi.RN2OtpremaVoza otp = new RadniNalozi.RN2OtpremaVoza(KorisnikCene, cboVozBuking.SelectedValue.ToString(), IDUsluge, txtSifra.Text);
            otp.Show();
        }

        private void pREGLEDIIPOSTAVKAKONTEJNERAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RadniNalozi.RN11PreglediPostavkaKontejnera pik = new RadniNalozi.RN11PreglediPostavkaKontejnera(KorisnikCene, Convert.ToInt32(cboVozBuking.SelectedValue), "0", txtSifra.Text);
            pik.Show();
        }

        private void dataGridView8_SelectionChanged(object sender, EventArgs e)
        {
            string IDUsluge = "0";
            foreach (DataGridViewRow row in dataGridView8.Rows)
            {

                if (row.Selected == true)
                {
                    IDUsluge = row.Cells[0].Value.ToString();
                    txtNalogID.Text = IDUsluge;

                }
            }
        }

        private void toolStripButton6_Click_1(object sender, EventArgs e)
        {
            Dokumenta.frmPovezivanjeKontejneraIVagona pkiv = new frmPovezivanjeKontejneraIVagona(txtSifra.Text);
            pkiv.Show();
        }
    }
}
