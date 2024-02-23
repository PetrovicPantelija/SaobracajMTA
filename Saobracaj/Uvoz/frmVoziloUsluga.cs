using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace Saobracaj.Uvoz
{
    public partial class frmVoziloUsluga : Form
    {
        bool status = false;
        public string connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public frmVoziloUsluga()
        {
            InitializeComponent();
        }

        public frmVoziloUsluga(string ID)
        {
            InitializeComponent();

            txtID.Text = ID.ToString();
            VratiOstalePodatke();
        }

        private void VratiOstalePodatke()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT  ID, VoziloOznaka, VoziloDatum, VoziloVozac, BrojTelefona, Napomena from UvozKonacnaZaglavlje " +
            "  where ID=" + txtID.Text, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtID.Text = dr["ID"].ToString();
                txtVozilo.Text = dr["VoziloOznaka"].ToString();
                dtpDatum.Value = Convert.ToDateTime(dr["VoziloDatum"].ToString());
                txtNapomena.Text = dr["VoziloOznaka"].ToString();
                txtVozac.Text = dr["VoziloOznaka"].ToString();
                txtBrojTelefona.Text = dr["VoziloOznaka"].ToString();

            }
            con.Close();
           
        }



        private void frmVoziloUsluga_Load(object sender, EventArgs e)
        {

        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            tsNew.Enabled = false;
            status = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            InsertUvozKonacnaZaglavlje ins = new InsertUvozKonacnaZaglavlje();
            if (status == true)
            {
                ins.InsUvozKonacnaZaglavlje(0, txtNapomena.Text, 0, txtVozilo.Text, Convert.ToDateTime(dtpDatum.Value), txtVozac.Text, txtBrojTelefona.Text, 0);
            }
            else
            {
                ins.UpdUvozKonacnaZaglavlje(Convert.ToInt32(txtID.Text.ToString()), 0, txtNapomena.Text, 0, txtVozilo.Text, Convert.ToDateTime(dtpDatum.Value), txtVozac.Text, txtBrojTelefona.Text);
            }
            //  FillGV();
            tsNew.Enabled = true;
            status = false;
        }
    }
}
