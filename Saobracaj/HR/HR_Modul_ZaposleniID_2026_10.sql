/* =============================================================================================
   Zaposleni.ID -> int IDENTITY NOT NULL + PK_Zaposleni (ispravka posle testa, HR-ISPRAVKE.md tačka 2)

   Problem: u bazama gde je dbo.Zaposleni postojala pre HR modula, ID je bio int NULL bez IDENTITY i bez
   primarnog ključa. Novi zaposleni je dobijao ID = NULL, a nijedan spoljni ključ ka Zaposleni(ID) nije napravljen.

   Pokreće se PRE HR_Modul_2026_10.sql (ta skripta sada proverava stanje i staje ako ID nije ispravan),
   a u bazi gde su HR skripte već puštene: ova skripta, pa ponovo HR_Modul_2026_10.sql i HR_Modul_Faza2_2026_10.sql
   (one prave spoljne ključeve koji ranije nisu mogli da nastanu; ova skripta pravi i one iz obe HR skripte).

   Skripta je idempotentna i ista za sve firme:
     1. ID je već IDENTITY i ima PK            -> ne radi ništa (samo dopuni spoljne ključeve)
     2. ID ima NULL vrednosti ili duplikate     -> prijavi i stani; te redove rešava korisnik
     3. inače: ID postaje IDENTITY NOT NULL uz očuvanje postojećih vrednosti (ALTER TABLE ... SWITCH u novu
        tabelu iste strukture), sa svim kolonama, RowVer, default-ima, check-ovima, indeksima, okidačima,
        pravima i spoljnim ključevima; zatim PK_Zaposleni i DBCC CHECKIDENT tako da sledeći ID bude MAX(ID) + 1.
   Sve je u jednoj transakciji.

   Provera stanja (može se pustiti bilo kad, ništa ne menja):
     SELECT c.is_identity, c.is_nullable,
            (SELECT COUNT(*) FROM sys.key_constraints k WHERE k.parent_object_id = OBJECT_ID('dbo.Zaposleni') AND k.type = 'PK') AS ima_pk
     FROM sys.columns c WHERE c.object_id = OBJECT_ID('dbo.Zaposleni') AND c.name = 'ID';
   ============================================================================================= */

SET NOCOUNT ON;
SET XACT_ABORT ON;
SET QUOTED_IDENTIFIER ON;   -- filtrirani indeksi (UX_Zaposleni_*) i okidač; sqlcmd ga podrazumevano isključuje
SET ANSI_NULLS ON;
GO

IF OBJECT_ID('dbo.Zaposleni', 'U') IS NULL
BEGIN
    RAISERROR(N'Tabela dbo.Zaposleni ne postoji; ova skripta nije potrebna (HR_Modul_2026_10.sql pravi tabelu sa IDENTITY i PK).', 10, 1);
    SET NOEXEC ON;
END
GO

DECLARE @identitet bit = CASE WHEN COLUMNPROPERTY(OBJECT_ID('dbo.Zaposleni'), 'ID', 'IsIdentity') = 1 THEN 1 ELSE 0 END;
DECLARE @pk bit = CASE WHEN EXISTS (
            SELECT 1 FROM sys.key_constraints k
            JOIN sys.index_columns ic ON ic.object_id = k.parent_object_id AND ic.index_id = k.unique_index_id
            JOIN sys.columns c ON c.object_id = ic.object_id AND c.column_id = ic.column_id
            WHERE k.parent_object_id = OBJECT_ID('dbo.Zaposleni') AND k.type = 'PK' AND c.name = 'ID'
              AND (SELECT COUNT(*) FROM sys.index_columns x WHERE x.object_id = k.parent_object_id AND x.index_id = k.unique_index_id AND x.key_ordinal > 0) = 1)
        THEN 1 ELSE 0 END;

IF @identitet = 1 AND @pk = 1
BEGIN
    PRINT N'Zaposleni.ID je već IDENTITY i primarni ključ; tabela se ne menja.';
    RETURN;
END

DECLARE @nulls int = (SELECT COUNT(*) FROM dbo.Zaposleni WHERE ID IS NULL);
DECLARE @duplikati int = (SELECT COUNT(*) FROM (SELECT ID FROM dbo.Zaposleni WHERE ID IS NOT NULL GROUP BY ID HAVING COUNT(*) > 1) x);
IF @nulls > 0 OR @duplikati > 0
BEGIN
    RAISERROR(N'Zaposleni ima %d redova sa ID = NULL i %d dupliranih ID vrednosti. Rešite te redove (npr. SELECT * FROM dbo.Zaposleni WHERE ID IS NULL), pa pokrenite skriptu ponovo. Ništa nije promenjeno.', 16, 1, @nulls, @duplikati);
    SET NOEXEC ON;
    RETURN;
END

-- pogledi sa SCHEMABINDING i indeksirani pogledi sprečavaju zamenu tabele
IF EXISTS (SELECT 1 FROM sys.sql_expression_dependencies d JOIN sys.sql_modules m ON m.object_id = d.referencing_id
           WHERE d.referenced_id = OBJECT_ID('dbo.Zaposleni') AND m.is_schema_bound = 1)
BEGIN
    DECLARE @vezani nvarchar(2000) = STUFF((SELECT N', ' + OBJECT_SCHEMA_NAME(d.referencing_id) + N'.' + OBJECT_NAME(d.referencing_id)
                                           FROM sys.sql_expression_dependencies d JOIN sys.sql_modules m ON m.object_id = d.referencing_id
                                           WHERE d.referenced_id = OBJECT_ID('dbo.Zaposleni') AND m.is_schema_bound = 1
                                           FOR XML PATH('')), 1, 2, N'');
    RAISERROR(N'Objekti sa SCHEMABINDING zavise od dbo.Zaposleni (%s); tabela ne može da se zameni. Ništa nije promenjeno.', 16, 1, @vezani);
    SET NOEXEC ON;
    RETURN;
END

BEGIN TRY
    BEGIN TRAN;

    DECLARE @obj int = OBJECT_ID('dbo.Zaposleni');
    DECLARE @posle TABLE (Red int IDENTITY(1,1), Sql nvarchar(max));   -- naredbe za ponovno pravljenje
    DECLARE @pre   TABLE (Red int IDENTITY(1,1), Sql nvarchar(max));   -- naredbe za uklanjanje pre zamene
    DECLARE @sql nvarchar(max);

    IF @identitet = 1
    BEGIN
        -- ID je već IDENTITY, nedostaje samo PK
        ALTER TABLE dbo.Zaposleni ALTER COLUMN ID int NOT NULL;
    END
    ELSE
    BEGIN
        /* ---- 1. definicije koje treba sačuvati ---- */

        -- spoljni ključevi DRUGIH tabela ka Zaposleni (ako postoje, npr. kad je postojao PK bez IDENTITY)
        INSERT @pre (Sql)
        SELECT N'ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(f.parent_object_id)) + N'.' + QUOTENAME(OBJECT_NAME(f.parent_object_id))
             + N' DROP CONSTRAINT ' + QUOTENAME(f.name) + N';'
        FROM sys.foreign_keys f WHERE f.referenced_object_id = @obj OR f.parent_object_id = @obj;

        -- spoljni ključevi (dolazni i odlazni) za ponovno pravljenje
        INSERT @posle (Sql)
        SELECT N'ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(f.parent_object_id)) + N'.' + QUOTENAME(OBJECT_NAME(f.parent_object_id))
             + CASE WHEN f.is_not_trusted = 1 THEN N' WITH NOCHECK' ELSE N' WITH CHECK' END
             + N' ADD CONSTRAINT ' + QUOTENAME(f.name) + N' FOREIGN KEY ('
             + STUFF((SELECT N', ' + QUOTENAME(c.name) FROM sys.foreign_key_columns fc JOIN sys.columns c ON c.object_id = fc.parent_object_id AND c.column_id = fc.parent_column_id
                      WHERE fc.constraint_object_id = f.object_id ORDER BY fc.constraint_column_id FOR XML PATH('')), 1, 2, N'')
             + N') REFERENCES ' + QUOTENAME(OBJECT_SCHEMA_NAME(f.referenced_object_id)) + N'.' + QUOTENAME(OBJECT_NAME(f.referenced_object_id)) + N' ('
             + STUFF((SELECT N', ' + QUOTENAME(c.name) FROM sys.foreign_key_columns fc JOIN sys.columns c ON c.object_id = fc.referenced_object_id AND c.column_id = fc.referenced_column_id
                      WHERE fc.constraint_object_id = f.object_id ORDER BY fc.constraint_column_id FOR XML PATH('')), 1, 2, N'')
             + N')'
             + CASE f.delete_referential_action WHEN 1 THEN N' ON DELETE CASCADE' WHEN 2 THEN N' ON DELETE SET NULL' WHEN 3 THEN N' ON DELETE SET DEFAULT' ELSE N'' END
             + CASE f.update_referential_action WHEN 1 THEN N' ON UPDATE CASCADE' WHEN 2 THEN N' ON UPDATE SET NULL' WHEN 3 THEN N' ON UPDATE SET DEFAULT' ELSE N'' END
             + N';'
             + CASE WHEN f.is_disabled = 1 THEN N' ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(f.parent_object_id)) + N'.' + QUOTENAME(OBJECT_NAME(f.parent_object_id))
                                                + N' NOCHECK CONSTRAINT ' + QUOTENAME(f.name) + N';' ELSE N'' END
        FROM sys.foreign_keys f WHERE f.referenced_object_id = @obj OR f.parent_object_id = @obj;

        -- default-i
        INSERT @posle (Sql)
        SELECT N'ALTER TABLE dbo.Zaposleni ADD CONSTRAINT ' + QUOTENAME(d.name) + N' DEFAULT ' + d.definition + N' FOR ' + QUOTENAME(c.name) + N';'
        FROM sys.default_constraints d JOIN sys.columns c ON c.object_id = d.parent_object_id AND c.column_id = d.parent_column_id
        WHERE d.parent_object_id = @obj;

        -- check-ovi
        INSERT @posle (Sql)
        SELECT N'ALTER TABLE dbo.Zaposleni ' + CASE WHEN k.is_not_trusted = 1 THEN N'WITH NOCHECK' ELSE N'WITH CHECK' END
             + N' ADD CONSTRAINT ' + QUOTENAME(k.name) + N' CHECK ' + k.definition + N';'
             + CASE WHEN k.is_disabled = 1 THEN N' ALTER TABLE dbo.Zaposleni NOCHECK CONSTRAINT ' + QUOTENAME(k.name) + N';' ELSE N'' END
        FROM sys.check_constraints k WHERE k.parent_object_id = @obj;

        -- indeksi i unikatna ograničenja (osim primarnog ključa; PK_Zaposleni se pravi posebno)
        INSERT @posle (Sql)
        SELECT CASE WHEN i.is_unique_constraint = 1
                    THEN N'ALTER TABLE dbo.Zaposleni ADD CONSTRAINT ' + QUOTENAME(i.name) + N' UNIQUE ' + i.type_desc COLLATE DATABASE_DEFAULT + N' ('
                    ELSE N'CREATE ' + CASE WHEN i.is_unique = 1 THEN N'UNIQUE ' ELSE N'' END + i.type_desc COLLATE DATABASE_DEFAULT
                         + N' INDEX ' + QUOTENAME(i.name) + N' ON dbo.Zaposleni (' END
             + STUFF((SELECT N', ' + QUOTENAME(c.name) + CASE WHEN ic.is_descending_key = 1 THEN N' DESC' ELSE N'' END
                      FROM sys.index_columns ic JOIN sys.columns c ON c.object_id = ic.object_id AND c.column_id = ic.column_id
                      WHERE ic.object_id = i.object_id AND ic.index_id = i.index_id AND ic.key_ordinal > 0 ORDER BY ic.key_ordinal FOR XML PATH('')), 1, 2, N'')
             + N')'
             + ISNULL(N' INCLUDE (' + STUFF((SELECT N', ' + QUOTENAME(c.name)
                      FROM sys.index_columns ic JOIN sys.columns c ON c.object_id = ic.object_id AND c.column_id = ic.column_id
                      WHERE ic.object_id = i.object_id AND ic.index_id = i.index_id AND ic.is_included_column = 1 FOR XML PATH('')), 1, 2, N'') + N')', N'')
             + CASE WHEN i.has_filter = 1 THEN N' WHERE ' + i.filter_definition ELSE N'' END
             + N';'
        FROM sys.indexes i
        WHERE i.object_id = @obj AND i.index_id > 0 AND i.is_primary_key = 0 AND i.is_hypothetical = 0 AND i.type IN (1, 2);

        INSERT @pre (Sql)
        SELECT CASE WHEN i.is_unique_constraint = 1 OR i.is_primary_key = 1
                    THEN N'ALTER TABLE dbo.Zaposleni DROP CONSTRAINT ' + QUOTENAME(i.name) + N';'
                    ELSE N'DROP INDEX ' + QUOTENAME(i.name) + N' ON dbo.Zaposleni;' END
        FROM sys.indexes i
        WHERE i.object_id = @obj AND i.index_id > 0 AND i.is_hypothetical = 0 AND i.type IN (1, 2)
        ORDER BY CASE WHEN i.type = 1 THEN 1 ELSE 0 END;   -- neklasterovani pa klasterovani

        -- okidači (definicija se izvršava ponovo posle zamene tabele)
        INSERT @posle (Sql)
        SELECT m.definition
        FROM sys.triggers t JOIN sys.sql_modules m ON m.object_id = t.object_id
        WHERE t.parent_id = @obj;
        INSERT @posle (Sql)
        SELECT N'DISABLE TRIGGER ' + QUOTENAME(t.name) + N' ON dbo.Zaposleni;'
        FROM sys.triggers t WHERE t.parent_id = @obj AND t.is_disabled = 1;

        -- prava na tabeli
        INSERT @posle (Sql)
        SELECT p.state_desc COLLATE DATABASE_DEFAULT + N' ' + p.permission_name COLLATE DATABASE_DEFAULT + N' ON dbo.Zaposleni TO ' + QUOTENAME(u.name) + N';'
        FROM sys.database_permissions p JOIN sys.database_principals u ON u.principal_id = p.grantee_principal_id
        WHERE p.major_id = @obj AND p.minor_id = 0 AND p.class = 1 AND p.state IN ('G', 'D');

        /* ---- 2. nova tabela iste strukture, ID = IDENTITY ---- */
        IF OBJECT_ID('dbo.Zaposleni_HRNovi', 'U') IS NOT NULL
            DROP TABLE dbo.Zaposleni_HRNovi;

        SELECT @sql = N'CREATE TABLE dbo.Zaposleni_HRNovi (' + STUFF((
            SELECT N', ' + QUOTENAME(c.name) + N' ' +
                   CASE
                     WHEN cc.object_id IS NOT NULL THEN N'AS ' + cc.definition + CASE WHEN cc.is_persisted = 1 THEN N' PERSISTED' ELSE N'' END
                     WHEN c.name = N'ID' THEN N'int IDENTITY(1,1) NOT NULL'
                     WHEN t.name = N'timestamp' THEN N'rowversion'
                     ELSE QUOTENAME(t.name)
                          + CASE WHEN t.name IN (N'nvarchar', N'nchar') THEN N'(' + CASE WHEN c.max_length = -1 THEN N'max' ELSE CAST(c.max_length / 2 AS nvarchar(10)) END + N')'
                                 WHEN t.name IN (N'varchar', N'char', N'varbinary', N'binary') THEN N'(' + CASE WHEN c.max_length = -1 THEN N'max' ELSE CAST(c.max_length AS nvarchar(10)) END + N')'
                                 WHEN t.name IN (N'decimal', N'numeric') THEN N'(' + CAST(c.precision AS nvarchar(5)) + N',' + CAST(c.scale AS nvarchar(5)) + N')'
                                 WHEN t.name IN (N'datetime2', N'time', N'datetimeoffset') THEN N'(' + CAST(c.scale AS nvarchar(5)) + N')'
                                 ELSE N'' END
                          + CASE WHEN c.collation_name IS NOT NULL THEN N' COLLATE ' + c.collation_name ELSE N'' END
                          + CASE WHEN c.is_nullable = 1 THEN N' NULL' ELSE N' NOT NULL' END
                   END
            FROM sys.columns c
            JOIN sys.types t ON t.user_type_id = c.user_type_id
            LEFT JOIN sys.computed_columns cc ON cc.object_id = c.object_id AND cc.column_id = c.column_id
            WHERE c.object_id = @obj
            ORDER BY c.column_id
            FOR XML PATH(''), TYPE).value('.', 'nvarchar(max)'), 1, 2, N'') + N');';
        EXEC sp_executesql @sql;

        /* ---- 3. uklanjanje indeksa i dolaznih FK, ID NOT NULL, zamena sadržaja (SWITCH, bez kopiranja podataka) ---- */
        DECLARE @red int = 1, @n int = (SELECT COUNT(*) FROM @pre);
        WHILE @red <= @n
        BEGIN
            SELECT @sql = Sql FROM @pre WHERE Red = @red;
            EXEC sp_executesql @sql;
            SET @red += 1;
        END

        ALTER TABLE dbo.Zaposleni ALTER COLUMN ID int NOT NULL;
        ALTER TABLE dbo.Zaposleni SWITCH TO dbo.Zaposleni_HRNovi;

        -- stara (sada prazna) tabela odlazi sa svojim default-ima, check-ovima, okidačima i odlaznim FK
        DROP TABLE dbo.Zaposleni;
        EXEC sp_rename N'dbo.Zaposleni_HRNovi', N'Zaposleni';
    END

    /* ---- 4. primarni ključ, brojač, sve sačuvano ---- */
    -- dinamički SQL: naredba se proverava tek kad se izvrši, posle zamene tabele (inače batch pada pri prevođenju jer je ID još NULL)
    IF NOT EXISTS (SELECT 1 FROM sys.key_constraints WHERE parent_object_id = OBJECT_ID('dbo.Zaposleni') AND type = 'PK')
        EXEC (N'ALTER TABLE dbo.Zaposleni ADD CONSTRAINT PK_Zaposleni PRIMARY KEY (ID);');

    -- Sledeći ID mora biti MAX(ID) + 1. Tabela u koju je urađen SWITCH nikad nije imala INSERT (last_value IS NULL);
    -- za takvu tabelu RESEED na N daje prvom sledećem redu baš N, pa se reseed radi na MAX + 1. Inače na MAX.
    DECLARE @max int = ISNULL((SELECT MAX(ID) FROM dbo.Zaposleni), 0);
    IF @max > 0
    BEGIN
        DECLARE @seed int = CASE WHEN (SELECT last_value FROM sys.identity_columns WHERE object_id = OBJECT_ID('dbo.Zaposleni') AND name = 'ID') IS NULL
                                 THEN @max + 1 ELSE @max END;
        EXEC sp_executesql N'DBCC CHECKIDENT (''dbo.Zaposleni'', RESEED, @m) WITH NO_INFOMSGS;', N'@m int', @m = @seed;
    END

    DECLARE @r int = 1, @ukupno int = (SELECT COUNT(*) FROM @posle);
    WHILE @r <= @ukupno
    BEGIN
        SELECT @sql = Sql FROM @posle WHERE Red = @r;
        EXEC sp_executesql @sql;
        SET @r += 1;
    END

    COMMIT;
    PRINT N'Zaposleni.ID je sada IDENTITY sa primarnim ključem PK_Zaposleni; poslednji ID = ' + CAST(@max AS nvarchar(10)) + N'.';
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK;
    DECLARE @poruka nvarchar(4000) = N'Izmena Zaposleni.ID nije uspela, ništa nije promenjeno: ' + ERROR_MESSAGE();
    RAISERROR(@poruka, 16, 1);
    SET NOEXEC ON;
    RETURN;
END CATCH
GO

-- pogledi koji čitaju Zaposleni (i iz drugih modula) se osvežavaju posle zamene tabele
DECLARE @pogled nvarchar(300);
DECLARE p CURSOR LOCAL FAST_FORWARD FOR
    SELECT DISTINCT QUOTENAME(OBJECT_SCHEMA_NAME(d.referencing_id)) + N'.' + QUOTENAME(OBJECT_NAME(d.referencing_id))
    FROM sys.sql_expression_dependencies d JOIN sys.views v ON v.object_id = d.referencing_id
    WHERE d.referenced_id = OBJECT_ID('dbo.Zaposleni');
OPEN p;
FETCH NEXT FROM p INTO @pogled;
WHILE @@FETCH_STATUS = 0
BEGIN
    BEGIN TRY
        EXEC sp_refreshview @pogled;
    END TRY
    BEGIN CATCH
        PRINT N'Pogled ' + @pogled + N' nije osvežen: ' + ERROR_MESSAGE();
    END CATCH
    FETCH NEXT FROM p INTO @pogled;
END
CLOSE p;
DEALLOCATE p;
GO

/* ---------------------------------------------------------------------------------------------
   Spoljni ključevi ka Zaposleni(ID) iz obe HR skripte (ranije nisu mogli da nastanu bez PK).
   Dodaje se samo ključ čija tabela i kolona postoje, a ne postoji već.
   --------------------------------------------------------------------------------------------- */
DECLARE @fk TABLE (Naziv sysname, Tabela sysname, Kolona sysname, RefTabela sysname, RefKolona sysname);
INSERT @fk VALUES
 (N'FK_ZaposleniSlike_Zaposleni',           N'ZaposleniSlike',               N'Zaposleni',             N'Zaposleni', N'ID'),
 (N'FK_ZaposleniRadnaMesta_Zaposleni',      N'ZaposleniRadnaMesta',          N'Zaposleni',             N'Zaposleni', N'ID'),
 (N'FK_ZaposleniUgovori_Zaposleni',         N'ZaposleniUgovori',             N'Zaposleni',             N'Zaposleni', N'ID'),
 (N'FK_ZaposleniMentori_Zaposleni',         N'ZaposleniMentori',             N'Zaposleni',             N'Zaposleni', N'ID'),
 (N'FK_ZaposleniMentori_Mentor',            N'ZaposleniMentori',             N'Mentor',                N'Zaposleni', N'ID'),
 (N'FK_ZaposleniSkolovanje_Zaposleni',      N'ZaposleniSkolovanje',          N'Zaposleni',             N'Zaposleni', N'ID'),
 (N'FK_ZaposleniDokumenti_Zaposleni',       N'ZaposleniDokumenti',           N'Zaposleni',             N'Zaposleni', N'ID'),
 (N'FK_ZaposleniPregledi_Zaposleni',        N'ZaposleniZdravstveniPregledi', N'Zaposleni',             N'Zaposleni', N'ID'),
 (N'FK_HTZReversi_Zaposleni',               N'HTZReversi',                   N'Zaposleni',             N'Zaposleni', N'ID'),
 (N'FK_HTZReversi_Izdao',                   N'HTZReversi',                   N'Izdao',                 N'Zaposleni', N'ID'),
 (N'FK_ZaposleniHTZZaduzenja_Zaposleni',    N'ZaposleniHTZZaduzenja',        N'Zaposleni',             N'Zaposleni', N'ID'),
 (N'FK_ZaposleniHitniKontakti_Zaposleni',   N'ZaposleniHitniKontakti',       N'Zaposleni',             N'Zaposleni', N'ID'),
 (N'FK_ZaposleniPovrede_Zaposleni',         N'ZaposleniPovrede',             N'Zaposleni',             N'Zaposleni', N'ID'),
 (N'FK_ZaposleniDisciplinske_Zaposleni',    N'ZaposleniDisciplinske',        N'Zaposleni',             N'Zaposleni', N'ID'),
 (N'FK_ZaposleniOcene_Zaposleni',           N'ZaposleniOcene',               N'Zaposleni',             N'Zaposleni', N'ID'),
 (N'FK_ZaposleniOcene_Ocenjivac',           N'ZaposleniOcene',               N'Ocenjivac',             N'Zaposleni', N'ID'),
 (N'FK_ZaposleniClanoviPorodice_Zaposleni', N'ZaposleniClanoviPorodice',     N'Zaposleni',             N'Zaposleni', N'ID'),
 (N'FK_ZaposleniPrilozi_Zaposleni',         N'ZaposleniPrilozi',             N'Zaposleni',             N'Zaposleni', N'ID'),
 (N'FK_ZahteviZaNabavku_Podnosilac',        N'ZahteviZaNabavku',             N'Podnosilac',            N'Zaposleni', N'ID'),
 (N'FK_ZahtevOdobrenja_Odobravalac',        N'ZahtevZaNabavkuOdobrenja',     N'Odobravalac',           N'Zaposleni', N'ID'),
 (N'FK_PravilaOdobravanja_Odobravalac',     N'PravilaOdobravanjaNabavke',    N'Odobravalac',           N'Zaposleni', N'ID'),
 (N'FK_PravilaOdobravanja_Zamenik',         N'PravilaOdobravanjaNabavke',    N'Zamenik',               N'Zaposleni', N'ID'),
 (N'FK_Zaposleni_Nadredjeni',               N'Zaposleni',                    N'NadredjeniSifra',       N'Zaposleni', N'ID'),
 (N'FK_EvidencijaRada_Zaposleni',           N'EvidencijaRada',               N'ZaposleniSifra',        N'Zaposleni', N'ID'),
 (N'FK_GODopust_Zaposleni',                 N'GODopust',                     N'ZaposleniSifra',        N'Zaposleni', N'ID'),
 (N'FK_ZaposleniOsnovGO_Zaposleni',         N'ZaposleniOsnovGO',             N'ZaposleniSifra',        N'Zaposleni', N'ID'),
 (N'FK_BeleskaORadu_Zaposleni',             N'BeleskaORadu',                 N'ZaposleniSifra',        N'Zaposleni', N'ID');

DECLARE @naziv sysname, @tab sysname, @kol sysname, @rt sysname, @rk sysname, @s nvarchar(max);
DECLARE f CURSOR LOCAL FAST_FORWARD FOR SELECT Naziv, Tabela, Kolona, RefTabela, RefKolona FROM @fk;
OPEN f;
FETCH NEXT FROM f INTO @naziv, @tab, @kol, @rt, @rk;
WHILE @@FETCH_STATUS = 0
BEGIN
    IF OBJECT_ID(N'dbo.' + @naziv, 'F') IS NULL AND OBJECT_ID(N'dbo.' + @tab, 'U') IS NOT NULL AND COL_LENGTH(N'dbo.' + @tab, @kol) IS NOT NULL
    BEGIN
        -- WITH NOCHECK: postojeći redovi bez para (npr. obrisan zaposleni) ne sprečavaju ključ; novi unosi se proveravaju
        SET @s = N'ALTER TABLE dbo.' + QUOTENAME(@tab) + N' WITH NOCHECK ADD CONSTRAINT ' + QUOTENAME(@naziv)
               + N' FOREIGN KEY (' + QUOTENAME(@kol) + N') REFERENCES dbo.' + QUOTENAME(@rt) + N' (' + QUOTENAME(@rk) + N');';
        BEGIN TRY
            EXEC sp_executesql @s;
            PRINT N'Dodat ' + @naziv;
        END TRY
        BEGIN CATCH
            PRINT N'FK ' + @naziv + N' nije dodat: ' + ERROR_MESSAGE();
        END CATCH
    END
    FETCH NEXT FROM f INTO @naziv, @tab, @kol, @rt, @rk;
END
CLOSE f;
DEALLOCATE f;
GO

SET NOEXEC OFF;
GO

-- stanje posle skripte
SELECT c.is_identity, c.is_nullable,
       (SELECT COUNT(*) FROM sys.key_constraints k WHERE k.parent_object_id = OBJECT_ID('dbo.Zaposleni') AND k.type = 'PK') AS ima_pk,
       (SELECT COUNT(*) FROM sys.foreign_keys f WHERE f.referenced_object_id = OBJECT_ID('dbo.Zaposleni')) AS fk_ka_zaposlenom
FROM sys.columns c WHERE c.object_id = OBJECT_ID('dbo.Zaposleni') AND c.name = 'ID';
GO
