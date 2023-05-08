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

namespace Saobracaj.Dokumenta
{
    public partial class frmRutaTrase : Form
    {
        bool status = true;
        int PrviUlazak = 0;

        public frmRutaTrase()
        {
            InitializeComponent();
        }

        public frmRutaTrase(string SifraTrase, string OznakaTrase)
        {
            InitializeComponent();
            txtSifra.Text = SifraTrase;
            txtVoz.Text = OznakaTrase;
            RefreshDataGrid();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void frmRutaTrase_Load(object sender, EventArgs e)
        {
            var select2 = " Select Distinct ID, RTrim(Opis) as Stanica From Stanice";
            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cmbStanica.DataSource = ds2.Tables[0];
            cmbStanica.DisplayMember = "Stanica";
            cmbStanica.ValueMember = "ID";

        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtID.Enabled = false;
            txtID.Text = "";
            status = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (status == true)
            {
                InsertRutaTrase ins = new InsertRutaTrase();
                ins.InsRutaTrase(Convert.ToInt32(txtSifra.Text), Convert.ToInt32(txtRB.Text), Convert.ToInt32(cmbStanica.SelectedValue));
                RefreshDataGrid();
                status = false;
            }
            else
            {

                InsertRutaTrase upd = new InsertRutaTrase();
                upd.UpdRutaTrase(Convert.ToInt32(txtID.Text), Convert.ToInt32(txtSifra.Text), Convert.ToInt32(txtRB.Text), Convert.ToInt32(cmbStanica.SelectedValue));
                status = false;
                txtSifra.Enabled = false;
                RefreshDataGrid();

            }
        }
        private void RefreshDataGrid()
        {

            //SELECT     TrasaStanice.IDTrase, TrasaStanice.RB, stanice.Opis, stanice.Kod FROM TrasaStanice INNER JOIN  PrugaStavke ON TrasaStanice.IDStanice = PrugaStavke.ID INNER JOIN stanice ON PrugaStavke.StanicaOd = stanice.ID
            //  var select = " SELECT     TrasaStanice.IDTrase, TrasaStanice.RB, stanice.Opis, stanice.Kod FROM TrasaStanice INNER JOIN  PrugaStavke ON TrasaStanice.IDStanice = PrugaStavke.ID INNER JOIN stanice ON PrugaStavke.StanicaOd = stanice.ID where IdTrase = " + txtSifra.Text;
            var select = "SELECT     RuteTrase.ID, RuteTrase.IDTrase,  Trase.OpisRelacije, Trase.Voz, RuteTrase.RB, RuteTrase.StanicaID, stanice.Opis " +
                      " FROM RuteTrase INNER JOIN " +
                      " Trase ON RuteTrase.IDTrase = Trase.ID INNER JOIN " +
                      " stanice ON RuteTrase.StanicaID = stanice.ID  where IdTrase = " + txtSifra.Text +
                      " Order by RB ";




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
            dataGridView1.Columns[0].HeaderText = "ID ";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "ID TRASE";
            dataGridView1.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Relacija";
            dataGridView1.Columns[2].Width = 240;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Voz";
            dataGridView1.Columns[3].Width = 240;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "RB";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Stanica ID";
            dataGridView1.Columns[5].Width = 40;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Stanica";
            dataGridView1.Columns[6].Width = 240;

        }


        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertRutaTrase upd = new InsertRutaTrase();
            upd.DeleteRutaTrase(Convert.ToInt32(txtID.Text));
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
                        txtID.Text = row.Cells[0].Value.ToString();
                        txtSifra.Text = row.Cells[1].Value.ToString();
                       // cboPruga.SelectedValue = Convert.ToInt32(row.Cells[1].Value.ToString());
                        txtVoz.Text = row.Cells[3].Value.ToString();
                        txtRB.Text = row.Cells[5].Value.ToString();
                        cmbStanica.SelectedValue = Convert.ToInt32(row.Cells[6].Value.ToString());
                       
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
