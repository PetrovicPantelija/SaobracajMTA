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
using System.Globalization;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Windows.Forms;

using MetroFramework.Forms;
using System.IO;
using Saobracaj.Pantheon_Export;
using Microsoft.Identity.Client;
using Saobracaj.Uvoz;
using Saobracaj.Izvoz;
using Saobracaj.MainLeget.LegNew;

namespace Saobracaj.Sifarnici
{
    public partial class frmLogovanjeSecond : Form
    {
        /*
        public static string company = "";
        public static string Firma = "";
        public static string connectionString = "";
        public static string PIB = "";
        public static string Naziv = "";
        public static string Grad = "";
        public static string Ulica = "";
        public static string PostanskiBR = "";
        public static string Line = "";
        public static string CompanyID = "";
        public static string MB = "";
        public static string Email = "";
        public static string Skladiste = "";
        public static string Lokacija = "";
        public static string user = "";
        */
        public frmLogovanjeSecond()
        {
            InitializeComponent();

            string basedir = AppDomain.CurrentDomain.BaseDirectory;
            string[] txtFile = Directory.GetFiles(basedir, "*txt");
            
            foreach (string file in txtFile)
            {
                frmLogovanje.company = Path.GetFileNameWithoutExtension(file);
                var companyConfig = ConfigManager.GetCompanyConfiguration(frmLogovanje.company);
                frmLogovanje.connectionString = companyConfig.DB;
                frmLogovanje.Firma = companyConfig.Naziv;
                frmLogovanje.PIB = companyConfig.PIB;
                frmLogovanje.Naziv = companyConfig.Name_Value;
                frmLogovanje.Ulica = companyConfig.Ulica_Value;
                frmLogovanje.Grad = companyConfig.Grad_Value;
                frmLogovanje.PostanskiBR = companyConfig.PostanskiBroj_Value;
                frmLogovanje.Line = companyConfig.Line_Value;
                frmLogovanje.CompanyID = companyConfig.CompanyID_Value;
                frmLogovanje.MB = companyConfig.MB_Value;
                frmLogovanje.Email = companyConfig.EmailSender_Value;
                frmLogovanje.Skladiste = companyConfig.OsnovnoSkladiste;
                frmLogovanje.Lokacija = companyConfig.OsnovnaLokacija;
            }
        }

        private void FillCombo()
        {
            var select = " Select Distinct RTrim(Korisnik) as Korisnik From Korisnici";
            SqlConnection myConnection = new SqlConnection(frmLogovanje.connectionString);
            var dataAdapter = new SqlDataAdapter(select, myConnection);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboKorisnik.DataSource = ds.Tables[0];
            cboKorisnik.DisplayMember = "Korisnik";
            cboKorisnik.ValueMember = "Korisnik";


            var select2 = " Select Distinct RTrim(Korisnik) as Korisnik From Korisnici";
            SqlConnection myConnection2 = new SqlConnection(frmLogovanje.connectionString);
            var dataAdapter2 = new SqlDataAdapter(select2, myConnection);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboKorisnik.DataSource = ds2.Tables[0];
            cboKorisnik.DisplayMember = "Korisnik";
            cboKorisnik.ValueMember = "Korisnik";
        }

        private void sfButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLogovanjeSecond_Load(object sender, EventArgs e)
        {
           
            FillCombo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection myConnection = new SqlConnection(frmLogovanje.connectionString);
            SqlCommand command = new SqlCommand("SELECT Korisnik FROM Korisnici where Rtrim(Password) = '" + txtPassword.Text + "' and RTRIM(Korisnik) = '" + cboKorisnik.Text + "'", myConnection);
            myConnection.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows == false)
            {
                MessageBox.Show("Nije ispravno uneta lozinka");
                return;
            }
            while (reader.Read())
            {
                string myString = reader.GetString(0); //The 0 stands for "the 0'th column", so the first column of the result.
                // Do somthing with this rows string, for example to put them in to a list
                if (myString.TrimEnd() == cboKorisnik.Text)
                {
                    frmLogovanje.user = myString;
                    if (checkBox1.Checked)
                    {
                        MainP mainf = new MainP(cboKorisnik.Text, 1);
                        mainf.Show();
                    }
                    if (checkBox2.Checked)
                    {
                        /*
                        LogistikaIzvoza1 frm = new LogistikaIzvoza1();
                        frm.Show(); Drumski1 frm = new Drumski1();
                        frm.Show();
                        */
                        NewMain frm = new NewMain();
                        frm.Show();
                    }
                    
                }
                else
                {
                    MessageBox.Show("Nije jedinstvena lozinka");
                    return;
                }
            }
        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            Administracija.frmSistemskaTabela sis = new Administracija.frmSistemskaTabela();
            sis.Show();
        }
    }
}
