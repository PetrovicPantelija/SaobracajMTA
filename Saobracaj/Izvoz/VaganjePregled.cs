using Microsoft.ReportingServices.Diagnostics.Internal;
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

namespace Saobracaj.Izvoz
{
    public partial class VaganjePregled : Syncfusion.Windows.Forms.Office2010Form
    {
        string brojKontejnera,vrstaKontejnera;
        int KontID;
        string Vozilo, Vozac;
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        public VaganjePregled()
        {
            InitializeComponent();
        }

        private void VaganjePregled_Load(object sender, EventArgs e)
        {
            FillGV();
        }
        private void FillGV()
        {
            var select = "select IzvozKonacna.ID , BrojKontejnera, VrstaKontejnera, Cirada, napomenazarobu, Vozilo, Vozac from IzvozKonacna Where Vaganje = 1 order by IzvozKonacna.ID desc";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new System.Data.DataSet();
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
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    textBox1.Text = row.Cells[0].Value.ToString();
                    brojKontejnera = row.Cells["BrojKontejnera"].Value.ToString().TrimEnd();
                    vrstaKontejnera = row.Cells["VrstaKontejnera"].Value.ToString();
                    KontID =  Convert.ToInt32(row.Cells["ID"].Value.ToString());
                    Vozilo = row.Cells["Vozilo"].Value.ToString();
                    Vozac = row.Cells["Vozac"].Value.ToString();
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FillGV();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            var select = "Select VagANJE.ID, IzvozKonacna.ID as KontID, IzvozKonacna.BrojKontejnera , Vaganje.Kamion, Vaganje.VagarskaPotvrdaBroj, Vaganje.Bruto, \r\nVaganje.Neto, Vaganje.Tara, Vaganje.DatumMerenja, Vaganje.Korisnik, IzvozKonacna.IDVaganja from Vaganje\r\ninner join IzvozKonacna on IzvozKonacna.ID = Vaganje.IzvozKonacnaID";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new System.Data.DataSet();
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

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Vaganje frm = new Vaganje(brojKontejnera, KontID, Vozilo, Vozac);
            frm.Show();
        }
    }
}
