using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Dokumenta
{
    public partial class frmPregledAktivnosti : Form
    {
        public frmPregledAktivnosti()
        {
            InitializeComponent();
        }

        private void btnPretrazi_Click(object sender, EventArgs e)
        {
            string dwhere = " where 1=1";
            if (chkAutomobili.Checked == false)
            {

                dwhere = dwhere + " and AktivnostiStavke.VrstaAktivnostiID <> 57 ";


            }
            var select = "";
            select = " SELECT     AktivnostiStavke.ID, AktivnostiStavke.IDNadredjena, AktivnostiStavke.VrstaAktivnostiID, VrstaAktivnosti.Naziv, AktivnostiStavke.DatumPocetka, " +
                     " AktivnostiStavke.DatumZavrsetka, AktivnostiStavke.Posao, AktivnostiStavke.OznakaPosla, AktivnostiStavke.MestoIzvrsenja, AktivnostiStavke.Teretnica, " +
                      "  AktivnostiStavke.Lokomotiva, AktivnostiStavke.Stanica, Stanice.Opis, Aktivnosti.Zaposleni, Delavci.DeSifra, Delavci.DeIme,Delavci.DePriimek,  Aktivnosti.VremeOd, " +
                     "  Aktivnosti.VremeDo " +
" FROM         AktivnostiStavke INNER JOIN " +
                      " VrstaAktivnosti ON AktivnostiStavke.VrstaAktivnostiID = VrstaAktivnosti.ID INNER JOIN " +
                     "  Aktivnosti ON AktivnostiStavke.IDNadredjena = Aktivnosti.ID INNER JOIN " +
                     "  Delavci ON Aktivnosti.Zaposleni = Delavci.DeSifra " +
                     " inner join Stanice on Stanice.ID = AktivnostiStavke.Stanica " + dwhere +
                     "  order by AktivnostiStavke.DatumPocetka desc   ";


            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
            dataGridView1.Columns[0].HeaderText = "Stavke ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Nadr ID";
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[1].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "VA ID";
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[2].Width = 50;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Vrsta aktivnosti";
            dataGridView1.Columns[3].Width = 150;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Datum pocetka";
            dataGridView1.Columns[4].Width = 100;

            DataGridViewColumn column5 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Datum zavrsetka";
            dataGridView1.Columns[5].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Posao";
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[6].Width = 100;

            DataGridViewColumn column7 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Oznaka posla";
            dataGridView1.Columns[7].Width = 100;

            DataGridViewColumn column8 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Mesto izvrsenja";
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[8].Width = 100;

            DataGridViewColumn column9 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Teretnica";
            dataGridView1.Columns[9].Width = 100;

            DataGridViewColumn column10 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Lokomotiva";
            dataGridView1.Columns[10].Width = 100;

            DataGridViewColumn column11 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Stanica ID";
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[11].Width = 100;

            DataGridViewColumn column12 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Stanica Naziv";
            dataGridView1.Columns[12].Width = 100;

            DataGridViewColumn column13 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Zaposleni";
            dataGridView1.Columns[13].Visible = false;
            dataGridView1.Columns[13].Width = 100;

            DataGridViewColumn column14 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "DeSifra";
            dataGridView1.Columns[14].Visible = false;
            dataGridView1.Columns[14].Width = 100;

            DataGridViewColumn column15 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Ime";
            dataGridView1.Columns[15].Width = 80;

            DataGridViewColumn column16 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Prezime";
            dataGridView1.Columns[16].Width = 80;

            DataGridViewColumn column17 = dataGridView1.Columns[17];
            dataGridView1.Columns[17].HeaderText = "VremeOd";
            dataGridView1.Columns[17].Width = 100;

            DataGridViewColumn column18 = dataGridView1.Columns[18];
            dataGridView1.Columns[18].HeaderText = "VremeDo";
            dataGridView1.Columns[18].Width = 100;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dwhere = " ";
            if (chkAutomobili.Checked == false)
            {

                dwhere = dwhere + " and AktivnostiStavke.VrstaAktivnostiID <> 57 ";


            }
            var select = "";
            select = " SELECT     AktivnostiStavke.ID, AktivnostiStavke.IDNadredjena, AktivnostiStavke.VrstaAktivnostiID, VrstaAktivnosti.Naziv, AktivnostiStavke.DatumPocetka, " +
                     " AktivnostiStavke.DatumZavrsetka, AktivnostiStavke.Posao, AktivnostiStavke.OznakaPosla, AktivnostiStavke.MestoIzvrsenja, AktivnostiStavke.Teretnica, " +
                      "  AktivnostiStavke.Lokomotiva, AktivnostiStavke.Stanica, Stanice.Opis, Aktivnosti.Zaposleni, Delavci.DeSifra, Delavci.DeIme,Delavci.DePriimek,  Aktivnosti.VremeOd, " +
                     "  Aktivnosti.VremeDo " +
" FROM         AktivnostiStavke INNER JOIN " +
                      " VrstaAktivnosti ON AktivnostiStavke.VrstaAktivnostiID = VrstaAktivnosti.ID INNER JOIN " +
                     "  Aktivnosti ON AktivnostiStavke.IDNadredjena = Aktivnosti.ID " +
                       " inner join Stanice on Stanice.ID = AktivnostiStavke.Stanica " +
                     " INNER JOIN " +
                     "  Delavci ON Aktivnosti.Zaposleni = Delavci.DeSifra where AktivnostiStavke.OznakaPosla = '" + cboPosao.SelectedValue + "'" + dwhere +
                     "  order by AktivnostiStavke.DatumPocetka desc   ";


            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
            dataGridView1.Columns[0].HeaderText = "Stavke ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Nadr ID";
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[1].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "VA ID";
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[2].Width = 50;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Vrsta aktivnosti";
            dataGridView1.Columns[3].Width = 150;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Datum pocetka";
            dataGridView1.Columns[4].Width = 100;

            DataGridViewColumn column5 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Datum zavrsetka";
            dataGridView1.Columns[5].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Posao";
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[6].Width = 100;

            DataGridViewColumn column7 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Oznaka posla";
            dataGridView1.Columns[7].Width = 100;

            DataGridViewColumn column8 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Mesto izvrsenja";
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[8].Width = 100;

            DataGridViewColumn column9 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Teretnica";
            dataGridView1.Columns[9].Width = 100;

            DataGridViewColumn column10 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Lokomotiva";
            dataGridView1.Columns[10].Width = 100;

            DataGridViewColumn column11 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Stanica ID";
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[11].Width = 100;

            DataGridViewColumn column12 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Stanica Naziv";
            dataGridView1.Columns[12].Width = 100;

            DataGridViewColumn column13 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Zaposleni";
            dataGridView1.Columns[13].Visible = false;
            dataGridView1.Columns[13].Width = 100;

            DataGridViewColumn column14 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "DeSifra";
            dataGridView1.Columns[14].Visible = false;
            dataGridView1.Columns[14].Width = 100;

            DataGridViewColumn column15 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Ime";
            dataGridView1.Columns[15].Width = 80;

            DataGridViewColumn column16 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Prezime";
            dataGridView1.Columns[16].Width = 80;

            DataGridViewColumn column17 = dataGridView1.Columns[17];
            dataGridView1.Columns[17].HeaderText = "VremeOd";
            dataGridView1.Columns[17].Width = 100;

            DataGridViewColumn column18 = dataGridView1.Columns[18];
            dataGridView1.Columns[18].HeaderText = "VremeDo";
            dataGridView1.Columns[18].Width = 100;
        }

        private void frmPregledAktivnosti_Load(object sender, EventArgs e)
        {
            /*var select3 = " select Oznaka from Najava  order by Oznaka";
            var s_connection3 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboPosao.DataSource = ds3.Tables[0];
            cboPosao.DisplayMember = "Oznaka";
            cboPosao.ValueMember = "Oznaka";*/
            
            chkAktvni.Checked = true;
            chkAutomobili.Checked = false;
            Cekirano();
        }

        private void cboPosao_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmPregledLokomotivaPrimopredaja plp = new frmPregledLokomotivaPrimopredaja();
            plp.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmAutomobiliPregledPrijava")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmAutomobiliPregledPrijava pa = new Dokumenta.frmAutomobiliPregledPrijava(Convert.ToInt32(txt_Sifra.Text));
                pa.Show();
            }


        }
        private void FillDG2Automobil(string ID)
        {
            var select = "  SELECT     ZaposleniPrijavaAuto.Id, AktivnostID, ZaposleniPrijavaAuto.OznakaPosla, Delavci.DeStaraSif, (RTRIM(Delavci.DeIme) + '  ' + RTRIM(Delavci.DePriimek)) AS Zaposleni, " +
               "      ZaposleniPrijavaAuto.DatumPrijave, ZaposleniPrijavaAuto.DatumOdjave, ZaposleniPrijavaAuto.AutomobilId, Automobili.RegBr, Automobili.Marka, " +
                "           ZaposleniPrijavaAuto.DirektnaPrimopredajaZaduzivanje, ZaposleniPrijavaAuto.DirektnaPrimopredajaRazduzivanje, ZaposleniPrijavaAuto.KilometrazaZaduzivanje, " +
                 "          ZaposleniPrijavaAuto.KilometrazaRazduzivanje, CistocaSpolja.CistocaVrsta AS CistocaSpolja, CistocaIznutra.CistocaVrsta AS CistocaIznutra,  " +
                 "          CistocaSpoljaRazduzivanje.CistocaVrsta AS CistocaSpoljaRazduzivanje, CistocaIznutraRazduzivanje.CistocaVrsta AS CistocaUnutraRazduzivanje,  " +
                 "          UljeAuto.UljeStatus AS UljeZaduzivanje, UljeAutoRazduzivanje.UljeStatus AS UljeRazduzivanje, ZaposleniPrijavaAuto.IdPosla,  " +
                 "          ZaposleniPrijavaAuto.NivoGorivaZaduzivanje, ZaposleniPrijavaAuto.NivoGorivaRazduzivanje, ZaposleniPrijavaAuto.Uloga, ZaposleniPrijavaAuto.MestoPolaska,  " +
                 "          ZaposleniPrijavaAuto.MestoDolaska,  ZaposleniPrijavaAuto.AktivnostId, stanice.Opis AS MestoPolaska, stanice_1.Opis AS MestoDolaska  " +
"     FROM         ZaposleniPrijavaAuto INNER JOIN AktivnostiStavke ON ZAposleniPrijavaAuto.AktivnostID = AktivnostiStavke.ID inner join Aktivnosti on AktivnostiStavke.IDNadredjena = Aktivnosti.ID INNER JOIN  " +
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
"    where  AktivnostiStavke.Id = '" + ID + "'";

            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
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

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "Aktivnosti ID";
            dataGridView2.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "Oznaka posla";
            // dataGridView1.Columns[2].Visible = false;
            dataGridView2.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView2.Columns[3];
            dataGridView2.Columns[3].HeaderText = "DeStaraSif";
            dataGridView2.Columns[3].Visible = false;
            dataGridView2.Columns[3].Width = 100;



            DataGridViewColumn column5 = dataGridView2.Columns[4];
            dataGridView2.Columns[4].HeaderText = "Zaposleni";
            dataGridView2.Columns[4].Width = 160;

            DataGridViewColumn column6 = dataGridView2.Columns[5];
            dataGridView2.Columns[5].HeaderText = "Datum prijave";
            dataGridView2.Columns[5].Width = 100;

            DataGridViewColumn column7 = dataGridView2.Columns[6];
            dataGridView2.Columns[6].HeaderText = "Datum odjave";
            dataGridView2.Columns[6].Width = 100;


            DataGridViewColumn column8 = dataGridView2.Columns[7];
            dataGridView2.Columns[7].HeaderText = "Automobil ID";
            dataGridView2.Columns[7].Visible = false;
            dataGridView2.Columns[7].Width = 100;

            DataGridViewColumn column9 = dataGridView2.Columns[8];
            dataGridView2.Columns[8].HeaderText = "Reg br";
            dataGridView2.Columns[8].Width = 80;

            DataGridViewColumn column10 = dataGridView2.Columns[9];
            dataGridView2.Columns[9].HeaderText = "Marka";
            dataGridView2.Columns[9].Visible = false;
            dataGridView2.Columns[9].Width = 100;





            DataGridViewColumn column11 = dataGridView2.Columns[10];
            dataGridView2.Columns[10].HeaderText = "DirektnaPrimopredaja";
            dataGridView2.Columns[10].Visible = false;
            dataGridView2.Columns[10].Width = 100;

            DataGridViewColumn column12 = dataGridView2.Columns[11];
            dataGridView2.Columns[11].HeaderText = "DirektnaPrimopredaja";
            dataGridView2.Columns[11].Visible = false;
            dataGridView2.Columns[11].Width = 100;

            DataGridViewColumn column13 = dataGridView2.Columns[12];
            dataGridView2.Columns[12].HeaderText = "Kolometraza zaduzenja";
            dataGridView2.Columns[12].Width = 80;

            DataGridViewColumn column14 = dataGridView2.Columns[13];
            dataGridView2.Columns[13].HeaderText = "Kolometraza razduzenja";
            dataGridView2.Columns[13].Width = 80;



            DataGridViewColumn column15 = dataGridView2.Columns[14];
            dataGridView2.Columns[14].HeaderText = "Cistoca spolja";
            dataGridView2.Columns[14].Visible = false;
            dataGridView2.Columns[14].Width = 100;

            DataGridViewColumn column16 = dataGridView2.Columns[15];
            dataGridView2.Columns[15].HeaderText = "Cistoca unutra";
            dataGridView2.Columns[15].Visible = false;
            dataGridView2.Columns[15].Width = 100;

            DataGridViewColumn column17 = dataGridView2.Columns[16];
            dataGridView2.Columns[16].HeaderText = "Cistoca spolja razduzivanje";
            dataGridView2.Columns[16].Visible = false;
            dataGridView2.Columns[16].Width = 100;

            DataGridViewColumn column18 = dataGridView2.Columns[17];
            dataGridView2.Columns[17].HeaderText = "Cistoca Unutra razduzivanje";
            dataGridView2.Columns[17].Visible = false;
            dataGridView2.Columns[17].Width = 100;


            DataGridViewColumn column19 = dataGridView2.Columns[18];
            dataGridView2.Columns[18].HeaderText = "Ulje zad";
            dataGridView2.Columns[18].Visible = false;
            dataGridView2.Columns[18].Width = 100;

            DataGridViewColumn column20 = dataGridView2.Columns[19];
            dataGridView2.Columns[19].HeaderText = "Ulje raz";
            dataGridView2.Columns[19].Visible = false;
            dataGridView2.Columns[19].Width = 100;




            DataGridViewColumn column21 = dataGridView2.Columns[20];
            dataGridView2.Columns[20].HeaderText = "ID posla";
            dataGridView2.Columns[20].Visible = false;
            dataGridView2.Columns[20].Width = 100;

            DataGridViewColumn column22 = dataGridView2.Columns[21];
            dataGridView2.Columns[21].HeaderText = "Nivo gor zad";
            dataGridView2.Columns[21].Visible = false;
            dataGridView2.Columns[21].Width = 100;

            DataGridViewColumn column23 = dataGridView2.Columns[22];
            dataGridView2.Columns[22].HeaderText = "Nivo gor raz";
            dataGridView2.Columns[22].Visible = false;
            dataGridView2.Columns[22].Width = 100;



            DataGridViewColumn column24 = dataGridView2.Columns[23];
            dataGridView2.Columns[23].HeaderText = "Uloga";
            // dataGridView1.Columns[22].Visible = false;
            dataGridView2.Columns[23].Width = 100;

            DataGridViewColumn column25 = dataGridView2.Columns[24];
            dataGridView2.Columns[24].HeaderText = "MP ID";
            dataGridView2.Columns[24].Visible = false;
            dataGridView2.Columns[24].Width = 10;

            DataGridViewColumn column26 = dataGridView2.Columns[25];
            dataGridView2.Columns[25].HeaderText = "MD ID";
            dataGridView2.Columns[25].Visible = false;
            dataGridView2.Columns[25].Width = 10;


            DataGridViewColumn column27 = dataGridView2.Columns[26];
            dataGridView2.Columns[26].HeaderText = "Aktivnosti ID";
            dataGridView2.Columns[26].Visible = false;
            dataGridView2.Columns[26].Width = 100;

            DataGridViewColumn column28 = dataGridView2.Columns[27];
            dataGridView2.Columns[27].HeaderText = "Mesto polaska";
            //  dataGridView2.Columns[27].Visible = false;
            dataGridView2.Columns[27].Width = 140;

            DataGridViewColumn column29 = dataGridView2.Columns[28];
            dataGridView2.Columns[28].HeaderText = "Mesto dolaska";
            //   dataGridView2.Columns[28].Visible = false;
            dataGridView2.Columns[28].Width = 140;

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

        private void FillGV2VUCA(string ID)
        {

            var select = " SELECT    Vuca.ID, AktivnostiStavke.OznakaPosla, SifVucaStatusi.Naziv, VucaStatusi.Vreme, VucaStatusi.Napomena as NapomenaVucaStatus, " +
        " Stanice.OPis as StanicaStatus,   Vuca.StavkaAktivnostiID, Vuca.VrstaPosla, Vuca.VrstaPrimopredaje, " +
         "  Vuca.Kilometraza, " +
         " Vuca.MotoSati, Vuca.NivoGoriva, Vuca.Napomena, Vuca.Lokomotiva, " +
        " Vuca.Dizel, AktivnostiStavke.VrstaAktivnostiID, AktivnostiStavke.Sati, " +
         " AktivnostiStavke.Posao, AktivnostiStavke.DatumPocetka,AktivnostiStavke.DatumZavrsetka,  " +
         " Delavci.DeSifra, Delavci.DeIme, DePriimek " +
        " FROM         Vuca INNER JOIN " +
        " AktivnostiStavke ON Vuca.StavkaAktivnostiID = AktivnostiStavke.ID " +
        " inner join Aktivnosti on Aktivnosti.ID = AktivnostiStavke.IdNadredjena " +
        " inner join Delavci on Delavci.DeSifra = Aktivnosti.Zaposleni " +
        " inner join VucaStatusi on VucaStatusi.VucaID = VUca.ID " +
        " inner join SifVucaStatusi on VucaStatusi.Status = SifVucaStatusi.ID " +
        " inner join Stanice on VucaStatusi.Stanica = Stanice.ID " +
        " where AktivnostiStavke.ID = '" + ID + "' order by VucaStatusi.Vreme asc";

            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
            dataGridView2.Columns[0].Width = 50;

            DataGridViewColumn column1 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "Posao";
            dataGridView2.Columns[1].Width = 150;

            DataGridViewColumn column2 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "Status";
            dataGridView2.Columns[2].Width = 100;

            DataGridViewColumn column3 = dataGridView2.Columns[3];
            dataGridView2.Columns[3].HeaderText = "Vreme";
            dataGridView2.Columns[3].Width = 90;

            DataGridViewColumn column4 = dataGridView2.Columns[4];
            dataGridView2.Columns[4].HeaderText = "NapomenaVucaStatus";
            dataGridView2.Columns[4].Width = 120;

            DataGridViewColumn column5 = dataGridView2.Columns[5];
            dataGridView2.Columns[5].HeaderText = "Stanica";
            dataGridView2.Columns[5].Width = 100;

            DataGridViewColumn column6 = dataGridView2.Columns[6];
            dataGridView2.Columns[6].HeaderText = "StavkaAktivnostiID";
            dataGridView2.Columns[6].Visible = false;
            dataGridView2.Columns[6].Width = 60;

            DataGridViewColumn column7 = dataGridView2.Columns[7];
            dataGridView2.Columns[7].HeaderText = "Vrsta Posla";
            dataGridView2.Columns[7].Width = 100;

            DataGridViewColumn column8 = dataGridView2.Columns[8];
            dataGridView2.Columns[8].HeaderText = "Vrsta primopredaje";
            dataGridView2.Columns[8].Visible = false;
            dataGridView2.Columns[8].Width = 100;


            DataGridViewColumn column9 = dataGridView2.Columns[9];
            dataGridView2.Columns[9].HeaderText = "Kilometraža";
            dataGridView2.Columns[9].Width = 100;

            DataGridViewColumn column10 = dataGridView2.Columns[10];
            dataGridView2.Columns[10].HeaderText = "Moto sati";
            dataGridView2.Columns[10].Width = 100;

            DataGridViewColumn column11 = dataGridView2.Columns[11];
            dataGridView2.Columns[11].HeaderText = "Nivo goriva";
            dataGridView2.Columns[11].Width = 80;

            DataGridViewColumn column12 = dataGridView2.Columns[12];
            dataGridView2.Columns[12].HeaderText = "Napomena";
            dataGridView2.Columns[12].Width = 150;

            DataGridViewColumn column13 = dataGridView2.Columns[13];
            dataGridView2.Columns[13].HeaderText = "Lokomotiva";
            dataGridView2.Columns[13].Width = 100;

            DataGridViewColumn column14 = dataGridView2.Columns[14];
            dataGridView2.Columns[14].HeaderText = "Dizel";
            dataGridView2.Columns[14].Visible = false;
            dataGridView2.Columns[14].Width = 100;

            DataGridViewColumn column15 = dataGridView2.Columns[15];
            dataGridView2.Columns[15].HeaderText = "VrstaAktivnostiID";
            dataGridView2.Columns[15].Visible = false;
            dataGridView2.Columns[15].Width = 100;

            DataGridViewColumn column16 = dataGridView2.Columns[16];
            dataGridView2.Columns[16].HeaderText = "Sati";
            dataGridView2.Columns[16].Width = 80;


            DataGridViewColumn column17 = dataGridView2.Columns[17];
            dataGridView2.Columns[17].HeaderText = "Posao";
            dataGridView2.Columns[17].Visible = false;
            dataGridView2.Columns[17].Width = 80;

            DataGridViewColumn column18 = dataGridView2.Columns[18];
            dataGridView2.Columns[18].HeaderText = "Datum pocetka";
            dataGridView2.Columns[18].Visible = false;
            dataGridView2.Columns[18].Width = 120;

            DataGridViewColumn column19 = dataGridView2.Columns[19];
            dataGridView2.Columns[19].HeaderText = "Datum zavrsetka";
            dataGridView2.Columns[19].Visible = false;
            dataGridView2.Columns[19].Width = 120;

            DataGridViewColumn column20 = dataGridView2.Columns[20];
            dataGridView2.Columns[20].HeaderText = "DeSifra";
            dataGridView2.Columns[20].Visible = false;
            dataGridView2.Columns[20].Width = 120;

            DataGridViewColumn column21 = dataGridView2.Columns[21];
            dataGridView2.Columns[21].HeaderText = "Ime";
            dataGridView2.Columns[21].Visible = false;
            dataGridView2.Columns[21].Width = 100;

            DataGridViewColumn column22 = dataGridView2.Columns[22];
            dataGridView2.Columns[22].HeaderText = "Prezime";
            dataGridView2.Columns[22].Visible = false;
            dataGridView2.Columns[22].Width = 100;



        }


        private void FillGV2TehnickiPregled(string ID)
        {
            var select = "  SELECT     TehnickiPregled.ID, TehnickiPregled.IDStavke, TehnickiPregled.RedniBrojKola, TehnickiPregled.BrojKola, SifRazredNepravilnosti.ID AS RazredNepravilnostiID, " +
                 "       SifRazredNepravilnosti.Naziv AS RazredNepravilnostiNaziv, SifGrupaNepravilnosti.Naziv AS GrupaNeispravnostiNaziv,  " +
                  "      SifNeispravnostPostupak.ID AS IDNeispravnostPostupak, SifNeispravnostPostupak.Naziv AS NeispravnostNaziv, TehnickiPregled.Napomena, " +
                 "       TehnickiPregled.Stanje " +
"  FROM         TehnickiPregled left JOIN  " +
                    "    SifRazredNepravilnosti ON TehnickiPregled.RazredNepravilnosti = SifRazredNepravilnosti.ID left JOIN  " +
                   "     SifGrupaNepravilnosti ON TehnickiPregled.GrupaNeispravnosti = SifGrupaNepravilnosti.ID left JOIN  " +
                    "    SifNeispravnostPostupak ON TehnickiPregled.SifraNeispravnosti = SifNeispravnostPostupak.ID " +
                    "    where IDStavke = '" + ID + "'    order by RedniBrojKola ";


            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
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

        private void FillGV2TehnickiPregledSlike(string ID)
        {
            var select = "  SELECT TehnickiPregledSlike.ID       ,TehnickiPregledSlike.TehnickiPregledId      ,TehnickiPregledSlike.Slika  " +
" , TehnickiPregledSlike.[Ime], TehnickiPregled.BrojKola FROM[dbo].[TehnickiPregledSlike] " +
  "          inner join TehnickiPregled on TehnickiPregledSlike.TehnickiPregledId = TehnickiPregled.ID" +
                    "    where TehnickiPregled.IDStavke = '" + ID + "'";


            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
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

        private void FillGV2KomercijalniPregledSlike(string ID)
        {
            var select = "  SELECT KomercijalniPregledSlike.ID       ,KomercijalniPregledSlike.KOmercijalniPregledId      ,KomercijalniPregledSlike.Slika  " +
" , KomercijalniPregledSlike.[Ime], KomercijalniPregled.BrojKola FROM[dbo].[KomercijalniPregledSlike] " +
  "          inner join KomercijalniPregled on KomercijalniPregledSlike.KomercijalniPregledId = KomercijalniPregled.ID" +
                    "    where KomercijalniPregled.IDStavke = '" + ID + "'";


            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
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


        private void FillGV2VucaPregledSlike(string ID)
        {
            var select = "  select VucaSlike.ID, VucaSlike.StavkaAktivnostiID, VucaSlike.Slika,VucaSlike.Ime, VUCA.Lokomotiva  from VucaSlike " +
" inner JOin VUCA on VucaSlike.StavkaAktivnostiId = Vuca.StavkaAktivnostiID" +
                    "    where Vuca.StavkaAktivnostiID = '" + ID + "'";


            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
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

        private void FillGV2KomercijalniPregled(string ID)
        {
            var select = "   SELECT     KomercijalniPregled.ID, KomercijalniPregled.IDStavke, KomercijalniPregled.RedniBrojKola, KomercijalniPregled.BrojKola, KomercijalniPregled.Napomena, " +
                 "     KomercijalniPregled.Stanje, KomercijalniPregled.VrstaNepravilnosti as VNID, SifOpisNeispravnosti.Naziv as VrstaNepravilnosti, KomercijalniPregled.OpisNeispravnosti as ONID, " +
                 "     SifVrstaNepravilnosti.Naziv AS VrstaNepravilnosti, KomercijalniPregled.VrstaKocnice, KomercijalniPregled.Duzina, KomercijalniPregled.Tara, KomercijalniPregled.KocnaMasa, " +
                 "          KomercijalniPregled.RucnaKocnica " +
"     FROM         KomercijalniPregled LEFT OUTER JOIN " +
                  "         SifVrstaNepravilnosti ON KomercijalniPregled.OpisNeispravnosti = SifVrstaNepravilnosti.ID LEFT OUTER JOIN " +
                   "        SifOpisNeispravnosti ON KomercijalniPregled.VrstaNepravilnosti = SifOpisNeispravnosti.ID " +
                    "    where IDStavke = '" + ID + "'    order by RedniBrojKola ";


            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
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

        private void FillDG2Empty()
        {
            var select = "   SELECT  1  ";


            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
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

        private void FillGV2Primopredaja(string ID)
        {

            var select = "  SELECT     LokomotivaPrimopredaja.ID, LokomotivaPrimopredaja.StavkaAktivnostiID, " +
            " LokomotivaPrimopredaja.VrstaPosla, LokomotivaPrimopredaja.VrstaPrimopredaje, " +
            "  LokomotivaPrimopredaja.Kilometraza, LokomotivaPrimopredaja.MotoSati,  LokomotivaPrimopredaja.Napomena, LokomotivaPrimopredaja.Lokomotiva, " +
                   "    LokomotivaPrimopredaja.Dizel, AktivnostiStavke.VrstaAktivnostiID, AktivnostiStavke.Sati, AktivnostiStavke.Posao, AktivnostiStavke.OznakaPosla, AktivnostiStavke.DatumZavrsetka, " +
                  "     AktivnostiStavke.DatumPocetka, Delavci.DeSifra, Delavci.DeIme, DePriimek " +
            " FROM         LokomotivaPrimopredaja INNER JOIN " +
                     "  AktivnostiStavke ON LokomotivaPrimopredaja.StavkaAktivnostiID = AktivnostiStavke.ID " +
                    "   inner join Aktivnosti on Aktivnosti.ID = AktivnostiStavke.IdNadredjena " +
                     "  inner join Delavci on Delavci.DeSifra = Aktivnosti.Zaposleni " +
                   "    where AktivnostiStavke.ID =  '" + ID + "'";

            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
        private void VratiPodatkeAktivnosti(string ID, string VrstaAktivnostiID)
        {

            switch (VrstaAktivnostiID)
            {
                case "57":
                    FillDG2Automobil(ID);
                    break;
                case "58":
                    FillGV2TehnickiPregled(ID);
                    FillGV2TehnickiPregledSlike(ID);

                    break;

                case "59":
                    FillGV2KomercijalniPregled(ID);
                    FillGV2KomercijalniPregledSlike(ID);
                    break;

                case "61":
                    FillGV2VUCA(ID);
                    FillGV2VucaPregledSlike(ID);
                    break;

                case "60":
                    FillGV2Primopredaja(ID);
                    break;

                default:
                    //Console.WriteLine($"Measured value is {measurement}.");
                    FillDG2Empty();
                    break;
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
                        VratiPodatkeAktivnosti(txt_Sifra.Text, row.Cells[2].Value.ToString());
                    }
                }
            }
            catch
            {

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmTehnickiPregled tp = new frmTehnickiPregled(Convert.ToInt32(txt_Sifra.Text));
            tp.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmKomercijalniPregled tp = new frmKomercijalniPregled(Convert.ToInt32(txt_Sifra.Text));
            tp.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            frmVucaPregled vucp = new frmVucaPregled();
            vucp.Show();
        }
        private void Cekirano()
        {
            if (chkAktvni.Checked == true)
            {
                var select3 = " select Oznaka from Najava  where Faktura = '' order by Oznaka";
                var s_connection3 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection myConnection3 = new SqlConnection(s_connection3);
                var c3 = new SqlConnection(s_connection3);
                var dataAdapter3 = new SqlDataAdapter(select3, c3);

                var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
                var ds3 = new DataSet();
                dataAdapter3.Fill(ds3);
                cboPosao.DataSource = ds3.Tables[0];
                cboPosao.DisplayMember = "Oznaka";
                cboPosao.ValueMember = "Oznaka";


            }
            else
            {
                var select3 = " select Oznaka from Najava   order by Oznaka";
                var s_connection3 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection myConnection3 = new SqlConnection(s_connection3);
                var c3 = new SqlConnection(s_connection3);
                var dataAdapter3 = new SqlDataAdapter(select3, c3);

                var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
                var ds3 = new DataSet();
                dataAdapter3.Fill(ds3);
                cboPosao.DataSource = ds3.Tables[0];
                cboPosao.DisplayMember = "Oznaka";
                cboPosao.ValueMember = "Oznaka";
            }
        }

        private void chkAktvni_Click(object sender, EventArgs e)
        {
            Cekirano();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string sifra = VratiPodatkeNajava();
            frmNajava naj = new frmNajava(sifra);
            naj.Show();
        }

        int VratiPodatkeTeretnica()
        {
            // int TeretnicaID = 0;
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" select Teretnica.ID as ID from Teretnica  inner join TrainList on TrainList.ID = Teretnica.TrainListID  where TrainList.KomOznaka = '" + cboPosao.Text + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                return Convert.ToInt32(dr["ID"].ToString());

            }

            con.Close();
            return 0;
        }

        string VratiPodatkeNajava()
        {
            // int TeretnicaID = 0;
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" select Najava.ID as ID from Najava where Najava.Oznaka = '" + cboPosao.Text + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                return dr["ID"].ToString();

            }

            con.Close();
            return "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int terBR = VratiPodatkeTeretnica();
            frmTeretnica ter = new frmTeretnica(terBR.ToString(), "sa");
            ter.Show();


        }

        private void button3_Click(object sender, EventArgs e)
        {

            frmTrainList ter = new frmTrainList(cboPosao.Text, 1);
            ter.Show();
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    if (row.Selected)
                    {
                        txtSifra.Text = row.Cells[1].Value.ToString();
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

        private void button10_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(txtPutanja.Text);
        }
    }
}
