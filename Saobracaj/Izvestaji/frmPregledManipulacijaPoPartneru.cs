using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TrackModal.Izvestaji
{
    public partial class frmPregledManipulacijaPoPartneru : Syncfusion.Windows.Forms.Office2010Form
    {
        public static string code = "frmPregledManipulacijaPoPartneru";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Saobracaj.Sifarnici.frmLogovanje.user.ToString();
        string niz = "";
        public frmPregledManipulacijaPoPartneru()
        {
            InitializeComponent();

        }
        private void frmPregledManipulacijaPoPartneru_Load(object sender, EventArgs e)
        {
            var select3 = " Select Distinct PaSifra, PaNaziv From Partnerji order by PaNaziv";
            var s_connection3 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboPlatilac.DataSource = ds3.Tables[0];
            cboPlatilac.DisplayMember = "PaNaziv";
            cboPlatilac.ValueMember = "PaSifra";
        }

        private void RefreshDataGrid3()
        {

            var select = "    select  NaruceneManipulacije.Broj as Dokument, NaruceneManipulacije.IDPrijemaVoza, " +
             " NaruceneManipulacije.IDPrijemaKamionom,NaruceneManipulacije.BrojKontejnera,  VrstaManipulacije.Naziv, " +
            " CASE WHEN NaruceneManipulacije.Uradjeno > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Uradjeno, " +
            " NaruceneManipulacije.DatumUradjeno, " +
            " NaruceneManipulacije.DatumOd,NaruceneManipulacije.DatumDo, NaruceneManipulacije.Datum, NaruceneManipulacije.Korisnik, " +
            " NaruceneManipulacije.ID, NaruceneManipulacije.IzPrijema " +
            " from NaruceneManipulacije " +
            " inner join VrstaManipulacije on NaruceneManipulacije.VrstaManipulacije = VrstaManipulacije.ID " +
           " inner join Partnerji on NaruceneManipulacije.Platilac = Partnerji.PaSifra " +
           " where NaruceneManipulacije.Platilac = " + Convert.ToInt32(cboPlatilac.SelectedValue) + " and  NaruceneManipulacije.DatumOd >= '" + dtpDatumOd.Text + "' and NaruceneManipulacije.DatumDo <= '" + dtpDatumDo.Text + "'";

            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = false;
            dataGridView1.DataSource = ds.Tables[0];
            /*
                DataGridViewColumn column = dataGridView1.Columns[0];
                dataGridView1.Columns[0].HeaderText = "Prijem";
                dataGridView1.Columns[0].Width = 40;
                // dataGridView2.Columns[0].Visible = false;

                DataGridViewColumn column2 = dataGridView1.Columns[1];
                dataGridView1.Columns[1].HeaderText = "Broj kontejnera";
                dataGridView1.Columns[1].Width = 100;

                DataGridViewColumn column3 = dataGridView1.Columns[2];
                dataGridView1.Columns[2].HeaderText = "Man ID";
                dataGridView1.Columns[2].Width = 50;

                DataGridViewColumn column4 = dataGridView1.Columns[3];
                dataGridView1.Columns[3].HeaderText = "Manipulacija";
                dataGridView1.Columns[3].Width = 50;



                DataGridViewColumn column5 = dataGridView1.Columns[4];
                dataGridView1.Columns[4].HeaderText = "Urađeno";
                dataGridView1.Columns[4].Width = 50;

                DataGridViewColumn column6 = dataGridView1.Columns[5];
                dataGridView1.Columns[5].HeaderText = "Datum od";
                dataGridView1.Columns[5].Width = 80;

                DataGridViewColumn column7 = dataGridView1.Columns[6];
                dataGridView1.Columns[6].HeaderText = "Datum do";
                dataGridView1.Columns[6].Width = 80;

                DataGridViewColumn column8 = dataGridView1.Columns[7];
                dataGridView1.Columns[7].HeaderText = "Datum";
                dataGridView1.Columns[7].Width = 80;

                DataGridViewColumn column9 = dataGridView1.Columns[8];
                dataGridView1.Columns[8].HeaderText = "Korisnik";
                dataGridView1.Columns[8].Width = 80;

                DataGridViewColumn column10 = dataGridView1.Columns[9];
                dataGridView1.Columns[9].HeaderText = "ID";
                dataGridView1.Columns[9].Width = 70;

            */



        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid3();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshDataGridPoKontejneru();
        }

        private void RefreshDataGridPoKontejneru()
        {

            var select = "    select  NaruceneManipulacije.Broj as Dokument, NaruceneManipulacije.IDPrijemaVoza, " +
             " NaruceneManipulacije.IDPrijemaKamionom,NaruceneManipulacije.BrojKontejnera,  VrstaManipulacije.Naziv, " +
            " CASE WHEN NaruceneManipulacije.Uradjeno > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Uradjeno, " +
            " NaruceneManipulacije.DatumUradjeno, " +
            " NaruceneManipulacije.DatumOd,NaruceneManipulacije.DatumDo, NaruceneManipulacije.Datum, NaruceneManipulacije.Korisnik, " +
            " NaruceneManipulacije.ID, NaruceneManipulacije.IzPrijema " +
            " from NaruceneManipulacije " +
            " inner join VrstaManipulacije on NaruceneManipulacije.VrstaManipulacije = VrstaManipulacije.ID " +
           " inner join Partnerji on NaruceneManipulacije.Platilac = Partnerji.PaSifra " +
           " where NaruceneManipulacije.Platilac = " + Convert.ToInt32(cboPlatilac.SelectedValue) + " and  NaruceneManipulacije.DatumOd >= '" + dtpDatumOd.Text + "' and NaruceneManipulacije.DatumDo <= '" + dtpDatumDo.Text + "' and NaruceneManipulacije.BrojKontejnera = '" + txtBrojKontejnera.Text + "'";

            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = false;
            dataGridView1.DataSource = ds.Tables[0];
            /*
                DataGridViewColumn column = dataGridView1.Columns[0];
                dataGridView1.Columns[0].HeaderText = "Prijem";
                dataGridView1.Columns[0].Width = 40;
                // dataGridView2.Columns[0].Visible = false;

                DataGridViewColumn column2 = dataGridView1.Columns[1];
                dataGridView1.Columns[1].HeaderText = "Broj kontejnera";
                dataGridView1.Columns[1].Width = 100;

                DataGridViewColumn column3 = dataGridView1.Columns[2];
                dataGridView1.Columns[2].HeaderText = "Man ID";
                dataGridView1.Columns[2].Width = 50;

                DataGridViewColumn column4 = dataGridView1.Columns[3];
                dataGridView1.Columns[3].HeaderText = "Manipulacija";
                dataGridView1.Columns[3].Width = 50;



                DataGridViewColumn column5 = dataGridView1.Columns[4];
                dataGridView1.Columns[4].HeaderText = "Urađeno";
                dataGridView1.Columns[4].Width = 50;

                DataGridViewColumn column6 = dataGridView1.Columns[5];
                dataGridView1.Columns[5].HeaderText = "Datum od";
                dataGridView1.Columns[5].Width = 80;

                DataGridViewColumn column7 = dataGridView1.Columns[6];
                dataGridView1.Columns[6].HeaderText = "Datum do";
                dataGridView1.Columns[6].Width = 80;

                DataGridViewColumn column8 = dataGridView1.Columns[7];
                dataGridView1.Columns[7].HeaderText = "Datum";
                dataGridView1.Columns[7].Width = 80;

                DataGridViewColumn column9 = dataGridView1.Columns[8];
                dataGridView1.Columns[8].HeaderText = "Korisnik";
                dataGridView1.Columns[8].Width = 80;

                DataGridViewColumn column10 = dataGridView1.Columns[9];
                dataGridView1.Columns[9].HeaderText = "ID";
                dataGridView1.Columns[9].Width = 70;

            */



        }
    }
}
