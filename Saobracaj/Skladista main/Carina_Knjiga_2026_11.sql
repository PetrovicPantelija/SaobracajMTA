/* Carina – carinska knjiga (evidencija carinskog skladišta, obrazac #1008)  RAZ-16, RAZ-18
   26.11.2026.
   Knjiga se ne unosi ručno, nego se izvodi iz potvrđenih događaja:
     - usklađištenje (+): naimenovanje C7 (RNCarinskoStavke) čiji nalog za prijem ima potvrđenu prijemnicu (Status ZA);
     - razduženje (-): stavka dokumenta razduženja C4 / IZ3 / C3... (RNCarinskoRazduzenjeStavka, ekran Dokument razduženja).
   Nerazdužena količina (kolone 8 i 9) je tekuće stanje naimenovanja posle svakog reda. Valuta je valuta stavke (RAZ-18).

   Kolone obrasca: 1 Rbr | 2 Datum | 3 CI prijave | 4 Vrsta dokumenta, broj/godina | 5 RB naimenovanja |
   6–7 Usklađištena roba: koleta, težina (u redu razduženja sa minusom) | 8–9 Nerazduženo: koleta, težina |
   10 Vrsta robe (naimenovanje + kontejneri + C7) | 11 Vrednost u valuti | 12 CI razduženja, vrsta, broj/godina |
   13 Datum izdavanja | 14 Napomena (vlasnik; otpremnice) | 15 Sektor

   Kolona 1 (Rbr) je magacinski broj u godini (MagacinskiBrojCarinskiGodina.Broj; magacinski broj je redni broj u carinskoj
   knjizi, posebnog rednog broja nema, 27.11.2026; kolonu MagacinskiPredmet.RbrKnjige uklanja Carina_RbrKnjige_Ukloni_2026_11.sql).

   Pre ove skripte moraju biti puštene: Carina_Kontejneri_2026_11.sql, Carina_Izlaz_2026_11.sql,
   Carina_MagacinskiBrojGodina_2026_11.sql. Ekran: Carinsko skladište > Carinska knjiga.
   Skripta se može pustiti više puta. Radi i na bazi sa nivoom kompatibilnosti 100. */

SET NOCOUNT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF OBJECT_ID('dbo.RNCarinskoRazduzenje', 'U') IS NULL OR OBJECT_ID('dbo.RNCarinskoRazduzenjeStavka', 'U') IS NULL
   OR OBJECT_ID('dbo.RNCarinskoOtpremnicaRazduzenje', 'U') IS NULL OR OBJECT_ID('dbo.RNCarinskoKontejner', 'U') IS NULL
   OR OBJECT_ID('dbo.MagacinskiBrojCarinskiGodina', 'U') IS NULL
BEGIN
    RAISERROR(N'GREŠKA: prvo pustiti Carina_Kontejneri_2026_11.sql, Carina_Izlaz_2026_11.sql i Carina_MagacinskiBrojGodina_2026_11.sql; ova skripta se ne izvršava.', 16, 1);
    SET NOEXEC ON;   -- ostatak skripte se ne izvršava (isključuje se na kraju)
END
GO

-- Svi parametri su filteri; NULL = bez filtera. @Od/@Do važe za datum reda (usklađištenje ili izdavanje),
-- a tekuće stanje se uvek računa od usklađištenja.
CREATE OR ALTER PROCEDURE dbo.sp_CarinskaKnjiga
    @Godina         int          = NULL,
    @MagacinskiBroj int          = NULL,
    @Vlasnik        int          = NULL,
    @Od             date         = NULL,
    @Do             date         = NULL,
    @C7             nvarchar(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET @C7 = NULLIF(LTRIM(RTRIM(ISNULL(@C7, N''))), N'');

    DECLARE @n TABLE (
        IDNaimenovanja  int PRIMARY KEY,
        RN              int,
        MagacinskiBroj  int,
        Godina          int,
        Rbr             int,
        Datum           date,
        CI              nvarchar(20),
        Dokument        nvarchar(150),
        BrojC7          nvarchar(100),
        RB              int,
        Naimenovanje    nvarchar(500),
        NHM             nvarchar(20),
        Koleta          decimal(18,3),
        Bruto           decimal(18,3),
        Neto            decimal(18,3),
        Vrednost        decimal(18,2),
        Valuta          nvarchar(10),
        VlasnikID       int,
        Vlasnik         nvarchar(200),
        Kontejneri      nvarchar(1000));

    INSERT @n (IDNaimenovanja, RN, MagacinskiBroj, Godina, Rbr, Datum, CI, Dokument, BrojC7, RB, Naimenovanje, NHM,
               Koleta, Bruto, Neto, Vrednost, Valuta, VlasnikID, Vlasnik, Kontejneri)
    SELECT n.ID, mp.RN, rns.MagacinskiBroj,
           ISNULL(mp.Godina, YEAR(ISNULL(mp.Datum, pr.Datum))),
           ISNULL(mbg.Broj, rns.MagacinskiBroj),   -- redni broj u knjizi = magacinski broj u godini (27.11.2026)
           CAST(ISNULL(mp.Datum, pr.Datum) AS date),
           ISNULL(NULLIF(RTRIM(CONVERT(nvarchar(20), mp.CI)), N''), RTRIM(CONVERT(nvarchar(20), ci.CIOznaka))),
           d.Vrsta + CASE WHEN d.Broj = N'' THEN N''
                          -- broj C7 u obliku CI/C7/broj/godina (42072/C7/15622/2025): kolona 4 je „C7 15622/2025“
                          WHEN d.Broj LIKE N'%/' + d.Vrsta + N'/%/%' AND d.Vrsta <> N''
                               THEN N' ' + SUBSTRING(d.Broj, CHARINDEX(N'/' + d.Vrsta + N'/', d.Broj) + LEN(d.Vrsta) + 2, 100)
                          WHEN CHARINDEX(N'/', d.Broj) > 0 THEN N' ' + d.Broj
                          ELSE N'/' + d.Broj + ISNULL(N'/' + CONVERT(nvarchar(4), ISNULL(mp.Godina, YEAR(ISNULL(mp.Datum, pr.Datum)))), N'') END,
           d.Broj,
           n.RB,
           RTRIM(CONVERT(nvarchar(500), ISNULL(n.Naimenovanje, N''))),
           RTRIM(CONVERT(nvarchar(20), ISNULL(n.NHM, N''))),
           ISNULL(n.Koleta, 0), ISNULL(n.Bruto, 0), ISNULL(n.Neto, 0), ISNULL(n.Vrednost, 0),
           RTRIM(CONVERT(nvarchar(10), ISNULL(n.Valuta, N''))),
           v.id,
           RTRIM(CONVERT(nvarchar(200), v.Naziv)),
           STUFF((SELECT N' ' + RTRIM(CONVERT(nvarchar(20), k.BrojKontejnera))
                  FROM dbo.RNCarinskoKontejner k
                  WHERE k.IDNaloga = mp.RN
                  ORDER BY k.RB
                  FOR XML PATH(''), TYPE).value('.', 'nvarchar(max)'), 1, 1, N'')
    FROM dbo.RNCarinskoStavke n
    JOIN dbo.MagacinskiPredmet mp ON mp.ID = n.IDNadredjena
    JOIN dbo.RadniNalogSkladista rns ON rns.ID = mp.RN
    LEFT JOIN dbo.MagacinskiBrojCarinskiGodina mbg ON mbg.ID = rns.MagacinskiBroj
    CROSS APPLY (SELECT MIN(p.Datum) AS Datum, COUNT(*) AS Broj
                 FROM dbo.RNCarinskoSkladistePrijemnica p
                 WHERE p.RN = mp.RN AND p.Status = 'ZA') pr
    LEFT JOIN dbo.Carinarnice ci ON ci.ID = rns.OdredisnaCarinarnicaPrijem
    LEFT JOIN dbo.VrstaCarinskogPostupka vcp ON vcp.ID = rns.CarinskiPostupak
    LEFT JOIN dbo.VlasniciRobe v ON v.id = ISNULL(NULLIF(mp.VlasnikRobe, 0), rns.VlasnikRobe)
    CROSS APPLY (SELECT ISNULL(NULLIF(RTRIM(CONVERT(nvarchar(20), mp.VrstaCP)), N''), ISNULL(RTRIM(CONVERT(nvarchar(20), vcp.Oznaka)), N'')) AS Vrsta,
                        ISNULL(NULLIF(RTRIM(CONVERT(nvarchar(100), mp.BrojJCI)), N''),
                               ISNULL(RTRIM(CONVERT(nvarchar(100), rns.SmestajniDokument)), N'')) AS Broj) d
    WHERE pr.Broj > 0
      AND (@MagacinskiBroj IS NULL OR rns.MagacinskiBroj = @MagacinskiBroj)
      AND (@Vlasnik IS NULL OR v.id = @Vlasnik)
      AND (@C7 IS NULL OR d.Broj LIKE N'%' + @C7 + N'%');

    IF @Godina IS NOT NULL
        DELETE FROM @n WHERE ISNULL(Godina, 0) <> @Godina;

    DECLARE @r TABLE (
        IDNaimenovanja  int,
        IDRazduzenja    int,
        Datum           date,
        Koleta          decimal(18,3),
        Bruto           decimal(18,3),
        Neto            decimal(18,3),
        Vrednost        decimal(18,2),
        Dokument        nvarchar(150),
        Napomena        nvarchar(1000),
        PRIMARY KEY (IDNaimenovanja, IDRazduzenja));

    INSERT @r (IDNaimenovanja, IDRazduzenja, Datum, Koleta, Bruto, Neto, Vrednost, Dokument, Napomena)
    SELECT rs.IDNaimenovanja, r.ID,
           CAST(ISNULL(r.DatumIzdavanja, r.DatumUnosa) AS date),
           rs.Koleta, rs.Bruto, rs.Neto, rs.Vrednost,
           CASE WHEN RTRIM(r.CI) <> N'' THEN RTRIM(r.CI) + N'/' ELSE N'' END
             + RTRIM(r.Vrsta) + N'/' + RTRIM(r.Broj) + N'/' + CONVERT(nvarchar(4), r.Godina),
           ISNULL(o.Otpremnice, N'') + CASE WHEN o.Otpremnice IS NOT NULL AND o.Napomena IS NOT NULL THEN N'; ' ELSE N'' END
             + ISNULL(o.Napomena, N'')
    FROM dbo.RNCarinskoRazduzenjeStavka rs
    JOIN dbo.RNCarinskoRazduzenje r ON r.ID = rs.IDRazduzenja
    CROSS APPLY (SELECT N'Otpremnica ' + STUFF((SELECT N', ' + CONVERT(nvarchar(10), x.IDOtpremnice)
                                                FROM dbo.RNCarinskoOtpremnicaRazduzenje x
                                                WHERE x.IDRazduzenja = r.ID
                                                ORDER BY x.IDOtpremnice
                                                FOR XML PATH(''), TYPE).value('.', 'nvarchar(max)'), 1, 2, N'') AS Otpremnice,
                        NULLIF(RTRIM(r.Napomena), N'') AS Napomena) o
    WHERE rs.IDNaimenovanja IN (SELECT IDNaimenovanja FROM @n);

    SELECT x.VrstaReda, x.IDNaimenovanja, x.IDRazduzenja, x.MagacinskiBroj, x.Godina,
           x.Rbr, x.Datum, x.CIPrijave, x.DokumentPrijave, x.RBNaimenovanja,
           x.Koleta, x.Tezina, x.NerazduzenoKoleta, x.NerazduzenaTezina,
           x.VrstaRobe, x.Vrednost, x.Valuta, x.DokumentRazduzenja, x.DatumIzdavanja, x.Napomena, x.Sektor,
           x.NHM, x.Neto, x.NerazduzenoNeto, x.NerazduzenaVrednost
    FROM (
        -- usklađištenje
        SELECT 1 AS VrstaReda, n.IDNaimenovanja, 0 AS IDRazduzenja, n.MagacinskiBroj, n.Godina,
               n.Rbr, n.Datum, n.CI AS CIPrijave, n.Dokument AS DokumentPrijave, n.RB AS RBNaimenovanja,
               n.Koleta, n.Bruto AS Tezina, n.Koleta AS NerazduzenoKoleta, n.Bruto AS NerazduzenaTezina,
               n.Naimenovanje + ISNULL(N' ' + NULLIF(n.Kontejneri, N''), N'') + CASE WHEN n.BrojC7 <> N'' THEN N' C7-' + n.BrojC7 ELSE N'' END AS VrstaRobe,
               n.Vrednost, n.Valuta, N'' AS DokumentRazduzenja, CAST(NULL AS date) AS DatumIzdavanja,
               ISNULL(n.Vlasnik, N'') AS Napomena, N'' AS Sektor,
               n.NHM, n.Neto, n.Neto AS NerazduzenoNeto, n.Vrednost AS NerazduzenaVrednost,
               n.Datum AS DatumReda
        FROM @n n
        UNION ALL
        -- razduženja, sa tekućim stanjem naimenovanja posle reda (redosled: datum izdavanja, pa dokument)
        SELECT 2, n.IDNaimenovanja, r.IDRazduzenja, n.MagacinskiBroj, n.Godina,
               n.Rbr, CAST(NULL AS date), n.CI, n.Dokument, n.RB,
               -r.Koleta, -r.Bruto, n.Koleta - z.Koleta, n.Bruto - z.Bruto,
               n.Naimenovanje + ISNULL(N' ' + NULLIF(n.Kontejneri, N''), N'') + CASE WHEN n.BrojC7 <> N'' THEN N' C7-' + n.BrojC7 ELSE N'' END,
               -r.Vrednost, n.Valuta, r.Dokument, r.Datum, r.Napomena, N'',
               n.NHM, -r.Neto, n.Neto - z.Neto, n.Vrednost - z.Vrednost,
               r.Datum
        FROM @n n
        JOIN @r r ON r.IDNaimenovanja = n.IDNaimenovanja
        CROSS APPLY (SELECT SUM(r2.Koleta) AS Koleta, SUM(r2.Bruto) AS Bruto, SUM(r2.Neto) AS Neto, SUM(r2.Vrednost) AS Vrednost
                     FROM @r r2
                     WHERE r2.IDNaimenovanja = r.IDNaimenovanja
                       AND (r2.Datum < r.Datum OR (r2.Datum = r.Datum AND r2.IDRazduzenja <= r.IDRazduzenja))) z
    ) x
    WHERE (@Od IS NULL OR x.DatumReda >= @Od)
      AND (@Do IS NULL OR x.DatumReda <= @Do)
    ORDER BY x.Godina, CASE WHEN x.Rbr IS NULL THEN 1 ELSE 0 END, x.Rbr, x.RBNaimenovanja, x.IDNaimenovanja,
             x.VrstaReda, x.DatumReda, x.IDRazduzenja;
END
GO

PRINT 'Carina_Knjiga_2026_11.sql: završeno.';
GO
SET NOEXEC OFF;
GO
