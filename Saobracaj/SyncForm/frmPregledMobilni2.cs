﻿using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Saobracaj.SyncForm
{
    public partial class frmPregledMobilni2 : Syncfusion.Windows.Forms.Office2010Form
    {
        public frmPregledMobilni2()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            InitializeComponent();

        }

        private void frmPregledMobilni2_Load(object sender, EventArgs e)
        {
            var select = "   Select ZaposleniPrijava.ID,ZaposleniPrijava.Zaposleni as ZaposleniID, Rtrim(DeIme) + ' ' + Rtrim(DePriimek) as  Zaposleni, " +
 " ZaposleniPrijava.DatumPrijave as SmenaPrijava,ZaposleniPrijava.DatumOdjave as SmenaOdjava, DATEDIFF(Minute, ZaposleniPrijava.DatumPrijave, ZaposleniPrijava.DatumOdjave) as MinutaRAdaPrijava, " +
 " ZaposleniPrijava.AktivnostID , AktivnostiStavke.DatumPocetka as PocetakAktivnosti,  " +
 " AktivnostiStavke.DatumZavrsetka as ZavrsetakAktivnosti, DATEDIFF(MINUTE, DatumPocetka, DatumZavrsetka) as MinutaRAdaAktivnosti, VrstaAktivnosti.Naziv,AktivnostiStavke.OznakaPosla " +
 "   from ZaposleniPrijava " +
 "  inner join Delavci on DeSifra = ZaposleniPrijava.Zaposleni " +
" inner join Aktivnosti on Aktivnosti.ID = ZaposleniPrijava.AktivnostID " +
" inner join AktivnostiStavke on AktivnostiStavke.IDNadredjena = Aktivnosti.ID " +
" inner join VrstaAktivnosti on VrstaAktivnosti.ID = AktivnostiStavke.VrstaAktivnostiID " +
"  order by ZaposleniPrijava.ID desc ";


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
