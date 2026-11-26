/* =============================================================================================
   Carina: redni broj iz carinske knjige na magacinskom predmetu (RAZ-17)
   Predlog: Carina-Izlaz-Tarife-Vlasnik-Kontejneri-Knjiga-Predlog.md, verzija 1.1, 26.11.2026.

   Pravila (potvrdio korisnik 26.11.2026):
     - brojač u knjizi je godišnji, po carinskom skladištu;
     - svaki C7 dobija svoj magacinski broj, pa jedan magacinski predmet = jedan C7 = jedan red usklađištenja u knjizi.
   Redni broj se čita iz polja 49 C7 (42072/1008/690/03.04.2025 -> 690). Godina je MagacinskiPredmet.Godina,
   skladište je MagacinskiPredmet.SifraSkladista (ako je prazno, RadniNalogSkladista.CarinskoSkladiste).

   Šta radi:
     1. MagacinskiPredmet dobija kolonu RbrKnjige int NULL.
     2. Ispisuje redne brojeve koji se ponavljaju u istoj godini i skladištu (ručna ispravka).
   Jedinstvenost proverava program pri Snimi. Jedinstveni filtrirani indeks se namerno ne pravi: traži
   SET QUOTED_IDENTIFIER ON u svakoj proceduri koja menja MagacinskiPredmet, a te procedure nisu proverene.

   Skripta se može pustiti više puta. Bez nje je polje „Rbr u carinskoj knjizi“ na Nalogu za PRIJEM zaključano.
   ============================================================================================= */
SET NOCOUNT ON;

IF OBJECT_ID('dbo.MagacinskiPredmet', 'U') IS NULL
    PRINT 'GREŠKA: tabela dbo.MagacinskiPredmet ne postoji.';
ELSE IF COL_LENGTH('dbo.MagacinskiPredmet', 'RbrKnjige') IS NULL
BEGIN
    ALTER TABLE dbo.MagacinskiPredmet ADD RbrKnjige int NULL;
    PRINT 'Dodata kolona dbo.MagacinskiPredmet.RbrKnjige.';
END
ELSE
    PRINT 'Kolona dbo.MagacinskiPredmet.RbrKnjige već postoji.';
GO

IF COL_LENGTH('dbo.MagacinskiPredmet', 'RbrKnjige') IS NOT NULL
   AND NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_MagacinskiPredmet_RbrKnjige')
BEGIN
    EXEC('ALTER TABLE dbo.MagacinskiPredmet ADD CONSTRAINT CK_MagacinskiPredmet_RbrKnjige CHECK (RbrKnjige IS NULL OR RbrKnjige > 0);');
    PRINT 'Dodata provera CK_MagacinskiPredmet_RbrKnjige (redni broj veći od 0).';
END
GO

/* Ručna provera: isti redni broj u istoj godini i skladištu */
IF COL_LENGTH('dbo.MagacinskiPredmet', 'RbrKnjige') IS NOT NULL
    EXEC('
    SELECT mp.Godina, RTRIM(CONVERT(nvarchar(50), ISNULL(mp.SifraSkladista, rns.CarinskoSkladiste))) AS Skladiste,
           mp.RbrKnjige, COUNT(*) AS Puta, MIN(mp.RN) AS [Prvi nalog], MAX(mp.RN) AS [Poslednji nalog]
    FROM dbo.MagacinskiPredmet mp
    JOIN dbo.RadniNalogSkladista rns ON rns.ID = mp.RN
    WHERE mp.RbrKnjige IS NOT NULL
    GROUP BY mp.Godina, RTRIM(CONVERT(nvarchar(50), ISNULL(mp.SifraSkladista, rns.CarinskoSkladiste))), mp.RbrKnjige
    HAVING COUNT(*) > 1
    ORDER BY mp.Godina, Skladiste, mp.RbrKnjige;');
GO
