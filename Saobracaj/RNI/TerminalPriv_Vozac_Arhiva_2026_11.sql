-- PRODUKCIJA (novembar 2026): Terminal privremeni - štiklir VOZAČ, vraćanje zapisa iz arhive i pokret pri prelasku
-- GATE_IN/_GATE_OUT iz PRIVREMENO u GATE IN.
-- Sadrži:
--   1. kolonu [VOZAČ] bit (0 = ne, 1 = da) u TerminalPriv i TerminalPrivArhiv; prikazuje se u žutom okviru forme, ne u gridu,
--   2. procedure insTerminalPriv i updTerminalPriv sa parametrom @VOZAC (ostalo isto kao u TerminalPriv_Brodari_Produkcija_2026_10.sql);
--      updTerminalPriv još: kad GATE_IN/_GATE_OUT pređe iz PRIVREMENO u GATE IN E ili GATE IN F, a GATE_IN_E/F je već unet,
--      pokret u izveštaj brodaru se upisuje kao da je GATE_IN_E/F tek popunjen,
--   3. proceduru InsertTerminalPrivArhiv koja prenosi i VOZAČ,
--   4. log trigger trg_TerminalPriv_Log: promena VOZAČ i akcija VRAĆANJE (vraćanje iz arhive),
--   5. novu proceduru VratiTerminalPrivIzArhive (dugme 'Vrati iz arhive' u formi Arhivirani podaci).
-- Pustiti PRE instalacije nove verzije programa: nova verzija čita kolonu VOZAČ i šalje @VOZAC.
-- Stara verzija programa radi i posle ove skripte (@VOZAC nije obavezan; bez njega izmena ne menja VOZAČ).
-- Skripta se može pustiti više puta. Pre pokretanja izabrati produkcionu bazu.
-- Vraćanje iz arhive koristi SET IDENTITY_INSERT dbo.TerminalPriv: korisnik iz konekcije programa mora imati
-- pravo ALTER nad tabelom TerminalPriv (vlasnik baze ga ima).

-- Provera: moraju postojati tabele i procedure iz ranijih skripti
IF OBJECT_ID('dbo.TerminalPriv', 'U') IS NULL OR COL_LENGTH('dbo.TerminalPriv', 'BELEŠKA') IS NULL
   OR OBJECT_ID('dbo.TerminalPrivArhiv', 'U') IS NULL OR COL_LENGTH('dbo.TerminalPrivArhiv', 'BELEŠKA') IS NULL
   OR OBJECT_ID('dbo.TerminalPrivremeniLog', 'U') IS NULL
   OR OBJECT_ID('dbo.insTerminalPrivCOSCOPokreti', 'P') IS NULL
   OR COL_LENGTH('dbo.TerminalPrivOOCL', 'ActiveMail') IS NULL
BEGIN
    RAISERROR(N'Baza nema sve tabele i procedure Terminala privremenog. Prvo izvršiti TerminalPriv_Brodari_Produkcija_2026_10.sql i TerminalPriv_OOCL_Mail_2026_11.sql. Skripta je prekinuta.', 16, 1);
    SET NOEXEC ON;
END
GO

-- 1. Kolona VOZAČ (postojeći zapisi dobijaju 0)
IF COL_LENGTH(N'dbo.TerminalPriv', N'VOZAČ') IS NULL
    ALTER TABLE dbo.TerminalPriv ADD [VOZAČ] bit NOT NULL CONSTRAINT DF_TerminalPriv_VOZAC DEFAULT (0);
GO
IF COL_LENGTH(N'dbo.TerminalPrivArhiv', N'VOZAČ') IS NULL
    ALTER TABLE dbo.TerminalPrivArhiv ADD [VOZAČ] bit NOT NULL CONSTRAINT DF_TerminalPrivArhiv_VOZAC DEFAULT (0);
GO

-- 2. insTerminalPriv i updTerminalPriv: isto kao u TerminalPriv_Brodari_Produkcija_2026_10.sql, uz @VOZAC

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
    @VOZAC bit = NULL,      -- VOZAČ (štiklir); NULL = starija verzija programa
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
        ([KONTEJNER], [STATUS], [POZICIJA], [VRSTA], [BRODAR], [NALOGODAVAC/UVOZ], [POSTUPAK/UVOZ], [UVOZNIK], [BL/UVOZ], [PLOMBA_UVOZ], [VOZ/kamion], [STANJE], [GATE_IN_E/F], [PREUZIMANJE_PUNOG/Razvoz], [VRAĆANJE_PRAZNOG/iz_Razvoza], [Konačni_GATE_OUT], [BOOKING/IZVOZ], [KLIJENT/IZVOZ], [GATE_OUT_EMPTY/Utovar], [GATE_IN_FULL/sa_Utovara], [GATE_IN/_GATE_OUT], [L/R], [TARA], [MAX_NOSIVOST_CNT], [VOZILO/PREUZIMANJE], [PLOMBA/IZVOZ], [NAPOMENA], [OPIS], [OTPREMA], [POSLATE SLIKE], [Prevoznik], [BELEŠKA], [VOZAČ])
    VALUES
        (@KONTEJNER, @STATUS, @POZICIJA, @VRSTA, @BRODAR, @NALOGODAVAC_UVOZ, @POSTUPAK_UVOZ, @UVOZNIK, @BL_UVOZ, @PLOMBA_UVOZ, @VOZ_KAMION, @STANJE, @GATE_IN_E_F, @PREUZIMANJE_PUNOG_RAZVOZ, @VRACANJE_PRAZNOG_IZ_RAZVOZA, @KONACNI_GATE_OUT, @BOOKING_IZVOZ, @KLIJENT_IZVOZ, @GATE_OUT_EMPTY_UTOVAR, @GATE_IN_FULL_SA_UTOVARA, @GATE_IN_GATE_OUT, @L_R, @TARA, @MAX_NOSIVOST_CNT, @VOZILO_PREUZIMANJE, @PLOMBA_IZVOZ, @NAPOMENA, @OPIS, @OTPREMA, @POSLATE_SLIKE, @PREVOZNIK, @BELESKA, ISNULL(@VOZAC, 0));

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
    @VOZAC bit = NULL,      -- VOZAČ (štiklir); NULL = starija verzija programa
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
            @StariKonacni datetime, @StariGateOutEmpty datetime, @StariGateInFull datetime,
            @StariGate nvarchar(50);
    IF @ZapisiMaersk = 1
        SELECT @StariGateInEF = [GATE_IN_E/F],
               @StariPreuzimanje = [PREUZIMANJE_PUNOG/Razvoz],
               @StariVracanje = [VRAĆANJE_PRAZNOG/iz_Razvoza],
               @StariKonacni = [Konačni_GATE_OUT],
               @StariGateOutEmpty = [GATE_OUT_EMPTY/Utovar],
               @StariGateInFull = [GATE_IN_FULL/sa_Utovara],
               @StariGate = [GATE_IN/_GATE_OUT]
        FROM dbo.TerminalPriv
        WHERE ID = @ID;

    -- GATE_IN/_GATE_OUT je prešao iz PRIVREMENO u GATE IN E ili GATE IN F: pokret za GATE_IN_E/F se upisuje kao da je
    -- datum tek popunjen (datum je obično unet dok je kontejner bio PRIVREMENO, a tada pravila ne upisuju pokret)
    IF @StariGate = N'PRIVREMENO' AND @GATE_IN_GATE_OUT IN (N'GATE IN E', N'GATE IN F')
        SET @StariGateInEF = NULL;

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
        [BELEŠKA] = @BELESKA,
        [VOZAČ] = ISNULL(@VOZAC, [VOZAČ])   -- bez @VOZAC (starija verzija programa) ostaje isto
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

-- 3. Arhiviranje prenosi i VOZAČ (ostalo isto kao u TerminalPrivArhiv.sql)
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
            deleted.ID, deleted.[KONTEJNER], deleted.[STATUS], deleted.[POZICIJA], deleted.[VRSTA], deleted.[BRODAR], deleted.[NALOGODAVAC/UVOZ], deleted.[POSTUPAK/UVOZ], deleted.[UVOZNIK], deleted.[BL/UVOZ], deleted.[PLOMBA_UVOZ], deleted.[VOZ/kamion], deleted.[STANJE], deleted.[GATE_IN_E/F], deleted.[PREUZIMANJE_PUNOG/Razvoz], deleted.[VRAĆANJE_PRAZNOG/iz_Razvoza], deleted.[Konačni_GATE_OUT], deleted.[BOOKING/IZVOZ], deleted.[KLIJENT/IZVOZ], deleted.[GATE_OUT_EMPTY/Utovar], deleted.[GATE_IN_FULL/sa_Utovara], deleted.[GATE_IN/_GATE_OUT], deleted.[L/R], deleted.[TARA], deleted.[MAX_NOSIVOST_CNT], deleted.[VOZILO/PREUZIMANJE], deleted.[PLOMBA/IZVOZ], deleted.[NAPOMENA], deleted.[OPIS], deleted.[OTPREMA], deleted.[POSLATE SLIKE], deleted.[Prevoznik], deleted.[BELEŠKA], deleted.[VOZAČ]
        INTO dbo.TerminalPrivArhiv
            (ID, [KONTEJNER], [STATUS], [POZICIJA], [VRSTA], [BRODAR], [NALOGODAVAC/UVOZ], [POSTUPAK/UVOZ], [UVOZNIK], [BL/UVOZ], [PLOMBA_UVOZ], [VOZ/kamion], [STANJE], [GATE_IN_E/F], [PREUZIMANJE_PUNOG/Razvoz], [VRAĆANJE_PRAZNOG/iz_Razvoza], [Konačni_GATE_OUT], [BOOKING/IZVOZ], [KLIJENT/IZVOZ], [GATE_OUT_EMPTY/Utovar], [GATE_IN_FULL/sa_Utovara], [GATE_IN/_GATE_OUT], [L/R], [TARA], [MAX_NOSIVOST_CNT], [VOZILO/PREUZIMANJE], [PLOMBA/IZVOZ], [NAPOMENA], [OPIS], [OTPREMA], [POSLATE SLIKE], [Prevoznik], [BELEŠKA], [VOZAČ])
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

-- 4. Log izmena: promena VOZAČ i akcija VRAĆANJE (ostalo isto kao u TerminalPrivremeniLog.sql)
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
            CASE WHEN EXISTS (SELECT d.[BELEŠKA] EXCEPT SELECT i.[BELEŠKA]) THEN N'BELEŠKA: ' + ISNULL(d.[BELEŠKA], N'(prazno)') + N' -> ' + ISNULL(i.[BELEŠKA], N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[VOZAČ] EXCEPT SELECT i.[VOZAČ]) THEN N'VOZAČ: ' + CASE WHEN d.[VOZAČ] = 1 THEN N'DA' ELSE N'NE' END + N' -> ' + CASE WHEN i.[VOZAČ] = 1 THEN N'DA' ELSE N'NE' END + N'; ' ELSE N'' END
        ) AS Opis) x
        WHERE LEN(x.Opis) > 0;
    END
    ELSE IF EXISTS (SELECT 1 FROM inserted)
    BEGIN
        INSERT INTO dbo.TerminalPrivremeniLog (TerminalPrivID, Akcija, Kontejner, Opis)
        -- VratiTerminalPrivIzArhive postavlja session context pa se vraćanje iz arhive razlikuje od novog zapisa
        SELECT ID,
               CASE WHEN CAST(SESSION_CONTEXT(N'VratiTerminalPriv') AS int) = 1 THEN N'VRAĆANJE' ELSE N'INSERT' END,
               KONTEJNER,
               CASE WHEN CAST(SESSION_CONTEXT(N'VratiTerminalPriv') AS int) = 1 THEN N'Vraćeno iz arhive' ELSE N'Novi zapis' END
        FROM inserted;
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

-- 5. Vraćanje zapisa iz arhive (dugme 'Vrati iz arhive' u formi Arhivirani podaci).
-- Zapis se vraća u TerminalPriv sa istim ID-jem (folderi slika i dokumenata i log ostaju povezani sa zapisom), a poništava se
-- konačni izlaz zbog koga je zapis arhiviran:
--   - Konačni_GATE_OUT postaje NULL (inače bi ga sledeće 'Arhiviraj' odmah vratilo u arhivu),
--   - iz izveštaja brodaru (TerminalPrivMaersk ... TerminalPrivCOSCO) briše se pokret koji je taj datum upisao,
--   - iz loga se briše deo izmene 'Konačni_GATE_OUT: (prazno) -> datum'; red loga se briše ako u njemu ništa ne ostane.
-- Pokret koji je upisao Konačni_GATE_OUT: poslednji pokret zapisa u tabeli brodara, ako je izlaz (GATE OUT ...) sa datumom
-- iz te izmene u logu (Konačni_GATE_OUT je posle mogao biti ispravljen); Maersk, MSC, CMA i OOCL porede i sat i minut.
-- Pokret koji je upisao neki drugi datum ostaje.
-- @Proveri = 1: ništa se ne menja; vraća dva skupa za poruku pre vraćanja: (1) kontejner, datumi, da li je nađena izmena u
-- logu i broj zapisa istog kontejnera u terminalu, (2) pokreti koji će biti obrisani i da li su već poslati brodaru.
IF OBJECT_ID('dbo.VratiTerminalPrivIzArhive', 'P') IS NOT NULL DROP PROCEDURE dbo.VratiTerminalPrivIzArhive;
GO
CREATE PROCEDURE dbo.VratiTerminalPrivIzArhive
    @ID int,
    @Proveri bit = 0
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @Kontejner nvarchar(30), @Konacni datetime;
    SELECT @Kontejner = [KONTEJNER], @Konacni = [Konačni_GATE_OUT] FROM dbo.TerminalPrivArhiv WHERE ID = @ID;

    IF @Kontejner IS NULL   -- KONTEJNER nije NULL ni u jednom zapisu arhive, pa NULL znači da zapisa nema
    BEGIN
        RAISERROR(N'Zapis ID %d nije u arhivi.', 16, 1, @ID);
        RETURN;
    END
    IF EXISTS (SELECT 1 FROM dbo.TerminalPriv WHERE ID = @ID)
    BEGIN
        RAISERROR(N'Zapis ID %d već postoji u terminalu.', 16, 1, @ID);
        RETURN;
    END

    -- poslednja izmena u logu koja je upisala Konačni_GATE_OUT; deo izmene je 'Konačni_GATE_OUT: (prazno) -> 2026-11-23 12:51:00; '
    DECLARE @Oznaka nvarchar(40) = N'Konačni_GATE_OUT: (prazno) -> ';
    DECLARE @DuzinaOznake int = DATALENGTH(@Oznaka) / 2;   -- LEN ne broji razmak na kraju
    DECLARE @LogID int, @Opis nvarchar(max), @Pocetak int, @Kraj int, @Tekst nvarchar(30);
    DECLARE @Okidac datetime = @Konacni;                     -- datum koji je upisao pokret

    SELECT TOP 1 @LogID = LogID, @Opis = Opis
    FROM dbo.TerminalPrivremeniLog
    WHERE TerminalPrivID = @ID AND Akcija = N'UPDATE' AND CHARINDEX(@Oznaka, Opis) > 0
    ORDER BY LogID DESC;

    IF @LogID IS NOT NULL
    BEGIN
        SET @Pocetak = CHARINDEX(@Oznaka, @Opis);
        SET @Kraj = CHARINDEX(N';', @Opis, @Pocetak + @DuzinaOznake);
        IF @Kraj = 0
            SET @Kraj = LEN(@Opis) + 1;
        SET @Tekst = SUBSTRING(@Opis, @Pocetak + @DuzinaOznake, @Kraj - @Pocetak - @DuzinaOznake);
        IF @Tekst LIKE N'[12][0-9][0-9][0-9]-[01][0-9]-[0-3][0-9] [0-2][0-9]:[0-5][0-9]:[0-5][0-9]'
            SET @Okidac = CONVERT(datetime, @Tekst, 120);   -- stil 120 ne zavisi od DATEFORMAT
    END

    -- datum i sat u obliku u kome ih upisuju procedure pokreta (insTerminalPriv...Pokreti)
    DECLARE @Dan nvarchar(10) = CONVERT(nvarchar(10), @Okidac, 104);           -- 23.11.2026
    DECLARE @Sat nvarchar(5) = LEFT(CONVERT(nvarchar(8), @Okidac, 108), 5);    -- 12:51

    DECLARE @Pokreti TABLE
    (
        Izvestaj      nvarchar(20)  NOT NULL,
        Kljuc         int           NOT NULL,
        Pokret        nvarchar(100) NULL,
        DatumPokreta  nvarchar(30)  NULL,
        Poslat        bit           NOT NULL,
        DatumIzvoza   datetime      NULL
    );

    -- za svakog brodara samo poslednji pokret zapisa, i to ako je izlaz sa datumom iz izmene
    INSERT INTO @Pokreti
    SELECT N'Maersk', x.MaerskID, x.Move + ISNULL(N' ' + x.[FULL/EMPTY], N''), x.[Date] + N' ' + x.[Time],
           CASE WHEN x.Active = 1 THEN 1 ELSE 0 END, x.DatumIzvoza
    FROM (SELECT TOP 1 * FROM dbo.TerminalPrivMaersk WHERE ID = @ID ORDER BY MaerskID DESC) x
    WHERE x.[Date] = @Dan + N'.' AND x.[Time] = @Sat AND x.Move = N'GATE OUT';

    INSERT INTO @Pokreti
    SELECT N'MSC', x.MSKID, x.[Container Status] + ISNULL(N' ' + x.[Full/Empty], N''), x.[Move Date/Time],
           CASE WHEN x.Active = 1 THEN 1 ELSE 0 END, x.DatumIzvoza
    FROM (SELECT TOP 1 * FROM dbo.TerminalPrivMSC WHERE ID = @ID ORDER BY MSKID DESC) x
    WHERE x.[Move Date/Time] = @Dan + N' ' + @Sat AND x.[Container Status] IN (N'ELR', N'ICO', N'MPO', N'MSH');

    INSERT INTO @Pokreti
    SELECT N'CMA', x.CMAID, x.[MOVE TYPE] + ISNULL(N' ' + x.[CONTAINER STATUS], N''), x.[MOVE DATE/TIME],
           CASE WHEN x.Active = 1 THEN 1 ELSE 0 END, x.DatumIzvoza
    FROM (SELECT TOP 1 * FROM dbo.TerminalPrivCMA WHERE ID = @ID ORDER BY CMAID DESC) x
    WHERE x.[MOVE DATE/TIME] = @Dan + N'. ' + @Sat AND x.[MOVE TYPE] = N'GATE OUT';

    -- OOCL: isti pokreti idu u izveštaj OOCL EDI (Active) i OOCL MAIL (ActiveMail)
    INSERT INTO @Pokreti
    SELECT N'OOCL', x.OOCLID, x.[MOVE TYPE] + ISNULL(N' ' + x.[CONTAINER STATUS], N''), x.[MOVE DATE/TIME],
           CASE WHEN x.Active = 1 OR x.ActiveMail = 1 THEN 1 ELSE 0 END, COALESCE(x.DatumIzvoza, x.DatumIzvozaMail)
    FROM (SELECT TOP 1 * FROM dbo.TerminalPrivOOCL WHERE ID = @ID ORDER BY OOCLID DESC) x
    WHERE x.[MOVE DATE/TIME] = @Dan + N'. ' + @Sat AND x.[MOVE TYPE] = N'GATE OUT';

    INSERT INTO @Pokreti
    SELECT N'HMM', x.HMMID, x.[1st MOVE] + ISNULL(N' ' + x.[Full/Empty], N''), x.[DATE OF THE MOVE],
           CASE WHEN x.Active = 1 THEN 1 ELSE 0 END, x.DatumIzvoza
    FROM (SELECT TOP 1 * FROM dbo.TerminalPrivHMM WHERE ID = @ID ORDER BY HMMID DESC) x
    WHERE x.[DATE OF THE MOVE] = @Dan + N'.' AND x.[1st MOVE] = N'GATE OUT';

    INSERT INTO @Pokreti
    SELECT N'HAPAG', x.HAPAGID, x.[1st MOVE] + ISNULL(N' ' + x.[Full/Empty], N''), x.[DATE OF THE MOVE],
           CASE WHEN x.Active = 1 THEN 1 ELSE 0 END, x.DatumIzvoza
    FROM (SELECT TOP 1 * FROM dbo.TerminalPrivHAPAG WHERE ID = @ID ORDER BY HAPAGID DESC) x
    WHERE x.[DATE OF THE MOVE] = @Dan + N'.' AND x.[1st MOVE] = N'GATE OUT';

    INSERT INTO @Pokreti
    SELECT N'FLEXBOX', x.FLEXBOXID, x.[1st MOVE] + ISNULL(N' ' + x.[Full/Empty], N''), x.[DATE OF THE MOVE],
           CASE WHEN x.Active = 1 THEN 1 ELSE 0 END, x.DatumIzvoza
    FROM (SELECT TOP 1 * FROM dbo.TerminalPrivFLEXBOX WHERE ID = @ID ORDER BY FLEXBOXID DESC) x
    WHERE x.[DATE OF THE MOVE] = @Dan + N'.' AND x.[1st MOVE] = N'GATE OUT';

    INSERT INTO @Pokreti
    SELECT N'COSCO', x.COSCOID, x.[1st MOVE] + ISNULL(N' ' + x.[Full/Empty], N''), x.[DATE OF THE MOVE],
           CASE WHEN x.Active = 1 THEN 1 ELSE 0 END, x.DatumIzvoza
    FROM (SELECT TOP 1 * FROM dbo.TerminalPrivCOSCO WHERE ID = @ID ORDER BY COSCOID DESC) x
    WHERE x.[DATE OF THE MOVE] = @Dan + N'.' AND x.[1st MOVE] IN (N'GATE OUT', N'LOADED ON TRAIN FROM DEPOT');

    IF @Proveri = 1
    BEGIN
        SELECT @Kontejner AS Kontejner,
               @Konacni AS KonacniGateOut,
               CASE WHEN @LogID IS NULL THEN 0 ELSE 1 END AS NadjenLog,
               (SELECT COUNT(*) FROM dbo.TerminalPriv WHERE [KONTEJNER] = @Kontejner) AS UTerminalu;
        SELECT Izvestaj, Pokret, DatumPokreta, Poslat, DatumIzvoza FROM @Pokreti ORDER BY Izvestaj;
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;

        DELETE t FROM dbo.TerminalPrivMaersk t INNER JOIN @Pokreti p ON p.Izvestaj = N'Maersk' AND p.Kljuc = t.MaerskID;
        DELETE t FROM dbo.TerminalPrivMSC t INNER JOIN @Pokreti p ON p.Izvestaj = N'MSC' AND p.Kljuc = t.MSKID;
        DELETE t FROM dbo.TerminalPrivCMA t INNER JOIN @Pokreti p ON p.Izvestaj = N'CMA' AND p.Kljuc = t.CMAID;
        DELETE t FROM dbo.TerminalPrivOOCL t INNER JOIN @Pokreti p ON p.Izvestaj = N'OOCL' AND p.Kljuc = t.OOCLID;
        DELETE t FROM dbo.TerminalPrivHMM t INNER JOIN @Pokreti p ON p.Izvestaj = N'HMM' AND p.Kljuc = t.HMMID;
        DELETE t FROM dbo.TerminalPrivHAPAG t INNER JOIN @Pokreti p ON p.Izvestaj = N'HAPAG' AND p.Kljuc = t.HAPAGID;
        DELETE t FROM dbo.TerminalPrivFLEXBOX t INNER JOIN @Pokreti p ON p.Izvestaj = N'FLEXBOX' AND p.Kljuc = t.FLEXBOXID;
        DELETE t FROM dbo.TerminalPrivCOSCO t INNER JOIN @Pokreti p ON p.Izvestaj = N'COSCO' AND p.Kljuc = t.COSCOID;

        -- RB (redni broj u sledećem fajlu) neizvezenih CMA i OOCL pokreta ostaje 1, 2, 3... bez preskakanja
        IF EXISTS (SELECT 1 FROM @Pokreti WHERE Izvestaj = N'CMA')
        BEGIN
            WITH x AS (SELECT [RB], ROW_NUMBER() OVER (ORDER BY CMAID) AS Novi FROM dbo.TerminalPrivCMA WHERE Active <> 1)
            UPDATE x SET [RB] = Novi;
        END
        IF EXISTS (SELECT 1 FROM @Pokreti WHERE Izvestaj = N'OOCL')
        BEGIN
            WITH x AS (SELECT [RB], ROW_NUMBER() OVER (ORDER BY OOCLID) AS Novi FROM dbo.TerminalPrivOOCL WHERE Active <> 1)
            UPDATE x SET [RB] = Novi;
        END

        -- iz izmene u logu briše se samo deo za Konačni_GATE_OUT (sa '; ' na kraju)
        IF @LogID IS NOT NULL
        BEGIN
            SET @Opis = STUFF(@Opis, @Pocetak, @Kraj + 2 - @Pocetak, N'');
            IF LEN(@Opis) = 0
                DELETE FROM dbo.TerminalPrivremeniLog WHERE LogID = @LogID;
            ELSE
                UPDATE dbo.TerminalPrivremeniLog SET Opis = @Opis WHERE LogID = @LogID;
        END

        -- trigger na TerminalPriv na osnovu ovoga u log upisuje VRAĆANJE umesto INSERT
        EXEC sp_set_session_context @key = N'VratiTerminalPriv', @value = 1;

        SET IDENTITY_INSERT dbo.TerminalPriv ON;

        INSERT INTO dbo.TerminalPriv
            (ID, [KONTEJNER], [STATUS], [POZICIJA], [VRSTA], [BRODAR], [NALOGODAVAC/UVOZ], [POSTUPAK/UVOZ], [UVOZNIK], [BL/UVOZ], [PLOMBA_UVOZ], [VOZ/kamion], [STANJE], [GATE_IN_E/F], [PREUZIMANJE_PUNOG/Razvoz], [VRAĆANJE_PRAZNOG/iz_Razvoza], [Konačni_GATE_OUT], [BOOKING/IZVOZ], [KLIJENT/IZVOZ], [GATE_OUT_EMPTY/Utovar], [GATE_IN_FULL/sa_Utovara], [GATE_IN/_GATE_OUT], [L/R], [TARA], [MAX_NOSIVOST_CNT], [VOZILO/PREUZIMANJE], [PLOMBA/IZVOZ], [NAPOMENA], [OPIS], [OTPREMA], [POSLATE SLIKE], [Prevoznik], [BELEŠKA], [VOZAČ])
        SELECT
             ID, [KONTEJNER], [STATUS], [POZICIJA], [VRSTA], [BRODAR], [NALOGODAVAC/UVOZ], [POSTUPAK/UVOZ], [UVOZNIK], [BL/UVOZ], [PLOMBA_UVOZ], [VOZ/kamion], [STANJE], [GATE_IN_E/F], [PREUZIMANJE_PUNOG/Razvoz], [VRAĆANJE_PRAZNOG/iz_Razvoza], NULL,               [BOOKING/IZVOZ], [KLIJENT/IZVOZ], [GATE_OUT_EMPTY/Utovar], [GATE_IN_FULL/sa_Utovara], [GATE_IN/_GATE_OUT], [L/R], [TARA], [MAX_NOSIVOST_CNT], [VOZILO/PREUZIMANJE], [PLOMBA/IZVOZ], [NAPOMENA], [OPIS], [OTPREMA], [POSLATE SLIKE], [Prevoznik], [BELEŠKA], [VOZAČ]
        FROM dbo.TerminalPrivArhiv
        WHERE ID = @ID;

        SET IDENTITY_INSERT dbo.TerminalPriv OFF;
        EXEC sp_set_session_context @key = N'VratiTerminalPriv', @value = 0;

        DELETE FROM dbo.TerminalPrivArhiv WHERE ID = @ID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        SET IDENTITY_INSERT dbo.TerminalPriv OFF;
        EXEC sp_set_session_context @key = N'VratiTerminalPriv', @value = 0;
        THROW;
    END CATCH
END
GO

SET NOEXEC OFF;
GO
