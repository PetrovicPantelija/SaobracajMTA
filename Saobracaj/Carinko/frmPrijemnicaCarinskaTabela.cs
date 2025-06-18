using Syncfusion.GridHelperClasses;
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

namespace Saobracaj.Carinko
{
    public partial class frmPrijemnicaCarinskaTabela : Form
    {
        public frmPrijemnicaCarinskaTabela()
        {
            InitializeComponent();
        }

        private void RefreshDataGrid()
        {
            var select = " SELECT     Skladista.ID, Partnerji.PaNaziv AS Vlasnik, PrijemnicaCarinska.ID AS Expr1, PrijemnicaCarinska.Status, PrijemnicaCarinska.Datum," +
                " PrijemnicaCarinska.Korisanik, PrijemnicaCarinska.SkladisteID, PrijemnicaCarinska.Dokument, PrijemnicaCarinska.MBR, " +
                "  PrijemnicaCarinska.VrstaSkladista, PrijemnicaCarinska.Vlasnik AS VlasnikRobe, PrijemnicaCarinska.Korisinik, PrijemnicaCarinska.Posiljalac,  " +
                "  PrijemnicaCarinska.Sektor, Partnerji_1.PaNaziv AS KorisnikRobe, Partnerji_2.PaNaziv AS Primalac,  " +
                "    Skladista.Naziv AS Skladiste " +
                " FROM        PrijemnicaCarinska INNER JOIN " +
                "  Skladista ON PrijemnicaCarinska.SkladisteID = Skladista.ID INNER JOIN " +
                "  Partnerji ON PrijemnicaCarinska.Vlasnik = Partnerji.PaSifra INNER JOIN " +
                " Partnerji AS Partnerji_1 ON PrijemnicaCarinska.Korisinik = Partnerji_1.PaSifra INNER JOIN " +
                "  Partnerji AS Partnerji_2 ON PrijemnicaCarinska.Posiljalac = Partnerji_2.PaSifra ";


            // var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            //  SqlConnection myConnection = new SqlConnection(s_connection);
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

            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            //Wiring the Dynamic Filter to GridGroupingControl
            dynamicFilter.WireGrid(this.gridGroupingControl1);

            GridExcelFilter gridExcelFilter = new GridExcelFilter();

            //Wiring GridExcelFilter to GridGroupingControl
            gridExcelFilter.WireGrid(this.gridGroupingControl1);


        }

    }
}
