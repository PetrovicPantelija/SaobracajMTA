-- PRODUKCIJA (oktobar 2026): Terminal privremeni - pokreti brodara Maersk, MSC, CMA, OOCL, HMM, HAPAG, FLEXBOX i COSCO.
-- Sadrži: tabele TerminalPrivMaersk, TerminalPrivMSC, TerminalPrivCMA, TerminalPrivOOCL, TerminalPrivHMM, TerminalPrivHAPAG,
--   TerminalPrivFLEXBOX i TerminalPrivCOSCO (prave se samo ako ne postoje), procedure pokreta svih brodara, insTerminalPriv
--   i updTerminalPriv (izlazni parametri @BrojMaersk, @BrojMSC, @BrojCMA, @BrojOOCL, @BrojHMM, @BrojHAPAG, @BrojFLEXBOX, @BrojCOSCO).
-- Zamenjuje TerminalPriv_OOCL_Produkcija_2026_10.sql i TerminalPriv_OOCL_HMM_Produkcija_2026_10.sql.
-- Skripta se može pustiti više puta. Pre pokretanja izabrati produkcionu bazu. Pustiti PRE instalacije nove verzije programa.

-- Provera: tabela TerminalPriv mora imati novu strukturu kolona (TerminalPriv_Izmena_2026_09.sql)
IF COL_LENGTH('dbo.TerminalPriv', 'BL/UVOZ') IS NULL OR COL_LENGTH('dbo.TerminalPriv', 'GATE_OUT_EMPTY/Utovar') IS NULL
BEGIN
    RAISERROR(N'Tabela dbo.TerminalPriv nema novu strukturu kolona. Prvo izvršiti TerminalPriv_Izmena_2026_09.sql. Skripta je prekinuta.', 16, 1);
    SET NOEXEC ON;
END
GO
-- Pokreti Maersk kontejnera (upisuje insTerminalPrivMaerskPokreti pri dugmetu 'Sačuvaj novi' i 'Promeni', izvozi dugme 'Izvoz Maersk').
-- ID je ID zapisa iz TerminalPriv i ponavlja se kad isti kontejner ima više pokreta; MaerskID je ključ zapisa.
IF OBJECT_ID('dbo.TerminalPrivMaersk', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.TerminalPrivMaersk
    (
        MaerskID      int IDENTITY(1,1) NOT NULL,
        ID            int           NOT NULL,
        Terminal      nvarchar(30)  NOT NULL CONSTRAINT DF_TerminalPrivMaersk_Terminal DEFAULT (N'RSSMKTM'),
        [Date]        nvarchar(30)  NULL,
        [Time]        nvarchar(30)  NULL,
        Container     nvarchar(30)  NULL,
        Move          nvarchar(30)  NULL,
        [FULL/EMPTY]  nvarchar(30)  NULL,
        [Act Fore]    nvarchar(30)  NULL,
        WGHT          nvarchar(30)  NULL,
        [BKNG No]     nvarchar(30)  NULL,
        [RAIL CODE]   nvarchar(30)  NULL,
        Seal          nvarchar(30)  NULL,
        [DAM Y/N]     nvarchar(30)  NULL,
        Comment       nvarchar(30)  NULL,
        Active        int           NOT NULL CONSTRAINT DF_TerminalPrivMaersk_Active DEFAULT (0),
        DatumIzvoza   datetime      NULL,
        CONSTRAINT PK_TerminalPrivMaersk PRIMARY KEY CLUSTERED (MaerskID)
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_TerminalPrivMaersk_Active' AND object_id = OBJECT_ID('dbo.TerminalPrivMaersk'))
    CREATE NONCLUSTERED INDEX IX_TerminalPrivMaersk_Active ON dbo.TerminalPrivMaersk (Active);
GO

-- Upis pokreta Maersk kontejnera u TerminalPrivMaersk (pozivaju insTerminalPriv i updTerminalPriv).
-- Za svaki datum koji je prešao iz NULL u popunjen traži se pravilo (datum + GATE_IN/_GATE_OUT + OTPREMA +
-- BOOKING/IZVOZ + BL/UVOZ); pokret se upisuje samo ako pravilo postoji. Stari datumi NULL = novi zapis.
IF OBJECT_ID('dbo.insTerminalPrivMaerskPokreti', 'P') IS NOT NULL DROP PROCEDURE dbo.insTerminalPrivMaerskPokreti;
GO

CREATE PROCEDURE dbo.insTerminalPrivMaerskPokreti
    @ID int,
    @KONTEJNER nvarchar(30),
    @BRODAR nvarchar(30) = NULL,
    @GATE_IN_GATE_OUT nvarchar(50) = NULL,
    @OTPREMA nvarchar(30) = NULL,
    @BOOKING_IZVOZ nvarchar(30) = NULL,
    @BL_UVOZ nvarchar(30) = NULL,
    @GATE_IN_E_F datetime = NULL,
    @PREUZIMANJE_PUNOG_RAZVOZ datetime = NULL,
    @VRACANJE_PRAZNOG_IZ_RAZVOZA datetime = NULL,
    @KONACNI_GATE_OUT datetime = NULL,
    @GATE_OUT_EMPTY_UTOVAR datetime = NULL,
    @GATE_IN_FULL_SA_UTOVARA datetime = NULL,
    @StariGateInEF datetime = NULL,
    @StariPreuzimanje datetime = NULL,
    @StariVracanje datetime = NULL,
    @StariKonacni datetime = NULL,
    @StariGateOutEmpty datetime = NULL,
    @StariGateInFull datetime = NULL,
    @BrojUpisanih int = NULL OUTPUT   -- broj upisanih pokreta (za poruku u formi)
AS
BEGIN
    SET NOCOUNT ON;

    SET @BrojUpisanih = 0;

    -- 1. brodar mora biti Maersk
    IF UPPER(ISNULL(@BRODAR, N'')) NOT LIKE N'%MAERSK%'
        RETURN;

    DECLARE @Booking nvarchar(30) = NULLIF(LTRIM(RTRIM(@BOOKING_IZVOZ)), N'');
    DECLARE @BL nvarchar(30) = NULLIF(LTRIM(RTRIM(@BL_UVOZ)), N'');
    DECLARE @ImaOtpremu bit = CASE WHEN LTRIM(RTRIM(ISNULL(@OTPREMA, N''))) <> N'' THEN 1 ELSE 0 END;
    DECLARE @ImaBooking bit = CASE WHEN @Booking IS NOT NULL THEN 1 ELSE 0 END;
    DECLARE @ImaBL bit = CASE WHEN @BL IS NOT NULL THEN 1 ELSE 0 END;

    INSERT INTO dbo.TerminalPrivMaersk
        (ID, Terminal, [Date], [Time], Container, Move, [FULL/EMPTY], [Act Fore], [BKNG No], [RAIL CODE])
    SELECT
        @ID,
        N'RSSMKTM',
        CONVERT(nvarchar(10), n.Vrednost, 104) + N'.',              -- 30.09.2026.
        LEFT(CONVERT(nvarchar(8), n.Vrednost, 108), 5),             -- 08:00
        @KONTEJNER,
        CASE @GATE_IN_GATE_OUT
            WHEN N'GATE IN E' THEN N'GATE IN' WHEN N'GATE IN F' THEN N'GATE IN'
            WHEN N'GATE OUT E' THEN N'GATE OUT' WHEN N'GATE OUT F' THEN N'GATE OUT' END,
        CASE @GATE_IN_GATE_OUT
            WHEN N'GATE IN E' THEN N'EMPTY' WHEN N'GATE IN F' THEN N'FULL'
            WHEN N'GATE OUT E' THEN N'EMPTY' WHEN N'GATE OUT F' THEN N'FULL' END,
        p.ActFore,
        CASE p.BkngIz WHEN 'B' THEN @Booking WHEN 'L' THEN @BL END,
        CASE WHEN p.RailCode = 1 THEN N'RSSMK' + CONVERT(nvarchar(6), n.Vrednost, 12) END   -- RSSMK260930
    FROM (VALUES
        -- 2. datum koji je prešao iz NULL u popunjen
        ('GATE_IN_EF', @GATE_IN_E_F, @StariGateInEF, 1),
        ('PREUZIMANJE', @PREUZIMANJE_PUNOG_RAZVOZ, @StariPreuzimanje, 2),
        ('VRACANJE', @VRACANJE_PRAZNOG_IZ_RAZVOZA, @StariVracanje, 3),
        ('KONACNI', @KONACNI_GATE_OUT, @StariKonacni, 4),
        ('GATE_OUT_EMPTY', @GATE_OUT_EMPTY_UTOVAR, @StariGateOutEmpty, 5),
        ('GATE_IN_FULL', @GATE_IN_FULL_SA_UTOVARA, @StariGateInFull, 6)
    ) n (Polje, Vrednost, Stara, Redosled)
    INNER JOIN (VALUES
        -- 3. pravila: Otprema/Booking/BL = 1 ima vrednost, 0 nema, NULL svejedno; BkngIz B = Booking, L = BL, NULL = prazno
        --  Polje             GATE_IN/_GATE_OUT  Otpr Book  BL    Act Fore  RAIL CODE  BKNG No
        ('KONACNI',        N'GATE OUT F', 1, 1, NULL, N'EXP', 1, 'B'),
        ('KONACNI',        N'GATE OUT E', 1, 1, NULL, N'POS', 1, 'B'),
        ('KONACNI',        N'GATE OUT E', 0, 1, NULL, NULL,   0, 'B'),
        ('KONACNI',        N'GATE OUT F', 0, 0, 1,    N'DEL', 0, 'L'),
        ('KONACNI',        N'GATE OUT F', 0, 1, NULL, N'EXP', 0, 'B'),
        ('GATE_IN_EF',     N'GATE IN F',  0, 1, NULL, N'EXP', 0, 'B'),
        ('GATE_IN_EF',     N'GATE IN F',  0, 0, 1,    N'IMP', 0, 'L'),
        ('GATE_IN_EF',     N'GATE IN E',  0, 0, 0,    NULL,   0, NULL),
        ('GATE_IN_FULL',   N'GATE IN F',  0, 1, NULL, N'EXP', 0, 'B'),
        ('PREUZIMANJE',    N'GATE OUT F', 0, 0, 1,    N'DEL', 0, 'L'),
        ('VRACANJE',       N'GATE IN E',  0, 0, 1,    NULL,   0, NULL),
        ('GATE_OUT_EMPTY', N'GATE OUT E', 0, 1, NULL, NULL,   0, 'B')
    ) p (Polje, Gate, Otprema, Booking, BL, ActFore, RailCode, BkngIz)
        ON p.Polje = n.Polje
       AND p.Gate = @GATE_IN_GATE_OUT
       AND p.Otprema = @ImaOtpremu
       AND p.Booking = @ImaBooking
       AND (p.BL IS NULL OR p.BL = @ImaBL)
    WHERE n.Stara IS NULL AND n.Vrednost IS NOT NULL
    ORDER BY n.Vrednost, n.Redosled;   -- hronoloski

    SET @BrojUpisanih = @@ROWCOUNT;
END
GO

-- Pokreti MSC kontejnera (upisuje insTerminalPrivMSCPokreti pri dugmetu 'Sačuvaj novi' i 'Promeni', izvozi dugme 'Izvoz MSC').
-- ID je ID zapisa iz TerminalPriv i ponavlja se kad isti kontejner ima više pokreta; MSKID je ključ zapisa.
IF OBJECT_ID('dbo.TerminalPrivMSC', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.TerminalPrivMSC
    (
        MSKID               int IDENTITY(1,1) NOT NULL,
        ID                  int           NOT NULL,
        [Depot Name]        nvarchar(30)  NULL,
        [Depot MSCCode]     nvarchar(30)  NULL,
        [EDI Partner Code]  nvarchar(30)  NULL,
        [Event Location]    nvarchar(30)  NULL,
        [Container Status]  nvarchar(30)  NULL,
        [Full/Empty]        nvarchar(30)  NULL,
        [Container Number]  nvarchar(30)  NULL,
        [Move Date/Time]    nvarchar(30)  NULL,
        [Booking Number]    nvarchar(30)  NULL,
        [Bl Number]         nvarchar(30)  NULL,
        [Remarks]           nvarchar(30)  NULL,
        [Active]            int           NOT NULL CONSTRAINT DF_TerminalPrivMSC_Active DEFAULT (0),
        [DatumIzvoza]       datetime      NULL,
        CONSTRAINT PK_TerminalPrivMSC PRIMARY KEY CLUSTERED (MSKID)
    );
END
GO

-- tabela kreirana pre dodavanja kolone ID
IF COL_LENGTH('dbo.TerminalPrivMSC', 'ID') IS NULL
    ALTER TABLE dbo.TerminalPrivMSC ADD ID int NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_TerminalPrivMSC_Active' AND object_id = OBJECT_ID('dbo.TerminalPrivMSC'))
    CREATE NONCLUSTERED INDEX IX_TerminalPrivMSC_Active ON dbo.TerminalPrivMSC (Active);
GO

-- Upis pokreta MSC kontejnera u TerminalPrivMSC (pozivaju insTerminalPriv i updTerminalPriv), po istom principu
-- kao insTerminalPrivMaerskPokreti: pokret se upisuje samo ako za datum koji je prešao iz NULL u popunjen postoji pravilo.
IF OBJECT_ID('dbo.insTerminalPrivMSCPokreti', 'P') IS NOT NULL DROP PROCEDURE dbo.insTerminalPrivMSCPokreti;
GO

CREATE PROCEDURE dbo.insTerminalPrivMSCPokreti
    @ID int,
    @KONTEJNER nvarchar(30),
    @BRODAR nvarchar(30) = NULL,
    @GATE_IN_GATE_OUT nvarchar(50) = NULL,
    @OTPREMA nvarchar(30) = NULL,
    @BOOKING_IZVOZ nvarchar(30) = NULL,
    @BL_UVOZ nvarchar(30) = NULL,
    @GATE_IN_E_F datetime = NULL,
    @PREUZIMANJE_PUNOG_RAZVOZ datetime = NULL,
    @VRACANJE_PRAZNOG_IZ_RAZVOZA datetime = NULL,
    @KONACNI_GATE_OUT datetime = NULL,
    @GATE_OUT_EMPTY_UTOVAR datetime = NULL,
    @GATE_IN_FULL_SA_UTOVARA datetime = NULL,
    @StariGateInEF datetime = NULL,
    @StariPreuzimanje datetime = NULL,
    @StariVracanje datetime = NULL,
    @StariKonacni datetime = NULL,
    @StariGateOutEmpty datetime = NULL,
    @StariGateInFull datetime = NULL,
    @BrojUpisanih int = NULL OUTPUT   -- broj upisanih pokreta (za poruku u formi)
AS
BEGIN
    SET NOCOUNT ON;

    SET @BrojUpisanih = 0;

    -- brodar mora biti MSC
    IF UPPER(ISNULL(@BRODAR, N'')) NOT LIKE N'%MSC%'
        RETURN;

    DECLARE @Booking nvarchar(30) = NULLIF(LTRIM(RTRIM(@BOOKING_IZVOZ)), N'');
    DECLARE @BL nvarchar(30) = NULLIF(LTRIM(RTRIM(@BL_UVOZ)), N'');
    DECLARE @ImaOtpremu bit = CASE WHEN LTRIM(RTRIM(ISNULL(@OTPREMA, N''))) <> N'' THEN 1 ELSE 0 END;
    DECLARE @ImaBooking bit = CASE WHEN @Booking IS NOT NULL THEN 1 ELSE 0 END;
    DECLARE @ImaBL bit = CASE WHEN @BL IS NOT NULL THEN 1 ELSE 0 END;

    INSERT INTO dbo.TerminalPrivMSC
        (ID, [Depot Name], [Depot MSCCode], [EDI Partner Code], [Event Location], [Container Status], [Full/Empty],
         [Container Number], [Move Date/Time], [Booking Number], [Bl Number])
    SELECT
        @ID,
        N'LEGET',
        N'RSSRMAA',
        N'RSSRMAA',
        N'RSSRM',
        p.ContainerStatus,
        p.FullEmpty,
        @KONTEJNER,
        CONVERT(nvarchar(10), n.Vrednost, 104) + N' ' + LEFT(CONVERT(nvarchar(8), n.Vrednost, 108), 5),   -- 08.10.2026 22:54
        CASE WHEN p.BookingUpis = 1 THEN @Booking END,
        CASE WHEN p.BLUpis = 1 THEN @BL END
    FROM (VALUES
        -- datum koji je prešao iz NULL u popunjen
        ('GATE_IN_EF', @GATE_IN_E_F, @StariGateInEF, 1),
        ('PREUZIMANJE', @PREUZIMANJE_PUNOG_RAZVOZ, @StariPreuzimanje, 2),
        ('VRACANJE', @VRACANJE_PRAZNOG_IZ_RAZVOZA, @StariVracanje, 3),
        ('KONACNI', @KONACNI_GATE_OUT, @StariKonacni, 4),
        ('GATE_OUT_EMPTY', @GATE_OUT_EMPTY_UTOVAR, @StariGateOutEmpty, 5),
        ('GATE_IN_FULL', @GATE_IN_FULL_SA_UTOVARA, @StariGateInFull, 6)
    ) n (Polje, Vrednost, Stara, Redosled)
    INNER JOIN (VALUES
        -- pravila: Otprema/Booking/BL = 1 ima vrednost, 0 nema, NULL svejedno; BookingUpis/BLUpis 1 = upisuje se vrednost
        --  #    Polje            GATE_IN/_GATE_OUT  Otpr  Book  BL    Container Status  Full/Empty  BookingUpis  BLUpis
        ('4.1', 'KONACNI',      N'GATE OUT F', NULL, 1,    NULL, N'ELR', N'FULL',  1, 0),
        ('4.2', 'GATE_IN_EF',   N'GATE IN F',  0,    1,    NULL, N'ERY', N'FULL',  1, 0),
        ('4.3', 'GATE_IN_FULL', N'GATE IN F',  0,    1,    NULL, N'ERY', N'FULL',  1, 0),
        ('4.4', 'GATE_IN_EF',   N'GATE IN F',  0,    0,    1,    N'IDR', N'FULL',  0, 1),
        ('4.5', 'PREUZIMANJE',  N'GATE OUT F', 0,    0,    1,    N'ICO', N'FULL',  0, 1),
        ('4.6', 'KONACNI',      N'GATE OUT F', 0,    0,    1,    N'ICO', N'FULL',  0, 1),
        ('4.7', 'GATE_IN_EF',   N'GATE IN E',  0,    0,    0,    N'MCY', N'EMPTY', 0, 0),
        ('4.8', 'VRACANJE',     N'GATE IN E',  0,    0,    1,    N'MCY', N'EMPTY', 0, 0),
        ('4.9', 'GATE_IN_EF',   N'GATE IN E',  0,    0,    1,    N'MPI', N'EMPTY', 0, 1),
        ('4.10', 'KONACNI',     N'GATE OUT E', 1,    1,    NULL, N'MPO', N'EMPTY', 1, 0),
        ('4.11', 'KONACNI',     N'GATE OUT E', 0,    1,    NULL, N'MSH', N'EMPTY', 1, 0),
        ('4.12', 'GATE_OUT_EMPTY', N'GATE OUT E', 0, 1,    NULL, N'MSH', N'EMPTY', 1, 0)
    ) p (Slucaj, Polje, Gate, Otprema, Booking, BL, ContainerStatus, FullEmpty, BookingUpis, BLUpis)
        ON p.Polje = n.Polje
       AND p.Gate = @GATE_IN_GATE_OUT
       AND (p.Otprema IS NULL OR p.Otprema = @ImaOtpremu)
       AND (p.Booking IS NULL OR p.Booking = @ImaBooking)
       AND (p.BL IS NULL OR p.BL = @ImaBL)
    WHERE n.Stara IS NULL AND n.Vrednost IS NOT NULL
    ORDER BY n.Vrednost, n.Redosled;   -- hronoloski

    SET @BrojUpisanih = @@ROWCOUNT;
END
GO

-- Pokreti CMA kontejnera (upisuje insTerminalPrivCMAPokreti pri dugmetu 'Promeni', izvozi 'Izveštaj CMA').
-- ID je ID zapisa iz TerminalPriv; CMAID je ključ zapisa. RB je redni broj u sledećem Excel fajlu
-- (broj još neizvezenih zapisa + 1).
IF OBJECT_ID('dbo.TerminalPrivCMA', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.TerminalPrivCMA
    (
        CMAID               int IDENTITY(1,1) NOT NULL,
        ID                  int           NOT NULL,
        [RB]                int           NULL,
        [CONTAINER STATUS]  nvarchar(30)  NULL,
        [opis]              nvarchar(60)  NULL,
        [MOVE TYPE]         nvarchar(30)  NULL,
        [CONTAINER NUMBER]  nvarchar(30)  NULL,
        [Size]              nvarchar(30)  NULL,
        [Type]              nvarchar(30)  NULL,
        [S/T]               nvarchar(30)  NULL,
        [ISO CODE]          nvarchar(30)  NULL,
        [EXPORT / IMPORT]   nvarchar(30)  NULL,
        [FULL/EMPTY]        nvarchar(30)  NULL,
        [BOOKING NUMBER]    nvarchar(30)  NULL,
        [MOVE DATE/TIME]    nvarchar(30)  NULL,
        [TRANSPORT]         nvarchar(30)  NULL,
        [Active]            int           NOT NULL CONSTRAINT DF_TerminalPrivCMA_Active DEFAULT (0),
        [DatumIzvoza]       datetime      NULL,
        CONSTRAINT PK_TerminalPrivCMA PRIMARY KEY CLUSTERED (CMAID)
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_TerminalPrivCMA_Active' AND object_id = OBJECT_ID('dbo.TerminalPrivCMA'))
    CREATE NONCLUSTERED INDEX IX_TerminalPrivCMA_Active ON dbo.TerminalPrivCMA (Active);
GO

-- Upis pokreta CMA kontejnera u TerminalPrivCMA (pozivaju insTerminalPriv i updTerminalPriv), po istom principu
-- kao insTerminalPrivMaerskPokreti: pokret se upisuje samo ako za datum koji je prešao iz NULL u popunjen postoji pravilo.
IF OBJECT_ID('dbo.insTerminalPrivCMAPokreti', 'P') IS NOT NULL DROP PROCEDURE dbo.insTerminalPrivCMAPokreti;
GO

CREATE PROCEDURE dbo.insTerminalPrivCMAPokreti
    @ID int,
    @KONTEJNER nvarchar(30),
    @BRODAR nvarchar(30) = NULL,
    @VRSTA nvarchar(30) = NULL,
    @VOZ_KAMION nvarchar(30) = NULL,
    @GATE_IN_GATE_OUT nvarchar(50) = NULL,
    @OTPREMA nvarchar(30) = NULL,
    @BOOKING_IZVOZ nvarchar(30) = NULL,
    @BL_UVOZ nvarchar(30) = NULL,
    @GATE_IN_E_F datetime = NULL,
    @PREUZIMANJE_PUNOG_RAZVOZ datetime = NULL,
    @VRACANJE_PRAZNOG_IZ_RAZVOZA datetime = NULL,
    @KONACNI_GATE_OUT datetime = NULL,
    @GATE_OUT_EMPTY_UTOVAR datetime = NULL,
    @GATE_IN_FULL_SA_UTOVARA datetime = NULL,
    @StariGateInEF datetime = NULL,
    @StariPreuzimanje datetime = NULL,
    @StariVracanje datetime = NULL,
    @StariKonacni datetime = NULL,
    @StariGateOutEmpty datetime = NULL,
    @StariGateInFull datetime = NULL,
    @BrojUpisanih int = NULL OUTPUT   -- broj upisanih pokreta (za poruku u formi)
AS
BEGIN
    SET NOCOUNT ON;

    SET @BrojUpisanih = 0;

    -- brodar mora biti CMA
    IF UPPER(ISNULL(@BRODAR, N'')) NOT LIKE N'%CMA%'
        RETURN;

    DECLARE @Booking nvarchar(30) = NULLIF(LTRIM(RTRIM(@BOOKING_IZVOZ)), N'');
    DECLARE @BL nvarchar(30) = NULLIF(LTRIM(RTRIM(@BL_UVOZ)), N'');
    DECLARE @ImaOtpremu bit = CASE WHEN LTRIM(RTRIM(ISNULL(@OTPREMA, N''))) <> N'' THEN 1 ELSE 0 END;
    DECLARE @ImaBooking bit = CASE WHEN @Booking IS NOT NULL THEN 1 ELSE 0 END;
    DECLARE @ImaBL bit = CASE WHEN @BL IS NOT NULL THEN 1 ELSE 0 END;
    -- VOZ/kamion: KAMION = tačno ta vrednost (bez obzira na velika/mala slova); sve ostalo, i prazno, nije KAMION
    DECLARE @Kamion bit = CASE WHEN UPPER(LTRIM(RTRIM(ISNULL(@VOZ_KAMION, N'')))) = N'KAMION' THEN 1 ELSE 0 END;

    -- VRSTA iz šifarnika (npr. 20' DB, 40' FLAT): Size = prva 3 karaktera, Type = ostatak posle razmaka
    DECLARE @VrstaCnt nvarchar(30) = NULLIF(LTRIM(RTRIM(@VRSTA)), N'');
    DECLARE @Size nvarchar(30) = NULLIF(LEFT(@VrstaCnt, 3), N'');
    DECLARE @Type nvarchar(30) = NULLIF(LTRIM(SUBSTRING(@VrstaCnt, 4, 30)), N'');
    DECLARE @Iso nvarchar(30) =
        (SELECT i.Iso FROM (VALUES
            (N'20''', N'DB', N'22G1'), (N'40''', N'DB', N'42G1'), (N'45''', N'HC', N'55G1'),
            (N'20''', N'HC', N'25G1'), (N'40''', N'HC', N'45G1'), (N'20''', N'OT', N'22U1'),
            (N'40''', N'FLAT', N'49P3'), (N'40''', N'RF', N'45H0')
         ) i (Size, Type, Iso)
         WHERE i.Size = @Size AND i.Type = @Type);

    -- RB: nastavlja se na broj još neizvezenih zapisa
    DECLARE @Aktivnih int = (SELECT COUNT(*) FROM dbo.TerminalPrivCMA WHERE Active <> 1);

    INSERT INTO dbo.TerminalPrivCMA
        (ID, [RB], [CONTAINER STATUS], [opis], [MOVE TYPE], [CONTAINER NUMBER], [Size], [Type], [S/T], [ISO CODE],
         [EXPORT / IMPORT], [FULL/EMPTY], [BOOKING NUMBER], [MOVE DATE/TIME], [TRANSPORT])
    SELECT
        @ID,
        @Aktivnih + ROW_NUMBER() OVER (ORDER BY n.Vrednost, n.Redosled),
        p.ContainerStatus,
        p.Opis,
        p.MoveType,
        @KONTEJNER,
        @Size,
        @Type,
        CASE WHEN @Size IS NOT NULL AND @Type IS NOT NULL THEN @Size + N'-' + @Type END,   -- 20'-DB
        @Iso,
        p.ExportImport,
        p.FullEmpty,
        CASE p.BkngIz WHEN 'B' THEN @Booking WHEN 'L' THEN @BL END,
        CONVERT(nvarchar(10), n.Vrednost, 104) + N'. ' + LEFT(CONVERT(nvarchar(8), n.Vrednost, 108), 5),   -- 08.10.2026. 22:54
        p.Transport
    FROM (VALUES
        -- datum koji je prešao iz NULL u popunjen
        ('GATE_IN_EF', @GATE_IN_E_F, @StariGateInEF, 1),
        ('PREUZIMANJE', @PREUZIMANJE_PUNOG_RAZVOZ, @StariPreuzimanje, 2),
        ('VRACANJE', @VRACANJE_PRAZNOG_IZ_RAZVOZA, @StariVracanje, 3),
        ('KONACNI', @KONACNI_GATE_OUT, @StariKonacni, 4),
        ('GATE_OUT_EMPTY', @GATE_OUT_EMPTY_UTOVAR, @StariGateOutEmpty, 5),
        ('GATE_IN_FULL', @GATE_IN_FULL_SA_UTOVARA, @StariGateInFull, 6)
    ) n (Polje, Vrednost, Stara, Redosled)
    INNER JOIN (VALUES
        -- pravila: Otprema/Booking/BL/Kamion = 1 ima (KAMION), 0 nema (nije KAMION), NULL svejedno;
        -- BkngIz B = Booking/Izvoz, L = BL/Uvoz
        --  #    Polje            GATE_IN/_GATE_OUT  Otpr  Book  BL    Kam   Status  Opis                                       Move Type    Exp/Imp     Full/Empty  BkngIz  Transport
        (1,  'KONACNI',      N'GATE OUT F', 1,    1,    NULL, NULL, N'TPF', N'UKRCAJ PUNOG NA VOZ',                    N'GATE OUT', N'EXPORT', N'FULL',  'B', N'RAIL'),
        (2,  'GATE_IN_EF',   N'GATE IN F',  0,    1,    NULL, NULL, N'IIT', N'POVRATAK PUNOG KONTEJNERA NA TERMINAL',  N'GATE IN',  N'EXPORT', N'FULL',  'B', N'TRUCK'),
        (3,  'GATE_IN_FULL', N'GATE IN F',  0,    1,    NULL, NULL, N'IIT', N'POVRATAK PUNOG KONTEJNERA NA TERMINAL',  N'GATE IN',  N'EXPORT', N'FULL',  'B', N'TRUCK'),
        (4,  'GATE_IN_EF',   N'GATE IN F',  0,    0,    1,    0,    N'IRI', N'DOLAZAK PUNOG NA TERMINAL',              N'GATE IN',  N'IMPORT', N'FULL',  'L', N'RAIL'),
        (5,  'PREUZIMANJE',  N'GATE OUT F', 0,    0,    1,    NULL, N'IFC', N'PREUZIMANJE PUNOG SA TERMINALA',         N'GATE OUT', N'IMPORT', N'FULL',  'L', N'TRUCK'),
        (6,  'KONACNI',      N'GATE OUT F', 0,    0,    1,    NULL, N'IFC', N'PREUZIMANJE PUNOG SA TERMINALA',         N'GATE OUT', N'IMPORT', N'FULL',  'L', N'TRUCK'),
        (7,  'GATE_IN_EF',   N'GATE IN E',  0,    0,    0,    1,    N'MEA', N'DOLAZAK PRAZNOG NA TERMINAL',            N'GATE IN',  N'EXPORT', N'EMPTY', 'L', N'TRUCK'),
        (8,  'VRACANJE',     N'GATE IN E',  0,    0,    1,    NULL, N'MEA', N'POVRATAK PRAZNOG NA TERMINAL',           N'GATE IN',  N'IMPORT', N'EMPTY', 'L', N'TRUCK'),
        (9,  'GATE_IN_EF',   N'GATE IN E',  0,    0,    1,    0,    N'MEA', N'DOLAZAK PRAZNOG NA TERMINAL VOZ',        N'GATE IN',  N'IMPORT', N'EMPTY', 'L', N'RAIL'),
        (10, 'KONACNI',      N'GATE OUT F', 0,    1,    NULL, NULL, N'TPF', N'UKRCAJ PUNOG NA KAMION',                 N'GATE OUT', N'EXPORT', N'FULL',  'B', N'TRUCK'),
        (11, 'GATE_IN_EF',   N'GATE IN F',  0,    0,    1,    1,    N'IRI', N'DOLAZAK PUNOG NA TERMINAL',              N'GATE IN',  N'IMPORT', N'FULL',  'L', N'TRUCK'),
        (12, 'KONACNI',      N'GATE OUT E', 1,    1,    NULL, NULL, N'MIT', N'REPOZICIONIRANJE',                       N'GATE OUT', N'EXPORT', N'EMPTY', 'B', N'RAIL'),
        (13, 'KONACNI',      N'GATE OUT E', 0,    1,    NULL, NULL, N'MOS', N'PREUZIMANJE PRAZNOG S TERMINALA',        N'GATE OUT', N'EXPORT', N'EMPTY', 'B', N'TRUCK'),
        (14, 'GATE_OUT_EMPTY', N'GATE OUT E', 0,  1,    NULL, NULL, N'MOS', N'PREUZIMANJE PRAZNOG S TERMINALA',        N'GATE OUT', N'EXPORT', N'EMPTY', 'B', N'TRUCK')
    ) p (Slucaj, Polje, Gate, Otprema, Booking, BL, Kamion, ContainerStatus, Opis, MoveType, ExportImport, FullEmpty, BkngIz, Transport)
        ON p.Polje = n.Polje
       AND p.Gate = @GATE_IN_GATE_OUT
       AND (p.Otprema IS NULL OR p.Otprema = @ImaOtpremu)
       AND (p.Booking IS NULL OR p.Booking = @ImaBooking)
       AND (p.BL IS NULL OR p.BL = @ImaBL)
       AND (p.Kamion IS NULL OR p.Kamion = @Kamion)
    WHERE n.Stara IS NULL AND n.Vrednost IS NOT NULL
    ORDER BY n.Vrednost, n.Redosled;   -- hronoloski

    SET @BrojUpisanih = @@ROWCOUNT;
END
GO

-- Pokreti OOCL kontejnera (upisuje insTerminalPrivOOCLPokreti pri dugmetu 'Promeni', izvozi 'Izveštaj OOCL').
-- ID je ID zapisa iz TerminalPriv; OOCLID je ključ zapisa. RB je redni broj u sledećem Excel fajlu
-- (broj još neizvezenih zapisa + 1).
IF OBJECT_ID('dbo.TerminalPrivOOCL', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.TerminalPrivOOCL
    (
        OOCLID              int IDENTITY(1,1) NOT NULL,
        ID                  int           NOT NULL,
        [RB]                int           NULL,
        [CONTAINER STATUS]  nvarchar(30)  NULL,
        [opis]              nvarchar(60)  NULL,
        [MOVE TYPE]         nvarchar(30)  NULL,
        [CONTAINER NUMBER]  nvarchar(30)  NULL,
        [Size]              nvarchar(30)  NULL,
        [Type]              nvarchar(30)  NULL,
        [S/T]               nvarchar(30)  NULL,
        [ISO CODE]          nvarchar(30)  NULL,
        [EXPORT / IMPORT]   nvarchar(30)  NULL,
        [FULL/EMPTY]        nvarchar(30)  NULL,
        [BOOKING NUMBER]    nvarchar(30)  NULL,
        [MOVE DATE/TIME]    nvarchar(30)  NULL,
        [TRANSPORT]         nvarchar(30)  NULL,
        [DAMAGE]            nvarchar(30)  NULL,
        [Active]            int           NOT NULL CONSTRAINT DF_TerminalPrivOOCL_Active DEFAULT (0),
        [DatumIzvoza]       datetime      NULL,
        CONSTRAINT PK_TerminalPrivOOCL PRIMARY KEY CLUSTERED (OOCLID)
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_TerminalPrivOOCL_Active' AND object_id = OBJECT_ID('dbo.TerminalPrivOOCL'))
    CREATE NONCLUSTERED INDEX IX_TerminalPrivOOCL_Active ON dbo.TerminalPrivOOCL (Active);
GO

-- Upis pokreta OOCL kontejnera u TerminalPrivOOCL (pozivaju insTerminalPriv i updTerminalPriv), po istom principu
-- kao insTerminalPrivCMAPokreti; pravila 15-20 važe samo za STANJE = OŠTEĆEN, pravila 1-14 za sva ostala stanja.
IF OBJECT_ID('dbo.insTerminalPrivOOCLPokreti', 'P') IS NOT NULL DROP PROCEDURE dbo.insTerminalPrivOOCLPokreti;
GO
CREATE PROCEDURE dbo.insTerminalPrivOOCLPokreti
    @ID int,
    @KONTEJNER nvarchar(30),
    @BRODAR nvarchar(30) = NULL,
    @VRSTA nvarchar(30) = NULL,
    @VOZ_KAMION nvarchar(30) = NULL,
    @STANJE nvarchar(30) = NULL,
    @GATE_IN_GATE_OUT nvarchar(50) = NULL,
    @OTPREMA nvarchar(30) = NULL,
    @BOOKING_IZVOZ nvarchar(30) = NULL,
    @BL_UVOZ nvarchar(30) = NULL,
    @GATE_IN_E_F datetime = NULL,
    @PREUZIMANJE_PUNOG_RAZVOZ datetime = NULL,
    @VRACANJE_PRAZNOG_IZ_RAZVOZA datetime = NULL,
    @KONACNI_GATE_OUT datetime = NULL,
    @GATE_OUT_EMPTY_UTOVAR datetime = NULL,
    @GATE_IN_FULL_SA_UTOVARA datetime = NULL,
    @StariGateInEF datetime = NULL,
    @StariPreuzimanje datetime = NULL,
    @StariVracanje datetime = NULL,
    @StariKonacni datetime = NULL,
    @StariGateOutEmpty datetime = NULL,
    @StariGateInFull datetime = NULL,
    @BrojUpisanih int = NULL OUTPUT   -- broj upisanih pokreta (za poruku u formi)
AS
BEGIN
    SET NOCOUNT ON;

    SET @BrojUpisanih = 0;

    -- brodar mora biti OOCL
    IF UPPER(ISNULL(@BRODAR, N'')) NOT LIKE N'%OOCL%'
        RETURN;

    DECLARE @Booking nvarchar(30) = NULLIF(LTRIM(RTRIM(@BOOKING_IZVOZ)), N'');
    DECLARE @BL nvarchar(30) = NULLIF(LTRIM(RTRIM(@BL_UVOZ)), N'');
    DECLARE @ImaOtpremu bit = CASE WHEN LTRIM(RTRIM(ISNULL(@OTPREMA, N''))) <> N'' THEN 1 ELSE 0 END;
    DECLARE @ImaBooking bit = CASE WHEN @Booking IS NOT NULL THEN 1 ELSE 0 END;
    DECLARE @ImaBL bit = CASE WHEN @BL IS NOT NULL THEN 1 ELSE 0 END;
    -- VOZ/kamion: KAMION = tačno ta vrednost (bez obzira na velika/mala slova); sve ostalo, i prazno, nije KAMION
    DECLARE @Kamion bit = CASE WHEN UPPER(LTRIM(RTRIM(ISNULL(@VOZ_KAMION, N'')))) = N'KAMION' THEN 1 ELSE 0 END;
    DECLARE @Ostecen bit = CASE WHEN UPPER(LTRIM(RTRIM(ISNULL(@STANJE, N'')))) = N'OŠTEĆEN' THEN 1 ELSE 0 END;

    -- VRSTA iz šifarnika (npr. 20' DB, 40' FLAT): Size = prva 3 karaktera, Type = ostatak posle razmaka
    DECLARE @VrstaCnt nvarchar(30) = NULLIF(LTRIM(RTRIM(@VRSTA)), N'');
    DECLARE @Size nvarchar(30) = NULLIF(LEFT(@VrstaCnt, 3), N'');
    DECLARE @Type nvarchar(30) = NULLIF(LTRIM(SUBSTRING(@VrstaCnt, 4, 30)), N'');
    DECLARE @Iso nvarchar(30) =
        (SELECT i.Iso FROM (VALUES
            (N'20''', N'DB', N'22G1'), (N'40''', N'DB', N'42G1'), (N'45''', N'HC', N'55G1'),
            (N'20''', N'HC', N'25G1'), (N'40''', N'HC', N'45G1'), (N'20''', N'OT', N'22U1'),
            (N'40''', N'FLAT', N'49P3'), (N'40''', N'RF', N'45H0')
         ) i (Size, Type, Iso)
         WHERE i.Size = @Size AND i.Type = @Type);

    -- RB: nastavlja se na broj još neizvezenih zapisa
    DECLARE @Aktivnih int = (SELECT COUNT(*) FROM dbo.TerminalPrivOOCL WHERE Active <> 1);

    INSERT INTO dbo.TerminalPrivOOCL
        (ID, [RB], [CONTAINER STATUS], [opis], [MOVE TYPE], [CONTAINER NUMBER], [Size], [Type], [S/T], [ISO CODE],
         [EXPORT / IMPORT], [FULL/EMPTY], [BOOKING NUMBER], [MOVE DATE/TIME], [TRANSPORT], [DAMAGE])
    SELECT
        @ID,
        @Aktivnih + ROW_NUMBER() OVER (ORDER BY n.Vrednost, n.Redosled),
        p.ContainerStatus,
        p.Opis,
        p.MoveType,
        @KONTEJNER,
        @Size,
        @Type,
        CASE WHEN @Size IS NOT NULL AND @Type IS NOT NULL THEN @Size + N'-' + @Type END,   -- 20'-DB
        @Iso,
        p.ExportImport,
        p.FullEmpty,
        CASE p.BkngIz WHEN 'B' THEN @Booking WHEN 'L' THEN @BL END,
        CONVERT(nvarchar(10), n.Vrednost, 104) + N'. ' + LEFT(CONVERT(nvarchar(8), n.Vrednost, 108), 5),   -- 08.10.2026. 22:54
        p.Transport,
        CASE WHEN @Ostecen = 1 THEN N'DA' ELSE N'NE' END
    FROM (VALUES
        -- datum koji je prešao iz NULL u popunjen
        ('GATE_IN_EF', @GATE_IN_E_F, @StariGateInEF, 1),
        ('PREUZIMANJE', @PREUZIMANJE_PUNOG_RAZVOZ, @StariPreuzimanje, 2),
        ('VRACANJE', @VRACANJE_PRAZNOG_IZ_RAZVOZA, @StariVracanje, 3),
        ('KONACNI', @KONACNI_GATE_OUT, @StariKonacni, 4),
        ('GATE_OUT_EMPTY', @GATE_OUT_EMPTY_UTOVAR, @StariGateOutEmpty, 5),
        ('GATE_IN_FULL', @GATE_IN_FULL_SA_UTOVARA, @StariGateInFull, 6)
    ) n (Polje, Vrednost, Stara, Redosled)
    INNER JOIN (VALUES
        -- pravila: Otprema/Booking/BL/Kamion = 1 ima (KAMION), 0 nema (nije KAMION), NULL svejedno; Ost 1 = STANJE OŠTEĆEN;
        -- BkngIz B = Booking/Izvoz, L = BL/Uvoz, N = ništa
        --  #    Polje              GATE_IN/_GATE_OUT  Otpr  Book  BL    Kam   Ost  Status    Opis                                       Move Type    Exp/Imp     Full/Empty  BkngIz  Transport
        (1,  'KONACNI',        N'GATE OUT E', 1,    1,    NULL, NULL, 0, N'MIT',   N'REPOZICIONIRANJE',                       N'GATE OUT', N'EXPORT', N'EMPTY', 'B', N'RAIL'),
        (2,  'KONACNI',        N'GATE OUT E', 0,    1,    NULL, NULL, 0, N'MOS',   N'PREUZIMANJE PRAZNOG S TERMINALA',        N'GATE OUT', N'EXPORT', N'EMPTY', 'B', N'TRUCK'),
        (3,  'GATE_OUT_EMPTY', N'GATE OUT E', 0,    1,    NULL, NULL, 0, N'MOS',   N'PREUZIMANJE PRAZNOG S TERMINALA',        N'GATE OUT', N'EXPORT', N'EMPTY', 'B', N'TRUCK'),
        (4,  'KONACNI',        N'GATE OUT F', 1,    1,    NULL, NULL, 0, N'TPF',   N'UKRCAJ PUNOG NA VOZ',                    N'GATE OUT', N'EXPORT', N'FULL',  'B', N'RAIL'),
        (5,  'GATE_IN_EF',     N'GATE IN F',  0,    1,    NULL, NULL, 0, N'IIT',   N'POVRATAK PUNOG KONTEJNERA NA TERMINAL',  N'GATE IN',  N'EXPORT', N'FULL',  'B', N'TRUCK'),
        (6,  'GATE_IN_FULL',   N'GATE IN F',  0,    1,    NULL, NULL, 0, N'IIT',   N'POVRATAK PUNOG KONTEJNERA NA TERMINAL',  N'GATE IN',  N'EXPORT', N'FULL',  'B', N'TRUCK'),
        (7,  'GATE_IN_EF',     N'GATE IN F',  0,    0,    1,    0,    0, N'IRI',   N'DOLAZAK PUNOG NA TERMINAL',              N'GATE IN',  N'IMPORT', N'FULL',  'L', N'RAIL'),
        (8,  'PREUZIMANJE',    N'GATE OUT F', 0,    0,    1,    NULL, 0, N'IFC',   N'PREUZIMANJE PUNOG SA TERMINALA',         N'GATE OUT', N'IMPORT', N'FULL',  'L', N'TRUCK'),
        (9,  'KONACNI',        N'GATE OUT F', 0,    0,    1,    NULL, 0, N'IFC',   N'PREUZIMANJE PUNOG SA TERMINALA',         N'GATE OUT', N'IMPORT', N'FULL',  'L', N'TRUCK'),
        (10, 'GATE_IN_EF',     N'GATE IN E',  0,    0,    0,    1,    0, N'MEA',   N'DOLAZAK PRAZNOG NA TERMINAL',            N'GATE IN',  N'EXPORT', N'EMPTY', 'N', N'TRUCK'),
        (11, 'VRACANJE',       N'GATE IN E',  0,    0,    1,    NULL, 0, N'MEA',   N'POVRATAK PRAZNOG NA TERMINAL',           N'GATE IN',  N'IMPORT', N'EMPTY', 'N', N'TRUCK'),
        (12, 'GATE_IN_EF',     N'GATE IN E',  0,    0,    1,    0,    0, N'MEA',   N'DOLAZAK PRAZNOG NA TERMINAL VOZ',        N'GATE IN',  N'IMPORT', N'EMPTY', 'L', N'RAIL'),
        (13, 'KONACNI',        N'GATE OUT F', 0,    1,    NULL, NULL, 0, N'TPF',   N'UKRCAJ PUNOG NA KAMION',                 N'GATE OUT', N'EXPORT', N'FULL',  'B', N'TRUCK'),
        (14, 'GATE_IN_EF',     N'GATE IN F',  0,    0,    1,    1,    0, N'IRI',   N'DOLAZAK PUNOG NA TERMINAL',              N'GATE IN',  N'IMPORT', N'FULL',  'L', N'TRUCK'),
        (15, 'GATE_IN_EF',     N'GATE IN E',  0,    0,    0,    1,    1, N'MEA_o', N'DOLAZAK PRAZNOG NA TERMINAL',            N'GATE IN',  N'EXPORT', N'EMPTY', 'N', N'TRUCK'),
        (16, 'VRACANJE',       N'GATE IN E',  0,    0,    1,    NULL, 1, N'MEA_o', N'POVRATAK PRAZNOG NA TERMINAL',           N'GATE IN',  N'IMPORT', N'EMPTY', 'N', N'TRUCK'),
        (17, 'GATE_IN_EF',     N'GATE IN E',  0,    0,    1,    0,    1, N'MEA_o', N'DOLAZAK PRAZNOG NA TERMINAL VOZ',        N'GATE IN',  N'IMPORT', N'EMPTY', 'L', N'RAIL'),
        (18, 'KONACNI',        N'GATE OUT E', 0,    1,    NULL, NULL, 1, N'MOS_o', N'PREUZIMANJE PRAZNOG S TERMINALA',        N'GATE OUT', N'EXPORT', N'EMPTY', 'B', N'TRUCK'),
        (19, 'GATE_OUT_EMPTY', N'GATE OUT E', 0,    1,    NULL, NULL, 1, N'MOS_o', N'PREUZIMANJE PRAZNOG S TERMINALA',        N'GATE OUT', N'EXPORT', N'EMPTY', 'B', N'TRUCK'),
        (20, 'KONACNI',        N'GATE OUT E', 1,    1,    NULL, NULL, 1, N'MIT_o', N'REPOZICIONIRANJE',                       N'GATE OUT', N'EXPORT', N'EMPTY', 'B', N'RAIL')
    ) p (Slucaj, Polje, Gate, Otprema, Booking, BL, Kamion, Ostecen, ContainerStatus, Opis, MoveType, ExportImport, FullEmpty, BkngIz, Transport)
        ON p.Polje = n.Polje
       AND p.Gate = @GATE_IN_GATE_OUT
       AND (p.Otprema IS NULL OR p.Otprema = @ImaOtpremu)
       AND (p.Booking IS NULL OR p.Booking = @ImaBooking)
       AND (p.BL IS NULL OR p.BL = @ImaBL)
       AND (p.Kamion IS NULL OR p.Kamion = @Kamion)
       AND p.Ostecen = @Ostecen
    WHERE n.Stara IS NULL AND n.Vrednost IS NOT NULL
    ORDER BY n.Vrednost, n.Redosled;   -- hronoloski

    SET @BrojUpisanih = @@ROWCOUNT;
END
GO

-- Pokreti HMM kontejnera (upisuje insTerminalPrivHMMPokreti pri dugmetu 'Promeni', izvozi 'Izveštaj HMM').
-- ID je ID zapisa iz TerminalPriv; HMMID je ključ zapisa.
IF OBJECT_ID('dbo.TerminalPrivHMM', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.TerminalPrivHMM
    (
        HMMID               int IDENTITY(1,1) NOT NULL,
        ID                  int           NOT NULL,
        [Container]         nvarchar(30)  NULL,
        [Type]              nvarchar(30)  NULL,
        [Size]              nvarchar(30)  NULL,
        [Full/Empty]        nvarchar(30)  NULL,
        [Booking number]    nvarchar(30)  NULL,
        [1st MOVE]          nvarchar(30)  NULL,
        [DATE OF THE MOVE]  nvarchar(30)  NULL,
        [Active]            int           NOT NULL CONSTRAINT DF_TerminalPrivHMM_Active DEFAULT (0),
        [DatumIzvoza]       datetime      NULL,
        CONSTRAINT PK_TerminalPrivHMM PRIMARY KEY CLUSTERED (HMMID)
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_TerminalPrivHMM_Active' AND object_id = OBJECT_ID('dbo.TerminalPrivHMM'))
    CREATE NONCLUSTERED INDEX IX_TerminalPrivHMM_Active ON dbo.TerminalPrivHMM (Active);
GO

-- Upis pokreta HMM kontejnera u TerminalPrivHMM (pozivaju insTerminalPriv i updTerminalPriv), po istom principu
-- kao insTerminalPrivCMAPokreti: pokret se upisuje samo ako za datum koji je prešao iz NULL u popunjen postoji pravilo.
IF OBJECT_ID('dbo.insTerminalPrivHMMPokreti', 'P') IS NOT NULL DROP PROCEDURE dbo.insTerminalPrivHMMPokreti;
GO
CREATE PROCEDURE dbo.insTerminalPrivHMMPokreti
    @ID int,
    @KONTEJNER nvarchar(30),
    @BRODAR nvarchar(30) = NULL,
    @VRSTA nvarchar(30) = NULL,
    @VOZ_KAMION nvarchar(30) = NULL,
    @GATE_IN_GATE_OUT nvarchar(50) = NULL,
    @BOOKING_IZVOZ nvarchar(30) = NULL,
    @PLOMBA_IZVOZ nvarchar(30) = NULL,
    @BL_UVOZ nvarchar(30) = NULL,
    @GATE_IN_E_F datetime = NULL,
    @PREUZIMANJE_PUNOG_RAZVOZ datetime = NULL,
    @VRACANJE_PRAZNOG_IZ_RAZVOZA datetime = NULL,
    @KONACNI_GATE_OUT datetime = NULL,
    @GATE_OUT_EMPTY_UTOVAR datetime = NULL,
    @GATE_IN_FULL_SA_UTOVARA datetime = NULL,
    @StariGateInEF datetime = NULL,
    @StariPreuzimanje datetime = NULL,
    @StariVracanje datetime = NULL,
    @StariKonacni datetime = NULL,
    @StariGateOutEmpty datetime = NULL,
    @StariGateInFull datetime = NULL,
    @BrojUpisanih int = NULL OUTPUT   -- broj upisanih pokreta (za poruku u formi)
AS
BEGIN
    SET NOCOUNT ON;

    SET @BrojUpisanih = 0;

    -- brodar mora biti HMM
    IF UPPER(ISNULL(@BRODAR, N'')) NOT LIKE N'%HMM%'
        RETURN;

    DECLARE @Booking nvarchar(30) = NULLIF(LTRIM(RTRIM(@BOOKING_IZVOZ)), N'');
    DECLARE @BL nvarchar(30) = NULLIF(LTRIM(RTRIM(@BL_UVOZ)), N'');
    DECLARE @ImaBooking bit = CASE WHEN @Booking IS NOT NULL THEN 1 ELSE 0 END;
    DECLARE @ImaPlombu bit = CASE WHEN LTRIM(RTRIM(ISNULL(@PLOMBA_IZVOZ, N''))) <> N'' THEN 1 ELSE 0 END;
    DECLARE @ImaBL bit = CASE WHEN @BL IS NOT NULL THEN 1 ELSE 0 END;
    -- VOZ/kamion: KAMION = tačno ta vrednost (bez obzira na velika/mala slova); sve ostalo, i prazno, nije KAMION
    DECLARE @Kamion bit = CASE WHEN UPPER(LTRIM(RTRIM(ISNULL(@VOZ_KAMION, N'')))) = N'KAMION' THEN 1 ELSE 0 END;

    -- VRSTA iz šifarnika (npr. 20' DB, 40' FLAT): Size = broj sa apostrofom (20'), Type = ostatak posle razmaka
    DECLARE @VrstaCnt nvarchar(30) = NULLIF(LTRIM(RTRIM(@VRSTA)), N'');
    DECLARE @Size nvarchar(30) = NULLIF(LTRIM(RTRIM(REPLACE(REPLACE(LEFT(@VrstaCnt, 3), N'''', N''), N'’', N''))), N'') + N'''';
    DECLARE @Type nvarchar(30) = NULLIF(LTRIM(SUBSTRING(@VrstaCnt, 4, 30)), N'');

    INSERT INTO dbo.TerminalPrivHMM
        (ID, [Container], [Type], [Size], [Full/Empty], [Booking number], [1st MOVE], [DATE OF THE MOVE])
    SELECT
        @ID,
        @KONTEJNER,
        @Type,
        @Size,
        p.FullEmpty,
        CASE p.BkngIz WHEN 'B' THEN @Booking WHEN 'L' THEN @BL END,
        p.Move,
        CONVERT(nvarchar(10), n.Vrednost, 104) + N'.'   -- 08.10.2026.
    FROM (VALUES
        -- datum koji je prešao iz NULL u popunjen
        ('GATE_IN_EF', @GATE_IN_E_F, @StariGateInEF, 1),
        ('PREUZIMANJE', @PREUZIMANJE_PUNOG_RAZVOZ, @StariPreuzimanje, 2),
        ('VRACANJE', @VRACANJE_PRAZNOG_IZ_RAZVOZA, @StariVracanje, 3),
        ('KONACNI', @KONACNI_GATE_OUT, @StariKonacni, 4),
        ('GATE_OUT_EMPTY', @GATE_OUT_EMPTY_UTOVAR, @StariGateOutEmpty, 5),
        ('GATE_IN_FULL', @GATE_IN_FULL_SA_UTOVARA, @StariGateInFull, 6)
    ) n (Polje, Vrednost, Stara, Redosled)
    INNER JOIN (VALUES
        -- pravila: Booking/Plomba/BL/Kamion = 1 ima (KAMION), 0 nema (nije KAMION), NULL svejedno; Otprema se ne gleda;
        -- BkngIz B = Booking/Izvoz, L = BL/Uvoz, N = ništa. Slučaj 2 iz specifikacije je isti kao 1, pa se ne ponavlja.
        --  #    Polje              GATE_IN/_GATE_OUT  Book  Plomba  Kam   BL    Full/Empty  BkngIz  1st MOVE
        (1,  'KONACNI',        N'GATE OUT E', 1,    NULL,   NULL, NULL, N'EMPTY', 'B', N'GATE OUT'),
        (3,  'GATE_OUT_EMPTY', N'GATE OUT E', 1,    NULL,   NULL, NULL, N'EMPTY', 'B', N'GATE OUT'),
        (4,  'KONACNI',        N'GATE OUT F', 1,    NULL,   NULL, NULL, N'FULL',  'B', N'GATE OUT'),
        (5,  'GATE_IN_EF',     N'GATE IN F',  1,    NULL,   NULL, NULL, N'FULL',  'B', N'GATE IN'),
        (6,  'GATE_IN_FULL',   N'GATE IN F',  1,    NULL,   NULL, NULL, N'FULL',  'B', N'GATE IN'),
        (7,  'GATE_IN_EF',     N'GATE IN F',  0,    0,      NULL, 1,    N'FULL',  'L', N'GATE IN'),
        (8,  'PREUZIMANJE',    N'GATE OUT F', 0,    0,      NULL, 1,    N'FULL',  'L', N'GATE OUT'),
        (9,  'KONACNI',        N'GATE OUT F', 0,    0,      NULL, 1,    N'FULL',  'L', N'GATE OUT'),
        (10, 'GATE_IN_EF',     N'GATE IN E',  NULL, NULL,   1,    NULL, N'EMPTY', 'N', N'GATE IN'),
        (11, 'VRACANJE',       N'GATE IN E',  NULL, NULL,   NULL, NULL, N'EMPTY', 'N', N'GATE IN'),
        (12, 'GATE_IN_EF',     N'GATE IN E',  NULL, NULL,   0,    1,    N'EMPTY', 'L', N'GATE IN')
    ) p (Slucaj, Polje, Gate, Booking, Plomba, Kamion, BL, FullEmpty, BkngIz, Move)
        ON p.Polje = n.Polje
       AND p.Gate = @GATE_IN_GATE_OUT
       AND (p.Booking IS NULL OR p.Booking = @ImaBooking)
       AND (p.Plomba IS NULL OR p.Plomba = @ImaPlombu)
       AND (p.Kamion IS NULL OR p.Kamion = @Kamion)
       AND (p.BL IS NULL OR p.BL = @ImaBL)
    WHERE n.Stara IS NULL AND n.Vrednost IS NOT NULL
    ORDER BY n.Vrednost, n.Redosled;   -- hronoloski

    SET @BrojUpisanih = @@ROWCOUNT;
END
GO

-- Pokreti HAPAG kontejnera (upisuje insTerminalPrivHAPAGPokreti pri dugmetu 'Promeni', izvozi 'Izveštaj HAPAG').
-- ID je ID zapisa iz TerminalPriv; HAPAGID je ključ zapisa.
IF OBJECT_ID('dbo.TerminalPrivHAPAG', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.TerminalPrivHAPAG
    (
        HAPAGID             int IDENTITY(1,1) NOT NULL,
        ID                  int           NOT NULL,
        [Container]         nvarchar(30)  NULL,
        [Type]              nvarchar(30)  NULL,
        [Size]              nvarchar(30)  NULL,
        [Full/Empty]        nvarchar(30)  NULL,
        [Booking number]    nvarchar(30)  NULL,
        [1st MOVE]          nvarchar(30)  NULL,
        [DATE OF THE MOVE]  nvarchar(30)  NULL,
        [PLOMBA]            nvarchar(30)  NULL,
        [Active]            int           NOT NULL CONSTRAINT DF_TerminalPrivHAPAG_Active DEFAULT (0),
        [DatumIzvoza]       datetime      NULL,
        CONSTRAINT PK_TerminalPrivHAPAG PRIMARY KEY CLUSTERED (HAPAGID)
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_TerminalPrivHAPAG_Active' AND object_id = OBJECT_ID('dbo.TerminalPrivHAPAG'))
    CREATE NONCLUSTERED INDEX IX_TerminalPrivHAPAG_Active ON dbo.TerminalPrivHAPAG (Active);
GO

-- Upis pokreta HAPAG kontejnera u TerminalPrivHAPAG (pozivaju insTerminalPriv i updTerminalPriv), po istom principu
-- kao insTerminalPrivHMMPokreti: pokret se upisuje samo ako za datum koji je prešao iz NULL u popunjen postoji pravilo.
IF OBJECT_ID('dbo.insTerminalPrivHAPAGPokreti', 'P') IS NOT NULL DROP PROCEDURE dbo.insTerminalPrivHAPAGPokreti;
GO
CREATE PROCEDURE dbo.insTerminalPrivHAPAGPokreti
    @ID int,
    @KONTEJNER nvarchar(30),
    @BRODAR nvarchar(30) = NULL,
    @VRSTA nvarchar(30) = NULL,
    @VOZ_KAMION nvarchar(30) = NULL,
    @GATE_IN_GATE_OUT nvarchar(50) = NULL,
    @BOOKING_IZVOZ nvarchar(30) = NULL,
    @PLOMBA_IZVOZ nvarchar(30) = NULL,
    @BL_UVOZ nvarchar(30) = NULL,
    @PLOMBA_UVOZ nvarchar(30) = NULL,
    @GATE_IN_E_F datetime = NULL,
    @PREUZIMANJE_PUNOG_RAZVOZ datetime = NULL,
    @VRACANJE_PRAZNOG_IZ_RAZVOZA datetime = NULL,
    @KONACNI_GATE_OUT datetime = NULL,
    @GATE_OUT_EMPTY_UTOVAR datetime = NULL,
    @GATE_IN_FULL_SA_UTOVARA datetime = NULL,
    @StariGateInEF datetime = NULL,
    @StariPreuzimanje datetime = NULL,
    @StariVracanje datetime = NULL,
    @StariKonacni datetime = NULL,
    @StariGateOutEmpty datetime = NULL,
    @StariGateInFull datetime = NULL,
    @BrojUpisanih int = NULL OUTPUT   -- broj upisanih pokreta (za poruku u formi)
AS
BEGIN
    SET NOCOUNT ON;

    SET @BrojUpisanih = 0;

    -- brodar mora biti HAPAG
    IF UPPER(ISNULL(@BRODAR, N'')) NOT LIKE N'%HAPAG%'
        RETURN;

    DECLARE @Booking nvarchar(30) = NULLIF(LTRIM(RTRIM(@BOOKING_IZVOZ)), N'');
    DECLARE @PlombaIz nvarchar(30) = NULLIF(LTRIM(RTRIM(@PLOMBA_IZVOZ)), N'');
    DECLARE @BL nvarchar(30) = NULLIF(LTRIM(RTRIM(@BL_UVOZ)), N'');
    DECLARE @PlombaUv nvarchar(30) = NULLIF(LTRIM(RTRIM(@PLOMBA_UVOZ)), N'');
    DECLARE @ImaBooking bit = CASE WHEN @Booking IS NOT NULL THEN 1 ELSE 0 END;
    DECLARE @ImaPlombuIz bit = CASE WHEN @PlombaIz IS NOT NULL THEN 1 ELSE 0 END;
    DECLARE @ImaBL bit = CASE WHEN @BL IS NOT NULL THEN 1 ELSE 0 END;
    DECLARE @ImaPlombuUv bit = CASE WHEN @PlombaUv IS NOT NULL THEN 1 ELSE 0 END;
    -- VOZ/kamion: KAMION = tačno ta vrednost (bez obzira na velika/mala slova); sve ostalo, i prazno, nije KAMION
    DECLARE @Kamion bit = CASE WHEN UPPER(LTRIM(RTRIM(ISNULL(@VOZ_KAMION, N'')))) = N'KAMION' THEN 1 ELSE 0 END;

    -- VRSTA iz šifarnika (npr. 20' DB, 40' FLAT): Size = broj sa apostrofom (20'), Type = ostatak posle razmaka
    DECLARE @VrstaCnt nvarchar(30) = NULLIF(LTRIM(RTRIM(@VRSTA)), N'');
    DECLARE @Size nvarchar(30) = NULLIF(LTRIM(RTRIM(REPLACE(REPLACE(LEFT(@VrstaCnt, 3), N'''', N''), N'’', N''))), N'') + N'''';
    DECLARE @Type nvarchar(30) = NULLIF(LTRIM(SUBSTRING(@VrstaCnt, 4, 30)), N'');

    INSERT INTO dbo.TerminalPrivHAPAG
        (ID, [Container], [Type], [Size], [Full/Empty], [Booking number], [1st MOVE], [DATE OF THE MOVE], [PLOMBA])
    SELECT
        @ID,
        @KONTEJNER,
        @Type,
        @Size,
        p.FullEmpty,
        CASE p.Upis WHEN 'I' THEN @Booking WHEN 'U' THEN @BL WHEN 'L' THEN @BL END,
        p.Move,
        CONVERT(nvarchar(10), n.Vrednost, 104) + N'.',   -- 08.10.2026.
        CASE p.Upis WHEN 'I' THEN @PlombaIz WHEN 'U' THEN @PlombaUv END
    FROM (VALUES
        -- datum koji je prešao iz NULL u popunjen
        ('GATE_IN_EF', @GATE_IN_E_F, @StariGateInEF, 1),
        ('PREUZIMANJE', @PREUZIMANJE_PUNOG_RAZVOZ, @StariPreuzimanje, 2),
        ('VRACANJE', @VRACANJE_PRAZNOG_IZ_RAZVOZA, @StariVracanje, 3),
        ('KONACNI', @KONACNI_GATE_OUT, @StariKonacni, 4),
        ('GATE_OUT_EMPTY', @GATE_OUT_EMPTY_UTOVAR, @StariGateOutEmpty, 5),
        ('GATE_IN_FULL', @GATE_IN_FULL_SA_UTOVARA, @StariGateInFull, 6)
    ) n (Polje, Vrednost, Stara, Redosled)
    INNER JOIN (VALUES
        -- pravila: Booking/Plomba/BL = 1 ima, 0 nema, NULL svejedno; Kamion 1 = KAMION, 0 = nije KAMION; Otprema se ne gleda.
        -- Upis: I = Booking i Plomba izvoz, U = BL i Plomba uvoz, L = samo BL, N = ništa.
        -- Slučaj 2 iz specifikacije je isti kao 1, pa se ne ponavlja (slučaj 8 ne postoji u specifikaciji).
        --  #    Polje              GATE_IN/_GATE_OUT  Book  PlIz  Kam   BL    PlUv  Full/Empty  Upis  1st MOVE
        (1,  'KONACNI',        N'GATE OUT E', 1,    1,    NULL, NULL, NULL, N'EMPTY', 'I', N'GATE OUT'),
        (3,  'GATE_OUT_EMPTY', N'GATE OUT E', 1,    1,    NULL, NULL, NULL, N'EMPTY', 'I', N'GATE OUT'),
        (4,  'KONACNI',        N'GATE OUT F', 1,    1,    NULL, NULL, NULL, N'FULL',  'I', N'GATE OUT'),
        (5,  'GATE_IN_EF',     N'GATE IN F',  1,    1,    NULL, NULL, NULL, N'FULL',  'I', N'GATE IN'),
        (6,  'GATE_IN_FULL',   N'GATE IN F',  1,    1,    NULL, NULL, NULL, N'FULL',  'I', N'GATE IN'),
        (7,  'GATE_IN_EF',     N'GATE IN F',  0,    0,    NULL, 1,    1,    N'FULL',  'U', N'GATE IN'),
        (9,  'PREUZIMANJE',    N'GATE OUT F', 0,    0,    NULL, 1,    1,    N'FULL',  'U', N'GATE OUT'),
        (10, 'KONACNI',        N'GATE OUT F', 0,    0,    NULL, 1,    1,    N'FULL',  'U', N'GATE OUT'),
        (11, 'GATE_IN_EF',     N'GATE IN E',  NULL, NULL, 1,    NULL, NULL, N'EMPTY', 'N', N'GATE IN'),
        (12, 'VRACANJE',       N'GATE IN E',  NULL, NULL, NULL, NULL, NULL, N'EMPTY', 'N', N'GATE IN'),
        (13, 'GATE_IN_EF',     N'GATE IN E',  NULL, NULL, 0,    1,    NULL, N'EMPTY', 'L', N'GATE IN')
    ) p (Slucaj, Polje, Gate, Booking, PlombaIz, Kamion, BL, PlombaUv, FullEmpty, Upis, Move)
        ON p.Polje = n.Polje
       AND p.Gate = @GATE_IN_GATE_OUT
       AND (p.Booking IS NULL OR p.Booking = @ImaBooking)
       AND (p.PlombaIz IS NULL OR p.PlombaIz = @ImaPlombuIz)
       AND (p.Kamion IS NULL OR p.Kamion = @Kamion)
       AND (p.BL IS NULL OR p.BL = @ImaBL)
       AND (p.PlombaUv IS NULL OR p.PlombaUv = @ImaPlombuUv)
    WHERE n.Stara IS NULL AND n.Vrednost IS NOT NULL
    ORDER BY n.Vrednost, n.Redosled;   -- hronoloski

    SET @BrojUpisanih = @@ROWCOUNT;
END
GO

-- Pokreti FLEXBOX kontejnera (upisuje insTerminalPrivFLEXBOXPokreti pri dugmetu 'Promeni', izvozi 'Izveštaj FLEXBOX').
-- ID je ID zapisa iz TerminalPriv; FLEXBOXID je ključ zapisa.
IF OBJECT_ID('dbo.TerminalPrivFLEXBOX', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.TerminalPrivFLEXBOX
    (
        FLEXBOXID           int IDENTITY(1,1) NOT NULL,
        ID                  int           NOT NULL,
        [Container]         nvarchar(30)  NULL,
        [Type]              nvarchar(30)  NULL,
        [Size]              nvarchar(30)  NULL,
        [Full/Empty]        nvarchar(30)  NULL,
        [Booking number]    nvarchar(30)  NULL,
        [1st MOVE]          nvarchar(30)  NULL,
        [DATE OF THE MOVE]  nvarchar(30)  NULL,
        [Active]            int           NOT NULL CONSTRAINT DF_TerminalPrivFLEXBOX_Active DEFAULT (0),
        [DatumIzvoza]       datetime      NULL,
        CONSTRAINT PK_TerminalPrivFLEXBOX PRIMARY KEY CLUSTERED (FLEXBOXID)
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_TerminalPrivFLEXBOX_Active' AND object_id = OBJECT_ID('dbo.TerminalPrivFLEXBOX'))
    CREATE NONCLUSTERED INDEX IX_TerminalPrivFLEXBOX_Active ON dbo.TerminalPrivFLEXBOX (Active);
GO

-- Upis pokreta FLEXBOX kontejnera u TerminalPrivFLEXBOX (pozivaju insTerminalPriv i updTerminalPriv), po istom principu
-- kao insTerminalPrivHMMPokreti (ista pravila): pokret se upisuje samo ako za datum koji je prešao iz NULL u popunjen postoji pravilo.
IF OBJECT_ID('dbo.insTerminalPrivFLEXBOXPokreti', 'P') IS NOT NULL DROP PROCEDURE dbo.insTerminalPrivFLEXBOXPokreti;
GO
CREATE PROCEDURE dbo.insTerminalPrivFLEXBOXPokreti
    @ID int,
    @KONTEJNER nvarchar(30),
    @BRODAR nvarchar(30) = NULL,
    @VRSTA nvarchar(30) = NULL,
    @VOZ_KAMION nvarchar(30) = NULL,
    @GATE_IN_GATE_OUT nvarchar(50) = NULL,
    @BOOKING_IZVOZ nvarchar(30) = NULL,
    @PLOMBA_IZVOZ nvarchar(30) = NULL,
    @BL_UVOZ nvarchar(30) = NULL,
    @GATE_IN_E_F datetime = NULL,
    @PREUZIMANJE_PUNOG_RAZVOZ datetime = NULL,
    @VRACANJE_PRAZNOG_IZ_RAZVOZA datetime = NULL,
    @KONACNI_GATE_OUT datetime = NULL,
    @GATE_OUT_EMPTY_UTOVAR datetime = NULL,
    @GATE_IN_FULL_SA_UTOVARA datetime = NULL,
    @StariGateInEF datetime = NULL,
    @StariPreuzimanje datetime = NULL,
    @StariVracanje datetime = NULL,
    @StariKonacni datetime = NULL,
    @StariGateOutEmpty datetime = NULL,
    @StariGateInFull datetime = NULL,
    @BrojUpisanih int = NULL OUTPUT   -- broj upisanih pokreta (za poruku u formi)
AS
BEGIN
    SET NOCOUNT ON;

    SET @BrojUpisanih = 0;

    -- brodar mora biti FLEXBOX
    IF UPPER(ISNULL(@BRODAR, N'')) NOT LIKE N'%FLEXBOX%'
        RETURN;

    DECLARE @Booking nvarchar(30) = NULLIF(LTRIM(RTRIM(@BOOKING_IZVOZ)), N'');
    DECLARE @BL nvarchar(30) = NULLIF(LTRIM(RTRIM(@BL_UVOZ)), N'');
    DECLARE @ImaBooking bit = CASE WHEN @Booking IS NOT NULL THEN 1 ELSE 0 END;
    DECLARE @ImaPlombu bit = CASE WHEN LTRIM(RTRIM(ISNULL(@PLOMBA_IZVOZ, N''))) <> N'' THEN 1 ELSE 0 END;
    DECLARE @ImaBL bit = CASE WHEN @BL IS NOT NULL THEN 1 ELSE 0 END;
    -- VOZ/kamion: KAMION = tačno ta vrednost (bez obzira na velika/mala slova); sve ostalo, i prazno, nije KAMION
    DECLARE @Kamion bit = CASE WHEN UPPER(LTRIM(RTRIM(ISNULL(@VOZ_KAMION, N'')))) = N'KAMION' THEN 1 ELSE 0 END;

    -- VRSTA iz šifarnika (npr. 20' DB, 40' FLAT): Size = broj sa apostrofom (20'), Type = ostatak posle razmaka
    DECLARE @VrstaCnt nvarchar(30) = NULLIF(LTRIM(RTRIM(@VRSTA)), N'');
    DECLARE @Size nvarchar(30) = NULLIF(LTRIM(RTRIM(REPLACE(REPLACE(LEFT(@VrstaCnt, 3), N'''', N''), N'’', N''))), N'') + N'''';
    DECLARE @Type nvarchar(30) = NULLIF(LTRIM(SUBSTRING(@VrstaCnt, 4, 30)), N'');

    INSERT INTO dbo.TerminalPrivFLEXBOX
        (ID, [Container], [Type], [Size], [Full/Empty], [Booking number], [1st MOVE], [DATE OF THE MOVE])
    SELECT
        @ID,
        @KONTEJNER,
        @Type,
        @Size,
        p.FullEmpty,
        CASE p.BkngIz WHEN 'B' THEN @Booking WHEN 'L' THEN @BL END,
        p.Move,
        CONVERT(nvarchar(10), n.Vrednost, 104) + N'.'   -- 08.10.2026.
    FROM (VALUES
        -- datum koji je prešao iz NULL u popunjen
        ('GATE_IN_EF', @GATE_IN_E_F, @StariGateInEF, 1),
        ('PREUZIMANJE', @PREUZIMANJE_PUNOG_RAZVOZ, @StariPreuzimanje, 2),
        ('VRACANJE', @VRACANJE_PRAZNOG_IZ_RAZVOZA, @StariVracanje, 3),
        ('KONACNI', @KONACNI_GATE_OUT, @StariKonacni, 4),
        ('GATE_OUT_EMPTY', @GATE_OUT_EMPTY_UTOVAR, @StariGateOutEmpty, 5),
        ('GATE_IN_FULL', @GATE_IN_FULL_SA_UTOVARA, @StariGateInFull, 6)
    ) n (Polje, Vrednost, Stara, Redosled)
    INNER JOIN (VALUES
        -- pravila: Booking/Plomba/BL/Kamion = 1 ima (KAMION), 0 nema (nije KAMION), NULL svejedno; Otprema se ne gleda;
        -- BkngIz B = Booking/Izvoz, L = BL/Uvoz, N = ništa. Slučaj 2 iz specifikacije je isti kao 1, pa se ne ponavlja.
        --  #    Polje              GATE_IN/_GATE_OUT  Book  Plomba  Kam   BL    Full/Empty  BkngIz  1st MOVE
        (1,  'KONACNI',        N'GATE OUT E', 1,    NULL,   NULL, NULL, N'EMPTY', 'B', N'GATE OUT'),
        (3,  'GATE_OUT_EMPTY', N'GATE OUT E', 1,    NULL,   NULL, NULL, N'EMPTY', 'B', N'GATE OUT'),
        (4,  'KONACNI',        N'GATE OUT F', 1,    NULL,   NULL, NULL, N'FULL',  'B', N'GATE OUT'),
        (5,  'GATE_IN_EF',     N'GATE IN F',  1,    NULL,   NULL, NULL, N'FULL',  'B', N'GATE IN'),
        (6,  'GATE_IN_FULL',   N'GATE IN F',  1,    NULL,   NULL, NULL, N'FULL',  'B', N'GATE IN'),
        (7,  'GATE_IN_EF',     N'GATE IN F',  0,    0,      NULL, 1,    N'FULL',  'L', N'GATE IN'),
        (8,  'PREUZIMANJE',    N'GATE OUT F', 0,    0,      NULL, 1,    N'FULL',  'L', N'GATE OUT'),
        (9,  'KONACNI',        N'GATE OUT F', 0,    0,      NULL, 1,    N'FULL',  'L', N'GATE OUT'),
        (10, 'GATE_IN_EF',     N'GATE IN E',  NULL, NULL,   1,    NULL, N'EMPTY', 'N', N'GATE IN'),
        (11, 'VRACANJE',       N'GATE IN E',  NULL, NULL,   NULL, NULL, N'EMPTY', 'N', N'GATE IN'),
        (12, 'GATE_IN_EF',     N'GATE IN E',  NULL, NULL,   0,    1,    N'EMPTY', 'L', N'GATE IN')
    ) p (Slucaj, Polje, Gate, Booking, Plomba, Kamion, BL, FullEmpty, BkngIz, Move)
        ON p.Polje = n.Polje
       AND p.Gate = @GATE_IN_GATE_OUT
       AND (p.Booking IS NULL OR p.Booking = @ImaBooking)
       AND (p.Plomba IS NULL OR p.Plomba = @ImaPlombu)
       AND (p.Kamion IS NULL OR p.Kamion = @Kamion)
       AND (p.BL IS NULL OR p.BL = @ImaBL)
    WHERE n.Stara IS NULL AND n.Vrednost IS NOT NULL
    ORDER BY n.Vrednost, n.Redosled;   -- hronoloski

    SET @BrojUpisanih = @@ROWCOUNT;
END
GO

-- Pokreti COSCO kontejnera (upisuje insTerminalPrivCOSCOPokreti pri dugmetu 'Promeni', izvozi 'Izveštaj COSCO').
-- ID je ID zapisa iz TerminalPriv; COSCOID je ključ zapisa.
IF OBJECT_ID('dbo.TerminalPrivCOSCO', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.TerminalPrivCOSCO
    (
        COSCOID             int IDENTITY(1,1) NOT NULL,
        ID                  int           NOT NULL,
        [Port]              nvarchar(30)  NULL,
        [Container]         nvarchar(30)  NULL,
        [Type]              nvarchar(30)  NULL,
        [Size]              nvarchar(30)  NULL,
        [Full/Empty]        nvarchar(30)  NULL,
        [Document number]   nvarchar(30)  NULL,
        [1st MOVE]          nvarchar(30)  NULL,
        [DATE OF THE MOVE]  nvarchar(30)  NULL,
        [Active]            int           NOT NULL CONSTRAINT DF_TerminalPrivCOSCO_Active DEFAULT (0),
        [DatumIzvoza]       datetime      NULL,
        CONSTRAINT PK_TerminalPrivCOSCO PRIMARY KEY CLUSTERED (COSCOID)
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_TerminalPrivCOSCO_Active' AND object_id = OBJECT_ID('dbo.TerminalPrivCOSCO'))
    CREATE NONCLUSTERED INDEX IX_TerminalPrivCOSCO_Active ON dbo.TerminalPrivCOSCO (Active);
GO

-- Upis pokreta COSCO kontejnera u TerminalPrivCOSCO (pozivaju insTerminalPriv i updTerminalPriv), po istom principu
-- kao insTerminalPrivHMMPokreti: pokret se upisuje samo ako za datum koji je prešao iz NULL u popunjen postoji pravilo.
IF OBJECT_ID('dbo.insTerminalPrivCOSCOPokreti', 'P') IS NOT NULL DROP PROCEDURE dbo.insTerminalPrivCOSCOPokreti;
GO
CREATE PROCEDURE dbo.insTerminalPrivCOSCOPokreti
    @ID int,
    @KONTEJNER nvarchar(30),
    @BRODAR nvarchar(30) = NULL,
    @VRSTA nvarchar(30) = NULL,
    @VOZ_KAMION nvarchar(30) = NULL,
    @GATE_IN_GATE_OUT nvarchar(50) = NULL,
    @OTPREMA nvarchar(30) = NULL,
    @BOOKING_IZVOZ nvarchar(30) = NULL,
    @BL_UVOZ nvarchar(30) = NULL,
    @GATE_IN_E_F datetime = NULL,
    @PREUZIMANJE_PUNOG_RAZVOZ datetime = NULL,
    @VRACANJE_PRAZNOG_IZ_RAZVOZA datetime = NULL,
    @KONACNI_GATE_OUT datetime = NULL,
    @GATE_OUT_EMPTY_UTOVAR datetime = NULL,
    @GATE_IN_FULL_SA_UTOVARA datetime = NULL,
    @StariGateInEF datetime = NULL,
    @StariPreuzimanje datetime = NULL,
    @StariVracanje datetime = NULL,
    @StariKonacni datetime = NULL,
    @StariGateOutEmpty datetime = NULL,
    @StariGateInFull datetime = NULL,
    @BrojUpisanih int = NULL OUTPUT   -- broj upisanih pokreta (za poruku u formi)
AS
BEGIN
    SET NOCOUNT ON;

    SET @BrojUpisanih = 0;

    -- brodar mora biti COSCO
    IF UPPER(ISNULL(@BRODAR, N'')) NOT LIKE N'%COSCO%'
        RETURN;

    DECLARE @Booking nvarchar(30) = NULLIF(LTRIM(RTRIM(@BOOKING_IZVOZ)), N'');
    DECLARE @BL nvarchar(30) = NULLIF(LTRIM(RTRIM(@BL_UVOZ)), N'');
    DECLARE @ImaOtpremu bit = CASE WHEN LTRIM(RTRIM(ISNULL(@OTPREMA, N''))) <> N'' THEN 1 ELSE 0 END;
    DECLARE @ImaBooking bit = CASE WHEN @Booking IS NOT NULL THEN 1 ELSE 0 END;
    DECLARE @ImaBL bit = CASE WHEN @BL IS NOT NULL THEN 1 ELSE 0 END;
    -- VOZ/kamion: KAMION = tačno ta vrednost (bez obzira na velika/mala slova); sve ostalo, i prazno, nije KAMION
    DECLARE @Kamion bit = CASE WHEN UPPER(LTRIM(RTRIM(ISNULL(@VOZ_KAMION, N'')))) = N'KAMION' THEN 1 ELSE 0 END;

    -- VRSTA iz šifarnika (npr. 20' DB, 40' FLAT): Size = broj sa apostrofom (20'), Type = ostatak posle razmaka
    DECLARE @VrstaCnt nvarchar(30) = NULLIF(LTRIM(RTRIM(@VRSTA)), N'');
    DECLARE @Size nvarchar(30) = NULLIF(LTRIM(RTRIM(REPLACE(REPLACE(LEFT(@VrstaCnt, 3), N'''', N''), N'’', N''))), N'') + N'''';
    DECLARE @Type nvarchar(30) = NULLIF(LTRIM(SUBSTRING(@VrstaCnt, 4, 30)), N'');

    INSERT INTO dbo.TerminalPrivCOSCO
        (ID, [Port], [Container], [Type], [Size], [Full/Empty], [Document number], [1st MOVE], [DATE OF THE MOVE])
    SELECT
        @ID,
        N'SSV11',
        @KONTEJNER,
        @Type,
        @Size,
        p.FullEmpty,
        CASE p.BkngIz WHEN 'B' THEN @Booking WHEN 'L' THEN @BL END,
        p.Move,
        CONVERT(nvarchar(10), n.Vrednost, 104) + N'.'   -- 08.10.2026.
    FROM (VALUES
        -- datum koji je prešao iz NULL u popunjen
        ('GATE_IN_EF', @GATE_IN_E_F, @StariGateInEF, 1),
        ('PREUZIMANJE', @PREUZIMANJE_PUNOG_RAZVOZ, @StariPreuzimanje, 2),
        ('VRACANJE', @VRACANJE_PRAZNOG_IZ_RAZVOZA, @StariVracanje, 3),
        ('KONACNI', @KONACNI_GATE_OUT, @StariKonacni, 4),
        ('GATE_OUT_EMPTY', @GATE_OUT_EMPTY_UTOVAR, @StariGateOutEmpty, 5),
        ('GATE_IN_FULL', @GATE_IN_FULL_SA_UTOVARA, @StariGateInFull, 6)
    ) n (Polje, Vrednost, Stara, Redosled)
    INNER JOIN (VALUES
        -- pravila: Otprema/Booking/BL = 1 ima, 0 nema, NULL svejedno; Kamion 1 = KAMION, 0 = nije KAMION; Plomba se ne gleda.
        -- BkngIz B = Booking/Izvoz, L = BL/Uvoz, N = ništa. Slučaj 2 iz specifikacije je isti kao 1, pa se ne ponavlja;
        -- slučaj 10 važi kad BL nije upisan, a 12 kad jeste (inače bi isti datum upisao oba pokreta).
        --  #    Polje              GATE_IN/_GATE_OUT  Otpr  Book  Kam   BL    Full/Empty  BkngIz  1st MOVE
        (1,  'KONACNI',        N'GATE OUT E', NULL, 1,    NULL, NULL, N'EMPTY', 'B', N'GATE OUT'),
        (3,  'GATE_OUT_EMPTY', N'GATE OUT E', NULL, 1,    NULL, NULL, N'EMPTY', 'B', N'GATE OUT'),
        (4,  'KONACNI',        N'GATE OUT F', 1,    1,    NULL, NULL, N'FULL',  'B', N'LOADED ON TRAIN FROM DEPOT'),
        (5,  'GATE_IN_EF',     N'GATE IN F',  NULL, 1,    NULL, NULL, N'FULL',  'B', N'GATE IN'),
        (6,  'GATE_IN_FULL',   N'GATE IN F',  NULL, 1,    NULL, NULL, N'FULL',  'B', N'GATE IN'),
        (7,  'GATE_IN_EF',     N'GATE IN F',  NULL, 0,    0,    1,    N'FULL',  'L', N'LOADED ON DEPOT FROM TRAIN'),
        (8,  'PREUZIMANJE',    N'GATE OUT F', 0,    0,    NULL, 1,    N'FULL',  'L', N'GATE OUT'),
        (9,  'KONACNI',        N'GATE OUT F', 0,    0,    NULL, 1,    N'FULL',  'L', N'GATE OUT'),
        (10, 'GATE_IN_EF',     N'GATE IN E',  NULL, NULL, NULL, 0,    N'EMPTY', 'N', N'GATE IN'),
        (11, 'VRACANJE',       N'GATE IN E',  NULL, NULL, NULL, NULL, N'EMPTY', 'N', N'GATE IN'),
        (12, 'GATE_IN_EF',     N'GATE IN E',  NULL, NULL, NULL, 1,    N'EMPTY', 'L', N'GATE IN'),
        (13, 'KONACNI',        N'GATE OUT F', 0,    1,    NULL, NULL, N'FULL',  'B', N'GATE OUT'),
        (14, 'GATE_IN_EF',     N'GATE IN F',  NULL, 0,    1,    1,    N'FULL',  'L', N'GATE IN')
    ) p (Slucaj, Polje, Gate, Otprema, Booking, Kamion, BL, FullEmpty, BkngIz, Move)
        ON p.Polje = n.Polje
       AND p.Gate = @GATE_IN_GATE_OUT
       AND (p.Otprema IS NULL OR p.Otprema = @ImaOtpremu)
       AND (p.Booking IS NULL OR p.Booking = @ImaBooking)
       AND (p.Kamion IS NULL OR p.Kamion = @Kamion)
       AND (p.BL IS NULL OR p.BL = @ImaBL)
    WHERE n.Stara IS NULL AND n.Vrednost IS NOT NULL
    ORDER BY n.Vrednost, n.Redosled;   -- hronoloski

    SET @BrojUpisanih = @@ROWCOUNT;
END
GO

-- kolona BELEŠKA (oktobar 2026) za tabelu kreiranu pre njenog dodavanja
IF COL_LENGTH(N'dbo.TerminalPriv', N'BELEŠKA') IS NULL
    ALTER TABLE dbo.TerminalPriv ADD [BELEŠKA] nvarchar(300) NULL;
GO

IF OBJECT_ID('dbo.insTerminalPriv', 'P') IS NOT NULL DROP PROCEDURE dbo.insTerminalPriv;
GO

CREATE PROCEDURE dbo.insTerminalPriv
    @KONTEJNER nvarchar(30),
    @STATUS nvarchar(30) = NULL,
    @POZICIJA nvarchar(30) = NULL,
    @VRSTA nvarchar(30) = NULL,
    @BRODAR nvarchar(30) = NULL,
    @NALOGODAVAC_UVOZ nvarchar(50) = NULL,
    @POSTUPAK_UVOZ nvarchar(50) = NULL,
    @UVOZNIK nvarchar(50) = NULL,
    @BL_UVOZ nvarchar(30) = NULL,
    @PLOMBA_UVOZ nvarchar(30) = NULL,
    @VOZ_KAMION nvarchar(30) = NULL,
    @STANJE nvarchar(30) = NULL,
    @GATE_IN_E_F datetime = NULL,
    @PREUZIMANJE_PUNOG_RAZVOZ datetime = NULL,
    @VRACANJE_PRAZNOG_IZ_RAZVOZA datetime = NULL,
    @KONACNI_GATE_OUT datetime = NULL,
    @BOOKING_IZVOZ nvarchar(30) = NULL,
    @KLIJENT_IZVOZ nvarchar(30) = NULL,
    @GATE_OUT_EMPTY_UTOVAR datetime = NULL,
    @GATE_IN_FULL_SA_UTOVARA datetime = NULL,
    @GATE_IN_GATE_OUT nvarchar(50) = NULL,
    @L_R nvarchar(30) = NULL,
    @TARA nvarchar(30) = NULL,
    @MAX_NOSIVOST_CNT nvarchar(30) = NULL,
    @VOZILO_PREUZIMANJE nvarchar(30) = NULL,
    @PLOMBA_IZVOZ nvarchar(30) = NULL,
    @NAPOMENA nvarchar(300) = NULL,
    @OPIS nvarchar(300) = NULL,
    @OTPREMA nvarchar(30) = NULL,
    @POSLATE_SLIKE nvarchar(30) = NULL,
    @PREVOZNIK nvarchar(30) = NULL,
    @BELESKA nvarchar(300) = NULL,
    @ZapisiMaersk bit = 0,  -- 1 = novi zapis dugmetom 'Promeni' posle 'Novi': upis pokreta u TerminalPrivMaersk i TerminalPrivMSC
    @BrojMaersk int = 0 OUTPUT,  -- broj upisanih pokreta u TerminalPrivMaersk
    @BrojMSC int = 0 OUTPUT,     -- broj upisanih pokreta u TerminalPrivMSC
    @BrojCMA int = 0 OUTPUT,     -- broj upisanih pokreta u TerminalPrivCMA
    @BrojOOCL int = 0 OUTPUT,    -- broj upisanih pokreta u TerminalPrivOOCL
    @BrojHMM int = 0 OUTPUT,     -- broj upisanih pokreta u TerminalPrivHMM
    @BrojHAPAG int = 0 OUTPUT,   -- broj upisanih pokreta u TerminalPrivHAPAG
    @BrojFLEXBOX int = 0 OUTPUT, -- broj upisanih pokreta u TerminalPrivFLEXBOX
    @BrojCOSCO int = 0 OUTPUT    -- broj upisanih pokreta u TerminalPrivCOSCO
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NoviID int;

    INSERT INTO dbo.TerminalPriv
        ([KONTEJNER], [STATUS], [POZICIJA], [VRSTA], [BRODAR], [NALOGODAVAC/UVOZ], [POSTUPAK/UVOZ], [UVOZNIK], [BL/UVOZ], [PLOMBA_UVOZ], [VOZ/kamion], [STANJE], [GATE_IN_E/F], [PREUZIMANJE_PUNOG/Razvoz], [VRAĆANJE_PRAZNOG/iz_Razvoza], [Konačni_GATE_OUT], [BOOKING/IZVOZ], [KLIJENT/IZVOZ], [GATE_OUT_EMPTY/Utovar], [GATE_IN_FULL/sa_Utovara], [GATE_IN/_GATE_OUT], [L/R], [TARA], [MAX_NOSIVOST_CNT], [VOZILO/PREUZIMANJE], [PLOMBA/IZVOZ], [NAPOMENA], [OPIS], [OTPREMA], [POSLATE SLIKE], [Prevoznik], [BELEŠKA])
    VALUES
        (@KONTEJNER, @STATUS, @POZICIJA, @VRSTA, @BRODAR, @NALOGODAVAC_UVOZ, @POSTUPAK_UVOZ, @UVOZNIK, @BL_UVOZ, @PLOMBA_UVOZ, @VOZ_KAMION, @STANJE, @GATE_IN_E_F, @PREUZIMANJE_PUNOG_RAZVOZ, @VRACANJE_PRAZNOG_IZ_RAZVOZA, @KONACNI_GATE_OUT, @BOOKING_IZVOZ, @KLIJENT_IZVOZ, @GATE_OUT_EMPTY_UTOVAR, @GATE_IN_FULL_SA_UTOVARA, @GATE_IN_GATE_OUT, @L_R, @TARA, @MAX_NOSIVOST_CNT, @VOZILO_PREUZIMANJE, @PLOMBA_IZVOZ, @NAPOMENA, @OPIS, @OTPREMA, @POSLATE_SLIKE, @PREVOZNIK, @BELESKA);

    SET @NoviID = CAST(SCOPE_IDENTITY() AS int);

    -- novi zapis: svi datumi su pre upisa bili prazni
    IF @ZapisiMaersk = 1
        EXEC dbo.insTerminalPrivMaerskPokreti
            @ID = @NoviID, @KONTEJNER = @KONTEJNER, @BRODAR = @BRODAR, @GATE_IN_GATE_OUT = @GATE_IN_GATE_OUT,
            @OTPREMA = @OTPREMA, @BOOKING_IZVOZ = @BOOKING_IZVOZ, @BL_UVOZ = @BL_UVOZ,
            @GATE_IN_E_F = @GATE_IN_E_F, @PREUZIMANJE_PUNOG_RAZVOZ = @PREUZIMANJE_PUNOG_RAZVOZ,
            @VRACANJE_PRAZNOG_IZ_RAZVOZA = @VRACANJE_PRAZNOG_IZ_RAZVOZA, @KONACNI_GATE_OUT = @KONACNI_GATE_OUT,
            @GATE_OUT_EMPTY_UTOVAR = @GATE_OUT_EMPTY_UTOVAR, @GATE_IN_FULL_SA_UTOVARA = @GATE_IN_FULL_SA_UTOVARA,
            @BrojUpisanih = @BrojMaersk OUTPUT;

    IF @ZapisiMaersk = 1
        EXEC dbo.insTerminalPrivMSCPokreti
            @ID = @NoviID, @KONTEJNER = @KONTEJNER, @BRODAR = @BRODAR, @GATE_IN_GATE_OUT = @GATE_IN_GATE_OUT,
            @OTPREMA = @OTPREMA, @BOOKING_IZVOZ = @BOOKING_IZVOZ, @BL_UVOZ = @BL_UVOZ,
            @GATE_IN_E_F = @GATE_IN_E_F, @PREUZIMANJE_PUNOG_RAZVOZ = @PREUZIMANJE_PUNOG_RAZVOZ,
            @VRACANJE_PRAZNOG_IZ_RAZVOZA = @VRACANJE_PRAZNOG_IZ_RAZVOZA, @KONACNI_GATE_OUT = @KONACNI_GATE_OUT,
            @GATE_OUT_EMPTY_UTOVAR = @GATE_OUT_EMPTY_UTOVAR, @GATE_IN_FULL_SA_UTOVARA = @GATE_IN_FULL_SA_UTOVARA,
            @BrojUpisanih = @BrojMSC OUTPUT;

    IF @ZapisiMaersk = 1
        EXEC dbo.insTerminalPrivCMAPokreti
            @ID = @NoviID, @KONTEJNER = @KONTEJNER, @BRODAR = @BRODAR, @VRSTA = @VRSTA, @VOZ_KAMION = @VOZ_KAMION,
            @GATE_IN_GATE_OUT = @GATE_IN_GATE_OUT, @OTPREMA = @OTPREMA, @BOOKING_IZVOZ = @BOOKING_IZVOZ, @BL_UVOZ = @BL_UVOZ,
            @GATE_IN_E_F = @GATE_IN_E_F, @PREUZIMANJE_PUNOG_RAZVOZ = @PREUZIMANJE_PUNOG_RAZVOZ,
            @VRACANJE_PRAZNOG_IZ_RAZVOZA = @VRACANJE_PRAZNOG_IZ_RAZVOZA, @KONACNI_GATE_OUT = @KONACNI_GATE_OUT,
            @GATE_OUT_EMPTY_UTOVAR = @GATE_OUT_EMPTY_UTOVAR, @GATE_IN_FULL_SA_UTOVARA = @GATE_IN_FULL_SA_UTOVARA,
            @BrojUpisanih = @BrojCMA OUTPUT;

    IF @ZapisiMaersk = 1
        EXEC dbo.insTerminalPrivOOCLPokreti
            @ID = @NoviID, @KONTEJNER = @KONTEJNER, @BRODAR = @BRODAR, @VRSTA = @VRSTA, @VOZ_KAMION = @VOZ_KAMION,
            @STANJE = @STANJE, @GATE_IN_GATE_OUT = @GATE_IN_GATE_OUT, @OTPREMA = @OTPREMA,
            @BOOKING_IZVOZ = @BOOKING_IZVOZ, @BL_UVOZ = @BL_UVOZ,
            @GATE_IN_E_F = @GATE_IN_E_F, @PREUZIMANJE_PUNOG_RAZVOZ = @PREUZIMANJE_PUNOG_RAZVOZ,
            @VRACANJE_PRAZNOG_IZ_RAZVOZA = @VRACANJE_PRAZNOG_IZ_RAZVOZA, @KONACNI_GATE_OUT = @KONACNI_GATE_OUT,
            @GATE_OUT_EMPTY_UTOVAR = @GATE_OUT_EMPTY_UTOVAR, @GATE_IN_FULL_SA_UTOVARA = @GATE_IN_FULL_SA_UTOVARA,
            @BrojUpisanih = @BrojOOCL OUTPUT;

    IF @ZapisiMaersk = 1
        EXEC dbo.insTerminalPrivHMMPokreti
            @ID = @NoviID, @KONTEJNER = @KONTEJNER, @BRODAR = @BRODAR, @VRSTA = @VRSTA, @VOZ_KAMION = @VOZ_KAMION,
            @GATE_IN_GATE_OUT = @GATE_IN_GATE_OUT, @BOOKING_IZVOZ = @BOOKING_IZVOZ, @PLOMBA_IZVOZ = @PLOMBA_IZVOZ,
            @BL_UVOZ = @BL_UVOZ,
            @GATE_IN_E_F = @GATE_IN_E_F, @PREUZIMANJE_PUNOG_RAZVOZ = @PREUZIMANJE_PUNOG_RAZVOZ,
            @VRACANJE_PRAZNOG_IZ_RAZVOZA = @VRACANJE_PRAZNOG_IZ_RAZVOZA, @KONACNI_GATE_OUT = @KONACNI_GATE_OUT,
            @GATE_OUT_EMPTY_UTOVAR = @GATE_OUT_EMPTY_UTOVAR, @GATE_IN_FULL_SA_UTOVARA = @GATE_IN_FULL_SA_UTOVARA,
            @BrojUpisanih = @BrojHMM OUTPUT;

    IF @ZapisiMaersk = 1
        EXEC dbo.insTerminalPrivHAPAGPokreti
            @ID = @NoviID, @KONTEJNER = @KONTEJNER, @BRODAR = @BRODAR, @VRSTA = @VRSTA, @VOZ_KAMION = @VOZ_KAMION,
            @GATE_IN_GATE_OUT = @GATE_IN_GATE_OUT, @BOOKING_IZVOZ = @BOOKING_IZVOZ, @PLOMBA_IZVOZ = @PLOMBA_IZVOZ,
            @BL_UVOZ = @BL_UVOZ, @PLOMBA_UVOZ = @PLOMBA_UVOZ,
            @GATE_IN_E_F = @GATE_IN_E_F, @PREUZIMANJE_PUNOG_RAZVOZ = @PREUZIMANJE_PUNOG_RAZVOZ,
            @VRACANJE_PRAZNOG_IZ_RAZVOZA = @VRACANJE_PRAZNOG_IZ_RAZVOZA, @KONACNI_GATE_OUT = @KONACNI_GATE_OUT,
            @GATE_OUT_EMPTY_UTOVAR = @GATE_OUT_EMPTY_UTOVAR, @GATE_IN_FULL_SA_UTOVARA = @GATE_IN_FULL_SA_UTOVARA,
            @BrojUpisanih = @BrojHAPAG OUTPUT;

    IF @ZapisiMaersk = 1
        EXEC dbo.insTerminalPrivFLEXBOXPokreti
            @ID = @NoviID, @KONTEJNER = @KONTEJNER, @BRODAR = @BRODAR, @VRSTA = @VRSTA, @VOZ_KAMION = @VOZ_KAMION,
            @GATE_IN_GATE_OUT = @GATE_IN_GATE_OUT, @BOOKING_IZVOZ = @BOOKING_IZVOZ, @PLOMBA_IZVOZ = @PLOMBA_IZVOZ,
            @BL_UVOZ = @BL_UVOZ,
            @GATE_IN_E_F = @GATE_IN_E_F, @PREUZIMANJE_PUNOG_RAZVOZ = @PREUZIMANJE_PUNOG_RAZVOZ,
            @VRACANJE_PRAZNOG_IZ_RAZVOZA = @VRACANJE_PRAZNOG_IZ_RAZVOZA, @KONACNI_GATE_OUT = @KONACNI_GATE_OUT,
            @GATE_OUT_EMPTY_UTOVAR = @GATE_OUT_EMPTY_UTOVAR, @GATE_IN_FULL_SA_UTOVARA = @GATE_IN_FULL_SA_UTOVARA,
            @BrojUpisanih = @BrojFLEXBOX OUTPUT;

    IF @ZapisiMaersk = 1
        EXEC dbo.insTerminalPrivCOSCOPokreti
            @ID = @NoviID, @KONTEJNER = @KONTEJNER, @BRODAR = @BRODAR, @VRSTA = @VRSTA, @VOZ_KAMION = @VOZ_KAMION,
            @GATE_IN_GATE_OUT = @GATE_IN_GATE_OUT, @OTPREMA = @OTPREMA, @BOOKING_IZVOZ = @BOOKING_IZVOZ, @BL_UVOZ = @BL_UVOZ,
            @GATE_IN_E_F = @GATE_IN_E_F, @PREUZIMANJE_PUNOG_RAZVOZ = @PREUZIMANJE_PUNOG_RAZVOZ,
            @VRACANJE_PRAZNOG_IZ_RAZVOZA = @VRACANJE_PRAZNOG_IZ_RAZVOZA, @KONACNI_GATE_OUT = @KONACNI_GATE_OUT,
            @GATE_OUT_EMPTY_UTOVAR = @GATE_OUT_EMPTY_UTOVAR, @GATE_IN_FULL_SA_UTOVARA = @GATE_IN_FULL_SA_UTOVARA,
            @BrojUpisanih = @BrojCOSCO OUTPUT;

    SELECT @NoviID AS ID;
END
GO

IF OBJECT_ID('dbo.updTerminalPriv', 'P') IS NOT NULL DROP PROCEDURE dbo.updTerminalPriv;
GO

CREATE PROCEDURE dbo.updTerminalPriv
    @ID int,
    @KONTEJNER nvarchar(30),
    @STATUS nvarchar(30) = NULL,
    @POZICIJA nvarchar(30) = NULL,
    @VRSTA nvarchar(30) = NULL,
    @BRODAR nvarchar(30) = NULL,
    @NALOGODAVAC_UVOZ nvarchar(50) = NULL,
    @POSTUPAK_UVOZ nvarchar(50) = NULL,
    @UVOZNIK nvarchar(50) = NULL,
    @BL_UVOZ nvarchar(30) = NULL,
    @PLOMBA_UVOZ nvarchar(30) = NULL,
    @VOZ_KAMION nvarchar(30) = NULL,
    @STANJE nvarchar(30) = NULL,
    @GATE_IN_E_F datetime = NULL,
    @PREUZIMANJE_PUNOG_RAZVOZ datetime = NULL,
    @VRACANJE_PRAZNOG_IZ_RAZVOZA datetime = NULL,
    @KONACNI_GATE_OUT datetime = NULL,
    @BOOKING_IZVOZ nvarchar(30) = NULL,
    @KLIJENT_IZVOZ nvarchar(30) = NULL,
    @GATE_OUT_EMPTY_UTOVAR datetime = NULL,
    @GATE_IN_FULL_SA_UTOVARA datetime = NULL,
    @GATE_IN_GATE_OUT nvarchar(50) = NULL,
    @L_R nvarchar(30) = NULL,
    @TARA nvarchar(30) = NULL,
    @MAX_NOSIVOST_CNT nvarchar(30) = NULL,
    @VOZILO_PREUZIMANJE nvarchar(30) = NULL,
    @PLOMBA_IZVOZ nvarchar(30) = NULL,
    @NAPOMENA nvarchar(300) = NULL,
    @OPIS nvarchar(300) = NULL,
    @OTPREMA nvarchar(30) = NULL,
    @POSLATE_SLIKE nvarchar(30) = NULL,
    @PREVOZNIK nvarchar(30) = NULL,
    @BELESKA nvarchar(300) = NULL,
    @ZapisiMaersk bit = 0,  -- 1 = izmena dugmetom 'Promeni': upis pokreta u TerminalPrivMaersk i TerminalPrivMSC
    @BrojMaersk int = 0 OUTPUT,  -- broj upisanih pokreta u TerminalPrivMaersk
    @BrojMSC int = 0 OUTPUT,     -- broj upisanih pokreta u TerminalPrivMSC
    @BrojCMA int = 0 OUTPUT,     -- broj upisanih pokreta u TerminalPrivCMA
    @BrojOOCL int = 0 OUTPUT,    -- broj upisanih pokreta u TerminalPrivOOCL
    @BrojHMM int = 0 OUTPUT,     -- broj upisanih pokreta u TerminalPrivHMM
    @BrojHAPAG int = 0 OUTPUT,   -- broj upisanih pokreta u TerminalPrivHAPAG
    @BrojFLEXBOX int = 0 OUTPUT, -- broj upisanih pokreta u TerminalPrivFLEXBOX
    @BrojCOSCO int = 0 OUTPUT    -- broj upisanih pokreta u TerminalPrivCOSCO
AS
BEGIN
    SET NOCOUNT ON;

    -- datumi pre izmene (za proveru koji je datum prešao iz praznog u popunjen)
    DECLARE @StariGateInEF datetime, @StariPreuzimanje datetime, @StariVracanje datetime,
            @StariKonacni datetime, @StariGateOutEmpty datetime, @StariGateInFull datetime;
    IF @ZapisiMaersk = 1
        SELECT @StariGateInEF = [GATE_IN_E/F],
               @StariPreuzimanje = [PREUZIMANJE_PUNOG/Razvoz],
               @StariVracanje = [VRAĆANJE_PRAZNOG/iz_Razvoza],
               @StariKonacni = [Konačni_GATE_OUT],
               @StariGateOutEmpty = [GATE_OUT_EMPTY/Utovar],
               @StariGateInFull = [GATE_IN_FULL/sa_Utovara]
        FROM dbo.TerminalPriv
        WHERE ID = @ID;

    UPDATE dbo.TerminalPriv
    SET [KONTEJNER] = @KONTEJNER,
        [STATUS] = @STATUS,
        [POZICIJA] = @POZICIJA,
        [VRSTA] = @VRSTA,
        [BRODAR] = @BRODAR,
        [NALOGODAVAC/UVOZ] = @NALOGODAVAC_UVOZ,
        [POSTUPAK/UVOZ] = @POSTUPAK_UVOZ,
        [UVOZNIK] = @UVOZNIK,
        [BL/UVOZ] = @BL_UVOZ,
        [PLOMBA_UVOZ] = @PLOMBA_UVOZ,
        [VOZ/kamion] = @VOZ_KAMION,
        [STANJE] = @STANJE,
        [GATE_IN_E/F] = @GATE_IN_E_F,
        [PREUZIMANJE_PUNOG/Razvoz] = @PREUZIMANJE_PUNOG_RAZVOZ,
        [VRAĆANJE_PRAZNOG/iz_Razvoza] = @VRACANJE_PRAZNOG_IZ_RAZVOZA,
        [Konačni_GATE_OUT] = @KONACNI_GATE_OUT,
        [BOOKING/IZVOZ] = @BOOKING_IZVOZ,
        [KLIJENT/IZVOZ] = @KLIJENT_IZVOZ,
        [GATE_OUT_EMPTY/Utovar] = @GATE_OUT_EMPTY_UTOVAR,
        [GATE_IN_FULL/sa_Utovara] = @GATE_IN_FULL_SA_UTOVARA,
        [GATE_IN/_GATE_OUT] = @GATE_IN_GATE_OUT,
        [L/R] = @L_R,
        [TARA] = @TARA,
        [MAX_NOSIVOST_CNT] = @MAX_NOSIVOST_CNT,
        [VOZILO/PREUZIMANJE] = @VOZILO_PREUZIMANJE,
        [PLOMBA/IZVOZ] = @PLOMBA_IZVOZ,
        [NAPOMENA] = @NAPOMENA,
        [OPIS] = @OPIS,
        [OTPREMA] = @OTPREMA,
        [POSLATE SLIKE] = @POSLATE_SLIKE,
        [Prevoznik] = @PREVOZNIK,
        [BELEŠKA] = @BELESKA
    WHERE ID = @ID;

    IF @ZapisiMaersk = 1
        EXEC dbo.insTerminalPrivMaerskPokreti
            @ID = @ID, @KONTEJNER = @KONTEJNER, @BRODAR = @BRODAR, @GATE_IN_GATE_OUT = @GATE_IN_GATE_OUT,
            @OTPREMA = @OTPREMA, @BOOKING_IZVOZ = @BOOKING_IZVOZ, @BL_UVOZ = @BL_UVOZ,
            @GATE_IN_E_F = @GATE_IN_E_F, @PREUZIMANJE_PUNOG_RAZVOZ = @PREUZIMANJE_PUNOG_RAZVOZ,
            @VRACANJE_PRAZNOG_IZ_RAZVOZA = @VRACANJE_PRAZNOG_IZ_RAZVOZA, @KONACNI_GATE_OUT = @KONACNI_GATE_OUT,
            @GATE_OUT_EMPTY_UTOVAR = @GATE_OUT_EMPTY_UTOVAR, @GATE_IN_FULL_SA_UTOVARA = @GATE_IN_FULL_SA_UTOVARA,
            @StariGateInEF = @StariGateInEF, @StariPreuzimanje = @StariPreuzimanje, @StariVracanje = @StariVracanje,
            @StariKonacni = @StariKonacni, @StariGateOutEmpty = @StariGateOutEmpty, @StariGateInFull = @StariGateInFull,
            @BrojUpisanih = @BrojMaersk OUTPUT;

    IF @ZapisiMaersk = 1
        EXEC dbo.insTerminalPrivMSCPokreti
            @ID = @ID, @KONTEJNER = @KONTEJNER, @BRODAR = @BRODAR, @GATE_IN_GATE_OUT = @GATE_IN_GATE_OUT,
            @OTPREMA = @OTPREMA, @BOOKING_IZVOZ = @BOOKING_IZVOZ, @BL_UVOZ = @BL_UVOZ,
            @GATE_IN_E_F = @GATE_IN_E_F, @PREUZIMANJE_PUNOG_RAZVOZ = @PREUZIMANJE_PUNOG_RAZVOZ,
            @VRACANJE_PRAZNOG_IZ_RAZVOZA = @VRACANJE_PRAZNOG_IZ_RAZVOZA, @KONACNI_GATE_OUT = @KONACNI_GATE_OUT,
            @GATE_OUT_EMPTY_UTOVAR = @GATE_OUT_EMPTY_UTOVAR, @GATE_IN_FULL_SA_UTOVARA = @GATE_IN_FULL_SA_UTOVARA,
            @StariGateInEF = @StariGateInEF, @StariPreuzimanje = @StariPreuzimanje, @StariVracanje = @StariVracanje,
            @StariKonacni = @StariKonacni, @StariGateOutEmpty = @StariGateOutEmpty, @StariGateInFull = @StariGateInFull,
            @BrojUpisanih = @BrojMSC OUTPUT;

    IF @ZapisiMaersk = 1
        EXEC dbo.insTerminalPrivCMAPokreti
            @ID = @ID, @KONTEJNER = @KONTEJNER, @BRODAR = @BRODAR, @VRSTA = @VRSTA, @VOZ_KAMION = @VOZ_KAMION,
            @GATE_IN_GATE_OUT = @GATE_IN_GATE_OUT, @OTPREMA = @OTPREMA, @BOOKING_IZVOZ = @BOOKING_IZVOZ, @BL_UVOZ = @BL_UVOZ,
            @GATE_IN_E_F = @GATE_IN_E_F, @PREUZIMANJE_PUNOG_RAZVOZ = @PREUZIMANJE_PUNOG_RAZVOZ,
            @VRACANJE_PRAZNOG_IZ_RAZVOZA = @VRACANJE_PRAZNOG_IZ_RAZVOZA, @KONACNI_GATE_OUT = @KONACNI_GATE_OUT,
            @GATE_OUT_EMPTY_UTOVAR = @GATE_OUT_EMPTY_UTOVAR, @GATE_IN_FULL_SA_UTOVARA = @GATE_IN_FULL_SA_UTOVARA,
            @StariGateInEF = @StariGateInEF, @StariPreuzimanje = @StariPreuzimanje, @StariVracanje = @StariVracanje,
            @StariKonacni = @StariKonacni, @StariGateOutEmpty = @StariGateOutEmpty, @StariGateInFull = @StariGateInFull,
            @BrojUpisanih = @BrojCMA OUTPUT;

    IF @ZapisiMaersk = 1
        EXEC dbo.insTerminalPrivOOCLPokreti
            @ID = @ID, @KONTEJNER = @KONTEJNER, @BRODAR = @BRODAR, @VRSTA = @VRSTA, @VOZ_KAMION = @VOZ_KAMION,
            @STANJE = @STANJE, @GATE_IN_GATE_OUT = @GATE_IN_GATE_OUT, @OTPREMA = @OTPREMA,
            @BOOKING_IZVOZ = @BOOKING_IZVOZ, @BL_UVOZ = @BL_UVOZ,
            @GATE_IN_E_F = @GATE_IN_E_F, @PREUZIMANJE_PUNOG_RAZVOZ = @PREUZIMANJE_PUNOG_RAZVOZ,
            @VRACANJE_PRAZNOG_IZ_RAZVOZA = @VRACANJE_PRAZNOG_IZ_RAZVOZA, @KONACNI_GATE_OUT = @KONACNI_GATE_OUT,
            @GATE_OUT_EMPTY_UTOVAR = @GATE_OUT_EMPTY_UTOVAR, @GATE_IN_FULL_SA_UTOVARA = @GATE_IN_FULL_SA_UTOVARA,
            @StariGateInEF = @StariGateInEF, @StariPreuzimanje = @StariPreuzimanje, @StariVracanje = @StariVracanje,
            @StariKonacni = @StariKonacni, @StariGateOutEmpty = @StariGateOutEmpty, @StariGateInFull = @StariGateInFull,
            @BrojUpisanih = @BrojOOCL OUTPUT;

    IF @ZapisiMaersk = 1
        EXEC dbo.insTerminalPrivHMMPokreti
            @ID = @ID, @KONTEJNER = @KONTEJNER, @BRODAR = @BRODAR, @VRSTA = @VRSTA, @VOZ_KAMION = @VOZ_KAMION,
            @GATE_IN_GATE_OUT = @GATE_IN_GATE_OUT, @BOOKING_IZVOZ = @BOOKING_IZVOZ, @PLOMBA_IZVOZ = @PLOMBA_IZVOZ,
            @BL_UVOZ = @BL_UVOZ,
            @GATE_IN_E_F = @GATE_IN_E_F, @PREUZIMANJE_PUNOG_RAZVOZ = @PREUZIMANJE_PUNOG_RAZVOZ,
            @VRACANJE_PRAZNOG_IZ_RAZVOZA = @VRACANJE_PRAZNOG_IZ_RAZVOZA, @KONACNI_GATE_OUT = @KONACNI_GATE_OUT,
            @GATE_OUT_EMPTY_UTOVAR = @GATE_OUT_EMPTY_UTOVAR, @GATE_IN_FULL_SA_UTOVARA = @GATE_IN_FULL_SA_UTOVARA,
            @StariGateInEF = @StariGateInEF, @StariPreuzimanje = @StariPreuzimanje, @StariVracanje = @StariVracanje,
            @StariKonacni = @StariKonacni, @StariGateOutEmpty = @StariGateOutEmpty, @StariGateInFull = @StariGateInFull,
            @BrojUpisanih = @BrojHMM OUTPUT;

    IF @ZapisiMaersk = 1
        EXEC dbo.insTerminalPrivHAPAGPokreti
            @ID = @ID, @KONTEJNER = @KONTEJNER, @BRODAR = @BRODAR, @VRSTA = @VRSTA, @VOZ_KAMION = @VOZ_KAMION,
            @GATE_IN_GATE_OUT = @GATE_IN_GATE_OUT, @BOOKING_IZVOZ = @BOOKING_IZVOZ, @PLOMBA_IZVOZ = @PLOMBA_IZVOZ,
            @BL_UVOZ = @BL_UVOZ, @PLOMBA_UVOZ = @PLOMBA_UVOZ,
            @GATE_IN_E_F = @GATE_IN_E_F, @PREUZIMANJE_PUNOG_RAZVOZ = @PREUZIMANJE_PUNOG_RAZVOZ,
            @VRACANJE_PRAZNOG_IZ_RAZVOZA = @VRACANJE_PRAZNOG_IZ_RAZVOZA, @KONACNI_GATE_OUT = @KONACNI_GATE_OUT,
            @GATE_OUT_EMPTY_UTOVAR = @GATE_OUT_EMPTY_UTOVAR, @GATE_IN_FULL_SA_UTOVARA = @GATE_IN_FULL_SA_UTOVARA,
            @StariGateInEF = @StariGateInEF, @StariPreuzimanje = @StariPreuzimanje, @StariVracanje = @StariVracanje,
            @StariKonacni = @StariKonacni, @StariGateOutEmpty = @StariGateOutEmpty, @StariGateInFull = @StariGateInFull,
            @BrojUpisanih = @BrojHAPAG OUTPUT;

    IF @ZapisiMaersk = 1
        EXEC dbo.insTerminalPrivFLEXBOXPokreti
            @ID = @ID, @KONTEJNER = @KONTEJNER, @BRODAR = @BRODAR, @VRSTA = @VRSTA, @VOZ_KAMION = @VOZ_KAMION,
            @GATE_IN_GATE_OUT = @GATE_IN_GATE_OUT, @BOOKING_IZVOZ = @BOOKING_IZVOZ, @PLOMBA_IZVOZ = @PLOMBA_IZVOZ,
            @BL_UVOZ = @BL_UVOZ,
            @GATE_IN_E_F = @GATE_IN_E_F, @PREUZIMANJE_PUNOG_RAZVOZ = @PREUZIMANJE_PUNOG_RAZVOZ,
            @VRACANJE_PRAZNOG_IZ_RAZVOZA = @VRACANJE_PRAZNOG_IZ_RAZVOZA, @KONACNI_GATE_OUT = @KONACNI_GATE_OUT,
            @GATE_OUT_EMPTY_UTOVAR = @GATE_OUT_EMPTY_UTOVAR, @GATE_IN_FULL_SA_UTOVARA = @GATE_IN_FULL_SA_UTOVARA,
            @StariGateInEF = @StariGateInEF, @StariPreuzimanje = @StariPreuzimanje, @StariVracanje = @StariVracanje,
            @StariKonacni = @StariKonacni, @StariGateOutEmpty = @StariGateOutEmpty, @StariGateInFull = @StariGateInFull,
            @BrojUpisanih = @BrojFLEXBOX OUTPUT;

    IF @ZapisiMaersk = 1
        EXEC dbo.insTerminalPrivCOSCOPokreti
            @ID = @ID, @KONTEJNER = @KONTEJNER, @BRODAR = @BRODAR, @VRSTA = @VRSTA, @VOZ_KAMION = @VOZ_KAMION,
            @GATE_IN_GATE_OUT = @GATE_IN_GATE_OUT, @OTPREMA = @OTPREMA, @BOOKING_IZVOZ = @BOOKING_IZVOZ, @BL_UVOZ = @BL_UVOZ,
            @GATE_IN_E_F = @GATE_IN_E_F, @PREUZIMANJE_PUNOG_RAZVOZ = @PREUZIMANJE_PUNOG_RAZVOZ,
            @VRACANJE_PRAZNOG_IZ_RAZVOZA = @VRACANJE_PRAZNOG_IZ_RAZVOZA, @KONACNI_GATE_OUT = @KONACNI_GATE_OUT,
            @GATE_OUT_EMPTY_UTOVAR = @GATE_OUT_EMPTY_UTOVAR, @GATE_IN_FULL_SA_UTOVARA = @GATE_IN_FULL_SA_UTOVARA,
            @StariGateInEF = @StariGateInEF, @StariPreuzimanje = @StariPreuzimanje, @StariVracanje = @StariVracanje,
            @StariKonacni = @StariKonacni, @StariGateOutEmpty = @StariGateOutEmpty, @StariGateInFull = @StariGateInFull,
            @BrojUpisanih = @BrojCOSCO OUTPUT;
END
GO

SET NOEXEC OFF;
GO
