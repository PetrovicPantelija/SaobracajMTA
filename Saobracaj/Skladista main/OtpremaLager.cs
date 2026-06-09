using Saobracaj.Skladista_main.Dokumenta;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main
{
    public partial class OtpremaLager : Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

        private readonly Action<List<Otprema.OtpremaStavke>> _vratiStavke;
        private readonly List<Otprema.OtpremaStavke> postojeceStavke;

        private BindingList<Otprema.OtpremaStavke> lagerStavke =
            new BindingList<Otprema.OtpremaStavke>();

        private BindingList<Otprema.OtpremaStavke> izabraneStavke =
            new BindingList<Otprema.OtpremaStavke>();

        public List<Otprema.OtpremaStavke> IzabraneStavke { get; private set; }

        private int MagacinskiBroj;
        private int RadniNalogOtpreme;
        private bool ucitavanje = false;
        private const decimal EPS = 0.0001m;

        // Pamti poslednju stvarno izabranu stavku iz dataGridView2.
        // Ovo sprečava da btnIzmeniStavku upiše nule kada panel nije popunjen.
        private Otprema.OtpremaStavke selektovanaStavka = null;

        // Otprema.OtpremaStavke nema posebna polja za ukupne vrednosti.
        // Zato ih čuvamo interno po ključu stavke.
        // U dataGridView1 polja NerazduzenoKoleta/NerazduzenaTezina/OstatakValute prikazuju TRENUTNI ostatak,
        // a ove mape čuvaju UKUPNO raspoloživo za kontrolu i ponovno računanje ostatka.
        private readonly Dictionary<string, decimal> ukupnoKoletaPoKljucu = new Dictionary<string, decimal>();
        private readonly Dictionary<string, decimal> ukupnoBrutoPoKljucu = new Dictionary<string, decimal>();
        private readonly Dictionary<string, decimal> ukupnoNetoPoKljucu = new Dictionary<string, decimal>();
        private readonly Dictionary<string, decimal> ukupnoVrednostPoKljucu = new Dictionary<string, decimal>();

        public OtpremaLager()
        {
            InitializeComponent();
            postojeceStavke = new List<Otprema.OtpremaStavke>();
            IzabraneStavke = new List<Otprema.OtpremaStavke>();
        }

        public OtpremaLager(
            List<Otprema.OtpremaStavke> postojeceStavke,
            Action<List<Otprema.OtpremaStavke>> vratiStavke,
            int magacinskiBroj,
            int radniNalogOtpreme = 0)
        {
            InitializeComponent();

            MagacinskiBroj = magacinskiBroj;
            RadniNalogOtpreme = radniNalogOtpreme;
            _vratiStavke = vratiStavke;

            this.postojeceStavke = postojeceStavke == null
                ? new List<Otprema.OtpremaStavke>()
                : postojeceStavke
                    .Where(x => x != null)
                    .Select(x => KopirajStavku(x))
                    .ToList();

            IzabraneStavke = new List<Otprema.OtpremaStavke>();
        }

        private void OtpremaLager_Load(object sender, EventArgs e)
        {
            PoveziGridove();
            PoveziEventeStavki();
            FillCombo();
            SrediGridove();
            OcistiPanelStavke();

            izabraneStavke.Clear();
            dataGridView2.Refresh();

            if (MagacinskiBroj != 0)
            {
                UcitajLagerPoMagacinskomBroju(MagacinskiBroj);
            }

            UcitajPostojeceIzabraneStavke();
        }

        private void UcitajPostojeceIzabraneStavke()
        {
            if (postojeceStavke == null || postojeceStavke.Count == 0)
            {
                RenumerisiIzabrane();
                dataGridView1.Refresh();
                dataGridView2.Refresh();
                SrediGridove();
                return;
            }

            izabraneStavke.Clear();

            foreach (Otprema.OtpremaStavke postojeca in postojeceStavke.Where(x => x != null))
            {
                Otprema.OtpremaStavke izabrana = KopirajStavku(postojeca);
                Otprema.OtpremaStavke lager = NadjiStavkuPoKljucu(lagerStavke, izabrana);

                if (lager != null)
                {
                    // Lager već sadrži ukupno raspoloživo za ovaj RN, bez drugih započetih naloga.
                    UpisiUkupneVrednosti(
                        izabrana,
                        lager.UkupnoKoletaLager > 0 ? lager.UkupnoKoletaLager : lager.NerazudzenoKoleta,
                        lager.UkupnoBrutoLager > 0 ? lager.UkupnoBrutoLager : lager.NerazduzenaTezina,
                        lager.UkupnoNetoLager > 0 ? lager.UkupnoNetoLager : lager.Neto,
                        lager.UkupnoVrednostLager > 0 ? lager.UkupnoVrednostLager : lager.OstatakValute);
                }
                else
                {
                    UpisiUkupneVrednostiAkoNedostaje(izabrana);
                }

                OgranicenjeIzabraneStavkeNaUkupno(izabrana);
                izabraneStavke.Add(izabrana);
            }

            RenumerisiIzabrane();

            foreach (Otprema.OtpremaStavke izabrana in izabraneStavke.ToList())
            {
                OsveziPreostaloNaLageruZaIzabranuStavku(izabrana);
            }

            dataGridView1.Refresh();
            dataGridView2.Refresh();
            SrediGridove();

            if (izabraneStavke.Count > 0)
            {
                SelektujStavkuUDataGridView2(izabraneStavke[0]);
                PopuniPanelStavke(izabraneStavke[0]);
            }
        }


        private void PoveziGridove()
        {
            dataGridView1.AutoGenerateColumns = true;
            dataGridView2.AutoGenerateColumns = true;

            dataGridView1.DataSource = lagerStavke;
            dataGridView2.DataSource = izabraneStavke;

            dataGridView1.ReadOnly = true;
            dataGridView2.ReadOnly = true;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridView1.MultiSelect = true;
            dataGridView2.MultiSelect = true;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        }
        private void PodesiSirinuKolona()
        {
            PodesiSirinuKolona(dataGridView1);
            PodesiSirinuKolona(dataGridView2);
        }

        private void PodesiSirinuKolona(DataGridView dgv)
        {
            if (dgv == null)
                return;

            if (dgv.Columns.Count == 0)
                return;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            if (dgv.Rows.Count == 0)
            {
                dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.ColumnHeader);
            }
            else
            {
                dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (!col.Visible)
                    continue;

                if (col.Width < 60)
                    col.Width = 60;
            }
        }
        private void PoveziEventeStavki()
        {
            btnIzmeniStavku.Click -= btnIzmeniStavku_Click;
            btnIzmeniStavku.Click += btnIzmeniStavku_Click;

            btnIzbaciStavku.Click -= btnIzbaciStavku_Click;
            btnIzbaciStavku.Click += btnIzbaciStavku_Click;

            dataGridView2.SelectionChanged -= dataGridView2_SelectionChanged;
            dataGridView2.SelectionChanged += dataGridView2_SelectionChanged;

            dataGridView2.CellClick -= dataGridView2_CellClick;
            dataGridView2.CellClick += dataGridView2_CellClick;

            dataGridView2.MouseUp -= dataGridView2_MouseUp;
            dataGridView2.MouseUp += dataGridView2_MouseUp;
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            PopuniPanelIzDataGridView2();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            PopuniPanelIzDataGridView2();
        }

        private void dataGridView2_MouseUp(object sender, MouseEventArgs e)
        {
            PopuniPanelIzDataGridView2();
        }

        private void PopuniPanelIzDataGridView2()
        {
            Otprema.OtpremaStavke stavka = VratiSelektovanuStavku(dataGridView2);

            if (stavka != null)
                PopuniPanelStavke(stavka);
        }

        private void FillCombo()
        {
            ucitavanje = true;

            if (MagacinskiBroj != 0)
            {
                PopuniComboZaProsledjeniMagacinskiBroj(MagacinskiBroj);

                cboMB.Enabled = false;
                cboVlasnik.Enabled = false;

                ucitavanje = false;
                return;
            }

            using (SqlConnection conn = new SqlConnection(connection))
            {
                string mb = @"
SELECT 
    Promet.MagacinskiBroj AS ID,
    CAST(Promet.MagacinskiBroj AS nvarchar(20)) + ' - ' + RTRIM(ISNULL(MagacinskiBrojCarinski.Naziv, '')) AS Naziv
FROM RNCarinskoPrijemnicaStavke
INNER JOIN RNCarinskoSkladistePrijemnica 
    ON RNCarinskoPrijemnicaStavke.IDNadredjena = RNCarinskoSkladistePrijemnica.ID
INNER JOIN RadniNalogSkladista 
    ON RNCarinskoSkladistePrijemnica.RN = RadniNalogSkladista.ID
INNER JOIN Promet 
    ON RNCarinskoPrijemnicaStavke.ID = Promet.IDSaDokumenta
LEFT JOIN MagacinskiBrojCarinski 
    ON Promet.MagacinskiBroj = MagacinskiBrojCarinski.ID
WHERE RNCarinskoSkladistePrijemnica.Status = 'ZA'
GROUP BY 
    Promet.MagacinskiBroj,
    MagacinskiBrojCarinski.Naziv
HAVING ISNULL(SUM(Promet.PrPrimKol), 0) - ISNULL(SUM(Promet.PrIzdKol), 0) > 0
ORDER BY Promet.MagacinskiBroj DESC";

                SqlDataAdapter daMb = new SqlDataAdapter(mb, conn);
                DataSet dsMB = new DataSet();
                daMb.Fill(dsMB);

                cboMB.DataSource = dsMB.Tables[0];
                cboMB.DisplayMember = "Naziv";
                cboMB.ValueMember = "ID";

                string vlasnik = @"
SELECT 
    Partnerji.PaSifra AS ID,
    RTRIM(Partnerji.PaNaziv) AS Naziv
FROM RNCarinskoPrijemnicaStavke
INNER JOIN RNCarinskoSkladistePrijemnica 
    ON RNCarinskoPrijemnicaStavke.IDNadredjena = RNCarinskoSkladistePrijemnica.ID
INNER JOIN RadniNalogSkladista 
    ON RNCarinskoSkladistePrijemnica.RN = RadniNalogSkladista.ID
INNER JOIN Partnerji 
    ON RadniNalogSkladista.VlasnikRobe = Partnerji.PaSifra
INNER JOIN Promet 
    ON RNCarinskoPrijemnicaStavke.ID = Promet.IDSaDokumenta
WHERE RNCarinskoSkladistePrijemnica.Status = 'ZA'
GROUP BY 
    Partnerji.PaSifra,
    Partnerji.PaNaziv
HAVING ISNULL(SUM(Promet.PrPrimKol), 0) - ISNULL(SUM(Promet.PrIzdKol), 0) > 0
ORDER BY RTRIM(Partnerji.PaNaziv)";

                SqlDataAdapter daVlasnik = new SqlDataAdapter(vlasnik, conn);
                DataSet dsVlasnik = new DataSet();
                daVlasnik.Fill(dsVlasnik);

                cboVlasnik.DataSource = dsVlasnik.Tables[0];
                cboVlasnik.DisplayMember = "Naziv";
                cboVlasnik.ValueMember = "ID";
            }

            ucitavanje = false;
        }

        private void PopuniComboZaProsledjeniMagacinskiBroj(int magacinskiBroj)
        {
            DataTable dtMB = new DataTable();
            dtMB.Columns.Add("ID", typeof(int));
            dtMB.Columns.Add("Naziv", typeof(string));

            DataTable dtVlasnik = new DataTable();
            dtVlasnik.Columns.Add("ID", typeof(int));
            dtVlasnik.Columns.Add("Naziv", typeof(string));

            using (SqlConnection conn = new SqlConnection(connection))
            using (SqlCommand cmd = new SqlCommand(@"
SELECT TOP 1
    Promet.MagacinskiBroj AS MagacinskiBroj,
    CAST(Promet.MagacinskiBroj AS nvarchar(20)) + ' - ' + RTRIM(ISNULL(MagacinskiBrojCarinski.Naziv, '')) AS MagacinskiBrojNaziv,
    Partnerji.PaSifra AS VlasnikID,
    RTRIM(Partnerji.PaNaziv) AS VlasnikNaziv
FROM RNCarinskoPrijemnicaStavke
INNER JOIN RNCarinskoSkladistePrijemnica 
    ON RNCarinskoPrijemnicaStavke.IDNadredjena = RNCarinskoSkladistePrijemnica.ID
INNER JOIN RadniNalogSkladista 
    ON RNCarinskoSkladistePrijemnica.RN = RadniNalogSkladista.ID
INNER JOIN Promet 
    ON RNCarinskoPrijemnicaStavke.ID = Promet.IDSaDokumenta
LEFT JOIN MagacinskiBrojCarinski 
    ON Promet.MagacinskiBroj = MagacinskiBrojCarinski.ID
LEFT JOIN Partnerji 
    ON RadniNalogSkladista.VlasnikRobe = Partnerji.PaSifra
WHERE 
    RNCarinskoSkladistePrijemnica.Status = 'ZA'
    AND Promet.MagacinskiBroj = @MagacinskiBroj
ORDER BY Promet.MagacinskiBroj DESC", conn))
            {
                cmd.Parameters.AddWithValue("@MagacinskiBroj", magacinskiBroj);
                cmd.Parameters.AddWithValue("@TrenutniRN", RadniNalogOtpreme);

                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        dtMB.Rows.Add(
                            GetInt(dr, "MagacinskiBroj"),
                            GetString(dr, "MagacinskiBrojNaziv")
                        );

                        dtVlasnik.Rows.Add(
                            GetInt(dr, "VlasnikID"),
                            GetString(dr, "VlasnikNaziv")
                        );
                    }
                    else
                    {
                        dtMB.Rows.Add(magacinskiBroj, magacinskiBroj.ToString());
                    }
                }
            }

            cboMB.DataSource = dtMB;
            cboMB.DisplayMember = "Naziv";
            cboMB.ValueMember = "ID";

            cboVlasnik.DataSource = dtVlasnik;
            cboVlasnik.DisplayMember = "Naziv";
            cboVlasnik.ValueMember = "ID";

            if (cboMB.Items.Count > 0)
                cboMB.SelectedIndex = 0;

            if (cboVlasnik.Items.Count > 0)
                cboVlasnik.SelectedIndex = 0;
        }

        private void cboMB_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ucitavanje)
                return;

            if (cboMB.SelectedValue == null || cboMB.SelectedValue is DataRowView)
                return;

            int magacinskiBroj = Convert.ToInt32(cboMB.SelectedValue);
            UcitajLagerPoMagacinskomBroju(magacinskiBroj);
        }

        private void cboVlasnik_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ucitavanje)
                return;

            if (cboVlasnik.SelectedValue == null || cboVlasnik.SelectedValue is DataRowView)
                return;

            int vlasnik = Convert.ToInt32(cboVlasnik.SelectedValue);
            UcitajLagerPoVlasniku(vlasnik);
        }

        private void UcitajLagerPoMagacinskomBroju(int magacinskiBroj)
        {
            lagerStavke.Clear();
            izabraneStavke.Clear();
            ukupnoKoletaPoKljucu.Clear();
            ukupnoBrutoPoKljucu.Clear();
            ukupnoNetoPoKljucu.Clear();
            ukupnoVrednostPoKljucu.Clear();
            OcistiPanelStavke();

            string query = OsnovniQuery() + @"
    AND MagacinskiBroj = @MagacinskiBroj
" + GroupByQuery();

            using (SqlConnection conn = new SqlConnection(connection))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MagacinskiBroj", magacinskiBroj);
                cmd.Parameters.AddWithValue("@TrenutniRN", RadniNalogOtpreme);

                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Otprema.OtpremaStavke stavka = MapirajStavku(dr);
                        lagerStavke.Add(stavka);
                    }
                }
            }

            dataGridView1.Refresh();
            dataGridView2.Refresh();
            SrediGridove();
        }

        private void UcitajLagerPoVlasniku(int vlasnik)
        {
            lagerStavke.Clear();
            izabraneStavke.Clear();
            ukupnoKoletaPoKljucu.Clear();
            ukupnoBrutoPoKljucu.Clear();
            ukupnoNetoPoKljucu.Clear();
            ukupnoVrednostPoKljucu.Clear();
            OcistiPanelStavke();

            string query = OsnovniQuery() + @"
    AND VlasnikRobe = @Vlasnik
" + GroupByQuery();

            using (SqlConnection conn = new SqlConnection(connection))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Vlasnik", vlasnik);
                cmd.Parameters.AddWithValue("@TrenutniRN", RadniNalogOtpreme);

                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Otprema.OtpremaStavke stavka = MapirajStavku(dr);
                        lagerStavke.Add(stavka);
                    }
                }
            }

            dataGridView1.Refresh();
            dataGridView2.Refresh();
            SrediGridove();
        }

        private string OsnovniQuery()
        {
            return @"
;WITH Primljeno AS
(
    SELECT 
        s.ID AS IDSkladisneStavke,
        s.IDNadredjena AS Prijemnica,
        s.RB AS PrijemRB,
        rn.MagacinskiBroj,
        rn.VlasnikRobe,
        s.Naimenovanje,
        s.NHM AS NHM,
        s.Naziv AS Naziv,
        s.JM,
        ISNULL(SUM(p.PrPrimKol), 0) - ISNULL(SUM(p.PrIzdKol), 0) AS KoletaPoPrometu,
        s.Koleta AS PrimljenoKoleta,
        s.Bruto AS PrimljenoBruto,
        ISNULL(s.Neto, 0) AS PrimljenoNeto,
        s.Vrednost AS PrimljenaVrednost,
        s.Valuta,
        '' AS DokumentRazduzenja,
        GETDATE() AS DatumRazduzenja,
        '' AS Destinacija,
        GETDATE() AS DatumOtpreme,
        ISNULL(skl.Naziv, s.Pozicija) AS Pozicija,
        s.Paleta,
        s.VrstaPaleta,
        tp.Dimenzije AS DimenzijePalete,
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
    WHERE pr.Status = 'ZA'
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
    WHERE ISNULL(IDNadredjena, 0) <> ISNULL(@TrenutniRN, 0)
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
Stock AS
(
    SELECT
        p.IDSkladisneStavke,
        p.Prijemnica,
        p.PrijemRB,
        p.MagacinskiBroj,
        p.VlasnikRobe,
        p.Naimenovanje,
        p.NHM,
        p.Naziv,
        p.JM,
        NerazduzenoKoleta =
            CASE WHEN p.KoletaPoPrometu - ISNULL(po.PomKoleta, 0) < 0 THEN 0
                 ELSE p.KoletaPoPrometu - ISNULL(po.PomKoleta, 0) END,
        NerazduzenaTezina =
            CASE WHEN p.PrimljenoBruto - ISNULL(z.ZavrsenoBruto, 0) - ISNULL(po.PomBruto, 0) < 0 THEN 0
                 ELSE p.PrimljenoBruto - ISNULL(z.ZavrsenoBruto, 0) - ISNULL(po.PomBruto, 0) END,
        NerazduzenoNeto =
            CASE WHEN p.PrimljenoNeto - ISNULL(z.ZavrsenoNeto, 0) - ISNULL(po.PomNeto, 0) < 0 THEN 0
                 ELSE p.PrimljenoNeto - ISNULL(z.ZavrsenoNeto, 0) - ISNULL(po.PomNeto, 0) END,
        OstatakValute =
            CASE WHEN p.PrimljenaVrednost - ISNULL(z.ZavrsenaVrednost, 0) - ISNULL(po.PomVrednost, 0) < 0 THEN 0
                 ELSE p.PrimljenaVrednost - ISNULL(z.ZavrsenaVrednost, 0) - ISNULL(po.PomVrednost, 0) END,
        p.Valuta,
        p.DokumentRazduzenja,
        p.DatumRazduzenja,
        p.Destinacija,
        p.DatumOtpreme,
        p.Pozicija,
        p.Paleta,
        p.VrstaPaleta,
        p.DimenzijePalete,
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
)
SELECT
    IDSkladisneStavke,
    Prijemnica,
    PrijemRB,
    MagacinskiBroj,
    VlasnikRobe,
    Naimenovanje,
    NHM,
    Naziv,
    JM,
    NerazduzenoKoleta,
    NerazduzenaTezina,
    NerazduzenoNeto,
    OstatakValute,
    Valuta,
    DokumentRazduzenja,
    DatumRazduzenja,
    Destinacija,
    DatumOtpreme,
    Pozicija,
    Paleta,
    VrstaPaleta,
    DimenzijePalete,
    SifraArtikla,
    PDV,
    Carina,
    TabelaPozicija
FROM Stock
WHERE
    (NerazduzenoKoleta > 0.0001 OR NerazduzenaTezina > 0.0001 OR NerazduzenoNeto > 0.0001 OR OstatakValute > 0.0001)
";
        }


        private string GroupByQuery()
        {
            return @"
ORDER BY IDSkladisneStavke";
        }


        private Otprema.OtpremaStavke MapirajStavku(SqlDataReader dr)
        {
            decimal preostalaKoleta = GetDecimal(dr, "NerazduzenoKoleta");
            decimal preostaliBruto = GetDecimal(dr, "NerazduzenaTezina");
            decimal preostaliNeto = GetDecimal(dr, "NerazduzenoNeto");
            decimal preostalaVrednost = GetDecimal(dr, "OstatakValute");

            Otprema.OtpremaStavke stavka = new Otprema.OtpremaStavke
            {
                IDSkladisneStavke = GetInt(dr, "IDSkladisneStavke"),
                RB = GetInt(dr, "PrijemRB"),
                PrijemRB = GetInt(dr, "PrijemRB"),
                Prijemnica = GetInt(dr, "Prijemnica"),
                Naimenovanje = GetString(dr, "Naimenovanje"),
                NHM = GetInt(dr, "NHM"),
                Naziv = GetString(dr, "Naziv"),
                JM = GetString(dr, "JM"),

                NerazudzenoKoleta = preostalaKoleta,
                NerazduzenaTezina = preostaliBruto,
                OstatakValute = preostalaVrednost,

                // U gornjem gridu Koleta/Bruto/Neto/Vrednost predstavljaju trenutno raspoloživi ostatak.
                Koleta = preostalaKoleta,
                Bruto = preostaliBruto,
                Neto = preostaliNeto,
                Vrednost = preostalaVrednost,

                UkupnoKoletaLager = preostalaKoleta,
                UkupnoBrutoLager = preostaliBruto,
                UkupnoNetoLager = preostaliNeto,
                UkupnoVrednostLager = preostalaVrednost,

                Valuta = GetString(dr, "Valuta"),
                DokumentRazduzenja = GetString(dr, "DokumentRazduzenja"),
                DatumRazduzenja = GetDateTime(dr, "DatumRazduzenja"),
                Destinacija = GetString(dr, "Destinacija"),
                DatumOtpreme = GetDateTime(dr, "DatumOtpreme"),
                Pozicija = GetString(dr, "Pozicija"),
                Paleta = GetInt(dr, "Paleta"),
                VrstaPalete = GetInt(dr, "VrstaPaleta"),
                DimenzijePalete = GetString(dr, "DimenzijePalete"),
                SifraArtikla = GetInt(dr, "SifraArtikla"),
                PDV = GetDecimal(dr, "PDV"),
                Carina = GetDecimal(dr, "Carina"),
                TabelaPozicija = GetString(dr, "TabelaPozicija")
            };

            UpisiUkupneVrednosti(stavka, preostalaKoleta, preostaliBruto, preostaliNeto, preostalaVrednost);
            return stavka;
        }


        private void UpisiUkupneVrednosti(
            Otprema.OtpremaStavke stavka,
            decimal ukupnoKoleta,
            decimal ukupnoBruto,
            decimal ukupnoNeto,
            decimal ukupnoVrednost)
        {
            if (stavka == null)
                return;

            string key = KljucStavke(stavka);

            ukupnoKoletaPoKljucu[key] = ukupnoKoleta;
            ukupnoBrutoPoKljucu[key] = ukupnoBruto;
            ukupnoNetoPoKljucu[key] = ukupnoNeto;
            ukupnoVrednostPoKljucu[key] = ukupnoVrednost;

            stavka.UkupnoKoletaLager = ukupnoKoleta;
            stavka.UkupnoBrutoLager = ukupnoBruto;
            stavka.UkupnoNetoLager = ukupnoNeto;
            stavka.UkupnoVrednostLager = ukupnoVrednost;
        }


        private void UpisiUkupneVrednostiAkoNedostaje(Otprema.OtpremaStavke stavka)
        {
            if (stavka == null)
                return;

            string key = KljucStavke(stavka);

            if (!ukupnoKoletaPoKljucu.ContainsKey(key))
                ukupnoKoletaPoKljucu[key] = stavka.UkupnoKoletaLager > 0 ? stavka.UkupnoKoletaLager : (stavka.NerazudzenoKoleta + stavka.Koleta);

            if (!ukupnoBrutoPoKljucu.ContainsKey(key))
                ukupnoBrutoPoKljucu[key] = stavka.UkupnoBrutoLager > 0 ? stavka.UkupnoBrutoLager : (stavka.NerazduzenaTezina + stavka.Bruto);

            if (!ukupnoNetoPoKljucu.ContainsKey(key))
                ukupnoNetoPoKljucu[key] = stavka.UkupnoNetoLager > 0 ? stavka.UkupnoNetoLager : stavka.Neto;

            if (!ukupnoVrednostPoKljucu.ContainsKey(key))
                ukupnoVrednostPoKljucu[key] = stavka.UkupnoVrednostLager > 0 ? stavka.UkupnoVrednostLager : (stavka.OstatakValute + stavka.Vrednost);

            stavka.UkupnoKoletaLager = ukupnoKoletaPoKljucu[key];
            stavka.UkupnoBrutoLager = ukupnoBrutoPoKljucu[key];
            stavka.UkupnoNetoLager = ukupnoNetoPoKljucu[key];
            stavka.UkupnoVrednostLager = ukupnoVrednostPoKljucu[key];
        }


        private void btnDodaj_Click(object sender, EventArgs e)
        {
            List<Otprema.OtpremaStavke> zaPrebacivanje = VratiSelektovane(dataGridView1);

            if (zaPrebacivanje.Count == 0)
            {
                MessageBox.Show("Izaberite stavku iz lagera.");
                return;
            }

            Otprema.OtpremaStavke poslednjaDodata = null;

            foreach (Otprema.OtpremaStavke stavka in zaPrebacivanje.ToList())
            {
                UpisiUkupneVrednostiAkoNedostaje(stavka);

                Otprema.OtpremaStavke postojecaIzabrana = NadjiStavkuPoKljucu(izabraneStavke, stavka);

                if (postojecaIzabrana == null)
                {
                    poslednjaDodata = KopirajStavku(stavka);

                    // Stavka koja je izabrana mora da nosi ukupno raspoloživo stanje,
                    // a ne samo trenutni ostatak iz dataGridView1.
                    poslednjaDodata.NerazudzenoKoleta = VratiUkupnoKoleta(stavka);
                    poslednjaDodata.NerazduzenaTezina = VratiUkupnoBruto(stavka);
                    poslednjaDodata.OstatakValute = VratiUkupnoVrednost(stavka);

                    izabraneStavke.Add(poslednjaDodata);
                }
                else
                {
                    DodajKolicinuNaIzabranu(postojecaIzabrana, stavka);
                    poslednjaDodata = postojecaIzabrana;
                }

                lagerStavke.Remove(stavka);
            }

            RenumerisiIzabrane();

            foreach (Otprema.OtpremaStavke izabrana in izabraneStavke.ToList())
            {
                OsveziPreostaloNaLageruZaIzabranuStavku(izabrana);
            }

            dataGridView1.Refresh();
            dataGridView2.Refresh();
            SrediGridove();

            if (poslednjaDodata != null)
            {
                SelektujStavkuUDataGridView2(poslednjaDodata);
                PopuniPanelStavke(poslednjaDodata);
            }
        }

        private void btnUkoloni_Click(object sender, EventArgs e)
        {
            VratiSelektovaneIzIzabranihNaLager();
        }

        private void btnIzbaciStavku_Click(object sender, EventArgs e)
        {
            Otprema.OtpremaStavke stavka = VratiStavkuZaIzmenuIzPanelaIliGrida();

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

            VratiJednuIzabranuStavkuNaLager(stavka);
            OcistiPanelStavke();
        }

        private void VratiSelektovaneIzIzabranihNaLager()
        {
            List<Otprema.OtpremaStavke> zaVracanje = VratiSelektovane(dataGridView2);

            if (zaVracanje.Count == 0)
            {
                MessageBox.Show("Izaberite stavku koju želite da uklonite.");
                return;
            }

            foreach (Otprema.OtpremaStavke stavka in zaVracanje.ToList())
            {
                VratiJednuIzabranuStavkuNaLager(stavka, false);
            }

            RenumerisiIzabrane();
            dataGridView1.Refresh();
            dataGridView2.Refresh();
            SrediGridove();
            OcistiPanelStavke();
        }

        private void VratiJednuIzabranuStavkuNaLager(Otprema.OtpremaStavke stavka)
        {
            VratiJednuIzabranuStavkuNaLager(stavka, true);
        }

        private void VratiJednuIzabranuStavkuNaLager(Otprema.OtpremaStavke stavka, bool osvezi)
        {
            if (stavka == null)
                return;

            DodajKolicinuNaLager(stavka);
            izabraneStavke.Remove(stavka);

            if (osvezi)
            {
                RenumerisiIzabrane();
                dataGridView1.Refresh();
                dataGridView2.Refresh();
                SrediGridove();
            }
        }

        private void btnIzmeniStavku_Click(object sender, EventArgs e)
        {
            Otprema.OtpremaStavke stavka = VratiStavkuZaIzmenuIzPanelaIliGrida();

            if (stavka == null)
            {
                MessageBox.Show("Izaberite stavku iz donje liste koju želite da izmenite.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtRB.Text))
            {
                MessageBox.Show("Prvo izaberite stavku iz donje liste.");
                return;
            }

            if (!AzurirajStavkuIzPanela(stavka))
                return;

            selektovanaStavka = stavka;

            // Posle izmene uzete količine, dataGridView1 mora da prikaže novi ostatak.
            OsveziPreostaloNaLageruZaIzabranuStavku(stavka);

            dataGridView1.Refresh();
            dataGridView2.Refresh();
            PopuniPanelStavke(stavka);
            SrediGridove();

            MessageBox.Show("Stavka je izmenjena.");
        }

        private bool AzurirajStavkuIzPanela(Otprema.OtpremaStavke stavka)
        {
            if (stavka == null)
                return false;

            decimal novaKoleta = GetDecimal(txtKoleta);
            decimal noviBruto = GetDecimal(txtBruto);
            decimal noviNeto = GetDecimal(txtNeto);
            decimal novaVrednost = GetDecimal(txtVrednost);

            if (novaKoleta < 0 || noviBruto < 0 || noviNeto < 0 || novaVrednost < 0)
            {
                MessageBox.Show("Količine i vrednosti ne mogu biti negativne.");
                return false;
            }

            decimal ukupnoKoleta = VratiUkupnoKoleta(stavka);
            decimal ukupnoBruto = VratiUkupnoBruto(stavka);
            decimal ukupnoNeto = VratiUkupnoNeto(stavka);
            decimal ukupnoVrednost = VratiUkupnoVrednost(stavka);

            if (ukupnoKoleta > 0 && novaKoleta > ukupnoKoleta)
            {
                MessageBox.Show("Uneta koleta ne mogu biti veća od nerazduženih koleta.");
                return false;
            }

            if (ukupnoBruto > 0 && noviBruto > ukupnoBruto)
            {
                MessageBox.Show("Uneti bruto ne može biti veći od nerazdužene težine.");
                return false;
            }

            if (ukupnoNeto > 0 && noviNeto > ukupnoNeto)
            {
                MessageBox.Show("Uneti neto ne može biti veći od nerazduženog neta.");
                return false;
            }

            if (ukupnoVrednost > 0 && novaVrednost > ukupnoVrednost)
            {
                MessageBox.Show("Uneta vrednost ne može biti veća od ostatka valute.");
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

            return true;
        }

        private void OsveziPreostaloNaLageruZaIzabranuStavku(Otprema.OtpremaStavke izabrana)
        {
            if (izabrana == null)
                return;

            string key = KljucStavke(izabrana);
            UpisiUkupneVrednostiAkoNedostaje(izabrana);

            Otprema.OtpremaStavke postojecaNaLageru = NadjiStavkuPoKljucu(lagerStavke, izabrana);

            if (postojecaNaLageru != null)
                lagerStavke.Remove(postojecaNaLageru);

            decimal ukupnoKoleta = VratiUkupnoKoleta(izabrana);
            decimal ukupnoBruto = VratiUkupnoBruto(izabrana);
            decimal ukupnoNeto = VratiUkupnoNeto(izabrana);
            decimal ukupnoVrednost = VratiUkupnoVrednost(izabrana);

            decimal ostatakKoleta = VratiNuluAkoJeMaloNegativno(ukupnoKoleta - izabrana.Koleta);
            decimal ostatakBruto = VratiNuluAkoJeMaloNegativno(ukupnoBruto - izabrana.Bruto);
            decimal ostatakNeto = VratiNuluAkoJeMaloNegativno(ukupnoNeto - izabrana.Neto);
            decimal ostatakVrednost = VratiNuluAkoJeMaloNegativno(ukupnoVrednost - izabrana.Vrednost);

            // U donjem gridu uzeta stavka takođe pokazuje preostalo stanje posle te uzete količine.
            izabrana.NerazudzenoKoleta = Math.Max(0, ostatakKoleta);
            izabrana.NerazduzenaTezina = Math.Max(0, ostatakBruto);
            izabrana.OstatakValute = Math.Max(0, ostatakVrednost);
            izabrana.UkupnoKoletaLager = ukupnoKoleta;
            izabrana.UkupnoBrutoLager = ukupnoBruto;
            izabrana.UkupnoNetoLager = ukupnoNeto;
            izabrana.UkupnoVrednostLager = ukupnoVrednost;

            if (ostatakKoleta <= EPS && ostatakBruto <= EPS && ostatakNeto <= EPS && ostatakVrednost <= EPS)
                return;

            Otprema.OtpremaStavke ostatak = KopirajStavku(izabrana);
            ostatak.RB = izabrana.PrijemRB > 0 ? izabrana.PrijemRB : izabrana.RB; // u lager gridu prikazujemo RB prijemne stavke ako postoji; ne postavljamo 0.

            ostatak.NerazudzenoKoleta = Math.Max(0, ostatakKoleta);
            ostatak.NerazduzenaTezina = Math.Max(0, ostatakBruto);
            ostatak.OstatakValute = Math.Max(0, ostatakVrednost);

            ostatak.Koleta = Math.Max(0, ostatakKoleta);
            ostatak.Bruto = Math.Max(0, ostatakBruto);
            ostatak.Neto = Math.Max(0, ostatakNeto);
            ostatak.Vrednost = Math.Max(0, ostatakVrednost);

            ostatak.UkupnoKoletaLager = ukupnoKoleta;
            ostatak.UkupnoBrutoLager = ukupnoBruto;
            ostatak.UkupnoNetoLager = ukupnoNeto;
            ostatak.UkupnoVrednostLager = ukupnoVrednost;

            lagerStavke.Add(ostatak);

            ukupnoKoletaPoKljucu[key] = ukupnoKoleta;
            ukupnoBrutoPoKljucu[key] = ukupnoBruto;
            ukupnoNetoPoKljucu[key] = ukupnoNeto;
            ukupnoVrednostPoKljucu[key] = ukupnoVrednost;
        }


        private void DodajKolicinuNaLager(Otprema.OtpremaStavke stavka)
        {
            if (stavka == null)
                return;

            UpisiUkupneVrednostiAkoNedostaje(stavka);

            Otprema.OtpremaStavke postojeca = NadjiStavkuPoKljucu(lagerStavke, stavka);

            if (postojeca == null)
            {
                postojeca = KopirajStavku(stavka);
                lagerStavke.Add(postojeca);
            }
            else
            {
                postojeca.Koleta += stavka.Koleta;
                postojeca.Bruto += stavka.Bruto;
                postojeca.Neto += stavka.Neto;
                postojeca.Vrednost += stavka.Vrednost;
            }

            OgranicenjeLagerStavkeNaUkupno(postojeca);
        }


        private void DodajKolicinuNaIzabranu(Otprema.OtpremaStavke izabrana, Otprema.OtpremaStavke saLagera)
        {
            if (izabrana == null || saLagera == null)
                return;

            UpisiUkupneVrednostiAkoNedostaje(saLagera);

            decimal ukupnoKoleta = VratiUkupnoKoleta(saLagera);
            decimal ukupnoBruto = VratiUkupnoBruto(saLagera);
            decimal ukupnoNeto = VratiUkupnoNeto(saLagera);
            decimal ukupnoVrednost = VratiUkupnoVrednost(saLagera);

            UpisiUkupneVrednosti(izabrana, ukupnoKoleta, ukupnoBruto, ukupnoNeto, ukupnoVrednost);

            izabrana.Koleta += saLagera.Koleta;
            izabrana.Bruto += saLagera.Bruto;
            izabrana.Neto += saLagera.Neto;
            izabrana.Vrednost += saLagera.Vrednost;

            OgranicenjeIzabraneStavkeNaUkupno(izabrana);
            OsveziPreostaloNaLageruZaIzabranuStavku(izabrana);
        }


        private void OgranicenjeIzabraneStavkeNaUkupno(Otprema.OtpremaStavke stavka)
        {
            if (stavka == null)
                return;

            decimal ukupnoKoleta = VratiUkupnoKoleta(stavka);
            if (ukupnoKoleta > 0 && stavka.Koleta > ukupnoKoleta)
                stavka.Koleta = ukupnoKoleta;

            decimal ukupnoBruto = VratiUkupnoBruto(stavka);
            if (ukupnoBruto > 0 && stavka.Bruto > ukupnoBruto)
                stavka.Bruto = ukupnoBruto;

            decimal ukupnoNeto = VratiUkupnoNeto(stavka);
            if (ukupnoNeto > 0 && stavka.Neto > ukupnoNeto)
                stavka.Neto = ukupnoNeto;

            decimal ukupnoVrednost = VratiUkupnoVrednost(stavka);
            if (ukupnoVrednost > 0 && stavka.Vrednost > ukupnoVrednost)
                stavka.Vrednost = ukupnoVrednost;
        }

        private void OgranicenjeLagerStavkeNaUkupno(Otprema.OtpremaStavke stavka)
        {
            if (stavka == null)
                return;

            decimal ukupnoKoleta = VratiUkupnoKoleta(stavka);
            if (ukupnoKoleta > 0 && stavka.Koleta > ukupnoKoleta)
                stavka.Koleta = ukupnoKoleta;

            decimal ukupnoBruto = VratiUkupnoBruto(stavka);
            if (ukupnoBruto > 0 && stavka.Bruto > ukupnoBruto)
                stavka.Bruto = ukupnoBruto;

            decimal ukupnoNeto = VratiUkupnoNeto(stavka);
            if (ukupnoNeto > 0 && stavka.Neto > ukupnoNeto)
                stavka.Neto = ukupnoNeto;

            decimal ukupnoVrednost = VratiUkupnoVrednost(stavka);
            if (ukupnoVrednost > 0 && stavka.Vrednost > ukupnoVrednost)
                stavka.Vrednost = ukupnoVrednost;

            // Za lager grid prikazujemo trenutno preostalo i u kolonama Nerazduzeno...
            stavka.NerazudzenoKoleta = stavka.Koleta;
            stavka.NerazduzenaTezina = stavka.Bruto;
            stavka.OstatakValute = stavka.Vrednost;
        }

        private decimal VratiNuluAkoJeMaloNegativno(decimal vrednost)
        {
            if (vrednost < 0 && Math.Abs(vrednost) <= EPS)
                return 0;

            return vrednost;
        }
        private void VratiSeNaPrethodnuFormu()
        {
            var main = this.TopLevelControl as NewMain;

            if (main != null)
            {
                main.NavigateBack();
                return;
            }

            Close();
        }

        private void btnKreiraj_Click(object sender, EventArgs e)
        {
            if (izabraneStavke.Count == 0)
            {
                MessageBox.Show("Niste izabrali nijednu stavku.");
                return;
            }

            RenumerisiIzabrane();

            IzabraneStavke = izabraneStavke
                .Select(x => KopirajStavku(x))
                .ToList();

            if (_vratiStavke != null)
            {
                _vratiStavke(IzabraneStavke);
            }

            VratiSeNaPrethodnuFormu();
        }

        private List<Otprema.OtpremaStavke> VratiSelektovane(DataGridView dgv)
        {
            List<Otprema.OtpremaStavke> rezultat = new List<Otprema.OtpremaStavke>();

            foreach (DataGridViewRow row in dgv.SelectedRows)
            {
                Otprema.OtpremaStavke stavka = row.DataBoundItem as Otprema.OtpremaStavke;

                if (stavka != null && !rezultat.Contains(stavka))
                    rezultat.Add(stavka);
            }

            if (rezultat.Count == 0 && dgv.CurrentRow != null)
            {
                Otprema.OtpremaStavke stavka = dgv.CurrentRow.DataBoundItem as Otprema.OtpremaStavke;

                if (stavka != null)
                    rezultat.Add(stavka);
            }

            return rezultat;
        }

        private Otprema.OtpremaStavke VratiSelektovanuStavku(DataGridView dgv)
        {
            if (dgv == null)
                return null;

            if (dgv.CurrentRow != null)
            {
                Otprema.OtpremaStavke current =
                    dgv.CurrentRow.DataBoundItem as Otprema.OtpremaStavke;

                if (current != null)
                    return current;
            }

            if (dgv.SelectedRows.Count > 0)
            {
                Otprema.OtpremaStavke selected =
                    dgv.SelectedRows[0].DataBoundItem as Otprema.OtpremaStavke;

                if (selected != null)
                    return selected;
            }

            return null;
        }

        private Otprema.OtpremaStavke VratiStavkuZaIzmenuIzPanelaIliGrida()
        {
            int rbStavke;

            if (int.TryParse(txtRB.Text.Trim(), out rbStavke))
            {
                Otprema.OtpremaStavke stavkaPoRB =
                    izabraneStavke.FirstOrDefault(x => x.RB == rbStavke);

                if (stavkaPoRB != null)
                    return stavkaPoRB;
            }

            if (selektovanaStavka != null)
            {
                Otprema.OtpremaStavke stavkaPoSelektovanoj =
                    izabraneStavke.FirstOrDefault(x => x.RB == selektovanaStavka.RB);

                if (stavkaPoSelektovanoj != null)
                    return stavkaPoSelektovanoj;
            }

            Otprema.OtpremaStavke stavkaIzGrida = VratiSelektovanuStavku(dataGridView2);

            if (stavkaIzGrida != null)
                return izabraneStavke.FirstOrDefault(x => x.RB == stavkaIzGrida.RB);

            return null;
        }

        private void SelektujStavkuUDataGridView2(Otprema.OtpremaStavke stavka)
        {
            if (stavka == null)
                return;

            try
            {
                dataGridView2.ClearSelection();

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    Otprema.OtpremaStavke red =
                        row.DataBoundItem as Otprema.OtpremaStavke;

                    if (red != null && KljucStavke(red) == KljucStavke(stavka))
                    {
                        row.Selected = true;
                        dataGridView2.CurrentCell = row.Cells[0];
                        selektovanaStavka = red;
                        return;
                    }
                }
            }
            catch
            {
                // Ako grid trenutno nije spreman za selekciju, panel se svakako puni direktno.
            }
        }

        private bool PostojiUListi(IEnumerable<Otprema.OtpremaStavke> lista, Otprema.OtpremaStavke stavka)
        {
            return NadjiStavkuPoKljucu(lista, stavka) != null;
        }

        private Otprema.OtpremaStavke NadjiStavkuPoKljucu(IEnumerable<Otprema.OtpremaStavke> lista, Otprema.OtpremaStavke stavka)
        {
            if (lista == null || stavka == null)
                return null;

            string key = KljucStavke(stavka);

            foreach (Otprema.OtpremaStavke s in lista)
            {
                if (KljucStavke(s) == key)
                    return s;
            }

            return null;
        }

        private string KljucStavke(Otprema.OtpremaStavke s)
        {
            if (s == null)
                return "";

            if (s.IDSkladisneStavke > 0)
                return "ID:" + s.IDSkladisneStavke.ToString();

            return "ALT:" + s.NHM + "|" + (s.Naziv ?? "") + "|" + (s.Naimenovanje ?? "") + "|" + s.Paleta + "|" + s.VrstaPalete + "|" + (s.Valuta ?? "");
        }

        private decimal VratiUkupnoKoleta(Otprema.OtpremaStavke stavka)
        {
            if (stavka == null)
                return 0;

            string key = KljucStavke(stavka);

            decimal koleta;
            if (ukupnoKoletaPoKljucu.TryGetValue(key, out koleta))
                return koleta;

            koleta = stavka.UkupnoKoletaLager > 0 ? stavka.UkupnoKoletaLager : (stavka.NerazudzenoKoleta + stavka.Koleta);
            ukupnoKoletaPoKljucu[key] = koleta;
            return koleta;
        }


        private decimal VratiUkupnoBruto(Otprema.OtpremaStavke stavka)
        {
            if (stavka == null)
                return 0;

            string key = KljucStavke(stavka);

            decimal bruto;
            if (ukupnoBrutoPoKljucu.TryGetValue(key, out bruto))
                return bruto;

            bruto = stavka.UkupnoBrutoLager > 0 ? stavka.UkupnoBrutoLager : (stavka.NerazduzenaTezina + stavka.Bruto);
            ukupnoBrutoPoKljucu[key] = bruto;
            return bruto;
        }


        private decimal VratiUkupnoNeto(Otprema.OtpremaStavke stavka)
        {
            if (stavka == null)
                return 0;

            string key = KljucStavke(stavka);

            decimal neto;
            if (ukupnoNetoPoKljucu.TryGetValue(key, out neto))
                return neto;

            neto = stavka.UkupnoNetoLager > 0 ? stavka.UkupnoNetoLager : stavka.Neto;
            ukupnoNetoPoKljucu[key] = neto;
            return neto;
        }


        private decimal VratiUkupnoVrednost(Otprema.OtpremaStavke stavka)
        {
            if (stavka == null)
                return 0;

            string key = KljucStavke(stavka);

            decimal vrednost;
            if (ukupnoVrednostPoKljucu.TryGetValue(key, out vrednost))
                return vrednost;

            vrednost = stavka.UkupnoVrednostLager > 0 ? stavka.UkupnoVrednostLager : (stavka.OstatakValute + stavka.Vrednost);
            ukupnoVrednostPoKljucu[key] = vrednost;
            return vrednost;
        }


        private bool IstiKljuc(Otprema.OtpremaStavke a, Otprema.OtpremaStavke b)
        {
            return KljucStavke(a) == KljucStavke(b);
        }

        private void RenumerisiIzabrane()
        {
            int rb = 1;

            foreach (Otprema.OtpremaStavke s in izabraneStavke)
            {
                s.RB = rb;
                rb++;
            }

            dataGridView2.Refresh();
        }

        private void PopuniPanelStavke(Otprema.OtpremaStavke stavka)
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

        private Otprema.OtpremaStavke KopirajStavku(Otprema.OtpremaStavke s)
        {
            if (s == null)
                return null;

            return new Otprema.OtpremaStavke
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


        private void SrediGridove()
        {
            SrediGrid(dataGridView1);
            SrediGrid(dataGridView2);

            SakrijKolonuAkoPostoji(dataGridView1, "IDSkladisneStavke");
            SakrijKolonuAkoPostoji(dataGridView2, "IDSkladisneStavke");
            SakrijKolonuAkoPostoji(dataGridView1, "UkupnoKoletaLager");
            SakrijKolonuAkoPostoji(dataGridView2, "UkupnoKoletaLager");
            SakrijKolonuAkoPostoji(dataGridView1, "UkupnoBrutoLager");
            SakrijKolonuAkoPostoji(dataGridView2, "UkupnoBrutoLager");
            SakrijKolonuAkoPostoji(dataGridView1, "UkupnoNetoLager");
            SakrijKolonuAkoPostoji(dataGridView2, "UkupnoNetoLager");
            SakrijKolonuAkoPostoji(dataGridView1, "UkupnoVrednostLager");
            SakrijKolonuAkoPostoji(dataGridView2, "UkupnoVrednostLager");

            PodesiSirinuKolona();
        }

        private void SrediGrid(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgv.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgv.BackgroundColor = Color.White;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void SakrijKolonuAkoPostoji(DataGridView dgv, string nazivKolone)
        {
            if (dgv.Columns.Contains(nazivKolone))
                dgv.Columns[nazivKolone].Visible = false;
        }

        private string GetString(SqlDataReader dr, string column)
        {
            return dr[column] == DBNull.Value ? "" : dr[column].ToString();
        }

        private int GetInt(SqlDataReader dr, string column)
        {
            if (dr[column] == DBNull.Value)
                return 0;

            int value;

            if (int.TryParse(dr[column].ToString(), out value))
                return value;

            decimal decValue;

            if (decimal.TryParse(dr[column].ToString(), out decValue))
                return Convert.ToInt32(decValue);

            return 0;
        }

        private decimal GetDecimal(SqlDataReader dr, string column)
        {
            if (dr[column] == DBNull.Value)
                return 0;

            decimal value;

            if (decimal.TryParse(dr[column].ToString(), out value))
                return value;

            return 0;
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

        private DateTime GetDateTime(SqlDataReader dr, string column)
        {
            if (dr[column] == DBNull.Value)
                return DateTime.MinValue;

            DateTime value;

            if (DateTime.TryParse(dr[column].ToString(), out value))
                return value;

            return DateTime.MinValue;
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            DialogResult odgovor = MessageBox.Show(
                "Da li želite da odbacite izmene i vratite se nazad?",
                "Odustani",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (odgovor != DialogResult.Yes)
                return;

            Close();
        }
    }
}
