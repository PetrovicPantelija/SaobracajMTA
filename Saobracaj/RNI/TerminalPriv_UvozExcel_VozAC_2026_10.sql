-- PRODUKCIJA (oktobar 2026): Terminal privremeni - Uvoz Excel upisuje i VOZ/kamion iz kolone AC (najava voza, broj voza).
-- Menja samo proceduru insTerminalPrivFromExcel (novi parametar @VOZ_KAMION). Tabela se ne menja.
-- Pustiti PRE instalacije nove verzije programa: nova verzija šalje @VOZ_KAMION, a stara procedura ga ne prima
-- (Uvoz Excel bi javio grešku). Stara verzija programa radi i sa novom procedurom (parametar ima podrazumevanu vrednost).
-- Skripta se može pustiti više puta.

IF OBJECT_ID('dbo.insTerminalPrivFromExcel', 'P') IS NOT NULL DROP PROCEDURE dbo.insTerminalPrivFromExcel;
GO
CREATE PROCEDURE dbo.insTerminalPrivFromExcel
    @KONTEJNER nvarchar(30),
    @VRSTA nvarchar(30) = NULL,
    @BRODAR nvarchar(30) = NULL,
    @NALOGODAVAC_UVOZ nvarchar(50) = NULL,
    @POSTUPAK_UVOZ nvarchar(50) = NULL,
    @UVOZNIK nvarchar(50) = NULL,
    @PLOMBA_UVOZ nvarchar(30) = NULL,
    @VOZ_KAMION nvarchar(30) = NULL      -- kolona AC u Excel-u (najava voza, broj voza)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.TerminalPriv ([KONTEJNER], [VRSTA], [BRODAR], [NALOGODAVAC/UVOZ], [POSTUPAK/UVOZ], [UVOZNIK], [PLOMBA_UVOZ], [VOZ/kamion])
    VALUES (@KONTEJNER, @VRSTA, @BRODAR, @NALOGODAVAC_UVOZ, @POSTUPAK_UVOZ, @UVOZNIK, @PLOMBA_UVOZ, @VOZ_KAMION);
END
GO
