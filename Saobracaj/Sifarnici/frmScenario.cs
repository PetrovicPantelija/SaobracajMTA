using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Sifarnici
{
    public partial class frmScenario : Syncfusion.Windows.Forms.Office2010Form
    {
        bool status = false;
        int vizuelniPregled = 0;
        int cir = 0;
        public frmScenario()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            InitializeComponent();
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (cbCir.Checked == true)
            {
                cir = 1;
            }
            if (cbVizuelni.Checked == true)
            {
                vizuelniPregled = 1;
            }
            if (status == true)
            {
                InsertScenario ins = new InsertScenario();
                //0-Ako je novi scenario
                //1-Ako je dodavanje stavke
                ins.InsScenario(0, txtNaziv.Text, Convert.ToInt32(cboUsluga.SelectedValue), cboPokret.Text, Convert.ToInt32(cboStatus.SelectedValue), cboForma.SelectedText,Convert.ToInt32(cboOJ.Text.Substring(0,1)),vizuelniPregled,cir);
                status = false;
            }
            else
            {
                InsertScenario upd = new InsertScenario();
                upd.UpdScenario(Convert.ToInt32(txtSifra.Text), Convert.ToInt32(txtRB.Text), txtNaziv.Text, Convert.ToInt32(cboUsluga.SelectedValue), cboPokret.Text, Convert.ToInt32(cboStatus.SelectedValue), cboForma.SelectedText,Convert.ToInt32(cboOJ.Text.Substring(0,1)),vizuelniPregled,cir);

            }
            RefreshDataGrid();
            vizuelniPregled = 0;cir = 0;
        }
        private void RefreshDataGrid()
        {
            var select = "";
            select = @" SELECT 
    Scenario.ID,
    Scenario.RB,
    Scenario.Naziv,
    Scenario.Usluga,
    VrstaManipulacije.Naziv AS UslugaN,
    Scenario.Pokret,
    Scenario.StatusKontejnera,
    KontejnerStatus.Naziv AS StatusN,
    Scenario.Forma,
    CASE 
        WHEN Scenario.OJIzdavanje = 1 THEN '1-Uvoz'
        WHEN Scenario.OJIzdavanje = 2 THEN '2-Izvoz'
        WHEN Scenario.OJIzdavanje = 4 THEN '4-Terminal'
        ELSE ''
    END AS OJIzdavanje,VizuelniPregled,CIR
FROM 
    Scenario
INNER JOIN 
    VrstaManipulacije ON VrstaManipulacije.ID = Scenario.Usluga
INNER JOIN 
    KontejnerStatus ON KontejnerStatus.ID = Scenario.StatusKontejnera
ORDER BY 
    Scenario.ID,
    Scenario.RB";

            //  "  where  Aktivnosti.Masinovodja = 1 and Zaposleni = " + Convert.ToInt32(cboZaposleni.SelectedValue) + " order by Aktivnosti.ID desc";


            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "RB";
            dataGridView1.Columns[1].Width = 30;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Naziv";
            dataGridView1.Columns[2].Width = 130;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "USL";
            dataGridView1.Columns[3].Width = 30;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Usluga";
            dataGridView1.Columns[4].Width = 230;

            DataGridViewColumn column5 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Pokret";
            dataGridView1.Columns[5].Width = 130;

            DataGridViewColumn column6 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "StID";
            dataGridView1.Columns[6].Width = 40;

            DataGridViewColumn column7 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Status";
            dataGridView1.Columns[7].Width = 140;

        }

        private void frmScenario_Load(object sender, EventArgs e)
        {
            var select2 = " Select Distinct ID, Naziv From VrstaManipulacije";
            var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
            var s_connection3 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);


            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboStatus.DataSource = ds3.Tables[0];
            cboStatus.DisplayMember = "Naziv";
            cboStatus.ValueMember = "ID";

            RefreshDataGrid();

            cboOJ.Items.Add("1-Uvoz");
            cboOJ.Items.Add("2-Izvoz");
            cboOJ.Items.Add("4-Terminal");


        }

        private void btnRacun_Click(object sender, EventArgs e)
        {
            InsertScenario ins = new InsertScenario();
            //0-Ako je novi scenario
            //1-Je broj tekuceg scaniraj
            if (cbCir.Checked == true)
            {
                cir = 1;
            }
            if (cbVizuelni.Checked == true)
            {
                vizuelniPregled = 1;
            }
            ins.InsScenario(Convert.ToInt32(txtSifra.Text), txtNaziv.Text, Convert.ToInt32(cboUsluga.SelectedValue), cboPokret.Text, Convert.ToInt32(cboStatus.SelectedValue), cboForma.Text,Convert.ToInt32(cboOJ.Text.Substring(0,1)),vizuelniPregled,cir);
            RefreshDataGrid();
        }
        private void VratiPodatkeSelect(string ID, string RB)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("Select Scenario.ID,Scenario.RB, Scenario.Naziv, Scenario.Usluga, Scenario.Pokret, Scenario.StatusKontejnera, Forma,VizuelniPregled,CIR from Scenario where ID=" + ID + " AND RB =" + RB, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtNaziv.Text = dr["Naziv"].ToString();
                cboUsluga.SelectedValue = Convert.ToInt32(dr["Usluga"].ToString());
                cboPokret.Text = dr["Pokret"].ToString();
                cboStatus.SelectedValue = dr["StatusKontejnera"].ToString();
                cboForma.Text = dr["Forma"].ToString();
                int Vizuelni = Convert.ToInt32(dr["VizuelniPregled"].ToString());
                int Cir = Convert.ToInt32(dr["CIR"].ToString());

                if (Vizuelni == 1)
                {
                    cbVizuelni.Checked = true;
                }
                else { cbVizuelni.Checked = false; }
                if(Cir == 1)
                {
                    cbCir.Checked = true;
                }
                else { cbCir.Checked = false; }
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
                        txtRB.Text = row.Cells[1].Value.ToString();
                        cboOJ.Text = row.Cells["OJIzdavanje"].Value.ToString();
                        VratiPodatkeSelect(txtSifra.Text, txtRB.Text);
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

        private void tsDelete_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmTerminalProces tp = new frmTerminalProces(txtSifra.Text);
            tp.Show();
        }
    }
}
