﻿using System;
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



namespace TrackModal.Dokumeta
{
    public partial class frmPrijemKontejneraKamionPregled : Syncfusion.Windows.Forms.Office2010Form
    {
        public static string code = "frmPrijemKontejneraKamionPregled";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Saobracaj.Sifarnici.frmLogovanje.user.ToString();
        string niz = "";
        string KorisnikCene;
        string pomModul = "";
        public frmPrijemKontejneraKamionPregled()
        {
            InitializeComponent();


            string Company = Saobracaj.Sifarnici.frmLogovanje.Firma;
            switch (Company)
            {
                case "Leget":
                    {
                        toolStripButton1.Visible = true;
                        toolStripButton5.Visible = true;
                        toolStripButton3.Visible = true;
                        toolStripButton7.Visible = false;
                        panelLeget.Visible = true;

                        break;
                    }
                default:
                    {

                        break;

                    }
                    break;
            }
        }
        public frmPrijemKontejneraKamionPregled(string Korisnik)
        {
            InitializeComponent();


            string Company = Saobracaj.Sifarnici.frmLogovanje.Firma;
            switch (Company)
            {
                case "Leget":
                    {
                        toolStripButton1.Visible = true;
                        toolStripButton5.Visible = true;
                        toolStripButton3.Visible = true;
                        toolStripButton7.Visible = false;
                        panelLeget.Visible = true;
                        break;
                    }
                default:
                    {

                        break;

                    }
                    break;
            }
            KorisnikCene = Korisnik;

        }
        
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void RefreshDataGrid()
        {
            /*
            var select = "SELECT [ID]," +
                 " CONVERT(varchar,DatumPrijema,104)      + ' '      + SUBSTRING(CONVERT(varchar,DatumPrijema,108),1,5) as DatumPrijema, " +
                 " CASE WHEN n1.StatusPrijema = 0 THEN '1-Najava' ELSE '2-Prijem' END as Status, " +
                " REgBrKamiona, ImeVozaca, " +
                " CONVERT(varchar,VremeDolaska,104)      + ' '      + SUBSTRING(CONVERT(varchar,VremeDolaska,108),1,5) as VremeDolaska, " +
               " [Datum] ,[Korisnik]," +
               
" (  SELECT  STUFF((SELECT distinct   '/ ' + Cast(ts.BrojKontejnera as nvarchar(20)) " +
 "  FROM PrijemKontejneraVozStavke ts where n1.ID = ts.IDNadredjenog " +
 " FOR XML PATH('')), 1, 1, ''  ) As Skupljen) " +
 " as Kontejner "+
               " FROM [dbo].[PrijemKontejneraVoz] as n1 where Vozom = 0";
            */

            var select = "SELECT n1.[ID]," +
                " DatumPrijema as DatumPrijema, " +
                " CASE WHEN n1.StatusPrijema = 0 THEN '1-Najava' ELSE '2-Prijem' END as Status, " +
               " REgBrKamiona, ImeVozaca, " +
               " VremeDolaska as VremeDolaska, " +
              " n1.[Datum] ,n1.[Korisnik] , "+

" (  SELECT  STUFF((SELECT distinct   '/ ' + Cast(ts.BrojKontejnera as nvarchar(20)) " +
"  FROM PrijemKontejneraVozStavke ts where n1.ID = ts.IDNadredjenog " +
" FOR XML PATH('')), 1, 1, ''  ) As Skupljen) " +
" as Kontejner , OrganizacioneJedinice.Naziv as Modul,  CASE WHEN n1.Poreklo = 0 THEN 'PLATFORMA' ELSE 'CIRADA' END as POREKLO " +
              " FROM [dbo].[PrijemKontejneraVoz] as n1 " +
              " inner join organizacioneJedinice on OrganizacioneJedinice.ID = n1.Modul " +
              " where Vozom = 0 order by ID desc";



            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "ETA";
            dataGridView1.Columns[1].Width = 100;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Status prijema";
            dataGridView1.Columns[2].Width = 80;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Reg. br. vozila";
            dataGridView1.Columns[3].Width = 150;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Ime vozača";
            dataGridView1.Columns[4].Width = 150;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "ATA";
            dataGridView1.Columns[5].Width = 80;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Datum unosa";
            dataGridView1.Columns[6].Width = 80;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Korisnik";
            dataGridView1.Columns[7].Width = 100;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Kontejneri";
            dataGridView1.Columns[8].Width = 200;


        }

        private void RefreshDataGridNajave()
        {
            var select = "SELECT [ID]," +
                 " CONVERT(varchar,DatumPrijema,104)      + ' '      + SUBSTRING(CONVERT(varchar,DatumPrijema,108),1,5) as DatumPrijema, " +
                 " CASE WHEN n1.StatusPrijema = 0 THEN '1-Najava' ELSE '2-Prijem' END as Status, " +
                " REgBrKamiona, ImeVozaca, " +
                " CONVERT(varchar,VremeDolaska,104)      + ' '      + SUBSTRING(CONVERT(varchar,VremeDolaska,108),1,5) as VremeDolaska, " +
               " [Datum] ,[Korisnik] ," +


" (  SELECT  STUFF((SELECT distinct   '/ ' + Cast(ts.BrojKontejnera as nvarchar(20)) " +
 "  FROM PrijemKontejneraVozStavke ts where n1.ID = ts.IDNadredjenog " +
 " FOR XML PATH('')), 1, 1, ''  ) As Skupljen) " +
 " as Kontejner  " +
               "FROM [dbo].[PrijemKontejneraVoz] as n1 where Vozom = 0 and n1.StatusPrijema = 0 Order by ID Desc";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "ETA";
            dataGridView1.Columns[1].Width = 150;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Status prijema";
            dataGridView1.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Reg. br. vozila";
            dataGridView1.Columns[3].Width = 200;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Ime vozača";
            dataGridView1.Columns[4].Width = 300;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "ATA";
            dataGridView1.Columns[5].Width = 100;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Datum unosa";
            dataGridView1.Columns[6].Width = 100;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Korisnik";
            dataGridView1.Columns[7].Width = 100;


        }

        private void RefreshDataGridLeget()
        {
            string pomPoreklo = "";
            int uslov = 0;

            string pomModul = "";
            int uslovModul = 0;
            if (chkUvoz.Checked == true)
            {
                pomModul = "1";
                uslovModul = 1;

            }
            if (chkIzvoz.Checked == true && uslovModul == 1)
            {
                pomModul = pomModul + ",2";
                uslovModul = 1;
            }
            if (chkIzvoz.Checked == true && uslovModul == 0)
            {
                pomModul = pomModul + "2";
            }
            if (chkTerminal.Checked == true && uslovModul == 1)
            {
                pomModul = pomModul + ",4";
            }
            if (chkTerminal.Checked == true && uslovModul == 0)
            {
                pomModul = pomModul + "4";
            }


            if (chkPlatforma.Checked == true)
            {
                pomPoreklo = "0";
                uslov = 1;

            }
            if (chkCirada.Checked == true && uslov == 1)
            {
                pomPoreklo = pomPoreklo + ",1";
                uslov = 1;
            }
            if (chkCirada.Checked == true && uslov == 0)
            {
                pomPoreklo = pomPoreklo + "1";
            }
            /*
            var select = "SELECT [ID]," +
                 " CONVERT(varchar,DatumPrijema,104)      + ' '      + SUBSTRING(CONVERT(varchar,DatumPrijema,108),1,5) as DatumPrijema, " +
                 " CASE WHEN n1.StatusPrijema = 0 THEN '1-Najava' ELSE '2-Prijem' END as Status, " +
                " REgBrKamiona, ImeVozaca, " +
                " CONVERT(varchar,VremeDolaska,104)      + ' '      + SUBSTRING(CONVERT(varchar,VremeDolaska,108),1,5) as VremeDolaska, " +
               " [Datum] ,[Korisnik]," +
               
" (  SELECT  STUFF((SELECT distinct   '/ ' + Cast(ts.BrojKontejnera as nvarchar(20)) " +
 "  FROM PrijemKontejneraVozStavke ts where n1.ID = ts.IDNadredjenog " +
 " FOR XML PATH('')), 1, 1, ''  ) As Skupljen) " +
 " as Kontejner "+
               " FROM [dbo].[PrijemKontejneraVoz] as n1 where Vozom = 0";
            */

            var select = "SELECT [ID]," +
                " DatumPrijema as DatumPrijema, " +
                " CASE WHEN n1.StatusPrijema = 0 THEN '1-Najava' ELSE '2-Prijem' END as Status, " +
               " REgBrKamiona, ImeVozaca, " +
               " VremeDolaska as VremeDolaska, " +
              " [Datum] ,[Korisnik]," +

" (  SELECT  STUFF((SELECT distinct   '/ ' + Cast(ts.BrojKontejnera as nvarchar(20)) " +
"  FROM PrijemKontejneraVozStavke ts where n1.ID = ts.IDNadredjenog " +
" FOR XML PATH('')), 1, 1, ''  ) As Skupljen) " +
" as Kontejner , Poreklo, Modul" +
              " FROM [dbo].[PrijemKontejneraVoz] as n1 where Vozom = 0 and Modul in ( " + pomModul + ") and Poreklo in ( " + pomPoreklo + ") order by ID desc";



            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "ETA";
            dataGridView1.Columns[1].Width = 100;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Status prijema";
            dataGridView1.Columns[2].Width = 80;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Reg. br. vozila";
            dataGridView1.Columns[3].Width = 150;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Ime vozača";
            dataGridView1.Columns[4].Width = 150;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "ATA";
            dataGridView1.Columns[5].Width = 80;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Datum unosa";
            dataGridView1.Columns[6].Width = 80;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Korisnik";
            dataGridView1.Columns[7].Width = 100;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Kontejneri";
            dataGridView1.Columns[8].Width = 200;


        }
        private void RefreshDataGridNajaveLeget()
        {
            string pomPoreklo = "";
            int uslov = 0;

            string pomModul = "";
            int uslovModul = 0;
            if (chkUvoz.Checked == true)
            {
                pomModul = "1";
                uslovModul = 1;
                
            }
            if (chkIzvoz.Checked == true && uslovModul == 1)
            {
                pomModul = pomModul + ",2";
                uslovModul = 1;
            }
            if (chkIzvoz.Checked == true && uslovModul == 0)
            {
                pomModul = pomModul + "2";
            }
            if (chkTerminal.Checked == true && uslovModul == 1)
            {
                pomModul = pomModul + ",4";
            }
            if (chkTerminal.Checked == true && uslovModul == 0)
            {
                pomModul = pomModul + "4";
            }


            if (chkPlatforma.Checked == true)
            {
                pomPoreklo = "0";
                uslov = 1;

            }
            if (chkCirada.Checked == true && uslov == 1)
            {
                pomPoreklo = pomPoreklo + ",1";
                uslov = 1;
            }
            if (chkCirada.Checked == true && uslov == 0)
            {
                pomPoreklo = pomPoreklo + "1";
            }
            var select = "SELECT [ID]," +
                 " CONVERT(varchar,DatumPrijema,104)      + ' '      + SUBSTRING(CONVERT(varchar,DatumPrijema,108),1,5) as DatumPrijema, " +
                 " CASE WHEN n1.StatusPrijema = 0 THEN '1-Najava' ELSE '2-Prijem' END as Status, " +
                " REgBrKamiona, ImeVozaca, " +
                " CONVERT(varchar,VremeDolaska,104)      + ' '      + SUBSTRING(CONVERT(varchar,VremeDolaska,108),1,5) as VremeDolaska, " +
               " [Datum] ,[Korisnik] ," +


" (  SELECT  STUFF((SELECT distinct   '/ ' + Cast(ts.BrojKontejnera as nvarchar(20)) " +
 "  FROM PrijemKontejneraVozStavke ts where n1.ID = ts.IDNadredjenog " +
 " FOR XML PATH('')), 1, 1, ''  ) As Skupljen) " +
 " as Kontejner, Poreklo, Modul  " +
               "FROM [dbo].[PrijemKontejneraVoz] as n1 where Vozom = 0 and n1.StatusPrijema = 0 and Modul in ( " + pomModul + ") and Poreklo in ( " + pomPoreklo + ") Order by ID Desc";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "ETA";
            dataGridView1.Columns[1].Width = 150;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Status prijema";
            dataGridView1.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Reg. br. vozila";
            dataGridView1.Columns[3].Width = 200;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Ime vozača";
            dataGridView1.Columns[4].Width = 300;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "ATA";
            dataGridView1.Columns[5].Width = 100;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Datum unosa";
            dataGridView1.Columns[6].Width = 100;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Korisnik";
            dataGridView1.Columns[7].Width = 100;


        }

        private void RefreshDataGridPrijemiLeget()
        {

            string pomPoreklo = "";
            int uslov = 0;

            string pomModul = "";
            int uslovModul = 0;
            if (chkUvoz.Checked == true)
            {
                pomModul = "0";
                uslovModul = 1;

            }
            if (chkIzvoz.Checked == true && uslovModul == 1)
            {
                pomModul = pomModul + ",2";
                uslovModul = 1;
            }
            if (chkIzvoz.Checked == true && uslovModul == 0)
            {
                pomModul = pomModul + "2";
            }
            if (chkTerminal.Checked == true && uslovModul == 1)
            {
                pomModul = pomModul + ",4";
            }
            if (chkTerminal.Checked == true && uslovModul == 0)
            {
                pomModul = pomModul + "4";
            }


            if (chkPlatforma.Checked == true)
            {
                pomPoreklo = "0";
                uslov = 1;

            }
            if (chkCirada.Checked == true && uslov == 1)
            {
                pomPoreklo = pomPoreklo + ",1";
                uslov = 1;
            }
            if (chkCirada.Checked == true && uslov == 0)
            {
                pomPoreklo = pomPoreklo + "1";
            }
            var select = "SELECT [ID]," +
                 " CONVERT(varchar,DatumPrijema,104)      + ' '      + SUBSTRING(CONVERT(varchar,DatumPrijema,108),1,5) as DatumPrijema, " +
                 " CASE WHEN n1.StatusPrijema = 0 THEN '1-Najava' ELSE '2-Prijem' END as Status, " +
                " REgBrKamiona, ImeVozaca, " +
                " CONVERT(varchar,VremeDolaska,104)      + ' '      + SUBSTRING(CONVERT(varchar,VremeDolaska,108),1,5) as VremeDolaska, " +
               " [Datum] ,[Korisnik] ," +
" (  SELECT  STUFF((SELECT distinct   '/ ' + Cast(ts.BrojKontejnera as nvarchar(20)) " +
 "  FROM PrijemKontejneraVozStavke ts where n1.ID = ts.IDNadredjenog " +
 " FOR XML PATH('')), 1, 1, ''  ) As Skupljen) " +
 " as Kontejner , Poreklo, Modul" +
               " FROM [dbo].[PrijemKontejneraVoz] as n1 where Vozom = 0 and n1.StatusPrijema = 1 and Modul in ( " + pomModul + ") and Poreklo in ( " + pomPoreklo + ") Order by ID Desc";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "ETA";
            dataGridView1.Columns[1].Width = 150;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Status prijema";
            dataGridView1.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Reg. br. vozila";
            dataGridView1.Columns[3].Width = 200;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Ime vozača";
            dataGridView1.Columns[4].Width = 300;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "ATA";
            dataGridView1.Columns[5].Width = 100;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Datum unosa";
            dataGridView1.Columns[6].Width = 100;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Korisnik";
            dataGridView1.Columns[7].Width = 100;


        }

        private void RefreshDataGridPrijemi()
        {
            var select = "SELECT [ID]," +
                 " CONVERT(varchar,DatumPrijema,104)      + ' '      + SUBSTRING(CONVERT(varchar,DatumPrijema,108),1,5) as DatumPrijema, " +
                 " CASE WHEN n1.StatusPrijema = 0 THEN '1-Najava' ELSE '2-Prijem' END as Status, " +
                " REgBrKamiona, ImeVozaca, " +
                " CONVERT(varchar,VremeDolaska,104)      + ' '      + SUBSTRING(CONVERT(varchar,VremeDolaska,108),1,5) as VremeDolaska, " +
               " [Datum] ,[Korisnik] , Modul, Poreklo," +
" (  SELECT  STUFF((SELECT distinct   '/ ' + Cast(ts.BrojKontejnera as nvarchar(20)) " +
 "  FROM PrijemKontejneraVozStavke ts where n1.ID = ts.IDNadredjenog " +
 " FOR XML PATH('')), 1, 1, ''  ) As Skupljen) " +
 " as Kontejner "  +
               " FROM [dbo].[PrijemKontejneraVoz] as n1 where Vozom = 0 and n1.StatusPrijema = 1 Order by ID Desc";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "ETA";
            dataGridView1.Columns[1].Width = 150;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Status prijema";
            dataGridView1.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Reg. br. vozila";
            dataGridView1.Columns[3].Width = 200;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Ime vozača";
            dataGridView1.Columns[4].Width = 300;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "ATA";
            dataGridView1.Columns[5].Width = 100;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Datum unosa";
            dataGridView1.Columns[6].Width = 100;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Korisnik";
            dataGridView1.Columns[7].Width = 100;


        }

        private void PrijemVozomPregled_Load(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }
        private void ChekurajModulIPoreklo(string Sifra)
        {
            int Modul = 0;
            int Poreklo = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Modul, Poreklo from PrijemKontejneraVoz where ID = " + Sifra, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Modul = Convert.ToInt32(dr["Modul"].ToString());
                Poreklo = Convert.ToInt32(dr["Poreklo"].ToString());
            }
            con.Close();
            if (Poreklo == 1)
            {
                chkCirada.Checked = true;
                chkPlatforma.Checked = false;
            }
            else
            {
                chkCirada.Checked = false;
                chkPlatforma.Checked = true;
            }

            if (Modul == 1)
            {

                chkTerminal.Checked = false;
                chkIzvoz.Checked = false;
                chkUvoz.Checked = true;
            }

            else if (Modul == 2)
            {
                //KADA TERMINAL PRIMA KONTEJNER OD BRODARA
                chkTerminal.Checked = false;
                chkIzvoz.Checked = true;
                chkUvoz.Checked = false;
               
            }
            else
            {
                chkTerminal.Checked = false;
                chkIzvoz.Checked = false;
                chkUvoz.Checked = true;
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

                        string Company = Saobracaj.Sifarnici.frmLogovanje.Firma;
                        switch (Company)
                        {
                            case "Leget":
                                {
                                    ChekurajModulIPoreklo(txtSifra.Text);
                                    return;

                                }
                            default:
                                {

                                    
                                    return;

                                }
                                break;
                        }

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
            string Company = Saobracaj.Sifarnici.frmLogovanje.Firma;
            switch (Company)
            {
                case "Leget":
                    {
                        int Modul = 2;
                        if (chkTerminal.Checked == true)
                        {
                            Modul = 4;
                        }
                        Saobracaj.Dokumenta.frmPrijemKontejneraKamionLegetIzvoz ter2 = new Saobracaj.Dokumenta.frmPrijemKontejneraKamionLegetIzvoz(txtSifra.Text,0, Modul);
                        ter2.Show();
                        return;

                    }
                default:
                    {

                        frmPrijemKontejneraVoz ter3 = new frmPrijemKontejneraVoz(Convert.ToInt32(txtSifra.Text), KorisnikCene, 0);
                        ter3.Show();
                        return;

                    }
                    break;
            }

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            string Company = Saobracaj.Sifarnici.frmLogovanje.Firma;
            switch (Company)
            {
                case "Leget":
                    {
                        RefreshDataGridLeget();
                        return;

                    }
                default:
                    {

                        RefreshDataGrid();
                        return;

                    }
                    break;
            }


           
        }

        private void frmPrijemKontejneraKamionPregled_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {


            string Company = Saobracaj.Sifarnici.frmLogovanje.Firma;
            switch (Company)
            {
                case "Leget":
                    {
                        Saobracaj.Dokumenta.frmPrijemKontejneraKamionLegetUvoz ter2 = new Saobracaj.Dokumenta.frmPrijemKontejneraKamionLegetUvoz(0, KorisnikCene, 0);
                        ter2.Show();
                        return;

                    }
                default:
                    {

                        frmPrijemKontejneraVoz ter3 = new frmPrijemKontejneraVoz(0, KorisnikCene, 0);
                        ter3.Show();
                        return;

                    }
                  
            }
            ///PANTA





        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            string Company = Saobracaj.Sifarnici.frmLogovanje.Firma;
            switch (Company)
            {
                case "Leget":
                    {
                        RefreshDataGridPrijemiLeget();
                        return;

                    }
                default:
                    {
                        RefreshDataGridPrijemi();
                        return;

                    }
                    break;
            }

           
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            string Company = Saobracaj.Sifarnici.frmLogovanje.Firma;
            switch (Company)
            {
                case "Leget":
                    {
                        RefreshDataGridNajaveLeget();
                        return;

                    }
                default:
                    {
                        RefreshDataGridNajave();
                        return;

                    }
                    break;
            }


            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshDataGridPoREgBr();
        }

        private void RefreshDataGridPoREgBr()
        {
            var select = "SELECT [ID]," +
                 " CONVERT(varchar,DatumPrijema,104)      + ' '      + SUBSTRING(CONVERT(varchar,DatumPrijema,108),1,5) as DatumPrijema, " +
                 " CASE WHEN n1.StatusPrijema = 0 THEN '1-Najava' ELSE '2-Prijem' END as Status, " +
                " REgBrKamiona, ImeVozaca, " +
                " CONVERT(varchar,VremeDolaska,104)      + ' '      + SUBSTRING(CONVERT(varchar,VremeDolaska,108),1,5) as VremeDolaska, " +
               " [Datum] ,[Korisnik]," +

" (  SELECT  STUFF((SELECT distinct   '/ ' + Cast(ts.BrojKontejnera as nvarchar(20)) " +
 "  FROM PrijemKontejneraVozStavke ts where n1.ID = ts.IDNadredjenog " +
 " FOR XML PATH('')), 1, 1, ''  ) As Skupljen) " +
 " as Kontejner " +
               " FROM [dbo].[PrijemKontejneraVoz] as n1 where Vozom = 0 and n1.REgBrKamiona = '" + txtRegBrKamiona.Text + "' Order by ID Desc";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "ETA";
            dataGridView1.Columns[1].Width = 100;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Status prijema";
            dataGridView1.Columns[2].Width = 80;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Reg. br. vozila";
            dataGridView1.Columns[3].Width = 150;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Ime vozača";
            dataGridView1.Columns[4].Width = 150;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "ATA";
            dataGridView1.Columns[5].Width = 80;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Datum unosa";
            dataGridView1.Columns[6].Width = 80;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Korisnik";
            dataGridView1.Columns[7].Width = 100;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Kontejneri";
            dataGridView1.Columns[8].Width = 200;


        }

        private void RefreshDataGridPoKontejneru()
        {
            var select = "SELECT n1.[ID], CONVERT(varchar,DatumPrijema,104)      + ' '      + " +
" SUBSTRING(CONVERT(varchar, DatumPrijema, 108), 1, 5) as DatumPrijema,  CASE WHEN n1.StatusPrijema = 0 " +
" THEN '1-Najava' ELSE '2-Prijem' END as Status,  REgBrKamiona, ImeVozaca,  CONVERT(varchar, n1.VremeDolaska, 104) " +
" + ' ' + SUBSTRING(CONVERT(varchar, n1.VremeDolaska, 108), 1, 5) as VremeDolaska,  n1.[Datum] ,n1.[Korisnik], " +
"  (SELECT  STUFF((SELECT distinct   '/ ' + Cast(ts.BrojKontejnera as nvarchar(20)) " +
 " FROM PrijemKontejneraVozStavke ts where n1.ID = ts.IDNadredjenog  FOR XML PATH('')), 1, 1, ''  ) As Skupljen) " +
  "    as Kontejner " +
  "  FROM[dbo].[PrijemKontejneraVoz] as n1 inner join PrijemKontejneraVozStavke as vs on n1.ID = vs.IDNadredjenog" +
  " where Vozom = 0 and vs.BrojKontejnera = '" + txtBrojKontejnera.Text + "'";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "ETA";
            dataGridView1.Columns[1].Width = 100;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Status prijema";
            dataGridView1.Columns[2].Width = 80;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Reg. br. vozila";
            dataGridView1.Columns[3].Width = 150;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Ime vozača";
            dataGridView1.Columns[4].Width = 150;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "ATA";
            dataGridView1.Columns[5].Width = 80;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Datum unosa";
            dataGridView1.Columns[6].Width = 80;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Korisnik";
            dataGridView1.Columns[7].Width = 100;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Kontejneri";
            dataGridView1.Columns[8].Width = 200;


        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshDataGridPoKontejneru();
        }

        private void RefreshDataGridPoBukinguBrodara()
        {
            var select = "SELECT n1.[ID], CONVERT(varchar,DatumPrijema,104)      + ' '      + " +
" SUBSTRING(CONVERT(varchar, DatumPrijema, 108), 1, 5) as DatumPrijema,  CASE WHEN n1.StatusPrijema = 0 " +
" THEN '1-Najava' ELSE '2-Prijem' END as Status,  REgBrKamiona, ImeVozaca,  CONVERT(varchar, n1.VremeDolaska, 104) " +
" + ' ' + SUBSTRING(CONVERT(varchar, n1.VremeDolaska, 108), 1, 5) as VremeDolaska,  n1.[Datum] ,n1.[Korisnik], " +
"  (SELECT  STUFF((SELECT distinct   '/ ' + Cast(ts.BrojKontejnera as nvarchar(20)) " +
 " FROM PrijemKontejneraVozStavke ts where n1.ID = ts.IDNadredjenog  FOR XML PATH('')), 1, 1, ''  ) As Skupljen) " +
  "    as Kontejner " +
  "  FROM[dbo].[PrijemKontejneraVoz] as n1 inner join PrijemKontejneraVozStavke as vs on n1.ID = vs.IDNadredjenog" +
  " where Vozom = 0 and vs.BukingBrodar = '" + txtBukingBrodar.Text + "'";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "ETA";
            dataGridView1.Columns[1].Width = 100;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Status prijema";
            dataGridView1.Columns[2].Width = 80;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Reg. br. vozila";
            dataGridView1.Columns[3].Width = 150;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Ime vozača";
            dataGridView1.Columns[4].Width = 150;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "ATA";
            dataGridView1.Columns[5].Width = 80;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Datum unosa";
            dataGridView1.Columns[6].Width = 80;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Korisnik";
            dataGridView1.Columns[7].Width = 100;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Kontejneri";
            dataGridView1.Columns[8].Width = 200;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefreshDataGridPoBukinguBrodara();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {

            string Company = Saobracaj.Sifarnici.frmLogovanje.Firma;
            switch (Company)
            {
                case "Leget":
                    {
                        Saobracaj.Dokumenta.frmPrijemKontejneraKamionLegetUvoz ter2 = new Saobracaj.Dokumenta.frmPrijemKontejneraKamionLegetUvoz(Convert.ToInt32(txtSifra.Text), KorisnikCene, 0);
                        ter2.Show();
                        return;

                    }
                default:
                    {

                        frmPrijemKontejneraVoz ter3 = new frmPrijemKontejneraVoz(Convert.ToInt32(txtSifra.Text), KorisnikCene, 0);
                        ter3.Show();
                        return;

                    }
                    break;
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            frmPrijemKontejneraVoz ter3 = new frmPrijemKontejneraVoz(Convert.ToInt32(txtSifra.Text), KorisnikCene, 0);
            ter3.Show();
        }
    }
}

