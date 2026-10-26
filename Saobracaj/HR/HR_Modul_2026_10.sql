/* =============================================================================================
   HR modul: Zaposleni (kadrovska evidencija) i Zahtev za nabavku
   Specifikacija: modul-zaposleni.md, zahtev-za-nabavku.md (verzija 0.1, 02.10.2026.)

   Skripta se može pokretati više puta: tabele i kolone se dodaju samo ako ne postoje,
   šifarnici se pune samo kad su prazni, pogledi i procedure se prave sa CREATE OR ALTER.

   Postojeće tabele se koriste, ne prave se ponovo:
     Zaposleni (PK ID)              proširuje se kolonama iz specifikacije (poglavlje 3.1)
     OrganizacioneJedinice (ID)     dodaje se NadredjenaJedinica
     Drzave (DrSifra)               šifra države se čuva kao tekst (DrSifra), bez FK
     Valute (VaSifra)               valuta zahteva i ponude, bez FK
     Partnerji (PaSifra)            dobavljač u ponudi i predloženi dobavljač, bez FK
     Stanice (ID)                   službeno mesto (matično mesto rada, mesto povrede, isporuke), bez FK

   Razlike u odnosu na specifikaciju:
     - Strani ključ ka zaposlenom pokazuje na Zaposleni.ID (postojeći PK), a ne na Sifra.
     - Audit kolone KreiraoKorisnik / IzmenioKorisnik su nvarchar(20): aplikacija korisnika
       prepoznaje po korisničkom imenu (frmLogovanje.user), a ne po broju.
     - Zaposleni.KorisnickoIme povezuje prijavljenog korisnika sa zaposlenim
       (lista "Čeka moje odobrenje", podnosilac zahteva).
     - Tabele Sredstva, RadniNalozi i Artikli ne postoje: kolone Sredstvo / RadniNalog / Artikal
       ostaju int bez FK, a za sada se popunjavaju tekstualne kolone SredstvoOpis i RadniNalogOpis.
     - HTZReversi ima i kolonu Storniran (greška na reversu se ispravlja storniranjem).
     - ZahteviZaNabavku ima i OdobreniIznos (povećanje iznosa posle odobrenja vraća zahtev na odobravanje).
   ============================================================================================= */

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

-- Pomoćna procedura: dodaje strani ključ ako ne postoji. Ako referencirana kolona nije PK/UNIQUE
-- (npr. starija baza bez PK na Zaposleni.ID), ključ se preskače uz poruku; aplikacija svejedno radi.
IF OBJECT_ID('tempdb..#HR_DodajFK') IS NOT NULL DROP PROCEDURE #HR_DodajFK;
GO
CREATE PROCEDURE #HR_DodajFK @Naziv sysname, @Tabela sysname, @Kolona sysname, @RefTabela sysname, @RefKolona sysname
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
   1. Šifarnici modula Zaposleni
   --------------------------------------------------------------------------------------------- */
IF OBJECT_ID('dbo.ZaposleniStatusi', 'U') IS NULL
    CREATE TABLE dbo.ZaposleniStatusi (
        Status  char(1)      NOT NULL CONSTRAINT PK_ZaposleniStatusi PRIMARY KEY,
        Naziv   nvarchar(30) NOT NULL,
        Aktivan bit          NOT NULL CONSTRAINT DF_ZaposleniStatusi_Aktivan DEFAULT 1);
IF NOT EXISTS (SELECT 1 FROM dbo.ZaposleniStatusi)
    INSERT dbo.ZaposleniStatusi (Status, Naziv) VALUES ('A', N'Aktivan'), ('P', N'Pasivan');
GO

IF OBJECT_ID('dbo.Opstine', 'U') IS NULL
    CREATE TABLE dbo.Opstine (
        Sifra   int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Opstine PRIMARY KEY,
        Naziv   nvarchar(50)  NOT NULL,
        Drzava  nvarchar(10)  NULL,          -- Drzave.DrSifra
        Aktivan bit           NOT NULL CONSTRAINT DF_Opstine_Aktivan DEFAULT 1);
GO

IF OBJECT_ID('dbo.RadnaMesta', 'U') IS NULL
    CREATE TABLE dbo.RadnaMesta (
        Sifra   int IDENTITY(1,1) NOT NULL CONSTRAINT PK_RadnaMesta PRIMARY KEY,
        Naziv   nvarchar(100) NOT NULL,
        Aktivan bit           NOT NULL CONSTRAINT DF_RadnaMesta_Aktivan DEFAULT 1);
GO

IF OBJECT_ID('dbo.OrganizacioneJedinice', 'U') IS NULL
    CREATE TABLE dbo.OrganizacioneJedinice (
        ID    int IDENTITY(1,1) NOT NULL CONSTRAINT PK_OrganizacioneJedinice PRIMARY KEY,
        Naziv nvarchar(100) NOT NULL);
IF COL_LENGTH('dbo.OrganizacioneJedinice', 'NadredjenaJedinica') IS NULL
    ALTER TABLE dbo.OrganizacioneJedinice ADD NadredjenaJedinica int NULL;
GO

-- Šifarnici sa kolonama Sifra, Naziv, Aktivan i početnim vrednostima
DECLARE @sifarnici TABLE (Tabela sysname, Vrednosti nvarchar(max));
INSERT @sifarnici VALUES
 (N'RadniOdnosi',            N'Određeno|Neodređeno|Ugovor o delu|Privremeni i povremeni poslovi'),
 (N'StepenObrazovanja',      N'I - osnovno obrazovanje|II - stručno osposobljavanje|III - srednje stručno (3 godine)|IV - srednje (4 godine)|V - specijalističko / majstorsko|VI.1 - osnovne strukovne / akademske (180 ESPB)|VI.2 - osnovne (240 ESPB)|VII.1 - master|VII.2 - specijalističke akademske|VIII - doktorske studije'),
 (N'GrupeDokumenata',        N'Lični dokument|Sertifikat|Licenca|Obuka'),
 (N'OceneSposobnosti',       N'Sposoban|Sposoban sa ograničenjem|Privremeno nesposoban|Nesposoban'),
 (N'HTZGrupe',               N'Obuća|Odeća|Zaštita glave|Zaštita ruku|Zaštita očiju i lica|Zaštita sluha|Zaštita od pada'),
 (N'RazloziRazduzenja',      N'Istekao rok|Oštećenje|Gubitak|Prestanak radnog odnosa'),
 (N'Srodstva',               N'Supružnik|Dete|Roditelj|Brat/sestra|Ostalo'),
 (N'TezinePovreda',          N'Laka|Teška|Smrtna|Kolektivna'),
 (N'VrsteUgovora',           N'Ugovor o radu|Aneks|Ugovor o delu|Ugovor o privremenim i povremenim poslovima'),
 (N'VrsteDisciplinskihMera', N'Opomena|Novčana kazna|Udaljenje sa rada|Otkaz');

DECLARE @t sysname, @v nvarchar(max), @sql nvarchar(max), @n int, @ostatak nvarchar(max), @deo nvarchar(200);
DECLARE c CURSOR LOCAL FAST_FORWARD FOR SELECT Tabela, Vrednosti FROM @sifarnici;
OPEN c;
FETCH NEXT FROM c INTO @t, @v;
WHILE @@FETCH_STATUS = 0
BEGIN
    IF OBJECT_ID(N'dbo.' + @t, 'U') IS NULL
    BEGIN
        SET @sql = N'CREATE TABLE dbo.' + QUOTENAME(@t) + N' ('
                 + N'Sifra int IDENTITY(1,1) NOT NULL CONSTRAINT ' + QUOTENAME(N'PK_' + @t) + N' PRIMARY KEY, '
                 + N'Naziv nvarchar(100) NOT NULL, '
                 + N'Aktivan bit NOT NULL CONSTRAINT ' + QUOTENAME(N'DF_' + @t + N'_Aktivan') + N' DEFAULT 1);';
        EXEC sp_executesql @sql;
    END
    -- početne vrednosti samo u prazan šifarnik, redom kako su navedene (bez STRING_SPLIT, radi starijeg nivoa kompatibilnosti baze)
    SET @sql = N'SELECT @n = COUNT(*) FROM dbo.' + QUOTENAME(@t) + N';';
    EXEC sp_executesql @sql, N'@n int OUTPUT', @n = @n OUTPUT;
    IF @n = 0
    BEGIN
        SET @ostatak = @v + N'|';
        WHILE CHARINDEX(N'|', @ostatak) > 0
        BEGIN
            SET @deo = LEFT(@ostatak, CHARINDEX(N'|', @ostatak) - 1);
            SET @ostatak = SUBSTRING(@ostatak, CHARINDEX(N'|', @ostatak) + 1, 4000);
            SET @sql = N'INSERT dbo.' + QUOTENAME(@t) + N' (Naziv) VALUES (@deo);';
            EXEC sp_executesql @sql, N'@deo nvarchar(200)', @deo = @deo;
        END
    END
    FETCH NEXT FROM c INTO @t, @v;
END
CLOSE c;
DEALLOCATE c;
GO

IF OBJECT_ID('dbo.VrstePregleda', 'U') IS NULL
    CREATE TABLE dbo.VrstePregleda (
        Sifra        int IDENTITY(1,1) NOT NULL CONSTRAINT PK_VrstePregleda PRIMARY KEY,
        Naziv        nvarchar(100) NOT NULL,
        PeriodMeseci int           NULL,
        Aktivan      bit           NOT NULL CONSTRAINT DF_VrstePregleda_Aktivan DEFAULT 1);
IF NOT EXISTS (SELECT 1 FROM dbo.VrstePregleda)
    INSERT dbo.VrstePregleda (Naziv, PeriodMeseci) VALUES (N'Prethodni', NULL), (N'Periodični', 12), (N'Vanredni', NULL);
GO

IF OBJECT_ID('dbo.VrsteDokumenta', 'U') IS NULL
    CREATE TABLE dbo.VrsteDokumenta (
        Sifra          int IDENTITY(1,1) NOT NULL CONSTRAINT PK_VrsteDokumenta PRIMARY KEY,
        Naziv          nvarchar(50) NOT NULL,
        GrupaDokumenta int          NOT NULL CONSTRAINT FK_VrsteDokumenta_Grupa REFERENCES dbo.GrupeDokumenata (Sifra),
        ImaRokVazenja  bit          NOT NULL CONSTRAINT DF_VrsteDokumenta_ImaRok DEFAULT 0,
        DanaUpozorenja int          NULL,
        ImaKategorije  bit          NOT NULL CONSTRAINT DF_VrsteDokumenta_ImaKat DEFAULT 0,
        Aktivan        bit          NOT NULL CONSTRAINT DF_VrsteDokumenta_Aktivan DEFAULT 1);
IF NOT EXISTS (SELECT 1 FROM dbo.VrsteDokumenta)
    INSERT dbo.VrsteDokumenta (Naziv, GrupaDokumenta, ImaRokVazenja, DanaUpozorenja, ImaKategorije)
    SELECT v.Naziv, g.Sifra, v.ImaRok, v.Dana, v.ImaKat
    FROM (VALUES (N'Lična karta',         N'Lični dokument', 1, 30, 0),
                 (N'Pasoš',               N'Lični dokument', 1, 30, 0),
                 (N'Vozačka dozvola',     N'Lični dokument', 1, 30, 1),
                 (N'Zdravstvena knjižica',N'Lični dokument', 0, NULL, 0),
                 (N'Licenca mašinovođe',  N'Licenca',        1, 60, 0),
                 (N'BZR obuka',           N'Obuka',          1, 30, 0),
                 (N'Provera znanja',      N'Obuka',          1, 30, 0)) v (Naziv, Grupa, ImaRok, Dana, ImaKat)
    JOIN dbo.GrupeDokumenata g ON g.Naziv = v.Grupa;
GO

IF OBJECT_ID('dbo.HTZOprema', 'U') IS NULL
    CREATE TABLE dbo.HTZOprema (
        Sifra             int IDENTITY(1,1) NOT NULL CONSTRAINT PK_HTZOprema PRIMARY KEY,
        Naziv             nvarchar(100) NOT NULL,
        GrupaHTZ          int           NULL CONSTRAINT FK_HTZOprema_Grupa REFERENCES dbo.HTZGrupe (Sifra),
        JedinicaMere      nvarchar(10)  NOT NULL CONSTRAINT DF_HTZOprema_JM DEFAULT N'kom',
        RokTrajanjaMeseci int           NULL,
        ImaVelicinu       bit           NOT NULL CONSTRAINT DF_HTZOprema_ImaVel DEFAULT 0,
        Aktivan           bit           NOT NULL CONSTRAINT DF_HTZOprema_Aktivan DEFAULT 1);
GO

/* ---------------------------------------------------------------------------------------------
   2. Zaposleni: postojeća tabela se proširuje (poglavlje 3.1)
      Postojeće kolone: email = Email, Telefon = Telefon1, SifraERP, Napomena, OrganizacionaJedinica,
      TipRadnika (stari tekstualni opis radnog mesta), SkolskaSprema.
   --------------------------------------------------------------------------------------------- */
IF OBJECT_ID('dbo.Zaposleni', 'U') IS NULL
    CREATE TABLE dbo.Zaposleni (
        ID       int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Zaposleni PRIMARY KEY,
        Prezime  nvarchar(100) NULL,
        Ime      nvarchar(100) NULL,
        email    nvarchar(100) NULL,
        Telefon  nvarchar(100) NULL,
        Napomena nvarchar(500) NULL,
        SifraERP nvarchar(30)  NULL);
GO

DECLARE @kolone TABLE (Kolona sysname, Definicija nvarchar(200));
INSERT @kolone VALUES
 (N'Status',              N'char(1) NOT NULL CONSTRAINT DF_Zaposleni_Status DEFAULT ''A'''),
 (N'Code',                N'nvarchar(30) NULL'),
 (N'SrednjePrezime',      N'nvarchar(35) NULL'),
 (N'ImeOca',              N'nvarchar(35) NULL'),
 (N'ImeMajke',            N'nvarchar(35) NULL'),
 (N'Pol',                 N'char(1) NULL'),
 (N'DatumRodjenja',       N'date NULL'),
 (N'MestoRodjenja',       N'nvarchar(35) NULL'),
 (N'OpstinaRodjenja',     N'int NULL'),
 (N'DrzavaRodjenja',      N'nvarchar(10) NULL'),
 (N'JMBG',                N'char(13) NULL'),
 (N'Telefon2',            N'nvarchar(20) NULL'),
 (N'AdresaStalnoPreb',    N'nvarchar(100) NULL'),
 (N'OpstinaStalnoPreb',   N'int NULL'),
 (N'DrzavaStalnoPreb',    N'nvarchar(10) NULL'),
 (N'AdresaPrivPreb',      N'nvarchar(100) NULL'),
 (N'OpstinaPrivPreb',     N'int NULL'),
 (N'DrzavaPrivPreb',      N'nvarchar(10) NULL'),
 (N'DatumUlaska',         N'date NULL'),
 (N'DatumIzlaska',        N'date NULL'),
 (N'PrethodniStazGodina', N'tinyint NULL'),
 (N'PrethodniStazMeseci', N'tinyint NULL'),
 (N'PrethodniStazDana',   N'tinyint NULL'),
 (N'KonfekcijskiBroj',    N'nvarchar(10) NULL'),
 (N'BrojObuce',           N'nvarchar(10) NULL'),
 (N'KorisnickoIme',       N'nvarchar(20) NULL'),
 (N'KreiraoKorisnik',     N'nvarchar(20) NULL'),
 (N'DatumKreiranja',      N'datetime2 NULL CONSTRAINT DF_Zaposleni_DatumKreiranja DEFAULT sysdatetime()'),
 (N'IzmenioKorisnik',     N'nvarchar(20) NULL'),
 (N'DatumIzmene',         N'datetime2 NULL'),
 (N'RowVer',              N'rowversion');

DECLARE @k sysname, @d nvarchar(200), @sql nvarchar(max);
DECLARE c CURSOR LOCAL FAST_FORWARD FOR SELECT Kolona, Definicija FROM @kolone;
OPEN c;
FETCH NEXT FROM c INTO @k, @d;
WHILE @@FETCH_STATUS = 0
BEGIN
    -- tabela može imati samo jednu rowversion kolonu; ako je već ima (pod drugim imenom), RowVer se ne dodaje
    IF COL_LENGTH('dbo.Zaposleni', @k) IS NULL
       AND NOT (@k = N'RowVer' AND EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Zaposleni') AND system_type_id = 189))
    BEGIN
        SET @sql = N'ALTER TABLE dbo.Zaposleni ADD ' + QUOTENAME(@k) + N' ' + @d + N';';
        EXEC sp_executesql @sql;
    END
    FETCH NEXT FROM c INTO @k, @d;
END
CLOSE c;
DEALLOCATE c;
GO

-- stare kolone koje HR forma ne popunjava (TipRadnika, SkolskaSprema, ...) moraju da dozvole NULL;
-- menja se samo nullability, tip kolone ostaje isti
DECLARE @k sysname, @sql nvarchar(max);
DECLARE c CURSOR LOCAL FAST_FORWARD FOR
    SELECT c.name,
           N'ALTER TABLE dbo.Zaposleni ALTER COLUMN ' + QUOTENAME(c.name) + N' ' + TYPE_NAME(c.user_type_id)
           + CASE WHEN TYPE_NAME(c.user_type_id) IN ('nvarchar', 'nchar') THEN N'(' + CASE WHEN c.max_length = -1 THEN N'max' ELSE CAST(c.max_length / 2 AS nvarchar(10)) END + N')'
                  WHEN TYPE_NAME(c.user_type_id) IN ('varchar', 'char', 'varbinary') THEN N'(' + CASE WHEN c.max_length = -1 THEN N'max' ELSE CAST(c.max_length AS nvarchar(10)) END + N')'
                  WHEN TYPE_NAME(c.user_type_id) IN ('decimal', 'numeric') THEN N'(' + CAST(c.precision AS nvarchar(10)) + N',' + CAST(c.scale AS nvarchar(10)) + N')'
                  ELSE N'' END
           + CASE WHEN c.collation_name IS NOT NULL THEN N' COLLATE ' + c.collation_name ELSE N'' END
           + N' NULL;'
    FROM sys.columns c
    WHERE c.object_id = OBJECT_ID('dbo.Zaposleni') AND c.is_nullable = 0 AND c.is_identity = 0 AND c.is_computed = 0
      AND c.name IN (N'TipRadnika', N'Datum', N'Korisnik', N'SkolskaSprema', N'OrganizacionaJedinica', N'email', N'Telefon', N'Napomena', N'SifraERP');
OPEN c;
FETCH NEXT FROM c INTO @k, @sql;
WHILE @@FETCH_STATUS = 0
BEGIN
    BEGIN TRY
        EXEC sp_executesql @sql;
    END TRY
    BEGIN CATCH
        PRINT N'Kolona Zaposleni.' + @k + N' ostaje NOT NULL: ' + ERROR_MESSAGE();
    END CATCH
    FETCH NEXT FROM c INTO @k, @sql;
END
CLOSE c;
DEALLOCATE c;
GO

-- postojeći zaposleni dobijaju Code jednak broju (kadrovska služba ga posle menja)
UPDATE dbo.Zaposleni SET Code = CAST(ID AS nvarchar(30)) WHERE Code IS NULL OR LTRIM(RTRIM(Code)) = N'';
GO

IF OBJECT_ID('dbo.CK_Zaposleni_Pol', 'C') IS NULL
    ALTER TABLE dbo.Zaposleni WITH NOCHECK ADD CONSTRAINT CK_Zaposleni_Pol CHECK (Pol IS NULL OR Pol IN ('M', 'Z'));
IF OBJECT_ID('dbo.CK_Zaposleni_DatumIzlaska', 'C') IS NULL
    ALTER TABLE dbo.Zaposleni WITH NOCHECK ADD CONSTRAINT CK_Zaposleni_DatumIzlaska CHECK (DatumIzlaska IS NULL OR DatumUlaska IS NULL OR DatumIzlaska >= DatumUlaska);
GO

-- jedinstveni Code, SifraERP i JMBG (filtrirani indeksi); postojeći duplikati se prijavljuju, ne brišu
BEGIN TRY
    IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UX_Zaposleni_Code' AND object_id = OBJECT_ID('dbo.Zaposleni'))
        CREATE UNIQUE INDEX UX_Zaposleni_Code ON dbo.Zaposleni (Code) WHERE Code IS NOT NULL;
END TRY BEGIN CATCH PRINT N'UX_Zaposleni_Code nije napravljen (dupli Code): ' + ERROR_MESSAGE(); END CATCH
BEGIN TRY
    IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UX_Zaposleni_SifraERP' AND object_id = OBJECT_ID('dbo.Zaposleni'))
        EXEC (N'CREATE UNIQUE INDEX UX_Zaposleni_SifraERP ON dbo.Zaposleni (SifraERP) WHERE SifraERP IS NOT NULL AND SifraERP <> N''''');
END TRY BEGIN CATCH PRINT N'UX_Zaposleni_SifraERP nije napravljen (dupla SifraERP): ' + ERROR_MESSAGE(); END CATCH
BEGIN TRY
    IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UX_Zaposleni_JMBG' AND object_id = OBJECT_ID('dbo.Zaposleni'))
        CREATE UNIQUE INDEX UX_Zaposleni_JMBG ON dbo.Zaposleni (JMBG) WHERE JMBG IS NOT NULL;
END TRY BEGIN CATCH PRINT N'UX_Zaposleni_JMBG nije napravljen (dupli JMBG): ' + ERROR_MESSAGE(); END CATCH
GO

EXEC #HR_DodajFK N'FK_Zaposleni_Status',          N'Zaposleni', N'Status',            N'ZaposleniStatusi', N'Status';
EXEC #HR_DodajFK N'FK_Zaposleni_OpstinaRodjenja', N'Zaposleni', N'OpstinaRodjenja',   N'Opstine', N'Sifra';
EXEC #HR_DodajFK N'FK_Zaposleni_OpstinaStalno',   N'Zaposleni', N'OpstinaStalnoPreb', N'Opstine', N'Sifra';
EXEC #HR_DodajFK N'FK_Zaposleni_OpstinaPriv',     N'Zaposleni', N'OpstinaPrivPreb',   N'Opstine', N'Sifra';
EXEC #HR_DodajFK N'FK_OrganizacioneJedinice_Nadredjena', N'OrganizacioneJedinice', N'NadredjenaJedinica', N'OrganizacioneJedinice', N'ID';
GO

/* ---------------------------------------------------------------------------------------------
   3. Istorijske i prateće tabele zaposlenog (poglavlje 4)
   --------------------------------------------------------------------------------------------- */
IF OBJECT_ID('dbo.ZaposleniSlike', 'U') IS NULL
    CREATE TABLE dbo.ZaposleniSlike (
        Zaposleni   int            NOT NULL CONSTRAINT PK_ZaposleniSlike PRIMARY KEY,
        Slika       varbinary(max) NOT NULL,
        TipFajla    nvarchar(50)   NULL,
        DatumIzmene datetime2      NOT NULL CONSTRAINT DF_ZaposleniSlike_DatumIzmene DEFAULT sysdatetime());

IF OBJECT_ID('dbo.ZaposleniRadnaMesta', 'U') IS NULL
    CREATE TABLE dbo.ZaposleniRadnaMesta (
        Sifra                 int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ZaposleniRadnaMesta PRIMARY KEY,
        Zaposleni             int           NOT NULL,
        RadnoMesto            int           NOT NULL CONSTRAINT FK_ZaposleniRadnaMesta_RadnoMesto REFERENCES dbo.RadnaMesta (Sifra),
        VrstaRadnogOdnosa     int           NOT NULL CONSTRAINT FK_ZaposleniRadnaMesta_Odnos REFERENCES dbo.RadniOdnosi (Sifra),
        OrganizacionaJedinica int           NULL,
        SluzbenoMesto         int           NULL,      -- Stanice.ID
        DatumOd               date          NOT NULL,
        DatumDo               date          NULL,
        Napomena              nvarchar(500) NULL,
        KreiraoKorisnik nvarchar(20) NULL, DatumKreiranja datetime2 NOT NULL CONSTRAINT DF_ZaposleniRadnaMesta_DK DEFAULT sysdatetime(),
        IzmenioKorisnik nvarchar(20) NULL, DatumIzmene datetime2 NULL, RowVer rowversion,
        CONSTRAINT CK_ZaposleniRadnaMesta_Period CHECK (DatumDo IS NULL OR DatumDo >= DatumOd));

IF OBJECT_ID('dbo.ZaposleniUgovori', 'U') IS NULL
    CREATE TABLE dbo.ZaposleniUgovori (
        Sifra               int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ZaposleniUgovori PRIMARY KEY,
        Zaposleni           int           NOT NULL,
        VrstaUgovora        int           NOT NULL CONSTRAINT FK_ZaposleniUgovori_Vrsta REFERENCES dbo.VrsteUgovora (Sifra),
        BrojUgovora         nvarchar(30)  NULL,
        DatumUgovora        date          NOT NULL,
        VaziOd              date          NOT NULL,
        VaziDo              date          NULL,
        ZaposleniRadnoMesto int           NULL CONSTRAINT FK_ZaposleniUgovori_RadnoMesto REFERENCES dbo.ZaposleniRadnaMesta (Sifra),
        Napomena            nvarchar(500) NULL,
        KreiraoKorisnik nvarchar(20) NULL, DatumKreiranja datetime2 NOT NULL CONSTRAINT DF_ZaposleniUgovori_DK DEFAULT sysdatetime(),
        IzmenioKorisnik nvarchar(20) NULL, DatumIzmene datetime2 NULL, RowVer rowversion,
        CONSTRAINT CK_ZaposleniUgovori_Period CHECK (VaziDo IS NULL OR VaziDo >= VaziOd));

IF OBJECT_ID('dbo.ZaposleniMentori', 'U') IS NULL
    CREATE TABLE dbo.ZaposleniMentori (
        Sifra     int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ZaposleniMentori PRIMARY KEY,
        Zaposleni int           NOT NULL,
        Mentor    int           NOT NULL,
        DatumOd   date          NOT NULL,
        DatumDo   date          NULL,
        Napomena  nvarchar(500) NULL,
        KreiraoKorisnik nvarchar(20) NULL, DatumKreiranja datetime2 NOT NULL CONSTRAINT DF_ZaposleniMentori_DK DEFAULT sysdatetime(),
        IzmenioKorisnik nvarchar(20) NULL, DatumIzmene datetime2 NULL, RowVer rowversion,
        CONSTRAINT CK_ZaposleniMentori_Mentor CHECK (Mentor <> Zaposleni),
        CONSTRAINT CK_ZaposleniMentori_Period CHECK (DatumDo IS NULL OR DatumDo >= DatumOd));

IF OBJECT_ID('dbo.ZaposleniMentorBeleske', 'U') IS NULL
    CREATE TABLE dbo.ZaposleniMentorBeleske (
        Sifra           int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ZaposleniMentorBeleske PRIMARY KEY,
        ZaposleniMentor int            NOT NULL CONSTRAINT FK_ZaposleniMentorBeleske_Mentor REFERENCES dbo.ZaposleniMentori (Sifra) ON DELETE CASCADE,
        Datum           date           NOT NULL,
        Beleska         nvarchar(2000) NOT NULL,
        KreiraoKorisnik nvarchar(20) NULL, DatumKreiranja datetime2 NOT NULL CONSTRAINT DF_ZaposleniMentorBeleske_DK DEFAULT sysdatetime(),
        IzmenioKorisnik nvarchar(20) NULL, DatumIzmene datetime2 NULL, RowVer rowversion);

IF OBJECT_ID('dbo.ZaposleniSkolovanje', 'U') IS NULL
    CREATE TABLE dbo.ZaposleniSkolovanje (
        Sifra             int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ZaposleniSkolovanje PRIMARY KEY,
        Zaposleni         int           NOT NULL,
        StepenObrazovanja int           NOT NULL CONSTRAINT FK_ZaposleniSkolovanje_Stepen REFERENCES dbo.StepenObrazovanja (Sifra),
        NazivSkole        nvarchar(100) NOT NULL,
        Mesto             nvarchar(35)  NULL,
        SmerZanimanje     nvarchar(100) NULL,
        DatumZavrsetka    date          NULL,
        BrojDiplome       nvarchar(30)  NULL,
        Napomena          nvarchar(500) NULL,
        KreiraoKorisnik nvarchar(20) NULL, DatumKreiranja datetime2 NOT NULL CONSTRAINT DF_ZaposleniSkolovanje_DK DEFAULT sysdatetime(),
        IzmenioKorisnik nvarchar(20) NULL, DatumIzmene datetime2 NULL, RowVer rowversion);

IF OBJECT_ID('dbo.ZaposleniDokumenti', 'U') IS NULL
    CREATE TABLE dbo.ZaposleniDokumenti (
        Sifra          int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ZaposleniDokumenti PRIMARY KEY,
        Zaposleni      int           NOT NULL,
        VrstaDokumenta int           NOT NULL CONSTRAINT FK_ZaposleniDokumenti_Vrsta REFERENCES dbo.VrsteDokumenta (Sifra),
        BrojDokumenta  nvarchar(30)  NULL,
        DatumIzdavanja date          NULL,
        VaziDo         date          NULL,
        IzdaoOrgan     nvarchar(100) NULL,
        Kategorije     nvarchar(50)  NULL,
        Napomena       nvarchar(500) NULL,
        KreiraoKorisnik nvarchar(20) NULL, DatumKreiranja datetime2 NOT NULL CONSTRAINT DF_ZaposleniDokumenti_DK DEFAULT sysdatetime(),
        IzmenioKorisnik nvarchar(20) NULL, DatumIzmene datetime2 NULL, RowVer rowversion);

IF OBJECT_ID('dbo.ZaposleniZdravstveniPregledi', 'U') IS NULL
    CREATE TABLE dbo.ZaposleniZdravstveniPregledi (
        Sifra            int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ZaposleniZdravstveniPregledi PRIMARY KEY,
        Zaposleni        int           NOT NULL,
        VrstaPregleda    int           NOT NULL CONSTRAINT FK_ZaposleniPregledi_Vrsta REFERENCES dbo.VrstePregleda (Sifra),
        DatumPregleda    date          NOT NULL,
        Ustanova         nvarchar(100) NULL,
        BrojIzvestaja    nvarchar(30)  NULL,
        OcenaSposobnosti int           NOT NULL CONSTRAINT FK_ZaposleniPregledi_Ocena REFERENCES dbo.OceneSposobnosti (Sifra),
        Ogranicenja      nvarchar(250) NULL,
        SledeciPregled   date          NULL,
        Napomena         nvarchar(500) NULL,
        KreiraoKorisnik nvarchar(20) NULL, DatumKreiranja datetime2 NOT NULL CONSTRAINT DF_ZaposleniPregledi_DK DEFAULT sysdatetime(),
        IzmenioKorisnik nvarchar(20) NULL, DatumIzmene datetime2 NULL, RowVer rowversion);

IF OBJECT_ID('dbo.HTZReversi', 'U') IS NULL
    CREATE TABLE dbo.HTZReversi (
        Sifra       int IDENTITY(1,1) NOT NULL CONSTRAINT PK_HTZReversi PRIMARY KEY,
        BrojReversa nvarchar(20)  NOT NULL CONSTRAINT UX_HTZReversi_Broj UNIQUE,
        Zaposleni   int           NOT NULL,
        Datum       date          NOT NULL,
        Izdao       int           NULL,
        Zakljucen   bit           NOT NULL CONSTRAINT DF_HTZReversi_Zakljucen DEFAULT 0,
        Storniran   bit           NOT NULL CONSTRAINT DF_HTZReversi_Storniran DEFAULT 0,
        Napomena    nvarchar(500) NULL,
        KreiraoKorisnik nvarchar(20) NULL, DatumKreiranja datetime2 NOT NULL CONSTRAINT DF_HTZReversi_DK DEFAULT sysdatetime(),
        IzmenioKorisnik nvarchar(20) NULL, DatumIzmene datetime2 NULL, RowVer rowversion);

IF OBJECT_ID('dbo.ZaposleniHTZZaduzenja', 'U') IS NULL
    CREATE TABLE dbo.ZaposleniHTZZaduzenja (
        Sifra            int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ZaposleniHTZZaduzenja PRIMARY KEY,
        Revers           int           NOT NULL CONSTRAINT FK_ZaposleniHTZZaduzenja_Revers REFERENCES dbo.HTZReversi (Sifra),
        Zaposleni        int           NOT NULL,
        HTZOprema        int           NOT NULL CONSTRAINT FK_ZaposleniHTZZaduzenja_Oprema REFERENCES dbo.HTZOprema (Sifra),
        Kolicina         decimal(9,2)  NOT NULL CONSTRAINT CK_ZaposleniHTZZaduzenja_Kolicina CHECK (Kolicina > 0),
        Velicina         nvarchar(10)  NULL,
        DatumZaduzenja   date          NOT NULL,
        RokZamene        date          NULL,
        DatumRazduzenja  date          NULL,
        RazlogRazduzenja int           NULL CONSTRAINT FK_ZaposleniHTZZaduzenja_Razlog REFERENCES dbo.RazloziRazduzenja (Sifra),
        Napomena         nvarchar(500) NULL);

IF OBJECT_ID('dbo.ZaposleniHitniKontakti', 'U') IS NULL
    CREATE TABLE dbo.ZaposleniHitniKontakti (
        Sifra     int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ZaposleniHitniKontakti PRIMARY KEY,
        Zaposleni int           NOT NULL,
        ImePrezime nvarchar(70) NOT NULL,
        Srodstvo  int           NULL CONSTRAINT FK_ZaposleniHitniKontakti_Srodstvo REFERENCES dbo.Srodstva (Sifra),
        Telefon1  nvarchar(20)  NOT NULL,
        Telefon2  nvarchar(20)  NULL,
        Prioritet tinyint       NOT NULL CONSTRAINT DF_ZaposleniHitniKontakti_Prioritet DEFAULT 1,
        Napomena  nvarchar(500) NULL);

IF OBJECT_ID('dbo.ZaposleniPovrede', 'U') IS NULL
    CREATE TABLE dbo.ZaposleniPovrede (
        Sifra         int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ZaposleniPovrede PRIMARY KEY,
        Zaposleni     int            NOT NULL,
        DatumVreme    datetime2      NOT NULL,
        SluzbenoMesto int            NULL,      -- Stanice.ID
        MestoOpis     nvarchar(100)  NULL,
        Opis          nvarchar(1000) NOT NULL,
        TezinaPovrede int            NOT NULL CONSTRAINT FK_ZaposleniPovrede_Tezina REFERENCES dbo.TezinePovreda (Sifra),
        BrojIzvestaja nvarchar(30)   NULL,
        DatumPrijave  date           NULL,
        DanaOdsustva  int            NULL,
        Napomena      nvarchar(500)  NULL,
        KreiraoKorisnik nvarchar(20) NULL, DatumKreiranja datetime2 NOT NULL CONSTRAINT DF_ZaposleniPovrede_DK DEFAULT sysdatetime(),
        IzmenioKorisnik nvarchar(20) NULL, DatumIzmene datetime2 NULL, RowVer rowversion);

IF OBJECT_ID('dbo.ZaposleniDisciplinske', 'U') IS NULL
    CREATE TABLE dbo.ZaposleniDisciplinske (
        Sifra       int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ZaposleniDisciplinske PRIMARY KEY,
        Zaposleni   int           NOT NULL,
        Datum       date          NOT NULL,
        VrstaMere   int           NOT NULL CONSTRAINT FK_ZaposleniDisciplinske_Vrsta REFERENCES dbo.VrsteDisciplinskihMera (Sifra),
        Razlog      nvarchar(500) NOT NULL,
        BrojResenja nvarchar(30)  NULL,
        VaziDo      date          NULL,
        Napomena    nvarchar(500) NULL,
        KreiraoKorisnik nvarchar(20) NULL, DatumKreiranja datetime2 NOT NULL CONSTRAINT DF_ZaposleniDisciplinske_DK DEFAULT sysdatetime(),
        IzmenioKorisnik nvarchar(20) NULL, DatumIzmene datetime2 NULL, RowVer rowversion);

IF OBJECT_ID('dbo.ZaposleniOcene', 'U') IS NULL
    CREATE TABLE dbo.ZaposleniOcene (
        Sifra     int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ZaposleniOcene PRIMARY KEY,
        Zaposleni int            NOT NULL,
        DatumOd   date           NOT NULL,
        DatumDo   date           NOT NULL,
        Ocenjivac int            NOT NULL,
        Ocena     tinyint        NOT NULL CONSTRAINT CK_ZaposleniOcene_Ocena CHECK (Ocena BETWEEN 1 AND 5),
        Komentar  nvarchar(2000) NULL,
        KreiraoKorisnik nvarchar(20) NULL, DatumKreiranja datetime2 NOT NULL CONSTRAINT DF_ZaposleniOcene_DK DEFAULT sysdatetime(),
        IzmenioKorisnik nvarchar(20) NULL, DatumIzmene datetime2 NULL, RowVer rowversion,
        CONSTRAINT CK_ZaposleniOcene_Period CHECK (DatumDo >= DatumOd));

IF OBJECT_ID('dbo.ZaposleniClanoviPorodice', 'U') IS NULL
    CREATE TABLE dbo.ZaposleniClanoviPorodice (
        Sifra         int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ZaposleniClanoviPorodice PRIMARY KEY,
        Zaposleni     int           NOT NULL,
        Ime           nvarchar(35)  NOT NULL,
        Prezime       nvarchar(35)  NOT NULL,
        Srodstvo      int           NOT NULL CONSTRAINT FK_ZaposleniClanoviPorodice_Srodstvo REFERENCES dbo.Srodstva (Sifra),
        DatumRodjenja date          NULL,
        JMBG          char(13)      NULL,
        Napomena      nvarchar(500) NULL);

IF OBJECT_ID('dbo.ZaposleniPrilozi', 'U') IS NULL
    CREATE TABLE dbo.ZaposleniPrilozi (
        Sifra      int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ZaposleniPrilozi PRIMARY KEY,
        Zaposleni  int            NOT NULL,
        Naziv      nvarchar(100)  NOT NULL,
        NazivFajla nvarchar(255)  NOT NULL,
        TipFajla   nvarchar(100)  NULL,
        Velicina   int            NULL,
        Sadrzaj    varbinary(max) NOT NULL,
        VezaTip    nvarchar(20)   NULL,
        VezaSifra  int            NULL,
        Datum      date           NOT NULL CONSTRAINT DF_ZaposleniPrilozi_Datum DEFAULT CAST(sysdatetime() AS date),
        KreiraoKorisnik nvarchar(20) NULL, DatumKreiranja datetime2 NOT NULL CONSTRAINT DF_ZaposleniPrilozi_DK DEFAULT sysdatetime(),
        IzmenioKorisnik nvarchar(20) NULL, DatumIzmene datetime2 NULL, RowVer rowversion);

-- zaštita podataka (7.5): svaki pristup osetljivim karticama se beleži
IF OBJECT_ID('dbo.ZaposleniPristupLog', 'U') IS NULL
    CREATE TABLE dbo.ZaposleniPristupLog (
        Sifra      int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ZaposleniPristupLog PRIMARY KEY,
        Zaposleni  int          NOT NULL,
        Kartica    nvarchar(50) NOT NULL,
        Korisnik   nvarchar(20) NOT NULL,
        DatumVreme datetime2    NOT NULL CONSTRAINT DF_ZaposleniPristupLog_DV DEFAULT sysdatetime());

-- opcione tabele (poglavlje 6)
IF OBJECT_ID('dbo.RadnoMestoObavezniDokumenti', 'U') IS NULL
    CREATE TABLE dbo.RadnoMestoObavezniDokumenti (
        Sifra          int IDENTITY(1,1) NOT NULL CONSTRAINT PK_RadnoMestoObavezniDokumenti PRIMARY KEY,
        RadnoMesto     int NOT NULL CONSTRAINT FK_RMObavezniDokumenti_RM REFERENCES dbo.RadnaMesta (Sifra),
        VrstaDokumenta int NOT NULL CONSTRAINT FK_RMObavezniDokumenti_VD REFERENCES dbo.VrsteDokumenta (Sifra),
        CONSTRAINT UX_RadnoMestoObavezniDokumenti UNIQUE (RadnoMesto, VrstaDokumenta));
IF OBJECT_ID('dbo.RadnoMestoObavezniPregledi', 'U') IS NULL
    CREATE TABLE dbo.RadnoMestoObavezniPregledi (
        Sifra         int IDENTITY(1,1) NOT NULL CONSTRAINT PK_RadnoMestoObavezniPregledi PRIMARY KEY,
        RadnoMesto    int NOT NULL CONSTRAINT FK_RMObavezniPregledi_RM REFERENCES dbo.RadnaMesta (Sifra),
        VrstaPregleda int NOT NULL CONSTRAINT FK_RMObavezniPregledi_VP REFERENCES dbo.VrstePregleda (Sifra),
        CONSTRAINT UX_RadnoMestoObavezniPregledi UNIQUE (RadnoMesto, VrstaPregleda));
IF OBJECT_ID('dbo.RadnoMestoNormativHTZ', 'U') IS NULL
    CREATE TABLE dbo.RadnoMestoNormativHTZ (
        Sifra      int IDENTITY(1,1) NOT NULL CONSTRAINT PK_RadnoMestoNormativHTZ PRIMARY KEY,
        RadnoMesto int          NOT NULL CONSTRAINT FK_RMNormativHTZ_RM REFERENCES dbo.RadnaMesta (Sifra),
        HTZOprema  int          NOT NULL CONSTRAINT FK_RMNormativHTZ_HTZ REFERENCES dbo.HTZOprema (Sifra),
        Kolicina   decimal(9,2) NOT NULL CONSTRAINT DF_RMNormativHTZ_Kolicina DEFAULT 1,
        CONSTRAINT UX_RadnoMestoNormativHTZ UNIQUE (RadnoMesto, HTZOprema));
GO

-- najviše jedan otvoren zapis po zaposlenom
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UX_ZaposleniRadnaMesta_Otvoren')
    CREATE UNIQUE INDEX UX_ZaposleniRadnaMesta_Otvoren ON dbo.ZaposleniRadnaMesta (Zaposleni) WHERE DatumDo IS NULL;
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UX_ZaposleniMentori_Otvoren')
    CREATE UNIQUE INDEX UX_ZaposleniMentori_Otvoren ON dbo.ZaposleniMentori (Zaposleni) WHERE DatumDo IS NULL;
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_ZaposleniDokumenti_Zaposleni')
    CREATE INDEX IX_ZaposleniDokumenti_Zaposleni ON dbo.ZaposleniDokumenti (Zaposleni, VaziDo);
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_ZaposleniHTZZaduzenja_Zaposleni')
    CREATE INDEX IX_ZaposleniHTZZaduzenja_Zaposleni ON dbo.ZaposleniHTZZaduzenja (Zaposleni, DatumRazduzenja);
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_ZaposleniPrilozi_Zaposleni')
    CREATE INDEX IX_ZaposleniPrilozi_Zaposleni ON dbo.ZaposleniPrilozi (Zaposleni);
GO

-- strani ključevi ka postojećim tabelama Zaposleni i OrganizacioneJedinice
EXEC #HR_DodajFK N'FK_ZaposleniSlike_Zaposleni',        N'ZaposleniSlike',               N'Zaposleni', N'Zaposleni', N'ID';
EXEC #HR_DodajFK N'FK_ZaposleniRadnaMesta_Zaposleni',   N'ZaposleniRadnaMesta',          N'Zaposleni', N'Zaposleni', N'ID';
EXEC #HR_DodajFK N'FK_ZaposleniRadnaMesta_OrgJed',      N'ZaposleniRadnaMesta',          N'OrganizacionaJedinica', N'OrganizacioneJedinice', N'ID';
EXEC #HR_DodajFK N'FK_ZaposleniUgovori_Zaposleni',      N'ZaposleniUgovori',             N'Zaposleni', N'Zaposleni', N'ID';
EXEC #HR_DodajFK N'FK_ZaposleniMentori_Zaposleni',      N'ZaposleniMentori',             N'Zaposleni', N'Zaposleni', N'ID';
EXEC #HR_DodajFK N'FK_ZaposleniMentori_Mentor',         N'ZaposleniMentori',             N'Mentor',    N'Zaposleni', N'ID';
EXEC #HR_DodajFK N'FK_ZaposleniSkolovanje_Zaposleni',   N'ZaposleniSkolovanje',          N'Zaposleni', N'Zaposleni', N'ID';
EXEC #HR_DodajFK N'FK_ZaposleniDokumenti_Zaposleni',    N'ZaposleniDokumenti',           N'Zaposleni', N'Zaposleni', N'ID';
EXEC #HR_DodajFK N'FK_ZaposleniPregledi_Zaposleni',     N'ZaposleniZdravstveniPregledi', N'Zaposleni', N'Zaposleni', N'ID';
EXEC #HR_DodajFK N'FK_HTZReversi_Zaposleni',            N'HTZReversi',                   N'Zaposleni', N'Zaposleni', N'ID';
EXEC #HR_DodajFK N'FK_HTZReversi_Izdao',                N'HTZReversi',                   N'Izdao',     N'Zaposleni', N'ID';
EXEC #HR_DodajFK N'FK_ZaposleniHTZZaduzenja_Zaposleni', N'ZaposleniHTZZaduzenja',        N'Zaposleni', N'Zaposleni', N'ID';
EXEC #HR_DodajFK N'FK_ZaposleniHitniKontakti_Zaposleni',N'ZaposleniHitniKontakti',       N'Zaposleni', N'Zaposleni', N'ID';
EXEC #HR_DodajFK N'FK_ZaposleniPovrede_Zaposleni',      N'ZaposleniPovrede',             N'Zaposleni', N'Zaposleni', N'ID';
EXEC #HR_DodajFK N'FK_ZaposleniDisciplinske_Zaposleni', N'ZaposleniDisciplinske',        N'Zaposleni', N'Zaposleni', N'ID';
EXEC #HR_DodajFK N'FK_ZaposleniOcene_Zaposleni',        N'ZaposleniOcene',               N'Zaposleni', N'Zaposleni', N'ID';
EXEC #HR_DodajFK N'FK_ZaposleniOcene_Ocenjivac',        N'ZaposleniOcene',               N'Ocenjivac', N'Zaposleni', N'ID';
EXEC #HR_DodajFK N'FK_ZaposleniClanoviPorodice_Zaposleni', N'ZaposleniClanoviPorodice',  N'Zaposleni', N'Zaposleni', N'ID';
EXEC #HR_DodajFK N'FK_ZaposleniPrilozi_Zaposleni',      N'ZaposleniPrilozi',             N'Zaposleni', N'Zaposleni', N'ID';
GO

/* ---------------------------------------------------------------------------------------------
   4. Zahtev za nabavku: šifarnici i tabele
   --------------------------------------------------------------------------------------------- */
IF OBJECT_ID('dbo.VrsteNabavke', 'U') IS NULL
    CREATE TABLE dbo.VrsteNabavke (
        Sifra           int IDENTITY(1,1) NOT NULL CONSTRAINT PK_VrsteNabavke PRIMARY KEY,
        Naziv           nvarchar(100) NOT NULL,
        ZahtevaSredstvo bit           NOT NULL CONSTRAINT DF_VrsteNabavke_ZahtevaSredstvo DEFAULT 0,
        Aktivan         bit           NOT NULL CONSTRAINT DF_VrsteNabavke_Aktivan DEFAULT 1);
IF NOT EXISTS (SELECT 1 FROM dbo.VrsteNabavke)
    INSERT dbo.VrsteNabavke (Naziv, ZahtevaSredstvo) VALUES
        (N'Rezervni delovi', 1), (N'HTZ oprema', 0), (N'Usluge servisa', 1), (N'Gorivo i maziva', 0),
        (N'Kancelarijski materijal', 0), (N'Osnovna sredstva', 0), (N'Ostalo', 0);

IF OBJECT_ID('dbo.PrioritetiNabavke', 'U') IS NULL
    CREATE TABLE dbo.PrioritetiNabavke (
        Sifra             int IDENTITY(1,1) NOT NULL CONSTRAINT PK_PrioritetiNabavke PRIMARY KEY,
        Naziv             nvarchar(100) NOT NULL,
        DanaZaRealizaciju int           NULL,
        Aktivan           bit           NOT NULL CONSTRAINT DF_PrioritetiNabavke_Aktivan DEFAULT 1);
IF NOT EXISTS (SELECT 1 FROM dbo.PrioritetiNabavke)
    INSERT dbo.PrioritetiNabavke (Naziv, DanaZaRealizaciju) VALUES (N'Hitno', 3), (N'Redovno', 15), (N'Planirano', 45);

-- statusi imaju stalne šifre (kod ih koristi), zato nisu IDENTITY
IF OBJECT_ID('dbo.StatusiZahtevaZaNabavku', 'U') IS NULL
    CREATE TABLE dbo.StatusiZahtevaZaNabavku (
        Sifra   int           NOT NULL CONSTRAINT PK_StatusiZahtevaZaNabavku PRIMARY KEY,
        Naziv   nvarchar(100) NOT NULL,
        Zavrsni bit           NOT NULL CONSTRAINT DF_StatusiZahteva_Zavrsni DEFAULT 0,
        Aktivan bit           NOT NULL CONSTRAINT DF_StatusiZahteva_Aktivan DEFAULT 1);
IF NOT EXISTS (SELECT 1 FROM dbo.StatusiZahtevaZaNabavku)
    INSERT dbo.StatusiZahtevaZaNabavku (Sifra, Naziv, Zavrsni) VALUES
        (1, N'Nacrt', 0), (2, N'Podnet', 0), (3, N'Na odobrenju', 0), (4, N'Vraćen na doradu', 0), (5, N'Odobren', 0),
        (6, N'U nabavci', 0), (7, N'Delimično realizovan', 0), (8, N'Realizovan', 1), (9, N'Odbijen', 1), (10, N'Storniran', 1);

IF OBJECT_ID('dbo.StatusiStavkiNabavke', 'U') IS NULL
    CREATE TABLE dbo.StatusiStavkiNabavke (
        Sifra   int           NOT NULL CONSTRAINT PK_StatusiStavkiNabavke PRIMARY KEY,
        Naziv   nvarchar(100) NOT NULL,
        Aktivan bit           NOT NULL CONSTRAINT DF_StatusiStavki_Aktivan DEFAULT 1);
IF NOT EXISTS (SELECT 1 FROM dbo.StatusiStavkiNabavke)
    INSERT dbo.StatusiStavkiNabavke (Sifra, Naziv) VALUES
        (1, N'Otvorena'), (2, N'Naručena'), (3, N'Delimično primljena'), (4, N'Primljena'), (5, N'Odbijena');

IF OBJECT_ID('dbo.MestaTroska', 'U') IS NULL
    CREATE TABLE dbo.MestaTroska (
        Sifra    int IDENTITY(1,1) NOT NULL CONSTRAINT PK_MestaTroska PRIMARY KEY,
        SifraERP nvarchar(20)  NULL,
        Naziv    nvarchar(100) NOT NULL,
        Aktivan  bit           NOT NULL CONSTRAINT DF_MestaTroska_Aktivan DEFAULT 1);
GO

IF OBJECT_ID('dbo.ZahteviZaNabavku', 'U') IS NULL
    CREATE TABLE dbo.ZahteviZaNabavku (
        Sifra                   int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ZahteviZaNabavku PRIMARY KEY,
        BrojZahteva             nvarchar(20)   NOT NULL CONSTRAINT UX_ZahteviZaNabavku_Broj UNIQUE,
        Datum                   date           NOT NULL,
        Podnosilac              int            NOT NULL,
        OrganizacionaJedinica   int            NOT NULL,
        MestoTroska             int            NULL CONSTRAINT FK_ZahteviZaNabavku_MestoTroska REFERENCES dbo.MestaTroska (Sifra),
        VrstaNabavke            int            NOT NULL CONSTRAINT FK_ZahteviZaNabavku_Vrsta REFERENCES dbo.VrsteNabavke (Sifra),
        Prioritet               int            NOT NULL CONSTRAINT FK_ZahteviZaNabavku_Prioritet REFERENCES dbo.PrioritetiNabavke (Sifra),
        PotrebnoDo              date           NULL,
        Svrha                   nvarchar(500)  NOT NULL,
        Sredstvo                int            NULL,     -- Sredstva (sistem evidencije sredstava), za sada bez FK
        SredstvoOpis            nvarchar(100)  NULL,
        RadniNalog              int            NULL,     -- RadniNalozi, za sada bez FK
        RadniNalogOpis          nvarchar(30)   NULL,
        MestoIsporuke           nvarchar(100)  NULL,
        SluzbenoMesto           int            NULL,     -- Stanice.ID
        PredlozeniDobavljac     int            NULL,     -- Partnerji.PaSifra
        ProcenjenaVrednost      decimal(18,2)  NOT NULL CONSTRAINT DF_ZahteviZaNabavku_Procenjena DEFAULT 0,
        OdobreniIznos           decimal(18,2)  NULL,
        Valuta                  char(3)        NOT NULL CONSTRAINT DF_ZahteviZaNabavku_Valuta DEFAULT 'RSD',
        Status                  int            NOT NULL CONSTRAINT FK_ZahteviZaNabavku_Status REFERENCES dbo.StatusiZahtevaZaNabavku (Sifra),
        TrenutniNivoOdobravanja tinyint        NULL,
        Napomena                nvarchar(1000) NULL,
        KreiraoKorisnik nvarchar(20) NULL, DatumKreiranja datetime2 NOT NULL CONSTRAINT DF_ZahteviZaNabavku_DK DEFAULT sysdatetime(),
        IzmenioKorisnik nvarchar(20) NULL, DatumIzmene datetime2 NULL, RowVer rowversion);

IF OBJECT_ID('dbo.ZahtevZaNabavkuStavke', 'U') IS NULL
    CREATE TABLE dbo.ZahtevZaNabavkuStavke (
        Sifra              int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ZahtevZaNabavkuStavke PRIMARY KEY,
        Zahtev             int            NOT NULL CONSTRAINT FK_ZahtevStavke_Zahtev REFERENCES dbo.ZahteviZaNabavku (Sifra) ON DELETE CASCADE,
        RedniBroj          smallint       NOT NULL,
        Artikal            int            NULL,     -- Artikli (magacin, faza 2)
        HTZOprema          int            NULL CONSTRAINT FK_ZahtevStavke_HTZ REFERENCES dbo.HTZOprema (Sifra),
        Opis               nvarchar(250)  NOT NULL,
        JedinicaMere       nvarchar(10)   NOT NULL,
        Kolicina           decimal(18,3)  NOT NULL CONSTRAINT CK_ZahtevStavke_Kolicina CHECK (Kolicina > 0),
        ProcenjenaCena     decimal(18,2)  NULL,
        ProcenjenaVrednost AS CAST(Kolicina * ISNULL(ProcenjenaCena, 0) AS decimal(18,2)) PERSISTED,
        Sredstvo           int            NULL,
        StatusStavke       int            NOT NULL CONSTRAINT FK_ZahtevStavke_Status REFERENCES dbo.StatusiStavkiNabavke (Sifra)
                                                   CONSTRAINT DF_ZahtevStavke_Status DEFAULT 1,
        PrimljenaKolicina  decimal(18,3)  NOT NULL CONSTRAINT DF_ZahtevStavke_Primljena DEFAULT 0,
        Napomena           nvarchar(500)  NULL,
        CONSTRAINT UX_ZahtevZaNabavkuStavke_RedniBroj UNIQUE (Zahtev, RedniBroj));

IF OBJECT_ID('dbo.ZahtevZaNabavkuOdobrenja', 'U') IS NULL
    CREATE TABLE dbo.ZahtevZaNabavkuOdobrenja (
        Sifra          int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ZahtevZaNabavkuOdobrenja PRIMARY KEY,
        Zahtev         int            NOT NULL CONSTRAINT FK_ZahtevOdobrenja_Zahtev REFERENCES dbo.ZahteviZaNabavku (Sifra) ON DELETE CASCADE,
        Nivo           tinyint        NOT NULL,
        Odobravalac    int            NOT NULL,
        Odluka         char(1)        NULL CONSTRAINT CK_ZahtevOdobrenja_Odluka CHECK (Odluka IN ('O', 'V', 'D')),
        DatumZadavanja datetime2      NOT NULL CONSTRAINT DF_ZahtevOdobrenja_DZ DEFAULT sysdatetime(),
        DatumOdluke    datetime2      NULL,
        Komentar       nvarchar(1000) NULL);

IF OBJECT_ID('dbo.PravilaOdobravanjaNabavke', 'U') IS NULL
    CREATE TABLE dbo.PravilaOdobravanjaNabavke (
        Sifra                 int IDENTITY(1,1) NOT NULL CONSTRAINT PK_PravilaOdobravanjaNabavke PRIMARY KEY,
        VrstaNabavke          int           NULL CONSTRAINT FK_PravilaOdobravanja_Vrsta REFERENCES dbo.VrsteNabavke (Sifra),
        OrganizacionaJedinica int           NULL,
        IznosOd               decimal(18,2) NOT NULL CONSTRAINT DF_PravilaOdobravanja_IznosOd DEFAULT 0,
        IznosDo               decimal(18,2) NULL,
        Nivo                  tinyint       NOT NULL,
        RadnoMestoOdobravaoca int           NULL CONSTRAINT FK_PravilaOdobravanja_RadnoMesto REFERENCES dbo.RadnaMesta (Sifra),
        Odobravalac           int           NULL,
        Zamenik               int           NULL,
        Aktivan               bit           NOT NULL CONSTRAINT DF_PravilaOdobravanja_Aktivan DEFAULT 1,
        CONSTRAINT CK_PravilaOdobravanja_Odobravalac CHECK (RadnoMestoOdobravaoca IS NOT NULL OR Odobravalac IS NOT NULL),
        CONSTRAINT CK_PravilaOdobravanja_Iznos CHECK (IznosDo IS NULL OR IznosDo >= IznosOd));

IF OBJECT_ID('dbo.ZahtevZaNabavkuPonude', 'U') IS NULL
    CREATE TABLE dbo.ZahtevZaNabavkuPonude (
        Sifra           int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ZahtevZaNabavkuPonude PRIMARY KEY,
        Zahtev          int           NOT NULL CONSTRAINT FK_ZahtevPonude_Zahtev REFERENCES dbo.ZahteviZaNabavku (Sifra) ON DELETE CASCADE,
        Partner         int           NOT NULL,      -- Partnerji.PaSifra
        BrojPonude      nvarchar(30)  NULL,
        DatumPonude     date          NOT NULL,
        VaziDo          date          NULL,
        Iznos           decimal(18,2) NOT NULL,
        Valuta          char(3)       NOT NULL CONSTRAINT DF_ZahtevPonude_Valuta DEFAULT 'RSD',
        RokIsporukeDana int           NULL,
        UsloviPlacanja  nvarchar(100) NULL,
        Izabrana        bit           NOT NULL CONSTRAINT DF_ZahtevPonude_Izabrana DEFAULT 0,
        RazlogIzbora    nvarchar(500) NULL,
        Napomena        nvarchar(500) NULL,
        KreiraoKorisnik nvarchar(20) NULL, DatumKreiranja datetime2 NOT NULL CONSTRAINT DF_ZahtevPonude_DK DEFAULT sysdatetime(),
        IzmenioKorisnik nvarchar(20) NULL, DatumIzmene datetime2 NULL, RowVer rowversion);

IF OBJECT_ID('dbo.ZahtevZaNabavkuIstorijaStatusa', 'U') IS NULL
    CREATE TABLE dbo.ZahtevZaNabavkuIstorijaStatusa (
        Sifra      int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ZahtevZaNabavkuIstorijaStatusa PRIMARY KEY,
        Zahtev     int           NOT NULL CONSTRAINT FK_ZahtevIstorija_Zahtev REFERENCES dbo.ZahteviZaNabavku (Sifra) ON DELETE CASCADE,
        StatusOd   int           NULL,
        StatusDo   int           NOT NULL,
        DatumVreme datetime2     NOT NULL CONSTRAINT DF_ZahtevIstorija_DV DEFAULT sysdatetime(),
        Korisnik   nvarchar(20)  NOT NULL,
        Komentar   nvarchar(500) NULL);

IF OBJECT_ID('dbo.ZahtevZaNabavkuPrilozi', 'U') IS NULL
    CREATE TABLE dbo.ZahtevZaNabavkuPrilozi (
        Sifra      int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ZahtevZaNabavkuPrilozi PRIMARY KEY,
        Zahtev     int            NOT NULL CONSTRAINT FK_ZahtevPrilozi_Zahtev REFERENCES dbo.ZahteviZaNabavku (Sifra) ON DELETE CASCADE,
        Ponuda     int            NULL CONSTRAINT FK_ZahtevPrilozi_Ponuda REFERENCES dbo.ZahtevZaNabavkuPonude (Sifra),
        Naziv      nvarchar(100)  NOT NULL,
        NazivFajla nvarchar(255)  NOT NULL,
        TipFajla   nvarchar(100)  NULL,
        Sadrzaj    varbinary(max) NOT NULL,
        KreiraoKorisnik nvarchar(20) NULL, DatumKreiranja datetime2 NOT NULL CONSTRAINT DF_ZahtevPrilozi_DK DEFAULT sysdatetime(),
        IzmenioKorisnik nvarchar(20) NULL, DatumIzmene datetime2 NULL, RowVer rowversion);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UX_ZahtevZaNabavkuPonude_Izabrana')
    CREATE UNIQUE INDEX UX_ZahtevZaNabavkuPonude_Izabrana ON dbo.ZahtevZaNabavkuPonude (Zahtev) WHERE Izabrana = 1;
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_ZahtevZaNabavkuOdobrenja_Ceka')
    CREATE INDEX IX_ZahtevZaNabavkuOdobrenja_Ceka ON dbo.ZahtevZaNabavkuOdobrenja (Odobravalac) WHERE Odluka IS NULL;
GO

EXEC #HR_DodajFK N'FK_ZahteviZaNabavku_Podnosilac', N'ZahteviZaNabavku',          N'Podnosilac',            N'Zaposleni', N'ID';
EXEC #HR_DodajFK N'FK_ZahteviZaNabavku_OrgJed',     N'ZahteviZaNabavku',          N'OrganizacionaJedinica', N'OrganizacioneJedinice', N'ID';
EXEC #HR_DodajFK N'FK_ZahtevOdobrenja_Odobravalac', N'ZahtevZaNabavkuOdobrenja',  N'Odobravalac',           N'Zaposleni', N'ID';
EXEC #HR_DodajFK N'FK_PravilaOdobravanja_OrgJed',   N'PravilaOdobravanjaNabavke', N'OrganizacionaJedinica', N'OrganizacioneJedinice', N'ID';
EXEC #HR_DodajFK N'FK_PravilaOdobravanja_Odobravalac', N'PravilaOdobravanjaNabavke', N'Odobravalac',        N'Zaposleni', N'ID';
EXEC #HR_DodajFK N'FK_PravilaOdobravanja_Zamenik',  N'PravilaOdobravanjaNabavke', N'Zamenik',               N'Zaposleni', N'ID';
GO

/* ---------------------------------------------------------------------------------------------
   5. Pogledi
   --------------------------------------------------------------------------------------------- */

-- Trenutno stanje zaposlenog: radno mesto, jedinica, matično službeno mesto, mentor, najviši stepen obrazovanja, staž
CREATE OR ALTER VIEW dbo.vZaposleniTrenutno
AS
SELECT z.ID AS Zaposleni,
       z.Code,
       z.Prezime, z.Ime,
       LTRIM(RTRIM(ISNULL(z.Prezime, N'') + N' ' + ISNULL(z.Ime, N''))) AS PrezimeIme,
       z.Status,
       zs.Naziv AS StatusNaziv,
       z.JMBG, z.SifraERP, z.DatumUlaska, z.DatumIzlaska,
       rm.Sifra AS ZaposleniRadnoMesto,
       rm.RadnoMesto,
       r.Naziv AS RadnoMestoNaziv,
       ISNULL(rm.OrganizacionaJedinica, z.OrganizacionaJedinica) AS OrganizacionaJedinica,
       oj.Naziv AS OrganizacionaJedinicaNaziv,
       rm.SluzbenoMesto,
       st.Opis AS SluzbenoMestoNaziv,
       m.Mentor,
       LTRIM(RTRIM(ISNULL(mz.Prezime, N'') + N' ' + ISNULL(mz.Ime, N''))) AS MentorNaziv,
       so.StepenObrazovanja,
       so.StepenObrazovanjaNaziv,
       -- staž u mesecima: prethodni + u firmi do danas (ili do izlaska)
       ISNULL(z.PrethodniStazGodina, 0) * 12 + ISNULL(z.PrethodniStazMeseci, 0)
         + CASE WHEN z.DatumUlaska IS NULL THEN 0
                ELSE DATEDIFF(MONTH, z.DatumUlaska, ISNULL(z.DatumIzlaska, CAST(GETDATE() AS date)))
                     - CASE WHEN DAY(ISNULL(z.DatumIzlaska, CAST(GETDATE() AS date))) < DAY(z.DatumUlaska) THEN 1 ELSE 0 END
           END AS StazMeseci
FROM dbo.Zaposleni z
LEFT JOIN dbo.ZaposleniStatusi zs ON zs.Status = z.Status
LEFT JOIN dbo.ZaposleniRadnaMesta rm ON rm.Zaposleni = z.ID AND rm.DatumDo IS NULL
LEFT JOIN dbo.RadnaMesta r ON r.Sifra = rm.RadnoMesto
LEFT JOIN dbo.OrganizacioneJedinice oj ON oj.ID = ISNULL(rm.OrganizacionaJedinica, z.OrganizacionaJedinica)
LEFT JOIN dbo.Stanice st ON st.ID = rm.SluzbenoMesto
LEFT JOIN dbo.ZaposleniMentori m ON m.Zaposleni = z.ID AND m.DatumDo IS NULL
LEFT JOIN dbo.Zaposleni mz ON mz.ID = m.Mentor
OUTER APPLY (SELECT TOP (1) s.StepenObrazovanja, so.Naziv AS StepenObrazovanjaNaziv
             FROM dbo.ZaposleniSkolovanje s
             JOIN dbo.StepenObrazovanja so ON so.Sifra = s.StepenObrazovanja
             WHERE s.Zaposleni = z.ID
             ORDER BY s.StepenObrazovanja DESC, s.DatumZavrsetka DESC) so;
GO

-- Svi rokovi zaposlenih: dokumenti, zdravstveni pregledi, HTZ za zamenu, ugovori na određeno,
-- obavezni dokumenti radnog mesta koji nedostaju. DanaDo < 0 znači da je rok prošao.
CREATE OR ALTER VIEW dbo.vZaposleniRokovi
AS
SELECT d.Zaposleni, N'Dokument' AS Vrsta, vd.Naziv AS StaIstice, d.VaziDo AS Datum,
       DATEDIFF(DAY, CAST(GETDATE() AS date), d.VaziDo) AS DanaDo,
       ISNULL(vd.DanaUpozorenja, 30) AS DanaUpozorenja,
       N'ZaposleniDokumenti' AS Izvor, d.Sifra AS IzvorSifra
FROM dbo.ZaposleniDokumenti d
JOIN dbo.VrsteDokumenta vd ON vd.Sifra = d.VrstaDokumenta
WHERE d.VaziDo IS NOT NULL AND vd.ImaRokVazenja = 1
  -- samo poslednji dokument iste vrste (stari, zamenjeni dokument ne upozorava)
  AND NOT EXISTS (SELECT 1 FROM dbo.ZaposleniDokumenti n
                  WHERE n.Zaposleni = d.Zaposleni AND n.VrstaDokumenta = d.VrstaDokumenta AND n.VaziDo > d.VaziDo)
UNION ALL
SELECT p.Zaposleni, N'Pregled', vp.Naziv + N' pregled', p.SledeciPregled,
       DATEDIFF(DAY, CAST(GETDATE() AS date), p.SledeciPregled), 30,
       N'ZaposleniZdravstveniPregledi', p.Sifra
FROM dbo.ZaposleniZdravstveniPregledi p
JOIN dbo.VrstePregleda vp ON vp.Sifra = p.VrstaPregleda
WHERE p.SledeciPregled IS NOT NULL
  AND NOT EXISTS (SELECT 1 FROM dbo.ZaposleniZdravstveniPregledi n
                  WHERE n.Zaposleni = p.Zaposleni AND n.DatumPregleda > p.DatumPregleda)
UNION ALL
SELECT h.Zaposleni, N'HTZ', N'HTZ: ' + o.Naziv, h.RokZamene,
       DATEDIFF(DAY, CAST(GETDATE() AS date), h.RokZamene), 30,
       N'ZaposleniHTZZaduzenja', h.Sifra
FROM dbo.ZaposleniHTZZaduzenja h
JOIN dbo.HTZOprema o ON o.Sifra = h.HTZOprema
JOIN dbo.HTZReversi rv ON rv.Sifra = h.Revers AND rv.Storniran = 0
WHERE h.RokZamene IS NOT NULL AND h.DatumRazduzenja IS NULL
UNION ALL
SELECT u.Zaposleni, N'Ugovor', vu.Naziv + N' (na određeno)', u.VaziDo,
       DATEDIFF(DAY, CAST(GETDATE() AS date), u.VaziDo), 30,
       N'ZaposleniUgovori', u.Sifra
FROM dbo.ZaposleniUgovori u
JOIN dbo.VrsteUgovora vu ON vu.Sifra = u.VrstaUgovora
WHERE u.VaziDo IS NOT NULL
  AND NOT EXISTS (SELECT 1 FROM dbo.ZaposleniUgovori n WHERE n.Zaposleni = u.Zaposleni AND n.VaziOd > u.VaziOd)
UNION ALL
SELECT rm.Zaposleni, N'Nedostaje', N'Nedostaje: ' + vd.Naziv, CAST(NULL AS date), -1, 0,
       N'RadnoMestoObavezniDokumenti', o.Sifra
FROM dbo.ZaposleniRadnaMesta rm
JOIN dbo.RadnoMestoObavezniDokumenti o ON o.RadnoMesto = rm.RadnoMesto
JOIN dbo.VrsteDokumenta vd ON vd.Sifra = o.VrstaDokumenta
WHERE rm.DatumDo IS NULL
  AND NOT EXISTS (SELECT 1 FROM dbo.ZaposleniDokumenti d
                  WHERE d.Zaposleni = rm.Zaposleni AND d.VrstaDokumenta = o.VrstaDokumenta
                    AND (d.VaziDo IS NULL OR d.VaziDo >= CAST(GETDATE() AS date)));
GO

-- Upozorenja po zaposlenom (kolona Upozorenja u listi): broj i najhitnije upozorenje
CREATE OR ALTER VIEW dbo.vZaposleniUpozorenja
AS
SELECT r.Zaposleni,
       COUNT(*) AS BrojUpozorenja,
       MIN(r.DanaDo) AS NajmanjeDana,
       (SELECT TOP (1) CASE WHEN x.Datum IS NULL THEN x.StaIstice
                            WHEN x.DanaDo < 0 THEN x.StaIstice + N' istekla'
                            ELSE x.StaIstice + N' ističe ' + FORMAT(x.Datum, 'dd.MM.') END
        FROM dbo.vZaposleniRokovi x
        WHERE x.Zaposleni = r.Zaposleni AND x.DanaDo <= x.DanaUpozorenja
        ORDER BY x.DanaDo) AS Opis
FROM dbo.vZaposleniRokovi r
WHERE r.DanaDo <= r.DanaUpozorenja
GROUP BY r.Zaposleni;
GO

/* ---------------------------------------------------------------------------------------------
   6. Procedure: zaposleni
   --------------------------------------------------------------------------------------------- */

-- Novo / izmena radnog mesta. Novo otvoreno radno mesto automatski zatvara prethodno
-- (DatumDo = dan pre novog DatumOd). Periodi istog zaposlenog se ne preklapaju.
CREATE OR ALTER PROCEDURE dbo.HR_ZaposleniRadnoMestoSnimi
    @Sifra int, @Zaposleni int, @RadnoMesto int, @VrstaRadnogOdnosa int, @OrganizacionaJedinica int = NULL,
    @SluzbenoMesto int = NULL, @DatumOd date, @DatumDo date = NULL, @Napomena nvarchar(500) = NULL,
    @Korisnik nvarchar(20), @NovaSifra int = NULL OUTPUT
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRAN;
    IF @DatumDo IS NULL AND ISNULL(@Sifra, 0) = 0
    BEGIN
        DECLARE @Otvoreno int, @OtvorenoOd date;
        SELECT @Otvoreno = Sifra, @OtvorenoOd = DatumOd FROM dbo.ZaposleniRadnaMesta WITH (UPDLOCK, HOLDLOCK)
        WHERE Zaposleni = @Zaposleni AND DatumDo IS NULL;
        IF @Otvoreno IS NOT NULL
        BEGIN
            IF @OtvorenoOd >= @DatumOd
                THROW 50001, N'Novo radno mesto mora da počne posle početka trenutnog radnog mesta.', 1;
            UPDATE dbo.ZaposleniRadnaMesta
            SET DatumDo = DATEADD(DAY, -1, @DatumOd), IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime()
            WHERE Sifra = @Otvoreno;
        END
    END

    IF EXISTS (SELECT 1 FROM dbo.ZaposleniRadnaMesta
               WHERE Zaposleni = @Zaposleni AND Sifra <> ISNULL(@Sifra, 0)
                 AND DatumOd <= ISNULL(@DatumDo, '99991231') AND ISNULL(DatumDo, '99991231') >= @DatumOd)
        THROW 50002, N'Period se preklapa sa drugim radnim mestom istog zaposlenog.', 1;

    IF ISNULL(@Sifra, 0) = 0
    BEGIN
        INSERT dbo.ZaposleniRadnaMesta (Zaposleni, RadnoMesto, VrstaRadnogOdnosa, OrganizacionaJedinica, SluzbenoMesto,
                                        DatumOd, DatumDo, Napomena, KreiraoKorisnik)
        VALUES (@Zaposleni, @RadnoMesto, @VrstaRadnogOdnosa, @OrganizacionaJedinica, @SluzbenoMesto,
                @DatumOd, @DatumDo, @Napomena, @Korisnik);
        SET @NovaSifra = SCOPE_IDENTITY();
    END
    ELSE
    BEGIN
        UPDATE dbo.ZaposleniRadnaMesta
        SET RadnoMesto = @RadnoMesto, VrstaRadnogOdnosa = @VrstaRadnogOdnosa, OrganizacionaJedinica = @OrganizacionaJedinica,
            SluzbenoMesto = @SluzbenoMesto, DatumOd = @DatumOd, DatumDo = @DatumDo, Napomena = @Napomena,
            IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime()
        WHERE Sifra = @Sifra;
        SET @NovaSifra = @Sifra;
    END

    -- trenutna organizaciona jedinica se upisuje i u Zaposleni (stari ekrani je čitaju odatle)
    UPDATE z SET OrganizacionaJedinica = rm.OrganizacionaJedinica
    FROM dbo.Zaposleni z
    JOIN dbo.ZaposleniRadnaMesta rm ON rm.Zaposleni = z.ID AND rm.DatumDo IS NULL
    WHERE z.ID = @Zaposleni AND rm.OrganizacionaJedinica IS NOT NULL;
    COMMIT;
END
GO

-- Novo / izmena mentorstva: isto pravilo kao radno mesto; mentor mora biti aktivan zaposleni
CREATE OR ALTER PROCEDURE dbo.HR_ZaposleniMentorSnimi
    @Sifra int, @Zaposleni int, @Mentor int, @DatumOd date, @DatumDo date = NULL, @Napomena nvarchar(500) = NULL,
    @Korisnik nvarchar(20), @NovaSifra int = NULL OUTPUT
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    IF @Mentor = @Zaposleni
        THROW 50003, N'Zaposleni ne može biti sam sebi mentor.', 1;
    IF NOT EXISTS (SELECT 1 FROM dbo.Zaposleni WHERE ID = @Mentor AND Status = 'A')
        THROW 50004, N'Za mentora se može izabrati samo aktivan zaposleni.', 1;

    BEGIN TRAN;
    IF @DatumDo IS NULL AND ISNULL(@Sifra, 0) = 0
    BEGIN
        DECLARE @Otvoreno int, @OtvorenoOd date;
        SELECT @Otvoreno = Sifra, @OtvorenoOd = DatumOd FROM dbo.ZaposleniMentori WITH (UPDLOCK, HOLDLOCK)
        WHERE Zaposleni = @Zaposleni AND DatumDo IS NULL;
        IF @Otvoreno IS NOT NULL
        BEGIN
            IF @OtvorenoOd >= @DatumOd
                THROW 50005, N'Novo mentorstvo mora da počne posle početka trenutnog mentorstva.', 1;
            UPDATE dbo.ZaposleniMentori
            SET DatumDo = DATEADD(DAY, -1, @DatumOd), IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime()
            WHERE Sifra = @Otvoreno;
        END
    END

    IF EXISTS (SELECT 1 FROM dbo.ZaposleniMentori
               WHERE Zaposleni = @Zaposleni AND Sifra <> ISNULL(@Sifra, 0)
                 AND DatumOd <= ISNULL(@DatumDo, '99991231') AND ISNULL(DatumDo, '99991231') >= @DatumOd)
        THROW 50006, N'Period se preklapa sa drugim mentorstvom istog zaposlenog.', 1;

    IF ISNULL(@Sifra, 0) = 0
    BEGIN
        INSERT dbo.ZaposleniMentori (Zaposleni, Mentor, DatumOd, DatumDo, Napomena, KreiraoKorisnik)
        VALUES (@Zaposleni, @Mentor, @DatumOd, @DatumDo, @Napomena, @Korisnik);
        SET @NovaSifra = SCOPE_IDENTITY();
    END
    ELSE
    BEGIN
        UPDATE dbo.ZaposleniMentori
        SET Mentor = @Mentor, DatumOd = @DatumOd, DatumDo = @DatumDo, Napomena = @Napomena,
            IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime()
        WHERE Sifra = @Sifra;
        SET @NovaSifra = @Sifra;
    END
    COMMIT;
END
GO

-- Novi HTZ revers: broj po godini (2026/0001), dodeljuje se pod zaključavanjem
CREATE OR ALTER PROCEDURE dbo.HR_HTZReversNovi
    @Zaposleni int, @Datum date, @Izdao int = NULL, @Napomena nvarchar(500) = NULL, @Korisnik nvarchar(20),
    @Sifra int OUTPUT, @BrojReversa nvarchar(20) OUTPUT
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    DECLARE @Godina char(4) = CAST(YEAR(@Datum) AS char(4)), @Poslednji int;
    BEGIN TRAN;
    SELECT @Poslednji = MAX(CAST(SUBSTRING(BrojReversa, 6, 10) AS int))
    FROM dbo.HTZReversi WITH (UPDLOCK, HOLDLOCK)
    WHERE BrojReversa LIKE @Godina + '/%';
    SET @BrojReversa = @Godina + '/' + RIGHT('0000' + CAST(ISNULL(@Poslednji, 0) + 1 AS varchar(10)), 4);
    INSERT dbo.HTZReversi (BrojReversa, Zaposleni, Datum, Izdao, Napomena, KreiraoKorisnik)
    VALUES (@BrojReversa, @Zaposleni, @Datum, @Izdao, @Napomena, @Korisnik);
    SET @Sifra = SCOPE_IDENTITY();
    COMMIT;
END
GO

-- Stavka reversa; posle štampe (Zakljucen) stavke se ne menjaju
CREATE OR ALTER PROCEDURE dbo.HR_HTZStavkaSnimi
    @Sifra int, @Revers int, @HTZOprema int, @Kolicina decimal(9,2), @Velicina nvarchar(10) = NULL,
    @DatumZaduzenja date, @RokZamene date = NULL, @Napomena nvarchar(500) = NULL, @NovaSifra int = NULL OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Zaposleni int, @Zakljucen bit, @Storniran bit;
    SELECT @Zaposleni = Zaposleni, @Zakljucen = Zakljucen, @Storniran = Storniran FROM dbo.HTZReversi WHERE Sifra = @Revers;
    IF @Zaposleni IS NULL THROW 50010, N'Revers ne postoji.', 1;
    IF @Zakljucen = 1 OR @Storniran = 1
        THROW 50011, N'Revers je odštampan ili storniran; stavke se ne menjaju. Grešku ispravite storniranjem i novim reversom.', 1;

    IF @RokZamene IS NULL
        SELECT @RokZamene = DATEADD(MONTH, RokTrajanjaMeseci, @DatumZaduzenja) FROM dbo.HTZOprema WHERE Sifra = @HTZOprema AND RokTrajanjaMeseci IS NOT NULL;

    IF ISNULL(@Sifra, 0) = 0
    BEGIN
        INSERT dbo.ZaposleniHTZZaduzenja (Revers, Zaposleni, HTZOprema, Kolicina, Velicina, DatumZaduzenja, RokZamene, Napomena)
        VALUES (@Revers, @Zaposleni, @HTZOprema, @Kolicina, @Velicina, @DatumZaduzenja, @RokZamene, @Napomena);
        SET @NovaSifra = SCOPE_IDENTITY();
    END
    ELSE
    BEGIN
        UPDATE dbo.ZaposleniHTZZaduzenja
        SET HTZOprema = @HTZOprema, Kolicina = @Kolicina, Velicina = @Velicina, DatumZaduzenja = @DatumZaduzenja,
            RokZamene = @RokZamene, Napomena = @Napomena
        WHERE Sifra = @Sifra AND Revers = @Revers;
        SET @NovaSifra = @Sifra;
    END
END
GO

CREATE OR ALTER PROCEDURE dbo.HR_HTZStavkaObrisi @Sifra int
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM dbo.ZaposleniHTZZaduzenja s JOIN dbo.HTZReversi r ON r.Sifra = s.Revers
               WHERE s.Sifra = @Sifra AND (r.Zakljucen = 1 OR r.Storniran = 1))
        THROW 50011, N'Revers je odštampan ili storniran; stavke se ne menjaju. Grešku ispravite storniranjem i novim reversom.', 1;
    DELETE dbo.ZaposleniHTZZaduzenja WHERE Sifra = @Sifra;
END
GO

-- Štampa: prva štampa zaključava revers, svaka sledeća je KOPIJA
CREATE OR ALTER PROCEDURE dbo.HR_HTZReversStampa @Revers int, @Korisnik nvarchar(20), @Kopija bit OUTPUT
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRAN;
    SELECT @Kopija = Zakljucen FROM dbo.HTZReversi WITH (UPDLOCK) WHERE Sifra = @Revers;
    IF @Kopija IS NULL THROW 50010, N'Revers ne postoji.', 1;
    IF NOT EXISTS (SELECT 1 FROM dbo.ZaposleniHTZZaduzenja WHERE Revers = @Revers)
        THROW 50012, N'Revers nema stavki.', 1;
    IF @Kopija = 0
        UPDATE dbo.HTZReversi SET Zakljucen = 1, IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime() WHERE Sifra = @Revers;
    COMMIT;
END
GO

CREATE OR ALTER PROCEDURE dbo.HR_HTZReversStorniraj @Revers int, @Korisnik nvarchar(20)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.HTZReversi SET Storniran = 1, IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime() WHERE Sifra = @Revers;
END
GO

CREATE OR ALTER PROCEDURE dbo.HR_HTZRazduzi @Stavka int, @Datum date, @Razlog int
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.ZaposleniHTZZaduzenja SET DatumRazduzenja = @Datum, RazlogRazduzenja = @Razlog
    WHERE Sifra = @Stavka AND DatumRazduzenja IS NULL;
END
GO

-- Kontrolna tabla rokova: sve što ističe u narednih @Dana dana ili je već isteklo (samo aktivni zaposleni)
CREATE OR ALTER PROCEDURE dbo.HR_KontrolnaTablaRokova @Dana int = 30, @OrganizacionaJedinica int = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT r.Zaposleni, t.PrezimeIme AS ZaposleniNaziv, t.OrganizacionaJedinicaNaziv AS OrgJedinica,
           r.Vrsta, r.StaIstice, r.Datum, r.DanaDo
    FROM dbo.vZaposleniRokovi r
    JOIN dbo.vZaposleniTrenutno t ON t.Zaposleni = r.Zaposleni
    WHERE t.Status = 'A'
      AND r.DanaDo <= @Dana
      AND (@OrganizacionaJedinica IS NULL OR t.OrganizacionaJedinica = @OrganizacionaJedinica)
    ORDER BY r.DanaDo, t.PrezimeIme;
END
GO

/* ---------------------------------------------------------------------------------------------
   7. Procedure: zahtev za nabavku
   --------------------------------------------------------------------------------------------- */
CREATE OR ALTER PROCEDURE dbo.HR_ZahtevIstorija @Zahtev int, @StatusOd int, @StatusDo int, @Korisnik nvarchar(20), @Komentar nvarchar(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT dbo.ZahtevZaNabavkuIstorijaStatusa (Zahtev, StatusOd, StatusDo, Korisnik, Komentar)
    VALUES (@Zahtev, @StatusOd, @StatusDo, @Korisnik, @Komentar);
    UPDATE dbo.ZahteviZaNabavku SET Status = @StatusDo, IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime() WHERE Sifra = @Zahtev;
END
GO

-- Novi zahtev u statusu Nacrt; broj ZN-2026/0001 po godini
CREATE OR ALTER PROCEDURE dbo.HR_ZahtevNovi
    @Datum date, @Podnosilac int, @OrganizacionaJedinica int, @VrstaNabavke int, @Prioritet int, @Svrha nvarchar(500),
    @Korisnik nvarchar(20), @Sifra int OUTPUT, @BrojZahteva nvarchar(20) OUTPUT
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    DECLARE @Prefiks nvarchar(10) = N'ZN-' + CAST(YEAR(@Datum) AS nvarchar(4)) + N'/', @Poslednji int;
    BEGIN TRAN;
    SELECT @Poslednji = MAX(CAST(SUBSTRING(BrojZahteva, LEN(@Prefiks) + 1, 10) AS int))
    FROM dbo.ZahteviZaNabavku WITH (UPDLOCK, HOLDLOCK)
    WHERE BrojZahteva LIKE @Prefiks + N'%';
    SET @BrojZahteva = @Prefiks + RIGHT(N'0000' + CAST(ISNULL(@Poslednji, 0) + 1 AS nvarchar(10)), 4);
    INSERT dbo.ZahteviZaNabavku (BrojZahteva, Datum, Podnosilac, OrganizacionaJedinica, VrstaNabavke, Prioritet, Svrha, Status, KreiraoKorisnik)
    VALUES (@BrojZahteva, @Datum, @Podnosilac, @OrganizacionaJedinica, @VrstaNabavke, @Prioritet, @Svrha, 1, @Korisnik);
    SET @Sifra = SCOPE_IDENTITY();
    INSERT dbo.ZahtevZaNabavkuIstorijaStatusa (Zahtev, StatusOd, StatusDo, Korisnik, Komentar)
    VALUES (@Sifra, NULL, 1, @Korisnik, N'Kreiran');
    COMMIT;
END
GO

-- Plan odobravanja: za svaki nivo bira se najpopunjenije pravilo koje odgovara vrsti, jedinici i iznosu.
-- Odobravalac je konkretna osoba iz pravila ili aktivan zaposleni na radnom mestu iz pravila
-- (prednost ima isti iz jedinice zahteva). Ako je odobravalac podnosilac, odobrava zamenik;
-- ako zamenika nema, nivo se preskače (prosleđuje se sledećem nivou).
CREATE OR ALTER FUNCTION dbo.fnHR_ZahtevPlanOdobravanja (@Zahtev int)
RETURNS @plan TABLE (Nivo tinyint, Odobravalac int NULL, Opis nvarchar(150), Preskocen bit)
AS
BEGIN
    DECLARE @Vrsta int, @OrgJed int, @Iznos decimal(18,2), @Podnosilac int;
    SELECT @Vrsta = VrstaNabavke, @OrgJed = OrganizacionaJedinica, @Iznos = ProcenjenaVrednost, @Podnosilac = Podnosilac
    FROM dbo.ZahteviZaNabavku WHERE Sifra = @Zahtev;

    ;WITH pravila AS (
        SELECT p.*, ROW_NUMBER() OVER (PARTITION BY p.Nivo
                                       ORDER BY CASE WHEN p.VrstaNabavke IS NULL THEN 0 ELSE 1 END
                                              + CASE WHEN p.OrganizacionaJedinica IS NULL THEN 0 ELSE 1 END DESC,
                                                p.IznosOd DESC, p.Sifra) AS rb
        FROM dbo.PravilaOdobravanjaNabavke p
        WHERE p.Aktivan = 1
          AND (p.VrstaNabavke IS NULL OR p.VrstaNabavke = @Vrsta)
          AND (p.OrganizacionaJedinica IS NULL OR p.OrganizacionaJedinica = @OrgJed)
          AND @Iznos >= p.IznosOd AND (p.IznosDo IS NULL OR @Iznos <= p.IznosDo))
    INSERT @plan (Nivo, Odobravalac, Opis, Preskocen)
    SELECT p.Nivo,
           CASE WHEN o.Odobravalac = @Podnosilac THEN p.Zamenik ELSE o.Odobravalac END,
           ISNULL(rm.Naziv, N'Odobravalac'),
           CASE WHEN o.Odobravalac IS NULL OR (o.Odobravalac = @Podnosilac AND p.Zamenik IS NULL) THEN 1 ELSE 0 END
    FROM pravila p
    LEFT JOIN dbo.RadnaMesta rm ON rm.Sifra = p.RadnoMestoOdobravaoca
    OUTER APPLY (SELECT COALESCE(p.Odobravalac,
                     (SELECT TOP (1) t.Zaposleni FROM dbo.vZaposleniTrenutno t
                      WHERE t.RadnoMesto = p.RadnoMestoOdobravaoca AND t.Status = 'A'
                      ORDER BY CASE WHEN t.OrganizacionaJedinica = @OrgJed THEN 0 ELSE 1 END, t.Zaposleni)) AS Odobravalac) o
    WHERE p.rb = 1;
    RETURN;
END
GO

-- Pregled toka odobravanja za formu: plan + odluke tekućeg kruga
CREATE OR ALTER PROCEDURE dbo.HR_ZahtevTokOdobravanja @Zahtev int
AS
BEGIN
    SET NOCOUNT ON;
    -- tekući krug: odluke posle poslednjeg podnošenja
    DECLARE @Podnet datetime2 = (SELECT MAX(DatumVreme) FROM dbo.ZahtevZaNabavkuIstorijaStatusa WHERE Zahtev = @Zahtev AND StatusDo = 2);
    SELECT p.Nivo, p.Opis, p.Preskocen,
           ISNULL(od.Odobravalac, p.Odobravalac) AS Odobravalac,
           LTRIM(RTRIM(ISNULL(z.Prezime, N'') + N' ' + ISNULL(z.Ime, N''))) AS OdobravalacNaziv,
           od.Odluka, od.DatumOdluke, od.Komentar,
           CASE WHEN od.Sifra IS NOT NULL AND od.Odluka IS NULL THEN 1 ELSE 0 END AS Ceka
    FROM dbo.fnHR_ZahtevPlanOdobravanja(@Zahtev) p
    OUTER APPLY (SELECT TOP (1) o.* FROM dbo.ZahtevZaNabavkuOdobrenja o
                 WHERE o.Zahtev = @Zahtev AND o.Nivo = p.Nivo AND o.DatumZadavanja >= ISNULL(@Podnet, '19000101')
                 ORDER BY o.Sifra DESC) od
    LEFT JOIN dbo.Zaposleni z ON z.ID = ISNULL(od.Odobravalac, p.Odobravalac)
    ORDER BY p.Nivo;
END
GO

-- Zbir stavki i status realizacije; povećanje iznosa posle odobrenja vraća zahtev na odobravanje
CREATE OR ALTER PROCEDURE dbo.HR_ZahtevPreracunaj @Zahtev int, @Korisnik nvarchar(20)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE z SET ProcenjenaVrednost = ISNULL((SELECT SUM(s.ProcenjenaVrednost) FROM dbo.ZahtevZaNabavkuStavke s WHERE s.Zahtev = z.Sifra), 0)
    FROM dbo.ZahteviZaNabavku z WHERE z.Sifra = @Zahtev;

    DECLARE @Status int, @Iznos decimal(18,2), @Odobreno decimal(18,2);
    SELECT @Status = Status, @Iznos = ProcenjenaVrednost, @Odobreno = OdobreniIznos FROM dbo.ZahteviZaNabavku WHERE Sifra = @Zahtev;

    IF @Status IN (5, 6) AND @Odobreno IS NOT NULL AND @Iznos > @Odobreno
    BEGIN
        EXEC dbo.HR_ZahtevPodnesi @Zahtev = @Zahtev, @Korisnik = @Korisnik,
             @Komentar = N'Iznos je povećan posle odobrenja; zahtev ide na ponovno odobravanje';
        RETURN;
    END

    IF @Status IN (6, 7) AND EXISTS (SELECT 1 FROM dbo.ZahtevZaNabavkuStavke WHERE Zahtev = @Zahtev)
    BEGIN
        DECLARE @Novi int =
            CASE WHEN NOT EXISTS (SELECT 1 FROM dbo.ZahtevZaNabavkuStavke WHERE Zahtev = @Zahtev AND StatusStavke NOT IN (4, 5)) THEN 8
                 WHEN EXISTS (SELECT 1 FROM dbo.ZahtevZaNabavkuStavke WHERE Zahtev = @Zahtev AND PrimljenaKolicina > 0) THEN 7
                 ELSE @Status END;
        IF @Novi <> @Status
            EXEC dbo.HR_ZahtevIstorija @Zahtev, @Status, @Novi, @Korisnik, NULL;
    END
END
GO

-- Podnošenje (iz nacrta, posle dorade ili ponovno odobravanje): Podnet, pa odmah Na odobrenju kod prvog nivoa
CREATE OR ALTER PROCEDURE dbo.HR_ZahtevPodnesi @Zahtev int, @Korisnik nvarchar(20), @Komentar nvarchar(500) = NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    DECLARE @Status int = (SELECT Status FROM dbo.ZahteviZaNabavku WHERE Sifra = @Zahtev);
    IF @Status NOT IN (1, 4, 5, 6)
        THROW 50020, N'Zahtev se može podneti samo iz nacrta ili kad je vraćen na doradu.', 1;
    IF NOT EXISTS (SELECT 1 FROM dbo.ZahtevZaNabavkuStavke WHERE Zahtev = @Zahtev)
        THROW 50021, N'Zahtev nema nijednu stavku.', 1;

    UPDATE z SET ProcenjenaVrednost = ISNULL((SELECT SUM(s.ProcenjenaVrednost) FROM dbo.ZahtevZaNabavkuStavke s WHERE s.Zahtev = z.Sifra), 0)
    FROM dbo.ZahteviZaNabavku z WHERE z.Sifra = @Zahtev;

    DECLARE @PrviNivo tinyint, @Odobravalac int;
    SELECT TOP (1) @PrviNivo = Nivo, @Odobravalac = Odobravalac
    FROM dbo.fnHR_ZahtevPlanOdobravanja(@Zahtev) WHERE Preskocen = 0 ORDER BY Nivo;
    IF @PrviNivo IS NULL
        THROW 50022, N'Za ovaj zahtev nije definisano nijedno pravilo odobravanja (HR modul > Pravila odobravanja), ili za radno mesto iz pravila nema aktivnog zaposlenog.', 1;

    BEGIN TRAN;
    -- odluke koje čekaju iz prethodnog kruga se zatvaraju
    UPDATE dbo.ZahtevZaNabavkuOdobrenja SET Odluka = 'V', DatumOdluke = sysdatetime(), Komentar = N'Zatvoreno ponovnim podnošenjem'
    WHERE Zahtev = @Zahtev AND Odluka IS NULL;

    EXEC dbo.HR_ZahtevIstorija @Zahtev, @Status, 2, @Korisnik, @Komentar;
    EXEC dbo.HR_ZahtevIstorija @Zahtev, 2, 3, @Korisnik, NULL;
    UPDATE dbo.ZahteviZaNabavku SET TrenutniNivoOdobravanja = @PrviNivo, OdobreniIznos = NULL WHERE Sifra = @Zahtev;
    INSERT dbo.ZahtevZaNabavkuOdobrenja (Zahtev, Nivo, Odobravalac) VALUES (@Zahtev, @PrviNivo, @Odobravalac);
    COMMIT;
END
GO

-- Odluka odobravaoca: O = odobreno, V = vraćeno na doradu, D = odbijeno
CREATE OR ALTER PROCEDURE dbo.HR_ZahtevOdluka @Zahtev int, @Odobravalac int, @Odluka char(1), @Komentar nvarchar(1000) = NULL, @Korisnik nvarchar(20)
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    IF @Odluka NOT IN ('O', 'V', 'D') THROW 50030, N'Nepoznata odluka.', 1;
    IF @Odluka IN ('V', 'D') AND LTRIM(RTRIM(ISNULL(@Komentar, N''))) = N''
        THROW 50031, N'Komentar je obavezan kod vraćanja na doradu i odbijanja.', 1;

    DECLARE @Status int, @Nivo tinyint, @Podnosilac int, @Odobrenje int;
    SELECT @Status = Status, @Nivo = TrenutniNivoOdobravanja, @Podnosilac = Podnosilac FROM dbo.ZahteviZaNabavku WHERE Sifra = @Zahtev;
    IF @Status <> 3 THROW 50032, N'Zahtev nije na odobrenju.', 1;
    IF @Odobravalac = @Podnosilac THROW 50033, N'Podnosilac ne može da odobri sopstveni zahtev.', 1;

    SELECT @Odobrenje = Sifra FROM dbo.ZahtevZaNabavkuOdobrenja
    WHERE Zahtev = @Zahtev AND Nivo = @Nivo AND Odobravalac = @Odobravalac AND Odluka IS NULL;
    IF @Odobrenje IS NULL THROW 50034, N'Zahtev ne čeka vašu odluku.', 1;

    BEGIN TRAN;
    UPDATE dbo.ZahtevZaNabavkuOdobrenja SET Odluka = @Odluka, DatumOdluke = sysdatetime(), Komentar = @Komentar WHERE Sifra = @Odobrenje;

    IF @Odluka = 'O'
    BEGIN
        DECLARE @SledeciNivo tinyint, @Sledeci int;
        SELECT TOP (1) @SledeciNivo = Nivo, @Sledeci = Odobravalac
        FROM dbo.fnHR_ZahtevPlanOdobravanja(@Zahtev) WHERE Preskocen = 0 AND Nivo > @Nivo ORDER BY Nivo;
        IF @SledeciNivo IS NOT NULL
        BEGIN
            UPDATE dbo.ZahteviZaNabavku SET TrenutniNivoOdobravanja = @SledeciNivo WHERE Sifra = @Zahtev;
            INSERT dbo.ZahtevZaNabavkuOdobrenja (Zahtev, Nivo, Odobravalac) VALUES (@Zahtev, @SledeciNivo, @Sledeci);
        END
        ELSE
        BEGIN
            UPDATE dbo.ZahteviZaNabavku SET TrenutniNivoOdobravanja = NULL, OdobreniIznos = ProcenjenaVrednost WHERE Sifra = @Zahtev;
            EXEC dbo.HR_ZahtevIstorija @Zahtev, 3, 5, @Korisnik, @Komentar;
        END
    END
    ELSE
    BEGIN
        DECLARE @NoviStatus int = CASE @Odluka WHEN 'V' THEN 4 ELSE 9 END;
        UPDATE dbo.ZahteviZaNabavku SET TrenutniNivoOdobravanja = NULL WHERE Sifra = @Zahtev;
        EXEC dbo.HR_ZahtevIstorija @Zahtev, 3, @NoviStatus, @Korisnik, @Komentar;
    END
    COMMIT;
END
GO

-- Storniranje: podnosilac iz nacrta / dorade, nabavka iz odobrenog ili u nabavci
CREATE OR ALTER PROCEDURE dbo.HR_ZahtevStorniraj @Zahtev int, @Korisnik nvarchar(20), @Komentar nvarchar(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Status int = (SELECT Status FROM dbo.ZahteviZaNabavku WHERE Sifra = @Zahtev);
    IF @Status NOT IN (1, 4, 5, 6)
        THROW 50040, N'Zahtev u ovom statusu ne može da se stornira.', 1;
    UPDATE dbo.ZahtevZaNabavkuOdobrenja SET Odluka = 'D', DatumOdluke = sysdatetime(), Komentar = N'Storniran'
    WHERE Zahtev = @Zahtev AND Odluka IS NULL;
    EXEC dbo.HR_ZahtevIstorija @Zahtev, @Status, 10, @Korisnik, @Komentar;
END
GO

-- Nabavka preuzima odobren zahtev; ako ima ponuda, jedna mora biti izabrana
CREATE OR ALTER PROCEDURE dbo.HR_ZahtevUNabavku @Zahtev int, @Korisnik nvarchar(20)
AS
BEGIN
    SET NOCOUNT ON;
    IF (SELECT Status FROM dbo.ZahteviZaNabavku WHERE Sifra = @Zahtev) <> 5
        THROW 50050, N'U nabavku ide samo odobren zahtev.', 1;
    IF EXISTS (SELECT 1 FROM dbo.ZahtevZaNabavkuPonude WHERE Zahtev = @Zahtev)
       AND NOT EXISTS (SELECT 1 FROM dbo.ZahtevZaNabavkuPonude WHERE Zahtev = @Zahtev AND Izabrana = 1)
        THROW 50051, N'Izaberite ponudu pre slanja u nabavku.', 1;
    UPDATE dbo.ZahtevZaNabavkuStavke SET StatusStavke = 2 WHERE Zahtev = @Zahtev AND StatusStavke = 1;
    EXEC dbo.HR_ZahtevIstorija @Zahtev, 5, 6, @Korisnik, NULL;
END
GO

-- Izbor ponude: najviše jedna izabrana; ako nije najjeftinija, obrazloženje je obavezno
CREATE OR ALTER PROCEDURE dbo.HR_PonudaIzaberi @Ponuda int, @RazlogIzbora nvarchar(500) = NULL, @Korisnik nvarchar(20)
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    DECLARE @Zahtev int, @Iznos decimal(18,2), @Valuta char(3), @Partner int;
    SELECT @Zahtev = Zahtev, @Iznos = Iznos, @Valuta = Valuta, @Partner = Partner FROM dbo.ZahtevZaNabavkuPonude WHERE Sifra = @Ponuda;
    IF @Zahtev IS NULL THROW 50060, N'Ponuda ne postoji.', 1;
    IF EXISTS (SELECT 1 FROM dbo.ZahtevZaNabavkuPonude WHERE Zahtev = @Zahtev AND Valuta = @Valuta AND Iznos < @Iznos)
       AND LTRIM(RTRIM(ISNULL(@RazlogIzbora, N''))) = N''
        THROW 50061, N'Izabrana ponuda nije najjeftinija; upišite obrazloženje izbora.', 1;
    BEGIN TRAN;
    UPDATE dbo.ZahtevZaNabavkuPonude SET Izabrana = 0 WHERE Zahtev = @Zahtev AND Izabrana = 1;
    UPDATE dbo.ZahtevZaNabavkuPonude
    SET Izabrana = 1, RazlogIzbora = NULLIF(LTRIM(RTRIM(@RazlogIzbora)), N''), IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime()
    WHERE Sifra = @Ponuda;
    UPDATE dbo.ZahteviZaNabavku SET PredlozeniDobavljac = @Partner WHERE Sifra = @Zahtev;
    COMMIT;
END
GO

-- Prijem robe po stavci; status stavke i zahteva se računa automatski
CREATE OR ALTER PROCEDURE dbo.HR_StavkaPrijem @Stavka int, @Kolicina decimal(18,3), @Odbijena bit = 0, @Korisnik nvarchar(20)
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    DECLARE @Zahtev int, @Status int;
    SELECT @Zahtev = s.Zahtev, @Status = z.Status FROM dbo.ZahtevZaNabavkuStavke s JOIN dbo.ZahteviZaNabavku z ON z.Sifra = s.Zahtev WHERE s.Sifra = @Stavka;
    IF @Status NOT IN (6, 7) THROW 50070, N'Prijem se upisuje samo za zahtev u nabavci.', 1;
    BEGIN TRAN;
    UPDATE dbo.ZahtevZaNabavkuStavke
    SET PrimljenaKolicina = PrimljenaKolicina + CASE WHEN @Odbijena = 1 THEN 0 ELSE @Kolicina END,
        StatusStavke = CASE WHEN @Odbijena = 1 THEN 5
                            WHEN PrimljenaKolicina + @Kolicina >= Kolicina THEN 4
                            WHEN PrimljenaKolicina + @Kolicina > 0 THEN 3
                            ELSE StatusStavke END
    WHERE Sifra = @Stavka;
    EXEC dbo.HR_ZahtevPreracunaj @Zahtev, @Korisnik;
    COMMIT;
END
GO

/* ---------------------------------------------------------------------------------------------
   8. Glavni prozor: modul 'HR modul', kartice i prava
      Modul (MainNovi) i kartice (mainNoviKartice) prave se kao kopije postojećih zapisa modula
      'Administracija'; kolone se čitaju iz baze, pa skripta ne zavisi od tačne strukture tabela.
      Korisnici sa bilo kojim pravom u Administraciji dobijaju sva prava u HR modulu;
      ostalima prava dodeljuje administrator kroz postojeći ekran za prava.
      Kartice 'Osetljivi podaci' i 'Obrada nabavke' nemaju svoju formu: one su samo pravo
      (osetljivi podaci zaposlenog; unos ponuda, slanje u nabavku i prijem robe).
   --------------------------------------------------------------------------------------------- */
IF OBJECT_ID('dbo.MainNovi', 'U') IS NOT NULL AND OBJECT_ID('dbo.mainNoviKartice', 'U') IS NOT NULL AND OBJECT_ID('dbo.MainNoviPrava', 'U') IS NOT NULL
BEGIN
    DECLARE @AdminMain int = (SELECT TOP (1) ID FROM dbo.MainNovi WHERE LTRIM(RTRIM(Naziv)) = N'Administracija' ORDER BY ID);
    DECLARE @HRMain int = (SELECT TOP (1) ID FROM dbo.MainNovi WHERE LTRIM(RTRIM(Naziv)) = N'HR modul' ORDER BY ID);
    DECLARE @sql nvarchar(max), @kolone nvarchar(max), @vrednosti nvarchar(max);

    IF @AdminMain IS NULL
        PRINT N'Modul Administracija nije pronađen u MainNovi; modul HR treba dodati ručno.';
    ELSE
    BEGIN
        IF @HRMain IS NULL
        BEGIN
            SELECT @kolone = STUFF((SELECT N', ' + QUOTENAME(c.name) FROM sys.columns c
                                    WHERE c.object_id = OBJECT_ID('dbo.MainNovi') AND c.is_identity = 0 AND c.is_computed = 0
                                      AND TYPE_NAME(c.system_type_id) <> 'timestamp'
                                    ORDER BY c.column_id FOR XML PATH('')), 1, 2, N''),
                   @vrednosti = STUFF((SELECT N', ' + CASE c.name WHEN 'Naziv' THEN N'N''HR modul'''
                                                                   WHEN 'ID' THEN N'(SELECT ISNULL(MAX(ID), 0) + 1 FROM dbo.MainNovi)'
                                                                   ELSE QUOTENAME(c.name) END
                                       FROM sys.columns c
                                       WHERE c.object_id = OBJECT_ID('dbo.MainNovi') AND c.is_identity = 0 AND c.is_computed = 0
                                         AND TYPE_NAME(c.system_type_id) <> 'timestamp'
                                       ORDER BY c.column_id FOR XML PATH('')), 1, 2, N'');
            SET @sql = N'INSERT INTO dbo.MainNovi (' + @kolone + N') SELECT ' + @vrednosti + N' FROM dbo.MainNovi WHERE ID = @Izvor';
            EXEC sp_executesql @sql, N'@Izvor int', @Izvor = @AdminMain;
            SET @HRMain = (SELECT TOP (1) ID FROM dbo.MainNovi WHERE LTRIM(RTRIM(Naziv)) = N'HR modul' ORDER BY ID);
            PRINT N'Dodat modul HR modul, ID = ' + CAST(@HRMain AS nvarchar(10));
        END

        DECLARE @Sablon int = (SELECT TOP (1) ID FROM dbo.mainNoviKartice WHERE MainID = @AdminMain AND Parent IS NULL ORDER BY ID);
        IF @Sablon IS NULL
            SET @Sablon = (SELECT TOP (1) ID FROM dbo.mainNoviKartice WHERE Parent IS NULL ORDER BY ID);

        DECLARE @Kartica nvarchar(50), @KarticaID int;
        DECLARE kartice CURSOR LOCAL FAST_FORWARD FOR
            SELECT Naziv FROM (VALUES (N'Zaposleni'), (N'Kontrolna tabla rokova'), (N'Zahtevi za nabavku'),
                                      (N'Pravila odobravanja'), (N'Šifarnici HR'), (N'Osetljivi podaci'), (N'Obrada nabavke')) n (Naziv);
        OPEN kartice;
        FETCH NEXT FROM kartice INTO @Kartica;
        WHILE @@FETCH_STATUS = 0 AND @Sablon IS NOT NULL
        BEGIN
            SET @KarticaID = (SELECT TOP (1) ID FROM dbo.mainNoviKartice WHERE MainID = @HRMain AND Parent IS NULL AND Naziv = @Kartica);
            IF @KarticaID IS NULL
            BEGIN
                SELECT @kolone = STUFF((SELECT N', ' + QUOTENAME(c.name) FROM sys.columns c
                                        WHERE c.object_id = OBJECT_ID('dbo.mainNoviKartice') AND c.is_identity = 0 AND c.is_computed = 0
                                          AND TYPE_NAME(c.system_type_id) <> 'timestamp'
                                        ORDER BY c.column_id FOR XML PATH('')), 1, 2, N''),
                       @vrednosti = STUFF((SELECT N', ' + CASE c.name WHEN 'Naziv' THEN N'@Kartica'
                                                                       WHEN 'MainID' THEN N'@HRMain'
                                                                       WHEN 'Parent' THEN N'NULL'
                                                                       WHEN 'ID' THEN N'(SELECT ISNULL(MAX(ID), 0) + 1 FROM dbo.mainNoviKartice)'
                                                                       ELSE QUOTENAME(c.name) END
                                           FROM sys.columns c
                                           WHERE c.object_id = OBJECT_ID('dbo.mainNoviKartice') AND c.is_identity = 0 AND c.is_computed = 0
                                             AND TYPE_NAME(c.system_type_id) <> 'timestamp'
                                           ORDER BY c.column_id FOR XML PATH('')), 1, 2, N'');
                SET @sql = N'INSERT INTO dbo.mainNoviKartice (' + @kolone + N') SELECT ' + @vrednosti + N' FROM dbo.mainNoviKartice WHERE ID = @Izvor';
                EXEC sp_executesql @sql, N'@Izvor int, @Kartica nvarchar(50), @HRMain int', @Izvor = @Sablon, @Kartica = @Kartica, @HRMain = @HRMain;
                SET @KarticaID = (SELECT TOP (1) ID FROM dbo.mainNoviKartice WHERE MainID = @HRMain AND Parent IS NULL AND Naziv = @Kartica);
                PRINT N'Dodata kartica ' + @Kartica + N', ID = ' + CAST(@KarticaID AS nvarchar(10));
            END

            -- prava: po jedan red za svakog korisnika koji ima pravo u Administraciji (kopija njegovog prvog reda)
            SELECT @kolone = STUFF((SELECT N', ' + QUOTENAME(c.name) FROM sys.columns c
                                    WHERE c.object_id = OBJECT_ID('dbo.MainNoviPrava') AND c.is_identity = 0 AND c.is_computed = 0
                                      AND TYPE_NAME(c.system_type_id) <> 'timestamp'
                                    ORDER BY c.column_id FOR XML PATH('')), 1, 2, N''),
                   @vrednosti = STUFF((SELECT N', ' + CASE c.name WHEN 'KarticaID' THEN N'@KarticaID'
                                                                   WHEN 'MainID' THEN N'@HRMain'
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
                     + N'   AND NOT EXISTS (SELECT 1 FROM dbo.MainNoviPrava x WHERE x.MainID = @HRMain AND x.KarticaID = @KarticaID AND x.Korisnik = p.Korisnik);';
            EXEC sp_executesql @sql, N'@AdminMain int, @HRMain int, @KarticaID int', @AdminMain = @AdminMain, @HRMain = @HRMain, @KarticaID = @KarticaID;

            FETCH NEXT FROM kartice INTO @Kartica;
        END
        CLOSE kartice;
        DEALLOCATE kartice;
    END
END
GO

DROP PROCEDURE #HR_DodajFK;
GO

PRINT N'HR modul: skripta završena.';
GO
