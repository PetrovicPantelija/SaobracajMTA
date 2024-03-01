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
    public partial class frmVoziloUsluga : Syncfusion.Windows.Forms.Office2010Form
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

        public frmVoziloUsluga(int ID, int Modul)
        {
            InitializeComponent();

           // txtID.Text = ID.ToString();
            if (Modul == 0)
            {
                chkUvoz.Checked = true;
                txtUsluge.Text = ID.ToString();
                VratiOstalePodatkeIzUsluge(ID, Modul);
            }
            
        }

        private void VratiOstalePodatkeIzUsluge(int ID, int Modul)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT [ID]      ,[RegBr]      ,[Datum]      ,[Vozac] " +
     " ,[BrojTelefona]      ,[Napomena]      ,[Modul]      ,[IDUsluge] " +
 " FROM [dbo].[VoziloUsluga]  " +
            "  where IDUsluge=" + ID + " and Modul = " + Modul, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtID.Text = dr["ID"].ToString();
                txtVozilo.Text = dr["RegBr"].ToString();
                dtpDatum.Value = Convert.ToDateTime(dr["Datum"].ToString());
                txtNapomena.Text = dr["Napomena"].ToString();
                txtVozac.Text = dr["Vozac"].ToString();
                txtBrojTelefona.Text = dr["BrojTelefona"].ToString();

                if (dr["ID"].ToString() == "0")
                {
                    chkUvoz.Checked = true;
                }
                txtUsluge.Text = dr["IDUsluge"].ToString();
            }
            con.Close();

        }

        private void VratiOstalePodatke()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT [ID]      ,[RegBr]      ,[Datum]      ,[Vozac] " +
     " ,[BrojTelefona]      ,[Napomena]      ,[Modul]      ,[IDUsluge] " +
 " FROM [dbo].[VoziloUsluga]  " +
            "  where ID=" + txtID.Text, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtID.Text = dr["ID"].ToString();
                txtVozilo.Text = dr["RegBr"].ToString();
                dtpDatum.Value = Convert.ToDateTime(dr["Datum"].ToString());
                txtNapomena.Text = dr["Napomena"].ToString();
                txtVozac.Text = dr["Vozac"].ToString();
                txtBrojTelefona.Text = dr["BrojTelefona"].ToString();

                if (dr["ID"].ToString() == "0")
                {
                    chkUvoz.Checked = true;
                }
                txtUsluge.Text = dr["IDUsluge"].ToString();
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
            int Modultmp = 0;
            if (chkUvoz.Checked == true)
                Modultmp = 0;
            else
            {
                Modultmp = 1;
            }
            InsertVoziloUsluga ins = new InsertVoziloUsluga();
            if (status == true)
            {
                ins.InsVoziloUsluga(txtVozilo.Text,Convert.ToDateTime(dtpDatum.Value), txtVozac.Text,txtBrojTelefona.Text,txtNapomena.Text, Modultmp, Convert.ToInt32(txtUsluge.Text));
            }
            else
            {
                ins.UpdVoziloUsliga(Convert.ToInt32(txtID.Text.ToString()), txtVozilo.Text, Convert.ToDateTime(dtpDatum.Value), txtVozac.Text, txtBrojTelefona.Text, txtNapomena.Text, Modultmp,  Convert.ToInt32(txtUsluge.Text));
            }
            //  FillGV();
            tsNew.Enabled = true;
            status = false;
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertVoziloUsluga del = new InsertVoziloUsluga();
            del.DelVoziloUsluga(Convert.ToInt32(txtID.Text));
            txtID.Text = "";
            txtVozilo.Text = "";
            txtVozac.Text = "";
            txtBrojTelefona.Text = "";
            txtNapomena.Text = "";
            txtUsluge.Text = "";
        }
    }
}
