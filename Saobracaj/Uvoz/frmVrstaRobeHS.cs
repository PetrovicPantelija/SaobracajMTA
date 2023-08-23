using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Uvoz
{
    public partial class frmVrstaRobeHS : Form
    {
        bool status = false;
        string connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
       
        public frmVrstaRobeHS()
        {
            InitializeComponent();
            FillGV();
        }
        private void FillGV()
        {
            var select = "Select VrstaRobeHS.ID, VrstaRobeHS.Naziv, VrstaRobeHS.HSKOd, ADRID, Izvozni " +
" , VrstaRobeADR.NAziv, VrstaRobeADR.Klasa, VrstaRobeADR.Grupa from VrstaRobeHS " +
"  left join VrstaRobeADR on VrstaRobeHS.ADRID = VrstaRobeADR.ID";
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

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "HS kod";
            dataGridView1.Columns[2].Width = 70;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "ADR ID";
            dataGridView1.Columns[3].Width = 70;


            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Izvozni";
            dataGridView1.Columns[4].Width = 70;
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            tsNew.Enabled = false;
            status = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {

            int Izvozni = 0;            
            InsertVrstaRobeHS ins = new InsertVrstaRobeHS();
            if (chkIzvozni.Checked == true)
            {
                Izvozni = 1;
            }
            if (status == true)
            {
                ins.InsVrstaRobeHS(txtNaziv.Text.ToString().TrimEnd(),txtHSCode.Text, Convert.ToInt32(txtADR.SelectedValue), Izvozni) ;
            }
            else
            {
                ins.UpdVrstaRobeHS(Convert.ToInt32(txtID.Text.ToString()),txtNaziv.Text.ToString().TrimEnd(), txtHSCode.Text, Convert.ToInt32(txtADR.SelectedValue), Izvozni);
            }
            FillGV();
            tsNew.Enabled = true;
            status = false;
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertVrstaRobeHS ins = new InsertVrstaRobeHS();
            ins.DelVrstaRobeHS(Convert.ToInt32(txtID.Text.ToString()));
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
                        txtHSCode.Text = row.Cells[2].Value.ToString();
                        if (row.Cells[4].Value.ToString() != "")
                        { txtADR.SelectedValue = Convert.ToInt32(row.Cells[4].Value.ToString()); }
                        else
                        {
                            txtADR.SelectedValue = 0;
                        }
                        if (row.Cells[4].Value.ToString() == "1")
                        {
                            chkIzvozni.Checked = true;
                        }
                        else
                        {
                            chkIzvozni.Checked = false;
                        }

                    }
                }
            }
            catch { }
        }

        private void frmVrstaRobeHS_Load(object sender, EventArgs e)
        {
            var conn = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;


            var adr = "Select ID, (Naziv + ' - ' + UNKod) as Naziv From VrstaRobeADR order by (UNKod + ' ' + Naziv)";
            var adrSAD = new SqlDataAdapter(adr, conn);
            var adrSDS = new DataSet();
            adrSAD.Fill(adrSDS);
            txtADR.DataSource = adrSDS.Tables[0];
            txtADR.DisplayMember = "Naziv";
            txtADR.ValueMember = "ID";
        }
    }
}
