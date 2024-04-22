using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Dokumenta
{
    public partial class frmTehnickiPregled : Form
    {
        public frmTehnickiPregled()
        {
            InitializeComponent();
        }

        public frmTehnickiPregled(int AktivnostStavkeID)
        {
            InitializeComponent();
            txtAktivnostStavkeID.Text = AktivnostStavkeID.ToString();
            FillGV();
            FillColor();
        }
        private void FillColor()
        {

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string Pakerista = row.Cells[10].Value.ToString();

                if ((Pakerista == "1"))
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                    row.DefaultCellStyle.SelectionBackColor = Color.Yellow;
                }
            }



        }
        private void FillGV()
        {
            var select = "  SELECT     TehnickiPregled.ID, TehnickiPregled.IDStavke, TehnickiPregled.RedniBrojKola, TehnickiPregled.BrojKola, SifRazredNepravilnosti.ID AS RazredNepravilnostiID, " +
                 "       SifRazredNepravilnosti.Naziv AS RazredNepravilnostiNaziv, SifGrupaNepravilnosti.Naziv AS GrupaNeispravnostiNaziv,  " +
                  "      SifNeispravnostPostupak.ID AS IDNeispravnostPostupak, SifNeispravnostPostupak.Naziv AS NeispravnostNaziv, TehnickiPregled.Napomena, " +
                 "       TehnickiPregled.Stanje " +
"  FROM         TehnickiPregled left JOIN  " +
                    "    SifRazredNepravilnosti ON TehnickiPregled.RazredNepravilnosti = SifRazredNepravilnosti.ID left JOIN  " +
                   "     SifGrupaNepravilnosti ON TehnickiPregled.GrupaNeispravnosti = SifGrupaNepravilnosti.ID left JOIN  " +
                    "    SifNeispravnostPostupak ON TehnickiPregled.SifraNeispravnosti = SifNeispravnostPostupak.ID " +
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
