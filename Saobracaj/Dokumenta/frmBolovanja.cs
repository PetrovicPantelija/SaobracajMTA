using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Dokumenta
{
    public partial class frmBolovanja : Form
    {
        Boolean status = false;
        public frmBolovanja()
        {
            InitializeComponent();
        }


        private void frmBolovanja_Load(object sender, EventArgs e)
        {
            var select3 = " select DeSifra as ID, (RTrim(DeIme) + ' ' + Rtrim(DePriimek)) as Opis from Delavci where DeSifStat <> 'P' order by opis";
            var s_connection3 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboZaposleni.DataSource = ds3.Tables[0];
            cboZaposleni.DisplayMember = "Opis";
            cboZaposleni.ValueMember = "ID";
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            {
                status = true;
                txtSifra.Text = "";
                txtSifra.Enabled = false;
                dtpVremeOd.Value = DateTime.Now;
                dtpVremeDo.Value = DateTime.Now;
                txtNapomena.Text = "";
            }
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (status == true)
            {
                insertBolovanja ins = new insertBolovanja();
                ins.InsBolovanja(dtpVremeOd.Value, dtpVremeDo.Value, Convert.ToInt32(txtUkupno.Text), Convert.ToInt32(cboZaposleni.SelectedValue), txtNapomena.Text, Convert.ToInt32(txtTipBolovanja.Text));
                status = false;
                RefreshDataGrid1();

            }
            else
            {
                insertBolovanja upd = new insertBolovanja();
                upd.UpdBolovanje(Convert.ToInt32(txtSifra.Text), dtpVremeOd.Value, dtpVremeDo.Value, Convert.ToInt32(txtUkupno.Text), Convert.ToInt32(cboZaposleni.SelectedValue), txtNapomena.Text, Convert.ToInt32(txtTipBolovanja.Text));
                RefreshDataGrid1();
            }
        }

        private void RefreshDataGrid1()
        {
            var select = " Select Bolovanje.ID,  DatumOd, DatumDo, Ukupno, Napomena,  (Rtrim(Delavci.DeIme) + ' ' + Rtrim(Delavci.DePriimek)) as Radnik " +
 " from Bolovanje inner join Delavci on " +
 " Bolovanje.ZaposleniID = Delavci.DeSifra  where Bolovanje.ZaposleniID = " + Convert.ToInt32(cboZaposleni.SelectedValue) + " order by id desc";

            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            dataGridView2.BorderStyle = BorderStyle.None;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView2.BackgroundColor = Color.White;

            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

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

        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid1();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var select = " Select Bolovanje.ID,  DatumOd, DatumDo, Ukupno, Napomena,  (Rtrim(Delavci.DeIme) + ' ' + Rtrim(Delavci.DePriimek)) as Radnik " +
 " from Bolovanje inner join Delavci on " +
 " Bolovanje.ZaposleniID = Delavci.DeSifra order by id desc";


            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            dataGridView2.BorderStyle = BorderStyle.None;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView2.BackgroundColor = Color.White;

            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

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
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            insertBolovanja del = new insertBolovanja();
            del.DeleteBolovanje(Convert.ToInt32(txtSifra.Text));
            RefreshDataGrid1();
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {

                        txtSifra.Text = row.Cells[0].Value.ToString();
                        RefreshDataGrid1();
                        // txtOpis.Text = row.Cells[1].Value.ToString();
                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }
    }
}

