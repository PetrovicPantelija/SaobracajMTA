-- Izmena strukture TerminalPriv i TerminalPrivArhiv (septembar 2026):
--   * nova kolona [BL/UVOZ] nvarchar(30)
--   * tekstualne kolone sa 25 na 30 karaktera; UVOZNIK, NALOGODAVAC/UVOZ i POSTUPAK/UVOZ 50; NAPOMENA i OPIS 300
--   * promena naziva kolona (podaci ostaju)
--   * GATE_IN/_GATE_OUT: nove vrednosti NE PRIJAVLJUJEMO POKRETE i PRIJAVITI SVE POKRETE; STANJE: nova vrednost FLEXI
--
-- REDOSLED IZVRSAVANJA nad postojecom bazom:
--   1. TerminalPriv_Izmena_2026_09.sql   (ova skripta: tabele)
--   2. TerminalPriv.sql                  (procedure sa novim kolonama)
--   3. TerminalPrivremeniLog.sql         (trigger loga sa novim kolonama)
--   4. TerminalPrivArhiv.sql             (procedure arhive sa novim kolonama)
-- Sve se radi u jednoj transakciji: ako bilo koji korak pukne, nista se ne menja. Skripta se moze pustiti i vise puta.
-- Trigger loga se ovde brise i ponovo pravi u koraku 3 (do tada se izmene ne loguju).

SET XACT_ABORT ON;
BEGIN TRANSACTION;
GO

IF @@TRANCOUNT = 0 BEGIN PRINT N'Izmena prekinuta zbog greske u prethodnom koraku - nista nije promenjeno.'; SET NOEXEC ON; END
-- 1) trigger, CHECK ogranicenja i indeks koji zavise od kolona koje se menjaju
IF OBJECT_ID('dbo.trg_TerminalPriv_Log', 'TR') IS NOT NULL DROP TRIGGER dbo.trg_TerminalPriv_Log;
IF OBJECT_ID('dbo.CK_TerminalPriv_KONTEJNER', 'C') IS NOT NULL ALTER TABLE dbo.TerminalPriv DROP CONSTRAINT CK_TerminalPriv_KONTEJNER;
IF OBJECT_ID('dbo.CK_TerminalPriv_STATUS', 'C') IS NOT NULL ALTER TABLE dbo.TerminalPriv DROP CONSTRAINT CK_TerminalPriv_STATUS;
IF OBJECT_ID('dbo.CK_TerminalPriv_STANJE', 'C') IS NOT NULL ALTER TABLE dbo.TerminalPriv DROP CONSTRAINT CK_TerminalPriv_STANJE;
IF OBJECT_ID('dbo.CK_TerminalPriv_GATE', 'C') IS NOT NULL ALTER TABLE dbo.TerminalPriv DROP CONSTRAINT CK_TerminalPriv_GATE;
IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_TerminalPrivArhiv_KONTEJNER' AND object_id = OBJECT_ID('dbo.TerminalPrivArhiv'))
    DROP INDEX IX_TerminalPrivArhiv_KONTEJNER ON dbo.TerminalPrivArhiv;
GO

IF @@TRANCOUNT = 0 BEGIN PRINT N'Izmena prekinuta zbog greske u prethodnom koraku - nista nije promenjeno.'; SET NOEXEC ON; END
-- 2) promena naziva kolona: TerminalPriv
IF OBJECT_ID('dbo.TerminalPriv', 'U') IS NOT NULL
BEGIN
    IF COL_LENGTH(N'dbo.TerminalPriv', N'NALOGODAVAC') IS NOT NULL AND COL_LENGTH(N'dbo.TerminalPriv', N'NALOGODAVAC/UVOZ') IS NULL
        EXEC sp_rename N'dbo.TerminalPriv.[NALOGODAVAC]', N'NALOGODAVAC/UVOZ', 'COLUMN';
    IF COL_LENGTH(N'dbo.TerminalPriv', N'POSTUPAK') IS NOT NULL AND COL_LENGTH(N'dbo.TerminalPriv', N'POSTUPAK/UVOZ') IS NULL
        EXEC sp_rename N'dbo.TerminalPriv.[POSTUPAK]', N'POSTUPAK/UVOZ', 'COLUMN';
    IF COL_LENGTH(N'dbo.TerminalPriv', N'VOZ') IS NOT NULL AND COL_LENGTH(N'dbo.TerminalPriv', N'VOZ/kamion') IS NULL
        EXEC sp_rename N'dbo.TerminalPriv.[VOZ]', N'VOZ/kamion', 'COLUMN';
    IF COL_LENGTH(N'dbo.TerminalPriv', N'PREUZIMANJE_PUNOG') IS NOT NULL AND COL_LENGTH(N'dbo.TerminalPriv', N'PREUZIMANJE_PUNOG/Razvoz') IS NULL
        EXEC sp_rename N'dbo.TerminalPriv.[PREUZIMANJE_PUNOG]', N'PREUZIMANJE_PUNOG/Razvoz', 'COLUMN';
    IF COL_LENGTH(N'dbo.TerminalPriv', N'VRAĆANJE_PRAZNOG') IS NOT NULL AND COL_LENGTH(N'dbo.TerminalPriv', N'VRAĆANJE_PRAZNOG/iz_Razvoza') IS NULL
        EXEC sp_rename N'dbo.TerminalPriv.[VRAĆANJE_PRAZNOG]', N'VRAĆANJE_PRAZNOG/iz_Razvoza', 'COLUMN';
    IF COL_LENGTH(N'dbo.TerminalPriv', N'BOOKING') IS NOT NULL AND COL_LENGTH(N'dbo.TerminalPriv', N'BOOKING/IZVOZ') IS NULL
        EXEC sp_rename N'dbo.TerminalPriv.[BOOKING]', N'BOOKING/IZVOZ', 'COLUMN';
    IF COL_LENGTH(N'dbo.TerminalPriv', N'KLIJENT') IS NOT NULL AND COL_LENGTH(N'dbo.TerminalPriv', N'KLIJENT/IZVOZ') IS NULL
        EXEC sp_rename N'dbo.TerminalPriv.[KLIJENT]', N'KLIJENT/IZVOZ', 'COLUMN';
    IF COL_LENGTH(N'dbo.TerminalPriv', N'GATE_OUT_EMPTY Utovar') IS NOT NULL AND COL_LENGTH(N'dbo.TerminalPriv', N'GATE_OUT_EMPTY/Utovar') IS NULL
        EXEC sp_rename N'dbo.TerminalPriv.[GATE_OUT_EMPTY Utovar]', N'GATE_OUT_EMPTY/Utovar', 'COLUMN';
    IF COL_LENGTH(N'dbo.TerminalPriv', N'GATE_IN_FULL Utovar') IS NOT NULL AND COL_LENGTH(N'dbo.TerminalPriv', N'GATE_IN_FULL/sa_Utovara') IS NULL
        EXEC sp_rename N'dbo.TerminalPriv.[GATE_IN_FULL Utovar]', N'GATE_IN_FULL/sa_Utovara', 'COLUMN';
    IF COL_LENGTH(N'dbo.TerminalPriv', N'GATE_IN/GATE_OUT') IS NOT NULL AND COL_LENGTH(N'dbo.TerminalPriv', N'GATE_IN/_GATE_OUT') IS NULL
        EXEC sp_rename N'dbo.TerminalPriv.[GATE_IN/GATE_OUT]', N'GATE_IN/_GATE_OUT', 'COLUMN';
    IF COL_LENGTH(N'dbo.TerminalPriv', N'MAX') IS NOT NULL AND COL_LENGTH(N'dbo.TerminalPriv', N'MAX_NOSIVOST_CNT') IS NULL
        EXEC sp_rename N'dbo.TerminalPriv.[MAX]', N'MAX_NOSIVOST_CNT', 'COLUMN';
    IF COL_LENGTH(N'dbo.TerminalPriv', N'VOZILO') IS NOT NULL AND COL_LENGTH(N'dbo.TerminalPriv', N'VOZILO/PREUZIMANJE') IS NULL
        EXEC sp_rename N'dbo.TerminalPriv.[VOZILO]', N'VOZILO/PREUZIMANJE', 'COLUMN';
    IF COL_LENGTH(N'dbo.TerminalPriv', N'PLOMBA') IS NOT NULL AND COL_LENGTH(N'dbo.TerminalPriv', N'PLOMBA/IZVOZ') IS NULL
        EXEC sp_rename N'dbo.TerminalPriv.[PLOMBA]', N'PLOMBA/IZVOZ', 'COLUMN';
END
GO

IF @@TRANCOUNT = 0 BEGIN PRINT N'Izmena prekinuta zbog greske u prethodnom koraku - nista nije promenjeno.'; SET NOEXEC ON; END
-- 3) nova kolona i nove velicine: TerminalPriv
IF OBJECT_ID('dbo.TerminalPriv', 'U') IS NOT NULL
BEGIN
    IF COL_LENGTH(N'dbo.TerminalPriv', N'BL/UVOZ') IS NULL
        EXEC (N'ALTER TABLE dbo.TerminalPriv ADD [BL/UVOZ] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPriv ALTER COLUMN [KONTEJNER] nvarchar(30) NOT NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPriv ALTER COLUMN [STATUS] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPriv ALTER COLUMN [POZICIJA] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPriv ALTER COLUMN [VRSTA] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPriv ALTER COLUMN [BRODAR] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPriv ALTER COLUMN [NALOGODAVAC/UVOZ] nvarchar(50) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPriv ALTER COLUMN [POSTUPAK/UVOZ] nvarchar(50) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPriv ALTER COLUMN [UVOZNIK] nvarchar(50) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPriv ALTER COLUMN [BL/UVOZ] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPriv ALTER COLUMN [PLOMBA_UVOZ] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPriv ALTER COLUMN [VOZ/kamion] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPriv ALTER COLUMN [STANJE] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPriv ALTER COLUMN [BOOKING/IZVOZ] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPriv ALTER COLUMN [KLIJENT/IZVOZ] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPriv ALTER COLUMN [GATE_IN/_GATE_OUT] nvarchar(50) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPriv ALTER COLUMN [L/R] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPriv ALTER COLUMN [TARA] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPriv ALTER COLUMN [MAX_NOSIVOST_CNT] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPriv ALTER COLUMN [VOZILO/PREUZIMANJE] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPriv ALTER COLUMN [PLOMBA/IZVOZ] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPriv ALTER COLUMN [NAPOMENA] nvarchar(300) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPriv ALTER COLUMN [OPIS] nvarchar(300) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPriv ALTER COLUMN [OTPREMA] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPriv ALTER COLUMN [POSLATE SLIKE] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPriv ALTER COLUMN [Prevoznik] nvarchar(30) NULL');
END
GO

IF @@TRANCOUNT = 0 BEGIN PRINT N'Izmena prekinuta zbog greske u prethodnom koraku - nista nije promenjeno.'; SET NOEXEC ON; END
-- 2) promena naziva kolona: TerminalPrivArhiv
IF OBJECT_ID('dbo.TerminalPrivArhiv', 'U') IS NOT NULL
BEGIN
    IF COL_LENGTH(N'dbo.TerminalPrivArhiv', N'NALOGODAVAC') IS NOT NULL AND COL_LENGTH(N'dbo.TerminalPrivArhiv', N'NALOGODAVAC/UVOZ') IS NULL
        EXEC sp_rename N'dbo.TerminalPrivArhiv.[NALOGODAVAC]', N'NALOGODAVAC/UVOZ', 'COLUMN';
    IF COL_LENGTH(N'dbo.TerminalPrivArhiv', N'POSTUPAK') IS NOT NULL AND COL_LENGTH(N'dbo.TerminalPrivArhiv', N'POSTUPAK/UVOZ') IS NULL
        EXEC sp_rename N'dbo.TerminalPrivArhiv.[POSTUPAK]', N'POSTUPAK/UVOZ', 'COLUMN';
    IF COL_LENGTH(N'dbo.TerminalPrivArhiv', N'VOZ') IS NOT NULL AND COL_LENGTH(N'dbo.TerminalPrivArhiv', N'VOZ/kamion') IS NULL
        EXEC sp_rename N'dbo.TerminalPrivArhiv.[VOZ]', N'VOZ/kamion', 'COLUMN';
    IF COL_LENGTH(N'dbo.TerminalPrivArhiv', N'PREUZIMANJE_PUNOG') IS NOT NULL AND COL_LENGTH(N'dbo.TerminalPrivArhiv', N'PREUZIMANJE_PUNOG/Razvoz') IS NULL
        EXEC sp_rename N'dbo.TerminalPrivArhiv.[PREUZIMANJE_PUNOG]', N'PREUZIMANJE_PUNOG/Razvoz', 'COLUMN';
    IF COL_LENGTH(N'dbo.TerminalPrivArhiv', N'VRAĆANJE_PRAZNOG') IS NOT NULL AND COL_LENGTH(N'dbo.TerminalPrivArhiv', N'VRAĆANJE_PRAZNOG/iz_Razvoza') IS NULL
        EXEC sp_rename N'dbo.TerminalPrivArhiv.[VRAĆANJE_PRAZNOG]', N'VRAĆANJE_PRAZNOG/iz_Razvoza', 'COLUMN';
    IF COL_LENGTH(N'dbo.TerminalPrivArhiv', N'BOOKING') IS NOT NULL AND COL_LENGTH(N'dbo.TerminalPrivArhiv', N'BOOKING/IZVOZ') IS NULL
        EXEC sp_rename N'dbo.TerminalPrivArhiv.[BOOKING]', N'BOOKING/IZVOZ', 'COLUMN';
    IF COL_LENGTH(N'dbo.TerminalPrivArhiv', N'KLIJENT') IS NOT NULL AND COL_LENGTH(N'dbo.TerminalPrivArhiv', N'KLIJENT/IZVOZ') IS NULL
        EXEC sp_rename N'dbo.TerminalPrivArhiv.[KLIJENT]', N'KLIJENT/IZVOZ', 'COLUMN';
    IF COL_LENGTH(N'dbo.TerminalPrivArhiv', N'GATE_OUT_EMPTY Utovar') IS NOT NULL AND COL_LENGTH(N'dbo.TerminalPrivArhiv', N'GATE_OUT_EMPTY/Utovar') IS NULL
        EXEC sp_rename N'dbo.TerminalPrivArhiv.[GATE_OUT_EMPTY Utovar]', N'GATE_OUT_EMPTY/Utovar', 'COLUMN';
    IF COL_LENGTH(N'dbo.TerminalPrivArhiv', N'GATE_IN_FULL Utovar') IS NOT NULL AND COL_LENGTH(N'dbo.TerminalPrivArhiv', N'GATE_IN_FULL/sa_Utovara') IS NULL
        EXEC sp_rename N'dbo.TerminalPrivArhiv.[GATE_IN_FULL Utovar]', N'GATE_IN_FULL/sa_Utovara', 'COLUMN';
    IF COL_LENGTH(N'dbo.TerminalPrivArhiv', N'GATE_IN/GATE_OUT') IS NOT NULL AND COL_LENGTH(N'dbo.TerminalPrivArhiv', N'GATE_IN/_GATE_OUT') IS NULL
        EXEC sp_rename N'dbo.TerminalPrivArhiv.[GATE_IN/GATE_OUT]', N'GATE_IN/_GATE_OUT', 'COLUMN';
    IF COL_LENGTH(N'dbo.TerminalPrivArhiv', N'MAX') IS NOT NULL AND COL_LENGTH(N'dbo.TerminalPrivArhiv', N'MAX_NOSIVOST_CNT') IS NULL
        EXEC sp_rename N'dbo.TerminalPrivArhiv.[MAX]', N'MAX_NOSIVOST_CNT', 'COLUMN';
    IF COL_LENGTH(N'dbo.TerminalPrivArhiv', N'VOZILO') IS NOT NULL AND COL_LENGTH(N'dbo.TerminalPrivArhiv', N'VOZILO/PREUZIMANJE') IS NULL
        EXEC sp_rename N'dbo.TerminalPrivArhiv.[VOZILO]', N'VOZILO/PREUZIMANJE', 'COLUMN';
    IF COL_LENGTH(N'dbo.TerminalPrivArhiv', N'PLOMBA') IS NOT NULL AND COL_LENGTH(N'dbo.TerminalPrivArhiv', N'PLOMBA/IZVOZ') IS NULL
        EXEC sp_rename N'dbo.TerminalPrivArhiv.[PLOMBA]', N'PLOMBA/IZVOZ', 'COLUMN';
END
GO

IF @@TRANCOUNT = 0 BEGIN PRINT N'Izmena prekinuta zbog greske u prethodnom koraku - nista nije promenjeno.'; SET NOEXEC ON; END
-- 3) nova kolona i nove velicine: TerminalPrivArhiv
IF OBJECT_ID('dbo.TerminalPrivArhiv', 'U') IS NOT NULL
BEGIN
    IF COL_LENGTH(N'dbo.TerminalPrivArhiv', N'BL/UVOZ') IS NULL
        EXEC (N'ALTER TABLE dbo.TerminalPrivArhiv ADD [BL/UVOZ] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPrivArhiv ALTER COLUMN [KONTEJNER] nvarchar(30) NOT NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPrivArhiv ALTER COLUMN [STATUS] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPrivArhiv ALTER COLUMN [POZICIJA] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPrivArhiv ALTER COLUMN [VRSTA] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPrivArhiv ALTER COLUMN [BRODAR] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPrivArhiv ALTER COLUMN [NALOGODAVAC/UVOZ] nvarchar(50) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPrivArhiv ALTER COLUMN [POSTUPAK/UVOZ] nvarchar(50) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPrivArhiv ALTER COLUMN [UVOZNIK] nvarchar(50) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPrivArhiv ALTER COLUMN [BL/UVOZ] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPrivArhiv ALTER COLUMN [PLOMBA_UVOZ] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPrivArhiv ALTER COLUMN [VOZ/kamion] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPrivArhiv ALTER COLUMN [STANJE] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPrivArhiv ALTER COLUMN [BOOKING/IZVOZ] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPrivArhiv ALTER COLUMN [KLIJENT/IZVOZ] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPrivArhiv ALTER COLUMN [GATE_IN/_GATE_OUT] nvarchar(50) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPrivArhiv ALTER COLUMN [L/R] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPrivArhiv ALTER COLUMN [TARA] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPrivArhiv ALTER COLUMN [MAX_NOSIVOST_CNT] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPrivArhiv ALTER COLUMN [VOZILO/PREUZIMANJE] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPrivArhiv ALTER COLUMN [PLOMBA/IZVOZ] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPrivArhiv ALTER COLUMN [NAPOMENA] nvarchar(300) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPrivArhiv ALTER COLUMN [OPIS] nvarchar(300) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPrivArhiv ALTER COLUMN [OTPREMA] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPrivArhiv ALTER COLUMN [POSLATE SLIKE] nvarchar(30) NULL');
    EXEC (N'ALTER TABLE dbo.TerminalPrivArhiv ALTER COLUMN [Prevoznik] nvarchar(30) NULL');
END
GO

IF @@TRANCOUNT = 0 BEGIN PRINT N'Izmena prekinuta zbog greske u prethodnom koraku - nista nije promenjeno.'; SET NOEXEC ON; END
-- 4) CHECK ogranicenja sa novim nazivima i vrednostima, indeks arhive, kolona Kontejner u logu
IF OBJECT_ID('dbo.CK_TerminalPriv_KONTEJNER', 'C') IS NULL
    EXEC (N'ALTER TABLE dbo.TerminalPriv ADD CONSTRAINT CK_TerminalPriv_KONTEJNER CHECK (LEN(KONTEJNER) = 11)');
IF OBJECT_ID('dbo.CK_TerminalPriv_STATUS', 'C') IS NULL
    EXEC (N'ALTER TABLE dbo.TerminalPriv ADD CONSTRAINT CK_TerminalPriv_STATUS CHECK (STATUS IN (N''PRAZAN'', N''PUN'', N''?'', N''RAZVOZ'', N''Blanks'', N''PRETOVAR'', N''U RAZVOZU'', N''UTOVAR''))');
IF OBJECT_ID('dbo.CK_TerminalPriv_STANJE', 'C') IS NULL
    EXEC (N'ALTER TABLE dbo.TerminalPriv ADD CONSTRAINT CK_TerminalPriv_STANJE CHECK (STANJE IN (N''DOBAR'', N''LOŠ'', N''FOOD GRADE'', N''OŠTEĆEN'', N''FLEXI''))');
IF OBJECT_ID('dbo.CK_TerminalPriv_GATE', 'C') IS NULL
    EXEC (N'ALTER TABLE dbo.TerminalPriv ADD CONSTRAINT CK_TerminalPriv_GATE CHECK ([GATE_IN/_GATE_OUT] IN (N''GATE IN E'', N''GATE IN F'', N''GATE OUT E'', N''GATE OUT F'', N''GATE IN E/REPOZICIJA'', N''PRIJAVITI GATE IN E'', N''NE ŠALJEMO POKRET BRODARU'', N''REUSE'', N''REPOZICIJA'', N''PRIVREMENO'', N''NE PRIJAVLJUJEMO POKRETE'', N''PRIJAVITI SVE POKRETE''))');
IF OBJECT_ID('dbo.TerminalPrivArhiv', 'U') IS NOT NULL AND NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_TerminalPrivArhiv_KONTEJNER' AND object_id = OBJECT_ID('dbo.TerminalPrivArhiv'))
    EXEC (N'CREATE NONCLUSTERED INDEX IX_TerminalPrivArhiv_KONTEJNER ON dbo.TerminalPrivArhiv (KONTEJNER)');
IF OBJECT_ID('dbo.TerminalPrivremeniLog', 'U') IS NOT NULL
    EXEC (N'ALTER TABLE dbo.TerminalPrivremeniLog ALTER COLUMN Kontejner nvarchar(30) NULL');
GO

IF @@TRANCOUNT = 0 BEGIN PRINT N'Izmena prekinuta zbog greske u prethodnom koraku - nista nije promenjeno.'; SET NOEXEC ON; END
COMMIT TRANSACTION;
PRINT N'Izmena strukture je zavrsena. Sledece: TerminalPriv.sql, TerminalPrivremeniLog.sql, TerminalPrivArhiv.sql.';
GO
SET NOEXEC OFF;
GO
