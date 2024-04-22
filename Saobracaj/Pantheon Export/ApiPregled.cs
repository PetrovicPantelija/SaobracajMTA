using Saobracaj.Sifarnici;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Pantheon_Export
{
    public partial class ApiPregled : Form
    {
        string connect = frmLogovanje.connectionString;
        public ApiPregled()
        {
            InitializeComponent();
        }

        private void ApiPregled_Load(object sender, EventArgs e)
        {
            var query = "Select * from ApiLogovi order by ID desc";
            SqlConnection conn = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[3].Width = 1000;
            dataGridView1.Columns[4].Width = 500;
        }
    }
}
