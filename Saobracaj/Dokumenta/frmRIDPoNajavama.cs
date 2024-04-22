using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Dokumenta
{
    public partial class frmRIDPoNajavama : Form
    {
        public static string code = "frmRIDPoNajavama";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Sifarnici.frmLogovanje.user.ToString();
        string niz = "";

        public frmRIDPoNajavama()
        {
            InitializeComponent();

        }
        private void frmRIDPoNajavama_Load(object sender, EventArgs e)
        {
            var select = " Select ROW_NUMBER() OVER(ORDER BY n1.ID DESC) AS Row, n1.ID as NajavaID, n1.Oznaka,  " +
          " StvarnoPrimanje, so.Opis as Otpravna, su.Opis as Uputna , " +
          " n1.PrevozniPut, " +
          " n1.BrojKola, " +
          "  n1.RIDBroj as OMBroj, n1.OnBroj as RIDBroj,  pos.PaNaziv as Posiljac, pri.PaNaziv as Primalac, " +
          " (  SELECT  STUFF(  (  SELECT distinct   ', ' + Cast(ts.BrojKola as nvarchar(20))  " +
         " FROM TeretnicaStavke ts where n1.ID = ts.IDNajave " +
         " FOR XML PATH('')   ), 1, 1, ''  ) As Skupljen) " +
         " as Vagoni, (Cast(n1.StvarnoPrimanje as nvarchar(20)) + ' do ' +  Cast(n1.StvarnaPredaja as nvarchar(20))) as VremeRealizacije, " +
         " n1.StvarnaPredaja,  n1.OnBroj + '\'+ n1.DispecerRID as DispecerOMBroj," +
         " (select SUM(neto) from TeretnicaStavke inner join Teretnica on TeretnicaStavke.BrojTeretnice = Teretnica.ID " +
         " where TeretnicaStavke.IDNajave = n1.ID) as Neto  " +
        " from Najava n1 " +
        " inner join stanice so on so.ID = n1.Otpravna " +
        " inner join stanice su on su.ID = n1.Uputna " +
        " inner join Partnerji pos on pos.PaSifra = n1.Posiljalac " +
        " inner join Partnerji pri on pri.PaSifra = n1.Primalac " +
        "  inner join (Select Distinct IDNajave, BrojTeretnice from TeretnicaStavke ) ts on n1.ID = ts.IdNajave " +
        " inner join Teretnica ter on ts.BrojTeretnice = ter.ID " +
        " where Ter.Prijemna = 1 and n1.Rid = 1 order by n1.ID desc";

            // " (Rtrim(NHM.Broj) + '-' + RTRIM(NHM.Naziv)) as NHM, " +
            ///" inner join NHM on NHM.ID = n1.RobaNhm " +
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView3.ReadOnly = true;
            dataGridView3.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView3.DataSource = ds.Tables[0];

            dataGridView3.BorderStyle = BorderStyle.None;
            dataGridView3.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView3.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView3.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView3.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView3.BackgroundColor = Color.White;

            dataGridView3.EnableHeadersVisualStyles = false;
            dataGridView3.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView3.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            DataGridViewColumn column = dataGridView3.Columns[0];
            dataGridView3.Columns[0].HeaderText = "ID";
            dataGridView3.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView3.Columns[1];
            dataGridView3.Columns[1].HeaderText = "Najava";
            dataGridView3.Columns[1].Width = 40;

            DataGridViewColumn column3 = dataGridView3.Columns[6];
            dataGridView3.Columns[6].HeaderText = "Broj kola";
            dataGridView3.Columns[6].Width = 40;



            DataGridViewColumn column12 = dataGridView3.Columns[12];
            dataGridView3.Columns[12].HeaderText = "Vagoni";
            dataGridView3.Columns[12].Width = 250;

            DataGridViewColumn column15 = dataGridView3.Columns[15];
            dataGridView3.Columns[15].HeaderText = "Dispicer";
            dataGridView3.Columns[15].Width = 100;

            DataGridViewColumn column16 = dataGridView3.Columns[16];
            dataGridView3.Columns[16].HeaderText = "Neto";
            dataGridView3.Columns[16].Width = 70;

            dataGridView3.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
