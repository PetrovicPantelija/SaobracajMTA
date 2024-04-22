using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Uvoz
{
    public partial class frmPregledRadnihNalogaInterni : Form
    {
        bool status = false;
        string connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public frmPregledRadnihNalogaInterni()
        {
            InitializeComponent();
        }

        private void FillGV()
        {
            var select = "SELECT RadniNalogInterni.[ID] " +
 " ,[OJIzdavanja], o1.Naziv as Izdao " +
 " ,[OJRealizacije] ,o2.Naziv as Realizuje " +
 " ,[DatumIzdavanja] ,[DatumRealizacije] " +
 " ,RadniNalogInterni.[Napomena] ,[Uradjen] " +
 " ,[Osnov] ,[BrojOsnov], UvozKonacna.BrojKontejnera, VrstaManipulacije.Naziv, UvozKonacnaVrstaManipulacije.Kolicina" +
 " ,[KorisnikIzdao] ,[KorisnikZavrsio], IDManipulacijaJed as IDUMAN,  Cene.Komitent as NalogodavacCena, Cene.Uvoznik as UvoznikCena, " +
 " Cene.VrstaManipulacije " +
 " FROM [RadniNalogInterni] " +
 " inner join UvozKonacna on UvozKonacna.ID = RadniNalogInterni.BrojOsnov " +
 " inner join OrganizacioneJedinice as o1 on OjIzdavanja = O1.ID " +
 " inner join OrganizacioneJedinice as o2 on OjRealizacije = O2.ID " +
 " inner join UvozKonacnaVrstaManipulacije on UvozKonacnaVrstaManipulacije.ID = RadniNalogInterni.IDManipulacijaJed " +
 " inner join Cene on UvozKonacnaVrstaManipulacije.IDVrstaManipulacije = Cene.ID " +
 " inner join VrstaManipulacije on Cene.VrstaManipulacije = VrstaManipulacije.ID " +
 " " +
 " union " +
 "SELECT RadniNalogInterni.[ID] " +
 " ,[OJIzdavanja], o1.Naziv as Izdao " +
 " ,[OJRealizacije] ,o2.Naziv as Realizuje " +
 " ,[DatumIzdavanja] ,[DatumRealizacije] " +
 " ,RadniNalogInterni.[Napomena] ,[Uradjen] " +
 " ,[Osnov] ,[BrojOsnov], IzvozKonacna.BrojKontejnera, VrstaManipulacije.Naziv, IzvozKonacnaVrstaManipulacije.Kolicina" +
 " ,[KorisnikIzdao] ,[KorisnikZavrsio], IDManipulacijaJed as IDUMAN,  Cene.Komitent as NalogodavacCena, Cene.Uvoznik as IzvoznikkCena, " +
 " Cene.VrstaManipulacije " +
 " FROM [RadniNalogInterni] " +
 " inner join IzvozKonacna on IzvozKonacna.ID = RadniNalogInterni.BrojOsnov " +
 " inner join OrganizacioneJedinice as o1 on OjIzdavanja = O1.ID " +
 " inner join OrganizacioneJedinice as o2 on OjRealizacije = O2.ID " +
 " inner join IzvozKonacnaVrstaManipulacije on IzvozKonacnaVrstaManipulacije.ID = RadniNalogInterni.IDManipulacijaJed " +
 " inner join Cene on IzvozKonacnaVrstaManipulacije.IDVrstaManipulacije = Cene.ID " +
 " inner join VrstaManipulacije on Cene.VrstaManipulacije = VrstaManipulacije.ID " +
 "  ";
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
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "OJIZD";
            dataGridView1.Columns[1].Width = 40;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Izdao";
            dataGridView1.Columns[2].Width = 90;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "OJ REAL";
            dataGridView1.Columns[3].Width = 40;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Realizuje";
            dataGridView1.Columns[4].Width = 90;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Datum izdavanja";
            dataGridView1.Columns[5].Width = 90;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Datum realizacije";
            dataGridView1.Columns[6].Width = 90;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Napomena";
            dataGridView1.Columns[7].Width = 90;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Uradjen";
            dataGridView1.Columns[8].Width = 40;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Osnov";
            dataGridView1.Columns[9].Width = 70;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Br kon";
            dataGridView1.Columns[10].Width = 40;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Kontejner";
            dataGridView1.Columns[11].Width = 90;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Manipulacija";
            dataGridView1.Columns[12].Width = 150;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Kolicina";
            dataGridView1.Columns[13].Width = 60;

        }

        private void FillGVOtvoreni()
        {
            var select = "SELECT RadniNalogInterni.[ID] " +
" ,[OJIzdavanja], o1.Naziv as Izdao " +
" ,[OJRealizacije] ,o2.Naziv as Realizuje " +
" ,[DatumIzdavanja] ,[DatumRealizacije] " +
" ,RadniNalogInterni.[Napomena] ,[Uradjen] " +
" ,[Osnov] ,[BrojOsnov], UvozKonacna.BrojKontejnera, VrstaManipulacije.Naziv, UvozKonacnaVrstaManipulacije.Kolicina" +
" ,[KorisnikIzdao] ,[KorisnikZavrsio], IDManipulacijaJed as IDUMAN,  Cene.Komitent as NalogodavacCena, Cene.Uvoznik as UvoznikCena, " +
" Cene.VrstaManipulacije " +
" FROM [RadniNalogInterni] " +
" inner join UvozKonacna on UvozKonacna.ID = RadniNalogInterni.BrojOsnov " +
" inner join OrganizacioneJedinice as o1 on OjIzdavanja = O1.ID " +
" inner join OrganizacioneJedinice as o2 on OjRealizacije = O2.ID " +
" inner join UvozKonacnaVrstaManipulacije on UvozKonacnaVrstaManipulacije.ID = RadniNalogInterni.IDManipulacijaJed " +
" inner join Cene on UvozKonacnaVrstaManipulacije.IDVrstaManipulacije = Cene.ID " +
" inner join VrstaManipulacije on Cene.VrstaManipulacije = VrstaManipulacije.ID " +
            " where Uradjen = 0 " +
" order by RadniNalogInterni.ID desc";
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
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "OJIZD";
            dataGridView1.Columns[1].Width = 40;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Izdao";
            dataGridView1.Columns[2].Width = 90;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "OJ REAL";
            dataGridView1.Columns[3].Width = 40;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Realizuje";
            dataGridView1.Columns[4].Width = 90;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Datum izdavanja";
            dataGridView1.Columns[5].Width = 90;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Datum realizacije";
            dataGridView1.Columns[6].Width = 90;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Napomena";
            dataGridView1.Columns[7].Width = 90;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Uradjen";
            dataGridView1.Columns[8].Width = 40;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Osnov";
            dataGridView1.Columns[9].Width = 70;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Br kon";
            dataGridView1.Columns[10].Width = 40;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Kontejner";
            dataGridView1.Columns[11].Width = 90;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Manipulacija";
            dataGridView1.Columns[12].Width = 150;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Kolicina";
            dataGridView1.Columns[13].Width = 60;


        }

        private void FillGVZatvoreni()
        {
            var select = "SELECT RadniNalogInterni.[ID] " +
" ,[OJIzdavanja], o1.Naziv as Izdao " +
" ,[OJRealizacije] ,o2.Naziv as Realizuje " +
" ,[DatumIzdavanja] ,[DatumRealizacije] " +
" ,RadniNalogInterni.[Napomena] ,[Uradjen] " +
" ,[Osnov] ,[BrojOsnov], UvozKonacna.BrojKontejnera, VrstaManipulacije.Naziv, UvozKonacnaVrstaManipulacije.Kolicina" +
" ,[KorisnikIzdao] ,[KorisnikZavrsio], IDManipulacijaJed as IDUMAN,  Cene.Komitent as NalogodavacCena, Cene.Uvoznik as UvoznikCena, " +
" Cene.VrstaManipulacije " +
" FROM [RadniNalogInterni] " +
" inner join UvozKonacna on UvozKonacna.ID = RadniNalogInterni.BrojOsnov " +
" inner join OrganizacioneJedinice as o1 on OjIzdavanja = O1.ID " +
" inner join OrganizacioneJedinice as o2 on OjRealizacije = O2.ID " +
" inner join UvozKonacnaVrstaManipulacije on UvozKonacnaVrstaManipulacije.ID = RadniNalogInterni.IDManipulacijaJed " +
" inner join Cene on UvozKonacnaVrstaManipulacije.IDVrstaManipulacije = Cene.ID " +
" inner join VrstaManipulacije on Cene.VrstaManipulacije = VrstaManipulacije.ID " +
            " where Uradjen = 1 " +
" order by RadniNalogInterni.ID desc";

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
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "OJIZD";
            dataGridView1.Columns[1].Width = 40;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Izdao";
            dataGridView1.Columns[2].Width = 90;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "OJ REAL";
            dataGridView1.Columns[3].Width = 40;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Realizuje";
            dataGridView1.Columns[4].Width = 90;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Datum izdavanja";
            dataGridView1.Columns[5].Width = 90;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Datum realizacije";
            dataGridView1.Columns[6].Width = 90;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Napomena";
            dataGridView1.Columns[7].Width = 90;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Uradjen";
            dataGridView1.Columns[8].Width = 40;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Osnov";
            dataGridView1.Columns[9].Width = 70;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Br kon";
            dataGridView1.Columns[10].Width = 40;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Kontejner";
            dataGridView1.Columns[11].Width = 90;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Manipulacija";
            dataGridView1.Columns[12].Width = 150;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Kolicina";
            dataGridView1.Columns[13].Width = 60;


        }

        private void FillGVOrganizacionaJedinica()
        {
            var select = "SELECT RadniNalogInterni.[ID] " +
" ,[OJIzdavanja], o1.Naziv as Izdao " +
" ,[OJRealizacije] ,o2.Naziv as Realizuje " +
" ,[DatumIzdavanja] ,[DatumRealizacije] " +
" ,RadniNalogInterni.[Napomena] ,[Uradjen] " +
" ,[Osnov] ,[BrojOsnov], UvozKonacna.BrojKontejnera, VrstaManipulacije.Naziv, UvozKonacnaVrstaManipulacije.Kolicina" +
" ,[KorisnikIzdao] ,[KorisnikZavrsio], IDManipulacijaJed as IDUMAN,  Cene.Komitent as NalogodavacCena, Cene.Uvoznik as UvoznikCena, " +
" Cene.VrstaManipulacije " +
" FROM [RadniNalogInterni] " +
" inner join UvozKonacna on UvozKonacna.ID = RadniNalogInterni.BrojOsnov " +
" inner join OrganizacioneJedinice as o1 on OjIzdavanja = O1.ID " +
" inner join OrganizacioneJedinice as o2 on OjRealizacije = O2.ID " +
" inner join UvozKonacnaVrstaManipulacije on UvozKonacnaVrstaManipulacije.ID = RadniNalogInterni.IDManipulacijaJed " +
" inner join Cene on UvozKonacnaVrstaManipulacije.IDVrstaManipulacije = Cene.ID " +
" inner join VrstaManipulacije on Cene.VrstaManipulacije = VrstaManipulacije.ID " +
            " where O2.ID =  " + Convert.ToInt32(cboOrgJed.SelectedValue) +
" order by RadniNalogInterni.ID desc";

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
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "OJIZD";
            dataGridView1.Columns[1].Width = 40;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Izdao";
            dataGridView1.Columns[2].Width = 90;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "OJ REAL";
            dataGridView1.Columns[3].Width = 40;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Realizuje";
            dataGridView1.Columns[4].Width = 90;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Datum izdavanja";
            dataGridView1.Columns[5].Width = 90;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Datum realizacije";
            dataGridView1.Columns[6].Width = 90;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Napomena";
            dataGridView1.Columns[7].Width = 90;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Uradjen";
            dataGridView1.Columns[8].Width = 40;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Osnov";
            dataGridView1.Columns[9].Width = 70;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Br kon";
            dataGridView1.Columns[10].Width = 40;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Kontejner";
            dataGridView1.Columns[11].Width = 90;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Manipulacija";
            dataGridView1.Columns[12].Width = 150;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Kolicina";
            dataGridView1.Columns[13].Width = 60;


        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            InsertRadniNalogInterni ins = new InsertRadniNalogInterni();
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        ins.UpdRadniNalogInterniZavrsen(Convert.ToInt32(row.Cells[0].Value.ToString()), "");
                    }
                }
            }
            catch { }
            FillGVOtvoreni();

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            FillGV();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            FillGVOtvoreni();
        }

        private void frmPregledRadnihNalogaInterni_Load(object sender, EventArgs e)
        {
            var partner2 = "Select ID, Naziv From OrganizacioneJedinice order by Naziv";
            var partAD2 = new SqlDataAdapter(partner2, connection);
            var partDS2 = new DataSet();
            partAD2.Fill(partDS2);
            cboOrgJed.DataSource = partDS2.Tables[0];
            cboOrgJed.DisplayMember = "Naziv";
            cboOrgJed.ValueMember = "ID";
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            FillGVOrganizacionaJedinica();
        }
    }
}
