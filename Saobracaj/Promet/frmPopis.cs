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

using Microsoft.Reporting.WinForms;
using Saobracaj.Sifarnici;

namespace TrackModal.Promet
{
    public partial class frmPopis : Syncfusion.Windows.Forms.Office2010Form
    {
        public static string code = "frmPopis";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Saobracaj.Sifarnici.frmLogovanje.user.ToString();
        string niz = "";

        string KorisnikCene = frmLogovanje.user;
        bool status = false;
        bool usao = false;
        public frmPopis()
        {
            InitializeComponent();

        }

        public frmPopis(string Korisnik)
        {
            KorisnikCene = Korisnik;
            InitializeComponent();

        }

        public frmPopis(int Broj, string Korisnik)
        {
            KorisnikCene = Korisnik;
           
            InitializeComponent();

            txtSifra.Text = Broj.ToString();
            VratiPodatke(Convert.ToInt32(txtSifra.Text));
            RefreshDataGrid();
        }
        
        private void VratiPodatke(int ID)
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select [ID] ,[DatumPopisa],[Napomena] from Popis where ID = " + ID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                dtpDatumPopisa.Value = Convert.ToDateTime(dr["DatumPopisa"].ToString());
              
                txtNapomena.Text = dr["Napomena"].ToString();
             

            }

            con.Close();

        }


        private void frmPopis_Load(object sender, EventArgs e)
        {
            var select3 = " Select Distinct ID, Naziv   From Skladista";
            var s_connection3 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
          //  cboSkladiste.DataSource = ds3.Tables[0];
          //  cboSkladiste.DisplayMember = "Naziv";
          //  cboSkladiste.ValueMember = "ID";


            var select4 = " Select Distinct ID, Naziv   From Skladista";
            var s_connection4 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection4 = new SqlConnection(s_connection4);
            var c4 = new SqlConnection(s_connection4);
            var dataAdapter4 = new SqlDataAdapter(select4, c4);

            var commandBuilder4 = new SqlCommandBuilder(dataAdapter4);
            var ds4 = new DataSet();
            dataAdapter4.Fill(ds4);
            cboSkladisteNovo.DataSource = ds4.Tables[0];
            cboSkladisteNovo.DisplayMember = "Naziv";
            cboSkladisteNovo.ValueMember = "ID";

            usao = true;
        }

        private void cboSkladisteNovo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (usao == true)
            {
                var select = " Select ID, Oznaka From Pozicija where Skladiste = " + Convert.ToInt32(cboSkladisteNovo.SelectedValue);
                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection myConnection = new SqlConnection(s_connection);
                var c = new SqlConnection(s_connection);
                var dataAdapter = new SqlDataAdapter(select, c);

                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new DataSet();
                dataAdapter.Fill(ds);
                cboPozicijaNovo.DataSource = ds.Tables[0];
                cboPozicijaNovo.DisplayMember = "Oznaka";
                cboPozicijaNovo.ValueMember = "ID";
            }
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtSifra.Enabled = false;
        }

        private void VratiPodatkeMax()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Max([ID]) as ID from Popis", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtSifra.Text = dr["ID"].ToString();
            }

            con.Close();
        }

        private void tsSave_Click(object sender, EventArgs e)
        { 
            if (status == true)
                {
                    /// ,  string RegBrKamiona,   string ImeVozaca,   int Vozom
                    InsertPopis ins = new InsertPopis();
                    ins.InsPopis(Convert.ToDateTime(dtpDatumPopisa.Text), txtNapomena.Text ,Convert.ToDateTime(DateTime.Now), KorisnikCene);
                    status = false;
                    VratiPodatkeMax();
                ins.InsStavkePopisa(Convert.ToInt32(txtSifra.Text),Convert.ToDateTime(dtpDatumPopisa.Value), KorisnikCene);

                }
                else
                {
                    //int TipCenovnika ,int Komitent, double Cena , int VrstaManipulacije ,DateTime  Datum , string Korisnik
                    InsertPopis upd = new InsertPopis();
                    upd.UpdPopis(Convert.ToInt32(txtSifra.Text), Convert.ToDateTime(dtpDatumPopisa.Text), txtNapomena.Text, Convert.ToDateTime(DateTime.Now), KorisnikCene);
                    status = false;
                }
            RefreshDataGrid();
           
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da želite da brišete?", "Izbor", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                InsertPopis upd = new InsertPopis();
                upd.DelPopis(Convert.ToInt32(txtSifra.Text));
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtSifra.Text == "")
            {
                MessageBox.Show("Niste uneli zaglavlje");
            }
            else
            {
                InsertPopis ins = new InsertPopis();
                ins.InsPopisStavke(Convert.ToInt32(txtSifra.Text), txtBrojKontejnera.Text, Convert.ToInt32(cboSkladisteNovo.SelectedValue), Convert.ToInt32(cboPozicijaNovo.SelectedValue), Convert.ToDateTime(DateTime.Now), KorisnikCene);
                RefreshDataGrid();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InsertPopis ins = new InsertPopis();
            ins.UpdPopisStavke(Convert.ToInt32(txtStavka.Text), Convert.ToInt32(txtSifra.Text), txtBrojKontejnera.Text, Convert.ToInt32(cboSkladisteNovo.SelectedValue), Convert.ToInt32(cboPozicijaNovo.SelectedValue), Convert.ToDateTime(DateTime.Now), KorisnikCene);
            RefreshDataGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da želite da brišete?", "Izbor", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                InsertPopis dels = new InsertPopis();
                dels.DelPopisStavke(Convert.ToInt32(txtStavka.Text));
                RefreshDataGrid();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }
        private void RefreshDataGrid()
        {
            var select = "  SELECT PopisStavke.ID AS Stavka,Popis.ID, Popis.DatumPopisa, Popis.Napomena, " +
           "  PopisStavke.BrojKontejnera, Skladista.Naziv, Pozicija.Oznaka,PopisStavke.Stanje,popisStavke.StvarnoStanje as StvarnoStanje,PopisStavke.SkladisteU as skU,popisstavke.LokacijaU as lokU " +
            "    FROM  Popis INNER JOIN " +
                         " PopisStavke ON Popis.ID = PopisStavke.IDNadredjenog INNER JOIN " +
                        " Skladista ON PopisStavke.SkladisteU = Skladista.ID INNER JOIN " +
                        " Pozicija ON PopisStavke.LokacijaU = Pozicija.ID " +
                           " where PopisStavke.IdNadredjenog = " + txtSifra.Text ;

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = false;
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns["StvarnoStanje"].ReadOnly = false;

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
            dataGridView1.Columns[0].HeaderText = "Stavka";
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].Visible = false;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Popis";
            dataGridView1.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Datum popisa";
            dataGridView1.Columns[2].Width = 70;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Napomena";
            dataGridView1.Columns[3].Width = 110;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Broj Kontejnera";
            dataGridView1.Columns[4].Width = 110;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Skladište";
            dataGridView1.Columns[5].Width = 80;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Pozicija";
            dataGridView1.Columns[6].Width = 80;

            dataGridView1.Columns["skU"].Visible = false;
            dataGridView1.Columns["lokU"].Visible = false;

        }

        private void VratiPodatkeStavke(string IdNadredjenog, int Stavka)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT [ID],[IDNadredjenog]       ,[BrojKontejnera] " +
             "  SkladisteU, LokacijaU " +
             " FROM PopisStavke " +
             " where IdNadredjenog = " + txtSifra.Text + " and ID = " + Stavka, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtStavka.Text = dr["ID"].ToString();
                txtBrojKontejnera.Text = dr["BrojKontejnera"].ToString();
                cboSkladisteNovo.SelectedValue = Convert.ToInt32(dr["SkladisteU"].ToString());
                cboPozicijaNovo.SelectedValue = Convert.ToInt32(dr["LokacijaU"].ToString());
            }

            con.Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txtSifra.Text = row.Cells[1].Value.ToString();
                        txtStavka.Text = row.Cells[0].Value.ToString();
                        VratiPodatkeStavke(txtSifra.Text, Convert.ToInt32(row.Cells[0].Value.ToString()));
                    }
                }
            }
            catch
            {
                
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
         frmPopisUporedniPrikaz2 pup = new frmPopisUporedniPrikaz2(Convert.ToInt32(txtSifra.Text));
            pup.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            frmPopisStampa popst = new frmPopisStampa(txtSifra.Text);
            popst.Show();
        }


        int PrStDok;

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            InsertPopis ins = new InsertPopis();
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select (Max(PrStDokumenta)+1) from Promet", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                PrStDok = Convert.ToInt32(dr[0].ToString());
            }
            con.Close();
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                string brojKontejnera = row.Cells["BrojKontejnera"].Value.ToString().TrimEnd();
                int skU = Convert.ToInt32(row.Cells["skU"].Value.ToString());
                int lokU = Convert.ToInt32(row.Cells["lokU"].Value.ToString());
                decimal stanje = Convert.ToDecimal(row.Cells["Stanje"].Value.ToString());
                decimal stvStanje = Convert.ToDecimal(row.Cells["StvarnoStanje"].Value.ToString());
                decimal razlika;
                if (stanje > stvStanje)
                {
                    razlika = stanje - stvStanje;
                    ins.InsPrometTA(Convert.ToDateTime(DateTime.Now), "OTP", PrStDok, row.Cells["BrojKontejnera"].Value.ToString().TrimEnd(), "OTV", 0, razlika, 0, 0, skU, lokU,DateTime.Now, KorisnikCene, 0, Convert.ToDateTime(dtpDatumPopisa.Value.ToString()));
                }
                if (stvStanje > stanje)
                {
                    razlika=stvStanje - stanje;
                    ins.InsPrometTA(Convert.ToDateTime(DateTime.Now), "PRI", PrStDok, row.Cells["BrojKontejnera"].Value.ToString().TrimEnd(), "OTV", razlika, 0, skU, lokU, 0, 0,DateTime.Now, KorisnikCene, 0, Convert.ToDateTime(dtpDatumPopisa.Value.ToString()));
                }
                //decimal razlika=Convert.ToDecimal(row)
            }
            RefreshDataGrid();
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["StvarnoStanje"].Value != null)
                {
                    if (row.Selected)
                    {
                        InsertPopis ins = new InsertPopis();
                        ins.InsStvarnoStanje(Convert.ToInt32(row.Cells[0].Value), Convert.ToDecimal(row.Cells["StvarnoStanje"].Value));
                    }
                }
            }
        }
    }
}
