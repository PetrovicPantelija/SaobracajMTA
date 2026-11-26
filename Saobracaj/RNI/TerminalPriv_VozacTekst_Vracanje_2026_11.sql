-- PRODUKCIJA (novembar 2026): Terminal privremeni - VOZAČ kao ime vozača (tekst umesto štiklira) i vraćanje iz arhive koje
-- poništava poslednju izmenu sa upisom u izveštaj brodaru, za bilo koji datum (ne samo Konačni_GATE_OUT).
-- Zamenjuje TerminalPriv_Vozac_Arhiva_2026_11.sql: ako ona nije puštena, dovoljno je pustiti ovu.
-- Sadrži:
--   1. kolonu [VOZAČ] nvarchar(50) NULL (ime vozača) u TerminalPriv i TerminalPrivArhiv; prikazuje se u žutom okviru forme,
--      ne u gridu. Ako je kolona već napravljena kao štiklir (bit), skripta prvo prikazuje zapise sa štiklom (tu treba upisati
--      ime vozača), pa kolonu pravi iznova kao tekst; štiklovi se ne prenose,
--   2. procedure insTerminalPriv i updTerminalPriv sa parametrom @VOZAC nvarchar(50) (ostalo isto kao u
--      TerminalPriv_Vozac_Arhiva_2026_11.sql),
--   3. proceduru InsertTerminalPrivArhiv koja prenosi i VOZAČ,
--   4. log trigger trg_TerminalPriv_Log: promena VOZAČ (tekst) i akcija VRAĆANJE (vraćanje iz arhive),
--   5. proceduru VratiTerminalPrivIzArhive (dugme 'Vrati iz arhive' u formi Arhivirani podaci), opis je kod procedure,
--   6. spisak zapisa kod kojih TARA nema tačno 4, a MAX_NOSIVOST_CNT tačno 5 karaktera (program ih ne čuva dok se ne isprave).
-- Pustiti neposredno PRE instalacije nove verzije programa, i odmah instalirati novu verziju na sve računare: verzija sa
-- štiklirom VOZAČ ne može da prikaže zapis u kome je upisano ime vozača.
-- Skripta se može pustiti više puta. Pre pokretanja izabrati produkcionu bazu.
-- Vraćanje iz arhive koristi SET IDENTITY_INSERT dbo.TerminalPriv: korisnik iz konekcije programa mora imati
-- pravo ALTER nad tabelom TerminalPriv (vlasnik baze ga ima).

-- procedure se prave sa ovim podešavanjima (VratiTerminalPrivIzArhive koristi XML, za koji je QUOTED_IDENTIFIER obavezan),
-- i kad se skripta pusti iz sqlcmd, gde je podrazumevano isključen
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

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

-- 1. Kolona VOZAČ: ime vozača, nvarchar(50) NULL
-- 1a. Kolona je već štiklir (bit): zapisi sa štiklom, da se posle instalacije upiše ime vozača (tabela je prazna ako ih nema)
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'dbo.TerminalPriv') AND name = N'VOZAČ' AND TYPE_NAME(system_type_id) = N'bit')
    EXEC (N'SELECT N''TerminalPriv'' AS Tabela, ID, [KONTEJNER] AS Kontejner FROM dbo.TerminalPriv WHERE [VOZAČ] = 1 ORDER BY ID;');
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'dbo.TerminalPrivArhiv') AND name = N'VOZAČ' AND TYPE_NAME(system_type_id) = N'bit')
    EXEC (N'SELECT N''TerminalPrivArhiv'' AS Tabela, ID, [KONTEJNER] AS Kontejner FROM dbo.TerminalPrivArhiv WHERE [VOZAČ] = 1 ORDER BY ID;');
GO

-- 1b. Štiklir se uklanja (podrazumevana vrednost, pa kolona) i kolona se dodaje kao tekst. Sve u jednoj transakciji po tabeli,
-- pa trigger i procedure ni u jednom trenutku ne ostaju bez kolone.
SET NOCOUNT ON;
SET XACT_ABORT ON;
DECLARE @Tabele TABLE (RB int, Naziv sysname);
INSERT INTO @Tabele VALUES (1, N'TerminalPriv'), (2, N'TerminalPrivArhiv');
DECLARE @RB int = 0, @Tabela sysname, @Sql nvarchar(max);

WHILE 1 = 1
BEGIN
    SELECT TOP 1 @RB = RB, @Tabela = Naziv FROM @Tabele WHERE RB > @RB ORDER BY RB;
    IF @@ROWCOUNT = 0 BREAK;

    BEGIN TRANSACTION;

    IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'dbo.' + @Tabela) AND name = N'VOZAČ' AND TYPE_NAME(system_type_id) = N'bit')
    BEGIN
        SET @Sql = N'';
        SELECT @Sql = @Sql + N'ALTER TABLE dbo.' + QUOTENAME(@Tabela) + N' DROP CONSTRAINT ' + QUOTENAME(dc.name) + N';'
        FROM sys.default_constraints dc
        JOIN sys.columns c ON c.object_id = dc.parent_object_id AND c.column_id = dc.parent_column_id
        WHERE dc.parent_object_id = OBJECT_ID(N'dbo.' + @Tabela) AND c.name = N'VOZAČ';
        SET @Sql = @Sql + N'ALTER TABLE dbo.' + QUOTENAME(@Tabela) + N' DROP COLUMN [VOZAČ];';
        EXEC sp_executesql @Sql;
        PRINT N'dbo.' + @Tabela + N': uklonjen štiklir VOZAČ (bit).';
    END

    IF COL_LENGTH(N'dbo.' + @Tabela, N'VOZAČ') IS NULL
    BEGIN
        SET @Sql = N'ALTER TABLE dbo.' + QUOTENAME(@Tabela) + N' ADD [VOZAČ] nvarchar(50) NULL;';
        EXEC sp_executesql @Sql;
        PRINT N'dbo.' + @Tabela + N': dodata kolona VOZAČ nvarchar(50).';
    END

    COMMIT TRANSACTION;
END
GO

-- 2. insTerminalPriv i updTerminalPriv: isto kao u TerminalPriv_Vozac_Arhiva_2026_11.sql, uz @VOZAC nvarchar(50)

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
    @VOZAC nvarchar(50) = NULL,   -- VOZAČ (ime vozača); prazno = nije uneto
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
        (@KONTEJNER, @STATUS, @POZICIJA, @VRSTA, @BRODAR, @NALOGODAVAC_UVOZ, @POSTUPAK_UVOZ, @UVOZNIK, @BL_UVOZ, @PLOMBA_UVOZ, @VOZ_KAMION, @STANJE, @GATE_IN_E_F, @PREUZIMANJE_PUNOG_RAZVOZ, @VRACANJE_PRAZNOG_IZ_RAZVOZA, @KONACNI_GATE_OUT, @BOOKING_IZVOZ, @KLIJENT_IZVOZ, @GATE_OUT_EMPTY_UTOVAR, @GATE_IN_FULL_SA_UTOVARA, @GATE_IN_GATE_OUT, @L_R, @TARA, @MAX_NOSIVOST_CNT, @VOZILO_PREUZIMANJE, @PLOMBA_IZVOZ, @NAPOMENA, @OPIS, @OTPREMA, @POSLATE_SLIKE, @PREVOZNIK, @BELESKA, NULLIF(LTRIM(RTRIM(@VOZAC)), N''));

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
    @VOZAC nvarchar(50) = NULL,   -- VOZAČ (ime vozača): program šalje prazan tekst kad je polje prazno; NULL = starija verzija programa
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
        [VOZAČ] = CASE WHEN @VOZAC IS NULL THEN [VOZAČ]                -- bez @VOZAC (starija verzija programa) ostaje isto
                       ELSE NULLIF(LTRIM(RTRIM(@VOZAC)), N'') END     -- prazan tekst briše ime vozača
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

-- 4. Log izmena: promena VOZAČ (ime vozača) i akcija VRAĆANJE (ostalo isto kao u TerminalPrivremeniLog.sql)
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
            CASE WHEN EXISTS (SELECT d.[VOZAČ] EXCEPT SELECT i.[VOZAČ]) THEN N'VOZAČ: ' + ISNULL(d.[VOZAČ], N'(prazno)') + N' -> ' + ISNULL(i.[VOZAČ], N'(prazno)') + N'; ' ELSE N'' END
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
-- Zapis se vraća u TerminalPriv sa istim ID-jem (folderi slika i dokumenata i log ostaju povezani sa zapisom). Poništava se
-- poslednja izmena koja je upisala pokret u izveštaj brodaru (TerminalPrivMaersk ... TerminalPrivCOSCO), za bilo koji datum:
--   - iz izveštaja brodaru brišu se pokreti koje je ta izmena upisala,
--   - datumi koji su upisali te pokrete postaju NULL,
--   - iz te izmene u logu briše se deo za te datume ('Polje: (prazno) -> datum; '); red loga se briše ako u njemu ništa ne ostane.
-- Konačni_GATE_OUT uvek postaje NULL (inače bi ga sledeće 'Arhiviraj' odmah vratilo u arhivu) i iz loga se briše njegov upis.
-- Kako se nalazi izmena: pokret se upisuje samo kad datum pređe iz praznog u popunjen i nosi taj datum (Maersk, MSC, CMA i OOCL
-- dan i sat, ostali samo dan) i smer (ulaz ili izlaz, po polju). Za poslednji pokret svakog izveštaja traži se najnovija izmena
-- u logu sa takvim upisom datuma, a od njih se uzima najnovija. Pokret koji nije upisala izmena iz loga (novi zapis, prelaz
-- PRIVREMENO -> GATE IN) poredi se sa datumima arhiviranog zapisa. Od kraja izveštaja brišu se uzastopni pokreti te izmene,
-- najviše jedan po datumu.
-- @Proveri = 1: ništa se ne menja; vraća tri skupa za poruku pre vraćanja: (1) kontejner, Konačni_GATE_OUT, da li je u logu nađen
-- upis Konačni_GATE_OUT, broj zapisa istog kontejnera u terminalu, broj pokreta zapisa u izveštajima i datum izmene koja se
-- poništava; (2) pokreti koji se brišu, da li su već poslati brodaru i koji ih je datum upisao; (3) datumi koji postaju prazni.
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

    -- datumi zapisa: naziv kolone (kao u logu), smer pokreta koji upisuju (U = ulaz, I = izlaz) i vrednost u arhivi
    DECLARE @Datumi TABLE
    (
        Polje     nvarchar(40) NOT NULL PRIMARY KEY,
        Redosled  int          NOT NULL,
        Smer      char(1)      NOT NULL,
        Vrednost  datetime     NULL
    );
    INSERT INTO @Datumi (Polje, Redosled, Smer, Vrednost)
    SELECT v.Polje, v.Redosled, v.Smer, v.Vrednost
    FROM dbo.TerminalPrivArhiv a
    CROSS APPLY (VALUES
        (N'GATE_IN_E/F',                 1, 'U', a.[GATE_IN_E/F]),
        (N'PREUZIMANJE_PUNOG/Razvoz',    2, 'I', a.[PREUZIMANJE_PUNOG/Razvoz]),
        (N'VRAĆANJE_PRAZNOG/iz_Razvoza', 3, 'U', a.[VRAĆANJE_PRAZNOG/iz_Razvoza]),
        (N'Konačni_GATE_OUT',            4, 'I', a.[Konačni_GATE_OUT]),
        (N'GATE_OUT_EMPTY/Utovar',       5, 'I', a.[GATE_OUT_EMPTY/Utovar]),
        (N'GATE_IN_FULL/sa_Utovara',     6, 'U', a.[GATE_IN_FULL/sa_Utovara])
    ) v (Polje, Redosled, Smer, Vrednost)
    WHERE a.ID = @ID;

    -- upisi datuma iz praznog u popunjen: iz izmena u logu ('Konačni_GATE_OUT: (prazno) -> 2026-11-23 12:51:00; ') i, sa
    -- LogID = 0, datumi arhiviranog zapisa (za pokret koji nije upisala nijedna izmena iz loga)
    DECLARE @Upisi TABLE
    (
        LogID     int           NOT NULL,
        Polje     nvarchar(40)  NOT NULL,
        Smer      char(1)       NOT NULL,
        Vrednost  datetime      NOT NULL,
        DanSat    nvarchar(16)  NOT NULL,   -- 23.11.2026 12:51
        Dan       nvarchar(10)  NOT NULL,   -- 23.11.2026
        Deo       nvarchar(100) NULL        -- deo izmene u logu, sa '; ' na kraju
    );
    INSERT INTO @Upisi (LogID, Polje, Smer, Vrednost, DanSat, Dan, Deo)
    SELECT x.LogID, x.Polje, x.Smer, x.Vrednost,
           CONVERT(nvarchar(10), x.Vrednost, 104) + N' ' + LEFT(CONVERT(nvarchar(8), x.Vrednost, 108), 5),
           CONVERT(nvarchar(10), x.Vrednost, 104),
           x.Deo
    FROM (
        SELECT l.LogID, d.Polje, d.Smer,
               -- stil 120 ne zavisi od DATEFORMAT; CASE da se pretvara samo tekst u obliku datuma (TRY_CONVERT ne postoji
               -- u bazi sa nivoom kompatibilnosti 100)
               CASE WHEN t.Tekst LIKE N'[12][0-9][0-9][0-9]-[01][0-9]-[0-3][0-9] [0-2][0-9]:[0-5][0-9]:[0-5][0-9]'
                    THEN CONVERT(datetime, t.Tekst, 120) END AS Vrednost,
               d.Polje + N': (prazno) -> ' + t.Tekst + N'; ' AS Deo
        FROM dbo.TerminalPrivremeniLog l
        CROSS JOIN @Datumi d
        CROSS APPLY (SELECT CHARINDEX(d.Polje + N': (prazno) -> ', l.Opis) AS Pocetak) p
        CROSS APPLY (SELECT SUBSTRING(l.Opis, p.Pocetak + LEN(d.Polje) + 14, 19) AS Tekst) t   -- 14 = dužina ': (prazno) -> '
        WHERE l.TerminalPrivID = @ID AND l.Akcija = N'UPDATE' AND p.Pocetak > 0
          AND t.Tekst LIKE N'[12][0-9][0-9][0-9]-[01][0-9]-[0-3][0-9] [0-2][0-9]:[0-5][0-9]:[0-5][0-9]'
        UNION ALL
        SELECT 0, d.Polje, d.Smer, d.Vrednost, NULL
        FROM @Datumi d
    ) x
    WHERE x.Vrednost IS NOT NULL;

    -- pokreti zapisa u izveštajima brodaru; Vreme je u obliku DanSat (izveštaj sa satom) ili Dan (izveštaj bez sata)
    DECLARE @Pokreti TABLE
    (
        Izvestaj      nvarchar(20)  NOT NULL,
        Kljuc         int           NOT NULL,
        Smer          char(1)       NOT NULL,
        Vreme         nvarchar(30)  NULL,
        SaSatom       bit           NOT NULL,
        Pokret        nvarchar(100) NULL,
        DatumPokreta  nvarchar(30)  NULL,
        Poslat        bit           NOT NULL,
        DatumIzvoza   datetime      NULL,
        Polje         nvarchar(40)  NULL,   -- popunjeno = pokret se briše; datum koji ga je upisao
        PRIMARY KEY (Izvestaj, Kljuc)
    );

    -- Maersk: Date 23.11.2026., Time 12:51
    INSERT INTO @Pokreti (Izvestaj, Kljuc, Smer, Vreme, SaSatom, Pokret, DatumPokreta, Poslat, DatumIzvoza)
    SELECT N'Maersk', MaerskID, CASE WHEN Move = N'GATE OUT' THEN 'I' ELSE 'U' END,
           LEFT([Date], 10) + N' ' + [Time], 1,
           Move + ISNULL(N' ' + [FULL/EMPTY], N''), [Date] + N' ' + [Time],
           CASE WHEN Active = 1 THEN 1 ELSE 0 END, DatumIzvoza
    FROM dbo.TerminalPrivMaersk WHERE ID = @ID;

    -- MSC: 23.11.2026 12:51; izlaz su ELR, ICO, MPO i MSH
    INSERT INTO @Pokreti (Izvestaj, Kljuc, Smer, Vreme, SaSatom, Pokret, DatumPokreta, Poslat, DatumIzvoza)
    SELECT N'MSC', MSKID, CASE WHEN [Container Status] IN (N'ELR', N'ICO', N'MPO', N'MSH') THEN 'I' ELSE 'U' END,
           [Move Date/Time], 1,
           [Container Status] + ISNULL(N' ' + [Full/Empty], N''), [Move Date/Time],
           CASE WHEN Active = 1 THEN 1 ELSE 0 END, DatumIzvoza
    FROM dbo.TerminalPrivMSC WHERE ID = @ID;

    -- CMA: 23.11.2026. 12:51
    INSERT INTO @Pokreti (Izvestaj, Kljuc, Smer, Vreme, SaSatom, Pokret, DatumPokreta, Poslat, DatumIzvoza)
    SELECT N'CMA', CMAID, CASE WHEN [MOVE TYPE] = N'GATE OUT' THEN 'I' ELSE 'U' END,
           REPLACE([MOVE DATE/TIME], N'. ', N' '), 1,
           [MOVE TYPE] + ISNULL(N' ' + [CONTAINER STATUS], N''), [MOVE DATE/TIME],
           CASE WHEN Active = 1 THEN 1 ELSE 0 END, DatumIzvoza
    FROM dbo.TerminalPrivCMA WHERE ID = @ID;

    -- OOCL: kao CMA; isti pokreti idu u izveštaj OOCL EDI (Active) i OOCL MAIL (ActiveMail)
    INSERT INTO @Pokreti (Izvestaj, Kljuc, Smer, Vreme, SaSatom, Pokret, DatumPokreta, Poslat, DatumIzvoza)
    SELECT N'OOCL', OOCLID, CASE WHEN [MOVE TYPE] = N'GATE OUT' THEN 'I' ELSE 'U' END,
           REPLACE([MOVE DATE/TIME], N'. ', N' '), 1,
           [MOVE TYPE] + ISNULL(N' ' + [CONTAINER STATUS], N''), [MOVE DATE/TIME],
           CASE WHEN Active = 1 OR ActiveMail = 1 THEN 1 ELSE 0 END, COALESCE(DatumIzvoza, DatumIzvozaMail)
    FROM dbo.TerminalPrivOOCL WHERE ID = @ID;

    -- HMM, HAPAG, FLEXBOX i COSCO: samo dan, 23.11.2026.
    INSERT INTO @Pokreti (Izvestaj, Kljuc, Smer, Vreme, SaSatom, Pokret, DatumPokreta, Poslat, DatumIzvoza)
    SELECT N'HMM', HMMID, CASE WHEN [1st MOVE] = N'GATE OUT' THEN 'I' ELSE 'U' END,
           LEFT([DATE OF THE MOVE], 10), 0,
           [1st MOVE] + ISNULL(N' ' + [Full/Empty], N''), [DATE OF THE MOVE],
           CASE WHEN Active = 1 THEN 1 ELSE 0 END, DatumIzvoza
    FROM dbo.TerminalPrivHMM WHERE ID = @ID;

    INSERT INTO @Pokreti (Izvestaj, Kljuc, Smer, Vreme, SaSatom, Pokret, DatumPokreta, Poslat, DatumIzvoza)
    SELECT N'HAPAG', HAPAGID, CASE WHEN [1st MOVE] = N'GATE OUT' THEN 'I' ELSE 'U' END,
           LEFT([DATE OF THE MOVE], 10), 0,
           [1st MOVE] + ISNULL(N' ' + [Full/Empty], N''), [DATE OF THE MOVE],
           CASE WHEN Active = 1 THEN 1 ELSE 0 END, DatumIzvoza
    FROM dbo.TerminalPrivHAPAG WHERE ID = @ID;

    INSERT INTO @Pokreti (Izvestaj, Kljuc, Smer, Vreme, SaSatom, Pokret, DatumPokreta, Poslat, DatumIzvoza)
    SELECT N'FLEXBOX', FLEXBOXID, CASE WHEN [1st MOVE] = N'GATE OUT' THEN 'I' ELSE 'U' END,
           LEFT([DATE OF THE MOVE], 10), 0,
           [1st MOVE] + ISNULL(N' ' + [Full/Empty], N''), [DATE OF THE MOVE],
           CASE WHEN Active = 1 THEN 1 ELSE 0 END, DatumIzvoza
    FROM dbo.TerminalPrivFLEXBOX WHERE ID = @ID;

    INSERT INTO @Pokreti (Izvestaj, Kljuc, Smer, Vreme, SaSatom, Pokret, DatumPokreta, Poslat, DatumIzvoza)
    SELECT N'COSCO', COSCOID, CASE WHEN [1st MOVE] IN (N'GATE OUT', N'LOADED ON TRAIN FROM DEPOT') THEN 'I' ELSE 'U' END,
           LEFT([DATE OF THE MOVE], 10), 0,
           [1st MOVE] + ISNULL(N' ' + [Full/Empty], N''), [DATE OF THE MOVE],
           CASE WHEN Active = 1 THEN 1 ELSE 0 END, DatumIzvoza
    FROM dbo.TerminalPrivCOSCO WHERE ID = @ID;

    -- izmena koja je poslednja upisala u izveštaj brodaru (LogID 0 = pokret nije upisala izmena iz loga)
    DECLARE @Izmena int;
    SELECT TOP 1 @Izmena = u.LogID
    FROM @Pokreti p
    INNER JOIN @Upisi u ON u.Smer = p.Smer AND p.Vreme = CASE WHEN p.SaSatom = 1 THEN u.DanSat ELSE u.Dan END
    WHERE p.Kljuc = (SELECT MAX(q.Kljuc) FROM @Pokreti q WHERE q.Izvestaj = p.Izvestaj)
    ORDER BY u.LogID DESC;

    -- pokreti te izmene: od kraja svakog izveštaja, dok pokret odgovara nekom upisu datuma te izmene (svaki upis jednom)
    IF @Izmena IS NOT NULL
    BEGIN
        DECLARE @Izvestaj nvarchar(20), @Kljuc int, @Polje nvarchar(40);
        DECLARE izvestaji CURSOR LOCAL FAST_FORWARD FOR SELECT DISTINCT Izvestaj FROM @Pokreti;
        OPEN izvestaji;
        FETCH NEXT FROM izvestaji INTO @Izvestaj;
        WHILE @@FETCH_STATUS = 0
        BEGIN
            SELECT @Kljuc = MAX(Kljuc) FROM @Pokreti WHERE Izvestaj = @Izvestaj;
            WHILE @Kljuc IS NOT NULL
            BEGIN
                SET @Polje = NULL;
                SELECT TOP 1 @Polje = u.Polje
                FROM @Pokreti p
                INNER JOIN @Upisi u ON u.LogID = @Izmena AND u.Smer = p.Smer
                                   AND p.Vreme = CASE WHEN p.SaSatom = 1 THEN u.DanSat ELSE u.Dan END
                WHERE p.Izvestaj = @Izvestaj AND p.Kljuc = @Kljuc
                  AND NOT EXISTS (SELECT 1 FROM @Pokreti z WHERE z.Izvestaj = @Izvestaj AND z.Polje = u.Polje)
                ORDER BY u.Vrednost DESC;
                IF @Polje IS NULL
                    BREAK;

                UPDATE @Pokreti SET Polje = @Polje WHERE Izvestaj = @Izvestaj AND Kljuc = @Kljuc;
                SELECT @Kljuc = MAX(Kljuc) FROM @Pokreti WHERE Izvestaj = @Izvestaj AND Kljuc < @Kljuc;
            END
            FETCH NEXT FROM izvestaji INTO @Izvestaj;
        END
        CLOSE izvestaji;
        DEALLOCATE izvestaji;
    END

    -- datumi koji postaju prazni: oni koji su upisali pokrete koji se brišu, i uvek Konačni_GATE_OUT. Iz loga se briše njihov
    -- upis iz te izmene; za Konačni_GATE_OUT koji nije deo te izmene, njegov poslednji upis.
    DECLARE @Ponisti TABLE
    (
        Polje     nvarchar(40)  NOT NULL PRIMARY KEY,
        Redosled  int           NOT NULL,
        Vrednost  datetime      NULL,
        LogID     int           NULL,
        Deo       nvarchar(100) NULL
    );
    INSERT INTO @Ponisti (Polje, Redosled, Vrednost)
    SELECT d.Polje, d.Redosled, d.Vrednost
    FROM @Datumi d
    WHERE d.Polje = N'Konačni_GATE_OUT' OR EXISTS (SELECT 1 FROM @Pokreti p WHERE p.Polje = d.Polje);

    UPDATE z SET LogID = x.LogID, Deo = x.Deo
    FROM @Ponisti z
    CROSS APPLY (SELECT TOP 1 u.LogID, u.Deo
                 FROM @Upisi u
                 WHERE u.Polje = z.Polje AND u.LogID > 0
                   AND (u.LogID = @Izmena OR NOT EXISTS (SELECT 1 FROM @Pokreti p WHERE p.Polje = z.Polje))
                 ORDER BY u.LogID DESC) x;

    IF @Proveri = 1
    BEGIN
        SELECT @Kontejner AS Kontejner,
               @Konacni AS KonacniGateOut,
               CASE WHEN EXISTS (SELECT 1 FROM @Ponisti WHERE Polje = N'Konačni_GATE_OUT' AND LogID IS NOT NULL) THEN 1 ELSE 0 END AS NadjenLog,
               (SELECT COUNT(*) FROM dbo.TerminalPriv WHERE [KONTEJNER] = @Kontejner) AS UTerminalu,
               (SELECT COUNT(*) FROM @Pokreti) AS BrojPokreta,
               (SELECT Datum FROM dbo.TerminalPrivremeniLog WHERE LogID = @Izmena) AS DatumIzmene;
        SELECT Izvestaj, Pokret, DatumPokreta, Poslat, DatumIzvoza, Polje
        FROM @Pokreti WHERE Polje IS NOT NULL ORDER BY Izvestaj, Kljuc;
        SELECT Polje, Vrednost, CASE WHEN LogID IS NULL THEN 0 ELSE 1 END AS IzLoga
        FROM @Ponisti ORDER BY Redosled;
        RETURN;
    END

    -- opis za red loga VRAĆANJE
    DECLARE @OpisVracanja nvarchar(max) = N'Vraćeno iz arhive. Poništeno: '
        + ISNULL(STUFF((SELECT N', ' + z.Polje + ISNULL(N' ' + CONVERT(nvarchar(20), z.Vrednost, 120), N'')
                        FROM @Ponisti z ORDER BY z.Redosled
                        FOR XML PATH(''), TYPE).value('.', 'nvarchar(max)'), 1, 2, N''), N'-') + N'.'
        + ISNULL(N' Obrisano iz izveštaja brodaru: '
                 + STUFF((SELECT N', ' + p.Izvestaj + N' ' + ISNULL(p.Pokret, N'') + N' ' + ISNULL(p.DatumPokreta, N'')
                                 + CASE WHEN p.Poslat = 1 THEN N' (već poslat)' ELSE N'' END
                          FROM @Pokreti p WHERE p.Polje IS NOT NULL ORDER BY p.Izvestaj, p.Kljuc
                          FOR XML PATH(''), TYPE).value('.', 'nvarchar(max)'), 1, 2, N''), N'');

    BEGIN TRY
        BEGIN TRANSACTION;

        DELETE t FROM dbo.TerminalPrivMaersk t INNER JOIN @Pokreti p ON p.Izvestaj = N'Maersk' AND p.Kljuc = t.MaerskID WHERE p.Polje IS NOT NULL;
        DELETE t FROM dbo.TerminalPrivMSC t INNER JOIN @Pokreti p ON p.Izvestaj = N'MSC' AND p.Kljuc = t.MSKID WHERE p.Polje IS NOT NULL;
        DELETE t FROM dbo.TerminalPrivCMA t INNER JOIN @Pokreti p ON p.Izvestaj = N'CMA' AND p.Kljuc = t.CMAID WHERE p.Polje IS NOT NULL;
        DELETE t FROM dbo.TerminalPrivOOCL t INNER JOIN @Pokreti p ON p.Izvestaj = N'OOCL' AND p.Kljuc = t.OOCLID WHERE p.Polje IS NOT NULL;
        DELETE t FROM dbo.TerminalPrivHMM t INNER JOIN @Pokreti p ON p.Izvestaj = N'HMM' AND p.Kljuc = t.HMMID WHERE p.Polje IS NOT NULL;
        DELETE t FROM dbo.TerminalPrivHAPAG t INNER JOIN @Pokreti p ON p.Izvestaj = N'HAPAG' AND p.Kljuc = t.HAPAGID WHERE p.Polje IS NOT NULL;
        DELETE t FROM dbo.TerminalPrivFLEXBOX t INNER JOIN @Pokreti p ON p.Izvestaj = N'FLEXBOX' AND p.Kljuc = t.FLEXBOXID WHERE p.Polje IS NOT NULL;
        DELETE t FROM dbo.TerminalPrivCOSCO t INNER JOIN @Pokreti p ON p.Izvestaj = N'COSCO' AND p.Kljuc = t.COSCOID WHERE p.Polje IS NOT NULL;

        -- RB (redni broj u sledećem fajlu) neizvezenih CMA i OOCL pokreta ostaje 1, 2, 3... bez preskakanja
        IF EXISTS (SELECT 1 FROM @Pokreti WHERE Izvestaj = N'CMA' AND Polje IS NOT NULL)
        BEGIN
            WITH x AS (SELECT [RB], ROW_NUMBER() OVER (ORDER BY CMAID) AS Novi FROM dbo.TerminalPrivCMA WHERE Active <> 1)
            UPDATE x SET [RB] = Novi;
        END
        IF EXISTS (SELECT 1 FROM @Pokreti WHERE Izvestaj = N'OOCL' AND Polje IS NOT NULL)
        BEGIN
            WITH x AS (SELECT [RB], ROW_NUMBER() OVER (ORDER BY OOCLID) AS Novi FROM dbo.TerminalPrivOOCL WHERE Active <> 1)
            UPDATE x SET [RB] = Novi;
        END

        -- iz izmena u logu briše se samo upis poništenih datuma; red loga u kome ništa ne ostane se briše
        DECLARE @LogID int, @Deo nvarchar(100);
        DECLARE upisi CURSOR LOCAL FAST_FORWARD FOR SELECT LogID, Deo FROM @Ponisti WHERE LogID IS NOT NULL;
        OPEN upisi;
        FETCH NEXT FROM upisi INTO @LogID, @Deo;
        WHILE @@FETCH_STATUS = 0
        BEGIN
            UPDATE dbo.TerminalPrivremeniLog SET Opis = REPLACE(Opis, @Deo, N'') WHERE LogID = @LogID;
            FETCH NEXT FROM upisi INTO @LogID, @Deo;
        END
        CLOSE upisi;
        DEALLOCATE upisi;

        DELETE FROM dbo.TerminalPrivremeniLog
        WHERE LogID IN (SELECT LogID FROM @Ponisti WHERE LogID IS NOT NULL) AND LEN(ISNULL(Opis, N'')) = 0;

        -- trigger na TerminalPriv na osnovu ovoga u log upisuje VRAĆANJE umesto INSERT
        EXEC sp_set_session_context @key = N'VratiTerminalPriv', @value = 1;

        SET IDENTITY_INSERT dbo.TerminalPriv ON;

        INSERT INTO dbo.TerminalPriv
            (ID, [KONTEJNER], [STATUS], [POZICIJA], [VRSTA], [BRODAR], [NALOGODAVAC/UVOZ], [POSTUPAK/UVOZ], [UVOZNIK], [BL/UVOZ], [PLOMBA_UVOZ], [VOZ/kamion], [STANJE], [GATE_IN_E/F], [PREUZIMANJE_PUNOG/Razvoz], [VRAĆANJE_PRAZNOG/iz_Razvoza], [Konačni_GATE_OUT], [BOOKING/IZVOZ], [KLIJENT/IZVOZ], [GATE_OUT_EMPTY/Utovar], [GATE_IN_FULL/sa_Utovara], [GATE_IN/_GATE_OUT], [L/R], [TARA], [MAX_NOSIVOST_CNT], [VOZILO/PREUZIMANJE], [PLOMBA/IZVOZ], [NAPOMENA], [OPIS], [OTPREMA], [POSLATE SLIKE], [Prevoznik], [BELEŠKA], [VOZAČ])
        SELECT
            ID, [KONTEJNER], [STATUS], [POZICIJA], [VRSTA], [BRODAR], [NALOGODAVAC/UVOZ], [POSTUPAK/UVOZ], [UVOZNIK], [BL/UVOZ], [PLOMBA_UVOZ], [VOZ/kamion], [STANJE],
            CASE WHEN EXISTS (SELECT 1 FROM @Ponisti WHERE Polje = N'GATE_IN_E/F') THEN NULL ELSE [GATE_IN_E/F] END,
            CASE WHEN EXISTS (SELECT 1 FROM @Ponisti WHERE Polje = N'PREUZIMANJE_PUNOG/Razvoz') THEN NULL ELSE [PREUZIMANJE_PUNOG/Razvoz] END,
            CASE WHEN EXISTS (SELECT 1 FROM @Ponisti WHERE Polje = N'VRAĆANJE_PRAZNOG/iz_Razvoza') THEN NULL ELSE [VRAĆANJE_PRAZNOG/iz_Razvoza] END,
            NULL,   -- Konačni_GATE_OUT
            [BOOKING/IZVOZ], [KLIJENT/IZVOZ],
            CASE WHEN EXISTS (SELECT 1 FROM @Ponisti WHERE Polje = N'GATE_OUT_EMPTY/Utovar') THEN NULL ELSE [GATE_OUT_EMPTY/Utovar] END,
            CASE WHEN EXISTS (SELECT 1 FROM @Ponisti WHERE Polje = N'GATE_IN_FULL/sa_Utovara') THEN NULL ELSE [GATE_IN_FULL/sa_Utovara] END,
            [GATE_IN/_GATE_OUT], [L/R], [TARA], [MAX_NOSIVOST_CNT], [VOZILO/PREUZIMANJE], [PLOMBA/IZVOZ], [NAPOMENA], [OPIS], [OTPREMA], [POSLATE SLIKE], [Prevoznik], [BELEŠKA], [VOZAČ]
        FROM dbo.TerminalPrivArhiv
        WHERE ID = @ID;

        SET IDENTITY_INSERT dbo.TerminalPriv OFF;
        EXEC sp_set_session_context @key = N'VratiTerminalPriv', @value = 0;

        -- red loga VRAĆANJE (upisao ga je trigger) dobija opis šta je poništeno
        UPDATE dbo.TerminalPrivremeniLog SET Opis = @OpisVracanja
        WHERE LogID = (SELECT MAX(LogID) FROM dbo.TerminalPrivremeniLog WHERE TerminalPrivID = @ID AND Akcija = N'VRAĆANJE');

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

-- 6. Zapisi u terminalu kod kojih TARA nema tačno 4, a MAX_NOSIVOST_CNT tačno 5 karaktera (prazno polje je dozvoljeno).
-- Program takav zapis ne čuva dok se vrednost ne ispravi; tabela je prazna ako takvih zapisa nema. Ništa se ne menja.
SELECT ID, [KONTEJNER] AS Kontejner, [TARA], [MAX_NOSIVOST_CNT]
FROM dbo.TerminalPriv
WHERE (LEN(LTRIM([TARA])) > 0 AND LEN(LTRIM([TARA])) <> 4)
   OR (LEN(LTRIM([MAX_NOSIVOST_CNT])) > 0 AND LEN(LTRIM([MAX_NOSIVOST_CNT])) <> 5)
ORDER BY ID;
GO

SET NOEXEC OFF;
GO
