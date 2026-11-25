/* =============================================================================================
   Nalog za prijem, magacinski predmet: PROVERA (samo čitanje, ništa se ne menja)
   Za tačke 2 (tarifni broj do 10 cifara) i 7 (vrednost garancije) potreban je tekst procedura
   i struktura tabela, jer nisu u repozitorijumu. Rezultate (sve tabele) poslati nazad.

   Pre pokretanja u SSMS: Tools > Options > Query Results > Results to Grid >
   Maximum Characters Retrieved > Non XML data = 65535, da se tekst procedura ne skrati.
   ============================================================================================= */
SET NOCOUNT ON;

/* 1. Procedure za stavke naimenovanja */
SELECT o.name AS Objekat, o.type_desc, OBJECT_DEFINITION(o.object_id) AS Tekst
FROM sys.objects o
WHERE o.name IN ('InsertCarinskoStavke', 'UpdateCarinskoStavke', 'DeleteCarinskoStavke');

/* 2. Okidači na RNCarinskoStavke */
SELECT t.name AS Okidac, t.is_disabled, OBJECT_DEFINITION(t.object_id) AS Tekst
FROM sys.triggers t
WHERE t.parent_id = OBJECT_ID('dbo.RNCarinskoStavke');

/* 3. Kolone RNCarinskoStavke (i izračunate kolone) */
SELECT c.column_id, c.name AS Kolona, TYPE_NAME(c.user_type_id) AS Tip, c.max_length, c.precision, c.scale,
       c.is_nullable, cc.definition AS Izracunata
FROM sys.columns c
LEFT JOIN sys.computed_columns cc ON cc.object_id = c.object_id AND cc.column_id = c.column_id
WHERE c.object_id = OBJECT_ID('dbo.RNCarinskoStavke')
ORDER BY c.column_id;

/* 4. Svi objekti u bazi koji pominju VrednostGarancije ili RNCarinskoStavke */
SELECT o.name AS Objekat, o.type_desc,
       CASE WHEN m.definition LIKE '%VrednostGarancije%' THEN 1 ELSE 0 END AS PominjeGaranciju,
       CASE WHEN m.definition LIKE '%RNCarinskoStavke%' THEN 1 ELSE 0 END AS PominjeStavke
FROM sys.sql_modules m
JOIN sys.objects o ON o.object_id = m.object_id
WHERE m.definition LIKE '%VrednostGarancije%' OR m.definition LIKE '%RNCarinskoStavke%'
ORDER BY o.type_desc, o.name;

/* 5. Kolona NHM u ostalim tabelama (gde tarifni broj ide dalje: prijemnica, otprema, promet) */
SELECT OBJECT_NAME(c.object_id) AS Tabela, c.name AS Kolona, TYPE_NAME(c.user_type_id) AS Tip, c.max_length
FROM sys.columns c
JOIN sys.tables t ON t.object_id = c.object_id
WHERE c.name IN ('NHM', 'TarifniBroj')
ORDER BY 1;

/* 6. Poslednje stavke: vrednost, stope i garancija */
SELECT TOP 10 s.ID, s.IDNadredjena, s.NHM, s.Vrednost, s.Valuta, s.Carina, s.PDV, s.VrednostGarancije
FROM dbo.RNCarinskoStavke s
ORDER BY s.ID DESC;

/* 7. Kolone tabela Valute i MerskeEnote, širina BrojKontejneraPrijem */
SELECT OBJECT_NAME(c.object_id) AS Tabela, c.name AS Kolona, TYPE_NAME(c.user_type_id) AS Tip, c.max_length,
       c.is_nullable, c.is_identity, dc.definition AS Podrazumevano
FROM sys.columns c
LEFT JOIN sys.default_constraints dc ON dc.object_id = c.default_object_id
WHERE c.object_id IN (OBJECT_ID('dbo.Valute'), OBJECT_ID('dbo.MerskeEnote'))
   OR (c.object_id = OBJECT_ID('dbo.RadniNalogSkladista') AND c.name = 'BrojKontejneraPrijem')
ORDER BY 1, c.column_id;
