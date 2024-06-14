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
using System.Security.Cryptography;
using System.Web.UI.WebControls;

namespace Saobracaj.RadniNalozi
{
    public partial class frmCIRPregledac : Syncfusion.Windows.Forms.Office2010Form
    {
        public frmCIRPregledac()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gridGroupingControl2.DataSource = null;
            gridGroupingControl2.ResetTableDescriptor();
            gridGroupingControl2.Refresh();
            var select = "";
            select = "Select RNPrijemPlatforme.ID,  'PRIJEM PLATFORMA' as Vrsta, BrojKontejnera, DATUMRAsporeda, TipKontenjera.Naziv as TipKOntejnera, " +
            " NalogIZdao, USkladiste, Skladista.Naziv,Kamion as PrevoznoSredstvo, NalogID, PrijemID, 'GATE IN TRACK' from RNPrijemPlatforme " +
            " inner join Skladista on Skladista.ID = USkladiste " +
            " inner join TipKontenjera on TipKontenjera.Id = VrstaKontejnera " +
            " where ZavrsenCIR is null AND Zavrsen = 1 " +
            " union " +
            "Select RNPrijemPlatforme2.ID,  'PRIJEM PLATFORMA TERMINAL' as Vrsta, BrojKontejnera, DATUMRAsporeda, TipKontenjera.Naziv as TipKOntejnera, " +
            " NalogIZdao, USkladiste, Skladista.Naziv,Kamion as PrevoznoSredstvo, NalogID, PrijemID, 'GATE IN TRACK BRODAR' from RNPrijemPlatforme2 " +
            " inner join Skladista on Skladista.ID = USkladiste " +
            " inner join TipKontenjera on TipKontenjera.Id = VrstaKontejnera " +
            " where ZavrsenCIR is null  " +
            " union " +
            " Select RNPrijemVoza.ID,  'PRIJEM VOZ' as Vrsta, RNPrijemVoza.BrojKontejnera, RNPrijemVoza.DatumRasporeda, TipKontenjera.Naziv as TipKOntejnera,  " +
            " NalogIzdao, Skladista.ID , Skladista.Naziv, (RTRIM(Voz.BrVoza) + ' ' + RTRIM(Voz.Relacija)) as PrevoznoSredstvo, NalogID, PRijemID, 'GATE IN TRAIN' " +
            " from RNPrijemVoza " +
            " inner join Voz on RNPrijemVoza.SaVoznogSredstva = Voz.ID " +
            " inner join Skladista on Skladista.ID = RNPrijemVoza.NaSkladiste " +
            " inner join TipKontenjera on TipKontenjera.Id = VrstaKontejnera " +
            " where PotrebanCIR = 1 ";

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
            select = "Select RNPrijemPlatforme.ID,  'PRIJEM PLATFORMA' as Vrsta, BrojKontejnera, DATUMRAsporeda, TipKontenjera.Naziv as TipKOntejnera, " +
           " NalogIZdao, USkladiste, Skladista.Naziv,Kamion as PrevoznoSredstvo, NalogID, PrijemID from RNPrijemPlatforme " +
           " inner join Skladista on Skladista.ID = USkladiste " +
           " inner join TipKontenjera on TipKontenjera.Id = VrstaKontejnera " +
           " where ZavrsenCIR = 1 " +
           " union " +
           " Select RNPrijemVoza.ID,  'PRIJEM VOZ' as Vrsta, RNPrijemVoza.BrojKontejnera, RNPrijemVoza.DatumRasporeda, TipKontenjera.Naziv as TipKOntejnera,  " +
           " NalogIzdao, Skladista.ID , Skladista.Naziv, (RTRIM(Voz.BrVoza) + ' ' + RTRIM(Voz.Relacija)) as PrevoznoSredstvo, NalogID, PRijemID " +
           " from RNPrijemVoza " +
           " inner join Voz on RNPrijemVoza.SaVoznogSredstva = Voz.ID " +
           " inner join Skladista on Skladista.ID = RNPrijemVoza.NaSkladiste " +
           " inner join TipKontenjera on TipKontenjera.Id = VrstaKontejnera " +
           " where ZavrsenCIR = 1 ";

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
