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
        DatumArhiviranja                   datetime       NOT NULL CONSTRAINT DF_TerminalPrivArhiv_Datum DEFAULT (GETDATE()),
        CONSTRAINT PK_TerminalPrivArhiv PRIMARY KEY CLUSTERED (ID)
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_TerminalPrivArhiv_KONTEJNER' AND object_id = OBJECT_ID('dbo.TerminalPrivArhiv'))
    CREATE NONCLUSTERED INDEX IX_TerminalPrivArhiv_KONTEJNER ON dbo.TerminalPrivArhiv (KONTEJNER);
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
            deleted.ID, deleted.[KONTEJNER], deleted.[STATUS], deleted.[POZICIJA], deleted.[VRSTA], deleted.[BRODAR], deleted.[NALOGODAVAC/UVOZ], deleted.[POSTUPAK/UVOZ], deleted.[UVOZNIK], deleted.[BL/UVOZ], deleted.[PLOMBA_UVOZ], deleted.[VOZ/kamion], deleted.[STANJE], deleted.[GATE_IN_E/F], deleted.[PREUZIMANJE_PUNOG/Razvoz], deleted.[VRAĆANJE_PRAZNOG/iz_Razvoza], deleted.[Konačni_GATE_OUT], deleted.[BOOKING/IZVOZ], deleted.[KLIJENT/IZVOZ], deleted.[GATE_OUT_EMPTY/Utovar], deleted.[GATE_IN_FULL/sa_Utovara], deleted.[GATE_IN/_GATE_OUT], deleted.[L/R], deleted.[TARA], deleted.[MAX_NOSIVOST_CNT], deleted.[VOZILO/PREUZIMANJE], deleted.[PLOMBA/IZVOZ], deleted.[NAPOMENA], deleted.[OPIS], deleted.[OTPREMA], deleted.[POSLATE SLIKE], deleted.[Prevoznik]
        INTO dbo.TerminalPrivArhiv
            (ID, [KONTEJNER], [STATUS], [POZICIJA], [VRSTA], [BRODAR], [NALOGODAVAC/UVOZ], [POSTUPAK/UVOZ], [UVOZNIK], [BL/UVOZ], [PLOMBA_UVOZ], [VOZ/kamion], [STANJE], [GATE_IN_E/F], [PREUZIMANJE_PUNOG/Razvoz], [VRAĆANJE_PRAZNOG/iz_Razvoza], [Konačni_GATE_OUT], [BOOKING/IZVOZ], [KLIJENT/IZVOZ], [GATE_OUT_EMPTY/Utovar], [GATE_IN_FULL/sa_Utovara], [GATE_IN/_GATE_OUT], [L/R], [TARA], [MAX_NOSIVOST_CNT], [VOZILO/PREUZIMANJE], [PLOMBA/IZVOZ], [NAPOMENA], [OPIS], [OTPREMA], [POSLATE SLIKE], [Prevoznik])
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
