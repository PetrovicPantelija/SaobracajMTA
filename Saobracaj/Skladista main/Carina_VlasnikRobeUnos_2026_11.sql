/* Carina – unos novog vlasnika robe (VlasniciRobe) sa naloga za prijem  RAZ-07
   26.11.2026.
   Nalog za PRIJEM > dugme „Novi vlasnik robe“: naziv i PIB; novi vlasnik se odmah bira na nalogu.
   Procedura sp_VlasnikRobe_Novi:
     - naziv i PIB su obavezni;
     - ako vlasnik sa istim PIB-om već postoji, ne pravi se duplikat: vraća se postojeći (@Postojao = 1);
     - šema tabele VlasniciRobe nije u repozitorijumu, pa procedura sama proverava da li je id IDENTITY
       (ako nije, uzima MAX + 1 pod zaključavanjem) i ostale obavezne kolone bez podrazumevane vrednosti
       popunjava praznim tekstom, nulom ili današnjim datumom.
   Skripta se može pustiti više puta. Radi i na bazi sa nivoom kompatibilnosti 100. */

SET NOCOUNT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF OBJECT_ID('dbo.VlasniciRobe', 'U') IS NULL
   OR COL_LENGTH('dbo.VlasniciRobe', 'id') IS NULL OR COL_LENGTH('dbo.VlasniciRobe', 'Naziv') IS NULL OR COL_LENGTH('dbo.VlasniciRobe', 'PIB') IS NULL
BEGIN
    RAISERROR(N'GREŠKA: tabela dbo.VlasniciRobe ne postoji ili nema kolone id, Naziv i PIB; ova skripta se ne izvršava.', 16, 1);
    SET NOEXEC ON;   -- ostatak skripte se ne izvršava (isključuje se na kraju)
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_VlasnikRobe_Novi
    @Naziv    nvarchar(200),
    @PIB      nvarchar(20),
    @ID       int OUTPUT,
    @Postojao bit OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    SET @Naziv = LTRIM(RTRIM(ISNULL(@Naziv, N'')));
    SET @PIB = LTRIM(RTRIM(ISNULL(@PIB, N'')));
    SET @ID = NULL;
    SET @Postojao = 0;
    IF @Naziv = N'' OR @PIB = N''
    BEGIN
        RAISERROR(N'Unesite naziv i PIB vlasnika robe.', 16, 1);
        RETURN;
    END

    BEGIN TRAN;

    -- isti PIB: vraća se postojeći vlasnik (zaključavanje sprečava dva ista unosa u isto vreme)
    SELECT TOP 1 @ID = id
    FROM dbo.VlasniciRobe WITH (UPDLOCK, HOLDLOCK)
    WHERE LTRIM(RTRIM(CONVERT(nvarchar(50), PIB))) = @PIB
    ORDER BY id;
    IF @ID IS NOT NULL
    BEGIN
        SET @Postojao = 1;
        COMMIT;
        RETURN;
    END

    DECLARE @tbl int = OBJECT_ID('dbo.VlasniciRobe');
    DECLARE @identity bit = CASE WHEN COLUMNPROPERTY(@tbl, 'id', 'IsIdentity') = 1 THEN 1 ELSE 0 END;
    DECLARE @kolone nvarchar(max) = N'[Naziv], [PIB]', @vrednosti nvarchar(max) = N'@Naziv, @PIB';
    DECLARE @noviID int;

    IF @identity = 0
    BEGIN
        SELECT @noviID = ISNULL(MAX(id), 0) + 1 FROM dbo.VlasniciRobe WITH (UPDLOCK, HOLDLOCK);
        SET @kolone = N'[id], ' + @kolone;
        SET @vrednosti = N'@NoviID, ' + @vrednosti;
    END

    -- ostale obavezne kolone bez podrazumevane vrednosti
    SELECT @kolone = @kolone + N', ' + QUOTENAME(c.name),
           @vrednosti = @vrednosti + N', ' +
               CASE WHEN t.name IN ('char', 'varchar', 'nchar', 'nvarchar', 'text', 'ntext') THEN N'N'''''
                    WHEN t.name IN ('date', 'datetime', 'datetime2', 'smalldatetime', 'datetimeoffset', 'time') THEN N'GETDATE()'
                    WHEN t.name = 'uniqueidentifier' THEN N'NEWID()'
                    ELSE N'0' END
    FROM sys.columns c
    JOIN sys.types t ON t.user_type_id = c.system_type_id AND t.user_type_id = t.system_type_id
    WHERE c.object_id = @tbl
      AND c.is_nullable = 0 AND c.is_identity = 0 AND c.is_computed = 0 AND c.default_object_id = 0
      AND t.name NOT IN ('timestamp')
      AND LOWER(c.name) NOT IN (N'id', N'naziv', N'pib');

    DECLARE @sql nvarchar(max) = N'INSERT dbo.VlasniciRobe (' + @kolone + N') VALUES (' + @vrednosti + N');'
        + CASE WHEN @identity = 1 THEN N' SET @Novi = SCOPE_IDENTITY();' ELSE N' SET @Novi = @NoviID;' END;
    EXEC sp_executesql @sql, N'@Naziv nvarchar(200), @PIB nvarchar(20), @NoviID int, @Novi int OUTPUT',
         @Naziv = @Naziv, @PIB = @PIB, @NoviID = @noviID, @Novi = @ID OUTPUT;

    COMMIT;
END
GO

PRINT 'Carina_VlasnikRobeUnos_2026_11.sql: završeno.';
GO
SET NOEXEC OFF;
GO
