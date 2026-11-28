using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Saobracaj.Skladista_main
{
    // Kartica prometa carinskog skladišta (dugme „Kartica prometa“ u meniju Carinsko skladište, 28.11.2026).
    // Tabela je ranije čitala view MagacinskaKnjigaCarinska, a on ima samo usklađištenja, pa se otpreme nisu videle.
    // Promet se sada čita iz dokumenata: ulaz su stavke potvrđenih prijemnica (Status ZA), izlaz su stavke otpremnica.
    // Stanje posle reda se računa ovde, po magacinskom broju i robi (tarifni broj, naimenovanje, naziv), jer baza ima
    // nivo kompatibilnosti ispod 110 (bez SUM ... OVER (ORDER BY ...)).
    public partial class MagacinskaKnjigaCarinska
    {
        private static string UpitKartice(string connection)
        {
            return @"
SELECT x.VrstaReda, x.Promet, x.Datum, x.Dokument, x.BrojDokumenta, x.Nalog, x.MBID,
       CASE WHEN x.MBID IS NULL THEN N'' ELSE " + MagacinskiBrojOznaka.Izraz(connection, "x.MBID") + @" END AS MagacinskiBroj,
       x.VlasnikRobe, x.TarifniBroj, x.Naimenovanje, x.Naziv, x.JM,
       x.UlazKoleta, x.IzlazKoleta, x.UlazBruto, x.IzlazBruto, x.Vrednost, x.Valuta, x.Pozicija, x.RB
FROM (
    -- ulaz: potvrđena prijemnica
    SELECT 1 AS VrstaReda, N'Ulaz' AS Promet, pr.Datum, N'Prijemnica' AS Dokument, pr.ID AS BrojDokumenta, rn.ID AS Nalog,
           rn.MagacinskiBroj AS MBID, RTRIM(CONVERT(nvarchar(200), ISNULL(v.Naziv, N''))) AS VlasnikRobe,
           RTRIM(CONVERT(nvarchar(20), ISNULL(s.NHM, N''))) AS TarifniBroj,
           RTRIM(CONVERT(nvarchar(500), ISNULL(s.Naimenovanje, N''))) AS Naimenovanje,
           RTRIM(CONVERT(nvarchar(500), ISNULL(s.Naziv, N''))) AS Naziv,
           RTRIM(CONVERT(nvarchar(50), ISNULL(s.JM, N''))) AS JM,
           CONVERT(decimal(18,3), ISNULL(s.Koleta, 0)) AS UlazKoleta, CONVERT(decimal(18,3), 0) AS IzlazKoleta,
           CONVERT(decimal(18,3), ISNULL(s.Bruto, 0)) AS UlazBruto, CONVERT(decimal(18,3), 0) AS IzlazBruto,
           CONVERT(decimal(18,2), ISNULL(s.Vrednost, 0)) AS Vrednost,
           RTRIM(CONVERT(nvarchar(10), ISNULL(s.Valuta, N''))) AS Valuta,
           RTRIM(CONVERT(nvarchar(100), ISNULL(s.Pozicija, N''))) AS Pozicija, s.RB
    FROM RNCarinskoPrijemnicaStavke s
    INNER JOIN RNCarinskoSkladistePrijemnica pr ON pr.ID = s.IDNadredjena
    INNER JOIN RadniNalogSkladista rn ON rn.ID = pr.RN
    LEFT JOIN VlasniciRobe v ON v.id = rn.VlasnikRobe
    WHERE pr.Status = 'ZA' AND rn.VrstaRN = 'Carinsko'

    UNION ALL

    -- izlaz: otpremnica; magacinski broj i vlasnik sa naloga prijema, a kad otpremnica nema prijemnicu, sa naloga otpreme
    SELECT 2, N'Izlaz', ot.DatumOtpreme, N'Otpremnica', ot.ID, ot.RN,
           ISNULL(rnp.MagacinskiBroj, rno.MagacinskiBroj), RTRIM(CONVERT(nvarchar(200), ISNULL(v.Naziv, N''))),
           RTRIM(CONVERT(nvarchar(20), ISNULL(st.NHM, N''))),
           RTRIM(CONVERT(nvarchar(500), ISNULL(st.Naimenovanje, N''))),
           RTRIM(CONVERT(nvarchar(500), ISNULL(st.Naziv, N''))),
           RTRIM(CONVERT(nvarchar(50), ISNULL(st.JM, N''))),
           CONVERT(decimal(18,3), 0), CONVERT(decimal(18,3), ISNULL(st.Koleta, 0)),
           CONVERT(decimal(18,3), 0), CONVERT(decimal(18,3), ISNULL(st.Bruto, 0)),
           CONVERT(decimal(18,2), ISNULL(st.Vrednost, 0)),
           RTRIM(CONVERT(nvarchar(10), ISNULL(st.Valuta, N''))),
           RTRIM(CONVERT(nvarchar(100), ISNULL(st.Pozicija, N''))), st.RB
    FROM RNCarinskoSkladisteOtpremnicaStavke st
    INNER JOIN RNCarinskoSkladisteOtpremnica ot ON ot.ID = st.IDNadredjena
    LEFT JOIN RadniNalogSkladista rno ON rno.ID = ot.RN
    LEFT JOIN RNCarinskoSkladistePrijemnica pp ON pp.ID = ot.Prijemnica
    LEFT JOIN RadniNalogSkladista rnp ON rnp.ID = pp.RN
    LEFT JOIN VlasniciRobe v ON v.id = ISNULL(rnp.VlasnikRobe, rno.VlasnikRobe)
    WHERE ISNULL(rnp.VrstaRN, rno.VrstaRN) = 'Carinsko'
) x";
        }

        // redovi poređani po magacinskom broju, robi i datumu (ulaz pre izlaza istog dana), sa stanjem posle svakog reda
        private static DataTable KarticaPrometa(string connection)
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(connection))
            using (var da = new SqlDataAdapter(UpitKartice(connection), conn))
                da.Fill(dt);

            var rez = dt.Clone();
            rez.Columns.Add("StanjeKoleta", typeof(decimal));
            rez.Columns.Add("StanjeBruto", typeof(decimal));

            Func<DataRow, string> roba = r => Convert.ToString(r["TarifniBroj"]) + "\u0001" + Convert.ToString(r["Naimenovanje"]) + "\u0001" + Convert.ToString(r["Naziv"]);
            var redosled = dt.Rows.Cast<DataRow>()
                .OrderBy(r => r["MBID"] == DBNull.Value ? int.MaxValue : Convert.ToInt32(r["MBID"]))
                .ThenBy(roba, StringComparer.CurrentCultureIgnoreCase)
                .ThenBy(r => r["Datum"] == DBNull.Value ? DateTime.MaxValue : Convert.ToDateTime(r["Datum"]).Date)
                .ThenBy(r => Convert.ToInt32(r["VrstaReda"]))
                .ThenBy(r => Convert.ToInt32(r["BrojDokumenta"]))
                .ThenBy(r => r["RB"] == DBNull.Value ? 0 : Convert.ToInt32(r["RB"]));

            var stanje = new Dictionary<string, decimal[]>(StringComparer.CurrentCultureIgnoreCase);
            foreach (var r in redosled)
            {
                string kljuc = Convert.ToString(r["MBID"]) + "\u0002" + roba(r);
                if (!stanje.TryGetValue(kljuc, out var s))
                    stanje[kljuc] = s = new decimal[2];
                s[0] += Convert.ToDecimal(r["UlazKoleta"]) - Convert.ToDecimal(r["IzlazKoleta"]);
                s[1] += Convert.ToDecimal(r["UlazBruto"]) - Convert.ToDecimal(r["IzlazBruto"]);

                rez.Rows.Add(r.ItemArray.Concat(new object[] { s[0], s[1] }).ToArray());
            }
            return rez;
        }

        // nazivi kolona rečima, brojevi desno; kolone za redosled se ne prikazuju
        private void PodesiKoloneKartice()
        {
            var naslovi = new Dictionary<string, string>
            {
                { "Promet", "Promet" }, { "Datum", "Datum" }, { "Dokument", "Dokument" }, { "BrojDokumenta", "Broj dokumenta" },
                { "Nalog", "Nalog" }, { "MagacinskiBroj", "Magacinski broj" }, { "VlasnikRobe", "Vlasnik robe" },
                { "TarifniBroj", "Tarifni broj" }, { "Naimenovanje", "Naimenovanje" }, { "Naziv", "Naziv robe" }, { "JM", "JM" },
                { "UlazKoleta", "Ulaz koleta" }, { "IzlazKoleta", "Izlaz koleta" }, { "StanjeKoleta", "Stanje koleta" },
                { "UlazBruto", "Ulaz bruto kg" }, { "IzlazBruto", "Izlaz bruto kg" }, { "StanjeBruto", "Stanje bruto kg" },
                { "Vrednost", "Vrednost" }, { "Valuta", "Valuta" }, { "Pozicija", "Pozicija" }
            };
            var formati = new Dictionary<string, string>
            {
                { "UlazKoleta", "#,##0.###" }, { "IzlazKoleta", "#,##0.###" }, { "StanjeKoleta", "#,##0.###" },
                { "UlazBruto", "N2" }, { "IzlazBruto", "N2" }, { "StanjeBruto", "N2" }, { "Vrednost", "N2" }
            };

            var td = gridGroupingControl1.TableDescriptor;
            foreach (var skrivena in new[] { "VrstaReda", "MBID", "RB" })
                if (td.VisibleColumns.Contains(skrivena))
                    td.VisibleColumns.Remove(skrivena);

            foreach (GridColumnDescriptor c in td.Columns)
            {
                if (naslovi.TryGetValue(c.MappingName, out var naslov))
                    c.HeaderText = naslov;
                if (formati.TryGetValue(c.MappingName, out var format))
                {
                    c.Appearance.AnyRecordFieldCell.Format = format;
                    c.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                }
                else if (c.MappingName == "BrojDokumenta" || c.MappingName == "Nalog")
                    c.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                else if (c.MappingName == "Datum")
                    c.Appearance.AnyRecordFieldCell.Format = "dd.MM.yyyy";
            }
        }
    }
}
