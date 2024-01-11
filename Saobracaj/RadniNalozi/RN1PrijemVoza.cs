using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Diagnostics.CodeAnalysis;
using Saobracaj;

//
namespace Saobracaj.RadniNalozi
{
    public partial class RN1PrijemVoza : Form
    {
        private string connect = Sifarnici.frmLogovanje.connectionString;
        private bool status = false;

        public RN1PrijemVoza()
        {
            InitializeComponent();
            FillGV();
            FillCombo();
        }

        public RN1PrijemVoza(string Korisnik)
        {
            InitializeComponent();
            FillGV();
            FillCombo();
            
        }

        public RN1PrijemVoza(string Korisnik, string IDVOza, string IDUsluge, string PrijemID)
        {
            InitializeComponent();
            FillGV();
            FillCombo();
            txtNalogIzdao.Text = Korisnik;
            cboSaVoznog.SelectedValue = Convert.ToInt32(IDVOza);
            cboUsluge.SelectedValue = Convert.ToInt32(IDUsluge);
            txtPrijemID.Text = PrijemID;
            RefreshStavkeVoza(PrijemID);

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
   " where IdNadredjenog = " + IDVOza + " and  UvozKonacnaVrstaManipulacije.IDVrstaManipulacije = " + cboUsluge.SelectedValue + " order by RB";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
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
            var select = "Select * from RNPrijemVoza where PrijemID = " + txtPrijemID.Text + " order by ID desc";
            SqlConnection conn = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void FillGVSamoPrijem()
        {
            var select = "Select * from RNPrijemVoza order by ID desc";
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
            cbovrstakontejnera.DataSource = daDS.Tables[0];
            cbovrstakontejnera.DisplayMember = "SkNaziv";
            cbovrstakontejnera.ValueMember = "ID";

            txtNalogIzdao.Text = Sifarnici.frmLogovanje.user.ToString().TrimEnd();
            //usluge->Manipulacije

            var vSredstvo = "Select Distinct ID, (Cast(BrVoza as nvarchar(6)) + '-' + Relacija) as IdVoza   From Voz";
            var daVS = new SqlDataAdapter(vSredstvo, conn);
            var dsVS = new DataSet();
            daVS.Fill(dsVS);
            cboSaVoznog.DataSource = dsVS.Tables[0];
            cboSaVoznog.DisplayMember = "IdVoza";
            cboSaVoznog.ValueMember = "ID";

            var partner2 = "Select PaSifra,PaNaziv From Partnerji where UvoznikCH = 1 order by PaSifra";
            var partAD2 = new SqlDataAdapter(partner2, conn);
            var partDS2 = new DataSet();
            partAD2.Fill(partDS2);
            cboUvoznik.DataSource = partDS2.Tables[0];
            cboUvoznik.DisplayMember = "PaNaziv";
            cboUvoznik.ValueMember = "PaSifra";

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
            cboUsluge.DataSource = dsUsluge.Tables[0];
            cboUsluge.DisplayMember = "Naziv";
            cboUsluge.ValueMember = "ID";

            var sklad = "Select SkSifra,SkNaziv From Sklad order by SkSifra";
            var daSklad = new SqlDataAdapter(sklad, conn);
            var dsSklad = new DataSet();
            daSklad.Fill(dsSklad);
            cboNaSkladiste.DataSource = dsSklad.Tables[0];
            cboNaSkladiste.DisplayMember = "SkNaziv";
            cboNaSkladiste.ValueMember = "SkSifra";

            var pozicija = "Select Id,Oznaka from Pozicija";
            var daPoz = new SqlDataAdapter(pozicija, conn);
            var dsPoz = new DataSet();
            daPoz.Fill(dsPoz);
            cboNaPoziciju.DataSource = dsPoz.Tables[0];
            cboNaPoziciju.DisplayMember = "Oznaka";
            cboNaPoziciju.ValueMember = "ID";
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            InsertRN rn = new InsertRN();
            DateTime datumRasporeda = Convert.ToDateTime(txtDatumRasporeda.Value);
            DateTime datumRealizacije=Convert.ToDateTime(txtDatumRealizacije.Value);

            if (status == true)
            {
                rn.InsRNPPrijemVoza(Convert.ToDateTime(txtDatumRasporeda.Value), txtbrojkontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cbovrstakontejnera.SelectedValue),
                    txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(txtDatumRealizacije.Value), Convert.ToInt32(cboSaVoznog.SelectedValue),
                    txtBrojPlombe.Text.ToString().TrimEnd(), Convert.ToInt32(cboUvoznik.SelectedValue), Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue),
                    Convert.ToInt32(cboNaSkladiste.SelectedValue), Convert.ToInt32(cboNaPoziciju.SelectedValue), Convert.ToInt32(cboUsluge.SelectedValue), "",txtNapomena.Text.ToString().TrimEnd());
            }
            else
            {
                rn.UpdRNPPrijemVoza(Convert.ToInt32(txtID.Text.ToString().TrimEnd()), Convert.ToDateTime(txtDatumRasporeda.Value), txtbrojkontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cbovrstakontejnera.SelectedValue),
                    txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(txtDatumRealizacije.Value), Convert.ToInt32(cboSaVoznog.SelectedValue),
                    txtBrojPlombe.Text.ToString().TrimEnd(), Convert.ToInt32(cboUvoznik.SelectedValue), Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue),
                    Convert.ToInt32(cboNaSkladiste.SelectedValue), Convert.ToInt32(cboNaPoziciju.SelectedValue), Convert.ToInt32(cboUsluge.SelectedValue), txtNalogRealizovao.Text.ToString().TrimEnd(),
                    txtNapomena.Text.ToString().TrimEnd());
            }
            FillGV();
            status = false;
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                InsertRN rn = new InsertRN();
                rn.DelRNPPrijemVoza(Convert.ToInt32(txtID.Text.ToString().TrimEnd()));
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

        private void txtDatumRasporeda_ValueChanged(object sender, EventArgs e)
        {
        }

        private void label7_Click(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void label10_Click(object sender, EventArgs e)
        {
        }

        private void RN1PrijemVoza_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            RadniNalozi.InsertRN ir = new InsertRN();
            ir.InsRNPPrijemVozaCeoVoz(Convert.ToDateTime(txtDatumRasporeda.Value), txtNalogIzdao.Text, Convert.ToDateTime(txtDatumRealizacije.Text), Convert.ToInt32(cboSaVoznog.SelectedValue), Convert.ToInt32(cboNaSkladiste.SelectedValue), Convert.ToInt32(cboNaPoziciju.SelectedValue), Convert.ToInt32(cboUsluge.SelectedValue), "", txtNapomena.Text, Convert.ToInt32(txtPrijemID.Text));
            FillGV();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillGVSamoPrijem();
        }
    }
}