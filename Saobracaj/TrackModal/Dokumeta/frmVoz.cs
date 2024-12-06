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
using Saobracaj.Izvoz;
using Syncfusion.Windows.Forms;
using System.IO;



namespace Testiranje.Dokumeta
{
    public partial class frmVoz : Form
    {
        public static string code = "frmVoz";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Saobracaj.Sifarnici.frmLogovanje.user.ToString();
        string niz = "";
        string BrojPlanaUvoza = "";
        string BrojPlanaIzvoza = "";

        string KorisnikCene = Saobracaj.Sifarnici.frmLogovanje.user;
        bool status = false;
        int IzPregleda = 0;
        //  int VozID = 1;
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

        private void ChangeTextBoxPanelLeget()
        {

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                foreach (Control control in panelLeget.Controls)
                {

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
         
        }

        private void ChangeTextBoxPanelLegetOperater()
        {
         

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                foreach (Control control in panelLegetOperater.Controls)
                {

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
           
        }

        public frmVoz()
        {
            InitializeComponent();

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");

            //KorisnikCene = "Panta";

            ProveriFirmu();
            ChangeTextBox();
            ChangeTextBoxPanelLeget();
            ChangeTextBoxPanelLegetOperater();
           
        }
      

        public frmVoz(string Korisnik)
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");

            KorisnikCene = Korisnik;
            KorisnikCene = "Panta";
            ChangeTextBox();
            ChangeTextBoxPanelLeget();
            ChangeTextBoxPanelLegetOperater();
            RefreshDataGrid();

            ProveriFirmu();
   
        }

        public void ProveriFirmu()
        {
            string Company = Saobracaj.Sifarnici.frmLogovanje.Firma;
            switch (Company)
            {
                case "Leget":
                    {
                        panelLeget.Visible = true;
                        panelLegetOperater.Visible = true;
                        panel1.Visible = false;
                        chkTerminal.Visible = true;
                       // panelLegetUvoz.Visible = true;
                        return;

                    }
                default:
                    {
                        toolStripButton1.Visible = false;
                        toolStripButton2.Visible = false;
                        panelLeget.Visible = false;
                        panelLegetOperater.Visible = false;
                        panel1.Visible = true;
                        return;

                    }
                    break;
            }

        }
        
        public frmVoz(int Voz, string Korisnik)
        {
            InitializeComponent();
            KorisnikCene = Kor;
          //  KorisnikCene = "Panta";
            txtSifra.Text = Voz.ToString();
            ChangeTextBox();
            ChangeTextBoxPanelLeget();
            ChangeTextBoxPanelLegetOperater();
            RefreshDataGrid2();
            VratiUkupanBrojKontejnera();
            IzPregleda = 1;
         

        }
        private void tsSave_Click(object sender, EventArgs e)
        {
            int Dolazeci = 0;
            int Ponedeljak = 0;
			int Utorak = 0;
			int Sreda = 0;
			int Cetvrtak = 0;
			int Petak = 0;
			int Subota = 0;
			int Nedelja = 0;
            
            if (chkDolazeci.Checked ==true)  
            Dolazeci = 1;
             if (chkPonedeljak.Checked ==true)  
            Ponedeljak = 1;
             if (chkUtorak.Checked ==true)  
            Utorak = 1;
              if (chkSreda.Checked ==true)  
            Sreda = 1;
                if (chkCetvrtak.Checked ==true)  
            Cetvrtak = 1;
              if (chkPetak.Checked ==true)  
            Petak = 1;
             if (chkSubota.Checked ==true)  
            Subota = 1;
              if (chkNedelja.Checked ==true)  
            Nedelja = 1;
         
			
            
            
            if (status == true)
            {
            InsertVoz ins = new InsertVoz();
            ins.InsVoz(Convert.ToInt32(txtBrVoza.Text), txtRelacija.Text, txtKalendarSaobracaja.Text, Convert.ToDateTime(dtpVremePolaska.Value), 
                Convert.ToDateTime(dtpVremeDolaska.Value), Convert.ToDouble(txtMaksBruto.Value), 
                Convert.ToDouble(txtDuzina.Value), Convert.ToDouble(txtMaksBrojKola.Value), Convert.ToDateTime(dtpVremeZavrsetkaUtovara.Value),
                Convert.ToDateTime(dtpVremeZavrsetkaKP.Value), Convert.ToDateTime(dtpVremePrimopredaje.Value), txtNapomena.Text,
                Convert.ToDateTime(DateTime.Now), KorisnikCene, Dolazeci, Convert.ToInt32(txtPostNaTerminalD.Value),
                Convert.ToInt32(txtKontrolniPregledD.Value), Convert.ToInt32(txtVremeIstovaraD.Value),
                Convert.ToInt32(txtVremePrimopredajeD.Value), Ponedeljak, Utorak, Sreda, Cetvrtak, Petak, Subota, Nedelja, 
                Convert.ToInt32(txtPostNaTerminalO.Value), Convert.ToInt32(txtVremeUtovaraO.Value), 
                Convert.ToInt32(txtVremeKontrolnogO.Value), Convert.ToInt32(txtVremeIzvlacenjaO.Value),
                Convert.ToDateTime(dtpVremePolaskaO.Value), Convert.ToDateTime(dtpVremeDolaskaO.Value),
                Convert.ToInt32(cboStanicaOd.SelectedValue), Convert.ToInt32(cboStanicaDo.SelectedValue), Convert.ToInt32(cboOperater.SelectedValue),
            Convert.ToInt32(cboVlasnik.SelectedValue),
            Convert.ToInt32(cboOperaterSrbija.SelectedValue),
            Convert.ToInt32(cboOperaterHR.SelectedValue),
            Convert.ToDateTime(dtpPlOtpreme.Value),
            Convert.ToDateTime(dtpPLFormiranja.Value),
            Convert.ToDateTime(dtpIzvlacenjeSaTerminala.Value),
            Convert.ToDateTime(dtpPreuzimanjeSM.Value),
            Convert.ToDateTime(dtpPolazakSid.Value),
            Convert.ToDateTime(dtpPredajaHR.Value),
            Convert.ToDateTime(dtpPrispeceRijeka.Value),
            Convert.ToDateTime(dtpIskrcajRijeka.Value),
             Convert.ToDateTime(dtpPristizanjaUSid.Value),
              Convert.ToDateTime(dtpSazeta.Value), txtNazivVoza.Text);
             status = false;
             VratiPodatkeMax();
            }
            else
            {
                //int TipCenovnika ,int Komitent, double Cena , int VrstaManipulacije ,DateTime  Datum , string Korisnik
            InsertVoz upd = new InsertVoz();
            upd.UpdVoz(Convert.ToInt32(txtSifra.Text), Convert.ToInt32(txtBrVoza.Text), txtRelacija.Text, txtKalendarSaobracaja.Text, Convert.ToDateTime(dtpVremePolaska.Value), Convert.ToDateTime(dtpVremeDolaska.Value), Convert.ToDouble(txtMaksBruto.Text), Convert.ToDouble(txtDuzina.Text), Convert.ToDouble(txtMaksBrojKola.Text), Convert.ToDateTime(dtpVremeZavrsetkaUtovara.Value), Convert.ToDateTime(dtpVremeZavrsetkaKP.Value), Convert.ToDateTime(dtpVremePrimopredaje.Value), txtNapomena.Text, Convert.ToDateTime(DateTime.Now),KorisnikCene, Dolazeci, Convert.ToInt32(txtPostNaTerminalD.Value), Convert.ToInt32(txtKontrolniPregledD.Value), Convert.ToInt32(txtVremeIstovaraD.Value), Convert.ToInt32(txtVremePrimopredajeD.Value), Ponedeljak, Utorak, Sreda, Cetvrtak, Petak, Subota, Nedelja, Convert.ToInt32(txtPostNaTerminalO.Value), Convert.ToInt32(txtVremeUtovaraO.Value), Convert.ToInt32(txtVremeKontrolnogO.Value), Convert.ToInt32(txtVremeIzvlacenjaO.Value), dtpVremePolaskaO.Value, dtpVremeDolaskaO.Value, Convert.ToInt32(cboStanicaOd.SelectedValue), Convert.ToInt32(cboStanicaDo.SelectedValue), Convert.ToInt32(cboOperater.SelectedValue),
            Convert.ToInt32(cboVlasnik.SelectedValue),
            Convert.ToInt32(cboOperaterSrbija.SelectedValue),
            Convert.ToInt32(cboOperaterHR.SelectedValue),
            Convert.ToDateTime(dtpPlOtpreme.Value),
            Convert.ToDateTime(dtpPLFormiranja.Value),
            Convert.ToDateTime(dtpIzvlacenjeSaTerminala.Value),
            Convert.ToDateTime(dtpPreuzimanjeSM.Value),
            Convert.ToDateTime(dtpPolazakSid.Value),
            Convert.ToDateTime(dtpPredajaHR.Value),
            Convert.ToDateTime(dtpPrispeceRijeka.Value),
            Convert.ToDateTime(dtpIskrcajRijeka.Value),
              Convert.ToDateTime(dtpPristizanjaUSid.Value),
              Convert.ToDateTime(dtpSazeta.Value), txtNazivVoza.Text);
                status = false;
            }
            RefreshDataGrid();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da želite da brišete?", "Izbor", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                InsertVoz upd = new InsertVoz();
                upd.DeleteVoz(Convert.ToInt32(txtSifra.Text));
                RefreshDataGrid();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }

            
        }

        private void RefreshDataGrid()
        {

            var select = "  SELECT [ID],[BrVoza],[Relacija],[MaksimalnaBruto],[MaksimalnaDuzina],[MaksimalanBrojKola],[Napomena],[Datum],[Korisnik] ,Dolazeci ,VremeDolaskaO, StanicaOd,StanicaDo,Operater  FROM [dbo].[Voz] order by ID desc";
           
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(90, 199, 249); // Selektovana boja
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.DefaultCellStyle.ForeColor = Color.FromArgb(51, 51, 54);
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);


            //Header
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 54);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Br voza";
            dataGridView1.Columns[1].Width = 100;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Relacija";
            dataGridView1.Columns[2].Width = 150;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Maksimalna bruto";
            dataGridView1.Columns[3].Width = 100;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Maksimalna dužina";
            dataGridView1.Columns[4].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Maksimalni broj kola";
            dataGridView1.Columns[5].Width = 100;


            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Napomena";
            dataGridView1.Columns[6].Width = 100;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Datum unosa";
            dataGridView1.Columns[7].Width = 100;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Korisnik";
            dataGridView1.Columns[8].Width = 100;

            VratiPodatke(txtSifra.Text);
        }

        public void VratiPodatke(string ID)
        {
            if (ID == "")
            {
                return;
            }
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();
           
            SqlCommand cmd = new SqlCommand("SELECT [ID],[BrVoza],[Relacija],[KalendarSaobracaja],[VremePolaska],[VremeDolaska],[MaksimalnaBruto],[MaksimalnaDuzina],[MaksimalanBrojKola],[VremeZavrsetkaUtovara],[VremeZavrsetkaKP],[VremePrimopredaje],[Napomena],[Datum],[Korisnik] ,Dolazeci,PostNaTerminalD ,KontrolniPregledD,VremeIstovaraD ,VremePrimopredajeD,Ponedeljak ,Utorak	,Sreda,Cetvrtak,Petak	,Subota ,Nedelja,PostNaTerminalO,VremeUtovaraO ,VremeKontrolnogO ,VremeIzvlacenjaO	,VremePolaskaO ,VremeDolaskaO, StanicaOd, StanicaDo, Napomena, Operater " +
                " ,[Vlasnik]      ,[OperaterSrbija]      ,[OperaterHR]      ,[PlOtpreme]      ,[PLFormiranja]      ,[IzvlacenjeSaTerminala] " +
      " ,[PreuzimanjeSM]      ,[PolazakSid]      ,[PredajaHR]      ,[PrispeceRijeka]      ,[IskrcajRijeka], NazivVoza  FROM [dbo].[Voz] where ID=" + txtSifra.Text, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                
                // Convert.ToInt32(cboTipCenovnika.SelectedValue), Convert.ToInt32(cboKomitent.SelectedValue), Convert.ToDouble(txtCena.Text), Convert.ToInt32(cboVrstaManipulacije.SelectedValue), Convert.ToDateTime(DateTime.Now), KorisnikCene
                txtBrVoza.Text = dr["BrVoza"].ToString();
                txtNazivVoza.Text = dr["NazivVoza"].ToString();
                txtRelacija.Text = dr["Relacija"].ToString();
                txtKalendarSaobracaja.Text = dr["KalendarSaobracaja"].ToString();
                txtNapomena.Text = dr["Napomena"].ToString();
                dtpVremePolaska.Value = Convert.ToDateTime(dr["VremePolaska"].ToString()); 
                dtpVremeDolaska.Value = Convert.ToDateTime(dr["VremeDolaska"].ToString()); 
                dtpVremeZavrsetkaUtovara.Value = Convert.ToDateTime(dr["VremeZavrsetkaUtovara"].ToString()); 
                dtpVremeZavrsetkaKP.Value = Convert.ToDateTime(dr["VremeZavrsetkaKP"].ToString());
                dtpVremePrimopredaje.Value = Convert.ToDateTime(dr["VremePrimopredaje"].ToString());
                txtMaksBruto.Value = Convert.ToDecimal(dr["MaksimalnaBruto"].ToString());
                txtDuzina.Value = Convert.ToDecimal(dr["MaksimalnaDuzina"].ToString());
                txtMaksBrojKola.Value = Convert.ToDecimal(dr["MaksimalanBrojKola"].ToString());
                if (dr["Dolazeci"].ToString() == "1")
                { 
                    chkDolazeci.Checked = true;
                }
                else
                {
                    chkDolazeci.Checked = false;
                }
                if (dr["Ponedeljak"].ToString() == "1")
                { chkPonedeljak.Checked = true; }
                else { chkPonedeljak.Checked = false; }
                if (dr["Utorak"].ToString() == "1")
                { chkUtorak.Checked = true; }
                else { chkUtorak.Checked = false; }
                if (dr["Sreda"].ToString() == "1")
                { chkSreda.Checked = true; }
                else { chkSreda.Checked = false; }
                if (dr["Cetvrtak"].ToString() == "1")
                { chkCetvrtak.Checked = true; }
                else { chkCetvrtak.Checked = false; }
                if (dr["Petak"].ToString() == "1")
                { chkPetak.Checked = true; }
                else { chkPetak.Checked = false; }
                if (dr["Subota"].ToString() == "1")
                { chkSubota.Checked = true; }
                else { chkSubota.Checked = false; }
                if (dr["Nedelja"].ToString() == "1")
                { chkNedelja.Checked = true; }
                else { chkNedelja.Checked = false; }
                txtPostNaTerminalD.Value = Convert.ToInt32(dr["PostNaTerminalD"].ToString());
                txtKontrolniPregledD.Value = Convert.ToInt32(dr["KontrolniPregledD"].ToString());
                txtVremeIstovaraD.Value = Convert.ToInt32(dr["VremeIstovaraD"].ToString());
                 txtVremePrimopredajeD.Value = Convert.ToInt32(dr["VremePrimopredajeD"].ToString());
                 txtPostNaTerminalO.Value = Convert.ToInt32(dr["PostNaTerminalO"].ToString());
                txtPostNaTerminalO.Value = Convert.ToInt32(dr["PostNaTerminalO"].ToString());
                 txtVremeUtovaraO.Value = Convert.ToInt32(dr["VremeUtovaraO"].ToString());
                 txtVremeKontrolnogO.Value = Convert.ToInt32(dr["VremeKontrolnogO"].ToString());
                 txtVremeIzvlacenjaO.Value = Convert.ToInt32(dr["VremeIzvlacenjaO"].ToString());
                 if (dr["VremePolaskaO"].ToString() == "")
                    dtpVremePolaskaO.Value = dtpVremePolaskaO.MinDate;
                else
                dtpVremePolaskaO.Value = Convert.ToDateTime(dr["VremePolaskaO"].ToString());
                 if (dr["VremeDolaskaO"].ToString() == "")
                    dtpVremeDolaskaO.Value = dtpVremeDolaskaO.MinDate;
                else
                dtpVremeDolaskaO.Value = Convert.ToDateTime(dr["VremeDolaskaO"].ToString());

                cboStanicaOd.SelectedValue = Convert.ToInt32(dr["StanicaOd"].ToString());
                cboStanicaDo.SelectedValue = Convert.ToInt32(dr["StanicaDo"].ToString());
                cboOperater.SelectedValue =  Convert.ToInt32(dr["Operater"].ToString());


                cboVlasnik.SelectedValue = Convert.ToInt32(dr["Vlasnik"].ToString());
                cboOperaterSrbija.SelectedValue = Convert.ToInt32(dr["OperaterSrbija"].ToString());
                cboOperaterHR.SelectedValue = Convert.ToInt32(dr["OperaterHR"].ToString());
                dtpPlOtpreme.Value = Convert.ToDateTime(dr["PlOtpreme"].ToString());
                dtpPLFormiranja.Value = Convert.ToDateTime(dr["PLFormiranja"].ToString());
                dtpIzvlacenjeSaTerminala.Value = Convert.ToDateTime(dr["IzvlacenjeSaTerminala"].ToString());
                dtpPreuzimanjeSM.Value = Convert.ToDateTime(dr["PreuzimanjeSM"].ToString());
                dtpPolazakSid.Value = Convert.ToDateTime(dr["PolazakSid"].ToString());
                dtpPredajaHR.Value = Convert.ToDateTime(dr["PredajaHR"].ToString());
                dtpPrispeceRijeka.Value = Convert.ToDateTime(dr["PrispeceRijeka"].ToString());
                dtpIskrcajRijeka.Value = Convert.ToDateTime(dr["IskrcajRijeka"].ToString());
            }
            if (chkDolazeci.Checked == true)
            {
                dtpVremePolaska.Enabled = true;
                dtpVremeDolaska.Enabled = true;
                txtPostNaTerminalD.Enabled = true;
                txtKontrolniPregledD.Enabled = true;
                txtVremeIstovaraD.Enabled = true;
                txtVremePrimopredajeD.Enabled = true;
                txtPostNaTerminalO.Enabled = false;
                txtPostNaTerminalO.Enabled = false;
                txtVremeUtovaraO.Enabled = false;
                txtVremeKontrolnogO.Enabled = false;
                txtVremeIzvlacenjaO.Enabled = false;
                dtpVremePolaskaO.Enabled = false;
                dtpVremeDolaskaO.Enabled = false;
                toolStripButton1.Visible = true;
                toolStripButton2.Visible = false;


                label36.Visible = false;
                dtpPLFormiranja.Visible = false;
                label37.Visible = false;
                dtpIzvlacenjeSaTerminala.Visible = false;
                label38.Visible = false;
                dtpPreuzimanjeSM.Visible = false;
                label39.Visible = false;
                dtpPolazakSid.Visible = false;
                label40.Visible = false;
                dtpPredajaHR.Visible = false;
                label41.Visible = false;
                dtpPrispeceRijeka.Visible = false;
                label42.Visible = false;
                dtpIskrcajRijeka.Visible = false;
                button7.Visible = false;
            }
            else
            {
                dtpVremePolaska.Enabled = false;
                dtpVremeDolaska.Enabled = false;
                txtPostNaTerminalD.Enabled = false;
                txtKontrolniPregledD.Enabled = false;
                txtVremeIstovaraD.Enabled = false;
                txtVremePrimopredajeD.Enabled = false;
                txtPostNaTerminalO.Enabled = true;
                txtPostNaTerminalO.Enabled = true;
                txtVremeUtovaraO.Enabled = true;
                txtVremeKontrolnogO.Enabled = true;
                txtVremeIzvlacenjaO.Enabled = true;
                dtpVremePolaskaO.Enabled = true;
                dtpVremeDolaskaO.Enabled = true;
                toolStripButton1.Visible = false;
                toolStripButton2.Visible = true;


                label36.Visible = true;
                dtpPLFormiranja.Visible = true;
                label37.Visible = true;
                dtpIzvlacenjeSaTerminala.Visible = true;
                label38.Visible = true;
                dtpPreuzimanjeSM.Visible = true;
                label39.Visible = true;
                dtpPolazakSid.Visible = true;
                label40.Visible = true;
                dtpPredajaHR.Visible = true;
                label41.Visible = true;
                dtpPrispeceRijeka.Visible = true;
                label42.Visible = true;
                dtpIskrcajRijeka.Visible = true;
                button7.Visible = true;
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
                        VratiUkupanBrojKontejnera();
                        RefreshDataGrid2();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void VratiPodatkeMax()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Max([ID]) as ID from Voz", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtSifra.Text = dr["ID"].ToString();
            }

            con.Close();
        }
        private void frmVoz_Load(object sender, EventArgs e)
        {
            ProveriFirmu();
            var select = " Select Distinct ID, Rtrim(Opis) as Opis  From Stanice";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboStanicaOd.DataSource = ds.Tables[0];
            cboStanicaOd.DisplayMember = "Opis";
            cboStanicaOd.ValueMember = "ID";

            var select2 = " Select Distinct ID, Rtrim(Opis) as Opis  From Stanice";
            var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboStanicaDo.DataSource = ds2.Tables[0];
            cboStanicaDo.DisplayMember = "Opis";
            cboStanicaDo.ValueMember = "ID";


            var select3 = "  Select Distinct ID, (Naziv + ' BRK: ' + Cast(Broj20 as nvarchar(10))) as Naziv  From SerijeKola";
            var s_connection3 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboSerijaKola.DataSource = ds3.Tables[0];
            cboSerijaKola.DisplayMember = "Naziv";
            cboSerijaKola.ValueMember = "ID";


            var select4 = "  select PaSifra, PaNaziv from Partnerji order by PaNaziv";
            var s_connection4 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection4 = new SqlConnection(s_connection4);
            var c4 = new SqlConnection(s_connection4);
            var dataAdapter4 = new SqlDataAdapter(select4, c4);

            var commandBuilder4 = new SqlCommandBuilder(dataAdapter4);
            var ds4 = new DataSet();
            dataAdapter4.Fill(ds4);
            cboOperater.DataSource = ds4.Tables[0];
            cboOperater.DisplayMember = "PaNaziv";
            cboOperater.ValueMember = "PaSifra";

            var nalogodavac1 = "Select PaSifra,PaNaziv From Partnerji  order by PaNaziv";
            var nal1AD = new SqlDataAdapter(nalogodavac1, s_connection4);
            var nal1DS = new DataSet();
            nal1AD.Fill(nal1DS);
            cboOperaterSrbija.DataSource = nal1DS.Tables[0];
            cboOperaterSrbija.DisplayMember = "PaNaziv";
            cboOperaterSrbija.ValueMember = "PaSifra";

            var nalogodavac2 = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var nal2AD = new SqlDataAdapter(nalogodavac2, s_connection4);
            var nal2DS = new DataSet();
            nal2AD.Fill(nal2DS);
            cboOperaterHR.DataSource = nal2DS.Tables[0];
            cboOperaterHR.DisplayMember = "PaNaziv";
            cboOperaterHR.ValueMember = "PaSifra";

            var nalogodavac3 = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var nal3AD = new SqlDataAdapter(nalogodavac3, s_connection4);
            var nal3DS = new DataSet();
            nal3AD.Fill(nal3DS);
            cboVlasnik.DataSource = nal3DS.Tables[0];
            cboVlasnik.DisplayMember = "PaNaziv";
            cboVlasnik.ValueMember = "PaSifra";

            //cboSerijaKola
            if (IzPregleda == 1)
            {
                VratiPodatke(txtSifra.Text);
            }
  
            RefreshDataGrid();
        }

        private void btnUbaciRelaciju_Click(object sender, EventArgs e)
        {
            txtRelacija.Text = cboStanicaOd.Text + '-' + cboStanicaDo.Text;
        }

        private void chkDolazeci_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDolazeci.Checked == true)
            {
                dtpVremePolaska.Enabled = true;
                dtpVremeDolaska.Enabled = true;
                txtPostNaTerminalD.Enabled = true;
                txtKontrolniPregledD.Enabled = true;
                txtVremeIstovaraD.Enabled = true;
                txtVremePrimopredajeD.Enabled = true;
                txtPostNaTerminalO.Enabled = false;
                txtPostNaTerminalO.Enabled = false;
                txtVremeUtovaraO.Enabled = false;
                txtVremeKontrolnogO.Enabled = false;
                txtVremeIzvlacenjaO.Enabled = false;
                dtpVremePolaskaO.Enabled = false;
                dtpVremeDolaskaO.Enabled = false;

                label36.Visible = false;
                dtpPLFormiranja.Visible = false;
                label37.Visible = false;
                dtpIzvlacenjeSaTerminala.Visible = false;
                label38.Visible = false;
                dtpPreuzimanjeSM.Visible = false;
                label39.Visible = false;
                dtpPolazakSid.Visible = false;
                label40.Visible = false;
                dtpPredajaHR.Visible = false;
                label41.Visible = false;
                dtpPrispeceRijeka.Visible = false;
                label42.Visible = false;
                dtpIskrcajRijeka.Visible = false;
                button7.Visible = false;
            }
                else
	            {
                dtpVremePolaska.Enabled = false;
                dtpVremeDolaska.Enabled = false;
                txtPostNaTerminalD.Enabled = false;
                txtKontrolniPregledD.Enabled = false;
                txtVremeIstovaraD.Enabled = false;
                txtVremePrimopredajeD.Enabled = false;
                txtPostNaTerminalO.Enabled = true;
                txtPostNaTerminalO.Enabled = true;
                txtVremeUtovaraO.Enabled = true;
                txtVremeKontrolnogO.Enabled = true;
                txtVremeIzvlacenjaO.Enabled = true;
                dtpVremePolaskaO.Enabled = true;
                dtpVremeDolaskaO.Enabled = true;

                label36.Visible = true;
                dtpPLFormiranja.Visible = true;
                label37.Visible = true;
                dtpIzvlacenjeSaTerminala.Visible = true;
                label38.Visible = true;
                dtpPreuzimanjeSM.Visible = true;
                label39.Visible = true;
                dtpPolazakSid.Visible = true;
                label40.Visible = true;
                dtpPredajaHR.Visible = true;
                label41.Visible = true;
                dtpPrispeceRijeka.Visible = true;
                label42.Visible = true;
                dtpIskrcajRijeka.Visible = true;
                button7.Visible = true;
            } 
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            txtSifra.Text = "";
            status = true;
            dtpVremePolaska.Value = DateTime.Today;
            dtpVremeDolaska.Value = DateTime.Today;
            dtpVremePolaskaO.Value = DateTime.Today;
            dtpVremeDolaskaO.Value = DateTime.Today;
            dtpVremePrimopredaje.Value = DateTime.Today;
            dtpVremeZavrsetkaKP.Value = DateTime.Today;
            dtpVremeZavrsetkaUtovara.Value = DateTime.Today;
            txtSifra.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dtpVremePolaska.Value = DateTime.Today;
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dtpVremeDolaska.Value = DateTime.Today;
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dtpVremePolaskaO.Value = DateTime.Today;
         
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dtpVremeDolaskaO.Value = DateTime.Today;
         
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (txtSifra.Text == "")
            {
                MessageBox.Show("Prvo oformite voz");
            
            }
            InsertVoz ins = new InsertVoz();
            ins.InsSerijeKola(Convert.ToInt32(txtSifra.Text), Convert.ToInt32(cboSerijaKola.SelectedValue),  Convert.ToInt32(nmBrojSerija.Value));
            RefreshDataGrid2();
            VratiUkupanBrojKontejnera();
        }
        private void RefreshDataGrid2()
        {
            var select = "  select VozSerijeKola.ID as Zapis, IDVoza, VozSerijeKola.TipKontejnera as IDT, Naziv, Broj20 as Nosivost20, BrojSerija from VozSerijeKola " + 
 " inner join SerijeKola on SerijeKola.Id = VozSerijeKola.TipKontejnera where IDVoza = " + Convert.ToInt32(txtSifra.Text);

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            dataGridView2.BorderStyle = BorderStyle.None;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView2.BackgroundColor = Color.White;

            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

         
            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "ID Voza";
            dataGridView2.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "TSV";
            dataGridView2.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView2.Columns[3];
            dataGridView2.Columns[3].HeaderText = "Naziv serije";
            dataGridView2.Columns[3].Width = 100;

            DataGridViewColumn column5 = dataGridView2.Columns[4];
            dataGridView2.Columns[4].HeaderText = "Nosivost 20 ST";
            dataGridView2.Columns[4].Width = 50;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            InsertVoz ins = new InsertVoz();
            ins.DelSerijeKola(Convert.ToInt32(txtSifraSerijeKola.Text));
            RefreshDataGrid2();
            VratiUkupanBrojKontejnera();
        }

        private void VratiUkupanBrojKontejnera()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select isnull(SUM(Broj20 * VozSerijeKola.BrojSerija),0) as BrojKontejnera from VozSerijeKola " +
            " inner join SerijeKola on SerijeKola.Id = VozSerijeKola.TipKontejnera where IDVoza = " + txtSifra.Text, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                nmrUkupanBrojKontejnera.Value = Convert.ToInt32(dr["BrojKontejnera"].ToString());
            }

            con.Close();

        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {

            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {
                        txtSifraSerijeKola.Text = row.Cells[0].Value.ToString();
                        cboSerijaKola.SelectedValue = Convert.ToInt32(row.Cells[2].Value.ToString());
                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {

        }

        private void VratiZadnjiBrojPlanaIzvoza()
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Max([ID]) as ID from IzvozKonacnaZaglavlje", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                BrojPlanaIzvoza = dr["ID"].ToString();
            }

            con.Close();
        }

        private void VratiZadnjiBrojPlanaUvoza()
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Max([ID]) as ID from UvozKonacnaZaglavlje", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
               
                BrojPlanaUvoza = dr["ID"].ToString();
            }

            con.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            int Postoji = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select ID from UvozKonacnaZaglavlje Where IDVoza="+Convert.ToInt32(txtSifra.Text), con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr.HasRows)
                {
                    MessageBox.Show("Već postoji kreiran plan!");
                    Postoji = 1;
                    Saobracaj.Uvoz.frmFormiranjePlana fplan = new Saobracaj.Uvoz.frmFormiranjePlana(Convert.ToInt32(dr[0].ToString()));
                    fplan.Show();
                }
              
            }
            if (Postoji == 0)
            {
                    Saobracaj.Uvoz.InsertUvozKonacnaZaglavlje ins = new Saobracaj.Uvoz.InsertUvozKonacnaZaglavlje();
                    ins.InsUvozKonacnaZaglavlje(Convert.ToInt32(txtSifra.Text), txtNapomena.Text, 1, "", Convert.ToDateTime("1.1.1900"), "", "", 0);

                    VratiZadnjiBrojPlanaUvoza();
                    MessageBox.Show("Uspesno ste formirirali novi Plan: " + BrojPlanaUvoza + " potrebno je da dodelite kontejnere planu, koristite opciju Popunjavanje Plana kontejnerima");
                    Saobracaj.Uvoz.frmFormiranjePlana fplan = new Saobracaj.Uvoz.frmFormiranjePlana(Convert.ToInt32(BrojPlanaUvoza));
                    fplan.Show();

            }

                
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            /*
            Saobracaj.Izvoz.frmIzvozKonacnaZaglavlje fukz = new Saobracaj.Izvoz.frmIzvozKonacnaZaglavlje(Convert.ToInt32(txtSifra.Text));
            fukz.Show();
            */
            int Postoji = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select ID from IzvozKonacnaZaglavlje Where IDVoza=" + Convert.ToInt32(txtSifra.Text), con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr.HasRows)
                {
                    MessageBox.Show("Već postoji kreiran plan!");
                    frmFormiranjePlanaIzvoz fpi = new frmFormiranjePlanaIzvoz(Convert.ToInt32(txtSifra.Text));
                    Postoji = 1;
                }
              
            }
            if (Postoji == 0)
            {
                int Terminal = 0;
                if (chkTerminal.Checked == true) { Terminal = 1; }

                InsertIzvozKonacnaZaglavlje ins = new InsertIzvozKonacnaZaglavlje();
                ins.InsIzvozKonacnaZaglavlje(Convert.ToInt32(txtSifra.Text), txtNapomena.Text, 1, "", Convert.ToDateTime("1.1.1900"), "", "", Terminal);
                VratiZadnjiBrojPlanaIzvoza();
                MessageBox.Show("Uspesno ste formirirali novi Plan: " + BrojPlanaIzvoza + " potrebno je da dodelite kontejnere planu, koristite opciju Popunjavanje Plana kontejnerima");
                frmFormiranjePlanaIzvoz fpi = new frmFormiranjePlanaIzvoz(Convert.ToInt32(BrojPlanaIzvoza));
                fpi.Show();
            }

        }

        private void tsPrvi_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            int idPlana = 0;
            string connectionString = Saobracaj.Sifarnici.frmLogovanje.connectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT ID FROM IzvozKonacnaZaglavlje WHERE IDVoza = @IDVoza";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IDVoza", Convert.ToInt32(txtSifra.Text));
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        idPlana = Convert.ToInt32(result);
                    }
                }
            }
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT BrojKontejnera FROM IzvozKonacna WHERE IDNadredjena = @IDNadredjena";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IDNadredjena", idPlana);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            foreach (DataRow row in dt.Rows)
            {
                string brKont = row["BrojKontejnera"].ToString().TrimEnd();
                int uvozID = 0;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT MAX(ID) FROM UvozKonacna WHERE BrojKontejnera = @BrojKontejnera";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@BrojKontejnera", brKont);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            uvozID = Convert.ToInt32(result);
                        }
                    }
                }

                if (uvozID > 0)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        string query = "UPDATE UvozKonacna SET EtaBrodara = @EtaBrodara WHERE ID = @ID";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@EtaBrodara", dtpPrispeceRijeka.Value);
                            cmd.Parameters.AddWithValue("@ID", uvozID);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
    }
}
