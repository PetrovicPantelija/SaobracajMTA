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
    public partial class frmRIDTeretnice : Form
    {
        public frmRIDTeretnice()
        {
            InitializeComponent();
        }

        private void frmRIDTeretnice_Load(object sender, EventArgs e)
        {
            // Teretnice bez RID-a
             var select = "    Select Najava.RID, ts.RID, ts.* from Najava " +
             " inner join TeretnicaStavke ts on Najava.ID = ts.IDNajave " +
             " where Najava.RID = 1 and [Status] not in (7,8) and ts.RID  = ''";

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
