using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp;

namespace Saobracaj.Izvoz
{
    public partial class frmIzvozDokumenta : Form
    {
        bool status = false;
        public frmIzvozDokumenta()
        {
            InitializeComponent();
        }

        public frmIzvozDokumenta(string sifra)
        {
            InitializeComponent();
            txtSifraUvoza.Text = sifra;
            FillCombo();
            RefreshDataGrid();
        }

        public frmIzvozDokumenta(string sifra, string Buking, string Voz)
        {
            InitializeComponent();
            txtSifraUvoza.Text = sifra;
            txtSifraUvoza2.Text = Buking;
            txtSifraUvoza3.Text = Voz;
            FillCombo();
            RefreshDataGrid();
            RefreshDataGrid2();
            RefreshDataGrid3();
        }

        private void RefreshDataGrid()
        {
            if (txtSifraUvoza.Text == "")
            {
                MessageBox.Show("Odaberite stavku");
                return;
            }
            int pomNaj = Convert.ToInt32(txtSifraUvoza.Text);
            var select = "select * from IzvozDokumenta  where TipDokumenta = 1 and IzvozDokumenta.IDIzvoz =  " + pomNaj;
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
            dataGridView1.Columns[1].HeaderText = "Izvozni zapis";
            dataGridView1.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Putanja";
            dataGridView1.Columns[2].Width = 550;
        }


        private void RefreshDataGrid2()
        {
            if (txtSifraUvoza2.Text == "")
            {
                MessageBox.Show("Odaberite stavku");
                return;
            }
            int pomNaj = Convert.ToInt32(txtSifraUvoza2.Text);
            var select = "select * from IzvozDokumenta  where TipDokumenta = 2 and IzvozDokumenta.IDIzvoz =  " + pomNaj;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
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

            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 30;

            DataGridViewColumn column2 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "Izvozni zapis";
            dataGridView2.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "Putanja";
            dataGridView2.Columns[2].Width = 550;
        }


        private void RefreshDataGrid3()
        {
            if (txtSifraUvoza3.Text == "")
            {
                MessageBox.Show("Odaberite stavku");
                return;
            }
            int pomNaj = Convert.ToInt32(txtSifraUvoza3.Text);
            var select = "select * from IzvozDokumenta  where TipDokumenta = 3 and IzvozDokumenta.IDIzvoz =  " + pomNaj;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds.Tables[0];


            dataGridView3.BorderStyle = BorderStyle.None;
            dataGridView3.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView3.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView3.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView3.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView3.BackgroundColor = Color.White;

            dataGridView3.EnableHeadersVisualStyles = false;
            dataGridView3.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView3.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            DataGridViewColumn column = dataGridView3.Columns[0];
            dataGridView3.Columns[0].HeaderText = "ID";
            dataGridView3.Columns[0].Width = 30;

            DataGridViewColumn column2 = dataGridView3.Columns[1];
            dataGridView3.Columns[1].HeaderText = "Izvozni zapis";
            dataGridView3.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView3.Columns[2];
            dataGridView3.Columns[2].HeaderText = "Putanja";
            dataGridView3.Columns[2].Width = 550;
        }

        public void FillCombo()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var val = "Select ID, Naziv, PotrebnoZaFakturu from TipKomercijalnogDokumenta order by Naziv";
            var valSAD = new SqlDataAdapter(val, myConnection);
            var valSDS = new DataSet();
            valSAD.Fill(valSDS);
            DataTable dt = valSDS.Tables[0];

            // novi red
            DataRow newRow = dt.NewRow();
            newRow["ID"] = 0;
            newRow["Naziv"] = ""; // prazan naziv
            newRow["PotrebnoZaFakturu"] = DBNull.Value;
            dt.Rows.InsertAt(newRow, 0);

            // vezivanje za combo
            cboTipDokumenta1.BindingContext = new BindingContext();
            cboTipDokumenta1.DataSource = dt;
            cboTipDokumenta1.DisplayMember = "Naziv";
            cboTipDokumenta1.ValueMember = "ID";

            cboTipDokumenta2.BindingContext = new BindingContext();
            cboTipDokumenta2.DataSource = dt;
            cboTipDokumenta2.DisplayMember = "Naziv";
            cboTipDokumenta2.ValueMember = "ID";

            cboTipDokumenta3.BindingContext = new BindingContext();
            cboTipDokumenta3.DataSource = dt;
            cboTipDokumenta3.DisplayMember = "Naziv";
            cboTipDokumenta3.ValueMember = "ID";
        }


        private void KopirajFajlPoTipu(string putanja, string FolderDestinacije, int Tip)
        {
            string fileName = ofd1.FileName; //Ovde ce trebati promena
            fileName = fileName.Replace(" ", "_");
            string sourcePath = fbd1.SelectedPath.ToString();
            string result = Path.GetFileName(fileName);
            string targetPath = "";

            targetPath = @"\\192.168.150.110\Leget\IZVOZ\" + FolderDestinacije + @"\Izvoz";

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

        private void KopirajFajlPoTipu2(string putanja, string FolderDestinacije, int Tip)
        {
            string fileName = ofd1.FileName; //Ovde ce trebati promena
            fileName = fileName.Replace(" ", "_");
            string sourcePath = fbd1.SelectedPath.ToString();
            string result = Path.GetFileName(fileName);
            string targetPath = "";

            targetPath = @"\\192.168.150.110\Leget\IZVOZBUKING\" + FolderDestinacije + @"\Izvoz";

            string sourceFile = putanja;
            string destFile = System.IO.Path.Combine(targetPath, result);

            if (!System.IO.Directory.Exists(targetPath))
            {
                System.IO.Directory.CreateDirectory(targetPath);
            }

            var remote = Path.Combine(targetPath, result);
            File.Copy(sourceFile, remote);
            txtPutanja2.Text = remote;

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

        private void KopirajFajlPoTipu3(string putanja, string FolderDestinacije, int Tip)
        {
            string fileName = ofd1.FileName; //Ovde ce trebati promena
            fileName = fileName.Replace(" ", "_");
            string sourcePath = fbd1.SelectedPath.ToString();
            string result = Path.GetFileName(fileName);
            string targetPath = "";

            targetPath = @"\\192.168.150.110\Leget\IZVOZVOZ\" + FolderDestinacije + @"\Izvoz";

            string sourceFile = putanja;
            string destFile = System.IO.Path.Combine(targetPath, result);

            if (!System.IO.Directory.Exists(targetPath))
            {
                System.IO.Directory.CreateDirectory(targetPath);
            }

            var remote = Path.Combine(targetPath, result);
            File.Copy(sourceFile, remote);
            txtPutanja3.Text = remote;

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

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
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
            System.Diagnostics.Process.Start(txtPutanja.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //UVodimo novo polje kod insert u tabelu
            // 1- Point kontejneru
            //    2- Point bukingu
            //   3 - Point vozu
            //  if (status == true)
            // {
            InsertIzvozDokumenta ins = new InsertIzvozDokumenta();
            KopirajFajlPoTipu(txtPutanja.Text, txtSifraUvoza.Text, 6);
            // uzmi vrednost iz combo
            int? tipDokValue = null;
            if (cboTipDokumenta1.SelectedValue != null &&
                int.TryParse(cboTipDokumenta1.SelectedValue.ToString(), out int tipID) &&
                tipID > 0)
            {
                tipDokValue = tipID;
            }
            ins.InsIzvozDokumenta(Convert.ToInt32(txtSifraUvoza.Text), txtPutanja.Text, 1, tipDokValue);
            RefreshDataGrid();

            status = true;
            //  }
            //  else
            //  {

            //  }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InsertIzvozDokumenta ins = new InsertIzvozDokumenta();
            ins.DelIzvozDokumenta(Convert.ToInt32(txtSifra.Text));
            RefreshDataGrid();

            status = true;
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

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string PictureFolder = txtPutanja2.Text;
            ofd1.InitialDirectory = PictureFolder;

            if (ofd1.ShowDialog() == DialogResult.OK)
            {
                txtPutanja2.Text = fbd1.SelectedPath.ToString() + ofd1.FileName;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string PictureFolder = txtPutanja3.Text;
            ofd1.InitialDirectory = PictureFolder;

            if (ofd1.ShowDialog() == DialogResult.OK)
            {
                txtPutanja3.Text = fbd1.SelectedPath.ToString() + ofd1.FileName;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(txtPutanja2.Text);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(txtPutanja3.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //  if (status == true)
            // {
            // 2 - buking
            InsertIzvozDokumenta ins = new InsertIzvozDokumenta();
            KopirajFajlPoTipu2(txtPutanja2.Text, txtSifraUvoza2.Text, 6);
            // uzmi vrednost iz combo
            int? tipDokValue = null;
            if (cboTipDokumenta2.SelectedValue != null &&
                int.TryParse(cboTipDokumenta2.SelectedValue.ToString(), out int tipID) &&
                tipID > 0)
            {
                tipDokValue = tipID;
            }
            ins.InsIzvozDokumenta(Convert.ToInt32(txtSifraUvoza2.Text), txtPutanja2.Text, 2, tipDokValue);
            RefreshDataGrid2();

            status = true;
            //  }
            //  else
            //  {

            //  }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //Po vozu
            InsertIzvozDokumenta ins = new InsertIzvozDokumenta();
            KopirajFajlPoTipu3(txtPutanja3.Text, txtSifraUvoza3.Text, 3);
            int? tipDokValue = null;
            if (cboTipDokumenta3.SelectedValue != null &&
                int.TryParse(cboTipDokumenta3.SelectedValue.ToString(), out int tipID) &&
                tipID > 0)
            {
                tipDokValue = tipID;
            }
            ins.InsIzvozDokumenta(Convert.ToInt32(txtSifraUvoza3.Text), txtPutanja3.Text, 3, tipDokValue);
            RefreshDataGrid3();

            status = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            InsertIzvozDokumenta ins = new InsertIzvozDokumenta();
            ins.DelIzvozDokumenta(Convert.ToInt32(txtSifra.Text));
            RefreshDataGrid2();

            status = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            InsertIzvozDokumenta ins = new InsertIzvozDokumenta();
            ins.DelIzvozDokumenta(Convert.ToInt32(txtSifra.Text));
            RefreshDataGrid3();

            status = true;
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {
                        txtSifra2.Text = row.Cells[0].Value.ToString();
                        txtPutanja.Text = row.Cells[2].Value.ToString();

                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    if (row.Selected)
                    {
                        txtSifra3.Text = row.Cells[0].Value.ToString();
                        txtPutanja3.Text = row.Cells[2].Value.ToString();

                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void frmIzvozDokumenta_Load(object sender, EventArgs e)
        {

        }
    }
}
