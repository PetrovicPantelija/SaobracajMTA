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

namespace Saobracaj.Uvoz
{
    public partial class frmPregledNerasporedjeni : Form
    {
        public frmPregledNerasporedjeni()
        {
            InitializeComponent();
        }

        private void RefreshDataGrid()
        {
            var select = "SELECT Uvoz.[ID] as Sifra  ,[EtaBroda]      ,[AtaBroda]  ,[StatusPrijema]   " +
  " , TipKontenjera.SkNaziv as Tipkontejnera  ,[BrojKontejnera]      ,[DobijenNalogBrodara]  ,[DobijeBZ]      ,[Napomena]  ,[PIN] " +
   "  ,PredefinisanePoruke.[Naziv] as DirigacijaZa  ,Brodovi.Naziv      ,[BrodskaTeretnica]  ,[ADR]      ,Partnerji.PaNaziv as VlasnikKontejnera " +
    "  ,[BukingBrodara]      ,[Nalogodavac]   ,[VrstaUsluge]      ,p3.PaNaziv as Uvoznik  ,p1.PaNaziv as Spedicija      ,p2.PaNaziv as SpedicijaLeget " +
    "   ,pp.naziv as CarinskiPostupak      ,pp2.naziv as PostupakSaRobom   ,pp3.naziv as NacinPakovanja " +
       "   ,(CINaziv + ' ' + CIOznaka) as OdredisnaCarina  ,p4.PaNaziv as OdredisnaSpedicija " +
        "   ,[MestoIstovara]  ,[KontaktOsoba]      ,[Email]      ,[BrojPlombe1]      ,[BrojPlombe2]   ,[Koleta]      ,[NetoRobe]      ,[BrutoRobe] " +
          "    ,[TaraKontejnera]  ,[BrutoKontejnera]      ,pp4.Naziv as NapomenaZaPozicioniranje  ,[AtaOtpreme]      ,[BrojVoza]       ,[RelacijaVoza] " +
            "    ,[AtaDolazak]      FROM Uvoz   inner join TipKontenjera on TipKontenjera.ID = Uvoz.TipKontejnera " +
            "    inner join Partnerji on Partnerji.PaSifra = Uvoz.VlasnikKontejnera " +
            "    inner join PredefinisanePoruke on PredefinisanePoruke.ID = DirigacijaKontejeraZa " +
            "    inner join Brodovi on Brodovi.ID = NazivBroda " +
            "    inner join Partnerji as p1 on p1.PaSifra = Uvoz.SpedicijaGranica " +
            "    inner join Partnerji as p2 on p2.PaSifra = Uvoz.SpedicijaRTC " +
            "    inner join Carinarnice on Carinarnice.ID = Uvoz.OdredisnaCarina " +
            "    inner join PredefinisanePoruke pp on pp.ID = Uvoz.CarinskiPostupak " +
            "     inner join PredefinisanePoruke pp2 on pp2.ID = Uvoz.PostupakSaRobom " +
            "     inner join PredefinisanePoruke pp3 on pp3.ID = Uvoz.NacinPakovanja " +
              "    inner join Partnerji as p3 on p3.PaSifra = Uvoz.Uvoznik " +
              "     inner join Partnerji as p4 on p4.PaSifra = Uvoz.OdredisnaSpedicija " +
" inner join PredefinisanePoruke pp4 on pp4.ID = Uvoz.NapomenaZaPozicioniranje ";
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
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Br voza";
            dataGridView1.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Relacija";
            dataGridView1.Columns[2].Width = 150;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Vr polaska";
            dataGridView1.Columns[3].Width = 70;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Vr dolaska";
            dataGridView1.Columns[4].Width = 70;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Max bruto";
            dataGridView1.Columns[5].Width = 70;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Max duž";
            dataGridView1.Columns[6].Width = 70;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Max br kola";
            dataGridView1.Columns[7].Width = 70;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Napomena";
            dataGridView1.Columns[8].Width = 100;
*/

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txtSifra.Text = row.Cells[0].Value.ToString();

                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void frmPregledNerasporedjeni_Load(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Uvoz fUvoz = new Uvoz();
            fUvoz.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Uvoz pUvoz = new Uvoz(Convert.ToInt32(txtSifra.Text));
            pUvoz.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }
    }
}
