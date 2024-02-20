using Saobracaj.Izvoz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Sifarnici
{
    public partial class KrajnjeDestinacije : Form
    {
        string connect = frmLogovanje.connectionString;
        bool status = false;

        public KrajnjeDestinacije()
        {
            InitializeComponent();
        }
        private void FillGV()
        {
            SqlConnection conn = new SqlConnection(connect);
            var query = "Select RTrim(DrSifra) as ID,RTrim(DrNaziv) as Drzava,DrKoda as Oznaka,DrNacVal as ValutaDrzave From Drzave order by DrSifra";
            var da = new SqlDataAdapter(query, conn);
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

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 60;
            dataGridView1.Columns[3].Width = 60;


            var query2 = "Select ID,Naziv,SifDr,RTrim(DrNaziv) as Drzava from KrajnjaDestinacija inner join Drzave on KrajnjaDestinacija.SifDr=Drzave.DrSifra order by ID desc";
            var da2 = new SqlDataAdapter( query2, conn);
            var ds2=new DataSet();
            da2.Fill(ds2);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource= ds2.Tables[0];
            dataGridView2.BorderStyle = BorderStyle.None;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView2.BackgroundColor = Color.LightBlue;

            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dataGridView2.Columns[0].Width = 50;
            dataGridView2.Columns[1].Width = 120;
            dataGridView2.Columns[2].Visible = false;
            dataGridView2.Columns[3].Width = 120;
        }

        private void KrajnjeDestinacije_Load(object sender, EventArgs e)
        {
            FillGV();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if(row.Selected)
                {
                    txtIdDrzave.Text = row.Cells[0].Value.ToString().TrimEnd();
                    txtDrzava.Text = row.Cells[1].Value.ToString();
                }
            }
        }
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Selected)
                {
                    txtID.Text = row.Cells[0].Value.ToString();
                    txtIdDrzave.Text = row.Cells[2].Value.ToString();
                    txtNaziv.Text = row.Cells[1].Value.ToString();
                    txtDrzava.Text = row.Cells[3].Value.ToString();
                }
            }
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            InsertKrajnjaDestinacija ins = new InsertKrajnjaDestinacija();
            if (status == true)
            {
                ins.InsKrajnjaDestinacija(txtNaziv.Text.ToString().TrimEnd(), Convert.ToInt32(txtIdDrzave.Text));
            }
            else
            {
                ins.UpdKrajnjaDestinacija(Convert.ToInt32(txtID.Text), txtNaziv.Text.ToString().TrimEnd(), Convert.ToInt32(txtIdDrzave.Text));
            }
            status = false;
            FillGV();

        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertKrajnjaDestinacija ins = new InsertKrajnjaDestinacija();

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Selected)
                {
                    ins.DelKrajnjaDestinacija(Convert.ToInt32(row.Cells[0].Value.ToString()));
                }
            }
        }
    }
}
