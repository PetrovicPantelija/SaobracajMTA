/* =============================================================================================
   OrganizacioneJedinice.ID -> int IDENTITY NOT NULL + PK_OrganizacioneJedinice (HR-ISPRAVKE-2.md tačka 2)

   Problem: u bazama gde je dbo.OrganizacioneJedinice postojala pre HR modula (npr. od 2022), ID je int NULL
   bez IDENTITY i bez primarnog ključa. HR šifarnik ne može da doda novu jedinicu (ključ se ne dodeljuje),
   a spoljni ključevi ka OrganizacioneJedinice(ID) iz HR skripti nisu napravljeni.

   Pokreće se PRE HR_Modul_2026_10.sql (ta skripta proverava stanje i staje ako ID nije ispravan),
   a u bazi gde su HR skripte već puštene: samo ova skripta (ona pravi i spoljne ključeve iz HR skripti).
   Isti obrazac kao HR_Modul_ZaposleniID_2026_10.sql.

   Skripta je idempotentna i ista za sve firme:
     1. ID je već IDENTITY i ima PK            -> ne radi ništa (samo dopuni spoljne ključeve)
     2. ID ima NULL vrednosti ili duplikate     -> prijavi i stani; te redove rešava korisnik
     3. inače: ID postaje IDENTITY NOT NULL uz očuvanje postojećih vrednosti (ALTER TABLE ... SWITCH u novu
        tabelu iste strukture), sa svim kolonama, default-ima, check-ovima, indeksima, okidačima, pravima
        i spoljnim ključevima; zatim PK_OrganizacioneJedinice i DBCC CHECKIDENT tako da sledeći ID bude MAX(ID) + 1.
   Sve je u jednoj transakciji.

   PRE PUŠTANJA proveriti procedure starih modula koje upisuju u tabelu (skripta ih ispisuje porukom):
   ako procedura sama zadaje ID (npr. INSERT ... (ID, Naziv, ...) VALUES (MAX(ID) + 1, ...)), posle ove skripte
   javlja grešku "Cannot insert explicit value for identity column" i mora da se prilagodi. Npr.:
     SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.InsertOrganizacionaJedinica'));

   Provera stanja (može se pustiti bilo kad, ništa ne menja):
     SELECT c.is_identity, c.is_nullable,
            (SELECT COUNT(*) FROM sys.key_constraints k WHERE k.parent_object_id = OBJECT_ID('dbo.OrganizacioneJedinice') AND k.type = 'PK') AS ima_pk
     FROM sys.columns c WHERE c.object_id = OBJECT_ID('dbo.OrganizacioneJedinice') AND c.name = 'ID';
   ============================================================================================= */

SET NOCOUNT ON;
SET XACT_ABORT ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
GO

IF OBJECT_ID('dbo.OrganizacioneJedinice', 'U') IS NULL
BEGIN
    RAISERROR(N'Tabela dbo.OrganizacioneJedinice ne postoji; ova skripta nije potrebna (HR_Modul_2026_10.sql pravi tabelu sa IDENTITY i PK).', 10, 1);
    SET NOEXEC ON;
END
GO

-- procedure, funkcije i okidači koji upisuju u tabelu: ne smeju sami da zadaju ID posle prelaska na IDENTITY
DECLARE @upisuju nvarchar(4000) = STUFF((
    SELECT N', ' + OBJECT_SCHEMA_NAME(m.object_id) + N'.' + OBJECT_NAME(m.object_id)
    FROM sys.sql_modules m
    WHERE m.object_id IN (SELECT d.referencing_id FROM sys.sql_expression_dependencies d WHERE d.referenced_id = OBJECT_ID('dbo.OrganizacioneJedinice'))
      AND m.definition LIKE N'%INSERT%OrganizacioneJedinice%'
    FOR XML PATH('')), 1, 2, N'');
IF @upisuju IS NOT NULL
    PRINT N'Upisuju u OrganizacioneJedinice (proveriti da ne zadaju ID): ' + @upisuju;
GO

DECLARE @identitet bit = CASE WHEN COLUMNPROPERTY(OBJECT_ID('dbo.OrganizacioneJedinice'), 'ID', 'IsIdentity') = 1 THEN 1 ELSE 0 END;
DECLARE @pk bit = CASE WHEN EXISTS (
            SELECT 1 FROM sys.key_constraints k
            JOIN sys.index_columns ic ON ic.object_id = k.parent_object_id AND ic.index_id = k.unique_index_id
            JOIN sys.columns c ON c.object_id = ic.object_id AND c.column_id = ic.column_id
            WHERE k.parent_object_id = OBJECT_ID('dbo.OrganizacioneJedinice') AND k.type = 'PK' AND c.name = 'ID'
              AND (SELECT COUNT(*) FROM sys.index_columns x WHERE x.object_id = k.parent_object_id AND x.index_id = k.unique_index_id AND x.key_ordinal > 0) = 1)
        THEN 1 ELSE 0 END;

IF @identitet = 1 AND @pk = 1
BEGIN
    PRINT N'OrganizacioneJedinice.ID je već IDENTITY i primarni ključ; tabela se ne menja.';
    RETURN;
END

DECLARE @nulls int = (SELECT COUNT(*) FROM dbo.OrganizacioneJedinice WHERE ID IS NULL);
DECLARE @duplikati int = (SELECT COUNT(*) FROM (SELECT ID FROM dbo.OrganizacioneJedinice WHERE ID IS NOT NULL GROUP BY ID HAVING COUNT(*) > 1) x);
IF @nulls > 0 OR @duplikati > 0
BEGIN
    RAISERROR(N'OrganizacioneJedinice ima %d redova sa ID = NULL i %d dupliranih ID vrednosti. Rešite te redove (npr. SELECT * FROM dbo.OrganizacioneJedinice WHERE ID IS NULL), pa pokrenite skriptu ponovo. Ništa nije promenjeno.', 16, 1, @nulls, @duplikati);
    SET NOEXEC ON;
    RETURN;
END

-- pogledi sa SCHEMABINDING i indeksirani pogledi sprečavaju zamenu tabele
IF EXISTS (SELECT 1 FROM sys.sql_expression_dependencies d JOIN sys.sql_modules m ON m.object_id = d.referencing_id
           WHERE d.referenced_id = OBJECT_ID('dbo.OrganizacioneJedinice') AND m.is_schema_bound = 1)
BEGIN
    DECLARE @vezani nvarchar(2000) = STUFF((SELECT N', ' + OBJECT_SCHEMA_NAME(d.referencing_id) + N'.' + OBJECT_NAME(d.referencing_id)
                                           FROM sys.sql_expression_dependencies d JOIN sys.sql_modules m ON m.object_id = d.referencing_id
                                           WHERE d.referenced_id = OBJECT_ID('dbo.OrganizacioneJedinice') AND m.is_schema_bound = 1
                                           FOR XML PATH('')), 1, 2, N'');
    RAISERROR(N'Objekti sa SCHEMABINDING zavise od dbo.OrganizacioneJedinice (%s); tabela ne može da se zameni. Ništa nije promenjeno.', 16, 1, @vezani);
    SET NOEXEC ON;
    RETURN;
END

BEGIN TRY
    BEGIN TRAN;

    DECLARE @obj int = OBJECT_ID('dbo.OrganizacioneJedinice');
    DECLARE @posle TABLE (Red int IDENTITY(1,1), Sql nvarchar(max));   -- naredbe za ponovno pravljenje
    DECLARE @pre   TABLE (Red int IDENTITY(1,1), Sql nvarchar(max));   -- naredbe za uklanjanje pre zamene
    DECLARE @sql nvarchar(max);

    IF @identitet = 1
    BEGIN
        -- ID je već IDENTITY, nedostaje samo PK
        ALTER TABLE dbo.OrganizacioneJedinice ALTER COLUMN ID int NOT NULL;
    END
    ELSE
    BEGIN
        /* ---- 1. definicije koje treba sačuvati ---- */

        -- spoljni ključevi (dolazni, odlazni i ka samoj sebi) se uklanjaju pre zamene
        INSERT @pre (Sql)
        SELECT N'ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(f.parent_object_id)) + N'.' + QUOTENAME(OBJECT_NAME(f.parent_object_id))
             + N' DROP CONSTRAINT ' + QUOTENAME(f.name) + N';'
        FROM sys.foreign_keys f WHERE f.referenced_object_id = @obj OR f.parent_object_id = @obj;

        -- i prave ponovo posle zamene
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
        SELECT N'ALTER TABLE dbo.OrganizacioneJedinice ADD CONSTRAINT ' + QUOTENAME(d.name) + N' DEFAULT ' + d.definition + N' FOR ' + QUOTENAME(c.name) + N';'
        FROM sys.default_constraints d JOIN sys.columns c ON c.object_id = d.parent_object_id AND c.column_id = d.parent_column_id
        WHERE d.parent_object_id = @obj;

        -- check-ovi
        INSERT @posle (Sql)
        SELECT N'ALTER TABLE dbo.OrganizacioneJedinice ' + CASE WHEN k.is_not_trusted = 1 THEN N'WITH NOCHECK' ELSE N'WITH CHECK' END
             + N' ADD CONSTRAINT ' + QUOTENAME(k.name) + N' CHECK ' + k.definition + N';'
             + CASE WHEN k.is_disabled = 1 THEN N' ALTER TABLE dbo.OrganizacioneJedinice NOCHECK CONSTRAINT ' + QUOTENAME(k.name) + N';' ELSE N'' END
        FROM sys.check_constraints k WHERE k.parent_object_id = @obj;

        -- indeksi i unikatna ograničenja (osim primarnog ključa; PK_OrganizacioneJedinice se pravi posebno)
        INSERT @posle (Sql)
        SELECT CASE WHEN i.is_unique_constraint = 1
                    THEN N'ALTER TABLE dbo.OrganizacioneJedinice ADD CONSTRAINT ' + QUOTENAME(i.name) + N' UNIQUE ' + i.type_desc COLLATE DATABASE_DEFAULT + N' ('
                    ELSE N'CREATE ' + CASE WHEN i.is_unique = 1 THEN N'UNIQUE ' ELSE N'' END + i.type_desc COLLATE DATABASE_DEFAULT
                         + N' INDEX ' + QUOTENAME(i.name) + N' ON dbo.OrganizacioneJedinice (' END
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
                    THEN N'ALTER TABLE dbo.OrganizacioneJedinice DROP CONSTRAINT ' + QUOTENAME(i.name) + N';'
                    ELSE N'DROP INDEX ' + QUOTENAME(i.name) + N' ON dbo.OrganizacioneJedinice;' END
        FROM sys.indexes i
        WHERE i.object_id = @obj AND i.index_id > 0 AND i.is_hypothetical = 0 AND i.type IN (1, 2)
        ORDER BY CASE WHEN i.type = 1 THEN 1 ELSE 0 END;   -- neklasterovani pa klasterovani

        -- okidači (definicija se izvršava ponovo posle zamene tabele)
        INSERT @posle (Sql)
        SELECT m.definition
        FROM sys.triggers t JOIN sys.sql_modules m ON m.object_id = t.object_id
        WHERE t.parent_id = @obj;
        INSERT @posle (Sql)
        SELECT N'DISABLE TRIGGER ' + QUOTENAME(t.name) + N' ON dbo.OrganizacioneJedinice;'
        FROM sys.triggers t WHERE t.parent_id = @obj AND t.is_disabled = 1;

        -- prava na tabeli
        INSERT @posle (Sql)
        SELECT p.state_desc COLLATE DATABASE_DEFAULT + N' ' + p.permission_name COLLATE DATABASE_DEFAULT + N' ON dbo.OrganizacioneJedinice TO ' + QUOTENAME(u.name) + N';'
        FROM sys.database_permissions p JOIN sys.database_principals u ON u.principal_id = p.grantee_principal_id
        WHERE p.major_id = @obj AND p.minor_id = 0 AND p.class = 1 AND p.state IN ('G', 'D');

        /* ---- 2. nova tabela iste strukture, ID = IDENTITY ---- */
        IF OBJECT_ID('dbo.OrganizacioneJedinice_HRNovi', 'U') IS NOT NULL
            DROP TABLE dbo.OrganizacioneJedinice_HRNovi;

        SELECT @sql = N'CREATE TABLE dbo.OrganizacioneJedinice_HRNovi (' + STUFF((
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

        /* ---- 3. uklanjanje indeksa i FK, ID NOT NULL, zamena sadržaja (SWITCH, bez kopiranja podataka) ---- */
        DECLARE @red int = 1, @n int = (SELECT COUNT(*) FROM @pre);
        WHILE @red <= @n
        BEGIN
            SELECT @sql = Sql FROM @pre WHERE Red = @red;
            EXEC sp_executesql @sql;
            SET @red += 1;
        END

        ALTER TABLE dbo.OrganizacioneJedinice ALTER COLUMN ID int NOT NULL;
        ALTER TABLE dbo.OrganizacioneJedinice SWITCH TO dbo.OrganizacioneJedinice_HRNovi;

        -- stara (sada prazna) tabela odlazi sa svojim default-ima, check-ovima i okidačima
        DROP TABLE dbo.OrganizacioneJedinice;
        EXEC sp_rename N'dbo.OrganizacioneJedinice_HRNovi', N'OrganizacioneJedinice';
    END

    /* ---- 4. primarni ključ, brojač, sve sačuvano ---- */
    -- dinamički SQL: naredba se proverava tek kad se izvrši, posle zamene tabele
    IF NOT EXISTS (SELECT 1 FROM sys.key_constraints WHERE parent_object_id = OBJECT_ID('dbo.OrganizacioneJedinice') AND type = 'PK')
        EXEC (N'ALTER TABLE dbo.OrganizacioneJedinice ADD CONSTRAINT PK_OrganizacioneJedinice PRIMARY KEY (ID);');

    -- Sledeći ID mora biti MAX(ID) + 1. Tabela u koju je urađen SWITCH nikad nije imala INSERT (last_value IS NULL);
    -- za takvu tabelu RESEED na N daje prvom sledećem redu baš N, pa se reseed radi na MAX + 1. Inače na MAX.
    DECLARE @max int = ISNULL((SELECT MAX(ID) FROM dbo.OrganizacioneJedinice), 0);
    IF @max > 0
    BEGIN
        DECLARE @seed int = CASE WHEN (SELECT last_value FROM sys.identity_columns WHERE object_id = OBJECT_ID('dbo.OrganizacioneJedinice') AND name = 'ID') IS NULL
                                 THEN @max + 1 ELSE @max END;
        EXEC sp_executesql N'DBCC CHECKIDENT (''dbo.OrganizacioneJedinice'', RESEED, @m) WITH NO_INFOMSGS;', N'@m int', @m = @seed;
    END

    DECLARE @r int = 1, @ukupno int = (SELECT COUNT(*) FROM @posle);
    WHILE @r <= @ukupno
    BEGIN
        SELECT @sql = Sql FROM @posle WHERE Red = @r;
        EXEC sp_executesql @sql;
        SET @r += 1;
    END

    COMMIT;
    PRINT N'OrganizacioneJedinice.ID je sada IDENTITY sa primarnim ključem PK_OrganizacioneJedinice; poslednji ID = ' + CAST(@max AS nvarchar(10)) + N'.';
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK;
    DECLARE @poruka nvarchar(4000) = N'Izmena OrganizacioneJedinice.ID nije uspela, ništa nije promenjeno: ' + ERROR_MESSAGE();
    RAISERROR(@poruka, 16, 1);
    SET NOEXEC ON;
    RETURN;
END CATCH
GO

-- pogledi koji čitaju OrganizacioneJedinice (i iz drugih modula) se osvežavaju posle zamene tabele
DECLARE @pogled nvarchar(300);
DECLARE p CURSOR LOCAL FAST_FORWARD FOR
    SELECT DISTINCT QUOTENAME(OBJECT_SCHEMA_NAME(d.referencing_id)) + N'.' + QUOTENAME(OBJECT_NAME(d.referencing_id))
    FROM sys.sql_expression_dependencies d JOIN sys.views v ON v.object_id = d.referencing_id
    WHERE d.referenced_id = OBJECT_ID('dbo.OrganizacioneJedinice');
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
   Spoljni ključevi ka OrganizacioneJedinice(ID) iz obe HR skripte (ranije nisu mogli da nastanu bez PK).
   Dodaje se samo ključ čija tabela i kolona postoje, a ne postoji već.
   --------------------------------------------------------------------------------------------- */
DECLARE @fk TABLE (Naziv sysname, Tabela sysname, Kolona sysname);
INSERT @fk VALUES
 (N'FK_OrganizacioneJedinice_Nadredjena', N'OrganizacioneJedinice',     N'NadredjenaJedinica'),
 (N'FK_ZaposleniRadnaMesta_OrgJed',       N'ZaposleniRadnaMesta',       N'OrganizacionaJedinica'),
 (N'FK_ZahteviZaNabavku_OrgJed',          N'ZahteviZaNabavku',          N'OrganizacionaJedinica'),
 (N'FK_PravilaOdobravanja_OrgJed',        N'PravilaOdobravanjaNabavke', N'OrganizacionaJedinica');

DECLARE @naziv sysname, @tab sysname, @kol sysname, @s nvarchar(max);
DECLARE f CURSOR LOCAL FAST_FORWARD FOR SELECT Naziv, Tabela, Kolona FROM @fk;
OPEN f;
FETCH NEXT FROM f INTO @naziv, @tab, @kol;
WHILE @@FETCH_STATUS = 0
BEGIN
    IF OBJECT_ID(N'dbo.' + @naziv, 'F') IS NULL AND OBJECT_ID(N'dbo.' + @tab, 'U') IS NOT NULL AND COL_LENGTH(N'dbo.' + @tab, @kol) IS NOT NULL
    BEGIN
        -- WITH NOCHECK: postojeći redovi bez para (npr. obrisana jedinica) ne sprečavaju ključ; novi unosi se proveravaju
        SET @s = N'ALTER TABLE dbo.' + QUOTENAME(@tab) + N' WITH NOCHECK ADD CONSTRAINT ' + QUOTENAME(@naziv)
               + N' FOREIGN KEY (' + QUOTENAME(@kol) + N') REFERENCES dbo.OrganizacioneJedinice (ID);';
        BEGIN TRY
            EXEC sp_executesql @s;
            PRINT N'Dodat ' + @naziv;
        END TRY
        BEGIN CATCH
            PRINT N'FK ' + @naziv + N' nije dodat: ' + ERROR_MESSAGE();
        END CATCH
    END
    FETCH NEXT FROM f INTO @naziv, @tab, @kol;
END
CLOSE f;
DEALLOCATE f;
GO

SET NOEXEC OFF;
GO

-- stanje posle skripte (fk_ka_jedinici treba da bude 4 kad su obe HR skripte puštene)
SELECT c.is_identity, c.is_nullable,
       (SELECT COUNT(*) FROM sys.key_constraints k WHERE k.parent_object_id = OBJECT_ID('dbo.OrganizacioneJedinice') AND k.type = 'PK') AS ima_pk,
       (SELECT COUNT(*) FROM sys.foreign_keys f WHERE f.referenced_object_id = OBJECT_ID('dbo.OrganizacioneJedinice')) AS fk_ka_jedinici,
       IDENT_CURRENT('dbo.OrganizacioneJedinice') AS ident_current
FROM sys.columns c WHERE c.object_id = OBJECT_ID('dbo.OrganizacioneJedinice') AND c.name = 'ID';
GO
