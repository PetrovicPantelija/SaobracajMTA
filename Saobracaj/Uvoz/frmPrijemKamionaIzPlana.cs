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
using System.Diagnostics.CodeAnalysis;

namespace Saobracaj.Uvoz
{
    public partial class frmPrijemKamionaIzPlana : Form
    {
        bool status = false;
        string KorisnikCene = "Panta";
        public string connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public frmPrijemKamionaIzPlana()
        {
            InitializeComponent();
        }

        public frmPrijemKamionaIzPlana(string Kontejner)
        {
            InitializeComponent();
            txtKontejnerID.Text = Kontejner;
        }

        private void frmPrijemKamionaIzPlana_Load(object sender, EventArgs e)
        {

        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtSifra.Enabled = false;

            dtpDatumPrijema.Value = DateTime.Now;
            dtpDatumPrijema.Enabled = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (status == true)
            {
                /// ,  string RegBrKamiona,   string ImeVozaca,   int Vozom
                Dokumeta.InsertPrijemKontejneraVoz ins = new Dokumeta.InsertPrijemKontejneraVoz();
                ins.InsertPrijemKontVoz(Convert.ToDateTime(dtpDatumPrijema.Text), Convert.ToInt32(cboStatusPrijema.SelectedIndex), Convert.ToInt32(cboBukingPrijema.SelectedValue), Convert.ToDateTime(dtpVremeDolaska.Value), Convert.ToDateTime(DateTime.Now), KorisnikCene, "", "", 1, txtNapomena.Text, Convert.ToInt32(cboPredefinisanePoruke.SelectedValue), Convert.ToInt32(cboOperater.SelectedValue), 0, 0, 0,0);
                status = false;
                VratiPodatkeMax();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertUvozKonacna ins = new InsertUvozKonacna();
            ins.PrenesiKontejnerIzPlanaNaPrijemnicu(Convert.ToInt32(txtKontejnerID.Text));
            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            //PANTA DATAGRID


            var select =
" SELECT PrijemKontejneraVozStavke.ID, PrijemKontejneraVozStavke.RB, PrijemKontejneraVozStavke.IDNadredjenog,   PrijemKontejneraVozStavke.KontejnerID, " +
" PrijemKontejneraVozStavke.BrojKontejnera,  TipKontenjera.Naziv AS TipKontejnera,PrijemKontejneraVozStavke.BrojVagona,  PrijemKontejneraVozStavke.Granica as GranicaTovarenja, " +
" PrijemKontejneraVozStavke.BrojOsovina,  PrijemKontejneraVozStavke.SopstvenaMasa as TaraVagona,  PrijemKontejneraVozStavke.Tara,  PrijemKontejneraVozStavke.Neto, " +
" PrijemKontejneraVozStavke.BTTORobe, PrijemKontejneraVozStavke.BTTOKontejnera, " +
" Partnerji_4.PANaziv as Brodar, UvozKonacna.BukingBrodara AS BukingBrodar,PrijemKontejneraVozStavke.BrojPlombe, " +
" PrijemKontejneraVozStavke.BrojPlombe2, PrijemKontejneraVozStavke.PeriodSkladistenjaOd, " +
" PrijemKontejneraVozStavke.PeriodSkladistenjaDo, DirigacijaKOntejneraZa.Naziv as DirigacijaKOntejneraZa, VrstePostupakaUvoz.Naziv as PostupakSaRobom," +
" Partnerji_2.PaNaziv AS Nalogodavac_Za_DrumskiPrevoz, Partnerji_3.PaNaziv AS Nalogodavac_Za_Usluge," +
 "  PrijemKontejneraVozStavke.PlaniraniLager as DIREKTNI_INDIREKTNI,    PrijemKontejneraVozStavke.NapomenaS, PrijemKontejneraVozStavke.Napomena2, " +
" PrijemKontejneraVozStavke.Datum, PrijemKontejneraVozStavke.Korisnik " +
" FROM  PrijemKontejneraVozStavke " +
" inner join UvozKonacna on UvozKonacna.ID = PrijemKontejneraVozStavke.KontejnerID " +
" INNER JOIN  Partnerji AS Partnerji_1 ON UvozKonacna.Nalogodavac1 = Partnerji_1.PaSifra " +
" INNER JOIN  Partnerji AS Partnerji_2 ON UvozKonacna.Nalogodavac2 = Partnerji_2.PaSifra " +
" INNER JOIN  Partnerji AS Partnerji_3 ON UvozKonacna.Nalogodavac3 = Partnerji_3.PaSifra " +
" INNER JOIN  Partnerji AS Partnerji_4 ON UvozKonacna.Brodar = Partnerji_4.PaSifra " +
" INNER JOIN TipKontenjera ON PrijemKontejneraVozStavke.TipKontejnera = TipKontenjera.ID " +
" INNER join DirigacijaKontejneraZa on DirigacijaKontejneraZa.ID = PrijemKontejneraVozStavke.StatusKontejnera " +
" INNER JOIN  Voz ON PrijemKontejneraVozStavke.IdVoza = Voz.ID " +
" INNER JOIN VrstePostupakaUvoz ON VrstePostupakaUvoz.id = PrijemKontejneraVozStavke.PostupakSaRobom where IdNadredjenog =  " + txtSifra.Text + " order by RB";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = false;
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

            //Panta refresh


            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[0].Visible = false;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "RB";
            dataGridView1.Columns[1].Width = 30;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "NAdr";
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[2].Width = 30;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "KontID";
            dataGridView1.Columns[3].Width = 40;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Br kont";
            dataGridView1.Columns[4].Width = 110;
            /*
          DataGridViewColumn column6 = dataGridView1.Columns[5];
          dataGridView1.Columns[5].HeaderText = "Granica";
          dataGridView1.Columns[5].Width = 50;

          DataGridViewColumn column7 = dataGridView1.Columns[6];
          dataGridView1.Columns[6].HeaderText = "Br os";
          dataGridView1.Columns[6].Width = 50;

          DataGridViewColumn column8 = dataGridView1.Columns[7];
          dataGridView1.Columns[7].HeaderText = "Sops masa";
          dataGridView1.Columns[7].Width = 50;

          DataGridViewColumn column9 = dataGridView1.Columns[8];
          dataGridView1.Columns[8].HeaderText = "Tara";
          dataGridView1.Columns[8].Width = 70;

          DataGridViewColumn column10 = dataGridView1.Columns[9];
          dataGridView1.Columns[9].HeaderText = "Neto";
          dataGridView1.Columns[9].Width = 70;

          DataGridViewColumn column11 = dataGridView1.Columns[10];
          dataGridView1.Columns[10].HeaderText = "Posiljalac";
          dataGridView1.Columns[10].Width = 50;

          DataGridViewColumn column12 = dataGridView1.Columns[11];
          dataGridView1.Columns[11].HeaderText = "Primalac";
          dataGridView1.Columns[11].Width = 50;

          DataGridViewColumn column13 = dataGridView1.Columns[12];
          dataGridView1.Columns[12].HeaderText = "Vlasnik";
          dataGridView1.Columns[12].Width = 40;

          DataGridViewColumn column14 = dataGridView1.Columns[13];
          dataGridView1.Columns[13].HeaderText = "Tip kontejnera";
          dataGridView1.Columns[13].Width = 70;

          DataGridViewColumn column15 = dataGridView1.Columns[14];
          dataGridView1.Columns[14].HeaderText = "NHM";
          dataGridView1.Columns[14].Width = 40;

          DataGridViewColumn column16 = dataGridView1.Columns[15];
          dataGridView1.Columns[15].HeaderText = "Buking broldar";
          dataGridView1.Columns[15].Width = 70;

          DataGridViewColumn column17 = dataGridView1.Columns[16];
          dataGridView1.Columns[16].HeaderText = "Status Kontejnera";
          dataGridView1.Columns[16].Width = 20;

          DataGridViewColumn column18 = dataGridView1.Columns[17];
          dataGridView1.Columns[17].HeaderText = "Br plombe";
          dataGridView1.Columns[17].Width = 90;

          DataGridViewColumn column19 = dataGridView1.Columns[18];
          dataGridView1.Columns[18].HeaderText = "Br plombe2";
          dataGridView1.Columns[18].Width = 9;

          DataGridViewColumn column20 = dataGridView1.Columns[19];
          dataGridView1.Columns[19].HeaderText = "Plan lager";
          dataGridView1.Columns[19].Width = 30;

          DataGridViewColumn column21 = dataGridView1.Columns[20];
          dataGridView1.Columns[20].HeaderText = "Vreme dolaska";
          dataGridView1.Columns[20].Width = 70;

          DataGridViewColumn column22 = dataGridView1.Columns[21];
          dataGridView1.Columns[21].HeaderText = "Vreme prip";
          dataGridView1.Columns[21].Visible = false;
          dataGridView1.Columns[21].Width = 70;

          DataGridViewColumn column23 = dataGridView1.Columns[22];
          dataGridView1.Columns[22].HeaderText = "Vreme odlaska";
          dataGridView1.Columns[22].Visible = false;
          dataGridView1.Columns[22].Width = 70;

          DataGridViewColumn column24 = dataGridView1.Columns[23];
          dataGridView1.Columns[23].HeaderText = "Datum";
          dataGridView1.Columns[23].Width = 70;

          DataGridViewColumn column25 = dataGridView1.Columns[24];
          dataGridView1.Columns[24].HeaderText = "Korisnik";
          dataGridView1.Columns[24].Width = 70;

          DataGridViewColumn column26 = dataGridView1.Columns[25];
          dataGridView1.Columns[25].HeaderText = "Napomena stav";
          dataGridView1.Columns[25].Width = 70;

          */
        }

        private void VratiPodatkeMax()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Max([ID]) as ID from PrijemKontejneraVoz", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtSifra.Text = dr["ID"].ToString();
            }

            con.Close();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
