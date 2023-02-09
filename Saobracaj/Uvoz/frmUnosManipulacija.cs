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

namespace Saobracaj.Uvoz
{
    public partial class frmUnosManipulacija : Form
    {
        public string connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        int pIDPlana = 0; 
        int pID = 0;
        int pNalogodavac1 = 0;
        int pNalogodavac2 = 0;
        int pNalogodavac3 = 0;
        int pUvoznik = 0;
        int pomOrgJed = 0;
        public frmUnosManipulacija()
        {
            InitializeComponent();
        }

        public frmUnosManipulacija(int IDPlana, int ID, int Nalogodavac1, int Nalogodavac2, int Nalogodavac3, int Uvoznik)
        {
            InitializeComponent();
            pIDPlana = IDPlana;
            pID = ID;
            pNalogodavac1 = Nalogodavac1;
            pNalogodavac2 = Nalogodavac2;
            pNalogodavac3 = Nalogodavac3;
            pUvoznik = Uvoznik;
            txtNadredjeni.Text = pIDPlana.ToString();
            FillDG8();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            FillDG6();
        }

        private void FillDG6()
        {
            //Opsti cenovnik pozivanje
            var select = "select Cene.ID, VrstaManipulacije.Naziv, Cene.Cena, VrstaManipulacije.ID, 1 as Kolicina,  VrstaManipulacije.OrgJed from Cene " +
            " inner join VrstaManipulacije on VrstaManipulacije.ID = Cene.VrstaManipulacije " +
            " where TipCenovnika = 1 order by VrstaManipulacije.Naziv ";
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
            dataGridView6.Columns[1].Width = 180;

            DataGridViewColumn column3 = dataGridView6.Columns[2];
            dataGridView6.Columns[2].HeaderText = "Cena";
            dataGridView6.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView6.Columns[3];
            dataGridView6.Columns[3].HeaderText = "IDVM";
            dataGridView6.Columns[3].Width = 50;
            dataGridView6.Columns[3].Visible = false;


            DataGridViewColumn column5 = dataGridView6.Columns[4];
            dataGridView6.Columns[4].HeaderText = "Kol";
            dataGridView6.Columns[4].Width = 50;

        }
        private void FillDN1()
        {
//Cenovnik po Nalogodavcu broj 1
            var select = "select Cene.ID, VrstaManipulacije.Naziv, Cene.Cena, VrstaManipulacije.ID, 1 as Kolicina, VrstaManipulacije.OrgJed from Cene " +
            " inner join VrstaManipulacije on VrstaManipulacije.ID = Cene.VrstaManipulacije " +
              "inner join OrganizacioneJedinice on VrstaManipulacije.OrgJed = OrganizacioneJedinice.ID " +
            " where  Uvoznik = " + Convert.ToInt32(cboUvoznik.SelectedValue)  + " and Cene.Komitent = " + Convert.ToInt32(cboNalogodavac1.SelectedValue) + "  order by VrstaManipulacije.Naziv ";
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
            // FillDG7();
            FillDN1();
        }

        private void UbaciStavkuUsluge(int ID, int Manipulacija, double Cena, double Kolicina, int OrgJed)
        {
            if (txtNadredjeni.Text != "0")
            {
                InsertUvozKonacna uvK = new InsertUvozKonacna();
                uvK.InsUbaciUsluguKonacna(ID, Manipulacija, Cena, Kolicina, OrgJed);
                FillDG8();
            }
            else
            {
                InsertUvozKonacna uvK = new InsertUvozKonacna();
                uvK.InsUbaciUslugu(ID,Manipulacija, Cena, Kolicina, OrgJed);
                FillDG8();

            }

           
        }

        private void FillDG8()
        {
            var select ="";
            if (txtNadredjeni.Text == "0")
            {
                select = "select  UvozVrstaManipulacije.ID as ID, UvozVrstaManipulacije.IDNadredjena as KontejnerID, Uvoz.BrojKontejnera, " + 
 " UvozVrstaManipulacije.Kolicina, " +
 " VrstaManipulacije.ID as ManipulacijaID,VrstaManipulacije.Naziv as ManipulacijaNaziv, " +
 " UvozVrstaManipulacije.Cena,OrganizacioneJedinice.ID, OrganizacioneJedinice.Naziv as OrganizacionaJedinica, " +
 " Partnerji.PaSifra as NalogodavacID,PArtnerji.PaNaziv as Nalogodavac, p2.PaSifra as Uvoznik, p2.PaNaziv as Uvoznik " +
  "  from UvozVrstaManipulacije " +
  "  inner join Cene on UvozVrstaManipulacije.IDVrstaManipulacije = Cene.ID " +
  "  inner   join VrstaManipulacije on VrstaManipulacije.ID = Cene.VrstaManipulacije " +
 " inner   join Partnerji on Cene.Komitent = Partnerji.PaSifra " +
 " inner    join Partnerji p2 on Cene.Uvoznik = p2.PaSifra " +
 " inner    join OrganizacioneJedinice on OrganizacioneJedinice.ID = UvozVrstaManipulacije.OrgJed " +
 " inner    join Uvoz on UvozVrstaManipulacije.IDNadredjena = Uvoz.ID";
                 

            }
            else
            {
                select = "select  UvozKonacnaVrstaManipulacije.ID as ID, UvozKonacnaVrstaManipulacije.IDNadredjena as KontejnerID, UvozKonacna.BrojKontejnera,  UvozKonacnaVrstaManipulacije.Kolicina, " +
 " VrstaManipulacije.ID as ManipulacijaID,VrstaManipulacije.Naziv as ManipulacijaNaziv, " +
"   UvozKonacnaVrstaManipulacije.Cena,OrganizacioneJedinice.ID, " +
 "  OrganizacioneJedinice.Naziv as OrganizacionaJedinica, " +
 "   Partnerji.PaSifra as NalogodavacID,PArtnerji.PaNaziv as Nalogodavac, p2.PaSifra as Uvoznik, p2.PaNaziv as Uvoznik " +
 "     from UvozKonacnaVrstaManipulacije inner join Cene on UvozKonacnaVrstaManipulacije.IDVrstaManipulacije = Cene.ID " +
 "    inner     join VrstaManipulacije on VrstaManipulacije.ID = Cene.VrstaManipulacije " +
"  inner     join Partnerji on Cene.Komitent = Partnerji.PaSifra  inner " +
  "    join Partnerji p2 on Cene.Uvoznik = p2.PaSifra " +
"  inner     join OrganizacioneJedinice on OrganizacioneJedinice.ID = UvozKonacnaVrstaManipulacije.OrgJed  inner " +
  "    join UvozKonacna on UvozKonacnaVrstaManipulacije.IDNadredjena = UvozKonacna.ID  where UvozKonacna.IDNadredjeni = " + Convert.ToInt32(txtNadredjeni.Text);
            }

            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView7.ReadOnly = true;
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
            dataGridView7.Columns[1].HeaderText = "Man";
            dataGridView7.Columns[1].Width = 30;

            DataGridViewColumn column3 = dataGridView7.Columns[2];
            dataGridView7.Columns[2].HeaderText = "Kontejner";
            dataGridView7.Columns[2].Width = 50;

        }

        private void button12_Click(object sender, EventArgs e)
        {
            int pomID = 0;
            int pomManupulacija = 0;
            double pomCena = 0;
            double pomkolicina = 1;
            try
            {
                foreach (DataGridViewRow row in dataGridView6.Rows)
                {
                    if (row.Selected)
                    {
                        
                        pomManupulacija = Convert.ToInt32(row.Cells[0].Value.ToString());
                        pomCena = Convert.ToDouble(row.Cells[2].Value.ToString());
                        pomkolicina= Convert.ToDouble(row.Cells[4].Value.ToString());
                        pomOrgJed = Convert.ToInt32(row.Cells[5].Value.ToString());
                        foreach (DataGridViewRow row2 in dataGridView1.Rows)
                        {
                            if (row2.Selected)
                            {
                                pomID = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                                UbaciStavkuUsluge(pomID, pomManupulacija, pomCena, pomkolicina, pomOrgJed);
                            }
                        }

                                
                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }
        private void FillGVUvozKonacnaPoPlanu()
        {
            var select = "    SELECT UvozKonacna.ID, BrojKontejnera, BrodskaTeretnica as BL,  DobijenNalogBrodara as Dobijen_Nalog_Brodara ,ATABroda, Brodovi.Naziv as Brod,Napomena1 as Napomena1, DobijeBZ as DatumBZ ,PIN, " +
 " [BrojKontejnera], TipKontenjera.Naziv as Vrsta_Kontejnera,  KontejnerskiTerminali.Naziv as R_L_SRB, pp1.Naziv as Dirigacija_Kontejnera_Za,  " +
 "   BrodskaTeretnica, VrstaRobeADR.Naziv as ADR, b.PaNaziv as Brodar, n1.PaNaziv as Nalogodavac1, Ref1 as Ref1, n2.PaNaziv as Nalogodavac2, Ref2 as Ref2, n3.PaNaziv as Nalogodavac3, Ref3 as Ref3, " +
  "      p1.PaNaziv as Uvoznik,   " +
  "  (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaManipulacije.Naziv as nvarchar(20))   FROM UvozKonacnaVrstaManipulacije " +
   "       inner join VrstaManipulacije on VrstaManipulacije.ID = UvozKonacnaVrstaManipulacije.IDVrstaManipulacije   where UvozKonacnaVrstaManipulacije.IDNadredjena = UvozKonacna.ID " +
   "        FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as VrsteUsluga,   " +
   "     (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaRobeHS.HSKod as nvarchar(20))   FROM UvozKonacnaVrstaRobeHS " +
   "       inner join VrstaRobeHS on UvozKonacnaVrstaRobeHS.IDVrstaRobeHS = VrstaRobeHS.ID   where UvozKonacnaVrstaRobeHS.IDNadredjena = UvozKonacna.ID " +
   "        FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as HS,   " +
   "    (SELECT  STUFF((SELECT distinct    '/' + Cast(NHM.Broj as nvarchar(20)) " +
  "            FROM UvozKonacnaNHM  inner join NHM on UvozKonacnaNHM.IDNHM = NHM.ID  where UvozKonacnaNHM.IDNadredjena = UvozKOnacna.ID   FOR XML PATH('')), 1, 1, ''  ) As Skupljen) as NHM,   " +
   "              VrstaPregleda as VrstaPregleda,p2.PaNaziv as SpedicijaRTC,  p3.PaNaziv as SpedicijaGranica,      " +
   " VrstaCarinskogPostupka.Naziv as CarinskiPostupak,   " +
   "                  VrstePostupakaUvoz.Naziv as PostupakSaRobom,uvNacinPakovanja.Naziv as NacinPakovanja, Napomena as Napomena2,  " +
   "                      (Carinarnice.CINaziv + ' ' + Carinarnice.CIOznaka + ' ' + CIEmail + ' ' + CITelefon + ' / ' + p3.PaNaziv) as Carinarnica,   " +
   "                               p4.PaNaziv as OdredisnaSpedicija, MestoIstovara, KontaktOsoba, Email,        BrojPlombe1, BrojPlombe2,   " +
" PredefinisanePoruke.Naziv as NapomenaZaPozicioniranje,  " +
 " NetoRobe, BrutoRobe, TaraKontejnera, BrutoKontejnera, " +
 " Koleta" +
 " FROM UvozKonacna inner join Partnerji on PaSifra = VlasnikKontejnera " +
 " inner join Partnerji p1 on p1.PaSifra = Uvoznik " +
 " inner join Partnerji p2 on p2.PaSifra = SpedicijaRTC " +
" inner join Partnerji p3 on p3.PaSifra = SpedicijaGranica " +
 "  inner join VrstaRobeHS on VrstaRobeHS.ID = UvozKonacna.NazivRobe " +
"  inner join NHM on NHM.ID = NHMBroj " +
 " inner join TipKontenjera on TipKontenjera.ID = UvozKonacna.TipKontejnera " +
 "  inner join Carinarnice on Carinarnice.ID = UvozKonacna.OdredisnaCarina " +
 "  inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = UvozKonacna.CarinskiPostupak " +
 " inner join Predefinisaneporuke on PredefinisanePoruke.ID = UvozKonacna.NapomenaZaPozicioniranje " +
 "  inner join KontejnerskiTerminali on KontejnerskiTerminali.ID = UvozKonacna.RLTErminali " +
 "  inner join Partnerji n1 on n1.PaSifra = Nalogodavac1 " +
 "  inner join Partnerji n2 on n2.PaSifra = Nalogodavac2 " +
 "  inner join Partnerji n3 on n3.PaSifra = Nalogodavac3 " +
 "  inner join Partnerji b on b.PaSifra = UvozKonacna.Brodar " +
  " inner join DirigacijaKontejneraZa pp1 on pp1.ID = UvozKonacna.DirigacijaKontejeraZa   " +
 "  inner join Brodovi on Brodovi.ID = UvozKonacna.NazivBroda " +
                              "   inner join VrstaRobeADR on VrstaRobeADR.ID = ADR " +
                              "    inner join VrstePostupakaUvoz on VrstePostupakaUvoz.ID = PostupakSaRobom    inner join uvNacinPakovanja " +
 " on uvNacinPakovanja.ID = NacinPakovanja  inner join Partnerji p4 on p4.PaSifra = OdredisnaSpedicija " +
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

            RefreshDataGridColor();

        }

        private void FillGVUvoz()
        {
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
    }
}
