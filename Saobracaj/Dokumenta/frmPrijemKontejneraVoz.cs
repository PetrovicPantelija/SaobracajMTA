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
using System.Net;
using System.Net.Mail;
using System.Diagnostics.CodeAnalysis;
using Saobracaj;
using Saobracaj.Sifarnici;
using Saobracaj.Dokumeta;
using Saobracaj.Dokumenta;

namespace TrackModal.Dokumeta
{
    public partial class frmPrijemKontejneraVoz : Syncfusion.Windows.Forms.Office2010Form
    {

        MailMessage mailMessage;
        string KorisnikCene;
        bool status = false;
        int usao = 0;
        string connect = frmLogovanje.connectionString;

        public frmPrijemKontejneraVoz()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
        }

        public frmPrijemKontejneraVoz(string Korisnik, int Vozom)
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            KorisnikCene = Korisnik;
            if (Vozom == 1)
            {
                chkVoz.Checked = true;
                txtImeVozaca.Enabled = false;
                txtRegBrKamiona.Enabled = false;
                dtpDatumPrijema.Value = DateTime.Now;
                dtpVremeDolaska.Value = DateTime.Now;
              //  dtpVremeOdlaska.Value = DateTime.Now;
              //  dtpVremePripremljen.Value = DateTime.Now;
                this.Text = "Prijem kontejnera vozom";
                dtpVremePripremljen.Enabled = false;
                dtpVremeOdlaska.Enabled = false;
            }
            else
            {
                chkVoz.Checked = false;
                cboBukingPrijema.Enabled = false;
                dtpDatumPrijema.Value = DateTime.Now;
                dtpVremeDolaska.Value = DateTime.Now;
              //  dtpVremeOdlaska.Value = DateTime.Now;
               // dtpVremePripremljen.Value = DateTime.Now;
                this.Text = "Prijem kontejnera Kamionom";
                txtVagon.Enabled = false;
                txtGranica.Enabled = false;
                dtpVremePripremljen.Enabled = false;
                dtpVremeOdlaska.Enabled = false;
                // toolStripButton6.Visible = false;
                chkVoz.Visible = false;
                label15.Visible = false;
                cboBukingPrijema.Visible = false;
                label4.Visible = false;
                txtVagon.Visible = false;
                button6.Visible = false;
                label22.Visible = false;
                txtGranica.Visible = false;
                label24.Visible = false;
                txtBrojOsovina.Visible = false;
                label23.Visible = false;
                txtSopstvenaMasa.Visible = false;
            }
            FillCombo();
        }

        public frmPrijemKontejneraVoz(int sifra, string Korisnik, int Vozom)
        {
            InitializeComponent();
            KorisnikCene = Korisnik;
            if (Vozom == 1)
            {
                chkVoz.Checked = true;
                txtImeVozaca.Enabled = false;
                txtRegBrKamiona.Enabled = false;
                this.Text = "Prijem kontejnera vozom";
                dtpVremePripremljen.Enabled = false;
                dtpVremeOdlaska.Enabled = false;
            }
            else
            {
                chkVoz.Checked = false;
                cboBukingPrijema.Enabled = false;
                this.Text = "Prijem kontejnera kamionom";
                txtVagon.Enabled = false;
                txtGranica.Enabled = false;
                dtpVremePripremljen.Enabled = false;
                dtpVremeOdlaska.Enabled = false;

                chkVoz.Visible = false;
                label15.Visible = false;
                cboBukingPrijema.Visible = false;
                label4.Visible = false;
                txtVagon.Visible = false;
                button6.Visible = false;
                label22.Visible = false;
                txtGranica.Visible = false;
                label24.Visible = false;
                txtBrojOsovina.Visible = false;
                label23.Visible = false;
                txtSopstvenaMasa.Visible = false;
                //  toolStripButton6.Visible = false;
            }
            txtSifra.Text = sifra.ToString();
            FillCombo();
            VratiPodatke(sifra);
            RefreshDataGrid();
        }

        private void FillCombo()
        {
            var select5 = " Select Distinct ID, (Cast(BrVoza as nvarchar(6)) + '-' + Relacija + '-' + Cast(Cast(VremePolaskaO as DateTime) as Nvarchar(12))) as IDVoza From Voz where dolazeci = 0";
            var s_connection5 = frmLogovanje.connectionString;
            SqlConnection myConnection5 = new SqlConnection(s_connection5);
            var c5 = new SqlConnection(s_connection5);
            var dataAdapter5 = new SqlDataAdapter(select5, c5);

            var commandBuilder5 = new SqlCommandBuilder(dataAdapter5);
            var ds5 = new DataSet();
            dataAdapter5.Fill(ds5);
            cboBukingOtpreme.DataSource = ds5.Tables[0];
            cboBukingOtpreme.DisplayMember = "IdVoza";
            cboBukingOtpreme.ValueMember = "ID";

            var select6 = " Select Distinct ID, (Cast(id as nvarchar(3)) + '-' + Naziv) as Naziv From StatusRobe Order by Naziv ";
            var s_connection6 = frmLogovanje.connectionString;
            SqlConnection myConnection6 = new SqlConnection(s_connection6);
            var c6 = new SqlConnection(s_connection6);
            var dataAdapter6 = new SqlDataAdapter(select6, c6);

            var commandBuilder6 = new SqlCommandBuilder(dataAdapter6);
            var ds6 = new DataSet();
            dataAdapter6.Fill(ds6);
            cboStatusKontejnera.DataSource = ds6.Tables[0];
            cboStatusKontejnera.DisplayMember = "Naziv";
            cboStatusKontejnera.ValueMember = "ID";
            /*
            var select7 = " Select Distinct ID, IdVoza From BukingVoza ";
            var s_connection7 = frmLogovanje.connectionString;
            SqlConnection myConnection7 = new SqlConnection(s_connection7);
            var c7 = new SqlConnection(s_connection7);
            var dataAdapter7 = new SqlDataAdapter(select7, c7);

            var commandBuilder7 = new SqlCommandBuilder(dataAdapter7);
            var ds7 = new DataSet();
            dataAdapter7.Fill(ds7);
            cboBukingOtpreme.DataSource = ds7.Tables[0];
            cboBukingOtpreme.DisplayMember = "IdVoza";
            cboBukingOtpreme.ValueMember = "ID";
            */
            var select8 = "  Select Distinct ID, (Cast(BrVoza as nvarchar(6)) + '-' + Relacija) as IdVoza   From Voz ";
            var s_connection8 = frmLogovanje.connectionString;
            SqlConnection myConnection8 = new SqlConnection(s_connection8);
            var c8 = new SqlConnection(s_connection8);
            var dataAdapter8 = new SqlDataAdapter(select8, c8);

            var commandBuilder8 = new SqlCommandBuilder(dataAdapter8);
            var ds8 = new DataSet();
            dataAdapter8.Fill(ds8);
            cboBukingPrijema.DataSource = ds8.Tables[0];
            cboBukingPrijema.DisplayMember = "IdVoza";
            cboBukingPrijema.ValueMember = "ID";
            //where Organizator = 1



            var select10 = " Select Distinct ID, Naziv  From PredefinisanePoruke order by ID";
            var s_connection10 = frmLogovanje.connectionString;
            SqlConnection myConnection10 = new SqlConnection(s_connection10);
            var c10 = new SqlConnection(s_connection10);
            var dataAdapter10 = new SqlDataAdapter(select10, c10);

            var commandBuilder10 = new SqlCommandBuilder(dataAdapter10);
            var ds10 = new DataSet();
            dataAdapter10.Fill(ds10);
            cboPredefinisanePoruke.DataSource = ds10.Tables[0];
            cboPredefinisanePoruke.DisplayMember = "Naziv";
            cboPredefinisanePoruke.ValueMember = "ID";

            usao = 1;

            var select11 = "select ID,RTrim(Naziv) as Naziv from VrstePostupakaUvoz order by Naziv";
            SqlConnection conn = new SqlConnection(connect);
            var da11 = new SqlDataAdapter(select11, conn);
            var ds11 = new DataSet();
            da11.Fill(ds11);
            cboPostupak.DataSource = ds11.Tables[0];
            cboPostupak.DisplayMember = "Naziv";
            cboPostupak.ValueMember = "ID";


        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtSifra.Text = "0";
            RefreshDataGrid();
            txtSifra.Text = "";
            txtSifra.Enabled = false;
            dtpVremeOdlaska.Value = DateTime.Now;
            dtpDatumPrijema.Value = DateTime.Now;
            dtpDatumPrijema.Enabled = true;
            if (chkVoz.Checked == true)
            {
                cboStatusKontejnera.SelectedValue = 1;


            }
            else
            {
                cboStatusKontejnera.SelectedValue = 2;
            }
           

        }

        private void VratiPodatkeMax()
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;;
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

        private void tsSave_Click(object sender, EventArgs e)
        {
            string sp = cboStatusPrijema.Text;
            int ini = cboStatusPrijema.SelectedIndex;
            int ini2 = Convert.ToInt32(cboStatusPrijema.SelectedValue);
            if (ini2 == 1)
            {
                dtpVremeDolaska.Value = DateTime.Now;
            }
            if (chkVoz.Checked == true)
            {
                if (status == true)
                {
                    /// ,  string RegBrKamiona,   string ImeVozaca,   int Vozom
                    Saobracaj.Dokumeta.InsertPrijemKontejneraVoz ins = new Saobracaj.Dokumeta.InsertPrijemKontejneraVoz();
                    ins.InsertPrijemKontVoz(Convert.ToDateTime(dtpDatumPrijema.Text), Convert.ToInt32(cboStatusPrijema.SelectedIndex), Convert.ToInt32(cboBukingPrijema.SelectedValue), Convert.ToDateTime(dtpVremeDolaska.Value), Convert.ToDateTime(DateTime.Now), KorisnikCene, "", "", 1, txtNapomena.Text, Convert.ToInt32(cboPredefinisanePoruke.SelectedValue),0,0,0,0,0);
                    status = false;
                    VratiPodatkeMax();
                }
                else
                {
                    //int TipCenovnika ,int Komitent, double Cena , int VrstaManipulacije ,DateTime  Datum , string Korisnik
                    Saobracaj.Dokumeta.InsertPrijemKontejneraVoz upd = new Saobracaj.Dokumeta.InsertPrijemKontejneraVoz();
                    upd.UpdPrijemKontejneraVoz(Convert.ToInt32(txtSifra.Text), Convert.ToDateTime(dtpDatumPrijema.Text), Convert.ToInt32(cboStatusPrijema.SelectedIndex), Convert.ToInt32(cboBukingPrijema.SelectedValue), Convert.ToDateTime(dtpVremeDolaska.Value), Convert.ToDateTime(DateTime.Now), KorisnikCene, "", "", 1, txtNapomena.Text,  Convert.ToInt32(cboPredefinisanePoruke.SelectedValue), 0, 0, 0, 0, 0);
                    status = false;
                }
            }
            else
            {
                if (status == true)
                {
                    /// ,  string RegBrKamiona,   string ImeVozaca,   int Vozom
                    Saobracaj.Dokumeta.InsertPrijemKontejneraVoz ins = new Saobracaj.Dokumeta.InsertPrijemKontejneraVoz();
                    ins.InsertPrijemKontVoz(Convert.ToDateTime(dtpDatumPrijema.Text), Convert.ToInt32(cboStatusPrijema.SelectedIndex), Convert.ToInt32(cboBukingPrijema.SelectedValue), Convert.ToDateTime(dtpVremeDolaska.Value), Convert.ToDateTime(DateTime.Now), KorisnikCene, txtRegBrKamiona.Text, txtImeVozaca.Text, 0, txtNapomena.Text,  Convert.ToInt32(cboPredefinisanePoruke.SelectedValue), 0, 0, 0, 0, 0);
                    status = false;
                    VratiPodatkeMax();
                }
                else
                {
                    //int TipCenovnika ,int Komitent, double Cena , int VrstaManipulacije ,DateTime  Datum , string Korisnik
                    Saobracaj.Dokumeta.InsertPrijemKontejneraVoz upd = new Saobracaj.Dokumeta.InsertPrijemKontejneraVoz();
                    upd.UpdPrijemKontejneraVoz(Convert.ToInt32(txtSifra.Text), Convert.ToDateTime(dtpDatumPrijema.Text), Convert.ToInt32(cboStatusPrijema.SelectedIndex), Convert.ToInt32(cboBukingPrijema.SelectedValue), Convert.ToDateTime(dtpVremeDolaska.Value), Convert.ToDateTime(DateTime.Now), KorisnikCene, txtRegBrKamiona.Text, txtImeVozaca.Text, 0, txtNapomena.Text, Convert.ToInt32(cboPredefinisanePoruke.SelectedValue), 0, 0, 0, 0, 0);
                    status = false;
                }
            
            
            }
           // RefreshDataGrid();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da brišete sve podatke?", "Brisanje celog prijem", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Saobracaj.Dokumeta.InsertPrijemKontejneraVoz upd = new Saobracaj.Dokumeta.InsertPrijemKontejneraVoz();
                upd.DeletePrijemKontejneraVoz(Convert.ToInt32(txtSifra.Text));
                MessageBox.Show("Izbrisani su podaci zaglavlja i pripadajuće stavke");
                RefreshDataGrid();
            }
            else if (dialogResult == DialogResult.No)
            {
            }

        }

        private void RefreshDataGrid()
        {
            var select = "  SELECT         PrijemKontejneraVozStavke.ID, PrijemKontejneraVozStavke.RB, PrijemKontejneraVozStavke.IDNadredjenog, " +
 " PrijemKontejneraVozStavke.BrojKontejnera, PrijemKontejneraVozStavke.BrojVagona, PrijemKontejneraVozStavke.Granica, " +
  "  PrijemKontejneraVozStavke.BrojOsovina, PrijemKontejneraVozStavke.SopstvenaMasa, PrijemKontejneraVozStavke.Tara," +
  "   PrijemKontejneraVozStavke.Neto, Partnerji.PaNaziv AS Posiljalac, Partnerji_1.PaNaziv AS primalac, " +
   "   Partnerji_2.PaNaziv AS Vlasnikkontejnera, TipKontenjera.Naziv AS TipKontejnera, NHM.Naziv AS VrstaRobe," +
  "     PrijemKontejneraVozStavke.BukingBrodar AS BukingBrodar, PrijemKontejneraVozStavke.StatusKontejnera," +
   "      PrijemKontejneraVozStavke.BrojPlombe, PrijemKontejneraVozStavke.BrojPlombe2,PrijemKontejneraVozStavke.PlaniraniLager, " +
   "       PrijemKontejneraVozStavke.VremeDolaska, PrijemKontejneraVozStavke.VremePripremljen,  PrijemKontejneraVozStavke.VremeOdlaska," +
   "        PrijemKontejneraVozStavke.Datum, PrijemKontejneraVozStavke.Korisnik, PrijemKontejneraVozStavke.NapomenaS" +
   "         FROM            Partnerji INNER JOIN PrijemKontejneraVozStavke" +
    "        ON Partnerji.PaSifra = PrijemKontejneraVozStavke.Posiljalac INNER JOIN" +
    "         Partnerji AS Partnerji_1 ON PrijemKontejneraVozStavke.Primalac = " +
    "         Partnerji_1.PaSifra" +
     "        INNER JOIN  Partnerji AS Partnerji_2" +
       "      ON PrijemKontejneraVozStavke.VlasnikKontejnera = Partnerji_2.PaSifra INNER JOIN TipKontenjera" +
       "       ON PrijemKontejneraVozStavke.TipKontejnera = TipKontenjera.ID" +
      "        INNER JOIN  NHM ON PrijemKontejneraVozStavke.VrstaRobe = NHM.ID" +
       "        INNER JOIN  PrijemKontejneraVoz ON PrijemKontejneraVozStavke.IdNAdredjenog = PrijemKontejneraVoz.ID" +
       "       INNER JOIN  Voz ON PrijemKontejneraVoz.IdVoza = Voz.ID " +
                           " where IdNadredjenog = " + txtSifra.Text + " order by RB";

            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = false;
            dataGridView1.DataSource = ds.Tables[0];

            //Panta refresh

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[0].Visible = false;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "RB";
            dataGridView1.Columns[1].Width = 30;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Br Dok";
            dataGridView1.Columns[2].Width = 30;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Broj kontejnera";
            dataGridView1.Columns[3].Width = 110;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Vagon";
            dataGridView1.Columns[4].Width = 110;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Granica";
            dataGridView1.Columns[5].Width = 50;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Br osov";
            dataGridView1.Columns[6].Width = 50;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Sop masa";
            dataGridView1.Columns[7].Width = 50;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Tara";
            dataGridView1.Columns[8].Width = 70;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Neto";
            dataGridView1.Columns[9].Width = 70;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Špediter";
            dataGridView1.Columns[10].Width = 150;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Primalac";
            dataGridView1.Columns[11].Width = 150;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Vlasnik kontejnera";
            dataGridView1.Columns[12].Width = 140;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Tip kontejnera";
            dataGridView1.Columns[13].Width = 70;

            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "NHM";
            dataGridView1.Columns[14].Width = 40;

            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Buking brodar";
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
            dataGridView1.Columns[19].HeaderText = "Lager";
            dataGridView1.Columns[19].Width = 30;

            DataGridViewColumn column21 = dataGridView1.Columns[20];
            dataGridView1.Columns[20].HeaderText = "Vreme dolaska";
            dataGridView1.Columns[20].Visible = false;
            dataGridView1.Columns[20].Width = 70;

            DataGridViewColumn column22 = dataGridView1.Columns[21];
            dataGridView1.Columns[21].HeaderText = "Pl Vreme pripreme";
            dataGridView1.Columns[21].Visible = false;
            dataGridView1.Columns[21].Width = 70;

            DataGridViewColumn column23 = dataGridView1.Columns[22];
            dataGridView1.Columns[22].HeaderText = "PL Vreme otpremeVreme odlaska";
            dataGridView1.Columns[22].Visible = false;
            dataGridView1.Columns[22].Width = 70;

            DataGridViewColumn column24 = dataGridView1.Columns[23];
            dataGridView1.Columns[23].HeaderText = "Datum";
            dataGridView1.Columns[23].Width = 70;

            DataGridViewColumn column25 = dataGridView1.Columns[24];
            dataGridView1.Columns[24].HeaderText = "Korisnik";
            dataGridView1.Columns[24].Width = 70;

            DataGridViewColumn column26 = dataGridView1.Columns[25];
            dataGridView1.Columns[25].HeaderText = "Napomena stavke";
            dataGridView1.Columns[25].Width = 70;


        }

        private void VratiPodatke(int ID)
        {
            
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            //VR SqlCommand cmd = new SqlCommand("select [ID] ,[DatumPrijema],[StatusPrijema],[IdVoza],[VremeDolaska],RegBrKamiona, ImeVozaca, NajavaEmail, PrijemEmail, Napomena, CIRUradjen, PredefinisanaPorukaID from PrijemKontejneraVoz where ID=" + ID, con);

            SqlCommand cmd = new SqlCommand("select [ID] ,[DatumPrijema],[StatusPrijema],[IdVoza],[VremeDolaska],RegBrKamiona, ImeVozaca, NajavaEmail, PrijemEmail, Napomena, CIRUradjen, Operater, OperaterHR, Modul, PredefinisanePorukeID from PrijemKontejneraVoz where ID=" + ID, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                dtpDatumPrijema.Value = Convert.ToDateTime(dr["DatumPrijema"].ToString());
                dtpVremeDolaska.Value = Convert.ToDateTime(dr["VremeDolaska"].ToString());
                cboBukingPrijema.SelectedValue = Convert.ToInt32(dr["IDVoza"].ToString());
                cboStatusPrijema.SelectedIndex = Convert.ToInt32(dr["StatusPrijema"].ToString());
                txtRegBrKamiona.Text = dr["RegBrKamiona"].ToString();
                cboPredefinisanePoruke.SelectedValue = Convert.ToInt32(dr["PredefinisanePorukeID"].ToString());
                //Napomena
                txtNapomena.Text = dr["Napomena"].ToString();
                
                if (Convert.ToInt32(dr["NajavaEmail"].ToString()) == 0)
                {
                    chkPoslatEmailNajava.Checked = false;
                }
                else
                {
                    chkPoslatEmailNajava.Checked = true;
                }

                if (Convert.ToInt32(dr["PrijemEmail"].ToString()) == 0)
                {
                    chkPoslatEmailPrijem.Checked = false;
                }
                else
                {
                    chkPoslatEmailPrijem.Checked = true;
                }
                if (Convert.ToInt32(dr["CIRUradjen"].ToString()) == 0)
                {
                    chkCIRUradjen.Checked = false;
                }
                else
                {
                    chkCIRUradjen.Checked = true;
                }


                txtImeVozaca.Text = dr["ImeVozaca"].ToString();
              
          
            }

            con.Close();
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtSifra.Text == "")
            {
                MessageBox.Show("Niste uneli zaglavlje");
            }
            else
            {
                Saobracaj.Dokumeta.InsertPrijemKontejneraVozStavke ins = new Saobracaj.Dokumeta.InsertPrijemKontejneraVozStavke();
                ins.InsertPrijemKontVozStavke(Convert.ToInt32(txtSifra.Text), txtBrojKontejnera.Text, txtVagon.Text, Convert.ToDouble(txtGranica.Value), Convert.ToDouble(txtBrojOsovina.Value), Convert.ToDouble(txtSopstvenaMasa.Value), Convert.ToDouble(txtTara.Value), Convert.ToDouble(txtNeto.Value), Convert.ToInt32(cboPosiljalac.SelectedValue), Convert.ToInt32(cboPrimalac.SelectedValue), Convert.ToInt32(cboVlasnikKontejnera.SelectedValue), Convert.ToInt32(cboTipKontejnera.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue), Convert.ToInt32(cboBukingOtpreme.SelectedValue), Convert.ToInt32(cboStatusKontejnera.SelectedValue), txtBrojPlombe.Text, Convert.ToInt32(txtPlaniraniLager.Text), Convert.ToInt32(cboBukingOtpreme.SelectedValue), Convert.ToDateTime(dtpVremeDolaska.Value), Convert.ToDateTime(dtpDatumPrijema.Value), Convert.ToDateTime(dtpVremeOdlaska.Value), Convert.ToDateTime(DateTime.Now), KorisnikCene, txtBrojPlombe2.Text, Convert.ToInt32(cboOrganizator.SelectedValue), txtBukingBrodar.Text, txtNapomenaS.Text,DateTime.Now, DateTime.Now,0, 0, 0 , "", 0,0, 0,"","","",0, 0);
                RefreshDataGrid();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Saobracaj.Dokumeta.InsertPrijemKontejneraVozStavke ins = new Saobracaj.Dokumeta.InsertPrijemKontejneraVozStavke();
            ins.UpdPrijemKontejneraVozStavke(Convert.ToInt32(txtStavka.Text), Convert.ToInt32(txtSifra.Text), txtBrojKontejnera.Text, txtVagon.Text, Convert.ToDouble(txtGranica.Value), Convert.ToDouble(txtBrojOsovina.Value), Convert.ToDouble(txtSopstvenaMasa.Value), Convert.ToDouble(txtTara.Value), Convert.ToDouble(txtNeto.Value), Convert.ToInt32(cboPosiljalac.SelectedValue), Convert.ToInt32(cboPrimalac.SelectedValue), Convert.ToInt32(cboVlasnikKontejnera.SelectedValue), Convert.ToInt32(cboTipKontejnera.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue), Convert.ToInt32(cboBukingOtpreme.SelectedValue), Convert.ToInt32(cboStatusKontejnera.SelectedValue), txtBrojPlombe.Text, Convert.ToInt32(txtPlaniraniLager.Text), Convert.ToInt32(cboBukingOtpreme.SelectedValue), Convert.ToDateTime(dtpVremeDolaska.Value), Convert.ToDateTime(dtpDatumPrijema.Value), Convert.ToDateTime(dtpVremeOdlaska.Value), Convert.ToDateTime(DateTime.Now), KorisnikCene, Convert.ToInt32(txtRB.Text), txtBrojPlombe2.Text, Convert.ToInt32(cboOrganizator.SelectedValue), txtBukingBrodar.Text, txtNapomenaS.Text, DateTime.Now, DateTime.Now, 0, 0, 0, "", 0, 0, 0, "0", "0", "0",0);
            RefreshDataGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da želite da brišete?", "Izbor", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                Saobracaj.Dokumeta.InsertPrijemKontejneraVozStavke dels = new Saobracaj.Dokumeta.InsertPrijemKontejneraVozStavke();
                dels.DeletePrijemKontejneraVozStavke(Convert.ToInt32(txtStavka.Text));
                RefreshDataGrid();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }


        }

        private void frmPrijemKontejneraVoz_Load(object sender, EventArgs e)
        {
           
        }

        private void FillCB()
        {
            var select = " Select Distinct ID, (Rtrim(Broj) + '-' + Naziv) as NHM  From NHM ORDER BY ID";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString; ;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboVrstaRobe.DataSource = ds.Tables[0];
            cboVrstaRobe.DisplayMember = "NHM";
            cboVrstaRobe.ValueMember = "ID";

            var select1 = " Select Distinct PaSifra as ID, PaNaziv as Naziv From Partnerji  order by PaNaziv";
            var s_connection1 = frmLogovanje.connectionString;
            SqlConnection myConnection1 = new SqlConnection(s_connection1);
            var c1 = new SqlConnection(s_connection1);
            var dataAdapter1 = new SqlDataAdapter(select1, c1);

            var commandBuilder1 = new SqlCommandBuilder(dataAdapter1);
            var ds1 = new DataSet();
            dataAdapter1.Fill(ds1);
            cboPosiljalac.DataSource = ds1.Tables[0];
            cboPosiljalac.DisplayMember = "Naziv";
            cboPosiljalac.ValueMember = "ID";
            //where Primalac = 1 
            var select2 = " Select Distinct PaSifra as ID, PaNaziv as Naziv From Partnerji order by PaNaziv";
            var s_connection2 = frmLogovanje.connectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboPrimalac.DataSource = ds2.Tables[0];
            cboPrimalac.DisplayMember = "Naziv";
            cboPrimalac.ValueMember = "ID";
            //where Vlasnik =1 
            var select3 = " Select Distinct PaSifra as ID, PaNaziv as Naziv From Partnerji  order by PaNaziv";
            var s_connection3 = frmLogovanje.connectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboVlasnikKontejnera.DataSource = ds3.Tables[0];
            cboVlasnikKontejnera.DisplayMember = "Naziv";
            cboVlasnikKontejnera.ValueMember = "ID";


            var select4 = " Select Distinct ID, Naziv From TipKontenjera order by Naziv ";
            var s_connection4 = frmLogovanje.connectionString;
            SqlConnection myConnection4 = new SqlConnection(s_connection4);
            var c4 = new SqlConnection(s_connection4);
            var dataAdapter4 = new SqlDataAdapter(select4, c4);

            var commandBuilder4 = new SqlCommandBuilder(dataAdapter4);
            var ds4 = new DataSet();
            dataAdapter4.Fill(ds4);
            cboTipKontejnera.DataSource = ds4.Tables[0];
            cboTipKontejnera.DisplayMember = "Naziv";
            cboTipKontejnera.ValueMember = "ID";

            var select9 = " Select Distinct PaSifra as ID, PaNaziv as Naziv From Partnerji  order by PaNaziv";
            var s_connection9 = frmLogovanje.connectionString;
            SqlConnection myConnection9 = new SqlConnection(s_connection9);
            var c9 = new SqlConnection(s_connection9);
            var dataAdapter9 = new SqlDataAdapter(select9, c9);

            var commandBuilder9 = new SqlCommandBuilder(dataAdapter9);
            var ds9 = new DataSet();
            dataAdapter9.Fill(ds9);
            cboOrganizator.DataSource = ds9.Tables[0];
            cboOrganizator.DisplayMember = "Naziv";
            cboOrganizator.ValueMember = "ID";
        }

        private void frmPrijemKontejneraVoz_Load_1(object sender, EventArgs e)
        {
            
            FillCB();
            //where Posiljalac = 1
           

    
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Saobracaj.Dokumenta.frmDokumentaPrijemKontejneraVoz frmd3 = new Saobracaj.Dokumenta.frmDokumentaPrijemKontejneraVoz(txtSifra.Text, KorisnikCene);
            frmd3.Show();
        }

        private void PosaljiMailOdjavaPrimalac(string Kome)
        {
            try
            {
                string zadnjibroj = txtSifra.Text;
                //drugi je kome
                var select2 = " SELECT Distinct Primalac"
                  + " FROM [dbo].[PrijemKontejneraVozStavke] where IDNadredjenog =  " + Convert.ToInt32(txtSifra.Text);

                var s_connection2 = frmLogovanje.connectionString;
                SqlConnection myConnection2 = new SqlConnection(s_connection2);
                var c2 = new SqlConnection(s_connection2);
                var dataAdapter2 = new SqlDataAdapter(select2, c2);

                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                foreach (DataRow myRow2 in ds2.Tables[0].Rows)
                {

                    mailMessage = new MailMessage("logistika@zitbgd.rs", Kome);

                    mailMessage.Subject = "RTC LUKA LEGET pristigla najava. Br.: " + zadnjibroj + " . ";

                    var select = "  SELECT PrijemKontejneraVozStavke.[ID] , Voz.BrVoza, BrojKontejnera, BrojVagona, Tara, Neto, Voz.Relacija, Voz.VremePolaska, Voz.VremeDolaska, " +
                    " PrijemKontejneraVozStavke.Primalac,  PrijemKontejneraVozStavke.Posiljalac, PrijemKontejneraVozStavke.VlasnikKontejnera, PrijemKontejneraVozStavke.Organizator " +
                " FROM [dbo].[PrijemKontejneraVozStavke] " +
				" inner join PrijemKontejneraVoz on PrijemKontejneraVozStavke.IDNadredjenog = PrijemKontejneraVoz.Id " +
				" inner join Voz on PrijemKontejneraVoz.IdVoza = Voz.Id " +
                " inner join Stanice on Voz.StanicaOd = Stanice.Id " +
                " where IDNadredjenog = " + Convert.ToInt32(txtSifra.Text) + " and Primalac =  " + myRow2["Primalac"].ToString();

                    var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    string body = "NAJAVA DOLASKA KONTEJNERA  :  <br /><br />";
                    body = body + " Poštovani,    <br /> <br /> <br />";
                  
                 
                    int countS = 0;
                    foreach (DataRow myRow in ds.Tables[0].Rows)
                    {
                        if (countS == 0)
                        {
                            body = body + " Očekivani dolazak je na relaciji :" + myRow["Relacija"].ToString() + "  vozom br.: " + myRow["BrVoza"].ToString() + "<br />";
                            body = body + " vreme polaska:  " + myRow["VremePolaska"].ToString() + "   vreme dolaska: " + myRow["VremeDolaska"].ToString() + "<br /><br /> ";
                            
                            body = body + " Prijavljujemo najavu dolaska kontejnera:  <br />";
                            body = body + "Broj kontejnera: <br />";
                        }
                        countS = countS + 1;
                        body = body + myRow["BrojKontejnera"].ToString() + ";";
                        //  body = body + "Broj vagona: " + myRow["BrojVagona"].ToString() + "<br />";
                        //  body = body + "Tara: " + myRow["Tara"].ToString() + "<br />";
                        //  body = body + "Neto: " + myRow["Neto"].ToString() + "<br />";
                        //

                    }

                    body = body + "<br /> Po izvršenom dolasku voza i prijemu kontejnera na terminal Leget-a u Beograd ranžirnoj obavestićemo Vas blagovremeno na e-mailom.   <br />";
                    body = body + "Terminal RTC LUKA LEGET <br />";
                    body = body + "Tel: <br />";
                    body = body + "Email: <br />";


                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.zitbgd.rs";

                    smtpClient.Port = 25;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential("logistika@zitbgd.rs", "zit2019log");

                    smtpClient.EnableSsl = false;
                    smtpClient.Send(mailMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void PosaljiMailOdjavaPosiljalac(string Kome)
        {
            try
            {
                string zadnjibroj = txtSifra.Text;
                //drugi je kome
                string nacinPrevoza = "";
                if (chkVoz.Checked == true)
                { nacinPrevoza = "vozom"; }
                else
                {
                    nacinPrevoza = "kamionom";
                }
                var select2 = " SELECT Distinct Posiljalac"
                  + " FROM [dbo].[PrijemKontejneraVozStavke] where IDNadredjenog =  " + Convert.ToInt32(txtSifra.Text);

                var s_connection2 = frmLogovanje.connectionString;
                SqlConnection myConnection2 = new SqlConnection(s_connection2);
                var c2 = new SqlConnection(s_connection2);
                var dataAdapter2 = new SqlDataAdapter(select2, c2);

                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                foreach (DataRow myRow2 in ds2.Tables[0].Rows)
                {

                    mailMessage = new MailMessage("logistika@zitbgd.rs", Kome);

                    mailMessage.Subject = "RTC LUKA LEGET pristigla najava. Br. voza: " + zadnjibroj + " . ";

                    var select = "  SELECT PrijemKontejneraVozStavke.[ID] , Voz.BrVoza, BrojKontejnera, BrojVagona, Tara, Neto, Voz.Relacija, Voz.VremePolaska, Voz.VremeDolaska, " +
                    " PrijemKontejneraVozStavke.Primalac,  PrijemKontejneraVozStavke.Posiljalac, PrijemKontejneraVozStavke.VlasnikKontejnera, PrijemKontejneraVozStavke.Organizator " +
                " FROM [dbo].[PrijemKontejneraVozStavke] " +
                " inner join PrijemKontejneraVoz on PrijemKontejneraVozStavke.IDNadredjenog = PrijemKontejneraVoz.Id " +
                " inner join Voz on PrijemKontejneraVoz.IdVoza = Voz.Id " +
                " inner join Stanice on Voz.StanicaOd = Stanice.Id " +
                " where IDNadredjenog = " + Convert.ToInt32(txtSifra.Text) + " and Posiljalac =  " + myRow2["Posiljalac"].ToString();

                    var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    string body = "NAJAVA DOLASKA KONTEJNERA VOZOM :  <br /><br />";
                    body = body + " Poštovani,    <br /> <br /> <br />";


                    int countS = 0;
                    foreach (DataRow myRow in ds.Tables[0].Rows)
                    {
                        if (countS == 0)
                        {
                            body = body + " Očekivani dolazak je na relaciji :" + myRow["Relacija"].ToString() + "  vozom br.: " + myRow["BrVoza"].ToString() + "<br />";
                            body = body + " vreme polaska:  " + myRow["VremePolaska"].ToString() + "   vreme dolaska: " + myRow["VremeDolaska"].ToString() + "<br /><br /> ";

                            body = body + " Prijavljujemo najavu dolaska kontejnera:  <br />";
                            body = body + "Broj kontejnera: <br />";
                        }
                        countS = countS + 1;
                        body = body + myRow["BrojKontejnera"].ToString() + ";";
                        //  body = body + "Broj vagona: " + myRow["BrojVagona"].ToString() + "<br />";
                        //  body = body + "Tara: " + myRow["Tara"].ToString() + "<br />";
                        //  body = body + "Neto: " + myRow["Neto"].ToString() + "<br />";
                        //

                    }

                    body = body + "<br /> Po izvršenom dolasku voza i prijemu kontejnera na terminal Leget-a u Beograd ranžirnoj obavestićemo Vas blagovremeno na e-mailom.   <br />";
                    body = body + "Terminal RTC LUKA LEGET <br />";
                  


                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.zitbgd.rs";

                    smtpClient.Port = 25;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential("logistika@zitbgd.rs", "zit2019log");

                    smtpClient.EnableSsl = false;
                    smtpClient.Send(mailMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void PosaljiMailOdjavaVlasnikKontejnera(string Kome)
        {
            try
            {
                string zadnjibroj = txtSifra.Text;
                //drugi je kome
                var select2 = " SELECT Distinct VlasnikKontejnera"
                  + " FROM [dbo].[PrijemKontejneraVozStavke] where IDNadredjenog =  " + Convert.ToInt32(txtSifra.Text);

                var s_connection2 = frmLogovanje.connectionString;
                SqlConnection myConnection2 = new SqlConnection(s_connection2);
                var c2 = new SqlConnection(s_connection2);
                var dataAdapter2 = new SqlDataAdapter(select2, c2);

                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                foreach (DataRow myRow2 in ds2.Tables[0].Rows)
                {

                    mailMessage = new MailMessage("logistika@zitbgd.rs", Kome);

                    mailMessage.Subject = "RTC LUKA LEGET pristigla najava. Br. : " + zadnjibroj + " . ";

                    var select = "  SELECT PrijemKontejneraVozStavke.[ID] , Voz.BrVoza, BrojKontejnera, BrojVagona, Tara, Neto, Voz.Relacija, Voz.VremePolaska, Voz.VremeDolaska, " +
                    " PrijemKontejneraVozStavke.Primalac,  PrijemKontejneraVozStavke.Posiljalac, PrijemKontejneraVozStavke.VlasnikKontejnera, PrijemKontejneraVozStavke.Organizator " +
                " FROM [dbo].[PrijemKontejneraVozStavke] " +
                " inner join PrijemKontejneraVoz on PrijemKontejneraVozStavke.IDNadredjenog = PrijemKontejneraVoz.Id " +
                " inner join Voz on PrijemKontejneraVoz.IdVoza = Voz.Id " +
                " inner join Stanice on Voz.StanicaOd = Stanice.Id " +
                " where IDNadredjenog = " + Convert.ToInt32(txtSifra.Text) + " and VlasnikKontejnera =  " + myRow2["VlasnikKontejnera"].ToString();

                    var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    string body = "NAJAVA DOLASKA KONTEJNERA  :  <br /><br />";
                    body = body + " Poštovani,    <br /> <br /> <br />";


                    int countS = 0;
                    foreach (DataRow myRow in ds.Tables[0].Rows)
                    {
                        if (countS == 0)
                        {
                            body = body + " Očekivani dolazak je na relaciji :" + myRow["Relacija"].ToString() + "  vozom br.: " + myRow["BrVoza"].ToString() + "<br />";
                            body = body + " vreme polaska:  " + myRow["VremePolaska"].ToString() + "   vreme dolaska: " + myRow["VremeDolaska"].ToString() + "<br /><br /> ";

                            body = body + " Prijavljujemo najavu dolaska kontejnera:  <br />";
                            body = body + "Broj kontejnera: <br />";
                        }
                        countS = countS + 1;
                        body = body + myRow["BrojKontejnera"].ToString() + ";";
                        //  body = body + "Broj vagona: " + myRow["BrojVagona"].ToString() + "<br />";
                        //  body = body + "Tara: " + myRow["Tara"].ToString() + "<br />";
                        //  body = body + "Neto: " + myRow["Neto"].ToString() + "<br />";
                        //

                    }

                    body = body + "<br /> Po izvršenom dolasku voza i prijemu kontejnera na terminal Leget-a u Beograd ranžirnoj obavestićemo Vas blagovremeno na e-mailom.   <br />";
                    body = body + "Terminal RTC LUKA LEGET <br />";
                 


                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.zitbgd.rs";

                    smtpClient.Port = 25;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential("logistika@zitbgd.rs", "zit2019log");

                    smtpClient.EnableSsl = false;
                    smtpClient.Send(mailMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void PosaljiMailOdjavaOrganizator(string Kome)
        {
            try
            {
                string zadnjibroj = txtSifra.Text;
                //drugi je kome
                var select2 = " SELECT Distinct Organizator"
                  + " FROM [dbo].[PrijemKontejneraVozStavke] where IDNadredjenog =  " + Convert.ToInt32(txtSifra.Text);

                var s_connection2 = frmLogovanje.connectionString;
                SqlConnection myConnection2 = new SqlConnection(s_connection2);
                var c2 = new SqlConnection(s_connection2);
                var dataAdapter2 = new SqlDataAdapter(select2, c2);

                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                foreach (DataRow myRow2 in ds2.Tables[0].Rows)
                {

                    mailMessage = new MailMessage("logistika@zitbgd.rs", Kome);

                    mailMessage.Subject = "RTC LUKA LEGET pristigla najava. Br. voza: " + zadnjibroj + " . ";

                    var select = "  SELECT PrijemKontejneraVozStavke.[ID] , Voz.BrVoza, BrojKontejnera, BrojVagona, Tara, Neto, Voz.Relacija, Voz.VremePolaska, Voz.VremeDolaska, " +
                    " PrijemKontejneraVozStavke.Primalac,  PrijemKontejneraVozStavke.Posiljalac, PrijemKontejneraVozStavke.VlasnikKontejnera, PrijemKontejneraVozStavke.Organizator " +
                " FROM [dbo].[PrijemKontejneraVozStavke] " +
                " inner join PrijemKontejneraVoz on PrijemKontejneraVozStavke.IDNadredjenog = PrijemKontejneraVoz.Id " +
                " inner join Voz on PrijemKontejneraVoz.IdVoza = Voz.Id " +
                " inner join Stanice on Voz.StanicaOd = Stanice.Id " +
                " where IDNadredjenog = " + Convert.ToInt32(txtSifra.Text) + " and Organizator =  " + myRow2["Organizator"].ToString();

                    var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    string body = "NAJAVA DOLASKA KONTEJNERA VOZOM :  <br /><br />";
                    body = body + " Poštovani,    <br /> <br /> <br />";


                    int countS = 0;
                    foreach (DataRow myRow in ds.Tables[0].Rows)
                    {
                        if (countS == 0)
                        {
                            body = body + " Očekivani dolazak je na relaciji :" + myRow["Relacija"].ToString() + "  vozom br.: " + myRow["BrVoza"].ToString() + "<br />";
                            body = body + " vreme polaska:  " + myRow["VremePolaska"].ToString() + "   vreme dolaska: " + myRow["VremeDolaska"].ToString() + "<br /><br /> ";

                            body = body + " Prijavljujemo najavu dolaska kontejnera:  <br />";
                            body = body + "Broj kontejnera: <br />";
                        }
                        countS = countS + 1;
                        body = body + myRow["BrojKontejnera"].ToString() + ";";
                        //  body = body + "Broj vagona: " + myRow["BrojVagona"].ToString() + "<br />";
                        //  body = body + "Tara: " + myRow["Tara"].ToString() + "<br />";
                        //  body = body + "Neto: " + myRow["Neto"].ToString() + "<br />";
                        //

                    }

                    body = body + "<br /> Po izvršenom dolasku voza i prijemu kontejnera na terminal Leget-a u Beograd ranžirnoj obavestićemo Vas blagovremeno na e-mailom.   <br />";
                    body = body + "Terminal RTC LUKA LEGET <br />";
                  


                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.zitbgd.rs";

                    smtpClient.Port = 25;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential("logistika@zitbgd.rs", "zit2019log");

                    smtpClient.EnableSsl = false;
                    smtpClient.Send(mailMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void PosaljiMailOdjavaPrimalacPrijem(string Kome)
        {
            try
            {
                string zadnjibroj = txtSifra.Text;
                //drugi je kome
                var select2 = " SELECT Distinct Primalac"
                  + " FROM [dbo].[PrijemKontejneraVozStavke] where IDNadredjenog =  " + Convert.ToInt32(txtSifra.Text);

                var s_connection2 = frmLogovanje.connectionString;
                SqlConnection myConnection2 = new SqlConnection(s_connection2);
                var c2 = new SqlConnection(s_connection2);
                var dataAdapter2 = new SqlDataAdapter(select2, c2);

                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                foreach (DataRow myRow2 in ds2.Tables[0].Rows)
                {

                    mailMessage = new MailMessage("logistika@zitbgd.rs", Kome);

                    mailMessage.Subject = "RTC LUKA LEGET pristigao voz. Br. zapisa: " + zadnjibroj + " . ";

                    var select = "  SELECT PrijemKontejneraVozStavke.[ID] , Voz.BrVoza, BrojKontejnera, BrojVagona, Tara, Neto, Voz.Relacija, Voz.VremePolaska, Voz.VremeDolaska, " +
                    " PrijemKontejneraVoz.VremeDolaska as vd, PrijemKontejneraVozStavke.Primalac,  PrijemKontejneraVozStavke.Posiljalac, PrijemKontejneraVozStavke.VlasnikKontejnera, PrijemKontejneraVozStavke.Organizator " +
                " FROM [dbo].[PrijemKontejneraVozStavke] " +
                " inner join PrijemKontejneraVoz on PrijemKontejneraVozStavke.IDNadredjenog = PrijemKontejneraVoz.Id " +
                " inner join Voz on PrijemKontejneraVoz.IdVoza = Voz.Id " +
                " inner join Stanice on Voz.StanicaOd = Stanice.Id " +
                " where IDNadredjenog = " + Convert.ToInt32(txtSifra.Text) + " and Primalac =  " + myRow2["Primalac"].ToString();

                    var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    string body = "PRIJEM KONTEJNERA VOZOM :  <br /><br />";
                    body = body + " Poštovani,    <br /> <br /> <br />";


                    int countS = 0;
                    foreach (DataRow myRow in ds.Tables[0].Rows)
                    {
                        if (countS == 0)
                        {
                            body = body + " Dolazak na relaciji :" + myRow["Relacija"].ToString() + "  vozom br.: " + myRow["BrVoza"].ToString() + "<br />";
                            body = body + " vreme polaska:  " + myRow["VremePolaska"].ToString() + "   stvarno vreme dolaska: " + myRow["vd"].ToString() + "<br /><br /> ";

                            body = body + " Prijavljujemo dolazak kontejnera na Leget terminal  <br />";
                            body = body + "Broj kontejnera: <br />";
                        }
                        countS = countS + 1;
                        body = body + myRow["BrojKontejnera"].ToString() + ";";
                      
                    }

                 
                    body = body + "Terminal RTC LUKA LEGET <br />";
                  


                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.zitbgd.rs";

                    smtpClient.Port = 25;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential("logistika@zitbgd.rs", "zit2019log");

                    smtpClient.EnableSsl = false;
                    smtpClient.Send(mailMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void PosaljiMailOdjavaPosiljalacPrijem(string Kome)
        {
            try
            {
                string zadnjibroj = txtSifra.Text;
                //drugi je kome
                var select2 = " SELECT Distinct Posiljalac"
                  + " FROM [dbo].[PrijemKontejneraVozStavke] where IDNadredjenog =  " + Convert.ToInt32(txtSifra.Text);

                var s_connection2 = frmLogovanje.connectionString;
                SqlConnection myConnection2 = new SqlConnection(s_connection2);
                var c2 = new SqlConnection(s_connection2);
                var dataAdapter2 = new SqlDataAdapter(select2, c2);

                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                foreach (DataRow myRow2 in ds2.Tables[0].Rows)
                {

                    mailMessage = new MailMessage("logistika@zitbgd.rs", Kome);

                    mailMessage.Subject = "RTC LUKA LEGET pristigao voz. Br. zapisa: " + zadnjibroj + " . ";

                    var select = "  SELECT PrijemKontejneraVozStavke.[ID] , Voz.BrVoza, BrojKontejnera, BrojVagona, Tara, Neto, Voz.Relacija, Voz.VremePolaska, Voz.VremeDolaska, " +
                    " PrijemKontejneraVoz.VremeDolaska as vd, PrijemKontejneraVozStavke.Primalac,  PrijemKontejneraVozStavke.Posiljalac, PrijemKontejneraVozStavke.VlasnikKontejnera, PrijemKontejneraVozStavke.Organizator " +
                " FROM [dbo].[PrijemKontejneraVozStavke] " +
                " inner join PrijemKontejneraVoz on PrijemKontejneraVozStavke.IDNadredjenog = PrijemKontejneraVoz.Id " +
                " inner join Voz on PrijemKontejneraVoz.IdVoza = Voz.Id " +
                " inner join Stanice on Voz.StanicaOd = Stanice.Id " +
                " where IDNadredjenog = " + Convert.ToInt32(txtSifra.Text) + " and Posiljalac =  " + myRow2["Posiljalac"].ToString();

                    var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    string body = "PRIJEM KONTEJNERA VOZOM :  <br /><br />";
                    body = body + " Poštovani,    <br /> <br /> <br />";


                    int countS = 0;
                    foreach (DataRow myRow in ds.Tables[0].Rows)
                    {
                        if (countS == 0)
                        {
                            body = body + " Dolazak na relaciji :" + myRow["Relacija"].ToString() + "  vozom br.: " + myRow["BrVoza"].ToString() + "<br />";
                            body = body + " vreme polaska:  " + myRow["VremePolaska"].ToString() + "   stvarno vreme dolaska: " + myRow["vd"].ToString() + "<br /><br /> ";

                            body = body + " Prijavljujemo dolazak kontejnera na RTC LUKA LEGET terminal  <br />";
                            body = body + "Broj kontejnera: <br />";
                        }
                        countS = countS + 1;
                        body = body + myRow["BrojKontejnera"].ToString() + ";";

                    }


                    body = body + "Terminal RTC LUKA LEGET <br />";
                  

                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.zitbgd.rs";

                    smtpClient.Port = 25;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential("logistika@zitbgd.rs", "zit2019log");

                    smtpClient.EnableSsl = false;
                    smtpClient.Send(mailMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void PosaljiMailOdjavaVlasnikKontejneraPrijem(string Kome)
        {
            try
            {
                string zadnjibroj = txtSifra.Text;
                //drugi je kome
                var select2 = " SELECT Distinct VlasnikKontejnera"
                  + " FROM [dbo].[PrijemKontejneraVozStavke] where IDNadredjenog =  " + Convert.ToInt32(txtSifra.Text);

                var s_connection2 = frmLogovanje.connectionString;
                SqlConnection myConnection2 = new SqlConnection(s_connection2);
                var c2 = new SqlConnection(s_connection2);
                var dataAdapter2 = new SqlDataAdapter(select2, c2);

                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                foreach (DataRow myRow2 in ds2.Tables[0].Rows)
                {

                    mailMessage = new MailMessage("logistika@zitbgd.rs", Kome);

                    mailMessage.Subject = "RTC LUKA LEGET pristigao voz. Br. zapisa: " + zadnjibroj + " . ";

                    var select = "  SELECT PrijemKontejneraVozStavke.[ID] , Voz.BrVoza, BrojKontejnera, BrojVagona, Tara, Neto, Voz.Relacija, Voz.VremePolaska, Voz.VremeDolaska, " +
                    " PrijemKontejneraVoz.VremeDolaska as vd, PrijemKontejneraVozStavke.Primalac,  PrijemKontejneraVozStavke.Posiljalac, PrijemKontejneraVozStavke.VlasnikKontejnera, PrijemKontejneraVozStavke.Organizator " +
                " FROM [dbo].[PrijemKontejneraVozStavke] " +
                " inner join PrijemKontejneraVoz on PrijemKontejneraVozStavke.IDNadredjenog = PrijemKontejneraVoz.Id " +
                " inner join Voz on PrijemKontejneraVoz.IdVoza = Voz.Id " +
                " inner join Stanice on Voz.StanicaOd = Stanice.Id " +
                " where IDNadredjenog = " + Convert.ToInt32(txtSifra.Text) + " and VlasnikKontejnera =  " + myRow2["VlasnikKontejnera"].ToString();

                    var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    string body = "PRIJEM KONTEJNERA VOZOM :  <br /><br />";
                    body = body + " Poštovani,    <br /> <br /> <br />";


                    int countS = 0;
                    foreach (DataRow myRow in ds.Tables[0].Rows)
                    {
                        if (countS == 0)
                        {
                            body = body + " Dolazak na relaciji :" + myRow["Relacija"].ToString() + "  vozom br.: " + myRow["BrVoza"].ToString() + "<br />";
                            body = body + " vreme polaska:  " + myRow["VremePolaska"].ToString() + "   stvarno vreme dolaska: " + myRow["vd"].ToString() + "<br /><br /> ";

                            body = body + " Prijavljujemo dolazak kontejnera na RTC LUKA LEGET terminal  <br />";
                            body = body + "Broj kontejnera: <br />";
                        }
                        countS = countS + 1;
                        body = body + myRow["BrojKontejnera"].ToString() + ";";

                    }


                    body = body + "Terminal RTC LUKA LEGET  <br />";
                


                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.zitbgd.rs";

                    smtpClient.Port = 25;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential("logistika@zitbgd.rs", "zit2019log");

                    smtpClient.EnableSsl = false;
                    smtpClient.Send(mailMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void PosaljiMailOdjavaOrganizatorPrijem(string Kome)
        {
            try
            {
                string zadnjibroj = txtSifra.Text;
                //drugi je kome
                var select2 = " SELECT Distinct Organizator"
                  + " FROM [dbo].[PrijemKontejneraVozStavke] where IDNadredjenog =  " + Convert.ToInt32(txtSifra.Text);

                var s_connection2 = frmLogovanje.connectionString;
                SqlConnection myConnection2 = new SqlConnection(s_connection2);
                var c2 = new SqlConnection(s_connection2);
                var dataAdapter2 = new SqlDataAdapter(select2, c2);

                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                foreach (DataRow myRow2 in ds2.Tables[0].Rows)
                {

                    mailMessage = new MailMessage("logistika@zitbgd.rs", Kome);

                    mailMessage.Subject = "RTC LUKA LEGET pristigao voz. Br. zapisa: " + zadnjibroj + " . ";

                    var select = "  SELECT PrijemKontejneraVozStavke.[ID] , Voz.BrVoza, BrojKontejnera, BrojVagona, Tara, Neto, Voz.Relacija, Voz.VremePolaska, Voz.VremeDolaska, " +
                    " PrijemKontejneraVoz.VremeDolaska as vd, PrijemKontejneraVozStavke.Primalac,  PrijemKontejneraVozStavke.Posiljalac, PrijemKontejneraVozStavke.VlasnikKontejnera, PrijemKontejneraVozStavke.Organizator " +
                " FROM [dbo].[PrijemKontejneraVozStavke] " +
                " inner join PrijemKontejneraVoz on PrijemKontejneraVozStavke.IDNadredjenog = PrijemKontejneraVoz.Id " +
                " inner join Voz on PrijemKontejneraVoz.IdVoza = Voz.Id " +
                " inner join Stanice on Voz.StanicaOd = Stanice.Id " +
                " where IDNadredjenog = " + Convert.ToInt32(txtSifra.Text) + " and Organizator =  " + myRow2["Organizator"].ToString();

                    var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    string body = "PRIJEM KONTEJNERA VOZOM :  <br /><br />";
                    body = body + " Poštovani,    <br /> <br /> <br />";


                    int countS = 0;
                    foreach (DataRow myRow in ds.Tables[0].Rows)
                    {
                        if (countS == 0)
                        {
                            body = body + " Dolazak na relaciji :" + myRow["Relacija"].ToString() + "  vozom br.: " + myRow["BrVoza"].ToString() + "<br />";
                            body = body + " vreme polaska:  " + myRow["VremePolaska"].ToString() + "   stvarno vreme dolaska: " + myRow["vd"].ToString() + "<br /><br /> ";

                            body = body + " Prijavljujemo dolazak kontejnera na RTC LUKA LEGET terminal  <br />";
                            body = body + "Broj kontejnera: <br />";
                        }
                        countS = countS + 1;
                        body = body + myRow["BrojKontejnera"].ToString() + ";";

                    }


                    body = body + "Terminal RTC LUKA LEGETa <br />";
                  


                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.zitbgd.rs";

                    smtpClient.Port = 25;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential("logistika@zitbgd.rs", "zit2019log");

                    smtpClient.EnableSsl = false;
                    smtpClient.Send(mailMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        /// <summary>
        /// Kamion
        /// </summary>
        private void PosaljiMailOdjavaPrimalacKamion(string Kome)
        {
            try
            {
                string zadnjibroj = txtSifra.Text;
                //drugi je kome
                var select2 = " SELECT Distinct Primalac"
                  + " FROM [dbo].[PrijemKontejneraVozStavke] where IDNadredjenog =  " + Convert.ToInt32(txtSifra.Text);

                var s_connection2 = frmLogovanje.connectionString;
                SqlConnection myConnection2 = new SqlConnection(s_connection2);
                var c2 = new SqlConnection(s_connection2);
                var dataAdapter2 = new SqlDataAdapter(select2, c2);

                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                foreach (DataRow myRow2 in ds2.Tables[0].Rows)
                {

                    mailMessage = new MailMessage("logistika@zitbgd.rs", Kome);

                    mailMessage.Subject = "RTC LUKA LEGET pristigla najava. Br. zapisa: " + zadnjibroj + " . ";

                    var select = "  SELECT PrijemKontejneraVozStavke.[ID] , RegBrKamiona, BrojKontejnera, BrojVagona, Tara, Neto, ImeVozaca, PrijemKontejneraVozStavke.VremeDolaska, " +
                    " PrijemKontejneraVozStavke.Primalac,  PrijemKontejneraVozStavke.Posiljalac, PrijemKontejneraVozStavke.VlasnikKontejnera, PrijemKontejneraVozStavke.Organizator " +
                " FROM [dbo].[PrijemKontejneraVozStavke] " +
                " inner join PrijemKontejneraVoz on PrijemKontejneraVozStavke.IDNadredjenog = PrijemKontejneraVoz.Id " +
                " where IDNadredjenog = " + Convert.ToInt32(txtSifra.Text) + " and Primalac =  " + myRow2["Primalac"].ToString();

                    var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    string body = "NAJAVA DOLASKA KONTEJNERA KAMIONOM   <br /><br />";
                    body = body + " Poštovani,    <br /> <br /> <br />";


                    int countS = 0;
                    foreach (DataRow myRow in ds.Tables[0].Rows)
                    {
                        if (countS == 0)
                        {
                            body = body + " Dolazak kamionom  :" + myRow["RegBrKamiona"].ToString() + "  vozač: " + myRow["ImeVozaca"].ToString() + "<br />";
                           // body = body + " vreme polaska:  " + myRow["VremePolaska"].ToString() + "   vreme dolaska: " + myRow["VremeDolaska"].ToString() + "<br /><br /> ";

                            body = body + " Prijavljujemo najavu dolaska kontejnera:  <br />";
                            body = body + "Broj kontejnera: <br />";
                        }
                        countS = countS + 1;
                        body = body + myRow["BrojKontejnera"].ToString() + ";";
                        //  body = body + "Broj vagona: " + myRow["BrojVagona"].ToString() + "<br />";
                        //  body = body + "Tara: " + myRow["Tara"].ToString() + "<br />";
                        //  body = body + "Neto: " + myRow["Neto"].ToString() + "<br />";
                        //

                    }

                    body = body + "<br /> Po izvršenom dolasku kamiona i prijemu kontejnera na terminal RTC LUKA LEGET obavestićemo Vas blagovremeno na e-mailom.   <br />";
                    body = body + "Terminal RTC LUKA LEGET<br />";
                  


                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.zitbgd.rs";

                    smtpClient.Port = 25;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential("logistika@zitbgd.rs", "zit2019log");

                    smtpClient.EnableSsl = false;
                    smtpClient.Send(mailMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void PosaljiMailOdjavaPosiljalacKamion(string Kome)
        {
            try
            {
                string zadnjibroj = txtSifra.Text;
                //drugi je kome
               
                var select2 = " SELECT Distinct Posiljalac"
                  + " FROM [dbo].[PrijemKontejneraVozStavke] where IDNadredjenog =  " + Convert.ToInt32(txtSifra.Text);

                var s_connection2 = frmLogovanje.connectionString;
                SqlConnection myConnection2 = new SqlConnection(s_connection2);
                var c2 = new SqlConnection(s_connection2);
                var dataAdapter2 = new SqlDataAdapter(select2, c2);

                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                foreach (DataRow myRow2 in ds2.Tables[0].Rows)
                {

                    mailMessage = new MailMessage("logistika@zitbgd.rs", Kome);

                    mailMessage.Subject = "RTC LUKA LEGET pristigla najava. Br. zapisa " + zadnjibroj + " . ";

                    var select = "  SELECT PrijemKontejneraVozStavke.[ID] , RegBrKamiona, BrojKontejnera, BrojVagona, Tara, Neto, ImeVozaca, PrijemKontejneraVozStavke.VremeDolaska, " +
                   " PrijemKontejneraVozStavke.Primalac,  PrijemKontejneraVozStavke.Posiljalac, PrijemKontejneraVozStavke.VlasnikKontejnera, PrijemKontejneraVozStavke.Organizator " +
               " FROM [dbo].[PrijemKontejneraVozStavke] " +
               " inner join PrijemKontejneraVoz on PrijemKontejneraVozStavke.IDNadredjenog = PrijemKontejneraVoz.Id " +
               " where IDNadredjenog = " + Convert.ToInt32(txtSifra.Text) + " and Posiljalac =  " + myRow2["Posiljalac"].ToString();

                    var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    string body = "NAJAVA DOLASKA KONTEJNERA KAMIONOM  <br /><br />";
                    body = body + " Poštovani,    <br /> <br /> <br />";


                    int countS = 0;
                    foreach (DataRow myRow in ds.Tables[0].Rows)
                    {
                        if (countS == 0)
                        {
                            body = body + " Dolazak kamionom  :" + myRow["RegBrKamiona"].ToString() + "  vozač: " + myRow["ImeVozaca"].ToString() + "<br />";
                            // body = body + " vreme polaska:  " + myRow["VremePolaska"].ToString() + "   vreme dolaska: " + myRow["VremeDolaska"].ToString() + "<br /><br /> ";

                            body = body + " Prijavljujemo najavu dolaska kontejnera:  <br />";
                            body = body + "Broj kontejnera: <br />";
                        }
                        countS = countS + 1;
                        body = body + myRow["BrojKontejnera"].ToString() + ";";
                        //  body = body + "Broj vagona: " + myRow["BrojVagona"].ToString() + "<br />";
                        //  body = body + "Tara: " + myRow["Tara"].ToString() + "<br />";
                        //  body = body + "Neto: " + myRow["Neto"].ToString() + "<br />";
                        //

                    }

                    body = body + "<br /> Po izvršenom dolasku kamiona i prijemu kontejnera na terminal RTC LUKA LEGET obavestićemo Vas blagovremeno na e-mailom.   <br />";
                    body = body + "Terminal RTC LUKA LEGET <br />";



                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.zitbgd.rs";

                    smtpClient.Port = 25;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential("logistika@zitbgd.rs", "zit2019log");

                    smtpClient.EnableSsl = false;
                    smtpClient.Send(mailMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void PosaljiMailOdjavaVlasnikKontejneraKamion(string Kome)
        {
            try
            {
                string zadnjibroj = txtSifra.Text;
                //drugi je kome
                var select2 = " SELECT Distinct VlasnikKontejnera"
                  + " FROM [dbo].[PrijemKontejneraVozStavke] where IDNadredjenog =  " + Convert.ToInt32(txtSifra.Text);

                var s_connection2 = frmLogovanje.connectionString;
                SqlConnection myConnection2 = new SqlConnection(s_connection2);
                var c2 = new SqlConnection(s_connection2);
                var dataAdapter2 = new SqlDataAdapter(select2, c2);

                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                foreach (DataRow myRow2 in ds2.Tables[0].Rows)
                {

                    mailMessage = new MailMessage("logistika@zitbgd.rs", Kome);

                    mailMessage.Subject = "RTC LUKA LEGET pristigla najava. Br. zapisa: " + zadnjibroj + " . ";

                    var select = "  SELECT PrijemKontejneraVozStavke.[ID] , RegBrKamiona, BrojKontejnera, BrojVagona, Tara, Neto, ImeVozaca, PrijemKontejneraVozStavke.VremeDolaska, " +
                   " PrijemKontejneraVozStavke.Primalac,  PrijemKontejneraVozStavke.Posiljalac, PrijemKontejneraVozStavke.VlasnikKontejnera, PrijemKontejneraVozStavke.Organizator " +
               " FROM [dbo].[PrijemKontejneraVozStavke] " +
               " inner join PrijemKontejneraVoz on PrijemKontejneraVozStavke.IDNadredjenog = PrijemKontejneraVoz.Id " +
               " where IDNadredjenog = " + Convert.ToInt32(txtSifra.Text) + " and VlasnikKontejnera =  " + myRow2["VlasnikKontejnera"].ToString();

                    var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    string body = "NAJAVA DOLASKA KONTEJNERA KAMIONOM   <br /><br />";
                    body = body + " Poštovani,    <br /> <br /> <br />";


                    int countS = 0;
                    foreach (DataRow myRow in ds.Tables[0].Rows)
                    {
                        if (countS == 0)
                        {
                            body = body + " Dolazak kamionom  :" + myRow["RegBrKamiona"].ToString() + "  vozač: " + myRow["ImeVozaca"].ToString() + "<br />";
                            // body = body + " vreme polaska:  " + myRow["VremePolaska"].ToString() + "   vreme dolaska: " + myRow["VremeDolaska"].ToString() + "<br /><br /> ";

                            body = body + " Prijavljujemo najavu dolaska kontejnera:  <br />";
                            body = body + "Broj kontejnera: <br />";
                        }
                        countS = countS + 1;
                        body = body + myRow["BrojKontejnera"].ToString() + ";";
                        //  body = body + "Broj vagona: " + myRow["BrojVagona"].ToString() + "<br />";
                        //  body = body + "Tara: " + myRow["Tara"].ToString() + "<br />";
                        //  body = body + "Neto: " + myRow["Neto"].ToString() + "<br />";
                        //

                    }

                    body = body + "<br /> Po izvršenom dolasku kamiona i prijemu kontejnera na terminal RTC LUKA LEGET obavestićemo Vas blagovremeno na e-mailom.   <br />";
                    body = body + "Terminal RTC LUKA LEGET <br />";



                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.zitbgd.rs";

                    smtpClient.Port = 25;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential("logistika@zitbgd.rs", "zit2019log");

                    smtpClient.EnableSsl = false;
                    smtpClient.Send(mailMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void PosaljiMailOdjavaOrganizatorKamion(string Kome)
        {
            try
            {
                string zadnjibroj = txtSifra.Text;
                //drugi je kome
                var select2 = " SELECT Distinct Organizator"
                  + " FROM [dbo].[PrijemKontejneraVozStavke] where IDNadredjenog =  " + Convert.ToInt32(txtSifra.Text);

                var s_connection2 = frmLogovanje.connectionString;
                SqlConnection myConnection2 = new SqlConnection(s_connection2);
                var c2 = new SqlConnection(s_connection2);
                var dataAdapter2 = new SqlDataAdapter(select2, c2);

                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                foreach (DataRow myRow2 in ds2.Tables[0].Rows)
                {

                    mailMessage = new MailMessage("logistika@zitbgd.rs", Kome);

                    mailMessage.Subject = "RTC LUKA LEGET pristigla najava. Br. zapisa: " + zadnjibroj + " . ";

                    var select = "  SELECT PrijemKontejneraVozStavke.[ID] , RegBrKamiona, BrojKontejnera, BrojVagona, Tara, Neto, ImeVozaca, PrijemKontejneraVozStavke.VremeDolaska, " +
                    " PrijemKontejneraVozStavke.Primalac,  PrijemKontejneraVozStavke.Posiljalac, PrijemKontejneraVozStavke.VlasnikKontejnera, PrijemKontejneraVozStavke.Organizator " +
                " FROM [dbo].[PrijemKontejneraVozStavke] " +
                " inner join PrijemKontejneraVoz on PrijemKontejneraVozStavke.IDNadredjenog = PrijemKontejneraVoz.Id " +
                " where IDNadredjenog = " + Convert.ToInt32(txtSifra.Text) + " and Organizator =  " + myRow2["Organizator"].ToString();

                    var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    string body = "NAJAVA DOLASKA KONTEJNERA KAMIONOM   <br /><br />";
                    body = body + " Poštovani,    <br /> <br /> <br />";


                    int countS = 0;
                    foreach (DataRow myRow in ds.Tables[0].Rows)
                    {
                        if (countS == 0)
                        {
                            body = body + " Dolazak kamionom  :" + myRow["RegBrKamiona"].ToString() + "  vozač: " + myRow["ImeVozaca"].ToString() + "<br />";
                            // body = body + " vreme polaska:  " + myRow["VremePolaska"].ToString() + "   vreme dolaska: " + myRow["VremeDolaska"].ToString() + "<br /><br /> ";

                            body = body + " Prijavljujemo najavu dolaska kontejnera:  <br />";
                            body = body + "Broj kontejnera: <br />";
                        }
                        countS = countS + 1;
                        body = body + myRow["BrojKontejnera"].ToString() + ";";
                        //  body = body + "Broj vagona: " + myRow["BrojVagona"].ToString() + "<br />";
                        //  body = body + "Tara: " + myRow["Tara"].ToString() + "<br />";
                        //  body = body + "Neto: " + myRow["Neto"].ToString() + "<br />";
                        //

                    }

                    body = body + "<br /> Po izvršenom dolasku kamiona i prijemu kontejnera na terminal RTC LUKA LEGET obavestićemo Vas blagovremeno na e-mailom.   <br />";
                    body = body + "Terminal RTC LUKA LEGET <br />";



                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.zitbgd.rs";

                    smtpClient.Port = 25;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential("logistika@zitbgd.rs", "zit2019log");

                    smtpClient.EnableSsl = false;
                    smtpClient.Send(mailMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void PosaljiMailOdjavaPrimalacPrijemKamion(string Kome)
        {
            try
            {
                string zadnjibroj = txtSifra.Text;
                //drugi je kome
                var select2 = " SELECT Distinct Primalac"
                  + " FROM [dbo].[PrijemKontejneraVozStavke] where IDNadredjenog =  " + Convert.ToInt32(txtSifra.Text);

                var s_connection2 = frmLogovanje.connectionString;
                SqlConnection myConnection2 = new SqlConnection(s_connection2);
                var c2 = new SqlConnection(s_connection2);
                var dataAdapter2 = new SqlDataAdapter(select2, c2);

                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                foreach (DataRow myRow2 in ds2.Tables[0].Rows)
                {

                    mailMessage = new MailMessage("logistika@zitbgd.rs", Kome);

                    mailMessage.Subject = "RTC LUKA LEGET pristigao  kamion Br. zapisa: " + zadnjibroj + " . ";

                    var select = "  SELECT PrijemKontejneraVozStavke.[ID] , RegBrKamiona, BrojKontejnera, BrojVagona, Tara, Neto, ImeVozaca, PrijemKontejneraVozStavke.VremeDolaska, " +
                     " PrijemKontejneraVozStavke.Primalac,  PrijemKontejneraVozStavke.Posiljalac, PrijemKontejneraVozStavke.VlasnikKontejnera, PrijemKontejneraVozStavke.Organizator " +
                 " FROM [dbo].[PrijemKontejneraVozStavke] " +
                 " inner join PrijemKontejneraVoz on PrijemKontejneraVozStavke.IDNadredjenog = PrijemKontejneraVoz.Id " +
                 " where IDNadredjenog = " + Convert.ToInt32(txtSifra.Text) + " and Primalac =  " + myRow2["Primalac"].ToString();

                    var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    string body = "PRIJEM KONTEJNERA KAMIONOM   <br /><br />";
                    body = body + " Poštovani,    <br /> <br /> <br />";


                    int countS = 0;
                    foreach (DataRow myRow in ds.Tables[0].Rows)
                    {
                        if (countS == 0)
                        {
                            body = body + " Dolazak kamionom  :" + myRow["RegBrKamiona"].ToString() + "  vozač: " + myRow["ImeVozaca"].ToString() + "<br />";
                            // body = body + " vreme polaska:  " + myRow["VremePolaska"].ToString() + "   vreme dolaska: " + myRow["VremeDolaska"].ToString() + "<br /><br /> ";

                            body = body + " Prijavljujemo  dolazak kontejnera:  <br />";
                            body = body + "Broj kontejnera: <br />";
                        }
                        countS = countS + 1;
                        body = body + myRow["BrojKontejnera"].ToString() + ";";
                        //  body = body + "Broj vagona: " + myRow["BrojVagona"].ToString() + "<br />";
                        //  body = body + "Tara: " + myRow["Tara"].ToString() + "<br />";
                        //  body = body + "Neto: " + myRow["Neto"].ToString() + "<br />";
                        //

                    }

                    body = body + "<br /> <br />";
                    body = body + "Terminal RTC LUKA LEGET <br />";



                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.zitbgd.rs";

                    smtpClient.Port = 25;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential("logistika@zitbgd.rs", "zit2019log");

                    smtpClient.EnableSsl = false;
                    smtpClient.Send(mailMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void PosaljiMailOdjavaPosiljalacPrijemKamion(string Kome)
        {
            try
            {
                string zadnjibroj = txtSifra.Text;
                //drugi je kome
                var select2 = " SELECT Distinct Posiljalac"
                  + " FROM [dbo].[PrijemKontejneraVozStavke] where IDNadredjenog =  " + Convert.ToInt32(txtSifra.Text);

                var s_connection2 = frmLogovanje.connectionString;
                SqlConnection myConnection2 = new SqlConnection(s_connection2);
                var c2 = new SqlConnection(s_connection2);
                var dataAdapter2 = new SqlDataAdapter(select2, c2);

                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                foreach (DataRow myRow2 in ds2.Tables[0].Rows)
                {

                    mailMessage = new MailMessage("logistika@zitbgd.rs", Kome);

                    mailMessage.Subject = "RTC LUKA LEGET pristigao kamion Br. zapisa: " + zadnjibroj + " . ";

                    var select = "  SELECT PrijemKontejneraVozStavke.[ID] , RegBrKamiona, BrojKontejnera, BrojVagona, Tara, Neto, ImeVozaca, PrijemKontejneraVozStavke.VremeDolaska, " +
                 " PrijemKontejneraVozStavke.Primalac,  PrijemKontejneraVozStavke.Posiljalac, PrijemKontejneraVozStavke.VlasnikKontejnera, PrijemKontejneraVozStavke.Organizator " +
             " FROM [dbo].[PrijemKontejneraVozStavke] " +
             " inner join PrijemKontejneraVoz on PrijemKontejneraVozStavke.IDNadredjenog = PrijemKontejneraVoz.Id " +
             " where IDNadredjenog = " + Convert.ToInt32(txtSifra.Text) + " and Posiljalac =  " + myRow2["Posiljalac"].ToString();

                    var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    string body = "PRIJEM KONTEJNERA KAMIONOM   <br /><br />";
                    body = body + " Poštovani,    <br /> <br /> <br />";


                    int countS = 0;
                    foreach (DataRow myRow in ds.Tables[0].Rows)
                    {
                        if (countS == 0)
                        {
                            body = body + " Dolazak kamionom  :" + myRow["RegBrKamiona"].ToString() + "  vozač: " + myRow["ImeVozaca"].ToString() + "<br />";
                            // body = body + " vreme polaska:  " + myRow["VremePolaska"].ToString() + "   vreme dolaska: " + myRow["VremeDolaska"].ToString() + "<br /><br /> ";

                            body = body + " Prijavljujemo  dolazak kontejnera:  <br />";
                            body = body + "Broj kontejnera: <br />";
                        }
                        countS = countS + 1;
                        body = body + myRow["BrojKontejnera"].ToString() + ";";
                        //  body = body + "Broj vagona: " + myRow["BrojVagona"].ToString() + "<br />";
                        //  body = body + "Tara: " + myRow["Tara"].ToString() + "<br />";
                        //  body = body + "Neto: " + myRow["Neto"].ToString() + "<br />";
                        //

                    }

                    // body = body + "<br /> Po izvršenom dolasku kamiona i prijemu kontejnera na terminal Leget-a u Beograd ranžirnoj obavestićemo Vas blagovremeno na e-mailom.   <br />";
                    body = body + "<br /> <br />";
                    body = body + "Terminal RTC LUKA LEGET <br />";




                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.zitbgd.rs";

                    smtpClient.Port = 25;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential("logistika@zitbgd.rs", "zit2019log");

                    smtpClient.EnableSsl = false;
                    smtpClient.Send(mailMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void PosaljiMailOdjavaVlasnikKontejneraPrijemKamion(string Kome)
        {
            try
            {
                string zadnjibroj = txtSifra.Text;
                //drugi je kome
                var select2 = " SELECT Distinct VlasnikKontejnera"
                  + " FROM [dbo].[PrijemKontejneraVozStavke] where IDNadredjenog =  " + Convert.ToInt32(txtSifra.Text);

                var s_connection2 = frmLogovanje.connectionString;
                SqlConnection myConnection2 = new SqlConnection(s_connection2);
                var c2 = new SqlConnection(s_connection2);
                var dataAdapter2 = new SqlDataAdapter(select2, c2);

                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                foreach (DataRow myRow2 in ds2.Tables[0].Rows)
                {

                    mailMessage = new MailMessage("logistika@zitbgd.rs", Kome);

                    mailMessage.Subject = "RTC LUKA LEGET pristigao kamion Br. zapisa: " + zadnjibroj + " . ";

                    var select = "  SELECT PrijemKontejneraVozStavke.[ID] , RegBrKamiona, BrojKontejnera, BrojVagona, Tara, Neto, ImeVozaca, PrijemKontejneraVozStavke.VremeDolaska," +
                 " PrijemKontejneraVozStavke.Primalac,  PrijemKontejneraVozStavke.Posiljalac, PrijemKontejneraVozStavke.VlasnikKontejnera, PrijemKontejneraVozStavke.Organizator " +
             " FROM [dbo].[PrijemKontejneraVozStavke] " +
             " inner join PrijemKontejneraVoz on PrijemKontejneraVozStavke.IDNadredjenog = PrijemKontejneraVoz.Id " +
             " where IDNadredjenog = " + Convert.ToInt32(txtSifra.Text) + " and VlasnikKontejnera =  " + myRow2["VlasnikKontejnera"].ToString();

                    var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    string body = "PRIJEM KONTEJNERA KAMIONOM   <br /><br />";
                    body = body + " Poštovani,    <br /> <br /> <br />";


                    int countS = 0;
                    foreach (DataRow myRow in ds.Tables[0].Rows)
                    {
                        if (countS == 0)
                        {
                            body = body + " Dolazak kamionom  :" + myRow["RegBrKamiona"].ToString() + "  vozač: " + myRow["ImeVozaca"].ToString() + "<br />";
                            // body = body + " vreme polaska:  " + myRow["VremePolaska"].ToString() + "   vreme dolaska: " + myRow["VremeDolaska"].ToString() + "<br /><br /> ";

                            body = body + " Prijavljujemo  dolazak kontejnera:  <br />";
                            body = body + "Broj kontejnera: <br />";
                        }
                        countS = countS + 1;
                        body = body + myRow["BrojKontejnera"].ToString() + ";";
                        //  body = body + "Broj vagona: " + myRow["BrojVagona"].ToString() + "<br />";
                        //  body = body + "Tara: " + myRow["Tara"].ToString() + "<br />";
                        //  body = body + "Neto: " + myRow["Neto"].ToString() + "<br />";
                        //

                    }

                    // body = body + "<br /> Po izvršenom dolasku kamiona i prijemu kontejnera na terminal Leget-a u Beograd ranžirnoj obavestićemo Vas blagovremeno na e-mailom.   <br />";
                    body = body + "<br /> <br />";
                    body = body + "Terminal RTC LUKA LEGET <br />";





                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.zitbgd.rs";

                    smtpClient.Port = 25;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential("logistika@zitbgd.rs", "zit2019log");

                    smtpClient.EnableSsl = false;
                    smtpClient.Send(mailMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void PosaljiMailOdjavaOrganizatorPrijemKamion(string Kome)
        {
            try
            {
                string zadnjibroj = txtSifra.Text;
                //drugi je kome
                var select2 = " SELECT Distinct Organizator"
                  + " FROM [dbo].[PrijemKontejneraVozStavke] where IDNadredjenog =  " + Convert.ToInt32(txtSifra.Text);

                var s_connection2 = frmLogovanje.connectionString;
                SqlConnection myConnection2 = new SqlConnection(s_connection2);
                var c2 = new SqlConnection(s_connection2);
                var dataAdapter2 = new SqlDataAdapter(select2, c2);

                var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                var ds2 = new DataSet();
                dataAdapter2.Fill(ds2);
                foreach (DataRow myRow2 in ds2.Tables[0].Rows)
                {

                    mailMessage = new MailMessage("logistika@zitbgd.rs", Kome);

                    mailMessage.Subject = "RTC LUKA LEGET pristigao kamion Br. zapisa: " + zadnjibroj + " . ";

                    var select = "  SELECT PrijemKontejneraVozStavke.[ID] , RegBrKamiona, BrojKontejnera, BrojVagona, Tara, Neto, ImeVozaca, PrijemKontejneraVozStavke.VremeDolaska, " +
                 " PrijemKontejneraVozStavke.Primalac,  PrijemKontejneraVozStavke.Posiljalac, PrijemKontejneraVozStavke.VlasnikKontejnera, PrijemKontejneraVozStavke.Organizator " +
             " FROM [dbo].[PrijemKontejneraVozStavke] " +
             " inner join PrijemKontejneraVoz on PrijemKontejneraVozStavke.IDNadredjenog = PrijemKontejneraVoz.Id " +
             " where IDNadredjenog = " + Convert.ToInt32(txtSifra.Text) + " and Organizator =  " + myRow2["Organizator"].ToString();

                    var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    string body = "PRIJEM KONTEJNERA KAMIONOM  <br /><br />";
                    body = body + " Poštovani,    <br /> <br /> <br />";


                    int countS = 0;
                    foreach (DataRow myRow in ds.Tables[0].Rows)
                    {
                        if (countS == 0)
                        {
                            body = body + " Dolazak kamionom  :" + myRow["RegBrKamiona"].ToString() + "  vozač: " + myRow["ImeVozaca"].ToString() + "<br />";
                            // body = body + " vreme polaska:  " + myRow["VremePolaska"].ToString() + "   vreme dolaska: " + myRow["VremeDolaska"].ToString() + "<br /><br /> ";

                            body = body + " Prijavljujemo  dolazak kontejnera:  <br />";
                            body = body + "Broj kontejnera: <br />";
                        }
                        countS = countS + 1;
                        body = body + myRow["BrojKontejnera"].ToString() + ";";
                        //  body = body + "Broj vagona: " + myRow["BrojVagona"].ToString() + "<br />";
                        //  body = body + "Tara: " + myRow["Tara"].ToString() + "<br />";
                        //  body = body + "Neto: " + myRow["Neto"].ToString() + "<br />";
                        //

                    }

                    // body = body + "<br /> Po izvršenom dolasku kamiona i prijemu kontejnera na terminal Leget-a u Beograd ranžirnoj obavestićemo Vas blagovremeno na e-mailom.   <br />";
                    body = body + "<br /> <br />";
                    body = body + "Terminal RTC LUKA LEGET<br />";





                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "mail.zitbgd.rs";

                    smtpClient.Port = 25;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential("logistika@zitbgd.rs", "zit2019log");

                    smtpClient.EnableSsl = false;
                    smtpClient.Send(mailMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }
        /// <returns></returns>


        string VratiPodatkeEmailPrimalac()
        {
            string emailovi = "";
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT email from Partnerji " +
             " where Id = " + Convert.ToInt32(cboPrimalac.SelectedValue), con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                emailovi = dr["email"].ToString();
               
            }

            con.Close();
            return emailovi;
        }

        string VratiPodatkeEmailPosiljalac()
        {
            string emailovi = "";
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT email from Partnerji " +
             " where Id = " + Convert.ToInt32(cboPosiljalac.SelectedValue), con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                emailovi = dr["email"].ToString();

            }

            con.Close();
            return emailovi;
        }

        string VratiPodatkeEmailOrganizator()
        {
            string emailovi = "";
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT email from Partnerji " +
             " where Id = " + Convert.ToInt32(cboOrganizator.SelectedValue), con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                emailovi = dr["email"].ToString();

            }

            con.Close();
            return emailovi;
        }

        string VratiPodatkeEmailVlasnikKontejnera()
        {
            string emailovi = "";
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT email from Partnerji " +
             " where Id = " + Convert.ToInt32(cboVlasnikKontejnera.SelectedValue), con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                emailovi = dr["email"].ToString();

            }

            con.Close();
            return emailovi;
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            if (cboStatusPrijema.SelectedIndex == 0)
            {
                if (chkPoslatEmailNajava.Checked == true)
                {
                    MessageBox.Show("Mail za najavu je već poslat");
                }
                else
                {
                    string phrase = VratiPodatkeEmailPrimalac();
                    string[] words = phrase.Split(';');

                    foreach (var word in words)
                    {
                        if (chkVoz.Checked == true)
                        { PosaljiMailOdjavaPrimalac(word.Trim()); }
                        else
                        {
                            PosaljiMailOdjavaPrimalacKamion(word.Trim());
                        }
                    }

                    string phrase2 = VratiPodatkeEmailPosiljalac();
                    string[] words2 = phrase2.Split(';');

                    foreach (var word2 in words2)
                    {
                        if (chkVoz.Checked == true)
                        { PosaljiMailOdjavaPosiljalac(word2.Trim()); }
                        else
                        { PosaljiMailOdjavaPosiljalacKamion(word2.Trim()); }
                    }

                    string phrase3 = VratiPodatkeEmailOrganizator();
                    string[] words3 = phrase3.Split(';');

                    foreach (var word3 in words3)
                    {
                        if (chkVoz.Checked == true)
                        { PosaljiMailOdjavaOrganizator(word3.Trim()); }
                        else
                        {
                            PosaljiMailOdjavaOrganizatorKamion(word3.Trim());
                        }
                    }


                    string phrase4 = VratiPodatkeEmailVlasnikKontejnera();
                    string[] words4 = phrase4.Split(';');

                    foreach (var word4 in words4)
                    {
                        if (chkVoz.Checked == true)
                        { PosaljiMailOdjavaVlasnikKontejnera(word4.Trim()); }
                        else
                        {
                            PosaljiMailOdjavaVlasnikKontejneraKamion(word4.Trim());
                        }
                            
                    }

                    Saobracaj.Dokumeta.InsertPrijemKontejneraVoz insTer = new Saobracaj.Dokumeta.InsertPrijemKontejneraVoz();
                    insTer.UpdateEmailPrijemNajava(Convert.ToInt32(txtSifra.Text));
                    chkPoslatEmailNajava.Checked = true;
                }
            
            }
            else
            {

                if (chkPoslatEmailPrijem.Checked == true)
                {
                    MessageBox.Show("Mail za prijem je već poslat");
                }
                else
                {
                    string phrase = VratiPodatkeEmailPrimalac();
                    string[] words = phrase.Split(';');

                    foreach (var word in words)
                    {
                        if (chkVoz.Checked == true)
                        { PosaljiMailOdjavaPrimalacPrijem(word.Trim()); }
                        else
                        {
                            PosaljiMailOdjavaPrimalacPrijemKamion(word.Trim());
                        }
                            
                    }

                    string phrase2 = VratiPodatkeEmailPosiljalac();
                    string[] words2 = phrase2.Split(';');

                    foreach (var word2 in words2)
                    {
                        if (chkVoz.Checked == true)
                        { PosaljiMailOdjavaPosiljalacPrijem(word2.Trim()); }
                        else
                        {
                            PosaljiMailOdjavaPosiljalacPrijemKamion(word2.Trim());
                        }
                            
                    }

                    string phrase3 = VratiPodatkeEmailOrganizator();
                    string[] words3 = phrase3.Split(';');

                    foreach (var word3 in words3)
                    {
                        if (chkVoz.Checked == true)
                        { PosaljiMailOdjavaOrganizatorPrijem(word3.Trim()); }
                        else
                        {
                            PosaljiMailOdjavaOrganizatorPrijemKamion(word3.Trim());
                        }   
                    }

                    string phrase4 = VratiPodatkeEmailVlasnikKontejnera();
                    string[] words4 = phrase4.Split(';');

                    foreach (var word4 in words4)
                    {
                        if (chkVoz.Checked == true)
                        { PosaljiMailOdjavaVlasnikKontejneraPrijem(word4.Trim()); }
                        else
                        {
                            PosaljiMailOdjavaVlasnikKontejneraPrijemKamion(word4.Trim());
                        } 
                    }

                    // PosaljiMailOdjavaPosiljalacPrijem("");
                    // PosaljiMailOdjavaVlasnikKontejneraPrijem("");
                    // PosaljiMailOdjavaOrganizatorPrijem("");
                    Saobracaj.Dokumeta.InsertPrijemKontejneraVoz insTer = new Saobracaj.Dokumeta.InsertPrijemKontejneraVoz();
                    insTer.UpdateEmailPrijemPrijem(Convert.ToInt32(txtSifra.Text));
                    chkPoslatEmailPrijem.Checked = true;
                }
                  
            }
        }

        private void txtBrojKontejnera_Leave(object sender, EventArgs e)
        {
            ProveraKontrolnogBroja();
            ProveraPrijemnicaZaKontejner(txtBrojKontejnera.Text.Trim());
        }

        private void ProveraPrijemnicaZaKontejner(string brojkontejnera)
        { 
        
        }

        private void ProveraKontrolnogBroja()
        {
            string KontrolniBroj = txtBrojKontejnera.Text.TrimEnd();
            string CheckDigit = "100";
            CheckDigit = KontrolniBroj.Substring(KontrolniBroj.Length - 1, 1);
            int kontrolni = 0;
            /*
            string A = "10"; string B = "11"; string C = "12"; string D = "13"; string E = "14"; string F = "15";
            string G = "16"; string H = "17"; string I = "18"; string J = "19"; string K = "20"; string L = "21";
            string M = "22"; string N = "23"; string O = "24"; string P = "25"; string Q = "26"; string R = "27";
            string S = "28"; string T= "29"; string U = "30"; string V = "31"; string W = "32"; string X = "33";
            string Y = "34"; string Z = "35";
            */
            string foo = KontrolniBroj.ToUpper();
            int ukupno = 0;
            int korak = 1;
            int Broj1 = 0;
            int korak1 = 0;
            int Ukupno1 = 0;
            foreach (char c in foo)
            {
                switch (c)
                {
                    case 'A':
                        // Some code here
                        Broj1 = 10;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 10;
                        korak = korak * 2;
                        break; // break that closes the case

                    case 'B':
                        Broj1 = 12;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 12;
                        korak = korak * 2;
                        break; // break that closes the case
                    case 'C':
                        Broj1 = 13;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 13;
                        korak = korak * 2;
                        break; // 
                    case 'D':
                        Broj1 = 14;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 14;
                        korak = korak * 2;
                        break; // 
                    case 'E':
                        Broj1 = 15;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 15;
                        korak = korak * 2;
                        break; // 
                    case 'F':
                        Broj1 = 16;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 16;
                        korak = korak * 2;
                        break; // 
                    case 'G':
                        Broj1 = 17;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 17;
                        korak = korak * 2;
                        break; // 
                    case 'H':
                        Broj1 = 18;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 18;
                        korak = korak * 2;
                        break; // 
                    case 'I':
                        Broj1 = 19;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 19;
                        korak = korak * 2;
                        break; // 
                    case 'J':
                        Broj1 = 20;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 20;
                        korak = korak * 2;
                        break; // 
                    case 'K':
                        Broj1 = 21;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 21;
                        korak = korak * 2;
                        break; // 
                    case 'L':
                        Broj1 = 23;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 23;
                        korak = korak * 2;
                        break; // 
                    case 'M':
                        Broj1 = 24;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 24;
                        korak = korak * 2;
                        break; // 
                    case 'N':
                        Broj1 = 25;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 25;
                        korak = korak * 2;
                        break; // 
                    case 'O':
                        Broj1 = 26;
                        korak1 = korak + 1;
                        ukupno = ukupno + korak * 26;
                        korak = korak * 2;
                        break; // 
                    case 'P':
                        Broj1 = 27;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 27;
                        korak = korak * 2;
                        break; // 
                    case 'Q':
                        Broj1 = 28;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 28;
                        korak = korak * 2;
                        break; // 
                    case 'R':
                        Broj1 = 29;
                        korak1 = korak + 1;
                        ukupno = ukupno + korak * 29;
                        korak = korak * 2;
                        break; // 
                    case 'S':
                        Broj1 = 30;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 30;
                        korak = korak * 2;
                        break; // 
                    case 'T':
                        Broj1 = 31;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 31;
                        korak = korak * 2;
                        break; // 
                    case 'U':
                        Broj1 = 32;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 32;
                        korak = korak * 2;
                        break; // 
                    case 'V':
                        Broj1 = 34;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 34;
                        korak = korak * 2;
                        break; // 
                    case 'W':
                        Broj1 = 35;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 35;
                        korak = korak * 2;
                        break; // 
                    case 'X':
                        Broj1 = 36;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 36;
                        korak = korak * 2;
                        break; // 
                    case 'Y':
                        Broj1 = 37;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 37;
                        korak = korak * 2;
                        break; // 
                    case 'Z':
                        Broj1 = 38;
                        korak1 = korak1 + 1;
                        ukupno = ukupno + korak * 38;
                        korak = korak * 2;
                        break; // 
                    case '0':
                        Broj1 = 0;
                        korak1 = korak1 + 1;
                    
                        break; // 
                    case '1':
                        Broj1 = 1;
                        korak1 = korak1 + 1;

                        break; // 
                    case '2':
                        Broj1 = 2;
                        korak1 = korak1 + 1;

                        break; //
                    case '3':
                        Broj1 = 3;
                        korak1 = korak1 + 1;

                        break; //
                    case '4':
                        Broj1 = 4;
                        korak1 = korak1 + 1;

                        break; //
                    case '5':
                        Broj1 = 5;
                        korak1 = korak1 + 1;
                        break; //
                    case '6':
                        Broj1 = 6;
                        korak1 = korak1 + 1;
                        break; //
                    case '7':
                        Broj1 = 7;
                        korak1 = korak1 + 1;
                        break; //
                    case '8':
                        Broj1 = 8;
                        korak1 = korak1 + 1;
                        break; //
                    case '9':
                        Broj1 = 9;
                        korak1 = korak1 + 1;
                        break; //
                    default:
                        {
                           
                                    break;
                         }
                }

                switch (korak1)
                {
                    case 1:
                        Ukupno1 = Ukupno1 + Broj1 * 1;
                        break; //
                    case 2:
                        Ukupno1 = Ukupno1 + Broj1 * 2;
                        break; //
                    case 3:
                        Ukupno1 = Ukupno1 + Broj1 * 4;
                        break; //
                    case 4:
                        Ukupno1 = Ukupno1 + Broj1 * 8;
                        break; //
                    case 5:
                        Ukupno1 = Ukupno1 + Broj1 * 16;
                        break; //
                    case 6:
                        Ukupno1 = Ukupno1 + Broj1 * 32;
                        break; //
                    case 7:
                        Ukupno1 = Ukupno1 + Broj1 * 64;
                        break; //
                    case 8:
                        Ukupno1 = Ukupno1 + Broj1 * 128;
                        break; //
                    case 9:
                        Ukupno1 = Ukupno1 + Broj1 * 256;
                        break;
                    case 10:
                        Ukupno1 = Ukupno1 + Broj1 * 512;
                        break;
                    default:
                        break;


                }

            }
            double kolicnik = Ukupno1 / 11;
            var Bez = int.Parse(kolicnik.ToString().Split('.').First());
            int Ukupno2 = Bez * 11;
            kontrolni = Ukupno1 - Ukupno2;


            int pomUkupno = ukupno / 11;
            pomUkupno = pomUkupno * 11;

            int ProveraJed = ukupno - pomUkupno;

            if (kontrolni.ToString() == CheckDigit)
            {
                //MessageBox.Show("Ispravan kontrolni broj");
            }
            else
            {
                MessageBox.Show("Pogrešan kontrolni broj");
            }
            /*
            if (ProveraJed.ToString() == CheckDigit)
            {
                MessageBox.Show("Ispravan kontrolni broj");
            }
            else
            {
                MessageBox.Show("Pogrešan kontrolni broj");
            }
            */

        }
      
        private void VratiPodatkeStavke(string IdNadredjenog, int RB)
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT [ID],[IDNadredjenog]       ,[BrojKontejnera],[BrojVagona] " +
             " ,[Granica],[BrojOsovina],[SopstvenaMasa],[Tara] " +
             " ,[Neto],[Posiljalac],[Primalac],[VlasnikKontejnera] " +
             " ,[TipKontejnera],[VrstaRobe],[Buking],[StatusKontejnera] " +
             " ,[BrojPlombe],[PlaniraniLager],[IdVoza],[VremeDolaska] " +
             " ,[VremePripremljen],[VremeOdlaska],[Datum],[Korisnik] " +
             " ,[RB],[BrojPlombe2],[Organizator], BukingBrodar, NapomenaS " +
             " FROM [dbo].[PrijemKontejneraVozStavke] " +
             " where IdNadredjenog = " + txtSifra.Text + " and RB = " + RB, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
              txtStavka.Text = dr["ID"].ToString();
              txtRB.Text = dr["RB"].ToString();
              txtBrojKontejnera.Text = dr["BrojKontejnera"].ToString();
              txtVagon.Text  = dr["BrojVagona"].ToString();
              txtTara.Value = Convert.ToDecimal(dr["Tara"].ToString());
              txtNeto.Value = Convert.ToDecimal(dr["Neto"].ToString());
              txtGranica.Value = Convert.ToDecimal(dr["Granica"].ToString());
              txtBrojOsovina.Value = Convert.ToDecimal(dr["BrojOsovina"].ToString());
              txtSopstvenaMasa.Value = Convert.ToDecimal(dr["SopstvenaMasa"].ToString());
              cboPosiljalac.SelectedValue = Convert.ToInt32(dr["Posiljalac"].ToString());
              cboPrimalac.SelectedValue = Convert.ToInt32(dr["Primalac"].ToString());
              cboVlasnikKontejnera.SelectedValue = Convert.ToInt32(dr["VlasnikKontejnera"].ToString());
              cboTipKontejnera.SelectedValue = Convert.ToInt32(dr["TipKontejnera"].ToString());
              cboVrstaRobe.SelectedValue = Convert.ToInt32(dr["VrstaRobe"].ToString());
              cboStatusKontejnera.SelectedValue = Convert.ToInt32(dr["StatusKontejnera"].ToString());
              cboBukingOtpreme.SelectedValue = Convert.ToInt32(dr["Buking"].ToString());
              cboOrganizator.SelectedValue = Convert.ToInt32(dr["Organizator"].ToString());
              txtBukingBrodar.Text = dr["BukingBrodar"].ToString();
              dtpVremeDolaska.Value = Convert.ToDateTime(dr["VremeDolaska"].ToString());
              dtpVremePripremljen.Value = Convert.ToDateTime(dr["VremePripremljen"].ToString());
              dtpVremeOdlaska.Value = Convert.ToDateTime(dr["VremeOdlaska"].ToString());
              txtBrojPlombe.Text = dr["BrojPlombe"].ToString();
              txtBrojPlombe2.Text = dr["BrojPlombe2"].ToString();
                //Napomena
                txtNapomenaS.Text = dr["NapomenaS"].ToString();
            }

            con.Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txtSifra.Text = row.Cells[2].Value.ToString();
                        VratiPodatkeStavke(txtSifra.Text, Convert.ToInt32(row.Cells[1].Value.ToString()));
                        PostupakRefresh(Convert.ToInt32(txtStavka.Text));


                    }
                }
                
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
           
        }

        private void cboBukingPrijema_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (usao == 1)
            {
                VratiPodatkeVoz(Convert.ToInt32(cboBukingPrijema.SelectedValue));
            }
           
        }

        private void VratiPodatkeVoz(int id)
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT VremePolaska, VremeDolaska " +
             " FROM [dbo].[Voz] " +
             " where Id = " + id , con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                dtpDatumPrijema.Value = Convert.ToDateTime(dr["VremeDolaska"].ToString());
                dtpVremeDolaska.Value = Convert.ToDateTime(dr["VremeDolaska"].ToString());
                // dtpVremePripremljen.Value = Convert.ToDateTime(dr["VremePripremljen"].ToString());
            }

            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    Saobracaj.Dokumeta.InsertPrijemKontejneraVozStavke insTer = new Saobracaj.Dokumeta.InsertPrijemKontejneraVozStavke();
                    insTer.UpdatePrijemKontejneraVozStavkeRB(Convert.ToInt32(row.Cells[1].Value.ToString()), Convert.ToInt32(row.Cells[0].Value.ToString()));
                }
                RefreshDataGrid();
            }
            catch
            {
                MessageBox.Show("Nije uspela promena rednog broja");
            }
        }

        private void dtpVremeDolaska_ValueChanged(object sender, EventArgs e)
        {
            if (cboStatusPrijema.Text == "1-Najava")
            {
                MessageBox.Show("Prijem je u statusu najava promena ATA nije ispravno");
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (txtStavka.Text.Trim() == "")
            {
                MessageBox.Show("Niste izabrali stavku za koju radite CIR");
                return;
            }
            Saobracaj.Dokumenta.frmCIR cir = new Saobracaj.Dokumenta.frmCIR(KorisnikCene, Convert.ToInt32(txtStavka.Text), 0, txtBrojKontejnera.Text, txtVagon.Text, Convert.ToDouble(txtTara.Value), txtRegBrKamiona.Text, Convert.ToDouble(txtNeto.Value), Convert.ToInt32(cboTipKontejnera.SelectedValue), dtpVremeDolaska.Value, txtBrojPlombe.Text, txtBrojPlombe2.Text);
            cir.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            int PostojiPrvi = 0;
            int PostojiDrugi = 0;
            string BrojKontejnera1 = "";
            string BrojKontejnera2 = "";
            string VrstaRobe1 = "";
            string VrstaRobe2 = "";
            double ukupnaMasa = 0;
            double ukupnaMasa2 = 0;
            string TipKontejnera = "";
            string TipKontejnera2 = "";
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        if (PostojiPrvi == 1)
                        {
                            // BrojKontejnera1 = row.Cells[3].Value.ToString();
                            BrojKontejnera2 = row.Cells[3].Value.ToString();
                            PostojiDrugi = 1;
                            VrstaRobe2 = row.Cells[14].Value.ToString();
                           // ukupnaMasa2 = ukupnaMasa + Convert.ToDouble(row.Cells[7].Value.ToString()) + Convert.ToDouble(row.Cells[8].Value.ToString()) + (Convert.ToDouble(row.Cells[9].Value.ToString()) / 1000);

                            ukupnaMasa2 = ukupnaMasa  + Convert.ToDouble(row.Cells[8].Value.ToString())  + (Convert.ToDouble(row.Cells[9].Value.ToString()) / 1000);
                            TipKontejnera2 = row.Cells[13].Value.ToString();
                        }
                        if (PostojiPrvi == 0)
                        {
                            BrojKontejnera1 = row.Cells[3].Value.ToString();
                            BrojKontejnera2 = "";
                            PostojiPrvi = 1;
                            VrstaRobe1 = row.Cells[14].Value.ToString();
                           // ukupnaMasa = ukupnaMasa + Convert.ToDouble(row.Cells[7].Value.ToString()) + Convert.ToDouble(row.Cells[8].Value.ToString()) + (Convert.ToDouble(row.Cells[9].Value.ToString()) / 1000);

                            ukupnaMasa = ukupnaMasa  + Convert.ToDouble(row.Cells[8].Value.ToString())  + (Convert.ToDouble(row.Cells[9].Value.ToString()) / 1000);
                            TipKontejnera = row.Cells[13].Value.ToString();
                        }
                      

                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }



            Saobracaj.Dokumenta.frmNalogZaPrevoz prevoz = new Saobracaj.Dokumenta.frmNalogZaPrevoz(BrojKontejnera1,BrojKontejnera2,VrstaRobe1,VrstaRobe2,ukupnaMasa, KorisnikCene, TipKontejnera, TipKontejnera2, ukupnaMasa2);
            prevoz.Show();
        }


        private void VratiPodatkeStavkeKontejnerSaPrijemnice(string BrojKontejnera)
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT  Top 1      [Granica],[BrojOsovina] " +
      " ,[SopstvenaMasa],[Tara],[Neto]      ,[Posiljalac],[Primalac],[VlasnikKontejnera] " +
      " ,[TipKontejnera],[VrstaRobe],[Buking]      ,[StatusKontejnera],[BrojPlombe],[PlaniraniLager], " +
        " [Organizator], BrojPlombe, BrojPlombe2 " +
    "  ,[BukingBrodar]  FROM[dbo].[PrijemKontejneraVozStavke] " +
     " where BrojKontejnera = '" + BrojKontejnera.Trim() + "' order by id desc ", con);


            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                txtTara.Value = Convert.ToDecimal(dr["Tara"].ToString());
                txtNeto.Value = Convert.ToDecimal(dr["Neto"].ToString());
                txtGranica.Value = Convert.ToDecimal(dr["Granica"].ToString());
                txtBrojOsovina.Value = Convert.ToDecimal(dr["BrojOsovina"].ToString());
                txtSopstvenaMasa.Value = Convert.ToDecimal(dr["SopstvenaMasa"].ToString());
                cboPosiljalac.SelectedValue = Convert.ToInt32(dr["Posiljalac"].ToString());
                cboPrimalac.SelectedValue = Convert.ToInt32(dr["Primalac"].ToString());
                cboVlasnikKontejnera.SelectedValue = Convert.ToInt32(dr["VlasnikKontejnera"].ToString());
                cboTipKontejnera.SelectedValue = Convert.ToInt32(dr["TipKontejnera"].ToString());
                cboVrstaRobe.SelectedValue = Convert.ToInt32(dr["VrstaRobe"].ToString());
                cboStatusKontejnera.SelectedValue = Convert.ToInt32(dr["StatusKontejnera"].ToString());
                txtBukingBrodar.Text = dr["Buking"].ToString();
                cboOrganizator.SelectedValue = Convert.ToInt32(dr["Organizator"].ToString());
                txtBrojPlombe.Text = dr["BrojPlombe"].ToString();
                txtBrojPlombe2.Text = dr["BrojPlombe2"].ToString();
            }

            con.Close();
        }

        private void VratiPodatkeStavkeVagonSaPrijemnice(string BrojVagona)
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT  Top 1      [Granica],[BrojOsovina] " +
            " ,[SopstvenaMasa] FROM[dbo].[PrijemKontejneraVozStavke] " +
            " where BrojKontejnera = '" + BrojVagona.Trim() + "' order by id desc ", con);


            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                txtGranica.Value = Convert.ToDecimal(dr["Granica"].ToString());
                txtBrojOsovina.Value = Convert.ToDecimal(dr["BrojOsovina"].ToString());
                txtSopstvenaMasa.Value = Convert.ToDecimal(dr["SopstvenaMasa"].ToString());


                con.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            VratiPodatkeStavkeKontejnerSaPrijemnice(txtBrojKontejnera.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            VratiPodatkeStavkeVagonSaPrijemnice(txtVagon.Text);
        }

        private void cboVlasnikKontejnera_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private int VratiPostojiPrijemKontejnera(string Kontejner)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" Select Count(*) as Broj from Promet where BRojKontejnera =  '" + Kontejner + "' and Zatvoren = 0", con);


            SqlDataReader dr = cmd.ExecuteReader();
            int SledeciBroj = 0;
            while (dr.Read())
            {
                SledeciBroj = Convert.ToInt32(dr["Broj"].ToString());

            }
            con.Close();
            return SledeciBroj;
        }

        private int VratiPodatkeMaxPromet()
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" select (Max(PrStDokumenta) + 1) as PrstDokumenta from Promet", con);


            SqlDataReader dr = cmd.ExecuteReader();
            int SledeciBroj = 0;
            while (dr.Read())
            {
                SledeciBroj = Convert.ToInt32(dr["PrstDokumenta"].ToString());

            }
            con.Close();
            return SledeciBroj;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Saobracaj.Dokumenta.frmKontejneriNaPrijemnici kon = new Saobracaj.Dokumenta.frmKontejneriNaPrijemnici(txtBrojKontejnera.Text.Trim());
            kon.Show();
        }

        private void VratiPodatkeTara()
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT Top 1 Tara " +
            " FROM [dbo].[TipKontenjera] " +
            " where ID = " + Convert.ToInt32(cboTipKontejnera.SelectedValue) + " order by id desc ", con);


            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtTara.Value = Convert.ToDecimal(dr["Tara"].ToString());

            }
            con.Close();
        }

        private void cboTipKontejnera_Leave(object sender, EventArgs e)
        {
            VratiPodatkeTara();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (cboStatusPrijema.Text != "2-Primljeno")
            {
                MessageBox.Show("Prijemnica mora biti u statusu Primljeno");
                return;
            }

            DialogResult dr = MessageBox.Show("Da li želite da sačuvate na predefinisano skladište?", "Skladište", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                //Panta
                int SledeciBroj = VratiPodatkeMaxPromet();
                MessageBox.Show("Prijem na centralno skladiste");
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    
                    Saobracaj.Dokumenta.InsertPromet ins = new Saobracaj.Dokumenta.InsertPromet();
                    int pom1 = 0;
                    int pom2 = 0;
                    int pom3 = 1;
                    string s1 = "PRI";
                    string s2 = "PRV";
                    int VecPostoji = VratiPostojiPrijemKontejnera(row.Cells[3].Value.ToString());
                    if (VecPostoji == 0)
                    {
                        ins.InsProm(Convert.ToDateTime(dtpVremeDolaska.Value), s1, SledeciBroj, row.Cells[3].Value.ToString(), s2, pom3, pom2, Convert.ToInt32(frmLogovanje.Skladiste), Convert.ToInt32(frmLogovanje.Lokacija), pom2, pom1, row.Cells[0].Value.ToString(), Convert.ToDateTime(DateTime.Now), KorisnikCene, 0, 0, Convert.ToDateTime(DateTime.Now));

                    }
                    else
                    {
                        MessageBox.Show("Postoji kontejner" + row.Cells[3].Value.ToString() + " koji nije zatvoren ");
                    }
                   
                }
            }
            else
            {
                PrijemNaSkladiste frm = new PrijemNaSkladiste(Convert.ToInt32(txtSifra.Text), Convert.ToDateTime(dtpVremeDolaska.Value), KorisnikCene);
                frm.Show();
            }
        }

        private void cboStatusPrijema_SelectedValueChanged(object sender, EventArgs e)
        {
            

            if (cboStatusPrijema.Text == "2-Primljeno" && usao == 1)
            { 

           // MessageBox.Show("Prijem na Centralno skladište, ATA je podešen");
                dtpVremeDolaska.Value = DateTime.Now;
                int SledeciBroj = VratiPodatkeMaxPromet();
                //  foreach (DataGridViewRow row in dataGridView1.Rows)
                //   {
                //     Saobracaj.Dokumenta.InsertPromet ins = new Saobracaj.Dokumenta.InsertPromet();
                //   int pom1 = 0;
                //   int pom2 = 0;
                //   int pom3 = 1;
                //   string s1 = "PRI";
                //   string s2 = "PRV";
                //   ins.InsProm(Convert.ToDateTime(dtpVremeDolaska.Value), s1, SledeciBroj, row.Cells[3].Value.ToString(), s2, pom3, pom2, 9, 1366, pom2, pom1, row.Cells[0].Value.ToString(), Convert.ToDateTime(DateTime.Now), KorisnikCene, 0, 0, Convert.ToDateTime(DateTime.Now));
            }
      
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            //Tip dokumenta = 1 - Prijemnica,   za 0 je otpremnica
            if (chkVoz.Checked == true)
            {
                Saobracaj.Dokumenta.frmPregledNarucenihManipulacija pnm = new Saobracaj.Dokumenta.frmPregledNarucenihManipulacija(KorisnikCene, Convert.ToInt32(txtSifra.Text), 1,1);
                pnm.Show();
            }
            else
            {
                Saobracaj.Dokumenta.frmPregledNarucenihManipulacija pnm = new Saobracaj.Dokumenta.frmPregledNarucenihManipulacija(KorisnikCene, Convert.ToInt32(txtSifra.Text), 0,1);
                pnm.Show();
            }
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {

            if (chkVoz.Checked == true)
            {
                Saobracaj.Dokumenta.frmManipulacije pnm = new Saobracaj.Dokumenta.frmManipulacije(KorisnikCene, Convert.ToInt32(txtSifra.Text), 1,1);
                pnm.Show();
            }
            else
            {
                Saobracaj.Dokumenta.frmManipulacije pnm = new Saobracaj.Dokumenta.frmManipulacije(KorisnikCene, Convert.ToInt32(txtSifra.Text), 0,1);
                pnm.Show();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }
        private void PostupakRefresh(int stavka)
        {
            var query = "Select PostupakStavkeVeza.ID,IdStavke,RTrim(Naziv) as Naziv From PostupakStavkeVeza inner join VrstePostupakaUvoz on PostupakStavkeVeza.IDPostupka=VrstePostupakaUvoz.ID Where IDStavke=" + stavka;
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = false;
            dataGridView2.DataSource = ds.Tables[0];

            dataGridView2.Columns[0].Width = 40;
            dataGridView2.Columns[1].Width = 50;
            dataGridView2.Columns[1].HeaderText = "Stavka";
            dataGridView2.Columns[2].Width = 100;
        }
        private void btnDodajPostupak_Click(object sender, EventArgs e)
        {
            if (txtStavka.Text == "")
            {
                MessageBox.Show("Mora se izabrati stavka!");
                return;
            }
            else
            {
                InsertPrijemKontejneraVoz ins = new InsertPrijemKontejneraVoz();
                ins.InsertPostupakStavke(Convert.ToInt32(txtStavka.Text), Convert.ToInt32(cboPostupak.SelectedValue));
                PostupakRefresh(Convert.ToInt32(txtStavka.Text));
            }
        }

        private void btnObrisiPostupak_Click(object sender, EventArgs e)
        {
            InsertPrijemKontejneraVoz ins = new InsertPrijemKontejneraVoz();
            foreach(DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Selected)
                {
                    ins.DeletePostupakStavke(Convert.ToInt32(row.Cells[0].Value));
                    PostupakRefresh(Convert.ToInt32(txtStavka.Text));
                }
            }
        }
    }

}


