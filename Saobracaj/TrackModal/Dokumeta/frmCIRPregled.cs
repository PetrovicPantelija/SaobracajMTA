using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Data;
using Syncfusion.Drawing;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Grouping;

using Microsoft.Reporting.WinForms;

namespace Testiranje.Dokumeta
{
    public partial class frmCIRPregled :  Syncfusion.Windows.Forms.Office2010Form
    {
        public frmCIRPregled()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
        }

        private void frmCIRPregled_Load(object sender, EventArgs e)
        {
            RefreshGV();

        }

        private void RefreshGV()
        {
            var select = "    SELECT top 4000 [ID] ,[BrKontejnera] ,[Datum] as DatumIzrade, CAST(CASE Interni WHEN 1 THEN 1 ELSE 0 END AS BIT) as Interni " +
            " ,CAST(CASE Prijem WHEN 1 THEN 1 ELSE 0 END AS BIT) as IzPrijema,[Dokument] " +
            " ,[TiKontejnera] " +
            " ,[Pun],[Tezina],[Plomba1] " +
            " ,[Plomba2],[DatumIn],[Vagon],[TruckNo] " +
            " ,CAST(CASE [Damaged] WHEN 1 THEN 1 ELSE 0 END AS BIT) as Ostecen,CAST(CASE ISpravan WHEN 1 THEN 1 ELSE 0 END AS BIT) as Ispravan,[Prevoz],[Containerresponsible] " +
            " ,[primedbe],[Received],[Inspected],[Delivery] " +
            " ,[Size] " +
            " ,[Duzina],[Sirina],[Visina],[sPlomba] " +
            " ,[sPlomba2], Nosivost , [Korisnik]  FROM [dbo].[CIR] order by ID desc";


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

        private void RefreshDataGrid()
        {
            var select = "  SELECT top 4000 [ID] ,[BrKontejnera] ,[Datum] as DatumIzrade, CAST(CASE Interni WHEN 1 THEN 1 ELSE 0 END AS BIT) " +
" ,[Prijem],[Dokument] " +
" ,[TiKontejnera],[MaterijalCelik],[MaterijalAlumini],[incoming] " +
" ,[Pun],[Tezina],[Plomba1] " +
" ,[Plomba2],[DatumIn],[Vagon],[TruckNo] " +
" ,[Damaged],[Ispravan],[Prevoz],[Containerresponsible] " +
" ,[primedbe],[Received],[Inspected],[Delivery] " +
" ,[Korisnik],[Size] " +
" ,[Duzina],[Sirina],[Visina],[sPlomba] " +
" ,[sPlomba2]  FROM[dbo].[CIR] order by ID desc";

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = false;
            dataGridView1.DataSource = ds.Tables[0];

         



        }

        private void gridGroupingControl1_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            try
            {
                if (gridGroupingControl1.Table.CurrentRecord != null)
                {
                    textBox1.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();

                    // txtSifra.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            RefreshGV();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Niste izabrali stavku za koju otvarate CIR");
                return;
            }
            Saobracaj.Dokumenta.frmCIR cir = new Saobracaj.Dokumenta.frmCIR(Convert.ToInt32(textBox1.Text));
            cir.Show();
        }
    }
}
