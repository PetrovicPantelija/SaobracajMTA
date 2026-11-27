/* =============================================================================================
   Nalog za PRIJEM: dugme Dodaj u delu „Unos naimenovanja“ ne dodaje red (test 27.11.2026, nalog 84, MB 24)

   Delovi 1–4 samo čitaju. Deo 5 (proba) poziva InsertCarinskoStavke sa podacima iz testa u transakciji
   koja se na kraju poništava (ROLLBACK): u bazi ne ostaje ništa, samo se brojač ID-a u RNCarinskoStavke
   pomeri. Bez probe: @Proba = 0. Pustiti na test bazi.

   Pre pokretanja u SSMS: Tools > Options > Query Results > SQL Server > Results to Grid >
   Maximum Characters Retrieved > Non XML data = 65535, da se tekst procedure ne skrati.
   Rezultat (sve tabele i poruke) poslati nazad.
   ============================================================================================= */
SET NOCOUNT ON;

DECLARE @RN int = 84;        -- nalog za prijem iz testa
DECLARE @Proba bit = 1;      -- 1 = proba upisa sa ROLLBACK (deo 5)

/* ---------------------------------------------------------------------------------------------
   1. Da li su puštene skripte od kojih zavise tarifni broj, zamena tarife i transportne jedinice
   --------------------------------------------------------------------------------------------- */
SELECT N'Carina_TarifniBroj_2026_11.sql' AS Skripta, OBJECT_NAME(c.object_id) AS Tabela, TYPE_NAME(c.user_type_id) AS [Tip NHM],
       CASE WHEN TYPE_NAME(c.user_type_id) LIKE '%char%' THEN N'puštena' ELSE N'NIJE puštena (NHM je broj)' END AS Stanje
FROM sys.columns c
WHERE c.name = 'NHM'
  AND c.object_id IN (OBJECT_ID('dbo.RNCarinskoStavke'), OBJECT_ID('dbo.RNCarinskoPrijemnicaStavke'),
                      OBJECT_ID('dbo.RNCarinskoSkladisteOtpremaStavkePom'), OBJECT_ID('dbo.RNCarinskoSkladisteOtpremnicaStavke'))
UNION ALL
SELECT N'Carina_ZamenaTarife_2026_11.sql', N'sp_NHM_Zameni', NULL,
       CASE WHEN OBJECT_ID('dbo.sp_NHM_Zameni', 'P') IS NULL THEN N'NIJE puštena' ELSE N'puštena' END
UNION ALL
SELECT N'Carina_TransportneJedinice_2026_11.sql', N'RNCarinskoKontejner', NULL,
       CASE WHEN OBJECT_ID('dbo.RNCarinskoKontejner', 'U') IS NULL THEN N'tabela ne postoji: prvo Carina_Kontejneri_2026_11.sql'
            WHEN COL_LENGTH('dbo.RNCarinskoKontejner', 'Tip') IS NULL THEN N'NIJE puštena (nema kolone Tip): vagoni i kamioni se ne snimaju'
            ELSE N'puštena' END;

-- parametri procedura za naimenovanja (posle Carina_TarifniBroj_2026_11.sql @NHM je varchar(10))
SELECT OBJECT_NAME(p.object_id) AS Procedura, p.parameter_id, p.name AS Parametar, TYPE_NAME(p.user_type_id) AS Tip,
       p.max_length, p.precision, p.scale, p.has_default_value
FROM sys.parameters p
WHERE p.object_id IN (OBJECT_ID('dbo.InsertCarinskoStavke'), OBJECT_ID('dbo.UpdateCarinskoStavke'))
ORDER BY 1, p.parameter_id;

/* ---------------------------------------------------------------------------------------------
   2. Magacinski predmet naloga i njegova naimenovanja
      Program upisuje naimenovanje pod MagacinskiPredmet.ID, a tabela na formi prikazuje
      naimenovanja svih predmeta čiji je RN = nalog.
   --------------------------------------------------------------------------------------------- */
SELECT mp.* FROM dbo.MagacinskiPredmet mp WHERE mp.RN = @RN ORDER BY mp.ID;

SELECT s.* FROM dbo.RNCarinskoStavke s
JOIN dbo.MagacinskiPredmet mp ON mp.ID = s.IDNadredjena
WHERE mp.RN = @RN
ORDER BY s.ID;

-- poslednja naimenovanja u celoj tabeli: da li je red otišao pod drugi predmet (ili bez predmeta)
SELECT TOP 10 s.ID, s.IDNadredjena, mp.RN AS [Nalog predmeta], s.NHM, s.Naimenovanje, s.RB, s.JM,
       s.Koleta, s.Bruto, s.Neto, s.Vrednost, s.Valuta
FROM dbo.RNCarinskoStavke s
LEFT JOIN dbo.MagacinskiPredmet mp ON mp.ID = s.IDNadredjena
ORDER BY s.ID DESC;

/* ---------------------------------------------------------------------------------------------
   3. Tekst procedura i okidači na RNCarinskoStavke (nisu u repozitorijumu)
   --------------------------------------------------------------------------------------------- */
SELECT o.name AS Objekat, o.type_desc, o.modify_date AS [Poslednja izmena], OBJECT_DEFINITION(o.object_id) AS Tekst
FROM sys.objects o
WHERE o.name IN ('InsertCarinskoStavke', 'UpdateCarinskoStavke');

SELECT t.name AS Okidac, t.is_disabled, OBJECT_DEFINITION(t.object_id) AS Tekst
FROM sys.triggers t
WHERE t.parent_id = OBJECT_ID('dbo.RNCarinskoStavke');

/* ---------------------------------------------------------------------------------------------
   4. Ograničenja na RNCarinskoStavke (CHECK, strani ključevi)
   --------------------------------------------------------------------------------------------- */
SELECT N'CHECK' AS Vrsta, cc.name AS Naziv, cc.definition AS Uslov, cc.is_disabled
FROM sys.check_constraints cc
WHERE cc.parent_object_id = OBJECT_ID('dbo.RNCarinskoStavke')
UNION ALL
SELECT N'strani ključ', fk.name, OBJECT_NAME(fk.referenced_object_id), fk.is_disabled
FROM sys.foreign_keys fk
WHERE fk.parent_object_id = OBJECT_ID('dbo.RNCarinskoStavke');

/* ---------------------------------------------------------------------------------------------
   5. PROBA: upis naimenovanja iz testa, pa ROLLBACK
      Isti parametri kao dugme Dodaj: NHM 73211900, Rostilji, RB 1, KOM, 981 / 981, bruto 7526,
      neto 6699, 26398,50 USD, carina 0, PDV 0. Predmet: poslednji magacinski predmet naloga.
   --------------------------------------------------------------------------------------------- */
IF @Proba = 1
BEGIN
    DECLARE @MP int, @Pre int, @Posle int, @MaxID int, @Greska nvarchar(4000);
    SELECT TOP 1 @MP = ID FROM dbo.MagacinskiPredmet WHERE RN = @RN ORDER BY ID DESC;

    IF @MP IS NULL
        SELECT N'Proba upisa' AS Provera,
               N'Nalog ' + CAST(@RN AS nvarchar(10)) + N' nema magacinski predmet (MagacinskiPredmet.RN); Dodaj tada javlja da prvo treba snimiti magacinski predmet.' AS Rezultat;
    ELSE
    BEGIN
        SELECT @Pre = COUNT(*) FROM dbo.RNCarinskoStavke WHERE IDNadredjena = @MP;
        SELECT @MaxID = ISNULL(MAX(ID), 0) FROM dbo.RNCarinskoStavke;

        BEGIN TRAN;
        BEGIN TRY
            EXEC dbo.InsertCarinskoStavke
                 @IDNadredjena = @MP, @NHM = '73211900', @Naimenovanje = N'Rostilji', @RB = 1, @JM = N'KOM',
                 @Kolicina = 981, @Koleta = 981, @Bruto = 7526, @Neto = 6699, @Paleta = 0, @VrstaPaleta = NULL,
                 @Vrednost = 26398.50, @Valuta = N'USD', @PDV = 0, @Carina = 0, @Napomena = N'';

            SELECT @Posle = COUNT(*) FROM dbo.RNCarinskoStavke WHERE IDNadredjena = @MP;

            SELECT N'Proba upisa (poništena)' AS Provera, @MP AS [Magacinski predmet], @Pre AS [Naimenovanja pre], @Posle AS [Naimenovanja posle],
                   CASE WHEN @Posle > @Pre THEN N'procedura UPISUJE red pod ovaj predmet'
                        WHEN EXISTS (SELECT 1 FROM dbo.RNCarinskoStavke WHERE ID > @MaxID) THEN N'procedura upisuje red, ali pod DRUGI predmet (vidi sledeću tabelu): to je uzrok'
                        ELSE N'procedura NE UPISUJE red i ne javlja grešku: to je uzrok (tekst procedure je u delu 3)' END AS Rezultat;

            -- redovi koje je procedura upisala u ovoj probi
            SELECT s.*, mp.RN AS [Nalog predmeta]
            FROM dbo.RNCarinskoStavke s
            LEFT JOIN dbo.MagacinskiPredmet mp ON mp.ID = s.IDNadredjena
            WHERE s.ID > @MaxID;
        END TRY
        BEGIN CATCH
            SET @Greska = ERROR_MESSAGE();
            SELECT N'Proba upisa (poništena)' AS Provera, @MP AS [Magacinski predmet],
                   N'GREŠKA ' + CAST(ERROR_NUMBER() AS nvarchar(10)) + N': ' + @Greska AS Rezultat;
        END CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK;
        PRINT 'Proba je poništena (ROLLBACK); u bazi nije ostao nijedan red.';
    END
END
