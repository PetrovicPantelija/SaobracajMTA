/* =============================================================================================
   HR modul, faza 2: Podešavanja, hijerarhija zaposleni–nadređeni, evidencija rada,
   beleške o radu, godišnji odmori sa zahtevom, odobravanjem i rešenjem, log.
   Specifikacija: "HR modul – specifikacija.md" (26.10.2026.)

   Pokreće se POSLE skripte HR_Modul_2026_10.sql. Može da se pokreće više puta.

   Usklađivanje sa postojećom bazom (specifikacija: "ako se tabela ili ključ zovu drugačije,
   menja se samo naziv u skriptama"):
     - Zaposleni su u dbo.Zaposleni sa ključem ID; kolone ZaposleniSifra / NadredjeniSifra pokazuju na Zaposleni.ID.
     - Zaposleni već ima email, DatumUlaska, DatumIzlaska i Status (A/P); koriste se umesto
       Email, DatumZaposlenja, DatumPrestanka i Aktivan iz specifikacije.
     - Postojeće tabele Dopust i DopustStavke (stari obrazac, vezane za Delavci) koriste stari ekrani,
       pa se ne diraju. Nove tabele se zovu GODopust, GODopustStavke i GODopustOsnov; postojeći podaci
       se prebacuju u njih (sekcija 9), uparivanjem Delavci.DeSifra sa Zaposleni.SifraERP.
     - Korisnik aplikacije u logu dolazi iz SESSION_CONTEXT('Korisnik') koji postavlja aplikacija.
   ============================================================================================= */

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

/* ---------------------------------------------------------------------------------------------
   1. Podesavanja (cela aplikacija)
   --------------------------------------------------------------------------------------------- */
IF OBJECT_ID('dbo.Podesavanja', 'U') IS NULL
    CREATE TABLE dbo.Podesavanja (
        ID           int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Podesavanja PRIMARY KEY,
        Grupa        nvarchar(50)  NOT NULL,
        Kljuc        nvarchar(100) NOT NULL,
        Vrednost     nvarchar(max) NULL,
        TipVrednosti varchar(20)   NOT NULL CONSTRAINT DF_Podesavanja_Tip DEFAULT ('TEXT'),
        Opis         nvarchar(250) NULL,
        NameOper     char(20)      NULL,
        DatumIzmene  datetime      NOT NULL CONSTRAINT DF_Podesavanja_Datum DEFAULT (GETDATE()),
        CONSTRAINT UQ_Podesavanja_Kljuc UNIQUE (Kljuc));
GO

-- Početni ključevi. Podaci o firmi i putanje Terminala se pri prvom pokretanju popunjavaju iz programa
-- (Config.cs za firmu na koju je program prijavljen), ako su ovde prazni.
DECLARE @p TABLE (Grupa nvarchar(50), Kljuc nvarchar(100), Vrednost nvarchar(max), Tip varchar(20), Opis nvarchar(250));
INSERT @p VALUES
 (N'Firma',  N'Firma.Naziv',              NULL, 'TEXT', N'Naziv firme: uvod i potpis rešenja, izveštaji, mail'),
 (N'Firma',  N'Firma.Adresa',             NULL, 'TEXT', N'Ulica i broj: uvod rešenja'),
 (N'Firma',  N'Firma.Mesto',              NULL, 'TEXT', N'Mesto: uvod rešenja'),
 (N'Firma',  N'Firma.MaticniBroj',        NULL, 'TEXT', N'Matični broj: uvod rešenja'),
 (N'Firma',  N'Firma.PIB',                NULL, 'TEXT', N'PIB: uvod rešenja'),
 (N'Firma',  N'Firma.Direktor',           NULL, 'TEXT', N'Direktor: uvod ("koga zastupa") i potpis rešenja'),
 (N'Firma',  N'Firma.Logo',               NULL, 'PATH', N'Logo za zaglavlje dokumenata'),
 (N'Putanje',N'Putanje.TerminalSlike',    NULL, 'PATH', N'Terminal privremeni – Zakači slike'),
 (N'Putanje',N'Putanje.TerminalDokumenta',NULL, 'PATH', N'Terminal privremeni – Zakači dokumenta'),
 (N'Putanje',N'Putanje.HRResenja',        NULL, 'PATH', N'PDF rešenja o godišnjem odmoru, po godinama'),
 (N'Putanje',N'Putanje.Sabloni',          NULL, 'PATH', N'Šabloni dokumenata'),
 (N'Mail',   N'Mail.SmtpServer',          NULL, 'TEXT', N'SMTP server za slanje rešenja'),
 (N'Mail',   N'Mail.SmtpPort',            N'25', 'INT', N'SMTP port'),
 (N'Mail',   N'Mail.SSL',                 N'1', 'BOOL', N'SMTP preko SSL/TLS'),
 (N'Mail',   N'Mail.Posiljalac',          NULL, 'TEXT', N'Adresa pošiljaoca rešenja'),
 (N'Mail',   N'Mail.Korisnik',            NULL, 'TEXT', N'Korisničko ime za SMTP'),
 (N'Mail',   N'Mail.Lozinka',             NULL, 'PASSWORD', N'Lozinka za SMTP (čuva se šifrovana)'),
 (N'Mail',   N'Mail.KopijaResenja',       NULL, 'TEXT', N'Adresa na koju ide kopija svakog rešenja (prazno = bez kopije)'),
 (N'HR',     N'HR.DnevniFondSati',        N'8', 'DECIMAL', N'Dnevni fond sati kada zaposleni nema raspored rada (pon–pet)'),
 (N'HR',     N'HR.GOFormatBrojaResenja',  N'GO-{GGGG}/{BROJ:000}', 'TEXT', N'Broj rešenja; {GGGG} = godina, {BROJ:000} = redni broj sa nulama'),
 (N'HR',     N'HR.GOZaokruzivanje',       N'NAVISE', 'TEXT', N'Srazmerni deo GO: NAVISE ili MATEMATICKI'),
 (N'HR',     N'HR.ResenjePravniOsnov',    N'Na osnovu člana 192 a u vezi sa članom 68, 69, 70, 72, 73 i 75 Zakona o radu Republike Srbije (''Sl. glasnik RS'', br. 24/05, 61/05, 54/09, 32/13, 75/14, 13/17 – odluka US, 113/17 i 95/18 – autentično tumačenje)', 'TEXT', N'Pravni osnov u uvodu rešenja; menja se kad se menja zakon'),
 (N'HR',     N'HR.ResenjePoukaOPravnomLeku', N'Protiv ovog rešenja zaposleni može podneti tužbu nadležnom sudu u roku od 60 dana od dana dostavljanja rešenja.', 'TEXT', N'Pouka o pravnom leku na kraju rešenja'),
 (N'HR',     N'HR.ResenjeSablon',         NULL, 'TEXT', N'Tekst rešenja o korišćenju GO; {Ključ} = podešavanje, [Polje] = podatak iz zahteva; red sa ^ na početku je naslov');

INSERT dbo.Podesavanja (Grupa, Kljuc, Vrednost, TipVrednosti, Opis)
SELECT p.Grupa, p.Kljuc, p.Vrednost, p.Tip, p.Opis FROM @p p
WHERE NOT EXISTS (SELECT 1 FROM dbo.Podesavanja x WHERE x.Kljuc = p.Kljuc);
GO

-- Šablon rešenja (postojeći šablon firme, sa ispravkama iz specifikacije 8.3)
UPDATE dbo.Podesavanja SET Vrednost =
N'{HR.ResenjePravniOsnov} poslodavac {Firma.Naziv}, {Firma.Adresa}, {Firma.Mesto}, matični broj: {Firma.MaticniBroj}, PIB: {Firma.PIB}, koga zastupa direktor {Firma.Direktor}, dana [DatumResenja] godine, donosi sledeće:

^REŠENJE
^O KORIŠĆENJU GODIŠNJEG ODMORA
^Broj: [BrojResenja]

[Zaposleni], iz [Prebivaliste], JMBG: [JMBG], zaposlenom na radnom mestu [RadnoMesto], određuje se korišćenje dela godišnjeg odmora za [Godina]. godinu u trajanju od [Dana] radnih dana.

Godišnji odmor će se koristiti u vremenu od [DatumOd] do [DatumDo] godine.

Zaposleni je u obavezi da se na rad javi [DatumPovratka] godine.

Za vreme korišćenja godišnjeg odmora zaposlenom pripada naknada zarade u visini prosečne zarade ostvarene u prethodnih dvanaest meseci u skladu sa čl. 114 Zakona o radu.

Ovo rešenje postaje konačno danom dostavljanja zaposlenom.

^O b r a z l o ž e n j e

U svakoj kalendarskoj godini zaposleni ima pravo na godišnji odmor u trajanju utvrđenom Pravilnikom o radu poslodavca i ugovorom o radu, a najmanje 20 radnih dana. Zaposleni je u [Godina]. godini ostvario pravo na [DanaGodisnjeg] dana godišnjeg odmora. Do dana donošenja ovog rešenja zaposleni je iskoristio [DanaIskoristio] dana godišnjeg odmora.

Pri utvrđivanju dužine godišnjeg odmora radna nedelja računata je kao pet radnih dana. Praznici koji su neradni dani u skladu sa zakonom, odsustvo sa rada uz naknadu zarade i privremena sprečenost za rad u skladu sa propisima o zdravstvenom osiguranju ne uračunavaju se u dane godišnjeg odmora.

Zaposleni se obaveštava da, u skladu sa čl. 75 st. 4 Zakona o radu, poslodavac može da izmeni vreme određeno za korišćenje godišnjeg odmora ako to zahtevaju potrebe posla, najkasnije pet radnih dana pre dana određenog za korišćenje godišnjeg odmora.

Zaposleni je dana [DatumZahteva] godine dostavio poslodavcu zahtev za korišćenje godišnjeg odmora u trajanju od [Dana] radnih dana, od [DatumOd] do [DatumDo] godine. Poslodavac je utvrdio da se zaposlenom može odobriti korišćenje godišnjeg odmora u navedenom periodu i da je vreme korišćenja godišnjeg odmora zaposlenog u skladu sa Planom (rasporedom) korišćenja godišnjih odmora kod poslodavca.

Pouka o pravnom leku: {HR.ResenjePoukaOPravnomLeku}

Poslodavac:
{Firma.Naziv}
direktor {Firma.Direktor}

Rešenje se zaposlenom dostavlja elektronskim putem. Rešenje je važeće bez potpisa i pečata. Po pisanom zahtevu zaposlenog, rešenje će biti dostavljeno u pisanoj formi.'
WHERE Kljuc = N'HR.ResenjeSablon' AND Vrednost IS NULL;
GO

/* ---------------------------------------------------------------------------------------------
   2. Log HR podataka (isti obrazac kao TerminalPrivremeniLog, uz korisnika i brisanje)
   --------------------------------------------------------------------------------------------- */
IF OBJECT_ID('dbo.HRLog', 'U') IS NULL
    CREATE TABLE dbo.HRLog (
        LogID    int IDENTITY(1,1) NOT NULL CONSTRAINT PK_HRLog PRIMARY KEY,
        Tabela   nvarchar(50)  NOT NULL,
        ZapisID  int           NULL,
        Datum    datetime      NOT NULL CONSTRAINT DF_HRLog_Datum DEFAULT (GETDATE()),
        Akcija   nvarchar(20)  NOT NULL,          -- INSERT / UPDATE / DELETE / OTKLJUCAJ ...
        Opis     nvarchar(max) NULL,              -- POLJE: stara vrednost -> nova vrednost
        Korisnik nvarchar(20)  NULL,
        Racunar  nvarchar(128) NULL CONSTRAINT DF_HRLog_Racunar DEFAULT (HOST_NAME()));
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_HRLog_Tabela' AND object_id = OBJECT_ID('dbo.HRLog'))
    CREATE INDEX IX_HRLog_Tabela ON dbo.HRLog (Tabela, ZapisID, Datum DESC);
GO

-- Razlika dva reda (XML RAW 'r'): "POLJE: staro -> novo; ...". Radi na svakom nivou kompatibilnosti baze.
CREATE OR ALTER FUNCTION dbo.fnHR_LogRazlika (@Staro xml, @Novo xml)
RETURNS nvarchar(max)
AS
BEGIN
    DECLARE @r nvarchar(max);
    ;WITH s AS (SELECT T.N.value('local-name(.)', 'nvarchar(128)') AS k, T.N.value('.', 'nvarchar(max)') AS v FROM @Staro.nodes('/r/@*') T(N)),
          n AS (SELECT T.N.value('local-name(.)', 'nvarchar(128)') AS k, T.N.value('.', 'nvarchar(max)') AS v FROM @Novo.nodes('/r/@*') T(N))
    SELECT @r = STUFF((
        SELECT N'; ' + COALESCE(s.k, n.k) + N': ' + ISNULL(s.v, N'NULL') + N' -> ' + ISNULL(n.v, N'NULL')
        FROM s FULL JOIN n ON n.k = s.k
        WHERE ISNULL(s.v, N'<NULL>') <> ISNULL(n.v, N'<NULL>')
          AND COALESCE(s.k, n.k) NOT IN (N'DatumUnosa', N'DatumIzmene', N'RowVer', N'Datum_x0020_izmene')
        FOR XML PATH(''), TYPE).value('.', 'nvarchar(max)'), 1, 2, N'');
    RETURN @r;
END
GO

/* ---------------------------------------------------------------------------------------------
   3. Hijerarhija zaposleni–nadređeni, raspored, veza korisnik–zaposleni
   --------------------------------------------------------------------------------------------- */
IF COL_LENGTH('dbo.Zaposleni', 'NadredjeniSifra') IS NULL
    ALTER TABLE dbo.Zaposleni ADD NadredjeniSifra int NULL;
IF COL_LENGTH('dbo.Zaposleni', 'RasporedRadaID') IS NULL
    ALTER TABLE dbo.Zaposleni ADD RasporedRadaID int NULL;
GO
IF OBJECT_ID('dbo.CK_Zaposleni_NijeSamSebi', 'C') IS NULL
    ALTER TABLE dbo.Zaposleni WITH NOCHECK ADD CONSTRAINT CK_Zaposleni_NijeSamSebi CHECK (NadredjeniSifra IS NULL OR NadredjeniSifra <> ID);
BEGIN TRY
    IF OBJECT_ID('dbo.FK_Zaposleni_Nadredjeni', 'F') IS NULL
        ALTER TABLE dbo.Zaposleni WITH NOCHECK ADD CONSTRAINT FK_Zaposleni_Nadredjeni FOREIGN KEY (NadredjeniSifra) REFERENCES dbo.Zaposleni (ID);
END TRY BEGIN CATCH PRINT N'FK_Zaposleni_Nadredjeni nije dodat: ' + ERROR_MESSAGE(); END CATCH
GO

-- veza korisnika aplikacije sa zaposlenim: ko je prijavljen = koji je to šef
IF OBJECT_ID('dbo.Korisnici', 'U') IS NOT NULL AND COL_LENGTH('dbo.Korisnici', 'ZaposleniSifra') IS NULL
    ALTER TABLE dbo.Korisnici ADD ZaposleniSifra int NULL;
GO
-- početno popunjavanje iz veze koju je HR modul već imao (Zaposleni.KorisnickoIme)
IF COL_LENGTH('dbo.Korisnici', 'ZaposleniSifra') IS NOT NULL
    EXEC (N'UPDATE k SET ZaposleniSifra = z.ID
            FROM dbo.Korisnici k
            JOIN dbo.Zaposleni z ON LOWER(RTRIM(z.KorisnickoIme)) = LOWER(RTRIM(k.Korisnik))
            WHERE k.ZaposleniSifra IS NULL');
GO

-- Ceo tim jednog šefa: direktni i indirektni podređeni
CREATE OR ALTER FUNCTION dbo.fn_Podredjeni (@Sifra int)
RETURNS TABLE
AS
RETURN
    WITH p AS (
        SELECT z.ID AS Sifra, 1 AS Nivo FROM dbo.Zaposleni z WHERE z.NadredjeniSifra = @Sifra AND z.ID <> @Sifra
        UNION ALL
        SELECT z.ID, p.Nivo + 1 FROM dbo.Zaposleni z JOIN p ON z.NadredjeniSifra = p.Sifra
        WHERE p.Nivo < 30)
    SELECT DISTINCT Sifra FROM p;
GO

/* ---------------------------------------------------------------------------------------------
   4. Šifarnici: vrste rada, praznici, raspored rada
   --------------------------------------------------------------------------------------------- */
IF OBJECT_ID('dbo.VrstaRada', 'U') IS NULL
    CREATE TABLE dbo.VrstaRada (
        ID              int IDENTITY(1,1) NOT NULL CONSTRAINT PK_VrstaRada PRIMARY KEY,
        Oznaka          varchar(5)    NOT NULL CONSTRAINT UQ_VrstaRada_Oznaka UNIQUE,
        Naziv           nvarchar(100) NOT NULL,
        Grupa           varchar(20)   NOT NULL,
        CeoDan          bit NOT NULL CONSTRAINT DF_VrstaRada_CeoDan DEFAULT (0),
        UlaziUFond      bit NOT NULL CONSTRAINT DF_VrstaRada_UlaziUFond DEFAULT (1),
        Placeno         bit NOT NULL CONSTRAINT DF_VrstaRada_Placeno DEFAULT (1),
        ProcenatNaknade decimal(5,2) NULL,
        SamoKrozZahtev  bit NOT NULL CONSTRAINT DF_VrstaRada_SamoKrozZahtev DEFAULT (0),
        Boja            int NULL,
        Redosled        int NULL,
        Aktivno         bit NOT NULL CONSTRAINT DF_VrstaRada_Aktivno DEFAULT (1),
        CONSTRAINT CK_VrstaRada_Grupa CHECK (Grupa IN ('RAD', 'ODSUSTVO', 'PRAZNIK')));
IF NOT EXISTS (SELECT 1 FROM dbo.VrstaRada)
    INSERT dbo.VrstaRada (Oznaka, Naziv, Grupa, CeoDan, UlaziUFond, Placeno, SamoKrozZahtev, Boja, Redosled) VALUES
     ('RR', N'Redovan rad',                    'RAD',      0, 1, 1, 0, NULL,      1),
     ('PR', N'Prekovremeni rad',               'RAD',      0, 1, 1, 0, NULL,      2),
     ('NR', N'Noćni rad',                      'RAD',      0, 0, 1, 0, NULL,      3),
     ('RP', N'Rad na praznik',                 'RAD',      0, 1, 1, 0, NULL,      4),
     ('KS', N'Kašnjenje / ranije napuštanje',  'ODSUSTVO', 0, 0, 0, 0, NULL,      5),
     -- do uključivanja toka preko zahteva (faza 3) GO može da se kuca u matrici; posle je samo kroz zahtev
     ('GO', N'Godišnji odmor',                 'ODSUSTVO', 1, 1, 1, 1, 14939366,  6),  -- svetlozelena #E3F4E6
     ('BO', N'Bolovanje',                      'ODSUSTVO', 1, 1, 1, 0, 16774870,  7),  -- svetložuta #FFF6D6
     ('PL', N'Plaćeno odsustvo',               'ODSUSTVO', 1, 1, 1, 0, 14938107,  8),  -- svetloplava #E3EFFB
     ('NO', N'Neplaćeno odsustvo',             'ODSUSTVO', 1, 0, 0, 0, 15591414,  9),
     ('SD', N'Slobodan dan',                   'ODSUSTVO', 1, 1, 1, 0, 14938107, 10),
     ('SP', N'Službeni put',                   'RAD',      1, 1, 1, 0, 14676977, 11),
     ('IZ', N'Neopravdan izostanak',           'ODSUSTVO', 1, 0, 0, 0, 16507875, 12),  -- svetlocrvena #FBE3E3
     ('DP', N'Državni praznik',                'PRAZNIK',  1, 1, 1, 0, 15133422, 13);  -- siva #E6EAEE
GO

IF OBJECT_ID('dbo.Praznici', 'U') IS NULL
    CREATE TABLE dbo.Praznici (
        Datum   date          NOT NULL CONSTRAINT PK_Praznici PRIMARY KEY,
        Naziv   nvarchar(100) NOT NULL,
        Neradni bit           NOT NULL CONSTRAINT DF_Praznici_Neradni DEFAULT (1));
-- ID je pomoćni broj za ekran šifarnika (ključ ostaje Datum)
IF COL_LENGTH('dbo.Praznici', 'ID') IS NULL
    ALTER TABLE dbo.Praznici ADD ID int IDENTITY(1,1) NOT NULL;
GO

IF OBJECT_ID('dbo.RasporedRada', 'U') IS NULL
    CREATE TABLE dbo.RasporedRada (
        ID      int IDENTITY(1,1) NOT NULL CONSTRAINT PK_RasporedRada PRIMARY KEY,
        Naziv   nvarchar(50) NOT NULL,
        Pon decimal(4,2) NOT NULL CONSTRAINT DF_RasporedRada_Pon DEFAULT (8),
        Uto decimal(4,2) NOT NULL CONSTRAINT DF_RasporedRada_Uto DEFAULT (8),
        Sre decimal(4,2) NOT NULL CONSTRAINT DF_RasporedRada_Sre DEFAULT (8),
        Cet decimal(4,2) NOT NULL CONSTRAINT DF_RasporedRada_Cet DEFAULT (8),
        Pet decimal(4,2) NOT NULL CONSTRAINT DF_RasporedRada_Pet DEFAULT (8),
        Sub decimal(4,2) NOT NULL CONSTRAINT DF_RasporedRada_Sub DEFAULT (0),
        Ned decimal(4,2) NOT NULL CONSTRAINT DF_RasporedRada_Ned DEFAULT (0),
        VremeOd time NULL,
        VremeDo time NULL);
IF NOT EXISTS (SELECT 1 FROM dbo.RasporedRada)
    INSERT dbo.RasporedRada (Naziv, Pon, Uto, Sre, Cet, Pet, Sub, Ned) VALUES
        (N'Pon–Pet 8 h', 8, 8, 8, 8, 8, 0, 0),
        (N'Pon–Sub 7 h', 7, 7, 7, 7, 7, 7, 0);
GO
BEGIN TRY
    IF OBJECT_ID('dbo.FK_Zaposleni_RasporedRada', 'F') IS NULL
        ALTER TABLE dbo.Zaposleni WITH NOCHECK ADD CONSTRAINT FK_Zaposleni_RasporedRada FOREIGN KEY (RasporedRadaID) REFERENCES dbo.RasporedRada (ID);
END TRY BEGIN CATCH PRINT N'FK_Zaposleni_RasporedRada nije dodat: ' + ERROR_MESSAGE(); END CATCH
GO

-- Kalendar perioda: datum, dan u nedelji (0 = ponedeljak … 6 = nedelja), neradni praznik
CREATE OR ALTER FUNCTION dbo.fnHR_Kalendar (@Od date, @Do date)
RETURNS TABLE
AS
RETURN
    WITH e AS (SELECT n FROM (VALUES (0),(1),(2),(3),(4),(5),(6),(7),(8),(9)) v (n)),
         b AS (SELECT a.n + 10 * x.n + 100 * y.n + 1000 * w.n AS n FROM e a CROSS JOIN e x CROSS JOIN e y CROSS JOIN e w)
    SELECT DATEADD(DAY, b.n, @Od) AS Datum,
           DATEDIFF(DAY, '19000101', DATEADD(DAY, b.n, @Od)) % 7 AS DanUNedelji,
           CASE WHEN pr.Datum IS NULL THEN 0 ELSE 1 END AS Praznik
    FROM b
    LEFT JOIN dbo.Praznici pr ON pr.Datum = DATEADD(DAY, b.n, @Od) AND pr.Neradni = 1
    WHERE b.n <= DATEDIFF(DAY, @Od, @Do);
GO

-- Radni dani za GO: ponedeljak–petak bez neradnih praznika (čl. 70: petodnevna radna nedelja)
CREATE OR ALTER FUNCTION dbo.fnHR_RadniDani (@Od date, @Do date)
RETURNS int
AS
BEGIN
    IF @Od IS NULL OR @Do IS NULL OR @Do < @Od RETURN 0;
    RETURN (SELECT COUNT(*) FROM dbo.fnHR_Kalendar(@Od, @Do) WHERE DanUNedelji < 5 AND Praznik = 0);
END
GO

CREATE OR ALTER FUNCTION dbo.fnHR_SledeciRadniDan (@Datum date)
RETURNS date
AS
BEGIN
    RETURN (SELECT MIN(Datum) FROM dbo.fnHR_Kalendar(DATEADD(DAY, 1, @Datum), DATEADD(DAY, 40, @Datum)) WHERE DanUNedelji < 5 AND Praznik = 0);
END
GO

CREATE OR ALTER FUNCTION dbo.fnHR_Podesavanje (@Kljuc nvarchar(100))
RETURNS nvarchar(max)
AS
BEGIN
    RETURN (SELECT Vrednost FROM dbo.Podesavanja WHERE Kljuc = @Kljuc);
END
GO

-- Dnevni fond sati po rasporedu zaposlenog (bez rasporeda: HR.DnevniFondSati od ponedeljka do petka)
CREATE OR ALTER FUNCTION dbo.fnHR_DnevniFond (@Zaposleni int, @Datum date)
RETURNS decimal(5,2)
AS
BEGIN
    DECLARE @dan int = DATEDIFF(DAY, '19000101', @Datum) % 7, @fond decimal(5,2);
    SELECT @fond = CASE @dan WHEN 0 THEN r.Pon WHEN 1 THEN r.Uto WHEN 2 THEN r.Sre WHEN 3 THEN r.Cet
                             WHEN 4 THEN r.Pet WHEN 5 THEN r.Sub ELSE r.Ned END
    FROM dbo.Zaposleni z JOIN dbo.RasporedRada r ON r.ID = z.RasporedRadaID
    WHERE z.ID = @Zaposleni;
    IF @fond IS NULL
        SET @fond = CASE WHEN @dan < 5 THEN ISNULL(TRY_CAST(REPLACE(dbo.fnHR_Podesavanje(N'HR.DnevniFondSati'), ',', '.') AS decimal(5,2)), 8) ELSE 0 END;
    RETURN @fond;
END
GO

/* ---------------------------------------------------------------------------------------------
   5. Evidencija rada i beleške
   --------------------------------------------------------------------------------------------- */
IF OBJECT_ID('dbo.EvidencijaRada', 'U') IS NULL
    CREATE TABLE dbo.EvidencijaRada (
        ID             int IDENTITY(1,1) NOT NULL CONSTRAINT PK_EvidencijaRada PRIMARY KEY,
        ZaposleniSifra int           NOT NULL,
        Datum          date          NOT NULL,
        VrstaRadaID    int           NOT NULL CONSTRAINT FK_EvidencijaRada_Vrsta REFERENCES dbo.VrstaRada (ID),
        Sati           decimal(5,2)  NOT NULL,
        VremeOd        time          NULL,
        VremeDo        time          NULL,
        Napomena       nvarchar(250) NULL,
        Izvor          char(1)       NOT NULL CONSTRAINT DF_EvidencijaRada_Izvor DEFAULT ('R'),  -- G generisano, R ručno, O iz zahteva, P praznik
        DopustStavkaID int           NULL,
        NameOper       char(20)      NULL,
        DatumUnosa     datetime      NOT NULL CONSTRAINT DF_EvidencijaRada_DatumUnosa DEFAULT (GETDATE()),
        CONSTRAINT CK_EvidencijaRada_Izvor CHECK (Izvor IN ('G', 'R', 'O', 'P')));
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_EvidencijaRada_ZapDatum' AND object_id = OBJECT_ID('dbo.EvidencijaRada'))
    CREATE INDEX IX_EvidencijaRada_ZapDatum ON dbo.EvidencijaRada (ZaposleniSifra, Datum);
GO
BEGIN TRY
    IF OBJECT_ID('dbo.FK_EvidencijaRada_Zaposleni', 'F') IS NULL
        ALTER TABLE dbo.EvidencijaRada ADD CONSTRAINT FK_EvidencijaRada_Zaposleni FOREIGN KEY (ZaposleniSifra) REFERENCES dbo.Zaposleni (ID);
END TRY BEGIN CATCH PRINT N'FK_EvidencijaRada_Zaposleni nije dodat: ' + ERROR_MESSAGE(); END CATCH
GO

IF OBJECT_ID('dbo.EvidencijaMesec', 'U') IS NULL
    CREATE TABLE dbo.EvidencijaMesec (
        ZaposleniSifra int      NOT NULL,
        Godina         smallint NOT NULL,
        Mesec          tinyint  NOT NULL,
        Status         tinyint  NOT NULL CONSTRAINT DF_EvidencijaMesec_Status DEFAULT (0),  -- 0 otvoren, 1 potvrdio šef, 2 zaključio HR
        PotvrdioSifra  int      NULL,
        DatumPotvrde   datetime NULL,
        ZakljucioSifra int      NULL,
        DatumZakljucka datetime NULL,
        CONSTRAINT PK_EvidencijaMesec PRIMARY KEY (ZaposleniSifra, Godina, Mesec));
GO

IF OBJECT_ID('dbo.BeleskaORadu', 'U') IS NULL
    CREATE TABLE dbo.BeleskaORadu (
        ID                 int IDENTITY(1,1) NOT NULL CONSTRAINT PK_BeleskaORadu PRIMARY KEY,
        ZaposleniSifra     int           NOT NULL,
        Datum              date          NOT NULL,
        AutorSifra         int           NOT NULL,
        Tip                varchar(20)   NOT NULL CONSTRAINT CK_BeleskaORadu_Tip CHECK (Tip IN ('POHVALA', 'PRIMEDBA', 'UPOZORENJE', 'INFO')),
        Tekst              nvarchar(max) NOT NULL,
        VidljivoZaposlenom bit           NOT NULL CONSTRAINT DF_BeleskaORadu_Vidljivo DEFAULT (0),
        NameOper           char(20)      NULL,
        DatumUnosa         datetime      NOT NULL CONSTRAINT DF_BeleskaORadu_DatumUnosa DEFAULT (GETDATE()));
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_BeleskaORadu_Zap' AND object_id = OBJECT_ID('dbo.BeleskaORadu'))
    CREATE INDEX IX_BeleskaORadu_Zap ON dbo.BeleskaORadu (ZaposleniSifra, Datum);
GO

/* ---------------------------------------------------------------------------------------------
   6. Godišnji odmor: osnovi, pravo po godini, stavke (zahtevi)
   --------------------------------------------------------------------------------------------- */
IF OBJECT_ID('dbo.OsnovGO', 'U') IS NULL
    CREATE TABLE dbo.OsnovGO (
        ID       int IDENTITY(1,1) NOT NULL CONSTRAINT PK_OsnovGO PRIMARY KEY,
        Oznaka   varchar(10)   NOT NULL CONSTRAINT UQ_OsnovGO_Oznaka UNIQUE,
        Naziv    nvarchar(100) NOT NULL,
        BrojDana smallint      NOT NULL,
        Osnovni  bit           NOT NULL CONSTRAINT DF_OsnovGO_Osnovni DEFAULT (0),
        Aktivno  bit           NOT NULL CONSTRAINT DF_OsnovGO_Aktivno DEFAULT (1),
        Napomena nvarchar(250) NULL);
IF NOT EXISTS (SELECT 1 FROM dbo.OsnovGO)
    INSERT dbo.OsnovGO (Oznaka, Naziv, BrojDana, Osnovni, Napomena) VALUES
        ('OSN',  N'Zakonski minimum', 20, 1, N'Dodeljuje se svima'),
        ('STAZ', N'Radno iskustvo', 0, 0, N'Po pravilniku firme'),
        ('USL',  N'Uslovi rada', 0, 0, N'Po pravilniku firme'),
        ('SS',   N'Stručna sprema', 0, 0, N'Po pravilniku firme'),
        ('DOP',  N'Doprinos na radu', 0, 0, N'Po pravilniku firme'),
        ('OST',  N'Drugi osnov (ugovor, pravilnik)', 2, 0, NULL);
GO

IF OBJECT_ID('dbo.ZaposleniOsnovGO', 'U') IS NULL
    CREATE TABLE dbo.ZaposleniOsnovGO (
        ID             int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ZaposleniOsnovGO PRIMARY KEY,
        ZaposleniSifra int           NOT NULL,
        OsnovGOID      int           NOT NULL CONSTRAINT FK_ZaposleniOsnovGO_Osnov REFERENCES dbo.OsnovGO (ID),
        BrojDana       smallint      NOT NULL,
        VaziOd         date          NOT NULL,
        VaziDo         date          NULL,
        Napomena       nvarchar(250) NULL,
        CONSTRAINT CK_ZaposleniOsnovGO_Period CHECK (VaziDo IS NULL OR VaziDo >= VaziOd));
GO

IF OBJECT_ID('dbo.GODopust', 'U') IS NULL
    CREATE TABLE dbo.GODopust (
        ID             int IDENTITY(1,1) NOT NULL CONSTRAINT PK_GODopust PRIMARY KEY,
        ZaposleniSifra int      NOT NULL,
        Godina         smallint NOT NULL,
        UkupnoDana     smallint NOT NULL,
        PotrosenoDana  smallint NOT NULL CONSTRAINT DF_GODopust_Potroseno DEFAULT (0),
        PreostaloDana  AS (UkupnoDana - PotrosenoDana),
        RokKoriscenja  date     NOT NULL,
        Srazmerno      bit      NOT NULL CONSTRAINT DF_GODopust_Srazmerno DEFAULT (0),
        MeseciRada     tinyint  NULL,
        NameOper       char(20) NULL,
        Datum          datetime NOT NULL CONSTRAINT DF_GODopust_Datum DEFAULT (GETDATE()),
        CONSTRAINT UQ_GODopust_ZapGodina UNIQUE (ZaposleniSifra, Godina));

IF OBJECT_ID('dbo.GODopustOsnov', 'U') IS NULL
    CREATE TABLE dbo.GODopustOsnov (
        ID        int IDENTITY(1,1) NOT NULL CONSTRAINT PK_GODopustOsnov PRIMARY KEY,
        DopustID  int      NOT NULL CONSTRAINT FK_GODopustOsnov_Dopust REFERENCES dbo.GODopust (ID) ON DELETE CASCADE,
        OsnovGOID int      NOT NULL CONSTRAINT FK_GODopustOsnov_Osnov REFERENCES dbo.OsnovGO (ID),
        BrojDana  smallint NOT NULL);

IF OBJECT_ID('dbo.StatusZahteva', 'U') IS NULL
    CREATE TABLE dbo.StatusZahteva (
        ID    tinyint      NOT NULL CONSTRAINT PK_StatusZahteva PRIMARY KEY,
        Naziv nvarchar(30) NOT NULL);
IF NOT EXISTS (SELECT 1 FROM dbo.StatusZahteva)
    INSERT dbo.StatusZahteva (ID, Naziv) VALUES (1, N'Nacrt'), (2, N'Podnet'), (3, N'Odobren'), (4, N'Odbijen'), (5, N'Otkazan'), (6, N'Iskorišćen');

IF OBJECT_ID('dbo.GODopustStavke', 'U') IS NULL
    CREATE TABLE dbo.GODopustStavke (
        ID                 int IDENTITY(1,1) NOT NULL CONSTRAINT PK_GODopustStavke PRIMARY KEY,
        DopustID           int           NOT NULL CONSTRAINT FK_GODopustStavke_Dopust REFERENCES dbo.GODopust (ID),
        DatumOd            date          NOT NULL,
        DatumDo            date          NOT NULL,
        BrojDana           smallint      NOT NULL,
        DatumPovratka      date          NULL,
        Razlog             nvarchar(100) NULL,
        Napomena           nvarchar(500) NULL,
        StatusID           tinyint       NOT NULL CONSTRAINT DF_GODopustStavke_Status DEFAULT (1) CONSTRAINT FK_GODopustStavke_Status REFERENCES dbo.StatusZahteva (ID),
        DatumZahteva       datetime      NOT NULL CONSTRAINT DF_GODopustStavke_DatumZahteva DEFAULT (GETDATE()),
        PodneoSifra        int           NULL,
        Kanal              char(1)       NOT NULL CONSTRAINT DF_GODopustStavke_Kanal DEFAULT ('M'),
        OdobrioSifra       int           NULL,
        DatumOdobrenja     datetime      NULL,
        RazlogOdbijanja    nvarchar(250) NULL,
        BrojResenja        nvarchar(30)  NULL,
        DatumResenja       date          NULL,
        PutanjaResenja     nvarchar(400) NULL,
        ResenjePoslatoMail bit           NOT NULL CONSTRAINT DF_GODopustStavke_Mail DEFAULT (0),
        DatumSlanjaMaila   datetime      NULL,
        ResenjeStampano    bit           NOT NULL CONSTRAINT DF_GODopustStavke_Stampano DEFAULT (0),
        NameOper           char(20)      NULL,
        DatumIzmene        datetime      NULL,
        CONSTRAINT CK_GODopustStavke_Period CHECK (DatumDo >= DatumOd),
        CONSTRAINT CK_GODopustStavke_Kanal CHECK (Kanal IN ('M', 'A')));
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_GODopustStavke_Dopust' AND object_id = OBJECT_ID('dbo.GODopustStavke'))
    CREATE INDEX IX_GODopustStavke_Dopust ON dbo.GODopustStavke (DopustID, StatusID);

IF OBJECT_ID('dbo.GOBrojacResenja', 'U') IS NULL
    CREATE TABLE dbo.GOBrojacResenja (
        Godina    smallint NOT NULL CONSTRAINT PK_GOBrojacResenja PRIMARY KEY,
        Poslednji int      NOT NULL);
GO
BEGIN TRY
    IF OBJECT_ID('dbo.FK_GODopust_Zaposleni', 'F') IS NULL
        ALTER TABLE dbo.GODopust ADD CONSTRAINT FK_GODopust_Zaposleni FOREIGN KEY (ZaposleniSifra) REFERENCES dbo.Zaposleni (ID);
    IF OBJECT_ID('dbo.FK_ZaposleniOsnovGO_Zaposleni', 'F') IS NULL
        ALTER TABLE dbo.ZaposleniOsnovGO ADD CONSTRAINT FK_ZaposleniOsnovGO_Zaposleni FOREIGN KEY (ZaposleniSifra) REFERENCES dbo.Zaposleni (ID);
    IF OBJECT_ID('dbo.FK_BeleskaORadu_Zaposleni', 'F') IS NULL
        ALTER TABLE dbo.BeleskaORadu ADD CONSTRAINT FK_BeleskaORadu_Zaposleni FOREIGN KEY (ZaposleniSifra) REFERENCES dbo.Zaposleni (ID);
END TRY BEGIN CATCH PRINT N'FK ka Zaposleni nije dodat: ' + ERROR_MESSAGE(); END CATCH
GO

-- osnov OSN za postojeće zaposlene koji ga nemaju
INSERT dbo.ZaposleniOsnovGO (ZaposleniSifra, OsnovGOID, BrojDana, VaziOd, Napomena)
SELECT z.ID, o.ID, o.BrojDana, ISNULL(z.DatumUlaska, '20000101'), N'Dodeljeno skriptom'
FROM dbo.Zaposleni z
CROSS JOIN dbo.OsnovGO o
WHERE o.Oznaka = 'OSN'
  AND NOT EXISTS (SELECT 1 FROM dbo.ZaposleniOsnovGO x WHERE x.ZaposleniSifra = z.ID AND x.OsnovGOID = o.ID);
GO

/* ---------------------------------------------------------------------------------------------
   7. Okidači: log promena, zaštita od kruga u hijerarhiji, OSN za novog zaposlenog
   --------------------------------------------------------------------------------------------- */
CREATE OR ALTER TRIGGER dbo.trg_Zaposleni_HR
ON dbo.Zaposleni
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF UPDATE(NadredjeniSifra)
    BEGIN
        -- novi nadređeni ne sme biti već podređen tom zaposlenom (A -> B -> A)
        IF EXISTS (SELECT 1 FROM inserted i
                   WHERE i.NadredjeniSifra IS NOT NULL
                     AND (i.NadredjeniSifra = i.ID OR i.NadredjeniSifra IN (SELECT Sifra FROM dbo.fn_Podredjeni(i.ID))))
            THROW 50101, N'Izabrani nadređeni je već podređen ovom zaposlenom (krug u hijerarhiji).', 1;

        INSERT dbo.HRLog (Tabela, ZapisID, Akcija, Opis, Korisnik)
        SELECT N'Zaposleni', i.ID, N'NADREDJENI',
               N'NadredjeniSifra: ' + ISNULL(CAST(d.NadredjeniSifra AS nvarchar(10)), N'NULL') + N' -> ' + ISNULL(CAST(i.NadredjeniSifra AS nvarchar(10)), N'NULL'),
               CONVERT(nvarchar(20), SESSION_CONTEXT(N'Korisnik'))
        FROM inserted i JOIN deleted d ON d.ID = i.ID
        WHERE ISNULL(d.NadredjeniSifra, -1) <> ISNULL(i.NadredjeniSifra, -1);
    END
    -- osnov OSN se ne upisuje ovde (red u drugoj tabeli bi promenio @@IDENTITY postojećih procedura za unos zaposlenog);
    -- dodeljuje ga forma zaposlenog pri prvom snimanju i sp_GO_Generisi za svakog ko ga nema
END
GO

-- Osnov OSN (zakonski minimum) za zaposlene koji ga nemaju
CREATE OR ALTER PROCEDURE dbo.sp_GO_DodeliOsnovni @ZaposleniSifra int = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT dbo.ZaposleniOsnovGO (ZaposleniSifra, OsnovGOID, BrojDana, VaziOd)
    SELECT z.ID, o.ID, o.BrojDana, ISNULL(z.DatumUlaska, CAST(GETDATE() AS date))
    FROM dbo.Zaposleni z CROSS JOIN dbo.OsnovGO o
    WHERE o.Osnovni = 1 AND o.Aktivno = 1
      AND (@ZaposleniSifra IS NULL OR z.ID = @ZaposleniSifra)
      AND NOT EXISTS (SELECT 1 FROM dbo.ZaposleniOsnovGO x WHERE x.ZaposleniSifra = z.ID AND x.OsnovGOID = o.ID);
END
GO

CREATE OR ALTER TRIGGER dbo.trg_EvidencijaRada_Log ON dbo.EvidencijaRada AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    -- generisani redovi (Popuni mesec) se ne loguju pojedinačno; loguje se ručni unos, GO iz zahteva i brisanje ručnog unosa
    INSERT dbo.HRLog (Tabela, ZapisID, Akcija, Opis, Korisnik)
    SELECT N'EvidencijaRada', COALESCE(i.ID, d.ID),
           CASE WHEN i.ID IS NULL THEN N'DELETE' WHEN d.ID IS NULL THEN N'INSERT' ELSE N'UPDATE' END,
           N'Zaposleni ' + CAST(COALESCE(i.ZaposleniSifra, d.ZaposleniSifra) AS nvarchar(10)) + N', ' + CONVERT(nvarchar(10), COALESCE(i.Datum, d.Datum), 104) + N': '
             + ISNULL(dbo.fnHR_LogRazlika((SELECT * FROM deleted x WHERE x.ID = d.ID FOR XML RAW('r'), TYPE),
                                          (SELECT * FROM inserted x WHERE x.ID = i.ID FOR XML RAW('r'), TYPE)), N''),
           CONVERT(nvarchar(20), SESSION_CONTEXT(N'Korisnik'))
    FROM inserted i FULL JOIN deleted d ON d.ID = i.ID
    WHERE COALESCE(i.Izvor, d.Izvor) IN ('R', 'O');
END
GO

CREATE OR ALTER TRIGGER dbo.trg_GODopust_Log ON dbo.GODopust AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT dbo.HRLog (Tabela, ZapisID, Akcija, Opis, Korisnik)
    SELECT N'GODopust', COALESCE(i.ID, d.ID),
           CASE WHEN i.ID IS NULL THEN N'DELETE' WHEN d.ID IS NULL THEN N'INSERT' ELSE N'UPDATE' END,
           dbo.fnHR_LogRazlika((SELECT * FROM deleted x WHERE x.ID = d.ID FOR XML RAW('r'), TYPE),
                               (SELECT * FROM inserted x WHERE x.ID = i.ID FOR XML RAW('r'), TYPE)),
           CONVERT(nvarchar(20), SESSION_CONTEXT(N'Korisnik'))
    FROM inserted i FULL JOIN deleted d ON d.ID = i.ID;
END
GO

CREATE OR ALTER TRIGGER dbo.trg_GODopustStavke_Log ON dbo.GODopustStavke AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT dbo.HRLog (Tabela, ZapisID, Akcija, Opis, Korisnik)
    SELECT N'GODopustStavke', COALESCE(i.ID, d.ID),
           CASE WHEN i.ID IS NULL THEN N'DELETE' WHEN d.ID IS NULL THEN N'INSERT' ELSE N'UPDATE' END,
           dbo.fnHR_LogRazlika((SELECT * FROM deleted x WHERE x.ID = d.ID FOR XML RAW('r'), TYPE),
                               (SELECT * FROM inserted x WHERE x.ID = i.ID FOR XML RAW('r'), TYPE)),
           CONVERT(nvarchar(20), SESSION_CONTEXT(N'Korisnik'))
    FROM inserted i FULL JOIN deleted d ON d.ID = i.ID;
END
GO

CREATE OR ALTER TRIGGER dbo.trg_ZaposleniOsnovGO_Log ON dbo.ZaposleniOsnovGO AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT dbo.HRLog (Tabela, ZapisID, Akcija, Opis, Korisnik)
    SELECT N'ZaposleniOsnovGO', COALESCE(i.ID, d.ID),
           CASE WHEN i.ID IS NULL THEN N'DELETE' WHEN d.ID IS NULL THEN N'INSERT' ELSE N'UPDATE' END,
           dbo.fnHR_LogRazlika((SELECT * FROM deleted x WHERE x.ID = d.ID FOR XML RAW('r'), TYPE),
                               (SELECT * FROM inserted x WHERE x.ID = i.ID FOR XML RAW('r'), TYPE)),
           CONVERT(nvarchar(20), SESSION_CONTEXT(N'Korisnik'))
    FROM inserted i FULL JOIN deleted d ON d.ID = i.ID;
END
GO

CREATE OR ALTER TRIGGER dbo.trg_BeleskaORadu_Log ON dbo.BeleskaORadu AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT dbo.HRLog (Tabela, ZapisID, Akcija, Opis, Korisnik)
    SELECT N'BeleskaORadu', COALESCE(i.ID, d.ID),
           CASE WHEN i.ID IS NULL THEN N'DELETE' WHEN d.ID IS NULL THEN N'INSERT' ELSE N'UPDATE' END,
           dbo.fnHR_LogRazlika((SELECT * FROM deleted x WHERE x.ID = d.ID FOR XML RAW('r'), TYPE),
                               (SELECT * FROM inserted x WHERE x.ID = i.ID FOR XML RAW('r'), TYPE)),
           CONVERT(nvarchar(20), SESSION_CONTEXT(N'Korisnik'))
    FROM inserted i FULL JOIN deleted d ON d.ID = i.ID;
END
GO

-- Podešavanja: lozinke se loguju bez vrednosti
CREATE OR ALTER TRIGGER dbo.trg_Podesavanja_Log ON dbo.Podesavanja AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT dbo.HRLog (Tabela, ZapisID, Akcija, Opis, Korisnik)
    SELECT N'Podesavanja', COALESCE(i.ID, d.ID),
           CASE WHEN i.ID IS NULL THEN N'DELETE' WHEN d.ID IS NULL THEN N'INSERT' ELSE N'UPDATE' END,
           COALESCE(i.Kljuc, d.Kljuc) + N': ' +
           CASE WHEN COALESCE(i.TipVrednosti, d.TipVrednosti) = 'PASSWORD' THEN N'lozinka je promenjena'
                ELSE ISNULL(d.Vrednost, N'NULL') + N' -> ' + ISNULL(i.Vrednost, N'NULL') END,
           CONVERT(nvarchar(20), SESSION_CONTEXT(N'Korisnik'))
    FROM inserted i FULL JOIN deleted d ON d.ID = i.ID
    WHERE ISNULL(i.Vrednost, N'<NULL>') <> ISNULL(d.Vrednost, N'<NULL>') OR i.ID IS NULL OR d.ID IS NULL;
END
GO

/* ---------------------------------------------------------------------------------------------
   8. Procedure: evidencija rada
   --------------------------------------------------------------------------------------------- */

-- Popuni mesec: redovan rad po rasporedu, praznici i odobreni GO; ručni unos i zaključan mesec se ne diraju
CREATE OR ALTER PROCEDURE dbo.sp_EvidencijaPopuniMesec
    @Godina smallint, @Mesec tinyint, @SefSifra int = NULL, @ZaposleniSifra int = NULL, @Korisnik nvarchar(20) = NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    DECLARE @Od date = DATEFROMPARTS(@Godina, @Mesec, 1);
    DECLARE @Do date = EOMONTH(@Od);
    DECLARE @RR int = (SELECT ID FROM dbo.VrstaRada WHERE Oznaka = 'RR'),
            @DP int = (SELECT ID FROM dbo.VrstaRada WHERE Oznaka = 'DP'),
            @GO int = (SELECT ID FROM dbo.VrstaRada WHERE Oznaka = 'GO');
    IF @RR IS NULL OR @DP IS NULL OR @GO IS NULL
        THROW 50201, N'U šifarniku vrsta rada nedostaje RR, DP ili GO.', 1;

    CREATE TABLE #z (Sifra int PRIMARY KEY, Ulazak date NULL, Izlazak date NULL);
    INSERT #z (Sifra, Ulazak, Izlazak)
    SELECT z.ID, z.DatumUlaska, z.DatumIzlaska
    FROM dbo.Zaposleni z
    WHERE (@ZaposleniSifra IS NULL OR z.ID = @ZaposleniSifra)
      AND (@SefSifra IS NULL OR z.ID IN (SELECT Sifra FROM dbo.fn_Podredjeni(@SefSifra)))
      AND (z.Status = 'A' OR z.DatumIzlaska >= @Od)
      AND (z.DatumUlaska IS NULL OR z.DatumUlaska <= @Do)
      AND (z.DatumIzlaska IS NULL OR z.DatumIzlaska >= @Od);

    -- zaključen mesec se ne dira
    DELETE z FROM #z z
    WHERE EXISTS (SELECT 1 FROM dbo.EvidencijaMesec m WHERE m.ZaposleniSifra = z.Sifra AND m.Godina = @Godina AND m.Mesec = @Mesec AND m.Status = 2);

    SELECT z.Sifra, k.Datum, k.DanUNedelji, k.Praznik, dbo.fnHR_DnevniFond(z.Sifra, k.Datum) AS Fond
    INTO #d
    FROM #z z CROSS JOIN dbo.fnHR_Kalendar(@Od, @Do) k
    WHERE (z.Ulazak IS NULL OR k.Datum >= z.Ulazak) AND (z.Izlazak IS NULL OR k.Datum <= z.Izlazak);

    -- 1. dan sa ručnim unosom se preskače
    DELETE d FROM #d d
    WHERE EXISTS (SELECT 1 FROM dbo.EvidencijaRada e WHERE e.ZaposleniSifra = d.Sifra AND e.Datum = d.Datum AND e.Izvor = 'R');

    BEGIN TRAN;
    INSERT dbo.EvidencijaMesec (ZaposleniSifra, Godina, Mesec)
    SELECT z.Sifra, @Godina, @Mesec FROM #z z
    WHERE NOT EXISTS (SELECT 1 FROM dbo.EvidencijaMesec m WHERE m.ZaposleniSifra = z.Sifra AND m.Godina = @Godina AND m.Mesec = @Mesec);

    DELETE e FROM dbo.EvidencijaRada e
    JOIN #d d ON d.Sifra = e.ZaposleniSifra AND d.Datum = e.Datum
    WHERE e.Izvor IN ('G', 'P', 'O');

    -- 2. neradni praznik: DP sa dnevnim fondom tog dana
    INSERT dbo.EvidencijaRada (ZaposleniSifra, Datum, VrstaRadaID, Sati, Izvor, NameOper)
    SELECT d.Sifra, d.Datum, @DP, d.Fond, 'P', @Korisnik FROM #d d WHERE d.Praznik = 1 AND d.Fond > 0;

    -- 3. odobren godišnji odmor (radni dan po petodnevnoj nedelji)
    INSERT dbo.EvidencijaRada (ZaposleniSifra, Datum, VrstaRadaID, Sati, Izvor, DopustStavkaID, NameOper)
    SELECT d.Sifra, d.Datum, @GO, CASE WHEN d.Fond > 0 THEN d.Fond ELSE ISNULL(TRY_CAST(REPLACE(dbo.fnHR_Podesavanje(N'HR.DnevniFondSati'), ',', '.') AS decimal(5,2)), 8) END,
           'O', s.ID, @Korisnik
    FROM #d d
    CROSS APPLY (SELECT TOP (1) st.ID FROM dbo.GODopustStavke st JOIN dbo.GODopust dp ON dp.ID = st.DopustID
                 WHERE dp.ZaposleniSifra = d.Sifra AND st.StatusID IN (3, 6) AND d.Datum BETWEEN st.DatumOd AND st.DatumDo
                 ORDER BY st.ID) s
    WHERE d.Praznik = 0 AND d.DanUNedelji < 5;

    -- 4. redovan rad po rasporedu
    INSERT dbo.EvidencijaRada (ZaposleniSifra, Datum, VrstaRadaID, Sati, Izvor, NameOper)
    SELECT d.Sifra, d.Datum, @RR, d.Fond, 'G', @Korisnik
    FROM #d d
    WHERE d.Praznik = 0 AND d.Fond > 0
      AND NOT EXISTS (SELECT 1 FROM dbo.EvidencijaRada e WHERE e.ZaposleniSifra = d.Sifra AND e.Datum = d.Datum AND e.Izvor = 'O');
    COMMIT;

    SELECT COUNT(*) AS Zaposlenih FROM #z;
END
GO

-- Upis jednog dana iz matrice (ručni unos). Bez oznaka: briše se ručni unos tog dana.
CREATE OR ALTER PROCEDURE dbo.sp_EvidencijaUpisDan
    @ZaposleniSifra int, @Datum date,
    @Oznaka1 varchar(5) = NULL, @Sati1 decimal(5,2) = NULL,
    @Oznaka2 varchar(5) = NULL, @Sati2 decimal(5,2) = NULL,
    @Napomena nvarchar(250) = NULL, @Korisnik nvarchar(20) = NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    IF EXISTS (SELECT 1 FROM dbo.EvidencijaMesec WHERE ZaposleniSifra = @ZaposleniSifra AND Godina = YEAR(@Datum) AND Mesec = MONTH(@Datum) AND Status = 2)
        THROW 50210, N'Mesec je zaključen; izmene su moguće tek posle otključavanja.', 1;
    IF EXISTS (SELECT 1 FROM dbo.EvidencijaRada WHERE ZaposleniSifra = @ZaposleniSifra AND Datum = @Datum AND Izvor = 'O')
        THROW 50211, N'Dan je pokriven odobrenim godišnjim odmorom; izmena ide kroz zahtev (otkazivanje ili prekid).', 1;

    DECLARE @V1 int, @V2 int, @Zahtev1 bit, @Zahtev2 bit;
    IF @Oznaka1 IS NOT NULL
    BEGIN
        SELECT @V1 = ID, @Zahtev1 = SamoKrozZahtev FROM dbo.VrstaRada WHERE Oznaka = @Oznaka1 AND Aktivno = 1;
        IF @V1 IS NULL THROW 50212, N'Nepoznata oznaka vrste rada.', 1;
    END
    IF @Oznaka2 IS NOT NULL
    BEGIN
        SELECT @V2 = ID, @Zahtev2 = SamoKrozZahtev FROM dbo.VrstaRada WHERE Oznaka = @Oznaka2 AND Aktivno = 1;
        IF @V2 IS NULL THROW 50212, N'Nepoznata oznaka vrste rada.', 1;
    END
    IF @Zahtev1 = 1 OR @Zahtev2 = 1
        THROW 50213, N'Ova vrsta se ne kuca u matrici; unosi se kroz zahtev (godišnji odmor).', 1;

    BEGIN TRAN;
    IF NOT EXISTS (SELECT 1 FROM dbo.EvidencijaMesec WHERE ZaposleniSifra = @ZaposleniSifra AND Godina = YEAR(@Datum) AND Mesec = MONTH(@Datum))
        INSERT dbo.EvidencijaMesec (ZaposleniSifra, Godina, Mesec) VALUES (@ZaposleniSifra, YEAR(@Datum), MONTH(@Datum));

    IF @V1 IS NULL
        DELETE dbo.EvidencijaRada WHERE ZaposleniSifra = @ZaposleniSifra AND Datum = @Datum AND Izvor = 'R';
    ELSE
    BEGIN
        DELETE dbo.EvidencijaRada WHERE ZaposleniSifra = @ZaposleniSifra AND Datum = @Datum AND Izvor IN ('G', 'P', 'R');
        INSERT dbo.EvidencijaRada (ZaposleniSifra, Datum, VrstaRadaID, Sati, Napomena, Izvor, NameOper)
        VALUES (@ZaposleniSifra, @Datum, @V1, ISNULL(@Sati1, 0), @Napomena, 'R', @Korisnik);
        IF @V2 IS NOT NULL AND ISNULL(@Sati2, 0) > 0
            INSERT dbo.EvidencijaRada (ZaposleniSifra, Datum, VrstaRadaID, Sati, Izvor, NameOper)
            VALUES (@ZaposleniSifra, @Datum, @V2, @Sati2, 'R', @Korisnik);
    END
    COMMIT;
END
GO

-- Jedna stavka dana (prozor "Dan"): novo ili izmena ručnog unosa
CREATE OR ALTER PROCEDURE dbo.sp_EvidencijaStavkaSnimi
    @ID int, @ZaposleniSifra int, @Datum date, @VrstaRadaID int, @Sati decimal(5,2),
    @VremeOd time = NULL, @VremeDo time = NULL, @Napomena nvarchar(250) = NULL, @Korisnik nvarchar(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM dbo.EvidencijaMesec WHERE ZaposleniSifra = @ZaposleniSifra AND Godina = YEAR(@Datum) AND Mesec = MONTH(@Datum) AND Status = 2)
        THROW 50210, N'Mesec je zaključen; izmene su moguće tek posle otključavanja.', 1;
    IF EXISTS (SELECT 1 FROM dbo.VrstaRada WHERE ID = @VrstaRadaID AND SamoKrozZahtev = 1)
        THROW 50213, N'Ova vrsta se ne kuca u matrici; unosi se kroz zahtev (godišnji odmor).', 1;
    IF ISNULL(@ID, 0) > 0 AND EXISTS (SELECT 1 FROM dbo.EvidencijaRada WHERE ID = @ID AND Izvor = 'O')
        THROW 50211, N'Stavka je iz odobrenog godišnjeg odmora; menja se kroz zahtev.', 1;

    IF ISNULL(@ID, 0) = 0
    BEGIN
        -- prvi ručni unos dana zamenjuje generisani rad
        DELETE dbo.EvidencijaRada WHERE ZaposleniSifra = @ZaposleniSifra AND Datum = @Datum AND Izvor IN ('G', 'P');
        INSERT dbo.EvidencijaRada (ZaposleniSifra, Datum, VrstaRadaID, Sati, VremeOd, VremeDo, Napomena, Izvor, NameOper)
        VALUES (@ZaposleniSifra, @Datum, @VrstaRadaID, @Sati, @VremeOd, @VremeDo, @Napomena, 'R', @Korisnik);
    END
    ELSE
        UPDATE dbo.EvidencijaRada
        SET VrstaRadaID = @VrstaRadaID, Sati = @Sati, VremeOd = @VremeOd, VremeDo = @VremeDo, Napomena = @Napomena, Izvor = 'R', NameOper = @Korisnik
        WHERE ID = @ID;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_EvidencijaStavkaObrisi @ID int
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Z int, @D date, @I char(1);
    SELECT @Z = ZaposleniSifra, @D = Datum, @I = Izvor FROM dbo.EvidencijaRada WHERE ID = @ID;
    IF @I = 'O' THROW 50211, N'Stavka je iz odobrenog godišnjeg odmora; menja se kroz zahtev.', 1;
    IF EXISTS (SELECT 1 FROM dbo.EvidencijaMesec WHERE ZaposleniSifra = @Z AND Godina = YEAR(@D) AND Mesec = MONTH(@D) AND Status = 2)
        THROW 50210, N'Mesec je zaključen; izmene su moguće tek posle otključavanja.', 1;
    DELETE dbo.EvidencijaRada WHERE ID = @ID;
END
GO

-- Status meseca: 1 = potvrdio šef, 2 = zaključio HR, 0 = otključano (beleži se u log)
CREATE OR ALTER PROCEDURE dbo.sp_EvidencijaMesecStatus
    @ZaposleniSifra int, @Godina smallint, @Mesec tinyint, @Status tinyint, @KorisnikSifra int = NULL, @Korisnik nvarchar(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM dbo.EvidencijaMesec WHERE ZaposleniSifra = @ZaposleniSifra AND Godina = @Godina AND Mesec = @Mesec)
        INSERT dbo.EvidencijaMesec (ZaposleniSifra, Godina, Mesec) VALUES (@ZaposleniSifra, @Godina, @Mesec);
    DECLARE @Staro tinyint = (SELECT Status FROM dbo.EvidencijaMesec WHERE ZaposleniSifra = @ZaposleniSifra AND Godina = @Godina AND Mesec = @Mesec);
    IF @Status = 1 AND @Staro = 2
        THROW 50220, N'Mesec je zaključen; potvrda nije moguća.', 1;

    UPDATE dbo.EvidencijaMesec
    SET Status = @Status,
        PotvrdioSifra  = CASE WHEN @Status = 1 THEN @KorisnikSifra ELSE PotvrdioSifra END,
        DatumPotvrde   = CASE WHEN @Status = 1 THEN GETDATE() ELSE DatumPotvrde END,
        ZakljucioSifra = CASE WHEN @Status = 2 THEN @KorisnikSifra ELSE ZakljucioSifra END,
        DatumZakljucka = CASE WHEN @Status = 2 THEN GETDATE() ELSE DatumZakljucka END
    WHERE ZaposleniSifra = @ZaposleniSifra AND Godina = @Godina AND Mesec = @Mesec;

    IF @Staro = 2 AND @Status < 2
        INSERT dbo.HRLog (Tabela, ZapisID, Akcija, Opis, Korisnik)
        VALUES (N'EvidencijaMesec', @ZaposleniSifra, N'OTKLJUCAJ',
                N'Mesec ' + CAST(@Mesec AS nvarchar(2)) + N'/' + CAST(@Godina AS nvarchar(4)) + N': Status 2 -> ' + CAST(@Status AS nvarchar(1)), @Korisnik);
    ELSE IF ISNULL(@Staro, 0) <> @Status
        INSERT dbo.HRLog (Tabela, ZapisID, Akcija, Opis, Korisnik)
        VALUES (N'EvidencijaMesec', @ZaposleniSifra, CASE @Status WHEN 1 THEN N'POTVRDI' WHEN 2 THEN N'ZAKLJUCI' ELSE N'STATUS' END,
                N'Mesec ' + CAST(@Mesec AS nvarchar(2)) + N'/' + CAST(@Godina AS nvarchar(4)) + N': Status ' + CAST(ISNULL(@Staro, 0) AS nvarchar(1)) + N' -> ' + CAST(@Status AS nvarchar(1)), @Korisnik);
END
GO

-- Zbir po zaposlenom i vrsti rada za obračun zarada
CREATE OR ALTER VIEW dbo.vw_EvidencijaZaObracun
AS
SELECT e.ZaposleniSifra, YEAR(e.Datum) AS Godina, MONTH(e.Datum) AS Mesec,
       v.ID AS VrstaRadaID, v.Oznaka, v.Naziv, v.Grupa, v.Placeno, v.ProcenatNaknade,
       SUM(e.Sati) AS Sati, COUNT(DISTINCT e.Datum) AS Dani
FROM dbo.EvidencijaRada e
JOIN dbo.VrstaRada v ON v.ID = e.VrstaRadaID
GROUP BY e.ZaposleniSifra, YEAR(e.Datum), MONTH(e.Datum), v.ID, v.Oznaka, v.Naziv, v.Grupa, v.Placeno, v.ProcenatNaknade;
GO

-- Beleška o radu: piše samo nadređeni iz lanca iznad zaposlenog ili HR; menja i briše samo autor
CREATE OR ALTER PROCEDURE dbo.sp_BeleskaSnimi
    @ID int, @ZaposleniSifra int, @Datum date, @AutorSifra int, @Tip varchar(20), @Tekst nvarchar(max),
    @VidljivoZaposlenom bit = 0, @HR bit = 0, @Korisnik nvarchar(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF ISNULL(@AutorSifra, 0) = 0
        THROW 50230, N'Prijavljeni korisnik nije povezan sa zaposlenim (Korisnici.ZaposleniSifra); beleška ne može da se upiše.', 1;
    IF @HR = 0 AND @ZaposleniSifra NOT IN (SELECT Sifra FROM dbo.fn_Podredjeni(@AutorSifra))
        THROW 50231, N'Belešku piše samo nadređeni iz lanca iznad zaposlenog ili HR.', 1;
    IF ISNULL(@ID, 0) = 0
        INSERT dbo.BeleskaORadu (ZaposleniSifra, Datum, AutorSifra, Tip, Tekst, VidljivoZaposlenom, NameOper)
        VALUES (@ZaposleniSifra, @Datum, @AutorSifra, @Tip, @Tekst, @VidljivoZaposlenom, @Korisnik);
    ELSE
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM dbo.BeleskaORadu WHERE ID = @ID AND AutorSifra = @AutorSifra)
            THROW 50232, N'Belešku menja samo njen autor.', 1;
        UPDATE dbo.BeleskaORadu SET Datum = @Datum, Tip = @Tip, Tekst = @Tekst, VidljivoZaposlenom = @VidljivoZaposlenom, NameOper = @Korisnik
        WHERE ID = @ID;
    END
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_BeleskaObrisi @ID int, @AutorSifra int
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM dbo.BeleskaORadu WHERE ID = @ID AND AutorSifra = @AutorSifra)
        THROW 50232, N'Belešku briše samo njen autor.', 1;
    DELETE dbo.BeleskaORadu WHERE ID = @ID;
END
GO

/* ---------------------------------------------------------------------------------------------
   9. Procedure: godišnji odmor
   --------------------------------------------------------------------------------------------- */

-- Potrošeni dani = zbir odobrenih i iskorišćenih stavki
CREATE OR ALTER PROCEDURE dbo.sp_GO_Preracunaj @DopustID int
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE d SET PotrosenoDana = ISNULL((SELECT SUM(s.BrojDana) FROM dbo.GODopustStavke s WHERE s.DopustID = d.ID AND s.StatusID IN (3, 6)), 0)
    FROM dbo.GODopust d WHERE d.ID = @DopustID;
END
GO

-- Odobren GO kome je prošao poslednji dan postaje Iskorišćen
CREATE OR ALTER PROCEDURE dbo.sp_GO_OznaciIskoriscene
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.GODopustStavke SET StatusID = 6, DatumIzmene = GETDATE()
    WHERE StatusID = 3 AND DatumDo < CAST(GETDATE() AS date);
END
GO

-- Generisanje prava za godinu iz osnova
CREATE OR ALTER PROCEDURE dbo.sp_GO_Generisi
    @Godina smallint, @ZaposleniSifra int = NULL, @Prepisi bit = 0, @Korisnik nvarchar(20) = NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    DECLARE @Pocetak date = DATEFROMPARTS(@Godina, 1, 1), @Kraj date = DATEFROMPARTS(@Godina, 12, 31);
    DECLARE @Zaokruzi nvarchar(20) = ISNULL(dbo.fnHR_Podesavanje(N'HR.GOZaokruzivanje'), N'NAVISE');
    DECLARE @rez TABLE (ZaposleniSifra int, UkupnoDana int NULL, Ishod nvarchar(20), Razlog nvarchar(200) NULL);
    EXEC dbo.sp_GO_DodeliOsnovni @ZaposleniSifra = @ZaposleniSifra;

    DECLARE @z int, @ulazak date, @izlazak date, @pun int, @meseci int, @ukupno int, @od date, @do date, @postoji int, @potroseno int;
    DECLARE c CURSOR LOCAL FAST_FORWARD FOR
        SELECT ID, DatumUlaska, DatumIzlaska FROM dbo.Zaposleni
        WHERE (@ZaposleniSifra IS NULL OR ID = @ZaposleniSifra)
          AND (Status = 'A' OR DatumIzlaska >= @Pocetak)
          AND (DatumUlaska IS NULL OR DatumUlaska <= @Kraj);
    OPEN c;
    FETCH NEXT FROM c INTO @z, @ulazak, @izlazak;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        SELECT @pun = SUM(o.BrojDana)
        FROM dbo.ZaposleniOsnovGO o
        WHERE o.ZaposleniSifra = @z AND o.VaziOd <= @Kraj AND (o.VaziDo IS NULL OR o.VaziDo >= @Pocetak);

        SET @postoji = (SELECT ID FROM dbo.GODopust WHERE ZaposleniSifra = @z AND Godina = @Godina);

        IF @pun IS NULL
            INSERT @rez VALUES (@z, NULL, N'preskočeno', N'nema osnova koji važi u toj godini');
        ELSE IF @postoji IS NOT NULL AND @Prepisi = 0
            INSERT @rez VALUES (@z, NULL, N'preskočeno', N'pravo za tu godinu već postoji');
        ELSE
        BEGIN
            -- srazmerni deo u godini zasnivanja ili prestanka radnog odnosa (čl. 72)
            SET @od = CASE WHEN @ulazak > @Pocetak THEN @ulazak ELSE @Pocetak END;
            SET @do = CASE WHEN @izlazak < @Kraj THEN @izlazak ELSE @Kraj END;
            SET @meseci = DATEDIFF(MONTH, @od, DATEADD(DAY, 1, @do))
                          - CASE WHEN DAY(DATEADD(DAY, 1, @do)) < DAY(@od) THEN 1 ELSE 0 END;
            IF @meseci > 12 SET @meseci = 12;
            IF @meseci < 0 SET @meseci = 0;
            IF @meseci < 12
                SET @ukupno = CASE WHEN @Zaokruzi = N'MATEMATICKI' THEN CAST(ROUND(@pun * @meseci / 12.0, 0) AS int)
                                   ELSE CAST(CEILING(@pun * @meseci / 12.0) AS int) END;
            ELSE
                SET @ukupno = @pun;

            BEGIN TRAN;
            IF @postoji IS NULL
            BEGIN
                INSERT dbo.GODopust (ZaposleniSifra, Godina, UkupnoDana, RokKoriscenja, Srazmerno, MeseciRada, NameOper)
                VALUES (@z, @Godina, @ukupno, DATEFROMPARTS(@Godina + 1, 6, 30), CASE WHEN @meseci < 12 THEN 1 ELSE 0 END,
                        CASE WHEN @meseci < 12 THEN @meseci END, @Korisnik);
                SET @postoji = SCOPE_IDENTITY();
                INSERT @rez VALUES (@z, @ukupno, N'generisano', NULL);
            END
            ELSE
            BEGIN
                SET @potroseno = (SELECT PotrosenoDana FROM dbo.GODopust WHERE ID = @postoji);
                IF @ukupno < @potroseno
                BEGIN
                    INSERT @rez VALUES (@z, @ukupno, N'preskočeno', N'novo ukupno (' + CAST(@ukupno AS nvarchar(5)) + N') je manje od već potrošenih dana (' + CAST(@potroseno AS nvarchar(5)) + N')');
                    SET @postoji = NULL;
                END
                ELSE
                BEGIN
                    UPDATE dbo.GODopust SET UkupnoDana = @ukupno, Srazmerno = CASE WHEN @meseci < 12 THEN 1 ELSE 0 END,
                           MeseciRada = CASE WHEN @meseci < 12 THEN @meseci END, NameOper = @Korisnik, Datum = GETDATE()
                    WHERE ID = @postoji;
                    DELETE dbo.GODopustOsnov WHERE DopustID = @postoji;
                    INSERT @rez VALUES (@z, @ukupno, N'ažurirano', NULL);
                END
            END
            IF @postoji IS NOT NULL
                INSERT dbo.GODopustOsnov (DopustID, OsnovGOID, BrojDana)
                SELECT @postoji, o.OsnovGOID, SUM(o.BrojDana)
                FROM dbo.ZaposleniOsnovGO o
                WHERE o.ZaposleniSifra = @z AND o.VaziOd <= @Kraj AND (o.VaziDo IS NULL OR o.VaziDo >= @Pocetak)
                GROUP BY o.OsnovGOID;
            COMMIT;
        END
        FETCH NEXT FROM c INTO @z, @ulazak, @izlazak;
    END
    CLOSE c;
    DEALLOCATE c;

    SELECT r.ZaposleniSifra, LTRIM(RTRIM(ISNULL(z.Prezime, N'') + N' ' + ISNULL(z.Ime, N''))) AS Zaposleni, r.UkupnoDana, r.Ishod, r.Razlog
    FROM @rez r JOIN dbo.Zaposleni z ON z.ID = r.ZaposleniSifra
    ORDER BY CASE r.Ishod WHEN N'preskočeno' THEN 1 ELSE 0 END, Zaposleni;
END
GO

-- Stanje GO zaposlenog (mobilna aplikacija i Leget+)
CREATE OR ALTER PROCEDURE dbo.sp_GO_Stanje @ZaposleniSifra int, @Godina smallint = NULL
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.sp_GO_OznaciIskoriscene;
    SET @Godina = ISNULL(@Godina, YEAR(GETDATE()));
    SELECT @Godina AS Godina,
           t.UkupnoDana, t.PotrosenoDana, t.PreostaloDana, t.RokKoriscenja,
           ISNULL((SELECT SUM(s.BrojDana) FROM dbo.GODopustStavke s WHERE s.DopustID = t.ID AND s.StatusID = 2), 0) AS NaCekanjuDana,
           p.PreostaloDana AS PreostaloProsleGodine, p.RokKoriscenja AS RokProsleGodine
    FROM (SELECT @ZaposleniSifra AS Z) x
    LEFT JOIN dbo.GODopust t ON t.ZaposleniSifra = x.Z AND t.Godina = @Godina
    LEFT JOIN dbo.GODopust p ON p.ZaposleniSifra = x.Z AND p.Godina = @Godina - 1 AND p.RokKoriscenja >= CAST(GETDATE() AS date);
END
GO

-- Novi zahtev (oba kanala: mobilna aplikacija 'M' i Leget+ 'A')
CREATE OR ALTER PROCEDURE dbo.sp_GO_PodnesiZahtev
    @ZaposleniSifra int, @DatumOd date, @DatumDo date, @Razlog nvarchar(100) = NULL, @Napomena nvarchar(500) = NULL,
    @Kanal char(1) = 'M', @PodneoSifra int = NULL, @Korisnik nvarchar(20) = NULL,
    @ID int = NULL OUTPUT, @BrojDana int = NULL OUTPUT, @DatumPovratka date = NULL OUTPUT, @Upozorenje nvarchar(500) = NULL OUTPUT
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    IF @DatumDo < @DatumOd THROW 50301, N'Datum do je pre datuma od.', 1;
    IF YEAR(@DatumOd) <> YEAR(@DatumDo) THROW 50302, N'Zahtev ne može da obuhvati dve kalendarske godine; podnesite dva zahteva.', 1;

    SET @BrojDana = dbo.fnHR_RadniDani(@DatumOd, @DatumDo);
    IF @BrojDana = 0 THROW 50303, N'U izabranom periodu nema radnih dana.', 1;
    SET @DatumPovratka = dbo.fnHR_SledeciRadniDan(@DatumDo);

    DECLARE @God smallint = YEAR(@DatumOd);
    DECLARE @Tek int, @TekOstatak int, @PrOstatak int = 0, @PrRok date;
    SELECT @Tek = ID, @TekOstatak = PreostaloDana - ISNULL((SELECT SUM(s.BrojDana) FROM dbo.GODopustStavke s WHERE s.DopustID = d.ID AND s.StatusID = 2), 0)
    FROM dbo.GODopust d WHERE d.ZaposleniSifra = @ZaposleniSifra AND d.Godina = @God;
    IF @Tek IS NULL
        THROW 50304, N'Pravo na godišnji odmor za tu godinu nije generisano (Godišnji odmori – pravo > Generiši za godinu).', 1;
    SELECT @PrOstatak = PreostaloDana - ISNULL((SELECT SUM(s.BrojDana) FROM dbo.GODopustStavke s WHERE s.DopustID = d.ID AND s.StatusID = 2), 0), @PrRok = RokKoriscenja
    FROM dbo.GODopust d WHERE d.ZaposleniSifra = @ZaposleniSifra AND d.Godina = @God - 1;
    IF @PrRok IS NULL OR @DatumOd > @PrRok SET @PrOstatak = 0;

    IF @BrojDana > @TekOstatak + ISNULL(@PrOstatak, 0)
        THROW 50305, N'Nema dovoljno preostalih dana godišnjeg odmora.', 1;

    IF EXISTS (SELECT 1 FROM dbo.GODopustStavke s JOIN dbo.GODopust d ON d.ID = s.DopustID
               WHERE d.ZaposleniSifra = @ZaposleniSifra AND s.StatusID IN (2, 3, 6) AND s.DatumOd <= @DatumDo AND s.DatumDo >= @DatumOd)
        THROW 50306, N'Period se preklapa sa drugim zahtevom za godišnji odmor.', 1;
    IF EXISTS (SELECT 1 FROM dbo.EvidencijaRada e JOIN dbo.VrstaRada v ON v.ID = e.VrstaRadaID
               WHERE e.ZaposleniSifra = @ZaposleniSifra AND v.Oznaka = 'BO' AND e.Datum BETWEEN @DatumOd AND @DatumDo)
        THROW 50307, N'U izabranom periodu postoji bolovanje.', 1;

    -- informativno: prvi deo najmanje dve radne nedelje neprekidno (čl. 73)
    IF @BrojDana < 10 AND NOT EXISTS (SELECT 1 FROM dbo.GODopustStavke s WHERE s.DopustID = @Tek AND s.StatusID IN (2, 3, 6) AND s.BrojDana >= 10)
        SET @Upozorenje = N'Zaposleni još nema dve radne nedelje godišnjeg odmora u kontinuitetu u ovoj godini (čl. 73 Zakona o radu).';

    INSERT dbo.GODopustStavke (DopustID, DatumOd, DatumDo, BrojDana, DatumPovratka, Razlog, Napomena, StatusID, PodneoSifra, Kanal, NameOper)
    VALUES (@Tek, @DatumOd, @DatumDo, @BrojDana, @DatumPovratka, @Razlog, @Napomena, 2, ISNULL(@PodneoSifra, @ZaposleniSifra), @Kanal, @Korisnik);
    SET @ID = SCOPE_IDENTITY();

    SELECT @ID AS ID, @BrojDana AS BrojDana, @DatumPovratka AS DatumPovratka, @Upozorenje AS Upozorenje;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_GO_MojiZahtevi @ZaposleniSifra int
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.sp_GO_OznaciIskoriscene;
    SELECT s.ID, d.Godina, s.DatumOd, s.DatumDo, s.BrojDana, s.DatumPovratka, s.StatusID, st.Naziv AS Status,
           LTRIM(RTRIM(ISNULL(o.Prezime, N'') + N' ' + ISNULL(o.Ime, N''))) AS Odobrio, s.DatumOdobrenja,
           s.RazlogOdbijanja, s.BrojResenja, s.PutanjaResenja, s.DatumZahteva
    FROM dbo.GODopustStavke s
    JOIN dbo.GODopust d ON d.ID = s.DopustID
    JOIN dbo.StatusZahteva st ON st.ID = s.StatusID
    LEFT JOIN dbo.Zaposleni o ON o.ID = s.OdobrioSifra
    WHERE d.ZaposleniSifra = @ZaposleniSifra
    ORDER BY s.DatumOd DESC;
END
GO

-- Povlačenje zahteva: samo dok čeka odobrenje
CREATE OR ALTER PROCEDURE dbo.sp_GO_PovuciZahtev @ID int, @ZaposleniSifra int, @Korisnik nvarchar(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM dbo.GODopustStavke s JOIN dbo.GODopust d ON d.ID = s.DopustID
                   WHERE s.ID = @ID AND d.ZaposleniSifra = @ZaposleniSifra AND s.StatusID = 2)
        THROW 50310, N'Zahtev se može povući samo dok čeka odobrenje.', 1;
    UPDATE dbo.GODopustStavke SET StatusID = 5, NameOper = @Korisnik, DatumIzmene = GETDATE() WHERE ID = @ID;
END
GO

-- GO u evidenciju rada za svaki radni dan perioda (zaključan mesec se ne dira)
CREATE OR ALTER PROCEDURE dbo.sp_GO_UpisiUEvidenciju @StavkaID int, @Korisnik nvarchar(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Z int, @Od date, @Do date, @GO int = (SELECT ID FROM dbo.VrstaRada WHERE Oznaka = 'GO');
    SELECT @Z = d.ZaposleniSifra, @Od = s.DatumOd, @Do = s.DatumDo
    FROM dbo.GODopustStavke s JOIN dbo.GODopust d ON d.ID = s.DopustID WHERE s.ID = @StavkaID;

    SELECT k.Datum INTO #dani
    FROM dbo.fnHR_Kalendar(@Od, @Do) k
    WHERE k.DanUNedelji < 5 AND k.Praznik = 0
      AND NOT EXISTS (SELECT 1 FROM dbo.EvidencijaMesec m WHERE m.ZaposleniSifra = @Z AND m.Godina = YEAR(k.Datum) AND m.Mesec = MONTH(k.Datum) AND m.Status = 2);

    DELETE e FROM dbo.EvidencijaRada e JOIN #dani d ON d.Datum = e.Datum
    WHERE e.ZaposleniSifra = @Z AND (e.Izvor IN ('G', 'R') OR (e.Izvor = 'O' AND e.DopustStavkaID = @StavkaID));

    INSERT dbo.EvidencijaRada (ZaposleniSifra, Datum, VrstaRadaID, Sati, Izvor, DopustStavkaID, NameOper)
    SELECT @Z, d.Datum, @GO,
           ISNULL(NULLIF(dbo.fnHR_DnevniFond(@Z, d.Datum), 0), ISNULL(TRY_CAST(REPLACE(dbo.fnHR_Podesavanje(N'HR.DnevniFondSati'), ',', '.') AS decimal(5,2)), 8)),
           'O', @StavkaID, @Korisnik
    FROM #dani d;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_GO_BrojResenja @Godina smallint, @Broj nvarchar(30) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @n int;
    IF NOT EXISTS (SELECT 1 FROM dbo.GOBrojacResenja WITH (UPDLOCK, HOLDLOCK) WHERE Godina = @Godina)
        INSERT dbo.GOBrojacResenja (Godina, Poslednji) VALUES (@Godina, 0);
    UPDATE dbo.GOBrojacResenja SET @n = Poslednji = Poslednji + 1 WHERE Godina = @Godina;

    DECLARE @f nvarchar(100) = ISNULL(NULLIF(dbo.fnHR_Podesavanje(N'HR.GOFormatBrojaResenja'), N''), N'GO-{GGGG}/{BROJ:000}');
    SET @f = REPLACE(@f, N'{GGGG}', CAST(@Godina AS nvarchar(4)));
    DECLARE @p int = CHARINDEX(N'{BROJ', @f);
    IF @p > 0
    BEGIN
        DECLARE @kraj int = CHARINDEX(N'}', @f, @p);
        DECLARE @tok nvarchar(50) = SUBSTRING(@f, @p, @kraj - @p + 1);
        DECLARE @nule int = LEN(@tok) - LEN(REPLACE(@tok, N'0', N''));
        DECLARE @s nvarchar(10) = CAST(@n AS nvarchar(10));
        IF LEN(@s) < @nule SET @s = RIGHT(REPLICATE(N'0', @nule) + @s, @nule);
        SET @f = REPLACE(@f, @tok, @s);
    END
    SET @Broj = @f;
END
GO

-- Odobravanje (jedna transakcija): pravo odobravanja, skidanje dana (prvo prošla godina do roka), broj rešenja, upis u evidenciju.
-- Vraća spisak odobrenih stavki (zahtev se po potrebi deli na dve: deo sa prošle i deo sa tekuće godine).
CREATE OR ALTER PROCEDURE dbo.sp_GO_Odobri @ID int, @OdobrioSifra int, @HR bit = 0, @Korisnik nvarchar(20) = NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    DECLARE @Z int, @Od date, @Do date, @Dana int, @Tek int, @Status tinyint, @God smallint;
    SELECT @Z = d.ZaposleniSifra, @Od = s.DatumOd, @Do = s.DatumDo, @Dana = s.BrojDana, @Tek = s.DopustID, @Status = s.StatusID, @God = d.Godina
    FROM dbo.GODopustStavke s JOIN dbo.GODopust d ON d.ID = s.DopustID WHERE s.ID = @ID;
    IF @Z IS NULL THROW 50320, N'Zahtev ne postoji.', 1;
    IF @Status <> 2 THROW 50321, N'Odobrava se samo zahtev koji čeka odobrenje.', 1;
    IF @HR = 0 AND (@OdobrioSifra IS NULL OR @Z NOT IN (SELECT Sifra FROM dbo.fn_Podredjeni(@OdobrioSifra)))
        THROW 50322, N'Zahtev odobrava nadređeni iz lanca iznad zaposlenog ili HR.', 1;
    IF @OdobrioSifra = @Z THROW 50323, N'Zaposleni ne može da odobri sopstveni zahtev.', 1;

    DECLARE @Pr int, @PrOstatak int = 0, @PrRok date, @IzProsle int = 0, @IzTekuce int, @TekOstatak int;
    SELECT @Pr = ID, @PrOstatak = PreostaloDana, @PrRok = RokKoriscenja FROM dbo.GODopust WHERE ZaposleniSifra = @Z AND Godina = @God - 1;
    SELECT @TekOstatak = PreostaloDana FROM dbo.GODopust WHERE ID = @Tek;
    IF @Pr IS NOT NULL AND @Od <= @PrRok AND @PrOstatak > 0
    BEGIN
        SET @IzProsle = dbo.fnHR_RadniDani(@Od, CASE WHEN @Do < @PrRok THEN @Do ELSE @PrRok END);
        IF @IzProsle > @PrOstatak SET @IzProsle = @PrOstatak;
    END
    SET @IzTekuce = @Dana - @IzProsle;
    IF @IzTekuce > @TekOstatak THROW 50324, N'Nema dovoljno preostalih dana godišnjeg odmora.', 1;

    DECLARE @odobrene TABLE (ID int);
    BEGIN TRAN;
    IF @IzProsle = @Dana
    BEGIN
        UPDATE dbo.GODopustStavke SET DopustID = @Pr WHERE ID = @ID;
        INSERT @odobrene VALUES (@ID);
    END
    ELSE IF @IzProsle > 0
    BEGIN
        DECLARE @Granica date = (SELECT Datum FROM (SELECT Datum, ROW_NUMBER() OVER (ORDER BY Datum) AS rb
                                                    FROM dbo.fnHR_Kalendar(@Od, @Do) WHERE DanUNedelji < 5 AND Praznik = 0) x
                                 WHERE rb = @IzProsle);
        INSERT dbo.GODopustStavke (DopustID, DatumOd, DatumDo, BrojDana, DatumPovratka, Razlog, Napomena, StatusID, DatumZahteva, PodneoSifra, Kanal, NameOper)
        SELECT @Tek, DATEADD(DAY, 1, @Granica), DatumDo, @IzTekuce, DatumPovratka, Razlog, Napomena, 2, DatumZahteva, PodneoSifra, Kanal, @Korisnik
        FROM dbo.GODopustStavke WHERE ID = @ID;
        INSERT @odobrene VALUES (SCOPE_IDENTITY());
        UPDATE dbo.GODopustStavke SET DopustID = @Pr, DatumDo = @Granica, BrojDana = @IzProsle, DatumPovratka = dbo.fnHR_SledeciRadniDan(@Granica)
        WHERE ID = @ID;
        INSERT @odobrene VALUES (@ID);
    END
    ELSE
        INSERT @odobrene VALUES (@ID);

    DECLARE @s int, @broj nvarchar(30);
    DECLARE o CURSOR LOCAL FAST_FORWARD FOR SELECT ID FROM @odobrene ORDER BY ID;
    OPEN o;
    FETCH NEXT FROM o INTO @s;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        EXEC dbo.sp_GO_BrojResenja @Godina = @God, @Broj = @broj OUTPUT;
        UPDATE dbo.GODopustStavke
        SET StatusID = 3, OdobrioSifra = @OdobrioSifra, DatumOdobrenja = GETDATE(), BrojResenja = @broj,
            DatumResenja = CAST(GETDATE() AS date), NameOper = @Korisnik, DatumIzmene = GETDATE()
        WHERE ID = @s;
        EXEC dbo.sp_GO_UpisiUEvidenciju @StavkaID = @s, @Korisnik = @Korisnik;
        FETCH NEXT FROM o INTO @s;
    END
    CLOSE o;
    DEALLOCATE o;

    EXEC dbo.sp_GO_Preracunaj @DopustID = @Tek;
    IF @Pr IS NOT NULL EXEC dbo.sp_GO_Preracunaj @DopustID = @Pr;
    COMMIT;

    SELECT ID FROM @odobrene ORDER BY ID;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_GO_Odbij @ID int, @OdobrioSifra int, @HR bit = 0, @Razlog nvarchar(250), @Korisnik nvarchar(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Z int, @Status tinyint;
    SELECT @Z = d.ZaposleniSifra, @Status = s.StatusID FROM dbo.GODopustStavke s JOIN dbo.GODopust d ON d.ID = s.DopustID WHERE s.ID = @ID;
    IF @Status <> 2 THROW 50321, N'Odbija se samo zahtev koji čeka odobrenje.', 1;
    IF LTRIM(RTRIM(ISNULL(@Razlog, N''))) = N'' THROW 50325, N'Razlog odbijanja je obavezan.', 1;
    IF @HR = 0 AND (@OdobrioSifra IS NULL OR @Z NOT IN (SELECT Sifra FROM dbo.fn_Podredjeni(@OdobrioSifra)))
        THROW 50322, N'Zahtev odbija nadređeni iz lanca iznad zaposlenog ili HR.', 1;
    UPDATE dbo.GODopustStavke
    SET StatusID = 4, OdobrioSifra = @OdobrioSifra, DatumOdobrenja = GETDATE(), RazlogOdbijanja = @Razlog, NameOper = @Korisnik, DatumIzmene = GETDATE()
    WHERE ID = @ID;
END
GO

-- Otkazivanje: zahtev koji čeka (povlačenje) ili odobren GO koji još nije počeo; dani se vraćaju, GO se briše iz evidencije
CREATE OR ALTER PROCEDURE dbo.sp_GO_Otkazi @ID int, @Korisnik nvarchar(20) = NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    DECLARE @Status tinyint, @Od date, @Dopust int, @Z int;
    SELECT @Status = s.StatusID, @Od = s.DatumOd, @Dopust = s.DopustID, @Z = d.ZaposleniSifra
    FROM dbo.GODopustStavke s JOIN dbo.GODopust d ON d.ID = s.DopustID WHERE s.ID = @ID;
    IF @Status NOT IN (2, 3) THROW 50330, N'Otkazuje se samo zahtev koji čeka odobrenje ili odobren godišnji odmor.', 1;
    IF @Status = 3 AND @Od <= CAST(GETDATE() AS date)
        THROW 50331, N'Godišnji odmor je počeo; za skraćenje koristite Prekid (bolovanje) ili izmenu perioda.', 1;
    BEGIN TRAN;
    UPDATE dbo.GODopustStavke SET StatusID = 5, NameOper = @Korisnik, DatumIzmene = GETDATE() WHERE ID = @ID;
    DELETE e FROM dbo.EvidencijaRada e
    WHERE e.DopustStavkaID = @ID AND e.Izvor = 'O'
      AND NOT EXISTS (SELECT 1 FROM dbo.EvidencijaMesec m WHERE m.ZaposleniSifra = e.ZaposleniSifra AND m.Godina = YEAR(e.Datum) AND m.Mesec = MONTH(e.Datum) AND m.Status = 2);
    EXEC dbo.sp_GO_Preracunaj @DopustID = @Dopust;
    COMMIT;
END
GO

-- Prekid zbog bolovanja: GO se skraćuje do dana pre bolovanja, neiskorišćeni dani se vraćaju, u evidenciju ide BO
CREATE OR ALTER PROCEDURE dbo.sp_GO_PrekidBolovanje @ID int, @BolovanjeOd date, @Korisnik nvarchar(20) = NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    DECLARE @Status tinyint, @Od date, @Do date, @Dopust int, @Z int, @BO int = (SELECT ID FROM dbo.VrstaRada WHERE Oznaka = 'BO');
    SELECT @Status = s.StatusID, @Od = s.DatumOd, @Do = s.DatumDo, @Dopust = s.DopustID, @Z = d.ZaposleniSifra
    FROM dbo.GODopustStavke s JOIN dbo.GODopust d ON d.ID = s.DopustID WHERE s.ID = @ID;
    IF @Status NOT IN (3, 6) THROW 50340, N'Prekid je moguć samo za odobren godišnji odmor.', 1;
    IF @BolovanjeOd <= @Od OR @BolovanjeOd > @Do THROW 50341, N'Početak bolovanja mora biti posle prvog i najkasnije poslednjeg dana odmora.', 1;

    DECLARE @NoviDo date = DATEADD(DAY, -1, @BolovanjeOd);
    BEGIN TRAN;
    UPDATE dbo.GODopustStavke
    SET DatumDo = @NoviDo, BrojDana = dbo.fnHR_RadniDani(@Od, @NoviDo), DatumPovratka = dbo.fnHR_SledeciRadniDan(@NoviDo),
        Napomena = LEFT(ISNULL(Napomena + N' ', N'') + N'Prekinut bolovanjem od ' + CONVERT(nvarchar(10), @BolovanjeOd, 104) + N'.', 500),
        NameOper = @Korisnik, DatumIzmene = GETDATE()
    WHERE ID = @ID;

    -- dani od početka bolovanja do starog kraja odmora: GO -> BO
    SELECT e.Datum, e.Sati INTO #dani FROM dbo.EvidencijaRada e
    WHERE e.DopustStavkaID = @ID AND e.Izvor = 'O' AND e.Datum >= @BolovanjeOd
      AND NOT EXISTS (SELECT 1 FROM dbo.EvidencijaMesec m WHERE m.ZaposleniSifra = @Z AND m.Godina = YEAR(e.Datum) AND m.Mesec = MONTH(e.Datum) AND m.Status = 2);
    DELETE e FROM dbo.EvidencijaRada e JOIN #dani d ON d.Datum = e.Datum WHERE e.DopustStavkaID = @ID AND e.Izvor = 'O';
    INSERT dbo.EvidencijaRada (ZaposleniSifra, Datum, VrstaRadaID, Sati, Napomena, Izvor, NameOper)
    SELECT @Z, d.Datum, @BO, d.Sati, N'Prekid godišnjeg odmora', 'R', @Korisnik FROM #dani d;

    EXEC dbo.sp_GO_Preracunaj @DopustID = @Dopust;
    COMMIT;
END
GO

-- Početno stanje već iskorišćenih dana pri prvom puštanju u rad
CREATE OR ALTER PROCEDURE dbo.sp_GO_PocetnoStanje @ZaposleniSifra int, @Godina smallint, @BrojDana smallint, @Korisnik nvarchar(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @D int = (SELECT ID FROM dbo.GODopust WHERE ZaposleniSifra = @ZaposleniSifra AND Godina = @Godina);
    IF @D IS NULL THROW 50350, N'Pravo za tu godinu nije generisano.', 1;
    DELETE dbo.GODopustStavke WHERE DopustID = @D AND Razlog = N'Početno stanje';
    IF @BrojDana > 0
        INSERT dbo.GODopustStavke (DopustID, DatumOd, DatumDo, BrojDana, Razlog, StatusID, Kanal, NameOper)
        VALUES (@D, DATEFROMPARTS(@Godina, 1, 1), DATEFROMPARTS(@Godina, 1, 1), @BrojDana, N'Početno stanje', 6, 'A', @Korisnik);
    EXEC dbo.sp_GO_Preracunaj @DopustID = @D;
END
GO

/* ---------------------------------------------------------------------------------------------
   10. Prebacivanje postojećih podataka iz Dopust / DopustStavke (stari obrazac)
       Zaposleni se uparuje preko Zaposleni.SifraERP = Delavci.DeSifra (Dopust.DoSifDe).
       Radi samo jednom: dok je GODopust prazan. Neupareni zapisi se prijavljuju.
   --------------------------------------------------------------------------------------------- */
IF OBJECT_ID('dbo.Dopust', 'U') IS NOT NULL AND COL_LENGTH('dbo.Dopust', 'DoSifDe') IS NOT NULL
   AND OBJECT_ID('dbo.DopustStavke', 'U') IS NOT NULL AND COL_LENGTH('dbo.DopustStavke', 'IDNadredjena') IS NOT NULL
   AND NOT EXISTS (SELECT 1 FROM dbo.GODopust)
BEGIN
    DECLARE @sql nvarchar(max) = N'
        DECLARE @mapa TABLE (StaraID int, NovaID int);
        MERGE dbo.GODopust AS t
        USING (SELECT d.DoStZapisa, z.ID AS Zaposleni, d.DoLeto, CAST(ISNULL(d.DoSkupaj, 0) AS smallint) AS Ukupno
               FROM dbo.Dopust d
               CROSS APPLY (SELECT TOP (1) ID FROM dbo.Zaposleni WHERE LTRIM(RTRIM(SifraERP)) = LTRIM(RTRIM(CAST(d.DoSifDe AS nvarchar(30)))) ORDER BY ID) z
               WHERE NOT EXISTS (SELECT 1 FROM dbo.Dopust d2 WHERE d2.DoSifDe = d.DoSifDe AND d2.DoLeto = d.DoLeto AND d2.DoStZapisa < d.DoStZapisa)) AS s
        ON 1 = 0
        WHEN NOT MATCHED THEN
            INSERT (ZaposleniSifra, Godina, UkupnoDana, RokKoriscenja, NameOper)
            VALUES (s.Zaposleni, s.DoLeto, s.Ukupno, DATEFROMPARTS(s.DoLeto + 1, 6, 30), ''PRENOS'')
        OUTPUT s.DoStZapisa, inserted.ID INTO @mapa;

        INSERT dbo.GODopustStavke (DopustID, DatumOd, DatumDo, BrojDana, DatumPovratka, Razlog, Napomena, StatusID, DatumZahteva, Kanal,
                                   OdobrioSifra, ResenjePoslatoMail, ResenjeStampano, NameOper)
        SELECT m.NovaID, CAST(s.VremeOd AS date), CAST(s.VremeDo AS date), CAST(ISNULL(s.Ukupno, 0) AS smallint), CAST(s.DatumPovratka AS date),
               LEFT(s.Razlog, 100), s.Napomena,
               CASE WHEN CAST(s.VremeDo AS date) < CAST(GETDATE() AS date) THEN 6 ELSE 3 END,
               ISNULL(s.DatumZahteva, GETDATE()), ''A'',
               (SELECT TOP (1) ID FROM dbo.Zaposleni WHERE LTRIM(RTRIM(SifraERP)) = LTRIM(RTRIM(CAST(s.Odobrio AS nvarchar(30))))),
               CASE WHEN ISNULL(s.PoslatMail, 0) <> 0 THEN 1 ELSE 0 END,
               CASE WHEN ISNULL(s.PoslatoResenje, 0) <> 0 THEN 1 ELSE 0 END, ''PRENOS''
        FROM dbo.DopustStavke s JOIN @mapa m ON m.StaraID = s.IDNadredjena
        WHERE s.VremeOd IS NOT NULL AND s.VremeDo IS NOT NULL AND s.VremeDo >= s.VremeOd;

        UPDATE d SET PotrosenoDana = ISNULL((SELECT SUM(x.BrojDana) FROM dbo.GODopustStavke x WHERE x.DopustID = d.ID AND x.StatusID IN (3, 6)), 0)
        FROM dbo.GODopust d;

        PRINT N''Prebačeno prava: '' + CAST((SELECT COUNT(*) FROM @mapa) AS nvarchar(10)) + N'' od '' + CAST((SELECT COUNT(*) FROM dbo.Dopust) AS nvarchar(10))
            + N''; neupareni zaposleni (Zaposleni.SifraERP <> Delavci.DeSifra) nisu prebačeni.'';';
    BEGIN TRY
        EXEC sp_executesql @sql;
    END TRY
    BEGIN CATCH
        PRINT N'Prebacivanje starih podataka nije uspelo: ' + ERROR_MESSAGE();
    END CATCH
END
GO

/* ---------------------------------------------------------------------------------------------
   11. Prava: kartice HR modula i Administracija > Podešavanja
       Kartice se prave kao kopije postojećih kartica istog modula. Prava na HR kartice dobijaju
       korisnici koji već imaju pravo na karticu 'Zaposleni' u HR modulu; pravo na Podešavanja
       dobijaju korisnici koji imaju pravo na karticu 'Prava' u Administraciji.
   --------------------------------------------------------------------------------------------- */
IF OBJECT_ID('tempdb..#HR_Kartica') IS NOT NULL DROP PROCEDURE #HR_Kartica;
GO
CREATE PROCEDURE #HR_Kartica @Modul nvarchar(100), @Kartica nvarchar(50), @PravaKaoKartica nvarchar(50)
AS
BEGIN
    DECLARE @Main int = (SELECT TOP (1) ID FROM dbo.MainNovi WHERE LTRIM(RTRIM(Naziv)) = @Modul ORDER BY ID);
    IF @Main IS NULL BEGIN PRINT N'Modul ' + @Modul + N' nije pronađen; kartica ' + @Kartica + N' nije dodata.'; RETURN; END
    DECLARE @Sablon int = (SELECT TOP (1) ID FROM dbo.mainNoviKartice WHERE MainID = @Main AND Parent IS NULL ORDER BY ID);
    IF @Sablon IS NULL SET @Sablon = (SELECT TOP (1) ID FROM dbo.mainNoviKartice WHERE Parent IS NULL ORDER BY ID);
    IF @Sablon IS NULL RETURN;
    DECLARE @KarticaID int = (SELECT TOP (1) ID FROM dbo.mainNoviKartice WHERE MainID = @Main AND Parent IS NULL AND Naziv = @Kartica);
    DECLARE @sql nvarchar(max), @kolone nvarchar(max), @vrednosti nvarchar(max);
    IF @KarticaID IS NULL
    BEGIN
        SELECT @kolone = STUFF((SELECT N', ' + QUOTENAME(c.name) FROM sys.columns c
                                WHERE c.object_id = OBJECT_ID('dbo.mainNoviKartice') AND c.is_identity = 0 AND c.is_computed = 0 AND TYPE_NAME(c.system_type_id) <> 'timestamp'
                                ORDER BY c.column_id FOR XML PATH('')), 1, 2, N''),
               @vrednosti = STUFF((SELECT N', ' + CASE c.name WHEN 'Naziv' THEN N'@Kartica' WHEN 'MainID' THEN N'@Main' WHEN 'Parent' THEN N'NULL'
                                                               WHEN 'ID' THEN N'(SELECT ISNULL(MAX(ID), 0) + 1 FROM dbo.mainNoviKartice)'
                                                               ELSE QUOTENAME(c.name) END
                                   FROM sys.columns c
                                   WHERE c.object_id = OBJECT_ID('dbo.mainNoviKartice') AND c.is_identity = 0 AND c.is_computed = 0 AND TYPE_NAME(c.system_type_id) <> 'timestamp'
                                   ORDER BY c.column_id FOR XML PATH('')), 1, 2, N'');
        SET @sql = N'INSERT INTO dbo.mainNoviKartice (' + @kolone + N') SELECT ' + @vrednosti + N' FROM dbo.mainNoviKartice WHERE ID = @Izvor';
        EXEC sp_executesql @sql, N'@Izvor int, @Kartica nvarchar(50), @Main int', @Izvor = @Sablon, @Kartica = @Kartica, @Main = @Main;
        SET @KarticaID = (SELECT TOP (1) ID FROM dbo.mainNoviKartice WHERE MainID = @Main AND Parent IS NULL AND Naziv = @Kartica);
        PRINT N'Dodata kartica ' + @Modul + N' > ' + @Kartica;
    END

    DECLARE @Izvorna int = (SELECT TOP (1) ID FROM dbo.mainNoviKartice WHERE MainID = @Main AND Naziv = @PravaKaoKartica);
    IF @Izvorna IS NULL RETURN;
    SELECT @kolone = STUFF((SELECT N', ' + QUOTENAME(c.name) FROM sys.columns c
                            WHERE c.object_id = OBJECT_ID('dbo.MainNoviPrava') AND c.is_identity = 0 AND c.is_computed = 0 AND TYPE_NAME(c.system_type_id) <> 'timestamp'
                            ORDER BY c.column_id FOR XML PATH('')), 1, 2, N''),
           @vrednosti = STUFF((SELECT N', ' + CASE c.name WHEN 'KarticaID' THEN N'@KarticaID'
                                                           WHEN 'ID' THEN N'(SELECT ISNULL(MAX(ID), 0) FROM dbo.MainNoviPrava) + ROW_NUMBER() OVER (ORDER BY (SELECT 1))'
                                                           ELSE N'p.' + QUOTENAME(c.name) END
                               FROM sys.columns c
                               WHERE c.object_id = OBJECT_ID('dbo.MainNoviPrava') AND c.is_identity = 0 AND c.is_computed = 0 AND TYPE_NAME(c.system_type_id) <> 'timestamp'
                               ORDER BY c.column_id FOR XML PATH('')), 1, 2, N'');
    SET @sql = N'INSERT INTO dbo.MainNoviPrava (' + @kolone + N') SELECT ' + @vrednosti
             + N' FROM dbo.MainNoviPrava p WHERE p.MainID = @Main AND p.KarticaID = @Izvorna'
             + N' AND NOT EXISTS (SELECT 1 FROM dbo.MainNoviPrava x WHERE x.MainID = @Main AND x.KarticaID = @KarticaID AND x.Korisnik = p.Korisnik);';
    EXEC sp_executesql @sql, N'@Main int, @KarticaID int, @Izvorna int', @Main = @Main, @KarticaID = @KarticaID, @Izvorna = @Izvorna;
END
GO

IF OBJECT_ID('dbo.MainNovi', 'U') IS NOT NULL AND OBJECT_ID('dbo.mainNoviKartice', 'U') IS NOT NULL AND OBJECT_ID('dbo.MainNoviPrava', 'U') IS NOT NULL
BEGIN
    EXEC #HR_Kartica N'HR modul', N'Evidencija rada', N'Zaposleni';
    EXEC #HR_Kartica N'HR modul', N'Zaključavanje meseca', N'Zaposleni';
    EXEC #HR_Kartica N'HR modul', N'Odobravanje zahteva', N'Zaposleni';
    EXEC #HR_Kartica N'HR modul', N'Godišnji odmori', N'Zaposleni';
    EXEC #HR_Kartica N'HR modul', N'Beleške o radu', N'Zaposleni';
    EXEC #HR_Kartica N'HR modul', N'HR log', N'Zaposleni';
    EXEC #HR_Kartica N'Administracija', N'Podešavanja', N'Prava';
END
GO

DROP PROCEDURE #HR_Kartica;
GO

PRINT N'HR modul, faza 2: skripta završena.';
GO
