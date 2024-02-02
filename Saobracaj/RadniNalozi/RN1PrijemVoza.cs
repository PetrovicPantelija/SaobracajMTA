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
using System.Drawing;

//
namespace Saobracaj.RadniNalozi
{
    public partial class RN1PrijemVoza : Syncfusion.Windows.Forms.Office2010Form
    {
        private string connect = Sifarnici.frmLogovanje.connectionString;
        private bool status = false;
        string KorisnikTekuci = Saobracaj.Sifarnici.frmLogovanje.user.ToString();

        public RN1PrijemVoza()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");

            InitializeComponent();
            FillGV();
            FillCombo();

        }

        public RN1PrijemVoza(string Korisnik)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");

            InitializeComponent();
            FillGV();
            FillCombo();
            KorisnikTekuci = Korisnik;
        }

        public RN1PrijemVoza(string Korisnik, string IDVOza, string IDUsluge, string PrijemID)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3tib3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");

            InitializeComponent();

            FillCombo();
            txtNalogIzdao.Text = Korisnik;
            cboSaVoznog.SelectedValue = Convert.ToInt32(IDVOza);
            NapuniVrstuUsluge(IDUsluge);
            txtNalogID.Text = IDUsluge;
            // cboUsluge.SelectedValue = Convert.ToInt32(IDUsluge);
            txtPrijemID.Text = PrijemID;
            RefreshStavkeVoza(PrijemID);
            FillGV();
            KorisnikTekuci = Korisnik;

        }
        private void NapuniVrstuUsluge(string IDUsluga)
        {
            SqlConnection conn = new SqlConnection(connect);
            var usluge = "Select VrstaManipulacije.ID,VrstaManipulacije.Naziv from RadniNalogInterni inner join " +
   " VrstaManipulacije on RadniNalogInterni.IDManipulacijaJed = VrstaManipulacije.ID where RadniNalogInterni.ID = " + IDUsluga;
            var daUsluge = new SqlDataAdapter(usluge, conn);
            var dsUsluge = new DataSet();
            daUsluge.Fill(dsUsluge);
            cboUsluge.DataSource = dsUsluge.Tables[0];
            cboUsluge.DisplayMember = "Naziv";
            cboUsluge.ValueMember = "ID";

            //cboUsluge.SelectedValue = Convert.ToInt32(IDUsluga);
        }
        private void RefreshStavkeVoza(string IDVOza)
        {
            var select =
        " SELECT PrijemKontejneraVozStavke.ID, PrijemKontejneraVozStavke.RB, PrijemKontejneraVozStavke.IDNadredjenog,   PrijemKontejneraVozStavke.KontejnerID, " +
        " PrijemKontejneraVozStavke.BrojKontejnera,  TipKontenjera.Naziv AS TipKontejnera,PrijemKontejneraVozStavke.BrojVagona,  PrijemKontejneraVozStavke.Granica as GranicaTovarenja, " +
        " PrijemKontejneraVozStavke.BrojOsovina,  PrijemKontejneraVozStavke.SopstvenaMasa as TaraVagona,  PrijemKontejneraVozStavke.Tara,  PrijemKontejneraVozStavke.Neto, " +
        " PrijemKontejneraVozStavke.BTTORobe, PrijemKontejneraVozStavke.BTTOKontejnera, " +
        " Partnerji_4.PANaziv as Brodar, UvozKonacna.BukingBrodara AS BukingBrodar,PrijemKontejneraVozStavke.BrojPlombe, " +
        " PrijemKontejneraVozStavke.BrojPlombe2, PrijemKontejneraVozStavke.PeriodSkladistenjaOd, " +
        " PrijemKontejneraVozStavke.PeriodSkladistenjaDo, DirigacijaKOntejneraZa.Naziv as DirigacijaKOntejneraZa, VrstePostupakaUvoz.Naziv as PostupakSaRobom," +
        " Partnerji_2.PaNaziv AS Nalogodavac_Za_DrumskiPrevoz, Partnerji_3.PaNaziv AS Nalogodavac_Za_Usluge," +
         "  PrijemKontejneraVozStavke.PlaniraniLager as DIREKTNI_INDIREKTNI,    PrijemKontejneraVozStavke.NapomenaS, PrijemKontejneraVozStavke.Napomena2, " +
        " PrijemKontejneraVozStavke.Datum, PrijemKontejneraVozStavke.Korisnik " +
        " FROM  PrijemKontejneraVozStavke " +
        " inner join UvozKonacna on UvozKonacna.ID = PrijemKontejneraVozStavke.KontejnerID " +
        " INNER JOIN  Partnerji AS Partnerji_1 ON UvozKonacna.Nalogodavac1 = Partnerji_1.PaSifra " +
        " INNER JOIN  Partnerji AS Partnerji_2 ON UvozKonacna.Nalogodavac2 = Partnerji_2.PaSifra " +
        " INNER JOIN  Partnerji AS Partnerji_3 ON UvozKonacna.Nalogodavac3 = Partnerji_3.PaSifra " +
        " INNER JOIN  Partnerji AS Partnerji_4 ON UvozKonacna.Brodar = Partnerji_4.PaSifra " +
        " INNER JOIN TipKontenjera ON PrijemKontejneraVozStavke.TipKontejnera = TipKontenjera.ID " +
        " INNER join DirigacijaKontejneraZa on DirigacijaKontejneraZa.ID = PrijemKontejneraVozStavke.StatusKontejnera " +
        " INNER JOIN  Voz ON PrijemKontejneraVozStavke.IdVoza = Voz.ID " +
        " INNER JOIN VrstePostupakaUvoz ON VrstePostupakaUvoz.id = PrijemKontejneraVozStavke.PostupakSaRobom where IdNadredjenog =  " + txtPrijemID.Text + " order by RB";
            // and UvozKonacnaVrstaManipulacije.IDVrstaManipulacije = " + cboUsluge.SelectedValue  +
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
            var select = "select RNPrijemVoza.ID,BrojKontejnera, TipKontenjera.Naziv as VrstaKontejnera, DatumRasporeda, NalogIzdao, Voz.BrVoza, NaSkladiste,Skladista.Naziv as Sklad,  PArtnerji.PaNaziv as Uvoznik, p2.PaNaziv as Brodar, VrstaManipulacije.Naziv as Usliga, BrojPlombe, RNPrijemVoza.Napomena, RNPrijemVoza.PrijemID,RNPrijemVoza.NalogID, DatumRealizacije, NalogRealizovao, Zavrsen  from RNPrijemVoza " +
" inner join TipKontenjera on TipKontenjera.ID = RNPrijemVoza.VrstaKontejnera " +
" inner join Voz on RNPrijemVoza.SaVoznogSredstva = Voz.ID " +
" inner join Skladista on Skladista.ID = NaSkladiste " +
" inner join Partnerji on Partnerji.PaSifra = RNPrijemVoza.Uvoznik " +
" inner join Partnerji p2 on p2.PaSifra = RNPrijemVoza.NazivBrodara " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = IdUsluge";
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
        private void FillGVSamoPrijem()
        {
            var select = "select RNPrijemVoza.ID,BrojKontejnera, TipKontenjera.Naziv as VrstaKontejnera, DatumRasporeda, NalogIzdao, Voz.BrVoza, NaSkladiste,Skladista.Naziv as Sklad,  PArtnerji.PaNaziv as Uvoznik, p2.PaNaziv as Brodar, VrstaManipulacije.Naziv as Usliga, BrojPlombe, RNPrijemVoza.Napomena, RNPrijemVoza.PrijemID,RNPrijemVoza.NalogID, DatumRealizacije, NalogRealizovao, Zavrsen  from RNPrijemVoza " +
" inner join TipKontenjera on TipKontenjera.ID = RNPrijemVoza.VrstaKontejnera " +
" inner join Voz on RNPrijemVoza.SaVoznogSredstva = Voz.ID " +
" inner join Skladista on Skladista.ID = NaSkladiste " +
" inner join Partnerji on Partnerji.PaSifra = RNPrijemVoza.Uvoznik " +
" inner join Partnerji p2 on p2.PaSifra = RNPrijemVoza.NazivBrodara " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = IdUsluge";
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

            var partner2 = "Select PaSifra,PaNaziv From Partnerji  order by PaSifra";
            var partAD2 = new SqlDataAdapter(partner2, conn);
            var partDS2 = new DataSet();
            partAD2.Fill(partDS2);
            cboUvoznik.DataSource = partDS2.Tables[0];
            cboUvoznik.DisplayMember = "PaNaziv";
            cboUvoznik.ValueMember = "PaSifra";

            var partner7 = "Select PaSifra,PaNaziv From Partnerji order by PaSifra";
            var partAD7 = new SqlDataAdapter(partner7, conn);
            var partDS7 = new DataSet();
            partAD7.Fill(partDS7);
            cboBrodar.DataSource = partDS7.Tables[0];
            cboBrodar.DisplayMember = "PaNaziv";
            cboBrodar.ValueMember = "PaSifra";
            /*
                        var roba = "Select ID,Naziv From NHM order by ID";
                        var daRoba = new SqlDataAdapter(roba, conn);
                        var dsRoba = new DataSet();
                        daRoba.Fill(dsRoba);
                        cboVrstaRobe.DataSource = dsRoba.Tables[0];
                        cboVrstaRobe.DisplayMember = "Naziv";
                        cboVrstaRobe.ValueMember = "ID";
            */


            var sklad = "select ID,naziv from Skladista";
            var daSklad = new SqlDataAdapter(sklad, conn);
            var dsSklad = new DataSet();
            daSklad.Fill(dsSklad);
            cboNaSkladiste.DataSource = dsSklad.Tables[0];
            cboNaSkladiste.DisplayMember = "Naziv";
            cboNaSkladiste.ValueMember = "ID";

            var pozicija = "Select Id,Opis from Pozicija";
            var daPoz = new SqlDataAdapter(pozicija, conn);
            var dsPoz = new DataSet();
            daPoz.Fill(dsPoz);
            cboNaPoziciju.DataSource = dsPoz.Tables[0];
            cboNaPoziciju.DisplayMember = "Opis";
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
            DateTime datumRealizacije = Convert.ToDateTime(txtDatumRealizacije.Value);

            if (status == true)
            {
                rn.InsRNPPrijemVoza(Convert.ToDateTime(txtDatumRasporeda.Value), txtbrojkontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cbovrstakontejnera.SelectedValue),
                    txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(txtDatumRealizacije.Value), Convert.ToInt32(cboSaVoznog.SelectedValue),
                    txtBrojPlombe.Text.ToString().TrimEnd(), Convert.ToInt32(cboUvoznik.SelectedValue), Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue),
                    Convert.ToInt32(cboNaSkladiste.SelectedValue), Convert.ToInt32(cboNaPoziciju.SelectedValue), Convert.ToInt32(cboUsluge.SelectedValue), "", txtNapomena.Text.ToString().TrimEnd());
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

            DialogResult dialogResult = MessageBox.Show("Formirace se RN za sve Interne Naloge po Vrsti usluge ", "Some Title", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                RadniNalozi.InsertRN ir = new InsertRN();
                ir.InsRNPPrijemVozaCeoVoz(Convert.ToDateTime(txtDatumRasporeda.Value), txtNalogIzdao.Text, Convert.ToDateTime(txtDatumRealizacije.Text), Convert.ToInt32(cboSaVoznog.SelectedValue), Convert.ToInt32(cboNaSkladiste.SelectedValue), Convert.ToInt32(cboNaPoziciju.SelectedValue), Convert.ToInt32(cboUsluge.SelectedValue), "", txtNapomena.Text, Convert.ToInt32(txtPrijemID.Text));
                FillGV();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }



            // RadniNalozi.InsertRN ir = new InsertRN();
            // ir.InsRNPPrijemVozaCeoVoz(Convert.ToDateTime(txtDatumRasporeda.Value), txtNalogIzdao.Text, Convert.ToDateTime(txtDatumRealizacije.Text), Convert.ToInt32(cboSaVoznog.SelectedValue), Convert.ToInt32(cboNaSkladiste.SelectedValue), Convert.ToInt32(cboNaPoziciju.SelectedValue), Convert.ToInt32(cboUsluge.SelectedValue), "", txtNapomena.Text, Convert.ToInt32(txtPrijemID.Text));
            // FillGV();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillGVSamoPrijem();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //PArametar 2 se odnosi na Radni nalog 
            Saobracaj.RadniNalozi.frmDodelaSkladista ds = new frmDodelaSkladista(txtPrijemID.Text, 1);
            ds.Show();
        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected == true)
                {
                    txtID.Text = row.Cells[0].Value.ToString();
                    VratiPodatkeStavka();
                    FillDG2();
                }
               
            }
        }

        private void FillDG2()
        {

            var select = "  SELECT     UvozKonacnaNHM.ID, NHM.Broj, UvozKonacnaNHM.IDNHM, NHM.Naziv FROM NHM INNER JOIN " +
" UvozKonacnaNHM ON NHM.ID = UvozKonacnaNHM.IDNHM " +
" inner join RadniNalogInterni on UvozKonacnanhm.idnadredjena = RadniNalogInterni.BrojOsnov " +
" where RadniNalogInterni.ID = " + Convert.ToInt32(txtNalogID.Text) + " order by UvozKonacnanhm.ID desc";


            SqlConnection conn = new SqlConnection(connect);
           
            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds.Tables[0];

            dataGridView3.BorderStyle = BorderStyle.None;
            dataGridView3.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView3.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView3.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView3.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView3.BackgroundColor = Color.White;

            dataGridView3.EnableHeadersVisualStyles = false;
            dataGridView3.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView3.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView3.Columns[0];
            dataGridView3.Columns[0].HeaderText = "ID";
            dataGridView3.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView3.Columns[1];
            dataGridView3.Columns[1].HeaderText = "NHM Broj";
            dataGridView3.Columns[1].Width = 70;

            DataGridViewColumn column3 = dataGridView3.Columns[2];
            dataGridView3.Columns[2].HeaderText = "ID";
            dataGridView3.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView3.Columns[3];
            dataGridView3.Columns[3].HeaderText = "NHM";
            dataGridView3.Columns[3].Width = 150;


        }

        private void VratiPodatkeStavka()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" select RNPrijemVoza.ID, RNPrijemVoza.BrojKontejnera, TipKontenjera.ID as VrstaKontejnera, DatumRasporeda," +
                " NalogIzdao, Voz.BrVoza, NaSkladiste, NaPozicijuSklad,  PArtnerji.PaSifra as Uvoznik, p2.PaSifra as Brodar, VrstaManipulacije.ID as Usluga, " +
                "BrojPlombe, RNPrijemVoza.Napomena, RNPrijemVoza.PrijemID, RNPrijemVoza.NalogID, DatumRealizacije, NalogRealizovao, " +
                "Zavrsen  from RNPrijemVoza " +
" inner join TipKontenjera on TipKontenjera.ID = RNPrijemVoza.VrstaKontejnera " +
" inner join Voz on RNPrijemVoza.SaVoznogSredstva = Voz.ID " +
" inner join Skladista on Skladista.ID = NaSkladiste " +
" inner join Partnerji on Partnerji.PaSifra = RNPrijemVoza.Uvoznik " +
" inner join Partnerji p2 on p2.PaSifra = RNPrijemVoza.NazivBrodara " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = IdUsluge"  +
             " where RNPrijemVoza.ID = " + txtID.Text, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtDatumRasporeda.Value = Convert.ToDateTime(dr["DatumRasporeda"].ToString());
                txtbrojkontejnera.Text = dr["BrojKontejnera"].ToString();
                cbovrstakontejnera.SelectedValue = Convert.ToInt32(dr["VrstaKontejnera"].ToString());
                        cboUsluge.SelectedValue = Convert.ToInt32(dr["Usluga"].ToString());
                  txtNalogRealizovao.Text = dr["NalogRealizovao"].ToString();

                txtPrijemID.Text = dr["PrijemID"].ToString();
             
                txtNalogID.Text = dr["NalogID"].ToString();
            
                 cboNaPoziciju.SelectedValue = Convert.ToInt32(dr["NaPozicijuSklad"].ToString());
                txtNalogIzdao.Text = dr["NalogIzdao"].ToString();
                txtDatumRealizacije.Value = Convert.ToDateTime(dr["DatumRealizacije"].ToString());
                cboUvoznik.SelectedValue = Convert.ToInt32(dr["Uvoznik"].ToString());
                 cboBrodar.SelectedValue = Convert.ToInt32(dr["Brodar"].ToString());
  //cboVrstaRobe.SelectedValue = Convert.ToInt32(dr["VrstaRobe"].ToString());
                cboNaSkladiste.SelectedValue = Convert.ToInt32(dr["NaSkladiste"].ToString());

                txtBrojPlombe.Text = dr["BrojPlombe"].ToString();
                txtNapomena.Text = dr["Napomena"].ToString();


                if (dr["Zavrsen"].ToString() == "1")
                { chkZavrsen.Checked = true; }
                else
                {
                    chkZavrsen.Checked = false;
                }
            }

            con.Close();
        }


        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            InsertRN up = new InsertRN();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected == true)
                {
                    up.PotvrdiUradjenRN1(Convert.ToInt32(row.Cells[0].Value.ToString()));
                }
                    
            }
        }

        private void txtNalogID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}