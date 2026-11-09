using Microsoft.Ajax.Utilities;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using Saobracaj.Izvoz;
using Saobracaj.MainLeget.LegNew;
using Saobracaj.Pantheon_Export;
using Saobracaj.Skladista;
using Saobracaj.Skladista_main.Dokumenta;
using Saobracaj.TrackModal.Sifarnici;
using Saobracaj.Uvoz;
using Syncfusion.GridHelperClasses;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.Windows.Forms.Tools.Design;
using Syncfusion.XlsIO.Implementation.XmlSerialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main
{
    public partial class frmNalogPrijem : Form
    {

        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

        string Tip = "Prijem";
        int IDInterni;
        string Ulaz;
        string Vrsta;
        string Status;
        string Korisnik = Saobracaj.Sifarnici.frmLogovanje.user;
        int? LOPotvrdjen = 0;
        int? MagacinskiBroj = null;
        string VlasnikRobe;
        string VlasnikSifra;
        string Skladiste;



        public frmNalogPrijem(string status, string tip, string vrsta, string ulaz, int brojNaloga, int? lopotvrdjen)
        {
            InitializeComponent();
            Status = status; // komercijalninalog, novi, lo
            Tip = tip; // prijem, otprema
            Vrsta = vrsta;//carinsko, komercijalno
            Ulaz = ulaz; // uvoz, izvoz, direktni
            int LOPotvrdjen = (lopotvrdjen.HasValue && lopotvrdjen.Value == 1) ? 1 : 0;
            podesiPolja(Status);
            txtKorisnik.Text = Korisnik;
            txtKorisnik.Enabled = false;
            if (brojNaloga > 0)
            {
                txtID.Text = brojNaloga.ToString();

            }

        }
        private void podesiPolja(string Status)
        {

            if (Ulaz == "Direktni")
            {
                if (Status.ToUpper() == "NOVI" || Status.ToUpper() == "KOMERCIJALNINALOG")
                {
                    lblCarinskoSkladiste.Visible = txtCarinskoSkladiste.Visible = false;
                    lblMagacinskiBroj.Visible = cboMagacinskiBroj.Visible = btnMagacinskiBroj.Visible = false;
                    lblVlasnikRobe.Visible = cboVlasnikRobe.Visible = false;
                    lblOpisRobe.Visible = txtOpisRobe.Visible = false;
                    lblOstalaSkladista.Visible = cboOstalaSkladista.Visible = false;
                    lblPIB.Visible = txtPIB.Visible = false;
                    lblPosebniUslovi.Visible = cboPosebniUslovi.Visible = false;
                    lblSmestajniDokument.Visible = txtSmestajniDokument.Visible = false;
                    if (Status.ToUpper() == "KOMERCIJALNINALOG" && LOPotvrdjen == 0)
                    {
                        btnLOPotvrda.Visible = true;
                        btnDelete.Visible = true;
                    }

                    panelMP.Visible = false;
                    panelStavke.Visible = false;
                    panelUnosStavki.Visible = false;

                    //   panelMP.Visible = false;

                    lblTitle.Text = "Komercijalni nalog " + Tip.ToUpper();
                }
                else if (Status.ToUpper() == "LO")
                {

                    panelMP.Visible = false;
                    panelStavke.Visible = false;
                    panelUnosStavki.Visible = false;
                    cboNalogodavac.Enabled = false;
                    cboCarinskiPostupak.Enabled = false;
                    //cboMestoIstovaraPrijem.Enabled = false;
                    //cboAdresaPrijem.Enabled = false;
                    //cboKontaktOsobaPrijem.Enabled = false;
                    planiranoVremePrijem.Enabled = false;
                    cboADR.Enabled = false;
                    cboCarinarnicaPrijem.Enabled = false;
                    cboSpediterPrijem.Enabled = false;
                    cboKontakOsobaSpediterPrijem.Enabled = false;
                    cboInspekciskiTretman.Enabled = false;
                    txtOpisPosla.Enabled = false;
                    lblTitle.Text = "LO nalog" + Tip.ToUpper();
                    txtPIB.Enabled = false;
                    panelMP.Visible = true;
                    panelStavke.Visible = true;
                    panelUnosStavki.Visible = true;

                    txtMagacinskiBroj.ReadOnly = true;
                    txtVlasnikSifra.ReadOnly = true;
                    txtVlasnikRobe.ReadOnly = true;
                    txtVrstaCP.ReadOnly = true;
                    txtCI.ReadOnly = true;
                    txtSklad.ReadOnly = true;
                    numGodina.Value = DateTime.Now.Year;


                    postaviPoljaMagacinski();

                    lblTitle.Text = "Nalog za " + Tip.ToUpper();
                }




                if (Vrsta.ToUpper() == "KOMERCIJALNO")
                {
                    lblCarinskoSkladiste.Visible = txtCarinskoSkladiste.Visible = false;
                    lblCarinskiPostupak.Visible = cboCarinskiPostupak.Visible = false;
                    lblCarinarnicaPrijem.Visible = cboCarinarnicaPrijem.Visible = false;
                    lblSpediterPrijem.Visible = cboSpediterPrijem.Visible = false;
                    lblKontakOsobaSpediterPrijem.Visible = cboKontakOsobaSpediterPrijem.Visible = false;
                    //  lblvremeRealizacijaPrijem.Visible = vremeRealizacijaPrijem.Visible = false;

                }
            }
            else if (Ulaz == "Izvoz")
            {
                postaviPoljaIzvoz();
            }
            this.PerformLayout();
            this.Refresh();

        }

        private void postaviPoljaMagacinski()
        {
            txtMagacinskiBroj.ReadOnly = true;
            txtVlasnikSifra.ReadOnly = true;
            txtVlasnikRobe.ReadOnly = true;
            txtVrstaCP.ReadOnly = true;
            txtCI.ReadOnly = true;
            txtSklad.ReadOnly = true;
            txtKoleta.ReadOnly = true;
            txtBruto.ReadOnly = true;
            txtNeto.ReadOnly = true;
            cboValuta.Enabled = false;
            numGodina.Value = DateTime.Now.Year;
            txtBrojJCI.ReadOnly = true;
            dtDatum.Enabled = false;
            txtVrednost.ReadOnly = true;

        }

        private void postaviPoljaIzvoz()
        {
            if (Status.ToUpper() == "KOMERCIJALNINALOG")
            {
                lblCarinskoSkladiste.Visible = txtCarinskoSkladiste.Visible = false;
                lblMagacinskiBroj.Visible = cboMagacinskiBroj.Visible = btnMagacinskiBroj.Visible = false;
                lblVlasnikRobe.Visible = cboVlasnikRobe.Visible = false;
                lblOpisRobe.Visible = txtOpisRobe.Visible = false;
                lblOstalaSkladista.Visible = cboOstalaSkladista.Visible = false;
                lblPIB.Visible = txtPIB.Visible = false;
                lblPosebniUslovi.Visible = cboPosebniUslovi.Visible = false;
                if (Status.ToUpper() == "KOMERCIJALNINALOG" && LOPotvrdjen == 0)
                {
                    btnLOPotvrda.Visible = true;
                    btnDelete.Visible = true;
                }

                panelMP.Visible = false;


                //   panelMP.Visible = false;

                lblTitle.Text = "Komercijalni nalog " + Tip.ToUpper();
            }
            else if (Status.ToUpper() == "LO")
            {


                cboNalogodavac.Enabled = false;
                cboCarinskiPostupak.Enabled = false;
                //cboMestoIstovaraPrijem.Enabled = false;
                //cboAdresaPrijem.Enabled = false;
                //cboKontaktOsobaPrijem.Enabled = false;
                planiranoVremePrijem.Enabled = false;
                cboADR.Enabled = false;
                cboCarinarnicaPrijem.Enabled = false;
                cboSpediterPrijem.Enabled = false;
                cboKontakOsobaSpediterPrijem.Enabled = false;
                cboInspekciskiTretman.Enabled = false;
                txtOpisPosla.Enabled = false;
                lblTitle.Text = "LO nalog" + Tip.ToUpper();
                txtPIB.Enabled = false;
                panelMP.Visible = true;

                postaviPoljaMagacinski();

                lblTitle.Text = "Nalog za " + Tip.ToUpper();
            }
        }
        //private void FillCombo()
        //{
        //    SqlConnection conn = new SqlConnection(connection);

        //    var dodatneUsluge = "SELECT ID, RTRIM(Naziv) AS Naziv FROM VrstaManipulacije ORDER BY ID ASC";
        //    var daDodatneUsluge = new SqlDataAdapter(dodatneUsluge, conn);
        //    var dsDodatneUsluge = new System.Data.DataSet();
        //    daDodatneUsluge.Fill(dsDodatneUsluge);
        //    cboDodatneUsluge.DataSource = dsDodatneUsluge.Tables[0];
        //    cboDodatneUsluge.DisplayMember = "Naziv";
        //    cboDodatneUsluge.ValueMember = "ID";

        //    var nalogodava = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
        //    var daNalogodava = new SqlDataAdapter(nalogodava, conn);
        //    var dsNalogodava = new System.Data.DataSet();
        //    daNalogodava.Fill(dsNalogodava);
        //    cboNalogodavac.DataSource = dsNalogodava.Tables[0];
        //    cboNalogodavac.DisplayMember = "PaNaziv";
        //    cboNalogodavac.ValueMember = "PaSifra";


        //    var partner = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
        //    var partAD = new SqlDataAdapter(partner, conn);
        //    var partDS = new System.Data.DataSet();
        //    partAD.Fill(partDS);
        //    cboVlasnikRobe.DataSource = partDS.Tables[0];
        //    cboVlasnikRobe.DisplayMember = "PaNaziv";
        //    cboVlasnikRobe.ValueMember = "PaSifra";

        //    var carinskiPostupak = "Select id,Naziv from VrstaCarinskogPostupka order by id asc";
        //    var carinskiPostupakDa = new SqlDataAdapter(carinskiPostupak, conn);
        //    var carinskiPostupakDs = new System.Data.DataSet();
        //    carinskiPostupakDa.Fill(carinskiPostupakDs);
        //    cboCarinskiPostupak.DataSource = carinskiPostupakDs.Tables[0];
        //    cboCarinskiPostupak.DisplayMember = "Naziv";
        //    cboCarinskiPostupak.ValueMember = "id";

        //    var spediter = "Select PaSifra,PaNaziv From Partnerji Where Spediter=1 order by PaSifra desc";
        //    var daSpediter = new SqlDataAdapter(spediter, conn);
        //    var dsSpediter = new System.Data.DataSet();
        //    daSpediter.Fill(dsSpediter);
        //    cboSpediterPrijem.DataSource = dsSpediter.Tables[0];
        //    cboSpediterPrijem.DisplayMember = "PaNaziv";
        //    cboSpediterPrijem.ValueMember = "PaSifra";


        //    var kospediter = $"SELECT PaKoZapSt, (Rtrim(PaKOIme) + ' ' + Rtrim(PaKoPriimek)) as Naziv FROM partnerjiKontOseba WHERE PaKOSifra = {cboSpediterPrijem.SelectedValue} ORDER BY PaKOIme";
        //    var dakSpediter = new SqlDataAdapter(kospediter, conn);
        //    var dskSpediter = new System.Data.DataSet();
        //    dakSpediter.Fill(dskSpediter);
        //    cboKontakOsobaSpediterPrijem.DataSource = dskSpediter.Tables[0];
        //    cboKontakOsobaSpediterPrijem.DisplayMember = "PaNaziv";
        //    cboKontakOsobaSpediterPrijem.ValueMember = "PaKoZapSt";


        //    var vrstaVozila = "Select ID,Naziv From VrstePrevoznogSredstva order by ID asc";
        //    var daVrstaVozila = new SqlDataAdapter(vrstaVozila, conn);
        //    var dsVrstaVozila = new System.Data.DataSet();
        //    daVrstaVozila.Fill(dsVrstaVozila);
        //    cboTipTransportaPrijem.DataSource = dsVrstaVozila.Tables[0];
        //    cboTipTransportaPrijem.DisplayMember = "Naziv";
        //    cboTipTransportaPrijem.ValueMember = "ID";



        //    var vrstaKamiona = "Select ID,Naziv From VrstaVozila order by ID asc";
        //    var daVrstaKamiona = new SqlDataAdapter(vrstaKamiona, conn);
        //    var dsVrstaKamiona = new System.Data.DataSet();
        //    daVrstaKamiona.Fill(dsVrstaKamiona);
        //    cboVrstaKamionaPrijem.DataSource = dsVrstaKamiona.Tables[0];
        //    cboVrstaKamionaPrijem.DisplayMember = "Naziv";
        //    cboVrstaKamionaPrijem.ValueMember = "ID";


        //    var carinarnica = "Select ID,Naziv From Carinarnice order by ID asc";
        //    var daCarinarnica = new SqlDataAdapter(carinarnica, conn);
        //    var dsCarinarnica = new System.Data.DataSet();
        //    daCarinarnica.Fill(dsCarinarnica);
        //    cboCarinarnicaPrijem.DataSource = dsCarinarnica.Tables[0];
        //    cboCarinarnicaPrijem.DisplayMember = "Naziv";
        //    cboCarinarnicaPrijem.ValueMember = "ID";
        //    cboCarinarnicaPrijem.SelectedValue = 140;



        //    var mu = "select ID, Naziv from MestaUtovara order by Naziv";
        //    var muAD = new SqlDataAdapter(mu, conn);
        //    var muDS = new System.Data.DataSet();
        //    muAD.Fill(muDS);
        //    cboMestoIstovaraPrijem.DataSource = muDS.Tables[0];
        //    cboMestoIstovaraPrijem.DisplayMember = "Naziv";
        //    cboMestoIstovaraPrijem.ValueMember = "ID";
        //    cboMestoIstovaraPrijem.SelectedValue = 2;


        //    var np4 = "Select ID,(Oznaka + ' ' + Naziv) as Naziv from uvNacinPakovanja order by Naziv";
        //    var npAD4 = new SqlDataAdapter(np4, conn);
        //    var npDS4 = new System.Data.DataSet();
        //    npAD4.Fill(npDS4);
        //    cboNacinPakovanja.DataSource = npDS4.Tables[0];
        //    cboNacinPakovanja.DisplayMember = "Naziv";
        //    cboNacinPakovanja.ValueMember = "ID";

        //    var adr = "Select ID, (  UNKod +' - '+ Klasa + ' - ' + Naziv  ) as Naziv From VrstaRobeADR order by UNKod";
        //    var adrSAD = new SqlDataAdapter(adr, conn);
        //    var adrSDS = new System.Data.DataSet();
        //    adrSAD.Fill(adrSDS);
        //    cboOstalaSkladista.DataSource = adrSDS.Tables[0];
        //    cboOstalaSkladista.DisplayMember = "Naziv";
        //    cboOstalaSkladista.ValueMember = "ID";
        //    cboOstalaSkladista.SelectedIndex = -1;

        //    var dir4 = "Select ID,Naziv from InspekciskiTretman order by Naziv";
        //    var dirAD4 = new SqlDataAdapter(dir4, conn);
        //    var dirDS4 = new System.Data.DataSet();
        //    dirAD4.Fill(dirDS4);
        //    cboInspekciskiTretman.DataSource = dirDS4.Tables[0];
        //    cboInspekciskiTretman.DisplayMember = "Naziv";
        //    cboInspekciskiTretman.ValueMember = "ID";
        //    cboInspekciskiTretman.SelectedIndex = -1;

        //    var adr1 = "Select ID, (  UNKod +' - '+ Klasa + ' - ' + Naziv  ) as Naziv From VrstaRobeADR order by UNKod";
        //    var adr1SAD = new SqlDataAdapter(adr, conn);
        //    var adr1SDS = new System.Data.DataSet();
        //    adr1SAD.Fill(adrSDS);
        //    cboADR.DataSource = adrSDS.Tables[0];
        //    cboADR.DisplayMember = "Naziv";
        //    cboADR.ValueMember = "ID";
        //    cboADR.SelectedIndex = -1;

        //}

        private void FillCombo()
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                // Lokalna pomoćna funkcija za punjenje combobox-a iz prosleđenog DataTable-a
                void PostaviCombo(ComboBox cbo, DataTable dt, string displayMember, string valueMember, int? defaultIndex = null, object defaultValue = null)
                {
                    cbo.DataSource = dt;
                    cbo.DisplayMember = displayMember;
                    cbo.ValueMember = valueMember;

                    if (defaultValue != null)
                        cbo.SelectedValue = defaultValue;
                    else if (defaultIndex.HasValue)
                        cbo.SelectedIndex = defaultIndex.Value;
                }

                // Generička funkcija koja izvršava upit i vraća DataTable
                DataTable UcitajTabelu(string sqlQuery, Action<SqlCommand> addParameters = null)
                {
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                    {
                        addParameters?.Invoke(cmd);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            return dt;
                        }
                    }
                }

                // 1. Dodatne usluge
                DataTable dtUsluge = UcitajTabelu("SELECT ID, RTRIM(Naziv) AS Naziv FROM VrstaManipulacije ORDER BY ID ASC");
                PostaviCombo(cboDodatneUsluge, dtUsluge, "Naziv", "ID");

                // 2. i 2.1 Partneri (Nalogodavac i Vlasnik robe)
                DataTable dtPartneri = UcitajTabelu("SELECT PaSifra, PaNaziv FROM Partnerji ORDER BY PaNaziv");
                PostaviCombo(cboNalogodavac, dtPartneri, "PaNaziv", "PaSifra");

                DataTable dtVlasnici = UcitajTabelu("SELECT id, Naziv FROM VlasniciRobe ORDER BY id ASC");
                PostaviCombo(cboVlasnikRobe, dtVlasnici, "Naziv", "id");

                // 3. Vrsta carinskog postupka
                DataTable dtCarina = UcitajTabelu("SELECT id, Naziv FROM VrstaCarinskogPostupka ORDER BY id ASC");
                PostaviCombo(cboCarinskiPostupak, dtCarina, "Naziv", "id");

                // 4. Špediter
                DataTable dtSpediteri = UcitajTabelu("SELECT PaSifra, PaNaziv FROM Partnerji WHERE Spediter = 1 ORDER BY PaSifra DESC");
                PostaviCombo(cboSpediterPrijem, dtSpediteri, "PaNaziv", "PaSifra");

                // 5. Kontakt osoba špeditera (sa parametrom radi bezbednosti)
                if (cboSpediterPrijem.SelectedValue != null && int.TryParse(cboSpediterPrijem.SelectedValue.ToString(), out int spediterSifra))
                {
                    string koQuery = "SELECT PaKoZapSt, (Rtrim(PaKOIme) + ' ' + Rtrim(PaKoPriimek)) as Naziv FROM partnerjiKontOseba WHERE PaKOSifra = @SpediterSifra ORDER BY PaKOIme";
                    DataTable dtKO = UcitajTabelu(koQuery, cmd => cmd.Parameters.AddWithValue("@SpediterSifra", spediterSifra));
                    PostaviCombo(cboKontakOsobaSpediterPrijem, dtKO, "Naziv", "PaKoZapSt");
                }

                // 6. Vrste prevoznog sredstva
                DataTable dtTransport = UcitajTabelu("SELECT ID, Naziv From VrstePrevoznogSredstva ORDER BY ID ASC");
                PostaviCombo(cboTipTransportaPrijem, dtTransport, "Naziv", "ID", defaultValue: 140);

                // 8. Carinarnice
                DataTable dtCarinarnice = UcitajTabelu("SELECT ID, Naziv FROM Carinarnice ORDER BY ID ASC");
                PostaviCombo(cboCarinarnicaPrijem, dtCarinarnice, "Naziv", "ID", defaultValue: 140);

                // 10. Način pakovanja
                DataTable dtPakovanje = UcitajTabelu("SELECT ID, (Oznaka + ' ' + Naziv) as Naziv FROM uvNacinPakovanja ORDER BY Naziv");
                PostaviCombo(cboNacinPakovanja, dtPakovanje, "Naziv", "ID");

                // 11. & 13. ADR / Ostala skladišta (Učitavamo jednom, pa delimo preko .Copy())
                DataTable dtAdr = UcitajTabelu("SELECT ID, (UNKod +' - '+ Klasa + ' - ' + Naziv) as Naziv FROM VrstaRobeADR ORDER BY UNKod");
                PostaviCombo(cboOstalaSkladista, dtAdr.Copy(), "Naziv", "ID", defaultIndex: -1);
                PostaviCombo(cboADR, dtAdr.Copy(), "Naziv", "ID", defaultIndex: -1);

                // 12. Inspekcijski tretman
                DataTable dtInspekcija = UcitajTabelu("SELECT ID, Naziv FROM InspekciskiTretman ORDER BY Naziv");
                PostaviCombo(cboInspekciskiTretman, dtInspekcija, "Naziv", "ID", defaultIndex: -1);

                // 14. Posebni Uslovi Smestaja
                DataTable dtUslovi = UcitajTabelu("SELECT ID, Naziv FROM PosebniUsoviSmestaja ORDER BY Naziv");
                PostaviCombo(cboPosebniUslovi, dtUslovi, "Naziv", "ID", defaultIndex: -1);

                // 15. Valute
                DataTable dtValute = UcitajTabelu("SELECT VaSifra, VaNaziv FROM Valute");
                PostaviCombo(cboValuta, dtValute, "VaNaziv", "VaSifra", defaultIndex: -1);

                PostaviCombo(cboValutaStavka, dtValute, "VaNaziv", "VaSifra", defaultIndex: -1);

                DataTable dtMerskeEnote = UcitajTabelu("SELECT MeSifra FROM MerskeEnote");
                PostaviCombo(cboJM, dtMerskeEnote, "MeSifra", "MeSifra", defaultIndex: -1);

                DataTable dtTipPalete = UcitajTabelu("SELECT ID,Naziv FROM TipPalete");
                PostaviCombo(cboVrstaPaleta, dtTipPalete, "Naziv", "ID", defaultIndex: -1);


            }
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

                //string query = @"SELECT d.Usluga, RTRIM(v.Naziv) Naziv
                //         FROM RNCarinskoSkladisteDodatneUsluge d
                //         INNER JOIN VrstaManipulacije v ON d.Usluga = v.ID
                //         WHERE d.RN = @RN";


                string query = @"SELECT d.IDManipulacijaJED AS Usluga, RTRIM(v.Naziv) Naziv
                                FROM RadniNalogInterni d
                                INNER JOIN VrstaManipulacije v ON d.IDManipulacijaJED = v.ID
                                WHERE d.BrojRN = @RN and d.IDManipulacijaJED <> 74 and TipRN = 'RN20' ";

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

        private void label18_Click(object sender, EventArgs e)
        {

        }

 

        private void frmNalogPrijem_Load(object sender, EventArgs e)
        {

            FillCombo();
            InitTable();
            FillDodatneUsluge(txtID.Text);
            if (!txtID.Text.IsNullOrWhiteSpace() && Convert.ToInt32(txtID.Text) > 0)
            {
                VratiPodatke(Convert.ToInt32(txtID.Text));
                VratiPodatkeMP();
                VratiCarinskeStavke();
                this.gridGroupingControl1.QueryCustomSummary += GridGroupingControl1_QueryCustomSummary;

            }
            txtID.ReadOnly = true;
        }


        private void button24_Click(object sender, EventArgs e)
        {
            int? nalogodavac = null;
            int? carinskiPostupak = null;
            int? nacinPakovanja = null;
            int? tipTransporta = null;
            int? vrstaKamionaPrijem = null;

            int? vaganje = null;

            string opisPosla = string.IsNullOrWhiteSpace(txtOpisPosla.Text) ? null : txtOpisPosla.Text.Trim();

            // Pokušaj konverzije u int
            if (int.TryParse(cboNalogodavac.SelectedValue.ToString(), out int nalogodavacInt))
            {
                nalogodavac = nalogodavacInt;
            }
            if (int.TryParse(cboCarinskiPostupak.SelectedValue.ToString(), out int carinskiPostupakInt))
            {
                carinskiPostupak = carinskiPostupakInt;
            }
            if (int.TryParse(cboNacinPakovanja.SelectedValue.ToString(), out int nacinPakovanjaInt))
            {

                nacinPakovanja = nacinPakovanjaInt;
            }
            
            if (cboTipTransportaPrijem != null && cboTipTransportaPrijem.SelectedValue != null && int.TryParse(cboTipTransportaPrijem.SelectedValue.ToString(), out int tipTransportaInt))
            {
                tipTransporta = tipTransportaInt;
            }
            //if (int.TryParse(cboVrstaKamionaPrijem.SelectedValue.ToString(), out int vrstaKamionaPrijemInt))
            //{
            //    vrstaKamionaPrijem = vrstaKamionaPrijemInt;
            //}

            //string voziloPrijem = string.IsNullOrWhiteSpace(txtVoziloPrijem.Text) ? null : txtVoziloPrijem.Text.Trim();
            //string vozacPrijem = string.IsNullOrWhiteSpace(txtVozacPrijem.Text) ? null : txtVozacPrijem.Text.Trim();
            //string lKPrijem = string.IsNullOrWhiteSpace(txtLKPrijem.Text) ? null : txtLKPrijem.Text.Trim();
            //string telefonPrijem = string.IsNullOrWhiteSpace(txtTelefonPrijem.Text) ? null : txtTelefonPrijem.Text.Trim();


            DateTime? planiranoVremeItsovara = planiranoVremePrijem.Value;
            DateTime? novoPlaniranoVremeItsovara = novoVremePrijem.Value;
            DateTime? realizacijaVremeItsovara = vremeRealizacijaPrijem.Value;


            if (chkVaganje.Checked == true)
            {
                vaganje = 1;
            }

            int? kontaktOsobaSpediter = null;
            if (cboKontakOsobaSpediterPrijem.SelectedValue != null)
            {
                kontaktOsobaSpediter = Convert.ToInt32(cboKontakOsobaSpediterPrijem.SelectedValue);
            }

            int? adr = null;
            if (cboADR.SelectedValue != null)
            {
                adr = Convert.ToInt32(cboADR.SelectedValue);
            }

            int? inspekcijskiTretman = null;
            if (cboInspekciskiTretman.SelectedValue != null)
            {
                inspekcijskiTretman = Convert.ToInt32(cboInspekciskiTretman.SelectedValue);
            }


            string Napomena = string.IsNullOrWhiteSpace(txtNapomena.Text) ? null : txtNapomena.Text.Trim();

            int? vlasnikRobe = null;
            if (cboVlasnikRobe.SelectedValue != null)
            {
                vlasnikRobe = Convert.ToInt32(cboVlasnikRobe.SelectedValue);
            }

            string opisRobe = string.IsNullOrWhiteSpace(txtOpisRobe.Text) ? null : txtOpisRobe.Text.Trim();
            int? posebniUslovi = null;
            if (cboPosebniUslovi.SelectedValue != null)
            {
                posebniUslovi = Convert.ToInt32(cboPosebniUslovi.SelectedValue);
            }

            int? magacinskiBroj = null;
            if (cboMagacinskiBroj.SelectedValue != null)
            {
                magacinskiBroj = Convert.ToInt32(cboMagacinskiBroj.SelectedValue);
            }


            int? ostalaSkladista = null;
            if (cboOstalaSkladista.SelectedValue != null)
            {
                ostalaSkladista = Convert.ToInt32(cboOstalaSkladista.SelectedValue);
            }
            string carinskoSkladiste = null;
            if (Vrsta.ToUpper() == "CARINSKO")
                carinskoSkladiste = "1008";

            string smestajniDokument = string.IsNullOrWhiteSpace(txtSmestajniDokument.Text) ? null : txtSmestajniDokument.Text.Trim();
            int upisPodatakaUMP = 0;
            // samo kada je vec lo potvrdjen onda se upisuju podaci u MP
            if (Status.ToUpper() == "LO")
                upisPodatakaUMP = 1;
            int? godina = null;
            if (numGodina.Value != null)
            {
                godina = Convert.ToInt32(numGodina.Value);
            }



            InsertCarinskoSkladiste insc = new InsertCarinskoSkladiste();
            if (txtID.Text.IsNullOrWhiteSpace())
            {

                int noviID = insc.InsertRadniNalogDirektni("Kreiran", DateTime.Now, Korisnik, Vrsta, Tip, carinskoSkladiste, nalogodavac,
                               carinskiPostupak, opisPosla,
                               nacinPakovanja, tipTransporta,
                               Convert.ToInt32(cboCarinarnicaPrijem.SelectedValue),
                               Convert.ToInt32(cboSpediterPrijem.SelectedValue), Convert.ToInt32(cboKontakOsobaSpediterPrijem.SelectedValue),

                               Convert.ToDateTime(planiranoVremePrijem.Value), Convert.ToDateTime(novoVremePrijem.Value), vaganje, adr, posebniUslovi, 0,
                               txtNapomena.Text.ToString().TrimEnd(), inspekcijskiTretman, 1, 0);
                txtID.Text = noviID.ToString();
            }
            else  // update
            {

                insc.UpdateRadniNalogDirektni(Convert.ToInt32(txtID.Text), nalogodavac, carinskiPostupak, opisPosla, nacinPakovanja, tipTransporta, Convert.ToInt32(cboCarinarnicaPrijem.SelectedValue),
                              Convert.ToInt32(cboSpediterPrijem.SelectedValue), Convert.ToInt32(cboKontakOsobaSpediterPrijem.SelectedValue),
                              Convert.ToDateTime(planiranoVremePrijem.Value), Convert.ToDateTime(novoVremePrijem.Value), vaganje, adr, posebniUslovi, 0,
                              txtNapomena.Text.ToString().TrimEnd(), inspekcijskiTretman, vlasnikRobe, opisRobe, magacinskiBroj, ostalaSkladista, smestajniDokument, upisPodatakaUMP, godina);

            }


            InsertSkladista ins = new InsertSkladista();
            if (txtID.Text != "")
            {
                try

                {
                    // int nalogID = VratiNalogIDF();
                    int nalogID = Convert.ToInt32(txtID.Text); // ID komercijalnog naloga
                    InsertUvoz isu = new InsertUvoz();

                    // Kreiramo listu u koju ćemo upakovati ID-jeve usluga iz grida
                    List<int> listaUsluga = new List<int>();

                    // Prolazak kroz redove grida (prilagodite naziv vašeg grida, npr. gridGroupingControl1)
                    foreach (DataRow row in dtUsluge.Rows) // ili preko SelectedRecords / Grid stavki zavisno šta koristite
                    {
                        int uslugaID = Convert.ToInt32(row["UslugaID"]);
                        listaUsluga.Add(uslugaID);
                    }

                    // Ako imamo stavki, pozivamo klasu koja šalje SVE ODJEDNOM u bazu
                    if (listaUsluga.Count > 0)
                    {

                        insc.KreirajDodatneUslugeSkladista(nalogID, listaUsluga, Korisnik);

                        //MessageBox.Show("Usluge uspešno sačuvane!", "Obaveštenje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //  RefreshGrid();
                    }
                    //int VratiNalogID = VratiNalogIDF();
                    //Saobracaj.Uvoz.InsertRadniNalogInterni ins1 = new Saobracaj.Uvoz.InsertRadniNalogInterni();
                    //ins1.InsRadniNalogInterniIzvozSkladistePotvrda(VratiNalogID);


                    //int idUsluge = 0;
                    //using (SqlConnection conn = new SqlConnection(connection))
                    //{
                    //    conn.Open();
                    //    using (SqlCommand cmd = new SqlCommand("Select Max(ID)+1 From RNCarinskoSkladisteDodatneUsluge", conn))
                    //    {
                    //        SqlDataReader dr = cmd.ExecuteReader();
                    //        while (dr.Read())
                    //        {

                    //            idUsluge = Convert.ToInt32(dr[0].ToString());
                    //        }
                    //    }
                    //}
                    //ins.DeleteDodatneUsluge(Convert.ToInt32(txtID.Text));

                    //foreach (DataRow row in dtUsluge.Rows)
                    //{
                    //    int valueMember = Convert.ToInt32(row["UslugaID"]);
                    //    ins.InsertDodatneUsluge(idUsluge, Convert.ToInt32(txtID.Text), valueMember);
                    //    idUsluge++;
                    //}

                    MessageBox.Show("Podaci su uspešno snimljeni! ");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR Update \n:" + ex.ToString());
                    return;
                }

            }
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
            //if (dgvUsluge.CurrentRow == null)
            //    return;

            //dgvUsluge.Rows.Remove(dgvUsluge.CurrentRow);
            if (dgvUsluge.CurrentRow == null)
                return;

            // Uzimamo red iz izvora podatka (DataTable-a) i brišemo ga
            DataRowView drv = dgvUsluge.CurrentRow.DataBoundItem as DataRowView;
            if (drv != null)
            {
                drv.Row.Delete(); // Briše i iz tabele dtUsluge i iz grida
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            int VratiNalogID = VratiNalogIDF();
            Saobracaj.Uvoz.InsertRadniNalogInterni ins1 = new Saobracaj.Uvoz.InsertRadniNalogInterni();
            ins1.UpdRadniNalogInterniIzvozSkladistePotvrdjen(Convert.ToInt32(VratiNalogID));

            MessageBox.Show("LO nalog potvrdjen");

            Status = "LO";
            btnSnimi.Enabled = false;
            //  podesiPolja(Status);
        }

        private void VratiPodatke(int id)
        {
            var conn = new SqlConnection(connection);
            conn.Open();
            var cmd = new SqlCommand(@"SELECT rns.ID
                                  ,rns.Status
                                  ,rns.Datum
                                  ,rns.Korisnik
                                  ,rns.VrstaRN
                                  ,rns.TipRN
                                  ,rns.CarinskoSkladiste
                                  ,rns.MagacinskiBroj
                                  ,rns.Nalogodavac
                                  ,rns.CarinskiPostupak
                                  ,rns.OpisPosla
                                  ,mp.VlasnikRobe
                                  ,rns.VrstaRobe
                                  ,mp.OpisRobe
                                  ,rns.NacinPakovanjaInt
                                  ,rns.OstalaSkladista
                                  ,vlr.PIB
                                  ,rns.VrstaPrevoznogSredstvaOtprema
                                  ,rns.VrstaKamionaOtprema
                                  ,rns.VoziloOtprema
                                  ,rns.VozacOtprema
                                  ,rns.BrojLKOtprema
                                  ,rns.BrojTelefonaOtprema
                                  ,rns.OdredisnaCarinarnicaOtpremaOtprema
                                  ,rns.SpediterOtprema
                                  ,rns.KontakOsobaSpediteraOtprema
                                  ,rns.MestoIstovaraOtprema
                                  ,rns.AdresaOtprema
                                  ,rns.KontaktOsobaIstovarOtprema
                                  ,rns.PlaniraniDatumOtpema
                                  ,rns.PlaniraniDatum2Otprema
                                  ,rns.BrojKontejneraOtprema
                                  ,rns.VrstaPrevoznogSredstvaPrijem
                                  ,rns.VrstaKamionaPrijem
                                  ,rns.VoziloPrijem
                                  ,rns.VozacPrijem
                                  ,rns.BrojLKPrijem
                                  ,rns.BrojTelefonaPrijem
                                  ,rns.OdredisnaCarinarnicaPrijem
                                  ,rns.SpediterPrijem
                                  ,rns.KontakOsobaSpediterIntPrijem
                                  ,rns.MestoIstovaraPrijem
                                  ,rns.AdresaIntPrijem
                                  ,rns.KontaktOsobaIstovarIntPrijem
                                  ,rns.PlaniraniDatumPrijem
                                  ,rns.PlaniraniDatum2Prijem
                                  ,rns.BrojKontejneraPrijem
                                  ,rns.PosebniUslovi
                                  ,rns.DodatneUslugeID
                                  ,rns.Napomena
                                  ,rns.Aktivan
                                  ,rns.Formiran
                                  ,rns.ADR
                                  ,rns.Vaganje
                                  ,rns.InspekcijskiTretman
                                  ,rns.SmestajniDokument
                                  ,IsNull(rnp.LOSkladiste,0) AS LO
                                  ,vlr.Naziv AS VlasnikRobeNaziv
                                  ,vlr.Sifra AS VlasnikRobeSifra
                                FROM RadniNalogSkladista rns
                                       LEFT JOIN RadniNalogInterni rni on rns.ID = rni.BrojRN AND rni.IDManipulacijaJED = 74
                                       LEFT JOIN RadniNalogInterniPotvrda rnp ON  rnp.IDNaloga = rni.ID
                                       LEFT JOIN MagacinskiPredmet mp on rns.ID = mp.RN 
                                       LEFT JOIN VlasniciRobe vlr ON  vlr.id = mp.VlasnikRobe
                                Where rns.ID=" + id, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                txtID.Text = dr["ID"].ToString();
                Vrsta = dr["VrstaRN"].ToString();
                txtPIB.Text = dr["Pib"].ToString();
                Tip = dr["TipRN"].ToString();
                txtCarinskoSkladiste.Text = dr["CarinskoSkladiste"].ToString();
                if (cboNalogodavac != null && dr["Nalogodavac"] != DBNull.Value)
                    cboNalogodavac.SelectedValue = Convert.ToInt32(dr["Nalogodavac"].ToString());
                if (cboMagacinskiBroj != null && dr["MagacinskiBroj"] != DBNull.Value)
                {
                    FillMagacinskiBroj();
                    cboMagacinskiBroj.SelectedValue = Convert.ToInt32(dr["MagacinskiBroj"].ToString());
                    MagacinskiBroj = Convert.ToInt32(dr["MagacinskiBroj"].ToString());
                }

                txtOpisRobe.Text = dr["OpisRobe"].ToString();
                cboCarinskiPostupak.SelectedValue = Convert.ToInt32(dr["CarinskiPostupak"].ToString());
                txtOpisPosla.Text = dr["OpisPosla"].ToString();
                if (cboVlasnikRobe != null && dr["VlasnikRobe"] != DBNull.Value)
                    cboVlasnikRobe.SelectedValue = Convert.ToInt32(dr["VlasnikRobe"].ToString());
                if (cboOstalaSkladista != null && dr["OstalaSkladista"] != DBNull.Value)
                    cboOstalaSkladista.SelectedValue = Convert.ToInt32(dr["OstalaSkladista"].ToString());
                if (cboNacinPakovanja != null && dr["NacinPakovanjaInt"] != DBNull.Value)
                    cboNacinPakovanja.SelectedValue = Convert.ToInt32(dr["NacinPakovanjaInt"].ToString());
                if (cboADR != null && dr["ADR"] != DBNull.Value)
                    cboADR.SelectedValue = Convert.ToInt32(dr["ADR"].ToString());
                if (cboInspekciskiTretman != null && dr["InspekcijskiTretman"] != DBNull.Value)
                    cboInspekciskiTretman.SelectedValue = Convert.ToInt32(dr["InspekcijskiTretman"].ToString());
                txtPIB.Text = dr["PIB"].ToString();

                cboCarinarnicaPrijem.SelectedValue = Convert.ToInt32(dr["OdredisnaCarinarnicaPrijem"].ToString());
                if (cboSpediterPrijem != null && dr["SpediterPrijem"] != DBNull.Value)
                {
                    cboSpediterPrijem.SelectedValue = Convert.ToInt32(dr["SpediterPrijem"].ToString());

                    if (cboKontakOsobaSpediterPrijem != null && dr["KontakOsobaSpediterIntPrijem"] != DBNull.Value)
                    {
                        PopuniKontaktOsobuSpediter(cboSpediterPrijem, cboKontakOsobaSpediterPrijem);
                        cboKontakOsobaSpediterPrijem.SelectedValue = Convert.ToInt32(dr["KontakOsobaSpediterIntPrijem"].ToString());
                    }
                }
                if(dr["VrstaPrevoznogSredstvaPrijem"]!= DBNull.Value)
                    cboTipTransportaPrijem.SelectedValue = Convert.ToInt32(dr["VrstaPrevoznogSredstvaPrijem"].ToString());


                planiranoVremePrijem.Value = Convert.ToDateTime(dr["PlaniraniDatumPrijem"].ToString());
                novoVremePrijem.Value = Convert.ToDateTime(dr["PlaniraniDatum2Prijem"].ToString());

                if (cboPosebniUslovi != null && dr["PosebniUslovi"] != DBNull.Value)
                    cboPosebniUslovi.SelectedValue = Convert.ToInt32(dr["PosebniUslovi"].ToString());

                txtNapomena.Text = dr["Napomena"].ToString();
                txtSmestajniDokument.Text = dr["SmestajniDokument"].ToString();

                if (dr["Vaganje"] != DBNull.Value && Convert.ToInt32(dr["Vaganje"]) == 1)
                    chkVaganje.Checked = true;

                LOPotvrdjen = Convert.ToInt32(dr["LO"]);

                VlasnikRobe = dr["VlasnikRobeNaziv"].ToString();
                VlasnikSifra = dr["VlasnikRobeSifra"].ToString();
                Skladiste = dr["CarinskoSkladiste"].ToString();


            }
            conn.Close();
        }

        private void VratiPodatkeMP()
        {
            if (string.IsNullOrEmpty(txtID.Text))
                return;

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                // Preporuka: Koristite SQL parametar umesto spajanja stringova zbog bezbednosti
                var cmd = new SqlCommand("SELECT mp.ID, MB,vlr.Naziv as NazivVlasnika , vlr.Sifra AS SifraRobe, mp.SifraSkladista, mp.Koleta," +
                                         "mp.Neto, mp.Bruto ,mp.Vrednost, mp.Valuta, vcp.Oznaka as CarinskiPostupak, Carinarnice.CIOznaka, BrojJCI, mp.Godina " +
                                        " From RadniNalogSkladista rns " +
                                        " LEFT JOIN MagacinskiPredmet mp   ON  rns.id = mp.RN" +
                                        " LEFT JOIN VlasniciRobe vlr ON  vlr.id = mp.VlasnikRobe " +
                                        " LEFT JOIN VrstaCarinskogPostupka vcp ON  vcp.id = rns.CarinskiPostupak " +
                                        " LEFT JOIN Carinarnice ON Carinarnice.ID = rns.OdredisnaCarinarnicaPrijem " +
                                        " WHERE rns.ID = @RN", conn);
                cmd.Parameters.AddWithValue("@RN", Convert.ToInt32(txtID.Text));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    // Proveravamo da li uopšte ima redova
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            txtVlasnikRobe.Text = dr["NazivVlasnika"].ToString();
                            txtVlasnikSifra.Text = dr["SifraRobe"].ToString();
                            txtSklad.Text = dr["SifraSkladista"].ToString();

                            txtMagacinskiBroj.Text = dr["MB"].ToString();
                            txtVrstaCP.Text = dr["CarinskiPostupak"].ToString();
                            txtCI.Text = dr["CIOznaka"].ToString();
                            txtSmestajniDokument.Text = dr["BrojJCI"].ToString();
                            txtBrojJCI.Text = dr["BrojJCI"].ToString();
                            if(dr["Godina"] != null && dr["Godina"] != DBNull.Value)
                                numGodina.Value = Convert.ToInt32(dr["Godina"].ToString());
                            txtMagacinskiPredmetID.Text = dr["ID"].ToString();


                        }
                    }
                    else
                    {
                        txtVlasnikRobe.Text = VlasnikRobe;
                        txtVlasnikSifra.Text = VlasnikSifra;
                        txtSklad.Text = Skladiste;
                        txtMagacinskiBroj.Text = MagacinskiBroj.ToString();

                    }
                }
            }
        }

        int VratiNalogIDF()
        {
            int nalogID = 0;
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("select ID from RadniNalogInterni where TipRN = 'RN20' and IDManipulacijaJED = 74 and BrojRN =" + txtID.Text, conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        nalogID = Convert.ToInt32(dr["ID"].ToString());
                    }
                }
                conn.Close();
            }

            return nalogID;
        }

        private void cboSpediterPrijem_Leave(object sender, EventArgs e)
        {
            PopuniKontaktOsobuSpediter(cboSpediterPrijem, cboKontakOsobaSpediterPrijem);
        }

        private void PopuniKontaktOsobuSpediter(ComboBox cboPartner, ComboBox cboKontakt)
        {
            // Provera selekcije
            if (cboPartner.SelectedValue == null || cboPartner.SelectedValue == DBNull.Value)
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {

                    string sql = @"SELECT PaKoZapSt, 
                           (Rtrim(PaKOIme) + ' ' + Rtrim(PaKoPriimek)) as Naziv 
                           FROM partnerjiKontOseba 
                           WHERE PaKOSifra = @Sifra 
                           ORDER BY PaKOIme";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Sifra", Convert.ToInt32(cboPartner.SelectedValue));

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            cboKontakt.DataSource = dt;
                            cboKontakt.DisplayMember = "Naziv";
                            cboKontakt.ValueMember = "PaKoZapSt";
                            cboKontakt.SelectedIndex = 0;
                        }
                        else
                        {
                            // Ako nema rezultata null
                            cboKontakt.DataSource = null;
                            cboKontakt.Items.Clear();
                            cboKontakt.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška kod kontakta: " + ex.Message);
            }
        }

        private void btnMagacinskiBroj_Click(object sender, EventArgs e)
        {
            FillMagacinskiBroj();
            panelMPEdit.Visible = true;
            btnMbSave.Enabled = true;
            btnMbNazad.Enabled = true;
            panelMPEdit.BringToFront();
        }

        private void FillMagacinskiBroj()
        {
            SqlConnection conn = new SqlConnection(connection);

            if (Vrsta == "Carinsko" || Vrsta == "Komercijalo")
            {
                string tabela = "";
                if (Vrsta == "Carinsko")
                    tabela = "MagacinskiBrojCarinski";
                if (Vrsta == "Komercijalo")
                    tabela = "MagacinskiBrojKomercijalni";
                //var partner5 = "Select ID, cast(ID as nvarchar(5))+' - '+Naziv as Naziv from MagacinskiBrojCarinski order by ID Desc";


                var partner5 = $"Select ID, cast(ID as nvarchar(5))+' - '+Naziv as Naziv from {tabela} order by ID Desc";
                var partAD5 = new SqlDataAdapter(partner5, conn);
                var partDS5 = new System.Data.DataSet();
                partAD5.Fill(partDS5);
                if (!MagacinskiBroj.HasValue || MagacinskiBroj == 0)
                {
                    cboMagacinskiBroj.DataSource = partDS5.Tables[0];
                    cboMagacinskiBroj.DisplayMember = "ID";
                    cboMagacinskiBroj.ValueMember = "ID";
                }

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

                int id;

                using (SqlCommand cmd = new SqlCommand(
                    $"SELECT ISNULL(MIN(ID), 0) FROM {tabela} WHERE Naziv = ''", conn))
                {
                    id = Convert.ToInt32(cmd.ExecuteScalar());
                }

                if (id != 0)
                {
                    txtMbID.Text = id.ToString();
                    txtMbID.Enabled = false;
                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand(
                        $"SELECT ISNULL(MAX(ID), 0) + 1 FROM {tabela}", conn))
                    {
                        id = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    txtMbID.Text = id.ToString();
                    txtMbID.Enabled = false;
                }

                conn.Close();
            }
        }

        private void cboMestoIstovaraPrijem_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kada se promeni mesto istovara, automatski popuni adrese i kontakte za to mesto
            //PopuniAdresu(cboMestoIstovaraPrijem, cboAdresaPrijem);
            //PopuniKontaktOsobu(cboMestoIstovaraPrijem, cboKontaktOsobaPrijem);

        }

        private void btnMbNazad_Click(object sender, EventArgs e)
        {
            panelMPEdit.Visible = false;
        }

        private void btnMbSave_Click(object sender, EventArgs e)
        {
            InsertCarinskoSkladiste ins = new InsertCarinskoSkladiste();

            if (Vrsta == "Carinsko")
            {
                int magacinskiBroj;

                if (!int.TryParse(txtMbID.Text, out magacinskiBroj))
                {
                    MessageBox.Show("ID magacinskog broja nije ispravan.");
                    return;
                }

                string naziv = txtMbNaziv.Text.Trim();

                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();

                    bool postoji;

                    using (SqlCommand cmd = new SqlCommand(
                        "SELECT COUNT(*) FROM MagacinskiBrojCarinski WHERE ID = @ID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", magacinskiBroj);

                        int brojRedova = Convert.ToInt32(cmd.ExecuteScalar());
                        postoji = brojRedova > 0;
                    }

                    if (postoji)
                    {
                        ins.UpdateMagacinskiBrojCarinski(magacinskiBroj, naziv);
                    }
                    else
                    {
                        ins.InsertMagacinskiBrojCarinski(magacinskiBroj, naziv);
                    }
                }

                MagacinskiBroj = magacinskiBroj;

                FillMagacinskiBroj();

                cboMagacinskiBroj.SelectedValue = MagacinskiBroj;

                // Aktivan = 1;
                panelMPEdit.Visible = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (LOPotvrdjen == 1)
            {
                MessageBox.Show("Ovaj nalog nije moguće izbrisati jer ga je već potvrdio LO!");
                return;
            }
            InsertCarinskoSkladiste insc = new InsertCarinskoSkladiste();
            if (!txtID.Text.IsNullOrWhiteSpace())
            {
                insc.DeleteRadniNalog(Convert.ToInt32(txtID.Text));
                MessageBox.Show("Nalog je uspešno izbrisan!");
                OnNalogObrisan?.Invoke();



                btnSnimi.Enabled = false;
                btnLOPotvrda.Enabled = false;
                btnDelete.Enabled = false;

            }

        }
        public System.Action OnNalogObrisan { get; set; }

        private void cboVlasnikRobe_Leave(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("select PIB, Sifra, Naziv from VlasniciRobe where ID = " + Convert.ToInt32(cboVlasnikRobe.SelectedValue), conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        txtPIB.Text = (dr["PIB"].ToString());
                        txtVlasnikSifra.Text = (dr["Sifra"].ToString());
                        txtVlasnikRobe.Text = (dr["Naziv"].ToString());
                    }
                }
                conn.Close();
            }


        }

        private void btnNoviMP_Click(object sender, EventArgs e)
        {
            InsertCarinskoSkladiste ins = new InsertCarinskoSkladiste();
            bool ima = false;
            int id = 0;
            using (SqlConnection conn = new SqlConnection(connection))
            {

                conn.Open();
                var cmd = new SqlCommand(@"SELECT IsNull(ID,0) AS ID From MagacinskiPredmet Where RN=" + Convert.ToInt32(txtID.Text), conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ima = true;
                    id = Convert.ToInt32(dr["ID"].ToString());
                }

            }

            decimal? neto = null;
            if (decimal.TryParse(txtNeto.Text, out decimal parsedNeto))
                neto = parsedNeto;

            decimal? bruto = null;
            if (decimal.TryParse(txtBruto.Text, out decimal parsedBruto))
                bruto = parsedBruto;

            decimal? vrednost = null;
            if (decimal.TryParse(txtVrednost.Text, out decimal parsedVrednost))
                vrednost = parsedVrednost;

            if (ima == true)
            { //cboValutaMP.SelectedValue.ToString()


                ins.InsertMagacinskiPredmet(Convert.ToInt32(txtID.Text), Convert.ToInt32(cboMagacinskiBroj.SelectedValue), DateTime.Now, Convert.ToInt32(cboVlasnikRobe.SelectedValue),
                    0, txtCI.Text.ToString(), (txtSklad.Text), txtVrstaCP.Text.ToString(),
                    txtBrojJCI.Text.ToString(), Convert.ToDateTime(dtDatum.Value), txtOpisRobe.Text.ToString(), Convert.ToDecimal(txtKoleta.Text), neto,
                    bruto, "eur", vrednost, "");

                MessageBox.Show("Upisan magacinski predmet!");
            }
            else
            {

                ins.UpdateMagacinskiPredmet(id, Convert.ToInt32(txtID.Text), Convert.ToInt32(cboMagacinskiBroj.SelectedValue), DateTime.Now, Convert.ToInt32(cboVlasnikRobe.SelectedValue),
                    0, txtCI.Text.ToString(), (txtSklad.Text), txtVrstaCP.Text.ToString(),
                    txtBrojJCI.Text.ToString(), Convert.ToDateTime(dtDatum.Value), txtOpisRobe.Text.ToString(), Convert.ToDecimal(txtKoleta.Text), neto,
                    bruto, cboValuta.SelectedValue.ToString(), vrednost, null);

                MessageBox.Show("Upisan magacinski predmet!");
            }
        }

        private void cboMagacinskiBroj_Leave(object sender, EventArgs e)
        {
            if (cboMagacinskiBroj.SelectedValue != null && panelMP.Visible)
            {
                // Uzimamo vrednost iz combobox-a kao string (ili int, zavisno od tipa)
                string izabranaVrednost = cboMagacinskiBroj.SelectedValue.ToString();

                // Upoređujemo sa trenutnom vrednošću u txtMagacinskiBroj
                // (ako je txtMagacinskiBroj običan TextBox, koristite .Text)
                if (txtMagacinskiBroj.Text != izabranaVrednost)
                {
                    txtMagacinskiBroj.Text = izabranaVrednost;
                }
            }
        }

        private void btnPregled_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtID.Text.IsNullOrWhiteSpace())
            {
                MessageBox.Show("Morate prvo osnovne podatke naloga!");
                return;
            }
            frmVozac cpo = new frmVozac(Convert.ToInt32(txtID.Text));
            cpo.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtID.Text.IsNullOrWhiteSpace())
            {
                MessageBox.Show("Morate prvo osnovne podatke naloga!");
                return;
            }
            frmMestoIsporuke cpo = new frmMestoIsporuke(Convert.ToInt32(txtID.Text));
            cpo.Show();
        }
        //private void VratiCarinskeStavke()
        //{
        //    // 1. SQL upit
        //    var select = "SELECT RNCarinskoStavke.ID," +
        //                 "   RNCarinskoStavke.NHM AS [NHM naimenovanja], " +
        //                 "   RNCarinskoStavke.Naimenovanje AS [Naimenovanje], " +
        //                 "   RNCarinskoStavke.RB AS [RB Naimenovanja], " +
        //                 "   RNCarinskoStavke.JM AS [JM], " +
        //                 "   RNCarinskoStavke.Kolicina AS [Količina po JM], " +
        //                 "   RNCarinskoStavke.Koleta AS [Broj Koleta], " +
        //                 "   RNCarinskoStavke.Bruto AS [Bruto], " +
        //                 "   RNCarinskoStavke.Neto AS [Neto], " +
        //                 "   RNCarinskoStavke.Paleta AS [Broj Paleta], " +
        //                 "   RNCarinskoStavke.VrstaPaleta AS [VrstaPaletaID], " +
        //                 "   TipPalete.Naziv AS [Vrsta palete], " +
        //                 "   RNCarinskoStavke.Vrednost AS [Vrednost], " +
        //                 "   RNCarinskoStavke.Valuta AS [Valuta], " +
        //                 "   RNCarinskoStavke.PDV AS [PDV], " +
        //                 "   RNCarinskoStavke.Carina AS [Carinska stopa], " +
        //                 "   RNCarinskoStavke.VrednostGarancije AS [Vrednost garancije], " +
        //                 "   RNCarinskoStavke.Napomena AS [Napomena] " +
        //                 "FROM RNCarinskoStavke " +
        //                 "INNER JOIN MagacinskiPredmet ON RNCarinskoStavke.IDNadredjena = MagacinskiPredmet.ID " +
        //                 "LEFT JOIN TipPalete ON RNCarinskoStavke.VrstaPaleta = TipPalete.ID  " +
        //                 "WHERE MagacinskiPredmet.RN = " + Convert.ToInt32(txtID.Text);

        //    var s_connection = Sifarnici.frmLogovanje.connectionString;

        //    using (SqlConnection myConnection = new SqlConnection(s_connection))
        //    {
        //        var dataAdapter = new SqlDataAdapter(select, myConnection);

        //        DataSet ds = new DataSet();
        //        dataAdapter.Fill(ds);

        //        DataTable dt = ds.Tables[0];

        //        // 2. Punjenje grida i podešavanje izgleda
        //        gridGroupingControl1.DataSource = dt;

        //        this.gridGroupingControl1.ShowGroupDropArea = false;
        //        this.gridGroupingControl1.TopLevelGroupOptions.ShowCaption = false;
        //        this.gridGroupingControl1.TableOptions.ListBoxSelectionMode = SelectionMode.One;
        //        this.gridGroupingControl1.TableOptions.ShowRowHeader = false;

        //        this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("ID");
        //        this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("VrstaPaletaID");

        //        // 3. Podešavanje filtera
        //        foreach (GridColumnDescriptor column in gridGroupingControl1.TableDescriptor.Columns)
        //        {
        //            column.AllowFilter = true;
        //        }

        //        GridDynamicFilter dynamicFilter = new GridDynamicFilter();
        //        dynamicFilter.WireGrid(gridGroupingControl1);

        //        GridExcelFilter gridExcelFilter = new GridExcelFilter();
        //        gridExcelFilter.WireGrid(gridGroupingControl1);

        //        // 4. Formiranje TOTAL reda (Stacked Headers)
        //        var td = this.gridGroupingControl1.Table.TableDescriptor;
        //        td.StackedHeaderRows.Clear();

        //        // Kolone za koje vršimo sabiranje
        //        string[] koloneZaSabiranje =
        //        {
        //            "Količina po JM",
        //            "Broj Koleta",
        //            "Bruto",
        //            "Neto",
        //            "Broj Paleta",
        //            "Vrednost"
        //        };

        //        // Provera da li su JM svuda iste
        //        string prvaJM = dt.Rows.Count > 0 ? dt.Rows[0]["JM"]?.ToString() : "";
        //        bool jmIste = true;
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            if (row["JM"]?.ToString() != prvaJM) { jmIste = false; break; }
        //        }

        //        // Provera da li su Valute svuda iste
        //        string prvaValuta = dt.Rows.Count > 0 ? dt.Rows[0]["Valuta"]?.ToString() : "";
        //        bool valuteIste = true;
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            if (row["Valuta"]?.ToString() != prvaValuta) { valuteIste = false; break; }
        //        }

        //        decimal sumaBruto = 0;
        //        decimal sumaNeto = 0;
        //        decimal sumaVrednost = 0;
        //        decimal sumaKoleta = 0;

        //        // Kreiramo zaglavlja za svaku kolonu
        //        List<GridStackedHeaderDescriptor> totalHeaders = new List<GridStackedHeaderDescriptor>();

        //        foreach (DataColumn kolona in dt.Columns)
        //        {
        //            string nazivKolone = kolona.ColumnName;
        //            string tekst = "";

        //            // Oznaka "TOTAL" ispod kolone Naimenovanje
        //            if (nazivKolone == "Naimenovanje")
        //            {
        //                tekst = "TOTAL";
        //            }
        //            // Ispis JM samo ako su sve iste
        //            else if (nazivKolone == "JM")
        //            {
        //                tekst = jmIste ? prvaJM : "";
        //            }
        //            // Ispis Valute samo ako su sve iste
        //            else if (nazivKolone == "Valuta")
        //            {
        //                tekst = valuteIste ? prvaValuta : "";
        //            }
        //            // Zbir za numeričke kolone
        //            else if (koloneZaSabiranje.Contains(nazivKolone))
        //            {
        //                decimal suma = 0;

        //                foreach (DataRow red in dt.Rows)
        //                {
        //                    if (red[nazivKolone] != DBNull.Value)
        //                    {
        //                        suma += Convert.ToDecimal(red[nazivKolone]);
        //                    }
        //                }

        //                // Hvatanje vrednosti za kontrole na osnovu naziva kolone
        //                if (nazivKolone == "Bruto") sumaBruto = suma;
        //                else if (nazivKolone == "Neto") sumaNeto = suma;
        //                else if (nazivKolone == "Vrednost") sumaVrednost = suma;
        //                else if (nazivKolone == "Broj Koleta") sumaKoleta = suma;

        //                tekst = suma.ToString("N2");
        //            }

        //            // Formiranje deskriptora za pojedinačnu kolonu
        //            GridStackedHeaderDescriptor header = new GridStackedHeaderDescriptor(
        //                "Total_" + nazivKolone,
        //                tekst);

        //            header.VisibleColumns.Add(
        //                new GridStackedHeaderVisibleColumnDescriptor(nazivKolone));

        //            totalHeaders.Add(header);
        //        }

        //        // Kreiranje celog reda sa sumama i dodavanje u tabelu
        //        GridStackedHeaderRowDescriptor totalRow = new GridStackedHeaderRowDescriptor(
        //            "TotalRow",
        //            totalHeaders.ToArray());

        //        td.StackedHeaderRows.Add(totalRow);

        //        // Uključivanje prikaza višeslojnih zaglavlja
        //        gridGroupingControl1.TopLevelGroupOptions.ShowStackedHeaders = true;

        //        txtBruto.Value = sumaBruto;
        //        txtNeto.Value = sumaNeto;
        //        txtVrednost.Value = sumaVrednost;
        //        txtKoleta.Value = sumaKoleta;

        //        if (valuteIste && !string.IsNullOrEmpty(prvaValuta))
        //        {
        //            cboValuta.Text = prvaValuta; // Ili cboValuta.SelectedValue = ... zavisno od bindovanja
        //        }
        //        else
        //        {
        //            cboValuta.SelectedIndex = -1; // Ako nisu iste, poništavamo izbor
        //        }
        //    }
        //}
        private void VratiCarinskeStavke()
        {
            // 1. SQL upit
            var select = "SELECT RNCarinskoStavke.ID," +
                         "   RNCarinskoStavke.NHM AS [NHM naimenovanja], " +
                         "   RNCarinskoStavke.Naimenovanje AS [Naimenovanje], " +
                         "   RNCarinskoStavke.RB AS [RB Naimenovanja], " +
                         "   RNCarinskoStavke.JM AS [JM], " +
                         "   RNCarinskoStavke.Kolicina AS [Količina po JM], " +
                         "   RNCarinskoStavke.Koleta AS [Broj Koleta], " +
                         "   RNCarinskoStavke.Bruto AS [Bruto], " +
                         "   RNCarinskoStavke.Neto AS [Neto], " +
                         "   RNCarinskoStavke.Paleta AS [Broj Paleta], " +
                         "   RNCarinskoStavke.VrstaPaleta AS [VrstaPaletaID], " +
                         "   TipPalete.Naziv AS [Vrsta palete], " +
                         "   RNCarinskoStavke.Vrednost AS [Vrednost], " +
                         "   RNCarinskoStavke.Valuta AS [Valuta], " +
                         "   RNCarinskoStavke.PDV AS [PDV], " +
                         "   RNCarinskoStavke.Carina AS [Carinska stopa], " +
                         "   RNCarinskoStavke.VrednostGarancije AS [Vrednost garancije], " +
                         "   RNCarinskoStavke.Napomena AS [Napomena] " +
                         "FROM RNCarinskoStavke " +
                         "INNER JOIN MagacinskiPredmet ON RNCarinskoStavke.IDNadredjena = MagacinskiPredmet.ID " +
                         "LEFT JOIN TipPalete ON RNCarinskoStavke.VrstaPaleta = TipPalete.ID  " +
                         "WHERE MagacinskiPredmet.RN = " + Convert.ToInt32(txtID.Text);

            var s_connection = Sifarnici.frmLogovanje.connectionString;

            using (SqlConnection myConnection = new SqlConnection(s_connection))
            {
                var dataAdapter = new SqlDataAdapter(select, myConnection);

                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);

                DataTable dt = ds.Tables[0];

                // 2. Punjenje grida i podešavanje izgleda
                gridGroupingControl1.DataSource = dt;

                this.gridGroupingControl1.ShowGroupDropArea = false;
                this.gridGroupingControl1.TopLevelGroupOptions.ShowCaption = false;
                this.gridGroupingControl1.TableOptions.ListBoxSelectionMode = SelectionMode.One;
                this.gridGroupingControl1.TableOptions.ShowRowHeader = false;

                this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("ID");
                this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("VrstaPaletaID");

                // 3. Podešavanje filtera
                foreach (GridColumnDescriptor column in gridGroupingControl1.TableDescriptor.Columns)
                {
                    column.AllowFilter = true;
                }

                GridDynamicFilter dynamicFilter = new GridDynamicFilter();
                dynamicFilter.WireGrid(gridGroupingControl1);

                GridExcelFilter gridExcelFilter = new GridExcelFilter();
                gridExcelFilter.WireGrid(gridGroupingControl1);

                // 4. Formiranje TOTAL reda (Stacked Headers)
                var td = this.gridGroupingControl1.Table.TableDescriptor;
                td.StackedHeaderRows.Clear();

                // Kolone za koje vršimo sabiranje
    //            string[] koloneZaSabiranje =
    //            {
    //    "Količina po JM",
    //    "Broj Koleta",
    //    "Bruto",
    //    "Neto",
    //    "Broj Paleta",
    //    "Vrednost"
    //};

                string[] koloneZaSabiranje =
                {
                    "Količina po JM",
                    "Broj Koleta",
                    "Bruto",
                    "Neto",
                    "Broj Paleta",
                    "Vrednost",
                    "Vrednost garancije" // Dodato ako postoji u bazi
                };

                // Provera da li su JM svuda iste
                string prvaJM = dt.Rows.Count > 0 ? dt.Rows[0]["JM"]?.ToString() : "";
                bool jmIste = true;

                foreach (DataRow row in dt.Rows)
                {
                    if (row["JM"]?.ToString() != prvaJM)
                    {
                        jmIste = false;
                        break;
                    }
                }

                // Provera da li su Valute svuda iste
                string prvaValuta = dt.Rows.Count > 0 ? dt.Rows[0]["Valuta"]?.ToString() : "";
                bool valuteIste = true;

                foreach (DataRow row in dt.Rows)
                {
                    if (row["Valuta"]?.ToString() != prvaValuta)
                    {
                        valuteIste = false;
                        break;
                    }
                }

                decimal sumaBruto = 0;
                decimal sumaNeto = 0;
                decimal sumaVrednost = 0;
                decimal sumaKoleta = 0;
                decimal sumaPaleta = 0;
                decimal sumaGarancija = 0;

                // 5. Kreiranje zaglavlja za svaku kolonu
                List<GridStackedHeaderDescriptor> totalHeaders =
                    new List<GridStackedHeaderDescriptor>();

                foreach (DataColumn kolona in dt.Columns)
                {
                    string nazivKolone = kolona.ColumnName;
                    string tekst = "";

                    // Oznaka TOTAL ispod kolone Naimenovanje
                    if (nazivKolone == "Naimenovanje")
                    {
                        tekst = "TOTAL";
                    }
                    // Ispis JM samo ako su sve iste
                    else if (nazivKolone == "JM")
                    {
                        tekst = jmIste ? prvaJM : "";
                    }
                    // Ispis Valute samo ako su sve iste
                    else if (nazivKolone == "Valuta")
                    {
                        tekst = valuteIste ? prvaValuta : "";
                    }
                    // Zbir za numeričke kolone
                    else if (koloneZaSabiranje.Contains(nazivKolone))
                    {
                        decimal suma = 0;

                        foreach (DataRow red in dt.Rows)
                        {
                            if (red[nazivKolone] != DBNull.Value)
                            {
                                suma += Convert.ToDecimal(red[nazivKolone]);
                            }
                        }

                        // Hvatanje vrednosti za kontrole
                        if (nazivKolone == "Bruto")
                            sumaBruto = suma;
                        else if (nazivKolone == "Neto")
                            sumaNeto = suma;
                        else if (nazivKolone == "Vrednost")
                            sumaVrednost = suma;
                        else if (nazivKolone == "Broj Koleta")
                            sumaKoleta = suma;
                        else if (nazivKolone == "Broj Paleta") 
                            sumaPaleta = suma;
                        else if (nazivKolone == "Vrednost garancije") 
                            sumaGarancija = suma;

                        tekst = suma.ToString("N2");
                    }

                    // Formiranje deskriptora za TOTAL ćeliju
                    GridStackedHeaderDescriptor header =
                        new GridStackedHeaderDescriptor(
                            "Total_" + nazivKolone,
                            tekst);

                    header.VisibleColumns.Add(
                        new GridStackedHeaderVisibleColumnDescriptor(nazivKolone));

                    totalHeaders.Add(header);
                }

                // 6. Kreiranje TOTAL reda i dodavanje u tabelu
                GridStackedHeaderRowDescriptor totalRow =
                    new GridStackedHeaderRowDescriptor(
                        "TotalRow",
                        totalHeaders.ToArray());

                td.StackedHeaderRows.Add(totalRow);

               
                var columnHeader =
                    gridGroupingControl1.Appearance.ColumnHeaderCell;

                columnHeader.BackColor = Color.FromArgb(240, 240, 248);
                columnHeader.TextColor = SystemColors.ControlText;
                columnHeader.Font.FontStyle = FontStyle.Regular;

                // Tanke ivice zaglavlja
                columnHeader.Borders.Top =
                    new GridBorder(GridBorderStyle.Solid,
                        Color.FromArgb(190, 190, 198),
                        GridBorderWeight.Thin);

                columnHeader.Borders.Bottom =
                    new GridBorder(GridBorderStyle.Solid,
                        Color.FromArgb(190, 190, 198),
                        GridBorderWeight.Thin);

                columnHeader.Borders.Right =
                    new GridBorder(GridBorderStyle.Solid,
                        Color.FromArgb(210, 210, 216),
                        GridBorderWeight.Thin);

                columnHeader.Themed = false;


                // 8. Stilizovanje TOTAL reda
                var totalAppearance =
                    gridGroupingControl1.Appearance.StackedHeaderCell;

                totalAppearance.BackColor = Color.FromArgb(240, 240, 248);
                totalAppearance.TextColor = SystemColors.ControlText;
                totalAppearance.Font.FontStyle = FontStyle.Regular;

                // Tanke ivice TOTAL reda
                totalAppearance.Borders.Top =
                    new GridBorder(GridBorderStyle.Solid,
                        Color.FromArgb(190, 190, 198),
                        GridBorderWeight.Thin);

                totalAppearance.Borders.Bottom =
                    new GridBorder(GridBorderStyle.Solid,
                        Color.FromArgb(190, 190, 198),
                        GridBorderWeight.Thin);

                totalAppearance.Borders.Right =
                    new GridBorder(GridBorderStyle.Solid,
                        Color.FromArgb(210, 210, 216),
                        GridBorderWeight.Thin);

                totalAppearance.Themed = false;


                // 9. Uključivanje prikaza TOTAL reda
                gridGroupingControl1.TopLevelGroupOptions.ShowStackedHeaders = true;
                // 10. Popunjavanje zbirnih vrednosti u kontrolama
                txtBruto.Value = sumaBruto;
                txtNeto.Value = sumaNeto;
                txtVrednost.Value = sumaVrednost;
                txtKoleta.Value = sumaKoleta;

                if (valuteIste && !string.IsNullOrEmpty(prvaValuta))
                {
                    cboValuta.Text = prvaValuta;
                }
                else
                {
                    cboValuta.SelectedIndex = -1;
                }

                if (jmIste && !string.IsNullOrEmpty(prvaJM))
                {
                    cboJM.Text = prvaJM;
                }
                else
                {
                   cboJM.SelectedIndex = -1;
                }
            }

        }
        private void GridGroupingControl1_QueryCustomSummary(object sender, GridQueryCustomSummaryEventArgs e)
        {
            // Provera da li se računa JM ili Valuta preko SummaryDescriptor-a
            string summaryName = e.SummaryDescriptor != null ? e.SummaryDescriptor.Name : "";

            if (summaryName == "JM" || summaryName == "Valuta")
            {
                string prvaVrednost = null;
                bool sveIste = true;

                // Prolazimo kroz redove tabele da vidimo da li su sve vrednosti iste
                foreach (Syncfusion.Grouping.Record rec in gridGroupingControl1.Table.Records)
                {
                    string trenutna = rec.GetValue(summaryName)?.ToString();
                    if (prvaVrednost == null)
                    {
                        prvaVrednost = trenutna;
                    }
                    else if (prvaVrednost != trenutna)
                    {
                        sveIste = false;
                        break;
                    }
                }

                // Kako u ovoj verziji nemamo direktan Result, koristimo Handled = true 
                // i upisujemo vrednost ukoliko nam verzija dozvoljava, 
                // ili jednostavno obeležavamo da je obrađeno.
                e.Handled = true;
            }
        }

        private void gridGroupingControl1_TableControlCurrentCellControlDoubleClick(object sender, GridTableControlControlEventArgs e)
        {
            var selectedRecord = this.gridGroupingControl1.Table.CurrentRecord;

            if (selectedRecord != null)
            {

                // --- 1. POMOĆNA FUNKCIJA ZA NUMERIC UP DOWN ---
                Action<NumericUpDown, string> setNumericValue = (numCtrl, fieldName) =>
                {
                    object val = selectedRecord.GetValue(fieldName);
                    if (val != null && decimal.TryParse(val.ToString(), out decimal decVal))
                    {
                        if (decVal >= numCtrl.Minimum && decVal <= numCtrl.Maximum)
                        {
                            numCtrl.Value = decVal;
                        }
                    }
                };



                // 2. Popunjavamo NumericUpDown polja preko naše bezbedne funkcije
                setNumericValue(txtNHMNaimenovanje, "NHM naimenovanja"); // prilagodi tačan naziv kolone ako nije "Naimenovanje"
                setNumericValue(txtKolicinaJM, "Količina po JM");
                setNumericValue(txtBrutoE, "Bruto");
                setNumericValue(txtNetoE, "Neto");
                setNumericValue(txtVrednostE, "Vrednost");
                setNumericValue(txtPaleta, "Broj Paleta");
                setNumericValue(txtCarina, "Carinska stopa");
                setNumericValue(txtPDV, "PDV");


                object valPaleta = selectedRecord.GetValue("VrstaPaletaID");
                if (valPaleta != null && int.TryParse(valPaleta.ToString(), out int idPalete))
                {
                    cboVrstaPaleta.SelectedValue = idPalete;
                }
                else
                {
                    cboVrstaPaleta.SelectedIndex = -1;
                }
                txtIDStavke.Text = selectedRecord.GetValue("ID")?.ToString();
                txtRBNaimenovanje.Text = selectedRecord.GetValue("RB Naimenovanja")?.ToString();
                txtNaimenovanje.Text = selectedRecord.GetValue("Naimenovanje")?.ToString();
                cboJM.Text = selectedRecord.GetValue("JM")?.ToString();
                txtKoletaE.Text = selectedRecord.GetValue("Broj Koleta")?.ToString();

                cboValutaStavka.Text = selectedRecord.GetValue("Valuta")?.ToString();
                txtNapomenaE.Text = selectedRecord.GetValue("Napomena")?.ToString();
            }


        }

        private void btnPaleta_Click(object sender, EventArgs e)
        {

            frmTipPalete frm = new frmTipPalete();
            frm.Show();
        }

        private void btnDodajStavku_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMagacinskiPredmetID.Text.IsNullOrWhiteSpace())
                {
                    MessageBox.Show("Neophodno je prvo snimiti podatke magacinskog predmeta");
                    return;
                }    

                int magacinskiID = int.TryParse(txtMagacinskiPredmetID.Text, out int magacinskiPredmet) ? magacinskiPredmet : 0;
                InsertSkladista ins = new InsertSkladista();

                int? nhm = int.TryParse(txtNHMNaimenovanje.Text, out int nhmValue) ? nhmValue : (int?)null;

                string naimenovanje = txtNaimenovanje.Text;

                int? rb = int.TryParse(txtRBNaimenovanje.Text, out int rbValue) ? rbValue : (int?)null;

                string jm = cboJM.Text;

                decimal? kolicina = decimal.TryParse(txtKolicinaJM.Text, out decimal kolicinaValue) ? kolicinaValue : (decimal?)null;

                decimal? koleta = decimal.TryParse(txtKoletaE.Text, out decimal koletaValue) ? koletaValue : (decimal?)null;

                decimal? bruto = decimal.TryParse(txtBrutoE.Text, out decimal brutoValue) ? brutoValue : (decimal?)null;

                decimal? neto = decimal.TryParse(txtNetoE.Text, out decimal netoValue) ? netoValue : (decimal?)null;

                int? paleta = int.TryParse(txtPaleta.Text, out int paletaValue) ? paletaValue : (int?)null;

                int? vrstaPaleta = cboVrstaPaleta.SelectedValue != null && cboVrstaPaleta.SelectedValue != DBNull.Value
                    ? Convert.ToInt32(cboVrstaPaleta.SelectedValue) : (int?)null;

                decimal? vrednost = decimal.TryParse(txtVrednostE.Text, out decimal vrednostValue) ? vrednostValue : (decimal?)null;

                string valuta = cboValutaStavka.Text;

                decimal? pdv = decimal.TryParse(txtPDV.Text, out decimal pdvValue) ? pdvValue : (decimal?)null;

                decimal? carina = decimal.TryParse(txtCarina.Text, out decimal carinaValue) ? carinaValue : (decimal?)null;



                string napomena = txtNapomenaE.Text;

                ins.InsertCarinskoStavke(magacinskiID, nhm, naimenovanje, rb, jm, kolicina, koleta, bruto, neto, paleta, vrstaPaleta, vrednost, valuta, pdv, carina, napomena);

                VratiCarinskeStavke();
                ResetujPoljaArtikli();



            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri unosu/izmeni stavke: " + ex.Message);
            }
        }

        private void ResetujPoljaArtikli()
        {
            //txtMagacinskiPredmetID.Clear();
            txtRBNaimenovanje.Clear();
            txtNaimenovanje.Clear();
          
            txtNapomenaE.Clear();

            // Ako hoćete fiksno na nulu umesto minimuma, možete i ovako:
            txtNHMNaimenovanje.Value = 0;
            txtKolicinaJM.Value = 0;
            txtKoletaE.Value = 0;
            txtBrutoE.Value = 0;
            txtNetoE.Value = 0;
            txtPaleta.Value = 0;
            txtVrednostE.Value = 0;
            txtPDV.Value = 0;
            txtCarina.Value = 0;

            // Padajuće liste (ComboBox)
            cboJM.SelectedIndex = -1;
            cboJM.Text = string.Empty;

            cboVrstaPaleta.SelectedIndex = -1;
            cboVrstaPaleta.Text = string.Empty;

            cboValutaStavka.SelectedIndex = -1;
            cboValutaStavka.Text = string.Empty;
        }
        private void btnIzmeniStavku_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtMagacinskiPredmetID.Text.IsNullOrWhiteSpace())
                {
                    MessageBox.Show("Neophodno je prvo snimiti podatke magacinskog predmeta");
                    return;
                }

                int magacinskiID = int.TryParse(txtMagacinskiPredmetID.Text, out int magacinskiPredmet) ? magacinskiPredmet : 0;
                InsertSkladista ins = new InsertSkladista();


                int id = int.TryParse(txtIDStavke.Text, out int idValue) ? idValue : 0;
                int? nhm = int.TryParse(txtNHMNaimenovanje.Text, out int nhmValue) ? nhmValue : (int?)null;

                string naimenovanje = txtNaimenovanje.Text;

                int? rb = int.TryParse(txtRBNaimenovanje.Text, out int rbValue) ? rbValue : (int?)null;

                string jm = cboJM.Text;

                decimal? kolicina = decimal.TryParse(txtKolicinaJM.Text, out decimal kolicinaValue) ? kolicinaValue : (decimal?)null;

                decimal? koleta = decimal.TryParse(txtKoletaE.Text, out decimal koletaValue) ? koletaValue : (decimal?)null;

                decimal? bruto = decimal.TryParse(txtBrutoE.Text, out decimal brutoValue) ? brutoValue : (decimal?)null;

                decimal? neto = decimal.TryParse(txtNetoE.Text, out decimal netoValue) ? netoValue : (decimal?)null;

                int? paleta = int.TryParse(txtPaleta.Text, out int paletaValue) ? paletaValue : (int?)null;

                int? vrstaPaleta = cboVrstaPaleta.SelectedValue != null && cboVrstaPaleta.SelectedValue != DBNull.Value
                    ? Convert.ToInt32(cboVrstaPaleta.SelectedValue) : (int?)null;

                decimal? vrednost = decimal.TryParse(txtVrednostE.Text, out decimal vrednostValue) ? vrednostValue : (decimal?)null;

                string valuta = cboValutaStavka.Text;

                decimal? pdv = decimal.TryParse(txtPDV.Text, out decimal pdvValue) ? pdvValue : (decimal?)null;

                decimal? carina = decimal.TryParse(txtCarina.Text, out decimal carinaValue) ? carinaValue : (decimal?)null;

                decimal? vrednostGarancije = (decimal?)null;

                string napomena = txtNapomenaE.Text;

                ins.UpdateCarinskoStavke(id, magacinskiID, nhm, naimenovanje, rb, jm, kolicina, koleta, bruto, neto, paleta, vrstaPaleta, vrednost, valuta, pdv, carina, napomena);

                VratiCarinskeStavke();
                ResetujPoljaArtikli();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri unosu/izmeni stavke: " + ex.Message);
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            int id = int.TryParse(txtIDStavke.Text, out int idValue) ? idValue : 0;
            if (id == 0)
            {
                MessageBox.Show("Nije izabrana stavka za brisanje.");
                return;
            }
            InsertSkladista ins = new InsertSkladista();

            ins.DeleteCarinskoStavke(id);

            VratiCarinskeStavke();
            ResetujPoljaArtikli();
        }

        private void txtSmestajniDokument_TextChanged(object sender, EventArgs e)
        {
            if (!txtSmestajniDokument.Text.IsNullOrWhiteSpace())
                txtBrojJCI.Text = txtSmestajniDokument.Text.Trim();
        }

       
              private void cbo_TextUpdate(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb == null) return;

            if (cb.DroppedDown)
            {
                // 1. Zapamti tačnu poziciju kursora i trenutno ukucan tekst
                int caretPosition = cb.SelectionStart;
                string typedText = cb.Text;

                // Ako je ComboBox automatski selektovao ceo/preostali tekst (SuggestAppend ponašanje)
                // uzimamo samo deo teksta do kursora koji je korisnik zaista ukucao
                if (cb.SelectionLength > 0 && caretPosition > 0 && caretPosition <= typedText.Length)
                {
                    typedText = typedText.Substring(0, caretPosition);
                }

                // 2. Zatvori veliki padajući meni
                cb.DroppedDown = false;

                // 3. Vrati samo ukucani tekst bez auto-completed dodatka
                cb.Text = typedText;

                // 4. Resetuj selekciju - postavi kursor na kraj bez plave selekcije (highlights)
                cb.SelectionStart = typedText.Length;
                cb.SelectionLength = 0;
            }
        }

      
    }
    

   

}
