using Microsoft.Office.Interop.Excel;
using Saobracaj.Dokumenta;
using Saobracaj.Izvoz;
using Saobracaj.RadniNalozi;
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
using System.Security.Cryptography;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Saobracaj.Kapija
{
    public partial class frmKapijaOtvoreniNalozi : Form
    {
        public frmKapijaOtvoreniNalozi()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var select = "";
            /*
                 select = "  select Distinct RadniNalogInterni.PlanID, UvozKonacna.BrojKontejnera, Scenario.Naziv, 'Uvozni' as OJ from RadniNalogInterni " +
               "  inner join UvozKonacna on RadniNalogInterni.BrojOsnov = UvozKonacna.ID inner join Scenario on UvozKonacna.Scenario = Scenario.ID " +
               "  where Uradjen not in (1, 2) " +
                " union " +
               "  select Distinct RadniNalogInterni.PlanID, IzvozKonacna.BrojKontejnera, Scenario.Naziv , 'Izvozni' as OJ  from RadniNalogInterni " +
               "  inner join IzvozKonacna on RadniNalogInterni.BrojOsnov = IzvozKonacna.ID " +
               "  inner   join Scenario on IzvozKonacna.Scenario = Scenario.ID " +
               "  where Uradjen not in (1, 2)";
            */
          
                select = "     select RadniNalogInterni.ID as KomNalogID, 'Izvoz' as Izvor, 'DOLAZAK' as Smer,  KorisnikIzdao, IZvozKonacna.BrojKontejnera, " +
" TipKontenjera.SkNaziv as VrstaKontejnera, " +
" (Select Top 1 Scenario.Naziv from Scenario where Scenario.ID = IzvozKonacna.Scenario) as SC, " +
"  CASE Drumski  WHEN 0 THEN ' '  WHEN 1 THEN 'L'  END AS Drumski,   " +
" CASE Cirada " +
"  WHEN 0 THEN 'PLATFORMA' " +
" WHEN 1 THEN 'CIRADA' " +
" END AS TipNaloga,  " +
" Vozilo, Vozac, BrojLK, BrojTelefona, PlaniranDtSpustanjaPunog as PlaniraniDatum, PlaniraniDtSpustanjaKontejnera as NoviDatum, GETDATE() as Datum, BrojStavkePorudzbenice, KapijaUlaz from RadniNalogInterni " +
" inner join RadniNalogInterniPotvrda on RadniNalogInterni.ID = RadniNalogInterniPotvrda.IDNaloga " +
" inner join IzvozKonacna on IzvozKonacna.ID = RadniNalogInterni.BrojOsnov " +
" inner join TipKontenjera on TipKontenjera.ID = IzvozKonacna.VrstaKontejnera " +
" where KapijaUlaz = 0 or KapijaUlaz = 10 " + 
"    union  select RadniNalogInterni.ID as KomNalogID, 'Izvoz' as Izvor, 'ODLAZAK',  KorisnikIzdao, IZvozKonacna.BrojKontejnera, " +
" TipKontenjera.SkNaziv as VrstaKontejnera, " +
" (Select Top 1 Scenario.Naziv from Scenario where Scenario.ID = IzvozKonacna.Scenario) as SC, " +
" CASE Drumski " +
" WHEN 0 THEN ' ' " +
" WHEN 1 THEN 'L' " +
" END AS Drumski,  " +
" CASE Cirada " +
"  WHEN 0 THEN 'PLATFORMA' " +
" WHEN 1 THEN 'CIRADA' " +
" END AS TipNaloga,  " +
 "       Vozilo, Vozac, BrojLK, BrojTelefona, PlaniranDtPreuzimanjaPunog, DtPreuzimanjaPunog, GETDATE() as Datum, BrojStavkePorudzbenice, KapijaUlaz  from RadniNalogInterni " +
 "       inner join RadniNalogInterniPotvrda on RadniNalogInterni.ID = RadniNalogInterniPotvrda.IDNaloga " +
 "       inner join IzvozKonacna on IzvozKonacna.ID = RadniNalogInterni.BrojOsnov " +
 "       inner join TipKontenjera on TipKontenjera.ID = IzvozKonacna.VrstaKontejnera " +
 "        where Kalmar = 1 and KapijaIzlaz = 0"; ;


            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            this.gridGroupingControl2.Table.Records.DeleteAll();

            gridGroupingControl2.DataSource = ds.Tables[0];
            gridGroupingControl2.ShowGroupDropArea = true;
            this.gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;

            foreach (GridColumnDescriptor column in this.gridGroupingControl2.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }

            GridConditionalFormatDescriptor gcfd3 = new GridConditionalFormatDescriptor();
            gcfd3.Appearance.AnyRecordFieldCell.BackColor = Color.Red;
            gcfd3.Appearance.AnyRecordFieldCell.TextColor = Color.Yellow;

            gcfd3.Expression = "[KapijaUlaz] = '10'";
            this.gridGroupingControl2.TableDescriptor.ConditionalFormats.Add(gcfd3);

            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            dynamicFilter.WireGrid(this.gridGroupingControl2);

        }

        private void button27_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtID.Enabled = false;
            string kor = Sifarnici.frmLogovanje.user;
            string vozac = "";
            string registarskibroj = "";
            string kontakt = "";
            string kontaktunutarfirme = "";
            int NalogID = 0;
            if (gridGroupingControl2.Table.CurrentRecord != null)
                {
                vozac = gridGroupingControl2.Table.CurrentRecord.GetValue("Vozac").ToString();
                registarskibroj = gridGroupingControl2.Table.CurrentRecord.GetValue("Vozilo").ToString();
                kontakt = gridGroupingControl2.Table.CurrentRecord.GetValue("BrojTelefona").ToString();
                NalogID = Convert.ToInt32(gridGroupingControl2.Table.CurrentRecord.GetValue("KomNalogID").ToString());
            }



            InsertKapija ins = new InsertKapija();
                int noviID = ins.InsKapija(1, vozac, registarskibroj, kontakt, "Izvoz", null, kontakt, kor, NalogID);
                txtID.Text = noviID.ToString();
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                MessageBox.Show("Izaberite zapis");
                return;
            }
            InsertKapija ukn = new InsertKapija();
            ukn.UpdeteKapijaNeslaganje(Convert.ToInt32(txtID.Text));
        }

        private void gridGroupingControl2_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            if (gridGroupingControl2.Table.CurrentRecord != null)
            {
                txtID.Text = gridGroupingControl2.Table.CurrentRecord.GetValue("KomNalogID").ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Kor = Sifarnici.frmLogovanje.user;
            foreach (SelectedRecord selectedRecord in this.gridGroupingControl2.Table.SelectedRecords)
            {
                InsertRadniNalogInterni ir = new InsertRadniNalogInterni();
                ir.PromeniStatusKapijaOdlazak(Convert.ToInt32(selectedRecord.Record.GetValue("KomNalogID").ToString()), Kor);

            }
        }
    }
}
