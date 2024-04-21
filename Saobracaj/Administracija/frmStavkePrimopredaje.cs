using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Administracija
{
    public partial class frmStavkePrimopredaje : Form
    {
        private bool status = false;
        private string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

        public frmStavkePrimopredaje()
        {
            InitializeComponent();
        }

        private void frmStavkePrimopredaje_Load(object sender, EventArgs e)
        {
            FillGV();
        }

        private void FillGV()
        {
            var select = "Select * From SifStavkePrimopredaje order by ID desc";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
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

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Naziv";
            dataGridView1.Columns[1].Width = 250;
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            Administracija.InsertStavkePrimopredaje ins = new Administracija.InsertStavkePrimopredaje();

            if (status == true)
            {
                ins.InsStavkePrimopredaje(txtNaziv.Text.ToString().TrimEnd());
                status = false;
            }
            else
            {
                ins.UpdateStavkePrimopredaje(Convert.ToInt32(txtID.Text), txtNaziv.Text.ToString().TrimEnd());
            }
            MessageBox.Show("Uspešno sačuvan zapis");
            FillGV();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            Administracija.InsertStavkePrimopredaje del = new Administracija.InsertStavkePrimopredaje();

            del.DeleteStavkePrimopredaje(Convert.ToInt32(txtID.Text));
            FillGV();
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
                        txtNaziv.Text = row.Cells[1].Value.ToString();
                    }
                }
            }
            catch { }
        }
    }
}