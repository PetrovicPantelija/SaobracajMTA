# Test: Kapija, Skladište, Izvoz, RNI

Datum: 24.11.2026
Grana: `master` (poslednji commit `d4baea7`)
Build: Debug, `Saobracaj.csproj`, bez grešaka (1 upozorenje CS0219 u `Drumski\InsertRadniNalogDrumski.cs:2799`, nije u obimu)

## 1. Obim i način rada

Pregledane su forme koje korisnik firme Leget otvara iz menija, po spisku u `docs/FORME-LEGET.md`:

| Modul | Šta je pregledano |
|---|---|
| Kapija | frmKapija1, frmKamionDetalji, frmKapijaOtvoreniNalozi, frmPregledKamiona, InsertKapija i prvi nivo formi koje Kapija otvara (frmPrijemVozaIzPlana, frmOtpremaKontejneraKamionomIzKontejnera, frmPrijemKontejneraKamionLegetIzvoz/Uvoz) |
| Skladište – carinsko | Pregled carinskog skladišta, Nalog za PRIJEM (frmNalogPrijem), Prijemnica sa uvozom packing liste, paletama, poljima i potvrdom, ViljuskaristiPregled, MagacinskaKnjigaCarinska, PregledLagera, skripta `Carina_PrijemnicaPL_2026_11.sql` |
| Skladište – komercijalno | SkladistaMain, KomercijalnoSkladiste/Pregled, NalogZaKomercijalni, PregledKomercijalnihNaloga, TipSkladista, RadniNaloziSkladista, OtpremaLager, Otprema, Otpremnica, InterniPoslovi, InterniPrenos, VanredniPoslovi, Pretovar, frmIzvestajCarinskoObelezje |
| Izvoz | Logistika izvoza (16 formi) i Izvoz forme iz Prijema i otpreme vozova (videti odeljak 6) |
| RNI | frmTerminalPrivremeni sa Arhivom, Dokumentima, Excel uvozom, Logom, Grupnom promenom i Slikama, frmScenarioSCI, klase izvoza brodara i SQL skripte u `RNI\` |

**Način rada:** statički pregled koda: čitanje koda po toku korisnika i provera upita i poziva procedura. Aplikacija nije pokretana, a pristupa bazi iz razvojne sesije nema.

**Šta to znači:**
- Svaki nalaz ima mesto u kodu (`fajl:red`) i proveren je čitanjem koda. Najvažniji nalazi su još jednom provereni direktno u fajlu.
- Ono što zavisi od procedura kojih nema u repozitorijumu (npr. `PotvrdiPrijem`, `InsertKapija`, `InsertRadniNalogSkladista`) nije potvrđeno. Takve stavke su u odeljcima „Proveriti“.
- Nalaz nije potvrđen u aplikaciji. Ručni scenariji iz odeljka 8 služe za to.

**Prioritet:**
- **V**: pad forme, pogrešan ili izgubljen podatak.
- **S**: smeta radu, a podatak ostaje ispravan.
- **N**: nisko.

**Pad forme:** `Program.cs` nema globalno hvatanje grešaka (`Application.ThreadException`). Neuhvaćena greška zato otvara standardni .NET prozor sa greškom: korisnik bira „Nastavi“ ili „Zatvori“, a radnja ostaje nedovršena.

Putanje u tabelama su relativne u odnosu na `Saobracaj\`.

## 2. Sažetak

| Modul | Nalaza | V | S | N | Proveriti |
|---|---|---|---|---|---|
| Kapija | 15 | 10 | 2 | 3 | 9 |
| Skladište – carinsko | 18 | 9 | 7 | 2 | 12 |
| Skladište – komercijalno | 15 | 12 | 3 | 0 | 11 |
| Izvoz | 18 | 17 | 1 | 0 | 10 |
| RNI | 14 | 3 | 6 | 5 | 11 |

### 2.1 Greške u kodu iz operativne faze (novembar 2026)

Ovi nalazi su u delovima koji su urađeni u Zadatku 2 i u ispravkama ISP-01..12. Treba ih ispraviti prvi, pre nego što se nova Prijemnica pusti u rad:

| Oznaka | Kratko |
|---|---|
| SKC-01 | Snimi u drugom prozoru vraća potvrđenu (ZA) prijemnicu na OD |
| SKC-04 | Koleta promenjena u glavnoj tabeli ne preračunava Količinu JM, neto, bruto i vrednost; kontrola CAR-P05 može lažno da prođe |
| SKC-05 | ISP-04 (NHM 7 cifara) blokira izmenu postojećih naimenovanja; `sp_Prijem_Kontrola` čita istu kolonu kao `NHM.ID` |
| SKC-03 | Novo dugme Dodaj za NHM upisuje NHM i naziv i kad ništa nije izabrano |
| SKC-10 | Greška pri snimanju stavki briše neusnimljen prenos iz PL i uparivanje |
| SKC-11 | Neispravan koeficijent iz fajla blokira prenos i kad artikal već postoji |
| SKC-12 | Nov nivo klasifikacije iz stabla ne može da se izabere za Formiraj |
| SKC-17 | Zatvorena grupa „Magacinski predmet“ sprečava osvežavanje magacinskog broja |
| SKC-18 | ISP-12 je u metodi koju nijedno dugme ne poziva; ISP-09 ponavlja postojeću grešku u kucanju „Komercijalo“ |

### 2.2 Ostalo što prvo treba ispraviti

| Oznaka | Kratko |
|---|---|
| IZV-17 | **Sve firme osim Legeta:** Otprema kamionom (GATE OUT) poziva samu sebe bez kraja → StackOverflow, aplikacija se gasi |
| IZV-15 | Vaganje se upisuje na drugi kontejner (grid bez filtera prepisuje ID kontejnera) |
| IZV-09 | Sačuvaj u planu bez ikakve izmene prepisuje ETA, plan utovara, vrednost robe i kontakt špeditera |
| IZV-11 | Grupna izmena u tabeli plana: datumi i tekst uvek daju SQL grešku, tri opcije pišu u nepostojeću kolonu |
| IZV-04 | Izmeni postojeći (Grupni unos) spaja stavku sa zaglavljem, pa Sačuvaj prepiše grupu praznim vrednostima |
| IZV-03 | Alfanumerički Booking i reference se tiho snimaju kao 0 |
| SKK-04 | Izmena Otpremnice menja zapis čiji je ID jednak broju radnog naloga (tuđu otpremnicu) |
| SKK-05 | Snimi u Internim poslovima, Internom prenosu i Vanrednim poslovima uvek javi grešku, pa drugi klik pravi duplikat naloga |
| SKK-07 | Potvrdi u Pregledu komercijalnih naloga upisuje svaki nalog kao Carinski |
| SKK-03 | Ponovno otvaranje Otpreme prepisuje napomenu, vlasnika i usluge podacima iz prijema |
| SKK-01 | Otprema propušta količinu kad je raspoloživo 0 koleta |
| KAP-01 | „Novi dolazak“ bez klika na Novi ne upisuje ništa, a prikaže „uspešno“ |
| KAP-03 | Potvrdi odlazak odjavljuje i DOLAZAK naloge (smer se čita iz tekućeg reda) |
| KAP-06 | Dugme „GATE IN KAMION UVOZ“ zove pogrešan konstruktor; šifra prijemnice se upisuje kao korisnik |
| KAP-08 / KAP-11 | Dvostruki klik na Sačuvaj na GATE IN/OUT pravi duple stavke i dupli RN4/RN6 |
| RNI-01 | Uvoz Excela upisuje iste kontejnere ponovo |
| RNI-02 | Izmena zapisa koji je drugi korisnik arhivirao pravi nove pokrete brodaru |
| RNI-07 | `&&` umesto `\|\|`: Cut-off port i napomene se prikazuju za pogrešne scenarije |

## 3. Zajedničke greške (ponavljaju se u svim modulima)

| Obrazac | Gde | Rešenje |
|---|---|---|
| ID novog zapisa se čita sa `SELECT MAX(ID)` posle upisa, van transakcije. Kad dva korisnika snime istovremeno, stavke i usluge idu na tuđi zapis | KAP-05, SKK-09, SKC-13, SKC-14, IZV-12, IZV-17 | Procedura vraća `SCOPE_IDENTITY()` kao OUTPUT parametar, kao što `InsertKapija` već radi sa `@IDPom` |
| `CurrentRecord` / `txtID` se koristi bez provere, pa klik bez izabranog reda ili na nesnimljenom zapisu obara formu | KAP-07, KAP-09, SKK-12, SKC-06, SKC-08, SKC-09, IZV-10, RNI-10 | Provera `CurrentRecord != null` i `int.TryParse`, uz poruku „Izaberite…“ / „Prvo sačuvajte…“ |
| Drugi klik na Sačuvaj ponovo prenosi stavke ili ponovo pravi RN | KAP-08, KAP-11, SKK-05, SKK-13, SKC-13, IZV-12, IZV-14 | Posle prenosa proveriti postojanje po NalogID ili onemogućiti dugme |
| Tekst ili broj se spaja u SQL: apostrof ili decimalni zarez obara formu | KAP-10, SKC-07, IZV-07, IZV-11, IZV-13 | Parametri |
| Datum se čita kao tekst (`Convert.ToDateTime(dtp.Text)`) ili broj sa tekućom kulturom: zavisi od regionalnih podešavanja računara | KAP (Proveriti), SKK (Proveriti), IZV-11, IZV-13 | `dtp.Value`, parametri |
| Vidljiva dugmad bez funkcije | KAP-15, SKK-15, IZV-18, RNI-13 | Povezati postojeću metodu ili sakriti dugme |
| Kod koji radi samo za Leget (ostale firme ne mogu da rade ili aplikacija pada) | IZV-17, Kapija (Proveriti: `panelHeader`) | Proveriti svaku formu sa `Firma == "Leget"` i za drugu firmu |
| Greška se proguta (`catch { }`), korisnik ne zna da radnja nije uspela | KAP-14, RNI-14 | Poruka ili zapis u status |

## 4. Kapija

| Oznaka | P | Mesto | Šta se dešava | Ispravka | Prihvatanje |
|---|---|---|---|---|---|
| KAP-01 | V | `Kapija\frmKamionDetalji.cs:306-318` | Novi dolazak → popuni polja → Sačuvaj bez „Novi“: `status` je false, poziva se `UpdeteKapija(0, …)`, ništa se ne upiše, a izađe „Unos je uspešno završen“. Isto posle „Otvori“ bez izabranog reda u Listi SVIH naloga | ID 0 ili prazan → INSERT; poruka samo ako je upisan bar jedan red | Sačuvaj bez Novi daje nov ID, zapis je u Listi SVIH naloga |
| KAP-02 | V | `Kapija\frmKamionDetalji.cs:288-296`; Designer `:224-235` | Datumi nemaju `ShowCheckBox`, pa je `Checked` uvek true. Kamion u krugu (DatumOdlaska NULL) posle izmene samo statusa dobija DatumOdlaska = 01.01.2000 ili vreme otvaranja forme | `ShowCheckBox = true`, `Checked = false` kad je u bazi NULL | Izmena samo statusa ostavlja DatumOdlaska prazan |
| KAP-03 | V | `Kapija\frmKapijaOtvoreniNalozi.cs:179-188` | Više izabranih redova: smer se čita iz `CurrentRecord`, ne iz reda koji se obrađuje. Tekući ODLAZAK → i DOLAZAK nalozi dobijaju `PromeniStatusKapijaOdlazak`. Tekući DOLAZAK → `return` prekida petlju bez poruke | Smer iz `selectedRecord.Record`, `continue` za ostale, poruka o preskočenim | Izbor ODLAZAK + DOLAZAK: obrađen samo ODLAZAK |
| KAP-04 | V | `Kapija\frmKapijaOtvoreniNalozi.cs:152, 165, 172` | Posle „Potvrdi dolazak“ u `txtID` je ID iz tabele Kapija, a „Prijavi neslaganje“ ga šalje kao `@NalogID`: neslaganje ide na pogrešan nalog, uz poruku o uspehu | KomNalogID čitati iz izabranog reda | Potvrdi dolazak na X → Prijavi neslaganje → Refresh: crven je X |
| KAP-05 | V | `Kapija\frmKapijaOtvoreniNalozi.cs:377-381, 435`; `Uvoz\frmPrijemVozaIzPlana.cs:351-354, 385`; `Izvoz\frmOtpremaKontejneraKamionomIzKontejnera.cs:322-325, 403`; `Dokumenta\frmPrijemKontejneraKamionLegetIzvoz.cs:585, 636-639` | Posle upisa prijemnice/otpreme ID se uzima kao `MAX(ID)`. Dva istovremena GATE IN/OUT: kontejner i RN4/RN6 idu na tuđu prijemnicu | OUTPUT `SCOPE_IDENTITY()` | Dva računara istovremeno: svaka prijemnica ima samo svoj kontejner |
| KAP-06 | V | `Uvoz\frmPrijemVozaIzPlana.cs:872`; `Dokumenta\frmPrijemKontejneraKamionLegetUvoz.cs:496` | „OTVORI - GATE IN KAMION UVOZ“ zove konstruktor `(string Korisnik, int Vozom)`: šifra prijemnice postaje korisnik, forma je prazna, a sledeće snimanje upisuje korisnika „1234“ | `new …(Convert.ToInt32(txtSifra.Text), KorisnikCene, 0)` uz proveru šifre | Dugme otvara tu prijemnicu, Korisnik je prijavljeni korisnik |
| KAP-07 | V | `Uvoz\frmPrijemVozaIzPlana.cs:843, 866, 878` | Dugmad OTVORI pre formiranja prijemnice: `Convert.ToInt32("")` → FormatException | Kao u `frmOtpremaKontejneraKamionomIzKontejnera.cs:429`: poruka „Morate prvo formirati zapis“ | Svako OTVORI pre snimanja daje poruku |
| KAP-08 | V | `Izvoz\frmOtpremaKontejneraKamionomIzKontejnera.cs:318-359` | Sačuvaj bez Novi: zaglavlje se ne upiše, a prenos kontejnera radi `Convert.ToInt32("")`. Drugi klik posle uspeha ponovo prenosi isti kontejner i nudi RN6 | Bez šifre poruka; provera da li je kontejner već na otpremi | Dva Sačuvaj: jedna stavka, jedan RN6 |
| KAP-09 | V | `Kapija\frmKamionDetalji.cs:334`; `Kapija\InsertKapija.cs:401-419` | Obriši posle Novi: `Convert.ToInt32("")`. Sa ID 0: ništa se ne obriše, forma se isprazni. `DelKapija` nikad ne postavlja `error = false` | Provera ID > 0; `error = false` posle uspeha | Obriši na praznoj formi daje „Izaberite zapis“ |
| KAP-10 | V | `Uvoz\frmPrijemVozaIzPlana.cs:898, 914-918`; `Izvoz\frmOtpremaKontejneraKamionomIzKontejnera.cs:488, 503-506` | `txtNalogID` može da se kuca; na svaku promenu se izvršava SQL sa sirovim tekstom. Brisanje broja ili slovo → SqlException; SQL injection | ReadOnly + `int.TryParse` + parametar | Kucanje u NalogID ne obara formu |
| KAP-11 | S | `Uvoz\frmPrijemVozaIzPlana.cs:345-358, 557-561, 629` | Sačuvaj bez Novi za kamion: ništa, bez poruke. Dva Sačuvaj: plan se prenosi dvaput, RN4 se nudi ponovo. Provera za uvoz `== "1"` propušta 2+ postojećih RN4 | Poruka bez šifre; provera postojanja `> 0` | Dva Sačuvaj: jedna stavka, jedan RN4 |
| KAP-12 | S | `Kapija\frmPregledKamiona.cs:135-148` | Datumi dolaze kao tekst `dd.mm.yyyy`: nema vremena, sortiranje je po tekstu (01.12. pre 02.11.), nema ORDER BY ni filtera po periodu | datetime kolone sa formatom „dd.MM.yyyy HH:mm“, `ORDER BY k.ID DESC`, filter po periodu | Sortiranje hronološko, vreme se vidi |
| KAP-13 | N | `Kapija\frmKapijaOtvoreniNalozi.cs:134-151` | Potvrdi dolazak bez izbora ne radi ništa i ne javlja. U „Kontakt unutar firme“ se upisuje telefon vozača | Poruka; KontaktUnutarFirme = KorisnikIzdao ili null | Klik bez izbora daje poruku |
| KAP-14 | N | `Kapija\frmKapijaOtvoreniNalozi.cs:528-533, 590-606` | Upis loga „Kamion je napustio kompleks“ guta svaku grešku. Kod više zapisa u Kapiji `Rows[0]` je proizvoljan | Poruka; `TOP 1 … ORDER BY ID DESC` | KontejnerLog ima zapis posle odlaska |
| KAP-15 | N | `MainLeget\Kapija\frmKapija1.Designer.cs:96, 116, 157` | „Izmeni postojeći“, „Izbriši postojeći“ i „Lista naloga Parking“ nemaju Click | Povezati ili sakriti | Svako vidljivo dugme otvara formu |

**Proveriti (Kapija):**
- `InsertKapija` (nije u repou): da li postavlja DatumDolaska i `RadniNalogInterni.KapijaUlaz`. Ako ne postavlja, „Potvrdi dolazak“ za isti nalog može više puta, jer C# ne proverava duplikat. Ako vrati NULL za `@IDPom`, `InsertKapija.cs:115` baca InvalidCastException, koji se ne hvata.
- `PromeniStatusKapijaOdlazak`: da li upisuje `Kapija.DatumOdlaska`, koji log odmah čita.
- `UpdateStatusKamionaKapija` ne dobija DatumPromeneStatusa, a frmKamionDetalji ga šalje.
- `frmPrijemVozaIzPlana.cs:459`: `Convert.ToInt32(PlanID)` pri otvaranju. Ako je PlanID NULL za izvozni nalog, forma pada pri otvaranju.
- `frmKapijaOtvoreniNalozi.cs:322-323`: rezultat `VratiKonkretanIDUsluge` se ne koristi; `Cirada` je fiksno 0.
- Kolone bez prefiksa tabele (`Kalmar`, `KapijaUlaz`, `KapijaIzlaz`, `Scenario`): opasnost od greške „Ambiguous column“ kad se tabela proširi.
- Spoj RadniNalogInterni i RadniNalogInterniPotvrda: nalog sa više potvrda se u listi „u NAJAVI“ vidi više puta.
- **Ostale firme:** u frmKamionDetalji i frmPregledKamiona sva dugmad su u `panelHeader`, koji je sakriven kad firma nije Leget (`frmKamionDetalji.cs:39, 128`). Druge firme ne mogu da snime.
- `Convert.ToDateTime(dtpDatumPrijema.Text)` (`frmPrijemVozaIzPlana.cs:352`, `frmOtpremaKontejneraKamionomIzKontejnera.cs:323`) pada na računaru sa en-US podešavanjima.

## 5. Skladište

### 5.1 Carinsko skladište i Prijemnica

Oznaka ★ = kod iz operativne faze (novembar 2026).

| Oznaka | P | Mesto | Šta se dešava | Ispravka | Prihvatanje |
|---|---|---|---|---|---|
| SKC-01 ★ | V | `Skladista main\Dokumenta\Prijemnica.cs:1385-1387`; `Prijemnica.UvozPL.cs:98-101, 135-138` | Ista prijemnica u dva prozora. B potvrdi (ZA), A klikne Snimi: šalje se stari status OD i prijemnica se vraća na OD. A menja stavke potvrđene prijemnice, jer je status učitan samo pri otvaranju; stavke koje je B dodao brišu se | Na početku Snimi ponovo učitati status; ako je u bazi ZA, prekinuti i učitati stavke; za zaglavlje slati status iz baze | Status ostaje ZA, A dobija poruku i ne menja stavke |
| SKC-02 | V | `Skladista main\frmNalogPrijem.cs:2370-2376, 2462-2476` | Posle Izmeni/Dodaj polja se obrišu, a `txtIDStavke` ostaje. Sledeći Obriši briše naimenovanje bez pitanja, bez provere da li ga koristi neka prijemnica | Obrisati `txtIDStavke` u `ResetujPoljaArtikli`; potvrda i provera `IDNaimenovanja` | Obriši posle Izmeni: „Nije izabrana stavka“ |
| SKC-03 ★ | V | `Prijemnica.cs:1083-1092`; `Prijemnica.Raspored.cs:281-289` | NHM lista: klik na ćeliju (ne na zaglavlje reda) pa Dodaj. `row.Selected` je false, ništa se ne izabere, a nov handler ipak upiše stari `nhm`, `IDArtikla = 0` i naziv iz `txtArtikal` | `dataGridView1.CurrentRow`; bez izbora panel ostaje otvoren | Klik na ćeliju pa Dodaj upisuje NHM tog reda; bez izbora stavka se ne menja |
| SKC-04 ★ | V | `Prijemnica.Raspored.cs:430-432`; `Prijemnica.UvozPL.cs:1424, 1438-1451` | Koleta se menja u glavnoj tabeli, a preračun (Količina JM, neto, bruto, vrednost po PL) postoji samo u Uparivanju. CAR-P05 tada poredi pune PL vrednosti sa carinom i delimičan prijem prolazi kao „Slaže se“ | Za stavke sa `IDPLStavke` posle izmene Koleta pozvati `PromeniKoleta`, ili kolonu zaključati | Koleta 100→80 u glavnoj tabeli daje isti preračun kao u Uparivanju |
| SKC-05 ★ | V | `frmNalogPrijem.Ispravke.cs:58-61`; `frmNalogPrijem.Designer.cs:1591`; `Carina_PrijemnicaPL_2026_11.sql:777`; `Prijemnica.cs:1087`; `Prijemnica.UvozPL.cs:846` | NHM naimenovanja: ISP-04 traži 7 cifara, a staro polje je primalo do 1.000.000, pa postojeća naimenovanja ne mogu da se izmene. `sp_Prijem_Kontrola` istu kolonu spaja kao `NHM.ID`, a Prijemnica u `stavka.NHM` upisuje `NHM.ID` | Odlučiti da li kolona čuva ID ili tarifni broj i uskladiti ISP-04, proceduru i Prijemnicu | Postojeće naimenovanje se menja bez promene NHM; NHMBroj u Uparivanju popunjen |
| SKC-06 | V | `Skladista main\ViljuskaristiPregled.cs:57, 108, 130, 148-153` | Klik na red rukovaoca prikazuje usluge drugog naloga: koristi se ID naloga rukovaocu kao `RadniNalogSkladista.ID` (treba `brLONaloga`). Klik na zaglavlje ili filter bez izbora → NullReferenceException | `brLONaloga` u upitu; provera `CurrentRecord` | Usluge su iz otvorenog naloga; klik bez izbora ne obara |
| SKC-07 | V | `Prijemnica.cs:765, 777` | Pretraga NHM sa apostrofom („d'oro“): tekst se spaja u SQL, `da.Fill` u KeyUp nema try | Parametar `@T` | Pretraga sa `'`, `%`, `[` radi |
| SKC-08 | V | `Prijemnica.cs:906, 1442-1446` | „Nalog rukovaocima“ na nesnimljenoj prijemnici: `Convert.ToInt32("")`. `panel7` se otvara ispod panela Uvoz/Uparivanje/Palete | Poruka za praznu prijemnicu; `panel7.BringToFront()` | Poruka na novoj prijemnici; panel uvek iznad |
| SKC-09 | V | `frmNalogPrijem.cs:549-561` | Nalogodavac ili Način pakovanja obrisan ili ukucan naziv kog nema: `SelectedValue.ToString()` → NullReferenceException | `Convert.ToString(SelectedValue)`; za nalogodavca poruka | Prazan Način pakovanja snima NULL, prazan Nalogodavac daje poruku |
| SKC-10 ★ | S | `Prijemnica.UvozPL.cs:1643-1648` | Greška pri snimanju jedne stavke ponovo učitava stavke iz baze: gube se neusnimljen prenos PL, uparivanje i izmene koleta | Posle rollback-a vratiti na 0 samo nove ID-jeve, listu zadržati | Posle greške tabela ostaje ista; drugi Snimi uspeva |
| SKC-11 ★ | S | `Carina_PrijemnicaPL_2026_11.sql:367-369`; `Prijemnica.UvozPL.cs:478-480` | Neispravan koeficijent iz fajla (2/5, nečitljiv) daje Greška i kad artikal postoji i ima svoj koeficijent. Izmena „JM po koletu“ ide u Artikli, ne u red PL, pa greška ostaje | ISP-02 samo za redove bez `IDArtikla`; za postojeći artikal Upozorenje | Postojeći artikal sa lošim koeficijentom u fajlu se prenosi |
| SKC-12 ★ | S | `Prijemnica.UvozPL.cs:267-271, 323-324` | Lista Klasifikacija novih artikala se puni jednom. Nivo uvezen posle toga (Uvoz artikala, Formiraj) nije u listi; dvoklik u stablu tiho briše izbor | Ponovo napuniti listu u callback-u i posle Formiraj | Dvoklik na nov nivo ga izabere |
| SKC-13 | S | `Prijemnica.cs:1144-1159` | Nalog rukovaocu: redovi bez transakcije; NULL u `Paleta1`/`VrstaPaleta` prekida petlju posle delimičnog upisa; drugi klik duplira; poruka prikazuje MAX(ID)+1 | Transakcija, provera NULL i postojećeg naloga, ID iz INSERT-a | Greška ne ostavlja delimičan nalog; nema duplikata |
| SKC-14 | S | `Prijemnica.cs:1373`; `frmNalogPrijem.cs:1216-1231, 1277-1293` | Broj nove prijemnice sa `MAX(ID)`. Magacinski broj se nudi kao MAX+1, a za postojeći broj `btnMbSave` tiho menja naziv: dva naloga dobiju isti broj | ID iz procedure; magacinski broj dodeljivati u bazi | Dva istovremena snimanja: različite prijemnice i brojevi |
| SKC-15 | S | `Skladista main\CarinskoSkladistePregled.cs:85, 104-119, 171-175`; `Prijemnica.cs:96` | `Ulaz` se ne briše pre čitanja: nalog bez RadniNalogInterni otvara se u režimu prethodnog (npr. Direktni). NULL u OJIzdavanja → FormatException. Dugme Prijemnica šalje `Vrsta` koju postavlja samo dugme Radni nalog | `Ulaz = null` pre čitanja, `TryParse`, `Vrsta` iz izabranog reda | Svaki nalog u svom režimu; Prijemnica prikazuje magacinski broj |
| SKC-16 | S | `frmNalogPrijem.cs:44, 60, 88-92, 193` | `int LOPotvrdjen = …` u konstruktoru pravi lokalnu promenljivu; polje ostaje 0, pa potvrđen komercijalni nalog i dalje nudi LO potvrdu i Obriši | Bez `int` | Nalog sa LO = 1 ne prikazuje LO potvrdu |
| SKC-17 ★ | N | `frmNalogPrijem.cs:1410` | Kad je grupa „Magacinski predmet…“ zatvorena, `panelMP.Visible` je false, pa izbor magacinskog broja ne menja `txtMagacinskiBroj` | `GrupeForme.Vidljiva(panelMP)` ili ukloniti uslov | Promena broja pri zatvorenoj grupi se vidi |
| SKC-18 ★ | N | `frmNalogPrijem.cs:1177, 1353, 1385`; `frmNalogPrijem.Ispravke.cs:99` | ISP-12 je u `btnNoviMP_Click`, koji nijedno dugme ne poziva, pa nema efekta. Postojeći kod (`:1177, 1182`) i ISP-09 porede sa „Komercijalo“, a `TipSkladista.cs:58` šalje „Komercijalno“: lista magacinskih brojeva za komercijalne naloge se nikad ne puni | ISP-12 premestiti u stvarno snimanje magacinskog predmeta; ispraviti „Komercijalno“ na sva tri mesta | Izmena magacinskog predmeta se snima; komercijalni nalog ima listu brojeva |

**Proveriti (carinsko):**
- `PotvrdiPrijem` (nije u repou): da li odbija prijemnicu koja je već ZA. Ako ne odbija, posle SKC-01 promet se knjiži dvaput. Treba proveriti i da li `Promet.IDSaDokumenta` čuva ID stavke.
- Potvrda: `VratiNalogIDF` nema filter `IDManipulacijaJED`, pa nije jasno koji interni nalog se potvrđuje. `PotvrdiPrijem` i `UpdRadniNalogInterni…` nisu u istoj transakciji.
- `UpdateRNCarinskoSkladistePrijemnica`: ako upisuje Status, stari režim (bez skripte) i stara forma `Skladista\PrijemnicaCarinskoSkladiste` na Snimi vraćaju ZA na OD i prave stavke iznova (novi ID), pa se gubi veza sa prometom, paletama i PL.
- Korekcija posle ZA menja koleta knjižene stavke, a promet se ne ispravlja. Pravo korekcije se proverava samo u C#.
- `NaloziViljuskaristiPregled` se nigde ne otvara. Upit koristi `RNCarinskoSkladisteRukovalac.Prijemnica` uz alias `rn` (greška 4104), a forma zove `PotvrdiPrijem` bez provera CAR-P04/P05/PL01/PP08.
- `PregledLagera`: `INNER JOIN TipPalete` izbacuje stavke bez vrste palete. Otprema se spaja po tekstu Pozicija, pa dodela polja posle ZA može da prekine umanjenje lagera.
- `KoloneGrida.Primeni`: proveriti u aplikaciji da li Syncfusion posle ponovnog vezivanja zadržava sklonjene kolone u meniju.
- CAR-P05 poredi vrednost bez provere valute.
- `frmNalogPrijem.VratiPodatke`: `Convert` nad `CarinskiPostupak`, `OdredisnaCarinarnicaPrijem` i `PlaniraniDatum2Prijem` bez provere DBNull.
- `sp_PL_FormirajArtikle`: kad je ključ artikla Naziv, šifra iz KataloškiBroj/BarKod može da se ponovi (UX Šifra + Katalog). Ako `Artikli.ID` nije IDENTITY, `MAX(ID)+ROW_NUMBER` radi bez UPDLOCK.
- `ProveriPLKolone`: `catch { return false; }` posle prolazne greške tiho prebacuje formu u stari režim (delete + insert stavki).
- Uvoz artikala kataloga čita koeficijent sa `BrojIzCelije`, pa „1/5“ daje NULL; uvoz PL koristi `KoeficijentIzCelije`.

### 5.2 Komercijalno skladište, otprema, radni nalozi

| Oznaka | P | Mesto | Šta se dešava | Ispravka | Prihvatanje |
|---|---|---|---|---|---|
| SKK-01 | V | `Skladista main\OtpremaLager.cs:1004` (i 1010-1022, 1157); `Skladista main\Dokumenta\Otprema.cs:1441` | Stavka ostaje na lageru ako je bilo koja veličina > 0. Ako je raspoloživo 0 koleta (a bruto 500 kg), uslov `ukupno > 0 && nova > ukupno` se preskače i otprema se 50 koleta kojih nema | Odbiti svaku količinu veću od raspoložive, i kad je raspoloživo 0 | Stavka sa 0 koleta: unos 1 daje poruku |
| SKK-02 | V | `OtpremaLager.cs:1028, 662, 671`; `Otprema.cs:1465`; `InsertSkladista.cs:1723` | Naziv artikla može da se menja, a lager se sa rezervacijom i otpremnicom povezuje po tekstu (Naziv, NHM, Pozicija), ne po ID stavke. Posle promene naziva rezervacija se ne oduzima i druga otprema uzima istu robu | Zaključati naziv; čuvati ID stavke prijemnice u rezervaciji i otpremnici | Posle promene naziva lager za drugu otpremu je i dalje umanjen |
| SKK-03 | V | `Otprema.cs:211-243, 637, 943`; `Otpremnica.cs:305-334` | Ponovno otvaranje sačuvane otpreme: kod posle učitavanja upiše Nalogodavca, Vlasnika, Vrstu robe, Napomenu i Posebne uslove iz naloga prijema. Usluge se filtriraju po `d.RN` sa ID grupe usluga. Snimi trajno upisuje tuđe podatke | Ne učitavati prijem kad je nalog otvoren iz baze; usluge prijema po `d.ID` | Napomena „X“ i usluga A ostaju posle ponovnog otvaranja |
| SKK-04 | V | `Skladista main\Dokumenta\Otpremnica.cs:757` | Izmena postojeće otpremnice: kao ID otpremnice šalje se `txtID` (broj radnog naloga), pa se menja otpremnica sa tim ID-jem | Prvi argument `txtOtpremnica.Text` | Menja se samo zapis sa ID = txtOtpremnica |
| SKK-05 | V | `Dokumenta\InterniPoslovi.cs:543-546`; `InterniPrenos.cs:547`; `VanredniPoslovi.cs:544`; `InsertSkladista.cs:189-207` | Snimi: nalog se upiše, pa `InsertRNInterni(3, Korisnik)` šalje korisničko ime u parametar tipa Int → InvalidCastException, koji se ne hvata u metodi. Korisnik vidi „ERROR Insert“, txtID ostaje prazan, a drugi Snimi pravi isti nalog još jednom. U Otpremi je ovaj poziv zakomentarisan | Ukloniti ili ispraviti poziv kao u `Otprema.cs:957-964`; txtID odmah posle upisa | Jedan Snimi = jedan nalog i poruka „RADNI NALOG KREIRAN“ |
| SKK-06 | V | `Skladista main\PregledKomercijalnihNaloga.cs:200, 213-215, 436, 702` | „SKLADIŠNI NALOZI UVOZ“: Otvori pada jer Uvoz upit nema kolonu `LO`. Vrsta naloga i Potvrdi za Uvoz čitaju `IzvozKonacna` po BrojOsnov, što je ID iz `UvozKonacna`. „Pregledaj podatke“ šalje „Izvoz“ | `LO` u upit; `UvozKonacna` za Uvoz (ili sakriti Potvrdi za Uvoz) | Otvori, Potvrdi i Pregledaj za Uvoz prikazuju izabrani uvozni kontejner |
| SKK-07 | V | `PregledKomercijalnihNaloga.cs:523-537` | Potvrdi računa `SkladisteTip` (Komercijalno/Carinsko), a ipak uvek upisuje VrstaRN = „Carinsko“ i skladište 1008. Komercijalni nalog se ne vidi u Komercijalno skladište → Prijem robe | Proslediti `SkladisteTip`, skladište po tipu | Kontejner bez carinskog postupka daje RN Komercijalno |
| SKK-08 | V | `PregledKomercijalnihNaloga.cs:382, 411, 462-464, 605, 677` | `txtNALOGID` se puni samo klikom u ćeliju, a BrojOsnov se čita iz tekućeg reda. Posle Refresh: NullReferenceException. Posle strelica ili filtera: nalog od kontejnera B vezan za uslugu A. NULL u VrstaKontejnera/Izvoznik/Cirada obara | ID i BrojOsnov iz istog `CurrentRecord`; prazniti txtNALOGID u FillGV | Refresh pa Potvrdi daje poruku |
| SKK-09 | V | `Otprema.cs:765, 920, 943, 971-976`; `PregledKomercijalnihNaloga.cs:593-605`; `Otpremnica.cs:745`; `InterniPoslovi.cs:506, 519, 557` | Broj RN se računa kao MAX(ID)+1 pre upisa i pod njim se upisuju usluge; posle upisa txtID je MAX(ID). Istovremeno snimanje ili praznina u IDENTITY: usluge i stavke na tuđem nalogu | OUTPUT `SCOPE_IDENTITY()`, jedna transakcija | Dva istovremena snimanja: svaki svoje usluge i stavke |
| SKK-10 | V | `Otprema.cs:995-1004, 1072-1081` | Rezervacije se brišu pa upisuju jedna po jedna, svaka u svojoj konekciji, posle poruke „SAČUVAN“. Pad trećeg upisa ostavlja dve. Raspoloživo se ne proverava pri snimanju: dva korisnika rezervišu istu robu | Transakcija i provera stanja na serveru | Drugi korisnik dobija odbijenicu |
| SKK-11 | S | `Otpremnica.cs:315-331, 740`; `OtpremaLager.cs:569, 660` | Zaglavlje otpremnice dobija poslednju prijemnicu iz upita bez ORDER BY. Otpremljeno se oduzima po prijemnici zaglavlja: kod više prijemnica istog magacinskog broja umanjuje se pogrešna | Prijemnica po stavci otpremnice | Svaka prijemnica umanjena tačno za svoje stavke |
| SKK-12 | V | `KomercijalnoSkladistePregled.cs:94, 147`; `Otprema.cs:842, 881, 947, 1936`; `InterniPoslovi.cs:431` | Pad na običnim radnjama: Prijemnica bez izabranog reda, „IDI NA OTPREMNICU“ na nesnimljenoj otpremi, Vlasnik bez matičnog broja (strani partner), Snimi sa praznim PIB-om, Magacinski predmet sa NULL OJIzdavanja | `CurrentRecord != null`, `int.TryParse` sa porukom | Svaka radnja daje poruku |
| SKK-13 | S | `Otprema.cs:1016-1023`; `InterniPoslovi.cs:579-586` | `Formiran = 1` se postavlja pre snimanja. Ako snimanje padne, sledeći klik kaže „VEĆ FORMIRAN“, a Snimi pravi nalog sa Formiran = 1 | Postaviti posle uspešnog UpdateRadniNalog | FORMIRAN RN na nesnimljenom nalogu daje poruku |
| SKK-14 | S | `KomercijalnoSkladistePregled.cs:102-105`; `CarinskoSkladistePregled.cs:116` | Komercijalni pregled prepoznaje Direktni po OJ 4, carinski po OJ 7. `Ulaz` se ne briše, pa frmNalogPrijem dobija režim prethodnog reda | OJ 7 kao u carinskom; `Ulaz = null` pre čitanja | Direktni nalog posle uvoznog: Ulaz = Direktni |
| SKK-15 | S | `Otpremnica.cs:549-552, 579-582`; `Pretovar.Designer.cs`; `KomercijalnoSkladistePregled.Designer.cs`; SkladistaMain, KomercijalnoSkladiste, NalogZaKomercijalni | Dugmad bez funkcije. Otpremnica: Štampaj, Zapisnik, Pošalji špediciji; Dodaj usluge je vezan za praznu metodu (ispravna `btnDodajUsluge_Click` nije vezana); Nazad je prazan. Pretovar: nijedno dugme. Komercijalni pregled: Štampaj, Skeniraj, Radni nalozi imaju kod, ali nisu vezani. Meni: Fakturisanje, Pozicije, Ostala skladišta, 7 od 8 dugmadi u KomercijalnoSkladiste, Izmeni/Otkaži postojeći | Vezati postojeće metode, ostalo sakriti | Svako vidljivo dugme nešto radi |

**Proveriti (komercijalno):**
- `InsertRnCarinskoSkladisteOtpremaStavkePom` ne dobija RN. Ako procedura uzima MAX(ID), izmena starije otpreme kači stavke na najnoviji nalog.
- `InsertRNCarinskoSkladisteOtpremnica`: da li kopira rezervacije u otpremnicu, piše Promet i briše rezervacije. Ako rezervacije ostaju, bruto i vrednost se oduzimaju dvaput.
- Direktna otprema ne pravi interni nalog RN20 (pozivi zakomentarisani), a lista direktnih naloga radi INNER JOIN na RN20: nalog posle snimanja može da nestane iz liste.
- NULL u nalogu prijema (VlasnikRobe, OstalaSkladista, DodatneUslugeID) obara `Otprema.cs:228-241, 732-747, 1153-1161` i `Otpremnica.cs:321-333`.
- `decimal.TryParse` sa tekućom kulturom (`OtpremaLager.cs:1634`, `Otprema.cs:1856`): na sr-Latn „12.5“ postaje 125.
- Napuštena otprema trajno drži rezervaciju (STORNO onemogućen), ako na serveru nema čišćenja.
- „Otvori“ za Uvoz i Izvoz ima fiksno tipRN = „Prijem“ (`PregledKomercijalnihNaloga.cs:228`): grana za Otpremu se ne izvršava.
- Štampa Prijemnica (`KomercijalnoSkladistePregled.cs:176`) šalje ID radnog naloga; proveriti šta izveštaj očekuje.
- FORME-LEGET.md navodi Prijem („Otvori“), a svi pozivi su zakomentarisani.
- Uslovni formati u `PregledKomercijalnihNaloga.cs:152-178` se dodaju na svaki Refresh i tabelama bez tih kolona.

## 6. Izvoz

Pregledano je 27 formi: svih 16 iz „Logistika izvoza“, plus:

- **Plan i dokumenti:** frmFormiranjePlanaIzvoz, frmIzvozKonacna, frmIzvozKonacnaTable, frmDopunaPlanaPraznimIzvoz, DogovoreniPosloviIzvoza, frmIzvozDokumenta, frmKontaktOsobeMU
- **Otprema:** frmOtpremaKontejneraKamionomIzKontejnera, frmOtpremaVozaIzPlana
- **Vaganje:** Vaganje, VaganjePregled, IzvestajVaganja

| Oznaka | P | Mesto | Šta se dešava | Ispravka | Prihvatanje |
|---|---|---|---|---|---|
| IZV-01 | V | `Izvoz\frmProdajniNalogIzvoz.cs:240-256, 275-284, 404, 702, 727` | Porudžbenica sa obrisanim Brodarom ili Izvoznikom (ili tekstom kog nema u listi) i praznim Kvalitetom se snima sa 0. Lista „Kreiraj novi“ radi INNER JOIN na Brodar, Izvoznik i Kvalitet (`frmProdajniNalogIzvozTabela.cs:150-153`), pa se stavka više nikad ne vidi. Poruka o uspehu iskače jednom po stavci; red obrisan tasterom Delete ostaje u bazi | Brodar, Izvoznik i Kvalitet obavezni u validaciji | Porudžbenica bez brodara ili kvaliteta se ne snima; poruka jednom |
| IZV-02 | V | `Izvoz\frmProdajniNalogIzvozTabela.cs:291, 299, 308-311, 326-327, 473, 529-530` | Klik na stavku: odgovor na „Drumski…“ se odmah upisuje, pa X na ADR ostavlja polovično stanje. Zatvaranje izbora scenarija sa X ipak otvara Grupni unos sa scenariom 0. Gornja granica količine se izračuna i ne koristi, pa stavka može da se formira više puta i preko naručenog | Upis tek posle svih koraka; `return` kad dijalog nije OK; provera Kolicina − KolicinaFormirana | Cancel/X ne menja bazu; veća količina se odbija |
| IZV-03 | V | `Izvoz\frmGrupniUnosPoljaIzvoz.cs:1204, 1479-1482, 1526-1556, 1609`; `frmIzvozKonacna.cs:1063-1076`; `InsertIzvoz.cs:3393-3397` | Booking je u porudžbenici tekst, a Grupni unos ga parsira kao int: „MSCU1234567“ ili „INV-12“ (Ref) se tiho snimi kao 0. Plan sa istim podacima pada na `Convert.ToInt32(txtBokingBrodara.Text)` | Booking i reference kao tekst u celom lancu; do tada odbiti unos koji nije broj | Booking i Ref posle snimanja izgledaju isto |
| IZV-04 | V | `Izvoz\frmGrupniUnosPoljaIzvoz.cs:483` | Izmeni postojeći: `BrojStavkePorudzbenice` (ID stavke) se spaja sa `ProdajniNalogIzvoz.ID` (zaglavlje). Prikaže se tuđi broj dokumenta, ili se ništa ne učita, a Sačuvaj prepiše grupu praznim vrednostima | Spoj preko `ProdajniNalogIzvozStavke` | Tačan broj porudžbenice i sva polja |
| IZV-05 | V | `Izvoz\frmGrupniUnosPoljaIzvoz.cs:2614-2615, 2989, 3017, 3036, 3113-3119, 3234-3246` | Formiraj KN I Faza: provera čita `TOP 1` iz cele grupe, pa se svi kontejneri ocenjuju po jednom. Zastavice se ne resetuju: „No“ na trećem redu ostavlja prva dva u planu, a dugme posle toga ne radi. Prazan grid ipak pravi dva interna RN i javlja uspeh | Provera po ID-ju pre ijednog prenosa; reset zastavica; `return` bez redova | Kontejner bez broja: ništa se ne prenosi; prazan grid ne pravi RN |
| IZV-06 | V | `Izvoz\frmGrupniUnosPoljaIzvoz.cs:705, 745, 747, 885, 1182, 2010-2055, 2128-2129` | Posle „Uspešno formirano“ kolone su tekst: „2,5“ u Koleta ili „,“ u BTTO → FormatException van try. Kolona `UtovarCeradePlaniraniDt` (sc. 25) se ne snima. Skrivene kolone „Datum realizacije“ na svaku izmenu reda dobijaju današnji datum | TryParse sa porukom; ispraviti ime kolone; skrivenu kolonu popunjavati samo kad je prazna | „2,5“ daje poruku; datum utovara cerade ostaje |
| IZV-07 | V | `Izvoz\frmGrupniUnosPoljaIzvoz.cs:2175-2181, 2257-2268, 2422, 2450`; `frmIzvozKonacna.cs:1740, 1768` | Dodaj usluge pre Sačuvaj: `IN ()` → SqlException. Za tip kontejnera duži od 3 znaka upit zatvara string sa `''`, pa radi samo kad je treći znak apostrof („40'…“); za „45G1“, „20DV“ → „Unclosed quotation mark“ | Poruka „Prvo sačuvajte“; parametar `@Tip` | Usluge se otvaraju za sve tipove |
| IZV-08 | V | `Izvoz\frmCarinskiPostupak.cs:215, 220, 225, 362-377` | Prvi postupak u listi (po abecedi) preskače validaciju, a Sačuvaj radi `SelectedValue.ToString()` nad praznim combo-om → NullReferenceException. „Bescarinski“ je ovde prvi po abecedi, a u Grupnom unosu ID = 0 | `SelectedValue?.ToString()`; bescarinski po ID-ju | Bescarinski sa praznim carinarnicama se snima |
| IZV-09 | V | `Izvoz\frmIzvozKonacna.cs:1063-1080, 1382-1385, 1397, 1450-1495` | Plan, Sačuvaj bez izmena menja podatke. Učitavanje ETA Leget, Planiranog datuma utovara, Vrednosti robe, Kontakta špeditera, TaraZ i Terminala je zakomentarisano, a Sačuvaj ih upisuje iz kontrola (danas, vrednost prethodnog kontejnera, 0). Fali zarez posle `KontaktSpeditera`, pa se kontakt špeditera upisuje u KontaktOsobe. „Dobijen nalog“ se čita iz pogrešne kolone i snima kao 0 | Učitati sva polja koja Update šalje; zarez; `DobijenNalogKlijent1` | Sačuvaj bez izmena ne menja bazu |
| IZV-10 | V | `Izvoz\frmIzvozKonacna.cs:1063, 1114-1118, 1284-1290, 1396, 2664-2675, 2716-2721` | Klik na zaglavlje/filter ili Sačuvaj bez izabranog kontejnera: `Convert.ToInt32("")`, `throw ex`. NULL u SpediterRijeka, Klijent1–3, Scenario obara učitavanje. Obriši briše kontejner sa stavkama bez potvrde. Izbriši NHM briše NHM prethodnog kontejnera | Provera izbora; SafeInt; potvrda brisanja; prazniti `txtIDNHM` | Bez izbora nema pada; Izbriši NHM ne dira tuđi NHM |
| IZV-11 | V | `Izvoz\frmIzvozKonacnaTable.cs:586-618, 634, 658, 673, 679, 690, 701`; Designer `:329` | Grupna izmena u tabeli plana. Datumi idu u SQL bez navodnika i uvek daju SqlException. Tri opcije pišu u nepostojeću kolonu `obijeBZ`, „Inspekcija]“ ima višak zagrade. Tekst se lepi u SQL i pada na apostrof („40' HC“). Brodska plomba upisuje šifru brodara. Grid dozvoljava izbor samo jednog reda | Parametri; ispravne kolone; višestruki izbor | Svaka opcija menja tačno označene redove, i sa apostrofom |
| IZV-12 | V | `Izvoz\frmFormiranjePlanaIzvoz.cs:518-552, 576-623` | `Insert into Izvoz Default Values`, pa `MAX(ID)` na drugoj konekciji. Ako INSERT padne, podaci uvoza ipak idu u poslednji (tuđi) kontejner. Petlja ne koristi `selectedRecord`. Drugi klik na Prenesi pravi još jedan kontejner i RN | `OUTPUT INSERTED.ID`; `return` na grešku; provera da li je RN već prenet | Dva klika: jedan kontejner, jedan RN |
| IZV-13 | V | `Izvoz\frmIzvozUnosManipulacije.cs:193-200, 302, 306, 398, 454, 3191-3203`; `frmIzvozKopiranjeUslugaKontejnera1.cs:166, 214` | Železnina se traži u **Uvoz** tabelama sa ID izvoznog kontejnera, pa se ubacuje ponovo pri svakom otvaranju. Cena se lepi u SQL kao double: „12,5“ (srpska podešavanja) je sintaksna greška. Brisanje cene → `Convert.ToDouble("")`. Kopiranje usluga čita nepostojeću tabelu „Izvoza“, a grid ciljnih kontejnera se nikad ne puni | Izvoz tabele i parametri; TryParse; napuniti grid | Jedna železnina; cena 12,5 snimljena; Kopiraj radi |
| IZV-14 | V | `Izvoz\frmIzvozPregledKontejneraDrumskeUsluge.cs:59-60, 196-197`; `Uvoz\InsertUvoz.cs:1262` | Formiranje naloga: nalog se napravi uz poruku o uspehu, pa osvežavanje koristi `Izvoz.ID` bez tabele Izvoz u FROM → SqlException. Grid ostaje star, a drugi klik pravi još jedan RN | `RefreshGridPoPorudzbini()` ili ispraviti alias; onemogućiti dugme | Posle formiranja NalogID popunjen; drugi klik javlja da nalog postoji |
| IZV-15 | V | `Izvoz\Vaganje.cs:145, 160, 237, 273-282, 464, 469, 549`; `VaganjePregled.cs:185` | Vaganje za kontejner X: FillGV veže sva vaganja bez filtera, prvi red se izabere i prepiše zaključan `txtKontID`, vozilo i težine. Sačuvaj upisuje vaganje na drugi kontejner. Tara > Bruto: `txtNeto` (Minimum 0) → ArgumentOutOfRangeException. „Izvagani kontejneri“ → Otvori šalje Vaganje.ID umesto KontID | Filter na kontID, ne menjati `txtKontID`; provera neto < 0; `Cells["KontID"]` | Vaganje za X ima IzvozKonacnaID = X; Tara > Bruto daje poruku |
| IZV-16 | V | `Izvoz\frmIzvozDokumenta.cs:488, 497, 511-512` | Izbriši na tabu Buking ili Voz briše ID iz `txtSifra`, tj. dokument izabran na tabu Kontejner. Izbor bukinga upisuje putanju u polje taba Kontejner, pa Otvori otvara pogrešan fajl | `txtSifra2`/`txtSifra3`, `txtPutanja2`; potvrda | Brisanje bukinga ne dira dokument kontejnera |
| IZV-17 | V | `Izvoz\frmOtpremaKontejneraKamionomIzKontejnera.cs:22, 34, 108-113` (i 318-359, vidi KAP-08) | **Sve firme osim Legeta (TA, DPT, VSD, KP):** `ChangeTextBox()` u else grani poziva sam sebe → StackOverflow, aplikacija se gasi čim se forma otvori (GATE OUT sa kapije) | Ukloniti rekurzivni poziv u else grani | Forma se otvara pod TA.txt |
| IZV-18 | S | `MainLeget\Izvoz\LogistikaIzvoza0.cs:44, 55`; `LogistikaIzvoza1.cs:134-152`; `LogistikaIzvoza2.cs:87, 93-103, 138`; `Izvoz\frmProdajniNalogIzvoz.Designer.cs:412`; `frmPreglegPorudzbenica.Designer.cs:128`; `frmDopunaPlanaPraznimIzvoz.Designer.cs:84-93` | Prazni handleri: Prijem i izdavanje robe, Izvoz prazne opreme, Formiranje praznog voza, Nalog za fakturisanje, Lista OTVORENIH (REALIZOVANIH nije vezan). Bez Click: Formiraj nalog za otpremu, Otvori (radi samo desni klik), Vrati selektovano. **Prava:** Izmeni i Otkaži proveravaju pravo kartice „Kreiraj novi“, a Kreiraj i Lista SVIH proveravaju „Otkaži postojeći“ (`OtvoriFormuSaPravom` traži po tekstu dugmeta) | Svako dugme prosleđuje svoj `.Text`; prazna dugmad povezati ili sakriti | Korisnik sa samo pravom „Otkaži“ ne može da kreira |

**Proveriti (Izvoz):**
- `InsertIzvozPorudzbenica`: da li ograničava broj kontejnera na Kolicina − KolicinaFormirana i zatvara stavku kad je formirana cela.
- `Insert/UpdateKontejnerTekuce` sa ključem „IDPI“ + broj porudžbenice (`frmProdajniNalogIzvoz.cs:714, 723`): ista oznaka za sve stavke, pa update može da prepiše sve stavke poslednjom.
- `InsertIzvozNHM` i `InsertIzvozNapomenePozicioniranja`: privremene liste se ne prazne (`frmGrupniUnosPoljaIzvoz.cs:1657-1673`), pa svaki Sačuvaj ponovo šalje iste NHM i napomene. NHM combo nudi samo `top 100`.
- Da li je `IzvozKonacna.ID == Izvoz.ID` posle prenosa. Na tome se zasnivaju `UPDATE … Scenario = 11` u obe tabele i spoj u drumskim uslugama.
- Fiksni plan 40 u „Formiraj KN I/II Faza“ (`frmGrupniUnosPoljaIzvoz.cs:3234, 3243-3244, 3519`).
- `FillDG8` radi INNER JOIN na Platilac i KontejnerStatus: usluge sa platiocem 0 ili statusom 0 su nevidljive.
- `DeleteProdajniNalogIzvoz`, `DelKontejnerIzvoz`: da li vraćaju KolicinaFormirana i brišu „IDPI“ zapis i usluge.
- `frmPregledGrupaKontejneraIzvoz.cs:149`: Scenario bez ISNULL; NULL obara Otvori.
- Syncfusion `TableControlCellClick` na zaglavlju/filteru: da li pokreće tok nad prethodnim CurrentRecord (porudžbenice, grupe, lista stavki). Proveriti ručno.
- Vaganje: Neto se šalje kao @Bruto (`Vaganje.cs:314, 319`); da li `InsertVaganje` sprečava drugo vaganje istog kontejnera.

## 7. RNI (Terminal privremeni, Scenario SCI)

| Oznaka | P | Mesto | Šta se dešava | Ispravka | Prihvatanje |
|---|---|---|---|---|---|
| RNI-01 | V | `RNI\frmTerminalPrivremeniExcel.cs:182-185, 207`; `TerminalPriv_UvozExcel_BL_LR_2026_11.sql:25-26`; `TerminalPriv.sql:42-46` | Ponovljen uvoz istog Excela ili isti kontejner dva puta u fajlu: svi se upisuju ponovo i za oba zapisa se prave pokreti brodara. Ni ručni unos nema proveru | Pre upisa proveriti duplikate u fajlu i aktivne zapise (`Konačni_GATE_OUT IS NULL`) | Drugi uvoz istog fajla ne upisuje ništa i navodi postojeće kontejnere |
| RNI-02 | V | `TerminalPriv.sql:172, 1345-1399`; `insertTerminalPriv.cs:201` | Korisnik B arhivira ili obriše zapis, A ga menja i potvrdi: UPDATE pogodi 0 redova bez greške, stari datumi su NULL, pa procedure pokreta prave pokrete za sve popunjene datume; poruka „Upisaću … (broj pokreta: N)“ | `IF @@ROWCOUNT = 0 THROW …` posle UPDATE | Izmena arhiviranog zapisa daje grešku, bez novih pokreta |
| RNI-03 | V | `RNI\frmTerminalPrivremeni.cs:328-334` (i 518) | Prelaz sa zapisa sa datumom na zapis bez datuma (ili Novi), pa štikliranje datuma: polje nosi datum prethodnog zapisa, a taj datum odmah ide u pokret brodaru | Za prazan datum postaviti `dtp.Value = DateTime.Now` pre `Checked = false` | Štikliranje pokazuje trenutni datum |
| RNI-04 | S | `frmTerminalPrivremeni.cs` (nema FormClosing); `frmTerminalPrivremeniPromeni.cs:116` | Posle Grupne promene („Za upis kliknite Sačuvaj izmene“) ili izmene polja, zatvaranje forme tiho gubi izmene | `FormClosing` sa Sačuvaj/Odbaci/Otkaži | Zatvaranje posle grupne promene pita |
| RNI-05 | S | `frmTerminalPrivremeniExcel.cs:151-152, 188-191`; Designer `:112` | Red „UKUPNO“ ili napomena ispod tabele ne prolazi proveru dužine kontejnera; red ne može da se obriše u gridu, pa uvoz ostaje blokiran | Dozvoliti brisanje reda ili preskočiti redove sa praznom kolonom C | Fajl sa redom UKUPNO može da se uveze |
| RNI-06 | S | `frmTerminalPrivremeniExcel.cs:14, 57-58, 66, 69, 80` | Mapiranje samo po slovu kolone, prvi list, prvi popunjen red je zaglavlje bez provere. Fajl bez zaglavlja gubi prvi kontejner; pomerena kolona tiho upisuje BL, voz, L/R, plombu u pogrešna polja | Proveriti tekst zaglavlja za C/H/AC/E/AJ i upozoriti | Pomerene kolone daju upozorenje |
| RNI-07 | S | `RNI\frmScenarioSCI.cs:897, 911, 920, 930` | `ScenarioID == 9 && ScenarioID == 25` i `== 7 && == 23` nikad nisu tačni: Cut-off port se sakriva baš za 9 i 25, napomene pozicioniranja su uvek sakrivene, za 9/25 se menja cboADR na skrivenom panelu | `\|\|` umesto `&&` na četiri mesta | Scenario 9/25 prikazuje Cut-off port, 7/23 napomene |
| RNI-08 | S | `frmScenarioSCI.cs:529-530` | Za scenarije 9 i 25 polje Vozač prikazuje vozilo | `dr["Vozac"]` | Vozač i Vozilo različiti |
| RNI-09 | S | `frmScenarioSCI.cs:366-368, 389-419`; poziv `Uvoz\frmRadniNalogInterniPregled.cs:2163` | Za Uvoz/Terminal forma se otvara prazna bez poruke; konekcija i čitač se ne zatvaraju (ni za Izvoz) | `using`; poruka „Pregled postoji samo za izvoz“ | Posle 150 otvaranja ima slobodnih konekcija |
| RNI-10 | N | `frmTerminalPrivremeni.cs:496-500, 518, 1014` | Zakači slike/dokumenta i Log rade nad redom iz grida, Potvrdi i Obriši nad `txtID`. Posle Novi slike se kače na stari red | `IzabraniId()`, poruka za 0 | Posle Novi: „prvo sačuvajte zapis“ |
| RNI-11 | N | `frmTerminalPrivremeni.cs:1206-1216`; `TerminalPrivMaerskIzvoz.cs:51-53` (svih 9 izvoza) | Excel brodara se snimi, pa označavanje padne: poruka „Izvoz nije uspeo“, a fajl postoji; isti pokreti idu i u sledeći izveštaj | Posebna poruka „Fajl snimljen, zapisi NISU označeni“ | Poruka jasna kad baza padne posle snimanja |
| RNI-12 | N | `frmTerminalPrivremeniDokumenta.cs:75-77, 89-90`; `frmTerminalPrivremeniSlike.cs:95-97, 103-104` | Nedostupan mrežni folder izgleda kao „nema slika“; `Directory.Exists` na UNC na UI niti može da zamrzne formu | Proveriti osnovni folder u pozadini, poruka „nije dostupan“ | Bez mreže forma javlja i ne zamrzava se |
| RNI-13 | N | `frmTerminalPrivremeni.cs:595-598` | „Štampaj potvrdu“ je vidljivo, a javlja da štampa nije napravljena | Sakriti | Dugme se ne vidi |
| RNI-14 | N | `frmScenarioSCI.cs:1158, 1185` | Greška upita NHM/napomena se proguta; prazan grid | Poruka | Greška je vidljiva |

**Proveriti (RNI):**
- Grupna promena posle filtera: da li `Table.SelectedRecords` sadrži i označene redove koje filter sakriva (`frmTerminalPrivremeniPromeni.cs:95-99`).
- `UsedRange` sa formatiranim praznim kolonama može dati 16384 kolone ili 1048576 redova: greška u DataGridView ili dugo blokiranje.
- `frmScenarioSCI.cs:423`: `Convert.ToInt32(dr["Scenario"])` bez provere DBNull, Load nema try.
- `delTerminalPriv` ne briše neizvezene pokrete: pokreti obrisanog kontejnera idu brodaru.
- Posle Potvrdi promenu `UcitajPodatke` verovatno gubi filter i poziciju.
- Skripta `TerminalPriv_UvozExcel_BL_LR_2026_11.sql` mora biti puštena na produkciji, inače uvoz pada na @BL_UVOZ / @L_R. Komentari u skriptama su zastareli (AJ → PLOMBA_UVOZ).
- `sp_set_session_context` (arhiva, log) traži SQL Server 2016+.
- Lost update: `updTerminalPriv` prepisuje sve kolone, uključujući POSLATE SLIKE, vrednostima iz ranije učitanog grida.
- OOCL MAIL pri prvom pokretanju izvozi i istorijske zapise (ActiveMail = 0), ako posle skripte nije pušten UPDATE.
- SQL spajanje teksta iz Excela ili polja nije nađeno; sve ide kroz parametre.

## 8. Ručni test scenariji

Scenariji proveravaju osnovni tok i nalaze iz ovog izveštaja. Kolona „Nalaz“ kaže koji nalaz korak potvrđuje; posle ispravke isti korak služi kao provera.

### 8.1 Kapija

| # | Korak | Očekivano | Nalaz |
|---|---|---|---|
| 1 | Kapija → Novi dolazak → vozač, registracija, status → Sačuvaj bez „Novi“ | Nov ID, zapis u Listi SVIH naloga | KAP-01 |
| 2 | Ista forma → Novi → Obriši → Da | Poruka „Izaberite zapis“ | KAP-09 |
| 3 | Lista SVIH naloga → kamion bez odlaska → Otvori → promeni samo status → Sačuvaj | Datum odlaska ostaje prazan | KAP-02 |
| 4 | Vozač „D'Angelo“, razlog „kontejner 40' HC“ → Sačuvaj → otvori ponovo | Tekst sačuvan tačno | — |
| 5 | Lista SVIH naloga → sortiraj po Datumu dolaska | Hronološki, vidi se vreme | KAP-12 |
| 6 | Lista naloga u NAJAVI → Refresh kad nema naloga | Prazna lista, bez greške | — |
| 7 | Bez izabranog reda: Potvrdi dolazak / Potvrdi odlazak / Prijavi neslaganje | Poruka za svako dugme | KAP-03, KAP-13 |
| 8 | DOLAZAK red → Potvrdi dolazak dvaput za isti nalog | Jedan zapis u tabeli Kapija | Proveriti InsertKapija |
| 9 | Potvrdi dolazak na nalogu X → odmah Prijavi neslaganje → Refresh | Crven je X | KAP-04 |
| 10 | Izaberi ODLAZAK i DOLAZAK red, poslednji klik na ODLAZAK → Potvrdi odlazak | Obrađen samo ODLAZAK | KAP-03 |
| 11 | ODLAZAK scenario 69 → Potvrdi odlazak → Da | Prijemnica sa tim kontejnerom, jedan RN4 | KAP-05 |
| 12 | Isti tok sa „Ne“ → u frmPrijemVozaIzPlana OTVORI (VOZ / IZVOZ / UVOZ / TERMINAL) pre snimanja | Poruka, bez pada | KAP-07 |
| 13 | frmPrijemVozaIzPlana → Novi → Sačuvaj dvaput → OTVORI - GATE IN KAMION UVOZ | Jedna stavka, jedan RN4; otvara se ta prijemnica | KAP-06, KAP-11 |
| 14 | GATE OUT forma → Sačuvaj bez Novi; zatim Novi → Sačuvaj dvaput | Poruka; jedna stavka, jedan RN6 | KAP-08 |
| 15 | U frmPrijemVozaIzPlana i GATE OUT formi obriši ili ukucaj slovo u NalogID | Polje zaključano ili bez pada | KAP-10 |
| 16 | Prijava kao druga firma (npr. TA.txt) → Kapija → Novi dolazak | Dugmad su vidljiva i rade | Proveriti (ostale firme) |

### 8.2 Carinsko skladište: Nalog za PRIJEM → Prijemnica → Potvrda

| # | Korak | Očekivano | Nalaz |
|---|---|---|---|
| 1 | Baza bez skripte: Carinsko skladište → Prijem robe → nalog → Prijemnica | Otvara se, nema dugmadi Uparivanje, Dodeli palete, Potvrdi; Snimi radi po starom | — |
| 2 | Pusti `Carina_PrijemnicaPL_2026_11.sql` dva puta | Bez greške | — |
| 3 | Nalog za PRIJEM: dodaj 2 naimenovanja sa NHM od 7 cifara → Snimi | Upisano, TOTAL tačan | — |
| 4 | Izmeni staro naimenovanje sa NHM < 1.000.000 | Izmena prolazi | SKC-05 |
| 5 | Dvoklik na A → Izmeni → odmah Obriši | Poruka ili potvrda; A ostaje | SKC-02 |
| 6 | Obriši tekst Nalogodavca → Snimi | Poruka, bez pada | SKC-09 |
| 7 | Zatvori grupu „Magacinski predmet“ → izaberi drugi magacinski broj → otvori grupu | Novi broj u polju | SKC-17 |
| 8 | Komercijalni nalog: otvori Nalog za PRIJEM | Lista magacinskih brojeva je puna | SKC-18 |
| 9 | Nova Prijemnica → Snimi zaglavlje | Broj prijemnice, status OD | — |
| 10 | Uvezi packing listu (DEUS), izaberi katalog; ponovi isti fajl | Redovi sa statusima; ponovljen fajl odbijen | — |
| 11 | JM po koletu za nov artikal → Formiraj (iz liste i iz stabla, posle Uvoza artikala) | Artikli formirani; nov nivo može da se izabere | SKC-12 |
| 12 | Postojeći artikal sa koeficijentom, u fajlu „2/5“ | Upozorenje, prenos dozvoljen | SKC-11 |
| 13 | Prenesi u stavke → Uparivanje: dodeli naimenovanje, koleta 100→80 | Količina JM, neto, bruto, vrednost srazmerno manji; „Delimično“ | — |
| 14 | Ista promena koleta u glavnoj tabeli | Isti preračun | SKC-04 |
| 15 | NHM u traci → klik na ćeliju (ne zaglavlje) → Dodaj; zatim Dodaj bez izbora | NHM tog reda; bez izbora stavka nepromenjena | SKC-03 |
| 16 | NHM pretraga „d'o“, artikal „O'Neil“, filter kontejnera sa apostrofom | Bez pada | SKC-07 |
| 17 | Koleta veće od carine → Snimi | Snimi odbija (Prekoračeno) | — |
| 18 | Snimi → zatvori → otvori | Isti ID stavki, PL Preneto | — |
| 19 | Izazovi grešku pri Snimi (npr. obriši stavku na paleti sa poljem) | Tabela ostaje kakva je bila | SKC-10 |
| 20 | Dodeli palete: 130 koleta, 60 po paleti → Spoji dve | 3 palete od 13 cifara (60/60/10); posle spajanja nova paleta | — |
| 21 | `Carina.Polja.ObaveznoZaPotvrdu = 1` → Potvrdi bez polja → Dodeli polje → Potvrdi | Prvo odbijeno; posle polja ZA, promet, Pozicija = polje | — |
| 22 | Dva prozora iste prijemnice: Potvrdi u B, Snimi u A | Status ostaje ZA, A dobija poruku | SKC-01 |
| 23 | ZA prijemnica: korisnik bez prava menja stavku → Snimi; zatim korisnik sa pravom | Prvi odbijen; drugi upisuje korekciju | — |
| 24 | Prijemnica bez stavki: Potvrdi, Dodeli palete; Nalog rukovaocima na nesnimljenoj | Poruke, bez pada | SKC-08 |
| 25 | Radni nalozi (ViljuskaristiPregled): klik na red rukovaoca; klik na zaglavlje bez izbora | Usluge iz tog naloga; bez pada | SKC-06 |

### 8.3 Komercijalno skladište: nalog → prijem → otprema → otpremnica

| # | Korak | Očekivano | Nalaz |
|---|---|---|---|
| 1 | Komercijalni nalozi → SKLADIŠNI NALOZI IZVOZ → Potvrdi bez klika na red | Poruka „Obeležite uslugu“ | — |
| 2 | Red sa uslugom skladištenja, kontejner bez carinskog postupka → Potvrdi | Jedan RN, VrstaRN = Komercijalno | SKK-07 |
| 3 | Ponovo Potvrdi na istom redu | „Već je generisan RN“ sa tačnim brojem | SKK-08 |
| 4 | Refresh → Potvrdi bez novog klika | Poruka, bez pada | SKK-08 |
| 5 | SKLADIŠNI NALOZI UVOZ → red → Otvori, Pregledaj podatke | Nalog i podaci uvoznog kontejnera | SKK-06 |
| 6 | Komercijalno skladište → Prijem robe na praznoj listi → Prijemnica | Poruka | SKK-12 |
| 7 | Prijem 10 koleta / 500 kg u magacinski broj X, status ZA | U „IZABERI SA LAGERA“ za X: 10 / 500 | — |
| 8 | Kreiraj novi → Komercijalno → Otprema, napomena „Isporuka za O'Brien“ → IDI NA OTPREMNICU pre snimanja | Poruka „prvo sačuvajte“ | SKK-12 |
| 9 | SNIMI | „RADNI NALOG KREIRAN“, txtID popunjen, apostrof sačuvan | — |
| 10 | IZABERI SA LAGERA → Dodaj bez izbora; zatim Koleta 11 → Izmeni | Poruka; 11 odbijeno | — |
| 11 | Koleta 4 / 200 kg → Izmeni → Kreiraj → SNIMI → zatvori i otvori otpremu | 4 / 200; napomena i usluge nepromenjene | SKK-03 |
| 12 | Druga otprema za X → lager | Raspoloživo 6 / 300 | SKK-02 |
| 13 | Stavka sa 0 koleta i bruto > 0 → Koleta 5 | Odbijeno | SKK-01 |
| 14 | Promeni naziv artikla u panelu → Izmeni → SNIMI → druga otprema za X | Naziv zaključan ili lager i dalje umanjen | SKK-02 |
| 15 | Otpremnica: Snimi → izmeni destinaciju → Snimi | Izmenjen baš taj zapis; X umanjen jednom | SKK-04 |
| 16 | Interni poslovi → popuni → SNIMI jednom | Jedan nalog, poruka o uspehu | SKK-05 |
| 17 | Otpremnica → Dodaj (usluge), Nazad u panelu rukovaoca | Usluga dodata; panel se zatvara | SKK-15 |

### 8.4 Izvoz: porudžbenica → nalog za otpremu → plan → usluge

| # | Korak | Očekivano | Nalaz |
|---|---|---|---|
| 1 | Porudžbenica → Kreiraj novi: Opis „D'Amico 40' HC“, Booking „MSCU1234567“, Brodar obrisan, 2 stavke, Kvalitet prazan → Sačuvaj | Traži Brodara i Kvalitet; posle dopune snima; jedna poruka | IZV-01 |
| 2 | Izmeni postojeći → Otvori bez reda, pa sa redom | Poruka; otvara izabranu porudžbenicu | IZV-18 |
| 3 | Formiranje naloga → Kreiraj novi → klik na zaglavlje kolone i filter red | Ne pokreće se pitanje „Drumski…“ | Proveriti |
| 4 | Klik na stavku → Drumski Da → ADR X | Drumski u bazi nepromenjen | IZV-02 |
| 5 | Ponovo → izbor scenarija zatvoren sa X | Grupni unos se ne otvara | IZV-02 |
| 6 | Scenario 13: Dodaj usluge pre Sačuvaj; Količina = naručeno + 1 | Poruka; količina odbijena | IZV-07, IZV-02 |
| 7 | Količina 2, Booking „MSCU1234567“, Ref „INV-12“ → Sačuvaj → zatvori → Izmeni postojeći | Booking, Ref i broj porudžbenice tačni | IZV-03, IZV-04 |
| 8 | U tabeli oba reda isti broj kontejnera; Koleta „2,5“, izlazak iz reda | Upozorenje na isti kontejner; poruka, bez pada | IZV-06 |
| 9 | Carinski postupak: prvi u listi, carinarnice prazne → Sačuvaj | Snimljeno ili validacija, bez pada | IZV-08 |
| 10 | Formiraj KN I Faza kad drugom kontejneru fali broj; zatim sa praznim gridom | Ništa nije preneto; prazan grid ne pravi RN | IZV-05 |
| 11 | Dodaj usluge za tip „45G1“ → S N I M I → zatvori → otvori → Kopiraj usluge | Bez greške; jedna železnina; lista ciljnih kontejnera puna | IZV-07, IZV-13 |
| 12 | Cena usluge „12,5“; zatim obriši cenu | Snimljeno 12,5; prazna cena daje poruku | IZV-13 |
| 13 | Pusti drumsku uslugu → Formiranje naloga dvaput | Jedan RN, grid osvežen | IZV-14 |
| 14 | Formiranje plana iz uvoza → Prenesi selektovano dvaput | Kontejner jednom u planu, jedan RN | IZV-12 |
| 15 | Plan: Sačuvaj bez izabranog kontejnera; zatim izaberi → Sačuvaj bez izmena | Poruka; ETA, plan utovara, vrednost robe, kontakt špeditera, Dobijen nalog nepromenjeni u bazi | IZV-09, IZV-10 |
| 16 | Tabela plana: označi 2 reda → Napomena za robu „40' HC“, Cut off port | Oba reda izmenjena, bez SqlException | IZV-11 |
| 17 | Dokumenta: dokument na tabu Kontejner, pa Izbriši na tabu Buking | Dokument kontejnera ostaje | IZV-16 |
| 18 | Vaganje za kontejner X → Sačuvaj; Tara > Bruto | Vaganje na X; poruka, bez pada | IZV-15 |
| 19 | Prijava kao TA (TA.txt) → otvori Otpremu kamionom (GATE OUT) | Forma se otvara, aplikacija radi | IZV-17 |
| 20 | Korisnik sa samo pravom „Otkaži postojeći“ → Logistika izvoza | Može samo da otkaže | IZV-18 |

### 8.5 RNI: Terminal privremeni

| # | Korak | Očekivano | Nalaz |
|---|---|---|---|
| 1 | Otvori Terminal privremeni → Zakači slike, Pregled log, Grupna promena bez izabranog reda | Poruka, bez pada | — |
| 2 | Uvoz Excel: zaglavlje u 1. redu, 3 kontejnera → Učitaj | „Upisano 3“; H→BL/UVOZ, E→L/R, AC→VOZ/kamion, AJ→PLOMBA_UVOZ; log INSERT | — |
| 3 | Isti fajl ponovo | Upozorenje, bez upisa | RNI-01 |
| 4 | Fajl sa istim kontejnerom u dva reda | Oba reda prijavljena | RNI-01 |
| 5 | Fajl sa kolonama samo do K | Bez pada, L…AJ prazni | — |
| 6 | Prazan red u sredini i red „UKUPNO“ na dnu | Prazan preskočen; UKUPNO se briše ili preskače | RNI-05 |
| 7 | Fajl bez zaglavlja; fajl sa pomerenom kolonom | Upozorenje | RNI-06 |
| 8 | UVOZNIK „O'Neil d.o.o.“, VRSTA „40'HC“, BRODAR „maersk“ | Upisano tačno, „MAERSK“ | — |
| 9 | Zapis sa datumom → zapis bez datuma → štikliraj GATE_IN_E/F | Trenutni datum | RNI-03 |
| 10 | MAERSK zapis: GATE IN F, BL, datum → Potvrdi promenu; ponovo Potvrdi | Jedan pokret Maersk; log staro → novo | — |
| 11 | Filter BRODAR = MSC → Ctrl+klik 3 reda → Grupna promena STANJE = DOBAR → Sačuvaj izmene | Promenjeno 3; redovi van filtera isti | Proveriti (filter) |
| 12 | Grupna promena → zatvori formu bez čuvanja | Pitanje o izmenama | RNI-04 |
| 13 | Novi → Zakači slike | „Prvo sačuvajte zapis“ | RNI-10 |
| 14 | Zakači slike: 2 jpg + 1 pdf; Zakači dokumenta isti fajl dvaput | „Preskočeno 1“, POSLATE SLIKE = 2; drugi dokument „ (2)“ | — |
| 15 | Mrežni folder nedostupan → Slike, Dokumenta, dodavanje | „Nije dostupan“, forma se ne zamrzava | RNI-12 |
| 16 | Arhiviraj | Zapisi sa Konačni GATE OUT prelaze u arhivu sa istim ID, slikama i dokumentima; log ARHIVA | — |
| 17 | Dva korisnika: B arhivira, A menja isti zapis → Potvrdi | A dobija grešku, bez novih pokreta | RNI-02 |
| 18 | Scenario SCI za nalog scenarija 9, 25, 7, 23 | 9/25: Cut-off port, ADR4 (samo 25), Vozač ≠ Vozilo; 7/23: napomene | RNI-07, RNI-08 |

## 9. Sledeći koraci

1. Ispraviti nalaze iz 2.1 (kod iz operativne faze). To su fajlovi Prijemnica*.cs, frmNalogPrijem.Ispravke.cs i skripta `Carina_PrijemnicaPL_2026_11.sql`.
2. Za nalaze u postojećem kodu (2.2 i ostali): odobrenje po fajlu, jer pravilo projekta zabranjuje izmenu postojećeg koda bez izričitog zahteva. Svaka ispravka mora da radi za sve firme.
3. Za stavke „Proveriti“: pregled procedura u bazi (PotvrdiPrijem, InsertKapija, InsertRadniNalogSkladista, InsertRNCarinskoSkladisteOtpremnica, InsertRnCarinskoSkladisteOtpremaStavkePom).
4. Ručne scenarije iz odeljka 8 proći na test bazi. Korak koji ne daje „Očekivano“ upisati kao potvrđen nalaz.
5. SKC-05 traži odluku: da li kolona NHM naimenovanja čuva ID iz tabele NHM ili tarifni broj od 7 cifara.

## 10. Ispravljeno (25.11.2026)

Ispravke su urađene na zahtev, a build prolazi. Nisu proverene u aplikaciji: proveravaju se koracima iz odeljka 8 navedenim u koloni „Provera“.

| Oznaka | Šta je promenjeno | Provera |
|---|---|---|
| IZV-17 | `Izvoz\frmOtpremaKontejneraKamionomIzKontejnera.cs`: uklonjen poziv `ChangeTextBox()` iz same sebe (grana za firme koje nisu Leget) | 8.4 korak 19 |
| IZV-15 | `Izvoz\Vaganje.cs`: tabela prikazuje samo vaganja kontejnera za koji je forma otvorena; neto se računa bez pada (tara veća od bruto daje 0); Sačuvaj odbija taru veću od bruto. `Izvoz\VaganjePregled.cs`: u prikazu „Izvagani kontejneri“ Otvori šalje KontID | 8.4 korak 18 |
| IZV-09 | `Izvoz\frmIzvozKonacna.cs` (`VratiPodatkeSelect`): dodat zarez posle KontaktSpeditera; učitavaju se ETA Leget, Planirani datum utovara, Vrednost robe, Kontakt špeditera, TaraZ i Dobijen nalog (iz `DobijenNalogKlijent1`); oznaka Terminal se čita posebnim upitom samo ako kolona postoji | 8.4 korak 15 |
| SKK-04 | `Skladista main\Dokumenta\Otpremnica.cs`: izmena šalje ID otpremnice (`txtOtpremnica`), a ne broj radnog naloga | 8.3 korak 15 |
| SKK-05 | `InterniPoslovi.cs`, `InterniPrenos.cs`, `VanredniPoslovi.cs`: poziv `InsertRNInterni`/`UpdateRNInterni` isključen kao u `Otprema.cs` (uvek je padao, pa nikad ništa nije upisao u bazu) | 8.3 korak 16 |
| SKK-07 | `Skladista main\PregledKomercijalnihNaloga.cs`: Potvrdi upisuje VrstaRN iz izračunatog tipa (Komercijalno/Carinsko); šifra skladišta 1008 samo za carinsko | 8.3 korak 2 |
| KAP-01 | `Kapija\frmKamionDetalji.cs`: Sačuvaj bez ID-ja upisuje nov dolazak; posle neuspelog upisa forma ostaje u režimu novog | 8.1 korak 1 |
| KAP-03 | `Kapija\frmKapijaOtvoreniNalozi.cs`: smer se čita iz svakog izabranog reda; nalozi koji nisu ODLAZAK se preskaču uz poruku; bez izbora poruka „Izaberite nalog za ODLAZAK“ | 8.1 koraci 7 i 10 |
| RNI-01 | `RNI\frmTerminalPrivremeniExcel.cs`, `RNI\insertTerminalPriv.cs` (`AktivniKontejneri`): kontejner koji je već u TerminalPriv bez Konačnog GATE OUT se preskače i navodi u izveštaju; isti kontejner dva puta u fajlu je greška (navode se oba reda) | 8.5 koraci 3 i 4 |
| RNI-07 | `RNI\frmScenarioSCI.cs`: `\|\|` umesto `&&` na četiri mesta; Cut-off port se sakriva za sve scenarije osim 9 i 25 | 8.5 korak 18 |

**Ista greška kao IZV-17, nije ispravljena (čeka odobrenje):** `ChangeTextBox()` poziva samu sebe u grani za firme koje nisu Leget i u ovim formama:
- `Dokumenta\frmPregledOtpreme.cs:123`
- `Dokumenta\frmPrijemKontejneraKamionPregled.cs:124`
- `Dokumenta\frmPrijemVozomPregled.cs:122`
- `RadniNalozi\frmDodelaSkladista.cs:116`
- `RadniNalozi\RN1PrijemVoza.cs:118`
- `RadniNalozi\RN5PrijemPlatforme2.cs:111`
- `RadniNalozi\RN6OtpremaPlatforme.cs:109`
- `RadniNalozi\RN7OtpremaPlatforme2.cs:109`
- `RadniNalozi\RN8OtpremaCirade.cs:110`
- `RadniNalozi\RN9PrijemCirade.cs:109`
- `Uvoz\frmFormiranjeRobnihDokumenata.cs:111`

### 10.1 Ispravke u kodu iz operativne faze (25.11.2026)

| Oznaka | Šta je promenjeno | Provera |
|---|---|---|
| SKC-01 | `Prijemnica.cs` (Snimi) i `Prijemnica.Potvrda.cs` (`StatusNepromenjenUBazi`): pre snimanja status se čita iz baze; ako se razlikuje od statusa pri otvaranju (npr. potvrđena u drugom prozoru), ništa se ne snima, stavke se ponovo učitavaju i korisnik dobija poruku | 8.2 korak 22 |
| SKC-04 | `Prijemnica.Raspored.cs` (`GridStavke_RecordContextChange`): kad se u tabeli stavki promeni koleta, poziva se `PromeniKoleta` kao u Uparivanju (Količina JM po koeficijentu; neto, bruto i vrednost srazmerno packing listi). Ručno unet neto, bruto i vrednost se ne prepisuju dok se koleta ne promeni | 8.2 korak 14 |
| SKC-05 | `frmNalogPrijem.Ispravke.cs` (`NHMIspravan`, `ZapamtiUcitaniNHM`), `frmNalogPrijem.cs` (Izmeni): izmena postojećeg naimenovanja prolazi dok se NHM ne menja; nov ili promenjen NHM i dalje mora imati 7 cifara | 8.2 korak 4 |
| SKC-18 | `frmNalogPrijem.Ispravke.cs` (`JeKomercijalno`, `PostojiTabelaKomercijalnihBrojeva`), `frmNalogPrijem.cs` (`FillMagacinskiBroj`): komercijalni nalog se prepoznaje po „Komercijalno“; lista se puni iz `MagacinskiBrojKomercijalni` samo ako ta tabela postoji (u kodu je nigde drugde nema). ISP-12: `btnNoviMP_Click` nije vezan ni za jedno dugme, a magacinski predmet upisuje procedura `UpdateRadniNalogDirektni` (nije u repozitorijumu), pa se uslov upis/izmena proverava u njoj | 8.2 korak 8 |

**Ostaje otvoreno:**
- SKC-05, drugi deo: `sp_Prijem_Kontrola` spaja NHM naimenovanja sa `NHM.ID`, a Prijemnica u NHM stavke upisuje `NHM.ID` pri izboru iz liste, a broj iz naimenovanja pri uparivanju. Ovo se rešava zajedno sa izmenom „tarifni broj kao tekst do 10 cifara“ (Magacinski predmet, čeka rezultat `NalogPrijem_MP_Provera_2026_11.sql`).
- SKC-18: dugme Sačuvaj na panelu magacinskog broja (`btnMbSave_Click`) upisuje samo carinski broj; komercijalni nalog može da izabere postojeći broj, ali ne i da napravi nov.
- SKC-01: dva prozora sa istim statusom (oba OD) i dalje mogu da prepišu stavke jedan drugom; snima se ono što je poslednje sačuvano.

### 10.2 Paket A (26.11.2026)

| Oznaka | Šta je promenjeno | Provera |
|---|---|---|
| IZV-17 (još 11 formi) | Uklonjen poziv `ChangeTextBox()` iz same sebe u granama za firme koje nisu Leget: frmPregledOtpreme, frmPrijemKontejneraKamionPregled, frmPrijemVozomPregled, frmDodelaSkladista, RN1PrijemVoza, RN5PrijemPlatforme2, RN6OtpremaPlatforme, RN7OtpremaPlatforme2, RN8OtpremaCirade, RN9PrijemCirade, frmFormiranjeRobnihDokumenata. Ponovna pretraga celog koda ne nalazi više nijednu | Prijava kao TA, otvoriti svaku formu |
| KAP-06 (+ KAP-07) | `Uvoz\frmPrijemVozaIzPlana.cs`: „GATE IN KAMION UVOZ“ zove konstruktor `(šifra, korisnik, vozom)`; sva četiri dugmeta OTVORI bez formirane prijemnice daju poruku | 8.1 koraci 12 i 13 |
| RNI-08 | `RNI\frmScenarioSCI.cs`: Vozač (scenariji 9 i 25) iz kolone Vozac | 8.5 korak 18 |
| SKC-16 | `frmNalogPrijem.cs`: LOPotvrdjen se upisuje u polje forme | Komercijalni nalog sa LO = 1 ne nudi LO potvrdu i Obriši |
| SKC-02 | `frmNalogPrijem.cs`, `frmNalogPrijem.Ispravke.cs` (`NaimenovanjeKoristePrijemnice`): ID naimenovanja se briše sa poljima; Izmeni bez izabranog naimenovanja daje poruku; Obriši pita za potvrdu i odbija naimenovanje koje koriste stavke prijemnice | 8.2 korak 5 |
| SKC-03 | `Dokumenta\Prijemnica.cs` (`btnNHMDodaj_Click`): uzima izabrani red ili red sa kursorom; bez izbora poruka, panel ostaje otvoren i stavka se ne menja | 8.2 korak 15 |
| SKC-07 | `Dokumenta\Prijemnica.cs` (`NHMKod`, `NHMNaziv`): tekst pretrage je parametar; `%`, `_` i `[` se traže kao obični znaci | 8.2 korak 16 |
| IZV-16 | `Izvoz\frmIzvozDokumenta.cs`: Izbriši na svakom tabu briše dokument izabran u svojoj tabeli, uz potvrdu; izbor bukinga upisuje putanju bukinga | 8.4 korak 17 |
| IZV-04 | `Izvoz\frmGrupniUnosPoljaIzvoz.cs` (`VratiPodatkeGrupeSelect`): porudžbenica se nalazi preko stavke (`ProdajniNalogIzvozStavke`), LEFT JOIN, pa se podaci kontejnera učitavaju i kad stavke nema | 8.4 korak 7 |
| SKK-06 | `Skladista main\PregledKomercijalnihNaloga.cs`: za listu Uvoz Otvori, Potvrdi i Pregledaj podatke daju poruku „radi samo za izvoz“; dugme Potvrdi se ne prikazuje za uvoz. Ove radnje su napravljene samo nad `IzvozKonacna`; uvozna verzija je nova funkcija | 8.3 korak 5 |

### 10.3 Paket B, Kapija i Izvoz (26.11.2026)

| Oznaka | Šta je promenjeno | Provera |
|---|---|---|
| KAP-08 | `Izvoz\frmOtpremaKontejneraKamionomIzKontejnera.cs`: bez zaglavlja otpreme (izvoz/terminal) poruka „Kliknite Novi“; drugi klik na Sačuvaj ne prenosi isti kontejner ponovo | 8.1 korak 14 |
| KAP-11 | `Uvoz\frmPrijemVozaIzPlana.cs`: Sačuvaj bez prijemnice daje poruku; drugi klik ne prenosi isti nalog ponovo; provera postojećeg RN4 važi i kad ih ima više | 8.1 korak 13 |
| KAP-04 (+ KAP-13) | `Kapija\frmKapijaOtvoreniNalozi.cs`: Prijavi neslaganje uzima nalog iz izabranog reda i osvežava listu; Potvrdi dolazak bez DOLAZAK reda daje poruku | 8.1 koraci 7 i 9 |
| KAP-02 | `Kapija\frmKamionDetalji.cs`: datumi imaju štiklir; prazan datum iz baze ostaje prazan; nov dolazak nema datum odlaska | 8.1 korak 3 |
| IZV-12 | `Izvoz\frmFormiranjePlanaIzvoz.cs`: ID novog kontejnera iz `SCOPE_IDENTITY()`; neuspeo upis prekida prenos; nalog iz uvoza se prenosi jednom; korisnik i datum kao parametri | 8.4 korak 14 |
| IZV-14 | `Izvoz\frmIzvozPregledKontejneraDrumskeUsluge.cs`: osvežavanje posle formiranja naloga koristi `IzvozKonacna.ID`; postojeća provera NalogID tada sprečava drugi nalog | 8.4 korak 13 |
| IZV-11 | `Izvoz\frmIzvozKonacnaTable.cs`: svaka opcija ima kolonu i vrednost kao parametar; ispravljene kolone (KontaktSpeditera, Spedicija, VrstaBrodskePlombe, Inspekcija); Adresa utovara daje poruku; više označenih redova; poruka sa brojem izmenjenih | 8.4 korak 16 |
| IZV-07 | `Izvoz\frmGrupniUnosPoljaIzvoz.cs`, `frmIzvozKonacna.cs`: tip kontejnera u upitima za železninu i repoziciju je parametar (radi i za 20DV, 45G1); Dodaj usluge pre snimanja daje poruku | 8.4 koraci 6 i 11 |
| IZV-13 | `Izvoz\frmIzvozUnosManipulacije.cs`: postojeća železnina se traži u izvoznim tabelama (ne ubacuje se ponovo); cena kao parametar; prazna ili neispravna cena daje poruku. `frmIzvozKopiranjeUslugaKontejnera1.cs`: tabela Izvoz umesto nepostojeće „Izvoza“ | 8.4 koraci 11 i 12 |
| IZV-05 | `Izvoz\frmGrupniUnosPoljaIzvoz.cs` (Formiraj KN I Faza): provera po svakom kontejneru (ne TOP 1 grupe), svi se proveravaju pre ijednog prenosa, poruka navodi kontejner, zastavice se vraćaju na svakom kliku, pitanje o bescarinskom postupku jednom, bez kontejnera nema radnih naloga | 8.4 korak 10 |

**Ostaje otvoreno iz paketa B:**
- Kopiranje usluga (`frmIzvozKopiranjeUslugaKontejnera1`): tabela ciljnih kontejnera se nigde ne puni. Treba odluka koje kontejnere ponuditi (isti plan, ista porudžbenica).
- Skladište (SKK-01, SKK-03, SKC-06, SKC-10, SKC-11, SKC-12, SKC-17) čeka da druga sesija završi izmene tarifnog broja u istim fajlovima.
- Zaštita od drugog klika (KAP-08, KAP-11, IZV-12) važi dok je forma otvorena; provera u bazi traži procedure kojih nema u repozitorijumu.

### 10.4 Paket B, Skladište (26.11.2026)

| Oznaka | Šta je promenjeno | Provera |
|---|---|---|
| SKK-01 | `Skladista main\OtpremaLager.cs`, `Dokumenta\Otprema.cs`: koleta veća od raspoloživih se odbija i kad je raspoloživo 0. Za bruto, neto i vrednost uslov „> 0“ ostaje, jer 0 tamo znači i da vrednost u prijemnici nije uneta | 8.3 korak 13 |
| SKK-03 | `Dokumenta\Otprema.cs`, `Dokumenta\Otpremnica.cs` (VratiPodatke): sačuvana otprema i otpremnica zadržavaju svoje podatke i usluge; iz naloga prijema uzima se samo veza sa prijemnicom | 8.3 korak 11 |
| SKC-06 | `Skladista main\ViljuskaristiPregled.cs`: usluge se prikazuju za nalog skladišta za koji je forma otvorena; klik na zaglavlje i „Skladištar potvrdio“ bez izbora ne obaraju formu | 8.2 korak 25 |
| SKC-10 | `Dokumenta\Prijemnica.UvozPL.cs` (SnimiStavkeBezBrisanja): posle greške pri snimanju stavke se ne čitaju iz baze; vraćaju se samo ID-jevi iz neuspelog pokušaja, a izmene ostaju u tabeli | 8.2 korak 19 |
| SKC-11 | `Carina_PrijemnicaPL_2026_11.sql` (sp_PL_Analiziraj): neispravan koeficijent iz fajla je Greška samo za nov artikal; postojeći artikal dobija Upozorenje i prenosi se. **Skriptu treba ponovo pustiti** | 8.2 korak 12 |
| SKC-12 | `Dokumenta\Prijemnica.UvozPL.cs` (UcitajKlasifikacije): lista klasifikacija se puni iznova posle Pregleda klasifikacije i posle dvoklika na nivo | 8.2 korak 11 |
| SKC-17 | `Skladista main\frmNalogPrijem.cs` (cboMagacinskiBroj_Leave): magacinski broj se upisuje i kad je grupa „Magacinski predmet“ zatvorena | 8.2 korak 7 |
