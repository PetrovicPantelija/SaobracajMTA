using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Data;
using Syncfusion.Drawing;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Grouping;

namespace Saobracaj.Mobile
{
    public partial class frmAnalizaGO : Syncfusion.Windows.Forms.Office2010Form
    {
        public frmAnalizaGO()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzcwMDg5QDMxMzgyZTM0MmUzMFhQSmlDM0M2bGpxcXVtT1VScTg1a0dtVTFLcUZiK0tLRnpvRTYyRFpMc3M9");
            InitializeComponent();
        }

        private void frmAnalizaGO_Load(object sender, EventArgs e)
        {
            var select = "  Select DopustStavke.ID as StavkaID, DoLeto as Godina, DoSifDe as SifraRadnik, (Rtrim(Delavci.DeIme) + ' ' + RTrim(Delavci.DePriimek)) as Radnik, VremeOd, VremeDo, Ukupno, Napomena, Razlog,  " +
            " StatusGodmora,Odobrio as SifraOdobrio, (Rtrim(Delavci.DeIme) + ' ' + RTrim(Delavci.DePriimek)) as Odobrio from DopustStavke " +
            " inner join Dopust on Dopust.DoStZapisa = DopustStavke.IdNadredjena " +
            " inner  join Delavci on Delavci.DeSifra = DoSifDe " +
            " inner" +
            " join Delavci o on o.DeSifra = DoSifDe ";

            var s_connection = ConfigurationManager.ConnectionStrings["Saobracaj.Properties.Settings.Perftech_BeogradConnectionString"].ConnectionString;
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
        }
    }
    }

