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
using Saobracaj;

namespace Saobracaj.Dokumenta
{
    public partial class frmPrijemKontejneraKamionLegetUvoz : Form
    {
        MailMessage mailMessage;
        string KorisnikCene;
        bool status = false;
        int usao = 0;
        public string connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public frmPrijemKontejneraKamionLegetUvoz()
        {
            InitializeComponent();
        }

        public frmPrijemKontejneraKamionLegetUvoz(string Korisnik, int Vozom)
        {
            InitializeComponent();
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
                dtpPerodSkladistenjaOd.Enabled = false;
                dtpPeriodSkladistenjaDo.Enabled = false;
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
                dtpPerodSkladistenjaOd.Enabled = false;
                dtpPeriodSkladistenjaDo.Enabled = false;
                // toolStripButton6.Visible = false;
            }

        }

        public frmPrijemKontejneraKamionLegetUvoz(string Korisnik, int Vozom, string NalogID)
        {
            InitializeComponent();
            KorisnikCene = Korisnik;
            txtNalogID.Text = NalogID;
            chkUvoz.Checked = true;

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
                dtpPerodSkladistenjaOd.Enabled = false;
                dtpPeriodSkladistenjaDo.Enabled = false;
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
                dtpPerodSkladistenjaOd.Enabled = false;
                dtpPeriodSkladistenjaDo.Enabled = false;
                // toolStripButton6.Visible = false;
            }
            VratiKontejnerID();

        }
        private void VratiKontejnerID()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select BrojOsnov from RadniNalogInterni where ID = " + txtNalogID.Text, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtKontejnerID.Text = dr["BrojOsnov"].ToString();
            }

            con.Close();

        }

        public frmPrijemKontejneraKamionLegetUvoz(int sifra, string Korisnik, int Vozom)
        {
            InitializeComponent();
            KorisnikCene = Korisnik;
            if (Vozom == 1)
            {
                chkVoz.Checked = true;
                txtImeVozaca.Enabled = false;
                txtRegBrKamiona.Enabled = false;
                this.Text = "Prijem kontejnera vozom";
                dtpPerodSkladistenjaOd.Enabled = false;
                dtpPeriodSkladistenjaDo.Enabled = false;
            }
            else
            {
                chkVoz.Checked = false;
                cboBukingPrijema.Enabled = false;
                this.Text = "Prijem kontejnera kamionom";
                txtVagon.Enabled = false;
                txtGranica.Enabled = false;
                dtpPerodSkladistenjaOd.Enabled = false;
                dtpPeriodSkladistenjaDo.Enabled = false;
                //  toolStripButton6.Visible = false;
            }
            txtSifra.Text = sifra.ToString();
            VratiPodatke(sifra);
            RefreshDataGrid();

        }

        private void FillDG3()
        {

            var select = "  SELECT     UvozKonacnaNHM.ID, NHM.Broj, UvozKonacnaNHM.IDNHM, NHM.Naziv FROM NHM INNER JOIN " +
                      " UvozKonacnaNHM ON NHM.ID = UvozKonacnaNHM.IDNHM where UvozKonacnanhm.idnadredjena = " + Convert.ToInt32(txtKontejnerID.Text) + " order by UvozKonacnanhm.ID desc";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
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

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView3.Columns[0];
            dataGridView3.Columns[0].HeaderText = "ID";
            dataGridView3.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView3.Columns[1];
            dataGridView3.Columns[1].HeaderText = "NHM Broj";
            dataGridView3.Columns[1].Width = 70;

            DataGridViewColumn column3 = dataGridView3.Columns[2];
            dataGridView3.Columns[2].HeaderText = "ID";
            dataGridView3.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView3.Columns[3];
            dataGridView3.Columns[3].HeaderText = "NHM";
            dataGridView3.Columns[3].Width = 150;


        }
        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtSifra.Enabled = false;
            dtpPerodSkladistenjaOd.Value = DateTime.Now;
            dtpPeriodSkladistenjaDo.Value = DateTime.Now;
            dtpDatumPrijema.Value = DateTime.Now;
            dtpDatumPrijema.Enabled = true;
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

        private void tsSave_Click(object sender, EventArgs e)
        {

            int pomDirektni_indirektni = 0;
            
            if (chkVrstaKamiona.Checked == true)
            { 
                pomDirektni_indirektni = 1; 
            }
            else
            {
                pomDirektni_indirektni = 0;
            }

            string sp = cboStatusPrijema.Text;
            int ini = cboStatusPrijema.SelectedIndex;
            int ini2 = Convert.ToInt32(cboStatusPrijema.SelectedValue);
            if (chkVoz.Checked == true)
            {
                //Promene ako je voz
                if (status == true)
                {
                    /// ,  string RegBrKamiona,   string ImeVozaca,   int Vozom
                    Saobracaj.Dokumeta.InsertPrijemKontejneraVoz ins = new Saobracaj.Dokumeta.InsertPrijemKontejneraVoz();
                    ins.InsertPrijemKontVoz(Convert.ToDateTime(dtpDatumPrijema.Text), Convert.ToInt32(cboStatusPrijema.SelectedIndex), Convert.ToInt32(cboBukingPrijema.SelectedValue), Convert.ToDateTime(dtpVremeDolaska.Value), Convert.ToDateTime(DateTime.Now), KorisnikCene, "", "", 1, txtNapomena.Text, Convert.ToInt32(cboPredefinisanePoruke.SelectedValue), 0, pomDirektni_indirektni, 0,0,0);
                    status = false;
                    VratiPodatkeMax();
                }
                else
                {
                    //int TipCenovnika ,int Komitent, double Cena , int VrstaManipulacije ,DateTime  Datum , string Korisnik
                    Saobracaj.Dokumeta.InsertPrijemKontejneraVoz upd = new Saobracaj.Dokumeta.InsertPrijemKontejneraVoz();
                    upd.UpdPrijemKontejneraVoz(Convert.ToInt32(txtSifra.Text), Convert.ToDateTime(dtpDatumPrijema.Text), Convert.ToInt32(cboStatusPrijema.SelectedIndex), Convert.ToInt32(cboBukingPrijema.SelectedValue), Convert.ToDateTime(dtpVremeDolaska.Value), Convert.ToDateTime(DateTime.Now), KorisnikCene, "", "", 1, txtNapomena.Text, Convert.ToInt32(cboPredefinisanePoruke.SelectedValue), 0, pomDirektni_indirektni, 0, 0, 0);
                    status = false;
                }
            }
            else
            {
                if (status == true)
                {
                    /// ,  string RegBrKamiona,   string ImeVozaca,   int Vozom
                    Saobracaj.Dokumeta.InsertPrijemKontejneraVoz ins = new Saobracaj.Dokumeta.InsertPrijemKontejneraVoz();
                    ins.InsertPrijemKontVoz(Convert.ToDateTime(dtpDatumPrijema.Text), Convert.ToInt32(cboStatusPrijema.SelectedIndex), Convert.ToInt32(cboBukingPrijema.SelectedValue), Convert.ToDateTime(dtpVremeDolaska.Value), Convert.ToDateTime(DateTime.Now), KorisnikCene, txtRegBrKamiona.Text, txtImeVozaca.Text, 0, txtNapomena.Text, Convert.ToInt32(cboPredefinisanePoruke.SelectedValue), 0, pomDirektni_indirektni, 0, 0, 0);
                    status = false;
                    VratiPodatkeMax();
                }
                else
                {
                    //int TipCenovnika ,int Komitent, double Cena , int VrstaManipulacije ,DateTime  Datum , string Korisnik
                    Saobracaj.Dokumeta.InsertPrijemKontejneraVoz upd = new Saobracaj.Dokumeta.InsertPrijemKontejneraVoz();
                    upd.UpdPrijemKontejneraVoz(Convert.ToInt32(txtSifra.Text), Convert.ToDateTime(dtpDatumPrijema.Text), Convert.ToInt32(cboStatusPrijema.SelectedIndex), Convert.ToInt32(cboBukingPrijema.SelectedValue), Convert.ToDateTime(dtpVremeDolaska.Value), Convert.ToDateTime(DateTime.Now), KorisnikCene, txtRegBrKamiona.Text, txtImeVozaca.Text, 0, txtNapomena.Text, Convert.ToInt32(cboPredefinisanePoruke.SelectedValue),0, pomDirektni_indirektni, 0, 0, 0);
                    status = false;
                }


            }
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

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Saobracaj.Dokumeta.frmDokumentaPrijemKontejneraKamion frmd3 = new Saobracaj.Dokumeta.frmDokumentaPrijemKontejneraKamion(txtSifra.Text, KorisnikCene);
            frmd3.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (txtStavka.Text.Trim() == "")
            {
                MessageBox.Show("Niste izabrali stavku za koju radite CIR");
                return;
            }
            frmCIR cir = new frmCIR(KorisnikCene, Convert.ToInt32(txtStavka.Text), 0, txtBrojKontejnera.Text, txtVagon.Text, Convert.ToDouble(txtTara.Value), txtRegBrKamiona.Text, Convert.ToDouble(txtNeto.Value), Convert.ToInt32(cboTipKontejnera.SelectedValue), dtpVremeDolaska.Value, txtBrojPlombe.Text, txtBrojPlombe2.Text);
            cir.Show();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
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

                            ukupnaMasa2 = ukupnaMasa + Convert.ToDouble(row.Cells[8].Value.ToString()) + (Convert.ToDouble(row.Cells[9].Value.ToString()) / 1000);
                            TipKontejnera2 = row.Cells[13].Value.ToString();
                        }
                        if (PostojiPrvi == 0)
                        {
                            BrojKontejnera1 = row.Cells[3].Value.ToString();
                            BrojKontejnera2 = "";
                            PostojiPrvi = 1;
                            VrstaRobe1 = row.Cells[14].Value.ToString();
                            // ukupnaMasa = ukupnaMasa + Convert.ToDouble(row.Cells[7].Value.ToString()) + Convert.ToDouble(row.Cells[8].Value.ToString()) + (Convert.ToDouble(row.Cells[9].Value.ToString()) / 1000);

                            ukupnaMasa = ukupnaMasa + Convert.ToDouble(row.Cells[8].Value.ToString()) + (Convert.ToDouble(row.Cells[9].Value.ToString()) / 1000);
                            TipKontejnera = row.Cells[13].Value.ToString();
                        }


                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }



            frmNalogZaPrevoz prevoz = new frmNalogZaPrevoz(BrojKontejnera1, BrojKontejnera2, VrstaRobe1, VrstaRobe2, ukupnaMasa, KorisnikCene, TipKontejnera, TipKontejnera2, ukupnaMasa2);
            prevoz.Show();
        }

        private int VratiPodatkeMaxPromet()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
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

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            int SledeciBroj = VratiPodatkeMaxPromet();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                MessageBox.Show("Prijem na centralno skladiste");
                InsertPromet ins = new InsertPromet();
                int pom1 = 0;
                int pom2 = 0;
                int pom3 = 1;
                string s1 = "PRI";
                string s2 = "PRV";
                ins.InsProm(Convert.ToDateTime(dtpVremeDolaska.Value), s1, SledeciBroj, row.Cells[3].Value.ToString(), s2, pom3, pom2, 1, 2, pom2, pom1, row.Cells[0].Value.ToString(), Convert.ToDateTime(DateTime.Now), KorisnikCene, 0, 0, Convert.ToDateTime(DateTime.Now));



            }
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (chkVoz.Checked == true)
            {
                frmPregledNarucenihManipulacija pnm = new frmPregledNarucenihManipulacija(KorisnikCene, Convert.ToInt32(txtSifra.Text), 1);
                pnm.Show();
            }
            else
            {
                frmPregledNarucenihManipulacija pnm = new frmPregledNarucenihManipulacija(KorisnikCene, Convert.ToInt32(txtSifra.Text), 0);
                pnm.Show();
            }
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (chkVoz.Checked == true)
            {
                frmManipulacije pnm = new frmManipulacije(KorisnikCene, Convert.ToInt32(txtSifra.Text), 1, 1);
                pnm.Show();
            }
            else
            {
                frmManipulacije pnm = new frmManipulacije(KorisnikCene, Convert.ToInt32(txtSifra.Text), 0, 1);
                pnm.Show();
            }
        }


        private void VratiPodatkeStavkeKontejnerSaPrijemnice(string BrojKontejnera)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
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
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
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

        private void button7_Click(object sender, EventArgs e)
        {
            frmKontejneriNaPrijemnici kon = new frmKontejneriNaPrijemnici(txtBrojKontejnera.Text.Trim());
            kon.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            VratiPodatkeStavkeVagonSaPrijemnice(txtVagon.Text);
        }

        private void GetID()
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand("Select MAX(ID) FROM Uvoz", conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    int IDpom = Convert.ToInt32(dr[0].ToString());
                    txtKontejnerID.Text = IDpom.ToString();
                }
            }
        }
        private void OformiKontejnerUUvozu()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connection);
                SqlCommand cmd = new SqlCommand("Insert into Uvoz Default Values", conn);
                conn.Open();
                var q = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            GetID();
        }

        private void UbaciPodatkeOKontejneru()
        {
            Saobracaj.Uvoz.InsertUvoz ins = new Saobracaj.Uvoz.InsertUvoz();
            ins.UpdUvoz(Convert.ToInt32(txtKontejnerID.Text), Convert.ToDateTime(DateTime.Now),
                Convert.ToDateTime(DateTime.Now), "TERMINAL", txtBrojKontejnera.Text,
                Convert.ToInt32(cboTipKontejnera.SelectedValue), Convert.ToDateTime(DateTime.Now), "",
                txtNapomena.Text.ToString().TrimEnd(), "", Convert.ToInt32(cboStatusKontejnera.SelectedValue), Convert.ToInt32(0),
                "", Convert.ToInt32(0), Convert.ToInt32(cboVlasnikKontejnera.SelectedValue), Convert.ToInt32(0), "",
                "", Convert.ToInt32(cboVlasnikKontejnera.SelectedValue), Convert.ToInt32(0), "", 
                Convert.ToInt32(0),
                Convert.ToInt32(0), Convert.ToInt32(0),
                Convert.ToInt32(cbPostupak.SelectedValue),
                Convert.ToInt32(0), Convert.ToInt32(0), 
                Convert.ToInt32(0),
                Convert.ToInt32(0), Convert.ToInt32(0),
                "", txtBrojPlombe.Text,
                txtBrojPlombe2.Text, Convert.ToDecimal(txtNeto.Value), Convert.ToDecimal(bttoRobe.Value),
                Convert.ToDecimal(txtTara.Value),
                Convert.ToDecimal(bttoKontejnera.Value), Convert.ToInt32(0), 
                Convert.ToDateTime(DateTime.Now),
                Convert.ToInt32(0), "",
                Convert.ToDateTime(DateTime.Now), Convert.ToDecimal(0), 
                Convert.ToInt32(0), txtNapomenaS.Text, Convert.ToInt32(0),
                Convert.ToInt32(cboOrganizator.SelectedValue), "",
                Convert.ToInt32(cboPosiljalac.SelectedValue), "",
                Convert.ToInt32(cboPrimalac.SelectedValue), "", Convert.ToInt32(cboVlasnikKontejnera.SelectedValue),
                "", 0, 0, Convert.ToInt32(0),
                "");


        }

        private void UbaciUsluguKontejnera()
        {
            Saobracaj.Uvoz.InsertUvozKonacna uvK = new Saobracaj.Uvoz.InsertUvozKonacna();
            uvK.InsUbaciUsluguKonacna(Convert.ToInt32(txtKontejnerID.Text), 69, 0, 1, 4, Convert.ToInt32(cboVlasnikKontejnera.SelectedValue), 0, "GATE IN EMPTY", 6, KorisnikCene);

        }

        private void PrebaciUUvozKonacna()
        {
            Saobracaj.Uvoz.InsertUvozKonacna ins = new Saobracaj.Uvoz.InsertUvozKonacna();
            ins.PrenesiUPlanUtovara(Convert.ToInt32(txtKontejnerID.Text), Convert.ToInt32(cboPlanUtovara.SelectedValue));
        }

        private void OformiRadniNalogInterni()
        {
            Saobracaj.Uvoz.InsertRadniNalogInterni ins = new Saobracaj.Uvoz.InsertRadniNalogInterni();
            ins.InsRadniNalogInterni2(Convert.ToInt32(4), Convert.ToInt32(4), Convert.ToDateTime(DateTime.Now), Convert.ToDateTime("1.1.1900. 00:00:00"), "", Convert.ToInt32(0), "PlanUtovaraT", Convert.ToInt32(txtKontejnerID.Text), KorisnikCene, "");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtSifra.Text == "")
            {
                MessageBox.Show("Niste uneli zaglavlje");
            }
            else if (chkTerminal.Checked == true)
            {
                OformiKontejnerUUvozu();
                UbaciPodatkeOKontejneru();
                PrebaciUUvozKonacna();
                UbaciUsluguKontejnera();
                OformiRadniNalogInterni();
                
                Saobracaj.Dokumeta.InsertPrijemKontejneraVozStavke ins = new Saobracaj.Dokumeta.InsertPrijemKontejneraVozStavke();
                ins.InsertPrijemKontVozStavke(Convert.ToInt32(txtSifra.Text), txtBrojKontejnera.Text, txtVagon.Text, Convert.ToDouble(txtGranica.Value), Convert.ToDouble(txtBrojOsovina.Value), Convert.ToDouble(txtSopstvenaMasa.Value), Convert.ToDouble(txtTara.Value), Convert.ToDouble(txtNeto.Value), Convert.ToInt32(cboPosiljalac.SelectedValue), Convert.ToInt32(cboPrimalac.SelectedValue), Convert.ToInt32(cboVlasnikKontejnera.SelectedValue), Convert.ToInt32(cboTipKontejnera.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue), Convert.ToInt32(cboBukingOtpreme.SelectedValue), Convert.ToInt32(cboStatusKontejnera.SelectedValue), txtBrojPlombe.Text, Convert.ToInt32(txtPlaniraniLager.Text), Convert.ToInt32(cboBukingOtpreme.SelectedValue), Convert.ToDateTime(dtpVremeDolaska.Value), Convert.ToDateTime(dtpDatumPrijema.Value), Convert.ToDateTime(dtpPeriodSkladistenjaDo.Value), Convert.ToDateTime(DateTime.Now), KorisnikCene, txtBrojPlombe2.Text, Convert.ToInt32(cboOrganizator.SelectedValue), txtBukingBrodar.Text, txtNapomenaS.Text, Convert.ToDateTime(dtpPerodSkladistenjaOd.Value), Convert.ToDateTime(dtpPeriodSkladistenjaDo.Value), Convert.ToDouble(bttoRobe.Value), Convert.ToInt32(txtKontejnerID.Text), Convert.ToDouble(bttoKontejnera.Value), txtNapomenaS2.Text, Convert.ToInt32(cbPostupak.SelectedValue), 0,0,"", "", "", Convert.ToInt32(txtNalogID.Text));
                RefreshDataGrid();
            }
            else
            {
                Saobracaj.Dokumeta.InsertPrijemKontejneraVozStavke ins = new Saobracaj.Dokumeta.InsertPrijemKontejneraVozStavke();
                ins.InsertPrijemKontVozStavke(Convert.ToInt32(txtSifra.Text), txtBrojKontejnera.Text, txtVagon.Text, Convert.ToDouble(txtGranica.Value), Convert.ToDouble(txtBrojOsovina.Value), Convert.ToDouble(txtSopstvenaMasa.Value), Convert.ToDouble(txtTara.Value), Convert.ToDouble(txtNeto.Value), Convert.ToInt32(cboPosiljalac.SelectedValue), Convert.ToInt32(cboPrimalac.SelectedValue), Convert.ToInt32(cboVlasnikKontejnera.SelectedValue), Convert.ToInt32(cboTipKontejnera.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue), Convert.ToInt32(cboBukingOtpreme.SelectedValue), Convert.ToInt32(cboStatusKontejnera.SelectedValue), txtBrojPlombe.Text, Convert.ToInt32(txtPlaniraniLager.Text), Convert.ToInt32(cboBukingOtpreme.SelectedValue), Convert.ToDateTime(dtpVremeDolaska.Value), Convert.ToDateTime(dtpDatumPrijema.Value), Convert.ToDateTime(dtpPeriodSkladistenjaDo.Value), Convert.ToDateTime(DateTime.Now), KorisnikCene, txtBrojPlombe2.Text, Convert.ToInt32(cboOrganizator.SelectedValue), txtBukingBrodar.Text, txtNapomenaS.Text, Convert.ToDateTime(dtpPerodSkladistenjaOd.Value), Convert.ToDateTime(dtpPeriodSkladistenjaDo.Value), Convert.ToDouble(bttoRobe.Value), Convert.ToInt32(txtKontejnerID.Text), Convert.ToDouble(bttoKontejnera.Value), txtNapomenaS2.Text, Convert.ToInt32(cbPostupak.SelectedValue), 0, 0, "", "", "", Convert.ToInt32(txtNalogID.Text));
                RefreshDataGrid();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Saobracaj.Dokumeta.InsertPrijemKontejneraVozStavke ins = new Saobracaj.Dokumeta.InsertPrijemKontejneraVozStavke();
            ins.UpdPrijemKontejneraVozStavke(Convert.ToInt32(txtStavka.Text), Convert.ToInt32(txtSifra.Text), txtBrojKontejnera.Text, txtVagon.Text, Convert.ToDouble(txtGranica.Value), Convert.ToDouble(txtBrojOsovina.Value), Convert.ToDouble(txtSopstvenaMasa.Value), Convert.ToDouble(txtTara.Value), Convert.ToDouble(txtNeto.Value), Convert.ToInt32(cboPosiljalac.SelectedValue), Convert.ToInt32(cboPrimalac.SelectedValue), Convert.ToInt32(cboVlasnikKontejnera.SelectedValue), Convert.ToInt32(cboTipKontejnera.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue), Convert.ToInt32(cboBukingOtpreme.SelectedValue), Convert.ToInt32(cboStatusKontejnera.SelectedValue), txtBrojPlombe.Text, Convert.ToInt32(txtPlaniraniLager.Text), Convert.ToInt32(cboBukingOtpreme.SelectedValue), Convert.ToDateTime(dtpVremeDolaska.Value), Convert.ToDateTime(dtpDatumPrijema.Value), Convert.ToDateTime(dtpPeriodSkladistenjaDo.Value), Convert.ToDateTime(DateTime.Now), KorisnikCene, Convert.ToInt32(txtRB.Text), txtBrojPlombe2.Text, Convert.ToInt32(cboOrganizator.SelectedValue), txtBukingBrodar.Text, txtNapomenaS.Text, Convert.ToDateTime(dtpPerodSkladistenjaOd.Value), Convert.ToDateTime(dtpPeriodSkladistenjaDo.Value), Convert.ToDouble(bttoRobe.Value), Convert.ToInt32(txtKontejnerID.Text), Convert.ToDouble(bttoKontejnera.Value), txtNapomenaS2.Text, Convert.ToInt32(cbPostupak.SelectedValue), 0, 0, "0", "0", "0");
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

            //Panta refresh
            /*

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
            dataGridView1.Columns[4].HeaderText = "Broj Vagona";
            dataGridView1.Columns[4].Width = 110;

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

        private void VratiPodatke(int ID)
        {

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            //VR SqlCommand cmd = new SqlCommand("select [ID] ,[DatumPrijema],[StatusPrijema],[IdVoza],[VremeDolaska],RegBrKamiona, ImeVozaca, NajavaEmail, PrijemEmail, Napomena, CIRUradjen, PredefinisanaPorukaID from PrijemKontejneraVoz where ID=" + ID, con);

            SqlCommand cmd = new SqlCommand("select [ID] ,[DatumPrijema],[StatusPrijema],[IdVoza],[VremeDolaska],RegBrKamiona, ImeVozaca, NajavaEmail, PrijemEmail, Napomena, CIRUradjen from PrijemKontejneraVoz where ID=" + ID, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                dtpDatumPrijema.Value = Convert.ToDateTime(dr["DatumPrijema"].ToString());
                dtpVremeDolaska.Value = Convert.ToDateTime(dr["VremeDolaska"].ToString());
                cboBukingPrijema.SelectedValue = Convert.ToInt32(dr["IDVoza"].ToString());
                cboStatusPrijema.SelectedIndex = Convert.ToInt32(dr["StatusPrijema"].ToString());
                txtRegBrKamiona.Text = dr["RegBrKamiona"].ToString();
                //VR cboPredefinisanePoruke.SelectedValue = Convert.ToInt32(dr["PredefinisanePorukeID"].ToString());
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

        private void frmPrijemKontejneraKamionLegetUvoz_Load(object sender, EventArgs e)
        {
            var select = " Select Distinct ID, (Broj + '-' + Naziv) as NHM  From NHM";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboVrstaRobe.DataSource = ds.Tables[0];
            cboVrstaRobe.DisplayMember = "NHM";
            cboVrstaRobe.ValueMember = "ID";

            //where Posiljalac = 1
            var select1 = " Select Distinct PaSifra as ID, PaNaziv as Naziv From Partnerji  order by PaNaziv";
            var s_connection1 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
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
            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
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
            var s_connection3 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboVlasnikKontejnera.DataSource = ds3.Tables[0];
            cboVlasnikKontejnera.DisplayMember = "Naziv";
            cboVlasnikKontejnera.ValueMember = "ID";


            var select4 = " Select Distinct ID, Naziv From TipKontenjera order by Naziv";
            var s_connection4 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection4 = new SqlConnection(s_connection4);
            var c4 = new SqlConnection(s_connection4);
            var dataAdapter4 = new SqlDataAdapter(select4, c4);

            var commandBuilder4 = new SqlCommandBuilder(dataAdapter4);
            var ds4 = new DataSet();
            dataAdapter4.Fill(ds4);
            cboTipKontejnera.DataSource = ds4.Tables[0];
            cboTipKontejnera.DisplayMember = "Naziv";
            cboTipKontejnera.ValueMember = "ID";

            var select5 = " Select Distinct ID, (Cast(BrVoza as nvarchar(6)) + '-' + Relacija + '-' + Cast(Cast(VremePolaskaO as DateTime) as Nvarchar(12))) as IDVoza From Voz where dolazeci = 0";
            var s_connection5 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection5 = new SqlConnection(s_connection5);
            var c5 = new SqlConnection(s_connection5);
            var dataAdapter5 = new SqlDataAdapter(select5, c5);

            var commandBuilder5 = new SqlCommandBuilder(dataAdapter5);
            var ds5 = new DataSet();
            dataAdapter5.Fill(ds5);
            cboBukingOtpreme.DataSource = ds5.Tables[0];
            cboBukingOtpreme.DisplayMember = "IdVoza";
            cboBukingOtpreme.ValueMember = "ID";

            /*
            var dir = "Select ID,Naziv from DirigacijaKontejneraZa order by ID";
            var dirAD = new SqlDataAdapter(dir, conn);
            var dirDS = new DataSet();
            dirAD.Fill(dirDS);
            cbDirigacija.DataSource = dirDS.Tables[0];
            cbDirigacija.DisplayMember = "Naziv";
            cbDirigacija.ValueMember = "ID";
            */


            var select6 = " Select ID,Naziv from DirigacijaKontejneraZa order by ID ";
            var s_connection6 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
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
            var s_connection7 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
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
            var s_connection8 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
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
            var select9 = " Select Distinct PaSifra as ID, PaNaziv as Naziv From Partnerji  order by PaNaziv";
            var s_connection9 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection9 = new SqlConnection(s_connection9);
            var c9 = new SqlConnection(s_connection9);
            var dataAdapter9 = new SqlDataAdapter(select9, c9);

            var commandBuilder9 = new SqlCommandBuilder(dataAdapter9);
            var ds9 = new DataSet();
            dataAdapter9.Fill(ds9);
            cboOrganizator.DataSource = ds9.Tables[0];
            cboOrganizator.DisplayMember = "Naziv";
            cboOrganizator.ValueMember = "ID";


            var select10 = " Select Distinct ID, Naziv  From PredefinisanePoruke order by ID";
            var s_connection10 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection10 = new SqlConnection(s_connection10);
            var c10 = new SqlConnection(s_connection10);
            var dataAdapter10 = new SqlDataAdapter(select10, c10);

            var commandBuilder10 = new SqlCommandBuilder(dataAdapter10);
            var ds10 = new DataSet();
            dataAdapter10.Fill(ds10);
            cboPredefinisanePoruke.DataSource = ds10.Tables[0];
            cboPredefinisanePoruke.DisplayMember = "Naziv";
            cboPredefinisanePoruke.ValueMember = "ID";

            var dir3 = "Select ID,Naziv from VrstePostupakaUvoz order by ID";
            var dirAD3 = new SqlDataAdapter(dir3, s_connection);
            var dirDS3 = new DataSet();
            dirAD3.Fill(dirDS3);
            cbPostupak.DataSource = dirDS3.Tables[0];
            cbPostupak.DisplayMember = "Naziv";
            cbPostupak.ValueMember = "ID";

            usao = 1;
        }

        private void VratiPodatkeStavke(string IdNadredjenog, int RB)
        {
            int KontejnerID = 0;
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT [ID],[IDNadredjenog]       ,[BrojKontejnera],[BrojVagona] " +
             " ,[Granica],[BrojOsovina],[SopstvenaMasa],[Tara] " +
             " ,[Neto],[Posiljalac],[Primalac],[VlasnikKontejnera] " +
             " ,[TipKontejnera],[VrstaRobe],[Buking],[StatusKontejnera] " +
             " ,[BrojPlombe],[PlaniraniLager],[IdVoza],[VremeDolaska] " +
             " ,[VremePripremljen],[VremeOdlaska],[Datum],[Korisnik] " +
             " ,[RB],[BrojPlombe2],[Organizator], BukingBrodar, NapomenaS, NapomenaSS " +
             "  ,[PeriodSkladistenjaOd]      ,[PeriodSkladistenjaDo]      ,[BTTORobe] " +
           "  ,[KontejnerID]      ,[BTTOKOntejnera]      ,[Napomena2]      ,[PostupakSaRobom]" +
             " FROM [dbo].[PrijemKontejneraVozStavke] " +
             " where IdNadredjenog = " + txtSifra.Text + " and RB = " + RB, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {


                txtKontejnerID.Text = dr["KontejnerID"].ToString();
                txtStavka.Text = dr["ID"].ToString();
                txtRB.Text = dr["RB"].ToString();
                txtBrojKontejnera.Text = dr["BrojKontejnera"].ToString();
                txtVagon.Text = dr["BrojVagona"].ToString();
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
                cboStatusKontejnera.SelectedValue = Convert.ToInt32(dr["StatusKontejnera"].ToString()); //DirigacijaKontejneraZa
                cboBukingOtpreme.SelectedValue = Convert.ToInt32(dr["Buking"].ToString());
                cboOrganizator.SelectedValue = Convert.ToInt32(dr["Organizator"].ToString());
                txtBukingBrodar.Text = dr["BukingBrodar"].ToString();
                dtpVremeDolaska.Value = Convert.ToDateTime(dr["VremeDolaska"].ToString());
                dtpPerodSkladistenjaOd.Value = Convert.ToDateTime(dr["PeriodSkladistenjaOd"].ToString());
                dtpPeriodSkladistenjaDo.Value = Convert.ToDateTime(dr["PeriodSkladistenjaDo"].ToString());
                txtKontejnerID.Text = dr["KontejnerID"].ToString();
                txtBrojPlombe.Text = dr["BrojPlombe"].ToString();
                txtBrojPlombe2.Text = dr["BrojPlombe2"].ToString();
                //Napomena
                txtNapomenaS.Text = dr["NapomenaS"].ToString();
                bttoRobe.Value = Convert.ToDecimal(dr["BTTORobe"].ToString());
                bttoKontejnera.Value = Convert.ToDecimal(dr["BTTOKOntejnera"].ToString());
                txtNapomenaS2.Text = dr["Napomena2"].ToString();
                cbPostupak.SelectedValue = Convert.ToInt32(dr["PostupakSaRobom"].ToString());
               // KontejnerID = Convert.ToInt32(dr["KontejnerID"].ToString()); // PostupakSaRobom
                
            }

            con.Close();

           // VratiPodatkeKontejnerID(KontejnerID);
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
                        PopuniPolja();
                        VratiPodatkeStavke(txtSifra.Text, Convert.ToInt32(row.Cells[1].Value.ToString()));
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void prijemKontejneraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string IDUsluge = "0";
            foreach (DataGridViewRow row in dataGridView8.Rows)
            {

                if (row.Selected == true)
                {
                    IDUsluge = row.Cells[0].Value.ToString();

                }
            }

            if (IDUsluge == "0")

            {
                DialogResult dialogResult = MessageBox.Show("Niste obeležili ni jednu uslugu, da li nastavljate dalje", "Usluga?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    RadniNalozi.RN4PrijemPlatforme ppl = new RadniNalozi.RN4PrijemPlatforme(txtSifra.Text, txtRegBrKamiona.Text, KorisnikCene, IDUsluge, 0);
                    ppl.Show();
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }

            }
            else
            {
                RadniNalozi.RN4PrijemPlatforme ppl = new RadniNalozi.RN4PrijemPlatforme(txtSifra.Text, txtRegBrKamiona.Text, KorisnikCene, IDUsluge,0);
                ppl.Show();
            }
        }

        private void pRIJEMCIRADEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RadniNalozi.RN9PrijemCirade pc = new RadniNalozi.RN9PrijemCirade(txtSifra.Text, txtRegBrKamiona.Text, KorisnikCene);
            pc.Show();
        }

        private void pREGLEDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RadniNalozi.RN10PregledIspraznjenogKontejnera rni = new RadniNalozi.RN10PregledIspraznjenogKontejnera(txtSifra.Text, Convert.ToInt32(cboBukingPrijema.SelectedValue), KorisnikCene, txtSifra.Text, txtRegBrKamiona.Text);
            rni.Show();
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }
        private void FillDGUslugeIzIzVozaNeraspoeredjeni()
        {

            if (txtKontejnerID.Text == "")
            {
                return;
            }
            var select = "";

            select = "select  IzvozVrstaManipulacije.ID as ID, IzvozVrstaManipulacije.IDNadredjena as KontejnerID, Izvoz.BrojKontejnera, " +
" IzvozVrstaManipulacije.Kolicina,  VrstaManipulacije.ID as ManipulacijaID,VrstaManipulacije.Naziv as ManipulacijaNaziv, " +
" IzvozVrstaManipulacije.Cena,OrganizacioneJedinice.ID,   OrganizacioneJedinice.Naziv as OrganizacionaJedinica,  " +
" Partnerji.PaSifra as NalogodavacID,PArtnerji.PaNaziv as Platilac " +
" from IzvozVrstaManipulacije " +
" Inner    join VrstaManipulacije on VrstaManipulacije.ID = IzvozVrstaManipulacije.IDVrstaManipulacije " +
" inner " +
" join PArtnerji on IzvozVrstaManipulacije.Platilac = PArtnerji.PaSifra " +
" inner " +
" join OrganizacioneJedinice on OrganizacioneJedinice.ID = IzvozVrstaManipulacije.OrgJed " +
" inner " +
" join Izvoz on IzvozVrstaManipulacije.IDNadredjena = Izvoz.ID where Izvoz.ID = " + Convert.ToInt32(txtKontejnerID.Text);



            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView5.ReadOnly = true;
            dataGridView5.DataSource = ds.Tables[0];


            dataGridView5.BorderStyle = BorderStyle.None;
            dataGridView5.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView5.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView5.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView5.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView5.BackgroundColor = Color.White;

            dataGridView5.EnableHeadersVisualStyles = false;
            dataGridView5.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView5.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView5.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView5.Columns[0];
            dataGridView5.Columns[0].HeaderText = "ID";
            dataGridView5.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView5.Columns[1];
            dataGridView5.Columns[1].HeaderText = "IDU";
            dataGridView5.Columns[1].Width = 30;

            DataGridViewColumn column3 = dataGridView5.Columns[2];
            dataGridView5.Columns[2].HeaderText = "Kontejner";
            dataGridView5.Columns[2].Width = 50;

        }
        private void txtKontejnerID_TextChanged(object sender, EventArgs e)
        {
            if (chkUvoz.Checked == true)
            {
                FillDGUsluge();
                PopuniPolja();
                FillDG3();

            }
            else if (chkTerminal.Checked == true)
            {
                FillDGUsluge();
                PopuniPolja();
                FillDG3();

            }
            else
            {
                FillDGUslugeIzIzVozaNeraspoeredjeni();
            }
            
        }


        private void PopuniPolja()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT PrijemKontejneraVozStavke.ID,  PrijemKontejneraVozStavke.KontejnerID, " +
" PrijemKontejneraVozStavke.BrojKontejnera,  TipKontenjera.ID AS TipKontejnera, PrijemKontejneraVozStavke.Granica as GranicaTovarenja, " +
" PrijemKontejneraVozStavke.BrojOsovina,  PrijemKontejneraVozStavke.SopstvenaMasa as TaraVagona,  PrijemKontejneraVozStavke.Tara,  " +
"PrijemKontejneraVozStavke.Neto, " +
" PrijemKontejneraVozStavke.BTTORobe, PrijemKontejneraVozStavke.BTTOKontejnera, Partnerji_1.PaSifra as Nalogodavac_ZA_VOZ , " +
" Partnerji_4.PaSifra as Brodar, UvozKonacna.BukingBrodara AS BukingBrodar,PrijemKontejneraVozStavke.BrojPlombe, " +
" PrijemKontejneraVozStavke.BrojPlombe2, PrijemKontejneraVozStavke.PeriodSkladistenjaOd, " +
" PrijemKontejneraVozStavke.PeriodSkladistenjaDo, DirigacijaKOntejneraZa.ID as DirigacijaKOntejneraZa, VrstePostupakaUvoz.ID as PostupakSaRobom," +
" Partnerji_2.PaSifra AS Nalogodavac_Za_DrumskiPrevoz, Partnerji_3.PaSifra AS Nalogodavac_Za_Usluge," +
 "  PrijemKontejneraVozStavke.PlaniraniLager as DIREKTNI_INDIREKTNI,    PrijemKontejneraVozStavke.NapomenaS, PrijemKontejneraVozStavke.Napomena2, " +
" PrijemKontejneraVozStavke.Datum, PrijemKontejneraVozStavke.Korisnik " +
" FROM  PrijemKontejneraVozStavke " +
" inner join PrijemKontejneraVoz on PrijemKontejneraVoz.ID = PrijemKontejneraVozStavke.IdNadredjenog" + 
" inner join UvozKonacna on UvozKonacna.ID = PrijemKontejneraVozStavke.KontejnerID " +
" INNER JOIN  Partnerji AS Partnerji_1 ON UvozKonacna.Nalogodavac1 = Partnerji_1.PaSifra " +
" INNER JOIN  Partnerji AS Partnerji_2 ON UvozKonacna.Nalogodavac2 = Partnerji_2.PaSifra " +
" INNER JOIN  Partnerji AS Partnerji_3 ON UvozKonacna.Nalogodavac3 = Partnerji_3.PaSifra " +
" INNER JOIN  Partnerji AS Partnerji_4 ON UvozKonacna.Brodar = Partnerji_4.PaSifra " +
" INNER JOIN TipKontenjera ON PrijemKontejneraVozStavke.TipKontejnera = TipKontenjera.ID " +
" INNER join DirigacijaKontejneraZa on DirigacijaKontejneraZa.ID = PrijemKontejneraVozStavke.StatusKontejnera " +
" INNER JOIN  Voz ON PrijemKontejneraVozStavke.IdVoza = Voz.ID " +
" INNER JOIN VrstePostupakaUvoz ON VrstePostupakaUvoz.id = PrijemKontejneraVozStavke.PostupakSaRobom " +
             " where PrijemKontejneraVozStavke.KontejnerID = " + Convert.ToInt32(txtKontejnerID.Text)  + " and PrijemKontejneraVoz.Vozom = 1", con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtStavka.Text = "";
                txtRB.Text = "";
                txtBrojKontejnera.Text = dr["BrojKontejnera"].ToString();
                txtVagon.Text = "";
                txtTara.Value = Convert.ToDecimal(dr["Tara"].ToString());
                txtNeto.Value = Convert.ToDecimal(dr["Neto"].ToString());
                txtGranica.Value = Convert.ToDecimal(dr["GranicaTovarenja"].ToString());
                txtBrojOsovina.Value = Convert.ToDecimal(dr["BrojOsovina"].ToString());
                txtSopstvenaMasa.Value = Convert.ToDecimal(dr["TaraVagona"].ToString());
                cboOrganizator.SelectedValue = Convert.ToInt32(dr["Nalogodavac_ZA_VOZ"].ToString());
                cboPosiljalac.SelectedValue = Convert.ToInt32(dr["Nalogodavac_Za_Usluge"].ToString());
                cboPrimalac.SelectedValue = Convert.ToInt32(dr["Nalogodavac_Za_DrumskiPrevoz"].ToString());
               
                cboVlasnikKontejnera.SelectedValue = Convert.ToInt32(dr["Brodar"].ToString());
                cboTipKontejnera.SelectedValue = Convert.ToInt32(dr["TipKontejnera"].ToString());
                cbPostupak.SelectedValue = Convert.ToInt32(dr["PostupakSaRobom"].ToString());
                // cboVrstaRobe.SelectedValue = Convert.ToInt32(dr["VrstaRobe"].ToString());
              cboStatusKontejnera.SelectedValue = Convert.ToInt32(dr["DirigacijaKontejneraZa"].ToString()); //DirigacijaKontejneraZa
                //  cboBukingOtpreme.SelectedValue = Convert.ToInt32(dr["Buking"].ToString());
                // cboOrganizator.SelectedValue = Convert.ToInt32(dr["Organizator"].ToString());
                txtBukingBrodar.Text = dr["BukingBrodar"].ToString();
                //dtpVremeDolaska.Value = Convert.ToDateTime(dr["VremeDolaska"].ToString());
                dtpPerodSkladistenjaOd.Value = Convert.ToDateTime(dr["PeriodSkladistenjaOd"].ToString());
                dtpPeriodSkladistenjaDo.Value = Convert.ToDateTime(dr["PeriodSkladistenjaDo"].ToString());
                txtPlaniraniLager.Text = dr["DIREKTNI_INDIREKTNI"].ToString(); 
                txtBrojPlombe.Text = dr["BrojPlombe"].ToString();
                txtBrojPlombe2.Text = dr["BrojPlombe2"].ToString();
                //Napomena
                txtNapomenaS.Text = dr["NapomenaS"].ToString();
                bttoRobe.Value = Convert.ToDecimal(dr["BTTORobe"].ToString());
                bttoKontejnera.Value = Convert.ToDecimal(dr["BTTOKOntejnera"].ToString());
                txtNapomenaS2.Text = dr["Napomena2"].ToString();
                cbPostupak.SelectedValue = Convert.ToInt32(dr["PostupakSaRobom"].ToString());
               // txtKontejnerID.Text = dr["KontejnerID"].ToString(); ;// PostupakSaRobom
            }

            con.Close();

        }

        private void FillDGUsluge()
        {


            if (txtKontejnerID.Text == "")
            {
                return;
            }
            var select = "";

            select = "select  RadniNalogInterni.ID as RNID, RadniNalogInterni.StatusIzdavanja, UvozKonacnaVrstaManipulacije.ID as ID, " +
" UvozKonacna.BrojKontejnera, UvozKonacnaVrstaManipulacije.IDNadredjena as KontejnerID,  UvozKonacnaVrstaManipulacije.Kolicina, " +
" VrstaManipulacije.ID as ManipulacijaID,VrstaManipulacije.Naziv as ManipulacijaNaziv,  OrganizacioneJedinice.ID, " +
" OrganizacioneJedinice.Naziv as OrganizacionaJedinica,  RadniNalogInterni.DatumIzdavanja, RadniNalogInterni.Uradjen, Partnerji.PaNaziv as Nalogodavac, " +
" RadniNalogInterni.Pokret, RadniNalogInterni.StatusKontejnera,KontejnerStatus.Naziv " +
" from RadniNalogInterni " +
" Inner  join VrstaManipulacije on VrstaManipulacije.ID = RadniNalogInterni.IDManipulacijaJED " +
" inner join UvozKonacnaVrstaManipulacije on UvozKonacnaVrstaManipulacije.ID = RadniNalogInterni.KonkretaIDUsluge " +
" inner join UvozKonacna on UvozKonacna.ID = RadniNalogInterni.BrojOsnov " +
" inner join PArtnerji on UvozKonacnaVrstaManipulacije.Platilac = PArtnerji.PaSifra " +
" inner join OrganizacioneJedinice on OrganizacioneJedinice.ID = RadniNalogInterni.OjIzdavanja " +
" inner join KontejnerStatus on KontejnerStatus.ID = RadniNalogInterni.StatusKontejnera where RadniNalogInterni.BrojOsnov = " + Convert.ToInt32(txtKontejnerID.Text);



            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView8.ReadOnly = true;
            dataGridView8.DataSource = ds.Tables[0];


            dataGridView8.BorderStyle = BorderStyle.None;
            dataGridView8.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView8.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView8.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView8.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView8.BackgroundColor = Color.White;

            dataGridView8.EnableHeadersVisualStyles = false;
            dataGridView8.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView8.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView8.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView8.Columns[0];
            dataGridView8.Columns[0].HeaderText = "RNID";
            dataGridView8.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView8.Columns[1];
            dataGridView8.Columns[1].HeaderText = "STATUS";
            dataGridView8.Columns[1].Width = 70;

            DataGridViewColumn column3 = dataGridView8.Columns[2];
            dataGridView8.Columns[2].HeaderText = "VID";
            dataGridView8.Columns[2].Visible = false;
            dataGridView8.Columns[2].Width = 70;

            DataGridViewColumn column4 = dataGridView8.Columns[3];
            dataGridView8.Columns[3].HeaderText = "MID";
            dataGridView8.Columns[3].Width = 50;

            DataGridViewColumn column5 = dataGridView8.Columns[4];
            dataGridView8.Columns[4].HeaderText = "V";
            dataGridView8.Columns[4].Width = 30;

            DataGridViewColumn column6 = dataGridView8.Columns[5];
            dataGridView8.Columns[5].HeaderText = "KOL";
            dataGridView8.Columns[5].Width = 30;

            DataGridViewColumn column7 = dataGridView8.Columns[6];
            dataGridView8.Columns[6].HeaderText = "VUID";
            dataGridView8.Columns[6].Width = 30;

            DataGridViewColumn column8 = dataGridView8.Columns[7];
            dataGridView8.Columns[7].HeaderText = "USLUGA";
            dataGridView8.Columns[7].Width = 170;

        }

        private void pRIJEMPLATFORMEBRODARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string IDUsluge = "0";
            foreach (DataGridViewRow row in dataGridView8.Rows)
            {

                if (row.Selected == true)
                {
                    IDUsluge = row.Cells[0].Value.ToString();

                }
            }

            if (IDUsluge == "0")

            {
                DialogResult dialogResult = MessageBox.Show("Niste obeležili ni jednu uslugu, da li nastavljate dalje", "Usluga?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    // 0 - Uvoz kao modul - ovde je direktno Terminal
                    RadniNalozi.RN5PrijemPlatforme2 ppl = new RadniNalozi.RN5PrijemPlatforme2(txtSifra.Text, txtRegBrKamiona.Text, KorisnikCene, IDUsluge, 0);
                    ppl.Show();
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }

            }
            else
            {
                RadniNalozi.RN5PrijemPlatforme2 ppl = new RadniNalozi.RN5PrijemPlatforme2(txtSifra.Text, txtRegBrKamiona.Text, KorisnikCene, IDUsluge, 0);
                ppl.Show();
            }


         //   RadniNalozi.RN5PrijemPlatforme2 ppl = new RadniNalozi.RN5PrijemPlatforme2(txtSifra.Text, txtRegBrKamiona.Text, KorisnikCene);
           // ppl.Show();
        }

        private void dataGridView8_SelectionChanged(object sender, EventArgs e)
        {
            string IDUsluge = "0";
            foreach (DataGridViewRow row in dataGridView8.Rows)
            {

                if (row.Selected == true)
                {
                    IDUsluge = row.Cells[0].Value.ToString();
                    txtNalogID.Text = IDUsluge;
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTerminal.Checked == true)
            {
                chkUvoz.Checked = false;
                cboVlasnikKontejnera.Enabled = true;
                panel2.Visible = true;//Brodar
            }

            var planutovara = "select UvozKonacnaZaglavlje.ID, ( 'PLAN:' + Cast(UvozKonacnaZaglavlje.ID as nvarchar(4)) + UvozKonacnaZaglavlje.Napomena ) as Naziv " +
" from UvozKonacnaZaglavlje  where Terminal = 1 order by UvozKonacnaZaglavlje.ID desc";
            var planutovaraSAD = new SqlDataAdapter(planutovara, connection);
            var planutovaraSDS = new DataSet();
            planutovaraSAD.Fill(planutovaraSDS);
            cboPlanUtovara.DataSource = planutovaraSDS.Tables[0];
            cboPlanUtovara.DisplayMember = "Naziv";
            cboPlanUtovara.ValueMember = "ID";
            //Dodaj kontejner UVozKOnacna po Izabranom Zaglavlju
            //Razvrstati 
            //Napravi uslugu
            //Napravi RadniNalogInterni Terminal - Terminalu

        }
    }
}
