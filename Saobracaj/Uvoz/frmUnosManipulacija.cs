using Saobracaj.Sifarnici;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Uvoz
{
    public partial class frmUnosManipulacija : Syncfusion.Windows.Forms.Office2010Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        int pIDPlana = 0;
        int pID = 0;
        int pNalogodavac1 = 0;
        int pNalogodavac2 = 0;
        int pNalogodavac3 = 0;
        int pUvoznik = 0;
        int pomOrgJed = 0;
        int Usao = 0;
        string KorisnikTekuci = "";
        public frmUnosManipulacija()
        {
            InitializeComponent();
            FillDG6(1);
            Usao = 0;
        }

        public frmUnosManipulacija(int IDPlana, int ID, int Nalogodavac1, int Nalogodavac2, int Nalogodavac3, int Uvoznik, string Korisnik)
        {
            InitializeComponent();
            pIDPlana = IDPlana;
            pID = ID;
            txtID.Text = pID.ToString();
            pNalogodavac1 = Nalogodavac1;
            pNalogodavac2 = Nalogodavac2;
            pNalogodavac3 = Nalogodavac3;
            pUvoznik = Uvoznik;
            txtNadredjeni.Text = pIDPlana.ToString();
            FillDG6(1);
            FillDG8();
            KorisnikTekuci = Korisnik;
            Usao = 0;


        }

        private void button11_Click(object sender, EventArgs e)
        {
            FillDG6(1);
        }


        private void FillDG6(int TipUsluge)
        {
            if (TipUsluge == 1)
            {
                var select = "SELECT VrstaManipulacije.[ID]      ,VrstaManipulacije.[Naziv] ,  " +
   " VrstaManipulacije.[JM] " +
  " ,VrstaManipulacije.[TipManipulacije]      ,VrstaManipulacije.[OrgJed]      ,OrganizacioneJedinice.Naziv as OJ " +
  " ,VrstaManipulacije.[Cena] ,'',0, '/',  VrstaManipulacije.[Datum] ,VrstaManipulacije.[Korisnik] FROM[VrstaManipulacije] " +
  "  inner join OrganizacioneJedinice on VrstaManipulacije.OrgJed = OrganizacioneJedinice.ID where Administrativna = 0  order by VrstaManipulacije.[Naziv] ";
                SqlConnection conn = new SqlConnection(connection);
                var da = new SqlDataAdapter(select, conn);
                var ds = new DataSet();
                da.Fill(ds);
                dataGridView6.ReadOnly = false;
                dataGridView6.DataSource = ds.Tables[0];


                dataGridView6.BorderStyle = BorderStyle.None;
                dataGridView6.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dataGridView6.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dataGridView6.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dataGridView6.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dataGridView6.BackgroundColor = Color.White;

                dataGridView6.EnableHeadersVisualStyles = false;
                dataGridView6.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dataGridView6.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dataGridView6.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
                DataGridViewColumn column = dataGridView6.Columns[0];
                dataGridView6.Columns[0].HeaderText = "ID";
                dataGridView6.Columns[0].Width = 20;

                DataGridViewColumn column2 = dataGridView6.Columns[1];
                dataGridView6.Columns[1].HeaderText = "Naziv";
                dataGridView6.Columns[1].Width = 180;

                DataGridViewColumn column3 = dataGridView6.Columns[2];
                dataGridView6.Columns[2].HeaderText = "JM";
                dataGridView6.Columns[2].Width = 50;

                DataGridViewColumn column4 = dataGridView6.Columns[3];
                dataGridView6.Columns[3].HeaderText = "Tip manipulacije";
                dataGridView6.Columns[3].Width = 50;
                dataGridView6.Columns[3].Visible = false;


                DataGridViewColumn column5 = dataGridView6.Columns[4];
                dataGridView6.Columns[4].HeaderText = "OJ";
                dataGridView6.Columns[4].Width = 30;

                DataGridViewColumn column6 = dataGridView6.Columns[5];
                dataGridView6.Columns[5].HeaderText = "OJ naziv";
                dataGridView6.Columns[5].Width = 100;

                DataGridViewColumn column7 = dataGridView6.Columns[6];
                dataGridView6.Columns[6].HeaderText = "Cena";
                dataGridView6.Columns[6].Width = 50;

                DataGridViewColumn column8 = dataGridView6.Columns[7];
                dataGridView6.Columns[7].HeaderText = "Pokret";
                dataGridView6.Columns[7].Width = 150;

                DataGridViewColumn column9 = dataGridView6.Columns[8];
                dataGridView6.Columns[8].HeaderText = "StatusID";
                dataGridView6.Columns[8].Width = 30;

                DataGridViewColumn column10 = dataGridView6.Columns[9];
                dataGridView6.Columns[9].HeaderText = "Status";
                dataGridView6.Columns[9].Width = 130;
            }
            else
            {
                var select = "SELECT VrstaManipulacije.[ID]      ,VrstaManipulacije.[Naziv] ,  " +
   " VrstaManipulacije.[JM] " +
  " ,VrstaManipulacije.[TipManipulacije]      ,VrstaManipulacije.[OrgJed]      ,OrganizacioneJedinice.Naziv as OJ " +
  " ,VrstaManipulacije.[Cena] ,'',0, '/',  VrstaManipulacije.[Datum] ,VrstaManipulacije.[Korisnik] FROM[VrstaManipulacije] " +
  "  inner join OrganizacioneJedinice on VrstaManipulacije.OrgJed = OrganizacioneJedinice.ID where Administrativna = 1  order by VrstaManipulacije.[Naziv] ";
                SqlConnection conn = new SqlConnection(connection);
                var da = new SqlDataAdapter(select, conn);
                var ds = new DataSet();
                da.Fill(ds);
                dataGridView6.ReadOnly = false;
                dataGridView6.DataSource = ds.Tables[0];


                dataGridView6.BorderStyle = BorderStyle.None;
                dataGridView6.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dataGridView6.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dataGridView6.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dataGridView6.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dataGridView6.BackgroundColor = Color.White;

                dataGridView6.EnableHeadersVisualStyles = false;
                dataGridView6.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dataGridView6.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dataGridView6.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                DataGridViewColumn column = dataGridView6.Columns[0];
                dataGridView6.Columns[0].HeaderText = "ID";
                dataGridView6.Columns[0].Width = 20;

                DataGridViewColumn column2 = dataGridView6.Columns[1];
                dataGridView6.Columns[1].HeaderText = "Naziv";
                dataGridView6.Columns[1].Width = 180;

                DataGridViewColumn column3 = dataGridView6.Columns[2];
                dataGridView6.Columns[2].HeaderText = "JM";
                dataGridView6.Columns[2].Width = 50;

                DataGridViewColumn column4 = dataGridView6.Columns[3];
                dataGridView6.Columns[3].HeaderText = "Tip manipulacije";
                dataGridView6.Columns[3].Width = 50;
                dataGridView6.Columns[3].Visible = false;


                DataGridViewColumn column5 = dataGridView6.Columns[4];
                dataGridView6.Columns[4].HeaderText = "OJ";
                dataGridView6.Columns[4].Width = 30;

                DataGridViewColumn column6 = dataGridView6.Columns[5];
                dataGridView6.Columns[5].HeaderText = "OJ naziv";
                dataGridView6.Columns[5].Width = 100;

                DataGridViewColumn column7 = dataGridView6.Columns[6];
                dataGridView6.Columns[6].HeaderText = "Cena";
                dataGridView6.Columns[6].Width = 50;

                DataGridViewColumn column8 = dataGridView6.Columns[7];
                dataGridView6.Columns[7].HeaderText = "Pokret";
                dataGridView6.Columns[7].Width = 150;

                DataGridViewColumn column9 = dataGridView6.Columns[8];
                dataGridView6.Columns[8].HeaderText = "StatusID";
                dataGridView6.Columns[8].Width = 30;

                DataGridViewColumn column10 = dataGridView6.Columns[9];
                dataGridView6.Columns[9].HeaderText = "Status";
                dataGridView6.Columns[9].Width = 130;
            }
        }

        private void FillDG6Scenario()
        {

            var select = " SELECT VrstaManipulacije.[ID]      ,VrstaManipulacije.[Naziv] ,  " +
 " VrstaManipulacije.[JM] " +
" ,VrstaManipulacije.[TipManipulacije]      ,VrstaManipulacije.[OrgJed]      ,OrganizacioneJedinice.Naziv as OJ " +
" ,VrstaManipulacije.[Cena] ,Scenario.Pokret,Scenario.StatusKontejnera,KontejnerStatus.Naziv, Scenario.Forma, " +
" VrstaManipulacije.[Datum] ,VrstaManipulacije.[Korisnik] FROM [VrstaManipulacije] " +
" inner join Scenario on Scenario.Usluga = VrstaManipulacije.ID " +
" inner join kontejnerStatus on KontejnerStatus.ID = Scenario.statusKOntejnera " +
"  inner join OrganizacioneJedinice on VrstaManipulacije.OrgJed = OrganizacioneJedinice.ID where Scenario.ID = " + Convert.ToInt32(cboScenario.SelectedValue) + " order by Scenario.RB asc ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView6.ReadOnly = false;
            dataGridView6.DataSource = ds.Tables[0];


            dataGridView6.BorderStyle = BorderStyle.None;
            dataGridView6.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView6.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView6.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView6.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView6.BackgroundColor = Color.White;

            dataGridView6.EnableHeadersVisualStyles = false;
            dataGridView6.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView6.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView6.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView6.Columns[0];
            dataGridView6.Columns[0].HeaderText = "ID";
            dataGridView6.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView6.Columns[1];
            dataGridView6.Columns[1].HeaderText = "Naziv";
            dataGridView6.Columns[1].Width = 180;

            DataGridViewColumn column3 = dataGridView6.Columns[2];
            dataGridView6.Columns[2].HeaderText = "JM";
            dataGridView6.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView6.Columns[3];
            dataGridView6.Columns[3].HeaderText = "Tip manipulacije";
            dataGridView6.Columns[3].Width = 50;
            dataGridView6.Columns[3].Visible = false;


            DataGridViewColumn column5 = dataGridView6.Columns[4];
            dataGridView6.Columns[4].HeaderText = "OJ";
            dataGridView6.Columns[4].Width = 30;

            DataGridViewColumn column6 = dataGridView6.Columns[5];
            dataGridView6.Columns[5].HeaderText = "OJ naziv";
            dataGridView6.Columns[5].Width = 100;

            DataGridViewColumn column7 = dataGridView6.Columns[6];
            dataGridView6.Columns[6].HeaderText = "Cena";
            dataGridView6.Columns[6].Width = 50;

            DataGridViewColumn column8 = dataGridView6.Columns[7];
            dataGridView6.Columns[7].HeaderText = "Pokret";
            dataGridView6.Columns[7].Width = 150;

            DataGridViewColumn column9 = dataGridView6.Columns[8];
            dataGridView6.Columns[8].HeaderText = "StatusID";
            dataGridView6.Columns[8].Width = 30;

            DataGridViewColumn column10 = dataGridView6.Columns[9];
            dataGridView6.Columns[9].HeaderText = "Status";
            dataGridView6.Columns[9].Width = 130;

            DataGridViewColumn column11 = dataGridView6.Columns[10];
            dataGridView6.Columns[10].HeaderText = "Otvara formu";
            dataGridView6.Columns[10].Width = 100;

        }
        private void FillDN1()
        {
            //Cenovnik po Nalogodavcu broj 1
            var select = "select Cene.ID, VrstaManipulacije.Naziv, Cene.Cena, VrstaManipulacije.ID, 1 as Kolicina, VrstaManipulacije.OrgJed from Cene " +
            " inner join VrstaManipulacije on VrstaManipulacije.ID = Cene.VrstaManipulacije " +
              "inner join OrganizacioneJedinice on VrstaManipulacije.OrgJed = OrganizacioneJedinice.ID " +
            " where  Uvoznik = " + Convert.ToInt32(cboUvoznik.SelectedValue) + " and Cene.Komitent = " + Convert.ToInt32(cboNalogodavac1.SelectedValue) + "  order by VrstaManipulacije.Naziv ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView6.ReadOnly = false;
            dataGridView6.DataSource = ds.Tables[0];


            dataGridView6.BorderStyle = BorderStyle.None;
            dataGridView6.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView6.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView6.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView6.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView6.BackgroundColor = Color.White;

            dataGridView6.EnableHeadersVisualStyles = false;
            dataGridView6.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView6.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView6.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView6.Columns[0];
            dataGridView6.Columns[0].HeaderText = "ID";
            dataGridView6.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView6.Columns[1];
            dataGridView6.Columns[1].HeaderText = "Man";
            dataGridView6.Columns[1].Width = 120;

            DataGridViewColumn column3 = dataGridView6.Columns[2];
            dataGridView6.Columns[2].HeaderText = "Cena";
            dataGridView6.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView6.Columns[3];
            dataGridView6.Columns[3].HeaderText = "IDVM";
            dataGridView6.Columns[3].Width = 50;
            dataGridView6.Columns[3].Visible = false;

            DataGridViewColumn column5 = dataGridView6.Columns[4];
            dataGridView6.Columns[4].HeaderText = "Količina";
            dataGridView6.Columns[4].Width = 50;

            DataGridViewColumn column6 = dataGridView6.Columns[5];
            dataGridView6.Columns[5].HeaderText = "OrgJed";
            dataGridView6.Columns[5].Width = 50;
        }
        private void FillDGN2()
        {
            //Cenovnik po Nalogodavcu broj 2
            var select = "select Cene.ID, VrstaManipulacije.Naziv, Cene.Cena, VrstaManipulacije.ID, 1 as Kolicina, VrstaManipulacije.OrgJed  from Cene " +
            " inner join VrstaManipulacije on VrstaManipulacije.ID = Cene.VrstaManipulacije " +
             "inner join OrganizacioneJedinice on VrstaManipulacije.OrgJed = OrganizacioneJedinice.ID " +
           " where  Uvoznik = " + Convert.ToInt32(cboUvoznik.SelectedValue) + " and Cene.Komitent = " + Convert.ToInt32(cboNalogodavac2.SelectedValue) + "  order by VrstaManipulacije.Naziv ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView6.ReadOnly = false;
            dataGridView6.DataSource = ds.Tables[0];


            dataGridView6.BorderStyle = BorderStyle.None;
            dataGridView6.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView6.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView6.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView6.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView6.BackgroundColor = Color.White;

            dataGridView6.EnableHeadersVisualStyles = false;
            dataGridView6.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView6.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView6.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView6.Columns[0];
            dataGridView6.Columns[0].HeaderText = "ID";
            dataGridView6.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView6.Columns[1];
            dataGridView6.Columns[1].HeaderText = "Man";
            dataGridView6.Columns[1].Width = 120;

            DataGridViewColumn column3 = dataGridView6.Columns[2];
            dataGridView6.Columns[2].HeaderText = "Cena";
            dataGridView6.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView6.Columns[3];
            dataGridView6.Columns[3].HeaderText = "IDVM";
            dataGridView6.Columns[3].Width = 50;
            dataGridView6.Columns[3].Visible = false;


            DataGridViewColumn column5 = dataGridView6.Columns[4];
            dataGridView6.Columns[4].HeaderText = "Količina";
            dataGridView6.Columns[4].Width = 50;

            DataGridViewColumn column6 = dataGridView6.Columns[5];
            dataGridView6.Columns[5].HeaderText = "OrgJed";
            dataGridView6.Columns[5].Width = 50;
        }
        private void FillDGN3()
        {
            //Cenovnik po Nalogodavcu broj 2
            var select = "select Cene.ID, VrstaManipulacije.Naziv, Cene.Cena, VrstaManipulacije.ID, 1 as Kolicina, VrstaManipulacije.OrgJed  from Cene " +
            " inner join VrstaManipulacije on VrstaManipulacije.ID = Cene.VrstaManipulacije " +
            " inner join OrganizacioneJedinice on VrstaManipulacije.OrgJed = OrganizacioneJedinice.ID " +
           " where  Uvoznik = " + Convert.ToInt32(cboUvoznik.SelectedValue) + " and Cene.Komitent = " + Convert.ToInt32(cboNalogodavac3.SelectedValue) + "  order by VrstaManipulacije.Naziv ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView6.ReadOnly = false;
            dataGridView6.DataSource = ds.Tables[0];


            dataGridView6.BorderStyle = BorderStyle.None;
            dataGridView6.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView6.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView6.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView6.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView6.BackgroundColor = Color.White;

            dataGridView6.EnableHeadersVisualStyles = false;
            dataGridView6.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView6.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView6.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView6.Columns[0];
            dataGridView6.Columns[0].HeaderText = "ID";
            dataGridView6.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView6.Columns[1];
            dataGridView6.Columns[1].HeaderText = "Man";
            dataGridView6.Columns[1].Width = 120;

            DataGridViewColumn column3 = dataGridView6.Columns[2];
            dataGridView6.Columns[2].HeaderText = "Cena";
            dataGridView6.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView6.Columns[3];
            dataGridView6.Columns[3].HeaderText = "IDVM";
            dataGridView6.Columns[3].Width = 50;
            dataGridView6.Columns[3].Visible = false;

            DataGridViewColumn column5 = dataGridView6.Columns[4];
            dataGridView6.Columns[4].HeaderText = "Količina";
            dataGridView6.Columns[4].Width = 50;

            DataGridViewColumn column6 = dataGridView6.Columns[5];
            dataGridView6.Columns[5].HeaderText = "OrgJed";
            dataGridView6.Columns[5].Width = 50;
        }


        private void FillDN1BU()
        {
            //Cenovnik po Nalogodavcu broj 1
            var select = "select Cene.ID, VrstaManipulacije.Naziv, Cene.Cena, VrstaManipulacije.ID, 1 as Kolicina, VrstaManipulacije.OrgJed from Cene " +
            " inner join VrstaManipulacije on VrstaManipulacije.ID = Cene.VrstaManipulacije " +
              "inner join OrganizacioneJedinice on VrstaManipulacije.OrgJed = OrganizacioneJedinice.ID " +
            " where  Uvoznik = " + Convert.ToInt32(0) + " and Cene.Komitent = " + Convert.ToInt32(cboNalogodavac1.SelectedValue) + "  order by VrstaManipulacije.Naziv ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView6.ReadOnly = false;
            dataGridView6.DataSource = ds.Tables[0];


            dataGridView6.BorderStyle = BorderStyle.None;
            dataGridView6.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView6.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView6.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView6.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView6.BackgroundColor = Color.White;

            dataGridView6.EnableHeadersVisualStyles = false;
            dataGridView6.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView6.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView6.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView6.Columns[0];
            dataGridView6.Columns[0].HeaderText = "ID";
            dataGridView6.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView6.Columns[1];
            dataGridView6.Columns[1].HeaderText = "Man";
            dataGridView6.Columns[1].Width = 120;

            DataGridViewColumn column3 = dataGridView6.Columns[2];
            dataGridView6.Columns[2].HeaderText = "Cena";
            dataGridView6.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView6.Columns[3];
            dataGridView6.Columns[3].HeaderText = "IDVM";
            dataGridView6.Columns[3].Width = 50;
            dataGridView6.Columns[3].Visible = false;

            DataGridViewColumn column5 = dataGridView6.Columns[4];
            dataGridView6.Columns[4].HeaderText = "Količina";
            dataGridView6.Columns[4].Width = 50;

            DataGridViewColumn column6 = dataGridView6.Columns[5];
            dataGridView6.Columns[5].HeaderText = "OrgJed";
            dataGridView6.Columns[5].Width = 50;
        }
        private void FillDGN2BU()
        {
            //Cenovnik po Nalogodavcu broj 2
            var select = "select Cene.ID, VrstaManipulacije.Naziv, Cene.Cena, VrstaManipulacije.ID, 1 as Kolicina, VrstaManipulacije.OrgJed  from Cene " +
            " inner join VrstaManipulacije on VrstaManipulacije.ID = Cene.VrstaManipulacije " +
             "inner join OrganizacioneJedinice on VrstaManipulacije.OrgJed = OrganizacioneJedinice.ID " +
           " where  Uvoznik = " + Convert.ToInt32(0) + " and Cene.Komitent = " + Convert.ToInt32(cboNalogodavac2.SelectedValue) + "  order by VrstaManipulacije.Naziv ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView6.ReadOnly = false;
            dataGridView6.DataSource = ds.Tables[0];


            dataGridView6.BorderStyle = BorderStyle.None;
            dataGridView6.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView6.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView6.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView6.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView6.BackgroundColor = Color.White;

            dataGridView6.EnableHeadersVisualStyles = false;
            dataGridView6.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView6.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView6.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView6.Columns[0];
            dataGridView6.Columns[0].HeaderText = "ID";
            dataGridView6.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView6.Columns[1];
            dataGridView6.Columns[1].HeaderText = "Man";
            dataGridView6.Columns[1].Width = 120;

            DataGridViewColumn column3 = dataGridView6.Columns[2];
            dataGridView6.Columns[2].HeaderText = "Cena";
            dataGridView6.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView6.Columns[3];
            dataGridView6.Columns[3].HeaderText = "IDVM";
            dataGridView6.Columns[3].Width = 50;
            dataGridView6.Columns[3].Visible = false;


            DataGridViewColumn column5 = dataGridView6.Columns[4];
            dataGridView6.Columns[4].HeaderText = "Količina";
            dataGridView6.Columns[4].Width = 50;

            DataGridViewColumn column6 = dataGridView6.Columns[5];
            dataGridView6.Columns[5].HeaderText = "OrgJed";
            dataGridView6.Columns[5].Width = 50;
        }
        private void FillDGN3BU()
        {
            //Cenovnik po Nalogodavcu broj 2
            var select = "select Cene.ID, VrstaManipulacije.Naziv, Cene.Cena, VrstaManipulacije.ID, 1 as Kolicina, VrstaManipulacije.OrgJed  from Cene " +
            " inner join VrstaManipulacije on VrstaManipulacije.ID = Cene.VrstaManipulacije " +
            " inner join OrganizacioneJedinice on VrstaManipulacije.OrgJed = OrganizacioneJedinice.ID " +
           " where  Uvoznik = " + Convert.ToInt32(0) + " and Cene.Komitent = " + Convert.ToInt32(cboNalogodavac3.SelectedValue) + "  order by VrstaManipulacije.Naziv ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView6.ReadOnly = false;
            dataGridView6.DataSource = ds.Tables[0];


            dataGridView6.BorderStyle = BorderStyle.None;
            dataGridView6.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView6.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView6.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView6.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView6.BackgroundColor = Color.White;

            dataGridView6.EnableHeadersVisualStyles = false;
            dataGridView6.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView6.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView6.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView6.Columns[0];
            dataGridView6.Columns[0].HeaderText = "ID";
            dataGridView6.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView6.Columns[1];
            dataGridView6.Columns[1].HeaderText = "Man";
            dataGridView6.Columns[1].Width = 120;

            DataGridViewColumn column3 = dataGridView6.Columns[2];
            dataGridView6.Columns[2].HeaderText = "Cena";
            dataGridView6.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView6.Columns[3];
            dataGridView6.Columns[3].HeaderText = "IDVM";
            dataGridView6.Columns[3].Width = 50;
            dataGridView6.Columns[3].Visible = false;

            DataGridViewColumn column5 = dataGridView6.Columns[4];
            dataGridView6.Columns[4].HeaderText = "Količina";
            dataGridView6.Columns[4].Width = 50;

            DataGridViewColumn column6 = dataGridView6.Columns[5];
            dataGridView6.Columns[5].HeaderText = "OrgJed";
            dataGridView6.Columns[5].Width = 50;
        }




        private void button13_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Pretrazuje cenovnik sa parametrima Nalogodavci 1 i Izvoznik");

            // FillDG7();
            FillDN1();
        }

        private void UbaciStavkuUsluge(int ID, int Manipulacija, double Cena, double Kolicina, int OrgJed, int pomPlatilac, string pomPokret, int pomStatusKontejnera, string pomForma)
        {
            if (txtNadredjeni.Text != "0")
            {
                InsertUvozKonacna uvK = new InsertUvozKonacna();
                uvK.InsUbaciUsluguKonacna(ID, Manipulacija, Cena, Kolicina, OrgJed, pomPlatilac, 0, pomPokret, pomStatusKontejnera, KorisnikTekuci, pomForma);
                FillDG8();
            }
            else
            {
                InsertUvozKonacna uvK = new InsertUvozKonacna();
                uvK.InsUbaciUslugu(ID, Manipulacija, Cena, Kolicina, OrgJed, pomPlatilac, 0, pomPokret, pomStatusKontejnera, KorisnikTekuci, pomForma);
                FillDG8();

            }


        }

        private void FillDG8()
        {
            var select = "";


            if (txtNadredjeni.Text == "0")
            {
                select = "select  UvozVrstaManipulacije.ID as ID, UvozVrstaManipulacije.IDNadredjena as KontejnerID, Uvoz.BrojKontejnera, " +
" UvozVrstaManipulacije.Kolicina,  VrstaManipulacije.ID as ManipulacijaID,VrstaManipulacije.Naziv as ManipulacijaNaziv, " +
" UvozVrstaManipulacije.Cena,OrganizacioneJedinice.ID,   OrganizacioneJedinice.Naziv as OrganizacionaJedinica,  " +
" Partnerji.PaSifra as NalogodavacID,PArtnerji.PaNaziv as Platilac, SaPDV, Pokret, KontejnerStatus.Naziv, Forma " +
" from UvozVrstaManipulacije " +
" Inner    join VrstaManipulacije on VrstaManipulacije.ID = UvozVrstaManipulacije.IDVrstaManipulacije " +
" inner " +
" join PArtnerji on UvozVrstaManipulacije.Platilac = PArtnerji.PaSifra " +
" inner " +
" join OrganizacioneJedinice on OrganizacioneJedinice.ID = UvozVrstaManipulacije.OrgJed " +
" inner " +
" join Uvoz on UvozVrstaManipulacije.IDNadredjena = Uvoz.ID" +
" left join KontejnerStatus on  UvozVrstaManipulacije.StatusKontejnera = KontejnerStatus.ID " +
" where Uvoz.ID = " + Convert.ToInt32(txtID.Text) + " order by UvozVrstaManipulacije.ID";


            }
            else
            {
                select = "select  UvozKonacnaVrstaManipulacije.ID as ID, UvozKonacnaVrstaManipulacije.IDNadredjena as KontejnerID, UvozKonacna.BrojKontejnera, " +
" UvozKonacnaVrstaManipulacije.Kolicina,  VrstaManipulacije.ID as ManipulacijaID,VrstaManipulacije.Naziv as ManipulacijaNaziv, " +
" UvozKonacnaVrstaManipulacije.Cena,OrganizacioneJedinice.ID,   OrganizacioneJedinice.Naziv as OrganizacionaJedinica,  " +
" Partnerji.PaSifra as NalogodavacID,PArtnerji.PaNaziv as Platilac, SaPDV, UvozKonacnaVrstaManipulacije.Pokret, KontejnerStatus.Naziv, Forma " +
" from UvozKonacnaVrstaManipulacije " +
" Inner    join VrstaManipulacije on VrstaManipulacije.ID = UvozKonacnaVrstaManipulacije.IDVrstaManipulacije" +
" inner " +
" join PArtnerji on UvozKonacnaVrstaManipulacije.Platilac = PArtnerji.PaSifra " +
" inner " +
" join OrganizacioneJedinice on OrganizacioneJedinice.ID = UvozKonacnaVrstaManipulacije.OrgJed " +
" inner " +
" join UvozKonacna on UvozKonacnaVrstaManipulacije.IDNadredjena = UvozKonacna.ID  " +
" inner " +
" join KontejnerStatus on UvozKonacnaVrstaManipulacije.StatusKOntejnera = KontejnerStatus.ID  " +
"where UvozKonacna.IDNadredjeni = " + Convert.ToInt32(txtNadredjeni.Text) + " order by UvozKonacnaVrstaManipulacije.ID";
            }

            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView7.ReadOnly = false;
            dataGridView7.DataSource = ds.Tables[0];


            dataGridView7.BorderStyle = BorderStyle.None;
            dataGridView7.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView7.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView7.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView7.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView7.BackgroundColor = Color.White;

            dataGridView7.EnableHeadersVisualStyles = false;
            dataGridView7.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView7.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView7.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView7.Columns[0];
            dataGridView7.Columns[0].HeaderText = "ID";
            dataGridView7.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView7.Columns[1];
            dataGridView7.Columns[1].HeaderText = "USL ID";
            dataGridView7.Columns[1].Width = 30;

            DataGridViewColumn column3 = dataGridView7.Columns[2];
            dataGridView7.Columns[2].HeaderText = "Kontejner";
            dataGridView7.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView7.Columns[3];
            dataGridView7.Columns[3].HeaderText = "Kol";
            dataGridView7.Columns[3].Width = 50;

            DataGridViewColumn column5 = dataGridView7.Columns[4];
            dataGridView7.Columns[4].HeaderText = "VRU";
            dataGridView7.Columns[4].Width = 30;

            DataGridViewColumn column6 = dataGridView7.Columns[5];
            dataGridView7.Columns[5].HeaderText = "Usluga";
            dataGridView7.Columns[5].Width = 250;

            DataGridViewColumn column7 = dataGridView7.Columns[6];
            dataGridView7.Columns[6].HeaderText = "Cena";
            dataGridView7.Columns[6].Width = 80;


            DataGridViewColumn column8 = dataGridView7.Columns[7];
            dataGridView7.Columns[7].HeaderText = "OJID";
            dataGridView7.Columns[7].Width = 20;

            DataGridViewColumn column9 = dataGridView7.Columns[8];
            dataGridView7.Columns[8].HeaderText = "Org Jed";
            dataGridView7.Columns[8].Width = 120;

            DataGridViewColumn column10 = dataGridView7.Columns[9];
            dataGridView7.Columns[9].HeaderText = "PLID";
            dataGridView7.Columns[9].Width = 40;

            DataGridViewColumn column11 = dataGridView7.Columns[10];
            dataGridView7.Columns[10].HeaderText = "Platilac";
            dataGridView7.Columns[10].Width = 140;

            //dataGridView7.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView7.SelectAll();
            /*
                        DataGridViewColumn column12 = dataGridView7.Columns[11];
                        dataGridView7.Columns[11].HeaderText = "SaPDV";
                        dataGridView7.Columns[11].Width = 140;
            */
        }

        int PretraziPoNalogodavciIIzvozniku(int PomManipulacija, int Nalogodavac, int Uvoznik)
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            int tmp = 0;
            con.Open();

            SqlCommand cmd = new SqlCommand("select count(*) as Broj from Cene where Komitent = " + Nalogodavac + " and Uvoznik = " + Uvoznik + " and ID = " + PomManipulacija, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {


                tmp = Convert.ToInt32(dr["Broj"].ToString());

            }

            con.Close();
            if (tmp > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }

        int PretraziPoNalogodavci(int PomManipulacija, int Nalogodavac, int Uvoznik)
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            int tmp = 0;
            con.Open();

            SqlCommand cmd = new SqlCommand("select count(*) as Broj from Cene where Komitent = " + Nalogodavac + " and Uvoznik = " + Uvoznik + " and ID = " + PomManipulacija, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {


                tmp = Convert.ToInt32(dr["Broj"].ToString());

            }

            con.Close();
            if (tmp > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }
        private void button12_Click(object sender, EventArgs e)
        {
            int CenaNadjenaTip1 = 0;
            int CenaNadjenaTip2 = 0;
            int pomID = 0;
            int pomManupulacija = 0;
            double pomCena = 0;
            double pomkolicina = 1;
            int pomPlatilac = 0;
            string pomPokret = "";
            int pomStatusKontejnera = 0;
            string pomForma = "";
            try
            {
                foreach (DataGridViewRow row in dataGridView6.Rows)
                {
                    if (row.Selected)
                    {
                        pomManupulacija = Convert.ToInt32(row.Cells[0].Value.ToString());
                        CenaNadjenaTip1 = PretraziPoNalogodavciIIzvozniku(pomManupulacija, Convert.ToInt32(cboNalogodavac1.SelectedValue), Convert.ToInt32(cboUvoznik.SelectedValue));
                        CenaNadjenaTip2 = PretraziPoNalogodavci(pomManupulacija, Convert.ToInt32(cboNalogodavac1.SelectedValue), Convert.ToInt32(cboUvoznik.SelectedValue));
                        pomPokret = row.Cells[7].Value.ToString();
                        pomStatusKontejnera = Convert.ToInt32(row.Cells[8].Value.ToString());
                        pomForma = row.Cells[10].Value.ToString();
                        if (CenaNadjenaTip1 == 1)
                        {
                            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                            SqlConnection con = new SqlConnection(s_connection);

                            con.Open();

                            SqlCommand cmd = new SqlCommand("select ID, Cena, OrgJed from Cene where VrstaManipulacije = " + pomManupulacija + " Uvoznik = " + cboUvoznik.SelectedValue + " and  Komitent = " + Convert.ToInt32(cboNalogodavac1.SelectedValue) + "", con);
                            SqlDataReader dr = cmd.ExecuteReader();

                            while (dr.Read())
                            {

                                pomCena = Convert.ToDouble(dr["Cena"].ToString());
                                pomkolicina = 1;


                            }

                            con.Close();
                            pomOrgJed = VratiOrgJed(pomManupulacija);
                            pomPlatilac = Convert.ToInt32(cboNalogodavac1.SelectedValue);
                        }

                        if (CenaNadjenaTip2 == 1 && CenaNadjenaTip2 == 0)
                        {
                            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                            SqlConnection con = new SqlConnection(s_connection);

                            con.Open();

                            SqlCommand cmd = new SqlCommand("select ID, Cena, OrgJed from Cene where Uvoznik = 0 and VrstaManipulacije = " + pomManupulacija + " and  Komitent = " + Convert.ToInt32(cboNalogodavac1.SelectedValue) + "", con);
                            SqlDataReader dr = cmd.ExecuteReader();

                            while (dr.Read())
                            {
                                //Izmenjeno
                                // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                                pomCena = Convert.ToDouble(dr["Cena"].ToString());
                                pomkolicina = 1;


                            }

                            con.Close();
                            pomPlatilac = Convert.ToInt32(cboNalogodavac1.SelectedValue);
                            pomOrgJed = VratiOrgJed(pomManupulacija);
                        }

                        if (CenaNadjenaTip2 == 0 && CenaNadjenaTip2 == 0)
                        {
                            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                            SqlConnection con = new SqlConnection(s_connection);

                            con.Open();

                            SqlCommand cmd = new SqlCommand("select ID, Cena, OrgJed from Cene where TipCenovnika =1 and VrstaManipulacije = " + pomManupulacija, con);
                            SqlDataReader dr = cmd.ExecuteReader();

                            while (dr.Read())
                            {
                                //Izmenjeno
                                // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                                pomCena = Convert.ToDouble(dr["Cena"].ToString());
                                pomkolicina = 1;


                            }

                            con.Close();
                            pomPlatilac = Convert.ToInt32(cboNalogodavac1.SelectedValue);
                            pomOrgJed = VratiOrgJed(pomManupulacija);
                            foreach (DataGridViewRow row2 in dataGridView1.Rows)
                            {
                                if (row2.Selected)
                                {
                                    pomID = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                                    UbaciStavkuUsluge(pomID, pomManupulacija, pomCena, pomkolicina, pomOrgJed, pomPlatilac, pomPokret, pomStatusKontejnera, pomForma);
                                }
                            }

                        }

                        //  pomCena = Convert.ToDouble(row.Cells[2].Value.ToString());
                        // pomkolicina= Convert.ToDouble(row.Cells[4].Value.ToString());
                        //   pomOrgJed = Convert.ToInt32(row.Cells[5].Value.ToString());


                    }
                }


            }
            catch
            {
                MessageBox.Show("Unos nije uspeo.Proverite da li imate definisanu cenu u Cenovniku!!!");
            }
        }
        private void FillGVUvozKonacnaPoPlanu()
        {
            var select = "    SELECT UvozKonacna.ID, BrojKontejnera,   CASE WHEN Prioritet > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Prioritet ,  CASE WHEN DobijenBZ > 0 THEN Cast(1 as bit) " +
          " ELSE Cast(0 as BIT) END as DobijenBZ ,TipKontenjera.Naziv as Vrsta_Kontejnera, DobijeBZ as DatumBZ ,  p1.PaNaziv as Uvoznik,   Brodovi.Naziv as Brod,n1.PaNaziv as Nalogodavac1, " +
          " Ref1 as Ref1, n2.PaNaziv as Nalogodavac2, Ref2 as Ref2, n3.PaNaziv as Nalogodavac3, Ref3 as Ref3, DobijenNalogBrodara as Dobijen_Nalog_Brodara ,BrodskaTeretnica as BL,  " +
          " Napomena1 as Napomena1, PIN,    BrodskaTeretnica,KontejnerskiTerminali.Naziv as R_L_SRB,  VrstaRobeADR.Naziv as ADR, b.PaNaziv as Brodar, pv.PaNaziv as VlasnikKontejnera,  " +
          " pp1.Naziv as Dirigacija_Kontejnera_Za,                VrstaPregleda as InsTret,p2.PaNaziv as SpedicijaRTC,  p3.PaNaziv as SpedicijaGranica,       VrstaCarinskogPostupka.Naziv as CarinskiPostupak,  " +
          " VrstePostupakaUvoz.Naziv as PostupakSaRobom,uvNacinPakovanja.Naziv as NacinPakovanja, Napomena as Napomena2,                         Carinarnice.Naziv as Carinarnica,   " +
          " p4.PaNaziv as OdredisnaSpedicija, MestaUtovara.Naziv as MestoIstovara, AdresaMestaUtovara, KontaktOsobe,  Email,  " +
          " BrojPlombe1, BrojPlombe2,    NetoRobe, BrutoRobe, TaraKontejnera, BrutoKontejnera,  Koleta FROM UvozKonacna inner join Partnerji on PaSifra = VlasnikKontejnera " +
          " inner join Partnerji p1 on p1.PaSifra = Uvoznik  inner join Partnerji p2 on p2.PaSifra = SpedicijaRTC  inner join Partnerji p3 on p3.PaSifra = SpedicijaGranica " +
          " inner join VrstaRobeHS on VrstaRobeHS.ID = UvozKonacna.NazivRobe   inner join NHM on NHM.ID = NHMBroj  inner join TipKontenjera on TipKontenjera.ID = UvozKonacna.TipKontejnera " +
          " inner join Carinarnice on Carinarnice.ID = UvozKonacna.OdredisnaCarina   inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = UvozKonacna.CarinskiPostupak " +
          " inner join KontejnerskiTerminali on KontejnerskiTerminali.ID = UvozKonacna.RLTErminali   inner join Partnerji n1 on n1.PaSifra = Nalogodavac1 " +
          " inner join Partnerji n2 on n2.PaSifra = Nalogodavac2   inner join Partnerji n3 on n3.PaSifra = Nalogodavac3   inner join Partnerji b on b.PaSifra = UvozKonacna.Brodar " +
          " inner join DirigacijaKontejneraZa pp1 on pp1.ID = UvozKonacna.DirigacijaKontejeraZa     inner join Brodovi on Brodovi.ID = UvozKonacna.NazivBroda    inner join VrstaRobeADR on VrstaRobeADR.ID = ADR     inner join VrstePostupakaUvoz on VrstePostupakaUvoz.ID = PostupakSaRobom   " +
          "  inner join MestaUtovara on UvozKOnacna.MestoIstovara = MestaUtovara.ID  " +
          " inner join uvNacinPakovanja on uvNacinPakovanja.ID = NacinPakovanja  inner join Partnerji p4 on p4.PaSifra = OdredisnaSpedicija " +
          " inner join Partnerji pv on pv.PaSifra = UvozKonacna.VlasnikKontejnera " +
            " where UvozKonacna.ID = " + pID + "  order by UvozKonacna.ID desc ";



            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
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

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Frozen = true;
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "BrojKontejnera";
            dataGridView1.Columns[1].Frozen = true;
            dataGridView1.Columns[1].Width = 100;
            /*
            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "BL";
            dataGridView1.Columns[2].Frozen = true;
            dataGridView1.Columns[2].Width = 90;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Dobijen_Nalog_Brodara";
            // dataGridView1.Columns[1].Frozen = true;
            dataGridView1.Columns[3].Width = 50;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "ATABroda";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column5 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Brod";
            dataGridView1.Columns[5].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Napomena";
            dataGridView1.Columns[6].Width = 100;

            DataGridViewColumn column7 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "DatumBZ";
            dataGridView1.Columns[7].Width = 80;

            DataGridViewColumn column8 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "PIN";
            dataGridView1.Columns[8].Width = 60;

            DataGridViewColumn column9 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "BrojKontejnera";
            //   dataGridView1.Columns[7].Frozen = true;
            dataGridView1.Columns[9].Width = 100;

            DataGridViewColumn column10 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Vrsta kontejnera";
            dataGridView1.Columns[10].Width = 120;

            DataGridViewColumn column11 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "R_L_SRB";
            dataGridView1.Columns[11].Width = 120;

            DataGridViewColumn column12 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Dirigacija_Kontejnera_Za";
            dataGridView1.Columns[12].Width = 100;

            DataGridViewColumn column13 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "BL";
            // dataGridView1.Columns[13].Frozen = true;
            dataGridView1.Columns[13].Width = 90;
            */
            RefreshDataGridColor();

        }

        private void FillGVUvozKonacnaPoPlanuSVI()
        {
            var select = "    SELECT UvozKonacna.ID, BrojKontejnera,   CASE WHEN Prioritet > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Prioritet ,  CASE WHEN DobijenBZ > 0 THEN Cast(1 as bit) " +
           " ELSE Cast(0 as BIT) END as DobijenBZ ,TipKontenjera.Naziv as Vrsta_Kontejnera, DobijeBZ as DatumBZ ,  p1.PaNaziv as Uvoznik,   Brodovi.Naziv as Brod,n1.PaNaziv as Nalogodavac1, " +
           " Ref1 as Ref1, n2.PaNaziv as Nalogodavac2, Ref2 as Ref2, n3.PaNaziv as Nalogodavac3, Ref3 as Ref3, DobijenNalogBrodara as Dobijen_Nalog_Brodara ,BrodskaTeretnica as BL,  " +
           " Napomena1 as Napomena1, PIN,    BrodskaTeretnica,KontejnerskiTerminali.Naziv as R_L_SRB,  VrstaRobeADR.Naziv as ADR, b.PaNaziv as Brodar, pv.PaNaziv as VlasnikKontejnera,  " +
           " pp1.Naziv as Dirigacija_Kontejnera_Za,                VrstaPregleda as InsTret,p2.PaNaziv as SpedicijaRTC,  p3.PaNaziv as SpedicijaGranica,       VrstaCarinskogPostupka.Naziv as CarinskiPostupak,  " +
           " VrstePostupakaUvoz.Naziv as PostupakSaRobom,uvNacinPakovanja.Naziv as NacinPakovanja, Napomena as Napomena2,                         Carinarnice.Naziv as Carinarnica,   " +
           " p4.PaNaziv as OdredisnaSpedicija, MestaUtovara.Naziv as MestoIstovara, AdresaMestaUtovara, KontaktOsobe,  Email,  " +
           " BrojPlombe1, BrojPlombe2,    NetoRobe, BrutoRobe, TaraKontejnera, BrutoKontejnera,  Koleta FROM UvozKonacna inner join Partnerji on PaSifra = VlasnikKontejnera " +
           " inner join Partnerji p1 on p1.PaSifra = Uvoznik  inner join Partnerji p2 on p2.PaSifra = SpedicijaRTC  inner join Partnerji p3 on p3.PaSifra = SpedicijaGranica " +
           " inner join VrstaRobeHS on VrstaRobeHS.ID = UvozKonacna.NazivRobe   inner join NHM on NHM.ID = NHMBroj  inner join TipKontenjera on TipKontenjera.ID = UvozKonacna.TipKontejnera " +
           " inner join Carinarnice on Carinarnice.ID = UvozKonacna.OdredisnaCarina   inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = UvozKonacna.CarinskiPostupak " +
           " inner join KontejnerskiTerminali on KontejnerskiTerminali.ID = UvozKonacna.RLTErminali   inner join Partnerji n1 on n1.PaSifra = Nalogodavac1 " +
           " inner join Partnerji n2 on n2.PaSifra = Nalogodavac2   inner join Partnerji n3 on n3.PaSifra = Nalogodavac3   inner join Partnerji b on b.PaSifra = UvozKonacna.Brodar " +
           " inner join DirigacijaKontejneraZa pp1 on pp1.ID = UvozKonacna.DirigacijaKontejeraZa     inner join Brodovi on Brodovi.ID = UvozKonacna.NazivBroda    inner join VrstaRobeADR on VrstaRobeADR.ID = ADR     inner join VrstePostupakaUvoz on VrstePostupakaUvoz.ID = PostupakSaRobom   " +
           "  inner join MestaUtovara on UvozKOnacna.MestoIstovara = MestaUtovara.ID  " +
           " inner join uvNacinPakovanja on uvNacinPakovanja.ID = NacinPakovanja  inner join Partnerji p4 on p4.PaSifra = OdredisnaSpedicija " +
           " inner join Partnerji pv on pv.PaSifra = UvozKonacna.VlasnikKontejnera " +
            " where UvozKonacna.IdNadredjeni = " + Convert.ToInt32(txtNadredjeni.Text) + "  order by UvozKonacna.ID desc ";



            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
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

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Frozen = true;
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "BrojKontejnera";
            dataGridView1.Columns[1].Frozen = true;
            dataGridView1.Columns[1].Width = 100;
            /*
            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "BL";
            dataGridView1.Columns[2].Frozen = true;
            dataGridView1.Columns[2].Width = 90;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Dobijen_Nalog_Brodara";
            // dataGridView1.Columns[1].Frozen = true;
            dataGridView1.Columns[3].Width = 50;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "ATABroda";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column5 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Brod";
            dataGridView1.Columns[5].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Napomena";
            dataGridView1.Columns[6].Width = 100;

            DataGridViewColumn column7 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "DatumBZ";
            dataGridView1.Columns[7].Width = 80;

            DataGridViewColumn column8 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "PIN";
            dataGridView1.Columns[8].Width = 60;

            DataGridViewColumn column9 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "BrojKontejnera";
            //   dataGridView1.Columns[7].Frozen = true;
            dataGridView1.Columns[9].Width = 100;

            DataGridViewColumn column10 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Vrsta kontejnera";
            dataGridView1.Columns[10].Width = 120;

            DataGridViewColumn column11 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "R_L_SRB";
            dataGridView1.Columns[11].Width = 120;

            DataGridViewColumn column12 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Dirigacija_Kontejnera_Za";
            dataGridView1.Columns[12].Width = 100;

            DataGridViewColumn column13 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "BL";
            // dataGridView1.Columns[13].Frozen = true;
            dataGridView1.Columns[13].Width = 90;
            */
            RefreshDataGridColor();

        }

        private void FillGVUvoz()
        {
            /*
            var select = "SELECT Uvoz.ID, [BrojKontejnera], BrodskaTeretnica as BL, DobijenNalogBrodara as Dobijen_Nalog_Brodara ,ATABroda, Brodovi.Naziv as Brod,Napomena1 as Napomena1, DobijeBZ as DatumBZ ,PIN, " +
 " [BrojKontejnera], TipKontenjera.Naziv as Vrsta_Kontejnera,  KontejnerskiTerminali.Naziv as R_L_SRB, pp1.Naziv as Dirigacija_Kontejnera_Za,  " +
 "   BrodskaTeretnica, VrstaRobeADR.Naziv as ADR, b.PaNaziv as Brodar, n1.PaNaziv as Nalogodavac1, Ref1 as Ref1, n2.PaNaziv as Nalogodavac2, Ref2 as Ref2, n3.PaNaziv as Nalogodavac3, Ref3 as Ref3, " +
  "      p1.PaNaziv as Uvoznik,   " +
  "  (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaManipulacije.Naziv as nvarchar(20))   FROM UvozVrstaManipulacije " +
   "       inner join VrstaManipulacije on VrstaManipulacije.ID = UvozVrstaManipulacije.IDVrstaManipulacije   where UvozVrstaManipulacije.IDNadredjena = Uvoz.ID " +
   "        FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as VrsteUsluga,   " +
   "     (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaRobeHS.HSKod as nvarchar(20))   FROM UvozVrstaRobeHS " +
   "       inner join VrstaRobeHS on UvozVrstaRobeHS.IDVrstaRobeHS = VrstaRobeHS.ID   where UvozVrstaRobeHS.IDNadredjena = Uvoz.ID " +
   "        FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as HS,   " +
   "    (SELECT  STUFF((SELECT distinct    '/' + Cast(NHM.Broj as nvarchar(20)) " +
  "            FROM UvozNHM  inner join NHM on UvozNHM.IDNHM = NHM.ID  where UvozNHM.IDNadredjena = Uvoz.ID   FOR XML PATH('')), 1, 1, ''  ) As Skupljen) as NHM,   " +
   "              VrstaPregleda as VrstaPregleda,p2.PaNaziv as SpedicijaRTC,  p3.PaNaziv as SpedicijaGranica,      " +
   " VrstaCarinskogPostupka.Naziv as CarinskiPostupak,   " +
   "                  VrstePostupakaUvoz.Naziv as PostupakSaRobom,uvNacinPakovanja.Naziv as NacinPakovanja, Napomena as Napomena2,  " +
   "                      (Carinarnice.CINaziv + ' ' + Carinarnice.CIOznaka + ' ' + CIEmail + ' ' + CITelefon + ' / ' + p3.PaNaziv) as Carinarnica,   " +
   "                               p4.PaNaziv as OdredisnaSpedicija, MestoIstovara, KontaktOsoba, Email,        BrojPlombe1, BrojPlombe2,   " +
" PredefinisanePoruke.Naziv as NapomenaZaPozicioniranje,  " +
 " NetoRobe, BrutoRobe, TaraKontejnera, BrutoKontejnera, " +
 " Koleta, green " +
 " FROM Uvoz inner join Partnerji on PaSifra = VlasnikKontejnera " +
 " inner join Partnerji p1 on p1.PaSifra = Uvoznik " +
 " inner join Partnerji p2 on p2.PaSifra = SpedicijaRTC " +
" inner join Partnerji p3 on p3.PaSifra = SpedicijaGranica " +
 "  inner join VrstaRobeHS on VrstaRobeHS.ID = Uvoz.NazivRobe " +
"  inner join NHM on NHM.ID = NHMBroj " +
 " inner join TipKontenjera on TipKontenjera.ID = Uvoz.TipKontejnera " +
 "  inner join Carinarnice on Carinarnice.ID = Uvoz.OdredisnaCarina " +
 "  inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = Uvoz.CarinskiPostupak " +
 " inner join Predefinisaneporuke on PredefinisanePoruke.ID = Uvoz.NapomenaZaPozicioniranje " +
 "  inner join KontejnerskiTerminali on KontejnerskiTerminali.ID = Uvoz.RLTErminali " +
 "  inner join Partnerji n1 on n1.PaSifra = Nalogodavac1 " +
 "  inner join Partnerji n2 on n2.PaSifra = Nalogodavac2 " +
 "  inner join Partnerji n3 on n3.PaSifra = Nalogodavac3 " +
 "  inner join Partnerji b on b.PaSifra = Uvoz.Brodar " +
  " inner join PredefinisanePoruke pp1 on pp1.ID = DirigacijaKontejeraZa   " +
 "  inner join Brodovi on Brodovi.ID = Uvoz.NazivBroda " +
                              "   inner join VrstaRobeADR on VrstaRobeADR.ID = ADR " +
                              "    inner join VrstePostupakaUvoz on VrstePostupakaUvoz.ID = PostupakSaRobom    inner join uvNacinPakovanja " +
 " on uvNacinPakovanja.ID = NacinPakovanja  inner join Partnerji p4 on p4.PaSifra = OdredisnaSpedicija  order by Uvoz.ID desc ";
            */

            var select = "SELECT Uvoz.ID, [BrojKontejnera],TipKontenjera.Naziv as Vrsta_Kontejnera, BrodskaTeretnica as BL,  DobijenBZ, Brodovi.Naziv as Brod, p1.PaNaziv as Uvoznik, " +
" n1.PaNaziv as NalogodavacZaVoz, Ref1 as Ref1,n2.PaNaziv as NalogodavacZaUsluge, Ref2 as Ref2,n3.PaNaziv as NalogodavacZaDrumski,DobijenNalogBrodara as Dobijen_Nalog_Brodara ,ATABroda,  " +
" Napomena1 as Napomena1,  DobijeBZ as DatumBZ ,PIN,    KontejnerskiTerminali.Naziv as R_L_SRB, pp1.Naziv as Dirigacija_Kontejnera_Za,   BrodskaTeretnica, " +
" VrstaRobeADR.Naziv as ADR, b.PaNaziv as Brodar,pv.PaNaziv as VlasnikKontejnera,     Ref3 as Ref3,         VrstaPregleda as InsTret,p2.PaNaziv as SpedicijaRTC,  " +
" p3.PaNaziv as SpedicijaGranica,       VrstaCarinskogPostupka.Naziv as CarinskiPostupak,  " +
" VrstePostupakaUvoz.Naziv as PostupakSaRobom,uvNacinPakovanja.Naziv as NacinPakovanja, " +
" Napomena as Napomena2, NaslovStatusaVozila as NaslovZaslanjestatusa,  Carinarnice.Naziv as Carinarnica,   " +
" p4.PaNaziv as OdredisnaSpedicija, MestaUtovara.Naziv as MestoIstovara, KontaktOsobe, Email, " +
" BrojPlombe1, BrojPlombe2,    NetoRobe, BrutoRobe, TaraKontejnera, BrutoKontejnera,  Koleta, green FROM Uvoz Left join Partnerji on PaSifra = VlasnikKontejnera " +
" inner join Partnerji p1 on p1.PaSifra = Uvoznik " +
" inner join Partnerji p2 on p2.PaSifra = SpedicijaRTC " +
" inner join Partnerji p3 on p3.PaSifra = SpedicijaGranica " +
" inner join TipKontenjera on TipKontenjera.ID = Uvoz.TipKontejnera " +
" inner join Carinarnice on Carinarnice.ID = Uvoz.OdredisnaCarina " +
" inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = Uvoz.CarinskiPostupak " +
" inner join Predefinisaneporuke on PredefinisanePoruke.ID = Uvoz.NapomenaZaPozicioniranje " +
" inner join KontejnerskiTerminali on KontejnerskiTerminali.ID = Uvoz.RLTErminali " +
" inner join Partnerji n1 on n1.PaSifra = Nalogodavac1 " +
" inner join Partnerji n2 on n2.PaSifra = Nalogodavac2 " +
" inner join Partnerji n3 on n3.PaSifra = Nalogodavac3 " +
" inner join Partnerji b on b.PaSifra = Uvoz.Brodar " +
" inner join  DirigacijaKontejneraZa pp1 on pp1.ID = Uvoz.DirigacijaKontejeraZa " +
" inner join Brodovi on Brodovi.ID = Uvoz.NazivBroda " +
" inner join VrstaRobeADR on VrstaRobeADR.ID = ADR " +
" inner join VrstePostupakaUvoz on VrstePostupakaUvoz.ID = PostupakSaRobom " +
" inner join MestaUtovara on Uvoz.MestoIstovara = MestaUtovara.ID " +
" inner join uvNacinPakovanja on uvNacinPakovanja.ID = NacinPakovanja " +
" inner join Partnerji p4 on p4.PaSifra = OdredisnaSpedicija " +
" inner join Partnerji pv on pv.PaSifra = Uvoz.VlasnikKontejnera " +
" where Uvoz.ID = " + pID + " order by Uvoz.ID desc ";

            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
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

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Frozen = true;
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "BrojKontejnera";
            dataGridView1.Columns[1].Frozen = true;
            dataGridView1.Columns[1].Width = 100;
            /*
            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "BL";
            dataGridView1.Columns[2].Frozen = true;
            dataGridView1.Columns[2].Width = 90;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Dobijen_Nalog_Brodara";
            // dataGridView1.Columns[1].Frozen = true;
            dataGridView1.Columns[3].Width = 50;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "ATABroda";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column5 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Brod";
            dataGridView1.Columns[5].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Napomena";
            dataGridView1.Columns[6].Width = 100;

            DataGridViewColumn column7 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "DatumBZ";
            dataGridView1.Columns[7].Width = 80;

            DataGridViewColumn column8 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "PIN";
            dataGridView1.Columns[8].Width = 60;

            DataGridViewColumn column9 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "BrojKontejnera";
            //   dataGridView1.Columns[7].Frozen = true;
            dataGridView1.Columns[9].Width = 100;

            DataGridViewColumn column10 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Vrsta kontejnera";
            dataGridView1.Columns[10].Width = 120;

            DataGridViewColumn column11 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "R_L_SRB";
            dataGridView1.Columns[11].Width = 120;

            DataGridViewColumn column12 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Dirigacija_Kontejnera_Za";
            dataGridView1.Columns[12].Width = 100;

            DataGridViewColumn column13 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "BL";
            // dataGridView1.Columns[13].Frozen = true;
            dataGridView1.Columns[13].Width = 90;
            */
            RefreshDataGridColor();


        }

        private void FillGVUvozSVI()
        {
            /*
            var select = "SELECT Uvoz.ID, [BrojKontejnera], BrodskaTeretnica as BL, DobijenNalogBrodara as Dobijen_Nalog_Brodara ,ATABroda, Brodovi.Naziv as Brod,Napomena1 as Napomena1, DobijeBZ as DatumBZ ,PIN, " +
 " [BrojKontejnera], TipKontenjera.Naziv as Vrsta_Kontejnera,  KontejnerskiTerminali.Naziv as R_L_SRB, pp1.Naziv as Dirigacija_Kontejnera_Za,  " +
 "   BrodskaTeretnica, VrstaRobeADR.Naziv as ADR, b.PaNaziv as Brodar, n1.PaNaziv as Nalogodavac1, Ref1 as Ref1, n2.PaNaziv as Nalogodavac2, Ref2 as Ref2, n3.PaNaziv as Nalogodavac3, Ref3 as Ref3, " +
  "      p1.PaNaziv as Uvoznik,   " +
  "  (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaManipulacije.Naziv as nvarchar(20))   FROM UvozVrstaManipulacije " +
   "       inner join VrstaManipulacije on VrstaManipulacije.ID = UvozVrstaManipulacije.IDVrstaManipulacije   where UvozVrstaManipulacije.IDNadredjena = Uvoz.ID " +
   "        FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as VrsteUsluga,   " +
   "     (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaRobeHS.HSKod as nvarchar(20))   FROM UvozVrstaRobeHS " +
   "       inner join VrstaRobeHS on UvozVrstaRobeHS.IDVrstaRobeHS = VrstaRobeHS.ID   where UvozVrstaRobeHS.IDNadredjena = Uvoz.ID " +
   "        FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as HS,   " +
   "    (SELECT  STUFF((SELECT distinct    '/' + Cast(NHM.Broj as nvarchar(20)) " +
  "            FROM UvozNHM  inner join NHM on UvozNHM.IDNHM = NHM.ID  where UvozNHM.IDNadredjena = Uvoz.ID   FOR XML PATH('')), 1, 1, ''  ) As Skupljen) as NHM,   " +
   "              VrstaPregleda as VrstaPregleda,p2.PaNaziv as SpedicijaRTC,  p3.PaNaziv as SpedicijaGranica,      " +
   " VrstaCarinskogPostupka.Naziv as CarinskiPostupak,   " +
   "                  VrstePostupakaUvoz.Naziv as PostupakSaRobom,uvNacinPakovanja.Naziv as NacinPakovanja, Napomena as Napomena2,  " +
   "                      (Carinarnice.CINaziv + ' ' + Carinarnice.CIOznaka + ' ' + CIEmail + ' ' + CITelefon + ' / ' + p3.PaNaziv) as Carinarnica,   " +
   "                               p4.PaNaziv as OdredisnaSpedicija, MestoIstovara, KontaktOsoba, Email,        BrojPlombe1, BrojPlombe2,   " +
" PredefinisanePoruke.Naziv as NapomenaZaPozicioniranje,  " +
 " NetoRobe, BrutoRobe, TaraKontejnera, BrutoKontejnera, " +
 " Koleta, green " +
 " FROM Uvoz inner join Partnerji on PaSifra = VlasnikKontejnera " +
 " inner join Partnerji p1 on p1.PaSifra = Uvoznik " +
 " inner join Partnerji p2 on p2.PaSifra = SpedicijaRTC " +
" inner join Partnerji p3 on p3.PaSifra = SpedicijaGranica " +
 "  inner join VrstaRobeHS on VrstaRobeHS.ID = Uvoz.NazivRobe " +
"  inner join NHM on NHM.ID = NHMBroj " +
 " inner join TipKontenjera on TipKontenjera.ID = Uvoz.TipKontejnera " +
 "  inner join Carinarnice on Carinarnice.ID = Uvoz.OdredisnaCarina " +
 "  inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = Uvoz.CarinskiPostupak " +
 " inner join Predefinisaneporuke on PredefinisanePoruke.ID = Uvoz.NapomenaZaPozicioniranje " +
 "  inner join KontejnerskiTerminali on KontejnerskiTerminali.ID = Uvoz.RLTErminali " +
 "  inner join Partnerji n1 on n1.PaSifra = Nalogodavac1 " +
 "  inner join Partnerji n2 on n2.PaSifra = Nalogodavac2 " +
 "  inner join Partnerji n3 on n3.PaSifra = Nalogodavac3 " +
 "  inner join Partnerji b on b.PaSifra = Uvoz.Brodar " +
  " inner join PredefinisanePoruke pp1 on pp1.ID = DirigacijaKontejeraZa   " +
 "  inner join Brodovi on Brodovi.ID = Uvoz.NazivBroda " +
                              "   inner join VrstaRobeADR on VrstaRobeADR.ID = ADR " +
                              "    inner join VrstePostupakaUvoz on VrstePostupakaUvoz.ID = PostupakSaRobom    inner join uvNacinPakovanja " +
 " on uvNacinPakovanja.ID = NacinPakovanja  inner join Partnerji p4 on p4.PaSifra = OdredisnaSpedicija  order by Uvoz.ID desc ";
            */

            var select = "SELECT Uvoz.ID, [BrojKontejnera],TipKontenjera.Naziv as Vrsta_Kontejnera, BrodskaTeretnica as BL,  DobijenBZ, Brodovi.Naziv as Brod, p1.PaNaziv as Uvoznik, " +
" n1.PaNaziv as NalogodavacZaVoz, Ref1 as Ref1,n2.PaNaziv as NalogodavacZaUsluge, Ref2 as Ref2,n3.PaNaziv as NalogodavacZaDrumski,DobijenNalogBrodara as Dobijen_Nalog_Brodara ,ATABroda,  " +
" Napomena1 as Napomena1,  DobijeBZ as DatumBZ ,PIN,    KontejnerskiTerminali.Naziv as R_L_SRB, pp1.Naziv as Dirigacija_Kontejnera_Za,   BrodskaTeretnica, " +
" VrstaRobeADR.Naziv as ADR, b.PaNaziv as Brodar,pv.PaNaziv as VlasnikKontejnera,     Ref3 as Ref3,         VrstaPregleda as InsTret,p2.PaNaziv as SpedicijaRTC,  " +
" p3.PaNaziv as SpedicijaGranica,       VrstaCarinskogPostupka.Naziv as CarinskiPostupak,  " +
" VrstePostupakaUvoz.Naziv as PostupakSaRobom,uvNacinPakovanja.Naziv as NacinPakovanja, " +
" Napomena as Napomena2, NaslovStatusaVozila as NaslovZaslanjestatusa,  Carinarnice.Naziv as Carinarnica,   " +
" p4.PaNaziv as OdredisnaSpedicija, MestaUtovara.Naziv as MestoIstovara, KontaktOsobe, Email, " +
" BrojPlombe1, BrojPlombe2,    NetoRobe, BrutoRobe, TaraKontejnera, BrutoKontejnera,  Koleta, green FROM Uvoz Left join Partnerji on PaSifra = VlasnikKontejnera " +
" inner join Partnerji p1 on p1.PaSifra = Uvoznik " +
" inner join Partnerji p2 on p2.PaSifra = SpedicijaRTC " +
" inner join Partnerji p3 on p3.PaSifra = SpedicijaGranica " +
" inner join TipKontenjera on TipKontenjera.ID = Uvoz.TipKontejnera " +
" inner join Carinarnice on Carinarnice.ID = Uvoz.OdredisnaCarina " +
" inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = Uvoz.CarinskiPostupak " +
" inner join Predefinisaneporuke on PredefinisanePoruke.ID = Uvoz.NapomenaZaPozicioniranje " +
" inner join KontejnerskiTerminali on KontejnerskiTerminali.ID = Uvoz.RLTErminali " +
" inner join Partnerji n1 on n1.PaSifra = Nalogodavac1 " +
" inner join Partnerji n2 on n2.PaSifra = Nalogodavac2 " +
" inner join Partnerji n3 on n3.PaSifra = Nalogodavac3 " +
" inner join Partnerji b on b.PaSifra = Uvoz.Brodar " +
" inner join  DirigacijaKontejneraZa pp1 on pp1.ID = Uvoz.DirigacijaKontejeraZa " +
" inner join Brodovi on Brodovi.ID = Uvoz.NazivBroda " +
" inner join VrstaRobeADR on VrstaRobeADR.ID = ADR " +
" inner join VrstePostupakaUvoz on VrstePostupakaUvoz.ID = PostupakSaRobom " +
" inner join MestaUtovara on Uvoz.MestoIstovara = MestaUtovara.ID " +
" inner join uvNacinPakovanja on uvNacinPakovanja.ID = NacinPakovanja " +
" inner join Partnerji p4 on p4.PaSifra = OdredisnaSpedicija " +
" inner join Partnerji pv on pv.PaSifra = Uvoz.VlasnikKontejnera " +
"  order by Uvoz.ID desc ";

            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
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

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Frozen = true;
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "BrojKontejnera";
            dataGridView1.Columns[1].Frozen = true;
            dataGridView1.Columns[1].Width = 100;
            /*
            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "BL";
            dataGridView1.Columns[2].Frozen = true;
            dataGridView1.Columns[2].Width = 90;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Dobijen_Nalog_Brodara";
            // dataGridView1.Columns[1].Frozen = true;
            dataGridView1.Columns[3].Width = 50;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "ATABroda";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column5 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Brod";
            dataGridView1.Columns[5].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Napomena";
            dataGridView1.Columns[6].Width = 100;

            DataGridViewColumn column7 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "DatumBZ";
            dataGridView1.Columns[7].Width = 80;

            DataGridViewColumn column8 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "PIN";
            dataGridView1.Columns[8].Width = 60;

            DataGridViewColumn column9 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "BrojKontejnera";
            //   dataGridView1.Columns[7].Frozen = true;
            dataGridView1.Columns[9].Width = 100;

            DataGridViewColumn column10 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Vrsta kontejnera";
            dataGridView1.Columns[10].Width = 120;

            DataGridViewColumn column11 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "R_L_SRB";
            dataGridView1.Columns[11].Width = 120;

            DataGridViewColumn column12 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Dirigacija_Kontejnera_Za";
            dataGridView1.Columns[12].Width = 100;

            DataGridViewColumn column13 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "BL";
            // dataGridView1.Columns[13].Frozen = true;
            dataGridView1.Columns[13].Width = 90;
            */
            RefreshDataGridColor();


        }

        private void RefreshDataGridColor()
        {

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {


                if (row.Cells[25].Value.ToString() == "1")
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                    row.DefaultCellStyle.SelectionBackColor = Color.Green;
                }
                else
                {

                }
            }

            dataGridView1.Refresh();

        }
        private void frmUnosManipulacija_Load(object sender, EventArgs e)
        {
            FillCombo();

            txtNadredjeni.Text = pIDPlana.ToString();
            txtID.Text = pID.ToString();
            cboNalogodavac1.SelectedValue = pNalogodavac1;
            cboNalogodavac2.SelectedValue = pNalogodavac2;
            cboNalogodavac3.SelectedValue = pNalogodavac3;
            cboUvoznik.SelectedValue = pUvoznik;
            if (txtNadredjeni.Text == "0")
            {
                FillGVUvoz();
            }
            else
            {
                FillGVUvozKonacnaPoPlanu();

            }
            Usao = 1;
        }

        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connection);

            var partner8 = "Select PaSifra,PaNaziv From Partnerji order by PaSifra";
            var partAD8 = new SqlDataAdapter(partner8, conn);
            var partDS8 = new DataSet();
            partAD8.Fill(partDS8);
            cboNalogodavac1.DataSource = partDS8.Tables[0];
            cboNalogodavac1.DisplayMember = "PaNaziv";
            cboNalogodavac1.ValueMember = "PaSifra";

            var partner9 = "Select PaSifra,PaNaziv From Partnerji order by PaSifra";
            var partAD9 = new SqlDataAdapter(partner9, conn);
            var partDS9 = new DataSet();
            partAD9.Fill(partDS9);
            cboNalogodavac2.DataSource = partDS9.Tables[0];
            cboNalogodavac2.DisplayMember = "PaNaziv";
            cboNalogodavac2.ValueMember = "PaSifra";

            var partner10 = "Select PaSifra,PaNaziv From Partnerji order by PaSifra";
            var partAD10 = new SqlDataAdapter(partner10, conn);
            var partDS10 = new DataSet();
            partAD9.Fill(partDS10);
            cboNalogodavac3.DataSource = partDS10.Tables[0];
            cboNalogodavac3.DisplayMember = "PaNaziv";
            cboNalogodavac3.ValueMember = "PaSifra";

            var partner2 = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var partAD2 = new SqlDataAdapter(partner2, conn);
            var partDS2 = new DataSet();
            partAD2.Fill(partDS2);
            cboUvoznik.DataSource = partDS2.Tables[0];
            cboUvoznik.DisplayMember = "PaNaziv";
            cboUvoznik.ValueMember = "PaSifra";


            var partner22 = "Select DISTINCT ID from Scenario Order by ID";
            var partAD22 = new SqlDataAdapter(partner22, conn);
            var partDS22 = new DataSet();
            partAD22.Fill(partDS22);
            cboScenario.DataSource = partDS22.Tables[0];
            cboScenario.DisplayMember = "ID";
            cboScenario.ValueMember = "ID";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillDGN2();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FillDGN3();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    txtID.Text = row.Cells[0].Value.ToString();
                    FillDG8();
                    //  VratiPodatkeSelect(Convert.ToInt32(txtID.Text));

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FillDN1BU();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FillDGN2BU();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FillDGN3BU();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int CenaNadjenaTip1 = 0;
            int CenaNadjenaTip2 = 0;
            int pomID = 0;
            int pomManupulacija = 0;
            double pomCena = 0;
            double pomkolicina = 1;
            int pomPlatilac = 0;
            string pomPokret = "";
            int pomStatusKontejnera = 0;
            string pomForma = "";
            try
            {
                foreach (DataGridViewRow row in dataGridView6.Rows)
                {
                    if (row.Selected)
                    {
                        pomManupulacija = Convert.ToInt32(row.Cells[0].Value.ToString());
                        CenaNadjenaTip1 = PretraziPoNalogodavciIIzvozniku(pomManupulacija, Convert.ToInt32(cboNalogodavac2.SelectedValue), Convert.ToInt32(cboUvoznik.SelectedValue));
                        CenaNadjenaTip2 = PretraziPoNalogodavci(pomManupulacija, Convert.ToInt32(cboNalogodavac2.SelectedValue), Convert.ToInt32(cboUvoznik.SelectedValue));
                        pomPokret = row.Cells[7].Value.ToString();
                        pomStatusKontejnera = Convert.ToInt32(row.Cells[8].Value.ToString());
                        pomForma = row.Cells[10].Value.ToString();
                        if (CenaNadjenaTip1 == 1)
                        {
                            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                            SqlConnection con = new SqlConnection(s_connection);

                            con.Open();

                            SqlCommand cmd = new SqlCommand("select ID, Cena, OrgJed from Cene where VrstaManipulacije = " + pomManupulacija + " Uvoznik = " + cboUvoznik.SelectedValue + " and  Komitent = " + Convert.ToInt32(cboNalogodavac2.SelectedValue) + "", con);
                            SqlDataReader dr = cmd.ExecuteReader();

                            while (dr.Read())
                            {

                                pomCena = Convert.ToDouble(dr["Cena"].ToString());
                                pomkolicina = 1;


                            }

                            con.Close();
                            pomPlatilac = Convert.ToInt32(cboNalogodavac2.SelectedValue);
                            pomOrgJed = VratiOrgJed(pomManupulacija);
                        }

                        if (CenaNadjenaTip2 == 1 && CenaNadjenaTip2 == 0)
                        {
                            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                            SqlConnection con = new SqlConnection(s_connection);

                            con.Open();

                            SqlCommand cmd = new SqlCommand("select ID, Cena, OrgJed from Cene where Uvoznik = 0 and VrstaManipulacije = " + pomManupulacija + " and  Komitent = " + Convert.ToInt32(cboNalogodavac2.SelectedValue) + "", con);
                            SqlDataReader dr = cmd.ExecuteReader();

                            while (dr.Read())
                            {
                                //Izmenjeno
                                // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                                pomCena = Convert.ToDouble(dr["Cena"].ToString());
                                pomkolicina = 1;


                            }

                            con.Close();
                            pomPlatilac = Convert.ToInt32(cboNalogodavac2.SelectedValue);
                            pomOrgJed = VratiOrgJed(pomManupulacija);
                        }

                        if (CenaNadjenaTip2 == 0 && CenaNadjenaTip2 == 0)
                        {
                            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                            SqlConnection con = new SqlConnection(s_connection);

                            con.Open();

                            SqlCommand cmd = new SqlCommand("select ID, Cena, OrgJed from Cene where TipCenovnika =1 and VrstaManipulacije = " + pomManupulacija, con);
                            SqlDataReader dr = cmd.ExecuteReader();

                            while (dr.Read())
                            {
                                //Izmenjeno
                                // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                                pomCena = Convert.ToDouble(dr["Cena"].ToString());
                                pomkolicina = 1;


                            }

                            con.Close();
                            pomPlatilac = Convert.ToInt32(cboNalogodavac2.SelectedValue);
                            pomOrgJed = VratiOrgJed(pomManupulacija);
                            foreach (DataGridViewRow row2 in dataGridView1.Rows)
                            {
                                if (row2.Selected)
                                {
                                    pomID = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                                    UbaciStavkuUsluge(pomID, pomManupulacija, pomCena, pomkolicina, pomOrgJed, pomPlatilac, pomPokret, pomStatusKontejnera, pomForma);
                                }
                            }
                        }

                        //  pomCena = Convert.ToDouble(row.Cells[2].Value.ToString());
                        // pomkolicina= Convert.ToDouble(row.Cells[4].Value.ToString());
                        //   pomOrgJed = Convert.ToInt32(row.Cells[5].Value.ToString());
                       


                    }
                }


            }
            catch
            {
                MessageBox.Show("Unos nije uspeo.Proverite da li imate definisanu cenu u Cenovniku!!!");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int CenaNadjenaTip1 = 0;
            int CenaNadjenaTip2 = 0;
            int pomID = 0;
            int pomManupulacija = 0;
            double pomCena = 0;
            double pomkolicina = 1;
            int pomPlatilac = 0;
            string pomPokret = "";
            int pomStatusKontejnera = 0;
            string pomForma = "";
            try
            {
                foreach (DataGridViewRow row in dataGridView6.Rows)
                {
                    if (row.Selected)
                    {
                        pomManupulacija = Convert.ToInt32(row.Cells[0].Value.ToString());
                        CenaNadjenaTip1 = PretraziPoNalogodavciIIzvozniku(pomManupulacija, Convert.ToInt32(cboNalogodavac3.SelectedValue), Convert.ToInt32(cboUvoznik.SelectedValue));
                        CenaNadjenaTip2 = PretraziPoNalogodavci(pomManupulacija, Convert.ToInt32(cboNalogodavac3.SelectedValue), Convert.ToInt32(cboUvoznik.SelectedValue));
                        pomPokret = row.Cells[7].Value.ToString();
                        pomStatusKontejnera = Convert.ToInt32(row.Cells[8].Value.ToString());
                        pomForma = row.Cells[10].Value.ToString();
                        if (CenaNadjenaTip1 == 1)
                        {
                            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                            SqlConnection con = new SqlConnection(s_connection);

                            con.Open();

                            SqlCommand cmd = new SqlCommand("select ID, Cena, OrgJed from Cene where VrstaManipulacije = " + pomManupulacija + " Uvoznik = " + cboUvoznik.SelectedValue + " and  Komitent = " + Convert.ToInt32(cboNalogodavac3.SelectedValue) + "", con);
                            SqlDataReader dr = cmd.ExecuteReader();

                            while (dr.Read())
                            {

                                pomCena = Convert.ToDouble(dr["Cena"].ToString());
                                pomkolicina = 1;


                            }

                            con.Close();
                            pomPlatilac = Convert.ToInt32(cboNalogodavac3.SelectedValue);
                            pomOrgJed = VratiOrgJed(pomManupulacija);
                        }

                        if (CenaNadjenaTip2 == 1 && CenaNadjenaTip2 == 0)
                        {
                            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                            SqlConnection con = new SqlConnection(s_connection);

                            con.Open();

                            SqlCommand cmd = new SqlCommand("select ID, Cena, OrgJed from Cene where Uvoznik = 0 and VrstaManipulacije = " + pomManupulacija + " and  Komitent = " + Convert.ToInt32(cboNalogodavac3.SelectedValue) + "", con);
                            SqlDataReader dr = cmd.ExecuteReader();

                            while (dr.Read())
                            {
                                //Izmenjeno
                                // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                                pomCena = Convert.ToDouble(dr["Cena"].ToString());
                                pomkolicina = 1;


                            }

                            con.Close();
                            pomPlatilac = Convert.ToInt32(cboNalogodavac3.SelectedValue);
                            pomOrgJed = VratiOrgJed(pomManupulacija);
                        }

                        if (CenaNadjenaTip2 == 0 && CenaNadjenaTip2 == 0)
                        {
                            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                            SqlConnection con = new SqlConnection(s_connection);

                            con.Open();

                            SqlCommand cmd = new SqlCommand("select ID, Cena, OrgJed from Cene where TipCenovnika =1 and VrstaManipulacije = " + pomManupulacija, con);
                            SqlDataReader dr = cmd.ExecuteReader();

                            while (dr.Read())
                            {
                                //Izmenjeno
                                // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                                pomCena = Convert.ToDouble(dr["Cena"].ToString());
                                pomkolicina = 1;


                            }

                            con.Close();
                            pomPlatilac = Convert.ToInt32(cboNalogodavac3.SelectedValue);
                            pomOrgJed = VratiOrgJed(pomManupulacija);
                            foreach (DataGridViewRow row2 in dataGridView1.Rows)
                            {
                                if (row2.Selected)
                                {
                                    pomID = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                                    UbaciStavkuUsluge(pomID, pomManupulacija, pomCena, pomkolicina, pomOrgJed, pomPlatilac, pomPokret, pomStatusKontejnera, pomForma);
                                }
                            }
                        }

                        //  pomCena = Convert.ToDouble(row.Cells[2].Value.ToString());
                        // pomkolicina= Convert.ToDouble(row.Cells[4].Value.ToString());
                        //   pomOrgJed = Convert.ToInt32(row.Cells[5].Value.ToString());
                       


                    }
                }


            }
            catch
            {
                MessageBox.Show("Unos nije uspeo.Proverite da li imate definisanu cenu u Cenovniku!!!");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int pom = 0;
            InsertUvozKonacna uvK = new InsertUvozKonacna();

            try
            {

                foreach (DataGridViewRow row2 in dataGridView7.Rows)
                {
                    if (row2.Selected)
                    {
                        if (txtNadredjeni.Text != "0")
                        {
                            pom = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                            uvK.DelUvozKonacnaUsluga(pom);
                        }
                        else
                        {
                            pom = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                            uvK.DelUvozUsluga(pom);
                        }
                    }
                }
                FillDG8();
            }
            catch
            {
                MessageBox.Show("Nije uspelo brisanje");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int pom = 0;
            InsertUvozKonacna uvK = new InsertUvozKonacna();

            try
            {

                foreach (DataGridViewRow row2 in dataGridView7.Rows)
                {
                    if (row2.Selected)
                    {
                        if (txtNadredjeni.Text != "0")
                        {
                            pom = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                            uvK.UpdPlatiocaUvozKonacnaUsluga(pom, Convert.ToInt32(cboUvoznik.SelectedValue));
                        }
                        else
                        {
                            pom = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                            uvK.UpdPlatiocaUvozUsluga(pom, Convert.ToInt32(cboUvoznik.SelectedValue));
                        }
                    }
                }
                FillDG8();
            }
            catch
            {
                MessageBox.Show("Nije uspelo brisanje");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int pomID = 0;
            int pomManupulacija = 0;
            double pomCena = 0;
            double pomkolicina = 1;
            int pomPlatilac = 0;
            string pomPokret = "";
            int pomStatusKontejnera = 0;
            string pomForma = "";
            try
            {
                foreach (DataGridViewRow row in dataGridView6.Rows)
                {
                    if (row.Selected == true)
                    {
                        pomManupulacija = Convert.ToInt32(row.Cells[0].Value.ToString());
                        pomPokret = row.Cells[7].Value.ToString();
                        pomStatusKontejnera = Convert.ToInt32(row.Cells[8].Value.ToString());
                        pomForma = row.Cells[10].Value.ToString();
                        var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                        SqlConnection con = new SqlConnection(s_connection);

                        con.Open();

                        SqlCommand cmd = new SqlCommand("select ID, Cena, OrgJed from Cene where TipCenovnika =1 and VrstaManipulacije = " + pomManupulacija, con);
                        SqlDataReader dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            //Izmenjeno
                            // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                            pomCena = Convert.ToDouble(dr["Cena"].ToString());
                            pomkolicina = 1;
                            // pomOrgJed = Convert.ToInt32(dr["OrgJed"].ToString());

                            // Nece se analizirati podrazumevano da nije cekirano
                            // pomSaPDV = Convert.ToInt32(dr["SaPDV"].ToString());
                        }

                        con.Close();

                        pomOrgJed = VratiOrgJed(pomManupulacija);
                        pomPlatilac = Convert.ToInt32(cboUvoznik.SelectedValue);



                        //  pomCena = Convert.ToDouble(row.Cells[2].Value.ToString());
                        // pomkolicina= Convert.ToDouble(row.Cells[4].Value.ToString());
                        //   pomOrgJed = Convert.ToInt32(row.Cells[5].Value.ToString());
                        foreach (DataGridViewRow row2 in dataGridView1.Rows)
                        {
                            if (row2.Selected == true)
                            {
                                pomID = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                                UbaciStavkuUsluge(pomID, pomManupulacija, pomCena, pomkolicina, pomOrgJed, pomPlatilac, pomPokret, pomStatusKontejnera, pomForma);
                            }
                        }


                    }
                    
                }
                FillDG8();

            }
            catch
            {
                MessageBox.Show("Unos nije uspeo.Proverite da li imate definisanu cenu u Cenovniku!!!");
            }
        }

        int VratiOrgJed(int Manipulacija)
        {
            int pomOJ = 0;
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select OrgJed from VrstaManipulacije  where ID= " + Manipulacija, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                //Izmenjeno
                // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                pomOJ = Convert.ToInt32(dr["OrgJed"].ToString());
            }
            con.Close();
            return pomOJ;

        }

        private void button14_Click(object sender, EventArgs e)
        {
            FillDG6(1);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            FillDG6(0);
        }

        private void dataGridView7_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (Usao == 1)
            {

                int columnIndex = dataGridView7.CurrentCell.ColumnIndex;
                int rowIndex = dataGridView7.CurrentCell.RowIndex;



                if (columnIndex == 6)
                {
                    string value = dataGridView7.Rows[rowIndex].Cells[0].EditedFormattedValue.ToString();
                    string value1 = dataGridView7.Rows[rowIndex].Cells[6].EditedFormattedValue.ToString();
                    string value2 = dataGridView7.Rows[rowIndex].Cells[6].Value.ToString();
                    if (txtNadredjeni.Text != "0")
                    {
                        InsertUvoz uvK = new InsertUvoz();
                        uvK.UpdCENAUvozKonacnaUsluga(Convert.ToInt32(value), Convert.ToDouble(value1));
                        // FillDG8();
                    }
                    else
                    {
                        InsertUvoz uvK = new InsertUvoz();
                        uvK.UpdCENAUvozUsluga(Convert.ToInt32(value), Convert.ToDouble(value1));
                        //FillDG8();

                    }


                }

            }
        }

        private void dataGridView7_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Usao == 1)
            {
                int columnIndex = dataGridView7.CurrentCell.ColumnIndex;
                int rowIndex = dataGridView7.CurrentCell.RowIndex;
                int tmp = 0;

                if (columnIndex == 11)
                {
                    string value = dataGridView7.Rows[rowIndex].Cells[0].Value.ToString();
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dataGridView7.Rows[rowIndex].Cells[11];
                    if (chk.EditedFormattedValue.ToString() == "True")
                    {
                        chk.Selected = false;
                        tmp = 0;
                        //Update PDV
                    }
                    else
                    {
                        chk.Selected = true;
                        tmp = 1;
                        //Update PDV
                    }

                    if (txtNadredjeni.Text != "0")
                    {
                        InsertUvoz uvK = new InsertUvoz();
                        uvK.UpdPDVUvozKonacnaUsluga(Convert.ToInt32(value), tmp);
                        // FillDG8();
                    }
                    else
                    {
                        InsertUvoz uvK = new InsertUvoz();
                        uvK.UpdPDVUvozUsluga(Convert.ToInt32(value), tmp);
                        // FillDG8();

                    }

                    // string value1 = dataGridView7.Rows[rowIndex].Cells[6].EditedFormattedValue.ToString();
                    // string value2 = dataGridView7.Rows[rowIndex].Cells[6].Value.ToString();
                    //Promeni cenu
                }


            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (txtNadredjeni.Text == "0")
            {
                FillGVUvozSVI();
            }
            else
            {
                FillGVUvozKonacnaPoPlanuSVI();

            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (txtNadredjeni.Text == "0")
            {
                FillGVUvoz();
            }
            else
            {
                FillGVUvozKonacnaPoPlanu();

            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            FillDG6Scenario();
            dataGridView6.SelectAll();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            int UslugaKamion = 0;
            try
            {

                foreach (DataGridViewRow row2 in dataGridView7.Rows)
                {
                    if (row2.Selected)
                    {
                        UslugaKamion = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta

                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspelo brisanje");
            }


            frmVoziloUsluga vu = new frmVoziloUsluga(UslugaKamion, 0);
            vu.Show();
        }
    }
}
