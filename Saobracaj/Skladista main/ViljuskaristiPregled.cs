using Microsoft.Office.Interop.Excel;
using Microsoft.ReportingServices.Diagnostics.Internal;
using Saobracaj.Dokumenta;
using Saobracaj.Izvoz;
using Saobracaj.Pantheon_Export;
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

namespace Saobracaj.Skladista_main
{
    public partial class ViljuskaristiPregled : Form
    {
        int brLONaloga = 0;
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        public ViljuskaristiPregled()
        {
            InitializeComponent();
        }
        public ViljuskaristiPregled(int BrLO)
        {
            InitializeComponent();
            brLONaloga = BrLO;
        }

        private void ViljuskaristiPregled_Load(object sender, EventArgs e)
        {
            FillGV();
        }

        private void FillGV()
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

            select = " select RNCarinskoSkladisteRukovalac.ID , RNCarinskoSkladisteRukovalac.Prijemnica, (Rtrim(DePriimek) + DeIme) as Rukovaoc , RNCarinskoSkladisteRukovalac.Status, " +
" RNCarinskoSkladisteRukovalac.Pozicija, Koleta, Paleta,TipPalete.Naziv as VPalete, Bruto, RNCarinskoSkladisteRukovalac.Napomena, " +
" RNCarinskoSkladisteRukovalac.Vozilo, Postupak, RNCarinskoSkladisteRukovalac.NalogIzdao, RNCarinskoSkladisteRukovalac.Uradjen, RadniNalogInterni.ID as NalogID, RadniNalogInterniSkladistePotvrda.SkladistarPotvrdio from RadniNalogSkladista " +
" inner join RadniNalogInterni on RadniNalogSkladista.ID = RadniNalogInterni.BrojRN " +
" inner join RadniNalogInterniSkladistePotvrda on RadniNalogInterniSkladistePotvrda.IDNaloga = RadniNalogInterni.ID " +
" inner join RNCarinskoSkladistePrijemnica on RadniNalogSkladista.ID = RNCarinskoSkladistePrijemnica.RN " +
" inner join RNCarinskoSkladisteRukovalac on  RNCarinskoSkladisteRukovalac.Prijemnica = RNCarinskoSkladistePrijemnica.ID " +
" left join TipPalete on RNCarinskoSkladisteRukovalac.PaletaTip = TipPalete.ID " +
" inner join Delavci on DeSifra = RNCarinskoSkladisteRukovalac.Rukovalac " +
" where RadniNalogSkladista.ID  = " + brLONaloga;


            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new System.Data.DataSet();
            dataAdapter.Fill(ds);
            this.gridGroupingControl2.Table.Records.DeleteAll();

            gridGroupingControl2.DataSource = ds.Tables[0];
            gridGroupingControl2.ShowGroupDropArea = true;
            this.gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;

            foreach (GridColumnDescriptor column in this.gridGroupingControl2.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }
            /*
            GridConditionalFormatDescriptor gcfd3 = new GridConditionalFormatDescriptor();
            gcfd3.Appearance.AnyRecordFieldCell.BackColor = Color.Red;
            gcfd3.Appearance.AnyRecordFieldCell.TextColor = Color.Yellow;

            gcfd3.Expression = "[KapijaUlaz] = '10'";
            this.gridGroupingControl2.TableDescriptor.ConditionalFormats.Add(gcfd3);
            */
            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            dynamicFilter.WireGrid(this.gridGroupingControl2);

        }

        private void button2_Click(object sender, EventArgs e)
        {
           FillGV();
        }

        int VratiNalogIDF()
        {
            int nalogID = 0;
            int ID = Convert.ToInt32(gridGroupingControl2.Table.CurrentRecord.GetValue("ID").ToString());
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("select ID from RadniNalogInterni where TipRN = 'RN20' and BrojRN =" + ID, conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        nalogID = Convert.ToInt32(dr["ID"].ToString());
                    }
                }
                conn.Close();
            }

            return nalogID;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int VratiNalogID = VratiNalogIDF();
            Saobracaj.Uvoz.InsertRadniNalogInterni ins1 = new Saobracaj.Uvoz.InsertRadniNalogInterni();
            int IDNajave = Convert.ToInt32(gridGroupingControl2.Table.CurrentRecord.GetValue("NalogID").ToString());
            ins1.UpdRadniNalogInterniIzvozSkladistarPotvrdio(Convert.ToInt32(IDNajave));
            MessageBox.Show("Skladistar potvrdio");
        }

        private void gridGroupingControl2_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
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
            int bn = Convert.ToInt32(gridGroupingControl2.Table.CurrentRecord.GetValue("ID").ToString());
            select = " select RNCarinskoSkladisteDodatneUsluge.* , VrstaManipulacije.Naziv as Usluga from RadniNalogSkladista " +
" inner join RNCarinskoSkladistePrijemnica on RadniNalogSkladista.ID = RNCarinskoSkladistePrijemnica.RN " +
" inner join RNCarinskoSkladisteDodatneUsluge on RNCarinskoSkladisteDodatneUsluge.RN = RadniNalogSkladista.ID " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = RNCarinskoSkladisteDodatneUsluge.Usluga " +
" where RadniNalogSkladista.ID =" + bn;


            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new System.Data.DataSet();
            dataAdapter.Fill(ds);
            this.gridGroupingControl1.Table.Records.DeleteAll();

            gridGroupingControl1.DataSource = ds.Tables[0];
            gridGroupingControl1.ShowGroupDropArea = true;
            this.gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;

            foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }
            /*
            GridConditionalFormatDescriptor gcfd3 = new GridConditionalFormatDescriptor();
            gcfd3.Appearance.AnyRecordFieldCell.BackColor = Color.Red;
            gcfd3.Appearance.AnyRecordFieldCell.TextColor = Color.Yellow;

            gcfd3.Expression = "[KapijaUlaz] = '10'";
            this.gridGroupingControl2.TableDescriptor.ConditionalFormats.Add(gcfd3);
            */
            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            dynamicFilter.WireGrid(this.gridGroupingControl1);
        }
    }
}
