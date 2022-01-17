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

namespace Saobracaj.SyncForm
{
    public partial class frmPregledMobilni : Syncfusion.Windows.Forms.Office2010Form
    {
        public frmPregledMobilni()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzcwMDg5QDMxMzgyZTM0MmUzMFhQSmlDM0M2bGpxcXVtT1VScTg1a0dtVTFLcUZiK0tLRnpvRTYyRFpMc3M9");
            InitializeComponent();
        }

        private void frmPregledMobilni_Load(object sender, EventArgs e)
        {
            var select = "  Select ZaposleniPrijava.ID,Zaposleni as ZaposleniID, Rtrim(DePriimek) + ' ' + Rtrim(DeIme) as Zaposleni,DatumPrijave, DatumOdjave, AktivnostID, LongPrijave, LatPrijave, LongOdjave, LatOdjave from ZaposleniPrijava " +
          " inner join Delavci on DeSifra = ZaposleniPrijava.Zaposleni order by ZaposleniPrijava.ID desc ";


            var s_connection = ConfigurationManager.ConnectionStrings["Saobracaj.Properties.Settings.Perftech_BeogradConnectionString"].ConnectionString;
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
    }
}
