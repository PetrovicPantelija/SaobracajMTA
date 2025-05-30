﻿using Syncfusion.Windows.Forms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Saobracaj.Dokumenta
{
    public partial class frmKontaktOsobe : Syncfusion.Windows.Forms.Office2010Form
    {
        bool status = false;
        int IzPartnera = 0;
        int IzPartneraICarine = 0;
        int pomPartner = 0;
        int pomCarina = 0;

        private void ChangeTextBox()
        {
            this.BackColor = Color.White;
            this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
            this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
            Office2010Colors.ApplyManagedColors(this, Color.White);
            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
            panelHeader.Visible = false;
            this.ControlBox = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                meniHeader.Visible = false;
                panelHeader.Visible = true;
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = Color.White;
                Office2010Colors.ApplyManagedColors(this, Color.White);




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
                        textBox.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                        // Example: Change font
                    }


                    if (control is System.Windows.Forms.Label label)
                    {
                        // Change properties here
                        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        label.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);  // Example: Change font

                        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                    }
                    if (control is DateTimePicker dtp)
                    {
                        dtp.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        dtp.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }
                    if (control is System.Windows.Forms.CheckBox chk)
                    {
                        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        chk.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ListBox lb)
                    {
                        lb.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        lb.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ComboBox cb)
                    {
                        cb.ForeColor = Color.FromArgb(51, 51, 54);
                        cb.BackColor = Color.White;// Example: Change background color
                        cb.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.NumericUpDown nu)
                    {
                        nu.ForeColor = Color.FromArgb(51, 51, 54);
                        nu.BackColor = Color.White;// Example: Change background color
                        nu.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                }
            }
            else
            {
                meniHeader.Visible = true;
                panelHeader.Visible = false;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
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


        }



        public frmKontaktOsobe()
        {
            InitializeComponent();
            IzPartnera = 0;
            ChangeTextBox();
        }

        public frmKontaktOsobe(int Partner)
        {
            InitializeComponent();
            IzPartnera = 1;
            pomPartner = Partner;
            ChangeTextBox();
        }

        public frmKontaktOsobe(int Carina, int Partner)
        {
            InitializeComponent();
            IzPartneraICarine = 1;
            pomPartner = Partner;
            pomCarina = Carina;
            ChangeTextBox();
        }

        private void frmKontaktOsobe_Load(object sender, EventArgs e)
        {
            var select = " Select Distinct PaSifra, PaNaziv  From Partnerji";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            txtPaKOSifra.DataSource = ds.Tables[0];
            txtPaKOSifra.DisplayMember = "PaNaziv";
            txtPaKOSifra.ValueMember = "PaSifra";


            var select2 = " Select ID, Naziv From Carinarnice order by ID";
            var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboCarinarnica.DataSource = ds2.Tables[0];
            cboCarinarnica.DisplayMember = "Naziv";
            cboCarinarnica.ValueMember = "ID";



            if (IzPartnera == 1)
            {
                txtPaKOSifra.SelectedValue = pomPartner;
                RefreshDataGridPoPartneru();

            }
            else if (IzPartneraICarine == 1)
            {
                txtPaKOSifra.SelectedValue = pomPartner;
                cboCarinarnica.SelectedValue = pomCarina;
                RefreshDataGridPoPartneruICarini();
            }
            else
            {
                RefreshDataGrid();
            }
        }

        private void RefreshDataGrid()
        {
            var select = " SELECT [PaKOZapSt]      ,[PaKOSifra]      ,[PaKOIme]      ,[PaKOPriimek]      ,[PaKOOddelek]      ,[PaKOTel] " +
     " ,[PaKOMail]      ,[PaKOOpomba]      ,[Operatika]  FROM [TESTIRANJE].[dbo].[partnerjiKontOseba]";

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);


            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView1);
            /*
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            */
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Partner ID";
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].HeaderText = "Ime";
            dataGridView1.Columns[3].HeaderText = "Prezime";
            dataGridView1.Columns[4].HeaderText = "Odeljenje";
            dataGridView1.Columns[5].HeaderText = "Telefon";
            dataGridView1.Columns[6].HeaderText = "eMail";
            dataGridView1.Columns[7].HeaderText = "Napomena";




        }

        private void RefreshDataGridPoPartneru()
        {
            var select = " SELECT [PaKOZapSt]      ,[PaKOSifra]      ,[PaKOIme]      ,[PaKOPriimek]      ,[PaKOOddelek]      ,[PaKOTel] " +
     " ,[PaKOMail]      ,[PaKOOpomba]      ,[Operatika]  FROM [partnerjiKontOseba] where PaKoSifra = " + Convert.ToInt32(txtPaKOSifra.SelectedValue);

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);


            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView1);

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Partner ID";
            dataGridView1.Columns[1].Width = 150;


            dataGridView1.Columns[2].HeaderText = "Ime";
            dataGridView1.Columns[3].HeaderText = "Prezime";
            dataGridView1.Columns[4].HeaderText = "Odeljenje";
            dataGridView1.Columns[5].HeaderText = "Telefon";
            dataGridView1.Columns[6].HeaderText = "eMail";
            dataGridView1.Columns[7].HeaderText = "Napomena";


        }

        private void RefreshDataGridPoPartneruICarini()
        {
            var select = " SELECT [PaKOZapSt]      ,[PaKOSifra]      ,[PaKOIme]      ,[PaKOPriimek]      ,[PaKOOddelek]      ,[PaKOTel] " +
     " ,[PaKOMail]      ,[PaKOOpomba]      ,[Operatika]  FROM [partnerjiKontOseba] where PaKoSifra = " + Convert.ToInt32(txtPaKOSifra.SelectedValue)
      + " AND Carinarnica = " + Convert.ToInt32(cboCarinarnica.SelectedValue);

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);


            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView1);

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Partner ID";
            dataGridView1.Columns[1].Width = 150;

            dataGridView1.Columns[2].HeaderText = "Ime";
            dataGridView1.Columns[3].HeaderText = "Prezime";
            dataGridView1.Columns[4].HeaderText = "Odeljenje";
            dataGridView1.Columns[5].HeaderText = "Telefon";
            dataGridView1.Columns[6].HeaderText = "eMail";
            dataGridView1.Columns[7].HeaderText = "Napomena";
        }


        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtPaKOZapSt.Enabled = false;

            txtPaKOZapSt.Text = "";
         //   txtPaKOSifra.Text = "";
            txtPaKOIme.Text = "";
            txtPaKOPriimek.Text = "";
            txtPaKOOddelek.Text = "";
            txtPaKOTel.Text = "";
            txtPaKOMail.Text = "";
            txtPaKOOpomba.Text = "";
            chkOperatika.Checked = false;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            int pomOperater = 0;
            if (chkOperatika.Checked == true)
            {
                pomOperater = 1;
            }

            if (status == true)
            {

                Sifarnici.InsertKontaktOsobe ins = new Sifarnici.InsertKontaktOsobe();
                ins.InsKontaktOsoba(Convert.ToInt32(txtPaKOSifra.SelectedValue), txtPaKOIme.Text, txtPaKOPriimek.Text, txtPaKOOddelek.Text, txtPaKOTel.Text, txtPaKOMail.Text, txtPaKOOpomba.Text, pomOperater, Convert.ToInt32(cboCarinarnica.SelectedValue));
            }
            else
            {
                Sifarnici.InsertKontaktOsobe upd = new Sifarnici.InsertKontaktOsobe();
                upd.UpdKontaktOsoba(Convert.ToInt32(txtPaKOZapSt.Text), Convert.ToInt32(txtPaKOSifra.SelectedValue), txtPaKOIme.Text, txtPaKOPriimek.Text, txtPaKOOddelek.Text, txtPaKOTel.Text, txtPaKOMail.Text, txtPaKOOpomba.Text, pomOperater, Convert.ToInt32(cboCarinarnica.SelectedValue));
            }
            RefreshDataGridPoPartneru();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            Sifarnici.InsertKontaktOsobe upd = new Sifarnici.InsertKontaktOsobe();
            upd.DelKontaktOsoba(Convert.ToInt32(txtPaKOZapSt.Text));
            RefreshDataGrid();
        }

        public string GetKontaktMail(int Partner)
        {
            RefreshDataGridPoPartneru();
            return txtPaKOMail.Text.TrimEnd();
        }
        public string GetKontaktMailSVISelektovani(int Partner)
        {
            RefreshDataGridPoPartneru();
            string emailovi = "";
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    emailovi = emailovi + ";" + row.Cells[6].Value.ToString().TrimEnd();

                }
            }

            return emailovi;
        }

        public string GetKontaktiSVISelektovani(int Partner)
        {
            RefreshDataGridPoPartneru();
            string emailovi = "";
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    emailovi = emailovi + ";" + row.Cells[6].Value.ToString().TrimEnd();

                }
            }

            return emailovi;
        }


        public string GetKontakt(int Partner)
        {

            RefreshDataGridPoPartneru();
            return txtPaKOIme.Text.TrimEnd() + " " + txtPaKOPriimek.Text.TrimEnd() + " " + txtPaKOTel.Text.TrimEnd();
        }

        public string GetKontaktSpeditera()
        {
            return txtPaKOIme.Text.TrimEnd() + " " + txtPaKOPriimek.Text.TrimEnd() + " " + txtPaKOTel.Text.TrimEnd();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {

                        txtPaKOZapSt.Text = row.Cells[0].Value.ToString();
                        // txtPaKOSifra.Text = row.Cells[1].Value.ToString();
                        txtPaKOIme.Text = row.Cells[2].Value.ToString();
                        txtPaKOPriimek.Text = row.Cells[3].Value.ToString();
                        txtPaKOOddelek.Text = row.Cells[4].Value.ToString();
                        txtPaKOTel.Text = row.Cells[5].Value.ToString();
                        txtPaKOMail.Text = row.Cells[6].Value.ToString();
                        txtPaKOOpomba.Text = row.Cells[7].Value.ToString();

                        if (row.Cells[7].Value.ToString() == "1")
                        { chkOperatika.Checked = true; }
                        else
                        {
                            chkOperatika.Checked = false;
                        }
                        txtPaKOSifra.SelectedValue = Convert.ToInt32(row.Cells[1].Value.ToString());
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }
    }
}
