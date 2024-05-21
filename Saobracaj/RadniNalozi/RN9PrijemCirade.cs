using Saobracaj.Sifarnici;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
//
namespace Saobracaj.RadniNalozi
{
    public partial class RN9PrijemCirade : Syncfusion.Windows.Forms.Office2010Form
    {
        private string connect = Sifarnici.frmLogovanje.connectionString;
        private bool status = false;
        string kor = frmLogovanje.user;

        public RN9PrijemCirade()
        {
            InitializeComponent();
            FillGV();
            FillCombo();
        }

        public RN9PrijemCirade(string PrijemID, string Kamion, string Korisnik)
        {
            InitializeComponent();
            FillGV();
            FillCombo();
            txtPrijemID.Text = PrijemID;
            txtKamion.Text = Kamion;
            txtNalogIzdao.Text = Korisnik;

        }
        private void FillGV()
        {
            var select = "Select * from RNPrijemCirade order by ID desc";
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

            var vSredstvo = "select ID,(CONVERT(NVARCHAR,BrVoza) + ' /' +Relacija) As Opis from Voz order by ID desc";
            var daVS = new SqlDataAdapter(vSredstvo, conn);
            var dsVS = new DataSet();
            daVS.Fill(dsVS);
            cboSaSredstva.DataSource = dsVS.Tables[0];
            cboSaSredstva.DisplayMember = "Opis";
            cboSaSredstva.ValueMember = "ID";

            var dir2 = "Select ID, (Oznaka + ' ' + Naziv) as Naziv from VrstaCarinskogPostupka order by Oznaka";
            var dirAD2 = new SqlDataAdapter(dir2, conn);
            var dirDS2 = new DataSet();
            dirAD2.Fill(dirDS2);
            cboPostupak.DataSource = dirDS2.Tables[0];
            cboPostupak.DisplayMember = "Naziv";
            cboPostupak.ValueMember = "ID";

            var it = "select ID, Naziv from InspekciskiTretman order by Naziv";
            var itAD = new SqlDataAdapter(it, conn);
            var itDS = new DataSet();
            itAD.Fill(itDS);
            cboInspekcijski.DataSource = itDS.Tables[0];
            cboInspekcijski.DisplayMember = "Naziv";
            cboInspekcijski.ValueMember = "ID";

            var partner4 = "Select PaSifra,PaNaziv From Partnerji where Spediter = 1 order by PaSifra";
            var partAD4 = new SqlDataAdapter(partner4, conn);
            var partDS4 = new DataSet();
            partAD4.Fill(partDS4);
            cboSpedicija.DataSource = partDS4.Tables[0];
            cboSpedicija.DisplayMember = "PaNaziv";
            cboSpedicija.ValueMember = "PaSifra";

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
            cboSaSklad.DataSource = dsSklad.Tables[0];
            cboSaSklad.DisplayMember = "Naziv";
            cboSaSklad.ValueMember = "ID";

            var pozicija = "Select Id,Opis from Pozicija";
            var daPoz = new SqlDataAdapter(pozicija, conn);
            var dsPoz = new DataSet();
            daPoz.Fill(dsPoz);
            cboSaPoz.DataSource = dsPoz.Tables[0];
            cboSaPoz.DisplayMember = "Opis";
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
                rn.InsRNPPrijemCirade(Convert.ToDateTime(txtDatumRasporeda.Value), txtBrojKontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cboVrstaKontejnera.SelectedValue),
                    txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(txtDatumRealizacije.Value), Convert.ToInt32(cboSaSredstva.SelectedValue),
                    txtBrojPlombe.Text.ToString().TrimEnd(), Convert.ToInt32(cboPostupak.SelectedValue), Convert.ToInt32(cboInspekcijski.SelectedValue),
                    Convert.ToInt32(cboSpedicija.SelectedValue), Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue), Convert.ToInt32(cboSaSklad.SelectedValue),
                    Convert.ToInt32(cboSaPoz.SelectedValue), Convert.ToInt32(cboUsluga.SelectedValue), "", txtNapomena.Text.ToString().TrimEnd());
            }
            else
            {
                rn.UpdRNPPrijemCirade(Convert.ToInt32(txtID.Text), Convert.ToDateTime(txtDatumRasporeda.Value), txtBrojKontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cboVrstaKontejnera.SelectedValue),
                    txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(txtDatumRealizacije.Value), Convert.ToInt32(cboSaSredstva.SelectedValue),
                    txtBrojPlombe.Text.ToString().TrimEnd(), Convert.ToInt32(cboPostupak.SelectedValue), Convert.ToInt32(cboInspekcijski.SelectedValue),
                    Convert.ToInt32(cboSpedicija.SelectedValue), Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue), Convert.ToInt32(cboSaSklad.SelectedValue),
                    Convert.ToInt32(cboSaPoz.SelectedValue), Convert.ToInt32(cboUsluga.SelectedValue), txtNalogRealizovao.Text.ToString().TrimEnd(), txtNapomena.Text.ToString().TrimEnd());
            }
            FillGV();
            status = false;
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                InsertRN rn = new InsertRN();
                rn.DelRNPPrijemCirade(Convert.ToInt32(txtID.Text.ToString().TrimEnd()));
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

        private void RN9PrijemCirade_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            RadniNalozi.InsertRN ir = new InsertRN();
            ir.InsRN9PrijmCiradeKam(Convert.ToDateTime(txtDatumRasporeda.Value), txtNalogIzdao.Text, Convert.ToDateTime(txtDatumRealizacije.Text), Convert.ToInt32(cboSaSredstva.SelectedValue), Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue), Convert.ToInt32(cboUsluga.SelectedValue), "", txtNapomena.Text, Convert.ToInt32(txtPrijemID.Text), txtKamion.Text, Convert.ToInt32(cboPostupak.SelectedValue), Convert.ToInt32(cboInspekcijski.SelectedValue), Convert.ToInt32(cboSpedicija.SelectedValue), Convert.ToInt32(cboBrodar.SelectedValue), txtBrojPlombe.Text,Convert.ToInt32(txtNalogID.Text));
            FillGV();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Prijemnica pr = new Prijemnica();
            pr.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            PrijemnicaPregled pp = new PrijemnicaPregled();
            pp.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            RadniNalozi.InsertRN rn = new InsertRN();
            rn.PotvrdiUradjenRN9(Convert.ToInt32(txtID.Text), kor);
        }
    }
}
