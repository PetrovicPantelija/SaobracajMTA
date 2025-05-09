﻿using Syncfusion.Windows.Forms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Saobracaj.TrackModal.Sifarnici
{
    public partial class frmKopiranjeCenovnikaPoTipu : Form
    {
        private void ChangeTextBox()
        {

            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
         


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
               
                this.BackColor = Color.White;
                this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
                this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
                this.ControlBox = true;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                Office2010Colors.ApplyManagedColors(this, Color.White);
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
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv.ColumnHeadersHeight = 30;
        }

        public frmKopiranjeCenovnikaPoTipu()
        {
            InitializeComponent();
        }

        private void frmKopiranjeCenovnikaPoTipu_Load(object sender, EventArgs e)
        {
            var select3 = " select ID, Naziv from TipCenovnika";
            var s_connection3 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboTipCenovnika1.DataSource = ds3.Tables[0];
            cboTipCenovnika1.DisplayMember = "Naziv";
            cboTipCenovnika1.ValueMember = "ID";

            var select4 = " select ID, Naziv from TipCenovnika";
            var s_connection4 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection4 = new SqlConnection(s_connection4);
            var c4 = new SqlConnection(s_connection4);
            var dataAdapter4 = new SqlDataAdapter(select4, c4);

            var commandBuilder4 = new SqlCommandBuilder(dataAdapter4);
            var ds4 = new DataSet();
            dataAdapter4.Fill(ds4);
            cboTipCenovnika2.DataSource = ds4.Tables[0];
            cboTipCenovnika2.DisplayMember = "Naziv";
            cboTipCenovnika2.ValueMember = "ID";


            var select6 = " Select PaSifra,PaNaziv from Partnerji order by PaNaziv ";
            var s_connection6 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection6 = new SqlConnection(s_connection6);
            var c6 = new SqlConnection(s_connection6);
            var dataAdapter6 = new SqlDataAdapter(select6, c6);

            var commandBuilder6 = new SqlCommandBuilder(dataAdapter6);
            var ds6 = new DataSet();
            dataAdapter6.Fill(ds6);
            cboPartner1.DataSource = ds6.Tables[0];
            cboPartner1.DisplayMember = "PaNaziv";
            cboPartner1.ValueMember = "PaSifra";

            var select7 = " Select PaSifra,PaNaziv from Partnerji order by PaNaziv ";
            var s_connection7 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection7 = new SqlConnection(s_connection7);
            var c7 = new SqlConnection(s_connection7);
            var dataAdapter7 = new SqlDataAdapter(select7, c7);

            var commandBuilder7 = new SqlCommandBuilder(dataAdapter7);
            var ds7 = new DataSet();
            dataAdapter7.Fill(ds7);
            cboPartner2.DataSource = ds7.Tables[0];
            cboPartner2.DisplayMember = "PaNaziv";
            cboPartner2.ValueMember = "PaSifra";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni?", "Kopiranje cenovnika po tipu 1 i partneru 1 U tip 2 i Partnera 2", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Projekat.InsertCene ins = new Projekat.InsertCene();
                ins.KopirajCeneTip(Convert.ToInt32(cboTipCenovnika1.SelectedValue), Convert.ToInt32(cboTipCenovnika2.SelectedValue), Convert.ToInt32(cboPartner1.SelectedValue), Convert.ToInt32(cboPartner2.SelectedValue), txtNaziv.Text);
                RefreshDataGridTipCenovnika2();
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }

            // ins.InsVrstaManipulacije(txtNaziv.Text, Convert.ToDateTime(DateTime.Now), KorisnikCene, txtJM.Text, uticeskladisno, txtJM2.Text, Convert.ToInt32(cboTipManipulacije.SelectedValue), Convert.ToInt32(cboOrgJed.SelectedValue), txtOznaka.Text, txtRelacija.Text, Convert.ToDouble(txtCena.Value));

        }

        private void RefreshDataGridRazred()
        {
            var select = " SELECT Cene.[ID] as ID ,[TipCenovnika].Naziv as TipCenovnika,[Partnerji].PaNaziv as Partner,Cene.[Cena],Cene.Cena2,[VrstaManipulacije].Naziv as VrstaManipulacije,Cene.[Datum],Cene.[Korisnik], VrstePostupakaUvoz.Naziv, p2.PaNaziv as Uvoznik FROM [dbo].[Cene] " +
           " inner join TipCenovnika on TipCenovnika.ID = Cene.TipCenovnika " +
           " inner join Partnerji on Partnerji.PaSifra = Cene.Komitent " +
            " inner join Partnerji p2 on p2.PaSifra = Cene.Uvoznik " +
            " inner join VrstePostupakaUvoz  on VrstePostupakaUvoz.ID = Cene.PostupakSaRobom " +
           " inner join VrstaManipulacije on VrstaManipulacije.Id = Cene.VrstaManipulacije  where Razred = '" + cboRazred.Text + "'" +
           " order by Cene.ID desc";
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

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

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            /*
             DataGridViewColumn column = dataGridView1.Columns[0];
             dataGridView1.Columns[0].HeaderText = "ID";
             dataGridView1.Columns[0].Width = 50;

             DataGridViewColumn column2 = dataGridView1.Columns[1];
             dataGridView1.Columns[1].HeaderText = "Naziv";
             dataGridView1.Columns[1].Width = 350;
            */




        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshDataGridRazred();
        }

        private void RefreshDataGridTipCenovnika2()
        {
            var select = " SELECT Cene.[ID] as ID ,[TipCenovnika].Naziv as TipCenovnika,[Partnerji].PaNaziv as Partner,Cene.[Cena],Cena2,[VrstaManipulacije].Naziv as VrstaManipulacije,Cene.[Datum],Cene.[Korisnik], VrstePostupakaUvoz.Naziv, p2.PaNaziv as Uvoznik FROM[dbo].[Cene] " +
            " inner join TipCenovnika on TipCenovnika.ID = Cene.TipCenovnika " +
            " inner join Partnerji on Partnerji.PaSifra = Cene.Komitent " +
             " inner join Partnerji p2 on p2.PaSifra = Cene.Uvoznik " +
             " inner join VrstePostupakaUvoz  on VrstePostupakaUvoz.ID = Cene.PostupakSaRobom " +
            " inner join VrstaManipulacije on VrstaManipulacije.Id = Cene.VrstaManipulacije  where TipCenovnika = " + cboTipCenovnika2.SelectedValue + " and Cene.Komitent =  " + Convert.ToInt32(cboPartner2.SelectedValue) +
            " order by Cene.ID desc";
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

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

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            /*
             DataGridViewColumn column = dataGridView1.Columns[0];
             dataGridView1.Columns[0].HeaderText = "ID";
             dataGridView1.Columns[0].Width = 50;

             DataGridViewColumn column2 = dataGridView1.Columns[1];
             dataGridView1.Columns[1].HeaderText = "Naziv";
             dataGridView1.Columns[1].Width = 350;
            */




        }

        private void RefreshDataGridTipCenovnika1()
        {
            var select = " SELECT Cene.[ID] as ID ,[TipCenovnika].Naziv as TipCenovnika,[Partnerji].PaNaziv as Partner,Cene.[Cena],Cena2,[VrstaManipulacije].Naziv as VrstaManipulacije,Cene.[Datum],Cene.[Korisnik], VrstePostupakaUvoz.Naziv, p2.PaNaziv as Uvoznik FROM[dbo].[Cene] " +
            " inner join TipCenovnika on TipCenovnika.ID = Cene.TipCenovnika " +
            " inner join Partnerji on Partnerji.PaSifra = Cene.Komitent " +
             " inner join Partnerji p2 on p2.PaSifra = Cene.Uvoznik " +
             " inner join VrstePostupakaUvoz  on VrstePostupakaUvoz.ID = Cene.PostupakSaRobom " +
            " inner join VrstaManipulacije on VrstaManipulacije.Id = Cene.VrstaManipulacije  where TipCenovnika = " + cboTipCenovnika1.SelectedValue + " and Cene.Komitent =  " + Convert.ToInt32(cboPartner1.SelectedValue) +
            " order by Cene.ID desc";
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

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

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            /*
             DataGridViewColumn column = dataGridView1.Columns[0];
             dataGridView1.Columns[0].HeaderText = "ID";
             dataGridView1.Columns[0].Width = 50;

             DataGridViewColumn column2 = dataGridView1.Columns[1];
             dataGridView1.Columns[1].HeaderText = "Naziv";
             dataGridView1.Columns[1].Width = 350;
            */




        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void tsDelete_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            RefreshDataGridTipCenovnika2();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefreshDataGridTipCenovnika1();
        }
    }
}
