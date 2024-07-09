using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Syncfusion.Grouping;

namespace Saobracaj.Izvoz
{
    public partial class frmIzvoz : Syncfusion.Windows.Forms.Office2010Form
    {
        float firstWidth;
        float firstHeight;
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        string tKorisnik = "";
        int NHMObrni = 0;
        int PlanZaPrebacivanje = 0;

        public frmIzvoz()
        {
            InitializeComponent();
        }
        public frmIzvoz(string Korisnik)
        {
            InitializeComponent();
            tslKreirao.Text = Korisnik;
            tKorisnik = Korisnik;
        }

        public frmIzvoz(int Terminal, int PlanTerminala)
        {
            InitializeComponent();
            // FillDG();

            FillCombo();
            chkTerminal.Checked = true;
            PlanZaPrebacivanje = PlanTerminala;

            //  FillDG4();

        }

        public frmIzvoz(int ID)
        {
            InitializeComponent();
           // FillDG();
          
            FillCombo();
            VratiPodatke(ID);
            FillDG2();
            FillDG3();
          //  FillDG4();

        }

        public void VratiPodatke(int ID)
        {
            
                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection con = new SqlConnection(s_connection);

                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT [ID]      ,[BrojVagona] " +
                 " ,[BrojKontejnera]      ,[VrstaKontejnera]      ,[BrodskaPlomba], [OstalePlombe] , [BookingBrodara] " +
                 " ,[Brodar]      ,[CutOffPort]      ,[NetoRobe]      ,[BrutoRobe] " +
                 " ,[BrutoRobeO]      ,[BrojKoleta]      ,[BrojKoletaO]      ,[CBM] " +
                 " ,[CBMO]      ,[VrednostRobeFaktura]      ,[Valuta]      ,[KrajnaDestinacija] " +
                 " ,[Postupanje]      ,[MestoPreuzimanja]      ,[Cirada]      ,[PlaniraniDatumUtovara] " +
                 " ,[MesoUtovara]      ,[KontaktOsoba]      ,[MestoCarinjenja]      ,[Spedicija] " +
                 " ,[AdresaSlanjaStatusa]      ,[NaslovSlanjaStatusa]      ,[EtaLeget]      ,[NapomenaReexport] " +
                 " ,[Inspekcija]      ,[AutoDana]      ,[NajavaVozila]      ,[NacinPakovanja] " +
                 " ,[NacinPretovara]      ,[DodatneNapomeneDrumski]      ,[Vaganje]      ,[VGMTezina] " +
                 " ,[Tara]      ,[VGMBrod]      ,[Izvoznik]      ,[Klijent1] " +
                 " ,[Napomena1REf]      ,[DobijenNalogKlijent1]      ,[Klijent2]      ,[Napomena2REf] " +
                 " ,[Klijent3]      ,[Napomena3REf]      ,[SpediterRijeka] , ADR , Korisnik, DatumKreiranja  " +
                 "  FROM [Izvoz] where ID=" + ID, con);
               
            SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    txtID.Text = dr["ID"].ToString();
                    txtBrojVagona.Text = dr["BrojVagona"].ToString();
                    txtBrKont.Text = dr["BrojKontejnera"].ToString();
                    txtTipKont.SelectedValue = Convert.ToInt32(dr["VrstaKontejnera"].ToString());
                    txtBrodskaPlomba.Text = dr["BrodskaPlomba"].ToString();
                    txtOstalePlombe.Text = dr["OstalePlombe"].ToString();
                    txtBokingBrodara.Text = dr["BookingBrodara"].ToString();
                   cboBrodar.SelectedValue = Convert.ToInt32(dr["Brodar"].ToString());
                dtpCutOffPort.Value = Convert.ToDateTime(dr["CutOffPort"].ToString());
                    txtNetoR.Value = Convert.ToDecimal(dr["NetoRobe"].ToString());
                    txtBrutoR.Value = Convert.ToDecimal(dr["BrutoRobe"].ToString());
                    txtBrutoO.Value = Convert.ToDecimal(dr["BrutoRobeO"].ToString());
                txtKoleta.Value = Convert.ToInt32(dr["BrojKoleta"].ToString());
                txtKoletaO.Value = Convert.ToInt32(dr["BrojKoletaO"].ToString());
                txtCBM.Value = Convert.ToDecimal(dr["CBM"].ToString());
                txtCBMO.Value = Convert.ToDecimal(dr["CBMO"].ToString());
                txtADR.SelectedValue = Convert.ToInt32(dr["ADR"].ToString());
                    txtVrednostRobeFaktura.Value = Convert.ToDecimal(dr["VrednostRobeFaktura"].ToString());
                txtValuta.Text = dr["Valuta"].ToString();
                cboKrajnjaDestinacija.SelectedValue = Convert.ToInt32(dr["KrajnaDestinacija"].ToString());
                cboPostupanjeSaRobom.SelectedValue = Convert.ToInt32(dr["Postupanje"].ToString());
                cboPPCNT.SelectedValue = Convert.ToInt32(dr["MestoPreuzimanja"].ToString());

                if (dr["Cirada"].ToString() == "1")
                { chkCirada.Text = "CIRADA"; }
                else
                { chkCirada.Text =  "PLATFORMA";  }

                dtpPlanUtovara.Value =  Convert.ToDateTime(dr["PlaniraniDatumUtovara"].ToString()); 
                cboMestoUtovara.SelectedValue = Convert.ToInt32(dr["MesoUtovara"].ToString());
                txtKontaktOsoba.SelectedValue = Convert.ToInt32(dr["KontaktOsoba"].ToString());
                cboCarina.SelectedValue = Convert.ToInt32(dr["MestoCarinjenja"].ToString());
                cboSpedicija.SelectedValue = Convert.ToInt32(dr["Spedicija"].ToString());
               // txtKontaktSpeditera
                    cboAdresaStatusVozila.Text = dr["AdresaSlanjaStatusa"].ToString();
                cboNaslovStatusaVozila.Text = dr["NaslovSlanjaStatusa"].ToString();
                dtpEtaLeget.Value = Convert.ToDateTime(dr["EtaLeget"].ToString());
                cboReexport.SelectedValue = Convert.ToInt32(dr["NapomenaReexport"].ToString());
                cboInspekciskiTretman.SelectedValue = Convert.ToInt32(dr["Inspekcija"].ToString());
                txtAutoDana.Value= Convert.ToDecimal(dr["AutoDana"].ToString());
                

                     if (dr["NajavaVozila"].ToString() == "1")
                { chkNajavaVozila.Checked = true; }
                else
                { chkNajavaVozila.Checked = false; }
                cbNacinPakovanja.SelectedValue = Convert.ToInt32(dr["NacinPakovanja"].ToString());
                if (dr["NacinPretovara"].ToString() == "1")
                { chkNacinPretovara.Checked = true;
                    chkIndirektno.Checked = false;
                
                }
                else
                { chkNacinPretovara.Checked = false;
                    chkIndirektno.Checked = true;
                }

                txtDodatneNapomene.Text = dr["DodatneNapomeneDrumski"].ToString();

                if (dr["Vaganje"].ToString() == "1")
                { chkVaganje.Checked = true; }
                else
                { chkVaganje.Checked = false; }
         
                txtOdvaganaTezina.Value = Convert.ToDecimal(dr["VGMTezina"].ToString());
                txtTaraKontejnera.Value = Convert.ToDecimal(dr["Tara"].ToString());
                txVGMBrodBruto.Value= Convert.ToDecimal(dr["VGMBrod"].ToString());
                cboIzvoznik.SelectedValue = Convert.ToInt32(dr["Izvoznik"].ToString());
                cboNalogodavac1.SelectedValue = Convert.ToInt32(dr["Klijent1"].ToString());
                txtRef1.Text = dr["Napomena1REf"].ToString();
                cboNalogodavac2.SelectedValue = Convert.ToInt32(dr["Klijent2"].ToString());
                txtRef2.Text = dr["Napomena2REf"].ToString();
                cboNalogodavac3.SelectedValue = Convert.ToInt32(dr["Klijent3"].ToString());
                txtRef3.Text = dr["Napomena3REf"].ToString();
                cboSpediterURijeci.SelectedValue = Convert.ToInt32(dr["SpediterRijeka"].ToString());


                tslKreirao.Text = dr["Kreirao"].ToString();
                tslDatum.Text = dr["DatumKreiranja"].ToString();






            }
                con.Close();
        }

      
        /*
          private void FillGV()
        {
            var select = " SELECT     Izvoz.VrstaKontejnera, TipKontenjera.Naziv, Izvoz.ID, Izvoz.BrojVagona, Izvoz.BrojKontejnera, Izvoz.BrodskaPlomba, Izvoz.OstalePlombe, Izvoz.BookingBrodara, Partnerji.PaNaziv, " +
                  "    Izvoz.CutOffPort, Izvoz.NetoRobe, Izvoz.BrutoRobe, Izvoz.BrutoRobeO, Izvoz.BrojKoleta, Izvoz.BrojKoletaO, Izvoz.CBM, Izvoz.CBMO, Izvoz.VrednostRobeFaktura, " +
                   "  (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaManipulacije.Naziv as nvarchar(20))   FROM IzvozVrstaManipulacije " +
   "       inner join VrstaManipulacije on VrstaManipulacije.ID = IzvozVrstaManipulacije.IDVrstaManipulacije   where IzvozVrstaManipulacije.IDNadredjena = Izvoz.ID " +
   "        FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as VrsteUsluga,   " +
   "     (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaRobeHS.HSKod as nvarchar(20))   FROM IzvozVrstaRobeHS " +
   "       inner join VrstaRobeHS on IzvozVrstaRobeHS.IDVrstaRobeHS = VrstaRobeHS.ID   where IzvozVrstaRobeHS.IDNadredjena = Izvoz.ID " +
   "        FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as HS,   " +
   "    (SELECT  STUFF((SELECT distinct    '/' + Cast(NHM.Broj as nvarchar(20)) " +
  "            FROM IzvozNHM  inner join NHM on IzvozNHM.IDNHM = NHM.ID  where IzvozNHM.IDNadredjena = Izvoz.ID   FOR XML PATH('')), 1, 1, ''  ) As Skupljen) as NHM, " +
                  "        Izvoz.Valuta, KrajnjaDestinacija.Naziv AS KrajnjaDestinacija, VrstePostupakaUvoz.Naziv AS Postupak, KontejnerskiTerminali.Naziv AS PPCNT, " +
                  "        KontejnerskiTerminali.Oznaka, Izvoz.Cirada, Izvoz.PlaniraniDatumUtovara, MestaUtovara.Naziv AS MestoUtovara, Izvoz.KontaktOsoba,  " +
                  "        Carinarnice.Naziv AS Carinarnica, Carinarnice.CIOznaka, Partnerji_1.PaNaziv AS Spedicija, AdresaStatusVozila.Naziv AS AdresaStatusVozila, " +
                  "        NaslovStatusaVozila.Naziv AS NaslovStatusaVozila, Izvoz.EtaLeget, VrstaCarinskogPostupka.Naziv AS Reexport, InspekciskiTretman.Naziv AS InspekciskiTretman, " +
                  "        Izvoz.AutoDana, Izvoz.NajavaVozila, Izvoz.DodatneNapomeneDrumski, Izvoz.Vaganje, Izvoz.VGMTezina, Izvoz.Tara, Izvoz.VGMBrod, " +
                  "        Partnerji_2.PaNaziv AS Izvoznik, Partnerji_3.PaNaziv AS Klijent1, Izvoz.Napomena1REf, Izvoz.DobijenNalogKlijent1, Partnerji_4.PaNaziv AS klijent2, " +
                  "        Izvoz.Napomena2REf, Partnerji_5.PaNaziv AS Klijent3, Izvoz.Napomena3REf, Partnerji_6.PaNaziv AS SpediterRijeka, uvNacinPakovanja.Naziv AS NacinPakovanja, " +
                  "        Izvoz.NacinPretovara " +
"    FROM         Izvoz INNER JOIN " +
                   "       TipKontenjera ON Izvoz.VrstaKontejnera = TipKontenjera.ID INNER JOIN " +
                  "        Partnerji ON Izvoz.Brodar = Partnerji.PaSifra INNER JOIN " +
                  "        KrajnjaDestinacija ON Izvoz.KrajnaDestinacija = KrajnjaDestinacija.ID INNER JOIN " +
                  "        VrstePostupakaUvoz ON Izvoz.Postupanje = VrstePostupakaUvoz.ID INNER JOIN " +
                  "        KontejnerskiTerminali ON Izvoz.MestoPreuzimanja = KontejnerskiTerminali.id INNER JOIN " +
                  "        MestaUtovara ON Izvoz.MesoUtovara = MestaUtovara.ID INNER JOIN " +
                  "        Carinarnice ON Izvoz.MestoCarinjenja = Carinarnice.ID INNER JOIN " +
                   "       Partnerji AS Partnerji_1 ON Izvoz.Spedicija = Partnerji_1.PaSifra INNER JOIN " +
                   "       AdresaStatusVozila ON Izvoz.AdresaSlanjaStatusa = AdresaStatusVozila.ID INNER JOIN " +
                   "       NaslovStatusaVozila ON Izvoz.NaslovSlanjaStatusa = NaslovStatusaVozila.ID INNER JOIN " +
                  "        VrstaCarinskogPostupka ON Izvoz.NapomenaReexport = VrstaCarinskogPostupka.id INNER JOIN " +
                   "       InspekciskiTretman ON Izvoz.Inspekcija = InspekciskiTretman.ID INNER JOIN " +
                   "       Partnerji AS Partnerji_2 ON Izvoz.Izvoznik = Partnerji_2.PaSifra INNER JOIN " +
                   "       Partnerji AS Partnerji_3 ON Izvoz.Klijent1 = Partnerji_3.PaSifra INNER JOIN " +
                    "      Partnerji AS Partnerji_4 ON Izvoz.Klijent2 = Partnerji_4.PaSifra INNER JOIN " +
                  "        Partnerji AS Partnerji_5 ON Izvoz.Klijent3 = Partnerji_5.PaSifra INNER JOIN " +
                 "         Partnerji AS Partnerji_6 ON Izvoz.SpediterRijeka = Partnerji_6.PaSifra INNER JOIN " +
                  "        uvNacinPakovanja ON Izvoz.NacinPakovanja = uvNacinPakovanja.ID order by Izvoz.ID desc ";

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
            dataGridView1.Columns[0].Frozen = true;
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "BrojKontejnera";
            dataGridView1.Columns[1].Frozen = true;
            dataGridView1.Columns[1].Width = 100;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "BL";
            dataGridView1.Columns[2].Frozen = true;
            dataGridView1.Columns[2].Width = 90;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Dobijen_Nalog_Brodara";
            // dataGridView1.Columns[1].Frozen = true;
            dataGridView1.Columns[3].Width = 50;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "ATABroda";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column5 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Brod";
            dataGridView1.Columns[5].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Napomena";
            dataGridView1.Columns[6].Width = 100;

            DataGridViewColumn column7 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "DatumBZ";
            dataGridView1.Columns[7].Width = 80;

            DataGridViewColumn column8 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "PIN";
            dataGridView1.Columns[8].Width = 60;

            DataGridViewColumn column9 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "BrojKontejnera";
            //   dataGridView1.Columns[7].Frozen = true;
            dataGridView1.Columns[9].Width = 100;

            DataGridViewColumn column10 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Vrsta kontejnera";
            dataGridView1.Columns[10].Width = 120;

            DataGridViewColumn column11 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "R_L_SRB";
            dataGridView1.Columns[11].Width = 120;

            DataGridViewColumn column12 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Dirigacija_Kontejnera_Za";
            dataGridView1.Columns[12].Width = 100;

            DataGridViewColumn column13 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "BL";
            // dataGridView1.Columns[13].Frozen = true;
            dataGridView1.Columns[13].Width = 90;

           // RefreshDataGridColor();


        }
         */

        private void frmIzvoz_Load(object sender, EventArgs e)
        {
            FillCombo();
          //  FillDG();
            firstWidth = this.Size.Width;
            firstHeight = this.Size.Height;
        }

        private void FillDG()
        {
            var select = " SELECT  Izvoz.ID as ID,    Izvoz.VrstaKontejnera, TipKontenjera.Naziv, Izvoz.BrojVagona, Izvoz.BrojKontejnera, Izvoz.BrodskaPlomba, Izvoz.OstalePlombe, Izvoz.BookingBrodara, Partnerji.PaNaziv, " +
                  "    Izvoz.CutOffPort, Izvoz.NetoRobe, Izvoz.BrutoRobe, Izvoz.BrutoRobeO, Izvoz.BrojKoleta, Izvoz.BrojKoletaO, Izvoz.CBM, Izvoz.CBMO, Izvoz.VrednostRobeFaktura, " +
                   "  (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaManipulacije.Naziv as nvarchar(20))   FROM IzvozVrstaManipulacije " +
   "       inner join VrstaManipulacije on VrstaManipulacije.ID = IzvozVrstaManipulacije.IDVrstaManipulacije   where IzvozVrstaManipulacije.IDNadredjena = Izvoz.ID " +
   "        FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as VrsteUsluga,   " +
 "     (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaRobeHS.HSKod as nvarchar(20))   FROM IzvozVrstaRobeHS " +
   "       inner join VrstaRobeHS on IzvozVrstaRobeHS.IDVrstaRobeHS = VrstaRobeHS.ID   where IzvozVrstaRobeHS.IDNadredjena = Izvoz.ID " +
   "        FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as HS,   " +
   "    (SELECT  STUFF((SELECT distinct    '/' + Cast(NHM.Broj as nvarchar(20)) " +
  "            FROM IzvozNHM  inner join NHM on IzvozNHM.IDNHM = NHM.ID  where IzvozNHM.IDNadredjena = Izvoz.ID   FOR XML PATH('')), 1, 1, ''  ) As Skupljen) as NHM, " +
                  "        Izvoz.Valuta, KrajnjaDestinacija.Naziv AS KrajnjaDestinacija, VrstePostupakaUvoz.Naziv AS Postupak, KontejnerskiTerminali.Naziv AS PPCNT, " +
                  "        KontejnerskiTerminali.Oznaka, Izvoz.Cirada, Izvoz.PlaniraniDatumUtovara, MestaUtovara.Naziv AS MestoUtovara, Izvoz.KontaktOsoba,  " +
                  "        Carinarnice.Naziv AS Carinarnica, Carinarnice.CIOznaka, Partnerji_1.PaNaziv AS Spedicija,  AdresaSlanjaStatusa, " +
                  "          NaslovSlanjaStatusa, Izvoz.EtaLeget, VrstaCarinskogPostupka.Naziv AS Reexport, InspekciskiTretman.Naziv AS InspekciskiTretman, " +
                  "        Izvoz.AutoDana, Izvoz.NajavaVozila, Izvoz.DodatneNapomeneDrumski, Izvoz.Vaganje, Izvoz.VGMTezina, Izvoz.Tara, Izvoz.VGMBrod, " +
                  "        Partnerji_2.PaNaziv AS Izvoznik, Partnerji_3.PaNaziv AS Klijent1, Izvoz.Napomena1REf, Izvoz.DobijenNalogKlijent1, Partnerji_4.PaNaziv AS klijent2, " +
                  "        Izvoz.Napomena2REf, Partnerji_5.PaNaziv AS Klijent3, Izvoz.Napomena3REf, Partnerji_6.PaNaziv AS SpediterRijeka, uvNacinPakovanja.Naziv AS NacinPakovanja, " +
                  "        Izvoz.NacinPretovara, Izvoz.VGMBrod2 " +
"    FROM         Izvoz Left JOIN " +
                   "       TipKontenjera ON Izvoz.VrstaKontejnera = TipKontenjera.ID LEFT JOIN " +
                  "        Partnerji ON Izvoz.Brodar = Partnerji.PaSifra LEFT JOIN " +
                  "        KrajnjaDestinacija ON Izvoz.KrajnaDestinacija = KrajnjaDestinacija.ID LEFT JOIN " +
                  "        VrstePostupakaUvoz ON Izvoz.Postupanje = VrstePostupakaUvoz.ID LEFT JOIN " +
                  "        KontejnerskiTerminali ON Izvoz.MestoPreuzimanja = KontejnerskiTerminali.id LEFT JOIN " +
                  "        MestaUtovara ON Izvoz.MesoUtovara = MestaUtovara.ID LEFT JOIN " +
                  "        Carinarnice ON Izvoz.MestoCarinjenja = Carinarnice.ID LEFT JOIN " +
                   "       Partnerji AS Partnerji_1 ON Izvoz.Spedicija = Partnerji_1.PaSifra LEFT JOIN " +
                  "        VrstaCarinskogPostupka ON Izvoz.NapomenaReexport = VrstaCarinskogPostupka.id LEFT JOIN " +
                   "       InspekciskiTretman ON Izvoz.Inspekcija = InspekciskiTretman.ID LEFT JOIN " +
                   "       Partnerji AS Partnerji_2 ON Izvoz.Izvoznik = Partnerji_2.PaSifra LEFT JOIN " +
                   "       Partnerji AS Partnerji_3 ON Izvoz.Klijent1 = Partnerji_3.PaSifra LEFT JOIN " +
                    "      Partnerji AS Partnerji_4 ON Izvoz.Klijent2 = Partnerji_4.PaSifra LEFT JOIN " +
                  "        Partnerji AS Partnerji_5 ON Izvoz.Klijent3 = Partnerji_5.PaSifra LEFT JOIN " +
                 "         Partnerji AS Partnerji_6 ON Izvoz.SpediterRijeka = Partnerji_6.PaSifra LEFT JOIN " +
                  "        uvNacinPakovanja ON Izvoz.NacinPakovanja = uvNacinPakovanja.ID order by Izvoz.ID desc ";


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
            dataGridView1.Columns[0].Frozen = true;
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "BrojKontejnera";
            dataGridView1.Columns[1].Frozen = true;
            dataGridView1.Columns[1].Width = 100;
            /*
            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "BL";
            dataGridView1.Columns[2].Frozen = true;
            dataGridView1.Columns[2].Width = 90;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Dobijen_Nalog_Brodara";
            // dataGridView1.Columns[1].Frozen = true;
            dataGridView1.Columns[3].Width = 50;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "ATABroda";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column5 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Brod";
            dataGridView1.Columns[5].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Napomena";
            dataGridView1.Columns[6].Width = 100;

            DataGridViewColumn column7 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "DatumBZ";
            dataGridView1.Columns[7].Width = 80;

            DataGridViewColumn column8 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "PIN";
            dataGridView1.Columns[8].Width = 60;

            DataGridViewColumn column9 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "BrojKontejnera";
            //   dataGridView1.Columns[7].Frozen = true;
            dataGridView1.Columns[9].Width = 100;

            DataGridViewColumn column10 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Vrsta kontejnera";
            dataGridView1.Columns[10].Width = 120;

            DataGridViewColumn column11 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "R_L_SRB";
            dataGridView1.Columns[11].Width = 120;

            DataGridViewColumn column12 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Dirigacija_Kontejnera_Za";
            dataGridView1.Columns[12].Width = 100;

            DataGridViewColumn column13 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "BL";
            // dataGridView1.Columns[13].Frozen = true;
            dataGridView1.Columns[13].Width = 90;

            RefreshDataGridColor();
*/
        }



        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connection);

            var tipkontejnera = "Select ID, SkNaziv From TipKontenjera order by SkNaziv";
            var tkAD = new SqlDataAdapter(tipkontejnera, conn);
            var tkDS = new DataSet();
            tkAD.Fill(tkDS);
            txtTipKont.DataSource = tkDS.Tables[0];
            txtTipKont.DisplayMember = "SkNaziv";
            txtTipKont.ValueMember = "ID";

            var bro = "Select PaSifra,PaNaziv From Partnerji where Brodar = 1 order by PaNaziv";
            var broAD = new SqlDataAdapter(bro, conn);
            var broDS = new DataSet();
            broAD.Fill(broDS);
            cboBrodar.DataSource = broDS.Tables[0];
            cboBrodar.DisplayMember = "PaNaziv";
            cboBrodar.ValueMember = "PaSifra";


            var adr = "Select ID, (  UNKod +' - '+ Klasa + ' - ' + Naziv  ) as Naziv From VrstaRobeADR order by UNKod";
            var adrSAD = new SqlDataAdapter(adr, conn);
            var adrSDS = new DataSet();
            adrSAD.Fill(adrSDS);
            txtADR.DataSource = adrSDS.Tables[0];
            txtADR.DisplayMember = "Naziv";
            txtADR.ValueMember = "ID";

            //Krajnja destinacija - Kad napravim sifarnik
            var kd = "Select ID, Naziv as Naziv From KrajnjaDestinacija order by ID";
            var kdSAD = new SqlDataAdapter(kd, conn);
            var kdSDS = new DataSet();
            kdSAD.Fill(kdSDS);
            cboKrajnjaDestinacija.DataSource = kdSDS.Tables[0];
            cboKrajnjaDestinacija.DisplayMember = "Naziv";
            cboKrajnjaDestinacija.ValueMember = "ID";

            var dir3 = "Select ID,Naziv from VrstePostupakaUvoz order by Naziv";
            var dirAD3 = new SqlDataAdapter(dir3, conn);
            var dirDS3 = new DataSet();
            dirAD3.Fill(dirDS3);
            cboPostupanjeSaRobom.DataSource = dirDS3.Tables[0];
            cboPostupanjeSaRobom.DisplayMember = "Naziv";
            cboPostupanjeSaRobom.ValueMember = "ID";

            var rl = "Select ID, (Naziv + ' - ' + Oznaka) as Naziv From KontejnerskiTerminali order by (Naziv + ' ' + Oznaka)";
            var rlSAD = new SqlDataAdapter(rl, conn);
            var rlSDS = new DataSet();
            rlSAD.Fill(rlSDS);
            cboPPCNT.DataSource = rlSDS.Tables[0];
            cboPPCNT.DisplayMember = "Naziv";
            cboPPCNT.ValueMember = "ID";

           
            //Mesta utovara u Srbiji - Dodati
            var dir = "Select ID,Naziv from MestaUtovara order by Naziv";
            var dirAD = new SqlDataAdapter(dir, conn);
            var dirDS = new DataSet();
            dirAD.Fill(dirDS);
            cboMestoUtovara.DataSource = dirDS.Tables[0];
            cboMestoUtovara.DisplayMember = "Naziv";
            cboMestoUtovara.ValueMember = "ID";

            var car = "Select ID, Naziv From Carinarnice order by Naziv";
            var carAD = new SqlDataAdapter(car, conn);
            var carDS = new DataSet();
            carAD.Fill(carDS);
            cboCarina.DataSource = carDS.Tables[0];
            cboCarina.DisplayMember = "Naziv";
            cboCarina.ValueMember = "ID";
            //Spediter
            var partner3 = "Select PaSifra,PaNaziv From Partnerji where Spediter =1 order by PaNaziv";
            var partAD3 = new SqlDataAdapter(partner3, conn);
            var partDS3 = new DataSet();
            partAD3.Fill(partDS3);
            cboSpedicija.DataSource = partDS3.Tables[0];
            cboSpedicija.DisplayMember = "PaNaziv";
            cboSpedicija.ValueMember = "PaSifra";

            //Spedicija
            var partner4 = "Select PaSifra,PaNaziv From Partnerji where Spediter =1 order by PaNaziv";
            var partAD4 = new SqlDataAdapter(partner3, conn);
            var partDS4 = new DataSet();
            partAD4.Fill(partDS4);
            cboSpedicijaJ.DataSource = partDS4.Tables[0];
            cboSpedicijaJ.DisplayMember = "PaNaziv";
            cboSpedicijaJ.ValueMember = "PaSifra";


            //Adresa statusa vozila
            /*
            var dir5 = "Select PaKOZapSt, (Rtrim(PaKOIme) + ' ' + Rtrim(PaKOPriimek)) as Naziv  from partnerjiKontOseba ";
            var dirAD5 = new SqlDataAdapter(dir5, conn);
            var dirDS5 = new DataSet();
            dirAD5.Fill(dirDS5);
            cboAdresaStatusVozila.DataSource = dirDS5.Tables[0];
            cboAdresaStatusVozila.DisplayMember = "Naziv";
            cboAdresaStatusVozila.ValueMember = "PaKOZapSt";
           */
            //Naslov statusa vozila
            /*
            var partner40 = "Select ID,Naziv from NaslovStatusaVozila order by Naziv";
            var partAD40 = new SqlDataAdapter(partner40, conn);
            var partDS40 = new DataSet();
            partAD40.Fill(partDS40);
            cboNaslovStatusaVozila.DataSource = partDS40.Tables[0];
            cboNaslovStatusaVozila.DisplayMember = "Naziv";
            cboNaslovStatusaVozila.ValueMember = "ID";
            */



            //carinski postupak
            var dir2 = "Select ID, (Oznaka + ' ' + Naziv) as Naziv from VrstaCarinskogPostupka order by Naziv";
            var dirAD2 = new SqlDataAdapter(dir2, conn);
            var dirDS2 = new DataSet();
            dirAD2.Fill(dirDS2);
            cboReexport.DataSource = dirDS2.Tables[0];
            cboReexport.DisplayMember = "Naziv";
            cboReexport.ValueMember = "ID";
            
            //Novi sifarnik Inpekciski tretman
            var dir4 = "Select ID,Naziv from InspekciskiTretman order by Naziv";
            var dirAD4 = new SqlDataAdapter(dir4, conn);
            var dirDS4 = new DataSet();
            dirAD4.Fill(dirDS4);
            cboInspekciskiTretman.DataSource = dirDS4.Tables[0];
            cboInspekciskiTretman.DisplayMember = "Naziv";
            cboInspekciskiTretman.ValueMember = "ID";


            var np4 = "Select ID,(Oznaka + ' ' + Naziv) as Naziv from uvNacinPakovanja order by Naziv";
            var npAD4 = new SqlDataAdapter(np4, conn);
            var npDS4 = new DataSet();
            npAD4.Fill(npDS4);
            cbNacinPakovanja.DataSource = npDS4.Tables[0];
            cbNacinPakovanja.DisplayMember = "Naziv";
            cbNacinPakovanja.ValueMember = "ID";

            var partner = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var partAD = new SqlDataAdapter(partner, conn);
            var partDS = new DataSet();
            partAD.Fill(partDS);
            cboIzvoznik.DataSource = partDS.Tables[0];
            cboIzvoznik.DisplayMember = "PaNaziv";
            cboIzvoznik.ValueMember = "PaSifra";

            var nalogodavac1 = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var nal1AD = new SqlDataAdapter(nalogodavac1, conn);
            var nal1DS = new DataSet();
            nal1AD.Fill(nal1DS);
            cboNalogodavac1.DataSource = nal1DS.Tables[0];
            cboNalogodavac1.DisplayMember = "PaNaziv";
            cboNalogodavac1.ValueMember = "PaSifra";

            var sfnalogodavac1 = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var sfnal1AD = new SqlDataAdapter(sfnalogodavac1, conn);
            var sfnal1DS = new DataSet();
            sfnal1AD.Fill(sfnal1DS);
            sfNalogodavac1.DataSource = sfnal1DS.Tables[0];
            sfNalogodavac1.DisplayMember = "PaNaziv";
            sfNalogodavac1.ValueMember = "PaSifra";

            var nalogodavac2 = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var nal2AD = new SqlDataAdapter(nalogodavac2, conn);
            var nal2DS = new DataSet();
            nal2AD.Fill(nal2DS);
            cboNalogodavac2.DataSource = nal2DS.Tables[0];
            cboNalogodavac2.DisplayMember = "PaNaziv";
            cboNalogodavac2.ValueMember = "PaSifra";

            var nalogodavac3 = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var nal3AD = new SqlDataAdapter(nalogodavac3, conn);
            var nal3DS = new DataSet();
            nal3AD.Fill(nal3DS);
            cboNalogodavac3.DataSource = nal3DS.Tables[0];
            cboNalogodavac3.DisplayMember = "PaNaziv";
            cboNalogodavac3.ValueMember = "PaSifra";

            var nalogodavac4 = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var nal4AD = new SqlDataAdapter(nalogodavac4, conn);
            var nal4DS = new DataSet();
            nal4AD.Fill(nal4DS);
            cboSpediterURijeci.DataSource = nal4DS.Tables[0];
            cboSpediterURijeci.DisplayMember = "PaNaziv";
            cboSpediterURijeci.ValueMember = "PaSifra";



            //Panta
            /*
            var VRHS = "Select ID,(HSKod + '   ' + Rtrim(Naziv)) as Naziv from VrstaRobeHS order by HSKod";
            var VRHSAD = new SqlDataAdapter(VRHS, conn);
            var VRHSDS = new DataSet();
            VRHSAD.Fill(VRHSDS);
            cboNazivRobe.DataSource = VRHSDS.Tables[0];
            cboNazivRobe.DisplayMember = "Naziv";
            cboNazivRobe.ValueMember = "ID";
            */
            var nhm = "";
            if (chkInterni.Checked == true)
            {
                nhm = "Select ID,(RTRIM(Naziv) + '-' + Rtrim(Broj)) as Naziv from NHM where Interni = 1 order by Naziv ";
            }
            else
            {
                nhm = "Select ID,(RTRIM(Naziv) + '-' + Rtrim(Broj)) as Naziv from NHM order by Naziv";
            }
            
            var nhmSAD = new SqlDataAdapter(nhm, conn);
            var nhmSDS = new DataSet();
            nhmSAD.Fill(nhmSDS);
            cboNHM.DataSource = nhmSDS.Tables[0];
            cboNHM.DisplayMember = "Naziv";
            cboNHM.ValueMember = "ID";


            var val = "Select VaSifra, VaNaziv from Valute order by VaSifra";
            var valSAD = new SqlDataAdapter(val, conn);
            var valSDS = new DataSet();
            valSAD.Fill(valSDS);
            txtValuta.DataSource = valSDS.Tables[0];
            txtValuta.DisplayMember = "VaNaziv";
            txtValuta.ValueMember = "VaSifra";


            // var bro2 = "Select Sifra, Naziv From VrstePlombi order by Sifra";
            var bro2 = "Select PaSifra, PaNaziv From Partnerji where Brodar = 1  order by PaNaziv";
            var broAD2 = new SqlDataAdapter(bro2, conn);
            var broDS2 = new DataSet();
            broAD2.Fill(broDS2);
            cboVrstaPlombe.DataSource = broDS2.Tables[0];
            cboVrstaPlombe.DisplayMember = "PaNaziv";
            cboVrstaPlombe.ValueMember = "PaSifra";


            var dir5 = "Select ID,Naziv from PredefinisanePoruke order by Naziv";
            var dirAD5 = new SqlDataAdapter(dir5, conn);
            var dirDS5 = new DataSet();
            dirAD5.Fill(dirDS5);
            cbNapomenaPoz.DataSource = dirDS5.Tables[0];
            cbNapomenaPoz.DisplayMember = "Naziv";
            cbNapomenaPoz.ValueMember = "ID";

            //FillCombo
            var partner22 = "SELECT ID, Min(Naziv) as Naziv FROM Scenario group by ID order by ID";
            var partAD22 = new SqlDataAdapter(partner22, conn);
            var partDS22 = new DataSet();
            partAD22.Fill(partDS22);
            cboScenario.DataSource = partDS22.Tables[0];
            cboScenario.DisplayMember = "Naziv";
            cboScenario.ValueMember = "ID";

        }




        private void GetID()
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand("Select MAX(ID) FROM Izvoz", conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    int IDpom = Convert.ToInt32(dr[0].ToString());
                    txtID.Text = IDpom.ToString();
                }
            }
            UpisiKorisnikaIVreme(Convert.ToInt32(txtID.Text));
        }

        private void UpisiKorisnikaIVreme(int IDpom)
        {
            

            using (SqlConnection connection1 = new SqlConnection(connection))
            using (SqlCommand command = connection1.CreateCommand())
            {
                command.CommandText = "Update Izvoz set Korisnik = '" + tKorisnik + "' , DatumKreiranja = ' " + DateTime.Now + "' where ID = " + IDpom;
            


                connection1.Open();
                command.ExecuteNonQuery();
                connection1.Close();
            }
            tslKreirao.Text = tKorisnik;
            tslDatum.Text = DateTime.Now.ToString();
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connection);
                SqlCommand cmd = new SqlCommand("Insert into Izvoz Default Values", conn);
                conn.Open();
                var q = cmd.ExecuteNonQuery();
                conn.Close();
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            GetID();
            tsNew.Enabled = false;
            txtBrKont.Text = "";
            chkNajavaVozila.Checked = false;
            txtVozilo.Text = "";
            txtVozac.Text = "";
            txtVozilo.Enabled = false;
            txtVozac.Enabled = false;
            dtpCutOffPort.Value = DateTime.Now;
            dtpPlanUtovara.Value = DateTime.Now;
            dtpEtaLeget.Value = DateTime.Now;
            dtpPeriodSkladistenjaOd.Value = DateTime.Now;
            dtpPeriodSkladistenjaDo.Value = DateTime.Now;
            
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            InsertIzvoz ins = new InsertIzvoz();
            int pomCirada = 0;
                if (chkCirada.Text == "CIRADA")
            {
                pomCirada = 1;
                        }

            int pomNajavaVozila = 0;
            if (chkNajavaVozila.Checked == true)
            {
                pomNajavaVozila = 1;
            }

            int pomNacinPretovara = 0;
            if (chkNacinPretovara.Checked == true)
            {
                pomNacinPretovara = 1;
            }

            int pomVaganje = 0;
            if (chkVaganje.Checked == true)
            {
                pomVaganje = 1;
            }

            int pomDobijenNalog = 0;
           
            if (cboDobijenNalog.Checked == true)
            {
                pomDobijenNalog = 1;
            }
            int pomTerminal = 0;
            if (chkTerminal.Checked == true)
            {
                pomTerminal= 1;
            }

            if (txtID.Text == "")
            {
                MessageBox.Show("Prvo kontejner mora dobiti izvozni broj, kliknite na Novi");
                return;
            
            }

            ins.UpdIzvoz(Convert.ToInt32(txtID.Text), txtBrojVagona.Text, txtBrKont.Text, Convert.ToInt32(txtTipKont.SelectedValue),
                txtBrodskaPlomba.Text, Convert.ToInt32(txtBokingBrodara.Text), Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToDateTime(dtpCutOffPort.Value),
                Convert.ToDecimal(txtNetoR.Value), Convert.ToDecimal(txtBrutoR.Value), Convert.ToDecimal(txtBrutoO.Value), Convert.ToInt32(txtKoleta.Value),
                Convert.ToInt32(txtKoletaO.Value), Convert.ToDecimal(txtCBM.Value), Convert.ToDecimal(txtCBMO.Value), Convert.ToDecimal(txtVrednostRobeFaktura.Value),
                Convert.ToString(txtValuta.SelectedValue), Convert.ToInt32(cboKrajnjaDestinacija.SelectedValue), Convert.ToInt32(cboPostupanjeSaRobom.SelectedValue),
                Convert.ToInt32(cboPPCNT.SelectedValue), pomCirada, Convert.ToDateTime(dtpPlanUtovara.Value), Convert.ToInt32(cboMestoUtovara.SelectedValue),
                Convert.ToInt32(txtKontaktOsoba.SelectedValue), Convert.ToInt32(cboCarina.SelectedValue), Convert.ToInt32(cboSpedicija.SelectedValue),
                cboAdresaStatusVozila.Text, cboNaslovStatusaVozila.Text, Convert.ToDateTime(dtpEtaLeget.Value),
                Convert.ToInt32(cboReexport.SelectedValue), Convert.ToInt32(cboInspekciskiTretman.SelectedValue), Convert.ToDecimal(txtAutoDana.Value),
                pomNajavaVozila, Convert.ToInt32(cbNacinPakovanja.SelectedValue), pomNacinPretovara, txtDodatneNapomene.Text, pomVaganje, Convert.ToDecimal(txtOdvaganaTezina.Value),
                Convert.ToDecimal(txtTaraKontejnera.Value), Convert.ToDecimal(txVGMBrodBruto.Value), Convert.ToInt32(cboIzvoznik.SelectedValue),
                Convert.ToInt32(cboNalogodavac1.SelectedValue), Convert.ToInt32(txtRef1.Text), pomDobijenNalog,
                Convert.ToInt32(cboNalogodavac2.SelectedValue), Convert.ToInt32(txtRef2.Text),
                Convert.ToInt32(cboNalogodavac3.SelectedValue), Convert.ToInt32(txtRef3.Text),
                 Convert.ToInt32(cboSpediterURijeci.SelectedValue), txtOstalePlombe.Text,
                 Convert.ToInt32(txtADR.SelectedValue), txtVozilo.Text, txtVozac.Text, Convert.ToInt32(cboSpedicijaJ.SelectedValue), 
                 Convert.ToDateTime(dtpPeriodSkladistenjaOd.Value), Convert.ToDateTime(dtpPeriodSkladistenjaDo.Value), Convert.ToInt32(cboVrstaPlombe.SelectedValue), txtNapomenaZaRobu.Text, Convert.ToDecimal(txtVGMBrod.Value), txtKontaktSpeditera.Text, txtKontaktOsobe.Text, Convert.ToInt32(txtUvozniID.Text), pomTerminal, Convert.ToInt32(cboScenario.SelectedValue));
            //Fale ostale plombe
            // Convert.ToDecimal(txtDodatneNapomene.Text -- treba staviti nvarchar

            /*
            ins.UpdUvoz(Convert.ToInt32(txtID.Text), Convert.ToDateTime(dtEtaRijeka.Value.ToString()),
                Convert.ToDateTime(dtAtaRijeka.Value.ToString()), txtStatus.Text.ToString().TrimEnd(), txtBrKont.Text,
                Convert.ToInt32(txtTipKont.SelectedValue), Convert.ToDateTime(dtNalogBrodara.Value.ToString()), txtBZ.Text.ToString().TrimEnd(),
                txtNapomena.Text.ToString().TrimEnd(), txtPIN.Text.ToString().TrimEnd(), Convert.ToInt32(cbDirigacija.SelectedValue), Convert.ToInt32(cbBrod.SelectedValue),
                txtTeretnica.Text, Convert.ToInt32(txtADR.SelectedValue), Convert.ToInt32(cbVlasnikKont.SelectedValue), Convert.ToInt32(txtBuking.Text), nalogodavci,
                usluge, Convert.ToInt32(cboUvoznik.SelectedValue), Convert.ToInt32(cboNHM.SelectedValue), cboNazivRobe.SelectedValue.ToString(), Convert.ToInt32(cboSpedicijaG.SelectedValue),
                Convert.ToInt32(cboSpedicijaRTC.SelectedValue), Convert.ToInt32(cboCarinskiPostupak.SelectedValue), Convert.ToInt32(cbPostupak.SelectedValue),
                Convert.ToInt32(cbNacinPakovanja.SelectedValue), Convert.ToInt32(cbOcarina.SelectedValue), Convert.ToInt32(cbOspedicija.SelectedValue),
                txtMesto.Text.ToString().TrimEnd(), txtKontaktOsoba.Text.ToString().TrimEnd(), txtMail.Text.ToString(), txtPlomba1.Text,
                txtPlomba2.Text, Convert.ToDecimal(txtNetoR.Value), Convert.ToDecimal(txtBrutoR.Value), Convert.ToDecimal(txtTaraK.Value),
                Convert.ToDecimal(txtBrutoK.Value), Convert.ToInt32(cbNapomenaPoz.SelectedValue), Convert.ToDateTime(dtAtaOtprema.Value.ToString()),
                Convert.ToInt32(txtBrojVoza.Text), txtRelacija.Text.ToString().TrimEnd(), Convert.ToDateTime(dtAtaDolazak.Value.ToString()), Convert.ToDecimal(txtKoleta.Value), Convert.ToInt32(cboRLTerminal.SelectedValue), txtNapomena1.Text, txtVrstaPregleda.Text,
                Convert.ToInt32(cboNalogodavac1.SelectedValue), txtRef1.Text,
                Convert.ToInt32(cboNalogodavac2.SelectedValue), txtRef2.Text,
                Convert.ToInt32(cboNalogodavac3.SelectedValue), txtRef3.Text, Convert.ToInt32(cboBrodar.SelectedValue));
            */
             FillDG();
            //  RefreshDataGridColor();
            tsNew.Enabled = true;
           // txtID.Text = "";
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            frmIzvozDokumenta fid = new frmIzvozDokumenta(txtID.Text, txtBokingBrodara.Text, "0");
            fid.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            FillDG6();
        }

        private void FillDG6()
        {

            var select = "select Cene.ID, VrstaManipulacije.Naziv, Cene.Cena, VrstaManipulacije.ID from Cene " +
            " inner join VrstaManipulacije on VrstaManipulacije.ID = Cene.VrstaManipulacije " +
            " where TipCenovnika = 1 order by VrstaManipulacije.Naziv ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView6.ReadOnly = true;
            dataGridView6.DataSource = ds.Tables[0];


            dataGridView6.BorderStyle = BorderStyle.None;
            dataGridView6.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView6.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView6.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView6.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView6.BackgroundColor = Color.White;

            dataGridView6.EnableHeadersVisualStyles = false;
            dataGridView6.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView6.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView6.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView6.Columns[0];
            dataGridView6.Columns[0].HeaderText = "ID";
            dataGridView6.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView6.Columns[1];
            dataGridView6.Columns[1].HeaderText = "Man";
            dataGridView6.Columns[1].Width = 120;

            DataGridViewColumn column3 = dataGridView6.Columns[2];
            dataGridView6.Columns[2].HeaderText = "Cena";
            dataGridView6.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView6.Columns[3];
            dataGridView6.Columns[3].HeaderText = "IDVM";
            dataGridView6.Columns[3].Width = 50;
            dataGridView6.Columns[3].Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FillDG7();
        }
        private void FillDG7()
        {

            var select = "select Cene.ID, VrstaManipulacije.Naziv, Cene.Cena, VrstaManipulacije.ID from Cene " +
            " inner join VrstaManipulacije on VrstaManipulacije.ID = Cene.VrstaManipulacije " +
            " where TipCenovnika <> 1 and Cene.PostupakSaRobom = " + Convert.ToInt32(cboPostupanjeSaRobom.SelectedValue) + " and Cene.Uvoznik = " + Convert.ToInt32(cboIzvoznik.SelectedValue) + "  order by VrstaManipulacije.Naziv ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView6.ReadOnly = true;
            dataGridView6.DataSource = ds.Tables[0];


            dataGridView6.BorderStyle = BorderStyle.None;
            dataGridView6.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView6.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView6.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView6.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView6.BackgroundColor = Color.White;

            dataGridView6.EnableHeadersVisualStyles = false;
            dataGridView6.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView6.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView6.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView6.Columns[0];
            dataGridView6.Columns[0].HeaderText = "ID";
            dataGridView6.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView6.Columns[1];
            dataGridView6.Columns[1].HeaderText = "Man";
            dataGridView6.Columns[1].Width = 120;

            DataGridViewColumn column3 = dataGridView6.Columns[2];
            dataGridView6.Columns[2].HeaderText = "Cena";
            dataGridView6.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView6.Columns[3];
            dataGridView6.Columns[3].HeaderText = "IDVM";
            dataGridView6.Columns[3].Width = 50;
            dataGridView6.Columns[3].Visible = false;
        }
/*
        private void UbaciStavkuUsluge(int ID, int Manipulacija, double Cena)
        {
            InsertIzvoz uvK = new InsertIzvoz();
            uvK.InsUbaciUslugu(Convert.ToInt32(txtID.Text), Manipulacija, Cena);
            FillDG8();
        }
*/
        private void FillDG8()
        {
            var select = "select  IzvozVrstaManipulacije.ID, VrstaManipulacije.Naziv, IzvozVrstaManipulacije.Cena, VrstaManipulacije.ID from IzvozVrstaManipulacije " +
            " inner join VrstaManipulacije on VrstaManipulacije.ID = IzvozVrstaManipulacije.IDVrstaManipulacije " +
                " where IzvozVrstaManipulacije.IDNadredjena = " + Convert.ToInt32(txtID.Text);
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView5.ReadOnly = true;
            dataGridView5.DataSource = ds.Tables[0];


            dataGridView5.BorderStyle = BorderStyle.None;
            dataGridView5.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView5.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView5.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView5.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView5.BackgroundColor = Color.White;

            dataGridView5.EnableHeadersVisualStyles = false;
            dataGridView5.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView5.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView5.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView5.Columns[0];
            dataGridView5.Columns[0].HeaderText = "ID";
            dataGridView5.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView5.Columns[1];
            dataGridView5.Columns[1].HeaderText = "Man";
            dataGridView5.Columns[1].Width = 120;

            DataGridViewColumn column3 = dataGridView5.Columns[2];
            dataGridView5.Columns[2].HeaderText = "Cena";
            dataGridView5.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView5.Columns[3];
            dataGridView5.Columns[3].HeaderText = "IDVM";
            dataGridView5.Columns[3].Width = 50;
            dataGridView5.Columns[3].Visible = false;

        }

      

        private void FillDG2()
        {
            var select = " SELECT     IzvozNHM.ID, NHM.Broj, IzvozNHM.IDNHM, NHM.Naziv FROM NHM INNER JOIN " +
                      " IzvozNHM ON NHM.ID = IzvozNHM.IDNHM where Izvoznhm.idnadredjena = " + Convert.ToInt32(txtID.Text) + " order by Izvoznhm.ID desc ";
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

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "Broj";
            dataGridView2.Columns[1].Width = 80;

            DataGridViewColumn column3 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "ID";
            dataGridView2.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView2.Columns[3];
            dataGridView2.Columns[3].HeaderText = "NHM";
            dataGridView2.Columns[3].Width = 150;



        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertIzvoz uvK = new InsertIzvoz();
            uvK.InsIzvozNHM(Convert.ToInt32(txtID.Text), Convert.ToInt32(cboNHM.SelectedValue));
            FillDG2();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InsertIzvoz uvK = new InsertIzvoz();
            uvK.DelIzvozNHM(Convert.ToInt32(txtIDNHM.Text));
            FillDG2();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            InsertIzvoz uvK = new InsertIzvoz();
            uvK.InsIzvozVrstaRobeHS(Convert.ToInt32(txtID.Text), Convert.ToInt32(cboNazivRobe.SelectedValue));
            FillDG3();
        }

        private void FillDG3()
        {
            var select = "select IzvozVrstaRobeHS.ID, IDVrstaRobeHS, VrstaRobeHS.HSKod as Kod,VrstaRobeHS.Naziv as HS from IzvozVrstaRobeHS " +
" inner join  VrstaRobeHS on VrstaRobeHS.ID = IzvozVrstaRobeHS.IDVrstaRobeHS where idnadredjena = " + Convert.ToInt32(txtID.Text) + " order by IzvozVrstaRobeHS.ID desc ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
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
            dataGridView3.Columns[1].HeaderText = "HSID";
            dataGridView3.Columns[1].Width = 20;

            DataGridViewColumn column3 = dataGridView3.Columns[2];
            dataGridView3.Columns[2].HeaderText = "HSKOD";
            dataGridView3.Columns[2].Width = 80;

            DataGridViewColumn column4 = dataGridView3.Columns[3];
            dataGridView3.Columns[3].HeaderText = "HS";
            dataGridView3.Columns[3].Width = 180;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            InsertIzvoz uvK = new InsertIzvoz();
            uvK.DelIzvozVrstaRobeHS(Convert.ToInt32(txtVrstaRobeHS.Text));
            FillDG3();
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {

            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {
                        txtIDNHM.Text = row.Cells[0].Value.ToString();

                    }
                }
            }
            catch { }
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    if (row.Selected)
                    {
                        txtVrstaRobeHS.Text = row.Cells[0].Value.ToString();

                    }
                }
            }
            catch { }
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {

        }

        private void VratiPodatkeSelect(int ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT [ID],[BrojVagona]      ,[BrojKontejnera],[VrstaKontejnera] " +
   "   ,[BrodskaPlomba],[BookingBrodara],[Brodar],[CutOffPort] " +
   "      ,[NetoRobe],[BrutoRobe],[BrutoRobeO],[BrojKoleta] " +
   "      ,[BrojKoletaO],[CBM],[CBMO],[VrednostRobeFaktura] " +
   "      ,[Valuta],[KrajnaDestinacija],[Postupanje],[MestoPreuzimanja] " +
   "      ,[Cirada],[PlaniraniDatumUtovara],[MesoUtovara],[KontaktOsoba] " +
   "      ,[MestoCarinjenja],[Spedicija],[AdresaSlanjaStatusa],[NaslovSlanjaStatusa] " +
   "      ,[EtaLeget],[NapomenaReexport],[Inspekcija],[AutoDana] " +
    "     ,[NajavaVozila],[NacinPakovanja],[NacinPretovara],[DodatneNapomeneDrumski] " +
    "     ,[Vaganje],[VGMTezina],[Tara],[VGMBrod] " +
   "      ,[Izvoznik],[Klijent1],[Napomena1REf],[DodatneNapomeneDrumski] " +
   "      ,[Klijent2],[Napomena2REf],[Klijent3],[Napomena3REf] " +
   "      ,[SpediterRijeka],[OstalePlombe],[ADR],[Vozilo],[Vozac], SpedicijaJ, PeriodSkladistenjaOd, PeriodSkladistenjaDo, VrstaBrodskePlombe, NapomenaZaRobu, VGMBrod2  ,[KontaktSpeditera] " +
      " ,[KontaktOsobe]      ,[Korisnik]      ,[DatumKreiranja], UvozID, Terminal, Scenario " +
 "  FROM [Izvoz] where ID=" + ID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtUvozniID.Text = dr["UvozID"].ToString();
                txtVozilo.Text = dr["Vozilo"].ToString();
                txtVozac.Text = dr["Vozac"].ToString();
                txtOstalePlombe.Text = dr["OstalePlombe"].ToString();
                cboSpediterURijeci.SelectedValue = Convert.ToInt32(dr["SpediterRijeka"].ToString());
                if (dr["DodatneNapomeneDrumski"].ToString() == "1")
                {
                    cboDobijenNalog.Checked = true;
                }
                else
                {
                    cboDobijenNalog.Checked = false;
                }

                if (dr["Terminal"].ToString() == "1")
                {
                    chkTerminal.Checked = true;
                }
                else
                {
                    chkTerminal.Checked = false;
                }
                txtRef3.Text = dr["Napomena3REf"].ToString();
                txtRef2.Text = dr["Napomena2REf"].ToString();
                txtRef1.Text = dr["Napomena1REf"].ToString();
                cboNalogodavac1.SelectedValue = Convert.ToInt32(dr["Klijent1"].ToString());
                cboNalogodavac2.SelectedValue = Convert.ToInt32(dr["Klijent2"].ToString());
                cboNalogodavac3.SelectedValue = Convert.ToInt32(dr["Klijent3"].ToString());
                cboIzvoznik.SelectedValue = Convert.ToInt32(dr["Izvoznik"].ToString());
                txVGMBrodBruto.Value = Convert.ToDecimal(dr["VGMBrod"].ToString());
                txtTaraKontejnera.Value = Convert.ToDecimal(dr["Tara"].ToString());
                txtOdvaganaTezina.Value = Convert.ToDecimal(dr["VGMTezina"].ToString());
                txtVGMBrod.Value = Convert.ToDecimal(dr["VGMBrod2"].ToString());
                if (dr["Vaganje"].ToString() == "1")
                {
                    chkVaganje.Checked = true;
                }
                else
                {
                    chkVaganje.Checked = false;
                }
                txtDodatneNapomene.Text = dr["DodatneNapomeneDrumski"].ToString();
                if (dr["NacinPretovara"].ToString() == "1")
                {
                    chkNacinPretovara.Checked = true;
                    chkIndirektno.Checked = false;
                }
                else
                {
                    chkNacinPretovara.Checked = false;
                    chkIndirektno.Checked = true;
                }
                cbNacinPakovanja.SelectedValue = Convert.ToInt32(dr["NacinPakovanja"].ToString());
                if (dr["NajavaVozila"].ToString() == "1")
                {
                    chkNajavaVozila.Checked = true;
                }
                else
                {
                    chkNajavaVozila.Checked = false;
                }
                dtpPeriodSkladistenjaDo.Value = Convert.ToDateTime(dr["PeriodSkladistenjaDo"].ToString());
                dtpPeriodSkladistenjaOd.Value = Convert.ToDateTime(dr["PeriodSkladistenjaOd"].ToString());
                cboSpedicijaJ.SelectedValue = Convert.ToInt32(dr["SpedicijaJ"].ToString());
                txtAutoDana.Value = Convert.ToDecimal(dr["AutoDana"].ToString());
                cboInspekciskiTretman.SelectedValue = Convert.ToInt32(dr["Inspekcija"].ToString());
                cboReexport.SelectedValue = Convert.ToInt32(dr["NapomenaReexport"].ToString());
                dtpEtaLeget.Value  = Convert.ToDateTime(dr["EtaLeget"].ToString());
                cboNaslovStatusaVozila.Text = dr["NaslovSlanjaStatusa"].ToString();
                cboAdresaStatusVozila.Text= dr["AdresaSlanjaStatusa"].ToString();
                txtADR.SelectedValue = Convert.ToInt32(dr["ADR"].ToString());
                cboSpedicija.SelectedValue = Convert.ToInt32(dr["Spedicija"].ToString());
                cboCarina.SelectedValue = Convert.ToInt32(dr["MestoCarinjenja"].ToString());
                txtKontaktOsoba.SelectedValue= Convert.ToInt32((dr["KontaktOsoba"].ToString()));
                cboMestoUtovara.SelectedValue = Convert.ToInt32(dr["MesoUtovara"].ToString());
                dtpPlanUtovara.Value = Convert.ToDateTime(dr["PlaniraniDatumUtovara"].ToString());
                if (dr["Cirada"].ToString() == "1")
                { chkCirada.Text = "CIRADA";
                }   
                else
                {
                    chkCirada.Text = "PLATFORMA";
                }
                cboPPCNT.SelectedValue = Convert.ToInt32(dr["MestoPreuzimanja"].ToString());
                cboPostupanjeSaRobom.SelectedValue = Convert.ToInt32(dr["Postupanje"].ToString());
                cboKrajnjaDestinacija.SelectedValue = Convert.ToInt32(dr["KrajnaDestinacija"].ToString());
                txtValuta.SelectedValue = dr["Valuta"].ToString();
                txtVrednostRobeFaktura.Value = Convert.ToDecimal(dr["VrednostRobeFaktura"].ToString());
                txtCBMO.Value = Convert.ToDecimal(dr["CBMO"].ToString());
                txtCBM.Value = Convert.ToDecimal(dr["CBM"].ToString());
                txtKoletaO.Value = Convert.ToDecimal(dr["BrojKoletaO"].ToString());
                txtKoleta.Value = Convert.ToDecimal(dr["BrojKoleta"].ToString());
                txtBrutoO.Value = Convert.ToDecimal(dr["BrutoRobeO"].ToString());
                txtBrutoR.Value = Convert.ToDecimal(dr["BrutoRobe"].ToString());
                txtNetoR.Value = Convert.ToDecimal(dr["NetoRobe"].ToString());
                dtpCutOffPort.Value = Convert.ToDateTime(dr["CutOffPort"].ToString());
                cboBrodar.SelectedValue = Convert.ToInt32(dr["Brodar"].ToString());
                txtBokingBrodara.Text = dr["BookingBrodara"].ToString();
                txtOstalePlombe.Text = dr["OstalePlombe"].ToString();
                txtBrodskaPlomba.Text   = dr["BrodskaPlomba"].ToString();
                txtTipKont.SelectedValue = Convert.ToInt32(dr["VrstaKontejnera"].ToString());
                txtBrKont.Text = dr["BrojKontejnera"].ToString();
                txtBrojVagona.Text = dr["BrojVagona"].ToString();
                cboVrstaPlombe.SelectedValue = Convert.ToInt32(dr["VrstaBrodskePlombe"].ToString());
                txtNapomenaZaRobu.Text = dr["NapomenaZaRobu"].ToString();
                txtKontaktSpeditera.Text = dr["KontaktSpeditera"].ToString();
                txtKontaktOsobe.Text =  dr["KontaktOsobe"].ToString();     
                tslDatum.Text = dr["DatumKreiranja"].ToString();
                tslKreirao.Text = dr["Korisnik"].ToString();

                cboScenario.SelectedValue = Convert.ToInt32(dr["Scenario"].ToString());

                /*

                dtEtaRijeka.Value = Convert.ToDateTime(dr["EtaBroda"].ToString());
                dtAtaRijeka.Value = Convert.ToDateTime(dr["AtaBroda"].ToString());
                txtStatus.Text = dr["StatusPrijema"].ToString();
                txtBrKont.Text = dr["BrojKontejnera"].ToString();
                txtTipKont.SelectedValue = Convert.ToInt32(dr["TipKontejnera"].ToString());
                dtNalogBrodara.Value = Convert.ToDateTime(dr["DobijenNalogBrodara"].ToString());
                txtBZ.Text = dr["DobijeBZ"].ToString();
                txtNapomena.Text = dr["Napomena"].ToString();
                txtPIN.Text = dr["PIN"].ToString();
                cbDirigacija.SelectedValue = Convert.ToInt32(dr["DirigacijaKontejeraZa"].ToString());
                cbBrod.SelectedValue = Convert.ToInt32(dr["NazivBroda"].ToString());
                txtTeretnica.Text = dr["BrodskaTeretnica"].ToString();
                txtADR.SelectedValue = Convert.ToInt32(dr["ADR"].ToString());
                cbVlasnikKont.SelectedValue = Convert.ToInt32(dr["VlasnikKontejnera"].ToString());
                txtBuking.Text = dr["BukingBrodara"].ToString();
                cboUvoznik.SelectedValue = Convert.ToInt32(dr["Uvoznik"].ToString());
                cboNHM.SelectedValue = 1;
                cboSpedicijaG.SelectedValue = Convert.ToInt32(dr["SpedicijaGranica"].ToString());
                cboSpedicijaRTC.SelectedValue = Convert.ToInt32(dr["SpedicijaRTC"].ToString());
                cboCarinskiPostupak.SelectedValue = Convert.ToInt32(dr["CarinskiPostupak"].ToString());
                cbPostupak.SelectedValue = Convert.ToInt32(dr["PostupakSaRobom"].ToString());
                cbNacinPakovanja.SelectedValue = Convert.ToInt32(dr["NacinPakovanja"].ToString());
                cbOcarina.SelectedValue = Convert.ToInt32(dr["OdredisnaCarina"].ToString());
                cbOspedicija.SelectedValue = Convert.ToInt32(dr["OdredisnaSpedicija"].ToString());
                txtMesto.Text = dr["MestoIstovara"].ToString();
                txtKontaktOsoba.Text = dr["KontaktOsoba"].ToString();
                txtMail.Text = dr["Email"].ToString();
                txtPlomba1.Text = dr["BrojPlombe1"].ToString();
                txtPlomba2.Text = dr["BrojPlombe2"].ToString();
                txtNetoR.Value = Convert.ToDecimal(dr["NetoRobe"].ToString());
                txtBrutoR.Value = Convert.ToDecimal(dr["BrutoRobe"].ToString());
                txtTaraK.Value = Convert.ToDecimal(dr["TaraKontejnera"].ToString());
                txtBrutoK.Value = Convert.ToDecimal(dr["BrutoKontejnera"].ToString());
                cbNapomenaPoz.SelectedValue = Convert.ToInt32(dr["NapomenaZaPozicioniranje"].ToString());
                dtAtaOtprema.Value = Convert.ToDateTime(dr["AtaOtpreme"].ToString());
                txtBrojVoza.Text = dr["BrojVoza"].ToString();
                txtRelacija.Text = dr["RelacijaVoza"].ToString();
                dtAtaDolazak.Value = Convert.ToDateTime(dr["AtaDolazak"].ToString());
                txtKoleta.Value = Convert.ToDecimal(dr["Koleta"].ToString());
                cboRLTerminal.SelectedValue = Convert.ToInt32(dr["RLTerminali"].ToString());
                txtNapomena1.Text = dr["Napomena1"].ToString();
                txtVrstaPregleda.Text = dr["VrstaPregleda"].ToString();
                cboNalogodavac1.SelectedValue = Convert.ToInt32(dr["Nalogodavac1"].ToString());
                txtRef1.Text = dr["Ref1"].ToString();
                cboNalogodavac2.SelectedValue = Convert.ToInt32(dr["Nalogodavac2"].ToString());
                txtRef2.Text = dr["Ref2"].ToString();
                cboNalogodavac3.SelectedValue = Convert.ToInt32(dr["Nalogodavac3"].ToString());
                txtRef3.Text = dr["Ref3"].ToString();

                cboBrodar.SelectedValue = Convert.ToInt32(dr["Brodar"].ToString());
                */




            }



            con.Close();


        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    txtID.Text = row.Cells[0].Value.ToString();
                    VratiPodatkeSelect(Convert.ToInt32(txtID.Text));
                    FillDG2();
                    FillDG3();
                   
                    FillDG8();
                    FillDGUsluge();
                }
            }
        }

        private void chkNajavaVozila_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNajavaVozila.Checked == true)
            {
                txtVozilo.Enabled = true;
                txtVozac.Enabled = true;
            }
            else
            {
                txtVozilo.Enabled = true;
                txtVozac.Enabled = true;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            { txtID.Text = "0"; }
            // int IDPlana, int ID, int Nalogodavac1, int Nalogodavac2, int Nalogodavac3
            frmIzvozUnosManipulacije um = new frmIzvozUnosManipulacije(Convert.ToInt32(0), Convert.ToInt32(txtID.Text), Convert.ToInt32(cboNalogodavac1.SelectedValue), Convert.ToInt32(cboNalogodavac2.SelectedValue), Convert.ToInt32(cboNalogodavac3.SelectedValue), Convert.ToInt32(cboIzvoznik.SelectedValue));
            um.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            VratiAdresuKontaktaIzNapomene(Convert.ToInt32(txtKontaktOsoba.SelectedValue));
           /*
                using (var detailForm = new frmKontaktOsobeMU(Convert.ToInt32(cboMestoUtovara.SelectedValue)))
            {
                detailForm.ShowDialog();

                txtAdresaMestaUtovara.Text = detailForm.GetKontaktAdresa(Convert.ToInt32(cboMestoUtovara.SelectedValue));
               
            }
           */
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (var detailForm = new Dokumenta.frmKontaktOsobe(Convert.ToInt32(cboSpedicija.SelectedValue)))
            {
                detailForm.ShowDialog();
                txtKontaktSpeditera.Text = detailForm.GetKontakt(Convert.ToInt32(cboSpedicija.SelectedValue));
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connection);
            if (chkInterni.Checked == true)
            {
                switch (NHMObrni)
                {
                    case 1:
                        {
                            var nhm = "Select ID,Rtrim(Broj) + '-' + (Rtrim(Naziv)) as Naziv from NHM where Interni = 1 order by NHM.Broj";
                            var nhmSAD = new SqlDataAdapter(nhm, conn);
                            var nhmSDS = new DataSet();
                            nhmSAD.Fill(nhmSDS);
                            cboNHM.DataSource = nhmSDS.Tables[0];
                            cboNHM.DisplayMember = "Naziv";
                            cboNHM.ValueMember = "ID";
                            NHMObrni = 0;
                            break;

                        }
                    case 0:
                        {
                            var nhm = "Select ID,Rtrim(Naziv) + '-' + (Rtrim(Broj)) as Naziv from NHM where Interni = 1 order by NHM.Naziv";
                            var nhmSAD = new SqlDataAdapter(nhm, conn);
                            var nhmSDS = new DataSet();
                            nhmSAD.Fill(nhmSDS);
                            cboNHM.DataSource = nhmSDS.Tables[0];
                            cboNHM.DisplayMember = "Naziv";
                            cboNHM.ValueMember = "ID";
                            NHMObrni = 1;
                            break;
                        }
                    default:
                        break;
                }
            }
            else
            {
                switch (NHMObrni)
                {
                    case 1:
                        {
                            var nhm = "Select ID,Rtrim(Broj) + '-' + (Rtrim(Naziv)) as Naziv from NHM order by NHM.Broj";
                            var nhmSAD = new SqlDataAdapter(nhm, conn);
                            var nhmSDS = new DataSet();
                            nhmSAD.Fill(nhmSDS);
                            cboNHM.DataSource = nhmSDS.Tables[0];
                            cboNHM.DisplayMember = "Naziv";
                            cboNHM.ValueMember = "ID";
                            NHMObrni = 0;
                            break;

                        }
                    case 0:
                        {
                            var nhm = "Select ID,Rtrim(Naziv) + '-' + (Rtrim(Broj)) as Naziv from NHM order by NHM.Naziv";
                            var nhmSAD = new SqlDataAdapter(nhm, conn);
                            var nhmSDS = new DataSet();
                            nhmSAD.Fill(nhmSDS);
                            cboNHM.DataSource = nhmSDS.Tables[0];
                            cboNHM.DisplayMember = "Naziv";
                            cboNHM.ValueMember = "ID";
                            NHMObrni = 1;
                            break;
                        }
                    default:
                        break;
                }
            }
            

        }

        private void VratiAdresuKontaktaIzNapomene(int Sifra)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select PaKOOpomba from partnerjiKontOsebaMU where PaKOZapST  =" + Sifra, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                if (dr["PaKOOpomba"].ToString() != "")
                {
                    txtAdresaMestaUtovara.Text = dr["PaKOOpomba"].ToString();
                }
                else
                {
                    txtAdresaMestaUtovara.Text = "";
                }


            }
            con.Close();


        }

        private void button15_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connection);
            // PaKOOpomba
            //Bilo  var ko = "select PaKoZapSt, (Rtrim(PaKOIme) + ' ' + Rtrim(PaKoPriimek)) as Naziv from partnerjiKontOsebaMU where PaKOSifra = '" + Convert.ToInt32(cboMestoUtovara.SelectedValue) + "'  order by PaKOIme";
            var ko = "select PaKoZapSt, (Rtrim(PaKOOpomba)) as Naziv from partnerjiKontOsebaMU where PaKOSifra = '" + Convert.ToInt32(cboMestoUtovara.SelectedValue) + "'  order by PaKOIme";

            var koAD = new SqlDataAdapter(ko, conn);
            var koDS = new DataSet();
            koAD.Fill(koDS);
            txtAdresaMestaUtovara.DataSource = koDS.Tables[0];
            txtAdresaMestaUtovara.DisplayMember = "Naziv";
            txtAdresaMestaUtovara.ValueMember = "PaKoZapSt";
            // VratiAdresuKontaktaIzNapomene(Convert.ToInt32(cboMestoUtovara.SelectedValue));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (var detailForm = new Dokumenta.frmKontaktOsobe(Convert.ToInt32(cboNalogodavac3.SelectedValue)))
            {
                detailForm.ShowDialog();
                cboAdresaStatusVozila.Text = detailForm.GetKontaktMailSVISelektovani(Convert.ToInt32(cboNalogodavac3.SelectedValue));
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            cboNaslovStatusaVozila.Text = "";
            if (checkedListBox2.GetItemCheckState(0) == CheckState.Checked)
            {
                cboNaslovStatusaVozila.Text = cboNaslovStatusaVozila.Text + " " + cboNalogodavac3.Text;
            }
            if (checkedListBox2.GetItemCheckState(1) == CheckState.Checked)
            {
                cboNaslovStatusaVozila.Text = cboNaslovStatusaVozila.Text + "; " + cboIzvoznik.Text;
            }

            if (checkedListBox2.GetItemCheckState(2) == CheckState.Checked)
            {
                cboNaslovStatusaVozila.Text = cboNaslovStatusaVozila.Text + "; " + txtBrKont.Text;
            }
            if (checkedListBox2.GetItemCheckState(3) == CheckState.Checked)
            {
                cboNaslovStatusaVozila.Text = cboNaslovStatusaVozila.Text + ", " + cboBrodar.Text;
            }

            if (checkedListBox2.GetItemCheckState(4) == CheckState.Checked)
            {
                cboNaslovStatusaVozila.Text = cboNaslovStatusaVozila.Text + "; " + txtBokingBrodara.Text;
            }

           
          
          
        }
        private void VratiNHM(int Sifra)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select TOP 1 ID from NHM Where ADRID =  " + Sifra, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    cboNHM.SelectedValue = Convert.ToInt32(dr["ID"].ToString());


                }
            }
            else
            {
                cboNHM.SelectedValue = 0;
            }
            con.Close();


        }
        private void button13_Click(object sender, EventArgs e)
        {
            VratiNHM(Convert.ToInt32(txtADR.SelectedValue));
        }

        private void txtTaraKontejnera_Leave(object sender, EventArgs e)
        {
            txVGMBrodBruto.Value = txtOdvaganaTezina.Value + txtTaraKontejnera.Value;
        }

        private void FillDG4()
        {
            var select = "select IzvozNapomenePozicioniranja.ID, IDNapomene, stNapomene from IzvozNapomenePozicioniranja " +
"  where IzvozNapomenePozicioniranja.IdNadredjena = " + Convert.ToInt32(txtID.Text) + " order by IzvozNapomenePozicioniranja.ID desc ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView4.ReadOnly = true;
            dataGridView4.DataSource = ds.Tables[0];

            dataGridView4.BorderStyle = BorderStyle.None;
            dataGridView4.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView4.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView4.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView4.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView4.BackgroundColor = Color.White;

            dataGridView4.EnableHeadersVisualStyles = false;
            dataGridView4.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView4.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView4.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView4.Columns[0];
            dataGridView4.Columns[0].HeaderText = "ID";
            dataGridView4.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView4.Columns[1];
            dataGridView4.Columns[1].HeaderText = "NapomenaID";
            dataGridView4.Columns[1].Width = 20;

            DataGridViewColumn column3 = dataGridView4.Columns[2];
            dataGridView4.Columns[2].HeaderText = "Napomena";
            dataGridView4.Columns[2].Width = 160;

        }

        private void button17_Click(object sender, EventArgs e)
        {
            InsertIzvozKonacna uvK = new InsertIzvozKonacna();
            
            uvK.InsIzvozNapomenePozicioniranja(Convert.ToInt32(txtID.Text), Convert.ToInt32(cbNapomenaPoz.SelectedValue), cbNapomenaPoz.Text);
            FillDG4();
           // RefreshDataGridColor();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            InsertIzvozKonacna uvK = new InsertIzvozKonacna();
            uvK.DelIzvozNapomenePozicioniranja(Convert.ToInt32(txtNapomenaPoz.Text));
            FillDG4();
        }

        private void FillDGUsluge()
        {
            if (txtID.Text == "")
            { return; }
            var select = "";

            select = "select  IzvozVrstaManipulacije.ID as ID, IzvozVrstaManipulacije.IDNadredjena as KontejnerID, Izvoz.BrojKontejnera, " +
" IzvozVrstaManipulacije.Kolicina,  VrstaManipulacije.ID as ManipulacijaID,VrstaManipulacije.Naziv as ManipulacijaNaziv, " +
" IzvozVrstaManipulacije.Cena,OrganizacioneJedinice.ID,   OrganizacioneJedinice.Naziv as OrganizacionaJedinica,  " +
" Partnerji.PaSifra as NalogodavacID,PArtnerji.PaNaziv as Platilac " +
" from IzvozVrstaManipulacije " +
" Inner    join VrstaManipulacije on VrstaManipulacije.ID = IzvozVrstaManipulacije.IDVrstaManipulacije " +
" inner " +
" join PArtnerji on IzvozVrstaManipulacije.Platilac = PArtnerji.PaSifra " +
" inner " +
" join OrganizacioneJedinice on OrganizacioneJedinice.ID = IzvozVrstaManipulacije.OrgJed " +
" inner " +
" join Izvoz on IzvozVrstaManipulacije.IDNadredjena = Izvoz.ID where Izvoz.ID = " + Convert.ToInt32(txtID.Text);



            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView7.ReadOnly = true;
            dataGridView7.DataSource = ds.Tables[0];


            dataGridView7.BorderStyle = BorderStyle.None;
            dataGridView7.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView7.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView7.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView7.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView7.BackgroundColor = Color.White;

            dataGridView7.EnableHeadersVisualStyles = false;
            dataGridView7.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView7.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView7.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView7.Columns[0];
            dataGridView7.Columns[0].HeaderText = "ID";
            dataGridView7.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView7.Columns[1];
            dataGridView7.Columns[1].HeaderText = "IDU";
            dataGridView7.Columns[1].Width = 30;

            DataGridViewColumn column3 = dataGridView7.Columns[2];
            dataGridView7.Columns[2].HeaderText = "Kontejner";
            dataGridView7.Columns[2].Width = 50;

        }

        private void dataGridView4_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView4.Rows)
                {
                    if (row.Selected)
                    {
                        txtNapomenaPoz.Text = row.Cells[0].Value.ToString();

                    }
                }
            }
            catch { }
        }

        private void chkVaganje_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVaganje.Checked == true)
            {
                txtOdvaganaTezina.Enabled = true;
                txtTaraKontejnera.Enabled = true;
                txVGMBrodBruto.Enabled = true;
                txtVGMBrod.Enabled = true;
            }
            else
            {
                txtOdvaganaTezina.Enabled = false;
                txtTaraKontejnera.Enabled = false;
                txVGMBrodBruto.Enabled = false;
                txtVGMBrod.Enabled = false;
                ;
            }
        }

        private void chkNacinPretovara_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNacinPretovara.Checked == true)
                chkIndirektno.Checked = false;
            else
            {
                chkIndirektno.Checked = true;
            }
        }

        private void chkIndirektno_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIndirektno.Checked == true)
                chkNacinPretovara.Checked = false;
            else
            {
                chkNacinPretovara.Checked = true;
            }
        }

        private void txVGMBrodBruto_Leave(object sender, EventArgs e)
        {
            txtOdvaganaTezina.Value= txVGMBrodBruto.Value  - txtTaraKontejnera.Value;
        }

        private void cboNalogodavac1_KeyUp(object sender, KeyEventArgs e)
        {
           

        }

        private void cboNalogodavac1_TextChanged(object sender, EventArgs e)
        {
            /*
            var nalogodavac1 = "Select PaSifra,PaNaziv From Partnerji  where PaNAziv like '%" + cboNalogodavac1.Text + "%' order by PaNaziv";
            var nal1AD = new SqlDataAdapter(nalogodavac1, connection);
            var nal1DS = new DataSet();
            nal1AD.Fill(nal1DS);
            cboNalogodavac1.DataSource = nal1DS.Tables[0];
            cboNalogodavac1.DisplayMember = "PaNaziv";
            cboNalogodavac1.ValueMember = "PaSifra";
            */
        }

        private void button18_Click(object sender, EventArgs e)
        {
            //Vrati kontakt osobe
            
            using (var detailForm = new frmKontaktOsobeMU(txtAdresaMestaUtovara.Text))
            {
                detailForm.ShowDialog();
                txtKontaktOsobe.Text = detailForm.GetSviKontaktiPoAdresi(txtAdresaMestaUtovara.Text);
            }
           // txtKontaktOsobe.Text = GetSviKontaktiPoAdresi(txtAdresaMestaUtovara.Text);



            SqlConnection conn = new SqlConnection(connection);
            // PaKOOpomba
            //Bilo  var ko = "select PaKoZapSt, (Rtrim(PaKOIme) + ' ' + Rtrim(PaKoPriimek)) as Naziv from partnerjiKontOsebaMU where PaKOSifra = '" + Convert.ToInt32(cboMestoUtovara.SelectedValue) + "'  order by PaKOIme";
            var ko = "select PaKoZapSt, (Rtrim(PaKOIme) + ' ' + Rtrim(PaKoPriimek)) as Naziv from partnerjiKontOsebaMU where PaKoZapSt = '" + Convert.ToInt32(txtAdresaMestaUtovara.SelectedValue) + "'  order by PaKOIme";

            var koAD = new SqlDataAdapter(ko, conn);
            var koDS = new DataSet();
            koAD.Fill(koDS);
            txtKontaktOsoba.DataSource = koDS.Tables[0];
            txtKontaktOsoba.DisplayMember = "Naziv";
            txtKontaktOsoba.ValueMember = "PaKoZapSt";
        }

        private void frmIzvoz_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == System.Windows.Forms.Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            using (var detailForm = new frmIzvozTable())
            {
                detailForm.ShowDialog();
                txtID.Text = detailForm.GetID();
                if (txtID.Text !=  "")
                {
                    VratiPodatkeSelect(Convert.ToInt32(txtID.Text));
                    FillDGUsluge();
                }
            }
        }

        private void frmIzvoz_Activated(object sender, EventArgs e)
        {

           
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

            if (txtID.Text == "")
            { txtID.Text = "0"; }
            // int IDPlana, int ID, int Nalogodavac1, int Nalogodavac2, int Nalogodavac3
            frmIzvozUnosManipulacije um = new frmIzvozUnosManipulacije(Convert.ToInt32(0), Convert.ToInt32(txtID.Text), Convert.ToInt32(cboNalogodavac1.SelectedValue), Convert.ToInt32(cboNalogodavac2.SelectedValue), Convert.ToInt32(cboNalogodavac3.SelectedValue), Convert.ToInt32(cboIzvoznik.SelectedValue));
            um.Show();
        }

        private void frmIzvoz_SizeChanged(object sender, EventArgs e)
        {
            float size1 = this.Size.Width / firstWidth;
            float size2 = this.Size.Height / firstHeight;

            SizeF scale = new SizeF(size1, size2);
            firstWidth = this.Size.Width;
            firstHeight = this.Size.Height;

            foreach (Control control in this.Controls)
            {

                control.Font = new Font(control.Font.FontFamily, control.Font.Size * ((size1 + size2) / 2));
                
                control.Scale(scale);
                
                 string ff = control.GetType().Name;
                 if (control.GetType().Name == "ComboBox")
                 {
                    ComboBox cb = (ComboBox)control;
                    cb.SelectedIndex = -1;
                 }
                 
              


            }
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
            FillDGUsluge();
        }

        private void txtVrednostRobeFaktura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void txtNetoR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void txtBrutoR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void txtBrutoO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void txtKoleta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void txtKoletaO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void txtCBM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void txtCBMO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void txtAutoDana_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void txtOdvaganaTezina_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void txtTaraKontejnera_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void txVGMBrodBruto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void txtVGMBrod_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void txtVrednostRobeFaktura_Enter(object sender, EventArgs e)
        {
            txtVrednostRobeFaktura.Select(0, txtVrednostRobeFaktura.Text.Length);
        }

        private void txtNetoR_Enter(object sender, EventArgs e)
        {
            txtNetoR.Select(0, txtNetoR.Text.Length);
        }

        private void txtBrutoR_Enter(object sender, EventArgs e)
        {

            txtBrutoR.Select(0, txtBrutoR.Text.Length);
        }

        private void txtBrutoO_Enter(object sender, EventArgs e)
        {
            txtBrutoO.Select(0, txtBrutoO.Text.Length);
        }

        private void txtKoleta_Enter(object sender, EventArgs e)
        {
            txtKoleta.Select(0, txtKoleta.Text.Length);
        }

        private void txtKoletaO_Enter(object sender, EventArgs e)
        {
            txtKoletaO.Select(0, txtKoletaO.Text.Length);
        }

        private void txtCBM_Enter(object sender, EventArgs e)
        {
            txtCBM.Select(0, txtCBM.Text.Length);
            
        }

        private void txtCBMO_Enter(object sender, EventArgs e)
        {
            txtCBMO.Select(0, txtCBMO.Text.Length);
        }

        private void txtOdvaganaTezina_Enter(object sender, EventArgs e)
        {
            txtOdvaganaTezina.Select(0, txtOdvaganaTezina.Text.Length);
        }

        private void txtTaraKontejnera_Enter(object sender, EventArgs e)
        {
            txtTaraKontejnera.Select(0, txtTaraKontejnera.Text.Length);
        }

        private void txVGMBrodBruto_Enter(object sender, EventArgs e)
        {
            txVGMBrodBruto.Select(0, txVGMBrodBruto.Text.Length);
        }

        private void txtVGMBrod_Enter(object sender, EventArgs e)
        {
            txtVGMBrod.Select(0, txtVGMBrod.Text.Length);
            
        }

        private void txtAutoDana_Enter(object sender, EventArgs e)
        {
                 txtAutoDana.Select(0, txtAutoDana.Text.Length);
        }

        private void dtpPeriodSkladistenjaDo_Leave(object sender, EventArgs e)
        {
            chkNajavaVozila.Focus();
        }

        private void frmIzvoz_KeyDown(object sender, KeyEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift && e.KeyCode == Keys.D)
            {
                frmIzvozDokumenta fid = new frmIzvozDokumenta(txtID.Text, txtBokingBrodara.Text, "0");
                fid.Show();
            }
            else if (Control.ModifierKeys == Keys.Shift && e.KeyCode == Keys.T)
            {
                using (var detailForm = new frmIzvozTable())
                {
                    detailForm.ShowDialog();
                    txtID.Text = detailForm.GetID();
                    VratiPodatkeSelect(Convert.ToInt32(txtID.Text));
                }
            }
            else if (Control.ModifierKeys == Keys.Shift && e.KeyCode == Keys.U)
            {
                if (txtID.Text == "")
                { txtID.Text = "0"; }
                // int IDPlana, int ID, int Nalogodavac1, int Nalogodavac2, int Nalogodavac3
                frmIzvozUnosManipulacije um = new frmIzvozUnosManipulacije(Convert.ToInt32(0), Convert.ToInt32(txtID.Text), Convert.ToInt32(cboNalogodavac1.SelectedValue), Convert.ToInt32(cboNalogodavac2.SelectedValue), Convert.ToInt32(cboNalogodavac3.SelectedValue), Convert.ToInt32(cboIzvoznik.SelectedValue));
                um.Show();

            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Saobracaj.Uvoz.frmPrijemKamionaPlatforma pkp = new Uvoz.frmPrijemKamionaPlatforma(txtID.Text);
            pkp.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            using (var detailForm = new IzvozOpredeljenje())
            {
                detailForm.ShowDialog();
                txtBrKont.Text = detailForm.GetBrojKontejnera();
                txtUvozniID.Text = detailForm.GetUvozniID();
                InsertIzvoz ins = new InsertIzvoz();
                ins.IzvozOpredelio(txtBrKont.Text);
                MessageBox.Show("Kontejner je opredeljen");
                // VratiPodatkeSelect(Convert.ToInt32(txtID.Text));
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            InsertIzvozKonacna ins = new InsertIzvozKonacna();
            ins.PrenesiUPlanUtovaraIzvoz(Convert.ToInt32(txtID.Text), PlanZaPrebacivanje);

            Uvoz.InsertRadniNalogInterni ins2 = new Uvoz.InsertRadniNalogInterni();
            //ins.InsRadniNalogInterni(Convert.ToInt32(1), Convert.ToInt32(4), Convert.ToDateTime(DateTime.Now), Convert.ToDateTime("1.1.1900. 00:00:00"), "", Convert.ToInt32(0), "PlanUtovara", Convert.ToInt32(txtNadredjeni.Text), KorisnikTekuci, "");
            ins2.InsRadniNalogInterniIzvoz(Convert.ToInt32(4), Convert.ToInt32(4), Convert.ToDateTime(DateTime.Now), Convert.ToDateTime("1.1.1900. 00:00:00"), "", Convert.ToInt32(0), "PlanUtovaraT", Convert.ToInt32(PlanZaPrebacivanje), tKorisnik, "");
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            InsertIzvoz uvK = new InsertIzvoz();
            uvK.InsUbaciUsluguKonacna(Convert.ToInt32(txtID.Text), 69, 0, 1, 4, Convert.ToInt32(cboBrodar.SelectedValue), 0, "GATE OUT EMPTY", 12, "GATE OUT KAMION");
            FillDG8();


            InsertIzvozKonacna ins = new InsertIzvozKonacna();
            ins.PrenesiUPlanUtovaraIzvoz(Convert.ToInt32(txtID.Text), PlanZaPrebacivanje);

            Uvoz.InsertRadniNalogInterni ins2 = new Uvoz.InsertRadniNalogInterni();
            //ins.InsRadniNalogInterni(Convert.ToInt32(1), Convert.ToInt32(4), Convert.ToDateTime(DateTime.Now), Convert.ToDateTime("1.1.1900. 00:00:00"), "", Convert.ToInt32(0), "PlanUtovara", Convert.ToInt32(txtNadredjeni.Text), KorisnikTekuci, "");
            ins2.InsRadniNalogInterniIzvoz(Convert.ToInt32(4), Convert.ToInt32(4), Convert.ToDateTime(DateTime.Now), Convert.ToDateTime("1.1.1900. 00:00:00"), "", Convert.ToInt32(0), "PlanUtovaraT", Convert.ToInt32(PlanZaPrebacivanje), tKorisnik, "");
        }
    }
    }
 

