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

using Microsoft.Reporting.WinForms;

namespace Saobracaj.Dokumenta
{
    public partial class frmPredjenaKilometrazaLokomotiva : Form
    {
        public frmPredjenaKilometrazaLokomotiva()
        {
            InitializeComponent();
        }

      

            private void RefreshDataGrid()
            {
                var select = " Select * from LokomotivaPredjeno";

                var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
                SqlConnection myConnection = new SqlConnection(s_connection);
                var c = new SqlConnection(s_connection);
                var dataAdapter = new SqlDataAdapter(select, c);

                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new DataSet();
                dataAdapter.Fill(ds);
                dataGridView1.ReadOnly = false;
                dataGridView1.DataSource = ds.Tables[0];



        }

        private void btnIzracunaj_Click(object sender, EventArgs e)
        {
            InsertProracunKM ins = new InsertProracunKM();

      
            ins.InsProracunKM(Convert.ToDateTime(dtpVremeOd.Value), Convert.ToDateTime(dtpVremeDo.Value));
            ins.InsProracunKM2(Convert.ToDateTime(dtpVremeOd.Value), Convert.ToDateTime(dtpVremeDo.Value));
            RefreshDataGrid();
            MessageBox.Show("Gotovo, to ti je završeno");
        }
    }
}
