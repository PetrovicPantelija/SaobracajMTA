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
    public partial class RN3PrijemVoza2 : Syncfusion.Windows.Forms.Office2010Form
    {
        private string connect = Sifarnici.frmLogovanje.connectionString;
        private bool status = false;
        public RN3PrijemVoza2()
        {
            InitializeComponent();
            FillGV();
            FillCombo();
        }

        public RN3PrijemVoza2(string Korisnik, string IDVOza, string IDUsluge, string PrijemID)
        {
            InitializeComponent();
            FillGV();
            FillCombo();
            txtNalogIzdao.Text = Korisnik;
            cboSaSredstva.SelectedValue = Convert.ToInt32(IDVOza);
            VratiPodatkeVrstaMan(IDUsluge.ToString());
          //  NapuniVrstuUsluge(IDUsluge);
            
            txtNalogID.Text = IDUsluge;
            txtPrijemID.Text = PrijemID;
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

            
        }
        private void VratiPodatkeVrstaMan(string IDUsluge)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" Select VrstaManipulacije.ID from RadniNalogInterni inner join " +
   " VrstaManipulacije on RadniNalogInterni.IDManipulacijaJed = VrstaManipulacije.ID where RadniNalogInterni.ID = " + IDUsluge, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                cboUsluga.SelectedValue = Convert.ToInt32(dr["ID"].ToString());

            }

            con.Close();
        }
        private void RefreshStavkeVoza(string IDVOza)
        {
            if (cboUsluga.SelectedValue is null)
            {
                MessageBox.Show("Izaberite uslugu pretovara");
                return;
            }
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
            var select = "SELECT         RNPrijemVoza2.ID, RNPrijemVoza2.BrojKontejnera,   TipKontenjera.Naziv as VrstaKontejnera, RNPrijemVoza2.DatumRasporeda, RNPrijemVoza2.BrojKontejnera, RNPrijemVoza2.NalogIzdao, RNPrijemVoza2.DatumRealizacije, " +
"  Voz.BrVoza,RNPrijemVoza2.BrojPlombe, Partnerji_2.PaNaziv as UVoznik, CarinskiPostupak.NAziv, InspekciskiTretman.NAziv as Inspekciskitretman, " +
"  Partnerji_1.PaNAziv as Brodar, RNPrijemVoza2.VrstaRobe, " +
"  RNPrijemVoza2.USkladiste, RNPrijemVoza2.UPozicijuSklad, RNPrijemVoza2.IdUsluge, RNPrijemVoza2.NalogRealizovao, " +
"  RNPrijemVoza2.Napomena, RNPrijemVoza2.PrijemID, RNPrijemVoza2.NalogID " +
"  FROM RNPrijemVoza2 INNER JOIN " +
"  TipKontenjera ON RNPrijemVoza2.VrstaKontejnera = TipKontenjera.ID " +
 "                          INNER JOIN  Partnerji AS Partnerji_1 ON RNPrijemVoza2.NazivBrodara = Partnerji_1.PaSifra " +
"  INNER JOIN  Partnerji AS Partnerji_2 ON RNPrijemVoza2.Uvoznik = Partnerji_2.PaSifra " +
"  INNER JOIN  VrstaCarinskogPostupka AS CarinskiPostupak ON RNPrijemVoza2.CarinskiPostupak = CarinskiPostupak.id " +
"  INNER JOIN  Voz ON RNPrijemVoza2.SaVoznogSredstva = Voz.ID " +
"  inner join InspekciskiTretman on RNPrijemVoza2.InspekcijskiPregled = InspekciskiTretman.ID order by RNPrijemVoza2.ID desc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(select, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
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

            var vSredstvo = "Select Distinct ID, (Cast(BrVoza as nvarchar(10)) + '-' + Relacija) as IdVoza   From Voz";
            var daVS = new SqlDataAdapter(vSredstvo, conn);
            var dsVS = new DataSet();
            daVS.Fill(dsVS);
            cboSaSredstva.DataSource = dsVS.Tables[0];
            cboSaSredstva.DisplayMember = "IdVoza";
            cboSaSredstva.ValueMember = "ID";

            var partner2 = "Select PaSifra,PaNaziv From Partnerji order by PaSifra";
            var partAD2 = new SqlDataAdapter(partner2, conn);
            var partDS2 = new DataSet();
            partAD2.Fill(partDS2);
            cboUvoznik.DataSource = partDS2.Tables[0];
            cboUvoznik.DisplayMember = "PaNaziv";
            cboUvoznik.ValueMember = "PaSifra";

            var partner7 = "Select PaSifra,PaNaziv From Partnerji  order by PaSifra";
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


            var it = "select ID, Naziv from InspekciskiTretman order by Naziv";
            var itAD = new SqlDataAdapter(it, conn);
            var itDS = new DataSet();
            itAD.Fill(itDS);
            cboInspekcijski.DataSource = itDS.Tables[0];
            cboInspekcijski.DisplayMember = "Naziv";
            cboInspekcijski.ValueMember = "ID";

            var dir2 = "Select ID, (Oznaka + ' ' + Naziv) as Naziv from VrstaCarinskogPostupka order by Naziv";
            var dirAD2 = new SqlDataAdapter(dir2, conn);
            var dirDS2 = new DataSet();
            dirAD2.Fill(dirDS2);
            cboPostupak.DataSource = dirDS2.Tables[0];
            cboPostupak.DisplayMember = "Naziv";
            cboPostupak.ValueMember = "ID";
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
                rn.InsRNPPrijemVoza2(Convert.ToDateTime(txtDatumRasporeda.Value), txtbrojkontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cboVrstaKontejnera.SelectedValue),
                    txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(txtDatumRealizacije.Value), Convert.ToInt32(cboSaSredstva.SelectedValue), txtBrPlombe.Text.ToString().TrimEnd(),
                    Convert.ToInt32(cboUvoznik.SelectedValue), Convert.ToInt32(cboPostupak.SelectedValue), Convert.ToInt32(cboInspekcijski.SelectedValue), Convert.ToInt32(cboBrodar.SelectedValue),
                    Convert.ToInt32(cboVrstaRobe.SelectedValue), Convert.ToInt32(cboNaSklad.SelectedValue), Convert.ToInt32(cboNaPoz.SelectedValue), Convert.ToInt32(cboUsluga.SelectedValue),
                    "", txtNapomena.Text.ToString().TrimEnd());
            }
            else
            {
                rn.UpdRNPPrijemVoza2(Convert.ToInt32(txtID.Text), Convert.ToDateTime(txtDatumRasporeda.Value), txtbrojkontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cboVrstaKontejnera.SelectedValue),
                    txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(txtDatumRealizacije.Value), Convert.ToInt32(cboSaSredstva.SelectedValue), txtBrPlombe.Text.ToString().TrimEnd(),
                    Convert.ToInt32(cboUvoznik.SelectedValue), Convert.ToInt32(cboPostupak.SelectedValue), Convert.ToInt32(cboInspekcijski.SelectedValue), Convert.ToInt32(cboBrodar.SelectedValue),
                    Convert.ToInt32(cboVrstaRobe.SelectedValue), Convert.ToInt32(cboNaSklad.SelectedValue), Convert.ToInt32(cboNaPoz.SelectedValue), Convert.ToInt32(cboUsluga.SelectedValue),
                    txtNalogRealizovao.Text.ToString().TrimEnd(), txtNapomena.Text.ToString().TrimEnd());
            }
            FillGV();
            status = false;
        }
        private void tsDelete_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                InsertRN rn = new InsertRN();
                rn.DelRNPPrijemVoza2(Convert.ToInt32(txtID.Text));
                FillGV();
            }
            else
            {
                MessageBox.Show("Izaberite broj zapisa!");
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
        private void txtinspekcijskipregled_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Formirace se RN za sve Interne Naloge po Vrsti usluge ", "PRETOVAR", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                RadniNalozi.InsertRN ir = new InsertRN();
                ir.InsRNPPrijemVozaCeoVozPretovar(Convert.ToDateTime(txtDatumRasporeda.Value), txtNalogIzdao.Text, Convert.ToDateTime(txtDatumRealizacije.Text), Convert.ToInt32(cboSaSredstva.SelectedValue), Convert.ToInt32(cboNaSklad.SelectedValue), Convert.ToInt32(cboNaPoz.SelectedValue), Convert.ToInt32(cboUsluga.SelectedValue), "", txtNapomena.Text, Convert.ToInt32(txtPrijemID.Text),  Convert.ToInt32(txtNalogID.Text));
                FillGV();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }


           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillGVSamoPrijem();
        }

        private void FillGVSamoPrijem()
        {
            var select = "Select * from RNPrijemVoza2 order by ID desc";
            SqlConnection conn = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void RN3PrijemVoza2_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txtID.Text = row.Cells[0].Value.ToString();
                        VratiPodatkeStavke(txtID.Text);
                        FillDG2();
                    }
                }
            }
            catch { }
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

        private void VratiPodatkeStavke(string ID)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT [ID]       ,[DatumRasporeda] " +
     " ,[BrojKontejnera]      ,[VrstaKontejnera]      ,[NalogIzdao]      ,[DatumRealizacije] " +
     "  ,[SaVoznogSredstva]      ,[BrojPlombe]      ,[Uvoznik]      ,[CarinskiPostupak] " +
     "  ,[InspekcijskiPregled]      ,[NazivBrodara]      ,[VrstaRobe]      ,[USkladiste] " +
     "  ,[UPozicijuSklad]      ,[IdUsluge]      ,[NalogRealizovao]      ,[Napomena] " +
     "  ,[PrijemID]      ,[NalogID],Zavrsen  FROM [dbo].[RNPrijemVoza2] " +
             " where ID = " + txtID.Text, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtDatumRasporeda.Value = Convert.ToDateTime(dr["DatumRasporeda"].ToString());
                txtbrojkontejnera.Text = dr["BrojKontejnera"].ToString();
                cboVrstaKontejnera.SelectedValue = Convert.ToInt32(dr["VrstaKontejnera"].ToString()); 
                txtNalogIzdao.Text = dr["NalogIzdao"].ToString();
                txtDatumRealizacije.Value = Convert.ToDateTime(dr["DatumRealizacije"].ToString());
                cboSaSredstva.SelectedValue = Convert.ToInt32(dr["SaVoznogSredstva"].ToString());
                cboUvoznik.SelectedValue = Convert.ToInt32(dr["Uvoznik"].ToString());
                cboPostupak.SelectedValue = Convert.ToInt32(dr["CarinskiPostupak"].ToString());
                txtBrPlombe.Text = dr["BrojPlombe"].ToString();
                cboInspekcijski.SelectedValue = Convert.ToInt32(dr["InspekcijskiPregled"].ToString());
                cboBrodar.SelectedValue = Convert.ToInt32(dr["NazivBrodara"].ToString());
                cboVrstaRobe.SelectedValue = Convert.ToInt32(dr["VrstaRobe"].ToString());
                cboUsluga.SelectedValue = Convert.ToInt32(dr["IdUsluge"].ToString());
                txtNalogRealizovao.Text = dr["NalogRealizovao"].ToString();
                txtNalogID.Text = dr["NalogID"].ToString();
                txtPrijemID.Text = dr["PrijemID"].ToString();
                cboNaSklad.SelectedValue = Convert.ToInt32(dr["USkladiste"].ToString());
                cboNaPoz.SelectedValue = Convert.ToInt32(dr["UPozicijuSklad"].ToString());
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

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            InsertRN up = new InsertRN();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected == true)
                {
                    up.PotvrdiUradjenRN3(Convert.ToInt32(row.Cells[0].Value.ToString()));
                }

            }
        }
    }
}
