/* =============================================================================================
   Servis: Magacin rezervnih delova (dopuna modula Servis i održavanje sredstava)
   Specifikacija: Servis-Magacin rezervnih delova-Specifikacija.md (verzija 0.1, 09.11.2026.)

   Skripta se može pokretati više puta: tabele se dodaju samo ako ne postoje, šifarnici se pune
   samo kad su prazni, pogledi, funkcije i procedure se prave sa CREATE OR ALTER.

   Sve nove tabele imaju prefiks SER. Postojeće tabele koje se koriste (samo se dodaju redovi):
     Partnerji (PaSifra)                 dobavljač dela i narudžbenice, sa FK (preskače se ako PaSifra nije PK/UNIQUE)
     Podesavanja (Kljuc)                 ključevi Servis.*: formati brojeva, nivoi odobrenja
     MainNovi, mainNoviKartice,
     MainNoviPrava                       modul 'Održavanje' i četiri prava iz poglavlja 7

   Osnovne tabele servisa ne postoje u bazi, pa se prave u minimalnom obliku (proširuju se kad se
   radi ostatak modula Servis): SERVrstaSredstva, SERSredstvo, SERPlanStavka, SERRadniNalog,
   SERRadniNalogStavka.

   Razlike u odnosu na specifikaciju:
     - Dokumenti (IZD, PRM, POV, POP, KOR, PST) imaju zaglavlje SERDokument; SERDeoKretanje pokazuje
       na zaglavlje (DokumentID) umesto kolone DokumentBroj.
     - Dodati tipovi kretanja: KOR (korekcija, MD-R11) i PST (početno stanje, OP-MD-04).
     - SERKorekcija čuva zahtev za korekciju do odobrenja; SERObavestenje čuva obaveštenja (MD-R08);
       SERLog je log promena (MD-R11); SERBrojac daje redne brojeve dokumenata.
     - Statusi su tinyint; nazivi za prikaz su u SERStatus (oblast + status).
     - Predlog nabavke (MD-R06) računa i delove koji su na nalozima u statusu „Nema na stanju“
       (MD-R09): uslov je raspoloživo − nedostaje + naručeno < minimum. Kad ništa ne nedostaje,
       isto je kao u specifikaciji.
     - Naručeno = neisporučen ostatak narudžbenica u statusu Nacrt, Čeka odobrenje, Poručeno
       i Delimično isporučeno (da predlog ne ponudi isti deo dva puta).
     - Obaveštenje o padu ispod minimuma (MD-R08) pravi se samo za kritične delove.
     - Ko odobrava narudžbenicu (MD-R07) određuje aplikacija: procedura prima nivo korisnika.

   Statusi:
     RadniNalog    1 Otvoren, 2 U radu, 3 Čeka delove, 4 Završen, 5 Storniran
     Rezervacija   1 Rezervisano, 2 Izdato, 3 Nema na stanju, 4 Storno
     Narudzbenica  1 Nacrt, 2 Čeka odobrenje, 3 Poručeno, 4 Delimično isporučeno, 5 Isporučeno, 6 Otkazano
     Popis         1 U toku, 2 Čeka odobrenje, 3 Odobren, 4 Storniran
     Korekcija     1 Čeka odobrenje, 2 Odobrena, 3 Odbijena
     NivoOdobrenja 1 Šef tehničke službe, 2 Tehnički direktor, 3 Direktor
   ============================================================================================= */

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

-- Pomoćna procedura: dodaje strani ključ ako ne postoji. Ako referencirana kolona nije PK/UNIQUE,
-- ključ se preskače uz poruku; aplikacija svejedno radi.
IF OBJECT_ID('tempdb..#SER_DodajFK') IS NOT NULL DROP PROCEDURE #SER_DodajFK;
GO
CREATE PROCEDURE #SER_DodajFK @Naziv sysname, @Tabela sysname, @Kolona sysname, @RefTabela sysname, @RefKolona sysname
AS
BEGIN
    IF OBJECT_ID(@Naziv, 'F') IS NOT NULL OR OBJECT_ID(@Tabela, 'U') IS NULL OR OBJECT_ID(@RefTabela, 'U') IS NULL
        RETURN;
    DECLARE @sql nvarchar(max) = N'ALTER TABLE dbo.' + QUOTENAME(@Tabela) + N' WITH CHECK ADD CONSTRAINT ' + QUOTENAME(@Naziv)
        + N' FOREIGN KEY (' + QUOTENAME(@Kolona) + N') REFERENCES dbo.' + QUOTENAME(@RefTabela) + N' (' + QUOTENAME(@RefKolona) + N');';
    BEGIN TRY
        EXEC sp_executesql @sql;
    END TRY
    BEGIN CATCH
        PRINT N'FK ' + @Naziv + N' nije dodat: ' + ERROR_MESSAGE();
    END CATCH
END
GO

/* ---------------------------------------------------------------------------------------------
   1. Šifarnici statusa i tipova dokumenata, brojač, log
   --------------------------------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SERStatus', 'U') IS NULL
    CREATE TABLE dbo.SERStatus (
        Oblast varchar(20)  NOT NULL,
        Status tinyint      NOT NULL,
        Naziv  nvarchar(40) NOT NULL,
        CONSTRAINT PK_SERStatus PRIMARY KEY (Oblast, Status));
GO
INSERT dbo.SERStatus (Oblast, Status, Naziv)
SELECT v.Oblast, v.Status, v.Naziv
FROM (VALUES ('RadniNalog', 1, N'Otvoren'), ('RadniNalog', 2, N'U radu'), ('RadniNalog', 3, N'Čeka delove'),
             ('RadniNalog', 4, N'Završen'), ('RadniNalog', 5, N'Storniran'),
             ('Rezervacija', 1, N'Rezervisano'), ('Rezervacija', 2, N'Izdato'), ('Rezervacija', 3, N'Nema na stanju'), ('Rezervacija', 4, N'Storno'),
             ('Narudzbenica', 1, N'Nacrt'), ('Narudzbenica', 2, N'Čeka odobrenje'), ('Narudzbenica', 3, N'Poručeno'),
             ('Narudzbenica', 4, N'Delimično isporučeno'), ('Narudzbenica', 5, N'Isporučeno'), ('Narudzbenica', 6, N'Otkazano'),
             ('Popis', 1, N'U toku'), ('Popis', 2, N'Čeka odobrenje'), ('Popis', 3, N'Odobren'), ('Popis', 4, N'Storniran'),
             ('Korekcija', 1, N'Čeka odobrenje'), ('Korekcija', 2, N'Odobrena'), ('Korekcija', 3, N'Odbijena'),
             ('NivoOdobrenja', 1, N'Šef tehničke službe'), ('NivoOdobrenja', 2, N'Tehnički direktor'), ('NivoOdobrenja', 3, N'Direktor')
     ) v (Oblast, Status, Naziv)
WHERE NOT EXISTS (SELECT 1 FROM dbo.SERStatus s WHERE s.Oblast = v.Oblast AND s.Status = v.Status);
GO

IF OBJECT_ID('dbo.SERDokumentTip', 'U') IS NULL
    CREATE TABLE dbo.SERDokumentTip (
        Tip   char(3)      NOT NULL CONSTRAINT PK_SERDokumentTip PRIMARY KEY,
        Naziv nvarchar(40) NOT NULL);
GO
INSERT dbo.SERDokumentTip (Tip, Naziv)
SELECT v.Tip, v.Naziv
FROM (VALUES ('PRM', N'Prijem'), ('IZD', N'Izdavanje'), ('POV', N'Povrat'), ('POP', N'Popis'),
             ('KOR', N'Korekcija'), ('PST', N'Početno stanje')) v (Tip, Naziv)
WHERE NOT EXISTS (SELECT 1 FROM dbo.SERDokumentTip t WHERE t.Tip = v.Tip);
GO

IF OBJECT_ID('dbo.SERBrojac', 'U') IS NULL
    CREATE TABLE dbo.SERBrojac (
        Kljuc     nvarchar(100) NOT NULL,
        Godina    smallint      NOT NULL,          -- 0 kad format nema {GGGG}
        Poslednji int           NOT NULL,
        CONSTRAINT PK_SERBrojac PRIMARY KEY (Kljuc, Godina));
GO

IF OBJECT_ID('dbo.SERLog', 'U') IS NULL
    CREATE TABLE dbo.SERLog (
        ID       bigint IDENTITY(1,1) NOT NULL CONSTRAINT PK_SERLog PRIMARY KEY,
        Vreme    datetime2     NOT NULL CONSTRAINT DF_SERLog_Vreme DEFAULT sysdatetime(),
        Korisnik nvarchar(20)  NULL,
        Tabela   sysname       NOT NULL,
        ZapisID  int           NOT NULL,
        Akcija   nvarchar(30)  NOT NULL,
        Opis     nvarchar(500) NULL);
GO

/* ---------------------------------------------------------------------------------------------
   2. Osnovne tabele servisa (minimalno, dok se ne uradi ostatak modula Servis)
   --------------------------------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SERVrstaSredstva', 'U') IS NULL
    CREATE TABLE dbo.SERVrstaSredstva (
        ID              int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SERVrstaSredstva PRIMARY KEY,
        Naziv           nvarchar(100) NOT NULL CONSTRAINT UQ_SERVrstaSredstva_Naziv UNIQUE,
        Napomena        nvarchar(500) NULL,
        Aktivno         bit           NOT NULL CONSTRAINT DF_SERVrstaSredstva_Aktivno DEFAULT 1,
        KreiraoKorisnik nvarchar(20) NULL, DatumKreiranja datetime2 NOT NULL CONSTRAINT DF_SERVrstaSredstva_DK DEFAULT sysdatetime(),
        IzmenioKorisnik nvarchar(20) NULL, DatumIzmene datetime2 NULL, RowVer rowversion);
GO

IF OBJECT_ID('dbo.SERSredstvo', 'U') IS NULL
    CREATE TABLE dbo.SERSredstvo (
        ID              int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SERSredstvo PRIMARY KEY,
        Oznaka          nvarchar(30)  NOT NULL CONSTRAINT UQ_SERSredstvo_Oznaka UNIQUE,
        Naziv           nvarchar(100) NOT NULL,
        VrstaSredstvaID int           NOT NULL CONSTRAINT FK_SERSredstvo_Vrsta REFERENCES dbo.SERVrstaSredstva (ID),
        SerijskiBroj    nvarchar(50)  NULL,
        Napomena        nvarchar(500) NULL,
        Aktivno         bit           NOT NULL CONSTRAINT DF_SERSredstvo_Aktivno DEFAULT 1,
        KreiraoKorisnik nvarchar(20) NULL, DatumKreiranja datetime2 NOT NULL CONSTRAINT DF_SERSredstvo_DK DEFAULT sysdatetime(),
        IzmenioKorisnik nvarchar(20) NULL, DatumIzmene datetime2 NULL, RowVer rowversion);
GO

-- Stavka plana održavanja: jedan servis za vrstu sredstva (npr. „Servis 500 mč“)
IF OBJECT_ID('dbo.SERPlanStavka', 'U') IS NULL
    CREATE TABLE dbo.SERPlanStavka (
        ID              int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SERPlanStavka PRIMARY KEY,
        VrstaSredstvaID int           NOT NULL CONSTRAINT FK_SERPlanStavka_Vrsta REFERENCES dbo.SERVrstaSredstva (ID),
        Naziv           nvarchar(100) NOT NULL,
        IntervalDana    int           NULL,
        IntervalSati    int           NULL,
        IntervalKm      int           NULL,
        Napomena        nvarchar(500) NULL,
        Aktivno         bit           NOT NULL CONSTRAINT DF_SERPlanStavka_Aktivno DEFAULT 1,
        KreiraoKorisnik nvarchar(20) NULL, DatumKreiranja datetime2 NOT NULL CONSTRAINT DF_SERPlanStavka_DK DEFAULT sysdatetime(),
        IzmenioKorisnik nvarchar(20) NULL, DatumIzmene datetime2 NULL, RowVer rowversion);
GO

-- P14: servis iz plana mora imati bar jedan interval veći od 0 (NOCHECK: ne dira zapise koji su već upisani bez intervala)
IF OBJECT_ID('dbo.CK_SERPlanStavka_Interval', 'C') IS NULL
    ALTER TABLE dbo.SERPlanStavka WITH NOCHECK
        ADD CONSTRAINT CK_SERPlanStavka_Interval CHECK (ISNULL(IntervalDana, 0) > 0 OR ISNULL(IntervalSati, 0) > 0 OR ISNULL(IntervalKm, 0) > 0);
GO

IF OBJECT_ID('dbo.SERRadniNalog', 'U') IS NULL
    CREATE TABLE dbo.SERRadniNalog (
        ID              int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SERRadniNalog PRIMARY KEY,
        Broj            nvarchar(20)  NOT NULL CONSTRAINT UQ_SERRadniNalog_Broj UNIQUE,
        Datum           date          NOT NULL,
        SredstvoID      int           NOT NULL CONSTRAINT FK_SERRadniNalog_Sredstvo REFERENCES dbo.SERSredstvo (ID),
        PlanStavkaID    int           NULL     CONSTRAINT FK_SERRadniNalog_PlanStavka REFERENCES dbo.SERPlanStavka (ID),
        Redovni         bit           NOT NULL CONSTRAINT DF_SERRadniNalog_Redovni DEFAULT 0,
        Opis            nvarchar(500) NULL,
        Status          tinyint       NOT NULL CONSTRAINT DF_SERRadniNalog_Status DEFAULT 1
                                               CONSTRAINT CK_SERRadniNalog_Status CHECK (Status BETWEEN 1 AND 5),
        DatumZavrsetka  date          NULL,
        Napomena        nvarchar(500) NULL,
        KreiraoKorisnik nvarchar(20) NULL, DatumKreiranja datetime2 NOT NULL CONSTRAINT DF_SERRadniNalog_DK DEFAULT sysdatetime(),
        IzmenioKorisnik nvarchar(20) NULL, DatumIzmene datetime2 NULL, RowVer rowversion);
GO

IF OBJECT_ID('dbo.SERRadniNalogStavka', 'U') IS NULL
    CREATE TABLE dbo.SERRadniNalogStavka (
        ID              int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SERRadniNalogStavka PRIMARY KEY,
        RadniNalogID    int           NOT NULL CONSTRAINT FK_SERRadniNalogStavka_Nalog REFERENCES dbo.SERRadniNalog (ID),
        Opis            nvarchar(200) NOT NULL,
        Napomena        nvarchar(500) NULL,
        KreiraoKorisnik nvarchar(20) NULL, DatumKreiranja datetime2 NOT NULL CONSTRAINT DF_SERRadniNalogStavka_DK DEFAULT sysdatetime(),
        IzmenioKorisnik nvarchar(20) NULL, DatumIzmene datetime2 NULL, RowVer rowversion);
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_SERRadniNalogStavka_Nalog' AND object_id = OBJECT_ID('dbo.SERRadniNalogStavka'))
    CREATE INDEX IX_SERRadniNalogStavka_Nalog ON dbo.SERRadniNalogStavka (RadniNalogID);
GO

/* ---------------------------------------------------------------------------------------------
   3. Šifarnik delova
   --------------------------------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SERDeoGrupa', 'U') IS NULL
    CREATE TABLE dbo.SERDeoGrupa (
        ID              int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SERDeoGrupa PRIMARY KEY,
        Naziv           nvarchar(100) NOT NULL CONSTRAINT UQ_SERDeoGrupa_Naziv UNIQUE,
        Aktivno         bit           NOT NULL CONSTRAINT DF_SERDeoGrupa_Aktivno DEFAULT 1,
        KreiraoKorisnik nvarchar(20) NULL, DatumKreiranja datetime2 NOT NULL CONSTRAINT DF_SERDeoGrupa_DK DEFAULT sysdatetime(),
        IzmenioKorisnik nvarchar(20) NULL, DatumIzmene datetime2 NULL, RowVer rowversion);
IF NOT EXISTS (SELECT 1 FROM dbo.SERDeoGrupa)
    INSERT dbo.SERDeoGrupa (Naziv)
    VALUES (N'Kočnice'), (N'Filteri'), (N'Maziva i gorivo'), (N'Hidraulika'), (N'Elektro'), (N'Gume'),
           (N'Zaptivke'), (N'Oprema za kontejnere'), (N'Ostalo');
GO

IF OBJECT_ID('dbo.SERDeo', 'U') IS NULL
    CREATE TABLE dbo.SERDeo (
        ID              int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SERDeo PRIMARY KEY,
        Sifra           nvarchar(20)   NOT NULL CONSTRAINT UQ_SERDeo_Sifra UNIQUE,
        Naziv           nvarchar(150)  NOT NULL,
        GrupaID         int            NULL     CONSTRAINT FK_SERDeo_Grupa REFERENCES dbo.SERDeoGrupa (ID),
        JM              nvarchar(10)   NOT NULL CONSTRAINT DF_SERDeo_JM DEFAULT N'kom',
        MinZaliha       decimal(18,3)  NOT NULL CONSTRAINT DF_SERDeo_Min DEFAULT 0,
        MaxZaliha       decimal(18,3)  NULL,
        ProsecnaCena    decimal(18,4)  NOT NULL CONSTRAINT DF_SERDeo_Cena DEFAULT 0,   -- menjaju je samo procedure (prijem, povrat)
        Lokacija        nvarchar(30)   NULL,
        Kritican        bit            NOT NULL CONSTRAINT DF_SERDeo_Kritican DEFAULT 0,
        DobavljacID     int            NULL,                                           -- Partnerji.PaSifra
        RokIsporukeDana int            NULL,
        Napomena        nvarchar(500)  NULL,
        Aktivno         bit            NOT NULL CONSTRAINT DF_SERDeo_Aktivno DEFAULT 1,
        KreiraoKorisnik nvarchar(20) NULL, DatumKreiranja datetime2 NOT NULL CONSTRAINT DF_SERDeo_DK DEFAULT sysdatetime(),
        IzmenioKorisnik nvarchar(20) NULL, DatumIzmene datetime2 NULL, RowVer rowversion,
        CONSTRAINT CK_SERDeo_Min CHECK (MinZaliha >= 0),
        CONSTRAINT CK_SERDeo_Max CHECK (MaxZaliha IS NULL OR MaxZaliha >= MinZaliha),
        CONSTRAINT CK_SERDeo_Cena CHECK (ProsecnaCena >= 0));
GO
EXEC #SER_DodajFK N'FK_SERDeo_Dobavljac', N'SERDeo', N'DobavljacID', N'Partnerji', N'PaSifra';
GO

IF OBJECT_ID('dbo.SERDeoPrimenljivost', 'U') IS NULL
    CREATE TABLE dbo.SERDeoPrimenljivost (
        ID              int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SERDeoPrimenljivost PRIMARY KEY,
        DeoID           int NOT NULL CONSTRAINT FK_SERDeoPrimenljivost_Deo REFERENCES dbo.SERDeo (ID),
        VrstaSredstvaID int NOT NULL CONSTRAINT FK_SERDeoPrimenljivost_Vrsta REFERENCES dbo.SERVrstaSredstva (ID),
        SredstvoID      int NULL     CONSTRAINT FK_SERDeoPrimenljivost_Sredstvo REFERENCES dbo.SERSredstvo (ID),
        KreiraoKorisnik nvarchar(20) NULL, DatumKreiranja datetime2 NOT NULL CONSTRAINT DF_SERDeoPrimenljivost_DK DEFAULT sysdatetime(),
        IzmenioKorisnik nvarchar(20) NULL, DatumIzmene datetime2 NULL, RowVer rowversion);
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UX_SERDeoPrimenljivost' AND object_id = OBJECT_ID('dbo.SERDeoPrimenljivost'))
    CREATE UNIQUE INDEX UX_SERDeoPrimenljivost ON dbo.SERDeoPrimenljivost (DeoID, VrstaSredstvaID, SredstvoID);
GO

-- Normativ delova po servisu (plan održavanja)
IF OBJECT_ID('dbo.SERPlanNormativ', 'U') IS NULL
    CREATE TABLE dbo.SERPlanNormativ (
        ID              int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SERPlanNormativ PRIMARY KEY,
        PlanStavkaID    int           NOT NULL CONSTRAINT FK_SERPlanNormativ_PlanStavka REFERENCES dbo.SERPlanStavka (ID),
        DeoID           int           NOT NULL CONSTRAINT FK_SERPlanNormativ_Deo REFERENCES dbo.SERDeo (ID),
        Kolicina        decimal(18,3) NOT NULL CONSTRAINT CK_SERPlanNormativ_Kolicina CHECK (Kolicina > 0),
        KreiraoKorisnik nvarchar(20) NULL, DatumKreiranja datetime2 NOT NULL CONSTRAINT DF_SERPlanNormativ_DK DEFAULT sysdatetime(),
        IzmenioKorisnik nvarchar(20) NULL, DatumIzmene datetime2 NULL, RowVer rowversion,
        CONSTRAINT UQ_SERPlanNormativ UNIQUE (PlanStavkaID, DeoID));
GO

/* ---------------------------------------------------------------------------------------------
   4. Narudžbenice
   --------------------------------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SERNarudzbenica', 'U') IS NULL
    CREATE TABLE dbo.SERNarudzbenica (
        ID              int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SERNarudzbenica PRIMARY KEY,
        Broj            nvarchar(20)  NOT NULL CONSTRAINT UQ_SERNarudzbenica_Broj UNIQUE,
        Datum           date          NOT NULL,
        DobavljacID     int           NOT NULL,                                        -- Partnerji.PaSifra
        OcekivaniDatum  date          NULL,
        Status          tinyint       NOT NULL CONSTRAINT DF_SERNarudzbenica_Status DEFAULT 1
                                               CONSTRAINT CK_SERNarudzbenica_Status CHECK (Status BETWEEN 1 AND 6),
        PotrebanNivo    tinyint       NULL     CONSTRAINT CK_SERNarudzbenica_Nivo CHECK (PotrebanNivo BETWEEN 1 AND 3),
        OdobrioKorisnik nvarchar(20)  NULL,
        DatumOdobrenja  datetime2     NULL,
        Napomena        nvarchar(500) NULL,
        KreiraoKorisnik nvarchar(20) NULL, DatumKreiranja datetime2 NOT NULL CONSTRAINT DF_SERNarudzbenica_DK DEFAULT sysdatetime(),
        IzmenioKorisnik nvarchar(20) NULL, DatumIzmene datetime2 NULL, RowVer rowversion);
GO
EXEC #SER_DodajFK N'FK_SERNarudzbenica_Dobavljac', N'SERNarudzbenica', N'DobavljacID', N'Partnerji', N'PaSifra';
GO

IF OBJECT_ID('dbo.SERNarudzbenicaStavka', 'U') IS NULL
    CREATE TABLE dbo.SERNarudzbenicaStavka (
        ID              int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SERNarudzbenicaStavka PRIMARY KEY,
        NarudzbenicaID  int           NOT NULL CONSTRAINT FK_SERNarudzbenicaStavka_Nar REFERENCES dbo.SERNarudzbenica (ID),
        DeoID           int           NOT NULL CONSTRAINT FK_SERNarudzbenicaStavka_Deo REFERENCES dbo.SERDeo (ID),
        Kolicina        decimal(18,3) NOT NULL CONSTRAINT CK_SERNarudzbenicaStavka_Kol CHECK (Kolicina > 0),
        Cena            decimal(18,4) NOT NULL CONSTRAINT CK_SERNarudzbenicaStavka_Cena CHECK (Cena >= 0),
        Napomena        nvarchar(500) NULL,
        KreiraoKorisnik nvarchar(20) NULL, DatumKreiranja datetime2 NOT NULL CONSTRAINT DF_SERNarudzbenicaStavka_DK DEFAULT sysdatetime(),
        IzmenioKorisnik nvarchar(20) NULL, DatumIzmene datetime2 NULL, RowVer rowversion,
        CONSTRAINT UQ_SERNarudzbenicaStavka UNIQUE (NarudzbenicaID, DeoID));
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_SERNarudzbenicaStavka_Deo' AND object_id = OBJECT_ID('dbo.SERNarudzbenicaStavka'))
    CREATE INDEX IX_SERNarudzbenicaStavka_Deo ON dbo.SERNarudzbenicaStavka (DeoID);
GO

/* ---------------------------------------------------------------------------------------------
   5. Rezervacije, dokumenti i kartica dela
   --------------------------------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SERDeoRezervacija', 'U') IS NULL
    CREATE TABLE dbo.SERDeoRezervacija (
        ID                 int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SERDeoRezervacija PRIMARY KEY,
        RadniNalogStavkaID int           NOT NULL CONSTRAINT FK_SERDeoRezervacija_Stavka REFERENCES dbo.SERRadniNalogStavka (ID),
        DeoID              int           NOT NULL CONSTRAINT FK_SERDeoRezervacija_Deo REFERENCES dbo.SERDeo (ID),
        Kolicina           decimal(18,3) NOT NULL CONSTRAINT CK_SERDeoRezervacija_Kol CHECK (Kolicina > 0),
        Status             tinyint       NOT NULL CONSTRAINT DF_SERDeoRezervacija_Status DEFAULT 1
                                                  CONSTRAINT CK_SERDeoRezervacija_Status CHECK (Status BETWEEN 1 AND 4),
        Napomena           nvarchar(500) NULL,
        KreiraoKorisnik nvarchar(20) NULL, DatumKreiranja datetime2 NOT NULL CONSTRAINT DF_SERDeoRezervacija_DK DEFAULT sysdatetime(),
        IzmenioKorisnik nvarchar(20) NULL, DatumIzmene datetime2 NULL, RowVer rowversion);
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_SERDeoRezervacija_Deo' AND object_id = OBJECT_ID('dbo.SERDeoRezervacija'))
    CREATE INDEX IX_SERDeoRezervacija_Deo ON dbo.SERDeoRezervacija (DeoID, Status) INCLUDE (Kolicina, RadniNalogStavkaID);
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_SERDeoRezervacija_Stavka' AND object_id = OBJECT_ID('dbo.SERDeoRezervacija'))
    CREATE INDEX IX_SERDeoRezervacija_Stavka ON dbo.SERDeoRezervacija (RadniNalogStavkaID);
GO

IF OBJECT_ID('dbo.SERDokument', 'U') IS NULL
    CREATE TABLE dbo.SERDokument (
        ID              int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SERDokument PRIMARY KEY,
        Tip             char(3)       NOT NULL CONSTRAINT FK_SERDokument_Tip REFERENCES dbo.SERDokumentTip (Tip),
        Broj            nvarchar(20)  NOT NULL CONSTRAINT UQ_SERDokument_Broj UNIQUE,
        Datum           date          NOT NULL,
        RadniNalogID    int           NULL CONSTRAINT FK_SERDokument_Nalog REFERENCES dbo.SERRadniNalog (ID),
        NarudzbenicaID  int           NULL CONSTRAINT FK_SERDokument_Nar REFERENCES dbo.SERNarudzbenica (ID),
        Razlog          nvarchar(250) NULL,          -- prijem bez narudžbenice, korekcija
        Napomena        nvarchar(500) NULL,
        KreiraoKorisnik nvarchar(20) NULL, DatumKreiranja datetime2 NOT NULL CONSTRAINT DF_SERDokument_DK DEFAULT sysdatetime(),
        IzmenioKorisnik nvarchar(20) NULL, DatumIzmene datetime2 NULL, RowVer rowversion);
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_SERDokument_TipDatum' AND object_id = OBJECT_ID('dbo.SERDokument'))
    CREATE INDEX IX_SERDokument_TipDatum ON dbo.SERDokument (Tip, Datum);
GO

-- Kartica dela: jedini izvor stanja. Redovi se ne menjaju i ne brišu (triger ispod).
IF OBJECT_ID('dbo.SERDeoKretanje', 'U') IS NULL
    CREATE TABLE dbo.SERDeoKretanje (
        ID                   int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SERDeoKretanje PRIMARY KEY,
        DokumentID           int           NOT NULL CONSTRAINT FK_SERDeoKretanje_Dokument REFERENCES dbo.SERDokument (ID),
        DeoID                int           NOT NULL CONSTRAINT FK_SERDeoKretanje_Deo REFERENCES dbo.SERDeo (ID),
        Datum                date          NOT NULL,
        Tip                  char(3)       NOT NULL CONSTRAINT FK_SERDeoKretanje_Tip REFERENCES dbo.SERDokumentTip (Tip),
        Kolicina             decimal(18,3) NOT NULL CONSTRAINT CK_SERDeoKretanje_Kol CHECK (Kolicina <> 0),   -- ulaz +, izlaz −
        Cena                 decimal(18,4) NOT NULL CONSTRAINT CK_SERDeoKretanje_Cena CHECK (Cena >= 0),
        Vrednost             AS (CAST(Kolicina * Cena AS decimal(18,2))),
        RadniNalogID         int           NULL CONSTRAINT FK_SERDeoKretanje_Nalog REFERENCES dbo.SERRadniNalog (ID),
        RezervacijaID        int           NULL CONSTRAINT FK_SERDeoKretanje_Rez REFERENCES dbo.SERDeoRezervacija (ID),
        NarudzbenicaStavkaID int           NULL CONSTRAINT FK_SERDeoKretanje_NarStavka REFERENCES dbo.SERNarudzbenicaStavka (ID),
        IzvorKretanjeID      int           NULL CONSTRAINT FK_SERDeoKretanje_Izvor REFERENCES dbo.SERDeoKretanje (ID),  -- povrat → izdavanje
        Napomena             nvarchar(500) NULL,
        KreiraoKorisnik      nvarchar(20)  NULL,
        DatumKreiranja       datetime2     NOT NULL CONSTRAINT DF_SERDeoKretanje_DK DEFAULT sysdatetime());
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_SERDeoKretanje_Deo' AND object_id = OBJECT_ID('dbo.SERDeoKretanje'))
    CREATE INDEX IX_SERDeoKretanje_Deo ON dbo.SERDeoKretanje (DeoID, Datum, ID) INCLUDE (Kolicina, Cena, Tip);
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_SERDeoKretanje_Nalog' AND object_id = OBJECT_ID('dbo.SERDeoKretanje'))
    CREATE INDEX IX_SERDeoKretanje_Nalog ON dbo.SERDeoKretanje (RadniNalogID) INCLUDE (Kolicina, Cena, Tip);
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_SERDeoKretanje_NarStavka' AND object_id = OBJECT_ID('dbo.SERDeoKretanje'))
    CREATE INDEX IX_SERDeoKretanje_NarStavka ON dbo.SERDeoKretanje (NarudzbenicaStavkaID) INCLUDE (Kolicina);
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_SERDeoKretanje_Dokument' AND object_id = OBJECT_ID('dbo.SERDeoKretanje'))
    CREATE INDEX IX_SERDeoKretanje_Dokument ON dbo.SERDeoKretanje (DokumentID);
GO

/* ---------------------------------------------------------------------------------------------
   6. Popis, korekcija, obaveštenja
   --------------------------------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SERPopis', 'U') IS NULL
    CREATE TABLE dbo.SERPopis (
        ID              int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SERPopis PRIMARY KEY,
        Broj            nvarchar(20)  NOT NULL CONSTRAINT UQ_SERPopis_Broj UNIQUE,
        Datum           date          NOT NULL,
        GrupaID         int           NULL CONSTRAINT FK_SERPopis_Grupa REFERENCES dbo.SERDeoGrupa (ID),   -- NULL = svi delovi
        Status          tinyint       NOT NULL CONSTRAINT DF_SERPopis_Status DEFAULT 1
                                               CONSTRAINT CK_SERPopis_Status CHECK (Status BETWEEN 1 AND 4),
        OdobrioKorisnik nvarchar(20)  NULL,
        DatumOdobrenja  datetime2     NULL,
        DokumentID      int           NULL CONSTRAINT FK_SERPopis_Dokument REFERENCES dbo.SERDokument (ID),  -- POP dokument posle odobrenja
        Napomena        nvarchar(500) NULL,
        KreiraoKorisnik nvarchar(20) NULL, DatumKreiranja datetime2 NOT NULL CONSTRAINT DF_SERPopis_DK DEFAULT sysdatetime(),
        IzmenioKorisnik nvarchar(20) NULL, DatumIzmene datetime2 NULL, RowVer rowversion);
GO

IF OBJECT_ID('dbo.SERPopisStavka', 'U') IS NULL
    CREATE TABLE dbo.SERPopisStavka (
        ID                      int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SERPopisStavka PRIMARY KEY,
        PopisID                 int           NOT NULL CONSTRAINT FK_SERPopisStavka_Popis REFERENCES dbo.SERPopis (ID),
        DeoID                   int           NOT NULL CONSTRAINT FK_SERPopisStavka_Deo REFERENCES dbo.SERDeo (ID),
        KnjigovodstvenaKolicina decimal(18,3) NOT NULL,
        PrebrojanaKolicina      decimal(18,3) NULL CONSTRAINT CK_SERPopisStavka_Prebrojano CHECK (PrebrojanaKolicina >= 0),  -- NULL = nije brojano
        Razlika                 AS (PrebrojanaKolicina - KnjigovodstvenaKolicina),
        Cena                    decimal(18,4) NOT NULL,          -- prosečna cena kad je popis otvoren
        VrednostRazlike         AS (CAST((PrebrojanaKolicina - KnjigovodstvenaKolicina) * Cena AS decimal(18,2))),
        Napomena                nvarchar(500) NULL,
        KreiraoKorisnik nvarchar(20) NULL, DatumKreiranja datetime2 NOT NULL CONSTRAINT DF_SERPopisStavka_DK DEFAULT sysdatetime(),
        IzmenioKorisnik nvarchar(20) NULL, DatumIzmene datetime2 NULL, RowVer rowversion,
        CONSTRAINT UQ_SERPopisStavka UNIQUE (PopisID, DeoID));
GO

IF OBJECT_ID('dbo.SERKorekcija', 'U') IS NULL
    CREATE TABLE dbo.SERKorekcija (
        ID              int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SERKorekcija PRIMARY KEY,
        DeoID           int           NOT NULL CONSTRAINT FK_SERKorekcija_Deo REFERENCES dbo.SERDeo (ID),
        Kolicina        decimal(18,3) NOT NULL CONSTRAINT CK_SERKorekcija_Kol CHECK (Kolicina <> 0),   -- + višak, − manjak
        Razlog          nvarchar(250) NOT NULL CONSTRAINT CK_SERKorekcija_Razlog CHECK (LEN(Razlog) > 0),
        Status          tinyint       NOT NULL CONSTRAINT DF_SERKorekcija_Status DEFAULT 1
                                               CONSTRAINT CK_SERKorekcija_Status CHECK (Status BETWEEN 1 AND 3),
        OdobrioKorisnik nvarchar(20)  NULL,
        DatumOdobrenja  datetime2     NULL,
        DokumentID      int           NULL CONSTRAINT FK_SERKorekcija_Dokument REFERENCES dbo.SERDokument (ID),
        KreiraoKorisnik nvarchar(20) NULL, DatumKreiranja datetime2 NOT NULL CONSTRAINT DF_SERKorekcija_DK DEFAULT sysdatetime(),
        IzmenioKorisnik nvarchar(20) NULL, DatumIzmene datetime2 NULL, RowVer rowversion);
GO

IF OBJECT_ID('dbo.SERObavestenje', 'U') IS NULL
    CREATE TABLE dbo.SERObavestenje (
        ID               int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SERObavestenje PRIMARY KEY,
        DeoID            int           NOT NULL CONSTRAINT FK_SERObavestenje_Deo REFERENCES dbo.SERDeo (ID),
        Vreme            datetime2     NOT NULL CONSTRAINT DF_SERObavestenje_Vreme DEFAULT sysdatetime(),
        Tekst            nvarchar(300) NOT NULL,
        Procitano        bit           NOT NULL CONSTRAINT DF_SERObavestenje_Procitano DEFAULT 0,
        ProcitaoKorisnik nvarchar(20)  NULL,
        DatumCitanja     datetime2     NULL);
GO

-- Ko je poslao na odobrenje: odobravalac ne sme biti podnosilac (razdvajanje dužnosti, Servis.Delovi.ZabranaSamoodobravanja)
IF COL_LENGTH('dbo.SERNarudzbenica', 'PoslaoKorisnik') IS NULL
    ALTER TABLE dbo.SERNarudzbenica ADD PoslaoKorisnik nvarchar(20) NULL;
IF COL_LENGTH('dbo.SERPopis', 'PoslaoKorisnik') IS NULL
    ALTER TABLE dbo.SERPopis ADD PoslaoKorisnik nvarchar(20) NULL;
GO

/* ---------------------------------------------------------------------------------------------
   7. Trigeri: kartica se ne menja (MD-R11), deo sa kretanjem se ne briše (MD-R12)
   --------------------------------------------------------------------------------------------- */
CREATE OR ALTER TRIGGER dbo.TR_SERDeoKretanje_Zabrana ON dbo.SERDeoKretanje
INSTEAD OF UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM deleted)
        THROW 50001, N'Kartica dela se ne menja i ne briše. Ispravka se radi novim dokumentom (povrat, korekcija, popis).', 1;
END
GO

CREATE OR ALTER TRIGGER dbo.TR_SERDeo_Brisanje ON dbo.SERDeo
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM deleted d WHERE EXISTS (SELECT 1 FROM dbo.SERDeoKretanje k WHERE k.DeoID = d.ID))
        THROW 50002, N'Deo koji ima kretanje ne može da se obriše; deaktivirajte ga.', 1;
    DELETE p FROM dbo.SERDeoPrimenljivost p JOIN deleted d ON d.ID = p.DeoID;
    DELETE dbo.SERDeo WHERE ID IN (SELECT ID FROM deleted);
END
GO

/* ---------------------------------------------------------------------------------------------
   8. Stanje dela i pogledi
   --------------------------------------------------------------------------------------------- */

-- Stanje, rezervisano, raspoloživo i naručeno; @DeoID NULL = svi delovi
CREATE OR ALTER FUNCTION dbo.fnSER_Stanje (@DeoID int)
RETURNS TABLE
AS
RETURN
    SELECT d.ID AS DeoID,
           ISNULL(k.Stanje, 0)                              AS Stanje,
           ISNULL(r.Rezervisano, 0)                         AS Rezervisano,
           ISNULL(k.Stanje, 0) - ISNULL(r.Rezervisano, 0)   AS Raspolozivo,
           ISNULL(r.Nedostaje, 0)                           AS Nedostaje,
           ISNULL(n.Naruceno, 0)                            AS Naruceno
    FROM dbo.SERDeo d
    OUTER APPLY (SELECT SUM(x.Kolicina) AS Stanje FROM dbo.SERDeoKretanje x WHERE x.DeoID = d.ID) k
    OUTER APPLY (SELECT SUM(CASE WHEN rz.Status = 1 THEN rz.Kolicina END) AS Rezervisano,
                        SUM(CASE WHEN rz.Status = 3 THEN rz.Kolicina END) AS Nedostaje
                 FROM dbo.SERDeoRezervacija rz
                 JOIN dbo.SERRadniNalogStavka s ON s.ID = rz.RadniNalogStavkaID
                 JOIN dbo.SERRadniNalog rn ON rn.ID = s.RadniNalogID
                 WHERE rz.DeoID = d.ID AND rz.Status IN (1, 3) AND rn.Status IN (1, 2, 3)) r
    OUTER APPLY (SELECT SUM(CASE WHEN ns.Kolicina > ISNULL(p.Primljeno, 0) THEN ns.Kolicina - ISNULL(p.Primljeno, 0) ELSE 0 END) AS Naruceno
                 FROM dbo.SERNarudzbenicaStavka ns
                 JOIN dbo.SERNarudzbenica nr ON nr.ID = ns.NarudzbenicaID
                 OUTER APPLY (SELECT SUM(x.Kolicina) AS Primljeno FROM dbo.SERDeoKretanje x
                              WHERE x.NarudzbenicaStavkaID = ns.ID AND x.Tip = 'PRM') p
                 WHERE ns.DeoID = d.ID AND nr.Status IN (1, 2, 3, 4)) n
    WHERE @DeoID IS NULL OR d.ID = @DeoID;
GO

-- Zalihe i šifarnik: tabela delova sa stanjem, vrednošću i statusom
CREATE OR ALTER VIEW dbo.vSERDeoStanje
AS
SELECT d.ID, d.Sifra, d.Naziv, d.GrupaID, g.Naziv AS Grupa, d.JM,
       s.Stanje, s.Rezervisano, s.Raspolozivo, s.Nedostaje, s.Naruceno,
       d.MinZaliha, d.MaxZaliha, d.ProsecnaCena,
       CAST(s.Stanje * d.ProsecnaCena AS decimal(18,2)) AS Vrednost,
       d.Lokacija, d.Kritican, d.DobavljacID, p.PaNaziv AS Dobavljac, d.RokIsporukeDana, d.Aktivno,
       CAST(CASE WHEN s.Raspolozivo < d.MinZaliha THEN 1 ELSE 0 END AS bit) AS IspodMinimuma,
       CASE WHEN d.Aktivno = 0 THEN N'Neaktivan'
            WHEN s.Raspolozivo <= 0 AND d.MinZaliha > 0 THEN N'Nema na stanju'
            WHEN s.Raspolozivo < d.MinZaliha THEN N'Ispod minimuma'
            ELSE N'Na stanju' END AS StatusTekst
FROM dbo.SERDeo d
CROSS APPLY dbo.fnSER_Stanje(d.ID) s
LEFT JOIN dbo.SERDeoGrupa g ON g.ID = d.GrupaID
LEFT JOIN dbo.Partnerji p ON p.PaSifra = d.DobavljacID;
GO

-- Potrebe za nabavkom (MD-R06, MD-R09)
CREATE OR ALTER VIEW dbo.vSERPredlogNabavke
AS
SELECT x.ID AS DeoID, x.Sifra, x.Naziv, x.Grupa, x.JM, x.Stanje, x.Rezervisano, x.Raspolozivo, x.Nedostaje, x.Naruceno,
       x.MinZaliha, x.MaxZaliha, x.Kritican, x.DobavljacID, x.Dobavljac, x.RokIsporukeDana,
       q.PredlozenaKolicina,
       x.ProsecnaCena AS Cena,
       CAST(q.PredlozenaKolicina * x.ProsecnaCena AS decimal(18,2)) AS Iznos
FROM dbo.vSERDeoStanje x
CROSS APPLY (SELECT CASE WHEN ISNULL(x.MaxZaliha, x.MinZaliha) - (x.Raspolozivo - x.Nedostaje) - x.Naruceno > 0
                         THEN ISNULL(x.MaxZaliha, x.MinZaliha) - (x.Raspolozivo - x.Nedostaje) - x.Naruceno
                         ELSE 0 END AS PredlozenaKolicina) q
WHERE x.Aktivno = 1
  AND x.Raspolozivo - x.Nedostaje + x.Naruceno < x.MinZaliha;
GO

-- Kartica dela sa stanjem posle svakog kretanja
CREATE OR ALTER VIEW dbo.vSERKarticaDela
AS
SELECT k.ID, k.DeoID, d.Sifra, d.Naziv, k.Datum, k.Tip, t.Naziv AS TipNaziv, dok.Broj AS DokumentBroj, k.DokumentID,
       k.Kolicina, k.Cena, k.Vrednost,
       SUM(k.Kolicina) OVER (PARTITION BY k.DeoID ORDER BY k.Datum, k.ID ROWS UNBOUNDED PRECEDING) AS StanjePosle,
       rn.Broj AS RadniNalog, k.RadniNalogID, nr.Broj AS Narudzbenica, k.Napomena, k.KreiraoKorisnik, k.DatumKreiranja
FROM dbo.SERDeoKretanje k
JOIN dbo.SERDeo d ON d.ID = k.DeoID
JOIN dbo.SERDokument dok ON dok.ID = k.DokumentID
JOIN dbo.SERDokumentTip t ON t.Tip = k.Tip
LEFT JOIN dbo.SERRadniNalog rn ON rn.ID = k.RadniNalogID
LEFT JOIN dbo.SERNarudzbenica nr ON nr.ID = dok.NarudzbenicaID;
GO

-- Izdatnice i prijemi: dokumenti sa zbirom ulaza i izlaza
CREATE OR ALTER VIEW dbo.vSERPromet
AS
SELECT dok.ID, dok.Tip, t.Naziv AS TipNaziv, dok.Broj, dok.Datum, dok.RadniNalogID, rn.Broj AS RadniNalog,
       dok.NarudzbenicaID, nr.Broj AS Narudzbenica, nr.DobavljacID, p.PaNaziv AS Dobavljac, dok.Razlog, dok.Napomena,
       ISNULL(z.BrojStavki, 0) AS BrojStavki,
       ISNULL(z.Ulaz, 0)  AS VrednostUlaza,
       ISNULL(z.Izlaz, 0) AS VrednostIzlaza,
       dok.KreiraoKorisnik, dok.DatumKreiranja
FROM dbo.SERDokument dok
JOIN dbo.SERDokumentTip t ON t.Tip = dok.Tip
LEFT JOIN dbo.SERRadniNalog rn ON rn.ID = dok.RadniNalogID
LEFT JOIN dbo.SERNarudzbenica nr ON nr.ID = dok.NarudzbenicaID
LEFT JOIN dbo.Partnerji p ON p.PaSifra = nr.DobavljacID
OUTER APPLY (SELECT COUNT(*) AS BrojStavki,
                    SUM(CASE WHEN k.Kolicina > 0 THEN k.Vrednost END)  AS Ulaz,
                    SUM(CASE WHEN k.Kolicina < 0 THEN -k.Vrednost END) AS Izlaz
             FROM dbo.SERDeoKretanje k WHERE k.DokumentID = dok.ID) z;
GO

-- Radni nalog → Delovi iz magacina
CREATE OR ALTER VIEW dbo.vSERRadniNalogDelovi
AS
SELECT rz.ID AS RezervacijaID, s.RadniNalogID, rz.RadniNalogStavkaID, s.Opis AS Stavka,
       rz.DeoID, d.Sifra, d.Naziv, d.JM, rz.Kolicina, rz.Status, st.Naziv AS StatusTekst,
       sd.Raspolozivo,
       ISNULL(-i.Izdato, 0)  AS Izdato,
       ISNULL(i.Vraceno, 0)  AS Vraceno,
       ISNULL(i.Trosak, 0)   AS Trosak,
       rz.Napomena
FROM dbo.SERDeoRezervacija rz
JOIN dbo.SERRadniNalogStavka s ON s.ID = rz.RadniNalogStavkaID
JOIN dbo.SERDeo d ON d.ID = rz.DeoID
JOIN dbo.SERStatus st ON st.Oblast = 'Rezervacija' AND st.Status = rz.Status
CROSS APPLY dbo.fnSER_Stanje(rz.DeoID) sd
OUTER APPLY (SELECT SUM(CASE WHEN k.Tip = 'IZD' THEN k.Kolicina END) AS Izdato,
                    SUM(CASE WHEN k.Tip = 'POV' THEN k.Kolicina END) AS Vraceno,
                    SUM(-k.Vrednost) AS Trosak
             FROM dbo.SERDeoKretanje k WHERE k.RezervacijaID = rz.ID) i;
GO

-- Trošak delova po nalogu: samo izdati delovi, umanjeno za povrat (MD-R02, MD-R04)
CREATE OR ALTER VIEW dbo.vSERRadniNalogTrosak
AS
SELECT rn.ID AS RadniNalogID, rn.Broj, rn.Datum, rn.SredstvoID, sr.Oznaka AS Sredstvo, rn.Status, st.Naziv AS StatusTekst,
       ISNULL(t.TrosakDelova, 0) AS TrosakDelova
FROM dbo.SERRadniNalog rn
JOIN dbo.SERSredstvo sr ON sr.ID = rn.SredstvoID
JOIN dbo.SERStatus st ON st.Oblast = 'RadniNalog' AND st.Status = rn.Status
OUTER APPLY (SELECT SUM(-k.Vrednost) AS TrosakDelova FROM dbo.SERDeoKretanje k
             WHERE k.RadniNalogID = rn.ID AND k.Tip IN ('IZD', 'POV')) t;
GO

CREATE OR ALTER VIEW dbo.vSERNarudzbenicaStavka
AS
SELECT ns.ID, ns.NarudzbenicaID, ns.DeoID, d.Sifra, d.Naziv, d.JM, ns.Kolicina, ns.Cena,
       CAST(ns.Kolicina * ns.Cena AS decimal(18,2)) AS Iznos,
       ISNULL(p.Primljeno, 0) AS Primljeno,
       CASE WHEN ns.Kolicina > ISNULL(p.Primljeno, 0) THEN ns.Kolicina - ISNULL(p.Primljeno, 0) ELSE 0 END AS Preostalo,
       ns.Napomena
FROM dbo.SERNarudzbenicaStavka ns
JOIN dbo.SERDeo d ON d.ID = ns.DeoID
OUTER APPLY (SELECT SUM(k.Kolicina) AS Primljeno FROM dbo.SERDeoKretanje k
             WHERE k.NarudzbenicaStavkaID = ns.ID AND k.Tip = 'PRM') p;
GO

CREATE OR ALTER VIEW dbo.vSERNarudzbenica
AS
SELECT n.ID, n.Broj, n.Datum, n.DobavljacID, p.PaNaziv AS Dobavljac, n.OcekivaniDatum,
       n.Status, st.Naziv AS StatusTekst, n.PotrebanNivo, nv.Naziv AS PotrebanNivoTekst,
       n.OdobrioKorisnik, n.DatumOdobrenja, ISNULL(z.Iznos, 0) AS Iznos, ISNULL(z.BrojStavki, 0) AS BrojStavki,
       n.Napomena, n.KreiraoKorisnik, n.DatumKreiranja
FROM dbo.SERNarudzbenica n
JOIN dbo.SERStatus st ON st.Oblast = 'Narudzbenica' AND st.Status = n.Status
LEFT JOIN dbo.SERStatus nv ON nv.Oblast = 'NivoOdobrenja' AND nv.Status = n.PotrebanNivo
LEFT JOIN dbo.Partnerji p ON p.PaSifra = n.DobavljacID
OUTER APPLY (SELECT SUM(CAST(s.Kolicina * s.Cena AS decimal(18,2))) AS Iznos, COUNT(*) AS BrojStavki
             FROM dbo.SERNarudzbenicaStavka s WHERE s.NarudzbenicaID = n.ID) z;
GO

-- Popis zaliha: zaglavlje sa manjkom i viškom
CREATE OR ALTER VIEW dbo.vSERPopis
AS
SELECT p.ID, p.Broj, p.Datum, p.GrupaID, g.Naziv AS Grupa, p.Status, st.Naziv AS StatusTekst,
       ISNULL(z.BrojStavki, 0)      AS BrojStavki,
       ISNULL(z.Prebrojano, 0)      AS Prebrojano,
       ISNULL(z.SaRazlikom, 0)      AS SaRazlikom,
       ISNULL(z.Manjak, 0)          AS VrednostManjka,
       ISNULL(z.Visak, 0)           AS VrednostViska,
       p.OdobrioKorisnik, p.DatumOdobrenja, p.DokumentID, p.Napomena, p.KreiraoKorisnik, p.DatumKreiranja
FROM dbo.SERPopis p
JOIN dbo.SERStatus st ON st.Oblast = 'Popis' AND st.Status = p.Status
LEFT JOIN dbo.SERDeoGrupa g ON g.ID = p.GrupaID
OUTER APPLY (SELECT COUNT(*) AS BrojStavki,
                    COUNT(s.PrebrojanaKolicina) AS Prebrojano,
                    SUM(CASE WHEN s.Razlika <> 0 THEN 1 ELSE 0 END) AS SaRazlikom,
                    SUM(CASE WHEN s.Razlika < 0 THEN -s.VrednostRazlike END) AS Manjak,
                    SUM(CASE WHEN s.Razlika > 0 THEN s.VrednostRazlike END) AS Visak
             FROM dbo.SERPopisStavka s WHERE s.PopisID = p.ID) z;
GO

/* ---------------------------------------------------------------------------------------------
   9. Pomoćne procedure: log, broj dokumenta, nivo odobrenja, obaveštenje, prosečna cena
   --------------------------------------------------------------------------------------------- */
CREATE OR ALTER PROCEDURE dbo.SER_LogUpisi
    @Tabela sysname, @ZapisID int, @Akcija nvarchar(30), @Opis nvarchar(500), @Korisnik nvarchar(20)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT dbo.SERLog (Korisnik, Tabela, ZapisID, Akcija, Opis) VALUES (@Korisnik, @Tabela, @ZapisID, @Akcija, @Opis);
END
GO

-- Razdvajanje dužnosti: isti korisnik ne odobrava ono što je sam podneo.
-- Servis.Delovi.ZabranaSamoodobravanja = 1 (podrazumevano) blokira; 0 dozvoljava, ali upisuje u SERLog.
CREATE OR ALTER PROCEDURE dbo.SER_ProveriSamoodobravanje
    @Podnosilac nvarchar(20), @Odobravalac nvarchar(20), @Tabela sysname, @ZapisID int
AS
BEGIN
    SET NOCOUNT ON;
    IF @Podnosilac IS NULL OR LOWER(LTRIM(RTRIM(@Podnosilac))) <> LOWER(LTRIM(RTRIM(@Odobravalac)))
        RETURN;
    DECLARE @Zabrana nvarchar(20) = N'1', @Poruka nvarchar(300);
    IF OBJECT_ID('dbo.Podesavanja', 'U') IS NOT NULL
        EXEC sp_executesql N'SELECT @z = ISNULL(NULLIF(LTRIM(RTRIM(Vrednost)), N''''), @z) FROM dbo.Podesavanja WHERE Kljuc = N''Servis.Delovi.ZabranaSamoodobravanja''',
                           N'@z nvarchar(20) OUTPUT', @z = @Zabrana OUTPUT;
    IF LOWER(@Zabrana) IN (N'1', N'true', N'da')
    BEGIN
        SET @Poruka = N'Zahtev ne sme da odobri isti korisnik koji ga je podneo (' + RTRIM(@Podnosilac) + N'). Odobrenje treba da da drugi ovlašćeni korisnik.';
        THROW 50801, @Poruka, 1;
    END
    EXEC dbo.SER_LogUpisi @Tabela, @ZapisID, N'Samoodobravanje', N'Isti korisnik je podneo i odobrio zahtev', @Odobravalac;
END
GO

-- Sledeći broj po formatu iz Podesavanja (npr. NAR-{GGGG}-{BROJ:0000}). Zove se unutar transakcije pozivaoca.
CREATE OR ALTER PROCEDURE dbo.SER_NoviBroj
    @Kljuc nvarchar(100), @Podrazumevano nvarchar(50), @Datum date, @Broj nvarchar(20) OUTPUT
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    DECLARE @Format nvarchar(100) = @Podrazumevano, @Godina smallint, @n int, @poz int, @kraj int, @nule int, @tekst nvarchar(20);

    IF OBJECT_ID('dbo.Podesavanja', 'U') IS NOT NULL
        EXEC sp_executesql N'SELECT @f = ISNULL(NULLIF(LTRIM(RTRIM(Vrednost)), N''''), @f) FROM dbo.Podesavanja WHERE Kljuc = @k',
                           N'@k nvarchar(100), @f nvarchar(100) OUTPUT', @k = @Kljuc, @f = @Format OUTPUT;

    SET @Godina = CASE WHEN CHARINDEX(N'{GGGG}', @Format) > 0 THEN YEAR(@Datum) ELSE 0 END;

    UPDATE dbo.SERBrojac WITH (UPDLOCK, HOLDLOCK)
    SET @n = Poslednji = Poslednji + 1
    WHERE Kljuc = @Kljuc AND Godina = @Godina;
    IF @@ROWCOUNT = 0
    BEGIN
        SET @n = 1;
        INSERT dbo.SERBrojac (Kljuc, Godina, Poslednji) VALUES (@Kljuc, @Godina, 1);
    END

    SET @Broj = REPLACE(@Format, N'{GGGG}', CAST(YEAR(@Datum) AS nvarchar(4)));
    SET @poz = CHARINDEX(N'{BROJ', @Broj);
    IF @poz = 0
        SET @Broj = @Broj + CAST(@n AS nvarchar(10));
    ELSE
    BEGIN
        SET @kraj = CHARINDEX(N'}', @Broj, @poz);
        SET @nule = CASE WHEN SUBSTRING(@Broj, @poz, 6) = N'{BROJ:' THEN @kraj - @poz - 6 ELSE 0 END;
        SET @tekst = CAST(@n AS nvarchar(10));
        IF LEN(@tekst) < @nule
            SET @tekst = RIGHT(REPLICATE(N'0', @nule) + @tekst, @nule);
        SET @Broj = STUFF(@Broj, @poz, @kraj - @poz + 1, @tekst);
    END
END
GO

-- Nivo odobrenja narudžbenice po iznosu (MD-R07): 1 šef tehničke službe, 2 tehnički direktor, 3 direktor
CREATE OR ALTER PROCEDURE dbo.SER_NivoOdobrenja @Iznos decimal(18,2), @Nivo tinyint OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @n1 nvarchar(50), @n2 nvarchar(50), @granica1 decimal(18,2) = 100000, @granica2 decimal(18,2) = 500000;
    IF OBJECT_ID('dbo.Podesavanja', 'U') IS NOT NULL
        EXEC sp_executesql N'SELECT @a = MAX(CASE WHEN Kljuc = N''Servis.Delovi.NivoOdobrenja1'' THEN Vrednost END),
                                    @b = MAX(CASE WHEN Kljuc = N''Servis.Delovi.NivoOdobrenja2'' THEN Vrednost END)
                             FROM dbo.Podesavanja WHERE Kljuc IN (N''Servis.Delovi.NivoOdobrenja1'', N''Servis.Delovi.NivoOdobrenja2'')',
                           N'@a nvarchar(50) OUTPUT, @b nvarchar(50) OUTPUT', @a = @n1 OUTPUT, @b = @n2 OUTPUT;
    -- iznos u celim dinarima; tačka, zarez i razmak se zanemaruju (100.000 = 100000)
    SET @n1 = REPLACE(REPLACE(REPLACE(@n1, N'.', N''), N',', N''), N' ', N'');
    SET @n2 = REPLACE(REPLACE(REPLACE(@n2, N'.', N''), N',', N''), N' ', N'');
    IF ISNUMERIC(@n1) = 1 SET @granica1 = CAST(@n1 AS decimal(18,2));
    IF ISNUMERIC(@n2) = 1 SET @granica2 = CAST(@n2 AS decimal(18,2));
    SET @Nivo = CASE WHEN @Iznos <= @granica1 THEN 1 WHEN @Iznos <= @granica2 THEN 2 ELSE 3 END;
END
GO

-- Obaveštenje tehničkoj službi kad kritičan deo padne ispod minimuma (MD-R08); jedno nepročitano po delu
CREATE OR ALTER PROCEDURE dbo.SER_ProveriMinimum @DeoID int
AS
BEGIN
    SET NOCOUNT ON;
    INSERT dbo.SERObavestenje (DeoID, Tekst)
    SELECT d.ID, N'Kritičan deo ' + d.Sifra + N' ' + d.Naziv + N' je ispod minimuma: raspoloživo '
                 + CAST(CAST(s.Raspolozivo AS decimal(18,2)) AS nvarchar(30)) + N' ' + d.JM
                 + N', minimum ' + CAST(CAST(d.MinZaliha AS decimal(18,2)) AS nvarchar(30)) + N' ' + d.JM + N'.'
    FROM dbo.SERDeo d
    CROSS APPLY dbo.fnSER_Stanje(d.ID) s
    WHERE d.ID = @DeoID AND d.Aktivno = 1 AND d.Kritican = 1 AND s.Raspolozivo < d.MinZaliha
      AND NOT EXISTS (SELECT 1 FROM dbo.SERObavestenje o WHERE o.DeoID = d.ID AND o.Procitano = 0);
END
GO

-- Prosečna ponderisana cena pre upisa ulaza (prijem, povrat, početno stanje, višak). Zove se unutar transakcije.
CREATE OR ALTER PROCEDURE dbo.SER_DeoCenaUlaz @DeoID int, @Kolicina decimal(18,3), @Cena decimal(18,4), @Korisnik nvarchar(20)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Stanje decimal(18,3) = (SELECT ISNULL(SUM(Kolicina), 0) FROM dbo.SERDeoKretanje WHERE DeoID = @DeoID);
    UPDATE dbo.SERDeo
    SET ProsecnaCena = CASE WHEN @Stanje <= 0 THEN @Cena
                            ELSE ROUND((@Stanje * ProsecnaCena + @Kolicina * @Cena) / (@Stanje + @Kolicina), 4) END
    WHERE ID = @DeoID;
END
GO

-- Novi dokument (zaglavlje). Zove se unutar transakcije.
CREATE OR ALTER PROCEDURE dbo.SER_DokumentNovi
    @Tip char(3), @Datum date, @RadniNalogID int = NULL, @NarudzbenicaID int = NULL, @Razlog nvarchar(250) = NULL,
    @Napomena nvarchar(500) = NULL, @Korisnik nvarchar(20), @DokumentID int OUTPUT, @Broj nvarchar(20) OUTPUT
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    DECLARE @Kljuc nvarchar(100) = CASE @Tip WHEN 'IZD' THEN N'Servis.Delovi.FormatIzdatnice'
                                             WHEN 'PRM' THEN N'Servis.Delovi.FormatPrijemnice'
                                             WHEN 'POV' THEN N'Servis.Delovi.FormatPovratnice'
                                             WHEN 'POP' THEN N'Servis.Delovi.FormatPopisnogDokumenta'
                                             WHEN 'KOR' THEN N'Servis.Delovi.FormatKorekcije'
                                             WHEN 'PST' THEN N'Servis.Delovi.FormatPocetnogStanja' END;
    DECLARE @Podrazumevano nvarchar(50) = RTRIM(@Tip) + N'-{BROJ:0000}';
    EXEC dbo.SER_NoviBroj @Kljuc, @Podrazumevano, @Datum, @Broj OUTPUT;
    INSERT dbo.SERDokument (Tip, Broj, Datum, RadniNalogID, NarudzbenicaID, Razlog, Napomena, KreiraoKorisnik)
    VALUES (@Tip, @Broj, @Datum, @RadniNalogID, @NarudzbenicaID, @Razlog, @Napomena, @Korisnik);
    SET @DokumentID = SCOPE_IDENTITY();
END
GO

/* ---------------------------------------------------------------------------------------------
   10. Radni nalog i rezervacije (MD-R01, MD-R03, MD-R09)
   --------------------------------------------------------------------------------------------- */

-- MD-R03: nalog sa stavkom „Nema na stanju“ čeka delove; kad ih više nema, vraća se u „U radu“
CREATE OR ALTER PROCEDURE dbo.SER_RadniNalogStatusPreracunaj @RadniNalogID int, @Korisnik nvarchar(20)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Status tinyint = (SELECT Status FROM dbo.SERRadniNalog WHERE ID = @RadniNalogID), @Novi tinyint;
    IF @Status NOT IN (1, 2, 3) RETURN;
    SET @Novi = CASE WHEN EXISTS (SELECT 1 FROM dbo.SERDeoRezervacija rz
                                  JOIN dbo.SERRadniNalogStavka s ON s.ID = rz.RadniNalogStavkaID
                                  WHERE s.RadniNalogID = @RadniNalogID AND rz.Status = 3) THEN 3
                     WHEN @Status = 3 THEN 2
                     ELSE @Status END;
    IF @Novi <> @Status
    BEGIN
        UPDATE dbo.SERRadniNalog SET Status = @Novi, IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime() WHERE ID = @RadniNalogID;
        DECLARE @Opis nvarchar(500) = (SELECT Naziv FROM dbo.SERStatus WHERE Oblast = 'RadniNalog' AND Status = @Novi);
        EXEC dbo.SER_LogUpisi N'SERRadniNalog', @RadniNalogID, N'Status', @Opis, @Korisnik;
    END
END
GO

-- Stavke „Nema na stanju“ dobijaju robu redom kako su nastale, kad raspoloživo to dozvoli (posle prijema, povrata, storna)
CREATE OR ALTER PROCEDURE dbo.SER_RezervacijePreracunaj @DeoID int, @Korisnik nvarchar(20)
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    DECLARE @RezID int, @Kol decimal(18,3), @NalogID int, @Raspolozivo decimal(18,3);
    DECLARE rez CURSOR LOCAL FAST_FORWARD FOR
        SELECT rz.ID, rz.Kolicina, s.RadniNalogID
        FROM dbo.SERDeoRezervacija rz
        JOIN dbo.SERRadniNalogStavka s ON s.ID = rz.RadniNalogStavkaID
        JOIN dbo.SERRadniNalog rn ON rn.ID = s.RadniNalogID
        WHERE rz.DeoID = @DeoID AND rz.Status = 3 AND rn.Status IN (1, 2, 3)
        ORDER BY rz.DatumKreiranja, rz.ID;
    OPEN rez;
    FETCH NEXT FROM rez INTO @RezID, @Kol, @NalogID;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        SELECT @Raspolozivo = Raspolozivo FROM dbo.fnSER_Stanje(@DeoID);
        IF @Raspolozivo >= @Kol
        BEGIN
            UPDATE dbo.SERDeoRezervacija SET Status = 1, IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime() WHERE ID = @RezID;
            EXEC dbo.SER_RadniNalogStatusPreracunaj @NalogID, @Korisnik;
        END
        FETCH NEXT FROM rez INTO @RezID, @Kol, @NalogID;
    END
    CLOSE rez;
    DEALLOCATE rez;
END
GO

-- MD-R01: rezervacija do raspoloživog; inače cela stavka ide kao „Nema na stanju“.
-- Bez stavke naloga deo ide na stavku „Delovi iz magacina“ (pravi se ako je nema).
CREATE OR ALTER PROCEDURE dbo.SER_RezervacijaDodaj
    @RadniNalogID int, @RadniNalogStavkaID int = NULL, @DeoID int, @Kolicina decimal(18,3), @Napomena nvarchar(500) = NULL,
    @Korisnik nvarchar(20), @RezervacijaID int OUTPUT
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    DECLARE @Raspolozivo decimal(18,3), @Status tinyint;
    IF @Kolicina IS NULL OR @Kolicina <= 0
        THROW 50101, N'Količina mora biti veća od nule.', 1;
    BEGIN TRAN;
    IF NOT EXISTS (SELECT 1 FROM dbo.SERRadniNalog WITH (UPDLOCK) WHERE ID = @RadniNalogID AND Status IN (1, 2, 3))
        THROW 50102, N'Radni nalog nije otvoren.', 1;
    IF NOT EXISTS (SELECT 1 FROM dbo.SERDeo WITH (UPDLOCK) WHERE ID = @DeoID AND Aktivno = 1)
        THROW 50103, N'Deo ne postoji ili nije aktivan.', 1;

    IF @RadniNalogStavkaID IS NULL
    BEGIN
        SELECT TOP (1) @RadniNalogStavkaID = ID FROM dbo.SERRadniNalogStavka
        WHERE RadniNalogID = @RadniNalogID AND Opis = N'Delovi iz magacina' ORDER BY ID;
        IF @RadniNalogStavkaID IS NULL
        BEGIN
            INSERT dbo.SERRadniNalogStavka (RadniNalogID, Opis, KreiraoKorisnik) VALUES (@RadniNalogID, N'Delovi iz magacina', @Korisnik);
            SET @RadniNalogStavkaID = SCOPE_IDENTITY();
        END
    END
    ELSE IF NOT EXISTS (SELECT 1 FROM dbo.SERRadniNalogStavka WHERE ID = @RadniNalogStavkaID AND RadniNalogID = @RadniNalogID)
        THROW 50104, N'Stavka ne pripada radnom nalogu.', 1;

    SELECT @Raspolozivo = Raspolozivo FROM dbo.fnSER_Stanje(@DeoID);
    SET @Status = CASE WHEN @Raspolozivo >= @Kolicina THEN 1 ELSE 3 END;

    INSERT dbo.SERDeoRezervacija (RadniNalogStavkaID, DeoID, Kolicina, Status, Napomena, KreiraoKorisnik)
    VALUES (@RadniNalogStavkaID, @DeoID, @Kolicina, @Status, @Napomena, @Korisnik);
    SET @RezervacijaID = SCOPE_IDENTITY();

    EXEC dbo.SER_RadniNalogStatusPreracunaj @RadniNalogID, @Korisnik;
    EXEC dbo.SER_ProveriMinimum @DeoID;
    COMMIT;
END
GO

CREATE OR ALTER PROCEDURE dbo.SER_RezervacijaStorno @RezervacijaID int, @Korisnik nvarchar(20)
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    DECLARE @DeoID int, @NalogID int;
    BEGIN TRAN;
    SELECT @DeoID = rz.DeoID, @NalogID = s.RadniNalogID
    FROM dbo.SERDeoRezervacija rz WITH (UPDLOCK)
    JOIN dbo.SERRadniNalogStavka s ON s.ID = rz.RadniNalogStavkaID
    WHERE rz.ID = @RezervacijaID AND rz.Status IN (1, 3);
    IF @DeoID IS NULL
        THROW 50111, N'Stornira se samo rezervisana stavka ili stavka koje nema na stanju; izdata stavka se vraća povratom.', 1;
    UPDATE dbo.SERDeoRezervacija SET Status = 4, IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime() WHERE ID = @RezervacijaID;
    EXEC dbo.SER_LogUpisi N'SERDeoRezervacija', @RezervacijaID, N'Storno', NULL, @Korisnik;
    EXEC dbo.SER_RadniNalogStatusPreracunaj @NalogID, @Korisnik;
    EXEC dbo.SER_RezervacijePreracunaj @DeoID, @Korisnik;
    COMMIT;
END
GO

-- MD-R09: redovni nalog iz plana rezerviše delove iz normativa (deo koji je već na nalogu se preskače)
CREATE OR ALTER PROCEDURE dbo.SER_RadniNalogRezervisiNormativ @RadniNalogID int, @Korisnik nvarchar(20)
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    DECLARE @DeoID int, @Kol decimal(18,3), @RezID int;
    DECLARE norm CURSOR LOCAL FAST_FORWARD FOR
        SELECT n.DeoID, n.Kolicina
        FROM dbo.SERRadniNalog rn
        JOIN dbo.SERPlanNormativ n ON n.PlanStavkaID = rn.PlanStavkaID
        JOIN dbo.SERDeo d ON d.ID = n.DeoID AND d.Aktivno = 1
        WHERE rn.ID = @RadniNalogID
          AND NOT EXISTS (SELECT 1 FROM dbo.SERDeoRezervacija rz
                          JOIN dbo.SERRadniNalogStavka s ON s.ID = rz.RadniNalogStavkaID
                          WHERE s.RadniNalogID = rn.ID AND rz.DeoID = n.DeoID AND rz.Status <> 4)
        ORDER BY n.ID;
    BEGIN TRAN;
    OPEN norm;
    FETCH NEXT FROM norm INTO @DeoID, @Kol;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        EXEC dbo.SER_RezervacijaDodaj @RadniNalogID, NULL, @DeoID, @Kol, N'Normativ', @Korisnik, @RezID OUTPUT;
        FETCH NEXT FROM norm INTO @DeoID, @Kol;
    END
    CLOSE norm;
    DEALLOCATE norm;
    COMMIT;
END
GO

CREATE OR ALTER PROCEDURE dbo.SER_RadniNalogNovi
    @Datum date, @SredstvoID int, @PlanStavkaID int = NULL, @Opis nvarchar(500) = NULL, @Korisnik nvarchar(20),
    @ID int OUTPUT, @Broj nvarchar(20) OUTPUT
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    IF @PlanStavkaID IS NOT NULL
       AND NOT EXISTS (SELECT 1 FROM dbo.SERPlanStavka p JOIN dbo.SERSredstvo s ON s.VrstaSredstvaID = p.VrstaSredstvaID
                       WHERE p.ID = @PlanStavkaID AND s.ID = @SredstvoID)
        THROW 50751, N'Izabrani servis iz plana ne odgovara vrsti sredstva.', 1;
    BEGIN TRAN;
    EXEC dbo.SER_NoviBroj N'Servis.FormatRadnogNaloga', N'RN-{GGGG}-{BROJ:0000}', @Datum, @Broj OUTPUT;
    INSERT dbo.SERRadniNalog (Broj, Datum, SredstvoID, PlanStavkaID, Redovni, Opis, KreiraoKorisnik)
    VALUES (@Broj, @Datum, @SredstvoID, @PlanStavkaID, CASE WHEN @PlanStavkaID IS NULL THEN 0 ELSE 1 END, @Opis, @Korisnik);
    SET @ID = SCOPE_IDENTITY();
    IF @PlanStavkaID IS NOT NULL
        EXEC dbo.SER_RadniNalogRezervisiNormativ @ID, @Korisnik;
    COMMIT;
END
GO

/* ---------------------------------------------------------------------------------------------
   11. Izdavanje i povrat (MD-R02, MD-R04)
   --------------------------------------------------------------------------------------------- */

-- Izdaje sve rezervisane delove naloga (ili samo jednu rezervaciju) po prosečnoj ceni u trenutku izdavanja
CREATE OR ALTER PROCEDURE dbo.SER_Izdaj
    @RadniNalogID int, @RezervacijaID int = NULL, @Datum date, @Napomena nvarchar(500) = NULL, @Korisnik nvarchar(20),
    @DokumentID int OUTPUT, @Broj nvarchar(20) OUTPUT
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    DECLARE @Stavke TABLE (RezervacijaID int PRIMARY KEY, DeoID int, Kolicina decimal(18,3));
    BEGIN TRAN;
    IF NOT EXISTS (SELECT 1 FROM dbo.SERRadniNalog WITH (UPDLOCK) WHERE ID = @RadniNalogID AND Status IN (1, 2, 3))
        THROW 50201, N'Radni nalog nije otvoren.', 1;

    INSERT @Stavke (RezervacijaID, DeoID, Kolicina)
    SELECT rz.ID, rz.DeoID, rz.Kolicina
    FROM dbo.SERDeoRezervacija rz WITH (UPDLOCK)
    JOIN dbo.SERRadniNalogStavka s ON s.ID = rz.RadniNalogStavkaID
    WHERE s.RadniNalogID = @RadniNalogID AND rz.Status = 1 AND (@RezervacijaID IS NULL OR rz.ID = @RezervacijaID);
    IF NOT EXISTS (SELECT 1 FROM @Stavke)
        THROW 50202, N'Nema rezervisanih delova za izdavanje.', 1;

    -- zaključavanje delova, pa provera stanja
    DECLARE @Zakljucano int = (SELECT COUNT(*) FROM dbo.SERDeo WITH (UPDLOCK, HOLDLOCK) WHERE ID IN (SELECT DeoID FROM @Stavke));
    IF EXISTS (SELECT 1 FROM (SELECT DeoID, SUM(Kolicina) AS Kol FROM @Stavke GROUP BY DeoID) x
               CROSS APPLY dbo.fnSER_Stanje(x.DeoID) s WHERE s.Stanje < x.Kol)
        THROW 50203, N'Stanje dela je manje od količine za izdavanje.', 1;

    EXEC dbo.SER_DokumentNovi 'IZD', @Datum, @RadniNalogID, NULL, NULL, @Napomena, @Korisnik, @DokumentID OUTPUT, @Broj OUTPUT;

    INSERT dbo.SERDeoKretanje (DokumentID, DeoID, Datum, Tip, Kolicina, Cena, RadniNalogID, RezervacijaID, KreiraoKorisnik)
    SELECT @DokumentID, x.DeoID, @Datum, 'IZD', -x.Kolicina, d.ProsecnaCena, @RadniNalogID, x.RezervacijaID, @Korisnik
    FROM @Stavke x JOIN dbo.SERDeo d ON d.ID = x.DeoID;

    UPDATE rz SET Status = 2, IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime()
    FROM dbo.SERDeoRezervacija rz JOIN @Stavke x ON x.RezervacijaID = rz.ID;

    UPDATE dbo.SERRadniNalog SET Status = 2, IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime()
    WHERE ID = @RadniNalogID AND Status = 1;
    EXEC dbo.SER_RadniNalogStatusPreracunaj @RadniNalogID, @Korisnik;

    DECLARE @DeoID int;
    DECLARE delovi CURSOR LOCAL FAST_FORWARD FOR SELECT DISTINCT DeoID FROM @Stavke;
    OPEN delovi;
    FETCH NEXT FROM delovi INTO @DeoID;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        EXEC dbo.SER_ProveriMinimum @DeoID;
        FETCH NEXT FROM delovi INTO @DeoID;
    END
    CLOSE delovi;
    DEALLOCATE delovi;

    EXEC dbo.SER_LogUpisi N'SERDokument', @DokumentID, N'Izdavanje', @Broj, @Korisnik;
    COMMIT;
END
GO

-- Povrat nepotrošenog dela po ceni sa izdatnice; umanjuje trošak naloga
CREATE OR ALTER PROCEDURE dbo.SER_Povrat
    @IzdavanjeKretanjeID int, @Kolicina decimal(18,3), @Datum date, @Napomena nvarchar(500) = NULL, @Korisnik nvarchar(20),
    @DokumentID int OUTPUT, @Broj nvarchar(20) OUTPUT
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    DECLARE @DeoID int, @Cena decimal(18,4), @NalogID int, @RezID int, @Izdato decimal(18,3), @Vraceno decimal(18,3);
    IF @Kolicina IS NULL OR @Kolicina <= 0
        THROW 50211, N'Količina mora biti veća od nule.', 1;
    BEGIN TRAN;
    SELECT @DeoID = DeoID, @Cena = Cena, @NalogID = RadniNalogID, @RezID = RezervacijaID, @Izdato = -Kolicina
    FROM dbo.SERDeoKretanje WHERE ID = @IzdavanjeKretanjeID AND Tip = 'IZD';
    IF @DeoID IS NULL
        THROW 50212, N'Povrat se radi samo za stavku izdatnice.', 1;
    IF NOT EXISTS (SELECT 1 FROM dbo.SERRadniNalog WHERE ID = @NalogID AND Status IN (1, 2, 3))
        THROW 50214, N'Nalog je završen ili stornirano. Ponovo otvorite nalog (uz razlog) pre povrata dela.', 1;
    SELECT @DeoID = ID FROM dbo.SERDeo WITH (UPDLOCK, HOLDLOCK) WHERE ID = @DeoID;   -- zaključavanje dela
    SELECT @Vraceno = ISNULL(SUM(Kolicina), 0) FROM dbo.SERDeoKretanje WHERE IzvorKretanjeID = @IzdavanjeKretanjeID AND Tip = 'POV';
    IF @Kolicina > @Izdato - @Vraceno
        THROW 50213, N'Količina povrata je veća od izdate količine koja nije vraćena.', 1;

    EXEC dbo.SER_DokumentNovi 'POV', @Datum, @NalogID, NULL, NULL, @Napomena, @Korisnik, @DokumentID OUTPUT, @Broj OUTPUT;
    EXEC dbo.SER_DeoCenaUlaz @DeoID, @Kolicina, @Cena, @Korisnik;
    INSERT dbo.SERDeoKretanje (DokumentID, DeoID, Datum, Tip, Kolicina, Cena, RadniNalogID, RezervacijaID, IzvorKretanjeID, Napomena, KreiraoKorisnik)
    VALUES (@DokumentID, @DeoID, @Datum, 'POV', @Kolicina, @Cena, @NalogID, @RezID, @IzdavanjeKretanjeID, @Napomena, @Korisnik);

    EXEC dbo.SER_RezervacijePreracunaj @DeoID, @Korisnik;
    EXEC dbo.SER_LogUpisi N'SERDokument', @DokumentID, N'Povrat', @Broj, @Korisnik;
    COMMIT;
END
GO

/* ---------------------------------------------------------------------------------------------
   12. Prijem i početno stanje (MD-R05, OP-MD-04)
   --------------------------------------------------------------------------------------------- */

-- Zaglavlje prijema: po narudžbenici, ili bez nje uz razlog
CREATE OR ALTER PROCEDURE dbo.SER_PrijemNovi
    @NarudzbenicaID int = NULL, @Datum date, @Razlog nvarchar(250) = NULL, @Napomena nvarchar(500) = NULL, @Korisnik nvarchar(20),
    @DokumentID int OUTPUT, @Broj nvarchar(20) OUTPUT
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    IF @NarudzbenicaID IS NULL AND LEN(LTRIM(RTRIM(ISNULL(@Razlog, N'')))) = 0
        THROW 50301, N'Prijem bez narudžbenice traži razlog.', 1;
    IF @NarudzbenicaID IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.SERNarudzbenica WHERE ID = @NarudzbenicaID AND Status IN (3, 4))
        THROW 50302, N'Prima se samo poručena ili delimično isporučena narudžbenica.', 1;
    BEGIN TRAN;
    EXEC dbo.SER_DokumentNovi 'PRM', @Datum, NULL, @NarudzbenicaID, @Razlog, @Napomena, @Korisnik, @DokumentID OUTPUT, @Broj OUTPUT;
    EXEC dbo.SER_LogUpisi N'SERDokument', @DokumentID, N'Prijem', @Broj, @Korisnik;
    COMMIT;
END
GO

-- Stavka prijema: količina i cena mogu da se razlikuju od narudžbenice; preračunava prosečnu cenu
CREATE OR ALTER PROCEDURE dbo.SER_PrijemStavka
    @DokumentID int, @NarudzbenicaStavkaID int = NULL, @DeoID int = NULL, @Kolicina decimal(18,3), @Cena decimal(18,4),
    @Napomena nvarchar(500) = NULL, @Korisnik nvarchar(20), @KretanjeID int OUTPUT
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    DECLARE @NarID int, @Datum date;
    IF @Kolicina IS NULL OR @Kolicina <= 0 OR @Cena IS NULL OR @Cena < 0
        THROW 50311, N'Količina mora biti veća od nule, a cena ne sme biti negativna.', 1;
    BEGIN TRAN;
    SELECT @NarID = NarudzbenicaID, @Datum = Datum FROM dbo.SERDokument WITH (UPDLOCK) WHERE ID = @DokumentID AND Tip = 'PRM';
    IF @Datum IS NULL
        THROW 50312, N'Dokument nije prijemnica.', 1;
    IF @NarID IS NOT NULL
    BEGIN
        IF @NarudzbenicaStavkaID IS NULL
            THROW 50313, N'Prijem po narudžbenici ide po stavci narudžbenice.', 1;
        SELECT @DeoID = DeoID FROM dbo.SERNarudzbenicaStavka WHERE ID = @NarudzbenicaStavkaID AND NarudzbenicaID = @NarID;
        IF @DeoID IS NULL
            THROW 50314, N'Stavka ne pripada narudžbenici sa prijemnice.', 1;

        -- prekoračenje preostale količine: do tolerancije (Servis.Delovi.TolerancijaPrijema, %) bez komentara, preko nje samo uz komentar
        DECLARE @Preostalo decimal(18,3) = (SELECT Preostalo FROM dbo.vSERNarudzbenicaStavka WHERE ID = @NarudzbenicaStavkaID),
                @Tolerancija decimal(9,2) = 0, @TolTekst nvarchar(50);
        IF OBJECT_ID('dbo.Podesavanja', 'U') IS NOT NULL
        BEGIN
            EXEC sp_executesql N'SELECT @t = Vrednost FROM dbo.Podesavanja WHERE Kljuc = N''Servis.Delovi.TolerancijaPrijema''',
                               N'@t nvarchar(50) OUTPUT', @t = @TolTekst OUTPUT;
            SET @Tolerancija = ISNULL(TRY_CAST(REPLACE(LTRIM(RTRIM(@TolTekst)), N',', N'.') AS decimal(9,2)), 0);
        END
        IF @Kolicina > @Preostalo * (1 + @Tolerancija / 100.0) AND LEN(LTRIM(RTRIM(ISNULL(@Napomena, N'')))) = 0
            THROW 50317, N'Količina je veća od preostale po narudžbenici. Unesite komentar uz stavku (razlog prekoračenja) ili smanjite količinu.', 1;
    END
    ELSE IF @NarudzbenicaStavkaID IS NOT NULL
        THROW 50315, N'Prijemnica bez narudžbenice ne prima stavku narudžbenice.', 1;
    IF NOT EXISTS (SELECT 1 FROM dbo.SERDeo WHERE ID = @DeoID)
        THROW 50316, N'Deo ne postoji.', 1;

    SELECT @DeoID = ID FROM dbo.SERDeo WITH (UPDLOCK, HOLDLOCK) WHERE ID = @DeoID;   -- zaključavanje dela
    EXEC dbo.SER_DeoCenaUlaz @DeoID, @Kolicina, @Cena, @Korisnik;
    INSERT dbo.SERDeoKretanje (DokumentID, DeoID, Datum, Tip, Kolicina, Cena, NarudzbenicaStavkaID, Napomena, KreiraoKorisnik)
    VALUES (@DokumentID, @DeoID, @Datum, 'PRM', @Kolicina, @Cena, @NarudzbenicaStavkaID, @Napomena, @Korisnik);
    SET @KretanjeID = SCOPE_IDENTITY();

    IF @NarID IS NOT NULL
        UPDATE dbo.SERNarudzbenica
        SET Status = CASE WHEN EXISTS (SELECT 1 FROM dbo.vSERNarudzbenicaStavka v WHERE v.NarudzbenicaID = @NarID AND v.Preostalo > 0) THEN 4 ELSE 5 END,
            IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime()
        WHERE ID = @NarID;

    EXEC dbo.SER_RezervacijePreracunaj @DeoID, @Korisnik;
    COMMIT;
END
GO

-- Početno stanje (prelazak sa postojećeg magacina): samo za deo bez kretanja.
-- @DokumentID NULL pravi novi PST dokument; isti dokument se prosleđuje za sledeće delove.
CREATE OR ALTER PROCEDURE dbo.SER_PocetnoStanje
    @DeoID int, @Kolicina decimal(18,3), @Cena decimal(18,4), @Datum date, @Korisnik nvarchar(20), @DokumentID int OUTPUT
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    DECLARE @Broj nvarchar(20);
    IF @Kolicina IS NULL OR @Kolicina <= 0 OR @Cena IS NULL OR @Cena < 0
        THROW 50321, N'Količina mora biti veća od nule, a cena ne sme biti negativna.', 1;
    BEGIN TRAN;
    SELECT @DeoID = ID FROM dbo.SERDeo WITH (UPDLOCK, HOLDLOCK) WHERE ID = @DeoID;   -- zaključavanje dela
    IF EXISTS (SELECT 1 FROM dbo.SERDeoKretanje WHERE DeoID = @DeoID)
        THROW 50322, N'Deo već ima kretanje; početno stanje se ne unosi.', 1;
    IF @DokumentID IS NULL
        EXEC dbo.SER_DokumentNovi 'PST', @Datum, NULL, NULL, NULL, NULL, @Korisnik, @DokumentID OUTPUT, @Broj OUTPUT;
    ELSE IF NOT EXISTS (SELECT 1 FROM dbo.SERDokument WHERE ID = @DokumentID AND Tip = 'PST')
        THROW 50323, N'Dokument nije početno stanje.', 1;
    EXEC dbo.SER_DeoCenaUlaz @DeoID, @Kolicina, @Cena, @Korisnik;
    INSERT dbo.SERDeoKretanje (DokumentID, DeoID, Datum, Tip, Kolicina, Cena, KreiraoKorisnik)
    SELECT @DokumentID, @DeoID, Datum, 'PST', @Kolicina, @Cena, @Korisnik FROM dbo.SERDokument WHERE ID = @DokumentID;
    COMMIT;
END
GO

/* ---------------------------------------------------------------------------------------------
   13. Narudžbenice (MD-R06, MD-R07)
   --------------------------------------------------------------------------------------------- */
CREATE OR ALTER PROCEDURE dbo.SER_NarudzbenicaNova
    @DobavljacID int, @Datum date, @OcekivaniDatum date = NULL, @Napomena nvarchar(500) = NULL, @Korisnik nvarchar(20),
    @ID int OUTPUT, @Broj nvarchar(20) OUTPUT
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRAN;
    EXEC dbo.SER_NoviBroj N'Servis.Delovi.FormatNarudzbenice', N'NAR-{GGGG}-{BROJ:0000}', @Datum, @Broj OUTPUT;
    INSERT dbo.SERNarudzbenica (Broj, Datum, DobavljacID, OcekivaniDatum, Napomena, KreiraoKorisnik)
    VALUES (@Broj, @Datum, @DobavljacID, @OcekivaniDatum, @Napomena, @Korisnik);
    SET @ID = SCOPE_IDENTITY();
    EXEC dbo.SER_LogUpisi N'SERNarudzbenica', @ID, N'Nova', @Broj, @Korisnik;
    COMMIT;
END
GO

-- Dodaje stavku ili menja količinu i cenu postojeće (isti deo); samo u nacrtu
CREATE OR ALTER PROCEDURE dbo.SER_NarudzbenicaStavkaSnimi
    @NarudzbenicaID int, @DeoID int, @Kolicina decimal(18,3), @Cena decimal(18,4), @Napomena nvarchar(500) = NULL,
    @Korisnik nvarchar(20), @StavkaID int OUTPUT
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRAN;
    IF NOT EXISTS (SELECT 1 FROM dbo.SERNarudzbenica WITH (UPDLOCK) WHERE ID = @NarudzbenicaID AND Status = 1)
        THROW 50401, N'Stavke se menjaju samo dok je narudžbenica nacrt.', 1;
    SET @StavkaID = (SELECT ID FROM dbo.SERNarudzbenicaStavka WHERE NarudzbenicaID = @NarudzbenicaID AND DeoID = @DeoID);
    IF @StavkaID IS NULL
    BEGIN
        INSERT dbo.SERNarudzbenicaStavka (NarudzbenicaID, DeoID, Kolicina, Cena, Napomena, KreiraoKorisnik)
        VALUES (@NarudzbenicaID, @DeoID, @Kolicina, @Cena, @Napomena, @Korisnik);
        SET @StavkaID = SCOPE_IDENTITY();
    END
    ELSE
        UPDATE dbo.SERNarudzbenicaStavka
        SET Kolicina = @Kolicina, Cena = @Cena, Napomena = @Napomena, IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime()
        WHERE ID = @StavkaID;
    COMMIT;
END
GO

-- Uklanja stavku iz nacrta (nacrt još nije dokument, pa se stavka briše)
CREATE OR ALTER PROCEDURE dbo.SER_NarudzbenicaStavkaObrisi @StavkaID int, @Korisnik nvarchar(20)
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRAN;
    IF NOT EXISTS (SELECT 1 FROM dbo.SERNarudzbenicaStavka s JOIN dbo.SERNarudzbenica n WITH (UPDLOCK) ON n.ID = s.NarudzbenicaID
                   WHERE s.ID = @StavkaID AND n.Status = 1)
        THROW 50402, N'Stavka se uklanja samo dok je narudžbenica nacrt.', 1;
    DELETE dbo.SERNarudzbenicaStavka WHERE ID = @StavkaID;
    EXEC dbo.SER_LogUpisi N'SERNarudzbenicaStavka', @StavkaID, N'Obrisana', NULL, @Korisnik;
    COMMIT;
END
GO

-- „Kreiraj narudžbenice“: predlog nabavke grupisan po dobavljaču, kao nacrti.
-- Vraća napravljene narudžbenice i delove iz predloga koji nemaju dobavljača.
CREATE OR ALTER PROCEDURE dbo.SER_NarudzbeniceIzPredloga @Datum date, @Korisnik nvarchar(20)
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    DECLARE @Predlog TABLE (DeoID int PRIMARY KEY, DobavljacID int, Kolicina decimal(18,3), Cena decimal(18,4), RokDana int);
    DECLARE @Nove TABLE (ID int, Broj nvarchar(20), DobavljacID int);
    DECLARE @Dob int, @Rok int, @ID int, @Broj nvarchar(20), @Ocekivani date;

    BEGIN TRAN;
    INSERT @Predlog (DeoID, DobavljacID, Kolicina, Cena, RokDana)
    SELECT DeoID, DobavljacID, PredlozenaKolicina, Cena, RokIsporukeDana
    FROM dbo.vSERPredlogNabavke WHERE PredlozenaKolicina > 0;

    DECLARE dob CURSOR LOCAL FAST_FORWARD FOR
        SELECT DobavljacID, MAX(RokDana) FROM @Predlog WHERE DobavljacID IS NOT NULL GROUP BY DobavljacID;
    OPEN dob;
    FETCH NEXT FROM dob INTO @Dob, @Rok;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        SET @Ocekivani = CASE WHEN @Rok IS NULL THEN NULL ELSE DATEADD(DAY, @Rok, @Datum) END;
        EXEC dbo.SER_NarudzbenicaNova @Dob, @Datum, @Ocekivani, N'Iz predloga nabavke', @Korisnik, @ID OUTPUT, @Broj OUTPUT;
        INSERT dbo.SERNarudzbenicaStavka (NarudzbenicaID, DeoID, Kolicina, Cena, KreiraoKorisnik)
        SELECT @ID, DeoID, Kolicina, Cena, @Korisnik FROM @Predlog WHERE DobavljacID = @Dob;
        INSERT @Nove VALUES (@ID, @Broj, @Dob);
        FETCH NEXT FROM dob INTO @Dob, @Rok;
    END
    CLOSE dob;
    DEALLOCATE dob;
    COMMIT;

    SELECT v.* FROM dbo.vSERNarudzbenica v JOIN @Nove n ON n.ID = v.ID ORDER BY v.Broj;
    SELECT d.ID AS DeoID, d.Sifra, d.Naziv, p.Kolicina AS PredlozenaKolicina
    FROM @Predlog p JOIN dbo.SERDeo d ON d.ID = p.DeoID WHERE p.DobavljacID IS NULL ORDER BY d.Sifra;
END
GO

-- Nacrt → Čeka odobrenje; nivo odobrenja po iznosu
CREATE OR ALTER PROCEDURE dbo.SER_NarudzbenicaPosalji @ID int, @Korisnik nvarchar(20), @Nivo tinyint OUTPUT
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    DECLARE @Iznos decimal(18,2);
    BEGIN TRAN;
    IF NOT EXISTS (SELECT 1 FROM dbo.SERNarudzbenica WITH (UPDLOCK) WHERE ID = @ID AND Status = 1)
        THROW 50411, N'Na odobrenje se šalje samo nacrt.', 1;
    IF NOT EXISTS (SELECT 1 FROM dbo.SERNarudzbenicaStavka WHERE NarudzbenicaID = @ID)
        THROW 50412, N'Narudžbenica nema stavke.', 1;
    SELECT @Iznos = SUM(CAST(Kolicina * Cena AS decimal(18,2))) FROM dbo.SERNarudzbenicaStavka WHERE NarudzbenicaID = @ID;
    IF ISNULL(@Iznos, 0) <= 0
        THROW 50413, N'Iznos narudžbenice je 0 RSD. Unesite cene stavki pre slanja na odobrenje.', 1;
    EXEC dbo.SER_NivoOdobrenja @Iznos, @Nivo OUTPUT;
    UPDATE dbo.SERNarudzbenica SET Status = 2, PotrebanNivo = @Nivo, PoslaoKorisnik = @Korisnik, IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime() WHERE ID = @ID;
    EXEC dbo.SER_LogUpisi N'SERNarudzbenica', @ID, N'Na odobrenje', NULL, @Korisnik;
    COMMIT;
END
GO

-- Čeka odobrenje → Poručeno (@Odobri = 1) ili nazad u Nacrt (@Odobri = 0).
-- @NivoKorisnika određuje aplikacija (1 šef tehničke službe, 2 tehnički direktor, 3 direktor).
CREATE OR ALTER PROCEDURE dbo.SER_NarudzbenicaOdobri
    @ID int, @Odobri bit, @NivoKorisnika tinyint, @Komentar nvarchar(500) = NULL, @Korisnik nvarchar(20)
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    DECLARE @Potreban tinyint, @Podnosilac nvarchar(20);
    BEGIN TRAN;
    SELECT @Potreban = PotrebanNivo, @Podnosilac = COALESCE(PoslaoKorisnik, KreiraoKorisnik)
    FROM dbo.SERNarudzbenica WITH (UPDLOCK) WHERE ID = @ID AND Status = 2;
    IF @Potreban IS NULL
        THROW 50421, N'Narudžbenica ne čeka odobrenje.', 1;
    IF @NivoKorisnika < @Potreban
        THROW 50422, N'Iznos narudžbenice traži viši nivo odobrenja.', 1;
    IF @Odobri = 1
        EXEC dbo.SER_ProveriSamoodobravanje @Podnosilac, @Korisnik, N'SERNarudzbenica', @ID;
    IF @Odobri = 1
        UPDATE dbo.SERNarudzbenica
        SET Status = 3, OdobrioKorisnik = @Korisnik, DatumOdobrenja = sysdatetime(), IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime()
        WHERE ID = @ID;
    ELSE
        UPDATE dbo.SERNarudzbenica SET Status = 1, PotrebanNivo = NULL, IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime() WHERE ID = @ID;
    DECLARE @Akcija nvarchar(30) = CASE WHEN @Odobri = 1 THEN N'Odobrena' ELSE N'Vraćena' END;
    EXEC dbo.SER_LogUpisi N'SERNarudzbenica', @ID, @Akcija, @Komentar, @Korisnik;
    COMMIT;
END
GO

-- Otkazivanje; kod delimično isporučene otkazuje se neisporučeni ostatak
CREATE OR ALTER PROCEDURE dbo.SER_NarudzbenicaOtkazi @ID int, @Razlog nvarchar(500), @Korisnik nvarchar(20)
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRAN;
    IF NOT EXISTS (SELECT 1 FROM dbo.SERNarudzbenica WITH (UPDLOCK) WHERE ID = @ID AND Status IN (1, 2, 3, 4))
        THROW 50431, N'Isporučena ili već otkazana narudžbenica se ne otkazuje.', 1;
    UPDATE dbo.SERNarudzbenica SET Status = 6, IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime() WHERE ID = @ID;
    EXEC dbo.SER_LogUpisi N'SERNarudzbenica', @ID, N'Otkazana', @Razlog, @Korisnik;
    COMMIT;
END
GO

/* ---------------------------------------------------------------------------------------------
   14. Popis (MD-R10)
   --------------------------------------------------------------------------------------------- */

-- Popisna lista: svi aktivni delovi (ili jedna grupa) sa knjigovodstvenom količinom i prosečnom cenom
CREATE OR ALTER PROCEDURE dbo.SER_PopisNovi
    @Datum date, @GrupaID int = NULL, @Napomena nvarchar(500) = NULL, @Korisnik nvarchar(20), @ID int OUTPUT, @Broj nvarchar(20) OUTPUT
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRAN;
    IF EXISTS (SELECT 1 FROM dbo.SERPopis WITH (UPDLOCK) WHERE Status IN (1, 2) AND (GrupaID IS NULL OR @GrupaID IS NULL OR GrupaID = @GrupaID))
        THROW 50501, N'Za iste delove već postoji popis koji nije završen.', 1;
    EXEC dbo.SER_NoviBroj N'Servis.Delovi.FormatPopisa', N'POP-{GGGG}-{BROJ:0000}', @Datum, @Broj OUTPUT;
    INSERT dbo.SERPopis (Broj, Datum, GrupaID, Napomena, KreiraoKorisnik) VALUES (@Broj, @Datum, @GrupaID, @Napomena, @Korisnik);
    SET @ID = SCOPE_IDENTITY();
    INSERT dbo.SERPopisStavka (PopisID, DeoID, KnjigovodstvenaKolicina, Cena, KreiraoKorisnik)
    SELECT @ID, d.ID, s.Stanje, d.ProsecnaCena, @Korisnik
    FROM dbo.SERDeo d CROSS APPLY dbo.fnSER_Stanje(d.ID) s
    WHERE d.Aktivno = 1 AND (@GrupaID IS NULL OR d.GrupaID = @GrupaID);
    EXEC dbo.SER_LogUpisi N'SERPopis', @ID, N'Nov', @Broj, @Korisnik;
    COMMIT;
END
GO

CREATE OR ALTER PROCEDURE dbo.SER_PopisUnesi @StavkaID int, @Prebrojano decimal(18,3), @Napomena nvarchar(500) = NULL, @Korisnik nvarchar(20)
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRAN;
    IF NOT EXISTS (SELECT 1 FROM dbo.SERPopisStavka s JOIN dbo.SERPopis p WITH (UPDLOCK) ON p.ID = s.PopisID WHERE s.ID = @StavkaID AND p.Status = 1)
        THROW 50511, N'Prebrojano se unosi samo dok je popis u toku.', 1;
    UPDATE dbo.SERPopisStavka
    SET PrebrojanaKolicina = @Prebrojano, Napomena = @Napomena, IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime()
    WHERE ID = @StavkaID;
    COMMIT;
END
GO

-- U toku → Čeka odobrenje. Stavka bez unete količine nije brojana i ne menja zalihu.
CREATE OR ALTER PROCEDURE dbo.SER_PopisPosalji @ID int, @Korisnik nvarchar(20)
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRAN;
    IF NOT EXISTS (SELECT 1 FROM dbo.SERPopis WITH (UPDLOCK) WHERE ID = @ID AND Status = 1)
        THROW 50521, N'Na odobrenje se šalje samo popis u toku.', 1;
    IF NOT EXISTS (SELECT 1 FROM dbo.SERPopisStavka WHERE PopisID = @ID AND PrebrojanaKolicina IS NOT NULL)
        THROW 50522, N'Ni jedna stavka nije prebrojana.', 1;
    UPDATE dbo.SERPopis SET Status = 2, PoslaoKorisnik = @Korisnik, IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime() WHERE ID = @ID;
    EXEC dbo.SER_LogUpisi N'SERPopis', @ID, N'Na odobrenje', NULL, @Korisnik;
    COMMIT;
END
GO

-- Šef tehničke službe odobrava razlike: POP dokument, razlika po prosečnoj ceni sa popisa.
-- @Odobri = 0 vraća popis u „U toku“.
CREATE OR ALTER PROCEDURE dbo.SER_PopisOdobri @ID int, @Odobri bit, @Komentar nvarchar(500) = NULL, @Korisnik nvarchar(20)
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    DECLARE @Datum date, @DokumentID int, @Broj nvarchar(20), @DeoID int, @Podnosilac nvarchar(20);
    BEGIN TRAN;
    SELECT @Datum = Datum, @Podnosilac = COALESCE(PoslaoKorisnik, KreiraoKorisnik)
    FROM dbo.SERPopis WITH (UPDLOCK) WHERE ID = @ID AND Status = 2;
    IF @Datum IS NULL
        THROW 50531, N'Popis ne čeka odobrenje.', 1;
    IF @Odobri = 1
        EXEC dbo.SER_ProveriSamoodobravanje @Podnosilac, @Korisnik, N'SERPopis', @ID;
    IF @Odobri = 0
    BEGIN
        UPDATE dbo.SERPopis SET Status = 1, IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime() WHERE ID = @ID;
        EXEC dbo.SER_LogUpisi N'SERPopis', @ID, N'Vraćen', @Komentar, @Korisnik;
        COMMIT;
        RETURN;
    END

    DECLARE @Zakljucano int = (SELECT COUNT(*) FROM dbo.SERDeo WITH (UPDLOCK, HOLDLOCK)
                               WHERE ID IN (SELECT DeoID FROM dbo.SERPopisStavka WHERE PopisID = @ID AND Razlika <> 0));   -- zaključavanje
    IF EXISTS (SELECT 1 FROM dbo.SERPopisStavka s CROSS APPLY dbo.fnSER_Stanje(s.DeoID) x
               WHERE s.PopisID = @ID AND s.Razlika < 0 AND x.Stanje + s.Razlika < 0)
        THROW 50532, N'Posle popisa stanje dela bi bilo negativno (bilo je kretanja posle otvaranja popisa).', 1;

    IF EXISTS (SELECT 1 FROM dbo.SERPopisStavka WHERE PopisID = @ID AND Razlika <> 0)
    BEGIN
        EXEC dbo.SER_DokumentNovi 'POP', @Datum, NULL, NULL, NULL, @Komentar, @Korisnik, @DokumentID OUTPUT, @Broj OUTPUT;
        INSERT dbo.SERDeoKretanje (DokumentID, DeoID, Datum, Tip, Kolicina, Cena, Napomena, KreiraoKorisnik)
        SELECT @DokumentID, s.DeoID, @Datum, 'POP', s.Razlika, s.Cena, s.Napomena, @Korisnik
        FROM dbo.SERPopisStavka s WHERE s.PopisID = @ID AND s.Razlika <> 0;
    END
    UPDATE dbo.SERPopis
    SET Status = 3, OdobrioKorisnik = @Korisnik, DatumOdobrenja = sysdatetime(), DokumentID = @DokumentID,
        IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime()
    WHERE ID = @ID;

    DECLARE delovi CURSOR LOCAL FAST_FORWARD FOR SELECT DeoID FROM dbo.SERPopisStavka WHERE PopisID = @ID AND Razlika <> 0;
    OPEN delovi;
    FETCH NEXT FROM delovi INTO @DeoID;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        EXEC dbo.SER_RezervacijePreracunaj @DeoID, @Korisnik;
        EXEC dbo.SER_ProveriMinimum @DeoID;
        FETCH NEXT FROM delovi INTO @DeoID;
    END
    CLOSE delovi;
    DEALLOCATE delovi;

    EXEC dbo.SER_LogUpisi N'SERPopis', @ID, N'Odobren', @Broj, @Korisnik;
    COMMIT;
END
GO

CREATE OR ALTER PROCEDURE dbo.SER_PopisStorniraj @ID int, @Razlog nvarchar(500), @Korisnik nvarchar(20)
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRAN;
    IF NOT EXISTS (SELECT 1 FROM dbo.SERPopis WITH (UPDLOCK) WHERE ID = @ID AND Status IN (1, 2))
        THROW 50541, N'Odobren popis se ne stornira; razlika se ispravlja korekcijom.', 1;
    UPDATE dbo.SERPopis SET Status = 4, IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime() WHERE ID = @ID;
    EXEC dbo.SER_LogUpisi N'SERPopis', @ID, N'Storniran', @Razlog, @Korisnik;
    COMMIT;
END
GO

/* ---------------------------------------------------------------------------------------------
   15. Korekcija zaliha (MD-R11): zahtev sa razlogom, pa odobrenje
   --------------------------------------------------------------------------------------------- */
CREATE OR ALTER PROCEDURE dbo.SER_KorekcijaNova
    @DeoID int, @Kolicina decimal(18,3), @Razlog nvarchar(250), @Korisnik nvarchar(20), @ID int OUTPUT
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    IF LEN(LTRIM(RTRIM(ISNULL(@Razlog, N'')))) = 0
        THROW 50601, N'Korekcija traži razlog.', 1;
    IF ISNULL(@Kolicina, 0) = 0
        THROW 50603, N'Korekcija ne može biti 0.', 1;
    IF NOT EXISTS (SELECT 1 FROM dbo.SERDeo WHERE ID = @DeoID AND Aktivno = 1)
        THROW 50604, N'Deo ne postoji ili nije aktivan.', 1;
    -- stanje umanjeno i za korekcije istog dela koje već čekaju odobrenje (inače bi dve korekcije zajedno srušile stanje ispod nule)
    IF @Kolicina < 0 AND (SELECT Stanje FROM dbo.fnSER_Stanje(@DeoID))
                         + ISNULL((SELECT SUM(Kolicina) FROM dbo.SERKorekcija WHERE DeoID = @DeoID AND Status = 1 AND Kolicina < 0), 0)
                         + @Kolicina < 0
        THROW 50602, N'Korekcija bi svela stanje dela ispod nule (računajući i korekcije koje čekaju odobrenje).', 1;
    INSERT dbo.SERKorekcija (DeoID, Kolicina, Razlog, KreiraoKorisnik) VALUES (@DeoID, @Kolicina, @Razlog, @Korisnik);
    SET @ID = SCOPE_IDENTITY();
    EXEC dbo.SER_LogUpisi N'SERKorekcija', @ID, N'Zahtev', @Razlog, @Korisnik;
END
GO

CREATE OR ALTER PROCEDURE dbo.SER_KorekcijaOdobri @ID int, @Odobri bit, @Komentar nvarchar(500) = NULL, @Korisnik nvarchar(20)
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    DECLARE @DeoID int, @Kol decimal(18,3), @Razlog nvarchar(250), @DokumentID int, @Broj nvarchar(20), @Danas date = CAST(GETDATE() AS date),
            @Podnosilac nvarchar(20);
    BEGIN TRAN;
    SELECT @DeoID = DeoID, @Kol = Kolicina, @Razlog = Razlog, @Podnosilac = KreiraoKorisnik
    FROM dbo.SERKorekcija WITH (UPDLOCK) WHERE ID = @ID AND Status = 1;
    IF @DeoID IS NULL
        THROW 50611, N'Korekcija ne čeka odobrenje.', 1;
    IF @Odobri = 1
    BEGIN
        EXEC dbo.SER_ProveriSamoodobravanje @Podnosilac, @Korisnik, N'SERKorekcija', @ID;
        SELECT @DeoID = ID FROM dbo.SERDeo WITH (UPDLOCK, HOLDLOCK) WHERE ID = @DeoID;   -- zaključavanje dela
        IF @Kol < 0 AND (SELECT Stanje FROM dbo.fnSER_Stanje(@DeoID)) + @Kol < 0
            THROW 50612, N'Posle korekcije stanje dela bi bilo negativno.', 1;
        EXEC dbo.SER_DokumentNovi 'KOR', @Danas, NULL, NULL, @Razlog, @Komentar, @Korisnik, @DokumentID OUTPUT, @Broj OUTPUT;
        INSERT dbo.SERDeoKretanje (DokumentID, DeoID, Datum, Tip, Kolicina, Cena, Napomena, KreiraoKorisnik)
        SELECT @DokumentID, @DeoID, @Danas, 'KOR', @Kol, ProsecnaCena, @Razlog, @Korisnik FROM dbo.SERDeo WHERE ID = @DeoID;
        UPDATE dbo.SERKorekcija
        SET Status = 2, OdobrioKorisnik = @Korisnik, DatumOdobrenja = sysdatetime(), DokumentID = @DokumentID,
            IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime()
        WHERE ID = @ID;
        EXEC dbo.SER_RezervacijePreracunaj @DeoID, @Korisnik;
        EXEC dbo.SER_ProveriMinimum @DeoID;
    END
    ELSE
        UPDATE dbo.SERKorekcija
        SET Status = 3, OdobrioKorisnik = @Korisnik, DatumOdobrenja = sysdatetime(), IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime()
        WHERE ID = @ID;
    DECLARE @Akcija nvarchar(30) = CASE WHEN @Odobri = 1 THEN N'Odobrena' ELSE N'Odbijena' END;
    EXEC dbo.SER_LogUpisi N'SERKorekcija', @ID, @Akcija, @Komentar, @Korisnik;
    COMMIT;
END
GO

/* ---------------------------------------------------------------------------------------------
   15a. Šifarnik dela, radni nalog i zaglavlje narudžbenice (procedure koje zovu forme)
   --------------------------------------------------------------------------------------------- */

-- Novi deo (@ID = 0) ili izmena. Prosečna cena se ovde ne menja (samo prijem i povrat).
CREATE OR ALTER PROCEDURE dbo.SER_DeoSnimi
    @ID int OUTPUT, @Sifra nvarchar(20), @Naziv nvarchar(150), @GrupaID int = NULL, @JM nvarchar(10),
    @MinZaliha decimal(18,3), @MaxZaliha decimal(18,3) = NULL, @Lokacija nvarchar(30) = NULL, @Kritican bit,
    @DobavljacID int = NULL, @RokIsporukeDana int = NULL, @Napomena nvarchar(500) = NULL, @Aktivno bit, @Korisnik nvarchar(20)
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    IF EXISTS (SELECT 1 FROM dbo.SERDeo WHERE Sifra = @Sifra AND ID <> ISNULL(@ID, 0))
        THROW 50701, N'Deo sa ovom šifrom već postoji.', 1;
    IF @MaxZaliha IS NOT NULL AND @MaxZaliha < @MinZaliha
        THROW 50702, N'Maksimalna zaliha mora biti veća ili jednaka minimalnoj.', 1;
    BEGIN TRAN;
    IF ISNULL(@ID, 0) = 0
    BEGIN
        INSERT dbo.SERDeo (Sifra, Naziv, GrupaID, JM, MinZaliha, MaxZaliha, Lokacija, Kritican, DobavljacID, RokIsporukeDana, Napomena, Aktivno, KreiraoKorisnik)
        VALUES (@Sifra, @Naziv, @GrupaID, @JM, @MinZaliha, @MaxZaliha, @Lokacija, @Kritican, @DobavljacID, @RokIsporukeDana, @Napomena, @Aktivno, @Korisnik);
        SET @ID = SCOPE_IDENTITY();
        EXEC dbo.SER_LogUpisi N'SERDeo', @ID, N'Nov', @Sifra, @Korisnik;
    END
    ELSE
    BEGIN
        -- deaktivacija: samo deo bez stanja i bez otvorenih rezervacija (inače bi ostao zaliha koju niko ne vidi)
        IF @Aktivno = 0 AND EXISTS (SELECT 1 FROM dbo.SERDeo WHERE ID = @ID AND Aktivno = 1)
        BEGIN
            DECLARE @Stanje decimal(18,3) = (SELECT Stanje FROM dbo.fnSER_Stanje(@ID)), @PorukaDeaktivacije nvarchar(300);
            IF @Stanje <> 0
            BEGIN
                SET @PorukaDeaktivacije = N'Deo ima stanje ' + CAST(CAST(@Stanje AS float) AS nvarchar(30)) + N' i ne može se deaktivirati. Prvo ga izdajte, vratite dobavljaču ili ispravite popisom.';
                THROW 50703, @PorukaDeaktivacije, 1;
            END
            IF EXISTS (SELECT 1 FROM dbo.SERDeoRezervacija rz
                       JOIN dbo.SERRadniNalogStavka s ON s.ID = rz.RadniNalogStavkaID
                       JOIN dbo.SERRadniNalog rn ON rn.ID = s.RadniNalogID
                       WHERE rz.DeoID = @ID AND rz.Status IN (1, 3) AND rn.Status IN (1, 2, 3))
                THROW 50704, N'Deo ima otvorene rezervacije na radnim nalozima i ne može se deaktivirati.', 1;
        END
        DECLARE @Opis nvarchar(500) =
            (SELECT CASE WHEN MinZaliha <> @MinZaliha THEN N'Min ' + CAST(MinZaliha AS nvarchar(30)) + N' → ' + CAST(@MinZaliha AS nvarchar(30)) + N'; ' ELSE N'' END
                  + CASE WHEN ISNULL(MaxZaliha, -1) <> ISNULL(@MaxZaliha, -1) THEN N'Max promenjen; ' ELSE N'' END
                  + CASE WHEN Kritican <> @Kritican THEN N'Kritičan: ' + CASE WHEN @Kritican = 1 THEN N'da' ELSE N'ne' END + N'; ' ELSE N'' END
                  + CASE WHEN Aktivno <> @Aktivno THEN CASE WHEN @Aktivno = 1 THEN N'Aktiviran; ' ELSE N'Deaktiviran; ' END ELSE N'' END
             FROM dbo.SERDeo WHERE ID = @ID);
        UPDATE dbo.SERDeo
        SET Sifra = @Sifra, Naziv = @Naziv, GrupaID = @GrupaID, JM = @JM, MinZaliha = @MinZaliha, MaxZaliha = @MaxZaliha,
            Lokacija = @Lokacija, Kritican = @Kritican, DobavljacID = @DobavljacID, RokIsporukeDana = @RokIsporukeDana,
            Napomena = @Napomena, Aktivno = @Aktivno, IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime()
        WHERE ID = @ID;
        EXEC dbo.SER_LogUpisi N'SERDeo', @ID, N'Izmena', @Opis, @Korisnik;
    END
    EXEC dbo.SER_ProveriMinimum @ID;
    COMMIT;
END
GO

-- Završetak naloga: rezervisani a neizdati delovi i stavke kojih nema na stanju se storniraju
CREATE OR ALTER PROCEDURE dbo.SER_RadniNalogZavrsi @RadniNalogID int, @Datum date, @Korisnik nvarchar(20)
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    DECLARE @Rez TABLE (DeoID int);
    DECLARE @DeoID int;
    BEGIN TRAN;
    IF NOT EXISTS (SELECT 1 FROM dbo.SERRadniNalog WITH (UPDLOCK) WHERE ID = @RadniNalogID AND Status IN (1, 2, 3))
        THROW 50711, N'Završava se samo otvoren nalog.', 1;
    UPDATE rz SET Status = 4, IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime()
    OUTPUT inserted.DeoID INTO @Rez
    FROM dbo.SERDeoRezervacija rz JOIN dbo.SERRadniNalogStavka s ON s.ID = rz.RadniNalogStavkaID
    WHERE s.RadniNalogID = @RadniNalogID AND rz.Status IN (1, 3);
    UPDATE dbo.SERRadniNalog SET Status = 4, DatumZavrsetka = @Datum, IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime()
    WHERE ID = @RadniNalogID;
    EXEC dbo.SER_LogUpisi N'SERRadniNalog', @RadniNalogID, N'Završen', NULL, @Korisnik;
    DECLARE delovi CURSOR LOCAL FAST_FORWARD FOR SELECT DISTINCT DeoID FROM @Rez;
    OPEN delovi;
    FETCH NEXT FROM delovi INTO @DeoID;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        EXEC dbo.SER_RezervacijePreracunaj @DeoID, @Korisnik;
        FETCH NEXT FROM delovi INTO @DeoID;
    END
    CLOSE delovi;
    DEALLOCATE delovi;
    COMMIT;
END
GO

-- Storno naloga: dozvoljen samo kad su svi izdati delovi vraćeni
CREATE OR ALTER PROCEDURE dbo.SER_RadniNalogStorniraj @RadniNalogID int, @Razlog nvarchar(500), @Korisnik nvarchar(20)
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    DECLARE @Rez TABLE (DeoID int);
    DECLARE @DeoID int;
    BEGIN TRAN;
    IF NOT EXISTS (SELECT 1 FROM dbo.SERRadniNalog WITH (UPDLOCK) WHERE ID = @RadniNalogID AND Status IN (1, 2, 3))
        THROW 50721, N'Stornira se samo otvoren nalog.', 1;
    IF EXISTS (SELECT 1 FROM dbo.SERDeoKretanje WHERE RadniNalogID = @RadniNalogID AND Tip IN ('IZD', 'POV')
               GROUP BY DeoID HAVING SUM(Kolicina) <> 0)
        THROW 50722, N'Nalog ima izdate delove; prvo ih vratite povratom.', 1;
    UPDATE rz SET Status = 4, IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime()
    OUTPUT inserted.DeoID INTO @Rez
    FROM dbo.SERDeoRezervacija rz JOIN dbo.SERRadniNalogStavka s ON s.ID = rz.RadniNalogStavkaID
    WHERE s.RadniNalogID = @RadniNalogID AND rz.Status IN (1, 3);
    UPDATE dbo.SERRadniNalog SET Status = 5, IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime() WHERE ID = @RadniNalogID;
    EXEC dbo.SER_LogUpisi N'SERRadniNalog', @RadniNalogID, N'Storniran', @Razlog, @Korisnik;
    DECLARE delovi CURSOR LOCAL FAST_FORWARD FOR SELECT DISTINCT DeoID FROM @Rez;
    OPEN delovi;
    FETCH NEXT FROM delovi INTO @DeoID;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        EXEC dbo.SER_RezervacijePreracunaj @DeoID, @Korisnik;
        FETCH NEXT FROM delovi INTO @DeoID;
    END
    CLOSE delovi;
    DEALLOCATE delovi;
    COMMIT;
END
GO

-- Ponovno otvaranje završenog naloga (uz razlog): dozvoljava povrat i izmene delova; stornirani nalog se ne otvara
CREATE OR ALTER PROCEDURE dbo.SER_RadniNalogOtvori @RadniNalogID int, @Razlog nvarchar(500), @Korisnik nvarchar(20)
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    IF LEN(LTRIM(RTRIM(ISNULL(@Razlog, N'')))) = 0
        THROW 50741, N'Ponovno otvaranje naloga traži razlog.', 1;
    BEGIN TRAN;
    IF NOT EXISTS (SELECT 1 FROM dbo.SERRadniNalog WITH (UPDLOCK) WHERE ID = @RadniNalogID AND Status = 4)
        THROW 50742, N'Ponovo se otvara samo završen nalog.', 1;
    UPDATE dbo.SERRadniNalog SET Status = 2, DatumZavrsetka = NULL, IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime() WHERE ID = @RadniNalogID;
    EXEC dbo.SER_LogUpisi N'SERRadniNalog', @RadniNalogID, N'Ponovo otvoren', @Razlog, @Korisnik;
    COMMIT;
END
GO

-- Zaglavlje narudžbenice se menja samo u nacrtu
CREATE OR ALTER PROCEDURE dbo.SER_NarudzbenicaZaglavlje
    @ID int, @DobavljacID int, @Datum date, @OcekivaniDatum date = NULL, @Napomena nvarchar(500) = NULL, @Korisnik nvarchar(20)
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRAN;
    IF NOT EXISTS (SELECT 1 FROM dbo.SERNarudzbenica WITH (UPDLOCK) WHERE ID = @ID AND Status = 1)
        THROW 50731, N'Zaglavlje se menja samo dok je narudžbenica nacrt.', 1;
    UPDATE dbo.SERNarudzbenica
    SET DobavljacID = @DobavljacID, Datum = @Datum, OcekivaniDatum = @OcekivaniDatum, Napomena = @Napomena,
        IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime()
    WHERE ID = @ID;
    COMMIT;
END
GO

/* ---------------------------------------------------------------------------------------------
   16. Podesavanja: ključevi Servis.* (dodaju se samo ako ne postoje)
   --------------------------------------------------------------------------------------------- */
IF OBJECT_ID('dbo.Podesavanja', 'U') IS NULL
    PRINT N'Tabela Podesavanja ne postoji; procedure koriste podrazumevane formate i nivoe (100.000 / 500.000 RSD).';
ELSE
BEGIN
    DECLARE @p TABLE (Grupa nvarchar(50), Kljuc nvarchar(100), Vrednost nvarchar(max), Tip varchar(20), Opis nvarchar(250));
    INSERT @p VALUES
     (N'Servis', N'Servis.Delovi.NivoOdobrenja1',          N'100000',                  'DECIMAL', N'Narudžbenica do ovog iznosa (RSD): odobrava šef tehničke službe'),
     (N'Servis', N'Servis.Delovi.NivoOdobrenja2',          N'500000',                  'DECIMAL', N'Narudžbenica do ovog iznosa (RSD): tehnički direktor; preko toga direktor'),
     (N'Servis', N'Servis.Delovi.FormatIzdatnice',         N'IZD-{BROJ:0000}',         'TEXT',    N'Broj izdatnice; {GGGG} = godina, {BROJ:0000} = redni broj sa nulama'),
     (N'Servis', N'Servis.Delovi.FormatPrijemnice',        N'PRM-{BROJ:0000}',         'TEXT',    N'Broj prijemnice'),
     (N'Servis', N'Servis.Delovi.FormatNarudzbenice',      N'NAR-{GGGG}-{BROJ:0000}',  'TEXT',    N'Broj narudžbenice'),
     (N'Servis', N'Servis.Delovi.FormatPovratnice',        N'POV-{BROJ:0000}',         'TEXT',    N'Broj povratnice'),
     (N'Servis', N'Servis.Delovi.FormatPopisa',            N'POP-{GGGG}-{BROJ:0000}',   'TEXT',    N'Broj popisa'),
     (N'Servis', N'Servis.Delovi.FormatPopisnogDokumenta', N'PPR-{BROJ:0000}',         'TEXT',    N'Broj dokumenta kojim se knjiže razlike iz popisa'),
     (N'Servis', N'Servis.Delovi.FormatKorekcije',         N'KOR-{BROJ:0000}',         'TEXT',    N'Broj dokumenta korekcije zaliha'),
     (N'Servis', N'Servis.Delovi.FormatPocetnogStanja',    N'PST-{BROJ:0000}',         'TEXT',    N'Broj dokumenta početnog stanja'),
     (N'Servis', N'Servis.FormatRadnogNaloga',             N'RN-{GGGG}-{BROJ:0000}',   'TEXT',    N'Broj radnog naloga servisa'),
     (N'Servis', N'Servis.Delovi.TolerancijaPrijema',      N'0',                       'DECIMAL', N'Dozvoljeno prekoračenje preostale količine narudžbenice na prijemu (%); preko toga prijem traži komentar'),
     (N'Servis', N'Servis.Delovi.ZabranaSamoodobravanja',  N'1',                       'BOOL',    N'1 = isti korisnik ne sme da odobri narudžbenicu, popis ili korekciju koje je sam podneo; 0 = dozvoljeno, ali se upisuje u SERLog');
    INSERT dbo.Podesavanja (Grupa, Kljuc, Vrednost, TipVrednosti, Opis)
    SELECT p.Grupa, p.Kljuc, p.Vrednost, p.Tip, p.Opis FROM @p p
    WHERE NOT EXISTS (SELECT 1 FROM dbo.Podesavanja x WHERE x.Kljuc = p.Kljuc);
    -- P21: popisi numerisani starim formatom (POP-2026-001) dobijaju 4 cifre (POP-2026-0001), ako taj broj nije zauzet
    UPDATE p SET Broj = LEFT(p.Broj, LEN(p.Broj) - 3) + N'0' + RIGHT(p.Broj, 3)
    FROM dbo.SERPopis p
    WHERE p.Broj LIKE N'POP-[0-9][0-9][0-9][0-9]-[0-9][0-9][0-9]'
      AND NOT EXISTS (SELECT 1 FROM dbo.SERPopis x WHERE x.Broj = LEFT(p.Broj, LEN(p.Broj) - 3) + N'0' + RIGHT(p.Broj, 3));
    -- format broja popisa je bio sa 3 cifre; usklađen sa ostalim dokumentima (menja se samo nepromenjena podrazumevana vrednost)
    UPDATE dbo.Podesavanja SET Vrednost = N'POP-{GGGG}-{BROJ:0000}'
    WHERE Kljuc = N'Servis.Delovi.FormatPopisa' AND Vrednost = N'POP-{GGGG}-{BROJ:000}';
END
GO

/* ---------------------------------------------------------------------------------------------
   17. Glavni prozor: modul 'Održavanje' (dugme btnOdrzavanje u NewMain) i prava iz poglavlja 7
      Ako modul ne postoji, pravi se kao kopija zapisa modula 'Administracija' (kao HR skripta).
      Kartice su samo prava. Tri kartice „Odobravanje …“ određuju nivo odobravanja narudžbenice (MD-R07),
      a šef tehničke službe odobrava i popis (MD-R10) i korekciju (MD-R11).
      Korisnici sa bilo kojim pravom u Administraciji dobijaju sva prava; ostalima prava
      dodeljuje administrator kroz postojeći ekran za prava.
   --------------------------------------------------------------------------------------------- */
IF OBJECT_ID('dbo.MainNovi', 'U') IS NOT NULL AND OBJECT_ID('dbo.mainNoviKartice', 'U') IS NOT NULL AND OBJECT_ID('dbo.MainNoviPrava', 'U') IS NOT NULL
BEGIN
    DECLARE @AdminMain int = (SELECT TOP (1) ID FROM dbo.MainNovi WHERE LTRIM(RTRIM(Naziv)) = N'Administracija' ORDER BY ID);
    DECLARE @SerMain int = (SELECT TOP (1) ID FROM dbo.MainNovi WHERE LTRIM(RTRIM(Naziv)) = N'Održavanje' ORDER BY ID);
    DECLARE @sql nvarchar(max), @kolone nvarchar(max), @vrednosti nvarchar(max);

    IF @AdminMain IS NULL
        PRINT N'Modul Administracija nije pronađen u MainNovi; modul Održavanje i prava treba dodati ručno.';
    ELSE
    BEGIN
        IF @SerMain IS NULL
        BEGIN
            SELECT @kolone = STUFF((SELECT N', ' + QUOTENAME(c.name) FROM sys.columns c
                                    WHERE c.object_id = OBJECT_ID('dbo.MainNovi') AND c.is_identity = 0 AND c.is_computed = 0
                                      AND TYPE_NAME(c.system_type_id) <> 'timestamp'
                                    ORDER BY c.column_id FOR XML PATH('')), 1, 2, N''),
                   @vrednosti = STUFF((SELECT N', ' + CASE c.name WHEN 'Naziv' THEN N'N''Održavanje'''
                                                                   WHEN 'ID' THEN N'(SELECT ISNULL(MAX(ID), 0) + 1 FROM dbo.MainNovi)'
                                                                   ELSE QUOTENAME(c.name) END
                                       FROM sys.columns c
                                       WHERE c.object_id = OBJECT_ID('dbo.MainNovi') AND c.is_identity = 0 AND c.is_computed = 0
                                         AND TYPE_NAME(c.system_type_id) <> 'timestamp'
                                       ORDER BY c.column_id FOR XML PATH('')), 1, 2, N'');
            SET @sql = N'INSERT INTO dbo.MainNovi (' + @kolone + N') SELECT ' + @vrednosti + N' FROM dbo.MainNovi WHERE ID = @Izvor';
            EXEC sp_executesql @sql, N'@Izvor int', @Izvor = @AdminMain;
            SET @SerMain = (SELECT TOP (1) ID FROM dbo.MainNovi WHERE LTRIM(RTRIM(Naziv)) = N'Održavanje' ORDER BY ID);
            PRINT N'Dodat modul Održavanje, ID = ' + CAST(@SerMain AS nvarchar(10));
        END

        DECLARE @Sablon int = (SELECT TOP (1) ID FROM dbo.mainNoviKartice WHERE MainID = @AdminMain AND Parent IS NULL ORDER BY ID);
        IF @Sablon IS NULL
            SET @Sablon = (SELECT TOP (1) ID FROM dbo.mainNoviKartice WHERE Parent IS NULL ORDER BY ID);

        DECLARE @Kartica nvarchar(50), @KarticaID int;
        DECLARE kartice CURSOR LOCAL FAST_FORWARD FOR
            SELECT Naziv FROM (VALUES (N'Delovi'), (N'Delovi magacioner'), (N'Delovi nabavka'), (N'Delovi korekcija'),
                                      (N'Odobravanje šef tehničke službe'), (N'Odobravanje tehnički direktor'), (N'Odobravanje direktor')) n (Naziv);
        OPEN kartice;
        FETCH NEXT FROM kartice INTO @Kartica;
        WHILE @@FETCH_STATUS = 0 AND @Sablon IS NOT NULL
        BEGIN
            SET @KarticaID = (SELECT TOP (1) ID FROM dbo.mainNoviKartice WHERE MainID = @SerMain AND Parent IS NULL AND Naziv = @Kartica);
            IF @KarticaID IS NULL
            BEGIN
                SELECT @kolone = STUFF((SELECT N', ' + QUOTENAME(c.name) FROM sys.columns c
                                        WHERE c.object_id = OBJECT_ID('dbo.mainNoviKartice') AND c.is_identity = 0 AND c.is_computed = 0
                                          AND TYPE_NAME(c.system_type_id) <> 'timestamp'
                                        ORDER BY c.column_id FOR XML PATH('')), 1, 2, N''),
                       @vrednosti = STUFF((SELECT N', ' + CASE c.name WHEN 'Naziv' THEN N'@Kartica'
                                                                       WHEN 'MainID' THEN N'@SerMain'
                                                                       WHEN 'Parent' THEN N'NULL'
                                                                       WHEN 'ID' THEN N'(SELECT ISNULL(MAX(ID), 0) + 1 FROM dbo.mainNoviKartice)'
                                                                       ELSE QUOTENAME(c.name) END
                                           FROM sys.columns c
                                           WHERE c.object_id = OBJECT_ID('dbo.mainNoviKartice') AND c.is_identity = 0 AND c.is_computed = 0
                                             AND TYPE_NAME(c.system_type_id) <> 'timestamp'
                                           ORDER BY c.column_id FOR XML PATH('')), 1, 2, N'');
                SET @sql = N'INSERT INTO dbo.mainNoviKartice (' + @kolone + N') SELECT ' + @vrednosti + N' FROM dbo.mainNoviKartice WHERE ID = @Izvor';
                EXEC sp_executesql @sql, N'@Izvor int, @Kartica nvarchar(50), @SerMain int', @Izvor = @Sablon, @Kartica = @Kartica, @SerMain = @SerMain;
                SET @KarticaID = (SELECT TOP (1) ID FROM dbo.mainNoviKartice WHERE MainID = @SerMain AND Parent IS NULL AND Naziv = @Kartica);
                PRINT N'Dodata kartica ' + @Kartica + N', ID = ' + CAST(@KarticaID AS nvarchar(10));
            END

            -- prava: po jedan red za svakog korisnika koji ima pravo u Administraciji (kopija njegovog prvog reda)
            SELECT @kolone = STUFF((SELECT N', ' + QUOTENAME(c.name) FROM sys.columns c
                                    WHERE c.object_id = OBJECT_ID('dbo.MainNoviPrava') AND c.is_identity = 0 AND c.is_computed = 0
                                      AND TYPE_NAME(c.system_type_id) <> 'timestamp'
                                    ORDER BY c.column_id FOR XML PATH('')), 1, 2, N''),
                   @vrednosti = STUFF((SELECT N', ' + CASE c.name WHEN 'KarticaID' THEN N'@KarticaID'
                                                                   WHEN 'MainID' THEN N'@SerMain'
                                                                   WHEN 'ID' THEN N'(SELECT ISNULL(MAX(ID), 0) FROM dbo.MainNoviPrava) + ROW_NUMBER() OVER (ORDER BY (SELECT 1))'
                                                                   ELSE N'p.' + QUOTENAME(c.name) END
                                       FROM sys.columns c
                                       WHERE c.object_id = OBJECT_ID('dbo.MainNoviPrava') AND c.is_identity = 0 AND c.is_computed = 0
                                         AND TYPE_NAME(c.system_type_id) <> 'timestamp'
                                       ORDER BY c.column_id FOR XML PATH('')), 1, 2, N'');
            SET @sql = N'INSERT INTO dbo.MainNoviPrava (' + @kolone + N') SELECT ' + @vrednosti
                     + N' FROM (SELECT *, ROW_NUMBER() OVER (PARTITION BY Korisnik ORDER BY KarticaID) AS rbKorisnika'
                     + N'       FROM dbo.MainNoviPrava WHERE MainID = @AdminMain) p'
                     + N' WHERE p.rbKorisnika = 1'
                     + N'   AND NOT EXISTS (SELECT 1 FROM dbo.MainNoviPrava x WHERE x.MainID = @SerMain AND x.KarticaID = @KarticaID AND x.Korisnik = p.Korisnik);';
            EXEC sp_executesql @sql, N'@AdminMain int, @SerMain int, @KarticaID int', @AdminMain = @AdminMain, @SerMain = @SerMain, @KarticaID = @KarticaID;

            FETCH NEXT FROM kartice INTO @Kartica;
        END
        CLOSE kartice;
        DEALLOCATE kartice;
    END
END
GO

DROP PROCEDURE #SER_DodajFK;
GO

PRINT N'Servis – Magacin rezervnih delova: skripta završena.';
GO
