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
using System.Net;
using System.Net.Mail;

namespace TrackModal.Izvestaji
{
    public partial class frmPregledManipulacijaPoPartneru : Form
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
            IdGrupe();
            IdForme();
            PravoPristupa();
        }
        public string IdGrupe()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select IdGrupe from KorisnikGrupa Where Korisnik = " + "'" + Kor.TrimEnd() + "'";
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            int count = 0;

            while (dr.Read())
            {
                if (dr.HasRows)
                {
                    if (count == 0)
                    {
                        niz = dr["IdGrupe"].ToString();
                        count++;
                    }
                    else
                    {
                        niz = niz + "," + dr["IdGrupe"].ToString();
                        count++;
                    }
                }
                else
                {
                    MessageBox.Show("Korisnik ne pripada grupi");
                }

            }
            conn.Close();
            return niz;
        }
        private int IdForme()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select IdForme from Forme where Rtrim(Code)=" + "'" + code + "'";
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                idForme = Convert.ToInt32(dr["IdForme"].ToString());
            }
            conn.Close();
            return idForme;
        }

        private void PravoPristupa()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select * From GrupeForme Where IdGrupe in (" + niz + ") and IdForme=" + idForme;
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows == false)
            {
                MessageBox.Show("Nemate prava za pristup ovoj formi", code);
                Pravo = false;
            }
            else
            {
                Pravo = true;
                while (reader.Read())
                {
                    insert = Convert.ToBoolean(reader["Upis"]);
                    if (insert == false)
                    {
                       // tsNew.Enabled = false;
                    }
                    update = Convert.ToBoolean(reader["Izmena"]);
                    if (update == false)
                    {
                       // tsSave.Enabled = false;
                    }
                    delete = Convert.ToBoolean(reader["Brisanje"]);
                    if (delete == false)
                    {
                       // tsDelete.Enabled = false;
                    }
                }
            }

            conn.Close();
        }
        private void frmPregledManipulacijaPoPartneru_Load(object sender, EventArgs e)
        {
            var select3 = " Select Distinct ID, Naziv From Komitenti order by Naziv";
            var s_connection3 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboPlatilac.DataSource = ds3.Tables[0];
            cboPlatilac.DisplayMember = "Naziv";
            cboPlatilac.ValueMember = "ID";
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
    " inner join Komitenti on NaruceneManipulacije.Platilac = Komitenti.ID " +
    " where NaruceneManipulacije.Platilac = " + Convert.ToInt32(cboPlatilac.SelectedValue) + " and  NaruceneManipulacije.DatumOd >= '" + dtpDatumOd.Text + "' and NaruceneManipulacije.DatumDo <= '" + dtpDatumDo.Text + "'";

                var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
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
           " inner join Komitenti on NaruceneManipulacije.Platilac = Komitenti.ID " +
           " where NaruceneManipulacije.Platilac = " + Convert.ToInt32(cboPlatilac.SelectedValue) + " and  NaruceneManipulacije.DatumOd >= '" + dtpDatumOd.Text + "' and NaruceneManipulacije.DatumDo <= '" + dtpDatumDo.Text + "' and NaruceneManipulacije.BrojKontejnera = '" + txtBrojKontejnera.Text + "'";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
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
