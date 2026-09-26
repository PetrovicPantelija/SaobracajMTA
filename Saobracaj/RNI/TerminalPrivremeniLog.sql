-- Log izmena tabele TerminalPriv: tabela TerminalPrivremeniLog + trigger trg_TerminalPriv_Log
-- Forma: Saobracaj.RNI.frmTerminalPrivremeniLog. Skriptu izvrsiti nad bazom aplikacije (posle TerminalPriv.sql).

IF OBJECT_ID('dbo.TerminalPrivremeniLog', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.TerminalPrivremeniLog
    (
        LogID          int IDENTITY(1,1) NOT NULL,
        TerminalPrivID int           NOT NULL,   -- ID zapisa u TerminalPriv
        Datum          datetime      NOT NULL CONSTRAINT DF_TerminalPrivremeniLog_Datum DEFAULT (GETDATE()),
        Akcija         nvarchar(10)  NOT NULL,   -- INSERT / UPDATE / DELETE
        Kontejner      nvarchar(25)  NULL,
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
            CASE WHEN EXISTS (SELECT d.[KONTEJNER] EXCEPT SELECT i.[KONTEJNER]) THEN N'KONTEJNER: ' + ISNULL(CONVERT(nvarchar(200), d.[KONTEJNER]), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(200), i.[KONTEJNER]), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[STATUS] EXCEPT SELECT i.[STATUS]) THEN N'STATUS: ' + ISNULL(CONVERT(nvarchar(200), d.[STATUS]), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(200), i.[STATUS]), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[POZICIJA] EXCEPT SELECT i.[POZICIJA]) THEN N'POZICIJA: ' + ISNULL(CONVERT(nvarchar(200), d.[POZICIJA]), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(200), i.[POZICIJA]), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[VRSTA] EXCEPT SELECT i.[VRSTA]) THEN N'VRSTA: ' + ISNULL(CONVERT(nvarchar(200), d.[VRSTA]), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(200), i.[VRSTA]), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[BRODAR] EXCEPT SELECT i.[BRODAR]) THEN N'BRODAR: ' + ISNULL(CONVERT(nvarchar(200), d.[BRODAR]), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(200), i.[BRODAR]), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[NALOGODAVAC] EXCEPT SELECT i.[NALOGODAVAC]) THEN N'NALOGODAVAC: ' + ISNULL(CONVERT(nvarchar(200), d.[NALOGODAVAC]), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(200), i.[NALOGODAVAC]), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[POSTUPAK] EXCEPT SELECT i.[POSTUPAK]) THEN N'POSTUPAK: ' + ISNULL(CONVERT(nvarchar(200), d.[POSTUPAK]), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(200), i.[POSTUPAK]), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[UVOZNIK] EXCEPT SELECT i.[UVOZNIK]) THEN N'UVOZNIK: ' + ISNULL(CONVERT(nvarchar(200), d.[UVOZNIK]), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(200), i.[UVOZNIK]), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[PLOMBA_UVOZ] EXCEPT SELECT i.[PLOMBA_UVOZ]) THEN N'PLOMBA_UVOZ: ' + ISNULL(CONVERT(nvarchar(200), d.[PLOMBA_UVOZ]), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(200), i.[PLOMBA_UVOZ]), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[VOZ] EXCEPT SELECT i.[VOZ]) THEN N'VOZ: ' + ISNULL(CONVERT(nvarchar(200), d.[VOZ]), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(200), i.[VOZ]), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[STANJE] EXCEPT SELECT i.[STANJE]) THEN N'STANJE: ' + ISNULL(CONVERT(nvarchar(200), d.[STANJE]), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(200), i.[STANJE]), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[GATE_IN_E/F] EXCEPT SELECT i.[GATE_IN_E/F]) THEN N'GATE_IN_E/F: ' + ISNULL(CONVERT(nvarchar(20), d.[GATE_IN_E/F], 120), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(20), i.[GATE_IN_E/F], 120), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[PREUZIMANJE_PUNOG] EXCEPT SELECT i.[PREUZIMANJE_PUNOG]) THEN N'PREUZIMANJE_PUNOG: ' + ISNULL(CONVERT(nvarchar(20), d.[PREUZIMANJE_PUNOG], 120), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(20), i.[PREUZIMANJE_PUNOG], 120), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[VRAĆANJE_PRAZNOG] EXCEPT SELECT i.[VRAĆANJE_PRAZNOG]) THEN N'VRAĆANJE_PRAZNOG: ' + ISNULL(CONVERT(nvarchar(20), d.[VRAĆANJE_PRAZNOG], 120), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(20), i.[VRAĆANJE_PRAZNOG], 120), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[Konačni_GATE_OUT] EXCEPT SELECT i.[Konačni_GATE_OUT]) THEN N'Konačni_GATE_OUT: ' + ISNULL(CONVERT(nvarchar(20), d.[Konačni_GATE_OUT], 120), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(20), i.[Konačni_GATE_OUT], 120), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[BOOKING] EXCEPT SELECT i.[BOOKING]) THEN N'BOOKING: ' + ISNULL(CONVERT(nvarchar(200), d.[BOOKING]), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(200), i.[BOOKING]), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[KLIJENT] EXCEPT SELECT i.[KLIJENT]) THEN N'KLIJENT: ' + ISNULL(CONVERT(nvarchar(200), d.[KLIJENT]), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(200), i.[KLIJENT]), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[GATE_OUT_EMPTY Utovar] EXCEPT SELECT i.[GATE_OUT_EMPTY Utovar]) THEN N'GATE_OUT_EMPTY Utovar: ' + ISNULL(CONVERT(nvarchar(20), d.[GATE_OUT_EMPTY Utovar], 120), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(20), i.[GATE_OUT_EMPTY Utovar], 120), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[GATE_IN_FULL Utovar] EXCEPT SELECT i.[GATE_IN_FULL Utovar]) THEN N'GATE_IN_FULL Utovar: ' + ISNULL(CONVERT(nvarchar(20), d.[GATE_IN_FULL Utovar], 120), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(20), i.[GATE_IN_FULL Utovar], 120), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[GATE_IN/GATE_OUT] EXCEPT SELECT i.[GATE_IN/GATE_OUT]) THEN N'GATE_IN/GATE_OUT: ' + ISNULL(CONVERT(nvarchar(200), d.[GATE_IN/GATE_OUT]), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(200), i.[GATE_IN/GATE_OUT]), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[L/R] EXCEPT SELECT i.[L/R]) THEN N'L/R: ' + ISNULL(CONVERT(nvarchar(200), d.[L/R]), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(200), i.[L/R]), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[TARA] EXCEPT SELECT i.[TARA]) THEN N'TARA: ' + ISNULL(CONVERT(nvarchar(200), d.[TARA]), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(200), i.[TARA]), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[MAX] EXCEPT SELECT i.[MAX]) THEN N'MAX: ' + ISNULL(CONVERT(nvarchar(200), d.[MAX]), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(200), i.[MAX]), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[VOZILO] EXCEPT SELECT i.[VOZILO]) THEN N'VOZILO: ' + ISNULL(CONVERT(nvarchar(200), d.[VOZILO]), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(200), i.[VOZILO]), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[PLOMBA] EXCEPT SELECT i.[PLOMBA]) THEN N'PLOMBA: ' + ISNULL(CONVERT(nvarchar(200), d.[PLOMBA]), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(200), i.[PLOMBA]), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[NAPOMENA] EXCEPT SELECT i.[NAPOMENA]) THEN N'NAPOMENA: ' + ISNULL(CONVERT(nvarchar(200), d.[NAPOMENA]), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(200), i.[NAPOMENA]), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[OPIS] EXCEPT SELECT i.[OPIS]) THEN N'OPIS: ' + ISNULL(CONVERT(nvarchar(200), d.[OPIS]), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(200), i.[OPIS]), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[OTPREMA] EXCEPT SELECT i.[OTPREMA]) THEN N'OTPREMA: ' + ISNULL(CONVERT(nvarchar(200), d.[OTPREMA]), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(200), i.[OTPREMA]), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[POSLATE SLIKE] EXCEPT SELECT i.[POSLATE SLIKE]) THEN N'POSLATE SLIKE: ' + ISNULL(CONVERT(nvarchar(200), d.[POSLATE SLIKE]), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(200), i.[POSLATE SLIKE]), N'(prazno)') + N'; ' ELSE N'' END,
            CASE WHEN EXISTS (SELECT d.[Prevoznik] EXCEPT SELECT i.[Prevoznik]) THEN N'Prevoznik: ' + ISNULL(CONVERT(nvarchar(200), d.[Prevoznik]), N'(prazno)') + N' -> ' + ISNULL(CONVERT(nvarchar(200), i.[Prevoznik]), N'(prazno)') + N'; ' ELSE N'' END
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
        SELECT ID, N'DELETE', KONTEJNER, N'Obrisan zapis' FROM deleted;
    END
END
GO