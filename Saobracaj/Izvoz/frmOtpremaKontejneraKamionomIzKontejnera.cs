using System;
using System.Configuration;
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
        public frmOtpremaKontejneraKamionomIzKontejnera()
        {
            InitializeComponent();
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

        public frmOtpremaKontejneraKamionomIzKontejnera(string KontejnerID, string NalogID, string Korisnik, int Cirada)
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
            }
            else
            {
                chkCirada.Checked = false;
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

        private void tsSave_Click(object sender, EventArgs e)
        {
            int Ciradatmp = 0;
            if (chkCirada.Checked == true)
                Ciradatmp = 1;

            if (status == true)
            {
                /// ,  string RegBrKamiona,   string ImeVozaca,   int Vozom
                Dokumenta.InsertOtprema ins = new Dokumenta.InsertOtprema();
                ins.InsertOtp(Convert.ToDateTime(dtpDatumOtpreme.Text), Convert.ToInt32(cboStatusOtpreme.SelectedIndex), Convert.ToInt32(cboVozBuking.SelectedValue), txtRegBrKamiona.Text, txtImeVozaca.Text, Convert.ToDateTime(dtpVremeOdlaska.Value), 0, Convert.ToDateTime(DateTime.Now), KorisnikCene, txtNapomena.Text, 0, 0, Ciradatmp, 0);
                status = false;
                VratiPodatkeMax();
                ProglasiFormiranomTerminal(Convert.ToInt32(txtNalogID.Text));
            }

            if (txtKontejnerID.Text != "" && txtNalogID.Text != "")
            {
                InsertIzvozKonacna ins = new InsertIzvozKonacna();
                ins.PrenesiKontejnerUOtpremuKamionomUvoz(Convert.ToInt32(txtKontejnerID.Text), Convert.ToInt32(txtNalogID.Text));
                // RefreshDataGrid();
                MessageBox.Show("Uspešno ste formirali Otpremu kamionom");

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
                            Saobracaj.Dokumenta.frmOtpremaKontejneraUvozKamion ter2 = new Saobracaj.Dokumenta.frmOtpremaKontejneraUvozKamion(Convert.ToInt32(txtSifra.Text), KorisnikCene);
                            ter2.Show();
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
    }
}
