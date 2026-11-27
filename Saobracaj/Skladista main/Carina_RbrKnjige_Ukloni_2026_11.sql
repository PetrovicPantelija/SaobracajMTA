/* =============================================================================================
   Carina: ukidanje posebnog rednog broja u carinskoj knjizi (MagacinskiPredmet.RbrKnjige, RAZ-17)
   27.11.2026.

   Magacinski broj je redni broj u carinskoj knjizi, pa je polje „Rbr u carinskoj knjizi“ na Nalogu za PRIJEM bilo
   dupliranje. Polje je uklonjeno iz programa, a carinska knjiga (dbo.sp_CarinskaKnjiga) za kolonu 1 koristi
   magacinski broj. Ova skripta uklanja kolonu iz baze (umesto skripte Carina_RbrKnjige_2026_11.sql, koja je dodala kolonu).

   Šta radi:
     1. Ispisuje magacinske predmete na kojima je uneti redni broj različit od magacinskog broja.
     2. Ako takvih nema (ili je @Obrisi = 1), briše proveru CK_MagacinskiPredmet_RbrKnjige i kolonu RbrKnjige.
        Ako ih ima, ne menja ništa: pregledati spisak, pa pustiti ponovo sa @Obrisi = 1.

   Posle ove skripte pustiti Carina_Knjiga_2026_11.sql (procedura više ne čita RbrKnjige).
   Skripta se može pustiti više puta.
   ============================================================================================= */
SET NOCOUNT ON;

DECLARE @Obrisi bit = 0;   -- 1 = obriši kolonu i kad postoje redni brojevi različiti od magacinskog broja

IF COL_LENGTH('dbo.MagacinskiPredmet', 'RbrKnjige') IS NULL
BEGIN
    PRINT 'Kolona dbo.MagacinskiPredmet.RbrKnjige ne postoji; nema šta da se uklanja.';
    RETURN;
END

DECLARE @Razliciti int;
EXEC sp_executesql N'
    SELECT @n = COUNT(*)
    FROM dbo.MagacinskiPredmet mp
    LEFT JOIN dbo.RadniNalogSkladista rns ON rns.ID = mp.RN
    WHERE mp.RbrKnjige IS NOT NULL AND mp.RbrKnjige <> ISNULL(rns.MagacinskiBroj, mp.MB);',
    N'@n int OUTPUT', @n = @Razliciti OUTPUT;

IF @Razliciti > 0
    EXEC(N'
    SELECT mp.ID AS [Magacinski predmet], mp.RN AS Nalog, mp.Godina,
           ISNULL(rns.MagacinskiBroj, mp.MB) AS [Magacinski broj], mp.RbrKnjige AS [Rbr u knjizi (ukida se)]
    FROM dbo.MagacinskiPredmet mp
    LEFT JOIN dbo.RadniNalogSkladista rns ON rns.ID = mp.RN
    WHERE mp.RbrKnjige IS NOT NULL AND mp.RbrKnjige <> ISNULL(rns.MagacinskiBroj, mp.MB)
    ORDER BY mp.Godina, mp.RN;');

IF @Razliciti > 0 AND @Obrisi = 0
BEGIN
    PRINT 'Postoji ' + CONVERT(varchar(10), @Razliciti) + ' magacinskih predmeta sa rednim brojem različitim od magacinskog broja (spisak iznad).';
    PRINT 'Ništa nije promenjeno. Posle pregleda pustiti ponovo sa @Obrisi = 1.';
    RETURN;
END

IF EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_MagacinskiPredmet_RbrKnjige')
BEGIN
    ALTER TABLE dbo.MagacinskiPredmet DROP CONSTRAINT CK_MagacinskiPredmet_RbrKnjige;
    PRINT 'Obrisana provera CK_MagacinskiPredmet_RbrKnjige.';
END

ALTER TABLE dbo.MagacinskiPredmet DROP COLUMN RbrKnjige;
PRINT 'Obrisana kolona dbo.MagacinskiPredmet.RbrKnjige. Sada pustiti Carina_Knjiga_2026_11.sql.';
