using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Tehnologija
{
    public partial class frmTehnologijaPregled : Syncfusion.Windows.Forms.Office2010Form
    {
        public static string code = "frmTehnologijaPregled";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Sifarnici.frmLogovanje.user.ToString();
        string niz = "";

        string KorisnikP = "";
        public frmTehnologijaPregled(string Korisnik)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            InitializeComponent();

            KorisnikP = Korisnik;
        }
        private void frmTehnologijaPregled_Load(object sender, EventArgs e)
        {
            var select = "  select Tehnologija.ID as IDTehnologije, " +
                " [PorudzbinaID]  as MP      , MpStaraSif as Kod, MpNaziv as Naziv, [Prazan]       ,[Napomena]       ,[Tonaza]       ,[TonazaPovratak] " +
                "  from Tehnologija " +
" inner join " +
" MaticniPodatki  on Tehnologija.PorudzbinaID = MaticniPodatki.MpSifra";


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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txtSifra.Text = row.Cells[0].Value.ToString();
                        // VratiPodatke(txtSifra.Text);
                        // txtOpis.Text = row.Cells[1].Value.ToString();
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
            Tehnologija.frmTehnologija tehn = new frmTehnologija(Convert.ToInt32(txtSifra.Text), KorisnikP);
            tehn.Show();

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            var select = "  select Tehnologija.ID as IDTehnologije, " +
                " [PorudzbinaID]  as MP      , MpStaraSif as Kod, MpNaziv as Naziv, [Prazan]       ,[Napomena]       ,[Tonaza]       ,[TonazaPovratak] " +
                "  from Tehnologija " +
" inner join " +
" MaticniPodatki  on Tehnologija.PorudzbinaID = MaticniPodatki.MpSifra";



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
            /*
                        DataGridViewColumn column = dataGridView1.Columns[0];
                        dataGridView1.Columns[0].HeaderText = "Tehn ID";
                        dataGridView1.Columns[0].Width = 35;

                        DataGridViewColumn column2 = dataGridView1.Columns[1];
                        dataGridView1.Columns[1].HeaderText = "Stavka ID";
                        dataGridView1.Columns[1].Width = 35;

                        DataGridViewColumn column3 = dataGridView1.Columns[2];
                        dataGridView1.Columns[2].HeaderText = "Porudzbina";
                        dataGridView1.Columns[2].Width = 50;

                        DataGridViewColumn column4 = dataGridView1.Columns[3];
                        dataGridView1.Columns[3].HeaderText = "Partner";
                        dataGridView1.Columns[3].Width = 50;

                        DataGridViewColumn column5 = dataGridView1.Columns[4];
                        dataGridView1.Columns[4].HeaderText = "Pa Naziv";
                        dataGridView1.Columns[4].Width = 170;

                        DataGridViewColumn column6 = dataGridView1.Columns[5];
                        dataGridView1.Columns[5].HeaderText = "Datum";
                        dataGridView1.Columns[5].Width = 70;

                        DataGridViewColumn column7 = dataGridView1.Columns[6];
                        dataGridView1.Columns[6].HeaderText = "MP";
                        dataGridView1.Columns[6].Width = 290;

                        DataGridViewColumn column8 = dataGridView1.Columns[7];
                        dataGridView1.Columns[7].HeaderText = "JM";
                        dataGridView1.Columns[7].Width = 30;

                        DataGridViewColumn column9 = dataGridView1.Columns[8];
                        dataGridView1.Columns[8].HeaderText = "JM2";
                        dataGridView1.Columns[8].Width = 30;

                        DataGridViewColumn column10 = dataGridView1.Columns[9];
                        dataGridView1.Columns[9].HeaderText = "kol";
                        dataGridView1.Columns[9].Width = 50;

                        DataGridViewColumn column11 = dataGridView1.Columns[10];
                        dataGridView1.Columns[10].HeaderText = "kol 2";
                        dataGridView1.Columns[10].Width = 50;

                        DataGridViewColumn column12 = dataGridView1.Columns[11];
                        dataGridView1.Columns[11].HeaderText = "NHM";
                        dataGridView1.Columns[11].Width = 70;

                        DataGridViewColumn column13 = dataGridView1.Columns[12];
                        dataGridView1.Columns[12].HeaderText = "Napomena";
                        dataGridView1.Columns[12].Width = 170;
            */
        }
    }
}
