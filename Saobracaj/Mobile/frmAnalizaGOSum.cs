using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Saobracaj.Mobile
{
    public partial class frmAnalizaGOSum : Syncfusion.Windows.Forms.Office2010Form
    {
        public frmAnalizaGOSum()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            InitializeComponent();

        }


        private void frmAnalizaGOSum_Load(object sender, EventArgs e)
        {

            var select = " Select RazporeditveStrMesto.RzSMSifStrMesta as MestoTroska, DelovnaMesta.DmNaziv as RadnoMesto, SUM(DoSkupaj) as Dana, (CASE WHEN Sum(Iskorisceno) IS NULL THEN 0 ELSE Sum(Iskorisceno) END) as Iskorisceno, (SUM(DoSkupaj) - (CASE WHEN Sum(Iskorisceno) IS NULL THEN 0 ELSE Sum(Iskorisceno) END)) as Preostalo, (Rtrim(Delavci.DeIme) + ' ' + Rtrim(Delavci.DePriimek)) as Zaposleni from Dopust " +
             "   inner join Delavci on Delavci.DeSifra = Dopust.DoSifDe " +
             "   inner join DelovnaMesta on DelovnaMesta.DmSifra = Delavci.DeSifDelMes " +
             "   inner join RazporeditveStrMesto on RazporeditveStrMesto.RzSMSifDe = Delavci.DeSifra " +
             "   left join(select SUM(Ukupno) as Iskorisceno, Delavci.DeSifra from DopustStavke " +
             "   inner join Dopust on Dopust.DoStZapisa = DopustStavke.IDNadredjena " +
             "   inner join Delavci on Delavci.DeSifra = Dopust.DoSifDe " +
             "   Where DeSifStat IN('A', 'R') " +
             "   group by Delavci.DeSifra) t1 on T1.DeSifra = Delavci.DeSifra " +
             "   group by(Rtrim(Delavci.DeIme) +' ' + Rtrim(Delavci.DePriimek)), RazporeditveStrMesto.RzSMSifStrMesta, DelovnaMesta.DmNaziv ";



            var s_connection = ConfigurationManager.ConnectionStrings["Saobracaj.Properties.Settings.TESTIRANJEConnectionString"].ConnectionString;
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

        }
    }
}

