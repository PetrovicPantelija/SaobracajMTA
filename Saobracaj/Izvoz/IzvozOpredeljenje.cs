using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Izvoz
{

    public partial class IzvozOpredeljenje : Syncfusion.Windows.Forms.Office2010Form
    {
        int Selektovani = 0;
        private Keys keyData;
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        public IzvozOpredeljenje()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            InitializeComponent();
            RefreshGV();
        }

        public string GetBrojKontejnera()
        {
            return txtBrojKontejnera.Text;
        }
        public string GetUvozniID()
        {
            return txtUvozniID.Text;
        }

        private void RefreshGV()
        {
            var select = " Select Kontejner, KontejnerStatus.Naziv as Kontejner_Status, Skladista.Naziv as Skladiste, Pokret, KontejnerTekuce.Stanje, " +
" (KontejnerskiTerminali.Oznaka + ' ' + KontejnerskiTerminali.Naziv) as RLSRB, KontejnerTekuce.StatusIZvoz, KontejnerTekuce.UlazniBroj " +
" , Partnerji.PaNaziv as Brodar, p2.PaNAziv as Uvoznik from KontejnerTekuce " +
" inner join KontejnerStatus on KontejnerStatus.ID = KontejnerTekuce.StatusKontejnera inner  join KontejnerskiTerminali on KontejnerskiTerminali.Id = KontejnerTekuce.RLSRB " +
" inner  join Skladista on Skladista.ID = KontejnerTekuce.Skladiste " +
 " inner  join Partnerji on PArtnerji.PaSifra = KontejnerTekuce.Brodar " +
  " inner  join Partnerji as p2 on p2.PaSifra = KontejnerTekuce.Uvoznik  ";


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

        private void gridGroupingControl1_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            try
            {
                if (gridGroupingControl1.Table.CurrentRecord != null)
                {
                    txtBrojKontejnera.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("Kontejner").ToString();
                    txtUvozniID.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("UlazniBroj").ToString();

                    // txtSifra.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void IzvozOpredeljenje_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
            {
                Close();
            }
        }
    }
}
