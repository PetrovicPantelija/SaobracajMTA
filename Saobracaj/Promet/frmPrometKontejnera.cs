﻿
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
using System.Net;
using System.Net.Mail;

using Microsoft.Reporting.WinForms;
using Saobracaj.Sifarnici;

namespace TrackModal.Promet
{
    public partial class frmPrometKontejnera : Syncfusion.Windows.Forms.Office2010Form
    {
        string KorisnikCene;
        public static string code = "frmPrometKontejnera";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Saobracaj.Sifarnici.frmLogovanje.user.ToString();
        string niz = "";
        public frmPrometKontejnera()
        {
            InitializeComponent();

        }

         public frmPrometKontejnera(string Korisnik)
        {
            KorisnikCene = Korisnik;
            InitializeComponent();

        }
        
        private void frmLager_Load(object sender, EventArgs e)
        {
          
        }

        private void btnPregled_Click(object sender, EventArgs e)
        {
            var select = "";
            if (frmLogovanje.Firma == "Leget")
            {
                select = " SELECT DISTINCT Promet.[Id], Promet.[DatumTransakcije], Promet.[VrstaDokumenta]   " +
 " ,Promet.[PrStDokumenta],Promet.[PrSifVrstePrometa],Promet.[BrojKontejnera] " +
" ,Promet.[PrPrimKol],Promet.[PrIzdKol] , Skladista.Naziv as Skladiste  , Pozicija.Oznaka, Skladista1.Naziv as SkaldisteIz, pozicija1.Oznaka as LokacijaIz, " +
" Promet.JedinicaMere, Promet.Lot, Promet.NalogID, Promet.MpSifra,NHM.Broj, NHM.Naziv, Promet.Skladisteno, Promet.[PrOznSled]  ,Zatvoren, DatumOtpreme, BrojOtpremnice, Promet.[Datum] ,Promet.[Korisnik]  FROM[dbo].[Promet] inner join Skladista on Promet.SkladisteU = Skladista.ID " +
" inner join Pozicija on Pozicija.ID = Promet.LokacijaU " +
" inner join skladista as skladista1 on skladista1.ID = promet.SkladisteIz " +
" inner join pozicija as pozicija1 on Pozicija1.ID = Promet.LokacijaIz " +
" inner join NHM on Promet.MpSifra = NHM.ID " +
       " where  Promet.BrojKontejnera  = '" + txtKontejner.Text + "'";
            }
            else
            {
                select = " SELECT DISTINCT Promet.[Id], Promet.[DatumTransakcije], Promet.[VrstaDokumenta]   " +
        " ,Promet.[PrStDokumenta],Promet.[PrSifVrstePrometa],Promet.[BrojKontejnera] " +
        " ,Promet.[PrPrimKol],Promet.[PrIzdKol] , Skladista.Naziv as Skladiste  , Pozicija.Oznaka, Skladista1.Naziv as SkaldisteIz, pozicija1.Oznaka as LokacijaIz, " +
        " Promet.[PrOznSled]  ,Zatvoren, DatumOtpreme, BrojOtpremnice, Promet.[Datum] ,Promet.[Korisnik]  FROM [dbo].[Promet] inner join Skladista on Promet.SkladisteU = Skladista.ID " +
        " inner join Pozicija on Pozicija.ID = Promet.LokacijaU " +
        " inner join skladista as skladista1 on skladista1.ID = promet.SkladisteIz " +
        " inner join pozicija as pozicija1 on Pozicija1.ID = Promet.LokacijaIz " +
        " where  Promet.BrojKontejnera  = '" + txtKontejner.Text + "'";
            }
           
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;;
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
            //string value = dataGridView1.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "Šifra";
            dataGridView1.Columns[0].Width = 40;
            // dataGridView1.Columns[0].Visible = false;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Datum Transakcije";
            dataGridView1.Columns[1].Width = 100;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Vrsta Dokumenta";
            dataGridView1.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Br Dokumenta";
            dataGridView1.Columns[3].Width = 50;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Vrsta Prometa";
            dataGridView1.Columns[4].Width = 50;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Broj kontejnera";
            dataGridView1.Columns[5].Width = 100;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "PrPrimKol";
            dataGridView1.Columns[6].Width = 80;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "SkladID ";
            dataGridView1.Columns[7].Width = 80;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Skladiste u";
            dataGridView1.Columns[8].Width = 80;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "LokacID ";
            dataGridView1.Columns[9].Width = 80;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Lokac U";
            dataGridView1.Columns[10].Width = 80;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Oznaka sled";
            dataGridView1.Columns[11].Width = 80;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Datum";
            dataGridView1.Columns[12].Width = 80;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Korisnik";
            dataGridView1.Columns[13].Width = 80;
             * */
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

