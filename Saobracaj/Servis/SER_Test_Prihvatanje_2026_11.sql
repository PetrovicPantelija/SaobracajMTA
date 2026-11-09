/* =============================================================================================
   Servis – Magacin rezervnih delova: test prihvatanja (specifikacija, poglavlje 8)
   Pravi probne podatke, proverava pravila i na kraju sve vraća (ROLLBACK): u bazi ništa ne ostaje.
   Rezultat je tabela provera; kolona OK = 1 znači da je provera prošla.

   Cene probnih delova su izabrane tako da daju brojeve iz specifikacije:
     Kočioni uložak 4 × 4.800 = 19.200 (trošak naloga pre izdavanja filtera sušača)
     Filter sušača 8.900, Set zaptivki 14.400, Twistlock 21.000 → predlog 250.300 RSD
     40-0005 520 RSD/l, 31-0012 1.450, 70-0010 1.550 → manjak 2.590, višak 1.450
   ============================================================================================= */
SET NOCOUNT ON;
SET XACT_ABORT ON;

IF OBJECT_ID('tempdb..#R') IS NOT NULL DROP TABLE #R;
CREATE TABLE #R (Rb int IDENTITY, Pravilo varchar(10), Provera nvarchar(120), Ocekivano nvarchar(100), Dobijeno nvarchar(100), OK bit);
GO

-- pomoćne procedure za probne podatke (privremene; nestaju kad se konekcija zatvori)
IF OBJECT_ID('tempdb..#TDeo') IS NOT NULL DROP PROCEDURE #TDeo;
GO
CREATE PROCEDURE #TDeo @Sifra nvarchar(20), @Stanje decimal(18,3), @Cena decimal(18,4), @Kritican bit, @ID int OUTPUT
AS
BEGIN
    DECLARE @Dob int = (SELECT TOP (1) PaSifra FROM dbo.Partnerji ORDER BY PaSifra), @Dok int;
    SET @ID = 0;
    EXEC dbo.SER_DeoSnimi @ID OUTPUT, @Sifra, @Sifra, NULL, N'kom', 0, NULL, NULL, @Kritican, @Dob, NULL, NULL, 1, N'TEST';
    IF @Stanje > 0
        EXEC dbo.SER_PocetnoStanje @ID, @Stanje, @Cena, '2026-11-09', N'TEST', @Dok OUTPUT;
END
GO
IF OBJECT_ID('tempdb..#TNar') IS NOT NULL DROP PROCEDURE #TNar;
GO
-- narudžbenica sa jednom stavkom; @Poslata = 1 šalje je na odobrenje (korisnik TEST), @Odobrena = 1 je odobri (korisnik TEST2)
CREATE PROCEDURE #TNar @DeoID int, @Kolicina decimal(18,3), @Cena decimal(18,4), @Poslata bit, @Odobrena bit, @Nar int OUTPUT, @Stavka int OUTPUT
AS
BEGIN
    DECLARE @Dob int = (SELECT TOP (1) PaSifra FROM dbo.Partnerji ORDER BY PaSifra), @Broj nvarchar(20), @Nivo tinyint;
    EXEC dbo.SER_NarudzbenicaNova @Dob, '2026-11-09', NULL, N'TEST', N'TEST', @Nar OUTPUT, @Broj OUTPUT;
    EXEC dbo.SER_NarudzbenicaStavkaSnimi @Nar, @DeoID, @Kolicina, @Cena, NULL, N'TEST', @Stavka OUTPUT;
    IF @Poslata = 1
        EXEC dbo.SER_NarudzbenicaPosalji @Nar, N'TEST', @Nivo OUTPUT;
    IF @Odobrena = 1
        EXEC dbo.SER_NarudzbenicaOdobri @Nar, 1, 3, NULL, N'TEST2';
END
GO

/* =============================================================================================
   A. Glavni tok (jedna transakcija, na kraju se sve vraća)
   ============================================================================================= */
DECLARE @R TABLE (Rb int IDENTITY, Pravilo varchar(10), Provera nvarchar(120), Ocekivano nvarchar(100), Dobijeno nvarchar(100), OK bit);   -- tabelarna promenljiva: ne vraća se sa ROLLBACK
DECLARE @K nvarchar(20) = N'TEST', @K2 nvarchar(20) = N'TEST2', @D date = '2026-11-09', @Dob int = (SELECT TOP (1) PaSifra FROM dbo.Partnerji ORDER BY PaSifra);
DECLARE @V int, @S int, @Dok int, @Broj nvarchar(20), @Rez int, @Nivo tinyint, @Kr int, @x decimal(18,3), @t nvarchar(60);
DECLARE @KU int = 0, @FS int = 0, @SZ int = 0, @TL int = 0, @FG int = 0, @HC int = 0, @UL int = 0, @FV int = 0, @SI int = 0;
DECLARE @RN97 int, @RN98 int, @Nar int, @St1 int, @St2 int, @Pop int, @BrojRN nvarchar(20);

BEGIN TRY
    BEGIN TRAN;

    /* ---------- probni podaci ---------- */
    INSERT dbo.SERVrstaSredstva (Naziv, KreiraoKorisnik) VALUES (N'TEST Reach stacker', @K);
    SET @V = SCOPE_IDENTITY();
    INSERT dbo.SERSredstvo (Oznaka, Naziv, VrstaSredstvaID, KreiraoKorisnik) VALUES (N'TEST-RS1', N'Reach stacker 1', @V, @K);
    SET @S = SCOPE_IDENTITY();

    EXEC dbo.SER_DeoSnimi @KU OUTPUT, N'TST-KU', N'Kočioni uložak', NULL, N'kom', 0, NULL, NULL, 0, NULL, NULL, NULL, 1, @K;
    EXEC dbo.SER_DeoSnimi @FS OUTPUT, N'TST-FS', N'Filter sušača', NULL, N'kom', 2, 6, N'A-01', 1, @Dob, 7, NULL, 1, @K;
    EXEC dbo.SER_DeoSnimi @SZ OUTPUT, N'TST-SZ', N'Set zaptivki', NULL, N'kom', 3, 8, NULL, 0, @Dob, 10, NULL, 1, @K;
    EXEC dbo.SER_DeoSnimi @TL OUTPUT, N'TST-TL', N'Twistlock', NULL, N'kom', 4, 8, NULL, 0, @Dob, 14, NULL, 1, @K;
    EXEC dbo.SER_DeoSnimi @FG OUTPUT, N'TST-FG', N'Filter goriva', NULL, N'kom', 6, 12, NULL, 0, @Dob, 5, NULL, 1, @K;
    EXEC dbo.SER_DeoSnimi @HC OUTPUT, N'TST-HC', N'Hidraulično crevo', NULL, N'kom', 2, 6, NULL, 0, @Dob, 5, NULL, 1, @K;
    EXEC dbo.SER_DeoSnimi @UL OUTPUT, N'40-0005', N'Hidraulično ulje', NULL, N'l', 0, NULL, NULL, 0, NULL, NULL, NULL, 1, @K;
    EXEC dbo.SER_DeoSnimi @FV OUTPUT, N'31-0012', N'Filter vazduha', NULL, N'kom', 0, NULL, NULL, 0, NULL, NULL, NULL, 1, @K;
    EXEC dbo.SER_DeoSnimi @SI OUTPUT, N'70-0010', N'Sijalica', NULL, N'kom', 0, NULL, NULL, 0, NULL, NULL, NULL, 1, @K;

    SET @Dok = NULL;
    EXEC dbo.SER_PocetnoStanje @KU, 10, 4800, @D, @K, @Dok OUTPUT;
    EXEC dbo.SER_PocetnoStanje @FS, 2, 8900, @D, @K, @Dok OUTPUT;
    EXEC dbo.SER_PocetnoStanje @SZ, 1, 14400, @D, @K, @Dok OUTPUT;
    EXEC dbo.SER_PocetnoStanje @TL, 3, 21000, @D, @K, @Dok OUTPUT;
    EXEC dbo.SER_PocetnoStanje @FG, 5, 2000, @D, @K, @Dok OUTPUT;
    EXEC dbo.SER_PocetnoStanje @UL, 20, 520, @D, @K, @Dok OUTPUT;
    EXEC dbo.SER_PocetnoStanje @FV, 4, 1450, @D, @K, @Dok OUTPUT;
    EXEC dbo.SER_PocetnoStanje @SI, 5, 1550, @D, @K, @Dok OUTPUT;

    -- naručeno: filter goriva 10, hidraulično crevo 4 (poručena narudžbenica)
    EXEC dbo.SER_NarudzbenicaNova @Dob, @D, NULL, N'TEST', @K, @Nar OUTPUT, @Broj OUTPUT;
    EXEC dbo.SER_NarudzbenicaStavkaSnimi @Nar, @FG, 10, 2000, NULL, @K, @St1 OUTPUT;
    EXEC dbo.SER_NarudzbenicaStavkaSnimi @Nar, @HC, 4, 3000, NULL, @K, @St2 OUTPUT;
    EXEC dbo.SER_NarudzbenicaPosalji @Nar, @K, @Nivo OUTPUT;
    INSERT @R SELECT 'MD-R07', N'Narudžbenica 32.000 RSD: nivo odobrenja', N'1', CAST(@Nivo AS nvarchar(10)), CASE WHEN @Nivo = 1 THEN 1 ELSE 0 END;
    EXEC dbo.SER_NarudzbenicaOdobri @Nar, 1, 1, NULL, @K2;      -- odobrava drugi korisnik (zabrana samoodobravanja)
    INSERT @R SELECT 'MD-R07', N'Odobrena narudžbenica je Poručeno', N'3', CAST(Status AS nvarchar(10)), CASE WHEN Status = 3 THEN 1 ELSE 0 END
    FROM dbo.SERNarudzbenica WHERE ID = @Nar;

    -- radni nalozi dobijaju brojeve RN-2026-0097 i RN-2026-0098
    DELETE dbo.SERBrojac WHERE Kljuc = N'Servis.FormatRadnogNaloga' AND Godina = 2026;
    INSERT dbo.SERBrojac (Kljuc, Godina, Poslednji) VALUES (N'Servis.FormatRadnogNaloga', 2026, 96);

    /* ---------- Nema dela: RN-2026-0097, hidraulično crevo (stanje 0, naručeno 4) ---------- */
    EXEC dbo.SER_RadniNalogNovi @D, @S, NULL, N'TEST crevo', @K, @RN97 OUTPUT, @BrojRN OUTPUT;
    INSERT @R SELECT 'Broj', N'Broj prvog naloga', N'RN-2026-0097', @BrojRN, CASE WHEN @BrojRN = N'RN-2026-0097' THEN 1 ELSE 0 END;
    EXEC dbo.SER_RezervacijaDodaj @RN97, NULL, @HC, 2, NULL, @K, @Rez OUTPUT;
    INSERT @R SELECT 'MD-R01', N'Crevo bez stanja: status stavke', N'Nema na stanju', st.Naziv, CASE WHEN rz.Status = 3 THEN 1 ELSE 0 END
    FROM dbo.SERDeoRezervacija rz JOIN dbo.SERStatus st ON st.Oblast = 'Rezervacija' AND st.Status = rz.Status WHERE rz.ID = @Rez;
    INSERT @R SELECT 'MD-R03', N'RN-2026-0097 status', N'Čeka delove', StatusTekst, CASE WHEN Status = 3 THEN 1 ELSE 0 END
    FROM dbo.vSERRadniNalogTrosak WHERE RadniNalogID = @RN97;

    /* ---------- Izdavanje: RN-2026-0098 ---------- */
    EXEC dbo.SER_RadniNalogNovi @D, @S, NULL, N'TEST servis', @K, @RN98 OUTPUT, @BrojRN OUTPUT;
    INSERT @R SELECT 'Broj', N'Broj drugog naloga', N'RN-2026-0098', @BrojRN, CASE WHEN @BrojRN = N'RN-2026-0098' THEN 1 ELSE 0 END;
    EXEC dbo.SER_RezervacijaDodaj @RN98, NULL, @KU, 4, NULL, @K, @Rez OUTPUT;
    EXEC dbo.SER_Izdaj @RN98, NULL, @D, NULL, @K, @Dok OUTPUT, @Broj OUTPUT;
    INSERT @R SELECT 'MD-R02', N'Trošak delova pre izdavanja filtera', N'19200.00', CAST(TrosakDelova AS nvarchar(30)), CASE WHEN TrosakDelova = 19200 THEN 1 ELSE 0 END
    FROM dbo.vSERRadniNalogTrosak WHERE RadniNalogID = @RN98;

    EXEC dbo.SER_RezervacijaDodaj @RN98, NULL, @FS, 1, NULL, @K, @Rez OUTPUT;
    -- Raspoloživo: stanje 2, rezervisano 1 → raspoloživo 1 (ispod minimuma 2)
    INSERT @R SELECT 'Rasp.', N'Filter sušača: stanje / rezervisano / raspoloživo', N'2 / 1 / 1',
           CONCAT(CAST(Stanje AS int), N' / ', CAST(Rezervisano AS int), N' / ', CAST(Raspolozivo AS int)),
           CASE WHEN Stanje = 2 AND Rezervisano = 1 AND Raspolozivo = 1 THEN 1 ELSE 0 END
    FROM dbo.vSERDeoStanje WHERE ID = @FS;
    INSERT @R SELECT 'Rasp.', N'Filter sušača ispod minimuma 2', N'Ispod minimuma', StatusTekst, CASE WHEN IspodMinimuma = 1 THEN 1 ELSE 0 END
    FROM dbo.vSERDeoStanje WHERE ID = @FS;
    INSERT @R SELECT 'MD-R08', N'Kritičan filter sušača: nepročitano obaveštenje', N'1', CAST(COUNT(*) AS nvarchar(10)), CASE WHEN COUNT(*) = 1 THEN 1 ELSE 0 END
    FROM dbo.SERObavestenje WHERE DeoID = @FS AND Procitano = 0;

    EXEC dbo.SER_Izdaj @RN98, NULL, @D, NULL, @K, @Dok OUTPUT, @Broj OUTPUT;
    INSERT @R SELECT 'MD-R02', N'Izdaj rezervisane delove: filter sušača stanje', N'1', CAST(CAST(Stanje AS int) AS nvarchar(10)), CASE WHEN Stanje = 1 THEN 1 ELSE 0 END
    FROM dbo.vSERDeoStanje WHERE ID = @FS;
    INSERT @R SELECT 'MD-R02', N'Trošak delova posle izdavanja', N'28100.00', CAST(TrosakDelova AS nvarchar(30)), CASE WHEN TrosakDelova = 28100 THEN 1 ELSE 0 END
    FROM dbo.vSERRadniNalogTrosak WHERE RadniNalogID = @RN98;
    INSERT @R SELECT 'MD-R02', N'Stavka filtera posle izdavanja', N'Izdato', StatusTekst, CASE WHEN Status = 2 THEN 1 ELSE 0 END
    FROM dbo.vSERRadniNalogDelovi WHERE RezervacijaID = @Rez;

    /* ---------- Predlog nabavke ---------- */
    INSERT @R SELECT 'MD-R06', N'Predlog: ' + Naziv, N'Predlog ' + CASE DeoID WHEN @SZ THEN N'7' ELSE N'5' END,
           N'Predlog ' + CAST(CAST(PredlozenaKolicina AS int) AS nvarchar(10)),
           CASE WHEN PredlozenaKolicina = CASE DeoID WHEN @SZ THEN 7 ELSE 5 END THEN 1 ELSE 0 END
    FROM dbo.vSERPredlogNabavke WHERE DeoID IN (@SZ, @FS, @TL);
    INSERT @R SELECT 'MD-R06', N'Broj probnih delova u predlogu', N'3', CAST(COUNT(*) AS nvarchar(10)), CASE WHEN COUNT(*) = 3 THEN 1 ELSE 0 END
    FROM dbo.vSERPredlogNabavke WHERE DeoID IN (@KU, @FS, @SZ, @TL, @FG, @HC, @UL, @FV, @SI);
    SELECT @x = SUM(Iznos) FROM dbo.vSERPredlogNabavke WHERE DeoID IN (@KU, @FS, @SZ, @TL, @FG, @HC, @UL, @FV, @SI);
    INSERT @R SELECT 'MD-R06', N'Predlog ukupno RSD', N'250300.00', CAST(CAST(@x AS decimal(18,2)) AS nvarchar(30)), CASE WHEN @x = 250300 THEN 1 ELSE 0 END;
    EXEC dbo.SER_NivoOdobrenja @x, @Nivo OUTPUT;
    INSERT @R SELECT 'MD-R07', N'Predlog: nivo odobrenja', N'Tehnički direktor', Naziv, CASE WHEN @Nivo = 2 THEN 1 ELSE 0 END
    FROM dbo.SERStatus WHERE Oblast = 'NivoOdobrenja' AND Status = @Nivo;
    INSERT @R SELECT 'Naruč.', N'Filter goriva (5 < 6, naručeno 10) nije u predlogu', N'nije', CASE WHEN EXISTS (SELECT 1 FROM dbo.vSERPredlogNabavke WHERE DeoID = @FG) THEN N'jeste' ELSE N'nije' END,
           CASE WHEN EXISTS (SELECT 1 FROM dbo.vSERPredlogNabavke WHERE DeoID = @FG) THEN 0 ELSE 1 END;
    INSERT @R SELECT 'Naruč.', N'Filter goriva: raspoloživo / naručeno', N'5 / 10', CONCAT(CAST(Raspolozivo AS int), N' / ', CAST(Naruceno AS int)),
           CASE WHEN Raspolozivo = 5 AND Naruceno = 10 THEN 1 ELSE 0 END
    FROM dbo.vSERDeoStanje WHERE ID = @FG;
    INSERT @R SELECT 'Nema', N'RN-2026-0097 i dalje čeka delove', N'Čeka delove', StatusTekst, CASE WHEN Status = 3 THEN 1 ELSE 0 END
    FROM dbo.vSERRadniNalogTrosak WHERE RadniNalogID = @RN97;

    /* ---------- Povrat (MD-R04) ---------- */
    SELECT @Kr = ID FROM dbo.SERDeoKretanje WHERE RadniNalogID = @RN98 AND DeoID = @KU AND Tip = 'IZD';
    EXEC dbo.SER_Povrat @Kr, 1, @D, N'TEST', @K, @Dok OUTPUT, @Broj OUTPUT;
    INSERT @R SELECT 'MD-R04', N'Povrat 1 uloška: trošak naloga', N'23300.00', CAST(TrosakDelova AS nvarchar(30)), CASE WHEN TrosakDelova = 23300 THEN 1 ELSE 0 END
    FROM dbo.vSERRadniNalogTrosak WHERE RadniNalogID = @RN98;
    INSERT @R SELECT 'MD-R04', N'Povrat 1 uloška: stanje uloška', N'7', CAST(CAST(Stanje AS int) AS nvarchar(10)), CASE WHEN Stanje = 7 THEN 1 ELSE 0 END
    FROM dbo.vSERDeoStanje WHERE ID = @KU;

    /* ---------- Prijem (MD-R05, MD-R03) ---------- */
    EXEC dbo.SER_PrijemNovi @Nar, @D, NULL, NULL, @K, @Dok OUTPUT, @Broj OUTPUT;
    EXEC dbo.SER_PrijemStavka @Dok, @St2, NULL, 4, 3000, NULL, @K, @Kr OUTPUT;
    INSERT @R SELECT 'MD-R03', N'Stiglo crevo: RN-2026-0097 se vraća', N'U radu', StatusTekst, CASE WHEN Status = 2 THEN 1 ELSE 0 END
    FROM dbo.vSERRadniNalogTrosak WHERE RadniNalogID = @RN97;
    INSERT @R SELECT 'MD-R05', N'Narudžbenica posle prijema samo creva', N'Delimično isporučeno', StatusTekst, CASE WHEN Status = 4 THEN 1 ELSE 0 END
    FROM dbo.vSERNarudzbenica WHERE ID = @Nar;
    EXEC dbo.SER_PrijemNovi NULL, @D, N'TEST hitna kupovina', NULL, @K, @Dok OUTPUT, @Broj OUTPUT;
    EXEC dbo.SER_PrijemStavka @Dok, NULL, @FS, 1, 9900, NULL, @K, @Kr OUTPUT;
    INSERT @R SELECT 'MD-R05', N'Prosečna cena filtera: 1 × 8.900 + 1 × 9.900', N'9400.0000', CAST(ProsecnaCena AS nvarchar(30)), CASE WHEN ProsecnaCena = 9400 THEN 1 ELSE 0 END
    FROM dbo.SERDeo WHERE ID = @FS;

    /* ---------- Popis (MD-R10) ---------- */
    UPDATE dbo.SERPopis SET Status = 4 WHERE Status IN (1, 2);   -- tuđi otvoreni popisi ne smeju da blokiraju test (transakcija se vraća)
    EXEC dbo.SER_PopisNovi @D, NULL, N'TEST', @K, @Pop OUTPUT, @Broj OUTPUT;
    UPDATE s SET PrebrojanaKolicina = CASE s.DeoID WHEN @UL THEN 18 WHEN @FV THEN 5 WHEN @SI THEN 4 END
    FROM dbo.SERPopisStavka s WHERE s.PopisID = @Pop AND s.DeoID IN (@UL, @FV, @SI);
    INSERT @R SELECT 'MD-R10', N'Popis: manjak / višak RSD', N'2590.00 / 1450.00', CONCAT(VrednostManjka, N' / ', VrednostViska),
           CASE WHEN VrednostManjka = 2590 AND VrednostViska = 1450 THEN 1 ELSE 0 END
    FROM dbo.vSERPopis WHERE ID = @Pop;
    INSERT @R SELECT 'MD-R10', N'Pre odobrenja zaliha 40-0005 se ne menja', N'20', CAST(CAST(Stanje AS int) AS nvarchar(10)), CASE WHEN Stanje = 20 THEN 1 ELSE 0 END
    FROM dbo.vSERDeoStanje WHERE ID = @UL;
    EXEC dbo.SER_PopisPosalji @Pop, @K;
    EXEC dbo.SER_PopisOdobri @Pop, 1, NULL, @K2;
    INSERT @R SELECT 'MD-R10', N'Posle odobrenja: 40-0005 / 31-0012 / 70-0010', N'18 / 5 / 4',
           CONCAT((SELECT CAST(Stanje AS int) FROM dbo.vSERDeoStanje WHERE ID = @UL), N' / ',
                  (SELECT CAST(Stanje AS int) FROM dbo.vSERDeoStanje WHERE ID = @FV), N' / ',
                  (SELECT CAST(Stanje AS int) FROM dbo.vSERDeoStanje WHERE ID = @SI)),
           CASE WHEN (SELECT Stanje FROM dbo.vSERDeoStanje WHERE ID = @UL) = 18 AND (SELECT Stanje FROM dbo.vSERDeoStanje WHERE ID = @FV) = 5
                 AND (SELECT Stanje FROM dbo.vSERDeoStanje WHERE ID = @SI) = 4 THEN 1 ELSE 0 END;

    /* ---------- MD-R12: deo sa kretanjem se ne briše (poslednja provera: odbijanje poništava transakciju) ---------- */
    BEGIN TRY
        DELETE dbo.SERDeo WHERE ID = @KU;
        SET @t = N'obrisan';
    END TRY
    BEGIN CATCH
        SET @t = N'odbijeno';
    END CATCH
    IF @@TRANCOUNT > 0 ROLLBACK;
    INSERT @R SELECT 'MD-R12', N'Brisanje dela sa kretanjem', N'odbijeno', @t, CASE WHEN @t = N'odbijeno' THEN 1 ELSE 0 END;
    INSERT @R SELECT 'Kraj', N'Sve provere izvršene, podaci vraćeni', N'da', N'da', 1;
END TRY
BEGIN CATCH
    DECLARE @Greska nvarchar(2048) = ERROR_MESSAGE() + N' (procedura ' + ISNULL(ERROR_PROCEDURE(), N'-') + N', red ' + CAST(ERROR_LINE() AS nvarchar(10)) + N')';
    IF @@TRANCOUNT > 0 ROLLBACK;
    INSERT @R SELECT 'GREŠKA', LEFT(@Greska, 120), N'', N'', 0;
END CATCH
INSERT #R (Pravilo, Provera, Ocekivano, Dobijeno, OK) SELECT Pravilo, Provera, Ocekivano, Dobijeno, OK FROM @R ORDER BY Rb;
GO

/* =============================================================================================
   B. Negativni scenariji: greška u proceduri poništava transakciju, pa svaki scenario ima svoju
      transakciju koja se uvek vraća. Očekuje se tačan broj greške (THROW) iz procedure.
   ============================================================================================= */

-- B1 (P8) narudžbenicu ne odobrava isti korisnik koji ju je poslao
DECLARE @DeoID int, @Nar int, @St int;
BEGIN TRY
    BEGIN TRAN;
    EXEC #TDeo N'B1-D', 0, 0, 0, @DeoID OUTPUT;
    EXEC #TNar @DeoID, 5, 1000, 1, 0, @Nar OUTPUT, @St OUTPUT;
    EXEC dbo.SER_NarudzbenicaOdobri @Nar, 1, 3, NULL, N'TEST';
    ROLLBACK;
    INSERT #R VALUES ('MD-R07', N'P8 narudžbenica: isti korisnik odobrava', N'greška 50801', N'odobreno', 0);
END TRY
BEGIN CATCH
    DECLARE @n1 int = ERROR_NUMBER(); IF @@TRANCOUNT > 0 ROLLBACK;
    INSERT #R VALUES ('MD-R07', N'P8 narudžbenica: isti korisnik odobrava', N'greška 50801', N'greška ' + CAST(@n1 AS nvarchar(10)), CASE WHEN @n1 = 50801 THEN 1 ELSE 0 END);
END CATCH
GO

-- B2 (P8) popis ne odobrava isti korisnik koji ga je poslao
DECLARE @DeoID int, @Pop int, @Broj nvarchar(20);
BEGIN TRY
    BEGIN TRAN;
    EXEC #TDeo N'B2-D', 10, 1000, 0, @DeoID OUTPUT;
    UPDATE dbo.SERPopis SET Status = 4 WHERE Status IN (1, 2);   -- tuđi otvoreni popisi ne smeju da blokiraju test (transakcija se vraća)
    EXEC dbo.SER_PopisNovi '2026-11-09', NULL, N'TEST', N'TEST', @Pop OUTPUT, @Broj OUTPUT;
    UPDATE dbo.SERPopisStavka SET PrebrojanaKolicina = 9 WHERE PopisID = @Pop AND DeoID = @DeoID;
    EXEC dbo.SER_PopisPosalji @Pop, N'TEST';
    EXEC dbo.SER_PopisOdobri @Pop, 1, NULL, N'TEST';
    ROLLBACK;
    INSERT #R VALUES ('MD-R10', N'P8 popis: isti korisnik odobrava', N'greška 50801', N'odobreno', 0);
END TRY
BEGIN CATCH
    DECLARE @n2 int = ERROR_NUMBER(); IF @@TRANCOUNT > 0 ROLLBACK;
    INSERT #R VALUES ('MD-R10', N'P8 popis: isti korisnik odobrava', N'greška 50801', N'greška ' + CAST(@n2 AS nvarchar(10)), CASE WHEN @n2 = 50801 THEN 1 ELSE 0 END);
END CATCH
GO

-- B3 (P8) korekciju ne odobrava isti korisnik koji ju je tražio
DECLARE @DeoID int, @Kor int;
BEGIN TRY
    BEGIN TRAN;
    EXEC #TDeo N'B3-D', 10, 1000, 0, @DeoID OUTPUT;
    EXEC dbo.SER_KorekcijaNova @DeoID, -2, N'test', N'TEST', @Kor OUTPUT;
    EXEC dbo.SER_KorekcijaOdobri @Kor, 1, NULL, N'TEST';
    ROLLBACK;
    INSERT #R VALUES ('MD-R11', N'P8 korekcija: isti korisnik odobrava', N'greška 50801', N'odobreno', 0);
END TRY
BEGIN CATCH
    DECLARE @n3 int = ERROR_NUMBER(); IF @@TRANCOUNT > 0 ROLLBACK;
    INSERT #R VALUES ('MD-R11', N'P8 korekcija: isti korisnik odobrava', N'greška 50801', N'greška ' + CAST(@n3 AS nvarchar(10)), CASE WHEN @n3 = 50801 THEN 1 ELSE 0 END);
END CATCH
GO

-- B4 (P7) prijem preko preostale količine traži komentar
DECLARE @DeoID int, @Nar int, @St int, @Dok int, @Broj nvarchar(20), @Kr int;
BEGIN TRY
    BEGIN TRAN;
    EXEC #TDeo N'B4-D', 0, 0, 0, @DeoID OUTPUT;
    EXEC #TNar @DeoID, 5, 1000, 1, 1, @Nar OUTPUT, @St OUTPUT;
    EXEC dbo.SER_PrijemNovi @Nar, '2026-11-09', NULL, NULL, N'TEST', @Dok OUTPUT, @Broj OUTPUT;
    EXEC dbo.SER_PrijemStavka @Dok, @St, NULL, 8, 1000, NULL, N'TEST', @Kr OUTPUT;
    ROLLBACK;
    INSERT #R VALUES ('MD-R05', N'P7 prijem 8 od naručenih 5, bez komentara', N'greška 50317', N'prihvaćeno', 0);
END TRY
BEGIN CATCH
    DECLARE @n4 int = ERROR_NUMBER(); IF @@TRANCOUNT > 0 ROLLBACK;
    INSERT #R VALUES ('MD-R05', N'P7 prijem 8 od naručenih 5, bez komentara', N'greška 50317', N'greška ' + CAST(@n4 AS nvarchar(10)), CASE WHEN @n4 = 50317 THEN 1 ELSE 0 END);
END CATCH
GO
DECLARE @DeoID int, @Nar int, @St int, @Dok int, @Broj nvarchar(20), @Kr int, @Stanje decimal(18,3), @r nvarchar(100);
BEGIN TRY
    BEGIN TRAN;
    EXEC #TDeo N'B4B-D', 0, 0, 0, @DeoID OUTPUT;
    EXEC #TNar @DeoID, 5, 1000, 1, 1, @Nar OUTPUT, @St OUTPUT;
    EXEC dbo.SER_PrijemNovi @Nar, '2026-11-09', NULL, NULL, N'TEST', @Dok OUTPUT, @Broj OUTPUT;
    EXEC dbo.SER_PrijemStavka @Dok, @St, NULL, 5, 1000, NULL, N'TEST', @Kr OUTPUT;            -- tačno preostalo: bez komentara
    SELECT @Stanje = Stanje FROM dbo.vSERDeoStanje WHERE ID = @DeoID;
    SET @r = N'stanje ' + CAST(CAST(@Stanje AS int) AS nvarchar(10));
    ROLLBACK;
    INSERT #R VALUES ('MD-R05', N'P7 prijem tačno preostale količine prolazi bez komentara', N'stanje 5', @r, CASE WHEN @Stanje = 5 THEN 1 ELSE 0 END);
END TRY
BEGIN CATCH
    DECLARE @n4b nvarchar(300) = ERROR_MESSAGE(); IF @@TRANCOUNT > 0 ROLLBACK;
    INSERT #R VALUES ('MD-R05', N'P7 prijem tačno preostale količine prolazi bez komentara', N'stanje 5', LEFT(@n4b, 100), 0);
END CATCH
GO
DECLARE @DeoID int, @Nar int, @St int, @Dok int, @Broj nvarchar(20), @Kr int, @Stanje decimal(18,3), @r nvarchar(100);
BEGIN TRY
    BEGIN TRAN;
    EXEC #TDeo N'B4C-D', 0, 0, 0, @DeoID OUTPUT;
    EXEC #TNar @DeoID, 5, 1000, 1, 1, @Nar OUTPUT, @St OUTPUT;
    EXEC dbo.SER_PrijemNovi @Nar, '2026-11-09', NULL, NULL, N'TEST', @Dok OUTPUT, @Broj OUTPUT;
    EXEC dbo.SER_PrijemStavka @Dok, @St, NULL, 8, 1000, N'Isporučilac poslao više, potvrđeno telefonom', N'TEST', @Kr OUTPUT;
    SELECT @Stanje = Stanje FROM dbo.vSERDeoStanje WHERE ID = @DeoID;
    SET @r = N'stanje ' + CAST(CAST(@Stanje AS int) AS nvarchar(10));
    ROLLBACK;
    INSERT #R VALUES ('MD-R05', N'P7 prijem 8 od naručenih 5 uz komentar prolazi', N'stanje 8', @r, CASE WHEN @Stanje = 8 THEN 1 ELSE 0 END);
END TRY
BEGIN CATCH
    DECLARE @n4c nvarchar(300) = ERROR_MESSAGE(); IF @@TRANCOUNT > 0 ROLLBACK;
    INSERT #R VALUES ('MD-R05', N'P7 prijem 8 od naručenih 5 uz komentar prolazi', N'stanje 8', LEFT(@n4c, 100), 0);
END CATCH
GO

-- B5 (P9) povrat na završen nalog se odbija; posle ponovnog otvaranja (uz razlog) prolazi
DECLARE @V int, @S int, @DeoID int, @RN int, @Broj nvarchar(20), @Rez int, @Dok int, @Kr int;
BEGIN TRY
    BEGIN TRAN;
    INSERT dbo.SERVrstaSredstva (Naziv, KreiraoKorisnik) VALUES (N'B5 vrsta', N'TEST'); SET @V = SCOPE_IDENTITY();
    INSERT dbo.SERSredstvo (Oznaka, Naziv, VrstaSredstvaID, KreiraoKorisnik) VALUES (N'B5-S', N'B5', @V, N'TEST'); SET @S = SCOPE_IDENTITY();
    EXEC #TDeo N'B5-D', 10, 1000, 0, @DeoID OUTPUT;
    EXEC dbo.SER_RadniNalogNovi '2026-11-09', @S, NULL, N'B5', N'TEST', @RN OUTPUT, @Broj OUTPUT;
    EXEC dbo.SER_RezervacijaDodaj @RN, NULL, @DeoID, 4, NULL, N'TEST', @Rez OUTPUT;
    EXEC dbo.SER_Izdaj @RN, NULL, '2026-11-09', NULL, N'TEST', @Dok OUTPUT, @Broj OUTPUT;
    SELECT @Kr = ID FROM dbo.SERDeoKretanje WHERE RadniNalogID = @RN AND Tip = 'IZD';
    EXEC dbo.SER_RadniNalogZavrsi @RN, '2026-11-09', N'TEST';
    EXEC dbo.SER_Povrat @Kr, 1, '2026-11-09', NULL, N'TEST', @Dok OUTPUT, @Broj OUTPUT;
    ROLLBACK;
    INSERT #R VALUES ('MD-R04', N'P9 povrat na završen nalog', N'greška 50214', N'prihvaćeno', 0);
END TRY
BEGIN CATCH
    DECLARE @n5 int = ERROR_NUMBER(); IF @@TRANCOUNT > 0 ROLLBACK;
    INSERT #R VALUES ('MD-R04', N'P9 povrat na završen nalog', N'greška 50214', N'greška ' + CAST(@n5 AS nvarchar(10)), CASE WHEN @n5 = 50214 THEN 1 ELSE 0 END);
END CATCH
GO
DECLARE @V int, @S int, @DeoID int, @RN int, @Broj nvarchar(20), @Rez int, @Dok int, @Kr int, @Stanje decimal(18,3), @r nvarchar(100), @Greska nvarchar(100) = N'';
BEGIN TRY
    BEGIN TRAN;
    INSERT dbo.SERVrstaSredstva (Naziv, KreiraoKorisnik) VALUES (N'B5B vrsta', N'TEST'); SET @V = SCOPE_IDENTITY();
    INSERT dbo.SERSredstvo (Oznaka, Naziv, VrstaSredstvaID, KreiraoKorisnik) VALUES (N'B5B-S', N'B5B', @V, N'TEST'); SET @S = SCOPE_IDENTITY();
    EXEC #TDeo N'B5B-D', 10, 1000, 0, @DeoID OUTPUT;
    EXEC dbo.SER_RadniNalogNovi '2026-11-09', @S, NULL, N'B5B', N'TEST', @RN OUTPUT, @Broj OUTPUT;
    EXEC dbo.SER_RezervacijaDodaj @RN, NULL, @DeoID, 4, NULL, N'TEST', @Rez OUTPUT;
    EXEC dbo.SER_Izdaj @RN, NULL, '2026-11-09', NULL, N'TEST', @Dok OUTPUT, @Broj OUTPUT;
    SELECT @Kr = ID FROM dbo.SERDeoKretanje WHERE RadniNalogID = @RN AND Tip = 'IZD';
    EXEC dbo.SER_RadniNalogZavrsi @RN, '2026-11-09', N'TEST';
    EXEC dbo.SER_RadniNalogOtvori @RN, N'Ostao deo, vraća se na zalihu', N'TEST';
    EXEC dbo.SER_Povrat @Kr, 1, '2026-11-09', NULL, N'TEST', @Dok OUTPUT, @Broj OUTPUT;
    SELECT @Stanje = Stanje FROM dbo.vSERDeoStanje WHERE ID = @DeoID;
    SET @r = N'stanje ' + CAST(CAST(@Stanje AS int) AS nvarchar(10));
    ROLLBACK;
    INSERT #R VALUES ('MD-R04', N'P9 povrat posle ponovnog otvaranja naloga uz razlog', N'stanje 7', @r, CASE WHEN @Stanje = 7 THEN 1 ELSE 0 END);
END TRY
BEGIN CATCH
    DECLARE @n5b nvarchar(300) = ERROR_MESSAGE(); IF @@TRANCOUNT > 0 ROLLBACK;
    INSERT #R VALUES ('MD-R04', N'P9 povrat posle ponovnog otvaranja naloga uz razlog', N'stanje 7', LEFT(@n5b, 100), 0);
END CATCH
GO

-- B6 (P11) negativna korekcija veća od stanja odbija se već pri zahtevu
DECLARE @DeoID int, @Kor int;
BEGIN TRY
    BEGIN TRAN;
    EXEC #TDeo N'B6-D', 44, 1000, 0, @DeoID OUTPUT;
    EXEC dbo.SER_KorekcijaNova @DeoID, -100, N'test', N'TEST', @Kor OUTPUT;
    ROLLBACK;
    INSERT #R VALUES ('MD-R11', N'P11 korekcija −100 pri stanju 44', N'greška 50602', N'prihvaćeno', 0);
END TRY
BEGIN CATCH
    DECLARE @n6 int = ERROR_NUMBER(); IF @@TRANCOUNT > 0 ROLLBACK;
    INSERT #R VALUES ('MD-R11', N'P11 korekcija −100 pri stanju 44', N'greška 50602', N'greška ' + CAST(@n6 AS nvarchar(10)), CASE WHEN @n6 = 50602 THEN 1 ELSE 0 END);
END CATCH
GO

-- B7 nivoi odobrenja po iznosu (granice 100.000 i 500.000) i uloga odobravaoca
DECLARE @n tinyint, @r nvarchar(100), @ok bit = 1;
EXEC dbo.SER_NivoOdobrenja 100000, @n OUTPUT; SET @r = N'100.000 → ' + CAST(@n AS nvarchar(5)); IF @n <> 1 SET @ok = 0;
EXEC dbo.SER_NivoOdobrenja 100000.01, @n OUTPUT; SET @r += N'; 100.000,01 → ' + CAST(@n AS nvarchar(5)); IF @n <> 2 SET @ok = 0;
EXEC dbo.SER_NivoOdobrenja 500000, @n OUTPUT; SET @r += N'; 500.000 → ' + CAST(@n AS nvarchar(5)); IF @n <> 2 SET @ok = 0;
EXEC dbo.SER_NivoOdobrenja 500000.01, @n OUTPUT; SET @r += N'; 500.000,01 → ' + CAST(@n AS nvarchar(5)); IF @n <> 3 SET @ok = 0;
INSERT #R VALUES ('MD-R07', N'Nivoi po iznosu', N'100.000 → 1; 100.000,01 → 2; 500.000 → 2; 500.000,01 → 3', @r, @ok);
GO
DECLARE @DeoID int, @Nar int, @St int;
BEGIN TRY
    BEGIN TRAN;
    EXEC #TDeo N'B7-D', 0, 0, 0, @DeoID OUTPUT;
    EXEC #TNar @DeoID, 15, 10000, 1, 0, @Nar OUTPUT, @St OUTPUT;                 -- 150.000 RSD: nivo 2
    EXEC dbo.SER_NarudzbenicaOdobri @Nar, 1, 1, NULL, N'TEST2';                  -- korisnik nivoa 1
    ROLLBACK;
    INSERT #R VALUES ('MD-R07', N'Šef (nivo 1) odobrava 150.000 RSD', N'greška 50422', N'odobreno', 0);
END TRY
BEGIN CATCH
    DECLARE @n7 int = ERROR_NUMBER(); IF @@TRANCOUNT > 0 ROLLBACK;
    INSERT #R VALUES ('MD-R07', N'Šef (nivo 1) odobrava 150.000 RSD', N'greška 50422', N'greška ' + CAST(@n7 AS nvarchar(10)), CASE WHEN @n7 = 50422 THEN 1 ELSE 0 END);
END CATCH
GO
DECLARE @DeoID int, @Nar int, @St int, @s tinyint, @r nvarchar(100);
BEGIN TRY
    BEGIN TRAN;
    EXEC #TDeo N'B7B-D', 0, 0, 0, @DeoID OUTPUT;
    EXEC #TNar @DeoID, 15, 10000, 1, 0, @Nar OUTPUT, @St OUTPUT;
    EXEC dbo.SER_NarudzbenicaOdobri @Nar, 1, 2, NULL, N'TEST2';                  -- tehnički direktor (nivo 2)
    SELECT @s = Status FROM dbo.SERNarudzbenica WHERE ID = @Nar;
    SET @r = N'status ' + CAST(@s AS nvarchar(5));
    ROLLBACK;
    INSERT #R VALUES ('MD-R07', N'Tehnički direktor (nivo 2) odobrava 150.000 RSD', N'status 3', @r, CASE WHEN @s = 3 THEN 1 ELSE 0 END);
END TRY
BEGIN CATCH
    DECLARE @n7b nvarchar(300) = ERROR_MESSAGE(); IF @@TRANCOUNT > 0 ROLLBACK;
    INSERT #R VALUES ('MD-R07', N'Tehnički direktor (nivo 2) odobrava 150.000 RSD', N'status 3', LEFT(@n7b, 100), 0);
END CATCH
GO

-- B8 isti popis se ne može odobriti dva puta (nema dvostrukog knjiženja)
DECLARE @DeoID int, @Pop int, @Broj nvarchar(20), @Stanje decimal(18,3);
BEGIN TRY
    BEGIN TRAN;
    EXEC #TDeo N'B8-D', 10, 1000, 0, @DeoID OUTPUT;
    UPDATE dbo.SERPopis SET Status = 4 WHERE Status IN (1, 2);   -- tuđi otvoreni popisi ne smeju da blokiraju test (transakcija se vraća)
    EXEC dbo.SER_PopisNovi '2026-11-09', NULL, N'TEST', N'TEST', @Pop OUTPUT, @Broj OUTPUT;
    UPDATE dbo.SERPopisStavka SET PrebrojanaKolicina = 8 WHERE PopisID = @Pop AND DeoID = @DeoID;
    EXEC dbo.SER_PopisPosalji @Pop, N'TEST';
    EXEC dbo.SER_PopisOdobri @Pop, 1, NULL, N'TEST2';
    EXEC dbo.SER_PopisOdobri @Pop, 1, NULL, N'TEST2';
    ROLLBACK;
    INSERT #R VALUES ('MD-R10', N'Ponovno odobravanje istog popisa', N'greška 50531', N'odobreno ponovo', 0);
END TRY
BEGIN CATCH
    DECLARE @n8 int = ERROR_NUMBER(); IF @@TRANCOUNT > 0 ROLLBACK;
    INSERT #R VALUES ('MD-R10', N'Ponovno odobravanje istog popisa', N'greška 50531', N'greška ' + CAST(@n8 AS nvarchar(10)), CASE WHEN @n8 = 50531 THEN 1 ELSE 0 END);
END CATCH
GO

-- B9 (P11) korekcije koje čekaju odobrenje se računaju: stanje 10, zahtev −6 prolazi, drugi −6 ne
DECLARE @DeoID int, @Kor int;
BEGIN TRY
    BEGIN TRAN;
    EXEC #TDeo N'B9-D', 10, 1000, 0, @DeoID OUTPUT;
    EXEC dbo.SER_KorekcijaNova @DeoID, -6, N'prva', N'TEST', @Kor OUTPUT;
    EXEC dbo.SER_KorekcijaNova @DeoID, -6, N'druga', N'TEST', @Kor OUTPUT;
    ROLLBACK;
    INSERT #R VALUES ('MD-R11', N'P11 druga korekcija −6 uz prvu −6 na čekanju (stanje 10)', N'greška 50602', N'prihvaćeno', 0);
END TRY
BEGIN CATCH
    DECLARE @n9 int = ERROR_NUMBER(); IF @@TRANCOUNT > 0 ROLLBACK;
    INSERT #R VALUES ('MD-R11', N'P11 druga korekcija −6 uz prvu −6 na čekanju (stanje 10)', N'greška 50602', N'greška ' + CAST(@n9 AS nvarchar(10)), CASE WHEN @n9 = 50602 THEN 1 ELSE 0 END);
END CATCH
GO

-- B10 (P15) minimum veći od maksimuma
DECLARE @DeoID int = 0, @Dob int = (SELECT TOP (1) PaSifra FROM dbo.Partnerji ORDER BY PaSifra);
BEGIN TRY
    BEGIN TRAN;
    EXEC dbo.SER_DeoSnimi @DeoID OUTPUT, N'B10-D', N'B10', NULL, N'kom', 10, 5, NULL, 0, @Dob, NULL, NULL, 1, N'TEST';
    ROLLBACK;
    INSERT #R VALUES ('Deo', N'P15 min 10, max 5', N'greška 50702', N'prihvaćeno', 0);
END TRY
BEGIN CATCH
    DECLARE @n10 int = ERROR_NUMBER(); IF @@TRANCOUNT > 0 ROLLBACK;
    INSERT #R VALUES ('Deo', N'P15 min 10, max 5', N'greška 50702', N'greška ' + CAST(@n10 AS nvarchar(10)), CASE WHEN @n10 = 50702 THEN 1 ELSE 0 END);
END CATCH
GO

-- B11 (P17) servis iz plana druge vrste sredstva se odbija; odgovarajući prolazi
DECLARE @V1 int, @V2 int, @S int, @P1 int, @P2 int, @RN int, @Broj nvarchar(20);
BEGIN TRY
    BEGIN TRAN;
    INSERT dbo.SERVrstaSredstva (Naziv, KreiraoKorisnik) VALUES (N'B11 vrsta 1', N'TEST'); SET @V1 = SCOPE_IDENTITY();
    INSERT dbo.SERVrstaSredstva (Naziv, KreiraoKorisnik) VALUES (N'B11 vrsta 2', N'TEST'); SET @V2 = SCOPE_IDENTITY();
    INSERT dbo.SERSredstvo (Oznaka, Naziv, VrstaSredstvaID, KreiraoKorisnik) VALUES (N'B11-S', N'B11', @V1, N'TEST'); SET @S = SCOPE_IDENTITY();
    INSERT dbo.SERPlanStavka (VrstaSredstvaID, Naziv, IntervalDana, KreiraoKorisnik) VALUES (@V1, N'B11 plan 1', 90, N'TEST'); SET @P1 = SCOPE_IDENTITY();
    INSERT dbo.SERPlanStavka (VrstaSredstvaID, Naziv, IntervalKm, KreiraoKorisnik) VALUES (@V2, N'B11 plan 2', 15000, N'TEST'); SET @P2 = SCOPE_IDENTITY();
    EXEC dbo.SER_RadniNalogNovi '2026-11-09', @S, @P1, N'odgovara', N'TEST', @RN OUTPUT, @Broj OUTPUT;
    INSERT #R VALUES ('MD-R09', N'P17 servis iste vrste sredstva prolazi', N'nalog napravljen', CASE WHEN @RN > 0 THEN N'nalog napravljen' ELSE N'nije' END, CASE WHEN @RN > 0 THEN 1 ELSE 0 END);
    EXEC dbo.SER_RadniNalogNovi '2026-11-09', @S, @P2, N'ne odgovara', N'TEST', @RN OUTPUT, @Broj OUTPUT;
    ROLLBACK;
    INSERT #R VALUES ('MD-R09', N'P17 servis druge vrste sredstva', N'greška 50751', N'prihvaćeno', 0);
END TRY
BEGIN CATCH
    DECLARE @n11 int = ERROR_NUMBER(); IF @@TRANCOUNT > 0 ROLLBACK;
    INSERT #R VALUES ('MD-R09', N'P17 servis druge vrste sredstva', N'greška 50751', N'greška ' + CAST(@n11 AS nvarchar(10)), CASE WHEN @n11 = 50751 THEN 1 ELSE 0 END);
END CATCH
GO

-- B12 (P14) plan bez ijednog intervala se ne snima
DECLARE @V int;
BEGIN TRY
    BEGIN TRAN;
    INSERT dbo.SERVrstaSredstva (Naziv, KreiraoKorisnik) VALUES (N'B12 vrsta', N'TEST'); SET @V = SCOPE_IDENTITY();
    INSERT dbo.SERPlanStavka (VrstaSredstvaID, Naziv, IntervalDana, IntervalSati, IntervalKm, KreiraoKorisnik) VALUES (@V, N'B12 plan', 0, NULL, NULL, N'TEST');
    ROLLBACK;
    INSERT #R VALUES ('Plan', N'P14 plan bez intervala', N'greška 547 (CK_SERPlanStavka_Interval)', N'snimljeno', 0);
END TRY
BEGIN CATCH
    DECLARE @n12 int = ERROR_NUMBER(), @m12 nvarchar(300) = ERROR_MESSAGE(); IF @@TRANCOUNT > 0 ROLLBACK;
    INSERT #R VALUES ('Plan', N'P14 plan bez intervala', N'greška 547 (CK_SERPlanStavka_Interval)', N'greška ' + CAST(@n12 AS nvarchar(10)),
                      CASE WHEN @n12 = 547 AND @m12 LIKE N'%CK_SERPlanStavka_Interval%' THEN 1 ELSE 0 END);
END CATCH
GO

-- B13 (P20) deaktivacija dela: sa stanjem se odbija, bez stanja prolazi
DECLARE @DeoID int, @Dob int = (SELECT TOP (1) PaSifra FROM dbo.Partnerji ORDER BY PaSifra);
BEGIN TRY
    BEGIN TRAN;
    EXEC #TDeo N'B13-D', 15, 1000, 0, @DeoID OUTPUT;
    EXEC dbo.SER_DeoSnimi @DeoID OUTPUT, N'B13-D', N'B13-D', NULL, N'kom', 0, NULL, NULL, 0, @Dob, NULL, NULL, 0, N'TEST';
    ROLLBACK;
    INSERT #R VALUES ('MD-R12', N'P20 deaktivacija dela sa stanjem 15', N'greška 50703', N'prihvaćeno', 0);
END TRY
BEGIN CATCH
    DECLARE @n13 int = ERROR_NUMBER(); IF @@TRANCOUNT > 0 ROLLBACK;
    INSERT #R VALUES ('MD-R12', N'P20 deaktivacija dela sa stanjem 15', N'greška 50703', N'greška ' + CAST(@n13 AS nvarchar(10)), CASE WHEN @n13 = 50703 THEN 1 ELSE 0 END);
END CATCH
GO
DECLARE @DeoID int, @Dob int = (SELECT TOP (1) PaSifra FROM dbo.Partnerji ORDER BY PaSifra), @A bit;
BEGIN TRY
    BEGIN TRAN;
    EXEC #TDeo N'B13B-D', 0, 0, 0, @DeoID OUTPUT;
    EXEC dbo.SER_DeoSnimi @DeoID OUTPUT, N'B13B-D', N'B13B-D', NULL, N'kom', 0, NULL, NULL, 0, @Dob, NULL, NULL, 0, N'TEST';
    SELECT @A = Aktivno FROM dbo.SERDeo WHERE ID = @DeoID;
    ROLLBACK;
    INSERT #R VALUES ('MD-R12', N'P20 deaktivacija dela bez stanja', N'Aktivno = 0', N'Aktivno = ' + CAST(@A AS nvarchar(5)), CASE WHEN @A = 0 THEN 1 ELSE 0 END);
END TRY
BEGIN CATCH
    DECLARE @n13b nvarchar(300) = ERROR_MESSAGE(); IF @@TRANCOUNT > 0 ROLLBACK;
    INSERT #R VALUES ('MD-R12', N'P20 deaktivacija dela bez stanja', N'Aktivno = 0', LEFT(@n13b, 100), 0);
END CATCH
GO
DECLARE @V int, @S int, @DeoID int, @RN int, @Rez int, @Broj nvarchar(20), @Dob int = (SELECT TOP (1) PaSifra FROM dbo.Partnerji ORDER BY PaSifra);
BEGIN TRY
    BEGIN TRAN;
    INSERT dbo.SERVrstaSredstva (Naziv, KreiraoKorisnik) VALUES (N'B13C vrsta', N'TEST'); SET @V = SCOPE_IDENTITY();
    INSERT dbo.SERSredstvo (Oznaka, Naziv, VrstaSredstvaID, KreiraoKorisnik) VALUES (N'B13C-S', N'B13C', @V, N'TEST'); SET @S = SCOPE_IDENTITY();
    EXEC #TDeo N'B13C-D', 0, 0, 0, @DeoID OUTPUT;                                           -- bez stanja
    EXEC dbo.SER_RadniNalogNovi '2026-11-09', @S, NULL, N'B13C', N'TEST', @RN OUTPUT, @Broj OUTPUT;
    EXEC dbo.SER_RezervacijaDodaj @RN, NULL, @DeoID, 2, NULL, N'TEST', @Rez OUTPUT;         -- „Nema na stanju“ na otvorenom nalogu
    EXEC dbo.SER_DeoSnimi @DeoID OUTPUT, N'B13C-D', N'B13C-D', NULL, N'kom', 0, NULL, NULL, 0, @Dob, NULL, NULL, 0, N'TEST';
    ROLLBACK;
    INSERT #R VALUES ('MD-R12', N'P20 deaktivacija dela sa otvorenom rezervacijom', N'greška 50704', N'prihvaćeno', 0);
END TRY
BEGIN CATCH
    DECLARE @n13c int = ERROR_NUMBER(); IF @@TRANCOUNT > 0 ROLLBACK;
    INSERT #R VALUES ('MD-R12', N'P20 deaktivacija dela sa otvorenom rezervacijom', N'greška 50704', N'greška ' + CAST(@n13c AS nvarchar(10)), CASE WHEN @n13c = 50704 THEN 1 ELSE 0 END);
END CATCH
GO

SELECT Rb, Pravilo, Provera, Ocekivano, Dobijeno, OK FROM #R ORDER BY Rb;
SELECT SUM(CAST(OK AS int)) AS Proslo, COUNT(*) AS Ukupno FROM #R;
GO
