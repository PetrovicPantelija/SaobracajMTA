using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Servis
{
    public partial class frmVrstePopisa : Form
    {
        bool status = false;
        public frmVrstePopisa()
        {
            InitializeComponent();

        }
        string niz = "";
        public static string code = "frmVrstePopisa";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Sifarnici.frmLogovanje.user.ToString();

        private void RefreshDataGrid()
        {
            var select = " Select ID, Naziv, LargoID from VrstaPopisa";
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

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Largo ID";
            dataGridView1.Columns[2].Width = 100;

        }

        private void frmVrstePopisa_Load(object sender, EventArgs e)
        {
            var select3 = " select MpSifra, (Cast(MpSifra as nvarchar(5)) + '-' + MpStaraSif) as Naziv from MaticniPodatki";
            var s_connection3 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboLargoID.DataSource = ds3.Tables[0];
            cboLargoID.DisplayMember = "Naziv";
            cboLargoID.ValueMember = "MpSifra";


            RefreshDataGrid();
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            txtSifra.Text = "";
            txtSifra.Enabled = false;
            txtOpis.Text = "";
            status = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (status == true)
            {
                InsertVrstaPopisa ins = new InsertVrstaPopisa();
                ins.InsVrstaPopisa(txtOpis.Text, Convert.ToInt32(cboLargoID.SelectedValue));
                RefreshDataGrid();
                status = false;
            }
            else
            {
                InsertVrstaPopisa upd = new InsertVrstaPopisa();
                upd.UpdVrstaPopisa(Convert.ToInt32(txtSifra.Text), txtOpis.Text, Convert.ToInt32(cboLargoID.SelectedValue));
                status = false;
                txtSifra.Enabled = false;
                RefreshDataGrid();
            }
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertVrstaPopisa del = new InsertVrstaPopisa();
            del.DelVrstaPopisa(Convert.ToInt32(txtSifra.Text));
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
                        txtOpis.Text = row.Cells[1].Value.ToString();
                        cboLargoID.SelectedValue = Convert.ToInt32(row.Cells[2].Value.ToString());
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
