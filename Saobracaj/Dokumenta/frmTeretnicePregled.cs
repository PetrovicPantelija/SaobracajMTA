using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Dokumenta
{
    public partial class frmTeretnicePregled : Form
    {
        public static string code = "frmTeretnicePregled";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Sifarnici.frmLogovanje.user.ToString();
        public int Teren;
        string niz = "";

        string Korisnik = "";
        public frmTeretnicePregled()
        {
            InitializeComponent();

        }
        public frmTeretnicePregled(string KorisnikPregled)
        {
            InitializeComponent();
            Korisnik = KorisnikPregled;

        }

        private void frmTeretnicePregled_Load(object sender, EventArgs e)
        {
            var select = " SELECT  top 3000 d1.ID, d1.BrojTeretnice, stanice.Opis AS StanicaOd, " +
" stanice_1.Opis AS StanicaDo, stanice_2.Opis AS StanicaPopisa, d1.VremeOd, " +
" d1.VremeDo, d1.BrojLista,( " +
" SELECT  " +
"  STUFF( " +
 "   ( " +
 "   SELECT distinct " +
 "     '/' + Cast(IDNajave as nvarchar(6)) " +
 "   FROM TeretnicaStavke " +
 "   where TeretnicaStavke.BrojTeretnice = d1.ID " +
 "   FOR XML PATH('') " +
 "   ), 1, 1, '' " +
 " ) As Skupljen) as Najave, TrainList.KomOznaka, d1.Korisnik " +
" FROM Teretnica d1 INNER JOIN  stanice ON d1.StanicaOd = stanice.ID " +
" INNER JOIN stanice AS stanice_1 ON d1.StanicaDo = stanice_1.ID " +
" INNER JOIN  stanice AS stanice_2 ON d1.StanicaPopisa = stanice_2.ID  " +
" left join TrainList on d1.TrainListID = TrainList.ID " +
"  order by d1.ID desc";
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

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 60;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Voz broj";
            dataGridView1.Columns[1].Width = 100;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Stanica Od";
            dataGridView1.Columns[2].Width = 120;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Stanica Do";
            dataGridView1.Columns[3].Width = 120;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Stanica Popisa";
            dataGridView1.Columns[4].Width = 120;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Vreme Od";
            dataGridView1.Columns[5].Width = 100;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Vreme Do";
            dataGridView1.Columns[6].Width = 100;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Broj lista";
            dataGridView1.Columns[7].Width = 100;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Najave";
            dataGridView1.Columns[8].Width = 100;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Posao";
            dataGridView1.Columns[9].Width = 100;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Korisnik";
            dataGridView1.Columns[10].Width = 150;


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
            frmTeretnica ter = new frmTeretnica(txtSifra.Text, Korisnik);
            ter.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var select = " SELECT  top 3000 d1.ID, d1.BrojTeretnice, stanice.Opis AS StanicaOd, " +
" stanice_1.Opis AS StanicaDo, stanice_2.Opis AS StanicaPopisa, d1.VremeOd, " +
" d1.VremeDo, d1.BrojLista,( " +
" SELECT  " +
"  STUFF( " +
 "   ( " +
 "   SELECT distinct " +
 "     '/' + Cast(IDNajave as nvarchar(6)) " +
 "   FROM TeretnicaStavke " +
 "   where TeretnicaStavke.BrojTeretnice = d1.ID " +
 "   FOR XML PATH('') " +
 "   ), 1, 1, '' " +
 " ) As Skupljen) as Najave, TrainList.KomOznaka, d1.Korisnik " +
" FROM Teretnica d1 INNER JOIN  stanice ON d1.StanicaOd = stanice.ID " +
" INNER JOIN stanice AS stanice_1 ON d1.StanicaDo = stanice_1.ID " +
" INNER JOIN  stanice AS stanice_2 ON d1.StanicaPopisa = stanice_2.ID  " +
" left join TrainList on d1.TrainListID = TrainList.ID " +
"  order by d1.ID desc";
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

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 60;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Voz broj";
            dataGridView1.Columns[1].Width = 100;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Stanica Od";
            dataGridView1.Columns[2].Width = 120;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Stanica Do";
            dataGridView1.Columns[3].Width = 120;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Stanica Popisa";
            dataGridView1.Columns[4].Width = 120;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Vreme Od";
            dataGridView1.Columns[5].Width = 100;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Vreme Do";
            dataGridView1.Columns[6].Width = 100;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Broj lista";
            dataGridView1.Columns[7].Width = 100;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Najave";
            dataGridView1.Columns[8].Width = 100;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Posao";
            dataGridView1.Columns[9].Width = 100;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Korisnik";
            dataGridView1.Columns[10].Width = 150;

        }

        private void tsDelete_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            frmTeretnica ter = new frmTeretnica(Kor);
            ter.Show();
        }

        private void tsNew_Click(object sender, EventArgs e)
        {

        }
    }
}
