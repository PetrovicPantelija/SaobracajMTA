/* =============================================================================================
   Carinski prijem na Prijemnici: uvoz packing liste, artikli, uparivanje, koleta
   Specifikacija: Carina-Prijemnica-Uvoz-Specifikacija.md (verzija 0.3, 16.11.2026.)

   Skripta se može pokretati više puta: kolone i tabele se dodaju samo ako ne postoje,
   procedure se prave sa CREATE OR ALTER, podešavanja se dodaju samo ako ključ ne postoji.
   Dok skripta nije puštena, Prijemnica radi kao do sada (program proverava kolonu IDNaimenovanja).

   Postojeće tabele:
     RNCarinskoPrijemnicaStavke   nove kolone IDNaimenovanja, KoletaPL, KolicinaJM, IDPLStavke;
                                  PDV i Carina prelaze na decimal(18,2) ako su celobrojni
     Artikli                      nova kolona Katalog (postojeći artikli: OSTALI), ključ Šifra + Katalog
     Artikli, ArtikliPrviNivo..
     ArtikliCetvrtiNivo           samo se dodaju redovi (sp_PL_FormirajArtikle); postojeći se ne menjaju
     Podesavanja                  ključevi Carina.* (mapiranje kolona, tolerancija, korekcija, arhiva)
     RNCarinskoStavke, MagacinskiPredmet, RNCarinskoSkladistePrijemnica, RadniNalogSkladista,
     Promet, NHM                  samo čitanje

   Nove tabele: RNCarinskoPackingLista, RNCarinskoPackingListaStavka, RNCarinskoPrijemnicaKorekcija,
                ArtikliKatalog (DEUS, OSTALI), ArtikliKatalogUvoz (privremeni redovi uvoza artikala kataloga),
                RNCarinskoPaleta, RNCarinskoPaletaStavka, RNCarinskoPaletaBrojac (palete, poglavlje 10).

   Razlike u odnosu na specifikaciju:
     - Sirov red packing liste ima i kolone NHM, Koleta i Koeficijent (ako ih fajl ima) i
       KoletaObracun (Količina / koeficijent).
     - sp_PL_PrenesiUStavke ne upisuje stavke, nego vraća redove za prenos; stavke se trajno
       upisuju tek na Snimi (4.1, tačka 4).
     - FK sa IDNaimenovanja na RNCarinskoStavke se ne pravi (ključ te tabele nije proveren); dodat je indeks.
   ============================================================================================= */
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

/* ---------------------------------------------------------------------------------------------
   1. RNCarinskoPrijemnicaStavke: nove kolone
   --------------------------------------------------------------------------------------------- */
IF COL_LENGTH('dbo.RNCarinskoPrijemnicaStavke', 'IDNaimenovanja') IS NULL
    ALTER TABLE dbo.RNCarinskoPrijemnicaStavke ADD IDNaimenovanja int NULL;
IF COL_LENGTH('dbo.RNCarinskoPrijemnicaStavke', 'KoletaPL') IS NULL
    ALTER TABLE dbo.RNCarinskoPrijemnicaStavke ADD KoletaPL decimal(18,3) NULL;
IF COL_LENGTH('dbo.RNCarinskoPrijemnicaStavke', 'KolicinaJM') IS NULL
    ALTER TABLE dbo.RNCarinskoPrijemnicaStavke ADD KolicinaJM decimal(18,3) NULL;
IF COL_LENGTH('dbo.RNCarinskoPrijemnicaStavke', 'IDPLStavke') IS NULL
    ALTER TABLE dbo.RNCarinskoPrijemnicaStavke ADD IDPLStavke int NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID('dbo.RNCarinskoPrijemnicaStavke') AND name = 'IX_RNCarinskoPrijemnicaStavke_IDNaimenovanja')
    CREATE INDEX IX_RNCarinskoPrijemnicaStavke_IDNaimenovanja ON dbo.RNCarinskoPrijemnicaStavke (IDNaimenovanja);
GO

-- PDV i Carina: decimalni tip (do sada su se pri snimanju pretvarali u ceo broj)
DECLARE @sql nvarchar(max) = N'';
SELECT @sql = @sql + N'ALTER TABLE dbo.RNCarinskoPrijemnicaStavke ALTER COLUMN ' + QUOTENAME(c.name) + N' decimal(18,2) '
            + CASE WHEN c.is_nullable = 1 THEN N'NULL' ELSE N'NOT NULL' END + N'; '
FROM sys.columns c
JOIN sys.types t ON t.user_type_id = c.user_type_id
WHERE c.object_id = OBJECT_ID('dbo.RNCarinskoPrijemnicaStavke')
  AND c.name IN ('PDV', 'Carina')
  AND t.name IN ('int', 'smallint', 'tinyint', 'bigint');

IF @sql <> N''
BEGIN
    BEGIN TRY
        EXEC sp_executesql @sql;
        PRINT 'PDV i Carina su prebačeni na decimal(18,2).';
    END TRY
    BEGIN CATCH
        PRINT 'UPOZORENJE: PDV i Carina nisu prebačeni na decimal (' + ERROR_MESSAGE() + '). Ukloniti zavisnost (npr. DEFAULT) i ponoviti.';
    END CATCH
END
GO

/* ---------------------------------------------------------------------------------------------
   2. Packing lista: jedan uvoz i sirovi redovi iz fajla
   --------------------------------------------------------------------------------------------- */
IF OBJECT_ID('dbo.RNCarinskoPackingLista', 'U') IS NULL
    CREATE TABLE dbo.RNCarinskoPackingLista (
        ID           int IDENTITY(1,1) NOT NULL CONSTRAINT PK_RNCarinskoPackingLista PRIMARY KEY,
        IDPrijemnice int           NOT NULL,
        NazivFajla   nvarchar(260) NOT NULL,
        HashFajla    char(64)      NOT NULL,
        DatumUvoza   datetime      NOT NULL CONSTRAINT DF_RNCarinskoPackingLista_Datum DEFAULT (GETDATE()),
        Korisnik     nvarchar(50)  NULL,
        Status       nvarchar(20)  NOT NULL CONSTRAINT DF_RNCarinskoPackingLista_Status DEFAULT (N'Uvezeno'),
        CONSTRAINT CK_RNCarinskoPackingLista_Status CHECK (Status IN (N'Uvezeno', N'Preneto', N'Zamenjeno')));
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID('dbo.RNCarinskoPackingLista') AND name = 'IX_RNCarinskoPackingLista_Prijemnica')
    CREATE INDEX IX_RNCarinskoPackingLista_Prijemnica ON dbo.RNCarinskoPackingLista (IDPrijemnice, Status);
GO

IF OBJECT_ID('dbo.RNCarinskoPackingListaStavka', 'U') IS NULL
    CREATE TABLE dbo.RNCarinskoPackingListaStavka (
        ID             int IDENTITY(1,1) NOT NULL CONSTRAINT PK_RNCarinskoPackingListaStavka PRIMARY KEY,
        IDPackingListe int            NOT NULL CONSTRAINT FK_RNCarinskoPackingListaStavka_PL REFERENCES dbo.RNCarinskoPackingLista (ID),
        RedUFajlu      int            NOT NULL,
        Nivo1Sifra     nvarchar(50)   NULL,
        Nivo1Naziv     nvarchar(200)  NULL,
        Nivo2Sifra     nvarchar(50)   NULL,
        Nivo2Naziv     nvarchar(200)  NULL,
        Nivo3Sifra     nvarchar(50)   NULL,
        Nivo3Naziv     nvarchar(200)  NULL,
        Nivo4Sifra     nvarchar(50)   NULL,
        Nivo4Naziv     nvarchar(200)  NULL,
        Sifra          nvarchar(50)   NULL,
        Artikal        nvarchar(250)  NULL,
        JM             nvarchar(20)   NULL,
        Kolicina       decimal(18,3)  NULL,
        Koleta         decimal(18,3)  NULL,      -- ako fajl ima kolonu koleta
        Koeficijent    decimal(18,6)  NULL,      -- JM po koletu: iz fajla ili unet pri formiranju artikla
        Neto           decimal(18,3)  NULL,
        Bruto          decimal(18,3)  NULL,
        Vrednost       decimal(18,2)  NULL,
        Valuta         nvarchar(10)   NULL,
        NHM            nvarchar(20)   NULL,
        IDArtikla      int            NULL,      -- rešeno po šifri; NULL = nov artikal
        KoletaObracun  decimal(18,3)  NULL,      -- Količina / koeficijent
        StatusReda     nvarchar(20)   NOT NULL CONSTRAINT DF_RNCarinskoPackingListaStavka_Status DEFAULT (N'Ok'),
        Poruka         nvarchar(1000) NULL,
        CONSTRAINT CK_RNCarinskoPackingListaStavka_Status CHECK (StatusReda IN (N'Ok', N'NoviArtikal', N'Upozorenje', N'Greska')));
GO
-- Kolone DEUS packing liste: bar kod, kataloški broj i neto, bruto i cena po JM (ukupno = količina × vrednost po JM)
IF COL_LENGTH('dbo.RNCarinskoPackingListaStavka', 'BarKod') IS NULL
    ALTER TABLE dbo.RNCarinskoPackingListaStavka ADD BarKod nvarchar(50) NULL;
IF COL_LENGTH('dbo.RNCarinskoPackingListaStavka', 'KataloskiBroj') IS NULL
    ALTER TABLE dbo.RNCarinskoPackingListaStavka ADD KataloskiBroj nvarchar(50) NULL;
IF COL_LENGTH('dbo.RNCarinskoPackingListaStavka', 'NetoPoJM') IS NULL
    ALTER TABLE dbo.RNCarinskoPackingListaStavka ADD NetoPoJM decimal(28,12) NULL;
IF COL_LENGTH('dbo.RNCarinskoPackingListaStavka', 'BrutoPoJM') IS NULL
    ALTER TABLE dbo.RNCarinskoPackingListaStavka ADD BrutoPoJM decimal(28,12) NULL;
IF COL_LENGTH('dbo.RNCarinskoPackingListaStavka', 'CenaPoJM') IS NULL
    ALTER TABLE dbo.RNCarinskoPackingListaStavka ADD CenaPoJM decimal(28,12) NULL;
-- ISP-02: JM po paleti iz fajla (npr. 1/300 -> 300) i koleta po paleti = JM po paleti / JM po koletu (predlog za palete)
IF COL_LENGTH('dbo.RNCarinskoPackingListaStavka', 'JMPoPaleti') IS NULL
    ALTER TABLE dbo.RNCarinskoPackingListaStavka ADD JMPoPaleti decimal(18,6) NULL;
IF COL_LENGTH('dbo.RNCarinskoPackingListaStavka', 'KoletaPoPaleti') IS NULL
    ALTER TABLE dbo.RNCarinskoPackingListaStavka ADD KoletaPoPaleti int NULL;
-- ISP-01: list fajla iz koga je packing lista uvezena
IF COL_LENGTH('dbo.RNCarinskoPackingLista', 'NazivLista') IS NULL
    ALTER TABLE dbo.RNCarinskoPackingLista ADD NazivLista nvarchar(100) NULL;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID('dbo.RNCarinskoPackingListaStavka') AND name = 'IX_RNCarinskoPackingListaStavka_PL')
    CREATE INDEX IX_RNCarinskoPackingListaStavka_PL ON dbo.RNCarinskoPackingListaStavka (IDPackingListe, RedUFajlu);
GO

/* ---------------------------------------------------------------------------------------------
   2a. Katalog artikala (DEUS, OSTALI, ...): artikal je jedinstven po Šifra + Katalog.
   Postojeći artikli dobijaju katalog OSTALI. Katalog packing liste bira korisnik pri uvozu
   i pri formiranju artikala.
   --------------------------------------------------------------------------------------------- */
IF OBJECT_ID('dbo.ArtikliKatalog', 'U') IS NULL
    CREATE TABLE dbo.ArtikliKatalog (
        Katalog nvarchar(30)  NOT NULL CONSTRAINT PK_ArtikliKatalog PRIMARY KEY,
        Opis    nvarchar(200) NULL,
        Aktivan bit           NOT NULL CONSTRAINT DF_ArtikliKatalog_Aktivan DEFAULT (1));
GO
INSERT dbo.ArtikliKatalog (Katalog, Opis)
SELECT k.Katalog, k.Opis
FROM (VALUES (N'DEUS', N'Artikli iz DEUS packing liste'), (N'OSTALI', N'Ostali artikli')) k (Katalog, Opis)
WHERE NOT EXISTS (SELECT 1 FROM dbo.ArtikliKatalog x WHERE x.Katalog = k.Katalog);
GO
IF COL_LENGTH('dbo.Artikli', 'Katalog') IS NULL
    ALTER TABLE dbo.Artikli ADD Katalog nvarchar(30) NOT NULL CONSTRAINT DF_Artikli_Katalog DEFAULT (N'OSTALI');
GO
IF OBJECT_ID('dbo.FK_Artikli_Katalog', 'F') IS NULL
    ALTER TABLE dbo.Artikli ADD CONSTRAINT FK_Artikli_Katalog FOREIGN KEY (Katalog) REFERENCES dbo.ArtikliKatalog (Katalog);
GO
-- Jedinstveni ključ Šifra + Katalog; ako u bazi već postoje duplikati, samo upozorenje (ključ se pravi kad se očiste)
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID('dbo.Artikli') AND name = 'UX_Artikli_Sifra_Katalog')
BEGIN
    BEGIN TRY
        EXEC sp_executesql N'
            IF EXISTS (SELECT 1 FROM dbo.Artikli GROUP BY Sifra, Katalog HAVING COUNT(*) > 1)
                PRINT ''UPOZORENJE: u Artikli postoje iste šifre u istom katalogu; ključ UX_Artikli_Sifra_Katalog nije napravljen.'';
            ELSE
                CREATE UNIQUE INDEX UX_Artikli_Sifra_Katalog ON dbo.Artikli (Sifra, Katalog);';
    END TRY
    BEGIN CATCH
        PRINT 'UPOZORENJE: ključ UX_Artikli_Sifra_Katalog nije napravljen (' + ERROR_MESSAGE() + ').';
    END CATCH
END
GO
IF COL_LENGTH('dbo.RNCarinskoPackingLista', 'Katalog') IS NULL
    ALTER TABLE dbo.RNCarinskoPackingLista ADD Katalog nvarchar(30) NULL;
GO

-- Zapis o korekciji stavki posle potvrde prijemnice (CAR-P08)
IF OBJECT_ID('dbo.RNCarinskoPrijemnicaKorekcija', 'U') IS NULL
    CREATE TABLE dbo.RNCarinskoPrijemnicaKorekcija (
        ID           int IDENTITY(1,1) NOT NULL CONSTRAINT PK_RNCarinskoPrijemnicaKorekcija PRIMARY KEY,
        IDPrijemnice int            NOT NULL,
        IDStavke     int            NULL,
        Akcija       nvarchar(20)   NOT NULL,   -- Dodavanje, Izmena, Brisanje
        Opis         nvarchar(1000) NULL,
        Korisnik     nvarchar(50)   NULL,
        Datum        datetime       NOT NULL CONSTRAINT DF_RNCarinskoPrijemnicaKorekcija_Datum DEFAULT (GETDATE()));
GO

/* ---------------------------------------------------------------------------------------------
   3. Podešavanja (CAR-P10)
   Kolone packing liste: više naziva zaglavlja odvojenih sa ; (velika/mala slova i kvačice se ne razlikuju).
   --------------------------------------------------------------------------------------------- */
IF OBJECT_ID('dbo.Podesavanja', 'U') IS NOT NULL
BEGIN
    DECLARE @p TABLE (Grupa nvarchar(50), Kljuc nvarchar(100), Vrednost nvarchar(max), Tip varchar(20), Opis nvarchar(250));
    INSERT @p VALUES
     (N'Carina', N'Carina.PL.NazivLista',        N'PAKING LISTA;PACKING LIST;Packing list', 'TEXT', N'Packing lista: naziv lista u Excel fajlu (prvi koji postoji); ako ga nema, Carina.PL.List, pa svi listovi redom'),
     (N'Carina', N'Carina.PL.List',              N'1', 'INT',  N'Packing lista: redni broj lista u Excel fajlu'),
     (N'Carina', N'Carina.PL.Kolona.JMPoPaleti', N'koficijent JM/ TR/PALETA;koeficijent JM/ TR/PALETA;JM po paleti', 'TEXT', N'Packing lista: JM po paleti (1/300 = 300 JM na paleti); koleta po paleti = JM po paleti / JM po koletu'),
     (N'Carina', N'Carina.PL.RedZaglavlja',      N'0', 'INT',  N'Packing lista: red zaglavlja (0 = traži se automatski u prvih 30 redova)'),
     (N'Carina', N'Carina.PL.Kolona.Sifra',      N'Šifra artikla;Šifra;Sifra;Item code;Code;Article code', 'TEXT', N'Packing lista: zaglavlje kolone šifra artikla'),
     (N'Carina', N'Carina.PL.Kolona.Artikal',    N'Naziv artikla;Artikal;Naziv;Opis;Description;Item', 'TEXT', N'Packing lista: zaglavlje kolone naziv artikla'),
     (N'Carina', N'Carina.PL.Kolona.JM',         N'JM;Jedinica mere;Unit;UOM', 'TEXT', N'Packing lista: zaglavlje kolone JM'),
     (N'Carina', N'Carina.PL.Kolona.Kolicina',   N'Količina;Kolicina;Quantity;Qty', 'TEXT', N'Packing lista: zaglavlje kolone količina (u JM)'),
     (N'Carina', N'Carina.PL.Kolona.Koleta',     N'Koleta;Broj koleta;Packages;Cartons;Ctns', 'TEXT', N'Packing lista: zaglavlje kolone koleta (nije obavezna)'),
     (N'Carina', N'Carina.PL.Kolona.Koeficijent',N'Koeficijent;JM po koletu;Pcs per carton;koficijent JM/ TRANSPORTNA KUTIJA (KOLETO);koeficijent JM/ TRANSPORTNA KUTIJA (KOLETO);koficijent JM/ TR/(KOLETO);koeficijent JM/ TR/(KOLETO)', 'TEXT', N'Packing lista: JM po koletu (nije obavezna); 1/5 = 1 koleto je 5 JM'),
     (N'Carina', N'Carina.PL.Kolona.Neto',       N'Neto;Neto kg;Net;Net weight', 'TEXT', N'Packing lista: zaglavlje kolone neto'),
     (N'Carina', N'Carina.PL.Kolona.Bruto',      N'Bruto;Bruto kg;Gross;Gross weight', 'TEXT', N'Packing lista: zaglavlje kolone bruto'),
     (N'Carina', N'Carina.PL.Kolona.Vrednost',   N'Vrednost;Iznos;Value;Amount', 'TEXT', N'Packing lista: zaglavlje kolone vrednost'),
     (N'Carina', N'Carina.PL.Kolona.Valuta',     N'Valuta;Currency', 'TEXT', N'Packing lista: zaglavlje kolone valuta'),
     (N'Carina', N'Carina.PL.Kolona.NHM',        N'NHM;Tarifni broj;HS code;HS', 'TEXT', N'Packing lista: zaglavlje kolone NHM / tarifni broj (nije obavezna)'),
     (N'Carina', N'Carina.PL.Kolona.NetoPoJM',   N'Neto težina po JM;Neto po JM;Net weight per unit', 'TEXT', N'Packing lista: neto po JM (ukupno = količina × neto po JM, ako nema kolone neto)'),
     (N'Carina', N'Carina.PL.Kolona.BrutoPoJM',  N'Bruto težina po JM;Bruto po JM;Gross weight per unit', 'TEXT', N'Packing lista: bruto po JM (ukupno = količina × bruto po JM, ako nema kolone bruto)'),
     (N'Carina', N'Carina.PL.Kolona.CenaPoJM',   N'Cena po JM;Jedinična cena;Unit price', 'TEXT', N'Packing lista: cena po JM (vrednost = količina × cena po JM, ako nema kolone vrednost)'),
     (N'Carina', N'Carina.PL.Kolona.BarKod',     N'Bar kod;Barkod;EAN', 'TEXT', N'Packing lista: bar kod (nije obavezna)'),
     (N'Carina', N'Carina.PL.Kolona.KataloskiBroj', N'Kataloški broj;Kataloski broj;Catalog number', 'TEXT', N'Packing lista: kataloški broj (nije obavezna)'),
     (N'Carina', N'Carina.PL.KljucArtikla',      N'Sifra', 'TEXT', N'Kako se prepoznaje artikal iz packing liste: Sifra (CAR-P01) ili Naziv (kad šifra u fajlu nije šifra artikla; nov artikal dobija kataloški broj, bar kod ili sledeći broj)'),
     (N'Carina', N'Carina.Prijem.TolerancijaApsolutna', N'0.05', 'DECIMAL', N'Dozvoljeno odstupanje primljeno/carina zbog zaokruživanja (kg, vrednost), kontrola po naimenovanju'),
     -- DEUS: zaglavlje „I NIVO“ … „IV NIVO“, a naziv nivoa je u koloni bez zaglavlja desno od njega (program to prepoznaje)
     (N'Carina', N'Carina.PL.Kolona.Nivo1Sifra', N'I nivo;Nivo 1 šifra;Prvi nivo šifra', 'TEXT', N'Packing lista: šifra prvog nivoa klasifikacije'),
     (N'Carina', N'Carina.PL.Kolona.Nivo1Naziv', N'Nivo 1 naziv;Prvi nivo naziv;Nivo 1', 'TEXT', N'Packing lista: naziv prvog nivoa klasifikacije'),
     (N'Carina', N'Carina.PL.Kolona.Nivo2Sifra', N'II nivo;Nivo 2 šifra;Drugi nivo šifra', 'TEXT', N'Packing lista: šifra drugog nivoa klasifikacije'),
     (N'Carina', N'Carina.PL.Kolona.Nivo2Naziv', N'Nivo 2 naziv;Drugi nivo naziv;Nivo 2', 'TEXT', N'Packing lista: naziv drugog nivoa klasifikacije'),
     (N'Carina', N'Carina.PL.Kolona.Nivo3Sifra', N'III nivo;Nivo 3 šifra;Treći nivo šifra', 'TEXT', N'Packing lista: šifra trećeg nivoa klasifikacije'),
     (N'Carina', N'Carina.PL.Kolona.Nivo3Naziv', N'Nivo 3 naziv;Treći nivo naziv;Nivo 3', 'TEXT', N'Packing lista: naziv trećeg nivoa klasifikacije'),
     (N'Carina', N'Carina.PL.Kolona.Nivo4Sifra', N'IV nivo;Nivo 4 šifra;Četvrti nivo šifra', 'TEXT', N'Packing lista: šifra četvrtog nivoa klasifikacije'),
     (N'Carina', N'Carina.PL.Kolona.Nivo4Naziv', N'Nivo 4 naziv;Četvrti nivo naziv;Nivo 4', 'TEXT', N'Packing lista: naziv četvrtog nivoa klasifikacije'),
     (N'Carina', N'Carina.PL.Arhiva',            NULL, 'PATH', N'Folder u koji se kopira uvezena packing lista (prazno = ne kopira se)'),
     (N'Carina', N'Carina.Prijem.TolerancijaProcenat', N'0', 'DECIMAL', N'Dozvoljeno odstupanje primljeno/carina u % (kontrola po naimenovanju, CAR-P05)'),
     (N'Carina', N'Carina.Prijemnica.KorekcijaKorisnici', NULL, 'TEXT', N'Korisnici koji smeju da menjaju stavke potvrđene prijemnice (ZA), odvojeni zarezom');

    INSERT dbo.Podesavanja (Grupa, Kljuc, Vrednost, TipVrednosti, Opis)
    SELECT p.Grupa, p.Kljuc, p.Vrednost, p.Tip, p.Opis FROM @p p
    WHERE NOT EXISTS (SELECT 1 FROM dbo.Podesavanja x WHERE x.Kljuc = p.Kljuc);
END
ELSE
    PRINT 'UPOZORENJE: tabela dbo.Podesavanja ne postoji; program koristi podrazumevane nazive kolona.';
GO

/* ---------------------------------------------------------------------------------------------
   3a. fn_PL_Koeficijent: Artikli.KoeficijentKoletaJM kao broj, bez greške kad u koloni nije broj
   (prazno, tekst, zarez kao decimalni separator). Nije broj -> NULL. Bez TRY_CONVERT (kompatibilnost < 110).
   --------------------------------------------------------------------------------------------- */
CREATE OR ALTER FUNCTION dbo.fn_PL_Koeficijent (@v nvarchar(100))
RETURNS decimal(18,6)
AS
BEGIN
    DECLARE @s nvarchar(100) = REPLACE(LTRIM(RTRIM(@v)), N',', N'.');
    IF @s IS NULL OR @s = N'' OR @s LIKE N'%[^0-9.]%' OR @s LIKE N'%.%.%' OR @s NOT LIKE N'%[0-9]%'
        RETURN NULL;
    IF LEN(CASE WHEN CHARINDEX(N'.', @s) > 0 THEN LEFT(@s, CHARINDEX(N'.', @s) - 1) ELSE @s END) > 12
        RETURN NULL;
    RETURN CONVERT(decimal(18,6), @s);
END
GO

/* ---------------------------------------------------------------------------------------------
   4. sp_PL_Analiziraj: rešava artikle po šifri i postavlja status svakog reda (CAR-P01..P03)
   Status reda je najteži nađeni: Greska > NoviArtikal > Upozorenje > Ok.
   --------------------------------------------------------------------------------------------- */
CREATE OR ALTER PROCEDURE dbo.sp_PL_Analiziraj
    @IDPackingListe int,
    @KljucArtikla   nvarchar(10) = N'Sifra'   -- Sifra (CAR-P01) ili Naziv (podešavanje Carina.PL.KljucArtikla)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @PoNazivu bit = CASE WHEN @KljucArtikla = N'Naziv' THEN 1 ELSE 0 END;
    -- artikal se traži samo u katalogu packing liste (NULL = stari uvoz, bez kataloga)
    DECLARE @Katalog nvarchar(30) = (SELECT Katalog FROM dbo.RNCarinskoPackingLista WHERE ID = @IDPackingListe);
    BEGIN TRAN;

    UPDATE dbo.RNCarinskoPackingListaStavka
    SET IDArtikla = NULL, KoletaObracun = NULL, StatusReda = N'Ok', Poruka = NULL
    WHERE IDPackingListe = @IDPackingListe;

    -- Kljuc: šifra ili naziv artikla iz fajla
    ;WITH x AS (SELECT ID, CASE WHEN @PoNazivu = 1 THEN UPPER(LTRIM(RTRIM(ISNULL(Artikal, N'')))) ELSE ISNULL(Sifra, N'') END AS Kljuc
                FROM dbo.RNCarinskoPackingListaStavka WHERE IDPackingListe = @IDPackingListe)
    SELECT ID, Kljuc INTO #k FROM x;

    -- Artikal po ključu (CAR-P01)
    IF @PoNazivu = 1
        UPDATE s SET IDArtikla = a.ID
        FROM dbo.RNCarinskoPackingListaStavka s
        JOIN #k k ON k.ID = s.ID
        CROSS APPLY (SELECT TOP 1 x.ID FROM dbo.Artikli x
                     WHERE UPPER(LTRIM(RTRIM(CONVERT(nvarchar(250), x.Artikal)))) COLLATE DATABASE_DEFAULT = k.Kljuc
                       AND (@Katalog IS NULL OR x.Katalog = @Katalog)
                     ORDER BY x.ID) a
        WHERE k.Kljuc <> N'';
    ELSE
        UPDATE s SET IDArtikla = a.ID
        FROM dbo.RNCarinskoPackingListaStavka s
        JOIN #k k ON k.ID = s.ID
        CROSS APPLY (SELECT TOP 1 x.ID FROM dbo.Artikli x
                     WHERE LTRIM(RTRIM(CONVERT(nvarchar(100), x.Sifra))) COLLATE DATABASE_DEFAULT = k.Kljuc
                       AND (@Katalog IS NULL OR x.Katalog = @Katalog)
                     ORDER BY x.ID) a
        WHERE k.Kljuc <> N'';

    -- Greške u fajlu
    UPDATE s
    SET StatusReda = N'Greska', Poruka = ISNULL(s.Poruka + N' ', N'') + CASE WHEN @PoNazivu = 1 THEN N'Prazan naziv artikla.' ELSE N'Prazna šifra artikla.' END
    FROM dbo.RNCarinskoPackingListaStavka s
    JOIN #k k ON k.ID = s.ID
    WHERE k.Kljuc = N'';

    UPDATE s
    SET StatusReda = N'Greska', Poruka = ISNULL(s.Poruka + N' ', N'') +
            CASE WHEN @PoNazivu = 1 THEN N'Isti artikal (naziv) se ponavlja u više redova fajla.'
                 ELSE N'Šifra se ponavlja u više redova fajla. Ako šifra u fajlu nije šifra artikla, podesite Carina.PL.KljucArtikla = Naziv.' END
    FROM dbo.RNCarinskoPackingListaStavka s
    JOIN #k k ON k.ID = s.ID
    WHERE k.Kljuc <> N'' AND k.Kljuc IN (SELECT Kljuc FROM #k WHERE Kljuc <> N'' GROUP BY Kljuc HAVING COUNT(*) > 1);

    UPDATE dbo.RNCarinskoPackingListaStavka
    SET StatusReda = N'Greska', Poruka = ISNULL(Poruka + N' ', N'') + N'Količina nije uneta ili nije broj.'
    WHERE IDPackingListe = @IDPackingListe AND ISNULL(Kolicina, 0) <= 0;

    -- Postojeći artikal: koeficijent iz šifarnika, upozorenja za razlike (postojeći artikal se ne menja)
    UPDATE s
    SET StatusReda = N'Greska', Poruka = ISNULL(s.Poruka + N' ', N'') + N'Artikal nema koeficijent koleta po JM; dopuniti ga u šifarniku artikala.'
    FROM dbo.RNCarinskoPackingListaStavka s
    JOIN dbo.Artikli a ON a.ID = s.IDArtikla
    WHERE s.IDPackingListe = @IDPackingListe AND ISNULL(dbo.fn_PL_Koeficijent(CONVERT(nvarchar(100), a.KoeficijentKoletaJM)), 0) <= 0;

    UPDATE s
    SET StatusReda = CASE WHEN s.StatusReda = N'Ok' THEN N'Upozorenje' ELSE s.StatusReda END,
        Poruka = ISNULL(s.Poruka + N' ', N'') + N'JM u fajlu (' + s.JM + N') razlikuje se od JM artikla (' + RTRIM(CONVERT(nvarchar(20), a.JM)) + N').'
    FROM dbo.RNCarinskoPackingListaStavka s
    JOIN dbo.Artikli a ON a.ID = s.IDArtikla
    WHERE s.IDPackingListe = @IDPackingListe AND ISNULL(s.JM, N'') <> N''
      AND UPPER(s.JM) <> UPPER(LTRIM(RTRIM(CONVERT(nvarchar(20), a.JM)))) COLLATE DATABASE_DEFAULT;

    UPDATE s
    SET StatusReda = CASE WHEN s.StatusReda = N'Ok' THEN N'Upozorenje' ELSE s.StatusReda END,
        Poruka = ISNULL(s.Poruka + N' ', N'') + N'Naziv u fajlu razlikuje se od naziva artikla (' + RTRIM(CONVERT(nvarchar(250), a.Artikal)) + N').'
    FROM dbo.RNCarinskoPackingListaStavka s
    JOIN dbo.Artikli a ON a.ID = s.IDArtikla
    WHERE s.IDPackingListe = @IDPackingListe AND ISNULL(s.Artikal, N'') <> N''
      AND UPPER(s.Artikal) <> UPPER(LTRIM(RTRIM(CONVERT(nvarchar(250), a.Artikal)))) COLLATE DATABASE_DEFAULT;

    -- četvrti nivo artikla: po šifri, a kad je fajl nema, po nazivu
    UPDATE s
    SET StatusReda = CASE WHEN s.StatusReda = N'Ok' THEN N'Upozorenje' ELSE s.StatusReda END,
        Poruka = ISNULL(s.Poruka + N' ', N'') + N'Klasifikacija u fajlu razlikuje se od klasifikacije artikla.'
    FROM dbo.RNCarinskoPackingListaStavka s
    JOIN dbo.Artikli a ON a.ID = s.IDArtikla
    JOIN dbo.ArtikliCetvrtiNivo c ON c.ID = a.IDNadredjena
    WHERE s.IDPackingListe = @IDPackingListe
      AND (   (ISNULL(s.Nivo4Sifra, N'') <> N''
               AND s.Nivo4Sifra <> LTRIM(RTRIM(CONVERT(nvarchar(100), c.Sifra))) COLLATE DATABASE_DEFAULT)
           OR (ISNULL(s.Nivo4Sifra, N'') = N'' AND ISNULL(s.Nivo4Naziv, N'') <> N''
               AND UPPER(LTRIM(RTRIM(s.Nivo4Naziv))) <> UPPER(LTRIM(RTRIM(CONVERT(nvarchar(200), c.Naziv)))) COLLATE DATABASE_DEFAULT));

    -- ISP-02: koeficijent iz fajla mora biti ceo broj veći od nule.
    -- SKC-11: greška samo za nov artikal; postojeći artikal koristi svoj koeficijent, pa je to upozorenje
    -- (ranije je red ostajao Greška, a izmena „JM po koletu“ ide u Artikli, pa prenos nije bio moguć).
    UPDATE dbo.RNCarinskoPackingListaStavka
    SET StatusReda = N'Greska', Poruka = ISNULL(Poruka + N' ', N'') + N'JM po koletu iz fajla (' + CONVERT(nvarchar(40), CONVERT(decimal(18,3), Koeficijent)) + N') nije ceo broj veći od nule.'
    WHERE IDPackingListe = @IDPackingListe AND IDArtikla IS NULL AND Koeficijent IS NOT NULL AND (Koeficijent <= 0 OR Koeficijent <> ROUND(Koeficijent, 0));

    UPDATE dbo.RNCarinskoPackingListaStavka
    SET StatusReda = CASE WHEN StatusReda = N'Ok' THEN N'Upozorenje' ELSE StatusReda END,
        Poruka = ISNULL(Poruka + N' ', N'') + N'JM po koletu iz fajla (' + CONVERT(nvarchar(40), CONVERT(decimal(18,3), Koeficijent)) + N') nije ceo broj; koristi se koeficijent artikla.'
    WHERE IDPackingListe = @IDPackingListe AND IDArtikla IS NOT NULL AND Koeficijent IS NOT NULL AND (Koeficijent <= 0 OR Koeficijent <> ROUND(Koeficijent, 0));

    UPDATE s
    SET StatusReda = CASE WHEN s.StatusReda = N'Ok' THEN N'Upozorenje' ELSE s.StatusReda END,
        Poruka = ISNULL(s.Poruka + N' ', N'') + N'JM po koletu u fajlu (' + CONVERT(nvarchar(40), CONVERT(decimal(18,3), s.Koeficijent))
               + N') razlikuje se od artikla (' + CONVERT(nvarchar(40), CONVERT(decimal(18,3), dbo.fn_PL_Koeficijent(CONVERT(nvarchar(100), a.KoeficijentKoletaJM))))
               + N'); koristi se koeficijent artikla.'
    FROM dbo.RNCarinskoPackingListaStavka s
    JOIN dbo.Artikli a ON a.ID = s.IDArtikla
    WHERE s.IDPackingListe = @IDPackingListe AND s.Koeficijent IS NOT NULL
      AND ISNULL(dbo.fn_PL_Koeficijent(CONVERT(nvarchar(100), a.KoeficijentKoletaJM)), 0) > 0
      AND s.Koeficijent <> dbo.fn_PL_Koeficijent(CONVERT(nvarchar(100), a.KoeficijentKoletaJM));

    -- Nov artikal (ne postoji u Artikli)
    UPDATE s
    SET StatusReda = CASE WHEN s.StatusReda = N'Greska' THEN s.StatusReda ELSE N'NoviArtikal' END,
        Poruka = ISNULL(s.Poruka + N' ', N'') + N'Nov artikal.'
               + CASE WHEN ISNULL(s.Koeficijent, 0) <= 0 THEN N' Unesite JM po koletu.' ELSE N'' END
               + CASE WHEN ISNULL(s.Nivo1Sifra, N'') + ISNULL(s.Nivo1Naziv, N'') + ISNULL(s.Nivo2Sifra, N'') + ISNULL(s.Nivo2Naziv, N'')
                         + ISNULL(s.Nivo3Sifra, N'') + ISNULL(s.Nivo3Naziv, N'') + ISNULL(s.Nivo4Sifra, N'') + ISNULL(s.Nivo4Naziv, N'') = N''
                      THEN N' Klasifikacija se bira pri formiranju.' ELSE N'' END
    FROM dbo.RNCarinskoPackingListaStavka s
    JOIN #k k ON k.ID = s.ID
    WHERE s.IDArtikla IS NULL AND k.Kljuc <> N'';

    -- Klasifikacija iz fajla (šifra ili naziv svakog nivoa) mora biti potpuna; ako je nema, bira se pri formiranju
    UPDATE s
    SET StatusReda = N'Greska', Poruka = ISNULL(s.Poruka + N' ', N'') + N'Klasifikacija u fajlu nije potpuna (nivoi 1–4).'
    FROM dbo.RNCarinskoPackingListaStavka s
    JOIN #k k ON k.ID = s.ID
    WHERE s.IDArtikla IS NULL AND k.Kljuc <> N''
      AND ISNULL(s.Nivo1Sifra, N'') + ISNULL(s.Nivo1Naziv, N'') + ISNULL(s.Nivo2Sifra, N'') + ISNULL(s.Nivo2Naziv, N'')
        + ISNULL(s.Nivo3Sifra, N'') + ISNULL(s.Nivo3Naziv, N'') + ISNULL(s.Nivo4Sifra, N'') + ISNULL(s.Nivo4Naziv, N'') <> N''
      AND (   ISNULL(s.Nivo1Sifra, N'') + ISNULL(s.Nivo1Naziv, N'') = N'' OR ISNULL(s.Nivo2Sifra, N'') + ISNULL(s.Nivo2Naziv, N'') = N''
           OR ISNULL(s.Nivo3Sifra, N'') + ISNULL(s.Nivo3Naziv, N'') = N'' OR ISNULL(s.Nivo4Sifra, N'') + ISNULL(s.Nivo4Naziv, N'') = N'');

    UPDATE s
    SET StatusReda = N'Greska', Poruka = ISNULL(s.Poruka + N' ', N'') + N'Za nov artikal nedostaje naziv ili JM.'
    FROM dbo.RNCarinskoPackingListaStavka s
    JOIN #k k ON k.ID = s.ID
    WHERE s.IDArtikla IS NULL AND k.Kljuc <> N''
      AND (ISNULL(s.Artikal, N'') = N'' OR ISNULL(s.JM, N'') = N'');

    -- Koleta = Količina / koeficijent mora biti ceo broj (CAR-P03)
    ;WITH k AS (
        SELECT s.ID, s.Kolicina,
               NULLIF(CASE WHEN s.IDArtikla IS NOT NULL THEN dbo.fn_PL_Koeficijent(CONVERT(nvarchar(100), a.KoeficijentKoletaJM)) ELSE s.Koeficijent END, 0) AS Koef
        FROM dbo.RNCarinskoPackingListaStavka s
        LEFT JOIN dbo.Artikli a ON a.ID = s.IDArtikla
        WHERE s.IDPackingListe = @IDPackingListe AND ISNULL(s.Kolicina, 0) > 0)
    UPDATE s
    SET KoletaObracun = CASE WHEN ABS(k.Kolicina / k.Koef - ROUND(k.Kolicina / k.Koef, 0)) < 0.0005
                             THEN ROUND(k.Kolicina / k.Koef, 0) ELSE NULL END,
        StatusReda = CASE WHEN ABS(k.Kolicina / k.Koef - ROUND(k.Kolicina / k.Koef, 0)) < 0.0005 THEN s.StatusReda ELSE N'Greska' END,
        Poruka = CASE WHEN ABS(k.Kolicina / k.Koef - ROUND(k.Kolicina / k.Koef, 0)) < 0.0005 THEN s.Poruka
                      ELSE ISNULL(s.Poruka + N' ', N'') + N'Količina ' + CONVERT(nvarchar(40), CONVERT(decimal(18,3), k.Kolicina))
                         + N' nije deljiva koeficijentom ' + CONVERT(nvarchar(40), CONVERT(decimal(18,3), k.Koef)) + N': koleta nije ceo broj.' END
    FROM dbo.RNCarinskoPackingListaStavka s
    JOIN k ON k.ID = s.ID
    WHERE k.Koef > 0;

    UPDATE dbo.RNCarinskoPackingListaStavka
    SET StatusReda = CASE WHEN StatusReda = N'Ok' THEN N'Upozorenje' ELSE StatusReda END,
        Poruka = ISNULL(Poruka + N' ', N'') + N'Koleta u fajlu (' + CONVERT(nvarchar(40), Koleta)
               + N') razlikuje se od obračuna (' + CONVERT(nvarchar(40), KoletaObracun) + N').'
    WHERE IDPackingListe = @IDPackingListe AND Koleta IS NOT NULL AND KoletaObracun IS NOT NULL AND Koleta <> KoletaObracun;

    COMMIT;
END
GO

/* ---------------------------------------------------------------------------------------------
   4a. sp_Artikli_NivoiZaR: za svaki red privremene tabele #r (pozivalac je pravi) traži ili pravi
   nivoe klasifikacije 1–4 i upisuje njihove ID u #r.ID1..ID4.
   #r mora imati kolone S1..S4 (šifra nivoa, NULL ako je nema), N1..N4 (naziv), ID1..ID4 (int NULL).
   Nivo se prepoznaje po šifri, a kad je nema (DEUS daje samo nazive), po nazivu; nivoi 2–4 uvek
   unutar nadređenog nivoa. Nov nivo bez šifre dobija sledeći broj. Postojeći nivoi se ne menjaju.
   Ako ID tabele nivoa nije IDENTITY, uzima se MAX(ID) + 1.
   --------------------------------------------------------------------------------------------- */
CREATE OR ALTER PROCEDURE dbo.sp_Artikli_NivoiZaR
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @nivo int = 1, @tabela nvarchar(100), @sqlNivo nvarchar(max), @upis nvarchar(max), @pronadji nvarchar(max);
    WHILE @nivo <= 4
    BEGIN
        SET @tabela = CASE @nivo WHEN 1 THEN N'dbo.ArtikliPrviNivo' WHEN 2 THEN N'dbo.ArtikliDrugiNivo'
                                 WHEN 3 THEN N'dbo.ArtikliTreciNivo' ELSE N'dbo.ArtikliCetvrtiNivo' END;

        SET @pronadji = N'UPDATE r SET ID{N} = (SELECT MIN(x.ID) FROM {TBL} x
                WHERE ((r.S{N} IS NOT NULL AND LTRIM(RTRIM(CONVERT(nvarchar(100), x.Sifra))) COLLATE DATABASE_DEFAULT = r.S{N})
                    OR (r.S{N} IS NULL AND UPPER(LTRIM(RTRIM(CONVERT(nvarchar(200), x.Naziv)))) COLLATE DATABASE_DEFAULT = UPPER(r.N{N}))) {PX})
            FROM #r r WHERE r.N{N} IS NOT NULL AND r.ID{N} IS NULL {PR};';

        SET @upis = CASE WHEN COLUMNPROPERTY(OBJECT_ID(@tabela), 'ID', 'IsIdentity') = 1
            THEN N'INSERT INTO {TBL} (Sifra, Naziv {PCOL}) SELECT ISNULL(S, CONVERT(nvarchar(50), @max + Rbr)), N {PVAL} FROM #nov;'
            ELSE N'INSERT INTO {TBL} (ID, Sifra, Naziv {PCOL})
                   SELECT (SELECT ISNULL(MAX(ID), 0) FROM {TBL}) + Rbr, ISNULL(S, CONVERT(nvarchar(50), @max + Rbr)), N {PVAL} FROM #nov;' END;

        SET @sqlNivo = @pronadji + N'
            DECLARE @max bigint;
            SELECT @max = ISNULL(MAX(CASE WHEN v.S <> N'''' AND v.S NOT LIKE N''%[^0-9]%'' AND LEN(v.S) <= 18 THEN CONVERT(bigint, v.S) END), 0)
            FROM (SELECT LTRIM(RTRIM(CONVERT(nvarchar(100), Sifra))) AS S FROM {TBL}) v;

            SELECT {PSEL} AS P, MAX(r.S{N}) AS S, MAX(r.N{N}) AS N,
                   ROW_NUMBER() OVER (ORDER BY {PGRP} COALESCE(r.S{N}, UPPER(r.N{N}))) AS Rbr
            INTO #nov
            FROM #r r
            WHERE r.N{N} IS NOT NULL AND r.ID{N} IS NULL {PR}
            GROUP BY {PGRP} COALESCE(r.S{N}, UPPER(r.N{N}));
            ' + @upis + N'
            DROP TABLE #nov;
            ' + @pronadji;

        SET @sqlNivo = REPLACE(@sqlNivo, N'{PX}',   CASE WHEN @nivo = 1 THEN N'' ELSE N'AND x.IDNadredjena = r.ID{P}' END);
        SET @sqlNivo = REPLACE(@sqlNivo, N'{PR}',   CASE WHEN @nivo = 1 THEN N'' ELSE N'AND r.ID{P} IS NOT NULL' END);
        SET @sqlNivo = REPLACE(@sqlNivo, N'{PSEL}', CASE WHEN @nivo = 1 THEN N'CAST(NULL AS int)' ELSE N'r.ID{P}' END);
        SET @sqlNivo = REPLACE(@sqlNivo, N'{PGRP}', CASE WHEN @nivo = 1 THEN N'' ELSE N'r.ID{P},' END);
        SET @sqlNivo = REPLACE(@sqlNivo, N'{PCOL}', CASE WHEN @nivo = 1 THEN N'' ELSE N', IDNadredjena' END);
        SET @sqlNivo = REPLACE(@sqlNivo, N'{PVAL}', CASE WHEN @nivo = 1 THEN N'' ELSE N', P' END);
        SET @sqlNivo = REPLACE(@sqlNivo, N'{TBL}',  @tabela);
        SET @sqlNivo = REPLACE(@sqlNivo, N'{N}',    CONVERT(nvarchar(1), @nivo));
        SET @sqlNivo = REPLACE(@sqlNivo, N'{P}',    CONVERT(nvarchar(1), @nivo - 1));

        EXEC sp_executesql @sqlNivo;
        SET @nivo = @nivo + 1;
    END
END
GO

/* ---------------------------------------------------------------------------------------------
   5. sp_PL_FormirajArtikle: pravi nedostajuće nivoe i artikle za redove „Nov artikal“ sa koeficijentom.
   Postojeći nivoi i artikli se ne menjaju. Ako ID neke tabele nije IDENTITY, uzima se MAX(ID) + 1.
   --------------------------------------------------------------------------------------------- */
CREATE OR ALTER PROCEDURE dbo.sp_PL_FormirajArtikle
    @IDPackingListe int,
    @Formirano      int OUTPUT,
    @KljucArtikla   nvarchar(10) = N'Sifra',
    @IDCetvrtiNivo  int = NULL,     -- klasifikacija za nove artikle bez nivoa u fajlu (bira korisnik)
    @Katalog        nvarchar(30) = NULL   -- katalog u koji se upisuju novi artikli (bira korisnik)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    SET @Formirano = 0;
    DECLARE @PoNazivu bit = CASE WHEN @KljucArtikla = N'Naziv' THEN 1 ELSE 0 END;

    IF EXISTS (SELECT 1 FROM dbo.RNCarinskoPackingLista WHERE ID = @IDPackingListe AND Status = N'Zamenjeno')
    BEGIN
        RAISERROR(N'Packing lista je zamenjena novim uvozom.', 16, 1);
        RETURN;
    END
    IF @Katalog IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.ArtikliKatalog WHERE Katalog = @Katalog)
    BEGIN
        RAISERROR(N'Katalog %s ne postoji.', 16, 1, @Katalog);
        RETURN;
    END

    BEGIN TRAN;

    -- Izabrani katalog važi za celu packing listu: artikli se ponovo traže u njemu pa se formiraju nedostajući
    IF @Katalog IS NOT NULL
    BEGIN
        UPDATE dbo.RNCarinskoPackingLista SET Katalog = @Katalog WHERE ID = @IDPackingListe;
        EXEC dbo.sp_PL_Analiziraj @IDPackingListe, @KljucArtikla;
    END
    SET @Katalog = ISNULL((SELECT Katalog FROM dbo.RNCarinskoPackingLista WHERE ID = @IDPackingListe), N'OSTALI');

    -- Nivo se prepoznaje po šifri, a kad je fajl nema (DEUS daje samo nazive), po nazivu;
    -- nivoi 2–4 uvek unutar nadređenog nivoa. Nov nivo bez šifre dobija sledeći broj.
    SELECT s.ID,
           NULLIF(LTRIM(RTRIM(ISNULL(s.Nivo1Sifra, N''))), N'') AS S1, NULLIF(LTRIM(RTRIM(ISNULL(s.Nivo1Naziv, N''))), N'') AS N1,
           NULLIF(LTRIM(RTRIM(ISNULL(s.Nivo2Sifra, N''))), N'') AS S2, NULLIF(LTRIM(RTRIM(ISNULL(s.Nivo2Naziv, N''))), N'') AS N2,
           NULLIF(LTRIM(RTRIM(ISNULL(s.Nivo3Sifra, N''))), N'') AS S3, NULLIF(LTRIM(RTRIM(ISNULL(s.Nivo3Naziv, N''))), N'') AS N3,
           NULLIF(LTRIM(RTRIM(ISNULL(s.Nivo4Sifra, N''))), N'') AS S4, NULLIF(LTRIM(RTRIM(ISNULL(s.Nivo4Naziv, N''))), N'') AS N4,
           s.Sifra, s.Artikal, s.JM, s.Koeficijent, s.KataloskiBroj, s.BarKod
    INTO #r0
    FROM dbo.RNCarinskoPackingListaStavka s
    WHERE s.IDPackingListe = @IDPackingListe AND s.StatusReda = N'NoviArtikal' AND s.IDArtikla IS NULL AND ISNULL(s.Koeficijent, 0) > 0
      AND (ISNULL(s.Nivo4Sifra, N'') + ISNULL(s.Nivo4Naziv, N'') <> N'' OR @IDCetvrtiNivo IS NOT NULL);

    IF NOT EXISTS (SELECT 1 FROM #r0)
    BEGIN
        COMMIT;
        RETURN;
    END

    SELECT ID, S1, ISNULL(N1, S1) AS N1, S2, ISNULL(N2, S2) AS N2, S3, ISNULL(N3, S3) AS N3, S4, ISNULL(N4, S4) AS N4,
           CAST(NULL AS int) AS ID1, CAST(NULL AS int) AS ID2, CAST(NULL AS int) AS ID3, CAST(NULL AS int) AS ID4,
           Sifra, Artikal, JM, Koeficijent, KataloskiBroj, BarKod
    INTO #r
    FROM #r0;

    EXEC dbo.sp_Artikli_NivoiZaR;   -- popunjava #r.ID1..ID4 (pravi nedostajuće nivoe)

    -- Artikli: klasifikacija iz fajla, inače izabrani četvrti nivo.
    -- Ključ Naziv: šifra novog artikla je kataloški broj, bar kod ili sledeći broj.
    -- (bez TRY_CONVERT: baza može imati nivo kompatibilnosti ispod 110)
    DECLARE @MaxSifra bigint;
    SELECT @MaxSifra = ISNULL(MAX(CASE WHEN v.S <> N'' AND v.S NOT LIKE N'%[^0-9]%' AND LEN(v.S) <= 18 THEN CONVERT(bigint, v.S) END), 0)
    FROM (SELECT LTRIM(RTRIM(CONVERT(nvarchar(100), Sifra))) AS S FROM dbo.Artikli WHERE Katalog = @Katalog) v;

    SELECT r.ID, r.Sifra, r.Artikal, r.JM, r.Koeficijent, r.KataloskiBroj, r.BarKod,
           COALESCE(r.ID4, @IDCetvrtiNivo) AS IDNadredjena, @Katalog AS Katalog,
           ROW_NUMBER() OVER (ORDER BY r.ID) AS Rbr
    INTO #a
    FROM #r r
    WHERE COALESCE(r.ID4, @IDCetvrtiNivo) IS NOT NULL
      AND NOT EXISTS (SELECT 1 FROM dbo.Artikli x
                      WHERE x.Katalog = @Katalog
                        AND (   (@PoNazivu = 0 AND LTRIM(RTRIM(CONVERT(nvarchar(100), x.Sifra))) COLLATE DATABASE_DEFAULT = r.Sifra)
                             OR (@PoNazivu = 1 AND UPPER(LTRIM(RTRIM(CONVERT(nvarchar(250), x.Artikal)))) COLLATE DATABASE_DEFAULT = UPPER(LTRIM(RTRIM(r.Artikal))))));

    IF @PoNazivu = 1
        UPDATE #a SET Sifra = COALESCE(NULLIF(KataloskiBroj, N''), NULLIF(BarKod, N''), CONVERT(nvarchar(50), @MaxSifra + Rbr));

    IF COLUMNPROPERTY(OBJECT_ID('dbo.Artikli'), 'ID', 'IsIdentity') = 1
        INSERT INTO dbo.Artikli (Sifra, Artikal, JM, KoeficijentKoletaJM, IDNadredjena, Katalog)
        SELECT Sifra, Artikal, JM, Koeficijent, IDNadredjena, Katalog FROM #a;
    ELSE
        EXEC sp_executesql N'INSERT INTO dbo.Artikli (ID, Sifra, Artikal, JM, KoeficijentKoletaJM, IDNadredjena, Katalog)
            SELECT (SELECT ISNULL(MAX(ID), 0) FROM dbo.Artikli) + ROW_NUMBER() OVER (ORDER BY Sifra), Sifra, Artikal, JM, Koeficijent, IDNadredjena, Katalog FROM #a';

    SET @Formirano = (SELECT COUNT(*) FROM #a);

    EXEC dbo.sp_PL_Analiziraj @IDPackingListe, @KljucArtikla;

    COMMIT;
END
GO

/* ---------------------------------------------------------------------------------------------
   6. sp_PL_PrenesiUStavke: redovi spremni za prenos u stavke prijemnice.
   Ne upisuje ništa; stavke se trajno upisuju na Snimi (sp_PrijemnicaStavka_Snimi).
   --------------------------------------------------------------------------------------------- */
CREATE OR ALTER PROCEDURE dbo.sp_PL_PrenesiUStavke
    @IDPackingListe int
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM dbo.RNCarinskoPackingListaStavka
               WHERE IDPackingListe = @IDPackingListe AND StatusReda IN (N'Greska', N'NoviArtikal'))
    BEGIN
        RAISERROR(N'Packing lista ima redove sa greškom ili nove artikle. Prvo ih rešite (Formiraj nove artikle).', 16, 1);
        RETURN;
    END

    SELECT s.ID AS IDPLStavke, s.RedUFajlu, s.IDArtikla,
           RTRIM(CONVERT(nvarchar(250), a.Artikal)) AS Artikal,
           RTRIM(CONVERT(nvarchar(20), a.JM)) AS JM,
           dbo.fn_PL_Koeficijent(CONVERT(nvarchar(100), a.KoeficijentKoletaJM)) AS Koeficijent,
           s.Kolicina, s.KoletaObracun AS KoletaPL, s.Neto, s.Bruto, s.Vrednost, s.Valuta, s.NHM
    FROM dbo.RNCarinskoPackingListaStavka s
    JOIN dbo.Artikli a ON a.ID = s.IDArtikla
    WHERE s.IDPackingListe = @IDPackingListe
    ORDER BY s.RedUFajlu;
END
GO

/* ---------------------------------------------------------------------------------------------
   7. sp_PrijemnicaStavka_Snimi: ažurira stavku po ID-u, dodaje novu ili briše uklonjenu (CAR-P07).
   Zamena za „obriši sve pa upiši“, pa ID stavke (veza sa Promet) ostaje isti.
   Posle potvrde prijemnice (Status ZA) svaka promena se zapisuje u RNCarinskoPrijemnicaKorekcija (CAR-P08).
   --------------------------------------------------------------------------------------------- */
CREATE OR ALTER PROCEDURE dbo.sp_PrijemnicaStavka_Snimi
    @ID             int OUTPUT,
    @IDNadredjena   int,
    @Obrisi         bit = 0,
    @RB             int = NULL,
    @NHM            varchar(10) = NULL,   -- tarifni broj kao tekst (Carina_TarifniBroj_2026_11.sql)
    @IDArtikla      int = NULL,
    @Naziv          nvarchar(max) = NULL,
    @Naimenovanje   nvarchar(max) = NULL,
    @JM             nvarchar(50) = NULL,
    @Koleta         decimal(18,3) = NULL,
    @Bruto          decimal(18,3) = NULL,
    @Neto           decimal(18,3) = NULL,
    @Vrednost       decimal(18,2) = NULL,
    @Valuta         nvarchar(50) = NULL,
    @Pozicija       nvarchar(200) = NULL,
    @Paleta         int = NULL,
    @VrstaPaleta    int = NULL,
    @PDV            decimal(18,2) = NULL,
    @Carina         decimal(18,2) = NULL,
    @Napomena       nvarchar(max) = NULL,
    @IDNaimenovanja int = NULL,
    @KoletaPL       decimal(18,3) = NULL,
    @KolicinaJM     decimal(18,3) = NULL,
    @IDPLStavke     int = NULL,
    @Korisnik       nvarchar(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Status nvarchar(10) = (SELECT RTRIM(Status) FROM dbo.RNCarinskoSkladistePrijemnica WHERE ID = @IDNadredjena);
    DECLARE @Stara decimal(18,3);

    IF @Obrisi = 1
    BEGIN
        IF @Status = N'ZA' AND EXISTS (
            SELECT 1 FROM dbo.Promet p
            JOIN dbo.RNCarinskoSkladistePrijemnica pr ON pr.ID = @IDNadredjena
            JOIN dbo.RadniNalogSkladista rn ON rn.ID = pr.RN
            WHERE p.IDSaDokumenta = @ID AND p.MagacinskiBroj = rn.MagacinskiBroj)
        BEGIN
            RAISERROR(N'Stavka RB %d je već knjižena u prometu skladišta i ne može da se obriše.', 16, 1, @RB);
            RETURN;
        END

        -- palete (poglavlje 10): stavka na pozicioniranoj paleti se ne briše; planirane palete stavke se brišu
        IF OBJECT_ID('dbo.RNCarinskoPaletaStavka', 'U') IS NOT NULL
        BEGIN
            IF EXISTS (SELECT 1 FROM dbo.RNCarinskoPaletaStavka ps JOIN dbo.RNCarinskoPaleta p ON p.ID = ps.IDPalete
                       WHERE ps.IDStavke = @ID AND p.Status <> 1)
            BEGIN
                RAISERROR(N'Stavka je na paleti koja ima polje i ne može da se obriše; prvo Ukloni polje.', 16, 1);
                RETURN;
            END
            DELETE FROM dbo.RNCarinskoPaletaStavka WHERE IDStavke = @ID;
            DELETE p FROM dbo.RNCarinskoPaleta p
            WHERE p.IDPrijemnice = @IDNadredjena AND p.Status = 1
              AND NOT EXISTS (SELECT 1 FROM dbo.RNCarinskoPaletaStavka x WHERE x.IDPalete = p.ID);
        END

        IF @Status = N'ZA'
            INSERT dbo.RNCarinskoPrijemnicaKorekcija (IDPrijemnice, IDStavke, Akcija, Opis, Korisnik)
            SELECT @IDNadredjena, @ID, N'Brisanje',
                   N'RB ' + CONVERT(nvarchar(10), RB) + N', ' + ISNULL(RTRIM(Naziv), N'') + N', koleta ' + CONVERT(nvarchar(40), Koleta), @Korisnik
            FROM dbo.RNCarinskoPrijemnicaStavke WHERE ID = @ID AND IDNadredjena = @IDNadredjena;

        DELETE FROM dbo.RNCarinskoPrijemnicaStavke WHERE ID = @ID AND IDNadredjena = @IDNadredjena;
        RETURN;
    END

    IF ISNULL(@ID, 0) > 0 AND EXISTS (SELECT 1 FROM dbo.RNCarinskoPrijemnicaStavke WHERE ID = @ID AND IDNadredjena = @IDNadredjena)
    BEGIN
        SELECT @Stara = Koleta FROM dbo.RNCarinskoPrijemnicaStavke WHERE ID = @ID;

        UPDATE dbo.RNCarinskoPrijemnicaStavke
        SET RB = @RB, NHM = @NHM, IDArtikla = @IDArtikla, Naziv = @Naziv, Naimenovanje = @Naimenovanje, JM = @JM,
            Koleta = @Koleta, Bruto = @Bruto, Neto = @Neto, Vrednost = @Vrednost, Valuta = @Valuta, Pozicija = @Pozicija,
            Paleta = @Paleta, VrstaPaleta = @VrstaPaleta, PDV = @PDV, Carina = @Carina, Napomena = @Napomena,
            IDNaimenovanja = @IDNaimenovanja, KoletaPL = @KoletaPL, KolicinaJM = @KolicinaJM, IDPLStavke = @IDPLStavke
        WHERE ID = @ID;

        IF @Status = N'ZA'
            INSERT dbo.RNCarinskoPrijemnicaKorekcija (IDPrijemnice, IDStavke, Akcija, Opis, Korisnik)
            VALUES (@IDNadredjena, @ID, N'Izmena',
                    N'RB ' + CONVERT(nvarchar(10), @RB) + N', koleta ' + ISNULL(CONVERT(nvarchar(40), @Stara), N'-') + N' -> ' + ISNULL(CONVERT(nvarchar(40), @Koleta), N'-'),
                    @Korisnik);
        RETURN;
    END

    IF COLUMNPROPERTY(OBJECT_ID('dbo.RNCarinskoPrijemnicaStavke'), 'ID', 'IsIdentity') = 1
    BEGIN
        INSERT INTO dbo.RNCarinskoPrijemnicaStavke
            (IDNadredjena, RB, NHM, IDArtikla, Naziv, Naimenovanje, JM, Koleta, Bruto, Neto, Vrednost, Valuta, Pozicija,
             Paleta, VrstaPaleta, PDV, Carina, Napomena, IDNaimenovanja, KoletaPL, KolicinaJM, IDPLStavke)
        VALUES
            (@IDNadredjena, @RB, @NHM, @IDArtikla, @Naziv, @Naimenovanje, @JM, @Koleta, @Bruto, @Neto, @Vrednost, @Valuta, @Pozicija,
             @Paleta, @VrstaPaleta, @PDV, @Carina, @Napomena, @IDNaimenovanja, @KoletaPL, @KolicinaJM, @IDPLStavke);
        SET @ID = SCOPE_IDENTITY();
    END
    ELSE
    BEGIN
        SELECT @ID = ISNULL(MAX(ID), 0) + 1 FROM dbo.RNCarinskoPrijemnicaStavke WITH (UPDLOCK, HOLDLOCK);
        EXEC sp_executesql N'INSERT INTO dbo.RNCarinskoPrijemnicaStavke
            (ID, IDNadredjena, RB, NHM, IDArtikla, Naziv, Naimenovanje, JM, Koleta, Bruto, Neto, Vrednost, Valuta, Pozicija,
             Paleta, VrstaPaleta, PDV, Carina, Napomena, IDNaimenovanja, KoletaPL, KolicinaJM, IDPLStavke)
            VALUES (@ID, @IDNadredjena, @RB, @NHM, @IDArtikla, @Naziv, @Naimenovanje, @JM, @Koleta, @Bruto, @Neto, @Vrednost, @Valuta, @Pozicija,
             @Paleta, @VrstaPaleta, @PDV, @Carina, @Napomena, @IDNaimenovanja, @KoletaPL, @KolicinaJM, @IDPLStavke)',
            N'@ID int, @IDNadredjena int, @RB int, @NHM varchar(10), @IDArtikla int, @Naziv nvarchar(max), @Naimenovanje nvarchar(max), @JM nvarchar(50),
              @Koleta decimal(18,3), @Bruto decimal(18,3), @Neto decimal(18,3), @Vrednost decimal(18,2), @Valuta nvarchar(50), @Pozicija nvarchar(200),
              @Paleta int, @VrstaPaleta int, @PDV decimal(18,2), @Carina decimal(18,2), @Napomena nvarchar(max), @IDNaimenovanja int,
              @KoletaPL decimal(18,3), @KolicinaJM decimal(18,3), @IDPLStavke int',
            @ID, @IDNadredjena, @RB, @NHM, @IDArtikla, @Naziv, @Naimenovanje, @JM, @Koleta, @Bruto, @Neto, @Vrednost, @Valuta, @Pozicija,
            @Paleta, @VrstaPaleta, @PDV, @Carina, @Napomena, @IDNaimenovanja, @KoletaPL, @KolicinaJM, @IDPLStavke;
    END

    IF @Status = N'ZA'
        INSERT dbo.RNCarinskoPrijemnicaKorekcija (IDPrijemnice, IDStavke, Akcija, Opis, Korisnik)
        VALUES (@IDNadredjena, @ID, N'Dodavanje', N'RB ' + CONVERT(nvarchar(10), @RB) + N', koleta ' + ISNULL(CONVERT(nvarchar(40), @Koleta), N'-'), @Korisnik);
END
GO

/* ---------------------------------------------------------------------------------------------
   8. sp_Prijem_Kontrola: naimenovanja magacinskog predmeta, sa carinskim količinama i onim što je
   primljeno na DRUGIM prijemnicama istog predmeta. Ova prijemnica se dodaje u programu
   (stavke se menjaju pre snimanja).
   --------------------------------------------------------------------------------------------- */
CREATE OR ALTER PROCEDURE dbo.sp_Prijem_Kontrola
    @RN           int,
    @IDPrijemnice int = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT n.ID, n.RB, RTRIM(CONVERT(nvarchar(500), n.Naimenovanje)) AS Naimenovanje, n.NHM,
           RTRIM(CONVERT(nvarchar(50), nh.Broj)) AS NHMBroj,
           ISNULL(n.Koleta, 0) AS CarinaKoleta, ISNULL(n.Neto, 0) AS CarinaNeto,
           ISNULL(n.Bruto, 0) AS CarinaBruto, ISNULL(n.Vrednost, 0) AS CarinaVrednost,
           ISNULL(r.Koleta, 0) AS RanijeKoleta, ISNULL(r.Neto, 0) AS RanijeNeto,
           ISNULL(r.Bruto, 0) AS RanijeBruto, ISNULL(r.Vrednost, 0) AS RanijeVrednost,
           ISNULL(n.PDV, 0) AS PDV, ISNULL(n.Carina, 0) AS Carina   -- ISP-07: prenose se na stavku pri uparivanju
    FROM dbo.RNCarinskoStavke n
    JOIN dbo.MagacinskiPredmet mp ON mp.ID = n.IDNadredjena
    LEFT JOIN dbo.NHM nh ON RTRIM(CONVERT(nvarchar(50), nh.Broj)) = RTRIM(CONVERT(nvarchar(50), n.NHM))   -- RAZ-03: NHM je tarifni broj, ne ID
    OUTER APPLY (SELECT SUM(s.Koleta) AS Koleta, SUM(s.Neto) AS Neto, SUM(s.Bruto) AS Bruto, SUM(s.Vrednost) AS Vrednost
                 FROM dbo.RNCarinskoPrijemnicaStavke s
                 JOIN dbo.RNCarinskoSkladistePrijemnica p ON p.ID = s.IDNadredjena
                 WHERE p.RN = @RN AND p.ID <> ISNULL(@IDPrijemnice, 0) AND s.IDNaimenovanja = n.ID) r
    WHERE mp.RN = @RN
    ORDER BY n.RB, n.ID;
END
GO

/* ---------------------------------------------------------------------------------------------
   9. Uvoz artikala kataloga (Klasifikacija artikala > Uvoz artikala).
   Fajl: I–IV NIVO (šifra + naziv), ŠIFRA ARTIKLA, ARTIKAL, JM, opciono JM po koletu.
   Program upisuje redove u ArtikliKatalogUvoz pa poziva sp_ArtikliKatalog_Uvezi:
     - greške (prazna/ponovljena šifra, bez naziva, nepotpuna klasifikacija) -> ništa se ne upisuje;
     - nedostajući nivoi se prave (sp_Artikli_NivoiZaR);
     - nov artikal (Šifra + Katalog ne postoji) se dodaje;
     - postojećem artiklu istog kataloga se dodeljuje klasifikacija iz fajla i ažuriraju naziv i JM;
       JM po koletu samo ako je u fajlu.
   --------------------------------------------------------------------------------------------- */
IF OBJECT_ID('dbo.ArtikliKatalogUvoz', 'U') IS NULL
    CREATE TABLE dbo.ArtikliKatalogUvoz (
        ID          int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ArtikliKatalogUvoz PRIMARY KEY,
        Uvoz        uniqueidentifier NOT NULL,
        RedUFajlu   int            NOT NULL,
        Nivo1Sifra  nvarchar(50)   NULL,
        Nivo1Naziv  nvarchar(200)  NULL,
        Nivo2Sifra  nvarchar(50)   NULL,
        Nivo2Naziv  nvarchar(200)  NULL,
        Nivo3Sifra  nvarchar(50)   NULL,
        Nivo3Naziv  nvarchar(200)  NULL,
        Nivo4Sifra  nvarchar(50)   NULL,
        Nivo4Naziv  nvarchar(200)  NULL,
        Sifra       nvarchar(50)   NULL,
        Artikal     nvarchar(250)  NULL,
        JM          nvarchar(20)   NULL,
        Koeficijent decimal(18,6)  NULL);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID('dbo.ArtikliKatalogUvoz') AND name = 'IX_ArtikliKatalogUvoz_Uvoz')
    CREATE INDEX IX_ArtikliKatalogUvoz_Uvoz ON dbo.ArtikliKatalogUvoz (Uvoz);
GO

CREATE OR ALTER PROCEDURE dbo.sp_ArtikliKatalog_Uvezi
    @Uvoz       uniqueidentifier,
    @Katalog    nvarchar(30),
    @Novih      int OUTPUT,
    @Izmenjenih int OUTPUT,
    @Nivoa      int OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    SELECT @Novih = 0, @Izmenjenih = 0, @Nivoa = 0;

    IF NOT EXISTS (SELECT 1 FROM dbo.ArtikliKatalog WHERE Katalog = @Katalog)
    BEGIN
        DELETE FROM dbo.ArtikliKatalogUvoz WHERE Uvoz = @Uvoz;
        RAISERROR(N'Katalog %s ne postoji.', 16, 1, @Katalog);
        RETURN;
    END

    SELECT u.RedUFajlu, LTRIM(RTRIM(ISNULL(u.Sifra, N''))) AS Sifra, LTRIM(RTRIM(ISNULL(u.Artikal, N''))) AS Artikal,
           NULLIF(LTRIM(RTRIM(ISNULL(u.JM, N''))), N'') AS JM, u.Koeficijent,
           NULLIF(LTRIM(RTRIM(ISNULL(u.Nivo1Sifra, N''))), N'') AS S1, NULLIF(LTRIM(RTRIM(ISNULL(u.Nivo1Naziv, N''))), N'') AS N1,
           NULLIF(LTRIM(RTRIM(ISNULL(u.Nivo2Sifra, N''))), N'') AS S2, NULLIF(LTRIM(RTRIM(ISNULL(u.Nivo2Naziv, N''))), N'') AS N2,
           NULLIF(LTRIM(RTRIM(ISNULL(u.Nivo3Sifra, N''))), N'') AS S3, NULLIF(LTRIM(RTRIM(ISNULL(u.Nivo3Naziv, N''))), N'') AS N3,
           NULLIF(LTRIM(RTRIM(ISNULL(u.Nivo4Sifra, N''))), N'') AS S4, NULLIF(LTRIM(RTRIM(ISNULL(u.Nivo4Naziv, N''))), N'') AS N4
    INTO #u
    FROM dbo.ArtikliKatalogUvoz u
    WHERE u.Uvoz = @Uvoz;

    DELETE FROM dbo.ArtikliKatalogUvoz WHERE Uvoz = @Uvoz;

    -- Provera fajla: ako ima grešaka, ništa se ne upisuje
    SELECT RedUFajlu, Poruka INTO #g FROM (
        SELECT RedUFajlu, N'nema šifre artikla' AS Poruka FROM #u WHERE Sifra = N''
        UNION ALL SELECT RedUFajlu, N'nema naziva artikla' FROM #u WHERE Artikal = N''
        UNION ALL SELECT RedUFajlu, N'šifra ' + Sifra + N' se ponavlja u fajlu' FROM #u
                  WHERE Sifra <> N'' AND Sifra IN (SELECT Sifra FROM #u GROUP BY Sifra HAVING COUNT(*) > 1)
        UNION ALL SELECT RedUFajlu, N'klasifikacija nije potpuna (nivoi 1–4)' FROM #u
                  WHERE ISNULL(S1, N1) IS NULL OR ISNULL(S2, N2) IS NULL OR ISNULL(S3, N3) IS NULL OR ISNULL(S4, N4) IS NULL) g;

    IF EXISTS (SELECT 1 FROM #g)
    BEGIN
        DECLARE @poruka nvarchar(2000) =
            (SELECT TOP 15 N'Red ' + CONVERT(nvarchar(10), RedUFajlu) + N': ' + Poruka + NCHAR(10)
             FROM #g ORDER BY RedUFajlu FOR XML PATH(''), TYPE).value('.', 'nvarchar(2000)');
        DECLARE @broj int = (SELECT COUNT(*) FROM #g);
        RAISERROR(N'Fajl ima %d grešaka, ništa nije upisano:%s%s', 16, 1, @broj, N'
', @poruka);
        RETURN;
    END

    BEGIN TRAN;

    SELECT RedUFajlu AS ID, S1, ISNULL(N1, S1) AS N1, S2, ISNULL(N2, S2) AS N2, S3, ISNULL(N3, S3) AS N3, S4, ISNULL(N4, S4) AS N4,
           CAST(NULL AS int) AS ID1, CAST(NULL AS int) AS ID2, CAST(NULL AS int) AS ID3, CAST(NULL AS int) AS ID4,
           Sifra, Artikal, JM, Koeficijent
    INTO #r
    FROM #u;

    DECLARE @nivoaPre int = (SELECT COUNT(*) FROM dbo.ArtikliPrviNivo) + (SELECT COUNT(*) FROM dbo.ArtikliDrugiNivo)
                          + (SELECT COUNT(*) FROM dbo.ArtikliTreciNivo) + (SELECT COUNT(*) FROM dbo.ArtikliCetvrtiNivo);
    EXEC dbo.sp_Artikli_NivoiZaR;
    SET @Nivoa = (SELECT COUNT(*) FROM dbo.ArtikliPrviNivo) + (SELECT COUNT(*) FROM dbo.ArtikliDrugiNivo)
               + (SELECT COUNT(*) FROM dbo.ArtikliTreciNivo) + (SELECT COUNT(*) FROM dbo.ArtikliCetvrtiNivo) - @nivoaPre;

    -- postojeći artikli kataloga: klasifikacija, naziv i JM iz fajla; JM po koletu samo ako je dat
    UPDATE a
    SET IDNadredjena = r.ID4, Artikal = r.Artikal
    FROM dbo.Artikli a
    JOIN #r r ON LTRIM(RTRIM(CONVERT(nvarchar(100), a.Sifra))) COLLATE DATABASE_DEFAULT = r.Sifra
    WHERE a.Katalog = @Katalog;
    SET @Izmenjenih = @@ROWCOUNT;

    UPDATE a SET JM = r.JM
    FROM dbo.Artikli a
    JOIN #r r ON LTRIM(RTRIM(CONVERT(nvarchar(100), a.Sifra))) COLLATE DATABASE_DEFAULT = r.Sifra
    WHERE a.Katalog = @Katalog AND r.JM IS NOT NULL;

    UPDATE a SET KoeficijentKoletaJM = r.Koeficijent
    FROM dbo.Artikli a
    JOIN #r r ON LTRIM(RTRIM(CONVERT(nvarchar(100), a.Sifra))) COLLATE DATABASE_DEFAULT = r.Sifra
    WHERE a.Katalog = @Katalog AND r.Koeficijent IS NOT NULL;

    SELECT r.Sifra, r.Artikal, r.JM, r.Koeficijent, r.ID4 AS IDNadredjena, @Katalog AS Katalog
    INTO #a
    FROM #r r
    WHERE NOT EXISTS (SELECT 1 FROM dbo.Artikli x
                      WHERE x.Katalog = @Katalog
                        AND LTRIM(RTRIM(CONVERT(nvarchar(100), x.Sifra))) COLLATE DATABASE_DEFAULT = r.Sifra);

    IF COLUMNPROPERTY(OBJECT_ID('dbo.Artikli'), 'ID', 'IsIdentity') = 1
        INSERT INTO dbo.Artikli (Sifra, Artikal, JM, KoeficijentKoletaJM, IDNadredjena, Katalog)
        SELECT Sifra, Artikal, JM, Koeficijent, IDNadredjena, Katalog FROM #a;
    ELSE
        EXEC sp_executesql N'INSERT INTO dbo.Artikli (ID, Sifra, Artikal, JM, KoeficijentKoletaJM, IDNadredjena, Katalog)
            SELECT (SELECT ISNULL(MAX(ID), 0) FROM dbo.Artikli) + ROW_NUMBER() OVER (ORDER BY Sifra), Sifra, Artikal, JM, Koeficijent, IDNadredjena, Katalog FROM #a';
    SET @Novih = (SELECT COUNT(*) FROM #a);

    COMMIT;
END
GO

/* =============================================================================================
   10. Palete stavki Prijemnice (Carina-Prijemnica-Palete-Specifikacija v0.4)
   Paleta ima jedinstven broj od 13 cifara (brojač na celu bazu, početak iz podešavanja).
   Jedna stavka može biti na više paleta, jedna paleta može nositi više stavki (mešana paleta).
   Stavka: Paleta = broj paleta sa stavkom, VrstaPaleta = vrsta palete sa najviše koleta stavke.
   Otvorena pitanja (podrazumevano): OP-21 13 cifara, stavka nije u broju; OP-22 kontejner se unosi u panelu;
   OP-23 koleta po paleti se unosi uz predlog; OP-24 mešana paleta samo za isto naimenovanje; OP-25 pozicije kasnije.
   ============================================================================================= */
IF OBJECT_ID('dbo.RNCarinskoPaleta', 'U') IS NULL
    CREATE TABLE dbo.RNCarinskoPaleta (
        ID             int IDENTITY(1,1) NOT NULL CONSTRAINT PK_RNCarinskoPaleta PRIMARY KEY,
        IDPrijemnice   int           NOT NULL,
        BrojPalete     bigint        NOT NULL CONSTRAINT UQ_RNCarinskoPaleta_Broj UNIQUE,
        VrstaPaleta    int           NOT NULL,
        BrojKontejnera nvarchar(30)  NULL,
        Pozicija       nvarchar(40)  NULL,
        Status         tinyint       NOT NULL CONSTRAINT DF_RNCarinskoPaleta_Status DEFAULT (1),   -- 1 Planirana, 2 Pozicionirana
        Korisnik       nvarchar(50)  NULL,
        DatumUnosa     datetime      NOT NULL CONSTRAINT DF_RNCarinskoPaleta_DatumUnosa DEFAULT (GETDATE()),
        DatumIzmene    datetime      NULL,
        CONSTRAINT CK_RNCarinskoPaleta_Broj CHECK (BrojPalete BETWEEN 1000000000000 AND 9999999999999),
        CONSTRAINT CK_RNCarinskoPaleta_Status CHECK (Status IN (1, 2, 3)));   -- 3 Izdata (Carina_Izlaz_2026_11.sql)
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID('dbo.RNCarinskoPaleta') AND name = 'IX_RNCarinskoPaleta_Prijemnica')
    CREATE INDEX IX_RNCarinskoPaleta_Prijemnica ON dbo.RNCarinskoPaleta (IDPrijemnice);
GO
IF OBJECT_ID('dbo.RNCarinskoPaletaStavka', 'U') IS NULL
    CREATE TABLE dbo.RNCarinskoPaletaStavka (
        IDPalete int NOT NULL CONSTRAINT FK_RNCarinskoPaletaStavka_Paleta REFERENCES dbo.RNCarinskoPaleta (ID),
        IDStavke int NOT NULL,       -- RNCarinskoPrijemnicaStavke.ID
        Koleta   int NOT NULL CONSTRAINT CK_RNCarinskoPaletaStavka_Koleta CHECK (Koleta > 0),
        CONSTRAINT PK_RNCarinskoPaletaStavka PRIMARY KEY (IDPalete, IDStavke));
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID('dbo.RNCarinskoPaletaStavka') AND name = 'IX_RNCarinskoPaletaStavka_Stavka')
    CREATE INDEX IX_RNCarinskoPaletaStavka_Stavka ON dbo.RNCarinskoPaletaStavka (IDStavke);
GO
IF OBJECT_ID('dbo.RNCarinskoPaletaBrojac', 'U') IS NULL
    CREATE TABLE dbo.RNCarinskoPaletaBrojac (
        ID        tinyint NOT NULL CONSTRAINT PK_RNCarinskoPaletaBrojac PRIMARY KEY CONSTRAINT CK_RNCarinskoPaletaBrojac_ID CHECK (ID = 1),
        Poslednji bigint  NOT NULL);
GO
IF OBJECT_ID('dbo.Podesavanja', 'U') IS NOT NULL
    INSERT dbo.Podesavanja (Grupa, Kljuc, Vrednost, TipVrednosti, Opis)
    SELECT p.Grupa, p.Kljuc, p.Vrednost, p.Tip, p.Opis
    FROM (VALUES
        (N'Carina', N'Carina.Palete.PocetakBroja', N'1000000000000', 'TEXT', N'Prvi broj palete (13 cifara); brojač ne ide ispod ove vrednosti (CAR-PL09)'),
        (N'Carina', N'Carina.Palete.KoletaPoPaleti', N'60', 'INT', N'Predlog koleta po paleti kad artikal još nema paleta (CAR-PL09)'),
        (N'Carina', N'Carina.Polja.GrupaPoljaID', NULL, 'TEXT', N'GrupaPoljaID iz Skladista koje se nude za palete, odvojeni zarezom (prazno = sva aktivna polja, CAR-PP02)'),
        (N'Carina', N'Carina.Polja.ObaveznoZaPotvrdu', N'0', 'BOOL', N'1 = prijemnica se ne potvrđuje dok svaka paleta nema polje (CAR-PP08)')
    ) p (Grupa, Kljuc, Vrednost, Tip, Opis)
    WHERE NOT EXISTS (SELECT 1 FROM dbo.Podesavanja x WHERE x.Kljuc = p.Kljuc);
GO

/* ---------------------------------------------------------------------------------------------
   10a. Polje palete (Carina-Prijemnica-Polja-Specifikacija v0.5.1): polje = red u Skladista.
   Paleta: IDSkladista + Pozicija (naziv polja), Status 2 = Na polju. Svaka dodela i uklanjanje u logu.
   --------------------------------------------------------------------------------------------- */
IF COL_LENGTH('dbo.RNCarinskoPaleta', 'IDSkladista') IS NULL
    ALTER TABLE dbo.RNCarinskoPaleta ADD IDSkladista int NULL;
IF COL_LENGTH('dbo.RNCarinskoPaleta', 'DatumPolja') IS NULL
    ALTER TABLE dbo.RNCarinskoPaleta ADD DatumPolja datetime NULL;
IF COL_LENGTH('dbo.RNCarinskoPaleta', 'KorisnikPolja') IS NULL
    ALTER TABLE dbo.RNCarinskoPaleta ADD KorisnikPolja nvarchar(50) NULL;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID('dbo.RNCarinskoPaleta') AND name = 'IX_RNCarinskoPaleta_Skladiste')
    CREATE INDEX IX_RNCarinskoPaleta_Skladiste ON dbo.RNCarinskoPaleta (IDSkladista);
GO
IF OBJECT_ID('dbo.RNCarinskoPaletaPoljeLog', 'U') IS NULL
    CREATE TABLE dbo.RNCarinskoPaletaPoljeLog (
        ID               int IDENTITY(1,1) NOT NULL CONSTRAINT PK_RNCarinskoPaletaPoljeLog PRIMARY KEY,
        IDPalete         int          NOT NULL,
        IDSkladistaStaro int          NULL,
        IDSkladistaNovo  int          NULL,
        Korisnik         nvarchar(50) NULL,
        Datum            datetime     NOT NULL CONSTRAINT DF_RNCarinskoPaletaPoljeLog_Datum DEFAULT (GETDATE()));
GO

-- Rezerviše @Broj uzastopnih brojeva paleta; poziva se u transakciji pozivaoca (brojevi se ne vraćaju, CAR-PL03)
CREATE OR ALTER PROCEDURE dbo.sp_Palete_UzmiBrojeve
    @Broj int,
    @Prvi bigint OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @pocetak bigint = 1000000000000, @v nvarchar(100);
    IF OBJECT_ID('dbo.Podesavanja', 'U') IS NOT NULL
        SELECT @v = LTRIM(RTRIM(Vrednost)) FROM dbo.Podesavanja WHERE Kljuc = N'Carina.Palete.PocetakBroja';
    IF @v IS NOT NULL AND LEN(@v) = 13 AND @v NOT LIKE N'%[^0-9]%' AND LEFT(@v, 1) <> N'0'
        SET @pocetak = CONVERT(bigint, @v);

    IF NOT EXISTS (SELECT 1 FROM dbo.RNCarinskoPaletaBrojac WITH (UPDLOCK, HOLDLOCK) WHERE ID = 1)
        INSERT dbo.RNCarinskoPaletaBrojac (ID, Poslednji) VALUES (1, @pocetak - 1);

    UPDATE dbo.RNCarinskoPaletaBrojac
    SET @Prvi = CASE WHEN Poslednji < @pocetak - 1 THEN @pocetak ELSE Poslednji + 1 END,
        Poslednji = CASE WHEN Poslednji < @pocetak - 1 THEN @pocetak - 1 ELSE Poslednji END + @Broj
    WHERE ID = 1;

    IF @Prvi + @Broj - 1 > 9999999999999
        RAISERROR(N'Brojač paleta bi prešao 13 cifara.', 16, 1);
END
GO

-- Stavka: Paleta = broj paleta, VrstaPaleta = vrsta sa najviše koleta (ostaje stara ako stavka nema paleta)
CREATE OR ALTER PROCEDURE dbo.sp_Palete_Preracunaj
    @IDPrijemnice int
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE s
    SET Paleta = (SELECT COUNT(*) FROM dbo.RNCarinskoPaletaStavka ps WHERE ps.IDStavke = s.ID),
        VrstaPaleta = ISNULL((SELECT TOP 1 p.VrstaPaleta
                              FROM dbo.RNCarinskoPaletaStavka ps
                              JOIN dbo.RNCarinskoPaleta p ON p.ID = ps.IDPalete
                              WHERE ps.IDStavke = s.ID
                              GROUP BY p.VrstaPaleta
                              ORDER BY SUM(ps.Koleta) DESC, p.VrstaPaleta), s.VrstaPaleta)
    FROM dbo.RNCarinskoPrijemnicaStavke s
    WHERE s.IDNadredjena = @IDPrijemnice;

    -- CAR-PP06: polje stavke = polje njenih paleta sa najviše koleta stavke (kod jednakih: polje palete sa
    -- najmanjim brojem); stavka čije palete nemaju polje -> prazno; stavka bez paleta se ne dira
    UPDATE s
    SET Pozicija = ISNULL((SELECT TOP 1 RTRIM(CONVERT(nvarchar(200), sk.Naziv))
                           FROM dbo.RNCarinskoPaletaStavka ps
                           JOIN dbo.RNCarinskoPaleta p ON p.ID = ps.IDPalete
                           JOIN dbo.Skladista sk ON sk.ID = p.IDSkladista
                           WHERE ps.IDStavke = s.ID
                           GROUP BY p.IDSkladista, sk.Naziv
                           ORDER BY SUM(ps.Koleta) DESC, MIN(p.BrojPalete)), N'')
    FROM dbo.RNCarinskoPrijemnicaStavke s
    WHERE s.IDNadredjena = @IDPrijemnice
      AND EXISTS (SELECT 1 FROM dbo.RNCarinskoPaletaStavka ps WHERE ps.IDStavke = s.ID);
END
GO

-- Zapis o izmeni paleta potvrđene prijemnice (CAR-PL08)
CREATE OR ALTER PROCEDURE dbo.sp_Palete_ZapisKorekcije
    @IDPrijemnice int,
    @IDStavke     int,
    @Opis         nvarchar(1000),
    @Korisnik     nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM dbo.RNCarinskoSkladistePrijemnica WHERE ID = @IDPrijemnice AND RTRIM(Status) = N'ZA')
        INSERT dbo.RNCarinskoPrijemnicaKorekcija (IDPrijemnice, IDStavke, Akcija, Opis, Korisnik)
        VALUES (@IDPrijemnice, @IDStavke, N'Palete', @Opis, @Korisnik);
END
GO

-- CAR-PL04: briše samo planirane palete stavke (iz mešane palete uklanja samo deo te stavke)
CREATE OR ALTER PROCEDURE dbo.sp_Palete_ObrisiPlanirane
    @IDStavke int,
    @Korisnik nvarchar(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @IDPrijemnice int = (SELECT IDNadredjena FROM dbo.RNCarinskoPrijemnicaStavke WHERE ID = @IDStavke);
    DECLARE @broj int;

    -- RAZ-13 (Carina_Izlaz_2026_11.sql): planirana paleta sa izlazom ili rezervacijom ostaje
    IF OBJECT_ID('dbo.RNCarinskoPaletaIzlaz', 'U') IS NOT NULL
        IF EXISTS (SELECT 1 FROM dbo.RNCarinskoPaletaIzlaz x JOIN dbo.RNCarinskoPaleta p ON p.ID = x.IDPalete
                   WHERE x.IDStavke = @IDStavke AND p.Status = 1)
        BEGIN RAISERROR(N'Stavka ima palete izabrane za otpremu; palete se ne brišu dok se otprema ne oslobodi.', 16, 1); RETURN; END

    BEGIN TRAN;
    DELETE ps
    FROM dbo.RNCarinskoPaletaStavka ps
    JOIN dbo.RNCarinskoPaleta p ON p.ID = ps.IDPalete
    WHERE ps.IDStavke = @IDStavke AND p.Status = 1;
    SET @broj = @@ROWCOUNT;

    DELETE p
    FROM dbo.RNCarinskoPaleta p
    WHERE p.IDPrijemnice = @IDPrijemnice AND p.Status = 1
      AND NOT EXISTS (SELECT 1 FROM dbo.RNCarinskoPaletaStavka ps WHERE ps.IDPalete = p.ID);

    IF @broj > 0
    BEGIN
        DECLARE @opis nvarchar(1000) = N'Obrisane planirane palete stavke: ' + CONVERT(nvarchar(10), @broj);
        EXEC dbo.sp_Palete_ZapisKorekcije @IDPrijemnice, @IDStavke, @opis, @Korisnik;
    END
    EXEC dbo.sp_Palete_Preracunaj @IDPrijemnice;
    COMMIT;
END
GO

-- Pravi CEIL(@Koleta / @KoletaPoPaleti) paleta za stavku: sve pune, poslednja nosi ostatak.
-- Za jednu paletu („Dodaj paletu“): @KoletaPoPaleti = @Koleta.
CREATE OR ALTER PROCEDURE dbo.sp_Palete_Napravi
    @IDStavke       int,
    @VrstaPaleta    int,
    @KoletaPoPaleti int,
    @Koleta         int,
    @BrojKontejnera nvarchar(30) = NULL,
    @Korisnik       nvarchar(50) = NULL,
    @Napravljeno    int = NULL OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @IDPrijemnice int = (SELECT IDNadredjena FROM dbo.RNCarinskoPrijemnicaStavke WHERE ID = @IDStavke);

    IF @IDPrijemnice IS NULL
    BEGIN RAISERROR(N'Stavka prijemnice ne postoji (snimite stavke pre dodele paleta).', 16, 1); RETURN; END
    IF NOT EXISTS (SELECT 1 FROM dbo.TipPalete WHERE ID = @VrstaPaleta)
    BEGIN RAISERROR(N'Izaberite vrstu palete.', 16, 1); RETURN; END
    IF ISNULL(@KoletaPoPaleti, 0) <= 0 OR ISNULL(@Koleta, 0) <= 0
    BEGIN RAISERROR(N'Koleta i koleta po paleti moraju biti veći od nule.', 16, 1); RETURN; END

    DECLARE @n int = (@Koleta + @KoletaPoPaleti - 1) / @KoletaPoPaleti, @prvi bigint;

    BEGIN TRAN;
    EXEC dbo.sp_Palete_UzmiBrojeve @n, @prvi OUTPUT;

    ;WITH br AS (SELECT TOP (@n) ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS i
                 FROM sys.all_columns a CROSS JOIN sys.all_columns b)
    INSERT dbo.RNCarinskoPaleta (IDPrijemnice, BrojPalete, VrstaPaleta, BrojKontejnera, Status, Korisnik)
    SELECT @IDPrijemnice, @prvi + i - 1, @VrstaPaleta, NULLIF(LTRIM(RTRIM(@BrojKontejnera)), N''), 1, @Korisnik FROM br;

    INSERT dbo.RNCarinskoPaletaStavka (IDPalete, IDStavke, Koleta)
    SELECT p.ID, @IDStavke,
           CASE WHEN p.BrojPalete - @prvi + 1 < @n THEN @KoletaPoPaleti ELSE @Koleta - (@n - 1) * @KoletaPoPaleti END
    FROM dbo.RNCarinskoPaleta p
    WHERE p.BrojPalete BETWEEN @prvi AND @prvi + @n - 1;

    DECLARE @opis nvarchar(1000) = N'Napravljeno paleta: ' + CONVERT(nvarchar(10), @n) + N', koleta ' + CONVERT(nvarchar(10), @Koleta)
                                 + ISNULL(N', kontejner ' + NULLIF(LTRIM(RTRIM(@BrojKontejnera)), N''), N'');
    EXEC dbo.sp_Palete_ZapisKorekcije @IDPrijemnice, @IDStavke, @opis, @Korisnik;
    EXEC dbo.sp_Palete_Preracunaj @IDPrijemnice;
    COMMIT;
    SET @Napravljeno = @n;
END
GO

-- Izbacuje paletu (samo planiranu, CAR-PL04)
CREATE OR ALTER PROCEDURE dbo.sp_Palete_Izbaci
    @IDPalete int,
    @Korisnik nvarchar(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @IDPrijemnice int, @status tinyint, @broj bigint;
    SELECT @IDPrijemnice = IDPrijemnice, @status = Status, @broj = BrojPalete FROM dbo.RNCarinskoPaleta WHERE ID = @IDPalete;
    IF @IDPrijemnice IS NULL RETURN;
    IF @status <> 1
    BEGIN RAISERROR(N'Paleta je na polju i ne može da se izbaci; prvo Ukloni polje (CAR-PP04).', 16, 1); RETURN; END
    IF OBJECT_ID('dbo.RNCarinskoPaletaIzlaz', 'U') IS NOT NULL   -- RAZ-13
        IF EXISTS (SELECT 1 FROM dbo.RNCarinskoPaletaIzlaz WHERE IDPalete = @IDPalete)
        BEGIN RAISERROR(N'Paleta je izabrana za otpremu ili izdata i ne može da se izbaci.', 16, 1); RETURN; END

    BEGIN TRAN;
    DELETE FROM dbo.RNCarinskoPaletaStavka WHERE IDPalete = @IDPalete;
    DELETE FROM dbo.RNCarinskoPaleta WHERE ID = @IDPalete;
    DECLARE @opis nvarchar(1000) = N'Izbačena paleta ' + CONVERT(nvarchar(20), @broj);
    EXEC dbo.sp_Palete_ZapisKorekcije @IDPrijemnice, NULL, @opis, @Korisnik;
    EXEC dbo.sp_Palete_Preracunaj @IDPrijemnice;
    COMMIT;
END
GO

-- Menja koleta stavke na paleti; 0 uklanja stavku sa palete (prazna planirana paleta se briše)
CREATE OR ALTER PROCEDURE dbo.sp_Palete_IzmeniKoleta
    @IDPalete int,
    @IDStavke int,
    @Koleta   int,
    @Korisnik nvarchar(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @IDPrijemnice int = (SELECT IDPrijemnice FROM dbo.RNCarinskoPaleta WHERE ID = @IDPalete);
    IF @IDPrijemnice IS NULL RETURN;
    IF @Koleta < 0
    BEGIN RAISERROR(N'Koleta ne mogu biti negativna.', 16, 1); RETURN; END
    IF EXISTS (SELECT 1 FROM dbo.RNCarinskoPaleta WHERE ID = @IDPalete AND Status <> 1)
    BEGIN RAISERROR(N'Paleta je na polju; koleta se ne menjaju dok se ne Ukloni polje (CAR-PP04).', 16, 1); RETURN; END
    IF OBJECT_ID('dbo.RNCarinskoPaletaIzlaz', 'U') IS NOT NULL   -- RAZ-13
        IF EXISTS (SELECT 1 FROM dbo.RNCarinskoPaletaIzlaz WHERE IDPalete = @IDPalete)
        BEGIN RAISERROR(N'Paleta je izabrana za otpremu ili izdata; koleta se ne menjaju.', 16, 1); RETURN; END

    BEGIN TRAN;
    IF @Koleta = 0
        DELETE FROM dbo.RNCarinskoPaletaStavka WHERE IDPalete = @IDPalete AND IDStavke = @IDStavke;
    ELSE
        UPDATE dbo.RNCarinskoPaletaStavka SET Koleta = @Koleta WHERE IDPalete = @IDPalete AND IDStavke = @IDStavke;

    UPDATE dbo.RNCarinskoPaleta SET DatumIzmene = GETDATE(), Korisnik = ISNULL(@Korisnik, Korisnik) WHERE ID = @IDPalete;
    DELETE FROM dbo.RNCarinskoPaleta
    WHERE ID = @IDPalete AND Status = 1 AND NOT EXISTS (SELECT 1 FROM dbo.RNCarinskoPaletaStavka WHERE IDPalete = @IDPalete);

    DECLARE @opis nvarchar(1000) = N'Koleta na paleti ' + CONVERT(nvarchar(10), @IDPalete) + N': ' + CONVERT(nvarchar(10), @Koleta);
    EXEC dbo.sp_Palete_ZapisKorekcije @IDPrijemnice, @IDStavke, @opis, @Korisnik;
    EXEC dbo.sp_Palete_Preracunaj @IDPrijemnice;
    COMMIT;
END
GO

-- Spaja dve ili više planiranih paleta iste prijemnice u jednu (mešana paleta, CAR-PL06; OP-24: isto naimenovanje).
-- @Palete: ID paleta odvojeni zarezom. Koleta iste stavke se sabiraju.
CREATE OR ALTER PROCEDURE dbo.sp_Palete_Spoji
    @Palete      nvarchar(max),
    @VrstaPaleta int = NULL,
    @Korisnik    nvarchar(50) = NULL,
    @NovaPaleta  bigint = NULL OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @ids TABLE (ID int PRIMARY KEY);
    DECLARE @x xml = CAST(N'<i>' + REPLACE(REPLACE(@Palete, N' ', N''), N',', N'</i><i>') + N'</i>' AS xml);
    INSERT @ids (ID)
    SELECT DISTINCT t.c.value('.', 'int') FROM @x.nodes('/i') t(c) WHERE t.c.value('.', 'nvarchar(20)') <> N'';

    IF (SELECT COUNT(*) FROM @ids) < 2
    BEGIN RAISERROR(N'Izaberite bar dve palete za spajanje.', 16, 1); RETURN; END
    IF EXISTS (SELECT 1 FROM @ids i WHERE NOT EXISTS (SELECT 1 FROM dbo.RNCarinskoPaleta p WHERE p.ID = i.ID))
    BEGIN RAISERROR(N'Neka od izabranih paleta više ne postoji.', 16, 1); RETURN; END
    IF (SELECT COUNT(DISTINCT p.IDPrijemnice) FROM dbo.RNCarinskoPaleta p JOIN @ids i ON i.ID = p.ID) > 1
    BEGIN RAISERROR(N'Spajaju se samo palete iste prijemnice.', 16, 1); RETURN; END
    IF EXISTS (SELECT 1 FROM dbo.RNCarinskoPaleta p JOIN @ids i ON i.ID = p.ID WHERE p.Status <> 1)
    BEGIN RAISERROR(N'Palete na polju se ne spajaju; prvo Ukloni polje (CAR-PP04).', 16, 1); RETURN; END
    IF OBJECT_ID('dbo.RNCarinskoPaletaIzlaz', 'U') IS NOT NULL   -- RAZ-13
        IF EXISTS (SELECT 1 FROM dbo.RNCarinskoPaletaIzlaz x JOIN @ids i ON i.ID = x.IDPalete)
        BEGIN RAISERROR(N'Paleta izabrana za otpremu ili izdata se ne spaja.', 16, 1); RETURN; END
    IF (SELECT COUNT(DISTINCT ISNULL(s.IDNaimenovanja, -1))
        FROM dbo.RNCarinskoPaletaStavka ps
        JOIN @ids i ON i.ID = ps.IDPalete
        JOIN dbo.RNCarinskoPrijemnicaStavke s ON s.ID = ps.IDStavke) > 1
    BEGIN RAISERROR(N'Mešana paleta sme da sadrži samo stavke istog naimenovanja.', 16, 1); RETURN; END

    DECLARE @IDPrijemnice int, @vrsta int, @kontejner nvarchar(30), @prvi bigint, @idNove int;
    SELECT TOP 1 @IDPrijemnice = p.IDPrijemnice, @vrsta = ISNULL(@VrstaPaleta, p.VrstaPaleta)
    FROM dbo.RNCarinskoPaleta p JOIN @ids i ON i.ID = p.ID ORDER BY p.BrojPalete;
    IF (SELECT COUNT(DISTINCT ISNULL(p.BrojKontejnera, N'')) FROM dbo.RNCarinskoPaleta p JOIN @ids i ON i.ID = p.ID) = 1
        SELECT TOP 1 @kontejner = p.BrojKontejnera FROM dbo.RNCarinskoPaleta p JOIN @ids i ON i.ID = p.ID;

    BEGIN TRAN;
    EXEC dbo.sp_Palete_UzmiBrojeve 1, @prvi OUTPUT;
    INSERT dbo.RNCarinskoPaleta (IDPrijemnice, BrojPalete, VrstaPaleta, BrojKontejnera, Status, Korisnik)
    VALUES (@IDPrijemnice, @prvi, @vrsta, @kontejner, 1, @Korisnik);
    SET @idNove = SCOPE_IDENTITY();

    INSERT dbo.RNCarinskoPaletaStavka (IDPalete, IDStavke, Koleta)
    SELECT @idNove, ps.IDStavke, SUM(ps.Koleta)
    FROM dbo.RNCarinskoPaletaStavka ps JOIN @ids i ON i.ID = ps.IDPalete
    GROUP BY ps.IDStavke;

    DELETE ps FROM dbo.RNCarinskoPaletaStavka ps JOIN @ids i ON i.ID = ps.IDPalete;
    DELETE p FROM dbo.RNCarinskoPaleta p JOIN @ids i ON i.ID = p.ID;

    DECLARE @opis nvarchar(1000) = N'Spojeno ' + CONVERT(nvarchar(10), (SELECT COUNT(*) FROM @ids)) + N' paleta u paletu ' + CONVERT(nvarchar(20), @prvi);
    EXEC dbo.sp_Palete_ZapisKorekcije @IDPrijemnice, NULL, @opis, @Korisnik;
    EXEC dbo.sp_Palete_Preracunaj @IDPrijemnice;
    COMMIT;
    SET @NovaPaleta = @prvi;
END
GO

/* ---------------------------------------------------------------------------------------------
   10b. Dodela polja paletama (v0.5.1). @ListaPaleta: ID paleta odvojeni zarezom.
   --------------------------------------------------------------------------------------------- */
-- Polja koja se nude (CAR-PP02) sa brojem paleta trenutno na svakom (informativno, sve prijemnice)
CREATE OR ALTER PROCEDURE dbo.sp_Polja_ZaPalete
    @IDPrijemnice int = NULL
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @grupe nvarchar(max);
    IF OBJECT_ID('dbo.Podesavanja', 'U') IS NOT NULL
        SELECT @grupe = REPLACE(REPLACE(LTRIM(RTRIM(Vrednost)), N' ', N''), N';', N',')
        FROM dbo.Podesavanja WHERE Kljuc = N'Carina.Polja.GrupaPoljaID';

    DECLARE @g TABLE (ID int PRIMARY KEY);
    IF ISNULL(@grupe, N'') <> N''
    BEGIN
        DECLARE @x xml = CAST(N'<i>' + REPLACE(@grupe, N',', N'</i><i>') + N'</i>' AS xml);
        INSERT @g (ID)
        SELECT DISTINCT t.c.value('.', 'int') FROM @x.nodes('/i') t(c) WHERE t.c.value('.', 'nvarchar(20)') <> N'';
    END

    SELECT sk.ID, RTRIM(CONVERT(nvarchar(200), sk.Naziv)) AS Naziv,
           (SELECT COUNT(*) FROM dbo.RNCarinskoPaleta p WHERE p.IDSkladista = sk.ID AND p.Status <> 3) AS BrojPaleta
    FROM dbo.Skladista sk
    WHERE CONVERT(int, ISNULL(sk.Aktivan, 1)) = 1
      AND (NOT EXISTS (SELECT 1 FROM @g) OR sk.GrupaPoljaID IN (SELECT ID FROM @g))
    ORDER BY Naziv;
END
GO

-- Upisuje polje i status Na polju (CAR-PP01). Posle potvrde (ZA) samo paletama bez polja (CAR-PP07).
CREATE OR ALTER PROCEDURE dbo.sp_Palete_DodeliPolje
    @IDPrijemnice int,
    @ListaPaleta  nvarchar(max),
    @IDSkladista  int,
    @Korisnik     nvarchar(50) = NULL,
    @Dodeljeno    int = NULL OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    SET @Dodeljeno = 0;

    DECLARE @ids TABLE (ID int PRIMARY KEY);
    DECLARE @x xml = CAST(N'<i>' + REPLACE(REPLACE(@ListaPaleta, N' ', N''), N',', N'</i><i>') + N'</i>' AS xml);
    INSERT @ids (ID)
    SELECT DISTINCT t.c.value('.', 'int') FROM @x.nodes('/i') t(c) WHERE t.c.value('.', 'nvarchar(20)') <> N'';

    IF NOT EXISTS (SELECT 1 FROM @ids)
    BEGIN RAISERROR(N'Izaberite palete.', 16, 1); RETURN; END
    IF EXISTS (SELECT 1 FROM @ids i WHERE NOT EXISTS (SELECT 1 FROM dbo.RNCarinskoPaleta p WHERE p.ID = i.ID AND p.IDPrijemnice = @IDPrijemnice))
    BEGIN RAISERROR(N'Neka od izabranih paleta ne pripada ovoj prijemnici.', 16, 1); RETURN; END

    DECLARE @naziv nvarchar(200) = (SELECT RTRIM(CONVERT(nvarchar(200), Naziv)) FROM dbo.Skladista WHERE ID = @IDSkladista);
    IF @naziv IS NULL
    BEGIN RAISERROR(N'Izaberite polje.', 16, 1); RETURN; END

    IF EXISTS (SELECT 1 FROM dbo.RNCarinskoPaleta p JOIN @ids i ON i.ID = p.ID WHERE p.Status = 3)   -- RAZ-13: izdata paleta
    BEGIN RAISERROR(N'Izdata paleta nema polje. Izaberite samo palete koje su u skladištu.', 16, 1); RETURN; END

    DECLARE @za bit = CASE WHEN EXISTS (SELECT 1 FROM dbo.RNCarinskoSkladistePrijemnica WHERE ID = @IDPrijemnice AND RTRIM(Status) = N'ZA') THEN 1 ELSE 0 END;
    IF @za = 1 AND EXISTS (SELECT 1 FROM dbo.RNCarinskoPaleta p JOIN @ids i ON i.ID = p.ID
                           WHERE p.IDSkladista IS NOT NULL AND p.IDSkladista <> @IDSkladista)
    BEGIN RAISERROR(N'Promena polja posle potvrde je premeštanje i nije u ovoj fazi. Izaberite samo palete bez polja.', 16, 1); RETURN; END

    BEGIN TRAN;
    INSERT dbo.RNCarinskoPaletaPoljeLog (IDPalete, IDSkladistaStaro, IDSkladistaNovo, Korisnik)
    SELECT p.ID, p.IDSkladista, @IDSkladista, @Korisnik
    FROM dbo.RNCarinskoPaleta p JOIN @ids i ON i.ID = p.ID
    WHERE ISNULL(p.IDSkladista, -1) <> @IDSkladista;

    UPDATE p
    SET IDSkladista = @IDSkladista, Pozicija = LEFT(@naziv, 40), Status = 2,
        DatumPolja = GETDATE(), KorisnikPolja = @Korisnik, DatumIzmene = GETDATE()
    FROM dbo.RNCarinskoPaleta p JOIN @ids i ON i.ID = p.ID
    WHERE ISNULL(p.IDSkladista, -1) <> @IDSkladista;
    SET @Dodeljeno = @@ROWCOUNT;

    IF @Dodeljeno > 0
    BEGIN
        DECLARE @opis nvarchar(1000) = N'Polje ' + @naziv + N' dodeljeno paletama: ' + CONVERT(nvarchar(10), @Dodeljeno);
        EXEC dbo.sp_Palete_ZapisKorekcije @IDPrijemnice, NULL, @opis, @Korisnik;
    END
    EXEC dbo.sp_Palete_Preracunaj @IDPrijemnice;
    COMMIT;
END
GO

-- Briše polje (status Planirana). Posle potvrde nije dozvoljeno: to je premeštanje (CAR-PP07).
CREATE OR ALTER PROCEDURE dbo.sp_Palete_UkloniPolje
    @IDPrijemnice int,
    @ListaPaleta  nvarchar(max),
    @Korisnik     nvarchar(50) = NULL,
    @Uklonjeno    int = NULL OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    SET @Uklonjeno = 0;

    DECLARE @ids TABLE (ID int PRIMARY KEY);
    DECLARE @x xml = CAST(N'<i>' + REPLACE(REPLACE(@ListaPaleta, N' ', N''), N',', N'</i><i>') + N'</i>' AS xml);
    INSERT @ids (ID)
    SELECT DISTINCT t.c.value('.', 'int') FROM @x.nodes('/i') t(c) WHERE t.c.value('.', 'nvarchar(20)') <> N'';

    IF NOT EXISTS (SELECT 1 FROM @ids)
    BEGIN RAISERROR(N'Izaberite palete.', 16, 1); RETURN; END
    IF EXISTS (SELECT 1 FROM @ids i WHERE NOT EXISTS (SELECT 1 FROM dbo.RNCarinskoPaleta p WHERE p.ID = i.ID AND p.IDPrijemnice = @IDPrijemnice))
    BEGIN RAISERROR(N'Neka od izabranih paleta ne pripada ovoj prijemnici.', 16, 1); RETURN; END
    IF EXISTS (SELECT 1 FROM dbo.RNCarinskoSkladistePrijemnica WHERE ID = @IDPrijemnice AND RTRIM(Status) = N'ZA')
       AND EXISTS (SELECT 1 FROM dbo.RNCarinskoPaleta p JOIN @ids i ON i.ID = p.ID WHERE p.IDSkladista IS NOT NULL)
    BEGIN RAISERROR(N'Promena polja posle potvrde je premeštanje i nije u ovoj fazi.', 16, 1); RETURN; END

    BEGIN TRAN;
    INSERT dbo.RNCarinskoPaletaPoljeLog (IDPalete, IDSkladistaStaro, IDSkladistaNovo, Korisnik)
    SELECT p.ID, p.IDSkladista, NULL, @Korisnik
    FROM dbo.RNCarinskoPaleta p JOIN @ids i ON i.ID = p.ID
    WHERE p.IDSkladista IS NOT NULL;

    UPDATE p
    SET IDSkladista = NULL, Pozicija = NULL, Status = 1, DatumPolja = NULL, KorisnikPolja = NULL, DatumIzmene = GETDATE()
    FROM dbo.RNCarinskoPaleta p JOIN @ids i ON i.ID = p.ID
    WHERE (p.IDSkladista IS NOT NULL OR p.Status <> 1) AND p.Status <> 3;   -- RAZ-13: izdata paleta ostaje izdata
    SET @Uklonjeno = @@ROWCOUNT;

    EXEC dbo.sp_Palete_Preracunaj @IDPrijemnice;
    COMMIT;
END
GO

PRINT 'Carina_PrijemnicaPL_2026_11.sql: završeno.';
GO
