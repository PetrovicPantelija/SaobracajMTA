-- PRODUKCIJA (novembar 2026): Podešavanja - korisnik i lozinka za deljene foldere na serveru.
-- Program se njima prijavljuje na server kad Windows nema pristup folderu (Terminal privremeni: Zakači slike, Zakači dokumenta,
-- Izvezi slike i dokumenta, kolone Slike i Dokumenta), pa Windows ne traži prijavu.
-- Vrednosti se NE upisuju u ovu skriptu (kod je na GitHub-u): unose se u programu, Administracija > Podešavanja, grupa Mreza.
-- Lozinka se tamo čuva šifrovana. Prazan Mreza.Korisnik = program se ne prijavljuje (radi kao do sada).
-- Skripta se može pustiti više puta.

IF OBJECT_ID('dbo.Podesavanja', 'U') IS NULL
BEGIN
    RAISERROR(N'Tabela dbo.Podesavanja ne postoji. Prvo izvršiti HR\HR_Modul_Faza2_2026_10.sql. Skripta je prekinuta.', 16, 1);
    SET NOEXEC ON;
END
GO

INSERT dbo.Podesavanja (Grupa, Kljuc, Vrednost, TipVrednosti, Opis)
SELECT p.Grupa, p.Kljuc, NULL, p.Tip, p.Opis
FROM (VALUES
    (N'Mreza', N'Mreza.Korisnik', 'TEXT',     N'Korisnik za deljene foldere na serveru (slike i dokumenta); može i server\korisnik'),
    (N'Mreza', N'Mreza.Lozinka',  'PASSWORD', N'Lozinka korisnika za deljene foldere (čuva se šifrovana)')
) p (Grupa, Kljuc, Tip, Opis)
WHERE NOT EXISTS (SELECT 1 FROM dbo.Podesavanja x WHERE x.Kljuc = p.Kljuc);
GO

SET NOEXEC OFF;
GO
