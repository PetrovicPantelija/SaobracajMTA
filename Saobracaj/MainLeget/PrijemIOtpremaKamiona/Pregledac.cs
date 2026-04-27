using Microsoft.Office.Interop.Excel;
using Saobracaj.Dokumenta;
using Saobracaj.Izvoz;
using Saobracaj.RadniNalozi;
using Syncfusion.GridHelperClasses;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Cryptography;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Saobracaj.MainLeget.PrijemIOtpremaKamiona
{
    public partial class Pregledac : Form
    {
        public Pregledac()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var select = "";

            select = "     select RadniNalogInterni.ID as KomNalogID, 'Izvoz' as IZvor, KorisnikIzdao, IZvozKonacna.BrojKontejnera, " +
" TipKontenjera.SkNaziv as VrstaKontejnera, CASE Cirada " +
"       WHEN 0 THEN 'PLATFORMA' " +
"               WHEN 1 THEN 'CIRADA' " +
"          END AS TipNaloga, RadniNalogInterniPotvrda.KapijaUlaz as Kapija , 'JOS PODATAKA ' " +
"        from RadniNalogInterni " +
"       inner join RadniNalogInterniPotvrda on RadniNalogInterni.ID = RadniNalogInterniPotvrda.IDNaloga " +
"       inner join IzvozKonacna on IzvozKonacna.ID = RadniNalogInterni.BrojOsnov " +
"       inner join TipKontenjera on TipKontenjera.ID = IzvozKonacna.VrstaKontejnera " +
"        where Pregledac = 0";


            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            this.gridGroupingControl2.Table.Records.DeleteAll();

            gridGroupingControl2.DataSource = ds.Tables[0];
            gridGroupingControl2.ShowGroupDropArea = true;
            this.gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;

            foreach (GridColumnDescriptor column in this.gridGroupingControl2.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }

            GridConditionalFormatDescriptor gcfd3 = new GridConditionalFormatDescriptor();
            gcfd3.Appearance.AnyRecordFieldCell.BackColor = Color.Green;
            gcfd3.Appearance.AnyRecordFieldCell.TextColor = Color.Yellow;

            gcfd3.Expression = "[Kapija] = '1'";
            this.gridGroupingControl2.TableDescriptor.ConditionalFormats.Add(gcfd3);

            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            dynamicFilter.WireGrid(this.gridGroupingControl2);
        }
    }
}
