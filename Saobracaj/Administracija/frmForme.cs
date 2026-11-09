using Saobracaj.Sifarnici;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Administracija
{
    public partial class frmForme : Form
    {

        public string connect = frmLogovanje.connectionString;
        private bool status = false;


        public frmForme()
        {
            InitializeComponent();
            txt_Sifra.Enabled = false;

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Administracija.frmFormePrava formePrava = new frmFormePrava();
            formePrava.Show();
        }

        private void frmForme_Load(object sender, EventArgs e)
        {
            RefreshDG();
            //Panta2
        }

        private void RefreshDG()
        {
            var query = "select * from Forme";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
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

            dataGridView1.Columns[0].HeaderText = "ID Forme";
            dataGridView1.Columns[0].Width = 55;
            dataGridView1.Columns[1].HeaderText = "Naziv Forme";
            dataGridView1.Columns[1].Width = 180;
            dataGridView1.Columns[2].HeaderText = "Code Forme";
            dataGridView1.Columns[2].Width = 150;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txt_Sifra.Text = row.Cells[0].Value.ToString();
                        txt_NazivForme.Text = row.Cells[1].Value.ToString();
                        txt_Code.Text = row.Cells[2].Value.ToString();
                    }
                }
            }
            catch
            {
            }
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            txt_Sifra.Text = "";
            status = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            InsertForme forme = new InsertForme();
            if (status == true)
            {
                forme.InsForme(txt_NazivForme.Text.TrimEnd(), txt_Code.Text.TrimEnd());
                RefreshDG();
                status = false;
            }
            else
            {
                forme.UpdateForme(Convert.ToInt32(txt_Sifra.Text.TrimEnd().ToString()), txt_NazivForme.Text.TrimEnd(), txt_Code.Text.TrimEnd());
                RefreshDG();
            }
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertForme forme = new InsertForme();
            forme.DeleteForme(Convert.ToInt32(txt_Sifra.Text));
            RefreshDG();
        }
    }
}