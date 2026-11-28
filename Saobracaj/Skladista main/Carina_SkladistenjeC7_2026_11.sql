/* =============================================================================================
   Carina: pregled nerazduženih C7 i izveštaj o skladištenju   28.11.2026.
   Ekran: Carinsko skladište > Pregled nerazduženih C7/SD (frmSkladistenjeC7).

   Jedan C7 = jedan magacinski predmet (MagacinskiPredmet) sa naimenovanjima (RNCarinskoStavke).
     - Početak skladištenja: datum potvrđene prijemnice naloga (RNCarinskoSkladistePrijemnica, Status ZA; najraniji).
       Prijemnica nema posebnu kolonu za datum potvrde, pa je to datum prijemnice, kao u carinskoj knjizi.
     - Razduženje: dokumenti razduženja (C4, IZ3, C3...) vezani za otpremnice (RNCarinskoRazduzenje,
       RNCarinskoRazduzenjeStavka, RNCarinskoOtpremnicaRazduzenje); datum je datum izdavanja dokumenta.
     - Kraj skladištenja: datum poslednjeg razduženja, kad je C7 razdužen do kraja (koleta i bruto).
       Dok nije razdužen do kraja, C7 je na skladištu i dani se računaju do danas.

   Procedure:
     dbo.sp_C7_Skladistenje   jedan red po C7: prijavljeno, razduženo, nerazduženo, početak, kraj, dani
     dbo.sp_C7_Razduzenja     razduženja jednog C7, sa stanjem posle svakog

   Pre ove skripte moraju biti puštene: Carina_Izlaz_2026_11.sql, Carina_MagacinskiBrojGodina_2026_11.sql.
   Skripta samo pravi procedure (podaci se ne menjaju) i može se pustiti više puta.
   ============================================================================================= */
SET NOCOUNT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF OBJECT_ID('dbo.RNCarinskoRazduzenje', 'U') IS NULL OR OBJECT_ID('dbo.RNCarinskoRazduzenjeStavka', 'U') IS NULL
   OR OBJECT_ID('dbo.RNCarinskoOtpremnicaRazduzenje', 'U') IS NULL OR OBJECT_ID('dbo.MagacinskiBrojCarinskiGodina', 'U') IS NULL
BEGIN
    RAISERROR(N'GREŠKA: prvo pustiti Carina_Izlaz_2026_11.sql i Carina_MagacinskiBrojGodina_2026_11.sql; ova skripta se ne izvršava.', 16, 1);
    SET NOEXEC ON;   -- ostatak skripte se ne izvršava (isključuje se na kraju)
END
GO

-- Svi parametri su filteri; NULL = bez filtera.
--   @Od, @Do         period: C7 koji su bili na skladištu u periodu (početak <= @Do, kraj >= @Od ili još na skladištu);
--                    DanaUPeriodu = dani skladištenja unutar perioda
--   @SamoNerazduzeni 1 = samo C7 koji nisu razduženi do kraja
--   @MinDana         samo C7 koji su na skladištu bar toliko dana (npr. 90)
CREATE OR ALTER PROCEDURE dbo.sp_C7_Skladistenje
    @Od              date         = NULL,
    @Do              date         = NULL,
    @Vlasnik         int          = NULL,
    @MagacinskiBroj  int          = NULL,
    @SamoNerazduzeni bit          = 0,
    @MinDana         int          = NULL,
    @C7              nvarchar(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET @C7 = NULLIF(LTRIM(RTRIM(ISNULL(@C7, N''))), N'');
    DECLARE @Danas date = CAST(GETDATE() AS date);

    SELECT c.IDMagacinskogPredmeta, c.RN, c.MagacinskiBroj, c.MagacinskiBrojOznaka, c.BrojC7, c.VlasnikID, c.Vlasnik,
           c.Naimenovanja, c.Pocetak, k.Kraj, c.PoslednjeRazduzenje, c.BrojRazduzenja,
           c.Koleta, c.Bruto, c.Neto, c.Vrednost, c.Valuta,
           c.RazduzenoKoleta, c.RazduzenoBruto,
           c.Koleta - c.RazduzenoKoleta AS NerazduzenoKoleta,
           c.Bruto - c.RazduzenoBruto AS NerazduzenoBruto,
           c.Neto - c.RazduzenoNeto AS NerazduzenoNeto,
           c.Vrednost - c.RazduzenoVrednost AS NerazduzenaVrednost,
           DATEDIFF(day, c.Pocetak, ISNULL(k.Kraj, @Danas)) AS DanaUkupno,
           CASE WHEN @Od IS NULL AND @Do IS NULL THEN NULL
                ELSE CASE WHEN p.Kraj < p.Pocetak THEN 0 ELSE DATEDIFF(day, p.Pocetak, p.Kraj) END END AS DanaUPeriodu,
           CASE WHEN k.Kraj IS NULL THEN N'Na skladištu' ELSE N'Razduženo' END AS Status,
           ISNULL(STUFF((SELECT N', ' + x.Dokument
                         FROM (SELECT DISTINCT r.ID,
                                      CASE WHEN RTRIM(r.CI) <> N'' THEN RTRIM(r.CI) + N'/' ELSE N'' END
                                        + RTRIM(r.Vrsta) + N'/' + RTRIM(r.Broj) + N'/' + CONVERT(nvarchar(4), r.Godina) AS Dokument
                               FROM dbo.RNCarinskoRazduzenjeStavka rs
                               JOIN dbo.RNCarinskoRazduzenje r ON r.ID = rs.IDRazduzenja
                               JOIN dbo.RNCarinskoStavke s ON s.ID = rs.IDNaimenovanja
                               WHERE s.IDNadredjena = c.IDMagacinskogPredmeta) x
                         ORDER BY x.ID
                         FOR XML PATH(''), TYPE).value('.', 'nvarchar(max)'), 1, 2, N''), N'') AS DokumentiRazduzenja,
           ISNULL(STUFF((SELECT N', ' + CONVERT(nvarchar(10), x.IDOtpremnice)
                         FROM (SELECT DISTINCT o.IDOtpremnice
                               FROM dbo.RNCarinskoRazduzenjeStavka rs
                               JOIN dbo.RNCarinskoStavke s ON s.ID = rs.IDNaimenovanja
                               JOIN dbo.RNCarinskoOtpremnicaRazduzenje o ON o.IDRazduzenja = rs.IDRazduzenja
                               WHERE s.IDNadredjena = c.IDMagacinskogPredmeta) x
                         ORDER BY x.IDOtpremnice
                         FOR XML PATH(''), TYPE).value('.', 'nvarchar(max)'), 1, 2, N''), N'') AS Otpremnice
    FROM (
        SELECT mp.ID AS IDMagacinskogPredmeta, mp.RN, rns.MagacinskiBroj,
               ISNULL(CONVERT(nvarchar(10), g.Broj) + N'/' + CONVERT(nvarchar(4), g.Godina),
                      CONVERT(nvarchar(20), rns.MagacinskiBroj)) AS MagacinskiBrojOznaka,
               ISNULL(NULLIF(RTRIM(CONVERT(nvarchar(100), mp.BrojJCI)), N''),
                      RTRIM(CONVERT(nvarchar(100), ISNULL(rns.SmestajniDokument, N'')))) AS BrojC7,
               v.id AS VlasnikID, RTRIM(CONVERT(nvarchar(200), ISNULL(v.Naziv, N''))) AS Vlasnik,
               pr.Datum AS Pocetak,
               n.Naimenovanja, n.Koleta, n.Bruto, n.Neto, n.Vrednost, n.Valuta,
               ISNULL(rz.Koleta, 0) AS RazduzenoKoleta, ISNULL(rz.Bruto, 0) AS RazduzenoBruto,
               ISNULL(rz.Neto, 0) AS RazduzenoNeto, ISNULL(rz.Vrednost, 0) AS RazduzenoVrednost,
               ISNULL(rz.Broj, 0) AS BrojRazduzenja, rz.Poslednje AS PoslednjeRazduzenje
        FROM dbo.MagacinskiPredmet mp
        JOIN dbo.RadniNalogSkladista rns ON rns.ID = mp.RN
        CROSS APPLY (SELECT CAST(MIN(x.Datum) AS date) AS Datum
                     FROM dbo.RNCarinskoSkladistePrijemnica x
                     WHERE x.RN = mp.RN AND x.Status = 'ZA') pr
        CROSS APPLY (SELECT COUNT(*) AS Naimenovanja,
                            ISNULL(SUM(s.Koleta), 0) AS Koleta, ISNULL(SUM(s.Bruto), 0) AS Bruto,
                            ISNULL(SUM(s.Neto), 0) AS Neto, ISNULL(SUM(s.Vrednost), 0) AS Vrednost,
                            CASE WHEN COUNT(DISTINCT RTRIM(CONVERT(nvarchar(10), s.Valuta))) = 1
                                 THEN MAX(RTRIM(CONVERT(nvarchar(10), s.Valuta))) ELSE N'' END AS Valuta
                     FROM dbo.RNCarinskoStavke s
                     WHERE s.IDNadredjena = mp.ID) n
        OUTER APPLY (SELECT SUM(rs.Koleta) AS Koleta, SUM(rs.Bruto) AS Bruto, SUM(rs.Neto) AS Neto, SUM(rs.Vrednost) AS Vrednost,
                            COUNT(DISTINCT rs.IDRazduzenja) AS Broj,
                            MAX(CAST(ISNULL(r.DatumIzdavanja, r.DatumUnosa) AS date)) AS Poslednje
                     FROM dbo.RNCarinskoRazduzenjeStavka rs
                     JOIN dbo.RNCarinskoRazduzenje r ON r.ID = rs.IDRazduzenja
                     JOIN dbo.RNCarinskoStavke s ON s.ID = rs.IDNaimenovanja
                     WHERE s.IDNadredjena = mp.ID) rz
        LEFT JOIN dbo.VlasniciRobe v ON v.id = ISNULL(NULLIF(mp.VlasnikRobe, 0), rns.VlasnikRobe)
        LEFT JOIN dbo.MagacinskiBrojCarinskiGodina g ON g.ID = rns.MagacinskiBroj
        WHERE pr.Datum IS NOT NULL
          AND n.Naimenovanja > 0
          AND (@MagacinskiBroj IS NULL OR rns.MagacinskiBroj = @MagacinskiBroj)
          AND (@Vlasnik IS NULL OR v.id = @Vlasnik)
    ) c
    -- kraj: poslednje razduženje, kad je C7 razdužen do kraja
    CROSS APPLY (SELECT CASE WHEN c.BrojRazduzenja > 0 AND c.Koleta - c.RazduzenoKoleta <= 0.0005
                                  AND c.Bruto - c.RazduzenoBruto <= 0.001
                             THEN c.PoslednjeRazduzenje END AS Kraj) k
    -- deo skladištenja unutar perioda (bez perioda: nebitno)
    CROSS APPLY (SELECT CASE WHEN @Od IS NOT NULL AND @Od > c.Pocetak THEN @Od ELSE c.Pocetak END AS Pocetak,
                        CASE WHEN k.Kraj IS NOT NULL AND (@Do IS NULL OR k.Kraj < @Do) THEN k.Kraj
                             WHEN @Do IS NOT NULL AND @Do < @Danas THEN @Do
                             ELSE @Danas END AS Kraj) p
    WHERE (@Do IS NULL OR c.Pocetak <= @Do)
      AND (@Od IS NULL OR k.Kraj IS NULL OR k.Kraj >= @Od)
      AND (@SamoNerazduzeni = 0 OR k.Kraj IS NULL)
      AND (@MinDana IS NULL OR DATEDIFF(day, c.Pocetak, ISNULL(k.Kraj, @Danas)) >= @MinDana)
      AND (@C7 IS NULL OR c.BrojC7 LIKE N'%' + @C7 + N'%')
    ORDER BY c.Pocetak, c.MagacinskiBroj, c.IDMagacinskogPredmeta;
END
GO

-- Razduženja jednog C7 (magacinskog predmeta) po datumu, sa nerazduženim stanjem posle svakog
CREATE OR ALTER PROCEDURE dbo.sp_C7_Razduzenja
    @MagacinskiPredmet int
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Pocetak date = (SELECT CAST(MIN(x.Datum) AS date)
                             FROM dbo.MagacinskiPredmet mp
                             JOIN dbo.RNCarinskoSkladistePrijemnica x ON x.RN = mp.RN AND x.Status = 'ZA'
                             WHERE mp.ID = @MagacinskiPredmet);

    DECLARE @Ukupno TABLE (Koleta decimal(18,3), Bruto decimal(18,3));
    INSERT @Ukupno
    SELECT ISNULL(SUM(s.Koleta), 0), ISNULL(SUM(s.Bruto), 0)
    FROM dbo.RNCarinskoStavke s WHERE s.IDNadredjena = @MagacinskiPredmet;

    DECLARE @r TABLE (IDRazduzenja int PRIMARY KEY, Datum date, Dokument nvarchar(100),
                      Koleta decimal(18,3), Bruto decimal(18,3), Neto decimal(18,3), Vrednost decimal(18,2));
    INSERT @r
    SELECT r.ID, CAST(ISNULL(r.DatumIzdavanja, r.DatumUnosa) AS date),
           CASE WHEN RTRIM(r.CI) <> N'' THEN RTRIM(r.CI) + N'/' ELSE N'' END
             + RTRIM(r.Vrsta) + N'/' + RTRIM(r.Broj) + N'/' + CONVERT(nvarchar(4), r.Godina),
           SUM(rs.Koleta), SUM(rs.Bruto), SUM(rs.Neto), SUM(rs.Vrednost)
    FROM dbo.RNCarinskoRazduzenjeStavka rs
    JOIN dbo.RNCarinskoRazduzenje r ON r.ID = rs.IDRazduzenja
    JOIN dbo.RNCarinskoStavke s ON s.ID = rs.IDNaimenovanja
    WHERE s.IDNadredjena = @MagacinskiPredmet
    GROUP BY r.ID, r.DatumIzdavanja, r.DatumUnosa, r.CI, r.Vrsta, r.Broj, r.Godina;

    SELECT r.IDRazduzenja, r.Datum, r.Dokument,
           ISNULL(STUFF((SELECT N', ' + CONVERT(nvarchar(10), o.IDOtpremnice)
                         FROM dbo.RNCarinskoOtpremnicaRazduzenje o
                         WHERE o.IDRazduzenja = r.IDRazduzenja
                         ORDER BY o.IDOtpremnice
                         FOR XML PATH(''), TYPE).value('.', 'nvarchar(max)'), 1, 2, N''), N'') AS Otpremnice,
           r.Koleta, r.Bruto, r.Neto, r.Vrednost,
           DATEDIFF(day, @Pocetak, r.Datum) AS DanaOdPocetka,
           u.Koleta - z.Koleta AS NerazduzenoKoleta,
           u.Bruto - z.Bruto AS NerazduzenoBruto
    FROM @r r
    CROSS JOIN @Ukupno u
    CROSS APPLY (SELECT SUM(r2.Koleta) AS Koleta, SUM(r2.Bruto) AS Bruto
                 FROM @r r2
                 WHERE r2.Datum < r.Datum OR (r2.Datum = r.Datum AND r2.IDRazduzenja <= r.IDRazduzenja)) z
    ORDER BY r.Datum, r.IDRazduzenja;
END
GO

PRINT 'Carina_SkladistenjeC7_2026_11.sql: procedure dbo.sp_C7_Skladistenje i dbo.sp_C7_Razduzenja su napravljene.';
GO
SET NOEXEC OFF;
GO
