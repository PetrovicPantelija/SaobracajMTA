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

namespace Saobracaj.Izvoz
{
    public partial class frmKontaktOsobeMU : Form
    {
        bool status = false;
        int IzPartnera = 0;
        int pomPartner = 0;

        public static string code = "frmKontaktOsobeMU";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Saobracaj.Sifarnici.frmLogovanje.user.ToString();
        string niz = "";
        public frmKontaktOsobeMU()
        {
            InitializeComponent();
            IzPartnera = 0;
            IdGrupe();
            IdForme();
            PravoPristupa();
        }
        public frmKontaktOsobeMU(int Partner)
        {
            InitializeComponent();
            IzPartnera = 1;
            pomPartner = Partner;
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
        private void frmKontaktOsobeMU_Load(object sender, EventArgs e)
        {
            var select = " Select Distinct ID, Naziv  From MestaUtovara";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            txtPaKOSifra.DataSource = ds.Tables[0];
            txtPaKOSifra.DisplayMember = "Naziv";
            txtPaKOSifra.ValueMember = "ID";

            if (IzPartnera == 1)
            {
                txtPaKOSifra.SelectedValue = pomPartner;
                RefreshDataGridPoPartneru();
               
            }
            else
            {
                RefreshDataGrid();
            }
        }

        private void RefreshDataGrid()
        {
            var select = " SELECT [PaKOZapSt]      ,[PaKOSifra]      ,[PaKOIme]      ,[PaKOPriimek]      ,[PaKOOddelek]      ,[PaKOTel] " +
     " ,[PaKOMail]      ,[PaKOOpomba]      ,[Operatika]  FROM [TESTIRANJE].[dbo].[partnerjiKontOsebaMU]";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
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
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Partner ID";
            dataGridView1.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Ime";
            dataGridView1.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Prezime";
            dataGridView1.Columns[3].Width = 100;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Odeljenje";
            dataGridView1.Columns[4].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Tel";
            dataGridView1.Columns[5].Width = 70;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Email";
            dataGridView1.Columns[6].Width = 70;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Adresa";
            dataGridView1.Columns[7].Width = 120;



        }

        private void RefreshDataGridPoPartneru()
        {
            var select = " SELECT [PaKOZapSt]      ,[PaKOSifra]      ,[PaKOIme]      ,[PaKOPriimek]      ,[PaKOOddelek]      ,[PaKOTel] " +
     " ,[PaKOMail]      ,[PaKOOpomba]      ,[Operatika]  FROM [TESTIRANJE].[dbo].[partnerjiKontOsebaMU] Where PaKOSifra = " + txtPaKOSifra.SelectedValue;

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
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
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Partner ID";
            dataGridView1.Columns[1].Width = 150;



        }

      
            public string GetKontakt()
            {
                return txtPaKOIme.Text.TrimEnd() + " " + txtPaKOPriimek.Text.TrimEnd() + " " + txtPaKOTel.Text.TrimEnd();
            }

        public string GetKontaktAdresa(int Partner)
        {

            RefreshDataGridPoPartneru();
            return txtPaKOOpomba.Text.TrimEnd();
        }



        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtPaKOZapSt.Enabled = false;

            txtPaKOZapSt.Text = "";
            txtPaKOSifra.Text = "";
            txtPaKOIme.Text = "";
            txtPaKOPriimek.Text = "";
            txtPaKOOddelek.Text = "";
            txtPaKOTel.Text = "";
            txtPaKOMail.Text = "";
            txtPaKOOpomba.Text = "";
            chkOperatika.Checked = false;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            int pomOperater = 0;
            if (chkOperatika.Checked == true)
            {
                pomOperater = 1;
            }

            if (status == true)
            {

                Sifarnici.InsertKontaktOsobeMU ins = new Sifarnici.InsertKontaktOsobeMU();
                ins.InsKontaktOsoba(Convert.ToInt32(txtPaKOSifra.SelectedValue), txtPaKOIme.Text, txtPaKOPriimek.Text, txtPaKOOddelek.Text, txtPaKOTel.Text, txtPaKOMail.Text, txtPaKOOpomba.Text, pomOperater);
            }
            else
            {
                Sifarnici.InsertKontaktOsobe upd = new Sifarnici.InsertKontaktOsobe();
                upd.UpdKontaktOsoba(Convert.ToInt32(txtPaKOZapSt.Text), Convert.ToInt32(txtPaKOSifra.SelectedValue), txtPaKOIme.Text, txtPaKOPriimek.Text, txtPaKOOddelek.Text, txtPaKOTel.Text, txtPaKOMail.Text, txtPaKOOpomba.Text, pomOperater);
            }
            RefreshDataGrid();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {

            Sifarnici.InsertKontaktOsobe upd = new Sifarnici.InsertKontaktOsobe();
            upd.DelKontaktOsoba(Convert.ToInt32(txtPaKOZapSt.Text));
            RefreshDataGrid();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {

                        txtPaKOZapSt.Text = row.Cells[0].Value.ToString();
                        // txtPaKOSifra.Text = row.Cells[1].Value.ToString();
                        txtPaKOIme.Text = row.Cells[2].Value.ToString();
                        txtPaKOPriimek.Text = row.Cells[3].Value.ToString();
                        txtPaKOOddelek.Text = row.Cells[4].Value.ToString();
                        txtPaKOTel.Text = row.Cells[5].Value.ToString();
                        txtPaKOMail.Text = row.Cells[6].Value.ToString();
                        txtPaKOOpomba.Text = row.Cells[7].Value.ToString();

                        if (row.Cells[7].Value.ToString() == "1")
                        { chkOperatika.Checked = true; }
                        else
                        {
                            chkOperatika.Checked = false;
                        }
                        txtPaKOSifra.SelectedValue = Convert.ToInt32(row.Cells[1].Value.ToString());
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }
    }
}
