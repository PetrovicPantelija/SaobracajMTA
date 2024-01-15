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
using System.Drawing;
//
namespace Saobracaj.RadniNalozi
{
    public partial class RN11PreglediPostavkaKontejnera : Form
    {
        private string connect = Sifarnici.frmLogovanje.connectionString;
        private bool status = false;
        public RN11PreglediPostavkaKontejnera()
        {
            InitializeComponent();
            FillGV();
            FillCombo();
        }
        private void FillGV()
        {
            var select = "Select * from RNPregledIpostavkaKontejnera order by ID desc";
            SqlConnection conn = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, conn);
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
        }

        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connect);

            var TipKontenjera = "Select ID,SkNaziv from TipKontenjera order by ID";
            var daTK = new SqlDataAdapter(TipKontenjera, conn);
            var daDS = new DataSet();
            daTK.Fill(daDS);
            cboVrstaKontejnera.DataSource = daDS.Tables[0];
            cboVrstaKontejnera.DisplayMember = "SkNaziv";
            cboVrstaKontejnera.ValueMember = "ID";

            txtNalogIzdao.Text = Sifarnici.frmLogovanje.user.ToString().TrimEnd();
            //usluge->Manipulacije

            var partner7 = "Select PaSifra,PaNaziv From Partnerji where Brodar = 1 order by PaSifra";
            var partAD7 = new SqlDataAdapter(partner7, conn);
            var partDS7 = new DataSet();
            partAD7.Fill(partDS7);
            cboBrodar.DataSource = partDS7.Tables[0];
            cboBrodar.DisplayMember = "PaNaziv";
            cboBrodar.ValueMember = "PaSifra";

            var roba = "Select ID,Naziv From NHM order by ID";
            var daRoba = new SqlDataAdapter(roba, conn);
            var dsRoba = new DataSet();
            daRoba.Fill(dsRoba);
            cboVrstaRobe.DataSource = dsRoba.Tables[0];
            cboVrstaRobe.DisplayMember = "Naziv";
            cboVrstaRobe.ValueMember = "ID";

            var usluge = "Select ID,Naziv from VrstaManipulacije order by ID";
            var daUsluge = new SqlDataAdapter(usluge, conn);
            var dsUsluge = new DataSet();
            daUsluge.Fill(dsUsluge);
            cboUsluga.DataSource = dsUsluge.Tables[0];
            cboUsluga.DisplayMember = "Naziv";
            cboUsluga.ValueMember = "ID";

            var sklad = "select ID,naziv from Skladista";
            var daSklad = new SqlDataAdapter(sklad, conn);
            var dsSklad = new DataSet();
            daSklad.Fill(dsSklad);
            cboNaSklad.DataSource = dsSklad.Tables[0];
            cboNaSklad.DisplayMember = "Naziv";
            cboNaSklad.ValueMember = "ID";

            var pozicija = "Select Id,Opis from Pozicija";
            var daPoz = new SqlDataAdapter(pozicija, conn);
            var dsPoz = new DataSet();
            daPoz.Fill(dsPoz);
            cboNaPoz.DataSource = dsPoz.Tables[0];
            cboNaPoz.DisplayMember = "Opis";
            cboNaPoz.ValueMember = "ID";

            var sklad2 = "Select SkSifra,SkNaziv From Sklad order by SkSifra";
            var daSklad2 = new SqlDataAdapter(sklad2, conn);
            var dsSklad2 = new DataSet();
            daSklad2.Fill(dsSklad2);
            cboSaSklad.DataSource = dsSklad2.Tables[0];
            cboSaSklad.DisplayMember = "SkNaziv";
            cboSaSklad.ValueMember = "SkSifra";

            var pozicija2 = "Select Id,Oznaka from Pozicija";
            var daPoz2 = new SqlDataAdapter(pozicija2, conn);
            var dsPoz2 = new DataSet();
            daPoz2.Fill(dsPoz2);
            cboSaPoz.DataSource = dsPoz2.Tables[0];
            cboSaPoz.DisplayMember = "Oznaka";
            cboSaPoz.ValueMember = "ID";
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            InsertRN rn = new InsertRN();
            if (status == true)
            {
                rn.InsRNPregledIpostavkaKontejnera(Convert.ToDateTime(txtDatumRasporeda.Value), txtBrojKontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cboVrstaKontejnera.SelectedValue),
                    txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(txtDatumRealizacije.Value), Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue),
                    Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue), Convert.ToInt32(cboNaSklad.SelectedValue), Convert.ToInt32(cboNaPoz.SelectedValue),
                    Convert.ToInt32(cboUsluga.SelectedValue), "", txtNapomena.Text.ToString().TrimEnd());
            }
            else
            {
                rn.UpdRNPregledIpostavkaKontejnera(Convert.ToInt32(txtID.Text), Convert.ToDateTime(txtDatumRasporeda.Value), txtBrojKontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cboVrstaKontejnera.SelectedValue),
                    txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(txtDatumRealizacije.Value), Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue),
                    Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue), Convert.ToInt32(cboNaSklad.SelectedValue), Convert.ToInt32(cboNaPoz.SelectedValue),
                    Convert.ToInt32(cboUsluga.SelectedValue), txtNalogRealizovao.Text.ToString().TrimEnd(), txtNapomena.Text.ToString().TrimEnd());
            }
            FillGV();
            status = false;
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                InsertRN rn = new InsertRN();
                rn.DelRNPregledIpostavkaKontejnera(Convert.ToInt32(txtID.Text.ToString().TrimEnd()));
                FillGV();
            }
            else
            {
                MessageBox.Show("Izaberite broj zapisa");
            }
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
                    }
                }
            }
            catch { }
        }

        private void RN11PreglediPostavkaKontejnera_Load(object sender, EventArgs e)
        {

        }
    }
}
