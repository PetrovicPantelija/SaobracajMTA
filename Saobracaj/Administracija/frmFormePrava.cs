using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Administracija
{
    public partial class frmFormePrava : Form
    {
        private string connect = Sifarnici.frmLogovanje.connectionString;
        private string niz = "";
        public static string code = "frmFormePrava";
        public bool Pravo;
        private int idGrupe;
        private int idForme;
        private bool insert;
        private bool update;
        private bool delete;
        private string Kor = Sifarnici.frmLogovanje.user.ToString();
        private bool status = false;

        public frmFormePrava()
        {
            InitializeComponent();
            IdGrupe();
            IdForme();
            PravoPristupa();

            NapiniComboBox();

            var query = "SELECT g.IdGrupe,g.Naziv,f.IdForme,f.Naziv,f.Code,a.Upis,a.Izmena,a.Brisanje " +
                "FROM GrupeKorisnik g,Forme f,GrupeForme a " +
                "WHERE g.IdGrupe=a.IdGrupe and f.IdForme=a.IdForme order by g.IdGrupe";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
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

            dataGridView1.Columns[0].HeaderText = "ID Grupe";
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].HeaderText = "Naziv Grupe";
            dataGridView1.Columns[1].Width = 170;
            dataGridView1.Columns[2].HeaderText = "ID Forme";
            dataGridView1.Columns[2].Width = 50;
            dataGridView1.Columns[3].HeaderText = "Naziv Forme";
            dataGridView1.Columns[3].Width = 130;
            dataGridView1.Columns[4].HeaderText = "Code Forme";
            dataGridView1.Columns[4].Width = 130;
            dataGridView1.Columns[5].Width = 50;
            dataGridView1.Columns[6].Width = 50;
            dataGridView1.Columns[7].Width = 50;
        }

        private void NapiniComboBox()
        {
            var select = " Select ID, ( Naziv) as Naziv From GrupeKorisnik";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboGrupeKorisnika.DataSource = ds.Tables[0];
            cboGrupeKorisnika.DisplayMember = "Naziv";
            cboGrupeKorisnika.ValueMember = "ID";

            NapuniFormeDG2();
        }

        private void NapuniFormeDG2()
        {
            var query = "select IDForme, Naziv, Code from Forme Order by IDForme";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            dataGridView2.BorderStyle = BorderStyle.None;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView2.BackgroundColor = Color.White;

            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            /*
            dataGridView1.Columns[0].HeaderText = "ID Grupe";
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].HeaderText = "Naziv Grupe";
            dataGridView1.Columns[1].Width = 170;
            dataGridView1.Columns[2].HeaderText = "ID Forme";
            dataGridView1.Columns[2].Width = 50;
            dataGridView1.Columns[3].HeaderText = "Naziv Forme";
            dataGridView1.Columns[3].Width = 130;
            dataGridView1.Columns[4].HeaderText = "Code Forme";
            dataGridView1.Columns[4].Width = 130;
            dataGridView1.Columns[5].Width = 50;
            dataGridView1.Columns[6].Width = 50;
            dataGridView1.Columns[7].Width = 50;

            */
        }

        public string IdGrupe()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            //Sifarnici.frmLogovanje frm = new Sifarnici.frmLogovanje();
            string query = "Select IdGrupe from KorisnikGrupa Where Korisnik = " + "'" + Kor.TrimEnd() + "'";
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            int count = 0;

            while (dr.Read())
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
            conn.Close();
            return niz;
        }

        private int IdForme()
        {
            string query = "Select IdForme from Forme where Rtrim(Code)=" + "'" + code + "'";
            SqlConnection conn = new SqlConnection(connect);
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
            string query = "Select * From GrupeForme Where IdGrupe in (" + niz + ") and IdForme=" + idForme;
            SqlConnection conn = new SqlConnection(connect);
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
                        //tsNew.Enabled = false;
                    }
                    update = Convert.ToBoolean(reader["Izmena"]);
                    if (update == false)
                    {
                        //tsSave.Enabled = false;
                    }
                    delete = Convert.ToBoolean(reader["Brisanje"]);
                    if (delete == false)
                    {
                        //tsDelete.Enabled = false;
                    }
                }
            }

            conn.Close();
        }

        private void RefreshDataGrid1()
        {
            var query = "SELECT g.IdGrupe,g.Naziv,f.IdForme,f.Naziv,f.Code,a.Upis,a.Izmena,a.Brisanje " +
                   "FROM GrupeKorisnik g,Forme f,GrupeForme a " +
                   "WHERE g.IdGrupe=a.IdGrupe and f.IdForme=a.IdForme order by g.IdGrupe";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
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

            dataGridView1.Columns[0].HeaderText = "ID Grupe";
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].HeaderText = "Naziv Grupe";
            dataGridView1.Columns[1].Width = 170;
            dataGridView1.Columns[2].HeaderText = "ID Forme";
            dataGridView1.Columns[2].Width = 50;
            dataGridView1.Columns[3].HeaderText = "Naziv Forme";
            dataGridView1.Columns[3].Width = 130;
            dataGridView1.Columns[4].HeaderText = "Code Forme";
            dataGridView1.Columns[4].Width = 130;
            dataGridView1.Columns[5].Width = 50;
            dataGridView1.Columns[6].Width = 50;
            dataGridView1.Columns[7].Width = 50;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                // it.InsTeretnica("", Convert.ToInt32(cboStanicaOd.SelectedValue), Convert.ToInt32(cboStanicaDo.SelectedValue), Convert.ToInt32(cboStanicaIsklj.SelectedValue), Convert.ToDateTime(dtpVremeOd.Value), Convert.ToDateTime(dtpVremeOd.Value), "", 0, 1, "sa", 0, Convert.ToInt32(cboRadniNalog.SelectedValue));

                // irn.InsRNTeretnica(Convert.ToInt32(cboRadniNalog.SelectedValue));

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {
                        Administracija.InsertAdministracije its = new Administracija.InsertAdministracije();
                        its.InsertPravoGrupeNaForme(Convert.ToInt32(cboGrupeKorisnika.SelectedValue), Convert.ToInt32(row.Cells[0].Value.ToString()), 1, 1, 1);
                        // Dokumenta.InsertIskljuceniVagoni div = new Dokumenta.InsertIskljuceniVagoni();
                        //  div.UpdateIskljuceniVagRSP(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(cboStanicaIsklj.SelectedValue));
                    }
                }
                RefreshDataGrid1();
                // MessageBox.Show("Vagoni su raspušteni sada se nalaze u stanici popisa");
            }
            catch
            {
                MessageBox.Show("Nije uspeo prenos stavki");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // it.InsTeretnica("", Convert.ToInt32(cboStanicaOd.SelectedValue), Convert.ToInt32(cboStanicaDo.SelectedValue), Convert.ToInt32(cboStanicaIsklj.SelectedValue), Convert.ToDateTime(dtpVremeOd.Value), Convert.ToDateTime(dtpVremeOd.Value), "", 0, 1, "sa", 0, Convert.ToInt32(cboRadniNalog.SelectedValue));

                // irn.InsRNTeretnica(Convert.ToInt32(cboRadniNalog.SelectedValue));

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        Administracija.InsertAdministracije its = new Administracija.InsertAdministracije();
                        its.DeletePravoGrupeNaForme(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()));
                        // Dokumenta.InsertIskljuceniVagoni div = new Dokumenta.InsertIskljuceniVagoni();
                        //  div.UpdateIskljuceniVagRSP(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(cboStanicaIsklj.SelectedValue));
                    }
                }
                RefreshDataGrid1();
                // MessageBox.Show("Vagoni su raspušteni sada se nalaze u stanici popisa");
            }
            catch
            {
                MessageBox.Show("Nije uspeo prenos stavki");
            }
        }
    }
}