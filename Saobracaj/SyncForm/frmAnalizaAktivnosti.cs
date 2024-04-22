using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Saobracaj.SyncForm
{
    public partial class frmAnalizaAktivnosti : Syncfusion.Windows.Forms.Office2010Form
    {
        public frmAnalizaAktivnosti()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            InitializeComponent();
        }
        public static string code = "frmAnalizaAktivnosti";

        private void frmAnalizaAktivnosti_Load(object sender, EventArgs e)
        {

            var select = "   Select Top 5000 Aktivnosti.ID as ID,  (Rtrim(Delavci.DeIme) + ' ' + Rtrim(Delavci.DePriimek)) as Zaposleni, VremeOd, " +
    " VremeDo, Aktivnosti.Kartica, Aktivnosti.RAcun, Aktivnosti.UkupniTroskovi, Aktivnosti.Ukupno ,Aktivnosti.Opis as OpisZaglavlje, VrstaAktivnosti.Naziv as VrstaAktivnosti,  " +
    "   AktivnostiStavke.Sati, AktivnostiStavke.VrstaAktivnostiID ,   AktivnostiStavke.Napomena,  AktivnostiStavke.Koeficijent, AktivnostiStavke.Posao as IDPosla " +
    "  from Aktivnosti inner join Delavci on Aktivnosti.Zaposleni = Delavci.DeSifra " +
    " inner     join AktivnostiStavke on AktivnostiStavke.IDNadredjena = Aktivnosti.ID " +
    " inner     join VrstaAktivnosti on VrstaAktivnosti.ID = AktivnostiStavke.VrstaAktivnostiID " +
    " order by Aktivnosti.ID desc";

            var s_connection = ConfigurationManager.ConnectionStrings["Saobracaj.Properties.Settings.TESTIRANJEConnectionString"].ConnectionString;
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
