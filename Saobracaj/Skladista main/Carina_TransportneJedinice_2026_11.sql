/* =============================================================================================
   Carina: transportne jedinice na nalogu za prijem – kontejner, vagon, kamion (RAZ-10)
   Predlog: Carina-Izlaz-Tarife-Vlasnik-Kontejneri-Knjiga-Predlog.md, verzija 1.1, 26.11.2026.

   Tabela RNCarinskoKontejner (Carina_Kontejneri_2026_11.sql) postaje tabela transportnih jedinica:
     - nova kolona Tip: KONTEJNER, VAGON ili KAMION (postojeći redovi su KONTEJNER);
     - BrojKontejnera prima 20 znakova (broj vagona, registracija kamiona);
     - provera 4 slova + 7 cifara (ISO 6346) važi samo za tip KONTEJNER.
   Naziv tabele i kolone se ne menja, jer ih čitaju Prijemnica, raspodela po kontejnerima i palete.

   Skripta se može pustiti više puta. Pre nje se vagon i kamion ne mogu upisati u tabelu (provera broja kontejnera).
   ============================================================================================= */
SET NOCOUNT ON;
SET XACT_ABORT ON;

IF OBJECT_ID('dbo.RNCarinskoKontejner', 'U') IS NULL
BEGIN
    PRINT 'GREŠKA: tabela dbo.RNCarinskoKontejner ne postoji. Prvo pustiti Carina_Kontejneri_2026_11.sql.';
    SET NOEXEC ON;
END
GO

IF COL_LENGTH('dbo.RNCarinskoKontejner', 'Tip') IS NULL
BEGIN
    ALTER TABLE dbo.RNCarinskoKontejner
        ADD Tip nvarchar(10) NOT NULL CONSTRAINT DF_RNCarinskoKontejner_Tip DEFAULT (N'KONTEJNER');
    PRINT 'Dodata kolona dbo.RNCarinskoKontejner.Tip (postojeći redovi: KONTEJNER).';
END
ELSE
    PRINT 'Kolona dbo.RNCarinskoKontejner.Tip već postoji.';
GO

IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_RNCarinskoKontejner_Tip')
BEGIN
    ALTER TABLE dbo.RNCarinskoKontejner
        ADD CONSTRAINT CK_RNCarinskoKontejner_Tip CHECK (Tip IN (N'KONTEJNER', N'VAGON', N'KAMION'));
    PRINT 'Dodata provera CK_RNCarinskoKontejner_Tip.';
END
GO

/* BrojKontejnera: 11 -> 20 znakova; ključ, indeks i provera broja se uklanjaju i prave ponovo, a provera
   4 slova + 7 cifara važi samo za kontejner */
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.RNCarinskoKontejner') AND name = 'BrojKontejnera' AND max_length < 40)
BEGIN
    BEGIN TRAN;

    IF EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_RNCarinskoKontejner_Broj')
        ALTER TABLE dbo.RNCarinskoKontejner DROP CONSTRAINT CK_RNCarinskoKontejner_Broj;
    IF EXISTS (SELECT 1 FROM sys.key_constraints WHERE name = 'UQ_RNCarinskoKontejner_Broj')
        ALTER TABLE dbo.RNCarinskoKontejner DROP CONSTRAINT UQ_RNCarinskoKontejner_Broj;
    IF EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID('dbo.RNCarinskoKontejner') AND name = 'IX_RNCarinskoKontejner_Broj')
        DROP INDEX IX_RNCarinskoKontejner_Broj ON dbo.RNCarinskoKontejner;

    ALTER TABLE dbo.RNCarinskoKontejner ALTER COLUMN BrojKontejnera nvarchar(20) NOT NULL;

    ALTER TABLE dbo.RNCarinskoKontejner ADD CONSTRAINT UQ_RNCarinskoKontejner_Broj UNIQUE (IDNaloga, BrojKontejnera);
    CREATE INDEX IX_RNCarinskoKontejner_Broj ON dbo.RNCarinskoKontejner (BrojKontejnera);

    COMMIT;
    PRINT 'dbo.RNCarinskoKontejner.BrojKontejnera: nvarchar(20).';
END
ELSE
    PRINT 'dbo.RNCarinskoKontejner.BrojKontejnera već prima 20 znakova.';
GO

IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_RNCarinskoKontejner_Broj')
BEGIN
    EXEC('ALTER TABLE dbo.RNCarinskoKontejner ADD CONSTRAINT CK_RNCarinskoKontejner_Broj CHECK
          (Tip <> N''KONTEJNER'' OR BrojKontejnera LIKE ''[A-Z][A-Z][A-Z][A-Z][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'');');
    PRINT 'Provera CK_RNCarinskoKontejner_Broj: 4 slova + 7 cifara samo za kontejner.';
END
GO

/* Stanje: broj transportnih jedinica po tipu */
SELECT Tip, COUNT(*) AS Broj FROM dbo.RNCarinskoKontejner GROUP BY Tip ORDER BY Tip;
GO

SET NOEXEC OFF;
GO
