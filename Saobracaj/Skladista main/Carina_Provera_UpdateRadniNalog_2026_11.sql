/* Carina – provera procedure UpdateRadniNalogSkladista (samo čitanje, ništa se ne menja)
   26.11.2026.
   Test baza je pri izmeni naloga otpreme javljala: „Procedure or function UpdateRadniNalogSkladista has too many
   arguments specified“. Kod (InsertSkladista.UpdateRadniNalog i Skladista\InsertCarinskoSkladiste.UpdateRadniNalog)
   šalje 51 parametar, isti spisak kao InsertRadniNalogSkladista plus @ID.
   Od ove verzije aplikacija šalje samo parametre koje procedura ima, a poruka posle izmene navodi polja koja nisu
   izmenjena. Skripta pokazuje razliku, da se odluči da li proceduru treba dopuniti.

   1. parametri koje kod šalje, a procedura ih nema (ta polja se pri izmeni naloga ne menjaju)
   2. parametri procedure koje kod ne šalje (moraju imati podrazumevanu vrednost, inače poziv ne radi)
   3. svi parametri procedure, redom
   4. isto poređenje za InsertRadniNalogSkladista, koji radi
   Radi i na bazi sa nivoom kompatibilnosti 100. */

SET NOCOUNT ON;

DECLARE @Kod TABLE (Rbr int PRIMARY KEY, Naziv sysname);
INSERT @Kod (Rbr, Naziv) VALUES
    (1, N'@ID'),
    (2, N'@Status'),
    (3, N'@Datum'),
    (4, N'@Korisnik'),
    (5, N'@VrstaRN'),
    (6, N'@TipRN'),
    (7, N'@CarinskoSkladiste'),
    (8, N'@MagacinskiBroj'),
    (9, N'@Nalogodavac'),
    (10, N'@CarinskiPostupak'),
    (11, N'@OpisPosla'),
    (12, N'@VlasnikRobe'),
    (13, N'@VrstaRobe'),
    (14, N'@NacinPakovanja'),
    (15, N'@OstalaSkladista'),
    (16, N'@PIB'),
    (17, N'@VrstaPrevoznogSredstvaOtprema'),
    (18, N'@VrstaKamionaOtprema'),
    (19, N'@VoziloOtprema'),
    (20, N'@VozacOtprema'),
    (21, N'@BrojLKOtprema'),
    (22, N'@BrojTelefonaOtprema'),
    (23, N'@OdredisnaCarinarnicaOtpremaOtprema'),
    (24, N'@SpediterOtprema'),
    (25, N'@KontakOsobaSpediteraOtprema'),
    (26, N'@MestoIstovaraOtprema'),
    (27, N'@AdresaOtprema'),
    (28, N'@KontaktOsobaIstovarOtprema'),
    (29, N'@PlaniraniDatumOtpema'),
    (30, N'@PlaniraniDatum2Otprema'),
    (31, N'@BrojKontejneraOtprema'),
    (32, N'@VrstaPrevoznogSredstvaPrijem'),
    (33, N'@VrstaKamionaPrijem'),
    (34, N'@VoziloPrijem'),
    (35, N'@VozacPrijem'),
    (36, N'@BrojLKPrijem'),
    (37, N'@BrojTelefonaPrijem'),
    (38, N'@OdredisnaCarinarnicaPrijem'),
    (39, N'@SpediterPrijem'),
    (40, N'@KontakOsobaSpediteraPrijem'),
    (41, N'@MestoIstovaraPrijem'),
    (42, N'@AdresaPrijem'),
    (43, N'@KontaktOsobaIstovarPrijem'),
    (44, N'@PlaniraniDatumPrijem'),
    (45, N'@PlaniraniDatum2Prijem'),
    (46, N'@BrojKontejneraPrijem'),
    (47, N'@PosebniUslovi'),
    (48, N'@DodatneUslugeID'),
    (49, N'@Napomena'),
    (50, N'@Aktivan'),
    (51, N'@Formiran');

IF OBJECT_ID('dbo.UpdateRadniNalogSkladista', 'P') IS NULL
    PRINT 'Procedura dbo.UpdateRadniNalogSkladista ne postoji u ovoj bazi.';

/* 1. Kod šalje, procedura nema */
SELECT k.Rbr, k.Naziv AS [Parametar iz koda koji procedura nema]
FROM @Kod k
WHERE NOT EXISTS (SELECT 1 FROM sys.parameters p
                  WHERE p.object_id = OBJECT_ID('dbo.UpdateRadniNalogSkladista') AND p.name = k.Naziv)
ORDER BY k.Rbr;

/* 2. Procedura ima, kod ne šalje */
SELECT p.parameter_id AS Rbr, p.name AS [Parametar procedure koji kod ne šalje], TYPE_NAME(p.user_type_id) AS Tip
FROM sys.parameters p
WHERE p.object_id = OBJECT_ID('dbo.UpdateRadniNalogSkladista')
  AND NOT EXISTS (SELECT 1 FROM @Kod k WHERE k.Naziv = p.name)
ORDER BY p.parameter_id;

/* 3. Svi parametri procedure */
SELECT p.parameter_id AS Rbr, p.name AS Parametar, TYPE_NAME(p.user_type_id) AS Tip, p.max_length AS Duzina
FROM sys.parameters p
WHERE p.object_id = OBJECT_ID('dbo.UpdateRadniNalogSkladista')
ORDER BY p.parameter_id;

SELECT o.name AS Procedura, o.create_date AS Napravljena, o.modify_date AS [Poslednja izmena],
       (SELECT COUNT(*) FROM sys.parameters p WHERE p.object_id = o.object_id) AS [Broj parametara]
FROM sys.objects o
WHERE o.object_id IN (OBJECT_ID('dbo.UpdateRadniNalogSkladista'), OBJECT_ID('dbo.InsertRadniNalogSkladista'));

/* 4. InsertRadniNalogSkladista: isti spisak bez @ID */
SELECT k.Rbr, k.Naziv AS [Insert: parametar iz koda koji procedura nema]
FROM @Kod k
WHERE k.Naziv <> N'@ID'
  AND NOT EXISTS (SELECT 1 FROM sys.parameters p
                  WHERE p.object_id = OBJECT_ID('dbo.InsertRadniNalogSkladista') AND p.name = k.Naziv)
ORDER BY k.Rbr;
GO
