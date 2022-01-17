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

namespace Saobracaj.Sifarnici
{
    public partial class frmSifarnikKontrolnihTipovaDokumenta : Syncfusion.Windows.Forms.Office2010Form
    {
        bool status = false; 
        public frmSifarnikKontrolnihTipovaDokumenta()
        {
            InitializeComponent();
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            txtSifra.Text = "";
            txtSifra.Enabled = false;
            txtNaziv.Text = "";
            status = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (status == true)
            {
                InsertKontrolneGreskeTipDokumenta ins = new InsertKontrolneGreskeTipDokumenta();
                ins.InsKontrolneGreskeTipDokumenta(txtNaziv.Text);
                RefreshDataGrid();
                status = false;
            }
            else
            {
                InsertKontrolneGreskeTipDokumenta upd = new InsertKontrolneGreskeTipDokumenta();
                upd.UpdKontrolneGreskeTipDokumenta(Convert.ToInt32(txtSifra.Text), txtNaziv.Text);
                status = false;
                txtSifra.Enabled = false;
                RefreshDataGrid();
            }
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {

            InsertKontrolneGreskeTipDokumenta del = new InsertKontrolneGreskeTipDokumenta();
            del.DeleteKontrolneGreskeTipDokumenta(Convert.ToInt32(txtSifra.Text));
            status = false;
            txtSifra.Enabled = false;
            RefreshDataGrid();
        }
        private void RefreshDataGrid()
        {
            var select = "  select ID, Naziv from KontrolneGreskeTipDokumenta";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Naziv";
            dataGridView1.Columns[1].Width = 250;
        }
        private void frmSifarnikKontrolnihTipovaDokumenta_Load(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txtSifra.Text = row.Cells[0].Value.ToString();
                        txtNaziv.Text = row.Cells[1].Value.ToString();
                       
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela promena stavki");
            }
        }
    }
}
