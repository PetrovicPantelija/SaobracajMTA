/* =============================================================================================
   Carina: magacinski broj po godini (broj + godina jedinstveni)   27.11.2026.

   Magacinski broj je redni broj u carinskoj knjizi, a knjiga se vodi po godini. MagacinskiBrojCarinski.ID je
   ključ na koji se vezuju nalozi, magacinski predmeti, promet i otpreme, pa ostaje interni broj. Broj u godini i
   godina su u novoj tabeli dbo.MagacinskiBrojCarinskiGodina (ID -> Godina, Broj; Godina + Broj jedinstveni).
   Kolone se namerno ne dodaju u MagacinskiBrojCarinski: procedure koje upisuju u nju nisu u repozitorijumu.

   Šta radi:
     1. Pravi tabelu dbo.MagacinskiBrojCarinskiGodina.
     2. Postojeći magacinski brojevi zadržavaju broj (Broj = ID); godina je godina magacinskog predmeta, odnosno
        najranijeg carinskog naloga sa tim brojem (bez naloga: tekuća godina).
     3. Okidač na MagacinskiBrojCarinski: nov magacinski broj dobija tekuću godinu i najveći broj te godine + 1;
        obrisan magacinski broj briše se i iz nove tabele.
     4. Funkcija dbo.fnMagacinskiBrojOznaka(ID) vraća oznaku za prikaz „broj/godina“ (npr. 23/2026); program je
        koristi u listama magacinskih brojeva kad postoji.
     5. Ispisuje brojeve kojima je godina dodeljena.

   Posle ove skripte pustiti Carina_Knjiga_2026_11.sql (kolona 1 knjige je broj u godini).
   Skripta se može pustiti više puta. Komercijalni magacinski brojevi (MagacinskiBrojKomercijalni) se ne menjaju.
   ============================================================================================= */
SET NOCOUNT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF OBJECT_ID('dbo.MagacinskiBrojCarinski', 'U') IS NULL
BEGIN
    RAISERROR(N'GREŠKA: tabela dbo.MagacinskiBrojCarinski ne postoji; skripta se ne izvršava.', 16, 1);
    SET NOEXEC ON;
END
GO

IF OBJECT_ID('dbo.MagacinskiBrojCarinskiGodina', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.MagacinskiBrojCarinskiGodina (
        ID      int NOT NULL CONSTRAINT PK_MagacinskiBrojCarinskiGodina PRIMARY KEY,   -- MagacinskiBrojCarinski.ID
        Godina  int NOT NULL,
        Broj    int NOT NULL,
        CONSTRAINT UQ_MagacinskiBrojCarinskiGodina_GodinaBroj UNIQUE (Godina, Broj),
        CONSTRAINT CK_MagacinskiBrojCarinskiGodina_Broj CHECK (Broj > 0)
    );
    PRINT 'Napravljena tabela dbo.MagacinskiBrojCarinskiGodina.';
END
ELSE
    PRINT 'Tabela dbo.MagacinskiBrojCarinskiGodina već postoji.';
GO

-- postojeći brojevi: broj ostaje isti, godina iz magacinskog predmeta / naloga
INSERT dbo.MagacinskiBrojCarinskiGodina (ID, Godina, Broj)
SELECT mb.ID,
       ISNULL((SELECT MIN(mp.Godina)
               FROM dbo.RadniNalogSkladista rns
               JOIN dbo.MagacinskiPredmet mp ON mp.RN = rns.ID
               WHERE rns.MagacinskiBroj = mb.ID AND rns.VrstaRN = 'Carinsko' AND mp.Godina IS NOT NULL),
       ISNULL((SELECT YEAR(MIN(rns.Datum))
               FROM dbo.RadniNalogSkladista rns
               WHERE rns.MagacinskiBroj = mb.ID AND rns.VrstaRN = 'Carinsko'),
              YEAR(GETDATE()))),
       mb.ID
FROM dbo.MagacinskiBrojCarinski mb
WHERE mb.ID > 0
  AND NOT EXISTS (SELECT 1 FROM dbo.MagacinskiBrojCarinskiGodina g WHERE g.ID = mb.ID);
PRINT 'Postojećim magacinskim brojevima dodeljena godina: ' + CONVERT(varchar(10), @@ROWCOUNT) + '.';
GO

CREATE OR ALTER TRIGGER dbo.trg_MagacinskiBrojCarinski_Godina
ON dbo.MagacinskiBrojCarinski
AFTER INSERT, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    DELETE g
    FROM dbo.MagacinskiBrojCarinskiGodina g
    JOIN deleted d ON d.ID = g.ID
    WHERE NOT EXISTS (SELECT 1 FROM inserted i WHERE i.ID = d.ID);

    DECLARE @Godina int = YEAR(GETDATE());
    DECLARE @Max int = ISNULL((SELECT MAX(Broj) FROM dbo.MagacinskiBrojCarinskiGodina WITH (UPDLOCK, HOLDLOCK)
                               WHERE Godina = @Godina), 0);

    INSERT dbo.MagacinskiBrojCarinskiGodina (ID, Godina, Broj)
    SELECT i.ID, @Godina, @Max + ROW_NUMBER() OVER (ORDER BY i.ID)
    FROM inserted i
    WHERE i.ID > 0
      AND NOT EXISTS (SELECT 1 FROM dbo.MagacinskiBrojCarinskiGodina g WHERE g.ID = i.ID);
END
GO
PRINT 'Okidač dbo.trg_MagacinskiBrojCarinski_Godina: nov magacinski broj dobija tekuću godinu i sledeći broj u godini.';
GO

-- oznaka za prikaz: „broj/godina“; magacinski broj bez godine (npr. obrisan red) prikazuje se kao ranije, samim ID
CREATE OR ALTER FUNCTION dbo.fnMagacinskiBrojOznaka (@ID int)
RETURNS nvarchar(20)
AS
BEGIN
    RETURN ISNULL((SELECT CONVERT(nvarchar(10), g.Broj) + N'/' + CONVERT(nvarchar(4), g.Godina)
                   FROM dbo.MagacinskiBrojCarinskiGodina g WHERE g.ID = @ID),
                  CONVERT(nvarchar(20), @ID));
END
GO
PRINT 'Funkcija dbo.fnMagacinskiBrojOznaka napravljena.';
GO

SELECT g.Godina, COUNT(*) AS [Magacinskih brojeva], MIN(g.Broj) AS [Najmanji broj], MAX(g.Broj) AS [Najveći broj]
FROM dbo.MagacinskiBrojCarinskiGodina g
GROUP BY g.Godina
ORDER BY g.Godina;
GO

SET NOEXEC OFF;
GO
