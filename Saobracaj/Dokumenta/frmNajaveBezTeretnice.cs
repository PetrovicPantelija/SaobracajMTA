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

using Microsoft.Reporting.WinForms;

namespace Saobracaj.Dokumenta
{
    public partial class frmNajaveBezTeretnice : Form
    {
        public frmNajaveBezTeretnice()
        {
            InitializeComponent();
        }

        private void frmNajaveBezTeretnice_Load(object sender, EventArgs e)
        {
            var select = "    Select Najava.ID, ts.IDNajave from Najava " +
            " left join (Select distinct IDNajave from TeretnicaStavke) ts " +
 " on Najava.ID = ts.IDNajave " +
" where ts.IDNajave is null ";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
        }
    }
}
