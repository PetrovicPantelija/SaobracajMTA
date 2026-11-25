/* =============================================================================================
   Nalog za prijem, magacinski predmet (25.11.2026.)
     9. lista valuta: dodaje se USD
     8. jedinice mere (MerskeEnote): dodaju se one koje ne postoje

   Skripta se može pokretati više puta: red se dodaje samo ako ne postoji.
   Postojeće tabele: Valute, MerskeEnote (samo se dodaju redovi; postojeći se ne menjaju).
   Na kraju se prikazuju obe tabele, da se proveri šta je dodato.
   ============================================================================================= */
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

BEGIN TRANSACTION;

/* ---------------------------------------------------------------------------------------------
   1. Valute: USD
      Šifra prati EUR: ako je EUR upisan kao 978 (numerički kod), USD dobija 840, inače USD.
      Naziv je USD (u stavkama se čuva naziv valute, kao EURO).
   --------------------------------------------------------------------------------------------- */
DECLARE @SifraEUR nvarchar(20) =
    (SELECT TOP 1 RTRIM(CONVERT(nvarchar(20), VaSifra)) FROM dbo.Valute
     WHERE RTRIM(CONVERT(nvarchar(50), VaNaziv)) IN (N'EURO', N'EUR') OR RTRIM(CONVERT(nvarchar(20), VaSifra)) IN (N'EUR', N'978'));

IF NOT EXISTS (SELECT 1 FROM dbo.Valute
               WHERE RTRIM(CONVERT(nvarchar(20), VaSifra)) IN (N'USD', N'840')
                  OR RTRIM(CONVERT(nvarchar(50), VaNaziv)) IN (N'USD', N'US DOLAR', N'DOLAR', N'AMERIČKI DOLAR'))
    INSERT INTO dbo.Valute (VaSifra, VaNaziv)
    VALUES (CASE WHEN @SifraEUR = N'978' THEN N'840' ELSE N'USD' END, N'USD');

/* ---------------------------------------------------------------------------------------------
   2. MerskeEnote: jedinice mere koje se koriste u carinskim stavkama
      Spisak se može dopuniti. Dodaju se samo šifre koje ne postoje i koje staju u kolonu MeSifra;
      naziv se upisuje ako tabela ima kolonu MeNaziv.
   --------------------------------------------------------------------------------------------- */
IF OBJECT_ID('tempdb..#JM') IS NOT NULL DROP TABLE #JM;
CREATE TABLE #JM (Sifra nvarchar(10) COLLATE DATABASE_DEFAULT, Naziv nvarchar(50) COLLATE DATABASE_DEFAULT);
INSERT INTO #JM (Sifra, Naziv) VALUES
    (N'KOM', N'Komad'),        (N'KG',  N'Kilogram'),    (N'G',   N'Gram'),
    (N'T',   N'Tona'),         (N'L',   N'Litar'),       (N'M',   N'Metar'),
    (N'M2',  N'Kvadratni metar'), (N'M3', N'Kubni metar'), (N'PAR', N'Par'),
    (N'KPL', N'Komplet'),      (N'SET', N'Set'),         (N'PAK', N'Paket'),
    (N'KUT', N'Kutija'),       (N'KOL', N'Koleto'),      (N'PAL', N'Paleta'),
    (N'ROL', N'Rolna'),        (N'KAR', N'Karton'),      (N'BAL', N'Bala'),
    (N'VRE', N'Vreća'),        (N'BUR', N'Bure'),        (N'KAN', N'Kanister'),
    (N'BOC', N'Boca'),         (N'KWH', N'Kilovat-čas');

DECLARE @DuzinaSifre int, @DuzinaNaziva int;
SELECT @DuzinaSifre = CASE WHEN c.max_length = -1 THEN 4000
                           WHEN TYPE_NAME(c.user_type_id) IN ('nvarchar', 'nchar') THEN c.max_length / 2
                           ELSE c.max_length END
FROM sys.columns c WHERE c.object_id = OBJECT_ID('dbo.MerskeEnote') AND c.name = 'MeSifra';
SELECT @DuzinaNaziva = CASE WHEN c.max_length = -1 THEN 4000
                            WHEN TYPE_NAME(c.user_type_id) IN ('nvarchar', 'nchar') THEN c.max_length / 2
                            ELSE c.max_length END
FROM sys.columns c WHERE c.object_id = OBJECT_ID('dbo.MerskeEnote') AND c.name = 'MeNaziv';

DELETE j FROM #JM j
WHERE LEN(j.Sifra) > ISNULL(@DuzinaSifre, 0)
   OR EXISTS (SELECT 1 FROM dbo.MerskeEnote m WHERE RTRIM(m.MeSifra) COLLATE DATABASE_DEFAULT = j.Sifra);

IF @DuzinaNaziva IS NOT NULL
    EXEC sp_executesql N'INSERT INTO dbo.MerskeEnote (MeSifra, MeNaziv) SELECT Sifra, LEFT(Naziv, @Duzina) FROM #JM;',
                       N'@Duzina int', @Duzina = @DuzinaNaziva;
ELSE
    INSERT INTO dbo.MerskeEnote (MeSifra) SELECT Sifra FROM #JM;

DROP TABLE #JM;

COMMIT TRANSACTION;
GO

/* Provera */
SELECT * FROM dbo.Valute;
SELECT * FROM dbo.MerskeEnote ORDER BY MeSifra;
GO
