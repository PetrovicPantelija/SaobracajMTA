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

namespace Testiranje.Sifarnici
{
    public partial class frmVrstaManipulacije : Form
    {
        public static string code = "frmVrstaManipulacije";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Saobracaj.Sifarnici.frmLogovanje.user.ToString();
        string niz = "";

        string KorisnikCene = "Panta";
        bool status = false;
        public frmVrstaManipulacije()
        {
            InitializeComponent();
            KorisnikCene = "Panta";
            IdGrupe();
            IdForme();
            PravoPristupa();
        }

        public frmVrstaManipulacije(string Korisnik)
        {
            InitializeComponent();
            KorisnikCene = Korisnik;
            IdGrupe();
            IdForme();
            PravoPristupa();
        }
        public string IdGrupe()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
                        tsNew.Enabled = false;
                    }
                    update = Convert.ToBoolean(reader["Izmena"]);
                    if (update == false)
                    {
                        tsSave.Enabled = false;
                    }
                    delete = Convert.ToBoolean(reader["Brisanje"]);
                    if (delete == false)
                    {
                        tsDelete.Enabled = false;
                    }
                }
            }

            conn.Close();
        }
        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtSifra.Enabled = false;
            txtNaziv.Text = "";
            txtJM.Text = "";
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            int uticeskladisno = 0;
            if (chkUticeSkladisno.Checked == true)
            {
                uticeskladisno = 1;
            
            }

            int administrativna= 0;
            if (chkAdministratvna.Checked == true)
            {
                administrativna = 1;

            }
            if (status == true)
            {
                InsertVrstaManipulacije ins = new InsertVrstaManipulacije();
                ins.InsVrstaManipulacije(txtNaziv.Text, Convert.ToDateTime(DateTime.Now), KorisnikCene, txtJM.Text, uticeskladisno, txtJM2.Text, Convert.ToInt32(cboTipManipulacije.SelectedValue), Convert.ToInt32(cboOrgJed.SelectedValue), txtOznaka.Text, txtRelacija.Text, Convert.ToDouble(txtCena.Value), Convert.ToInt32(cboGrupaVrsteManipulacije.SelectedValue), administrativna);
                status = false;
            }
            else
            {
                //int TipCenovnika ,int Komitent, double Cena , int VrstaManipulacije ,DateTime  Datum , string Korisnik
                InsertVrstaManipulacije upd = new InsertVrstaManipulacije();
                upd.UpdVrstaManipulacije(Convert.ToInt32(txtSifra.Text), txtNaziv.Text, Convert.ToDateTime(DateTime.Now), KorisnikCene, txtJM.Text, uticeskladisno, txtJM2.Text, Convert.ToInt32(cboTipManipulacije.SelectedValue), Convert.ToInt32(cboOrgJed.SelectedValue), txtOznaka.Text, txtRelacija.Text, Convert.ToDouble(txtCena.Value), Convert.ToInt32(cboGrupaVrsteManipulacije.SelectedValue), administrativna);
            }
            RefreshDataGrid();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da želite da brišete?", "Izbor", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                InsertVrstaManipulacije upd = new InsertVrstaManipulacije();
                upd.DeleteVrstaManipulacije(Convert.ToInt32(txtSifra.Text));
                RefreshDataGrid();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }

            
        }

        private void RefreshDataGrid()
        {
            var select = " SELECT VrstaManipulacije.[ID] as VID,VrstaManipulacije.Naziv as VrstaM,VrstaManipulacije.JM as JMM," +
                " CASE WHEN UticeSkladisno > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as UticeSkladisno, " +
                "VrstaManipulacije.[Datum] as Dat,VrstaManipulacije.[Korisnik] as Kor, TipManipulacije, VrstaManipulacije.OrgJed, OrganizacioneJedinice.Naziv, VrstaManipulacije.Oznaka, VrstaManipulacije.Relacija,VrstaManipulacije.Cena " +
             " FROM [dbo].[VrstaManipulacije] " +
             " inner join OrganizacioneJedinice on OrganizacioneJedinice.ID = VrstaManipulacije.OrgJed" +
             " order by VrstaManipulacije.[ID]";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Naziv";
            dataGridView1.Columns[1].Width = 350;


            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "JM";
            dataGridView1.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Utiče skladišno";
            dataGridView1.Columns[3].Width = 100;
          
            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Datum";
            dataGridView1.Columns[4].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Korisnik";
            dataGridView1.Columns[5].Width = 100;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Tip Manipulacije";
            dataGridView1.Columns[6].Width = 40;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "OrgJed";
            dataGridView1.Columns[7].Width = 80;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "OrgJed";
            dataGridView1.Columns[8].Width = 80;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Oznaka";
            dataGridView1.Columns[9].Width = 80;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Relacija";
            dataGridView1.Columns[10].Width = 120;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Cena";
            dataGridView1.Columns[11].Width = 80;


        }

        private void VratiPodatke(string ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT ID , Naziv, JM, UticeSkladisno, TipManipulacije, OrgJed, Oznaka,Relacija, Cena, GrupaVrsteManipulacijeID, Administrativna from VrstaManipulacije where ID=" + txtSifra.Text, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                // Convert.ToInt32(cboTipCenovnika.SelectedValue), Convert.ToInt32(cboKomitent.SelectedValue), Convert.ToDouble(txtCena.Text), Convert.ToInt32(cboVrstaManipulacije.SelectedValue), Convert.ToDateTime(DateTime.Now), KorisnikCene

                txtNaziv.Text = dr["Naziv"].ToString();
                txtJM.Text = dr["JM"].ToString();
                if (dr["UticeSkladisno"].ToString() == "True")
                { chkUticeSkladisno.Checked = true; }
                else { chkUticeSkladisno.Checked = false; }

                cboTipManipulacije.SelectedValue = Convert.ToInt32(dr["TipManipulacije"].ToString());
                cboOrgJed.SelectedValue = Convert.ToInt32(dr["OrgJed"].ToString());
                txtOznaka.Text = dr["Oznaka"].ToString();
                txtRelacija.Text = dr["Relacija"].ToString();
                txtCena.Value = Convert.ToDecimal(dr["Cena"].ToString());
                cboGrupaVrsteManipulacije.SelectedValue = Convert.ToInt32(dr["GrupaVrsteManipulacijeID"].ToString());

                if (dr["Administrativna"].ToString() == "1")
                { chkAdministratvna.Checked = true; }
                else { chkAdministratvna.Checked = false; }
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
                        txtSifra.Text = row.Cells[0].Value.ToString();
                        VratiPodatke(txtSifra.Text);
                        // txtOpis.Text = row.Cells[1].Value.ToString();
                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void frmVrstaManipulacije_Load(object sender, EventArgs e)
        {
            txtCena.Visible = false;
            label13.Visible = false;
            label9.Visible = false;

            RefreshDataGrid();

            var select = " Select ID, Naziv From TipManipulacije";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboTipManipulacije.DataSource = ds.Tables[0];
            cboTipManipulacije.DisplayMember = "Naziv";
            cboTipManipulacije.ValueMember = "ID";

            // SELECT ID, Naziv from OrganizacioneJedinice

            var select2 = " SELECT ID, Naziv from OrganizacioneJedinice order by ID";
            var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboOrgJed.DataSource = ds2.Tables[0];
            cboOrgJed.DisplayMember = "Naziv";
            cboOrgJed.ValueMember = "ID";


            var select3 = " SELECT ID, Naziv from GrupaVrsteManipulacije order by ID";
            var s_connection3 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboGrupaVrsteManipulacije.DataSource = ds3.Tables[0];
            cboGrupaVrsteManipulacije.DisplayMember = "Naziv";
            cboGrupaVrsteManipulacije.ValueMember = "ID";






        }

        private void tsPrvi_Click(object sender, EventArgs e)
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Min([ID]) as ID from VrstaManipulacije", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtSifra.Text = dr["ID"].ToString();
            }
            VratiPodatke(txtSifra.Text);
            con.Close();

        }

        private void tsPoslednja_Click(object sender, EventArgs e)
        {


            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Max([ID]) as ID from VrstaManipulacije", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtSifra.Text = dr["ID"].ToString();
            }
            VratiPodatke(txtSifra.Text);
            con.Close();



        }

        private void tsNazad_Click(object sender, EventArgs e)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            int prvi = 0;
            con.Open();

            SqlCommand cmd = new SqlCommand("select top 1 ID as ID from VrstaManipulacije where ID <" + Convert.ToInt32(txtSifra.Text) + " Order by ID desc", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                prvi = Convert.ToInt32(dr["ID"].ToString());
                txtSifra.Text = prvi.ToString();
            }

            con.Close();
            if ((Convert.ToInt32(txtSifra.Text) - 1) > prvi)
                VratiPodatke((Convert.ToInt32(txtSifra.Text) - 1).ToString());
            else
                VratiPodatke((Convert.ToInt32(prvi)).ToString());
        }

        private void VratiPodatkeMax()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Max([ID]) as ID from VrstaManipulacije", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtSifra.Text = dr["ID"].ToString();
            }

            con.Close();
        }

        private void tsNapred_Click(object sender, EventArgs e)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            int zadnji = 0;
            con.Open();

            SqlCommand cmd = new SqlCommand("select top 1 ID as ID from VrstaManipulacije where ID >" + Convert.ToInt32(txtSifra.Text) + " Order by ID", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                zadnji = Convert.ToInt32(dr["ID"].ToString());
                txtSifra.Text = zadnji.ToString();
            }

            con.Close();

            if ((Convert.ToInt32(txtSifra.Text) + 1) == zadnji)
                VratiPodatke((Convert.ToInt32(zadnji).ToString()));
            else
                VratiPodatke((Convert.ToInt32(txtSifra.Text) + 1).ToString());

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
