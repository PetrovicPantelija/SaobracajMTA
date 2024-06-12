using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Uvoz
{
    public partial class frmKontejnerTekuceArhiv : Syncfusion.Windows.Forms.Office2010Form
    {
        public frmKontejnerTekuceArhiv()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var select = "";

            select = "  Select KontejnerTekuceArhiv.Kontejner, KontejnerStatus.Naziv as Status, KontejnerTekuceArhiv.Pokret as Pokret, Skladista.Naziv as Skladiste, " +
" Skladista.Kapacitet, Pozicija.Opis as Pozicija, VizuelniNapomena, " +
" Ostecenja as Ispravnost, KontejnerskiTerminali.Naziv as RLSRB, Kvalitet, CIR, STATUSIzvoz , " +
" PArtnerji.PaNaziv as Brodar, puv.PaNAziv as Uvoznik, UlazniBroj as KontejnerID_UVoz, DatumGIN as DATUM_GATEIN, DatumOtpremaPlat  as DATUM_OTP_PLAT, OperacijaUradjena as ZadnjaOperacija, Skladisnina, SkladisninaOd, SkladisninaDo " +
" from KontejnerTekuceArhiv " +
" inner join KontejnerStatus on KontejnerStatus.ID = KontejnerTekuceArhiv.StatusKontejnera " +
" inner " +
" join Skladista on Skladista.Id = KontejnerTekuceArhiv.Skladiste " +
" inner " +
" join Pozicija on Pozicija.Id = KontejnerTekuceArhiv.Pozicija " +
" inner " +
" join PArtnerji on KontejnerTekuceArhiv.Brodar = Partnerji.PaSifra " +
" inner " +
" join PArtnerji as PUV on KontejnerTekuceArhiv.Uvoznik = PUV.PASifra " +
" left " +
" join KontejnerskiTerminali on KontejnerskiTerminali.ID = KontejnerTekuceArhiv.RLSRB";

            var s_connection = Sifarnici.frmLogovanje.connectionString;
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
