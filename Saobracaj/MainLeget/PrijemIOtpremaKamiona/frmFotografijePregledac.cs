using iTextSharp.text;
using iTextSharp.text.pdf;
using Saobracaj.Dokumenta;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Saobracaj.MainLeget.PrijemIOtpremaKamiona
{
    // Ovaj form je namenjen za pregled i cuvanje fotografija koje se vezuju za komecijalni nalog. Najava moze imati vise fotografija, a svaka fotografija ima svoj tip (tara, plomba, kontejner).
    public partial class frmFotografijePregledac : Form
    {
        bool status = false;
        public frmFotografijePregledac()
        {
            InitializeComponent();
        }

        public frmFotografijePregledac(int sifra)
        {
            InitializeComponent();
            txtSifraNajave.Text = sifra.ToString();
            RefreshDataGrid();
        }
        public string removeDoubleBackslashes(string input)
        {
            char[] separator = new char[1] { '\\' };
            string result = "";
            string[] subResult = input.Split(separator);
            for (int i = 0; i <= subResult.Length - 1; i++)
            {
                result = i < subResult.Length - 1 ? result + subResult[i] + "\\" : result + subResult[i];
            }
            return result;
        }

        private void btnTovarniList_Click(object sender, EventArgs e)
        {
            string PictureFolder = txtTara.Text;
            ofd1.InitialDirectory = PictureFolder;

            if (ofd1.ShowDialog() == DialogResult.OK)
            {
                txtTara.Text = fbd.SelectedPath.ToString() + ofd1.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string PictureFolder = txtPlomba.Text;
            ofd1.InitialDirectory = PictureFolder;

            if (ofd1.ShowDialog() == DialogResult.OK)
            {
                txtPlomba.Text = fbd.SelectedPath.ToString() + ofd1.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string PictureFolder = txtKontejner.Text;
            ofd1.InitialDirectory = PictureFolder;

            if (ofd1.ShowDialog() == DialogResult.OK)
            {
                txtKontejner.Text = fbd.SelectedPath.ToString() + ofd1.FileName;
            }
        }

        private void KopirajFajlPoTipu(string putanja, string FolderDestinacije, int Tip)
        {
            string fileName = ofd1.FileName; //Ovde ce trebati promena
            fileName = fileName.Replace(" ", "_");
            string sourcePath = fbd.SelectedPath.ToString();
            string result = Path.GetFileName(fileName);
            string targetPath = "";

            if (Tip == 1)
            {
                targetPath = @"\\192.168.150.110\Pregledac\Tara\" + FolderDestinacije ;
            }
            else if (Tip == 2)
            {
                targetPath = @"\\192.168.150.110\Pregledac\Plomba\" + FolderDestinacije ;
            }
            else if (Tip == 3)
            {
                targetPath = @"\\192.168.150.110\Pregledac\Kontejner\" + FolderDestinacije ;
            }
         

            string sourceFile = putanja;
            string destFile = System.IO.Path.Combine(targetPath, result);

            if (!System.IO.Directory.Exists(targetPath))
            {
                System.IO.Directory.CreateDirectory(targetPath);
            }

            var remote = Path.Combine(targetPath, result);
            File.Copy(sourceFile, remote);
            if (Tip == 1)
            {
                txtTara.Text = remote;
            }
            else if (Tip == 2)
            {
                txtPlomba.Text = remote;
            }
            else if (Tip == 3)
            {
                txtKontejner.Text = remote;
            }
           



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

        private void btnSacuvajTovarniList_Click(object sender, EventArgs e)
        {
            if (status == true)
            {
                InsertFotografijeDokumenta ins = new InsertFotografijeDokumenta();
                KopirajFajlPoTipu(txtTara.Text, txtSifraNajave.Text, 1);
                ins.InsFotDokumenta(Convert.ToInt32(txtSifraNajave.Text), txtTara.Text);
                //  ins.UpdateNajavaCIM(Convert.ToInt32(txtSifraNajave.Text));
                RefreshDataGrid();
                status = true;
            }
            else
            {
                /*
                  InsertNajavaDokumenta ins = new InsertNajavaDokumenta();
                  KopirajFajl(txtPutanja2.Text, txtSifraNajave.Text, true);
                  ins.InsNajDokumenta(Convert.ToInt32(txtSifraNajave.Text), txtPutanja2.Text);
                  RefreshDataGrid();
                  status = false;
                 */
            }
        }
        private void RefreshDataGrid()
        {
            int pomNaj = Convert.ToInt32(txtSifraNajave.Text);
            var select = "select * from DokumentaPregledac  where DokumentaPregledac.IDNajave =  " + pomNaj;
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
            dataGridView1.Columns[1].HeaderText = "Najava";
            dataGridView1.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Putanja";
            dataGridView1.Columns[2].Width = 550;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //P1      txtPutanja.Text = txtPutanja.Text.Replace("192.168.1.6", "WSS");
                System.Diagnostics.Process.Start(txtTara.Text);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void frmFotografijePregledac_Load(object sender, EventArgs e)
        {

        }
    }
}
