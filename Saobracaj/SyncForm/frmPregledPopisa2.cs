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
    public partial class frmPregledPopisa2 : Syncfusion.Windows.Forms.Office2010Form
    {
        public frmPregledPopisa2()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzcwMDg5QDMxMzgyZTM0MmUzMFhQSmlDM0M2bGpxcXVtT1VScTg1a0dtVTFLcUZiK0tLRnpvRTYyRFpMc3M9");
            InitializeComponent();
        }

        private void frmPregledPopisa2_Load(object sender, EventArgs e)
        {
            var select = "  Select top 500 t1.IDLokomotivaPrijava, t1.Lokomotiva, LokomotivaPrijava.Zaposleni as ZaposleniID, (RTrim(Delavci.DePriimek) + ' ' + Rtrim(Delavci.DeIme)) as Zaposleni, LokomotivaPopis.Kolicina, " +
            " LokomotiveVrstePopisa.ReferentnaKolicina, LokomotivaPopis.Vreme, " +
            " LokomotivaPopis.Napomena, LokomotiveVrstePopisa.ID as IDVrstePopisa, " +
            " VrstaPopisa.Naziv as VrstaPopisa  from(select LokomotivaPrijava.ID as IDLokomotivaPrijava," +
            "  LokomotivaPrijava.Lokomotiva  from LokomotivaPrijava" +
           " where LokomotivaPrijava.Smer = 0 " +
                                        "     ) t1 " +
                                         "   inner join LokomotivaPrijava on LokomotivaPrijava.ID = t1.IDLokomotivaPrijava " +
           " inner join Delavci on Delavci.DeSifra = LokomotivaPrijava.Zaposleni " +
           " inner join LokomotivaPopis on t1.IDLokomotivaPrijava = LokomotivaPopis.LokomotivaPrijavaID " +
           " inner join LokomotiveVrstePopisa on LokomotiveVrstePopisa.VrstaPopisaID = LokomotivaPopis.VrstaPopisaID " +
           " inner join VrstaPopisa on VrstaPopisa.ID = LokomotiveVrstePopisa.ID" +
           " order by  t1.IDLokomotivaPrijava desc";

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
