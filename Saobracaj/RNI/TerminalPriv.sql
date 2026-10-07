-- Terminal privremeni: tabela TerminalPriv + stored procedure insTerminalPriv, updTerminalPriv, delTerminalPriv,
-- insTerminalPrivFromExcel i updTerminalPrivPoslateSlike. Forma: Saobracaj.RNI.frmTerminalPrivremeni
-- Nova baza: izvrsiti ovu skriptu. Postojeca baza (stara struktura kolona): prvo TerminalPriv_Izmena_2026_09.sql.

IF OBJECT_ID('dbo.TerminalPriv', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.TerminalPriv
    (
        ID                                 int            IDENTITY(1,1) NOT NULL,
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
        CONSTRAINT PK_TerminalPriv PRIMARY KEY CLUSTERED (ID),
        CONSTRAINT CK_TerminalPriv_KONTEJNER CHECK (LEN(KONTEJNER) = 11),
        CONSTRAINT CK_TerminalPriv_STATUS CHECK (STATUS IN (N'PRAZAN', N'PUN', N'?', N'RAZVOZ', N'Blanks', N'PRETOVAR', N'U RAZVOZU', N'UTOVAR')),
        CONSTRAINT CK_TerminalPriv_STANJE CHECK (STANJE IN (N'DOBAR', N'LOŠ', N'FOOD GRADE', N'OŠTEĆEN', N'FLEXI')),
        CONSTRAINT CK_TerminalPriv_GATE CHECK ([GATE_IN/_GATE_OUT] IN (N'GATE IN E', N'GATE IN F', N'GATE OUT E', N'GATE OUT F', N'GATE IN E/REPOZICIJA', N'PRIJAVITI GATE IN E', N'NE ŠALJEMO POKRET BRODARU', N'REUSE', N'REPOZICIJA', N'PRIVREMENO', N'NE PRIJAVLJUJEMO POKRETE', N'PRIJAVITI SVE POKRETE'))
    );
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
    @StariGateInFull datetime = NULL
AS
BEGIN
    SET NOCOUNT ON;

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
        ('VRACANJE',       N'GATE IN E',  0, 0, 0,    NULL,   0, NULL),
        ('GATE_OUT_EMPTY', N'GATE OUT E', 0, 1, 0,    NULL,   0, 'B')
    ) p (Polje, Gate, Otprema, Booking, BL, ActFore, RailCode, BkngIz)
        ON p.Polje = n.Polje
       AND p.Gate = @GATE_IN_GATE_OUT
       AND p.Otprema = @ImaOtpremu
       AND p.Booking = @ImaBooking
       AND (p.BL IS NULL OR p.BL = @ImaBL)
    WHERE n.Stara IS NULL AND n.Vrednost IS NOT NULL
    ORDER BY n.Vrednost, n.Redosled;   -- hronoloski
END
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
    @ZapisiMaersk bit = 0   -- 1 = novi zapis dugmetom 'Sačuvaj novi': upis pokreta u TerminalPrivMaersk
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NoviID int;

    INSERT INTO dbo.TerminalPriv
        ([KONTEJNER], [STATUS], [POZICIJA], [VRSTA], [BRODAR], [NALOGODAVAC/UVOZ], [POSTUPAK/UVOZ], [UVOZNIK], [BL/UVOZ], [PLOMBA_UVOZ], [VOZ/kamion], [STANJE], [GATE_IN_E/F], [PREUZIMANJE_PUNOG/Razvoz], [VRAĆANJE_PRAZNOG/iz_Razvoza], [Konačni_GATE_OUT], [BOOKING/IZVOZ], [KLIJENT/IZVOZ], [GATE_OUT_EMPTY/Utovar], [GATE_IN_FULL/sa_Utovara], [GATE_IN/_GATE_OUT], [L/R], [TARA], [MAX_NOSIVOST_CNT], [VOZILO/PREUZIMANJE], [PLOMBA/IZVOZ], [NAPOMENA], [OPIS], [OTPREMA], [POSLATE SLIKE], [Prevoznik])
    VALUES
        (@KONTEJNER, @STATUS, @POZICIJA, @VRSTA, @BRODAR, @NALOGODAVAC_UVOZ, @POSTUPAK_UVOZ, @UVOZNIK, @BL_UVOZ, @PLOMBA_UVOZ, @VOZ_KAMION, @STANJE, @GATE_IN_E_F, @PREUZIMANJE_PUNOG_RAZVOZ, @VRACANJE_PRAZNOG_IZ_RAZVOZA, @KONACNI_GATE_OUT, @BOOKING_IZVOZ, @KLIJENT_IZVOZ, @GATE_OUT_EMPTY_UTOVAR, @GATE_IN_FULL_SA_UTOVARA, @GATE_IN_GATE_OUT, @L_R, @TARA, @MAX_NOSIVOST_CNT, @VOZILO_PREUZIMANJE, @PLOMBA_IZVOZ, @NAPOMENA, @OPIS, @OTPREMA, @POSLATE_SLIKE, @PREVOZNIK);

    SET @NoviID = CAST(SCOPE_IDENTITY() AS int);

    -- novi zapis: svi datumi su pre upisa bili prazni
    IF @ZapisiMaersk = 1
        EXEC dbo.insTerminalPrivMaerskPokreti
            @ID = @NoviID, @KONTEJNER = @KONTEJNER, @BRODAR = @BRODAR, @GATE_IN_GATE_OUT = @GATE_IN_GATE_OUT,
            @OTPREMA = @OTPREMA, @BOOKING_IZVOZ = @BOOKING_IZVOZ, @BL_UVOZ = @BL_UVOZ,
            @GATE_IN_E_F = @GATE_IN_E_F, @PREUZIMANJE_PUNOG_RAZVOZ = @PREUZIMANJE_PUNOG_RAZVOZ,
            @VRACANJE_PRAZNOG_IZ_RAZVOZA = @VRACANJE_PRAZNOG_IZ_RAZVOZA, @KONACNI_GATE_OUT = @KONACNI_GATE_OUT,
            @GATE_OUT_EMPTY_UTOVAR = @GATE_OUT_EMPTY_UTOVAR, @GATE_IN_FULL_SA_UTOVARA = @GATE_IN_FULL_SA_UTOVARA;

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
    @ZapisiMaersk bit = 0   -- 1 = izmena dugmetom 'Promeni': upis pokreta u TerminalPrivMaersk
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
        [Prevoznik] = @PREVOZNIK
    WHERE ID = @ID;

    IF @ZapisiMaersk = 1
        EXEC dbo.insTerminalPrivMaerskPokreti
            @ID = @ID, @KONTEJNER = @KONTEJNER, @BRODAR = @BRODAR, @GATE_IN_GATE_OUT = @GATE_IN_GATE_OUT,
            @OTPREMA = @OTPREMA, @BOOKING_IZVOZ = @BOOKING_IZVOZ, @BL_UVOZ = @BL_UVOZ,
            @GATE_IN_E_F = @GATE_IN_E_F, @PREUZIMANJE_PUNOG_RAZVOZ = @PREUZIMANJE_PUNOG_RAZVOZ,
            @VRACANJE_PRAZNOG_IZ_RAZVOZA = @VRACANJE_PRAZNOG_IZ_RAZVOZA, @KONACNI_GATE_OUT = @KONACNI_GATE_OUT,
            @GATE_OUT_EMPTY_UTOVAR = @GATE_OUT_EMPTY_UTOVAR, @GATE_IN_FULL_SA_UTOVARA = @GATE_IN_FULL_SA_UTOVARA,
            @StariGateInEF = @StariGateInEF, @StariPreuzimanje = @StariPreuzimanje, @StariVracanje = @StariVracanje,
            @StariKonacni = @StariKonacni, @StariGateOutEmpty = @StariGateOutEmpty, @StariGateInFull = @StariGateInFull;
END
GO

IF OBJECT_ID('dbo.delTerminalPriv', 'P') IS NOT NULL DROP PROCEDURE dbo.delTerminalPriv;
GO
CREATE PROCEDURE dbo.delTerminalPriv
    @ID int
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM dbo.TerminalPriv WHERE ID = @ID;
END
GO

-- Uvoz iz Excel-a (frmTerminalPrivremeniExcel): upisuju se samo polja koja se uvoze, ostala ostaju NULL
-- Excel kolona C -> KONTEJNER, D -> VRSTA, K -> BRODAR, L -> NALOGODAVAC/UVOZ, T -> POSTUPAK/UVOZ, M -> UVOZNIK, H -> PLOMBA_UVOZ
IF OBJECT_ID('dbo.insTerminalPrivFromExcel', 'P') IS NOT NULL DROP PROCEDURE dbo.insTerminalPrivFromExcel;
GO
CREATE PROCEDURE dbo.insTerminalPrivFromExcel
    @KONTEJNER nvarchar(30),
    @VRSTA nvarchar(30) = NULL,
    @BRODAR nvarchar(30) = NULL,
    @NALOGODAVAC_UVOZ nvarchar(50) = NULL,
    @POSTUPAK_UVOZ nvarchar(50) = NULL,
    @UVOZNIK nvarchar(50) = NULL,
    @PLOMBA_UVOZ nvarchar(30) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.TerminalPriv ([KONTEJNER], [VRSTA], [BRODAR], [NALOGODAVAC/UVOZ], [POSTUPAK/UVOZ], [UVOZNIK], [PLOMBA_UVOZ])
    VALUES (@KONTEJNER, @VRSTA, @BRODAR, @NALOGODAVAC_UVOZ, @POSTUPAK_UVOZ, @UVOZNIK, @PLOMBA_UVOZ);
END
GO

-- Broj poslatih slika (frmTerminalPrivremeniSlike): menja samo polje POSLATE SLIKE
IF OBJECT_ID('dbo.updTerminalPrivPoslateSlike', 'P') IS NOT NULL DROP PROCEDURE dbo.updTerminalPrivPoslateSlike;
GO
CREATE PROCEDURE dbo.updTerminalPrivPoslateSlike
    @ID int,
    @POSLATE_SLIKE nvarchar(30) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.TerminalPriv SET [POSLATE SLIKE] = @POSLATE_SLIKE WHERE ID = @ID;
END
GO
