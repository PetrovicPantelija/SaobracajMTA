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
    public partial class frmNHM : Form
    {
        bool status = false;
        int chekiran = 0;
        public frmNHM()
        {
            InitializeComponent();
        }
        private void RefreshDataGrid()
        {
            var select = " Select ID,Broj, Naziv, CASE WHEN RID > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as RID from NHM";
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
            dataGridView1.Columns[1].HeaderText = "Broj";
            dataGridView1.Columns[1].Width = 150;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Naziv";
            dataGridView1.Columns[2].Width = 300;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "RID";
            dataGridView1.Columns[3].Width = 100;

        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (chkRid.Checked == true)
            {
                chekiran = 1;
            }
            else
            {
                chekiran = 0;
            }
            if (status == true)
            {
                Insertnhm ins = new Insertnhm();
                ins.InsNHM(txtBroj.Text,  txtNaziv.Text, chekiran);
                RefreshDataGrid();
                status = false;
            }
            else
            {
                Insertnhm upd = new Insertnhm();
                upd.UpdStanice(Convert.ToInt32(txtSifra.Text), txtBroj.Text, txtNaziv.Text, chekiran);
                status = false;
                txtSifra.Enabled = false;
                RefreshDataGrid();
            }
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            txtSifra.Text = "";
            txtSifra.Enabled = false;
            txtBroj.Text = "";
            txtNaziv.Text = "";

            status = true;
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            Insertnhm del = new Insertnhm();
            del.DeleteStanice(Convert.ToInt32(txtSifra.Text));
            status = false;
            txtSifra.Enabled = false;
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
                        txtBroj.Text = row.Cells[1].Value.ToString();
                        txtNaziv.Text = row.Cells[2].Value.ToString();
                        chkRid.Checked = Convert.ToBoolean(row.Cells[3].Value.ToString());
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela promena stavki");
            }
        }

        private void frmNHM_Load(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }
        }
    }

