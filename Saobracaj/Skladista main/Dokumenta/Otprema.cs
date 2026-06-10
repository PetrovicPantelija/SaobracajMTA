using GMap.NET.Internals;
using Microsoft.ReportingServices.Diagnostics.Internal;
using Saobracaj.Skladista;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main.Dokumenta
{
    public partial class Otprema : Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

        string Tip = "Otprema";
        int IDInterni;
        string Ulaz;
        string Vrsta;
        string Korisnik = Saobracaj.Sifarnici.frmLogovanje.user;
        public Otprema(int iDInterni, string ulaz, string vrsta, string korinsik)
        {
            InitializeComponent();
            IDInterni = iDInterni;
            Ulaz = ulaz;
            Vrsta = vrsta;

            btnStorno.Text = "";
            btnStorno.Enabled = false;
            btnIspravka.Text = "";
            btnIspravka.Enabled = false;
            btnSaglasnost.Text = "";
            btnSaglasnost.Enabled = false;

            panel5.Visible = false;
            panel2.Visible = false;
            if (Vrsta == "Carinsko")
            {
                textBox1.Text = "1008";
            }

            btnMagacinskiBroj.Enabled = false;
            btnMagacinskiBroj.Visible = false;
        }
        public Otprema(int iDInterni, string ulaz, string vrsta, string korisnik, int rn)
        {
            InitializeComponent();
            IDInterni = iDInterni;
            Ulaz = ulaz;
            Vrsta = vrsta;

            btnStorno.Text = "";
            btnStorno.Enabled = false;
            btnIspravka.Text = "";
            btnIspravka.Enabled = false;
            btnSaglasnost.Text = "";
            btnSaglasnost.Enabled = false;


            panel5.Visible = false;
            panel2.Visible = false;
            if (Vrsta == "Carinsko")
            {
                textBox1.Text = "1008";
            }

            btnMagacinskiBroj.Enabled = false;
            btnMagacinskiBroj.Visible = false;

            txtID.Text = rn.ToString();
        }
        int ID;
        public Otprema(int id, string ulaz, string vrsta)
        {
            InitializeComponent();
            ID = id;
            Ulaz = ulaz;
            Vrsta = vrsta;


            panel5.Visible = false;
            panel2.Visible = false;
            if (Vrsta == "Carinsko")
            {
                textBox1.Text = "1008";
            }

            btnMagacinskiBroj.Enabled = false;
            btnMagacinskiBroj.Visible = false;

            txtID.Text = ID.ToString();
        }
        private void Otprema_Load(object sender, EventArgs e)
        {
            FillCombo();
            InitTable();
            FillMagacinskiBroj();
            FillDodatneUsluge(txtID.Text);
            if (txtID.Text != "")
            {
                VratiPodatke(Convert.ToInt32(txtID.Text));
                VratiStavkeOtpreme(Convert.ToInt32(txtID.Text));
            }

            PoveziEventeStavki();
            OcistiPanelStavke();
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


                            PrijemnicaID = Convert.ToInt32(dr2["prijemID"].ToString());
                            PrijemRN = Convert.ToInt32(dr2["RN"].ToString());
                            textBox2.Text = PrijemnicaID.ToString();

                            int dodatneUslugeID = Convert.ToInt32(dr2["DodatneUslugeID"].ToString());
                            FillDodatneUsluge(dodatneUslugeID.ToString());
                        }
                    }
                }
            }
        }
        private void VratiStavkeOtpreme(int rn)
        {
            list.Clear();

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                string query = @"
;WITH Primljeno AS
(
    SELECT 
        s.ID AS IDSkladisneStavke,
        s.IDNadredjena AS Prijemnica,
        s.RB AS PrijemRB,
        rn.MagacinskiBroj,
        rn.VlasnikRobe,
        s.Naimenovanje,
        s.NHM,
        s.Naziv,
        s.JM,
        ISNULL(SUM(p.PrPrimKol), 0) - ISNULL(SUM(p.PrIzdKol), 0) AS KoletaPoPrometu,
        s.Koleta AS PrimljenoKoleta,
        s.Bruto AS PrimljenoBruto,
        s.Neto AS PrimljenoNeto,
        s.Vrednost AS PrimljenaVrednost,
        s.Valuta,
        ISNULL(skl.Naziv, s.Pozicija) AS Pozicija,
        s.Paleta,
        s.VrstaPaleta,
        tp.Dimenzije AS DimenzijaPaleta,
        s.NHM AS SifraArtikla,
        s.PDV,
        s.Carina,
        ISNULL(MAX(p.SkladisteU), '') AS TabelaPozicija
    FROM RNCarinskoPrijemnicaStavke s
    INNER JOIN RNCarinskoSkladistePrijemnica pr 
        ON s.IDNadredjena = pr.ID
    INNER JOIN RadniNalogSkladista rn 
        ON pr.RN = rn.ID
    LEFT JOIN Promet p 
        ON s.ID = p.IDSaDokumenta
       AND p.MagacinskiBroj = rn.MagacinskiBroj
    INNER JOIN TipPalete tp 
        ON s.VrstaPaleta = tp.ID
    LEFT JOIN Skladista skl  
        ON s.Pozicija = skl.Naziv
    WHERE 
        pr.Status = 'ZA'
        AND rn.MagacinskiBroj = (SELECT MagacinskiBroj FROM RadniNalogSkladista WHERE ID = @RN)
    GROUP BY 
        s.ID,
        s.IDNadredjena,
        s.RB,
        rn.MagacinskiBroj,
        rn.VlasnikRobe,
        s.Naimenovanje,
        s.NHM,
        s.Naziv,
        s.JM,
        s.Koleta,
        s.Bruto,
        s.Neto,
        s.Vrednost,
        s.Valuta,
        s.Pozicija,
        skl.Naziv,
        s.Paleta,
        s.VrstaPaleta,
        tp.Dimenzije,
        s.PDV,
        s.Carina
),
Zavrseno AS
(
    SELECT
        ot.Prijemnica,
        st.NHM,
        st.Naziv,
        st.Naimenovanje,
        st.JM,
        st.Valuta,
        st.Pozicija,
        st.VrstaPaleta,
        SUM(ISNULL(st.Koleta, 0)) AS ZavrsenoKoleta,
        SUM(ISNULL(st.Bruto, 0)) AS ZavrsenoBruto,
        SUM(ISNULL(st.Neto, 0)) AS ZavrsenoNeto,
        SUM(ISNULL(st.Vrednost, 0)) AS ZavrsenaVrednost
    FROM RNCarinskoSkladisteOtpremnicaStavke st
    INNER JOIN RNCarinskoSkladisteOtpremnica ot
        ON st.IDNadredjena = ot.ID
    GROUP BY
        ot.Prijemnica,
        st.NHM,
        st.Naziv,
        st.Naimenovanje,
        st.JM,
        st.Valuta,
        st.Pozicija,
        st.VrstaPaleta
),
PomOstali AS
(
    SELECT
        Prijemnica,
        NHM,
        Naziv,
        Naimenovanje,
        JM,
        Valuta,
        Pozicija,
        VrstaPaleta,
        SUM(ISNULL(Koleta, 0)) AS PomKoleta,
        SUM(ISNULL(Bruto, 0)) AS PomBruto,
        SUM(ISNULL(Neto, 0)) AS PomNeto,
        SUM(ISNULL(Vrednost, 0)) AS PomVrednost
    FROM RNCarinskoSkladisteOtpremaStavkePom
    WHERE ISNULL(IDNadredjena, 0) <> @RN
    GROUP BY
        Prijemnica,
        NHM,
        Naziv,
        Naimenovanje,
        JM,
        Valuta,
        Pozicija,
        VrstaPaleta
),
Lager AS
(
    SELECT
        p.*,
        NerazduzenoKoleta =
            CASE WHEN p.KoletaPoPrometu - ISNULL(po.PomKoleta, 0) < 0 THEN 0
                 ELSE p.KoletaPoPrometu - ISNULL(po.PomKoleta, 0) END,
        NerazduzenaTezina =
            CASE WHEN p.PrimljenoBruto - ISNULL(z.ZavrsenoBruto, 0) - ISNULL(po.PomBruto, 0) < 0 THEN 0
                 ELSE p.PrimljenoBruto - ISNULL(z.ZavrsenoBruto, 0) - ISNULL(po.PomBruto, 0) END,
        NerazduzenoNeto =
            CASE WHEN ISNULL(p.PrimljenoNeto, 0) - ISNULL(z.ZavrsenoNeto, 0) - ISNULL(po.PomNeto, 0) < 0 THEN 0
                 ELSE ISNULL(p.PrimljenoNeto, 0) - ISNULL(z.ZavrsenoNeto, 0) - ISNULL(po.PomNeto, 0) END,
        OstatakValute =
            CASE WHEN p.PrimljenaVrednost - ISNULL(z.ZavrsenaVrednost, 0) - ISNULL(po.PomVrednost, 0) < 0 THEN 0
                 ELSE p.PrimljenaVrednost - ISNULL(z.ZavrsenaVrednost, 0) - ISNULL(po.PomVrednost, 0) END
    FROM Primljeno p
    LEFT JOIN Zavrseno z
        ON z.Prijemnica = p.Prijemnica
       AND ISNULL(z.NHM, 0) = ISNULL(p.NHM, 0)
       AND RTRIM(ISNULL(z.Naziv, '')) = RTRIM(ISNULL(p.Naziv, ''))
       AND RTRIM(ISNULL(z.Naimenovanje, '')) = RTRIM(ISNULL(p.Naimenovanje, ''))
       AND RTRIM(ISNULL(z.JM, '')) = RTRIM(ISNULL(p.JM, ''))
       AND RTRIM(ISNULL(z.Valuta, '')) = RTRIM(ISNULL(p.Valuta, ''))
       AND RTRIM(ISNULL(z.Pozicija, '')) = RTRIM(ISNULL(p.Pozicija, ''))
       AND ISNULL(z.VrstaPaleta, 0) = ISNULL(p.VrstaPaleta, 0)
    LEFT JOIN PomOstali po
        ON po.Prijemnica = p.Prijemnica
       AND ISNULL(po.NHM, 0) = ISNULL(p.NHM, 0)
       AND RTRIM(ISNULL(po.Naziv, '')) = RTRIM(ISNULL(p.Naziv, ''))
       AND RTRIM(ISNULL(po.Naimenovanje, '')) = RTRIM(ISNULL(p.Naimenovanje, ''))
       AND RTRIM(ISNULL(po.JM, '')) = RTRIM(ISNULL(p.JM, ''))
       AND RTRIM(ISNULL(po.Valuta, '')) = RTRIM(ISNULL(p.Valuta, ''))
       AND RTRIM(ISNULL(po.Pozicija, '')) = RTRIM(ISNULL(p.Pozicija, ''))
       AND ISNULL(po.VrstaPaleta, 0) = ISNULL(p.VrstaPaleta, 0)
)
SELECT
    ISNULL(l.IDSkladisneStavke, 0) AS IDStavka,
    ISNULL(l.Prijemnica, o.Prijemnica) AS Prijemnica,
    ISNULL(l.PrijemRB, 0) AS PrijemRB,
    o.Naimenovanje,
    o.NHM,
    o.Naziv,
    o.JM,
    CASE WHEN ISNULL(l.NerazduzenoKoleta, 0) - ISNULL(o.Koleta, 0) < 0 THEN 0 ELSE ISNULL(l.NerazduzenoKoleta, 0) - ISNULL(o.Koleta, 0) END AS NerazduzenoKoleta,
    CASE WHEN ISNULL(l.NerazduzenaTezina, 0) - ISNULL(o.Bruto, 0) < 0 THEN 0 ELSE ISNULL(l.NerazduzenaTezina, 0) - ISNULL(o.Bruto, 0) END AS NerazduzenaTezina,
    CASE WHEN ISNULL(l.NerazduzenoNeto, 0) - ISNULL(o.Neto, 0) < 0 THEN 0 ELSE ISNULL(l.NerazduzenoNeto, 0) - ISNULL(o.Neto, 0) END AS NerazduzenoNeto,
    CASE WHEN ISNULL(l.OstatakValute, 0) - ISNULL(o.Vrednost, 0) < 0 THEN 0 ELSE ISNULL(l.OstatakValute, 0) - ISNULL(o.Vrednost, 0) END AS OstatakValute,
    ISNULL(l.NerazduzenoKoleta, o.Koleta) AS UkupnoKoletaLager,
    ISNULL(l.NerazduzenaTezina, o.Bruto) AS UkupnoBrutoLager,
    ISNULL(l.NerazduzenoNeto, o.Neto) AS UkupnoNetoLager,
    ISNULL(l.OstatakValute, o.Vrednost) AS UkupnoVrednostLager,
    o.Valuta,
    '' AS DokumentRazduzenja,
    GETDATE() AS DatumRazduzenja,
    '' AS Destinacija,
    GETDATE() AS DatumOtpreme,
    o.Pozicija,
    o.Paleta,
    o.VrstaPaleta,
    ISNULL(l.DimenzijaPaleta, '') AS DimenzijaPaleta,
    ISNULL(l.SifraArtikla, o.NHM) AS SifraArtikla,
    o.PDV,
    o.Carina,
    ISNULL(l.TabelaPozicija, '') AS TabelaPozicija,
    o.RB,
    o.Koleta,
    o.Bruto,
    o.Neto,
    o.Vrednost,
    o.Napomena,
    o.Kreirao
FROM RNCarinskoSkladisteOtpremaStavkePom o
LEFT JOIN Lager l
    ON l.Prijemnica = o.Prijemnica
   AND ISNULL(l.NHM, 0) = ISNULL(o.NHM, 0)
   AND RTRIM(ISNULL(l.Naziv, '')) = RTRIM(ISNULL(o.Naziv, ''))
   AND RTRIM(ISNULL(l.Naimenovanje, '')) = RTRIM(ISNULL(o.Naimenovanje, ''))
   AND RTRIM(ISNULL(l.JM, '')) = RTRIM(ISNULL(o.JM, ''))
   AND RTRIM(ISNULL(l.Valuta, '')) = RTRIM(ISNULL(o.Valuta, ''))
   AND RTRIM(ISNULL(l.Pozicija, '')) = RTRIM(ISNULL(o.Pozicija, ''))
   AND ISNULL(l.VrstaPaleta, 0) = ISNULL(o.VrstaPaleta, 0)
WHERE o.IDNadredjena = @RN
ORDER BY o.RB;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RN", rn);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var stavka = new OtpremaStavke
                            {
                                IDSkladisneStavke = dr["IDStavka"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IDStavka"]),
                                RB = dr["RB"] == DBNull.Value ? 0 : Convert.ToInt32(dr["RB"]),
                                Prijemnica = dr["Prijemnica"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Prijemnica"]),
                                PrijemRB = dr["PrijemRB"] == DBNull.Value ? 0 : Convert.ToInt32(dr["PrijemRB"]),

                                Naimenovanje = dr["Naimenovanje"] == DBNull.Value ? "" : dr["Naimenovanje"].ToString(),
                                NHM = dr["NHM"] == DBNull.Value ? 0 : Convert.ToInt32(dr["NHM"]),
                                Naziv = dr["Naziv"] == DBNull.Value ? "" : dr["Naziv"].ToString(),
                                JM = dr["JM"] == DBNull.Value ? "" : dr["JM"].ToString(),

                                NerazudzenoKoleta = dr["NerazduzenoKoleta"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["NerazduzenoKoleta"]),
                                NerazduzenaTezina = dr["NerazduzenaTezina"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["NerazduzenaTezina"]),
                                OstatakValute = dr["OstatakValute"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["OstatakValute"]),

                                UkupnoKoletaLager = dr["UkupnoKoletaLager"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["UkupnoKoletaLager"]),
                                UkupnoBrutoLager = dr["UkupnoBrutoLager"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["UkupnoBrutoLager"]),
                                UkupnoNetoLager = dr["UkupnoNetoLager"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["UkupnoNetoLager"]),
                                UkupnoVrednostLager = dr["UkupnoVrednostLager"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["UkupnoVrednostLager"]),

                                Koleta = dr["Koleta"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Koleta"]),
                                Bruto = dr["Bruto"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Bruto"]),
                                Neto = dr["Neto"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Neto"]),
                                Vrednost = dr["Vrednost"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Vrednost"]),

                                Valuta = dr["Valuta"] == DBNull.Value ? "" : dr["Valuta"].ToString(),
                                DokumentRazduzenja = "",
                                DatumRazduzenja = DateTime.Now,
                                Destinacija = "",
                                DatumOtpreme = DateTime.Now,

                                Pozicija = dr["Pozicija"] == DBNull.Value ? "" : dr["Pozicija"].ToString(),
                                Paleta = dr["Paleta"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Paleta"]),
                                VrstaPalete = dr["VrstaPaleta"] == DBNull.Value ? 0 : Convert.ToInt32(dr["VrstaPaleta"]),
                                DimenzijePalete = dr["DimenzijaPaleta"] == DBNull.Value ? "" : dr["DimenzijaPaleta"].ToString(),
                                SifraArtikla = dr["SifraArtikla"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SifraArtikla"]),

                                PDV = dr["PDV"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["PDV"]),
                                Carina = dr["Carina"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Carina"]),
                                TabelaPozicija = dr["TabelaPozicija"] == DBNull.Value ? "" : dr["TabelaPozicija"].ToString()
                            };

                            list.Add(stavka);

                            if (stavka.Prijemnica != 0)
                            {
                                PrijemnicaID = stavka.Prijemnica;
                            }
                        }
                    }
                }
            }

            if (list.Count > 0)
                rb = list.Max(x => x.RB) + 1;
            else
                rb = 1;

            OsveziGrid();
            OcistiPanelStavke();
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
        private void FillMagacinskiBroj()
        {
            SqlConnection conn = new SqlConnection(connection);

            if (Vrsta == "Carinsko")
            {
                var partner5 = @"Select MagacinskiBroj as ID, (Cast(MagacinskiBrojCarinski.ID as nvarchar(5))+' - ' + MagacinskiBrojCarinski.Naziv) as Naziv
	From RadniNalogSkladista 
	inner join RNCarinskoSkladistePrijemnica on RadniNalogSkladista.ID=RNCarinskoSkladistePrijemnica.RN 
	inner join MagacinskiBrojCarinski on RadniNalogSkladista.MagacinskiBroj=MagacinskiBrojCarinski.ID
	Where RNCarinskoSkladistePrijemnica.Status='ZA' order by ID desc";
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
                dataGridView2.ReadOnly = true;
                dataGridView2.DataSource = ds.Tables[0];

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
        int MagacinskiBroj;
        int Aktivan;
        private void btnMagacinskiBroj_Click(object sender, EventArgs e)
        {
            FillMagacinskiBroj();
            panel5.Visible = true;
        }

        private void cboMagacinskiBroj_SelectionChangeCommitted(object sender, EventArgs e)
        {
            MagacinskiBroj = Convert.ToInt32(cboMagacinskiBroj.SelectedValue);
            if (MagacinskiBroj != 0)
            {
                Aktivan = 1;
            }

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(@"select Nalogodavac,VlasnikRobe,VrstaRobe,NacinPakovanja,OstalaSkladista,PIB,PosebniUslovi,DodatneUslugeID,Napomena,RadniNalogSkladista.ID as RN,
RNCarinskoSkladistePrijemnica.ID as prijemID
From RadniNalogSkladista
inner join RNCarinskoSkladistePrijemnica on RNCarinskoSkladistePrijemnica.RN=RadniNalogSkladista.ID
Where MagacinskiBroj=" + MagacinskiBroj, conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        cboNalogodavac.SelectedValue = Convert.ToInt32(dr["Nalogodavac"].ToString());
                        cboVlasnikRobe.SelectedValue = Convert.ToInt32(dr["VlasnikRobe"].ToString());
                        txtVrstaRobe.Text = dr["VrstaRobe"].ToString();
                        txtNacinPakovanja.Text = dr["NacinPakovanja"].ToString();
                        cboADR.SelectedValue = Convert.ToInt32(dr["OstalaSkladista"].ToString());
                        txtPIB.Text = dr["PIB"].ToString();
                        txtPosebniUslovi.Text = dr["PosebniUslovi"].ToString();
                        txtNapomena.Text = dr["Napomena"].ToString();


                        PrijemnicaID = Convert.ToInt32(dr["prijemID"].ToString());
                        PrijemRN = Convert.ToInt32(dr["RN"].ToString());
                        textBox2.Text = PrijemnicaID.ToString();

                        int dodatneUslugeID = Convert.ToInt32(dr["DodatneUslugeID"].ToString());
                        FillDodatneUsluge(dodatneUslugeID.ToString());
                    }
                }
            }

        }

        private void btnMbSave_Click(object sender, EventArgs e)
        {
            InsertSkladista ins = new InsertSkladista();

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
            //OTPREMA
            InsertSkladista ins = new InsertSkladista();
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

                    ins.UpdateRadniNalog(Convert.ToInt32(txtID.Text), "Kreiran", DateTime.Now, Korisnik, Vrsta, Tip, textBox1.Text.ToString().TrimEnd(), MagacinskiBroj, Convert.ToInt32(cboNalogodavac.SelectedValue),
                        Convert.ToInt32(cboCarinskiPostupak.SelectedValue), txtOpisPosla.Text.ToString().TrimEnd(), Convert.ToInt32(cboVlasnikRobe.SelectedValue), txtVrstaRobe.Text.ToString().TrimEnd(),
                        txtNacinPakovanja.Text.ToString().TrimEnd(), Convert.ToInt32(cboADR.SelectedValue), Convert.ToInt32(txtPIB.Text), Convert.ToInt32(cboTipTransportaOtprema.SelectedValue),
                        Convert.ToInt32(cboVrstaKamionaOtprema.SelectedValue), txtVoziloOtprema.Text.ToString().TrimEnd(), txtVozacOtprema.Text.ToString().TrimEnd(), txtLKOtprema.Text.ToString().TrimEnd(),
                        txtTelefonOtprema.Text.ToString().TrimEnd(), Convert.ToInt32(cboCarinarnicaOtprema.SelectedValue), Convert.ToInt32(cboSpediterOtprema.SelectedValue), txtKontakOsobaSpediterOtprema.Text.ToString().TrimEnd(),
                        Convert.ToInt32(cboMestoIstovaraOtprema.SelectedValue), txtAdresaOtprema.Text.ToString().TrimEnd(), txtKontaktOsobaOtprema.Text.ToString().TrimEnd(), Convert.ToDateTime(planiranoVremeOtprema.Value),
                        Convert.ToDateTime(novoVremeOtprema.Value), txtKontejnerOtprema.Text.ToString().TrimEnd(), null, null,
                        null, null, null, null, null,
                        null, null, null, null, null,
                        null, null, null, txtPosebniUslovi.Text.ToString().TrimEnd(), idUsluge,
                        txtNapomena.Text.ToString().TrimEnd(), Aktivan, Formiran);

                    /*
                    if (Ulaz != "")
                    {
                        ins.InsertRNInterni(3, Korisnik);
                    }
                    else
                    {
                        ins.UpdateRNInterni(IDInterni, Convert.ToInt32(txtID.Text));
                    }
                    */

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
                        Convert.ToDateTime(novoVremeOtprema.Value), txtKontejnerOtprema.Text.ToString().TrimEnd(), null, null,
                        null, null, null, null, null,
                        null, null, null, null, null,
                        null, null, null, txtPosebniUslovi.Text.ToString().TrimEnd(), IdUsluge,
                        txtNapomena.Text.ToString().TrimEnd(), Aktivan, Formiran);

                    /* if (Ulaz != "")
                     {
                         ins.InsertRNInterni(3, Korisnik);
                     }
                     else
                     {
                         ins.UpdateRNInterni(IDInterni, Convert.ToInt32(txtID.Text));
                     }*/

                    MessageBox.Show("RADNI NALOG KREIRAN");

                    using (SqlConnection conn = new SqlConnection(connection))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("select Max(ID) as ID from RadniNalogSkladista", conn))
                        {
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                txtID.Text = dr["ID"].ToString();
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
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("Delete From RNCarinskoSkladisteOtpremaStavkePom Where IDNadredjena=" + Convert.ToInt32(txtID.Text), conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                foreach (var i in list)
                {
                    ins.InsertOtpremaStavke(i.RB, i.NHM, i.Naziv, i.Naimenovanje, i.JM, i.Koleta, i.Bruto, i.Vrednost, i.Valuta, i.Pozicija, i.Paleta, i.VrstaPalete, i.PDV, i.Carina, i.Neto,
                        "", Korisnik, i.Prijemnica > 0 ? i.Prijemnica : PrijemnicaID);
                }
                MessageBox.Show("Sačuvane stavke otpreme!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR Insert otprema stavke:" + ex.ToString());
            }
        }

        private void btnFormiranRN_Click(object sender, EventArgs e)
        {
            InsertSkladista ins = new InsertSkladista();
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

                    ins.UpdateRadniNalog(Convert.ToInt32(txtID.Text), "Kreiran", DateTime.Now, Korisnik, Vrsta, Tip, textBox1.Text.ToString().TrimEnd(), MagacinskiBroj, Convert.ToInt32(cboNalogodavac.SelectedValue),
                        Convert.ToInt32(cboCarinskiPostupak.SelectedValue), txtOpisPosla.Text.ToString().TrimEnd(), Convert.ToInt32(cboVlasnikRobe.SelectedValue), txtVrstaRobe.Text.ToString().TrimEnd(),
                        txtNacinPakovanja.Text.ToString().TrimEnd(), Convert.ToInt32(cboADR.SelectedValue), Convert.ToInt32(txtPIB.Text), Convert.ToInt32(cboTipTransportaOtprema.SelectedValue),
                        Convert.ToInt32(cboVrstaKamionaOtprema.SelectedValue), txtVoziloOtprema.Text.ToString().TrimEnd(), txtVozacOtprema.Text.ToString().TrimEnd(), txtLKOtprema.Text.ToString().TrimEnd(),
                        txtTelefonOtprema.Text.ToString().TrimEnd(), Convert.ToInt32(cboCarinarnicaOtprema.SelectedValue), Convert.ToInt32(cboSpediterOtprema.SelectedValue), txtKontakOsobaSpediterOtprema.Text.ToString().TrimEnd(),
                        Convert.ToInt32(cboMestoIstovaraOtprema.SelectedValue), txtAdresaOtprema.Text.ToString().TrimEnd(), txtKontaktOsobaOtprema.Text.ToString().TrimEnd(), Convert.ToDateTime(planiranoVremeOtprema.Value),
                        Convert.ToDateTime(novoVremeOtprema.Value), txtKontejnerOtprema.Text.ToString().TrimEnd(), null, null,
                        null, null, null, null, null,
                        null, null, null, null, null,
                        null, null, null, txtPosebniUslovi.Text.ToString().TrimEnd(), idUsluge,
                        txtNapomena.Text.ToString().TrimEnd(), Aktivan, Formiran);

                    MessageBox.Show("RADNI NALOG SAČUVAN");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR Update \n:" + ex.ToString());
                    return;
                }
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("Delete From RNCarinskoSkladisteOtpremaStavkePom Where IDNadredjena=" + Convert.ToInt32(txtID.Text), conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                foreach (var i in list)
                {
                    ins.InsertOtpremaStavke(i.RB, i.NHM, i.Naziv, i.Naimenovanje, i.JM, i.Koleta, i.Bruto, i.Vrednost, i.Valuta, i.Pozicija, i.Paleta, i.VrstaPalete, i.PDV, i.Carina, i.Neto,
                        "", Korisnik, i.Prijemnica > 0 ? i.Prijemnica : PrijemnicaID);
                }
                MessageBox.Show("Sačuvane stavke otpreme!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR Insert otprema stavke:" + ex.ToString());
            }
        }

        private void btnPrijemnica_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            VratiPrijemnice();
        }

        private void VratiPrijemnice()
        {
            var query = @"select p.ID as Prijemnica,RN,RTrim(Partnerji.PaNaziv) as Posiljalac,p.Datum,MagacinskiBroj
from RNCarinskoSkladistePrijemnica as p
inner join Partnerji on p.Posiljalac=Partnerji.PaSifra
inner join RadniNalogSkladista on p.RN=RadniNalogSkladista.ID
Where p.Status='ZA'
order by p.ID desc";
            SqlConnection conn = new SqlConnection(connection);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            System.Data.DataSet ds = new System.Data.DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = true;
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
        }

        private void btnPrijemnicaNazad_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }
        int PrijemnicaID;
        int PrijemRN;
        private void btnPotvrdiPrijemnicu_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Selected)
                {
                    PrijemnicaID = Convert.ToInt32(row.Cells["Prijemnica"].Value);
                    PrijemRN = Convert.ToInt32(row.Cells["RN"].Value);
                    textBox2.Text = PrijemnicaID.ToString();
                    MagacinskiBroj = Convert.ToInt32(row.Cells["MagacinskiBroj"].Value);
                    cboMagacinskiBroj.SelectedValue = MagacinskiBroj;
                }
            }
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(@"select Nalogodavac,VlasnikRobe,VrstaRobe,NacinPakovanja,OstalaSkladista,PIB,PosebniUslovi,DodatneUslugeID,Napomena
From RadniNalogSkladista
Where ID=" + PrijemRN, conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        cboNalogodavac.SelectedValue = Convert.ToInt32(dr["Nalogodavac"].ToString());
                        cboVlasnikRobe.SelectedValue = Convert.ToInt32(dr["VlasnikRobe"].ToString());
                        txtVrstaRobe.Text = dr["VrstaRobe"].ToString();
                        txtNacinPakovanja.Text = dr["NacinPakovanja"].ToString();
                        cboADR.SelectedValue = Convert.ToInt32(dr["OstalaSkladista"].ToString());
                        txtPIB.Text = dr["PIB"].ToString();
                        txtPosebniUslovi.Text = dr["PosebniUslovi"].ToString();
                        txtNapomena.Text = dr["Napomena"].ToString();
                        int dodatneUslugeID = Convert.ToInt32(dr["DodatneUslugeID"].ToString());
                        FillDodatneUsluge(dodatneUslugeID.ToString());
                    }
                }
            }
            panel2.Visible = false;
        }
        public class OtpremaStavke
        {
            public int IDSkladisneStavke { get; set; }
            public int RB { get; set; }

            public string Naimenovanje { get; set; }
            public int NHM { get; set; }
            public string Naziv { get; set; }
            public string JM { get; set; }

            // Preostalo na lageru
            public decimal NerazudzenoKoleta { get; set; }
            public decimal NerazduzenaTezina { get; set; }
            public decimal OstatakValute { get; set; }

            // Kolicine/vrednosti koje korisnik rucno odredjuje za otpremu
            public decimal Koleta { get; set; }
            public decimal Bruto { get; set; }
            public decimal Neto { get; set; }
            public decimal Vrednost { get; set; }

            public string Valuta { get; set; }
            public string DokumentRazduzenja { get; set; }
            public DateTime DatumRazduzenja { get; set; }
            public string Destinacija { get; set; }
            public DateTime DatumOtpreme { get; set; }
            public string Pozicija { get; set; }
            public int Paleta { get; set; }
            public int VrstaPalete { get; set; }
            public string DimenzijePalete { get; set; }
            public int SifraArtikla { get; set; }
            public decimal PDV { get; set; }
            public decimal Carina { get; set; }
            public string TabelaPozicija { get; set; }

            // Interna veza sa prijemnicom i ukupno raspoloživo stanje za izabranu stavku.
            // Sakriveno je iz gridova, ali je potrebno za tačan obračun ostatka pri izmeni količina.
            [Browsable(false)]
            public int Prijemnica { get; set; }

            [Browsable(false)]
            public int PrijemRB { get; set; }

            [Browsable(false)]
            public decimal UkupnoKoletaLager { get; set; }

            [Browsable(false)]
            public decimal UkupnoBrutoLager { get; set; }

            [Browsable(false)]
            public decimal UkupnoNetoLager { get; set; }

            [Browsable(false)]
            public decimal UkupnoVrednostLager { get; set; }
        }

        private List<OtpremaStavke> list = new List<OtpremaStavke>();
        private int rb = 1;
        private OtpremaStavke selektovanaStavka = null;

        private void PoveziEventeStavki()
        {
            btnIzmeniStavku.Click -= btnIzmeniStavku_Click;
            btnIzmeniStavku.Click += btnIzmeniStavku_Click;

            btnIzbaciStavku.Click -= btnIzbaciStavku_Click;
            btnIzbaciStavku.Click += btnIzbaciStavku_Click;

            gridGroupingControl1.Click -= gridGroupingControl1_Click;
            gridGroupingControl1.Click += gridGroupingControl1_Click;

            gridGroupingControl1.MouseUp -= gridGroupingControl1_MouseUp;
            gridGroupingControl1.MouseUp += gridGroupingControl1_MouseUp;

            gridGroupingControl1.TableControlCellClick -= gridGroupingControl1_TableControlCellClick;
            gridGroupingControl1.TableControlCellClick += gridGroupingControl1_TableControlCellClick;
        }
        private void gridGroupingControl1_TableControlCellClick(
    object sender,
    Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlCellClickEventArgs e)
        {
            OtpremaStavke stavka = VratiStavkuIzKliknutogReda(e);

            if (stavka != null)
            {
                PopuniPanelStavke(stavka);
            }
        }
        private OtpremaStavke VratiStavkuIzKliknutogReda(
    Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlCellClickEventArgs e)
        {
            try
            {
                var style = e.TableControl.GetTableViewStyleInfo(e.Inner.RowIndex, e.Inner.ColIndex);

                if (style == null || style.TableCellIdentity == null)
                    return null;

                object displayElement = style.TableCellIdentity.DisplayElement;

                object data = VratiDataIzDisplayElement(displayElement);

                OtpremaStavke stavkaSaGrida = data as OtpremaStavke;

                if (stavkaSaGrida == null)
                    return null;

                OtpremaStavke stavkaIzListe = list.FirstOrDefault(x => x.RB == stavkaSaGrida.RB);

                if (stavkaIzListe != null)
                    return stavkaIzListe;

                return stavkaSaGrida;
            }
            catch
            {
                return null;
            }
        }

        private object VratiDataIzDisplayElement(object displayElement)
        {
            if (displayElement == null)
                return null;

            try
            {
                // Ako je displayElement direktno Record
                var getDataMethod = displayElement.GetType().GetMethod("GetData", Type.EmptyTypes);

                if (getDataMethod != null)
                {
                    object directData = getDataMethod.Invoke(displayElement, null);

                    if (directData != null)
                        return directData;
                }

                // Kod nekih verzija Syncfusion-a DisplayElement ima GetRecord()
                var getRecordMethod = displayElement.GetType().GetMethod("GetRecord", Type.EmptyTypes);

                if (getRecordMethod != null)
                {
                    object record = getRecordMethod.Invoke(displayElement, null);

                    if (record != null)
                    {
                        var recordGetData = record.GetType().GetMethod("GetData", Type.EmptyTypes);

                        if (recordGetData != null)
                            return recordGetData.Invoke(record, null);
                    }
                }

                // Kod nekih verzija postoji ParentRecord
                var parentRecordProperty = displayElement.GetType().GetProperty("ParentRecord");

                if (parentRecordProperty != null)
                {
                    object record = parentRecordProperty.GetValue(displayElement, null);

                    if (record != null)
                    {
                        var recordGetData = record.GetType().GetMethod("GetData", Type.EmptyTypes);

                        if (recordGetData != null)
                            return recordGetData.Invoke(record, null);
                    }
                }
            }
            catch
            {
                return null;
            }

            return null;
        }
        private void gridGroupingControl1_MouseUp(object sender, MouseEventArgs e)
        {
            OtpremaStavke stavka = VratiSelektovanuStavkuIzGrida();

            if (stavka != null)
            {
                PopuniPanelStavke(stavka);
            }
        }

        private OtpremaStavke VratiSelektovanuStavkuIzGrida()
        {
            if (selektovanaStavka != null)
            {
                OtpremaStavke stavkaPoSelektovanoj = list.FirstOrDefault(x => x.RB == selektovanaStavka.RB);

                if (stavkaPoSelektovanoj != null)
                    return stavkaPoSelektovanoj;
            }

            int rbStavke;

            if (int.TryParse(txtRB.Text.Trim(), out rbStavke))
            {
                OtpremaStavke stavkaPoRB = list.FirstOrDefault(x => x.RB == rbStavke);

                if (stavkaPoRB != null)
                    return stavkaPoRB;
            }

            return null;
        }

        private void PopuniPanelStavke(OtpremaStavke stavka)
        {
            if (stavka == null)
            {
                OcistiPanelStavke();
                return;
            }

            selektovanaStavka = stavka;

            txtRB.Text = stavka.RB.ToString();
            txtArtikal.Text = stavka.Naziv ?? "";
            txtKoleta.Text = stavka.Koleta.ToString("0.####");
            txtBruto.Text = stavka.Bruto.ToString("0.####");
            txtNeto.Text = stavka.Neto.ToString("0.####");
            txtPaleta.Text = stavka.Paleta.ToString();
            txtVrednost.Text = stavka.Vrednost.ToString("0.####");
            txtPDV.Text = stavka.PDV.ToString("0.####");
            txtCarina.Text = stavka.Carina.ToString("0.####");

        }

        private void OcistiPanelStavke()
        {
            selektovanaStavka = null;

            txtRB.Text = "";
            txtArtikal.Text = "";
            txtKoleta.Text = "";
            txtBruto.Text = "";
            txtNeto.Text = "";
            txtPaleta.Text = "";
            txtVrednost.Text = "";
            txtPDV.Text = "";
            txtCarina.Text = "";
        }

        private bool AzurirajStavkuIzPanela(OtpremaStavke stavka)
        {
            if (stavka == null)
                return false;

            decimal staraKoleta = stavka.Koleta;
            decimal stariBruto = stavka.Bruto;
            decimal stariNeto = stavka.Neto;
            decimal staraVrednost = stavka.Vrednost;

            decimal novaKoleta = GetDecimal(txtKoleta);
            decimal noviBruto = GetDecimal(txtBruto);
            decimal noviNeto = GetDecimal(txtNeto);
            decimal novaVrednost = GetDecimal(txtVrednost);

            if (novaKoleta < 0 || noviBruto < 0 || noviNeto < 0 || novaVrednost < 0)
            {
                MessageBox.Show("Količine i vrednosti ne mogu biti negativne.");
                return false;
            }

            decimal maxKoleta = stavka.UkupnoKoletaLager > 0 ? stavka.UkupnoKoletaLager : stavka.NerazudzenoKoleta + staraKoleta;
            decimal maxBruto = stavka.UkupnoBrutoLager > 0 ? stavka.UkupnoBrutoLager : stavka.NerazduzenaTezina + stariBruto;
            decimal maxNeto = stavka.UkupnoNetoLager > 0 ? stavka.UkupnoNetoLager : stavka.Neto;
            decimal maxVrednost = stavka.UkupnoVrednostLager > 0 ? stavka.UkupnoVrednostLager : stavka.OstatakValute + staraVrednost;

            if (maxKoleta > 0 && novaKoleta > maxKoleta)
            {
                MessageBox.Show("Uneta koleta ne mogu biti veća od ukupno raspoloživih koleta za tu stavku.");
                return false;
            }

            if (maxBruto > 0 && noviBruto > maxBruto)
            {
                MessageBox.Show("Uneti bruto ne može biti veći od ukupno raspoložive težine za tu stavku.");
                return false;
            }

            if (maxNeto > 0 && noviNeto > maxNeto)
            {
                MessageBox.Show("Uneti neto ne može biti veći od ukupno raspoloživog neta za tu stavku.");
                return false;
            }

            if (maxVrednost > 0 && novaVrednost > maxVrednost)
            {
                MessageBox.Show("Uneta vrednost ne može biti veća od ukupno raspoložive vrednosti za tu stavku.");
                return false;
            }

            stavka.Naziv = txtArtikal.Text.Trim();
            stavka.Koleta = novaKoleta;
            stavka.Bruto = noviBruto;
            stavka.Neto = noviNeto;
            stavka.Paleta = GetInt(txtPaleta);
            stavka.Vrednost = novaVrednost;
            stavka.PDV = GetDecimal(txtPDV);
            stavka.Carina = GetDecimal(txtCarina);

            // U listi otpreme kolone Nerazduženo prikazuju ostatak posle ove stavke.
            stavka.NerazudzenoKoleta = VratiNuluAkoJeMaloNegativno(maxKoleta - novaKoleta);
            stavka.NerazduzenaTezina = VratiNuluAkoJeMaloNegativno(maxBruto - noviBruto);
            stavka.OstatakValute = VratiNuluAkoJeMaloNegativno(maxVrednost - novaVrednost);

            return true;
        }


        private void btnIzmeniStavku_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRB.Text))
            {
                MessageBox.Show("Prvo izaberite stavku iz liste.");
                return;
            }

            int rbStavke;

            if (!int.TryParse(txtRB.Text.Trim(), out rbStavke))
            {
                MessageBox.Show("RB stavke nije ispravan.");
                return;
            }

            OtpremaStavke stavka = list.FirstOrDefault(x => x.RB == rbStavke);

            if (stavka == null)
            {
                MessageBox.Show("Stavka nije pronađena u listi.");
                return;
            }

            if (!AzurirajStavkuIzPanela(stavka))
                return;

            selektovanaStavka = stavka;

            OsveziGrid();
            PopuniPanelStavke(stavka);

            MessageBox.Show("Stavka je izmenjena.");
        }

        private void btnIzbaciStavku_Click(object sender, EventArgs e)
        {
            OtpremaStavke stavka = VratiSelektovanuStavkuIzGrida();

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
            RenumerisiRB();
            OsveziGrid();
            OcistiPanelStavke();
        }

        private void btnRazduzi_Click(object sender, EventArgs e)
        {
            if (MagacinskiBroj == 0)
            {
                MessageBox.Show("Nije odabran magacinski broj!");
                return;
            }

            list.Clear();

            int trenutniRN = string.IsNullOrWhiteSpace(txtID.Text) ? 0 : Convert.ToInt32(txtID.Text);

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                string query = @"
;WITH Primljeno AS
(
    SELECT 
        s.ID AS IDSkladisneStavke,
        s.IDNadredjena AS Prijemnica,
        s.RB AS PrijemRB,
        rn.MagacinskiBroj,
        s.Naimenovanje,
        s.NHM,
        s.Naziv,
        s.JM,
        ISNULL(SUM(p.PrPrimKol), 0) - ISNULL(SUM(p.PrIzdKol), 0) AS KoletaPoPrometu,
        s.Bruto AS PrimljenoBruto,
        ISNULL(s.Neto, 0) AS PrimljenoNeto,
        s.Vrednost AS PrimljenaVrednost,
        s.Valuta,
        ISNULL(skl.Naziv, s.Pozicija) AS Pozicija,
        s.Paleta,
        s.VrstaPaleta,
        tp.Dimenzije AS DimenzijaPaleta,
        s.NHM AS SifraArtikla,
        s.PDV,
        s.Carina,
        ISNULL(MAX(p.SkladisteU), '') AS TabelaPozicija
    FROM RNCarinskoPrijemnicaStavke s
    INNER JOIN RNCarinskoSkladistePrijemnica pr ON s.IDNadredjena = pr.ID
    INNER JOIN RadniNalogSkladista rn ON pr.RN = rn.ID
    LEFT JOIN Promet p ON s.ID = p.IDSaDokumenta AND p.MagacinskiBroj = rn.MagacinskiBroj
    INNER JOIN TipPalete tp ON s.VrstaPaleta = tp.ID
    LEFT JOIN Skladista skl ON s.Pozicija = skl.Naziv
    WHERE pr.Status = 'ZA'
      AND rn.MagacinskiBroj = @MagacinskiBroj
    GROUP BY 
        s.ID,
        s.IDNadredjena,
        s.RB,
        rn.MagacinskiBroj,
        s.Naimenovanje,
        s.NHM,
        s.Naziv,
        s.JM,
        s.Bruto,
        s.Neto,
        s.Vrednost,
        s.Valuta,
        s.Pozicija,
        skl.Naziv,
        s.Paleta,
        s.VrstaPaleta,
        tp.Dimenzije,
        s.PDV,
        s.Carina
),
Zavrseno AS
(
    SELECT
        ot.Prijemnica,
        st.NHM,
        st.Naziv,
        st.Naimenovanje,
        st.JM,
        st.Valuta,
        st.Pozicija,
        st.VrstaPaleta,
        SUM(ISNULL(st.Koleta, 0)) AS ZavrsenoKoleta,
        SUM(ISNULL(st.Bruto, 0)) AS ZavrsenoBruto,
        SUM(ISNULL(st.Neto, 0)) AS ZavrsenoNeto,
        SUM(ISNULL(st.Vrednost, 0)) AS ZavrsenaVrednost
    FROM RNCarinskoSkladisteOtpremnicaStavke st
    INNER JOIN RNCarinskoSkladisteOtpremnica ot
        ON st.IDNadredjena = ot.ID
    GROUP BY
        ot.Prijemnica,
        st.NHM,
        st.Naziv,
        st.Naimenovanje,
        st.JM,
        st.Valuta,
        st.Pozicija,
        st.VrstaPaleta
),
PomOstali AS
(
    SELECT Prijemnica, NHM, Naziv, Naimenovanje, JM, Valuta, Pozicija, VrstaPaleta,
           SUM(ISNULL(Koleta, 0)) AS PomKoleta,
           SUM(ISNULL(Bruto, 0)) AS PomBruto,
           SUM(ISNULL(Neto, 0)) AS PomNeto,
           SUM(ISNULL(Vrednost, 0)) AS PomVrednost
    FROM RNCarinskoSkladisteOtpremaStavkePom
    WHERE ISNULL(IDNadredjena, 0) <> @TrenutniRN
    GROUP BY Prijemnica, NHM, Naziv, Naimenovanje, JM, Valuta, Pozicija, VrstaPaleta
)
SELECT
    p.IDSkladisneStavke,
    p.Prijemnica,
    p.Naimenovanje,
    p.NHM,
    p.Naziv,
    p.JM,
    CASE WHEN p.KoletaPoPrometu - ISNULL(po.PomKoleta, 0) < 0 THEN 0 ELSE p.KoletaPoPrometu - ISNULL(po.PomKoleta, 0) END AS NerazduzenoKoleta,
    CASE WHEN p.PrimljenoBruto - ISNULL(z.ZavrsenoBruto, 0) - ISNULL(po.PomBruto, 0) < 0 THEN 0 ELSE p.PrimljenoBruto - ISNULL(z.ZavrsenoBruto, 0) - ISNULL(po.PomBruto, 0) END AS NerazduzenaTezina,
    CASE WHEN p.PrimljenoNeto - ISNULL(z.ZavrsenoNeto, 0) - ISNULL(po.PomNeto, 0) < 0 THEN 0 ELSE p.PrimljenoNeto - ISNULL(z.ZavrsenoNeto, 0) - ISNULL(po.PomNeto, 0) END AS NerazduzenoNeto,
    CASE WHEN p.PrimljenaVrednost - ISNULL(z.ZavrsenaVrednost, 0) - ISNULL(po.PomVrednost, 0) < 0 THEN 0 ELSE p.PrimljenaVrednost - ISNULL(z.ZavrsenaVrednost, 0) - ISNULL(po.PomVrednost, 0) END AS OstatakValute,
    p.Valuta,
    '' AS DokumentRazduzenja,
    GETDATE() AS DatumRazduzenja,
    '' AS Destinacija,
    GETDATE() AS DatumOtpreme,
    p.Pozicija,
    p.Paleta,
    p.VrstaPaleta,
    p.DimenzijaPaleta,
    p.SifraArtikla,
    p.PDV,
    p.Carina,
    p.TabelaPozicija
FROM Primljeno p
LEFT JOIN Zavrseno z
    ON z.Prijemnica = p.Prijemnica
   AND ISNULL(z.NHM, 0) = ISNULL(p.NHM, 0)
   AND RTRIM(ISNULL(z.Naziv, '')) = RTRIM(ISNULL(p.Naziv, ''))
   AND RTRIM(ISNULL(z.Naimenovanje, '')) = RTRIM(ISNULL(p.Naimenovanje, ''))
   AND RTRIM(ISNULL(z.JM, '')) = RTRIM(ISNULL(p.JM, ''))
   AND RTRIM(ISNULL(z.Valuta, '')) = RTRIM(ISNULL(p.Valuta, ''))
   AND RTRIM(ISNULL(z.Pozicija, '')) = RTRIM(ISNULL(p.Pozicija, ''))
   AND ISNULL(z.VrstaPaleta, 0) = ISNULL(p.VrstaPaleta, 0)
LEFT JOIN PomOstali po
    ON po.Prijemnica = p.Prijemnica
   AND ISNULL(po.NHM, 0) = ISNULL(p.NHM, 0)
   AND RTRIM(ISNULL(po.Naziv, '')) = RTRIM(ISNULL(p.Naziv, ''))
   AND RTRIM(ISNULL(po.Naimenovanje, '')) = RTRIM(ISNULL(p.Naimenovanje, ''))
   AND RTRIM(ISNULL(po.JM, '')) = RTRIM(ISNULL(p.JM, ''))
   AND RTRIM(ISNULL(po.Valuta, '')) = RTRIM(ISNULL(p.Valuta, ''))
   AND RTRIM(ISNULL(po.Pozicija, '')) = RTRIM(ISNULL(p.Pozicija, ''))
   AND ISNULL(po.VrstaPaleta, 0) = ISNULL(p.VrstaPaleta, 0)
WHERE
    (CASE WHEN p.KoletaPoPrometu - ISNULL(po.PomKoleta, 0) < 0 THEN 0 ELSE p.KoletaPoPrometu - ISNULL(po.PomKoleta, 0) END > 0.0001
     OR CASE WHEN p.PrimljenoBruto - ISNULL(z.ZavrsenoBruto, 0) - ISNULL(po.PomBruto, 0) < 0 THEN 0 ELSE p.PrimljenoBruto - ISNULL(z.ZavrsenoBruto, 0) - ISNULL(po.PomBruto, 0) END > 0.0001
     OR CASE WHEN p.PrimljenaVrednost - ISNULL(z.ZavrsenaVrednost, 0) - ISNULL(po.PomVrednost, 0) < 0 THEN 0 ELSE p.PrimljenaVrednost - ISNULL(z.ZavrsenaVrednost, 0) - ISNULL(po.PomVrednost, 0) END > 0.0001)
ORDER BY p.IDSkladisneStavke";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MagacinskiBroj", MagacinskiBroj);
                    cmd.Parameters.AddWithValue("@TrenutniRN", trenutniRN);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            decimal preostalaKoleta = dr["NerazduzenoKoleta"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["NerazduzenoKoleta"]);
                            decimal preostaliBruto = dr["NerazduzenaTezina"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["NerazduzenaTezina"]);
                            decimal preostaliNeto = dr["NerazduzenoNeto"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["NerazduzenoNeto"]);
                            decimal preostalaVrednost = dr["OstatakValute"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["OstatakValute"]);

                            var stavka = new OtpremaStavke
                            {
                                IDSkladisneStavke = dr["IDSkladisneStavke"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IDSkladisneStavke"]),
                                RB = rb,
                                Prijemnica = dr["Prijemnica"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Prijemnica"]),
                                Naimenovanje = dr["Naimenovanje"] == DBNull.Value ? "" : dr["Naimenovanje"].ToString(),
                                NHM = dr["NHM"] == DBNull.Value ? 0 : Convert.ToInt32(dr["NHM"]),
                                Naziv = dr["Naziv"] == DBNull.Value ? "" : dr["Naziv"].ToString(),
                                JM = dr["JM"] == DBNull.Value ? "" : dr["JM"].ToString(),

                                // Razduži sve uzima celu trenutno raspoloživu količinu, pa je ostatak 0.
                                NerazudzenoKoleta = 0,
                                NerazduzenaTezina = 0,
                                OstatakValute = 0,

                                Koleta = preostalaKoleta,
                                Bruto = preostaliBruto,
                                Neto = preostaliNeto,
                                Vrednost = preostalaVrednost,

                                UkupnoKoletaLager = preostalaKoleta,
                                UkupnoBrutoLager = preostaliBruto,
                                UkupnoNetoLager = preostaliNeto,
                                UkupnoVrednostLager = preostalaVrednost,

                                Valuta = dr["Valuta"] == DBNull.Value ? "" : dr["Valuta"].ToString(),
                                DokumentRazduzenja = dr["DokumentRazduzenja"] == DBNull.Value ? "" : dr["DokumentRazduzenja"].ToString(),
                                DatumRazduzenja = dr["DatumRazduzenja"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["DatumRazduzenja"]),
                                Destinacija = dr["Destinacija"] == DBNull.Value ? "" : dr["Destinacija"].ToString(),
                                DatumOtpreme = dr["DatumOtpreme"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["DatumOtpreme"]),
                                Pozicija = dr["Pozicija"] == DBNull.Value ? "" : dr["Pozicija"].ToString(),
                                Paleta = dr["Paleta"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Paleta"]),
                                VrstaPalete = dr["VrstaPaleta"] == DBNull.Value ? 0 : Convert.ToInt32(dr["VrstaPaleta"]),
                                DimenzijePalete = dr["DimenzijaPaleta"] == DBNull.Value ? "" : dr["DimenzijaPaleta"].ToString(),
                                SifraArtikla = dr["SifraArtikla"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SifraArtikla"]),
                                PDV = dr["PDV"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["PDV"]),
                                Carina = dr["Carina"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Carina"]),
                                TabelaPozicija = dr["TabelaPozicija"] == DBNull.Value ? "" : dr["TabelaPozicija"].ToString()
                            };

                            list.Add(stavka);
                            rb++;
                        }
                    }
                }
            }

            RenumerisiRB();
            OsveziGrid();
            OcistiPanelStavke();
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

        private OtpremaStavke KopirajStavku(OtpremaStavke s)
        {
            if (s == null)
                return null;

            return new OtpremaStavke
            {
                IDSkladisneStavke = s.IDSkladisneStavke,
                RB = s.RB,
                Naimenovanje = s.Naimenovanje,
                NHM = s.NHM,
                Naziv = s.Naziv,
                JM = s.JM,

                NerazudzenoKoleta = s.NerazudzenoKoleta,
                NerazduzenaTezina = s.NerazduzenaTezina,
                OstatakValute = s.OstatakValute,

                Koleta = s.Koleta,
                Bruto = s.Bruto,
                Neto = s.Neto,
                Vrednost = s.Vrednost,

                Valuta = s.Valuta,
                DokumentRazduzenja = s.DokumentRazduzenja,
                DatumRazduzenja = s.DatumRazduzenja,
                Destinacija = s.Destinacija,
                DatumOtpreme = s.DatumOtpreme,
                Pozicija = s.Pozicija,
                Paleta = s.Paleta,
                VrstaPalete = s.VrstaPalete,
                DimenzijePalete = s.DimenzijePalete,
                SifraArtikla = s.SifraArtikla,
                PDV = s.PDV,
                Carina = s.Carina,
                TabelaPozicija = s.TabelaPozicija,

                Prijemnica = s.Prijemnica,
                PrijemRB = s.PrijemRB,
                UkupnoKoletaLager = s.UkupnoKoletaLager,
                UkupnoBrutoLager = s.UkupnoBrutoLager,
                UkupnoNetoLager = s.UkupnoNetoLager,
                UkupnoVrednostLager = s.UkupnoVrednostLager
            };
        }


        private void RenumerisiRB()
        {
            int broj = 1;

            foreach (OtpremaStavke s in list)
            {
                s.RB = broj;
                broj++;
            }

            rb = broj;
        }

        private decimal VratiNuluAkoJeMaloNegativno(decimal vrednost)
        {
            const decimal eps = 0.0001m;

            if (vrednost < 0 && Math.Abs(vrednost) <= eps)
                return 0;

            return vrednost < 0 ? 0 : vrednost;
        }

        private decimal GetDecimal(TextBox tb)
        {
            if (tb == null || string.IsNullOrWhiteSpace(tb.Text))
                return 0;

            decimal value;
            if (decimal.TryParse(tb.Text.Trim(), out value))
                return value;

            return 0;
        }

        private int GetInt(TextBox tb)
        {
            if (tb == null || string.IsNullOrWhiteSpace(tb.Text))
                return 0;

            int value;
            if (int.TryParse(tb.Text.Trim(), out value))
                return value;

            decimal decValue;
            if (decimal.TryParse(tb.Text.Trim(), out decValue))
                return Convert.ToInt32(decValue);

            return 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MagacinskiBroj == 0)
            {
                MessageBox.Show("Prvo izaberite magacinski broj.");
                return;
            }

            var main = this.TopLevelControl as NewMain;

            if (main == null)
            {
                MessageBox.Show("Nije pronađena glavna forma.");
                return;
            }

            List<OtpremaStavke> postojeceStavkeZaLager = list
                .Where(x => x != null)
                .Select(x => KopirajStavku(x))
                .ToList();

            main.OtvoriFormuBezPrava(() =>
                new Saobracaj.Skladista_main.OtpremaLager(
                    postojeceStavkeZaLager,
                    izabrane =>
                    {
                        list = izabrane == null
                            ? new List<OtpremaStavke>()
                            : izabrane
                                .Where(x => x != null)
                                .Select(x => KopirajStavku(x))
                                .ToList();

                        RenumerisiRB();
                        OsveziGrid();
                        OcistiPanelStavke();
                    },
                    MagacinskiBroj,
                    string.IsNullOrWhiteSpace(txtID.Text) ? 0 : Convert.ToInt32(txtID.Text)
                )
            );
        }

        private void gridGroupingControl1_Click(object sender, EventArgs e)
        {
            OtpremaStavke stavka = VratiSelektovanuStavkuIzGrida();

            if (stavka != null)
            {
                PopuniPanelStavke(stavka);
            }
        }

        private void btnOtpremnica_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuBezPrava(() => new Otpremnica(Tip, Vrsta, Convert.ToInt32(txtID.Text)));
        }
    }
}
