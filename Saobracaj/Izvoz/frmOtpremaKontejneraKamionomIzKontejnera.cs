using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Izvoz
{
    public partial class frmOtpremaKontejneraKamionomIzKontejnera : Syncfusion.Windows.Forms.Office2010Form
    {
        bool status = false;
        string KorisnikCene = Sifarnici.frmLogovanje.user;
        int KonkretnaUsluga = 0;
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        bool pomOtprema = false;
        string kontejner;
        public frmOtpremaKontejneraKamionomIzKontejnera()
        {
            InitializeComponent();
        }
        public frmOtpremaKontejneraKamionomIzKontejnera(int Osnov,string Registracija, string Vozac, string Kontejner, int NalogID)
        {
            InitializeComponent();
            txtRegBrKamiona.Text= Registracija;
            txtImeVozaca.Text = Vozac;
            txtKontejnerID.Text = Osnov.ToString();
            txtNalogID.Text = NalogID.ToString();
            kontejner = Kontejner;
           // PovuciIzPrijemnice();
        }


        public frmOtpremaKontejneraKamionomIzKontejnera(string KontejnerID)
        {
            InitializeComponent();
            txtKontejnerID.Text = KontejnerID;
        }
        int VratiKonkretanIDUsluge()
        {
            int Konkretan = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select KonkretaIDUsluge from RadniNalogInterni where ID = " + txtNalogID.Text, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Konkretan = Convert.ToInt32(dr["KonkretaIDUsluge"].ToString().TrimEnd());



            }
            con.Close();
            return Konkretan;

        }

        public frmOtpremaKontejneraKamionomIzKontejnera(string KontejnerID, string NalogID, string Korisnik, int Cirada, int OJ)
        {
            InitializeComponent();
            txtKontejnerID.Text = KontejnerID;
            txtNalogID.Text = NalogID;
            KorisnikCene = Korisnik;
            KonkretnaUsluga = VratiKonkretanIDUsluge();
            cboStatusOtpreme.SelectedIndex = 0;
            if (Cirada == 1)
            {
                chkCirada.Checked = true;
                chkPlatforma.Checked = false;
            }
            else
            {
                chkCirada.Checked = false;
                chkPlatforma.Checked = true;
            }
            if (OJ == 1)
            { 
            chkUvoz.Checked = true;
                chkIzvoz.Checked = false;
                chkTerminal.Checked = false;
            
            }

            if (OJ == 2)
            {
                chkUvoz.Checked = false;
                chkIzvoz.Checked = true;
                chkTerminal.Checked = false;

            }

            if (OJ == 4)
            {
                chkUvoz.Checked = false;
                chkIzvoz.Checked = false;
                chkTerminal.Checked = true;

            }
        }

        private void VratiOstalePodatkeIzUsluge(int ID, int Modul)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT [ID]      ,[RegBr]      ,[Datum]      ,[Vozac] " +
     " ,[BrojTelefona]      ,[Napomena]      ,[Modul]      ,[IDUsluge] " +
 " FROM [dbo].[VoziloUsluga]  " +
            "  where IDUsluge=" + ID + " and Modul = " + Modul, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtRegBrKamiona.Text = dr["RegBr"].ToString();
                txtImeVozaca.Text = dr["Vozac"].ToString();
                dtpDatumOtpreme.Value = Convert.ToDateTime(dr["Datum"].ToString());
                txtNapomena.Text = "BR TELEFONA:" + dr["BrojTelefona"].ToString() + " Napomena: " + dr["Napomena"].ToString();

            }
            con.Close();

        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtSifra.Enabled = false;

            dtpDatumOtpreme.Value = DateTime.Now;
            dtpDatumOtpreme.Enabled = true;
        }

        private void ProglasiFormiranomTerminal(int NalogID)
        {
            Saobracaj.Uvoz.InsertRadniNalogInterni iri = new Uvoz.InsertRadniNalogInterni();
            iri.UpdRadniNalogInterniFormiran(NalogID);


        }
        int VratiUsluguPoNalogu(string NalogID)
        {
            int Manipulacija = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT IDManipulacijaJed  from RadniNalogInterni " +
            "  where ID =" + NalogID, con);;
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
               Manipulacija =  Convert.ToInt32(dr["IDManipulacijaJed"].ToString());

            }
            con.Close();
            return Manipulacija;

        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            int Ciradatmp = 0;
            int ModulPorekla = 0;
            if (chkUvoz.Checked == true)
            {
                ModulPorekla = 1;
            }

            if (chkIzvoz.Checked == true)
            {
                ModulPorekla = 2;
            }

            if (chkTerminal.Checked == true)
            {
                ModulPorekla = 4;
            }
            if (chkCirada.Checked == true)
                Ciradatmp = 1;

            if (status == true)
            {

                /// ,  string RegBrKamiona,   string ImeVozaca,   int Vozom
                Dokumenta.InsertOtprema ins = new Dokumenta.InsertOtprema();
                ins.InsertOtp(Convert.ToDateTime(dtpDatumOtpreme.Text), Convert.ToInt32(cboStatusOtpreme.SelectedIndex), Convert.ToInt32(cboVozBuking.SelectedValue), txtRegBrKamiona.Text, txtImeVozaca.Text, Convert.ToDateTime(dtpVremeOdlaska.Value), 0, Convert.ToDateTime(DateTime.Now), KorisnikCene, txtNapomena.Text, 0, 0, Ciradatmp, ModulPorekla);
                status = false;
                VratiPodatkeMax();
                if (chkTerminal.Checked == true)
                { ProglasiFormiranomTerminal(Convert.ToInt32(txtNalogID.Text)); }
               
            }

            if (txtKontejnerID.Text != "" && txtNalogID.Text != "")
            {
                int Usluga = VratiUsluguPoNalogu(txtNalogID.Text);
                if (chkUvoz.Checked == true)
                {
                    
                    InsertIzvozKonacna ins = new InsertIzvozKonacna();
                    ins.PrenesiKontejnerUOtpremuKamionomUvoz(Convert.ToInt32(txtKontejnerID.Text), Convert.ToInt32(txtNalogID.Text));
                    // RefreshDataGrid();
                    MessageBox.Show("Uspešno ste formirali Otpremu kamionom");
                    DialogResult dialogResult = MessageBox.Show("Da li želite da formirate RN 6 za otpremu platforme", "Radni nalozi?", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        RadniNalozi.RN6OtpremaPlatforme rnop = new RadniNalozi.RN6OtpremaPlatforme(txtSifra.Text, KorisnikCene, txtNalogID.Text, txtRegBrKamiona.Text, 0, txtNalogID.Text);
                        rnop.Show();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        return;
                    }

                    //FORMIRATI RADNI NALOG
                }
                else if (chkIzvoz.Checked == true)
                {
                    InsertIzvozKonacna ins = new InsertIzvozKonacna();
                    //Prakticno napravi stavku
                    ins.PrenesiKontejnerUOtpremuKamionomIzvoz(Convert.ToInt32(txtSifra.Text), Convert.ToInt32(txtKontejnerID.Text), Convert.ToInt32(txtNalogID.Text));
                    // RefreshDataGrid();
                    MessageBox.Show("Uspešno ste formirali Otpremu kamionom");
                    DialogResult dialogResult = MessageBox.Show("Da li želite da formirate RN 6 OTPREMA PLATFORME", "Radni nalozi?", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                       // string OtpremaID, string Korisnik, string Usluga, string Kamion, int Uvoz
                        RadniNalozi.RN6OtpremaPlatforme ppl = new RadniNalozi.RN6OtpremaPlatforme(txtSifra.Text, KorisnikCene,  Usluga.ToString(), txtRegBrKamiona.Text, 1, txtNalogID.Text);
                        ppl.Show();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        return;
                    }
                }
                else if (chkTerminal.Checked == true)
                {
                    InsertIzvozKonacna ins = new InsertIzvozKonacna();
                    //Prakticno napravi stavku
                    ins.PrenesiKontejnerUOtpremuKamionomIzvoz(Convert.ToInt32(txtSifra.Text), Convert.ToInt32(txtKontejnerID.Text), Convert.ToInt32(txtNalogID.Text));
                    // RefreshDataGrid();
                    MessageBox.Show("Uspešno ste formirali Otpremu kamionom");

                }
              

            }
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

        private void button1_Click(object sender, EventArgs e)
        {



        }

        private void frmOtpremaKontejneraKamionomIzKontejnera_Load(object sender, EventArgs e)
        {
            if (chkUvoz.Checked == true)
            { VratiOstalePodatkeIzUsluge(KonkretnaUsluga, 0); }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (txtSifra.Text == "")
            {
                MessageBox.Show("Morate prvo formirati zapis");
            }
            else
            {
                string Company = Saobracaj.Sifarnici.frmLogovanje.Firma;
                switch (Company)
                {
                    case "Leget":
                        {
                            if (chkIzvoz.Checked == true)
                            {
                                Saobracaj.Dokumenta.frmOtpremaKontejneraIzvozKamion ter2 = new Saobracaj.Dokumenta.frmOtpremaKontejneraIzvozKamion( KorisnikCene,txtNalogID.Text, 0,2, txtSifra.Text);
                                ter2.Show();
                            }
                            else if (chkUvoz.Checked == true)
                            {
                                Saobracaj.Dokumenta.frmOtpremaKontejneraUvozKamion ter2 = new Saobracaj.Dokumenta.frmOtpremaKontejneraUvozKamion(Convert.ToInt32(txtSifra.Text), KorisnikCene);
                                ter2.Show();
                            }
                            else if (chkTerminal.Checked == true)
                            {
                                Saobracaj.Dokumenta.frmOtpremaKontejneraIzvozKamion ter2 = new Saobracaj.Dokumenta.frmOtpremaKontejneraIzvozKamion(KorisnikCene, txtNalogID.Text, 0, 4, txtSifra.Text);
                                ter2.Show();
                            }
                            return;
                        }
                    default:
                        {

                            Saobracaj.Dokumeta.frmOtpremaKontejnera ter3 = new Saobracaj.Dokumeta.frmOtpremaKontejnera(Convert.ToInt32(txtSifra.Text), KorisnikCene);
                            ter3.Show();
                            return;

                        }

                }
            }

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }
    }
}
