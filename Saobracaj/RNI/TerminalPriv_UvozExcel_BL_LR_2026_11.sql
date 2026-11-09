-- PRODUKCIJA (novembar 2026): Terminal privremeni - Uvoz Excel: kolona H (BL) ide u BL/UVOZ, kolona E (R/L) u L/R,
-- kolona AC (najava voza) u VOZ/kamion. PLOMBA_UVOZ se više ne uvozi (ranije se punila iz kolone H).
-- Menja samo proceduru insTerminalPrivFromExcel (novi parametri @BL_UVOZ, @L_R, @VOZ_KAMION); zamenjuje
-- TerminalPriv_UvozExcel_VozAC_2026_10.sql. Tabela i postojeći podaci se ne menjaju.
-- Pustiti PRE instalacije nove verzije programa: nova verzija šalje nove parametre, a stara procedura ih ne prima
-- (Uvoz Excel bi javio grešku). Stara verzija programa radi i sa novom procedurom (@PLOMBA_UVOZ je ostao).
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
    @PLOMBA_UVOZ nvarchar(30) = NULL,    -- program ga više ne šalje; ostaje zbog starije verzije programa
    @VOZ_KAMION nvarchar(30) = NULL,     -- kolona AC u Excel-u (najava voza, broj voza)
    @BL_UVOZ nvarchar(30) = NULL,        -- kolona H u Excel-u
    @L_R nvarchar(30) = NULL             -- kolona E u Excel-u (R/L)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.TerminalPriv ([KONTEJNER], [VRSTA], [BRODAR], [NALOGODAVAC/UVOZ], [POSTUPAK/UVOZ], [UVOZNIK], [PLOMBA_UVOZ], [VOZ/kamion], [BL/UVOZ], [L/R])
    VALUES (@KONTEJNER, @VRSTA, @BRODAR, @NALOGODAVAC_UVOZ, @POSTUPAK_UVOZ, @UVOZNIK, @PLOMBA_UVOZ, @VOZ_KAMION, @BL_UVOZ, @L_R);
END
GO
