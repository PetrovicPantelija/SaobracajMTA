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

using Microsoft.Reporting.WinForms;
using System.IO;
using Saobracaj.Sifarnici;

namespace Saobracaj.Dokumenta
{
    public partial class frmVucaPregled : Form
    {
        public string connect = frmLogovanje.connectionString;
        public frmVucaPregled()
        {
            InitializeComponent();
        }

        private void frmVucaPregled_Load(object sender, EventArgs e)
        {
            FillGV();
        }

        private void FillGV()
        {

            var select = " SELECT   top 1000  Vuca.ID, AktivnostiStavke.OznakaPosla, SifVucaStatusi.Naziv, VucaStatusi.Vreme, VucaStatusi.Napomena as NapomenaVucaStatus, " +
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
        " order by Vuca.ID desc";

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
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Posao";
            dataGridView1.Columns[1].Width = 150;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Status";
            dataGridView1.Columns[2].Width = 100;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Vreme";
            dataGridView1.Columns[3].Width = 90;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "NapomenaVucaStatus";
            dataGridView1.Columns[4].Width = 120;

            DataGridViewColumn column5 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Stanica";
            dataGridView1.Columns[5].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "StavkaAktivnostiID";
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[6].Width = 60;

            DataGridViewColumn column7 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Vrsta Posla";
            dataGridView1.Columns[7].Width = 100;

            DataGridViewColumn column8 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Vrsta primopredaje";
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[8].Width = 100;


            DataGridViewColumn column9 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Kilometraža";
            dataGridView1.Columns[9].Width = 100;

            DataGridViewColumn column10 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Moto sati";
            dataGridView1.Columns[10].Width = 100;

            DataGridViewColumn column11 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Nivo goriva";
            dataGridView1.Columns[11].Width = 80;

            DataGridViewColumn column12 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Napomena";
            dataGridView1.Columns[12].Width = 150;

            DataGridViewColumn column13 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Lokomotiva";
            dataGridView1.Columns[13].Width = 100;

            DataGridViewColumn column14 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Dizel";
            dataGridView1.Columns[14].Visible = false;
            dataGridView1.Columns[14].Width = 100;

            DataGridViewColumn column15 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "VrstaAktivnostiID";
            dataGridView1.Columns[15].Visible = false;
            dataGridView1.Columns[15].Width = 100;

            DataGridViewColumn column16 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Sati";
            dataGridView1.Columns[16].Width = 80;


            DataGridViewColumn column17 = dataGridView1.Columns[17];
            dataGridView1.Columns[17].HeaderText = "Posao";
            dataGridView1.Columns[17].Visible = false;
            dataGridView1.Columns[17].Width = 80;

            DataGridViewColumn column18 = dataGridView1.Columns[18];
            dataGridView1.Columns[18].HeaderText = "Datum pocetka";
            dataGridView1.Columns[18].Width = 120;

            DataGridViewColumn column19 = dataGridView1.Columns[19];
            dataGridView1.Columns[19].HeaderText = "Datum zavrsetka";
            dataGridView1.Columns[19].Width = 120;

            DataGridViewColumn column20 = dataGridView1.Columns[20];
            dataGridView1.Columns[20].HeaderText = "DeSifra";
            dataGridView1.Columns[20].Visible = false;
            dataGridView1.Columns[20].Width = 120;

            DataGridViewColumn column21 = dataGridView1.Columns[21];
            dataGridView1.Columns[21].HeaderText = "Ime";
            dataGridView1.Columns[21].Width = 100;

            DataGridViewColumn column22 = dataGridView1.Columns[22];
            dataGridView1.Columns[22].HeaderText = "Prezime";
            dataGridView1.Columns[22].Width = 100;



        }

        private void FillDGAktivni()
        {
            var select = " SELECT     Vuca.ID,  AktivnostiStavke.OznakaPosla, SifVucaStatusi.Naziv, VucaStatusi.Vreme, VucaStatusi.Napomena as NapomenaVucaStatus, " +
            " Stanice.OPis as StanicaStatus,   Vuca.StavkaAktivnostiID, Vuca.VrstaPosla, Vuca.VrstaPrimopredaje, " +
            "  Vuca.Kilometraza, Vuca.MotoSati, Vuca.NivoGoriva, Vuca.Napomena, Vuca.Lokomotiva,  " +
            " Vuca.Dizel, AktivnostiStavke.VrstaAktivnostiID, AktivnostiStavke.Sati, AktivnostiStavke.Posao,  AktivnostiStavke.DatumZavrsetka, " +
            " AktivnostiStavke.DatumPocetka, Delavci.DeSifra, Delavci.DeIme, DePriimek " +
            " FROM         Vuca INNER JOIN " +
            " AktivnostiStavke ON Vuca.StavkaAktivnostiID = AktivnostiStavke.ID " +
            " inner join Aktivnosti on Aktivnosti.ID = AktivnostiStavke.IdNadredjena " +
            " inner join Delavci on Delavci.DeSifra = Aktivnosti.Zaposleni " +
            " inner join VucaStatusi on VucaStatusi.VucaID = VUca.ID " +
            " inner join SifVucaStatusi on VucaStatusi.Status = SifVucaStatusi.ID " +
            " inner join Stanice on VucaStatusi.Stanica = Stanice.ID " +
            " inner join Najava on Najava.ID = AktivnostiStavke.Posao " +
            " WHERE(Najava.Faktura = '') " +
            " order by AktivnostiStavke.Posao, Vuca.ID desc";

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
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Posao";
            dataGridView1.Columns[1].Width = 150;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Status";
            dataGridView1.Columns[2].Width = 100;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Vreme";
            dataGridView1.Columns[3].Width = 90;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "NapomenaVucaStatus";
            dataGridView1.Columns[4].Width = 120;

            DataGridViewColumn column5 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Stanica";
            dataGridView1.Columns[5].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "StavkaAktivnostiID";
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[6].Width = 60;

            DataGridViewColumn column7 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Vrsta Posla";
            dataGridView1.Columns[7].Width = 100;

            DataGridViewColumn column8 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Vrsta primopredaje";
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[8].Width = 100;


            DataGridViewColumn column9 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Kilometraža";
            dataGridView1.Columns[9].Width = 100;

            DataGridViewColumn column10 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Moto sati";
            dataGridView1.Columns[10].Width = 100;

            DataGridViewColumn column11 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Nivo goriva";
            dataGridView1.Columns[11].Width = 80;

            DataGridViewColumn column12 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Napomena";
            dataGridView1.Columns[12].Width = 150;

            DataGridViewColumn column13 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Lokomotiva";
            dataGridView1.Columns[13].Width = 100;
         

            DataGridViewColumn column14 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Dizel";
            dataGridView1.Columns[14].Visible = false;
            dataGridView1.Columns[14].Width = 100;

            DataGridViewColumn column15 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "VrstaAktivnostiID";
            dataGridView1.Columns[15].Visible = false;
            dataGridView1.Columns[15].Width = 100;

            DataGridViewColumn column16 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Sati";
            dataGridView1.Columns[16].Width = 80;


            DataGridViewColumn column17 = dataGridView1.Columns[17];
            dataGridView1.Columns[17].HeaderText = "Posao";
            dataGridView1.Columns[17].Visible = false;
            dataGridView1.Columns[17].Width = 80;

            DataGridViewColumn column18 = dataGridView1.Columns[18];
            dataGridView1.Columns[18].HeaderText = "Datum Zavrsetka";
            dataGridView1.Columns[18].Width = 120;

            DataGridViewColumn column19 = dataGridView1.Columns[19];
            dataGridView1.Columns[19].HeaderText = "Datum pocetka";
            dataGridView1.Columns[19].Width = 120;
         

            DataGridViewColumn column20 = dataGridView1.Columns[20];
            dataGridView1.Columns[20].HeaderText = "DeSifra";
            dataGridView1.Columns[20].Visible = false;
            dataGridView1.Columns[20].Width = 120;

            DataGridViewColumn column21 = dataGridView1.Columns[21];
            dataGridView1.Columns[21].HeaderText = "Ime";
            dataGridView1.Columns[21].Width = 100;

            DataGridViewColumn column22 = dataGridView1.Columns[22];
            dataGridView1.Columns[22].HeaderText = "Prezime";
            dataGridView1.Columns[22].Width = 100;


        }

        private void btn_OtvoriSliku_Click(object sender, EventArgs e)
        {
            FillGV();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillDGAktivni();
        }
    }
}
