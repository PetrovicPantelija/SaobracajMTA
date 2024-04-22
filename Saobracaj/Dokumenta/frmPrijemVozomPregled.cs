using Saobracaj.Sifarnici;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TrackModal.Dokumeta
{
    public partial class frmPrijemVozomPregled : Syncfusion.Windows.Forms.Office2010Form
    {
        string KorisnikCene;
        public static string code = "frmPrijemVozomPregled";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Saobracaj.Sifarnici.frmLogovanje.user.ToString();
        string niz = "";

        public frmPrijemVozomPregled()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");

            InitializeComponent();

        }

        public frmPrijemVozomPregled(string Korisnik)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");

            InitializeComponent();
            KorisnikCene = Korisnik;

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void RefreshDataGrid()
        {
            var select = "SELECT PrijemKontejneraVoz.[ID],Voz.BrVoza, Voz.Relacija, " +
             " CONVERT(varchar,PrijemKontejneraVoz.[DatumPrijema],104)      + ' '      + SUBSTRING(CONVERT(varchar,PrijemKontejneraVoz.[DatumPrijema],108),1,5) as ETA, " +
          " CASE WHEN PrijemKontejneraVoz.StatusPrijema = 0 THEN '1-Najava' ELSE '2-Prijem' END as Status, " +
         " CONVERT(varchar,PrijemKontejneraVoz.VremeDolaska,104)      + ' '      + SUBSTRING(CONVERT(varchar,PrijemKontejneraVoz.VremeDolaska,108),1,5) as ATA, " +
          "  [PrijemKontejneraVoz].Korisnik,[PrijemKontejneraVoz].Datum  " +
            " FROM [dbo].[PrijemKontejneraVoz] " +
           " inner join Voz on Voz.ID = PrijemKontejneraVoz.IdVoza  " +
           " where PrijemKontejneraVoz.Vozom = 1 order by PrijemKontejneraVoz.[ID] desc";
            var s_connection = frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Br Voza";
            dataGridView1.Columns[1].Width = 80;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Relacija";
            dataGridView1.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "ETA";
            dataGridView1.Columns[3].Width = 150;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Status";
            dataGridView1.Columns[4].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "ATA";
            dataGridView1.Columns[5].Width = 150;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Korisnik";
            dataGridView1.Columns[6].Width = 100;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Datum";
            dataGridView1.Columns[7].Width = 100;

        }

        private void RefreshDataGridLeget()
        {
            var select = "SELECT PrijemKontejneraVoz.[ID],Voz.BrVoza, Voz.Relacija,  " +
" CONVERT(varchar, PrijemKontejneraVoz.[DatumPrijema], 104) + ' ' + SUBSTRING(CONVERT(varchar, PrijemKontejneraVoz.[DatumPrijema], 108), 1, 5) as ETA, " +
"  CASE WHEN PrijemKontejneraVoz.StatusPrijema = 0 THEN '1-Najava' ELSE '2-Prijem' END as Status, " +
"  CONVERT(varchar, PrijemKontejneraVoz.VremeDolaska, 104) + ' ' + SUBSTRING(CONVERT(varchar, PrijemKontejneraVoz.VremeDolaska, 108), 1, 5) as ATA, " +
" p1.PaNAziv as OperaterSRB, p2.PaNaziv as OperaterHR, PristizanjaUSid as DV_PristizanjaUSid, Sazeta as DV_SAzeta, " +
" (SELECT  Count(*) from UvozKonacna where UvozKonacna.IDNadredjeni = UvozKonacnaZaglavlje.ID and IDNadredjeni = UvozKonacnaZaglavlje.[ID]  ) as BrojSpakovanihKonterjnera,  " +
" (SELECT  SUM(UvozKonacna.TaraKontejnera) from UvozKonacna where UvozKonacna.IDNadredjeni = UvozKonacnaZaglavlje.ID and IDNadredjeni = UvozKonacnaZaglavlje.[ID]  ) as TaraSpakovanihKonterjnera,  " +
" (SELECT  SUM(UvozKonacna.BrutoKontejnera) from UvozKonacna where UvozKonacna.IDNadredjeni = UvozKonacnaZaglavlje.ID and IDNadredjeni = UvozKonacnaZaglavlje.[ID]  ) as BrutoSpakovanihKonterjnera, " +
" (SELECT  SUM(UvozKonacna.BrutoRobe) from UvozKonacna where UvozKonacna.IDNadredjeni = UvozKonacnaZaglavlje.ID and IDNadredjeni = UvozKonacnaZaglavlje.[ID]  ) as BrutoRobeSpakovanihKonterjnera, " +
"  [MaksimalnaBruto],[MaksimalnaDuzina] " +
" ,[MaksimalanBrojKola] " +
  "           FROM[dbo].[PrijemKontejneraVoz] " +
" inner join Voz on Voz.ID = PrijemKontejneraVoz.IdVoza " +
" inner join UvozKonacnaZaglavlje On Voz.ID = UvozKonacnaZaglavlje.IDVoza " +
" inner join Partnerji p1 on p1.PaSifra = Voz.OperaterSrbija " +
" inner join Partnerji p2 on p2.PaSifra = Voz.OperaterHR " +
" where PrijemKontejneraVoz.Vozom = 1 order by PrijemKontejneraVoz.[ID] desc";
            var s_connection = frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Br Voza";
            dataGridView1.Columns[1].Width = 80;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Relacija";
            dataGridView1.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "ETA";
            dataGridView1.Columns[3].Width = 150;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Status";
            dataGridView1.Columns[4].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "ATA";
            dataGridView1.Columns[5].Width = 150;



        }


        private void RefreshDataGridNajave()
        {
            var select = "SELECT PrijemKontejneraVoz.[ID],Voz.BrVoza, Voz.Relacija, " +
             " CONVERT(varchar,PrijemKontejneraVoz.[DatumPrijema],104)      + ' '      + SUBSTRING(CONVERT(varchar,PrijemKontejneraVoz.[DatumPrijema],108),1,5) as ETA, " +
          " CASE WHEN PrijemKontejneraVoz.StatusPrijema = 0 THEN '1-Najava' ELSE '2-Prijem' END as Status, " +
         " CONVERT(varchar,PrijemKontejneraVoz.VremeDolaska,104)      + ' '      + SUBSTRING(CONVERT(varchar,PrijemKontejneraVoz.VremeDolaska,108),1,5) as ATA, " +
          "  [PrijemKontejneraVoz].Korisnik,[PrijemKontejneraVoz].Datum  " +
            " FROM [dbo].[PrijemKontejneraVoz] " +
           " inner join Voz on Voz.ID = PrijemKontejneraVoz.IdVoza " +
           " where PrijemKontejneraVoz.StatusPrijema = 0  and PrijemKontejneraVoz.Vozom = 1 order by PrijemKontejneraVoz.[ID] desc";
            var s_connection = frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Br Voza";
            dataGridView1.Columns[1].Width = 80;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Relacija";
            dataGridView1.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "ETA";
            dataGridView1.Columns[3].Width = 150;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Status";
            dataGridView1.Columns[4].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "ATA";
            dataGridView1.Columns[5].Width = 150;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Korisnik";
            dataGridView1.Columns[6].Width = 100;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Datum";
            dataGridView1.Columns[7].Width = 100;

        }

        private void RefreshDataGridNajaveLeget()
        {
            var select = "SELECT PrijemKontejneraVoz.[ID],Voz.BrVoza, Voz.Relacija,  " +
" CONVERT(varchar, PrijemKontejneraVoz.[DatumPrijema], 104) + ' ' + SUBSTRING(CONVERT(varchar, PrijemKontejneraVoz.[DatumPrijema], 108), 1, 5) as ETA, " +
"  CASE WHEN PrijemKontejneraVoz.StatusPrijema = 0 THEN '1-Najava' ELSE '2-Prijem' END as Status, " +
"  CONVERT(varchar, PrijemKontejneraVoz.VremeDolaska, 104) + ' ' + SUBSTRING(CONVERT(varchar, PrijemKontejneraVoz.VremeDolaska, 108), 1, 5) as ATA, " +
" p1.PaNAziv as OperaterSRB, p2.PaNaziv as OperaterHR, PristizanjaUSid as DV_PristizanjaUSid, Sazeta as DV_SAzeta, " +
" (SELECT  Count(*) from UvozKonacna where UvozKonacna.IDNadredjeni = UvozKonacnaZaglavlje.ID and IDNadredjeni = UvozKonacnaZaglavlje.[ID]  ) as BrojSpakovanihKonterjnera,  " +
" (SELECT  SUM(UvozKonacna.TaraKontejnera) from UvozKonacna where UvozKonacna.IDNadredjeni = UvozKonacnaZaglavlje.ID and IDNadredjeni = UvozKonacnaZaglavlje.[ID]  ) as TaraSpakovanihKonterjnera,  " +
" (SELECT  SUM(UvozKonacna.BrutoKontejnera) from UvozKonacna where UvozKonacna.IDNadredjeni = UvozKonacnaZaglavlje.ID and IDNadredjeni = UvozKonacnaZaglavlje.[ID]  ) as BrutoSpakovanihKonterjnera, " +
" (SELECT  SUM(UvozKonacna.BrutoRobe) from UvozKonacna where UvozKonacna.IDNadredjeni = UvozKonacnaZaglavlje.ID and IDNadredjeni = UvozKonacnaZaglavlje.[ID]  ) as BrutoRobeSpakovanihKonterjnera, " +
"  [MaksimalnaBruto],[MaksimalnaDuzina] " +
" ,[MaksimalanBrojKola] " +
  "           FROM[dbo].[PrijemKontejneraVoz] " +
" inner join Voz on Voz.ID = PrijemKontejneraVoz.IdVoza " +
" inner join UvozKonacnaZaglavlje On Voz.ID = UvozKonacnaZaglavlje.IDVoza " +
" inner join Partnerji p1 on p1.PaSifra = Voz.OperaterSrbija " +
" inner join Partnerji p2 on p2.PaSifra = Voz.OperaterHR " +
           " where PrijemKontejneraVoz.StatusPrijema = 0  and PrijemKontejneraVoz.Vozom = 1 order by PrijemKontejneraVoz.[ID] desc";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Br Voza";
            dataGridView1.Columns[1].Width = 80;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Relacija";
            dataGridView1.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "ETA";
            dataGridView1.Columns[3].Width = 150;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Status";
            dataGridView1.Columns[4].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "ATA";
            dataGridView1.Columns[5].Width = 150;



        }

        private void RefreshDataGridPrijemi()
        {
            /*
            var select = "SELECT PrijemKontejneraVoz.[ID],Voz.BrVoza, Voz.Relacija, " +
             " CONVERT(varchar,PrijemKontejneraVoz.[DatumPrijema],104)      + ' '      + SUBSTRING(CONVERT(varchar,PrijemKontejneraVoz.[DatumPrijema],108),1,5) as ETA, " +
          " CASE WHEN PrijemKontejneraVoz.StatusPrijema = 0 THEN '1-Najava' ELSE '2-Prijem' END as Status, " +
         " CONVERT(varchar,PrijemKontejneraVoz.VremeDolaska,104)      + ' '      + SUBSTRING(CONVERT(varchar,PrijemKontejneraVoz.VremeDolaska,108),1,5) as ATA, " +
          "  [PrijemKontejneraVoz].Korisnik,[PrijemKontejneraVoz].Datum  " +
            " FROM [dbo].[PrijemKontejneraVoz] " +
           " inner join Voz on Voz.ID = PrijemKontejneraVoz.IdVoza " +
           " where PrijemKontejneraVoz.StatusPrijema = 1 and PrijemKontejneraVoz.Vozom = 1  order by PrijemKontejneraVoz.[ID] desc ";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);
            */

            var select = "SELECT PrijemKontejneraVoz.[ID],Voz.BrVoza, Voz.Relacija, " +
            " PrijemKontejneraVoz.[DatumPrijema] as ETA, " +
         " CASE WHEN PrijemKontejneraVoz.StatusPrijema = 0 THEN '1-Najava' ELSE '2-Prijem' END as Status, " +
        " PrijemKontejneraVoz.VremeDolaska as ATA, " +
         "  [PrijemKontejneraVoz].Korisnik,[PrijemKontejneraVoz].Datum  " +
           " FROM [dbo].[PrijemKontejneraVoz] " +
          " inner join Voz on Voz.ID = PrijemKontejneraVoz.IdVoza " +
          " where PrijemKontejneraVoz.StatusPrijema = 1 and PrijemKontejneraVoz.Vozom = 1  order by PrijemKontejneraVoz.[ID] desc ";
            var s_connection = frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);


            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Br Voza";
            dataGridView1.Columns[1].Width = 80;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Relacija";
            dataGridView1.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "ETA";
            dataGridView1.Columns[3].Width = 150;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Status";
            dataGridView1.Columns[4].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "ATA";
            dataGridView1.Columns[5].Width = 150;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Korisnik";
            dataGridView1.Columns[6].Width = 100;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Datum";
            dataGridView1.Columns[7].Width = 100;

        }

        private void RefreshDataGridPrijemiLeget()
        {
            /*
            var select = "SELECT PrijemKontejneraVoz.[ID],Voz.BrVoza, Voz.Relacija, " +
             " CONVERT(varchar,PrijemKontejneraVoz.[DatumPrijema],104)      + ' '      + SUBSTRING(CONVERT(varchar,PrijemKontejneraVoz.[DatumPrijema],108),1,5) as ETA, " +
          " CASE WHEN PrijemKontejneraVoz.StatusPrijema = 0 THEN '1-Najava' ELSE '2-Prijem' END as Status, " +
         " CONVERT(varchar,PrijemKontejneraVoz.VremeDolaska,104)      + ' '      + SUBSTRING(CONVERT(varchar,PrijemKontejneraVoz.VremeDolaska,108),1,5) as ATA, " +
          "  [PrijemKontejneraVoz].Korisnik,[PrijemKontejneraVoz].Datum  " +
            " FROM [dbo].[PrijemKontejneraVoz] " +
           " inner join Voz on Voz.ID = PrijemKontejneraVoz.IdVoza " +
           " where PrijemKontejneraVoz.StatusPrijema = 1 and PrijemKontejneraVoz.Vozom = 1  order by PrijemKontejneraVoz.[ID] desc ";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);
            */


            var select = "SELECT PrijemKontejneraVoz.[ID],Voz.BrVoza, Voz.Relacija,  " +
" CONVERT(varchar, PrijemKontejneraVoz.[DatumPrijema], 104) + ' ' + SUBSTRING(CONVERT(varchar, PrijemKontejneraVoz.[DatumPrijema], 108), 1, 5) as ETA, " +
"  CASE WHEN PrijemKontejneraVoz.StatusPrijema = 0 THEN '1-Najava' ELSE '2-Prijem' END as Status, " +
"  CONVERT(varchar, PrijemKontejneraVoz.VremeDolaska, 104) + ' ' + SUBSTRING(CONVERT(varchar, PrijemKontejneraVoz.VremeDolaska, 108), 1, 5) as ATA, " +
" p1.PaNAziv as OperaterSRB, p2.PaNaziv as OperaterHR, PristizanjaUSid as DV_PristizanjaUSid, Sazeta as DV_SAzeta, " +
" (SELECT  Count(*) from UvozKonacna where UvozKonacna.IDNadredjeni = UvozKonacnaZaglavlje.ID and IDNadredjeni = UvozKonacnaZaglavlje.[ID]  ) as BrojSpakovanihKonterjnera,  " +
" (SELECT  SUM(UvozKonacna.TaraKontejnera) from UvozKonacna where UvozKonacna.IDNadredjeni = UvozKonacnaZaglavlje.ID and IDNadredjeni = UvozKonacnaZaglavlje.[ID]  ) as TaraSpakovanihKonterjnera,  " +
" (SELECT  SUM(UvozKonacna.BrutoKontejnera) from UvozKonacna where UvozKonacna.IDNadredjeni = UvozKonacnaZaglavlje.ID and IDNadredjeni = UvozKonacnaZaglavlje.[ID]  ) as BrutoSpakovanihKonterjnera, " +
" (SELECT  SUM(UvozKonacna.BrutoRobe) from UvozKonacna where UvozKonacna.IDNadredjeni = UvozKonacnaZaglavlje.ID and IDNadredjeni = UvozKonacnaZaglavlje.[ID]  ) as BrutoRobeSpakovanihKonterjnera, " +
"  [MaksimalnaBruto],[MaksimalnaDuzina] " +
" ,[MaksimalanBrojKola] " +
  "           FROM[dbo].[PrijemKontejneraVoz] " +
" inner join Voz on Voz.ID = PrijemKontejneraVoz.IdVoza " +
" inner join UvozKonacnaZaglavlje On Voz.ID = UvozKonacnaZaglavlje.IDVoza " +
" inner join Partnerji p1 on p1.PaSifra = Voz.OperaterSrbija " +
" inner join Partnerji p2 on p2.PaSifra = Voz.OperaterHR " +
          " where PrijemKontejneraVoz.StatusPrijema = 1 and PrijemKontejneraVoz.Vozom = 1  order by PrijemKontejneraVoz.[ID] desc ";
            var s_connection = frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);


            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Br Voza";
            dataGridView1.Columns[1].Width = 80;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Relacija";
            dataGridView1.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "ETA";
            dataGridView1.Columns[3].Width = 150;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Status";
            dataGridView1.Columns[4].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "ATA";
            dataGridView1.Columns[5].Width = 150;



        }

        private void PrijemVozomPregled_Load(object sender, EventArgs e)
        {
            string Company = Saobracaj.Sifarnici.frmLogovanje.Firma;
            switch (Company)
            {
                case "Leget":
                    {
                        RefreshDataGridLeget();
                        return;

                    }
                default:
                    {
                        RefreshDataGrid();
                    }
                    break;
            }

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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string Company = Saobracaj.Sifarnici.frmLogovanje.Firma;
            switch (Company)
            {
                case "Leget":
                    {
                        Saobracaj.Dokumenta.frmPrijemKontejneraLegetVozUvoz ter2 = new Saobracaj.Dokumenta.frmPrijemKontejneraLegetVozUvoz(Convert.ToInt32(txtSifra.Text), KorisnikCene, 1);
                        ter2.Show();
                        return;

                    }
                default:
                    {

                        frmPrijemKontejneraVoz ter3 = new frmPrijemKontejneraVoz(Convert.ToInt32(txtSifra.Text), KorisnikCene, 1);
                        ter3.Show();
                        return;

                    }
                    break;
            }
            ///PANTA

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            string Company = Saobracaj.Sifarnici.frmLogovanje.Firma;
            switch (Company)
            {
                case "Leget":
                    {
                        RefreshDataGridLeget();
                        return;

                    }
                default:
                    {
                        RefreshDataGrid(); ;
                        return;

                    }
                    break;
            }


        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            string Company = Saobracaj.Sifarnici.frmLogovanje.Firma;
            switch (Company)
            {
                case "Leget":
                    {
                        Saobracaj.Dokumenta.frmPrijemKontejneraLegetVozUvoz ter2 = new Saobracaj.Dokumenta.frmPrijemKontejneraLegetVozUvoz(KorisnikCene, 1);
                        ter2.Show();
                        return;

                    }
                default:
                    {

                        frmPrijemKontejneraVoz ter3 = new frmPrijemKontejneraVoz(KorisnikCene, 1);
                        ter3.Show();
                        return;

                    }
                    break;
            }



        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            string Company = Saobracaj.Sifarnici.frmLogovanje.Firma;
            switch (Company)
            {
                case "Leget":
                    {
                        RefreshDataGridNajaveLeget();
                        return;

                    }
                default:
                    {
                        RefreshDataGridNajave();
                    }
                    break;
            }

            //  RefreshDataGridNajave();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            string Company = Saobracaj.Sifarnici.frmLogovanje.Firma;
            switch (Company)
            {
                case "Leget":
                    {
                        RefreshDataGridPrijemiLeget();
                        return;

                    }
                default:
                    {
                        RefreshDataGridPrijemi();
                    }
                    break;
            }



            // RefreshDataGridPrijemi();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Company = Saobracaj.Sifarnici.frmLogovanje.Firma;
            switch (Company)
            {
                case "Leget":
                    {
                        RefreshDataGridPoBukinguBrodaraLeget();
                        return;

                    }
                default:
                    {
                        RefreshDataGridPoBukinguBrodara();
                    }
                    break;
            }
            // RefreshDataGridPoBukinguBrodara();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Company = Saobracaj.Sifarnici.frmLogovanje.Firma;
            switch (Company)
            {
                case "Leget":
                    {
                        RefreshDataGridPoKontejneruLeget();
                        return;

                    }
                default:
                    {
                        RefreshDataGridPoKontejneru();
                    }
                    break;
            }

        }

        private void RefreshDataGridPoKontejneru()
        {
            var select = "SELECT PrijemKontejneraVoz.[ID],Voz.BrVoza, Voz.Relacija, " +
                " CONVERT(varchar,PrijemKontejneraVoz.[DatumPrijema],104)      + ' '      + SUBSTRING(CONVERT(varchar,PrijemKontejneraVoz.[DatumPrijema],108),1,5) as ETA, " +
             " CASE WHEN PrijemKontejneraVoz.StatusPrijema = 0 THEN '1-Najava' ELSE '2-Prijem' END as Status, " +
            " CONVERT(varchar,PrijemKontejneraVoz.VremeDolaska,104)      + ' '      + SUBSTRING(CONVERT(varchar,PrijemKontejneraVoz.VremeDolaska,108),1,5) as ATA, " +
             "  [PrijemKontejneraVoz].Korisnik,[PrijemKontejneraVoz].Datum  " +
               " FROM [dbo].[PrijemKontejneraVoz] " +
              " inner join Voz on Voz.ID = PrijemKontejneraVoz.IdVoza  " +
              " inner join prijemKontejneraVozstavke on PrijemKontejneraVozStavke.IdNadredjenog = PrijemKontejneraVoz.ID " +
              " where PrijemKontejneraVoz.Vozom = 1 and PrijemKontejneraVozStavke.BrojKontejnera = '" + txtBrojKontejnera.Text + " 'order by PrijemKontejneraVoz.[ID] desc ";
            var s_connection = frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Br Voza";
            dataGridView1.Columns[1].Width = 80;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Relacija";
            dataGridView1.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "ETA";
            dataGridView1.Columns[3].Width = 150;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Status";
            dataGridView1.Columns[4].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "ATA";
            dataGridView1.Columns[5].Width = 150;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Korisnik";
            dataGridView1.Columns[6].Width = 100;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Datum";
            dataGridView1.Columns[7].Width = 100;
        }

        private void RefreshDataGridPoKontejneruLeget()
        {
            var select = "SELECT PrijemKontejneraVoz.[ID],Voz.BrVoza, Voz.Relacija,  " +
    " CONVERT(varchar, PrijemKontejneraVoz.[DatumPrijema], 104) + ' ' + SUBSTRING(CONVERT(varchar, PrijemKontejneraVoz.[DatumPrijema], 108), 1, 5) as ETA, " +
    "  CASE WHEN PrijemKontejneraVoz.StatusPrijema = 0 THEN '1-Najava' ELSE '2-Prijem' END as Status, " +
    "  CONVERT(varchar, PrijemKontejneraVoz.VremeDolaska, 104) + ' ' + SUBSTRING(CONVERT(varchar, PrijemKontejneraVoz.VremeDolaska, 108), 1, 5) as ATA, " +
    " p1.PaNAziv as OperaterSRB, p2.PaNaziv as OperaterHR, PristizanjaUSid as DV_PristizanjaUSid, Sazeta as DV_SAzeta, " +
    " (SELECT  Count(*) from UvozKonacna where UvozKonacna.IDNadredjeni = UvozKonacnaZaglavlje.ID and IDNadredjeni = UvozKonacnaZaglavlje.[ID]  ) as BrojSpakovanihKonterjnera,  " +
    " (SELECT  SUM(UvozKonacna.TaraKontejnera) from UvozKonacna where UvozKonacna.IDNadredjeni = UvozKonacnaZaglavlje.ID and IDNadredjeni = UvozKonacnaZaglavlje.[ID]  ) as TaraSpakovanihKonterjnera,  " +
    " (SELECT  SUM(UvozKonacna.BrutoKontejnera) from UvozKonacna where UvozKonacna.IDNadredjeni = UvozKonacnaZaglavlje.ID and IDNadredjeni = UvozKonacnaZaglavlje.[ID]  ) as BrutoSpakovanihKonterjnera, " +
    " (SELECT  SUM(UvozKonacna.BrutoRobe) from UvozKonacna where UvozKonacna.IDNadredjeni = UvozKonacnaZaglavlje.ID and IDNadredjeni = UvozKonacnaZaglavlje.[ID]  ) as BrutoRobeSpakovanihKonterjnera, " +
    "  [MaksimalnaBruto],[MaksimalnaDuzina] " +
    " ,[MaksimalanBrojKola] " +
      "           FROM[dbo].[PrijemKontejneraVoz] " +
    " inner join Voz on Voz.ID = PrijemKontejneraVoz.IdVoza " +
    " inner join UvozKonacnaZaglavlje On Voz.ID = UvozKonacnaZaglavlje.IDVoza " +
    " inner join Partnerji p1 on p1.PaSifra = Voz.OperaterSrbija " +
    " inner join Partnerji p2 on p2.PaSifra = Voz.OperaterHR " +
                  " where PrijemKontejneraVoz.Vozom = 1 and PrijemKontejneraVozStavke.BrojKontejnera = '" + txtBrojKontejnera.Text + " 'order by PrijemKontejneraVoz.[ID] desc ";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Br Voza";
            dataGridView1.Columns[1].Width = 80;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Relacija";
            dataGridView1.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "ETA";
            dataGridView1.Columns[3].Width = 150;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Status";
            dataGridView1.Columns[4].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "ATA";
            dataGridView1.Columns[5].Width = 150;

        }

        private void RefreshDataGridPoBukinguBrodara()
        {
            var select = "SELECT PrijemKontejneraVoz.[ID],Voz.BrVoza, Voz.Relacija, " +
                " CONVERT(varchar,PrijemKontejneraVoz.[DatumPrijema],104)      + ' '      + SUBSTRING(CONVERT(varchar,PrijemKontejneraVoz.[DatumPrijema],108),1,5) as ETA, " +
             " CASE WHEN PrijemKontejneraVoz.StatusPrijema = 0 THEN '1-Najava' ELSE '2-Prijem' END as Status, " +
            " CONVERT(varchar,PrijemKontejneraVoz.VremeDolaska,104)      + ' '      + SUBSTRING(CONVERT(varchar,PrijemKontejneraVoz.VremeDolaska,108),1,5) as ATA, " +
             "  [PrijemKontejneraVoz].Korisnik,[PrijemKontejneraVoz].Datum  " +
               " FROM [dbo].[PrijemKontejneraVoz] " +
              " inner join Voz on Voz.ID = PrijemKontejneraVoz.IdVoza  " +
              " inner join PrijemKontejneraVozStavke on PrijemKontejneraVozStavke.IdNadredjenog = PrijemKontejneraVoz.ID " +
              " where PrijemKontejneraVoz.Vozom = 1 and PrijemKontejneraVozStavke.BukingBrodar = '" + txtBukingBrodar.Text + "' order by PrijemKontejneraVoz.[ID] desc ";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Br Voza";
            dataGridView1.Columns[1].Width = 80;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Relacija";
            dataGridView1.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "ETA";
            dataGridView1.Columns[3].Width = 150;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Status";
            dataGridView1.Columns[4].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "ATA";
            dataGridView1.Columns[5].Width = 150;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Korisnik";
            dataGridView1.Columns[6].Width = 100;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Datum";
            dataGridView1.Columns[7].Width = 100;
        }

        private void RefreshDataGridPoBukinguBrodaraLeget()
        {

            var select = "SELECT PrijemKontejneraVoz.[ID],Voz.BrVoza, Voz.Relacija,  " +
    " CONVERT(varchar, PrijemKontejneraVoz.[DatumPrijema], 104) + ' ' + SUBSTRING(CONVERT(varchar, PrijemKontejneraVoz.[DatumPrijema], 108), 1, 5) as ETA, " +
    "  CASE WHEN PrijemKontejneraVoz.StatusPrijema = 0 THEN '1-Najava' ELSE '2-Prijem' END as Status, " +
    "  CONVERT(varchar, PrijemKontejneraVoz.VremeDolaska, 104) + ' ' + SUBSTRING(CONVERT(varchar, PrijemKontejneraVoz.VremeDolaska, 108), 1, 5) as ATA, " +
    " p1.PaNAziv as OperaterSRB, p2.PaNaziv as OperaterHR, PristizanjaUSid as DV_PristizanjaUSid, Sazeta as DV_SAzeta, " +
    " (SELECT  Count(*) from UvozKonacna where UvozKonacna.IDNadredjeni = UvozKonacnaZaglavlje.ID and IDNadredjeni = UvozKonacnaZaglavlje.[ID]  ) as BrojSpakovanihKonterjnera,  " +
    " (SELECT  SUM(UvozKonacna.TaraKontejnera) from UvozKonacna where UvozKonacna.IDNadredjeni = UvozKonacnaZaglavlje.ID and IDNadredjeni = UvozKonacnaZaglavlje.[ID]  ) as TaraSpakovanihKonterjnera,  " +
    " (SELECT  SUM(UvozKonacna.BrutoKontejnera) from UvozKonacna where UvozKonacna.IDNadredjeni = UvozKonacnaZaglavlje.ID and IDNadredjeni = UvozKonacnaZaglavlje.[ID]  ) as BrutoSpakovanihKonterjnera, " +
    " (SELECT  SUM(UvozKonacna.BrutoRobe) from UvozKonacna where UvozKonacna.IDNadredjeni = UvozKonacnaZaglavlje.ID and IDNadredjeni = UvozKonacnaZaglavlje.[ID]  ) as BrutoRobeSpakovanihKonterjnera, " +
    "  [MaksimalnaBruto],[MaksimalnaDuzina] " +
    " ,[MaksimalanBrojKola] " +
      "           FROM[dbo].[PrijemKontejneraVoz] " +
    " inner join Voz on Voz.ID = PrijemKontejneraVoz.IdVoza " +
    " inner join UvozKonacnaZaglavlje On Voz.ID = UvozKonacnaZaglavlje.IDVoza " +
    " inner join Partnerji p1 on p1.PaSifra = Voz.OperaterSrbija " +
    " inner join Partnerji p2 on p2.PaSifra = Voz.OperaterHR " +
              " where PrijemKontejneraVoz.Vozom = 1 and PrijemKontejneraVozStavke.BukingBrodar = '" + txtBukingBrodar.Text + "' order by PrijemKontejneraVoz.[ID] desc ";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Br Voza";
            dataGridView1.Columns[1].Width = 80;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Relacija";
            dataGridView1.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "ETA";
            dataGridView1.Columns[3].Width = 150;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Status";
            dataGridView1.Columns[4].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "ATA";
            dataGridView1.Columns[5].Width = 150;

        }
    }
}

