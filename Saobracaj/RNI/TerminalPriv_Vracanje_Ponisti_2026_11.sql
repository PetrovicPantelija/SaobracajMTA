-- PRODUKCIJA (novembar 2026): Terminal privremeni - vraćanje iz arhive poništava celu poslednju izmenu zapisa (kao Undo).
-- Pustiti POSLE TerminalPriv_VozacTekst_Vracanje_2026_11.sql; menja samo proceduru VratiTerminalPrivIzArhive.
-- Dopuna u odnosu na prethodnu verziju: osim datuma koji su upisali pokret u izveštaj brodaru, na staru vrednost se vraćaju
-- SVA polja koja je ta izmena promenila (npr. GATE_IN/_GATE_OUT: GATE OUT E -> GATE IN E, BL/UVOZ, NAPOMENA...), a poništena
-- izmena se briše iz loga. Red loga VRAĆANJE beleži šta je vraćeno (Polje: vrednost u arhivi -> vraćena vrednost).
-- Pustiti neposredno pre instalacije nove verzije programa (poruka pre vraćanja čita nov oblik provere).
-- Skripta se može pustiti više puta. Pre pokretanja izabrati produkcionu bazu.
-- Vraćanje iz arhive koristi SET IDENTITY_INSERT dbo.TerminalPriv i dinamički UPDATE nad dbo.TerminalPrivArhiv: korisnik iz
-- konekcije programa mora imati pravo ALTER nad TerminalPriv i UPDATE nad TerminalPrivArhiv (vlasnik baze ih ima).

-- procedura koristi XML, za koji je QUOTED_IDENTIFIER obavezan, i kad se skripta pusti iz sqlcmd
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

-- Provera: mora biti puštena TerminalPriv_VozacTekst_Vracanje_2026_11.sql (VOZAČ kao tekst, log trigger sa akcijom VRAĆANJE)
IF OBJECT_ID('dbo.TerminalPriv', 'U') IS NULL OR OBJECT_ID('dbo.TerminalPrivArhiv', 'U') IS NULL
   OR OBJECT_ID('dbo.TerminalPrivremeniLog', 'U') IS NULL OR OBJECT_ID('dbo.trg_TerminalPriv_Log', 'TR') IS NULL
   OR NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'dbo.TerminalPrivArhiv') AND name = N'VOZAČ' AND TYPE_NAME(system_type_id) = N'nvarchar')
   OR COL_LENGTH('dbo.TerminalPrivOOCL', 'ActiveMail') IS NULL
BEGIN
    RAISERROR(N'Baza nema kolonu VOZAČ kao tekst ili log Terminala privremenog. Prvo izvršiti TerminalPriv_VozacTekst_Vracanje_2026_11.sql. Skripta je prekinuta.', 16, 1);
    SET NOEXEC ON;
END
GO

-- Vraćanje zapisa iz arhive (dugme 'Vrati iz arhive' u formi Arhivirani podaci).
-- Zapis se vraća u TerminalPriv sa istim ID-jem (folderi slika i dokumenata i log ostaju povezani sa zapisom). Poništava se
-- poslednja izmena zapisa, kao Undo:
--   - izmena koja se poništava je ona koja je upisala Konačni_GATE_OUT ili, ako je ranija, poslednja izmena koja je upisala
--     pokret u izveštaj brodaru (TerminalPrivMaersk ... TerminalPrivCOSCO); poništavaju se ona i sve kasnije izmene,
--   - sva polja koja su te izmene promenile vraćaju se na vrednost pre njih (vrednosti se čitaju iz loga, 'Polje: staro -> novo; '),
--     osim POSLATE SLIKE (broj slika u folderu; slike se ne brišu),
--   - iz izveštaja brodaru brišu se pokreti koje su te izmene upisale (pokret nosi datum koji je prešao iz praznog u popunjen,
--     Maersk, MSC, CMA i OOCL dan i sat, ostali samo dan, i smer: ulaz ili izlaz, po polju; prelaz GATE_IN/_GATE_OUT
--     PRIVREMENO -> GATE IN E/F upisuje pokret za GATE_IN_E/F); od kraja izveštaja brišu se uzastopni pokreti tih izmena,
--   - poništene izmene se brišu iz loga (ostaje samo deo za POSLATE SLIKE, ako ga ima).
-- Konačni_GATE_OUT uvek postaje NULL (inače bi ga sledeće 'Arhiviraj' odmah vratilo u arhivu).
-- Ako u logu nema ni upisa Konačni_GATE_OUT ni izmene koja je upisala pokret (pokret je upisan pri unosu zapisa), kao do sada:
-- briše se poslednji pokret, datum koji ga je upisao i Konačni_GATE_OUT.
-- Čita se samo log od poslednjeg unosa ili vraćanja zapisa (INSERT, VRAĆANJE), pa raniji krugovi arhiviranja ne smetaju.
-- @Proveri = 1: ništa se ne menja; vraća tri skupa za poruku pre vraćanja: (1) Kontejner, KonacniGateOut, NadjenLog (nađen upis
-- Konačni_GATE_OUT u logu), UTerminalu (broj zapisa istog kontejnera u terminalu), BrojPokreta (pokreti zapisa u izveštajima),
-- DatumIzmene (izmena od koje se poništava), BrojIzmena (broj poništenih izmena); (2) pokreti koji se brišu (Izvestaj, Pokret,
-- DatumPokreta, Poslat, DatumIzvoza, Polje = datum koji ga je upisao); (3) polja koja se menjaju (Polje, JeDatum, Sada = vrednost
-- u arhivi, Postaje = vraćena vrednost; datum u obliku 2026-11-23 12:51:00, NULL = prazno).
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

    DECLARE @ObrazacDatuma nvarchar(100) = N'[12][0-9][0-9][0-9]-[01][0-9]-[0-3][0-9] [0-2][0-9]:[0-5][0-9]:[0-5][0-9]';

    -- polja zapisa redom kao u logu (trg_TerminalPriv_Log), smer pokreta koji datum upisuje (U = ulaz, I = izlaz; NULL = polje
    -- nije datum) i vrednost u arhivi kao tekst iz loga (datum stilom 120, NULL = prazno)
    DECLARE @Polja TABLE
    (
        RB      int           NOT NULL PRIMARY KEY,
        Polje   nvarchar(40)  NOT NULL UNIQUE,
        Smer    char(1)       NULL,
        Arhiva  nvarchar(max) NULL,
        Tekst   nvarchar(max) NULL   -- vrednost u prolazu kroz log, od poslednje izmene ka prvoj
    );
    INSERT INTO @Polja (RB, Polje, Smer, Arhiva)
    SELECT v.RB, v.Polje, v.Smer, v.Arhiva
    FROM dbo.TerminalPrivArhiv a
    CROSS APPLY (VALUES
        (1,  N'KONTEJNER',                   NULL, CAST(a.[KONTEJNER] AS nvarchar(max))),
        (2,  N'STATUS',                      NULL, a.[STATUS]),
        (3,  N'POZICIJA',                    NULL, a.[POZICIJA]),
        (4,  N'VRSTA',                       NULL, a.[VRSTA]),
        (5,  N'BRODAR',                      NULL, a.[BRODAR]),
        (6,  N'NALOGODAVAC/UVOZ',            NULL, a.[NALOGODAVAC/UVOZ]),
        (7,  N'POSTUPAK/UVOZ',               NULL, a.[POSTUPAK/UVOZ]),
        (8,  N'UVOZNIK',                     NULL, a.[UVOZNIK]),
        (9,  N'BL/UVOZ',                     NULL, a.[BL/UVOZ]),
        (10, N'PLOMBA_UVOZ',                 NULL, a.[PLOMBA_UVOZ]),
        (11, N'VOZ/kamion',                  NULL, a.[VOZ/kamion]),
        (12, N'STANJE',                      NULL, a.[STANJE]),
        (13, N'GATE_IN_E/F',                 'U',  CONVERT(nvarchar(20), a.[GATE_IN_E/F], 120)),
        (14, N'PREUZIMANJE_PUNOG/Razvoz',    'I',  CONVERT(nvarchar(20), a.[PREUZIMANJE_PUNOG/Razvoz], 120)),
        (15, N'VRAĆANJE_PRAZNOG/iz_Razvoza', 'U',  CONVERT(nvarchar(20), a.[VRAĆANJE_PRAZNOG/iz_Razvoza], 120)),
        (16, N'Konačni_GATE_OUT',            'I',  CONVERT(nvarchar(20), a.[Konačni_GATE_OUT], 120)),
        (17, N'BOOKING/IZVOZ',               NULL, a.[BOOKING/IZVOZ]),
        (18, N'KLIJENT/IZVOZ',               NULL, a.[KLIJENT/IZVOZ]),
        (19, N'GATE_OUT_EMPTY/Utovar',       'I',  CONVERT(nvarchar(20), a.[GATE_OUT_EMPTY/Utovar], 120)),
        (20, N'GATE_IN_FULL/sa_Utovara',     'U',  CONVERT(nvarchar(20), a.[GATE_IN_FULL/sa_Utovara], 120)),
        (21, N'GATE_IN/_GATE_OUT',           NULL, a.[GATE_IN/_GATE_OUT]),
        (22, N'L/R',                         NULL, a.[L/R]),
        (23, N'TARA',                        NULL, a.[TARA]),
        (24, N'MAX_NOSIVOST_CNT',            NULL, a.[MAX_NOSIVOST_CNT]),
        (25, N'VOZILO/PREUZIMANJE',          NULL, a.[VOZILO/PREUZIMANJE]),
        (26, N'PLOMBA/IZVOZ',                NULL, a.[PLOMBA/IZVOZ]),
        (27, N'NAPOMENA',                    NULL, a.[NAPOMENA]),
        (28, N'OPIS',                        NULL, a.[OPIS]),
        (29, N'OTPREMA',                     NULL, a.[OTPREMA]),
        (30, N'POSLATE SLIKE',               NULL, a.[POSLATE SLIKE]),
        (31, N'Prevoznik',                   NULL, a.[Prevoznik]),
        (32, N'BELEŠKA',                     NULL, a.[BELEŠKA]),
        (33, N'VOZAČ',                       NULL, a.[VOZAČ])
    ) v (RB, Polje, Smer, Arhiva)
    WHERE a.ID = @ID;
    UPDATE @Polja SET Tekst = Arhiva;

    -- izmene zapisa od poslednjeg unosa ili vraćanja iz arhive; GateInEF = GATE_IN_E/F posle izmene
    DECLARE @Granica int;
    SELECT @Granica = ISNULL(MAX(LogID), 0) FROM dbo.TerminalPrivremeniLog
    WHERE TerminalPrivID = @ID AND Akcija IN (N'INSERT', N'VRAĆANJE');

    DECLARE @Redovi TABLE
    (
        LogID     int           NOT NULL PRIMARY KEY,
        Opis      nvarchar(max) NULL,
        GateInEF  nvarchar(max) NULL,
        Ostatak   nvarchar(max) NULL   -- deo opisa koji nije pročitan (ostaje u logu)
    );
    INSERT INTO @Redovi (LogID, Opis)
    SELECT LogID, Opis FROM dbo.TerminalPrivremeniLog
    WHERE TerminalPrivID = @ID AND Akcija = N'UPDATE' AND LogID > @Granica;

    -- delovi izmena: staro i novo po polju (NULL = prazno)
    DECLARE @Deli TABLE
    (
        LogID  int           NOT NULL,
        RB     int           NOT NULL,
        Staro  nvarchar(max) NULL,
        Novo   nvarchar(max) NULL,
        PRIMARY KEY (LogID, RB)
    );

    -- Čitanje izmena od poslednje ka prvoj. Deo izmene je 'Polje: staro -> novo; ', polja su uvek istim redom, a novo je vrednost
    -- posle izmene (Tekst), pa se kraj dela traži kao ' -> novo; ' iza kog je kraj opisa ili sledeće polje; tako vrednost sme da
    -- sadrži ' -> ' i '; '. Ako se novo ne poklapa, deo se deli na prvom ' -> ' i na '; ' iza kog je kraj ili sledeće polje.
    DECLARE @LogID int, @Opis nvarchar(max), @Duz int, @Poz int, @RB int, @Polje nvarchar(40), @Tekst nvarchar(max),
            @Pocetak int, @Trazi nvarchar(max), @Nadjeno int, @Kraj int, @Strelica int, @Pokusaj int,
            @Staro nvarchar(max), @Novo nvarchar(max);

    SELECT @LogID = MAX(LogID) FROM @Redovi;
    WHILE @LogID IS NOT NULL
    BEGIN
        SELECT @Opis = ISNULL(Opis, N'') FROM @Redovi WHERE LogID = @LogID;
        SET @Duz = DATALENGTH(@Opis) / 2;
        SET @Poz = 1;
        SET @RB = 0;
        UPDATE @Redovi SET GateInEF = (SELECT Tekst FROM @Polja WHERE Polje = N'GATE_IN_E/F') WHERE LogID = @LogID;

        WHILE @Poz <= @Duz
        BEGIN
            SET @Polje = NULL;
            SELECT TOP 1 @RB = RB, @Polje = Polje, @Tekst = Tekst
            FROM @Polja
            WHERE RB > @RB AND SUBSTRING(@Opis, @Poz, DATALENGTH(Polje) / 2 + 2) = Polje + N': '
            ORDER BY RB;
            IF @Polje IS NULL
                BREAK;

            SET @Pocetak = @Poz + DATALENGTH(@Polje) / 2 + 2;   -- početak stare vrednosti
            SET @Kraj = 0;
            SET @Strelica = 0;
            SET @Pokusaj = 1;
            WHILE @Pokusaj <= 2 AND @Kraj = 0
            BEGIN
                IF @Pokusaj = 1
                BEGIN
                    SET @Trazi = N' -> ' + ISNULL(@Tekst, N'(prazno)') + N'; ';
                    SET @Nadjeno = CHARINDEX(@Trazi, @Opis, @Pocetak);
                END
                ELSE
                BEGIN
                    SET @Strelica = CHARINDEX(N' -> ', @Opis, @Pocetak);
                    SET @Trazi = N'; ';
                    SET @Nadjeno = CASE WHEN @Strelica > 0 THEN CHARINDEX(@Trazi, @Opis, @Strelica + 4) ELSE 0 END;
                END

                WHILE @Nadjeno > 0 AND @Kraj = 0
                BEGIN
                    IF @Nadjeno + DATALENGTH(@Trazi) / 2 > @Duz
                       OR EXISTS (SELECT 1 FROM @Polja
                                  WHERE RB > @RB
                                    AND SUBSTRING(@Opis, @Nadjeno + DATALENGTH(@Trazi) / 2, DATALENGTH(Polje) / 2 + 2) = Polje + N': ')
                        SET @Kraj = @Nadjeno + DATALENGTH(@Trazi) / 2;
                    ELSE
                        SET @Nadjeno = CHARINDEX(@Trazi, @Opis, @Nadjeno + 1);
                END
                SET @Pokusaj = @Pokusaj + 1;
            END
            IF @Kraj = 0
                BREAK;

            IF @Strelica = 0
            BEGIN
                SET @Staro = SUBSTRING(@Opis, @Pocetak, @Nadjeno - @Pocetak);
                SET @Novo = @Tekst;
            END
            ELSE
            BEGIN
                SET @Staro = SUBSTRING(@Opis, @Pocetak, @Strelica - @Pocetak);
                SET @Novo = SUBSTRING(@Opis, @Strelica + 4, @Nadjeno - @Strelica - 4);
                IF @Novo = N'(prazno)' SET @Novo = NULL;
            END
            IF @Staro = N'(prazno)' SET @Staro = NULL;

            INSERT INTO @Deli (LogID, RB, Staro, Novo) VALUES (@LogID, @RB, @Staro, @Novo);
            UPDATE @Polja SET Tekst = @Staro WHERE RB = @RB;
            SET @Poz = @Kraj;
        END

        IF @Poz <= @Duz
            UPDATE @Redovi SET Ostatak = SUBSTRING(@Opis, @Poz, @Duz) WHERE LogID = @LogID;

        SELECT @LogID = MAX(LogID) FROM @Redovi WHERE LogID < @LogID;
    END

    -- upisi datuma iz praznog u popunjen (svaki je mogao da upiše pokret): iz izmena u logu, prelaz PRIVREMENO -> GATE IN E/F
    -- (updTerminalPriv tada upisuje pokret za GATE_IN_E/F kao da je datum tek popunjen) i, sa LogID = 0, datumi arhiviranog
    -- zapisa (za pokret koji nije upisala nijedna izmena iz loga)
    DECLARE @Upisi TABLE
    (
        LogID     int           NOT NULL,
        Polje     nvarchar(40)  NOT NULL,
        Smer      char(1)       NOT NULL,
        Vrednost  datetime      NOT NULL,
        DanSat    nvarchar(16)  NULL,   -- 23.11.2026 12:51
        Dan       nvarchar(10)  NULL,   -- 23.11.2026
        PRIMARY KEY (LogID, Polje)
    );
    INSERT INTO @Upisi (LogID, Polje, Smer, Vrednost)
    -- CASE: pretvara se samo tekst u obliku datuma (TRY_CONVERT ne postoji u bazi sa nivoom kompatibilnosti 100)
    SELECT d.LogID, p.Polje, p.Smer, CASE WHEN d.Novo LIKE @ObrazacDatuma THEN CONVERT(datetime, d.Novo, 120) END
    FROM @Deli d
    INNER JOIN @Polja p ON p.RB = d.RB
    WHERE p.Smer IS NOT NULL AND d.Staro IS NULL AND d.Novo LIKE @ObrazacDatuma;

    INSERT INTO @Upisi (LogID, Polje, Smer, Vrednost)
    SELECT r.LogID, N'GATE_IN_E/F', 'U', CASE WHEN r.GateInEF LIKE @ObrazacDatuma THEN CONVERT(datetime, r.GateInEF, 120) END
    FROM @Redovi r
    INNER JOIN @Deli g ON g.LogID = r.LogID
    INNER JOIN @Polja p ON p.RB = g.RB AND p.Polje = N'GATE_IN/_GATE_OUT'
    WHERE g.Staro = N'PRIVREMENO' AND g.Novo IN (N'GATE IN E', N'GATE IN F') AND r.GateInEF LIKE @ObrazacDatuma
      AND NOT EXISTS (SELECT 1 FROM @Upisi u WHERE u.LogID = r.LogID AND u.Polje = N'GATE_IN_E/F');

    INSERT INTO @Upisi (LogID, Polje, Smer, Vrednost)
    SELECT 0, Polje, Smer, CONVERT(datetime, Arhiva, 120)
    FROM @Polja
    WHERE Smer IS NOT NULL AND Arhiva IS NOT NULL;

    UPDATE @Upisi
    SET DanSat = CONVERT(nvarchar(10), Vrednost, 104) + N' ' + LEFT(CONVERT(nvarchar(8), Vrednost, 108), 5),
        Dan = CONVERT(nvarchar(10), Vrednost, 104);

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
        UpisLogID     int           NULL,   -- izmena koja ga je upisala
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

    -- izmena koja je upisala Konačni_GATE_OUT; poništava se od ranije od te dve izmene (@Od) do kraja loga
    DECLARE @KonacniLog int, @Od int;
    SELECT @KonacniLog = MAX(LogID) FROM @Upisi WHERE Polje = N'Konačni_GATE_OUT' AND LogID > 0;
    SET @Od = CASE WHEN @Izmena > 0 AND @KonacniLog > 0
                   THEN CASE WHEN @Izmena < @KonacniLog THEN @Izmena ELSE @KonacniLog END
                   WHEN @Izmena > 0 THEN @Izmena
                   ELSE @KonacniLog END;

    -- pokreti koji se brišu: od kraja svakog izveštaja, dok pokret odgovara nekom upisu datuma poništenih izmena (svaki upis
    -- jednom). Bez poništenih izmena (@Od NULL) to su upisi izmene @Izmena (0 = datumi arhiviranog zapisa).
    IF @Od IS NOT NULL OR @Izmena IS NOT NULL
    BEGIN
        DECLARE @Izvestaj nvarchar(20), @Kljuc int, @PoljePokreta nvarchar(40), @UpisLogID int;
        DECLARE izvestaji CURSOR LOCAL FAST_FORWARD FOR SELECT DISTINCT Izvestaj FROM @Pokreti;
        OPEN izvestaji;
        FETCH NEXT FROM izvestaji INTO @Izvestaj;
        WHILE @@FETCH_STATUS = 0
        BEGIN
            SELECT @Kljuc = MAX(Kljuc) FROM @Pokreti WHERE Izvestaj = @Izvestaj;
            WHILE @Kljuc IS NOT NULL
            BEGIN
                SET @PoljePokreta = NULL;
                SELECT TOP 1 @PoljePokreta = u.Polje, @UpisLogID = u.LogID
                FROM @Pokreti p
                INNER JOIN @Upisi u ON u.Smer = p.Smer
                                   AND p.Vreme = CASE WHEN p.SaSatom = 1 THEN u.DanSat ELSE u.Dan END
                WHERE p.Izvestaj = @Izvestaj AND p.Kljuc = @Kljuc
                  AND (u.LogID >= @Od OR (@Od IS NULL AND u.LogID = @Izmena))
                  AND NOT EXISTS (SELECT 1 FROM @Pokreti z
                                  WHERE z.Izvestaj = @Izvestaj AND z.Polje = u.Polje AND z.UpisLogID = u.LogID)
                ORDER BY u.LogID DESC, u.Vrednost DESC;
                IF @PoljePokreta IS NULL
                    BREAK;

                UPDATE @Pokreti SET Polje = @PoljePokreta, UpisLogID = @UpisLogID WHERE Izvestaj = @Izvestaj AND Kljuc = @Kljuc;
                SELECT @Kljuc = MAX(Kljuc) FROM @Pokreti WHERE Izvestaj = @Izvestaj AND Kljuc < @Kljuc;
            END
            FETCH NEXT FROM izvestaji INTO @Izvestaj;
        END
        CLOSE izvestaji;
        DEALLOCATE izvestaji;
    END

    -- polja koja se menjaju: Sada = vrednost u arhivi, Postaje = vrednost pre prve poništene izmene. Bez poništenih izmena:
    -- datumi koji su upisali pokrete koji se brišu postaju prazni. Konačni_GATE_OUT uvek postaje prazan.
    DECLARE @Promene TABLE
    (
        RB       int           NOT NULL PRIMARY KEY,
        Polje    nvarchar(40)  NOT NULL,
        JeDatum  bit           NOT NULL,
        Sada     nvarchar(max) NULL,
        Postaje  nvarchar(max) NULL
    );
    IF @Od IS NOT NULL
        INSERT INTO @Promene (RB, Polje, JeDatum, Sada, Postaje)
        SELECT p.RB, p.Polje, CASE WHEN p.Smer IS NULL THEN 0 ELSE 1 END, p.Arhiva, d.Staro
        FROM @Polja p
        INNER JOIN @Deli d ON d.RB = p.RB
                          AND d.LogID = (SELECT MIN(x.LogID) FROM @Deli x WHERE x.RB = p.RB AND x.LogID >= @Od)
        WHERE p.Polje <> N'POSLATE SLIKE';
    ELSE
        INSERT INTO @Promene (RB, Polje, JeDatum, Sada, Postaje)
        SELECT p.RB, p.Polje, 1, p.Arhiva, NULL
        FROM @Polja p
        WHERE EXISTS (SELECT 1 FROM @Pokreti k WHERE k.Polje = p.Polje);

    UPDATE @Promene SET Postaje = NULL WHERE Polje = N'Konačni_GATE_OUT';
    INSERT INTO @Promene (RB, Polje, JeDatum, Sada, Postaje)
    SELECT RB, Polje, 1, Arhiva, NULL
    FROM @Polja
    WHERE Polje = N'Konačni_GATE_OUT' AND NOT EXISTS (SELECT 1 FROM @Promene WHERE Polje = N'Konačni_GATE_OUT');

    -- ne menja se: polje koje su izmene vratile na istu vrednost, prazan KONTEJNER i datum koji nije u obliku datuma
    DELETE FROM @Promene
    WHERE NOT EXISTS (SELECT Sada EXCEPT SELECT Postaje)
       OR (Polje = N'KONTEJNER' AND Postaje IS NULL)
       OR (JeDatum = 1 AND Postaje IS NOT NULL AND Postaje NOT LIKE @ObrazacDatuma);

    -- poništene izmene: one od @Od u kojima se nešto vraća (POSLATE SLIKE se ne vraća)
    DECLARE @BrojIzmena int;
    SELECT @BrojIzmena = COUNT(*) FROM @Redovi r
    WHERE r.LogID >= @Od
      AND EXISTS (SELECT 1 FROM @Deli d INNER JOIN @Polja p ON p.RB = d.RB WHERE d.LogID = r.LogID AND p.Polje <> N'POSLATE SLIKE');

    IF @Proveri = 1
    BEGIN
        SELECT @Kontejner AS Kontejner,
               @Konacni AS KonacniGateOut,
               CASE WHEN @KonacniLog IS NULL THEN 0 ELSE 1 END AS NadjenLog,
               (SELECT COUNT(*) FROM dbo.TerminalPriv WHERE [KONTEJNER] = @Kontejner) AS UTerminalu,
               (SELECT COUNT(*) FROM @Pokreti) AS BrojPokreta,
               (SELECT Datum FROM dbo.TerminalPrivremeniLog WHERE LogID = @Od) AS DatumIzmene,
               @BrojIzmena AS BrojIzmena;
        SELECT Izvestaj, Pokret, DatumPokreta, Poslat, DatumIzvoza, Polje
        FROM @Pokreti WHERE Polje IS NOT NULL ORDER BY Izvestaj, Kljuc;
        SELECT Polje, JeDatum, Sada, Postaje
        FROM @Promene ORDER BY RB;
        RETURN;
    END

    -- opis za red loga VRAĆANJE, u obliku loga: 'Polje: vrednost u arhivi -> vraćena vrednost; '
    DECLARE @DatumIzmene datetime;
    SELECT @DatumIzmene = Datum FROM dbo.TerminalPrivremeniLog WHERE LogID = @Od;

    DECLARE @OpisVracanja nvarchar(max) = N'Vraćeno iz arhive.'
        + CASE WHEN @BrojIzmena > 0
               THEN N' Poništena izmena od ' + CONVERT(nvarchar(10), @DatumIzmene, 104) + N' ' + LEFT(CONVERT(nvarchar(8), @DatumIzmene, 108), 5)
                    + CASE WHEN @BrojIzmena > 1 THEN N' i ' + CAST(@BrojIzmena - 1 AS nvarchar(10)) + N' kasnijih' ELSE N'' END + N'.'
               ELSE N'' END
        + ISNULL(RTRIM(N' Vraćeno: '
                 + (SELECT z.Polje + N': ' + ISNULL(z.Sada, N'(prazno)') + N' -> ' + ISNULL(z.Postaje, N'(prazno)') + N'; '
                    FROM @Promene z ORDER BY z.RB
                    FOR XML PATH(''), TYPE).value('.', 'nvarchar(max)')), N'')
        + ISNULL(N' Obrisano iz izveštaja brodaru: '
                 + STUFF((SELECT N', ' + p.Izvestaj + N' ' + ISNULL(p.Pokret, N'') + N' ' + ISNULL(p.DatumPokreta, N'')
                                 + CASE WHEN p.Poslat = 1 THEN N' (već poslat)' ELSE N'' END
                          FROM @Pokreti p WHERE p.Polje IS NOT NULL ORDER BY p.Izvestaj, p.Kljuc
                          FOR XML PATH(''), TYPE).value('.', 'nvarchar(max)'), 1, 2, N'') + N'.', N'');

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

        -- poništene izmene se brišu iz loga; ostaje samo deo za POSLATE SLIKE i deo opisa koji nije pročitan
        IF @Od IS NOT NULL
        BEGIN
            UPDATE l
            SET Opis = ISNULL((SELECT N'POSLATE SLIKE: ' + ISNULL(d.Staro, N'(prazno)') + N' -> ' + ISNULL(d.Novo, N'(prazno)') + N'; '
                               FROM @Deli d INNER JOIN @Polja p ON p.RB = d.RB
                               WHERE d.LogID = r.LogID AND p.Polje = N'POSLATE SLIKE'), N'')
                     + ISNULL(r.Ostatak, N'')
            FROM dbo.TerminalPrivremeniLog l
            INNER JOIN @Redovi r ON r.LogID = l.LogID
            WHERE r.LogID >= @Od;

            DELETE l
            FROM dbo.TerminalPrivremeniLog l
            INNER JOIN @Redovi r ON r.LogID = l.LogID
            WHERE r.LogID >= @Od AND LEN(ISNULL(l.Opis, N'')) = 0;
        END

        -- vraćene vrednosti se upisuju u zapis arhive, koji se zatim prebacuje u terminal (Konačni_GATE_OUT je NULL u INSERT-u)
        DECLARE @Sql nvarchar(max), @PoljePromene nvarchar(40), @JeDatum bit, @Postaje nvarchar(max);
        DECLARE promene CURSOR LOCAL FAST_FORWARD FOR
            SELECT Polje, JeDatum, Postaje FROM @Promene WHERE Polje <> N'Konačni_GATE_OUT';
        OPEN promene;
        FETCH NEXT FROM promene INTO @PoljePromene, @JeDatum, @Postaje;
        WHILE @@FETCH_STATUS = 0
        BEGIN
            SET @Sql = N'UPDATE dbo.TerminalPrivArhiv SET ' + QUOTENAME(@PoljePromene) + N' = '
                     + CASE WHEN @JeDatum = 1 THEN N'CONVERT(datetime, @Vrednost, 120)' ELSE N'@Vrednost' END
                     + N' WHERE ID = @ID;';
            EXEC sp_executesql @Sql, N'@Vrednost nvarchar(max), @ID int', @Vrednost = @Postaje, @ID = @ID;
            FETCH NEXT FROM promene INTO @PoljePromene, @JeDatum, @Postaje;
        END
        CLOSE promene;
        DEALLOCATE promene;

        -- trigger na TerminalPriv na osnovu ovoga u log upisuje VRAĆANJE umesto INSERT
        EXEC sp_set_session_context @key = N'VratiTerminalPriv', @value = 1;

        SET IDENTITY_INSERT dbo.TerminalPriv ON;

        INSERT INTO dbo.TerminalPriv
            (ID, [KONTEJNER], [STATUS], [POZICIJA], [VRSTA], [BRODAR], [NALOGODAVAC/UVOZ], [POSTUPAK/UVOZ], [UVOZNIK], [BL/UVOZ], [PLOMBA_UVOZ], [VOZ/kamion], [STANJE], [GATE_IN_E/F], [PREUZIMANJE_PUNOG/Razvoz], [VRAĆANJE_PRAZNOG/iz_Razvoza], [Konačni_GATE_OUT], [BOOKING/IZVOZ], [KLIJENT/IZVOZ], [GATE_OUT_EMPTY/Utovar], [GATE_IN_FULL/sa_Utovara], [GATE_IN/_GATE_OUT], [L/R], [TARA], [MAX_NOSIVOST_CNT], [VOZILO/PREUZIMANJE], [PLOMBA/IZVOZ], [NAPOMENA], [OPIS], [OTPREMA], [POSLATE SLIKE], [Prevoznik], [BELEŠKA], [VOZAČ])
        SELECT
            ID, [KONTEJNER], [STATUS], [POZICIJA], [VRSTA], [BRODAR], [NALOGODAVAC/UVOZ], [POSTUPAK/UVOZ], [UVOZNIK], [BL/UVOZ], [PLOMBA_UVOZ], [VOZ/kamion], [STANJE], [GATE_IN_E/F], [PREUZIMANJE_PUNOG/Razvoz], [VRAĆANJE_PRAZNOG/iz_Razvoza],
            NULL,   -- Konačni_GATE_OUT
            [BOOKING/IZVOZ], [KLIJENT/IZVOZ], [GATE_OUT_EMPTY/Utovar], [GATE_IN_FULL/sa_Utovara], [GATE_IN/_GATE_OUT], [L/R], [TARA], [MAX_NOSIVOST_CNT], [VOZILO/PREUZIMANJE], [PLOMBA/IZVOZ], [NAPOMENA], [OPIS], [OTPREMA], [POSLATE SLIKE], [Prevoznik], [BELEŠKA], [VOZAČ]
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

SET NOEXEC OFF;
GO
