# HR modul – ispravke posle testa (26.10.2026)

Zadatak za Claude Code. Testirano je kroz Debug build (`Saobracaj\bin\Debug`, firma Leget, baza **Testiranje**), unosom podataka u forme HR modula.

Ovaj fajl je **izričit zahtev** za izmene navedene ispod (pravilo iz `CLAUDE.md`: kod se ne menja bez zahteva). Menjaj samo ono što je ovde navedeno. Za sve ostalo prvo pitaj.

Redosled: 1 → 2 → 3 → build → ručni test (odeljak „Provera“) → 4 (čišćenje, samo uz potvrdu).

---

## 1. Pad programa u dijalogu sa neobaveznom padajućom listom (KRITIČNO)

**Simptom**
U svakom dijalogu `frmHRStavka` koji ima neobavezno polje tipa `HRTip.Lista`, „+ Dodaj“ i „Izmeni“ ruše program neobrađenim izuzetkom:

```
System.ArgumentOutOfRangeException: InvalidArgument=Value of '0' is not valid for 'SelectedIndex'.
   at System.Windows.Forms.ComboBox.set_SelectedIndex(Int32 value)
   at Saobracaj.HR.HRObrazac.Postavi(String kolona, Object v)
   at Saobracaj.HR.HRObrazac.PostaviSve(HRVrednosti v)
   at Saobracaj.HR.frmHRStavka..ctor(String naslov, IList`1 polja, HRVrednosti pocetne, Int32 brojKolona)
```

**Potvrđeno na:**
- Šifarnici HR → Organizacione jedinice (polje „Nadređena jedinica“): Dodaj i Izmeni
- Šifarnici HR → Vrste rada (polje „Boja“): Dodaj
- Zahtev za nabavku → kartica Stavke → „+ Dodaj“ (polje „HTZ oprema“). Zbog toga zahtev ne može da dobije stavke ni da se podnese.

Trebalo bi da pada i svuda gde je `HRPolje.Lista(...)` bez `obavezno = true` u dijalogu: HTZ oprema (GrupaHTZ), Opštine (Drzava), Pravila odobravanja, kartice zaposlenog (Srodstvo, Sluzbeno mesto, Organizaciona jedinica…). Spisak daje `Select-String -Path Saobracaj\HR\*.cs -Pattern 'HRPolje\.Lista\('`.

**Uzrok**
`HRObrazac.PuniListu` postavlja `cb.DataSource` dok ComboBox još nema roditelja (BindingContext), pa `cb.Items.Count == 0`. Za prazno neobavezno polje `Postavi` gleda samo `DataTable` (`dt.Rows[0][0] == DBNull.Value`) i postavlja `SelectedIndex = 0` na listu bez stavki.

Na formama gde je obrazac već na formi (Zaposleni → Osnovno, Novi zahtev za nabavku → Podaci) iste liste rade, jer tada `Items` već postoje.

**Ispravka** (`Saobracaj/HR/HRObrazac.cs`, metoda `Postavi`, grana `case ComboBox cb:` za `prazno`):
- `SelectedIndex = 0` postavljaj samo ako je `cb.Items.Count > 0` i ako je prva stavka prazna. Inače postavi `-1` i `cb.Text = ""`.
- Proveri i granu `else` (`cb.SelectedValue = ...`): ako `Items` još nisu napunjeni, vrednost se izgubi i polje u dijalogu „Izmeni“ ostane prazno. Ako je tako, obezbedi da se lista veže pre postavljanja vrednosti. Na primer, u `PuniListu` dodeli `cb.BindingContext = new BindingContext()` pre `DataSource`, ili postavljaj vrednosti tek u `frmHRStavka.Load`. Izaberi rešenje koje radi i za dijalog i za forme sa karticama.

**Prihvatanje**
- Organizacione jedinice: „+ Dodaj“ otvara dijalog, „Nadređena jedinica“ je prazna, snimanje radi.
- Organizacione jedinice: „Izmeni“ na postojećem redu otvara dijalog sa tačnom nadređenom jedinicom.
- Vrste rada: „+ Dodaj“ radi.
- Zahtev ZN-2026/0001 → Stavke → „+ Dodaj“ radi, stavka se snima, zahtev može da se podnese.

---

## 2. Zaposleni dobija `ID = NULL` (KRITIČNO, baza + kod)

**Simptom**
„+ Novi zaposleni“ → popunjena polja → „Sačuvaj“: forma ne javlja grešku i ostaje na „Novi zaposleni“, a u bazi nastaje red sa `ID = NULL`. Takav zaposleni:
- ne može da se otvori,
- ne pojavljuje se u Evidenciji rada,
- ruši Godišnji odmori → „Generiši za godinu“ za **sve** zaposlene:
  `Cannot insert the value NULL into column 'ZaposleniSifra', table 'Testiranje.dbo.ZaposleniOsnovGO'`.

**Uzrok (baza Testiranje)**
- `dbo.Zaposleni` je postojala pre HR skripte, pa je `IF OBJECT_ID('dbo.Zaposleni','U') IS NULL CREATE TABLE ...` u `HR_Modul_2026_10.sql` preskočen.
- Postojeća kolona `ID` je `int NULL`, **bez IDENTITY i bez primarnog ključa** (tabela je heap). Postoje samo unikatni indeksi `UX_Zaposleni_Code`, `UX_Zaposleni_SifraERP`, `UX_Zaposleni_JMBG` i okidač `trg_Zaposleni_HR`.
- Zbog toga `SCOPE_IDENTITY()` u `frmHRZaposleni.Sacuvaj` vraća NULL (`id` ostaje 0), a nijedan spoljni ključ ka `Zaposleni(ID)` nije napravljen (`sys.foreign_keys` vraća 0). Pogođeni su npr. `FK_Zaposleni_Nadredjeni`, `FK_EvidencijaRada_Zaposleni`, `FK_GODopust_Zaposleni`, `FK_ZaposleniOsnovGO_Zaposleni`, `FK_BeleskaORadu_Zaposleni` u `HR_Modul_Faza2_2026_10.sql`, kao i FK-ovi tabela `Zaposleni*` iz prve skripte.
- Stanje u Testiranje: 2 reda (ID 1 „NEMA NEMA“ i test red TEST001 sa ID NULL).

**Ispravka**

a) **Nova SQL skripta** `Saobracaj/HR/HR_Modul_ZaposleniID_2026_10.sql`. Ne dodaje se u `.csproj`, kao i postojeće `.sql`. Mora biti idempotentna i bezbedna za sve firme (Leget, TA, DPT, VSD, KP):
1. Ako `Zaposleni.ID` već jeste IDENTITY i PK, ne radi ništa.
2. Pre izmene prijavi (`RAISERROR`, pa prekid) ako postoje redovi sa `ID IS NULL` ili duplikati `ID`. Popravku tih redova ne radi automatski: to rešava korisnik (vidi tačku 4).
3. Pretvori `ID` u `int IDENTITY NOT NULL` uz očuvanje postojećih vrednosti. Preporuka: `ALTER TABLE ... SWITCH` u novu tabelu iste strukture sa `IDENTITY`, pa `DBCC CHECKIDENT` na `MAX(ID)`. Druga mogućnost je kopiranje sa `SET IDENTITY_INSERT`. Sačuvaj sve kolone, `RowVer`, default-e, check-ove, unikatne indekse i okidač `trg_Zaposleni_HR`. Pazi na 29 objekata koji zavise od `dbo.Zaposleni` (`sys.sql_expression_dependencies`), uključujući poglede i procedure drugih modula.
4. Dodaj `PK_Zaposleni` na `ID`.
5. Ponovo napravi spoljne ključeve ka `Zaposleni(ID)` iz obe HR skripte, uz `IF OBJECT_ID('dbo.FK_...','F') IS NULL`.
6. Sve u transakciji (`SET XACT_ABORT ON`).

b) **Zaštita u postojećoj skripti**: na početak `HR_Modul_2026_10.sql`, posle `CREATE TABLE` bloka za Zaposleni, dodaj proveru. Ako `ID` nije IDENTITY ili nema PK, skripta staje sa jasnom porukom: „Pokrenite prvo HR_Modul_ZaposleniID_2026_10.sql“. Tako se nijedna baza više neće tiho poluinstalirati.

c) **Kod** (`Saobracaj/HR/frmHRZaposleni.cs`, `Sacuvaj`, grana `id == 0`): ako `INSERT` vrati NULL/0, ne nastavljaj tiho.
- Prikaži poruku da zapis nije dobio šifru i da treba pokrenuti skriptu.
- Ne ostavljaj zapis bez ID-a u bazi: `INSERT` i proveru radi u transakciji pa uradi rollback, ili `INSERT` uslovi postojanjem IDENTITY kolone.
- Isto proveri za `HRTabelaKartica.SnimiOpste` (vraća `CeoBroj(SCOPE_IDENTITY())`).

**Pokretanje na bazi**
Skriptu pusti na **Testiranje** tek posle tačke 4 (brisanje reda sa ID NULL), i to samo uz potvrdu korisnika. Na produkcionim bazama ne puštaj ništa. Samo napiši upit koji korisnik može da pokrene da proveri stanje:

```sql
SELECT c.is_identity, c.is_nullable,
       (SELECT COUNT(*) FROM sys.key_constraints k WHERE k.parent_object_id = OBJECT_ID('dbo.Zaposleni') AND k.type = 'PK') AS ima_pk
FROM sys.columns c WHERE c.object_id = OBJECT_ID('dbo.Zaposleni') AND c.name = 'ID';
```

**Prihvatanje**
- Novi zaposleni posle „Sačuvaj“ dobija ID, naslov forme se menja u ime zaposlenog i ostale kartice se mogu popunjavati.
- Zaposleni se vidi u Evidenciji rada.
- „Generiši za godinu“ (2026) prolazi bez greške.
- `sys.foreign_keys` za `dbo.Zaposleni` vraća očekivane FK-ove.

---

## 3. Dupli klik na listi zaposlenih ne otvara zaposlenog (PROVERITI)

**Simptom**
Zaposleni → Lista: dupli klik na red („Dupli klik na red otvara formu zaposlenog“, piše u dnu) samo stavi ćeliju u režim unosa (okvir sa kursorom), a forma se ne otvori. Probano tri puta, na kolonama Code, Prezime i ime, Status, na redu „NEMA NEMA“ (ID 1). Na listi nema drugog dugmeta za otvaranje, pa kartice zaposlenog nisu testirane.

Napomena: klikovi su slati automatizovano, pa prvo ručno proveri mišem da li se problem ponavlja.

**Gde gledati**
- `HRGrid.DuplikKlik` koristi `TableControlCellDoubleClick`. `HRGrid.Podesi` postavlja `AllowEdit = false`, ali ćelija se i dalje aktivira (renderer TextBox), pa dupli klik verovatno završi u kontroli ćelije i događaj se ne podigne.
- Mogući pravci: `grid.TableModel.Options.ActivateCurrentCellBehavior = GridCellActivateAction.None`, ili ćelije `ReadOnly`/`CellType = "Static"`, ili slušanje `TableControl.MouseDoubleClick` uz `PointToRowCol`.
- Ista metoda se koristi u `HRTabelaKartica` (Izmeni na dupli klik), u Kontrolnoj tabli, Zahtevima i drugde. Proveri sve.
- `HRGrid.IzabraniZapis` uz `AllowSelection = None` čita `CurrentRecord`. Proveri da posle izmene dupli klik otvara baš kliknuti red.

**Prihvatanje**
Dupli klik na red u listi Zaposleni, Zahtevi za nabavku i u šifarniku otvara odgovarajuću formu ili dijalog.

---

## 4. Test podaci u bazi Testiranje (čišćenje – SAMO UZ POTVRDU KORISNIKA)

Tokom testa uneto je sledeće. Pre brisanja pitaj korisnika šta ostaje.

| Šta | Gde |
|---|---|
| Zaposleni `Code = 'TEST001'` (Testović Petar), **ID NULL**: mora da se ukloni pre tačke 2 | `dbo.Zaposleni` |
| Radno mesto „TEST Vozač viljuškara“ | `dbo.RadnaMesta` |
| Mesto troška „TEST Terminal Batajnica“ (ERP `T-100`) | `dbo.MestaTroska` (proveri ime tabele u `DefMestaTroska`) |
| 13 praznika za 2026. (iz „Predloži praznike“), verovatno ih treba zadržati | tabela iz `DefPraznici` |
| Evidencija za NEMA NEMA: 13.10.2026 `BO`, 16.10.2026 `8+2` | `dbo.EvidencijaRada` |
| Zahtev za nabavku ZN-2026/0001 (Nacrt, bez stavki) | tabela zahteva iz `frmHRZahtev` |

Brisanje reda bez ID-a: `DELETE FROM dbo.Zaposleni WHERE ID IS NULL AND Code = N'TEST001';`. Proveri i `dbo.HRLog`/`ZaposleniPristupLog` da li okidač upisuje nešto vezano za taj red.

---

## Ostala zapažanja (nije obavezno, samo predlog – pitaj pre izmene)

- **Točkić miša nad ComboBox-om menja vrednost.** Na formi Zaposleni → Osnovno skrolovanje točkićem iznad polja „Država privremenog boravka“ promenilo je vrednost u „Estonija“, a forma se nije pomerila. Predlog: u `HRObrazac.NapraviKontrolu` za `ComboBox` i `DateTimePicker` prosleđivati `MouseWheel` roditeljskom panelu kad lista nije otvorena.
- **Neobavezna lista se teško prazni.** Brisanje teksta (Ctrl+A, Delete) ne pomaže, jer AutoComplete vrati poslednju vrednost na izlasku iz polja. Jedino radi izbor praznog reda na vrhu liste. Predlog: Delete/Backspace na praznom tekstu postavlja `SelectedIndex` na praznu stavku.
- **Korisnik `test` nije povezan sa zaposlenim** (`Korisnici.ZaposleniSifra`). Beleške o radu se zato ne mogu upisati (poruka je jasna), a brojač „Zahtevi za GO (n)“ je uvek 0. Ovo je podešavanje, ne greška. Za test povezati korisnika sa zaposlenim.

## Šta je radilo (ne dirati)

- Šifarnici bez neobaveznih lista: Radna mesta, Mesta troška (dodavanje i provera obaveznog polja).
- „Predloži praznike za godinu“ 2026: 13 praznika, Uskrs 10–13.04, Dan državnosti 15–16.02 + pomereni 17.02.
- Novi zaposleni: polja se snimaju, ispravan JMBG prolazi proveru; problem je samo ID iz tačke 2.
- Evidencija rada: upis `8+2`, `BO` (pitanje za doznaku), poruka za nepoznatu oznaku, F2 → prozor Dan.
- Zahtev za nabavku: snimanje zaglavlja, automatski broj ZN-2026/0001, status Nacrt.
- Kontrolna tabla rokova i HR log se otvaraju; log beleži izmene evidencije.

## Provera posle izmena

1. Build Debug (komanda iz `CLAUDE.md`), bez grešaka.
2. Pokreni `Saobracaj\bin\Debug\Saobracaj.exe`, prijava, „Novi izgled“, HR modul.
3. Prođi kriterijume prihvatanja iz tačaka 1–3.
4. Proveri da se ništa ne menja za druge firme (TA, DPT, VSD, KP): izmene su u zajedničkom kodu HR modula, a SQL skripta mora biti idempotentna.
