using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
//
namespace Saobracaj.RadniNalozi
{
    public partial class RN10PregledIspraznjenogKontejnera : Syncfusion.Windows.Forms.Office2010Form
    {
        private string connect = Sifarnici.frmLogovanje.connectionString;
        private bool status = false;
        public RN10PregledIspraznjenogKontejnera()
        {
            InitializeComponent();
            FillGV();
            FillCombo();
        }

        public RN10PregledIspraznjenogKontejnera(string Korisnik, int IDVOza, string IDUsluge, string PrijemID, string Kamion)
        {
            InitializeComponent();
            FillGV();
            FillCombo();
            txtNalogIzdao.Text = Korisnik;
            cboSaVoznog.SelectedValue = Convert.ToInt32(IDVOza);
            NapuniVrstuUsluge(IDUsluge);
            // cboUsluga.SelectedValue = Convert.ToInt32(IDUsluge);
            txtPrijemID.Text = PrijemID;
            txtKamion.Text = Kamion;

            RefreshStavkeVoza(PrijemID);

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

            cboUsluga.SelectedValue = Convert.ToInt32(IDUsluga);
        }
        private void RefreshStavkeVoza(string IDVOza)
        {
            var select = "  SELECT         PrijemKontejneraVozStavke.ID, PrijemKontejneraVozStavke.RB, PrijemKontejneraVozStavke.IDNadredjenog,  " +
   " PrijemKontejneraVozStavke.KontejnerID, " +
   " PrijemKontejneraVozStavke.BrojKontejnera, " +
   " PrijemKontejneraVozStavke.BrojVagona, " +
   " PrijemKontejneraVozStavke.Granica,  " +
   " PrijemKontejneraVozStavke.SopstvenaMasa as TaraVagona, " +
   " PrijemKontejneraVozStavke.Tara, " +
   " PrijemKontejneraVozStavke.Neto, " +
   " PrijemKontejneraVozStavke.BTTORobe," +
   " PrijemKontejneraVozStavke.BTTOKontejnera, " +
   " Partnerji_3.PaNaziv AS Nalogodavac_Za_Voz, " +
   " Partnerji.PaNaziv AS Nalogodavac_Za_Usluge," +
   " Partnerji_1.PaNaziv AS Nalogodavac_Za_DrumskiPrevoz,  " +
   " Partnerji_2.PaNaziv AS Vlasnikkontejnera, " +
   " TipKontenjera.Naziv AS TipKontejnera,   " +
   " PrijemKontejneraVozStavke.BukingBrodar AS BukingBrodar," +
   " DirigacijaKOntejneraZa.Naziv as DirigacijaKOntejneraZa, " +
   " PrijemKontejneraVozStavke.BrojPlombe, PrijemKontejneraVozStavke.BrojPlombe2," +
   " PrijemKontejneraVozStavke.PlaniraniLager as DIREKTNI_INDIREKTNI,  " +
   " PrijemKontejneraVozStavke.PeriodSkladistenjaOd, PrijemKontejneraVozStavke.PeriodSkladistenjaDo, " +
   " PrijemKontejneraVozStavke.NapomenaS, PrijemKontejneraVozStavke.Napomena2, VrstePostupakaUvoz.Naziv as PostupakSaRobom," +
   " PrijemKontejneraVozStavke.Datum, PrijemKontejneraVozStavke.Korisnik" +
   " FROM  Partnerji INNER JOIN PrijemKontejneraVozStavke        ON Partnerji.PaSifra = PrijemKontejneraVozStavke.Posiljalac " +
   " INNER JOIN  Partnerji AS Partnerji_1 ON PrijemKontejneraVozStavke.Primalac = Partnerji_1.PaSifra " +
   " INNER JOIN  Partnerji AS Partnerji_2 ON PrijemKontejneraVozStavke.VlasnikKontejnera = Partnerji_2.PaSifra " +
   " INNER JOIN  Partnerji AS Partnerji_3 ON PrijemKontejneraVozStavke.Organizator = Partnerji_3.PaSifra " +
   " INNER JOIN TipKontenjera ON PrijemKontejneraVozStavke.TipKontejnera = TipKontenjera.ID " +
   " LEFT join DirigacijaKontejneraZa on DirigacijaKontejneraZa.ID = PrijemKontejneraVozStavke.StatusKontejnera " +
   " INNER JOIN  Voz ON PrijemKontejneraVozStavke.IdVoza = Voz.ID " +
   " INNER JOIN VrstePostupakaUvoz ON VrstePostupakaUvoz.id = PrijemKontejneraVozStavke.PostupakSaRobom " +
   " inner join UvozKonacnaVrstaManipulacije on UvozKonacnaVrstaManipulacije.IDNAdredjena = PrijemKontejneraVozStavke.KontejnerID " +
   " where IdNadredjenog = " + IDVOza + " and  UvozKonacnaVrstaManipulacije.IDVrstaManipulacije = " + cboUsluga.SelectedValue + " order by RB";

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = false;
            dataGridView2.DataSource = ds.Tables[0];



        }


        private void FillGV()
        {
            var select = "Select * from RNPregledIspraznjenogKontejnera order by ID desc";
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

            var partner7 = "Select PaSifra,PaNaziv From Partnerji  order by PaSifra";
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
            cboNaPoz.DisplayMember = "Pozicija";
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
                rn.InsRNPregledIspraznjenogKontejnera(Convert.ToDateTime(dtpDatumRasporeda.Value), txtBrojKontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cboVrstaKontejnera.SelectedValue),
                    txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(dtpDatumRealizacije.Value), Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue),
                    Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue), Convert.ToInt32(cboNaSklad.SelectedValue), Convert.ToInt32(cboNaPoz.SelectedValue),
                    Convert.ToInt32(cboUsluga.SelectedValue), "", txtNapomena.Text.ToString().TrimEnd());
            }
            else
            {
                rn.UpdRNPregledIspraznjenogKontejnera(Convert.ToInt32(txtID.Text), Convert.ToDateTime(dtpDatumRasporeda.Value), txtBrojKontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cboVrstaKontejnera.SelectedValue),
                    txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(dtpDatumRealizacije.Value), Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue),
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
                rn.DelRNPregledIspraznjenogKontejnera(Convert.ToInt32(txtID.Text.ToString().TrimEnd()));
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

        private void RN10PregledIspraznjenogKontejnera_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Formirace se RN za sve Interne Naloge po Vrsti usluge ", "PREGLED KONTEJNERA", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                RadniNalozi.InsertRN ir = new InsertRN();
                ir.InsRN10PregledCeoVoz(Convert.ToDateTime(dtpDatumRasporeda.Value), txtNalogIzdao.Text, Convert.ToDateTime(dtpDatumRealizacije.Text), Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue), Convert.ToInt32(cboNaSklad.SelectedValue), Convert.ToInt32(cboNaPoz.SelectedValue), Convert.ToInt32(cboUsluga.SelectedValue), "", txtNapomena.Text, Convert.ToInt32(txtPrijemID.Text), txtKamion.Text);
                FillGV();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }


        }
    }
}
