using Saobracaj.Uvoz;
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

namespace Saobracaj.RadniNalozi
{
    public partial class frmKalmarGateOutVoz : Syncfusion.Windows.Forms.Office2010Form
    {
        public frmKalmarGateOutVoz()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gridGroupingControl2.DataSource = null;
            gridGroupingControl2.ResetTableDescriptor();
            gridGroupingControl2.Refresh();
            var select = "";
            select = "SELECT        RNOtpremaVoza.ID, RNOtpremaVoza.DatumRasporeda, RNOtpremaVoza.BrojKontejnera, TipKontenjera.Naziv, TipKontenjera.SkNaziv, RNOtpremaVoza.NalogIzdao, RNOtpremaVoza.DatumRealizacije, " +
                    "     Voz.BrVoza, Voz.Relacija, RNOtpremaVoza.BrojPlombe, RNOtpremaVoza.BrojVagona, Skladista.ID AS SkladisteID, Skladista.Naziv AS NAzivSkladista, VrstaManipulacije.Naziv AS Usluga, " +
                     "     RNOtpremaVoza.NalogRealizovao, RNOtpremaVoza.Napomena, RNOtpremaVoza.OtpremaID, RNOtpremaVoza.NalogID, RNOtpremaVoza.Zavrsen " +
" FROM            RNOtpremaVoza INNER JOIN " +
                     "     TipKontenjera ON RNOtpremaVoza.VrstaKontejnera = TipKontenjera.ID INNER JOIN " +
                      "    Voz ON RNOtpremaVoza.NaVoznoSredstvo = Voz.ID INNER JOIN " +
                     "     Skladista ON RNOtpremaVoza.SaSkladista = Skladista.ID INNER JOIN " +
                     "     VrstaManipulacije ON RNOtpremaVoza.IdUsluge = VrstaManipulacije.ID where Zavrsen <> 1";

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
            select = "SELECT        RNOtpremaVoza.ID, RNOtpremaVoza.DatumRasporeda, RNOtpremaVoza.BrojKontejnera, TipKontenjera.Naziv, TipKontenjera.SkNaziv, RNOtpremaVoza.NalogIzdao, RNOtpremaVoza.DatumRealizacije, " +
                    "     Voz.BrVoza, Voz.Relacija, RNOtpremaVoza.BrojPlombe, RNOtpremaVoza.BrojVagona, Skladista.ID AS SkladisteID, Skladista.Naziv AS NAzivSkladista, VrstaManipulacije.Naziv AS Usluga, " +
                     "     RNOtpremaVoza.NalogRealizovao, RNOtpremaVoza.Napomena, RNOtpremaVoza.OtpremaID, RNOtpremaVoza.NalogID, RNOtpremaVoza.Zavrsen " +
" FROM            RNOtpremaVoza INNER JOIN " +
                     "     TipKontenjera ON RNOtpremaVoza.VrstaKontejnera = TipKontenjera.ID INNER JOIN " +
                      "    Voz ON RNOtpremaVoza.NaVoznoSredstvo = Voz.ID INNER JOIN " +
                     "     Skladista ON RNOtpremaVoza.SaSkladista = Skladista.ID INNER JOIN " +
                     "     VrstaManipulacije ON RNOtpremaVoza.IdUsluge = VrstaManipulacije.ID where Zavrsen = 1";

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
