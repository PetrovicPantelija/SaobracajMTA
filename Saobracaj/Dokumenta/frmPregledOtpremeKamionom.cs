

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


namespace TrackModal.Dokumeta
{
    public partial class frmPregledOtpremeKamionom : Syncfusion.Windows.Forms.Office2010Form
    {
        public static string code = "frmPregledOtpremeKamionom";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Saobracaj.Sifarnici.frmLogovanje.user.ToString();
        string niz = "";
        string KorisnikCene;

        public frmPregledOtpremeKamionom()
        {
            InitializeComponent();

            string Company = Saobracaj.Sifarnici.frmLogovanje.Firma;
            switch (Company)
            {
                case "Leget":
                    {
                        toolStripButton7.Visible = false;
                        toolStripButton1.Visible = true;
                        toolStripButton4.Visible = true;
                        panelLeget.Visible = true;
                        break;
                    }
                default:
                    {

                        break;

                    }
              
            }
        }

        public frmPregledOtpremeKamionom(string Korisnik)
        {
            InitializeComponent();
            KorisnikCene = Korisnik;

            string Company = Saobracaj.Sifarnici.frmLogovanje.Firma;
            switch (Company)
            {
                case "Leget":
                    {
                        toolStripButton7.Visible = false;
                        toolStripButton1.Visible = true;
                        toolStripButton4.Visible = true;
                        panelLeget.Visible = true;
                        break;
                    }
                default:
                    {

                        break;

                    }

            }
        }
        
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void RefreshDataGrid()
        {
            if (chkKamionom.Checked == false)
            {
                var select = "SELECT top 500 [ID],[DatumOtpreme],[StatusOtpreme],[IdVoza],[RegBrKamiona]," +
                    "[ImeVozaca],[VremeOdlaska] ,[NacinOtpreme] ,[Datum] ,[Korisnik],  " +
                    " (  SELECT  STUFF((SELECT distinct   '/ ' + Cast(ts.BrojKontejnera as nvarchar(20)) " +
 "  FROM OtpremaKontejneraVozStavke ts where n1.ID = ts.IDNadredjenog " +
 " FOR XML PATH('')), 1, 1, ''  ) As Skupljen) " +
 " as Kontejner " +
                    "FROM [dbo].[OtpremaKontejnera] as n1 where NacinOtpreme = 1 order by ID desc";
                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
                dataGridView1.Columns[1].HeaderText = "Datum otpreme";
                dataGridView1.Columns[1].Width = 150;

                DataGridViewColumn column3 = dataGridView1.Columns[2];
                dataGridView1.Columns[2].HeaderText = "Status otpreme";
                dataGridView1.Columns[2].Width = 50;

                DataGridViewColumn column4 = dataGridView1.Columns[3];
                dataGridView1.Columns[3].HeaderText = "Voz";
                dataGridView1.Columns[3].Width = 100;

                DataGridViewColumn column5 = dataGridView1.Columns[4];
                dataGridView1.Columns[4].HeaderText = "Reg Br Kamiona";
                dataGridView1.Columns[4].Width = 100;

                DataGridViewColumn column6 = dataGridView1.Columns[5];
                dataGridView1.Columns[5].HeaderText = "Ime vozača";
                dataGridView1.Columns[5].Width = 100;

                DataGridViewColumn column7 = dataGridView1.Columns[6];
                dataGridView1.Columns[6].HeaderText = "Otpremljen vozom";
                dataGridView1.Columns[6].Width = 100;

                DataGridViewColumn column8 = dataGridView1.Columns[7];
                dataGridView1.Columns[7].HeaderText = "Datum odlaska";
                dataGridView1.Columns[7].Width = 100;

                DataGridViewColumn column9 = dataGridView1.Columns[8];
                dataGridView1.Columns[8].HeaderText = "Datum";
                dataGridView1.Columns[8].Width = 100;

                DataGridViewColumn column10 = dataGridView1.Columns[9];
                dataGridView1.Columns[9].HeaderText = "Korisnik";
                dataGridView1.Columns[9].Width = 100;
            }
            else
            {
                var select = "SELECT top 500 [ID],[DatumOtpreme],[StatusOtpreme]," +                   
                    " [IdVoza],[RegBrKamiona],[ImeVozaca],[VremeOdlaska] ,[NacinOtpreme] ," +
                    " [Datum] ,[Korisnik],  " +
                     " CASE WHEN Poreklo = 0 THEN 'Uvoz' ELSE 'Izvoz' END " +
                    " FROM [dbo].[OtpremaKontejnera] where NacinOtpreme = 0 order by ID desc";
                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
                dataGridView1.Columns[1].HeaderText = "Datum otpreme";
                dataGridView1.Columns[1].Width = 150;

                DataGridViewColumn column3 = dataGridView1.Columns[2];
                dataGridView1.Columns[2].HeaderText = "Status otpreme";
                dataGridView1.Columns[2].Width = 50;

                DataGridViewColumn column4 = dataGridView1.Columns[3];
                dataGridView1.Columns[3].HeaderText = "Voz";
                dataGridView1.Columns[3].Width = 100;

                DataGridViewColumn column5 = dataGridView1.Columns[4];
                dataGridView1.Columns[4].HeaderText = "Reg Br Kamiona";
                dataGridView1.Columns[4].Width = 100;

                DataGridViewColumn column6 = dataGridView1.Columns[5];
                dataGridView1.Columns[5].HeaderText = "Ime vozača";
                dataGridView1.Columns[5].Width = 100;

                DataGridViewColumn column7 = dataGridView1.Columns[6];
                dataGridView1.Columns[6].HeaderText = "Otpremljen vozom";
                dataGridView1.Columns[6].Width = 100;

                DataGridViewColumn column8 = dataGridView1.Columns[7];
                dataGridView1.Columns[7].HeaderText = "Datum odlaska";
                dataGridView1.Columns[7].Width = 100;

                DataGridViewColumn column9 = dataGridView1.Columns[8];
                dataGridView1.Columns[8].HeaderText = "Datum";
                dataGridView1.Columns[8].Width = 100;

                DataGridViewColumn column10 = dataGridView1.Columns[9];
                dataGridView1.Columns[9].HeaderText = "Korisnik";
                dataGridView1.Columns[9].Width = 100;
            
            }
        }

        private void RefreshDataGridLeget()
        {

            string pomPoreklo = "";
            int uslov = 0;

            string pomModul = "";
            int uslovModul = 0;
            if (chkUvoz.Checked == true)
            {
                pomModul = "1";
                uslovModul = 1;

            }
            if (chkIzvoz.Checked == true && uslovModul == 1)
            {
                pomModul = pomModul + ",2";
                uslovModul = 1;
            }
            if (chkIzvoz.Checked == true && uslovModul == 0)
            {
                pomModul = pomModul + "2";
            }
            if (chkTerminal.Checked == true && uslovModul == 1)
            {
                pomModul = pomModul + ",4";
            }
            if (chkTerminal.Checked == true && uslovModul == 0)
            {
                pomModul = pomModul + "4";
            }


            if (chkPlatforma.Checked == true)
            {
                pomPoreklo = "0";
                uslov = 1;

            }
            if (chkCirada.Checked == true && uslov == 1)
            {
                pomPoreklo = pomPoreklo + ",1";
                uslov = 1;
            }
            if (chkCirada.Checked == true && uslov == 0)
            {
                pomPoreklo = pomPoreklo + "1";
            }
            //Kod Izvoza VrstaKamion je CIRada, Platforma a Poreklo je OJ
            if (chkKamionom.Checked == false)
            {
                var select = "SELECT top 500 [ID],[DatumOtpreme],[StatusOtpreme],[IdVoza],[RegBrKamiona]," +
                    "[ImeVozaca],[VremeOdlaska] ,[NacinOtpreme] ,[Datum] ,[Korisnik],  " +
                    " (  SELECT  STUFF((SELECT distinct   '/ ' + Cast(ts.BrojKontejnera as nvarchar(20)) " +
 "  FROM OtpremaKontejneraVozStavke ts where n1.ID = ts.IDNadredjenog " +
 " FOR XML PATH('')), 1, 1, ''  ) As Skupljen) " +
 " as Kontejner, VrstaKamiona, Poreklo " +
                    "FROM [dbo].[OtpremaKontejnera] as n1 where NacinOtpreme = 1  order by ID desc";
                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
                dataGridView1.Columns[1].HeaderText = "Datum otpreme";
                dataGridView1.Columns[1].Width = 150;

                DataGridViewColumn column3 = dataGridView1.Columns[2];
                dataGridView1.Columns[2].HeaderText = "Status otpreme";
                dataGridView1.Columns[2].Width = 50;

                DataGridViewColumn column4 = dataGridView1.Columns[3];
                dataGridView1.Columns[3].HeaderText = "Voz";
                dataGridView1.Columns[3].Width = 100;

                DataGridViewColumn column5 = dataGridView1.Columns[4];
                dataGridView1.Columns[4].HeaderText = "Reg Br Kamiona";
                dataGridView1.Columns[4].Width = 100;

                DataGridViewColumn column6 = dataGridView1.Columns[5];
                dataGridView1.Columns[5].HeaderText = "Ime vozača";
                dataGridView1.Columns[5].Width = 100;

                DataGridViewColumn column7 = dataGridView1.Columns[6];
                dataGridView1.Columns[6].HeaderText = "Otpremljen vozom";
                dataGridView1.Columns[6].Width = 100;

                DataGridViewColumn column8 = dataGridView1.Columns[7];
                dataGridView1.Columns[7].HeaderText = "Datum odlaska";
                dataGridView1.Columns[7].Width = 100;

                DataGridViewColumn column9 = dataGridView1.Columns[8];
                dataGridView1.Columns[8].HeaderText = "Datum";
                dataGridView1.Columns[8].Width = 100;

                DataGridViewColumn column10 = dataGridView1.Columns[9];
                dataGridView1.Columns[9].HeaderText = "Korisnik";
                dataGridView1.Columns[9].Width = 100;
            }
            else
            {
                var select = "SELECT top 500 [ID],[DatumOtpreme],[StatusOtpreme]," +
                    " [IdVoza],[RegBrKamiona],[ImeVozaca],[VremeOdlaska] ,[NacinOtpreme] ," +
                    " [Datum] ,[Korisnik],  " +
                     " CASE WHEN Poreklo = 0 THEN 'Uvoz' ELSE 'Izvoz' END " +
                    " FROM [dbo].[OtpremaKontejnera] where NacinOtpreme = 0 and Poreklo in ( " + pomModul + ") and VrstaKamiona in ( " + pomPoreklo + ") order by ID desc";
                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
                dataGridView1.Columns[1].HeaderText = "Datum otpreme";
                dataGridView1.Columns[1].Width = 150;

                DataGridViewColumn column3 = dataGridView1.Columns[2];
                dataGridView1.Columns[2].HeaderText = "Status otpreme";
                dataGridView1.Columns[2].Width = 50;

                DataGridViewColumn column4 = dataGridView1.Columns[3];
                dataGridView1.Columns[3].HeaderText = "Voz";
                dataGridView1.Columns[3].Width = 100;

                DataGridViewColumn column5 = dataGridView1.Columns[4];
                dataGridView1.Columns[4].HeaderText = "Reg Br Kamiona";
                dataGridView1.Columns[4].Width = 100;

                DataGridViewColumn column6 = dataGridView1.Columns[5];
                dataGridView1.Columns[5].HeaderText = "Ime vozača";
                dataGridView1.Columns[5].Width = 100;

                DataGridViewColumn column7 = dataGridView1.Columns[6];
                dataGridView1.Columns[6].HeaderText = "Otpremljen vozom";
                dataGridView1.Columns[6].Width = 100;

                DataGridViewColumn column8 = dataGridView1.Columns[7];
                dataGridView1.Columns[7].HeaderText = "Datum odlaska";
                dataGridView1.Columns[7].Width = 100;

                DataGridViewColumn column9 = dataGridView1.Columns[8];
                dataGridView1.Columns[8].HeaderText = "Datum";
                dataGridView1.Columns[8].Width = 100;

                DataGridViewColumn column10 = dataGridView1.Columns[9];
                dataGridView1.Columns[9].HeaderText = "Korisnik";
                dataGridView1.Columns[9].Width = 100;

            }
        }

        private void PrijemVozomPregled_Load(object sender, EventArgs e)
        {
            RefreshDataGrid();
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
                        Saobracaj.Dokumenta.frmOtpremaKontejneraIzvozKamion ter2 = new Saobracaj.Dokumenta.frmOtpremaKontejneraIzvozKamion(Convert.ToInt32(txtSifra.Text), KorisnikCene);
                        ter2.Show();
                        return;
                    }
                default:
                    {

                        Saobracaj.Dokumeta.frmOtpremaKontejnera ter3 = new Saobracaj.Dokumeta.frmOtpremaKontejnera(Convert.ToInt32(txtSifra.Text), KorisnikCene);
                        ter3.Show();
                        return;

                    }
                    break;
            }

            // Saobracaj.Dokumeta.frmOtpremaKontejnera ter = new Saobracaj.Dokumeta.frmOtpremaKontejnera(Convert.ToInt32(txtSifra.Text), KorisnikCene);
            // ter.Show();
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

                        RefreshDataGrid();
                        return;

                    }
                    break;
            }


       
        }

        private void frmPregledOtpreme_Load(object sender, EventArgs e)
        {
           // RefreshDataGrid();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            Saobracaj.Dokumeta.frmOtpremaKontejnera otpr = new Saobracaj.Dokumeta.frmOtpremaKontejnera(Convert.ToInt32(txtSifra.Text), KorisnikCene);
            otpr.Show();
        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
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

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            string Company = Saobracaj.Sifarnici.frmLogovanje.Firma;
            switch (Company)
            {
                case "Leget":
                    {
                        Saobracaj.Dokumenta.frmOtpremaKontejneraUvozKamion ter2 = new Saobracaj.Dokumenta.frmOtpremaKontejneraUvozKamion( KorisnikCene, 0);
                        ter2.Show();
                        return;

                    }
                default:
                    {

                        Saobracaj.Dokumeta.frmOtpremaKontejnera ter3 = new Saobracaj.Dokumeta.frmOtpremaKontejnera(KorisnikCene, 0);
                        ter3.Show();
                        return;

                    }
                    break;
            }

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            string Company = Saobracaj.Sifarnici.frmLogovanje.Firma;
            switch (Company)
            {
                case "Leget":
                    {
                        REfreshDataGridOtpremaNajavaLeget();
                        return;

                    }
                default:
                    {

                        REfreshDataGridOtpremaNajava();
                        return;

                    }
                    break;
            }


            
        }

        public void REfreshDataGridOtpremaNajavaLeget()
        {
            string pomPoreklo = "";
            int uslov = 0;

            string pomModul = "";
            int uslovModul = 0;
            if (chkUvoz.Checked == true)
            {
                pomModul = "1";
                uslovModul = 1;

            }
            if (chkIzvoz.Checked == true && uslovModul == 1)
            {
                pomModul = pomModul + ",2";
                uslovModul = 1;
            }
            if (chkIzvoz.Checked == true && uslovModul == 0)
            {
                pomModul = pomModul + "2";
            }
            if (chkTerminal.Checked == true && uslovModul == 1)
            {
                pomModul = pomModul + ",4";
            }
            if (chkTerminal.Checked == true && uslovModul == 0)
            {
                pomModul = pomModul + "4";
            }


            if (chkPlatforma.Checked == true)
            {
                pomPoreklo = "0";
                uslov = 1;

            }
            if (chkCirada.Checked == true && uslov == 1)
            {
                pomPoreklo = pomPoreklo + ",1";
                uslov = 1;
            }
            if (chkCirada.Checked == true && uslov == 0)
            {
                pomPoreklo = pomPoreklo + "1";
            }


            var select = " SELECT top 500 n1.[ID],RegBrKamiona, ImeVozaca, " +
                " n1.DatumOtpreme as ETA, " +
                        " CASE WHEN n1.StatusOtpreme = 0 THEN '1-Najava' ELSE '2-Otpremljen' END as Status, " +
                     " n1.VremeOdlaska as ATA, " +
           "  n1.[Datum] ,n1.[Korisnik], " +
           " (  SELECT  STUFF((SELECT distinct   '/ ' + Cast(ts.BrojKontejnera as nvarchar(20)) " +
 "  FROM OtpremaKontejneraVozStavke ts where n1.ID = ts.IDNadredjenog " +
 " FOR XML PATH('')), 1, 1, ''  ) As Skupljen) " +
 " as Kontejner, VrstaKamiona, Poreklo " +
          "   FROM [dbo].[OtpremaKontejnera] as n1 " +
             " where n1.StatusOtpreme = 0  " +
           " and NacinOtpreme = 0 and Poreklo in ( " + pomModul + ") and VrstaKamiona in ( " + pomPoreklo + ") order by n1.ID desc ";


            /*
            var select = " SELECT top 500 n1.[ID],RegBrKamiona, ImeVozaca, " +
                " CONVERT(varchar,n1.DatumOtpreme,104)      + ' '      + SUBSTRING(CONVERT(varchar,n1.[DatumOtpreme],108),1,5) as ETA, " +
                        " CASE WHEN n1.StatusOtpreme = 0 THEN '1-Najava' ELSE '2-Otpremljen' END as Status, " +
                     " CONVERT(varchar,n1.VremeOdlaska,104)      + ' '      + SUBSTRING(CONVERT(varchar,n1.[VremeOdlaska],108),1,5) as ATA, " +
           "  n1.[Datum] ,n1.[Korisnik], " +
           " (  SELECT  STUFF((SELECT distinct   '/ ' + Cast(ts.BrojKontejnera as nvarchar(20)) " +
 "  FROM OtpremaKontejneraVozStavke ts where n1.ID = ts.IDNadredjenog " +
 " FOR XML PATH('')), 1, 1, ''  ) As Skupljen) " +
 " as Kontejner " +
          "   FROM [dbo].[OtpremaKontejnera] as n1 " +
             " where n1.StatusOtpreme = 0  " +
           " and NacinOtpreme = 0 order by n1.ID desc ";
            */
            /*
             var select = "SELECT PrijemKontejneraVoz.[ID],Voz.BrVoza, Voz.Relacija, " +
                " CONVERT(varchar,PrijemKontejneraVoz.[DatumPrijema],104)      + ' '      + SUBSTRING(CONVERT(varchar,PrijemKontejneraVoz.[DatumPrijema],108),1,5) as ETA, " +
             " CASE WHEN PrijemKontejneraVoz.StatusPrijema = 0 THEN '1-Najava' ELSE '2-Prijem' END as Status, " +
            " CONVERT(varchar,PrijemKontejneraVoz.VremeDolaska,104)      + ' '      + SUBSTRING(CONVERT(varchar,PrijemKontejneraVoz.VremeDolaska,108),1,5) as ATA, " +
             "  [PrijemKontejneraVoz].Korisnik,[PrijemKontejneraVoz].Datum  " +
               " FROM [dbo].[PrijemKontejneraVoz] " +
              " inner join Voz on Voz.ID = PrijemKontejneraVoz.IdVoza " +
              " where PrijemKontejneraVoz.StatusPrijema = 0  order by PrijemKontejneraVoz.[ID] desc";
            */
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
            dataGridView1.Columns[1].HeaderText = "Reg br";
            dataGridView1.Columns[1].Width = 80;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Ime vozača";
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

        public void REfreshDataGridOtpremaNajava()
        {
            var select = " SELECT top 500 n1.[ID],RegBrKamiona, ImeVozaca, " +
                " n1.DatumOtpreme as ETA, " +
                        " CASE WHEN n1.StatusOtpreme = 0 THEN '1-Najava' ELSE '2-Otpremljen' END as Status, " +
                     " n1.VremeOdlaska as ATA, " +
           "  n1.[Datum] ,n1.[Korisnik], " +
           " (  SELECT  STUFF((SELECT distinct   '/ ' + Cast(ts.BrojKontejnera as nvarchar(20)) " +
 "  FROM OtpremaKontejneraVozStavke ts where n1.ID = ts.IDNadredjenog " +
 " FOR XML PATH('')), 1, 1, ''  ) As Skupljen) " +
 " as Kontejner " +
          "   FROM [dbo].[OtpremaKontejnera] as n1 " +
             " where n1.StatusOtpreme = 0  " +
           " and NacinOtpreme = 0 order by n1.ID desc ";


            /*
            var select = " SELECT top 500 n1.[ID],RegBrKamiona, ImeVozaca, " +
                " CONVERT(varchar,n1.DatumOtpreme,104)      + ' '      + SUBSTRING(CONVERT(varchar,n1.[DatumOtpreme],108),1,5) as ETA, " +
                        " CASE WHEN n1.StatusOtpreme = 0 THEN '1-Najava' ELSE '2-Otpremljen' END as Status, " +
                     " CONVERT(varchar,n1.VremeOdlaska,104)      + ' '      + SUBSTRING(CONVERT(varchar,n1.[VremeOdlaska],108),1,5) as ATA, " +
           "  n1.[Datum] ,n1.[Korisnik], " +
           " (  SELECT  STUFF((SELECT distinct   '/ ' + Cast(ts.BrojKontejnera as nvarchar(20)) " +
 "  FROM OtpremaKontejneraVozStavke ts where n1.ID = ts.IDNadredjenog " +
 " FOR XML PATH('')), 1, 1, ''  ) As Skupljen) " +
 " as Kontejner " +
          "   FROM [dbo].[OtpremaKontejnera] as n1 " +
             " where n1.StatusOtpreme = 0  " +
           " and NacinOtpreme = 0 order by n1.ID desc ";
            */
            /*
             var select = "SELECT PrijemKontejneraVoz.[ID],Voz.BrVoza, Voz.Relacija, " +
                " CONVERT(varchar,PrijemKontejneraVoz.[DatumPrijema],104)      + ' '      + SUBSTRING(CONVERT(varchar,PrijemKontejneraVoz.[DatumPrijema],108),1,5) as ETA, " +
             " CASE WHEN PrijemKontejneraVoz.StatusPrijema = 0 THEN '1-Najava' ELSE '2-Prijem' END as Status, " +
            " CONVERT(varchar,PrijemKontejneraVoz.VremeDolaska,104)      + ' '      + SUBSTRING(CONVERT(varchar,PrijemKontejneraVoz.VremeDolaska,108),1,5) as ATA, " +
             "  [PrijemKontejneraVoz].Korisnik,[PrijemKontejneraVoz].Datum  " +
               " FROM [dbo].[PrijemKontejneraVoz] " +
              " inner join Voz on Voz.ID = PrijemKontejneraVoz.IdVoza " +
              " where PrijemKontejneraVoz.StatusPrijema = 0  order by PrijemKontejneraVoz.[ID] desc";
            */
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
            dataGridView1.Columns[1].HeaderText = "Reg br";
            dataGridView1.Columns[1].Width = 80;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Ime vozača";
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

        public void REfreshDataGridOtpremaOtpremljenLeget()
        {
            string pomPoreklo = "";
            int uslov = 0;

            string pomModul = "";
            int uslovModul = 0;
            if (chkUvoz.Checked == true)
            {
                pomModul = "1";
                uslovModul = 1;

            }
            if (chkIzvoz.Checked == true && uslovModul == 1)
            {
                pomModul = pomModul + ",2";
                uslovModul = 1;
            }
            if (chkIzvoz.Checked == true && uslovModul == 0)
            {
                pomModul = pomModul + "2";
            }
            if (chkTerminal.Checked == true && uslovModul == 1)
            {
                pomModul = pomModul + ",4";
            }
            if (chkTerminal.Checked == true && uslovModul == 0)
            {
                pomModul = pomModul + "4";
            }


            if (chkPlatforma.Checked == true)
            {
                pomPoreklo = "0";
                uslov = 1;

            }
            if (chkCirada.Checked == true && uslov == 1)
            {
                pomPoreklo = pomPoreklo + ",1";
                uslov = 1;
            }
            if (chkCirada.Checked == true && uslov == 0)
            {
                pomPoreklo = pomPoreklo + "1";
            }


            var select = " SELECT top 500 n1.[ID],RegBrKamiona, ImeVozaca, " +
                " n1.DatumOtpreme as ETA, " +
                        " CASE WHEN n1.StatusOtpreme = 0 THEN '1-Najava' ELSE '2-Otpremljen' END as Status, " +
                     " n1.VremeOdlaska as ATA, " +
           "  n1.[Datum] ,n1.[Korisnik], " +
            " (  SELECT  STUFF((SELECT distinct   '/ ' + Cast(ts.BrojKontejnera as nvarchar(20)) " +
 "  FROM OtpremaKontejneraVozStavke ts where n1.ID = ts.IDNadredjenog " +
 " FOR XML PATH('')), 1, 1, ''  ) As Skupljen) " +
 " as Kontejner, VrstaKamiona, Poreklo " +
          "   FROM [dbo].[OtpremaKontejnera] as n1 " +
             " where n1.StatusOtpreme = 1  " +
           " and NacinOtpreme = 0 and Poreklo in ( " + pomModul + ") and VrstaKamiona in ( " + pomPoreklo + ") order by n1.ID desc ";

            /*
                        var select = " SELECT top 500 n1.[ID],RegBrKamiona, ImeVozaca, " +
                            " CONVERT(varchar,n1.DatumOtpreme,104)      + ' '      + SUBSTRING(CONVERT(varchar,n1.[DatumOtpreme],108),1,5) as ETA, " +
                                    " CASE WHEN n1.StatusOtpreme = 0 THEN '1-Najava' ELSE '2-Otpremljen' END as Status, " +
                                 " CONVERT(varchar,n1.VremeOdlaska,104)      + ' '      + SUBSTRING(CONVERT(varchar,n1.[VremeOdlaska],108),1,5) as ATA, " +
                       "  n1.[Datum] ,n1.[Korisnik], " +
                        " (  SELECT  STUFF((SELECT distinct   '/ ' + Cast(ts.BrojKontejnera as nvarchar(20)) " +
             "  FROM OtpremaKontejneraVozStavke ts where n1.ID = ts.IDNadredjenog " +
             " FOR XML PATH('')), 1, 1, ''  ) As Skupljen) " +
             " as Kontejner " +
                      "   FROM [dbo].[OtpremaKontejnera] as n1 " +
                         " where n1.StatusOtpreme = 1  " +
                       " and NacinOtpreme = 0 order by n1.ID desc ";
            */
            /*
             var select = "SELECT PrijemKontejneraVoz.[ID],Voz.BrVoza, Voz.Relacija, " +
                " CONVERT(varchar,PrijemKontejneraVoz.[DatumPrijema],104)      + ' '      + SUBSTRING(CONVERT(varchar,PrijemKontejneraVoz.[DatumPrijema],108),1,5) as ETA, " +
             " CASE WHEN PrijemKontejneraVoz.StatusPrijema = 0 THEN '1-Najava' ELSE '2-Prijem' END as Status, " +
            " CONVERT(varchar,PrijemKontejneraVoz.VremeDolaska,104)      + ' '      + SUBSTRING(CONVERT(varchar,PrijemKontejneraVoz.VremeDolaska,108),1,5) as ATA, " +
             "  [PrijemKontejneraVoz].Korisnik,[PrijemKontejneraVoz].Datum  " +
               " FROM [dbo].[PrijemKontejneraVoz] " +
              " inner join Voz on Voz.ID = PrijemKontejneraVoz.IdVoza " +
              " where PrijemKontejneraVoz.StatusPrijema = 0  order by PrijemKontejneraVoz.[ID] desc";
            */
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
            dataGridView1.Columns[1].HeaderText = "Reg br";
            dataGridView1.Columns[1].Width = 80;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Ime vozača";
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

        public void REfreshDataGridOtpremaOtpremljen()
        {
            var select = " SELECT top 500 n1.[ID],RegBrKamiona, ImeVozaca, " +
                " n1.DatumOtpreme as ETA, " +
                        " CASE WHEN n1.StatusOtpreme = 0 THEN '1-Najava' ELSE '2-Otpremljen' END as Status, " +
                     " n1.VremeOdlaska as ATA, " +
           "  n1.[Datum] ,n1.[Korisnik], " +
            " (  SELECT  STUFF((SELECT distinct   '/ ' + Cast(ts.BrojKontejnera as nvarchar(20)) " +
 "  FROM OtpremaKontejneraVozStavke ts where n1.ID = ts.IDNadredjenog " +
 " FOR XML PATH('')), 1, 1, ''  ) As Skupljen) " +
 " as Kontejner " +
          "   FROM [dbo].[OtpremaKontejnera] as n1 " +
             " where n1.StatusOtpreme = 1  " +
           " and NacinOtpreme = 0 order by n1.ID desc ";

/*
            var select = " SELECT top 500 n1.[ID],RegBrKamiona, ImeVozaca, " +
                " CONVERT(varchar,n1.DatumOtpreme,104)      + ' '      + SUBSTRING(CONVERT(varchar,n1.[DatumOtpreme],108),1,5) as ETA, " +
                        " CASE WHEN n1.StatusOtpreme = 0 THEN '1-Najava' ELSE '2-Otpremljen' END as Status, " +
                     " CONVERT(varchar,n1.VremeOdlaska,104)      + ' '      + SUBSTRING(CONVERT(varchar,n1.[VremeOdlaska],108),1,5) as ATA, " +
           "  n1.[Datum] ,n1.[Korisnik], " +
            " (  SELECT  STUFF((SELECT distinct   '/ ' + Cast(ts.BrojKontejnera as nvarchar(20)) " +
 "  FROM OtpremaKontejneraVozStavke ts where n1.ID = ts.IDNadredjenog " +
 " FOR XML PATH('')), 1, 1, ''  ) As Skupljen) " +
 " as Kontejner " +
          "   FROM [dbo].[OtpremaKontejnera] as n1 " +
             " where n1.StatusOtpreme = 1  " +
           " and NacinOtpreme = 0 order by n1.ID desc ";
*/
            /*
             var select = "SELECT PrijemKontejneraVoz.[ID],Voz.BrVoza, Voz.Relacija, " +
                " CONVERT(varchar,PrijemKontejneraVoz.[DatumPrijema],104)      + ' '      + SUBSTRING(CONVERT(varchar,PrijemKontejneraVoz.[DatumPrijema],108),1,5) as ETA, " +
             " CASE WHEN PrijemKontejneraVoz.StatusPrijema = 0 THEN '1-Najava' ELSE '2-Prijem' END as Status, " +
            " CONVERT(varchar,PrijemKontejneraVoz.VremeDolaska,104)      + ' '      + SUBSTRING(CONVERT(varchar,PrijemKontejneraVoz.VremeDolaska,108),1,5) as ATA, " +
             "  [PrijemKontejneraVoz].Korisnik,[PrijemKontejneraVoz].Datum  " +
               " FROM [dbo].[PrijemKontejneraVoz] " +
              " inner join Voz on Voz.ID = PrijemKontejneraVoz.IdVoza " +
              " where PrijemKontejneraVoz.StatusPrijema = 0  order by PrijemKontejneraVoz.[ID] desc";
            */
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
            dataGridView1.Columns[1].HeaderText = "Reg br";
            dataGridView1.Columns[1].Width = 80;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Ime vozača";
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
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            string Company = Saobracaj.Sifarnici.frmLogovanje.Firma;
            switch (Company)
            {
                case "Leget":
                    {
                        REfreshDataGridOtpremaOtpremljenLeget();
                        return;

                    }
                default:
                    {

                        REfreshDataGridOtpremaOtpremljen();
                        return;

                    }
                    break;
            }

           
        }

        private void frmPregledOtpremeKamionom_Load(object sender, EventArgs e)
        {

        }


        private void RefreshDataGridPoREgBr()
        {
          
                var select = "SELECT top 500 [ID],[DatumOtpreme],[StatusOtpreme],[IdVoza],[RegBrKamiona]," +
                    "[ImeVozaca],[VremeOdlaska] ,[NacinOtpreme] ,[Datum] ,[Korisnik],  " +
                    " (  SELECT  STUFF((SELECT distinct   '/ ' + Cast(ts.BrojKontejnera as nvarchar(20)) " +
 "  FROM OtpremaKontejneraVozStavke ts where n1.ID = ts.IDNadredjenog " +
 " FOR XML PATH('')), 1, 1, ''  ) As Skupljen) " +
 " as Kontejner " +
                    "FROM [dbo].[OtpremaKontejnera] as n1 where NacinOtpreme = 0 and n1.REgBrKamiona = '" + txtRegBrKamiona.Text + "' order by ID desc";
                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
                dataGridView1.Columns[1].HeaderText = "Datum otpreme";
                dataGridView1.Columns[1].Width = 150;

                DataGridViewColumn column3 = dataGridView1.Columns[2];
                dataGridView1.Columns[2].HeaderText = "Status otpreme";
                dataGridView1.Columns[2].Width = 50;

                DataGridViewColumn column4 = dataGridView1.Columns[3];
                dataGridView1.Columns[3].HeaderText = "Voz";
                dataGridView1.Columns[3].Width = 100;

                DataGridViewColumn column5 = dataGridView1.Columns[4];
                dataGridView1.Columns[4].HeaderText = "Reg Br Kamiona";
                dataGridView1.Columns[4].Width = 100;

                DataGridViewColumn column6 = dataGridView1.Columns[5];
                dataGridView1.Columns[5].HeaderText = "Ime vozača";
                dataGridView1.Columns[5].Width = 100;

                DataGridViewColumn column7 = dataGridView1.Columns[6];
                dataGridView1.Columns[6].HeaderText = "Otpremljen vozom";
                dataGridView1.Columns[6].Width = 100;

                DataGridViewColumn column8 = dataGridView1.Columns[7];
                dataGridView1.Columns[7].HeaderText = "Datum odlaska";
                dataGridView1.Columns[7].Width = 100;

                DataGridViewColumn column9 = dataGridView1.Columns[8];
                dataGridView1.Columns[8].HeaderText = "Datum";
                dataGridView1.Columns[8].Width = 100;

                DataGridViewColumn column10 = dataGridView1.Columns[9];
                dataGridView1.Columns[9].HeaderText = "Korisnik";
                dataGridView1.Columns[9].Width = 100;
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshDataGridPoREgBr();
        }


        private void RefreshDataGridPoKontejneru()
        {

            var select = "SELECT top 500 n1.[ID],n1.[DatumOtpreme],n1.[StatusOtpreme],n1.[IdVoza],n1.[RegBrKamiona]," +
                "n1.[ImeVozaca],n1.[VremeOdlaska] ,n1.[NacinOtpreme] ,n1.[Datum] ,n1.[Korisnik],  " +
                " (  SELECT  STUFF((SELECT distinct   '/ ' + Cast(ts.BrojKontejnera as nvarchar(20)) " +
"  FROM OtpremaKontejneraVozStavke ts where n1.ID = ts.IDNadredjenog " +
" FOR XML PATH('')), 1, 1, ''  ) As Skupljen) " +
" as Kontejner " +
                "FROM [dbo].[OtpremaKontejnera] as n1 inner join OtpremaKontejneraVozStavke os on n1.ID = os.IDNadredjenog where n1.NacinOtpreme = 0 and os.BrojKontejnera = '" + txtBrojKontejnera.Text + "' order by ID desc";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
            dataGridView1.Columns[1].HeaderText = "Datum otpreme";
            dataGridView1.Columns[1].Width = 150;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Status otpreme";
            dataGridView1.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Voz";
            dataGridView1.Columns[3].Width = 100;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Reg Br Kamiona";
            dataGridView1.Columns[4].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Ime vozača";
            dataGridView1.Columns[5].Width = 100;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Otpremljen vozom";
            dataGridView1.Columns[6].Width = 100;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Datum odlaska";
            dataGridView1.Columns[7].Width = 100;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Datum";
            dataGridView1.Columns[8].Width = 100;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Korisnik";
            dataGridView1.Columns[9].Width = 100;

        }

        private void RefreshDataGridPoBukingu()
        {

            var select = "SELECT top 500 n1.[ID],n1.[DatumOtpreme],n1.[StatusOtpreme],n1.[IdVoza],n1.[RegBrKamiona]," +
                "n1.[ImeVozaca],n1.[VremeOdlaska] ,n1.[NacinOtpreme] ,n1.[Datum] ,n1.[Korisnik],  " +
                " (  SELECT  STUFF((SELECT distinct   '/ ' + Cast(ts.BrojKontejnera as nvarchar(20)) " +
"  FROM OtpremaKontejneraVozStavke ts where n1.ID = ts.IDNadredjenog " +
" FOR XML PATH('')), 1, 1, ''  ) As Skupljen) " +
" as Kontejner " +
                "FROM [dbo].[OtpremaKontejnera] as n1 inner join OtpremaKontejneraVozStavke os on n1.ID = os.IDNadredjenog where n1.NacinOtpreme = 0 and os.Buking = '" + txtBukingBrodar.Text + "' order by ID desc";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
            dataGridView1.Columns[1].HeaderText = "Datum otpreme";
            dataGridView1.Columns[1].Width = 150;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Status otpreme";
            dataGridView1.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Voz";
            dataGridView1.Columns[3].Width = 100;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Reg Br Kamiona";
            dataGridView1.Columns[4].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Ime vozača";
            dataGridView1.Columns[5].Width = 100;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Otpremljen vozom";
            dataGridView1.Columns[6].Width = 100;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Datum odlaska";
            dataGridView1.Columns[7].Width = 100;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Datum";
            dataGridView1.Columns[8].Width = 100;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Korisnik";
            dataGridView1.Columns[9].Width = 100;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefreshDataGridPoBukingu();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshDataGridPoKontejneru();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            string Company = Saobracaj.Sifarnici.frmLogovanje.Firma;
            switch (Company)
            {
                case "Leget":
                    {
                        Saobracaj.Dokumenta.frmOtpremaKontejneraUvozKamion ter2 = new Saobracaj.Dokumenta.frmOtpremaKontejneraUvozKamion(Convert.ToInt32(txtSifra.Text), KorisnikCene);
                        ter2.Show();
                        return;
                    }
                default:
                    {

                        Saobracaj.Dokumeta.frmOtpremaKontejnera ter3 = new Saobracaj.Dokumeta.frmOtpremaKontejnera(Convert.ToInt32(txtSifra.Text), KorisnikCene);
                        ter3.Show();
                        return;

                    }
                    break;
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Saobracaj.Dokumeta.frmOtpremaKontejnera ter3 = new Saobracaj.Dokumeta.frmOtpremaKontejnera(Convert.ToInt32(txtSifra.Text), KorisnikCene);
            ter3.Show();
        }
    }
}



