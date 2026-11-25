/* Carina – tabela kontejnera na magacinskom predmetu (nalog za prijem)
   25.11.2026.
   Kada je vrsta prevoznog sredstva Kontejner, nalog dobija tabelu kontejnera iz kojih stiže roba.
   Zbirna lista ostaje i u RadniNalogSkladista.BrojKontejneraPrijem (čitaju je Prijemnica, Otprema i drugi ekrani).
   Skripta se može pustiti više puta. */

SET NOCOUNT ON;

IF OBJECT_ID('dbo.RNCarinskoKontejner', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.RNCarinskoKontejner
    (
        ID             int IDENTITY(1,1) NOT NULL CONSTRAINT PK_RNCarinskoKontejner PRIMARY KEY,
        IDNaloga       int            NOT NULL,          -- RadniNalogSkladista.ID
        RB             smallint       NOT NULL,
        BrojKontejnera nvarchar(11)   NOT NULL,          -- 4 slova + 7 cifara, npr. MSDU1184432
        Koleta         int            NULL,
        Bruto          decimal(18,3)  NULL,
        Napomena       nvarchar(200)  NULL,
        NameOper       nvarchar(50)   NULL,
        DatumUnosa     datetime       NOT NULL CONSTRAINT DF_RNCarinskoKontejner_DatumUnosa DEFAULT (GETDATE()),
        CONSTRAINT UQ_RNCarinskoKontejner_Broj UNIQUE (IDNaloga, BrojKontejnera),
        CONSTRAINT CK_RNCarinskoKontejner_Broj CHECK (BrojKontejnera LIKE '[A-Z][A-Z][A-Z][A-Z][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
        CONSTRAINT CK_RNCarinskoKontejner_Koleta CHECK (Koleta IS NULL OR Koleta >= 0),
        CONSTRAINT CK_RNCarinskoKontejner_Bruto CHECK (Bruto IS NULL OR Bruto >= 0)
    );
    CREATE INDEX IX_RNCarinskoKontejner_Broj ON dbo.RNCarinskoKontejner (BrojKontejnera);
    PRINT 'Napravljena tabela dbo.RNCarinskoKontejner';
END
ELSE
    PRINT 'Tabela dbo.RNCarinskoKontejner već postoji';
GO

/* Prenos postojećih brojeva iz zbirnog polja (samo nalozi koji još nemaju redove u tabeli).
   Prenose se samo delovi koji liče na broj kontejnera (4 slova + 7 cifara). */
;WITH Delovi AS
(
    SELECT rns.ID AS IDNaloga,
           UPPER(LTRIM(RTRIM(REPLACE(REPLACE(s.value, ' ', ''), '-', '')))) AS Broj
    FROM dbo.RadniNalogSkladista rns
    CROSS APPLY STRING_SPLIT(REPLACE(REPLACE(rns.BrojKontejneraPrijem, ';', ','), '/', ','), ',') s
    WHERE rns.BrojKontejneraPrijem IS NOT NULL AND LTRIM(RTRIM(rns.BrojKontejneraPrijem)) <> ''
      AND NOT EXISTS (SELECT 1 FROM dbo.RNCarinskoKontejner k WHERE k.IDNaloga = rns.ID)
),
Ispravni AS
(
    SELECT DISTINCT IDNaloga, Broj
    FROM Delovi
    WHERE Broj LIKE '[A-Z][A-Z][A-Z][A-Z][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'
)
INSERT INTO dbo.RNCarinskoKontejner (IDNaloga, RB, BrojKontejnera, NameOper)
SELECT IDNaloga,
       ROW_NUMBER() OVER (PARTITION BY IDNaloga ORDER BY Broj),
       Broj,
       'prenos 2026-11'
FROM Ispravni;

PRINT 'Preneto redova iz BrojKontejneraPrijem: ' + CAST(@@ROWCOUNT AS varchar(10));
GO

/* Provera */
SELECT TOP 20 k.IDNaloga, k.RB, k.BrojKontejnera, k.Koleta, k.Bruto, rns.BrojKontejneraPrijem
FROM dbo.RNCarinskoKontejner k
INNER JOIN dbo.RadniNalogSkladista rns ON rns.ID = k.IDNaloga
ORDER BY k.IDNaloga DESC, k.RB;
