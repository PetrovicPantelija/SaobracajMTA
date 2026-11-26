/* =============================================================================================
   Carina: izlaz po paletama, rezervacije i dokument razduženja (RAZ-13, RAZ-14, RAZ-15)
   Predlog: Carina-Izlaz-Tarife-Vlasnik-Kontejneri-Knjiga-Predlog.md, verzija 1.1, 26.11.2026.

   Šta radi:
     1. RNCarinskoPaletaIzlaz: koleta jedne stavke sa jedne palete na nalogu otpreme.
          Status 1 = rezervisano (otprema u pripremi), 2 = izdato (potvrđen izlaz otpremnice).
          Bruto, neto i vrednost se računaju pri rezervaciji: srazmerno koletima, a poslednji izlaz
          stavke uzima tačan ostatak (nema ostatka od zaokruživanja).
     2. RNCarinskoPaleta: status 3 = Izdata (paleta bez stanja; skida se sa polja, polje ostaje u logu).
     3. RNCarinskoRazduzenje, RNCarinskoRazduzenjeStavka, RNCarinskoOtpremnicaRazduzenje:
          dokument razduženja (C4, IZ3, C3...) po naimenovanju C7, više prema više sa otpremnicama.
     4. Procedure sp_Izlaz_* i sp_Razduzenje_*.
     5. Izveštaj (ništa se ne menja): šta postojeće procedure otpremnice rade sa prometom skladišta.

   Pre ove skripte mora biti puštena Carina_PrijemnicaPL_2026_11.sql (palete, IDNaimenovanja na stavci).
   Posle ove skripte ponovo pustiti Carina_PrijemnicaPL_2026_11.sql nije potrebno; ako se ipak pusti,
   ostaje sve iz ove skripte (procedure paleta u njoj već znaju za izlaz).

   Skripta se može pustiti više puta. Pre puštanja napraviti rezervnu kopiju baze.
   Rezultat (poruke i tabele) poslati nazad.
   ============================================================================================= */
SET NOCOUNT ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
GO

IF OBJECT_ID('dbo.RNCarinskoPaleta', 'U') IS NULL OR OBJECT_ID('dbo.RNCarinskoPaletaStavka', 'U') IS NULL
   OR OBJECT_ID('dbo.RNCarinskoPaletaPoljeLog', 'U') IS NULL OR COL_LENGTH('dbo.RNCarinskoPrijemnicaStavke', 'IDNaimenovanja') IS NULL
BEGIN
    RAISERROR(N'GREŠKA: nema tabela paleta i polja. Prvo pustiti Carina_PrijemnicaPL_2026_11.sql; ova skripta se ne izvršava.', 16, 1);
    SET NOEXEC ON;   -- ostatak skripte se ne izvršava (isključuje se na kraju)
END
GO

/* ---------------------------------------------------------------------------------------------
   1. Status palete 3 = Izdata
   --------------------------------------------------------------------------------------------- */
IF EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_RNCarinskoPaleta_Status'
           AND parent_object_id = OBJECT_ID('dbo.RNCarinskoPaleta') AND definition NOT LIKE '%3%')
BEGIN
    ALTER TABLE dbo.RNCarinskoPaleta DROP CONSTRAINT CK_RNCarinskoPaleta_Status;
    PRINT 'Uklonjena stara provera statusa palete (1, 2).';
END
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_RNCarinskoPaleta_Status' AND parent_object_id = OBJECT_ID('dbo.RNCarinskoPaleta'))
BEGIN
    ALTER TABLE dbo.RNCarinskoPaleta ADD CONSTRAINT CK_RNCarinskoPaleta_Status CHECK (Status IN (1, 2, 3));   -- 1 Planirana, 2 Na polju, 3 Izdata
    PRINT 'Status palete: 1 Planirana, 2 Na polju, 3 Izdata.';
END
GO

/* ---------------------------------------------------------------------------------------------
   2. Izlaz po paletama (RAZ-13) i rezervacije (RAZ-15)
   --------------------------------------------------------------------------------------------- */
IF OBJECT_ID('dbo.RNCarinskoPaletaIzlaz', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.RNCarinskoPaletaIzlaz (
        ID               int IDENTITY(1,1) NOT NULL CONSTRAINT PK_RNCarinskoPaletaIzlaz PRIMARY KEY,
        IDPalete         int           NOT NULL CONSTRAINT FK_RNCarinskoPaletaIzlaz_Paleta REFERENCES dbo.RNCarinskoPaleta (ID),
        IDStavke         int           NOT NULL,          -- RNCarinskoPrijemnicaStavke.ID
        RN               int           NOT NULL,          -- RadniNalogSkladista.ID (nalog otpreme)
        IDOtpremnice     int           NULL,              -- RNCarinskoSkladisteOtpremnica.ID, upisuje se potvrdom izlaza
        Koleta           int           NOT NULL CONSTRAINT CK_RNCarinskoPaletaIzlaz_Koleta CHECK (Koleta > 0),
        Bruto            decimal(18,3) NULL,
        Neto             decimal(18,3) NULL,
        Vrednost         decimal(18,2) NULL,
        Status           tinyint       NOT NULL CONSTRAINT DF_RNCarinskoPaletaIzlaz_Status DEFAULT (1)
                                       CONSTRAINT CK_RNCarinskoPaletaIzlaz_Status CHECK (Status IN (1, 2)),   -- 1 Rezervisano, 2 Izdato
        Korisnik         nvarchar(50)  NULL,
        DatumRezervacije datetime      NOT NULL CONSTRAINT DF_RNCarinskoPaletaIzlaz_Datum DEFAULT (GETDATE()),
        DatumIzlaza      datetime      NULL,
        KorisnikIzlaza   nvarchar(50)  NULL);
    PRINT 'Napravljena tabela dbo.RNCarinskoPaletaIzlaz.';
END
ELSE
    PRINT 'Tabela dbo.RNCarinskoPaletaIzlaz već postoji.';
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID('dbo.RNCarinskoPaletaIzlaz') AND name = 'IX_RNCarinskoPaletaIzlaz_Paleta')
    CREATE INDEX IX_RNCarinskoPaletaIzlaz_Paleta ON dbo.RNCarinskoPaletaIzlaz (IDPalete, IDStavke) INCLUDE (Koleta, Status, RN);
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID('dbo.RNCarinskoPaletaIzlaz') AND name = 'IX_RNCarinskoPaletaIzlaz_RN')
    CREATE INDEX IX_RNCarinskoPaletaIzlaz_RN ON dbo.RNCarinskoPaletaIzlaz (RN, Status);
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID('dbo.RNCarinskoPaletaIzlaz') AND name = 'IX_RNCarinskoPaletaIzlaz_Stavka')
    CREATE INDEX IX_RNCarinskoPaletaIzlaz_Stavka ON dbo.RNCarinskoPaletaIzlaz (IDStavke) INCLUDE (Koleta, Bruto, Neto, Vrednost, Status, RN);
GO

/* ---------------------------------------------------------------------------------------------
   3. Dokument razduženja (RAZ-14)
   --------------------------------------------------------------------------------------------- */
IF OBJECT_ID('dbo.RNCarinskoRazduzenje', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.RNCarinskoRazduzenje (
        ID              int IDENTITY(1,1) NOT NULL CONSTRAINT PK_RNCarinskoRazduzenje PRIMARY KEY,
        CI              nvarchar(10)  NOT NULL CONSTRAINT DF_RNCarinskoRazduzenje_CI DEFAULT (N''),   -- carinarnica (npr. 42072)
        Vrsta           nvarchar(10)  NOT NULL,          -- C4, IZ3, C3, ...
        Broj            nvarchar(20)  NOT NULL,
        Godina          smallint      NOT NULL,
        DatumIzdavanja  date          NULL,
        Valuta          nvarchar(10)  NULL,
        Kurs            decimal(18,6) NULL,
        Napomena        nvarchar(500) NULL,
        Korisnik        nvarchar(50)  NULL,
        DatumUnosa      datetime      NOT NULL CONSTRAINT DF_RNCarinskoRazduzenje_DatumUnosa DEFAULT (GETDATE()),
        DatumIzmene     datetime      NULL,
        CONSTRAINT UQ_RNCarinskoRazduzenje_Dokument UNIQUE (CI, Vrsta, Broj, Godina));
    PRINT 'Napravljena tabela dbo.RNCarinskoRazduzenje.';
END
GO
IF OBJECT_ID('dbo.RNCarinskoRazduzenjeStavka', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.RNCarinskoRazduzenjeStavka (
        ID              int IDENTITY(1,1) NOT NULL CONSTRAINT PK_RNCarinskoRazduzenjeStavka PRIMARY KEY,
        IDRazduzenja    int           NOT NULL CONSTRAINT FK_RNCarinskoRazduzenjeStavka_Razduzenje REFERENCES dbo.RNCarinskoRazduzenje (ID),
        IDNaimenovanja  int           NOT NULL,          -- RNCarinskoStavke.ID (naimenovanje C7)
        Koleta          decimal(18,3) NOT NULL CONSTRAINT DF_RNCarinskoRazduzenjeStavka_Koleta DEFAULT (0),
        Neto            decimal(18,3) NOT NULL CONSTRAINT DF_RNCarinskoRazduzenjeStavka_Neto DEFAULT (0),
        Bruto           decimal(18,3) NOT NULL CONSTRAINT DF_RNCarinskoRazduzenjeStavka_Bruto DEFAULT (0),
        Vrednost        decimal(18,2) NOT NULL CONSTRAINT DF_RNCarinskoRazduzenjeStavka_Vrednost DEFAULT (0),
        CONSTRAINT UQ_RNCarinskoRazduzenjeStavka UNIQUE (IDRazduzenja, IDNaimenovanja),
        CONSTRAINT CK_RNCarinskoRazduzenjeStavka_Pozitivno CHECK (Koleta >= 0 AND Neto >= 0 AND Bruto >= 0 AND Vrednost >= 0));
    PRINT 'Napravljena tabela dbo.RNCarinskoRazduzenjeStavka.';
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID('dbo.RNCarinskoRazduzenjeStavka') AND name = 'IX_RNCarinskoRazduzenjeStavka_Naimenovanje')
    CREATE INDEX IX_RNCarinskoRazduzenjeStavka_Naimenovanje ON dbo.RNCarinskoRazduzenjeStavka (IDNaimenovanja) INCLUDE (Koleta, Neto, Bruto, Vrednost);
GO
IF OBJECT_ID('dbo.RNCarinskoOtpremnicaRazduzenje', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.RNCarinskoOtpremnicaRazduzenje (
        IDOtpremnice    int NOT NULL,                    -- RNCarinskoSkladisteOtpremnica.ID
        IDRazduzenja    int NOT NULL CONSTRAINT FK_RNCarinskoOtpremnicaRazduzenje_Razduzenje REFERENCES dbo.RNCarinskoRazduzenje (ID),
        CONSTRAINT PK_RNCarinskoOtpremnicaRazduzenje PRIMARY KEY (IDOtpremnice, IDRazduzenja));
    PRINT 'Napravljena tabela dbo.RNCarinskoOtpremnicaRazduzenje.';
END
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID('dbo.RNCarinskoOtpremnicaRazduzenje') AND name = 'IX_RNCarinskoOtpremnicaRazduzenje_Razduzenje')
    CREATE INDEX IX_RNCarinskoOtpremnicaRazduzenje_Razduzenje ON dbo.RNCarinskoOtpremnicaRazduzenje (IDRazduzenja);
GO

/* ---------------------------------------------------------------------------------------------
   4. Lager po paletama za nalog otpreme
   Jedan red = jedna stavka na jednoj paleti potvrđene prijemnice (ZA) istog magacinskog broja,
   sa koletima koja još nisu izdata. Rezervacije drugih naloga se oduzimaju od slobodnog;
   rezervacija naloga @RN se prikazuje posebno (može da se menja).
   Rezervacija naloga koji više ne postoji ne zauzima paletu.
   --------------------------------------------------------------------------------------------- */
CREATE OR ALTER PROCEDURE dbo.sp_Izlaz_LagerPaleta
    @MagacinskiBroj int,
    @RN             int = 0
AS
BEGIN
    SET NOCOUNT ON;
    SELECT p.ID AS IDPalete, p.BrojPalete, ps.IDStavke, s.IDNadredjena AS Prijemnica, s.RB,
           RTRIM(CONVERT(nvarchar(500), s.Naziv)) AS Naziv,
           RTRIM(CONVERT(nvarchar(500), s.Naimenovanje)) AS Naimenovanje,
           s.IDNaimenovanja,
           RTRIM(CONVERT(nvarchar(20), s.NHM)) AS NHM,
           ISNULL(p.BrojKontejnera, N'') AS Kontejner,
           ISNULL(RTRIM(CONVERT(nvarchar(200), sk.Naziv)), ISNULL(p.Pozicija, N'')) AS Polje,
           CASE p.Status WHEN 1 THEN N'Planirana' WHEN 2 THEN N'Na polju' ELSE N'Izdata' END AS StatusPalete,
           pr.Datum AS DatumPrijema,
           ps.Koleta AS NaPaleti,
           ISNULL(izd.Koleta, 0) AS Izdato,
           ISNULL(rezD.Koleta, 0) AS RezervisanoDrugi,
           ISNULL(STUFF((SELECT DISTINCT N', ' + CONVERT(nvarchar(10), x.RN)
                         FROM dbo.RNCarinskoPaletaIzlaz x
                         JOIN dbo.RadniNalogSkladista r ON r.ID = x.RN
                         WHERE x.IDPalete = p.ID AND x.IDStavke = ps.IDStavke AND x.Status = 1 AND x.RN <> @RN
                         FOR XML PATH(''), TYPE).value('.', 'nvarchar(max)'), 1, 2, N''), N'') AS NaloziRezervacije,
           ISNULL(rezO.Koleta, 0) AS RezervisanoOvaj,
           ps.Koleta - ISNULL(izd.Koleta, 0) - ISNULL(rezD.Koleta, 0) AS Slobodno
    FROM dbo.RNCarinskoPaleta p
    JOIN dbo.RNCarinskoPaletaStavka ps ON ps.IDPalete = p.ID
    JOIN dbo.RNCarinskoPrijemnicaStavke s ON s.ID = ps.IDStavke
    JOIN dbo.RNCarinskoSkladistePrijemnica pr ON pr.ID = p.IDPrijemnice
    JOIN dbo.RadniNalogSkladista rn ON rn.ID = pr.RN
    LEFT JOIN dbo.Skladista sk ON sk.ID = p.IDSkladista
    OUTER APPLY (SELECT SUM(x.Koleta) AS Koleta FROM dbo.RNCarinskoPaletaIzlaz x
                 WHERE x.IDPalete = p.ID AND x.IDStavke = ps.IDStavke AND x.Status = 2) izd
    OUTER APPLY (SELECT SUM(x.Koleta) AS Koleta FROM dbo.RNCarinskoPaletaIzlaz x
                 JOIN dbo.RadniNalogSkladista r ON r.ID = x.RN
                 WHERE x.IDPalete = p.ID AND x.IDStavke = ps.IDStavke AND x.Status = 1 AND x.RN <> @RN) rezD
    OUTER APPLY (SELECT SUM(x.Koleta) AS Koleta FROM dbo.RNCarinskoPaletaIzlaz x
                 WHERE x.IDPalete = p.ID AND x.IDStavke = ps.IDStavke AND x.Status = 1 AND x.RN = @RN) rezO
    WHERE RTRIM(pr.Status) = N'ZA'
      AND rn.MagacinskiBroj = @MagacinskiBroj
      AND ps.Koleta - ISNULL(izd.Koleta, 0) > 0
    ORDER BY pr.Datum, pr.ID, p.BrojPalete, s.RB;
END
GO

/* ---------------------------------------------------------------------------------------------
   5. Rezervacije naloga otpreme: zbir po stavci prijemnice (za listu stavki otpreme) i po paleti
   --------------------------------------------------------------------------------------------- */
CREATE OR ALTER PROCEDURE dbo.sp_Izlaz_RezervacijeNaloga
    @RN int
AS
BEGIN
    SET NOCOUNT ON;
    -- 1. po stavci prijemnice
    SELECT x.IDStavke, s.IDNadredjena AS Prijemnica, s.RB AS PrijemRB,
           RTRIM(CONVERT(nvarchar(500), s.Naimenovanje)) AS Naimenovanje,
           RTRIM(CONVERT(nvarchar(20), s.NHM)) AS NHM,
           RTRIM(CONVERT(nvarchar(500), s.Naziv)) AS Naziv,
           RTRIM(CONVERT(nvarchar(50), s.JM)) AS JM,
           RTRIM(CONVERT(nvarchar(50), s.Valuta)) AS Valuta,
           -- pozicija stavke prijemnice: postojeći lager otpreme po njoj povezuje stavku otpreme sa prijemom
           RTRIM(CONVERT(nvarchar(200), ISNULL(s.Pozicija, N''))) AS Pozicija,
           s.VrstaPaleta, tp.Dimenzije AS DimenzijaPaleta,
           (SELECT RTRIM(CONVERT(nvarchar(50), art.Sifra)) FROM dbo.Artikli art WHERE art.ID = s.IDArtikla) AS SifraArtikla,
           ISNULL(s.PDV, 0) AS PDV, ISNULL(s.Carina, 0) AS Carina,
           SUM(x.Koleta) AS Koleta, SUM(ISNULL(x.Bruto, 0)) AS Bruto, SUM(ISNULL(x.Neto, 0)) AS Neto, SUM(ISNULL(x.Vrednost, 0)) AS Vrednost,
           COUNT(DISTINCT x.IDPalete) AS BrojPaleta,
           -- polje sa najviše koleta ove stavke (kao Pozicija stavke na prijemnici)
           (SELECT TOP 1 ISNULL(RTRIM(CONVERT(nvarchar(200), sk.Naziv)), ISNULL(p2.Pozicija, N''))
            FROM dbo.RNCarinskoPaletaIzlaz x2
            JOIN dbo.RNCarinskoPaleta p2 ON p2.ID = x2.IDPalete
            LEFT JOIN dbo.Skladista sk ON sk.ID = p2.IDSkladista
            WHERE x2.RN = @RN AND x2.IDStavke = x.IDStavke AND x2.Status = 1
            GROUP BY sk.Naziv, p2.Pozicija ORDER BY SUM(x2.Koleta) DESC) AS Polje
    FROM dbo.RNCarinskoPaletaIzlaz x
    JOIN dbo.RNCarinskoPrijemnicaStavke s ON s.ID = x.IDStavke
    LEFT JOIN dbo.TipPalete tp ON tp.ID = s.VrstaPaleta
    WHERE x.RN = @RN AND x.Status = 1
    GROUP BY x.IDStavke, s.IDNadredjena, s.RB, s.Naimenovanje, s.NHM, s.Naziv, s.JM, s.Valuta, s.Pozicija, s.VrstaPaleta, tp.Dimenzije,
             s.IDArtikla, s.PDV, s.Carina
    ORDER BY s.IDNadredjena, s.RB;

    -- 2. po paleti (nalog rukovaocu, pregled)
    SELECT x.ID, x.IDPalete, p.BrojPalete, x.IDStavke, s.RB AS PrijemRB, RTRIM(CONVERT(nvarchar(500), s.Naziv)) AS Naziv,
           ISNULL(p.BrojKontejnera, N'') AS Kontejner,
           ISNULL(RTRIM(CONVERT(nvarchar(200), sk.Naziv)), ISNULL(p.Pozicija, N'')) AS Polje,
           x.Koleta, x.Bruto, x.Neto, x.Vrednost,
           ps.Koleta - ISNULL((SELECT SUM(y.Koleta) FROM dbo.RNCarinskoPaletaIzlaz y
                               WHERE y.IDPalete = x.IDPalete AND y.IDStavke = x.IDStavke AND y.Status = 2), 0) AS NaPaletiPre,
           x.Status
    FROM dbo.RNCarinskoPaletaIzlaz x
    JOIN dbo.RNCarinskoPaleta p ON p.ID = x.IDPalete
    JOIN dbo.RNCarinskoPaletaStavka ps ON ps.IDPalete = x.IDPalete AND ps.IDStavke = x.IDStavke
    JOIN dbo.RNCarinskoPrijemnicaStavke s ON s.ID = x.IDStavke
    LEFT JOIN dbo.Skladista sk ON sk.ID = p.IDSkladista
    WHERE x.RN = @RN AND x.Status = 1
    ORDER BY Polje, p.BrojPalete, s.RB;
END
GO

/* ---------------------------------------------------------------------------------------------
   6. Rezerviše palete za nalog otpreme (zamenjuje sve dosadašnje rezervacije naloga).
   @Izbor: <i p="IDPalete" s="IDStavke" k="Koleta"/>... ; prazno = oslobađa sve rezervacije naloga.
   Bruto, neto i vrednost: srazmerno koletima stavke; kad posle ovog naloga stavka ostaje bez koleta
   (svi izlazi i rezervacije zajedno), nalog uzima tačan ostatak bruta, neta i vrednosti stavke.
   --------------------------------------------------------------------------------------------- */
CREATE OR ALTER PROCEDURE dbo.sp_Izlaz_Rezervisi
    @RN       int,
    @Izbor    nvarchar(max),
    @Korisnik nvarchar(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @mb int = (SELECT MagacinskiBroj FROM dbo.RadniNalogSkladista WHERE ID = @RN);
    IF @mb IS NULL
    BEGIN RAISERROR(N'Nalog otpreme ne postoji; prvo ga snimite.', 16, 1); RETURN; END
    IF EXISTS (SELECT 1 FROM dbo.RNCarinskoPaletaIzlaz WHERE RN = @RN AND Status = 2)
    BEGIN RAISERROR(N'Izlaz ovog naloga je već potvrđen; rezervacije se više ne menjaju.', 16, 1); RETURN; END

    DECLARE @n TABLE (IDPalete int NOT NULL, IDStavke int NOT NULL, Koleta int NOT NULL, PRIMARY KEY (IDPalete, IDStavke));
    IF LTRIM(RTRIM(ISNULL(@Izbor, N''))) <> N''
    BEGIN
        DECLARE @x xml = CAST(N'<r>' + @Izbor + N'</r>' AS xml);
        INSERT @n (IDPalete, IDStavke, Koleta)
        SELECT d.p, d.s, SUM(d.k)
        FROM (SELECT t.c.value('@p', 'int') AS p, t.c.value('@s', 'int') AS s, t.c.value('@k', 'int') AS k
              FROM @x.nodes('/r/i') t(c)) d
        GROUP BY d.p, d.s
        HAVING SUM(d.k) <> 0;
    END

    IF EXISTS (SELECT 1 FROM @n WHERE Koleta < 0)
    BEGIN RAISERROR(N'Koleta za izlaz ne mogu biti negativna.', 16, 1); RETURN; END

    DECLARE @poruka nvarchar(2000);
    -- stavka mora biti na toj paleti, prijemnica potvrđena, isti magacinski broj
    SELECT TOP 1 @poruka = N'Paleta ' + ISNULL(CONVERT(nvarchar(20), p.BrojPalete), CONVERT(nvarchar(10), n.IDPalete))
                         + N' ne nosi izabranu stavku ili nije na potvrđenoj prijemnici ovog magacinskog broja.'
    FROM @n n
    LEFT JOIN dbo.RNCarinskoPaleta p ON p.ID = n.IDPalete
    WHERE NOT EXISTS (SELECT 1 FROM dbo.RNCarinskoPaletaStavka ps
                      JOIN dbo.RNCarinskoPaleta p2 ON p2.ID = ps.IDPalete
                      JOIN dbo.RNCarinskoSkladistePrijemnica pr ON pr.ID = p2.IDPrijemnice
                      JOIN dbo.RadniNalogSkladista r ON r.ID = pr.RN
                      WHERE ps.IDPalete = n.IDPalete AND ps.IDStavke = n.IDStavke
                        AND RTRIM(pr.Status) = N'ZA' AND r.MagacinskiBroj = @mb);
    IF @poruka IS NOT NULL
    BEGIN RAISERROR(@poruka, 16, 1); RETURN; END

    BEGIN TRAN;
    DELETE FROM dbo.RNCarinskoPaletaIzlaz WHERE RN = @RN AND Status = 1;

    -- slobodno na paleti (zaključava redove izlaza tih paleta do kraja transakcije)
    SELECT TOP 1 @poruka = N'Paleta ' + CONVERT(nvarchar(20), p.BrojPalete) + N', RB ' + CONVERT(nvarchar(10), s.RB)
                         + N': traženo ' + CONVERT(nvarchar(10), n.Koleta) + N', slobodno '
                         + CONVERT(nvarchar(10), ps.Koleta - ISNULL(z.Izdato, 0) - ISNULL(z.Rezervisano, 0)) + N'.'
                         + CASE WHEN z.Nalog IS NOT NULL THEN N' Paleta je rezervisana nalogom ' + CONVERT(nvarchar(10), z.Nalog) + N'.' ELSE N'' END
    FROM @n n
    JOIN dbo.RNCarinskoPaletaStavka ps ON ps.IDPalete = n.IDPalete AND ps.IDStavke = n.IDStavke
    JOIN dbo.RNCarinskoPaleta p ON p.ID = n.IDPalete
    JOIN dbo.RNCarinskoPrijemnicaStavke s ON s.ID = n.IDStavke
    OUTER APPLY (SELECT SUM(CASE WHEN x.Status = 2 THEN x.Koleta ELSE 0 END) AS Izdato,
                        SUM(CASE WHEN x.Status = 1 AND r.ID IS NOT NULL THEN x.Koleta ELSE 0 END) AS Rezervisano,
                        MIN(CASE WHEN x.Status = 1 AND r.ID IS NOT NULL THEN x.RN END) AS Nalog
                 FROM dbo.RNCarinskoPaletaIzlaz x WITH (UPDLOCK, HOLDLOCK)
                 LEFT JOIN dbo.RadniNalogSkladista r ON r.ID = x.RN
                 WHERE x.IDPalete = n.IDPalete AND x.IDStavke = n.IDStavke) z
    WHERE n.Koleta > ps.Koleta - ISNULL(z.Izdato, 0) - ISNULL(z.Rezervisano, 0);
    IF @poruka IS NOT NULL
    BEGIN
        ROLLBACK;
        RAISERROR(@poruka, 16, 1);
        RETURN;
    END

    -- iznosi po stavci za ovaj nalog
    DECLARE @st TABLE (IDStavke int PRIMARY KEY, KoletaRN int, Bruto decimal(18,3), Neto decimal(18,3), Vrednost decimal(18,2));
    INSERT @st (IDStavke, KoletaRN, Bruto, Neto, Vrednost)
    SELECT n.IDStavke, n.KoletaRN,
           CASE WHEN n.KoletaRN >= ISNULL(s.Koleta, 0) - ISNULL(o.Koleta, 0) THEN ISNULL(s.Bruto, 0) - ISNULL(o.Bruto, 0)
                ELSE ROUND(ISNULL(s.Bruto, 0) * n.KoletaRN / NULLIF(s.Koleta, 0), 3) END,
           CASE WHEN n.KoletaRN >= ISNULL(s.Koleta, 0) - ISNULL(o.Koleta, 0) THEN ISNULL(s.Neto, 0) - ISNULL(o.Neto, 0)
                ELSE ROUND(ISNULL(s.Neto, 0) * n.KoletaRN / NULLIF(s.Koleta, 0), 3) END,
           CASE WHEN n.KoletaRN >= ISNULL(s.Koleta, 0) - ISNULL(o.Koleta, 0) THEN ISNULL(s.Vrednost, 0) - ISNULL(o.Vrednost, 0)
                ELSE ROUND(ISNULL(s.Vrednost, 0) * n.KoletaRN / NULLIF(s.Koleta, 0), 2) END
    FROM (SELECT IDStavke, SUM(Koleta) AS KoletaRN FROM @n GROUP BY IDStavke) n
    JOIN dbo.RNCarinskoPrijemnicaStavke s ON s.ID = n.IDStavke
    OUTER APPLY (SELECT SUM(x.Koleta) AS Koleta, SUM(ISNULL(x.Bruto, 0)) AS Bruto, SUM(ISNULL(x.Neto, 0)) AS Neto, SUM(ISNULL(x.Vrednost, 0)) AS Vrednost
                 FROM dbo.RNCarinskoPaletaIzlaz x
                 LEFT JOIN dbo.RadniNalogSkladista r ON r.ID = x.RN
                 WHERE x.IDStavke = n.IDStavke AND (x.Status = 2 OR r.ID IS NOT NULL)) o;

    UPDATE @st SET Bruto = CASE WHEN Bruto < 0 THEN 0 ELSE Bruto END,
                   Neto = CASE WHEN Neto < 0 THEN 0 ELSE Neto END,
                   Vrednost = CASE WHEN Vrednost < 0 THEN 0 ELSE Vrednost END;

    -- raspodela iznosa stavke na palete: kumulativno zaokruživanje, zbir je tačno iznos stavke
    ;WITH r AS (
        SELECT n.IDPalete, n.IDStavke, n.Koleta, st.KoletaRN, st.Bruto, st.Neto, st.Vrednost,
               SUM(n.Koleta) OVER (PARTITION BY n.IDStavke ORDER BY n.IDPalete ROWS UNBOUNDED PRECEDING) AS Kum
        FROM @n n JOIN @st st ON st.IDStavke = n.IDStavke
        WHERE n.Koleta > 0)
    INSERT dbo.RNCarinskoPaletaIzlaz (IDPalete, IDStavke, RN, Koleta, Bruto, Neto, Vrednost, Status, Korisnik)
    SELECT IDPalete, IDStavke, @RN, Koleta,
           ROUND(Bruto * Kum / KoletaRN, 3) - ROUND(Bruto * (Kum - Koleta) / KoletaRN, 3),
           ROUND(Neto * Kum / KoletaRN, 3) - ROUND(Neto * (Kum - Koleta) / KoletaRN, 3),
           ROUND(Vrednost * Kum / KoletaRN, 2) - ROUND(Vrednost * (Kum - Koleta) / KoletaRN, 2),
           1, @Korisnik
    FROM r;

    COMMIT;
END
GO

/* ---------------------------------------------------------------------------------------------
   7. Oslobađa rezervacije naloga (cele ili jedne stavke prijemnice)
   --------------------------------------------------------------------------------------------- */
CREATE OR ALTER PROCEDURE dbo.sp_Izlaz_Oslobodi
    @RN       int,
    @IDStavke int = NULL
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM dbo.RNCarinskoPaletaIzlaz
    WHERE RN = @RN AND Status = 1 AND (@IDStavke IS NULL OR IDStavke = @IDStavke);
    SELECT @@ROWCOUNT AS Oslobodjeno;
END
GO

/* ---------------------------------------------------------------------------------------------
   8. Potvrda izlaza otpremnice: rezervacije naloga postaju izdato. Paleta bez stanja dobija
   status 3 (Izdata) i skida se sa polja; promena polja ide u log (polje se ne briše).
   Promet skladišta se ovde NE menja (vidi izveštaj u delu 11).
   --------------------------------------------------------------------------------------------- */
CREATE OR ALTER PROCEDURE dbo.sp_Izlaz_Potvrdi
    @IDOtpremnice int,
    @Korisnik     nvarchar(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @RN int = (SELECT RN FROM dbo.RNCarinskoSkladisteOtpremnica WHERE ID = @IDOtpremnice);
    IF @RN IS NULL
    BEGIN RAISERROR(N'Otpremnica ne postoji ili nema nalog otpreme.', 16, 1); RETURN; END
    IF NOT EXISTS (SELECT 1 FROM dbo.RNCarinskoPaletaIzlaz WHERE RN = @RN AND Status = 1)
    BEGIN
        IF EXISTS (SELECT 1 FROM dbo.RNCarinskoPaletaIzlaz WHERE RN = @RN AND Status = 2)
            RAISERROR(N'Izlaz paleta ove otpremnice je već potvrđen.', 16, 1);
        ELSE
            RAISERROR(N'Nalog otpreme nema izabranih paleta (Otprema > Izbor paleta).', 16, 1);
        RETURN;
    END

    DECLARE @palete TABLE (IDPalete int PRIMARY KEY);
    BEGIN TRAN;
    -- ponovna provera stanja: izdato + ova rezervacija ne sme preći koleta na paleti
    DECLARE @poruka nvarchar(1000);
    SELECT TOP 1 @poruka = N'Paleta ' + CONVERT(nvarchar(20), p.BrojPalete) + N' nema dovoljno koleta za izlaz (na paleti '
                         + CONVERT(nvarchar(10), ps.Koleta) + N', već izdato ' + CONVERT(nvarchar(10), ISNULL(z.Izdato, 0))
                         + N', ova otpremnica ' + CONVERT(nvarchar(10), x.Koleta) + N').'
    FROM (SELECT IDPalete, IDStavke, SUM(Koleta) AS Koleta FROM dbo.RNCarinskoPaletaIzlaz WITH (UPDLOCK, HOLDLOCK)
          WHERE RN = @RN AND Status = 1 GROUP BY IDPalete, IDStavke) x
    JOIN dbo.RNCarinskoPaletaStavka ps ON ps.IDPalete = x.IDPalete AND ps.IDStavke = x.IDStavke
    JOIN dbo.RNCarinskoPaleta p ON p.ID = x.IDPalete
    OUTER APPLY (SELECT SUM(y.Koleta) AS Izdato FROM dbo.RNCarinskoPaletaIzlaz y
                 WHERE y.IDPalete = x.IDPalete AND y.IDStavke = x.IDStavke AND y.Status = 2) z
    WHERE x.Koleta + ISNULL(z.Izdato, 0) > ps.Koleta;
    IF @poruka IS NOT NULL
    BEGIN
        ROLLBACK;
        RAISERROR(@poruka, 16, 1);
        RETURN;
    END

    UPDATE dbo.RNCarinskoPaletaIzlaz
    SET Status = 2, IDOtpremnice = @IDOtpremnice, DatumIzlaza = GETDATE(), KorisnikIzlaza = @Korisnik
    WHERE RN = @RN AND Status = 1;

    INSERT @palete (IDPalete)
    SELECT DISTINCT IDPalete FROM dbo.RNCarinskoPaletaIzlaz WHERE IDOtpremnice = @IDOtpremnice AND Status = 2;

    -- palete bez stanja: status 3, sa polja (log)
    DECLARE @prazne TABLE (IDPalete int PRIMARY KEY, IDSkladista int NULL);
    INSERT @prazne (IDPalete, IDSkladista)
    SELECT p.ID, p.IDSkladista
    FROM dbo.RNCarinskoPaleta p
    JOIN @palete l ON l.IDPalete = p.ID
    WHERE p.Status <> 3
      AND (SELECT SUM(ps.Koleta) FROM dbo.RNCarinskoPaletaStavka ps WHERE ps.IDPalete = p.ID)
        <= (SELECT ISNULL(SUM(x.Koleta), 0) FROM dbo.RNCarinskoPaletaIzlaz x WHERE x.IDPalete = p.ID AND x.Status = 2);

    INSERT dbo.RNCarinskoPaletaPoljeLog (IDPalete, IDSkladistaStaro, IDSkladistaNovo, Korisnik)
    SELECT IDPalete, IDSkladista, NULL, @Korisnik FROM @prazne WHERE IDSkladista IS NOT NULL;

    UPDATE p SET Status = 3, IDSkladista = NULL, DatumIzmene = GETDATE()   -- Pozicija ostaje: poslednje polje palete
    FROM dbo.RNCarinskoPaleta p JOIN @prazne z ON z.IDPalete = p.ID;

    COMMIT;

    SELECT (SELECT COUNT(*) FROM @palete) AS Palete,
           (SELECT COUNT(*) FROM @prazne) AS IzdatePalete,
           (SELECT ISNULL(SUM(Koleta), 0) FROM dbo.RNCarinskoPaletaIzlaz WHERE IDOtpremnice = @IDOtpremnice AND Status = 2) AS Koleta;
END
GO

/* ---------------------------------------------------------------------------------------------
   9. Palete otpremnice (izdate ili rezervisane nalogom otpremnice), po polju: za nalog rukovaocu
   --------------------------------------------------------------------------------------------- */
CREATE OR ALTER PROCEDURE dbo.sp_Izlaz_PaleteOtpremnice
    @IDOtpremnice int
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @RN int = (SELECT RN FROM dbo.RNCarinskoSkladisteOtpremnica WHERE ID = @IDOtpremnice);
    SELECT ISNULL(RTRIM(CONVERT(nvarchar(200), sk.Naziv)), ISNULL(p.Pozicija, N'')) AS Polje,
           p.BrojPalete, s.RB AS PrijemRB, RTRIM(CONVERT(nvarchar(500), s.Naziv)) AS Naziv,
           ISNULL(p.BrojKontejnera, N'') AS Kontejner, x.Koleta,
           -- cela: uzima sve što je na paleti ostalo od te stavke pre ovog izlaza
           CASE WHEN x.Koleta >= ps.Koleta - ISNULL((SELECT SUM(y.Koleta) FROM dbo.RNCarinskoPaletaIzlaz y
                                                    WHERE y.IDPalete = x.IDPalete AND y.IDStavke = x.IDStavke
                                                      AND y.Status = 2 AND y.ID < x.ID), 0)
                THEN N'cela' ELSE N'deo' END AS Uzima,
           CASE x.Status WHEN 1 THEN N'Rezervisano' ELSE N'Izdato' END AS Status
    FROM dbo.RNCarinskoPaletaIzlaz x
    JOIN dbo.RNCarinskoPaleta p ON p.ID = x.IDPalete
    JOIN dbo.RNCarinskoPaletaStavka ps ON ps.IDPalete = x.IDPalete AND ps.IDStavke = x.IDStavke
    JOIN dbo.RNCarinskoPrijemnicaStavke s ON s.ID = x.IDStavke
    LEFT JOIN dbo.Skladista sk ON sk.ID = p.IDSkladista
    WHERE x.IDOtpremnice = @IDOtpremnice OR (x.RN = @RN AND x.Status = 1)
    ORDER BY Polje, p.BrojPalete, s.RB;
END
GO

/* ---------------------------------------------------------------------------------------------
   10. Dokument razduženja (RAZ-14)
   --------------------------------------------------------------------------------------------- */
-- Naimenovanja C7 jednog magacinskog broja (ili naimenovanja jednog dokumenta razduženja) sa stanjem.
-- Prijavljeno = naimenovanje (C7). Razduženo = svi dokumenti osim @IDRazduzenja. Ovaj dokument = @IDRazduzenja.
CREATE OR ALTER PROCEDURE dbo.sp_Razduzenje_Naimenovanja
    @MagacinskiBroj int = NULL,
    @IDRazduzenja   int = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT n.ID AS IDNaimenovanja, rns.MagacinskiBroj, mp.ID AS MagacinskiPredmet,
           RTRIM(CONVERT(nvarchar(100), ISNULL(rns.SmestajniDokument, N''))) AS C7,
           n.RB, RTRIM(CONVERT(nvarchar(500), n.Naimenovanje)) AS Naimenovanje,
           RTRIM(CONVERT(nvarchar(20), n.NHM)) AS NHM,
           RTRIM(CONVERT(nvarchar(20), n.Valuta)) AS Valuta,
           ISNULL(n.Koleta, 0) AS PrijavljenoKoleta, ISNULL(n.Neto, 0) AS PrijavljenoNeto,
           ISNULL(n.Bruto, 0) AS PrijavljenoBruto, ISNULL(n.Vrednost, 0) AS PrijavljenoVrednost,
           ISNULL(d.Koleta, 0) AS RazduzenoKoleta, ISNULL(d.Neto, 0) AS RazduzenoNeto,
           ISNULL(d.Bruto, 0) AS RazduzenoBruto, ISNULL(d.Vrednost, 0) AS RazduzenoVrednost,
           ISNULL(o.Koleta, 0) AS Koleta, ISNULL(o.Neto, 0) AS Neto, ISNULL(o.Bruto, 0) AS Bruto, ISNULL(o.Vrednost, 0) AS Vrednost
    FROM dbo.RNCarinskoStavke n
    JOIN dbo.MagacinskiPredmet mp ON mp.ID = n.IDNadredjena
    JOIN dbo.RadniNalogSkladista rns ON rns.ID = mp.RN
    OUTER APPLY (SELECT SUM(rs.Koleta) AS Koleta, SUM(rs.Neto) AS Neto, SUM(rs.Bruto) AS Bruto, SUM(rs.Vrednost) AS Vrednost
                 FROM dbo.RNCarinskoRazduzenjeStavka rs
                 WHERE rs.IDNaimenovanja = n.ID AND rs.IDRazduzenja <> ISNULL(@IDRazduzenja, -1)) d
    OUTER APPLY (SELECT rs.Koleta, rs.Neto, rs.Bruto, rs.Vrednost
                 FROM dbo.RNCarinskoRazduzenjeStavka rs
                 WHERE rs.IDNaimenovanja = n.ID AND rs.IDRazduzenja = @IDRazduzenja) o
    WHERE (@MagacinskiBroj IS NOT NULL AND rns.MagacinskiBroj = @MagacinskiBroj)
       OR (@IDRazduzenja IS NOT NULL AND EXISTS (SELECT 1 FROM dbo.RNCarinskoRazduzenjeStavka rs
                                                  WHERE rs.IDRazduzenja = @IDRazduzenja AND rs.IDNaimenovanja = n.ID))
    ORDER BY rns.MagacinskiBroj, n.RB;
END
GO

-- Snima zaglavlje, stavke (zamenjuju se) i veze sa otpremnicama (zamenjuju se).
-- @Stavke: <i n="IDNaimenovanja" k="koleta" ne="neto" b="bruto" v="vrednost"/>...  (decimalna tačka)
-- @Otpremnice: ID otpremnica odvojeni zarezom.
CREATE OR ALTER PROCEDURE dbo.sp_Razduzenje_Snimi
    @ID             int = NULL OUTPUT,
    @CI             nvarchar(10),
    @Vrsta          nvarchar(10),
    @Broj           nvarchar(20),
    @Godina         smallint,
    @DatumIzdavanja date = NULL,
    @Valuta         nvarchar(10) = NULL,
    @Kurs           decimal(18,6) = NULL,
    @Napomena       nvarchar(500) = NULL,
    @Stavke         nvarchar(max),
    @Otpremnice     nvarchar(max) = NULL,
    @Korisnik       nvarchar(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    SET @CI = LTRIM(RTRIM(ISNULL(@CI, N'')));
    SET @Vrsta = UPPER(LTRIM(RTRIM(ISNULL(@Vrsta, N''))));
    SET @Broj = LTRIM(RTRIM(ISNULL(@Broj, N'')));
    IF @Vrsta = N'' OR @Broj = N'' OR ISNULL(@Godina, 0) < 2000
    BEGIN RAISERROR(N'Unesite vrstu, broj i godinu dokumenta razduženja.', 16, 1); RETURN; END
    IF EXISTS (SELECT 1 FROM dbo.RNCarinskoRazduzenje
               WHERE CI = @CI AND Vrsta = @Vrsta AND Broj = @Broj AND Godina = @Godina AND ID <> ISNULL(@ID, -1))
    BEGIN RAISERROR(N'Dokument razduženja sa istim CI, vrstom, brojem i godinom već postoji.', 16, 1); RETURN; END

    DECLARE @s TABLE (IDNaimenovanja int PRIMARY KEY, Koleta decimal(18,3), Neto decimal(18,3), Bruto decimal(18,3), Vrednost decimal(18,2));
    DECLARE @x xml = CAST(N'<r>' + ISNULL(@Stavke, N'') + N'</r>' AS xml);
    INSERT @s (IDNaimenovanja, Koleta, Neto, Bruto, Vrednost)
    SELECT d.n, SUM(ISNULL(d.k, 0)), SUM(ISNULL(d.ne, 0)), SUM(ISNULL(d.b, 0)), SUM(ISNULL(d.v, 0))
    FROM (SELECT t.c.value('@n', 'int') AS n,
                 t.c.value('@k', 'decimal(18,3)') AS k, t.c.value('@ne', 'decimal(18,3)') AS ne,
                 t.c.value('@b', 'decimal(18,3)') AS b, t.c.value('@v', 'decimal(18,2)') AS v
          FROM @x.nodes('/r/i') t(c)) d
    GROUP BY d.n;
    DELETE FROM @s WHERE Koleta = 0 AND Neto = 0 AND Bruto = 0 AND Vrednost = 0;

    IF NOT EXISTS (SELECT 1 FROM @s)
    BEGIN RAISERROR(N'Dokument nema nijedno naimenovanje sa količinom.', 16, 1); RETURN; END
    IF EXISTS (SELECT 1 FROM @s WHERE Koleta < 0 OR Neto < 0 OR Bruto < 0 OR Vrednost < 0)
    BEGIN RAISERROR(N'Količine i vrednost ne mogu biti negativne.', 16, 1); RETURN; END
    IF EXISTS (SELECT 1 FROM @s s WHERE NOT EXISTS (SELECT 1 FROM dbo.RNCarinskoStavke n WHERE n.ID = s.IDNaimenovanja))
    BEGIN RAISERROR(N'Neko od naimenovanja više ne postoji.', 16, 1); RETURN; END

    -- ne sme preći nerazduženo (prijavljeno na C7 minus ostali dokumenti)
    DECLARE @poruka nvarchar(2000);
    SELECT TOP 1 @poruka = N'RB ' + CONVERT(nvarchar(10), n.RB) + N' ' + LEFT(RTRIM(CONVERT(nvarchar(200), n.Naimenovanje)), 60)
                         + N': razduženje prelazi nerazduženo ('
                         + CASE WHEN s.Koleta > ISNULL(n.Koleta, 0) - ISNULL(d.Koleta, 0) + 0.0005 THEN N'koleta ' + CONVERT(nvarchar(30), ISNULL(n.Koleta, 0) - ISNULL(d.Koleta, 0))
                                WHEN s.Neto > ISNULL(n.Neto, 0) - ISNULL(d.Neto, 0) + 0.0005 THEN N'neto ' + CONVERT(nvarchar(30), ISNULL(n.Neto, 0) - ISNULL(d.Neto, 0))
                                WHEN s.Bruto > ISNULL(n.Bruto, 0) - ISNULL(d.Bruto, 0) + 0.0005 THEN N'bruto ' + CONVERT(nvarchar(30), ISNULL(n.Bruto, 0) - ISNULL(d.Bruto, 0))
                                ELSE N'vrednost ' + CONVERT(nvarchar(30), ISNULL(n.Vrednost, 0) - ISNULL(d.Vrednost, 0)) END + N').'
    FROM @s s
    JOIN dbo.RNCarinskoStavke n ON n.ID = s.IDNaimenovanja
    OUTER APPLY (SELECT SUM(rs.Koleta) AS Koleta, SUM(rs.Neto) AS Neto, SUM(rs.Bruto) AS Bruto, SUM(rs.Vrednost) AS Vrednost
                 FROM dbo.RNCarinskoRazduzenjeStavka rs WITH (UPDLOCK, HOLDLOCK)
                 WHERE rs.IDNaimenovanja = s.IDNaimenovanja AND rs.IDRazduzenja <> ISNULL(@ID, -1)) d
    WHERE s.Koleta > ISNULL(n.Koleta, 0) - ISNULL(d.Koleta, 0) + 0.0005
       OR s.Neto > ISNULL(n.Neto, 0) - ISNULL(d.Neto, 0) + 0.0005
       OR s.Bruto > ISNULL(n.Bruto, 0) - ISNULL(d.Bruto, 0) + 0.0005
       OR s.Vrednost > ISNULL(n.Vrednost, 0) - ISNULL(d.Vrednost, 0) + 0.005;
    IF @poruka IS NOT NULL
    BEGIN RAISERROR(@poruka, 16, 1); RETURN; END

    DECLARE @o TABLE (IDOtpremnice int PRIMARY KEY);
    IF LTRIM(RTRIM(ISNULL(@Otpremnice, N''))) <> N''
    BEGIN
        DECLARE @xo xml = CAST(N'<i>' + REPLACE(REPLACE(@Otpremnice, N' ', N''), N',', N'</i><i>') + N'</i>' AS xml);
        INSERT @o (IDOtpremnice)
        SELECT DISTINCT t.c.value('.', 'int') FROM @xo.nodes('/i') t(c) WHERE t.c.value('.', 'nvarchar(20)') <> N'';
    END
    IF EXISTS (SELECT 1 FROM @o o WHERE NOT EXISTS (SELECT 1 FROM dbo.RNCarinskoSkladisteOtpremnica ot WHERE ot.ID = o.IDOtpremnice))
    BEGIN RAISERROR(N'Neka od izabranih otpremnica ne postoji.', 16, 1); RETURN; END

    BEGIN TRAN;
    IF @ID IS NULL OR NOT EXISTS (SELECT 1 FROM dbo.RNCarinskoRazduzenje WHERE ID = @ID)
    BEGIN
        INSERT dbo.RNCarinskoRazduzenje (CI, Vrsta, Broj, Godina, DatumIzdavanja, Valuta, Kurs, Napomena, Korisnik)
        VALUES (@CI, @Vrsta, @Broj, @Godina, @DatumIzdavanja, NULLIF(LTRIM(RTRIM(@Valuta)), N''), @Kurs, NULLIF(LTRIM(RTRIM(@Napomena)), N''), @Korisnik);
        SET @ID = SCOPE_IDENTITY();
    END
    ELSE
        UPDATE dbo.RNCarinskoRazduzenje
        SET CI = @CI, Vrsta = @Vrsta, Broj = @Broj, Godina = @Godina, DatumIzdavanja = @DatumIzdavanja,
            Valuta = NULLIF(LTRIM(RTRIM(@Valuta)), N''), Kurs = @Kurs, Napomena = NULLIF(LTRIM(RTRIM(@Napomena)), N''),
            Korisnik = @Korisnik, DatumIzmene = GETDATE()
        WHERE ID = @ID;

    DELETE FROM dbo.RNCarinskoRazduzenjeStavka WHERE IDRazduzenja = @ID;
    INSERT dbo.RNCarinskoRazduzenjeStavka (IDRazduzenja, IDNaimenovanja, Koleta, Neto, Bruto, Vrednost)
    SELECT @ID, IDNaimenovanja, Koleta, Neto, Bruto, Vrednost FROM @s;

    DELETE FROM dbo.RNCarinskoOtpremnicaRazduzenje WHERE IDRazduzenja = @ID;
    INSERT dbo.RNCarinskoOtpremnicaRazduzenje (IDOtpremnice, IDRazduzenja)
    SELECT IDOtpremnice, @ID FROM @o;
    COMMIT;
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_Razduzenje_Obrisi
    @ID int
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRAN;
    DELETE FROM dbo.RNCarinskoOtpremnicaRazduzenje WHERE IDRazduzenja = @ID;
    DELETE FROM dbo.RNCarinskoRazduzenjeStavka WHERE IDRazduzenja = @ID;
    DELETE FROM dbo.RNCarinskoRazduzenje WHERE ID = @ID;
    COMMIT;
END
GO

-- Dokumenti razduženja: svi (najnoviji prvo) ili samo oni vezani za otpremnicu
CREATE OR ALTER PROCEDURE dbo.sp_Razduzenje_Lista
    @IDOtpremnice int = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT r.ID, r.CI, r.Vrsta, r.Broj, r.Godina, r.DatumIzdavanja, r.Valuta,
           RTRIM(r.Vrsta + N' ' + r.Broj + N'/' + CONVERT(nvarchar(4), r.Godina)) AS Dokument,
           ISNULL(STUFF((SELECT N', ' + CONVERT(nvarchar(10), x.IDOtpremnice)
                         FROM dbo.RNCarinskoOtpremnicaRazduzenje x WHERE x.IDRazduzenja = r.ID ORDER BY x.IDOtpremnice
                         FOR XML PATH(''), TYPE).value('.', 'nvarchar(max)'), 1, 2, N''), N'') AS Otpremnice,
           (SELECT SUM(rs.Koleta) FROM dbo.RNCarinskoRazduzenjeStavka rs WHERE rs.IDRazduzenja = r.ID) AS Koleta,
           (SELECT SUM(rs.Bruto) FROM dbo.RNCarinskoRazduzenjeStavka rs WHERE rs.IDRazduzenja = r.ID) AS Bruto,
           (SELECT SUM(rs.Vrednost) FROM dbo.RNCarinskoRazduzenjeStavka rs WHERE rs.IDRazduzenja = r.ID) AS Vrednost,
           r.Napomena
    FROM dbo.RNCarinskoRazduzenje r
    WHERE @IDOtpremnice IS NULL
       OR EXISTS (SELECT 1 FROM dbo.RNCarinskoOtpremnicaRazduzenje x WHERE x.IDRazduzenja = r.ID AND x.IDOtpremnice = @IDOtpremnice)
    ORDER BY r.DatumUnosa DESC, r.ID DESC;
END
GO

/* ---------------------------------------------------------------------------------------------
   11. IZVEŠTAJ (ništa se ne menja): šta postojeće procedure otpremnice rade.
   Lager otpreme računa koleta iz prometa skladišta (Promet), a bruto i vrednost iz stavki otpremnice.
   Tekst ovih procedura nije u repozitorijumu, pa se ne zna gde se izlaz knjiži u Promet.
   Potvrda izlaza paleta (deo 8) zato Promet ne dira. Tabelu poslati nazad.
   --------------------------------------------------------------------------------------------- */
SELECT o.name AS Procedura,
       CASE WHEN m.definition LIKE '%Promet%' THEN N'da' ELSE N'ne' END AS [Pominje Promet],
       CASE WHEN m.definition LIKE '%INSERT%Promet%' OR m.definition LIKE '%InsertPromet%' THEN N'da' ELSE N'ne' END AS [Upisuje u Promet],
       CASE WHEN m.definition LIKE '%RNCarinskoSkladisteOtpremnicaStavke%' THEN N'da' ELSE N'ne' END AS [Piše stavke otpremnice],
       CASE WHEN m.definition LIKE '%RNCarinskoSkladisteOtpremaStavkePom%' THEN N'da' ELSE N'ne' END AS [Čita stavke otpreme]
FROM sys.objects o
JOIN sys.sql_modules m ON m.object_id = o.object_id
WHERE o.name IN (N'InsertRNCarinskoSkladisteOtpremnica', N'UpdateRNCarinskoSkladisteOtpremnica',
                 N'InsertRNCarinskoRukovalac', N'InsertRnCarinskoSkladisteOtpremaStavkePom')
   OR (o.type IN ('P', 'TR') AND m.definition LIKE '%RNCarinskoSkladisteOtpremnicaStavke%')
ORDER BY o.name;
GO

PRINT 'Carina_Izlaz_2026_11.sql: završeno.';
GO
SET NOEXEC OFF;
GO
