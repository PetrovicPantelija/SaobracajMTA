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

using Microsoft.Reporting.WinForms;
using System.IO;

namespace Saobracaj.Dokumenta
{
    public partial class frmKomercijalniPregled : Form
    {
        public frmKomercijalniPregled()
        {
            InitializeComponent();
        }

        public frmKomercijalniPregled(int AktivnostStavkeID)
        {
            InitializeComponent();
            txtAktivnostStavkeID.Text = AktivnostStavkeID.ToString();
            FillGV();
        }

        private void FillGV()
        {
            var select = "   SELECT     KomercijalniPregled.ID, KomercijalniPregled.IDStavke, KomercijalniPregled.RedniBrojKola, KomercijalniPregled.BrojKola, KomercijalniPregled.Napomena, " +
                 "     KomercijalniPregled.Stanje, KomercijalniPregled.VrstaNepravilnosti as VNID, SifOpisNeispravnosti.Naziv as VrstaNepravilnosti, KomercijalniPregled.OpisNeispravnosti as ONID, " +
                 "     SifVrstaNepravilnosti.Naziv AS VrstaNepravilnosti, KomercijalniPregled.VrstaKocnice, KomercijalniPregled.Duzina, KomercijalniPregled.Tara, KomercijalniPregled.KocnaMasa, " +
                 "          KomercijalniPregled.RucnaKocnica " +
"     FROM         KomercijalniPregled LEFT OUTER JOIN " +
                  "         SifVrstaNepravilnosti ON KomercijalniPregled.OpisNeispravnosti = SifVrstaNepravilnosti.ID LEFT OUTER JOIN " +
                   "        SifOpisNeispravnosti ON KomercijalniPregled.VrstaNepravilnosti = SifOpisNeispravnosti.ID " +
                    "    where IDStavke = " + txtAktivnostStavkeID.Text + "    order by RedniBrojKola ";


            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            /*
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[2].HeaderText = "Zaposleni";
            dataGridView1.Columns[3].HeaderText = "DatumPrijave";
            dataGridView1.Columns[6].HeaderText = "Registarski BR";
            dataGridView1.Columns[8].HeaderText = "Relacija";
            dataGridView1.Columns[11].HeaderText = "KilometrazaZaduzivanje";
            dataGridView1.Columns[12].HeaderText = "KilometrazaRazduzivanje";
            */
        }
    }
}
