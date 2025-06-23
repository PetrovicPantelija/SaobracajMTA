using Saobracaj.Carinko;
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

namespace Saobracaj.Carinsko
{
    public partial class frmOtpremnicaCarinskoTabela : Form
    {
        public frmOtpremnicaCarinskoTabela()
        {
            InitializeComponent();
        }


        private void RefreshDataGrid()
        {
            var select = "  SELECT     OtpremnicaCarinska.ID, OtpremnicaCarinska.Status, Partnerji.PaNaziv AS Vlasnik, OtpremnicaCarinska.ID AS Expr1, OtpremnicaCarinska.Status, "+
 " OtpremnicaCarinska.Datum, OtpremnicaCarinska.Korisnik, OtpremnicaCarinska.SkladisteID, OtpremnicaCarinska.Dokument, OtpremnicaCarinska.MBR, "+
" OtpremnicaCarinska.Vlasnik AS VlasnikRobe, OtpremnicaCarinska.KorisnikRoba, OtpremnicaCarinska.Nalogodavac,  "+
" Partnerji_1.PaNaziv AS KorisnikRobe, Partnerji_2.PaNaziv AS Primalac,      Skladista.Naziv AS Skladiste "+
"  FROM        dbo.OtpremnicaCarinska INNER JOIN Skladista ON OtpremnicaCarinska.SkladisteID = Skladista.ID "+
" INNER JOIN   Partnerji ON OtpremnicaCarinska.Vlasnik = Partnerji.PaSifra "+
" INNER JOIN  Partnerji AS Partnerji_1 ON OtpremnicaCarinska.KorisnikRoba = Partnerji_1.PaSifra "+
" INNER JOIN   Partnerji AS Partnerji_2 ON OtpremnicaCarinska.Nalogodavac = Partnerji_2.PaSifra ";


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

        private void frmOtpremnicaCarinskoTabela_Load(object sender, EventArgs e)
        {

        }

        private void button23_Click(object sender, EventArgs e)
        {
            frmOtpremnicaCarinsko car = new frmOtpremnicaCarinsko();
            car.Show();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            frmOtpremnicaCarinsko pc = new frmOtpremnicaCarinsko(txtSifra.Text);
            pc.Show();
        }

        private void gridGroupingControl1_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            try
            {
                if (gridGroupingControl1.Table.CurrentRecord != null)
                {
                    txtSifra.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }
    }
}
