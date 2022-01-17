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

namespace Saobracaj.Sifarnici
{
    public partial class frmStatusVoza : Form
    {
        bool status = false;
        public frmStatusVoza()
        {
            InitializeComponent();
        }

        private void RefreshDataGrid()
        {
            var select = " Select * from StatusVoza";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Opis";
            dataGridView1.Columns[1].Width = 150;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (status == true)
            {
                InsertStatusVoza ins = new InsertStatusVoza();
                ins.InsStatusVoza(txtOpis.Text);
                RefreshDataGrid();
                status = false;
            }
            else
            {
                
                InsertStatusVoza upd = new InsertStatusVoza();
                upd.UpdStanice(Convert.ToInt32(txtSifra.Text), txtOpis.Text);
                status = false;
                txtSifra.Enabled = false;
                RefreshDataGrid();
                 
            }
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            txtSifra.Text = "";
            txtSifra.Enabled = false;
            txtSifra.Text = "";
            txtOpis.Text = "";
            status = true;
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertStatusVoza del = new InsertStatusVoza();
            del.DeleteStatusVoza(Convert.ToInt32(txtSifra.Text));
            status = false;
            txtSifra.Enabled = false;
            RefreshDataGrid();
        }

        private void frmStatusVoza_Load(object sender, EventArgs e)
        {
           RefreshDataGrid();
        }
    }
}
