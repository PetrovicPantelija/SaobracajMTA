-- ANALIZA pre izmene hijerarhije skladišnog prostora (Zona -> Lokacija -> Objekat -> Polje -> Pozicija).
-- Samo čita podatke, ništa ne menja. Pustiti na bazi TESTIRANJE i rezultate (sve tabele) poslati nazad.
SET NOCOUNT ON;

-- 1) Da li je ID IDENTITY u postojećim tabelama
SELECT OBJECT_NAME(c.object_id) AS Tabela, c.name AS Kolona, c.is_identity, TYPE_NAME(c.user_type_id) AS Tip, c.max_length
FROM sys.columns c
WHERE c.object_id IN (OBJECT_ID('dbo.Zone'), OBJECT_ID('dbo.SkladistaGrupa'), OBJECT_ID('dbo.Skladista'), OBJECT_ID('dbo.Pozicija'))
  AND c.name IN ('ID', 'Opis', 'Namena', 'Oznaka')
ORDER BY Tabela, Kolona;

-- 2) Definicije procedura koje koriste forme šifarnika (ID, Datum, Korisnik, brisanje i (de)aktivacija)
SELECT o.name AS Procedura, OBJECT_DEFINITION(o.object_id) AS Definicija
FROM sys.objects o
WHERE o.type = 'P' AND o.name IN (
    'InsertZone', 'UpdateZone', 'DeleteZone', 'AktivirajZonaPozicije', 'DeAktivirajZonaPozicije',
    'InsertSkladistaGrupa', 'UpdateSkladistaGrupa', 'DeleteSkladisteGrupa', 'AktivirajSkladistaGrupa', 'DeAktivirajSkladistaGrupa',
    'InsertSkladista', 'UpdateSkladista', 'DeleteSkladista', 'AktivirajSkladista', 'DeAktivirajSkladista',
    'InsertPozicija', 'UpdatePozicija', 'DeletePozicija', 'AktivirajPozicija', 'DeAktivirajPozicija')
ORDER BY o.name;

-- 3) Objekti u bazi (procedure, view-ovi, funkcije, trigeri) koji koriste kolone iz specifikacije
SELECT OBJECT_NAME(m.object_id) AS Objekat, o.type_desc,
       CASE WHEN m.definition LIKE '%GrupaPoljaID%' THEN 'X' ELSE '' END AS GrupaPoljaID,
       CASE WHEN m.definition LIKE '%TipSkladistaID%' THEN 'X' ELSE '' END AS TipSkladistaID,
       CASE WHEN m.definition LIKE '%Namena%' AND m.definition LIKE '%Pozicija%' THEN 'X' ELSE '' END AS Pozicija_Namena,
       CASE WHEN m.definition LIKE '%Pozicija%' AND m.definition LIKE '%Opis%' THEN 'X' ELSE '' END AS Pozicija_Opis
FROM sys.sql_modules m
INNER JOIN sys.objects o ON o.object_id = m.object_id
WHERE m.definition LIKE '%GrupaPoljaID%'
   OR m.definition LIKE '%TipSkladistaID%'
   OR (m.definition LIKE '%Pozicija%' AND (m.definition LIKE '%Namena%' OR m.definition LIKE '%Opis%'))
ORDER BY Objekat;

-- 4) Šifarnici tipova (vrednost "/" i "CNT" za uslovna polja na formi Polja)
SELECT 'TipSkladista' AS Sifarnik, ID, Naziv FROM dbo.TipSkladista
UNION ALL SELECT 'TipZone', ID, Naziv FROM dbo.TipZone
UNION ALL SELECT 'TipPalete', ID, Naziv FROM dbo.TipPalete
UNION ALL SELECT 'uvKvalitetKontejnera', ID, Naziv FROM dbo.uvKvalitetKontejnera
ORDER BY Sifarnik, ID;
SELECT ID, Naziv, SkNaziv FROM dbo.TipKontenjera WHERE Naziv LIKE '%/%' OR SkNaziv LIKE '%/%' OR SkNaziv = N'' ORDER BY ID;

-- 5) Carinske ispostave: kandidat je tabela Carinarnice (koriste je Izvoz/Uvoz/Drumski)
SELECT c.name AS Kolona, TYPE_NAME(c.user_type_id) AS Tip, c.max_length
FROM sys.columns c WHERE c.object_id = OBJECT_ID('dbo.Carinarnice') ORDER BY c.column_id;
SELECT TOP 20 * FROM dbo.Carinarnice ORDER BY ID;

-- 6) Prava: kartice šifarnika na ekranu Podešavanja sistema (OtvoriFormuSaPravom traži karticu po nazivu)
SELECT k.* FROM dbo.mainNoviKartice k
WHERE k.Naziv IN (N'Zone', N'Lokacije', N'Polja', N'Pozicije', N'Tipovi kontejnera', N'Partneri', N'Objekti', N'Šifarnici', N'Sifarnici')
ORDER BY k.MainID, k.Parent, k.ID;

-- 7) Postojeći podaci (za migraciju)
SELECT (SELECT COUNT(*) FROM dbo.Zone) AS Zone, (SELECT COUNT(*) FROM dbo.SkladistaGrupa) AS Lokacije,
       (SELECT COUNT(*) FROM dbo.Skladista) AS Polja, (SELECT COUNT(*) FROM dbo.Pozicija) AS Pozicije;

-- polja bez ispravne lokacije
SELECT s.ID, s.Oznaka, s.Naziv, s.GrupaPoljaID
FROM dbo.Skladista s LEFT JOIN dbo.SkladistaGrupa g ON g.ID = s.GrupaPoljaID
WHERE g.ID IS NULL ORDER BY s.ID;

-- lokacije sa zonom i tipom (lokacije u nefizičkim zonama, prazan tip)
SELECT g.ID, g.Oznaka, g.Naziv, g.ZonaID, z.Oznaka AS Zona, tz.Naziv AS TipZone, g.TipSkladistaID, ts.Naziv AS TipSkladista, g.Aktivna
FROM dbo.SkladistaGrupa g
LEFT JOIN dbo.Zone z ON z.ID = g.ZonaID
LEFT JOIN dbo.TipZone tz ON tz.ID = z.TipZoneID
LEFT JOIN dbo.TipSkladista ts ON ts.ID = g.TipSkladistaID
ORDER BY g.ID;

-- duple oznake (pravilo "jedinstvena oznaka")
SELECT 'Zone' AS Tabela, Oznaka, COUNT(*) AS Broj FROM dbo.Zone GROUP BY Oznaka HAVING COUNT(*) > 1
UNION ALL SELECT 'SkladistaGrupa', Oznaka, COUNT(*) FROM dbo.SkladistaGrupa GROUP BY Oznaka HAVING COUNT(*) > 1
UNION ALL SELECT 'Skladista', Oznaka, COUNT(*) FROM dbo.Skladista GROUP BY Oznaka HAVING COUNT(*) > 1
UNION ALL SELECT 'Pozicija', Oznaka, COUNT(*) FROM dbo.Pozicija GROUP BY Oznaka HAVING COUNT(*) > 1;

-- najduže vrednosti u kolonama koje se proširuju
SELECT MAX(LEN(Opis)) AS MaxOpis, MAX(LEN(Namena)) AS MaxNamena FROM dbo.Pozicija;

-- aktivna deca ispod neaktivnog roditelja (pravilo "aktivnost i roditelj")
SELECT 'Lokacija u neaktivnoj zoni' AS Problem, g.ID, g.Oznaka FROM dbo.SkladistaGrupa g JOIN dbo.Zone z ON z.ID = g.ZonaID
WHERE ISNULL(g.Aktivna, 0) > 0 AND ISNULL(z.Aktivna, 0) = 0
UNION ALL
SELECT 'Polje u neaktivnoj lokaciji', s.ID, s.Oznaka FROM dbo.Skladista s JOIN dbo.SkladistaGrupa g ON g.ID = s.GrupaPoljaID
WHERE ISNULL(s.Aktivan, 0) > 0 AND ISNULL(g.Aktivna, 0) = 0
UNION ALL
SELECT 'Pozicija u neaktivnom polju', p.ID, p.Oznaka FROM dbo.Pozicija p JOIN dbo.Skladista s ON s.ID = p.Skladiste
WHERE ISNULL(p.Aktivna, 0) > 0 AND ISNULL(s.Aktivan, 0) = 0;
