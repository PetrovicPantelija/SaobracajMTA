﻿using Syncfusion.Grouping;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;


namespace Saobracaj.Izvoz
{
    public partial class frmDopunaPlanaPraznimIzvoz : Syncfusion.Windows.Forms.Office2010Form
    {
        int pomPostojiPlan = 0;
        int pomPlan = 0;
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        public frmDopunaPlanaPraznimIzvoz()
        {
            InitializeComponent();
        }
        private void RefreshSync()
        {
            var select = " SELECT        KontejnerTekuce.Kontejner, KontejnerTekuce.StatusKontejnera, KontejnerStatus.Naziv, KontejnerTekuce.Skladiste, Skladista.Naziv AS SkladisteNaziv, "+ 
" KontejnerTekuce.Pokret, KontejnerTekuce.Stanje,    KontejnerTekuce.OSTECENJA, KontejnerTekuce.RLSRB, KontejnerTekuce.Kvalitet, KontejnerTekuce.CIR, "+
" KontejnerTekuce.StatusIzvoz, KontejnerTekuce.DatumGIN, KontejnerTekuce.TipKontejnera,      TipKontenjera.Naziv AS TipKontejnera, KontejnerTekuce.Brodar, " +
" Partnerji.PaNaziv as BrodarNAziv, Partnerji_1.PaSifra, Partnerji_1.PaNaziv AS Uvoznik, KontejnerTekuce.UlazniBroj, KontejnerTekuce.DatumOtpremaPlat,  " +
" KontejnerTekuce.DatumPrijemaPLat, KontejnerTekuce.OperacijaUradjena FROM            KontejnerTekuce" +
" INNER JOIN KontejnerStatus ON KontejnerTekuce.StatusKontejnera = KontejnerStatus.ID" +
" INNER JOIN      TipKontenjera ON KontejnerTekuce.TipKontejnera = TipKontenjera.ID" +
" INNER JOIN     Partnerji ON KontejnerTekuce.Brodar = Partnerji.PaSifra" +
" inner JOIN      Skladista on KontejnerTekuce.Skladiste = Skladista.ID" +
" INNER JOIN      Partnerji AS Partnerji_1 ON KontejnerTekuce.Uvoznik = Partnerji_1.PaSifra  where KontejnerTekuce.StatusKontejnera = 13";

            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            // dataGridView1.ReadOnly = true;
            gridGroupingControl1.DataSource = ds.Tables[0];
            gridGroupingControl1.ShowGroupDropArea = true;
            this.gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;
            foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }



        }

        private void VratiUkupanBrojKontejneraPrenetih()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Isnull(SUM(CASE WHEN VrstaKontejnera = 2 THEN 1 else 2 END),0) as BrojKontejnera from IzvozKonacna " +
            " where IDNadredjena = " + cboPlanUtovara.SelectedValue, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                nmSpakovanih.Value = Convert.ToInt32(dr["BrojKontejnera"].ToString());
            }

            con.Close();

        }

        private void VratiUkupanBrojKontejneraPrenetihBezSerije()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select count(*) as BrojKontejnera from IzvozKonacna " +
            " where IDNadredjena = " + cboPlanUtovara.SelectedValue, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtSpakovaniUB.Value = Convert.ToInt32(dr["BrojKontejnera"].ToString());
            }

            con.Close();

        }

        private void VratiUkupanBrojKontejnera()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select isnull(SUM(Broj20 * BrojSerija),0) as BrojKontejnera from VozSerijeKola " +
            " inner join SerijeKola on SerijeKola.Id = VozSerijeKola.TipKontejnera where IDVoza = (Select Top 1 IDVoza from IzvozKonacnaZaglavlje where ID = " + cboPlanUtovara.SelectedValue + " ) ", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                nmrUkupanBrojKontejnera.Value = Convert.ToInt32(dr["BrojKontejnera"].ToString());
            }

            con.Close();

        }

        private void VratiUkupanBrojKontejneraSumaSerija()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select isnull(BrojSerija,0) as BrojKontejnera from VozSerijeKola " +
            " inner join SerijeKola on SerijeKola.Id = VozSerijeKola.TipKontejnera where IDVoza = (Select Top 1 IDVoza from IzvozKonacnaZaglavlje where ID = " + cboPlanUtovara.SelectedValue + " ) ", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                nmrUkupanBrojKontejneraSS.Value = Convert.ToInt32(dr["BrojKontejnera"].ToString());
            }

            con.Close();

        }

        private void RefreshDataGrid2()
        {
            //TREBA STAVITI INNER JOIN
            var select = "      SELECT  IzvozKonacna.ID as ID,    IzvozKonacna.VrstaKontejnera, TipKontenjera.Naziv, IzvozKonacna.BrojVagona, IzvozKonacna.BrojKontejnera, IzvozKonacna.BrodskaPlomba, " +
" IzvozKonacna.OstalePlombe, IzvozKonacna.BookingBrodara,      Partnerji.PaNaziv,     IzvozKonacna.CutOffPort, IzvozKonacna.NetoRobe, IzvozKonacna.BrutoRobe, " +
"  IzvozKonacna.BrutoRobeO, IzvozKonacna.BrojKoleta, IzvozKonacna.BrojKoletaO, IzvozKonacna.CBM, IzvozKonacna.CBMO, " +
 " IzvozKonacna.VrednostRobeFaktura,   (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaManipulacije.Naziv as nvarchar(20))" +
"  FROM IzvozVrstaManipulacije           inner join VrstaManipulacije on VrstaManipulacije.ID = IzvozVrstaManipulacije.IDVrstaManipulacije" +
"  where IzvozVrstaManipulacije.IDNadredjena = IzvozKonacna.ID            FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as VrsteUsluga, " +
"  (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaRobeHS.HSKod as nvarchar(20))             FROM IzvozVrstaRobeHS " +
 " inner join VrstaRobeHS on IzvozVrstaRobeHS.IDVrstaRobeHS = VrstaRobeHS.ID" +
"  where IzvozVrstaRobeHS.IDNadredjena = IzvozKonacna.ID              FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as HS,  " +
"  (SELECT  STUFF((SELECT distinct    '/' + Cast(NHM.Broj as nvarchar(20))              FROM IzvozNHM  inner join NHM" +
 " on IzvozNHM.IDNHM = NHM.ID  where IzvozNHM.IDNadredjena = IzvozKonacna.ID   FOR XML PATH('')), 1, 1, ''  ) As Skupljen) as NHM,   " +
"  IzvozKonacna.Valuta, KrajnjaDestinacija.Naziv AS KrajnjaDestinacija, VrstePostupakaUvoz.Naziv AS Postupak, KontejnerskiTerminali.Naziv AS PPCNT,  " +
 " KontejnerskiTerminali.Oznaka, IzvozKonacna.Cirada, IzvozKonacna.PlaniraniDatumUtovara, MestaUtovara.Naziv AS MestoUtovara, IzvozKonacna.KontaktOsoba, " +
"  Carinarnice.Naziv AS Carinarnica, Carinarnice.CIOznaka, Partnerji_1.PaNaziv AS Spedicija, AdresaSlanjaStatusa AS AdresaStatusVozila,   " +
"  NaslovSlanjaStatusa AS NaslovStatusaVozila, IzvozKonacna.EtaLeget, VrstaCarinskogPostupka.Naziv AS Reexport, InspekciskiTretman.Naziv AS InspekciskiTretman,  " +
"  IzvozKonacna.AutoDana, IzvozKonacna.NajavaVozila, IzvozKonacna.DodatneNapomeneDrumski, IzvozKonacna.Vaganje, IzvozKonacna.VGMTezina, IzvozKonacna.Tara, " +
"  IzvozKonacna.VGMBrod,                     Partnerji_2.PaNaziv AS Izvoznik, Partnerji_3.PaNaziv AS Klijent1, IzvozKonacna.Napomena1REf,  " +
"  IzvozKonacna.DobijenNalogKlijent1, Partnerji_4.PaNaziv AS klijent2, " +
"  IzvozKonacna.Napomena2REf, Partnerji_5.PaNaziv AS Klijent3, IzvozKonacna.Napomena3REf, Partnerji_6.PaNaziv AS SpediterRijeka, uvNacinPakovanja.Naziv AS NacinPakovanja,  " +
"  IzvozKonacna.NacinPretovara FROM         IzvozKonacna LEFT JOIN TipKontenjera ON IzvozKonacna.VrstaKontejnera = TipKontenjera.ID Left JOIN " +
 " Partnerji ON IzvozKonacna.Brodar = Partnerji.PaSifra LEFT JOIN         KrajnjaDestinacija ON IzvozKonacna.KrajnaDestinacija = KrajnjaDestinacija.ID " +
 " LEFT JOIN         VrstePostupakaUvoz ON IzvozKonacna.Postupanje = VrstePostupakaUvoz.ID LEFT JOIN     KontejnerskiTerminali " +
 " ON IzvozKonacna.MestoPreuzimanja = KontejnerskiTerminali.id LEFT JOIN         MestaUtovara ON IzvozKonacna.MesoUtovara = MestaUtovara.ID " +
 " LEFT JOIN         Carinarnice ON IzvozKonacna.MestoCarinjenja = Carinarnice.ID Left JOIN        Partnerji AS Partnerji_1 " +
 " ON IzvozKonacna.Spedicija = Partnerji_1.PaSifra " +
" left JOIN         VrstaCarinskogPostupka ON IzvozKonacna.NapomenaReexport = VrstaCarinskogPostupka.id " +
 " left JOIN        InspekciskiTretman ON IzvozKonacna.Inspekcija = InspekciskiTretman.ID " +
 " left JOIN        Partnerji AS Partnerji_2 ON IzvozKonacna.Izvoznik = Partnerji_2.PaSifra " +
" left JOIN        Partnerji AS Partnerji_3 ON IzvozKonacna.Klijent1 = Partnerji_3.PaSifra " +
 " left JOIN       Partnerji AS Partnerji_4 ON IzvozKonacna.Klijent2 = Partnerji_4.PaSifra " +
 " left JOIN         Partnerji AS Partnerji_5 ON IzvozKonacna.Klijent3 = Partnerji_5.PaSifra " +
"  LEFT JOIN          Partnerji AS Partnerji_6 ON IzvozKonacna.SpediterRijeka = Partnerji_6.PaSifra " +
 " LEFT JOIN         uvNacinPakovanja ON IzvozKonacna.NacinPakovanja = uvNacinPakovanja.ID  " +
            " where IzvozKonacna.IdNadredjena = " + Convert.ToInt32(cboPlanUtovara.SelectedValue) + " order by IzvozKonacna.ID desc";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];


            dataGridView2.BorderStyle = BorderStyle.None;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView2.BackgroundColor = Color.White;

            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 50;
        }
        private void FillCombo()
        {
            var planutovara = "select IzvozKonacnaZaglavlje.ID, ( 'P:' + Cast(IzvozKonacnaZaglavlje.ID as nvarchar(4)) + ' T:' " +
 " + Cast(Convert(Nvarchar(10), Voz.VremePolaska, 104) as nvarchar(12)) + ' V:' + Cast(BrVoza as nvarchar(15)) + ' ' + Relacija) as Naziv " +
" from IzvozKonacnaZaglavlje " +
" inner join Voz on Voz.Id = IzvozKonacnaZaglavlje.IdVoza order by IzvozKonacnaZaglavlje.ID desc";
            var planutovaraSAD = new SqlDataAdapter(planutovara, connection);
            var planutovaraSDS = new DataSet();
            planutovaraSAD.Fill(planutovaraSDS);
            cboPlanUtovara.DataSource = planutovaraSDS.Tables[0];
            cboPlanUtovara.DisplayMember = "Naziv";
            cboPlanUtovara.ValueMember = "ID";

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.gridGroupingControl1.Table.SelectedRecords.Count > 0)
            {
                foreach (SelectedRecord selectedRecord in this.gridGroupingControl1.Table.SelectedRecords)
                {
                    InsertIzvozKonacna ins = new InsertIzvozKonacna();
                    ins.PrenesiUPlanUtovaraPrazan(selectedRecord.Record.GetValue("Kontejner").ToString(), Convert.ToInt32(cboPlanUtovara.SelectedValue));

                    //To get the cell value of particular column of selected records   
                    //  string cellValue = selectedRecord.Record.GetValue("ID").ToString();
                    // MessageBox.Show(cellValue);
                }
            }

            RefreshSync();
            // RefreshDataGrid1();
            RefreshDataGrid2();
            VratiUkupanBrojKontejnera();
            VratiUkupanBrojKontejneraPrenetih();
            VratiUkupanBrojKontejneraPrenetihBezSerije();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefreshDataGrid2();
            RefreshDataGrid3();
            VratiUkupanBrojKontejnera();
            VratiUkupanBrojKontejneraSumaSerija();
            VratiUkupanBrojKontejneraPrenetih();
            VratiUkupanBrojKontejneraPrenetihBezSerije();
        }

        private void frmDopunaPlanaPraznimIzvoz_Load(object sender, EventArgs e)
        {
            // RefreshDataGrid1();
            RefreshSync();
            RefreshDataGrid2();
            FillCombo();
            if (pomPostojiPlan == 1)
            {
                cboPlanUtovara.SelectedValue = pomPlan;
                RefreshDataGrid2();
                RefreshDataGrid3();
                VratiUkupanBrojKontejnera();
                VratiUkupanBrojKontejneraSumaSerija();
                VratiUkupanBrojKontejneraPrenetih();
                VratiUkupanBrojKontejneraPrenetihBezSerije();

            }
        }
        private void RefreshDataGrid3()
        {
            var select = "  select SerijeKola.ID as Zapis, IzvozKonacnaZaglavlje.IDVoza, VozSerijeKola.TipKontejnera as IDT, Naziv, Broj20 as Nosivost20, BrojSerija, (Broj20 * BrojSerija) as UkupnoPoSeriji  from VozSerijeKola " +
  " inner join IzvozKonacnaZaglavlje on IzvozKonacnaZaglavlje.IdVoza = VozSerijeKola.IDVoza " +
  "  inner join SerijeKola on SerijeKola.Id = VozSerijeKola.TipKontejnera where IzvozkonacnaZaglavlje.ID = " + Convert.ToInt32(cboPlanUtovara.SelectedValue);

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
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


            DataGridViewColumn column = dataGridView3.Columns[0];
            dataGridView3.Columns[0].HeaderText = "ID";
            dataGridView3.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView3.Columns[1];
            dataGridView3.Columns[1].HeaderText = "ID Voza";
            dataGridView3.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView3.Columns[2];
            dataGridView3.Columns[2].HeaderText = "TSV";
            dataGridView3.Columns[2].Width = 40;

            DataGridViewColumn column4 = dataGridView3.Columns[3];
            dataGridView3.Columns[3].HeaderText = "Naziv serije";
            dataGridView3.Columns[3].Width = 80;

            DataGridViewColumn column5 = dataGridView3.Columns[4];
            dataGridView3.Columns[4].HeaderText = "Nosivost 20 ST";
            dataGridView3.Columns[4].Width = 60;

            DataGridViewColumn column6 = dataGridView3.Columns[5];
            dataGridView3.Columns[5].HeaderText = "Broj Serija";
            dataGridView3.Columns[5].Width = 60;

            DataGridViewColumn column7 = dataGridView3.Columns[6];
            dataGridView3.Columns[6].HeaderText = "Ukupno po Seriji";
            dataGridView3.Columns[6].Width = 70;

        }
        private void gridGroupingControl1_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            try
            {
                if (gridGroupingControl1.Table.CurrentRecord != null)
                {
                    txtSifra.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("Kontejner").ToString();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
