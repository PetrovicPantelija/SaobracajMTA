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
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Windows.Forms;

using Microsoft.Reporting.WinForms;
namespace Saobracaj.Sifarnici
{
    public partial class frmLokomotive : Syncfusion.Windows.Forms.Office2010Form
    {
        public frmLokomotive()
        {
            InitializeComponent();
        }

        private void frmLokomotive_Load(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            var select = "";
            select = "select  SmSifra, SmNaziv, SmOpis, Password, StatusLokomotive, Dizel, SmPogDela from Mesta where Lokomotiva = 1  ";

            //  "  where  Aktivnosti.Masinovodja = 1 and Zaposleni = " + Convert.ToInt32(cboZaposleni.SelectedValue) + " order by Aktivnosti.ID desc";


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
            dataGridView1.Columns[0].HeaderText = "Lok";
            dataGridView1.Columns[0].Width = 80;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Lok naz";
            dataGridView1.Columns[1].Width = 100;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Lok opis";
            dataGridView1.Columns[2].Width = 150;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Lozinka";
            dataGridView1.Columns[3].Width = 100;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Aktivna";
            dataGridView1.Columns[4].Width = 60;

            DataGridViewColumn column5 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Dizel";
            dataGridView1.Columns[5].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Masa";
            dataGridView1.Columns[6].Width = 80;


        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txtLokomotiva.Text = row.Cells[0].Value.ToString();
                        txtPassword.Text = row.Cells[3].Value.ToString();
                        if (row.Cells[4].Value.ToString() == "1")
                        {
                            chkAktivna.Checked = true;
                        }
                        else
                        {
                            chkAktivna.Checked = false;
                        }
                        if (row.Cells[5].Value.ToString() == "1")
                        {
                            chkDizel.Checked = true;
                        }
                        else
                        {
                            chkDizel.Checked = false;
                        }
                        txtMasa.Value = Convert.ToDecimal(row.Cells[5].Value.ToString());


                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void tsSave_Click(object sender, EventArgs e)
        {

        }

        private void btnRacun_Click(object sender, EventArgs e)
        {

            int Aktivna = 0;
            int Dizel = 0;
            if (chkAktivna.Checked == true)
            {
                Aktivna = 1;
            }

            if (chkDizel.Checked == true)
            {
                Dizel = 1;
            }

            InsertLokomotive ins = new InsertLokomotive();
            ins.InsLokomotive(txtLokomotiva.Text, txtPassword.Text, Convert.ToDouble(txtMasa.Value), Dizel, Aktivna);
            RefreshDataGrid();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
