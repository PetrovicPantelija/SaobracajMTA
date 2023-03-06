using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace Saobracaj.Izvoz
{
    public partial class frmFormiranjePlanaIzvoz : Form
    {
        int pomPostojiPlan = 0;
        int pomPlan = 0;
        public string connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        public frmFormiranjePlanaIzvoz()
        {
            InitializeComponent();
        }

        public frmFormiranjePlanaIzvoz(int Plan)
        {
            InitializeComponent();
            pomPostojiPlan = 1;
            pomPlan = Plan;
        }

        private void VratiUkupanBrojKontejneraPrenetih()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Isnull(SUM(CASE WHEN TipKontejnera = 2 THEN 1 else 2 END),0) as BrojKontejnera from IzvozKonacna " +
            " where IDNadredjeni = " + cboPlanUtovara.SelectedValue, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                nmSpakovanih.Value = Convert.ToInt32(dr["BrojKontejnera"].ToString());
            }

            con.Close();

        }

        private void VratiUkupanBrojKontejneraPrenetihBezSerije()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select count(*) as BrojKontejnera from IzvozKonacna " +
            " where IDNadredjeni = " + cboPlanUtovara.SelectedValue, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtSpakovaniUB.Value = Convert.ToInt32(dr["BrojKontejnera"].ToString());
            }

            con.Close();

        }

        private void VratiUkupanBrojKontejnera()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
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
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
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

        private void RefreshDataGrid3()
        {
            var select = "  select SerijeKola.ID as Zapis, UvozKonacnaZaglavlje.IDVoza, VozSerijeKola.TipKontejnera as IDT, Naziv, Broj20 as Nosivost20, BrojSerija, (Broj20 * BrojSerija) as UkupnoPoSeriji  from VozSerijeKola " +
  " inner join IzvozKonacnaZaglavlje on IzvozKonacnaZaglavlje.IdVoza = VozSerijeKola.IDVoza " +
  "  inner join SerijeKola on SerijeKola.Id = VozSerijeKola.TipKontejnera where IzvozkonacnaZaglavlje.ID = " + Convert.ToInt32(cboPlanUtovara.SelectedValue);

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
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
        private void RefreshDataGrid1()
        {

            var select = " select Izvoz.ID, BrojVagona,  BrojKontejnera, TipKontenjera.SkNaziv from Izvoz " +
" inner join TipKontenjera on TipKontenjera.ID = Izvoz.VrstaKontejnera order by Izvoz.ID desc";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
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

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

        }

        private void RefreshDataGrid2()
        {
            var select = "    SELECT     IzvozKonacna.ID, IzvozKonacna.BrojVagona, IzvozKonacna.BrojKontejnera, IzvozKonacna.VrstaKontejnera AS VKID, TipKontenjera.Naziv AS TipKontejnera, IzvozKonacna.BrodskaPlomba, IzvozKonacna.OstalePlombe, " +
                "          IzvozKonacna.BookingBrodara, " +
              "            (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaManipulacije.Naziv as nvarchar(20))   FROM IzvozKonacnaVrstaManipulacije" +
  "     inner join VrstaManipulacije on VrstaManipulacije.ID = IzvozKonacnaVrstaManipulacije.IDVrstaManipulacije   where IzvozKonacnaVrstaManipulacije.IDNadredjena = IzvozKonacna.ID" +
 "     FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as VrsteUsluga,  " +
  "    (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaRobeHS.HSKod as nvarchar(20))   FROM IzvozKonacnaVrstaRobeHS " +
 "     inner join VrstaRobeHS on IzvozKonacnaVrstaRobeHS.IDVrstaRobeHS = VrstaRobeHS.ID   where IzvozKonacnaVrstaRobeHS.IDNadredjena = IzvozKonacna.ID " +
  "     FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as HS,  " +
 "      (SELECT  STUFF((SELECT distinct    '/' + Cast(NHM.Broj as nvarchar(20)) " +
 "     FROM IzvozKonacnaNHM  inner join NHM on IzvozKonacnaNHM.IDNHM = NHM.ID  where IzvozKonacnaNHM.IDNadredjena = IzvozKonacna.ID   FOR XML PATH('')), 1, 1, ''  ) As Skupljen) as NHM,  " +
                 "         IzvozKonacna.Brodar, Partnerji.PaNaziv AS Brodar, IzvozKonacna.CutOffPort, IzvozKonacna.NetoRobe, IzvozKonacna.BrutoRobe, IzvozKonacna.BrutoRobeO, IzvozKonacna.BrojKoleta, " +
                "          IzvozKonacna.BrojKoletaO, IzvozKonacna.CBM, IzvozKonacna.CBMO, IzvozKonacna.VrednostRobeFaktura, IzvozKonacna.Valuta, IzvozKonacna.KrajnaDestinacija AS KDID, KrajnjaDestinacija.Naziv AS KrajnjaNaZiv, " +
                 "         IzvozKonacna.Postupanje, VrstePostupakaUvoz.Naziv AS PostupanjeID, IzvozKonacna.MestoPreuzimanja, KontejnerskiTerminali.Naziv AS MestoPNaziv, " +
                 "         KontejnerskiTerminali.Oznaka AS MPOznaka, IzvozKonacna.Cirada, IzvozKonacna.PlaniraniDatumUtovara, IzvozKonacna.MesoUtovara, MestaUtovara.Naziv AS MestoUtovNaziv, " +
                 "         IzvozKonacna.KontaktOsoba, IzvozKonacna.MestoCarinjenja, Carinarnice.CINaziv, Carinarnice.CIOznaka, IzvozKonacna.Spedicija, Partnerji_1.PaNaziv AS SpedicijaNaziv, " +
                 "         IzvozKonacna.AdresaSlanjaStatusa, AdresaStatusVozila.Naziv AS AdresaNaziv, IzvozKonacna.NaslovSlanjaStatusa, NaslovStatusaVozila.Naziv AS Naslov, IzvozKonacna.EtaLeget, " +
                  "        IzvozKonacna.NapomenaReexport, VrstaCarinskogPostupka.Naziv AS ReeksportNaziv, VrstaCarinskogPostupka.Oznaka AS ReexportOznaka, IzvozKonacna.Inspekcija, " +
                  "        InspekciskiTretman.Naziv AS InspekcijaID, IzvozKonacna.AutoDana, IzvozKonacna.NajavaVozila, IzvozKonacna.NacinPakovanja, uvNacinPakovanja.Naziv AS NacinPakovanjaNaziv, " +
                  "        IzvozKonacna.NacinPretovara, IzvozKonacna.DodatneNapomeneDrumski, IzvozKonacna.Vaganje, IzvozKonacna.Tara, IzvozKonacna.VGMTezina, IzvozKonacna.VGMBrod, IzvozKonacna.IzvozKonacnanik, " +
                   "       Partnerji_2.PaNaziv AS IzvozKonacnanikNasl, IzvozKonacna.ADR, VrstaRobeADR.UNKod, VrstaRobeADR.Klasa, VrstaRobeADR.Naziv, IzvozKonacna.Klijent1, Partnerji_3.PaNaziv AS Klijent1, " +
                  "        IzvozKonacna.Napomena1REf, IzvozKonacna.DobijenNalogKlijent1, IzvozKonacna.Klijent2, Partnerji_4.PaNaziv AS KLijent2Naziv, IzvozKonacna.Napomena2REf, IzvozKonacna.Klijent3, " +
                 "         Partnerji_5.PaNaziv AS Klijent3Naziv, IzvozKonacna.Napomena3REf, IzvozKonacna.SpediterRijeka, Partnerji_6.PaNaziv AS SpediterRijekaNaziv " +
"    FROM         IzvozKonacna INNER JOIN " +
                    "      TipKontenjera ON IzvozKonacna.VrstaKontejnera = TipKontenjera.ID INNER JOIN " +
                    "      Partnerji ON IzvozKonacna.Brodar = Partnerji.PaSifra INNER JOIN " +
                   "       KrajnjaDestinacija ON IzvozKonacna.KrajnaDestinacija = KrajnjaDestinacija.ID INNER JOIN " +
                   "       VrstePostupakaUvoz ON IzvozKonacna.Postupanje = VrstePostupakaUvoz.ID INNER JOIN " +
                   "       MestaUtovara ON IzvozKonacna.MesoUtovara = MestaUtovara.ID INNER JOIN " +
                   "       Carinarnice ON IzvozKonacna.MestoCarinjenja = Carinarnice.ID INNER JOIN " +
                   "       Partnerji AS Partnerji_1 ON IzvozKonacna.Spedicija = Partnerji_1.PaSifra INNER JOIN " +
                   "       AdresaStatusVozila ON IzvozKonacna.AdresaSlanjaStatusa = AdresaStatusVozila.ID INNER JOIN " +
                   "       NaslovStatusaVozila ON IzvozKonacna.NaslovSlanjaStatusa = NaslovStatusaVozila.ID INNER JOIN " +
                   "       VrstaCarinskogPostupka ON IzvozKonacna.NapomenaReexport = VrstaCarinskogPostupka.id INNER JOIN " +
                   "       InspekciskiTretman ON IzvozKonacna.Inspekcija = InspekciskiTretman.ID INNER JOIN " +
                  "        uvNacinPakovanja ON IzvozKonacna.NacinPakovanja = uvNacinPakovanja.ID INNER JOIN " +
                   "       Partnerji AS Partnerji_2 ON Partnerji.PaSifra = Partnerji_2.PaSifra AND IzvozKonacna.IzvozKonacnanik = Partnerji_2.PaSifra INNER JOIN " +
                   "       VrstaRobeADR ON IzvozKonacna.ADR = VrstaRobeADR.ID INNER JOIN " +
                   "       Partnerji AS Partnerji_3 ON Partnerji.PaSifra = Partnerji_3.PaSifra AND IzvozKonacna.Klijent1 = Partnerji_3.PaSifra INNER JOIN " +
                   "       Partnerji AS Partnerji_4 ON Partnerji.PaSifra = Partnerji_4.PaSifra AND IzvozKonacna.Klijent2 = Partnerji_4.PaSifra INNER JOIN " +
                   "       Partnerji AS Partnerji_5 ON Partnerji.PaSifra = Partnerji_5.PaSifra AND IzvozKonacna.Klijent3 = Partnerji_5.PaSifra INNER JOIN " +
                   "       Partnerji AS Partnerji_6 ON Partnerji.PaSifra = Partnerji_6.PaSifra AND IzvozKonacna.SpediterRijeka = Partnerji_6.PaSifra Inner JOIN " +
                   "       KontejnerskiTerminali on  KontejnerskiTerminali.ID = IzvozKonacna.MestoPreuzimanja " +
            " where IzvozKonacna.IdNadredjeni = " + Convert.ToInt32(cboPlanUtovara.SelectedValue) + " order by IzvozKonacna.ID desc";
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

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    //Panta
                    InsertIzvozKonacna ins = new InsertIzvozKonacna();
                    ins.PrenesiUPlanUtovaraIzvoz(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(cboPlanUtovara.SelectedValue));

                }
            }
            RefreshDataGrid1();
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

        private void frmFormiranjePlanaIzvoz_Load(object sender, EventArgs e)
        {
            RefreshDataGrid1();
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
    }
}
