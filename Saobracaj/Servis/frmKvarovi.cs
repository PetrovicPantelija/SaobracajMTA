using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Servis
{
    public partial class frmKvarovi : Form
    {
        bool status = false;
        public frmKvarovi()
        {
            InitializeComponent();

        }
        string niz = "";
        public static string code = "frmKvarovi";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Sifarnici.frmLogovanje.user.ToString();

        private void RefreshDataGrid()
        {
            var select = " Select Kvarovi.ID as ID, Kvarovi.Naziv as Kvar,GrupaKvarova.ID as GrupaID, GrupaKvarova.Naziv as Grupa from Kvarovi inner join GrupaKvarova on GrupaKvarova.ID = Kvarovi.GrupaKvarovaID ";
            // var select = "SELECT RkShippingItemPak.ShippingItemPakId as ID, RkShipping.ShippingNo as BarkodUtovara, RkShipping.BrojIstovara as BrojUtovara, RkShipping.DatumIstovara as DatumUtovara, RkShipping.BrojUtovara as BrojIstovara,  RkShipping.DatumUtovara as DatumIstovara , Saloni.MestoIsporuke, RkShippingItemPak.PaketName, RkShippingItemPak.LargoPakId, RkShippingItemPak.LargoNaziv, RkShippingItemPak.Paleta, RkShippingItemPak.Tezina,  RkShippingItemPak.LargoDimenzija  FROM [dbo].RkShippingItemPak inner join RkShipping on [dbo].RkShippingItemPak.ShipingIDz = RkShipping.[ShippingID] inner join SysKomitenti on RkShipping.KupacIDz = SysKomitenti.KomintentID inner join Saloni on RkShipping.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'";
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

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Kvar";
            dataGridView1.Columns[1].Width = 350;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Grupa ID";
            dataGridView1.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Grupa";
            dataGridView1.Columns[3].Width = 350;
        }

        private void frmKvarovi_Load(object sender, EventArgs e)
        {
            var select3 = " select ID, Naziv from GrupaKvarova order by ID";
            var s_connection3 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboGrupaKvarova.DataSource = ds3.Tables[0];
            cboGrupaKvarova.DisplayMember = "Naziv";
            cboGrupaKvarova.ValueMember = "ID";
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            txtSifra.Text = "";
            txtSifra.Enabled = false;
            txtOpis.Text = "";
            status = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (status == true)
            {
                InsertKvarovi ins = new InsertKvarovi();
                // ins.DeleteSaloni();
                ins.InsKVAR(txtOpis.Text, Convert.ToInt32(cboGrupaKvarova.SelectedValue));
                RefreshDataGrid();
                status = false;
            }
            else
            {
                Servis.InsertKvarovi upd = new Servis.InsertKvarovi();
                upd.UpdKvar(Convert.ToInt32(txtSifra.Text), txtOpis.Text, Convert.ToInt32(cboGrupaKvarova.SelectedValue));
                status = false;
                txtSifra.Enabled = false;
                RefreshDataGrid();
            }
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            Servis.InsertKvarovi del = new Servis.InsertKvarovi();
            del.DeleteKvarovi(Convert.ToInt32(txtSifra.Text));
            status = false;
            txtSifra.Enabled = false;
            RefreshDataGrid();
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
                        cboGrupaKvarova.SelectedValue = row.Cells[2].Value.ToString();
                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var select = " Select Kvarovi.ID as ID, Kvarovi.Naziv as Kvar,GrupaKvarova.ID as GrupaID, GrupaKvarova.Naziv as Grupa from Kvarovi inner join GrupaKvarova on GrupaKvarova.ID = Kvarovi.GrupaKvarovaID where GrupaKvarova.ID = " + Convert.ToInt32(cboGrupaKvarova.SelectedValue);
            // var select = "SELECT RkShippingItemPak.ShippingItemPakId as ID, RkShipping.ShippingNo as BarkodUtovara, RkShipping.BrojIstovara as BrojUtovara, RkShipping.DatumIstovara as DatumUtovara, RkShipping.BrojUtovara as BrojIstovara,  RkShipping.DatumUtovara as DatumIstovara , Saloni.MestoIsporuke, RkShippingItemPak.PaketName, RkShippingItemPak.LargoPakId, RkShippingItemPak.LargoNaziv, RkShippingItemPak.Paleta, RkShippingItemPak.Tezina,  RkShippingItemPak.LargoDimenzija  FROM [dbo].RkShippingItemPak inner join RkShipping on [dbo].RkShippingItemPak.ShipingIDz = RkShipping.[ShippingID] inner join SysKomitenti on RkShipping.KupacIDz = SysKomitenti.KomintentID inner join Saloni on RkShipping.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'";
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

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Kvar";
            dataGridView1.Columns[1].Width = 350;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Grupa ID";
            dataGridView1.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Grupa";
            dataGridView1.Columns[3].Width = 350;
        }
    }
}
