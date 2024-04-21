using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data.SqlClient;


//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp;
using System.Text.RegularExpressions;

using System.Drawing.Imaging;

namespace Saobracaj.Dokumenta
{
    public partial class frmEvidencijaRadaDokumenti : Form
    {
        public static string code = "frmEvidencijaRadaDokumenti";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Saobracaj.Sifarnici.frmLogovanje.user.ToString();
        string niz = "";
        bool status = false;
        public frmEvidencijaRadaDokumenti()
        {
            InitializeComponent();
        }

        public frmEvidencijaRadaDokumenti(string sifra)
        {
            InitializeComponent();
            txtSifraNajave.Text = sifra;
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
        private void button1_Click(object sender, EventArgs e)
        {
            string PictureFolder = txtPutanja.Text;
            ofd1.InitialDirectory = PictureFolder;

            if (ofd1.ShowDialog() == DialogResult.OK)
            {
                txtPutanja.Text = fbd1.SelectedPath.ToString() + ofd1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           // txtPutanja.Text = txtPutanja.Text.Replace("192.168.1.6", "WSS");
            System.Diagnostics.Process.Start(txtPutanja.Text);
            //TA\Racuni\2259\Racuni
        }

        private void RefreshDataGridAktivnostiSlike()
        {
            var select = "";


            select = "Select Aktivnosti.ID as Zapis,  Aktivnosti.Oznaka, " +
                             " (RTrim(DeIme) + ' ' + RTRim(DePriimek)) as Zaposleni,  " +
                             "  VremeOD, VremeDo,  Aktivnosti.Opis, UkupniTroskovi, RAcun, Kartica," +
                               " Aktivnosti.DatumInserta " +
                              " from Aktivnosti " +
                              " inner join AktivnostiDokumenta on Aktivnosti.ID = AktivnostiDokumenta.IDAktivnosti " +
                             " inner join Delavci on Delavci.DeSifra = Aktivnosti.Zaposleni  " +
                               " inner join Kraji on Kraji.KrSifra = Aktivnosti.MestoUpucivanja" +
                              " order by Aktivnosti.ID desc";


            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 30;

            DataGridViewColumn column1 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "Oznaka";
            dataGridView2.Columns[1].Width = 30;

            DataGridViewColumn column2 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "Zaposleni";
            dataGridView2.Columns[2].Width = 100;

            DataGridViewColumn column3 = dataGridView2.Columns[3];
            dataGridView2.Columns[3].HeaderText = "Vreme od";
            dataGridView2.Columns[3].Width = 100;

            DataGridViewColumn column4 = dataGridView2.Columns[4];
            dataGridView2.Columns[4].HeaderText = "Vreme do";
            dataGridView2.Columns[4].Width = 100;


            DataGridViewColumn column5 = dataGridView2.Columns[5];
            dataGridView2.Columns[5].HeaderText = "Opis";
            dataGridView2.Columns[5].Width = 120;

            DataGridViewColumn column6 = dataGridView2.Columns[6];
            dataGridView2.Columns[6].HeaderText = "Ukupni troškovi";
            dataGridView2.Columns[6].Width = 50;


            DataGridViewColumn column8 = dataGridView2.Columns[7];
            dataGridView2.Columns[7].HeaderText = "Računi";
            dataGridView2.Columns[7].Width = 50;

            DataGridViewColumn column9 = dataGridView2.Columns[8];
            dataGridView2.Columns[8].HeaderText = "Kartice";
            dataGridView2.Columns[8].Width = 50;


        }

        private void RefreshDataGridAktivnosti()
        {
            var select = "";

        
                select = "Select Aktivnosti.ID as Zapis,  Aktivnosti.Oznaka, " +
                                 " (RTrim(DeIme) + ' ' + RTRim(DePriimek)) as Zaposleni,  " +
                                 "  VremeOD, VremeDo,  Aktivnosti.Opis, UkupniTroskovi, RAcun, Kartica," +
                                   " Aktivnosti.DatumInserta " +
                                  " from Aktivnosti  " +
                                 " inner join Delavci on Delavci.DeSifra = Aktivnosti.Zaposleni  " +
                                   " inner join Kraji on Kraji.KrSifra = Aktivnosti.MestoUpucivanja" +
                                  " order by Aktivnosti.ID desc";
      

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 30;

            DataGridViewColumn column1 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "Oznaka";
            dataGridView2.Columns[1].Width = 30;

            DataGridViewColumn column2 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "Zaposleni";
            dataGridView2.Columns[2].Width = 100;

            DataGridViewColumn column3 = dataGridView2.Columns[3];
            dataGridView2.Columns[3].HeaderText = "Vreme od";
            dataGridView2.Columns[3].Width = 100;

            DataGridViewColumn column4 = dataGridView2.Columns[4];
            dataGridView2.Columns[4].HeaderText = "Vreme do";
            dataGridView2.Columns[4].Width = 100;

         
            DataGridViewColumn column5 = dataGridView2.Columns[5];
            dataGridView2.Columns[5].HeaderText = "Opis";
            dataGridView2.Columns[5].Width = 120;

            DataGridViewColumn column6 = dataGridView2.Columns[6];
            dataGridView2.Columns[6].HeaderText = "Ukupni troškovi";
            dataGridView2.Columns[6].Width = 50;


            DataGridViewColumn column8 = dataGridView2.Columns[7];
            dataGridView2.Columns[7].HeaderText = "Računi";
            dataGridView2.Columns[7].Width = 50;

            DataGridViewColumn column9 = dataGridView2.Columns[8];
            dataGridView2.Columns[8].HeaderText = "Kartice";
            dataGridView2.Columns[8].Width = 50;


        }


        private void RefreshDataGrid()
        {
            int pomNaj = Convert.ToInt32(txtSifraNajave.Text);
            var select = "select * from AktivnostiDokumenta  where AktivnostiDokumenta.IDAktivnosti =  " + pomNaj;
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

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 30;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Aktivnost";
            dataGridView1.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Putanja";
            dataGridView1.Columns[2].Width = 550;
        }

        private void KopirajFajlPoTipu(string putanja, string FolderDestinacije, int Tip)
        {
            string fileName = ofd1.FileName; //Ovde ce trebati promena
            fileName = fileName.Replace(" ", "_");
            string sourcePath = fbd1.SelectedPath.ToString();
            string result = Path.GetFileName(fileName);
            string targetPath = "";

            targetPath = @"\\192.168.129.7\TA\Racuni\" + FolderDestinacije + @"\Racuni";
            
            string sourceFile = putanja;
            string destFile = System.IO.Path.Combine(targetPath, result);

            if (!System.IO.Directory.Exists(targetPath))
            {
                System.IO.Directory.CreateDirectory(targetPath);
            }

            var remote = Path.Combine(targetPath, result);
            File.Copy(sourceFile, remote);
            txtPutanja.Text = remote;

            if (System.IO.Directory.Exists(sourcePath))
            {
                string[] files = System.IO.Directory.GetFiles(sourcePath);
                // Copy the files and overwrite destination files if they already exist.
                foreach (string s in files)
                {
                    // Use static Path methods to extract only the file name from the path.
                    fileName = System.IO.Path.GetFileName(s);
                    destFile = System.IO.Path.Combine(targetPath, fileName);
                    System.IO.File.Copy(s, destFile, true);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (status == true)
            {
                InsertAktivnostiDokumenta ins = new InsertAktivnostiDokumenta();
                KopirajFajlPoTipu(txtPutanja.Text, txtSifraNajave.Text, 6);
                ins.InsNajDokumenta(Convert.ToInt32(txtSifraNajave.Text), txtPutanja.Text);
                RefreshDataGrid();

                status = true;
            }
            else
            {

            }
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            RefreshDataGridAktivnostiSlike();
        }

        private void frmEvidencijaRadaDokumenti_Load(object sender, EventArgs e)
        {
            RefreshDataGridAktivnosti();
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
                        txtPutanja.Text = row.Cells[2].Value.ToString();
                        txtPutanja.Text = txtPutanja.Text.Replace("192.168.129.7", "\\\\192.168.129.7\\TA");

                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void VratiDokumentaAktivnosti()
        {

            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
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

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                VratiDokumentaAktivnosti();
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {
                       
                        txtSifraNajave.Text = row.Cells[0].Value.ToString();
                        RefreshDataGrid();
                        // txtPutanja.Text = row.Cells[2].Value.ToString();
                    }
                }
                

            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
            string path = Path.Combine(@"//192.168.129.7/TA/Racuni/", txtSifra.Text + "/");
            //txtPutanja.Text = "\\192.168.1.6\";
            DirectoryInfo dir_info = new DirectoryInfo(path);
            txtPutanja.Text = dir_info.FullName;
        }
    }
}
