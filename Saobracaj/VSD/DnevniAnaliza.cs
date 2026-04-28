using Microsoft.Office.Interop.Excel;
using Saobracaj.Dokumenta;
using Saobracaj.Izvoz;
using Saobracaj.RadniNalozi;
using Syncfusion.Drawing;
using Syncfusion.GridHelperClasses;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Numerics;
using System.Security.Cryptography;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Saobracaj.VSD
{
    public partial class DnevniAnaliza : Form
    {
        public DnevniAnaliza()
        {
            InitializeComponent();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            var select = "     SELECT [Plan].[ID] as PlanID" +
      " ,[Godina]       ,[Mesec] " +
      " ,[UkupnoDana]      ,[TekuceDana] " +
      " ,[Plan].[Naziv] as PlanNaziv  FROM [Plan] ";
  

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

            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            dynamicFilter.WireGrid(this.gridGroupingControl2);
    }

        private void gridGroupingControl2_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            if (gridGroupingControl2.Table.CurrentRecord != null)
            {
                textBox1.Text = gridGroupingControl2.Table.CurrentRecord.GetValue("PlanID").ToString();
                //Sve usluge za odredjeni kontejner

                var select = "";


                select = "  SELECT  [Datum]      ,[Komercijalista]      ,[Brend]      ,[Kolicina]      ,[PNC] " +
      " ,[PVrednost]      ,[RUC]      ,[Profit]      ,[PlanID]   FROM [VSD].[dbo].[DnevniERP] ";


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

                GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
                summaryColumnDescriptor.Appearance.AnySummaryCell.Interior = new BrushInfo(Color.FromArgb(192, 255, 162));
                summaryColumnDescriptor.DataMember = "Kolicina";
                summaryColumnDescriptor.Format = "{Sum}";
                summaryColumnDescriptor.Name = "Kolicina";
                summaryColumnDescriptor.SummaryType = Syncfusion.Grouping.SummaryType.Int32Aggregate;

                GridSummaryRowDescriptor summaryRowDescriptor = new GridSummaryRowDescriptor();
                summaryRowDescriptor.SummaryColumns.Add(summaryColumnDescriptor);
                summaryRowDescriptor.Appearance.AnySummaryCell.Interior = new BrushInfo(Color.FromArgb(255, 231, 162));

                this.gridGroupingControl1.TableDescriptor.SummaryRows.Add(summaryRowDescriptor);

                 GridDynamicFilter dynamicFilter = new GridDynamicFilter();
                  dynamicFilter.WireGrid(this.gridGroupingControl1);
            }
        }

        private void tabSplitterPage1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gridGroupingControl2_Click(object sender, EventArgs e)
        {
           

              

           
         
        }
    }
}
