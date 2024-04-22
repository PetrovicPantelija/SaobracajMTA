using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Saobracaj.Servis
{


    public partial class frmNamirenja : Form
    {
        private List<PictureBox> PictureBoxes = new List<PictureBox>();

        // Thumbnail sizes.
        private const int ThumbWidth = 500;
        private const int ThumbHeight = 500;

        public frmNamirenja()
        {
            InitializeComponent();

        }
        public static string code = "frmNamirenja";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Sifarnici.frmLogovanje.user.ToString();
        string niz = "";

        private void metroButton1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {


            /*
            string PictureFolder = txtPutanja.Text;
            ofd1.InitialDirectory = PictureFolder;

            if (ofd1.ShowDialog() == DialogResult.OK)
            {
                txtPutanja.Text = fbd1.SelectedPath.ToString() + ofd1.FileName;
            }

            DirectoryInfo di = new DirectoryInfo(path); FileInfo[] files = di.GetFiles("*.*"); foreach (FileInfo file in files) { pictureBox1.Image = Bitmap.FromFile(file.FullName); }

            string imeslike = "\\192.168.1.6" + @"\NamirenjaGorivomIUljem\26\16033648948435708401413646490656.jpg";
            pictureBox1.Image = new Bitmap(imeslike);
            */
            var select = "select  LokomotivaNamirenje.ID as IDNamirenja, LokomotivaVrstaNamirenja.Naziv as VrstaNamirenja,LokomotivaPrijava.Lokomotiva,  (Rtrim(Delavci.DeIme) + ' ' + Rtrim(DElavci.DePriimek)) as Zaposleni , LokomotivaNamirenje.DatumNamirenja as DatumNamirenja, LokomotivaNamirenje.Kolicina, LokomotivaNamirenje.Kolicina2 ,LokomotivaNamirenje.Napomena from LokomotivaNamirenje " +
            " inner join lokomotivaVrstaNamirenja on lokomotivaVrstaNamirenja.ID = LokomotivaNamirenje.VrstaNamirenjaID " +
            " inner join LokomotivaPrijava on LokomotivaPrijava.ID = LokomotivaNamirenje.LokomotivaPrijavaID " +
            " inner join Delavci on Delavci.DeSifra = LokomotivaPrijava.Zaposleni " +
            " order by IDNamirenja desc ";

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



        }

        private void PictureBox_DoubleClick(object sender, EventArgs e)
        {
            // Get the file's information.
            /* PictureBox pic = sender as PictureBox;
             FileInfo file_into = pic.Tag as FileInfo;

             // "Start" the file.
             Process.Start(file_into.FullName);
            */
        }

        private void txtDirectory_TextChanged(object sender, EventArgs e)
        {
            // Delete the old PictureBoxes.





            foreach (PictureBox pic in PictureBoxes)
            {
                pic.DoubleClick -= PictureBox_DoubleClick;
                pic.Dispose();
            }
            flpThumbnails.Controls.Clear();
            PictureBoxes = new List<PictureBox>();

            // If the directory doesn't exist, do nothing else.
            if (!Directory.Exists(txtDirectory.Text)) return;

            // Get the names of the files in the directory.
            List<string> filenames = new List<string>();
            string[] patterns = { "*.png", "*.gif", "*.jpg", "*.bmp", "*.tif" };
            foreach (string pattern in patterns)
            {
                filenames.AddRange(Directory.GetFiles(txtDirectory.Text,
                    pattern, SearchOption.TopDirectoryOnly));
            }
            filenames.Sort();

            // Load the files.
            foreach (string filename in filenames)
            {
                // Load the picture into a PictureBox.
                PictureBox pic = new PictureBox();

                pic.ClientSize = new Size(ThumbWidth, ThumbHeight);
                pic.Image = new Bitmap(filename);

                // If the image is too big, zoom.
                if ((pic.Image.Width > ThumbWidth) ||
                    (pic.Image.Height > ThumbHeight))
                {
                    pic.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    pic.SizeMode = PictureBoxSizeMode.CenterImage;
                }

                // Add the DoubleClick event handler.
                pic.DoubleClick += PictureBox_DoubleClick;

                // Add a tooltip.
                FileInfo file_info = new FileInfo(filename);
                /*  tipPicture.SetToolTip(pic, file_info.Name +
                      "\nCreated: " + file_info.CreationTime.ToShortDateString() +
                      "\n(" + pic.Image.Width + " x " + pic.Image.Height + ") " +
                      ToFileSizeApi(file_info.Length));
                  pic.Tag = file_info;
                */
                // Add the PictureBox to the FlowLayoutPanel.
                pic.Parent = flpThumbnails;
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
                        // txtPutanja.Text = row.Cells[2].Value.ToString();
                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
            string path = Path.Combine(@"//192.168.129.7/TA/NamirenjaGorivomIUljem/", txtSifra.Text + "/");
            //txtPutanja.Text = "\\192.168.1.6\";
            DirectoryInfo dir_info = new DirectoryInfo(path);
            txtDirectory.Text = dir_info.FullName;
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            InsertNamirenja del = new InsertNamirenja();
            del.DeleteNamirenja(Convert.ToInt32(txtSifra.Text));
            txtSifra.Enabled = false;
            RefreshDataGrid();
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            InsertNamirenja del = new InsertNamirenja();
            del.UpdateNamirenja(Convert.ToInt32(txtSifra.Text), Convert.ToDouble(txtKolicina2.Value));
            txtSifra.Enabled = false;
            RefreshDataGrid();



        }
    }
}
