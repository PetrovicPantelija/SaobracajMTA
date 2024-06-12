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
using Syncfusion.Data;
using Syncfusion.Drawing;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Grouping;
using Saobracaj.Uvoz;

namespace Saobracaj.RadniNalozi
{
    public partial class frmKalmarGateOut : Syncfusion.Windows.Forms.Office2010Form
    {
        public frmKalmarGateOut()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gridGroupingControl2.DataSource = null;
            gridGroupingControl2.ResetTableDescriptor();
            gridGroupingControl2.Refresh();
            var select = "";
            select = "Select RNOtpremaPlatforme.ID, RNOtpremaPlatforme.BrojKontejnera, DATUMRAsporeda, TipKontenjera.Naziv, NalogIZdao, SaSkladista, Skladista.Naziv,Kamion, " +
                " NalogID, OtpremaID from RNOtpremaPlatforme " +
" inner join Skladista on Skladista.ID = SaSkladista " +
" inner join TipKontenjera on TipKontenjera.Id = VrstaKontejnera" +
" where Zavrsen is null or Zavrsen = 0" +
" union " +
  " Select RNOtpremaPlatforme2.ID, RNOtpremaPlatforme2.BrojKontejnera, DATUMRAsporeda, TipKontenjera.Naziv, NalogIZdao, SaSkladista, Skladista.Naziv,Kamion, " +
                " NalogID,  OtpremaID from RNOtpremaPlatforme2 " +
" inner join Skladista on Skladista.ID = SaSkladista " +
" inner join TipKontenjera on TipKontenjera.Id = VrstaKontejnera" +
" where Zavrsen is null or Zavrsen = 0";

            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl2.DataSource = ds.Tables[0];
            gridGroupingControl2.ShowGroupDropArea = true;
            this.gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;
            foreach (GridColumnDescriptor column in this.gridGroupingControl2.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gridGroupingControl2.DataSource = null;
            gridGroupingControl2.ResetTableDescriptor();
            gridGroupingControl2.Refresh();
            var select = "";
            select = "Select RNOtpremaPlatforme.ID, DATUMRAsporeda, VrstaKontejnera, NalogIZdao, SaSkladista, Skladista.Naziv,Kamion, " +
                " NalogID, Uvoz, OtpremaID from RNOtpremaPlatforme " +
" inner join Skladista on Skladista.ID = SaSkladista " +
" inner join TipKontenjera on TipKontenjera.Id = VrstaKontejnera" +
" where Zatvoren = 1";

            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl2.DataSource = ds.Tables[0];
            gridGroupingControl2.ShowGroupDropArea = true;
            this.gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;
            foreach (GridColumnDescriptor column in this.gridGroupingControl2.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }
        }
    }
}
