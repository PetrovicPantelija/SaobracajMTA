using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace Saobracaj.SyncForm
{
    public partial class frmPregledMasinovodje : Syncfusion.Windows.Forms.Office2010Form
    {
        public frmPregledMasinovodje()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            InitializeComponent();

        }
        string niz = "";
        public static string code = "frmPregledMasinovodje";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Sifarnici.frmLogovanje.user.ToString();

        private void frmPregledMasinovodje_Load(object sender, EventArgs e)
        {
            var select = "  select Lokomotivaprijava.ID as ID, Lokomotiva, (select case when Smer = 1 then 'ODJAVA' else 'PRIJAVA' end) as Smer,Stanice.Opis as Stanica," +
                " LokomotivaPrijava.Datum, Rtrim(Delavci.DeIme)+ ' ' + Rtrim(Delavci.DePriimek) as Zaposleni," +
                " MotoSati, KM, Gorivo, Napomena, AktivnostID  from LokomotivaPrijava " +
                " inner join Delavci on Delavci.DeSifra = LokomotivaPrijava.Zaposleni " +
                " inner join Stanice on Stanice.ID = LokomotivaPrijava.Stanica order by LokomotivaPrijava.ID desc ";

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

        private void gridGroupingControl1_SelectedRecordsChanged(object sender, Syncfusion.Grouping.SelectedRecordsChangedEventArgs e)
        {
            // foreach (SelectedRecord rec in this.gridGroupingControl1.Table.SelectedRecords) textBox1.Text =  rec.Record.ToString());
        }

        private void gridGroupingControl1_SelectedRecordsChanging(object sender, Syncfusion.Grouping.SelectedRecordsChangedEventArgs e)
        {
            foreach (Syncfusion.Grouping.SelectedRecord rec in this.gridGroupingControl1.Table.SelectedRecords)
            { textBox1.Text = rec.Record.ToString(); }
        }
    }
}
