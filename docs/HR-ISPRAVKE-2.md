# HR modul – ispravke posle drugog testa (27.10.2026)

Zadatak za Claude Code. Nastavak na `docs/HR-ISPRAVKE.md` (commit `d0dc3d5`). Testirano kroz Debug build od 27.10.2026 07:52, firma Leget, baza **Testiranje**.

Ovaj fajl je **izričit zahtev** za izmene navedene ispod (pravilo iz `CLAUDE.md`). Menjaj samo ono što je ovde navedeno. Za sve ostalo prvo pitaj. Na bazi ne puštaj ništa bez potvrde korisnika, a na produkcionim bazama ništa.

Ispravke iz prvog kruga rade. Dijalozi sa neobaveznom listom se otvaraju i vrednost se učitava pri izmeni, dupli klik otvara kliknuti red, a novi zaposleni dobija ID. Ostala su dva problema sa ključevima u bazi.

---

## 1. Posle `HR_Modul_ZaposleniID_2026_10.sql` prvi novi zaposleni pada na duplikat ID-a (KRITIČNO)

**Simptom**
Prvo „Sačuvaj“ novog zaposlenog posle puštanja skripte javlja „Code, JMBG ili Šifra u ERP-u već postoji kod drugog zaposlenog.“, iako ne postoji nijedan takav zaposleni. Drugi pokušaj uspe.

**Uzrok**
- U skripti, posle `ALTER TABLE ... SWITCH`, ide `DBCC CHECKIDENT ('dbo.Zaposleni', RESEED, @max)` (oko reda 219–221).
- Nova tabela u koju je urađen SWITCH nikad nije imala INSERT. Za takvu tabelu SQL Server prvom sledećem redu daje baš `@max`, a ne `@max + 1` (dokumentovano ponašanje za `CHECKIDENT RESEED` na tabeli bez unetih redova).
- U Testiranje je `@max = 1` (NEMA NEMA). Prvi INSERT je dobio ID 1, pa je pukao PK. Tek sledeći je dobio 2, odnosno 3.
- Poruka je pogrešna. `frmHRZaposleni.Sacuvaj` svaku grešku 2601/2627 prijavljuje kao duplikat Code/JMBG/ERP, pa i grešku na `PK_Zaposleni`.

**Ispravka**
a) U `HR_Modul_ZaposleniID_2026_10.sql`, posle SWITCH-a i RESEED-a, obezbedi da sledeći ID bude `MAX(ID) + 1`. Na primer:
   - Ako je `sys.identity_columns.last_value IS NULL` za `dbo.Zaposleni`, reseed na `@max + 1`, inače na `@max`.
   - Ili posle reseed-a proveri `IDENT_CURRENT('dbo.Zaposleni') + IDENT_INCR('dbo.Zaposleni') > @max`.
   - Isto primeni u svakoj skripti koja radi SWITCH/reseed (vidi tačku 2).
b) U `frmHRZaposleni.Sacuvaj`, u `catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)`: tekst „Code, JMBG ili Šifra u ERP-u već postoji…“ prikaži samo kad poruka greške sadrži `UX_Zaposleni_Code`, `UX_Zaposleni_JMBG` ili `UX_Zaposleni_SifraERP`, i navedi koje polje. Za ostale (npr. `PK_Zaposleni`) prikaži stvarnu poruku baze.

**Za bazu Testiranje** ništa ne treba ispravljati, jer je `IDENT_CURRENT` sada 3 i ispravan. Problem se javlja na svakoj sledećoj bazi na kojoj se skripta pusti.

**Prihvatanje**
Na kopiji baze sa starom `Zaposleni` (ID bez IDENTITY) pusti skriptu, pa odmah u aplikaciji dodaj novog zaposlenog. Prvi pokušaj uspeva i dobija `MAX(ID) + 1`.

---

## 2. `OrganizacioneJedinice.ID` nije IDENTITY ni PK (KRITIČNO, isti problem kao Zaposleni)

**Simptom**
Šifarnici HR → Organizacione jedinice → „+ Dodaj“ → Naziv „TEST Odsek za kontejnere“, Nadređena jedinica „Terminal“ → Sačuvaj. Nova zaštita iz `HRTabelaKartica.SnimiOpste` ispravno prijavi:
„Zapis nije snimljen: tabela OrganizacioneJedinice ne dodeljuje šifru (ključ nije IDENTITY). Proverite SQL skripte HR modula.“
Nova organizaciona jedinica zato ne može da se doda.

**Uzrok (baza Testiranje)**
- `dbo.OrganizacioneJedinice` postoji od 2022. Kolone su `ID int NULL`, `Naziv`, `Datum`, `Korisnik`, `NadredjenaJedinica`. Nema IDENTITY ni primarni ključ.
- Zato `IF OBJECT_ID(...) IS NULL CREATE TABLE` u `HR_Modul_2026_10.sql` (red ~79) nije ništa uradio. Spoljni ključevi ka `OrganizacioneJedinice(ID)` nisu napravljeni: `FK_OrganizacioneJedinice_Nadredjena`, `FK_ZaposleniRadnaMesta_OrgJed`, `FK_ZahteviZaNabavku_OrgJed`, `FK_PravilaOdobravanja_OrgJed` (`sys.foreign_keys` za tu tabelu je prazan).
- Postojećih 7 redova ima ID 1–7, bez NULL i bez duplikata.
- Ostale tabele koje HR koristi su u redu. Proverio sam sve tabele iz `CREATE TABLE` u obe HR skripte i sve šifarnike: sve imaju IDENTITY ili PK. Izuzetak su statusne tabele sa fiksnim šiframa (`StatusZahteva`, `ZaposleniStatusi`…), koje imaju PK bez IDENTITY, što je ispravno.

**Ispravka**
a) Proširi `HR_Modul_ZaposleniID_2026_10.sql` ili napravi novu skriptu `HR_Modul_OrgJedID_2026_10.sql`, po istom obrascu kao za Zaposleni:
   - idempotentno;
   - prekid ako postoje `ID IS NULL` ili duplikati;
   - SWITCH u tabelu sa `ID int IDENTITY NOT NULL`;
   - `PK_OrganizacioneJedinice`;
   - ispravan reseed (tačka 1a);
   - ponovo napravljeni FK-ovi gore;
   - sve u transakciji.
   Proveri zavisnosti (`sys.sql_expression_dependencies`), jer tabelu koriste i stari moduli.
b) U `HR_Modul_2026_10.sql` dodaj istu zaštitu kao za Zaposleni: ako `OrganizacioneJedinice.ID` nije IDENTITY sa PK, skripta staje sa porukom koju skriptu treba prvo pustiti.
c) Po želji, napiši opšti upit za proveru baze pre instalacije: sve tabele iz HR skripti koje već postoje, a nemaju IDENTITY/PK na ključu. Korisnik može da ga pusti na svakoj firmi.

**Prihvatanje**
- Organizacione jedinice: dodavanje „TEST Odsek za kontejnere“ sa nadređenom „Terminal“ uspeva. Izmena i brisanje rade.
- `sys.foreign_keys` za `dbo.OrganizacioneJedinice` vraća 4 FK-a.

---

## 3. Manje stvari (predlog – pitaj pre izmene)

- **Ukupan iznos zahteva bez decimala.** U zahtevu za nabavku „Ukupno (RSD)“ i donja traka prikazuju `4.505` odnosno `45.500,5`, a kolona Cena `450,50`. Predlog: iznos uvek u formatu `#,##0.00` (`HRGrid` format „iznos“).
- **Strelica u ćelijama datuma.** U kolonama sa datumom (Praznici, Radna mesta, Ugovori, HTZ, GO) pored datuma se vidi strelica padajuće liste. Izgleda da se ćelija crta kao combo. Kozmetika.
- **Broj ugovora se prelama u dva reda** (`TEST-UG-01/2024`) jer je kolona uska.
- **Odobravanje se ne može testirati dok korisnik nije povezan sa zaposlenim.** Korisnik `test` nema `Korisnici.ZaposleniSifra`, a zaposleni nemaju nadređenog. Zahtev ZN-2026/0001 čeka „Testović Petar“, a GO zahtev čeka nadređenog koji nije postavljen. Ovo je podešavanje, ne greška. Proveri samo da li GO zahtev zaposlenog bez nadređenog ikad može da se odobri; ako ne može, predlog je upozorenje pri podnošenju.

---

## Šta je provereno i radi (ne dirati)

- Dijalozi sa neobaveznom listom: Org. jedinice i Vrste rada se otvaraju; „Boja“ se učitava pri izmeni; HTZ oprema, Pravila odobravanja, Hitni kontakti, Stavka zahteva i Radno mesto zaposlenog rade.
- Dupli klik otvara kliknuti red u listama Zaposleni i Zahtevi za nabavku, u šifarniku (Vrste rada) i u stavkama zahteva.
- Novi zaposleni: JMBG sa pogrešnom kontrolnom cifrom daje upozorenje; snimanje dodeljuje ID; zakonski osnov OSN 20 dana se dodeljuje automatski.
- Kartice zaposlenog: radno mesto (zaglavlje se osvežava), ugovor (radno mesto iz ugovora se nudi), hitni kontakt, HTZ revers sa stavkom (rok zamene +12 meseci).
- Godišnji odmori: „Generiši za godinu“ 2026 je generisao 2, preskočio 0. GO zahtev 02–06.11.2026 je izračunao 5 radnih dana i povratak 09.11, uz napomenu o čl. 73.
- Evidencija rada: novi zaposleni se pojavljuje, „Popuni mesec“ upisuje redovan rad.
- Zahtev za nabavku: stavka sa cenom `450,50` se snima; bez pravila „Podnesi“ daje jasnu poruku; sa pravilom prelazi u „Na odobrenju (1/1)“.

## Test podaci u bazi Testiranje (brisanje samo uz potvrdu)

- Zaposleni TEST001 Testović Petar (ID 3) sa radnim mestom, ugovorom TEST-UG-01/2024, hitnim kontaktom, reversom 2026/0001 i GO zahtevom 02–06.11.2026
- Radno mesto „TEST Vozač viljuškara“, HTZ oprema „TEST Zaštitne cipele S3“, vrsta rada „TR – TEST Terenski rad“
- Pravilo odobravanja nivo 1 (sve vrste, sve jedinice, odobravalac Testović Petar)
- Zahtev ZN-2026/0001 sa stavkom (10 × 450,50), status Na odobrenju
- GO pravo 2026 za oba zaposlena; evidencija za oktobar 2026 (Popuni mesec)
