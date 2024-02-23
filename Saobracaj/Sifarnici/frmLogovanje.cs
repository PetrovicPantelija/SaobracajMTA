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

namespace Saobracaj.Sifarnici
{
    public partial class frmLogovanje : Syncfusion.Windows.Forms.Office2010Form
    {
        //
        public string company = "";

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

        public static string user = "";
        public frmLogovanje()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            InitializeComponent();
            Main();
        }
        private void Main()
        {
            string basedir = AppDomain.CurrentDomain.BaseDirectory;
            string[] txtFile = Directory.GetFiles(basedir, "*txt");
           // string company = "";
            foreach(string file in txtFile)
            {
                company = Path.GetFileNameWithoutExtension(file);
            }
            var companyConfig = ConfigManager.GetCompanyConfiguration(company);
            connectionString = companyConfig.DB;
            Firma = companyConfig.Naziv;
            PIB= companyConfig.PIB;
            Naziv = companyConfig.Name_Value;
            Ulica = companyConfig.Ulica_Value;
            Grad = companyConfig.Grad_Value;
            PostanskiBR = companyConfig.PostanskiBroj_Value;
            Line = companyConfig.Line_Value;
            CompanyID = companyConfig.CompanyID_Value;
            MB= companyConfig.MB_Value;
            Email = companyConfig.EmailSender_Value;
        }
        private void frmLogovanje_Load(object sender, EventArgs e)
        {
            var select = " Select Distinct RTrim(Korisnik) as Korisnik From Korisnici";
            SqlConnection myConnection = new SqlConnection(connectionString);
            var dataAdapter = new SqlDataAdapter(select, myConnection);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboKorisnik.DataSource = ds.Tables[0];
            cboKorisnik.DisplayMember = "Korisnik";
            cboKorisnik.ValueMember = "Korisnik";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // var select = " Select Distinct DatumUtovara From RkShipping where Stanje = 1 and Vozilo = '" + cboVozila.Text + "'";
            SqlConnection myConnection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("SELECT Korisnik FROM Korisnici where Rtrim(Password) = '" + txtPassword.Text + "' and RTRIM(Korisnik) = '"+ cboKorisnik.Text + "'", myConnection);
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
                    user = myString;
                    MainP mainf = new MainP(cboKorisnik.Text, 1);
                    mainf.Show();
                }
                else
                {
                    MessageBox.Show("Nije jedinstvena lozinka");
                    return;
                }
            }
        }

        private void button1_Enter(object sender, EventArgs e)
        {
            user = cboKorisnik.Text.ToString();
            // var select = " Select Distinct DatumUtovara From RkShipping where Stanje = 1 and Vozilo = '" + cboVozila.Text + "'";
            SqlConnection myConnection = new SqlConnection(connectionString);
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
                    user = myString;
                    MainP mainf = new MainP(cboKorisnik.Text, 1);
                    mainf.Show();
                    
                }
                else
                {
                    MessageBox.Show("Nije ispravno uneta lozinka");
                    return;
                }
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            KrajnjeDestinacije frm = new KrajnjeDestinacije();
            frm.Show();
        }
    }
}
