using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Dokumenta
{
    public partial class frmRadniNalogIzNajave : Form
    {
        public frmRadniNalogIzNajave()
        {
            InitializeComponent();
        }

        public frmRadniNalogIzNajave(string NajavaID)
        {
            InitializeComponent();
            VratiPosaoOznaku(NajavaID);
            txtSifra.Text = NajavaID.ToString();
            int RN = 0;
            RN = VratiRN(NajavaID);
            /*-
             if (RN == 0)
             {

                 KopiranjeRNIzPOsla();


             }
            */
            // txtSifra.Text = RN.ToString();
            // VratiPodatke(RN.ToString());
        }



        int VratiRN(string NajavaID)
        {

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select TOP 1 IDRadnogNaloga from RadniNalogVezaNajave where IDNajave =" + NajavaID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                // txtOpis.Text = dr["BrojNajave"].ToString();
                // cboNajava.SelectedValue = Convert.ToInt32(dr["Najava"].ToString());
                return Convert.ToInt32(dr["IDRadnogNaloga"].ToString());
            }

            con.Close();
            return 0;
        }

        private void VratiPosaoOznaku(string NajavaJID)
        {
            txtSifra.Text = NajavaJID;
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select TOP 1 Oznaka from Najava where ID =" + Convert.ToInt32(NajavaJID), con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                txtOznaka.Text = dr["Oznaka"].ToString();
            }

            con.Close();



        }

        private void RefreshDataGrid1()
        {
            var select = " select RadniNalogVezaNajave.*, Najava.Oznaka  from RadniNalogVezaNajave " +
            " INNER join Najava on RadniNalogVezaNajave.IDNajave = Najava.ID ";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds.Tables[0];

            dataGridView3.BorderStyle = BorderStyle.None;
            dataGridView3.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView3.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView3.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView3.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView3.BackgroundColor = Color.White;

            dataGridView3.EnableHeadersVisualStyles = false;
            dataGridView3.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView3.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;


            /*
            
            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 40;



            DataGridViewColumn column3 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "Vreme Od";
            dataGridView2.Columns[1].Width = 100;

            DataGridViewColumn column4 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "Vreme Do";
            dataGridView2.Columns[2].Width = 100;

            DataGridViewColumn column5 = dataGridView2.Columns[3];
            dataGridView2.Columns[3].HeaderText = "Ukupno";
            dataGridView2.Columns[3].Width = 50;

            DataGridViewColumn column6 = dataGridView2.Columns[4];
            dataGridView2.Columns[4].HeaderText = "Napomena";
            dataGridView2.Columns[4].Width = 120;

            DataGridViewColumn column7 = dataGridView2.Columns[5];
            dataGridView2.Columns[5].HeaderText = "Zaposleni";
            dataGridView2.Columns[5].Width = 120;

            */

        }

        private void frmRadniNalogIzNajave_Load(object sender, EventArgs e)
        {
            RefreshDataGrid1();
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    if (row.Selected)
                    {
                        txtRN.Text = row.Cells[1].Value.ToString();


                        // txtOpis.Text = row.Cells[1].Value.ToString();
                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void VratiMAXRN()
        {

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select MAX(ID) as MAX_RN from RadniNalog ", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                txtRNNovi.Text = dr["MAX_RN"].ToString();
            }

            con.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            InsertRadniNalog ins = new InsertRadniNalog();
            ins.InsRadniNalogIzNajaveKopiranjem(Convert.ToInt32(txtRN.Text), Convert.ToInt32(txtSifra.Text), txtOznaka.Text);
            VratiMAXRN();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmRadniNalog rn = new frmRadniNalog(txtSifra.Text, 1, 1);
            rn.Show();
        }
    }
}
