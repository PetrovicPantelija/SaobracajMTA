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
    public partial class frmAnalizaAktivnosti : Syncfusion.Windows.Forms.Office2010Form
    {
        public frmAnalizaAktivnosti()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzcwMDg5QDMxMzgyZTM0MmUzMFhQSmlDM0M2bGpxcXVtT1VScTg1a0dtVTFLcUZiK0tLRnpvRTYyRFpMc3M9");
            InitializeComponent();
        }

        private void frmAnalizaAktivnosti_Load(object sender, EventArgs e)
        {

            var select = "  Select Aktivnosti.ID as ID, " +
" (Rtrim(Delavci.DePriimek) + ' ' + Rtrim(Delavci.DeIme)) as Zaposleni, VremeOd, " +
" VremeDo, Aktivnosti.Kartica, Aktivnosti.RAcun, Aktivnosti.UkupniTroskovi, Aktivnosti.Ukupno ,Aktivnosti.Opis as OpisZaglavlje, VrstaAktivnosti.Naziv as VrstaAktivnosti, " +
"  AktivnostiStavke.BrojVagona, AktivnostiStavke.Sati, AktivnostiStavke.VrstaAktivnostiID , " +
"  AktivnostiStavke.Napomena, AktivnostiStavke.Razlog, " +
"  AktivnostiStavke.Koeficijent, AktivnostiStavke.Posao as IDPosla, " +
"  (Rtrim(Nal.DePriimek) + ' ' + Rtrim(Nal.DeIme)) as Zaposleni " +
"  from Aktivnosti " +
"  inner join Delavci on Aktivnosti.Zaposleni = Delavci.DeSifra " +
"  inner " +
"  join AktivnostiStavke on AktivnostiStavke.IDNadredjena = Aktivnosti.ID " +
"  inner " +
"  join VrstaAktivnosti on VrstaAktivnosti.ID = AktivnostiStavke.VrstaAktivnostiID " +
"  inner " +
"  join Delavci nal on AktivnostiStavke.Nalogodavac = nal.DeSifra " +
"  order by Aktivnosti.ID ";

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
