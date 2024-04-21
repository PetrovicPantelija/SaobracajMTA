using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Syncfusion.Windows.Forms.Grid.Grouping;

namespace Saobracaj.Izvoz
{
    public partial class DogovoreniPosloviIzvoza : Syncfusion.Windows.Forms.Office2010Form
    {
        Boolean status = false;
        public DogovoreniPosloviIzvoza()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
        }

        private void DogovoreniPosloviIzvoza_Load(object sender, EventArgs e)
        {
            var select9 = " Select Distinct PaSifra, PaNaziv From PArtnerji order by PaNaziv";
            var s_connection9 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection9 = new SqlConnection(s_connection9);
            var c9 = new SqlConnection(s_connection9);
            var dataAdapter9 = new SqlDataAdapter(select9, c9);

            var commandBuilder9 = new SqlCommandBuilder(dataAdapter9);
            var ds9 = new DataSet();
            dataAdapter9.Fill(ds9);
            cboPartner.DataSource = ds9.Tables[0];
            cboPartner.DisplayMember = "PaNaziv";
            cboPartner.ValueMember = "PaSifra";

            RefreshDataGrid();
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtID.Enabled = false;
            txtNapomena.Text = "";
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            int tmpZatvoren = 0;
            if (chkZavrsen.Checked == true)
            {
                tmpZatvoren = 1;
            }
            if (status == true)
            {
                InsertDogovorenoIzvoz ins = new InsertDogovorenoIzvoz();
                ins.InsDogovorenoIzvoz(Convert.ToInt32(cboPartner.SelectedValue), Convert.ToDateTime(dtpPeriodOd.Value), Convert.ToDateTime(dtpPeriodDo.Value), Convert.ToInt32(txtBrojKontejnera.Value), Convert.ToInt32(txtBrojIsporucenih.Value), txtNapomena.Text, tmpZatvoren );
                status = false;
            }
            else
            {
                InsertDogovorenoIzvoz ins = new InsertDogovorenoIzvoz();
                ins.UpdDogovorenoIzvoz(Convert.ToInt32(txtID.Text), Convert.ToInt32(cboPartner.SelectedValue), Convert.ToDateTime(dtpPeriodOd.Value), Convert.ToDateTime(dtpPeriodDo.Value), Convert.ToInt32(txtBrojKontejnera.Value), Convert.ToInt32(txtBrojIsporucenih.Value), txtNapomena.Text, tmpZatvoren);
           
            }
            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            var select = " SELECT [ID], Partnerji.PaNaziv as Partner " +
     " ,[PeriodOd]      ,[PeriodDo] " +
     "  ,[BrojUgovorenih]      ,[BrojUradjenih] " +
     "  ,[Napomena]      ,[Zatvoren] " +
       "      FROM [dbo].[DogovorenoIzvoz] " +
"   inner join PArtnerji on Partnerji.PaSifra = [Partner]";



            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);


            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            dataGridView2.BorderStyle = BorderStyle.None;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView2.BackgroundColor = Color.White;

            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "Šifra";
            dataGridView2.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "Partner";
            dataGridView2.Columns[1].Width = 150;



        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertDogovorenoIzvoz del = new InsertDogovorenoIzvoz();
            del.DelDogovorenoIzvoz(Convert.ToInt32(txtID.Text));
            RefreshDataGrid();
        }

        private void VratiPodatke(int ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT [ID]      ,[Partner] " +
    " ,[PeriodOd]      ,[PeriodDo]      ,[BrojUgovorenih] " +
     "  ,[BrojUradjenih]      ,[Napomena]      ,[Zatvoren] " +
 "  FROM[dbo].[DogovorenoIzvoz] " +
             " where ID= " + ID, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtID.Text = dr["ID"].ToString();
              
                cboPartner.SelectedValue = Convert.ToInt32(dr["Partner"].ToString());

                dtpPeriodDo.Value = Convert.ToDateTime(dr["PeriodDo"].ToString());
                dtpPeriodOd.Value = Convert.ToDateTime(dr["PeriodOd"].ToString());
                txtBrojKontejnera.Value = Convert.ToInt32(dr["BrojUgovorenih"].ToString());
               txtBrojIsporucenih.Value = Convert.ToInt32(dr["BrojUradjenih"].ToString());
                txtNapomena.Text = dr["Napomena"].ToString();
                if (dr["Zatvoren"].ToString() == "1")
                {
                    chkZavrsen.Checked = true;
                }
                else
                {
                    chkZavrsen.Checked = false;

                }
            }

            con.Close();
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {
                        txtID.Text = row.Cells[0].Value.ToString();
                       
                        VratiPodatke(Convert.ToInt32(txtID.Text));

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
