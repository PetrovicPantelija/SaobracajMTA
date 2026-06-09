using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main.Dokumenta
{
    public partial class PregledLagera : Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

        private string Ulaz;
        private bool ucitavanje = false;

        private BindingList<OtpremaStavke> list = new BindingList<OtpremaStavke>();
        private BindingSource bsStavke = new BindingSource();

        public PregledLagera(string ulaz)
        {
            InitializeComponent();
            Ulaz = ulaz;
        }

        private void PregledLagera_Load(object sender, EventArgs e)
        {
            PoveziGrid();
            FillCombo();
        }

        private void PoveziGrid()
        {
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dataGridView1.RowPrePaint -= dataGridView1_RowPrePaint;
            dataGridView1.RowPrePaint += dataGridView1_RowPrePaint;

            bsStavke.DataSource = list;
            dataGridView1.DataSource = bsStavke;

            SrediGrid();
        }

        private void FillCombo()
        {
            ucitavanje = true;

            cboPrvi.DataSource = null;
            cboFilter.DataSource = null;
            list.Clear();
            bsStavke.ResetBindings(false);

            if (Ulaz == "Magacinski broj")
            {
                lblPrva.Text = "Magacinski broj:";
                lblFilter.Text = "Vlasnik robe:";

                cboPrvi.Enabled = true;
                cboFilter.Enabled = false;

                cboPrvi.DropDownStyle = ComboBoxStyle.DropDownList;
                cboFilter.DropDownStyle = ComboBoxStyle.DropDownList;

                PopuniMagacinskeBrojeveZaPrviCombo();
            }
            else if (Ulaz == "Partner")
            {
                lblPrva.Text = "Partner:";
                lblFilter.Text = "Magacinski broj:";

                cboPrvi.Enabled = true;
                cboFilter.Enabled = true;

                cboPrvi.DropDownStyle = ComboBoxStyle.DropDownList;
                cboFilter.DropDownStyle = ComboBoxStyle.DropDownList;

                PopuniPartnereZaPrviCombo();
            }

            ucitavanje = false;

            if (cboPrvi.Items.Count > 0)
            {
                cboPrvi.SelectedIndex = 0;
                ObradiPromenuPrvogCombo();
            }
        }

        private void PopuniMagacinskeBrojeveZaPrviCombo()
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                string query = OsnovniQueryBezWhere() + @"
SELECT 
    MagacinskiBroj AS ID,
    CAST(MagacinskiBroj AS nvarchar(20)) + ' - ' + RTRIM(ISNULL(MagacinskiBrojNaziv, '')) AS Naziv
FROM Stock
WHERE
    (NerazduzenoKoleta > 0.0001 OR NerazduzenaTezina > 0.0001 OR NerazduzenoNeto > 0.0001 OR OstatakValute > 0.0001)
GROUP BY MagacinskiBroj, MagacinskiBrojNaziv
ORDER BY MagacinskiBroj DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);

                cboPrvi.DataSource = ds.Tables[0];
                cboPrvi.DisplayMember = "Naziv";
                cboPrvi.ValueMember = "ID";
            }
        }

        private void PopuniPartnereZaPrviCombo()
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                string query = OsnovniQueryBezWhere() + @"
SELECT 
    VlasnikRobe AS ID,
    RTRIM(ISNULL(VlasnikRobeNaziv, '')) AS Naziv
FROM Stock
WHERE
    (NerazduzenoKoleta > 0.0001 OR NerazduzenaTezina > 0.0001 OR NerazduzenoNeto > 0.0001 OR OstatakValute > 0.0001)
GROUP BY VlasnikRobe, VlasnikRobeNaziv
ORDER BY RTRIM(ISNULL(VlasnikRobeNaziv, ''))";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);

                cboPrvi.DataSource = ds.Tables[0];
                cboPrvi.DisplayMember = "Naziv";
                cboPrvi.ValueMember = "ID";
            }
        }

        private void cboPrvi_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ucitavanje)
                return;

            ObradiPromenuPrvogCombo();
        }

        private void ObradiPromenuPrvogCombo()
        {
            int prviID;

            if (!TryGetSelectedInt(cboPrvi, out prviID))
                return;

            if (Ulaz == "Magacinski broj")
            {
                PopuniVlasnikaZaMagacinskiBroj(prviID);
                UcitajLagerPoMagacinskomBroju(prviID);
            }
            else if (Ulaz == "Partner")
            {
                PopuniMagacinskeBrojeveZaPartnera(prviID);
                UcitajLagerPoVlasniku(prviID);
            }
        }

        private void PopuniVlasnikaZaMagacinskiBroj(int magacinskiBroj)
        {
            ucitavanje = true;

            using (SqlConnection conn = new SqlConnection(connection))
            {
                string query = OsnovniQueryBezWhere() + @"
SELECT 
    VlasnikRobe AS ID,
    RTRIM(ISNULL(VlasnikRobeNaziv, '')) AS Naziv
FROM Stock
WHERE
    MagacinskiBroj = @MagacinskiBroj
    AND (NerazduzenoKoleta > 0.0001 OR NerazduzenaTezina > 0.0001 OR NerazduzenoNeto > 0.0001 OR OstatakValute > 0.0001)
GROUP BY VlasnikRobe, VlasnikRobeNaziv
ORDER BY RTRIM(ISNULL(VlasnikRobeNaziv, ''))";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MagacinskiBroj", magacinskiBroj);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    cboFilter.DataSource = ds.Tables[0];
                    cboFilter.DisplayMember = "Naziv";
                    cboFilter.ValueMember = "ID";
                }
            }

            if (cboFilter.Items.Count > 0)
                cboFilter.SelectedIndex = 0;

            cboFilter.Enabled = false;

            ucitavanje = false;
        }

        private void PopuniMagacinskeBrojeveZaPartnera(int vlasnik)
        {
            ucitavanje = true;

            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Naziv", typeof(string));

            dt.Rows.Add(0, "Svi magacinski brojevi");

            using (SqlConnection conn = new SqlConnection(connection))
            {
                string query = OsnovniQueryBezWhere() + @"
SELECT 
    MagacinskiBroj AS ID,
    CAST(MagacinskiBroj AS nvarchar(20)) + ' - ' + RTRIM(ISNULL(MagacinskiBrojNaziv, '')) AS Naziv
FROM Stock
WHERE
    VlasnikRobe = @Vlasnik
    AND (NerazduzenoKoleta > 0.0001 OR NerazduzenaTezina > 0.0001 OR NerazduzenoNeto > 0.0001 OR OstatakValute > 0.0001)
GROUP BY MagacinskiBroj, MagacinskiBrojNaziv
ORDER BY MagacinskiBroj DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Vlasnik", vlasnik);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        dt.Rows.Add(
                            Convert.ToInt32(row["ID"]),
                            row["Naziv"].ToString()
                        );
                    }
                }
            }

            cboFilter.DataSource = dt;
            cboFilter.DisplayMember = "Naziv";
            cboFilter.ValueMember = "ID";
            cboFilter.Enabled = true;

            if (cboFilter.Items.Count > 0)
                cboFilter.SelectedIndex = 0;

            ucitavanje = false;
        }

        private void cboFilter_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ucitavanje)
                return;

            if (Ulaz != "Partner")
                return;

            int vlasnik;
            int magacinskiBroj;

            if (!TryGetSelectedInt(cboPrvi, out vlasnik))
                return;

            if (!TryGetSelectedInt(cboFilter, out magacinskiBroj))
                return;

            if (magacinskiBroj == 0)
            {
                UcitajLagerPoVlasniku(vlasnik);
            }
            else
            {
                UcitajPoVlasnikuIMagacinskomBroju(vlasnik, magacinskiBroj);
            }
        }

        private void UcitajLagerPoMagacinskomBroju(int magacinskiBroj)
        {
            list.Clear();

            string query = OsnovniQuery() + @"
    AND MagacinskiBroj = @MagacinskiBroj
" + OrderByQuery();

            using (SqlConnection conn = new SqlConnection(connection))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MagacinskiBroj", magacinskiBroj);

                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(MapirajStavku(dr));
                    }
                }
            }

            RenumerisiRB();
            bsStavke.ResetBindings(false);
            SrediGrid();
        }

        private void UcitajLagerPoVlasniku(int vlasnik)
        {
            list.Clear();

            string query = OsnovniQuery() + @"
    AND VlasnikRobe = @Vlasnik
" + OrderByQuery();

            using (SqlConnection conn = new SqlConnection(connection))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Vlasnik", vlasnik);

                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(MapirajStavku(dr));
                    }
                }
            }

            RenumerisiRB();
            bsStavke.ResetBindings(false);
            SrediGrid();
        }

        private void UcitajPoVlasnikuIMagacinskomBroju(int vlasnik, int magacinskiBroj)
        {
            list.Clear();

            string query = OsnovniQuery() + @"
    AND VlasnikRobe = @Vlasnik
    AND MagacinskiBroj = @MagacinskiBroj
" + OrderByQuery();

            using (SqlConnection conn = new SqlConnection(connection))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Vlasnik", vlasnik);
                cmd.Parameters.AddWithValue("@MagacinskiBroj", magacinskiBroj);

                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(MapirajStavku(dr));
                    }
                }
            }

            RenumerisiRB();
            bsStavke.ResetBindings(false);
            SrediGrid();
        }

        // Ovaj query prikazuje PREOSTALO FIZIČKO stanje lagera.
        // Rezervacije iz RNCarinskoSkladisteOtpremaStavkePom se NE oduzimaju od preostalog stanja,
        // nego se prikazuju u posebnim kolonama i obeležavaju bojom.
        private string OsnovniQuery()
        {
            return OsnovniQueryBezWhere() + @"
SELECT *
FROM Stock
WHERE
    (NerazduzenoKoleta > 0.0001 OR NerazduzenaTezina > 0.0001 OR NerazduzenoNeto > 0.0001 OR OstatakValute > 0.0001)
";
        }

        private string OsnovniQueryBezWhere()
        {
            return @"
WITH Primljeno AS
(
    SELECT 
        s.ID AS IDSkladisneStavke,
        s.IDNadredjena AS Prijemnica,
        s.RB AS PrijemRB,
        rn.MagacinskiBroj,
        RTRIM(ISNULL(mb.Naziv, '')) AS MagacinskiBrojNaziv,
        rn.VlasnikRobe,
        RTRIM(ISNULL(pa.PaNaziv, '')) AS VlasnikRobeNaziv,
        s.Naimenovanje,
        s.NHM,
        s.Naziv,
        s.JM,
        ISNULL(SUM(p.PrPrimKol), 0) - ISNULL(SUM(p.PrIzdKol), 0) AS KoletaPoPrometu,
        s.Koleta AS PrimljenoKoleta,
        s.Bruto AS PrimljenoBruto,
        ISNULL(s.Neto, 0) AS PrimljenoNeto,
        s.Vrednost AS PrimljenaVrednost,
        s.Valuta,
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
    INNER JOIN Promet p 
        ON s.ID = p.IDSaDokumenta
       AND p.MagacinskiBroj = rn.MagacinskiBroj
    INNER JOIN TipPalete tp 
        ON s.VrstaPaleta = tp.ID
    LEFT JOIN Skladista skl 
        ON s.Pozicija = skl.Naziv
    LEFT JOIN MagacinskiBrojCarinski mb
        ON rn.MagacinskiBroj = mb.ID
    LEFT JOIN Partnerji pa
        ON rn.VlasnikRobe = pa.PaSifra
    WHERE pr.Status = 'ZA'
    GROUP BY 
        s.ID,
        s.IDNadredjena,
        s.RB,
        rn.MagacinskiBroj,
        mb.Naziv,
        rn.VlasnikRobe,
        pa.PaNaziv,
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
Rezervisano AS
(
    SELECT
        pom.Prijemnica,
        pom.NHM,
        pom.Naziv,
        pom.Naimenovanje,
        pom.JM,
        pom.Valuta,
        pom.Pozicija,
        pom.VrstaPaleta,
        SUM(ISNULL(pom.Koleta, 0)) AS RezervisanoKoleta,
        SUM(ISNULL(pom.Bruto, 0)) AS RezervisanoBruto,
        SUM(ISNULL(pom.Neto, 0)) AS RezervisanoNeto,
        SUM(ISNULL(pom.Vrednost, 0)) AS RezervisanaVrednost,
        RezervisanoRN =
            STUFF
            (
                (
                    SELECT DISTINCT ', ' + CAST(pom2.IDNadredjena AS varchar(20))
                    FROM RNCarinskoSkladisteOtpremaStavkePom pom2
                    WHERE ISNULL(pom2.Prijemnica, 0) = ISNULL(pom.Prijemnica, 0)
                      AND ISNULL(pom2.NHM, 0) = ISNULL(pom.NHM, 0)
                      AND RTRIM(ISNULL(pom2.Naziv, '')) = RTRIM(ISNULL(pom.Naziv, ''))
                      AND RTRIM(ISNULL(pom2.Naimenovanje, '')) = RTRIM(ISNULL(pom.Naimenovanje, ''))
                      AND RTRIM(ISNULL(pom2.JM, '')) = RTRIM(ISNULL(pom.JM, ''))
                      AND RTRIM(ISNULL(pom2.Valuta, '')) = RTRIM(ISNULL(pom.Valuta, ''))
                      AND RTRIM(ISNULL(pom2.Pozicija, '')) = RTRIM(ISNULL(pom.Pozicija, ''))
                      AND ISNULL(pom2.VrstaPaleta, 0) = ISNULL(pom.VrstaPaleta, 0)
                    FOR XML PATH(''), TYPE
                ).value('.', 'nvarchar(max)'),
                1,
                2,
                ''
            )
    FROM RNCarinskoSkladisteOtpremaStavkePom pom
    GROUP BY
        pom.Prijemnica,
        pom.NHM,
        pom.Naziv,
        pom.Naimenovanje,
        pom.JM,
        pom.Valuta,
        pom.Pozicija,
        pom.VrstaPaleta
),
Stock AS
(
    SELECT
        p.IDSkladisneStavke,
        p.Prijemnica,
        p.PrijemRB,
        p.MagacinskiBroj,
        p.MagacinskiBrojNaziv,
        p.VlasnikRobe,
        p.VlasnikRobeNaziv,
        p.Naimenovanje,
        p.NHM,
        p.Naziv,
        p.JM,

        NerazduzenoKoleta =
            CASE WHEN p.KoletaPoPrometu < 0 THEN 0 ELSE p.KoletaPoPrometu END,

        TezinaKomad = CAST(p.PrimljenoBruto / NULLIF(p.PrimljenoKoleta, 0) AS decimal(18, 2)),

        NerazduzenaTezina =
            CAST(
                CASE WHEN p.PrimljenoBruto - ISNULL(z.ZavrsenoBruto, 0) < 0 THEN 0
                     ELSE p.PrimljenoBruto - ISNULL(z.ZavrsenoBruto, 0) END
                AS decimal(18, 2)
            ),

        NerazduzenoNeto =
            CAST(
                CASE WHEN p.PrimljenoNeto - ISNULL(z.ZavrsenoNeto, 0) < 0 THEN 0
                     ELSE p.PrimljenoNeto - ISNULL(z.ZavrsenoNeto, 0) END
                AS decimal(18, 2)
            ),

        JedinicnaCena = CAST(p.PrimljenaVrednost / NULLIF(p.PrimljenoKoleta, 0) AS decimal(18, 2)),

        OstatakValute =
            CAST(
                CASE WHEN p.PrimljenaVrednost - ISNULL(z.ZavrsenaVrednost, 0) < 0 THEN 0
                     ELSE p.PrimljenaVrednost - ISNULL(z.ZavrsenaVrednost, 0) END
                AS decimal(18, 2)
            ),

        RezervisanoKoleta = ISNULL(r.RezervisanoKoleta, 0),
        RezervisanoBruto = ISNULL(r.RezervisanoBruto, 0),
        RezervisanoNeto = ISNULL(r.RezervisanoNeto, 0),
        RezervisanaVrednost = ISNULL(r.RezervisanaVrednost, 0),

        SlobodnoKoleta =
            CAST(
                CASE WHEN p.KoletaPoPrometu - ISNULL(r.RezervisanoKoleta, 0) < 0 THEN 0
                     ELSE p.KoletaPoPrometu - ISNULL(r.RezervisanoKoleta, 0) END
                AS decimal(18, 2)
            ),

        SlobodnaTezina =
            CAST(
                CASE WHEN (p.PrimljenoBruto - ISNULL(z.ZavrsenoBruto, 0)) - ISNULL(r.RezervisanoBruto, 0) < 0 THEN 0
                     ELSE (p.PrimljenoBruto - ISNULL(z.ZavrsenoBruto, 0)) - ISNULL(r.RezervisanoBruto, 0) END
                AS decimal(18, 2)
            ),

        SlobodanNeto =
            CAST(
                CASE WHEN (p.PrimljenoNeto - ISNULL(z.ZavrsenoNeto, 0)) - ISNULL(r.RezervisanoNeto, 0) < 0 THEN 0
                     ELSE (p.PrimljenoNeto - ISNULL(z.ZavrsenoNeto, 0)) - ISNULL(r.RezervisanoNeto, 0) END
                AS decimal(18, 2)
            ),

        SlobodnaVrednost =
            CAST(
                CASE WHEN (p.PrimljenaVrednost - ISNULL(z.ZavrsenaVrednost, 0)) - ISNULL(r.RezervisanaVrednost, 0) < 0 THEN 0
                     ELSE (p.PrimljenaVrednost - ISNULL(z.ZavrsenaVrednost, 0)) - ISNULL(r.RezervisanaVrednost, 0) END
                AS decimal(18, 2)
            ),

        Rezervisano =
            CASE WHEN ISNULL(r.RezervisanoKoleta, 0) > 0
                   OR ISNULL(r.RezervisanoBruto, 0) > 0
                   OR ISNULL(r.RezervisanoNeto, 0) > 0
                   OR ISNULL(r.RezervisanaVrednost, 0) > 0
                 THEN CAST(1 AS bit)
                 ELSE CAST(0 AS bit)
            END,

        RezervisanoRN = ISNULL(r.RezervisanoRN, ''),

        p.Valuta,
        '' AS DokumentRazduzenja,
        GETDATE() AS DatumRazduzenja,
        '' AS Destinacija,
        GETDATE() AS DatumOtpreme,
        p.Pozicija,
        p.Paleta,
        p.VrstaPaleta AS VrstaPalete,
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
    LEFT JOIN Rezervisano r
        ON r.Prijemnica = p.Prijemnica
       AND ISNULL(r.NHM, 0) = ISNULL(p.NHM, 0)
       AND RTRIM(ISNULL(r.Naziv, '')) = RTRIM(ISNULL(p.Naziv, ''))
       AND RTRIM(ISNULL(r.Naimenovanje, '')) = RTRIM(ISNULL(p.Naimenovanje, ''))
       AND RTRIM(ISNULL(r.JM, '')) = RTRIM(ISNULL(p.JM, ''))
       AND RTRIM(ISNULL(r.Valuta, '')) = RTRIM(ISNULL(p.Valuta, ''))
       AND RTRIM(ISNULL(r.Pozicija, '')) = RTRIM(ISNULL(p.Pozicija, ''))
       AND ISNULL(r.VrstaPaleta, 0) = ISNULL(p.VrstaPaleta, 0)
)
";
        }

        private string OrderByQuery()
        {
            return @"
ORDER BY 
    MagacinskiBroj DESC,
    Prijemnica DESC,
    PrijemRB ASC,
    IDSkladisneStavke ASC";
        }

        private OtpremaStavke MapirajStavku(SqlDataReader dr)
        {
            return new OtpremaStavke
            {
                IDSkladisneStavke = GetInt(dr, "IDSkladisneStavke"),
                Prijemnica = GetInt(dr, "Prijemnica"),
                PrijemRB = GetInt(dr, "PrijemRB"),

                MagacinskiBroj = GetInt(dr, "MagacinskiBroj"),
                MagacinskiBrojNaziv = GetString(dr, "MagacinskiBrojNaziv"),

                VlasnikRobe = GetInt(dr, "VlasnikRobe"),
                VlasnikRobeNaziv = GetString(dr, "VlasnikRobeNaziv"),

                RB = 0,
                Naimenovanje = GetString(dr, "Naimenovanje"),
                NHM = GetInt(dr, "NHM"),
                Naziv = GetString(dr, "Naziv"),
                JM = GetString(dr, "JM"),

                NerazudzenoKoleta = GetDecimal(dr, "NerazduzenoKoleta"),
                TezinaKomad = GetDecimal(dr, "TezinaKomad"),
                NerazduzenaTezina = GetDecimal(dr, "NerazduzenaTezina"),
                NerazduzenoNeto = GetDecimal(dr, "NerazduzenoNeto"),
                JedinicnaCena = GetDecimal(dr, "JedinicnaCena"),
                OstatakValute = GetDecimal(dr, "OstatakValute"),

                RezervisanoKoleta = GetDecimal(dr, "RezervisanoKoleta"),
                RezervisanoBruto = GetDecimal(dr, "RezervisanoBruto"),
                RezervisanoNeto = GetDecimal(dr, "RezervisanoNeto"),
                RezervisanaVrednost = GetDecimal(dr, "RezervisanaVrednost"),
                SlobodnoKoleta = GetDecimal(dr, "SlobodnoKoleta"),
                SlobodnaTezina = GetDecimal(dr, "SlobodnaTezina"),
                SlobodanNeto = GetDecimal(dr, "SlobodanNeto"),
                SlobodnaVrednost = GetDecimal(dr, "SlobodnaVrednost"),
                Rezervisano = GetBool(dr, "Rezervisano"),
                RezervisanoRN = GetString(dr, "RezervisanoRN"),

                Valuta = GetString(dr, "Valuta"),
                DokumentRazduzenja = GetString(dr, "DokumentRazduzenja"),
                DatumRazduzenja = GetDateTime(dr, "DatumRazduzenja"),
                Destinacija = GetString(dr, "Destinacija"),
                DatumOtpreme = GetDateTime(dr, "DatumOtpreme"),
                Pozicija = GetString(dr, "Pozicija"),
                Paleta = GetInt(dr, "Paleta"),
                VrstaPalete = GetInt(dr, "VrstaPalete"),
                DimenzijePalete = GetString(dr, "DimenzijePalete"),
                SifraArtikla = GetInt(dr, "SifraArtikla"),
                PDV = GetDecimal(dr, "PDV"),
                Carina = GetDecimal(dr, "Carina"),
                TabelaPozicija = GetString(dr, "TabelaPozicija")
            };
        }

        private void RenumerisiRB()
        {
            int rb = 1;

            foreach (OtpremaStavke stavka in list)
            {
                stavka.RB = rb;
                rb++;
            }
        }

        private bool TryGetSelectedInt(ComboBox combo, out int value)
        {
            value = 0;

            if (combo == null)
                return false;

            if (combo.SelectedValue == null)
                return false;

            if (combo.SelectedValue is DataRowView)
                return false;

            return int.TryParse(combo.SelectedValue.ToString(), out value);
        }

        private void SrediGrid()
        {
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

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            PodesiNaziveKolona();
        }

        private void PodesiNaziveKolona()
        {
            if (dataGridView1.Columns.Count == 0)
                return;

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.Visible = false;
            }

            int index = 0;

            PodesiVidljivuKolonu("Prijemnica", "Prijemnica", ref index);
            PodesiVidljivuKolonu("MagacinskiBroj", "Magacinski broj", ref index);
            PodesiVidljivuKolonu("VlasnikRobe", "Vlasnik robe", ref index);
            PodesiVidljivuKolonu("RB", "RB", ref index);
            PodesiVidljivuKolonu("NHM", "NHM", ref index);
            PodesiVidljivuKolonu("Naziv", "Naziv", ref index);
            PodesiVidljivuKolonu("JM", "JM", ref index);

            // Prikaz korisniku: slobodno stanje se prikazuje kao nerazduženo stanje.
            PodesiVidljivuKolonu("SlobodnoKoleta", "Nerazduženo koleta", ref index);
            PodesiVidljivuKolonu("SlobodnaTezina", "Nerazdužena težina", ref index);
            PodesiVidljivuKolonu("SlobodnaVrednost", "Nerazdužena vrednost", ref index);

            PodesiVidljivuKolonu("Rezervisano", "Rezervisano", ref index);
            PodesiVidljivuKolonu("RezervisanoRN", "RN rezervacije", ref index);
            PodesiVidljivuKolonu("RezervisanoKoleta", "Rez. koleta", ref index);
            PodesiVidljivuKolonu("RezervisanoBruto", "Rez. težina", ref index);
            PodesiVidljivuKolonu("RezervisanaVrednost", "Rez. vrednost", ref index);

            PodesiVidljivuKolonu("Pozicija", "Pozicija", ref index);
            PodesiVidljivuKolonu("Paleta", "Paleta", ref index);
            PodesiVidljivuKolonu("Naimenovanje", "Naimenovanje", ref index);
            PodesiVidljivuKolonu("PDV", "PDV", ref index);
            PodesiVidljivuKolonu("Carina", "Carina", ref index);
        }

        private void PodesiVidljivuKolonu(string columnName, string headerText, ref int displayIndex)
        {
            if (!dataGridView1.Columns.Contains(columnName))
                return;

            DataGridViewColumn col = dataGridView1.Columns[columnName];
            col.Visible = true;
            col.HeaderText = headerText;
            col.DisplayIndex = displayIndex;
            displayIndex++;
        }

        private void SetHeader(string columnName, string headerText)
        {
            if (dataGridView1.Columns.Contains(columnName))
                dataGridView1.Columns[columnName].HeaderText = headerText;
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count)
                return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            OtpremaStavke stavka = row.DataBoundItem as OtpremaStavke;

            if (stavka == null)
                return;

            if (stavka.Rezervisano)
            {
                row.DefaultCellStyle.BackColor = Color.Khaki;
                row.DefaultCellStyle.ForeColor = Color.Black;
                row.DefaultCellStyle.SelectionBackColor = Color.DarkGoldenrod;
                row.DefaultCellStyle.SelectionForeColor = Color.White;
            }
            else
            {
                row.DefaultCellStyle.BackColor = e.RowIndex % 2 == 0
                    ? Color.White
                    : Color.FromArgb(238, 239, 249);
                row.DefaultCellStyle.ForeColor = Color.Black;
                row.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                row.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            }
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

        private DateTime GetDateTime(SqlDataReader dr, string column)
        {
            if (dr[column] == DBNull.Value)
                return DateTime.MinValue;

            DateTime value;

            if (DateTime.TryParse(dr[column].ToString(), out value))
                return value;

            return DateTime.MinValue;
        }

        private bool GetBool(SqlDataReader dr, string column)
        {
            if (dr[column] == DBNull.Value)
                return false;

            bool value;
            if (bool.TryParse(dr[column].ToString(), out value))
                return value;

            int intValue;
            if (int.TryParse(dr[column].ToString(), out intValue))
                return intValue != 0;

            return false;
        }

        public class OtpremaStavke
        {
            public int IDSkladisneStavke { get; set; }
            public int Prijemnica { get; set; }
            public int PrijemRB { get; set; }

            public int RB { get; set; }

            public int MagacinskiBroj { get; set; }
            public string MagacinskiBrojNaziv { get; set; }

            public int VlasnikRobe { get; set; }
            public string VlasnikRobeNaziv { get; set; }

            public string Naimenovanje { get; set; }
            public int NHM { get; set; }
            public string Naziv { get; set; }
            public string JM { get; set; }

            public decimal NerazudzenoKoleta { get; set; }
            public decimal TezinaKomad { get; set; }
            public decimal NerazduzenaTezina { get; set; }
            public decimal NerazduzenoNeto { get; set; }

            public decimal JedinicnaCena { get; set; }
            public decimal OstatakValute { get; set; }
            public string Valuta { get; set; }

            public decimal RezervisanoKoleta { get; set; }
            public decimal RezervisanoBruto { get; set; }
            public decimal RezervisanoNeto { get; set; }
            public decimal RezervisanaVrednost { get; set; }

            public decimal SlobodnoKoleta { get; set; }
            public decimal SlobodnaTezina { get; set; }
            public decimal SlobodanNeto { get; set; }
            public decimal SlobodnaVrednost { get; set; }

            public bool Rezervisano { get; set; }
            public string RezervisanoRN { get; set; }

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
        }
    }
}
