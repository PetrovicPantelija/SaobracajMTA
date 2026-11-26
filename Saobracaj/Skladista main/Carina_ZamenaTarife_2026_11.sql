/* =============================================================================================
   Carina: zamena tarifnog broja naimenovanja sa tragom (RAZ-05)
   Predlog: Carina-Izlaz-Tarife-Vlasnik-Kontejneri-Knjiga-Predlog.md, verzija 1.1, 26.11.2026.

   Kad carina ispravi tarifni broj posle usklađištenja (ispravka C7), zamena se radi jednom radnjom na Nalogu za PRIJEM
   (dugme „Zameni tarifni broj“), u jednoj transakciji (sp_NHM_Zameni):
     1. red u RNCarinskoNHMIzmena (stari, novi, razlog, dokument, korisnik, datum);
     2. RNCarinskoStavke.NHM (naimenovanje);
     3. RNCarinskoPrijemnicaStavke.NHM svih stavki vezanih za naimenovanje (IDNaimenovanja); za potvrđenu prijemnicu
        (status ZA) i red u RNCarinskoPrijemnicaKorekcija;
     4. otvorena otprema (RNCarinskoSkladisteOtpremaStavkePom) za te stavke.
   Potvrđene otpremnice (RNCarinskoSkladisteOtpremnicaStavke) se ne menjaju. Pregled lagera i Otprema vezuju
   potvrđene otpremnice za stavke prijemnice po tekstu i tarifnom broju, pa bi zamena posle razduženja vratila
   razduženu robu u lager. Zato procedura odbija zamenu kad naimenovanje već ima potvrđenu otpremnicu.

   Uslov: puštene Carina_PrijemnicaPL_2026_11.sql (IDNaimenovanja) i Carina_TarifniBroj_2026_11.sql (NHM kao tekst).
   Skripta se može pustiti više puta.
   ============================================================================================= */
SET NOCOUNT ON;

IF COL_LENGTH('dbo.RNCarinskoPrijemnicaStavke', 'IDNaimenovanja') IS NULL
   OR NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.RNCarinskoStavke') AND name = 'NHM' AND TYPE_NAME(user_type_id) LIKE '%char%')
BEGIN
    RAISERROR(N'Prvo pustiti Carina_PrijemnicaPL_2026_11.sql i Carina_TarifniBroj_2026_11.sql (NHM mora biti tekst). Skripta je prekinuta.', 16, 1);
    SET NOEXEC ON;
END
GO

IF OBJECT_ID('dbo.RNCarinskoNHMIzmena', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.RNCarinskoNHMIzmena
    (
        ID                int IDENTITY(1,1) NOT NULL CONSTRAINT PK_RNCarinskoNHMIzmena PRIMARY KEY,
        IDNaimenovanja    int            NOT NULL,          -- RNCarinskoStavke.ID
        IDMagacinskogPredmeta int        NULL,              -- MagacinskiPredmet.ID
        RN                int            NULL,              -- RadniNalogSkladista.ID (nalog za prijem)
        MagacinskiBroj    int            NULL,
        BrojC7            nvarchar(50)   NULL,
        RBNaimenovanja    int            NULL,
        StariNHM          varchar(10)    NULL,
        NoviNHM           varchar(10)    NOT NULL,
        Razlog            nvarchar(500)  NOT NULL,
        Dokument          nvarchar(200)  NULL,              -- npr. ispravka C7 1177/2025, 12.04.2025
        StavkiPrijemnice  int            NOT NULL CONSTRAINT DF_RNCarinskoNHMIzmena_SP DEFAULT (0),
        StavkiOtpreme     int            NOT NULL CONSTRAINT DF_RNCarinskoNHMIzmena_SO DEFAULT (0),
        Korisnik          nvarchar(50)   NULL,
        Datum             datetime       NOT NULL CONSTRAINT DF_RNCarinskoNHMIzmena_Datum DEFAULT (GETDATE())
    );
    CREATE INDEX IX_RNCarinskoNHMIzmena_Naimenovanje ON dbo.RNCarinskoNHMIzmena (IDNaimenovanja);
    CREATE INDEX IX_RNCarinskoNHMIzmena_MB ON dbo.RNCarinskoNHMIzmena (MagacinskiBroj, Datum);
    PRINT 'Napravljena tabela dbo.RNCarinskoNHMIzmena';
END
ELSE
    PRINT 'Tabela dbo.RNCarinskoNHMIzmena već postoji';
GO

/* ---------------------------------------------------------------------------------------------
   sp_NHM_Zameni: zamena tarifnog broja jednog naimenovanja. Vraća jedan red: ID izmene, broj
   promenjenih stavki prijemnice i otpreme. Greška (RAISERROR) ne menja ništa.
   --------------------------------------------------------------------------------------------- */
CREATE OR ALTER PROCEDURE dbo.sp_NHM_Zameni
    @IDNaimenovanja int,
    @NoviNHM        varchar(10),
    @Razlog         nvarchar(500),
    @Dokument       nvarchar(200) = NULL,
    @BrojC7         nvarchar(50) = NULL,
    @Korisnik       nvarchar(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    SET @NoviNHM = LTRIM(RTRIM(@NoviNHM));
    SET @Razlog = LTRIM(RTRIM(ISNULL(@Razlog, N'')));
    SET @Dokument = NULLIF(LTRIM(RTRIM(@Dokument)), N'');

    IF @NoviNHM IS NULL OR @NoviNHM LIKE '%[^0-9]%' OR LEN(@NoviNHM) NOT IN (8, 10)
    BEGIN
        RAISERROR(N'Novi tarifni broj mora imati 8 cifara (ili 10 za TARIC).', 16, 1);
        RETURN;
    END
    IF @Razlog = N''
    BEGIN
        RAISERROR(N'Razlog zamene je obavezan.', 16, 1);
        RETURN;
    END

    DECLARE @Stari varchar(10), @MP int, @RB int, @RN int, @MB int;
    SELECT @Stari = NULLIF(LTRIM(RTRIM(CONVERT(varchar(10), n.NHM))), ''), @MP = n.IDNadredjena, @RB = n.RB
    FROM dbo.RNCarinskoStavke n WHERE n.ID = @IDNaimenovanja;
    IF @MP IS NULL
    BEGIN
        RAISERROR(N'Naimenovanje %d ne postoji.', 16, 1, @IDNaimenovanja);
        RETURN;
    END
    IF ISNULL(@Stari, '') = @NoviNHM
    BEGIN
        RAISERROR(N'Naimenovanje već ima tarifni broj %s.', 16, 1, @NoviNHM);
        RETURN;
    END

    SELECT @RN = mp.RN, @MB = rns.MagacinskiBroj
    FROM dbo.MagacinskiPredmet mp
    LEFT JOIN dbo.RadniNalogSkladista rns ON rns.ID = mp.RN
    WHERE mp.ID = @MP;

    -- stavke prijemnice vezane za naimenovanje (pre zamene)
    DECLARE @Stavke TABLE (ID int PRIMARY KEY, IDPrijemnice int, RB int, NHM varchar(10), Naziv nvarchar(500), Naimenovanje nvarchar(500));
    INSERT @Stavke (ID, IDPrijemnice, RB, NHM, Naziv, Naimenovanje)
    SELECT s.ID, s.IDNadredjena, s.RB, CONVERT(varchar(10), s.NHM),
           RTRIM(ISNULL(CONVERT(nvarchar(500), s.Naziv), N'')), RTRIM(ISNULL(CONVERT(nvarchar(500), s.Naimenovanje), N''))
    FROM dbo.RNCarinskoPrijemnicaStavke s
    WHERE s.IDNaimenovanja = @IDNaimenovanja;

    -- potvrđena otpremnica za te stavke: veza je po prijemnici, tarifnom broju i tekstu (kao u Otpremi i Pregledu lagera)
    DECLARE @Otpremnice nvarchar(400);
    IF OBJECT_ID('dbo.RNCarinskoSkladisteOtpremnicaStavke', 'U') IS NOT NULL
        SELECT @Otpremnice = ISNULL(@Otpremnice + N', ', N'') + CONVERT(nvarchar(20), x.ID)
        FROM (SELECT DISTINCT TOP 20 ot.ID
              FROM dbo.RNCarinskoSkladisteOtpremnicaStavke st
              JOIN dbo.RNCarinskoSkladisteOtpremnica ot ON ot.ID = st.IDNadredjena
              JOIN @Stavke s ON s.IDPrijemnice = ot.Prijemnica
                            AND ISNULL(CONVERT(varchar(10), st.NHM), '') = ISNULL(s.NHM, '')
                            AND RTRIM(ISNULL(CONVERT(nvarchar(500), st.Naziv), N'')) = s.Naziv
                            AND RTRIM(ISNULL(CONVERT(nvarchar(500), st.Naimenovanje), N'')) = s.Naimenovanje
              ORDER BY ot.ID) x;
    IF @Otpremnice IS NOT NULL
    BEGIN
        RAISERROR(N'Naimenovanje je već razduženo na otpremnicama: %s. Zamena tarifnog broja posle razduženja nije dozvoljena, jer bi razdužena roba ponovo bila na lageru.', 16, 1, @Otpremnice);
        RETURN;
    END

    DECLARE @NaOtpremi int = 0, @NaPrijemnici int = 0, @IDIzmene int;

    BEGIN TRAN;

    -- otvorena otprema: isti ključ kao u Otpremi (prijemnica, tarifni broj, tekst), pre zamene na stavkama
    IF OBJECT_ID('dbo.RNCarinskoSkladisteOtpremaStavkePom', 'U') IS NOT NULL
    BEGIN
        UPDATE pom SET NHM = @NoviNHM
        FROM dbo.RNCarinskoSkladisteOtpremaStavkePom pom
        WHERE EXISTS (SELECT 1 FROM @Stavke s
                      WHERE s.IDPrijemnice = pom.Prijemnica
                        AND ISNULL(CONVERT(varchar(10), pom.NHM), '') = ISNULL(s.NHM, '')
                        AND RTRIM(ISNULL(CONVERT(nvarchar(500), pom.Naziv), N'')) = s.Naziv
                        AND RTRIM(ISNULL(CONVERT(nvarchar(500), pom.Naimenovanje), N'')) = s.Naimenovanje);
        SET @NaOtpremi = @@ROWCOUNT;
    END

    -- potvrđena prijemnica: trag u korekcijama (CAR-P08)
    IF OBJECT_ID('dbo.RNCarinskoPrijemnicaKorekcija', 'U') IS NOT NULL
        INSERT dbo.RNCarinskoPrijemnicaKorekcija (IDPrijemnice, IDStavke, Akcija, Opis, Korisnik)
        SELECT s.IDPrijemnice, s.ID, N'Tarifni broj',
               LEFT(N'RB ' + CONVERT(nvarchar(10), s.RB) + N': ' + ISNULL(s.NHM, N'-') + N' -> ' + @NoviNHM + N'. ' + @Razlog
                    + ISNULL(N' (' + @Dokument + N')', N''), 1000),
               @Korisnik
        FROM @Stavke s
        JOIN dbo.RNCarinskoSkladistePrijemnica pr ON pr.ID = s.IDPrijemnice
        WHERE RTRIM(pr.Status) = N'ZA';

    UPDATE dbo.RNCarinskoPrijemnicaStavke SET NHM = @NoviNHM WHERE IDNaimenovanja = @IDNaimenovanja;
    SET @NaPrijemnici = @@ROWCOUNT;

    UPDATE dbo.RNCarinskoStavke SET NHM = @NoviNHM WHERE ID = @IDNaimenovanja;

    INSERT dbo.RNCarinskoNHMIzmena
        (IDNaimenovanja, IDMagacinskogPredmeta, RN, MagacinskiBroj, BrojC7, RBNaimenovanja, StariNHM, NoviNHM, Razlog, Dokument,
         StavkiPrijemnice, StavkiOtpreme, Korisnik)
    VALUES
        (@IDNaimenovanja, @MP, @RN, @MB, NULLIF(LTRIM(RTRIM(@BrojC7)), N''), @RB, @Stari, @NoviNHM, @Razlog, @Dokument,
         @NaPrijemnici, @NaOtpremi, @Korisnik);
    SET @IDIzmene = SCOPE_IDENTITY();

    COMMIT;

    SELECT @IDIzmene AS IDIzmene, @NaPrijemnici AS StavkiPrijemnice, @NaOtpremi AS StavkiOtpreme;
END
GO

SET NOEXEC OFF;
GO
