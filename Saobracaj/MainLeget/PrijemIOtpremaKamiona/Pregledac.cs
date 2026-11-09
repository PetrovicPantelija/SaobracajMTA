using Microsoft.Office.Interop.Excel;
using Saobracaj.Dokumenta;
using Saobracaj.Izvoz;
using Saobracaj.RadniNalozi;
using Saobracaj.Skladista_main.Dokumenta;
using Saobracaj.Uvoz;
using Syncfusion.GridHelperClasses;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Net;
using System.Security.Cryptography;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Saobracaj.MainLeget.PrijemIOtpremaKamiona
{
    public partial class Pregledac : Form
    {
        public Pregledac()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            VratiPodatke();
        }

        private void VratiPodatke()
        {
            var select = "";
            /*
            select RadniNalogInterni.ID as KomNalogID, 'Izvoz' as Izvor, 'DOLAZAK' as Smer,  KorisnikIzdao, IZvozKonacna.BrojKontejnera,
TipKontenjera.SkNaziv as VrstaKontejnera, 
(Select Top 1 Scenario.Naziv from Scenario where Scenario.ID = IzvozKonacna.Scenario) as SC, 
CASE Drumski  WHEN 0 THEN ' '  WHEN 1 THEN 'L'  END AS Drumski,   
CASE Cirada
 WHEN 0 THEN 'PLATFORMA'
WHEN 1 THEN 'CIRADA'
 END AS TipNaloga, 
 Vozilo, Vozac, BrojLK, BrojTelefona, PlaniranDtSpustanjaPunog as PlaniraniDatum, PlaniraniDtSpustanjaKontejnera as NoviDatum, GETDATE() as Datum, 
 BrojStavkePorudzbenice, KapijaUlaz from RadniNalogInterni
inner join RadniNalogInterniPotvrda on RadniNalogInterni.ID = RadniNalogInterniPotvrda.IDNaloga
inner join IzvozKonacna on IzvozKonacna.ID = RadniNalogInterni.BrojOsnov
inner join TipKontenjera on TipKontenjera.ID = IzvozKonacna.VrstaKontejnera
            */
            select = "    select RadniNalogInterni.ID as KomNalogID, RadniNalogInterniPotvrda.FazaUsluge,ProdajniNalogIzvozStavke.IDNAdredjenog as Porudz,'Izvoz' as Izvor, 'DOLAZAK' as Smer,  KorisnikIzdao, IZvozKonacna.OpisPosla, IZvozKonacna.BrojKontejnera, " +
" TipKontenjera.SkNaziv as VrstaKontejnera, " +
" (Select Top 1 Scenario.Naziv from Scenario where Scenario.ID = IzvozKonacna.Scenario) as SC,  " +
" CASE IzvozKonacna.Drumski  WHEN 0 THEN ' '  WHEN 1 THEN 'L'  END AS DrumskiIma,  " +
" CASE Cirada " +
" WHEN 1 THEN 'PLATFORMA' " +
" WHEN 2 THEN 'CIRADA' " +
" END AS TipNaloga, IzvozKonacna.ID AS IzvozID, " +
" Partnerji.PANaziv as Brodar, IzvozKonacna.Tara, p1.PaNaziv as VlasnikBrodskaPlomba, BrodskaPlomba as BrojBrodskePlombe, OstalePlombe , " +
" PlaniranDtSpustanjaPunog as PlaniraniDatum, PlaniraniDtSpustanjaKontejnera as NoviDatum, Kapija.DatumDolaska as KapijaDolazak, BrojStavkePorudzbenice, KapijaUlaz, RadniNalogInterniPotvrda.Scenario, " +
"RadniNalogInterni.IDManipulacijaJed, RadniNalogInterniPotvrda.Pregledac ,  RadniNalogInterniPotvrda.Kalmar" +
" from RadniNalogInterni " +
" inner join RadniNalogInterniPotvrda on RadniNalogInterni.ID = RadniNalogInterniPotvrda.IDNaloga " +
" inner join IzvozKonacna on IzvozKonacna.ID = RadniNalogInterni.BrojOsnov " +
" inner join ProdajniNalogIzvozStavke on ProdajniNalogIzvozStavke.ID = IzvozKonacna.BrojStavkePorudzbenice " +
" inner join TipKontenjera on TipKontenjera.ID = IzvozKonacna.VrstaKontejnera " +
" inner join Partnerji on PArtnerji.PaSifra = IzvozKonacna.Brodar " +
" inner join Partnerji p1 on p1.PaSifra = IzvozKonacna.VrstaBrodskePlombe " +
" left join Kapija on Kapija.NalogID = RadniNalogInterni.ID " +
            /*" where  (KapijaUlaz = 0 or KapijaUlaz = 1) and Pregledac = 0 "; */
            "WHERE RadniNalogInterniPotvrda.FazaUsluge = 1 " +
            " AND ( " +
            //--Za sve standardne naloge (Scenario 1 i Druga faza Scenarija 2): Važi STARI uslov bez izmena
            " (/*(KapijaUlaz = 0 OR KapijaUlaz = 1)  AND */ RadniNalogInterni.IDManipulacijaJed <> 69 AND  RadniNalogInterniPotvrda.Pregledac = 0 )" +
            "  OR " +

           //Samo za Prvu fazu Scenarija 2 (ID manipulacije 69): Mora dodatno da sačekati Kalmara
           "(/*KapijaUlaz >= 1 AND */RadniNalogInterni.IDManipulacijaJed = 69  AND RadniNalogInterniPotvrda.Pregledac < 2 ) " +
   ")";


            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            this.gridGroupingControl2.Table.Records.DeleteAll();

            gridGroupingControl2.DataSource = ds.Tables[0];
            this.gridGroupingControl2.TableDescriptor.VisibleColumns.Remove("IzvozID");
            this.gridGroupingControl2.TableDescriptor.VisibleColumns.Remove("Scenario");
            this.gridGroupingControl2.TableDescriptor.VisibleColumns.Remove("IDManipulacijaJed");
            this.gridGroupingControl2.TableDescriptor.VisibleColumns.Remove("Pregledac");
            this.gridGroupingControl2.TableDescriptor.VisibleColumns.Remove("Kalmar");
            gridGroupingControl2.ShowGroupDropArea = true;
            this.gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;

            foreach (GridColumnDescriptor column in this.gridGroupingControl2.TableDescriptor.Columns)
            {
                column.AllowFilter = true;

            }




            //GridConditionalFormatDescriptor gcfd3 = new GridConditionalFormatDescriptor();
            //gcfd3.Appearance.AnyRecordFieldCell.BackColor = Color.Yellow;
            //gcfd3.Appearance.AnyRecordFieldCell.TextColor = Color.Black;

            //gcfd3.Expression = "[KapijaUlaz] = 1";
            //this.gridGroupingControl2.TableDescriptor.ConditionalFormats.Add(gcfd3);

            this.gridGroupingControl2.TableDescriptor.ConditionalFormats.Clear();

            // 1a. Pun (70)
            GridConditionalFormatDescriptor gcfdPun70 = new GridConditionalFormatDescriptor();
            gcfdPun70.Appearance.AnyRecordFieldCell.BackColor = Color.Yellow;
            gcfdPun70.Appearance.AnyRecordFieldCell.TextColor = Color.Black;
            gcfdPun70.Expression = "[IDManipulacijaJed] = 70 AND [KapijaUlaz] = 1 AND [Pregledac] = 0";

            // 1b. Pun (71)
            GridConditionalFormatDescriptor gcfdPun71 = new GridConditionalFormatDescriptor();
            gcfdPun71.Appearance.AnyRecordFieldCell.BackColor = Color.Yellow;
            gcfdPun71.Appearance.AnyRecordFieldCell.TextColor = Color.Black;
            gcfdPun71.Expression = "[IDManipulacijaJed] = 71 AND [KapijaUlaz] = 1 AND [Pregledac] = 0";

            // 2. Prazan (69) scenario 2
            GridConditionalFormatDescriptor gcfdPrazan2 = new GridConditionalFormatDescriptor();
            gcfdPrazan2.Appearance.AnyRecordFieldCell.BackColor = Color.Yellow;
            gcfdPrazan2.Appearance.AnyRecordFieldCell.TextColor = Color.Black;
            gcfdPrazan2.Expression = "[IDManipulacijaJed] = 69 AND [KapijaUlaz] = 2 AND [Kalmar] = 1 AND [Pregledac] = 1 AND [Scenario] = 2";


            // 2. Prazan (69) scenario 1
            GridConditionalFormatDescriptor gcfdPrazan1 = new GridConditionalFormatDescriptor();
            gcfdPrazan1.Appearance.AnyRecordFieldCell.BackColor = Color.Yellow;
            gcfdPrazan1.Appearance.AnyRecordFieldCell.TextColor = Color.Black;
            gcfdPrazan1.Expression = "[IDManipulacijaJed] = 69 AND [KapijaUlaz] = 2 AND [Kalmar] = 1 AND [Pregledac] = 0 AND [Scenario] = 1";

            // Dodavanje pravila
            this.gridGroupingControl2.TableDescriptor.ConditionalFormats.Add(gcfdPun70);
            this.gridGroupingControl2.TableDescriptor.ConditionalFormats.Add(gcfdPun71);
            this.gridGroupingControl2.TableDescriptor.ConditionalFormats.Add(gcfdPrazan2);
            this.gridGroupingControl2.TableDescriptor.ConditionalFormats.Add(gcfdPrazan1);

            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            dynamicFilter.WireGrid(this.gridGroupingControl2);
        }

        int VratiBrojOsnov(int NajavaID)
        {
            int pom = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" select BrojOsnov from RadniNalogInterni where ID = " + Convert.ToInt32(NajavaID), con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                pom = Convert.ToInt32(dr["BrojOsnov"].ToString());

            }
            con.Close();

            return pom;


        }

        int VratiRN(int NajavaID, int Scenario, int fazaUsluge, int idManipulacija)
        {
            int pom = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();
            SqlCommand cmd = new SqlCommand();
            if (Scenario == 2 && fazaUsluge == 1 && idManipulacija == 69)
            {
                 cmd = new SqlCommand(" select ID from RNOtpremaPlatforme where NalogID = " + Convert.ToInt32(NajavaID), con);
            }
            else 
            {
                 cmd = new SqlCommand(" select ID from RNPrijemPlatforme where NalogID = " + Convert.ToInt32(NajavaID), con);
            }

               
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
               pom = Convert.ToInt32(dr["ID"].ToString());

            }
            con.Close();

            return pom;


        }
        private void button1_Click(object sender, EventArgs e)
        {
            string Kor = Sifarnici.frmLogovanje.user;


            InsertRN up = new InsertRN();

            if (this.gridGroupingControl2.Table.SelectedRecords.Count > 0)
            {
                foreach (SelectedRecord selectedRecord in this.gridGroupingControl2.Table.SelectedRecords)
                {
                    int scenario = Convert.ToInt32(selectedRecord.Record.GetValue("Scenario"));
                    int fazaUsluge = Convert.ToInt32(selectedRecord.Record.GetValue("FazaUsluge"));
                    int idManipulacija = Convert.ToInt32(selectedRecord.Record.GetValue("IDManipulacijaJed")); 

                    //   up.UpdateRN4UradjeneVizuelni(BrojRN, Kor);
                    if (scenario == 2 && fazaUsluge == 1 && idManipulacija == 69)
                    {
                        int BrojRN = VratiRN(Convert.ToInt32(selectedRecord.Record.GetValue("KomNalogID").ToString()), scenario, fazaUsluge, idManipulacija);
                       up.UpdateRN6UradjeneVizuelni(BrojRN, Kor);
                    }
                    else
                    {
                        int BrojRN = VratiRN(Convert.ToInt32(selectedRecord.Record.GetValue("KomNalogID").ToString()), scenario, fazaUsluge, idManipulacija);
                        up.UpdateRN4UradjeneVizuelni(BrojRN, Kor);
                    }

                }
            }


            MessageBox.Show("Uspešno potvrđen vizuelni pregled!");
            VratiPodatke();


        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (this.gridGroupingControl2.Table.SelectedRecords.Count > 0)
            {
                foreach (SelectedRecord selectedRecord in this.gridGroupingControl2.Table.SelectedRecords)
                {
                    ZapisnikONepravilnosti zon = new ZapisnikONepravilnosti(selectedRecord.Record.GetValue("KomNalogID").ToString());
                    zon.Show();

                }
            }

         
        }
        int VratiPrijemID(int NajavaID, int Scenario, int fazaUsluge, int idManipulacija)
        {
            int pom = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("", con);
            if (Scenario == 2 && fazaUsluge == 1 && idManipulacija == 69)
            {
                cmd = new SqlCommand(" Select ID from OtpremaKontejneraVozStavke where NAjavaID = " + Convert.ToInt32(NajavaID), con);
            }
            else
            {
                cmd = new SqlCommand(" Select ID from PrijemKontejneraVozStavke where NAjavaID = " + Convert.ToInt32(NajavaID), con);
            }
         
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                pom = Convert.ToInt32(dr["ID"].ToString());

            }
            con.Close();

            return pom;


        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Kor = Sifarnici.frmLogovanje.user;
            string NalogID = "";
            if (this.gridGroupingControl2.Table.SelectedRecords.Count > 0)
            {  
         
                foreach (SelectedRecord selectedRecord in this.gridGroupingControl2.Table.SelectedRecords)
                {
                   InsertRN rn = new InsertRN();
                        NalogID = selectedRecord.Record.GetValue("KomNalogID").ToString();
                    
                    int idManipulacija = Convert.ToInt32(selectedRecord.Record.GetValue("IDManipulacijaJed"));
                    int scenario = Convert.ToInt32(selectedRecord.Record.GetValue("Scenario"));
                    string tipRN = "";
                    if (scenario == 2 && idManipulacija == 69)
                    {
                        rn.UpdateRN6PotrebanCIR(Convert.ToInt32(selectedRecord.Record.GetValue("KomNalogID").ToString()), Kor);
                        tipRN = "RN6";
                    }
                    else
                    {
                        rn.UpdateRN4PotrebanCIR(Convert.ToInt32(selectedRecord.Record.GetValue("KomNalogID").ToString()), Kor);
                        tipRN = "RN4";
                    }

                    DialogResult dialogResult = MessageBox.Show("Da li želite da napravite CIR u app?", "Izraditi CIR", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        
                        int fazaUsluge = Convert.ToInt32(selectedRecord.Record.GetValue("FazaUsluge"));
                        int BrojRN = VratiRN(Convert.ToInt32(NalogID), scenario, fazaUsluge, idManipulacija);
                        int PrijemID = VratiPrijemID(Convert.ToInt32(NalogID), scenario, fazaUsluge, idManipulacija);
                        frmCIR cir = new frmCIR(PrijemID, 1, tipRN, BrojRN, Convert.ToInt32(NalogID));
                        cir.Show();
                    }


                }
            }
            //DialogResult dialogResult = MessageBox.Show("Da li želite da napravite CIR u app?", "Izraditi CIR", MessageBoxButtons.YesNo);
            //if (dialogResult == DialogResult.Yes)
            //{
            //    int scenario = Convert.ToInt32(selectedRecord.Record.GetValue("Scenario"));
            //    int BrojRN = VratiRN(Convert.ToInt32(NalogID));
            //    int PrijemID = VratiPrijemID(Convert.ToInt32(NalogID));
            //    frmCIR cir = new frmCIR(PrijemID,1,"RN4", BrojRN, Convert.ToInt32(NalogID));
            //    cir.Show();
            //}
           
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int brojosnov = 0;
            foreach (SelectedRecord selectedRecord in this.gridGroupingControl2.Table.SelectedRecords)
            {

                brojosnov = VratiBrojOsnov(Convert.ToInt32(selectedRecord.Record.GetValue("KomNalogID").ToString()));
            }
             //= VratiBrojOsnov(Convert.ToInt32(this.gridGroupingControl2.Table.SelectedRecords[0].Record.GetValue("ID").ToString()));
            frmDodatneUsluge dd = new frmDodatneUsluge(brojosnov.ToString(),2);
            dd.Show();
        }

        private void Pregledac_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            int brojosnov = 0;
            foreach (SelectedRecord selectedRecord in this.gridGroupingControl2.Table.SelectedRecords)
            {

                brojosnov = VratiBrojOsnov(Convert.ToInt32(selectedRecord.Record.GetValue("KomNalogID").ToString()));
            }
            frmFotografijePregledac gp = new frmFotografijePregledac(brojosnov);
            gp.Show();
        }
    }
}
