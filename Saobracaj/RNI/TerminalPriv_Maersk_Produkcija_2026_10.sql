-- PRODUKCIJA (oktobar 2026): Terminal privremeni - Maersk, MSC i CMA pokreti, kolona BELEŠKA, šifarnik VRSTA.
-- 1. Tabele TerminalPrivMaersk, TerminalPrivMSC i TerminalPrivCMA sa indeksima (ako ne postoje), procedure
--    insTerminalPrivMaerskPokreti, insTerminalPrivMSCPokreti, insTerminalPrivCMAPokreti, insTerminalPriv i updTerminalPriv.
-- 2. Kolona BELEŠKA u TerminalPriv i TerminalPrivArhiv, procedura InsertTerminalPrivArhiv i log trigger trg_TerminalPriv_Log.
-- 3. VRSTA u TerminalPriv se prevodi u oblik iz šifarnika (40'HC -> 40' HC); na kraju se prikazuju vrednosti za ručnu ispravku.
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
        (11, 'GATE_IN_EF',   N'GATE IN F',  0,    0,    1,    1,    N'IRI', N'DOLAZAK PUNOG NA TERMINAL',              N'GATE IN',  N'IMPORT', N'FULL',  'L', N'TRUCK')
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
    @BrojCMA int = 0 OUTPUT      -- broj upisanih pokreta u TerminalPrivCMA
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
    @BrojCMA int = 0 OUTPUT      -- broj upisanih pokreta u TerminalPrivCMA
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
END
GO

-- Arhiva zapisa TerminalPriv: tabela TerminalPrivArhiv + procedure InsertTerminalPrivArhiv i updTerminalPrivArhivPoslateSlike
-- Forma: Saobracaj.RNI.frmTerminalPrivremeniArhiva. Skriptu izvrsiti nad bazom aplikacije (posle TerminalPriv.sql i TerminalPrivremeniLog.sql).
-- ID zapisa se pri arhiviranju cuva isti kao u TerminalPriv, pa folderi slika/dokumenata i log ostaju povezani sa zapisom.
-- Tabela nema CHECK ogranicenja jer ih SQL Server ne dozvoljava na cilju OUTPUT INTO; zapise prima samo iz TerminalPriv (vec provereni).

IF OBJECT_ID('dbo.TerminalPrivArhiv', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.TerminalPrivArhiv
    (
        ID                                 int            NOT NULL,   -- ID iz TerminalPriv (nije IDENTITY)
        [KONTEJNER]                        nvarchar(30)   NOT NULL,
        [STATUS]                           nvarchar(30)   NULL,
        [POZICIJA]                         nvarchar(30)   NULL,
        [VRSTA]                            nvarchar(30)   NULL,
        [BRODAR]                           nvarchar(30)   NULL,
        [NALOGODAVAC/UVOZ]                 nvarchar(50)   NULL,
        [POSTUPAK/UVOZ]                    nvarchar(50)   NULL,
        [UVOZNIK]                          nvarchar(50)   NULL,
        [BL/UVOZ]                          nvarchar(30)   NULL,
        [PLOMBA_UVOZ]                      nvarchar(30)   NULL,
        [VOZ/kamion]                       nvarchar(30)   NULL,
        [STANJE]                           nvarchar(30)   NULL,
        [GATE_IN_E/F]                      datetime       NULL,
        [PREUZIMANJE_PUNOG/Razvoz]         datetime       NULL,
        [VRAĆANJE_PRAZNOG/iz_Razvoza]      datetime       NULL,
        [Konačni_GATE_OUT]                 datetime       NULL,
        [BOOKING/IZVOZ]                    nvarchar(30)   NULL,
        [KLIJENT/IZVOZ]                    nvarchar(30)   NULL,
        [GATE_OUT_EMPTY/Utovar]            datetime       NULL,
        [GATE_IN_FULL/sa_Utovara]          datetime       NULL,
        [GATE_IN/_GATE_OUT]                nvarchar(50)   NULL,
        [L/R]                              nvarchar(30)   NULL,
        [TARA]                             nvarchar(30)   NULL,
        [MAX_NOSIVOST_CNT]                 nvarchar(30)   NULL,
        [VOZILO/PREUZIMANJE]               nvarchar(30)   NULL,
        [PLOMBA/IZVOZ]                     nvarchar(30)   NULL,
        [NAPOMENA]                         nvarchar(300)  NULL,
        [OPIS]                             nvarchar(300)  NULL,
        [OTPREMA]                          nvarchar(30)   NULL,
        [POSLATE SLIKE]                    nvarchar(30)   NULL,
        [Prevoznik]                        nvarchar(30)   NULL,
        [BELEŠKA]                          nvarchar(300)  NULL,
        DatumArhiviranja                   datetime       NOT NULL CONSTRAINT DF_TerminalPrivArhiv_Datum DEFAULT (GETDATE()),
        CONSTRAINT PK_TerminalPrivArhiv PRIMARY KEY CLUSTERED (ID)
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_TerminalPrivArhiv_KONTEJNER' AND object_id = OBJECT_ID('dbo.TerminalPrivArhiv'))
    CREATE NONCLUSTERED INDEX IX_TerminalPrivArhiv_KONTEJNER ON dbo.TerminalPrivArhiv (KONTEJNER);
GO

-- kolona BELEŠKA (oktobar 2026) za tabelu kreiranu pre njenog dodavanja
IF COL_LENGTH(N'dbo.TerminalPrivArhiv', N'BELEŠKA') IS NULL
    ALTER TABLE dbo.TerminalPrivArhiv ADD [BELEŠKA] nvarchar(300) NULL;
GO

-- Svi zapisi iz TerminalPriv kod kojih Konacni_GATE_OUT nije NULL prebacuju se u TerminalPrivArhiv i brisu iz TerminalPriv.
-- DELETE ... OUTPUT INTO je jedan atomican upis (ako nesto pukne, nista se ne prebacuje). Vraca broj arhiviranih zapisa.
IF OBJECT_ID('dbo.InsertTerminalPrivArhiv', 'P') IS NOT NULL DROP PROCEDURE dbo.InsertTerminalPrivArhiv;
GO

CREATE PROCEDURE dbo.InsertTerminalPrivArhiv
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @Arhivirano int = 0;

    -- trigger na TerminalPriv na osnovu ovoga u log upisuje ARHIVA umesto DELETE
    EXEC sp_set_session_context @key = N'ArhivirajTerminalPriv', @value = 1;

    BEGIN TRY
        DELETE t
        OUTPUT
            deleted.ID, deleted.[KONTEJNER], deleted.[STATUS], deleted.[POZICIJA], deleted.[VRSTA], deleted.[BRODAR], deleted.[NALOGODAVAC/UVOZ], deleted.[POSTUPAK/UVOZ], deleted.[UVOZNIK], deleted.[BL/UVOZ], deleted.[PLOMBA_UVOZ], deleted.[VOZ/kamion], deleted.[STANJE], deleted.[GATE_IN_E/F], deleted.[PREUZIMANJE_PUNOG/Razvoz], deleted.[VRAĆANJE_PRAZNOG/iz_Razvoza], deleted.[Konačni_GATE_OUT], deleted.[BOOKING/IZVOZ], deleted.[KLIJENT/IZVOZ], deleted.[GATE_OUT_EMPTY/Utovar], deleted.[GATE_IN_FULL/sa_Utovara], deleted.[GATE_IN/_GATE_OUT], deleted.[L/R], deleted.[TARA], deleted.[MAX_NOSIVOST_CNT], deleted.[VOZILO/PREUZIMANJE], deleted.[PLOMBA/IZVOZ], deleted.[NAPOMENA], deleted.[OPIS], deleted.[OTPREMA], deleted.[POSLATE SLIKE], deleted.[Prevoznik], deleted.[BELEŠKA]
        INTO dbo.TerminalPrivArhiv
            (ID, [KONTEJNER], [STATUS], [POZICIJA], [VRSTA], [BRODAR], [NALOGODAVAC/UVOZ], [POSTUPAK/UVOZ], [UVOZNIK], [BL/UVOZ], [PLOMBA_UVOZ], [VOZ/kamion], [STANJE], [GATE_IN_E/F], [PREUZIMANJE_PUNOG/Razvoz], [VRAĆANJE_PRAZNOG/iz_Razvoza], [Konačni_GATE_OUT], [BOOKING/IZVOZ], [KLIJENT/IZVOZ], [GATE_OUT_EMPTY/Utovar], [GATE_IN_FULL/sa_Utovara], [GATE_IN/_GATE_OUT], [L/R], [TARA], [MAX_NOSIVOST_CNT], [VOZILO/PREUZIMANJE], [PLOMBA/IZVOZ], [NAPOMENA], [OPIS], [OTPREMA], [POSLATE SLIKE], [Prevoznik], [BELEŠKA])
        FROM dbo.TerminalPriv t
        WHERE t.[Konačni_GATE_OUT] IS NOT NULL;

        SET @Arhivirano = @@ROWCOUNT;
    END TRY
    BEGIN CATCH
        EXEC sp_set_session_context @key = N'ArhivirajTerminalPriv', @value = 0;
        THROW;
    END CATCH

    EXEC sp_set_session_context @key = N'ArhivirajTerminalPriv', @value = 0;

    SELECT @Arhivirano AS Arhivirano;
END
GO

-- Broj poslatih slika za arhivirani zapis (frmTerminalPrivremeniSlike otvoren iz arhive)
IF OBJECT_ID('dbo.updTerminalPrivArhivPoslateSlike', 'P') IS NOT NULL DROP PROCEDURE dbo.updTerminalPrivArhivPoslateSlike;
GO

CREATE PROCEDURE dbo.updTerminalPrivArhivPoslateSlike
    @ID int,
    @POSLATE_SLIKE nvarchar(30) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.TerminalPrivArhiv SET [POSLATE SLIKE] = @POSLATE_SLIKE WHERE ID = @ID;
END
GO

-- Log izmena tabele TerminalPriv: tabela TerminalPrivremeniLog + trigger trg_TerminalPriv_Log
-- Forma: Saobracaj.RNI.frmTerminalPrivremeniLog. Skriptu izvrsiti nad bazom aplikacije (posle TerminalPriv.sql).

IF OBJECT_ID('dbo.TerminalPrivremeniLog', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.TerminalPrivremeniLog
    (
        LogID          int IDENTITY(1,1) NOT NULL,
        TerminalPrivID int           NOT NULL,   -- ID zapisa u TerminalPriv
        Datum          datetime      NOT NULL CONSTRAINT DF_TerminalPrivremeniLog_Datum DEFAULT (GETDATE()),
        Akcija         nvarchar(10)  NOT NULL,   -- INSERT / UPDATE / DELETE / ARHIVA
        Kontejner      nvarchar(30)  NULL,
        Opis           nvarchar(max) NULL,       -- kod UPDATE: kolona: staro -> novo
        Racunar        nvarchar(128) NULL CONSTRAINT DF_TerminalPrivremeniLog_Racunar DEFAULT (HOST_NAME()),
        CONSTRAINT PK_TerminalPrivremeniLog PRIMARY KEY CLUSTERED (LogID)
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_TerminalPrivremeniLog_ID_Datum' AND object_id = OBJECT_ID('dbo.TerminalPrivremeniLog'))
    CREATE NONCLUSTERED INDEX IX_TerminalPrivremeniLog_ID_Datum ON dbo.TerminalPrivremeniLog (TerminalPrivID, Datum DESC);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_TerminalPrivremeniLog_Datum' AND object_id = OBJECT_ID('dbo.TerminalPrivremeniLog'))
    CREATE NONCLUSTERED INDEX IX_TerminalPrivremeniLog_Datum ON dbo.TerminalPrivremeniLog (Datum DESC);
GO

IF OBJECT_ID('dbo.trg_TerminalPriv_Log', 'TR') IS NOT NULL DROP TRIGGER dbo.trg_TerminalPriv_Log;
GO

CREATE TRIGGER dbo.trg_TerminalPriv_Log
ON dbo.TerminalPriv
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM inserted) AND EXISTS (SELECT 1 FROM deleted)
    BEGIN
        -- UPDATE: upisuju se samo promenjene kolone (staro -> novo); red bez promena se ne loguje
        INSERT INTO dbo.TerminalPrivremeniLog (TerminalPrivID, Akcija, Kontejner, Opis)
        SELECT i.ID, N'UPDATE', i.KONTEJNER, x.Opis
        FROM inserted i
        INNER JOIN deleted d ON d.ID = i.ID
        CROSS APPLY (SELECT CONCAT(
            CASE WHEN EXISTS (SELECT d.[KONTEJNER] EXCEPT SELECT i.[KONTEJNER]) THEN N'KONTEJNER: ' + ISNULL(d.[KONTEJNER], N'(prazno)') + N' -> ' + ISNULL(i.[KONTEJNER], N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[STATUS] EXCEPT SELECT i.[STATUS]) THEN N'STATUS: ' + ISNULL(d.[STATUS], N'(prazno)') + N' -> ' + ISNULL(i.[STATUS], N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[POZICIJA] EXCEPT SELECT i.[POZICIJA]) THEN N'POZICIJA: ' + ISNULL(d.[POZICIJA], N'(prazno)') + N' -> ' + ISNULL(i.[POZICIJA], N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[VRSTA] EXCEPT SELECT i.[VRSTA]) THEN N'VRSTA: ' + ISNULL(d.[VRSTA], N'(prazno)') + N' -> ' + ISNULL(i.[VRSTA], N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[BRODAR] EXCEPT SELECT i.[BRODAR]) THEN N'BRODAR: ' + ISNULL(d.[BRODAR], N'(prazno)') + N' -> ' + ISNULL(i.[BRODAR], N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[NALOGODAVAC/UVOZ] EXCEPT SELECT i.[NALOGODAVAC/UVOZ]) THEN N'NALOGODAVAC/UVOZ: ' + ISNULL(d.[NALOGODAVAC/UVOZ], N'(prazno)') + N' -> ' + ISNULL(i.[NALOGODAVAC/UVOZ], N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[POSTUPAK/UVOZ] EXCEPT SELECT i.[POSTUPAK/UVOZ]) THEN N'POSTUPAK/UVOZ: ' + ISNULL(d.[POSTUPAK/UVOZ], N'(prazno)') + N' -> ' + ISNULL(i.[POSTUPAK/UVOZ], N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[UVOZNIK] EXCEPT SELECT i.[UVOZNIK]) THEN N'UVOZNIK: ' + ISNULL(d.[UVOZNIK], N'(prazno)') + N' -> ' + ISNULL(i.[UVOZNIK], N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[BL/UVOZ] EXCEPT SELECT i.[BL/UVOZ]) THEN N'BL/UVOZ: ' + ISNULL(d.[BL/UVOZ], N'(prazno)') + N' -> ' + ISNULL(i.[BL/UVOZ], N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[PLOMBA_UVOZ] EXCEPT SELECT i.[PLOMBA_UVOZ]) THEN N'PLOMBA_UVOZ: ' + ISNULL(d.[PLOMBA_UVOZ], N'(prazno)') + N' -> ' + ISNULL(i.[PLOMBA_UVOZ], N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[VOZ/kamion] EXCEPT SELECT i.[VOZ/kamion]) THEN N'VOZ/kamion: ' + ISNULL(d.[VOZ/kamion], N'(prazno)') + N' -> ' + ISNULL(i.[VOZ/kamion], N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[STANJE] EXCEPT SELECT i.[STANJE]) THEN N'STANJE: ' + ISNULL(d.[STANJE], N'(prazno)') + N' -> ' + ISNULL(i.[STANJE], N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[GATE_IN_E/F] EXCEPT SELECT i.[GATE_IN_E/F]) THEN N'GATE_IN_E/F: ' + ISNULL(CONVERT(nvarchar(20), d.[GATE_IN_E/F], 120), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(20), i.[GATE_IN_E/F], 120), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[PREUZIMANJE_PUNOG/Razvoz] EXCEPT SELECT i.[PREUZIMANJE_PUNOG/Razvoz]) THEN N'PREUZIMANJE_PUNOG/Razvoz: ' + ISNULL(CONVERT(nvarchar(20), d.[PREUZIMANJE_PUNOG/Razvoz], 120), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(20), i.[PREUZIMANJE_PUNOG/Razvoz], 120), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[VRAĆANJE_PRAZNOG/iz_Razvoza] EXCEPT SELECT i.[VRAĆANJE_PRAZNOG/iz_Razvoza]) THEN N'VRAĆANJE_PRAZNOG/iz_Razvoza: ' + ISNULL(CONVERT(nvarchar(20), d.[VRAĆANJE_PRAZNOG/iz_Razvoza], 120), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(20), i.[VRAĆANJE_PRAZNOG/iz_Razvoza], 120), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[Konačni_GATE_OUT] EXCEPT SELECT i.[Konačni_GATE_OUT]) THEN N'Konačni_GATE_OUT: ' + ISNULL(CONVERT(nvarchar(20), d.[Konačni_GATE_OUT], 120), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(20), i.[Konačni_GATE_OUT], 120), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[BOOKING/IZVOZ] EXCEPT SELECT i.[BOOKING/IZVOZ]) THEN N'BOOKING/IZVOZ: ' + ISNULL(d.[BOOKING/IZVOZ], N'(prazno)') + N' -> ' + ISNULL(i.[BOOKING/IZVOZ], N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[KLIJENT/IZVOZ] EXCEPT SELECT i.[KLIJENT/IZVOZ]) THEN N'KLIJENT/IZVOZ: ' + ISNULL(d.[KLIJENT/IZVOZ], N'(prazno)') + N' -> ' + ISNULL(i.[KLIJENT/IZVOZ], N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[GATE_OUT_EMPTY/Utovar] EXCEPT SELECT i.[GATE_OUT_EMPTY/Utovar]) THEN N'GATE_OUT_EMPTY/Utovar: ' + ISNULL(CONVERT(nvarchar(20), d.[GATE_OUT_EMPTY/Utovar], 120), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(20), i.[GATE_OUT_EMPTY/Utovar], 120), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[GATE_IN_FULL/sa_Utovara] EXCEPT SELECT i.[GATE_IN_FULL/sa_Utovara]) THEN N'GATE_IN_FULL/sa_Utovara: ' + ISNULL(CONVERT(nvarchar(20), d.[GATE_IN_FULL/sa_Utovara], 120), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(20), i.[GATE_IN_FULL/sa_Utovara], 120), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[GATE_IN/_GATE_OUT] EXCEPT SELECT i.[GATE_IN/_GATE_OUT]) THEN N'GATE_IN/_GATE_OUT: ' + ISNULL(d.[GATE_IN/_GATE_OUT], N'(prazno)') + N' -> ' + ISNULL(i.[GATE_IN/_GATE_OUT], N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[L/R] EXCEPT SELECT i.[L/R]) THEN N'L/R: ' + ISNULL(d.[L/R], N'(prazno)') + N' -> ' + ISNULL(i.[L/R], N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[TARA] EXCEPT SELECT i.[TARA]) THEN N'TARA: ' + ISNULL(d.[TARA], N'(prazno)') + N' -> ' + ISNULL(i.[TARA], N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[MAX_NOSIVOST_CNT] EXCEPT SELECT i.[MAX_NOSIVOST_CNT]) THEN N'MAX_NOSIVOST_CNT: ' + ISNULL(d.[MAX_NOSIVOST_CNT], N'(prazno)') + N' -> ' + ISNULL(i.[MAX_NOSIVOST_CNT], N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[VOZILO/PREUZIMANJE] EXCEPT SELECT i.[VOZILO/PREUZIMANJE]) THEN N'VOZILO/PREUZIMANJE: ' + ISNULL(d.[VOZILO/PREUZIMANJE], N'(prazno)') + N' -> ' + ISNULL(i.[VOZILO/PREUZIMANJE], N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[PLOMBA/IZVOZ] EXCEPT SELECT i.[PLOMBA/IZVOZ]) THEN N'PLOMBA/IZVOZ: ' + ISNULL(d.[PLOMBA/IZVOZ], N'(prazno)') + N' -> ' + ISNULL(i.[PLOMBA/IZVOZ], N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[NAPOMENA] EXCEPT SELECT i.[NAPOMENA]) THEN N'NAPOMENA: ' + ISNULL(d.[NAPOMENA], N'(prazno)') + N' -> ' + ISNULL(i.[NAPOMENA], N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[OPIS] EXCEPT SELECT i.[OPIS]) THEN N'OPIS: ' + ISNULL(d.[OPIS], N'(prazno)') + N' -> ' + ISNULL(i.[OPIS], N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[OTPREMA] EXCEPT SELECT i.[OTPREMA]) THEN N'OTPREMA: ' + ISNULL(d.[OTPREMA], N'(prazno)') + N' -> ' + ISNULL(i.[OTPREMA], N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[POSLATE SLIKE] EXCEPT SELECT i.[POSLATE SLIKE]) THEN N'POSLATE SLIKE: ' + ISNULL(d.[POSLATE SLIKE], N'(prazno)') + N' -> ' + ISNULL(i.[POSLATE SLIKE], N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[Prevoznik] EXCEPT SELECT i.[Prevoznik]) THEN N'Prevoznik: ' + ISNULL(d.[Prevoznik], N'(prazno)') + N' -> ' + ISNULL(i.[Prevoznik], N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[BELEŠKA] EXCEPT SELECT i.[BELEŠKA]) THEN N'BELEŠKA: ' + ISNULL(d.[BELEŠKA], N'(prazno)') + N' -> ' + ISNULL(i.[BELEŠKA], N'(prazno)') + N'; ' ELSE N'' END
        ) AS Opis) x
        WHERE LEN(x.Opis) > 0;
    END
    ELSE IF EXISTS (SELECT 1 FROM inserted)
    BEGIN
        INSERT INTO dbo.TerminalPrivremeniLog (TerminalPrivID, Akcija, Kontejner, Opis)
        SELECT ID, N'INSERT', KONTEJNER, N'Novi zapis' FROM inserted;
    END
    ELSE
    BEGIN
        INSERT INTO dbo.TerminalPrivremeniLog (TerminalPrivID, Akcija, Kontejner, Opis)
        -- InsertTerminalPrivArhiv postavlja session context pa se prebacivanje u arhivu razlikuje od brisanja
        SELECT ID,
               CASE WHEN CAST(SESSION_CONTEXT(N'ArhivirajTerminalPriv') AS int) = 1 THEN N'ARHIVA' ELSE N'DELETE' END,
               KONTEJNER,
               CASE WHEN CAST(SESSION_CONTEXT(N'ArhivirajTerminalPriv') AS int) = 1 THEN N'Prebačeno u arhivu' ELSE N'Obrisan zapis' END
        FROM deleted;
    END
END
GO

-- VRSTA (vrsta kontejnera) u TerminalPriv prevodi se u oblik iz šifarnika (insertTerminalPriv.VrsteCnt).
-- Vrednost koja se od šifarnika razlikuje samo po razmaku, apostrofu ili velikim/malim slovima se menja
-- (40'HC, 40HC -> 40' HC). Ostale vrednosti (npr. 40RH, 20, 30) ostaju; forma ih prikazuje i pri čuvanju
-- traži izbor iz šifarnika. Skripta se može pustiti više puta.

UPDATE t
SET [VRSTA] = s.Vrsta
FROM dbo.TerminalPriv t
INNER JOIN (VALUES
    (N'20'' DB'), (N'20'' HC'), (N'20'' OT'), (N'20'' TC'), (N'40'' DB'),
    (N'40'' HC'), (N'40'' OT'), (N'40'' FLAT'), (N'40'' RF'), (N'45'' HC')
) s (Vrsta)
    ON UPPER(REPLACE(REPLACE(t.[VRSTA], N' ', N''), N'''', N'')) = UPPER(REPLACE(REPLACE(s.Vrsta, N' ', N''), N'''', N''))
WHERE t.[VRSTA] <> s.Vrsta COLLATE Latin1_General_BIN;

SELECT @@ROWCOUNT AS Prevedeno;

-- Vrednosti koje nisu u šifarniku (treba ih ispraviti ručno)
SELECT [VRSTA], COUNT(*) AS Broj
FROM dbo.TerminalPriv
WHERE [VRSTA] IS NOT NULL
  AND [VRSTA] NOT IN (N'20'' DB', N'20'' HC', N'20'' OT', N'20'' TC', N'40'' DB', N'40'' HC', N'40'' OT', N'40'' FLAT', N'40'' RF', N'45'' HC')
GROUP BY [VRSTA];
GO

SET NOEXEC OFF;
GO
