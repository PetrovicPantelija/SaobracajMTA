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
using Saobracaj.Tehnologija;

namespace Saobracaj.Sifarnici
{
    public partial class frmTerminalProces : Syncfusion.Windows.Forms.Office2010Form
    {
        bool status = false;
        string Scenario = "";
        public frmTerminalProces()
        {
            InitializeComponent();
        }

        public frmTerminalProces(string SifraScenaria)
        {
            InitializeComponent();
            Scenario = SifraScenaria;
        }


        private void tsSave_Click(object sender, EventArgs e)
        {
            if (status == true)
            {
                InserTerminalProces ins = new InserTerminalProces();
                //0-Ako je novi scenario
                //1-Ako je dodavanje stavke
                ins.InsTerminalProces(Convert.ToInt32(cboScenario.SelectedValue),  Convert.ToInt32(cboTerminalOperacije.SelectedValue) );
                status = false;
            }
            else
            {
                InserTerminalProces upd = new InserTerminalProces();
                upd.UpdTerminalProces(Convert.ToInt32(txtID.Text), Convert.ToInt32(txtRB.Text), Convert.ToInt32(cboScenario.SelectedValue), Convert.ToInt32(cboTerminalOperacije.SelectedValue));

            }
            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            var select = "";
            select = " SELECT[ID] ,[RB] ,[ScenarioID] ,[TerminalOperacijaID]  FROM [dbo].[TerminalProces] ";

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

            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "RB";
            dataGridView1.Columns[1].Width = 30;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Scenario";
            dataGridView1.Columns[2].Width = 130;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Terminalska operacija";
            dataGridView1.Columns[3].Width = 30;

            
        }


        private void RefreshDataGridPoScenariju()
        {
            var select = "";
            select = " SELECT TerminalProces.[ID] ,TerminalProces.[RB] ,[ScenarioID] ,[TerminalOperacijaID], TerminalOperacije.Naziv  FROM [dbo].[TerminalProces]" +
                " inner join TerminalOperacije on TerminalOperacije.ID = TerminalOperacijaID where [ScenarioID]  = " + Scenario;

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

            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "RB";
            dataGridView1.Columns[1].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Scenario";
            dataGridView1.Columns[2].Width = 40;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Terminalska operacija ID";
            dataGridView1.Columns[3].Width = 30;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Terminalska operacija ";
            dataGridView1.Columns[4].Width = 130;


        }


        private void frmTerminalProces_Load(object sender, EventArgs e)
        {

            var select2 = " SELECT [ID]      ,[Naziv]  FROM [dbo].[TerminalOperacije]";
            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboTerminalOperacije.DataSource = ds2.Tables[0];
            cboTerminalOperacije.DisplayMember = "Naziv";
            cboTerminalOperacije.ValueMember = "ID";


            var select3 = " Select Distinct ID From Scenario";
            var s_connection3 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);


            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboScenario.DataSource = ds3.Tables[0];
            cboScenario.DisplayMember = "ID";
            cboScenario.ValueMember = "ID";

            cboScenario.SelectedValue = Scenario;
            RefreshDataGridPoScenariju();



        }

        private void VratiPodatkeSelect(string ID, string RB)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT [ID]      ,[RB]      ,[ScenarioID]      ,[TerminalOperacijaID]  FROM [dbo].[TerminalProces] where ID=" + ID + " AND RB =" + RB, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtRB.Text = dr["RB"].ToString();
                cboScenario.SelectedValue = Convert.ToInt32(dr["ScenarioID"].ToString());

                cboTerminalOperacije.SelectedValue = dr["TerminalOperacijaID"].ToString();
            }

            con.Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txtID.Text = row.Cells[0].Value.ToString();
                        txtRB.Text = row.Cells[1].Value.ToString();
                        VratiPodatkeSelect(txtID.Text, txtRB.Text);
                        // txtOznaka.Enabled = false;
                        // txtOpis.Text = row.Cells[1].Value.ToString();
                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InserTerminalProces del = new InserTerminalProces();
            //0-Ako je novi scenario
            //1-Je broj tekuceg scaniraj
            del.DelTerminalProces(Convert.ToInt32(txtID.Text));
            RefreshDataGrid();
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtID.Text = "";
            txtRB.Text = "";
        }

        private void btnRacun_Click(object sender, EventArgs e)
        {
            RefreshDataGridPoScenariju();

        }
    }
}
