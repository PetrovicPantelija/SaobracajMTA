using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Testiranje.Dokumeta
{
    public partial class frmPregledTovarnihListova : Syncfusion.Windows.Forms.Office2010Form
    {
        public static string code = "frmPregledTovarnihListova";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Saobracaj.Sifarnici.frmLogovanje.user.ToString();
        string niz = "";

        public frmPregledTovarnihListova()
        {
            InitializeComponent();

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {

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

                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var select = "select TovarniList.ID as ID, Partnerji.PaNaziv as Posiljalac," +
               " k2.PaNaziv as Primalac, MestoIspostavljanja, DatumIspostavljanja, CIMBroj " +
                " from TovarniList " +
               " inner Join Partnerji on TovarniList.Posiljalac = Partnerji.PaSifra " +
               " inner Join Partnerji k2 on TovarniList.Primalac = k2.PaSifra " +
               " order by ID desc  ";
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
        }

        private void toolStripButton4_Click_1(object sender, EventArgs e)
        {
            Saobracaj.Testiranje.frmTovarniList tl = new Saobracaj.Testiranje.frmTovarniList(Convert.ToInt32(txtSifra.Text));
            tl.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Saobracaj.Testiranje.frmTovarniList tl = new Saobracaj.Testiranje.frmTovarniList();
            tl.Show();
        }
    }
}
