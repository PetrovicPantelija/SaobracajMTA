using Saobracaj.Sifarnici;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Uvoz
{
    public partial class frmPregledPlanovaUtovara : Syncfusion.Windows.Forms.Office2010Form
    {
        public static string code = "frmPregledPlanovaUtovara";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Saobracaj.Sifarnici.frmLogovanje.user.ToString();
        string niz = "";
        string KorisnikF = "";
        int pomTerminal = 0;
        public frmPregledPlanovaUtovara(string Korisnik)
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");

            KorisnikF = Korisnik;
        }
        public frmPregledPlanovaUtovara(int Terminal)
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            pomTerminal = Terminal;
            KorisnikF = Saobracaj.Sifarnici.frmLogovanje.user.ToString(); 
          
        }
        private void RefreshDataGrid()
        {


            var select = "";
            string Company = Saobracaj.Sifarnici.frmLogovanje.Firma;
            switch (Company)
            {
                case "Leget":
                    {
                        select = " SELECT UvozKonacnaZaglavlje.[ID] as PLANID,[Voz].[ID] ,[Relacija], p1.PaNAziv as OperaterSRB, p2.PaNaziv as OperaterHR," +
                        " PristizanjaUSid as DV_PristizanjaUSid, Sazeta as DV_SAzeta, " +
                        " (SELECT  Count(*) from UvozKonacna where UvozKonacna.IDNadredjeni = UvozKonacnaZaglavlje.ID and IDNadredjeni = UvozKonacnaZaglavlje.[ID]  ) as BrojSpakovanihKonterjnera, " +
                        " (SELECT  SUM(UvozKonacna.TaraKontejnera) from UvozKonacna where UvozKonacna.IDNadredjeni = UvozKonacnaZaglavlje.ID and IDNadredjeni = UvozKonacnaZaglavlje.[ID]  ) as TaraSpakovanihKonterjnera, " +
                        " (SELECT  SUM(UvozKonacna.BrutoKontejnera) from UvozKonacna where UvozKonacna.IDNadredjeni = UvozKonacnaZaglavlje.ID and IDNadredjeni = UvozKonacnaZaglavlje.[ID]  ) as BrutoSpakovanihKonterjnera, " +
                        " (SELECT  SUM(UvozKonacna.BrutoRobe) from UvozKonacna where UvozKonacna.IDNadredjeni = UvozKonacnaZaglavlje.ID and IDNadredjeni = UvozKonacnaZaglavlje.[ID]  ) as BrutoRobeSpakovanihKonterjnera, " +
                        " [MaksimalnaBruto],[MaksimalnaDuzina] " +
                        " ,[MaksimalanBrojKola] " +
                        " ,[Voz].Napomena " +
                        " ,Voz.[Datum] ,[Korisnik],[Dolazeci] " +
                        "  FROM [dbo].[Voz] " +
                         " Left join UvozKonacnaZaglavlje On Voz.ID = UvozKonacnaZaglavlje.IDVoza " +
                         " inner join Partnerji p1 on p1.PaSifra = OperaterSrbija " +
                          " inner join Partnerji p2 on p2.PaSifra = OperaterHR " +
                         " where Dolazeci = 1 and Terminal = " + pomTerminal + "Order by [Voz].[ID] desc";
                        break;

                    }
                default:
                    {
                        select = "SELECT [ID] ,[BrVoza],[Relacija], " +
             " CONVERT(varchar,VremePolaska,104)      + ' '      + SUBSTRING(CONVERT(varchar,VremePolaska,108),1,5) as VremePolaska, " +
             " CONVERT(varchar,[VremeDolaska],104)      + ' '      + SUBSTRING(CONVERT(varchar,[VremeDolaska],108),1,5)  as VremeDolaska, " +
             " [MaksimalnaBruto],[MaksimalnaDuzina] " +
             " ,[MaksimalanBrojKola] " +
             " ,[Napomena]" +
             " ,[PostNaTerminalD] as Postavka,[KontrolniPregledD] as Kontrolni ,[VremePrimopredajeD] as Primopredaja,[VremeIstovaraD] as Istovar" +
             " ,[Datum] ,[Korisnik],[Dolazeci] " +
             " FROM [dbo].[Voz] where Dolazeci = 1 ";

                        break;

                    }
            }
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
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

        private void frmPregledPlanovaUtovara_Load(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            frmUvozKonacna pUvoz = new frmUvozKonacna(Convert.ToInt32(txtSifra.Text), KorisnikF);
            pUvoz.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            frmUvozKonacnaZaglavlje fukz = new frmUvozKonacnaZaglavlje();
            fukz.Show();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (txtSifra.Text == "")
            {
                MessageBox.Show("Odaberite Plan Terminala");
                return;
            }
            Saobracaj.Uvoz.Uvoz uv = new Saobracaj.Uvoz.Uvoz(1, Convert.ToInt32(txtSifra.Text));
            uv.Show();
        }
    }
}
