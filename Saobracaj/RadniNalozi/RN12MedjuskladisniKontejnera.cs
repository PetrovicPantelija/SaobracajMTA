using Saobracaj.Uvoz;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using static Syncfusion.WinForms.Core.NativeScroll;
using Saobracaj;
//
namespace Saobracaj.RadniNalozi
{
    public partial class RN12MedjuskladisniKontejnera : Syncfusion.Windows.Forms.Office2010Form
    {
        private string connect = Sifarnici.frmLogovanje.connectionString;
        private bool status = false;
        string KorisnikTekuci = Saobracaj.Sifarnici.frmLogovanje.user.ToString();
        public RN12MedjuskladisniKontejnera()
        {
            InitializeComponent();
            FillGV();
            FillCombo();
        }
        private void NapuniVrstuUsluge(string IDUsluga)
        {
            SqlConnection conn = new SqlConnection(connect);
            var usluge = "Select VrstaManipulacije.ID,VrstaManipulacije.Naziv from RadniNalogInterni inner join " +
   " VrstaManipulacije on RadniNalogInterni.IDManipulacijaJed = VrstaManipulacije.ID where RadniNalogInterni.ID = " + IDUsluga;
            var daUsluge = new SqlDataAdapter(usluge, conn);
            var dsUsluge = new DataSet();
            daUsluge.Fill(dsUsluge);
            cboUsluga.DataSource = dsUsluge.Tables[0];
            cboUsluga.DisplayMember = "Naziv";
            cboUsluga.ValueMember = "ID";

            //cboUsluge.SelectedValue = Convert.ToInt32(IDUsluga);
        }

        public RN12MedjuskladisniKontejnera(int NalogID, string BrojKontejnera, string Napomena)
        {
            InitializeComponent();
            FillGV();
            FillCombo();
            txtNapomena.Text = Napomena;
            txtBrojKontejnera.Text = BrojKontejnera;
            txtNalogID.Text = NalogID.ToString();

            NapuniVrstuUsluge(NalogID.ToString());

            KorisnikTekuci = Saobracaj.Sifarnici.frmLogovanje.user;
            txtDatumRasporeda.Value = DateTime.Now;
        }

        private void FillGV()
        {
            var select = "SELECT       RNMedjuskladisni.ID as ID,  RNMedjuskladisni.BrojKontejnera,  RNMedjuskladisni.VrstaKontejnera,TipKontenjera.Naziv, " +
                "Skladista.ID as SaID, Skladista.Naziv AS SkladSa, Skladista_1.ID AS NaID, Skladista_1.Naziv AS NaSkladiste," +
                "                     RNMedjuskladisni.DatumRasporeda, " +
               " RNMedjuskladisni.NalogIzdao, RNMedjuskladisni.DatumRealizacije, RNMedjuskladisni.NalogRealizovao, RNMedjuskladisni.Napomena, " +
               " RNMedjuskladisni.Zavrsen FROM            RNMedjuskladisni INNER JOIN" +
               " TipKontenjera ON RNMedjuskladisni.VrstaKontejnera = TipKontenjera.ID INNER JOIN" +
               " Skladista ON RNMedjuskladisni.SaSkladista = Skladista.ID INNER JOIN                         Skladista AS Skladista_1 " +
               " ON RNMedjuskladisni.NaSkladiste = Skladista_1.ID Order by Zavrsen, RNMedjuskladisni.ID";
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
            cboNaPoz.DisplayMember = "Oznaka";
            cboNaPoz.ValueMember = "ID";

            var sklad2 = "select ID,naziv from Skladista";
            var daSklad2 = new SqlDataAdapter(sklad2, conn);
            var dsSklad2 = new DataSet();
            daSklad2.Fill(dsSklad2);
            cboSaSklad.DataSource = dsSklad2.Tables[0];
            cboSaSklad.DisplayMember = "Naziv";
            cboSaSklad.ValueMember = "ID";

            var pozicija2 = "Select Id,Opis from Pozicija";
            var daPoz2 = new SqlDataAdapter(pozicija2, conn);
            var dsPoz2 = new DataSet();
            daPoz2.Fill(dsPoz2);
            cboSaPoz.DataSource = dsPoz2.Tables[0];
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
                rn.InsRnMedjuskladisni(Convert.ToDateTime(txtDatumRasporeda.Value), txtBrojKontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cboVrstaKontejnera.SelectedValue),
                    txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(txtDatumRealizacije.Value), Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue),
                    Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue), Convert.ToInt32(cboNaSklad.SelectedValue), Convert.ToInt32(cboNaPoz.SelectedValue),
                    Convert.ToInt32(cboUsluga.SelectedValue), "", txtNapomena.Text.ToString().TrimEnd(), Convert.ToInt32(txtNalogID.Text));
            }
            else
            {
                rn.UpdRnMedjuskladisni(Convert.ToInt32(txtID.Text), Convert.ToDateTime(txtDatumRasporeda.Value), txtBrojKontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cboVrstaKontejnera.SelectedValue),
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
                rn.DelRnMedjuskladisni(Convert.ToInt32(txtID.Text.ToString().TrimEnd()));
                FillGV();
            }
            else
            {
                MessageBox.Show("Izaberite broj zapisa");
            }
        }

        private void VratiPodatke(string ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT  ID, DatumRasporeda, VrstaKontejnera, BrojKontejnera, NalogIzdao, SaSkladista, SaPozicijeSklad, NaSkladiste, " +
                " NaPoziciju, NalogRealizovao, Napomena, Zavrsen " +
                " FROM  RNMedjuskladisni where ID=" + ID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtID.Text = dr["ID"].ToString();
                txtDatumRasporeda.Value = Convert.ToDateTime(dr["DatumRasporeda"].ToString());
                cboVrstaKontejnera.SelectedValue = Convert.ToInt32(dr["VrstaKontejnera"].ToString());
                txtBrojKontejnera.Text = dr["BrojKontejnera"].ToString();
                txtNalogIzdao.Text = dr["NalogIzdao"].ToString();
                cboSaSklad.SelectedValue = Convert.ToInt32(dr["SaSkladista"].ToString());
                cboSaPoz.SelectedValue = Convert.ToInt32(dr["SaPozicijeSklad"].ToString());
                cboNaSklad.SelectedValue = Convert.ToInt32(dr["NaSkladiste"].ToString());
                cboNaPoz.SelectedValue = Convert.ToInt32(dr["NaPoziciju"].ToString());
                txtNalogRealizovao.Text = dr["NalogRealizovao"].ToString();
                txtNapomena.Text = dr["Napomena"].ToString();
                if (dr["Zavrsen"].ToString() == "1")
                {
                    chkZavrsen.Checked = true;
                }
                else
                {
                    chkZavrsen.Checked = false;
                }

             
            }
            con.Close();


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
                        VratiPodatke(txtID.Text);
                    }
                }
            }
            catch { }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

          
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            InsertRN up = new InsertRN();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected == true)
                {
                    up.PotvrdiUradjenRN12(Convert.ToInt32(row.Cells[0].Value.ToString()), KorisnikTekuci);
                }

            }
        }

        private void VratiOstaloIzTekuceg(string BrojKontejnera)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT TipKontejnera, Skladiste " +
  " FROM KontejnerTekuce where Kontejner= '" + BrojKontejnera + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
               // txtID.Text = dr["ID"].ToString();
                cboVrstaKontejnera.SelectedValue = Convert.ToInt32(dr["TipKontejnera"].ToString());
                cboSaSklad.SelectedValue = Convert.ToInt32(dr["Skladiste"].ToString());
            }
            con.Close();




        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            using (var detailForm = new Uvoz.frmKontejnerTekuce())
            {
                detailForm.ShowDialog();
                txtBrojKontejnera.Text = detailForm.GetBrojKontejnera();
                VratiOstaloIzTekuceg(txtBrojKontejnera.Text);
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            RadniNalozi.InsertRN ir = new InsertRN();
            ir.InsRN12Medjuskladisni(Convert.ToDateTime(txtDatumRasporeda.Value), txtNalogIzdao.Text, Convert.ToDateTime(txtDatumRealizacije.Text), Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue), Convert.ToInt32(cboNaSklad.SelectedValue), Convert.ToInt32(cboNaPoz.SelectedValue), Convert.ToInt32(cboUsluga.SelectedValue), "", txtNapomena.Text, txtBrojKontejnera.Text, Convert.ToInt32(cboVrstaKontejnera.SelectedValue), Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(txtNalogID.Text));
            FillGV();
        }
    }
}
