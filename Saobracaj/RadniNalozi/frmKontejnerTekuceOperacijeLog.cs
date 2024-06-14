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
using System.Windows.Controls;

namespace Saobracaj.RadniNalozi
{
    public partial class frmKontejnerTekuceOperacijeLog : Syncfusion.Windows.Forms.Office2010Form
    {
        public frmKontejnerTekuceOperacijeLog()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
          
            timer1.Start();
           /* gridGroupingControl2.DataSource = null;
            gridGroupingControl2.ResetTableDescriptor();
            gridGroupingControl2.Refresh();
            var select = "";
            select = "SELECT top 5000 * from KontejnerTekuceOperacije order by ID desc";

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
            this.gridGroupingControl2.AllowProportionalColumnSizing = true;
            foreach (GridColumnDescriptor column in this.gridGroupingControl2.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
               
            }
           */
        }

        private void frmKontejnerTekuceOperacijeLog_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            gridGroupingControl2.DataSource = null;
            gridGroupingControl2.ResetTableDescriptor();
            gridGroupingControl2.Refresh();
            var select = "";
            select = "SELECT top 5000 * from KontejnerTekuceOperacije order by ID desc";

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
            this.gridGroupingControl2.AllowProportionalColumnSizing = true;
            foreach (GridColumnDescriptor column in this.gridGroupingControl2.TableDescriptor.Columns)
            {
                column.AllowFilter = true;

            }
        }
    }
}
