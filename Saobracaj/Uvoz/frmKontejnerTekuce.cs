using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Saobracaj.Uvoz
{
    public partial class frmKontejnerTekuce : Syncfusion.Windows.Forms.Office2010Form
    {
        public frmKontejnerTekuce()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var select = "";

            select = "  Select KontejnerTekuce.Kontejner, KontejnerStatus.Naziv as Status, KontejnerTekuce.Pokret as Pokret, Skladista.Naziv as Skladiste, " +
" Skladista.Kapacitet, Pozicija.Opis as Pozicija, VizuelniNapomena, " +
" Ostecenja as Ispravnost, KontejnerskiTerminali.Naziv as RLSRB, Kvalitet, CIR, STATUSIzvoz , " +
" PArtnerji.PaNaziv as Brodar, puv.PaNAziv as Uvoznik, UlazniBroj as KontejnerID_UVoz, DatumGIN as DATUM_GATEIN, DatumOtpremaPlat  as DATUM_OTP_PLAT, OperacijaUradjena as ZadnjaOperacija, Skladisnina, SkladisninaOd, SkladisninaDo " +
" from KontejnerTekuce " +
" inner join KontejnerStatus on KontejnerStatus.ID = KontejnerTekuce.StatusKontejnera " +
" inner " +
" join Skladista on Skladista.Id = KontejnerTekuce.Skladiste " +
" inner " +
" join Pozicija on Pozicija.Id = KontejnerTekuce.Pozicija " +
" inner " +
" join PArtnerji on KontejnerTekuce.Brodar = Partnerji.PaSifra " +
" inner " +
" join PArtnerji as PUV on KontejnerTekuce.Uvoznik = PUV.PASifra " +
" left " +
" join KontejnerskiTerminali on KontejnerskiTerminali.ID = KOntejnerTekuce.RLSRB";

            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl1.DataSource = ds.Tables[0];
            gridGroupingControl1.ShowGroupDropArea = true;
            this.gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;
            foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }
            /*
            GridConditionalFormatDescriptor gcfd = new GridConditionalFormatDescriptor();
            gcfd.Appearance.AnyRecordFieldCell.BackColor = Color.Green;
            gcfd.Appearance.AnyRecordFieldCell.TextColor = Color.White;

            gcfd.Expression = "[StatusIzdavanja] =  'DOPUNA'";

            //To add the conditional format instances to the ConditionalFormats collection. 
            this.gridGroupingControl1.TableDescriptor.ConditionalFormats.Add(gcfd);

            GridConditionalFormatDescriptor gcfd2 = new GridConditionalFormatDescriptor();
            gcfd2.Appearance.AnyRecordFieldCell.BackColor = Color.DarkRed;
            gcfd2.Appearance.AnyRecordFieldCell.TextColor = Color.White;

            gcfd2.Expression = "[StatusIzdavanja] =  'STORNO'";

            //To add the conditional format instances to the ConditionalFormats collection. 
            this.gridGroupingControl1.TableDescriptor.ConditionalFormats.Add(gcfd2);
            this.gridGroupingControl1.TableDescriptor.Columns[0].Width = 30;
            this.gridGroupingControl1.TableDescriptor.Columns[1].Width = 70;

            this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("OJIzdavanja");
            this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("OJRealizacije");
            this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("IDVrstaManipulacije");

            */
            /*
            GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor.Appearance.AnySummaryCell.Interior = new BrushInfo(Color.FromArgb(192, 255, 162));
            summaryColumnDescriptor.DataMember = "Uradjen";
            summaryColumnDescriptor.Format = "{Sum}";
            summaryColumnDescriptor.Name = "Uradjeno";
            summaryColumnDescriptor.SummaryType = Syncfusion.Grouping.SummaryType.Int32Aggregate;

            GridSummaryRowDescriptor summaryRowDescriptor = new GridSummaryRowDescriptor();
            summaryRowDescriptor.SummaryColumns.Add(summaryColumnDescriptor);
            summaryRowDescriptor.Appearance.AnySummaryCell.Interior = new BrushInfo(Color.FromArgb(255, 231, 162));

            this.gridGroupingControl1.TableDescriptor.SummaryRows.Add(summaryRowDescriptor);
            */
        }
    }
}
