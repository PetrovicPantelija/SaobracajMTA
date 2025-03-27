﻿using Syncfusion.Windows.Forms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;



//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp;

namespace Saobracaj.Sifarnici
{
    public partial class frmTrase : Form
    {
        public static string code = "frmTrase";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Sifarnici.frmLogovanje.user.ToString();
        bool status = false;
        int pomTrasa = 0;
        string niz = "";
        public frmTrase()
        {
            InitializeComponent();
            ChangeTextBox();

        }

        public frmTrase(int Trasa)
        {
            InitializeComponent();
            ChangeTextBox();
            pomTrasa = Trasa;
        }

        private void ChangeTextBox()
        {
            panelHeader.Visible = false;

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
                panelHeader.Visible = false;
                meniHeader.Visible = true;
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
        private void RefreshDataGrid()
        {
            var select = " SELECT     Trase.ID,  stanice.Opis as Pocetna, " +
                      " stanice_1.Opis AS Krajnja, Trase.Relacija, Trase.OpisRelacije, Trase.Voz, Trase.Rang, Trase.TezinaVoza, Trase.TezinaLokomotive, Trase.Prevoznik, Trase.DuzinaVoza, " +
                      " Trase.ProcenatKocenja, Trase.Cena, Trase.Rastojanje, CONVERT(VARCHAR(8),Trase.VremePolaska,108) as VremePolaska, CONVERT(VARCHAR(8),Trase.VremeDolaska,108) as VremeDolaska, Trase.DuzinaTrajanja, Trase.Rezi, Trase.Godina " +
                      " FROM         Trase INNER JOIN " +
                      " stanice ON Trase.Pocetna = stanice.ID INNER JOIN " +
                      " stanice AS stanice_1 ON Trase.Krajnja = stanice_1.ID Order By Trase.ID desc ";
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Početna";
            dataGridView1.Columns[1].Width = 100;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Krajnja";
            dataGridView1.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Relacija";
            dataGridView1.Columns[3].Width = 150;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Opis relacije";
            dataGridView1.Columns[4].Width = 150;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Voz";
            dataGridView1.Columns[5].Width = 50;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Rang";
            dataGridView1.Columns[6].Width = 30;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Težina voza";
            dataGridView1.Columns[7].Width = 30;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Težina lokomotive";
            dataGridView1.Columns[8].Width = 30;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Prevoznik";
            dataGridView1.Columns[9].Width = 70;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Dužina voza";
            dataGridView1.Columns[10].Width = 70;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Procenat kočenja";
            dataGridView1.Columns[11].Width = 70;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Cena";
            dataGridView1.Columns[12].Width = 90;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Rastojanje";
            dataGridView1.Columns[13].Width = 70;

            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Vreme polaska";
            dataGridView1.Columns[14].Width = 70;

            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Vreme dolaska";
            dataGridView1.Columns[15].Width = 70;

            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Dužina trajanja";
            dataGridView1.Columns[16].Width = 70;

            DataGridViewColumn column18 = dataGridView1.Columns[17];
            dataGridView1.Columns[17].HeaderText = "Reži";
            dataGridView1.Columns[17].Width = 70;

            DataGridViewColumn column19 = dataGridView1.Columns[18];
            dataGridView1.Columns[18].HeaderText = "Godina";
            dataGridView1.Columns[18].Width = 70;

            /*
            @Cena decimal(18,2),
            @Rastojanje decimal(18,2),
            @VremePolaska datetime,
            @VremeDolaska datetime,
            @DuzinaTrajanja int,
            @Rezi tinyint,
            @Godina nvarchar(4)
            */
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            txtSifra.Text = "";
            txtSifra.Enabled = false;
            txtRelacija.Text = "";
            txtOpisRelacije.Text = "";
            txtVoz.Text = "";
            txtTezinaVozaM.Value = 0;
            txtTezinaLokM.Value = 0;
            txtProcenatKocenjaM.Value = 0;
            txtCenaM.Value = 0;
            txtRastojanjeM.Value = 0;
            txtDuzinaTrajanjaM.Value = 0;
            status = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            int pom = 0;
            if (chkDE.Checked == true)
            {
                pom = 1;
            }

            if (status == true)
            {
                double rastojanje = 0;
                rastojanje = Convert.ToDouble(txtMagistralno.Value) + Convert.ToDouble(txtRegionalno.Value) + Convert.ToDouble(txtLokalne.Value);
                InsertTrase ins = new InsertTrase();
                ins.InsTras(Convert.ToInt32(cmbPocetna.SelectedValue), Convert.ToInt32(cboKrajnja.SelectedValue), txtRelacija.Text, txtOpisRelacije.Text, txtVoz.Text, cmbRang.Text, Convert.ToDouble(txtTezinaVozaM.Text), Convert.ToDouble(txtTezinaLokM.Text), Convert.ToInt32(cboPrevoznik.SelectedValue), Convert.ToDouble(txtDuzinaLokomM.Text), Convert.ToDouble(txtProcenatKocenjaM.Text), Convert.ToDouble(txtCenaM.Text), Convert.ToDouble(txtRastojanjeM.Text), dtpVremePolaska.Value, dtpVremeDolaska.Value, Convert.ToInt32(txtDuzinaTrajanjaM.Text), chkRezi.Checked, cboGodinaVazenja.Text, Convert.ToDouble(txtCenaKalk.Value), Convert.ToDouble(txtMagistralno.Value), Convert.ToDouble(txtRegionalno.Value), Convert.ToDouble(txtLokalne.Value), Convert.ToDouble(txtDana.Value), Convert.ToDouble(txtNajveciOtpor.Value), Convert.ToDouble(txtNajkraciKolosek.Value), Convert.ToDouble(txtOsovinskoOpterecenje.Value), pom, Convert.ToDouble(txtElektro.Value), Convert.ToDouble(txtDizel.Value), Convert.ToDouble(txtRastojanjeP.Value));
                RefreshDataGrid();
                txtSifra.Enabled = true;
                status = false;

            }
            else
            {
                double rastojanje = 0;
                rastojanje = Convert.ToDouble(txtMagistralno.Value) + Convert.ToDouble(txtRegionalno.Value) + Convert.ToDouble(txtLokalne.Value);

                if (rastojanje == Convert.ToDouble(txtRastojanjeM.Value))
                {
                    InsertTrase upd = new InsertTrase();
                    upd.UpdTrase(Convert.ToInt32(txtSifra.Text), Convert.ToInt32(cmbPocetna.SelectedValue), Convert.ToInt32(cboKrajnja.SelectedValue), txtRelacija.Text, txtOpisRelacije.Text, txtVoz.Text, cmbRang.Text, Convert.ToDouble(txtTezinaVozaM.Text), Convert.ToDouble(txtTezinaLokM.Text), Convert.ToInt32(cboPrevoznik.SelectedValue), Convert.ToDouble(txtDuzinaLokomM.Text), Convert.ToDouble(txtProcenatKocenjaM.Text), Convert.ToDouble(txtCenaM.Text), Convert.ToDouble(txtRastojanjeM.Text), dtpVremePolaska.Value, dtpVremeDolaska.Value, Convert.ToInt32(txtDuzinaTrajanjaM.Text), chkRezi.Checked, cboGodinaVazenja.Text, Convert.ToDouble(txtCenaKalk.Value), Convert.ToDouble(txtMagistralno.Value), Convert.ToDouble(txtRegionalno.Value), Convert.ToDouble(txtLokalne.Value), Convert.ToDouble(txtDana.Value), Convert.ToDouble(txtNajveciOtpor.Value), Convert.ToDouble(txtNajkraciKolosek.Value), Convert.ToDouble(txtOsovinskoOpterecenje.Value), pom, Convert.ToDouble(txtElektro.Value), Convert.ToDouble(txtDizel.Value), Convert.ToDouble(txtRastojanjeP.Value));
                    status = false;
                    txtSifra.Enabled = false;
                    RefreshDataGrid();
                }
                else
                {
                    MessageBox.Show("Zbir rastojanja nije isto Ukupno rastojanje");
                }
            }
        }

        private void cmbRang_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertTrase del = new InsertTrase();
            del.DeleteTrase(Convert.ToInt32(txtSifra.Text));
            status = false;
            txtSifra.Enabled = false;
            RefreshDataGrid();
        }

        private void frmTrase_Load(object sender, EventArgs e)
        {
            var select = " Select Distinct ID, RTrim(Opis) as Stanica From Stanice";
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cmbPocetna.DataSource = ds.Tables[0];
            cmbPocetna.DisplayMember = "Stanica";
            cmbPocetna.ValueMember = "ID";

            var select2 = " Select Distinct ID, RTrim(Opis) as Stanica From Stanice";
            var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboKrajnja.DataSource = ds2.Tables[0];
            cboKrajnja.DisplayMember = "Stanica";
            cboKrajnja.ValueMember = "ID";

            var select3 = " Select Distinct PaSifra, RTrim(PaNaziv) as Partner From Partnerji where Prevoznik = 1";
            var s_connection3 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboPrevoznik.DataSource = ds3.Tables[0];
            cboPrevoznik.DisplayMember = "Partner";
            cboPrevoznik.ValueMember = "PaSifra";

            RefreshDataGrid();
            if (pomTrasa != 0)
            {
                VratiPodatke(pomTrasa);
            }
        }

        private void btnSastaviStanice_Click(object sender, EventArgs e)
        {
            txtRelacija.Text = cmbPocetna.Text + "-" + cboKrajnja.Text;
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void dtpVremeDolaska_Leave(object sender, EventArgs e)
        {
            TimeSpan span = dtpVremeDolaska.Value - dtpVremePolaska.Value;
            double totalMinutes = span.TotalMinutes;
            txtDuzinaTrajanjaM.Value = Convert.ToDecimal(totalMinutes);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmTrasaStanice frmTS = new frmTrasaStanice(txtSifra.Text, txtVoz.Text);
            frmTS.Show();
        }

        private void VratiPodatke(int IdTrase)
        {
            bool temp = false;

            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select * from Trase where ID=" + IdTrase, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                /*
                StanicaOD.SelectedValue = Convert.ToInt32(dr["StanicaOd"].ToString());
                stanicaDO.SelectedValue = Convert.ToInt32(dr["StanicaDo"].ToString());
               
                RastojanjeM.Value = Convert.ToInt32(dr["RastojanjeM"].ToString());
                StacionazaKM.Value = Convert.ToInt32(dr["StacionazaKM"].ToString());
                StacionazaM.Value = Convert.ToInt32(dr["StacionazaM"].ToString());
                VmaxL.Value = Convert.ToInt32(dr["VmaxL"].ToString());
                VmaxD.Value = Convert.ToInt32(dr["VmaxD"].ToString());
                OsOtpor.Text = dr["OsOtpor"].ToString();
                Vaga.Text = dr["Vaga"].ToString();
                PutTer.Text = dr["PutTer"].ToString();
                Nagib.Value = Convert.ToDecimal(dr["Nagib"].ToString());
                MNU.Value = Convert.ToDecimal(dr["MNU"].ToString());
                MNP.Value = Convert.ToDecimal(dr["MNP"].ToString());
             */

                cmbPocetna.SelectedValue = Convert.ToInt32(dr["Pocetna"].ToString());
                cboKrajnja.SelectedValue = Convert.ToInt32(dr["Krajnja"].ToString());
                txtRelacija.Text = dr["Relacija"].ToString();
                txtOpisRelacije.Text = dr["OpisRelacije"].ToString();
                txtVoz.Text = dr["Voz"].ToString();
                cmbRang.Text = dr["Rang"].ToString();
                txtTezinaVozaM.Value = Convert.ToDecimal(dr["TezinaVoza"].ToString());
                txtTezinaLokM.Value = Convert.ToDecimal(dr["TezinaLokomotive"].ToString());
                cboPrevoznik.SelectedValue = Convert.ToInt32(dr["Prevoznik"].ToString());
                txtDuzinaLokomM.Value = Convert.ToDecimal(dr["DuzinaVoza"].ToString());
                txtProcenatKocenjaM.Value = Convert.ToDecimal(dr["ProcenatKocenja"].ToString());
                txtCenaM.Value = Convert.ToDecimal(dr["Cena"].ToString());
                txtRastojanjeM.Value = Convert.ToDecimal(dr["Rastojanje"].ToString());
                dtpVremePolaska.Value = Convert.ToDateTime(dr["VremePolaska"].ToString());
                dtpVremeDolaska.Value = Convert.ToDateTime(dr["VremeDolaska"].ToString());
                txtDuzinaTrajanjaM.Value = Convert.ToInt32(dr["DuzinaTrajanja"].ToString());
                if (Convert.ToInt32(dr["Rezi"].ToString()) == 0)
                {
                    chkRezi.Checked = false;
                }
                else
                {
                    chkRezi.Checked = true;
                }
                cboGodinaVazenja.Text = dr["Godina"].ToString();
                txtCenaKalk.Value = Convert.ToDecimal(dr["CenaKalk"].ToString());
                txtMagistralno.Value = Convert.ToDecimal(dr["RastojanjeMag"].ToString());
                txtRegionalno.Value = Convert.ToDecimal(dr["RastojanjeReg"].ToString());
                txtLokalne.Value = Convert.ToDecimal(dr["RastojanjeLok"].ToString());
                txtDana.Value = Convert.ToDecimal(dr["Dana"].ToString());
                txtNajveciOtpor.Value = Convert.ToDecimal(dr["NajveciOtpor"].ToString());
                txtNajkraciKolosek.Value = Convert.ToDecimal(dr["NajkraciKol"].ToString());
                txtOsovinskoOpterecenje.Value = Convert.ToDecimal(dr["OsovinskoOpter"].ToString());
                txtRastojanjeP.Value = Convert.ToDecimal(dr["PrevoznoRastojanje"].ToString());
                if (Convert.ToInt32(dr["TipED"].ToString()) == 0)
                {
                    chkDE.Checked = false;
                }
                else
                {
                    chkDE.Checked = true;
                }
                txtElektro.Value = Convert.ToDecimal(dr["ElektroKM"].ToString());
                txtDizel.Value = Convert.ToDecimal(dr["DizelKM"].ToString());
                temp = true;
            }

            if (temp == false)

                MessageBox.Show("not found");
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
                        txtSifra.Text = row.Cells[0].Value.ToString();
                        VratiPodatke(Convert.ToInt32(row.Cells[0].Value.ToString()));

                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela promena stavki");
            }
        }

        private void chkDE_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDE.Checked == true)
            {
                txtDizel.Enabled = true;
                txtElektro.Enabled = false;
            }
            else
            {
                txtDizel.Enabled = true;
                txtElektro.Enabled = true;
            }
        }

        private void btnRacun_Click(object sender, EventArgs e)
        {
            string PictureFolder = txtTrase.Text;
            ofd1.InitialDirectory = PictureFolder;

            if (ofd1.ShowDialog() == DialogResult.OK)
            {
                txtTrase.Text = fbd.SelectedPath.ToString() + ofd1.FileName;
            }
        }

        private void KopirajFajlPoTipu(string putanja, string FolderDestinacije, int Tip)
        {
            string fileName = ofd1.FileName; //Ovde ce trebati promena
            fileName = fileName.Replace(" ", "_");
            string sourcePath = fbd.SelectedPath.ToString();
            string result = Path.GetFileName(fileName);
            string targetPath = "";

            if (Tip == 1)
            {
                targetPath = @"\\192.168.129.7\TA\Saobracaj\Listice\" + FolderDestinacije;
            }
            else if (Tip == 2)
            {
                targetPath = @"\\192.168.129.7\TA\Saobracaj\" + FolderDestinacije + @"\CIM";
            }
            else if (Tip == 3)
            {
                targetPath = @"\\192.168.129.7\TA\Saobracaj\" + FolderDestinacije + @"\CIT23";
            }
            else if (Tip == 4)
            {
                targetPath = @"\\192.168.129.7\TA\Saobracaj\" + FolderDestinacije + @"\Racuni";
            }
            else if (Tip == 5)
            {
                targetPath = @"\\192.168.129.7\TA\Saobracaj\" + FolderDestinacije + @"\PrijemnaTeretnica";
            }
            else
            {
                targetPath = @"\\192.168.129.7\TA\Saobracaj\" + FolderDestinacije;
            }

            string sourceFile = putanja;
            string destFile = System.IO.Path.Combine(targetPath, result);

            if (!System.IO.Directory.Exists(targetPath))
            {
                System.IO.Directory.CreateDirectory(targetPath);
            }

            var remote = Path.Combine(targetPath, result);
            File.Copy(sourceFile, remote);
            if (Tip == 1)
            {
                txtTrase.Text = remote;
            }
            else
            {
                // txtPutanja.Text = remote;
            }



            if (System.IO.Directory.Exists(sourcePath))
            {
                string[] files = System.IO.Directory.GetFiles(sourcePath);
                // Copy the files and overwrite destination files if they already exist.
                foreach (string s in files)
                {
                    // Use static Path methods to extract only the file name from the path.
                    fileName = System.IO.Path.GetFileName(s);
                    destFile = System.IO.Path.Combine(targetPath, fileName);
                    System.IO.File.Copy(s, destFile, true);
                }
            }
        }

        private void btnRacunSacuvaj_Click(object sender, EventArgs e)
        {

            Dokumenta.InsertTraseDokumenta ins = new Dokumenta.InsertTraseDokumenta();
            KopirajFajlPoTipu(txtTrase.Text, txtVoz.Text.TrimEnd(), 1);
            ins.InsTraseDokumenta(Convert.ToInt32(txtVoz.Text), txtTrase.Text.TrimEnd());
            RefreshDataGrid();

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Dokumenta.frmRutaTrase tr = new Dokumenta.frmRutaTrase(txtSifra.Text, txtVoz.Text);
            tr.Show();
        }
    }
}
