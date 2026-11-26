/* =============================================================================================
   Carina: raspodela stavki prijemnice po kontejnerima (RAZ-08)
   Predlog: Carina-Izlaz-Tarife-Vlasnik-Kontejneri-Knjiga-Predlog.md, verzija 1.1, 26.11.2026.

   Jedan red = koliko koleta jedne stavke prijemnice je stiglo u jednom kontejneru.
   Primer SC1 (Vamaco): 3 artikla (6 / 5 / 13 rolni) u 5 kontejnera (5 / 5 / 5 / 5 / 4).

   Razlika u odnosu na predlog: red ne upućuje na RNCarinskoKontejner.ID, nego na nalog i broj kontejnera
   (IDNaloga + BrojKontejnera). Nalog za PRIJEM pri svakom Snimi briše i ponovo upisuje svoje kontejnere,
   pa se njihov ID menja, a broj kontejnera ostaje isti. BrojKontejnera ima 20 znakova, da kasnije primi
   i oznaku vagona ili kamiona (RAZ-10).

   Postojeće tabele: RNCarinskoPackingListaStavka dobija kolonu Kontejner (RAZ-09); ostale se ne menjaju. RNCarinskoPrijemnicaStavke.BrojKontejnera ostaje zbirni tekst za stare ekrane
   (program ga popunjava pri snimanju raspodele).
   Skripta se može pustiti više puta. Bez nje Prijemnica nema dugme „Raspodela po kontejnerima“.
   ============================================================================================= */
SET NOCOUNT ON;

IF OBJECT_ID('dbo.RNCarinskoStavkaKontejner', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.RNCarinskoStavkaKontejner
    (
        ID             int IDENTITY(1,1) NOT NULL CONSTRAINT PK_RNCarinskoStavkaKontejner PRIMARY KEY,
        IDPrijemnice   int            NOT NULL,          -- RNCarinskoSkladistePrijemnica.ID
        IDStavke       int            NOT NULL,          -- RNCarinskoPrijemnicaStavke.ID
        IDNaloga       int            NOT NULL,          -- RadniNalogSkladista.ID (nalog za prijem, isto kao RNCarinskoKontejner.IDNaloga)
        BrojKontejnera nvarchar(20)   NOT NULL,
        Koleta         int            NOT NULL,
        Neto           decimal(18,3)  NULL,
        Bruto          decimal(18,3)  NULL,
        Vrednost       decimal(18,2)  NULL,
        NameOper       nvarchar(50)   NULL,
        DatumUnosa     datetime       NOT NULL CONSTRAINT DF_RNCarinskoStavkaKontejner_DatumUnosa DEFAULT (GETDATE()),
        CONSTRAINT UQ_RNCarinskoStavkaKontejner UNIQUE (IDStavke, BrojKontejnera),
        CONSTRAINT CK_RNCarinskoStavkaKontejner_Koleta CHECK (Koleta > 0)
    );
    CREATE INDEX IX_RNCarinskoStavkaKontejner_Prijemnica ON dbo.RNCarinskoStavkaKontejner (IDPrijemnice);
    CREATE INDEX IX_RNCarinskoStavkaKontejner_Kontejner ON dbo.RNCarinskoStavkaKontejner (IDNaloga, BrojKontejnera);
    PRINT 'Napravljena tabela dbo.RNCarinskoStavkaKontejner';
END
ELSE
    PRINT 'Tabela dbo.RNCarinskoStavkaKontejner već postoji';
GO

/* RAZ-09: kontejner iz packing liste (opciona kolona fajla: Kontejner, Container, Container No, Cont., Broj kontejnera).
   Iz nje Prijemnica predlaže raspodelu po kontejnerima. Uvozi bez te kolone rade kao do sada. */
IF OBJECT_ID('dbo.RNCarinskoPackingListaStavka', 'U') IS NULL
    PRINT 'Tabela dbo.RNCarinskoPackingListaStavka ne postoji (Carina_PrijemnicaPL_2026_11.sql nije puštena), kolona Kontejner se ne dodaje.';
ELSE IF COL_LENGTH('dbo.RNCarinskoPackingListaStavka', 'Kontejner') IS NULL
BEGIN
    ALTER TABLE dbo.RNCarinskoPackingListaStavka ADD Kontejner nvarchar(20) NULL;
    PRINT 'Dodata kolona dbo.RNCarinskoPackingListaStavka.Kontejner';
END
ELSE
    PRINT 'Kolona dbo.RNCarinskoPackingListaStavka.Kontejner već postoji';
GO

/* Provera: raspodela čije stavke više nema (stavka obrisana na Prijemnici); program ih briše pri sledećem snimanju raspodele */
SELECT rk.IDPrijemnice, rk.IDStavke, rk.BrojKontejnera, rk.Koleta
FROM dbo.RNCarinskoStavkaKontejner rk
WHERE NOT EXISTS (SELECT 1 FROM dbo.RNCarinskoPrijemnicaStavke s WHERE s.ID = rk.IDStavke)
ORDER BY rk.IDPrijemnice, rk.IDStavke;
GO
