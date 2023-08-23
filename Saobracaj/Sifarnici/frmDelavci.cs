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

namespace Saobracaj.Sifarnici
{
    public partial class frmDelavci : Form
    {
        bool status = false;
        public frmDelavci()
        {
            InitializeComponent();
        }

        private void frmDelavci_Load(object sender, EventArgs e)
        {
            var select2 = " Select Distinct DmSifra, DmNaziv  From DelovnaMesta";
            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            txtDeSifDelMes.DataSource = ds2.Tables[0];
            txtDeSifDelMes.DisplayMember = "DmNaziv";
            txtDeSifDelMes.ValueMember = "DmSifra";

            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            var select = " SELECT     Delavci.DeSifra, RTRIM(Delavci.DePriimek) + ' ' + RTRIM(Delavci.DeIme) AS Naziv, Delavci.DeTelefon1, Delavci.DeTelefon2, Delavci.DeEMail, " +
                      "   CASE WHEN Delavci.Masinovodja > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Masinovodja , " +
                       "   CASE WHEN Delavci.Pregledac  > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Pomocnik , " +
                         "   CASE WHEN Delavci.Vozovodja   > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Vozovodja  , " +
                          "   CASE WHEN Delavci.PregledacKola   > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as PregledacKola  , " +
                          "   CASE WHEN Delavci.Manevrista   > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Manevrista  , " +
                     "   DelovnaMesta.DmNaziv, Delavci.DeUlHisStBivS, Delavci.DeKrajBivS " +
                     " FROM         Delavci INNER JOIN " +
                     "  DelovnaMesta ON Delavci.DeSifDelMes = DelovnaMesta.DmSifra where DeSifStat <> 'P'";



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
            dataGridView1.Columns[0].HeaderText = "Šifra";
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Naziv";
            dataGridView1.Columns[1].Width = 150;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Telefon - 1";
            dataGridView1.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Telefon - 2";
            dataGridView1.Columns[3].Width = 100;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "E-mail";
            dataGridView1.Columns[4].Width = 150;



            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Masinovodja";
            dataGridView1.Columns[5].Width = 50;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Pomocnik";
            dataGridView1.Columns[6].Width = 50;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Vozovođa";
            dataGridView1.Columns[7].Width = 50;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Preg kola";
            dataGridView1.Columns[8].Width = 50;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Manevrista";
            dataGridView1.Columns[9].Width = 50;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Radno mesto";
            dataGridView1.Columns[10].Width = 100;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Ulica";
            dataGridView1.Columns[11].Width = 100;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Mesto";
            dataGridView1.Columns[12].Width = 100;



        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtDeSifra.Enabled = false;
            txtDeSifra.Text = "";
            txtDePriimek.Text = "";
            txtDeIme.Text = "";
            txtDeTelefon1.Text = "";
            txtDeTelefon2.Text = "";
            txtDeEMail.Text = "";
            txtDeUlHisStBivS.Text = "";
            txtDeKrajBivS.Text = "";
            txtDeSifDelMes.Text = "";
            txtDeSifStat.Text = "";
            chkManevrista.Checked = false;
            chkMasinovodja.Checked = false;
            chkPomocnik.Checked = false;
            chkPregledacKola.Checked = false;
            chkVozovodja.Checked = false;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            int PomManevrista, PomPomocnik, PomVozovodja, PomPregledacKola, PomMasinovodja;

            if (chkManevrista.Checked)
            {
                PomManevrista = 1;
            }
            else
            {
                PomManevrista = 0;
            }

            if (chkPomocnik.Checked)
            {
                PomPomocnik = 1;
            }
            else
            {
                PomPomocnik = 0;
            }

            if (chkVozovodja.Checked)
            {
                PomVozovodja = 1;
            }
            else
            {
                PomVozovodja = 0;
            }

            if (chkPregledacKola.Checked)
            {
                PomPregledacKola = 1;
            }
            else
            {
                PomPregledacKola = 0;
            }

            if (chkMasinovodja.Checked)
            {
                PomMasinovodja = 1;
            }
            else
            {
                PomMasinovodja = 0;
            }
            if (status == true)
            {
               // txtDeSifra.Text,  txtDePriimek.Text,  txtDeIme.Text, txtDeTelefon1.Text,  txtDeTelefon2.Text ,  txtDeEMail.Text , txtDeUlHisStBivS.Text , txtDeKrajBivS.Text , txtDeSifDelMes.Text ,  txtDeSifStat.Text ,  PomManevrista, PomPomocnik, PomVozovodja, PomPregledacKola, PomMasinovodja)
                //  txtNaziv.Text,  txtUlica.Text,  txtMesto.Text,  txtOblast.Text, txtPosta.Text ,txtDrzava.Text, txtTelefon.Text, txtTR.Text ,  txtNapomena.Text,txtMaticniBroj.Text,  txtEmail.Text,  txtPIB.Text
                InsertDelavciMTA ins = new InsertDelavciMTA();
                ins.InsDelavciMTA( txtDePriimek.Text, txtDeIme.Text, txtDeTelefon1.Text, txtDeTelefon2.Text, txtDeEMail.Text, txtDeUlHisStBivS.Text, txtDeKrajBivS.Text, Convert.ToInt32(txtDeSifDelMes.SelectedValue), txtDeSifStat.Text,  PomManevrista, PomPomocnik, PomVozovodja, PomPregledacKola, PomMasinovodja);
            }
            else
            {
                InsertDelavciMTA upd = new InsertDelavciMTA();
                upd.UpdDelavciMTA(Convert.ToInt32(txtDeSifra.Text), txtDePriimek.Text, txtDeIme.Text, txtDeTelefon1.Text, txtDeTelefon2.Text, txtDeEMail.Text, txtDeUlHisStBivS.Text, txtDeKrajBivS.Text, Convert.ToInt32(txtDeSifDelMes.SelectedValue), txtDeSifStat.Text,  PomManevrista, PomPomocnik, PomVozovodja, PomPregledacKola, PomMasinovodja);
            }
            RefreshDataGrid();
        }

        private void VratiPodatke(string ID)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT     Delavci.DeSifra, RTRIM(Delavci.DePriimek) as Prezime, RTRIM(Delavci.DeIme) AS Ime, Delavci.DeTelefon1, Delavci.DeTelefon2, Delavci.DeEMail, " +
                      "   CASE WHEN Delavci.Masinovodja > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Masinovodja , " +
                       "   CASE WHEN Delavci.Pregledac  > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Pomocnik , " +
                         "   CASE WHEN Delavci.Vozovodja   > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Vozovodja  , " +
                          "   CASE WHEN Delavci.PregledacKola   > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as PregledacKola  , " +
                          "   CASE WHEN Delavci.Manevrista   > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Manevrista  , " +
                     "   DelovnaMesta.DmSifra, DelovnaMesta.DmNaziv, Delavci.DeUlHisStBivS, Delavci.DeKrajBivS, Delavci.DeSifStat " +
                     " FROM         Delavci INNER JOIN " +
                     "  DelovnaMesta ON Delavci.DeSifDelMes = DelovnaMesta.DmSifra where DeSifStat <> 'P' ANd    Delavci.DeSifra=" + ID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                // Convert.ToInt32(cboTipCenovnika.SelectedValue), Convert.ToInt32(cboKomitent.SelectedValue), Convert.ToDouble(txtCena.Text), Convert.ToInt32(cboVrstaManipulacije.SelectedValue), Convert.ToDateTime(DateTime.Now), KorisnikCene

                txtDePriimek.Text = dr["Prezime"].ToString().ToString();
                txtDeIme.Text = dr["Ime"].ToString().ToString();
                txtDeTelefon1.Text = dr["DeTelefon1"].ToString().ToString();
                txtDeTelefon2.Text = dr["DeTelefon2"].ToString().ToString();
                txtDeEMail.Text = dr["DeEMail"].ToString().ToString();
                txtDeUlHisStBivS.Text = dr["DeUlHisStBivS"].ToString().ToString();
                txtDeKrajBivS.Text = dr["DeKrajBivS"].ToString().ToString();
                txtDeSifDelMes.SelectedValue = Convert.ToInt32(dr["DmSifra"].ToString().ToString());
                txtDeSifStat.Text = dr["DeSifStat"].ToString().ToString();

                if (dr["Manevrista"].ToString().ToString() == "1")
                {
                    chkManevrista.Checked = true;
                }
                else
                {
                    chkManevrista.Checked = false;
                }
                if (dr["Masinovodja"].ToString().ToString() == "1")
                {
                    chkMasinovodja.Checked = true;
                }
                else
                {
                    chkMasinovodja.Checked = false;
                }
                if (dr["Pomocnik"].ToString().ToString() == "1")
                {
                    chkPomocnik.Checked = true;
                }
                else
                {
                    chkPomocnik.Checked = false;

                }
                if (dr["Vozovodja"].ToString().ToString() == "1")
                {
                    chkVozovodja.Checked = true;
                }
                else
                {
                    chkVozovodja.Checked = false;
                }

                if (dr["PregledacKola"].ToString().ToString() == "1")
                {
                    chkPregledacKola.Checked = true;
                }
                else
                {
                    chkPregledacKola.Checked = false;
                }

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
                        txtDeSifra.Text = row.Cells[0].Value.ToString();
                        VratiPodatke(txtDeSifra.Text);
                        
                    
                        /*
                        if (row.Cells[13].Value.ToString() == "1")
                        {
                            chkPregledacKola.Checked = true;
                        }
                        else
                        {
                            chkPregledacKola.Checked = false;
                        }
                        if (row.Cells[14].Value.ToString() == "1")
                        {
                            chkVozovodja.Checked = true;
                        }
                        else
                        {
                            chkVozovodja.Checked = false;
                        }
                       */
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void tsPoslednja_Click(object sender, EventArgs e)
        {

        }

        private void tsNazad_Click(object sender, EventArgs e)
        {

        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertDelavciMTA del = new InsertDelavciMTA();
            del.DelDelavciMTA(Convert.ToInt32(txtDeSifra.Text));
            RefreshDataGrid();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            int PomManevrista, PomPomocnik, PomVozovodja, PomPregledacKola, PomMasinovodja;

            if (chkManevrista.Checked)
            {
                PomManevrista = 1;
            }
            else
            {
                PomManevrista = 0;
            }

            if (chkPomocnik.Checked)
            {
                PomPomocnik = 1;
            }
            else
            {
                PomPomocnik = 0;
            }

            if (chkVozovodja.Checked)
            {
                PomVozovodja = 1;
            }
            else
            {
                PomVozovodja = 0;
            }

            if (chkPregledacKola.Checked)
            {
                PomPregledacKola = 1;
            }
            else
            {
                PomPregledacKola = 0;
            }

            if (chkMasinovodja.Checked)
            {
                PomMasinovodja = 1;
            }
            else
            {
                PomMasinovodja = 0;
            }
          
                // txtDeSifra.Text,  txtDePriimek.Text,  txtDeIme.Text, txtDeTelefon1.Text,  txtDeTelefon2.Text ,  txtDeEMail.Text , txtDeUlHisStBivS.Text , txtDeKrajBivS.Text , txtDeSifDelMes.Text ,  txtDeSifStat.Text ,  PomManevrista, PomPomocnik, PomVozovodja, PomPregledacKola, PomMasinovodja)
                //  txtNaziv.Text,  txtUlica.Text,  txtMesto.Text,  txtOblast.Text, txtPosta.Text ,txtDrzava.Text, txtTelefon.Text, txtTR.Text ,  txtNapomena.Text,txtMaticniBroj.Text,  txtEmail.Text,  txtPIB.Text
                InsertDelavciMTA ins = new InsertDelavciMTA();
                ins.InsDelavciStariMTA(Convert.ToInt32(txtDeSifra.Text),   txtDePriimek.Text, txtDeIme.Text, txtDeTelefon1.Text, txtDeTelefon2.Text, txtDeEMail.Text, txtDeUlHisStBivS.Text, txtDeKrajBivS.Text, Convert.ToInt32(txtDeSifDelMes.SelectedValue), txtDeSifStat.Text, PomManevrista, PomPomocnik, PomVozovodja, PomPregledacKola, PomMasinovodja);
         
         
            RefreshDataGrid();
        }
    }
}
