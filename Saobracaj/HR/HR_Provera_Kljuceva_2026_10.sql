/* =============================================================================================
   Provera baze pre instalacije HR modula (HR-ISPRAVKE-2.md tačka 2c). Samo čita, ništa ne menja.

   HR skripte prave tabelu samo ako ne postoji (IF OBJECT_ID(...) IS NULL CREATE TABLE). Ako tabela sa istim
   imenom već postoji iz starijih modula, njen ključ može biti bez IDENTITY ili bez primarnog ključa, pa HR
   ekrani ne mogu da dodaju zapis, a spoljni ključevi ne nastaju (tako je bilo sa Zaposleni i OrganizacioneJedinice).

   Upit vraća samo tabele iz obe HR skripte koje postoje, a ključ im nije onakav kakav HR očekuje.
   Prazan rezultat = baza je spremna. Pušta se na svakoj firmi pre HR_Modul_2026_10.sql.

   Rešenje za pronađenu tabelu:
     Zaposleni               -> HR_Modul_ZaposleniID_2026_10.sql
     OrganizacioneJedinice   -> HR_Modul_OrgJedID_2026_10.sql
     ostale                  -> javiti razvoju (skripta po istom obrascu)
   ============================================================================================= */

SET NOCOUNT ON;

-- Tabela, kolona ključa (NULL = složeni ključ, proverava se samo da PK postoji), da li ključ treba da bude IDENTITY
DECLARE @hr TABLE (Tabela sysname, Kljuc sysname NULL, Identitet bit);
INSERT @hr VALUES
 -- HR_Modul_2026_10.sql
 (N'ZaposleniStatusi', N'Status', 0),
 (N'Opstine', N'Sifra', 1),
 (N'RadnaMesta', N'Sifra', 1),
 (N'OrganizacioneJedinice', N'ID', 1),
 (N'RadniOdnosi', N'Sifra', 1),
 (N'StepenObrazovanja', N'Sifra', 1),
 (N'GrupeDokumenata', N'Sifra', 1),
 (N'OceneSposobnosti', N'Sifra', 1),
 (N'HTZGrupe', N'Sifra', 1),
 (N'RazloziRazduzenja', N'Sifra', 1),
 (N'Srodstva', N'Sifra', 1),
 (N'TezinePovreda', N'Sifra', 1),
 (N'VrsteUgovora', N'Sifra', 1),
 (N'VrsteDisciplinskihMera', N'Sifra', 1),
 (N'VrstePregleda', N'Sifra', 1),
 (N'VrsteDokumenta', N'Sifra', 1),
 (N'HTZOprema', N'Sifra', 1),
 (N'Zaposleni', N'ID', 1),
 (N'ZaposleniSlike', N'Zaposleni', 0),
 (N'ZaposleniRadnaMesta', N'Sifra', 1),
 (N'ZaposleniUgovori', N'Sifra', 1),
 (N'ZaposleniMentori', N'Sifra', 1),
 (N'ZaposleniMentorBeleske', N'Sifra', 1),
 (N'ZaposleniSkolovanje', N'Sifra', 1),
 (N'ZaposleniDokumenti', N'Sifra', 1),
 (N'ZaposleniZdravstveniPregledi', N'Sifra', 1),
 (N'HTZReversi', N'Sifra', 1),
 (N'ZaposleniHTZZaduzenja', N'Sifra', 1),
 (N'ZaposleniHitniKontakti', N'Sifra', 1),
 (N'ZaposleniPovrede', N'Sifra', 1),
 (N'ZaposleniDisciplinske', N'Sifra', 1),
 (N'ZaposleniOcene', N'Sifra', 1),
 (N'ZaposleniClanoviPorodice', N'Sifra', 1),
 (N'ZaposleniPrilozi', N'Sifra', 1),
 (N'ZaposleniPristupLog', N'Sifra', 1),
 (N'RadnoMestoObavezniDokumenti', N'Sifra', 1),
 (N'RadnoMestoObavezniPregledi', N'Sifra', 1),
 (N'RadnoMestoNormativHTZ', N'Sifra', 1),
 (N'VrsteNabavke', N'Sifra', 1),
 (N'PrioritetiNabavke', N'Sifra', 1),
 (N'StatusiZahtevaZaNabavku', N'Sifra', 0),
 (N'StatusiStavkiNabavke', N'Sifra', 0),
 (N'MestaTroska', N'Sifra', 1),
 (N'ZahteviZaNabavku', N'Sifra', 1),
 (N'ZahtevZaNabavkuStavke', N'Sifra', 1),
 (N'ZahtevZaNabavkuOdobrenja', N'Sifra', 1),
 (N'PravilaOdobravanjaNabavke', N'Sifra', 1),
 (N'ZahtevZaNabavkuPonude', N'Sifra', 1),
 (N'ZahtevZaNabavkuIstorijaStatusa', N'Sifra', 1),
 (N'ZahtevZaNabavkuPrilozi', N'Sifra', 1),
 -- HR_Modul_Faza2_2026_10.sql
 (N'Podesavanja', N'ID', 1),
 (N'HRLog', N'LogID', 1),
 (N'VrstaRada', N'ID', 1),
 (N'Praznici', N'Datum', 0),
 (N'RasporedRada', N'ID', 1),
 (N'EvidencijaRada', N'ID', 1),
 (N'EvidencijaMesec', NULL, 0),
 (N'BeleskaORadu', N'ID', 1),
 (N'OsnovGO', N'ID', 1),
 (N'ZaposleniOsnovGO', N'ID', 1),
 (N'GODopust', N'ID', 1),
 (N'GODopustOsnov', N'ID', 1),
 (N'StatusZahteva', N'ID', 0),
 (N'GODopustStavke', N'ID', 1),
 (N'GOBrojacResenja', N'Godina', 0);

SELECT x.Tabela, x.Kljuc,
       CASE WHEN x.Kljuc IS NOT NULL AND x.kolona_postoji = 0 THEN N'nema kolonu ' + x.Kljuc
            ELSE STUFF(
                   CASE WHEN x.ima_pk = 0 THEN N', nema primarni ključ'
                        WHEN x.Kljuc IS NOT NULL AND x.pk_na_kljucu = 0 THEN N', primarni ključ nije na ' + x.Kljuc ELSE N'' END
                 + CASE WHEN x.Identitet = 1 AND x.je_identitet = 0 THEN N', ' + x.Kljuc + N' nije IDENTITY' ELSE N'' END
                 + CASE WHEN x.Kljuc IS NOT NULL AND x.nullable = 1 THEN N', ' + x.Kljuc + N' dozvoljava NULL' ELSE N'' END, 1, 2, N'')
       END AS Problem
FROM (
    SELECT h.Tabela, h.Kljuc, h.Identitet,
           CASE WHEN c.column_id IS NULL THEN 0 ELSE 1 END AS kolona_postoji,
           ISNULL(CAST(c.is_identity AS int), 0) AS je_identitet,
           ISNULL(CAST(c.is_nullable AS int), 0) AS nullable,
           CASE WHEN EXISTS (SELECT 1 FROM sys.key_constraints k WHERE k.parent_object_id = OBJECT_ID(N'dbo.' + h.Tabela) AND k.type = 'PK') THEN 1 ELSE 0 END AS ima_pk,
           CASE WHEN EXISTS (SELECT 1 FROM sys.key_constraints k
                             JOIN sys.index_columns ic ON ic.object_id = k.parent_object_id AND ic.index_id = k.unique_index_id
                             WHERE k.parent_object_id = OBJECT_ID(N'dbo.' + h.Tabela) AND k.type = 'PK' AND ic.column_id = c.column_id) THEN 1 ELSE 0 END AS pk_na_kljucu
    FROM @hr h
    LEFT JOIN sys.columns c ON c.object_id = OBJECT_ID(N'dbo.' + h.Tabela) AND c.name = h.Kljuc
    WHERE OBJECT_ID(N'dbo.' + h.Tabela, 'U') IS NOT NULL
) x
WHERE (x.Kljuc IS NOT NULL AND x.kolona_postoji = 0)
   OR x.ima_pk = 0
   OR (x.Kljuc IS NOT NULL AND x.pk_na_kljucu = 0)
   OR (x.Identitet = 1 AND x.je_identitet = 0)
   OR (x.Kljuc IS NOT NULL AND x.nullable = 1)
ORDER BY x.Tabela;
