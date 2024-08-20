using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
//using MetroFramework.Forms;

namespace Saobracaj.Sifarnici
{
    public partial class frmStanice : Syncfusion.Windows.Forms.Office2010Form
    {
       
        string Kor = Sifarnici.frmLogovanje.user.ToString();

        bool status = false;
        public frmStanice()
        {
            InitializeComponent();

        }
        private void tsSave_Click(object sender, EventArgs e)
        {
            int pomGranicna = 0;
            if (chkGranicna.Checked == false)
            {
                pomGranicna = 0;
            }
            else
            {
                pomGranicna = 1;
            }

            if (status == true)
            {
                InsertStanice ins = new InsertStanice();
                // ins.DeleteSaloni();
                ins.InsStanice(txtOpis.Text, pomGranicna, txtKod.Text, cboDrzava.Text, Convert.ToDouble(txtLongitude.Value), Convert.ToDouble(txtLatitude.Value), txtPrelaz.Text);
                //MessageBox.Show("Uspešno uneta stanica");
                RefreshDataGrid();
                status = false;
            }
            else
            {
                InsertStanice upd = new InsertStanice();
                upd.UpdStanice(Convert.ToInt32(txtSifra.Text), txtOpis.Text, pomGranicna, txtKod.Text, cboDrzava.Text, Convert.ToDouble(txtLongitude.Value), Convert.ToDouble(txtLatitude.Value), txtPrelaz.Text);
                status = false;
                txtSifra.Enabled = false;
               // RefreshDataGrid();
            }
        }

        private void frmStanice_Load(object sender, EventArgs e)
        {
            FillCombo();
        }
        private void FillCombo()
        {
            var select = " Select ID, opis, (case when granicna = 1 then 'true' else 'false' end) as granicna, Kod,Drzava, Longitude, Latitude, Prelaz from Stanice";
            // var select = "SELECT RkShippingItemPak.ShippingItemPakId as ID, RkShipping.ShippingNo as BarkodUtovara, RkShipping.BrojIstovara as BrojUtovara, RkShipping.DatumIstovara as DatumUtovara, RkShipping.BrojUtovara as BrojIstovara,  RkShipping.DatumUtovara as DatumIstovara , Saloni.MestoIsporuke, RkShippingItemPak.PaketName, RkShippingItemPak.LargoPakId, RkShippingItemPak.LargoNaziv, RkShippingItemPak.Paleta, RkShippingItemPak.Tezina,  RkShippingItemPak.LargoDimenzija  FROM [dbo].RkShippingItemPak inner join RkShipping on [dbo].RkShippingItemPak.ShipingIDz = RkShipping.[ShippingID] inner join SysKomitenti on RkShipping.KupacIDz = SysKomitenti.KomintentID inner join Saloni on RkShipping.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'";
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
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

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "Šifra";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Opis";
            dataGridView1.Columns[1].Width = 350;

            DataGridViewCheckBoxColumn column3 = new DataGridViewCheckBoxColumn();
            dataGridView1.Columns[2].HeaderText = "Granična";
            column3.CellTemplate = new DataGridViewCheckBoxCell();
            column3.CellTemplate.Style.BackColor = Color.Beige;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Kod";
            dataGridView1.Columns[3].Width = 50;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Država";
            dataGridView1.Columns[4].Width = 50;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Longitude";
            dataGridView1.Columns[5].Width = 80;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Latitude";
            dataGridView1.Columns[6].Width = 80;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Prelaz";
            dataGridView1.Columns[7].Width = 80;


            var select2 = " Select Distinct DrSifra, RTrim(DrNaziv) as Naziv From Drzave";
            var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboDrzava.DataSource = ds2.Tables[0];
            cboDrzava.DisplayMember = "DrSifra";
            cboDrzava.ValueMember = "DrSifra";

        }
        private void RefreshDataGrid()
        {
            var select = " Select * from Stanice";
            // var select = "SELECT RkShippingItemPak.ShippingItemPakId as ID, RkShipping.ShippingNo as BarkodUtovara, RkShipping.BrojIstovara as BrojUtovara, RkShipping.DatumIstovara as DatumUtovara, RkShipping.BrojUtovara as BrojIstovara,  RkShipping.DatumUtovara as DatumIstovara , Saloni.MestoIsporuke, RkShippingItemPak.PaketName, RkShippingItemPak.LargoPakId, RkShippingItemPak.LargoNaziv, RkShippingItemPak.Paleta, RkShippingItemPak.Tezina,  RkShippingItemPak.LargoDimenzija  FROM [dbo].RkShippingItemPak inner join RkShipping on [dbo].RkShippingItemPak.ShipingIDz = RkShipping.[ShippingID] inner join SysKomitenti on RkShipping.KupacIDz = SysKomitenti.KomintentID inner join Saloni on RkShipping.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'";
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "Šifra";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Opis";
            dataGridView1.Columns[1].Width = 350;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Granična";
            dataGridView1.Columns[2].Width = 50;
            //  column3.CellTemplate = new DataGridViewCheckBoxCell();
            // column3.CellTemplate.Style.BackColor = Color.Beige;
            // dataGridView1.DefaultCellStyle.
            // DataGridViewCell cell = new DataGridViewCheckBoxCell();
            //column3.CellTemplate = cell;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Kod";
            dataGridView1.Columns[3].Width = 50;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Država";
            dataGridView1.Columns[4].Width = 50;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Longitude";
            dataGridView1.Columns[5].Width = 80;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Latitude";
            dataGridView1.Columns[6].Width = 80;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Prelaz";
            dataGridView1.Columns[7].Width = 80;
        }

        private void dataGridView1_Click(object sender, EventArgs e)
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
                        txtOpis.Text = row.Cells[1].Value.ToString();
                        txtKod.Text = row.Cells[3].Value.ToString();
                        cboDrzava.Text = row.Cells[4].Value.ToString();
                        txtLongitude.Value = Convert.ToDecimal(row.Cells[5].Value);
                        txtLatitude.Value = Convert.ToDecimal(row.Cells[6].Value);
                        txtPrelaz.Text = row.Cells[7].Value.ToString();
                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            txtSifra.Text = "";
            txtSifra.Enabled = false;
            txtOpis.Text = "";
            txtKod.Text = "";
            cboDrzava.Text = "";
            txtLongitude.Text = "";
            txtLatitude.Text = "";

            status = true;
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertStanice del = new InsertStanice();
            del.DeleteStanice(Convert.ToInt32(txtSifra.Text));
            status = false;
            txtSifra.Enabled = false;
            RefreshDataGrid();
        }

        private void tsPrvi_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtDrzava_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
