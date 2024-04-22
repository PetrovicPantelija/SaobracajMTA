using Saobracaj.Sifarnici;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Dokumenta
{
    public partial class frmPregledLokomotivaPrimopredaja : Form
    {
        public string connect = frmLogovanje.connectionString;
        public frmPregledLokomotivaPrimopredaja()
        {
            InitializeComponent();
        }

        private void frmPregledLokomotivaPrimopredaja_Load(object sender, EventArgs e)
        {
            FillGV();
        }
        private void FillGV()
        {

            var select = "  SELECT     LokomotivaPrimopredaja.ID, LokomotivaPrimopredaja.StavkaAktivnostiID, " +
            " LokomotivaPrimopredaja.VrstaPosla, LokomotivaPrimopredaja.VrstaPrimopredaje, " +
            "  LokomotivaPrimopredaja.Kilometraza, LokomotivaPrimopredaja.MotoSati,  LokomotivaPrimopredaja.Napomena, LokomotivaPrimopredaja.Lokomotiva, " +
                   "    LokomotivaPrimopredaja.Dizel, AktivnostiStavke.VrstaAktivnostiID, AktivnostiStavke.Sati, AktivnostiStavke.Posao, AktivnostiStavke.OznakaPosla, AktivnostiStavke.DatumZavrsetka, " +
                  "     AktivnostiStavke.DatumPocetka, Delavci.DeSifra, Delavci.DeIme, DePriimek " +
            " FROM         LokomotivaPrimopredaja INNER JOIN " +
                     "  AktivnostiStavke ON LokomotivaPrimopredaja.StavkaAktivnostiID = AktivnostiStavke.ID " +
                    "   inner join Aktivnosti on Aktivnosti.ID = AktivnostiStavke.IdNadredjena " +
                     "  inner join Delavci on Delavci.DeSifra = Aktivnosti.Zaposleni " +
                   "    order by LokomotivaPrimopredaja.ID desc ";

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

        private void btn_OtvoriSliku_Click(object sender, EventArgs e)
        {
            FillGV();
        }
    }
}
