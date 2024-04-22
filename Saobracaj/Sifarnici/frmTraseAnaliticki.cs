using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Saobracaj.Sifarnici
{
    public partial class frmTraseAnaliticki : Syncfusion.Windows.Forms.Office2010Form
    {
        public static string code = "frmTraseAnaliticki";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Sifarnici.frmLogovanje.user.ToString();
        string niz = "";
        public frmTraseAnaliticki()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            InitializeComponent();

        }
        private void frmTraseAnaliticki_Load(object sender, EventArgs e)
        {
            var select = "  Select Trase.ID, Trase.Godina, Trase.Pocetna as PocetnaID, stanice.Opis as Pocetna, Trase.Krajnja as KrajnjaID,st2.Opis as Krajnja, Trase.Relacija, Trase.OpisRelacije, Trase.Voz as Trasa, Trase.Rang, " +
            " Trase.TezinaVoza, Trase.TezinaLokomotive, Trase.DuzinaVoza, Trase.ProcenatKocenja, Trase.Cena, Trase.CenaKalk, Trase.Rastojanje, Trase.RastojanjeMag, Trase.RastojanjeReg, Trase.RastojanjeLok, Trase.NajveciOtpor, Trase.NajkraciKol, Trase.OsovinskoOpter " +
            " ,Trase.TipED, Trase.ElektroKM, Trase.DizelKM, Trase.PrevoznoRastojanje from Trase " +
            " inner join stanice on Trase.Pocetna = stanice.ID " +
            " inner join stanice st2 on Trase.Krajnja = st2.ID";

            var s_connection = ConfigurationManager.ConnectionStrings["Saobracaj.Properties.Settings.TestiranjeConnectionString"].ConnectionString;
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
