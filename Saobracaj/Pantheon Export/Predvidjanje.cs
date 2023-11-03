using Org.BouncyCastle.Asn1.X509.Qualified;
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
    public partial class Predvidjanje : Form
    {
        bool status = false;
        private string connect = Sifarnici.frmLogovanje.connectionString;
        public Predvidjanje()
        {
            InitializeComponent();
            FillGV();
            FillCombo();
        }
        private void FillGV()
        {
            var select = "Select p.ID,PredvodjanjePoz as RB,PredvidjanjeID,p.Datum,Subjekt,PaNaziv,p.NosilacTroska,NosiociTroskova.NosilacTroska as [Nosilac troska],p.Odeljenje,Iznos,Valuta " +
                "from Predvidjanje p " +
                "inner join Partnerji on p.Subjekt=Partnerji.PaSifra " +
                "inner join NosiociTroskova on p.NosilacTroska=NosiociTroskova.ID order by p.ID desc";
            SqlConnection conn = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

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
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[6].Visible = false;

            dataGridView1.Columns["ID"].Width = 50;
            dataGridView1.Columns["RB"].Width = 50;
            dataGridView1.Columns["PredvidjanjeID"].Width = 100;
            //dataGridView1.Columns["PredividjanjeID"].HeaderText = "PredvidjanjeID";
            dataGridView1.Columns["Datum"].Width = 150;
            dataGridView1.Columns["PaNaziv"].Width = 330;
            dataGridView1.Columns["Nosilac troska"].Width = 80;
            dataGridView1.Columns["Odeljenje"].Width = 150;
            dataGridView1.Columns["Iznos"].Width = 150;
            dataGridView1.Columns["Valuta"].Width = 50;


        }
        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connect);

            var valuta = "Select VaSifra,VaNaziv From Valute";
            var valutaDa = new SqlDataAdapter(valuta, conn);
            var valutaDS = new DataSet();
            valutaDa.Fill(valutaDS);
            cboValuta.DataSource = valutaDS.Tables[0];
            cboValuta.DisplayMember = "VaNaziv";
            cboValuta.ValueMember = "VaSifra";

            var query = "Select PaSifra,PaNaziv from Partnerji order by PaSifra";
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboSubjekt.DataSource = ds.Tables[0];
            cboSubjekt.DisplayMember = "PaNaziv";
            cboSubjekt.ValueMember = "PaSifra";

            var nosilac = "Select ID,NosilacTroska from NosiociTroskova order by ID desc";
            var nosilacDa = new SqlDataAdapter(nosilac, conn);
            var nosilacDS = new DataSet();
            nosilacDa.Fill(nosilacDS);
            cboNosilacTroska.DataSource = nosilacDS.Tables[0];
            cboNosilacTroska.DisplayMember = "NosilacTroska";
            cboNosilacTroska.ValueMember = "ID";

        }
        int RB;
        string predvidjanjeID;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach(DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        RB = Convert.ToInt32(row.Cells[1].Value);
                        predvidjanjeID = row.Cells[2].Value.ToString();
                        txtID.Text = row.Cells["ID"].Value.ToString();
                        dateTimePicker1.Value = Convert.ToDateTime(row.Cells["Datum"].Value);
                        cboSubjekt.SelectedValue = Convert.ToInt32(row.Cells[4].Value);
                        cboNosilacTroska.SelectedValue = Convert.ToInt32(row.Cells[6].Value);
                        txtOdeljenje.Text = row.Cells["Odeljenje"].Value.ToString().TrimEnd();
                        txtIznos.Text = row.Cells["Iznos"].Value.ToString();
                    }
                }
            }
            catch { }
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true; 
            string query = "Select (Max(ID) + 1) as id from Predvidjanje";
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr["id"].GetType() == typeof(DBNull))
                { ID = 1; }
                else
                {
                    ID = Convert.ToInt32(dr["id"].ToString());
                }
            }
            conn.Close();
        }
        int ID;
        private void tsSave_Click(object sender, EventArgs e)
        {
            int poz = dataGridView1.RowCount+1;
            
            string g = DateTime.Now.ToString("yy");
            string predvidjanje = "SUP-" + g + "-" + ID.ToString();
            InsertPatheonExport ins = new InsertPatheonExport();
            if (status == true)
            {
                ins.InsPredvidjanje(predvidjanje, poz, Convert.ToDateTime(dateTimePicker1.Value), Convert.ToInt32(cboSubjekt.SelectedValue), Convert.ToInt32(cboNosilacTroska.SelectedValue), txtOdeljenje.Text.ToString(), Convert.ToDecimal(txtIznos.Text.ToString()), cboValuta.SelectedValue.ToString(), 0);
            }
            else
            {
                ins.UpdPredvidjanje(Convert.ToInt32(txtID.Text), predvidjanjeID, RB, Convert.ToDateTime(dateTimePicker1.Value), Convert.ToInt32(cboSubjekt.SelectedValue), Convert.ToInt32(cboNosilacTroska.SelectedValue), txtOdeljenje.Text.ToString(), Convert.ToDecimal(txtIznos.Text.ToString()), cboValuta.SelectedValue.ToString(), 1);
            }
            FillGV();
            status = false;
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertPatheonExport ins = new InsertPatheonExport();
            ins.DelPredvidjanje(Convert.ToInt32(txtID.Text));
            FillGV();
        }
    }
}
