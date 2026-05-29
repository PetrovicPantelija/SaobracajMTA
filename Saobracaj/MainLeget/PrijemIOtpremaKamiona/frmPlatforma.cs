using GMap.NET.MapProviders;
using Microsoft.Office.Interop.Excel;
using Saobracaj.Dokumenta;
using Saobracaj.Drumski;
using Saobracaj.Izvoz;
using Saobracaj.RadniNalozi;
using Saobracaj.Uvoz;
using Syncfusion.GridHelperClasses;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.XlsIO.Implementation.XmlSerialization;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Cryptography;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Saobracaj.MainLeget.PrijemIOtpremaKamiona
{
    public partial class frmPlatforma : Form
    {
        public frmPlatforma()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
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

            select = "     SELECT RadniNalogInterni.[ID] as KomNalID, RadniNalogInterni.BrojOsnov as KontID," + 
" DatumPrijema as DatumPrijema, " +
"  CASE WHEN n1.StatusPrijema = 0 THEN '1-Najava' ELSE '2-Prijem' END as Status,  " +
"  REgBrKamiona, ImeVozaca, " +
"  n1.VremeDolaska as VremeDol,  " +
"  n1.[Datum] ,n1.[Korisnik] ,  RadniNalogInterniPotvrda.KapijaUlaz, RadniNalogInterniPotvrda.Pregledac," +

 "  (SELECT  STUFF((SELECT distinct   '/ ' + Cast(ts.BrojKontejnera as nvarchar(20)) " +
 "  FROM PrijemKontejneraVozStavke ts where n1.ID = ts.IDNadredjenog " +
"  FOR XML PATH('')), 1, 1, ''  ) As Skupljen)  " +
 "  as Kontejner , OrganizacioneJedinice.Naziv as Modul,  CASE WHEN n1.Poreklo = 0 THEN 'PLATFORMA' ELSE 'CIRADA' END as POREKLO " +
"  FROM[dbo].[PrijemKontejneraVoz] as n1 " +
"  inner join organizacioneJedinice on OrganizacioneJedinice.ID = n1.Modul " +
"  inner join PrijemKontejneraVozStavke on PrijemKontejneraVozStavke.IDNadredjenog = n1.ID " +
"  inner join RadniNalogInterni on RadniNalogInterni.ID = PrijemKontejneraVozStavke.NajavaID " +
"  inner join RadniNalogInterniPotvrda on RadniNalogInterni.ID = RadniNalogInterniPotvrda.IDNaloga " +
"  where Vozom = 0  and RadniNalogInterniPotvrda.Kamion = 0 and (Pregledac = 1 or Pregledac = 2)  order by n1.ID desc";


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



            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            dynamicFilter.WireGrid(this.gridGroupingControl2);
        }

        private void gridGroupingControl2_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {

        }
        int VratiPrijemnicu(int KomNalID)
        {
            int PrijemID = 0;
            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter("Select ID from PrijemKontejneraVoz where NajavaID=" + KomNalID, c);
            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                PrijemID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
            return PrijemID;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int PrijemID = VratiPrijemnicu(Convert.ToInt32(this.gridGroupingControl2.Table.SelectedRecords[0].Record.GetValue("KomNalID").ToString()));
            Saobracaj.RadniNalozi.frmDodelaSkladista ds = new frmDodelaSkladista(PrijemID.ToString(), 2);
            ds.Show();

            foreach (SelectedRecord selectedRecord in this.gridGroupingControl2.Table.SelectedRecords)
            {
                RadniNalozi.InsertRN ir = new InsertRN();
                ir.UpdRNPrijemPlatformeKamPusti(Convert.ToInt32(selectedRecord.Record.GetValue("KomNalID").ToString()));

            }
        }

        private void frmPlatforma_Load(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            foreach (SelectedRecord selectedRecord in this.gridGroupingControl2.Table.SelectedRecords)
            {
                frmPrijemKamionaDetalji frm = new frmPrijemKamionaDetalji(Convert.ToInt32(selectedRecord.Record.GetValue("KontID").ToString()));
                frm.Show();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // use the selectedRecord variable and Convert.ToString to avoid NRE
if (gridGroupingControl2.Table.SelectedRecords.Count > 0)
{
    var rec = gridGroupingControl2.Table.SelectedRecords[0].Record;
    var bkontejnera = Convert.ToString(rec.GetValue("Kontejner"));
    var vozilo = Convert.ToString(rec.GetValue("REgBrKamiona")); // or "RegBrKamiona" if SQL fixed
    var vozac = Convert.ToString(rec.GetValue("ImeVozaca"));
    var kontId = Convert.ToInt32(rec.GetValue("KontID") ?? 0);
    var vag = new Vaganje(bkontejnera, kontId, vozilo, vozac);
    vag.Show();
}
        }

        private void button27_Click(object sender, EventArgs e)
        {
            foreach (SelectedRecord selectedRecord in this.gridGroupingControl2.Table.SelectedRecords)
            {
                InsertRadniNalogInterni ir = new InsertRadniNalogInterni();
                ir.PromeniStatusKapija(Convert.ToInt32(selectedRecord.Record.GetValue("KomNalID").ToString()));

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (SelectedRecord selectedRecord in this.gridGroupingControl2.Table.SelectedRecords)
            {
                InsertRadniNalogInterni ir = new InsertRadniNalogInterni();
                ir.PromeniStatusPregledac(Convert.ToInt32(selectedRecord.Record.GetValue("KomNalID").ToString()));

            }
        }
    }
}
