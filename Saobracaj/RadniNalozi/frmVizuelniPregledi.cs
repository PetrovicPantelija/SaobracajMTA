﻿using System;
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
    public partial class frmVizuelniPregledi : Syncfusion.Windows.Forms.Office2010Form
    {
        public frmVizuelniPregledi()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gridGroupingControl2.DataSource = null;
            gridGroupingControl2.ResetTableDescriptor();
            gridGroupingControl2.Refresh();
            var select = "";
            select = "SELECT RNPrijemVoza.[ID] , VOZ.BrVoza , Voz.Relacija    ,[BrojKontejnera], TipKontenjera.Naziv,DatumRasporeda          , " +
     " [NalogIzdao]      ,[DatumRealizacije]      ,[SaVoznogSredstva]	   " +
     "  ,[BrojPlombe]      ,[NalogRealizovaoVP]      ,[ZavrsenVP]      ,[NapomenaVP] " +
     "  ,[DatumRealizacijeVP]      ,[PrijemID] " +
     "  ,[NalogID]            from RNPrijemVoza " +
      "       inner join TipKontenjera on TipKontenjera.ID = [VrstaKontejnera] " +
       "      inner " +
      "       join Voz on Voz.ID = SaVoznogSredstva " +
"  where ZavrsenVP is null";

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
            select = "SELECT RNPrijemVoza.[ID]      ,[DatumRasporeda]      ,[BrojKontejnera]      ,TipKontenjera.Naziv, " +
     " [NalogIzdao]      ,[DatumRealizacije]      ,[SaVoznogSredstva]	  , VOZ.BrVoza " +
     "  ,[BrojPlombe]      ,[NalogRealizovaoVP]      ,[ZavrsenVP]      ,[NapomenaVP] " +
     "  ,[DatumRealizacijeVP]      ,[PrijemID] " +
     "  ,[NalogID]            from RNPrijemVoza " +
      "       inner join TipKontenjera on TipKontenjera.ID = [VrstaKontejnera] " +
       "      inner " +
      "       join Voz on Voz.ID = SaVoznogSredstva " +
"  where ZavrsenVP = 1 ";

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
