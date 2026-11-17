/* =============================================================================================
   Brisanje podataka uvoza DEUS packing liste (test), BEZ klasifikacije.

   Briše:
     - RNCarinskoPackingLista i RNCarinskoPackingListaStavka za katalog DEUS
       (i uvoze bez kataloga, napravljene pre uvođenja kataloga, ako je @IUvoziBezKataloga = 1);
     - stavke prijemnice prenete iz tih packing listi (RNCarinskoPrijemnicaStavke.IDPLStavke),
       samo na prijemnicama koje nisu potvrđene (Status <> ZA);
       na potvrđenim prijemnicama stavke ostaju, samo se briše veza IDPLStavke;
     - privremene redove uvoza artikala (ArtikliKatalogUvoz).

   NE briše: klasifikaciju (ArtikliPrviNivo … ArtikliCetvrtiNivo), artikle (Artikli), kataloge,
   podešavanja, zaglavlja prijemnica.

   Upotreba: prvo pustiti sa @Obrisi = 0 (samo prikaz), proveriti spisak, pa pustiti sa @Obrisi = 1.
   ============================================================================================= */
SET NOCOUNT ON;
SET XACT_ABORT ON;

DECLARE @Obrisi            bit          = 0;        -- 0 = samo prikaz, 1 = obriši
DECLARE @Katalog           nvarchar(30) = N'DEUS';
DECLARE @IUvoziBezKataloga bit          = 1;        -- i packing liste uvezene pre uvođenja kataloga (Katalog NULL)

SELECT pl.ID INTO #pl
FROM dbo.RNCarinskoPackingLista pl
WHERE pl.Katalog = @Katalog OR (@IUvoziBezKataloga = 1 AND pl.Katalog IS NULL);

PRINT '--- Packing liste koje se brišu ---';
SELECT pl.ID, pl.IDPrijemnice, pl.NazivFajla, pl.DatumUvoza, pl.Korisnik, pl.Status, pl.Katalog,
       (SELECT COUNT(*) FROM dbo.RNCarinskoPackingListaStavka s WHERE s.IDPackingListe = pl.ID) AS Redova
FROM dbo.RNCarinskoPackingLista pl
WHERE pl.ID IN (SELECT ID FROM #pl)
ORDER BY pl.ID;

PRINT '--- Stavke prijemnica prenete iz tih packing listi ---';
SELECT st.ID, st.IDNadredjena AS Prijemnica, pr.Status AS StatusPrijemnice, st.RB, st.Naziv, st.Koleta,
       CASE WHEN RTRIM(ISNULL(pr.Status, N'')) = N'ZA' THEN N'ostaje (potvrđena), briše se samo veza' ELSE N'briše se' END AS Akcija
FROM dbo.RNCarinskoPrijemnicaStavke st
JOIN dbo.RNCarinskoSkladistePrijemnica pr ON pr.ID = st.IDNadredjena
WHERE st.IDPLStavke IN (SELECT s.ID FROM dbo.RNCarinskoPackingListaStavka s WHERE s.IDPackingListe IN (SELECT ID FROM #pl))
ORDER BY st.IDNadredjena, st.RB;

IF @Obrisi = 1
BEGIN
    BEGIN TRAN;

    -- stavke prijemnica iz packing liste: nepotvrđene se brišu, potvrđenima se briše samo veza
    DELETE st
    FROM dbo.RNCarinskoPrijemnicaStavke st
    JOIN dbo.RNCarinskoSkladistePrijemnica pr ON pr.ID = st.IDNadredjena
    WHERE RTRIM(ISNULL(pr.Status, N'')) <> N'ZA'
      AND st.IDPLStavke IN (SELECT s.ID FROM dbo.RNCarinskoPackingListaStavka s WHERE s.IDPackingListe IN (SELECT ID FROM #pl));
    PRINT 'Obrisano stavki prijemnica: ' + CONVERT(varchar(10), @@ROWCOUNT);

    UPDATE dbo.RNCarinskoPrijemnicaStavke SET IDPLStavke = NULL, KoletaPL = NULL
    WHERE IDPLStavke IN (SELECT s.ID FROM dbo.RNCarinskoPackingListaStavka s WHERE s.IDPackingListe IN (SELECT ID FROM #pl));
    PRINT 'Stavki potvrđenih prijemnica bez veze sa packing listom: ' + CONVERT(varchar(10), @@ROWCOUNT);

    DELETE FROM dbo.RNCarinskoPackingListaStavka WHERE IDPackingListe IN (SELECT ID FROM #pl);
    PRINT 'Obrisano redova packing liste: ' + CONVERT(varchar(10), @@ROWCOUNT);

    DELETE FROM dbo.RNCarinskoPackingLista WHERE ID IN (SELECT ID FROM #pl);
    PRINT 'Obrisano packing listi: ' + CONVERT(varchar(10), @@ROWCOUNT);

    DELETE FROM dbo.ArtikliKatalogUvoz;
    PRINT 'Obrisano privremenih redova uvoza artikala: ' + CONVERT(varchar(10), @@ROWCOUNT);

    COMMIT;
    PRINT 'Gotovo. Klasifikacija i artikli nisu dirani.';
END
ELSE
    PRINT 'Samo prikaz (@Obrisi = 0). Za brisanje postavite @Obrisi = 1 i pustite ponovo.';

DROP TABLE #pl;
