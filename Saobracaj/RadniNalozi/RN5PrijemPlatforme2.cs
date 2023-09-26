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
//
namespace Saobracaj.RadniNalozi
{
    public partial class RN5PrijemPlatforme2 : Form
    {
        private string connect = Sifarnici.frmLogovanje.connectionString;
        private bool status = false;
        public RN5PrijemPlatforme2()
        {
            InitializeComponent();
            FillGV();
            FillCombo();
        }
        private void FillGV()
        {
            var select = "Select * from RNPrijemPlatforme2 order by ID desc";
            SqlConnection conn = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
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

            var vSredstvo = "select ID,(CONVERT(NVARCHAR,BrVoza) + ' /' +Relacija) As Opis from Voz order by ID desc";
            var daVS = new SqlDataAdapter(vSredstvo, conn);
            var dsVS = new DataSet();
            daVS.Fill(dsVS);
            cboSaSredstva.DataSource = dsVS.Tables[0];
            cboSaSredstva.DisplayMember = "Opis";
            cboSaSredstva.ValueMember = "ID";

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

            var sklad = "Select SkSifra,SkNaziv From Sklad order by SkSifra";
            var daSklad = new SqlDataAdapter(sklad, conn);
            var dsSklad = new DataSet();
            daSklad.Fill(dsSklad);
            cboNaSklad.DataSource = dsSklad.Tables[0];
            cboNaSklad.DisplayMember = "SkNaziv";
            cboNaSklad.ValueMember = "SkSifra";

            var pozicija = "Select Id,Oznaka from Pozicija";
            var daPoz = new SqlDataAdapter(pozicija, conn);
            var dsPoz = new DataSet();
            daPoz.Fill(dsPoz);
            cboNaPoz.DataSource = dsPoz.Tables[0];
            cboNaPoz.DisplayMember = "Oznaka";
            cboNaPoz.ValueMember = "ID";
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
                rn.InsRNPPrijemPlatforme2(Convert.ToDateTime(txtDatumRasporeda.Value), txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(txtDatumRealizacije.Value),
                    Convert.ToInt32(cboSaSredstva.SelectedValue), txtbrojkontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cboVrstaKontejnera.SelectedValue),
                    Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue), Convert.ToInt32(cboNaSklad.SelectedValue), Convert.ToInt32(cboNaPoz.SelectedValue),
                    Convert.ToInt32(cboUsluga.SelectedValue), "", txtNapomena.Text.ToString().TrimEnd());
            }
            else
            {
                rn.UpdRNPPrijemPlatforme2(Convert.ToInt32(txtID.Text), Convert.ToDateTime(txtDatumRasporeda.Value), txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(txtDatumRealizacije.Value),
                    Convert.ToInt32(cboSaSredstva.SelectedValue), txtbrojkontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cboVrstaKontejnera.SelectedValue),
                    Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue), Convert.ToInt32(cboNaSklad.SelectedValue), Convert.ToInt32(cboNaPoz.SelectedValue),
                    Convert.ToInt32(cboUsluga.SelectedValue),txtNalogRealizovao.Text.ToString().TrimEnd(), txtNapomena.Text.ToString().TrimEnd());
            }
            FillGV();
            status = true;
        }
        private void tsDelete_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                InsertRN rn = new InsertRN();
                rn.DelRNPPrijemPlatforme2(Convert.ToInt32(txtID.Text));
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
                foreach(DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txtID.Text = row.Cells[0].Value.ToString();
                    }
                }
            }
            catch { }
        }
        private void RN5_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
