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
using System.Diagnostics.CodeAnalysis;

namespace Saobracaj.Izvoz
{
    public partial class frmOtpremaKontejneraKamionomIzKontejnera : Form
    {
        bool status = false;
        string KorisnikCene = "Panta";
        public string connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public frmOtpremaKontejneraKamionomIzKontejnera()
        {
            InitializeComponent();
        }

        public frmOtpremaKontejneraKamionomIzKontejnera(string KontejnerID)
        {
            InitializeComponent();
            txtKontejnerID.Text = KontejnerID;
        }

        public frmOtpremaKontejneraKamionomIzKontejnera(string KontejnerID, string NalogID, string Korisnik)
        {
            InitializeComponent();
            txtKontejnerID.Text = KontejnerID;
            txtNalogID.Text = NalogID;
            KorisnikCene = Korisnik;
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtSifra.Enabled = false;

            dtpDatumOtpreme.Value = DateTime.Now;
            dtpDatumOtpreme.Enabled = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (status == true)
            {
                /// ,  string RegBrKamiona,   string ImeVozaca,   int Vozom
                Dokumenta.InsertOtprema ins = new Dokumenta.InsertOtprema();
                ins.InsertOtp(Convert.ToDateTime(dtpDatumOtpreme.Text), Convert.ToInt32(cboStatusOtpreme.SelectedIndex), Convert.ToInt32(cboVozBuking.SelectedValue), txtRegBrKamiona.Text, txtImeVozaca.Text, Convert.ToDateTime(dtpVremeOdlaska.Value), 0, Convert.ToDateTime(DateTime.Now), KorisnikCene, txtNapomena.Text, 0, 0, 0, 0);
                status = false;
                VratiPodatkeMax();
            }
        }
        private void VratiPodatkeMax()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
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
            if (txtKontejnerID.Text != "" && txtNalogID.Text != "")
            {
                InsertIzvozKonacna ins = new InsertIzvozKonacna();
                ins.PrenesiKontejnerUOtpremuKamionomUvoz(Convert.ToInt32(txtKontejnerID.Text), Convert.ToInt32(txtNalogID.Text));
                // RefreshDataGrid();
                MessageBox.Show("Uspešno ste formirali Otpremu kamionom");

            }


        }
    }
}
