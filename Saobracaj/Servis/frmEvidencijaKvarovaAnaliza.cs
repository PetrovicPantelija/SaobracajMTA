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

namespace Saobracaj.Servis
{
    public partial class frmEvidencijaKvarovaAnaliza : Syncfusion.Windows.Forms.Office2010Form
    {
        public frmEvidencijaKvarovaAnaliza()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzcwMDg5QDMxMzgyZTM0MmUzMFhQSmlDM0M2bGpxcXVtT1VScTg1a0dtVTFLcUZiK0tLRnpvRTYyRFpMc3M9");
            InitializeComponent();
        }

        private void frmEvidencijaKvarovaAnaliza_Load(object sender, EventArgs e)
        {
            var select = "  select EvidencijaKvarova.ID, Kvarovi.ID, Lokomotiva, Kvarovi.Naziv as Kvar, GrupaKvarova.Naziv as GrupaKvarova, DatumPrijave, Rtrim(Delavci.DePriimek) + ' ' + Rtrim(Delavci.DeIme) as Prijavio,  StatusKvara.Naziv as Status, Rtrim(D2.DePriimek) + ' ' + Rtrim(D2.DeIme) as Prijavio, DatumPromene, Napomena  from EvidencijaKvarova " +
             " inner join Delavci on Delavci.DeSifra = EvidencijaKvarova.Prijavio " +
             " inner join Delavci d2 on d2.DeSifra = EvidencijaKvarova.Promenio " +
             " inner join Kvarovi on Kvarovi.ID = EvidencijaKvarova.Kvar " +
             " inner join StatusKvara on EvidencijaKvarova.StatusKvara = StatusKvara.ID " +
             " inner join GrupaKvarova on Kvarovi.GrupaKvarovaID = GrupaKvarova.ID " ;

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
