using Saobracaj.Sifarnici;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Administracija
{
    public partial class frmSistematizacijaPovezivanje : Form
    {
        private bool status = false;

        public frmSistematizacijaPovezivanje()
        {
            InitializeComponent();

        }

        private string niz = "";
        public static string code = "frmSistematizacijaPovezivanje";
        public bool Pravo;
        private int idGrupe;
        private int idForme;
        private bool insert;
        private bool update;
        private bool delete;
        private string Kor = Sifarnici.frmLogovanje.user.ToString();

        private void frmSistematizacijaPovezivanje_Load(object sender, EventArgs e)
        {
            var select = " Select ID, (cast(ID as nvarchar(5)) + ' ' + Naziv) as Naziv from GrupaDelovnihMesta";
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboSistematizacijaDincic.DataSource = ds.Tables[0];
            cboSistematizacijaDincic.DisplayMember = "Naziv";
            cboSistematizacijaDincic.ValueMember = "ID";

            //select DmSifra, (cast(DmSifra as nvarchar(5)) + ' - ' + DmNaziv) as Naziv from DelovnaMesta

            var select2 = " select DmSifra, (cast(DmSifra as nvarchar(5)) + ' - ' + DmNaziv) as Naziv from DelovnaMesta";
            var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboSistematizacija.DataSource = ds2.Tables[0];
            cboSistematizacija.DisplayMember = "Naziv";
            cboSistematizacija.ValueMember = "DmSifra";

            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            var select = " select DmId,DelovnaMesta.DmNaziv as Largo,GrupaDmId, GrupaDelovnihMesta.Naziv as Dincic from GrupisanjeDelovnihMesta " +
                           " inner join GrupaDelovnihMesta on GrupaDelovnihMesta.Id = GrupisanjeDelovnihMesta.GrupaDmId " +
                           " inner join DelovnaMesta on GrupisanjeDelovnihMesta.DmId = DelovnaMesta.DMSifra ";
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
            dataGridView1.Columns[0].HeaderText = "Largo ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Largo naziv";
            dataGridView1.Columns[1].Width = 350;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Dincic ID";
            dataGridView1.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Dincic Grupa ID";
            dataGridView1.Columns[3].Width = 350;
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (status == true)
            {
                InsertSistematizacija ins = new InsertSistematizacija();
                // ins.DeleteSaloni();
                ins.InsertSistematizacijaPovezivanje(Convert.ToInt32(cboSistematizacija.SelectedValue), Convert.ToInt32(cboSistematizacijaDincic.SelectedValue));
                RefreshDataGrid();
                status = false;
            }
            else
            {
                MessageBox.Show("Promena nije moguća na taj način potrebno je prvo da obrišete pa unesete vezu");
            }
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertSistematizacija ins = new InsertSistematizacija();
            ins.DeleteSistematizacijaPovezivanje(Convert.ToInt32(cboSistematizacija.SelectedValue), Convert.ToInt32(cboSistematizacijaDincic.SelectedValue));
            RefreshDataGrid();
            status = false;
        }
    }
}