﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace TrackModal.Promet
{
    public partial class frmPregledSkladistePrijem : Syncfusion.Windows.Forms.Office2010Form
    {
        string KorisnikCene;
        public frmPregledSkladistePrijem()
        {
            InitializeComponent();
        }

        public frmPregledSkladistePrijem(string Korisnik)
        {
            KorisnikCene = Korisnik;
            InitializeComponent();
        }

        private void frmPregledSkladistePrijem_Load(object sender, EventArgs e)
        {
            var select = " select PrijemKontejneraVoz.ID,  (Cast(PrijemKontejneraVoz.ID as nvarchar(5)) + '-' + Relacija + '-' + Cast(Cast(PrijemKontejneraVoz.VremeDolaska as DateTime) as Nvarchar(12))) as IDVoza from PrijemKontejneraVoz " +
          " inner join Voz on Voz.ID = PrijemKontejneraVoz.IdVoza " +
          " where PrijemKontejneraVoz.StatusPrijema = 1 and PrijemKontejneraVoz.Vozom = 1 order by PrijemKontejneraVoz.VremeDolaska desc";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboPrijemVozom.DataSource = ds.Tables[0];
            cboPrijemVozom.DisplayMember = "IDVoza";
            cboPrijemVozom.ValueMember = "ID";

            var select2 = "select PrijemKontejneraVoz.ID,  (Cast(PrijemKontejneraVoz.ID as nvarchar(15)) + '-' + Cast(PrijemKontejneraVoz.RegBrKamiona as nvarchar(15)) + '-'  + Cast(ImeVozaca as Nvarchar(12))+ '-' + Cast(Cast(PrijemKontejneraVoz.VremeDolaska as DateTime) as Nvarchar(12))) as IDVoza from PrijemKontejneraVoz " +
            " where PrijemKontejneraVoz.StatusPrijema = 1 and Vozom = 0 order by PrijemKontejneraVoz.VremeDolaska desc";
            var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboPrijemKamionom.DataSource = ds2.Tables[0];
            cboPrijemKamionom.DisplayMember = "IDVoza";
            cboPrijemKamionom.ValueMember = "ID";
        }

        private void btnPretrazi_Click(object sender, EventArgs e)
        {
            chkVoz.Checked = true;
            RefreshDataGrid2();
        }
        private void RefreshDataGrid2()
        {
            var select = "SELECT Promet.[Id], Promet.[DatumTransakcije], Promet.[VrstaDokumenta] " +
            " ,Promet.[PrStDokumenta],Promet.[PrSifVrstePrometa],Promet.[BrojKontejnera] " +
            " ,Promet.[PrPrimKol] ,Promet.[SkladisteU], Skladista.Naziv as Skladiste " +
            " ,Promet.[LokacijaU] as LokacijaU,Pozicija.Oznaka ,Promet.[PrOznSled] " +
            " ,PrijemKontejneraVoz.ID as Prijemnica ,PrijemKontejneraVozStavke.ID as PrijemnicaStavka " +
            " FROM [dbo].[Promet] inner join Skladista on Promet.SkladisteU = Skladista.ID " +
            " inner join Pozicija on Promet.LokacijaU = Pozicija.ID " +
            " inner join PrijemKontejneraVozStavke on PrijemKontejneraVozStavke.ID = Promet.PrOznSled" +
            " inner join PrijemKontejneraVoz on PrijemKontejneraVoz.ID = PrijemKontejneraVozStavke.IdNadredjenog" +
            " where PrijemKontejneraVoz.id = " + Convert.ToInt32(cboPrijemVozom.SelectedValue) + " and VrstaDokumenta = 'PRI'";

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = false;
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
            dataGridView2.Columns[0].HeaderText = "Šifra";
            dataGridView2.Columns[0].Width = 80;
            // dataGridView2.Columns[0].Visible = false;

            DataGridViewColumn column2 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "DatumTransakcije";
            dataGridView2.Columns[1].Width = 140;

            DataGridViewColumn column3 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "VrstaDokumenta";
            dataGridView2.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView2.Columns[3];
            dataGridView2.Columns[3].HeaderText = "PrStDokumenta";
            dataGridView2.Columns[3].Width = 50;
            dataGridView2.Columns[3].DefaultCellStyle.BackColor = Color.LightSeaGreen;

            DataGridViewColumn column5 = dataGridView2.Columns[4];
            dataGridView2.Columns[4].HeaderText = "PrSifVrstePrometa";
            dataGridView2.Columns[4].Width = 50;

            DataGridViewColumn column6 = dataGridView2.Columns[5];
            dataGridView2.Columns[5].HeaderText = "Broj kontejnera";
            dataGridView2.Columns[5].Width = 100;
            dataGridView2.Columns[5].DefaultCellStyle.BackColor = Color.LightSeaGreen;

            DataGridViewColumn column7 = dataGridView2.Columns[6];
            dataGridView2.Columns[6].HeaderText = "PrPrimKol";
            dataGridView2.Columns[6].Width = 80;

            DataGridViewColumn column8 = dataGridView2.Columns[7];
            dataGridView2.Columns[7].HeaderText = "SkladID ";
            dataGridView2.Columns[7].Width = 80;

            DataGridViewColumn column9 = dataGridView2.Columns[8];
            dataGridView2.Columns[8].HeaderText = "Skladiste u";
            dataGridView2.Columns[8].Width = 80;

            DataGridViewColumn column10 = dataGridView2.Columns[9];
            dataGridView2.Columns[9].HeaderText = "LokacID ";
            dataGridView2.Columns[9].Width = 80;

            DataGridViewColumn column11 = dataGridView2.Columns[10];
            dataGridView2.Columns[10].HeaderText = "Lokac U";
            dataGridView2.Columns[10].Width = 80;

            DataGridViewColumn column12 = dataGridView2.Columns[11];
            dataGridView2.Columns[11].HeaderText = "Oznaka sled";
            dataGridView2.Columns[11].Width = 80;

            DataGridViewColumn column13 = dataGridView2.Columns[12];
            dataGridView2.Columns[12].HeaderText = "Prijemnica ID";
            dataGridView2.Columns[12].Width = 80;
            dataGridView2.Columns[12].DefaultCellStyle.BackColor = Color.LightSeaGreen;

            DataGridViewColumn column14 = dataGridView2.Columns[13];
            dataGridView2.Columns[13].HeaderText = "Stavka prijemnice ID";
            dataGridView2.Columns[13].Width = 80;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            chkVoz.Checked = false;
            RefreshDataGrid3();
        }

        private void RefreshDataGrid3()
        {
            var select = "SELECT Promet.[Id], Promet.[DatumTransakcije], Promet.[VrstaDokumenta] " +
            " ,Promet.[PrStDokumenta],Promet.[PrSifVrstePrometa],Promet.[BrojKontejnera] " +
            " ,Promet.[PrPrimKol] ,Promet.[SkladisteU], Skladista.Naziv as Skladiste " +
            " ,Promet.[LokacijaU] as LokacijaU,Pozicija.Oznaka ,Promet.[PrOznSled] " +
            " ,PrijemKontejneraVoz.ID as Prijemnica ,PrijemKontejneraVozStavke.ID as PrijemnicaStavka " +
            " FROM [dbo].[Promet] inner join Skladista on Promet.SkladisteU = Skladista.ID " +
            " inner join Pozicija on Promet.LokacijaU = Pozicija.ID " +
            " inner join PrijemKontejneraVozStavke on PrijemKontejneraVozStavke.ID = Promet.PrOznSled" +
            " inner join PrijemKontejneraVoz on PrijemKontejneraVoz.ID = PrijemKontejneraVozStavke.IdNadredjenog" +
            " where PrijemKontejneraVoz.id = " + Convert.ToInt32(cboPrijemKamionom.SelectedValue) + " and VrstaDokumenta = 'PRI'";

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = false;
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
            dataGridView2.Columns[0].HeaderText = "Šifra";
            dataGridView2.Columns[0].Width = 80;
            // dataGridView2.Columns[0].Visible = false;

            DataGridViewColumn column2 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "DatumTransakcije";
            dataGridView2.Columns[1].Width = 140;

            DataGridViewColumn column3 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "VrstaDokumenta";
            dataGridView2.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView2.Columns[3];
            dataGridView2.Columns[3].HeaderText = "PrStDokumenta";
            dataGridView2.Columns[3].Width = 50;
            dataGridView2.Columns[3].DefaultCellStyle.BackColor = Color.LightSeaGreen;

            DataGridViewColumn column5 = dataGridView2.Columns[4];
            dataGridView2.Columns[4].HeaderText = "PrSifVrstePrometa";
            dataGridView2.Columns[4].Width = 50;

            DataGridViewColumn column6 = dataGridView2.Columns[5];
            dataGridView2.Columns[5].HeaderText = "Broj kontejnera";
            dataGridView2.Columns[5].Width = 100;
            dataGridView2.Columns[5].DefaultCellStyle.BackColor = Color.LightSeaGreen;

            DataGridViewColumn column7 = dataGridView2.Columns[6];
            dataGridView2.Columns[6].HeaderText = "PrPrimKol";
            dataGridView2.Columns[6].Width = 80;

            DataGridViewColumn column8 = dataGridView2.Columns[7];
            dataGridView2.Columns[7].HeaderText = "SkladID ";
            dataGridView2.Columns[7].Width = 80;

            DataGridViewColumn column9 = dataGridView2.Columns[8];
            dataGridView2.Columns[8].HeaderText = "Skladiste u";
            dataGridView2.Columns[8].Width = 80;

            DataGridViewColumn column10 = dataGridView2.Columns[9];
            dataGridView2.Columns[9].HeaderText = "LokacID ";
            dataGridView2.Columns[9].Width = 80;

            DataGridViewColumn column11 = dataGridView2.Columns[10];
            dataGridView2.Columns[10].HeaderText = "Lokac U";
            dataGridView2.Columns[10].Width = 80;

            DataGridViewColumn column12 = dataGridView2.Columns[11];
            dataGridView2.Columns[11].HeaderText = "Oznaka sled";
            dataGridView2.Columns[11].Width = 80;

            DataGridViewColumn column13 = dataGridView2.Columns[12];
            dataGridView2.Columns[12].HeaderText = "Prijemnica ID";
            dataGridView2.Columns[12].Width = 80;
            dataGridView2.Columns[12].DefaultCellStyle.BackColor = Color.LightSeaGreen;

            DataGridViewColumn column14 = dataGridView2.Columns[13];
            dataGridView2.Columns[13].HeaderText = "Stavka prijemnice ID";
            dataGridView2.Columns[13].Width = 80;


        }
    }
}
