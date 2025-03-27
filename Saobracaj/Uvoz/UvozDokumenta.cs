using Syncfusion.Windows.Forms;
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

namespace Saobracaj.Uvoz
{
    public partial class UvozDokumenta : Form
    {
        bool status = false;
        public UvozDokumenta()
        {
            InitializeComponent();
        }

        private void ChangeTextBox()
        {
            panelHeader.Visible = false;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
                panelHeader.Visible = true;
                meniHeader.Visible = false;
                this.BackColor = Color.White;
                this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
                this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
                this.ControlBox = true;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                Office2010Colors.ApplyManagedColors(this, Color.White);
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = Color.White;
                Office2010Colors.ApplyManagedColors(this, Color.White);

                foreach (Control control in this.Controls)
                {
                    if (control is System.Windows.Forms.Button buttons)
                    {
                        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                        buttons.FlatStyle = FlatStyle.Flat;
                    }
                }


                foreach (Control control in this.Controls)
                {

                    if (control is System.Windows.Forms.TextBox textBox)
                    {

                        textBox.BackColor = Color.White;// Example: Change background color
                        textBox.ForeColor = Color.FromArgb(51, 51, 54); //Boja slova u kvadratu
                        textBox.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                        // Example: Change font
                    }


                    if (control is System.Windows.Forms.Label label)
                    {
                        // Change properties here
                        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        label.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);  // Example: Change font

                        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                    }

                    if (control is DateTimePicker dtp)
                    {
                        dtp.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        dtp.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.CheckBox chk)
                    {
                        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        chk.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ListBox lb)
                    {
                        lb.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        lb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ComboBox cb)
                    {
                        cb.ForeColor = Color.FromArgb(51, 51, 54);
                        cb.BackColor = Color.White;// Example: Change background color
                        cb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.NumericUpDown nu)
                    {
                        nu.ForeColor = Color.FromArgb(51, 51, 54);
                        nu.BackColor = Color.White;// Example: Change background color
                        nu.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }
                }
            }
            else
            {
                panelHeader.Visible = false;
                meniHeader.Visible = true;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        private void PodesiDatagridView(DataGridView dgv)
        {

            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(90, 199, 249); // Selektovana boja
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.BackgroundColor = Color.White;

            dgv.DefaultCellStyle.Font = new Font("Helvetica", 12F, GraphicsUnit.Pixel);
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(51, 51, 54);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);
            dgv.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);


            //Header
            dgv.EnableHeadersVisualStyles = false;
            //   header.Style.Font = new Font("Arial", 12F, FontStyle.Bold);
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 54);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv.ColumnHeadersHeight = 30;
        }

        public UvozDokumenta(string sifra, string Nadredjeni)
        {
            InitializeComponent();
            txtSifraUvoza.Text = sifra;
            txtPlanID.Text = Nadredjeni;
            chkZaVoz.Checked = true;
            ChangeTextBox();
            RefreshDataGridCeoVoz();
            RefreshDataGrid();
            RefreshDataGridKontejnere();
            RefreshDataGridUslugaSvePoVozu();
        }

        private void RefreshDataGrid()
        {
            if (txtSifraUvoza.Text == "")
            {
                MessageBox.Show("Odaberite stavku");
                return;
            }
            int pomNaj = Convert.ToInt32(txtSifraUvoza.Text);
            var select = "";
            if (txtPlanID.Text == "0")
            {
                select = "select * from UvozDokumenta  inner join Uvoz on Uvoz.ID = UvozDokumenta.IDUvoz";
            }
            else
            {
                select = "select * from UvozDokumenta  inner join UvozKonacna on UvozKonacna.ID = UvozDokumenta.IDUvoz where IdNadredjeni = " + txtPlanID.Text;
            }
           
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];


            PodesiDatagridView(dataGridView1);

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 30;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "KontejnerID";
            dataGridView1.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Putanja";
            dataGridView1.Columns[2].Width = 550;
        }

        private void RefreshDataGridCeoVoz()
        {
            if (txtSifraUvoza.Text == "")
            {
                MessageBox.Show("Odaberite stavku");
                return;
            }
            int pomNaj = Convert.ToInt32(txtSifraUvoza.Text);
            var select = "select * from UvozDokumentaCeoVoz  where UvozDokumentaCeoVoz.PlanID =  " + txtPlanID.Text;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];


            PodesiDatagridView(dataGridView2);

            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 30;

            DataGridViewColumn column2 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "PlanID";
            dataGridView2.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "Putanja";
            dataGridView2.Columns[2].Width = 550;
        }


        private void RefreshDataGridKontejnere()
        {
            if (txtSifraUvoza.Text == "")
            {
                MessageBox.Show("Odaberite stavku");
                return;
            }
            int pomNaj = Convert.ToInt32(txtSifraUvoza.Text);
            var select = "";
            if (txtPlanID.Text == "0")
            { 
            select = "select ID, BrojKontejnera from Uvoz";
            
            }
            else
            {
                select = "select ID, BrojKontejnera from UvozKonacna where IdNadredjeni = " + txtPlanID.Text;
            }


               
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView4.ReadOnly = true;
            dataGridView4.DataSource = ds.Tables[0];


            PodesiDatagridView(dataGridView4);

            DataGridViewColumn column = dataGridView4.Columns[0];
            dataGridView4.Columns[0].HeaderText = "ID";
            dataGridView4.Columns[0].Width = 30;

            DataGridViewColumn column2 = dataGridView4.Columns[1];
            dataGridView4.Columns[1].HeaderText = "Kontejner";
            dataGridView4.Columns[1].Width = 150;

        }


        private void RefreshDataGridUslugaSvePoVozu()
        {
            if (txtSifraUvoza.Text == "")
            {
                MessageBox.Show("Odaberite stavku");
                return;
            }
            int pomNaj = Convert.ToInt32(txtSifraUvoza.Text);
            var select = "";
            if (chkZaVoz.Checked == true)
            {
                select = "select UvozDokumentaUsluge.ID, UvozDokumentaUsluge.IDUsluge, UvozDokumentaUsluge.Putanja, VrstaManipulacije.Naziv as Usluga " +
                                 "from UvozDokumentaUsluge inner join UvozKonacnaVrstaManipulacije on UvozKonacnaVrstaManipulacije.ID = UvozDokumentaUsluge.IDUsluge" +
                                 " inner join UvozKonacna on UvozKonacna.ID = UvozKonacnaVrstaManipulacije.IDNAdredjena " +
                                 "inner join VrstaManipulacije on VrstaManipulacije.ID = UvozKonacnaVrstaManipulacije.IDVrstaManipulacije where UvozKonacna.IDNadredjeni =  " + txtPlanID.Text;

            }

           
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds.Tables[0];


            PodesiDatagridView(dataGridView3);

            DataGridViewColumn column = dataGridView3.Columns[0];
            dataGridView3.Columns[0].HeaderText = "ID";
            dataGridView3.Columns[0].Width = 30;

            DataGridViewColumn column2 = dataGridView3.Columns[1];
            dataGridView3.Columns[1].HeaderText = "UslugaID";
            dataGridView3.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView3.Columns[2];
            dataGridView3.Columns[2].HeaderText = "Putanja";
            dataGridView3.Columns[2].Width = 550;
        }


        private void RefreshDataGridUsluga()
        {
            if (txtSifraUvoza.Text == "")
            {
                MessageBox.Show("Odaberite stavku");
                return;
            }
            int pomNaj = Convert.ToInt32(txtSifraUvoza.Text);
            var select = "select * from UvozDokumentaUsluga  where UvozDokumentaUsluga.IDUsluge =  " + txtSifraUsluge.Text;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];


            PodesiDatagridView(dataGridView2);

            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 30;

            DataGridViewColumn column2 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "UslugaID";
            dataGridView2.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "Putanja";
            dataGridView2.Columns[2].Width = 550;
        }

        private void PozoviUslugu(string KontejnerID)
        {
            if (txtSifraUvoza.Text == "")
            {
                MessageBox.Show("Odaberite stavku");
                return;
            }
            int pomNaj = Convert.ToInt32(txtSifraUvoza.Text);
            var select = "";
            if (txtPlanID.Text == "0")
            {
                select = "select UvozVrstaManipulacije.ID, IdNadredjena, VrstaManipulacije.Naziv from UvozVrstaManipulacije INNER JOIN VrstaManipulACIJE ON VrstaManipulacije.ID = UvozVrstaManipulacije.IDVrstaManipulacije where IDNadredjena = " + KontejnerID ;
            }
            else
            {
                select = "select UvozKonacnaVrstaManipulacije.ID, IdNadredjena, VrstaManipulacije.Naziv from UvozKonacnaVrstaManipulacije INNER JOIN VrstaManipulACIJE ON VrstaManipulacije.ID = UvozKonacnaVrstaManipulacije.IDVrstaManipulacije where IDNadredjena = " + KontejnerID ;
            }



            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView5.ReadOnly = true;
            dataGridView5.DataSource = ds.Tables[0];


            PodesiDatagridView(dataGridView5);

            DataGridViewColumn column = dataGridView5.Columns[0];
            dataGridView5.Columns[0].HeaderText = "ID";
            dataGridView5.Columns[0].Width = 30;

            DataGridViewColumn column2 = dataGridView5.Columns[1];
            dataGridView5.Columns[1].HeaderText = "Nadredjena";
            dataGridView5.Columns[1].Width = 150;

            DataGridViewColumn column3 = dataGridView5.Columns[2];
            dataGridView5.Columns[2].HeaderText = "Usluga";
            dataGridView5.Columns[2].Width = 350;

        }

        private void KopirajFajlPoTipu(string putanja, string FolderDestinacije, int Tip)
        {
            string fileName = ofd1.FileName; //Ovde ce trebati promena
            fileName = fileName.Replace(" ", "_");
            string sourcePath = fbd1.SelectedPath.ToString();
            string result = Path.GetFileName(fileName);
            string targetPath = "";

            targetPath = @"\\192.168.99.10\Leget\Uvoz\" + FolderDestinacije + @"\Uvoz";

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

        private void KopirajFajlPoTipuCeoVoz(string putanja, string FolderDestinacije, int Tip)
        {
            string fileName = ofd1.FileName; //Ovde ce trebati promena
            fileName = fileName.Replace(" ", "_");
            string sourcePath = fbd1.SelectedPath.ToString();
            string result = Path.GetFileName(fileName);
            string targetPath = "";

            targetPath = @"\\192.168.99.10\Leget\Uvoz\Voz\" + FolderDestinacije ;

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

        private void KopirajFajlPoTipuUsluga(string putanja, string FolderDestinacije, int Tip)
        {
            string fileName = ofd1.FileName; //Ovde ce trebati promena
            fileName = fileName.Replace(" ", "_");
            string sourcePath = fbd1.SelectedPath.ToString();
            string result = Path.GetFileName(fileName);
            string targetPath = "";

            targetPath = @"\\192.168.99.10\Leget\Uvoz\Usluga\" + FolderDestinacije;

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
            //  if (status == true)
            // {
            InsertUvozDokumenta ins = new InsertUvozDokumenta();
           

            if (chkZaVoz.Checked == true)
            {
                KopirajFajlPoTipuCeoVoz(txtPutanja.Text, txtPlanID.Text, 6);
                ins.InsUvozDokumentaCeoVoz(Convert.ToInt32(txtPlanID.Text), txtPutanja.Text);
                RefreshDataGridCeoVoz();

            }
            else if (chkKontejner.Checked == true)
            {
                foreach (DataGridViewRow row in dataGridView4.Rows)
                {
                    if (row.Selected)
                    {
                        KopirajFajlPoTipu(txtPutanja.Text, row.Cells[0].Value.ToString(), 6);
                        ins.InsUvozDokumenta(Convert.ToInt32(row.Cells[0].Value.ToString()), txtPutanja.Text);
                        

                    }
                }
                RefreshDataGrid();
            }
            else if (chkUsluge.Checked == true)
            {
                KopirajFajlPoTipuUsluga(txtPutanja.Text, txtPlanID.Text, 6);
                ins.InsUvozDokumentaUsluga(Convert.ToInt32(txtSifraUsluge.Text), txtPutanja.Text);
                RefreshDataGridUsluga();
            }
          

          
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

        private void button3_Click(object sender, EventArgs e)
        {
            InsertUvozDokumenta ins = new InsertUvozDokumenta();
            ins.DelUvozDokumenta(Convert.ToInt32(txtSifra.Text));
            RefreshDataGrid();

            status = true;

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
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

        private void chkKontejner_CheckedChanged(object sender, EventArgs e)
        {

            if (chkKontejner.Checked == true)
            {
                chkZaVoz.Checked = false;
                chkUsluge.Checked = false;
                RefreshDataGridKontejnere();

            }

          
        }

        private void dataGridView4_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView4.Rows)
            {
                if (row.Selected)
                {
                    PozoviUslugu(row.Cells[0].Value.ToString()); 
                }
            }
          
        }

        private void dataGridView5_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView5.Rows)
            {
                if (row.Selected)
                {
                   txtSifraUsluge.Text = row.Cells[0].Value.ToString();
                }
            }
        }

        private void chkZaVoz_CheckedChanged(object sender, EventArgs e)
        {
            if (chkZaVoz.Checked == true)
            {
                chkKontejner.Checked = false;
                chkUsluge.Checked = false;
            
            }
        }

        private void chkUsluge_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUsluge.Checked == true)
            {
                chkZaVoz.Checked = false;
                chkKontejner.Checked = false;
              //  RefreshDataGridKontejnere();

            }
        }
    }
}
