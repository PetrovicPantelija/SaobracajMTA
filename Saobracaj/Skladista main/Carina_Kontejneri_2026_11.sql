/* Carina – tabela kontejnera na magacinskom predmetu (nalog za prijem)
   25.11.2026.
   Kada je vrsta prevoznog sredstva Kontejner, nalog dobija tabelu kontejnera iz kojih stiže roba.
   Zbirna lista ostaje i u RadniNalogSkladista.BrojKontejneraPrijem (čitaju je Prijemnica, Otprema i drugi ekrani).
   Skripta se može pustiti više puta. */

SET NOCOUNT ON;
SET QUOTED_IDENTIFIER ON;   -- potrebno za XML metode (.nodes, .value) u prenosu ispod
SET ANSI_NULLS ON;

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
   Prenose se samo delovi koji liče na broj kontejnera (4 slova + 7 cifara).
   Podela po , ; / ide preko XML-a, ne STRING_SPLIT (baza može imati nivo kompatibilnosti ispod 130);
   znakovi & < > se pretvaraju u separator, jer bi pokvarili XML, a nisu deo broja kontejnera. */
;WITH Izvor AS
(
    SELECT rns.ID AS IDNaloga,
           CAST(N'<x>' + REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(
               CONVERT(nvarchar(max), rns.BrojKontejneraPrijem),
               N'&', N','), N'<', N','), N'>', N','), N';', N','), N'/', N','),
               N',', N'</x><x>') + N'</x>' AS xml) AS X
    FROM dbo.RadniNalogSkladista rns
    WHERE rns.BrojKontejneraPrijem IS NOT NULL AND LTRIM(RTRIM(rns.BrojKontejneraPrijem)) <> ''
      AND NOT EXISTS (SELECT 1 FROM dbo.RNCarinskoKontejner k WHERE k.IDNaloga = rns.ID)
),
Delovi AS
(
    SELECT i.IDNaloga,
           UPPER(LTRIM(RTRIM(REPLACE(REPLACE(t.c.value('.', 'nvarchar(200)'), ' ', ''), '-', '')))) AS Broj
    FROM Izvor i
    CROSS APPLY i.X.nodes('/x') t(c)
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
