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
    public partial class frmAnalizaPorudzbina : Syncfusion.Windows.Forms.Office2010Form
    {
        public frmAnalizaPorudzbina()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzcwMDg5QDMxMzgyZTM0MmUzMFhQSmlDM0M2bGpxcXVtT1VScTg1a0dtVTFLcUZiK0tLRnpvRTYyRFpMc3M9");
            InitializeComponent();
        }

        private void frmAnalizaPorudzbina_Load(object sender, EventArgs e)
        {
            var select = "  Select NaPartPlac,   PaNaziv, PaUlicaHisnaSt , NaPNarZap, NaPNaziv, NaPEM,NaPKolNar,NaPKolNar2,  NaPOpomba, " +
            " Narocilo.NaStNar from Narocilo inner join Partnerji on Narocilo.NaPartPlac = PArtnerji.PaSifra " +
            " inner join NarociloPostav on Narocilo.NaStNar = NarociloPostav.NaPStNar " +
            " where NaStatus in ('PO') " +
            " order by PaNaziv ";

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
