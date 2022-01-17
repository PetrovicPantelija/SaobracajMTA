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

namespace Saobracaj.Administracija
{
    public partial class frmSistematizacija : Form
    {
        bool status = false;
        public frmSistematizacija()
        {
            InitializeComponent();
        }

        private void RefreshDataGrid()
        {
            var select = " Select ID,Naziv from GrupaDelovnihMesta";
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
            dataGridView1.Columns[1].HeaderText = "Naziv";
            dataGridView1.Columns[1].Width = 350;

           

        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (status == true)
            {
                InsertSistematizacija ins = new InsertSistematizacija();
                ins.InsSistematizacija(txtNaziv.Text);
                RefreshDataGrid();
                status = false;
            }
            else
            {
                InsertSistematizacija upd = new InsertSistematizacija();
                upd.UpdSistematizacija(Convert.ToInt32(txtSifra.Text),  txtNaziv.Text);
                status = false;
                txtSifra.Enabled = false;
                RefreshDataGrid();
            }
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            txtSifra.Text = "";
            txtSifra.Enabled = false;
            txtNaziv.Text = "";
            status = true;
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertSistematizacija upd = new InsertSistematizacija();
            upd.DeleteSistematizacija(Convert.ToInt32(txtSifra.Text));
            status = false;
            txtSifra.Enabled = false;
            RefreshDataGrid();
        }

        private void frmSistematizacija_Load(object sender, EventArgs e)
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
                        // cboGrupaKvarova.SelectedValue = row.Cells[2].Value.ToString();
                    }
                }
              }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }
    }
}
