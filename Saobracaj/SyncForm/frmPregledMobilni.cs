using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Saobracaj.SyncForm
{
    public partial class frmPregledMobilni : Syncfusion.Windows.Forms.Office2010Form
    {
        public frmPregledMobilni()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            InitializeComponent();

        }
        string niz = "";
        public static string code = "frmPregledMobilni";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Sifarnici.frmLogovanje.user.ToString();

        private void frmPregledMobilni_Load(object sender, EventArgs e)
        {
            var select = "  Select ZaposleniPrijava.ID,Zaposleni as ZaposleniID, Rtrim(DeIme) + ' ' + Rtrim(DePriimek) as Zaposleni,DatumPrijave, DatumOdjave, AktivnostID, LongPrijave, LatPrijave, LongOdjave, LatOdjave from ZaposleniPrijava " +
          " inner join Delavci on DeSifra = ZaposleniPrijava.Zaposleni order by ZaposleniPrijava.ID desc ";


            var s_connection = Sifarnici.frmLogovanje.connectionString;
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
