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
    public partial class frmPregledMasinovodje2 : Syncfusion.Windows.Forms.Office2010Form
    {
        public frmPregledMasinovodje2()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            InitializeComponent();
        }

        private void frmPregledMasinovodje2_Load(object sender, EventArgs e)
        {
            var select = "  select distinct Lokomotivaprijava.ID as ID, AktivnostiStavke.OznakaPosla, Lokomotivaprijava.Lokomotiva ,(select case when Smer = 1 then 'ODJAVA' else 'PRIJAVA' end) as Smer,Stanice.Opis as Stanica," +
" LokomotivaPrijava.Datum, Rtrim(Delavci.DeIme) + ' ' + Rtrim(Delavci.DePriimek) as Zaposleni," +
" Lokomotivaprijava.MotoSati, Lokomotivaprijava.KM, Lokomotivaprijava.Gorivo, Lokomotivaprijava.Napomena, Lokomotivaprijava.AktivnostID, AktivnostiStavke.Posao from LokomotivaPrijava" +
" inner join Delavci on Delavci.DeSifra = LokomotivaPrijava.Zaposleni" +
" inner join Stanice on Stanice.ID = LokomotivaPrijava.Stanica" +
" left join Aktivnosti on Aktivnosti.ID = LokomotivaPrijava.AktivnostID" +
" left join AktivnostiStavke on AktivnostiStavke.IDNadredjena = Aktivnosti.ID" +
" order by LokomotivaPrijava.ID desc ";

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
