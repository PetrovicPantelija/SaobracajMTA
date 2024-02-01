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

namespace Saobracaj.Sifarnici
{
    public partial class frmScenario : Form
    {
        bool status = false;
        public frmScenario()
        {
            InitializeComponent();
        }

        private void tsSave_Click(object sender, EventArgs e)
        {

            if (status == true)
            {
                InsertScenario ins = new InsertScenario();
                //0-Ako je novi scenario
                //1-Ako je dodavanje stavke
                ins.InsScenario(0, txtNaziv.Text, Convert.ToInt32(cboUsluga.SelectedValue), cboPokret.Text, Convert.ToInt32(cboStatus.SelectedValue));
                status = false;
            }
            else
            {
                InsertScenario upd = new InsertScenario();
                upd.UpdScenario(Convert.ToInt32(txtSifra.Text), Convert.ToInt32(txtRB.Text), txtNaziv.Text, Convert.ToInt32(cboUsluga.SelectedValue), cboPokret.Text, Convert.ToInt32(cboStatus.SelectedValue) );
            
            }
            RefreshDataGrid();
        }
        private void RefreshDataGrid()
        {
            var select = "";
            select = "Select Scenario.ID,Scenario.RB, Scenario.Naziv, Scenario.Usluga, VrstaManipulacije.Naziv as UslugaN,  Scenario.Pokret, Scenario.StatusKontejnera, " +
" KontejnerStatus.Naziv as StatusN from Scenario " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = Usluga " +
" inner join KontejnerStatus on KontejnerStatus.ID = Scenario.StatusKOntejnera  ";

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
            dataGridView1.Columns[0].HeaderText = "Šifra";
            dataGridView1.Columns[0].Width = 80;
            /*
            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Lok naz";
            dataGridView1.Columns[1].Width = 100;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Lozinka";
            dataGridView1.Columns[2].Width = 150;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Aktivna";
            dataGridView1.Columns[3].Width = 60;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Dizel";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column5 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Masa";
            dataGridView1.Columns[5].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Serija ID";
            dataGridView1.Columns[6].Width = 80;
            */
        }

        private void frmScenario_Load(object sender, EventArgs e)
        {
            var select2 = " Select Distinct ID, Naziv From VrstaManipulacije";
            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboUsluga.DataSource = ds2.Tables[0];
            cboUsluga.DisplayMember = "Naziv";
            cboUsluga.ValueMember = "ID";


            var select3 = " Select Distinct ID, Naziv From KontejnerStatus";
            var s_connection3 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);


            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboStatus.DataSource = ds3.Tables[0];
            cboStatus.DisplayMember = "Naziv";
            cboStatus.ValueMember = "ID";
        }

        private void btnRacun_Click(object sender, EventArgs e)
        {
            InsertScenario ins = new InsertScenario();
            //0-Ako je novi scenario
            //1-Je broj tekuceg scaniraj
            ins.InsScenario(Convert.ToInt32(txtSifra.Text), txtNaziv.Text, Convert.ToInt32(cboUsluga.SelectedValue), cboPokret.Text, Convert.ToInt32(cboStatus.SelectedValue));
                     RefreshDataGrid();
}
        private void VratiPodatkeSelect(string ID)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("Scenario.ID,Scenario.RB, Scenario.Naziv, Scenario.Usluga, Scenario.Pokret, Scenario.StatusKontejnera where ID=" + txtSifra.Text + " AND RB =" + txtRB.Text, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtNaziv.Text = dr["Naziv"].ToString();
                cboUsluga.SelectedValue = Convert.ToInt32(dr["Usluga"].ToString());
                cboPokret.Text = dr["Usluga"].ToString();
                cboStatus.SelectedValue = dr["StatusKontejnera"].ToString();

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
                        txtSifra.Text = row.Cells[0].Value.ToString();
                        txtRB.Text = row.Cells[0].Value.ToString();
                        VratiPodatkeSelect(txtSifra.Text);
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

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
        }
    }
}
