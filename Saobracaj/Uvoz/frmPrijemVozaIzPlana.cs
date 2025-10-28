using Saobracaj.Dokumenta;
using Saobracaj.Izvoz;
using Saobracaj.RadniNalozi;
using Saobracaj.Sifarnici;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.Windows.Forms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace Saobracaj.Uvoz
{
    public partial class frmPrijemVozaIzPlana :Form
    {
        bool status = false;
        string KorisnikCene = Saobracaj.Sifarnici.frmLogovanje.user.ToString();
        int IzRNI = 0;
        int trnI = 0;
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        int Kamion = 0;
        int OJD = 0;

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
            // this.FormBorderStyle = FormBorderStyle.FixedSingle;

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
                meniHeader.Visible = true;
                panelHeader.Visible = false;
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;

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

        public frmPrijemVozaIzPlana()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            ChangeTextBox();
        }

        public frmPrijemVozaIzPlana(int RNI, int Vozom, int OJ)
        {

            //Vozom za voz - 0; Vozaom = 1 za Kamion 
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            ChangeTextBox();

            IzRNI = 1;
            trnI = RNI;
            txtNalogID.Text = RNI.ToString();
            OJD = OJ;
            if (OJ == 2)
            { 
            chkIzvoz.Checked = true;
                chkUvoz.Checked = false;
                chkTErminal.Checked = false;
            
            }

            if (OJ == 1)
            {
                chkIzvoz.Checked = false;
                chkUvoz.Checked = true;
                chkTErminal.Checked = false;

            }
            if (OJ == 4)
            {
                chkIzvoz.Checked = false;
                chkUvoz.Checked = false;
                chkTErminal.Checked = true;

            }


            if (Vozom == 0 ) 
            { Kamion = 1; // Ynaci Prijem Kamiona platforme
                                          }
            else
            {
                Kamion = 0;
            }
            if (Kamion == 0 )
            {
                label30.Visible = true;
                txtRegBrKamiona.Visible = true;
                label29.Visible = true;
                txtImeVozaca.Visible = true;
                chkVoz.Checked = false;
                label15.Visible = false;
                cboBukingPrijema.Visible = false;
                label35.Visible = false;
                cboOperater.Visible = false;
                label3.Visible = false;
                cboOperaterHR.Visible = false;
                txtNapomenaVozac.Visible = true;
                txtTelefon.Visible = true;
                label6.Visible = true;
                label5.Visible = true;
                VratiKamion(RNI); // Vraca podatke o kamionu i vozacu
            }
            else {
                label30.Visible = false;
                txtRegBrKamiona.Visible = false;
                label29.Visible = false;
                txtImeVozaca.Visible = false;
                chkVoz.Checked = true;
                label15.Visible = true;
                cboBukingPrijema.Visible = true;
                label35.Visible = true;
                cboOperater.Visible = true;
                label3.Visible = true;
                cboOperaterHR.Visible = true;
                txtTelefon.Visible = false;
                txtNapomenaVozac.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
            }

        }

        private void VratiKamion(int RNI)
        {
            int IzUvoza = 0;
            if (chkUvoz.Checked == true)
            {
                IzUvoza = 0;
            }
            else 
            { 
                IzUvoza = 1; 
            }

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" Select RegBr, Vozac, BrojTelefona, VoziloUsluga.Napomena from RadniNalogInterni inner join VoziloUsluga On VoziloUsluga.IdUsluge = RadniNalogInterni.KonkretaIDUsluge where RadniNalogInterni.ID =  " + RNI + " and Modul = " + IzUvoza, con); // UVoz
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtRegBrKamiona.Text = dr["RegBr"].ToString();
                txtImeVozaca.Text = dr["Vozac"].ToString();
                txtTelefon.Text = dr["BrojTelefona"].ToString();
                txtNapomena.Text = dr["Napomena"].ToString();
            }

            con.Close();

        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtSifra.Enabled = false;
            dtpDatumPrijema.Value = DateTime.Now;
            dtpDatumPrijema.Enabled = true;
        }
        int ProveriDaLIPostojiVoz(int Voz)
        {
            int Postoji = 0;
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select top 1 ID from PrijemKontejneraVoz where IDVoza = " + Voz + " order by PrijemKontejneraVoz.ID Desc", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Postoji = Convert.ToInt32(dr["ID"].ToString());
            }
            con.Close();
            return Postoji;

        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            //Kamion = 1 znaci da je voz
            if (Kamion == 1)
            {
                //Panta 1
                int i = ProveriDaLIPostojiVoz(Convert.ToInt32(cboBukingPrijema.SelectedValue));
                if (i > 0)
                {
                    DialogResult dialogResult = MessageBox.Show("Već postoji GATE IN za taj voz, da li želite da nastavite?", "GATE IN VOZ", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        /// ,  string RegBrKamiona,   string ImeVozaca,   int Vozom
                        VratiIDPrijemnice(Convert.ToInt32(cboBukingPrijema.SelectedValue));
                        Dokumeta.InsertPrijemKontejneraVoz upd = new Dokumeta.InsertPrijemKontejneraVoz();
                        upd.UpdPrijemKontejneraVoz(Convert.ToInt32(txtSifra.Text), Convert.ToDateTime(dtpDatumPrijema.Text), Convert.ToInt32(cboStatusPrijema.SelectedIndex), Convert.ToInt32(cboBukingPrijema.SelectedValue), Convert.ToDateTime(dtpVremeDolaska.Value), Convert.ToDateTime(DateTime.Now), KorisnikCene, "", "", 1, txtNapomena.Text, Convert.ToInt32(cboPredefinisanePoruke.SelectedValue), Convert.ToInt32(cboOperater.SelectedValue), 0, 0, Convert.ToInt32(cboOperaterHR.SelectedValue), OJD);
                        status = false;


                       
                     
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        /*
                         *  Dokumeta.InsertPrijemKontejneraVoz ins = new Dokumeta.InsertPrijemKontejneraVoz();
                             ins.InsertPrijemKontVoz(Convert.ToDateTime(dtpDatumPrijema.Text), Convert.ToInt32(cboStatusPrijema.SelectedIndex), Convert.ToInt32(cboBukingPrijema.SelectedValue), Convert.ToDateTime(dtpVremeDolaska.Value), Convert.ToDateTime(DateTime.Now), KorisnikCene, "", "", 1, txtNapomena.Text, Convert.ToInt32(cboPredefinisanePoruke.SelectedValue), Convert.ToInt32(cboOperater.SelectedValue), 0, 0, Convert.ToInt32(cboOperaterHR.SelectedValue), OJD);
                             status = false;
                             VratiPodatkeMax();
                         */

                    }

                    ///

                }

                else
                {
                    Dokumeta.InsertPrijemKontejneraVoz ins = new Dokumeta.InsertPrijemKontejneraVoz();
                    ins.InsertPrijemKontVoz(Convert.ToDateTime(dtpDatumPrijema.Text), Convert.ToInt32(cboStatusPrijema.SelectedIndex), Convert.ToInt32(cboBukingPrijema.SelectedValue), Convert.ToDateTime(dtpVremeDolaska.Value), Convert.ToDateTime(DateTime.Now), KorisnikCene, "", "", 1, txtNapomena.Text, Convert.ToInt32(cboPredefinisanePoruke.SelectedValue), Convert.ToInt32(cboOperater.SelectedValue), 0, 0, Convert.ToInt32(cboOperaterHR.SelectedValue), OJD);
                    status = false;
                    VratiPodatkeMax();

                }
               


            }
         
            else if (Kamion == 0)
            {
                if (status == true)
                {
                    //Ovde unosim Kamion Zaglavlje
                    
                    Dokumeta.InsertPrijemKontejneraVoz ins = new Dokumeta.InsertPrijemKontejneraVoz();
                    ins.InsertPrijemKontVoz(Convert.ToDateTime(dtpDatumPrijema.Text), Convert.ToInt32(cboStatusPrijema.SelectedIndex), Convert.ToInt32(cboBukingPrijema.SelectedValue), Convert.ToDateTime(dtpVremeDolaska.Value), Convert.ToDateTime(DateTime.Now), KorisnikCene, txtRegBrKamiona.Text, txtImeVozaca.Text, 0, txtNapomena.Text, Convert.ToInt32(cboPredefinisanePoruke.SelectedValue), Convert.ToInt32(cboOperater.SelectedValue), 0, 0, Convert.ToInt32(cboOperaterHR.SelectedValue), OJD);
                    status = false;
                    VratiPodatkeMax();
                }
            }

            PrenesiStavkeIRN();

        }
        private void VratiIDPrijemnice(int Voz)
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Max([ID]) as ID from PrijemKontejneraVoz where IDVoza = " + Voz, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtSifra.Text = dr["ID"].ToString();
            }

            con.Close();
        }
        private void VratiPodatkeMax()
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Max([ID]) as ID from PrijemKontejneraVoz", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtSifra.Text = dr["ID"].ToString();
            }

            con.Close();
        }


        private void ProveriDaLijeFormiranVecRN4()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Count(*) as broj from RNPrijemPlatforme where NalogID = " + txtNalogID.Text, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtPostoji.Text = dr["broj"].ToString();
            }

            con.Close();
        }

        private void VratiVozIzPlana()
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("Select Top 1 IDVoza, OperaterSrbija, OperaterHR, Voz.Napomena from UvozKonacnaZaglavlje " +
" inner join Voz on UvozKonacnaZaglavlje.IDVoza = Voz.ID " +
" where UvozKonacnaZaglavlje.ID = " + Convert.ToInt32(cboPlanUtovara.SelectedValue));
            cmd.Connection = con;
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                cboBukingPrijema.SelectedValue = Convert.ToInt32(dr["IDVoza"].ToString());
                cboOperater.SelectedValue = Convert.ToInt32(dr["OperaterSrbija"].ToString());
                cboOperaterHR.SelectedValue = Convert.ToInt32(dr["OperaterHR"].ToString());
                txtNapomena.Text = dr["Napomena"].ToString();
            }

            con.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            VratiVozIzPlana();

        }

        int VratiPlan()
        {
            int PlanID = 0;
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select PlanID from RadniNalogInterni where ID = " + trnI, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                PlanID = Convert.ToInt32(dr["PlanID"].ToString());
            }
            con.Close();
            return PlanID;


        }

        private void frmPrijemVozaIzPlana_Load(object sender, EventArgs e)
        {
            var planutovara = "select UvozKonacnaZaglavlje.ID,(Cast(UvozKonacnaZaglavlje.ID as nvarchar(5)) + '-'  + NazivVoza  +  '-' + Cast(BrVoza as nvarchar(15)) + ' '  + Relacija ) as Naziv from UvozKonacnaZaglavlje " +
          " inner join Voz on Voz.Id = UvozKonacnaZaglavlje.IdVoza order by UvozKonacnaZaglavlje.ID desc";
            var planutovaraSAD = new SqlDataAdapter(planutovara, connection);
            var planutovaraSDS = new DataSet();
            planutovaraSAD.Fill(planutovaraSDS);
            cboPlanUtovara.DataSource = planutovaraSDS.Tables[0];
            cboPlanUtovara.DisplayMember = "Naziv";
            cboPlanUtovara.ValueMember = "ID";

            var select8 = "  Select Distinct ID, (Cast(BrVoza as nvarchar(10)) + '-' + Relacija) as IdVoza   From Voz ";
            var s_connection8 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection8 = new SqlConnection(s_connection8);
            var c8 = new SqlConnection(s_connection8);
            var dataAdapter8 = new SqlDataAdapter(select8, c8);

            var commandBuilder8 = new SqlCommandBuilder(dataAdapter8);
            var ds8 = new DataSet();
            dataAdapter8.Fill(ds8);
            cboBukingPrijema.DataSource = ds8.Tables[0];
            cboBukingPrijema.DisplayMember = "IdVoza";
            cboBukingPrijema.ValueMember = "ID";


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


            var select5 = "  select PaSifra, PaNaziv from Partnerji order by PaNaziv";
            var s_connection5 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection5 = new SqlConnection(s_connection4);
            var c5 = new SqlConnection(s_connection5);
            var dataAdapter5 = new SqlDataAdapter(select5, c5);

            var commandBuilder5 = new SqlCommandBuilder(dataAdapter5);
            var ds5 = new DataSet();
            dataAdapter5.Fill(ds5);
            cboOperaterHR.DataSource = ds5.Tables[0];
            cboOperaterHR.DisplayMember = "PaNaziv";
            cboOperaterHR.ValueMember = "PaSifra";

            if (IzRNI == 1)
            {
                int voz = VratiPlan();
                cboPlanUtovara.SelectedValue = voz;
                VratiVozIzPlana();
            }

        }

        private void ProglasiObradjenimRNIVOZ(int PlanID)
        {
            InsertRadniNalogInterni irni = new InsertRadniNalogInterni();
            irni.PromeniFormiranRNIVoz(PlanID);
        }
        int VratiUsluguPoNalogu(string NalogID)
        {
            int Manipulacija = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT IDManipulacijaJed  from RadniNalogInterni " +
            "  where ID =" + NalogID, con); ;
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Manipulacija = Convert.ToInt32(dr["IDManipulacijaJed"].ToString());

            }
            con.Close();
            return Manipulacija;

        }

        private void PrenesiStavkeIRN()
        {
            int Usluga = VratiUsluguPoNalogu(txtNalogID.Text);
            if (txtSifra.Text == "")
            {
                return;
            }
            else
            {
                if (Kamion == 1)
                {
                    ///
                    //Ako jer kamion 1 onda prenosim voz
                    InsertUvozKonacna ins = new InsertUvozKonacna();
                    
                   
                        //Stavke voza
                        //OVaj poziv vazi samo za Voz
                        ins.PrenesiPlanUtovaraUPrijemVoz(Convert.ToInt32(txtSifra.Text), Convert.ToInt32(cboPlanUtovara.SelectedValue));
                   

                   // VratiPodatkeMax();
                    RefreshDataGrid();
                    ProglasiObradjenimRNIVOZ(Convert.ToInt32(cboPlanUtovara.SelectedValue));
                    MessageBox.Show("Uspešno ste formirali/dopunili prijemnicu za izabrani plan");
                    DialogResult dialogResult = MessageBox.Show("Da li želite da formirate RN za Vizuelni pregled i Kalmaristu", "Radni nalozi?", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        RadniNalozi.RN1PrijemVoza rnpv = new RadniNalozi.RN1PrijemVoza(KorisnikCene, cboBukingPrijema.SelectedValue.ToString(), Usluga.ToString(), txtSifra.Text);
                        rnpv.Show();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        return;
                    }
                }
                else if (Kamion == 0)
                {
                    InsertUvozKonacna ins = new InsertUvozKonacna();
                    if (chkIzvoz.Checked == true)
                    {
                        ins.PrenesiPlanUtovaraUPrijemVozIzvoz(Convert.ToInt32(txtSifra.Text), Convert.ToInt32(txtNalogID.Text));
                    }
                    else if (chkTErminal.Checked == true)
                    {
                        //
                        ins.PrenesiPlanUtovaraUPrijemPLatforma(Convert.ToInt32(txtSifra.Text), Convert.ToInt32(txtNalogID.Text));
                    }
                    else
                    {
                        ins.PrenesiPlanUtovaraUPrijemPLatforma(Convert.ToInt32(txtSifra.Text), Convert.ToInt32(txtNalogID.Text));
                    }
               
                    VratiPodatkeMax();
                    RefreshDataGrid();
                    int Modul = 0;
                    if (chkIzvoz.Checked == true)
                    {
                        Modul = 1; // Izvoz
                        DialogResult dialogResult = MessageBox.Show("Da li želite da formirate RN 4 PRIJEM PLATFORME", "Radni nalozi?", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            RadniNalozi.RN4PrijemPlatforme ppl = new RadniNalozi.RN4PrijemPlatforme(txtSifra.Text, txtRegBrKamiona.Text, KorisnikCene, Usluga.ToString(), Modul, txtNalogID.Text);
                            ppl.Show();
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            return;
                        }

                    }
                    else if (chkUvoz.Checked == true)
                    {
                        ProveriDaLijeFormiranVecRN4();
                        if (txtPostoji.Text == "1")
                        {
                            MessageBox.Show("Vec je napravljen RN");
                            return;
                        }
                        Modul = 0; // Da li je ovo Uvoz
                        DialogResult dialogResult = MessageBox.Show("Da li želite da formirate RN 4 PRIJEM PLATFORME", "Radni nalozi?", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {

                            RadniNalozi.RN4PrijemPlatforme ppl = new RadniNalozi.RN4PrijemPlatforme(txtSifra.Text, txtRegBrKamiona.Text, KorisnikCene, Usluga.ToString(), Modul, txtNalogID.Text);
                            ppl.Show();
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            return;
                        }

                    }
                    else if (chkTErminal.Checked == true)
                    {
                        Modul = 4;
                        DialogResult dialogResult = MessageBox.Show("Da li želite da formirate RN 5 PRIJEM PLATFORME BRODAR", "Radni nalozi?", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            RadniNalozi.RN5PrijemPlatforme2 ppl = new RadniNalozi.RN5PrijemPlatforme2(txtSifra.Text, txtRegBrKamiona.Text, KorisnikCene, Usluga.ToString(), Modul, Convert.ToInt32(txtNalogID.Text));
                            ppl.Show();
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            return;
                        }
                    }
                    else
                    {
                        Modul = 0;
                    }
                    // ProglasiObradjenimRNIVOZ(Convert.ToInt32(cboPlanUtovara.SelectedValue));
                 

                }

            }






        }
        private void button1_Click(object sender, EventArgs e)
        {
           
          

        }

        private void RefreshDataGrid()
        {
            var select =
" SELECT PrijemKontejneraVozStavke.ID, PrijemKontejneraVozStavke.RB, PrijemKontejneraVozStavke.IDNadredjenog,   PrijemKontejneraVozStavke.KontejnerID, " +
" PrijemKontejneraVozStavke.BrojKontejnera,  TipKontenjera.Naziv AS TipKontejnera,PrijemKontejneraVozStavke.BrojVagona,  PrijemKontejneraVozStavke.Granica as GranicaTovarenja, " +
" PrijemKontejneraVozStavke.BrojOsovina,  PrijemKontejneraVozStavke.SopstvenaMasa as TaraVagona,  PrijemKontejneraVozStavke.Tara,  PrijemKontejneraVozStavke.Neto, " +
" PrijemKontejneraVozStavke.BTTORobe, PrijemKontejneraVozStavke.BTTOKontejnera, " +
" Partnerji_4.PANaziv as Brodar, UvozKonacna.BukingBrodara AS BukingBrodar,PrijemKontejneraVozStavke.BrojPlombe, " +
" PrijemKontejneraVozStavke.BrojPlombe2, PrijemKontejneraVozStavke.PeriodSkladistenjaOd, " +
" PrijemKontejneraVozStavke.PeriodSkladistenjaDo, DirigacijaKOntejneraZa.Naziv as DirigacijaKOntejneraZa, VrstePostupakaUvoz.Naziv as PostupakSaRobom," +
" Partnerji_2.PaNaziv AS Nalogodavac_Za_DrumskiPrevoz, Partnerji_3.PaNaziv AS Nalogodavac_Za_Usluge," +
 "  PrijemKontejneraVozStavke.PlaniraniLager as DIREKTNI_INDIREKTNI,    PrijemKontejneraVozStavke.NapomenaS, PrijemKontejneraVozStavke.Napomena2, " +
" PrijemKontejneraVozStavke.Datum, PrijemKontejneraVozStavke.Korisnik " +
" FROM  PrijemKontejneraVozStavke " +
" inner join UvozKonacna on UvozKonacna.ID = PrijemKontejneraVozStavke.KontejnerID " +
" INNER JOIN  Partnerji AS Partnerji_1 ON UvozKonacna.Nalogodavac1 = Partnerji_1.PaSifra " +
" INNER JOIN  Partnerji AS Partnerji_2 ON UvozKonacna.Nalogodavac2 = Partnerji_2.PaSifra " +
" INNER JOIN  Partnerji AS Partnerji_3 ON UvozKonacna.Nalogodavac3 = Partnerji_3.PaSifra " +
" INNER JOIN  Partnerji AS Partnerji_4 ON UvozKonacna.Brodar = Partnerji_4.PaSifra " +
" INNER JOIN TipKontenjera ON PrijemKontejneraVozStavke.TipKontejnera = TipKontenjera.ID " +
" INNER join DirigacijaKontejneraZa on DirigacijaKontejneraZa.ID = PrijemKontejneraVozStavke.StatusKontejnera " +
" INNER JOIN  Voz ON PrijemKontejneraVozStavke.IdVoza = Voz.ID " +
" INNER JOIN VrstePostupakaUvoz ON VrstePostupakaUvoz.id = PrijemKontejneraVozStavke.PostupakSaRobom where IdNadredjenog =  " + txtSifra.Text + " order by RB";


            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = false;
            dataGridView1.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView1);

            //Panta refresh

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[0].Visible = false;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "RB";
            dataGridView1.Columns[1].Width = 30;
            /*
            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Br Dok";
            dataGridView1.Columns[2].Width = 30;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Broj kontejnera";
            dataGridView1.Columns[3].Width = 110;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Broj Vagona";
            dataGridView1.Columns[4].Width = 110;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Granica";
            dataGridView1.Columns[5].Width = 50;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Br os";
            dataGridView1.Columns[6].Width = 50;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Sops masa";
            dataGridView1.Columns[7].Width = 50;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Tara";
            dataGridView1.Columns[8].Width = 70;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Neto";
            dataGridView1.Columns[9].Width = 70;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Posiljalac";
            dataGridView1.Columns[10].Width = 50;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Primalac";
            dataGridView1.Columns[11].Width = 50;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Vlasnik";
            dataGridView1.Columns[12].Width = 40;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Tip kontejnera";
            dataGridView1.Columns[13].Width = 70;

            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "NHM";
            dataGridView1.Columns[14].Width = 40;

            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Buking broldar";
            dataGridView1.Columns[15].Width = 70;

            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Status Kontejnera";
            dataGridView1.Columns[16].Width = 20;

            DataGridViewColumn column18 = dataGridView1.Columns[17];
            dataGridView1.Columns[17].HeaderText = "Br plombe";
            dataGridView1.Columns[17].Width = 90;

            DataGridViewColumn column19 = dataGridView1.Columns[18];
            dataGridView1.Columns[18].HeaderText = "Br plombe2";
            dataGridView1.Columns[18].Width = 9;

            DataGridViewColumn column20 = dataGridView1.Columns[19];
            dataGridView1.Columns[19].HeaderText = "Plan lager";
            dataGridView1.Columns[19].Width = 30;

            DataGridViewColumn column21 = dataGridView1.Columns[20];
            dataGridView1.Columns[20].HeaderText = "Vreme dolaska";
            dataGridView1.Columns[20].Width = 70;

            DataGridViewColumn column22 = dataGridView1.Columns[21];
            dataGridView1.Columns[21].HeaderText = "Vreme prip";
            dataGridView1.Columns[21].Visible = false;
            dataGridView1.Columns[21].Width = 70;

            DataGridViewColumn column23 = dataGridView1.Columns[22];
            dataGridView1.Columns[22].HeaderText = "Vreme odlaska";
            dataGridView1.Columns[22].Visible = false;
            dataGridView1.Columns[22].Width = 70;

            DataGridViewColumn column24 = dataGridView1.Columns[23];
            dataGridView1.Columns[23].HeaderText = "Datum";
            dataGridView1.Columns[23].Width = 70;

            DataGridViewColumn column25 = dataGridView1.Columns[24];
            dataGridView1.Columns[24].HeaderText = "Korisnik";
            dataGridView1.Columns[24].Width = 70;

            DataGridViewColumn column26 = dataGridView1.Columns[25];
            dataGridView1.Columns[25].HeaderText = "Napomena stav";
            dataGridView1.Columns[25].Width = 70;
            */

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string Company = Saobracaj.Sifarnici.frmLogovanje.Firma;
            switch (Company)
            {
                case "Leget":
                    {
                        Saobracaj.Dokumenta.frmPrijemKontejneraLegetVozUvoz ter2 = new Saobracaj.Dokumenta.frmPrijemKontejneraLegetVozUvoz(Convert.ToInt32(txtSifra.Text), KorisnikCene, 1);
                        ter2.Show();
                        return;

                    }
                default:
                    {


                        return;

                    }
                    break;
            }
        }

        private void cboOperater_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            frmPrijemKontejneraKamionLegetIzvoz li = new frmPrijemKontejneraKamionLegetIzvoz(txtSifra.Text,0, OJD);
            li.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            frmPrijemKontejneraKamionLegetUvoz lu = new frmPrijemKontejneraKamionLegetUvoz(txtSifra.Text, 0);
            lu.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            frmPrijemKontejneraKamionLegetIzvoz li = new frmPrijemKontejneraKamionLegetIzvoz(txtSifra.Text, 0, OJD);
            li.Show();
        }
        string VratiKontejner(string NalogID)
        {/*
            select
    CASE
WHEN RadniNalogInterni.OJIzdavanja = 1 THEN(Select BrojKontejnera from UvozKOnacna inner join RadniNalogInterni on UvozKOnacna.ID = RadniNalogInterni.BrojOsnov where RadniNalogInterni.ID = 222)
WHEN RadniNalogInterni.OJIzdavanja = 2 THEN(Select BrojKontejnera from IzvozKonacna inner join RadniNalogInterni on IzvozKonacna.ID = RadniNalogInterni.BrojOsnov where RadniNalogInterni.ID = 222)
else 'NEMA'
END AS BrojKontejnera
from RadniNalogInterni where RadniNalogInterni.ID = 222
            */

            string Konkretan = "";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select   CASE WHEN RadniNalogInterni.OJIzdavanja = 1 THEN(Select BrojKontejnera from UvozKOnacna inner join RadniNalogInterni on UvozKOnacna.ID = RadniNalogInterni.BrojOsnov where RadniNalogInterni.ID = " + txtNalogID.Text + ") WHEN RadniNalogInterni.OJIzdavanja = 2 THEN(Select BrojKontejnera from IzvozKonacna inner join RadniNalogInterni on IzvozKonacna.ID = RadniNalogInterni.BrojOsnov where RadniNalogInterni.ID = " + txtNalogID.Text + ") else 'NEMA' END AS BrojKontejnera from RadniNalogInterni where RadniNalogInterni.ID =  " + txtNalogID.Text, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Konkretan = dr["BrojKontejnera"].ToString().TrimEnd();



            }
            con.Close();
            return Konkretan;

        }


        private void txtNalogID_TextChanged(object sender, EventArgs e)
        {
            
            txtBrojKontejnera.Text = VratiKontejner(txtNalogID.Text);
        }
    }
}
