using Microsoft.ReportingServices.Diagnostics.Internal;
using Org.BouncyCastle.Crypto;
using Saobracaj.TrackModal.Sifarnici;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Skladista
{
    public partial class PrijemnicaCarinskoSkladiste : Form
    {
        string Tip;
        int RN;
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        string tKorisnik = Saobracaj.Sifarnici.frmLogovanje.user;
        public PrijemnicaCarinskoSkladiste(string tip,int rn)
        {
            InitializeComponent();
            Tip = tip;
            RN = rn;
            InitTable();
            FillCombo();
            VratiPodatke(RN);
            FillDodatneUsluge(RN.ToString());
            VratiStavkePrijemnice(Convert.ToInt32(txtPrijemnica.Text));

            panel6.Visible = false;
            panel7.Visible = false;
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

        int MagacinskiBroj;
        string tipMB;
        int DodatneUslugeID;
        int Aktivan;
        string Vrsta;
        int Formiran;
        private void VratiPodatke(int id)
        {
            var conn = new SqlConnection(connection);
            conn.Open();
            var cmd = new SqlCommand(@"  SELECT RadniNalogSkladista.ID
      ,RadniNalogSkladista.Status
      ,RadniNalogSkladista.Datum
      ,[Korisnik]
      ,[VrstaRN]
      ,[TipRN]
      ,[CarinskoSkladiste]
      ,[MagacinskiBroj]
      ,[Nalogodavac]
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
	  ,RNCarinskoSkladistePrijemnica.ID as Prijemnica,RadniNalogSkladista.CarinskiPostupak as CarinskiPostupak,SmestajniDokument,Rok,Posiljalac,Faktura,CRM,uProcesu
    FROM RadniNalogSkladista
inner join RNCarinskoSkladistePrijemnica on RadniNalogSkladista.ID=RNCarinskoSkladistePrijemnica.RN
Where RadniNalogSkladista.ID=" + id, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtID.Text = dr["ID"].ToString();
                Vrsta = dr["VrstaRN"].ToString();
                Tip = dr["TipRN"].ToString();
                textBox1.Text = dr["CarinskoSkladiste"].ToString();
                cboNalogodavac.SelectedValue = Convert.ToInt32(dr["Nalogodavac"].ToString());
                cboCarinskiPostupak.SelectedValue = Convert.ToInt32(dr["CarinskiPostupak"].ToString());
                cboVlasnikRobe.SelectedValue = Convert.ToInt32(dr["VlasnikRobe"].ToString());
                txtNacinPakovanja.Text = dr["NacinPakovanja"].ToString();
                txtVrstaRobe.Text = dr["VrstaRobe"].ToString();
                cboADR.SelectedValue = Convert.ToInt32(dr["OstalaSkladista"].ToString());
                txtPIB.Text = dr["PIB"].ToString();
               
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
                dateTimePicker1.Value = Convert.ToDateTime(dr["PlaniraniDatumPrijem"].ToString());
                dateTimePicker2.Value = Convert.ToDateTime(dr["PlaniraniDatum2Prijem"].ToString());
                txtKontejnerPrijem.Text = dr["BrojKontejneraPrijem"].ToString();
                txtPosebniUslovi.Text = dr["PosebniUslovi"].ToString();
                txtNapomena.Text = dr["Napomena"].ToString();
                Aktivan = Convert.ToInt32(dr["Aktivan"].ToString());
                Formiran = Convert.ToInt32(dr["Formiran"].ToString());
                if (MagacinskiBroj != 0)
                {
                    cboMagacinskiBroj.SelectedValue = MagacinskiBroj;
                }
                txtPrijemnica.Text = dr["Prijemnica"].ToString();
                DodatneUslugeID = Convert.ToInt32(dr["DodatneUslugeID"].ToString());
                if (dr["CarinskiPostupak"] != DBNull.Value)
                {
                    cboCarinskiPostupak.SelectedValue = Convert.ToInt32(dr["CarinskiPostupak"].ToString());
                }
                txtSmestajniDokument.Text = dr["SmestajniDokument"].ToString();
                txtRok.Text = dr["Rok"].ToString();
                if (dr["Posiljalac"] != DBNull.Value)
                {
                    cboPosiljalac.SelectedValue = Convert.ToInt32(dr["Posiljalac"].ToString());
                }
                txtFaktura.Text = dr["Faktura"].ToString();
                txtCRM.Text = dr["CRM"].ToString();

                int proces = Convert.ToInt32(dr["uProcesu"].ToString());
                if (proces == 1)
                {
                    chkUprocesu.Checked = true;
                }
            }
            conn.Close();
        }
        private void VratiStavkePrijemnice(int prijemnica)
        {
            list.Clear();

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                string query = @"
        SELECT 
            RB,
            NHM,
            RNCarinskoPrijemnicaStavke.Naziv as Naziv,
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
        FROM RNCarinskoPrijemnicaStavke
        LEFT JOIN TipPalete 
            ON TipPalete.ID = RNCarinskoPrijemnicaStavke.VrstaPaleta
        WHERE IDNadredjena = @ID
        ORDER BY RB";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", prijemnica);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var stavka = new PrijemnicaStavke
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
        private void FillCombo()
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
            }

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

            var mu = "select ID, Naziv from MestaUtovara order by Naziv";
            var muAD = new SqlDataAdapter(mu, conn);
            var muDS = new System.Data.DataSet();
            muAD.Fill(muDS);
            cboMestoIstovaraPrijem.DataSource = muDS.Tables[0];
            cboMestoIstovaraPrijem.DisplayMember = "Naziv";
            cboMestoIstovaraPrijem.ValueMember = "ID";

            var jm = "Select MeSifra From MerskeEnote order by MeSifra asc";
            var jmDA = new SqlDataAdapter(jm, conn);
            var jmDS = new System.Data.DataSet();
            jmDA.Fill(jmDS);
            cboJM.DataSource = jmDS.Tables[0];
            cboJM.DisplayMember = "MeSifra";
            cboJM.ValueMember = "MeSifra";

            var valutaS = "Select VaSifra,VaNaziv From Valute Order by VaSifra asc";
            var daValutaS = new SqlDataAdapter(valutaS, conn);
            var dsValutaS = new System.Data.DataSet();
            daValutaS.Fill(dsValutaS);
            cboValutaStavka.DataSource = dsValutaS.Tables[0];
            cboValutaStavka.DisplayMember = "VaNaziv";
            cboValutaStavka.ValueMember = "VaSifra";

            var paleta = "Select ID,Naziv From TipPalete order by ID asc";
            var daPaleta = new SqlDataAdapter(paleta, conn);
            var dsPaleta = new System.Data.DataSet();
            daPaleta.Fill(dsPaleta);
            cboVrstaPaleta.DataSource = dsPaleta.Tables[0];
            cboVrstaPaleta.DisplayMember = "Naziv";
            cboVrstaPaleta.ValueMember = "ID";

            var postupak = "Select Id,Naziv From VrstaCarinskogPostupka order by ID desc";
            var daPostupak = new SqlDataAdapter(postupak, conn);
            var dsPostupak = new System.Data.DataSet();
            daPostupak.Fill(dsPostupak);
            cboCarinskiPostupak.DataSource = dsPostupak.Tables[0];
            cboCarinskiPostupak.DisplayMember = "Naziv";
            cboCarinskiPostupak.ValueMember = "Id";

            var posiljalac= "Select PaSifra,PaNaziv From Partnerji Where Posiljalac=1 order by PaSifra";
            var daPosiljalac = new SqlDataAdapter(posiljalac, conn);
            var dsPosiljalac = new System.Data.DataSet();
            daPosiljalac.Fill(dsPosiljalac);
            cboPosiljalac.DataSource = dsPosiljalac.Tables[0];
            cboPosiljalac.DisplayMember = "PaNaziv";
            cboPosiljalac.ValueMember = "PaSifra";


            var pozicija = "Select ID,Naziv From Skladista Order by id asc";
            var daPozicija = new SqlDataAdapter(pozicija, conn);
            var dsPozicija = new System.Data.DataSet();
            daPozicija.Fill(dsPozicija);
            cboPozicija.DataSource = dsPozicija.Tables[0];
            cboPozicija.DisplayMember = "Naziv";
            cboPozicija.ValueMember = "ID";

        }
        
        private void cboMagacinskiBroj_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }
        public class PrijemnicaStavke
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
        private List<PrijemnicaStavke> list = new List<PrijemnicaStavke>();
        private int rb = 1;

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

        private void OcistiPoljaStavke()
        {
            selektovanaStavka = null;
            selektovaniRB = 0;
            nhm = 0;

            txtArtikal.Text = "";
            txtNaimenovanje.Text = "";
            txtKoleta.Text = "";
            txtBruto.Text = "";
            txtNeto.Text = "";
            txtVrednost.Text = "";
            txtPaleta.Text = "";
            txtPDV.Text = "";
            txtCarina.Text = "";
            txtNapomenaStavke.Text = "";

            if (cboJM.Items.Count > 0) cboJM.SelectedIndex = 0;
            if (cboValutaStavka.Items.Count > 0) cboValutaStavka.SelectedIndex = 0;
            if (cboPozicija.Items.Count > 0) cboPozicija.SelectedIndex = 0;
            if (cboVrstaPaleta.Items.Count > 0) cboVrstaPaleta.SelectedIndex = 0;
        }
        private PrijemnicaStavke selektovanaStavka = null;
        private int selektovaniRB = 0;
        private void PopuniPoljaIzStavke(PrijemnicaStavke stavka)
        {
            if (stavka == null)
                return;

            selektovanaStavka = stavka;
            selektovaniRB = stavka.RB;
            nhm = stavka.NHM;

            txtArtikal.Text = stavka.Naziv;
            txtNaimenovanje.Text = stavka.Naimenovanje;
            txtKoleta.Text = stavka.Koleta.ToString();
            txtBruto.Text = stavka.Bruto.ToString();
            txtNeto.Text = stavka.Neto.ToString();
            txtVrednost.Text = stavka.Vrednost.ToString();
            txtPaleta.Text = stavka.Paleta.ToString();
            txtPDV.Text = stavka.PDV.ToString();
            txtCarina.Text = stavka.Carina.ToString();
            txtNapomenaStavke.Text = stavka.Napomena;

            if (!string.IsNullOrWhiteSpace(stavka.JM))
                cboJM.SelectedValue = stavka.JM;

            if (!string.IsNullOrWhiteSpace(stavka.Valuta))
                cboValutaStavka.SelectedValue = stavka.Valuta;

            if (!string.IsNullOrWhiteSpace(stavka.Pozicija))
                cboPozicija.Text = stavka.Pozicija;

            if (stavka.VrstaPaleta != 0)
                cboVrstaPaleta.SelectedValue = stavka.VrstaPaleta;
        }

        private void ReorderRB()
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].RB = i + 1;
            }

            rb = list.Count + 1;
        }

        private PrijemnicaStavke VratiSelektovanuStavkuIzGrida()
        {
            try
            {
                if (gridGroupingControl1.Table == null)
                    return null;

                if (gridGroupingControl1.Table.CurrentRecord == null)
                    return null;

                return gridGroupingControl1.Table.CurrentRecord.GetData() as PrijemnicaStavke;
            }
            catch
            {
                return null;
            }
        }
        private void btnDodaj_Click(object sender, EventArgs e)
        {
            try
            {
                var postojeca = selektovanaStavka;

                if (postojeca == null && selektovaniRB > 0)
                {
                    postojeca = list.FirstOrDefault(x => x.RB == selektovaniRB);
                }

                if (postojeca == null)
                {
                    var stavka = new PrijemnicaStavke
                    {
                        RB = rb,
                        NHM = nhm,
                        Naziv = txtArtikal.Text.Trim(),
                        Naimenovanje = txtNaimenovanje.Text.Trim(),
                        JM = cboJM.SelectedValue == null ? "" : cboJM.SelectedValue.ToString(),
                        Koleta = GetDecimal(txtKoleta),
                        Bruto = GetDecimal(txtBruto),
                        Neto = GetDecimal(txtNeto),
                        Vrednost = GetDecimal(txtVrednost),
                        Valuta = cboValutaStavka.SelectedValue == null ? "" : cboValutaStavka.SelectedValue.ToString(),
                        Pozicija = cboPozicija.Text,
                        Paleta = GetInt(txtPaleta),
                        VrstaPaleta = cboVrstaPaleta.SelectedValue == null ? 0 : Convert.ToInt32(cboVrstaPaleta.SelectedValue),
                        PaletaOpis = cboVrstaPaleta.Text,
                        DimenzijePaleta = "",
                        PDV = GetDecimal(txtPDV),
                        Carina = GetDecimal(txtCarina),
                        Napomena = txtNapomenaStavke.Text.Trim()
                    };

                    list.Add(stavka);
                }
                else
                {
                    postojeca.NHM = nhm;
                    postojeca.Naziv = txtArtikal.Text.Trim();
                    postojeca.Naimenovanje = txtNaimenovanje.Text.Trim();
                    postojeca.JM = cboJM.SelectedValue == null ? "" : cboJM.SelectedValue.ToString();
                    postojeca.Koleta = GetDecimal(txtKoleta);
                    postojeca.Bruto = GetDecimal(txtBruto);
                    postojeca.Neto = GetDecimal(txtNeto);
                    postojeca.Vrednost = GetDecimal(txtVrednost);
                    postojeca.Valuta = cboValutaStavka.SelectedValue == null ? "" : cboValutaStavka.SelectedValue.ToString();
                    postojeca.Pozicija = cboPozicija.Text;
                    postojeca.Paleta = GetInt(txtPaleta);
                    postojeca.VrstaPaleta = cboVrstaPaleta.SelectedValue == null ? 0 : Convert.ToInt32(cboVrstaPaleta.SelectedValue);
                    postojeca.PaletaOpis = cboVrstaPaleta.Text;
                    postojeca.PDV = GetDecimal(txtPDV);
                    postojeca.Carina = GetDecimal(txtCarina);
                    postojeca.Napomena = txtNapomenaStavke.Text.Trim();
                }

                ReorderRB();
                OsveziGrid();
                OcistiPoljaStavke();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri unosu/izmeni stavke: " + ex.Message);
            }
        }

        private void btnMapa_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuBezPrava(() => new TerminalMap.TerminalMapFRM());
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

        private void FillNHM()
        {
            SqlConnection conn = new SqlConnection(connection);
            var query = "select ID,Broj,RTRIM(Naziv) as Naziv from NHM order by ID asc";
            var da = new SqlDataAdapter(query, conn);
            var ds = new System.Data.DataSet();

            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;

        }

        private void NHMKod()
        {
            SqlConnection conn = new SqlConnection(connection);
            var query = "select ID,Broj,RTRIM(Naziv) as Naziv from NHM Where Broj like '%"+txtNhm.Text.ToString().TrimEnd()+"%'order by ID asc";
            var da = new SqlDataAdapter(query, conn);
            var ds = new System.Data.DataSet();

            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
        }
        private void NHMNaziv()
        {
            SqlConnection conn = new SqlConnection(connection);
            var query = "select ID,Broj,RTRIM(Naziv) as Naziv from NHM Where Naziv like '%" + txtNhmNaziv.Text.ToString().TrimEnd() + "%'order by ID asc";
            var da = new SqlDataAdapter(query, conn);
            var ds = new System.Data.DataSet();

            da.Fill(ds);
            dataGridView1.ReadOnly = false;
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;

        }

        private void btnNHM_Click(object sender, EventArgs e)
        {
            panel6.Visible = true;
            FillNHM();
            txtNhm.Text = "";
            txtNhmNaziv.Text = "";
        }

        private void txtNhm_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtNhm.Text != "")
            {
                NHMKod();
            }
            else
            {
                FillNHM();
            }
        }

        private void txtNhmNaziv_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtNhmNaziv.Text != "")
            {
                NHMNaziv();
            }
            else
            {
                FillNHM();
            }
        }

        private void btnNHMNazad_Click(object sender, EventArgs e)
        {
            txtNhm.Text = "";
            txtNhmNaziv.Text = "";
            panel6.Visible = false;
        }
        int nhm = 0;

        private void btnNHMDodaj_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    nhm = Convert.ToInt32(row.Cells["ID"].Value);
                    txtArtikal.Text = row.Cells["Naziv"].Value.ToString().TrimEnd();
                }
            }
            panel6.Visible = false;
        }

        private void btnPaleta_Click(object sender, EventArgs e)
        {
            frmTipPalete frm = new frmTipPalete();
            frm.Show();
        }

        private void btnSnimi_Click(object sender, EventArgs e)
        {
            InsertCarinskoSkladiste ins = new InsertCarinskoSkladiste();
            try
            {
                int proces;
                if (chkUprocesu.Checked)
                {
                    proces = 1;
                }
                else
                {
                    proces = 0;
                }
                    ins.UpdatePrijemnicaCarinska(tKorisnik, Convert.ToInt32(txtPrijemnica.Text), Convert.ToInt32(txtID.Text), Convert.ToInt32(cboCarinskiPostupak.SelectedValue),
                        txtSmestajniDokument.Text.ToString().TrimEnd(), txtRok.Text.ToString().TrimEnd(), Convert.ToInt32(cboPosiljalac.SelectedValue), txtFaktura.Text.ToString().TrimEnd(),
                        txtCRM.Text.ToString().TrimEnd(), "OD",proces);

                foreach(var i in list)
                {
                    ins.InsertPrijemnicaCarinskaStavke(Convert.ToInt32(txtPrijemnica.Text), i.RB, i.NHM, i.Naziv, i.Naimenovanje, i.JM, i.Koleta, i.Bruto, i.Vrednost, i.Valuta,
                        i.Pozicija, i.Paleta, i.VrstaPaleta, Convert.ToInt32(i.PDV), Convert.ToInt32(i.Carina),Convert.ToDecimal(i.Neto),i.Napomena);
                }

                MessageBox.Show("Sačuvane stavke prijemnice");
            }
            catch(Exception ex)
            {
                MessageBox.Show("ERROR Insert prijemnica:" + ex.ToString());
            }
        }

        private void NalogZaViljuskaristu()
        {
            SqlConnection conn = new SqlConnection(connection);
            var query = "SELECT IDNadredjena as Prijemnica,Pozicija,VrstaPaleta,TipPalete.Naziv as Paleta,SUM(Koleta) AS Koleta,SUM(Paleta) AS Paleta,SUM(BRuto) as Bruto,Sum(Neto) as Neto " +
                "FROM RNCarinskoPrijemnicaStavke " +
                "left join TipPalete on RNCarinskoPrijemnicaStavke.VrstaPaleta=TipPalete.ID " +
                "Where IDNadredjena=" + Convert.ToInt32(txtPrijemnica.Text) + " GROUP BY IDNadredjena,Pozicija,VrstaPaleta,TipPalete.Naziv ORDER BY IDNadredjena,Pozicija,VrstaPaleta;";
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

            label49.Text = "Prijemnica: " + txtPrijemnica.Text.ToString();

            var viljuskarista = "SELECT DeSifra, (RTrim(DeIme)+' '+RTrim(DePriimek)) as Zaposleni from Delavci Where Viljuskarista=1";
            var daViljuskarista = new SqlDataAdapter(viljuskarista, conn);
            var dsViljuskarista = new System.Data.DataSet();
            daViljuskarista.Fill(dsViljuskarista);
            cboRukovalac.DataSource = dsViljuskarista.Tables[0];
            cboRukovalac.DisplayMember = "Zaposleni";
            cboRukovalac.ValueMember = "DeSifra";

        }
        int viljuskaristaID;
        private void btnNalog_Click(object sender, EventArgs e)
        {
            panel7.Visible = true;
            NalogZaViljuskaristu();
        }

        private void btnVratiNalog_Click(object sender, EventArgs e)
        {
            panel7.Visible = false;
        }

        private void cboRukovalac_SelectionChangeCommitted(object sender, EventArgs e)
        {
            viljuskaristaID= Convert.ToInt32(cboRukovalac.SelectedValue);
        }

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

            int rnID = 0;
            conn.Open();
            var cmd2 = new SqlCommand(@"select (ISNULL(Max(ID)+1,0)) from RNCarinskoSkladisteRukovalac", conn);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                rnID = Convert.ToInt32(dr2[0].ToString());
            }
            conn.Close();

            if(rnID==0)
            {
                rnID = 1;
            }

            InsertCarinskoSkladiste ins = new InsertCarinskoSkladiste();
            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    ins.InsertNalogRukovalac(rnID, Convert.ToInt32(txtPrijemnica.Text), Convert.ToInt32(cboRukovalac.SelectedValue), row.Cells["Pozicija"].Value.ToString(), Convert.ToDecimal(row.Cells["Koleta"].Value),
                    Convert.ToInt32(row.Cells["Paleta1"].Value), Convert.ToInt32(row.Cells["VrstaPaleta"].Value), Convert.ToDecimal(row.Cells["Bruto"].Value), DodatneUslugeID, "", txtVoziloPrijem.Text.ToString().TrimEnd(),
                    1, tKorisnik);
                }
                MessageBox.Show("Kreiran nalog za rukovaoca. ID:" + rnID.ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


            //ins.InsertNalogRukovalac(Convert.ToInt32(txtPrijemnica.Text), viljuskaristaID, DodatneUslugeID);

        }
        private int ToIntExcel(object value)
        {
            if (value == null)
                return 0;

            int intResult;
            if (int.TryParse(value.ToString(), out intResult))
                return intResult;

            decimal decResult;
            if (decimal.TryParse(value.ToString(), out decResult))
                return Convert.ToInt32(decResult);

            return 0;
        }

        private decimal ToDecimalExcel(object value)
        {
            if (value == null)
                return 0;

            decimal result;
            if (decimal.TryParse(value.ToString(), out result))
                return result;

            return 0;
        }

        private string ToStringExcel(object value)
        {
            return value == null ? "" : value.ToString().Trim();
        }
        private void UcitajStavkeIzExcela()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Izaberite Excel fajl";
            ofd.Filter = "Excel fajlovi (*.xlsx;*.xls)|*.xlsx;*.xls";
            ofd.Multiselect = false;

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            Microsoft.Office.Interop.Excel.Application xlApp = null;
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook = null;
            Microsoft.Office.Interop.Excel.Worksheet xlWorksheet = null;
            Microsoft.Office.Interop.Excel.Range xlRange = null;

            try
            {
                xlApp = new Microsoft.Office.Interop.Excel.Application();
                xlWorkbook = xlApp.Workbooks.Open(ofd.FileName);
                xlWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets[1];
                xlRange = xlWorksheet.UsedRange;

                int rowCount = xlRange.Rows.Count;

                // Ako prvi red predstavlja header, kreni od 2
                for (int row = 2; row <= rowCount; row++)
                {
                    object rbObj = (xlRange.Cells[row, 1] as Microsoft.Office.Interop.Excel.Range)?.Value2;
                    object nazivObj = (xlRange.Cells[row, 2] as Microsoft.Office.Interop.Excel.Range)?.Value2;
                    object nhmObj = (xlRange.Cells[row, 3] as Microsoft.Office.Interop.Excel.Range)?.Value2;
                    object koletaObj = (xlRange.Cells[row, 4] as Microsoft.Office.Interop.Excel.Range)?.Value2;
                    object netoObj = (xlRange.Cells[row, 5] as Microsoft.Office.Interop.Excel.Range)?.Value2;
                    object brutoObj = (xlRange.Cells[row, 6] as Microsoft.Office.Interop.Excel.Range)?.Value2;
                    object napomenaObj = (xlRange.Cells[row, 7] as Microsoft.Office.Interop.Excel.Range)?.Value2;

                    bool redJePrazan =
                        rbObj == null &&
                        nazivObj == null &&
                        nhmObj == null &&
                        koletaObj == null &&
                        netoObj == null &&
                        brutoObj == null &&
                        napomenaObj == null;

                    if (redJePrazan)
                        continue;

                    var stavka = new PrijemnicaStavke
                    {
                        RB = ToIntExcel(rbObj),
                        Naziv = ToStringExcel(nazivObj),
                        NHM = ToIntExcel(nhmObj),
                        Koleta = ToDecimalExcel(koletaObj),
                        Neto = ToDecimalExcel(netoObj),
                        Bruto = ToDecimalExcel(brutoObj),
                        Napomena = ToStringExcel(napomenaObj),

                        // ostala polja ostaju prazna / 0, korisnik ih kasnije dopunjava
                        Naimenovanje = "",
                        JM = "",
                        Vrednost = 0,
                        Valuta = "",
                        Pozicija = "",
                        Paleta = 0,
                        VrstaPaleta = 0,
                        PaletaOpis = "",
                        DimenzijePaleta = "",
                        PDV = 0,
                        Carina = 0
                    };

                    list.Add(stavka);
                }

                ReorderRB();
                OsveziGrid();
                OcistiPoljaStavke();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri učitavanju Excel fajla: " + ex.Message);
            }
            finally
            {
                if (xlRange != null) Marshal.ReleaseComObject(xlRange);
                if (xlWorksheet != null) Marshal.ReleaseComObject(xlWorksheet);

                if (xlWorkbook != null)
                {
                    xlWorkbook.Close(false);
                    Marshal.ReleaseComObject(xlWorkbook);
                }

                if (xlApp != null)
                {
                    xlApp.Quit();
                    Marshal.ReleaseComObject(xlApp);
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        private void btnPackingLista_Click(object sender, EventArgs e)
        {
            UcitajStavkeIzExcela();
        }

        private void btnUkloni_Click(object sender, EventArgs e)
        {
            try
            {
                var stavka = VratiSelektovanuStavkuIzGrida();

                if (stavka == null)
                {
                    MessageBox.Show("Nijedna stavka nije selektovana.");
                    return;
                }

                var rezultat = MessageBox.Show(
                    "Da li ste sigurni da želite da uklonite selektovanu stavku?",
                    "Potvrda brisanja",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (rezultat != DialogResult.Yes)
                    return;

                list.Remove(stavka);

                ReorderRB();
                OsveziGrid();
                OcistiPoljaStavke();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri uklanjanju stavke: " + ex.Message);
            }
        }

        private void gridGroupingControl1_TableControlCellClick(object sender, Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlCellClickEventArgs e)
        {
            var stavka = VratiSelektovanuStavkuIzGrida();
            if (stavka != null)
            {
                PopuniPoljaIzStavke(stavka);
            }
        }
    }
}
