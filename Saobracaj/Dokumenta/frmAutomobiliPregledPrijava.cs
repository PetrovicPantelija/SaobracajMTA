using Saobracaj.Sifarnici;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Saobracaj.Dokumenta
{
    public partial class frmAutomobiliPregledPrijava : Form
    {
        private List<PictureBox> PictureBoxes = new List<PictureBox>();
        List<string> filenames = new List<string>();
        List<string> videos = new List<string>();
        private const int ThumbWidth = 458;
        private const int ThumbHeight = 288;
        int slika = 0;
        int DosaoSpolja = 0;

        public string connect = frmLogovanje.connectionString;
        bool status = false;

        public frmAutomobiliPregledPrijava()
        {
            InitializeComponent();

        }

        public frmAutomobiliPregledPrijava(int sifra)
        {
            InitializeComponent();
            txt_Sifra.Text = sifra.ToString();
            FillGV();
            DosaoSpolja = 1;
        }
        private void frmAutomobiliPregledPrijava_Load(object sender, EventArgs e)
        {
            FillGV();
            FillData();
            if (DosaoSpolja == 1)
            {
                VratiPodatke(txt_Sifra.Text);

            }
        }
        private void FillData()
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter();

            var query = "Select DeSifra, Rtrim(DeIme) + ' ' +Rtrim(DePriimek)as Zaposleni From Delavci Order By DeIme";
            da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            combo_Zaposleni.DataSource = ds.Tables[0];
            combo_Zaposleni.DisplayMember = "Zaposleni";
            combo_Zaposleni.ValueMember = "DeSifra";

            var query1 = "Select ID,RegBr,Marka From Automobili";
            da = new SqlDataAdapter(query1, conn);
            var ds1 = new DataSet();
            da.Fill(ds1);
            combo_Automobil.DataSource = ds1.Tables[0];
            combo_Automobil.DisplayMember = "RegBr";
            combo_Automobil.ValueMember = "ID";

            var query2 = "Select Id,CistocaVrsta From CistocaSpolja";
            da = new SqlDataAdapter(query2, conn);
            var ds2 = new DataSet();
            da.Fill(ds2);
            combo_CistocaSpoljaZad.DataSource = ds2.Tables[0];
            combo_CistocaSpoljaZad.DisplayMember = "CistocaVrsta";
            combo_CistocaSpoljaZad.ValueMember = "Id";

            var query3 = "Select Id, CistocaVrsta From CistocaIznutra";
            da = new SqlDataAdapter(query3, conn);
            var ds3 = new DataSet();
            da.Fill(ds3);
            combo_CistocaUnutraZad.DataSource = ds3.Tables[0];
            combo_CistocaUnutraZad.DisplayMember = "CistocaVrsta";
            combo_CistocaUnutraZad.ValueMember = "Id";

            var query4 = "Select Id, UljeStatus From UljeAuto";
            da = new SqlDataAdapter(query4, conn);
            var ds4 = new DataSet();
            da.Fill(ds4);
            combo_NivoUljaZad.DataSource = ds4.Tables[0];
            combo_NivoUljaZad.DisplayMember = "UljeStatus";
            combo_NivoUljaZad.ValueMember = "Id";

            var query5 = "Select Id,CistocaVrsta From CistocaSpoljaRazduzivanje";
            da = new SqlDataAdapter(query5, conn);
            var ds5 = new DataSet();
            da.Fill(ds5);
            combo_CistocaSpoljaRaz.DataSource = ds5.Tables[0];
            combo_CistocaSpoljaRaz.DisplayMember = "CistocaVrsta";
            combo_CistocaSpoljaRaz.ValueMember = "Id";

            var query6 = "Select Id, CistocaVrsta From CistocaIznutraRazduzivanje";
            da = new SqlDataAdapter(query6, conn);
            var ds6 = new DataSet();
            da.Fill(ds6);
            combo_CistocaUnutraRaz.DataSource = ds6.Tables[0];
            combo_CistocaUnutraRaz.DisplayMember = "CistocaVrsta";
            combo_CistocaUnutraRaz.ValueMember = "Id";

            var query7 = "Select Id,UljeStatus From UljeAutoRazduzivanje";
            da = new SqlDataAdapter(query7, conn);
            var ds7 = new DataSet();
            da.Fill(ds7);
            combo_NivoUljaRaz.DataSource = ds7.Tables[0];
            combo_NivoUljaRaz.DisplayMember = "UljeStatus";
            combo_NivoUljaRaz.ValueMember = "Id";


            var query8 = "Select Id,Opis From Stanice";
            da = new SqlDataAdapter(query8, conn);
            var ds8 = new DataSet();
            da.Fill(ds8);
            cboMestoPolaska.DataSource = ds8.Tables[0];
            cboMestoPolaska.DisplayMember = "Opis";
            cboMestoPolaska.ValueMember = "Id";

            var query9 = "Select Id,Opis From Stanice";
            da = new SqlDataAdapter(query9, conn);
            var ds9 = new DataSet();
            da.Fill(ds9);
            cboMestoDolaska.DataSource = ds9.Tables[0];
            cboMestoDolaska.DisplayMember = "Opis";
            cboMestoDolaska.ValueMember = "Id";
        }
        private void FillGV()
        {
            var select = "  SELECT     ZaposleniPrijavaAuto.Id, AktivnostID, ZaposleniPrijavaAuto.OznakaPosla, Delavci.DeStaraSif, (RTRIM(Delavci.DeIme) + '  ' + RTRIM(Delavci.DePriimek)) AS Zaposleni, " +
               "      ZaposleniPrijavaAuto.DatumPrijave, ZaposleniPrijavaAuto.DatumOdjave, ZaposleniPrijavaAuto.AutomobilId, Automobili.RegBr, Automobili.Marka, " +
                "           ZaposleniPrijavaAuto.DirektnaPrimopredajaZaduzivanje, ZaposleniPrijavaAuto.DirektnaPrimopredajaRazduzivanje, ZaposleniPrijavaAuto.KilometrazaZaduzivanje, " +
                 "          ZaposleniPrijavaAuto.KilometrazaRazduzivanje, CistocaSpolja.CistocaVrsta AS CistocaSpolja, CistocaIznutra.CistocaVrsta AS CistocaIznutra,  " +
                 "          CistocaSpoljaRazduzivanje.CistocaVrsta AS CistocaSpoljaRazduzivanje, CistocaIznutraRazduzivanje.CistocaVrsta AS CistocaUnutraRazduzivanje,  " +
                 "          UljeAuto.UljeStatus AS UljeZaduzivanje, UljeAutoRazduzivanje.UljeStatus AS UljeRazduzivanje, ZaposleniPrijavaAuto.IdPosla,  " +
                 "          ZaposleniPrijavaAuto.NivoGorivaZaduzivanje, ZaposleniPrijavaAuto.NivoGorivaRazduzivanje, ZaposleniPrijavaAuto.Uloga, ZaposleniPrijavaAuto.MestoPolaska,  " +
                 "          ZaposleniPrijavaAuto.MestoDolaska,  ZaposleniPrijavaAuto.AktivnostId, stanice.Opis AS MestoPolaska, stanice_1.Opis AS MestoDolaska  " +
"     FROM         ZaposleniPrijavaAuto INNER JOIN  " +
                  "         Delavci ON ZaposleniPrijavaAuto.Zaposleni = Delavci.DeSifra INNER JOIN  " +
                  "         Automobili ON ZaposleniPrijavaAuto.AutomobilId = Automobili.ID left JOIN  " +
                  "         UljeAuto ON ZaposleniPrijavaAuto.NivoUljaZaduzivanje = UljeAuto.Id INNER JOIN  " +
                  "         stanice ON  ZaposleniPrijavaAuto.MestoPolaska = stanice.ID left JOIN  " +
                  "         stanice AS stanice_1 ON ZaposleniPrijavaAuto.MestoDolaska = stanice_1.ID LEFT OUTER JOIN  " +
                   "        UljeAutoRazduzivanje ON ZaposleniPrijavaAuto.NivoUljaRazduzivanje = UljeAutoRazduzivanje.Id LEFT OUTER JOIN  " +
                   "        CistocaSpoljaRazduzivanje ON ZaposleniPrijavaAuto.CistocaSpoljaRazduzivanje = CistocaSpoljaRazduzivanje.Id LEFT OUTER JOIN  " +
                  "             CistocaIznutra ON ZaposleniPrijavaAuto.CistocaIznutraZaduzivanje = CistocaIznutra.Id LEFT OUTER JOIN  " +
                  "         CistocaIznutraRazduzivanje ON ZaposleniPrijavaAuto.CistocaIznutraRazduzivanje = CistocaIznutraRazduzivanje.Id LEFT OUTER JOIN  " +
                  "         CistocaSpolja ON ZaposleniPrijavaAuto.CistocaSpoljaZaduzivanje = CistocaSpolja.Id  " +
"     ORDER BY ZaposleniPrijavaAuto.Id DESC";

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

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Aktivnosti ID";
            dataGridView1.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Oznaka posla";
            // dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "DeStaraSif";
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[3].Width = 100;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Zaposleni";
            dataGridView1.Columns[4].Width = 160;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Datum prijave";
            dataGridView1.Columns[5].Width = 100;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Datum odjave";
            dataGridView1.Columns[6].Width = 100;


            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Automobil ID";
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[7].Width = 100;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Reg br";
            dataGridView1.Columns[8].Width = 80;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Marka";
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[9].Width = 100;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "DirektnaPrimopredaja";
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[10].Width = 100;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "DirektnaPrimopredaja";
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[11].Width = 100;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Kolometraza zaduzenja";
            dataGridView1.Columns[12].Width = 80;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Kolometraza razduzenja";
            dataGridView1.Columns[13].Width = 80;

            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Cistoca spolja";
            dataGridView1.Columns[14].Visible = false;
            dataGridView1.Columns[14].Width = 100;

            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Cistoca unutra";
            dataGridView1.Columns[15].Visible = false;
            dataGridView1.Columns[15].Width = 100;

            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Cistoca spolja razduzivanje";
            dataGridView1.Columns[16].Visible = false;
            dataGridView1.Columns[16].Width = 100;

            DataGridViewColumn column18 = dataGridView1.Columns[17];
            dataGridView1.Columns[17].HeaderText = "Cistoca Unutra razduzivanje";
            dataGridView1.Columns[17].Visible = false;
            dataGridView1.Columns[17].Width = 100;

            DataGridViewColumn column19 = dataGridView1.Columns[18];
            dataGridView1.Columns[18].HeaderText = "Ulje zad";
            dataGridView1.Columns[18].Visible = false;
            dataGridView1.Columns[18].Width = 100;

            DataGridViewColumn column20 = dataGridView1.Columns[19];
            dataGridView1.Columns[19].HeaderText = "Ulje raz";
            dataGridView1.Columns[19].Visible = false;
            dataGridView1.Columns[19].Width = 100;

            DataGridViewColumn column21 = dataGridView1.Columns[20];
            dataGridView1.Columns[20].HeaderText = "ID posla";
            dataGridView1.Columns[20].Visible = false;
            dataGridView1.Columns[20].Width = 100;

            DataGridViewColumn column22 = dataGridView1.Columns[21];
            dataGridView1.Columns[21].HeaderText = "Nivo gor zad";
            dataGridView1.Columns[21].Visible = false;
            dataGridView1.Columns[21].Width = 100;

            DataGridViewColumn column23 = dataGridView1.Columns[22];
            dataGridView1.Columns[22].HeaderText = "Nivo gor raz";
            dataGridView1.Columns[22].Visible = false;
            dataGridView1.Columns[22].Width = 100;

            DataGridViewColumn column24 = dataGridView1.Columns[23];
            dataGridView1.Columns[23].HeaderText = "Uloga";
            // dataGridView1.Columns[22].Visible = false;
            dataGridView1.Columns[23].Width = 100;

            DataGridViewColumn column25 = dataGridView1.Columns[24];
            dataGridView1.Columns[24].HeaderText = "MP ID";
            dataGridView1.Columns[24].Visible = false;
            dataGridView1.Columns[24].Width = 100;

            DataGridViewColumn column26 = dataGridView1.Columns[25];
            dataGridView1.Columns[25].HeaderText = "MD ID";
            dataGridView1.Columns[25].Visible = false;
            dataGridView1.Columns[25].Width = 100;


            DataGridViewColumn column27 = dataGridView1.Columns[26];
            dataGridView1.Columns[26].HeaderText = "Aktivnosti ID";
            dataGridView1.Columns[26].Visible = false;
            dataGridView1.Columns[26].Width = 100;

            DataGridViewColumn column28 = dataGridView1.Columns[27];
            dataGridView1.Columns[27].HeaderText = "Mesto polaska";
            // dataGridView1.Columns[25].Visible = false;
            dataGridView1.Columns[27].Width = 140;

            DataGridViewColumn column29 = dataGridView1.Columns[28];
            dataGridView1.Columns[28].HeaderText = "Mesto dolaska";
            // dataGridView1.Columns[25].Visible = false;
            dataGridView1.Columns[28].Width = 140;

            /*
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[2].HeaderText = "Zaposleni";
            dataGridView1.Columns[3].HeaderText = "DatumPrijave";
            dataGridView1.Columns[6].HeaderText = "Registarski BR";
            dataGridView1.Columns[8].HeaderText = "Relacija";
            dataGridView1.Columns[11].HeaderText = "KilometrazaZaduzivanje";
            dataGridView1.Columns[12].HeaderText = "KilometrazaRazduzivanje";
            */
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertAutomobiliPregledPrijava insert = new InsertAutomobiliPregledPrijava();
            insert.DeleteAutomobiliPregledPrijava(Convert.ToInt32(txt_Sifra.Text));
            FillGV();
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            txt_Sifra.Text = "";
            txt_Sifra.Enabled = false;
            status = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            InsertAutomobiliPregledPrijava insert = new InsertAutomobiliPregledPrijava();
            bool dirPredZad = false;
            bool dirPredRaz = false;
            bool plomba1Zad = false;
            bool plomba2Zad = false;
            bool plomba1Raz = false;
            bool plomba2Raz = false;
            if (cb_DirPredZad.Checked)
            {
                dirPredZad = true;
            }
            if (cb_DirPredRaz.Checked)
            {
                dirPredRaz = true;

                if (txt_KmRazduzenje.Text.Equals(""))
                {
                    txt_KmRazduzenje.Text = "0";
                }
                if (txt_KmZaduzenje.Text.Equals(""))
                {
                    txt_KmZaduzenje.Text = "0";
                }
                if (status == true)
                {
                    insert.InsAutomobiliPregledPrijava(Convert.ToInt32(combo_Zaposleni.SelectedValue.ToString()),
                        Convert.ToDateTime(dtpDatumPrijave.Value),
                        Convert.ToDateTime(dt_Odjava.Value), Convert.ToInt32(combo_Automobil.SelectedValue.ToString()),
                        Convert.ToInt32(combo_CistocaSpoljaZad.SelectedValue.ToString()), Convert.ToInt32(combo_CistocaUnutraZad.SelectedValue.ToString()),
                        Convert.ToInt32(combo_CistocaSpoljaRaz.SelectedValue.ToString()), Convert.ToInt32(combo_CistocaUnutraRaz.SelectedValue.ToString()),
                        Convert.ToInt32(combo_NivoUljaZad.SelectedValue.ToString()), dirPredZad, Convert.ToInt32(combo_NivoUljaRaz.SelectedValue.ToString()),
                        dirPredRaz, float.Parse(txt_KmZaduzenje.Text), float.Parse(txt_KmRazduzenje.Text), plomba1Zad, plomba2Zad,
                        plomba1Raz, plomba2Raz);
                    FillGV();

                    txt_Sifra.Enabled = true;
                    status = false;
                }
                else
                {
                    insert.UpdAutomobiliPregledPrijava(Convert.ToInt32(txt_Sifra.Text), Convert.ToInt32(combo_Zaposleni.SelectedValue.ToString()),
                        Convert.ToDateTime(dtpDatumPrijave.Value),
                        Convert.ToDateTime(dt_Odjava.Value), Convert.ToInt32(combo_Automobil.SelectedValue.ToString()),
                        Convert.ToInt32(combo_CistocaSpoljaZad.SelectedValue.ToString()), Convert.ToInt32(combo_CistocaUnutraZad.SelectedValue.ToString()),
                        Convert.ToInt32(combo_CistocaSpoljaRaz.SelectedValue.ToString()), Convert.ToInt32(combo_CistocaUnutraRaz.SelectedValue.ToString()),
                        Convert.ToInt32(combo_NivoUljaZad.SelectedValue.ToString()), dirPredZad, Convert.ToInt32(combo_NivoUljaRaz.SelectedValue.ToString()),
                        dirPredRaz, float.Parse(txt_KmZaduzenje.Text), float.Parse(txt_KmRazduzenje.Text), plomba1Zad, plomba2Zad,
                        plomba1Raz, plomba2Raz);
                    FillGV();
                }
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
                        txt_Sifra.Text = row.Cells[0].Value.ToString();
                        VratiPodatke(txt_Sifra.Text);


                        if (row.Cells[9].Value.Equals(true))
                        {
                            cb_DirPredZad.Checked = true;
                        }
                        PictureBoxes.Clear();
                        filenames.Clear();
                        pictureBox1.Image = null;
                        // FillGV();
                    }
                }
            }
            catch
            {

            }
        }
        private void Slike()
        {

            if (txt_Sifra.Text.Equals(""))
            {
                MessageBox.Show("Mora se izabrati zapis");
            }
            else
            {
                string folder = txt_Sifra.Text.ToString().TrimEnd();
                string path = @"\\192.168.129.7\TA\CistocaSluzbeniAutomobili\" + folder;
                string[] files = Directory.GetFiles(path);
                if (files.Length == 0)
                {
                    MessageBox.Show("Nema dodatih slika");
                }
                else
                {
                    string[] filterVideo = { "*.heic", ".*mp4" };
                    foreach (string video in filterVideo)
                    {
                        videos.AddRange(Directory.GetFiles(path, video, SearchOption.TopDirectoryOnly));
                        if (videos.Count > 0)
                        {
                            MessageBox.Show("Video se mora otvoriti iz foldera");
                            videos.Clear();
                            System.Diagnostics.Process.Start(path);
                            return;
                        }

                    }
                    string[] paterns = { "*.png", "*.gif", "*.jpg", "*.bmp", "*.tif" };

                    foreach (string pattern in paterns)
                    {
                        filenames.AddRange(Directory.GetFiles(path, pattern, SearchOption.TopDirectoryOnly));
                    }
                    filenames.Sort();
                    for (int i = 0; i < files.Length; i++)
                    {
                        if (slika < 0 || slika > files.Length - 1)
                        {
                            if (slika < 0)
                            {
                                slika = 0;
                            }
                            if (slika > files.Length - 1)
                            {
                                slika = files.Length - 1;
                            }
                            return;
                        }
                        else
                        {
                            //pictureBox1.ClientSize = new Size(ThumbWidth, ThumbHeight);
                            pictureBox1.Image = new Bitmap(files[slika]);

                            // If the image is too big, zoom.
                            if ((pictureBox1.Image.Width > ThumbWidth) ||
                                (pictureBox1.Image.Height > ThumbHeight))
                            {
                                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                            }
                            else
                            {
                                pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
                            }
                        }
                    }
                }
            }
        }
        private void btn_OtvoriSliku_Click(object sender, EventArgs e)
        {
            Slike();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string folder = txt_Sifra.Text.ToString().TrimEnd();
            string path = @"\\192.168.129.7\TA\CistocaSluzbeniAutomobili\" + folder;
            System.Diagnostics.Process.Start(path);
        }

        private void btn_Napred_Click(object sender, EventArgs e)
        {
            slika++;
            Slike();
        }

        private void btn_nazad_Click(object sender, EventArgs e)
        {
            slika--;
            Slike();
        }


        private void VratiPodatke(string ID)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT [Id] " +
    " ,[Zaposleni]      ,[DatumPrijave]      ,[DatumOdjave]      ,[AutomobilId] " +
    "   ,[CistocaSpoljaZaduzivanje]      ,[CistocaIznutraZaduzivanje] " +
    "   ,[CistocaSpoljaRazduzivanje]      ,[CistocaIznutraRazduzivanje] " +
     "  ,[NivoUljaZaduzivanje]      ,[DirektnaPrimopredajaZaduzivanje] " +
    "   ,[NivoUljaRazduzivanje]      ,[DirektnaPrimopredajaRazduzivanje] " +
    "   ,[KilometrazaZaduzivanje]      ,[KilometrazaRazduzivanje] " +
    "   ,[OznakaPosla]      ,[IdPosla]      ,[NivoGorivaZaduzivanje]      ,[NivoGorivaRazduzivanje] " +
     "  ,[MestoPolaska]      ,[MestoDolaska]      ,[AktivnostId]      ,[Uloga] " +
 "  FROM [ZaposleniPrijavaAuto] where ID=" + txt_Sifra.Text, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                combo_Zaposleni.SelectedValue = Convert.ToInt32(dr["Zaposleni"].ToString());
                dtpDatumPrijave.Value = Convert.ToDateTime(dr["DatumPrijave"].ToString());
                dt_Odjava.Value = Convert.ToDateTime(dr["DatumOdjave"].ToString());
                combo_Automobil.SelectedValue = Convert.ToInt32(dr["AutomobilId"].ToString());
                combo_CistocaSpoljaZad.SelectedValue = Convert.ToInt32(dr["CistocaSpoljaZaduzivanje"].ToString()); ;
                combo_CistocaUnutraZad.SelectedValue = Convert.ToInt32(dr["CistocaIznutraZaduzivanje"].ToString()); ;
                combo_CistocaSpoljaRaz.SelectedValue = Convert.ToInt32(dr["CistocaSpoljaRazduzivanje"].ToString());
                combo_CistocaUnutraRaz.SelectedValue = Convert.ToInt32(dr["CistocaIznutraRazduzivanje"].ToString());
                combo_NivoUljaZad.SelectedValue = Convert.ToInt32(dr["NivoUljaZaduzivanje"].ToString());
                combo_NivoUljaRaz.SelectedValue = Convert.ToInt32(dr["NivoUljaRazduzivanje"].ToString());
                if (dr["DirektnaPrimopredajaZaduzivanje"].ToString() == "1")
                {
                    cb_DirPredZad.Checked = true;
                }
                else
                {
                    cb_DirPredZad.Checked = false;
                }
                if (dr["DirektnaPrimopredajaRazduzivanje"].ToString() == "1")
                {
                    cb_DirPredRaz.Checked = true;
                }
                else
                {
                    cb_DirPredRaz.Checked = false;
                }

                txtNGZaduzenje.Text = dr["NivoGorivaZaduzivanje"].ToString();
                txtNGRazduzenje.Text = dr["NivoGorivaRazduzivanje"].ToString();
                txt_KmZaduzenje.Text = dr["KilometrazaZaduzivanje"].ToString();
                txt_KmRazduzenje.Text = dr["KilometrazaRazduzivanje"].ToString();
                txtPosao.Text = dr["OznakaPosla"].ToString();
                txtUloga.Text = dr["Uloga"].ToString();
                cboMestoPolaska.SelectedValue = Convert.ToInt32(dr["MestoPolaska"].ToString());
                cboMestoDolaska.SelectedValue = Convert.ToInt32(dr["MestoDolaska"].ToString());

            }
            con.Close();
        }
    }
}