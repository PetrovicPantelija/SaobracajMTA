using Microsoft.ReportingServices.Diagnostics.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Skladista
{
    public partial class RNSkladista : Form
    {
        string Tip;
        string Vrsta;
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        string Korisnik = Saobracaj.Sifarnici.frmLogovanje.user;

        public RNSkladista(string vrsta,string tip,string korisnik)
        {
            InitializeComponent();
            Tip = tip;
            Vrsta = vrsta;

            lblTip.Text = Tip;
            panel5.Visible = false;

            if (Vrsta == "Carinsko")
            {
                textBox1.Text = "1008";
            }

            FillCombo();
            InitTable();
            FillMagacinskiBroj();
            FillDodatneUsluge(txtID.Text);
        }
        int ID;
        public RNSkladista(int id,string vrsta, string tip,string korisnik)
        {
            InitializeComponent();
            Tip = tip;
            Vrsta = vrsta;

            lblTip.Text = Tip;
            panel5.Visible = false;
            ID = id;
            txtID.Text= ID.ToString();  
            if (Vrsta == "Carinsko")
            {
                textBox1.Text = "1008";
            }

            FillCombo();
            InitTable();
            FillMagacinskiBroj();
            
            FillDodatneUsluge(txtID.Text);
        }
        private void RNSkladista_Load(object sender, EventArgs e)
        {
            if (Vrsta == "Carinsko" && Tip == "Prijem")
            {
                panelOtprema1.Visible = false;
                panelOtprema2.Visible = false;
                FillComboPrijem();

                cboMagacinskiBroj.Visible = false;
                label3.Visible = false;
                btnMagacinskiBroj.Visible = false;

            }
            if (Vrsta == "Carinsko" && Tip == "Otprema")
            {
                panelPrijem1.Visible = false;
                panelPrijem2.Visible = false;
                FillComboOtprema();
            }
            if (Vrsta == "Carinsko" && Tip == "Pretovar")
            {
                FillComboOtprema();
                FillComboPrijem();

                cboMagacinskiBroj.Visible = false;
                label3.Visible = false;
                btnMagacinskiBroj.Visible = false;
            }
            if (Vrsta == "Carinsko" && Tip == "Interni prenos")
            {
                panelPrijem1.Visible = false;
                panelPrijem2.Visible = false;
                FillComboOtprema();
            }
            if (Vrsta == "Carinsko" && Tip == "Vanredni poslovi")
            {
                panelPrijem1.Visible = false;
                panelPrijem2.Visible = false;
                FillComboOtprema();
            }
            if (Vrsta == "Carinsko" && Tip == "Interni poslovi")
            {
                panelPrijem1.Visible = false;
                panelPrijem2.Visible = false;
                FillComboOtprema();

                cboMagacinskiBroj.Visible = false;
                label3.Visible = false;
                btnMagacinskiBroj.Visible = false;
            }
            btnStorno.Text = "";
            btnStorno.Enabled = false;
            btnIspravka.Text = "";
            btnIspravka.Enabled = false;
            btnSaglasnost.Text = "";
            btnSaglasnost.Enabled = false;
            btnPrijemnica.Text = "";
            btnPrijemnica.Enabled = false;


            if(txtID.Text!="")
            {
                VratiPodatke(ID);
            }
        }
        private void VratiPodatke(int id)
        {
            var conn = new SqlConnection(connection);
            conn.Open();
            var cmd = new SqlCommand(@"SELECT [ID]
      ,[Status]
      ,[Datum]
      ,[Korisnik]
      ,[VrstaRN]
      ,[TipRN]
      ,[CarinskoSkladiste]
      ,[MagacinskiBroj]
      ,[Nalogodavac]
      ,[CarinskiPostupak]
      ,[OpisPosla]
      ,[VlasnikRobe]
      ,[VrstaRobe]
      ,[NacinPakovanja]
      ,[OstalaSkladista]
      ,[PIB]
      ,[VrstaPrevoznogSredstvaOtprema]
      ,[VrstaKamionaOtprema]
      ,[VoziloOtprema]
      ,[VozacOtprema]
      ,[BrojLKOtprema]
      ,[BrojTelefonaOtprema]
      ,[OdredisnaCarinarnicaOtpremaOtprema]
      ,[SpediterOtprema]
      ,[KontakOsobaSpediteraOtprema]
      ,[MestoIstovaraOtprema]
      ,[AdresaOtprema]
      ,[KontaktOsobaIstovarOtprema]
      ,[PlaniraniDatumOtpema]
      ,[PlaniraniDatum2Otprema]
      ,[BrojKontejneraOtprema]
      ,[VrstaPrevoznogSredstvaPrijem]
      ,[VrstaKamionaPrijem]
      ,[VoziloPrijem]
      ,[VozacPrijem]
      ,[BrojLKPrijem]
      ,[BrojTelefonaPrijem]
      ,[OdredisnaCarinarnicaPrijem]
      ,[SpediterPrijem]
      ,[KontakOsobaSpediteraPrijem]
      ,[MestoIstovaraPrijem]
      ,[AdresaPrijem]
      ,[KontaktOsobaIstovarPrijem]
      ,[PlaniraniDatumPrijem]
      ,[PlaniraniDatum2Prijem]
      ,[BrojKontejneraPrijem]
      ,[PosebniUslovi]
      ,[DodatneUslugeID]
      ,[Napomena]
      ,[Aktivan]
      ,[Formiran]
    FROM RadniNalogSkladista
    Where ID=" + id, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read()) { 
                txtID.Text= dr["ID"].ToString();
                Vrsta = dr["VrstaRN"].ToString();
                Tip= dr["TipRN"].ToString();
                textBox1.Text = dr["CarinskoSkladiste"].ToString();
                cboNalogodavac.SelectedValue = Convert.ToInt32(dr["Nalogodavac"].ToString());
                cboCarinskiPostupak.SelectedValue = Convert.ToInt32(dr["CarinskiPostupak"].ToString());
                txtOpisPosla.Text = dr["OpisPosla"].ToString();
                cboVlasnikRobe.SelectedValue = Convert.ToInt32(dr["VlasnikRobe"].ToString());
                txtNacinPakovanja.Text = dr["NacinPakovanja"].ToString();
                txtVrstaRobe.Text = dr["VrstaRobe"].ToString();
                cboADR.SelectedValue = Convert.ToInt32(dr["OstalaSkladista"].ToString());
                txtPIB.Text = dr["PIB"].ToString();
                cboTipTransportaOtprema.SelectedValue = Convert.ToInt32(dr["VrstaPrevoznogSredstvaOtprema"].ToString());
                cboVrstaKamionaOtprema.SelectedValue = Convert.ToInt32(dr["VrstaKamionaOtprema"].ToString());
                txtVoziloOtprema.Text = dr["VoziloOtprema"].ToString();
                txtVozacOtprema.Text = dr["VozacOtprema"].ToString();
                txtLKOtprema.Text = dr["BrojLKOtprema"].ToString();
                txtTelefonOtprema.Text = dr["BrojTelefonaOtprema"].ToString();
                cboCarinarnicaOtprema.SelectedValue = Convert.ToInt32(dr["OdredisnaCarinarnicaOtpremaOtprema"].ToString());
                cboSpediterOtprema.SelectedValue = Convert.ToInt32(dr["SpediterOtprema"].ToString());
                txtKontakOsobaSpediterOtprema.Text = dr["KontakOsobaSpediteraOtprema"].ToString();
                cboMestoIstovaraOtprema.SelectedValue = Convert.ToInt32(dr["MestoIstovaraOtprema"].ToString());
                txtAdresaOtprema.Text = dr["AdresaOtprema"].ToString();
                txtKontaktOsobaOtprema.Text = dr["KontaktOsobaIstovarOtprema"].ToString();
                planiranoVremeOtprema.Value = Convert.ToDateTime(dr["PlaniraniDatumOtpema"].ToString());
                novoVremeOtprema.Value = Convert.ToDateTime(dr["PlaniraniDatum2Otprema"].ToString());
                txtKontejnerOtprema.Text = dr["BrojKontejneraOtprema"].ToString();
                cboTipTransportaPrijem.SelectedValue = Convert.ToInt32(dr["VrstaPrevoznogSredstvaPrijem"].ToString());
                cboVrstaKamionaPrijem.SelectedValue = Convert.ToInt32(dr["VrstaKamionaPrijem"].ToString());
                txtVoziloPrijem.Text = dr["VoziloPrijem"].ToString();
                txtVozacPrijem.Text = dr["VozacPrijem"].ToString();
                txtLKPrijem.Text = dr["BrojLKPrijem"].ToString();
                txtTelefonPrijem.Text = dr["BrojTelefonaPrijem"].ToString();
                cboCarinarnicaPrijem.SelectedValue = Convert.ToInt32(dr["OdredisnaCarinarnicaPrijem"].ToString());
                cboSpediterPrijem.SelectedValue = Convert.ToInt32(dr["SpediterPrijem"].ToString());
                txtKontakOsobaSpediterPrijem.Text = dr["KontakOsobaSpediteraPrijem"].ToString();
                cboMestoIstovaraPrijem.SelectedValue = Convert.ToInt32(dr["MestoIstovaraPrijem"].ToString());
                txtAdresaPrijem.Text = dr["AdresaPrijem"].ToString();
                txtKontaktOsobaPrijem.Text = dr["KontaktOsobaIstovarPrijem"].ToString();
                planiranoVremePrijem.Value = Convert.ToDateTime(dr["PlaniraniDatumPrijem"].ToString());
                novoVremePrijem.Value = Convert.ToDateTime(dr["PlaniraniDatum2Prijem"].ToString());
                txtKontejnerPrijem.Text = dr["BrojKontejneraPrijem"].ToString();
                txtPosebniUslovi.Text = dr["PosebniUslovi"].ToString();
                txtNapomena.Text = dr["Napomena"].ToString();
                Aktivan= Convert.ToInt32(dr["Aktivan"].ToString());
                Formiran = Convert.ToInt32(dr["Formiran"].ToString());
                if (MagacinskiBroj != 0)
                {
                    cboMagacinskiBroj.SelectedValue = MagacinskiBroj;
                }
            }
            conn.Close();
        }
        
        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connection);

            var dodatneUsluge = "SELECT ID, RTRIM(Naziv) AS Naziv FROM VrstaManipulacije ORDER BY ID ASC";
            var daDodatneUsluge = new SqlDataAdapter(dodatneUsluge, conn);
            var dsDodatneUsluge = new System.Data.DataSet();
            daDodatneUsluge.Fill(dsDodatneUsluge);
            cboDodatneUsluge.DataSource = dsDodatneUsluge.Tables[0];
            cboDodatneUsluge.DisplayMember = "Naziv";
            cboDodatneUsluge.ValueMember = "ID";

            var nalogodava = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var daNalogodava = new SqlDataAdapter(nalogodava, conn);
            var dsNalogodava = new System.Data.DataSet();
            daNalogodava.Fill(dsNalogodava);
            cboNalogodavac.DataSource = dsNalogodava.Tables[0];
            cboNalogodavac.DisplayMember = "PaNaziv";
            cboNalogodavac.ValueMember = "PaSifra";

            var partner = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var partAD = new SqlDataAdapter(partner, conn);
            var partDS = new System.Data.DataSet();
            partAD.Fill(partDS);
            cboVlasnikRobe.DataSource = partDS.Tables[0];
            cboVlasnikRobe.DisplayMember = "PaNaziv";
            cboVlasnikRobe.ValueMember = "PaSifra";

            var carinskiPostupak = "Select id,Naziv from VrstaCarinskogPostupka order by id asc";
            var carinskiPostupakDa = new SqlDataAdapter(carinskiPostupak, conn);
            var carinskiPostupakDs = new System.Data.DataSet();
            carinskiPostupakDa.Fill(carinskiPostupakDs);
            cboCarinskiPostupak.DataSource = carinskiPostupakDs.Tables[0];
            cboCarinskiPostupak.DisplayMember = "Naziv";
            cboCarinskiPostupak.ValueMember = "id";
        }
        private void FillComboOtprema()
        {
            SqlConnection conn = new SqlConnection(connection);

            var spediter = "Select PaSifra,PaNaziv From Partnerji Where Spediter=1 order by PaSifra desc";
            var daSpediter = new SqlDataAdapter(spediter, conn);
            var dsSpediter = new System.Data.DataSet();
            daSpediter.Fill(dsSpediter);
            cboSpediterOtprema.DataSource = dsSpediter.Tables[0];
            cboSpediterOtprema.DisplayMember = "PaNaziv";
            cboSpediterOtprema.ValueMember = "PaSifra";

            var vrstaVozila = "Select ID,Naziv From VrstePrevoznogSredstva order by ID asc";
            var daVrstaVozila = new SqlDataAdapter(vrstaVozila, conn);
            var dsVrstaVozila = new System.Data.DataSet();
            daVrstaVozila.Fill(dsVrstaVozila);
        
            cboTipTransportaOtprema.DataSource = dsVrstaVozila.Tables[0];
            cboTipTransportaOtprema.DisplayMember = "Naziv";
            cboTipTransportaOtprema.ValueMember = "ID";


            var vrstaKamiona = "Select ID,Naziv From VrstaVozila order by ID asc";
            var daVrstaKamiona = new SqlDataAdapter(vrstaKamiona, conn);
            var dsVrstaKamiona = new System.Data.DataSet();
            daVrstaKamiona.Fill(dsVrstaKamiona);
          
            cboVrstaKamionaOtprema.DataSource = dsVrstaKamiona.Tables[0];
            cboVrstaKamionaOtprema.DisplayMember = "Naziv";
            cboVrstaKamionaOtprema.ValueMember = "ID";

            var carinarnica = "Select ID,Naziv From Carinarnice order by ID asc";
            var daCarinarnica = new SqlDataAdapter(carinarnica, conn);
            var dsCarinarnica = new System.Data.DataSet();
            daCarinarnica.Fill(dsCarinarnica);
          
            cboCarinarnicaOtprema.DataSource = dsCarinarnica.Tables[0];
            cboCarinarnicaOtprema.DisplayMember = "Naziv";
            cboCarinarnicaOtprema.ValueMember = "ID";
            cboCarinarnicaOtprema.SelectedValue = 140;


            var mu = "select ID, Naziv from MestaUtovara order by Naziv";
            var muAD = new SqlDataAdapter(mu, conn);
            var muDS = new System.Data.DataSet();
            muAD.Fill(muDS);
        
            cboMestoIstovaraOtprema.DataSource = muDS.Tables[0];
            cboMestoIstovaraOtprema.DisplayMember = "Naziv";
            cboMestoIstovaraOtprema.ValueMember = "ID";
            cboMestoIstovaraOtprema.SelectedValue = 2;

        }
        private void FillComboPrijem()
        {
            SqlConnection conn = new SqlConnection(connection);

            var spediter = "Select PaSifra,PaNaziv From Partnerji Where Spediter=1 order by PaSifra desc";
            var daSpediter = new SqlDataAdapter(spediter, conn);
            var dsSpediter = new System.Data.DataSet();
            daSpediter.Fill(dsSpediter);
            cboSpediterPrijem.DataSource = dsSpediter.Tables[0];
            cboSpediterPrijem.DisplayMember = "PaNaziv";
            cboSpediterPrijem.ValueMember = "PaSifra";


            var vrstaVozila = "Select ID,Naziv From VrstePrevoznogSredstva order by ID asc";
            var daVrstaVozila = new SqlDataAdapter(vrstaVozila, conn);
            var dsVrstaVozila = new System.Data.DataSet();
            daVrstaVozila.Fill(dsVrstaVozila);
            cboTipTransportaPrijem.DataSource = dsVrstaVozila.Tables[0];
            cboTipTransportaPrijem.DisplayMember = "Naziv";
            cboTipTransportaPrijem.ValueMember = "ID";



            var vrstaKamiona = "Select ID,Naziv From VrstaVozila order by ID asc";
            var daVrstaKamiona = new SqlDataAdapter(vrstaKamiona, conn);
            var dsVrstaKamiona = new System.Data.DataSet();
            daVrstaKamiona.Fill(dsVrstaKamiona);
            cboVrstaKamionaPrijem.DataSource = dsVrstaKamiona.Tables[0];
            cboVrstaKamionaPrijem.DisplayMember = "Naziv";
            cboVrstaKamionaPrijem.ValueMember = "ID";


            var carinarnica = "Select ID,Naziv From Carinarnice order by ID asc";
            var daCarinarnica = new SqlDataAdapter(carinarnica, conn);
            var dsCarinarnica = new System.Data.DataSet();
            daCarinarnica.Fill(dsCarinarnica);
            cboCarinarnicaPrijem.DataSource = dsCarinarnica.Tables[0];
            cboCarinarnicaPrijem.DisplayMember = "Naziv";
            cboCarinarnicaPrijem.ValueMember = "ID";
            cboCarinarnicaPrijem.SelectedValue = 140;



            var mu = "select ID, Naziv from MestaUtovara order by Naziv";
            var muAD = new SqlDataAdapter(mu, conn);
            var muDS = new System.Data.DataSet();
            muAD.Fill(muDS);
            cboMestoIstovaraPrijem.DataSource = muDS.Tables[0];
            cboMestoIstovaraPrijem.DisplayMember = "Naziv";
            cboMestoIstovaraPrijem.ValueMember = "ID";
            cboMestoIstovaraPrijem.SelectedValue = 2;
        }

        DataTable dtUsluge = new DataTable();
        private void InitTable()
        {
            dtUsluge = new DataTable();
            dtUsluge.Columns.Add("UslugaID", typeof(int));
            dtUsluge.Columns.Add("Naziv", typeof(string));

            dgvUsluge.AutoGenerateColumns = true;
            dgvUsluge.DataSource = dtUsluge;
        }
        private void FillDodatneUsluge(string rn)
        {
            dtUsluge.Rows.Clear();

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                string query = @"SELECT d.Usluga, RTRIM(v.Naziv) Naziv
                         FROM RNCarinskoSkladisteDodatneUsluge d
                         INNER JOIN VrstaManipulacije v ON d.Usluga = v.ID
                         WHERE d.RN = @RN";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RN", rn);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            dtUsluge.Rows.Add(
                                Convert.ToInt32(dr["Usluga"]),
                                dr["Naziv"].ToString()
                            );
                        }
                    }
                }
            }
        }
        private void FillMagacinskiBroj()
        {
            SqlConnection conn = new SqlConnection(connection);

            if (Vrsta == "Carinsko")
            {
                var partner5 = "Select ID, Naziv from MagacinskiBrojCarinski order by ID Desc";
                var partAD5 = new SqlDataAdapter(partner5, conn);
                var partDS5 = new System.Data.DataSet();
                partAD5.Fill(partDS5);
                cboMagacinskiBroj.DataSource = partDS5.Tables[0];
                cboMagacinskiBroj.DisplayMember = "Naziv";
                cboMagacinskiBroj.ValueMember = "ID";

                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection myConnection = new SqlConnection(s_connection);
                var c = new SqlConnection(s_connection);
                var dataAdapter = new SqlDataAdapter(partner5, c);

                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new System.Data.DataSet();
                dataAdapter.Fill(ds);
                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = ds.Tables[0];

                conn.Open();
                using (SqlCommand cmd = new SqlCommand("select ISNUll(MIN(ID),0) from MagacinskiBrojCarinski Where Naziv=''", conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (Convert.ToInt32(dr[0].ToString()) == 0)
                        {
                            txtMbID.Text = "";
                        }
                        else
                        {
                            txtMbID.Text = dr[0].ToString();
                        }
                    }

                }
            }
        }
        private void btnMagacinskiBroj_Click(object sender, EventArgs e)
        {
            FillMagacinskiBroj();
            panel5.Visible = true;
        }
        int MagacinskiBroj;
        int Aktivan = 0;
        private void btnMbSave_Click(object sender, EventArgs e)
        {
            InsertCarinskoSkladiste ins = new InsertCarinskoSkladiste();

            if (Vrsta == "Carinsko")
            {
                if (string.IsNullOrWhiteSpace(txtMbID.Text))
                {
                    using (SqlConnection conn = new SqlConnection(connection))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("select Max(ID)+1 from MagacinskiBrojCarinski", conn))
                        {
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                MagacinskiBroj = Convert.ToInt32(dr[0].ToString());
                                if (MagacinskiBroj != 0)
                                {
                                    Aktivan = 1;
                                }
                            }
                        }
                    }

                    ins.InsertMagacinskiBrojCarinski(MagacinskiBroj, txtMbNaziv.Text.Trim());
                    FillMagacinskiBroj();
                    cboMagacinskiBroj.SelectedValue = MagacinskiBroj;

                }
                else
                {
                    int mbId = Convert.ToInt32(txtMbID.Text.Trim());

                    ins.UpdateMagacinskiBrojCarinski(mbId, txtMbNaziv.Text.Trim());
                    FillMagacinskiBroj();
                    MagacinskiBroj = mbId;
                    cboMagacinskiBroj.SelectedValue = MagacinskiBroj;
                    if (MagacinskiBroj != 0)
                    {
                        Aktivan = 1;
                    }
                }

                panel5.Visible = false;
            }
        }
        private void btnMbNazad_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
        }
        private void btnDodaj_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(cboDodatneUsluge.SelectedValue);
            string naziv = cboDodatneUsluge.Text;

            foreach (DataRow row in dtUsluge.Rows)
            {
                if ((int)row["UslugaID"] == id)
                {
                    MessageBox.Show("Usluga je već dodata.");
                    return;
                }
            }

            dtUsluge.Rows.Add(id, naziv);
        }
        private void btnIzbaci_Click(object sender, EventArgs e)
        {
            if (dgvUsluge.CurrentRow == null)
                return;

            dgvUsluge.Rows.Remove(dgvUsluge.CurrentRow);
        }
        private void cboMagacinskiBroj_SelectionChangeCommitted(object sender, EventArgs e)
        {
            MagacinskiBroj = Convert.ToInt32(cboMagacinskiBroj.SelectedValue);
            if (MagacinskiBroj != 0)
            {
                Aktivan = 1;
            }
        }

        int PIB;
        private void cboVlasnikRobe_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var conn = new SqlConnection(connection);
            conn.Open();
            var cmd = new SqlCommand("SELECT PaEMatSt1 from Partnerji Where PaSifra=" + Convert.ToInt32(cboVlasnikRobe.SelectedValue), conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                PIB = Convert.ToInt32(dr[0].ToString());
                txtPIB.Text = PIB.ToString();
            }
            conn.Close();
        }
        int Formiran = 0;
        private void btnSnimi_Click(object sender, EventArgs e)
        {
            InsertCarinskoSkladiste ins = new InsertCarinskoSkladiste();
            if (txtID.Text != "")
            {
                try
                {
                    int idUsluge = 0;
                    using (SqlConnection conn = new SqlConnection(connection))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("Select top 1 ID From RNCarinskoSkladisteDodatneUsluge Where RN=" + Convert.ToInt32(txtID.Text), conn))
                        {
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                idUsluge = Convert.ToInt32(dr[0].ToString());
                            }
                        }
                    }
                    ins.DeleteDodatneUsluge(Convert.ToInt32(txtID.Text));

                    foreach (DataRow row in dtUsluge.Rows)
                    {
                        int valueMember = Convert.ToInt32(row["UslugaID"]);
                        ins.InsertDodatneUsluge(idUsluge, Convert.ToInt32(txtID.Text), valueMember);
                    }

                    ins.UpdateRadniNalog(Convert.ToInt32(txtID.Text),"Kreiran", DateTime.Now, Korisnik, Vrsta, Tip, textBox1.Text.ToString().TrimEnd(), MagacinskiBroj, Convert.ToInt32(cboNalogodavac.SelectedValue),
                        Convert.ToInt32(cboCarinskiPostupak.SelectedValue), txtOpisPosla.Text.ToString().TrimEnd(), Convert.ToInt32(cboVlasnikRobe.SelectedValue), txtVrstaRobe.Text.ToString().TrimEnd(),
                        txtNacinPakovanja.Text.ToString().TrimEnd(), Convert.ToInt32(cboADR.SelectedValue), Convert.ToInt32(txtPIB.Text), Convert.ToInt32(cboTipTransportaOtprema.SelectedValue),
                        Convert.ToInt32(cboVrstaKamionaOtprema.SelectedValue), txtVoziloOtprema.Text.ToString().TrimEnd(), txtVozacOtprema.Text.ToString().TrimEnd(), txtLKOtprema.Text.ToString().TrimEnd(),
                        txtTelefonOtprema.Text.ToString().TrimEnd(), Convert.ToInt32(cboCarinarnicaOtprema.SelectedValue), Convert.ToInt32(cboSpediterOtprema.SelectedValue), txtKontakOsobaSpediterOtprema.Text.ToString().TrimEnd(),
                        Convert.ToInt32(cboMestoIstovaraOtprema.SelectedValue), txtAdresaOtprema.Text.ToString().TrimEnd(), txtKontaktOsobaOtprema.Text.ToString().TrimEnd(), Convert.ToDateTime(planiranoVremeOtprema.Value),
                        Convert.ToDateTime(novoVremeOtprema.Value), txtKontejnerOtprema.Text.ToString().TrimEnd(), Convert.ToInt32(cboTipTransportaPrijem.SelectedValue), Convert.ToInt32(cboVrstaKamionaPrijem.SelectedValue),
                        txtVoziloPrijem.Text.ToString().TrimEnd(), txtVozacPrijem.Text.ToString().TrimEnd(), txtLKPrijem.Text.ToString().TrimEnd(), txtTelefonPrijem.Text.ToString().TrimEnd(), Convert.ToInt32(cboCarinarnicaPrijem.SelectedValue),
                        Convert.ToInt32(cboSpediterPrijem.SelectedValue), txtKontakOsobaSpediterPrijem.Text.ToString().TrimEnd(), Convert.ToInt32(cboMestoIstovaraPrijem.SelectedValue), txtAdresaPrijem.Text.ToString().TrimEnd(), txtKontaktOsobaPrijem.Text.ToString().TrimEnd(),
                        Convert.ToDateTime(planiranoVremePrijem.Value), Convert.ToDateTime(novoVremePrijem.Value), txtKontejnerPrijem.Text.ToString().TrimEnd(), txtPosebniUslovi.Text.ToString().TrimEnd(), idUsluge,
                        txtNapomena.Text.ToString().TrimEnd(), Aktivan, Formiran);

                    MessageBox.Show("RADNI NALOG SAČUVAN");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR Update \n:" + ex.ToString());
                    return;
                }
            }
            else
            {
                try
                {
                    int rn = 0;

                    using (SqlConnection conn = new SqlConnection(connection))
                    {
                        conn.Open();

                        using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(ID),0)+1 FROM RadniNalogSkladista", conn))
                        {
                            object result = cmd.ExecuteScalar();
                            rn = Convert.ToInt32(result);
                        }
                    }

                    int IdUsluge = 0;

                    using (SqlConnection conn = new SqlConnection(connection))
                    {
                        conn.Open();

                        using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(ID),0)+1 FROM RNCarinskoSkladisteDodatneUsluge", conn))
                        {
                            object result = cmd.ExecuteScalar();
                            IdUsluge = Convert.ToInt32(result);
                        }
                    }

                    foreach (DataRow row in dtUsluge.Rows)
                    {
                        int valueMember = Convert.ToInt32(row["UslugaID"]);
                        ins.InsertDodatneUsluge(IdUsluge, rn, valueMember);
                    }

                    ins.InsertRadniNalog("Kreiran", DateTime.Now, Korisnik, Vrsta, Tip, textBox1.Text.ToString().TrimEnd(), MagacinskiBroj, Convert.ToInt32(cboNalogodavac.SelectedValue),
                        Convert.ToInt32(cboCarinskiPostupak.SelectedValue), txtOpisPosla.Text.ToString().TrimEnd(), Convert.ToInt32(cboVlasnikRobe.SelectedValue), txtVrstaRobe.Text.ToString().TrimEnd(),
                        txtNacinPakovanja.Text.ToString().TrimEnd(), Convert.ToInt32(cboADR.SelectedValue), Convert.ToInt32(txtPIB.Text), Convert.ToInt32(cboTipTransportaOtprema.SelectedValue),
                        Convert.ToInt32(cboVrstaKamionaOtprema.SelectedValue), txtVoziloOtprema.Text.ToString().TrimEnd(), txtVozacOtprema.Text.ToString().TrimEnd(), txtLKOtprema.Text.ToString().TrimEnd(),
                        txtTelefonOtprema.Text.ToString().TrimEnd(), Convert.ToInt32(cboCarinarnicaOtprema.SelectedValue), Convert.ToInt32(cboSpediterOtprema.SelectedValue), txtKontakOsobaSpediterOtprema.Text.ToString().TrimEnd(),
                        Convert.ToInt32(cboMestoIstovaraOtprema.SelectedValue), txtAdresaOtprema.Text.ToString().TrimEnd(), txtKontaktOsobaOtprema.Text.ToString().TrimEnd(), Convert.ToDateTime(planiranoVremeOtprema.Value),
                        Convert.ToDateTime(novoVremeOtprema.Value), txtKontejnerOtprema.Text.ToString().TrimEnd(), Convert.ToInt32(cboTipTransportaPrijem.SelectedValue), Convert.ToInt32(cboVrstaKamionaPrijem.SelectedValue),
                        txtVoziloPrijem.Text.ToString().TrimEnd(), txtVozacPrijem.Text.ToString().TrimEnd(), txtLKPrijem.Text.ToString().TrimEnd(), txtTelefonPrijem.Text.ToString().TrimEnd(), Convert.ToInt32(cboCarinarnicaPrijem.SelectedValue),
                        Convert.ToInt32(cboSpediterPrijem.SelectedValue), txtKontakOsobaSpediterPrijem.Text.ToString().TrimEnd(), Convert.ToInt32(cboMestoIstovaraPrijem.SelectedValue), txtAdresaPrijem.Text.ToString().TrimEnd(), txtKontaktOsobaPrijem.Text.ToString().TrimEnd(),
                        Convert.ToDateTime(planiranoVremePrijem.Value), Convert.ToDateTime(novoVremePrijem.Value), txtKontejnerPrijem.Text.ToString().TrimEnd(), txtPosebniUslovi.Text.ToString().TrimEnd(), IdUsluge,
                        txtNapomena.Text.ToString().TrimEnd(), Aktivan, Formiran);

                    MessageBox.Show("RADNI NALOG KREIRAN");

                    using (SqlConnection conn = new SqlConnection(connection))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("select Max(ID) as ID from RadniNalogSkladista", conn))
                        {
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                               txtID.Text=dr["ID"].ToString();
                            }
                        }
                        conn.Close();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR Insert \n:" + ex.ToString());
                    return;
                }
            }
        }

        private void btnFormiranRN_Click(object sender, EventArgs e)
        {
            InsertCarinskoSkladiste ins = new InsertCarinskoSkladiste();
            if (Formiran == 1)
            {
                MessageBox.Show("RADNI NALOG JE VEĆ FORMIRAN");
                return;
            }
            else
            {
                Formiran = 1;
                try
                {
                    int idUsluge = 0;
                    using (SqlConnection conn = new SqlConnection(connection))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("Select top 1 ID From RNCarinskoSkladisteDodatneUsluge Where RN=" + Convert.ToInt32(txtID.Text), conn))
                        {
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                idUsluge = Convert.ToInt32(dr[0].ToString());
                            }
                        }
                    }
                    ins.DeleteDodatneUsluge(Convert.ToInt32(txtID.Text));

                    foreach (DataRow row in dtUsluge.Rows)
                    {
                        int valueMember = Convert.ToInt32(row["UslugaID"]);
                        ins.InsertDodatneUsluge(idUsluge, Convert.ToInt32(txtID.Text), valueMember);
                    }

                    ins.UpdateRadniNalog(Convert.ToInt32(txtID.Text), "FORMIRAN", DateTime.Now, Korisnik, Vrsta, Tip, textBox1.Text.ToString().TrimEnd(), MagacinskiBroj, Convert.ToInt32(cboNalogodavac.SelectedValue),
                        Convert.ToInt32(cboCarinskiPostupak.SelectedValue), txtOpisPosla.Text.ToString().TrimEnd(), Convert.ToInt32(cboVlasnikRobe.SelectedValue), txtVrstaRobe.Text.ToString().TrimEnd(),
                        txtNacinPakovanja.Text.ToString().TrimEnd(), Convert.ToInt32(cboADR.SelectedValue), Convert.ToInt32(txtPIB.Text), Convert.ToInt32(cboTipTransportaOtprema.SelectedValue),
                        Convert.ToInt32(cboVrstaKamionaOtprema.SelectedValue), txtVoziloOtprema.Text.ToString().TrimEnd(), txtVozacOtprema.Text.ToString().TrimEnd(), txtLKOtprema.Text.ToString().TrimEnd(),
                        txtTelefonOtprema.Text.ToString().TrimEnd(), Convert.ToInt32(cboCarinarnicaOtprema.SelectedValue), Convert.ToInt32(cboSpediterOtprema.SelectedValue), txtKontakOsobaSpediterOtprema.Text.ToString().TrimEnd(),
                        Convert.ToInt32(cboMestoIstovaraOtprema.SelectedValue), txtAdresaOtprema.Text.ToString().TrimEnd(), txtKontaktOsobaOtprema.Text.ToString().TrimEnd(), Convert.ToDateTime(planiranoVremeOtprema.Value),
                        Convert.ToDateTime(novoVremeOtprema.Value), txtKontejnerOtprema.Text.ToString().TrimEnd(), Convert.ToInt32(cboTipTransportaPrijem.SelectedValue), Convert.ToInt32(cboVrstaKamionaPrijem.SelectedValue),
                        txtVoziloPrijem.Text.ToString().TrimEnd(), txtVozacPrijem.Text.ToString().TrimEnd(), txtLKPrijem.Text.ToString().TrimEnd(), txtTelefonPrijem.Text.ToString().TrimEnd(), Convert.ToInt32(cboCarinarnicaPrijem.SelectedValue),
                        Convert.ToInt32(cboSpediterPrijem.SelectedValue), txtKontakOsobaSpediterPrijem.Text.ToString().TrimEnd(), Convert.ToInt32(cboMestoIstovaraPrijem.SelectedValue), txtAdresaPrijem.Text.ToString().TrimEnd(), txtKontaktOsobaPrijem.Text.ToString().TrimEnd(),
                        Convert.ToDateTime(planiranoVremePrijem.Value), Convert.ToDateTime(novoVremePrijem.Value), txtKontejnerPrijem.Text.ToString().TrimEnd(), txtPosebniUslovi.Text.ToString().TrimEnd(), idUsluge,
                        txtNapomena.Text.ToString().TrimEnd(), Aktivan, Formiran);

                    MessageBox.Show("RADNI NALOG SAČUVAN");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR Update \n:" + ex.ToString());
                    return;
                }
            }
        }
    }
}
