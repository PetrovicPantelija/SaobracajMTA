-- Hijerarhija skladišnog prostora (oktobar 2026): Zona -> Lokacija -> Objekat -> Polje -> Pozicija.
-- 1. Nova tabela SkladisteObjekat (nivo III), nove kolone u SkladistaGrupa, Skladista.SkladisteObjekatID,
--    Pozicija.Opis nvarchar(100) i Pozicija.Namena (skraćeni naziv) nvarchar(20).
-- 2. Procedure za Objekte i za nove kolone. Postojeće procedure (InsertSkladistaGrupa, InsertSkladista,
--    InsertPozicija ...) se ne menjaju: forme ih pozivaju kao do sada, pa nove vrednosti upisuju posebnim procedurama.
--    AktivnostHijerarhijaSkladista: aktivnost u dubinu (dugmad Aktivne / Neaktivne i promena aktivnosti pri snimanju).
--    VrstaPozicija (1 = kontejnerske, 2 = paletne) na polju i poziciji; promena vrste na polju aktivira pozicije te vrste.
-- 3. Migracija: svaka lokacija bez objekta dobija jedan objekat (ista Oznaka i Naziv), a polja se vezuju za njega.
-- 4. Kartice 'Objekti', 'Polja' i 'Pozicije' na ekranu Šifarnici (ako ne postoje: kopija kartice 'Lokacije')
--    i ista prava kao za 'Lokacije'.
-- Skripta se može pustiti više puta; drugo pokretanje ništa ne menja. Pustiti PRE instalacije nove verzije programa.
SET NOCOUNT ON;
GO

-- ---------------------------------------------------------------------------------------------
-- 1. Struktura
-- ---------------------------------------------------------------------------------------------
IF OBJECT_ID('dbo.SkladisteObjekat', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.SkladisteObjekat
    (
        ID                 int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SkladisteObjekat PRIMARY KEY,
        SkladistaGrupaID   int            NOT NULL,   -- lokacija
        Oznaka             nvarchar(30)   NOT NULL,
        Naziv              nvarchar(100)  NOT NULL,
        TipSkladistaID     int            NOT NULL,   -- KONTEJNERSKO / PALETNO / SLOBODNO (TipSkladista)
        Latitude           decimal(9, 6)  NULL,
        Longitude          decimal(9, 6)  NULL,
        Aktivan            int            NOT NULL CONSTRAINT DF_SkladisteObjekat_Aktivan DEFAULT (1),
        Datum              datetime       NULL,
        Korisnik           nvarchar(20)   NULL
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_SkladisteObjekat_SkladistaGrupaID' AND object_id = OBJECT_ID('dbo.SkladisteObjekat'))
    CREATE NONCLUSTERED INDEX IX_SkladisteObjekat_SkladistaGrupaID ON dbo.SkladisteObjekat (SkladistaGrupaID);
GO

IF COL_LENGTH('dbo.SkladistaGrupa', 'PostanskiBroj') IS NULL
    ALTER TABLE dbo.SkladistaGrupa ADD PostanskiBroj nvarchar(10) NULL;
IF COL_LENGTH('dbo.SkladistaGrupa', 'Latitude') IS NULL
    ALTER TABLE dbo.SkladistaGrupa ADD Latitude decimal(9, 6) NULL;
IF COL_LENGTH('dbo.SkladistaGrupa', 'Longitude') IS NULL
    ALTER TABLE dbo.SkladistaGrupa ADD Longitude decimal(9, 6) NULL;
IF COL_LENGTH('dbo.SkladistaGrupa', 'CarinskoSkladiste') IS NULL
    ALTER TABLE dbo.SkladistaGrupa ADD CarinskoSkladiste int NOT NULL CONSTRAINT DF_SkladistaGrupa_CarinskoSkladiste DEFAULT (0);
IF COL_LENGTH('dbo.SkladistaGrupa', 'CarinskaIspostavaID') IS NULL
    ALTER TABLE dbo.SkladistaGrupa ADD CarinskaIspostavaID int NULL;           -- Carinarnice.ID, samo uz CarinskoSkladiste = 1
IF COL_LENGTH('dbo.SkladistaGrupa', 'CarinskaSifraSkladista') IS NULL
    ALTER TABLE dbo.SkladistaGrupa ADD CarinskaSifraSkladista nvarchar(30) NULL;
IF COL_LENGTH('dbo.SkladistaGrupa', 'CarinskiNazivSkladista') IS NULL
    ALTER TABLE dbo.SkladistaGrupa ADD CarinskiNazivSkladista nvarchar(100) NULL;
IF COL_LENGTH('dbo.SkladistaGrupa', 'DrzalacSkladista') IS NULL
    ALTER TABLE dbo.SkladistaGrupa ADD DrzalacSkladista nvarchar(100) NULL;
IF COL_LENGTH('dbo.SkladistaGrupa', 'Datum') IS NULL
    ALTER TABLE dbo.SkladistaGrupa ADD Datum datetime NULL;
IF COL_LENGTH('dbo.SkladistaGrupa', 'Korisnik') IS NULL
    ALTER TABLE dbo.SkladistaGrupa ADD Korisnik nvarchar(20) NULL;
GO

IF COL_LENGTH('dbo.Skladista', 'SkladisteObjekatID') IS NULL
    ALTER TABLE dbo.Skladista ADD SkladisteObjekatID int NULL;
GO

-- Vrsta pozicija: 1 = kontejnerske, 2 = paletne. Polje radi sa jednom vrstom; aktivne su samo njegove pozicije te vrste.
-- Postojeća polja i pozicije postaju kontejnerske (podrazumevana vrednost).
IF COL_LENGTH('dbo.Skladista', 'VrstaPozicija') IS NULL
    ALTER TABLE dbo.Skladista ADD VrstaPozicija int NOT NULL CONSTRAINT DF_Skladista_VrstaPozicija DEFAULT (1);
IF COL_LENGTH('dbo.Pozicija', 'VrstaPozicija') IS NULL
    ALTER TABLE dbo.Pozicija ADD VrstaPozicija int NOT NULL CONSTRAINT DF_Pozicija_VrstaPozicija DEFAULT (1);
GO

-- naziv pozicije do 100 znakova, skraćeni naziv (kolona Namena, na formi 'Sk naziv') do 20
IF COL_LENGTH('dbo.Pozicija', 'Opis') < 200
    ALTER TABLE dbo.Pozicija ALTER COLUMN Opis nvarchar(100) NULL;
IF COL_LENGTH('dbo.Pozicija', 'Namena') < 40
    ALTER TABLE dbo.Pozicija ALTER COLUMN Namena nvarchar(20) NULL;
GO

-- ---------------------------------------------------------------------------------------------
-- 2. Procedure
-- ---------------------------------------------------------------------------------------------
IF OBJECT_ID('dbo.InsertSkladisteObjekat', 'P') IS NOT NULL DROP PROCEDURE dbo.InsertSkladisteObjekat;
GO
CREATE PROCEDURE dbo.InsertSkladisteObjekat
    @SkladistaGrupaID int,
    @Oznaka nvarchar(30),
    @Naziv nvarchar(100),
    @TipSkladistaID int,
    @Latitude decimal(9, 6) = NULL,
    @Longitude decimal(9, 6) = NULL,
    @Aktivan int,
    @Datum datetime,
    @Korisnik nvarchar(20)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.SkladisteObjekat (SkladistaGrupaID, Oznaka, Naziv, TipSkladistaID, Latitude, Longitude, Aktivan, Datum, Korisnik)
    VALUES (@SkladistaGrupaID, @Oznaka, @Naziv, @TipSkladistaID, @Latitude, @Longitude, @Aktivan, @Datum, @Korisnik);

    SELECT CAST(SCOPE_IDENTITY() AS int) AS ID;
END
GO

-- Promena lokacije objekta prenosi se i na GrupaPoljaID njegovih polja (kolona koju koriste ostali moduli).
IF OBJECT_ID('dbo.UpdateSkladisteObjekat', 'P') IS NOT NULL DROP PROCEDURE dbo.UpdateSkladisteObjekat;
GO
CREATE PROCEDURE dbo.UpdateSkladisteObjekat
    @ID int,
    @SkladistaGrupaID int,
    @Oznaka nvarchar(30),
    @Naziv nvarchar(100),
    @TipSkladistaID int,
    @Latitude decimal(9, 6) = NULL,
    @Longitude decimal(9, 6) = NULL,
    @Aktivan int,
    @Datum datetime,
    @Korisnik nvarchar(20)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.SkladisteObjekat
    SET SkladistaGrupaID = @SkladistaGrupaID, Oznaka = @Oznaka, Naziv = @Naziv, TipSkladistaID = @TipSkladistaID,
        Latitude = @Latitude, Longitude = @Longitude, Aktivan = @Aktivan, Datum = @Datum, Korisnik = @Korisnik
    WHERE ID = @ID;

    UPDATE dbo.Skladista SET GrupaPoljaID = @SkladistaGrupaID
    WHERE SkladisteObjekatID = @ID AND ISNULL(GrupaPoljaID, -1) <> @SkladistaGrupaID;
END
GO

IF OBJECT_ID('dbo.DeleteSkladisteObjekat', 'P') IS NOT NULL DROP PROCEDURE dbo.DeleteSkladisteObjekat;
GO
CREATE PROCEDURE dbo.DeleteSkladisteObjekat
    @ID int
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM dbo.Skladista WHERE SkladisteObjekatID = @ID)
    BEGIN
        RAISERROR(N'Objekat ima polja i ne može se obrisati.', 16, 1);
        RETURN;
    END

    DELETE FROM dbo.SkladisteObjekat WHERE ID = @ID;
END
GO

IF OBJECT_ID('dbo.AktivnostSkladisteObjekat', 'P') IS NOT NULL DROP PROCEDURE dbo.AktivnostSkladisteObjekat;
GO
CREATE PROCEDURE dbo.AktivnostSkladisteObjekat
    @ID int,
    @Aktivan int
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.SkladisteObjekat SET Aktivan = @Aktivan WHERE ID = @ID;
END
GO

-- Nove kolone lokacije; poziva se posle postojećih InsertSkladistaGrupa / UpdateSkladistaGrupa
IF OBJECT_ID('dbo.UpdateSkladistaGrupaDodatno', 'P') IS NOT NULL DROP PROCEDURE dbo.UpdateSkladistaGrupaDodatno;
GO
CREATE PROCEDURE dbo.UpdateSkladistaGrupaDodatno
    @ID int,
    @PostanskiBroj nvarchar(10) = NULL,
    @Latitude decimal(9, 6) = NULL,
    @Longitude decimal(9, 6) = NULL,
    @CarinskoSkladiste int,
    @CarinskaIspostavaID int = NULL,
    @CarinskaSifraSkladista nvarchar(30) = NULL,
    @CarinskiNazivSkladista nvarchar(100) = NULL,
    @DrzalacSkladista nvarchar(100) = NULL,
    @Datum datetime,
    @Korisnik nvarchar(20)
AS
BEGIN
    SET NOCOUNT ON;

    -- carinska polja važe samo za carinsko skladište
    IF @CarinskoSkladiste <> 1
        SELECT @CarinskaIspostavaID = NULL, @CarinskaSifraSkladista = NULL, @CarinskiNazivSkladista = NULL, @DrzalacSkladista = NULL;

    UPDATE dbo.SkladistaGrupa
    SET PostanskiBroj = @PostanskiBroj, Latitude = @Latitude, Longitude = @Longitude,
        CarinskoSkladiste = @CarinskoSkladiste, CarinskaIspostavaID = @CarinskaIspostavaID,
        CarinskaSifraSkladista = @CarinskaSifraSkladista, CarinskiNazivSkladista = @CarinskiNazivSkladista,
        DrzalacSkladista = @DrzalacSkladista, Datum = @Datum, Korisnik = @Korisnik
    WHERE ID = @ID;
END
GO

-- Objekat polja; GrupaPoljaID se popunjava lokacijom objekta. Poziva se posle InsertSkladista / UpdateSkladista.
IF OBJECT_ID('dbo.UpdateSkladistaObjekat', 'P') IS NOT NULL DROP PROCEDURE dbo.UpdateSkladistaObjekat;
GO
CREATE PROCEDURE dbo.UpdateSkladistaObjekat
    @ID int,
    @SkladisteObjekatID int
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE s
    SET SkladisteObjekatID = o.ID, GrupaPoljaID = o.SkladistaGrupaID
    FROM dbo.Skladista s
    INNER JOIN dbo.SkladisteObjekat o ON o.ID = @SkladisteObjekatID
    WHERE s.ID = @ID;
END
GO

-- Pun naziv (do 100) i skraćeni naziv (do 20) pozicije; poziva se posle InsertPozicija / UpdatePozicija,
-- jer postojeće procedure mogu imati kraće parametre
IF OBJECT_ID('dbo.UpdatePozicijaNazivi', 'P') IS NOT NULL DROP PROCEDURE dbo.UpdatePozicijaNazivi;
GO
CREATE PROCEDURE dbo.UpdatePozicijaNazivi
    @ID int,
    @Opis nvarchar(100) = NULL,
    @Namena nvarchar(20) = NULL,
    @VrstaPozicija int = NULL   -- 1 = kontejnerska, 2 = paletna; NULL = ne menja se
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Pozicija
    SET Opis = @Opis, Namena = @Namena, VrstaPozicija = ISNULL(@VrstaPozicija, VrstaPozicija)
    WHERE ID = @ID;
END
GO

-- Vrsta pozicija polja. Kada se vrsta promeni, pozicije nove vrste postaju aktivne (ako je polje aktivno),
-- a pozicije druge vrste neaktivne. Vraća broj pozicija kojima je promenjena aktivnost.
IF OBJECT_ID('dbo.UpdateSkladistaVrstaPozicija', 'P') IS NOT NULL DROP PROCEDURE dbo.UpdateSkladistaVrstaPozicija;
GO
CREATE PROCEDURE dbo.UpdateSkladistaVrstaPozicija
    @ID int,
    @VrstaPozicija int,
    @BrojPozicija int = NULL OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    SET @BrojPozicija = 0;
    DECLARE @Stara int, @PoljeAktivno int;
    SELECT @Stara = VrstaPozicija, @PoljeAktivno = CASE WHEN ISNULL(Aktivan, 0) > 0 THEN 1 ELSE 0 END
    FROM dbo.Skladista WHERE ID = @ID;

    UPDATE dbo.Skladista SET VrstaPozicija = @VrstaPozicija WHERE ID = @ID;

    IF @Stara IS NULL OR @Stara = @VrstaPozicija
        RETURN;

    UPDATE dbo.Pozicija
    SET Aktivna = CASE WHEN VrstaPozicija = @VrstaPozicija THEN @PoljeAktivno ELSE 0 END
    WHERE Skladiste = @ID
      AND ISNULL(Aktivna, -1) <> CASE WHEN VrstaPozicija = @VrstaPozicija THEN @PoljeAktivno ELSE 0 END;
    SET @BrojPozicija = @@ROWCOUNT;
END
GO

-- Aktivnost u dubinu: zapis i SVI njegovi podređeni zapisi dobijaju istu aktivnost
-- (Zona -> Lokacije -> Objekti -> Polja -> Pozicije). Polja vezana direktno za lokaciju (GrupaPoljaID, bez objekta)
-- računaju se kao deca lokacije. @Nivo: Zona, Lokacija, Objekat, Polje, Pozicija.
-- Da li se zapis sme aktivirati (roditelj mora biti aktivan) proverava forma.
IF OBJECT_ID('dbo.AktivnostHijerarhijaSkladista', 'P') IS NOT NULL DROP PROCEDURE dbo.AktivnostHijerarhijaSkladista;
GO
CREATE PROCEDURE dbo.AktivnostHijerarhijaSkladista
    @Nivo nvarchar(20),
    @ID int,
    @Aktivan int,
    @BrojPodredjenih int = NULL OUTPUT   -- broj podređenih zapisa kojima je promenjena aktivnost
AS
BEGIN
    SET NOCOUNT ON;

    SET @Aktivan = CASE WHEN @Aktivan > 0 THEN 1 ELSE 0 END;
    SET @BrojPodredjenih = 0;

    DECLARE @Lokacije TABLE (ID int NOT NULL);
    DECLARE @Objekti TABLE (ID int NOT NULL);
    DECLARE @Polja TABLE (ID int NOT NULL);

    -- sam zapis
    IF @Nivo = N'Zona'
        UPDATE dbo.Zone SET Aktivna = @Aktivan WHERE ID = @ID;
    ELSE IF @Nivo = N'Lokacija'
        UPDATE dbo.SkladistaGrupa SET Aktivna = @Aktivan WHERE ID = @ID;
    ELSE IF @Nivo = N'Objekat'
        UPDATE dbo.SkladisteObjekat SET Aktivan = @Aktivan WHERE ID = @ID;
    ELSE IF @Nivo = N'Polje'
        UPDATE dbo.Skladista SET Aktivan = @Aktivan WHERE ID = @ID;
    ELSE IF @Nivo = N'Pozicija'
    BEGIN
        UPDATE dbo.Pozicija SET Aktivna = @Aktivan WHERE ID = @ID;
        RETURN;
    END
    ELSE
    BEGIN
        RAISERROR(N'Nepoznat nivo hijerarhije: %s', 16, 1, @Nivo);
        RETURN;
    END

    -- podređeni zapisi, nivo po nivo
    IF @Nivo = N'Zona'
        INSERT INTO @Lokacije (ID) SELECT ID FROM dbo.SkladistaGrupa WHERE ZonaID = @ID AND ID IS NOT NULL;

    IF @Nivo IN (N'Zona', N'Lokacija')
        INSERT INTO @Objekti (ID)
        SELECT o.ID FROM dbo.SkladisteObjekat o
        WHERE o.SkladistaGrupaID IN (SELECT ID FROM @Lokacije) OR (@Nivo = N'Lokacija' AND o.SkladistaGrupaID = @ID);

    IF @Nivo IN (N'Zona', N'Lokacija', N'Objekat')
        INSERT INTO @Polja (ID)
        SELECT s.ID FROM dbo.Skladista s
        WHERE s.ID IS NOT NULL
          AND (s.SkladisteObjekatID IN (SELECT ID FROM @Objekti)
               OR (@Nivo = N'Objekat' AND s.SkladisteObjekatID = @ID)
               OR s.GrupaPoljaID IN (SELECT ID FROM @Lokacije)
               OR (@Nivo = N'Lokacija' AND s.GrupaPoljaID = @ID));

    UPDATE dbo.SkladistaGrupa SET Aktivna = @Aktivan
    WHERE ID IN (SELECT ID FROM @Lokacije) AND ISNULL(Aktivna, -1) <> @Aktivan;
    SET @BrojPodredjenih = @BrojPodredjenih + @@ROWCOUNT;

    UPDATE dbo.SkladisteObjekat SET Aktivan = @Aktivan
    WHERE ID IN (SELECT ID FROM @Objekti) AND Aktivan <> @Aktivan;
    SET @BrojPodredjenih = @BrojPodredjenih + @@ROWCOUNT;

    UPDATE dbo.Skladista SET Aktivan = @Aktivan
    WHERE ID IN (SELECT ID FROM @Polja) AND ISNULL(Aktivan, -1) <> @Aktivan;
    SET @BrojPodredjenih = @BrojPodredjenih + @@ROWCOUNT;

    -- pri aktiviranju postaju aktivne samo pozicije iste vrste kao polje (kontejnerske / paletne), ostale neaktivne
    UPDATE p SET Aktivna = CASE WHEN @Aktivan = 1 AND p.VrstaPozicija = s.VrstaPozicija THEN 1 ELSE 0 END
    FROM dbo.Pozicija p
    INNER JOIN dbo.Skladista s ON s.ID = p.Skladiste
    WHERE (p.Skladiste IN (SELECT ID FROM @Polja) OR (@Nivo = N'Polje' AND p.Skladiste = @ID))
      AND ISNULL(p.Aktivna, -1) <> CASE WHEN @Aktivan = 1 AND p.VrstaPozicija = s.VrstaPozicija THEN 1 ELSE 0 END;
    SET @BrojPodredjenih = @BrojPodredjenih + @@ROWCOUNT;
END
GO

-- ---------------------------------------------------------------------------------------------
-- 3. Migracija: lokacija bez objekta -> jedan objekat; polja bez objekta -> objekat svoje lokacije
-- ---------------------------------------------------------------------------------------------
DECLARE @Sada datetime = GETDATE();
DECLARE @PrviTip int = (SELECT MIN(ID) FROM dbo.TipSkladista);

-- lokacije bez tipa skladišta dobijaju prvi tip iz šifarnika (ispisuju se za proveru)
SELECT g.ID AS LokacijaID, g.Oznaka, g.Naziv, N'nema tip skladišta, objekat dobija TipSkladistaID = '
       + CAST(@PrviTip AS nvarchar(10)) AS Napomena
FROM dbo.SkladistaGrupa g
WHERE NOT EXISTS (SELECT 1 FROM dbo.SkladisteObjekat o WHERE o.SkladistaGrupaID = g.ID)
  AND NOT EXISTS (SELECT 1 FROM dbo.TipSkladista t WHERE t.ID = g.TipSkladistaID);

INSERT INTO dbo.SkladisteObjekat (SkladistaGrupaID, Oznaka, Naziv, TipSkladistaID, Aktivan, Datum, Korisnik)
SELECT g.ID,
       ISNULL(NULLIF(LTRIM(RTRIM(g.Oznaka)), N''), N'L' + CAST(g.ID AS nvarchar(10))),
       ISNULL(NULLIF(LTRIM(RTRIM(g.Naziv)), N''), N'Lokacija ' + CAST(g.ID AS nvarchar(10))),
       CASE WHEN EXISTS (SELECT 1 FROM dbo.TipSkladista t WHERE t.ID = g.TipSkladistaID) THEN g.TipSkladistaID ELSE @PrviTip END,
       CASE WHEN ISNULL(g.Aktivna, 0) > 0 THEN 1 ELSE 0 END,
       @Sada,
       N'migracija'
FROM dbo.SkladistaGrupa g
WHERE g.ID IS NOT NULL
  AND NOT EXISTS (SELECT 1 FROM dbo.SkladisteObjekat o WHERE o.SkladistaGrupaID = g.ID);

PRINT N'Napravljeno objekata: ' + CAST(@@ROWCOUNT AS nvarchar(10));

-- polje dobija objekat napravljen od njegove lokacije (samo polja koja još nemaju objekat)
UPDATE s
SET SkladisteObjekatID = o.ID
FROM dbo.Skladista s
INNER JOIN dbo.SkladisteObjekat o ON o.SkladistaGrupaID = s.GrupaPoljaID AND o.Korisnik = N'migracija'
WHERE s.SkladisteObjekatID IS NULL;

PRINT N'Polja vezana za objekat: ' + CAST(@@ROWCOUNT AS nvarchar(10));
GO

-- polja koja su ostala bez objekta (prazan GrupaPoljaID ili nepostojeća lokacija)
SELECT s.ID, s.Oznaka, s.Naziv, s.GrupaPoljaID AS LokacijaID, N'polje nema objekat' AS Napomena
FROM dbo.Skladista s
WHERE s.SkladisteObjekatID IS NULL
ORDER BY s.ID;

-- objekti sa istom oznakom (jedinstvenost se proverava pri snimanju u formi)
SELECT Oznaka, COUNT(*) AS Broj, N'dupla oznaka objekta' AS Napomena
FROM dbo.SkladisteObjekat GROUP BY Oznaka HAVING COUNT(*) > 1;
GO

-- ---------------------------------------------------------------------------------------------
-- 4. Kartice 'Objekti', 'Polja' i 'Pozicije' (ekran Šifarnici otvara formu u glavnom prozoru preko kartice i prava)
--    Svaka koja ne postoji pravi se kao kopija kartice 'Lokacije' (isti MainID i Parent), a korisnici sa pravom na 'Lokacije'
--    dobijaju pravo i na nju. Kolone se čitaju iz baze, pa skripta ne zavisi od tačne strukture tabela.
-- ---------------------------------------------------------------------------------------------
IF OBJECT_ID('dbo.mainNoviKartice', 'U') IS NOT NULL AND OBJECT_ID('dbo.MainNoviPrava', 'U') IS NOT NULL
BEGIN
    DECLARE @LokacijeID int, @MainID int, @Parent int, @ObjektiID int, @NovaKartica nvarchar(50), @sql nvarchar(max), @kolone nvarchar(max), @vrednosti nvarchar(max), @Broj int;
    DECLARE @KarticaIdentity bit = CASE WHEN COLUMNPROPERTY(OBJECT_ID('dbo.mainNoviKartice'), 'ID', 'IsIdentity') = 1 THEN 1 ELSE 0 END;
    DECLARE @PravaIdentity bit = CASE WHEN COLUMNPROPERTY(OBJECT_ID('dbo.MainNoviPrava'), 'ID', 'IsIdentity') = 1 THEN 1 ELSE 0 END;

    DECLARE kartice CURSOR LOCAL FAST_FORWARD FOR
        SELECT k.ID, k.MainID, k.Parent, n.Naziv FROM dbo.mainNoviKartice k
        CROSS JOIN (VALUES (N'Objekti'), (N'Polja'), (N'Pozicije')) n (Naziv)
        WHERE k.Naziv = N'Lokacije';
    OPEN kartice;
    FETCH NEXT FROM kartice INTO @LokacijeID, @MainID, @Parent, @NovaKartica;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        SELECT @ObjektiID = NULL;
        SELECT @ObjektiID = ID FROM dbo.mainNoviKartice
        WHERE MainID = @MainID AND ISNULL(Parent, -1) = ISNULL(@Parent, -1) AND Naziv = @NovaKartica;

        IF @ObjektiID IS NULL
        BEGIN
            SELECT @kolone = STUFF((SELECT N', ' + QUOTENAME(c.name)
                                    FROM sys.columns c
                                    WHERE c.object_id = OBJECT_ID('dbo.mainNoviKartice') AND c.is_identity = 0 AND c.is_computed = 0
                                    ORDER BY c.column_id FOR XML PATH('')), 1, 2, N''),
                   @vrednosti = STUFF((SELECT N', ' + CASE c.name
                                                        WHEN 'Naziv' THEN N'@NovaKartica'
                                                        WHEN 'ID' THEN N'(SELECT ISNULL(MAX(ID), 0) + 1 FROM dbo.mainNoviKartice)'
                                                        ELSE QUOTENAME(c.name) END
                                       FROM sys.columns c
                                       WHERE c.object_id = OBJECT_ID('dbo.mainNoviKartice') AND c.is_identity = 0 AND c.is_computed = 0
                                       ORDER BY c.column_id FOR XML PATH('')), 1, 2, N'');
            SET @sql = N'INSERT INTO dbo.mainNoviKartice (' + @kolone + N') SELECT ' + @vrednosti
                     + N' FROM dbo.mainNoviKartice WHERE ID = @Izvor';
            EXEC sp_executesql @sql, N'@Izvor int, @NovaKartica nvarchar(50)', @Izvor = @LokacijeID, @NovaKartica = @NovaKartica;

            SELECT @ObjektiID = ID FROM dbo.mainNoviKartice
            WHERE MainID = @MainID AND ISNULL(Parent, -1) = ISNULL(@Parent, -1) AND Naziv = @NovaKartica;
            PRINT N'Dodata kartica ' + @NovaKartica + N', ID = ' + CAST(@ObjektiID AS nvarchar(10)) + N', MainID = ' + CAST(@MainID AS nvarchar(10));
        END

        -- prava: svi koji imaju 'Lokacije' dobijaju i novu karticu (samo ako je već nemaju)
        SELECT @kolone = STUFF((SELECT N', ' + QUOTENAME(c.name)
                                FROM sys.columns c
                                WHERE c.object_id = OBJECT_ID('dbo.MainNoviPrava') AND c.is_identity = 0 AND c.is_computed = 0
                                ORDER BY c.column_id FOR XML PATH('')), 1, 2, N''),
               @vrednosti = STUFF((SELECT N', ' + CASE c.name
                                                    WHEN 'KarticaID' THEN N'@Novi'
                                                    WHEN 'ID' THEN N'(SELECT ISNULL(MAX(ID), 0) FROM dbo.MainNoviPrava) + ROW_NUMBER() OVER (ORDER BY (SELECT 1))'
                                                    ELSE N'p.' + QUOTENAME(c.name) END
                                   FROM sys.columns c
                                   WHERE c.object_id = OBJECT_ID('dbo.MainNoviPrava') AND c.is_identity = 0 AND c.is_computed = 0
                                   ORDER BY c.column_id FOR XML PATH('')), 1, 2, N'');
        SET @sql = N'INSERT INTO dbo.MainNoviPrava (' + @kolone + N') SELECT ' + @vrednosti
                 + N' FROM dbo.MainNoviPrava p WHERE p.KarticaID = @Izvor AND p.MainID = @MainID'
                 + N' AND NOT EXISTS (SELECT 1 FROM dbo.MainNoviPrava x WHERE x.KarticaID = @Novi AND x.MainID = p.MainID AND x.Korisnik = p.Korisnik);'
                 + N' SET @Broj = @@ROWCOUNT;';
        SET @Broj = 0;
        EXEC sp_executesql @sql, N'@Izvor int, @Novi int, @MainID int, @Broj int OUTPUT',
             @Izvor = @LokacijeID, @Novi = @ObjektiID, @MainID = @MainID, @Broj = @Broj OUTPUT;
        PRINT N'Dodato prava za ' + @NovaKartica + N': ' + CAST(ISNULL(@Broj, 0) AS nvarchar(10));

        FETCH NEXT FROM kartice INTO @LokacijeID, @MainID, @Parent, @NovaKartica;
    END
    CLOSE kartice;
    DEALLOCATE kartice;
END
GO
