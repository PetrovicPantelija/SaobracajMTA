/* Carina – vlasnik robe na nalogu skladišta (RAZ-06)
   26.11.2026.
   Procedura UpdateRadniNalogSkladistaDirektni upisuje vlasnika robe samo u MagacinskiPredmet, a liste naloga
   (Prijem robe, Otprema robe), Prijemnica, Otprema, Izbor sa lagera i Pregled lagera ga čitaju iz
   RadniNalogSkladista.VlasnikRobe. Zato je na tim ekranima vlasnik bio prazan, a na magacinskom predmetu nije.

   Skripta nalogu upisuje vlasnika njegovog magacinskog predmeta kad je vlasnik na nalogu prazan (NULL, 0) ili ga nema
   u VlasniciRobe (stara šifra iz Partnerji). Nalozi na kojima su oba vlasnika iz VlasniciRobe, a različiti, samo se
   prikazuju: ispraviti ih na formi (Snimi na nalogu za prijem sada sam prenosi vlasnika sa magacinskog predmeta).

   Upotreba: prvo pustiti sa @Upisi = 0 (samo prikaz), proveriti spisak, pa pustiti sa @Upisi = 1.
   Skripta se može pustiti više puta. Radi i na bazi sa nivoom kompatibilnosti 100. */

SET NOCOUNT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;

DECLARE @Upisi bit;
SET @Upisi = 0;   -- 0 = samo prikaz, 1 = upis

IF OBJECT_ID('dbo.VlasniciRobe', 'U') IS NULL OR OBJECT_ID('dbo.MagacinskiPredmet', 'U') IS NULL
BEGIN
    RAISERROR(N'GREŠKA: nema tabele dbo.VlasniciRobe ili dbo.MagacinskiPredmet. Skripta se ne izvršava.', 16, 1);
    RETURN;
END

DECLARE @Promene TABLE
(
    Nalog          int PRIMARY KEY,
    VrstaRN        nvarchar(50),
    TipRN          nvarchar(50),
    MagacinskiBroj int,
    StariVlasnik   int,
    StariKaoPartner nvarchar(200),
    NoviVlasnik    int,
    NoviNaziv      nvarchar(200)
);

INSERT @Promene (Nalog, VrstaRN, TipRN, MagacinskiBroj, StariVlasnik, StariKaoPartner, NoviVlasnik, NoviNaziv)
SELECT rns.ID,
       RTRIM(CONVERT(nvarchar(50), rns.VrstaRN)),
       RTRIM(CONVERT(nvarchar(50), rns.TipRN)),
       rns.MagacinskiBroj,
       rns.VlasnikRobe,
       RTRIM(CONVERT(nvarchar(200), pa.PaNaziv)),
       mp.VlasnikRobe,
       RTRIM(CONVERT(nvarchar(200), vn.Naziv))
FROM dbo.RadniNalogSkladista rns
CROSS APPLY (SELECT TOP 1 m.VlasnikRobe
             FROM dbo.MagacinskiPredmet m
             WHERE m.RN = rns.ID AND m.VlasnikRobe IN (SELECT id FROM dbo.VlasniciRobe)
             ORDER BY m.ID) mp
LEFT JOIN dbo.VlasniciRobe vs ON vs.id = rns.VlasnikRobe
LEFT JOIN dbo.VlasniciRobe vn ON vn.id = mp.VlasnikRobe
LEFT JOIN dbo.Partnerji pa ON pa.PaSifra = rns.VlasnikRobe
WHERE ISNULL(rns.VlasnikRobe, 0) = 0 OR vs.id IS NULL;

/* 1. Nalozi koji dobijaju vlasnika sa magacinskog predmeta */
SELECT Nalog, VrstaRN, TipRN, MagacinskiBroj,
       StariVlasnik AS [Vlasnik na nalogu], StariKaoPartner AS [Kao Partnerji.PaSifra (staro)],
       NoviVlasnik AS [Vlasnik na magacinskom predmetu], NoviNaziv AS [Naziv iz VlasniciRobe]
FROM @Promene
ORDER BY Nalog;

DECLARE @Broj int;
SELECT @Broj = COUNT(*) FROM @Promene;

IF @Upisi = 1
BEGIN
    UPDATE rns SET VlasnikRobe = p.NoviVlasnik
    FROM dbo.RadniNalogSkladista rns
    INNER JOIN @Promene p ON p.Nalog = rns.ID;
    SET @Broj = @@ROWCOUNT;
    PRINT 'Upisan vlasnik na naloga: ' + CAST(@Broj AS varchar(10));
END
ELSE
    PRINT 'Samo prikaz (@Upisi = 0): vlasnika bi dobilo naloga: ' + CAST(@Broj AS varchar(10));

/* 2. Za proveru, ništa se ne menja: oba vlasnika su iz VlasniciRobe, a razlikuju se */
SELECT rns.ID AS Nalog, rns.VrstaRN, rns.TipRN, rns.MagacinskiBroj,
       rns.VlasnikRobe AS [Vlasnik na nalogu], RTRIM(CONVERT(nvarchar(200), vs.Naziv)) AS [Naziv na nalogu],
       mp.VlasnikRobe AS [Vlasnik na magacinskom predmetu], RTRIM(CONVERT(nvarchar(200), vn.Naziv)) AS [Naziv na magacinskom predmetu]
FROM dbo.RadniNalogSkladista rns
CROSS APPLY (SELECT TOP 1 m.VlasnikRobe
             FROM dbo.MagacinskiPredmet m
             WHERE m.RN = rns.ID AND m.VlasnikRobe IN (SELECT id FROM dbo.VlasniciRobe)
             ORDER BY m.ID) mp
INNER JOIN dbo.VlasniciRobe vs ON vs.id = rns.VlasnikRobe
LEFT JOIN dbo.VlasniciRobe vn ON vn.id = mp.VlasnikRobe
WHERE rns.VlasnikRobe <> mp.VlasnikRobe
ORDER BY rns.ID;

PRINT 'Carina_VlasnikRobe_2026_11.sql: završeno.';
GO
