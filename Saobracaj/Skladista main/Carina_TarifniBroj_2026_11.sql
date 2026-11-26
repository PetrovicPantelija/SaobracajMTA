/* =============================================================================================
   Carina: tarifni broj kao tekst (RAZ-01, RAZ-02, RAZ-03) i provera vlasnika robe (RAZ-06)
   Predlog: Carina-Izlaz-Tarife-Vlasnik-Kontejneri-Knjiga-Predlog.md, verzija 1.1, 26.11.2026.

   Šta radi:
     1. Kolona NHM (tarifni broj) prelazi sa int na varchar(10) u tabelama
          RNCarinskoStavke                      naimenovanja magacinskog predmeta
          RNCarinskoPrijemnicaStavke            stavke prijemnice
          RNCarinskoSkladisteOtpremaStavkePom   stavke otpreme u pripremi
          RNCarinskoSkladisteOtpremnicaStavke   stavke otpremnice
        Vrednost 0 postaje NULL (nije unet). Vodeća nula se od sada čuva (02071100).
     2. U procedurama koje rade sa tim tabelama parametar/promenljiva "@NHM int" postaje
        "@NHM varchar(10)". Tekst procedura nije u repozitorijumu, pa se menja samo taj deo
        teksta, a za svaku proceduru se ispisuje šta je promenjeno.
     3. sp_Prijem_Kontrola: naimenovanje se spaja sa šifarnikom NHM po broju, ne po ID-u (RAZ-03).
     4. Izveštaji za ručnu proveru (ništa se ne menja):
          - tarifni brojevi koji nemaju 8 ili 10 cifara (npr. 7 cifara iz ranijeg pravila ISP-04);
          - stavke prijemnice kojima je NHM verovatno ID iz šifarnika NHM, a ne tarifni broj;
          - vlasnik robe: nalozi čiji vlasnik nije iz VlasniciRobe (Otprema je birala iz Partnerji).

   Program radi i pre i posle skripte: dok su kolone int, tarifni broj se šalje kao broj
   (vodeća nula se gubi, 10 cifara se ne prima).

   Skripta se može pustiti više puta. Pre puštanja napraviti rezervnu kopiju baze.
   Ako za neku tabelu ispiše "PRESKOČENO", tu tabelu treba srediti ručno (indeks ili veza na koloni NHM)
   i skriptu pustiti ponovo.
   Rezultat (poruke i tabele) poslati nazad.
   ============================================================================================= */
SET NOCOUNT ON;

/* ---------------------------------------------------------------------------------------------
   0. Stanje pre izmene: tip kolone NHM u svim tabelama
   --------------------------------------------------------------------------------------------- */
SELECT OBJECT_NAME(c.object_id) AS Tabela, c.name AS Kolona, TYPE_NAME(c.user_type_id) AS Tip,
       c.max_length, c.is_nullable
FROM sys.columns c
JOIN sys.tables t ON t.object_id = c.object_id
WHERE c.name = 'NHM'
ORDER BY 1;
GO

/* ---------------------------------------------------------------------------------------------
   1. Kolona NHM -> varchar(10)
   Podrazumevana vrednost (npr. DEFAULT 0) i automatska statistika na koloni se uklanjaju, jer
   sprečavaju promenu tipa. Indeks, CHECK ili strani ključ na koloni: tabela se preskače.
   --------------------------------------------------------------------------------------------- */
DECLARE @Tabele TABLE (RB int, Naziv sysname);
INSERT @Tabele VALUES (1, N'RNCarinskoStavke'), (2, N'RNCarinskoPrijemnicaStavke'),
                      (3, N'RNCarinskoSkladisteOtpremaStavkePom'), (4, N'RNCarinskoSkladisteOtpremnicaStavke');

DECLARE @rb int = 0, @t sysname, @oid int, @col int, @tip sysname, @sql nvarchar(max), @prepreka nvarchar(max);

WHILE 1 = 1
BEGIN
    SELECT TOP 1 @rb = RB, @t = Naziv FROM @Tabele WHERE RB > @rb ORDER BY RB;
    IF @@ROWCOUNT = 0 BREAK;

    SET @oid = OBJECT_ID(N'dbo.' + @t, 'U');
    IF @oid IS NULL
    BEGIN
        PRINT 'Tabela dbo.' + @t + ' ne postoji, preskače se.';
        CONTINUE;
    END

    SELECT @col = column_id, @tip = TYPE_NAME(user_type_id) FROM sys.columns WHERE object_id = @oid AND name = 'NHM';
    IF @col IS NULL
    BEGIN
        PRINT 'dbo.' + @t + ' nema kolonu NHM, preskače se.';
        CONTINUE;
    END
    IF @tip LIKE '%char%'
    BEGIN
        PRINT 'dbo.' + @t + '.NHM je već tekst (' + @tip + ').';
        SET @col = NULL;
        CONTINUE;
    END

    -- prepreke: indeks, CHECK, strani ključ, izračunata kolona, pogled sa SCHEMABINDING
    SET @prepreka = NULL;
    SELECT @prepreka = ISNULL(@prepreka + ', ', '') + N'indeks ' + i.name
    FROM sys.index_columns ic JOIN sys.indexes i ON i.object_id = ic.object_id AND i.index_id = ic.index_id
    WHERE ic.object_id = @oid AND ic.column_id = @col;
    SELECT @prepreka = ISNULL(@prepreka + ', ', '') + N'CHECK ' + name
    FROM sys.check_constraints WHERE parent_object_id = @oid AND parent_column_id = @col;
    SELECT @prepreka = ISNULL(@prepreka + ', ', '') + N'strani ključ ' + OBJECT_NAME(fkc.constraint_object_id)
    FROM sys.foreign_key_columns fkc
    WHERE (fkc.parent_object_id = @oid AND fkc.parent_column_id = @col)
       OR (fkc.referenced_object_id = @oid AND fkc.referenced_column_id = @col);
    SELECT @prepreka = ISNULL(@prepreka + ', ', '') + N'izračunata kolona ' + cc.name
    FROM sys.computed_columns cc WHERE cc.object_id = @oid AND cc.definition LIKE '%NHM%';
    SELECT @prepreka = ISNULL(@prepreka + ', ', '') + N'pogled ' + OBJECT_NAME(d.referencing_id)
    FROM sys.sql_expression_dependencies d
    JOIN sys.sql_modules m ON m.object_id = d.referencing_id AND m.is_schema_bound = 1
    WHERE d.referenced_id = @oid;

    IF @prepreka IS NOT NULL
    BEGIN
        PRINT 'PRESKOČENO dbo.' + @t + '.NHM: ' + @prepreka + '. Ukloniti pa pustiti skriptu ponovo.';
        SET @col = NULL;
        CONTINUE;
    END

    BEGIN TRY
        BEGIN TRAN;

        -- podrazumevana vrednost
        SET @sql = NULL;
        SELECT @sql = N'ALTER TABLE dbo.' + QUOTENAME(@t) + N' DROP CONSTRAINT ' + QUOTENAME(dc.name) + N';'
        FROM sys.default_constraints dc WHERE dc.parent_object_id = @oid AND dc.parent_column_id = @col;
        IF @sql IS NOT NULL
        BEGIN
            EXEC sp_executesql @sql;
            PRINT 'dbo.' + @t + ': uklonjena podrazumevana vrednost kolone NHM.';
        END

        -- statistika na koloni (automatska se ponovo pravi sama)
        SET @sql = N'';
        SELECT @sql = @sql + N'DROP STATISTICS dbo.' + QUOTENAME(@t) + N'.' + QUOTENAME(s.name) + N';'
        FROM sys.stats s JOIN sys.stats_columns sc ON sc.object_id = s.object_id AND sc.stats_id = s.stats_id
        WHERE s.object_id = @oid AND sc.column_id = @col
          AND NOT EXISTS (SELECT 1 FROM sys.indexes i WHERE i.object_id = s.object_id AND i.index_id = s.stats_id);
        IF @sql <> N''
            EXEC sp_executesql @sql;

        SET @sql = N'ALTER TABLE dbo.' + QUOTENAME(@t) + N' ALTER COLUMN NHM varchar(10) NULL;';
        EXEC sp_executesql @sql;

        SET @sql = N'UPDATE dbo.' + QUOTENAME(@t) + N' SET NHM = NULL WHERE NHM IN (''0'', '''');';
        EXEC sp_executesql @sql;

        COMMIT;
        PRINT 'dbo.' + @t + '.NHM: ' + @tip + ' -> varchar(10).';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK;
        PRINT 'GREŠKA dbo.' + @t + '.NHM: ' + ERROR_MESSAGE();
    END CATCH

    SET @col = NULL;
END
GO

/* ---------------------------------------------------------------------------------------------
   2. Procedure: "@NHM int" -> "@NHM varchar(10)"
   Obuhvata procedure koje pominju @NHM i jednu od četiri tabele. Menja se svako pojavljivanje
   "@NHM <razmak> [AS] int" (parametar, DECLARE, potpis u sp_executesql).
   --------------------------------------------------------------------------------------------- */
DECLARE @Promene TABLE (Procedura sysname, Zamena int, Rezultat nvarchar(4000));
DECLARE @ime sysname, @def nvarchar(max), @pos int, @i int, @n int, @kraj int, @ok bit, @err nvarchar(4000);

DECLARE cur CURSOR LOCAL FAST_FORWARD FOR
    SELECT p.name, m.definition
    FROM sys.procedures p
    JOIN sys.sql_modules m ON m.object_id = p.object_id
    WHERE m.definition LIKE '%@NHM%'
      AND (m.definition LIKE '%RNCarinskoStavke%' OR m.definition LIKE '%RNCarinskoPrijemnicaStavke%'
        OR m.definition LIKE '%RNCarinskoSkladisteOtpremaStavkePom%' OR m.definition LIKE '%RNCarinskoSkladisteOtpremnicaStavke%');

OPEN cur;
FETCH NEXT FROM cur INTO @ime, @def;
WHILE @@FETCH_STATUS = 0
BEGIN
    SET @n = 0;
    SET @pos = CHARINDEX(N'@NHM', @def);
    WHILE @pos > 0
    BEGIN
        SET @i = @pos + 4;
        -- posle @NHM mora razmak (inače je to drugo ime, npr. @NHMBroj)
        IF UNICODE(SUBSTRING(@def, @i, 1)) IN (9, 10, 13, 32)
        BEGIN
            WHILE UNICODE(SUBSTRING(@def, @i, 1)) IN (9, 10, 13, 32) SET @i = @i + 1;
            IF UPPER(SUBSTRING(@def, @i, 2)) = N'AS' AND UNICODE(SUBSTRING(@def, @i + 2, 1)) IN (9, 10, 13, 32)
            BEGIN
                SET @i = @i + 2;
                WHILE UNICODE(SUBSTRING(@def, @i, 1)) IN (9, 10, 13, 32) SET @i = @i + 1;
            END
            IF UPPER(SUBSTRING(@def, @i, 3)) = N'INT'
               AND SUBSTRING(@def, @i + 3, 1) NOT LIKE N'[A-Za-z0-9_]'
            BEGIN
                SET @def = STUFF(@def, @i, 3, N'varchar(10)');
                SET @n = @n + 1;
            END
        END
        SET @pos = CHARINDEX(N'@NHM', @def, @pos + 4);
    END

    IF @n = 0
        INSERT @Promene VALUES (@ime, 0, N'nema "@NHM int" (već promenjeno ili drugačiji zapis): proveriti ručno');
    ELSE
    BEGIN
        -- CREATE PROCEDURE -> ALTER PROCEDURE (prvo CREATE posle kog sledi PROC); CREATE OR ALTER ostaje kakav jeste
        SET @ok = 0;
        SET @pos = CHARINDEX(N'CREATE', @def);
        WHILE @pos > 0 AND @ok = 0
        BEGIN
            SET @i = @pos + 6;
            WHILE UNICODE(SUBSTRING(@def, @i, 1)) IN (9, 10, 13, 32) SET @i = @i + 1;
            IF @i > @pos + 6 AND UPPER(SUBSTRING(@def, @i, 4)) = N'PROC'
            BEGIN
                SET @def = STUFF(@def, @pos, 6, N'ALTER');
                SET @ok = 1;
            END
            ELSE IF @i > @pos + 6 AND UPPER(SUBSTRING(@def, @i, 2)) = N'OR' AND UNICODE(SUBSTRING(@def, @i + 2, 1)) IN (9, 10, 13, 32)
                SET @ok = 1;
            ELSE
                SET @pos = CHARINDEX(N'CREATE', @def, @pos + 6);
        END

        IF @ok = 0
            INSERT @Promene VALUES (@ime, @n, N'GREŠKA: nije nađeno CREATE PROCEDURE; procedura nije promenjena');
        ELSE
        BEGIN
            BEGIN TRY
                EXEC sp_executesql @def;
                INSERT @Promene VALUES (@ime, @n, N'promenjeno');
            END TRY
            BEGIN CATCH
                SET @err = ERROR_MESSAGE();
                INSERT @Promene VALUES (@ime, @n, N'GREŠKA: ' + @err);
            END CATCH
        END
    END
    FETCH NEXT FROM cur INTO @ime, @def;
END
CLOSE cur;
DEALLOCATE cur;

SELECT Procedura, Zamena AS [Broj zamena], Rezultat FROM @Promene ORDER BY Procedura;
GO

/* ---------------------------------------------------------------------------------------------
   3. sp_Prijem_Kontrola: spoj sa šifarnikom NHM po broju (RAZ-03)
   Ista izmena je i u Carina_PrijemnicaPL_2026_11.sql, da je ponovno puštanje te skripte ne vrati.
   --------------------------------------------------------------------------------------------- */
DECLARE @def nvarchar(max) = OBJECT_DEFINITION(OBJECT_ID('dbo.sp_Prijem_Kontrola'));
IF @def IS NULL
    PRINT 'sp_Prijem_Kontrola ne postoji (Carina_PrijemnicaPL_2026_11.sql nije puštena), preskače se.';
ELSE IF CHARINDEX(N'nh.ID = n.NHM', @def) = 0
    PRINT 'sp_Prijem_Kontrola: spoj po ID-u nije nađen (već ispravljeno).';
ELSE
BEGIN
    SET @def = REPLACE(@def, N'nh.ID = n.NHM', N'RTRIM(CONVERT(nvarchar(50), nh.Broj)) = RTRIM(CONVERT(nvarchar(50), n.NHM))');
    -- procedura je napravljena sa CREATE OR ALTER (Carina_PrijemnicaPL_2026_11.sql); inače CREATE -> ALTER
    IF CHARINDEX(N'CREATE OR ALTER', @def) = 0
        SET @def = STUFF(@def, CHARINDEX(N'CREATE', @def), 6, N'ALTER');
    EXEC sp_executesql @def;
    PRINT 'sp_Prijem_Kontrola: naimenovanje se spaja sa šifarnikom NHM po broju.';
END
GO

/* ---------------------------------------------------------------------------------------------
   4. Pogledi koji čitaju ove tabele: osvežavanje opisa kolona (tip NHM)
   --------------------------------------------------------------------------------------------- */
DECLARE @v nvarchar(600);
DECLARE curv CURSOR LOCAL FAST_FORWARD FOR
    SELECT DISTINCT QUOTENAME(SCHEMA_NAME(o.schema_id)) + N'.' + QUOTENAME(o.name)
    FROM sys.sql_expression_dependencies d
    JOIN sys.objects o ON o.object_id = d.referencing_id AND o.type = 'V'
    JOIN sys.sql_modules m ON m.object_id = o.object_id AND m.is_schema_bound = 0
    WHERE d.referenced_id IN (OBJECT_ID('dbo.RNCarinskoStavke'), OBJECT_ID('dbo.RNCarinskoPrijemnicaStavke'),
                              OBJECT_ID('dbo.RNCarinskoSkladisteOtpremaStavkePom'), OBJECT_ID('dbo.RNCarinskoSkladisteOtpremnicaStavke'));
OPEN curv;
FETCH NEXT FROM curv INTO @v;
WHILE @@FETCH_STATUS = 0
BEGIN
    BEGIN TRY
        EXEC sp_refreshview @v;
        PRINT 'Osvežen pogled ' + @v;
    END TRY
    BEGIN CATCH
        PRINT 'GREŠKA pri osvežavanju pogleda ' + @v + ': ' + ERROR_MESSAGE();
    END CATCH
    FETCH NEXT FROM curv INTO @v;
END
CLOSE curv;
DEALLOCATE curv;
GO

/* ---------------------------------------------------------------------------------------------
   5. Stanje posle izmene
   --------------------------------------------------------------------------------------------- */
-- 5.1 tip kolone NHM (ostale tabele sa int NHM ne prate tarifni broj carinskog skladišta; proveriti spisak)
SELECT OBJECT_NAME(c.object_id) AS Tabela, TYPE_NAME(c.user_type_id) AS Tip, c.max_length, c.is_nullable
FROM sys.columns c
JOIN sys.tables t ON t.object_id = c.object_id
WHERE c.name = 'NHM'
ORDER BY 1;

-- 5.2 ostali objekti koji pominju @NHM, a nisu obuhvaćeni (okidači, funkcije, procedure nad drugim tabelama)
SELECT o.name AS Objekat, o.type_desc
FROM sys.sql_modules m
JOIN sys.objects o ON o.object_id = m.object_id
WHERE m.definition LIKE '%@NHM%int%'
  AND (o.type <> 'P' OR NOT (m.definition LIKE '%RNCarinskoStavke%' OR m.definition LIKE '%RNCarinskoPrijemnicaStavke%'
        OR m.definition LIKE '%RNCarinskoSkladisteOtpremaStavkePom%' OR m.definition LIKE '%RNCarinskoSkladisteOtpremnicaStavke%'))
ORDER BY o.type_desc, o.name;
GO

/* ---------------------------------------------------------------------------------------------
   6. RUČNA PROVERA: tarifni brojevi koji nemaju 8 ili 10 cifara
   Najčešće 7 cifara iz ranijeg pravila ISP-04 (npr. 4802562 umesto 48025620) ili izgubljena
   vodeća nula (2071100 umesto 02071100). Program ne može da zna koje od ta dva; ispraviti ručno.
   --------------------------------------------------------------------------------------------- */
IF OBJECT_ID('dbo.RNCarinskoStavke', 'U') IS NOT NULL
    SELECT 'Naimenovanje' AS Gde, s.ID, s.IDNadredjena AS [Magacinski predmet], s.RB,
           RTRIM(CONVERT(nvarchar(200), s.Naimenovanje)) AS Naimenovanje,
           CONVERT(varchar(20), s.NHM) AS [Tarifni broj], LEN(CONVERT(varchar(20), s.NHM)) AS Cifara
    FROM dbo.RNCarinskoStavke s
    WHERE s.NHM IS NOT NULL AND LEN(CONVERT(varchar(20), s.NHM)) NOT IN (8, 10)
      AND CONVERT(varchar(20), s.NHM) <> '0'
    ORDER BY s.IDNadredjena, s.RB;
GO

/* ---------------------------------------------------------------------------------------------
   7. RUČNA PROVERA (RAZ-03): stavke prijemnice kojima je NHM verovatno ID iz šifarnika NHM
   Dugme NHM na Prijemnici je do sada upisivalo ID reda šifarnika. Kandidat: broj kraći od 7 cifara
   koji postoji kao NHM.ID. Predlog je tarifni broj tog reda šifarnika, ili tarifni broj naimenovanja
   kad je stavka vezana za naimenovanje.

   Ispravka: posle pregleda staviti @Primeni = 1 i pustiti samo ovaj deo (od DECLARE do GO).
   Ispravljaju se samo stavke vezane za naimenovanje (predlog sa naimenovanja); ostale ručno.
   --------------------------------------------------------------------------------------------- */
DECLARE @Primeni bit = 0;

IF OBJECT_ID('dbo.RNCarinskoPrijemnicaStavke', 'U') IS NOT NULL AND OBJECT_ID('dbo.NHM', 'U') IS NOT NULL
BEGIN
    DECLARE @imaVezu bit = CASE WHEN COL_LENGTH('dbo.RNCarinskoPrijemnicaStavke', 'IDNaimenovanja') IS NULL THEN 0 ELSE 1 END;
    DECLARE @sql nvarchar(max) = N'
    SELECT s.ID, s.IDNadredjena AS Prijemnica, s.RB, RTRIM(CONVERT(nvarchar(200), s.Naziv)) AS Naziv,
           CONVERT(varchar(20), s.NHM) AS [NHM sada],
           RTRIM(CONVERT(nvarchar(50), nh.Broj)) AS [Broj iz šifarnika za taj ID],
           RTRIM(CONVERT(nvarchar(200), nh.Naziv)) AS [Naziv iz šifarnika],
           ' + CASE WHEN @imaVezu = 1 THEN N'CONVERT(varchar(20), n.NHM)' ELSE N'NULL' END + N' AS [Tarifni broj naimenovanja]
    FROM dbo.RNCarinskoPrijemnicaStavke s
    JOIN dbo.NHM nh ON CONVERT(varchar(20), nh.ID) = CONVERT(varchar(20), s.NHM)
    ' + CASE WHEN @imaVezu = 1 THEN N'LEFT JOIN dbo.RNCarinskoStavke n ON n.ID = s.IDNaimenovanja' ELSE N'' END + N'
    WHERE s.NHM IS NOT NULL AND LEN(CONVERT(varchar(20), s.NHM)) < 7
    ORDER BY s.IDNadredjena, s.RB;';
    EXEC sp_executesql @sql;

    IF @Primeni = 1 AND @imaVezu = 1
    BEGIN
        SET @sql = N'
        UPDATE s SET NHM = n.NHM
        FROM dbo.RNCarinskoPrijemnicaStavke s
        JOIN dbo.NHM nh ON CONVERT(varchar(20), nh.ID) = CONVERT(varchar(20), s.NHM)
        JOIN dbo.RNCarinskoStavke n ON n.ID = s.IDNaimenovanja
        WHERE s.NHM IS NOT NULL AND LEN(CONVERT(varchar(20), s.NHM)) < 7 AND n.NHM IS NOT NULL;
        PRINT ''RAZ-03: ispravljeno stavki prijemnice: '' + CAST(@@ROWCOUNT AS varchar(10));';
        EXEC sp_executesql @sql;
    END
END
GO

/* ---------------------------------------------------------------------------------------------
   8. RUČNA PROVERA (RAZ-06): vlasnik robe na nalozima skladišta
   Vlasnik robe je iz posebne tabele VlasniciRobe (odluka 26.11.2026). Nalog za PRIJEM je već čuvao VlasniciRobe.id;
   Prijemnica, Otpremnica i Otprema su vlasnika birale iz Partnerji, a Otprema je izabranog i snimala (PaSifra).
   Tabela prikazuje naloge čiji vlasnik nije u VlasniciRobe ili se razlikuje od vlasnika na magacinskom predmetu
   istog magacinskog broja. Ništa se ne menja: vlasnika ispraviti na formi.
   --------------------------------------------------------------------------------------------- */
IF OBJECT_ID('dbo.VlasniciRobe', 'U') IS NOT NULL AND OBJECT_ID('dbo.MagacinskiPredmet', 'U') IS NOT NULL
BEGIN
    SELECT rns.ID AS Nalog, rns.VrstaRN, rns.TipRN, rns.MagacinskiBroj, rns.VlasnikRobe AS [Broj u bazi],
           RTRIM(CONVERT(nvarchar(200), vr.Naziv)) AS [Kao VlasniciRobe.id],
           RTRIM(CONVERT(nvarchar(200), pa.PaNaziv)) AS [Kao Partnerji.PaSifra (staro)],
           mpv.VlasnikRobe AS [Vlasnik na magacinskom predmetu],
           RTRIM(CONVERT(nvarchar(200), vm.Naziv)) AS [Naziv na magacinskom predmetu],
           CASE WHEN vr.id IS NULL THEN N'nije u VlasniciRobe' ELSE N'razlikuje se od magacinskog predmeta' END AS Razlog
    FROM dbo.RadniNalogSkladista rns
    LEFT JOIN dbo.VlasniciRobe vr ON vr.id = rns.VlasnikRobe
    LEFT JOIN dbo.Partnerji pa ON pa.PaSifra = rns.VlasnikRobe
    OUTER APPLY (SELECT TOP 1 mp.VlasnikRobe FROM dbo.MagacinskiPredmet mp
                 JOIN dbo.RadniNalogSkladista r2 ON r2.ID = mp.RN
                 WHERE r2.MagacinskiBroj = rns.MagacinskiBroj AND mp.VlasnikRobe IS NOT NULL
                 ORDER BY mp.ID) mpv
    LEFT JOIN dbo.VlasniciRobe vm ON vm.id = mpv.VlasnikRobe
    WHERE rns.VlasnikRobe IS NOT NULL AND rns.VlasnikRobe <> 0 AND ISNULL(rns.MagacinskiBroj, 0) <> 0
      AND (vr.id IS NULL OR (mpv.VlasnikRobe IS NOT NULL AND mpv.VlasnikRobe <> rns.VlasnikRobe))
    ORDER BY rns.ID DESC;
END
GO
