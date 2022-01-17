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
    public partial class frmSifarnikKontrolnihGresaka : Syncfusion.Windows.Forms.Office2010Form
    {
        bool status = false;
        public frmSifarnikKontrolnihGresaka()
        {
            InitializeComponent();
        }

        private void RefreshDataGrid()
        {
            var select = "  select KontrolneGreske.ID, KontrolneGreske.Naziv, KontrolneGreskeTipDokumenta.Naziv as TipDokumenta from KontrolneGreske" +
                " inner join KontrolneGreskeTipDokumenta on KontrolneGreskeTipDokumenta.ID = KontrolneGreske.TipDokumenta ";


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
            dataGridView1.Columns[1].Width = 170;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Tip dokumenta";
            dataGridView1.Columns[1].Width = 170;
        }

        private void frmSifarnikKontrolnihGresaka_Load(object sender, EventArgs e)
        {
            var select3 = " select ID, Naziv from KontrolneGreskeTipDokumenta order by Naziv";
            var s_connection3 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboTipDokumenta.DataSource = ds3.Tables[0];
            cboTipDokumenta.DisplayMember = "Naziv";
            cboTipDokumenta.ValueMember = "ID";


            RefreshDataGrid();
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
            InsertKontrolneGreske del = new InsertKontrolneGreske();
            del.DeleteKontrolneGreske(Convert.ToInt32(txtSifra.Text));
            status = false;
            txtSifra.Enabled = false;
            RefreshDataGrid();
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (status == true)
            {
                InsertKontrolneGreske ins = new InsertKontrolneGreske();
                ins.InsKontrolneGreske(txtNaziv.Text, Convert.ToInt32(cboTipDokumenta.SelectedValue));
                RefreshDataGrid();
                status = false;
            }
            else
            {
                InsertKontrolneGreske upd = new InsertKontrolneGreske();
                upd.UpdKontrolneGreske(Convert.ToInt32(txtSifra.Text), txtNaziv.Text, Convert.ToInt32(cboTipDokumenta.SelectedValue));
                status = false;
                txtSifra.Enabled = false;
                RefreshDataGrid();
            }
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
