using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Saobracaj.Sifarnici;

namespace Saobracaj.Izvoz
{
    public partial class frmIzvozUnosManipulacije : Syncfusion.Windows.Forms.Office2010Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        int pIDPlana = 0;
        int pID = 0;
        int pNalogodavac1 = 0;
        int pNalogodavac2 = 0;
        int pNalogodavac3 = 0;
        int pIzvoznik = 0;
        int pomOrgJed = 0;
        int Usao = 0;
        public frmIzvozUnosManipulacije()
        {
            InitializeComponent();
            FillDG6(1);
            int Usao = 0;
        }

        public frmIzvozUnosManipulacije(int IDPlana, int ID, int Nalogodavac1, int Nalogodavac2, int Nalogodavac3, int Izvoznik)
        {
            InitializeComponent();
            pIDPlana = IDPlana;
            pID = ID;
            pNalogodavac1 = Nalogodavac1;
            pNalogodavac2 = Nalogodavac2;
            pNalogodavac3 = Nalogodavac3;
            pIzvoznik = Izvoznik;
            txtNadredjeni.Text = pIDPlana.ToString();
            FillDG6(1);
            FillDG8();
            int Usao = 0;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            FillDG6(1);
        }

        private void FillDG6(int TipUsluge)
        {
            var select = "";

            if (TipUsluge == 1)
            {
                select = " SELECT VrstaManipulacije.[ID]      ,VrstaManipulacije.[Naziv]    " +
     " , VrstaManipulacije.[JM]           ,VrstaManipulacije.[JM2] " +
    " ,VrstaManipulacije.[TipManipulacije]      ,VrstaManipulacije.[OrgJed]      ,[Oznaka]      ,[Relacija],OrganizacioneJedinice.Naziv as OJ " +
   " ,VrstaManipulacije.[Cena] ,VrstaManipulacije.[Datum] ,VrstaManipulacije.[Korisnik] FROM[VrstaManipulacije] " +
   " inner join OrganizacioneJedinice on VrstaManipulacije.OrgJed = OrganizacioneJedinice.ID" +
   " where Administrativna = 0 ";
   }
            else if (TipUsluge == 2)
            {
                //Po grupi Manipulacije
                select = "SELECT VrstaManipulacije.[ID]      ,VrstaManipulacije.[Naziv] ,  " +
      " VrstaManipulacije.[JM] " +
     " ,VrstaManipulacije.[TipManipulacije]      ,VrstaManipulacije.[OrgJed]      ,OrganizacioneJedinice.Naziv as OJ " +
     " ,VrstaManipulacije.[Cena] ,'',0, '/',  VrstaManipulacije.[Datum] ,VrstaManipulacije.[Korisnik] FROM [VrstaManipulacije] " +
     "  inner join OrganizacioneJedinice on VrstaManipulacije.OrgJed = OrganizacioneJedinice.ID where VrstaManipulacije.GrupaVrsteManipulacijeID = " + Convert.ToInt32(cboGrupaVrsteManipulacije.SelectedValue) + "  order by VrstaManipulacije.[Naziv] ";

            }

            else if (TipUsluge == 3)
            {
                //Po grupi Manipulacije
               select = "SELECT VrstaManipulacije.[ID]      ,VrstaManipulacije.[Naziv] ,  " +
      " VrstaManipulacije.[JM] " +
     " ,VrstaManipulacije.[TipManipulacije]      ,VrstaManipulacije.[OrgJed]      ,OrganizacioneJedinice.Naziv as OJ " +
     " ,VrstaManipulacije.[Cena] ,'',0, '/',  VrstaManipulacije.[Datum] ,VrstaManipulacije.[Korisnik] FROM [VrstaManipulacije] " +
     "  inner join OrganizacioneJedinice on VrstaManipulacije.OrgJed = OrganizacioneJedinice.ID where VrstaManipulacije.Dodatna = 1  order by VrstaManipulacije.[Naziv] ";

            }
            else
            {
                select = " SELECT VrstaManipulacije.[ID]      ,VrstaManipulacije.[Naziv]    " +
                " , VrstaManipulacije.[JM]           ,VrstaManipulacije.[JM2] " +
               " ,VrstaManipulacije.[TipManipulacije]      ,VrstaManipulacije.[OrgJed]      ,[Oznaka]      ,[Relacija],OrganizacioneJedinice.Naziv as OJ " +
              " ,VrstaManipulacije.[Cena] ,VrstaManipulacije.[Datum] ,VrstaManipulacije.[Korisnik] FROM[VrstaManipulacije] " +
              " inner join OrganizacioneJedinice on VrstaManipulacije.OrgJed = OrganizacioneJedinice.ID " +
              " where ADministrativna = 1 ";
            }
            //Opsti cenovnik pozivanje
          
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
            dataGridView6.Columns[3].HeaderText = "JM2";
            dataGridView6.Columns[3].Width = 50;
            dataGridView6.Columns[3].Visible = false;


            DataGridViewColumn column5 = dataGridView6.Columns[4];
            dataGridView6.Columns[4].HeaderText = "Tip manipulacije";
            dataGridView6.Columns[4].Width = 50;

            DataGridViewColumn column6 = dataGridView6.Columns[5];
            dataGridView6.Columns[5].HeaderText = "Org jed";
            dataGridView6.Columns[5].Width = 50;

            DataGridViewColumn column7 = dataGridView6.Columns[6];
            dataGridView6.Columns[6].HeaderText = "Oznaka";
            dataGridView6.Columns[6].Width = 50;

            DataGridViewColumn column8 = dataGridView6.Columns[7];
            dataGridView6.Columns[7].HeaderText = "Relacija";
            dataGridView6.Columns[7].Width = 150;

            DataGridViewColumn column9 = dataGridView6.Columns[8];
            dataGridView6.Columns[8].HeaderText = "Cena";
            dataGridView6.Columns[8].Width = 100;

        }

        private void button13_Click(object sender, EventArgs e)
        {
            FillDN1();
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

        private void button3_Click(object sender, EventArgs e)
        {
            FillDN1BU();
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

        private void button1_Click(object sender, EventArgs e)
        {
            FillDGN2();
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

        private void button4_Click(object sender, EventArgs e)
        {
            FillDGN2BU();
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

        private void button2_Click(object sender, EventArgs e)
        {
            FillDGN3();
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

        private void button5_Click(object sender, EventArgs e)
        {
            FillDGN3BU();
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
        private void UbaciStavkuUsluge(int ID, int Manipulacija, double Cena, double Kolicina, int OrgJed, int Platilac, string PomPokret, int PomStatusKOntejnera, string PomForma)
        {
            if (txtNadredjeni.Text != "0")
            {
                InsertIzvoz uvK = new InsertIzvoz();
                uvK.InsUbaciUsluguKonacna(ID, Manipulacija, Cena, Kolicina, OrgJed, Platilac, 0, PomPokret, PomStatusKOntejnera, PomForma);
                FillDG8();
            }
            else
            {
                InsertIzvoz uvK = new InsertIzvoz();
                uvK.InsUbaciUslugu(ID, Manipulacija, Cena, Kolicina, OrgJed, Platilac, 0, PomPokret, PomStatusKOntejnera, PomForma);
                FillDG8();

            }


        }

        private void FillDG8()
        {
            //Mora count
            var select = "";
            if (txtNadredjeni.Text == "0")
            {
                select = "  select  IzvozVrstaManipulacije.ID as ID, IzvozVrstaManipulacije.IDNadredjena as KontejnerID, Izvoz.BrojKontejnera, " +
  " IzvozVrstaManipulacije.Kolicina,  VrstaManipulacije.ID as ManipulacijaID,VrstaManipulacije.Naziv as ManipulacijaNaziv,  " +
 "  IzvozVrstaManipulacije.Cena,OrganizacioneJedinice.ID as OJID, OrganizacioneJedinice.Naziv as OrganizacionaJedinica,  " +
 "  Partnerji.PaSifra as PlatilacID,PArtnerji.PaNaziv as Platilac, SaPDV, IzvozVrstaManipulacije.Pokret, KontejnerStatus.Naziv as StatusKontejnera, IzvozVrstaManipulacije.Forma " +
 "  from IzvozVrstaManipulacije " +
 " inner join VrstaManipulacije on VrstaManipulacije.ID = IDVrstaManipulacije " +
 " inner " +
  " join PArtnerji on PArtnerji.PaSifra = IzvozVrstaManipulacije.Platilac " +
" inner " +
"   join OrganizacioneJedinice on OrganizacioneJedinice.ID = IzvozVrstaManipulacije.OrgJed " +
" inner " +
 "  join Izvoz on IzvozVrstaManipulacije.IDNadredjena = Izvoz.ID" +
" inner " +
" join KontejnerStatus on IzvozVrstaManipulacije.StatusKontejnera = KontejnerStatus.ID where IzvozVrstaManipulacije.IDNadredjena =  " + pID; 


            }
            else
            {
                select = "  select IzvozKonacnaVrstaManipulacije.ID as ID, IzvozKonacnaVrstaManipulacije.IDNadredjena as KontejnerID, IzvozKonacna.BrojKontejnera, " +
 " IzvozKonacnaVrstaManipulacije.Kolicina,  VrstaManipulacije.ID as ManipulacijaID,VrstaManipulacije.Naziv as ManipulacijaNaziv,   " +
 " IzvozKonacnaVrstaManipulacije.Cena,OrganizacioneJedinice.ID, OrganizacioneJedinice.Naziv as OrganizacionaJedinica,   " +
 " Partnerji.PaSifra as PlatilacID,PArtnerji.PaNaziv as Platilac,SaPDV,IzvozKonacnaVrstaManipulacije.Pokret, " +
 " KontejnerStatus.Naziv as StatusKontejnera,IzvozKonacnaVrstaManipulacije.Forma  from IzvozKonacnaVrstaManipulacije " +
 "  inner join IzvozKonacna on IzvozKonacnaVrstaManipulacije.IDNadredjena = IzvozKonacna.ID " +
 " inner   join VrstaManipulacije on VrstaManipulacije.ID = IDVrstaManipulacije    " +
 " inner   join PArtnerji on PArtnerji.PaSifra = IzvozKonacnaVrstaManipulacije.Platilac " +
" inner   join OrganizacioneJedinice on OrganizacioneJedinice.ID = IzvozKonacnaVrstaManipulacije.OrgJed " +
" inner    join KontejnerStatus on IzvozKonacnaVrstaManipulacije.StatusKontejnera = KontejnerStatus.ID " +
" where IzvozKonacna.IDNadredjena = " + Convert.ToInt32(txtNadredjeni.Text) + " Order by IzvozKonacnaVrstaManipulacije.ID asc";
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
            dataGridView7.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView7.Columns[1];
            dataGridView7.Columns[1].HeaderText = "KontID";
            dataGridView7.Columns[1].Width = 30;

            DataGridViewColumn column3 = dataGridView7.Columns[2];
            dataGridView7.Columns[2].HeaderText = "Kontejner";
            dataGridView7.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView7.Columns[3];
            dataGridView7.Columns[3].HeaderText = "Kolicina";
            dataGridView7.Columns[3].Width = 50;

            DataGridViewColumn column5 = dataGridView7.Columns[4];
            dataGridView7.Columns[4].HeaderText = "USLID";
            dataGridView7.Columns[4].Width = 50;


            DataGridViewColumn column6 = dataGridView7.Columns[5];
            dataGridView7.Columns[5].HeaderText = "Usluga";
            dataGridView7.Columns[5].Width = 220;

            DataGridViewColumn column7 = dataGridView7.Columns[6];
            dataGridView7.Columns[6].HeaderText = "Cena";
            dataGridView7.Columns[6].Width = 80;


            DataGridViewColumn column8 = dataGridView7.Columns[7];
            dataGridView7.Columns[7].HeaderText = "OJID";
            dataGridView7.Columns[7].Width = 50;

            DataGridViewColumn column9 = dataGridView7.Columns[8];
            dataGridView7.Columns[8].HeaderText = "OJNaziv";
            dataGridView7.Columns[8].Width = 50;

            DataGridViewColumn column10 = dataGridView7.Columns[9];
            dataGridView7.Columns[9].HeaderText = "PlatilacID";
            dataGridView7.Columns[9].Width = 50;

            DataGridViewColumn column11 = dataGridView7.Columns[10];
            dataGridView7.Columns[10].HeaderText = "Platilac";
            dataGridView7.Columns[10].Width = 150;

            DataGridViewColumn column12 = dataGridView7.Columns[11];
            dataGridView7.Columns[11].HeaderText = "SaPDV";
            dataGridView7.Columns[11].Width = 50;

            DataGridViewColumn column13 = dataGridView7.Columns[12];
            dataGridView7.Columns[12].HeaderText = "Pokret";
            dataGridView7.Columns[12].Width = 100;

            DataGridViewColumn column14 = dataGridView7.Columns[13];
            dataGridView7.Columns[13].HeaderText = "Status";
            dataGridView7.Columns[13].Width = 100;

            DataGridViewColumn column15 = dataGridView7.Columns[14];
            dataGridView7.Columns[14].HeaderText = "Forma";
            dataGridView7.Columns[14].Width = 100;

        }

        int PretraziPoNalogodavciIIzvozniku(int PomManipulacija, int Nalogodavac, int Uvoznik)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
            int pomStatusKontejnera = 0;
            string pomPokret = "";
            string pomForma = "";

            try
            {
                foreach (DataGridViewRow row in dataGridView6.Rows)
                {
                    if (row.Selected)
                    {
                        pomManupulacija = Convert.ToInt32(row.Cells[0].Value.ToString());
                        int Adm = ProveriAdministrativna(pomManupulacija);
                        if (Adm == 0)
                        {
                            pomPokret = row.Cells[7].Value.ToString();
                            pomStatusKontejnera = Convert.ToInt32(row.Cells[8].Value.ToString());
                            pomForma = row.Cells[10].Value.ToString();

                        }
                        else
                        {
                            pomPokret = "/";
                            pomStatusKontejnera = Convert.ToInt32(0);
                            pomForma = "/";
                        }
                        CenaNadjenaTip1 = PretraziPoNalogodavciIIzvozniku(pomManupulacija, Convert.ToInt32(cboNalogodavac1.SelectedValue), Convert.ToInt32(cboUvoznik.SelectedValue));
                        CenaNadjenaTip2 = PretraziPoNalogodavci(pomManupulacija, Convert.ToInt32(cboNalogodavac1.SelectedValue), Convert.ToInt32(cboUvoznik.SelectedValue));


                        if (CenaNadjenaTip1 == 1)
                        {
                            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                            SqlConnection con = new SqlConnection(s_connection);

                            con.Open();

                            SqlCommand cmd = new SqlCommand("select ID, Cena, OrgJed from Cene where VrstaManipulacije = " + pomManupulacija + " Uvoznik = " + cboUvoznik.SelectedValue + " and  Komitent = " + Convert.ToInt32(cboNalogodavac1.SelectedValue) + "", con);
                            SqlDataReader dr = cmd.ExecuteReader();

                            while (dr.Read())
                            {

                                pomCena = Convert.ToDouble(dr["Cena"].ToString());
                                pomkolicina = 1;
                                pomOrgJed = Convert.ToInt32(dr["OrgJed"].ToString());

                            }

                            con.Close();
                            pomPlatilac = Convert.ToInt32(cboNalogodavac1.SelectedValue);

                        }

                        if (CenaNadjenaTip2 == 1 && CenaNadjenaTip2 == 0)
                        {
                            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
                                pomOrgJed = Convert.ToInt32(dr["OrgJed"].ToString());

                            }

                            con.Close();

                            pomPlatilac = Convert.ToInt32(cboNalogodavac1.SelectedValue);
                        }

                        if (CenaNadjenaTip2 == 0 && CenaNadjenaTip2 == 0)
                        {
                            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
                                pomOrgJed = Convert.ToInt32(dr["OrgJed"].ToString());

                            }

                            con.Close();
                            pomPlatilac = Convert.ToInt32(cboNalogodavac1.SelectedValue);;

                        }
                        pomOrgJed = VratiOrgJed(pomManupulacija);
                        //  pomCena = Convert.ToDouble(row.Cells[2].Value.ToString());
                        // pomkolicina= Convert.ToDouble(row.Cells[4].Value.ToString());
                        //   pomOrgJed = Convert.ToInt32(row.Cells[5].Value.ToString());
                        foreach (DataGridViewRow row2 in dataGridView1.Rows)
                        {
                            if (row2.Selected)
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
                MessageBox.Show("Nije uspela selekcija stavki");
            }
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

        private void FillGVIzvoz()
        {
            var select = " SELECT  Izvoz.ID as ID,  Izvoz.BrojKontejnera,  Izvoz.VrstaKontejnera as Vrk_ID, TipKontenjera.Naziv as VrstaKontejnera, Partnerji.PaNaziv as Brodar, Izvoz.BookingBrodara, " +
 " Izvoz.BrojVagona,   Izvoz.CutOffPort,Partnerji_2.PaNaziv AS Izvoznik,Partnerji_3.PaNaziv AS Nalogodavac1, Partnerji_4.PaNaziv AS kNalogodavac2, Partnerji_5.PaNaziv AS Nalogodavac3, " +
 " Izvoz.DobijenNalogKlijent1, Izvoz.BrodskaPlomba, Izvoz.OstalePlombe,  " +
 " Izvoz.NetoRobe, Izvoz.BrutoRobe, Izvoz.BrutoRobeO, Izvoz.BrojKoleta, Izvoz.BrojKoletaO, Izvoz.CBM, Izvoz.CBMO, Izvoz.VrednostRobeFaktura,  " +
 " (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaManipulacije.Naziv as nvarchar(20))   FROM IzvozVrstaManipulacije " +
 " inner join VrstaManipulacije on VrstaManipulacije.ID = IzvozVrstaManipulacije.IDVrstaManipulacije   where IzvozVrstaManipulacije.IDNadredjena = Izvoz.ID" +
 " FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as VrsteUsluga,        (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaRobeHS.HSKod as nvarchar(20)) " +
 " FROM IzvozVrstaRobeHS        inner join VrstaRobeHS on IzvozVrstaRobeHS.IDVrstaRobeHS = VrstaRobeHS.ID   where IzvozVrstaRobeHS.IDNadredjena = Izvoz.ID " +
 " FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as HS,       (SELECT  STUFF((SELECT distinct    '/' + Cast(NHM.Broj as nvarchar(20)) " +
 " FROM IzvozNHM  inner join NHM on IzvozNHM.IDNHM = NHM.ID  where IzvozNHM.IDNadredjena = Izvoz.ID   FOR XML PATH('')), 1, 1, ''  ) As Skupljen) as NHM,  " +
 " Izvoz.Valuta, KrajnjaDestinacija.Naziv AS KrajnjaDestinacija, VrstePostupakaUvoz.Naziv AS Postupak, KontejnerskiTerminali.Naziv AS PPCNT, " +
 " KontejnerskiTerminali.Oznaka, Izvoz.Cirada, Izvoz.PlaniraniDatumUtovara, MestaUtovara.Naziv AS MestoUtovara, Izvoz.KontaktOsoba,  " +
 " Carinarnice.Naziv AS Carinarnica, Carinarnice.CIOznaka, Partnerji_1.PaNaziv AS Spedicija,  AdresaSlanjaStatusa,    " +
 " NaslovSlanjaStatusa, Izvoz.EtaLeget, VrstaCarinskogPostupka.Naziv AS Reexport, InspekciskiTretman.Naziv AS InspekciskiTretman,   " +
 " Izvoz.AutoDana, Izvoz.NajavaVozila, Izvoz.DodatneNapomeneDrumski, Izvoz.Vaganje, Izvoz.VGMTezina, Izvoz.Tara, Izvoz.VGMBrod,   " +
 "   Izvoz.Napomena1REf, " +
 " Izvoz.Napomena2REf, Izvoz.Napomena3REf, Partnerji_6.PaNaziv AS SpediterRijeka, uvNacinPakovanja.Naziv AS NacinPakovanja,  " +
 " Izvoz.NacinPretovara FROM         Izvoz Left JOIN TipKontenjera ON Izvoz.VrstaKontejnera = TipKontenjera.ID LEFT JOIN " +
 " Partnerji ON Izvoz.Brodar = Partnerji.PaSifra LEFT JOIN         KrajnjaDestinacija ON Izvoz.KrajnaDestinacija = KrajnjaDestinacija.ID LEFT JOIN " +
 " VrstePostupakaUvoz ON Izvoz.Postupanje = VrstePostupakaUvoz.ID LEFT JOIN         KontejnerskiTerminali ON Izvoz.MestoPreuzimanja = KontejnerskiTerminali.id " +
 " LEFT JOIN " +
 " MestaUtovara ON Izvoz.MesoUtovara = MestaUtovara.ID " +
 " LEFT JOIN         Carinarnice ON Izvoz.MestoCarinjenja = Carinarnice.ID " +
 " LEFT JOIN        Partnerji AS Partnerji_1 ON Izvoz.Spedicija = Partnerji_1.PaSifra " +
 " LEFT JOIN         VrstaCarinskogPostupka ON Izvoz.NapomenaReexport = VrstaCarinskogPostupka.id " +
 " LEFT JOIN        InspekciskiTretman ON Izvoz.Inspekcija = InspekciskiTretman.ID " +
 " LEFT JOIN        Partnerji AS Partnerji_2 ON Izvoz.Izvoznik = Partnerji_2.PaSifra " +
 " LEFT JOIN        Partnerji AS Partnerji_3 ON Izvoz.Klijent1 = Partnerji_3.PaSifra " +
 " LEFT JOIN       Partnerji AS Partnerji_4 ON Izvoz.Klijent2 = Partnerji_4.PaSifra " +
 " LEFT JOIN         Partnerji AS Partnerji_5 ON Izvoz.Klijent3 = Partnerji_5.PaSifra LEFT JOIN " +
 " Partnerji AS Partnerji_6 ON Izvoz.SpediterRijeka = Partnerji_6.PaSifra " +
 " LEFT JOIN         uvNacinPakovanja ON Izvoz.NacinPakovanja = uvNacinPakovanja.ID where Izvoz.ID = " + pID + "order by Izvoz.ID desc  ";

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

          


        }

        private void FillGVIzvozKonacnaPoPlanu()
        {
            var select = " SELECT  IzvozKonacna.ID as ID,  IzvozKonacna.BrojKontejnera,  IzvozKonacna.VrstaKontejnera as Vrk_ID, TipKontenjera.Naziv as VrstaKontejnera, Partnerji.PaNaziv as Brodar, IzvozKonacna.BookingBrodara, " +
" IzvozKonacna.BrojVagona,   IzvozKonacna.CutOffPort,Partnerji_2.PaNaziv AS Izvoznik,Partnerji_3.PaNaziv AS Nalogodavac1, Partnerji_4.PaNaziv AS Nalogodavac2, Partnerji_5.PaNaziv AS Nalogodavac3, " +
" IzvozKonacna.DobijenNalogKlijent1, IzvozKonacna.BrodskaPlomba, IzvozKonacna.OstalePlombe,  " +
" IzvozKonacna.NetoRobe, IzvozKonacna.BrutoRobe, IzvozKonacna.BrutoRobeO, IzvozKonacna.BrojKoleta, IzvozKonacna.BrojKoletaO, IzvozKonacna.CBM, IzvozKonacna.CBMO, IzvozKonacna.VrednostRobeFaktura,  " +
" (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaManipulacije.Naziv as nvarchar(20))   FROM IzvozKonacnaVrstaManipulacije " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = IzvozKonacnaVrstaManipulacije.IDVrstaManipulacije   where IzvozKonacnaVrstaManipulacije.IDNadredjena = IzvozKonacna.ID" +
" FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as VrsteUsluga,        (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaRobeHS.HSKod as nvarchar(20)) " +
" FROM IzvozKonacnaVrstaRobeHS        inner join VrstaRobeHS on IzvozKonacnaVrstaRobeHS.IDVrstaRobeHS = VrstaRobeHS.ID   where IzvozKonacnaVrstaRobeHS.IDNadredjena = IzvozKonacna.ID " +
" FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as HS,       (SELECT  STUFF((SELECT distinct    '/' + Cast(NHM.Broj as nvarchar(20)) " +
" FROM IzvozKonacnaNHM  inner join NHM on IzvozKonacnaNHM.IDNHM = NHM.ID  where IzvozKonacnaNHM.IDNadredjena = IzvozKonacna.ID   FOR XML PATH('')), 1, 1, ''  ) As Skupljen) as NHM,  " +
" IzvozKonacna.Valuta, KrajnjaDestinacija.Naziv AS KrajnjaDestinacija, VrstePostupakaUvoz.Naziv AS Postupak, KontejnerskiTerminali.Naziv AS PPCNT, " +
" KontejnerskiTerminali.Oznaka, IzvozKonacna.Cirada, IzvozKonacna.PlaniraniDatumUtovara, MestaUtovara.Naziv AS MestoUtovara, IzvozKonacna.KontaktOsoba,  " +
" Carinarnice.Naziv AS Carinarnica, Carinarnice.CIOznaka, Partnerji_1.PaNaziv AS Spedicija,  AdresaSlanjaStatusa,    " +
" NaslovSlanjaStatusa, IzvozKonacna.EtaLeget, VrstaCarinskogPostupka.Naziv AS Reexport, InspekciskiTretman.Naziv AS InspekciskiTretman,   " +
" IzvozKonacna.AutoDana, IzvozKonacna.NajavaVozila, IzvozKonacna.DodatneNapomeneDrumski, IzvozKonacna.Vaganje, IzvozKonacna.VGMTezina, IzvozKonacna.Tara, IzvozKonacna.VGMBrod,   " +
"   IzvozKonacna.Napomena1REf, " +
" IzvozKonacna.Napomena2REf, IzvozKonacna.Napomena3REf, Partnerji_6.PaNaziv AS SpediterRijeka, uvNacinPakovanja.Naziv AS NacinPakovanja,  " +
" IzvozKonacna.NacinPretovara FROM         IzvozKonacna Left JOIN TipKontenjera ON IzvozKonacna.VrstaKontejnera = TipKontenjera.ID LEFT JOIN " +
" Partnerji ON IzvozKonacna.Brodar = Partnerji.PaSifra LEFT JOIN         KrajnjaDestinacija ON IzvozKonacna.KrajnaDestinacija = KrajnjaDestinacija.ID LEFT JOIN " +
" VrstePostupakaUvoz ON IzvozKonacna.Postupanje = VrstePostupakaUvoz.ID LEFT JOIN         KontejnerskiTerminali ON IzvozKonacna.MestoPreuzimanja = KontejnerskiTerminali.id " +
" LEFT JOIN " +
" MestaUtovara ON IzvozKonacna.MesoUtovara = MestaUtovara.ID " +
" LEFT JOIN         Carinarnice ON IzvozKonacna.MestoCarinjenja = Carinarnice.ID " +
" LEFT JOIN        Partnerji AS Partnerji_1 ON IzvozKonacna.Spedicija = Partnerji_1.PaSifra " +
" LEFT JOIN         VrstaCarinskogPostupka ON IzvozKonacna.NapomenaReexport = VrstaCarinskogPostupka.id " +
" LEFT JOIN        InspekciskiTretman ON IzvozKonacna.Inspekcija = InspekciskiTretman.ID " +
" LEFT JOIN        Partnerji AS Partnerji_2 ON IzvozKonacna.Izvoznik = Partnerji_2.PaSifra " +
" LEFT JOIN        Partnerji AS Partnerji_3 ON IzvozKonacna.Klijent1 = Partnerji_3.PaSifra " +
" LEFT JOIN       Partnerji AS Partnerji_4 ON IzvozKonacna.Klijent2 = Partnerji_4.PaSifra " +
" LEFT JOIN         Partnerji AS Partnerji_5 ON IzvozKonacna.Klijent3 = Partnerji_5.PaSifra LEFT JOIN " +
" Partnerji AS Partnerji_6 ON IzvozKonacna.SpediterRijeka = Partnerji_6.PaSifra " +
" LEFT JOIN         uvNacinPakovanja ON IzvozKonacna.NacinPakovanja = uvNacinPakovanja.ID where IzvozKonacna.ID = " + pID + " order by IzvozKonacna.ID desc  ";
          //  " where IzvozKonacna.IdNadredjena = " + Convert.ToInt32(txtNadredjeni.Text) + " order by IzvozKonacna.ID desc";


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
          

        }


        private void FillGVIzvozSVI()
        {
            var select = " SELECT  Izvoz.ID as ID,  Izvoz.BrojKontejnera,  Izvoz.VrstaKontejnera as Vrk_ID, TipKontenjera.Naziv as VrstaKontejnera, Partnerji.PaNaziv as Brodar, Izvoz.BookingBrodara, " +
 " Izvoz.BrojVagona,   Izvoz.CutOffPort,Partnerji_2.PaNaziv AS Izvoznik,Partnerji_3.PaNaziv AS Nalogodavac1, Partnerji_4.PaNaziv AS kNalogodavac2, Partnerji_5.PaNaziv AS Nalogodavac3, " +
 " Izvoz.DobijenNalogKlijent1, Izvoz.BrodskaPlomba, Izvoz.OstalePlombe,  " +
 " Izvoz.NetoRobe, Izvoz.BrutoRobe, Izvoz.BrutoRobeO, Izvoz.BrojKoleta, Izvoz.BrojKoletaO, Izvoz.CBM, Izvoz.CBMO, Izvoz.VrednostRobeFaktura,  " +
 " (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaManipulacije.Naziv as nvarchar(20))   FROM IzvozVrstaManipulacije " +
 " inner join VrstaManipulacije on VrstaManipulacije.ID = IzvozVrstaManipulacije.IDVrstaManipulacije   where IzvozVrstaManipulacije.IDNadredjena = Izvoz.ID" +
 " FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as VrsteUsluga,        (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaRobeHS.HSKod as nvarchar(20)) " +
 " FROM IzvozVrstaRobeHS        inner join VrstaRobeHS on IzvozVrstaRobeHS.IDVrstaRobeHS = VrstaRobeHS.ID   where IzvozVrstaRobeHS.IDNadredjena = Izvoz.ID " +
 " FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as HS,       (SELECT  STUFF((SELECT distinct    '/' + Cast(NHM.Broj as nvarchar(20)) " +
 " FROM IzvozNHM  inner join NHM on IzvozNHM.IDNHM = NHM.ID  where IzvozNHM.IDNadredjena = Izvoz.ID   FOR XML PATH('')), 1, 1, ''  ) As Skupljen) as NHM,  " +
 " Izvoz.Valuta, KrajnjaDestinacija.Naziv AS KrajnjaDestinacija, VrstePostupakaUvoz.Naziv AS Postupak, KontejnerskiTerminali.Naziv AS PPCNT, " +
 " KontejnerskiTerminali.Oznaka, Izvoz.Cirada, Izvoz.PlaniraniDatumUtovara, MestaUtovara.Naziv AS MestoUtovara, Izvoz.KontaktOsoba,  " +
 " Carinarnice.Naziv AS Carinarnica, Carinarnice.CIOznaka, Partnerji_1.PaNaziv AS Spedicija,  AdresaSlanjaStatusa,    " +
 " NaslovSlanjaStatusa, Izvoz.EtaLeget, VrstaCarinskogPostupka.Naziv AS Reexport, InspekciskiTretman.Naziv AS InspekciskiTretman,   " +
 " Izvoz.AutoDana, Izvoz.NajavaVozila, Izvoz.DodatneNapomeneDrumski, Izvoz.Vaganje, Izvoz.VGMTezina, Izvoz.Tara, Izvoz.VGMBrod,   " +
 "   Izvoz.Napomena1REf, " +
 " Izvoz.Napomena2REf, Izvoz.Napomena3REf, Partnerji_6.PaNaziv AS SpediterRijeka, uvNacinPakovanja.Naziv AS NacinPakovanja,  " +
 " Izvoz.NacinPretovara FROM         Izvoz Left JOIN TipKontenjera ON Izvoz.VrstaKontejnera = TipKontenjera.ID LEFT JOIN " +
 " Partnerji ON Izvoz.Brodar = Partnerji.PaSifra LEFT JOIN         KrajnjaDestinacija ON Izvoz.KrajnaDestinacija = KrajnjaDestinacija.ID LEFT JOIN " +
 " VrstePostupakaUvoz ON Izvoz.Postupanje = VrstePostupakaUvoz.ID LEFT JOIN         KontejnerskiTerminali ON Izvoz.MestoPreuzimanja = KontejnerskiTerminali.id " +
 " LEFT JOIN " +
 " MestaUtovara ON Izvoz.MesoUtovara = MestaUtovara.ID " +
 " LEFT JOIN         Carinarnice ON Izvoz.MestoCarinjenja = Carinarnice.ID " +
 " LEFT JOIN        Partnerji AS Partnerji_1 ON Izvoz.Spedicija = Partnerji_1.PaSifra " +
 " LEFT JOIN         VrstaCarinskogPostupka ON Izvoz.NapomenaReexport = VrstaCarinskogPostupka.id " +
 " LEFT JOIN        InspekciskiTretman ON Izvoz.Inspekcija = InspekciskiTretman.ID " +
 " LEFT JOIN        Partnerji AS Partnerji_2 ON Izvoz.Izvoznik = Partnerji_2.PaSifra " +
 " LEFT JOIN        Partnerji AS Partnerji_3 ON Izvoz.Klijent1 = Partnerji_3.PaSifra " +
 " LEFT JOIN       Partnerji AS Partnerji_4 ON Izvoz.Klijent2 = Partnerji_4.PaSifra " +
 " LEFT JOIN         Partnerji AS Partnerji_5 ON Izvoz.Klijent3 = Partnerji_5.PaSifra LEFT JOIN " +
 " Partnerji AS Partnerji_6 ON Izvoz.SpediterRijeka = Partnerji_6.PaSifra " +
 " LEFT JOIN         uvNacinPakovanja ON Izvoz.NacinPakovanja = uvNacinPakovanja.ID order by Izvoz.ID desc  ";

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




        }

        private void FillGVIzvozKonacnaPoPlanuSVI()
        {
            var select = " SELECT  IzvozKonacna.ID as ID,  IzvozKonacna.BrojKontejnera,  IzvozKonacna.VrstaKontejnera as Vrk_ID, TipKontenjera.Naziv as VrstaKontejnera, Partnerji.PaNaziv as Brodar, IzvozKonacna.BookingBrodara, " +
" IzvozKonacna.BrojVagona,   IzvozKonacna.CutOffPort,Partnerji_2.PaNaziv AS Izvoznik,Partnerji_3.PaNaziv AS Nalogodavac1, Partnerji_4.PaNaziv AS Nalogodavac2, Partnerji_5.PaNaziv AS Nalogodavac3, " +
" IzvozKonacna.DobijenNalogKlijent1, IzvozKonacna.BrodskaPlomba, IzvozKonacna.OstalePlombe,  " +
" IzvozKonacna.NetoRobe, IzvozKonacna.BrutoRobe, IzvozKonacna.BrutoRobeO, IzvozKonacna.BrojKoleta, IzvozKonacna.BrojKoletaO, IzvozKonacna.CBM, IzvozKonacna.CBMO, IzvozKonacna.VrednostRobeFaktura,  " +
" (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaManipulacije.Naziv as nvarchar(20))   FROM IzvozKonacnaVrstaManipulacije " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = IzvozKonacnaVrstaManipulacije.IDVrstaManipulacije   where IzvozKonacnaVrstaManipulacije.IDNadredjena = IzvozKonacna.ID" +
" FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as VrsteUsluga,        (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaRobeHS.HSKod as nvarchar(20)) " +
" FROM IzvozKonacnaVrstaRobeHS        inner join VrstaRobeHS on IzvozKonacnaVrstaRobeHS.IDVrstaRobeHS = VrstaRobeHS.ID   where IzvozKonacnaVrstaRobeHS.IDNadredjena = IzvozKonacna.ID " +
" FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as HS,       (SELECT  STUFF((SELECT distinct    '/' + Cast(NHM.Broj as nvarchar(20)) " +
" FROM IzvozKonacnaNHM  inner join NHM on IzvozKonacnaNHM.IDNHM = NHM.ID  where IzvozKonacnaNHM.IDNadredjena = IzvozKonacna.ID   FOR XML PATH('')), 1, 1, ''  ) As Skupljen) as NHM,  " +
" IzvozKonacna.Valuta, KrajnjaDestinacija.Naziv AS KrajnjaDestinacija, VrstePostupakaUvoz.Naziv AS Postupak, KontejnerskiTerminali.Naziv AS PPCNT, " +
" KontejnerskiTerminali.Oznaka, IzvozKonacna.Cirada, IzvozKonacna.PlaniraniDatumUtovara, MestaUtovara.Naziv AS MestoUtovara, IzvozKonacna.KontaktOsoba,  " +
" Carinarnice.Naziv AS Carinarnica, Carinarnice.CIOznaka, Partnerji_1.PaNaziv AS Spedicija,  AdresaSlanjaStatusa,    " +
" NaslovSlanjaStatusa, IzvozKonacna.EtaLeget, VrstaCarinskogPostupka.Naziv AS Reexport, InspekciskiTretman.Naziv AS InspekciskiTretman,   " +
" IzvozKonacna.AutoDana, IzvozKonacna.NajavaVozila, IzvozKonacna.DodatneNapomeneDrumski, IzvozKonacna.Vaganje, IzvozKonacna.VGMTezina, IzvozKonacna.Tara, IzvozKonacna.VGMBrod,   " +
"   IzvozKonacna.Napomena1REf, " +
" IzvozKonacna.Napomena2REf, IzvozKonacna.Napomena3REf, Partnerji_6.PaNaziv AS SpediterRijeka, uvNacinPakovanja.Naziv AS NacinPakovanja,  " +
" IzvozKonacna.NacinPretovara FROM         IzvozKonacna Left JOIN TipKontenjera ON IzvozKonacna.VrstaKontejnera = TipKontenjera.ID LEFT JOIN " +
" Partnerji ON IzvozKonacna.Brodar = Partnerji.PaSifra LEFT JOIN         KrajnjaDestinacija ON IzvozKonacna.KrajnaDestinacija = KrajnjaDestinacija.ID LEFT JOIN " +
" VrstePostupakaUvoz ON IzvozKonacna.Postupanje = VrstePostupakaUvoz.ID LEFT JOIN         KontejnerskiTerminali ON IzvozKonacna.MestoPreuzimanja = KontejnerskiTerminali.id " +
" LEFT JOIN " +
" MestaUtovara ON IzvozKonacna.MesoUtovara = MestaUtovara.ID " +
" LEFT JOIN         Carinarnice ON IzvozKonacna.MestoCarinjenja = Carinarnice.ID " +
" LEFT JOIN        Partnerji AS Partnerji_1 ON IzvozKonacna.Spedicija = Partnerji_1.PaSifra " +
" LEFT JOIN         VrstaCarinskogPostupka ON IzvozKonacna.NapomenaReexport = VrstaCarinskogPostupka.id " +
" LEFT JOIN        InspekciskiTretman ON IzvozKonacna.Inspekcija = InspekciskiTretman.ID " +
" LEFT JOIN        Partnerji AS Partnerji_2 ON IzvozKonacna.Izvoznik = Partnerji_2.PaSifra " +
" LEFT JOIN        Partnerji AS Partnerji_3 ON IzvozKonacna.Klijent1 = Partnerji_3.PaSifra " +
" LEFT JOIN       Partnerji AS Partnerji_4 ON IzvozKonacna.Klijent2 = Partnerji_4.PaSifra " +
" LEFT JOIN         Partnerji AS Partnerji_5 ON IzvozKonacna.Klijent3 = Partnerji_5.PaSifra LEFT JOIN " +
" Partnerji AS Partnerji_6 ON IzvozKonacna.SpediterRijeka = Partnerji_6.PaSifra " +
" LEFT JOIN         uvNacinPakovanja ON IzvozKonacna.NacinPakovanja = uvNacinPakovanja.ID " +
" where IzvozKonacna.IdNadredjena = " + Convert.ToInt32(txtNadredjeni.Text) + " order by IzvozKonacna.ID desc";
            //  " where IzvozKonacna.IdNadredjena = " + Convert.ToInt32(txtNadredjeni.Text) + " order by IzvozKonacna.ID desc";


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


        }

        private void frmIzvozUnosManipulacije_Load(object sender, EventArgs e)
        {
            FillCombo();

            txtNadredjeni.Text = pIDPlana.ToString();
            txtID.Text = pID.ToString();
            cboNalogodavac1.SelectedValue = pNalogodavac1;
            cboNalogodavac2.SelectedValue = pNalogodavac2;
            cboNalogodavac3.SelectedValue = pNalogodavac3;
            cboUvoznik.SelectedValue = pIzvoznik;
            if (txtNadredjeni.Text == "0")
            {
                FillGVIzvoz();
            }
            else
            {
                FillGVIzvozKonacnaPoPlanu();

            }
            Usao = 1;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    txtID.Text = row.Cells[0].Value.ToString();
                    //  VratiPodatkeSelect(Convert.ToInt32(txtID.Text));

                }
            }
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
            int pomStatusKontejnera = 0;
            string pomPokret= "";
            string pomForma = "";
            try
            {
                foreach (DataGridViewRow row in dataGridView6.Rows)
                {
                    if (row.Selected)
                    {
                        pomManupulacija = Convert.ToInt32(row.Cells[0].Value.ToString());
                        int Adm = ProveriAdministrativna(pomManupulacija);
                        if (Adm == 0)
                        {
                            pomPokret = row.Cells[7].Value.ToString();
                            pomStatusKontejnera = Convert.ToInt32(row.Cells[8].Value.ToString());
                            pomForma = row.Cells[10].Value.ToString();

                        }
                        else
                        {
                            pomPokret = "/";
                            pomStatusKontejnera = Convert.ToInt32(0);
                            pomForma = "/";
                        }
                        CenaNadjenaTip1 = PretraziPoNalogodavciIIzvozniku(pomManupulacija, Convert.ToInt32(cboNalogodavac2.SelectedValue), Convert.ToInt32(cboUvoznik.SelectedValue));
                        CenaNadjenaTip2 = PretraziPoNalogodavci(pomManupulacija, Convert.ToInt32(cboNalogodavac2.SelectedValue), Convert.ToInt32(cboUvoznik.SelectedValue));


                        if (CenaNadjenaTip1 == 1)
                        {
                            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                            SqlConnection con = new SqlConnection(s_connection);

                            con.Open();

                            SqlCommand cmd = new SqlCommand("select ID, Cena, OrgJed from Cene where VrstaManipulacije = " + pomManupulacija + " Uvoznik = " + cboUvoznik.SelectedValue + " and  Komitent = " + Convert.ToInt32(cboNalogodavac2.SelectedValue) + "", con);
                            SqlDataReader dr = cmd.ExecuteReader();

                            while (dr.Read())
                            {

                                pomCena = Convert.ToDouble(dr["Cena"].ToString());
                                pomkolicina = 1;
                                pomOrgJed = Convert.ToInt32(dr["OrgJed"].ToString());

                            }

                            con.Close();
                            pomPlatilac = Convert.ToInt32(cboNalogodavac2.SelectedValue);

                        }

                        if (CenaNadjenaTip2 == 1 && CenaNadjenaTip2 == 0)
                        {
                            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
                                pomOrgJed = Convert.ToInt32(dr["OrgJed"].ToString());

                            }

                            con.Close();
                            pomPlatilac = Convert.ToInt32(cboNalogodavac2.SelectedValue);

                        }

                        if (CenaNadjenaTip2 == 0 && CenaNadjenaTip2 == 0)
                        {
                            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
                                pomOrgJed = Convert.ToInt32(dr["OrgJed"].ToString());

                            }

                            con.Close();
                            pomPlatilac = Convert.ToInt32(cboNalogodavac2.SelectedValue);

                        }
                        pomOrgJed = VratiOrgJed(pomManupulacija);
                        //  pomCena = Convert.ToDouble(row.Cells[2].Value.ToString());
                        // pomkolicina= Convert.ToDouble(row.Cells[4].Value.ToString());
                        //   pomOrgJed = Convert.ToInt32(row.Cells[5].Value.ToString());
                        foreach (DataGridViewRow row2 in dataGridView1.Rows)
                        {
                            if (row2.Selected)
                            {
                                pomID = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                                UbaciStavkuUsluge(pomID, pomManupulacija, pomCena, pomkolicina, pomOrgJed,pomPlatilac, pomPokret, pomStatusKontejnera, pomForma);
                            }
                        }


                    }
                }
                FillDG8();

            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
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
            int pomStatusKontejnera = 0;
            string  pomPokret = "";
            string  pomForma = "";
            try
            {
                foreach (DataGridViewRow row in dataGridView6.Rows)
                {
                    if (row.Selected)
                    {
                        pomManupulacija = Convert.ToInt32(row.Cells[0].Value.ToString());
                        CenaNadjenaTip1 = PretraziPoNalogodavciIIzvozniku(pomManupulacija, Convert.ToInt32(cboNalogodavac3.SelectedValue), Convert.ToInt32(cboUvoznik.SelectedValue));
                        CenaNadjenaTip2 = PretraziPoNalogodavci(pomManupulacija, Convert.ToInt32(cboNalogodavac3.SelectedValue), Convert.ToInt32(cboUvoznik.SelectedValue));
                        int Adm = ProveriAdministrativna(pomManupulacija);
                        if (Adm == 0)
                        {
                            pomPokret = row.Cells[7].Value.ToString();
                            pomStatusKontejnera = Convert.ToInt32(row.Cells[8].Value.ToString());
                            pomForma = row.Cells[10].Value.ToString();

                        }
                        else
                        {
                            pomPokret = "/";
                            pomStatusKontejnera = Convert.ToInt32(0);
                            pomForma = "/";
                        }

                        if (CenaNadjenaTip1 == 1)
                        {
                            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                            SqlConnection con = new SqlConnection(s_connection);

                            con.Open();

                            SqlCommand cmd = new SqlCommand("select ID, Cena, OrgJed from Cene where VrstaManipulacije = " + pomManupulacija + " Uvoznik = " + cboUvoznik.SelectedValue + " and  Komitent = " + Convert.ToInt32(cboNalogodavac3.SelectedValue) + "", con);
                            SqlDataReader dr = cmd.ExecuteReader();

                            while (dr.Read())
                            {

                                pomCena = Convert.ToDouble(dr["Cena"].ToString());
                                pomkolicina = 1;
                                pomOrgJed = Convert.ToInt32(dr["OrgJed"].ToString());

                            }

                            con.Close();

                            pomPlatilac = Convert.ToInt32(cboNalogodavac3.SelectedValue);
                        }

                        if (CenaNadjenaTip2 == 1 && CenaNadjenaTip2 == 0)
                        {
                            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
                                pomOrgJed = Convert.ToInt32(dr["OrgJed"].ToString());

                            }

                            con.Close();
                            pomPlatilac = Convert.ToInt32(cboNalogodavac3.SelectedValue);

                        }

                        if (CenaNadjenaTip2 == 0 && CenaNadjenaTip2 == 0)
                        {
                            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
                                pomOrgJed = Convert.ToInt32(dr["OrgJed"].ToString());

                            }

                            con.Close();
                            pomPlatilac = Convert.ToInt32(cboNalogodavac3.SelectedValue);

                        }
                        pomOrgJed = VratiOrgJed(pomManupulacija);
                        //  pomCena = Convert.ToDouble(row.Cells[2].Value.ToString());
                        // pomkolicina= Convert.ToDouble(row.Cells[4].Value.ToString());
                        //   pomOrgJed = Convert.ToInt32(row.Cells[5].Value.ToString());
                        foreach (DataGridViewRow row2 in dataGridView1.Rows)
                        {
                            if (row2.Selected)
                            {
                                pomID = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                                UbaciStavkuUsluge(pomID, pomManupulacija, pomCena, pomkolicina, pomOrgJed, pomPlatilac, pomPokret, pomStatusKontejnera,pomForma);
                            }
                        }


                    }
                }

                FillDG8();
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int pom = 0;
            InsertIzvoz uvK = new InsertIzvoz();
          
            try
            {
              
                        foreach (DataGridViewRow row2 in dataGridView7.Rows)
                        {
                            if (row2.Selected)
                            {
                        if (txtNadredjeni.Text != "0")
                        {
                            pom = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                            uvK.DelIzvozKonacnaUsluga(pom);
                        }
                        else
                        {
                            pom = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                            uvK.DelIzvozUsluga(pom);
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
            InsertIzvoz uvK = new InsertIzvoz();

            try
            {

                foreach (DataGridViewRow row2 in dataGridView7.Rows)
                {
                    if (row2.Selected)
                    {
                        if (txtNadredjeni.Text != "0")
                        {
                            pom = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                            uvK.UpdPlatiocaIzvozKonacnaUsluga(pom, Convert.ToInt32(cboUvoznik.SelectedValue));
                        }
                        else
                        {
                            pom = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                            uvK.UpdPlatiocaIzvozUsluga(pom, Convert.ToInt32(cboUvoznik.SelectedValue));
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
        int VratiOrgJed(int Manipulacija)
        {
            int pomOJ = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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

        int VratiBrojManipulacija(int ID)
        {
            int pomBZ = 0;
            string Komanda = "";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();
            if (txtNadredjeni.Text != "0")
            {
                Komanda = "select Count(*) as Broj from IzvozKonacnaVrstaManipulacije  where ID= ";
            }
            else
            {
                Komanda = "select Count(*) as Broj from IzvozVrstaManipulacije  where ID= ";
            }

            SqlCommand cmd = new SqlCommand(Komanda + ID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                //Izmenjeno
                // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                pomBZ = Convert.ToInt32(dr["Broj"].ToString());
            }
            con.Close();
            return pomBZ;

        }

        int VratiBrojScenario(int ID)
        {
            int pomBZ = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Count(*) as Broj from Scenario  where ID= " + ID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                //Izmenjeno
                // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                pomBZ = Convert.ToInt32(dr["Broj"].ToString());
            }
            con.Close();
            return pomBZ;

        }

        int ProveriDaLiSuIsteManipulacije(int ID, int SC)
        {
            int postoji = 0;
            int konacan = 1;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select usluga from Scenario  where ID= " + ID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                //Izmenjeno
                // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                postoji = VratiPostojiUsluga(Convert.ToInt32(dr["usluga"].ToString()), ID);
                if (postoji == 0)
                {
                    konacan = 0;
                }
            }
            con.Close();
            return konacan;

        }
        int VratiPostojiUsluga(int usluga, int kontejnerid)
        {
            int postoji = 0;
            string Komanda = "";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            if (txtNadredjeni.Text != "0")
            {
                Komanda = "select Count(*) as Broj from IzvozKonacnaVrstaManipulacije where IDNadredjena = " + kontejnerid + " and IDVrstaManipulacije = " + usluga;
            }
            else
            {
                Komanda = "select Count(*) as Broj from IzvozVrstaManipulacije where IDNadredjena = " + kontejnerid + " and IDVrstaManipulacije = " + usluga;
            }

            SqlCommand cmd = new SqlCommand(Komanda, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                //Izmenjeno
                // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                if (Convert.ToInt32(dr["Broj"].ToString()) == 1)
                {
                    postoji = 1;
                }
            }
            con.Close();
            return postoji;


        }

        private void ProveriScenario(int ID)
        {
            //Panta 2
            // broj Zapisa za kontejner
            //Izab raniScenario
            int IzabraniScenario = 0;
            int BrojZapisaKontejnera = VratiBrojManipulacija(ID);
            int BSC7 = VratiBrojScenario(7);
            int BSC8 = VratiBrojScenario(8);
            int BSC9 = VratiBrojScenario(9);
            int BSC10 = VratiBrojScenario(10);
            int BSC11 = VratiBrojScenario(11);
            int BSC12 = VratiBrojScenario(12);
            int BSC13 = VratiBrojScenario(13);
            int BSC14 = VratiBrojScenario(14);
            int BSC15 = VratiBrojScenario(15);
            int BSC16 = VratiBrojScenario(16);

            int BSC23 = VratiBrojScenario(23);
            int BSC24 = VratiBrojScenario(24);
            int BSC25 = VratiBrojScenario(25);
            int BSC26 = VratiBrojScenario(26);
            int rasporedjen = 0;
            if (txtNadredjeni.Text != "0")
            {
                rasporedjen = 1;
            }
            else
            {
                rasporedjen = 0;
            }
            if (BrojZapisaKontejnera == BSC7)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 7);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(7, ID, 2, rasporedjen);
                    MessageBox.Show("Izabrali ste scenario 7");
                }
            }
            if (BrojZapisaKontejnera == BSC8)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 8);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(8, ID, 2, rasporedjen);
                    MessageBox.Show("Izabrali ste scenario 8");
                }
            }

            if (BrojZapisaKontejnera == BSC9)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 9);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(9, ID, 2, rasporedjen);
                    MessageBox.Show("Izabrali ste scenario 9");
                }
            }

            if (BrojZapisaKontejnera == BSC10)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 10);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(10, ID, 2, rasporedjen);
                    MessageBox.Show("Izabrali ste scenario 10");
                }
            }

            if (BrojZapisaKontejnera == BSC11)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 11);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(11, ID, 2, rasporedjen);
                    MessageBox.Show("Izabrali ste scenario 11");
                }
            }

            if (BrojZapisaKontejnera == BSC12)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 12);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(12, ID, 2, rasporedjen);
                    MessageBox.Show("Izabrali ste scenario 12");
                }
            }

            if (BrojZapisaKontejnera == BSC13)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 13);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(13, ID, 2, rasporedjen);
                    MessageBox.Show("Izabrali ste scenario 13");
                }
            }

            if (BrojZapisaKontejnera == BSC14)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 14);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(14, ID, 2, rasporedjen);
                    MessageBox.Show("Izabrali ste scenario 14");
                }
            }

            if (BrojZapisaKontejnera == BSC15)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 15);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(15, ID, 2, rasporedjen);
                    MessageBox.Show("Izabrali ste scenario 15");
                }
            }

            if (BrojZapisaKontejnera == BSC16)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 16);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(16, ID, 2, rasporedjen);
                    MessageBox.Show("Izabrali ste scenario 16");
                }
            }

            if (BrojZapisaKontejnera == BSC23)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 23);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(23, ID, 2, rasporedjen);
                    MessageBox.Show("Izabrali ste scenario 23");
                }
            }


            if (BrojZapisaKontejnera == BSC24)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 24);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(24, ID, 2, rasporedjen);
                    MessageBox.Show("Izabrali ste scenario 24");
                }
            }

            if (BrojZapisaKontejnera == BSC25)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 25);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(25, ID, 2, rasporedjen);
                    MessageBox.Show("Izabrali ste scenario 25");
                }
            }


            if (BrojZapisaKontejnera == BSC26)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 26);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(26, ID, 2, rasporedjen);
                    MessageBox.Show("Izabrali ste scenario 26");
                }
            }

            // Proveri da li je isti broj 
            // Proveri da lii su iste manipulacije

        }

        int ProveriAdministrativna(int ID)
        {
            int pomBZ = 0;
            string Komanda = "";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            Komanda = "select Administrativna from VrstaManipulacije  where ID = ";


            SqlCommand cmd = new SqlCommand(Komanda + ID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                //Izmenjeno
                // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                pomBZ = Convert.ToInt32(dr["Administrativna"].ToString());
            }
            con.Close();
            return pomBZ;

        }

        private void button10_Click(object sender, EventArgs e)
        {
           //Uzima cenu samo za PArtnera
            int pomID = 0;
            int pomManupulacija = 0;
            double pomCena = 0;
            double pomkolicina = 1;
            string pomPokret = "";
            int pomStatusKontejnera = 0;
            int pomPlatilac = 0;
            string pomForma = "";
            try
            {
                foreach (DataGridViewRow row in dataGridView6.Rows)
                {
                    if (row.Selected)
                    {
                        pomManupulacija = Convert.ToInt32(row.Cells[0].Value.ToString());
                        int Adm = ProveriAdministrativna(pomManupulacija);
                        if (Adm == 0)
                        {
                            pomPokret = row.Cells[7].Value.ToString();
                            pomStatusKontejnera = Convert.ToInt32(row.Cells[8].Value.ToString());
                            pomForma = row.Cells[10].Value.ToString();

                        }
                        else
                        {
                            pomPokret = "/";
                            pomStatusKontejnera = Convert.ToInt32(0);
                            pomForma = "/";
                        }
                        // CenaNadjenaTip1 = PretraziPoNalogodavciIIzvozniku(pomManupulacija, Convert.ToInt32(cboNalogodavac3.SelectedValue), Convert.ToInt32(cboUvoznik.SelectedValue));
                        // CenaNadjenaTip2 = PretraziPoNalogodavci(pomManupulacija, Convert.ToInt32(cboNalogodavac3.SelectedValue), Convert.ToInt32(cboUvoznik.SelectedValue));



                        var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
                                pomOrgJed = Convert.ToInt32(dr["OrgJed"].ToString());

                            }
                            
                            con.Close();
                            pomPlatilac = Convert.ToInt32(cboUvoznik.SelectedValue);
                        pomOrgJed = VratiOrgJed(pomManupulacija);


                        //  pomCena = Convert.ToDouble(row.Cells[2].Value.ToString());
                        // pomkolicina= Convert.ToDouble(row.Cells[4].Value.ToString());
                        //   pomOrgJed = Convert.ToInt32(row.Cells[5].Value.ToString());
                        foreach (DataGridViewRow row2 in dataGridView1.Rows)
                        {
                            if (row2.Selected)
                            {
                                pomID = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                                UbaciStavkuUsluge(pomID, pomManupulacija, pomCena, pomkolicina, pomOrgJed, pomPlatilac, pomPokret, pomStatusKontejnera,pomForma);
                            }
                        }


                    }
                }

                FillDG8();
                ProveriScenario(pomID);
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            FillDG6(1);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            FillDG6(0);
        }

        private void dataGridView7_CurrentCellChanged(object sender, EventArgs e)
        {
            
            
        }
        private void dataGridView7_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

            if (Usao == 1)
            {

                int columnIndex = dataGridView7.CurrentCell.ColumnIndex;
                int rowIndex = dataGridView7.CurrentCell.RowIndex;



                if (columnIndex == 6)
                {   string value = dataGridView7.Rows[rowIndex].Cells[0].EditedFormattedValue.ToString();
                    string value1 = dataGridView7.Rows[rowIndex].Cells[6].EditedFormattedValue.ToString();
                    string value2 = dataGridView7.Rows[rowIndex].Cells[6].Value.ToString();
                    if (txtNadredjeni.Text != "0")
                    {
                        InsertIzvoz uvK = new InsertIzvoz();
                        uvK.UpdCENAIzvozKonacnaUsluga(Convert.ToInt32(value), Convert.ToDouble(value1));
                       // FillDG8();
                    }
                    else
                    {
                        InsertIzvoz uvK = new InsertIzvoz();
                        uvK.UpdCENAIzvozUsluga(Convert.ToInt32(value), Convert.ToDouble(value1));
                        //FillDG8();

                    }


                }

            }
        }
        private void dataGridView7_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
      
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
                        InsertIzvoz uvK = new InsertIzvoz();
                        uvK.UpdPDVIzvozKonacnaUsluga(Convert.ToInt32(value), tmp);
                       // FillDG8();
                    }
                    else
                    {
                        InsertIzvoz uvK = new InsertIzvoz();
                        uvK.UpdPDVIzvozUsluga(Convert.ToInt32(value), tmp);
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
                FillGVIzvozSVI();
            }
            else
            {
                FillGVIzvozKonacnaPoPlanuSVI();

            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (txtNadredjeni.Text == "0")
            {
                FillGVIzvoz();
            }
            else
            {
                FillGVIzvozKonacnaPoPlanu();

            }
        }
        private void FillDG6Scenario()
        {
        

            var select = " SELECT VrstaManipulacije.[ID]      ,VrstaManipulacije.[Naziv] ,  " +
 " VrstaManipulacije.[JM] " +
" ,VrstaManipulacije.[TipManipulacije]      ,VrstaManipulacije.[OrgJed]      ,OrganizacioneJedinice.Naziv as OJ " +
" ,VrstaManipulacije.[Cena] ,Scenario.Pokret,Scenario.StatusKontejnera,KontejnerStatus.Naziv, Scenario.Forma, VrstaManipulacije.[Datum] ,VrstaManipulacije.[Korisnik] FROM[VrstaManipulacije] " +
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
            dataGridView6.Columns[10].HeaderText = "Forma";
            dataGridView6.Columns[10].Width = 100;

        }

        private void button16_Click(object sender, EventArgs e)
        {
            FillDG6Scenario();
            dataGridView6.SelectAll();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (txtNadredjeni.Text != "0")
            {
                InsertIzvoz uvK = new InsertIzvoz();
                uvK.InsUbaciUsluguKonacnaPlan(Convert.ToInt32(txtNadredjeni.Text));
                FillDG8();
            }
            else
            {
               // InsertIzvoz uvK = new InsertIzvoz();
              //  uvK.InsUbaciUslugu(ID, Manipulacija, Cena, Kolicina, OrgJed, Platilac, 0, PomPokret, PomStatusKOntejnera);
              //  FillDG8();

            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            FillDG6(3);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            FillDG6(3);
        }
    }
    }

