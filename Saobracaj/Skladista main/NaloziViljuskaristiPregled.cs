using Syncfusion.Grouping;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace Saobracaj.Skladista_main
{
    public partial class NaloziViljuskaristiPregled : Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        private SynchronizationContext _uiContext;
        private bool _ucitavanjeUToku = false;

        public NaloziViljuskaristiPregled()
        {
            InitializeComponent();
        }

        private void NaloziViljuskaristiPregled_Load(object sender, EventArgs e)
        {
            _uiContext = SynchronizationContext.Current;
            UcitajGridRukovalac();
        }

        private void UcitajGridRukovalac()
        {
            if (_uiContext != null && SynchronizationContext.Current != _uiContext)
            {
                IzvrsiNaUiThreadu(delegate
                {
                    UcitajGridRukovalac();
                });

                return;
            }

            if (_ucitavanjeUToku)
                return;

            _ucitavanjeUToku = true;

            try
            {
                string masterSql = @"
;WITH RN_BASE AS
(
    SELECT
        rn.ID,
        rn.Prijemnica,
        rn.Postupak AS PostupakID,
        csp.Naziv AS Postupak,
        Rukovalac = RTRIM(ISNULL(d.DeIme, '')) + ' ' + RTRIM(ISNULL(d.DePriimek, '')),
        rn.Koleta,
        rn.Paleta,
        rn.Bruto,
        rn.Vozilo,
        rn.Napomena,
        rn.Uradjen
    FROM RNCarinskoSkladisteRukovalac rn
    INNER JOIN CarinskoSkladistePostupak csp 
        ON rn.Postupak = csp.ID
    LEFT JOIN Delavci d 
        ON rn.Rukovalac = d.DeSifra
    WHERE rn.Status = 'OD'
),
RN_SUM AS
(
    SELECT
        ID =
            STUFF
            (
                (
                    SELECT ', ' + CAST(x.ID AS varchar(20))
                    FROM RN_BASE x
                    WHERE x.Prijemnica = b.Prijemnica
                      AND x.PostupakID = b.PostupakID
                    ORDER BY x.ID
                    FOR XML PATH(''), TYPE
                ).value('.', 'nvarchar(max)'),
                1,
                2,
                ''
            ),

        b.Prijemnica AS BrojDokumenta,
        b.PostupakID,
        b.Postupak,

        Rukovalac = MAX(b.Rukovalac),

        Koleta = SUM(ISNULL(b.Koleta, 0)),
        Paleta = SUM(ISNULL(b.Paleta, 0)),
        Bruto = SUM(ISNULL(b.Bruto, 0)),

        Vozilo = MAX(b.Vozilo),
        Napomena = MAX(NULLIF(LTRIM(RTRIM(ISNULL(b.Napomena, ''))), '')),

        Uradjen = MIN(CASE WHEN ISNULL(b.Uradjen, 0) = 1 THEN 1 ELSE 0 END)
    FROM RN_BASE b
    GROUP BY
        b.Prijemnica,
        b.PostupakID,
        b.Postupak
),
USLUGE_STATUS AS
(
    SELECT
        b.Prijemnica AS BrojDokumenta,
        b.PostupakID,

        ImaUradjenuUslugu =
            MAX(CASE WHEN ISNULL(du.Uradjen, 0) = 1 THEN 1 ELSE 0 END)
    FROM RN_BASE b

    LEFT JOIN RNCarinskoSkladistePrijemnica p 
        ON b.Postupak = 'Prijem'
       AND b.Prijemnica = p.ID

    LEFT JOIN RNCarinskoSkladisteOtpremnica o 
        ON b.Postupak = 'Otprema'
       AND b.Prijemnica = o.ID

    LEFT JOIN RNCarinskoSkladisteDodatneUsluge du 
        ON du.RN = CASE 
                       WHEN b.Postupak = 'Prijem' THEN p.RN
                       WHEN b.Postupak = 'Otprema' THEN o.RN
                   END

    GROUP BY
        b.Prijemnica,
        b.PostupakID
)
SELECT
    s.ID,
    s.BrojDokumenta,
    s.PostupakID,
    s.Postupak,
    s.Rukovalac,
    s.Koleta,
    s.Paleta,
    s.Bruto,
    s.Vozilo,
    s.Napomena,
    s.Uradjen,
    ISNULL(u.ImaUradjenuUslugu, 0) AS ImaUradjenuUslugu,

    BojaReda =
    CASE
        WHEN s.Uradjen = 1 AND ISNULL(u.ImaUradjenuUslugu, 0) = 1 THEN 'GREEN'
        WHEN s.Uradjen = 1 AND ISNULL(u.ImaUradjenuUslugu, 0) = 0 THEN 'YELLOW'
        WHEN s.Uradjen = 0 AND ISNULL(u.ImaUradjenuUslugu, 0) = 1 THEN 'YELLOW'
        ELSE 'RED'
    END
FROM RN_SUM s
LEFT JOIN USLUGE_STATUS u 
    ON s.BrojDokumenta = u.BrojDokumenta
   AND s.PostupakID = u.PostupakID
ORDER BY
    s.BrojDokumenta DESC,
    s.PostupakID;
";

                string detailSql = @"
;WITH DETAIL_RAW AS
(
    SELECT
        p.ID AS BrojDokumenta,
        csp.ID AS PostupakID,
        csp.Naziv AS Postupak,
        r.ID AS StavkaID,
        r.RB,
        r.Naziv AS NazivArtikla,
        r.JM,
        r.Koleta,
        r.Bruto,
        r.Neto,
        r.Pozicija,
        r.Paleta,
        tp.Naziv AS VrstaPalete,
        vm.Naziv AS NazivUsluge,
        du.Uradjen AS OdradjenaUsluga
    FROM RNCarinskoSkladistePrijemnica p
    INNER JOIN CarinskoSkladistePostupak csp 
        ON csp.Naziv = 'Prijem'
    INNER JOIN RNCarinskoPrijemnicaStavke r 
        ON r.IDNadredjena = p.ID
    LEFT JOIN TipPalete tp 
        ON r.VrstaPaleta = tp.ID
    LEFT JOIN RNCarinskoSkladisteDodatneUsluge du 
        ON p.RN = du.RN
    LEFT JOIN VrstaManipulacije vm 
        ON du.Usluga = vm.ID
    WHERE EXISTS
    (
        SELECT 1
        FROM RNCarinskoSkladisteRukovalac rn
        WHERE rn.Prijemnica = p.ID
          AND rn.Postupak = csp.ID
          AND rn.Status = 'OD'
    )

    UNION ALL

    SELECT
        o.ID AS BrojDokumenta,
        csp.ID AS PostupakID,
        csp.Naziv AS Postupak,
        r.ID AS StavkaID,
        r.RB,
        r.Naziv AS NazivArtikla,
        r.JM,
        r.Koleta,
        r.Bruto,
        r.Neto,
        r.Pozicija,
        r.Paleta,
        tp.Naziv AS VrstaPalete,
        vm.Naziv AS NazivUsluge,
        du.Uradjen AS OdradjenaUsluga
    FROM RNCarinskoSkladisteOtpremnica o
    INNER JOIN CarinskoSkladistePostupak csp 
        ON csp.Naziv = 'Otprema'
    INNER JOIN RNCarinskoSkladisteOtpremnicaStavke r 
        ON r.IDNadredjena = o.ID
    LEFT JOIN TipPalete tp 
        ON r.VrstaPaleta = tp.ID
    LEFT JOIN RNCarinskoSkladisteDodatneUsluge du 
        ON o.RN = du.RN
    LEFT JOIN VrstaManipulacije vm 
        ON du.Usluga = vm.ID
    WHERE EXISTS
    (
        SELECT 1
        FROM RNCarinskoSkladisteRukovalac rn
        WHERE rn.Prijemnica = o.ID
          AND rn.Postupak = csp.ID
          AND rn.Status = 'OD'
    )
),
DETAIL_NUM AS
(
    SELECT
        *,
        RedUsluge = ROW_NUMBER() OVER
        (
            PARTITION BY PostupakID, BrojDokumenta, StavkaID
            ORDER BY NazivUsluge
        )
    FROM DETAIL_RAW
)
SELECT
    BrojDokumenta,
    PostupakID,
    StavkaID,

    RB =
        CASE WHEN RedUsluge = 1 THEN RB ELSE NULL END,

    NazivArtikla =
        CASE WHEN RedUsluge = 1 THEN NazivArtikla ELSE '' END,

    NazivUsluge,

    JM =
        CASE WHEN RedUsluge = 1 THEN JM ELSE '' END,

    Koleta =
        CASE WHEN RedUsluge = 1 THEN Koleta ELSE NULL END,

    Bruto =
        CASE WHEN RedUsluge = 1 THEN Bruto ELSE NULL END,

    Neto =
        CASE WHEN RedUsluge = 1 THEN Neto ELSE NULL END,

    Pozicija =
        CASE WHEN RedUsluge = 1 THEN Pozicija ELSE '' END,

    Paleta =
        CASE WHEN RedUsluge = 1 THEN Paleta ELSE NULL END,

    VrstaPalete =
        CASE WHEN RedUsluge = 1 THEN VrstaPalete ELSE '' END,

    OdradjenaUsluga
FROM DETAIL_NUM
ORDER BY
    BrojDokumenta DESC,
    PostupakID,
    StavkaID ASC,
    RedUsluge ASC;
";

                DataSet ds = new DataSet();

                using (SqlConnection conn = new SqlConnection(connection))
                {
                    using (SqlDataAdapter daMaster = new SqlDataAdapter(masterSql, conn))
                    {
                        daMaster.Fill(ds, "Dokumenti");
                    }

                    using (SqlDataAdapter daDetail = new SqlDataAdapter(detailSql, conn))
                    {
                        daDetail.Fill(ds, "Detalji");
                    }
                }

                ds.Relations.Add(
    "Dokument_Detalji",
    new DataColumn[]
    {
        ds.Tables["Dokumenti"].Columns["PostupakID"],
        ds.Tables["Dokumenti"].Columns["BrojDokumenta"]
    },
    new DataColumn[]
    {
        ds.Tables["Detalji"].Columns["PostupakID"],
        ds.Tables["Detalji"].Columns["BrojDokumenta"]
    },
    false
);

                gridGroupingControl1.DataSource = null;
                gridGroupingControl1.DataSource = ds;
                gridGroupingControl1.DataMember = "Dokumenti";

                PodesiGridRukovalac();
            }
            finally
            {
                _ucitavanjeUToku = false;
            }
        }

        private void IzvrsiNaUiThreadu(Action akcija)
        {
            if (_uiContext != null && SynchronizationContext.Current != _uiContext)
            {
                _uiContext.Post(delegate
                {
                    if (!this.IsDisposed && !this.Disposing)
                        akcija();
                }, null);

                return;
            }

            if (!this.IsDisposed && !this.Disposing)
                akcija();
        }

        private void PodesiGridRukovalac()
        {
            gridGroupingControl1.TopLevelGroupOptions.ShowCaption = false;
            gridGroupingControl1.ChildGroupOptions.ShowCaption = false;

            gridGroupingControl1.TableDescriptor.AllowEdit = false;
            gridGroupingControl1.TableOptions.AllowSelection = GridSelectionFlags.Row;
            gridGroupingControl1.TableOptions.ListBoxSelectionMode = SelectionMode.One;

            GridTableDescriptor master = gridGroupingControl1.TableDescriptor;

            master.VisibleColumns.Clear();
            master.VisibleColumns.Add("ID");
            master.VisibleColumns.Add("BrojDokumenta");
            master.VisibleColumns.Add("Postupak");
            master.VisibleColumns.Add("Rukovalac");
            master.VisibleColumns.Add("Koleta");
            master.VisibleColumns.Add("Paleta");
            master.VisibleColumns.Add("Bruto");
            master.VisibleColumns.Add("Vozilo");
            master.VisibleColumns.Add("Napomena");
            master.VisibleColumns.Add("Uradjen");

            master.Columns["ID"].HeaderText = "RN rukovaocu";
            master.Columns["BrojDokumenta"].HeaderText = "Broj dokumenta";
            master.Columns["Postupak"].HeaderText = "Postupak";
            master.Columns["Rukovalac"].HeaderText = "Rukovalac";
            master.Columns["Koleta"].HeaderText = "Koleta";
            master.Columns["Paleta"].HeaderText = "Paleta";
            master.Columns["Bruto"].HeaderText = "Bruto";
            master.Columns["Vozilo"].HeaderText = "Vozilo";
            master.Columns["Napomena"].HeaderText = "Napomena";
            master.Columns["Uradjen"].HeaderText = "Urađen";

            PodesiBojeMasterRedova(master);


            GridRelationDescriptor rel = NadjiRelaciju(master, "Dokument_Detalji");

            if (rel != null)
            {
                GridTableDescriptor detail = rel.ChildTableDescriptor;

                detail.AllowEdit = false;

                detail.VisibleColumns.Clear();
                detail.VisibleColumns.Add("RB");
                detail.VisibleColumns.Add("NazivArtikla");
                detail.VisibleColumns.Add("NazivUsluge");
                detail.VisibleColumns.Add("JM");
                detail.VisibleColumns.Add("Koleta");
                detail.VisibleColumns.Add("Bruto");
                detail.VisibleColumns.Add("Neto");
                detail.VisibleColumns.Add("Pozicija");
                detail.VisibleColumns.Add("Paleta");
                detail.VisibleColumns.Add("VrstaPalete");
                detail.VisibleColumns.Add("OdradjenaUsluga");

                detail.Columns["RB"].HeaderText = "RB";
                detail.Columns["NazivArtikla"].HeaderText = "Naziv artikla";
                detail.Columns["NazivUsluge"].HeaderText = "Usluga";
                detail.Columns["JM"].HeaderText = "JM";
                detail.Columns["Koleta"].HeaderText = "Koleta";
                detail.Columns["Bruto"].HeaderText = "Bruto";
                detail.Columns["Neto"].HeaderText = "Neto";
                detail.Columns["Pozicija"].HeaderText = "Pozicija";
                detail.Columns["Paleta"].HeaderText = "Paleta";
                detail.Columns["VrstaPalete"].HeaderText = "Vrsta palete";
                detail.Columns["OdradjenaUsluga"].HeaderText = "Urađena usluga";
            }



            gridGroupingControl1.Refresh();
        }
        private void PodesiBojeMasterRedova(GridTableDescriptor master)
        {
            master.ConditionalFormats.Clear();

            GridConditionalFormatDescriptor redFormat = new GridConditionalFormatDescriptor();
            redFormat.Name = "Red_RED";
            redFormat.Expression = "[BojaReda] = 'RED'";
            redFormat.Appearance.AnyRecordFieldCell.BackColor = Color.LightCoral;
            redFormat.Appearance.AnyRecordFieldCell.TextColor = Color.Black;
            master.ConditionalFormats.Add(redFormat);

            GridConditionalFormatDescriptor yellowFormat = new GridConditionalFormatDescriptor();
            yellowFormat.Name = "Red_YELLOW";
            yellowFormat.Expression = "[BojaReda] = 'YELLOW'";
            yellowFormat.Appearance.AnyRecordFieldCell.BackColor = Color.Khaki;
            yellowFormat.Appearance.AnyRecordFieldCell.TextColor = Color.Black;
            master.ConditionalFormats.Add(yellowFormat);

            GridConditionalFormatDescriptor greenFormat = new GridConditionalFormatDescriptor();
            greenFormat.Name = "Red_GREEN";
            greenFormat.Expression = "[BojaReda] = 'GREEN'";
            greenFormat.Appearance.AnyRecordFieldCell.BackColor = Color.LightGreen;
            greenFormat.Appearance.AnyRecordFieldCell.TextColor = Color.Black;
            master.ConditionalFormats.Add(greenFormat);
        }
        private GridRelationDescriptor NadjiRelaciju(GridTableDescriptor master, string nazivRelacije)
        {
            foreach (GridRelationDescriptor item in master.Relations)
            {
                if (item.Name == nazivRelacije)
                    return item;
            }

            return null;
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Record record = gridGroupingControl1.Table.CurrentRecord;

                if (record == null)
                {
                    MessageBox.Show("Izaberite red dokumenta.", "Obaveštenje",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DataRowView row = record.GetData() as DataRowView;

                if (row == null)
                {
                    MessageBox.Show("Nije moguće pročitati izabrani red.", "Greška",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!row.Row.Table.Columns.Contains("BojaReda"))
                {
                    MessageBox.Show("Izaberite glavni red dokumenta, ne red iz podtabele.", "Obaveštenje",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int brojDokumenta = Convert.ToInt32(row["BrojDokumenta"]);
                string postupak = Convert.ToString(row["Postupak"]).Trim();

                if (string.IsNullOrWhiteSpace(postupak))
                {
                    MessageBox.Show("Za izabrani dokument nije pronađen postupak.", "Greška",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult potvrda = MessageBox.Show(
                    "Da li želite da potvrdite dokument broj " + brojDokumenta +
                    " za postupak: " + postupak + "?",
                    "Potvrda dokumenta",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (potvrda != DialogResult.Yes)
                    return;

                if (postupak.Equals("Prijem", StringComparison.OrdinalIgnoreCase))
                {
                    PotvrdiPrijem(brojDokumenta);

                    MessageBox.Show("Prijem je uspešno potvrđen.", "Uspešno",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (postupak.Equals("Otprema", StringComparison.OrdinalIgnoreCase))
                {
                    PotvrdiOtprema(brojDokumenta);

                    MessageBox.Show("Otprema je uspešno potvrđena.", "Uspešno",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        "Za postupak '" + postupak + "' nije definisana procedura potvrde.",
                        "Obaveštenje",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                UcitajGridRukovalac();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom potvrde dokumenta:\n" + ex.Message,
                    "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PotvrdiPrijem(int brojDokumenta)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            using (SqlCommand cmd = new SqlCommand("PotvrdiPrijem", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Prijemnica", SqlDbType.Int).Value = brojDokumenta;

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void PotvrdiOtprema(int brojDokumenta)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            using (SqlCommand cmd = new SqlCommand("PotvrdiOtpremu", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Otpremnica", SqlDbType.Int).Value = brojDokumenta;

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}