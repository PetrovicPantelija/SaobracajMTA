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
            CASE WHEN EXISTS (SELECT d.[Prevoznik] EXCEPT SELECT i.[Prevoznik]) THEN N'Prevoznik: ' + ISNULL(d.[Prevoznik], N'(prazno)') + N' -> ' + ISNULL(i.[Prevoznik], N'(prazno)') + N'; ' ELSE N'' END
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
