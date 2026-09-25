-- Terminal privremeni: tabela TerminalPriv + stored procedure insTerminalPriv, updTerminalPriv, delTerminalPriv
-- Skriptu izvrsiti nad bazom aplikacije. Forma: Saobracaj.RNI.frmTerminalPrivremeni

IF OBJECT_ID('dbo.TerminalPriv', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.TerminalPriv
    (
        ID                      int IDENTITY(1,1) NOT NULL,
        KONTEJNER               nvarchar(25)  NOT NULL,
        STATUS                  nvarchar(25)  NULL,
        POZICIJA                nvarchar(25)  NULL,
        VRSTA                   nvarchar(25)  NULL,
        BRODAR                  nvarchar(25)  NULL,
        NALOGODAVAC             nvarchar(25)  NULL,
        POSTUPAK                nvarchar(25)  NULL,
        UVOZNIK                 nvarchar(25)  NULL,
        PLOMBA_UVOZ             nvarchar(25)  NULL,
        VOZ                     nvarchar(25)  NULL,
        STANJE                  nvarchar(25)  NULL,
        [GATE_IN_E/F]           datetime      NULL,
        PREUZIMANJE_PUNOG       datetime      NULL,
        [VRAĆANJE_PRAZNOG]      datetime      NULL,
        [Konačni_GATE_OUT]      datetime      NULL,
        BOOKING                 nvarchar(25)  NULL,
        KLIJENT                 nvarchar(25)  NULL,
        [GATE_OUT_EMPTY Utovar] datetime      NULL,
        [GATE_IN_FULL Utovar]   datetime      NULL,
        [GATE_IN/GATE_OUT]      nvarchar(50)  NULL,
        [L/R]                   nvarchar(25)  NULL,
        TARA                    nvarchar(25)  NULL,
        [MAX]                   nvarchar(25)  NULL,
        VOZILO                  nvarchar(25)  NULL,
        PLOMBA                  nvarchar(25)  NULL,
        NAPOMENA                nvarchar(100) NULL,
        OPIS                    nvarchar(100) NULL,
        OTPREMA                 nvarchar(25)  NULL,
        [POSLATE SLIKE]         nvarchar(25)  NULL,
        Prevoznik               nvarchar(25)  NULL,
        CONSTRAINT PK_TerminalPriv PRIMARY KEY CLUSTERED (ID),
        CONSTRAINT CK_TerminalPriv_KONTEJNER CHECK (LEN(KONTEJNER) = 11),
        CONSTRAINT CK_TerminalPriv_STATUS CHECK (STATUS IN (N'PRAZAN', N'PUN', N'?', N'RAZVOZ', N'Blanks', N'PRETOVAR', N'U RAZVOZU', N'UTOVAR')),
        CONSTRAINT CK_TerminalPriv_STANJE CHECK (STANJE IN (N'DOBAR', N'LOŠ', N'FOOD GRADE', N'OŠTEĆEN')),
        CONSTRAINT CK_TerminalPriv_GATE CHECK ([GATE_IN/GATE_OUT] IN (N'GATE IN E', N'GATE IN F', N'GATE OUT E', N'GATE OUT F', N'GATE IN E/REPOZICIJA', N'PRIJAVITI GATE IN E', N'NE ŠALJEMO POKRET BRODARU', N'REUSE', N'REPOZICIJA', N'PRIVREMENO'))
    );
END
GO

IF OBJECT_ID('dbo.insTerminalPriv', 'P') IS NOT NULL DROP PROCEDURE dbo.insTerminalPriv;
GO
CREATE PROCEDURE dbo.insTerminalPriv
    @KONTEJNER nvarchar(25),
    @STATUS nvarchar(25) = NULL,
    @POZICIJA nvarchar(25) = NULL,
    @VRSTA nvarchar(25) = NULL,
    @BRODAR nvarchar(25) = NULL,
    @NALOGODAVAC nvarchar(25) = NULL,
    @POSTUPAK nvarchar(25) = NULL,
    @UVOZNIK nvarchar(25) = NULL,
    @PLOMBA_UVOZ nvarchar(25) = NULL,
    @VOZ nvarchar(25) = NULL,
    @STANJE nvarchar(25) = NULL,
    @GATE_IN_EF datetime = NULL,
    @PREUZIMANJE_PUNOG datetime = NULL,
    @VRACANJE_PRAZNOG datetime = NULL,
    @KONACNI_GATE_OUT datetime = NULL,
    @BOOKING nvarchar(25) = NULL,
    @KLIJENT nvarchar(25) = NULL,
    @GATE_OUT_EMPTY_UTOVAR datetime = NULL,
    @GATE_IN_FULL_UTOVAR datetime = NULL,
    @GATE_IN_GATE_OUT nvarchar(50) = NULL,
    @LR nvarchar(25) = NULL,
    @TARA nvarchar(25) = NULL,
    @MAX nvarchar(25) = NULL,
    @VOZILO nvarchar(25) = NULL,
    @PLOMBA nvarchar(25) = NULL,
    @NAPOMENA nvarchar(100) = NULL,
    @OPIS nvarchar(100) = NULL,
    @OTPREMA nvarchar(25) = NULL,
    @POSLATE_SLIKE nvarchar(25) = NULL,
    @PREVOZNIK nvarchar(25) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.TerminalPriv
    (
        KONTEJNER, STATUS, POZICIJA, VRSTA, BRODAR, NALOGODAVAC, POSTUPAK, UVOZNIK, PLOMBA_UVOZ, VOZ, STANJE,
        [GATE_IN_E/F], PREUZIMANJE_PUNOG, [VRAĆANJE_PRAZNOG], [Konačni_GATE_OUT], BOOKING, KLIJENT,
        [GATE_OUT_EMPTY Utovar], [GATE_IN_FULL Utovar], [GATE_IN/GATE_OUT], [L/R], TARA, [MAX], VOZILO, PLOMBA,
        NAPOMENA, OPIS, OTPREMA, [POSLATE SLIKE], Prevoznik
    )
    VALUES
    (
        @KONTEJNER, @STATUS, @POZICIJA, @VRSTA, @BRODAR, @NALOGODAVAC, @POSTUPAK, @UVOZNIK, @PLOMBA_UVOZ, @VOZ, @STANJE,
        @GATE_IN_EF, @PREUZIMANJE_PUNOG, @VRACANJE_PRAZNOG, @KONACNI_GATE_OUT, @BOOKING, @KLIJENT,
        @GATE_OUT_EMPTY_UTOVAR, @GATE_IN_FULL_UTOVAR, @GATE_IN_GATE_OUT, @LR, @TARA, @MAX, @VOZILO, @PLOMBA,
        @NAPOMENA, @OPIS, @OTPREMA, @POSLATE_SLIKE, @PREVOZNIK
    );

    SELECT CAST(SCOPE_IDENTITY() AS int) AS ID;
END
GO

IF OBJECT_ID('dbo.updTerminalPriv', 'P') IS NOT NULL DROP PROCEDURE dbo.updTerminalPriv;
GO
CREATE PROCEDURE dbo.updTerminalPriv
    @ID int,
    @KONTEJNER nvarchar(25),
    @STATUS nvarchar(25) = NULL,
    @POZICIJA nvarchar(25) = NULL,
    @VRSTA nvarchar(25) = NULL,
    @BRODAR nvarchar(25) = NULL,
    @NALOGODAVAC nvarchar(25) = NULL,
    @POSTUPAK nvarchar(25) = NULL,
    @UVOZNIK nvarchar(25) = NULL,
    @PLOMBA_UVOZ nvarchar(25) = NULL,
    @VOZ nvarchar(25) = NULL,
    @STANJE nvarchar(25) = NULL,
    @GATE_IN_EF datetime = NULL,
    @PREUZIMANJE_PUNOG datetime = NULL,
    @VRACANJE_PRAZNOG datetime = NULL,
    @KONACNI_GATE_OUT datetime = NULL,
    @BOOKING nvarchar(25) = NULL,
    @KLIJENT nvarchar(25) = NULL,
    @GATE_OUT_EMPTY_UTOVAR datetime = NULL,
    @GATE_IN_FULL_UTOVAR datetime = NULL,
    @GATE_IN_GATE_OUT nvarchar(50) = NULL,
    @LR nvarchar(25) = NULL,
    @TARA nvarchar(25) = NULL,
    @MAX nvarchar(25) = NULL,
    @VOZILO nvarchar(25) = NULL,
    @PLOMBA nvarchar(25) = NULL,
    @NAPOMENA nvarchar(100) = NULL,
    @OPIS nvarchar(100) = NULL,
    @OTPREMA nvarchar(25) = NULL,
    @POSLATE_SLIKE nvarchar(25) = NULL,
    @PREVOZNIK nvarchar(25) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.TerminalPriv
    SET KONTEJNER = @KONTEJNER,
        STATUS = @STATUS,
        POZICIJA = @POZICIJA,
        VRSTA = @VRSTA,
        BRODAR = @BRODAR,
        NALOGODAVAC = @NALOGODAVAC,
        POSTUPAK = @POSTUPAK,
        UVOZNIK = @UVOZNIK,
        PLOMBA_UVOZ = @PLOMBA_UVOZ,
        VOZ = @VOZ,
        STANJE = @STANJE,
        [GATE_IN_E/F] = @GATE_IN_EF,
        PREUZIMANJE_PUNOG = @PREUZIMANJE_PUNOG,
        [VRAĆANJE_PRAZNOG] = @VRACANJE_PRAZNOG,
        [Konačni_GATE_OUT] = @KONACNI_GATE_OUT,
        BOOKING = @BOOKING,
        KLIJENT = @KLIJENT,
        [GATE_OUT_EMPTY Utovar] = @GATE_OUT_EMPTY_UTOVAR,
        [GATE_IN_FULL Utovar] = @GATE_IN_FULL_UTOVAR,
        [GATE_IN/GATE_OUT] = @GATE_IN_GATE_OUT,
        [L/R] = @LR,
        TARA = @TARA,
        [MAX] = @MAX,
        VOZILO = @VOZILO,
        PLOMBA = @PLOMBA,
        NAPOMENA = @NAPOMENA,
        OPIS = @OPIS,
        OTPREMA = @OTPREMA,
        [POSLATE SLIKE] = @POSLATE_SLIKE,
        Prevoznik = @PREVOZNIK
    WHERE ID = @ID;
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
-- Excel kolona C -> KONTEJNER, D -> VRSTA, K -> BRODAR, L -> NALOGODAVAC, T -> POSTUPAK, M -> UVOZNIK, H -> PLOMBA_UVOZ
IF OBJECT_ID('dbo.insTerminalPrivFromExcel', 'P') IS NOT NULL DROP PROCEDURE dbo.insTerminalPrivFromExcel;
GO
CREATE PROCEDURE dbo.insTerminalPrivFromExcel
    @KONTEJNER nvarchar(25),
    @VRSTA nvarchar(25) = NULL,
    @BRODAR nvarchar(25) = NULL,
    @NALOGODAVAC nvarchar(25) = NULL,
    @POSTUPAK nvarchar(25) = NULL,
    @UVOZNIK nvarchar(25) = NULL,
    @PLOMBA_UVOZ nvarchar(25) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.TerminalPriv (KONTEJNER, VRSTA, BRODAR, NALOGODAVAC, POSTUPAK, UVOZNIK, PLOMBA_UVOZ)
    VALUES (@KONTEJNER, @VRSTA, @BRODAR, @NALOGODAVAC, @POSTUPAK, @UVOZNIK, @PLOMBA_UVOZ);
END
GO
