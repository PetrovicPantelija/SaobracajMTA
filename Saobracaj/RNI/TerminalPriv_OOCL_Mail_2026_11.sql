-- PRODUKCIJA (novembar 2026): Terminal privremeni - izveštaj OOCL MAIL.
-- Dodaje kolone ActiveMail i DatumIzvozaMail u TerminalPrivOOCL; postojeći podaci i procedure se ne menjaju.
-- Pustiti PRE instalacije nove verzije programa. Stara verzija programa radi i sa novim kolonama.
-- Skripta se može pustiti više puta.
--
-- Svi postojeći zapisi dobijaju ActiveMail = 0, pa ih prvi izveštaj OOCL MAIL izvozi sve, i one koje je već izvezao OOCL EDI.
-- Ako prvi izveštaj treba da krene samo od novih pokreta, posle skripte pustiti:
--   UPDATE dbo.TerminalPrivOOCL SET ActiveMail = 1 WHERE Active = 1;

-- Izveštaj OOCL MAIL pamti svoj izvoz nezavisno od izveštaja OOCL EDI (Active, DatumIzvoza)
IF COL_LENGTH('dbo.TerminalPrivOOCL', 'ActiveMail') IS NULL
    ALTER TABLE dbo.TerminalPrivOOCL ADD [ActiveMail] int NOT NULL CONSTRAINT DF_TerminalPrivOOCL_ActiveMail DEFAULT (0);
IF COL_LENGTH('dbo.TerminalPrivOOCL', 'DatumIzvozaMail') IS NULL
    ALTER TABLE dbo.TerminalPrivOOCL ADD [DatumIzvozaMail] datetime NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_TerminalPrivOOCL_ActiveMail' AND object_id = OBJECT_ID('dbo.TerminalPrivOOCL'))
    CREATE NONCLUSTERED INDEX IX_TerminalPrivOOCL_ActiveMail ON dbo.TerminalPrivOOCL (ActiveMail);
GO
