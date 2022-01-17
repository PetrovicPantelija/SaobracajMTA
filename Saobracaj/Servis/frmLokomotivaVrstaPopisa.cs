using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;

namespace Saobracaj.Servis
{
    public partial class frmLokomotivaVrstaPopisa : Form
    {
        bool status = false;
        public frmLokomotivaVrstaPopisa()
        {
            InitializeComponent();
        }

        private void frmLokomotivaVrstaPopisa_Load(object sender, EventArgs e)
        {
            var select3 = " Select ID, (Cast(ID as nvarchar(3)) + Naziv) as Naziv from VrstaPopisa";
            var s_connection3 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboVrstaPopisa.DataSource = ds3.Tables[0];
            cboVrstaPopisa.DisplayMember = "Naziv";
            cboVrstaPopisa.ValueMember = "ID";


            var select4 = " select SmSifra, (SmSifra + SmNaziv) as Naziv from Mesta where Lokomotiva = 1";
            var s_connection4 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection4 = new SqlConnection(s_connection4);
            var c4 = new SqlConnection(s_connection4);
            var dataAdapter4 = new SqlDataAdapter(select4, c4);

            var commandBuilder4 = new SqlCommandBuilder(dataAdapter4);
            var ds4 = new DataSet();
            dataAdapter4.Fill(ds4);
            cboLokomotiva.DataSource = ds4.Tables[0];
            cboLokomotiva.DisplayMember = "Naziv";
            cboLokomotiva.ValueMember = "SmSifra";

            RefreshDataGrid();
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            txtSifra.Text = "";
            txtSifra.Enabled = false;
            txtKolicina.Value = 0;
            txtReferetnaKolicina.Value = 0;
            status = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (status == true)
            {
                InsertLokomotiveVrstaPopisa ins = new InsertLokomotiveVrstaPopisa();
                ins.InsLokomotiveVrstePopisa(cboLokomotiva.SelectedValue.ToString(), Convert.ToInt32(cboVrstaPopisa.SelectedValue), Convert.ToInt32(txtKolicina.Text), Convert.ToInt32(txtReferetnaKolicina.Text));
                RefreshDataGrid();
                status = false;
            }
            else
            {
                InsertLokomotiveVrstaPopisa upd = new InsertLokomotiveVrstaPopisa();
                upd.UpdLokomotiveVrstePopisa(Convert.ToInt32(txtSifra.Text), cboLokomotiva.SelectedValue.ToString(), Convert.ToInt32(cboVrstaPopisa.SelectedValue), Convert.ToInt32(txtKolicina.Text), Convert.ToInt32(txtReferetnaKolicina.Text));
                status = false;
                txtSifra.Enabled = false;
                RefreshDataGrid();
            }

        }

        private void RefreshDataGrid()
        {
            var select = " select LokomotiveVrstePopisa.ID, Lokomotiva, VrstaPopisa.ID as VrstaPopisaID, VrstaPopisa.Naziv as VrstaPopisa, Kolicina, ReferentnaKolicina  from LokomotiveVrstePopisa " +
            " inner join VrstaPopisa on VrstaPopisa.ID = LokomotiveVrstePopisa.VrstaPopisaID" ;
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

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Lokomotiva";
            dataGridView1.Columns[1].Width = 150;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Vrsta popisa ID";
            dataGridView1.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Vrsta popisa";
            dataGridView1.Columns[3].Width = 150;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Količina";
            dataGridView1.Columns[4].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Referetna kolicina";
            dataGridView1.Columns[5].Width = 100;




        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertLokomotiveVrstaPopisa del = new InsertLokomotiveVrstaPopisa();
            del.DeleteLokomotiveVrstePopisa(Convert.ToInt32(txtSifra.Text));
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
                        cboLokomotiva.SelectedValue = row.Cells[1].Value.ToString();
                        cboVrstaPopisa.SelectedValue = Convert.ToInt32(row.Cells[2].Value.ToString());
                        txtKolicina.Value = Convert.ToInt32(row.Cells[4].Value.ToString());
                        txtReferetnaKolicina.Value= Convert.ToInt32(row.Cells[5].Value.ToString());
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

        }
    }
}
