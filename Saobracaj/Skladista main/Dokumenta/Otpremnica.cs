using Saobracaj.RadniNalozi;
using Saobracaj.Skladista;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main.Dokumenta
{
    public partial class Otpremnica : Form
    {
        string Tip;
        int RN;
        string Vrsta;

        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        string Korisnik = Saobracaj.Sifarnici.frmLogovanje.user;
        public Otpremnica(string tip, string vrsta, int rn)
        {
            InitializeComponent();
            Tip = tip;
            RN = rn;
            Vrsta = vrsta;
            FillCombo();
            InitTable();
            FillMagacinskiBroj();
            FillDodatneUsluge(RN.ToString());
            txtID.Text = RN.ToString();

            VratiPodatke(Convert.ToInt32(txtID.Text));
            VratiStavkeOtpreme(Convert.ToInt32(txtID.Text));
            if (txtOtpremnica.Text != "")
            {
                VratiOtpremnicu(Convert.ToInt32(txtOtpremnica.Text));
            }

            panel7.Visible = false;
        }
        private void FillMagacinskiBroj()
        {
            SqlConnection conn = new SqlConnection(connection);

            if (Vrsta == "Carinsko")
            {
                var partner5 = @"Select MagacinskiBroj as ID, MagacinskiBrojCarinski.Naziv as Naziv
	From RadniNalogSkladista 
	inner join RNCarinskoSkladistePrijemnica on RadniNalogSkladista.ID=RNCarinskoSkladistePrijemnica.RN 
	inner join MagacinskiBrojCarinski on RadniNalogSkladista.MagacinskiBroj=MagacinskiBrojCarinski.ID
	Where RNCarinskoSkladistePrijemnica.Status='ZA'";
                var partAD5 = new SqlDataAdapter(partner5, conn);
                var partDS5 = new System.Data.DataSet();
                partAD5.Fill(partDS5);
                cboMagacinskiBroj.DataSource = partDS5.Tables[0];
                cboMagacinskiBroj.DisplayMember = "Naziv";
                cboMagacinskiBroj.ValueMember = "ID";

                
            }
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
        int Aktivan;
        int Formiran;
        int MagacinskiBroj;
        private void VratiPodatke(int id)
        {
            using (SqlConnection conn2 = new SqlConnection(connection))
            {
                conn2.Open();
                using (SqlCommand cmd2 = new SqlCommand("select ID From RNCarinskoSkladisteOtpremnica Where RN=" + id, conn2))
                {
                    using (SqlDataReader dr2 = cmd2.ExecuteReader())
                    {
                        if (dr2.Read())
                        {
                            txtOtpremnica.Text = dr2["ID"].ToString();
                        }
                    }
                }
                conn2.Close();
            }

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
            while (dr.Read())
            {
                txtID.Text = dr["ID"].ToString();
                Vrsta = dr["VrstaRN"].ToString();
                Tip = dr["TipRN"].ToString();
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

                txtPosebniUslovi.Text = dr["PosebniUslovi"].ToString();
                txtNapomena.Text = dr["Napomena"].ToString();
                Aktivan = Convert.ToInt32(dr["Aktivan"].ToString());
                Formiran = Convert.ToInt32(dr["Formiran"].ToString());

                MagacinskiBroj = Convert.ToInt32(dr["MagacinskiBroj"].ToString());
            }
            conn.Close();
            if (MagacinskiBroj != 0)
            {
                cboMagacinskiBroj.SelectedValue = MagacinskiBroj;
                using (SqlConnection conn2 = new SqlConnection(connection))
                {
                    conn2.Open();
                    using (SqlCommand cmd2 = new SqlCommand(@"select Nalogodavac,VlasnikRobe,VrstaRobe,NacinPakovanja,OstalaSkladista,PIB,PosebniUslovi,DodatneUslugeID,Napomena,RadniNalogSkladista.ID as RN,
RNCarinskoSkladistePrijemnica.ID as prijemID
From RadniNalogSkladista
inner join RNCarinskoSkladistePrijemnica on RNCarinskoSkladistePrijemnica.RN=RadniNalogSkladista.ID
Where MagacinskiBroj=" + MagacinskiBroj, conn2))
                    {
                        SqlDataReader dr2 = cmd2.ExecuteReader();
                        while (dr2.Read())
                        {
                            cboNalogodavac.SelectedValue = Convert.ToInt32(dr2["Nalogodavac"].ToString());
                            cboVlasnikRobe.SelectedValue = Convert.ToInt32(dr2["VlasnikRobe"].ToString());
                            txtVrstaRobe.Text = dr2["VrstaRobe"].ToString();
                            txtNacinPakovanja.Text = dr2["NacinPakovanja"].ToString();
                            cboADR.SelectedValue = Convert.ToInt32(dr2["OstalaSkladista"].ToString());
                            txtPIB.Text = dr2["PIB"].ToString();
                            txtPosebniUslovi.Text = dr2["PosebniUslovi"].ToString();
                            txtNapomena.Text = dr2["Napomena"].ToString();


                            int PrijemnicaID = Convert.ToInt32(dr2["prijemID"].ToString());
                            textBox2.Text = PrijemnicaID.ToString();

                            int dodatneUslugeID = Convert.ToInt32(dr2["DodatneUslugeID"].ToString());
                            FillDodatneUsluge(dodatneUslugeID.ToString());
                        }
                    }
                    conn2.Close();
                }
            }
        }
        private void VratiOtpremnicu(int otpremnica)
        {
            using (SqlConnection conn2 = new SqlConnection(connection))
            {
                conn2.Open();
                using (SqlCommand cmd2 = new SqlCommand(@"Select DokumentRazduzenja,DatumRazduzenja,Destinacija,DatumOtpreme From RNCarinskoSkladisteOtpremnica WHere ID=" +otpremnica, conn2))
                {
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    while (dr2.Read())
                    {
                        txtDokumentRazduzenja.Text = dr2["DokumentRazduzenja"].ToString();
                        dtDatumRazduzenja.Value = Convert.ToDateTime(dr2["DatumRazduzenja"].ToString());
                        txtDestinacija.Text = dr2["Destinacija"].ToString().TrimEnd();
                    }
                }
                conn2.Close();
            }
        }
        public class OtpremnicaStavke
        {
            public int RB { get; set; }
            public int NHM { get; set; }
            public string Naziv { get; set; }
            public string Naimenovanje { get; set; }
            public string JM { get; set; }
            public decimal Koleta { get; set; }
            public decimal Bruto { get; set; }
            public decimal Neto { get; set; }
            public decimal Vrednost { get; set; }
            public string Valuta { get; set; }
            public string Pozicija { get; set; }
            public int Paleta { get; set; }
            public int VrstaPaleta { get; set; }
            public string PaletaOpis { get; set; }
            public string DimenzijePaleta { get; set; }
            public decimal PDV { get; set; }
            public decimal Carina { get; set; }
            public string Napomena { get; set; }
        }
        private List<OtpremnicaStavke> list = new List<OtpremnicaStavke>();
        private int rb = 1;
        private void VratiStavkeOtpreme(int rn)
        {
            list.Clear();

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                string query = @"
        SELECT 
            RB,
            NHM,
            RNCarinskoSkladisteOtpremaStavkePom.Naziv as Naziv,
            Naimenovanje,
            JM,
            Koleta,
            Bruto,
            Neto,
            Vrednost,
            Valuta,
            Pozicija,
            Paleta,
            VrstaPaleta,
            PDV,
            Carina,
            Napomena,
            TipPalete.Naziv as PaletaOpis,
            TipPalete.Dimenzije as DimenzijePalete
        FROM RNCarinskoSkladisteOtpremaStavkePom
        LEFT JOIN TipPalete 
            ON TipPalete.ID = RNCarinskoSkladisteOtpremaStavkePom.VrstaPaleta
        WHERE IDNadredjena = @ID
        ORDER BY RB";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", rn);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var stavka = new OtpremnicaStavke
                            {
                                RB = Convert.ToInt32(dr["RB"]),
                                NHM = dr["NHM"] == DBNull.Value ? 0 : Convert.ToInt32(dr["NHM"]),
                                Naziv = dr["Naziv"] == DBNull.Value ? "" : dr["Naziv"].ToString(),
                                Naimenovanje = dr["Naimenovanje"] == DBNull.Value ? "" : dr["Naimenovanje"].ToString(),
                                JM = dr["JM"] == DBNull.Value ? "" : dr["JM"].ToString(),
                                Koleta = dr["Koleta"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Koleta"]),
                                Bruto = dr["Bruto"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Bruto"]),
                                Neto = dr["Neto"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Neto"]),
                                Vrednost = dr["Vrednost"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Vrednost"]),
                                Valuta = dr["Valuta"] == DBNull.Value ? "" : dr["Valuta"].ToString(),
                                Pozicija = dr["Pozicija"] == DBNull.Value ? "" : dr["Pozicija"].ToString(),
                                Paleta = dr["Paleta"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Paleta"]),
                                VrstaPaleta = dr["VrstaPaleta"] == DBNull.Value ? 0 : Convert.ToInt32(dr["VrstaPaleta"]),
                                PaletaOpis = dr["PaletaOpis"] == DBNull.Value ? "" : dr["PaletaOpis"].ToString(),
                                DimenzijePaleta = dr["DimenzijePalete"] == DBNull.Value ? "" : dr["DimenzijePalete"].ToString(),
                                PDV = dr["PDV"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["PDV"]),
                                Carina = dr["Carina"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Carina"]),
                                Napomena = dr["Napomena"] == DBNull.Value ? "" : dr["Napomena"].ToString()
                            };

                            list.Add(stavka);
                        }
                    }
                }
            }

            rb = list.Count > 0 ? list.Max(x => x.RB) + 1 : 1;
            OsveziGrid();
        }
        private void OsveziGrid()
        {
            gridGroupingControl1.DataSource = null;
            gridGroupingControl1.DataSource = list.ToList();
            gridGroupingControl1.Refresh();

            if (list.Count > 0)
                rb = list.Max(x => x.RB) + 1;
            else
                rb = 1;
        }
        private decimal GetDecimal(TextBox tb)
        {
            if (string.IsNullOrWhiteSpace(tb.Text))
                return 0;

            decimal value;
            if (decimal.TryParse(tb.Text.Trim(), out value))
                return value;

            return 0;
        }

        private int GetInt(TextBox tb)
        {
            if (string.IsNullOrWhiteSpace(tb.Text))
                return 0;

            int value;
            if (int.TryParse(tb.Text.Trim(), out value))
                return value;

            decimal decValue;
            if (decimal.TryParse(tb.Text.Trim(), out decValue))
                return Convert.ToInt32(decValue);

            return 0;
        }
        private OtpremnicaStavke selektovanaStavka = null;
        private int selektovaniRB = 0;
        int nhm = 0;

        private void ReorderRB()
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].RB = i + 1;
            }

            rb = list.Count + 1;
        }

        private OtpremnicaStavke VratiSelektovanuStavkuIzGrida()
        {
            try
            {
                if (gridGroupingControl1.Table == null)
                    return null;

                if (gridGroupingControl1.Table.CurrentRecord == null)
                    return null;

                return gridGroupingControl1.Table.CurrentRecord.GetData() as OtpremnicaStavke;
            }
            catch
            {
                return null;
            }
        }
        private void btnIzbaci_Click(object sender, EventArgs e)
        {
            if (dgvUsluge.CurrentRow == null)
                return;

            dgvUsluge.Rows.Remove(dgvUsluge.CurrentRow);
        }

        private void btnDodajUsluge_Click(object sender, EventArgs e)
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

        private void btnDodaj_Click(object sender, EventArgs e)
        {

        }

        private void cboVlasnikRobe_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void cboMagacinskiBroj_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void btnMagacinskiBroj_Click(object sender, EventArgs e)
        {

        }

        private void btnPrijemnica_Click(object sender, EventArgs e)
        {

        }

        private void cboRukovalac_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void btnVratiNalog_Click(object sender, EventArgs e)
        {

        }
        int DodatneUslugeID;
        private void btnIzdajNalog_Click(object sender, EventArgs e)
        {
            var conn = new SqlConnection(connection);
            conn.Open();
            var cmd = new SqlCommand(@"select ID From RNCarinskoSkladisteDodatneUsluge Where RN=" + Convert.ToInt32(txtID.Text), conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                DodatneUslugeID = Convert.ToInt32(dr["ID"].ToString());
            }
            conn.Close();

            InsertSkladista ins = new InsertSkladista();
            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    ins.InsertNalogRukovalac(Convert.ToInt32(txtOtpremnica.Text), Convert.ToInt32(cboRukovalac.SelectedValue), row.Cells["Pozicija"].Value.ToString(), Convert.ToDecimal(row.Cells["Koleta"].Value),
                    Convert.ToInt32(row.Cells["Paleta1"].Value), Convert.ToInt32(row.Cells["VrstaPaleta"].Value), Convert.ToDecimal(row.Cells["Bruto"].Value), DodatneUslugeID, "", txtVoziloOtprema.Text.ToString().TrimEnd(),
                    2, Korisnik);
                }
                int rnID = 0;
                conn.Open();
                var cmd2 = new SqlCommand(@"select (ISNULL(Max(ID)+1,0)) from RNCarinskoSkladisteRukovalac", conn);
                SqlDataReader dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    rnID = Convert.ToInt32(dr2[0].ToString());
                }
                conn.Close();
                MessageBox.Show("Kreiran nalog za rukovaoca. ID:" + rnID.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnNHMNazad_Click(object sender, EventArgs e)
        {

        }

        private void btnNHMDodaj_Click(object sender, EventArgs e)
        {

        }

        private void txtNhmNaziv_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtNhm_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void Otpremnica_Load(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void gridGroupingControl1_TableControlCellClick(object sender, Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlCellClickEventArgs e)
        {

        }

        private void btnIzbaciStavku_Click(object sender, EventArgs e)
        {
            OtpremnicaStavke stavka = VratiSelektovanuStavkuIzGrida();

            if (stavka == null)
            {
                MessageBox.Show("Izaberite stavku koju želite da izbacite.");
                return;
            }

            DialogResult odgovor = MessageBox.Show(
                "Da li želite da izbacite selektovanu stavku iz liste?",
                "Izbaci stavku",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (odgovor != DialogResult.Yes)
                return;

            list.Remove(stavka);
            ReorderRB();
            OsveziGrid();
        }

        private void btnNalog_Click(object sender, EventArgs e)
        {
            if (txtOtpremnica.Text == "")
            {
                MessageBox.Show("Mora se prvo sačuvati otpremnica!");
                return;
            }
            else
            {
                panel7.Visible = true;
                NalogZaViljuskaristu();
            }
        }
        private void NalogZaViljuskaristu()
        {
            SqlConnection conn = new SqlConnection(connection);
            var query = @"   SELECT IDNadredjena as Prijemnica,Pozicija,VrstaPaleta,TipPalete.Naziv as Paleta,SUM(Koleta) AS Koleta,SUM(Paleta) AS Paleta,SUM(BRuto) as Bruto,Sum(Neto) as Neto 
                FROM RNCarinskoSkladisteOtpremnicaStavke 
                left join TipPalete on RNCarinskoSkladisteOtpremnicaStavke.VrstaPaleta=TipPalete.ID 
                Where IDNadredjena=" + Convert.ToInt32(txtOtpremnica.Text) + " Group by IDNadredjena,Pozicija,VrstaPaleta,TipPalete.Naziv,Koleta,Paleta,Bruto,Neto";
            var da = new SqlDataAdapter(query, conn);
            var ds = new System.Data.DataSet();

            da.Fill(ds);
            dataGridView2.ReadOnly = false;
            dataGridView2.DataSource = ds.Tables[0];
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[2].Visible = false;
            dataGridView2.Columns[1].Width = 80;
            dataGridView2.Columns[3].Width = 60;
            dataGridView2.Columns[4].Width = 60;
            dataGridView2.Columns[5].Width = 60;
            dataGridView2.Columns[6].Width = 80;
            dataGridView2.Columns[7].Width = 80;

            label49.Text = "Otpremnica: " + txtOtpremnica.Text.ToString();

            var viljuskarista = "SELECT DeSifra, (RTrim(DeIme)+' '+RTrim(DePriimek)) as Zaposleni from Delavci Where Viljuskarista=1";
            var daViljuskarista = new SqlDataAdapter(viljuskarista, conn);
            var dsViljuskarista = new System.Data.DataSet();
            daViljuskarista.Fill(dsViljuskarista);
            cboRukovalac.DataSource = dsViljuskarista.Tables[0];
            cboRukovalac.DisplayMember = "Zaposleni";
            cboRukovalac.ValueMember = "DeSifra";
        }

        private void btnSnimi_Click(object sender, EventArgs e)
        {
            InsertSkladista ins = new InsertSkladista();
            try
            {
                if (txtOtpremnica.Text == "")
                {
                    ins.InsertCarinskaOtpremnica(Korisnik, Convert.ToInt32(txtID.Text), Convert.ToInt32(cboCarinskiPostupak.SelectedValue), txtDokumentRazduzenja.Text.ToString().TrimEnd(),
                        Convert.ToDateTime(dtDatumRazduzenja.Value), txtDestinacija.Text.ToString().TrimEnd(), DateTime.Now, Convert.ToInt32(textBox2.Text));

                    using(SqlConnection conn=new SqlConnection(connection))
                    {
                        conn.Open();
                        using(SqlCommand cmd=new SqlCommand("Select MAX(ID) From RNCarinskoSkladisteOtpremnica", conn))
                        {
                            var result = cmd.ExecuteScalar();
                            if (result != null)
                            {
                                txtOtpremnica.Text = result.ToString();
                            }
                        }
                    }
                }
                else
                {
                    ins.UpdateCarinskaOtpremnica(Convert.ToInt32(txtID.Text),Korisnik, Convert.ToInt32(txtID.Text), Convert.ToInt32(cboCarinskiPostupak.SelectedValue), txtDokumentRazduzenja.Text.ToString().TrimEnd(),
                        Convert.ToDateTime(dtDatumRazduzenja.Value), txtDestinacija.Text.ToString().TrimEnd(), DateTime.Now, Convert.ToInt32(textBox2.Text));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR Insert otprmenica:" + ex.ToString());
            }
        }
    }
}
