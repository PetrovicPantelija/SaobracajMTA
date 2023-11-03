using Microsoft.Office.Interop.Excel;
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

namespace Saobracaj.Pantheon_Export
{
    public partial class NosiociTroskova : Form
    {
        bool status = false;
        private string connect = Sifarnici.frmLogovanje.connectionString;

        public NosiociTroskova()
        {
            InitializeComponent();
            FillCombo();
            FillGV();
        }
        private void FillGV()
        {
            var select = "Select ID,NosilacTroska,NazivNosiocaTroska,Grupa,PaNaziv,Odeljenje,Kupac From NosiociTroskova inner join Partnerji on NosiociTroskova.Kupac=Partnerji.PaSifra order by ID desc";
            SqlConnection conn = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 220;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].Width = 300;
            dataGridView1.Columns[5].Width = 150;


            dataGridView1.BorderStyle = BorderStyle.FixedSingle;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;



            dataGridView1.Columns[6].Visible = false;
        }
        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connect);
            var query = "Select PaSifra,PaNaziv from Partnerji order by PaSifra";
            var da=new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboKupac.DataSource = ds.Tables[0];
            cboKupac.DisplayMember = "PaNaziv";
            cboKupac.ValueMember = "PaSifra";
           
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
        }
        
        private void tsSave_Click(object sender, EventArgs e)
        {
            InsertPatheonExport ins = new InsertPatheonExport();
            if (status == true)
            {
                ins.InsNosiociTroskova(txtNosilacTroska.Text.ToString().TrimEnd(),txtNazivNosioca.Text.ToString().TrimEnd(),txtGrupa.Text.ToString().TrimEnd(),Convert.ToInt32(cboKupac.SelectedValue),txtOdeljenje.Text.ToString().TrimEnd());
            }
            else
            {
                ins.UpdNosiociTroskova(Convert.ToInt32(txtID.Text), txtNosilacTroska.Text.ToString().TrimEnd(), txtNazivNosioca.Text.ToString().TrimEnd(), txtGrupa.Text.ToString().TrimEnd(), Convert.ToInt32(cboKupac.SelectedValue), txtOdeljenje.Text.ToString().TrimEnd());
            }
            FillGV();
            status = false;
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertPatheonExport ins = new InsertPatheonExport();
            ins.DelNosiociTroskova(Convert.ToInt32(txtID.Text));
            FillGV();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach(DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txtID.Text = row.Cells[0].Value.ToString().TrimEnd();
                        txtNosilacTroska.Text = row.Cells[1].Value.ToString().TrimEnd();
                        txtNazivNosioca.Text = row.Cells[2].Value.ToString().TrimEnd();
                        txtGrupa.Text = row.Cells[3].Value.ToString().TrimEnd();
                        cboKupac.SelectedValue = Convert.ToInt32(row.Cells[6].Value);
                        txtOdeljenje.Text = row.Cells[5].Value.ToString().TrimEnd();
                    }
                }
            }
            catch { }
        }
    }
}
