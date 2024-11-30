using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Dokumenta
{
    public partial class frmVozilaPregledPrijava : Syncfusion.Windows.Forms.Office2010Form
    {
        public frmVozilaPregledPrijava()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzcwMDg5QDMxMzgyZTM0MmUzMFhQSmlDM0M2bGpxcXVtT1VScTg1a0dtVTFLcUZiK0tLRnpvRTYyRFpMc3M9");
        }

        private void frmVozilaPregledPrijava_Load(object sender, EventArgs e)
        {
            var select = "   SELECT     ZaposleniPrijavaAuto.Id, Delavci.DeStaraSif, (RTRIM(Delavci.DeIme) + '  ' + RTRIM(Delavci.DePriimek)) AS Zaposleni, " +
       " ZaposleniPrijavaAuto.DatumPrijave, ZaposleniPrijavaAuto.DatumOdjave, ZaposleniPrijavaAuto.AutomobilId, Vozila.Naziv " +
      " FROM         ZaposleniPrijavaAuto INNER JOIN " +
      " Delavci ON ZaposleniPrijavaAuto.Zaposleni = Delavci.DeSifra INNER JOIN " +
      " Vozila ON ZaposleniPrijavaAuto.AutomobilId = Vozila.ID " +
      " where ZaposleniPrijavaAuto.DatumPrijave > DateADD(DAY, -45, GetDAte())  and ZaposleniPrijavaAuto.Uloga = 'Kalmaristi' " +
      " ORDER BY ZaposleniPrijavaAuto.Id DESC ";

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
              GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
              summaryColumnDescriptor.Appearance.AnySummaryCell.Interior = new BrushInfo(Color.FromArgb(192, 255, 162));
              summaryColumnDescriptor.DataMember = "Ukupno";
              summaryColumnDescriptor.Format = "{Sum}";
              summaryColumnDescriptor.Name = "Potrošeno";
              summaryColumnDescriptor.SummaryType = Syncfusion.Grouping.SummaryType.Int32Aggregate;

              GridSummaryRowDescriptor summaryRowDescriptor = new GridSummaryRowDescriptor();
              summaryRowDescriptor.SummaryColumns.Add(summaryColumnDescriptor);
              summaryRowDescriptor.Appearance.AnySummaryCell.Interior = new BrushInfo(Color.FromArgb(255, 231, 162));

              this.gridGroupingControl1.TableDescriptor.SummaryRows.Add(summaryRowDescriptor);
            */
        }
    }
}
