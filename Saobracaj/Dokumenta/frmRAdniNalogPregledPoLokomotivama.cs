﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Dokumenta
{
    public partial class frmRAdniNalogPregledPoLokomotivama : Form
    {
        public static string code = "frmRAdniNalogPregledPoLokomotivama";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Sifarnici.frmLogovanje.user.ToString();
        string niz = "";
        public frmRAdniNalogPregledPoLokomotivama()
        {
            InitializeComponent();

        }
        private void RefreshDataGrid1()
        {
            string pom = "'1'";
            var select = " SELECT  d1.IDRadnogNaloga, RNT.SMSifra,d1.RB, d1.IDTrase,  Trase.Voz,  RN.StatusRN,   " +
 " CASE WHEN d1.Rezi > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Rezi, " +
 " stanice_2.Opis as StanicaOd, stanice_3.Opis as StanicaDo, " +
 "  (Cast(Zaposleni.DeSifra as nvarchar(3)) + '--' + Rtrim(Zaposleni.DeIme) + ' ' + Rtrim(Zaposleni.DePriimek)) as Planer,  " +
 "    (SELECT  STUFF((SELECT distinct   '/' + (Cast(del.DeSifra as nvarchar(3)) " +
 "    + '--' + Rtrim(del.DeIme) + ' ' + Rtrim(del.DePriimek)) " +
  "     from RadniNalogTraseLokZap inner Join Delavci del " +
  "     on(RadniNalogTraseLokZap.DeSifra = del.DeSifra) " +
  "     where RadniNalogTraseLokZap.IDRadnogNaloga = d1.IDRadnogNaloga " +
  "     and  RadniNalogTraseLokZap.IdTrase = d1.IDTrase " +
  "      FOR XML PATH('')), 1, 1, ''  ) As Skupljen2)    as Zaposleni2,  " +
  "       d1.DatumPolaska ,d1.DatumDolaska ,  d1.Vreme ,d1.DatumPolaskaReal , " +
  "        d1.DatumDolaskaReal ,d1.VremeReal ,  " +
  "        stanice.Opis AS TRPocetna ,stanice_1.Opis AS TRKrajnja,  Trase.Relacija " +
  "         FROM RadniNalogTrase d1 INNER JOIN  Trase ON d1.IDTrase = Trase.ID " +
  "          INNER JOIN  stanice ON Trase.Pocetna = stanice.ID " +
  "          INNER JOIN  stanice AS stanice_2 ON d1.StanicaOd = stanice_2.ID " +
  "           INNER JOIN  stanice AS stanice_3 ON d1.StanicaDo = stanice_3.ID " +
  "           INNER JOIN  stanice AS stanice_1 ON Trase.Krajnja = stanice_1.ID " +
  "            inner Join RadniNalog as RN ON d1.IDRadnogNaloga = RN.ID " +
  "             inner Join Delavci as Zaposleni ON RN.Planer = Zaposleni.DeSifra " +
  "             inner join RadniNalogLokNaTrasi as RNT on RNT.IDRadnogNaloga = RN.ID ";


            if (chkLA.Checked == true)
            {
                pom = pom + ",'LA'";
            }
            if (chkOD.Checked == true)
            {
                pom = pom + ",'OD'";
            }

            if (chkPL.Checked == true)
            {
                pom = pom + ",'PL'";
            }

            if (chkPR.Checked == true)
            {
                pom = pom + ",'PR'";
            }

            if (chkST.Checked == true)
            {
                pom = pom + ",'ST'";
            }
            if (chkZA.Checked == true)
            {
                pom = pom + ",'ZA'";
            }

            select = select + "where RN.StatusRN in ( " + pom + ")" + "  order by RNT.SMSifra "; ;


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
            /*
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "RN";
            dataGridView1.Columns[0].Width = 30;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "RB";
            dataGridView1.Columns[1].Width = 30;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].Visible = false;
            //  dataGridView1.Columns[2].HeaderText = "ID Trase";
            //  dataGridView1.Columns[2].Width = 30;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Trase";
            dataGridView1.Columns[3].Width = 50;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "St";
            dataGridView1.Columns[4].Width = 30;

            //Rezi

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Rezi";
            dataGridView1.Columns[5].Width = 30;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Stanica od";
            dataGridView1.Columns[6].Width = 90;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Stanica do";
            dataGridView1.Columns[7].Width = 90;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Planer";
            dataGridView1.Columns[8].Width = 80;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Lokomotive";
            dataGridView1.Columns[9].Width = 120;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Osoblje";
            dataGridView1.Columns[10].Width = 220;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Pl polazak";
            dataGridView1.Columns[11].Width = 90;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Pl Dolazak";
            dataGridView1.Columns[12].Width = 90;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Pl vreme";
            dataGridView1.Columns[13].Width = 40;

            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Rel. polazak";
            dataGridView1.Columns[14].Width = 90;

            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Rel dolazak";
            dataGridView1.Columns[15].Width = 90;

            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Real vreme";
            dataGridView1.Columns[16].Width = 40;

            DataGridViewColumn column18 = dataGridView1.Columns[17];
            dataGridView1.Columns[17].HeaderText = "Tr Stanica od";
            dataGridView1.Columns[17].Width = 90;

            DataGridViewColumn column19 = dataGridView1.Columns[18];
            dataGridView1.Columns[18].HeaderText = "Tr Stanica do";
            dataGridView1.Columns[18].Width = 90;

            DataGridViewColumn column20 = dataGridView1.Columns[19];
            dataGridView1.Columns[19].HeaderText = "Trasa relacija";
            dataGridView1.Columns[19].Width = 150;
            */
        }


        private void RefreshDataGrid2()
        {
            string pom = "'1'";
            var select = " SELECT  d1.IDRadnogNaloga, RNT.SMSifra,d1.RB, d1.IDTrase,  Trase.Voz,  RN.StatusRN,   " +
 " CASE WHEN d1.Rezi > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Rezi, " +
 " stanice_2.Opis as StanicaOd, stanice_3.Opis as StanicaDo, " +
 "  (Cast(Zaposleni.DeSifra as nvarchar(3)) + '--' + Rtrim(Zaposleni.DeIme) + ' ' + Rtrim(Zaposleni.DePriimek)) as Planer,  " +
 "    (SELECT  STUFF((SELECT distinct   '/' + (Cast(del.DeSifra as nvarchar(3)) " +
 "    + '--' + Rtrim(del.DeIme) + ' ' + Rtrim(del.DePriimek)) " +
  "     from RadniNalogTraseLokZap inner Join Delavci del " +
  "     on(RadniNalogTraseLokZap.DeSifra = del.DeSifra) " +
  "     where RadniNalogTraseLokZap.IDRadnogNaloga = d1.IDRadnogNaloga " +
  "     and  RadniNalogTraseLokZap.IdTrase = d1.IDTrase " +
  "      FOR XML PATH('')), 1, 1, ''  ) As Skupljen2)    as Zaposleni2,  " +
  "       d1.DatumPolaska ,d1.DatumDolaska ,  d1.Vreme ,d1.DatumPolaskaReal , " +
  "        d1.DatumDolaskaReal ,d1.VremeReal ,  " +
  "        stanice.Opis AS TRPocetna ,stanice_1.Opis AS TRKrajnja,  Trase.Relacija " +
  "         FROM RadniNalogTrase d1 INNER JOIN  Trase ON d1.IDTrase = Trase.ID " +
  "          INNER JOIN  stanice ON Trase.Pocetna = stanice.ID " +
  "          INNER JOIN  stanice AS stanice_2 ON d1.StanicaOd = stanice_2.ID " +
  "           INNER JOIN  stanice AS stanice_3 ON d1.StanicaDo = stanice_3.ID " +
  "           INNER JOIN  stanice AS stanice_1 ON Trase.Krajnja = stanice_1.ID " +
  "            inner Join RadniNalog as RN ON d1.IDRadnogNaloga = RN.ID " +
  "             inner Join Delavci as Zaposleni ON RN.Planer = Zaposleni.DeSifra " +
  "             inner join RadniNalogLokNaTrasi as RNT on RNT.IDRadnogNaloga = RN.ID ";


            if (chkLA.Checked == true)
            {
                pom = pom + ",'LA'";
            }
            if (chkOD.Checked == true)
            {
                pom = pom + ",'OD'";
            }

            if (chkPL.Checked == true)
            {
                pom = pom + ",'PL'";
            }

            if (chkPR.Checked == true)
            {
                pom = pom + ",'PR'";
            }

            if (chkST.Checked == true)
            {
                pom = pom + ",'ST'";
            }
            if (chkZA.Checked == true)
            {
                pom = pom + ",'ZA'";
            }

            select = select + "where RNT.SMSifra = '" + cboLokomotiva.SelectedValue + "' and RN.StatusRN in ( " + pom + ")" + "  order by RNT.SMSifra "; ;


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
            /*
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "RN";
            dataGridView1.Columns[0].Width = 30;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "RB";
            dataGridView1.Columns[1].Width = 30;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].Visible = false;
            //  dataGridView1.Columns[2].HeaderText = "ID Trase";
            //  dataGridView1.Columns[2].Width = 30;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Trase";
            dataGridView1.Columns[3].Width = 50;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "St";
            dataGridView1.Columns[4].Width = 30;

            //Rezi

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Rezi";
            dataGridView1.Columns[5].Width = 30;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Stanica od";
            dataGridView1.Columns[6].Width = 90;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Stanica do";
            dataGridView1.Columns[7].Width = 90;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Planer";
            dataGridView1.Columns[8].Width = 80;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Lokomotive";
            dataGridView1.Columns[9].Width = 120;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Osoblje";
            dataGridView1.Columns[10].Width = 220;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Pl polazak";
            dataGridView1.Columns[11].Width = 90;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Pl Dolazak";
            dataGridView1.Columns[12].Width = 90;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Pl vreme";
            dataGridView1.Columns[13].Width = 40;

            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Rel. polazak";
            dataGridView1.Columns[14].Width = 90;

            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Rel dolazak";
            dataGridView1.Columns[15].Width = 90;

            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Real vreme";
            dataGridView1.Columns[16].Width = 40;

            DataGridViewColumn column18 = dataGridView1.Columns[17];
            dataGridView1.Columns[17].HeaderText = "Tr Stanica od";
            dataGridView1.Columns[17].Width = 90;

            DataGridViewColumn column19 = dataGridView1.Columns[18];
            dataGridView1.Columns[18].HeaderText = "Tr Stanica do";
            dataGridView1.Columns[18].Width = 90;

            DataGridViewColumn column20 = dataGridView1.Columns[19];
            dataGridView1.Columns[19].HeaderText = "Trasa relacija";
            dataGridView1.Columns[19].Width = 150;
            */
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // RefreshDataGrid1();
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
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            RefreshDataGrid1();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmRadniNalog ter = new frmRadniNalog(txtSifra.Text);
            ter.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            frmRadniNalog ter = new frmRadniNalog();
            ter.Show();
        }

        private void frmRAdniNalogPregledPoLokomotivama_Load(object sender, EventArgs e)
        {
            var select2 = " Select SmSifra, SmSifra as Opis from Mesta where Lokomotiva=1";
            var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboLokomotiva.DataSource = ds2.Tables[0];
            cboLokomotiva.DisplayMember = "Opis";
            cboLokomotiva.ValueMember = "SmSifra";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid2();
        }

        private void cboLokomotiva_SelectedValueChanged(object sender, EventArgs e)
        {

        }
    }
}
