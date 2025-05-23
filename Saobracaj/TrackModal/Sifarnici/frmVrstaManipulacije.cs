﻿using Syncfusion.Windows.Forms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Testiranje.Sifarnici
{
    public partial class frmVrstaManipulacije : Form
    {
        public static string code = "frmVrstaManipulacije";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Saobracaj.Sifarnici.frmLogovanje.user.ToString();
        string niz = "";

        string KorisnikCene = "Panta";
        bool status = false;

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

        public frmVrstaManipulacije()
        {
            InitializeComponent();
            ChangeTextBox();
            KorisnikCene = Saobracaj.Sifarnici.frmLogovanje.user;

        }

        public frmVrstaManipulacije(string Korisnik)
        {
            InitializeComponent();
            ChangeTextBox();
            KorisnikCene = Korisnik;

        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtSifra.Enabled = false;
            txtNaziv.Text = "";
            txtJM.Text = "";
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            int uticeskladisno = 0;
            if (chkUticeSkladisno.Checked == true)
            {
                uticeskladisno = 1;

            }

            int administrativna = 0;
            if (chkAdministratvna.Checked == true)
            {
                administrativna = 1;

            }

            int drumska = 0;
            if (chkDrumski.Checked == true)
            {
                drumska = 1;

            }

            int dodatna = 0;
            if (chkDodatna.Checked == true)
            {
                dodatna = 1;

            }

            int potvrdauradio = 0;
            if (chkPotvrdaUradio.Checked == true)
            {
                potvrdauradio = 1;

            }

            if (status == true)
            {
                InsertVrstaManipulacije ins = new InsertVrstaManipulacije();
                ins.InsVrstaManipulacije(txtNaziv.Text, Convert.ToDateTime(DateTime.Now), KorisnikCene, txtJM.Text, uticeskladisno, txtJM2.Text, Convert.ToInt32(cboTipManipulacije.SelectedValue), Convert.ToInt32(cboOrgJed.SelectedValue), txtOznaka.Text, txtRelacija.Text, Convert.ToDouble(txtCena.Value), Convert.ToInt32(cboGrupaVrsteManipulacije.SelectedValue), administrativna, drumska, dodatna, potvrdauradio, txtApstrakt1.Text, txtApstrakt2.Text,Convert.ToInt32(txtTipKont.SelectedValue), Convert.ToInt32(cboRLTerminal.SelectedValue), Convert.ToInt32(cboRLTerminal2.SelectedValue), Convert.ToInt32(cboRLTerminal3.SelectedValue));
                status = false;
            }
            else
            {
                //int TipCenovnika ,int Komitent, double Cena , int VrstaManipulacije ,DateTime  Datum , string Korisnik
                InsertVrstaManipulacije upd = new InsertVrstaManipulacije();
                upd.UpdVrstaManipulacije(Convert.ToInt32(txtSifra.Text), txtNaziv.Text, Convert.ToDateTime(DateTime.Now), KorisnikCene, txtJM.Text, uticeskladisno, txtJM2.Text, Convert.ToInt32(cboTipManipulacije.SelectedValue), Convert.ToInt32(cboOrgJed.SelectedValue), txtOznaka.Text, txtRelacija.Text, Convert.ToDouble(txtCena.Value), Convert.ToInt32(cboGrupaVrsteManipulacije.SelectedValue), administrativna, drumska, dodatna,potvrdauradio, txtApstrakt1.Text, txtApstrakt2.Text,Convert.ToInt32(txtTipKont.SelectedValue), Convert.ToInt32(cboRLTerminal.SelectedValue), Convert.ToInt32(cboRLTerminal2.SelectedValue), Convert.ToInt32(cboRLTerminal3.SelectedValue));
            }
            RefreshDataGrid();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da želite da brišete?", "Izbor", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                InsertVrstaManipulacije upd = new InsertVrstaManipulacije();
                upd.DeleteVrstaManipulacije(Convert.ToInt32(txtSifra.Text));
                RefreshDataGrid();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }


        }

        private void RefreshDataGrid()
        {
            var select = " SELECT VrstaManipulacije.[ID] as VID,VrstaManipulacije.Naziv as VrstaM,VrstaManipulacije.JM as JMM," +
                " CASE WHEN UticeSkladisno > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as UticeSkladisno, " +
                "VrstaManipulacije.[Datum] as Dat,VrstaManipulacije.[Korisnik] as Kor, TipManipulacije, VrstaManipulacije.OrgJed, " +
                " OrganizacioneJedinice.Naziv, VrstaManipulacije.Oznaka, VrstaManipulacije.Relacija,VrstaManipulacije.Cena, " +
                " CASE WHEN Administrativna > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Administrativna," +
                "CASE WHEN Drumska > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Drumska,TipKontejnera as [Vrsta kontejnera]  " +
             " FROM [dbo].[VrstaManipulacije] " +
             " inner join OrganizacioneJedinice on OrganizacioneJedinice.ID = VrstaManipulacije.OrgJed" +
             " order by VrstaManipulacije.[ID]";
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                PodesiDatagridView(dataGridView1);
            }
            else
            {
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
            }

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Naziv";
            dataGridView1.Columns[1].Width = 350;


            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "JM";
            dataGridView1.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Utiče skladišno";
            dataGridView1.Columns[3].Width = 100;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Datum";
            dataGridView1.Columns[4].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Korisnik";
            dataGridView1.Columns[5].Width = 100;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Tip Manipulacije";
            dataGridView1.Columns[6].Width = 40;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "OrgJed";
            dataGridView1.Columns[7].Width = 80;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "OrgJed";
            dataGridView1.Columns[8].Width = 80;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Oznaka";
            dataGridView1.Columns[9].Width = 80;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Relacija";
            dataGridView1.Columns[10].Width = 120;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Cena";
            dataGridView1.Columns[11].Width = 80;


        }

        private void VratiPodatke(string ID)
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT ID , Naziv, JM, UticeSkladisno, TipManipulacije, OrgJed, Oznaka,Relacija, Cena, GrupaVrsteManipulacijeID, Administrativna, Drumska, Dodatna, PotvrdaUradio, Apstrakt1, Apstrakt2, TipKontejnera, RlTerminali, RLTerminali2, RLTerminali3 from VrstaManipulacije where ID=" + txtSifra.Text, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                // Convert.ToInt32(cboTipCenovnika.SelectedValue), Convert.ToInt32(cboKomitent.SelectedValue), Convert.ToDouble(txtCena.Text), Convert.ToInt32(cboVrstaManipulacije.SelectedValue), Convert.ToDateTime(DateTime.Now), KorisnikCene

                txtNaziv.Text = dr["Naziv"].ToString();
                txtJM.Text = dr["JM"].ToString();
                if (dr["UticeSkladisno"].ToString() == "True")
                { chkUticeSkladisno.Checked = true; }
                else { chkUticeSkladisno.Checked = false; }

                cboTipManipulacije.SelectedValue = Convert.ToInt32(dr["TipManipulacije"].ToString());
                cboOrgJed.SelectedValue = Convert.ToInt32(dr["OrgJed"].ToString());
                txtOznaka.Text = dr["Oznaka"].ToString();
                txtRelacija.Text = dr["Relacija"].ToString();
                txtCena.Value = Convert.ToDecimal(dr["Cena"].ToString());
                cboGrupaVrsteManipulacije.SelectedValue = Convert.ToInt32(dr["GrupaVrsteManipulacijeID"].ToString());

                if (dr["Administrativna"].ToString() == "1")
                { chkAdministratvna.Checked = true; }
                else { chkAdministratvna.Checked = false; }

                if (dr["Drumska"].ToString() == "1")
                { chkDrumski.Checked = true; }
                else { chkDrumski.Checked = false; }

                if (dr["Dodatna"].ToString() == "1")
                { chkDodatna.Checked = true; }
                else { chkDodatna.Checked = false; }

                if (dr["PotvrdaUradio"].ToString() == "1")
                { chkPotvrdaUradio.Checked = true; }
                else { chkPotvrdaUradio.Checked = false; }

                txtApstrakt1.Text = dr["Apstrakt1"].ToString();
                txtApstrakt2.Text = dr["Apstrakt2"].ToString();

                txtTipKont.SelectedValue = Convert.ToInt32(dr["TipKontejnera"].ToString());

                cboRLTerminal.SelectedValue = Convert.ToInt32(dr["RLTerminali"].ToString());
                cboRLTerminal2.SelectedValue = Convert.ToInt32(dr["RLTerminali2"].ToString());
                cboRLTerminal3.SelectedValue = Convert.ToInt32(dr["RLTerminali3"].ToString());
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
                        txtSifra.Text = row.Cells[0].Value.ToString();
                        VratiPodatke(txtSifra.Text);
                        if (chkDodatna.Checked == true)
                        {
                            panelDodatnaUluga.Visible = true;
                        }
                        else
                        {
                            panelDodatnaUluga.Visible = false;
                        }
                        // txtOpis.Text = row.Cells[1].Value.ToString();
                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void frmVrstaManipulacije_Load(object sender, EventArgs e)
        {
            txtCena.Visible = false;
            label13.Visible = false;
            label9.Visible = false;

            RefreshDataGrid();

            var select = " Select ID, Naziv From TipManipulacije";
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboTipManipulacije.DataSource = ds.Tables[0];
            cboTipManipulacije.DisplayMember = "Naziv";
            cboTipManipulacije.ValueMember = "ID";

            // SELECT ID, Naziv from OrganizacioneJedinice

            var select2 = " SELECT ID, Naziv from OrganizacioneJedinice order by ID";
            var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboOrgJed.DataSource = ds2.Tables[0];
            cboOrgJed.DisplayMember = "Naziv";
            cboOrgJed.ValueMember = "ID";


            var select3 = " SELECT ID, Naziv from GrupaVrsteManipulacije order by ID";
            var s_connection3 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboGrupaVrsteManipulacije.DataSource = ds3.Tables[0];
            cboGrupaVrsteManipulacije.DisplayMember = "Naziv";
            cboGrupaVrsteManipulacije.ValueMember = "ID";


            var tipkontejnera = "Select ID, SkNaziv From TipKontenjera order by SkNaziv";
            var s_connection4 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection4 = new SqlConnection(s_connection4);
            var tkAD = new SqlDataAdapter(tipkontejnera, myConnection4);
            var tkDS = new DataSet();
            tkAD.Fill(tkDS);
            txtTipKont.DataSource = tkDS.Tables[0];
            txtTipKont.DisplayMember = "SkNaziv";
            txtTipKont.ValueMember = "ID";


            var jmr1 = "Select MeSifra, RTrim(MeNaziv) as MeNaziv From MerskeEnote order by MeSifra";
            var s_connection5 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection5 = new SqlConnection(s_connection5);
            var tkAD2 = new SqlDataAdapter(jmr1, myConnection5);
            var tkDS2 = new DataSet();
            tkAD2.Fill(tkDS2);
            txtJM.DataSource = tkDS2.Tables[0];
            txtJM.DisplayMember = "MeNaziv";
            txtJM.ValueMember = "MeSifra";


            var jmr2 = "Select MeSifra, RTRIM(MeNaziv) as MeNaziv From MerskeEnote order by MeSifra";
            var s_connection6 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection6 = new SqlConnection(s_connection6);
            var tkAD3 = new SqlDataAdapter(jmr2, myConnection6);
            var tkDS3 = new DataSet();
            tkAD3.Fill(tkDS3);
            txtJM2.DataSource = tkDS3.Tables[0];
            txtJM2.DisplayMember = "MeNaziv";
            txtJM2.ValueMember = "MeSifra";

            //Kontejnerski terminali
            var rl = "Select ID, (Naziv + ' - ' + Oznaka) as Naziv From KontejnerskiTerminali order by (Naziv + ' ' + Oznaka)";
            var rlSAD = new SqlDataAdapter(rl, s_connection6);
            var rlSDS = new DataSet();
            rlSAD.Fill(rlSDS);
            cboRLTerminal.DataSource = rlSDS.Tables[0];
            cboRLTerminal.DisplayMember = "Naziv";
            cboRLTerminal.ValueMember = "ID";

            //Kontejnerski terminali 2
            var rl2 = "Select ID, (Naziv + ' - ' + Oznaka) as Naziv From KontejnerskiTerminali order by (Naziv + ' ' + Oznaka)";
            var rlSAD2 = new SqlDataAdapter(rl2, s_connection6);
            var rlSDS2 = new DataSet();
            rlSAD2.Fill(rlSDS2);
            cboRLTerminal2.DataSource = rlSDS2.Tables[0];
            cboRLTerminal2.DisplayMember = "Naziv";
            cboRLTerminal2.ValueMember = "ID";

            var rl3 = "Select ID, (Naziv + ' - ' + Oznaka) as Naziv From KontejnerskiTerminali order by (Naziv + ' ' + Oznaka)";
            var rlSAD3 = new SqlDataAdapter(rl3, s_connection6);
            var rlSDS3 = new DataSet();
            rlSAD3.Fill(rlSDS3);
            cboRLTerminal3.DataSource = rlSDS3.Tables[0];
            cboRLTerminal3.DisplayMember = "Naziv";
            cboRLTerminal3.ValueMember = "ID";



        }

        private void tsPrvi_Click(object sender, EventArgs e)
        {

            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Min([ID]) as ID from VrstaManipulacije", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtSifra.Text = dr["ID"].ToString();
            }
            VratiPodatke(txtSifra.Text);
            con.Close();

        }

        private void tsPoslednja_Click(object sender, EventArgs e)
        {


            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Max([ID]) as ID from VrstaManipulacije", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtSifra.Text = dr["ID"].ToString();
            }
            VratiPodatke(txtSifra.Text);
            con.Close();



        }

        private void tsNazad_Click(object sender, EventArgs e)
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            int prvi = 0;
            con.Open();

            SqlCommand cmd = new SqlCommand("select top 1 ID as ID from VrstaManipulacije where ID <" + Convert.ToInt32(txtSifra.Text) + " Order by ID desc", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                prvi = Convert.ToInt32(dr["ID"].ToString());
                txtSifra.Text = prvi.ToString();
            }

            con.Close();
            if ((Convert.ToInt32(txtSifra.Text) - 1) > prvi)
                VratiPodatke((Convert.ToInt32(txtSifra.Text) - 1).ToString());
            else
                VratiPodatke((Convert.ToInt32(prvi)).ToString());
        }

        private void VratiPodatkeMax()
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Max([ID]) as ID from VrstaManipulacije", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtSifra.Text = dr["ID"].ToString();
            }

            con.Close();
        }

        private void tsNapred_Click(object sender, EventArgs e)
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            int zadnji = 0;
            con.Open();

            SqlCommand cmd = new SqlCommand("select top 1 ID as ID from VrstaManipulacije where ID >" + Convert.ToInt32(txtSifra.Text) + " Order by ID", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                zadnji = Convert.ToInt32(dr["ID"].ToString());
                txtSifra.Text = zadnji.ToString();
            }

            con.Close();

            if ((Convert.ToInt32(txtSifra.Text) + 1) == zadnji)
                VratiPodatke((Convert.ToInt32(zadnji).ToString()));
            else
                VratiPodatke((Convert.ToInt32(txtSifra.Text) + 1).ToString());

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void chkDodatna_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDodatna.Checked == true)
            {
                panelDodatnaUluga.Visible = true;
            }
        }

        private void panelDodatnaUluga_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
