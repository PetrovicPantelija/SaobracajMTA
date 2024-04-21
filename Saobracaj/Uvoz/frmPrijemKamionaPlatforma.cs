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

namespace Saobracaj.Uvoz
{
    public partial class frmPrijemKamionaPlatforma :  Syncfusion.Windows.Forms.Office2010Form
    {
        bool status = false;
        string KorisnikCene = "Panta";
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        public frmPrijemKamionaPlatforma()
        {
            InitializeComponent();
        }

        public frmPrijemKamionaPlatforma(string Kontejner)
        {
            InitializeComponent();
            txtKontejnerID.Text = Kontejner;
        }

        private void frmPrijemKamionaPlatforma_Load(object sender, EventArgs e)
        {

        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtSifra.Enabled = false;

            dtpDatumPrijema.Value = DateTime.Now;
            dtpDatumPrijema.Enabled = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (status == true)
            {
                /// ,  string RegBrKamiona,   string ImeVozaca,   int Vozom
                Dokumeta.InsertPrijemKontejneraVoz ins = new Dokumeta.InsertPrijemKontejneraVoz();
                ins.InsertPrijemKontVoz(Convert.ToDateTime(dtpDatumPrijema.Text),Convert.ToInt32(cboStatusPrijema.SelectedIndex), Convert.ToInt32(cboBukingPrijema.SelectedValue), Convert.ToDateTime(dtpVremeDolaska.Value), Convert.ToDateTime(DateTime.Now), KorisnikCene, txtRegBrKamiona.Text, txtImeVozaca.Text, 0, txtNapomena.Text, Convert.ToInt32(cboPredefinisanePoruke.SelectedValue), Convert.ToInt32(cboOperater.SelectedValue), 0, 0,0,0);
                status = false;
                VratiPodatkeMax();
            }
        }

        private void VratiPodatkeMax()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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

        private void button1_Click(object sender, EventArgs e)
        {
            Izvoz.InsertIzvoz ins = new Izvoz.InsertIzvoz();
            ins.PrenesiKontejnerIzNerasporedjenihKamion(Convert.ToInt32(txtKontejnerID.Text));
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
