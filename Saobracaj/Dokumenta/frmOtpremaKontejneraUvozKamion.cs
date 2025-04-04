﻿using Syncfusion.Windows.Forms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace Saobracaj.Dokumenta
{
    public partial class frmOtpremaKontejneraUvozKamion : Form
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

        private void ChangeTextBoxPanel5()
        {


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;


                foreach (Control control in panel5.Controls)
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

        private void ChangeTextBoxPanel6()
        {


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;


                foreach (Control control in panel6.Controls)
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
        public frmOtpremaKontejneraUvozKamion()
        {
            InitializeComponent();
            ChangeTextBoxPanel1();
            ChangeTextBoxPanel5();
            ChangeTextBoxPanel6();
            ChangeTextBox();
        }
        public frmOtpremaKontejneraUvozKamion(string Korisnik)
        {
            InitializeComponent();
            ChangeTextBoxPanel1();
            ChangeTextBoxPanel5();
            ChangeTextBoxPanel6();
            ChangeTextBox();
            KorisnikCene = Korisnik;
        }

        public frmOtpremaKontejneraUvozKamion(string Korisnik, int Vozom)
        {
            InitializeComponent();
            KorisnikCene = Korisnik;
            ChangeTextBoxPanel1();
            ChangeTextBoxPanel5();
            ChangeTextBoxPanel6();
            ChangeTextBox();
            if (Vozom == 1)
            {
                chkVoz.Checked = true;
                txtRegBrKamiona.Enabled = false;
                txtImeVozaca.Enabled = false;
                cboVozBuking.Enabled = true;
                // toolStripButton3.Visible = false;

                // toolStripLabel1.Visible = true;
            }
            else
            {
                chkVoz.Checked = false;
                txtRegBrKamiona.Enabled = true;
                txtImeVozaca.Enabled = true;
                cboVozBuking.Enabled = false;
                //   toolStripButton3.Visible = false;
                //toolStripLabel1.Visible = false;

                dtpDatumOtpreme.Value = DateTime.Now;
                dtpVremeOdlaska.Value = DateTime.Now;
            }
        }


        public frmOtpremaKontejneraUvozKamion(int sifra, string Korisnik)
        {
            InitializeComponent();
            KorisnikCene = Korisnik;
            ChangeTextBoxPanel1();
            ChangeTextBoxPanel5();
            ChangeTextBoxPanel6();
            ChangeTextBox();
            txtSifra.Text = sifra.ToString();
            VratiPodatke(sifra);
            RefreshDataGrid2();
            if (chkVoz.Checked == true)
            {
                //  toolStripButton3.Visible = false;
                // toolStripLabel1.Visible = true;
            }
            else
            {
                //  toolStripButton3.Visible = true;
                // toolStripLabel1.Visible = false;
            }
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            int pomDirektni_indirektni = 0;

            if (chkUvoz.Checked == true)
            {
                pomDirektni_indirektni = 1;
            }
            else
            {
                pomDirektni_indirektni = 0;
            }


            int otpremaVozom = 0;
            if (chkVoz.Checked == true)
            {
                otpremaVozom = 1;
            }
            else
            { otpremaVozom = 0; };
            if (status == true)
            {

                Saobracaj.Dokumenta.InsertOtprema ins = new Saobracaj.Dokumenta.InsertOtprema();
                ins.InsertOtp(Convert.ToDateTime(dtpDatumOtpreme.Text), Convert.ToInt32(cboStatusOtpreme.SelectedIndex), Convert.ToInt32(cboVozBuking.SelectedValue), txtRegBrKamiona.Text, txtImeVozaca.Text, Convert.ToDateTime(dtpVremeOdlaska.Value), otpremaVozom, Convert.ToDateTime(DateTime.Now), KorisnikCene, txtNapomena.Text, Convert.ToInt32(cboPredefinisanePoruke.SelectedValue), 0, pomDirektni_indirektni, 0);
                status = false;
                VratiPodatkeMax();
            }
            else
            {
                //int TipCenovnika ,int Komitent, double Cena , int VrstaManipulacije ,DateTime  Datum , string Korisnik
                Saobracaj.Dokumenta.InsertOtprema upd = new Saobracaj.Dokumenta.InsertOtprema();
                upd.UpdOtpremaKontejnera(Convert.ToInt32(txtSifra.Text), Convert.ToDateTime(dtpDatumOtpreme.Text), Convert.ToInt32(cboStatusOtpreme.SelectedIndex), Convert.ToInt32(cboVozBuking.SelectedValue), txtRegBrKamiona.Text, txtImeVozaca.Text, Convert.ToDateTime(dtpVremeOdlaska.Value), otpremaVozom, Convert.ToDateTime(DateTime.Now), KorisnikCene, txtNapomena.Text, Convert.ToInt32(cboPredefinisanePoruke.SelectedValue), 0, pomDirektni_indirektni, 0);
                status = false;
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
                Saobracaj.Dokumenta.InsertOtpremaKontejneraStavke ins = new Saobracaj.Dokumenta.InsertOtpremaKontejneraStavke();
                ins.InsertOtpremaKontejneraStav(Convert.ToInt32(txtSifra.Text), txtBrojKontejnera.Text, txtVagon.Text, Convert.ToDouble(txtGranica.Value), Convert.ToDouble(txtBrojOsovina.Value), Convert.ToDouble(txtSopstvenaMasa.Value), Convert.ToDouble(txtTara.Value), Convert.ToDouble(txtNeto.Value), Convert.ToInt32(cboPosiljalac.SelectedValue), Convert.ToInt32(cboPrimalac.SelectedValue), Convert.ToInt32(cboVlasnikKontejnera.SelectedValue), Convert.ToInt32(cboTipKontejnera.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue), txtBukingBrodar.Text, Convert.ToInt32(cboStatusKontejnera.SelectedValue), txtBrojPlombe.Text, Convert.ToInt32(txtPlaniraniLager.Text), 0, Convert.ToDateTime(dtpVremePripremljen.Value), Convert.ToDateTime(dtpVremeOdlaska.Value), Convert.ToDateTime(DateTime.Now), KorisnikCene, txtBrojPlombe2.Text, Convert.ToInt32(cboOrganizator.SelectedValue), txtNapomenaS.Text, Convert.ToDateTime(dtpPerodSkladistenjaOd.Value), Convert.ToDateTime(dtpPeriodSkladistenjaDo.Value), Convert.ToDouble(bttoRobe.Value), Convert.ToInt32(txtKontejnerID.Text), Convert.ToDouble(bttoKontejnera.Value), txtNapomenaS2.Text, Convert.ToInt32(cbPostupak.SelectedValue), 0, 0, 0, 0, "", "", "", Convert.ToInt32(txtNalogID.Text));
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

        private void tsDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da želite da brišete?", "Izbor", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                Saobracaj.Dokumenta.InsertOtprema upd = new Saobracaj.Dokumenta.InsertOtprema();
                upd.DeleteOtpremaKontejnera(Convert.ToInt32(txtSifra.Text));
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void RefreshDataGrid()
        {

            var select = " SELECT         OtpremaKontejneraVozStavke.ID, OtpremaKontejneraVozStavke.RB, " +
" OtpremaKontejneraVozStavke.IDNadredjenog,   OtpremaKontejneraVozStavke.KontejnerID, " +
" OtpremaKontejneraVozStavke.BrojKontejnera,  TipKontenjera.Naziv AS TipKontejnera, " +
" UvozKOnacna.TaraKontejnera," +
" UvozKonacna.NetoRobe," +
" UvozKonacna.BrutoKontejnera," +
" UvozKonacna.BrutoRobe," +
" Partnerji_4.PANaziv as Brodar, UvozKonacna.BukingBrodara AS BukingBrodar,OtpremaKontejneraVozStavke.BrojPlombe, " +
" OtpremaKontejneraVozStavke.BrojPlombe2, OtpremaKontejneraVozStavke.PeriodSkladistenjaOd, " +
" OtpremaKontejneraVozStavke.PeriodSkladistenjaDo, DirigacijaKOntejneraZa.Naziv as DirigacijaKOntejneraZa, " +
" Partnerji_2.PaNaziv AS Nalogodavac_Za_DrumskiPrevoz, Partnerji_3.PaNaziv AS Nalogodavac_Za_Usluge, " +
"   OtpremaKontejneraVozStavke.PlaniraniLager as DIREKTNI_INDIREKTNI,    " +
" OtpremaKontejneraVozStavke.Datum, OtpremaKontejneraVozStavke.Korisnik,  UvozKonacna.NacinPakovanja, " +
" UvozKonacna.Napomena1, UvozKonacna.Napomena, UvozKOnacna.BrojPlombe1, UvozKonacna.BrojPlombe2" +
" FROM  OtpremaKontejneraVozStavke" +
" inner join UvozKonacna on UvozKonacna.ID = OtpremaKontejneraVozStavke.KontejnerID" +
" INNER JOIN  Partnerji AS Partnerji_1 ON UvozKonacna.Nalogodavac1 = Partnerji_1.PaSifra" +
" INNER JOIN  Partnerji AS Partnerji_2 ON UvozKonacna.Nalogodavac2 = Partnerji_2.PaSifra" +
" INNER JOIN  Partnerji AS Partnerji_3 ON UvozKonacna.Nalogodavac3 = Partnerji_3.PaSifra" +
" INNER JOIN  Partnerji AS Partnerji_4 ON UvozKonacna.Brodar = Partnerji_4.PaSifra" +
" INNER JOIN TipKontenjera ON OtpremaKontejneraVozStavke.TipKontejnera = TipKontenjera.ID" +
" Inner join DirigacijaKontejneraZa on DirigacijaKontejneraZa.ID = UvozKonacna.PostupakSaRobom" +
" inner join uvNacinPakovanja on UvozKonacna.NacinPakovanja = uvNacinPakovanja.ID" +
   " where OtpremaKontejneraVozStavke.IdNadredjenog = " + txtSifra.Text + " order by RB";

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
          


        }


        private void RefreshDataGrid2()
        {

            var select = "  Select OtpremaKontejneraVozStavke.ID, OtpremaKontejneraVozStavke.RB,  OtpremaKontejneraVozStavke.IDNadredjenog,  " +
 " OtpremaKontejneraVozStavke.KontejnerID,  OtpremaKontejneraVozStavke.BrojKontejnera,  " +
 " TipKontenjera.Naziv AS TipKontejnera,  UvozKOnacna.TaraKontejnera, UvozKonacna.NetoRobe, " +
 " UvozKonacna.BrutoKontejnera, UvozKonacna.BrutoRobe, Partnerji_4.PANaziv as Brodar, " +
 " UvozKonacna.BukingBrodara AS BukingBrodar,OtpremaKontejneraVozStavke.BrojPlombe, " +
 " OtpremaKontejneraVozStavke.BrojPlombe2, OtpremaKontejneraVozStavke.PeriodSkladistenjaOd,  " +
 " OtpremaKontejneraVozStavke.PeriodSkladistenjaDo, DirigacijaKOntejneraZa.Naziv as DirigacijaKOntejneraZa,  " +
 " Partnerji_2.PaNaziv AS Nalogodavac_Za_DrumskiPrevoz, Partnerji_3.PaNaziv AS Nalogodavac_Za_Usluge,   " +
 " OtpremaKontejneraVozStavke.PlaniraniLager as DIREKTNI_INDIREKTNI, " +
 " OtpremaKontejneraVozStavke.Datum, OtpremaKontejneraVozStavke.Korisnik, " +
 " UvozKonacna.NacinPakovanja,  UvozKonacna.Napomena1, UvozKonacna.Napomena, " +
 " UvozKOnacna.BrojPlombe1, UvozKonacna.BrojPlombe2" +
 " FROM  OtpremaKontejneraVozStavke" +
 " inner join UvozKonacna on UvozKonacna.ID = OtpremaKontejneraVozStavke.KontejnerID" +
 " INNER JOIN  Partnerji AS Partnerji_1 ON UvozKonacna.Nalogodavac1 = Partnerji_1.PaSifra" +
 " INNER JOIN  Partnerji AS Partnerji_2 ON UvozKonacna.Nalogodavac2 = Partnerji_2.PaSifra" +
 " INNER JOIN  Partnerji AS Partnerji_3 ON UvozKonacna.Nalogodavac3 = Partnerji_3.PaSifra" +
"  INNER JOIN  Partnerji AS Partnerji_4 ON UvozKonacna.Brodar = Partnerji_4.PaSifra" +
 " INNER JOIN TipKontenjera ON OtpremaKontejneraVozStavke.TipKontejnera = TipKontenjera.ID" +
 " Inner join DirigacijaKontejneraZa on DirigacijaKontejneraZa.ID = UvozKonacna.DirigacijaKontejeraZa" +
 " inner join uvNacinPakovanja on UvozKonacna.NacinPakovanja = uvNacinPakovanja.ID" +
 " where OtpremaKontejneraVozStavke.IdNadredjenog = " + txtSifra.Text + " order by RB";

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

            /*

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


            */




        }
        private void VratiPodatke(int ID)
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            // SqlCommand cmd = new SqlCommand("select [ID] ,[DatumOtpreme],[StatusOtpreme],[IdVoza],[VremeOdlaska], [RegBrKamiona], [ImeVozaca], NacinOtpreme, Napomena, NajavaEmail, OtpremaEmail, Zatvoren, CIRUradjen, PredefinisanePorukeID from OtpremaKontejnera where ID = " + ID, con);

            SqlCommand cmd = new SqlCommand("select [ID] ,[DatumOtpreme],[StatusOtpreme],[IdVoza],[VremeOdlaska], [RegBrKamiona], [ImeVozaca], NacinOtpreme, Napomena, NajavaEmail, OtpremaEmail, Zatvoren, CIRUradjen, VrstaKamiona, Poreklo from OtpremaKontejnera where ID = " + ID, con);

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

                if (Convert.ToInt32(dr["VrstaKamiona"].ToString()) == 0)
                {
                    chkPlatforma.Checked = true;
                    chkCirada.Checked = false;

                }
                else
                {
                    chkPlatforma.Checked = false;
                    chkCirada.Checked = true;
                }

                if (Convert.ToInt32(dr["Poreklo"].ToString()) == 1)
                {
                    chkUvoz.Checked = true;
                    chkIzvoz.Checked = false;
                    chkTerminal.Checked = false;

                }
                else if (Convert.ToInt32(dr["Poreklo"].ToString()) == 2)
                {
                    chkUvoz.Checked = false;
                    chkIzvoz.Checked = true;
                    chkTerminal.Checked = false;
                }

                else if (Convert.ToInt32(dr["Poreklo"].ToString()) == 4)
                {
                    chkUvoz.Checked = false;
                    chkIzvoz.Checked = false;
                    chkTerminal.Checked = true;
                }

            }

            con.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Saobracaj.Dokumenta.InsertOtpremaKontejneraStavke ins = new Saobracaj.Dokumenta.InsertOtpremaKontejneraStavke();
            ins.UpdOtpremaKontejneraVozStav(Convert.ToInt32(txtStavka.Text), Convert.ToInt32(txtSifra.Text), txtBrojKontejnera.Text, txtVagon.Text, Convert.ToDouble(txtGranica.Value), Convert.ToDouble(txtBrojOsovina.Value), Convert.ToDouble(txtSopstvenaMasa.Value), Convert.ToDouble(txtTara.Value), Convert.ToDouble(txtNeto.Value), Convert.ToInt32(cboPosiljalac.SelectedValue), Convert.ToInt32(cboPrimalac.SelectedValue), Convert.ToInt32(cboVlasnikKontejnera.SelectedValue), Convert.ToInt32(cboTipKontejnera.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue), txtBukingBrodar.Text, Convert.ToInt32(cboStatusKontejnera.SelectedValue), txtBrojPlombe.Text, Convert.ToInt32(txtPlaniraniLager.Text), 0, Convert.ToDateTime(dtpVremePripremljen.Value), Convert.ToDateTime(dtpVremeOdlaska.Value), Convert.ToDateTime(DateTime.Now), KorisnikCene, Convert.ToInt32(txtRB.Text), txtBrojPlombe2.Text, Convert.ToInt32(cboOrganizator.SelectedValue), txtNapomenaS.Text, Convert.ToDateTime(dtpPerodSkladistenjaOd.Value), Convert.ToDateTime(dtpPeriodSkladistenjaDo.Value), Convert.ToDouble(bttoRobe.Value), Convert.ToInt32(txtKontejnerID.Text), Convert.ToDouble(bttoKontejnera.Value), txtNapomenaS2.Text, Convert.ToInt32(cbPostupak.SelectedValue), 0, 0, 0, 0, "", "", "");
            RefreshDataGrid2();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da želite da brišete?", "Izbor", MessageBoxButtons.YesNo);

            Saobracaj.Dokumenta.InsertOtpremaKontejneraStavke dels = new Saobracaj.Dokumenta.InsertOtpremaKontejneraStavke();


            if (dialogResult == DialogResult.Yes)
            {
                dels.DeleteOtpremaKontejneraVozStav(Convert.ToInt32(txtStavka.Text));
                RefreshDataGrid2();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }

            // InsertOtpremaKontejneraStavke dels = new InsertOtpremaKontejneraStavke();
            // dels.DeleteOtpremaKontejneraVozStav(Convert.ToInt32(txtStavka.Text));
            // RefreshDataGrid2();
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

        private void frmOtpremaKontejneraUvozKamion_Load(object sender, EventArgs e)
        {
            var select = " Select Distinct ID, (NKM + '-' + Naziv) as NKM  From VrstaRobe";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboVrstaRobe.DataSource = ds.Tables[0];
            cboVrstaRobe.DisplayMember = "NKM";
            cboVrstaRobe.ValueMember = "ID";


            var select1 = " Select Distinct PaSifra, PaNaziv From Partnerji  order by PaNaziv";
            var s_connection1 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection1 = new SqlConnection(s_connection1);
            var c1 = new SqlConnection(s_connection1);
            var dataAdapter1 = new SqlDataAdapter(select1, c1);

            var commandBuilder1 = new SqlCommandBuilder(dataAdapter1);
            var ds1 = new DataSet();
            dataAdapter1.Fill(ds1);
            cboPosiljalac.DataSource = ds1.Tables[0];
            cboPosiljalac.DisplayMember = "PaNaziv";
            cboPosiljalac.ValueMember = "PaSifra";

            var select2 = " Select Distinct PaSifra, PaNaziv From Partnerji  order by PaNaziv";
            var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboPrimalac.DataSource = ds2.Tables[0];
            cboPrimalac.DisplayMember = "PaNaziv";
            cboPrimalac.ValueMember = "PaSifra";

            var select3 = " Select Distinct PaSifra, PaNaziv From Partnerji  order by PaNaziv";
            var s_connection3 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboVlasnikKontejnera.DataSource = ds3.Tables[0];
            cboVlasnikKontejnera.DisplayMember = "PaNaziv";
            cboVlasnikKontejnera.ValueMember = "PaSifra";


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
            /*
            var select5 = " Select Distinct ID, (Cast(id as nvarchar(5)) + '-' + Cast(BrVoza as nvarchar(6)) + '-' + Cast(Relacija as nvarchar(100)) + '- '  + Cast(Convert(nvarchar(10),VremeDolaskaO,105) as Nvarchar(10))) as Naziv  From Voz where dolazeci = 0 ";
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
            */
            var select6 = " Select Distinct ID,  Naziv From DirigacijaKontejneraZa ";
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
            /*
            var select7 = "  Select Distinct ID, (Cast(IDVoza as nvarchar(6)) + ' ' + StanicaOtpreme + ' ' + Cast(Convert(nvarchar(10),DatumOtpreme,105) as Nvarchar(10))) as Naziv  From BukingVoza ";
            var s_connection7 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection7 = new SqlConnection(s_connection7);
            var c7 = new SqlConnection(s_connection7);
            var dataAdapter7 = new SqlDataAdapter(select7, c7);

            var commandBuilder7 = new SqlCommandBuilder(dataAdapter7);
            var ds7 = new DataSet();
            dataAdapter7.Fill(ds7);
            cboBukingOtpreme.DataSource = ds7.Tables[0];
            cboBukingOtpreme.DisplayMember = "IdVoza";
            cboBukingOtpreme.ValueMember = "ID";
            */
            var select9 = " Select Distinct PaSifra, PaNaziv From Partnerji  order by PaNaziv";
            var s_connection9 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection9 = new SqlConnection(s_connection9);
            var c9 = new SqlConnection(s_connection9);
            var dataAdapter9 = new SqlDataAdapter(select9, c9);

            var commandBuilder9 = new SqlCommandBuilder(dataAdapter9);
            var ds9 = new DataSet();
            dataAdapter9.Fill(ds9);
            cboOrganizator.DataSource = ds9.Tables[0];
            cboOrganizator.DisplayMember = "PaNaziv";
            cboOrganizator.ValueMember = "PaSifra";
            /*
            var select10 = " Select Distinct ID, Naziv  From PredefinisanePoruke";
            var s_connection10 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection10 = new SqlConnection(s_connection10);
            var c10 = new SqlConnection(s_connection10);
            var dataAdapter10 = new SqlDataAdapter(select10, c10);

            var commandBuilder10 = new SqlCommandBuilder(dataAdapter10);
            var ds10 = new DataSet();
            dataAdapter10.Fill(ds10);
            cboPredefinisanePoruke.DataSource = ds10.Tables[0];
            cboPredefinisanePoruke.DisplayMember = "Naziv";
            cboPredefinisanePoruke.ValueMember = "ID";
             */
            var select11 = " Select ID,Naziv From uvNacinPakovanja";
            var s_connection11 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection11 = new SqlConnection(s_connection11);
            var c11 = new SqlConnection(s_connection11);
            var dataAdapter11 = new SqlDataAdapter(select11, c11);

            var commandBuilder11 = new SqlCommandBuilder(dataAdapter11);
            var ds11 = new DataSet();
            dataAdapter11.Fill(ds11);
            cbPostupak.DataSource = ds11.Tables[0];
            cbPostupak.DisplayMember = "Naziv";
            cbPostupak.ValueMember = "ID";

            var partner2 = "Select PaSifra,PaNaziv From Partnerji  order by PaSifra";
            var partAD2 = new SqlDataAdapter(partner2, s_connection11);
            var partDS2 = new DataSet();
            partAD2.Fill(partDS2);
            cboUvoznik.DataSource = partDS2.Tables[0];
            cboUvoznik.DisplayMember = "PaNaziv";
            cboUvoznik.ValueMember = "PaSifra";

            var partner4 = "Select PaSifra,PaNaziv From Partnerji  order by PaSifra";
            var partAD4 = new SqlDataAdapter(partner4, s_connection11);
            var partDS4 = new DataSet();
            partAD4.Fill(partDS4);
            cboSpedicijaLeget.DataSource = partDS4.Tables[0];
            cboSpedicijaLeget.DisplayMember = "PaNaziv";
            cboSpedicijaLeget.ValueMember = "PaSifra";

            usao = 1;
        }

        private void VratiPodatkeStavkeKontejnerSaOtpremnice(string BrojKontejnera)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT         OtpremaKontejneraVozStavke.ID, OtpremaKontejneraVozStavke.RB, " +
" OtpremaKontejneraVozStavke.IDNadredjenog, OtpremaKontejneraVozStavke.KontejnerID," +
" OtpremaKontejneraVozStavke.BrojKontejnera, TipKontenjera.ID AS TipKontejnera, " +
" UvozKOnacna.TaraKontejnera, " +
" UvozKonacna.NetoRobe, " +
" UvozKonacna.BrutoKontejnera, " +
" UvozKonacna.BrutoRobe, " +
" Partnerji_4.PaSifra as Brodar, UvozKonacna.BukingBrodara AS BukingBrodar, " +
" Partnerji_1.PaSifra as NalogodavacZaVoz, " +
" Partnerji_2.PaSifra AS Nalogodavac_Za_DrumskiPrevoz, Partnerji_3.PaSifra AS Nalogodavac_Za_Usluge, " +
"   OtpremaKontejneraVozStavke.PlaniraniLager as DIREKTNI_INDIREKTNI, " +
" OtpremaKontejneraVozStavke.Datum, OtpremaKontejneraVozStavke.Korisnik, UvozKonacna.NacinPakovanja as NacinPakovanja, " +
" UvozKonacna.Napomena1, UvozKonacna.Napomena, UvozKOnacna.BrojPlombe1, UvozKonacna.BrojPlombe2, " +
" DirigacijaKOntejneraZa.ID as DirigacijaKOntejneraZa, NalogID, RLTerminali, ADR, UvozKonacna.Uvoznik, UvozKonacna.SpedicijaRTC" +
" , UvozKonacna.CarinskiPostupak, UvozKonacna.NacinPakovanja,  UvozKonacna.Koleta, UvozKonacna.KoletaTer, UvozKonacna.Napomena1   " +
" FROM  OtpremaKontejneraVozStavke " +
" inner join UvozKonacna on UvozKonacna.ID = OtpremaKontejneraVozStavke.KontejnerID " +
" INNER JOIN  Partnerji AS Partnerji_1 ON UvozKonacna.Nalogodavac1 = Partnerji_1.PaSifra " +
" INNER JOIN  Partnerji AS Partnerji_2 ON UvozKonacna.Nalogodavac2 = Partnerji_2.PaSifra " +
" INNER JOIN  Partnerji AS Partnerji_3 ON UvozKonacna.Nalogodavac3 = Partnerji_3.PaSifra " +
" INNER JOIN  Partnerji AS Partnerji_4 ON UvozKonacna.Brodar = Partnerji_4.PaSifra " +
" INNER JOIN TipKontenjera ON OtpremaKontejneraVozStavke.TipKontejnera = TipKontenjera.ID " +
" Inner join DirigacijaKontejneraZa on DirigacijaKontejneraZa.ID = UvozKonacna.DirigacijaKontejeraZa" +
" inner join uvNacinPakovanja on UvozKonacna.NacinPakovanja = uvNacinPakovanja.ID  where OtpremaKontejneraVozStavke.KontejnerID = " + txtKontejnerID.Text +
 " and UvozKonacna.ID = " + BrojKontejnera, con);


            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                cboTipKontejnera.SelectedValue = Convert.ToInt32(dr["TipKontejnera"].ToString());
                txtTara.Value = Convert.ToDecimal(dr["TaraKontejnera"].ToString());
                txtNeto.Value = Convert.ToDecimal(dr["NetoRobe"].ToString());
                bttoRobe.Value = Convert.ToDecimal(dr["BrutoRobe"].ToString());
                bttoKontejnera.Value = Convert.ToDecimal(dr["BrutoKontejnera"].ToString());
                cboVlasnikKontejnera.SelectedValue = Convert.ToInt32(dr["Brodar"].ToString());
                txtBukingBrodar.Text = dr["BukingBrodar"].ToString();
                cboPosiljalac.SelectedValue = Convert.ToInt32(dr["NalogodavacZaVoz"].ToString());
                cboPrimalac.SelectedValue = Convert.ToInt32(dr["Nalogodavac_Za_DrumskiPrevoz"].ToString());
                cboOrganizator.SelectedValue = Convert.ToInt32(dr["Nalogodavac_Za_Usluge"].ToString());
                txtNapomenaS.Text = dr["Napomena1"].ToString();
                txtNapomenaS2.Text = dr["Napomena"].ToString();
                cboStatusKontejnera.SelectedValue = Convert.ToInt32(dr["DirigacijaKOntejneraZa"].ToString());
                cbPostupak.SelectedValue = Convert.ToInt32(dr["NacinPakovanja"].ToString());
                txtNalogID.Text = dr["NalogID"].ToString();
                /*
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
                               */
                txtBrojPlombe.Text = dr["BrojPlombe1"].ToString();
                txtBrojPlombe2.Text = dr["BrojPlombe2"].ToString();
                cboUvoznik.SelectedValue = Convert.ToInt32(dr["Uvoznik"].ToString());
                cboSpedicijaLeget.SelectedValue = Convert.ToInt32(dr["SpedicijaRTC"].ToString());
                if (txtNapomenaS.Text == "1")
                {
                    chkSlobodan.Checked = true;
                }

                //CIRADA UVOZ DODATA POLJA PRIJEM
              
                cboCarinskiPostupak.SelectedValue = Convert.ToInt32(dr["CarinskiPostupak"].ToString());
                cbNacinPakovanja.SelectedValue = Convert.ToInt32(dr["NacinPakovanja"].ToString());
                txtKoleta.Value = Convert.ToDecimal(dr["Koleta"].ToString());
                txtKoletaTer.Value = Convert.ToDecimal(dr["KoletaTer"].ToString());
              



            }

            con.Close();
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

            SqlCommand cmd = new SqlCommand(" SELECT [ID],[IDNadredjenog] ,[BrojKontejnera], KontejnerID, NalogID " +
             " FROM [dbo].[OtpremaKontejneraVozStavke] " +
             " where IdNadredjenog = " + txtSifra.Text + " and RB = " + RB, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtStavka.Text = dr["ID"].ToString();
                txtRB.Text = RB.ToString();
                txtBrojKontejnera.Text = dr["BrojKontejnera"].ToString();
                txtKontejnerID.Text = dr["KOntejnerID"].ToString();
                txtNalogID.Text = dr["NalogID"].ToString();

            }

            con.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            /*
            //Zatvaranje kontejnera
            //
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                Saobracaj.Dokumenta.InsertPromet ins = new Saobracaj.Dokumenta.InsertPromet();


                ins.UpdateZatvorenOtprema(row.Cells[1].Value.ToString(), Convert.ToDateTime(dtpVremeOdlaska.Value), Convert.ToInt32(txtSifra.Text));
            }
            */
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Saobracaj.Dokumenta.frmDokumentaOtpremaKontejnera dokotp = new Saobracaj.Dokumenta.frmDokumentaOtpremaKontejnera(txtSifra.Text, KorisnikCene);
            dokotp.Show();
        }

        private void VratiVremeVoza()
        {
            if (status == true)
            {




                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection con = new SqlConnection(s_connection);

                con.Open();

                SqlCommand cmd = new SqlCommand("select VremePolaskaO from Voz where ID = " + Convert.ToInt32(cboVozBuking.SelectedValue), con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    dtpDatumOtpreme.Value = Convert.ToDateTime(dr["VremePolaskaO"].ToString());
                    dtpVremeOdlaska.Value = Convert.ToDateTime(dr["VremePolaskaO"].ToString());
                }

                con.Close();

            }

        }

        private void cboStatusOtpreme_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboStatusOtpreme.Text == "2-Otpremljen" && usao == 1)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    dtpVremeOdlaska.Value = DateTime.Now;
                    Saobracaj.Dokumenta.InsertPromet ins = new Saobracaj.Dokumenta.InsertPromet();
                    ins.UpdateZatvorenOtprema(row.Cells[3].Value.ToString(), Convert.ToDateTime(dtpVremeOdlaska.Value), Convert.ToInt32(txtSifra.Text));
                }
            }
        }
        int VratiPostojiUslugaDrumskogPrevoza(string KontejnerID)
        {
           

                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection con = new SqlConnection(s_connection);

                con.Open();

                SqlCommand cmd = new SqlCommand("select Count(*) as Broj from UvozKonacnaVrstaManipulacije inner join VrstaManipulacije on VrstaManipulacije.ID = UvozKonacnaVrstaManipulacije.IDVrstaManipulacije where UvozKonacnaVrstaManipulacije.IDNadredjena = "+ KontejnerID +" and VrstaManipulacije.Drumska = 1 " , con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    if (Convert.ToInt32(dr["Broj"].ToString()) > 0) 
                    {
                    chkDrumski.Checked = true;
                    }
                    else
                    {
                        chkDrumski.Checked = false;
                    }
                    
                }

                con.Close();

          


            return 0; 
        }

        private void RefreshDataGridRNCirade()
        {
            var select = "";
            //PANTA DATAGRID
            if (chkCirada.Checked == true)
            {
           select =
" select * from RNOtpremaCirade " +
             " where NalogID = " + txtNalogID.Text;
            }
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

        private void RefreshDataGridRNPlatforme()
        {
            var select = "";
            //PANTA DATAGRID
            if (chkPlatforma.Checked == true)
            {
                select =
     " select * from RNOtpremaPlatforme " +
                  " where NalogID = " + txtNalogID.Text;
            }
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
                       int l = VratiPostojiUslugaDrumskogPrevoza(txtKontejnerID.Text);
                        if (chkCirada.Checked == true)
                        {
                            RefreshDataGridRNCirade();
                        }
                        if (chkPlatforma.Checked == true)
                        {
                            RefreshDataGridRNPlatforme();
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }


        string VratiPodatkeEmailPrimalac()
        {
            string emailovi = "";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT email from Komitenti " +
             " where Id = " + Convert.ToInt32(cboPrimalac.SelectedValue), con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                emailovi = dr["email"].ToString();

            }

            con.Close();
            return emailovi;
        }

        string VratiPodatkeEmailPosiljalac()
        {
            string emailovi = "";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT email from Komitenti " +
             " where Id = " + Convert.ToInt32(cboPosiljalac.SelectedValue), con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                emailovi = dr["email"].ToString();

            }

            con.Close();
            return emailovi;
        }

        string VratiPodatkeEmailOrganizator()
        {
            string emailovi = "";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT email from Komitenti " +
             " where Id = " + Convert.ToInt32(cboOrganizator.SelectedValue), con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                emailovi = dr["email"].ToString();

            }

            con.Close();
            return emailovi;
        }

        string VratiPodatkeEmailVlasnikKontejnera()
        {
            string emailovi = "";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT email from Komitenti " +
             " where Id = " + Convert.ToInt32(cboVlasnikKontejnera.SelectedValue), con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                emailovi = dr["email"].ToString();

            }

            con.Close();
            return emailovi;
        }

        private void PosaljiMailOdjavaPrimalac(string Kome)
        {
            try
            {
                string zadnjibroj = txtSifra.Text;
                //drugi je kome
                var select2 = " SELECT Distinct Primalac"
                  + " FROM [dbo].[OtpremaKontejneraVozStavke] where IDNadredjenog =  " + Convert.ToInt32(txtSifra.Text);

                var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection myConnection2 = new SqlConnection(s_connection2);
                var c2 = new SqlConnection(s_connection2);
                var dataAdapter2 = new SqlDataAdapter(select2, c2);

                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                foreach (DataRow myRow2 in ds2.Tables[0].Rows)
                {

                    mailMessage = new MailMessage("logistika@zitbgd.rs", Kome);

                    mailMessage.Subject = "Leget najava. Br. voza: " + zadnjibroj + " . ";

                    var select = "  SELECT OtpremaKontejneraVozStavke.[ID] , Voz.BrVoza, BrojKontejnera, BrojVagona, Tara, Neto, Voz.Relacija, Voz.VremePolaskaO, Voz.VremeDolaskaO, " +
                    " OtpremaKontejneraVozStavke.Primalac,  OtpremaKontejneraVozStavke.Posiljalac, OtpremaKontejneraVozStavke.VlasnikKontejnera, OtpremaKontejneraVozStavke.Organizator " +
                " FROM [dbo].[OtpremaKontejneraVozStavke] " +
                " inner join OtpremaKontejnera on OtpremaKontejneraVozStavke.IDNadredjenog = OtpremaKontejnera.Id " +
                " inner join Voz on OtpremaKontejnera.IdVoza = Voz.Id " +
                " inner join Stanice on Voz.StanicaOd = Stanice.Id " +
                " where IDNadredjenog = " + Convert.ToInt32(txtSifra.Text) + " and Primalac =  " + myRow2["Primalac"].ToString();


                    var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    string body = "NAJAVA ODLASKA KONTEJNERA VOZOM :  <br /><br />";
                    body = body + " Poštovani,    <br /> <br /> <br />";


                    int countS = 0;
                    foreach (DataRow myRow in ds.Tables[0].Rows)
                    {
                        if (countS == 0)
                        {
                            body = body + " Očekivani odlazak je na relaciji :" + myRow["Relacija"].ToString() + "  vozom br.: " + myRow["BrVoza"].ToString() + "<br />";
                            body = body + " vreme polaska:  " + myRow["VremePolaskaO"].ToString() + "   vreme dolaska: " + myRow["VremeDolaskaO"].ToString() + "<br /><br /> ";

                            body = body + " Prijavljujemo najavu odlaska kontejnera:  <br />";
                            body = body + "Broj kontejnera: <br />";
                        }
                        countS = countS + 1;
                        body = body + myRow["BrojKontejnera"].ToString() + ";";
                        //  body = body + "Broj vagona: " + myRow["BrojVagona"].ToString() + "<br />";
                        //  body = body + "Tara: " + myRow["Tara"].ToString() + "<br />";
                        //  body = body + "Neto: " + myRow["Neto"].ToString() + "<br />";
                        //

                    }

                    //  body = body + "<br /> Po izvršenom dolasku voza i prijemu kontejnera na terminal Leget-a u Beograd ranžirnoj obavestićemo Vas blagovremeno na e-mailom.   <br />";
                    body = body + "RTC LUKA LEGET <br />";
                    body = body + "Tel: <br />";
                    body = body + "Email: <br />";


                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.zitbgd.rs";

                    smtpClient.Port = 25;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential("logistika@zitbgd.rs", "zit2019log");

                    smtpClient.EnableSsl = false;
                    smtpClient.Send(mailMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void PosaljiMailOdjavaPrimalacOtprema(string Kome)
        {
            try
            {
                string zadnjibroj = txtSifra.Text;
                //drugi je kome
                var select2 = " SELECT Distinct Primalac"
                  + " FROM [dbo].[OtpremaKontejneraVozStavke] where IDNadredjenog =  " + Convert.ToInt32(txtSifra.Text);

                var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection myConnection2 = new SqlConnection(s_connection2);
                var c2 = new SqlConnection(s_connection2);
                var dataAdapter2 = new SqlDataAdapter(select2, c2);

                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                foreach (DataRow myRow2 in ds2.Tables[0].Rows)
                {

                    mailMessage = new MailMessage("logistika@zitbgd.rs", Kome);

                    mailMessage.Subject = "Leget pristigla najava. Br. voza: " + zadnjibroj + " . ";

                    var select = "  SELECT OtpremaKontejneraVozStavke.[ID] , Voz.BrVoza, BrojKontejnera, BrojVagona, Tara, Neto, Voz.Relacija, Voz.VremePolaskaO, Voz.VremeDolaskaO, " +
                    " OtpremaKontejneraVozStavke.Primalac,  OtpremaKontejneraVozStavke.Posiljalac, OtpremaKontejneraVozStavke.VlasnikKontejnera, OtpremaKontejneraVozStavke.Organizator " +
                " FROM [dbo].[OtpremaKontejneraVozStavke] " +
                " inner join OtpremaKontejnera on OtpremaKontejneraVozStavke.IDNadredjenog = OtpremaKontejnera.Id " +
                " inner join Voz on OtpremaKontejnera.IdVoza = Voz.Id " +
                " inner join Stanice on Voz.StanicaOd = Stanice.Id " +
                " where IDNadredjenog = " + Convert.ToInt32(txtSifra.Text) + " and Primalac =  " + myRow2["Primalac"].ToString();


                    var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    string body = "OTPREMA KONTEJNERA VOZOM :  <br /><br />";
                    body = body + " Poštovani,    <br /> <br /> <br />";


                    int countS = 0;
                    foreach (DataRow myRow in ds.Tables[0].Rows)
                    {
                        if (countS == 0)
                        {
                            body = body + " Odlazak je na relaciji :" + myRow["Relacija"].ToString() + "  vozom br.: " + myRow["BrVoza"].ToString() + "<br />";
                            body = body + " vreme polaska:  " + myRow["VremePolaskaO"].ToString() + "   vreme dolaska: " + myRow["VremeDolaskaO"].ToString() + "<br /><br /> ";

                            body = body + " Prijavljujemo otpremu kontejnera:  <br />";
                            body = body + "Broj kontejnera: <br />";
                        }
                        countS = countS + 1;
                        body = body + myRow["BrojKontejnera"].ToString() + ";";
                        //  body = body + "Broj vagona: " + myRow["BrojVagona"].ToString() + "<br />";
                        //  body = body + "Tara: " + myRow["Tara"].ToString() + "<br />";
                        //  body = body + "Neto: " + myRow["Neto"].ToString() + "<br />";
                        //

                    }

                    //  body = body + "<br /> Po izvršenom dolasku voza i prijemu kontejnera na terminal ŽIT-a u Beograd ranžirnoj obavestićemo Vas blagovremeno na e-mailom.   <br />";
                    body = body + "Terminal ŽIT  <br />";
                    body = body + "Tel: <br />";
                    body = body + "Email: <br />";


                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.zitbgd.rs";

                    smtpClient.Port = 25;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential("logistika@zitbgd.rs", "zit2019log");

                    smtpClient.EnableSsl = false;
                    smtpClient.Send(mailMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void PosaljiMailOdjavaPosiljalac(string Kome)
        {
            try
            {
                string zadnjibroj = txtSifra.Text;
                //drugi je kome
                var select2 = " SELECT Distinct Posiljalac"
                  + " FROM [dbo].[OtpremaKontejneraVozStavke] where IDNadredjenog =  " + Convert.ToInt32(txtSifra.Text);

                var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection myConnection2 = new SqlConnection(s_connection2);
                var c2 = new SqlConnection(s_connection2);
                var dataAdapter2 = new SqlDataAdapter(select2, c2);

                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                foreach (DataRow myRow2 in ds2.Tables[0].Rows)
                {

                    mailMessage = new MailMessage("logistika@zitbgd.rs", Kome);

                    mailMessage.Subject = "Leget najava. Br. voza: " + zadnjibroj + " . ";

                    var select = "  SELECT OtpremaKontejneraVozStavke.[ID] , Voz.BrVoza, BrojKontejnera, BrojVagona, Tara, Neto, Voz.Relacija, Voz.VremePolaskaO, Voz.VremeDolaskaO, " +
                    " OtpremaKontejneraVozStavke.Primalac,  OtpremaKontejneraVozStavke.Posiljalac, OtpremaKontejneraVozStavke.VlasnikKontejnera, OtpremaKontejneraVozStavke.Organizator " +
                " FROM [dbo].[OtpremaKontejneraVozStavke] " +
                " inner join OtpremaKontejnera on OtpremaKontejneraVozStavke.IDNadredjenog = OtpremaKontejnera.Id " +
                " inner join Voz on OtpremaKontejnera.IdVoza = Voz.Id " +
                " inner join Stanice on Voz.StanicaOd = Stanice.Id " +
                " where IDNadredjenog = " + Convert.ToInt32(txtSifra.Text) + " and Primalac =  " + myRow2["Primalac"].ToString();


                    var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    string body = "NAJAVA ODLASKA KONTEJNERA VOZOM :  <br /><br />";
                    body = body + " Poštovani,    <br /> <br /> <br />";


                    int countS = 0;
                    foreach (DataRow myRow in ds.Tables[0].Rows)
                    {
                        if (countS == 0)
                        {
                            body = body + " Očekivani odlazak je na relaciji :" + myRow["Relacija"].ToString() + "  vozom br.: " + myRow["BrVoza"].ToString() + "<br />";
                            body = body + " vreme polaska:  " + myRow["VremePolaskaO"].ToString() + "   vreme dolaska: " + myRow["VremeDolaskaO"].ToString() + "<br /><br /> ";

                            body = body + " Prijavljujemo najavu odlaska kontejnera:  <br />";
                            body = body + "Broj kontejnera: <br />";
                        }
                        countS = countS + 1;
                        body = body + myRow["BrojKontejnera"].ToString() + ";";
                        //  body = body + "Broj vagona: " + myRow["BrojVagona"].ToString() + "<br />";
                        //  body = body + "Tara: " + myRow["Tara"].ToString() + "<br />";
                        //  body = body + "Neto: " + myRow["Neto"].ToString() + "<br />";
                        //

                    }

                    //  body = body + "<br /> Po izvršenom dolasku voza i prijemu kontejnera na terminal ŽIT-a u Beograd ranžirnoj obavestićemo Vas blagovremeno na e-mailom.   <br />";
                    body = body + "Terminal ŽIT  <br />";
                    body = body + "Tel: <br />";
                    body = body + "Email: <br />";


                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.zitbgd.rs";

                    smtpClient.Port = 25;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential("logistika@zitbgd.rs", "zit2019log");

                    smtpClient.EnableSsl = false;
                    smtpClient.Send(mailMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void PosaljiMailOdjavaPosiljalacOtprema(string Kome)
        {
            try
            {
                string zadnjibroj = txtSifra.Text;
                //drugi je kome
                var select2 = " SELECT Distinct Posiljalac"
                  + " FROM [dbo].[OtpremaKontejneraVozStavke] where IDNadredjenog =  " + Convert.ToInt32(txtSifra.Text);

                var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection myConnection2 = new SqlConnection(s_connection2);
                var c2 = new SqlConnection(s_connection2);
                var dataAdapter2 = new SqlDataAdapter(select2, c2);

                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                foreach (DataRow myRow2 in ds2.Tables[0].Rows)
                {

                    mailMessage = new MailMessage("logistika@zitbgd.rs", Kome);

                    mailMessage.Subject = "Leget pristigla najava. Br. voza: " + zadnjibroj + " . ";

                    var select = "  SELECT OtpremaKontejneraVozStavke.[ID] , Voz.BrVoza, BrojKontejnera, BrojVagona, Tara, Neto, Voz.Relacija, Voz.VremePolaskaO, Voz.VremeDolaskaO, " +
                    " OtpremaKontejneraVozStavke.Primalac,  OtpremaKontejneraVozStavke.Posiljalac, OtpremaKontejneraVozStavke.VlasnikKontejnera, OtpremaKontejneraVozStavke.Organizator " +
                " FROM [dbo].[OtpremaKontejneraVozStavke] " +
                " inner join OtpremaKontejnera on OtpremaKontejneraVozStavke.IDNadredjenog = OtpremaKontejnera.Id " +
                " inner join Voz on OtpremaKontejnera.IdVoza = Voz.Id " +
                " inner join Stanice on Voz.StanicaOd = Stanice.Id " +
                " where IDNadredjenog = " + Convert.ToInt32(txtSifra.Text) + " and Primalac =  " + myRow2["Primalac"].ToString();
                    /*
                    var select5 = "  SELECT OtpremaKontejneraVozStavke.ID, OtpremaKontejneraVozStavke.RB, OtpremaKontejneraVozStavke.IDNadredjenog,  OtpremaKontejneraVozStavke.BrojKontejnera, OtpremaKontejneraVozStavke.Granica, "
                        + " OtpremaKontejneraVozStavke.BrojOsovina, OtpremaKontejneraVozStavke.SopstvenaMasa, OtpremaKontejneraVozStavke.Tara, OtpremaKontejneraVozStavke.Neto, Komitenti.Naziv AS Posiljalac, Komitenti_1.Naziv AS primalac, "
                        + " Komitenti_2.Naziv AS Vlasnikkontejnera, " +
                          " Komitenti_3.Naziv AS Organizator, " +
                        "  TipKontenjera.Naziv AS TipKontejnera, VrstaRobe.Naziv AS VrstaRobe, OtpremaKontejneraVozStavke.Buking , OtpremaKontejneraVozStavke.StatusKontejnera, "
                        + " OtpremaKontejneraVozStavke.BrojPlombe, OtpremaKontejneraVozStavke.BrojPlombe2, OtpremaKontejneraVozStavke.PlaniraniLager,"
                         + " OtpremaKontejneraVozStavke.BrojVagona, "
                        + " OtpremaKontejneraVozStavke.Datum, OtpremaKontejneraVozStavke.Korisnik "
                        + "FROM  Komitenti INNER JOIN "
                        + " OtpremaKontejneraVozStavke ON Komitenti.ID = OtpremaKontejneraVozStavke.Posiljalac INNER JOIN "
                        + " Komitenti AS Komitenti_1 ON OtpremaKontejneraVozStavke.Primalac = Komitenti_1.ID INNER JOIN "
                        + " Komitenti AS Komitenti_2 ON OtpremaKontejneraVozStavke.VlasnikKontejnera = Komitenti_2.ID INNER JOIN "
                          + " Komitenti AS Komitenti_3 ON OtpremaKontejneraVozStavke.Organizator = Komitenti_3.ID INNER JOIN "
                         + "TipKontenjera ON OtpremaKontejneraVozStavke.TipKontejnera = TipKontenjera.ID INNER JOIN "
                        + " VrstaRobe ON OtpremaKontejneraVozStavke.VrstaRobe = VrstaRobe.ID "
                          + " where IdNadredjenog = " + txtSifra.Text + " order by RB";
                          */

                    var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    string body = "OTPREMA KONTEJNERA VOZOM :  <br /><br />";
                    body = body + " Poštovani,    <br /> <br /> <br />";


                    int countS = 0;
                    foreach (DataRow myRow in ds.Tables[0].Rows)
                    {
                        if (countS == 0)
                        {
                            body = body + " Odlazak je na relaciji :" + myRow["Relacija"].ToString() + "  vozom br.: " + myRow["BrVoza"].ToString() + "<br />";
                            body = body + " vreme polaska:  " + myRow["VremePolaskaO"].ToString() + "   vreme dolaska: " + myRow["VremeDolaskaO"].ToString() + "<br /><br /> ";

                            body = body + " Prijavljujemo otpremu kontejnera:  <br />";
                            body = body + "Broj kontejnera: <br />";
                        }
                        countS = countS + 1;
                        body = body + myRow["BrojKontejnera"].ToString() + ";";
                        //  body = body + "Broj vagona: " + myRow["BrojVagona"].ToString() + "<br />";
                        //  body = body + "Tara: " + myRow["Tara"].ToString() + "<br />";
                        //  body = body + "Neto: " + myRow["Neto"].ToString() + "<br />";
                        //

                    }

                    //  body = body + "<br /> Po izvršenom dolasku voza i prijemu kontejnera na terminal ŽIT-a u Beograd ranžirnoj obavestićemo Vas blagovremeno na e-mailom.   <br />";
                    body = body + "Terminal ŽIT  <br />";
                    body = body + "Tel: <br />";
                    body = body + "Email: <br />";


                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.zitbgd.rs";

                    smtpClient.Port = 25;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential("logistika@zitbgd.rs", "zit2019log");

                    smtpClient.EnableSsl = false;
                    smtpClient.Send(mailMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void PosaljiMailOdjavaOrganizator(string Kome)
        {
            try
            {
                string zadnjibroj = txtSifra.Text;
                //drugi je kome
                var select2 = " SELECT Distinct Organizator"
                  + " FROM [dbo].[OtpremaKontejneraVozStavke] where IDNadredjenog =  " + Convert.ToInt32(txtSifra.Text);

                var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection myConnection2 = new SqlConnection(s_connection2);
                var c2 = new SqlConnection(s_connection2);
                var dataAdapter2 = new SqlDataAdapter(select2, c2);

                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                foreach (DataRow myRow2 in ds2.Tables[0].Rows)
                {

                    mailMessage = new MailMessage("logistika@zitbgd.rs", Kome);

                    mailMessage.Subject = "Leget najava. Br. voza: " + zadnjibroj + " . ";

                    var select = "  SELECT OtpremaKontejneraVozStavke.[ID] , Voz.BrVoza, BrojKontejnera, BrojVagona, Tara, Neto, Voz.Relacija, Voz.VremePolaskaO, Voz.VremeDolaskaO, " +
                    " OtpremaKontejneraVozStavke.Primalac,  OtpremaKontejneraVozStavke.Posiljalac, OtpremaKontejneraVozStavke.VlasnikKontejnera, OtpremaKontejneraVozStavke.Organizator " +
                " FROM [dbo].[OtpremaKontejneraVozStavke] " +
                " inner join OtpremaKontejnera on OtpremaKontejneraVozStavke.IDNadredjenog = OtpremaKontejnera.Id " +
                " inner join Voz on OtpremaKontejnera.IdVoza = Voz.Id " +
                " inner join Stanice on Voz.StanicaOd = Stanice.Id " +
                " where IDNadredjenog = " + Convert.ToInt32(txtSifra.Text) + " and Primalac =  " + myRow2["Primalac"].ToString();


                    var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    string body = "NAJAVA ODLASKA KONTEJNERA VOZOM :  <br /><br />";
                    body = body + " Poštovani,    <br /> <br /> <br />";


                    int countS = 0;
                    foreach (DataRow myRow in ds.Tables[0].Rows)
                    {
                        if (countS == 0)
                        {
                            body = body + " Očekivani odlazak je na relaciji :" + myRow["Relacija"].ToString() + "  vozom br.: " + myRow["BrVoza"].ToString() + "<br />";
                            body = body + " vreme polaska:  " + myRow["VremePolaskaO"].ToString() + "   vreme dolaska: " + myRow["VremeDolaskaO"].ToString() + "<br /><br /> ";

                            body = body + " Prijavljujemo najavu odlaska kontejnera:  <br />";
                            body = body + "Broj kontejnera: <br />";
                        }
                        countS = countS + 1;
                        body = body + myRow["BrojKontejnera"].ToString() + ";";
                        //  body = body + "Broj vagona: " + myRow["BrojVagona"].ToString() + "<br />";
                        //  body = body + "Tara: " + myRow["Tara"].ToString() + "<br />";
                        //  body = body + "Neto: " + myRow["Neto"].ToString() + "<br />";
                        //

                    }

                    //  body = body + "<br /> Po izvršenom dolasku voza i prijemu kontejnera na terminal ŽIT-a u Beograd ranžirnoj obavestićemo Vas blagovremeno na e-mailom.   <br />";
                    body = body + "Terminal ŽIT  <br />";
                    body = body + "Tel: <br />";
                    body = body + "Email: <br />";


                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.zitbgd.rs";

                    smtpClient.Port = 25;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential("logistika@zitbgd.rs", "zit2019log");

                    smtpClient.EnableSsl = false;
                    smtpClient.Send(mailMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void PosaljiMailOdjavaOrganizatorOtprema(string Kome)
        {
            try
            {
                string zadnjibroj = txtSifra.Text;
                //drugi je kome
                var select2 = " SELECT Distinct Organizator"
                  + " FROM [dbo].[OtpremaKontejneraVozStavke] where IDNadredjenog =  " + Convert.ToInt32(txtSifra.Text);

                var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection myConnection2 = new SqlConnection(s_connection2);
                var c2 = new SqlConnection(s_connection2);
                var dataAdapter2 = new SqlDataAdapter(select2, c2);

                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                foreach (DataRow myRow2 in ds2.Tables[0].Rows)
                {

                    mailMessage = new MailMessage("logistika@zitbgd.rs", Kome);

                    mailMessage.Subject = "Leget pristigla najava. Br. voza: " + zadnjibroj + " . ";

                    var select = "  SELECT OtpremaKontejneraVozStavke.[ID] , Voz.BrVoza, BrojKontejnera, BrojVagona, Tara, Neto, Voz.Relacija, Voz.VremePolaskaO, Voz.VremeDolaskaO, " +
                    " OtpremaKontejneraVozStavke.Primalac,  OtpremaKontejneraVozStavke.Posiljalac, OtpremaKontejneraVozStavke.VlasnikKontejnera, OtpremaKontejneraVozStavke.Organizator " +
                " FROM [dbo].[OtpremaKontejneraVozStavke] " +
                " inner join OtpremaKontejnera on OtpremaKontejneraVozStavke.IDNadredjenog = OtpremaKontejnera.Id " +
                " inner join Voz on OtpremaKontejnera.IdVoza = Voz.Id " +
                " inner join Stanice on Voz.StanicaOd = Stanice.Id " +
                " where IDNadredjenog = " + Convert.ToInt32(txtSifra.Text) + " and Primalac =  " + myRow2["Primalac"].ToString();
                    /*
                    var select5 = "  SELECT OtpremaKontejneraVozStavke.ID, OtpremaKontejneraVozStavke.RB, OtpremaKontejneraVozStavke.IDNadredjenog,  OtpremaKontejneraVozStavke.BrojKontejnera, OtpremaKontejneraVozStavke.Granica, "
                        + " OtpremaKontejneraVozStavke.BrojOsovina, OtpremaKontejneraVozStavke.SopstvenaMasa, OtpremaKontejneraVozStavke.Tara, OtpremaKontejneraVozStavke.Neto, Komitenti.Naziv AS Posiljalac, Komitenti_1.Naziv AS primalac, "
                        + " Komitenti_2.Naziv AS Vlasnikkontejnera, " +
                          " Komitenti_3.Naziv AS Organizator, " +
                        "  TipKontenjera.Naziv AS TipKontejnera, VrstaRobe.Naziv AS VrstaRobe, OtpremaKontejneraVozStavke.Buking , OtpremaKontejneraVozStavke.StatusKontejnera, "
                        + " OtpremaKontejneraVozStavke.BrojPlombe, OtpremaKontejneraVozStavke.BrojPlombe2, OtpremaKontejneraVozStavke.PlaniraniLager,"
                         + " OtpremaKontejneraVozStavke.BrojVagona, "
                        + " OtpremaKontejneraVozStavke.Datum, OtpremaKontejneraVozStavke.Korisnik "
                        + "FROM  Komitenti INNER JOIN "
                        + " OtpremaKontejneraVozStavke ON Komitenti.ID = OtpremaKontejneraVozStavke.Posiljalac INNER JOIN "
                        + " Komitenti AS Komitenti_1 ON OtpremaKontejneraVozStavke.Primalac = Komitenti_1.ID INNER JOIN "
                        + " Komitenti AS Komitenti_2 ON OtpremaKontejneraVozStavke.VlasnikKontejnera = Komitenti_2.ID INNER JOIN "
                          + " Komitenti AS Komitenti_3 ON OtpremaKontejneraVozStavke.Organizator = Komitenti_3.ID INNER JOIN "
                         + "TipKontenjera ON OtpremaKontejneraVozStavke.TipKontejnera = TipKontenjera.ID INNER JOIN "
                        + " VrstaRobe ON OtpremaKontejneraVozStavke.VrstaRobe = VrstaRobe.ID "
                          + " where IdNadredjenog = " + txtSifra.Text + " order by RB";
                          */

                    var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    string body = "OTPREMA KONTEJNERA VOZOM :  <br /><br />";
                    body = body + " Poštovani,    <br /> <br /> <br />";


                    int countS = 0;
                    foreach (DataRow myRow in ds.Tables[0].Rows)
                    {
                        if (countS == 0)
                        {
                            body = body + " Odlazak je na relaciji :" + myRow["Relacija"].ToString() + "  vozom br.: " + myRow["BrVoza"].ToString() + "<br />";
                            body = body + " vreme polaska:  " + myRow["VremePolaskaO"].ToString() + "   vreme dolaska: " + myRow["VremeDolaskaO"].ToString() + "<br /><br /> ";

                            body = body + " Prijavljujemo otpremu kontejnera:  <br />";
                            body = body + "Broj kontejnera: <br />";
                        }
                        countS = countS + 1;
                        body = body + myRow["BrojKontejnera"].ToString() + ";";
                        //  body = body + "Broj vagona: " + myRow["BrojVagona"].ToString() + "<br />";
                        //  body = body + "Tara: " + myRow["Tara"].ToString() + "<br />";
                        //  body = body + "Neto: " + myRow["Neto"].ToString() + "<br />";
                        //

                    }

                    //  body = body + "<br /> Po izvršenom dolasku voza i prijemu kontejnera na terminal ŽIT-a u Beograd ranžirnoj obavestićemo Vas blagovremeno na e-mailom.   <br />";
                    body = body + "Terminal ŽIT  <br />";
                    body = body + "Tel: <br />";
                    body = body + "Email: <br />";


                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.zitbgd.rs";

                    smtpClient.Port = 25;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential("logistika@zitbgd.rs", "zit2019log");

                    smtpClient.EnableSsl = false;
                    smtpClient.Send(mailMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void PosaljiMailOdjavaVlasnikKontejnera(string Kome)
        {
            try
            {
                string zadnjibroj = txtSifra.Text;
                //drugi je kome
                var select2 = " SELECT Distinct VlasnikKontejnera"
                  + " FROM [dbo].[OtpremaKontejneraVozStavke] where IDNadredjenog =  " + Convert.ToInt32(txtSifra.Text);

                var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection myConnection2 = new SqlConnection(s_connection2);
                var c2 = new SqlConnection(s_connection2);
                var dataAdapter2 = new SqlDataAdapter(select2, c2);

                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                foreach (DataRow myRow2 in ds2.Tables[0].Rows)
                {

                    mailMessage = new MailMessage("logistika@zitbgd.rs", Kome);

                    mailMessage.Subject = "Leget najava. Br. voza: " + zadnjibroj + " . ";

                    var select = "  SELECT OtpremaKontejneraVozStavke.[ID] , Voz.BrVoza, BrojKontejnera, BrojVagona, Tara, Neto, Voz.Relacija, Voz.VremePolaskaO, Voz.VremeDolaskaO, " +
                    " OtpremaKontejneraVozStavke.Primalac,  OtpremaKontejneraVozStavke.Posiljalac, OtpremaKontejneraVozStavke.VlasnikKontejnera, OtpremaKontejneraVozStavke.Organizator " +
                " FROM [dbo].[OtpremaKontejneraVozStavke] " +
                " inner join OtpremaKontejnera on OtpremaKontejneraVozStavke.IDNadredjenog = OtpremaKontejnera.Id " +
                " inner join Voz on OtpremaKontejnera.IdVoza = Voz.Id " +
                " inner join Stanice on Voz.StanicaOd = Stanice.Id " +
                " where IDNadredjenog = " + Convert.ToInt32(txtSifra.Text) + " and Primalac =  " + myRow2["Primalac"].ToString();


                    var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    string body = "NAJAVA ODLASKA KONTEJNERA VOZOM :  <br /><br />";
                    body = body + " Poštovani,    <br /> <br /> <br />";


                    int countS = 0;
                    foreach (DataRow myRow in ds.Tables[0].Rows)
                    {
                        if (countS == 0)
                        {
                            body = body + " Očekivani odlazak je na relaciji :" + myRow["Relacija"].ToString() + "  vozom br.: " + myRow["BrVoza"].ToString() + "<br />";
                            body = body + " vreme polaska:  " + myRow["VremePolaskaO"].ToString() + "   vreme dolaska: " + myRow["VremeDolaskaO"].ToString() + "<br /><br /> ";

                            body = body + " Prijavljujemo najavu odlaska kontejnera:  <br />";
                            body = body + "Broj kontejnera: <br />";
                        }
                        countS = countS + 1;
                        body = body + myRow["BrojKontejnera"].ToString() + ";";
                        //  body = body + "Broj vagona: " + myRow["BrojVagona"].ToString() + "<br />";
                        //  body = body + "Tara: " + myRow["Tara"].ToString() + "<br />";
                        //  body = body + "Neto: " + myRow["Neto"].ToString() + "<br />";
                        //

                    }

                    //  body = body + "<br /> Po izvršenom dolasku voza i prijemu kontejnera na terminal ŽIT-a u Beograd ranžirnoj obavestićemo Vas blagovremeno na e-mailom.   <br />";
                    body = body + "Terminal ŽIT  <br />";
                    body = body + "Tel: <br />";
                    body = body + "Email: <br />";


                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.zitbgd.rs";

                    smtpClient.Port = 25;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential("logistika@zitbgd.rs", "zit2019log");

                    smtpClient.EnableSsl = false;
                    smtpClient.Send(mailMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void PosaljiMailOdjavaVlasnikKontejneraOtprema(string Kome)
        {
            try
            {
                string zadnjibroj = txtSifra.Text;
                //drugi je kome
                var select2 = " SELECT Distinct VlasnikKontejnera"
                  + " FROM [dbo].[OtpremaKontejneraVozStavke] where IDNadredjenog =  " + Convert.ToInt32(txtSifra.Text);

                var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection myConnection2 = new SqlConnection(s_connection2);
                var c2 = new SqlConnection(s_connection2);
                var dataAdapter2 = new SqlDataAdapter(select2, c2);

                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                foreach (DataRow myRow2 in ds2.Tables[0].Rows)
                {

                    mailMessage = new MailMessage("logistika@zitbgd.rs", Kome);

                    mailMessage.Subject = "Leget pristigla najava. Br. voza: " + zadnjibroj + " . ";

                    var select = "  SELECT OtpremaKontejneraVozStavke.[ID] , Voz.BrVoza, BrojKontejnera, BrojVagona, Tara, Neto, Voz.Relacija, Voz.VremePolaskaO, Voz.VremeDolaskaO, " +
                    " OtpremaKontejneraVozStavke.Primalac,  OtpremaKontejneraVozStavke.Posiljalac, OtpremaKontejneraVozStavke.VlasnikKontejnera, OtpremaKontejneraVozStavke.Organizator " +
                " FROM [dbo].[OtpremaKontejneraVozStavke] " +
                " inner join OtpremaKontejnera on OtpremaKontejneraVozStavke.IDNadredjenog = OtpremaKontejnera.Id " +
                " inner join Voz on OtpremaKontejnera.IdVoza = Voz.Id " +
                " inner join Stanice on Voz.StanicaOd = Stanice.Id " +
                " where IDNadredjenog = " + Convert.ToInt32(txtSifra.Text) + " and Primalac =  " + myRow2["Primalac"].ToString();


                    var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    string body = "OTPREMA KONTEJNERA VOZOM :  <br /><br />";
                    body = body + " Poštovani,    <br /> <br /> <br />";


                    int countS = 0;
                    foreach (DataRow myRow in ds.Tables[0].Rows)
                    {
                        if (countS == 0)
                        {
                            body = body + " Odlazak je na relaciji :" + myRow["Relacija"].ToString() + "  vozom br.: " + myRow["BrVoza"].ToString() + "<br />";
                            body = body + " vreme polaska:  " + myRow["VremePolaskaO"].ToString() + "   vreme dolaska: " + myRow["VremeDolaskaO"].ToString() + "<br /><br /> ";

                            body = body + " Prijavljujemo otpremu kontejnera:  <br />";
                            body = body + "Broj kontejnera: <br />";
                        }
                        countS = countS + 1;
                        body = body + myRow["BrojKontejnera"].ToString() + ";";
                        //  body = body + "Broj vagona: " + myRow["BrojVagona"].ToString() + "<br />";
                        //  body = body + "Tara: " + myRow["Tara"].ToString() + "<br />";
                        //  body = body + "Neto: " + myRow["Neto"].ToString() + "<br />";
                        //

                    }

                    //  body = body + "<br /> Po izvršenom dolasku voza i prijemu kontejnera na terminal ŽIT-a u Beograd ranžirnoj obavestićemo Vas blagovremeno na e-mailom.   <br />";
                    body = body + "Terminal ŽIT <br />";
                    body = body + "Tel: <br />";
                    body = body + "Email: <br />";


                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.zitbgd.rs";

                    smtpClient.Port = 25;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential("logistika@zitbgd.rs", "zit2019log");

                    smtpClient.EnableSsl = false;
                    smtpClient.Send(mailMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        //Kamion
        private void PosaljiMailOdjavaPrimalacKamion(string Kome)
        {
            try
            {
                string zadnjibroj = txtSifra.Text;
                //drugi je kome
                var select2 = " SELECT Distinct Primalac"
                  + " FROM [dbo].[OtpremaKontejneraVozStavke] where IDNadredjenog =  " + Convert.ToInt32(txtSifra.Text);

                var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection myConnection2 = new SqlConnection(s_connection2);
                var c2 = new SqlConnection(s_connection2);
                var dataAdapter2 = new SqlDataAdapter(select2, c2);

                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                foreach (DataRow myRow2 in ds2.Tables[0].Rows)
                {

                    mailMessage = new MailMessage("logistika@zitbgd.rs", Kome);

                    mailMessage.Subject = "Leget najava odlaska. Br. zapisa: " + zadnjibroj + " . ";

                    var select = "  SELECT OtpremaKontejneraVozStavke.[ID] , RegBrKamiona, BrojKontejnera, BrojVagona, Tara, Neto,ImeVozaca, " +
                    " OtpremaKontejneraVozStavke.Primalac,  OtpremaKontejneraVozStavke.Posiljalac, OtpremaKontejneraVozStavke.VlasnikKontejnera, OtpremaKontejneraVozStavke.Organizator " +
                " FROM [dbo].[OtpremaKontejneraVozStavke] " +
                " inner join OtpremaKontejnera on OtpremaKontejneraVozStavke.IDNadredjenog = OtpremaKontejnera.Id " +
                " where IDNadredjenog = " + Convert.ToInt32(txtSifra.Text) + " and Primalac =  " + myRow2["Primalac"].ToString();


                    var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    string body = "NAJAVA ODLASKA KONTEJNERA KAMION :  <br /><br />";
                    body = body + " Poštovani,    <br /> <br /> <br />";


                    int countS = 0;
                    foreach (DataRow myRow in ds.Tables[0].Rows)
                    {
                        if (countS == 0)
                        {
                            body = body + " Očekivani odlazak je kamionom :" + myRow["RegBrKamiona"].ToString() + "  vozač.: " + myRow["ImeVozaca"].ToString() + "<br />";
                            // body = body + " vreme polaska:  " + myRow["VremePolaskaO"].ToString() + "   vreme dolaska: " + myRow["VremeDolaskaO"].ToString() + "<br /><br /> ";

                            body = body + " Prijavljujemo najavu odlaska kontejnera:  <br />";
                            body = body + "Broj kontejnera: <br />";
                        }
                        countS = countS + 1;
                        body = body + myRow["BrojKontejnera"].ToString() + ";";


                    }

                    body = body + "<br />   <br />";
                    body = body + "Terminal ŽIT  <br />";



                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.zitbgd.rs";

                    smtpClient.Port = 25;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential("logistika@zitbgd.rs", "zit2019log");

                    smtpClient.EnableSsl = false;
                    smtpClient.Send(mailMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void PosaljiMailOdjavaPrimalacOtpremaKamion(string Kome)
        {
            try
            {
                string zadnjibroj = txtSifra.Text;
                //drugi je kome
                var select2 = " SELECT Distinct Primalac"
                  + " FROM [dbo].[OtpremaKontejneraVozStavke] where IDNadredjenog =  " + Convert.ToInt32(txtSifra.Text);

                var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection myConnection2 = new SqlConnection(s_connection2);
                var c2 = new SqlConnection(s_connection2);
                var dataAdapter2 = new SqlDataAdapter(select2, c2);

                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                foreach (DataRow myRow2 in ds2.Tables[0].Rows)
                {

                    mailMessage = new MailMessage("logistika@zitbgd.rs", Kome);

                    mailMessage.Subject = "Leget otprema Br. zapisa: " + zadnjibroj + " . ";

                    var select = "  SELECT OtpremaKontejneraVozStavke.[ID] , RegBrKamiona, BrojKontejnera, BrojVagona, Tara, Neto,ImeVozaca, " +
                    " OtpremaKontejneraVozStavke.Primalac,  OtpremaKontejneraVozStavke.Posiljalac, OtpremaKontejneraVozStavke.VlasnikKontejnera, OtpremaKontejneraVozStavke.Organizator " +
                " FROM [dbo].[OtpremaKontejneraVozStavke] " +
                " inner join OtpremaKontejnera on OtpremaKontejneraVozStavke.IDNadredjenog = OtpremaKontejnera.Id " +
                " where IDNadredjenog = " + Convert.ToInt32(txtSifra.Text) + " and Primalac =  " + myRow2["Primalac"].ToString();


                    var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    string body = "ODLAZAK KONTEJNERA KAMION :  <br /><br />";
                    body = body + " Poštovani,    <br /> <br /> <br />";


                    int countS = 0;
                    foreach (DataRow myRow in ds.Tables[0].Rows)
                    {
                        if (countS == 0)
                        {
                            body = body + " Odlazak je kamionom :" + myRow["RegBrKamiona"].ToString() + "  vozač.: " + myRow["ImeVozaca"].ToString() + "<br />";
                            // body = body + " vreme polaska:  " + myRow["VremePolaskaO"].ToString() + "   vreme dolaska: " + myRow["VremeDolaskaO"].ToString() + "<br /><br /> ";

                            body = body + " Prijavljujemo odlazak kontejnera:  <br />";
                            body = body + "Broj kontejnera: <br />";
                        }
                        countS = countS + 1;
                        body = body + myRow["BrojKontejnera"].ToString() + ";";


                    }

                    body = body + "<br />   <br />";
                    body = body + "Terminal ŽIT <br />";


                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.zitbgd.rs";

                    smtpClient.Port = 25;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential("logistika@zitbgd.rs", "zit2019log");

                    smtpClient.EnableSsl = false;
                    smtpClient.Send(mailMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void PosaljiMailOdjavaPosiljalacKamion(string Kome)
        {
            try
            {
                string zadnjibroj = txtSifra.Text;
                //drugi je kome
                var select2 = " SELECT Distinct Posiljalac"
                  + " FROM [dbo].[OtpremaKontejneraVozStavke] where IDNadredjenog =  " + Convert.ToInt32(txtSifra.Text);

                var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection myConnection2 = new SqlConnection(s_connection2);
                var c2 = new SqlConnection(s_connection2);
                var dataAdapter2 = new SqlDataAdapter(select2, c2);

                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                foreach (DataRow myRow2 in ds2.Tables[0].Rows)
                {

                    mailMessage = new MailMessage("logistika@zitbgd.rs", Kome);

                    mailMessage.Subject = "Leget najava. Br. zapisa: " + zadnjibroj + " . ";

                    var select = "  SELECT OtpremaKontejneraVozStavke.[ID] , RegBrKamiona, BrojKontejnera, BrojVagona, Tara, Neto,ImeVozaca, " +
                   " OtpremaKontejneraVozStavke.Primalac,  OtpremaKontejneraVozStavke.Posiljalac, OtpremaKontejneraVozStavke.VlasnikKontejnera, OtpremaKontejneraVozStavke.Organizator " +
               " FROM [dbo].[OtpremaKontejneraVozStavke] " +
               " inner join OtpremaKontejnera on OtpremaKontejneraVozStavke.IDNadredjenog = OtpremaKontejnera.Id " +
               " where IDNadredjenog = " + Convert.ToInt32(txtSifra.Text) + " and Posiljalac =  " + myRow2["Posiljalac"].ToString();


                    var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    string body = "NAJAVA ODLASKA KONTEJNERA KAMION :  <br /><br />";
                    body = body + " Poštovani,    <br /> <br /> <br />";


                    int countS = 0;
                    foreach (DataRow myRow in ds.Tables[0].Rows)
                    {
                        if (countS == 0)
                        {
                            body = body + " Očekivani odlazak je kamionom :" + myRow["RegBrKamiona"].ToString() + "  vozač.: " + myRow["ImeVozaca"].ToString() + "<br />";
                            // body = body + " vreme polaska:  " + myRow["VremePolaskaO"].ToString() + "   vreme dolaska: " + myRow["VremeDolaskaO"].ToString() + "<br /><br /> ";

                            body = body + " Prijavljujemo najavu odlaska kontejnera:  <br />";
                            body = body + "Broj kontejnera: <br />";
                        }
                        countS = countS + 1;
                        body = body + myRow["BrojKontejnera"].ToString() + ";";


                    }

                    body = body + "<br />   <br />";
                    body = body + "Terminal ŽIT <br />";


                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.zitbgd.rs";

                    smtpClient.Port = 25;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential("logistika@zitbgd.rs", "zit2019log");

                    smtpClient.EnableSsl = false;
                    smtpClient.Send(mailMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void PosaljiMailOdjavaPosiljalacOtpremaKamion(string Kome)
        {
            try
            {
                string zadnjibroj = txtSifra.Text;
                //drugi je kome
                var select2 = " SELECT Distinct Posiljalac"
                  + " FROM [dbo].[OtpremaKontejneraVozStavke] where IDNadredjenog =  " + Convert.ToInt32(txtSifra.Text);

                var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection myConnection2 = new SqlConnection(s_connection2);
                var c2 = new SqlConnection(s_connection2);
                var dataAdapter2 = new SqlDataAdapter(select2, c2);

                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                foreach (DataRow myRow2 in ds2.Tables[0].Rows)
                {

                    mailMessage = new MailMessage("logistika@zitbgd.rs", Kome);

                    mailMessage.Subject = "Leget pristigla najava. Br. voza: " + zadnjibroj + " . ";

                    var select = "  SELECT OtpremaKontejneraVozStavke.[ID] , RegBrKamiona, BrojKontejnera, BrojVagona, Tara, Neto,ImeVozaca, " +
                      " OtpremaKontejneraVozStavke.Primalac,  OtpremaKontejneraVozStavke.Posiljalac, OtpremaKontejneraVozStavke.VlasnikKontejnera, OtpremaKontejneraVozStavke.Organizator " +
                  " FROM [dbo].[OtpremaKontejneraVozStavke] " +
                  " inner join OtpremaKontejnera on OtpremaKontejneraVozStavke.IDNadredjenog = OtpremaKontejnera.Id " +
                  " where IDNadredjenog = " + Convert.ToInt32(txtSifra.Text) + " and Posiljalac =  " + myRow2["Posiljalac"].ToString();


                    var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    string body = "ODLAZAK KONTEJNERA KAMION :  <br /><br />";
                    body = body + " Poštovani,    <br /> <br /> <br />";


                    int countS = 0;
                    foreach (DataRow myRow in ds.Tables[0].Rows)
                    {
                        if (countS == 0)
                        {
                            body = body + " Odlazak je kamionom :" + myRow["RegBrKamiona"].ToString() + "  vozač.: " + myRow["ImeVozaca"].ToString() + "<br />";
                            // body = body + " vreme polaska:  " + myRow["VremePolaskaO"].ToString() + "   vreme dolaska: " + myRow["VremeDolaskaO"].ToString() + "<br /><br /> ";

                            body = body + " Prijavljujemo odlazak kontejnera:  <br />";
                            body = body + "Broj kontejnera: <br />";
                        }
                        countS = countS + 1;
                        body = body + myRow["BrojKontejnera"].ToString() + ";";


                    }

                    body = body + "<br />   <br />";
                    body = body + "Terminal ŽIT <br />";


                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.zitbgd.rs";

                    smtpClient.Port = 25;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential("logistika@zitbgd.rs", "zit2019log");

                    smtpClient.EnableSsl = false;
                    smtpClient.Send(mailMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void PosaljiMailOdjavaOrganizatorKamion(string Kome)
        {
            try
            {
                string zadnjibroj = txtSifra.Text;
                //drugi je kome
                var select2 = " SELECT Distinct Organizator"
                  + " FROM [dbo].[OtpremaKontejneraVozStavke] where IDNadredjenog =  " + Convert.ToInt32(txtSifra.Text);

                var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection myConnection2 = new SqlConnection(s_connection2);
                var c2 = new SqlConnection(s_connection2);
                var dataAdapter2 = new SqlDataAdapter(select2, c2);

                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                foreach (DataRow myRow2 in ds2.Tables[0].Rows)
                {

                    mailMessage = new MailMessage("logistika@zitbgd.rs", Kome);

                    mailMessage.Subject = "Leget najava odlaska. Br. zapisa: " + zadnjibroj + " . ";

                    var select = "  SELECT OtpremaKontejneraVozStavke.[ID] , RegBrKamiona, BrojKontejnera, BrojVagona, Tara, Neto,ImeVozaca, " +
                   " OtpremaKontejneraVozStavke.Primalac,  OtpremaKontejneraVozStavke.Posiljalac, OtpremaKontejneraVozStavke.VlasnikKontejnera, OtpremaKontejneraVozStavke.Organizator " +
               " FROM [dbo].[OtpremaKontejneraVozStavke] " +
               " inner join OtpremaKontejnera on OtpremaKontejneraVozStavke.IDNadredjenog = OtpremaKontejnera.Id " +
               " where IDNadredjenog = " + Convert.ToInt32(txtSifra.Text) + " and Organizator =  " + myRow2["Organizator"].ToString();


                    var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    string body = "NAJAVA ODLASKA KONTEJNERA KAMION :  <br /><br />";
                    body = body + " Poštovani,    <br /> <br /> <br />";


                    int countS = 0;
                    foreach (DataRow myRow in ds.Tables[0].Rows)
                    {
                        if (countS == 0)
                        {
                            body = body + " Očekivani odlazak kamionom :" + myRow["RegBrKamiona"].ToString() + "  vozač.: " + myRow["ImeVozaca"].ToString() + "<br />";
                            // body = body + " vreme polaska:  " + myRow["VremePolaskaO"].ToString() + "   vreme dolaska: " + myRow["VremeDolaskaO"].ToString() + "<br /><br /> ";

                            body = body + " Prijavljujemo najavu odlaska kontejnera:  <br />";
                            body = body + "Broj kontejnera: <br />";
                        }
                        countS = countS + 1;
                        body = body + myRow["BrojKontejnera"].ToString() + ";";


                    }

                    body = body + "<br />   <br />";
                    body = body + "Terminal ŽIT  <br />";


                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.zitbgd.rs";

                    smtpClient.Port = 25;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential("logistika@zitbgd.rs", "zit2019log");

                    smtpClient.EnableSsl = false;
                    smtpClient.Send(mailMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void PosaljiMailOdjavaOrganizatorOtpremaKamion(string Kome)
        {
            try
            {
                string zadnjibroj = txtSifra.Text;
                //drugi je kome
                var select2 = " SELECT Distinct Organizator"
                  + " FROM [dbo].[OtpremaKontejneraVozStavke] where IDNadredjenog =  " + Convert.ToInt32(txtSifra.Text);

                var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection myConnection2 = new SqlConnection(s_connection2);
                var c2 = new SqlConnection(s_connection2);
                var dataAdapter2 = new SqlDataAdapter(select2, c2);

                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                foreach (DataRow myRow2 in ds2.Tables[0].Rows)
                {

                    mailMessage = new MailMessage("logistika@zitbgd.rs", Kome);

                    mailMessage.Subject = "Leget pristigla najava. Br. voza: " + zadnjibroj + " . ";

                    var select = "  SELECT OtpremaKontejneraVozStavke.[ID] , RegBrKamiona, BrojKontejnera, BrojVagona, Tara, Neto,ImeVozaca, " +
                      " OtpremaKontejneraVozStavke.Primalac,  OtpremaKontejneraVozStavke.Posiljalac, OtpremaKontejneraVozStavke.VlasnikKontejnera, OtpremaKontejneraVozStavke.Organizator " +
                  " FROM [dbo].[OtpremaKontejneraVozStavke] " +
                  " inner join OtpremaKontejnera on OtpremaKontejneraVozStavke.IDNadredjenog = OtpremaKontejnera.Id " +
                  " where IDNadredjenog = " + Convert.ToInt32(txtSifra.Text) + " and Organizator =  " + myRow2["Organizator"].ToString();


                    var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    string body = "ODLAZAK KONTEJNERA KAMIONOM :  <br /><br />";
                    body = body + " Poštovani,    <br /> <br /> <br />";


                    int countS = 0;
                    foreach (DataRow myRow in ds.Tables[0].Rows)
                    {
                        if (countS == 0)
                        {
                            body = body + " Odlazak je kamionom :" + myRow["RegBrKamiona"].ToString() + "  vozač.: " + myRow["ImeVozaca"].ToString() + "<br />";
                            // body = body + " vreme polaska:  " + myRow["VremePolaskaO"].ToString() + "   vreme dolaska: " + myRow["VremeDolaskaO"].ToString() + "<br /><br /> ";

                            body = body + " Prijavljujemo odlazak kontejnera:  <br />";
                            body = body + "Broj kontejnera: <br />";
                        }
                        countS = countS + 1;
                        body = body + myRow["BrojKontejnera"].ToString() + ";";


                    }

                    body = body + "<br />   <br />";
                    body = body + "Terminal ŽIT <br />";


                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.zitbgd.rs";

                    smtpClient.Port = 25;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential("logistika@zitbgd.rs", "zit2019log");

                    smtpClient.EnableSsl = false;
                    smtpClient.Send(mailMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void PosaljiMailOdjavaVlasnikKontejneraKamion(string Kome)
        {
            try
            {
                string zadnjibroj = txtSifra.Text;
                //drugi je kome
                var select2 = " SELECT Distinct VlasnikKontejnera"
                  + " FROM [dbo].[OtpremaKontejneraVozStavke] where IDNadredjenog =  " + Convert.ToInt32(txtSifra.Text);

                var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection myConnection2 = new SqlConnection(s_connection2);
                var c2 = new SqlConnection(s_connection2);
                var dataAdapter2 = new SqlDataAdapter(select2, c2);

                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                foreach (DataRow myRow2 in ds2.Tables[0].Rows)
                {

                    mailMessage = new MailMessage("logistika@zitbgd.rs", Kome);

                    mailMessage.Subject = "Leget najava. Br. voza: " + zadnjibroj + " . ";

                    var select = "  SELECT OtpremaKontejneraVozStavke.[ID] , RegBrKamiona, BrojKontejnera, BrojVagona, Tara, Neto,ImeVozaca, " +
                   " OtpremaKontejneraVozStavke.Primalac,  OtpremaKontejneraVozStavke.Posiljalac, OtpremaKontejneraVozStavke.VlasnikKontejnera, OtpremaKontejneraVozStavke.Organizator " +
               " FROM [dbo].[OtpremaKontejneraVozStavke] " +
               " inner join OtpremaKontejnera on OtpremaKontejneraVozStavke.IDNadredjenog = OtpremaKontejnera.Id " +
               " where IDNadredjenog = " + Convert.ToInt32(txtSifra.Text) + " and VlasnikKontejnera =  " + myRow2["VlasnikKontejnera"].ToString();


                    var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    string body = "NAJAVA ODLASKA KONTEJNERA KAMIONOM :  <br /><br />";
                    body = body + " Poštovani,    <br /> <br /> <br />";


                    int countS = 0;
                    foreach (DataRow myRow in ds.Tables[0].Rows)
                    {
                        if (countS == 0)
                        {
                            body = body + " Očekivani odlazak kamionom :" + myRow["RegBrKamiona"].ToString() + "  vozač.: " + myRow["ImeVozaca"].ToString() + "<br />";
                            // body = body + " vreme polaska:  " + myRow["VremePolaskaO"].ToString() + "   vreme dolaska: " + myRow["VremeDolaskaO"].ToString() + "<br /><br /> ";

                            body = body + " Prijavljujemo najavu odlaska kontejnera:  <br />";
                            body = body + "Broj kontejnera: <br />";
                        }
                        countS = countS + 1;
                        body = body + myRow["BrojKontejnera"].ToString() + ";";


                    }

                    body = body + "<br />   <br />";
                    body = body + "Terminal ŽIT  <br />";


                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.zitbgd.rs";

                    smtpClient.Port = 25;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential("logistika@zitbgd.rs", "zit2019log");

                    smtpClient.EnableSsl = false;
                    smtpClient.Send(mailMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void PosaljiMailOdjavaVlasnikKontejneraOtpremaKamion(string Kome)
        {
            try
            {
                string zadnjibroj = txtSifra.Text;
                //drugi je kome
                var select2 = " SELECT Distinct VlasnikKontejnera"
                  + " FROM [dbo].[OtpremaKontejneraVozStavke] where IDNadredjenog =  " + Convert.ToInt32(txtSifra.Text);

                var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection myConnection2 = new SqlConnection(s_connection2);
                var c2 = new SqlConnection(s_connection2);
                var dataAdapter2 = new SqlDataAdapter(select2, c2);

                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                foreach (DataRow myRow2 in ds2.Tables[0].Rows)
                {

                    mailMessage = new MailMessage("logistika@zitbgd.rs", Kome);

                    mailMessage.Subject = "Leget otprema Br. zapisa: " + zadnjibroj + " . ";

                    var select = "  SELECT OtpremaKontejneraVozStavke.[ID] , RegBrKamiona, BrojKontejnera, BrojVagona, Tara, Neto,ImeVozaca, " +
                   " OtpremaKontejneraVozStavke.Primalac,  OtpremaKontejneraVozStavke.Posiljalac, OtpremaKontejneraVozStavke.VlasnikKontejnera, OtpremaKontejneraVozStavke.Organizator " +
               " FROM [dbo].[OtpremaKontejneraVozStavke] " +
               " inner join OtpremaKontejnera on OtpremaKontejneraVozStavke.IDNadredjenog = OtpremaKontejnera.Id " +
               " where IDNadredjenog = " + Convert.ToInt32(txtSifra.Text) + " and VlasnikKontejnera =  " + myRow2["VlasnikKonteejnera"].ToString();


                    var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    string body = "ODLAZAK KONTEJNERA KAMIONOM :  <br /><br />";
                    body = body + " Poštovani,    <br /> <br /> <br />";


                    int countS = 0;
                    foreach (DataRow myRow in ds.Tables[0].Rows)
                    {
                        if (countS == 0)
                        {
                            body = body + " Odlazak je kamionom :" + myRow["RegBrKamiona"].ToString() + "  vozač.: " + myRow["ImeVozaca"].ToString() + "<br />";
                            // body = body + " vreme polaska:  " + myRow["VremePolaskaO"].ToString() + "   vreme dolaska: " + myRow["VremeDolaskaO"].ToString() + "<br /><br /> ";

                            body = body + " Prijavljujemo odlazak kontejnera:  <br />";
                            body = body + "Broj kontejnera: <br />";
                        }
                        countS = countS + 1;
                        body = body + myRow["BrojKontejnera"].ToString() + ";";


                    }

                    body = body + "<br />   <br />";
                    body = body + "Terminal ŽIT  <br />";


                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.zitbgd.rs";

                    smtpClient.Port = 25;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential("logistika@zitbgd.rs", "zit2019log");

                    smtpClient.EnableSsl = false;
                    smtpClient.Send(mailMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            if (cboStatusOtpreme.SelectedIndex == 0)
            {
                if (chkNajava.Checked == true)
                {
                    MessageBox.Show("Mail za najavu otpreme je već poslat");
                }
                else
                {
                    string phrase = VratiPodatkeEmailPrimalac();
                    string[] words = phrase.Split(';');

                    foreach (var word in words)
                    {
                        if (chkVoz.Checked == true)
                        {
                            PosaljiMailOdjavaPrimalac(word.Trim());
                        }
                        else
                        {
                            PosaljiMailOdjavaPrimalacKamion(word.Trim());//
                        }

                    }

                    string phrase2 = VratiPodatkeEmailPosiljalac();
                    string[] words2 = phrase2.Split(';');

                    foreach (var word2 in words2)
                    {
                        if (chkVoz.Checked == true)
                        {
                            PosaljiMailOdjavaPosiljalac(word2.Trim());
                        }
                        else
                        {
                            PosaljiMailOdjavaPosiljalacKamion(word2.Trim());
                        }
                    }

                    string phrase3 = VratiPodatkeEmailOrganizator();
                    string[] words3 = phrase3.Split(';');

                    foreach (var word3 in words3)
                    {
                        if (chkVoz.Checked == true)
                        {
                            PosaljiMailOdjavaOrganizator(word3.Trim());
                        }
                        else
                        {
                            PosaljiMailOdjavaOrganizatorKamion(word3.Trim());
                        }
                    }

                    string phrase4 = VratiPodatkeEmailVlasnikKontejnera();
                    string[] words4 = phrase4.Split(';');

                    foreach (var word4 in words4)
                    {
                        if (chkVoz.Checked == true)
                        {
                            PosaljiMailOdjavaVlasnikKontejnera(word4.Trim());
                        }
                        else
                        {
                            PosaljiMailOdjavaVlasnikKontejneraKamion(word4.Trim());
                        }
                    }
                    Saobracaj.Dokumenta.InsertOtprema insTer = new Saobracaj.Dokumenta.InsertOtprema();
                    insTer.UpdateEmailOtpremaNajava(Convert.ToInt32(txtSifra.Text));
                    chkNajava.Checked = true;
                }
            }
            else
            {
                if (chkOtprema.Checked == true)
                {
                    MessageBox.Show("Mail za otpremu je već poslat");
                }
                else
                {
                    string phrase = VratiPodatkeEmailPrimalac();
                    string[] words = phrase.Split(';');

                    foreach (var word in words)
                    {
                        if (chkVoz.Checked == true)
                        {
                            PosaljiMailOdjavaPrimalacOtprema(word.Trim());
                        }
                        else
                        {
                            PosaljiMailOdjavaPrimalacOtpremaKamion(word.Trim());
                        }
                    }

                    string phrase2 = VratiPodatkeEmailPosiljalac();
                    string[] words2 = phrase2.Split(';');

                    foreach (var word2 in words2)
                    {
                        if (chkVoz.Checked == true)
                        {
                            PosaljiMailOdjavaPosiljalacOtprema(word2.Trim());
                        }
                        else
                        {
                            PosaljiMailOdjavaPosiljalacOtpremaKamion(word2.Trim());
                        }
                    }

                    string phrase3 = VratiPodatkeEmailOrganizator();
                    string[] words3 = phrase3.Split(';');

                    foreach (var word3 in words3)
                    {
                        if (chkVoz.Checked == true)
                        {
                            PosaljiMailOdjavaOrganizatorOtprema(word3.Trim());
                        }
                        else
                        {
                            PosaljiMailOdjavaOrganizatorOtprema(word3.Trim());
                        }
                    }

                    string phrase4 = VratiPodatkeEmailVlasnikKontejnera();
                    string[] words4 = phrase4.Split(';');

                    foreach (var word4 in words4)
                    {
                        if (chkVoz.Checked == true)
                        {
                            PosaljiMailOdjavaVlasnikKontejneraOtprema(word4.Trim());
                        }
                        else
                        {
                            PosaljiMailOdjavaVlasnikKontejneraOtpremaKamion(word4.Trim());
                        }
                    }


                    Saobracaj.Dokumenta.InsertOtprema insTer = new Saobracaj.Dokumenta.InsertOtprema();
                    insTer.UpdateEmailOtpremaOtprema(Convert.ToInt32(txtSifra.Text));
                    chkOtprema.Checked = true;
                }

            }
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

        private void button7_Click(object sender, EventArgs e)
        {
            Saobracaj.Promet.frmLagerOperater lager = new Saobracaj.Promet.frmLagerOperater();
            lager.Show();
        }

        private void oTPREMAPLATFORMEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string IDUsluge = "0";
            foreach (DataGridViewRow row in dataGridView8.Rows)
            {

                if (row.Selected == true)
                {
                    IDUsluge = row.Cells[0].Value.ToString();

                }


            }
            //ID usluge je ustvaribroj RadnogNalogaInternog

            RadniNalozi.RN6OtpremaPlatforme rnop = new RadniNalozi.RN6OtpremaPlatforme(txtSifra.Text, KorisnikCene, IDUsluge, txtRegBrKamiona.Text, 0, txtNalogID.Text, txtBrojKontejnera.Text);
            rnop.Show();
        }

        private void oTPREMAPLATFORMEToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RadniNalozi.RN7OtpremaPlatforme2 rv = new RadniNalozi.RN7OtpremaPlatforme2(txtSifra.Text, KorisnikCene, txtRegBrKamiona.Text);
            rv.Show();
        }

        private void oTPREMACIRADEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string IDUsluge = "0";
            foreach (DataGridViewRow row in dataGridView8.Rows)
            {
                if (row.Selected == true)
                {
                    IDUsluge = row.Cells[0].Value.ToString();
                }
            }

            RadniNalozi.RN8OtpremaCirade oc = new RadniNalozi.RN8OtpremaCirade(txtSifra.Text, KorisnikCene, IDUsluge, txtRegBrKamiona.Text);
            oc.Show();
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }
        private void FillDGUsluge()
        {

            if (txtKontejnerID.Text == "")
            {
                return;
            }
            var select = "";

            select = "select  RadniNalogInterni.ID as RNID, RadniNalogInterni.StatusIzdavanja, UvozKonacnaVrstaManipulacije.ID as ID, " +
" UvozKonacna.BrojKontejnera, UvozKonacnaVrstaManipulacije.IDNadredjena as KontejnerID,  UvozKonacnaVrstaManipulacije.Kolicina, " +
" VrstaManipulacije.ID as ManipulacijaID,VrstaManipulacije.Naziv as ManipulacijaNaziv,  OrganizacioneJedinice.ID, " +
" OrganizacioneJedinice.Naziv as OrganizacionaJedinica,  RadniNalogInterni.DatumIzdavanja, RadniNalogInterni.Uradjen, Partnerji.PaNaziv as Nalogodavac, " +
" RadniNalogInterni.Pokret, RadniNalogInterni.StatusKontejnera,KontejnerStatus.Naziv " +
" from RadniNalogInterni " +
" Inner  join VrstaManipulacije on VrstaManipulacije.ID = RadniNalogInterni.IDManipulacijaJED " +
" inner join UvozKonacnaVrstaManipulacije on UvozKonacnaVrstaManipulacije.ID = RadniNalogInterni.KonkretaIDUsluge " +
" inner join UvozKonacna on UvozKonacna.ID = RadniNalogInterni.BrojOsnov " +
" inner join PArtnerji on UvozKonacnaVrstaManipulacije.Platilac = PArtnerji.PaSifra " +
" inner join OrganizacioneJedinice on OrganizacioneJedinice.ID = RadniNalogInterni.OjIzdavanja " +
" inner join KontejnerStatus on KontejnerStatus.ID = RadniNalogInterni.StatusKontejnera where RadniNalogInterni.BrojOsnov = " + Convert.ToInt32(txtKontejnerID.Text);



            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView8.ReadOnly = true;
            dataGridView8.DataSource = ds.Tables[0];


            PodesiDatagridView(dataGridView8);

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView8.Columns[0];
            dataGridView8.Columns[0].HeaderText = "RNID";
            dataGridView8.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView8.Columns[1];
            dataGridView8.Columns[1].HeaderText = "STATUS";
            dataGridView8.Columns[1].Width = 70;

            DataGridViewColumn column3 = dataGridView8.Columns[2];
            dataGridView8.Columns[2].HeaderText = "VID";
            dataGridView8.Columns[2].Visible = false;
            dataGridView8.Columns[2].Width = 70;

            DataGridViewColumn column4 = dataGridView8.Columns[3];
            dataGridView8.Columns[3].HeaderText = "MID";
            dataGridView8.Columns[3].Width = 50;

            DataGridViewColumn column5 = dataGridView8.Columns[4];
            dataGridView8.Columns[4].HeaderText = "V";
            dataGridView8.Columns[4].Visible = false;
            dataGridView8.Columns[4].Width = 30;

            DataGridViewColumn column6 = dataGridView8.Columns[5];
            dataGridView8.Columns[5].HeaderText = "KOL";
            dataGridView8.Columns[5].Visible = false;
            dataGridView8.Columns[5].Width = 30;

            DataGridViewColumn column7 = dataGridView8.Columns[6];
            dataGridView8.Columns[6].HeaderText = "VUID";
            dataGridView8.Columns[6].Visible = false;
            dataGridView8.Columns[6].Width = 30;

            DataGridViewColumn column8 = dataGridView8.Columns[7];
            dataGridView8.Columns[7].HeaderText = "USLUGA";
            dataGridView8.Columns[7].Width = 270;

        }
        private void VratiPodatkeSaUvoznogVoza()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("  Select top 1 Voz.ID, PlOtpreme, Sazeta from Voz " +
  "   inner join UvozKonacnaZaglavlje on UvozKonacnaZaglavlje.IDVoza = Voz.ID " +
" inner join RAdniNalogInterni on RAdniNalogInterni.PlanID = UvozKonacnaZaglavlje.ID " +
" inner join PrijemKontejneraVoz on PrijemKontejneraVoz.ID = RAdniNalogInterni.BrojDokPrevoza " +
" Where BrojDokPrevoza = " + txtSifra.Text, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                cboBukingPrijema.SelectedValue = Convert.ToInt32(dr["ID"].ToString());
                dtpSazeta.Value = Convert.ToDateTime(dr["Sazeta"].ToString());
            }

            con.Close();

        }

        private void VratiPodatkeSaUvoznogVozaIzKontejnera()
        {

            var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con2 = new SqlConnection(s_connection2);

            con2.Open();

            SqlCommand cmd2 = new SqlCommand("select top 1 DatumPrijema as ETA, PrijemKontejneraVoz.VremeDolaska as ATA, PrijemKontejneraVoz.IdVoza from PrijemKontejneraVoz " +
 " inner join PrijemKontejneraVozStavke on PrijemKontejneraVozStavke.IDNadredjenog = PrijemKontejneraVoz.ID " +
 " where Vozom = 1 and PrijemKontejneraVozStavke.BrojKontejnera = '" + txtBrojKontejnera.Text.Trim() +
"' Order By PrijemKontejneraVoz.ID DEsc", con2);

            SqlDataReader dr2 = cmd2.ExecuteReader();

            while (dr2.Read())
            {
                dtpATALeget.Value = dtpETALeget.Value = Convert.ToDateTime(dr2["ATA"].ToString());
                cboBukingPrijema.SelectedValue = Convert.ToInt32(dr2["IDVoza"].ToString());
                dtpETALeget.Value = Convert.ToDateTime(dr2["ETA"].ToString());
            }

            con2.Close();



        }
        private void txtKontejnerID_TextChanged(object sender, EventArgs e)
        {

            VratiPodatkeStavkeKontejnerSaOtpremnice(txtKontejnerID.Text);
            if (chkUvoz.Checked == true && chkCirada.Checked == true)
            {
                //Uvoz cirada pakovanje
                panel5.Visible = true;
                panel6.Visible = true;
          
                label47.Visible = false;
                cboRLTerminal.Visible = false;
                label38.Visible = false;
                cboVlasnikKontejnera.Visible = false;

             
                VratiPodatkeSaUvoznogVoza();
                VratiPodatkeSaUvoznogVozaIzKontejnera();
            }
            FillDGUsluge();
            FillDG2();
            FillDG4();
        }

        private void FillDG4()
        {
            var select = "select UvozKonacnaNapomenePozicioniranja.ID, IDNapomene, PredefinisanePoruke.Naziv from UvozKonacnaNapomenePozicioniranja " +
" inner join  PredefinisanePoruke on PredefinisanePoruke.ID = UvozKonacnaNapomenePozicioniranja.IDNapomene where UvozKonacnaNapomenePozicioniranja.IdNadredjena  = " + Convert.ToInt32(txtKontejnerID.Text) + " order by UvozKonacnaNapomenePozicioniranja.ID desc ";
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
            dataGridView4.Columns[2].Width = 500;

        }

        private void FillDG2()
        {

            var select = "  SELECT UvozKonacnaNHM.ID, NHM.Broj, UvozKonacnaNHM.IDNHM, NHM.Naziv FROM NHM INNER JOIN " +
                      " UvozKonacnaNHM ON NHM.ID = UvozKonacnaNHM.IDNHM where UvozKonacnanhm.idnadredjena = " + Convert.ToInt32(txtKontejnerID.Text) + " order by UvozKonacnanhm.ID desc";
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
            dataGridView3.Columns[1].HeaderText = "NHM Broj";
            dataGridView3.Columns[1].Width = 70;

            DataGridViewColumn column3 = dataGridView3.Columns[2];
            dataGridView3.Columns[2].HeaderText = "ID";
            dataGridView3.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView3.Columns[3];
            dataGridView3.Columns[3].HeaderText = "NHM";
            dataGridView3.Columns[3].Width = 200;


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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbPostupak_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da želite da promenite podatke u Modulu Uvoz?", "Izbor", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                Saobracaj.Uvoz.InsertUvozKonacna upd = new Uvoz.InsertUvozKonacna();
                upd.PromeniSaPrijemnice(Convert.ToInt32(txtKontejnerID.Text), txtBrojPlombe.Text, txtBrojPlombe2.Text, txtBrojKontejnera.Text, txtVagon.Text, Convert.ToDouble(txtTara.Value), Convert.ToInt32(cboTipKontejnera.SelectedValue), Convert.ToDecimal(txtTaraTerminal.Value));
            }

            Saobracaj.Dokumenta.InsertOtpremaKontejneraStavke ins = new Saobracaj.Dokumenta.InsertOtpremaKontejneraStavke();
            ins.UpdOtpremaKontejneraVozStav(Convert.ToInt32(txtStavka.Text), Convert.ToInt32(txtSifra.Text), txtBrojKontejnera.Text, txtVagon.Text, Convert.ToDouble(txtGranica.Value), Convert.ToDouble(txtBrojOsovina.Value), Convert.ToDouble(txtSopstvenaMasa.Value), Convert.ToDouble(txtTara.Value), Convert.ToDouble(txtNeto.Value), Convert.ToInt32(cboPosiljalac.SelectedValue), Convert.ToInt32(cboPrimalac.SelectedValue), Convert.ToInt32(cboVlasnikKontejnera.SelectedValue), Convert.ToInt32(cboTipKontejnera.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue), txtBukingBrodar.Text, Convert.ToInt32(cboStatusKontejnera.SelectedValue), txtBrojPlombe.Text, Convert.ToInt32(txtPlaniraniLager.Text), 0, Convert.ToDateTime(dtpVremePripremljen.Value), Convert.ToDateTime(dtpVremeOdlaska.Value), Convert.ToDateTime(DateTime.Now), KorisnikCene, Convert.ToInt32(txtRB.Text), txtBrojPlombe2.Text, Convert.ToInt32(cboOrganizator.SelectedValue), txtNapomenaS.Text, Convert.ToDateTime(dtpPerodSkladistenjaOd.Value), Convert.ToDateTime(dtpPeriodSkladistenjaDo.Value), Convert.ToDouble(bttoRobe.Value), Convert.ToInt32(txtKontejnerID.Text), Convert.ToDouble(bttoKontejnera.Value), txtNapomenaS2.Text, Convert.ToInt32(cbPostupak.SelectedValue), 0, 0, 0, 0, "", "", "");
            RefreshDataGrid2();
        }

        private void chkPlatforma_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPlatforma.Checked == true)
                chkCirada.Checked = false;
            else
            {
                chkCirada.Checked = true;
            }
        }

        private void chkCirada_CheckedChanged(object sender, EventArgs e)
        {

            if (chkCirada.Checked == true)
                chkPlatforma.Checked = false;
            else
            {
                chkPlatforma.Checked = true;
            }
        }

        private void chkTerminal_CheckedChanged(object sender, EventArgs e)
        {
            chkUvoz.Checked = false;
            chkIzvoz.Checked = false;
        }
    }
}
