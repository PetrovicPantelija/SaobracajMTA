using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
namespace Saobracaj.Sifarnici
{
    public partial class frmLokomotive : Syncfusion.Windows.Forms.Office2010Form
    {
        public static string code = "frmLokomotive";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Sifarnici.frmLogovanje.user.ToString();
        string niz = "";
        bool status = false;

        public frmLokomotive()
        {
            InitializeComponent();

        }
        private void frmLokomotive_Load(object sender, EventArgs e)
        {

            var select2 = " Select Distinct ID, (Rtrim(Oznaka) + ' - ' + RTrim(Opis)) as Naziv From LokomotivaSerija";
            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboSerija.DataSource = ds2.Tables[0];
            cboSerija.DisplayMember = "Naziv";
            cboSerija.ValueMember = "ID";

            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            var select = "";
            select = "select  SmSifra, SmNaziv,  Password, StatusLokomotive, Dizel, SmPogDela as Masa , Serija from Mesta where Lokomotiva = 1  ";

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
                        txtNaziv.Text = row.Cells[1].Value.ToString();
                        txtPassword.Text = row.Cells[2].Value.ToString();
                        if (row.Cells[3].Value.ToString() == "1")
                        {
                            chkAktivna.Checked = true;
                        }
                        else
                        {
                            chkAktivna.Checked = false;
                        }
                        if (row.Cells[4].Value.ToString() == "1")
                        {
                            chkDizel.Checked = true;
                        }
                        else
                        {
                            chkDizel.Checked = false;
                        }
                        txtMasa.Value = Convert.ToDecimal(row.Cells[5].Value.ToString());
                        cboSerija.SelectedValue = Convert.ToInt32(row.Cells[6].Value.ToString());

                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
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
            ins.InsLokomotiva(txtLokomotiva.Text, txtNaziv.Text, txtPassword.Text, Convert.ToDouble(txtMasa.Value), Dizel, Aktivna, Convert.ToInt32(cboSerija.SelectedValue));
            RefreshDataGrid();
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            txtLokomotiva.Text = "";
            //  txtLokomotiva.Enabled = false;
            txtPassword.Text = "";
            txtNaziv.Text = "";
            txtMasa.Value = 0;
            chkAktivna.Checked = false;

            status = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
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

            if (status == true)
            {
                InsertLokomotive ins = new InsertLokomotive();
                ins.InsLokomotiva(txtLokomotiva.Text, txtNaziv.Text, txtPassword.Text, Convert.ToDouble(txtMasa.Value), Dizel, Aktivna, Convert.ToInt32(cboSerija.SelectedValue));
                RefreshDataGrid();
                status = false;
            }
            else
            {

                InsertLokomotive upd = new InsertLokomotive();
                upd.UpdLokomotive(txtLokomotiva.Text, txtNaziv.Text, txtPassword.Text, Convert.ToDouble(txtMasa.Value), Dizel, Aktivna, Convert.ToInt32(cboSerija.SelectedValue));
                status = false;
                txtLokomotiva.Enabled = false;
                RefreshDataGrid();

            }
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertLokomotive upd = new InsertLokomotive();
            upd.DeleteLokomotive(txtLokomotiva.Text);
            status = false;
            txtLokomotiva.Enabled = false;
            RefreshDataGrid();
        }
    }
}
