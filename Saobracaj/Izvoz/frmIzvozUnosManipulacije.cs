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

namespace Saobracaj.Izvoz
{
    public partial class frmIzvozUnosManipulacije : Form
    {
        public string connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        int pIDPlana = 0;
        int pID = 0;
        int pNalogodavac1 = 0;
        int pNalogodavac2 = 0;
        int pNalogodavac3 = 0;
        int pIzvoznik = 0;
        int pomOrgJed = 0;
        public frmIzvozUnosManipulacije()
        {
            InitializeComponent();
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
            " where  Uvoznik = " + Convert.ToInt32(cboIzvoznik.SelectedValue) + " and Cene.Komitent = " + Convert.ToInt32(cboNalogodavac1.SelectedValue) + "  order by VrstaManipulacije.Naziv ";
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
           " where  Uvoznik = " + Convert.ToInt32(cboIzvoznik.SelectedValue) + " and Cene.Komitent = " + Convert.ToInt32(cboNalogodavac2.SelectedValue) + "  order by VrstaManipulacije.Naziv ";
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
           " where  Uvoznik = " + Convert.ToInt32(cboIzvoznik.SelectedValue) + " and Cene.Komitent = " + Convert.ToInt32(cboNalogodavac3.SelectedValue) + "  order by VrstaManipulacije.Naziv ";
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
        private void UbaciStavkuUsluge(int ID, int Manipulacija, double Cena, double Kolicina, int OrgJed)
        {
            if (txtNadredjeni.Text != "0")
            {
                InsertIzvoz uvK = new InsertIzvoz();
                uvK.InsUbaciUsluguKonacna(ID, Manipulacija, Cena, Kolicina, OrgJed);
                FillDG8();
            }
            else
            {
                InsertIzvoz uvK = new InsertIzvoz();
                uvK.InsUbaciUslugu(ID, Manipulacija, Cena, Kolicina, OrgJed);
                FillDG8();

            }


        }

        private void FillDG8()
        {
            var select = "";
            if (txtNadredjeni.Text == "0")
            {
                select = "select  IzvozVrstaManipulacije.ID as ID, IzvozVrstaManipulacije.IDNadredjena as KontejnerID, Izvoz.BrojKontejnera, " +
 " IzvozVrstaManipulacije.Kolicina, " +
 " VrstaManipulacije.ID as ManipulacijaID,VrstaManipulacije.Naziv as ManipulacijaNaziv, " +
 " IzvozVrstaManipulacije.Cena,OrganizacioneJedinice.ID, OrganizacioneJedinice.Naziv as OrganizacionaJedinica, " +
 " Partnerji.PaSifra as NalogodavacID,PArtnerji.PaNaziv as Nalogodavac, p2.PaSifra as Izvoznik, p2.PaNaziv as Izvoznik " +
  "  from IzvozVrstaManipulacije " +
  "  inner join Cene on IzvozVrstaManipulacije.IDVrstaManipulacije = Cene.ID " +
  "  inner   join VrstaManipulacije on VrstaManipulacije.ID = Cene.VrstaManipulacije " +
 " inner   join Partnerji on Cene.Komitent = Partnerji.PaSifra " +
 " inner    join Partnerji p2 on Cene.Uvoznik = p2.PaSifra " +
 " inner    join OrganizacioneJedinice on OrganizacioneJedinice.ID = IzvozVrstaManipulacije.OrgJed " +
 " inner    join Izvoz on IzvozVrstaManipulacije.IDNadredjena = Izvoz.ID";


            }
            else
            {
                select = "select  IzvozKonacnaVrstaManipulacije.ID as ID, IzvozKonacnaVrstaManipulacije.IDNadredjena as KontejnerID, IzvozKonacna.BrojKontejnera,  IzvozKonacnaVrstaManipulacije.Kolicina, " +
 " VrstaManipulacije.ID as ManipulacijaID,VrstaManipulacije.Naziv as ManipulacijaNaziv, " +
"   IzvozKonacnaVrstaManipulacije.Cena,OrganizacioneJedinice.ID, " +
 "  OrganizacioneJedinice.Naziv as OrganizacionaJedinica, " +
 "   Partnerji.PaSifra as NalogodavacID,PArtnerji.PaNaziv as Nalogodavac, p2.PaSifra as Uvoznik, p2.PaNaziv as Uvoznik " +
 "     from IzvozKonacnaVrstaManipulacije inner join Cene on IzvozKonacnaVrstaManipulacije.IDVrstaManipulacije = Cene.ID " +
 "    inner     join VrstaManipulacije on VrstaManipulacije.ID = Cene.VrstaManipulacije " +
"  inner     join Partnerji on Cene.Komitent = Partnerji.PaSifra  inner " +
  "    join Partnerji p2 on Cene.Uvoznik = p2.PaSifra " +
"  inner     join OrganizacioneJedinice on OrganizacioneJedinice.ID = IzvozKonacnaVrstaManipulacije.OrgJed  inner " +
  "    join IzvozKonacna on IzvozKonacnaVrstaManipulacije.IDNadredjena = IzvozKonacna.ID  where IzvozKonacna.IDNadredjena = " + Convert.ToInt32(txtNadredjeni.Text);
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
                        pomkolicina = Convert.ToDouble(row.Cells[4].Value.ToString());
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
            cboIzvoznik.DataSource = partDS2.Tables[0];
            cboIzvoznik.DisplayMember = "PaNaziv";
            cboIzvoznik.ValueMember = "PaSifra";
        }

        private void FillGVIzvoz()
        {
            var select = " SELECT  Izvoz.ID as ID,    Izvoz.VrstaKontejnera, TipKontenjera.Naziv, Izvoz.BrojVagona, Izvoz.BrojKontejnera, Izvoz.BrodskaPlomba, Izvoz.OstalePlombe, Izvoz.BookingBrodara, Partnerji.PaNaziv, " +
                  "    Izvoz.CutOffPort, Izvoz.NetoRobe, Izvoz.BrutoRobe, Izvoz.BrutoRobeO, Izvoz.BrojKoleta, Izvoz.BrojKoletaO, Izvoz.CBM, Izvoz.CBMO, Izvoz.VrednostRobeFaktura, " +
                   "  (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaManipulacije.Naziv as nvarchar(20))   FROM IzvozVrstaManipulacije " +
   "       inner join VrstaManipulacije on VrstaManipulacije.ID = IzvozVrstaManipulacije.IDVrstaManipulacije   where IzvozVrstaManipulacije.IDNadredjena = Izvoz.ID " +
   "        FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as VrsteUsluga,   " +
   "     (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaRobeHS.HSKod as nvarchar(20))   FROM IzvozVrstaRobeHS " +
   "       inner join VrstaRobeHS on IzvozVrstaRobeHS.IDVrstaRobeHS = VrstaRobeHS.ID   where IzvozVrstaRobeHS.IDNadredjena = Izvoz.ID " +
   "        FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as HS,   " +
   "    (SELECT  STUFF((SELECT distinct    '/' + Cast(NHM.Broj as nvarchar(20)) " +
  "            FROM IzvozNHM  inner join NHM on IzvozNHM.IDNHM = NHM.ID  where IzvozNHM.IDNadredjena = Izvoz.ID   FOR XML PATH('')), 1, 1, ''  ) As Skupljen) as NHM, " +
                  "        Izvoz.Valuta, KrajnjaDestinacija.Naziv AS KrajnjaDestinacija, VrstePostupakaUvoz.Naziv AS Postupak, KontejnerskiTerminali.Naziv AS PPCNT, " +
                  "        KontejnerskiTerminali.Oznaka, Izvoz.Cirada, Izvoz.PlaniraniDatumUtovara, MestaUtovara.Naziv AS MestoUtovara, Izvoz.KontaktOsoba,  " +
                  "        Carinarnice.Naziv AS Carinarnica, Carinarnice.CIOznaka, Partnerji_1.PaNaziv AS Spedicija, AdresaStatusVozila.Naziv AS AdresaStatusVozila, " +
                  "        NaslovStatusaVozila.Naziv AS NaslovStatusaVozila, Izvoz.EtaLeget, VrstaCarinskogPostupka.Naziv AS Reexport, InspekciskiTretman.Naziv AS InspekciskiTretman, " +
                  "        Izvoz.AutoDana, Izvoz.NajavaVozila, Izvoz.DodatneNapomeneDrumski, Izvoz.Vaganje, Izvoz.VGMTezina, Izvoz.Tara, Izvoz.VGMBrod, " +
                  "        Partnerji_2.PaNaziv AS Izvoznik, Partnerji_3.PaNaziv AS Klijent1, Izvoz.Napomena1REf, Izvoz.DobijenNalogKlijent1, Partnerji_4.PaNaziv AS klijent2, " +
                  "        Izvoz.Napomena2REf, Partnerji_5.PaNaziv AS Klijent3, Izvoz.Napomena3REf, Partnerji_6.PaNaziv AS SpediterRijeka, uvNacinPakovanja.Naziv AS NacinPakovanja, " +
                  "        Izvoz.NacinPretovara " +
"    FROM         Izvoz INNER JOIN " +
                   "       TipKontenjera ON Izvoz.VrstaKontejnera = TipKontenjera.ID INNER JOIN " +
                  "        Partnerji ON Izvoz.Brodar = Partnerji.PaSifra INNER JOIN " +
                  "        KrajnjaDestinacija ON Izvoz.KrajnaDestinacija = KrajnjaDestinacija.ID INNER JOIN " +
                  "        VrstePostupakaUvoz ON Izvoz.Postupanje = VrstePostupakaUvoz.ID INNER JOIN " +
                  "        KontejnerskiTerminali ON Izvoz.MestoPreuzimanja = KontejnerskiTerminali.id INNER JOIN " +
                  "        MestaUtovara ON Izvoz.MesoUtovara = MestaUtovara.ID INNER JOIN " +
                  "        Carinarnice ON Izvoz.MestoCarinjenja = Carinarnice.ID INNER JOIN " +
                   "       Partnerji AS Partnerji_1 ON Izvoz.Spedicija = Partnerji_1.PaSifra INNER JOIN " +
                   "       AdresaStatusVozila ON Izvoz.AdresaSlanjaStatusa = AdresaStatusVozila.ID INNER JOIN " +
                   "       NaslovStatusaVozila ON Izvoz.NaslovSlanjaStatusa = NaslovStatusaVozila.ID INNER JOIN " +
                  "        VrstaCarinskogPostupka ON Izvoz.NapomenaReexport = VrstaCarinskogPostupka.id INNER JOIN " +
                   "       InspekciskiTretman ON Izvoz.Inspekcija = InspekciskiTretman.ID INNER JOIN " +
                   "       Partnerji AS Partnerji_2 ON Izvoz.Izvoznik = Partnerji_2.PaSifra INNER JOIN " +
                   "       Partnerji AS Partnerji_3 ON Izvoz.Klijent1 = Partnerji_3.PaSifra INNER JOIN " +
                    "      Partnerji AS Partnerji_4 ON Izvoz.Klijent2 = Partnerji_4.PaSifra INNER JOIN " +
                  "        Partnerji AS Partnerji_5 ON Izvoz.Klijent3 = Partnerji_5.PaSifra INNER JOIN " +
                 "         Partnerji AS Partnerji_6 ON Izvoz.SpediterRijeka = Partnerji_6.PaSifra INNER JOIN " +
                  "        uvNacinPakovanja ON Izvoz.NacinPakovanja = uvNacinPakovanja.ID order by Izvoz.ID desc ";

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
            var select = " SELECT  IzvozKonacna.ID as ID,    IzvozKonacna.VrstaKontejnera, TipKontenjera.Naziv, IzvozKonacna.BrojVagona, IzvozKonacna.BrojKontejnera, IzvozKonacna.BrodskaPlomba, IzvozKonacna.OstalePlombe, IzvozKonacna.BookingBrodara, Partnerji.PaNaziv, " +
                   "    IzvozKonacna.CutOffPort, IzvozKonacna.NetoRobe, IzvozKonacna.BrutoRobe, IzvozKonacna.BrutoRobeO, IzvozKonacna.BrojKoleta, IzvozKonacna.BrojKoletaO, IzvozKonacna.CBM, IzvozKonacna.CBMO, IzvozKonacna.VrednostRobeFaktura, " +
                    "  (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaManipulacije.Naziv as nvarchar(20))   FROM IzvozKonacnaVrstaManipulacije " +
    "       inner join VrstaManipulacije on VrstaManipulacije.ID = IzvozKonacnaVrstaManipulacije.IDVrstaManipulacije   where IzvozKonacnaVrstaManipulacije.IDNadredjena = IzvozKonacna.ID " +
    "        FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as VrsteUsluga,   " +
    "     (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaRobeHS.HSKod as nvarchar(20))   FROM IzvozKonacnaVrstaRobeHS " +
    "       inner join VrstaRobeHS on IzvozKonacnaVrstaRobeHS.IDVrstaRobeHS = VrstaRobeHS.ID   where IzvozKonacnaVrstaRobeHS.IDNadredjena = IzvozKonacna.ID " +
    "        FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as HS,   " +
    "    (SELECT  STUFF((SELECT distinct    '/' + Cast(NHM.Broj as nvarchar(20)) " +
   "            FROM IzvozKonacnaNHM  inner join NHM on IzvozKonacnaNHM.IDNHM = NHM.ID  where IzvozKonacnaNHM.IDNadredjena = IzvozKonacna.ID   FOR XML PATH('')), 1, 1, ''  ) As Skupljen) as NHM, " +
                   "        IzvozKonacna.Valuta, KrajnjaDestinacija.Naziv AS KrajnjaDestinacija, VrstePostupakaUvoz.Naziv AS Postupak, KontejnerskiTerminali.Naziv AS PPCNT, " +
                   "        KontejnerskiTerminali.Oznaka, IzvozKonacna.Cirada, IzvozKonacna.PlaniraniDatumUtovara, MestaUtovara.Naziv AS MestoUtovara, IzvozKonacna.KontaktOsoba,  " +
                   "        Carinarnice.Naziv AS Carinarnica, Carinarnice.CIOznaka, Partnerji_1.PaNaziv AS Spedicija, AdresaStatusVozila.Naziv AS AdresaStatusVozila, " +
                   "        NaslovStatusaVozila.Naziv AS NaslovStatusaVozila, IzvozKonacna.EtaLeget, VrstaCarinskogPostupka.Naziv AS Reexport, InspekciskiTretman.Naziv AS InspekciskiTretman, " +
                   "        IzvozKonacna.AutoDana, IzvozKonacna.NajavaVozila, IzvozKonacna.DodatneNapomeneDrumski, IzvozKonacna.Vaganje, IzvozKonacna.VGMTezina, IzvozKonacna.Tara, IzvozKonacna.VGMBrod, " +
                   "        Partnerji_2.PaNaziv AS Izvoznik, Partnerji_3.PaNaziv AS Klijent1, IzvozKonacna.Napomena1REf, IzvozKonacna.DobijenNalogKlijent1, Partnerji_4.PaNaziv AS klijent2, " +
                   "        IzvozKonacna.Napomena2REf, Partnerji_5.PaNaziv AS Klijent3, IzvozKonacna.Napomena3REf, Partnerji_6.PaNaziv AS SpediterRijeka, uvNacinPakovanja.Naziv AS NacinPakovanja, " +
                   "        IzvozKonacna.NacinPretovara " +
 "    FROM         IzvozKonacna INNER JOIN " +
                    "       TipKontenjera ON IzvozKonacna.VrstaKontejnera = TipKontenjera.ID INNER JOIN " +
                   "        Partnerji ON IzvozKonacna.Brodar = Partnerji.PaSifra INNER JOIN " +
                   "        KrajnjaDestinacija ON IzvozKonacna.KrajnaDestinacija = KrajnjaDestinacija.ID INNER JOIN " +
                   "        VrstePostupakaUvoz ON IzvozKonacna.Postupanje = VrstePostupakaUvoz.ID INNER JOIN " +
                   "        KontejnerskiTerminali ON IzvozKonacna.MestoPreuzimanja = KontejnerskiTerminali.id INNER JOIN " +
                   "        MestaUtovara ON IzvozKonacna.MesoUtovara = MestaUtovara.ID INNER JOIN " +
                   "        Carinarnice ON IzvozKonacna.MestoCarinjenja = Carinarnice.ID INNER JOIN " +
                    "       Partnerji AS Partnerji_1 ON IzvozKonacna.Spedicija = Partnerji_1.PaSifra INNER JOIN " +
                    "       AdresaStatusVozila ON IzvozKonacna.AdresaSlanjaStatusa = AdresaStatusVozila.ID INNER JOIN " +
                    "       NaslovStatusaVozila ON IzvozKonacna.NaslovSlanjaStatusa = NaslovStatusaVozila.ID INNER JOIN " +
                   "        VrstaCarinskogPostupka ON IzvozKonacna.NapomenaReexport = VrstaCarinskogPostupka.id INNER JOIN " +
                    "       InspekciskiTretman ON IzvozKonacna.Inspekcija = InspekciskiTretman.ID INNER JOIN " +
                    "       Partnerji AS Partnerji_2 ON IzvozKonacna.Izvoznik = Partnerji_2.PaSifra INNER JOIN " +
                    "       Partnerji AS Partnerji_3 ON IzvozKonacna.Klijent1 = Partnerji_3.PaSifra INNER JOIN " +
                     "      Partnerji AS Partnerji_4 ON IzvozKonacna.Klijent2 = Partnerji_4.PaSifra INNER JOIN " +
                   "        Partnerji AS Partnerji_5 ON IzvozKonacna.Klijent3 = Partnerji_5.PaSifra INNER JOIN " +
                  "         Partnerji AS Partnerji_6 ON IzvozKonacna.SpediterRijeka = Partnerji_6.PaSifra INNER JOIN " +
                   "        uvNacinPakovanja ON IzvozKonacna.NacinPakovanja = uvNacinPakovanja.ID  " +
            " where IzvozKonacna.IdNadredjena = " + Convert.ToInt32(txtNadredjeni.Text) + "  order by IzvozKonacna.ID desc ";


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
            cboIzvoznik.SelectedValue = pIzvoznik;
            if (txtNadredjeni.Text == "0")
            {
                FillGVIzvoz();
            }
            else
            {
                FillGVIzvozKonacnaPoPlanu();

            }
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
    }
}
