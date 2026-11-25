# Leget — stari i novi izgled: forme, tabele, funkcionalnosti i šifarnici

Statička analiza koda iz `D:\Cloude\LG\SaobracajMTA` (trenutno stanje, firma **Leget**). U projektu ništa nije menjano. Isti metod dostupnosti formi kao `tools/analiza-formi` (samo izvršni kod, Leget grane, bez sakrivenih kontrola), pokrenut posebno od starog menija `MainP` i od novog `NewMain`.

## Sadržaj

1. Ukratko
2. Prijava na Leget
3. Šifarnici potrebni novom izgledu (šta treba preneti iz starog)
4. Stari izgled (MainP): meni i tabele
5. Novi izgled (NewMain): meni i tabele
6. Poređenje formi
7. Tabele: novi i stari izgled
8. Forme — detalji
9. Metod i ograničenja

## 1. Ukratko

| | Novi izgled (NewMain) | Stari izgled (MainP) |
|---|---|---|
| Dostupnih formi | 232 | 432 |
| Od toga samo u tom izgledu | 118 | 318 |
| Zajedničkih formi | 114 | 114 |
| Tabela u SQL-u formi | 298 | 349 |
| Tabela u koje se upisuje | 102 | 204 |

- Tabela koje forme novog izgleda koriste kao izvor za combobox: **78**
  - forma za unos/izmenu postoji i u novom izgledu: **22**
  - **forma za unos/izmenu postoji samo u starom izgledu: 33** (odeljak 3.2 — ovo treba preneti u novi izgled)
  - ni u jednom izgledu nije pronađena forma koja ih puni: **23** (odeljak 3.3)

## 2. Prijava na Leget

1. `Program.cs` u folderu programa traži `.txt` fajl; `Leget.txt` bira firmu Leget. `ConfigManager.GetCompanyConfiguration("Leget")` (`Config.cs`) daje konekciju na bazu, mrežni folder `Dokumenta` i podatke firme (naziv, adresa, MB, e-mail, osnovno skladište i lokacija), koji se upisuju u statička polja `frmLogovanje`.
2. Otvara se `Sifarnici/frmLogovanjeSecond` (prozor „Logovanje“, naslov „Prijava na Leget plus nalog“).

**Funkcionalnosti forme za prijavu**

| Kontrola | Šta radi | Tabele |
|---|---|---|
| otvaranje forme (`FillCombo`) | puni listu korisnika | `Korisnici` (S) |
| izbor korisnika + lozinka, dugme „POKRENI“ (`button1_Click`) | proverava `Korisnik` + `Password`; postavlja `frmLogovanje.user` | `Korisnici` (S) |
| polje „Stari izgled“ (`checkBox1`) | ako je uključeno, „POKRENI“ otvara `MainP(korisnik, 1)` | vidi odeljak 4 |
| polje „Novi izgled“ (`checkBox2`, podrazumevano uključeno) | ako je uključeno, „POKRENI“ otvara `NewMain(korisnik)` | vidi odeljak 5 |
| oba polja uključena | otvaraju se oba glavna prozora | |
| klik na naslov (`label1_MouseClick`) | skrivena prečica, i pre prijave: `Administracija/frmSistemskaTabela` | `SistemskePostavkeHeader` (S) |
| dugme „ZATVORI“ (`sfButton1_Click`) | zatvara prijavu | |

Napomena: lozinka se proverava upitom sastavljenim nadovezivanjem teksta (`"... where Rtrim(Password) = '" + txtPassword.Text + "' and ..."`), lozinka se čuva u tabeli `Korisnici` kao običan tekst. To je bezbednosni rizik (SQL injection), nevezano za izgled.

Šta korisnik zaista vidi u oba izgleda zavisi i od prava u bazi: novi izgled čita `MainNovi`, `MainNoviPrava`, `mainNoviKartice`; stari izgled ima svoje provere po korisniku u `MainP`. Analiza pokazuje šta kod dozvoljava, ne prava pojedinačnog korisnika.

## 3. Šifarnici potrebni novom izgledu

### 3.1 Kako je rađeno

- Za svaku formu novog izgleda nađeni su combobox-ovi (tip kontrole sadrži `Combo`/`DropDown`, ili ime `cbo…`/`cb…`) kojima se dodeljuje `DataSource`, i tabele iz `SELECT` upita koji ih pune.
- Za svaku takvu tabelu tražena je forma koja u nju upisuje: direktno (`INSERT`/`UPDATE`/`DELETE`) ili preko procedure čije ime je `Insert…`/`Update…`/`Delete…` + ime tabele (npr. `InsertTipKontenjera` → `TipKontenjera`).
- Ako takva forma postoji među formama novog izgleda, šifarnik je pokriven. Ako postoji samo u starom izgledu, šifarnik treba preneti.
- Tabele čija imena su na slovenačkom (`Partnerji`, `MerskeEnote`, `Delavci`, `Postaje`…) potiču iz Pantheon ERP-a i verovatno se pune sinhronizacijom, ne ručno; označene su gde se pojave.

### 3.2 Forma za održavanje postoji samo u starom izgledu — treba ih preneti u novi

Poređano po broju formi novog izgleda koje ih koriste.

| # | Tabela | Forma za održavanje u starom izgledu i put do nje (MainP) | Koristi se u novom izgledu (forma: combobox) |
|---|---|---|---|
| 1 | `VrstaCarinskogPostupka` | **frmVrstaCarinskogPostupka** (`Uvoz/frmVrstaCarinskogPostupka.cs`): Terminal › Šifarnici › Vrsta Carinskog postupka → **frmVrstaCarinskogPostupka** | frmCarinskiPostupak (`cboCarinskiPUnutrasniTransport`), frmDrumski1 (`cboCarinskiPUnutrasniTransportI1`), frmIzvozKonacna (`cboReexport`), frmIzvozKonacnaTable (`cboOpsti`), frmOtpremaKontejneraLegetIZVOZ (`cboReexport`), frmPrijemKamionaDetalji (`cboCarinskiPUnutrasniTransport`) … (+14 formi) |
| 2 | `Carinarnice` | **Carinarnice** (`Uvoz/Carinarnice.cs`): Uvoz › Šifarnici › Carinarnice → **Carinarnice** | frmCarinskiPostupak (`cboOdredisnaCarinarnica, cboPolaznaCarinarnica`), frmDrumski (`cboOCarinarnica, cboPolaznaCarinarnica`), frmDrumski1 (`cboOCarinarnicaI1, cboPolaznaCarinarnicaI1`), frmIzvozKonacna (`cboCarina`), frmIzvozKonacnaTable (`cboOpsti`), frmKontaktOsobe (`cboCarinarnica`) … (+13 formi) |
| 3 | `MestaUtovara` | **frmMestaUtovara** (`Izvoz/frmMestaUtovara.cs`): Terminal › Šifarnici › Mesta utovara → **frmMestaUtovara** | frmDrumski (`cboMestoIstovara, cboMestoPreuzimanja, cboMestoUtovara`), frmDrumski1 (`cboMestoSpustanjaPunogI1`), frmIzvozKonacna (`cboMestoUtovara`), frmIzvozKonacnaTable (`cboOpsti`), frmKontaktOsobeMU (`txtPaKOSifra`), frmNalogZaPrevoz (`txtIstovarnoMesto, txtUtovarnoMesto`) … (+12 formi) |
| 4 | `VrstaVozila` | **frmVrstaVozila** (`Drumski/frmVrstaVozila.cs`): Drumski › Šifarnici › Vrsta vozila → **frmVrstaVozila**<br>**frmVrstaVozilaEdit** (`Drumski/frmVrstaVozilaEdit.cs`): Drumski › Šifarnici › Vrsta vozila → **frmVrstaVozila** → `button19` (Click) → **frmVrstaVozilaEdit** | frmAutomobilEdit (`cboTipVozila`), frmAutomobiliDrumski (`cboTipVozila`), frmDrumski (`cboTipTransporta`), frmDrumski1 (`cboTipTransportaI1`), frmPrijemKamionaDetalji (`cboVrstaKamiona`), frmRelacija (`cboVrstaKamiona`) … (+9 formi) |
| 5 | `VrstaManipulacije` | **frmVrstaManipulacije** (`TrackModal/Sifarnici/frmVrstaManipulacije.cs`): Terminal › Šifarnici › Vrste usluga → **frmVrstaManipulacije** | frmDodatneUsluge (`cboUsluga`), InterniPoslovi (`cboDodatneUsluge`), InterniPrenos (`cboDodatneUsluge`), Otprema (`cboDodatneUsluge`), Otpremnica (`cboDodatneUsluge`), Prijemnica (`cboDodatneUsluge`) … (+8 formi) |
| 6 | `VrstaRobeADR` | **frmVrstaRobeADR** (`Uvoz/frmVrstaRobeADR.cs`): Terminal › Šifarnici › Vrsta robe ADR → **frmVrstaRobeADR**<br>**frmVrstaRobeHS** (`Uvoz/frmVrstaRobeHS.cs`): Izvoz › Šifarnici › Vrsta robe HS → **frmVrstaRobeHS** | frmDrumski1 (`cboADRI1`), frmGrupniUnosPoljaIzvoz (`cboADR`), frmIzvozKonacna (`txtADR`), frmNalogZaPrevoz (`cboVrstaRobeKomerijala`), frmOtpremaKontejneraIzvozKamion (`txtADR`), frmOtpremaKontejneraLegetIZVOZ (`txtADR`) … (+7 formi) |
| 7 | `NHM` | **frmNHM** (`Sifarnici/frmNHM.cs`): Osnovni podaci › Prevozi › NHM → **frmNHM** | frmDrumski1 (`cboVrstaRobeI1`), frmGrupniUnosPoljaIzvoz (`cboVrstaRobe`), frmIzvozKonacna (`cboNHM`), frmLagerOperater (`cboVrstaRobe`), frmOtpremaKontejnera (`cboVrstaRobe`), frmUvozKonacna (`cboNHM`) … (+6 formi) |
| 8 | `InspekciskiTretman` | **frmInspekciskiTretman** (`Izvoz/frmInspekciskiTretman.cs`): Terminal › Šifarnici › Inspekciski tretman → **frmInspekciskiTretman** | frmGrupniUnosPoljaIzvoz (`cboInspekciskiTretman`), frmIzvozKonacna (`cboInspekciskiTretman`), frmIzvozKonacnaTable (`cboOpsti`), frmOtpremaKontejneraIzvozKamion (`cboStatusKontejnera`), frmPrijemKamionaDetalji (`cboInspekciskiTretman`), frmPrijemKontejneraKamionLegetIzvoz (`cboInspekciskiTretman`) … (+5 formi) |
| 9 | `KontejnerskiTerminali` | **frmKontejnerskiTerminali** (`Uvoz/frmKontejnerskiTerminali.cs`): Izvoz › Šifarnici › Kontejnerski terminali → **frmKontejnerskiTerminali** | frmDrumski1 (`cboMestoSpustanjaPunogI1`), frmIzvozKonacna (`cboPPCNT, cboPPCNT2, cboPPCNT3`), frmIzvozKonacnaTable (`cboOpsti`), frmPrijemKamionaDetalji (`cboNalogodavac`), frmPrijemKontejneraKamionLegetIzvoz (`cboPPCNT`), frmPrijemKontejneraKamionLegetUvoz (`cboRLTerminal`) … (+5 formi) |
| 10 | `uvNacinPakovanja` | **fruvNacinPakovanja** (`Uvoz/fruvNacinPakovanja.cs`): Izvoz › Šifarnici › Način pakovanja → **fruvNacinPakovanja** | frmDrumski1 (`cboNacinPakovanjaI1`), frmGrupniUnosPoljaIzvoz (`cboNacinPakovanja`), frmIzvozKonacna (`cbNacinPakovanja`), frmIzvozKonacnaTable (`cboOpsti`), frmOtpremaKontejneraUvozKamion (`cbPostupak`), frmPrijemKamionaDetalji (`cboNacinPakovanja`) … (+4 formi) |
| 11 | `uvKvalitetKontejnera` | **frmuvKvalitetKontejnera** (`Uvoz/frmuvKvalitetKontejnera.cs`): Terminal › Šifarnici › Kvalitet kontejnera → **frmuvKvalitetKontejnera** | frmCIR (`cboKvalitet`), frmDefinisiPoziciju (`cboKvalitet, cboKvalitet2`), frmDrumski1 (`cboKvalitetKontejneraI1`), frmGrupniUnosPoljaIzvoz (`cboKvalitetKontejnera`), frmPopisKonterjnera (`cboKvalitet`), frmPrijemKamionaDetalji (`cboKvalitetKontejnera`) … (+2 formi) |
| 12 | `VrstePostupakaUvoz` | **frmVrstePostupakaUvoz** (`Uvoz/frmVrstePostupakaUvoz.cs`): Terminal › Šifarnici › Predefinisane poruke - treba → **frmVrstePostupakaUvoz** | frmIzvozKonacna (`cboPostupanjeSaRobom`), frmOtpremaKontejnera (`cboPostupak`), frmOtpremaKontejneraLegetIZVOZ (`cboPostupak`), frmPrijemKontejneraKamionLegetIzvoz (`cbPostupak`), frmPrijemKontejneraKamionLegetUvoz (`cbPostupak`), frmPrijemKontejneraLegetVozUvoz (`cbPostupak`) … (+2 formi) |
| 13 | `Vozila` | **frmVozila** (`TrackModal/Dokumeta/frmVozila.cs`): Terminal › Šifarnici › Sredstva rada → **frmVozila** | frmAutoprevozniList2 (`cboPrikljucnoVozilo, cboVozilo`), frmPutniNalog (`cboPrikljucnoVozilo, cboVozilo`), frmRadniNalogTransport (`cboPrikljucnoVozilo, cboVozilo`), Otpremnica (`cboVozilo`) |
| 14 | `DirigacijaKontejneraZa` | **frmDirigacijaKontejneraZa** (`Uvoz/frmDirigacijaKontejneraZa.cs`): Terminal › Šifarnici › Dirigacija kontejnera za → **frmDirigacijaKontejneraZa** | frmOtpremaKontejneraLegetIZVOZ (`cboStatusKontejnera`), frmOtpremaKontejneraUvozKamion (`cboStatusKontejnera`), frmPrijemKontejneraKamionLegetIzvoz (`cboStatusKontejnera`), frmPrijemKontejneraKamionLegetUvoz (`cboStatusKontejnera`), frmPrijemKontejneraLegetVozUvoz (`cboStatusKontejnera`), frmUvozKonacna (`cbDirigacija`) |
| 15 | `Predefinisaneporuke` | **frmPredefinisanePoruke** (`TrackModal/Sifarnici/frmPredefinisanePoruke.cs`): Terminal › Šifarnici › Predefinisane poruke → **frmPredefinisanePoruke** | frmIzvozKonacna (`cbNapomenaPoz`), frmOtpremaKontejnera (`cboPredefinisanaPoruka, cboPredefinisanePoruke`), frmPrijemKontejneraKamionLegetIzvoz (`cboPredefinisanePoruke`), frmPrijemKontejneraKamionLegetUvoz (`cboPredefinisanePoruke`), frmPrijemKontejneraLegetVozUvoz (`cboPredefinisanePoruke`) |
| 16 | `Valute` | **frmValute** (`Izvoz/frmValute.cs`): Izvoz › Šifarnici › Valute → **frmValute** | frmDrumski (`txtValuta`), frmDrumski1 (`txtValutaI1`), frmIzvozKonacna (`txtValuta`), frmIzvozKonacnaTable (`cboOpsti`), frmPartnerji (`cboValuta`), Prijemnica (`cboValutaStavka`) |
| 17 | `Scenario` | **frmScenario** (`Sifarnici/frmScenario.cs`): Terminal › Šifarnici › Scenario → **frmScenario** | frmIzvozKonacna (`cboScenario`), frmIzvozUnosManipulacije (`cboScenario`), frmUnosManipulacija (`cboScenario`), frmUvozKonacna (`cboScenario`) |
| 18 | `KrajnjaDestinacija` | **KrajnjeDestinacije** (`Sifarnici/KrajnjeDestinacije.cs`): Izvoz › Šifarnici › Krajnje destinacije → **KrajnjeDestinacije** | frmIzvozKonacna (`cboKrajnjaDestinacija`), frmIzvozKonacnaTable (`cboOpsti`), frmOtpremaKontejneraLegetIZVOZ (`cboKrajnjaDestinacija`) |
| 19 | `NapomenaZaPozicioniranje` | **frmNapomenaZaPozicioniranje** (`Uvoz/frmNapomenaZaPozicioniranje.cs`): Izvoz › Šifarnici › Napomene za pozicioniranje → **frmNapomenaZaPozicioniranje** | frmDrumski1 (`cboNapomenaPozI1`), frmGrupniUnosPoljaIzvoz (`cbNapomenaPoz`), frmUvozKonacna (`cbNapomenaPoz`) |
| 20 | `StatusVozila` | **frmStatusVozila** (`Drumski/frmStatusVozila.cs`): Drumski › Šifarnici › Status vozila → **frmStatusVozila**<br>**frmStatusVozilaDetalji** (`Drumski/frmStatusVozilaDetalji.cs`): Drumski › Šifarnici › Status vozila → **frmStatusVozila** → `button22` (Click) → **frmStatusVozilaDetalji** | frmDrumski (`cboStatus`), frmDrumski1 (`cboStatusI1`), frmPregledNalogaDrumski (`cboStatus`) |
| 21 | `VrstaRobe` | **frmVrstaRobe** (`TrackModal/Sifarnici/frmVrstaRobe.cs`): Terminal › Šifarnici › Vrsta robe HS → **frmVrstaRobe** | frmNalogZaPrevoz (`cboVrstaRobeKomerijala`), frmOtpremaKontejnera (`cboVrstaRobe`), frmOtpremaKontejneraUvozKamion (`cboVrstaRobe`) |
| 22 | `Brodovi` | **Brodovi** (`Uvoz/Brodovi.cs`): Izvoz › Šifarnici › Brodovi → **Brodovi** | frmUvozKonacna (`cbBrod`), frmUvozKonacnaTable (`cboOpsti`) |
| 23 | `GrupaVrsteManipulacije` | **frmGrupaVrsteManipulacije** (`TrackModal/Sifarnici/frmGrupaVrsteManipulacije.cs`): Terminal › Šifarnici › Grupe usluga → **frmGrupaVrsteManipulacije** | frmIzvozUnosManipulacije (`cboGrupaVrsteManipulacije`), frmUnosManipulacija (`cboGrupaVrsteManipulacije`) |
| 24 | `MerskeEnote` *(Pantheon)* | **frmJediniceMere** (`Sifarnici/frmJediniceMere.cs`): Komercijala › Ugovori › Jedinice mere → **frmJediniceMere**<br>**frmStatusUsluge** (`Sifarnici/frmStatusUsluge.cs`): Uvoz › Šifarnici › Status uluge → **frmStatusUsluge** | Prijemnica (`cbo3, cboJM`) |
| 25 | `StatusKapija` | **frmStatus** (`Kapija/frmStatus.cs`): Kapija › Šifarnici › Status kapija → **frmStatus** | frmKamionDetalji (`cboStatus`), frmPregledKamiona (`cboStatus`) |
| 26 | `TipKomercijalnogDokumenta` | **frmTipKomercijalnogDokumenta** (`Sifarnici/frmTipKomercijalnogDokumenta.cs`): Izvoz › Šifarnici › Tip komercijalnog dokumenta → **frmTipKomercijalnogDokumenta** | frmIzvozDokumenta (`cboTipDokumenta1`), UvozDokumenta (`cboTipDokumenta`) |
| 27 | `Delovi` | **frmDelovi** (`TrackModal/Sifarnici/frmDelovi.cs`): Terminal › Šifarnici › Delovi kontejnera → **frmDelovi** | frmCIR (`cboDeo`) |
| 28 | `Greske` | **frmGreske** (`TrackModal/Sifarnici/frmGreske.cs`): Terminal › Šifarnici › Oštećenja → **frmGreske** | frmCIR (`cboOstecenja`) |
| 29 | `SerijeKola` | **frmSerijeKola** (`Uvoz/frmSerijeKola.cs`): Terminal › Šifarnici › Serije kola → **frmSerijeKola** | frmVoz (`cboSerijaKola`) |
| 30 | `Stanice` | **frmStanice** (`Sifarnici/frmStanice.cs`): Osnovni podaci › Prevozni šifarnici › Stanice → **frmStanice** | frmVoz (`cboStanicaDo, cboStanicaOd`) |
| 31 | `TipSkladista` | **frmTipSkladista** (`TrackModal/Sifarnici/frmTipSkladista.cs`): Terminal › Šifarnici › Tip skladišta → **frmTipSkladista** | SkladisteGrupa (`cboCarinskaIspostava`) |
| 32 | `TipZone` | **frmTipZone** (`TrackModal/Sifarnici/frmTipZone.cs`): Terminal › Šifarnici › Tip zone → **frmTipZone** | frmZone (`cboTipZoneID`) |
| 33 | `VrstaRobeHS` | **frmVrstaRobeHS** (`Uvoz/frmVrstaRobeHS.cs`): Izvoz › Šifarnici › Vrsta robe HS → **frmVrstaRobeHS** | frmIzvozKonacna (`cboNazivRobe`) |

#### 3.2.1 Isto, grupisano po formi starog izgleda koju treba dodati u novi meni

| Forma (stari izgled) | Fajl | Put u MainP | Tabele koje pokriva |
|---|---|---|---|
| **Brodovi** | `Uvoz/Brodovi.cs` | Izvoz › Šifarnici › Brodovi → **Brodovi** | `Brodovi` |
| **Carinarnice** | `Uvoz/Carinarnice.cs` | Uvoz › Šifarnici › Carinarnice → **Carinarnice** | `Carinarnice` |
| **frmDelovi** | `TrackModal/Sifarnici/frmDelovi.cs` | Terminal › Šifarnici › Delovi kontejnera → **frmDelovi** | `Delovi` |
| **frmDirigacijaKontejneraZa** | `Uvoz/frmDirigacijaKontejneraZa.cs` | Terminal › Šifarnici › Dirigacija kontejnera za → **frmDirigacijaKontejneraZa** | `DirigacijaKontejneraZa` |
| **frmGreske** | `TrackModal/Sifarnici/frmGreske.cs` | Terminal › Šifarnici › Oštećenja → **frmGreske** | `Greske` |
| **frmGrupaVrsteManipulacije** | `TrackModal/Sifarnici/frmGrupaVrsteManipulacije.cs` | Terminal › Šifarnici › Grupe usluga → **frmGrupaVrsteManipulacije** | `GrupaVrsteManipulacije` |
| **frmInspekciskiTretman** | `Izvoz/frmInspekciskiTretman.cs` | Terminal › Šifarnici › Inspekciski tretman → **frmInspekciskiTretman** | `InspekciskiTretman` |
| **frmJediniceMere** | `Sifarnici/frmJediniceMere.cs` | Komercijala › Ugovori › Jedinice mere → **frmJediniceMere** | `MerskeEnote` |
| **frmKontejnerskiTerminali** | `Uvoz/frmKontejnerskiTerminali.cs` | Izvoz › Šifarnici › Kontejnerski terminali → **frmKontejnerskiTerminali** | `KontejnerskiTerminali` |
| **frmMestaUtovara** | `Izvoz/frmMestaUtovara.cs` | Terminal › Šifarnici › Mesta utovara → **frmMestaUtovara** | `MestaUtovara` |
| **frmNapomenaZaPozicioniranje** | `Uvoz/frmNapomenaZaPozicioniranje.cs` | Izvoz › Šifarnici › Napomene za pozicioniranje → **frmNapomenaZaPozicioniranje** | `NapomenaZaPozicioniranje` |
| **frmNHM** | `Sifarnici/frmNHM.cs` | Osnovni podaci › Prevozi › NHM → **frmNHM** | `NHM` |
| **frmPredefinisanePoruke** | `TrackModal/Sifarnici/frmPredefinisanePoruke.cs` | Terminal › Šifarnici › Predefinisane poruke → **frmPredefinisanePoruke** | `Predefinisaneporuke` |
| **frmScenario** | `Sifarnici/frmScenario.cs` | Terminal › Šifarnici › Scenario → **frmScenario** | `Scenario` |
| **frmSerijeKola** | `Uvoz/frmSerijeKola.cs` | Terminal › Šifarnici › Serije kola → **frmSerijeKola** | `SerijeKola` |
| **frmStanice** | `Sifarnici/frmStanice.cs` | Osnovni podaci › Prevozni šifarnici › Stanice → **frmStanice** | `Stanice` |
| **frmStatus** | `Kapija/frmStatus.cs` | Kapija › Šifarnici › Status kapija → **frmStatus** | `StatusKapija` |
| **frmStatusVozila** | `Drumski/frmStatusVozila.cs` | Drumski › Šifarnici › Status vozila → **frmStatusVozila** | `StatusVozila` |
| **frmTipKomercijalnogDokumenta** | `Sifarnici/frmTipKomercijalnogDokumenta.cs` | Izvoz › Šifarnici › Tip komercijalnog dokumenta → **frmTipKomercijalnogDokumenta** | `TipKomercijalnogDokumenta` |
| **frmTipSkladista** | `TrackModal/Sifarnici/frmTipSkladista.cs` | Terminal › Šifarnici › Tip skladišta → **frmTipSkladista** | `TipSkladista` |
| **frmTipZone** | `TrackModal/Sifarnici/frmTipZone.cs` | Terminal › Šifarnici › Tip zone → **frmTipZone** | `TipZone` |
| **frmuvKvalitetKontejnera** | `Uvoz/frmuvKvalitetKontejnera.cs` | Terminal › Šifarnici › Kvalitet kontejnera → **frmuvKvalitetKontejnera** | `uvKvalitetKontejnera` |
| **frmValute** | `Izvoz/frmValute.cs` | Izvoz › Šifarnici › Valute → **frmValute** | `Valute` |
| **frmVozila** | `TrackModal/Dokumeta/frmVozila.cs` | Terminal › Šifarnici › Sredstva rada → **frmVozila** | `Vozila` |
| **frmVrstaCarinskogPostupka** | `Uvoz/frmVrstaCarinskogPostupka.cs` | Terminal › Šifarnici › Vrsta Carinskog postupka → **frmVrstaCarinskogPostupka** | `VrstaCarinskogPostupka` |
| **frmVrstaManipulacije** | `TrackModal/Sifarnici/frmVrstaManipulacije.cs` | Terminal › Šifarnici › Vrste usluga → **frmVrstaManipulacije** | `VrstaManipulacije` |
| **frmVrstaRobe** | `TrackModal/Sifarnici/frmVrstaRobe.cs` | Terminal › Šifarnici › Vrsta robe HS → **frmVrstaRobe** | `VrstaRobe` |
| **frmVrstaRobeADR** | `Uvoz/frmVrstaRobeADR.cs` | Terminal › Šifarnici › Vrsta robe ADR → **frmVrstaRobeADR** | `VrstaRobeADR` |
| **frmVrstaRobeHS** | `Uvoz/frmVrstaRobeHS.cs` | Izvoz › Šifarnici › Vrsta robe HS → **frmVrstaRobeHS** | `VrstaRobeHS` |
| **frmVrstaVozila** | `Drumski/frmVrstaVozila.cs` | Drumski › Šifarnici › Vrsta vozila → **frmVrstaVozila** | `VrstaVozila` |
| **frmVrstePostupakaUvoz** | `Uvoz/frmVrstePostupakaUvoz.cs` | Terminal › Šifarnici › Predefinisane poruke - treba → **frmVrstePostupakaUvoz** | `VrstePostupakaUvoz` |
| **fruvNacinPakovanja** | `Uvoz/fruvNacinPakovanja.cs` | Izvoz › Šifarnici › Način pakovanja → **fruvNacinPakovanja** | `uvNacinPakovanja` |
| **KrajnjeDestinacije** | `Sifarnici/KrajnjeDestinacije.cs` | Izvoz › Šifarnici › Krajnje destinacije → **KrajnjeDestinacije** | `KrajnjaDestinacija` |

### 3.3 Combobox tabele bez pronađene forme za održavanje (ni u jednom izgledu)

Za ove tabele kod ne pokazuje formu koja ih puni za Leget. Mogući razlozi: puni ih Pantheon/sinhronizacija ili skripta u bazi; forma postoji ali je za Leget sakrivena ili mrtva; ili je izvor pogled (`uv…`, `v…`) ili transakciona tabela, a ne šifarnik. Kolona „Forma postoji u projektu“ pokazuje forme koje upisuju u tabelu, a nisu dostupne Leget-u ni iz jednog menija.

| Tabela | Forma postoji u projektu (nedostupna) | Koristi se u novom izgledu |
|---|---|---|
| `partnerjiKontOsebaMU` *(Pantheon)* | — | frmDrumski1 (`cboCilj, cboKontakt`), frmIzvozKonacna (`txtKontaktOsoba`), frmMestoIsporuke (`cboCilj, cboKontakt`), frmPrijemKamionaDetalji (`cboCilj, cboKontakt`) … (+5 formi) |
| `VrstePrevoznogSredstva` | — | InterniPoslovi (`cboTipTransportaOtprema`), InterniPrenos (`cboTipTransportaOtprema`), Otprema (`cboTipTransportaOtprema`), Otpremnica (`cboTipTransportaOtprema`) … (+2 formi) |
| `Delavci` *(Pantheon)* | — | frmKorisnici (`cboLargoKorisnik`), Otpremnica (`cboRukovalac, cboVozac, comboBox1`), Prijemnica (`cboPrimio, cboRukovalac, cbo_Referent`) |
| `Komitenti` | frmKomitent (`TrackModal/Sifarnici/frmKomitent.cs`) | frmAutoprevozniList2 (`cboPlatilac, cboPrimalac`), frmManipulacije (`cboPlatilac`), frmNalogZaPrevoz (`cboPlatilac, cboPrimalac`), frmOtpremaKontejnera (`cboOrganizator, cboPosiljalac, cboPrimalac`) |
| `organizacioneJedinice` | — | frmNalogZaPrevoz (`cboOrganizacionaJedinica`), frmRadniNalogInterniPregled (`cboIzdatOd, cboIzdatZa`) |
| `partnerjiKontOseba` *(Pantheon)* | — | frmIzvozKonacna (`cboSpedicija`), frmNalogPrijem (`cboKontakt`), frmUvozKonacna (`cbOspedicija`) |
| `PrijemKontejneraVoz` | frmPrijemKontejneraVoz (`TrackModal/Dokumeta/frmPrijemKontejneraVoz.cs`), frmPrijemKontejneraVoz (`Dokumenta/frmPrijemKontejneraVoz.cs`) | frmManipulacije (`cboPrijemKamionom, cboPrijemVozom`), frmPregledNarucenihManipulacija (`cboPrijemKamionom, cboPrijemVozom`) |
| `DrumskiPozicioniranje` | — | frmDrumski (`cboNapomenaPoz`), frmDrumski1 (`cboNapomenaPozI1`) |
| `KontejnerStatus` | — | frmPopisKonterjnera (`cboStatusKontejnera`), frmUnosManipulacija (`cboStatus`) |
| `Lokac` | — | Otpremnica (`cbo_Skladiste`), Prijemnica (`cbo_Lokacija`) |
| `Mesta` | — | Otpremnica (`cbo_MestoTroska`), Prijemnica (`cbo_MestoTroska`) |
| `PutniNalog` | — | frmPutniNalog (`cboVozacRute`) |
| `PutniNalogZaposleni` | — | frmPutniNalog (`cboVozacRute`) |
| `Sklad` | — | Otpremnica (`cbo_Skladiste`), Prijemnica (`cbo_Skladiste`) |
| `StatusRobe` | frmStatusRobe (`TrackModal/Sifarnici/frmStatusRobe.cs`) | frmOtpremaKontejnera (`cboStatusKontejnera`) |
| `ArtikliCetvrtiNivo` | — | Prijemnica (`cboPLKlasifikacija`) |
| `ArtikliDrugiNivo` | — | Prijemnica (`cboPLKlasifikacija`) |
| `ArtikliPrviNivo` | — | Prijemnica (`cboPLKlasifikacija`) |
| `ArtikliTreciNivo` | — | Prijemnica (`cboPLKlasifikacija`) |
| `PodtipProtokolaRazno` | — | frmProtokolTransportnogNaloga (`cboSituacija, cboSituacijaCerada`) |
| `SistemskePostavke` | — | frmDrumski (`cboKamion`) |
| `Stock` | — | PregledLagera (`cboFilter, cboPrvi`) |
| `vSERNarudzbenica` | — | frmSERPrijem (`cbNarudzbenica`) |

### 3.4 Combobox tabele koje se već održavaju u novom izgledu

| Tabela | Forma u novom izgledu (modul) | U „Podešavanja sistema“? |
|---|---|---|
| `Automobili` | frmAutomobilEdit (Drumski transport), frmAutomobiliDrumski (Drumski transport) | ne |
| `IzvozKonacna` | frmFormiranjePlanaIzvoz (Prijem i otprema vozova), frmIzvozKonacnaTable (Prijem i otprema vozova), frmIzvozUnosManipulacije (Logistika izvoza) | ne |
| `IzvozKonacnaZaglavlje` | frmIzvozKonacna (Prijem i otprema vozova), frmVoz (Prijem i otprema vozova) | ne |
| `Korisnici` | frmKorisnici (Administracija) | ne |
| `MagacinskiBrojCarinski` | frmNalogPrijem (Operacije skladišta), InterniPoslovi (Operacije skladišta), InterniPrenos (Operacije skladišta), Otprema (Operacije skladišta) … | ne |
| `OtpremaKontejnera` | frmOtpremaKontejnera (Prijem i otprema vozova), frmOtpremaKontejneraIzvozKamion (Prijem i otprema vozova), frmOtpremaKontejneraLegetIZVOZ (Prijem i otprema vozova) | ne |
| `Partnerji` *(Pantheon)* | frmPartnerji (Podešavanja sistema) | da |
| `Pozicija` | frmPozicija (Podešavanja sistema) | da |
| `PredefinisanePozicije` | frmDefinisiPoziciju (Prijem i otprema vozova) | ne |
| `Promet` | frmPrijemKontejneraKamionLegetUvoz (Prijem i otprema vozova), frmPrijemKontejneraLegetVozUvoz (Prijem i otprema vozova), Otpremnica (Operacije pretovari), Prijemnica (Operacije pretovari) | ne |
| `RadniNalogDrumski` | frmDrumski (Drumski transport), frmPregledNalogaDrumski (Drumski transport) | ne |
| `RadniNalogInterni` | frmUvozKonacna (Prijem i otprema vozova) | ne |
| `RadniNalogSkladista` | InterniPoslovi (Operacije skladišta), InterniPrenos (Operacije skladišta), Otprema (Operacije skladišta), PregledKomercijalnihNaloga (Operacije skladišta) … | ne |
| `RNCarinskoPrijemnicaStavke` | Prijemnica (Operacije skladišta) | ne |
| `RNCarinskoSkladistePrijemnica` | Prijemnica (Operacije skladišta) | ne |
| `Skladista` | frmSkladista (Podešavanja sistema), TerminalMapFRM (Prijem i otprema vozova) | da |
| `SkladistaGrupa` | SkladisteGrupa (Podešavanja sistema) | da |
| `TipKontenjera` | frmTipKontejnera (Podešavanja sistema) | da |
| `TipPalete` | frmTipPalete (Operacije skladišta) | ne |
| `UvozKonacnaZaglavlje` | frmUvozKonacna (Prijem i otprema vozova) | ne |
| `Voz` | frmVoz (Prijem i otprema vozova) | ne |
| `Zaposleni` | frmHRZaposleni (HR modul) | ne |

### 3.5 Ostale tabele koje novi izgled samo čita, a pune se samo u starom izgledu

Nisu izvor combobox-a, ali ih forme novog izgleda čitaju (grid, provere, pregledi), a jedina forma koja u njih upisuje je u starom izgledu. Deo su šifarnici, deo transakcije koje nastaju radom u starom izgledu.

| Tabela | Upisuje (stari izgled) | Čita (novi izgled) |
|---|---|---|
| `Cene` | frmCene | frmIzvozKonacna, frmIzvozUnosManipulacije, frmNalogZaPrevoz, frmUnosManipulacija, frmUvozKonacna |
| `IzvozNHM` | frmIzvoz | frmDopunaPlanaPraznimIzvoz, frmDrumski1, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna … (+1) |
| `IzvozVrstaRobeHS` | frmIzvoz | frmDopunaPlanaPraznimIzvoz, frmFormiranjePlanaIzvoz, frmIzvozKonacna, frmIzvozUnosManipulacije |
| `MAticniPodatki` *(Pantheon)* | frmMaticniPodatki | Otpremnica, Prijemnica |
| `Narocilo` *(Pantheon)* | frmDogovori | Otpremnica |
| `RNOtpremaCirade` | RN8OtpremaCirade | frmAnalizaRadnihNaloga, frmOtpremaKontejneraIzvozKamion, frmOtpremaKontejneraUvozKamion |
| `RNPregledIpostavkaKontejnera` | RN11PreglediPostavkaKontejnera | frmAnalizaRadnihNaloga |
| `RNPregledIspraznjenogKontejnera` | RN10PregledIspraznjenogKontejnera | frmAnalizaRadnihNaloga |
| `RNPrijemCirade` | RN9PrijemCirade | frmAnalizaRadnihNaloga, frmPrijemKontejneraKamionLegetIzvoz, frmPrijemKontejneraKamionLegetUvoz |
| `UvozNapomenePozicioniranja` | Uvoz | frmFormiranjePlana, frmRadniNalogInterniPregled, frmUvozKonacna, frmUvozKonacnaTable |
| `UvozVrstaRobeHS` | Uvoz | frmUvozKonacna |

### 3.6 Šifarnici starog izgleda kojih nema u novom izgledu

Sva dugmad starog menija (`MainP`) iz grupa sa šifarnicima i osnovnim podacima, čija forma nije dostupna iz novog izgleda. „Treba novom izgledu“ znači da forme novog izgleda čitaju bar jednu tabelu koju ova forma puni.

| Put u MainP | Forma | Tabele koje puni | Treba novom izgledu |
|---|---|---|---|
| Carinsko › Šifarnici › Carinarnice | **Carinarnice** (`Uvoz/Carinarnice.cs`) | `Carinarnice` | **da**: `Carinarnice` |
| Carinsko › Šifarnici › Jedinice mere | **frmJediniceMere** (`Sifarnici/frmJediniceMere.cs`) | `MerskeEnote` | **da**: `MerskeEnote` |
| Drumski › Šifarnici › Mesta utovara | **frmMestaUtovara** (`Izvoz/frmMestaUtovara.cs`) | `MestaUtovara` | **da**: `MestaUtovara` |
| Drumski › Šifarnici › Pozicioniranje | **frmPozicioniranje** (`Drumski/frmPozicioniranje.cs`) | — | ne |
| Drumski › Šifarnici › Status vozila | **frmStatusVozila** (`Drumski/frmStatusVozila.cs`) | `StatusVozila` | **da**: `StatusVozila` |
| Drumski › Šifarnici › Valute | **frmValute** (`Izvoz/frmValute.cs`) | `Valute` | **da**: `Valute` |
| Drumski › Šifarnici › Vrsta vozila | **frmVrstaVozila** (`Drumski/frmVrstaVozila.cs`) | `VrstaVozila` | **da**: `VrstaVozila` |
| Evidencije › Vrste aktivnosti › Cene | **frmCenaPoRadniku** (`Sifarnici/frmCenaPoRadniku.cs`) | `EvidencijaCenaRadnik` | ne |
| Evidencije › Vrste aktivnosti › Vrste | **frmVrsteAktivnosti** (`Sifarnici/frmVrsteAktivnosti.cs`) | `VrstaAktivnosti` | ne |
| Izvoz › Šifarnici › Adrese statusa vozila | **frmAdresaStatusVozila** (`Izvoz/frmAdresaStatusVozila.cs`) | `AdresaStatusVozila` | ne |
| Izvoz › Šifarnici › Brodovi | **Brodovi** (`Uvoz/Brodovi.cs`) | `Brodovi` | **da**: `Brodovi` |
| Izvoz › Šifarnici › Carinarnice | **Carinarnice** (`Uvoz/Carinarnice.cs`) | `Carinarnice` | **da**: `Carinarnice` |
| Izvoz › Šifarnici › Cene | **frmCene** (`TrackModal/Sifarnici/frmCene.cs`) | `Aktivnosti`, `Cene` | **da**: `Cene` |
| Izvoz › Šifarnici › Dirigacija kontejnera za | **frmDirigacijaKontejneraZa** (`Uvoz/frmDirigacijaKontejneraZa.cs`) | `DirigacijaKontejneraZa` | **da**: `DirigacijaKontejneraZa` |
| Izvoz › Šifarnici › Grupe usluga | **frmGrupaVrsteManipulacije** (`TrackModal/Sifarnici/frmGrupaVrsteManipulacije.cs`) | `GrupaVrsteManipulacije` | **da**: `GrupaVrsteManipulacije` |
| Izvoz › Šifarnici › Inspekciski tretman | **frmInspekciskiTretman** (`Izvoz/frmInspekciskiTretman.cs`) | `InspekciskiTretman` | **da**: `InspekciskiTretman` |
| Izvoz › Šifarnici › Kontejnerski terminali | **frmKontejnerskiTerminali** (`Uvoz/frmKontejnerskiTerminali.cs`) | `KontejnerskiTerminali` | **da**: `KontejnerskiTerminali` |
| Izvoz › Šifarnici › Krajnje destinacije | **KrajnjeDestinacije** (`Sifarnici/KrajnjeDestinacije.cs`) | `KrajnjaDestinacija` | **da**: `KrajnjaDestinacija` |
| Izvoz › Šifarnici › Kvalitet kontejnera | **frmuvKvalitetKontejnera** (`Uvoz/frmuvKvalitetKontejnera.cs`) | `uvKvalitetKontejnera` | **da**: `uvKvalitetKontejnera` |
| Izvoz › Šifarnici › Mesta utovara | **frmMestaUtovara** (`Izvoz/frmMestaUtovara.cs`) | `MestaUtovara` | **da**: `MestaUtovara` |
| Izvoz › Šifarnici › Napomene za pozicioniranje | **frmNapomenaZaPozicioniranje** (`Uvoz/frmNapomenaZaPozicioniranje.cs`) | `NapomenaZaPozicioniranje` | **da**: `NapomenaZaPozicioniranje` |
| Izvoz › Šifarnici › Naslovi statusa vozila | **frmNaslovStatusaVozila** (`Izvoz/frmNaslovStatusaVozila.cs`) | `NaslovStatusaVozila` | ne |
| Izvoz › Šifarnici › Način pakovanja | **fruvNacinPakovanja** (`Uvoz/fruvNacinPakovanja.cs`) | `uvNacinPakovanja` | **da**: `uvNacinPakovanja` |
| Izvoz › Šifarnici › NHM | **frmNHM** (`Sifarnici/frmNHM.cs`) | `NHM` | **da**: `NHM` |
| Izvoz › Šifarnici › Organizacione jedinice | **frmOrganizacionaJedinica** (`TrackModal/Sifarnici/frmOrganizacionaJedinica.cs`) | — | ne |
| Izvoz › Šifarnici › Postupak sa robom/Kontejnerom | **frmVrstePostupakaUvoz** (`Uvoz/frmVrstePostupakaUvoz.cs`) | `VrstePostupakaUvoz` | **da**: `VrstePostupakaUvoz` |
| Izvoz › Šifarnici › REEXPORT | **frmVrstaCarinskogPostupka** (`Uvoz/frmVrstaCarinskogPostupka.cs`) | `VrstaCarinskogPostupka` | **da**: `VrstaCarinskogPostupka` |
| Izvoz › Šifarnici › Serije kola | **frmSerijeKola** (`Uvoz/frmSerijeKola.cs`) | `SerijeKola` | **da**: `SerijeKola` |
| Izvoz › Šifarnici › Tip cenovnika | **frmTipCenovnika** (`TrackModal/Sifarnici/frmTipCenovnika.cs`) | `TipCenovnika` | ne |
| Izvoz › Šifarnici › Tip komercijalnog dokumenta | **frmTipKomercijalnogDokumenta** (`Sifarnici/frmTipKomercijalnogDokumenta.cs`) | `TipKomercijalnogDokumenta` | **da**: `TipKomercijalnogDokumenta` |
| Izvoz › Šifarnici › Valute | **frmValute** (`Izvoz/frmValute.cs`) | `Valute` | **da**: `Valute` |
| Izvoz › Šifarnici › Vrsta Carinskog postupka | **frmVrstePostupakaUvoz** (`Uvoz/frmVrstePostupakaUvoz.cs`) | `VrstePostupakaUvoz` | **da**: `VrstePostupakaUvoz` |
| Izvoz › Šifarnici › Vrsta robe ADR | **frmVrstaRobeADR** (`Uvoz/frmVrstaRobeADR.cs`) | `VrstaRobeADR` | **da**: `VrstaRobeADR` |
| Izvoz › Šifarnici › Vrsta robe HS | **frmVrstaRobeHS** (`Uvoz/frmVrstaRobeHS.cs`) | `VrstaRobeADR`, `VrstaRobeHS` | **da**: `VrstaRobeADR`, `VrstaRobeHS` |
| Izvoz › Šifarnici › Vrste plombi | **frmVrstePlombi** (`Izvoz/frmVrstePlombi.cs`) | `VrstePlombi` | ne |
| Izvoz › Šifarnici › Vrste usluga | **frmVrstaManipulacije** (`TrackModal/Sifarnici/frmVrstaManipulacije.cs`) | `VrstaManipulacije` | **da**: `VrstaManipulacije` |
| Kadrovi › Osnovni podaci › Bolovanja | **frmBolovanja** (`Dokumenta/frmBolovanja.cs`) | `Bolovanje` | ne |
| Kadrovi › Osnovni podaci › Rad noću/Prekovremeni | **frmPrekovremeniRad** (`Dokumenta/frmPrekovremeniRad.cs`) | `PrekovremeniRad` | ne |
| Kadrovi › Osnovni podaci › Radna mesta | **frmDelovnaMesta** (`Sifarnici/frmDelovnaMesta.cs`) | `DelovnaMesta` | ne |
| Kadrovi › Osnovni podaci › Radnici | **frmDelavci** (`Sifarnici/frmDelavci.cs`) | — | ne |
| Kadrovi › Testiranje radnika › Evidencija poznavanja pruga i lokomotiva | **frmOsoblje** (`Sifarnici/frmOsoblje.cs`) | — | ne |
| Kapija › Šifarnici › Status kapija | **frmStatus** (`Kapija/frmStatus.cs`) | `StatusKapija` | **da**: `StatusKapija` |
| Komercijala › Ugovori › Jedinice mere | **frmJediniceMere** (`Sifarnici/frmJediniceMere.cs`) | `MerskeEnote` | **da**: `MerskeEnote` |
| Komercijala › Ugovori › Matični podaci | **frmMaticniPodatki** (`Sifarnici/frmMaticniPodatki.cs`) | `MAticniPodatki` | **da**: `MAticniPodatki` |
| Komercijala › Ugovori › Načini isporuke | **frmNacinIsporuke** (`Sifarnici/frmNacinIsporuke.cs`) | `NacinDobave` | ne |
| Komercijala › Ugovori › Prodajne grupe | **frmProdajnaGrupa** (`Sifarnici/frmProdajnaGrupa.cs`) | `ProdSkup` | ne |
| Komercijala › Ugovori › Tip saobraćajnog prevoza | **frmObjekt** (`Sifarnici/frmObjekt.cs`) | `TipSaobPrevoza` | ne |
| Održavanje › Obrada kvarova › Lokomotive | **frmLokomotive** (`Sifarnici/frmLokomotive.cs`) | — | ne |
| Operatika › Komunikacija › Telegrami | **frmTelegrami** (`Sifarnici/frmTelegrami.cs`) | `telegrami` | ne |
| Operatika › Kontrola dokumentacije › Tip dokumenta | **frmSifarnikKontrolnihTipovaDokumenta** (`Sifarnici/frmSifarnikKontrolnihTipovaDokumenta.cs`) | `KontrolneGreskeTipDokumenta` | ne |
| Operatika › Kontrola dokumentacije › Šifarnik grešaka | **frmSifarnikKontrolnihGresaka** (`Sifarnici/frmSifarnikKontrolnihGresaka.cs`) | `KontrolneGreske` | ne |
| Osnovni podaci › Prevozi › NHM | **frmNHM** (`Sifarnici/frmNHM.cs`) | `NHM` | **da**: `NHM` |
| Osnovni podaci › Prevozi › Razlozi stajanja lokomotive | **frmRazlozi** (`Sifarnici/frmRazlozi.cs`) | `Razlozi` | ne |
| Osnovni podaci › Prevozi › Status vozova | **frmStatusVoza** (`Sifarnici/frmStatusVoza.cs`) | `StatusVoza` | ne |
| Osnovni podaci › Prevozi › Tip prevoza | **frmTipPrevoza** (`Sifarnici/frmTipPrevoza.cs`) | `TipSaobPrevoza` | ne |
| Osnovni podaci › Prevozi › Tipovi telegrama | **frmTipTelegrama** (`Sifarnici/frmTipTelegrama.cs`) | `TipTelegrama` | ne |
| Osnovni podaci › Prevozni šifarnici › Lokomotive | **frmLokomotive** (`Sifarnici/frmLokomotive.cs`) | — | ne |
| Osnovni podaci › Prevozni šifarnici › Pruge | **frmPruge** (`Sifarnici/frmPruge.cs`) | `PrugaStavke` | ne |
| Osnovni podaci › Prevozni šifarnici › Serije lokomotive | **frmSerijeLokomotiva** (`Sifarnici/frmSerijeLokomotiva.cs`) | `LokomotivaSerija` | ne |
| Osnovni podaci › Prevozni šifarnici › Serije Vagoni | **frmVagoniSerije** (`Sifarnici/frmVagoniSerije.cs`) | `VagoniSerije` | ne |
| Osnovni podaci › Prevozni šifarnici › Stanice | **frmStanice** (`Sifarnici/frmStanice.cs`) | `Stanice` | **da**: `Stanice` |
| Osnovni podaci › Prevozni šifarnici › Stavke primopredaje | **frmStavkePrimopredaje** (`Administracija/frmStavkePrimopredaje.cs`) | — | ne |
| Osnovni podaci › Prevozni šifarnici › Trase | **frmTrase** (`Sifarnici/frmTrase.cs`) | `Trase` | ne |
| Osnovni podaci › Prevozni šifarnici › Trase analitički | **frmTraseAnaliticki** (`Sifarnici/frmTraseAnaliticki.cs`) | — | ne |
| Osnovni podaci › Prevozni šifarnici › Vuča statusi | **frmVucaStatusi** (`Dokumenta/frmVucaStatusi.cs`) | — | ne |
| Terminal › Šifarnici › Carinarnice | **Carinarnice** (`Uvoz/Carinarnice.cs`) | `Carinarnice` | **da**: `Carinarnice` |
| Terminal › Šifarnici › Cene | **frmCene** (`TrackModal/Sifarnici/frmCene.cs`) | `Aktivnosti`, `Cene` | **da**: `Cene` |
| Terminal › Šifarnici › Delovi kontejnera | **frmDelovi** (`TrackModal/Sifarnici/frmDelovi.cs`) | `Delovi` | **da**: `Delovi` |
| Terminal › Šifarnici › Dirigacija kontejnera za | **frmDirigacijaKontejneraZa** (`Uvoz/frmDirigacijaKontejneraZa.cs`) | `DirigacijaKontejneraZa` | **da**: `DirigacijaKontejneraZa` |
| Terminal › Šifarnici › Grupe usluga | **frmGrupaVrsteManipulacije** (`TrackModal/Sifarnici/frmGrupaVrsteManipulacije.cs`) | `GrupaVrsteManipulacije` | **da**: `GrupaVrsteManipulacije` |
| Terminal › Šifarnici › Inspekciski tretman | **frmInspekciskiTretman** (`Izvoz/frmInspekciskiTretman.cs`) | `InspekciskiTretman` | **da**: `InspekciskiTretman` |
| Terminal › Šifarnici › Jedinice mere | **frmJediniceMere** (`Sifarnici/frmJediniceMere.cs`) | `MerskeEnote` | **da**: `MerskeEnote` |
| Terminal › Šifarnici › Kontejner status | **frmKontejnerStatus** (`Sifarnici/frmKontejnerStatus.cs`) | — | ne |
| Terminal › Šifarnici › Kontejnerski terminali | **frmKontejnerskiTerminali** (`Uvoz/frmKontejnerskiTerminali.cs`) | `KontejnerskiTerminali` | **da**: `KontejnerskiTerminali` |
| Terminal › Šifarnici › Krajnje destinacije | **KrajnjeDestinacije** (`Sifarnici/KrajnjeDestinacije.cs`) | `KrajnjaDestinacija` | **da**: `KrajnjaDestinacija` |
| Terminal › Šifarnici › Kvalitet kontejnera | **frmuvKvalitetKontejnera** (`Uvoz/frmuvKvalitetKontejnera.cs`) | `uvKvalitetKontejnera` | **da**: `uvKvalitetKontejnera` |
| Terminal › Šifarnici › Mesta utovara | **frmMestaUtovara** (`Izvoz/frmMestaUtovara.cs`) | `MestaUtovara` | **da**: `MestaUtovara` |
| Terminal › Šifarnici › Napomene za pozicioniranje | **frmNapomenaZaPozicioniranje** (`Uvoz/frmNapomenaZaPozicioniranje.cs`) | `NapomenaZaPozicioniranje` | **da**: `NapomenaZaPozicioniranje` |
| Terminal › Šifarnici › Način dolaska / odlaska | **frmNacinDolaskaOdlaska** (`TrackModal/Sifarnici/frmNacinDolaskaOdlaska.cs`) | — | ne |
| Terminal › Šifarnici › Način pakovanja | **fruvNacinPakovanja** (`Uvoz/fruvNacinPakovanja.cs`) | `uvNacinPakovanja` | **da**: `uvNacinPakovanja` |
| Terminal › Šifarnici › NHM | **frmNHM** (`Sifarnici/frmNHM.cs`) | `NHM` | **da**: `NHM` |
| Terminal › Šifarnici › Organizacione jedinice | **frmOrganizacionaJedinica** (`TrackModal/Sifarnici/frmOrganizacionaJedinica.cs`) | — | ne |
| Terminal › Šifarnici › Oštećenja | **frmGreske** (`TrackModal/Sifarnici/frmGreske.cs`) | `Greske` | **da**: `Greske` |
| Terminal › Šifarnici › Postupak sa robom/Kontejnerom | **frmVrstePostupakaUvoz** (`Uvoz/frmVrstePostupakaUvoz.cs`) | `VrstePostupakaUvoz` | **da**: `VrstePostupakaUvoz` |
| Terminal › Šifarnici › Predefinisane poruke | **frmPredefinisanePoruke** (`TrackModal/Sifarnici/frmPredefinisanePoruke.cs`) | `Predefinisaneporuke` | **da**: `Predefinisaneporuke` |
| Terminal › Šifarnici › Predefinisane poruke - treba | **frmVrstePostupakaUvoz** (`Uvoz/frmVrstePostupakaUvoz.cs`) | `VrstePostupakaUvoz` | **da**: `VrstePostupakaUvoz` |
| Terminal › Šifarnici › Scenario | **frmScenario** (`Sifarnici/frmScenario.cs`) | `Scenario` | **da**: `Scenario` |
| Terminal › Šifarnici › Serije kola | **frmSerijeKola** (`Uvoz/frmSerijeKola.cs`) | `SerijeKola` | **da**: `SerijeKola` |
| Terminal › Šifarnici › Sredstva rada | **frmVozila** (`TrackModal/Dokumeta/frmVozila.cs`) | `Vozila` | **da**: `Vozila` |
| Terminal › Šifarnici › Stanice | **frmStanice** (`Sifarnici/frmStanice.cs`) | `Stanice` | **da**: `Stanice` |
| Terminal › Šifarnici › Status kontejnera | **frmKontejnerStatus** (`Sifarnici/frmKontejnerStatus.cs`) | — | ne |
| Terminal › Šifarnici › Terminalske operacije | **frmTerminalOperacije** (`Tehnologija/frmTerminalOperacije.cs`) | `TerminalOperacije` | ne |
| Terminal › Šifarnici › Tip cenovnika | **frmTipCenovnika** (`TrackModal/Sifarnici/frmTipCenovnika.cs`) | `TipCenovnika` | ne |
| Terminal › Šifarnici › Tip skladišta | **frmTipSkladista** (`TrackModal/Sifarnici/frmTipSkladista.cs`) | `TipSkladista` | **da**: `TipSkladista` |
| Terminal › Šifarnici › Tip zone | **frmTipZone** (`TrackModal/Sifarnici/frmTipZone.cs`) | `TipZone` | **da**: `TipZone` |
| Terminal › Šifarnici › Vrsta Carinskog postupka | **frmVrstaCarinskogPostupka** (`Uvoz/frmVrstaCarinskogPostupka.cs`) | `VrstaCarinskogPostupka` | **da**: `VrstaCarinskogPostupka` |
| Terminal › Šifarnici › Vrsta robe ADR | **frmVrstaRobeADR** (`Uvoz/frmVrstaRobeADR.cs`) | `VrstaRobeADR` | **da**: `VrstaRobeADR` |
| Terminal › Šifarnici › Vrsta robe HS | **frmVrstaRobe** (`TrackModal/Sifarnici/frmVrstaRobe.cs`) | `VrstaRobe` | **da**: `VrstaRobe` |
| Terminal › Šifarnici › Vrste plombi | **frmVrstePlombi** (`Izvoz/frmVrstePlombi.cs`) | `VrstePlombi` | ne |
| Terminal › Šifarnici › Vrste usluga | **frmVrstaManipulacije** (`TrackModal/Sifarnici/frmVrstaManipulacije.cs`) | `VrstaManipulacije` | **da**: `VrstaManipulacije` |
| Terminal › Šifarnici › Zaposleni | **frmDelavci** (`Sifarnici/frmDelavci.cs`) | — | ne |
| Uvoz › Šifarnici › Adrese statusa vozila | **frmAdresaStatusVozila** (`Izvoz/frmAdresaStatusVozila.cs`) | `AdresaStatusVozila` | ne |
| Uvoz › Šifarnici › Brodovi | **Brodovi** (`Uvoz/Brodovi.cs`) | `Brodovi` | **da**: `Brodovi` |
| Uvoz › Šifarnici › Carinarnice | **Carinarnice** (`Uvoz/Carinarnice.cs`) | `Carinarnice` | **da**: `Carinarnice` |
| Uvoz › Šifarnici › Cene | **frmCene** (`TrackModal/Sifarnici/frmCene.cs`) | `Aktivnosti`, `Cene` | **da**: `Cene` |
| Uvoz › Šifarnici › Grupe usluga | **frmGrupaVrsteManipulacije** (`TrackModal/Sifarnici/frmGrupaVrsteManipulacije.cs`) | `GrupaVrsteManipulacije` | **da**: `GrupaVrsteManipulacije` |
| Uvoz › Šifarnici › Krajnje destinacije | **KrajnjeDestinacije** (`Sifarnici/KrajnjeDestinacije.cs`) | `KrajnjaDestinacija` | **da**: `KrajnjaDestinacija` |
| Uvoz › Šifarnici › Kvalitet kontejnera | **frmuvKvalitetKontejnera** (`Uvoz/frmuvKvalitetKontejnera.cs`) | `uvKvalitetKontejnera` | **da**: `uvKvalitetKontejnera` |
| Uvoz › Šifarnici › Naslovi statusa vozila | **frmNaslovStatusaVozila** (`Izvoz/frmNaslovStatusaVozila.cs`) | `NaslovStatusaVozila` | ne |
| Uvoz › Šifarnici › Organizacione jedinice | **frmOrganizacionaJedinica** (`TrackModal/Sifarnici/frmOrganizacionaJedinica.cs`) | — | ne |
| Uvoz › Šifarnici › Status uluge | **frmStatusUsluge** (`Sifarnici/frmStatusUsluge.cs`) | `MerskeEnote` | **da**: `MerskeEnote` |
| Uvoz › Šifarnici › Tip cenovnika | **frmTipCenovnika** (`TrackModal/Sifarnici/frmTipCenovnika.cs`) | `TipCenovnika` | ne |
| Uvoz › Šifarnici › Tip komercijalnog dokumenta | **frmTipKomercijalnogDokumenta** (`Sifarnici/frmTipKomercijalnogDokumenta.cs`) | `TipKomercijalnogDokumenta` | **da**: `TipKomercijalnogDokumenta` |
| Uvoz › Šifarnici › Valute | **frmValute** (`Izvoz/frmValute.cs`) | `Valute` | **da**: `Valute` |
| Uvoz › Šifarnici › Vrsta robe HS | **frmVrstaRobeHS** (`Uvoz/frmVrstaRobeHS.cs`) | `VrstaRobeADR`, `VrstaRobeHS` | **da**: `VrstaRobeADR`, `VrstaRobeHS` |

## 4. Stari izgled (MainP)

Stari glavni prozor je Syncfusion ribbon: kartice (npr. „Osnovni podaci“, „Komercijala“) → grupe → dugmad. Tabela ispod navodi svako dugme koje za Leget otvara formu, da li je ta forma dostupna i u novom izgledu, i tabele koje forma piše.

Tabele koje čita sam `MainP`: `Kartice` (S), `KarticeForme` (S), `KarticeKorisnik` (S), `KarticeMain` (S)

| Kartica › Grupa › Dugme | Forma | Fajl | U novom izgledu | Upisuje u tabele |
|---|---|---|---|---|
| Osnovni podaci › Prevozni šifarnici › Stanice | **frmStanice** | `Sifarnici/frmStanice.cs` | ne | `Stanice` |
| Osnovni podaci › Prevozni šifarnici › Trase | **frmTrase** | `Sifarnici/frmTrase.cs` | ne | `Trase` |
| Osnovni podaci › Prevozni šifarnici › Pruge | **frmPruge** | `Sifarnici/frmPruge.cs` | ne | `PrugaStavke` |
| Osnovni podaci › Prevozni šifarnici › Lokomotive | **frmLokomotive** | `Sifarnici/frmLokomotive.cs` | ne |  |
| Osnovni podaci › Prevozni šifarnici › Serije lokomotive | **frmSerijeLokomotiva** | `Sifarnici/frmSerijeLokomotiva.cs` | ne | `LokomotivaSerija` |
| Osnovni podaci › Prevozi › Status vozova | **frmStatusVoza** | `Sifarnici/frmStatusVoza.cs` | ne | `StatusVoza` |
| Osnovni podaci › Prevozi › Razlozi stajanja lokomotive | **frmRazlozi** | `Sifarnici/frmRazlozi.cs` | ne | `Razlozi` |
| Osnovni podaci › Prevozi › Tip prevoza | **frmTipPrevoza** | `Sifarnici/frmTipPrevoza.cs` | ne | `TipSaobPrevoza` |
| Osnovni podaci › Partneri › Partneri | **frmPartnerji** | `Sifarnici/frmPartnerji.cs` | da | `Drzave`, `Partnerji`, `Poste` |
| Komercijala › Najave › Poslovi | **frmNajava** | `Dokumenta/frmNajava.cs` | ne | `Najava`, `NajavaDeljenjeVoza`, `NajavaDodatneUsluge` |
| Komercijala › Najave › Poslovi arhiva | **frmNajava** | `Dokumenta/frmNajava.cs` | ne | `Najava`, `NajavaDeljenjeVoza`, `NajavaDodatneUsluge` |
| Komercijala › Najave › Poslovi tabla | **frmNajava20** | `Dokumenta/frmNajava20.cs` | ne |  |
| Komercijala › Najave › Svi poslovi | **frmNajavaArhivaAnaliza** | `SyncForm/frmNajavaArhivaAnaliza.cs` | ne |  |
| Komercijala › Ugovori › Dogovoreno | **frmAnalizaPorudzbina** | `SyncForm/frmAnalizaPorudzbina.cs` | ne |  |
| Komercijala › Teretnice › Teretnice | **frmTeretnicePregled** | `Dokumenta/frmTeretnicePregled.cs` | ne |  |
| Komercijala › Teretnice › Isključeni vagoni | **frmIskljuceniVagoni** | `Dokumenta/frmIskljuceniVagoni.cs` | ne |  |
| Komercijala › Teretnice › Teretnice bez najave | **frmNajaveBezTeretnice** | `Dokumenta/frmNajaveBezTeretnice.cs` | ne |  |
| Komercijala › RID › RID | **frmPregledRID** | `Dokumenta/frmPregledRID.cs` | ne |  |
| Komercijala › RID › RID po najavama | **frmRIDPoNajavama** | `Dokumenta/frmRIDPoNajavama.cs` | ne |  |
| Komercijala › RID › Teretnice bez RID-a | **frmRIDTeretnice** | `Dokumenta/frmRIDTeretnice.cs` | ne |  |
| Operatika › Tabla › Markeri | **frmMapa** | `Dokumenta/frmMapa.cs` | ne |  |
| Operatika › Prevozni nalozi › Prevoz po trasama | **frmRadniNalogPregled** | `Dokumenta/frmRadniNalogPregled.cs` | ne |  |
| Operatika › Prevozni nalozi › Prevoz po lokomotivama | **frmRAdniNalogPregledPoLokomotivama** | `Dokumenta/frmRAdniNalogPregledPoLokomotivama.cs` | ne |  |
| Operatika › Prevozni nalozi › Štampa prevoza | **frmStampaRadnogNaloga** | `Dokumenta/frmStampaRadnogNaloga.cs` | ne |  |
| Operatika › Prevozni nalozi › Raspuštanje vagoni | **frmRaspustiVagone** | `Dokumenta/frmRaspustiVagone.cs` | ne |  |
| Administrator › Dozvole KOVID › MUP dozvole | **frmMUPDozvola** | `Dokumenta/frmMUPDozvola.cs` | ne | `MUPDozvola` |
| Administrator › Dozvole KOVID › Trenutne adrese | **frmMupMesto** | `Dokumenta/frmMupMesto.cs` | ne | `Automobili`, `MUPMesto` |
| Administrator › Admin › Korisnici | **frmKorisnici** | `Administracija/frmKorisnici.cs` | da | `Korisnici` |
| Knjige › Delovodna knjiga › Novi zapis | **frmRegistator** | `Dokumenta/frmRegistator.cs` | ne | `Registrator` |
| Knjige › Delovodna knjiga › Pregled | **frmRegistratorPregled** | `Dokumenta/frmRegistratorPregled.cs` | ne |  |
| Održavanje › Lokomotive osnovno › Vrsta popisa | **frmVrstePopisa** | `Servis/frmVrstePopisa.cs` | ne | `VrstaPopisa` |
| Održavanje › Lokomotive osnovno › Popisne stavke | **frmLokomotivaVrstaPopisa** | `Servis/frmLokomotivaVrstaPopisa.cs` | ne | `LokomotiveVrstePopisa` |
| Održavanje › Lokomotive osnovno › Grupe kvarova | **frmGrupaKvarova** | `Servis/frmGrupaKvarova.cs` | ne | `GrupaKvarova` |
| Održavanje › Lokomotive osnovno › Kvarovi | **frmKvarovi** | `Servis/frmKvarovi.cs` | ne | `Kvarovi` |
| Održavanje › Obrada kvarova › Prijavljeni kvarovi | **frmEvidencijaKvarova** | `Servis/frmEvidencijaKvarova.cs` | ne |  |
| Održavanje › Obrada kvarova › Analiza kvarova | **frmEvidencijaKvarovaAnaliza** | `Servis/frmEvidencijaKvarovaAnaliza.cs` | ne |  |
| Evidencije › Vrste aktivnosti › Vrste | **frmVrsteAktivnosti** | `Sifarnici/frmVrsteAktivnosti.cs` | ne | `VrstaAktivnosti` |
| Evidencije › Vrste aktivnosti › Cene | **frmCenaPoRadniku** | `Sifarnici/frmCenaPoRadniku.cs` | ne | `EvidencijaCenaRadnik` |
| Evidencije › Vrste aktivnosti › Prava | **frmPravoAktivnosti** | `Dokumenta/frmPravoAktivnosti.cs` | ne | `PravoAktivnosti` |
| Evidencije › Vrste aktivnosti › Prava više radnika | **frmPravoAktivnostiViseRadnika** | `Dokumenta/frmPravoAktivnostiViseRadnika.cs` | ne | `PravoAktivnosti` |
| Kadrovi › Zarade › Obračun zarade | **frmIzracunZarada** | `Dokumenta/frmIzracunZarada.cs` | ne |  |
| Kadrovi › Zarade › Plaćeno/Neplaćeno | **frmPlacenoNeplaceno** | `Dokumenta/frmPlacenoNeplaceno.cs` | ne |  |
| Kadrovi › Zarade › Osnovna zarada | **frmOsnovnaZarada** | `Dokumenta/frmOsnovnaZarada.cs` | ne | `Zarada` |
| Evidencije › Prijave i odjave › Prijave/Odjave terenci tel | **frmPrijavaSmene** | `Mobile/frmPrijavaSmene.cs` | ne | `ZaposleniPrijava` |
| Evidencije › Prijave i odjave › Analiza terenci tel | **frmPregledMobilni** | `SyncForm/frmPregledMobilni.cs` | ne |  |
| Evidencije › Prijave i odjave › Prijava odjava mašinovođe | **frmPrijavaMasinovodje** | `Servis/frmPrijavaMasinovodje.cs` | ne | `LokomotivaPrijava` |
| Evidencije › Prijave i odjave › Analiza prijave odjave tablet | **frmPregledMasinovodje** | `SyncForm/frmPregledMasinovodje.cs` | ne |  |
| Održavanje › Namirenja › Obrade namirenja | **frmNamirenja** | `Servis/frmNamirenja.cs` | ne | `LokomotivaNamirenje` |
| Održavanje › Namirenja › Analize | **frmNamirenjaSumarno** | `Servis/frmNamirenjaSumarno.cs` | ne |  |
| Kadrovi › Putni nalozi › Pregled Putni nalozi | **frmPutniNaloziPregled** | `Dokumenta/frmPutniNaloziPregled.cs` | ne |  |
| Kadrovi › Putni nalozi › Neplaćeno sređivanje | **frmEvidencijaRAdaNeplaceno** | `Dokumenta/frmEvidencijaRAdaNeplaceno.cs` | ne |  |
| Kadrovi › Putni nalozi › Troškovi banaka | **frmFinansije** | `Dokumenta/frmFinansije.cs` | ne |  |
| Evidencije › Planiranje radnika › Kalendar | **frmPlanRada** | `SyncForm/frmPlanRada.cs` | ne |  |
| Evidencije › Evidencija aktivnosti › Evidencija | **frmEvidencijaRada** | `Dokumenta/frmEvidencijaRada.cs` | ne | `Aktivnosti`, `AktivnostiStavke`, `LogAktivnosti` |
| Evidencije › Evidencija aktivnosti › Izveštaj zarade | **frmZarade** | `Dokumenta/frmZarade.cs` | ne |  |
| Evidencije › Evidencija aktivnosti › Pretraga smena | **frmEvidencijaRadaZaglavlje** | `Dokumenta/frmEvidencijaRadaZaglavlje.cs` | ne | `Aktivnosti` |
| Evidencije › Evidencija aktivnosti › Pretraga aktivnosti | **frmAnalizaAktivnosti** | `SyncForm/frmAnalizaAktivnosti.cs` | ne |  |
| Kadrovi › Zarade › Parametri obrčuna | **frmParametriObracuna** | `Dokumenta/frmParametriObracuna.cs` | ne |  |
| Kadrovi › Zarade › Izveštaj zarade | **frmZarade** | `Dokumenta/frmZarade.cs` | ne |  |
| Komercijala › Ugovori › Tehnologija | **frmTehnologija** | `Tehnologija/frmTehnologija.cs` | ne | `Tehnologija`, `TehnologijaTrasaLokomotiva`, `TehnologijaTrase` |
| Osnovni podaci › Prevozni šifarnici › Trase analitički | **frmTraseAnaliticki** | `Sifarnici/frmTraseAnaliticki.cs` | ne |  |
| Komercijala › Ugovori › Tehnologije pregled | **frmTehnologijaPregled** | `Tehnologija/frmTehnologijaPregled.cs` | ne |  |
| Održavanje › Obrada kvarova › Prijava kvara | **frmPrijavaKvara** | `Servis/frmPrijavaKvara.cs` | ne | `EvidencijaKvarova` |
| Operatika › Kontrola dokumentacije › Šifarnik grešaka | **frmSifarnikKontrolnihGresaka** | `Sifarnici/frmSifarnikKontrolnihGresaka.cs` | ne | `KontrolneGreske` |
| Operatika › Kontrola dokumentacije › Kontrolna forma | **frmKontrola** | `Dokumenta/frmKontrola.cs` | ne | `KontrolaDokumentacije` |
| Operatika › Kontrola dokumentacije › Tip dokumenta | **frmSifarnikKontrolnihTipovaDokumenta** | `Sifarnici/frmSifarnikKontrolnihTipovaDokumenta.cs` | ne | `KontrolneGreskeTipDokumenta` |
| Operatika › Tabla › RN tabla | **frmCentralnaTablaRN** | `Dokumenta/frmCentralnaTablaRN.cs` | ne |  |
| Održavanje › Popis lokomotiva › Popis pregled 1 | **frmPregledPopisa1** | `Servis/frmPregledPopisa1.cs` | ne |  |
| Održavanje › Popis lokomotiva › Popis pregled 2 | **frmPregledPopisa2** | `SyncForm/frmPregledPopisa2.cs` | ne |  |
| Evidencije › Prijave i odjave › Predjena KM u Periodu | **frmPredjenaKilometrazaLokomotiva** | `Dokumenta/frmPredjenaKilometrazaLokomotiva.cs` | ne |  |
| Operatika › Tabla › Centralna tabla | **frmCentralnaTablaCpajal** | `Dokumenta/frmCentralnaTablaCpajal.cs` | ne |  |
| Održavanje › Automobili › Grupe kvarova | **frmGrupaKvarovaAuto** | `Servis/frmGrupaKvarovaAuto.cs` | ne | `GrupaKvarovaAuto` |
| Održavanje › Automobili › Kvarovi | **frmKvaroviAuto** | `Servis/frmKvaroviAuto.cs` | ne | `KvaroviAuto` |
| Evidencije › Evidencija aktivnosti › LOG promene troškova | **frmEvidencijaRadaPromene** | `Dokumenta/frmEvidencijaRadaPromene.cs` | ne |  |
| Kadrovi › Zarade › Obračun fiksni | **frmObracunFiksni** | `Dokumenta/frmObracunFiksni.cs` | ne |  |
| Održavanje › Automobili › Vozni park | **frmAutomobili** | `Dokumenta/frmAutomobili.cs` | ne | `Automobili` |
| Održavanje › Automobili › Zaduzivanje / Razduzivanje | **frmAutomobiliPregledPrijava** | `Dokumenta/frmAutomobiliPregledPrijava.cs` | ne |  |
| Održavanje › Automobili › Prijavljeni kvarovi | **frmAutomobiliKvarovi** | `Dokumenta/frmAutomobiliKvarovi.cs` | ne |  |
| Održavanje › Automobili › Prijava kvara Auto | **frmPrijavaKvaraAuto** | `Servis/frmPrijavaKvaraAuto.cs` | ne |  |
| Administrator › Dodela prava › Forme Prava | **Prava** | `Administracija/Prava.cs` | ne |  |
| Operatika › Prevozni nalozi › Pokretanje vagoni | **frmPravljenjeVoza** | `frmPravljenjeVoza.cs` | ne |  |
| Evidencije › Prijave i odjave › Tokovi dokumentacije | **frmTokoviDokumentacije** | `Mobile/frmTokoviDokumentacije.cs` | ne |  |
| Održavanje › Obrada kvarova › Lokomotive | **frmLokomotive** | `Sifarnici/frmLokomotive.cs` | ne |  |
| Komercijala › Najave › Propratnice | **frmPropratnice** | `Dokumenta/frmPropratnice.cs` | ne | `Propratnica` |
| Administrator › Dozvole mašinovođe › Položene pruge | **frmPolozenePruge** | `Administracija/frmPolozenePruge.cs` | ne |  |
| Administrator › Dozvole mašinovođe › Položene lokomotive | **frmPolozeneLokomotive** | `Administracija/frmPolozeneLokomotive.cs` | ne |  |
| Komercijala › Najave › Mail partneri | **frmMailNajava** | `Dokumenta/frmMailNajava.cs` | ne |  |
| Terminal › Šifarnici › Oštećenja | **frmGreske** | `TrackModal/Sifarnici/frmGreske.cs` | ne | `Greske` |
| Terminal › Šifarnici › Delovi kontejnera | **frmDelovi** | `TrackModal/Sifarnici/frmDelovi.cs` | ne | `Delovi` |
| Terminal › Šifarnici › Cene | **frmCene** | `TrackModal/Sifarnici/frmCene.cs` | ne | `Aktivnosti`, `Cene` |
| Terminal › Šifarnici › Tip cenovnika | **frmTipCenovnika** | `TrackModal/Sifarnici/frmTipCenovnika.cs` | ne | `TipCenovnika` |
| Terminal › Šifarnici › Partneri | **frmPartnerji** | `Sifarnici/frmPartnerji.cs` | da | `Drzave`, `Partnerji`, `Poste` |
| Terminal › Šifarnici › Način dolaska / odlaska | **frmNacinDolaskaOdlaska** | `TrackModal/Sifarnici/frmNacinDolaskaOdlaska.cs` | ne |  |
| Terminal › Šifarnici › Status kontejnera | **frmKontejnerStatus** | `Sifarnici/frmKontejnerStatus.cs` | ne |  |
| Terminal › Šifarnici › Zaposleni | **frmDelavci** | `Sifarnici/frmDelavci.cs` | ne |  |
| Terminal › Šifarnici › Vrsta robe HS | **frmVrstaRobe** | `TrackModal/Sifarnici/frmVrstaRobe.cs` | ne | `VrstaRobe` |
| Terminal › Šifarnici › Tip kontejnera | **frmTipKontejnera** | `TrackModal/Sifarnici/frmTipKontejnera.cs` | da | `TipKontenjera` |
| Terminal › Šifarnici › Sredstva rada | **frmVozila** | `TrackModal/Dokumeta/frmVozila.cs` | ne | `Vozila` |
| Terminal › Šifarnici › Stanice | **frmStanice** | `Sifarnici/frmStanice.cs` | ne | `Stanice` |
| Terminal › Šifarnici › Pozicije | **frmPozicija** | `TrackModal/Sifarnici/frmPozicija.cs` | da | `Pozicija` |
| Terminal › Šifarnici › Skladista | **frmSkladista** | `TrackModal/Sifarnici/frmSkladista.cs` | da | `Skladista` |
| Terminal › Šifarnici › Predefinisane poruke - treba | **frmVrstePostupakaUvoz** | `Uvoz/frmVrstePostupakaUvoz.cs` | ne | `VrstePostupakaUvoz` |
| Terminal › Prevoz › Pregled vozova | **frmPregledVozova** | `TrackModal/Dokumeta/frmPregledVozova.cs` | da |  |
| Terminal › Prevoz › Vozovi | **frmVoz** | `TrackModal/Dokumeta/frmVoz.cs` | da | `IzvozKonacnaZaglavlje`, `UvozKonacna`, `Voz` |
| Terminal › Prevoz › Prijem kontejnera kamionom pregled | **frmPrijemKontejneraKamionPregled** | `TrackModal/Dokumeta/frmPrijemKontejneraKamionPregled.cs` | ne |  |
| Terminal › Prevoz › Prijem kontejnera vozom pregled | **frmPrijemVozomPregled** | `TrackModal/Dokumeta/frmPrijemVozomPregled.cs` | da |  |
| Terminal › Prevoz › Otprema kontejnera vozom | **frmPregledOtpreme** | `TrackModal/Dokumeta/frmPregledOtpreme.cs` | da |  |
| Terminal › Prevoz › Otprema kontejnera kamionom | **frmPregledOtpremeKamionom** | `TrackModal/Dokumeta/frmPregledOtpremeKamionom.cs` | ne |  |
| Terminal › Prevoz › Pregled nalozi za prevoz | **frmPregledNaloziZaPrevoz** | `TrackModal/Dokumeta/frmPregledNaloziZaPrevoz.cs` | ne |  |
| Terminal › Prevoz › Pregled putni nalozi | **frmPutniNalog** | `Dokumenta/frmPutniNalog.cs` | da | `PutniNalogGorivo`, `PutniNalogKvarovi` |
| Terminal › Prevoz › Pregled tovarni listovi | **frmPregledTovarnihListova** | `TrackModal/Dokumeta/frmPregledTovarnihListova.cs` | ne |  |
| Terminal › Terminalske usluge › Raspored manipulacija | **frmManipulacije** | `Dokumenta/frmManipulacije.cs` | da | `NaruceneManipulacije` |
| Terminal › Terminalske usluge › Pregled manipulacija po prevozu | **frmPregledNarucenihManipulacija** | `Dokumenta/frmPregledNarucenihManipulacija.cs` | da | `NaruceneManipulacije` |
| Terminal › Terminalske usluge › Pregled manipulacija po partneru | **frmPregledManipulacijaPoPartneru** | `Izvestaji/frmPregledManipulacijaPoPartneru.cs` | ne |  |
| Terminal › Skladišno poslovanje › Pregled prijema raspored | **frmPregledSkladistePrijem** | `Promet/frmPregledSkladistePrijem.cs` | ne |  |
| Terminal › Skladišno poslovanje › Prijem raspored | **frmSkladistePrijem** | `TrackModal/Promet/frmSkladistePrijem.cs` | ne | `Promet`, `PrometManipulacije` |
| Terminal › Skladišno poslovanje › Pregled međuskladišni po prijemu | **frmPregledMedjuskladisniPrenos** | `Promet/frmPregledMedjuskladisniPrenos.cs` | ne |  |
| Terminal › Skladišno poslovanje › Međuskladišni prenos | **frmMedjuskladisniPrenos** | `Promet/frmMedjuskladisniPrenos.cs` | ne |  |
| Terminal › Skladišno poslovanje › Otprema usluga | **frmSkladisteOtprema** | `Promet/frmSkladisteOtprema.cs` | ne |  |
| Terminal › Skladišno poslovanje › Pregled prometa | **frmPrometKontejnera** | `Promet/frmPrometKontejnera.cs` | ne |  |
| Terminal › Skladišno poslovanje › Pregled stanja zaliha | **frmLager** | `Promet/frmLager.cs` | ne |  |
| Terminal › Skladišno poslovanje › Popisna lista | **frmPopis** | `Promet/frmPopis.cs` | ne | `Popis`, `PopisStavke` |
| Terminal › Skladišno poslovanje › Pregled popisnih lista | **frmPopisPregled** | `Promet/frmPopisPregled.cs` | ne |  |
| Terminal › Skladišno poslovanje › Pregled stanja zaliha - Magacioner | **frmLagerOperater** | `Promet/frmLagerOperater.cs` | da |  |
| Terminal › Transport › Pregled nalozi za prevoz | **frmPregledNaloziZaPrevoz** | `TrackModal/Dokumeta/frmPregledNaloziZaPrevoz.cs` | ne |  |
| Terminal › Transport › Nalozi za prevoz | **frmNalogZaPrevoz** | `Dokumenta/frmNalogZaPrevoz.cs` | da | `NajavaPrevozaTroskovi` |
| Terminal › Transport › Pregled puti nalozi | **frmPregledPutnihNaloga** | `TrackModal/Dokumeta/frmPregledPutnihNaloga.cs` | ne |  |
| Terminal › Transport › Putni nalozi | **frmPutniNalog** | `Dokumenta/frmPutniNalog.cs` | da | `PutniNalogGorivo`, `PutniNalogKvarovi` |
| Terminal › Transport › Pregled radni nalozi transport | **frmPregledRadniNaloziTransport** | `TrackModal/Dokumeta/frmPregledRadniNaloziTransport.cs` | ne |  |
| Terminal › Transport › Radni nalozi transport | **frmRadniNalogTransport** | `Dokumenta/frmRadniNalogTransport.cs` | da |  |
| Terminal › Transport › Autoprevoznički list | **frmAutoprevozniList2** | `Dokumenta/frmAutoprevozniList2.cs` | da | `Autoprevoznilist` |
| Terminal › Transport › Pregled autoprevoznički list | **frmPregledAutoPrevozniList** | `TrackModal/Dokumeta/frmPregledAutoPrevozniList.cs` | ne |  |
| Terminal › Transport › Izveštaji › Kvarovi | **frmSelectTransport6** | `Izvestaji/frmSelectTransport6.cs` | ne |  |
| Terminal › Transport › Izveštaji › Vozači po rutama | **frmSelectTransport5** | `Izvestaji/frmSelectTransport5.cs` | ne |  |
| Terminal › Transport › Izveštaji › Vozila po PN | **frmSelectTransport4** | `Izvestaji/frmSelectTransport4.cs` | ne |  |
| Terminal › Transport › Izveštaji › Ture po relaciji | **frmSelectTransport3** | `Izvestaji/frmSelectTransport3.cs` | ne |  |
| Terminal › Transport › Izveštaji › Po tipu kontejnera | **frmSelectTransport2** | `Izvestaji/frmSelectTransport2.cs` | ne |  |
| Terminal › Transport › Izveštaji › Kameni agregat | **frmSelectTransport1** | `Izvestaji/frmSelectTransport1.cs` | ne |  |
| Komercijala › Ugovori › Matični podaci | **frmMaticniPodatki** | `Sifarnici/frmMaticniPodatki.cs` | ne | `MAticniPodatki` |
| Komercijala › Ugovori › Prodajne grupe | **frmProdajnaGrupa** | `Sifarnici/frmProdajnaGrupa.cs` | ne | `ProdSkup` |
| Komercijala › Ugovori › Jedinice mere | **frmJediniceMere** | `Sifarnici/frmJediniceMere.cs` | ne | `MerskeEnote` |
| Komercijala › Ugovori › Unos ugovora | **frmDogovori** | `Dokumenta/frmDogovori.cs` | ne | `Narocilo` |
| Komercijala › Ugovori › Tip saobraćajnog prevoza | **frmObjekt** | `Sifarnici/frmObjekt.cs` | ne | `TipSaobPrevoza` |
| Komercijala › Ugovori › Načini isporuke | **frmNacinIsporuke** | `Sifarnici/frmNacinIsporuke.cs` | ne | `NacinDobave` |
| Osnovni podaci › Partneri › Kontakt osobe | **frmKontaktOsobe** | `Dokumenta/frmKontaktOsobe.cs` | da |  |
| Kadrovi › Osnovni podaci › Radna mesta | **frmDelovnaMesta** | `Sifarnici/frmDelovnaMesta.cs` | ne | `DelovnaMesta` |
| Kadrovi › Osnovni podaci › Radnici | **frmDelavci** | `Sifarnici/frmDelavci.cs` | ne |  |
| Kadrovi › Osnovni podaci › Bolovanja | **frmBolovanja** | `Dokumenta/frmBolovanja.cs` | ne | `Bolovanje` |
| Kadrovi › Osnovni podaci › Rad noću/Prekovremeni | **frmPrekovremeniRad** | `Dokumenta/frmPrekovremeniRad.cs` | ne | `PrekovremeniRad` |
| Kadrovi › Godisnji odmori › Pristigli zahtevi | **frmSlobodniDani** | `Mobile/frmSlobodniDani.cs` | ne | `EvidencijaZahteva` |
| Kadrovi › Godisnji odmori › Evidencija GO | **frmEvidencijaGodišnjihOdmora** | `Dokumenta/frmEvidencijaGodišnjihOdmora.cs` | ne | `1ReportGO`, `DopustStavke` |
| Kadrovi › Godisnji odmori › Analiza GO | **frmAnalizaGO** | `Mobile/frmAnalizaGO.cs` | ne |  |
| Kadrovi › Godisnji odmori › Analiza GO 2 | **frmAnalizaGOSum** | `Mobile/frmAnalizaGOSum.cs` | ne |  |
| Kadrovi › Testiranje radnika › Formiranje testa | **Pitanja** | `Testiranje/Pitanja.cs` | ne |  |
| Kadrovi › Testiranje radnika › Generisanje testa | **frmGenerisanjeTestaKorisnik** | `Testiranje/frmGenerisanjeTestaKorisnik.cs` | ne |  |
| Kadrovi › Testiranje radnika › Štampa rezultata | **TestiranjeStampa** | `Testiranje/TestiranjeStampa.cs` | ne |  |
| Kadrovi › Testiranje radnika › Posebni obrasci | **Obrasci** | `Testiranje/Obrasci.cs` | ne |  |
| Operatika › Komunikacija › Telegrami | **frmTelegrami** | `Sifarnici/frmTelegrami.cs` | ne | `telegrami` |
| Operatika › Komunikacija › Obaveštenja | **frmNotifikacije** | `Administracija/frmNotifikacije.cs` | ne | `Notifikacije` |
| Operatika › Kontrola dokumentacije › Završna dokumentacija | **frmZavrsnaDokumentacija** | `Mobile/frmZavrsnaDokumentacija.cs` | ne |  |
| Operatika › Komunikacija › Plombe | **frmPlombe** | `Servis/frmPlombe.cs` | ne | `Plombe` |
| Osnovni podaci › Prevozi › NHM | **frmNHM** | `Sifarnici/frmNHM.cs` | ne | `NHM` |
| Kadrovi › Testiranje radnika › Evidencija poznavanja pruga i lokomotiva | **frmOsoblje** | `Sifarnici/frmOsoblje.cs` | ne |  |
| Osnovni podaci › Prevozi › Tipovi telegrama | **frmTipTelegrama** | `Sifarnici/frmTipTelegrama.cs` | ne | `TipTelegrama` |
| Komercijala › Ugovori › Pregled dogovora | **frmDogovoriPregled** | `Dokumenta/frmDogovoriPregled.cs` | ne |  |
| Uvoz › Uvoz obrada › Neraspoređeni kontejneri - Obrazac | **Uvoz** | `Uvoz/Uvoz.cs` | ne | `RadniNalogInterni`, `Uvoz`, `UvozKonacna`, `UvozNapomenePozicioniranja`, `UvozNHM`, `UvozVrstaManipulacije`, `UvozVrstaRobeHS` |
| Uvoz › Uvoz obrada › Neraspoređen kontejneri tabela | **frmPregledNerasporedjeni** | `Uvoz/frmPregledNerasporedjeni.cs` | ne | `Uvoz` |
| Uvoz › Uvoz obrada › Vozovi obrazac unosa | **frmVoz** | `TrackModal/Dokumeta/frmVoz.cs` | da | `IzvozKonacnaZaglavlje`, `UvozKonacna`, `Voz` |
| Uvoz › Uvoz obrada › Planovi utovara | **frmPregledPlanovaUtovara** | `Uvoz/frmPregledPlanovaUtovara.cs` | ne |  |
| Uvoz › Uvoz obrada › Popunjavanje plana kontejnerima | **frmFormiranjePlana** | `Uvoz/frmFormiranjePlana.cs` | da |  |
| Uvoz › Uvoz obrada › Uvoz Excel - HŽ cargo | **frmPlanUtovaraOdgovorExcel** | `Uvoz/frmPlanUtovaraOdgovorExcel.cs` | ne |  |
| Uvoz › Uvoz obrada › Prenos Plana utovara u Prijem voza Terminal | **frmPrijemVozaIzPlana** | `Uvoz/frmPrijemVozaIzPlana.cs` | da |  |
| Uvoz › Uvoz obrada › Pregled operacija nad kontejnerima | **frmAnalizaUvoza** | `Uvoz/frmAnalizaUvoza.cs` | ne |  |
| Uvoz › Uvoz obrada › Prebacivanje kontejnera iz plana u plan | **frmPrebacivanjeIzPlanaUPlan** | `Uvoz/frmPrebacivanjeIzPlanaUPlan.cs` | ne |  |
| Izvoz › Zadaci › Unos RN obrazac | **frmRadniNaloziInterni** | `Uvoz/frmRadniNaloziInterni.cs` | ne | `RadniNalogInterni` |
| Izvoz › Zadaci › Pregled radnih naloga | **frmRadniNalogInterniPregled** | `Uvoz/frmRadniNalogInterniPregled.cs` | da | `RadniNalogInterniPotvrda` |
| Uvoz › Uvoz obrada › Popunjavanje plana DRUMSKI | **frmForrmiranjePlanaDrumski** | `Uvoz/frmForrmiranjePlanaDrumski.cs` | ne | `RadniNalogInterni` |
| Uvoz › Uvoz obrada › Svi planovi utovara -Drumski | **frmPregledPlanoviUtovaraDrumski** | `Uvoz/frmPregledPlanoviUtovaraDrumski.cs` | ne |  |
| Izvoz › Izvoz › Nerasporedjeni kontejneri tabela | **frmPregledKontejneraIzvoz** | `Izvoz/frmPregledKontejneraIzvoz.cs` | ne |  |
| Izvoz › Izvoz › Planovi utovara / pakovanja voza | **frmPlanoviIzvoza** | `Izvoz/frmPlanoviIzvoza.cs` | ne |  |
| Komercijala › Najave › Train list | **frmTrainList** | `Dokumenta/frmTrainList.cs` | ne | `this` |
| Osnovni podaci › Prevozni šifarnici › Serije Vagoni | **frmVagoniSerije** | `Sifarnici/frmVagoniSerije.cs` | ne | `VagoniSerije` |
| Izvoz › Izvoz › Vozovi obrazac unosa | **frmVoz** | `TrackModal/Dokumeta/frmVoz.cs` | da | `IzvozKonacnaZaglavlje`, `UvozKonacna`, `Voz` |
| Izvoz › Izvoz › Popunjavanje plana kontejnerima | **frmFormiranjePlanaIzvoz** | `Izvoz/frmFormiranjePlanaIzvoz.cs` | da | `Izvoz`, `IzvozKonacna` |
| Izvoz › Izvoz › Prebacivanje iz plana u plan | **frmPrebacivanjeIzPlanaUPlan** | `Izvoz/frmPrebacivanjeIzPlanaUPlan.cs` | ne |  |
| Evidencije › Vrste aktivnosti › Sklop kola | **frmGrupaNepravilnosti** | `Neispravnosti/frmGrupaNepravilnosti.cs` | ne | `SifGrupaNepravilnosti` |
| Evidencije › Vrste aktivnosti › Kod/Opis/Postupak | **frmNeispravnostPostupak** | `Neispravnosti/frmNeispravnostPostupak.cs` | ne | `SifNeispravnostPostupak` |
| Evidencije › Vrste aktivnosti › Opis neispravnosti | **frmOpisNeispravnosti** | `Neispravnosti/frmOpisNeispravnosti.cs` | ne | `SifOpisNeispravnosti` |
| Evidencije › Vrste aktivnosti › Kolski deo | **frmRazredNepravilnosti** | `Neispravnosti/frmRazredNepravilnosti.cs` | ne | `SifRazredNepravilnosti` |
| Evidencije › Vrste aktivnosti › Vrsta nepravilnosti | **frmVrstaNepravilnosti** | `Neispravnosti/frmVrstaNepravilnosti.cs` | ne | `SifVrstaNepravilnosti` |
| Evidencije › Evidencija aktivnosti › Pregled trenutnih aktivnosti | **frmPregledAktivnosti** | `Dokumenta/frmPregledAktivnosti.cs` | ne |  |
| Osnovni podaci › Prevozi › Tipovi kontejnera | **frmTipKontejnera** | `TrackModal/Sifarnici/frmTipKontejnera.cs` | da | `TipKontenjera` |
| Osnovni podaci › Prevozni šifarnici › Vuča statusi | **frmVucaStatusi** | `Dokumenta/frmVucaStatusi.cs` | ne |  |
| Osnovni podaci › Prevozni šifarnici › Stavke primopredaje | **frmStavkePrimopredaje** | `Administracija/frmStavkePrimopredaje.cs` | ne |  |
| Terminal › Prevoz › Plan otpreme kontejnera vozom | **frmPregledPlanovaUtovara** | `Uvoz/frmPregledPlanovaUtovara.cs` | ne |  |
| Terminal › Prevoz › Plan otpreme kontejnera | **frmPlanoviIzvoza** | `Izvoz/frmPlanoviIzvoza.cs` | ne |  |
| Terminal › Šifarnici › Organizacione jedinice | **frmOrganizacionaJedinica** | `TrackModal/Sifarnici/frmOrganizacionaJedinica.cs` | ne |  |
| Terminal › Šifarnici › Kvalitet kontejnera | **frmuvKvalitetKontejnera** | `Uvoz/frmuvKvalitetKontejnera.cs` | ne | `uvKvalitetKontejnera` |
| Evidencije › Prijave i odjave › Prijave odjave stari pogled | **frmPrijavaSmeneOld** | `Mobile/frmPrijavaSmeneOld.cs` | ne | `ZaposleniPrijava` |
| Evidencije › Prijave i odjave › Prijava odjava masinovodje stari pregled | **frmPrijavaMasinovodjeOld** | `Servis/frmPrijavaMasinovodjeOld.cs` | ne | `LokomotivaPrijava` |
| Izvoz › Šifarnici › Vrsta robe HS | **frmVrstaRobeHS** | `Uvoz/frmVrstaRobeHS.cs` | ne | `VrstaRobeADR`, `VrstaRobeHS` |
| Uvoz › Šifarnici › Vrsta robe HS | **frmVrstaRobeHS** | `Uvoz/frmVrstaRobeHS.cs` | ne | `VrstaRobeADR`, `VrstaRobeHS` |
| Uvoz › Šifarnici › Partneri | **frmPartnerji** | `Sifarnici/frmPartnerji.cs` | da | `Drzave`, `Partnerji`, `Poste` |
| Izvoz › Šifarnici › Partneri | **frmPartnerji** | `Sifarnici/frmPartnerji.cs` | da | `Drzave`, `Partnerji`, `Poste` |
| Izvoz › Šifarnici › Tip cenovnika | **frmTipCenovnika** | `TrackModal/Sifarnici/frmTipCenovnika.cs` | ne | `TipCenovnika` |
| Uvoz › Šifarnici › Tip cenovnika | **frmTipCenovnika** | `TrackModal/Sifarnici/frmTipCenovnika.cs` | ne | `TipCenovnika` |
| Terminal › Šifarnici › Vrste usluga | **frmVrstaManipulacije** | `TrackModal/Sifarnici/frmVrstaManipulacije.cs` | ne | `VrstaManipulacije` |
| Izvoz › Šifarnici › Vrste usluga | **frmVrstaManipulacije** | `TrackModal/Sifarnici/frmVrstaManipulacije.cs` | ne | `VrstaManipulacije` |
| Uvoz › Šifarnici › Cene | **frmCene** | `TrackModal/Sifarnici/frmCene.cs` | ne | `Aktivnosti`, `Cene` |
| Izvoz › Šifarnici › Cene | **frmCene** | `TrackModal/Sifarnici/frmCene.cs` | ne | `Aktivnosti`, `Cene` |
| Izvoz › Šifarnici › Brodovi | **Brodovi** | `Uvoz/Brodovi.cs` | ne | `Brodovi` |
| Uvoz › Šifarnici › Brodovi | **Brodovi** | `Uvoz/Brodovi.cs` | ne | `Brodovi` |
| Uvoz › Šifarnici › Carinarnice | **Carinarnice** | `Uvoz/Carinarnice.cs` | ne | `Carinarnice` |
| Izvoz › Šifarnici › Carinarnice | **Carinarnice** | `Uvoz/Carinarnice.cs` | ne | `Carinarnice` |
| Terminal › Šifarnici › Serije kola | **frmSerijeKola** | `Uvoz/frmSerijeKola.cs` | ne | `SerijeKola` |
| Izvoz › Šifarnici › Serije kola | **frmSerijeKola** | `Uvoz/frmSerijeKola.cs` | ne | `SerijeKola` |
| Izvoz › Šifarnici › Kontejnerski terminali | **frmKontejnerskiTerminali** | `Uvoz/frmKontejnerskiTerminali.cs` | ne | `KontejnerskiTerminali` |
| Terminal › Šifarnici › Kontejnerski terminali | **frmKontejnerskiTerminali** | `Uvoz/frmKontejnerskiTerminali.cs` | ne | `KontejnerskiTerminali` |
| Terminal › Šifarnici › Vrsta robe ADR | **frmVrstaRobeADR** | `Uvoz/frmVrstaRobeADR.cs` | ne | `VrstaRobeADR` |
| Izvoz › Šifarnici › Vrsta robe ADR | **frmVrstaRobeADR** | `Uvoz/frmVrstaRobeADR.cs` | ne | `VrstaRobeADR` |
| Izvoz › Šifarnici › NHM | **frmNHM** | `Sifarnici/frmNHM.cs` | ne | `NHM` |
| Terminal › Šifarnici › NHM | **frmNHM** | `Sifarnici/frmNHM.cs` | ne | `NHM` |
| Terminal › Šifarnici › Vrsta Carinskog postupka | **frmVrstaCarinskogPostupka** | `Uvoz/frmVrstaCarinskogPostupka.cs` | ne | `VrstaCarinskogPostupka` |
| Izvoz › Šifarnici › Vrsta Carinskog postupka | **frmVrstePostupakaUvoz** | `Uvoz/frmVrstePostupakaUvoz.cs` | ne | `VrstePostupakaUvoz` |
| Izvoz › Šifarnici › Način pakovanja | **fruvNacinPakovanja** | `Uvoz/fruvNacinPakovanja.cs` | ne | `uvNacinPakovanja` |
| Terminal › Šifarnici › Način pakovanja | **fruvNacinPakovanja** | `Uvoz/fruvNacinPakovanja.cs` | ne | `uvNacinPakovanja` |
| Uvoz › Šifarnici › Kvalitet kontejnera | **frmuvKvalitetKontejnera** | `Uvoz/frmuvKvalitetKontejnera.cs` | ne | `uvKvalitetKontejnera` |
| Izvoz › Šifarnici › Kvalitet kontejnera | **frmuvKvalitetKontejnera** | `Uvoz/frmuvKvalitetKontejnera.cs` | ne | `uvKvalitetKontejnera` |
| Izvoz › Šifarnici › Tip kontejnera | **frmTipKontejnera** | `TrackModal/Sifarnici/frmTipKontejnera.cs` | da | `TipKontenjera` |
| Uvoz › Šifarnici › Tip kontejnera | **frmTipKontejnera** | `TrackModal/Sifarnici/frmTipKontejnera.cs` | da | `TipKontenjera` |
| Terminal › Šifarnici › Postupak sa robom/Kontejnerom | **frmVrstePostupakaUvoz** | `Uvoz/frmVrstePostupakaUvoz.cs` | ne | `VrstePostupakaUvoz` |
| Izvoz › Šifarnici › Postupak sa robom/Kontejnerom | **frmVrstePostupakaUvoz** | `Uvoz/frmVrstePostupakaUvoz.cs` | ne | `VrstePostupakaUvoz` |
| Izvoz › Izvoz › Pregled vozova | **frmPregledVozova** | `TrackModal/Dokumeta/frmPregledVozova.cs` | da |  |
| Uvoz › Uvoz obrada › Pregled vozova | **frmPregledVozova** | `TrackModal/Dokumeta/frmPregledVozova.cs` | da |  |
| Terminal › Šifarnici › Dirigacija kontejnera za | **frmDirigacijaKontejneraZa** | `Uvoz/frmDirigacijaKontejneraZa.cs` | ne | `DirigacijaKontejneraZa` |
| Izvoz › Šifarnici › Dirigacija kontejnera za | **frmDirigacijaKontejneraZa** | `Uvoz/frmDirigacijaKontejneraZa.cs` | ne | `DirigacijaKontejneraZa` |
| Izvoz › Šifarnici › Napomene za pozicioniranje | **frmNapomenaZaPozicioniranje** | `Uvoz/frmNapomenaZaPozicioniranje.cs` | ne | `NapomenaZaPozicioniranje` |
| Terminal › Šifarnici › Napomene za pozicioniranje | **frmNapomenaZaPozicioniranje** | `Uvoz/frmNapomenaZaPozicioniranje.cs` | ne | `NapomenaZaPozicioniranje` |
| Uvoz › Šifarnici › Organizacione jedinice | **frmOrganizacionaJedinica** | `TrackModal/Sifarnici/frmOrganizacionaJedinica.cs` | ne |  |
| Izvoz › Šifarnici › Organizacione jedinice | **frmOrganizacionaJedinica** | `TrackModal/Sifarnici/frmOrganizacionaJedinica.cs` | ne |  |
| Izvoz › Šifarnici › Krajnje destinacije | **KrajnjeDestinacije** | `Sifarnici/KrajnjeDestinacije.cs` | ne | `KrajnjaDestinacija` |
| Uvoz › Šifarnici › Krajnje destinacije | **KrajnjeDestinacije** | `Sifarnici/KrajnjeDestinacije.cs` | ne | `KrajnjaDestinacija` |
| Terminal › Šifarnici › Mesta utovara | **frmMestaUtovara** | `Izvoz/frmMestaUtovara.cs` | ne | `MestaUtovara` |
| Izvoz › Šifarnici › Mesta utovara | **frmMestaUtovara** | `Izvoz/frmMestaUtovara.cs` | ne | `MestaUtovara` |
| Izvoz › Šifarnici › Kontakt osobe Mesta utovara | **frmKontaktOsobeMU** | `Izvoz/frmKontaktOsobeMU.cs` | da |  |
| Terminal › Šifarnici › Kontakt osobe Mesta utovara | **frmKontaktOsobeMU** | `Izvoz/frmKontaktOsobeMU.cs` | da |  |
| Uvoz › Šifarnici › Adrese statusa vozila | **frmAdresaStatusVozila** | `Izvoz/frmAdresaStatusVozila.cs` | ne | `AdresaStatusVozila` |
| Izvoz › Šifarnici › Adrese statusa vozila | **frmAdresaStatusVozila** | `Izvoz/frmAdresaStatusVozila.cs` | ne | `AdresaStatusVozila` |
| Izvoz › Šifarnici › Naslovi statusa vozila | **frmNaslovStatusaVozila** | `Izvoz/frmNaslovStatusaVozila.cs` | ne | `NaslovStatusaVozila` |
| Uvoz › Šifarnici › Naslovi statusa vozila | **frmNaslovStatusaVozila** | `Izvoz/frmNaslovStatusaVozila.cs` | ne | `NaslovStatusaVozila` |
| Terminal › Šifarnici › Inspekciski tretman | **frmInspekciskiTretman** | `Izvoz/frmInspekciskiTretman.cs` | ne | `InspekciskiTretman` |
| Izvoz › Šifarnici › Inspekciski tretman | **frmInspekciskiTretman** | `Izvoz/frmInspekciskiTretman.cs` | ne | `InspekciskiTretman` |
| Izvoz › Šifarnici › Valute | **frmValute** | `Izvoz/frmValute.cs` | ne | `Valute` |
| Uvoz › Šifarnici › Valute | **frmValute** | `Izvoz/frmValute.cs` | ne | `Valute` |
| Terminal › Šifarnici › Vrste plombi | **frmVrstePlombi** | `Izvoz/frmVrstePlombi.cs` | ne | `VrstePlombi` |
| Izvoz › Šifarnici › Vrste plombi | **frmVrstePlombi** | `Izvoz/frmVrstePlombi.cs` | ne | `VrstePlombi` |
| Izvoz › Izvoz › Nerasporedjeni kontejneri Obrazac | **frmIzvoz** | `Izvoz/frmIzvoz.cs` | ne | `Izvoz`, `IzvozKonacnaVrstaManipulacije`, `IzvozNHM`, `IzvozVrstaRobeHS` |
| Terminal › Šifarnici › Grupe usluga | **frmGrupaVrsteManipulacije** | `TrackModal/Sifarnici/frmGrupaVrsteManipulacije.cs` | ne | `GrupaVrsteManipulacije` |
| Terminal › Šifarnici › Carinarnice | **Carinarnice** | `Uvoz/Carinarnice.cs` | ne | `Carinarnice` |
| Uvoz › Šifarnici › Grupe usluga | **frmGrupaVrsteManipulacije** | `TrackModal/Sifarnici/frmGrupaVrsteManipulacije.cs` | ne | `GrupaVrsteManipulacije` |
| Izvoz › Šifarnici › Grupe usluga | **frmGrupaVrsteManipulacije** | `TrackModal/Sifarnici/frmGrupaVrsteManipulacije.cs` | ne | `GrupaVrsteManipulacije` |
| Evidencije › Evidencija aktivnosti › Troskovi po smeni / Slike | **frmEvidencijaRadaDokumenti** | `Dokumenta/frmEvidencijaRadaDokumenti.cs` | ne | `AktivnostiDokumenta` |
| Evidencije › Pregledi › Analiza aktivnosti po poslu | **frmPregledMobilni2** | `SyncForm/frmPregledMobilni2.cs` | ne |  |
| Evidencije › Pregledi › Pregled po Lokomotivi | **PoLokomotivama** | `ReportingOperatika/PoLokomotivama.cs` | ne |  |
| Evidencije › Pregledi › Pregled mobilnim | **frmPregledMobilni2** | `SyncForm/frmPregledMobilni2.cs` | ne |  |
| Evidencije › Pregledi › Pregled masinovodje 2 | **frmPregledMasinovodje2** | `SyncForm/frmPregledMasinovodje2.cs` | ne |  |
| Komercijala › eDokumenta › eFakture | **EFaktura** | `EDokumenta/EFaktura.cs` | ne |  |
| Komercijala › eDokumenta › eOdobrenja | **EOdobrenje** | `EDokumenta/EOdobrenje.cs` | ne |  |
| Komercijala › eDokumenta › eZaduženja | **EZaduzenje** | `EDokumenta/EZaduzenje.cs` | ne |  |
| Komercijala › eDokumenta › eAvansni | **EAvansni** | `EDokumenta/EAvansni.cs` | ne |  |
| Komercijala › eDokumenta › Poslata dokumenta | **PoslataDokumenta** | `EDokumenta/PoslataDokumenta.cs` | ne |  |
| Komercijala › eDokumenta › Primljena dokumenta | **PrimljenaDokumenta** | `EDokumenta/PrimljenaDokumenta.cs` | ne |  |
| Terminal › Radni nalozi | **RN1PrijemVoza** | `RadniNalozi/RN1PrijemVoza.cs` | da | `RNPrijemVoza` |
| Terminal › Radni nalozi › RN2 - OTPREMA VOZA | **RN2OtpremaVoza** | `RadniNalozi/RN2OtpremaVoza.cs` | da | `RNOtpremaVoza` |
| Terminal › Radni nalozi › RN1 - PRIJEM VOZA | **RN1PrijemVoza** | `RadniNalozi/RN1PrijemVoza.cs` | da | `RNPrijemVoza` |
| Terminal › Radni nalozi › RN4 - PRIJEM PLATFORME | **RN4PrijemPlatforme** | `RadniNalozi/RN4PrijemPlatforme.cs` | da | `RNPrijemPlatforme` |
| Terminal › Radni nalozi › RN5 - PRIJEM PLAT BRODAR | **RN5PrijemPlatforme2** | `RadniNalozi/RN5PrijemPlatforme2.cs` | da | `RNPrijemPlatforme2` |
| Terminal › Radni nalozi › RN6 - OTPREMA PLATFORME | **RN6OtpremaPlatforme** | `RadniNalozi/RN6OtpremaPlatforme.cs` | da | `RNOtpremaPlatforme` |
| Terminal › Radni nalozi › RN7 - OTP PLAT BRODAR | **RN7OtpremaPlatforme2** | `RadniNalozi/RN7OtpremaPlatforme2.cs` | da | `RNOtpremaPlatforme2` |
| Terminal › Radni nalozi › RN8 - OTPREMA CIRADE | **RN8OtpremaCirade** | `RadniNalozi/RN8OtpremaCirade.cs` | ne | `RNOtpremaCirade` |
| Terminal › Radni nalozi › RN9 - PRIJEM CIRADE | **RN9PrijemCirade** | `RadniNalozi/RN9PrijemCirade.cs` | ne | `RNPrijemCirade` |
| Terminal › Radni nalozi › Pregled ispraznjenog kontejnera - N | **RN10PregledIspraznjenogKontejnera** | `RadniNalozi/RN10PregledIspraznjenogKontejnera.cs` | ne | `RNPregledIspraznjenogKontejnera` |
| Terminal › Radni nalozi › Pregled i postavka kontejnera - N | **RN11PreglediPostavkaKontejnera** | `RadniNalozi/RN11PreglediPostavkaKontejnera.cs` | ne | `RNPregledIpostavkaKontejnera` |
| Terminal › Radni nalozi › RN12 - INTERNI PRENOS | **RN12MedjuskladisniKontejnera** | `RadniNalozi/RN12MedjuskladisniKontejnera.cs` | da | `RNMedjuskladisni` |
| Komercijala › RID › RID | **frmPregledRID** | `Dokumenta/frmPregledRID.cs` | ne |  |
| Komercijala › RID › RID PO POSLOVIMA | **frmRIDPoNajavama** | `Dokumenta/frmRIDPoNajavama.cs` | ne |  |
| Komercijala › RID › RID TERETNICE | **frmRIDTeretnice** | `Dokumenta/frmRIDTeretnice.cs` | ne |  |
| Terminal › Radni nalozi › Prijem kontejnera kamion - N | **frmPrijemKontejneraKamionLegetUvoz** | `Dokumenta/frmPrijemKontejneraKamionLegetUvoz.cs` | da | `Promet`, `Uvoz` |
| Terminal › Radni nalozi › Prijem kontejnera voz - N | **frmPrijemKontejneraLegetVozUvoz** | `Dokumenta/frmPrijemKontejneraLegetVozUvoz.cs` | da | `Promet` |
| Terminal › Radni nalozi › Otprema kontejnera Kamion - N | **frmOtpremaKontejneraUvozKamion** | `Dokumenta/frmOtpremaKontejneraUvozKamion.cs` | da |  |
| Terminal › Radni nalozi › Prijem kontejenra kamion - N | **frmPrijemKontejneraKamionLegetIzvoz** | `Dokumenta/frmPrijemKontejneraKamionLegetIzvoz.cs` | da |  |
| Terminal › Radni nalozi › Otprema kontejnera voz - N | **frmOtpremaKontejneraLegetIZVOZ** | `Dokumenta/frmOtpremaKontejneraLegetIZVOZ.cs` | da | `OtpremaKontejnera`, `OtpremaKontejneraVozStavke` |
| Terminal › Radni nalozi › Otprema kontejenera Kamion - N | **frmOtpremaKontejneraIzvozKamion** | `Dokumenta/frmOtpremaKontejneraIzvozKamion.cs` | da | `OtpremaKontejnera`, `OtpremaKontejneraVozStavke` |
| Komercijala › CRM › Nosioci troškova | **NosiociTroskova** | `Pantheon Export/NosiociTroskova.cs` | ne | `ApiLogovi`, `NosiociTroskova` |
| Komercijala › CRM › Predviđanje | **Predvidjanje** | `Pantheon Export/Predvidjanje.cs` | ne | `ApiLogovi`, `Predvidjanje` |
| Komercijala › CRM › Ulazne fakture pregled | **UlFakPregled** | `Pantheon Export/UlFakPregled.cs` | ne | `ApiLogovi`, `UlFak` |
| Komercijala › CRM › Ulazne fakture | **UlazneFakture** | `Pantheon Export/UlazneFakture.cs` | ne | `UlFak`, `UlFakPostav` |
| Komercijala › CRM › Izlazne fakture pregled | **IzlazneFakturePregled** | `Pantheon Export/IzlazneFakturePregled.cs` | ne | `ApiLogovi`, `Faktura`, `Najava` |
| Komercijala › CRM › Izlazne fakture | **IzlazneFakture** | `Pantheon Export/IzlazneFakture.cs` | ne | `Faktura`, `FakturaPostav` |
| Izvoz › Šifarnici › REEXPORT | **frmVrstaCarinskogPostupka** | `Uvoz/frmVrstaCarinskogPostupka.cs` | ne | `VrstaCarinskogPostupka` |
| Izvoz › Izvoz › Pregled izvoznih kontejnera istorija | **frmIzvozIstorija** | `Izvoz/frmIzvozIstorija.cs` | ne |  |
| Izvoz › Izvoz › Prebacivanje kontejnera iz uvoza u Plan izvoza | **frmPregledKontejneraIzvozIzUvoza** | `Izvoz/frmPregledKontejneraIzvozIzUvoza.cs` | ne |  |
| Terminal › Terminalske usluge › ZADACI IPU DISPEČER | **PlaniraniPretovar** | `Uvoz/PlaniraniPretovar.cs` | ne | `KontejnerTekuceOperacije` |
| Terminal › Prevoz › Otprema kamionom - Leget | **frmOtpremaKontejneraUvozKamion** | `Dokumenta/frmOtpremaKontejneraUvozKamion.cs` | da |  |
| Komercijala › Ugovori › Opportunity | **Opportunity** | `Pantheon Export/Opportunity.cs` | ne | `Opportunity` |
| Komercijala › CRM › Izvestaj posla | **PrihodiPosla** | `Pantheon Export/PrihodiPosla.cs` | ne |  |
| Terminal › Terminalske usluge › Pregled radnih naloga Terminal | **frmAnalizaRadnihNaloga** | `RadniNalozi/frmAnalizaRadnihNaloga.cs` | da |  |
| Terminal › Terminalske usluge › Formiranje robnih dokumenata - N | **frmFormiranjeRobnihDokumenata** | `Uvoz/frmFormiranjeRobnihDokumenata.cs` | ne |  |
| Terminal › Šifarnici › Kontejner status | **frmKontejnerStatus** | `Sifarnici/frmKontejnerStatus.cs` | ne |  |
| Terminal › Terminalske usluge › TERMINAL CENTRAL | **frmKontejnerTekuce** | `Uvoz/frmKontejnerTekuce.cs` | da |  |
| Terminal › Šifarnici › Scenario | **frmScenario** | `Sifarnici/frmScenario.cs` | ne | `Scenario` |
| Terminal › Terminalske usluge › Pregled skladista | **frmPregledSkladistaNovi** | `RadniNalozi/frmPregledSkladistaNovi.cs` | da |  |
| Terminal › Terminalske usluge › Grupni prijem kamiona - N | **frmUvozKonacnaZaglavlje** | `Uvoz/frmUvozKonacnaZaglavlje.cs` | ne | `UvozKonacnaZaglavlje` |
| Izvoz › Izvoz › Dogovoreni poslovi | **DogovoreniPosloviIzvoza** | `Izvoz/DogovoreniPosloviIzvoza.cs` | da | `DogovorenoIzvoz` |
| Terminal › Terminalske usluge › Dogovoreni poslovi Izvoza | **DogovoreniPosloviIzvoza** | `Izvoz/DogovoreniPosloviIzvoza.cs` | da | `DogovorenoIzvoz` |
| Terminal › Terminalske usluge › Poveži kontejner sa nalogom izvoza | **frmIzvozTerminalPovezi** | `Izvoz/frmIzvozTerminalPovezi.cs` | ne |  |
| Terminal › Šifarnici › Krajnje destinacije | **KrajnjeDestinacije** | `Sifarnici/KrajnjeDestinacije.cs` | ne | `KrajnjaDestinacija` |
| Komercijala › CRM › apiPregled | **ApiPregled** | `Pantheon Export/ApiPregled.cs` | ne |  |
| Terminal › Prevoz › CIR | **frmCIRPregled** | `TrackModal/Dokumeta/frmCIRPregled.cs` | da |  |
| Terminal › Šifarnici › Terminalske operacije | **frmTerminalOperacije** | `Tehnologija/frmTerminalOperacije.cs` | ne | `TerminalOperacije` |
| Terminal › Prevoz › K - GATE IN VOZ | **frmKalmarGateInVoz** | `RadniNalozi/frmKalmarGateInVoz.cs` | ne |  |
| Terminal › Prevoz › P - VIZUELNI PREGLEDI | **frmVizuelniPregledi** | `RadniNalozi/frmVizuelniPregledi.cs` | ne |  |
| Terminal › Prevoz › K - GATE OUT KAMION | **frmKalmarGateOut** | `RadniNalozi/frmKalmarGateOut.cs` | ne |  |
| Terminal › Prevoz › K - GATE IN KAMION | **frmKalmarGateIN** | `RadniNalozi/frmKalmarGateIN.cs` | ne |  |
| Terminal › Prevoz › P - CIR | **frmCIRPregledac** | `RadniNalozi/frmCIRPregledac.cs` | ne |  |
| Terminal › Prevoz › K - GATE OUT VOZ | **frmKalmarGateOutVoz** | `RadniNalozi/frmKalmarGateOutVoz.cs` | ne |  |
| Terminal › Prevoz › K - PRENOS | **frmKalmarGateINKaminonPreneseno** | `RadniNalozi/frmKalmarGateINKaminonPreneseno.cs` | ne |  |
| Terminal › Terminalske usluge › Nerasporedjeni kontejneri SC15 | **Uvoz** | `Uvoz/Uvoz.cs` | ne | `RadniNalogInterni`, `Uvoz`, `UvozKonacna`, `UvozNapomenePozicioniranja`, `UvozNHM`, `UvozVrstaManipulacije`, `UvozVrstaRobeHS` |
| Terminal › Šifarnici › Predefinisane poruke | **frmPredefinisanePoruke** | `TrackModal/Sifarnici/frmPredefinisanePoruke.cs` | ne | `Predefinisaneporuke` |
| Terminal › Terminalske usluge › Po nalogu Brodara odlazak | **frmPlanoviIzvoza** | `Izvoz/frmPlanoviIzvoza.cs` | ne |  |
| Terminal › Terminalske usluge › Po nalogu Brodara dolazak | **frmPregledPlanovaUtovara** | `Uvoz/frmPregledPlanovaUtovara.cs` | ne |  |
| Terminal › Terminalske usluge › Dopuna plana Praznim kont | **frmDopunaPlanaPraznimIzvoz** | `Izvoz/frmDopunaPlanaPraznimIzvoz.cs` | da |  |
| Terminal › Terminalske usluge › ZADACI INTERMODALNI DISPEČER | **frmRadniNalogInterniPregled** | `Uvoz/frmRadniNalogInterniPregled.cs` | da | `RadniNalogInterniPotvrda` |
| Terminal › Prevoz › PREGLED URADJENIH OPERACIJA | **frmKontejnerTekuceOperacijeLog** | `RadniNalozi/frmKontejnerTekuceOperacijeLog.cs` | ne |  |
| Terminal › Skladišno poslovanje › PRIJEMI ROBE PRETOVAR | **PrijemnicaPregled** | `RadniNalozi/PrijemnicaPregled.cs` | da |  |
| Terminal › Skladišno poslovanje › OTPREMA ROBE PRETOVAR | **OtpremnicaPregled** | `RadniNalozi/OtpremnicaPregled.cs` | da |  |
| Terminal › Terminalske usluge › TERMINAL CENTRAL ARHIV | **frmKontejnerTekuceArhiv** | `Uvoz/frmKontejnerTekuceArhiv.cs` | da |  |
| Terminal › Terminalske usluge › PREDEFINISANJE POZICIJA | **frmDefinisiPoziciju** | `Tehnologija/frmDefinisiPoziciju.cs` | da | `PredefinisanePozicije` |
| Terminal › Terminalske usluge › DODATNE USLUGE | **frmDodatneUsluge** | `Uvoz/frmDodatneUsluge.cs` | da |  |
| Terminal › Šifarnici › Jedinice mere | **frmJediniceMere** | `Sifarnici/frmJediniceMere.cs` | ne | `MerskeEnote` |
| Terminal › Skladišno poslovanje › VAGANJE PREGLED | **VaganjePregled** | `Izvoz/VaganjePregled.cs` | da |  |
| Održavanje › Popis lokomotiva › PREGLEDI LO | **frmPregledLokomotivaPrimopredaja** | `Dokumenta/frmPregledLokomotivaPrimopredaja.cs` | ne |  |
| Terminal › Terminalske usluge › ZADACI TERMINAL SLUŽBENIK | **frmRadniNalogPregledSluzbenik** | `Uvoz/frmRadniNalogPregledSluzbenik.cs` | ne |  |
| Terminal › Skladišno poslovanje › POPIS KONTEJNERA | **frmPopisKonterjnera** | `Dokumenta/frmPopisKonterjnera.cs` | da | `PopisKontejnera`, `PopisKontejneraStavke` |
| Održavanje › SREDSTVA RADA › VOZILA | **frmVozila** | `TrackModal/Dokumeta/frmVozila.cs` | ne | `Vozila` |
| Održavanje › SREDSTVA RADA › Zaduženje / Razduženje | **frmVozilaPregledPrijava** | `Dokumenta/frmVozilaPregledPrijava.cs` | ne |  |
| Uvoz › Šifarnici › Status uluge | **frmStatusUsluge** | `Sifarnici/frmStatusUsluge.cs` | ne | `MerskeEnote` |
| Drumski › Šifarnici › Vozni park | **frmAutomobiliDrumski** | `Drumski/frmAutomobiliDrumski.cs` | da | `Automobili` |
| Drumski › Šifarnici › Valute | **frmValute** | `Izvoz/frmValute.cs` | ne | `Valute` |
| Drumski › Šifarnici › Status vozila | **frmStatusVozila** | `Drumski/frmStatusVozila.cs` | ne | `StatusVozila` |
| Drumski › Šifarnici › Vrsta vozila | **frmVrstaVozila** | `Drumski/frmVrstaVozila.cs` | ne | `VrstaVozila` |
| Drumski › Drumski › Lista naloga | **frmPregledNalogaDrumski** | `Drumski/frmPregledNalogaDrumski.cs` | da | `RadniNalogDrumski` |
| Drumski › Drumski › Pakovanje kamiona | **PakovanjeKamiona1** | `Drumski/PakovanjeKamiona1.cs` | da | `FakturaDrumski` |
| Drumski › Drumski › Lista arhiviranih naloga | **frmPregledArhiviranihNalogaDrumski** | `Drumski/frmPregledArhiviranihNalogaDrumski.cs` | ne |  |
| Izvoz › Izvoz › Operativni plan | **frmOperativniPlanIzvoz** | `Izvoz/frmOperativniPlanIzvoz.cs` | ne |  |
| Uvoz › Uvoz obrada › Operativni plan | **frmOperativniPlanUvoz** | `Uvoz/frmOperativniPlanUvoz.cs` | ne |  |
| Drumski › Šifarnici › Mesta utovara | **frmMestaUtovara** | `Izvoz/frmMestaUtovara.cs` | ne | `MestaUtovara` |
| Drumski › Šifarnici › Pozicioniranje | **frmPozicioniranje** | `Drumski/frmPozicioniranje.cs` | ne |  |
| Drumski › Šifarnici › Partneri | **frmPartnerji** | `Sifarnici/frmPartnerji.cs` | da | `Drzave`, `Partnerji`, `Poste` |
| Carinsko › Magacinsko poslovanje › Magacinski brojevi | **frmMagacinskiBrojevi** | `Carinsko/frmMagacinskiBrojevi.cs` | ne |  |
| Carinsko › Magacinsko poslovanje › Pregled Prijemnica | **frmPrijemnicaCarinskaTabela** | `Carinsko/frmPrijemnicaCarinskaTabela.cs` | ne |  |
| Carinsko › Šifarnici › Partneri | **frmPartnerji** | `Sifarnici/frmPartnerji.cs` | da | `Drzave`, `Partnerji`, `Poste` |
| Carinsko › Šifarnici › Carinarnice | **Carinarnice** | `Uvoz/Carinarnice.cs` | ne | `Carinarnice` |
| Carinsko › Šifarnici › Jedinice mere | **frmJediniceMere** | `Sifarnici/frmJediniceMere.cs` | ne | `MerskeEnote` |
| Carinsko › Šifarnici › Skladista | **frmSkladista** | `TrackModal/Sifarnici/frmSkladista.cs` | da | `Skladista` |
| Carinsko › Šifarnici › Pozicije | **frmPozicija** | `TrackModal/Sifarnici/frmPozicija.cs` | da | `Pozicija` |
| Carinsko › Magacinsko poslovanje › Pregled Otpremnica | **frmOtpremnicaCarinskoTabela** | `Carinsko/frmOtpremnicaCarinskoTabela.cs` | ne |  |
| Carinsko › Magacinsko poslovanje › Analize carinskog | **frmPregledCarinskog** | `Carinsko/frmPregledCarinskog.cs` | ne |  |
| Uvoz › Uvoz obrada › Zapisnici o nepravilnostima/ostećenima | **UvozZapisniciONepravilnostima** | `Uvoz/UvozZapisniciONepravilnostima.cs` | ne |  |
| Uvoz › FINANSIJE › Obrada uvoznih usluga | **ObradaUvoznihUsluga** | `Finansije/ObradaUvoznihUsluga.cs` | ne |  |
| Drumski › Finansije › Lista naloga | **frmPregledFaktura** | `Drumski/frmPregledFaktura.cs` | ne |  |
| Kapija › Kapije › Pregled kamiona | **frmPregledKamiona** | `Kapija/frmPregledKamiona.cs` | da |  |
| Izvoz › Šifarnici › Tip komercijalnog dokumenta | **frmTipKomercijalnogDokumenta** | `Sifarnici/frmTipKomercijalnogDokumenta.cs` | ne | `TipKomercijalnogDokumenta` |
| Kapija › Šifarnici › Status kapija | **frmStatus** | `Kapija/frmStatus.cs` | ne | `StatusKapija` |
| Uvoz › Šifarnici › Tip komercijalnog dokumenta | **frmTipKomercijalnogDokumenta** | `Sifarnici/frmTipKomercijalnogDokumenta.cs` | ne | `TipKomercijalnogDokumenta` |
| Terminal › Šifarnici › Tip zone | **frmTipZone** | `TrackModal/Sifarnici/frmTipZone.cs` | ne | `TipZone` |
| Terminal › Šifarnici › Tip palete | **frmTipPalete** | `TrackModal/Sifarnici/frmTipPalete.cs` | da | `TipPalete` |
| Terminal › Šifarnici › Tip skladišta | **frmTipSkladista** | `TrackModal/Sifarnici/frmTipSkladista.cs` | ne | `TipSkladista` |
| Terminal › Šifarnici › Zone | **frmZone** | `TrackModal/Sifarnici/frmZone.cs` | da | `Zone` |
| Terminal › Šifarnici › Lokacije | **SkladisteGrupa** | `TrackModal/Sifarnici/SkladisteGrupa.cs` | da | `SkladistaGrupa` |

### 4.1 Tabele po kartici starog menija

Obuhvata sve forme do kojih se stiže iz kartice (i forme koje one otvaraju).

**Administrator** — 7 formi, 14 tabela (upis: 4)  
`Automobili` SD, `Delavci` S, `DelovnaMesta` S, `Kartice` S, `KarticeForme` S, `KarticeKorisnik` S, `KarticeMain` S, `Korisnici` SID, `LokomotivaSerija` S, `MUPDozvola` SI, `MUPMesto` SU, `PolozeneLokomotive` S, `PolozenePruge` S, `Pruga` S

**Carinsko** — 9 formi, 12 tabela (upis: 2)  
`MagacinskiBrojevi` S, `MerskeEnote` S, `OtpremnicaCarinska` SIUD, `OtpremnicaCarinskaStavke` S, `Partnerji` S, `Pozicija` S, `PrijemnicaCarinska` SUD, `PrijemnicaCarinskaStavke` S, `Promet` S, `Skladista` S, `SkladistaGrupa` S, `Valute` S

**Drumski** — 30 formi, 50 tabela (upis: 11)  
`Automobili` SIUD, `AutomobiliTehnickiProblem` SID, `Carinarnice` S, `Delavci` S, `DokumentaFaktureDrumski` SI, `DokumentaRadnogNalogaDrumski` S, `DokumentaSkladiste` SI, `DrumskiPozicioniranje` S, `FakturaDrumski` SI, `FakturaDrumskiStavka` SIUD, `Izvoz` S, `IzvozKonacna` S, `IzvozKonacnaZaglavlje` S, `IzvozNapomenePozicioniranja` S, `IzvozNHM` S, `KontejnerskiTerminali` S, `Korisnici` S, `KursnaLista` S, `MapiranjeTipaNaloga` S, `MestaUtovara` S, `NapomenaZaPozicioniranje` S, `NHM` S, `Partnerji` S, `partnerjiKontOsebaMU` S, `PodtipProtokolaRazno` S, `ProtokolRadniNalogDrumski` SIUD, `RadniNalogDrumski` SIUD, `RadniNalogDrumskiIzmene` S, `RadniNalogDrumskiNHM` SI, `RadniNalogInterni` S, `ScenarijaTokaTransporta` S, `ScenarioTokTransporta_Statusi` S, `SistemskePostavke` S, `StatusVozila` SIUD, `TipKontenjera` S, `TipProtokola` S, `UploadedFiles` S, `UploadTokens` S, `uvKvalitetKontejnera` S, `uvNacinPakovanja` S, `Uvoz` S, `UvozKonacna` S, `UvozKonacnaZaglavlje` S, `Valute` S, `Voz` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaRobeADR` S, `VrstaVozila` SIUD, `VrstePostupakaUvoz` S

**Evidencije** — 38 formi, 46 tabela (upis: 14)  
`Aktivnosti` SIUD, `AktivnostiDokumenta` SI, `AktivnostiPrvi` S, `AktivnostiStavke` SID, `AktivnostiStavkeDokumenta` S, `Appointments` S, `Automobili` S, `CistocaIznutra` S, `CistocaIznutraRazduzivanje` S, `CistocaSpolja` S, `CistocaSpoljaRazduzivanje` S, `Config` S, `Delavci` S, `DelovnaMesta` S, `EvidencijaCenaRadnik` SIUD, `EvidencijaTokoviDokumentacije` S, `KomercijalniPregled` S, `Kraji` S, `Labels` S, `LogAktivnosti` SI, `LokomotivaPredjeno` S, `LokomotivaPrijava` SIUD, `LokomotivaPrimopredaja` S, `Markers` S, `Mesta` S, `Najava` S, `PravoAktivnosti` SID, `Reminders` S, `SifGrupaNepravilnosti` SIUD, `SifNeispravnostPostupak` SIUD, `SifOpisNeispravnosti` SIUD, `SifRazredNepravilnosti` SIUD, `SifVrstaNepravilnosti` SIUD, `SifVucaStatusi` S, `Stanice` S, `TehnickiPregled` S, `Teretnica` S, `TrainList` S, `UljeAuto` S, `UljeAutoRazduzivanje` S, `VrstaAktivnosti` SIUD, `Vuca` S, `VucaSlike` S, `VucaStatusi` S, `ZaposleniPrijava` SD, `ZaposleniPrijavaAuto` S

**Izvoz** — 38 formi, 77 tabela (upis: 23)  
`Automobili` S, `Brodovi` SIUD, `Carinarnice` S, `Cene` S, `DirigacijaKontejneraZa` S, `DogovorenoIzvoz` SIUD, `Drzave` S, `GrupaVrsteManipulacije` S, `InspekciskiTretman` S, `Izvoz` SIU, `IzvozDokumenta` SID, `IzvozKonacna` SU, `IzvozKonacnaNapomenePozicioniranja` SID, `IzvozKonacnaNHM` S, `IzvozKonacnaVrstaManipulacije` SID, `IzvozKonacnaVrstaRobeHS` S, `IzvozKonacnaZaglavlje` SIUD, `IzvozNapomenePozicioniranja` S, `IzvozNHM` SID, `IzvozVrstaManipulacije` SIUD, `IzvozVrstaRobeHS` SID, `KontejnerskiTerminali` SIUD, `KontejnerStatus` S, `KontejnerTekuce` S, `KrajnjaDestinacija` SIUD, `MestaUtovara` S, `NapomenaZaPozicioniranje` SIUD, `NaslovStatusaVozila` SIUD, `NHM` S, `organizacioneJedinice` S, `OtpremaKontejnera` S, `OtpremaKontejneraVozStavke` S, `Partnerji` S, `partnerjiKontOseba` S, `partnerjiKontOsebaMU` S, `PostupakStavkeOtpremaVeza` S, `Predefinisaneporuke` S, `PrijemKontejneraVoz` S, `PrijemKontejneraVozStavke` S, `ProdajniNalogIzvoz` S, `ProdajniNalogIzvozStavke` S, `Promet` S, `RadniNalogDrumski` S, `RadniNalogInterni` SIUD, `RadniNalogInterniPotvrda` SI, `Scenario` S, `SerijeKola` S, `Skladista` S, `Stanice` S, `StatusRobe` S, `StatusVozila` S, `TipKomercijalnogDokumenta` SD, `TipKontenjera` S, `uvKvalitetKontejnera` S, `uvNacinPakovanja` SIUD, `Uvoz` S, `UvozKonacna` S, `UvozKonacnaNapomenePozicioniranja` S, `UvozKonacnaNHM` S, `UvozKonacnaVrstaManipulacije` S, `UvozKonacnaZaglavlje` S, `UvozNapomenePozicioniranja` S, `UvozNHM` S, `UvozVrstaManipulacije` S, `UvozVrstaRobeHS` S, `Valute` SIUD, `Voz` S, `VoziloUsluga` S, `VozSerijeKola` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaRobe` S, `VrstaRobeADR` SD, `VrstaRobeHS` SIU, `VrstaVozila` S, `VrstePostupakaUvoz` S, `ZapisnikONepravilnosti` SID

**Kadrovi** — 21 formi, 29 tabela (upis: 7)  
`1ReportGO` SI, `Aktivnosti` S, `AktivnostiDokumenta` S, `AktivnostiStavke` S, `Bolovanje` SIUD, `Delavci` S, `DelavciLokMasin` S, `DelavciPrugaMasin` S, `DelovnaMesta` SIUD, `Dopust` S, `DopustStavke` SIUD, `EvidencijaGOLog` S, `EvidencijaZahteva` SU, `EvidencujaZahtevaVrsta` S, `Korisnici` S, `Kraji` S, `Mesta` S, `ObracunZaposleni` S, `PrekovremeiRad` S, `PrekovremeniRad` SIUD, `Pruga` S, `Razporeditve` S, `RazporeditveStrMesto` S, `testovi` S, `TestoviGenerisanjaPitanja` S, `testovipitanja` S, `TestoviPitanjaOdgovor` S, `VrstaAktivnosti` S, `Zarada` SIUD

**Kapija** — 3 formi, 2 tabela (upis: 2)  
`Kapija` SIUD, `StatusKapija` SIUD

**Knjige** — 3 formi, 4 tabela (upis: 2)  
`Delavci` S, `Partnerji` S, `Registrator` SIUD, `RegistratorDokumenta` SI

**Komercijala** — 54 formi, 84 tabela (upis: 30)  
`Aktivnosti` S, `AktivnostiStavke` S, `ApiLogovi` SI, `ATrainListStavke` S, `BankeFak` S, `Davek` S, `Delavci` S, `DelovnaMesta` S, `DObavnica` S, `Faktura` SIUD, `FakturaPostav` SID, `Izjave` S, `KomercijalniPregled` S, `Korisnici` S, `LokomotivaSerija` S, `MAticniPodatki` SIUD, `MerskeEnote` SIUD, `Mesta` S, `NacinDobave` SIUD, `Najava` SIUD, `NajavaDeljenjeVoza` SI, `NajavaDodatneUsluge` SI, `NajavaDokumenta` SID, `NajavaLog` S, `NajavaPrevoznik` S, `Narocilo` SIUD, `NarociloPostav` S, `NHM` S, `NosiociTroskova` SIU, `Odeljenja` S, `Opportunity` SIUD, `Partnerji` S, `PartnerjiDod` S, `partnerjiKontOseba` S, `PopisTeretnicaStavke` S, `PravoAktivnosti` S, `Predvidjanje` SIUD, `ProdSkup` SID, `Propratnica` SIUD, `PropratnicaRazduzenje` S, `PropratnicaZaduzenje` S, `PropratniceRazduzivanjeSlike` S, `PropratniceZaduzivanjeSlike` S, `Pruga` S, `PrugaStavke` S, `RadniNalog` SIU, `RadniNalogDokumenta` SI, `RadniNalogLokNaTrasi` SIUD, `RadniNalogTeretnice` S, `RadniNalogTrase` SUD, `RadniNalogTraseLok` S, `RadniNalogTraseLokUzr` S, `RadniNalogTraseLokZap` S, `RadniNalogTraseLokZapEvid` S, `RadniNalogVezaNajave` SID, `Razlozi` S, `SifOpisNeispravnosti` S, `SifVrstaNepravilnosti` S, `SifVucaStatusi` S, `Stanice` S, `StatusDelNaloga` S, `StatusTrasa` S, `StatusVoza` S, `Tecaj` S, `Tehnologija` SIUD, `TehnologijaAktivnosti` S, `TehnologijaTrasaLokomotiva` SIUD, `TehnologijaTrase` SIU, `Teretnica` SUD, `TeretnicaIskljuceniVagoni` S, `TeretnicaKomercijalniPregled` S, `TeretnicaStavke` SUD, `this` D, `TipKontenjera` S, `TipSaobPrevoza` SIUD, `TrainList` S, `TrainListStavke` S, `Trase` S, `UlFak` SIUD, `UlFakPostav` SID, `VagoniSerije` S, `Valute` S, `VrstaAktivnosti` S, `VucaStatusi` S

**MainP** — 1 formi, 4 tabela (upis: 0)  
`Kartice` S, `KarticeForme` S, `KarticeKorisnik` S, `KarticeMain` S

**Održavanje** — 19 formi, 32 tabela (upis: 9)  
`Aktivnosti` S, `AktivnostiStavke` S, `AktivnostiStavkeDokumenta` S, `Automobili` SIUD, `CistocaIznutra` S, `CistocaIznutraRazduzivanje` S, `CistocaSpolja` S, `CistocaSpoljaRazduzivanje` S, `Delavci` S, `EvidencijaKvarova` SD, `EvidencijaKvarovaAuto` S, `GrupaKvarova` SIU, `GrupaKvarovaAuto` SIUD, `Kvarovi` SI, `KvaroviAuto` SI, `LokomotivaNamirenje` SD, `LokomotivaPopis` S, `LokomotivaPrijava` S, `lokomotivaVrstaNamirenja` S, `LokomotiveVrstePopisa` SD, `MAticniPodatki` S, `Mesta` S, `Stanice` S, `StatusKvara` S, `StatusKvaraAuto` S, `UljeAuto` S, `UljeAutoRazduzivanje` S, `Vozila` S, `VrstaAktivnosti` S, `VrstaPopisa` SIUD, `VrstaVozila` S, `ZaposleniPrijavaAuto` S

**Operatika** — 17 formi, 25 tabela (upis: 6)  
`Delavci` S, `DelovnaMesta` S, `KontrolaDokumentacije` SIUD, `KontrolaDokumentacijeGreske` S, `KontrolneGreske` SIUD, `KontrolneGreskeTipDokumenta` SIUD, `Korisnici` S, `Mesta` S, `Najava` S, `Notifikacije` SIUD, `Partnerji` S, `Plombe` SIUD, `Pruga` S, `RadniNalog` S, `RadniNalogLokNaTrasi` S, `RadniNalogTrase` S, `RadniNalogTraseLokZap` S, `RadniNalogVezaNajave` S, `Stanice` S, `telegrami` SIUD, `TeretnicaIskljuceniVagoni` S, `TeretnicaStavke` S, `TipZavrsnogDokumentaID` S, `Trase` S, `ZavrsnaDokumenta` S

**Osnovni podaci** — 20 formi, 25 tabela (upis: 14)  
`Carinarnice` S, `Drzave` SI, `Korisnici` S, `LokomotivaSerija` SIUD, `Mesta` S, `NHM` SIUD, `Partnerji` SUD, `PartnerjiFirma` S, `partnerjiKontOseba` S, `Poste` SI, `Pruga` S, `PrugaStavke` SIU, `Razlozi` SIU, `RuteTrase` S, `SifStavkePrimopredaje` S, `SifVucaStatusi` S, `Stanice` SIUD, `StatusVoza` SIUD, `TipSaobPrevoza` SIU, `TipTelegrama` SIUD, `TrasaStanice` SIUD, `Trase` SIUD, `VagoniSerije` SIUD, `Valute` S, `VrstaRobeADR` S

**Terminal** — 142 formi, 136 tabela (upis: 70)  
`Aktivnosti` D, `Autoprevoznilist` SIU, `Carinarnice` S, `Cene` SIUD, `CIR` S, `CIRGreske` S, `Delavci` S, `Delovi` SIUD, `DelovnaMesta` S, `DirigacijaKontejneraZa` SIUD, `DObavnica` SI, `DobavnicaPostav` SI, `DokumentaNajavaKomitenta` SI, `DokumentaOtpremaKontejneraVoz` SID, `DokumentaPrijemKontejneraKamion` SI, `DokumentaPrijemKontejneraVoz` S, `DokumentaVozila` SID, `Greske` SIUD, `GrupaVrsteManipulacije` SIUD, `InspekciskiTretman` SIUD, `Izvoz` S, `IzvozKonacna` S, `IzvozKonacnaNapomenePozicioniranja` S, `IzvozKonacnaNHM` S, `IzvozKonacnaVrstaManipulacije` S, `IzvozKonacnaVrstaRobeHS` S, `IzvozKonacnaZaglavlje` SI, `IzvozNHM` S, `IzvozVrstaManipulacije` S, `IzvozVrstaRobeHS` S, `Komitenti` S, `KontejnerLog` I, `KontejnerskiTerminali` S, `KontejnerStatus` S, `KontejnerTekuce` S, `KontejnerTekuceArhiv` S, `KontejnerTekuceOperacije` SI, `KrajnjaDestinacija` S, `Lokac` S, `MAticniPodatki` S, `MerskeEnote` S, `Mesta` S, `MestaUtovara` SIUD, `NacinDolaskaOdlaska` S, `NajavaPrevoza` S, `NajavaPrevozaTroskovi` SD, `Narocilo` S, `NarociloPostav` S, `NaruceneManipulacije` SID, `NHM` S, `NNal` S, `NNalP` S, `NPre` S, `NPreP` S, `OrganizacionaJedinice` S, `organizacioneJedinice` S, `OtpremaKontejnera` SUD, `OtpremaKontejneraVozStavke` SIUD, `OtpremaKontejneraVozz` S, `Partnerji` S, `Popis` SIUD, `PopisKontejnera` SIU, `PopisKontejneraStavke` SIUD, `PopisStavke` SUD, `Pozicija` SIUD, `Predefinisaneporuke` SIUD, `PredefinisanePozicije` SI, `PrijemKontejneraVoz` S, `PrijemKontejneraVozStavke` S, `Promet` SID, `PrometManipulacije` SID, `PutniNalog` S, `PutniNalogGorivo` SUD, `PutniNalogGume` S, `PutniNalogKvarovi` SIUD, `PutniNalogRuta` S, `PutniNalogServis` S, `PutniNalogTroskovi` S, `PutniNalogZaposleni` S, `RadniNalogInterni` S, `RadniNalogInterniPotvrda` S, `RadniNalogTransport` S, `RNMedjuskladisni` SUD, `RNOtpremaCirade` SIUD, `RNOtpremaPlatforme` SIUD, `RNOtpremaPlatforme2` SIUD, `RNOtpremaVoza` SIUD, `RNPregledIpostavkaKontejnera` SIUD, `RNPregledIspraznjenogKontejnera` SIUD, `RNPrijemCirade` SIUD, `RNPrijemPlatforme` SIUD, `RNPrijemPlatforme2` SIUD, `RNPrijemVoza` SIUD, `Scenario` SIU, `SerijeKola` SIUD, `Sklad` S, `Skladista` SIUD, `SkladistaGrupa` SIU, `Skladiste_Korisnik` S, `SkladisteObjekat` S, `Stanice` S, `StatusRobe` S, `TerminalOperacije` SIUD, `TerminalProces` SIUD, `TerminalRadniNalozi` S, `TipCenovnika` SIUD, `TipKontenjera` SIUD, `TipManipulacije` S, `TipPalete` SIUD, `TipRobe` S, `TipSkladista` SIUD, `TipZone` SIUD, `TovarniList` S, `uvKvalitetKontejnera` SIUD, `uvNacinPakovanja` S, `Uvoz` SI, `UvozKonacna` SU, `UvozKonacnaNapomenePozicioniranja` S, `UvozKonacnaNHM` S, `UvozKonacnaVrstaManipulacije` S, `UvozKonacnaZaglavlje` SIUD, `Vaganje` SIU, `Voz` SUD, `Vozila` SIUD, `VoziloUsluga` S, `VozSerijeKola` S, `VrstaCarinskogPostupka` SIUD, `VrstaManipulacije` SIUD, `VrstaRobe` SIUD, `VrstaRobeADR` SIUD, `VrstaRobeHS` S, `VrstePlombi` SIUD, `VrstePostupakaUvoz` SIUD, `ZapisnikOOstecenju` SID, `Zaposleni` S, `Zone` SIUD

**Uvoz** — 30 formi, 59 tabela (upis: 21)  
`AdresaStatusVozila` SIUD, `Automobili` S, `Brodovi` S, `Carinarnice` SIUD, `Cene` S, `DirigacijaKontejneraZa` S, `GrupaVrsteManipulacije` S, `InspekciskiTretman` S, `IzvozKonacna` S, `KontejnerskiTerminali` S, `KontejnerStatus` S, `MerskeEnote` SIUD, `MestaUtovara` S, `Nalogodavci` S, `NapomenaZaPozicioniranje` S, `NHM` S, `organizacioneJedinice` S, `Partnerji` S, `partnerjiKontOseba` S, `partnerjiKontOsebaMU` S, `Predefinisaneporuke` S, `PrijemKontejneraVoz` S, `PrijemKontejneraVozStavke` S, `RadniNalogDrumski` S, `RadniNalogInterni` SI, `RNPrijemPlatforme` S, `Scenario` S, `SerijeKola` S, `StatusVozila` S, `TipKomercijalnogDokumenta` S, `TipKontenjera` S, `uvNacinPakovanja` S, `Uvoz` SIUD, `UvozDokumenta` SID, `UvozDokumentaCeoVoz` SID, `UvozDokumentaUsluge` SID, `UvozKonacna` SIUD, `UvozKonacnaLogExcel` S, `UvozKonacnaNapomenePozicioniranja` SID, `UvozKonacnaNHM` SID, `UvozKonacnaVrstaManipulacije` SID, `UvozKonacnaVrstaRobeHS` SD, `UvozKonacnaZaglavlje` SUD, `UvozNapomenePozicioniranja` SID, `UvozNHM` SID, `UvozUvoznici` SID, `UvozVrstaManipulacije` SIUD, `UvozVrstaRobeHS` SID, `uvuvozkonacnaoz` U, `Voz` S, `VoziloUsluga` SIUD, `VozSerijeKola` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaRobe` S, `VrstaRobeADR` S, `VrstaRobeHS` S, `VrstePostupakaUvoz` S, `ZapisnikOOstecenju` S

## 5. Novi izgled (NewMain)

Levi meni sa modulima; svaki modul otvara svoju početnu formu sa karticama. Detaljno stablo „ko koga otvara“ je u `docs/LEGET-TABELE-FORME-FUNKCIONALNOSTI.md` (odeljak 3).

Tabele koje čita sam `NewMain` (prava, meni, kartice): `MainNovi` (S), `mainNoviKartice` (S), `MainNoviPrava` (S)

| Modul (dugme) | Formi | Od toga i u starom izgledu | Tabela | Tabela sa upisom |
|---|---|---|---|---|
| Administracija | 3 | 1 | 3 | 2 |
| Depo | 1 | 0 | 0 | 0 |
| Drumski transport | 22 | 13 | 47 | 7 |
| Dugme „!!!“ (samo korisnik test) | 1 | 0 | 4 | 2 |
| HR modul | 15 | 0 | 71 | 8 |
| Intermodalni terminal | 11 | 0 | 1 | 1 |
| Kapija | 4 | 2 | 10 | 2 |
| Logistika izvoza | 15 | 3 | 42 | 9 |
| NewMain | 1 | 0 | 3 | 0 |
| Održavanje | 14 | 0 | 26 | 3 |
| Operacije pretovari | 9 | 5 | 31 | 4 |
| Operacije skladišta | 27 | 2 | 68 | 13 |
| Podešavanja sistema | 10 | 8 | 18 | 8 |
| Početni ekran (Dashboard) | 1 | 0 | 1 | 0 |
| Prijem i otprema kamiona | 14 | 4 | 37 | 2 |
| Prijem i otprema vozova | 84 | 76 | 122 | 47 |

**Administracija** — forme: Administracija1, frmKorisnici, frmPodesavanja  
Tabele: `Delavci` S, `Korisnici` SID, `Podesavanja` SU

**Depo** — forme: DepoMain  
Tabele: 

**Drumski transport** — forme: Cerade, Drumski1, frmAutomobilEdit, frmAutomobiliDrumski, frmBrojFaktureModal, frmDrumski, frmDrumski1, frmFormaZaVozace, frmListaProtokola, frmNalogZaFakturisanje, frmObradaUlaznihFaktura, frmPodesavanjeRaspolozivosti, frmPregledFajlova, frmPregledNalogaDrumski, frmPregledPorukeVozacu, frmProtokolTransportnogNaloga, frmStatus, NalogZaDrumski, PakovanjeKamiona1, PakovanjeKamionaCerade, Platforma, VanGabaritni  
Tabele: `Automobili` SIUD, `AutomobiliTehnickiProblem` SID, `Carinarnice` S, `Delavci` S, `DokumentaRadnogNalogaDrumski` S, `Dokumenti` S, `DrumskiPozicioniranje` S, `FakturaDrumski` SI, `FakturaDrumskiStavka` SIU, `Izvoz` S, `IzvozKonacna` S, `IzvozKonacnaZaglavlje` S, `IzvozNapomenePozicioniranja` S, `IzvozNHM` S, `KontejnerskiTerminali` S, `Korisnici` S, `MapiranjeTipaNaloga` S, `MestaUtovara` S, `NapomenaZaPozicioniranje` S, `NHM` S, `Partnerji` S, `partnerjiKontOsebaMU` S, `PodtipProtokolaRazno` S, `PraviloArtikla` S, `ProtokolRadniNalogDrumski` SIUD, `RadniNalogDrumski` SIUD, `RadniNalogDrumskiNHM` SI, `RadniNalogInterni` S, `ScenarijaTokaTransporta` S, `ScenarioTokTransporta_Statusi` S, `SistemskePostavke` S, `StatusVozila` S, `TipKontenjera` S, `TipProtokola` S, `UploadTokens` S, `uvKvalitetKontejnera` S, `uvNacinPakovanja` S, `Uvoz` S, `UvozKonacna` S, `UvozKonacnaZaglavlje` S, `Valute` S, `Voz` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaRobeADR` S, `VrstaVozila` S, `VrstePostupakaUvoz` S

**Dugme „!!!“ (samo korisnik test)** — forme: AdministracijaPravoPristupa  
Tabele: `Korisnici` S, `MainNovi` S, `mainNoviKartice` SID, `MainNoviPrava` SID

**HR modul** — forme: frmHRDan, frmHREvidencijaRada, frmHRGOPravo, frmHRGOZahtevi, frmHRGrupniUnos, frmHRKontrolnaTabla, frmHRLog, frmHRMeni, frmHRRevers, frmHRSifarnici, frmHRStavka, frmHRZahtev, frmHRZahteviLista, frmHRZaposleni, frmHRZaposleniLista  
Tabele: `BeleskaORadu` S, `EvidencijaMesec` S, `EvidencijaRada` S, `fn_Podredjeni` S, `fnHR_ZahtevPlanOdobravanja` S, `GODopust` S, `GODopustOsnov` S, `GODopustStavke` SU, `GrupeDokumenata` S, `HRLog` S, `HTZGrupe` S, `HTZOprema` S, `HTZReversi` SU, `Korisnici` S, `MainNovi` S, `mainNoviKartice` S, `MainNoviPrava` S, `OceneSposobnosti` S, `organizacioneJedinice` S, `OsnovGO` S, `Partnerji` S, `Praznici` S, `PrioritetiNabavke` S, `RadnaMesta` S, `RadniOdnosi` S, `RadnoMestoNormativHTZ` S, `RadnoMestoObavezniDokumenti` S, `RadnoMestoObavezniPregledi` S, `RasporedRada` S, `RazloziRazduzenja` S, `Srodstva` S, `Stanice` S, `StatusiStavkiNabavke` S, `StatusiZahtevaZaNabavku` S, `StatusZahteva` S, `StepenObrazovanja` S, `TezinePovreda` S, `VrstaRada` S, `VrsteDisciplinskihMera` S, `VrsteDokumenta` S, `VrsteNabavke` S, `VrstePregleda` S, `VrsteUgovora` S, `vw_EvidencijaZaObracun` S, `vZaposleniRokovi` S, `vZaposleniTrenutno` S, `vZaposleniUpozorenja` S, `ZahteviZaNabavku` S, `ZahtevZaNabavkuIstorijaStatusa` S, `ZahtevZaNabavkuOdobrenja` S, `ZahtevZaNabavkuPonude` SD, `ZahtevZaNabavkuPrilozi` SD, `ZahtevZaNabavkuStavke` SUD, `Zaposleni` SIU, `ZaposleniClanoviPorodice` S, `ZaposleniDisciplinske` S, `ZaposleniDokumenti` S, `ZaposleniHitniKontakti` S, `ZaposleniHTZZaduzenja` S, `ZaposleniMentorBeleske` S, `ZaposleniMentori` S, `ZaposleniOcene` S, `ZaposleniOsnovGO` S, `ZaposleniPovrede` S, `ZaposleniPrilozi` SD, `ZaposleniRadnaMesta` S, `ZaposleniSkolovanje` S, `ZaposleniSlike` SD, `ZaposleniStatusi` S, `ZaposleniUgovori` S, `ZaposleniZdravstveniPregledi` S

**Intermodalni terminal** — forme: frmTerminalPrivremeni, frmTerminalPrivremeniArhiva, frmTerminalPrivremeniDokumenta, frmTerminalPrivremeniExcel, frmTerminalPrivremeniLog, frmTerminalPrivremeniOpstiIzvoz, frmTerminalPrivremeniPromeni, frmTerminalPrivremeniSlike, Intermodalni1, IntermodalniKomN, VozniPlanovi  
Tabele: `TerminalPriv` IUD

**Kapija** — forme: frmKamionDetalji, frmKapija1, frmKapijaOtvoreniNalozi, frmPregledKamiona  
Tabele: `IzvozKonacna` S, `Kapija` SIUD, `KontejnerLog` I, `MestaUtovara` S, `PrijemKontejneraVoz` S, `RadniNalogInterni` S, `RadniNalogInterniPotvrda` S, `Scenario` S, `StatusKapija` S, `TipKontenjera` S

**Logistika izvoza** — forme: frmCarinskiPostupak, frmGrupniUnosPoljaIzvoz, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozPregledKontejneraDrumskeUsluge, frmIzvozUnosManipulacije, frmLogTokaProcesa, frmPregledGrupaKontejneraIzvoz, frmPregledPorudzbenica, frmProdajniNalogIzvoz, frmProdajniNalogIzvozTabela, frmRelacija, LogistikaIzvoza0, LogistikaIzvoza1, LogistikaIzvoza2, NalogIzvozaZaOtpremu  
Tabele: `Automobili` S, `Carinarnice` S, `Cene` S, `GrupaVrsteManipulacije` S, `InspekciskiTretman` S, `Izvoz` SU, `IzvozKonacna` SU, `IzvozKonacnaNHM` S, `IzvozKonacnaVrstaManipulacije` SID, `IzvozKonacnaVrstaRobeHS` S, `IzvozNapomenePozicioniranja` SI, `IzvozNHM` S, `IzvozVrstaManipulacije` SIUD, `IzvozVrstaRobeHS` S, `KontejnerLog` SI, `KontejnerskiTerminali` S, `KontejnerStatus` S, `KontejnerTekuce` IU, `KrajnjaDestinacija` S, `MerskeEnote` S, `MestaUtovara` S, `NapomenaZaPozicioniranje` S, `NHM` S, `organizacioneJedinice` S, `Partnerji` S, `partnerjiKontOsebaMU` S, `ProdajniNalogIzvoz` SIUD, `ProdajniNalogIzvozStavke` SIU, `RadniNalogDrumski` S, `Scenario` S, `StatusVozila` S, `TipKontenjera` S, `uvKvalitetKontejnera` S, `uvNacinPakovanja` S, `UvozKonacnaVrstaManipulacije` S, `UvozVrstaManipulacije` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaRobeADR` S, `VrstaRobeHS` S, `VrstaVozila` S, `VrstePostupakaUvoz` S

**NewMain** — forme: NewMain  
Tabele: `MainNovi` S, `mainNoviKartice` S, `MainNoviPrava` S

**Održavanje** — forme: frmSERDeo, frmSERKorekcije, frmSERMeni, frmSERNabavka, frmSERNalog, frmSERNalozi, frmSERNarudzbenica, frmSERObavestenja, frmSERPopis, frmSERPrijem, frmSERPromet, frmSERSifarnici, frmSERStavka, frmSERZalihe  
Tabele: `Podesavanja` S, `SERDeo` S, `SERDeoGrupa` S, `SERDeoKretanje` S, `SERDeoPrimenljivost` SD, `SERDeoRezervacija` S, `SERDokument` S, `SERKorekcija` S, `SERNarudzbenicaStavka` S, `SERObavestenje` SU, `SERPlanNormativ` SUD, `SERPlanStavka` S, `SERPopisStavka` S, `SERRadniNalog` S, `SERSredstvo` S, `SERStatus` S, `SERVrstaSredstva` S, `vSERDeoStanje` S, `vSERKarticaDela` S, `vSERNarudzbenica` S, `vSERNarudzbenicaStavka` S, `vSERPopis` S, `vSERPredlogNabavke` S, `vSERPromet` S, `vSERRadniNalogDelovi` S, `vSERRadniNalogTrosak` S

**Operacije pretovari** — forme: frmPretovariSkladista, frmPretovarPrijem, Otpremnica, OtpremnicaPregled, PretovariMain, PretovariPrijem, Prijemnica, PrijemnicaPregled, ZapisnikOOstecenjuRobe  
Tabele: `Delavci` S, `DObavnica` SI, `DobavnicaPostav` SI, `IzvozKonacna` S, `Lokac` S, `MAticniPodatki` S, `MerskeEnote` S, `Mesta` S, `Narocilo` S, `NarociloPostav` S, `NHM` S, `NNal` S, `NNalP` S, `NPre` S, `NPreP` S, `organizacioneJedinice` S, `Partnerji` S, `Pozicija` S, `PrijemKontejneraVozStavke` S, `Promet` SI, `RadniNalogInterni` S, `RadniNalogInterniPotvrda` S, `RNMedjuskladisni` S, `Sklad` S, `Skladista` S, `SkladistaGrupa` S, `Skladiste_Korisnik` S, `TipRobe` S, `UvozKonacnaNHM` S, `Vozila` S, `ZapisnikOOstecenju` SID

**Operacije skladišta** — forme: CarinskoSkladiste, CarinskoSkladistePregled, frmIzvestajCarinskoObelezje, frmMestoIsporuke, frmNalogPrijem, frmSeniranjeDokumenata, frmTipPalete, frmVozac, InterniPoslovi, InterniPrenos, KomercijalnoSkladiste, KomercijalnoSkladistePregled, MagacinskaKnjigaCarinska, NalogZaKomercijalni, Otprema, OtpremaLager, Otpremnica, PaletePrebacivanje, PregledKomercijalnihNaloga, PregledLagera, Pretovar, Prijemnica, RadniNaloziSkladista, SkladistaMain, TipSkladista, VanredniPoslovi, ViljuskaristiPregled  
Tabele: `Artikli` SU, `ArtikliCetvrtiNivo` S, `ArtikliDrugiNivo` S, `ArtikliPrviNivo` S, `ArtikliTreciNivo` S, `Carinarnice` S, `Delavci` S, `DokumentaFaktureDrumski` SI, `DokumentaSkladiste` SI, `InspekciskiTretman` S, `IzvozKonacna` S, `IzvozKonacnaVrstaManipulacije` S, `IzvozKonacnaZaglavlje` S, `KontejnerStatus` S, `Korisnici` S, `Lager` S, `MagacinskaKnjigaCarinska` S, `MagacinskiBrojCarinski` SIU, `MagacinskiPredmet` S, `MerskeEnote` S, `MestaUtovara` S, `NHM` S, `organizacioneJedinice` S, `Partnerji` S, `partnerjiKontOseba` S, `partnerjiKontOsebaMU` S, `PomOstali` S, `PosebniUsoviSmestaja` S, `Primljeno` S, `ProdajniNalogIzvoz` S, `Promet` S, `RadniNalogDrumski` S, `RadniNalogInterni` S, `RadniNalogInterniPotvrda` S, `RadniNalogInterniSkladistePotvrda` S, `RadniNalogSkladista` SIU, `Rezervisano` S, `RNCarinskoPackingLista` SIU, `RNCarinskoPackingListaStavka` SIU, `RNCarinskoPaleta` S, `RNCarinskoPaletaStavka` S, `RNCarinskoPrijemnicaStavke` SID, `RNCarinskoSkladisteDodatneUsluge` SID, `RNCarinskoSkladisteOtpremaStavkePom` SID, `RNCarinskoSkladisteOtpremnica` SIU, `RNCarinskoSkladisteOtpremnicaStavke` S, `RNCarinskoSkladistePrijemnica` SIU, `RNCarinskoSkladisteRukovalac` S, `RNCarinskoStavke` S, `Scenario` S, `Skladista` S, `Stock` S, `TipKontenjera` S, `TipPalete` SIUD, `uvNacinPakovanja` S, `UvozKonacna` S, `UvozKonacnaNapomenePozicioniranja` S, `UvozKonacnaVrstaManipulacije` S, `UvozKonacnaZaglavlje` S, `Valute` S, `VlasniciRobe` S, `Voz` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaRobeADR` S, `VrstaVozila` S, `VrstePrevoznogSredstva` S, `Zavrseno` S

**Podešavanja sistema** — forme: frmKontaktOsobe, frmPartnerji, frmPartnerjiFirme, frmPozicija, frmSkladista, frmSkladisteObjekat, frmTipKontejnera, frmZone, Podesavanje1, SkladisteGrupa  
Tabele: `Carinarnice` S, `Drzave` SI, `Korisnici` S, `Partnerji` SUD, `PartnerjiFirma` S, `partnerjiKontOseba` S, `Poste` SI, `Pozicija` SIUD, `Skladista` SIUD, `SkladistaGrupa` SIU, `SkladisteObjekat` S, `TipKontenjera` SIUD, `TipPalete` S, `TipSkladista` S, `TipZone` S, `uvKvalitetKontejnera` S, `Valute` S, `Zone` SIUD

**Početni ekran (Dashboard)** — forme: GlavniEkran  
Tabele: `TerminalPriv` S

**Prijem i otprema kamiona** — forme: frmCerada, frmCeradaOtprema, frmCIR, frmFotografijePregledac, frmPlatforma, frmPlatformaOtprema, frmPrijemIOtpremaCerada, frmPrijemIOtpremaKamiona1, frmPrijemIOtpremaPlatforma, frmPrijemKamionaDetalji, IzvestajVaganja, Pregledac, Vaganje, VaganjePregled  
Tabele: `Carinarnice` S, `CIR` S, `CIRGreske` S, `Delovi` S, `DokumentaPregledac` S, `Greske` S, `InspekciskiTretman` S, `IzvozKonacna` S, `IzvozKonacnaNHM` S, `Kapija` S, `KontejnerLog` I, `KontejnerskiTerminali` S, `MestaUtovara` S, `NaruceneManipulacije` S, `NHM` S, `organizacioneJedinice` S, `OtpremaKontejnera` S, `OtpremaKontejneraVozStavke` S, `OtpremaKontejneraVozz` S, `Partnerji` S, `partnerjiKontOsebaMU` S, `PrijemKontejneraVoz` S, `PrijemKontejneraVozStavke` S, `ProdajniNalogIzvozStavke` S, `RadniNalogInterni` S, `RadniNalogInterniPotvrda` S, `RNOtpremaPlatforme` S, `RNPrijemPlatforme` S, `Scenario` S, `TipKontenjera` S, `uvKvalitetKontejnera` S, `uvNacinPakovanja` S, `Vaganje` SIU, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaRobeADR` S, `VrstaVozila` S

**Prijem i otprema vozova** — forme: DogovoreniPosloviIzvoza, frmAnalizaRadnihNaloga, frmAutoprevozniList2, frmCIRPregled, frmDefinisiPoziciju, frmDodatneUsluge, frmDodatniList, frmDodelaSkladista, frmDokumentaOtpremaKontejnera, frmDokumentaPrijemKontejneraKamion, frmDokumentaPrijemKontejneraVoz, frmDopunaPlanaPraznimIzvoz, frmFormiranjePlana, frmFormiranjePlanaIzvoz, frmIzvozDokumenta, frmIzvozKonacna, frmIzvozKonacnaTable, frmKontaktOsobeMU, frmKontejneriNaPrijemnici, frmKontejnerTekuce, frmKontejnerTekuceArhiv, frmLagerOperater, frmManipulacije, frmNajavaKomitemtaDokument, frmNalogZaPrevoz, frmOtpremaKontejnera, frmOtpremaKontejneraIzvozKamion, frmOtpremaKontejneraKamionomIzKontejnera, frmOtpremaKontejneraLegetIZVOZ, frmOtpremaKontejneraUvozKamion, frmOtpremaVozaIzPlana, frmPopisKontejneraStampa, frmPopisKonterjnera, frmPovezivanjeKontejneraIVagona, frmPregledKontejneraDrumskeUsluge, frmPregledNarucenihManipulacija, frmPregledOtpreme, frmPregledSkladistaNovi, frmPregledVozova, frmPrijemIOtpremaVozova2 … (+35)  
Tabele: `Automobili` S, `Autoprevoznilist` SIU, `Brodovi` S, `Carinarnice` S, `Cene` S, `CIR` S, `DirigacijaKontejneraZa` S, `DogovorenoIzvoz` SIUD, `DokumentaNajavaKomitenta` SI, `DokumentaOtpremaKontejneraVoz` SID, `DokumentaPrijemKontejneraKamion` SI, `DokumentaPrijemKontejneraVoz` S, `GrupaVrsteManipulacije` S, `InspekciskiTretman` S, `Izvoz` SIU, `IzvozDokumenta` SID, `IzvozKonacna` SU, `IzvozKonacnaNapomenePozicioniranja` SID, `IzvozKonacnaNHM` S, `IzvozKonacnaVrstaManipulacije` S, `IzvozKonacnaVrstaRobeHS` S, `IzvozKonacnaZaglavlje` SIU, `IzvozNapomenePozicioniranja` S, `IzvozNHM` S, `IzvozVrstaManipulacije` S, `IzvozVrstaRobeHS` S, `Komitenti` S, `KontejnerskiTerminali` S, `KontejnerStatus` S, `KontejnerTekuce` S, `KontejnerTekuceArhiv` S, `KrajnjaDestinacija` S, `MestaUtovara` S, `NajavaPrevoza` S, `NajavaPrevozaTroskovi` SD, `NapomenaZaPozicioniranje` S, `NaruceneManipulacije` SID, `NHM` S, `organizacioneJedinice` S, `OtpremaKontejnera` SUD, `OtpremaKontejneraVozStavke` SIUD, `Partnerji` S, `partnerjiKontOseba` S, `partnerjiKontOsebaMU` S, `PopisKontejnera` SIU, `PopisKontejneraStavke` SIUD, `PostupakStavkeOtpremaVeza` S, `Pozicija` S, `Predefinisaneporuke` S, `PredefinisanePozicije` SI, `PrijemKontejneraVoz` S, `PrijemKontejneraVozStavke` S, `ProdajniNalogIzvoz` S, `ProdajniNalogIzvozStavke` S, `Promet` SI, `PutniNalog` S, `PutniNalogGorivo` SUD, `PutniNalogGume` S, `PutniNalogKvarovi` SIUD, `PutniNalogRuta` S, `PutniNalogServis` S, `PutniNalogTroskovi` S, `PutniNalogZaposleni` S, `RadniNalogDrumski` S, `RadniNalogInterni` SI, `RadniNalogInterniPotvrda` SI, `RadniNalogTransport` S, `RNMedjuskladisni` SUD, `RNOtpremaCirade` S, `RNOtpremaPlatforme` SIUD, `RNOtpremaPlatforme2` SIUD, `RNOtpremaVoza` SIUD, `RNPregledIpostavkaKontejnera` S, `RNPregledIspraznjenogKontejnera` S, `RNPrijemCirade` S, `RNPrijemPlatforme` SIUD, `RNPrijemPlatforme2` SIUD, `RNPrijemVoza` SIUD, `Scenario` S, `SerijeKola` S, `Skladista` SU, `SkladistaGrupa` S, `Stanice` S, `StatusRobe` S, `StatusVozila` S, `TerminalRadniNalozi` S, `TipKomercijalnogDokumenta` S, `TipKontenjera` S, `TovarniList` S, `uvKvalitetKontejnera` S, `uvNacinPakovanja` S, `Uvoz` SIU, `UvozDokumenta` SID, `UvozDokumentaCeoVoz` SID, `UvozDokumentaUsluge` SID, `UvozKonacna` SUD, `UvozKonacnaNapomenePozicioniranja` SID, `UvozKonacnaNHM` SID, `UvozKonacnaVrstaManipulacije` SID, `UvozKonacnaVrstaRobeHS` SD, `UvozKonacnaZaglavlje` SUD, `UvozNapomenePozicioniranja` S, `UvozNHM` SID, `UvozUvoznici` SID, `UvozVrstaManipulacije` SIUD, `UvozVrstaRobeHS` S, `uvuvozkonacnaoz` U, `Valute` S, `Voz` SUD, `Vozila` S, `VoziloUsluga` SIUD, `VozSerijeKola` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaRobe` S, `VrstaRobeADR` S, `VrstaRobeHS` S, `VrstaVozila` S, `VrstePostupakaUvoz` S, `ZapisnikONepravilnosti` SID, `Zaposleni` S, `Zone` S

### 5.1 Podešavanja sistema u novom izgledu

| Forma | Fajl | Upisuje u tabele |
|---|---|---|
| frmKontaktOsobe | `Dokumenta/frmKontaktOsobe.cs` | — |
| frmPartnerji | `Sifarnici/frmPartnerji.cs` | `Drzave`, `Partnerji`, `Poste` |
| frmPartnerjiFirme | `Sifarnici/frmPartnerjiFirme.cs` | — |
| frmPozicija | `TrackModal/Sifarnici/frmPozicija.cs` | `Pozicija` |
| frmSkladista | `TrackModal/Sifarnici/frmSkladista.cs` | `Skladista` |
| frmSkladisteObjekat | `TrackModal/Sifarnici/frmSkladisteObjekat.cs` | — |
| frmTipKontejnera | `TrackModal/Sifarnici/frmTipKontejnera.cs` | `TipKontenjera` |
| frmZone | `TrackModal/Sifarnici/frmZone.cs` | `Zone` |
| Podesavanje1 | `MainLeget/Podesavanjesistema/Podesavanje1.cs` | — |
| SkladisteGrupa | `TrackModal/Sifarnici/SkladisteGrupa.cs` | `SkladistaGrupa` |

## 6. Poređenje formi

### 6.1 Forme samo u novom izgledu (118)

Administracija1, AdministracijaPravoPristupa, CarinskoSkladiste, CarinskoSkladistePregled, Cerade, DepoMain, Drumski1, frmCarinskiPostupak, frmCerada, frmCeradaOtprema, frmFotografijePregledac, frmGrupniUnosPoljaIzvoz, frmHRDan, frmHREvidencijaRada, frmHRGOPravo, frmHRGOZahtevi, frmHRGrupniUnos, frmHRKontrolnaTabla, frmHRLog, frmHRMeni, frmHRRevers, frmHRSifarnici, frmHRStavka, frmHRZahtev, frmHRZahteviLista, frmHRZaposleni, frmHRZaposleniLista, frmIzvestajCarinskoObelezje, frmKapija1, frmKapijaOtvoreniNalozi, frmLogTokaProcesa, frmMestoIsporuke, frmNalogPrijem, frmNalogZaFakturisanje, frmObradaUlaznihFaktura, frmPlatforma, frmPlatformaOtprema, frmPodesavanja, frmPregledGrupaKontejneraIzvoz, frmPregledPorudzbenica, frmPretovariSkladista, frmPretovarPrijem, frmPrijemIOtpremaCerada, frmPrijemIOtpremaKamiona1, frmPrijemIOtpremaPlatforma, frmPrijemIOtpremaVozova2, frmPrijemIOtpremaVozova3, frmPrijemIOtpremaVozova4, frmPrijemKamionaDetalji, frmProdajniNalogIzvoz, frmProdajniNalogIzvozTabela, frmRelacija, frmRN12InterniPrenos, frmSERDeo, frmSERKorekcije, frmSERMeni, frmSERNabavka, frmSERNalog, frmSERNalozi, frmSERNarudzbenica, frmSERObavestenja, frmSERPopis, frmSERPrijem, frmSERPromet, frmSERSifarnici, frmSERStavka, frmSERZalihe, frmSkladisteObjekat, frmStatus, frmTerminalPrivremeni, frmTerminalPrivremeniArhiva, frmTerminalPrivremeniDokumenta, frmTerminalPrivremeniExcel, frmTerminalPrivremeniLog, frmTerminalPrivremeniOpstiIzvoz, frmTerminalPrivremeniPromeni, frmTerminalPrivremeniSlike, frmVozac, frmVozOtprema, frmVozPrijem, GlavniEkran, Intermodalni1, IntermodalniKomN, InterniPoslovi, InterniPrenos, KomercijalnoSkladiste, KomercijalnoSkladistePregled, LogistikaIzvoza0, LogistikaIzvoza1, LogistikaIzvoza2, MagacinskaKnjigaCarinska, NalogIzvozaZaOtpremu, NalogZaDrumski, NalogZaKomercijalni, NewMain, Otprema, OtpremaLager, Otpremnica, PakovanjeKamionaCerade, PaletePrebacivanje, Platforma, Podesavanje1, Pregledac, PregledKomercijalnihNaloga, PregledLagera, Pretovar, PretovariMain, PretovariPrijem, PrijemIOtpremaVozova1, Prijemnica, RadniNaloziSkladista, SkladistaMain, TerminalMapFRM, TipSkladista, VanGabaritni, VanredniPoslovi, ViljuskaristiPregled, VozniPlanovi

### 6.2 Forme samo u starom izgledu (318), po kartici

- **Administrator** (6): frmMUPDozvola, frmMupMesto, frmPolozeneLokomotive, frmPolozenePruge, Obavestenje, Prava
- **Carinsko** (9): frmMagacinskiBrojevi, frmOtpremnicaCarinskaStampa, frmOtpremnicaCarinsko, frmOtpremnicaCarinskoTabela, frmPregledCarinskog, frmPrijemnicaCarinskaStampa, frmPrijemnicaCarinskaTabela, frmPrijemnicaCarinsko, frmPrijemnicaStavke
- **Drumski** (16): DodavanjeSkeniraneFakture, DodavanjeSkeniranePrevoznice, frmFaktureDetalji, frmIzvestajNalogZaPrevoz, frmObjedinjenaDokumentIzlaznaFakura, frmPozicioniranje, frmPozicioniranjeEdit, frmPregledArhiviranihNalogaDrumski, frmPregledDokumenataKamiona, frmPregledDokumenataVozaca, frmPregledFaktura, frmPregledSkeniranihDokumenata, frmStatusVozila, frmStatusVozilaDetalji, frmVrstaVozila, frmVrstaVozilaEdit
- **Evidencije** (38): frmAnalizaAktivnosti, frmCenaPoRadniku, frmEvidencijaRada, frmEvidencijaRadaCG, frmEvidencijaRadaDokumenti, frmEvidencijaRadaKragujevac, frmEvidencijaRadaPromene, frmEvidencijaRadaRemont, frmEvidencijaRadaSmederevo, frmEvidencijaRadaZaglavlje, frmGrupaNepravilnosti, frmKalendar, frmKomercijalniPregled, frmLogAktivnosti, frmNeispravnostPostupak, frmOpisNeispravnosti, frmPlanRada, frmPravoAktivnosti, frmPravoAktivnostiViseRadnika, frmPredjenaKilometrazaLokomotiva, frmPregledAktivnosti, frmPregledMasinovodje, frmPregledMasinovodje2, frmPregledMobilni, frmPregledMobilni2, frmPrijavaMasinovodje, frmPrijavaMasinovodjeOld, frmPrijavaSmene, frmPrijavaSmeneOld, frmRazredNepravilnosti, frmTehnickiPregled, frmTokoviDokumentacije, frmTokoviDokumentacijeSlika, frmVrstaNepravilnosti, frmVrsteAktivnosti, frmVucaPregled, frmZarade, PoLokomotivama
- **Izvoz** (22): Brodovi, frmIzvoz, frmIzvozIstorija, frmIzvozKonacnaZaglavlje, frmIzvozKopiranjeKontejnera, frmIzvozTable, frmKontejnerskiTerminali, frmNapomenaZaPozicioniranje, frmNaslovStatusaVozila, frmOperativniPlanIzvoz, frmPlanoviIzvoza, frmPrebacivanjeIzPlanaUPlan, frmPregledKontejneraIzvoz, frmPregledKontejneraIzvozIzUvoza, frmPrijemKamionaPlatforma, frmRadniNaloziInterni, frmTipKomercijalnogDokumenta, frmValute, frmVrstaRobeHS, fruvNacinPakovanja, IzvozOpredeljenje, KrajnjeDestinacije
- **Kadrovi** (21): frmAnalizaGO, frmAnalizaGOSum, frmBolovanja, frmDelovnaMesta, frmEvidencijaGodišnjihOdmora, frmEvidencijaGodLOG, frmEvidencijaRAdaNeplaceno, frmFinansije, frmGenerisanjeTestaKorisnik, frmIzracunZarada, frmObracunFiksni, frmOsnovnaZarada, frmOsoblje, frmParametriObracuna, frmPlacenoNeplaceno, frmPrekovremeniRad, frmPutniNaloziPregled, frmSlobodniDani, Obrasci, Pitanja, TestiranjeStampa
- **Kapija** (1): frmStatus
- **Knjige** (3): frmRegistator, frmRegistratorDokumenta, frmRegistratorPregled
- **Komercijala** (54): ApiPregled, DraganStatusi, EAvansni, EFaktura, EOdobrenje, EZaduzenje, frmAnalizaPorudzbina, frmDogovori, frmDogovoriPregled, frmDokumentaNajava, frmDokumentaRadniNalog, frmIskljuceniVagoni, frmJediniceMere, frmKasnjenja, frmLokomotiveNaTrasi, frmMailNajava, frmMaticniPodatki, frmNacinIsporuke, frmNajava, frmNajava20, frmNajavaArhivaAnaliza, frmNajavaLog, frmNajaveBezTeretnice, frmObjekt, frmPodtrase, frmPregledRID, frmProdajnaGrupa, frmPronadjiVagon, frmPropratnice, frmRadniNalog, frmRadniNalogIzNajave, frmRadniNalogTraseLok, frmRadniNalogZaposleni, frmRIDPoNajavama, frmRIDTeretnice, frmSendEmailWithAttacment, frmTehnologija, frmTehnologijaPregled, frmTeretnica, frmTeretnicaKopiranjeStavki, frmTeretnicaTerenIzmena, frmTeretnicePregled, frmTrainList, IzlazneFakture, IzlazneFakturePregled, NosiociTroskova, Opportunity, PoslataDokumenta, Predvidjanje, PrihodiPosla, PrimljenaDokumenta, UlazneFakture, UlFakPregled, Valuta
- **MainP** (1): MainP
- **Održavanje** (19): frmAutomobili, frmAutomobiliKvarovi, frmAutomobiliPregledPrijava, frmEvidencijaKvarova, frmEvidencijaKvarovaAnaliza, frmGrupaKvarova, frmGrupaKvarovaAuto, frmKvarovi, frmKvaroviAuto, frmLokomotivaVrstaPopisa, frmNamirenja, frmNamirenjaSumarno, frmPregledLokomotivaPrimopredaja, frmPregledPopisa1, frmPregledPopisa2, frmPrijavaKvara, frmPrijavaKvaraAuto, frmVozilaPregledPrijava, frmVrstePopisa
- **Operatika** (17): frmCentralnaTablaCpajal, frmCentralnaTablaRN, frmKontrola, frmMapa, frmMapaOpis, frmNotifikacije, frmPlombe, frmPravljenjeVoza, frmRadniNalogPregled, frmRAdniNalogPregledPoLokomotivama, frmRaspustiVagone, frmSifarnikKontrolnihGresaka, frmSifarnikKontrolnihTipovaDokumenta, frmStampaRadnogNaloga, frmTelegrami, frmTelegramiPrikazi, frmZavrsnaDokumentacija
- **Osnovni podaci** (17): frmLokomotive, frmNHM, frmPruge, frmPrugeSpisak, frmRazlozi, frmRutaTrase, frmSerijeLokomotiva, frmStanice, frmStatusVoza, frmStavkePrimopredaje, frmTipPrevoza, frmTipTelegrama, frmTrasaStanice, frmTrase, frmTraseAnaliticki, frmVagoniSerije, frmVucaStatusi
- **Terminal** (75): frmCene, frmCIRPregledac, frmDelavci, frmDelovi, frmDirigacijaKontejneraZa, frmDokumentaVozila, frmFormiranjeRobnihDokumenata, frmGreske, frmGrupaVrsteManipulacije, frmInspekciskiTretman, frmIzvozTerminalPovezi, frmKalmarGateIN, frmKalmarGateINKaminonPreneseno, frmKalmarGateInVoz, frmKalmarGateOut, frmKalmarGateOutVoz, frmKontejnerStatus, frmKontejnerTekuceOperacijeLog, frmKopiranjeCenovnikaPoTipu, frmLager, frmMedjuskladisniPrenos, frmMestaUtovara, frmNacinDolaskaOdlaska, frmNalozi, frmOrganizacionaJedinica, frmPopis, frmPopisPregled, frmPopisStampa, frmPopisUporedniPrikaz2, frmPredefinisanePoruke, frmPregledAutoPrevozniList, frmPregledManipulacijaPoPartneru, frmPregledMedjuskladisniPrenos, frmPregledNaloziZaPrevoz, frmPregledOtprema, frmPregledOtpremeKamionom, frmPregledPutnihNaloga, frmPregledRadniNaloziTransport, frmPregledSkladistePrijem, frmPregledTovarnihListova, frmPrijemKontejneraKamionPregled, frmPrometKontejnera, frmRadniNalogPregledSluzbenik, frmScenario, frmSelectTransport1, frmSelectTransport2, frmSelectTransport3, frmSelectTransport4, frmSelectTransport5, frmSelectTransport6, frmSerijeKola, frmSkladisteOtprema, frmSkladistePrijem, frmSredstvoRada, frmTerminalOperacije, frmTerminalProces, frmTipCenovnika, frmTipSkladista, frmTipZone, frmuvKvalitetKontejnera, frmUvozKonacnaZaglavlje, frmVizuelniPregledi, frmVozila, frmVrstaCarinskogPostupka, frmVrstaManipulacije, frmVrstaRobe, frmVrstaRobeADR, frmVrstePlombi, frmVrstePostupakaUvoz, PlaniraniPretovar, RN10PregledIspraznjenogKontejnera, RN11PreglediPostavkaKontejnera, RN8OtpremaCirade, RN9PrijemCirade
- **Uvoz** (19): Carinarnice, FormMessageBox, frmAdresaStatusVozila, frmAnalizaUvoza, frmForrmiranjePlanaDrumski, frmOperativniPlanUvoz, frmPlanUtovaraOdgovorExcel, frmPrebacivanjeIzPlanaUPlan, frmPregledNerasporedjeni, frmPregledPlanovaUtovara, frmPregledPlanoviUtovaraDrumski, frmRAdniNalogInterniPUvoz, frmStatusUsluge, frmUvozKopirajKontejner, ObradaUvoznihUsluga, Uvoz, UvozKonacnaPronadji, UvozTable, UvozZapisniciONepravilnostima

### 6.3 Forme u oba izgleda (114)

DogovoreniPosloviIzvoza, frmAnalizaRadnihNaloga, frmAutomobilEdit, frmAutomobiliDrumski, frmAutoprevozniList2, frmBrojFaktureModal, frmCIR, frmCIRPregled, frmDefinisiPoziciju, frmDodatneUsluge, frmDodatniList, frmDodelaSkladista, frmDokumentaOtpremaKontejnera, frmDokumentaPrijemKontejneraKamion, frmDokumentaPrijemKontejneraVoz, frmDopunaPlanaPraznimIzvoz, frmDrumski, frmDrumski1, frmFormaZaVozace, frmFormiranjePlana, frmFormiranjePlanaIzvoz, frmIzvozDokumenta, frmIzvozKonacna, frmIzvozKonacnaTable, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozPregledKontejneraDrumskeUsluge, frmIzvozUnosManipulacije, frmKamionDetalji, frmKontaktOsobe, frmKontaktOsobeMU, frmKontejneriNaPrijemnici, frmKontejnerTekuce, frmKontejnerTekuceArhiv, frmKorisnici, frmLagerOperater, frmListaProtokola, frmManipulacije, frmNajavaKomitemtaDokument, frmNalogZaPrevoz, frmOtpremaKontejnera, frmOtpremaKontejneraIzvozKamion, frmOtpremaKontejneraKamionomIzKontejnera, frmOtpremaKontejneraLegetIZVOZ, frmOtpremaKontejneraUvozKamion, frmOtpremaVozaIzPlana, frmPartnerji, frmPartnerjiFirme, frmPodesavanjeRaspolozivosti, frmPopisKontejneraStampa, frmPopisKonterjnera, frmPovezivanjeKontejneraIVagona, frmPozicija, frmPregledFajlova, frmPregledKamiona, frmPregledKontejneraDrumskeUsluge, frmPregledNalogaDrumski, frmPregledNarucenihManipulacija, frmPregledOtpreme, frmPregledPorukeVozacu, frmPregledSkladistaNovi, frmPregledVozova, frmPrijemKamionaIzPlana, frmPrijemKontejneraKamionLegetIzvoz, frmPrijemKontejneraKamionLegetUvoz, frmPrijemKontejneraLegetVozUvoz, frmPrijemVozaIzPlana, frmPrijemVozomPregled, frmProtokolTransportnogNaloga, frmPutniNalog, frmRadniNalogInterniPregled, frmRadniNalogTransport, frmScenarioSCI, frmSeniranjeDokumenata, frmSkladista, frmTipKontejnera, frmTipPalete, frmTovarniList, frmUnosManipulacija, frmUvozKonacna, frmUvozKonacnaTable, frmUvozKopirajUslugeKontejnera, frmUvozUvoznici, frmVoz, frmVoziloUsluga, frmZone, IzvestajVaganja, Otpremnica, OtpremnicaPregled, PakovanjeKamiona1, Prijemnica, PrijemnicaPregled, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme, RN7OtpremaPlatforme2, SkladisteGrupa, UvozDokumenta, UvozNHM, Vaganje, VaganjePregled, ZapisnikONepravilnosti, ZapisnikOOstecenjuRobe

## 7. Tabele: novi i stari izgled

S = čitanje, I/U/D = upis/izmena/brisanje direktnim SQL-om; `p` = upis preko procedure prepoznate po imenu (Insert/Update/Delete + tabela). Kolona „Combo“ = koristi se kao izvor combobox-a u novom izgledu.

| Tabela | Novi izgled | Br. formi (N) | Stari izgled | Br. formi (S) | Combo | Status |
|---|---|---|---|---|---|---|
| `1ReportGO` | — | 0 | SIp | 1 |  | samo stari |
| `AdresaStatusVozila` | — | 0 | SIUDp | 1 |  | samo stari |
| `Aktivnosti` | — | 0 | SIUDp | 23 |  | samo stari |
| `AktivnostiDokumenta` | — | 0 | SIp | 4 |  | samo stari |
| `AktivnostiPrvi` | — | 0 | S | 1 |  | samo stari |
| `AktivnostiStavke` | — | 0 | SIDp | 19 |  | samo stari |
| `AktivnostiStavkeDokumenta` | — | 0 | S | 2 |  | samo stari |
| `ApiLogovi` | — | 0 | SIp | 6 |  | samo stari |
| `Appointments` | — | 0 | S | 2 |  | samo stari |
| `Artikli` | SU | 1 | — | 0 |  | samo novi |
| `ArtikliCetvrtiNivo` | S | 1 | — | 0 | da | samo novi |
| `ArtikliDrugiNivo` | S | 1 | — | 0 | da | samo novi |
| `ArtikliPrviNivo` | S | 1 | — | 0 | da | samo novi |
| `ArtikliTreciNivo` | S | 1 | — | 0 | da | samo novi |
| `ATrainListStavke` | — | 0 | S | 1 |  | samo stari |
| `Automobili` | SIUDp | 14 | SIUDp | 26 | da | oba |
| `AutomobiliTehnickiProblem` | SIDp | 5 | SIDp | 3 |  | oba |
| `Autoprevoznilist` | SIUp | 2 | SIUp | 3 |  | oba |
| `BankeFak` | — | 0 | S | 4 |  | samo stari |
| `BeleskaORadu` | S | 3 | — | 0 |  | samo novi |
| `Bolovanje` | — | 0 | SIUDp | 2 |  | samo stari |
| `Brodovi` | S | 6 | SIUDp | 13 | da | oba |
| `Carinarnice` | S | 32 | SIUDp | 34 | da | oba |
| `Cene` | S | 6 | SIUDp | 10 |  | oba |
| `CIR` | S | 2 | S | 2 |  | oba |
| `CIRGreske` | S | 1 | S | 1 |  | oba |
| `CistocaIznutra` | — | 0 | S | 2 |  | samo stari |
| `CistocaIznutraRazduzivanje` | — | 0 | S | 2 |  | samo stari |
| `CistocaSpolja` | — | 0 | S | 2 |  | samo stari |
| `CistocaSpoljaRazduzivanje` | — | 0 | S | 2 |  | samo stari |
| `Config` | — | 0 | S | 1 |  | samo stari |
| `Davek` | — | 0 | S | 4 |  | samo stari |
| `Delavci` | S | 20 | S | 100 | da | oba |
| `DelavciLokMasin` | — | 0 | S | 1 |  | samo stari |
| `DelavciPrugaMasin` | — | 0 | S | 1 |  | samo stari |
| `Delovi` | S | 1 | SIUDp | 2 | da | oba |
| `DelovnaMesta` | — | 0 | SIUDp | 10 |  | samo stari |
| `DirigacijaKontejneraZa` | S | 13 | SIUDp | 20 | da | oba |
| `DObavnica` | SIp | 2 | SIp | 4 |  | oba |
| `DobavnicaPostav` | SIp | 2 | SIp | 2 |  | oba |
| `DogovorenoIzvoz` | SIUDp | 1 | SIUDp | 1 |  | oba |
| `DokumentaFaktureDrumski` | SIp | 1 | SIp | 7 |  | oba |
| `DokumentaNajavaKomitenta` | SIp | 2 | SIp | 2 |  | oba |
| `DokumentaOtpremaKontejneraVoz` | SIDp | 2 | SIDp | 2 |  | oba |
| `DokumentaPregledac` | S | 1 | — | 0 |  | samo novi |
| `DokumentaPrijemKontejneraKamion` | SIp | 1 | SIp | 1 |  | oba |
| `DokumentaPrijemKontejneraVoz` | S | 1 | S | 1 |  | oba |
| `DokumentaRadnogNalogaDrumski` | S | 4 | S | 4 |  | oba |
| `DokumentaSkladiste` | SIp | 1 | SIp | 1 |  | oba |
| `DokumentaVozila` | — | 0 | SIDp | 1 |  | samo stari |
| `Dokumenti` | S | 3 | — | 0 |  | samo novi |
| `Dopust` | — | 0 | S | 4 |  | samo stari |
| `DopustStavke` | — | 0 | SIUDp | 4 |  | samo stari |
| `DrumskiPozicioniranje` | S | 5 | S | 4 | da | oba |
| `Drzave` | SIp | 1 | SIp | 3 |  | oba |
| `EvidencijaCenaRadnik` | — | 0 | SIUDp | 1 |  | samo stari |
| `EvidencijaGOLog` | — | 0 | S | 1 |  | samo stari |
| `EvidencijaKvarova` | — | 0 | SDp | 3 |  | samo stari |
| `EvidencijaKvarovaAuto` | — | 0 | S | 2 |  | samo stari |
| `EvidencijaMesec` | S | 1 | — | 0 |  | samo novi |
| `EvidencijaRada` | S | 2 | — | 0 |  | samo novi |
| `EvidencijaTokoviDokumentacije` | — | 0 | S | 2 |  | samo stari |
| `EvidencijaZahteva` | — | 0 | SUp | 2 |  | samo stari |
| `EvidencujaZahtevaVrsta` | — | 0 | S | 2 |  | samo stari |
| `Faktura` | — | 0 | SIUDp | 10 |  | samo stari |
| `FakturaDrumski` | SIp | 4 | SIp | 5 |  | oba |
| `FakturaDrumskiStavka` | SIUp | 2 | SIUDp | 4 |  | oba |
| `FakturaPostav` | — | 0 | SIDp | 9 |  | samo stari |
| `fn_Podredjeni` | S | 2 | — | 0 |  | samo novi |
| `fnHR_ZahtevPlanOdobravanja` | S | 2 | — | 0 |  | samo novi |
| `GODopust` | S | 4 | — | 0 |  | samo novi |
| `GODopustOsnov` | S | 1 | — | 0 |  | samo novi |
| `GODopustStavke` | SU | 4 | — | 0 |  | samo novi |
| `Greske` | S | 1 | SIUDp | 2 | da | oba |
| `GrupaKvarova` | — | 0 | SIUp | 5 |  | samo stari |
| `GrupaKvarovaAuto` | — | 0 | SIUDp | 4 |  | samo stari |
| `GrupaVrsteManipulacije` | S | 2 | SIUDp | 4 | da | oba |
| `GrupeDokumenata` | S | 1 | — | 0 |  | samo novi |
| `HRLog` | S | 1 | — | 0 |  | samo novi |
| `HTZGrupe` | S | 1 | — | 0 |  | samo novi |
| `HTZOprema` | S | 4 | — | 0 |  | samo novi |
| `HTZReversi` | SU | 2 | — | 0 |  | samo novi |
| `InspekciskiTretman` | S | 16 | SIUDp | 24 | da | oba |
| `Izjave` | — | 0 | S | 4 |  | samo stari |
| `Izvoz` | SIU | 18 | SIU | 18 |  | oba |
| `IzvozDokumenta` | SIDp | 1 | SIDp | 1 |  | oba |
| `IzvozKonacna` | SU | 41 | SU | 38 | da | oba |
| `IzvozKonacnaNapomenePozicioniranja` | SIDp | 2 | SIDp | 2 |  | oba |
| `IzvozKonacnaNHM` | S | 6 | S | 6 |  | oba |
| `IzvozKonacnaVrstaManipulacije` | SIDp | 9 | SIDp | 14 |  | oba |
| `IzvozKonacnaVrstaRobeHS` | S | 3 | S | 4 |  | oba |
| `IzvozKonacnaZaglavlje` | SIUp | 10 | SIUDp | 16 | da | oba |
| `IzvozNapomenePozicioniranja` | SIp | 3 | S | 3 |  | oba |
| `IzvozNHM` | S | 6 | SIDp | 11 |  | oba |
| `IzvozVrstaManipulacije` | SIUDp | 7 | SIUDp | 13 |  | oba |
| `IzvozVrstaRobeHS` | S | 4 | SIDp | 10 |  | oba |
| `Kapija` | SIUDp | 5 | SIUDp | 2 |  | oba |
| `Kartice` | — | 0 | S | 2 |  | samo stari |
| `KarticeForme` | — | 0 | S | 2 |  | samo stari |
| `KarticeKorisnik` | — | 0 | S | 2 |  | samo stari |
| `KarticeMain` | — | 0 | S | 2 |  | samo stari |
| `KomercijalniPregled` | — | 0 | S | 3 |  | samo stari |
| `Komitenti` | S | 8 | S | 10 | da | oba |
| `KontejnerLog` | SIp | 6 | Ip | 1 |  | oba |
| `KontejnerskiTerminali` | S | 26 | SIUDp | 36 | da | oba |
| `KontejnerStatus` | S | 19 | S | 26 | da | oba |
| `KontejnerTekuce` | SIUp | 13 | S | 14 |  | oba |
| `KontejnerTekuceArhiv` | S | 1 | S | 1 |  | oba |
| `KontejnerTekuceOperacije` | — | 0 | SIp | 2 |  | samo stari |
| `KontrolaDokumentacije` | — | 0 | SIUDp | 1 |  | samo stari |
| `KontrolaDokumentacijeGreske` | — | 0 | S | 1 |  | samo stari |
| `KontrolneGreske` | — | 0 | SIUDp | 2 |  | samo stari |
| `KontrolneGreskeTipDokumenta` | — | 0 | SIUDp | 3 |  | samo stari |
| `Korisnici` | SIDp | 24 | SIDp | 23 | da | oba |
| `Kraji` | — | 0 | S | 4 |  | samo stari |
| `KrajnjaDestinacija` | S | 6 | SIUDp | 14 | da | oba |
| `KursnaLista` | — | 0 | S | 1 |  | samo stari |
| `Kvarovi` | — | 0 | SIp | 4 |  | samo stari |
| `KvaroviAuto` | — | 0 | SIp | 3 |  | samo stari |
| `Labels` | — | 0 | S | 1 |  | samo stari |
| `Lager` | S | 1 | — | 0 |  | samo novi |
| `LogAktivnosti` | — | 0 | SIp | 3 |  | samo stari |
| `Lokac` | S | 2 | S | 2 | da | oba |
| `LokomotivaNamirenje` | — | 0 | SDp | 2 |  | samo stari |
| `LokomotivaPopis` | — | 0 | S | 2 |  | samo stari |
| `LokomotivaPredjeno` | — | 0 | S | 1 |  | samo stari |
| `LokomotivaPrijava` | — | 0 | SIUDp | 9 |  | samo stari |
| `LokomotivaPrimopredaja` | — | 0 | S | 1 |  | samo stari |
| `LokomotivaSerija` | — | 0 | SIUDp | 4 |  | samo stari |
| `lokomotivaVrstaNamirenja` | — | 0 | S | 2 |  | samo stari |
| `LokomotiveVrstePopisa` | — | 0 | SDp | 3 |  | samo stari |
| `MagacinskaKnjigaCarinska` | S | 1 | — | 0 |  | samo novi |
| `MagacinskiBrojCarinski` | SIUp | 9 | — | 0 | da | samo novi |
| `MagacinskiBrojevi` | — | 0 | S | 3 |  | samo stari |
| `MagacinskiPredmet` | S | 1 | — | 0 |  | samo novi |
| `MainNovi` | S | 6 | — | 0 |  | samo novi |
| `mainNoviKartice` | SID | 6 | — | 0 |  | samo novi |
| `MainNoviPrava` | SID | 6 | — | 0 |  | samo novi |
| `MapiranjeTipaNaloga` | S | 3 | S | 1 |  | oba |
| `Markers` | — | 0 | S | 1 |  | samo stari |
| `MAticniPodatki` | S | 2 | SIUDp | 17 |  | oba |
| `MerskeEnote` | S | 4 | SIUDp | 12 | da | oba |
| `Mesta` | S | 2 | S | 14 | da | oba |
| `MestaUtovara` | S | 36 | SIUDp | 33 | da | oba |
| `MUPDozvola` | — | 0 | SIp | 1 |  | samo stari |
| `MUPMesto` | — | 0 | SUp | 2 |  | samo stari |
| `NacinDobave` | — | 0 | SIUDp | 3 |  | samo stari |
| `NacinDolaskaOdlaska` | — | 0 | S | 1 |  | samo stari |
| `Najava` | — | 0 | SIUDp | 27 |  | samo stari |
| `NajavaDeljenjeVoza` | — | 0 | SIp | 1 |  | samo stari |
| `NajavaDodatneUsluge` | — | 0 | SIp | 1 |  | samo stari |
| `NajavaDokumenta` | — | 0 | SIDp | 1 |  | samo stari |
| `NajavaLog` | — | 0 | S | 1 |  | samo stari |
| `NajavaPrevoza` | S | 4 | S | 5 |  | oba |
| `NajavaPrevozaTroskovi` | SDp | 2 | SDp | 2 |  | oba |
| `NajavaPrevoznik` | — | 0 | S | 1 |  | samo stari |
| `Nalogodavci` | — | 0 | S | 1 |  | samo stari |
| `NapomenaZaPozicioniranje` | S | 9 | SIUDp | 8 | da | oba |
| `Narocilo` | S | 1 | SIUDp | 5 |  | oba |
| `NarociloPostav` | S | 1 | S | 4 |  | oba |
| `NaruceneManipulacije` | SIDp | 4 | SIDp | 8 |  | oba |
| `NaslovStatusaVozila` | — | 0 | SIUDp | 1 |  | samo stari |
| `NHM` | S | 27 | SIUDp | 43 | da | oba |
| `NNal` | S | 1 | S | 1 |  | oba |
| `NNalP` | S | 1 | S | 1 |  | oba |
| `NosiociTroskova` | — | 0 | SIUp | 9 |  | samo stari |
| `Notifikacije` | — | 0 | SIUDp | 1 |  | samo stari |
| `NPre` | S | 2 | S | 2 |  | oba |
| `NPreP` | S | 2 | S | 2 |  | oba |
| `ObracunZaposleni` | — | 0 | S | 1 |  | samo stari |
| `OceneSposobnosti` | S | 1 | — | 0 |  | samo novi |
| `Odeljenja` | — | 0 | S | 5 |  | samo stari |
| `Opportunity` | — | 0 | SIUDp | 4 |  | samo stari |
| `OrganizacionaJedinice` | — | 0 | S | 1 |  | samo stari |
| `organizacioneJedinice` | S | 25 | S | 29 | da | oba |
| `OsnovGO` | S | 3 | — | 0 |  | samo novi |
| `OtpremaKontejnera` | SUDp | 14 | SUDp | 17 | da | oba |
| `OtpremaKontejneraVozStavke` | SIUDp | 15 | SIUDp | 14 |  | oba |
| `OtpremaKontejneraVozz` | S | 1 | S | 1 |  | oba |
| `OtpremnicaCarinska` | — | 0 | SIUDp | 3 |  | samo stari |
| `OtpremnicaCarinskaStavke` | — | 0 | S | 2 |  | samo stari |
| `Partnerji` | SUDp | 88 | SUDp | 135 | da | oba |
| `PartnerjiDod` | — | 0 | S | 4 |  | samo stari |
| `PartnerjiFirma` | S | 2 | S | 2 |  | oba |
| `partnerjiKontOseba` | S | 5 | S | 7 | da | oba |
| `partnerjiKontOsebaMU` | S | 15 | S | 16 | da | oba |
| `Plombe` | — | 0 | SIUDp | 1 |  | samo stari |
| `Podesavanja` | SU | 3 | — | 0 |  | samo novi |
| `PodtipProtokolaRazno` | S | 1 | S | 1 | da | oba |
| `PolozeneLokomotive` | — | 0 | S | 1 |  | samo stari |
| `PolozenePruge` | — | 0 | S | 1 |  | samo stari |
| `PomOstali` | S | 2 | — | 0 |  | samo novi |
| `Popis` | — | 0 | SIUDp | 2 |  | samo stari |
| `PopisKontejnera` | SIUp | 1 | SIUp | 1 |  | oba |
| `PopisKontejneraStavke` | SIUDp | 1 | SIUDp | 1 |  | oba |
| `PopisStavke` | — | 0 | SUDp | 1 |  | samo stari |
| `PopisTeretnicaStavke` | — | 0 | S | 1 |  | samo stari |
| `PosebniUsoviSmestaja` | S | 1 | — | 0 |  | samo novi |
| `Poste` | SIp | 1 | SIp | 1 |  | oba |
| `PostupakStavkeOtpremaVeza` | S | 1 | S | 1 |  | oba |
| `Pozicija` | SIUDp | 21 | SIUDp | 36 | da | oba |
| `PraviloArtikla` | S | 2 | — | 0 |  | samo novi |
| `PravoAktivnosti` | — | 0 | SIDp | 4 |  | samo stari |
| `Praznici` | S | 2 | — | 0 |  | samo novi |
| `Predefinisaneporuke` | S | 14 | SIUDp | 21 | da | oba |
| `PredefinisanePozicije` | SIp | 2 | SIp | 1 | da | oba |
| `Predvidjanje` | — | 0 | SIUDp | 7 |  | samo stari |
| `PrekovremeiRad` | — | 0 | S | 1 |  | samo stari |
| `PrekovremeniRad` | — | 0 | SIUDp | 2 |  | samo stari |
| `PrijemKontejneraVoz` | S | 15 | S | 21 | da | oba |
| `PrijemKontejneraVozStavke` | S | 18 | S | 21 |  | oba |
| `PrijemnicaCarinska` | — | 0 | SUDp | 5 |  | samo stari |
| `PrijemnicaCarinskaStavke` | — | 0 | S | 3 |  | samo stari |
| `Primljeno` | S | 3 | — | 0 |  | samo novi |
| `PrioritetiNabavke` | S | 1 | — | 0 |  | samo novi |
| `ProdajniNalogIzvoz` | SIUDp | 6 | S | 1 |  | oba |
| `ProdajniNalogIzvozStavke` | SIUp | 7 | S | 1 |  | oba |
| `ProdSkup` | — | 0 | SIDp | 2 |  | samo stari |
| `Promet` | SIp | 14 | SIDp | 23 | da | oba |
| `PrometManipulacije` | — | 0 | SIDp | 3 |  | samo stari |
| `Propratnica` | — | 0 | SIUDp | 1 |  | samo stari |
| `PropratnicaRazduzenje` | — | 0 | S | 1 |  | samo stari |
| `PropratnicaZaduzenje` | — | 0 | S | 1 |  | samo stari |
| `PropratniceRazduzivanjeSlike` | — | 0 | S | 1 |  | samo stari |
| `PropratniceZaduzivanjeSlike` | — | 0 | S | 1 |  | samo stari |
| `ProtokolRadniNalogDrumski` | SIUDp | 6 | SIUDp | 3 |  | oba |
| `Pruga` | — | 0 | S | 8 |  | samo stari |
| `PrugaStavke` | — | 0 | SIUp | 2 |  | samo stari |
| `PutniNalog` | S | 4 | S | 5 | da | oba |
| `PutniNalogGorivo` | SUDp | 2 | SUDp | 2 |  | oba |
| `PutniNalogGume` | S | 2 | S | 2 |  | oba |
| `PutniNalogKvarovi` | SIUDp | 2 | SIUDp | 2 |  | oba |
| `PutniNalogRuta` | S | 2 | S | 2 |  | oba |
| `PutniNalogServis` | S | 2 | S | 2 |  | oba |
| `PutniNalogTroskovi` | S | 2 | S | 2 |  | oba |
| `PutniNalogZaposleni` | S | 2 | S | 2 | da | oba |
| `RadnaMesta` | S | 2 | — | 0 |  | samo novi |
| `RadniNalog` | — | 0 | SIUp | 8 |  | samo stari |
| `RadniNalogDokumenta` | — | 0 | SIp | 1 |  | samo stari |
| `RadniNalogDrumski` | SIUDp | 15 | SIUDp | 21 | da | oba |
| `RadniNalogDrumskiIzmene` | — | 0 | S | 1 |  | samo stari |
| `RadniNalogDrumskiNHM` | SIp | 1 | SIp | 1 |  | oba |
| `RadniNalogInterni` | SIp | 44 | SIUDp | 38 | da | oba |
| `RadniNalogInterniPotvrda` | SIp | 15 | SIp | 2 |  | oba |
| `RadniNalogInterniSkladistePotvrda` | S | 2 | — | 0 |  | samo novi |
| `RadniNalogLokNaTrasi` | — | 0 | SIUDp | 6 |  | samo stari |
| `RadniNalogSkladista` | SIUp | 15 | — | 0 | da | samo novi |
| `RadniNalogTeretnice` | — | 0 | S | 1 |  | samo stari |
| `RadniNalogTransport` | S | 4 | S | 5 |  | oba |
| `RadniNalogTrase` | — | 0 | SUDp | 6 |  | samo stari |
| `RadniNalogTraseLok` | — | 0 | S | 1 |  | samo stari |
| `RadniNalogTraseLokUzr` | — | 0 | S | 1 |  | samo stari |
| `RadniNalogTraseLokZap` | — | 0 | S | 4 |  | samo stari |
| `RadniNalogTraseLokZapEvid` | — | 0 | S | 1 |  | samo stari |
| `RadniNalogVezaNajave` | — | 0 | SIDp | 6 |  | samo stari |
| `RadniOdnosi` | S | 1 | — | 0 |  | samo novi |
| `RadnoMestoNormativHTZ` | S | 1 | — | 0 |  | samo novi |
| `RadnoMestoObavezniDokumenti` | S | 1 | — | 0 |  | samo novi |
| `RadnoMestoObavezniPregledi` | S | 1 | — | 0 |  | samo novi |
| `RasporedRada` | S | 3 | — | 0 |  | samo novi |
| `Razlozi` | — | 0 | SIUp | 4 |  | samo stari |
| `RazloziRazduzenja` | S | 1 | — | 0 |  | samo novi |
| `Razporeditve` | — | 0 | S | 1 |  | samo stari |
| `RazporeditveStrMesto` | — | 0 | S | 1 |  | samo stari |
| `Registrator` | — | 0 | SIUDp | 2 |  | samo stari |
| `RegistratorDokumenta` | — | 0 | SIp | 1 |  | samo stari |
| `Reminders` | — | 0 | S | 1 |  | samo stari |
| `Rezervisano` | S | 1 | — | 0 |  | samo novi |
| `RNCarinskoPackingLista` | SIU | 1 | — | 0 |  | samo novi |
| `RNCarinskoPackingListaStavka` | SIU | 1 | — | 0 |  | samo novi |
| `RNCarinskoPaleta` | S | 1 | — | 0 |  | samo novi |
| `RNCarinskoPaletaStavka` | S | 1 | — | 0 |  | samo novi |
| `RNCarinskoPrijemnicaStavke` | SIDp | 4 | — | 0 | da | samo novi |
| `RNCarinskoSkladisteDodatneUsluge` | SIDp | 7 | — | 0 |  | samo novi |
| `RNCarinskoSkladisteOtpremaStavkePom` | SIDp | 4 | — | 0 |  | samo novi |
| `RNCarinskoSkladisteOtpremnica` | SIUp | 4 | — | 0 |  | samo novi |
| `RNCarinskoSkladisteOtpremnicaStavke` | S | 4 | — | 0 |  | samo novi |
| `RNCarinskoSkladistePrijemnica` | SIUp | 6 | — | 0 | da | samo novi |
| `RNCarinskoSkladisteRukovalac` | S | 3 | — | 0 |  | samo novi |
| `RNCarinskoStavke` | S | 1 | — | 0 |  | samo novi |
| `RNMedjuskladisni` | SUDp | 3 | SUDp | 2 |  | oba |
| `RNOtpremaCirade` | S | 3 | SIUDp | 5 |  | oba |
| `RNOtpremaPlatforme` | SIUDp | 5 | SIUDp | 5 |  | oba |
| `RNOtpremaPlatforme2` | SIUDp | 2 | SIUDp | 3 |  | oba |
| `RNOtpremaVoza` | SIUDp | 3 | SIUDp | 4 |  | oba |
| `RNPregledIpostavkaKontejnera` | S | 1 | SIUDp | 2 |  | oba |
| `RNPregledIspraznjenogKontejnera` | S | 1 | SIUDp | 2 |  | oba |
| `RNPrijemCirade` | S | 3 | SIUDp | 5 |  | oba |
| `RNPrijemPlatforme` | SIUDp | 8 | SIUDp | 9 |  | oba |
| `RNPrijemPlatforme2` | SIUDp | 3 | SIUDp | 5 |  | oba |
| `RNPrijemVoza` | SIUDp | 6 | SIUDp | 9 |  | oba |
| `RuteTrase` | — | 0 | S | 1 |  | samo stari |
| `ScenarijaTokaTransporta` | S | 3 | S | 1 |  | oba |
| `Scenario` | S | 13 | SIUp | 19 | da | oba |
| `ScenarioTokTransporta_Statusi` | S | 3 | S | 1 |  | oba |
| `SERDeo` | S | 9 | — | 0 |  | samo novi |
| `SERDeoGrupa` | S | 1 | — | 0 |  | samo novi |
| `SERDeoKretanje` | S | 3 | — | 0 |  | samo novi |
| `SERDeoPrimenljivost` | SD | 2 | — | 0 |  | samo novi |
| `SERDeoRezervacija` | S | 1 | — | 0 |  | samo novi |
| `SERDokument` | S | 1 | — | 0 |  | samo novi |
| `SerijeKola` | S | 4 | SIUDp | 5 | da | oba |
| `SERKorekcija` | S | 2 | — | 0 |  | samo novi |
| `SERNarudzbenicaStavka` | S | 1 | — | 0 |  | samo novi |
| `SERObavestenje` | SU | 2 | — | 0 |  | samo novi |
| `SERPlanNormativ` | SUD | 1 | — | 0 |  | samo novi |
| `SERPlanStavka` | S | 3 | — | 0 |  | samo novi |
| `SERPopisStavka` | S | 1 | — | 0 |  | samo novi |
| `SERRadniNalog` | S | 2 | — | 0 |  | samo novi |
| `SERSredstvo` | S | 4 | — | 0 |  | samo novi |
| `SERStatus` | S | 2 | — | 0 |  | samo novi |
| `SERVrstaSredstva` | S | 2 | — | 0 |  | samo novi |
| `SifGrupaNepravilnosti` | — | 0 | SIUDp | 3 |  | samo stari |
| `SifNeispravnostPostupak` | — | 0 | SIUDp | 3 |  | samo stari |
| `SifOpisNeispravnosti` | — | 0 | SIUDp | 4 |  | samo stari |
| `SifRazredNepravilnosti` | — | 0 | SIUDp | 3 |  | samo stari |
| `SifStavkePrimopredaje` | — | 0 | S | 1 |  | samo stari |
| `SifVrstaNepravilnosti` | — | 0 | SIUDp | 4 |  | samo stari |
| `SifVucaStatusi` | — | 0 | S | 4 |  | samo stari |
| `SistemskePostavke` | S | 7 | S | 6 | da | oba |
| `Sklad` | S | 2 | S | 4 | da | oba |
| `Skladista` | SIUDp | 34 | SIUDp | 54 | da | oba |
| `SkladistaGrupa` | SIUp | 7 | SIUp | 5 | da | oba |
| `Skladiste_Korisnik` | S | 1 | S | 1 |  | oba |
| `SkladisteObjekat` | S | 3 | S | 2 |  | oba |
| `Srodstva` | S | 1 | — | 0 |  | samo novi |
| `Stanice` | S | 6 | SIUDp | 49 | da | oba |
| `StatusDelNaloga` | — | 0 | S | 1 |  | samo stari |
| `StatusiStavkiNabavke` | S | 1 | — | 0 |  | samo novi |
| `StatusiZahtevaZaNabavku` | S | 2 | — | 0 |  | samo novi |
| `StatusKapija` | S | 2 | SIUDp | 3 | da | oba |
| `StatusKvara` | — | 0 | S | 3 |  | samo stari |
| `StatusKvaraAuto` | — | 0 | S | 1 |  | samo stari |
| `StatusRobe` | S | 2 | S | 2 | da | oba |
| `StatusTrasa` | — | 0 | S | 1 |  | samo stari |
| `StatusVoza` | — | 0 | SIUDp | 3 |  | samo stari |
| `StatusVozila` | S | 10 | SIUDp | 12 | da | oba |
| `StatusZahteva` | S | 2 | — | 0 |  | samo novi |
| `StepenObrazovanja` | S | 1 | — | 0 |  | samo novi |
| `Stock` | S | 2 | — | 0 | da | samo novi |
| `Tecaj` | — | 0 | S | 4 |  | samo stari |
| `TehnickiPregled` | — | 0 | S | 2 |  | samo stari |
| `Tehnologija` | — | 0 | SIUDp | 4 |  | samo stari |
| `TehnologijaAktivnosti` | — | 0 | S | 1 |  | samo stari |
| `TehnologijaTrasaLokomotiva` | — | 0 | SIUDp | 1 |  | samo stari |
| `TehnologijaTrase` | — | 0 | SIUp | 1 |  | samo stari |
| `telegrami` | — | 0 | SIUDp | 2 |  | samo stari |
| `Teretnica` | — | 0 | SUDp | 8 |  | samo stari |
| `TeretnicaIskljuceniVagoni` | — | 0 | S | 6 |  | samo stari |
| `TeretnicaKomercijalniPregled` | — | 0 | S | 1 |  | samo stari |
| `TeretnicaStavke` | — | 0 | SUDp | 14 |  | samo stari |
| `TerminalOperacije` | — | 0 | SIUDp | 2 |  | samo stari |
| `TerminalPriv` | SIUDp | 2 | — | 0 |  | samo novi |
| `TerminalProces` | — | 0 | SIUDp | 1 |  | samo stari |
| `TerminalRadniNalozi` | S | 1 | S | 1 |  | oba |
| `testovi` | — | 0 | S | 3 |  | samo stari |
| `TestoviGenerisanjaPitanja` | — | 0 | S | 1 |  | samo stari |
| `testovipitanja` | — | 0 | S | 2 |  | samo stari |
| `TestoviPitanjaOdgovor` | — | 0 | S | 1 |  | samo stari |
| `TezinePovreda` | S | 1 | — | 0 |  | samo novi |
| `this` | — | 0 | D | 1 |  | samo stari |
| `TipCenovnika` | — | 0 | SIUDp | 3 |  | samo stari |
| `TipKomercijalnogDokumenta` | S | 2 | SDp | 3 | da | oba |
| `TipKontenjera` | SIUDp | 59 | SIUDp | 82 | da | oba |
| `TipManipulacije` | — | 0 | S | 1 |  | samo stari |
| `TipPalete` | SIUDp | 9 | SIUDp | 2 | da | oba |
| `TipProtokola` | S | 3 | S | 1 |  | oba |
| `TipRobe` | S | 1 | S | 1 |  | oba |
| `TipSaobPrevoza` | — | 0 | SIUDp | 5 |  | samo stari |
| `TipSkladista` | S | 3 | SIUDp | 3 | da | oba |
| `TipTelegrama` | — | 0 | SIUDp | 1 |  | samo stari |
| `TipZavrsnogDokumentaID` | — | 0 | S | 1 |  | samo stari |
| `TipZone` | S | 2 | SIUDp | 3 | da | oba |
| `TovarniList` | S | 1 | S | 2 |  | oba |
| `TrainList` | — | 0 | S | 5 |  | samo stari |
| `TrainListStavke` | — | 0 | S | 1 |  | samo stari |
| `TrasaStanice` | — | 0 | SIUDp | 1 |  | samo stari |
| `Trase` | — | 0 | SIUDp | 17 |  | samo stari |
| `UlFak` | — | 0 | SIUDp | 5 |  | samo stari |
| `UlFakPostav` | — | 0 | SIDp | 4 |  | samo stari |
| `UljeAuto` | — | 0 | S | 2 |  | samo stari |
| `UljeAutoRazduzivanje` | — | 0 | S | 2 |  | samo stari |
| `UploadedFiles` | — | 0 | S | 4 |  | samo stari |
| `UploadTokens` | S | 3 | S | 1 |  | oba |
| `uvKvalitetKontejnera` | S | 12 | SIUDp | 7 | da | oba |
| `uvNacinPakovanja` | S | 20 | SIUDp | 29 | da | oba |
| `Uvoz` | SIU | 16 | SIUDp | 20 |  | oba |
| `UvozDokumenta` | SIDp | 1 | SIDp | 1 |  | oba |
| `UvozDokumentaCeoVoz` | SIDp | 1 | SIDp | 1 |  | oba |
| `UvozDokumentaUsluge` | SIDp | 1 | SIDp | 1 |  | oba |
| `UvozKonacna` | SUDp | 31 | SIUDp | 43 |  | oba |
| `UvozKonacnaLogExcel` | — | 0 | S | 1 |  | samo stari |
| `UvozKonacnaNapomenePozicioniranja` | SIDp | 6 | SIDp | 5 |  | oba |
| `UvozKonacnaNHM` | SIDp | 9 | SIDp | 12 |  | oba |
| `UvozKonacnaVrstaManipulacije` | SIDp | 12 | SIDp | 18 |  | oba |
| `UvozKonacnaVrstaRobeHS` | SDp | 1 | SDp | 2 |  | oba |
| `UvozKonacnaZaglavlje` | SUDp | 13 | SIUDp | 25 | da | oba |
| `UvozNapomenePozicioniranja` | S | 4 | SIDp | 7 |  | oba |
| `UvozNHM` | SIDp | 2 | SIDp | 5 |  | oba |
| `UvozUvoznici` | SIDp | 2 | SIDp | 4 |  | oba |
| `UvozVrstaManipulacije` | SIUDp | 4 | SIUDp | 7 |  | oba |
| `UvozVrstaRobeHS` | S | 1 | SIDp | 3 |  | oba |
| `uvuvozkonacnaoz` | U | 1 | U | 1 |  | oba |
| `Vaganje` | SIUp | 2 | SIUp | 2 |  | oba |
| `VagoniSerije` | — | 0 | SIUDp | 3 |  | samo stari |
| `Valute` | S | 7 | SIUDp | 14 | da | oba |
| `VlasniciRobe` | S | 2 | — | 0 |  | samo novi |
| `Voz` | SUDp | 38 | SUDp | 62 | da | oba |
| `Vozila` | S | 7 | SIUDp | 14 | da | oba |
| `VoziloUsluga` | SIUDp | 4 | SIUDp | 4 |  | oba |
| `VozSerijeKola` | S | 4 | S | 4 |  | oba |
| `VrstaAktivnosti` | — | 0 | SIUDp | 18 |  | samo stari |
| `VrstaCarinskogPostupka` | S | 32 | SIUDp | 38 | da | oba |
| `VrstaManipulacije` | S | 46 | SIUDp | 62 | da | oba |
| `VrstaPopisa` | — | 0 | SIUDp | 4 |  | samo stari |
| `VrstaRada` | S | 3 | — | 0 |  | samo novi |
| `VrstaRobe` | S | 9 | SIUDp | 12 | da | oba |
| `VrstaRobeADR` | S | 20 | SIUDp | 27 | da | oba |
| `VrstaRobeHS` | S | 7 | SIUp | 17 | da | oba |
| `VrstaVozila` | S | 20 | SIUDp | 11 | da | oba |
| `VrsteDisciplinskihMera` | S | 1 | — | 0 |  | samo novi |
| `VrsteDokumenta` | S | 2 | — | 0 |  | samo novi |
| `VrsteNabavke` | S | 2 | — | 0 |  | samo novi |
| `VrstePlombi` | — | 0 | SIUDp | 1 |  | samo stari |
| `VrstePostupakaUvoz` | S | 21 | SIUDp | 37 | da | oba |
| `VrstePregleda` | S | 2 | — | 0 |  | samo novi |
| `VrstePrevoznogSredstva` | S | 7 | — | 0 | da | samo novi |
| `VrsteUgovora` | S | 1 | — | 0 |  | samo novi |
| `vSERDeoStanje` | S | 3 | — | 0 |  | samo novi |
| `vSERKarticaDela` | S | 1 | — | 0 |  | samo novi |
| `vSERNarudzbenica` | S | 4 | — | 0 | da | samo novi |
| `vSERNarudzbenicaStavka` | S | 3 | — | 0 |  | samo novi |
| `vSERPopis` | S | 1 | — | 0 |  | samo novi |
| `vSERPredlogNabavke` | S | 2 | — | 0 |  | samo novi |
| `vSERPromet` | S | 1 | — | 0 |  | samo novi |
| `vSERRadniNalogDelovi` | S | 1 | — | 0 |  | samo novi |
| `vSERRadniNalogTrosak` | S | 2 | — | 0 |  | samo novi |
| `Vuca` | — | 0 | S | 3 |  | samo stari |
| `VucaSlike` | — | 0 | S | 1 |  | samo stari |
| `VucaStatusi` | — | 0 | S | 3 |  | samo stari |
| `vw_EvidencijaZaObracun` | S | 1 | — | 0 |  | samo novi |
| `vZaposleniRokovi` | S | 1 | — | 0 |  | samo novi |
| `vZaposleniTrenutno` | S | 8 | — | 0 |  | samo novi |
| `vZaposleniUpozorenja` | S | 2 | — | 0 |  | samo novi |
| `ZahteviZaNabavku` | S | 2 | — | 0 |  | samo novi |
| `ZahtevZaNabavkuIstorijaStatusa` | S | 1 | — | 0 |  | samo novi |
| `ZahtevZaNabavkuOdobrenja` | S | 2 | — | 0 |  | samo novi |
| `ZahtevZaNabavkuPonude` | SD | 1 | — | 0 |  | samo novi |
| `ZahtevZaNabavkuPrilozi` | SD | 1 | — | 0 |  | samo novi |
| `ZahtevZaNabavkuStavke` | SUD | 2 | — | 0 |  | samo novi |
| `ZapisnikONepravilnosti` | SIDp | 1 | SIDp | 1 |  | oba |
| `ZapisnikOOstecenju` | SIDp | 1 | SIDp | 2 |  | oba |
| `Zaposleni` | SIU | 16 | S | 10 | da | oba |
| `ZaposleniClanoviPorodice` | S | 1 | — | 0 |  | samo novi |
| `ZaposleniDisciplinske` | S | 1 | — | 0 |  | samo novi |
| `ZaposleniDokumenti` | S | 1 | — | 0 |  | samo novi |
| `ZaposleniHitniKontakti` | S | 1 | — | 0 |  | samo novi |
| `ZaposleniHTZZaduzenja` | S | 2 | — | 0 |  | samo novi |
| `ZaposleniMentorBeleske` | S | 1 | — | 0 |  | samo novi |
| `ZaposleniMentori` | S | 1 | — | 0 |  | samo novi |
| `ZaposleniOcene` | S | 1 | — | 0 |  | samo novi |
| `ZaposleniOsnovGO` | S | 1 | — | 0 |  | samo novi |
| `ZaposleniPovrede` | S | 1 | — | 0 |  | samo novi |
| `ZaposleniPrijava` | — | 0 | SDp | 4 |  | samo stari |
| `ZaposleniPrijavaAuto` | — | 0 | S | 3 |  | samo stari |
| `ZaposleniPrilozi` | SD | 1 | — | 0 |  | samo novi |
| `ZaposleniRadnaMesta` | S | 1 | — | 0 |  | samo novi |
| `ZaposleniSkolovanje` | S | 1 | — | 0 |  | samo novi |
| `ZaposleniSlike` | SD | 1 | — | 0 |  | samo novi |
| `ZaposleniStatusi` | S | 1 | — | 0 |  | samo novi |
| `ZaposleniUgovori` | S | 1 | — | 0 |  | samo novi |
| `ZaposleniZdravstveniPregledi` | S | 1 | — | 0 |  | samo novi |
| `Zarada` | — | 0 | SIUDp | 2 |  | samo stari |
| `Zavrseno` | S | 3 | — | 0 |  | samo novi |
| `ZavrsnaDokumenta` | — | 0 | S | 1 |  | samo stari |
| `Zone` | SIUDp | 5 | SIUDp | 2 |  | oba |

## 8. Forme — detalji

Oznaka uz ime: **[N]** samo novi izgled, **[S]** samo stari, **[N+S]** oba, **[P]** prijava. Za forme novog izgleda navedeni su i combobox-ovi sa tabelama koje ih pune.

#### Administracija1 [N]
- **Fajl:** `Administracija/Administracija1.cs` — „Administracija“
- **Novi izgled:** Administracija → **Administracija1**
- **Otvara:** frmKorisnici („Korisnici“), AdministracijaPravoPristupa („Prava“), frmPodesavanja („Podešavanja“)
- **Funkcionalnosti:**
  - „Korisnici“ — klik → `btnKorisnici_Click` — otvara: frmKorisnici
  - „Prava“ — klik → `btnPrava_Click` — otvara: AdministracijaPravoPristupa
  - „Podešavanja“ — klik → `btnPodesavanja_Click` — otvara: frmPodesavanja

#### AdministracijaPravoPristupa [N]
- **Fajl:** `MainLeget/AdministracijaPravoPristupa.cs` — „AdministracijaPravoPristupa“
- **Novi izgled:** !!! → **AdministracijaPravoPristupa**
- **Tabele:** `Korisnici` S, `MainNovi` S, `mainNoviKartice` SID, `MainNoviPrava` SID
- **Combobox → tabela:** `cboKorisnik` ← `Korisnici`
- **Funkcionalnosti:**
  - „Dodeli pravo korisniku“ — klik → `button1_Click` — piše: MainNoviPrava[DI]
  - `miDodaj` — klik → `MiDodajFormu_Click` — piše: mainNoviKartice[I]
  - `miObrisi` — klik → `MiObrisiFormu_Click` — piše: MainNoviPrava[D], mainNoviKartice[D]

#### ApiPregled [S]
- **Fajl:** `Pantheon Export/ApiPregled.cs` — „ApiPregled“
- **Stari izgled:** Komercijala › CRM › apiPregled → **ApiPregled**
- **Otvara:** DraganStatusi („Nosioci troškova“)
- **Tabele:** `ApiLogovi` S
- **Funkcionalnosti:**
  - „Nosioci troškova“ — klik → `toolStripButton1_Click` — otvara: DraganStatusi
  - „Predviđanja“ — klik → `toolStripButton2_Click` — otvara: DraganStatusi
  - „Izlazne fakture“ — klik → `toolStripButton3_Click` — otvara: DraganStatusi
  - „Ulazne fakture“ — klik → `toolStripButton4_Click` — otvara: DraganStatusi
  - `this` — otvaranje forme → `ApiPregled_Load` — čita: ApiLogovi

#### Brodovi [S]
- **Fajl:** `Uvoz/Brodovi.cs` — „Brodovi“
- **Stari izgled:** Izvoz › Šifarnici › Brodovi → **Brodovi**
- **Tabele:** `Brodovi` SIUD p
- **Procedure:** `DeleteBrodovi`, `InsertBrodovi`, `UpdateBrodovi`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertBrodovi, UpdateBrodovi; preko: InsertBrodovi.InsBrodovi, InsertBrodovi.UpdBrodovi
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteBrodovi; preko: InsertBrodovi.DelBrodovi
  - `button20` — klik → `tsDelete_Click` — proc: DeleteBrodovi; preko: InsertBrodovi.DelBrodovi
  - `button21` — klik → `tsSave_Click` — proc: InsertBrodovi, UpdateBrodovi; preko: InsertBrodovi.InsBrodovi, InsertBrodovi.UpdBrodovi

#### Carinarnice [S]
- **Fajl:** `Uvoz/Carinarnice.cs` — „Carinske ispostave“
- **Stari izgled:** Uvoz › Šifarnici › Carinarnice → **Carinarnice**
- **Tabele:** `Carinarnice` SIUD p
- **Procedure:** `DeleteCarinarnice`, `InsertCarinarnice`, `UpdateCarinarnice`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertCarinarnice, UpdateCarinarnice; preko: InsertCarinarnice.InsCarinarnice, InsertCarinarnice.UpdCarinarnice
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteCarinarnice; preko: InsertCarinarnice.DelCarinarnice
  - `button20` — klik → `tsDelete_Click` — proc: DeleteCarinarnice; preko: InsertCarinarnice.DelCarinarnice
  - `button21` — klik → `tsSave_Click` — proc: InsertCarinarnice, UpdateCarinarnice; preko: InsertCarinarnice.InsCarinarnice, InsertCarinarnice.UpdCarinarnice

#### CarinskoSkladiste [N]
- **Fajl:** `Skladista main/CarinskoSkladiste.cs` — „CarinskoSkladiste“
- **Novi izgled:** Operacije skladišta → **SkladistaMain** → „Carinsko skladište“ → **CarinskoSkladiste**
- **Otvara:** CarinskoSkladistePregled („Prijem robe“), MagacinskaKnjigaCarinska („Magacinska knjiga“), PregledLagera („Pregled lagera robe“)
- **Funkcionalnosti:**
  - „Pretovar robe“ — klik → `btnPretovarRobe_Click` — otvara: CarinskoSkladistePregled
  - „Pregled kartica po klijentu“ — klik → `btnPregledKartica_Click` — otvara: PregledLagera
  - „Pregled lagera robe“ — klik → `btnPregledLagera_Click` — otvara: PregledLagera
  - „Magacinska knjiga“ — klik → `btnMagacinskaKnjiga_Click` — otvara: MagacinskaKnjigaCarinska
  - „Otprema robe“ — klik → `btnOtpremaRobe_Click` — otvara: CarinskoSkladistePregled
  - „Prijem robe“ — klik → `btnPrijemRobe_Click` — otvara: CarinskoSkladistePregled

#### CarinskoSkladistePregled [N]
- **Fajl:** `Skladista main/CarinskoSkladistePregled.cs` — „CarinskoSkladistePregled“
- **Novi izgled:** Operacije skladišta → **SkladistaMain** → „Carinsko skladište“ → **CarinskoSkladiste** → „Prijem robe“ → **CarinskoSkladistePregled**
- **Otvara:** frmNalogPrijem („Magacinski predmet“), Otprema („Magacinski predmet“), ViljuskaristiPregled („Radni nalozi“), Prijemnica („Prijemnica“), frmIzvestajCarinskoObelezje („Štampaj“), frmSeniranjeDokumenata („Skeniraj“)
- **Tabele:** `Partnerji` S, `RadniNalogInterni` S, `RadniNalogInterniPotvrda` S, `RadniNalogSkladista` S
- **Funkcionalnosti:**
  - „Skeniraj“ — klik → `button5_Click` — otvara: frmSeniranjeDokumenata
  - „Štampaj“ — klik → `button4_Click` — otvara: frmIzvestajCarinskoObelezje
  - „Radni nalozi“ — klik → `btnViljuskarista_Click` — otvara: ViljuskaristiPregled
  - „Prijemnica“ — klik → `button25_Click` — otvara: Prijemnica
  - „Magacinski predmet“ — klik → `button24_Click` — otvara: frmNalogPrijem, Otprema; čita: RadniNalogInterni

#### Cerade [N]
- **Fajl:** `MainLeget/Drumski/Cerade.cs` — „Cerade“
- **Novi izgled:** Drumski transport → **Drumski1** → „Cerada“ → **Cerade**
- **Otvara:** NalogZaDrumski („NALOG ZA DRUMSKI TRANSPORT“), frmStatus („STATUSI VOZILA“), frmPodesavanjeRaspolozivosti („PROVERA \r\nRASPOLOŽIVOSTI VOZILA“), PakovanjeKamionaCerade („FORMIRANJE\r\nTRANSPORTNOG NALOGA“), frmNalogZaFakturisanje („NALOG ZA FAKTURISANJE“), frmAutomobiliDrumski („UNOS PREVOZNIK/KAMIONER“)
- **Funkcionalnosti:**
  - „STATUSI VOZILA“ — klik → `btnStatusi_Click` — otvara: frmStatus
  - „PROVERA \r\nRASPOLOŽIVOSTI VOZILA“ — klik → `btnProvera_Click` — otvara: frmPodesavanjeRaspolozivosti
  - „FORMIRANJE\r\nTRANSPORTNOG NALOGA“ — klik → `btnFormiranjeNaloga_Click` — otvara: PakovanjeKamionaCerade
  - „NALOG ZA FAKTURISANJE“ — klik → `btnNalogFakturisanje_Click` — otvara: frmNalogZaFakturisanje
  - „UNOS PREVOZNIK/KAMIONER“ — klik → `btnPonude_Click` — otvara: frmAutomobiliDrumski
  - „NALOG ZA DRUMSKI TRANSPORT“ — klik → `btnNalogDrumski_Click` — otvara: NalogZaDrumski

#### DepoMain [N]
- **Fajl:** `MainLeget/Depocnt/DepoMain.cs` — „DepoMain“
- **Novi izgled:** Depo → **DepoMain**

#### DodavanjeSkeniraneFakture [S]
- **Fajl:** `Drumski/DodavanjeSkeniraneFakture.cs` — „DodavanjeSkeniraneFakture“
- **Stari izgled:** Drumski › Finansije › Lista naloga → **frmPregledFaktura** → „Pregled i izmena podataka fakture“ → **frmFaktureDetalji** → „Dodaj skeniranu fakturu“ → **DodavanjeSkeniraneFakture**
- **Tabele:** `Automobili` S, `Delavci` S, `DokumentaFaktureDrumski` SI p, `FakturaDrumski` S, `FakturaDrumskiStavka` S, `Korisnici` S, `Partnerji` S, `RadniNalogDrumski` S
- **Procedure:** `InsertDokumentaFaktureDrumski`
- **Funkcionalnosti:**
  - `button2` — klik → `btnSnimi_Click` — čita: DokumentaFaktureDrumski; proc: InsertDokumentaFaktureDrumski; preko: InsertFakture.SnimiUFajlBazu
  - „Odabir dokumenata“ — klik → `btnUpload_Click` — čita: DokumentaFaktureDrumski

#### DodavanjeSkeniranePrevoznice [S]
- **Fajl:** `Drumski/DodavanjeSkeniranePrevoznice.cs` — „DodavanjeSkeniranePrevoznice“
- **Stari izgled:** Drumski › Finansije › Lista naloga → **frmPregledFaktura** → „Pregled i izmena podataka fakture“ → **frmFaktureDetalji** → „Dodaj skeniranu prevoznicu“ → **DodavanjeSkeniranePrevoznice**
- **Tabele:** `Automobili` S, `Delavci` S, `DokumentaFaktureDrumski` SI p, `Korisnici` S, `Partnerji` S, `RadniNalogDrumski` S
- **Procedure:** `InsertDokumentaFaktureDrumski`
- **Funkcionalnosti:**
  - `button2` — klik → `btnSnimi_Click` — čita: DokumentaFaktureDrumski; proc: InsertDokumentaFaktureDrumski; preko: InsertFakture.SnimiUFajlBazu
  - „Odabir dokumenata“ — klik → `btnUpload_Click` — čita: DokumentaFaktureDrumski

#### DogovoreniPosloviIzvoza [N+S]
- **Fajl:** `Izvoz/DogovoreniPosloviIzvoza.cs` — „DOGOVORENI POSLOVI “
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Pregled naloga“ → **frmPrijemIOtpremaVozova3** → „Dogovoreni poslovi izvoza“ → **DogovoreniPosloviIzvoza**
- **Stari izgled:** Izvoz › Izvoz › Dogovoreni poslovi → **DogovoreniPosloviIzvoza**
- **Tabele:** `DogovorenoIzvoz` SIUD p, `Partnerji` S
- **Procedure:** `DeleteDogovorenoIzvoz`, `InsertDogovorenoIzvoz`, `UpdateDogovorenoIzvoz`
- **Combobox → tabela:** `cboPartner` ← `Partnerji`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertDogovorenoIzvoz, UpdateDogovorenoIzvoz; preko: InsertDogovorenoIzvoz.InsDogovorenoIzvoz, InsertDogovorenoIzvoz.UpdDogovorenoIzvoz
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteDogovorenoIzvoz; preko: InsertDogovorenoIzvoz.DelDogovorenoIzvoz
  - `button20` — klik → `tsDelete_Click` — proc: DeleteDogovorenoIzvoz; preko: InsertDogovorenoIzvoz.DelDogovorenoIzvoz
  - `button21` — klik → `tsSave_Click` — proc: InsertDogovorenoIzvoz, UpdateDogovorenoIzvoz; preko: InsertDogovorenoIzvoz.InsDogovorenoIzvoz, InsertDogovorenoIzvoz.UpdDogovorenoIzvoz
  - `this` — otvaranje forme → `DogovoreniPosloviIzvoza_Load` — čita: PArtnerji

#### DraganStatusi [S]
- **Fajl:** `Pantheon Export/DraganStatusi.cs` — „DraganStatusi“
- **Stari izgled:** Komercijala › CRM › apiPregled → **ApiPregled** → „Nosioci troškova“ → **DraganStatusi**
- **Tabele:** `Delavci` S, `Faktura` SU, `FakturaPostav` S, `Izjave` S, `MAticniPodatki` S, `NosiociTroskova` SU, `Odeljenja` S, `Opportunity` S, `Partnerji` S, `Predvidjanje` SU, `UlFak` SU, `UlFakPostav` S
- **Funkcionalnosti:**
  - „Update selektovano“ — klik → `button1_Click` — piše: Faktura[U], NosiociTroskova[U], Predvidjanje[U], UlFak[U]
  - `this` — otvaranje forme → `DraganStatusi_Load` — čita 12 tabela

#### Drumski1 [N]
- **Fajl:** `MainLeget/Drumski/Drumski1.cs` — „MainLeget“
- **Novi izgled:** Drumski transport → **Drumski1**
- **Otvara:** Cerade („Cerada“), Platforma („Platforme“), VanGabaritni („Van Gabaritni“), frmObradaUlaznihFaktura („Kontrola i obračun transporta“), frmNalogZaFakturisanje („NALOG ZA FAKTURISANJE“)
- **Funkcionalnosti:**
  - „Cerada“ — klik → `btnForm1_Click` — otvara: Cerade
  - „Platforme“ — klik → `sfButton7_Click` — otvara: Platforma
  - „Van Gabaritni“ — klik → `sfButton6_Click` — otvara: VanGabaritni
  - „Kontrola i obračun transporta“ — klik → `sfButton1_Click_1` — otvara: frmObradaUlaznihFaktura
  - „NALOG ZA FAKTURISANJE“ — klik → `btnNalogFakturisanje_Click` — otvara: frmNalogZaFakturisanje

#### EAvansni [S]
- **Fajl:** `EDokumenta/EAvansni.cs` — „EAvansni“
- **Stari izgled:** Komercijala › eDokumenta › eAvansni → **EAvansni**
- **Otvara:** EOdobrenje („Knjižna odobrenja“), EZaduzenje („Knjižna zaduženja“), PoslataDokumenta („Poslata dokumenta“), PrimljenaDokumenta („Primljena dokumenta“), EFaktura („Fakture“)
- **Tabele:** `BankeFak` S, `Davek` S, `DObavnica` S, `Faktura` S, `FakturaPostav` S, `Partnerji` S, `PartnerjiDod` S, `Tecaj` S
- **Funkcionalnosti:**
  - „Ponovi osporeno“ — klik → `button1_Click`
  - „Posalji“ — klik → `btn_Posalji_Click`
  - „Dodatna dokumenta“ — klik → `btn_Dodatna_Click` — izvoz u Excel
  - „Fakture“ — klik → `faktureToolStripMenuItem_Click` — otvara: EFaktura
  - „Knjižna odobrenja“ — klik → `knjižnaOdobrenjaToolStripMenuItem_Click` — otvara: EOdobrenje
  - „Knjižna zaduženja“ — klik → `knjižnaZaduženjaToolStripMenuItem_Click` — otvara: EZaduzenje
  - „Poslata dokumenta“ — klik → `poslataDokumentaToolStripMenuItem_Click` — otvara: PoslataDokumenta
  - „Primljena dokumenta“ — klik → `primljenaDokumentaToolStripMenuItem_Click` — otvara: PrimljenaDokumenta
  - „Ucitaj PDF“ — klik → `btn_PDF_Click`
  - „Pronadji“ — klik → `btn_Pronadji_Click`

#### EFaktura [S]
- **Fajl:** `EDokumenta/EFaktura.cs` — „EFaktura“
- **Stari izgled:** Komercijala › eDokumenta › eFakture → **EFaktura**
- **Otvara:** EOdobrenje („Knjižna odobrenja“), EZaduzenje („Knjižna zaduženja“), PoslataDokumenta („Poslata dokumenta“), PrimljenaDokumenta („Primljena dokumenta“), EAvansni („Avansni račun“)
- **Tabele:** `BankeFak` S, `Davek` S, `DObavnica` S, `Faktura` S, `FakturaPostav` S, `Partnerji` S, `PartnerjiDod` S, `Tecaj` S
- **Funkcionalnosti:**
  - „Dodatna dokumenta“ — klik → `btn_Dodatna_Click` — izvoz u Excel
  - „Dodaj“ — klik → `btn_Avans_Click`
  - „Posalji“ — klik → `btn_Posalji_Click`
  - „Ponovi osporeno“ — klik → `button1_Click`
  - „Knjižna odobrenja“ — klik → `knjižnaOdobrenjaToolStripMenuItem_Click` — otvara: EOdobrenje
  - „Knjižna zaduženja“ — klik → `knjižnaZaduženjaToolStripMenuItem_Click` — otvara: EZaduzenje
  - „Avansni račun“ — klik → `avansniRačunToolStripMenuItem_Click` — otvara: EAvansni
  - „Poslata dokumenta“ — klik → `poslataDokumentaToolStripMenuItem_Click` — otvara: PoslataDokumenta
  - „Primljena dokumenta“ — klik → `primljenaDokumentaToolStripMenuItem_Click` — otvara: PrimljenaDokumenta
  - „Ucitaj PDF“ — klik → `btn_PDF_Click`
  - „Pronadji“ — klik → `btn_Pronadji_Click`

#### EOdobrenje [S]
- **Fajl:** `EDokumenta/EOdobrenje.cs` — „EOdobrenje“
- **Stari izgled:** Komercijala › eDokumenta › eOdobrenja → **EOdobrenje**
- **Otvara:** EFaktura („Fakture“), EZaduzenje („Knjižna zaduženja“), PoslataDokumenta („Poslata dokumenta“), PrimljenaDokumenta („Primljena dokumenta“), EAvansni („Avansni račun“)
- **Tabele:** `BankeFak` S, `Davek` S, `Faktura` S, `FakturaPostav` S, `Partnerji` S, `PartnerjiDod` S, `Tecaj` S
- **Funkcionalnosti:**
  - „Posalji“ — klik → `btn_Posalji_Click`
  - „Ponovi osporeno“ — klik → `button1_Click`
  - „Dodatna dokumenta“ — klik → `btn_Dodatna_Click` — izvoz u Excel
  - „Fakture“ — klik → `faktureToolStripMenuItem_Click` — otvara: EFaktura
  - „Knjižna zaduženja“ — klik → `knjižnaZaduženjaToolStripMenuItem_Click` — otvara: EZaduzenje
  - „Avansni račun“ — klik → `avansniRačunToolStripMenuItem_Click` — otvara: EAvansni
  - „Poslata dokumenta“ — klik → `poslataDokumentaToolStripMenuItem_Click` — otvara: PoslataDokumenta
  - „Primljena dokumenta“ — klik → `primljenaDokumentaToolStripMenuItem_Click` — otvara: PrimljenaDokumenta
  - „Ucitaj PDF“ — klik → `btn_PDF_Click`
  - „Pronadji“ — klik → `btn_Pronadji_Click`

#### EZaduzenje [S]
- **Fajl:** `EDokumenta/EZaduzenje.cs` — „EZaduzenje“
- **Stari izgled:** Komercijala › eDokumenta › eZaduženja → **EZaduzenje**
- **Otvara:** EFaktura („Fakture“), PoslataDokumenta („Poslata dokumenta“), PrimljenaDokumenta („Primljena dokumenta“), EOdobrenje („Knjižna odobrenja“), EAvansni („Avansni račun“)
- **Tabele:** `BankeFak` S, `Davek` S, `Faktura` S, `FakturaPostav` S, `Partnerji` S, `PartnerjiDod` S, `Tecaj` S
- **Funkcionalnosti:**
  - „Posalji“ — klik → `btn_Posalji_Click`
  - „Ponovi osporeno“ — klik → `button1_Click`
  - „Dodatna dokumenta“ — klik → `btn_Dodatna_Click` — izvoz u Excel
  - „Fakture“ — klik → `faktureToolStripMenuItem_Click` — otvara: EFaktura
  - „Knjižna odobrenja“ — klik → `knjižnaOdobrenjaToolStripMenuItem_Click` — otvara: EOdobrenje
  - „Avansni račun“ — klik → `avansniRačunToolStripMenuItem_Click` — otvara: EAvansni
  - „Poslata dokumenta“ — klik → `poslataDokumentaToolStripMenuItem_Click` — otvara: PoslataDokumenta
  - „Primljena dokumenta“ — klik → `primljenaDokumentaToolStripMenuItem_Click` — otvara: PrimljenaDokumenta
  - „Ucitaj PDF“ — klik → `btn_PDF_Click`
  - „Pronadji“ — klik → `btn_Pronadji_Click`

#### FormMessageBox [S]
- **Fajl:** `Pomocni/FormMessageBox.cs` — „Form1“
- **Stari izgled:** Uvoz › Uvoz obrada › Neraspoređeni kontejneri - Obrazac → **Uvoz** → metoda `Show` → **FormMessageBox**
- **Funkcionalnosti:**
  - „X“ — klik → `btnClose_Click`
  - „button1“ — klik → `button1_Click`

#### frmAdresaStatusVozila [S]
- **Fajl:** `Izvoz/frmAdresaStatusVozila.cs` — „Adresa status vozila“
- **Stari izgled:** Uvoz › Šifarnici › Adrese statusa vozila → **frmAdresaStatusVozila**
- **Tabele:** `AdresaStatusVozila` SIUD p
- **Procedure:** `DeleteAdresaStatusVozila`, `InsertAdresaStatusVozila`, `UpdateAdresaStatusVozila`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertAdresaStatusVozila, UpdateAdresaStatusVozila; preko: InsertAdresaStatusVozila.InsAdresaStatusVozila, InsertAdresaStatusVozila.UpdAdresaStatusVozila
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteAdresaStatusVozila; preko: InsertAdresaStatusVozila.DelAdresaStatusVozila
  - `button20` — klik → `tsDelete_Click` — proc: DeleteAdresaStatusVozila; preko: InsertAdresaStatusVozila.DelAdresaStatusVozila
  - `button21` — klik → `tsSave_Click` — proc: InsertAdresaStatusVozila, UpdateAdresaStatusVozila; preko: InsertAdresaStatusVozila.InsAdresaStatusVozila, InsertAdresaStatusVozila.UpdAdresaStatusVozila

#### frmAnalizaAktivnosti [S]
- **Fajl:** `SyncForm/frmAnalizaAktivnosti.cs` — „Analiza aktivnosti“
- **Stari izgled:** Evidencije › Evidencija aktivnosti › Pretraga aktivnosti → **frmAnalizaAktivnosti**
- **Tabele:** `Aktivnosti` S, `AktivnostiStavke` S, `Delavci` S, `VrstaAktivnosti` S
- **Funkcionalnosti:**
  - `this` — otvaranje forme → `frmAnalizaAktivnosti_Load` — čita: Aktivnosti, AktivnostiStavke, Delavci, VrstaAktivnosti

#### frmAnalizaGO [S]
- **Fajl:** `Mobile/frmAnalizaGO.cs` — „Analiza godisnjih odmora“
- **Stari izgled:** Kadrovi › Godisnji odmori › Analiza GO → **frmAnalizaGO**
- **Tabele:** `Delavci` S, `Dopust` S, `DopustStavke` S
- **Funkcionalnosti:**
  - `this` — otvaranje forme → `frmAnalizaGO_Load` — čita: Delavci, Dopust, DopustStavke

#### frmAnalizaGOSum [S]
- **Fajl:** `Mobile/frmAnalizaGOSum.cs` — „Analiza GO Sumarno“
- **Stari izgled:** Kadrovi › Godisnji odmori › Analiza GO 2 → **frmAnalizaGOSum**
- **Tabele:** `Delavci` S, `DelovnaMesta` S, `Dopust` S, `DopustStavke` S, `RazporeditveStrMesto` S
- **Funkcionalnosti:**
  - `this` — otvaranje forme → `frmAnalizaGOSum_Load` — čita: Delavci, DelovnaMesta, Dopust, DopustStavke, RazporeditveStrMesto

#### frmAnalizaPorudzbina [S]
- **Fajl:** `SyncForm/frmAnalizaPorudzbina.cs` — „Dogovorene porudžbine“
- **Stari izgled:** Komercijala › Ugovori › Dogovoreno → **frmAnalizaPorudzbina**
- **Tabele:** `Narocilo` S, `NarociloPostav` S, `Partnerji` S
- **Funkcionalnosti:**
  - `this` — otvaranje forme → `frmAnalizaPorudzbina_Load` — čita: Narocilo, NarociloPostav, Partnerji

#### frmAnalizaRadnihNaloga [N+S]
- **Fajl:** `RadniNalozi/frmAnalizaRadnihNaloga.cs` — „TERMINAL CENTRAL“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Pregled naloga“ → **frmPrijemIOtpremaVozova3** → „Pregled radnih naloga“ → **frmAnalizaRadnihNaloga**
- **Stari izgled:** Terminal › Terminalske usluge › Pregled radnih naloga Terminal → **frmAnalizaRadnihNaloga**
- **Otvara:** RN1PrijemVoza („OTVORI RN“)
- **Tabele:** `Izvoz` S, `IzvozKonacna` S, `KontejnerskiTerminali` S, `KontejnerStatus` S, `KontejnerTekuce` S, `OtpremaKontejnera` S, `OtpremaKontejneraVozStavke` S, `Partnerji` S, `Pozicija` S, `PrijemKontejneraVoz` S, `PrijemKontejneraVozStavke` S, `RNOtpremaCirade` S, `RNOtpremaPlatforme` S, `RNOtpremaPlatforme2` S, `RNOtpremaVoza` S, `RNPregledIpostavkaKontejnera` S, `RNPregledIspraznjenogKontejnera` S, `RNPrijemCirade` S, `RNPrijemPlatforme` S, `RNPrijemVoza` S, `Skladista` S, `TerminalRadniNalozi` S, `TipKontenjera` S, `UvozKonacna` S, `Voz` S, `VrstaManipulacije` S
- **Funkcionalnosti:**
  - „OTVORI RN“ — klik → `button20_Click` — otvara: RN1PrijemVoza
  - „ПО ТРЕНУТНОМ STATUSU“ — klik → `button18_Click` — čita 13 tabela
  - „IZ -PREGLED/POSTAVKA VOZ“ — klik → `button19_Click` — čita 8 tabela
  - „UV - PREGLED ISPRAŽNJENOG KONTEJNERA“ — klik → `button16_Click` — čita 8 tabela
  - „IZ - PRIJEM CIRADE“ — klik → `button17_Click` — čita 8 tabela
  - „UV - OTPREMA CIRADE“ — klik → `button15_Click` — čita 8 tabela
  - „BB - OTPREMA PLATFORMA“ — klik → `button11_Click` — čita 8 tabela
  - „UV - OTPREMA PLATFORMA“ — klik → `button8_Click` — čita 10 tabela
  - „BS - PRIJEM PLATFORMA BRODAR“ — klik → `button6_Click`
  - „IZ/U -PRIJEM PLATFORMA“ — klik → `button4_Click` — čita 10 tabela
  - „IZ - OTPREMA VOZA“ — klik → `button5_Click` — čita 11 tabela
  - „U - PRETOVAR PRIJEM VOZA“ — klik → `button3_Click` — čita 11 tabela
  - „1: UV - PRIJEM VOZA“ — klik → `button2_Click` — čita 11 tabela
  - „SVI RN“ — klik → `button1_Click` — čita: TerminalRadniNalozi
  - „gridGroupingControl2“ — klik na ćeliju → `gridGroupingControl2_TableControlCellClick`

#### frmAnalizaUvoza [S]
- **Fajl:** `Uvoz/frmAnalizaUvoza.cs` — „Pregled operacija nad kontejnerima“
- **Stari izgled:** Uvoz › Uvoz obrada › Pregled operacija nad kontejnerima → **frmAnalizaUvoza**
- **Tabele:** `KontejnerStatus` S, `organizacioneJedinice` S, `Partnerji` S, `RadniNalogInterni` S, `Scenario` S, `TipKontenjera` S, `UvozKonacna` S, `UvozKonacnaVrstaManipulacije` S, `UvozKonacnaZaglavlje` S, `Voz` S, `VrstaManipulacije` S
- **Procedure:** `UpdateUvozKonacnaManZaFakturisanje`, `UpdateUvozKonacnaObjedinjenja`
- **Funkcionalnosti:**
  - „Proglasi za fakturisanje“ — klik → `button25_Click` — proc: UpdateUvozKonacnaManZaFakturisanje; preko: InsertUvozKonacna.UpdUvozKonacnaManZaFakturisanje
  - „Proglasi objedinjena“ — klik → `button24_Click` — proc: UpdateUvozKonacnaObjedinjenja; preko: InsertUvozKonacna.UpdUvozKonacnaManObjedinjena
  - „Refresh usluge“ — klik → `button1_Click`
  - „Refresh radni nalozi“ — klik → `button23_Click`

#### frmAutomobilEdit [N+S]
- **Fajl:** `Drumski/frmAutomobilEdit.cs` — „Automobil detalji“
- **Novi izgled:** Drumski transport → **Drumski1** → „Cerada“ → **Cerade** → „UNOS PREVOZNIK/KAMIONER“ → **frmAutomobiliDrumski** → „Novi“ → **frmAutomobilEdit**
- **Stari izgled:** Drumski › Šifarnici › Vozni park → **frmAutomobiliDrumski** → „Novi“ → **frmAutomobilEdit**
- **Otvara:** frmPartnerji („Novi prevoznik/kamioner“)
- **Tabele:** `Automobili` IUD p, `Delavci` S, `Korisnici` S, `Partnerji` S, `VrstaVozila` S
- **Procedure:** `DeleteAutomobili`, `InsertAutomobili`, `UpdateAutomobili`
- **Combobox → tabela:** `cboTipVozila` ← `VrstaVozila`; `cboPrevoznik` ← `Partnerji`
- **Funkcionalnosti:**
  - „Novi prevoznik/kamioner“ — klik → `btnDodavanjePartnera_Click` — otvara: frmPartnerji
  - `button1` — klik → `button1_Click` — proc: DeleteAutomobili; preko: InsertAutomobili.DeleteAutomobili
  - `button2` — klik → `button2_Click` — proc: InsertAutomobili, UpdateAutomobili; preko: InsertAutomobili.InsAutomobili, InsertAutomobili.UpdAutobili

#### frmAutomobili [S]
- **Fajl:** `Dokumenta/frmAutomobili.cs` — „Automobili“
- **Stari izgled:** Održavanje › Automobili › Vozni park → **frmAutomobili**
- **Tabele:** `Automobili` SIUD p, `Delavci` S, `Mesta` S, `VrstaVozila` S
- **Procedure:** `DeleteAutomobili`, `InsertAutomobili`, `UpdateAutomobili`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertAutomobili, UpdateAutomobili; preko: InsertAutomobili.InsAutomobili, InsertAutomobili.UpdAutobili
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteAutomobili; preko: InsertAutomobili.DeleteAutomobili
  - „Skenirana dokumentacija“ — klik → `toolStripButton1_Click` — otvara: frmAutomobiliDokumenta
  - „Evidencija Servisa“ — klik → `toolStripButton2_Click` — otvara: frmAutomobiliServis
  - „Evidencija registracija“ — klik → `toolStripButton3_Click` — otvara: frmAutomobiliRegistracija
  - „Evidencija troškova iz UF“ — klik → `toolStripButton4_Click` — otvara: frmEvidencijaTroskova
  - „Evidencija kvarova“ — klik → `toolStripButton5_Click` — otvara: frmEvidencijaKvarova
  - `button1` — klik → `tsDelete_Click` — proc: DeleteAutomobili; preko: InsertAutomobili.DeleteAutomobili
  - `button2` — klik → `tsSave_Click` — proc: InsertAutomobili, UpdateAutomobili; preko: InsertAutomobili.InsAutomobili, InsertAutomobili.UpdAutobili

#### frmAutomobiliDrumski [N+S]
- **Fajl:** `Drumski/frmAutomobiliDrumski.cs` — „Automobili“
- **Novi izgled:** Drumski transport → **Drumski1** → „Cerada“ → **Cerade** → „UNOS PREVOZNIK/KAMIONER“ → **frmAutomobiliDrumski**
- **Stari izgled:** Drumski › Šifarnici › Vozni park → **frmAutomobiliDrumski**
- **Otvara:** frmAutomobilEdit („Novi“), frmPodesavanjeRaspolozivosti („Raspoloživost“)
- **Tabele:** `Automobili` SIUD p, `AutomobiliTehnickiProblem` S, `Delavci` S, `Korisnici` S, `Partnerji` S, `RadniNalogDrumski` S, `SistemskePostavke` S, `VrstaVozila` S
- **Procedure:** `DeleteAutomobili`, `InsertAutomobili`, `UpdateAutomobili`
- **Combobox → tabela:** `cboTipVozila` ← `VrstaVozila`; `cboPrevoznik` ← `Partnerji`; `cboPrevoznikFilter` ← `Partnerji`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click` — otvara: frmAutomobilEdit
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertAutomobili, UpdateAutomobili; preko: InsertAutomobili.InsAutomobili, InsertAutomobili.UpdAutobili
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteAutomobili; preko: InsertAutomobili.DeleteAutomobili
  - „Skenirana dokumentacija“ — klik → `toolStripButton1_Click` — otvara: frmAutomobiliDokumenta
  - „Evidencija Servisa“ — klik → `toolStripButton2_Click` — otvara: frmAutomobiliServis
  - „Evidencija registracija“ — klik → `toolStripButton3_Click` — otvara: frmAutomobiliRegistracija
  - „Evidencija troškova iz UF“ — klik → `toolStripButton4_Click` — otvara: frmEvidencijaTroskova
  - „Evidencija kvarova“ — klik → `toolStripButton5_Click` — otvara: frmEvidencijaKvarova
  - „Raspoloživost“ — klik → `button4_Click`
  - `button2` — klik → `tsSave_Click` — proc: InsertAutomobili, UpdateAutomobili; preko: InsertAutomobili.InsAutomobili, InsertAutomobili.UpdAutobili
  - `button1` — klik → `tsDelete_Click` — proc: DeleteAutomobili; preko: InsertAutomobili.DeleteAutomobili
  - `button3` — klik → `tsNew_Click` — otvara: frmAutomobilEdit
  - „Detalji“ — klik → `otvoriToolStripMenuItem_Click` — otvara: frmAutomobilEdit
  - „Sutra“ — čekiranje → `chkDatumD_CheckedChanged`
  - „Danas“ — čekiranje → `chkDatumD_CheckedChanged`
  - „Filtriraj“ — klik → `btnFiltriraj_Click`

#### frmAutomobiliKvarovi [S]
- **Fajl:** `Dokumenta/frmAutomobiliKvarovi.cs` — „Automobili kvarovi“
- **Stari izgled:** Održavanje › Automobili › Prijavljeni kvarovi → **frmAutomobiliKvarovi**
- **Tabele:** `Automobili` S, `Delavci` S, `EvidencijaKvarovaAuto` S, `GrupaKvarovaAuto` S, `KvaroviAuto` S
- **Funkcionalnosti:**
  - `this` — otvaranje forme → `frmAutomobiliKvarovi_Load` — čita: Automobili, Delavci, EvidencijaKvarovaAuto, GrupaKvarovaAuto, KvaroviAuto

#### frmAutomobiliPregledPrijava [S]
- **Fajl:** `Dokumenta/frmAutomobiliPregledPrijava.cs` — „Automobili pregled Zaduzenja/Razduzduzenja“
- **Stari izgled:** Održavanje › Automobili › Zaduzivanje / Razduzivanje → **frmAutomobiliPregledPrijava**
- **Tabele:** `Automobili` S, `CistocaIznutra` S, `CistocaIznutraRazduzivanje` S, `CistocaSpolja` S, `CistocaSpoljaRazduzivanje` S, `Delavci` S, `Stanice` S, `UljeAuto` S, `UljeAutoRazduzivanje` S, `ZaposleniPrijavaAuto` S
- **Procedure:** `sp_DeleteZaposleniPrijavaAuto`, `sp_InsertZaposleniPrijavaAuto`, `sp_UpdateZaposleniPrijavaAuto`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: sp_InsertZaposleniPrijavaAuto, sp_UpdateZaposleniPrijavaAuto; preko: InsertAutomobiliPregledPrijava.InsAutomobiliPregledPrijava, InsertAutomobiliPregledPrijava.UpdAutomobiliPregledPrijava
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: sp_DeleteZaposleniPrijavaAuto; preko: InsertAutomobiliPregledPrijava.DeleteAutomobiliPregledPrijava
  - „Otvori“ — klik → `btn_OtvoriSliku_Click`
  - „<<“ — klik → `btn_nazad_Click`
  - „>>“ — klik → `btn_Napred_Click`
  - „Otvori folder“ — klik → `button1_Click`

#### frmAutoprevozniList2 [N+S]
- **Fajl:** `TrackModal/Dokumeta/frmAutoprevozniList2.cs` — „AutoPrevozni list“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Voz“ → **frmPrijemIOtpremaVozova4** → „Gate out voz“ → **frmPregledOtpreme** → „Otvori voz“ → **frmOtpremaKontejnera** → „Nalog Za Prevoz“ → **frmNalogZaPrevoz** → „Otvori putni nalog“ → **frmPutniNalog** → „Radni nalog“ → **frmRadniNalogTransport** → „Autoprevozni list“ → **frmAutoprevozniList2**
- **Stari izgled:** Terminal › Transport › Pregled autoprevoznički list → **frmPregledAutoPrevozniList** → „Otvori autoprevoznilist“ → **frmAutoprevozniList2**
- **Tabele:** `Autoprevoznilist` SIU p, `Komitenti` S, `NajavaPrevoza` S, `PutniNalog` S, `RadniNalogTransport` S, `Vozila` S, `Zaposleni` S
- **Procedure:** `DeleteAutoprevozniListGeneralni`, `InsertAutoPrevozniList`, `InsertAutoPrevozniListGeneralni`, `InsertAutoPrevozniListKameni`, `UpdateAutoprevoznilist`, `UpdateAutoprevoznilistGeneralni`, `UpdateAutoprevoznilistKameni`
- **Combobox → tabela:** `cboPlatilac` ← `Komitenti`; `cboPrimalac` ← `Komitenti`; `cboVozilo` ← `Vozila`; `cboPrikljucnoVozilo` ← `Vozila`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertAutoPrevozniList, UpdateAutoprevoznilist; preko: InsertAutoprevozniList.InsAutoprevozniList, InsertAutoprevozniList.UpdAutoprevozniList
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „Štampa Autoprevozni list“ — klik → `toolStripButton1_Click` — izveštaj: rptAutoprevozniList.rdlc
  - „Izbriši“ — klik → `button1_Click` — proc: DeleteAutoprevozniListGeneralni; preko: InsertAutoprevozniList.DelAutoprevozniListGeneralni
  - „Promeni“ — klik → `button2_Click` — proc: UpdateAutoprevoznilistGeneralni; preko: InsertAutoprevozniList.UpdAutoprevozniListGeneralni
  - „Unesi“ — klik → `button3_Click` — proc: InsertAutoPrevozniListGeneralni; preko: InsertAutoprevozniList.InsAutoprevozniListGeneralni
  - „Izbriši“ — klik → `button4_Click` — proc: DeleteAutoprevozniListGeneralni; preko: InsertAutoprevozniList.DelAutoprevozniListGeneralni
  - „Promeni“ — klik → `button5_Click` — proc: UpdateAutoprevoznilistKameni; preko: InsertAutoprevozniList.UpdAutoprevozniListKameni
  - „Unesi“ — klik → `button6_Click` — proc: InsertAutoPrevozniListKameni; preko: InsertAutoprevozniList.InsAutoprevozniListKameni
  - `this` — otvaranje forme → `frmAutoprevozniList2_Load` — čita: Komitenti, Vozila

#### frmAutoprevozniList2 [N+S]
- **Fajl:** `Dokumenta/frmAutoprevozniList2.cs` — „AutoPrevozni list“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Formiraj otpremu kamionom“ → **frmOtpremaKontejneraKamionomIzKontejnera** → „Otvori“ → **frmOtpremaKontejnera** → „Nalog Za Prevoz“ → **frmNalogZaPrevoz** → „Otvori putni nalog“ → **frmPutniNalog** → „Radni nalog“ → **frmRadniNalogTransport** → „Autoprevozni list“ → **frmAutoprevozniList2**
- **Stari izgled:** Terminal › Transport › Autoprevoznički list → **frmAutoprevozniList2**
- **Tabele:** `Autoprevoznilist` SIU p, `Komitenti` S, `NajavaPrevoza` S, `PutniNalog` S, `RadniNalogTransport` S, `Vozila` S, `Zaposleni` S
- **Procedure:** `DeleteAutoprevozniListGeneralni`, `InsertAutoPrevozniList`, `InsertAutoPrevozniListGeneralni`, `InsertAutoPrevozniListKameni`, `UpdateAutoprevoznilist`, `UpdateAutoprevoznilistGeneralni`, `UpdateAutoprevoznilistKameni`
- **Combobox → tabela:** `cboPlatilac` ← `Komitenti`; `cboPrimalac` ← `Komitenti`; `cboVozilo` ← `Vozila`; `cboPrikljucnoVozilo` ← `Vozila`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertAutoPrevozniList, UpdateAutoprevoznilist; preko: InsertAutoprevozniList.InsAutoprevozniList, InsertAutoprevozniList.UpdAutoprevozniList
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „Štampa Autoprevozni list“ — klik → `toolStripButton1_Click` — izveštaj: rptAutoprevozniList.rdlc
  - „Izbriši“ — klik → `button1_Click` — proc: DeleteAutoprevozniListGeneralni; preko: InsertAutoprevozniList.DelAutoprevozniListGeneralni
  - „Promeni“ — klik → `button2_Click` — proc: UpdateAutoprevoznilistGeneralni; preko: InsertAutoprevozniList.UpdAutoprevozniListGeneralni
  - „Unesi“ — klik → `button3_Click` — proc: InsertAutoPrevozniListGeneralni; preko: InsertAutoprevozniList.InsAutoprevozniListGeneralni
  - „Izbriši“ — klik → `button4_Click` — proc: DeleteAutoprevozniListGeneralni; preko: InsertAutoprevozniList.DelAutoprevozniListGeneralni
  - „Promeni“ — klik → `button5_Click` — proc: UpdateAutoprevoznilistKameni; preko: InsertAutoprevozniList.UpdAutoprevozniListKameni
  - „Unesi“ — klik → `button6_Click` — proc: InsertAutoPrevozniListKameni; preko: InsertAutoprevozniList.InsAutoprevozniListKameni
  - `this` — otvaranje forme → `frmAutoprevozniList2_Load` — čita: Komitenti, Vozila

#### frmBolovanja [S]
- **Fajl:** `Dokumenta/frmBolovanja.cs` — „Bolovanja“
- **Stari izgled:** Kadrovi › Osnovni podaci › Bolovanja → **frmBolovanja**
- **Tabele:** `Bolovanje` SIUD p, `Delavci` S
- **Procedure:** `DeleteBolovanje`, `InsertBolovanje`, `UpdateBolovanje`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertBolovanje, UpdateBolovanje; preko: insertBolovanja.InsBolovanja, insertBolovanja.UpdBolovanje
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteBolovanje; preko: insertBolovanja.DeleteBolovanje
  - „Prikaži“ — klik → `button1_Click`
  - „Prikaži sve“ — klik → `button2_Click` — čita: Bolovanje, Delavci
  - `this` — otvaranje forme → `frmBolovanja_Load` — čita: Delavci

#### frmBrojFaktureModal [N+S]
- **Fajl:** `Drumski/frmBrojFaktureModal.cs` — „Unosna forma“
- **Novi izgled:** Drumski transport → **Drumski1** → „NALOG ZA FAKTURISANJE“ → **frmNalogZaFakturisanje** → „Objedinjena dokumenta po izlaznoj fakturi“ → **frmBrojFaktureModal**
- **Stari izgled:** Drumski › Finansije › Lista naloga → **frmPregledFaktura** → „Objedinjena dokumentacija po broju izlazne fakture“ → **frmBrojFaktureModal**
- **Funkcionalnosti:**
  - „OK“ — klik → `button5_Click`

#### frmCarinskiPostupak [N]
- **Fajl:** `Izvoz/frmCarinskiPostupak.cs` — „Carinski postupak“
- **Novi izgled:** Logistika izvoza → **LogistikaIzvoza1** → „Formiranje naloga za otpremu“ → **LogistikaIzvoza2** → „Kreiraj novi“ → **frmProdajniNalogIzvozTabela** → `gridGroupingControl1` (TableControlCellClick) → **frmGrupniUnosPoljaIzvoz** → „Carinski postupak“ → **frmCarinskiPostupak**
- **Otvara:** frmKontaktOsobe (`btnPolazna` (Click))
- **Tabele:** `Carinarnice` S, `Izvoz` S, `Partnerji` S, `VrstaCarinskogPostupka` S
- **Procedure:** `UpdateIzvozPorudzbenicaCarina`
- **Combobox → tabela:** `cboPolaznaCarinarnica` ← `Carinarnice`; `cboSpediterPolazna` ← `Partnerji`; `cboOdredisnaCarinarnica` ← `Carinarnice`; `cboSpediterOdredisna` ← `Partnerji`; `cboCarinskiPUnutrasniTransport` ← `VrstaCarinskogPostupka`
- **Funkcionalnosti:**
  - `button21` — klik → `button21_Click` — proc: UpdateIzvozPorudzbenicaCarina; preko: InsertIzvoz.UpdateIzvozPorudzbenicaCarina

#### frmCenaPoRadniku [S]
- **Fajl:** `Sifarnici/frmCenaPoRadniku.cs` — „Cene po radniku“
- **Stari izgled:** Evidencije › Vrste aktivnosti › Cene → **frmCenaPoRadniku**
- **Tabele:** `Delavci` S, `EvidencijaCenaRadnik` SIUD p
- **Procedure:** `DeleteEvidencijaCenaRadnik`, `InsertEvidencijaCenaRadnik`, `UpdateEvidencijaCenaRadnik`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertEvidencijaCenaRadnik, UpdateEvidencijaCenaRadnik; preko: InsertEvidencijaSatiPoRadniku.InsCenaPoRadniku, InsertEvidencijaSatiPoRadniku.UpdCenaPoRadniku
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteEvidencijaCenaRadnik; preko: InsertEvidencijaSatiPoRadniku.DeleteEvidencijaCenaRadnik

#### frmCene [S]
- **Fajl:** `TrackModal/Sifarnici/frmCene.cs` — „Cene“
- **Stari izgled:** Terminal › Šifarnici › Cene → **frmCene**
- **Otvara:** frmKopiranjeCenovnikaPoTipu („Kopiraj cenovnik“)
- **Tabele:** `Aktivnosti` D p, `Cene` SIUD p, `organizacioneJedinice` S, `Partnerji` S, `TipCenovnika` S, `VrstaManipulacije` S, `VrstePostupakaUvoz` S
- **Procedure:** `DeleteAktivnosti`, `DeleteCene`, `InsertCene`, `KopirajCene`, `UpdateCene`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertCene, UpdateCene; preko: InsertCene.InsCene, InsertCene.UpdCene
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteAktivnosti, DeleteCene; preko: InsertCene.DeleteCene
  - „toolStripButton1“ — klik → `tsPrvi_Click` — čita: Cene
  - „toolStripButton1“ — klik → `tsNazad_Click` — čita: Cene
  - „toolStripButton1“ — klik → `tsNapred_Click` — čita: Cene
  - „toolStripButton1“ — klik → `tsPoslednja_Click` — čita: Cene
  - „Kopiranje cenovnika forma“ — klik → `toolStripButton2_Click`
  - „Kopiraj po komitentu“ — klik → `toolStripButton1_Click` — proc: KopirajCene; preko: InsertCene.KopirajCene
  - „?“ — klik → `button1_Click`
  - „?“ — klik → `button3_Click`
  - „?“ — klik → `button4_Click`
  - „*“ — klik → `button5_Click`
  - „Kopiraj po komitentu“ — klik → `toolStripButton1_Click` — proc: KopirajCene; preko: InsertCene.KopirajCene
  - „Kopiraj cenovnik“ — klik → `toolStripButton2_Click`
  - `button20` — klik → `tsDelete_Click` — proc: DeleteAktivnosti, DeleteCene; preko: InsertCene.DeleteCene
  - `button21` — klik → `tsSave_Click` — proc: InsertCene, UpdateCene; preko: InsertCene.InsCene, InsertCene.UpdCene
  - `this` — otvaranje forme → `frmCene_Load` — čita: OrganizacioneJedinice, Partnerji, TipCenovnika, VrstaManipulacije, VrstePostupakaUvoz

#### frmCentralnaTablaCpajal [S]
- **Fajl:** `Dokumenta/frmCentralnaTablaCpajal.cs` — „Centralna tabla “
- **Stari izgled:** Operatika › Tabla › Centralna tabla → **frmCentralnaTablaCpajal**
- **Otvara:** frmRadniNalog („Otvori RN“), frmNajava („Otvori Najavu“)
- **Tabele:** `Delavci` S, `Najava` S, `Partnerji` S, `RadniNalog` S, `RadniNalogLokNaTrasi` S, `RadniNalogTrase` S, `RadniNalogTraseLokZap` S, `Stanice` S, `Trase` S
- **Funkcionalnosti:**
  - „Otvori Najavu“ — klik → `otvoriNajavuToolStripMenuItem_Click` — otvara: frmNajava
  - „Otvori RN“ — klik → `otvoriRNToolStripMenuItem_Click` — otvara: frmRadniNalog

#### frmCentralnaTablaRN [S]
- **Fajl:** `Dokumenta/frmCentralnaTablaRN.cs` — „Pregled RN“
- **Stari izgled:** Operatika › Tabla › RN tabla → **frmCentralnaTablaRN**

#### frmCerada [N]
- **Fajl:** `MainLeget/PrijemIOtpremaKamiona/frmCerada.cs` — „frmCerada“
- **Novi izgled:** Prijem i otprema kamiona → **frmPrijemIOtpremaKamiona1** → „CERADA“ → **frmPrijemIOtpremaCerada** → „Prijem“ → **frmCerada**
- **Tabele:** `organizacioneJedinice` S, `PrijemKontejneraVozStavke` S, `RadniNalogInterni` S, `RadniNalogInterniPotvrda` S
- **Procedure:** `PromeniStatusPrijemPotvrdioKapiju`
- **Funkcionalnosti:**
  - „Refresh“ — klik → `button8_Click`
  - „Potvrdi kapiju“ — klik → `button27_Click` — proc: PromeniStatusPrijemPotvrdioKapiju; preko: InsertRadniNalogInterni.PromeniStatusKapija

#### frmCeradaOtprema [N]
- **Fajl:** `MainLeget/PrijemIOtpremaKamiona/frmCeradaOtprema.cs` — „frmCeradaOtprema“
- **Novi izgled:** Prijem i otprema kamiona → **frmPrijemIOtpremaKamiona1** → „CERADA“ → **frmPrijemIOtpremaCerada** → „Otprema“ → **frmCeradaOtprema**
- **Tabele:** `organizacioneJedinice` S, `OtpremaKontejneraVozStavke` S, `RadniNalogInterni` S, `RadniNalogInterniPotvrda` S
- **Funkcionalnosti:**
  - „Refresh“ — klik → `button8_Click`

#### frmCIR [N+S]
- **Fajl:** `Dokumenta/frmCIR.cs` — „CIR“
- **Novi izgled:** Prijem i otprema kamiona → **frmPrijemIOtpremaKamiona1** → „PREGLEDAČ“ → **Pregledac** → „Izradi CIR“ → **frmCIR**
- **Stari izgled:** Terminal › Radni nalozi › Otprema kontejnera Kamion - N → **frmOtpremaKontejneraUvozKamion** → „CIR“ → **frmCIR**
- **Otvara:** frmDodelaSkladista („ODREDI PRIVREMENU POZICIJU“)
- **Tabele:** `CIR` S, `CIRGreske` S, `Delovi` S, `Greske` S, `NaruceneManipulacije` S, `OtpremaKontejnera` S, `OtpremaKontejneraVozStavke` S, `OtpremaKontejneraVozz` S, `PrijemKontejneraVoz` S, `PrijemKontejneraVozStavke` S, `TipKontenjera` S, `uvKvalitetKontejnera` S, `VrstaManipulacije` S
- **Combobox → tabela:** `cboOstecenja` ← `Greske`; `cboTipKontejnera` ← `TipKontenjera`; `cboDeo` ← `Delovi`; `cboKvalitet` ← `uvKvalitetKontejnera`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click`
  - „toolStripButton1“ — klik → `tsDelete_Click_1`
  - „Štampa CIR“ — klik → `toolStripLabel1_Click` — izveštaj: rptCIR.rdlc, rptCIRTA.rdlc
  - „Pozovi postojeći“ — klik → `toolStripLabel2_Click`
  - „AŽURIRAJ TEKUCI KONTEJNER“ — klik → `toolStripButton1_Click`
  - „ODREDI PRIVREMENU POZICIJU“ — klik → `toolStripButton2_Click` — otvara: frmDodelaSkladista
  - „Čelik“ — čekiranje → `chkMaterijalCelik_CheckedChanged`
  - „Alumini“ — čekiranje → `chkAlumini_CheckedChanged`
  - „Otprema“ — čekiranje → `chkincoming_CheckedChanged`
  - „Pun“ — čekiranje → `chkPun_CheckedChanged`
  - „Oštećen“ — čekiranje → `chkDemaged_CheckedChanged`
  - „Ispravan“ — čekiranje → `chkIspravan_CheckedChanged`
  - „Primio:“ — klik → `label5_Click`
  - „Prijem“ — čekiranje → `chkPrijem_CheckedChanged`
  - „Ubaci“ — klik → `button1_Click` — čita: CIRGreske, Delovi, Greske
  - „Izbriši“ — klik → `button3_Click` — čita: CIRGreske, Delovi, Greske
  - „Prazan“ — čekiranje → `chkPrazan_CheckedChanged`
  - „Povuci Manipulacije“ — klik → `button2_Click`
  - „Odredi privremenu poziciju“ — klik → `toolStripButton2_Click` — otvara: frmDodelaSkladista
  - „Ažuriraj tekući kontejner“ — klik → `toolStripButton1_Click`
  - „Pozovi postojeći“ — klik → `toolStripLabel2_Click`
  - „Štampaj“ — klik → `toolStripLabel1_Click` — izveštaj: rptCIR.rdlc, rptCIRTA.rdlc
  - „Štampa “ — klik → `toolStripLabel1_Click` — izveštaj: rptCIR.rdlc, rptCIRTA.rdlc
  - „Pozovi postojeći“ — klik → `button8_Click`
  - „Ažuriraj tekući kontejner“ — klik → `toolStripButton1_Click`
  - „ODREDI PRIVREMENU POZICIJU“ — klik → `toolStripButton2_Click` — otvara: frmDodelaSkladista
  - `this` — otvaranje forme → `frmCIR_Load` — čita: Delovi, Greske, TipKontenjera, uvKvalitetKontejnera

#### frmCIRPregled [N+S]
- **Fajl:** `TrackModal/Dokumeta/frmCIRPregled.cs` — „PREGLED POSTOJECIH CIR“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Depo cnt“ → **frmPrijemIOtpremaVozova2** → „CIR“ → **frmCIRPregled**
- **Stari izgled:** Terminal › Prevoz › CIR → **frmCIRPregled**
- **Otvara:** frmCIR („Otvori“)
- **Tabele:** `CIR` S
- **Funkcionalnosti:**
  - „Pozovi postojeći“ — klik → `toolStripLabel1_Click`
  - „Refresh“ — klik → `toolStripLabel2_Click`
  - „Refresh“ — klik → `toolStripLabel2_Click`
  - „Otvori“ — klik → `toolStripLabel1_Click`
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`

#### frmCIRPregledac [S]
- **Fajl:** `RadniNalozi/frmCIRPregledac.cs` — „CIR pregledi“
- **Stari izgled:** Terminal › Prevoz › P - CIR → **frmCIRPregledac**
- **Tabele:** `RNPrijemPlatforme` S, `RNPrijemPlatforme2` S, `RNPrijemVoza` S, `Skladista` S, `TipKontenjera` S, `Voz` S
- **Funkcionalnosti:**
  - „2: URAĐENI“ — klik → `button1_Click` — čita: RNPrijemPlatforme, RNPrijemVoza, Skladista, TipKontenjera, Voz
  - „1: AKTIVNI“ — klik → `button2_Click` — čita: RNPrijemPlatforme, RNPrijemPlatforme2, RNPrijemVoza, Skladista, TipKontenjera, Voz
  - „Uradjeni“ — klik → `button1_Click` — čita: RNPrijemPlatforme, RNPrijemVoza, Skladista, TipKontenjera, Voz
  - „Aktivni“ — klik → `button2_Click` — čita: RNPrijemPlatforme, RNPrijemPlatforme2, RNPrijemVoza, Skladista, TipKontenjera, Voz

#### frmDefinisiPoziciju [N+S]
- **Fajl:** `Tehnologija/frmDefinisiPoziciju.cs` — „DEFINISANJE POZICIJE“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Depo cnt“ → **frmPrijemIOtpremaVozova2** → „Pregled skladišta“ → **TerminalMapFRM** → „Definiši/Predefiniši“ → **frmDefinisiPoziciju**
- **Stari izgled:** Terminal › Terminalske usluge › PREDEFINISANJE POZICIJA → **frmDefinisiPoziciju**
- **Tabele:** `IzvozKonacna` S, `Partnerji` S, `PredefinisanePozicije` SI p, `Skladista` S, `TipKontenjera` S, `uvKvalitetKontejnera` S
- **Procedure:** `InsertPredefinisanePozicije`
- **Combobox → tabela:** `txtTipKont` ← `TipKontenjera`; `txtTipKont2` ← `TipKontenjera`; `cboBrodar` ← `Partnerji`; `cboBrodar2` ← `Partnerji`; `cboIzvoznik` ← `Partnerji`; `cboNalogodavac1` ← `Partnerji`; `cboKvalitet` ← `uvKvalitetKontejnera`; `cboKvalitet2` ← `uvKvalitetKontejnera`; `cboNaSkladiste` ← `Skladista`; `cboNaSkladiste2` ← `Skladista`; `cboNaSkladiste3` ← `Skladista`; `cboNaSkladiste4` ← `Skladista`; `cboNaSkladiste5` ← `Skladista`; `cboNaSkladiste6` ← `Skladista`; `cboBooking` ← `IzvozKonacna`
- **Funkcionalnosti:**
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - „gridGroupingControl2“ — klik na ćeliju → `gridGroupingControl2_TableControlCellClick`
  - „SNIMI PRAZAN“ — klik → `toolStripButton1_Click` — proc: InsertPredefinisanePozicije; preko: InsertDefinisiPoziciju.InsDefinisiPoziciju
  - „SNIMI PUN“ — klik → `toolStripButton3_Click` — proc: InsertPredefinisanePozicije; preko: InsertDefinisiPoziciju.InsDefinisiPoziciju
  - „REFRESH TEKUCE“ — klik → `toolStripButton5_Click`
  - „Refresh“ — klik → `toolStripButton5_Click`
  - „Snimi pun“ — klik → `toolStripButton3_Click` — proc: InsertPredefinisanePozicije; preko: InsertDefinisiPoziciju.InsDefinisiPoziciju
  - „Snimi Prazan“ — klik → `toolStripButton1_Click` — proc: InsertPredefinisanePozicije; preko: InsertDefinisiPoziciju.InsDefinisiPoziciju
  - „Vrati sve“ — klik → `button1_Click`
  - `this` — otvaranje forme → `frmDefinisiPoziciju_Load` — čita: IzvozKonacna, Partnerji, Skladista, TipKontenjera, uvKvalitetKontejnera

#### frmDelavci [S]
- **Fajl:** `Sifarnici/frmDelavci.cs` — „Zaposleni“
- **Stari izgled:** Terminal › Šifarnici › Zaposleni → **frmDelavci**
- **Tabele:** `Delavci` S, `DelovnaMesta` S
- **Procedure:** `DeleteDelavciMTA`, `InsertDelavciMTA`, `InsertDelavciStariMTA`, `UpdateDelavciMTA`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertDelavciMTA, UpdateDelavciMTA; preko: InsertDelavciMTA.InsDelavciMTA, InsertDelavciMTA.UpdDelavciMTA
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteDelavciMTA; preko: InsertDelavciMTA.DelDelavciMTA
  - „SACUVAJ STAROG“ — klik → `toolStripButton1_Click` — proc: InsertDelavciStariMTA; preko: InsertDelavciMTA.InsDelavciStariMTA
  - „Sacuvaj starog“ — klik → `toolStripButton1_Click` — proc: InsertDelavciStariMTA; preko: InsertDelavciMTA.InsDelavciStariMTA
  - `button20` — klik → `tsDelete_Click` — proc: DeleteDelavciMTA; preko: InsertDelavciMTA.DelDelavciMTA
  - `button21` — klik → `tsSave_Click` — proc: InsertDelavciMTA, UpdateDelavciMTA; preko: InsertDelavciMTA.InsDelavciMTA, InsertDelavciMTA.UpdDelavciMTA
  - `this` — otvaranje forme → `frmDelavci_Load` — čita: DelovnaMesta

#### frmDelovi [S]
- **Fajl:** `TrackModal/Sifarnici/frmDelovi.cs` — „ŠIFARNICI - DELOVI“
- **Stari izgled:** Terminal › Šifarnici › Delovi kontejnera → **frmDelovi**
- **Tabele:** `Delovi` SIUD p
- **Procedure:** `DeleteDelovi`, `InsertDelovi`, `UpdateDelovi`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertDelovi, UpdateDelovi; preko: InsertDelovi.InsDelovi, InsertDelovi.UpdDelovi
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteDelovi; preko: InsertDelovi.DeleteStatusRobe
  - `button20` — klik → `tsDelete_Click` — proc: DeleteDelovi; preko: InsertDelovi.DeleteStatusRobe
  - `button21` — klik → `tsSave_Click` — proc: InsertDelovi, UpdateDelovi; preko: InsertDelovi.InsDelovi, InsertDelovi.UpdDelovi

#### frmDelovnaMesta [S]
- **Fajl:** `Sifarnici/frmDelovnaMesta.cs` — „Radna mesta“
- **Stari izgled:** Kadrovi › Osnovni podaci › Radna mesta → **frmDelovnaMesta**
- **Tabele:** `DelovnaMesta` SIUD p
- **Procedure:** `DeleteDelovnaMesta`, `InsertDelovnaMesta`, `UpdateDelovnaMesta`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertDelovnaMesta, UpdateDelovnaMesta; preko: InsertDelovnaMesta.InsDelovnaMesta, InsertDelovnaMesta.UpdDelovnaMesta
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteDelovnaMesta; preko: InsertDelovnaMesta.DelDelovnaMesta

#### frmDirigacijaKontejneraZa [S]
- **Fajl:** `Uvoz/frmDirigacijaKontejneraZa.cs` — „Dirigacija kontejnera za“
- **Stari izgled:** Terminal › Šifarnici › Dirigacija kontejnera za → **frmDirigacijaKontejneraZa**
- **Tabele:** `DirigacijaKontejneraZa` SIUD p
- **Procedure:** `DeleteDirigacijaKontejneraZa`, `InsertDirigacijaKontejneraZa`, `UpdateDirigacijaKontejneraZa`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertDirigacijaKontejneraZa, UpdateDirigacijaKontejneraZa; preko: InertDirigacijaKontejneraZa.InsDirigacijaKontejneraZa, InertDirigacijaKontejneraZa.UpdDirigacijaKontejneraZa
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteDirigacijaKontejneraZa; preko: InertDirigacijaKontejneraZa.DelDirigacijaKontejneraZa
  - `button20` — klik → `tsDelete_Click` — proc: DeleteDirigacijaKontejneraZa; preko: InertDirigacijaKontejneraZa.DelDirigacijaKontejneraZa
  - `button21` — klik → `tsSave_Click` — proc: InsertDirigacijaKontejneraZa, UpdateDirigacijaKontejneraZa; preko: InertDirigacijaKontejneraZa.InsDirigacijaKontejneraZa, InertDirigacijaKontejneraZa.UpdDirigacijaKontejneraZa

#### frmDodatneUsluge [N+S]
- **Fajl:** `Uvoz/frmDodatneUsluge.cs` — „UNOS DODATNIH USLUGA“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Dodatne usluge“ → **frmDodatneUsluge**
- **Stari izgled:** Terminal › Terminalske usluge › DODATNE USLUGE → **frmDodatneUsluge**
- **Otvara:** frmIzvozDokumenta („Dodeli slike“), UvozDokumenta („Dodeli slike“)
- **Tabele:** `IzvozKonacna` S, `KontejnerStatus` S, `KontejnerTekuce` S, `RadniNalogInterni` S, `Skladista` S, `TipKontenjera` S, `UvozKonacna` S, `Voz` S, `VrstaManipulacije` S
- **Procedure:** `InsUbaciDodatnuUsluguIzvoz`, `InsUbaciDodatnuUsluguUvoz`, `ZavrsiDodatnuUsluguIzvozZadata`, `ZavrsiDodatnuUsluguUvozZadata`
- **Combobox → tabela:** `cboBukingPrijema` ← `Voz`; `cboUsluga` ← `VrstaManipulacije`
- **Funkcionalnosti:**
  - „Dodeli slike“ — klik → `button1_Click_1` — otvara: frmIzvozDokumenta, UvozDokumenta
  - „Sačuvaj promenu“ — klik → `button4_Click` — proc: InsUbaciDodatnuUsluguIzvoz, InsUbaciDodatnuUsluguUvoz, ZavrsiDodatnuUsluguIzvozZadata, ZavrsiDodatnuUsluguUvozZadata; preko: InsertUvozKonacna.InsUbaciDodatnuUsluguIzvoz, InsertUvozKonacna.InsUbaciDodatnuUsluguUvoz, InsertUvozKonacna.ZavrsiDodatnuUsluguIzvozZadata, InsertUvozKonacna.ZavrsiDodatnuUsluguUvozZadata
  - „Pozovi“ — klik → `button3_Click` — čita 8 tabela
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - `this` — otvaranje forme → `frmDodatneUsluge_Load` — čita: Voz, VrstaManipulacije

#### frmDodatniList [N+S]
- **Fajl:** `Izvestaji/frmDodatniList.cs` — „Dodatni list“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Formiraj otpremu kamionom“ → **frmOtpremaKontejneraKamionomIzKontejnera** → „Otvori“ → **frmOtpremaKontejnera** → „Štampa dodatni list“ → **frmDodatniList**
- **Stari izgled:** Terminal › Radni nalozi › Otprema kontejnera voz - N → **frmOtpremaKontejneraLegetIZVOZ** → „Štampa dodatni list“ → **frmDodatniList**
- **Funkcionalnosti:**
  - „Štampaj“ — klik → `btnStampa_Click` — izveštaj: rptDodatniList.rdlc

#### frmDodatniList [N+S]
- **Fajl:** `TrackModal/Izvestaji/frmDodatniList.cs` — „Dodatni list“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Voz“ → **frmPrijemIOtpremaVozova4** → „Gate out voz“ → **frmPregledOtpreme** → „Otvori voz“ → **frmOtpremaKontejnera** → „Štampa dodatni list“ → **frmDodatniList**
- **Stari izgled:** Terminal › Prevoz › Otprema kontejnera vozom → **frmPregledOtpreme** → „Otvori voz“ → **frmOtpremaKontejnera** → „Štampa dodatni list“ → **frmDodatniList**
- **Funkcionalnosti:**
  - „Štampaj“ — klik → `btnStampa_Click` — izveštaj: rptDodatniList.rdlc

#### frmDodelaSkladista [N+S]
- **Fajl:** `RadniNalozi/frmDodelaSkladista.cs` — „ODREDI POZICIJU“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Voz“ → **frmPrijemIOtpremaVozova4** → „RN prijem voza“ → **RN1PrijemVoza** → „Dodela polja“ → **frmDodelaSkladista**
- **Stari izgled:** Terminal › Radni nalozi → **RN1PrijemVoza** → „Dodela polja“ → **frmDodelaSkladista**
- **Otvara:** frmPregledSkladistaNovi („MAPA SKLADISTA“)
- **Tabele:** `KontejnerTekuce` S, `Partnerji` S, `Pozicija` S, `RNMedjuskladisni` S, `RNOtpremaPlatforme` S, `RNPrijemPlatforme` S, `RNPrijemPlatforme2` S, `RNPrijemVoza` S, `Skladista` S, `TipKontenjera` S, `Voz` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S
- **Procedure:** `UpdateRN12MedjuskladisniSkladiste`, `UpdateRN4PrivremenoSkladiste`, `UpdateRN4SkladisteIzCira`, `UpdateRN5PrijemSkladiste`, `UpdateRN6OtpremaSkladiste`, `UpdateRNPrijemVozaSkladiste`
- **Funkcionalnosti:**
  - „Seleckovano polje >> Selectovan zapis“ — klik → `btnUnesi_Click` — proc: UpdateRN12MedjuskladisniSkladiste, UpdateRN4PrivremenoSkladiste, UpdateRN4SkladisteIzCira, UpdateRN5PrijemSkladiste, UpdateRN6OtpremaSkladiste, UpdateRNPrijemVozaSkladiste; preko: InsertRN.UpdateRN12Skladiste, InsertRN.UpdateRN1Skladiste, InsertRN.UpdateRN4PrivremenoSkladiste, InsertRN.UpdateRN4SkladisteIzCIRA, InsertRN.UpdateRN5Skladiste
  - „MAPA SKLADISTA“ — klik → `toolStripButton7_Click` — otvara: frmPregledSkladistaNovi

#### frmDogovori [S]
- **Fajl:** `Dokumenta/frmDogovori.cs` — „Ugovori“
- **Stari izgled:** Komercijala › Ugovori › Unos ugovora → **frmDogovori**
- **Tabele:** `MAticniPodatki` S, `NacinDobave` S, `Narocilo` SIUD p, `NarociloPostav` S, `Partnerji` S, `TipSaobPrevoza` S
- **Procedure:** `DeleteNarocilo`, `DeleteNarociloStavke`, `InsertNarocilo`, `InsertNarociloStavke`, `UpdateNarocilo`, `UpdateNarociloStavke`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertNarocilo, UpdateNarocilo; preko: InsertNarocilo.InsNarocilo, InsertNarocilo.UpdNarocilo
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteNarocilo; preko: InsertNarocilo.DeleteNarocilo
  - „Izbriši“ — klik → `button2_Click` — proc: DeleteNarociloStavke; preko: InsertNarocilo.DeleteNarociloStavka
  - „Promeni“ — klik → `button1_Click` — proc: UpdateNarociloStavke; preko: InsertNarocilo.UpdNarociloStavka
  - „Ubaci“ — klik → `button11_Click` — proc: InsertNarociloStavke; preko: InsertNarocilo.InsNarociloStavka
  - `this` — otvaranje forme → `frmDogovori_Load` — čita: MaticniPOdatki, NacinDobave, Partnerji, TipSaobPrevoza

#### frmDogovoriPregled [S]
- **Fajl:** `Dokumenta/frmDogovoriPregled.cs` — „Pregled komercijalnih dogovora“
- **Stari izgled:** Komercijala › Ugovori › Pregled dogovora → **frmDogovoriPregled**
- **Otvara:** frmDogovori („Otvori dogovor“)
- **Tabele:** `NacinDobave` S, `Narocilo` S, `Partnerji` S, `TipSaobPrevoza` S
- **Funkcionalnosti:**
  - „Otvori dogovor“ — klik → `toolStripButton5_Click` — otvara: frmDogovori

#### frmDokumentaNajava [S]
- **Fajl:** `Dokumenta/frmDokumentaNajava.cs` — „Dokumenta najava“
- **Stari izgled:** Komercijala › Najave › Poslovi → **frmNajava** → „Dokumenta posla“ → **frmDokumentaNajava**
- **Tabele:** `NajavaDokumenta` SID p
- **Procedure:** `DeleteNajavaDokumenta`, `InsertNajavaDokumenta`
- **Funkcionalnosti:**
  - „toolStrip1“ — ItemClicked → `toolStrip1_ItemClicked`
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click`
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteNajavaDokumenta; preko: InsertNajavaDokumenta.DelNajDokumenta
  - „Pronađi“ — klik → `button1_Click`
  - „Otvori“ — klik → `button2_Click`
  - „Pronađi“ — klik → `button3_Click`
  - „Sačuvaj“ — klik → `button4_Click` — proc: InsertNajavaDokumenta; preko: InsertNajavaDokumenta.InsNajDokumenta
  - „Sačuvaj“ — klik → `button6_Click` — proc: InsertNajavaDokumenta; preko: InsertNajavaDokumenta.InsNajDokumenta
  - „Sačuvaj“ — klik → `btnSacuvajTovarniList_Click` — proc: InsertNajavaDokumenta; preko: InsertNajavaDokumenta.InsNajDokumenta
  - „Pronađi“ — klik → `btnTovarniList_Click`
  - „Sačuvaj“ — klik → `btnCIT23Sacuvaj_Click` — proc: InsertNajavaDokumenta; preko: InsertNajavaDokumenta.InsNajDokumenta
  - „Pronađi“ — klik → `btnCIT23_Click`
  - „Sačuvaj“ — klik → `btnRacunSacuvaj_Click` — proc: InsertNajavaDokumenta; preko: InsertNajavaDokumenta.InsNajDokumenta
  - „Pronađi“ — klik → `btnRacun_Click`
  - „Dokumentacija pripremljena formiraj pdf “ — klik → `button12_Click`
  - „Sačuvaj“ — klik → `button13_Click` — proc: InsertNajavaDokumenta; preko: InsertNajavaDokumenta.InsNajDokumenta
  - „Pronađi“ — klik → `button14_Click`
  - „Otvori“ — klik → `button5_Click_1`

#### frmDokumentaOtpremaKontejnera [N+S]
- **Fajl:** `TrackModal/Dokumeta/frmDokumentaOtpremaKontejnera.cs` — „Dokumenta otprema kontejnera“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Voz“ → **frmPrijemIOtpremaVozova4** → „Gate out voz“ → **frmPregledOtpreme** → „Otvori voz“ → **frmOtpremaKontejnera** → „Dokumenta otpreme“ → **frmDokumentaOtpremaKontejnera**
- **Stari izgled:** Terminal › Prevoz › Otprema kontejnera vozom → **frmPregledOtpreme** → „Otvori voz“ → **frmOtpremaKontejnera** → „Dokumenta otpreme“ → **frmDokumentaOtpremaKontejnera**
- **Tabele:** `DokumentaOtpremaKontejneraVoz` SID p
- **Procedure:** `DeleteDokumentaOtpremaKontejneraVoz`, `InsertDokumentaOtpremaKontejneraVoz`
- **Funkcionalnosti:**
  - „Sačuvaj“ — klik → `button4_Click` — proc: InsertDokumentaOtpremaKontejneraVoz; preko: InsertDokumentaOtpremaVoz.InsOtpremaDokumenta
  - „Otvori“ — klik → `button2_Click`
  - „Pronađi“ — klik → `button1_Click`
  - „Brisanje“ — klik → `button3_Click` — proc: DeleteDokumentaOtpremaKontejneraVoz; preko: InsertDokumentaOtpremaVoz.DelOtpremaDokumenta

#### frmDokumentaOtpremaKontejnera [N+S]
- **Fajl:** `Dokumenta/frmDokumentaOtpremaKontejnera.cs` — „Dokumenta otprema kontejnera“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Formiraj otpremu kamionom“ → **frmOtpremaKontejneraKamionomIzKontejnera** → „Otvori“ → **frmOtpremaKontejneraIzvozKamion** → „Dokumenta otpreme“ → **frmDokumentaOtpremaKontejnera**
- **Stari izgled:** Terminal › Radni nalozi › Otprema kontejnera Kamion - N → **frmOtpremaKontejneraUvozKamion** → „Dokumenta otpreme“ → **frmDokumentaOtpremaKontejnera**
- **Tabele:** `DokumentaOtpremaKontejneraVoz` SID p
- **Procedure:** `DeleteDokumentaOtpremaKontejneraVoz`, `InsertDokumentaOtpremaKontejneraVoz`
- **Funkcionalnosti:**
  - „Sačuvaj“ — klik → `button4_Click` — proc: InsertDokumentaOtpremaKontejneraVoz; preko: InsertDokumentaOtpremaVoz.InsOtpremaDokumenta
  - „Otvori“ — klik → `button2_Click`
  - „Pronađi“ — klik → `button1_Click`
  - „Brisanje“ — klik → `button3_Click` — proc: DeleteDokumentaOtpremaKontejneraVoz; preko: InsertDokumentaOtpremaVoz.DelOtpremaDokumenta

#### frmDokumentaPrijemKontejneraKamion [N+S]
- **Fajl:** `Dokumenta/frmDokumentaPrijemKontejneraKamion.cs` — „Dokumenta prijem kontejnera kamionom“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Potvrdi“ → **frmPrijemKontejneraKamionLegetUvoz** → „Dokumenta prijema“ → **frmDokumentaPrijemKontejneraKamion**
- **Stari izgled:** Terminal › Radni nalozi › Prijem kontejnera kamion - N → **frmPrijemKontejneraKamionLegetUvoz** → „Dokumenta prijema“ → **frmDokumentaPrijemKontejneraKamion**
- **Tabele:** `DokumentaPrijemKontejneraKamion` SI p
- **Procedure:** `InsertDokumentaPrijemKontejneraKamion`
- **Funkcionalnosti:**
  - „Sačuvaj“ — klik → `button4_Click` — proc: InsertDokumentaPrijemKontejneraKamion; preko: InsertDokumentaPrijemKontejneraKamion.InsPrijemDokumenta
  - „Otvori“ — klik → `button2_Click`
  - „Pronađi“ — klik → `button1_Click`

#### frmDokumentaPrijemKontejneraVoz [N+S]
- **Fajl:** `Dokumenta/frmDokumentaPrijemKontejneraVoz.cs` — „Dokumenta prijema“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Potvrdi“ → **frmPrijemVozaIzPlana** → „OTVORI - GATE IN VOZ“ → **frmPrijemKontejneraLegetVozUvoz** → „Dokumenta prijema“ → **frmDokumentaPrijemKontejneraVoz**
- **Stari izgled:** Terminal › Radni nalozi › Prijem kontejnera voz - N → **frmPrijemKontejneraLegetVozUvoz** → „Dokumenta prijema“ → **frmDokumentaPrijemKontejneraVoz**
- **Tabele:** `DokumentaPrijemKontejneraVoz` S
- **Funkcionalnosti:**
  - „Sačuvaj“ — klik → `button4_Click`
  - „Otvori“ — klik → `button2_Click`
  - „Pronađi“ — klik → `button1_Click`
  - „Izbriši“ — klik → `button3_Click`

#### frmDokumentaRadniNalog [S]
- **Fajl:** `Dokumenta/frmDokumentaRadniNalog.cs` — „Dokumenta radni nalog“
- **Stari izgled:** Komercijala › Najave › Poslovi → **frmNajava** → „Prevozni nalog - Trase i Lokomotive“ → **frmRadniNalog** → „Dokumenta“ → **frmDokumentaRadniNalog**
- **Tabele:** `RadniNalogDokumenta` SI p
- **Procedure:** `InsertRadniNalogDokumenta`
- **Funkcionalnosti:**
  - „Sačuvaj“ — klik → `button4_Click` — proc: InsertRadniNalogDokumenta; preko: InsertRadniNalogDokumenta.InsRNDokumenta
  - „Pronađi“ — klik → `button3_Click`
  - „Otvori“ — klik → `button2_Click`
  - „Pronađi“ — klik → `button1_Click`
  - „Novi“ — klik → `tsNew_Click`
  - „Pronađi“ — klik → `button5_Click`
  - „Sačuvaj“ — klik → `button7_Click` — proc: InsertRadniNalogDokumenta; preko: InsertRadniNalogDokumenta.InsRNDokumenta
  - „Sačuvaj“ — klik → `button8_Click` — proc: InsertRadniNalogDokumenta; preko: InsertRadniNalogDokumenta.InsRNDokumenta
  - „Pronađi“ — klik → `button9_Click`
  - „Sačuvaj“ — klik → `button10_Click` — proc: InsertRadniNalogDokumenta; preko: InsertRadniNalogDokumenta.InsRNDokumenta
  - „Pronađi“ — klik → `button11_Click`
  - „Sačuvaj“ — klik → `button12_Click` — proc: InsertRadniNalogDokumenta; preko: InsertRadniNalogDokumenta.InsRNDokumenta
  - „Pronađi“ — klik → `button13_Click`
  - „Otvori“ — klik → `button14_Click`
  - „Otvori“ — klik → `button15_Click`
  - „Otvori“ — klik → `button16_Click`
  - „Otvori“ — klik → `button17_Click`
  - „Sačuvaj“ — klik → `button18_Click` — proc: InsertRadniNalogDokumenta; preko: InsertRadniNalogDokumenta.InsRNDokumenta
  - „Pronađi“ — klik → `button19_Click`
  - „Otvori“ — klik → `button20_Click`
  - „Otvori“ — klik → `button21_Click`
  - „Sačuvaj“ — klik → `button22_Click` — proc: InsertRadniNalogDokumenta; preko: InsertRadniNalogDokumenta.InsRNDokumenta

#### frmDokumentaVozila [S]
- **Fajl:** `TrackModal/Dokumeta/frmDokumentaVozila.cs` — „Dokumenta vozila“
- **Stari izgled:** Terminal › Šifarnici › Sredstva rada → **frmVozila** → „Dokumentacija“ → **frmDokumentaVozila**
- **Tabele:** `DokumentaVozila` SID p
- **Procedure:** `DeleteDokumentaVozila`, `InsertDokumentaVozila`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „Sačuvaj“ — klik → `button4_Click` — proc: InsertDokumentaVozila; preko: InsertDokumentaVozila.InsVozDokumenta
  - „Otvori“ — klik → `button2_Click`
  - „Pronađi“ — klik → `button1_Click`
  - „Izbriši“ — klik → `button3_Click` — proc: DeleteDokumentaVozila; preko: InsertDokumentaVozila.DelVozDokumenta

#### frmDopunaPlanaPraznimIzvoz [N+S]
- **Fajl:** `Izvoz/frmDopunaPlanaPraznimIzvoz.cs` — „DOPUNA PLANA PRAYNIM KONTEJNERIMA IZVOZ ILI TERMINAL“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Voz“ → **frmPrijemIOtpremaVozova4** → „Dopuna plana praznim cnt“ → **frmDopunaPlanaPraznimIzvoz**
- **Stari izgled:** Terminal › Terminalske usluge › Dopuna plana Praznim kont → **frmDopunaPlanaPraznimIzvoz**
- **Tabele:** `Carinarnice` S, `InspekciskiTretman` S, `IzvozKonacna` S, `IzvozKonacnaZaglavlje` S, `IzvozNHM` S, `IzvozVrstaManipulacije` S, `IzvozVrstaRobeHS` S, `KontejnerskiTerminali` S, `KontejnerStatus` S, `KontejnerTekuce` S, `KrajnjaDestinacija` S, `MestaUtovara` S, `NHM` S, `Partnerji` S, `SerijeKola` S, `Skladista` S, `TipKontenjera` S, `uvNacinPakovanja` S, `Voz` S, `VozSerijeKola` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaRobeHS` S, `VrstePostupakaUvoz` S
- **Procedure:** `PrenesiUPlanUtovaraSelektovanoIzvozTErminal`
- **Combobox → tabela:** `cboPlanUtovara` ← `IzvozKonacnaZaglavlje`, `Voz`
- **Funkcionalnosti:**
  - „Prenesi selektovano“ — klik → `button1_Click` — proc: PrenesiUPlanUtovaraSelektovanoIzvozTErminal; preko: InsertIzvozKonacna.PrenesiUPlanUtovaraPrazan
  - „Refresh“ — klik → `button2_Click`

#### frmDrumski [N+S]
- **Fajl:** `Drumski/frmDrumski.cs` — „DRUMSKI UNOSNA FORMA“
- **Novi izgled:** Drumski transport → **Drumski1** → „Cerada“ → **Cerade** → „NALOG ZA DRUMSKI TRANSPORT“ → **NalogZaDrumski** → „TRANSPORTNI NALOZI IZVOZ“ → **frmPregledNalogaDrumski** → „Otvori“ → **frmDrumski**
- **Stari izgled:** Drumski › Drumski › Lista naloga → **frmPregledNalogaDrumski** → „Otvori“ → **frmDrumski**
- **Otvara:** frmPregledFajlova („Otvori dokumenta“), frmPartnerji („Nova špedicija“)
- **Tabele:** `Automobili` S, `Carinarnice` S, `Delavci` S, `DrumskiPozicioniranje` S, `Izvoz` S, `IzvozKonacna` S, `IzvozKonacnaZaglavlje` S, `Korisnici` S, `MestaUtovara` S, `NapomenaZaPozicioniranje` S, `Partnerji` S, `partnerjiKontOsebaMU` S, `RadniNalogDrumski` SIUD p, `RadniNalogInterni` S, `SistemskePostavke` S, `StatusVozila` S, `TipKontenjera` S, `Uvoz` S, `UvozKonacna` S, `UvozKonacnaZaglavlje` S, `Valute` S, `Voz` S, `VrstaCarinskogPostupka` S, `VrstaVozila` S, `VrstePostupakaUvoz` S
- **Procedure:** `DeleteRadniNalogDrumski`, `InsertDokumentaRadniNalogDrumski`, `InsertRadniNalogDrumski`, `NapraviRadniNalogDrumskiIzDrumskog`, `UpdateRadniNalogDrumski`, `UpdateRadniNalogInterniZavrsen`
- **Combobox → tabela:** `cbOspedicija` ← `Partnerji`; `cboPolaznaSpedicija` ← `Partnerji`; `txtValuta` ← `Valute`; `cboStatus` ← `StatusVozila`; `cboTipTransporta` ← `VrstaVozila`; `cboKlijent` ← `Partnerji`; `cboMestoPreuzimanja` ← `MestaUtovara`; `cboMestoUtovara` ← `MestaUtovara`; `cboMestoIstovara` ← `MestaUtovara`; `cboNapomenaPoz` ← `DrumskiPozicioniranje`; `cboOCarinarnica` ← `Carinarnice`; `cboPolaznaCarinarnica` ← `Carinarnice`; `cboVrstaKontejnera` ← `TipKontenjera`; `cboKamion` ← `Automobili`, `RadniNalogDrumski`, `SistemskePostavke`
- **Funkcionalnosti:**
  - „Nova špedicija“ — klik → `btnPartner_Click` — otvara: frmPartnerji
  - „Otvori dokumenta“ — klik → `button4_Click` — otvara: frmPregledFajlova
  - „Dodaj dokument“ — klik → `button1_Click` — proc: InsertDokumentaRadniNalogDrumski; preko: InsertRadniNalogDrumski.SnimiUFajlBazu
  - „Formiranje naloga“ — klik → `btnFormiranjeNaloga_Click` — proc: NapraviRadniNalogDrumskiIzDrumskog; preko: InsertRadniNalogDrumski.PostaviNalogIDNaRedove
  - `button21` — klik → `button21_Click_1` — proc: DeleteRadniNalogDrumski; preko: InsertRadniNalogDrumski.DelRadniNalogDrumski
  - `button2` — klik → `button21_Click` — čita: RadniNalogDrumski, RadniNalogInterni, SistemskePostavke; proc: InsertRadniNalogDrumski, UpdateRadniNalogDrumski, UpdateRadniNalogInterniZavrsen; preko: InsertRadniNalogDrumski.InsRadniNalogDrumski, InsertRadniNalogDrumski.UpdateRadniNalogDrumski, InsertRadniNalogInterni.UpdRadniNalogInterniZavrsen

#### frmDrumski1 [N+S]
- **Fajl:** `Drumski/frmDrumski1.cs` — „DRUMSKI UNOSNA FORMA“
- **Novi izgled:** Drumski transport → **Drumski1** → „Cerada“ → **Cerade** → „NALOG ZA DRUMSKI TRANSPORT“ → **NalogZaDrumski** → „Kreiraj novi“ → **frmDrumski1**
- **Stari izgled:** Drumski › Drumski › Lista naloga → **frmPregledNalogaDrumski** → „Otvori“ → **frmDrumski1**
- **Otvara:** frmPregledFajlova („Otvori dokumenta“), frmPartnerji („Nova špedicija“)
- **Tabele:** `Automobili` S, `Carinarnice` S, `Delavci` S, `DrumskiPozicioniranje` S, `Izvoz` S, `IzvozKonacna` S, `IzvozKonacnaZaglavlje` S, `IzvozNapomenePozicioniranja` S, `IzvozNHM` S, `KontejnerskiTerminali` S, `Korisnici` S, `MestaUtovara` S, `NapomenaZaPozicioniranje` S, `NHM` S, `Partnerji` S, `partnerjiKontOsebaMU` S, `RadniNalogDrumski` S, `RadniNalogDrumskiNHM` SI p, `RadniNalogInterni` S, `SistemskePostavke` S, `StatusVozila` S, `TipKontenjera` S, `uvKvalitetKontejnera` S, `uvNacinPakovanja` S, `Uvoz` S, `UvozKonacna` S, `UvozKonacnaZaglavlje` S, `Valute` S, `Voz` S, `VrstaCarinskogPostupka` S, `VrstaRobeADR` S, `VrstaVozila` S, `VrstePostupakaUvoz` S
- **Procedure:** `InsertDokumentaRadniNalogDrumski`, `InsertRadniNalogDrumskiNHM`, `InsertRadniNalogDrumskiNovi`, `NapraviRadniNalogDrumskiIzDrumskog`, `UpdateRadniNalogDrumskiIzvoz`, `UpdateRadniNalogDrumskiNovi`, `UpdateRadniNalogDrumskiOdobrioLO`, `UpdateRadniNalogInterniZavrsen`
- **Combobox → tabela:** `cbOspedicijaI1` ← `Partnerji`; `cboPolaznaSpedicijaI1` ← `Partnerji`; `txtValutaI1` ← `Valute`; `cboStatusI1` ← `StatusVozila`; `cboTipTransportaI1` ← `VrstaVozila`; `cboKlijentI1` ← `Partnerji`; `cboMestoSpustanjaPunogI1` ← `KontejnerskiTerminali`, `MestaUtovara`; `cboNapomenaPozI1` ← `DrumskiPozicioniranje`, `NapomenaZaPozicioniranje`; `cboOCarinarnicaI1` ← `Carinarnice`; `cboCarinskiPUnutrasniTransportI1` ← `VrstaCarinskogPostupka`; `cboPolaznaCarinarnicaI1` ← `Carinarnice`; `cboVrstaKontejneraI1` ← `TipKontenjera`; `cboADRI1` ← `VrstaRobeADR`; `cboKvalitetKontejneraI1` ← `uvKvalitetKontejnera`; `cboVrstaRobeI1` ← `NHM`; `cboNacinPakovanjaI1` ← `uvNacinPakovanja`; `cboCilj` ← `partnerjiKontOsebaMU`; `cboKontakt` ← `partnerjiKontOsebaMU`
- **Funkcionalnosti:**
  - `button2` — klik → `button2_Click` — čita: RadniNalogDrumski, RadniNalogInterni, SistemskePostavke; proc: InsertRadniNalogDrumskiNHM, InsertRadniNalogDrumskiNovi, UpdateRadniNalogDrumskiIzvoz, UpdateRadniNalogDrumskiNovi, UpdateRadniNalogInterniZavrsen; preko: InsertRadniNalogDrumski.InsRadniNalogDrumskiNHM, InsertRadniNalogDrumski.InsRadniNalogDrumskiNovi, InsertRadniNalogDrumski.UpdRadniNalogDrumskiIzvoz, InsertRadniNalogDrumski.UpdRadniNalogDrumskiNovi, InsertRadniNalogInterni.UpdRadniNalogInterniZavrsen
  - „Formiranje naloga“ — klik → `btnFormiranjeNaloga_Click` — proc: NapraviRadniNalogDrumskiIzDrumskog; preko: InsertRadniNalogDrumski.PostaviNalogIDNaRedove
  - „Dodaj dokument“ — klik → `button1_Click` — proc: InsertDokumentaRadniNalogDrumski; preko: InsertRadniNalogDrumski.SnimiUFajlBazu
  - „Otvori dokumenta“ — klik → `button4_Click` — otvara: frmPregledFajlova
  - „Nova špedicija“ — klik → `btnPartner_Click` — otvara: frmPartnerji
  - „Potvrdi LO nalog za Transport“ — klik → `btnPovrdiLO_Click` — proc: UpdateRadniNalogDrumskiOdobrioLO; preko: InsertRadniNalogDrumski.UpdateOdobrioLO
  - „Unesi“ — klik → `btnUnesiNHM_Click`
  - „Unesi“ — klik → `btnUnesiNHM_Click`

#### frmEvidencijaGodišnjihOdmora [S]
- **Fajl:** `Dokumenta/frmEvidencijaGodišnjihOdmora.cs` — „Evidencija Godišnjih odmora“
- **Stari izgled:** Kadrovi › Godisnji odmori › Evidencija GO → **frmEvidencijaGodišnjihOdmora**
- **Otvara:** frmEvidencijaGodLOG („LOG“)
- **Tabele:** `1ReportGO` SI p, `Delavci` S, `DelovnaMesta` S, `Dopust` S, `DopustStavke` SIUD p, `EvidencijaZahteva` S, `EvidencujaZahtevaVrsta` S, `Razporeditve` S
- **Procedure:** `DeleteDopustStavke`, `Insert1REportGO`, `InsertDopustStavke`, `sp_InsertGoLOG`, `UpdateDopustStavke`, `UpdateEVGOPoslatMail`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click` — proc: sp_InsertGoLOG; preko: InsertEvidencijaGOLog.InsertGoLOG
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertDopustStavke, UpdateDopustStavke, sp_InsertGoLOG; preko: InsertEvidencijaGOLog.InsertGoLOG, InsertEvidencijaGodisnjihOdmora.InsEvidGodisnjihOdmora, InsertEvidencijaGodisnjihOdmora.UpdEvidGodisnjihOdmora
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteDopustStavke; preko: InsertEvidencijaGodisnjihOdmora.DeleteDopustStavke
  - „Pripremi štampu/email“ — klik → `toolStripButton1_Click` — proc: Insert1REportGO; izveštaj: rptGodisnjiOdmor.rdlc; preko: Insert1ReportGO.InsReportGO
  - „LOG“ — klik → `toolStripButton2_Click` — otvara: frmEvidencijaGodLOG
  - „Poslato Resenje“ — čekiranje → `cbResenje_CheckedChanged` — proc: sp_InsertGoLOG; preko: InsertEvidencijaGOLog.InsertGoLOG
  - „Poslat Email“ — čekiranje → `cbMail_CheckedChanged` — proc: sp_InsertGoLOG; preko: InsertEvidencijaGOLog.InsertGoLOG
  - „Pošalji mail“ — klik → `btnPosaljiMail_Click`
  - „Prikaži“ — klik → `button1_Click`
  - `this` — otvaranje forme → `frmEvidencijaGodišnjihOdmora_Load` — čita: Delavci, Dopust

#### frmEvidencijaGodLOG [S]
- **Fajl:** `Dokumenta/frmEvidencijaGodLOG.cs` — „EvidencijaGodLOG“
- **Stari izgled:** Kadrovi › Godisnji odmori › Evidencija GO → **frmEvidencijaGodišnjihOdmora** → „LOG“ → **frmEvidencijaGodLOG**
- **Tabele:** `EvidencijaGOLog` S

#### frmEvidencijaKvarova [S]
- **Fajl:** `Servis/frmEvidencijaKvarova.cs` — „EvidencijaKvarova“
- **Stari izgled:** Održavanje › Obrada kvarova › Prijavljeni kvarovi → **frmEvidencijaKvarova**
- **Tabele:** `Delavci` S, `EvidencijaKvarova` S, `GrupaKvarova` S, `Kvarovi` S, `Mesta` S, `StatusKvara` S
- **Procedure:** `DeleteStampajKvarovi`, `InsertStampajKvarovi`, `UpdateEvidencijaKvarovaPoID`, `UpdateEvidencijaKvarovaPromeniKvar`
- **Funkcionalnosti:**
  - `this` — otvaranje forme → `frmEvidencijaKvarova_Load` — čita: Delavci, GrupaKvarova, Mesta, StatusKvara
  - „Ažuriraj sve“ — klik → `metroButton1_Click`
  - „?“ — klik → `cboPretraziLokomotivu_Click`
  - „Promeni status“ — klik → `metroButton2_Click` — proc: UpdateEvidencijaKvarovaPoID; preko: InsertKvarovi.UpdEvidenciKvarovaPoID
  - „?“ — klik → `btnPickDirectory_Click`
  - „Štampaj“ — klik → `metroButton6_Click` — izveštaj: rptEvidencijaKvarova.rdlc
  - „Selektovani za štampu“ — klik → `metroButton5_Click` — proc: DeleteStampajKvarovi, InsertStampajKvarovi; preko: InsertKvarovi.DeleteStampajKvarovi, InsertKvarovi.InsStampajKvarovi
  - „Promeni kvar“ — klik → `metroButton3_Click` — proc: UpdateEvidencijaKvarovaPromeniKvar; preko: InsertKvarovi.UpdEvidenciKvarovaPromeniKvar
  - `cboGrupaKvara` — izbor → `cboGrupaKvara_SelectedValueChanged` — čita: Kvarovi
  - „Prikazi po kvaru“ — klik → `metroButton4_Click`

#### frmEvidencijaKvarovaAnaliza [S]
- **Fajl:** `Servis/frmEvidencijaKvarovaAnaliza.cs` — „Analiza evidencije kvarova“
- **Stari izgled:** Održavanje › Obrada kvarova › Analiza kvarova → **frmEvidencijaKvarovaAnaliza**
- **Tabele:** `Delavci` S, `EvidencijaKvarova` S, `GrupaKvarova` S, `Kvarovi` S, `StatusKvara` S
- **Funkcionalnosti:**
  - `this` — otvaranje forme → `frmEvidencijaKvarovaAnaliza_Load` — čita: Delavci, EvidencijaKvarova, GrupaKvarova, Kvarovi, StatusKvara

#### frmEvidencijaRada [S]
- **Fajl:** `Dokumenta/frmEvidencijaRada.cs` — „Unos rada radnika“
- **Stari izgled:** Evidencije › Evidencija aktivnosti › Evidencija → **frmEvidencijaRada**
- **Otvara:** frmEvidencijaRadaDokumenti („Računi“), frmLogAktivnosti („Log file“)
- **Tabele:** `Aktivnosti` SU p, `AktivnostiPrvi` S, `AktivnostiStavke` SID p, `Config` S, `Delavci` S, `Kraji` S, `LogAktivnosti` I p, `PravoAktivnosti` S, `VrstaAktivnosti` S
- **Procedure:** `DeleteAktivnostiStavke`, `InsertAktivnostiStavke`, `InsertAktivnostiStavkeMas`, `InsertLogAktivnosti`, `PromeniKoeficijentAktivnostiStavke`, `PromeniSatiAktivnostiStavke`, `ProveriAktivnostPostoji`, `UpdateAktivnosti`, `UpdateAktivnostiZarada`
- **Funkcionalnosti:**
  - „Osveži“ — ItemClicked → `toolStrip1_ItemClicked`
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertAktivnosti, InsertAktivnostiPrvi, InsertLogAktivnosti, UpdateAktivnosti; preko: InsertAktivnosti.InsAktivnosti, InsertAktivnosti.InsAktivnostiPrvi, InsertAktivnosti.UpdAktivnosti, InsertLogAktivnosti.InsLog
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „Plaćeno“ — klik → `toolStripButton1_Click`
  - „Računi“ — klik → `toolStripButton3_Click` — otvara: frmEvidencijaRadaDokumenti
  - „Kontrolisano outside“ — klik → `toolStripButton2_Click` — proc: UpdateAktivnostiKontrolisanoSpoljno; preko: InsertAktivnosti.UpdateAktivnostiKontrolisanoSpoljno
  - „Log file“ — klik → `toolStripButton4_Click` — otvara: frmLogAktivnosti
  - „Unesi po satu“ — klik → `btnUbaciAktivnost_Click` — proc: InsertAktivnostiStavke; preko: InsertAktivnostiStavke.InsAktivnostiStavke
  - „>>“ — klik → `btnUnesi_Click` — proc: InsertAktivnostiStavke; preko: InsertAktivnostiStavke.InsAktivnostiStavke
  - „Pošalji mail“ — klik → `btnPosaljiMail_Click` — čita: Aktivnosti, AktivnostiStavke, Delavci, VrstaAktivnosti; proc: UpdateAktivnostiPoslatMail; preko: InsertAktivnosti.UpdatePoslaoMailAktivnosti
  - „Izbriši stavku“ — klik → `button1_Click` — proc: DeleteAktivnostiStavke; preko: InsertAktivnostiStavke.DeleteAktivnostiStavke
  - „Kraj unosa“ — klik → `button2_Click` — proc: InsertAktivnostiStavke, UpdateAktivnostiZarada; preko: InsertAktivnosti.UpdAktivnostiZarada, InsertAktivnostiStavke.InsAktivnostiStavke
  - „Refresh“ — klik → `button3_Click`
  - „H“ — klik → `button4_Click` — proc: PromeniSatiAktivnostiStavke; preko: InsertAktivnostiStavke.PromeniSati
  - „Nadležni mašinovodja stavke“ — klik → `button12_Click`
  - „Snimi selektovane“ — klik → `button5_Click` — proc: InsertAktivnostiStavkeMas; preko: InsertAktivnostiStavke.InsAktivnostiStavkeMas
  - „K“ — klik → `button6_Click` — proc: PromeniKoeficijentAktivnostiStavke; preko: InsertAktivnostiStavke.PromeniKoeficijent
  - `this` — otvaranje forme → `frmEvidencijaRada_Load` — čita: Delavci, Kraji

#### frmEvidencijaRadaCG [S]
- **Fajl:** `Dokumenta/frmEvidencijaRadaCG.cs` — „Evidencija Rada CG“
- **Stari izgled:** Evidencije › Evidencija aktivnosti › Pretraga smena → **frmEvidencijaRadaZaglavlje** → „Otvori zapis“ → **frmEvidencijaRadaCG**
- **Tabele:** `Aktivnosti` SIU p, `AktivnostiStavke` S, `Delavci` S, `VrstaAktivnosti` S
- **Procedure:** `InsertAktivnosti`, `UpdateAktivnosti`, `UpdateAktivnostiPoslatMail`
- **Funkcionalnosti:**
  - „Pošalji mail“ — klik → `btnPosaljiMail_Click` — čita: Aktivnosti, AktivnostiStavke, Delavci, VrstaAktivnosti; proc: UpdateAktivnostiPoslatMail; preko: InsertAktivnosti.UpdatePoslaoMailAktivnosti
  - „>>“ — klik → `btnUnesi_Click` — proc: InsertAktivnostiStavke; preko: InsertAktivnostiStavke.InsAktivnostiStavke
  - „Unesi po satu“ — klik → `btnUbaciAktivnost_Click` — proc: InsertAktivnostiStavke; preko: InsertAktivnostiStavke.InsAktivnostiStavke
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertAktivnosti, UpdateAktivnosti; preko: InsertAktivnosti.InsAktivnosti, InsertAktivnosti.UpdAktivnosti
  - „Novi“ — klik → `tsNew_Click`
  - `this` — otvaranje forme → `frmEvidencijaRada_Load` — čita: Delavci, VrstaAktivnosti

#### frmEvidencijaRadaDokumenti [S]
- **Fajl:** `Dokumenta/frmEvidencijaRadaDokumenti.cs` — „Troskovi sa terena sa slikama“
- **Stari izgled:** Evidencije › Evidencija aktivnosti › Troskovi po smeni / Slike → **frmEvidencijaRadaDokumenti**
- **Tabele:** `Aktivnosti` S, `AktivnostiDokumenta` SI p, `Delavci` S, `Kraji` S
- **Procedure:** `InsertAktivnostiDokumenta`
- **Funkcionalnosti:**
  - „Prikazi samo atovnosti koje imaju slike“ — klik → `tsNew_Click`
  - „Sačuvaj“ — klik → `button4_Click` — proc: InsertAktivnostiDokumenta; preko: InsertAktivnostiDokumenta.InsNajDokumenta
  - „Otvori“ — klik → `button2_Click`
  - „Pronađi“ — klik → `button1_Click`

#### frmEvidencijaRadaKragujevac [S]
- **Fajl:** `Dokumenta/frmEvidencijaRadaKragujevac.cs` — „Evidencija rada Kragujevac“
- **Stari izgled:** Evidencije › Evidencija aktivnosti › Pretraga smena → **frmEvidencijaRadaZaglavlje** → „Otvori zapis“ → **frmEvidencijaRadaKragujevac**
- **Tabele:** `Aktivnosti` SIU p, `AktivnostiStavke` S, `Delavci` S, `VrstaAktivnosti` S
- **Procedure:** `InsertAktivnosti`, `UpdateAktivnosti`, `UpdateAktivnostiPoslatMail`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertAktivnosti, UpdateAktivnosti; preko: InsertAktivnosti.InsAktivnosti, InsertAktivnosti.UpdAktivnosti
  - „Pošalji mail“ — klik → `btnPosaljiMail_Click` — čita: Aktivnosti, AktivnostiStavke, Delavci, VrstaAktivnosti; proc: UpdateAktivnostiPoslatMail; preko: InsertAktivnosti.UpdatePoslaoMailAktivnosti
  - „Unesi po satu“ — klik → `btnUbaciAktivnost_Click` — proc: InsertAktivnostiStavke; preko: InsertAktivnostiStavke.InsAktivnostiStavke
  - `this` — otvaranje forme → `frmEvidencijaRada_Load` — čita: Delavci, VrstaAktivnosti

#### frmEvidencijaRAdaNeplaceno [S]
- **Fajl:** `Dokumenta/frmEvidencijaRAdaNeplaceno.cs` — „Neplaćeno “
- **Stari izgled:** Kadrovi › Putni nalozi › Neplaćeno sređivanje → **frmEvidencijaRAdaNeplaceno**
- **Otvara:** frmEvidencijaRada („Otvori zapis“)
- **Tabele:** `Aktivnosti` S, `AktivnostiDokumenta` S, `Delavci` S
- **Procedure:** `UpdateAktivnostiPlaceno`, `UpdateAktivnostiPlacenoRacuni`
- **Funkcionalnosti:**
  - „Pretraži sve neplaćeno“ — klik → `btnPretrazi_Click` — čita: Aktivnosti, AktivnostiDokumenta, Delavci
  - „Pretraži radnik“ — klik → `button1_Click` — čita: Aktivnosti, AktivnostiDokumenta, Delavci
  - „Plati selektovane stavke troškovi“ — klik → `button4_Click` — proc: UpdateAktivnostiPlaceno; preko: InsertAktivnosti.UpdateAktivnostiPlaceno
  - „Pregled kartica izvršen“ — klik → `button8_Click`
  - „Pregled računa izvršen“ — klik → `button6_Click` — proc: UpdateAktivnostiPlacenoRacuni; preko: InsertAktivnosti.UpdateAktivnostiPlacenoRacuni
  - „Otvori zapis“ — klik → `button3_Click` — otvara: frmEvidencijaRada
  - `this` — otvaranje forme → `frmEvidencijaRAdaNeplaceno_Load` — čita: Delavci

#### frmEvidencijaRadaPromene [S]
- **Fajl:** `Dokumenta/frmEvidencijaRadaPromene.cs` — „Promene na troškovima“
- **Stari izgled:** Evidencije › Evidencija aktivnosti › LOG promene troškova → **frmEvidencijaRadaPromene**
- **Tabele:** `Aktivnosti` S, `Delavci` S, `LogAktivnosti` S
- **Funkcionalnosti:**
  - „Pretraži“ — klik → `btnPretrazi_Click` — čita: Aktivnosti, Delavci, LogAktivnosti
  - „Pretraži“ — klik → `button2_Click` — čita: Aktivnosti, Delavci, LogAktivnosti
  - `this` — otvaranje forme → `frmEvidencijaRadaPromene_Load` — čita: Delavci

#### frmEvidencijaRadaRemont [S]
- **Fajl:** `Dokumenta/frmEvidencijaRadaRemont.cs` — „Evidencija rada remont“
- **Stari izgled:** Evidencije › Evidencija aktivnosti › Pretraga smena → **frmEvidencijaRadaZaglavlje** → „Otvori zapis“ → **frmEvidencijaRadaRemont**
- **Tabele:** `Aktivnosti` SIU p, `AktivnostiStavke` S, `Delavci` S, `VrstaAktivnosti` S
- **Procedure:** `InsertAktivnosti`, `UpdateAktivnosti`, `UpdateAktivnostiPoslatMail`
- **Funkcionalnosti:**
  - „Pošalji mail“ — klik → `btnPosaljiMail_Click` — čita: Aktivnosti, AktivnostiStavke, Delavci, VrstaAktivnosti; proc: UpdateAktivnostiPoslatMail; preko: InsertAktivnosti.UpdatePoslaoMailAktivnosti
  - „>>“ — klik → `btnUnesi_Click` — proc: InsertAktivnostiStavke; preko: InsertAktivnostiStavke.InsAktivnostiStavke
  - „Unesi po satu“ — klik → `btnUbaciAktivnost_Click` — proc: InsertAktivnostiStavke; preko: InsertAktivnostiStavke.InsAktivnostiStavke
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertAktivnosti, UpdateAktivnosti; preko: InsertAktivnosti.InsAktivnosti, InsertAktivnosti.UpdAktivnosti
  - „Novi“ — klik → `tsNew_Click`
  - `this` — otvaranje forme → `frmEvidencijaRada_Load` — čita: Delavci, VrstaAktivnosti

#### frmEvidencijaRadaSmederevo [S]
- **Fajl:** `Dokumenta/frmEvidencijaRadaSmederevo.cs` — „Evidencija rada Smederevo“
- **Stari izgled:** Evidencije › Evidencija aktivnosti › Pretraga smena → **frmEvidencijaRadaZaglavlje** → „Otvori zapis“ → **frmEvidencijaRadaSmederevo**
- **Tabele:** `Aktivnosti` SIU p, `AktivnostiStavke` S, `Delavci` S, `VrstaAktivnosti` S
- **Procedure:** `InsertAktivnosti`, `UpdateAktivnosti`, `UpdateAktivnostiPoslatMail`
- **Funkcionalnosti:**
  - „Pošalji mail“ — klik → `btnPosaljiMail_Click` — čita: Aktivnosti, AktivnostiStavke, Delavci, VrstaAktivnosti; proc: UpdateAktivnostiPoslatMail; preko: InsertAktivnosti.UpdatePoslaoMailAktivnosti
  - „>>“ — klik → `btnUnesi_Click` — proc: InsertAktivnostiStavke; preko: InsertAktivnostiStavke.InsAktivnostiStavke
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertAktivnosti, UpdateAktivnosti; preko: InsertAktivnosti.InsAktivnosti, InsertAktivnosti.UpdAktivnosti
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „Unesi po satu“ — klik → `btnUbaciAktivnost_Click` — proc: InsertAktivnostiStavke; preko: InsertAktivnostiStavke.InsAktivnostiStavke
  - `this` — otvaranje forme → `frmEvidencijaRada_Load` — čita: Delavci, VrstaAktivnosti

#### frmEvidencijaRadaZaglavlje [S]
- **Fajl:** `Dokumenta/frmEvidencijaRadaZaglavlje.cs` — „Evidencija rada po zaglavljima“
- **Stari izgled:** Evidencije › Evidencija aktivnosti › Pretraga smena → **frmEvidencijaRadaZaglavlje**
- **Otvara:** frmEvidencijaRadaKragujevac („Otvori zapis“), frmEvidencijaRadaSmederevo („Otvori zapis“), frmEvidencijaRadaCG („Otvori zapis“), frmEvidencijaRadaRemont („Otvori zapis“), frmEvidencijaRada („Otvori zapis“)
- **Tabele:** `Aktivnosti` SD p, `AktivnostiDokumenta` S, `AktivnostiStavke` S, `Delavci` S, `Kraji` S, `VrstaAktivnosti` S
- **Procedure:** `DeleteAktivnosti`, `DeleteAktivnostiID`, `DeleteAktivnostiStavkePoNadredjenom`, `InsertAktivnostiID`, `InsertAktivnostiZaglavljeID`, `UpdateAktivnostiPlacenoRacuni`, `UpdateAktivnostiPregledano`, `UpdateMasinovodja`
- **Funkcionalnosti:**
  - „Izbriši selektovane stavke“ — klik → `button1_Click` — proc: DeleteAktivnosti, DeleteAktivnostiStavkePoNadredjenom; preko: InsertAktivnosti.DeleteAktivnosti, InsertAktivnostiStavke.DeleteAktivnostiStavkePoNadredjenom
  - „Pretraži“ — klik → `btnPretrazi_Click` — čita: Aktivnosti, AktivnostiDokumenta, Delavci, Kraji
  - „Pretraži“ — klik → `button2_Click` — čita: Aktivnosti, Delavci
  - „Otvori zapis“ — klik → `button3_Click` — otvara: frmEvidencijaRadaKragujevac, frmEvidencijaRadaSmederevo, frmEvidencijaRadaCG, frmEvidencijaRadaRemont, frmEvidencijaRada
  - „Refresh“ — klik → `btn_Click`
  - „Plati selektovane stavke troškovi“ — klik → `button4_Click`
  - „Update masinovodja“ — klik → `button5_Click` — proc: UpdateMasinovodja; preko: InsertAktivnosti.UpdateMasinovodja
  - „Pregledano računa izvršen“ — klik → `button6_Click` — proc: UpdateAktivnostiPlacenoRacuni; preko: InsertAktivnosti.UpdateAktivnostiPlacenoRacuni
  - „Izdračunaj“ — klik → `button7_Click`
  - „Pregledano kartice“ — klik → `button8_Click` — proc: UpdateAktivnostiPregledano; preko: InsertAktivnosti.UpdateAktivnostiPregledano
  - „Pretraži“ — klik → `button9_Click` — čita: Aktivnosti, AktivnostiDokumenta, AktivnostiStavke, Delavci, Kraji, VrstaAktivnosti
  - „Spoljni“ — klik → `button10_Click` — čita: Aktivnosti, AktivnostiDokumenta, Delavci, Kraji
  - „Saberi smenu“ — klik → `button11_Click` — proc: DeleteAktivnostiID, InsertAktivnostiID, InsertAktivnostiZaglavljeID; preko: InsertAktivnosti.DeleteID, InsertAktivnosti.InsAktivnotiZaglavlje, InsertAktivnosti.InsID
  - `this` — otvaranje forme → `frmEvidencijaRadaZaglavlje_Load` — čita: Delavci

#### frmFaktureDetalji [S]
- **Fajl:** `Drumski/frmFaktureDetalji.cs` — „Pregled detalja fakture“
- **Stari izgled:** Drumski › Finansije › Lista naloga → **frmPregledFaktura** → „Pregled i izmena podataka fakture“ → **frmFaktureDetalji**
- **Otvara:** DodavanjeSkeniranePrevoznice („Dodaj skeniranu prevoznicu“), DodavanjeSkeniraneFakture („Dodaj skeniranu fakturu“), frmPregledSkeniranihDokumenata („Pregled skeniranih dokumenata“)
- **Tabele:** `Delavci` S, `FakturaDrumski` S, `FakturaDrumskiStavka` SIUD p, `IzvozKonacna` S, `IzvozKonacnaZaglavlje` S, `Korisnici` S, `RadniNalogDrumski` S, `UvozKonacna` S, `UvozKonacnaZaglavlje` S, `Voz` S
- **Procedure:** `DeleteFakturaDrumskiStavka`, `InsertFakturaDrumskiStavka`, `UpdateFakturaDrumskiStavka`
- **Funkcionalnosti:**
  - „Pregled skeniranih dokumenata“ — klik → `button4_Click` — otvara: frmPregledSkeniranihDokumenata
  - „Dodaj skeniranu fakturu“ — klik → `button3_Click` — otvara: DodavanjeSkeniraneFakture
  - „Dodaj skeniranu prevoznicu“ — klik → `button1_Click` — otvara: DodavanjeSkeniranePrevoznice
  - `button21` — klik → `button21_Click` — proc: DeleteFakturaDrumskiStavka; preko: InsertFakture.DelStavkaFakture
  - `button2` — klik → `btnSnimi_Click` — čita: FakturaDrumski, FakturaDrumskiStavka; proc: InsertFakturaDrumskiStavka, UpdateFakturaDrumskiStavka; preko: InsertFakture.InsStavkeFakture, InsertFakture.UpdateFakturaDrumskiStavka

#### frmFinansije [S]
- **Fajl:** `Dokumenta/frmFinansije.cs` — „Troškovi za banku“
- **Stari izgled:** Kadrovi › Putni nalozi › Troškovi banaka → **frmFinansije**
- **Otvara:** frmEvidencijaRada („Otvori Zapis stavke nepregledane“)
- **Tabele:** `Aktivnosti` S, `AktivnostiDokumenta` S, `Delavci` S
- **Procedure:** `UpdateAktivnostiPlaceno`
- **Funkcionalnosti:**
  - „Štampaj“ — klik → `btnStampa_Click` — izveštaj: Troskovi.rdlc
  - „Plati sve stavke“ — klik → `button1_Click` — proc: UpdateAktivnostiPlaceno; preko: InsertAktivnosti.UpdateAktivnostiPlaceno
  - „Plaćene stavke“ — klik → `button2_Click`
  - „Plaćene nepregledane stavke“ — klik → `button3_Click`
  - „Plaćene Zaposleni“ — klik → `button4_Click`
  - „Štampaj“ — klik → `button5_Click`
  - „Plaćene Zaposleni“ — klik → `button6_Click`
  - „Otvori Zapis stavke nepregledane“ — klik → `button7_Click` — otvara: frmEvidencijaRada
  - `this` — otvaranje forme → `frmFinansije_Load` — čita: Delavci

#### frmFormaZaVozace [N+S]
- **Fajl:** `DrumskiApp/frmFormaZaVozace.cs` — „frmFormaZaVozace“
- **Novi izgled:** Drumski transport → **Drumski1** → „Cerada“ → **Cerade** → „NALOG ZA DRUMSKI TRANSPORT“ → **NalogZaDrumski** → „TRANSPORTNI NALOZI IZVOZ“ → **frmPregledNalogaDrumski** → „Vozac“ → **frmFormaZaVozace**
- **Stari izgled:** Drumski › Drumski › Lista naloga → **frmPregledNalogaDrumski** → „Vozac“ → **frmFormaZaVozace**
- **Tabele:** `KontejnerskiTerminali` S, `MestaUtovara` S, `RadniNalogDrumski` S, `TipKontenjera` S, `VrstaRobeADR` S
- **Procedure:** `UpdateKorakPotvrdeVozaca`
- **Funkcionalnosti:**
  - „POTVRDA“ — klik → `button2_Click` — proc: UpdateKorakPotvrdeVozaca; preko: InsertRadniNalogDrumski.UpdKorakPotvrdeVozaca
  - „DODAJ DOKUMENTACIJU“ — klik → `btnDodajDokumentaciju_Click`

#### frmFormiranjePlana [N+S]
- **Fajl:** `Uvoz/frmFormiranjePlana.cs` — „Formiranje plana utovara ubacivanje stavki“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Voz“ → **frmPrijemIOtpremaVozova4** → „Vozovi“ → **frmVoz** → „UVOZ - FORMIRANJE PLANA“ → **frmFormiranjePlana**
- **Stari izgled:** Uvoz › Uvoz obrada › Popunjavanje plana kontejnerima → **frmFormiranjePlana**
- **Otvara:** frmUvozKonacna („Otvori plan“)
- **Tabele:** `Brodovi` S, `Carinarnice` S, `DirigacijaKontejneraZa` S, `KontejnerskiTerminali` S, `MestaUtovara` S, `Partnerji` S, `Predefinisaneporuke` S, `Scenario` S, `SerijeKola` S, `TipKontenjera` S, `uvNacinPakovanja` S, `Uvoz` S, `UvozKonacna` S, `UvozKonacnaZaglavlje` S, `UvozNapomenePozicioniranja` S, `Voz` S, `VozSerijeKola` S, `VrstaCarinskogPostupka` S, `VrstaRobeADR` S, `VrstePostupakaUvoz` S
- **Procedure:** `PrenesiUPlanUtovaraSelektovano`, `VratiIzPlanaUtovaraSelektovano`
- **Combobox → tabela:** `cboPlanUtovara` ← `Partnerji`, `UvozKonacnaZaglavlje`, `Voz`
- **Funkcionalnosti:**
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - „button4“ — klik → `button4_Click`
  - „Otvori plan“ — klik → `button5_Click` — otvara: frmUvozKonacna
  - „Vrati iz plana u neraspoređene“ — klik → `button3_Click` — proc: VratiIzPlanaUtovaraSelektovano; preko: InsertUvozKonacna.VratiIzPlanaUtovara
  - „Ukupan broj mesta voz:“ — klik → `label29_Click`
  - „Refresh“ — klik → `button2_Click`
  - „Prenesi selektovan“ — klik → `button1_Click` — proc: PrenesiUPlanUtovaraSelektovano; preko: InsertUvozKonacna.PrenesiUPlanUtovara

#### frmFormiranjePlanaIzvoz [N+S]
- **Fajl:** `Izvoz/frmFormiranjePlanaIzvoz.cs` — „Formiranje plana izvoz - popunjavanje kontejnerima“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Dodaj izvoznom vozu iz uvoza“ → **frmFormiranjePlanaIzvoz**
- **Stari izgled:** Izvoz › Izvoz › Popunjavanje plana kontejnerima → **frmFormiranjePlanaIzvoz**
- **Otvara:** frmIzvozKonacna („Otvori plan“)
- **Tabele:** `Carinarnice` S, `InspekciskiTretman` S, `Izvoz` SIU, `IzvozKonacna` SU, `IzvozKonacnaZaglavlje` S, `IzvozNHM` S, `IzvozVrstaManipulacije` S, `IzvozVrstaRobeHS` S, `KontejnerskiTerminali` S, `KontejnerStatus` S, `KrajnjaDestinacija` S, `MestaUtovara` S, `NHM` S, `organizacioneJedinice` S, `Partnerji` S, `RadniNalogInterni` S, `Scenario` S, `SerijeKola` S, `TipKontenjera` S, `uvNacinPakovanja` S, `UvozKonacna` S, `UvozKonacnaNapomenePozicioniranja` S, `UvozKonacnaVrstaManipulacije` S, `UvozKonacnaZaglavlje` S, `Voz` S, `VozSerijeKola` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaRobeHS` S, `VrstePostupakaUvoz` S
- **Procedure:** `InsertIzvozVMIzUvoza`, `PrenesiUPlanUtovaraSelektovanoIzvoz`, `spPrenesiIzUvozaUIzvoz`, `VratiKontejnerNerasporedjenoSelektovanoIzvoz`
- **Combobox → tabela:** `cboPlanUtovara` ← `IzvozKonacnaZaglavlje`, `Voz`
- **Funkcionalnosti:**
  - „Refresh“ — klik → `button2_Click`
  - „Prenesi selektovano“ — klik → `button1_Click` — piše: IzvozKonacna[U], Izvoz[U]; proc: InsertIzvozVMIzUvoza, PrenesiUPlanUtovaraSelektovanoIzvoz, spPrenesiIzUvozaUIzvoz; preko: InsertIzvoz.InsUbaciUsluguPlan, InsertIzvozKonacna.PrenesiUPlanUtovaraIzvoz, InsertIzvozKonacna.UpdejtujPodatkeIzUvoza
  - „Otvori plan“ — klik → `button4_Click` — otvara: frmIzvozKonacna
  - „Vrati selektovano“ — klik → `button3_Click` — proc: VratiKontejnerNerasporedjenoSelektovanoIzvoz; preko: InsertIzvozKonacna.VratiUNerasporedjene
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`

#### frmFormiranjeRobnihDokumenata [S]
- **Fajl:** `Uvoz/frmFormiranjeRobnihDokumenata.cs` — „Formiranje robnih dokumenta“
- **Stari izgled:** Terminal › Terminalske usluge › Formiranje robnih dokumenata - N → **frmFormiranjeRobnihDokumenata**
- **Otvara:** frmPrijemVozaIzPlana („Formiranje prijemnice voza na osnovu plana Uvoza“)
- **Funkcionalnosti:**
  - „Formiranje prijemnice voza na osnovu plana Uvoza“ — klik → `button1_Click` — otvara: frmPrijemVozaIzPlana

#### frmForrmiranjePlanaDrumski [S]
- **Fajl:** `Uvoz/frmForrmiranjePlanaDrumski.cs` — „Drumski prevoz formiranje plana“
- **Stari izgled:** Uvoz › Uvoz obrada › Popunjavanje plana DRUMSKI → **frmForrmiranjePlanaDrumski**
- **Tabele:** `Brodovi` S, `Carinarnice` S, `NHM` S, `Partnerji` S, `Predefinisaneporuke` S, `RadniNalogInterni` I p, `TipKontenjera` S, `uvNacinPakovanja` S, `Uvoz` S, `UvozKonacna` S, `UvozKonacnaNHM` S, `UvozKonacnaVrstaRobeHS` S, `UvozKonacnaZaglavlje` S, `VrstaCarinskogPostupka` S, `VrstaRobeADR` S, `VrstaRobeHS` S, `VrstePostupakaUvoz` S
- **Procedure:** `InsertRadniNalogInterni`
- **Funkcionalnosti:**
  - „Prenesi selektovano“ — klik → `button1_Click` — proc: PrenesiUPlanUtovaraSelektovano; preko: InsertUvozKonacna.PrenesiUPlanUtovara
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertUvozKonacnaZaglavlje, UpdateUvozKonacnaZaglavlje; preko: InsertUvozKonacnaZaglavlje.InsUvozKonacnaZaglavlje, InsertUvozKonacnaZaglavlje.UpdUvozKonacnaZaglavlje
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „Napravi nalog“ — klik → `toolStripButton1_Click` — proc: InsertRadniNalogInterni; preko: InsertRadniNalogInterni.InsRadniNalogInterni

#### frmFotografijePregledac [N]
- **Fajl:** `MainLeget/PrijemIOtpremaKamiona/frmFotografijePregledac.cs` — „frmFotografijePregledac“
- **Novi izgled:** Prijem i otprema kamiona → **frmPrijemIOtpremaKamiona1** → „PREGLEDAČ“ → **Pregledac** → „Fotografije“ → **frmFotografijePregledac**
- **Tabele:** `DokumentaPregledac` S
- **Funkcionalnosti:**
  - „Sačuvaj“ — klik → `btnSacuvajTovarniList_Click`
  - „Pronađi“ — klik → `btnTovarniList_Click`
  - „Pronađi“ — klik → `button3_Click`
  - „Otvori“ — klik → `button2_Click`
  - „Pronađi“ — klik → `button1_Click`

#### frmGenerisanjeTestaKorisnik [S]
- **Fajl:** `Testiranje/frmGenerisanjeTestaKorisnik.cs` — „Generisanje testa “
- **Stari izgled:** Kadrovi › Testiranje radnika › Generisanje testa → **frmGenerisanjeTestaKorisnik**
- **Tabele:** `Delavci` S, `DelovnaMesta` S, `Korisnici` S, `testovi` S, `TestoviGenerisanjaPitanja` S, `testovipitanja` S
- **Funkcionalnosti:**
  - „Generiši Test“ — klik → `toolStripButton1_Click`
  - „Izbaci selektovana pitanja“ — klik → `btnIzbaciTrasa_Click`
  - `this` — otvaranje forme → `frmGenerisanjeTestaKorisnik_Load` — čita: Testovi

#### frmGreske [S]
- **Fajl:** `TrackModal/Sifarnici/frmGreske.cs` — „OŠTEĆENJE KONTEJNERA“
- **Stari izgled:** Terminal › Šifarnici › Oštećenja → **frmGreske**
- **Tabele:** `Greske` SIUD p
- **Procedure:** `DeleteGreske`, `InsertGreske`, `UpdateGreske`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertGreske, UpdateGreske; preko: InsertGreske.InsGreske, InsertGreske.UpdGreske
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteGreske; preko: InsertGreske.DeleteGreske
  - „toolStripButton1“ — klik → `tsPrvi_Click` — čita: Greske
  - „toolStripButton1“ — klik → `tsNazad_Click` — čita: Greske
  - „toolStripButton1“ — klik → `tsNapred_Click` — čita: Greske
  - „toolStripButton1“ — klik → `tsPoslednja_Click` — čita: Greske
  - `button20` — klik → `tsDelete_Click` — proc: DeleteGreske; preko: InsertGreske.DeleteGreske
  - `button21` — klik → `tsSave_Click` — proc: InsertGreske, UpdateGreske; preko: InsertGreske.InsGreske, InsertGreske.UpdGreske

#### frmGrupaKvarova [S]
- **Fajl:** `Servis/frmGrupaKvarova.cs` — „Grupe kvarova“
- **Stari izgled:** Održavanje › Lokomotive osnovno › Grupe kvarova → **frmGrupaKvarova**
- **Tabele:** `GrupaKvarova` SIU p
- **Procedure:** `InsertGrupaKvarova`, `UpdateGrupaKvarova`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertGrupaKvarova, UpdateGrupaKvarova; preko: InsertGrupaKvarova.InsGRKVAR, InsertGrupaKvarova.UpdGRKvarova
  - „toolStripButton1“ — klik → `tsDelete_Click`

#### frmGrupaKvarovaAuto [S]
- **Fajl:** `Servis/frmGrupaKvarovaAuto.cs` — „Grupa kvarova automobili“
- **Stari izgled:** Održavanje › Automobili › Grupe kvarova → **frmGrupaKvarovaAuto**
- **Tabele:** `GrupaKvarovaAuto` SIUD p
- **Procedure:** `DeleteGrupaKvarovaAuto`, `InsertGrupaKvarovaAuto`, `UpdateGrupaKvarovaAuto`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertGrupaKvarovaAuto, UpdateGrupaKvarovaAuto; preko: InsertGrupaKvarova.InsGRKVARAuto, InsertGrupaKvarova.UpdGRKvarovaAuto
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteGrupaKvarovaAuto; preko: InsertGrupaKvarova.DeleteGRKvarovaAuto

#### frmGrupaNepravilnosti [S]
- **Fajl:** `Neispravnosti/frmGrupaNepravilnosti.cs` — „Grupa nepravilnosti“
- **Stari izgled:** Evidencije › Vrste aktivnosti › Sklop kola → **frmGrupaNepravilnosti**
- **Tabele:** `SifGrupaNepravilnosti` SIUD p
- **Procedure:** `DeleteSifGrupaNepravilnosti`, `InsertSifGrupaNepravilnosti`, `UpdateSifGrupaNepravilnosti`
- **Funkcionalnosti:**
  - „Kod“ — klik → `label2_Click`
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertSifGrupaNepravilnosti, UpdateSifGrupaNepravilnosti; preko: InsertNepravilnosti.InsSifGrupaNepravilnosti, InsertNepravilnosti.UpdSifGrupaNepravilnosti
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteSifGrupaNepravilnosti; preko: InsertNepravilnosti.DelSifGrupaNepravilnosti
  - „Opis“ — klik → `label3_Click`

#### frmGrupaVrsteManipulacije [S]
- **Fajl:** `TrackModal/Sifarnici/frmGrupaVrsteManipulacije.cs` — „Grupe usluga“
- **Stari izgled:** Terminal › Šifarnici › Grupe usluga → **frmGrupaVrsteManipulacije**
- **Tabele:** `GrupaVrsteManipulacije` SIUD p
- **Procedure:** `DeleteGrupaVrsteManipulacije`, `InsertGrupaVrsteManipulacije`, `UpdateGrupaVrsteManipulacije`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertGrupaVrsteManipulacije, UpdateGrupaVrsteManipulacije; preko: InsertGrupaVrsteManipulacije.InsGrupaVrsteManipulacije, InsertGrupaVrsteManipulacije.UpdGrupaVrsteManipulacije
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteGrupaVrsteManipulacije; preko: InsertGrupaVrsteManipulacije.DeleteGrupaVrsteManipulacije
  - `button20` — klik → `tsDelete_Click` — proc: DeleteGrupaVrsteManipulacije; preko: InsertGrupaVrsteManipulacije.DeleteGrupaVrsteManipulacije
  - `button21` — klik → `tsSave_Click` — proc: InsertGrupaVrsteManipulacije, UpdateGrupaVrsteManipulacije; preko: InsertGrupaVrsteManipulacije.InsGrupaVrsteManipulacije, InsertGrupaVrsteManipulacije.UpdGrupaVrsteManipulacije

#### frmGrupniUnosPoljaIzvoz [N]
- **Fajl:** `Izvoz/frmGrupniUnosPoljaIzvoz.cs` — „Nalog za otpremu“
- **Novi izgled:** Logistika izvoza → **LogistikaIzvoza1** → „Formiranje naloga za otpremu“ → **LogistikaIzvoza2** → „Kreiraj novi“ → **frmProdajniNalogIzvozTabela** → `gridGroupingControl1` (TableControlCellClick) → **frmGrupniUnosPoljaIzvoz**
- **Otvara:** frmCarinskiPostupak („Carinski postupak“), frmRelacija („Relacija“), frmIzvozUnosManipulacije („Dodaj usluge“), frmIzvozPregledKontejneraDrumskeUsluge („Pusti Drumski uslugu“)
- **Tabele:** `InspekciskiTretman` S, `Izvoz` S, `IzvozKonacna` S, `IzvozNapomenePozicioniranja` SI p, `IzvozNHM` S, `IzvozVrstaManipulacije` S, `KontejnerLog` I p, `KontejnerskiTerminali` S, `MestaUtovara` S, `NapomenaZaPozicioniranje` S, `NHM` S, `Partnerji` S, `ProdajniNalogIzvoz` S, `ProdajniNalogIzvozStavke` S, `Scenario` S, `TipKontenjera` S, `uvKvalitetKontejnera` S, `uvNacinPakovanja` S, `VrstaManipulacije` S, `VrstaRobeADR` S
- **Procedure:** `DeleteIzvozVrstaManTerminalskih`, `InsertIzvozNapomenePozicioniranja`, `InsertIzvozPorudzbenica`, `InsertKontejnerLog`, `UpdateIzvozPorudzbenica`, `UpdateIzvozPorudzbenicaPojedinacni`
- **Combobox → tabela:** `cboIzvoznik` ← `Partnerji`; `cboInspekciskiTretman` ← `InspekciskiTretman`; `cboADR` ← `VrstaRobeADR`; `cboKvalitetKontejnera` ← `uvKvalitetKontejnera`; `cboVrstaKontejnera` ← `TipKontenjera`; `cboVrstaRobe` ← `NHM`; `cboNacinPakovanja` ← `uvNacinPakovanja`; `cbNapomenaPoz` ← `NapomenaZaPozicioniranje`
- **Funkcionalnosti:**
  - „Formiraj KN II Faza“ — klik → `button4_Click`
  - „Formiraj KN I Faza“ — klik → `button2_Click` — proc: PrenesiUPlanUtovaraSelektovanoIzvoz; preko: InsertIzvozKonacna.PrenesiUPlanUtovaraIzvoz
  - „Dodaj usluge“ — klik → `btnUsluge_Click` — otvara: frmIzvozUnosManipulacije; čita: IzvozVrstaManipulacije; proc: InsertIzvozVrstaManipulacije; preko: InsertIzvoz.InsUbaciUslugu
  - „Carinski postupak“ — klik → `button1_Click` — otvara: frmCarinskiPostupak
  - „Relacija“ — klik → `button27_Click` — otvara: frmRelacija
  - `btnSave` — klik → `btnSave_Click` — proc: InsertIzvozNHM, InsertIzvozNapomenePozicioniranja, InsertIzvozPorudzbenica, UpdateIzvozPorudzbenica; preko: InsertIzvoz.InsIzvozNHM, InsertIzvoz.InsIzvozPorudzbenica, InsertIzvoz.UpdateIzvozPorudzbenica, InsertIzvozKonacna.InsIzvozNapomenePozicioniranja
  - „Unesi“ — klik → `btnUnesiNHM_Click`
  - „Unesi“ — klik → `button3_Click`
  - „Pusti Drumski uslugu“ — klik → `button5_Click` — otvara: frmIzvozPregledKontejneraDrumskeUsluge

#### frmHRDan [N]
- **Fajl:** `HR/frmHRDan.cs`
- **Novi izgled:** HR modul → **frmHRMeni** → pri otvaranju forme → **frmHREvidencijaRada** → metoda `OtvoriDan` → **frmHRDan**
- **Tabele:** `BeleskaORadu` S, `EvidencijaRada` S, `VrstaRada` S, `Zaposleni` S

#### frmHREvidencijaRada [N]
- **Fajl:** `HR/frmHREvidencijaRada.cs`
- **Novi izgled:** HR modul → **frmHRMeni** → pri otvaranju forme → **frmHREvidencijaRada**
- **Otvara:** frmHRDan (metoda `OtvoriDan`), frmHRGrupniUnos (metoda `GrupniUnos`)
- **Tabele:** `BeleskaORadu` S, `EvidencijaMesec` S, `EvidencijaRada` S, `fn_Podredjeni` S, `Korisnici` S, `MainNovi` S, `mainNoviKartice` S, `MainNoviPrava` S, `Praznici` S, `RasporedRada` S, `VrstaRada` S, `vw_EvidencijaZaObracun` S, `vZaposleniTrenutno` S, `Zaposleni` S

#### frmHRGOPravo [N]
- **Fajl:** `HR/frmHRGOPravo.cs`
- **Novi izgled:** HR modul → **frmHRMeni** → pri otvaranju forme → **frmHRGOPravo**
- **Otvara:** frmHRGOZahtevi (pri otvaranju forme), frmHRStavka (metoda `Generisi`)
- **Tabele:** `GODopust` S, `GODopustOsnov` S, `GODopustStavke` S, `OsnovGO` S, `vZaposleniTrenutno` S, `Zaposleni` S

#### frmHRGOZahtevi [N]
- **Fajl:** `HR/frmHRGOZahtevi.cs`
- **Novi izgled:** HR modul → **frmHRMeni** → pri otvaranju forme → **frmHRGOZahtevi**
- **Tabele:** `fn_Podredjeni` S, `GODopust` S, `GODopustStavke` SU, `Korisnici` S, `MainNovi` S, `mainNoviKartice` S, `MainNoviPrava` S, `StatusZahteva` S, `Zaposleni` S
- **Procedure:** `sp_GO_Otkazi`, `sp_GO_PodnesiZahtev`

#### frmHRGrupniUnos [N]
- **Fajl:** `HR/frmHRGrupniUnos.cs`
- **Novi izgled:** HR modul → **frmHRMeni** → pri otvaranju forme → **frmHREvidencijaRada** → metoda `GrupniUnos` → **frmHRGrupniUnos**

#### frmHRKontrolnaTabla [N]
- **Fajl:** `HR/frmHRKontrolnaTabla.cs`
- **Novi izgled:** HR modul → **frmHRMeni** → kartica „Kontrolna tabla rokova“ → **frmHRKontrolnaTabla**
- **Otvara:** frmHRZaposleni (pri otvaranju forme)
- **Tabele:** `Korisnici` S, `vZaposleniTrenutno` S, `Zaposleni` S

#### frmHRLog [N]
- **Fajl:** `HR/frmHRLog.cs`
- **Novi izgled:** HR modul → **frmHRMeni** → kartica „HR log“ → **frmHRLog**
- **Tabele:** `HRLog` S

#### frmHRMeni [N]
- **Fajl:** `HR/frmHRMeni.cs`
- **Novi izgled:** HR modul → **frmHRMeni**
- **Otvara:** frmHRZaposleniLista (kartica „Zaposleni“), frmHREvidencijaRada (pri otvaranju forme), frmHRGOZahtevi (pri otvaranju forme), frmHRGOPravo (pri otvaranju forme), frmHRKontrolnaTabla (kartica „Kontrolna tabla rokova“), frmHRZahteviLista (kartica „Zahtevi za nabavku“), frmHRSifarnici (kartica „Pravila odobravanja“), frmHRLog (kartica „HR log“)
- **Tabele:** `GODopust` S, `GODopustStavke` S, `Korisnici` S, `Zaposleni` S

#### frmHRRevers [N]
- **Fajl:** `HR/frmHRRevers.cs`
- **Novi izgled:** HR modul → **frmHRMeni** → kartica „Zaposleni“ → **frmHRZaposleniLista** → pri otvaranju forme → **frmHRZaposleni** → metoda `PodesiDodatneKomande` → **frmHRRevers**
- **Tabele:** `HTZGrupe` S, `HTZOprema` S, `HTZReversi` SU, `Korisnici` S, `vZaposleniTrenutno` S, `Zaposleni` S, `ZaposleniHTZZaduzenja` S

#### frmHRSifarnici [N]
- **Fajl:** `HR/frmHRSifarnici.cs`
- **Novi izgled:** HR modul → **frmHRMeni** → kartica „Pravila odobravanja“ → **frmHRSifarnici**
- **Otvara:** frmHRStavka (metoda `PredloziPraznike`)
- **Tabele:** `HTZOprema` S, `organizacioneJedinice` S, `OsnovGO` S, `Praznici` S, `RadnaMesta` S, `RadnoMestoNormativHTZ` S, `RadnoMestoObavezniDokumenti` S, `RadnoMestoObavezniPregledi` S, `RasporedRada` S, `VrstaRada` S, `VrsteDokumenta` S, `VrstePregleda` S

#### frmHRStavka [N]
- **Fajl:** `HR/frmHRStavka.cs`
- **Novi izgled:** HR modul → **frmHRMeni** → pri otvaranju forme → **frmHRGOPravo** → metoda `Generisi` → **frmHRStavka**

#### frmHRZahtev [N]
- **Fajl:** `HR/frmHRZahtev.cs`
- **Novi izgled:** HR modul → **frmHRMeni** → kartica „Zahtevi za nabavku“ → **frmHRZahteviLista** → pri otvaranju forme → **frmHRZahtev**
- **Otvara:** frmHRStavka (metoda `Prijem`)
- **Tabele:** `fnHR_ZahtevPlanOdobravanja` S, `HTZOprema` S, `Korisnici` S, `MainNovi` S, `mainNoviKartice` S, `MainNoviPrava` S, `organizacioneJedinice` S, `Partnerji` S, `PrioritetiNabavke` S, `Stanice` S, `StatusiStavkiNabavke` S, `StatusiZahtevaZaNabavku` S, `VrsteNabavke` S, `vZaposleniTrenutno` S, `ZahteviZaNabavku` S, `ZahtevZaNabavkuIstorijaStatusa` S, `ZahtevZaNabavkuOdobrenja` S, `ZahtevZaNabavkuPonude` SD, `ZahtevZaNabavkuPrilozi` SD, `ZahtevZaNabavkuStavke` SUD, `Zaposleni` S
- **Procedure:** `HR_ZahtevPreracunaj`
- **Funkcionalnosti:**
  - `podaci` — Promena → `PodaciPromena` — čita: PrioritetiNabavke, VrsteNabavke, vZaposleniTrenutno
  - `stavke` — Promenjeno → `Ucitaj` — čita 10 tabela; preko: HRPrava.TrenutniZaposleni

#### frmHRZahteviLista [N]
- **Fajl:** `HR/frmHRZahteviLista.cs`
- **Novi izgled:** HR modul → **frmHRMeni** → kartica „Zahtevi za nabavku“ → **frmHRZahteviLista**
- **Otvara:** frmHRZahtev (pri otvaranju forme)
- **Tabele:** `fnHR_ZahtevPlanOdobravanja` S, `Korisnici` S, `StatusiZahtevaZaNabavku` S, `VrsteNabavke` S, `vZaposleniTrenutno` S, `ZahteviZaNabavku` S, `ZahtevZaNabavkuOdobrenja` S, `ZahtevZaNabavkuStavke` S, `Zaposleni` S

#### frmHRZaposleni [N]
- **Fajl:** `HR/frmHRZaposleni.cs`
- **Novi izgled:** HR modul → **frmHRMeni** → kartica „Zaposleni“ → **frmHRZaposleniLista** → pri otvaranju forme → **frmHRZaposleni**
- **Otvara:** frmHRGOZahtevi (metoda `PodesiDodatneKomande`), frmHRRevers (metoda `PodesiDodatneKomande`), frmHRStavka (metoda `Razduzi`)
- **Tabele:** `BeleskaORadu` S, `GODopust` S, `GODopustStavke` S, `GrupeDokumenata` S, `HTZOprema` S, `HTZReversi` S, `Korisnici` S, `MainNovi` S, `mainNoviKartice` S, `MainNoviPrava` S, `OceneSposobnosti` S, `organizacioneJedinice` S, `OsnovGO` S, `RadnaMesta` S, `RadniOdnosi` S, `RasporedRada` S, `RazloziRazduzenja` S, `Srodstva` S, `Stanice` S, `StatusZahteva` S, `StepenObrazovanja` S, `TezinePovreda` S, `VrsteDisciplinskihMera` S, `VrsteDokumenta` S, `VrstePregleda` S, `VrsteUgovora` S, `vZaposleniRokovi` S, `vZaposleniTrenutno` S, `vZaposleniUpozorenja` S, `Zaposleni` SIU, `ZaposleniClanoviPorodice` S, `ZaposleniDisciplinske` S, `ZaposleniDokumenti` S, `ZaposleniHitniKontakti` S, `ZaposleniHTZZaduzenja` S, `ZaposleniMentorBeleske` S, `ZaposleniMentori` S, `ZaposleniOcene` S, `ZaposleniOsnovGO` S, `ZaposleniPovrede` S, `ZaposleniPrilozi` SD, `ZaposleniRadnaMesta` S, `ZaposleniSkolovanje` S, `ZaposleniSlike` SD, `ZaposleniStatusi` S, `ZaposleniUgovori` S, `ZaposleniZdravstveniPregledi` S
- **Funkcionalnosti:**
  - `tUgovori` — Promenjeno → `OsveziZaglavlje` — čita: Zaposleni, vZaposleniTrenutno, vZaposleniUpozorenja
  - `t` — Promenjeno → `OsveziZaglavlje` — čita: Zaposleni, vZaposleniTrenutno, vZaposleniUpozorenja

#### frmHRZaposleniLista [N]
- **Fajl:** `HR/frmHRZaposleniLista.cs`
- **Novi izgled:** HR modul → **frmHRMeni** → kartica „Zaposleni“ → **frmHRZaposleniLista**
- **Otvara:** frmHRZaposleni (pri otvaranju forme)
- **Tabele:** `vZaposleniTrenutno` S, `vZaposleniUpozorenja` S

#### frmInspekciskiTretman [S]
- **Fajl:** `Izvoz/frmInspekciskiTretman.cs` — „Inspekciski tretman“
- **Stari izgled:** Terminal › Šifarnici › Inspekciski tretman → **frmInspekciskiTretman**
- **Tabele:** `InspekciskiTretman` SIUD p
- **Procedure:** `DeleteInspekciskiTretman`, `InsertInspekciskiTretman`, `UpdateInspekciskiTretman`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertInspekciskiTretman, UpdateInspekciskiTretman; preko: InsertInspekciskiTretman.InsInspekciskiTretman, InsertInspekciskiTretman.UpdInspekciskiTretman
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteInspekciskiTretman; preko: InsertInspekciskiTretman.DelInspekciskiTretman
  - `button20` — klik → `tsDelete_Click` — proc: DeleteInspekciskiTretman; preko: InsertInspekciskiTretman.DelInspekciskiTretman
  - `button21` — klik → `tsSave_Click` — proc: InsertInspekciskiTretman, UpdateInspekciskiTretman; preko: InsertInspekciskiTretman.InsInspekciskiTretman, InsertInspekciskiTretman.UpdInspekciskiTretman

#### frmIskljuceniVagoni [S]
- **Fajl:** `Dokumenta/frmIskljuceniVagoni.cs` — „Isključeni vagoni“
- **Stari izgled:** Komercijala › Teretnice › Isključeni vagoni → **frmIskljuceniVagoni**
- **Tabele:** `Stanice` S, `TeretnicaIskljuceniVagoni` S, `TeretnicaStavke` S
- **Funkcionalnosti:**
  - `this` — otvaranje forme → `frmIskljuceniVagoni_Load` — čita: TeretnicaIskljuceniVagoni, TeretnicaStavke, stanice

#### frmIzracunZarada [S]
- **Fajl:** `Dokumenta/frmIzracunZarada.cs` — „Izraćun Zarada“
- **Stari izgled:** Kadrovi › Zarade › Obračun zarade → **frmIzracunZarada**
- **Tabele:** `ObracunZaposleni` S
- **Procedure:** `DeleteObracunZarade`, `SelectObracunMMV`, `SelectObracunMMVIno`, `SelectObracunMMVKragujevac`, `SelectObracunMMVLokomotiva`, `SelectObracunMMVMilsped`, `SelectObracunMMVRemont`, `SelectObracunMMVSmederevo`, `SelectObracunMMVUkupno1`, `SelectPNBrisanje`, `SelectPNBrisanje2`, `SelectPNBrisanje3`, `SelectPNBrisanje4`, `SelectPNBrisanje5`, `SelectPutniNalozi`, `SelectPutniNaloziPola`, `UpdateKazne`, `UpdateMinimalac`, `UpdateObracunZarade`, `UpdatePnZapisa`, `UpdateUkupnoUEUR`, `UpdateUkupnoUSmanjenje`, `UpdateZakljucavanjeSmene`
- **Funkcionalnosti:**
  - „Izračunaj 1“ — klik → `btnIzracunaj_Click` — proc: DeleteObracunZarade, SelectObracunMMV, SelectObracunMMVIno, SelectObracunMMVKragujevac, SelectObracunMMVLokomotiva, SelectObracunMMVMilsped; preko: InsertObracunSati.DelObracun, InsertObracunSati.InsObracun, InsertObracunSati.UpdINO, InsertObracunSati.UpdKragujevac, InsertObracunSati.UpdLokomotiva
  - „?“ — klik → `button2_Click` — proc: UpdateUkupnoUEUR; preko: InsertObracunSati.UpdUkupnoUEUR
  - „? - 3“ — klik → `button3_Click` — proc: UpdateUkupnoUSmanjenje; preko: InsertObracunSati.UpdUkupnoSmanjenje
  - „Minimalac 2“ — klik → `button4_Click` — proc: UpdateMinimalac; preko: InsertObracunSati.UpdUkupnoMinimalac
  - „PN 1“ — klik → `button5_Click` — proc: SelectPutniNalozi, SelectPutniNaloziPola; preko: InsertObracunSati.UpdPutniNalozi, InsertObracunSati.UpdPutniNaloziPola
  - „PN - 2“ — klik → `button7_Click` — proc: SelectPNBrisanje, SelectPNBrisanje2, SelectPNBrisanje3, SelectPNBrisanje4, SelectPNBrisanje5; preko: InsertObracunSati.SelectPNBrisanje, InsertObracunSati.SelectPNBrisanje2, InsertObracunSati.SelectPNBrisanje3, InsertObracunSati.SelectPNBrisanje4, InsertObracunSati.SelectPNBrisanje5
  - „Brisanje viška PN - 3“ — klik → `button8_Click`
  - „Zaključavanje smena“ — klik → `button1_Click_2` — proc: UpdateZakljucavanjeSmene; preko: InsertObracunSati.UpdZakljucavanjeSmene
  - „Refresh“ — klik → `button9_Click_1`

#### frmIzvestajCarinskoObelezje [N]
- **Fajl:** `Skladista/frmIzvestajCarinskoObelezje.cs` — „Carinsko obeležje“
- **Novi izgled:** Operacije skladišta → **SkladistaMain** → „Carinsko skladište“ → **CarinskoSkladiste** → „Prijem robe“ → **CarinskoSkladistePregled** → „Štampaj“ → **frmIzvestajCarinskoObelezje**

#### frmIzvestajNalogZaPrevoz [S]
- **Fajl:** `Drumski/frmIzvestajNalogZaPrevoz.cs` — „frmIzvestajNalogZaPrevoz“
- **Stari izgled:** Drumski › Finansije › Lista naloga → **frmPregledFaktura** → „Nalog za obavljanje prevoza“ → **frmIzvestajNalogZaPrevoz**
- **Procedure:** `rpt_RNalogDrumskiObavljanjePrevoza`
- **Funkcionalnosti:**
  - `reportViewer1` — otvaranje forme → `reportViewer1_Load` — proc: rpt_RNalogDrumskiObavljanjePrevoza; izveštaj: Saobracaj.Drumski.rptNalogPrevoza.rdlc

#### frmIzvoz [S]
- **Fajl:** `Izvoz/frmIzvoz.cs` — „Nerasporedjeni - Obrazac“
- **Stari izgled:** Izvoz › Izvoz › Nerasporedjeni kontejneri Obrazac → **frmIzvoz**
- **Otvara:** frmIzvozDokumenta („Dokumentacija“), frmIzvozUnosManipulacije („Unos usluga“), frmKontaktOsobe (`button6` (Click)), frmKontaktOsobeMU (`button18` (Click)), frmIzvozTable („Tabela“), frmPrijemKamionaPlatforma („Formiraj prijem kam plat“), IzvozOpredeljenje („Kontejner na skladistu“), frmIzvozPregledKontejneraDrumskeUsluge („Drumske usluge“)
- **Tabele:** `Carinarnice` S, `Cene` S, `InspekciskiTretman` S, `Izvoz` SIU, `IzvozKonacnaVrstaManipulacije` I p, `IzvozNapomenePozicioniranja` S, `IzvozNHM` SID p, `IzvozVrstaManipulacije` S, `IzvozVrstaRobeHS` SID p, `KontejnerskiTerminali` S, `KrajnjaDestinacija` S, `MestaUtovara` S, `NHM` S, `organizacioneJedinice` S, `Partnerji` S, `partnerjiKontOseba` S, `partnerjiKontOsebaMU` S, `Predefinisaneporuke` S, `Scenario` S, `TipKontenjera` S, `uvNacinPakovanja` S, `UvozKonacna` S, `Valute` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaRobeADR` S, `VrstaRobeHS` S, `VrstePostupakaUvoz` S
- **Procedure:** `DeleteIzvozNHM`, `DeleteIzvozSve`, `DeleteIzvozVrstaRobeHS`, `InsertIzvozKonacnaVrstaManipulacije`, `InsertIzvozNHM`, `InsertIzvozVrstaRobeHS`, `PrenesiUPlanUtovaraSelektovanoIzvoz`, `PrenesiUPlanUtovaraSelektovanoIzvozBrodar`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click` — piše: Izvoz[I]
  - „tsSave“ — klik → `tsSave_Click` — proc: UpdateIzvoz; preko: InsertIzvoz.UpdIzvoz
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteIzvozSve; preko: InsertIzvoz.DelIzvozSve
  - „Dokumentacija  + SHD“ — klik → `toolStripButton6_Click` — otvara: frmIzvozDokumenta
  - „Tabela + SHT“ — klik → `toolStripButton1_Click` — otvara: frmIzvozTable
  - „Unos usluga + SHU“ — klik → `toolStripButton2_Click` — otvara: frmIzvozUnosManipulacije
  - „KONTEJNERI NA SKLADISTU“ — klik → `toolStripButton4_Click` — otvara: IzvozOpredeljenje; proc: UpdateIzvozStatus; preko: InsertIzvoz.IzvozOpredelio
  - „FORMIRAJ PRIJEM KAM PLAT“ — klik → `toolStripButton3_Click`
  - „SC116 - Konacna, Usluga, Plan“ — klik → `toolStripButton7_Click` — proc: InsertIzvozKonacnaVrstaManipulacije, PrenesiUPlanUtovaraSelektovanoIzvozBrodar; preko: InsertIzvoz.InsUbaciUsluguKonacna, InsertIzvozKonacna.PrenesiUPlanUtovaraIzvozBrodar
  - „POSTAVI U PLAN, IZDAJ RN - TERMINAL“ — klik → `toolStripButton5_Click` — proc: PrenesiUPlanUtovaraSelektovanoIzvoz; preko: InsertIzvozKonacna.PrenesiUPlanUtovaraIzvoz
  - „Delete“ — klik → `button2_Click` — proc: DeleteIzvozVrstaRobeHS; preko: InsertIzvoz.DelIzvozVrstaRobeHS
  - „Unesi“ — klik → `button4_Click` — proc: InsertIzvozVrstaRobeHS; preko: InsertIzvoz.InsIzvozVrstaRobeHS
  - „Izbriši“ — klik → `button3_Click` — proc: DeleteIzvozNHM; preko: InsertIzvoz.DelIzvozNHM
  - „Unesi“ — klik → `button1_Click` — proc: InsertIzvozNHM; preko: InsertIzvoz.InsIzvozNHM
  - „OPŠTI“ — klik → `button10_Click`
  - „NPP V V“ — klik → `button8_Click`
  - „Unos usluga“ — klik → `button14_Click` — otvara: frmIzvozUnosManipulacije
  - `button11` — klik → `button11_Click` — čita: NHM
  - `button15` — klik → `button15_Click` — čita: partnerjiKontOsebaMU
  - `button18` — klik → `button18_Click` — otvara: frmKontaktOsobeMU; čita: partnerjiKontOsebaMU
  - „Interni“ — čekiranje → `chkInterni_CheckedChanged`
  - „Unesi“ — klik → `button17_Click_1`
  - „Drumske usluge“ — klik → `button30_Click` — otvara: frmIzvozPregledKontejneraDrumskeUsluge
  - „Terminal - postavi plan, izdaj rn“ — klik → `toolStripButton5_Click` — proc: PrenesiUPlanUtovaraSelektovanoIzvoz; preko: InsertIzvozKonacna.PrenesiUPlanUtovaraIzvoz
  - „SC116 - Konacna, Usluga, Plan“ — klik → `toolStripButton7_Click` — proc: InsertIzvozKonacnaVrstaManipulacije, PrenesiUPlanUtovaraSelektovanoIzvozBrodar; preko: InsertIzvoz.InsUbaciUsluguKonacna, InsertIzvozKonacna.PrenesiUPlanUtovaraIzvozBrodar
  - „Formiraj prijem kam plat“ — klik → `toolStripButton3_Click`
  - „Kontejner na skladistu“ — klik → `toolStripButton4_Click` — otvara: IzvozOpredeljenje; proc: UpdateIzvozStatus; preko: InsertIzvoz.IzvozOpredelio
  - „Tabela“ — klik → `toolStripButton1_Click` — otvara: frmIzvozTable
  - „Usluge“ — klik → `toolStripButton2_Click` — otvara: frmIzvozUnosManipulacije
  - „Dokumentacija“ — klik → `toolStripButton6_Click` — otvara: frmIzvozDokumenta
  - `button20` — klik → `tsDelete_Click` — proc: DeleteIzvozSve; preko: InsertIzvoz.DelIzvozSve
  - `button21` — klik → `tsSave_Click` — proc: UpdateIzvoz; preko: InsertIzvoz.UpdIzvoz
  - `button22` — klik → `tsNew_Click` — piše: Izvoz[I]
  - `button7` — klik → `button7_Click_2` — čita: Partnerji, partnerjiKontOseba
  - „Opšti“ — čekiranje → `chkOpsti_CheckedChanged`
  - „Vrsta robe“ — klik → `label59_Click`
  - „Najava vozila i vozača“ — čekiranje → `chkNajavaVozila_CheckedChanged`
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`

#### frmIzvozDokumenta [N+S]
- **Fajl:** `Izvoz/frmIzvozDokumenta.cs` — „Izvoz dokumentacija“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Dodatne usluge“ → **frmDodatneUsluge** → „Dodeli slike“ → **frmIzvozDokumenta**
- **Stari izgled:** Izvoz › Izvoz › Nerasporedjeni kontejneri Obrazac → **frmIzvoz** → „Dokumentacija“ → **frmIzvozDokumenta**
- **Tabele:** `IzvozDokumenta` SID p, `TipKomercijalnogDokumenta` S
- **Procedure:** `DeleteIzvozDokumenta`, `InsertIzvozDokumenta`
- **Combobox → tabela:** `cboTipDokumenta1` ← `TipKomercijalnogDokumenta`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „Dokumentacija“ — klik → `toolStripButton1_Click`
  - „Izbriši“ — klik → `button3_Click` — proc: DeleteIzvozDokumenta; preko: InsertIzvozDokumenta.DelIzvozDokumenta
  - „Sačuvaj“ — klik → `button4_Click` — proc: InsertIzvozDokumenta; preko: InsertIzvozDokumenta.InsIzvozDokumenta
  - „Otvori“ — klik → `button2_Click`
  - „Pronađi“ — klik → `button1_Click`
  - „Izbriši“ — klik → `button5_Click` — proc: DeleteIzvozDokumenta; preko: InsertIzvozDokumenta.DelIzvozDokumenta
  - „Pronađi“ — klik → `button6_Click`
  - „Sačuvaj“ — klik → `button7_Click` — proc: InsertIzvozDokumenta; preko: InsertIzvozDokumenta.InsIzvozDokumenta
  - „Otvori“ — klik → `button8_Click`
  - „Izbriši“ — klik → `button9_Click` — proc: DeleteIzvozDokumenta; preko: InsertIzvozDokumenta.DelIzvozDokumenta
  - „Pronađi“ — klik → `button10_Click`
  - „Sačuvaj“ — klik → `button11_Click` — proc: InsertIzvozDokumenta; preko: InsertIzvozDokumenta.InsIzvozDokumenta
  - „Otvori“ — klik → `button12_Click`

#### frmIzvozIstorija [S]
- **Fajl:** `Izvoz/frmIzvozIstorija.cs` — „Pregled istoriskih podataka kroz planove“
- **Stari izgled:** Izvoz › Izvoz › Pregled izvoznih kontejnera istorija → **frmIzvozIstorija**
- **Tabele:** `Carinarnice` S, `InspekciskiTretman` S, `IzvozKonacna` S, `IzvozKonacnaVrstaManipulacije` S, `IzvozKonacnaZaglavlje` S, `KontejnerskiTerminali` S, `KontejnerStatus` S, `KrajnjaDestinacija` S, `MestaUtovara` S, `organizacioneJedinice` S, `Partnerji` S, `RadniNalogInterni` S, `Scenario` S, `TipKontenjera` S, `uvNacinPakovanja` S, `Voz` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstePostupakaUvoz` S
- **Funkcionalnosti:**
  - „Refresh istorija“ — klik → `button1_Click`
  - „Refresh radni nalozi“ — klik → `button23_Click`
  - „gridGroupingControl2“ — klik na ćeliju → `gridGroupingControl2_TableControlCellClick`

#### frmIzvozKonacna [N+S]
- **Fajl:** `Izvoz/frmIzvozKonacna.cs` — „Izvoz konačna“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Dodaj izvoznom vozu iz uvoza“ → **frmFormiranjePlanaIzvoz** → „Otvori plan“ → **frmIzvozKonacna**
- **Stari izgled:** Izvoz › Izvoz › Planovi utovara / pakovanja voza → **frmPlanoviIzvoza** → „Otvori“ → **frmIzvozKonacna**
- **Otvara:** frmIzvozUnosManipulacije („Unos usluga“), frmKontaktOsobe (`button16` (Click)), frmIzvozDokumenta („Dokumentacija“), frmFormiranjePlanaIzvoz („Dodaj nerasporedjene“), frmKontaktOsobeMU (`button22` (Click)), frmIzvozKonacnaTable („Tabela“), frmOtpremaVozaIzPlana („Otprema kontejnera Terminal“), frmPovezivanjeKontejneraIVagona („Pakovanje voza“), frmIzvozPregledKontejneraDrumskeUsluge („Drumske usluge“)
- **Tabele:** `Carinarnice` S, `Cene` S, `InspekciskiTretman` S, `IzvozKonacna` S, `IzvozKonacnaNapomenePozicioniranja` SID p, `IzvozKonacnaNHM` S, `IzvozKonacnaVrstaManipulacije` S, `IzvozKonacnaVrstaRobeHS` S, `IzvozKonacnaZaglavlje` SU p, `IzvozNHM` S, `IzvozVrstaManipulacije` S, `IzvozVrstaRobeHS` S, `KontejnerskiTerminali` S, `KontejnerStatus` S, `KrajnjaDestinacija` S, `MestaUtovara` S, `NHM` S, `organizacioneJedinice` S, `Partnerji` S, `partnerjiKontOseba` S, `partnerjiKontOsebaMU` S, `Predefinisaneporuke` S, `RadniNalogInterni` S, `Scenario` S, `TipKontenjera` S, `uvNacinPakovanja` S, `UvozKonacna` S, `UvozKonacnaVrstaManipulacije` S, `Valute` S, `Voz` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaRobeADR` S, `VrstaRobeHS` S, `VrstePostupakaUvoz` S
- **Procedure:** `DeleteIzvozKonacnaNapomenePozicioniranja`, `DeleteIzvozKonacnaSve`, `DeleteIzvozVrstaManTerminalskih`, `InsertIzvozKonacnaNapomenePozicioniranja`, `UpdateIzvozKonacnaZaglavlje`, `VratiKontejnerNerasporedjenoSelektovanoIzvoz`
- **Combobox → tabela:** `txtTipKont` ← `TipKontenjera`; `cboBrodar` ← `Partnerji`; `txtADR` ← `VrstaRobeADR`; `cboKrajnjaDestinacija` ← `KrajnjaDestinacija`; `cboPostupanjeSaRobom` ← `VrstePostupakaUvoz`; `cboPPCNT` ← `KontejnerskiTerminali`; `cboPPCNT2` ← `KontejnerskiTerminali`; `cboPPCNT3` ← `KontejnerskiTerminali`; `cboMestoUtovara` ← `MestaUtovara`; `cboCarina` ← `Carinarnice`; `cboSpedicija` ← `Partnerji`; `cboReexport` ← `VrstaCarinskogPostupka`; `cboInspekciskiTretman` ← `InspekciskiTretman`; `cbNacinPakovanja` ← `uvNacinPakovanja`; `cboIzvoznik` ← `Partnerji`; `cboNalogodavac1` ← `Partnerji`; `cboNalogodavac2` ← `Partnerji`; `cboNalogodavac3` ← `Partnerji`; `cboSpediterURijeci` ← `Partnerji`; `cboVoz` ← `Voz`; `cboNazivRobe` ← `VrstaRobeHS`; `cboNHM` ← `NHM`; `txtValuta` ← `Valute`; `cboVrstaPlombe` ← `Partnerji`; `cbNapomenaPoz` ← `Predefinisaneporuke`; `cboScenario` ← `Scenario`; `cboNHM` ← `NHM`; `cboNHM` ← `NHM`; `cboNHM` ← `NHM`; `cboNHM` ← `NHM`
- **Funkcionalnosti:**
  - „tsSave“ — klik → `tsSave_Click` — proc: UpdateIzvozKonacna; preko: InsertIzvoz.UpdIzvozKonacna
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteIzvozKonacnaSve; preko: InsertIzvozKonacna.DelIzvozKonacnaSve
  - „Dokumentacija-SHD“ — klik → `toolStripButton6_Click` — otvara: frmIzvozDokumenta
  - „Tabela - SH-T“ — klik → `toolStripButton5_Click` — otvara: frmIzvozKonacnaTable
  - „Usluge - SH - U“ — klik → `toolStripButton4_Click` — otvara: frmIzvozUnosManipulacije
  - „Izdavanje naloga“ — klik → `toolStripButton1_Click`
  - „Dodavanje nerasporedjenih u plan“ — klik → `toolStripButton2_Click` — otvara: frmFormiranjePlanaIzvoz
  - „Vrati kontejner u nerasporedjene“ — klik → `toolStripButton3_Click` — proc: VratiKontejnerNerasporedjenoSelektovanoIzvoz; preko: InsertIzvozKonacna.VratiUNerasporedjene
  - „Otprema kontejnera Terminal“ — klik → `toolStripButton8_Click` — otvara: frmOtpremaVozaIzPlana
  - „OPŠTI“ — klik → `button10_Click`
  - „V V V V V“ — klik → `button9_Click`
  - „NPP V V“ — klik → `button8_Click`
  - „Vaganje“ — čekiranje → `chkVaganje_CheckedChanged`
  - „Direktno “ — čekiranje → `chkNacinPretovara_CheckedChanged`
  - „Najava vozila/cnt i vozača“ — čekiranje → `chkNajavaVozila_CheckedChanged`
  - „Delete“ — klik → `button2_Click` — proc: DeleteIzvozKonacnaVrstaRobeHS; preko: InsertIzvoz.DelIzvozKonacnaVrstaRobeHS
  - „Unesi“ — klik → `button4_Click` — proc: InsertIzvozkonacnaVrstaRobeHS; preko: InsertIzvoz.InsIzvozKonacnaVrstaRobeHS
  - „Izbriši“ — klik → `button3_Click` — proc: DeleteIzvozKonacnaNHM; preko: InsertIzvoz.DelIzvozKonacnaNHM
  - „Unesi“ — klik → `button1_Click` — proc: InsertIzvozKonacnaNHM; preko: InsertIzvoz.InsIzvozKonacnaNHM
  - „Unos usluga“ — klik → `button14_Click` — otvara: frmIzvozUnosManipulacije
  - „Unesi“ — klik → `button11_Click` — čita: NHM
  - „Unesi“ — klik → `button15_Click` — čita: NHM
  - `button12` — klik → `button12_Click` — čita: partnerjiKontOsebaMU
  - „?“ — klik → `button17_Click`
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - „Izbriši“ — klik → `button20_Click` — proc: DeleteIzvozKonacnaNapomenePozicioniranja; preko: InsertIzvozKonacna.DelIzvozKonacnaNapomenePozicioniranja
  - „Unesi“ — klik → `button21_Click` — proc: InsertIzvozKonacnaNapomenePozicioniranja; preko: InsertIzvozKonacna.InsIzvozKonacnaNapomenePozicioniranja
  - „Indirektno“ — čekiranje → `chkIndirektno_CheckedChanged`
  - `button22` — klik → `button22_Click` — otvara: frmKontaktOsobeMU; čita: partnerjiKontOsebaMU
  - „Terminal“ — čekiranje → `checkBox1_CheckedChanged`
  - „Drumske usluge“ — klik → `button32_Click` — otvara: frmIzvozPregledKontejneraDrumskeUsluge
  - „Pakovanje voza“ — klik → `button31_Click` — otvara: frmPovezivanjeKontejneraIVagona
  - „Otprema kontejnera Terminal“ — klik → `toolStripButton8_Click` — otvara: frmOtpremaVozaIzPlana
  - „Vrati u nerasporedjene“ — klik → `toolStripButton3_Click` — proc: VratiKontejnerNerasporedjenoSelektovanoIzvoz; preko: InsertIzvozKonacna.VratiUNerasporedjene
  - „Dodaj nerasporedjene“ — klik → `toolStripButton2_Click` — otvara: frmFormiranjePlanaIzvoz
  - „Izdavanje naloga“ — klik → `toolStripButton1_Click`
  - „Tabela“ — klik → `toolStripButton5_Click` — otvara: frmIzvozKonacnaTable
  - „Usluge“ — klik → `toolStripButton4_Click` — otvara: frmIzvozUnosManipulacije
  - „Dokumentacija“ — klik → `toolStripButton6_Click` — otvara: frmIzvozDokumenta
  - `button6` — klik → `tsDelete_Click` — proc: DeleteIzvozKonacnaSve; preko: InsertIzvozKonacna.DelIzvozKonacnaSve
  - … još 2

#### frmIzvozKonacnaTable [N+S]
- **Fajl:** `Izvoz/frmIzvozKonacnaTable.cs` — „Tabela izvoz konačna“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Dodaj izvoznom vozu iz uvoza“ → **frmFormiranjePlanaIzvoz** → „Otvori plan“ → **frmIzvozKonacna** → „Tabela“ → **frmIzvozKonacnaTable**
- **Stari izgled:** Izvoz › Izvoz › Planovi utovara / pakovanja voza → **frmPlanoviIzvoza** → „Otvori“ → **frmIzvozKonacna** → „Tabela“ → **frmIzvozKonacnaTable**
- **Tabele:** `Carinarnice` S, `InspekciskiTretman` S, `IzvozKonacna` SU, `IzvozKonacnaNHM` S, `IzvozKonacnaVrstaManipulacije` S, `IzvozKonacnaVrstaRobeHS` S, `KontejnerskiTerminali` S, `KrajnjaDestinacija` S, `MestaUtovara` S, `NHM` S, `Partnerji` S, `TipKontenjera` S, `uvNacinPakovanja` S, `Valute` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaRobeHS` S, `VrstePostupakaUvoz` S
- **Combobox → tabela:** `cboOpsti` ← `Partnerji`; `cboOpsti` ← `Partnerji`; `cboOpsti` ← `Partnerji`; `cboOpsti` ← `Partnerji`; `cboOpsti` ← `Partnerji`; `cboOpsti` ← `TipKontenjera`; `cboOpsti` ← `Partnerji`; `cboOpsti` ← `Partnerji`; `cboOpsti` ← `Valute`; `cboOpsti` ← `KrajnjaDestinacija`; `cboOpsti` ← `KontejnerskiTerminali`; `cboOpsti` ← `MestaUtovara`; `cboOpsti` ← `Carinarnice`; `cboOpsti` ← `Partnerji`; `cboOpsti` ← `VrstaCarinskogPostupka`; `cboOpsti` ← `InspekciskiTretman`; `cboOpsti` ← `uvNacinPakovanja`
- **Funkcionalnosti:**
  - „IZLAZ“ — klik → `button1_Click`
  - „Primeni izmenu“ — klik → `button2_Click`
  - „Izberi“ — klik → `button3_Click`
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - „Refresh“ — klik → `button4_Click`

#### frmIzvozKonacnaZaglavlje [S]
- **Fajl:** `Izvoz/frmIzvozKonacnaZaglavlje.cs` — „Izvoz vozom konačna zaglavlje“
- **Stari izgled:** Izvoz › Izvoz › Planovi utovara / pakovanja voza → **frmPlanoviIzvoza** → „Novi plan“ → **frmIzvozKonacnaZaglavlje**
- **Otvara:** frmFormiranjePlanaIzvoz („Dopune kontejnera“)
- **Tabele:** `IzvozKonacnaZaglavlje` SIUD p, `Stanice` S, `Voz` S
- **Procedure:** `DeleteIzvozKonacnaZaglavlje`, `InsertIzvozKonacnaZaglavlje`, `UpdateIzvozKonacnaZaglavlje`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertIzvozKonacnaZaglavlje, UpdateIzvozKonacnaZaglavlje; preko: InsertIzvozKonacnaZaglavlje.InsIzvozKonacnaZaglavlje, InsertIzvozKonacnaZaglavlje.UpdIzvozKonacnaZaglavlje
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteIzvozKonacnaZaglavlje; preko: InsertIzvozKonacnaZaglavlje.DelIzvozKonacnaZaglavlje
  - „Dopuna kontejnera nerasporedjeni“ — klik → `toolStripButton1_Click` — otvara: frmFormiranjePlanaIzvoz
  - „Dopune kontejnera“ — klik → `toolStripButton1_Click` — otvara: frmFormiranjePlanaIzvoz
  - `button20` — klik → `tsDelete_Click` — proc: DeleteIzvozKonacnaZaglavlje; preko: InsertIzvozKonacnaZaglavlje.DelIzvozKonacnaZaglavlje
  - `button21` — klik → `tsSave_Click` — proc: InsertIzvozKonacnaZaglavlje, UpdateIzvozKonacnaZaglavlje; preko: InsertIzvozKonacnaZaglavlje.InsIzvozKonacnaZaglavlje, InsertIzvozKonacnaZaglavlje.UpdIzvozKonacnaZaglavlje
  - `this` — otvaranje forme → `frmIzvozKonacnaZaglavlje_Load` — čita: Voz

#### frmIzvozKopiranjeKontejnera [S]
- **Fajl:** `Izvoz/frmIzvozKopiranjeKontejnera.cs` — „Izvoz kopiranje“
- **Stari izgled:** Izvoz › Izvoz › Nerasporedjeni kontejneri tabela → **frmPregledKontejneraIzvoz** → „Kopiraj“ → **frmIzvozKopiranjeKontejnera**
- **Procedure:** `KopirajKontejnerIzvoz`
- **Funkcionalnosti:**
  - „Kopiraj“ — klik → `button1_Click` — proc: KopirajKontejnerIzvoz; preko: InsertIzvoz.KopirajKontejner

#### frmIzvozKopiranjeUslugaKontejnera1 [N+S]
- **Fajl:** `Izvoz/frmIzvozKopiranjeUslugaKontejnera1.cs` — „Izvoz kopiranje usluga kontejnera“
- **Novi izgled:** Logistika izvoza → **LogistikaIzvoza1** → „Formiranje naloga za otpremu“ → **LogistikaIzvoza2** → „Kreiraj novi“ → **frmProdajniNalogIzvozTabela** → `gridGroupingControl1` (TableControlCellClick) → **frmGrupniUnosPoljaIzvoz** → „Dodaj usluge“ → **frmIzvozUnosManipulacije** → „Kopiraj usluge“ → **frmIzvozKopiranjeUslugaKontejnera1**
- **Stari izgled:** Izvoz › Izvoz › Nerasporedjeni kontejneri Obrazac → **frmIzvoz** → „Unos usluga“ → **frmIzvozUnosManipulacije** → „Kopiraj usluge“ → **frmIzvozKopiranjeUslugaKontejnera1**
- **Tabele:** `IzvozKonacna` S
- **Funkcionalnosti:**
  - „Kopiraj“ — klik → `button23_Click`

#### frmIzvozPregledKontejneraDrumskeUsluge [N+S]
- **Fajl:** `Izvoz/frmIzvozPregledKontejneraDrumskeUsluge.cs` — „Generisanje drumske usluge“
- **Novi izgled:** Logistika izvoza → **LogistikaIzvoza1** → „Formiranje naloga za otpremu“ → **LogistikaIzvoza2** → „Kreiraj novi“ → **frmProdajniNalogIzvozTabela** → `gridGroupingControl1` (TableControlCellClick) → **frmGrupniUnosPoljaIzvoz** → „Pusti Drumski uslugu“ → **frmIzvozPregledKontejneraDrumskeUsluge**
- **Stari izgled:** Izvoz › Izvoz › Nerasporedjeni kontejneri Obrazac → **frmIzvoz** → „Drumske usluge“ → **frmIzvozPregledKontejneraDrumskeUsluge**
- **Tabele:** `Automobili` S, `IzvozKonacna` S, `IzvozKonacnaVrstaManipulacije` S, `IzvozVrstaManipulacije` S, `organizacioneJedinice` S, `RadniNalogDrumski` S, `StatusVozila` S, `VrstaManipulacije` S
- **Procedure:** `DodajStavkeURadniNalog`, `NapraviRadniNalogDrumski`
- **Funkcionalnosti:**
  - „Dopuna naloga“ — klik → `button1_Click` — proc: DodajStavkeURadniNalog; preko: InsertUvoz.UpdateRadniNalogDrumski
  - „Formiranje naloga“ — klik → `button23_Click` — proc: NapraviRadniNalogDrumski; preko: InsertUvoz.KreirajRadniNalogDrumski

#### frmIzvozTable [S]
- **Fajl:** `Izvoz/frmIzvozTable.cs` — „Nerasporedjeni tabela“
- **Stari izgled:** Izvoz › Izvoz › Nerasporedjeni kontejneri Obrazac → **frmIzvoz** → „Tabela“ → **frmIzvozTable**
- **Otvara:** frmUvozKopirajKontejner („Kopiranje“)
- **Tabele:** `Carinarnice` S, `InspekciskiTretman` S, `Izvoz` SU, `IzvozNHM` S, `IzvozVrstaManipulacije` S, `IzvozVrstaRobeHS` S, `KontejnerskiTerminali` S, `KrajnjaDestinacija` S, `MestaUtovara` S, `NHM` S, `Partnerji` S, `TipKontenjera` S, `uvNacinPakovanja` S, `Valute` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaRobeHS` S, `VrstePostupakaUvoz` S
- **Funkcionalnosti:**
  - „IZLAZ“ — klik → `button1_Click`
  - „Primeni izmenu“ — klik → `button2_Click`
  - „Izberi“ — klik → `button3_Click`
  - „Refresh“ — klik → `button4_Click`
  - „Kopiranje “ — klik → `button25_Click` — otvara: frmUvozKopirajKontejner
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`

#### frmIzvozTerminalPovezi [S]
- **Fajl:** `Izvoz/frmIzvozTerminalPovezi.cs` — „POVEZIVANJE KONTEJNERA SA NALOGOM IZVOZA“
- **Stari izgled:** Terminal › Terminalske usluge › Poveži kontejner sa nalogom izvoza → **frmIzvozTerminalPovezi**
- **Tabele:** `Carinarnice` S, `InspekciskiTretman` S, `Izvoz` S, `IzvozKonacna` S, `IzvozKonacnaNHM` S, `IzvozKonacnaVrstaManipulacije` S, `IzvozKonacnaVrstaRobeHS` S, `IzvozNHM` S, `IzvozVrstaManipulacije` S, `IzvozVrstaRobeHS` S, `KontejnerskiTerminali` S, `KontejnerStatus` S, `KontejnerTekuce` S, `KrajnjaDestinacija` S, `MestaUtovara` S, `NHM` S, `Partnerji` S, `Pozicija` S, `Skladista` S, `TipKontenjera` S, `uvNacinPakovanja` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaRobeHS` S, `VrstePostupakaUvoz` S
- **Procedure:** `OdrediKontejnerTerminal`, `UpdateIzvozTerminalIzbor`
- **Funkcionalnosti:**
  - „Iz nerasporedjenih“ — klik → `chkNerasporedjeni_CheckedChanged`
  - „POVEŽI / SNIMI PODATKE“ — klik → `button1_Click` — proc: OdrediKontejnerTerminal, UpdateIzvozTerminalIzbor; preko: InsertIzvoz.UpdIzvozTerminalIzbor, InsertIzvozKonacna.OdrediKontejnerTerminal
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`

#### frmIzvozUnosManipulacije [N+S]
- **Fajl:** `Izvoz/frmIzvozUnosManipulacije.cs` — „Unos usluge izvoz“
- **Novi izgled:** Logistika izvoza → **LogistikaIzvoza1** → „Formiranje naloga za otpremu“ → **LogistikaIzvoza2** → „Kreiraj novi“ → **frmProdajniNalogIzvozTabela** → `gridGroupingControl1` (TableControlCellClick) → **frmGrupniUnosPoljaIzvoz** → „Dodaj usluge“ → **frmIzvozUnosManipulacije**
- **Stari izgled:** Izvoz › Izvoz › Nerasporedjeni kontejneri Obrazac → **frmIzvoz** → „Unos usluga“ → **frmIzvozUnosManipulacije**
- **Otvara:** frmIzvozKopiranjeUslugaKontejnera1 („Kopiraj usluge“)
- **Tabele:** `Carinarnice` S, `Cene` S, `GrupaVrsteManipulacije` S, `InspekciskiTretman` S, `Izvoz` SU, `IzvozKonacna` SU, `IzvozKonacnaNHM` S, `IzvozKonacnaVrstaManipulacije` SID p, `IzvozKonacnaVrstaRobeHS` S, `IzvozNHM` S, `IzvozVrstaManipulacije` SIUD p, `IzvozVrstaRobeHS` S, `KontejnerskiTerminali` S, `KontejnerStatus` S, `KrajnjaDestinacija` S, `MestaUtovara` S, `NHM` S, `organizacioneJedinice` S, `Partnerji` S, `Scenario` S, `TipKontenjera` S, `uvNacinPakovanja` S, `UvozKonacnaVrstaManipulacije` S, `UvozVrstaManipulacije` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaRobeHS` S, `VrstePostupakaUvoz` S
- **Procedure:** `DeleteIzvozKonacnaVrstaManipulacije`, `DeleteIzvozVrstaManipulacije`, `InsertIzvozKonacnaVMIzUvoza`, `InsertIzvozKonacnaVrstaManipulacije`, `InsertIzvozVMIzUvoza`, `InsertIzvozVrstaManipulacije`, `UpdateScenarioKontejnera`, `UpdCenaIzvozKonacnaVrstaManipulacije`, `UpdCenaIzvozVrstaManipulacije`, `UpdPDVIzvozKonacnaVrstaManipulacije`, `UpdPDVIzvozVrstaManipulacije`, `UpdPlatiocaIzvozKonacnaVrstaManipulacije`, `UpdPlatiocaIzvozVrstaManipulacije`
- **Combobox → tabela:** `cboNalogodavac1` ← `Partnerji`; `cboNalogodavac2` ← `Partnerji`; `cboNalogodavac3` ← `Partnerji`; `cboUvoznik` ← `Partnerji`; `cboScenario` ← `Scenario`; `cboScenario` ← `Scenario`; `cboScenario` ← `Scenario`; `cboScenario` ← `Scenario`; `cboScenario` ← `Scenario`; `cboScenario` ← `Scenario`; `cboScenario` ← `Scenario`; `cboScenario` ← `Scenario`; `cboGrupaVrsteManipulacije` ← `GrupaVrsteManipulacije`
- **Funkcionalnosti:**
  - „N3-BU“ — klik → `button5_Click`
  - „N2-BU“ — klik → `button4_Click`
  - „N1-BU“ — klik → `button3_Click`
  - „N3“ — klik → `button2_Click`
  - „N2“ — klik → `button1_Click`
  - `dataGridView7` — klik na ćeliju → `dataGridView7_CellClick` — proc: UpdPDVIzvozKonacnaVrstaManipulacije, UpdPDVIzvozVrstaManipulacije; preko: InsertIzvoz.UpdPDVIzvozKonacnaUsluga, InsertIzvoz.UpdPDVIzvozUsluga
  - `dataGridView7` — CellLeave → `dataGridView7_CellLeave` — proc: UpdCenaIzvozKonacnaVrstaManipulacije, UpdCenaIzvozVrstaManipulacije; preko: InsertIzvoz.UpdCENAIzvozKonacnaUsluga, InsertIzvoz.UpdCENAIzvozUsluga
  - „N1 >>>>“ — klik → `button12_Click` — čita: Cene
  - „OPŠTI“ — klik → `button11_Click`
  - „N1“ — klik → `button13_Click`
  - „N2 >>>>“ — klik → `button6_Click` — čita: Cene
  - „N3 >>>>“ — klik → `button7_Click` — čita: Cene
  - „Obrisi selektovane“ — klik → `button8_Click` — proc: DeleteIzvozKonacnaVrstaManipulacije, DeleteIzvozVrstaManipulacije; preko: InsertIzvoz.DelIzvozKonacnaUsluga, InsertIzvoz.DelIzvozUsluga
  - „Promeni  platioca“ — klik → `button9_Click` — proc: UpdPlatiocaIzvozKonacnaVrstaManipulacije, UpdPlatiocaIzvozVrstaManipulacije; preko: InsertIzvoz.UpdPlatiocaIzvozKonacnaUsluga, InsertIzvoz.UpdPlatiocaIzvozUsluga
  - „P >>>>“ — klik → `button10_Click` — čita: Cene
  - „SVE USLUGE“ — klik → `button14_Click`
  - „ADMINISTRATIVNE“ — klik → `button15_Click`
  - „Prikazi sve kontejnere“ — klik → `toolStripButton6_Click`
  - „Prikazi selektovani kontejner“ — klik → `toolStripButton1_Click`
  - „Prenesi uvozne usluge pakovanja voza - Pakovanje voza“ — klik → `toolStripButton2_Click` — piše: IzvozKonacna[U], Izvoz[U]; proc: InsertIzvozKonacnaVMIzUvoza, InsertIzvozVMIzUvoza; preko: InsertIzvoz.InsUbaciUsluguKonacnaPlan, InsertIzvoz.InsUbaciUsluguPlan
  - „PO SCENARIJU“ — klik → `button16_Click`
  - „DODATNE“ — klik → `button17_Click`
  - „PO GRUPI“ — klik → `button18_Click`
  - „Promeni  platioca“ — klik → `button19_Click` — proc: UpdPlatiocaIzvozKonacnaVrstaManipulacije, UpdPlatiocaIzvozVrstaManipulacije; preko: InsertIzvoz.UpdPlatiocaIzvozKonacnaUsluga, InsertIzvoz.UpdPlatiocaIzvozUsluga
  - „Promeni  platioca“ — klik → `button20_Click` — proc: UpdPlatiocaIzvozKonacnaVrstaManipulacije, UpdPlatiocaIzvozVrstaManipulacije; preko: InsertIzvoz.UpdPlatiocaIzvozKonacnaUsluga, InsertIzvoz.UpdPlatiocaIzvozUsluga
  - „Promeni  platioca“ — klik → `button21_Click` — proc: UpdPlatiocaIzvozKonacnaVrstaManipulacije, UpdPlatiocaIzvozVrstaManipulacije; preko: InsertIzvoz.UpdPlatiocaIzvozKonacnaUsluga, InsertIzvoz.UpdPlatiocaIzvozUsluga
  - „S N I M I“ — klik → `button22_Click`
  - „Kopiraj usluge“ — klik → `button27_Click` — otvara: frmIzvozKopiranjeUslugaKontejnera1
  - „Prenesi uvozne usluge“ — klik → `toolStripButton2_Click` — piše: IzvozKonacna[U], Izvoz[U]; proc: InsertIzvozKonacnaVMIzUvoza, InsertIzvozVMIzUvoza; preko: InsertIzvoz.InsUbaciUsluguKonacnaPlan, InsertIzvoz.InsUbaciUsluguPlan
  - „Prikaži selektovani kontejner“ — klik → `toolStripButton1_Click`
  - „Prikaži sve kontejnere“ — klik → `toolStripButton6_Click`
  - `this` — otvaranje forme → `frmIzvozUnosManipulacije_Load` — čita: GrupaVrsteManipulacije

#### frmJediniceMere [S]
- **Fajl:** `Sifarnici/frmJediniceMere.cs` — „Jedinice mere“
- **Stari izgled:** Komercijala › Ugovori › Jedinice mere → **frmJediniceMere**
- **Tabele:** `MerskeEnote` SIUD p
- **Procedure:** `DeleteMerskeEnote`, `InsertMerskeEnote`, `UpdateMerskeEnote`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertMerskeEnote, UpdateMerskeEnote; preko: InsertMerskeEnote.InsMerskeEnote, InsertMerskeEnote.UpdMerskeEnote
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteMerskeEnote; preko: InsertMerskeEnote.DelMerskeEnote
  - `button20` — klik → `tsDelete_Click` — proc: DeleteMerskeEnote; preko: InsertMerskeEnote.DelMerskeEnote
  - `button21` — klik → `tsSave_Click` — proc: InsertMerskeEnote, UpdateMerskeEnote; preko: InsertMerskeEnote.InsMerskeEnote, InsertMerskeEnote.UpdMerskeEnote

#### frmKalendar [S]
- **Fajl:** `SyncForm/frmKalendar.cs` — „Kalendar“
- **Stari izgled:** Evidencije › Planiranje radnika › Kalendar → **frmPlanRada** → „Otvori kalendar“ → **frmKalendar**
- **Tabele:** `Appointments` S
- **Funkcionalnosti:**
  - „Save“ — klik → `buttonAdv1_Click`

#### frmKalmarGateIN [S]
- **Fajl:** `RadniNalozi/frmKalmarGateIN.cs` — „KALMAR GATE IN“
- **Stari izgled:** Terminal › Prevoz › K - GATE IN KAMION → **frmKalmarGateIN**
- **Tabele:** `RNPrijemPlatforme` S, `RNPrijemPlatforme2` S, `Skladista` S, `TipKontenjera` S
- **Funkcionalnosti:**
  - „2: URAĐENI“ — klik → `button1_Click` — čita: RNPrijemPlatforme, Skladista, TipKontenjera
  - „1: AKTIVNI“ — klik → `button2_Click` — čita: RNPrijemPlatforme, RNPrijemPlatforme2, Skladista, TipKontenjera
  - „Uradjeni“ — klik → `button1_Click` — čita: RNPrijemPlatforme, Skladista, TipKontenjera
  - „Aktivni“ — klik → `button2_Click` — čita: RNPrijemPlatforme, RNPrijemPlatforme2, Skladista, TipKontenjera

#### frmKalmarGateINKaminonPreneseno [S]
- **Fajl:** `RadniNalozi/frmKalmarGateINKaminonPreneseno.cs` — „KALMAR GATE IN - KAMION PRENOS POSLE CIR“
- **Stari izgled:** Terminal › Prevoz › K - PRENOS → **frmKalmarGateINKaminonPreneseno**
- **Tabele:** `RNPrijemPlatforme` S, `Skladista` S, `TipKontenjera` S
- **Funkcionalnosti:**
  - „2: URAĐENI“ — klik → `button1_Click` — čita: RNPrijemPlatforme, Skladista, TipKontenjera
  - „1: AKTIVNI“ — klik → `button2_Click` — čita: RNPrijemPlatforme, Skladista, TipKontenjera
  - „Uradjeni“ — klik → `button1_Click` — čita: RNPrijemPlatforme, Skladista, TipKontenjera
  - „Aktivni“ — klik → `button2_Click` — čita: RNPrijemPlatforme, Skladista, TipKontenjera

#### frmKalmarGateInVoz [S]
- **Fajl:** `RadniNalozi/frmKalmarGateInVoz.cs` — „KALMAR - GATE IN VOZ“
- **Stari izgled:** Terminal › Prevoz › K - GATE IN VOZ → **frmKalmarGateInVoz**
- **Tabele:** `RNPrijemVoza` S, `Skladista` S, `TipKontenjera` S, `Voz` S
- **Funkcionalnosti:**
  - „1: URAĐENI“ — klik → `button1_Click` — čita: RNPrijemVoza, Skladista, TipKontenjera, Voz
  - „1: AKTIVNI“ — klik → `button2_Click` — čita: RNPrijemVoza, Skladista, TipKontenjera, Voz
  - „Uradjeni“ — klik → `button1_Click` — čita: RNPrijemVoza, Skladista, TipKontenjera, Voz
  - „Aktivni“ — klik → `button2_Click` — čita: RNPrijemVoza, Skladista, TipKontenjera, Voz

#### frmKalmarGateOut [S]
- **Fajl:** `RadniNalozi/frmKalmarGateOut.cs` — „KALMAR GATE OUT KAMION“
- **Stari izgled:** Terminal › Prevoz › K - GATE OUT KAMION → **frmKalmarGateOut**
- **Tabele:** `RNOtpremaPlatforme` S, `RNOtpremaPlatforme2` S, `Skladista` S, `TipKontenjera` S
- **Funkcionalnosti:**
  - „2: URAĐENI“ — klik → `button1_Click` — čita: RNOtpremaPlatforme, Skladista, TipKontenjera
  - „1: AKTIVNI“ — klik → `button2_Click` — čita: RNOtpremaPlatforme, RNOtpremaPlatforme2, Skladista, TipKontenjera
  - „Uradjeni“ — klik → `button1_Click` — čita: RNOtpremaPlatforme, Skladista, TipKontenjera
  - „Aktivni“ — klik → `button2_Click` — čita: RNOtpremaPlatforme, RNOtpremaPlatforme2, Skladista, TipKontenjera

#### frmKalmarGateOutVoz [S]
- **Fajl:** `RadniNalozi/frmKalmarGateOutVoz.cs` — „KALMAR GATE OUT TRAIN“
- **Stari izgled:** Terminal › Prevoz › K - GATE OUT VOZ → **frmKalmarGateOutVoz**
- **Tabele:** `RNOtpremaVoza` S, `Skladista` S, `TipKontenjera` S, `Voz` S, `VrstaManipulacije` S
- **Funkcionalnosti:**
  - „2: URAĐENI“ — klik → `button1_Click` — čita: RNOtpremaVoza, Skladista, TipKontenjera, Voz, VrstaManipulacije
  - „1: AKTIVNI“ — klik → `button2_Click` — čita: RNOtpremaVoza, Skladista, TipKontenjera, Voz, VrstaManipulacije
  - „Uradjeni“ — klik → `button1_Click` — čita: RNOtpremaVoza, Skladista, TipKontenjera, Voz, VrstaManipulacije
  - „Aktivni“ — klik → `button2_Click` — čita: RNOtpremaVoza, Skladista, TipKontenjera, Voz, VrstaManipulacije

#### frmKamionDetalji [N+S]
- **Fajl:** `Kapija/frmKamionDetalji.cs` — „Kamion unosna forma“
- **Novi izgled:** Kapija → **frmKapija1** → „Novi dolazak“ → **frmKamionDetalji**
- **Stari izgled:** Kapija › Kapije › Pregled kamiona → **frmPregledKamiona** → „Otvori“ → **frmKamionDetalji**
- **Tabele:** `Kapija` SIUD p, `StatusKapija` S
- **Procedure:** `DeleteKapija`, `InsertKapija`, `UpdateKapija`
- **Combobox → tabela:** `cboStatus` ← `StatusKapija`
- **Funkcionalnosti:**
  - `button20` — klik → `button20_Click` — proc: DeleteKapija; preko: InsertKapija.DelKapija
  - `button21` — klik → `button21_Click` — proc: InsertKapija, UpdateKapija; preko: InsertKapija.InsKapija, InsertKapija.UpdeteKapija

#### frmKapija1 [N]
- **Fajl:** `MainLeget/Kapija/frmKapija1.cs` — „frmKapija1“
- **Novi izgled:** Kapija → **frmKapija1**
- **Otvara:** frmKamionDetalji („Novi dolazak“), frmPregledKamiona („Lista SVIH naloga“), frmKapijaOtvoreniNalozi („Lista naloga u NAJAVI“)
- **Funkcionalnosti:**
  - „Novi dolazak“ — klik → `sfButton1_Click`
  - „Lista SVIH naloga“ — klik → `sfButton6_Click`
  - „Lista naloga u NAJAVI“ — klik → `sfButton4_Click` — otvara: frmKapijaOtvoreniNalozi

#### frmKapijaOtvoreniNalozi [N]
- **Fajl:** `Kapija/frmKapijaOtvoreniNalozi.cs` — „Kapija nalozi“
- **Novi izgled:** Kapija → **frmKapija1** → „Lista naloga u NAJAVI“ → **frmKapijaOtvoreniNalozi**
- **Otvara:** frmPrijemVozaIzPlana (metoda `FormirajRadniNalog`), frmOtpremaKontejneraKamionomIzKontejnera (metoda `FormirajRadniNalog`)
- **Tabele:** `IzvozKonacna` S, `Kapija` SI p, `KontejnerLog` I p, `MestaUtovara` S, `PrijemKontejneraVoz` S, `RadniNalogInterni` S, `RadniNalogInterniPotvrda` S, `Scenario` S, `TipKontenjera` S
- **Procedure:** `InsertKapija`, `InsertKontejnerLog`, `InsertRN4PrPlatformeKamIzvoz`, `spPrenesiPlanUtovaraUPrijemPlatformaIzvoz`, `UpdateKapijaNeslaganje`
- **Funkcionalnosti:**
  - „Potvrdi odlazak“ — klik → `button3_Click` — čita: RadniNalogInterni, RadniNalogInterniPotvrda; proc: PromeniStatusKapijaOdlazak; preko: InsertRadniNalogInterni.PromeniStatusKapijaOdlazak
  - „Refresh“ — klik → `button2_Click`
  - „Prijavi neslaganje“ — klik → `button1_Click` — proc: UpdateKapijaNeslaganje; preko: InsertKapija.UpdeteKapijaNeslaganje
  - „Potvrdi dolazak“ — klik → `button27_Click` — proc: InsertKapija; preko: InsertKapija.InsKapija
  - „gridGroupingControl2“ — klik na ćeliju → `gridGroupingControl2_TableControlCellClick`

#### frmKasnjenja [S]
- **Fajl:** `Dokumenta/frmKasnjenja.cs` — „Kašnjenja na trasi“
- **Stari izgled:** Komercijala › Najave › Poslovi → **frmNajava** → „Prevozni nalog - Trase i Lokomotive“ → **frmRadniNalog** → „Unos trasa“ → **frmRadniNalogTraseLok** → „Uzroci kašnjenja“ → **frmKasnjenja**
- **Tabele:** `RadniNalogTraseLokUzr` S, `Razlozi` S, `Trase` S
- **Procedure:** `InsertUzrociKasnjenjaLok`, `UpdateUzrociKasnjenjaLok`
- **Funkcionalnosti:**
  - „Unesi “ — klik → `button1_Click` — proc: InsertUzrociKasnjenjaLok, UpdateUzrociKasnjenjaLok; preko: InsertUzrociKasnjenjaLok.InsRNTLU
  - `this` — otvaranje forme → `frmKasnjenja_Load` — čita: Razlozi, Trase

#### frmKomercijalniPregled [S]
- **Fajl:** `Dokumenta/frmKomercijalniPregled.cs` — „Komercijalni pregled“
- **Stari izgled:** Evidencije › Evidencija aktivnosti › Pregled trenutnih aktivnosti → **frmPregledAktivnosti** → „Komercijalni pregled“ → **frmKomercijalniPregled**
- **Tabele:** `KomercijalniPregled` S, `SifOpisNeispravnosti` S, `SifVrstaNepravilnosti` S

#### frmKontaktOsobe [N+S]
- **Fajl:** `Dokumenta/frmKontaktOsobe.cs` — „Kontakt osobe“
- **Novi izgled:** Podešavanja sistema → **Podesavanje1** → „Partneri“ → **frmPartnerji** → „Kontakt osobe“ → **frmKontaktOsobe**
- **Stari izgled:** Osnovni podaci › Partneri › Kontakt osobe → **frmKontaktOsobe**
- **Tabele:** `Carinarnice` S, `Partnerji` S, `partnerjiKontOseba` S
- **Combobox → tabela:** `txtPaKOSifra` ← `Partnerji`; `cboCarinarnica` ← `Carinarnice`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click`
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „Svi kontakti“ — klik → `toolStripButton1_Click`
  - „Svi kontakti“ — klik → `toolStripButton1_Click`
  - `this` — otvaranje forme → `frmKontaktOsobe_Load` — čita: Carinarnice, Partnerji

#### frmKontaktOsobeMU [N+S]
- **Fajl:** `Izvoz/frmKontaktOsobeMU.cs` — „Kontakt osobe na mestu utovara“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Dodaj izvoznom vozu iz uvoza“ → **frmFormiranjePlanaIzvoz** → „Otvori plan“ → **frmIzvozKonacna** → `button22` (Click) → **frmKontaktOsobeMU**
- **Stari izgled:** Izvoz › Šifarnici › Kontakt osobe Mesta utovara → **frmKontaktOsobeMU**
- **Tabele:** `MestaUtovara` S, `partnerjiKontOsebaMU` S
- **Combobox → tabela:** `txtPaKOSifra` ← `MestaUtovara`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click`
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - `this` — otvaranje forme → `frmKontaktOsobeMU_Load` — čita: MestaUtovara

#### frmKontejneriNaPrijemnici [N+S]
- **Fajl:** `Dokumenta/frmKontejneriNaPrijemnici.cs` — „Kontejner na prijemnicama“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Potvrdi“ → **frmPrijemKontejneraKamionLegetUvoz** → „?“ → **frmKontejneriNaPrijemnici**
- **Stari izgled:** Terminal › Radni nalozi › Prijem kontejnera kamion - N → **frmPrijemKontejneraKamionLegetUvoz** → „?“ → **frmKontejneriNaPrijemnici**
- **Tabele:** `PrijemKontejneraVozStavke` S

#### frmKontejnerskiTerminali [S]
- **Fajl:** `Uvoz/frmKontejnerskiTerminali.cs` — „Kontejnerski terminali“
- **Stari izgled:** Izvoz › Šifarnici › Kontejnerski terminali → **frmKontejnerskiTerminali**
- **Tabele:** `KontejnerskiTerminali` SIUD p
- **Procedure:** `DeleteKontejnerskiTerminali`, `InsertKontejnerskiTerminali`, `UpdateKontejnerskiTerminali`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertKontejnerskiTerminali, UpdateKontejnerskiTerminali; preko: InsertKontejnerskiTerminali.InsKontejnerskiTerminali, InsertKontejnerskiTerminali.UpdKontejnerskiTerminali
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteKontejnerskiTerminali; preko: InsertKontejnerskiTerminali.DelKontejnerskiTerminali
  - `button20` — klik → `tsDelete_Click` — proc: DeleteKontejnerskiTerminali; preko: InsertKontejnerskiTerminali.DelKontejnerskiTerminali
  - `button21` — klik → `tsSave_Click` — proc: InsertKontejnerskiTerminali, UpdateKontejnerskiTerminali; preko: InsertKontejnerskiTerminali.InsKontejnerskiTerminali, InsertKontejnerskiTerminali.UpdKontejnerskiTerminali

#### frmKontejnerStatus [S]
- **Fajl:** `Sifarnici/frmKontejnerStatus.cs` — „ŠIFRNIK STATUSA KONTEJNERA“
- **Stari izgled:** Terminal › Šifarnici › Status kontejnera → **frmKontejnerStatus**
- **Tabele:** `KontejnerStatus` S
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click`
  - „toolStripButton1“ — klik → `tsDelete_Click`

#### frmKontejnerTekuce [N+S]
- **Fajl:** `Uvoz/frmKontejnerTekuce.cs` — „Tekuci status kontejnera“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Depo cnt“ → **frmPrijemIOtpremaVozova2** → „Pregled skladišta“ → **TerminalMapFRM** → „Terminal central“ → **frmKontejnerTekuce**
- **Stari izgled:** Terminal › Terminalske usluge › TERMINAL CENTRAL → **frmKontejnerTekuce**
- **Tabele:** `KontejnerskiTerminali` S, `KontejnerStatus` S, `KontejnerTekuce` S, `Partnerji` S, `Pozicija` S, `Skladista` S
- **Funkcionalnosti:**
  - „TEKUCE STANJE - REFRESH“ — klik → `button1_Click` — čita: KontejnerStatus, KontejnerTekuce, KontejnerskiTerminali, PArtnerji, Pozicija, Skladista; izvoz u Excel
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - „Refresh“ — klik → `button1_Click` — čita: KontejnerStatus, KontejnerTekuce, KontejnerskiTerminali, PArtnerji, Pozicija, Skladista; izvoz u Excel

#### frmKontejnerTekuceArhiv [N+S]
- **Fajl:** `Uvoz/frmKontejnerTekuceArhiv.cs` — „BAZA ZAVRSENIH KONTEJNERA“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Terminal central (arhiv)“ → **frmKontejnerTekuceArhiv**
- **Stari izgled:** Terminal › Terminalske usluge › TERMINAL CENTRAL ARHIV → **frmKontejnerTekuceArhiv**
- **Tabele:** `KontejnerskiTerminali` S, `KontejnerStatus` S, `KontejnerTekuceArhiv` S, `Partnerji` S, `Pozicija` S, `Skladista` S
- **Funkcionalnosti:**
  - „Refresh“ — klik → `button1_Click` — čita: KontejnerStatus, KontejnerTekuceArhiv, KontejnerskiTerminali, PArtnerji, Pozicija, Skladista

#### frmKontejnerTekuceOperacijeLog [S]
- **Fajl:** `RadniNalozi/frmKontejnerTekuceOperacijeLog.cs` — „LOG OPERACIJA“
- **Stari izgled:** Terminal › Prevoz › PREGLED URADJENIH OPERACIJA → **frmKontejnerTekuceOperacijeLog**
- **Tabele:** `KontejnerTekuceOperacije` S
- **Funkcionalnosti:**
  - „Pokreni“ — klik → `button2_Click`
  - `timer1` — Tick → `timer1_Tick` — čita: KontejnerTekuceOperacije
  - „Pokreni“ — klik → `button2_Click`

#### frmKontrola [S]
- **Fajl:** `Dokumenta/frmKontrola.cs` — „Kontrola“
- **Stari izgled:** Operatika › Kontrola dokumentacije › Kontrolna forma → **frmKontrola**
- **Tabele:** `Delavci` S, `KontrolaDokumentacije` SIUD p, `KontrolaDokumentacijeGreske` S, `KontrolneGreske` S, `KontrolneGreskeTipDokumenta` S, `Najava` S
- **Procedure:** `DeleteKontrolaDokumentacije`, `InsertKontrolaDokumentacije`, `UpdateKontrolaDokumentacije`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertKontrolaDokumentacije, UpdateKontrolaDokumentacije; preko: InsertKontrola.InsKontrola, InsertKontrola.UpdKontrola
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteKontrolaDokumentacije; preko: InsertKontrola.DeleteKontrola
  - „Unesi“ — klik → `button1_Click`
  - „Izbaci“ — klik → `sfButton1_Click`
  - „Promeni“ — klik → `sfButton2_Click`
  - `cboTipDokumenta` — izbor → `cboTipDokumenta_SelectedValueChanged` — čita: KontrolneGreske
  - „?“ — klik → `sfButton3_Click` — čita: Delavci, KontrolneGreske, KontrolneGreskeTipDokumenta, Najava

#### frmKopiranjeCenovnikaPoTipu [S]
- **Fajl:** `TrackModal/Sifarnici/frmKopiranjeCenovnikaPoTipu.cs` — „Kopiranje cenovnika po parametrima“
- **Stari izgled:** Terminal › Šifarnici › Cene → **frmCene** → „Kopiraj cenovnik“ → **frmKopiranjeCenovnikaPoTipu**
- **Tabele:** `Cene` S, `Partnerji` S, `TipCenovnika` S, `VrstaManipulacije` S, `VrstePostupakaUvoz` S
- **Funkcionalnosti:**
  - „Pretraži“ — klik → `button3_Click`
  - „Kopiraj “ — klik → `button1_Click`
  - „Kopiranje selektovanog“ — klik → `button4_Click`
  - „Prikazi trenutni“ — klik → `button2_Click`
  - „Prikazi novi“ — klik → `button5_Click`
  - `this` — otvaranje forme → `frmKopiranjeCenovnikaPoTipu_Load` — čita: Partnerji, TipCenovnika

#### frmKorisnici [N+S]
- **Fajl:** `Administracija/frmKorisnici.cs` — „Korisnici“
- **Novi izgled:** Administracija → **Administracija1** → „Korisnici“ → **frmKorisnici**
- **Stari izgled:** Administrator › Admin › Korisnici → **frmKorisnici**
- **Tabele:** `Delavci` S, `Korisnici` SID p
- **Procedure:** `DeleteKorisnici`, `InsertKorisnici`
- **Combobox → tabela:** `cboLargoKorisnik` ← `Delavci`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertKorisnici; preko: InsertKorisnici.InsKorisnici
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteKorisnici; preko: InsertKorisnici.DeleteKorisnici
  - `this` — otvaranje forme → `frmKorisnici_Load` — čita: Delavci

#### frmKvarovi [S]
- **Fajl:** `Servis/frmKvarovi.cs` — „Kvarovi“
- **Stari izgled:** Održavanje › Lokomotive osnovno › Kvarovi → **frmKvarovi**
- **Tabele:** `GrupaKvarova` S, `Kvarovi` SI p
- **Procedure:** `InsertKvarovi`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertKvarovi; preko: InsertKvarovi.InsKVAR
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „?“ — klik → `button1_Click` — čita: GrupaKvarova, Kvarovi
  - `this` — otvaranje forme → `frmKvarovi_Load` — čita: GrupaKvarova

#### frmKvaroviAuto [S]
- **Fajl:** `Servis/frmKvaroviAuto.cs` — „Kvarovi auto“
- **Stari izgled:** Održavanje › Automobili › Kvarovi → **frmKvaroviAuto**
- **Tabele:** `GrupaKvarovaAuto` S, `KvaroviAuto` SI p
- **Procedure:** `InsertKvaroviAuto`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertKvaroviAuto; preko: InsertKvarovi.InsKVARAuto
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „?“ — klik → `button1_Click` — čita: GrupaKvarovaAuto, KvaroviAuto
  - `this` — otvaranje forme → `frmKvaroviAuto_Load` — čita: GrupaKvarovaAuto

#### frmLager [S]
- **Fajl:** `Promet/frmLager.cs` — „Pregled Lagera“
- **Stari izgled:** Terminal › Skladišno poslovanje › Pregled stanja zaliha → **frmLager**
- **Tabele:** `Komitenti` S, `Pozicija` S, `PrijemKontejneraVozStavke` S, `Promet` S, `Skladista` S, `TipKontenjera` S, `VrstaRobe` S
- **Funkcionalnosti:**
  - „Pretraži“ — klik → `btnPregled_Click` — čita: Pozicija, Promet, Skladista, pozicija, skladista
  - „?“ — klik → `button1_Click` — čita: Pozicija, PrijemKontejneraVozStavke, Promet, Skladista, pozicija, skladista
  - „?“ — klik → `button2_Click` — čita: Pozicija, PrijemKontejneraVozStavke, Promet, Skladista, pozicija, skladista
  - „?“ — klik → `button3_Click` — čita: Pozicija, PrijemKontejneraVozStavke, Promet, Skladista, pozicija, skladista
  - `this` — otvaranje forme → `frmLager_Load` — čita: Komitenti, Skladista, TipKontenjera, VrstaRobe

#### frmLagerOperater [N+S]
- **Fajl:** `Promet/frmLagerOperater.cs` — „Lager operater“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Formiraj otpremu kamionom“ → **frmOtpremaKontejneraKamionomIzKontejnera** → „Otvori“ → **frmOtpremaKontejneraUvozKamion** → „MAPA SKLADIŠTA“ → **frmLagerOperater**
- **Stari izgled:** Terminal › Skladišno poslovanje › Pregled stanja zaliha - Magacioner → **frmLagerOperater**
- **Tabele:** `NHM` S, `Partnerji` S, `Pozicija` S, `PrijemKontejneraVozStavke` S, `Promet` S, `Skladista` S, `TipKontenjera` S
- **Combobox → tabela:** `cboVrstaRobe` ← `NHM`; `cboVlasnikKontejnera` ← `Partnerji`; `cboSkladiste` ← `Skladista`; `cboTipKontejnera` ← `TipKontenjera`
- **Funkcionalnosti:**
  - „?“ — klik → `button3_Click` — čita: Pozicija, PrijemKontejneraVozStavke, Promet, Skladista
  - „?“ — klik → `button2_Click` — čita: Pozicija, PrijemKontejneraVozStavke, Promet, Skladista
  - „?“ — klik → `button1_Click` — čita: Pozicija, PrijemKontejneraVozStavke, Promet, Skladista
  - „Pretraži“ — klik → `btnPregled_Click` — čita: Pozicija, Promet, Skladista
  - `this` — otvaranje forme → `frmLagerOperater_Load` — čita: NHM, Partnerji, Skladista, TipKontenjera

#### frmListaProtokola [N+S]
- **Fajl:** `Drumski/frmListaProtokola.cs` — „Protokoli“
- **Novi izgled:** Drumski transport → **Drumski1** → „Cerada“ → **Cerade** → „FORMIRANJE\r\nTRANSPORTNOG NALOGA“ → **PakovanjeKamionaCerade** → „PROTOKOL“ → **frmListaProtokola**
- **Stari izgled:** Drumski › Drumski › Pakovanje kamiona → **PakovanjeKamiona1** → „PROTOKOL“ → **frmListaProtokola**
- **Otvara:** frmProtokolTransportnogNaloga (`subOstali` (Click))
- **Tabele:** `Izvoz` S, `IzvozKonacna` S, `Partnerji` S, `ProtokolRadniNalogDrumski` SD p, `RadniNalogDrumski` S, `TipProtokola` S
- **Procedure:** `DeleteProtokolRadniNalogDrumski`
- **Funkcionalnosti:**
  - „Otvori“ — klik → `button1_Click` — otvara: frmProtokolTransportnogNaloga
  - „Kreiraj protokol“ — klik → `btnKreirajNovi_Click`
  - „Otvori“ — klik → `otvoriToolStripMenuItem_Click` — otvara: frmProtokolTransportnogNaloga
  - „Storniraj“ — klik → `stornirajToolStripMenuItem_Click` — proc: DeleteProtokolRadniNalogDrumski; preko: InsertProtokolTransportnogNaloga.DelProtokolRadniNalogDrumski
  - `subAutodan` — klik → `SubAutodan_Click` — otvara: frmProtokolTransportnogNaloga
  - `subDodatnaRuta` — klik → `SubDodatnaRuta_Click` — otvara: frmProtokolTransportnogNaloga
  - `subOstali` — klik → `SubOstali_Click` — otvara: frmProtokolTransportnogNaloga

#### frmLogAktivnosti [S]
- **Fajl:** `Dokumenta/frmLogAktivnosti.cs` — „Log aktivnosti“
- **Stari izgled:** Evidencije › Evidencija aktivnosti › Evidencija → **frmEvidencijaRada** → „Log file“ → **frmLogAktivnosti**
- **Tabele:** `LogAktivnosti` S

#### frmLogovanjeSecond [P]
- **Fajl:** `Sifarnici/frmLogovanjeSecond.cs` — „Logovanje“
- **Tabele:** `Korisnici` S
- **Funkcionalnosti:**
  - „ZATVORI“ — klik → `sfButton1_Click`
  - „POKRENI“ — klik → `button1_Click` — otvara: MainP, NewMain; čita: Korisnici
  - „Prijava na Leget plus nalog“ — MouseClick → `label1_MouseClick`

#### frmLogTokaProcesa [N]
- **Fajl:** `Izvoz/frmLogTokaProcesa.cs` — „frmLogTokaProcesa“
- **Novi izgled:** Logistika izvoza → **LogistikaIzvoza1** → „Formiranje naloga za otpremu“ → **LogistikaIzvoza2** → „Izmeni postojeći“ → **frmPregledGrupaKontejneraIzvoz** → „Otvori“ → **frmLogTokaProcesa**
- **Tabele:** `KontejnerLog` S

#### frmLokomotivaVrstaPopisa [S]
- **Fajl:** `Servis/frmLokomotivaVrstaPopisa.cs` — „Lokomotiva vrste popisa“
- **Stari izgled:** Održavanje › Lokomotive osnovno › Popisne stavke → **frmLokomotivaVrstaPopisa**
- **Tabele:** `LokomotiveVrstePopisa` SD p, `Mesta` S, `VrstaPopisa` S
- **Procedure:** `DeleteLokomotiveVrstePopisa`, `InsertLokomotivaVrstaPopisa`, `UpdateLokomotiveVrstaPopisa`
- **Funkcionalnosti:**
  - „?“ — klik → `button1_Click`
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertLokomotivaVrstaPopisa, UpdateLokomotiveVrstaPopisa; preko: InsertLokomotiveVrstaPopisa.InsLokomotiveVrstePopisa, InsertLokomotiveVrstaPopisa.UpdLokomotiveVrstePopisa
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteLokomotiveVrstePopisa; preko: InsertLokomotiveVrstaPopisa.DeleteLokomotiveVrstePopisa
  - `this` — otvaranje forme → `frmLokomotivaVrstaPopisa_Load` — čita: Mesta, VrstaPopisa

#### frmLokomotive [S]
- **Fajl:** `Sifarnici/frmLokomotive.cs` — „Lokomotive“
- **Stari izgled:** Osnovni podaci › Prevozni šifarnici › Lokomotive → **frmLokomotive**
- **Tabele:** `LokomotivaSerija` S, `Mesta` S
- **Procedure:** `DeleteLokomotiva`, `InsertLokomotive`, `UpdateLokomotive`
- **Funkcionalnosti:**
  - „Promeni podatke“ — klik → `btnRacun_Click` — proc: InsertLokomotive; preko: InsertLokomotive.InsLokomotiva
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertLokomotive, UpdateLokomotive; preko: InsertLokomotive.InsLokomotiva, InsertLokomotive.UpdLokomotive
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteLokomotiva; preko: InsertLokomotive.DeleteLokomotive
  - „Aktivne/Neaktivne“ — klik → `toolStripButton1_Click`
  - „Aktivne/Neaktivne“ — klik → `toolStripButton1_Click`
  - `button20` — klik → `tsDelete_Click` — proc: DeleteLokomotiva; preko: InsertLokomotive.DeleteLokomotive
  - `button21` — klik → `tsSave_Click` — proc: InsertLokomotive, UpdateLokomotive; preko: InsertLokomotive.InsLokomotiva, InsertLokomotive.UpdLokomotive
  - `this` — otvaranje forme → `frmLokomotive_Load` — čita: LokomotivaSerija

#### frmLokomotiveNaTrasi [S]
- **Fajl:** `Dokumenta/frmLokomotiveNaTrasi.cs` — „Lokomotive na trasi“
- **Stari izgled:** Komercijala › Najave › Poslovi → **frmNajava** → „Prevozni nalog - Trase i Lokomotive“ → **frmRadniNalog** → „Lokomotive“ → **frmLokomotiveNaTrasi**
- **Tabele:** `Mesta` S, `RadniNalogLokNaTrasi` SIUD p, `Stanice` S, `Trase` S
- **Procedure:** `DeleteRadniNalogLokNaTrasi`, `InsertRadniNalogLokNaTrasi`, `UpdateRadniNalogLokNaTrasi`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertRadniNalogLokNaTrasi, UpdateRadniNalogLokNaTrasi; preko: InsertLokomotiveNaTrasi.InsRNTL, InsertLokomotiveNaTrasi.UpdRNTL
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteRadniNalogLokNaTrasi; preko: InsertLokomotiveNaTrasi.delRNTL
  - `this` — otvaranje forme → `frmLokomotiveNaTrasi_Load` — čita: Mesta, Stanice, Trase

#### frmMagacinskiBrojevi [S]
- **Fajl:** `Carinsko/frmMagacinskiBrojevi.cs` — „Magacinski brojevi“
- **Stari izgled:** Carinsko › Magacinsko poslovanje › Magacinski brojevi → **frmMagacinskiBrojevi**
- **Tabele:** `MagacinskiBrojevi` S
- **Procedure:** `DeleteMagacinskiBroj`, `InsertMagacinskiBroj`
- **Funkcionalnosti:**
  - `button20` — klik → `button20_Click` — proc: DeleteMagacinskiBroj; preko: InsertMagacinskiBrojevi.DelMagacinskiBrojevi
  - `button22` — klik → `button22_Click` — proc: InsertMagacinskiBroj; preko: InsertMagacinskiBrojevi.InsMagacinskiBrojevi

#### frmMailNajava [S]
- **Fajl:** `Dokumenta/frmMailNajava.cs` — „frmMailNajava“
- **Stari izgled:** Komercijala › Najave › Mail partneri → **frmMailNajava**
- **Tabele:** `Najava` S, `Partnerji` S, `partnerjiKontOseba` S, `Stanice` S
- **Funkcionalnosti:**
  - „Filtriraj“ — klik → `btn_Filter_Click` — čita: Najava, Partnerji, partnerjiKontOseba, stanice
  - „Posalji svima“ — klik → `btn_PosaljiSvi_Click` — čita: Najava, Partnerji, PartnerjiKontOseba, partnerjiKontOseba, stanice
  - „Posalji“ — klik → `btn_Posalji_Click` — čita: Najava, Partnerji, PartnerjiKontOseba, partnerjiKontOseba, stanice
  - „Sve najave“ — klik → `btn_Svi_Click`

#### frmManipulacije [N+S]
- **Fajl:** `TrackModal/Dokumeta/frmManipulacije.cs` — „Usluge“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Voz“ → **frmPrijemIOtpremaVozova4** → „Gate out voz“ → **frmPregledOtpreme** → „Otvori voz“ → **frmOtpremaKontejnera** → „Raspored manipulacija“ → **frmManipulacije**
- **Stari izgled:** Terminal › Prevoz › Otprema kontejnera vozom → **frmPregledOtpreme** → „Otvori voz“ → **frmOtpremaKontejnera** → „Raspored manipulacija“ → **frmManipulacije**
- **Otvara:** frmNajavaKomitemtaDokument („Nalog komitenta“)
- **Tabele:** `Komitenti` S, `NaruceneManipulacije` SID p, `OtpremaKontejnera` S, `OtpremaKontejneraVozStavke` S, `PrijemKontejneraVoz` S, `PrijemKontejneraVozStavke` S, `Voz` S, `VrstaManipulacije` S
- **Procedure:** `DeleteNaruceneManipulacije`, `InsertNaruceneManipulacije`
- **Combobox → tabela:** `cboPrijemVozom` ← `PrijemKontejneraVoz`, `Voz`; `cboPrijemKamionom` ← `PrijemKontejneraVoz`; `cboPrijemVozom` ← `OtpremaKontejnera`, `Voz`; `cboPrijemKamionom` ← `OtpremaKontejnera`; `cboPlatilac` ← `Komitenti`; `cboPrijemVozom` ← `PrijemKontejneraVoz`, `Voz`; `cboPrijemKamionom` ← `PrijemKontejneraVoz`; `cboPlatilac` ← `Komitenti`; `cboPrijemVozom` ← `OtpremaKontejnera`, `Voz`; `cboPlatilac` ← `Komitenti`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „POTVRDI URADJENA USLUGA SA PROMETOM“ — klik → `toolStripButton2_Click`
  - „Nalog komitenta “ — klik → `toolStripButton1_Click` — otvara: frmNajavaKomitemtaDokument
  - „POTVRDI URADJENA USLUGA BEZ PROMETA“ — klik → `toolStripButton3_Click`
  - „>>“ — klik → `btnUnesi_Click` — proc: InsertNaruceneManipulacije; preko: InsertNaruceneManipulacije.InsertNarManipulacije
  - „Period od:“ — klik → `label21_Click`
  - „Pretraži“ — klik → `btnPretrazi_Click` — čita: OtpremaKontejneraVozStavke, PrijemKontejneraVozStavke
  - „Pretraži“ — klik → `button1_Click` — čita: OtpremaKontejneraVozStavke, PrijemKontejneraVozStavke
  - „Platilac:“ — klik → `label8_Click`
  - „Izbriši manipulaciju“ — klik → `button2_Click` — proc: DeleteNaruceneManipulacije; preko: InsertNaruceneManipulacije.DeleteNarManipulacija
  - „?“ — klik → `button3_Click`
  - „Otprema“ — čekiranje → `chkOtprema_CheckedChanged`
  - „Prijem“ — čekiranje → `chkPrijem_CheckedChanged`
  - „Promet“ — klik → `button4_Click`
  - „Usluga“ — klik → `button5_Click`
  - „Bez zauzetih“ — čekiranje → `chkZauzete_CheckedChanged`
  - `this` — otvaranje forme → `frmManipulacije_Load` — čita: Komitenti, OtpremaKontejnera, PrijemKontejneraVoz, Voz
  - „toolStripButton1“ — klik → `tsPrvi_Click` — čita: NaruceneManipulacije
  - „toolStripButton1“ — klik → `tsNazad_Click` — čita: NaruceneManipulacije
  - „toolStripButton1“ — klik → `tsNapred_Click` — čita: NaruceneManipulacije
  - „toolStripButton1“ — klik → `tsPoslednja_Click` — čita: NaruceneManipulacije

#### frmManipulacije [N+S]
- **Fajl:** `Dokumenta/frmManipulacije.cs` — „Usluge“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Formiraj otpremu kamionom“ → **frmOtpremaKontejneraKamionomIzKontejnera** → „Otvori“ → **frmOtpremaKontejnera** → „Raspored usluga“ → **frmManipulacije**
- **Stari izgled:** Terminal › Terminalske usluge › Raspored manipulacija → **frmManipulacije**
- **Otvara:** frmNajavaKomitemtaDokument („Nalog komitenta“)
- **Tabele:** `NaruceneManipulacije` SID p, `OtpremaKontejnera` S, `OtpremaKontejneraVozStavke` S, `Partnerji` S, `Pozicija` S, `PrijemKontejneraVoz` S, `PrijemKontejneraVozStavke` S, `Promet` S, `Skladista` S, `Voz` S, `VrstaManipulacije` S
- **Procedure:** `DeleteNaruceneManipulacije`, `InsertNaruceneManipulacije`, `UpdateUradjenoNaruceneManipulacije`
- **Combobox → tabela:** `cboSkladiste` ← `Skladista`; `cboSkladIz` ← `Skladista`; `cboPozIz` ← `Pozicija`; `cboPrijemVozom` ← `PrijemKontejneraVoz`, `Voz`; `cboPrijemKamionom` ← `PrijemKontejneraVoz`; `cboPrijemVozom` ← `OtpremaKontejnera`, `Voz`; `cboPrijemKamionom` ← `OtpremaKontejnera`; `cboPlatilac` ← `Partnerji`; `cboPrijemVozom` ← `PrijemKontejneraVoz`, `Voz`; `cboPrijemKamionom` ← `PrijemKontejneraVoz`; `cboPlatilac` ← `Partnerji`; `cboPrijemVozom` ← `OtpremaKontejnera`, `Voz`; `cboPlatilac` ← `Partnerji`; `cboPozicija` ← `Pozicija`, `Promet`, `Skladista`; `cboPozicija` ← `Pozicija`, `Promet`, `Skladista`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „POTVRDI URADJENA USLUGA SA PROMETOM“ — klik → `toolStripButton2_Click`
  - „Nalog komitenta “ — klik → `toolStripButton1_Click` — otvara: frmNajavaKomitemtaDokument
  - „POTVRDI URADJENA USLUGA BEZ PROMETA“ — klik → `toolStripButton3_Click`
  - „>>“ — klik → `btnUnesi_Click` — proc: InsertNaruceneManipulacije; preko: InsertNaruceneManipulacije.InsertNarManipulacije
  - „Period od:“ — klik → `label21_Click`
  - „Pretraži“ — klik → `btnPretrazi_Click` — čita: OtpremaKontejneraVozStavke, PrijemKontejneraVozStavke
  - „Pretraži“ — klik → `button1_Click` — čita: OtpremaKontejneraVozStavke, PrijemKontejneraVozStavke
  - „Platilac:“ — klik → `label8_Click`
  - „Izbriši manipulaciju“ — klik → `button2_Click` — proc: DeleteNaruceneManipulacije; preko: InsertNaruceneManipulacije.DeleteNarManipulacija
  - „?“ — klik → `button3_Click`
  - „Otprema“ — čekiranje → `chkOtprema_CheckedChanged`
  - „Prijem“ — čekiranje → `chkPrijem_CheckedChanged`
  - `cboSkladiste` — izbor → `cboSkladiste_SelectedValueChanged` — čita: Pozicija, Promet, Skladista
  - „Promet“ — klik → `button4_Click`
  - „Usluga“ — klik → `button5_Click`
  - „Bez zauzetih“ — čekiranje → `chkZauzete_CheckedChanged` — čita: Pozicija, Promet, Skladista
  - `this` — otvaranje forme → `frmManipulacije_Load` — čita: OtpremaKontejnera, Partnerji, Pozicija, PrijemKontejneraVoz, Skladista, Voz
  - „toolStripButton1“ — klik → `tsPrvi_Click` — čita: NaruceneManipulacije
  - „toolStripButton1“ — klik → `tsNazad_Click` — čita: NaruceneManipulacije
  - „toolStripButton1“ — klik → `tsNapred_Click` — čita: NaruceneManipulacije
  - „toolStripButton1“ — klik → `tsPoslednja_Click` — čita: NaruceneManipulacije

#### frmMapa [S]
- **Fajl:** `Dokumenta/frmMapa.cs` — „Mapa prevoza“
- **Stari izgled:** Operatika › Tabla › Markeri → **frmMapa**
- **Otvara:** frmMapaOpis („Prikaži trasu“)
- **Tabele:** `Najava` S, `Partnerji` S, `Stanice` S
- **Funkcionalnosti:**
  - „Filtriraj po stanici i statusu“ — klik → `btn_StanicaStatus_Click` — čita: Najava, stanice
  - „Filtriraj po statusu“ — klik → `btn_Status_Click` — čita: Najava, Partnerji, stanice
  - „Filtriraj po partneru“ — klik → `btn_Partneri_Click` — otvara: frmMapaOpis; čita: Najava, Partnerji, stanice
  - „Prikaži trasu“ — klik → `btn_Trasa_Click` — otvara: frmMapaOpis; čita: Najava, Stanice
  - „Prikazi stanice“ — klik → `btn_SveStanice_Click`
  - „+“ — klik → `btn_ZoomIn_Click`
  - „-“ — klik → `btn_ZoomOut_Click`
  - „Detaljno“ — klik → `btn_Detaljno_Click` — otvara: frmMapaOpis
  - `map` — OnMarkerClick → `map_OnMarkerClick` — čita: Najava, stanice

#### frmMapaOpis [S]
- **Fajl:** `Dokumenta/frmMapaOpis.cs` — „Mapa opis“
- **Stari izgled:** Operatika › Tabla › Markeri → **frmMapa** → „Prikaži trasu“ → **frmMapaOpis**
- **Tabele:** `Najava` S, `Partnerji` S, `Stanice` S

#### frmMaticniPodatki [S]
- **Fajl:** `Sifarnici/frmMaticniPodatki.cs` — „Prevozna usluga“
- **Stari izgled:** Komercijala › Ugovori › Matični podaci → **frmMaticniPodatki**
- **Tabele:** `MAticniPodatki` SIUD p, `MerskeEnote` S, `ProdSkup` S
- **Procedure:** `DeleteMaticniPOdatki`, `InsertMaticniPodatki`, `UpdateMaticniPodatki`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertMaticniPodatki, UpdateMaticniPodatki; preko: InsertMaticniPodatki.InsMaticniPodatki, InsertMaticniPodatki.UpdMaticniPodatki
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteMaticniPOdatki; preko: InsertMaticniPodatki.DeleteMaticniPodatki
  - `this` — otvaranje forme → `frmMaticniPodatki_Load` — čita: MerskeEnote, ProdSkup

#### frmMedjuskladisniPrenos [S]
- **Fajl:** `Promet/frmMedjuskladisniPrenos.cs` — „Interni skladišni prenos “
- **Stari izgled:** Terminal › Skladišno poslovanje › Međuskladišni prenos → **frmMedjuskladisniPrenos**
- **Otvara:** frmNalozi („Radni nalog“), frmSredstvoRada („Nalog po sredstvima rada“), RN12MedjuskladisniKontejnera („RN Međuskladisni prenos“)
- **Tabele:** `Delavci` S, `NaruceneManipulacije` S, `NHM` S, `Partnerji` S, `Pozicija` S, `PrijemKontejneraVoz` S, `PrijemKontejneraVozStavke` S, `Promet` S, `PrometManipulacije` S, `Skladista` S, `TipKontenjera` S, `Vozila` S, `VrstaManipulacije` S
- **Funkcionalnosti:**
  - `cboSkladiste` — izbor → `cboSkladiste_SelectedValueChanged` — čita: Pozicija
  - `dataGridView1` — izbor → `dataGridView1_SelectionChanged` — čita: NaruceneManipulacije, Partnerji, PrijemKontejneraVozStavke, VrstaManipulacije
  - „?“ — klik → `btnOsvezi_Click` — čita: NHM, Pozicija, PrijemKontejneraVozStavke, Promet, Skladista, TipKontenjera
  - „>>>>“ — klik → `btnUbaci_Click`
  - „Izbriši stavku prenosa“ — klik → `button2_Click`
  - „Novi“ — klik → `tsNew_Click`
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „toolStripButton1“ — klik → `tsPrvi_Click` — čita: Promet
  - „toolStripButton1“ — klik → `tsNazad_Click` — čita: Promet
  - „toolStripButton1“ — klik → `tsNapred_Click` — čita: Promet
  - „toolStripButton1“ — klik → `tsPoslednja_Click` — čita: Promet
  - „Radni nalog“ — klik → `toolStripButton1_Click`
  - „Nalog po sredstvima rada“ — klik → `toolStripButton3_Click`
  - „>>>>“ — klik → `button1_Click`
  - „Izbriši stavku manipulacije“ — klik → `button3_Click`
  - „?“ — klik → `button4_Click` — čita: NHM, Pozicija, PrijemKontejneraVozStavke, Promet, Skladista, TipKontenjera
  - „?“ — klik → `button5_Click` — čita: NHM, Pozicija, PrijemKontejneraVozStavke, Promet, Skladista, TipKontenjera
  - „?“ — klik → `button6_Click` — čita: NHM, Pozicija, PrijemKontejneraVozStavke, Promet, Skladista, TipKontenjera
  - `this` — otvaranje forme → `frmMedjuskladisniPrenos_Load` — čita: Delavci, NHM, PArtnerji, Skladista, Vozila
  - „RN Međuskladisni prenos“ — klik → `toolStripButton2_Click`
  - „Bez zauzetih“ — čekiranje → `chkZauzete_CheckedChanged` — čita: Pozicija, Promet, Skladista

#### frmMestaUtovara [S]
- **Fajl:** `Izvoz/frmMestaUtovara.cs` — „Mesta utovara“
- **Stari izgled:** Terminal › Šifarnici › Mesta utovara → **frmMestaUtovara**
- **Otvara:** frmKontaktOsobeMU („Kontakt osobe“)
- **Tabele:** `MestaUtovara` SIUD p
- **Procedure:** `DeleteMestaUtovara`, `InsertMestaUtovara`, `UpdateMestaUtovara`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertMestaUtovara, UpdateMestaUtovara; preko: InsertMestaUtovara.InsMestaUtovara, InsertMestaUtovara.UpdMestaUtovara
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteMestaUtovara; preko: InsertMestaUtovara.DelMestaUtovara
  - „Kontakt osobe “ — klik → `toolStripButton1_Click` — otvara: frmKontaktOsobeMU
  - „Kontakt osobe“ — klik → `toolStripButton1_Click` — otvara: frmKontaktOsobeMU
  - `button20` — klik → `tsDelete_Click` — proc: DeleteMestaUtovara; preko: InsertMestaUtovara.DelMestaUtovara
  - `button21` — klik → `tsSave_Click` — proc: InsertMestaUtovara, UpdateMestaUtovara; preko: InsertMestaUtovara.InsMestaUtovara, InsertMestaUtovara.UpdMestaUtovara

#### frmMestoIsporuke [N]
- **Fajl:** `Skladista main/frmMestoIsporuke.cs` — „frmMestoIsporuke“
- **Novi izgled:** Operacije skladišta → **SkladistaMain** → kartica „Radni nalozi“ → **NalogZaKomercijalni** → „SKLADIŠNI NALOZI UVOZ“ → **PregledKomercijalnihNaloga** → „Otvori“ → **frmNalogPrijem** → „Mesto Istovara“ → **frmMestoIsporuke**
- **Tabele:** `MestaUtovara` S, `partnerjiKontOsebaMU` S, `RadniNalogInterni` S, `RadniNalogInterniPotvrda` S, `RadniNalogSkladista` S, `VlasniciRobe` S
- **Procedure:** `UpdateRadniNalogDirektniIstovar`
- **Combobox → tabela:** `cboCilj` ← `partnerjiKontOsebaMU`; `cboKontakt` ← `partnerjiKontOsebaMU`
- **Funkcionalnosti:**
  - „Snimi“ — klik → `btnSnimi_Click` — proc: UpdateRadniNalogDirektniIstovar; preko: InsertCarinskoSkladiste.UpdateRadniNalogDirektniIstovar

#### frmMUPDozvola [S]
- **Fajl:** `Dokumenta/frmMUPDozvola.cs` — „MUP Dozvola“
- **Stari izgled:** Administrator › Dozvole KOVID › MUP dozvole → **frmMUPDozvola**
- **Tabele:** `Automobili` S, `Delavci` S, `DelovnaMesta` S, `MUPDozvola` SI p, `MUPMesto` S
- **Procedure:** `InsertMUPDozvola`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertMUPDozvola; preko: InsertMUPDozvola.InsMUPDozvola
  - „Pošalji mail“ — klik → `btnPosaljiMail_Click` — čita: Delavci, MupDozvola
  - „Štampaj“ — klik → `btnStampa_Click` — izveštaj: rptMUPDozvola.rdlc
  - „Službeni“ — čekiranje → `chkSluzbeni_CheckedChanged` — čita: Automobili
  - „Stan“ — čekiranje → `chkStan_CheckedChanged` — čita: MUPMesto
  - `this` — otvaranje forme → `frmMUPDozvola_Load` — čita: Delavci

#### frmMupMesto [S]
- **Fajl:** `Dokumenta/frmMupMesto.cs` — „MUP MESTO“
- **Stari izgled:** Administrator › Dozvole KOVID › Trenutne adrese → **frmMupMesto**
- **Tabele:** `Automobili` D p, `Delavci` S, `MUPMesto` SU p
- **Procedure:** `DeleteAutomobili`, `UpdateMUPMesto`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertMUPMesto, UpdateMUPMesto; preko: InsertMUPMesto.InsMUPMesto, InsertMUPMesto.UpdMUPMesto
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteAutomobili; preko: InsertAutomobili.DeleteAutomobili

#### frmNacinDolaskaOdlaska [S]
- **Fajl:** `TrackModal/Sifarnici/frmNacinDolaskaOdlaska.cs` — „Način dolaska odlaska“
- **Stari izgled:** Terminal › Šifarnici › Način dolaska / odlaska → **frmNacinDolaskaOdlaska**
- **Tabele:** `NacinDolaskaOdlaska` S
- **Procedure:** `DeleteNacinDoOd`, `InsertNacinDoOd`, `UpdateNacinDoOd`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertNacinDoOd, UpdateNacinDoOd; preko: InsertNacinDolaskaOdlaska.InsNacinDolaskaOdlaska, InsertNacinDolaskaOdlaska.UpdNacinDolaskaOdlaska
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteNacinDoOd; preko: InsertNacinDolaskaOdlaska.DeleteNacinDolaskaOdlaska
  - „toolStripButton1“ — klik → `tsPrvi_Click` — čita: NacinDolaskaOdlaska
  - „toolStripButton1“ — klik → `tsNazad_Click` — čita: NacinDolaskaOdlaska
  - „toolStripButton1“ — klik → `tsNapred_Click` — čita: NacinDolaskaOdlaska
  - „toolStripButton1“ — klik → `tsPoslednja_Click` — čita: NacinDolaskaOdlaska
  - „>>“ — klik → `tsPoslednja_Click` — čita: NacinDolaskaOdlaska
  - „>“ — klik → `tsNapred_Click` — čita: NacinDolaskaOdlaska
  - „<“ — klik → `tsNazad_Click` — čita: NacinDolaskaOdlaska
  - „<<“ — klik → `tsPrvi_Click` — čita: NacinDolaskaOdlaska
  - `button20` — klik → `tsDelete_Click` — proc: DeleteNacinDoOd; preko: InsertNacinDolaskaOdlaska.DeleteNacinDolaskaOdlaska
  - `button21` — klik → `tsSave_Click` — proc: InsertNacinDoOd, UpdateNacinDoOd; preko: InsertNacinDolaskaOdlaska.InsNacinDolaskaOdlaska, InsertNacinDolaskaOdlaska.UpdNacinDolaskaOdlaska

#### frmNacinIsporuke [S]
- **Fajl:** `Sifarnici/frmNacinIsporuke.cs` — „Načini isporuke“
- **Stari izgled:** Komercijala › Ugovori › Načini isporuke → **frmNacinIsporuke**
- **Tabele:** `NacinDobave` SIUD p
- **Procedure:** `DeleteNacinDobave`, `InsertNacinDobave`, `UpdateNacinDobave`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertNacinDobave, UpdateNacinDobave; preko: InsertNacinDobave.InsNacinDobave, InsertNacinDobave.UpdNacinDobave
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteNacinDobave; preko: InsertNacinDobave.DelNacinDobave

#### frmNajava [S]
- **Fajl:** `Dokumenta/frmNajava.cs` — „Prevozi“
- **Stari izgled:** Komercijala › Najave › Poslovi → **frmNajava**
- **Otvara:** frmDokumentaNajava („Dokumenta posla“), frmSendEmailWithAttacment („Pošalji mail“), frmTrase („?“), frmNajavaLog („Log“), frmPronadjiVagon („Pronađi Vagon“), frmTrainList („Otvori train list“), frmRadniNalog („Prevozni nalog - Trase i Lokomotive“), frmRadniNalogIzNajave („Prevozni nalog - Trase i Lokomotive“)
- **Tabele:** `Aktivnosti` S, `AktivnostiStavke` S, `Delavci` S, `MAticniPodatki` S, `Najava` SIUD p, `NajavaDeljenjeVoza` SI p, `NajavaDodatneUsluge` SI p, `NajavaPrevoznik` S, `Narocilo` S, `NarociloPostav` S, `NHM` S, `Opportunity` S, `Partnerji` S, `RadniNalogVezaNajave` S, `Razlozi` S, `SifVucaStatusi` S, `Stanice` S, `StatusVoza` S, `Tehnologija` S, `Teretnica` S, `TeretnicaStavke` S, `TipSaobPrevoza` S, `TrainList` S, `Trase` S, `VagoniSerije` S, `VrstaAktivnosti` S, `VucaStatusi` S
- **Procedure:** `DeleteNajava`, `InsertNajava`, `InsertNajavaDeljenjeVoza`, `InsertNajavaDodatneUsluge`, `InsertNTizNajave`, `UpdateNajava`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — čita: Najava; proc: InsertNajava, UpdateNajava, sp_InsertNajavaLOG; preko: InsertNajava.InsNaj, InsertNajava.UpdNaj, InsertNajavaLog.insNajavaLog
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteNajava, sp_InsertNajavaLOG; preko: InsertNajava.DeleteNaj, InsertNajavaLog.insNajavaLog
  - „Dokumenta posla“ — klik → `toolStripButton1_Click` — otvara: frmDokumentaNajava
  - „Posalji email“ — klik → `toolStripButton2_Click` — otvara: frmSendEmailWithAttacment
  - „Štampaj izveštaj“ — klik → `toolStripButton3_Click` — izveštaj: Najava.rdlc, NajavaPrim.rdlc
  - „Pregled tekuci“ — klik → `toolStripButton4_Click`
  - „Pregled arhive“ — klik → `toolStripButton11_Click`
  - „Pronadji Vagon“ — klik → `toolStripButton5_Click`
  - „LOG“ — klik → `toolStripButton6_Click` — otvara: frmNajavaLog
  - „Excel kašnjenja“ — klik → `toolStripButton7_Click` — čita: Najava, partnerji, stanice; izvoz u Excel
  - „Otvori TrainList“ — klik → `toolStripButton8_Click` — otvara: frmTrainList
  - „Novi train list“ — klik → `toolStripButton9_Click` — otvara: frmTrainList
  - „Prevozni nalog - Trase i Lokomotive“ — klik → `toolStripButton10_Click` — otvara: frmRadniNalog, frmRadniNalogIzNajave
  - „Nosioci Troškova“ — klik → `toolStripButton12_Click` — otvara: NosiociTroskova
  - „Pozicija voza“ — klik → `toolStripButton13_Click` — otvara: LocoTrackTA
  - „RID“ — čekiranje → `chkRID_CheckedChanged`
  - „Pred pred:“ — klik → `label21_Click`
  - `tabNajave` — izbor → `tabNajave_SelectedIndexChanged` — čita: NajavaPrevoznik, Partnerji, TeretnicaStavke, stanice
  - „Dodaj“ — klik → `btnUnesi_Click` — proc: InsertNajavaDodatneUsluge; preko: InsertNajava.InsNajDodatneUSluge
  - `multiColumnComboBox2` — klik → `multiColumnComboBox2_Click` — čita: MaticniPodatki, Narocilo, NarociloPostav
  - „Refresh“ — klik → `button12_Click` — čita: MaticniPodatki, Narocilo, NarociloPostav
  - „Dodaj“ — klik → `button11_Click_2` — proc: InsertNajavaDeljenjeVoza; preko: InsertNajava.InsNajDeljenjeVoza
  - „?“ — klik → `button2_Click`
  - „AdHoc“ — čekiranje → `chkAdHoc_CheckedChanged`
  - „?“ — klik → `button4_Click` — čita: Stanice
  - „?“ — klik → `button5_Click` — čita: Stanice
  - „?“ — klik → `button6_Click` — čita: Stanice
  - „Dispečer RID:“ — klik → `label29_Click`
  - „Ima povrat“ — čekiranje → `chkImaPovrat_CheckedChanged`
  - „Iz najave“ — čekiranje → `chkIzNajave_CheckedChanged`
  - „Iz najave“ — klik → `chkIzNajave_Click`
  - `cboOpportunity` — SelectionChangeCommitted → `cboOpportunity_SelectionChangeCommitted` — čita: Opportunity
  - „?“ — klik → `button3_Click_1`
  - „Prevozni nalog - Trase i Lokomotive“ — klik → `toolStripButton10_Click` — otvara: frmRadniNalog, frmRadniNalogIzNajave
  - „Novi train list“ — klik → `toolStripButton9_Click` — otvara: frmTrainList
  - „Otvori train list“ — klik → `toolStripButton8_Click` — otvara: frmTrainList
  - „Excel kašnjenja“ — klik → `toolStripButton7_Click` — čita: Najava, partnerji, stanice; izvoz u Excel
  - „Log“ — klik → `toolStripButton6_Click` — otvara: frmNajavaLog
  - „Pronađi Vagon“ — klik → `toolStripButton5_Click`
  - … još 8

#### frmNajava20 [S]
- **Fajl:** `Dokumenta/frmNajava20.cs` — „Najave vozova“
- **Stari izgled:** Komercijala › Najave › Poslovi tabla → **frmNajava20**
- **Tabele:** `Najava` S, `Partnerji` S, `Stanice` S, `Trase` S
- **Funkcionalnosti:**
  - `timer` — Tick → `timer_Tick` — čita: Najava, Partnerji, Trase, stanice

#### frmNajavaArhivaAnaliza [S]
- **Fajl:** `SyncForm/frmNajavaArhivaAnaliza.cs` — „Najave arhiva analize“
- **Stari izgled:** Komercijala › Najave › Svi poslovi → **frmNajavaArhivaAnaliza**
- **Tabele:** `Najava` S, `Partnerji` S, `Stanice` S
- **Funkcionalnosti:**
  - `this` — otvaranje forme → `frmNajavaArhivaAnaliza_Load` — čita: Najava, Partnerji, stanice

#### frmNajavaKomitemtaDokument [N+S]
- **Fajl:** `Dokumenta/frmNajavaKomitemtaDokument.cs` — „Najava Komitenta Dokument“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Formiraj otpremu kamionom“ → **frmOtpremaKontejneraKamionomIzKontejnera** → „Otvori“ → **frmOtpremaKontejnera** → „Raspored usluga“ → **frmManipulacije** → „Nalog komitenta“ → **frmNajavaKomitemtaDokument**
- **Stari izgled:** Terminal › Terminalske usluge › Raspored manipulacija → **frmManipulacije** → „Nalog komitenta“ → **frmNajavaKomitemtaDokument**
- **Tabele:** `DokumentaNajavaKomitenta` SI p
- **Procedure:** `InsertDokumentaNajavaKomitenta`
- **Funkcionalnosti:**
  - „Sačuvaj“ — klik → `button4_Click` — proc: InsertDokumentaNajavaKomitenta; preko: InsertDokumentaNajava.InsNajavaDokumenta
  - „Otvori“ — klik → `button2_Click`
  - „Pronađi“ — klik → `button1_Click`

#### frmNajavaKomitemtaDokument [N+S]
- **Fajl:** `TrackModal/Dokumeta/frmNajavaKomitemtaDokument.cs` — „Najava Komitenta Dokument“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Voz“ → **frmPrijemIOtpremaVozova4** → „Gate out voz“ → **frmPregledOtpreme** → „Otvori voz“ → **frmOtpremaKontejnera** → „Raspored manipulacija“ → **frmManipulacije** → „Nalog komitenta“ → **frmNajavaKomitemtaDokument**
- **Stari izgled:** Terminal › Prevoz › Otprema kontejnera vozom → **frmPregledOtpreme** → „Otvori voz“ → **frmOtpremaKontejnera** → „Raspored manipulacija“ → **frmManipulacije** → „Nalog komitenta“ → **frmNajavaKomitemtaDokument**
- **Tabele:** `DokumentaNajavaKomitenta` SI p
- **Procedure:** `InsertDokumentaNajavaKomitenta`
- **Funkcionalnosti:**
  - „Sačuvaj“ — klik → `button4_Click` — proc: InsertDokumentaNajavaKomitenta; preko: InsertDokumentaNajava.InsNajavaDokumenta
  - „Otvori“ — klik → `button2_Click`
  - „Pronađi“ — klik → `button1_Click`

#### frmNajavaLog [S]
- **Fajl:** `Dokumenta/frmNajavaLog.cs` — „Log posla“
- **Stari izgled:** Komercijala › Najave › Poslovi → **frmNajava** → „Log“ → **frmNajavaLog**
- **Tabele:** `NajavaLog` S
- **Funkcionalnosti:**
  - „Proveri“ — klik → `button1_Click` — čita: NajavaLog

#### frmNajaveBezTeretnice [S]
- **Fajl:** `Dokumenta/frmNajaveBezTeretnice.cs` — „Najave bez Teretnice“
- **Stari izgled:** Komercijala › Teretnice › Teretnice bez najave → **frmNajaveBezTeretnice**
- **Tabele:** `Najava` S, `TeretnicaStavke` S
- **Funkcionalnosti:**
  - `this` — otvaranje forme → `frmNajaveBezTeretnice_Load` — čita: Najava, TeretnicaStavke

#### frmNalogPrijem [N]
- **Fajl:** `Skladista main/frmNalogPrijem.Ispravke.cs` — „frmNalogPrijem“
- **Novi izgled:** Operacije skladišta → **SkladistaMain** → kartica „Radni nalozi“ → **NalogZaKomercijalni** → „SKLADIŠNI NALOZI UVOZ“ → **PregledKomercijalnihNaloga** → „Otvori“ → **frmNalogPrijem**
- **Otvara:** frmVozac („Vozač“), frmMestoIsporuke („Mesto Istovara“), frmTipPalete („?“)
- **Tabele:** `Carinarnice` S, `InspekciskiTretman` S, `MagacinskiBrojCarinski` SIU p, `MagacinskiPredmet` S, `MerskeEnote` S, `Partnerji` S, `partnerjiKontOseba` S, `PosebniUsoviSmestaja` S, `RadniNalogInterni` S, `RadniNalogInterniPotvrda` S, `RadniNalogSkladista` S, `RNCarinskoStavke` S, `TipPalete` S, `uvNacinPakovanja` S, `Valute` S, `VlasniciRobe` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaRobeADR` S, `VrstePrevoznogSredstva` S
- **Procedure:** `DeleteCarinskoStavke`, `DeleteRadniNalogSkladisteDirektni`, `InsertCarinskoStavke`, `InsertMagacinskiBrojCarinski`, `InsertRadniNalogSkladistaDirektni`, `InsertRNInterniSkladisteDodatneUsluge`, `UpdateCarinskoStavke`, `UpdateMagacinskiBrojCarinski`, `UpdateRadniNalogSkladistaDirektni`
- **Combobox → tabela:** `cboKontakt` ← `partnerjiKontOseba`
- **Funkcionalnosti:**
  - „Izbriši“ — klik → `btnDelete_Click` — proc: DeleteRadniNalogSkladisteDirektni; preko: InsertCarinskoSkladiste.DeleteRadniNalog
  - „Pregled“ — klik → `btnPregled_Click`
  - „Potvrdi: LO nalog“ — klik → `button25_Click`
  - „Mesto Istovara“ — klik → `button2_Click` — otvara: frmMestoIsporuke
  - „Vozač“ — klik → `button1_Click` — otvara: frmVozac
  - „Snimi“ — klik → `button24_Click` — proc: InsertRNInterniSkladisteDodatneUsluge, InsertRadniNalogSkladistaDirektni, UpdateRadniNalogSkladistaDirektni; preko: InsertCarinskoSkladiste.InsertRadniNalogDirektni, InsertCarinskoSkladiste.KreirajDodatneUslugeSkladista, InsertCarinskoSkladiste.UpdateRadniNalogDirektni
  - „?“ — klik → `btnMagacinskiBroj_Click`
  - „PLANIRANI \r\nDATUM/VREME REALIZACIJE“ — klik → `label18_Click`
  - „NAZAD“ — klik → `btnMbNazad_Click`
  - „SNIMI“ — klik → `btnMbSave_Click` — čita: MagacinskiBrojCarinski; proc: InsertMagacinskiBrojCarinski, UpdateMagacinskiBrojCarinski; preko: InsertCarinskoSkladiste.InsertMagacinskiBrojCarinski, InsertCarinskoSkladiste.UpdateMagacinskiBrojCarinski
  - „Izbaci“ — klik → `btnIzbaci_Click`
  - „Dodaj“ — klik → `btnDodaj_Click`
  - „gridGroupingControl1“ — TableControlCurrentCellControlDoubleClick → `gridGroupingControl1_TableControlCurrentCellControlDoubleClick`
  - „Izbriši“ — klik → `button6_Click` — proc: DeleteCarinskoStavke; preko: InsertSkladista.DeleteCarinskoStavke
  - „Izmeni“ — klik → `btnIzmeniStavku_Click` — proc: UpdateCarinskoStavke; preko: InsertSkladista.UpdateCarinskoStavke
  - „Dodaj“ — klik → `btnDodajStavku_Click` — proc: InsertCarinskoStavke; preko: InsertSkladista.InsertCarinskoStavke
  - „?“ — klik → `btnPaleta_Click` — otvara: frmTipPalete
  - „gridGroupingControl1“ — QueryCustomSummary → `GridGroupingControl1_QueryCustomSummary`

#### frmNalogZaFakturisanje [N]
- **Fajl:** `Drumski/frmNalogZaFakturisanje.cs` — „frmNalogZaFakturisanje“
- **Novi izgled:** Drumski transport → **Drumski1** → „NALOG ZA FAKTURISANJE“ → **frmNalogZaFakturisanje**
- **Otvara:** frmBrojFaktureModal („Objedinjena dokumenta po izlaznoj fakturi“)
- **Tabele:** `Automobili` S, `Delavci` S, `DokumentaRadnogNalogaDrumski` S, `Dokumenti` S, `FakturaDrumski` S, `FakturaDrumskiStavka` SIU p, `Izvoz` S, `IzvozKonacna` S, `Korisnici` S, `MestaUtovara` S, `Partnerji` S, `PraviloArtikla` S, `ProtokolRadniNalogDrumski` S, `RadniNalogDrumski` S, `StatusVozila` S, `TipProtokola` S, `Uvoz` S, `UvozKonacna` S, `VrstaVozila` S
- **Procedure:** `InsertFakturaDrumskiStavka`, `UpdateFakturaDrumskiStavka`
- **Funkcionalnosti:**
  - „Objedinjena dokument po broju naloga“ — klik → `btnObjedinjenaDokumentaNalogID_Click` — otvara: frmBrojFaktureModal; čita: DokumentaRadnogNalogaDrumski, radniNalogDrumski
  - „Objedinjena dokumenta po izlaznoj fakturi“ — klik → `btnObjedinjenaDokumentaIFaktura_Click` — otvara: frmBrojFaktureModal; čita: DokumentaRadnogNalogaDrumski, FakturaDrumski, FakturaDrumskiStavka, radniNalogDrumski
  - „Nalozi bez izlazne fakture“ — klik → `btnBezIzlazneFakture_Click_1`
  - „Pretraži“ — klik → `button1_Click`
  - „gridGroupingControl1“ — TableControlMouseDown → `GridGroupingControl1_TableControlMouseDown`
  - „Izađi bez promena“ — klik → `btnIzadjiBezPromena_Click_1`
  - „Potvrdi izlaznu fakturu“ — klik → `btnPotvrdi_Click`
  - „Detalji“ — klik → `detaljiToolStripMenuItem_Click`

#### frmNalogZaPrevoz [N+S]
- **Fajl:** `TrackModal/Dokumeta/frmNalogZaPrevoz.cs` — „Nalog za prevoz“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Voz“ → **frmPrijemIOtpremaVozova4** → „Gate out voz“ → **frmPregledOtpreme** → „Otvori voz“ → **frmOtpremaKontejnera** → „Nalog Za Prevoz“ → **frmNalogZaPrevoz**
- **Stari izgled:** Terminal › Prevoz › Pregled nalozi za prevoz → **frmPregledNaloziZaPrevoz** → „Otvori“ → **frmNalogZaPrevoz**
- **Otvara:** frmPutniNalog („Otvori putni nalog“)
- **Tabele:** `Cene` S, `Komitenti` S, `NajavaPrevoza` S, `NajavaPrevozaTroskovi` SD p, `organizacioneJedinice` S, `VrstaManipulacije` S, `VrstaRobe` S
- **Procedure:** `DeleteNajavaPrevozaTroskovi`, `InsertNaloziZaPrevoz`, `InsertNaloziZaPrevozTrosak`, `UpdateNaloziZaprevoz`, `UpdateNaloziZaPrevozTrosak`
- **Combobox → tabela:** `cboOrganizacionaJedinica` ← `organizacioneJedinice`; `cboPlatilac` ← `Komitenti`; `cboPrimalac` ← `Komitenti`; `cboVrstaRobeKomerijala` ← `VrstaRobe`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertNaloziZaPrevoz, UpdateNaloziZaprevoz; preko: InsertNalogZaPrevoz.InsNalogZaPrevoz, InsertNalogZaPrevoz.UpdNaloziZaPrevoz
  - „Štampa Nalog za prevoz“ — klik → `toolStripLabel1_Click` — izveštaj: rptNalogZaPrevoz.rdlc
  - „Otvori putni nalog“ — klik → `toolStripLabel2_Click` — otvara: frmPutniNalog
  - „>>“ — klik → `btnUnesi_Click` — proc: InsertNaloziZaPrevozTrosak; preko: InsertNalogZaPrevoz.InsNalogZaPrevozTroskovi
  - „?“ — klik → `button1_Click` — proc: UpdateNaloziZaPrevozTrosak; preko: InsertNalogZaPrevoz.UpdNalogZaPrevozTroskovi
  - „Dokument“ — klik → `tabPage1_Click`
  - „?“ — klik → `button3_Click`
  - „Briši stavku“ — klik → `button2_Click` — proc: DeleteNajavaPrevozaTroskovi; preko: InsertNalogZaPrevoz.DelNalogZaPrevozTroskovi
  - „>>“ — klik → `button4_Click`
  - `this` — otvaranje forme → `frmNalogZaPrevoz_Load` — čita: Komitenti, OrganizacioneJedinice, VrstaManipulacije, VrstaRobe

#### frmNalogZaPrevoz [N+S]
- **Fajl:** `Dokumenta/frmNalogZaPrevoz.cs` — „Nalog za prevoz“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Formiraj otpremu kamionom“ → **frmOtpremaKontejneraKamionomIzKontejnera** → „Otvori“ → **frmOtpremaKontejnera** → „Nalog Za Prevoz“ → **frmNalogZaPrevoz**
- **Stari izgled:** Terminal › Transport › Nalozi za prevoz → **frmNalogZaPrevoz**
- **Otvara:** frmPutniNalog („Otvori putni nalog“), frmKontaktOsobe („?“)
- **Tabele:** `Cene` S, `MestaUtovara` S, `NajavaPrevoza` S, `NajavaPrevozaTroskovi` SD p, `organizacioneJedinice` S, `Partnerji` S, `VrstaManipulacije` S, `VrstaRobeADR` S
- **Procedure:** `DeleteNajavaPrevozaTroskovi`, `InsertNaloziZaPrevoz`, `InsertNaloziZaPrevozTrosak`, `UpdateNaloziZaprevoz`, `UpdateNaloziZaPrevozTrosak`
- **Combobox → tabela:** `cboOrganizacionaJedinica` ← `organizacioneJedinice`; `cboPlatilac` ← `Partnerji`; `cboPrimalac` ← `Partnerji`; `cboVrstaRobeKomerijala` ← `VrstaRobeADR`; `txtUtovarnoMesto` ← `MestaUtovara`; `txtIstovarnoMesto` ← `MestaUtovara`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertNaloziZaPrevoz, UpdateNaloziZaprevoz; preko: InsertNalogZaPrevoz.InsNalogZaPrevoz, InsertNalogZaPrevoz.UpdNaloziZaPrevoz
  - „Štampa Nalog za prevoz“ — klik → `toolStripLabel1_Click` — izveštaj: rptNalogZaPrevoz.rdlc, rptNalogZaPrevozTA.rdlc
  - „Otvori putni nalog“ — klik → `toolStripLabel2_Click` — otvara: frmPutniNalog
  - „>>“ — klik → `btnUnesi_Click` — proc: InsertNaloziZaPrevozTrosak; preko: InsertNalogZaPrevoz.InsNalogZaPrevozTroskovi
  - „?“ — klik → `button1_Click` — proc: UpdateNaloziZaPrevozTrosak; preko: InsertNalogZaPrevoz.UpdNalogZaPrevozTroskovi
  - „Dokument“ — klik → `tabPage1_Click`
  - „?“ — klik → `button3_Click`
  - „Briši stavku“ — klik → `button2_Click` — proc: DeleteNajavaPrevozaTroskovi; preko: InsertNalogZaPrevoz.DelNalogZaPrevozTroskovi
  - „>>“ — klik → `button4_Click`
  - `this` — otvaranje forme → `frmNalogZaPrevoz_Load` — čita: MestaUtovara, OrganizacioneJedinice, Partnerji, VrstaManipulacije, VrstaRobeADR

#### frmNalozi [S]
- **Fajl:** `Promet/frmNalozi.cs` — „Štampa Radnog naloga“
- **Stari izgled:** Terminal › Skladišno poslovanje › Međuskladišni prenos → **frmMedjuskladisniPrenos** → „Radni nalog“ → **frmNalozi**
- **Funkcionalnosti:**
  - „Štampaj“ — klik → `btnStampa_Click` — izveštaj: rptNalogZaRadMSP.rdlc

#### frmNamirenja [S]
- **Fajl:** `Servis/frmNamirenja.cs` — „Namirenje gorivom i uljem“
- **Stari izgled:** Održavanje › Namirenja › Obrade namirenja → **frmNamirenja**
- **Tabele:** `Delavci` S, `LokomotivaNamirenje` SD p, `LokomotivaPrijava` S, `lokomotivaVrstaNamirenja` S
- **Procedure:** `DeleteLokomotivaNamirenje`, `UpdateLokomotivaNamirenjeKol`
- **Funkcionalnosti:**
  - „Ažuriraj“ — klik → `metroButton1_Click`
  - „Brisanje zapisa“ — klik → `metroButton2_Click` — proc: DeleteLokomotivaNamirenje; preko: InsertNamirenja.DeleteNamirenja
  - „Primljeno“ — klik → `metroButton3_Click` — proc: UpdateLokomotivaNamirenjeKol; preko: InsertNamirenja.UpdateNamirenja

#### frmNamirenjaSumarno [S]
- **Fajl:** `Servis/frmNamirenjaSumarno.cs` — „Namirenja sumarno“
- **Stari izgled:** Održavanje › Namirenja › Analize → **frmNamirenjaSumarno**
- **Tabele:** `Delavci` S, `LokomotivaNamirenje` S, `LokomotivaPrijava` S, `lokomotivaVrstaNamirenja` S
- **Funkcionalnosti:**
  - `this` — otvaranje forme → `frmNamirenjaSumarno_Load` — čita: Delavci, LokomotivaNamirenje, LokomotivaPrijava, lokomotivaVrstaNamirenja

#### frmNapomenaZaPozicioniranje [S]
- **Fajl:** `Uvoz/frmNapomenaZaPozicioniranje.cs` — „Napomena za pozicioniranje“
- **Stari izgled:** Izvoz › Šifarnici › Napomene za pozicioniranje → **frmNapomenaZaPozicioniranje**
- **Tabele:** `NapomenaZaPozicioniranje` SIUD p
- **Procedure:** `DeleteNapomenaZaPozicioniranje`, `InsertNapomenaZaPozicioniranje`, `UpdateNapomenaZaPozicioniranje`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertNapomenaZaPozicioniranje, UpdateNapomenaZaPozicioniranje; preko: InsertNapomenaZaPozioniranje.InsNapomenaZaPozicioniranje, InsertNapomenaZaPozioniranje.UpdNapomenaZaPozicioniranje
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteNapomenaZaPozicioniranje; preko: InsertNapomenaZaPozioniranje.DelNapomenaZaPozicioniranje
  - `button20` — klik → `tsDelete_Click` — proc: DeleteNapomenaZaPozicioniranje; preko: InsertNapomenaZaPozioniranje.DelNapomenaZaPozicioniranje
  - `button21` — klik → `tsSave_Click` — proc: InsertNapomenaZaPozicioniranje, UpdateNapomenaZaPozicioniranje; preko: InsertNapomenaZaPozioniranje.InsNapomenaZaPozicioniranje, InsertNapomenaZaPozioniranje.UpdNapomenaZaPozicioniranje

#### frmNaslovStatusaVozila [S]
- **Fajl:** `Izvoz/frmNaslovStatusaVozila.cs` — „Naslov statusa vozila“
- **Stari izgled:** Izvoz › Šifarnici › Naslovi statusa vozila → **frmNaslovStatusaVozila**
- **Tabele:** `NaslovStatusaVozila` SIUD p
- **Procedure:** `DeleteNaslovStatusaVozila`, `InsertNaslovStatusaVozila`, `UpdateNaslovStatusaVozila`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertNaslovStatusaVozila, UpdateNaslovStatusaVozila; preko: InsertNaslovStatusaVozila.InsNaslovStatusaVozila, InsertNaslovStatusaVozila.UpdNaslovStatusaVozila
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteNaslovStatusaVozila; preko: InsertNaslovStatusaVozila.DelNaslovStatusaVozila
  - `button20` — klik → `tsDelete_Click` — proc: DeleteNaslovStatusaVozila; preko: InsertNaslovStatusaVozila.DelNaslovStatusaVozila
  - `button21` — klik → `tsSave_Click` — proc: InsertNaslovStatusaVozila, UpdateNaslovStatusaVozila; preko: InsertNaslovStatusaVozila.InsNaslovStatusaVozila, InsertNaslovStatusaVozila.UpdNaslovStatusaVozila

#### frmNeispravnostPostupak [S]
- **Fajl:** `Neispravnosti/frmNeispravnostPostupak.cs` — „Neispravnost Postupak“
- **Stari izgled:** Evidencije › Vrste aktivnosti › Kod/Opis/Postupak → **frmNeispravnostPostupak**
- **Tabele:** `SifNeispravnostPostupak` SIUD p
- **Procedure:** `DeleteSifNeispravnostPostupak`, `InsertSifNeispravnostPostupak`, `UpdateSifNeispravnostPostupak`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertSifNeispravnostPostupak, UpdateSifNeispravnostPostupak; preko: InsertNepravilnosti.InsSifNeispravnostPostupak, InsertNepravilnosti.UpdSifNeispravnostPostupak
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteSifNeispravnostPostupak; preko: InsertNepravilnosti.DelSifNeispravnostPostupak

#### frmNHM [S]
- **Fajl:** `Sifarnici/frmNHM.cs` — „NHM brojevi“
- **Stari izgled:** Osnovni podaci › Prevozi › NHM → **frmNHM**
- **Tabele:** `NHM` SIUD p, `VrstaRobeADR` S
- **Procedure:** `DeleteNHM`, `InsertNhm`, `UpdateNHM`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertNhm, UpdateNHM; preko: Insertnhm.InsNHM, Insertnhm.UpdNHM
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteNHM; preko: Insertnhm.DeleteNHM
  - `button20` — klik → `tsDelete_Click` — proc: DeleteNHM; preko: Insertnhm.DeleteNHM
  - `button21` — klik → `tsSave_Click` — proc: InsertNhm, UpdateNHM; preko: Insertnhm.InsNHM, Insertnhm.UpdNHM
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - `this` — otvaranje forme → `frmNHM_Load` — čita: VrstaRobeADR

#### frmNotifikacije [S]
- **Fajl:** `Administracija/frmNotifikacije.cs` — „Notifikacije“
- **Stari izgled:** Operatika › Komunikacija › Obaveštenja → **frmNotifikacije**
- **Tabele:** `Delavci` S, `DelovnaMesta` S, `Korisnici` S, `Notifikacije` SIUD p
- **Procedure:** `DeleteNotifikacije`, `InsertNotifikacije`, `UpdateNotifikacije`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertNotifikacije, UpdateNotifikacije; preko: InsertObavestenje.InsObavestenje, InsertObavestenje.UpdObavestenje
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteNotifikacije; preko: InsertObavestenje.DelObavestenje
  - „Obaveštenje“ — klik → `label1_Click`
  - „pretraži korisnike“ — klik → `button1_Click` — čita: Delavci, DelovnaMesta, Korisnici
  - „REFRESH“ — klik → `toolStripButton1_Click`

#### frmObjedinjenaDokumentIzlaznaFakura [S]
- **Fajl:** `Drumski/frmObjedinjenaDokumentIzlaznaFakura.cs` — „frmObjedinjenaDokumentIzlaznaFakura“
- **Stari izgled:** Drumski › Finansije › Lista naloga → **frmPregledFaktura** → „Objedinjena dokumentacija po broju izlazne fakture“ → **frmObjedinjenaDokumentIzlaznaFakura**
- **Tabele:** `Automobili` S, `Delavci` S, `DokumentaFaktureDrumski` S, `DokumentaRadnogNalogaDrumski` S, `FakturaDrumski` S, `FakturaDrumskiStavka` S, `Partnerji` S, `RadniNalogDrumski` S, `UploadedFiles` S
- **Procedure:** `DeleteDokumentFaktureDrumski`
- **Funkcionalnosti:**
  - „Preuzmi zbirni pdf“ — klik → `button1_Click`

#### frmObjekt [S]
- **Fajl:** `Sifarnici/frmObjekt.cs` — „Tip saobracajnog prevoza“
- **Stari izgled:** Komercijala › Ugovori › Tip saobraćajnog prevoza → **frmObjekt**
- **Tabele:** `TipSaobPrevoza` SIUD p
- **Procedure:** `DeleteTipSaobPrevoza`, `InsertTipSaobPrevoza`, `UpdateTipSaobPrevoza`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertTipSaobPrevoza, UpdateTipSaobPrevoza; preko: InsertTipSaobPrevoza.InsTipSaobPrevoza, InsertTipSaobPrevoza.UpdTipSaobPrevoza
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteTipSaobPrevoza; preko: InsertTipSaobPrevoza.DelTipSaobPrevoza

#### frmObracunFiksni [S]
- **Fajl:** `Dokumenta/frmObracunFiksni.cs` — „Obračun zarade“
- **Stari izgled:** Kadrovi › Zarade › Obračun fiksni → **frmObracunFiksni**
- **Tabele:** `Bolovanje` S, `Delavci` S, `Dopust` S, `DopustStavke` S, `PrekovremeniRad` S, `Zarada` S
- **Funkcionalnosti:**
  - „Povuci podatke“ — klik → `btnPostaviPrviDeo_Click` — čita: Delavci, Zarada
  - „Povuci sate“ — klik → `metroButton2_Click`

#### frmObradaUlaznihFaktura [N]
- **Fajl:** `Drumski/frmObradaUlaznihFaktura.cs` — „frmObradaUlaznihFaktura“
- **Novi izgled:** Drumski transport → **Drumski1** → „Kontrola i obračun transporta“ → **frmObradaUlaznihFaktura**
- **Tabele:** `Automobili` S, `Delavci` S, `DokumentaRadnogNalogaDrumski` S, `Dokumenti` S, `FakturaDrumski` S, `FakturaDrumskiStavka` S, `Izvoz` S, `IzvozKonacna` S, `Korisnici` S, `MestaUtovara` S, `Partnerji` S, `PraviloArtikla` S, `ProtokolRadniNalogDrumski` S, `RadniNalogDrumski` S, `StatusVozila` S, `TipProtokola` S, `Uvoz` S, `UvozKonacna` S, `VrstaVozila` S
- **Procedure:** `InsertDokumentaRadniNalogDrumski`
- **Funkcionalnosti:**
  - „Nalozi  bez ulazne fakture“ — klik → `btnBezUlazneFakture_Click`
  - „Pretraži“ — klik → `button1_Click`
  - „gridGroupingControl1“ — TableControlMouseDown → `GridGroupingControl1_TableControlMouseDown`
  - „gridGroupingControl1“ — dupli klik → `gridGroupingControl1_DoubleClick`
  - „Dodaj“ — klik → `btnDodajDokumenta_Click` — proc: InsertDokumentaRadniNalogDrumski; preko: InsertRadniNalogDrumski.SnimiUFajlBazu
  - „Izađi bez promena“ — klik → `button3_Click`
  - „Potvrdi ulaznu fakturu“ — klik → `button2_Click`
  - „Detalji“ — klik → `detaljiToolStripMenuItem_Click`

#### frmOperativniPlanIzvoz [S]
- **Fajl:** `Izvoz/frmOperativniPlanIzvoz.cs` — „frmOperativniPlanIzvoz“
- **Stari izgled:** Izvoz › Izvoz › Operativni plan → **frmOperativniPlanIzvoz**
- **Tabele:** `Automobili` S, `Izvoz` S, `IzvozKonacna` S, `IzvozKonacnaVrstaManipulacije` S, `IzvozVrstaManipulacije` S, `organizacioneJedinice` S, `RadniNalogDrumski` S, `StatusVozila` S, `VrstaManipulacije` S
- **Procedure:** `DodajStavkeURadniNalog`, `NapraviRadniNalogDrumski`
- **Funkcionalnosti:**
  - „Dopuna naloga“ — klik → `button2_Click` — proc: DodajStavkeURadniNalog; preko: InsertUvoz.UpdateRadniNalogDrumski
  - „Formirnje naloga“ — klik → `button1_Click` — proc: NapraviRadniNalogDrumski; preko: InsertUvoz.KreirajRadniNalogDrumski

#### frmOperativniPlanUvoz [S]
- **Fajl:** `Uvoz/frmOperativniPlanUvoz.cs` — „Lista naloga drumski“
- **Stari izgled:** Uvoz › Uvoz obrada › Operativni plan → **frmOperativniPlanUvoz**
- **Tabele:** `Automobili` S, `organizacioneJedinice` S, `RadniNalogDrumski` S, `StatusVozila` S, `Uvoz` S, `UvozKonacna` S, `UvozKonacnaVrstaManipulacije` S, `UvozVrstaManipulacije` S, `VrstaManipulacije` S
- **Procedure:** `DodajStavkeURadniNalog`, `NapraviRadniNalogDrumski`
- **Funkcionalnosti:**
  - „Dopuna naloga“ — klik → `button2_Click` — proc: DodajStavkeURadniNalog; preko: InsertUvoz.UpdateRadniNalogDrumski
  - „Formirnje naloga“ — klik → `button1_Click` — proc: NapraviRadniNalogDrumski; preko: InsertUvoz.KreirajRadniNalogDrumski

#### frmOpisNeispravnosti [S]
- **Fajl:** `Neispravnosti/frmOpisNeispravnosti.cs` — „Opis Neispravnosti“
- **Stari izgled:** Evidencije › Vrste aktivnosti › Opis neispravnosti → **frmOpisNeispravnosti**
- **Tabele:** `SifOpisNeispravnosti` SIUD p
- **Procedure:** `DeleteSifOpisNeispravnosti`, `InsertSifOpisNeispravnosti`, `UpdateSifOpisNeispravnosti`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertSifOpisNeispravnosti, UpdateSifOpisNeispravnosti; preko: InsertNepravilnosti.InsSifOpisNeispravnosti, InsertNepravilnosti.UpdSifOpisNeispravnosti
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteSifOpisNeispravnosti; preko: InsertNepravilnosti.DelSifOpisNeispravnosti

#### frmOrganizacionaJedinica [S]
- **Fajl:** `TrackModal/Sifarnici/frmOrganizacionaJedinica.cs` — „Organizacione jedinice“
- **Stari izgled:** Terminal › Šifarnici › Organizacione jedinice → **frmOrganizacionaJedinica**
- **Tabele:** `OrganizacionaJedinice` S, `organizacioneJedinice` S
- **Procedure:** `DeleteOrganizacionaJedinica`, `InsertOrganizacionaJedinica`, `UpdateOrganizacionaJedinica`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertOrganizacionaJedinica, UpdateOrganizacionaJedinica; preko: InsertOrganizacionaJedinica.InsOrganizaciona, InsertOrganizacionaJedinica.UpdOrganizaciona
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteOrganizacionaJedinica; preko: InsertOrganizacionaJedinica.DeleteLekarski
  - „toolStripButton1“ — klik → `tsPrvi_Click` — čita: OrganizacionaJedinice
  - „toolStripButton1“ — klik → `tsNazad_Click` — čita: OrganizacioneJedinice
  - „toolStripButton1“ — klik → `tsNapred_Click` — čita: OrganizacioneJedinice
  - „toolStripButton1“ — klik → `tsPoslednja_Click` — čita: OrganizacionaJedinice
  - „Šifra:“ — klik → `label1_Click`
  - `button20` — klik → `tsDelete_Click` — proc: DeleteOrganizacionaJedinica; preko: InsertOrganizacionaJedinica.DeleteLekarski
  - `button21` — klik → `tsSave_Click` — proc: InsertOrganizacionaJedinica, UpdateOrganizacionaJedinica; preko: InsertOrganizacionaJedinica.InsOrganizaciona, InsertOrganizacionaJedinica.UpdOrganizaciona

#### frmOsnovnaZarada [S]
- **Fajl:** `Dokumenta/frmOsnovnaZarada.cs` — „Osnovna zarada“
- **Stari izgled:** Kadrovi › Zarade › Osnovna zarada → **frmOsnovnaZarada**
- **Tabele:** `Delavci` S, `Zarada` SIUD p
- **Procedure:** `DeleteZarada`, `InsertZarada`, `PrebaciIzPlata`, `UpdateZarada`, `UpdateZaradaPostaviPrviDeo`, `UpdateZaradeDrugideoSvi`, `UpdateZaradePostaviDrugiDeo`, `UpdateZaradePrvideoSvi`, `UpdateZaradePrvideoSviMinimalna`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertZarada, UpdateZarada; preko: InsertOsnovnaZarada.InsZar, InsertOsnovnaZarada.UpdZar
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteZarada; preko: InsertOsnovnaZarada.DeleteZar
  - „Povuci iz plata“ — klik → `toolStripButton1_Click` — proc: PrebaciIzPlata; preko: InsertOsnovnaZarada.PrebaciIzPlata
  - „Prikaži samo fiksne“ — klik → `toolStripButton2_Click`
  - „Prikaži one koji nisu fiksni“ — klik → `toolStripButton3_Click`
  - „Refresh“ — klik → `btnStampa_Click`
  - „Postavi prvi deo svima isti“ — klik → `btnPostaviPrviDeo_Click` — proc: UpdateZaradaPostaviPrviDeo; preko: InsertOsnovnaZarada.PostaviPrviDeo
  - „Prebaci I deo u DelavciPL“ — klik → `metroButton1_Click` — proc: UpdateZaradePrvideoSvi; preko: InsertOsnovnaZarada.UpdateZaradePrvideoSvi
  - „Prebaci II deo u DelavciPL“ — klik → `metroButton2_Click` — proc: UpdateZaradeDrugideoSvi; preko: InsertOsnovnaZarada.UpdateZaradeDrugideoSvi
  - „Postavi drugi deo “ — klik → `metroButton3_Click` — proc: UpdateZaradePostaviDrugiDeo; preko: InsertOsnovnaZarada.PostaviDrugiDeo
  - „Sredi podatke pre arhiviranja Largo“ — klik → `metroButton4_Click`
  - „Prebaci I deo Ugovorna“ — klik → `metroButton5_Click` — proc: UpdateZaradePrvideoSviMinimalna; preko: InsertOsnovnaZarada.UpdateZaradePrvideoSviMinimalna
  - `this` — otvaranje forme → `frmOsnovnaZarada_Load` — čita: Delavci

#### frmOsoblje [S]
- **Fajl:** `Sifarnici/frmOsoblje.cs` — „Osoblje“
- **Stari izgled:** Kadrovi › Testiranje radnika › Evidencija poznavanja pruga i lokomotiva → **frmOsoblje**
- **Tabele:** `Delavci` S, `DelavciLokMasin` S, `DelavciPrugaMasin` S, `DelovnaMesta` S, `Mesta` S, `Pruga` S
- **Funkcionalnosti:**
  - „Unos lokomotive“ — ItemClicked → `toolStrip1_ItemClicked`
  - „tsSave“ — klik → `tsSave_Click`
  - „Dodeljivanje lokomotive“ — klik → `toolStripButton1_Click`
  - „Dodeljivanje pruge“ — klik → `toolStripButton2_Click`
  - `this` — otvaranje forme → `frmOsoblje_Load` — čita: Mesta, Pruga

#### frmOtpremaKontejnera [N+S]
- **Fajl:** `TrackModal/Dokumeta/frmOtpremaKontejnera.cs` — „Otprema kontejnera“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Voz“ → **frmPrijemIOtpremaVozova4** → „Gate out voz“ → **frmPregledOtpreme** → „Otvori voz“ → **frmOtpremaKontejnera**
- **Stari izgled:** Terminal › Prevoz › Otprema kontejnera vozom → **frmPregledOtpreme** → „Otvori voz“ → **frmOtpremaKontejnera**
- **Otvara:** frmDokumentaOtpremaKontejnera („Dokumenta otpreme“), frmDodatniList („Štampa dodatni list“), frmNalogZaPrevoz („Nalog Za Prevoz“), frmManipulacije („Raspored manipulacija“)
- **Tabele:** `Komitenti` S, `OtpremaKontejnera` SUD p, `OtpremaKontejneraVozStavke` SIUD p, `Predefinisaneporuke` S, `Promet` S, `Stanice` S, `StatusRobe` S, `TipKontenjera` S, `Voz` S, `VrstaRobe` S
- **Procedure:** `DeleteOtpremaKontejnera`, `DeleteOtpremaKontejneraVozStavke`, `InsertOtpremaKontejneraVozStavke`, `UpdateOtpremaKontejnera`, `UpdateOtpremaKontejneraVozStavke`, `UpdateOtpremaKontejneraVozStavkeRB`
- **Combobox → tabela:** `cboVrstaRobe` ← `VrstaRobe`; `cboPosiljalac` ← `Komitenti`; `cboPrimalac` ← `Komitenti`; `cboVlasnikKontejnera` ← `Komitenti`; `cboTipKontejnera` ← `TipKontenjera`; `cboVozBuking` ← `Voz`; `cboStatusKontejnera` ← `StatusRobe`; `cboOrganizator` ← `Komitenti`; `cboPredefinisanePoruke` ← `Predefinisaneporuke`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertOtpremaKontejnera, UpdateOtpremaKontejnera; preko: InsertOtprema.InsertOtp, InsertOtprema.UpdOtpremaKontejnera
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteOtpremaKontejnera; preko: InsertOtprema.DeleteOtpremaKontejnera
  - „Zatvaranje skladišta“ — klik → `toolStripButton1_Click`
  - „Dokumenta otpreme“ — klik → `toolStripButton2_Click` — otvara: frmDokumentaOtpremaKontejnera
  - „Pošalji mail“ — klik → `toolStripLabel2_Click` — proc: UpdateEmailOtpremaNajava, UpdateEmailOtpremaOtprema; preko: InsertOtprema.UpdateEmailOtpremaNajava, InsertOtprema.UpdateEmailOtpremaOtprema
  - „Štampa dodatni list“ — klik → `toolStripLabel1_Click`
  - „CIR“ — klik → `toolStripButton5_Click` — otvara: frmCIR
  - „Nalog Za Prevoz“ — klik → `toolStripButton3_Click` — otvara: frmNalogZaPrevoz
  - „Raspored manipulacija“ — klik → `toolStripButton4_Click` — otvara: frmManipulacije
  - „Usluge pregled“ — klik → `toolStripButton9_Click`
  - „Tovarni list“ — klik → `toolStripButton6_Click`
  - „Šifra:“ — klik → `label1_Click`
  - „Reg br vozila:“ — klik → `label4_Click`
  - „Vozom“ — čekiranje → `chkVoz_CheckedChanged`
  - „Promeni sve“ — klik → `button9_Click`
  - „Promeni RB“ — klik → `button4_Click` — proc: UpdateOtpremaKontejneraVozStavkeRB; preko: InsertPrijemKontejneraVozStavke.UpdateOtpremaKontejneraVozStavkeRB
  - „Izbriši“ — klik → `button3_Click` — proc: DeleteOtpremaKontejneraVozStavke; preko: InsertOtpremaKontejneraStavke.DeleteOtpremaKontejneraVozStav
  - „Promeni“ — klik → `button2_Click` — proc: UpdateOtpremaKontejneraVozStavke; preko: InsertOtpremaKontejneraStavke.UpdOtpremaKontejneraVozStav
  - „Ubaci“ — klik → `button1_Click` — čita: OtpremaKontejneraVozStavke, Promet; proc: InsertOtpremaKontejneraVozStavke; preko: InsertOtpremaKontejneraStavke.InsertOtpremaKontejneraStav
  - „?“ — klik → `button6_Click`
  - „?“ — klik → `button5_Click`
  - „Tip kontejnera:“ — klik → `label11_Click`
  - „Obriši“ — klik → `btnObrisiPostupak_Click`
  - „Dodaj“ — klik → `btnDodajPostupak_Click`
  - `this` — otvaranje forme → `frmOtpremaKontejnera_Load` — čita: Komitenti, PredefinisanePoruke, StatusRobe, TipKontenjera, Voz, VrstaRobe

#### frmOtpremaKontejnera [N+S]
- **Fajl:** `Dokumenta/frmOtpremaKontejnera.cs` — „Otprema kontejnera“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Formiraj otpremu kamionom“ → **frmOtpremaKontejneraKamionomIzKontejnera** → „Otvori“ → **frmOtpremaKontejnera**
- **Stari izgled:** Izvoz › Zadaci › Pregled radnih naloga → **frmRadniNalogInterniPregled** → „Formiraj otpremu kamionom“ → **frmOtpremaKontejneraKamionomIzKontejnera** → „Otvori“ → **frmOtpremaKontejnera**
- **Otvara:** frmDokumentaOtpremaKontejnera („Dokumenta otpreme“), frmDodatniList („Štampa dodatni list“), frmCIR („CIR“), frmNalogZaPrevoz („Nalog Za Prevoz“), frmManipulacije („Raspored usluga“), frmTovarniList („Tovarni list“), frmPregledNarucenihManipulacija („Usluge pregled“)
- **Tabele:** `NHM` S, `OtpremaKontejnera` S, `OtpremaKontejneraVozStavke` S, `Partnerji` S, `PostupakStavkeOtpremaVeza` S, `Predefinisaneporuke` S, `Promet` S, `Stanice` S, `StatusRobe` S, `TipKontenjera` S, `Voz` S, `VrstaRobe` S, `VrstePostupakaUvoz` S
- **Procedure:** `DeletePostupakStavkeOtprema`, `InsertPostupakStavkeOtprema`
- **Combobox → tabela:** `cboVrstaRobe` ← `NHM`; `cboPosiljalac` ← `Partnerji`; `cboPrimalac` ← `Partnerji`; `cboVlasnikKontejnera` ← `Partnerji`; `cboTipKontejnera` ← `TipKontenjera`; `cboVozBuking` ← `Voz`; `cboStatusKontejnera` ← `StatusRobe`; `cboOrganizator` ← `Partnerji`; `cboPredefinisanaPoruka` ← `Predefinisaneporuke`; `cboPostupak` ← `VrstePostupakaUvoz`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click`
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „Zatvaranje skladišta“ — klik → `toolStripButton1_Click`
  - „Dokumenta otpreme“ — klik → `toolStripButton2_Click`
  - „Pošalji mail“ — klik → `toolStripLabel2_Click`
  - „Štampa dodatni list“ — klik → `toolStripLabel1_Click`
  - „CIR“ — klik → `toolStripButton5_Click`
  - „Nalog Za Prevoz“ — klik → `toolStripButton3_Click`
  - „Raspored manipulacija“ — klik → `toolStripButton4_Click`
  - „Usluge pregled“ — klik → `toolStripButton9_Click`
  - „Tovarni list“ — klik → `toolStripButton6_Click`
  - „Šifra:“ — klik → `label1_Click`
  - „Reg br vozila:“ — klik → `label4_Click`
  - „Vozom“ — čekiranje → `chkVoz_CheckedChanged`
  - „Promeni sve“ — klik → `button9_Click`
  - „Promeni RB“ — klik → `button4_Click`
  - „Izbriši“ — klik → `button3_Click`
  - „Promeni“ — klik → `button2_Click`
  - „Ubaci“ — klik → `button1_Click`
  - „?“ — klik → `button6_Click`
  - „?“ — klik → `button5_Click`
  - „Tip kontejnera:“ — klik → `label11_Click`
  - „Obriši“ — klik → `btnObrisiPostupak_Click` — proc: DeletePostupakStavkeOtprema; preko: InsertPrijemKontejneraVoz.DeletePostupakStavkeOtprema
  - „Dodaj“ — klik → `btnDodajPostupak_Click` — proc: InsertPostupakStavkeOtprema; preko: InsertPrijemKontejneraVoz.InsertPostupakStavkeOtprema
  - `this` — otvaranje forme → `frmOtpremaKontejnera_Load` — čita: Partnerji, PredefinisanePoruke, StatusRobe, Voz, VrstePostupakaUvoz

#### frmOtpremaKontejneraIzvozKamion [N+S]
- **Fajl:** `Dokumenta/frmOtpremaKontejneraIzvozKamion.cs` — „GATE OUT KAMION“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Formiraj otpremu kamionom“ → **frmOtpremaKontejneraKamionomIzKontejnera** → „Otvori“ → **frmOtpremaKontejneraIzvozKamion**
- **Stari izgled:** Terminal › Radni nalozi › Otprema kontejenera Kamion - N → **frmOtpremaKontejneraIzvozKamion**
- **Otvara:** frmDokumentaOtpremaKontejnera („Dokumenta otpreme“), RN6OtpremaPlatforme („Radni nalog Otprema“), RN7OtpremaPlatforme2 („Radni nalozi BRODAR“)
- **Tabele:** `InspekciskiTretman` S, `IzvozKonacna` S, `IzvozKonacnaVrstaManipulacije` S, `Komitenti` S, `organizacioneJedinice` S, `OtpremaKontejnera` SUD p, `OtpremaKontejneraVozStavke` SIUD p, `Partnerji` S, `Promet` S, `RadniNalogInterni` S, `RNOtpremaCirade` S, `RNPrijemVoza` S, `Skladista` S, `TipKontenjera` S, `Voz` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaRobe` S, `VrstaRobeADR` S
- **Procedure:** `DeleteOtpremaKontejnera`, `DeleteOtpremaKontejneraVozStavke`, `InsertOtpremaKontejneraVozStavke`, `UpdateOtpremaKontejnera`, `UpdateOtpremaKontejneraVozStavke`
- **Combobox → tabela:** `cboPosiljalac` ← `Partnerji`; `cboPrimalac` ← `Partnerji`; `cboVlasnikKontejnera` ← `Partnerji`; `cboTipKontejnera` ← `TipKontenjera`; `cboStatusKontejnera` ← `InspekciskiTretman`; `cboOrganizator` ← `Partnerji`; `cboIzvoznik` ← `Partnerji`; `txtADR` ← `VrstaRobeADR`
- **Funkcionalnosti:**
  - „Manipulacije kontejnerom“ — ItemClicked → `btnManipulacija_ItemClicked`
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertOtpremaKontejnera, UpdateOtpremaKontejnera; preko: InsertOtprema.InsertOtp, InsertOtprema.UpdOtpremaKontejnera
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteOtpremaKontejnera; preko: InsertOtprema.DeleteOtpremaKontejnera
  - „Zatvaranje skladišta“ — klik → `toolStripButton1_Click`
  - „DOKUMENTA OTPREME“ — klik → `toolStripButton2_Click` — otvara: frmDokumentaOtpremaKontejnera
  - „CIR“ — klik → `toolStripButton5_Click`
  - „RN OTPREMU PLAT BRODAR“ — klik → `toolStripButton6_Click`
  - „RN NALOG OTPREMU PLATFORMA“ — klik → `toolStripButton8_Click`
  - „USLUGE RASPORED“ — klik → `toolStripButton4_Click`
  - „REG BR VOZILA“ — klik → `label4_Click`
  - „IME VOZAČA“ — klik → `label5_Click`
  - „ATD:“ — klik → `label18_Click`
  - „STATUS“ — klik → `label2_Click`
  - „?“ — klik → `button6_Click`
  - „Promeni RB“ — klik → `button4_Click`
  - „Izbriši“ — klik → `button3_Click` — proc: DeleteOtpremaKontejneraVozStavke; preko: InsertOtpremaKontejneraStavke.DeleteOtpremaKontejneraVozStav
  - „Promeni“ — klik → `button2_Click` — proc: UpdateOtpremaKontejneraVozStavke; preko: InsertOtpremaKontejneraStavke.UpdOtpremaKontejneraVozStav
  - „Ubaci“ — klik → `button1_Click` — čita: OtpremaKontejneraVozStavke, Promet; proc: InsertOtpremaKontejneraVozStavke; preko: InsertOtpremaKontejneraStavke.InsertOtpremaKontejneraStav
  - „Radni nalog Otprema“ — klik → `toolStripButton8_Click`
  - „Radni nalozi BRODAR“ — klik → `toolStripButton6_Click`
  - „Dokumenta otpreme“ — klik → `toolStripButton2_Click` — otvara: frmDokumentaOtpremaKontejnera
  - `button20` — klik → `tsDelete_Click` — proc: DeleteOtpremaKontejnera; preko: InsertOtprema.DeleteOtpremaKontejnera
  - `button21` — klik → `tsSave_Click` — proc: InsertOtpremaKontejnera, UpdateOtpremaKontejnera; preko: InsertOtprema.InsertOtp, InsertOtprema.UpdOtpremaKontejnera

#### frmOtpremaKontejneraKamionomIzKontejnera [N+S]
- **Fajl:** `Izvoz/frmOtpremaKontejneraKamionomIzKontejnera.cs` — „Otprema kontejnera kamionom - FORMIRANJE“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Formiraj otpremu kamionom“ → **frmOtpremaKontejneraKamionomIzKontejnera**
- **Stari izgled:** Izvoz › Zadaci › Pregled radnih naloga → **frmRadniNalogInterniPregled** → „Formiraj otpremu kamionom“ → **frmOtpremaKontejneraKamionomIzKontejnera**
- **Otvara:** RN6OtpremaPlatforme (`button21` (Click)), frmOtpremaKontejneraIzvozKamion („Otvori“), frmOtpremaKontejneraUvozKamion („Otvori“), frmOtpremaKontejnera („Otvori“), RN7OtpremaPlatforme2 (metoda `PrenesiRN7`)
- **Tabele:** `IzvozKonacna` S, `OtpremaKontejnera` S, `RadniNalogInterni` S, `UvozKonacna` S, `VoziloUsluga` S
- **Procedure:** `spPrenesiKontejnerUOtpremuKamionom`, `spPrenesiKontejnerUOtpremuKamionomIzvoz`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: spPrenesiKontejnerUOtpremuKamionom, spPrenesiKontejnerUOtpremuKamionomIzvoz; preko: InsertIzvozKonacna.PrenesiKontejnerUOtpremuKamionomIzvoz, InsertIzvozKonacna.PrenesiKontejnerUOtpremuKamionomUvoz
  - „OTVORI “ — klik → `toolStripButton1_Click`
  - „Otvori“ — klik → `toolStripButton1_Click`
  - `button21` — klik → `tsSave_Click` — proc: spPrenesiKontejnerUOtpremuKamionom, spPrenesiKontejnerUOtpremuKamionomIzvoz; preko: InsertIzvozKonacna.PrenesiKontejnerUOtpremuKamionomIzvoz, InsertIzvozKonacna.PrenesiKontejnerUOtpremuKamionomUvoz

#### frmOtpremaKontejneraLegetIZVOZ [N+S]
- **Fajl:** `Dokumenta/frmOtpremaKontejneraLegetIZVOZ.cs` — „GATE OUT VOZ - IZVOZ“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Potvrdi“ → **frmOtpremaVozaIzPlana** → „Otvori“ → **frmOtpremaKontejneraLegetIZVOZ**
- **Stari izgled:** Terminal › Radni nalozi › Otprema kontejnera voz - N → **frmOtpremaKontejneraLegetIZVOZ**
- **Otvara:** frmDokumentaOtpremaKontejnera („Dokumenta otpreme“), frmDodatniList („Štampa dodatni list“), frmPovezivanjeKontejneraIVagona („Poveži vagone“)
- **Tabele:** `Carinarnice` S, `DirigacijaKontejneraZa` S, `InspekciskiTretman` S, `IzvozKonacna` S, `IzvozKonacnaNapomenePozicioniranja` S, `IzvozKonacnaNHM` S, `IzvozKonacnaVrstaManipulacije` S, `Komitenti` S, `KontejnerskiTerminali` S, `KrajnjaDestinacija` S, `NHM` S, `organizacioneJedinice` S, `OtpremaKontejnera` SUD p, `OtpremaKontejneraVozStavke` SIUD p, `Partnerji` S, `Predefinisaneporuke` S, `RNOtpremaVoza` S, `Skladista` S, `TipKontenjera` S, `Voz` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaRobe` S, `VrstaRobeADR` S, `VrstePostupakaUvoz` S
- **Procedure:** `DeleteOtpremaKontejnera`, `DeleteOtpremaKontejneraVozStavke`, `InsertOtpremaKontejneraVozStavke`, `UpdateOtpremaKontejnera`, `UpdateOtpremaKontejneraVozStavke`
- **Combobox → tabela:** `cboTipKontejnera` ← `TipKontenjera`; `cboVozBuking` ← `Voz`; `cboOperater` ← `Partnerji`; `cboVlasnikKontejnera` ← `Partnerji`; `cboStatusKontejnera` ← `DirigacijaKontejneraZa`; `cboPostupak` ← `VrstePostupakaUvoz`; `cboKrajnjaDestinacija` ← `KrajnjaDestinacija`; `cboCarina` ← `Carinarnice`; `cboSpedicija` ← `Partnerji`; `cboReexport` ← `VrstaCarinskogPostupka`; `txtADR` ← `VrstaRobeADR`; `cboNalogodavac1` ← `Partnerji`; `cboNalogodavac2` ← `Partnerji`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertOtpremaKontejnera, UpdateOtpremaKontejnera; preko: InsertOtprema.InsertOtp, InsertOtprema.UpdOtpremaKontejnera
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteOtpremaKontejnera; preko: InsertOtprema.DeleteOtpremaKontejnera
  - „Zatvaranje skladišta“ — klik → `toolStripButton1_Click`
  - „DOKUMENTA OTPREME“ — klik → `toolStripButton2_Click` — otvara: frmDokumentaOtpremaKontejnera
  - „RADNI NALOZI“ — klik → `toolStripDropDownButton1_Click`
  - „OTPREMA VOZA“ — klik → `rasdniNalogOtpremaVozaToolStripMenuItem_Click`
  - „PREGLEDI I POSTAVKA KONTEJNERA“ — klik → `pREGLEDIIPOSTAVKAKONTEJNERAToolStripMenuItem_Click`
  - „CIR“ — klik → `toolStripButton5_Click`
  - „POVEŽI VAGONE“ — klik → `toolStripButton6_Click_1` — otvara: frmPovezivanjeKontejneraIVagona
  - „PRIPREMA VOZA ZA OTPREMU“ — klik → `toolStripButton7_Click`
  - „ŠTAPMA DODATNI LIST“ — klik → `toolStripLabel1_Click`
  - „RASPORED USLUGA“ — klik → `toolStripButton4_Click`
  - „?“ — klik → `button6_Click`
  - „?“ — klik → `button5_Click`
  - „Promeni RB“ — klik → `button4_Click`
  - „Izbriši“ — klik → `button3_Click` — proc: DeleteOtpremaKontejneraVozStavke; preko: InsertOtpremaKontejneraStavke.DeleteOtpremaKontejneraVozStav
  - „Promeni“ — klik → `button2_Click` — proc: UpdateOtpremaKontejneraVozStavke; preko: InsertOtpremaKontejneraStavke.UpdOtpremaKontejneraVozStav
  - „Ubaci“ — klik → `button1_Click` — proc: InsertOtpremaKontejneraVozStavke; preko: InsertOtpremaKontejneraStavke.InsertOtpremaKontejneraStav
  - „Poveži vagone“ — klik → `toolStripButton6_Click_1` — otvara: frmPovezivanjeKontejneraIVagona
  - „Štampa dodatni list“ — klik → `toolStripLabel1_Click`
  - „Radni nalozi “ — klik → `toolStripDropDownButton1_Click`
  - „Dokumenta otpreme“ — klik → `toolStripButton2_Click` — otvara: frmDokumentaOtpremaKontejnera
  - `button20` — klik → `tsDelete_Click` — proc: DeleteOtpremaKontejnera; preko: InsertOtprema.DeleteOtpremaKontejnera
  - `button21` — klik → `tsSave_Click` — proc: InsertOtpremaKontejnera, UpdateOtpremaKontejnera; preko: InsertOtprema.InsertOtp, InsertOtprema.UpdOtpremaKontejnera
  - `this` — otvaranje forme → `frmOtpremaKontejneraLegetIZVOZ_Load` — čita 9 tabela

#### frmOtpremaKontejneraUvozKamion [N+S]
- **Fajl:** `Dokumenta/frmOtpremaKontejneraUvozKamion.cs` — „GATE OUT KAMION - UVOZ“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Formiraj otpremu kamionom“ → **frmOtpremaKontejneraKamionomIzKontejnera** → „Otvori“ → **frmOtpremaKontejneraUvozKamion**
- **Stari izgled:** Terminal › Radni nalozi › Otprema kontejnera Kamion - N → **frmOtpremaKontejneraUvozKamion**
- **Otvara:** frmDokumentaOtpremaKontejnera („Dokumenta otpreme“), frmCIR („CIR“), frmLagerOperater („MAPA SKLADIŠTA“)
- **Tabele:** `DirigacijaKontejneraZa` S, `Komitenti` S, `KontejnerStatus` S, `NHM` S, `organizacioneJedinice` S, `OtpremaKontejnera` S, `OtpremaKontejneraVozStavke` S, `Partnerji` S, `Predefinisaneporuke` S, `PrijemKontejneraVoz` S, `PrijemKontejneraVozStavke` S, `RadniNalogInterni` S, `RNOtpremaCirade` S, `RNOtpremaPlatforme` S, `Stanice` S, `TipKontenjera` S, `uvNacinPakovanja` S, `UvozKonacna` S, `UvozKonacnaNapomenePozicioniranja` S, `UvozKonacnaNHM` S, `UvozKonacnaVrstaManipulacije` S, `UvozKonacnaZaglavlje` S, `Voz` S, `VrstaManipulacije` S, `VrstaRobe` S
- **Combobox → tabela:** `cboVrstaRobe` ← `VrstaRobe`; `cboPosiljalac` ← `Partnerji`; `cboPrimalac` ← `Partnerji`; `cboVlasnikKontejnera` ← `Partnerji`; `cboTipKontejnera` ← `TipKontenjera`; `cboStatusKontejnera` ← `DirigacijaKontejneraZa`; `cboOrganizator` ← `Partnerji`; `cbPostupak` ← `uvNacinPakovanja`; `cboUvoznik` ← `Partnerji`; `cboSpedicijaLeget` ← `Partnerji`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click`
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „Zatvaranje skladišta“ — klik → `toolStripButton1_Click`
  - „DOKUMENTA OTPREME“ — klik → `toolStripButton2_Click`
  - „POŠALJI MAIL“ — klik → `toolStripLabel2_Click`
  - „Radni nalozi“ — klik → `toolStripDropDownButton1_Click`
  - „OTPREMA PLATFORME“ — klik → `oTPREMAPLATFORMEToolStripMenuItem_Click`
  - „OTPREMA PLATFORME - BRODAR “ — klik → `oTPREMAPLATFORMEToolStripMenuItem1_Click`
  - „OTPREMA CIRADE“ — klik → `oTPREMACIRADEToolStripMenuItem_Click`
  - „CIR“ — klik → `toolStripButton5_Click`
  - „Promeni“ — klik → `button8_Click`
  - „MAPA SKLADIŠTA“ — klik → `button7_Click`
  - „Izbriši“ — klik → `button3_Click`
  - „Promeni“ — klik → `button2_Click`
  - „Ubaci“ — klik → `button1_Click`
  - „CIRADA“ — čekiranje → `chkCirada_CheckedChanged`
  - „Terminal“ — čekiranje → `chkTerminal_CheckedChanged`
  - „PLATFORMA“ — čekiranje → `chkPlatforma_CheckedChanged`
  - „CIR“ — klik → `toolStripButton5_Click`
  - „Radni nalozi“ — klik → `toolStripDropDownButton1_Click`
  - „Dokumenta otpreme“ — klik → `toolStripButton2_Click`
  - „Pošalji mail“ — klik → `toolStripLabel2_Click`
  - `this` — otvaranje forme → `frmOtpremaKontejneraUvozKamion_Load` — čita: DirigacijaKontejneraZa, Partnerji, TipKontenjera, VrstaRobe, uvNacinPakovanja

#### frmOtpremaVozaIzPlana [N+S]
- **Fajl:** `Izvoz/frmOtpremaVozaIzPlana.cs` — „Formiranje otpreme voza iz plana“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Potvrdi“ → **frmOtpremaVozaIzPlana**
- **Stari izgled:** Izvoz › Zadaci › Pregled radnih naloga → **frmRadniNalogInterniPregled** → „Potvrdi“ → **frmOtpremaVozaIzPlana**
- **Otvara:** RN2OtpremaVoza („Prenesi stavke iz plana“), frmOtpremaKontejneraLegetIZVOZ („Otvori“), frmOtpremaKontejnera („Otvori“)
- **Tabele:** `IzvozKonacna` S, `IzvozKonacnaZaglavlje` S, `OtpremaKontejnera` S, `OtpremaKontejneraVozStavke` S, `Partnerji` S, `RadniNalogInterni` S, `TipKontenjera` S, `UvozKonacna` S, `Voz` S, `VrstaRobe` S
- **Procedure:** `spPrenesiIzPlanUtovaraUOtpremaVoz`
- **Combobox → tabela:** `cboPlanUtovara` ← `IzvozKonacnaZaglavlje`, `Voz`; `cboVozBuking` ← `Voz`; `cboOperater` ← `Partnerji`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click`
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „OTVORI VOZ“ — klik → `toolStripButton1_Click`
  - „Voz iz plana“ — klik → `button2_Click`
  - „Prenesi stavke iz plana“ — klik → `button1_Click` — proc: spPrenesiIzPlanUtovaraUOtpremaVoz; preko: InsertIzvozKonacna.PrenesiIzPlanUtovaraUOtpremaVoz
  - „Otvori“ — klik → `toolStripButton1_Click`
  - `this` — otvaranje forme → `frmOtpremaVozaIzPlana_Load` — čita: IzvozKonacnaZaglavlje, Partnerji, Voz

#### frmOtpremnicaCarinskaStampa [S]
- **Fajl:** `Carinsko/frmOtpremnicaCarinskaStampa.cs` — „Otpremnica štampa“
- **Stari izgled:** Carinsko › Magacinsko poslovanje › Pregled Otpremnica → **frmOtpremnicaCarinskoTabela** → „Novi“ → **frmOtpremnicaCarinsko** → „Štampa otpremnice“ → **frmOtpremnicaCarinskaStampa**
- **Funkcionalnosti:**
  - „Štampa “ — klik → `button4_Click` — izveštaj: rptOtpremnicaCarinska.rdlc

#### frmOtpremnicaCarinsko [S]
- **Fajl:** `Carinsko/frmOtpremnicaCarinsko.cs` — „Otpremnica carinsko“
- **Stari izgled:** Carinsko › Magacinsko poslovanje › Pregled Otpremnica → **frmOtpremnicaCarinskoTabela** → „Novi“ → **frmOtpremnicaCarinsko**
- **Otvara:** frmPrijemnicaStavke (`button3` (Click)), frmOtpremnicaCarinskaStampa („Štampa otpremnice“)
- **Tabele:** `MagacinskiBrojevi` S, `MerskeEnote` S, `OtpremnicaCarinska` SIUD p, `OtpremnicaCarinskaStavke` S, `Partnerji` S, `Pozicija` S, `PrijemnicaCarinska` S, `Promet` S, `Skladista` S, `Valute` S
- **Procedure:** `DeleteOtpremnicaCarinska`, `InsertOtpremnicaCarinska`, `UpdateOtpremnicaCarinska`, `UpdatePrijemnicaCarinskaStatus`, `updatePrijemnicaCarinskaStavkaKolicina`
- **Funkcionalnosti:**
  - „Štampa otpremnice“ — klik → `button4_Click_1` — otvara: frmOtpremnicaCarinskaStampa
  - „Otvori“ — klik → `button5_Click` — čita: Promet; proc: DeletePromet, UpdatePrijemnicaCarinskaStatus, updatePrijemnicaCarinskaStavkaKolicina; preko: InsertIsporuka.DeletePromet, InsertPrijemnicaCarinska.updetePrijemnicaCarinskaStatus, insertOtpremnicaCarinsko.updetePrijemnicaCarinskaStavkaKolicina
  - „Proknjiži“ — klik → `button2_Click` — čita: Promet; proc: InsertPromet, UpdateOtpremnicaCarinskaStatus, updatePrijemnicaCarinskaStavkaKolicina; preko: InsertIsporuka.InsertPromet, insertOtpremnicaCarinsko.updeteOtpremnicaCarinskaStatus, insertOtpremnicaCarinsko.updetePrijemnicaCarinskaStavkaKolicina
  - `button20` — klik → `button20_Click` — proc: DeleteOtpremnicaCarinska; preko: insertOtpremnicaCarinsko.DelOtpremnicaCarinsko
  - `button21` — klik → `button21_Click` — proc: InsertOtpremnicaCarinska, UpdateOtpremnicaCarinska; preko: insertOtpremnicaCarinsko.InsOtpremnicaCarinska, insertOtpremnicaCarinsko.updOtpremnicaCarinska
  - `button3` — klik → `button3_Click` — otvara: frmPrijemnicaStavke

#### frmOtpremnicaCarinskoTabela [S]
- **Fajl:** `Carinsko/frmOtpremnicaCarinskoTabela.cs` — „Otpremnice tabela“
- **Stari izgled:** Carinsko › Magacinsko poslovanje › Pregled Otpremnica → **frmOtpremnicaCarinskoTabela**
- **Otvara:** frmOtpremnicaCarinsko („Novi“)
- **Tabele:** `OtpremnicaCarinska` S, `Partnerji` S, `Skladista` S
- **Funkcionalnosti:**
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - „Refresh“ — klik → `button25_Click`
  - „Otvori“ — klik → `button24_Click` — otvara: frmOtpremnicaCarinsko
  - „Novi“ — klik → `button23_Click` — otvara: frmOtpremnicaCarinsko

#### frmParametriObracuna [S]
- **Fajl:** `Dokumenta/frmParametriObracuna.cs` — „Parametri obračuna“
- **Stari izgled:** Kadrovi › Zarade › Parametri obrčuna → **frmParametriObracuna**
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click`

#### frmPartnerji [N+S]
- **Fajl:** `Sifarnici/frmPartnerji.cs` — „ PARTNERI“
- **Novi izgled:** Podešavanja sistema → **Podesavanje1** → „Partneri“ → **frmPartnerji**
- **Stari izgled:** Osnovni podaci › Partneri › Partneri → **frmPartnerji**
- **Otvara:** frmKontaktOsobe („Kontakt osobe“), frmPartnerjiFirme („Firme“)
- **Tabele:** `Drzave` SI p, `Korisnici` S, `Partnerji` SUD p, `PartnerjiFirma` S, `partnerjiKontOseba` S, `Poste` SI p, `Valute` S
- **Procedure:** `DeletePartnerji`, `InsertDrzave`, `InsertParnerji`, `InsertPoste`, `UpdatePartneri`
- **Combobox → tabela:** `cboValuta` ← `Valute`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click_1` — čita: Korisnici; proc: InsertParnerji, UpdatePartneri; preko: InsertPartnerji.InsPartneri, InsertPartnerji.UpdPartneri
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeletePartnerji; preko: InsertPartnerji.DelPartneri
  - „Kontakt osobe“ — klik → `toolStripButton1_Click`
  - „Export Pantheon“ — klik → `toolStripButton2_Click` — piše: Partnerji[U]; čita: Drzave; proc: InsertApiLogovi; preko: InsertPatheonExport.InsApiLog
  - „button1“ — klik → `btnDrzava_Click`
  - „Unesi poštu“ — klik → `button4_Click`
  - „Unesi državu“ — klik → `button3_Click`
  - „Unesi poštu“ — klik → `button5_Click` — proc: InsertPoste; preko: InsertPartnerji.InsPoste
  - „Mesto“ — klik → `label30_Click`
  - „Unesi državu“ — klik → `button2_Click` — proc: InsertDrzave; preko: InsertPartnerji.InsDrzave
  - „Potvrdi“ — klik → `button1_Click_1`
  - „--“ — klik → `btnMin_Click`
  - „Firme“ — klik → `btnFirme_Click`
  - „Kontakt osobe“ — klik → `toolStripButton1_Click`
  - `button20` — klik → `tsDelete_Click` — proc: DeletePartnerji; preko: InsertPartnerji.DelPartneri
  - `button21` — klik → `tsSave_Click_1` — čita: Korisnici; proc: InsertParnerji, UpdatePartneri; preko: InsertPartnerji.InsPartneri, InsertPartnerji.UpdPartneri
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - `this` — otvaranje forme → `frmPartnerji_Load` — čita: Valute

#### frmPartnerjiFirme [N+S]
- **Fajl:** `Sifarnici/frmPartnerjiFirme.cs` — „APP šifre partnera“
- **Novi izgled:** Podešavanja sistema → **Podesavanje1** → „Partneri“ → **frmPartnerji** → „Firme“ → **frmPartnerjiFirme**
- **Stari izgled:** Osnovni podaci › Partneri › Partneri → **frmPartnerji** → „Firme“ → **frmPartnerjiFirme**
- **Tabele:** `Partnerji` S, `PartnerjiFirma` S
- **Combobox → tabela:** `txtPartnerID` ← `Partnerji`
- **Funkcionalnosti:**
  - „Sve firme“ — klik → `button23_Click`
  - `this` — otvaranje forme → `frmPartnerjiFirme_Load` — čita: Partnerji

#### frmPlacenoNeplaceno [S]
- **Fajl:** `Dokumenta/frmPlacenoNeplaceno.cs` — „Plaćeno Neplaćeno“
- **Stari izgled:** Kadrovi › Zarade › Plaćeno/Neplaćeno → **frmPlacenoNeplaceno**
- **Tabele:** `Delavci` S
- **Funkcionalnosti:**
  - „Nije plaćeno“ — klik → `btnStampa_Click` — izveštaj: rptPlacenoNeplaceno.rdlc
  - „Plaćeno“ — klik → `button1_Click` — izveštaj: rptPlacenoTrue.rdlc
  - `this` — otvaranje forme → `frmPlacenoNeplaceno_Load` — čita: Delavci

#### frmPlanoviIzvoza [S]
- **Fajl:** `Izvoz/frmPlanoviIzvoza.cs` — „Planovi izvoza“
- **Stari izgled:** Izvoz › Izvoz › Planovi utovara / pakovanja voza → **frmPlanoviIzvoza**
- **Otvara:** frmIzvozKonacna („Otvori“), frmIzvozKonacnaZaglavlje („Novi plan“), frmIzvoz („Terminal novi kontejner“)
- **Tabele:** `IzvozKonacnaZaglavlje` S, `Stanice` S, `Voz` S
- **Funkcionalnosti:**
  - „Refresh“ — klik → `toolStripButton1_Click`
  - „Otvori“ — klik → `toolStripButton2_Click` — otvara: frmIzvozKonacna
  - „Novi plan izvoza“ — klik → `toolStripButton3_Click` — otvara: frmIzvozKonacnaZaglavlje
  - „Nov kontejner - Terminal“ — klik → `toolStripButton4_Click` — otvara: frmIzvoz
  - „Terminal novi kontejner“ — klik → `toolStripButton4_Click` — otvara: frmIzvoz
  - „Novi plan“ — klik → `toolStripButton3_Click` — otvara: frmIzvozKonacnaZaglavlje
  - „Otvori“ — klik → `toolStripButton2_Click` — otvara: frmIzvozKonacna
  - „Refresh“ — klik → `toolStripButton1_Click`

#### frmPlanRada [S]
- **Fajl:** `SyncForm/frmPlanRada.cs` — „Meni plana rada“
- **Stari izgled:** Evidencije › Planiranje radnika › Kalendar → **frmPlanRada**
- **Otvara:** frmPregledMobilni („Prijave mobilni“), frmKalendar („Otvori kalendar“)
- **Tabele:** `Appointments` S, `Labels` S, `Markers` S, `Reminders` S
- **Funkcionalnosti:**
  - „Otvori kalendar“ — klik → `toolStripButton1_Click` — otvara: frmKalendar
  - „Prijave mobilni“ — klik → `toolStripButton4_Click` — otvara: frmPregledMobilni
  - `this` — otvaranje forme → `frmPlanRada_Load` — čita: Appointments, Labels, Markers, Reminders

#### frmPlanUtovaraOdgovorExcel [S]
- **Fajl:** `Uvoz/frmPlanUtovaraOdgovorExcel.cs` — „Uvoz Excel broj kola“
- **Stari izgled:** Uvoz › Uvoz obrada › Uvoz Excel - HŽ cargo → **frmPlanUtovaraOdgovorExcel**
- **Tabele:** `UvozKonacna` S, `UvozKonacnaLogExcel` S, `UvozKonacnaZaglavlje` S, `Voz` S
- **Procedure:** `UpdateBrojKolaIzExcela`, `UpdateBrojKolaIzExcelaVoz`
- **Funkcionalnosti:**
  - „Upload“ — klik → `button1_Click` — izvoz u Excel
  - „Povezi A236“ — klik → `button4_Click` — proc: UpdateBrojKolaIzExcelaVoz; preko: InsertUvozKonacna.UpdateBrojKolaIzExcelaVoz; izvoz u Excel
  - „Poveži“ — klik → `button2_Click` — proc: UpdateBrojKolaIzExcela; preko: InsertUvozKonacna.UpdateBrojKolaIzExcela; izvoz u Excel
  - „Upload A236“ — klik → `button3_Click` — izvoz u Excel

#### frmPlatforma [N]
- **Fajl:** `MainLeget/PrijemIOtpremaKamiona/frmPlatforma.cs` — „Platforma“
- **Novi izgled:** Prijem i otprema kamiona → **frmPrijemIOtpremaKamiona1** → „PLATFORMA“ → **frmPrijemIOtpremaPlatforma** → „Prijem“ → **frmPlatforma**
- **Otvara:** frmDodelaSkladista („Radni nalog kalmar“), frmPrijemKamionaDetalji („Pregled“)
- **Tabele:** `IzvozKonacna` S, `Kapija` S, `KontejnerLog` I p, `MestaUtovara` S, `organizacioneJedinice` S, `PrijemKontejneraVozStavke` S, `RadniNalogInterni` S, `RadniNalogInterniPotvrda` S, `RNPrijemPlatforme` S
- **Procedure:** `InsertKontejnerLog`, `PromeniStatusPrijemPotvrdioKapiju`, `PromeniStatusPrijemPotvrdioVP`
- **Funkcionalnosti:**
  - „Refresh“ — klik → `button8_Click`
  - „Štampaj“ — klik → `button8_Click`
  - „Radni nalog Vaga“ — klik → `button5_Click`
  - „Radni nalog kalmar“ — klik → `button4_Click` — otvara: frmDodelaSkladista; proc: updRN4PrPlatformeKamIzvozPusti; preko: InsertRN.UpdRNPrijemPlatformeKamPusti
  - „Skeniraj dokumenta“ — klik → `button3_Click`
  - „Potvrdi pregled“ — klik → `button21_Click`
  - „Potvrdi kapiju“ — klik → `button27_Click` — proc: PromeniStatusPrijemPotvrdioKapiju; preko: InsertRadniNalogInterni.PromeniStatusKapija
  - „Potvrdi pregled“ — klik → `button1_Click` — proc: PromeniStatusPrijemPotvrdioVP; preko: InsertRadniNalogInterni.PromeniStatusPregledac
  - „Pregled“ — klik → `button9_Click` — otvara: frmPrijemKamionaDetalji

#### frmPlatformaOtprema [N]
- **Fajl:** `MainLeget/PrijemIOtpremaKamiona/frmPlatformaOtprema.cs` — „frmPlatformaOtprema“
- **Novi izgled:** Prijem i otprema kamiona → **frmPrijemIOtpremaKamiona1** → „PLATFORMA“ → **frmPrijemIOtpremaPlatforma** → „Otprema“ → **frmPlatformaOtprema**
- **Otvara:** frmDodelaSkladista („Radni nalog kalmar“)
- **Tabele:** `organizacioneJedinice` S, `OtpremaKontejneraVozStavke` S, `RadniNalogInterni` S, `RadniNalogInterniPotvrda` S
- **Procedure:** `DozvoliIzlaz`, `ObavljenPregled`, `PromeniStatusKalmar`, `PromeniStatusPrijemPotvrdioKapiju`
- **Funkcionalnosti:**
  - „Kalmar“ — klik → `button6_Click` — proc: PromeniStatusKalmar; preko: InsertRadniNalogInterni.PromeniStatusKalmar
  - „Dozvoli izlaz“ — klik → `button1_Click` — proc: DozvoliIzlaz; preko: InsertRadniNalogInterni.DozvoliIzlaz
  - „Refresh“ — klik → `button9_Click`
  - „Štampaj“ — klik → `button8_Click`
  - „Radni nalog kalmar“ — klik → `button5_Click` — otvara: frmDodelaSkladista; proc: updRN4PrPlatformeKamIzvozPusti; preko: InsertRN.UpdRNPrijemPlatformeKamPusti
  - „Potvrdi pregled“ — klik → `button2_Click` — proc: ObavljenPregled; preko: InsertRadniNalogInterni.ObavljenPregled
  - „Potvrdi kapiju“ — klik → `button27_Click` — proc: PromeniStatusPrijemPotvrdioKapiju; preko: InsertRadniNalogInterni.PromeniStatusKapija

#### frmPlombe [S]
- **Fajl:** `Servis/frmPlombe.cs` — „Plombe“
- **Stari izgled:** Operatika › Komunikacija › Plombe → **frmPlombe**
- **Tabele:** `Korisnici` S, `Najava` S, `Plombe` SIUD p
- **Procedure:** `DeletePlombe`, `InsertPlombe`, `UpdatePlombe`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertPlombe, UpdatePlombe; preko: InsertPlombe.InsPlombe, InsertPlombe.UpdPlombe
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeletePlombe; preko: InsertPlombe.DelPlombe
  - „Učitaj“ — klik → `button1_Click`
  - „Sačuvaj“ — klik → `button2_Click` — čita: Plombe
  - „Otvori“ — klik → `btn_Otvori_Click`

#### frmPodesavanja [N]
- **Fajl:** `Administracija/frmPodesavanja.cs`
- **Novi izgled:** Administracija → **Administracija1** → „Podešavanja“ → **frmPodesavanja**
- **Otvara:** frmHRStavka (metoda `Izmeni`)
- **Tabele:** `Podesavanja` SU

#### frmPodesavanjeRaspolozivosti [N+S]
- **Fajl:** `Drumski/frmPodesavanjeRaspolozivosti.cs` — „frmPodesavanjeRaspolozivosti“
- **Novi izgled:** Drumski transport → **Drumski1** → „Cerada“ → **Cerade** → „PROVERA \r\nRASPOLOŽIVOSTI VOZILA“ → **frmPodesavanjeRaspolozivosti**
- **Stari izgled:** Drumski › Šifarnici › Vozni park → **frmAutomobiliDrumski** → „Raspoloživost“ → **frmPodesavanjeRaspolozivosti**
- **Tabele:** `Automobili` S, `AutomobiliTehnickiProblem` SID p, `Delavci` S, `Partnerji` S, `VrstaVozila` S
- **Procedure:** `DeleteAutomobiliTehnickiProblem`, `InsertAutomobiliTehnickiProblem`
- **Combobox → tabela:** `cboPrevoznikFilter` ← `Partnerji`
- **Funkcionalnosti:**
  - „VRATI U RASPOLOŽIVE“ — klik → `button1_Click` — proc: DeleteAutomobiliTehnickiProblem; preko: InsertAutomobili.DelAutomobiliTehnickiProblem
  - „OZNAČI KAO NERASPOLOŽIVE“ — klik → `button2_Click` — čita: AutomobiliTehnickiProblem; proc: InsertAutomobiliTehnickiProblem; preko: InsertAutomobili.InsAutomobiliTehnickiProblem
  - „Filtriraj“ — klik → `btnFiltriraj_Click`

#### frmPodtrase [S]
- **Fajl:** `Dokumenta/frmPodtrase.cs` — „Podtrase“
- **Stari izgled:** Komercijala › Najave › Poslovi → **frmNajava** → „Prevozni nalog - Trase i Lokomotive“ → **frmRadniNalog** → „Unos trasa“ → **frmRadniNalogTraseLok** → „Unos podtrasa“ → **frmPodtrase**
- **Tabele:** `Pruga` S, `PrugaStavke` S, `Stanice` S
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click`
  - `this` — otvaranje forme → `frmPodtrase_Load` — čita: Pruga

#### frmPolozeneLokomotive [S]
- **Fajl:** `Administracija/frmPolozeneLokomotive.cs` — „Položene lokomotive“
- **Stari izgled:** Administrator › Dozvole mašinovođe › Položene lokomotive → **frmPolozeneLokomotive**
- **Otvara:** frmPolozenePruge („Položene Pruge“)
- **Tabele:** `Delavci` S, `LokomotivaSerija` S, `PolozeneLokomotive` S
- **Procedure:** `sp_DeletePolozeneLokomotive`, `sp_InsertPolozeneLokomotive`, `sp_UpdatePolozeneLokomotive`
- **Funkcionalnosti:**
  - „Traži po zaposlenom“ — klik → `btn_zapolseni_Click` — čita: Delavci, LokomotivaSerija, PolozeneLokomotive
  - „Traži po lokomotivi“ — klik → `btn_lokomotiva_Click` — čita: Delavci, LokomotivaSerija, PolozeneLokomotive
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: sp_InsertPolozeneLokomotive, sp_UpdatePolozeneLokomotive; preko: InsertPolozeneLokomotive.InsPolozeneLokomotive, InsertPolozeneLokomotive.UpdPolozeneLokomotive
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: sp_DeletePolozeneLokomotive; preko: InsertPolozeneLokomotive.DelPolozeneLokomotive
  - „Položene Pruge“ — klik → `toolStripButton1_Click` — otvara: frmPolozenePruge

#### frmPolozenePruge [S]
- **Fajl:** `Administracija/frmPolozenePruge.cs` — „Položene pruge“
- **Stari izgled:** Administrator › Dozvole mašinovođe › Položene pruge → **frmPolozenePruge**
- **Otvara:** frmPolozeneLokomotive („Položene Lokomotive“)
- **Tabele:** `Delavci` S, `PolozenePruge` S, `Pruga` S
- **Procedure:** `sp_DeletePolozenePruge`, `sp_InsertPolozenePruge`, `sp_UpdatePolozenePruge`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: sp_InsertPolozenePruge, sp_UpdatePolozenePruge; preko: InsertPolozenePruge.InsPolozenePruge, InsertPolozenePruge.UpdPolozenePruge
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: sp_DeletePolozenePruge; preko: InsertPolozenePruge.DelPolozenePruge
  - „Položene Lokomotive“ — klik → `toolStripButton1_Click` — otvara: frmPolozeneLokomotive
  - „Traži po zaposlenom“ — klik → `btn_zapolseni_Click` — čita: Delavci, PolozenePruge, Pruga
  - „Traži po pruzi“ — klik → `btn_pruga_Click` — čita: Delavci, PolozenePruge, Pruga

#### frmPopis [S]
- **Fajl:** `Promet/frmPopis.cs` — „Popis“
- **Stari izgled:** Terminal › Skladišno poslovanje › Popisna lista → **frmPopis**
- **Otvara:** frmPopisUporedniPrikaz2 („Uporedni prikaz sa stanjem“), frmPopisStampa („Štampa popisa“)
- **Tabele:** `Popis` SIUD p, `PopisStavke` SUD p, `Pozicija` S, `Promet` S, `Skladista` S
- **Procedure:** `DeletePopis`, `DeletePopisStavke`, `InsertPopis`, `InsertPrometPopis`, `InsertStavkePopisa`, `InsertStavkePopisaPoSkladistu`, `InsertStvarnoStanje`, `UpdatePopis`, `UpdatePopisStavke`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertPopis, InsertStavkePopisa, InsertStavkePopisaPoSkladistu, UpdatePopis; preko: InsertPopis.InsPopis, InsertPopis.InsStavkePopisa, InsertPopis.InsStavkePopisaPoSkladistu, InsertPopis.UpdPopis
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeletePopis; preko: InsertPopis.DelPopis
  - `cboSkladisteNovo` — izbor → `cboSkladisteNovo_SelectedIndexChanged` — čita: Pozicija
  - „Izbriši“ — klik → `button3_Click` — proc: DeletePopisStavke; preko: InsertPopis.DelPopisStavke
  - „Promeni“ — klik → `button2_Click` — proc: UpdatePopisStavke; preko: InsertPopis.UpdPopisStavke
  - „Ubaci“ — klik → `button1_Click` — proc: InsertStavkePopisa, InsertStavkePopisaPoSkladistu; preko: InsertPopis.InsStavkePopisa, InsertPopis.InsStavkePopisaPoSkladistu
  - „Uporedni prikaz sa stanjem“ — klik → `toolStripButton1_Click` — otvara: frmPopisUporedniPrikaz2
  - „Štampa popisa“ — klik → `toolStripButton2_Click` — otvara: frmPopisStampa
  - `this` — otvaranje forme → `frmPopis_Load` — čita: Skladista
  - „Završi popis“ — klik → `toolStripButton3_Click` — čita: Promet; proc: InsertPrometPopis; preko: InsertPopis.InsPrometTA
  - `dataGridView1` — promena vrednosti → `dataGridView1_CellValueChanged` — proc: InsertStvarnoStanje; preko: InsertPopis.InsStvarnoStanje
  - „Refresh“ — klik → `button4_Click`

#### frmPopisKontejneraStampa [N+S]
- **Fajl:** `Dokumenta/frmPopisKontejneraStampa.cs` — „Popis kontejnera štampa“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Depo cnt“ → **frmPrijemIOtpremaVozova2** → „Popis cnt“ → **frmPopisKonterjnera** → „Štampa“ → **frmPopisKontejneraStampa**
- **Stari izgled:** Terminal › Skladišno poslovanje › POPIS KONTEJNERA → **frmPopisKonterjnera** → „Štampa“ → **frmPopisKontejneraStampa**
- **Funkcionalnosti:**
  - „Štampa“ — klik → `button2_Click` — izveštaj: rptPopisnaListaKontejnera.rdlc

#### frmPopisKonterjnera [N+S]
- **Fajl:** `Dokumenta/frmPopisKonterjnera.cs` — „Popis kontejnera“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Depo cnt“ → **frmPrijemIOtpremaVozova2** → „Popis cnt“ → **frmPopisKonterjnera**
- **Stari izgled:** Terminal › Skladišno poslovanje › POPIS KONTEJNERA → **frmPopisKonterjnera**
- **Otvara:** frmPopisKontejneraStampa („Štampa“)
- **Tabele:** `KontejnerStatus` S, `Partnerji` S, `PopisKontejnera` SIU p, `PopisKontejneraStavke` SIUD p, `Pozicija` S, `Skladista` S, `TipKontenjera` S, `uvKvalitetKontejnera` S
- **Procedure:** `DeletePopisKontejneraStavke`, `InsertPopisKontejnera`, `InsertPopisKontejneraKontTekuce`, `InsertPopisKontejneraStavke`, `UpdatePopisKontejnera`, `UpdatePopisKontejneraStavke`
- **Combobox → tabela:** `cboSkladisteNovo` ← `Skladista`; `cboSkladiste` ← `Skladista`; `cboPozicija` ← `Pozicija`; `cboPozicijaNova` ← `Pozicija`; `cboBrodar` ← `Partnerji`; `cboTipKontejnera` ← `TipKontenjera`; `cboStatusKontejnera` ← `KontejnerStatus`; `cboKvalitet` ← `uvKvalitetKontejnera`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click_1`
  - „tsSave“ — klik → `tsSave_Click_1` — proc: InsertPopisKontejnera, UpdatePopisKontejnera; preko: InsertPopisKontejnera.InsPopisKontejnera, InsertPopisKontejnera.UpdPopisKontejnera
  - „Generisi stavke sa skladista“ — klik → `toolStripButton2_Click_1` — proc: InsertPopisKontejneraKontTekuce; preko: InsertPopisKontejnera.IUpisiSveStavkeKontejnerTekuce
  - „Osveži“ — klik → `toolStripButton1_Click_1`
  - „Štampa“ — klik → `toolStripButton3_Click_1` — otvara: frmPopisKontejneraStampa
  - „Štampa“ — klik → `toolStripButton3_Click_1` — otvara: frmPopisKontejneraStampa
  - „Refresh“ — klik → `toolStripButton1_Click_1`
  - „Generiši stavke popisa“ — klik → `toolStripButton2_Click_1` — proc: InsertPopisKontejneraKontTekuce; preko: InsertPopisKontejnera.IUpisiSveStavkeKontejnerTekuce
  - `button21` — klik → `tsSave_Click_1` — proc: InsertPopisKontejnera, UpdatePopisKontejnera; preko: InsertPopisKontejnera.InsPopisKontejnera, InsertPopisKontejnera.UpdPopisKontejnera
  - „Izbriši“ — klik → `button3_Click` — proc: DeletePopisKontejneraStavke; preko: InsertPopisKontejnera.DelPopisKontejneraStavke
  - „Promeni“ — klik → `button2_Click` — proc: UpdatePopisKontejneraStavke; preko: InsertPopisKontejnera.UpdPopisKontejneraStavke
  - „Ubaci“ — klik → `button1_Click` — proc: InsertPopisKontejneraStavke; preko: InsertPopisKontejnera.InsPopisKontejneraStavke
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - `this` — otvaranje forme → `frmPopisKonterjnera_Load` — čita: KontejnerStatus, Partnerji, Pozicija, Skladista, TipKontenjera, uvKvalitetKontejnera

#### frmPopisPregled [S]
- **Fajl:** `Promet/frmPopisPregled.cs` — „Pregled popisnih lista“
- **Stari izgled:** Terminal › Skladišno poslovanje › Pregled popisnih lista → **frmPopisPregled**
- **Otvara:** frmPopis („Otvori popisnu listu“)
- **Tabele:** `Popis` S
- **Funkcionalnosti:**
  - „Otvori popisnu listu“ — klik → `button3_Click` — otvara: frmPopis

#### frmPopisStampa [S]
- **Fajl:** `Promet/frmPopisStampa.cs` — „Štampa popisa“
- **Stari izgled:** Terminal › Skladišno poslovanje › Popisna lista → **frmPopis** → „Štampa popisa“ → **frmPopisStampa**
- **Funkcionalnosti:**
  - „Štampa“ — klik → `button2_Click` — izveštaj: rptPopisnLista.rdlc

#### frmPopisUporedniPrikaz2 [S]
- **Fajl:** `Promet/frmPopisUporedniPrikaz2.cs` — „Uporedni prikaz popisa“
- **Stari izgled:** Terminal › Skladišno poslovanje › Popisna lista → **frmPopis** → „Uporedni prikaz sa stanjem“ → **frmPopisUporedniPrikaz2**
- **Procedure:** `SelectPopisStavkeUporedni`
- **Funkcionalnosti:**
  - „Prikazi“ — klik → `button3_Click` — proc: SelectPopisStavkeUporedni; preko: InsertPopis.InsertPopisStavkeUporedni

#### frmPovezivanjeKontejneraIVagona [N+S]
- **Fajl:** `Dokumenta/frmPovezivanjeKontejneraIVagona.cs` — „PAKOVANJE VOZA“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Potvrdi“ → **frmPrijemVozaIzPlana** → „OTVORI - GATE IN VOZ“ → **frmPrijemKontejneraLegetVozUvoz** → „Povezivanje vagona“ → **frmPovezivanjeKontejneraIVagona**
- **Stari izgled:** Terminal › Radni nalozi › Prijem kontejnera voz - N → **frmPrijemKontejneraLegetVozUvoz** → „Povezivanje vagona“ → **frmPovezivanjeKontejneraIVagona**
- **Tabele:** `IzvozKonacna` S, `OtpremaKontejneraVozStavke` S, `Partnerji` S, `PrijemKontejneraVoz` S, `PrijemKontejneraVozStavke` S, `TipKontenjera` S, `Voz` S, `VrstaRobe` S
- **Procedure:** `UpdateIzvozKonacnaVagon`, `UpdateOtpremaKontVozStavkeVagon`
- **Combobox → tabela:** `cboBukingPrijema` ← `Voz`
- **Funkcionalnosti:**
  - „Sačuvaj“ — klik → `button1_Click` — proc: UpdateIzvozKonacnaVagon, UpdateOtpremaKontVozStavkeVagon; preko: InsertIzvozKonacna.UpdateIzvozKonacnaVagon, InsertOtpremaKontejneraStavke.UpdOtpremaKontejneraVozStavVagoni
  - „?“ — klik → `button5_Click` — čita: PrijemKontejneraVoz, PrijemKontejneraVozStavke
  - „PRIKAŽI ISTORISKI PODACI“ — čekiranje → `chkPostojeci_CheckedChanged`
  - `this` — otvaranje forme → `frmPovezivanjeKontejneraIVagona_Load` — čita: Voz

#### frmPozicija [N+S]
- **Fajl:** `TrackModal/Sifarnici/frmPozicija.cs` — „Pozicije“
- **Novi izgled:** Podešavanja sistema → **Podesavanje1** → „Pozicije“ → **frmPozicija**
- **Stari izgled:** Terminal › Šifarnici › Pozicije → **frmPozicija**
- **Tabele:** `Pozicija` SIUD p, `Skladista` S, `SkladistaGrupa` S, `SkladisteObjekat` S
- **Procedure:** `DeletePozicija`, `InsertPozicija`, `UpdatePozicija`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertPozicija, UpdatePozicija; preko: InsertPozicija.InsPozicija, InsertPozicija.UpdPozicija
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeletePozicija; preko: InsertPozicija.DeletePozicija
  - „toolStripButton1“ — klik → `tsPrvi_Click` — čita: Pozicija
  - „toolStripButton1“ — klik → `tsNazad_Click` — čita: Pozicija
  - „toolStripButton1“ — klik → `tsNapred_Click` — čita: Pozicija
  - „toolStripButton1“ — klik → `tsPoslednja_Click` — čita: Pozicija
  - „gridGroupingControl1“ — SelectedRecordsChanged → `gridGroupingControl1_SelectedRecordsChanged`
  - „?“ — klik → `btnPretraziPoSkladistu_Click` — čita: Pozicija, Skladista
  - „Refresh“ — klik → `button2_Click`
  - „Neaktivne“ — klik → `button1_Click`
  - „Aktivne“ — klik → `button23_Click`
  - `button20` — klik → `tsDelete_Click` — proc: DeletePozicija; preko: InsertPozicija.DeletePozicija
  - `button21` — klik → `tsSave_Click` — proc: InsertPozicija, UpdatePozicija; preko: InsertPozicija.InsPozicija, InsertPozicija.UpdPozicija
  - `cboSkladiste` — SelectionChangeCommitted → `cboSkladiste_SelectedIndexChanged` — čita: Skladista; preko: HijerarhijaSkladista.VrstaPozicijaPolja

#### frmPozicioniranje [S]
- **Fajl:** `Drumski/frmPozicioniranje.cs` — „frmPozicioniranje“
- **Stari izgled:** Drumski › Šifarnici › Pozicioniranje → **frmPozicioniranje**
- **Otvara:** frmPozicioniranjeEdit (`button22` (Click))
- **Tabele:** `DrumskiPozicioniranje` S
- **Procedure:** `DeletePozicioniranje`, `InsertPozicioniranje`, `UpdatePozicioniranje`
- **Funkcionalnosti:**
  - `dataGridView1` — izbor → `dataGridView1_SelectionChanged` — otvara: frmPozicioniranjeEdit
  - `button22` — klik → `tsNew_Click` — otvara: frmPozicioniranjeEdit

#### frmPozicioniranjeEdit [S]
- **Fajl:** `Drumski/frmPozicioniranjeEdit.cs` — „Pozicioniranje detalji“
- **Stari izgled:** Drumski › Šifarnici › Pozicioniranje → **frmPozicioniranje** → `button22` (Click) → **frmPozicioniranjeEdit**
- **Procedure:** `DeletePozicioniranje`, `InsertPozicioniranje`, `UpdatePozicioniranje`
- **Funkcionalnosti:**
  - `button20` — klik → `button20_Click` — proc: DeletePozicioniranje; preko: InsertNapomena.DelNapomena
  - `button21` — klik → `button21_Click` — proc: InsertPozicioniranje, UpdatePozicioniranje; preko: InsertNapomena.InsNapomena, InsertNapomena.UpdNapomena

#### frmPravljenjeVoza [S]
- **Fajl:** `frmPravljenjeVoza.cs` — „Pravljenje voza“
- **Stari izgled:** Operatika › Prevozni nalozi › Pokretanje vagoni → **frmPravljenjeVoza**
- **Tabele:** `RadniNalog` S, `Stanice` S, `TeretnicaIskljuceniVagoni` S, `TeretnicaStavke` S
- **Procedure:** `InsertTeretnicaStavkaRN`
- **Funkcionalnosti:**
  - „Pretraži“ — klik → `button3_Click` — čita: TeretnicaIskljuceniVagoni, TeretnicaStavke, stanice
  - „Veži vagone za RN / Napravi  Teretnicu“ — klik → `button1_Click` — proc: InsertTeretnicaStavkaRN; preko: InsertTeretnicaStavke.InsTeretnicaStavkeRN
  - `this` — otvaranje forme → `frmPravljenjeVoza_Load` — čita: RadniNalog, TeretnicaIskljuceniVagoni, TeretnicaStavke, stanice

#### frmPravoAktivnosti [S]
- **Fajl:** `Dokumenta/frmPravoAktivnosti.cs` — „Pravo aktivnosti“
- **Stari izgled:** Evidencije › Vrste aktivnosti › Prava → **frmPravoAktivnosti**
- **Tabele:** `Delavci` S, `PravoAktivnosti` SID p, `VrstaAktivnosti` S
- **Procedure:** `DeletePravoAktivnosti`, `InsertPravoAktivnosti`
- **Funkcionalnosti:**
  - „Izbriši“ — klik → `button1_Click` — proc: DeletePravoAktivnosti; preko: InsertPravoAktivnosti.DelPravoAktivnosti
  - „>>“ — klik → `btnUnesi_Click` — proc: InsertPravoAktivnosti; preko: InsertPravoAktivnosti.InsPravoAktivnosti
  - „Refresh“ — klik → `button2_Click`
  - `this` — otvaranje forme → `frmPravoAktivnosti_Load` — čita: Delavci

#### frmPravoAktivnostiViseRadnika [S]
- **Fajl:** `Dokumenta/frmPravoAktivnostiViseRadnika.cs` — „Dodela prava za grupu radnika“
- **Stari izgled:** Evidencije › Vrste aktivnosti › Prava više radnika → **frmPravoAktivnostiViseRadnika**
- **Tabele:** `Delavci` S, `DelovnaMesta` S, `PravoAktivnosti` SI p, `VrstaAktivnosti` S
- **Procedure:** `InsertPravoAktivnosti`
- **Funkcionalnosti:**
  - „Refresh“ — klik → `button2_Click`
  - „>>“ — klik → `btnUnesi_Click` — proc: InsertPravoAktivnosti; preko: InsertPravoAktivnosti.InsPravoAktivnosti

#### frmPrebacivanjeIzPlanaUPlan [S]
- **Fajl:** `Izvoz/frmPrebacivanjeIzPlanaUPlan.cs` — „Prebacianje iz plana u plan“
- **Stari izgled:** Izvoz › Izvoz › Prebacivanje iz plana u plan → **frmPrebacivanjeIzPlanaUPlan**
- **Tabele:** `Carinarnice` S, `InspekciskiTretman` S, `IzvozKonacna` S, `IzvozKonacnaZaglavlje` S, `IzvozNHM` S, `IzvozVrstaManipulacije` S, `IzvozVrstaRobeHS` S, `KontejnerskiTerminali` S, `KrajnjaDestinacija` S, `MestaUtovara` S, `NHM` S, `Partnerji` S, `partnerjiKontOsebaMU` S, `TipKontenjera` S, `uvNacinPakovanja` S, `Voz` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaRobeADR` S, `VrstaRobeHS` S, `VrstePostupakaUvoz` S
- **Procedure:** `PrenesiIzPlanIzvozaUPlanIzvoza`
- **Funkcionalnosti:**
  - „Refresh“ — klik → `button1_Click`
  - „Refresh“ — klik → `button2_Click`
  - „Prebaci selektovane stavke“ — klik → `button3_Click` — proc: PrenesiIzPlanIzvozaUPlanIzvoza; preko: InsertIzvozKonacna.PrenesiIzPlanUtovaraUPlanUtovara
  - „<<<<<“ — klik → `button4_Click`
  - `this` — otvaranje forme → `frmPrebacivanjeIzPlanaUPlan_Load` — čita: IzvozKonacnaZaglavlje, Voz

#### frmPrebacivanjeIzPlanaUPlan [S]
- **Fajl:** `Uvoz/frmPrebacivanjeIzPlanaUPlan.cs` — „Prebacianje iz plana u plan“
- **Stari izgled:** Uvoz › Uvoz obrada › Prebacivanje kontejnera iz plana u plan → **frmPrebacivanjeIzPlanaUPlan**
- **Tabele:** `Brodovi` S, `Carinarnice` S, `DirigacijaKontejneraZa` S, `KontejnerskiTerminali` S, `MestaUtovara` S, `NHM` S, `Partnerji` S, `partnerjiKontOsebaMU` S, `TipKontenjera` S, `uvNacinPakovanja` S, `UvozKonacna` S, `UvozKonacnaZaglavlje` S, `Voz` S, `VrstaCarinskogPostupka` S, `VrstaRobeADR` S, `VrstaRobeHS` S, `VrstePostupakaUvoz` S
- **Procedure:** `PrenesiIzPlanUtovaraUPlanUtovara`
- **Funkcionalnosti:**
  - „Refresh“ — klik → `button1_Click`
  - „Refresh“ — klik → `button2_Click`
  - „Prebaci selektovane stavke“ — klik → `button3_Click` — proc: PrenesiIzPlanUtovaraUPlanUtovara; preko: InsertUvozKonacna.PrenesiIzPlanUtovaraUPlanUtovara
  - „<<<<<“ — klik → `button4_Click` — proc: PrenesiIzPlanUtovaraUPlanUtovara; preko: InsertUvozKonacna.PrenesiIzPlanUtovaraUPlanUtovara
  - `this` — otvaranje forme → `frmPrebacivanjeIzPlanaUPlan_Load` — čita: Partnerji, UvozKonacnaZaglavlje, Voz

#### frmPredefinisanePoruke [S]
- **Fajl:** `TrackModal/Sifarnici/frmPredefinisanePoruke.cs` — „Predefinisane Poruke“
- **Stari izgled:** Terminal › Šifarnici › Predefinisane poruke → **frmPredefinisanePoruke**
- **Tabele:** `Predefinisaneporuke` SIUD p
- **Procedure:** `deletePredefinisanePoruke`, `InsertPredefinisanePoruke`, `UpdatePredefinisanePoruke`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertPredefinisanePoruke, UpdatePredefinisanePoruke; preko: InsertPredefinisanePoruke.InsPredefinisanePoruke, InsertPredefinisanePoruke.UpdPredefinisanePoruke
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: deletePredefinisanePoruke; preko: InsertPredefinisanePoruke.DeletePredefinisanePoruke

#### frmPredjenaKilometrazaLokomotiva [S]
- **Fajl:** `Dokumenta/frmPredjenaKilometrazaLokomotiva.cs` — „Predjena kilometraza lokomotiva“
- **Stari izgled:** Evidencije › Prijave i odjave › Predjena KM u Periodu → **frmPredjenaKilometrazaLokomotiva**
- **Tabele:** `LokomotivaPredjeno` S
- **Procedure:** `InsertPredjenaKM`, `InsertPredjenaKM2`
- **Funkcionalnosti:**
  - „Izračunaj 1“ — klik → `btnIzracunaj_Click` — proc: InsertPredjenaKM, InsertPredjenaKM2; preko: InsertProracunKM.InsProracunKM, InsertProracunKM.InsProracunKM2

#### frmPregledAktivnosti [S]
- **Fajl:** `Dokumenta/frmPregledAktivnosti.cs` — „Pregled aktivnosti“
- **Stari izgled:** Evidencije › Evidencija aktivnosti › Pregled trenutnih aktivnosti → **frmPregledAktivnosti**
- **Otvara:** frmPregledLokomotivaPrimopredaja („LO-dnevni pregled“), frmAutomobiliPregledPrijava („Automobili“), frmTehnickiPregled („Tehnicki pregled“), frmKomercijalniPregled („Komercijalni pregled“), frmVucaPregled („Vuca“), frmNajava („Otvori prevoz“), frmTeretnica („Otvori Teretnice“), frmTrainList („Otvori TrainList“)
- **Tabele:** `Aktivnosti` S, `AktivnostiStavke` S, `AktivnostiStavkeDokumenta` S, `Automobili` S, `CistocaIznutra` S, `CistocaIznutraRazduzivanje` S, `CistocaSpolja` S, `CistocaSpoljaRazduzivanje` S, `Delavci` S, `KomercijalniPregled` S, `LokomotivaPrimopredaja` S, `Najava` S, `SifGrupaNepravilnosti` S, `SifNeispravnostPostupak` S, `SifOpisNeispravnosti` S, `SifRazredNepravilnosti` S, `SifVrstaNepravilnosti` S, `SifVucaStatusi` S, `Stanice` S, `TehnickiPregled` S, `Teretnica` S, `TrainList` S, `UljeAuto` S, `UljeAutoRazduzivanje` S, `VrstaAktivnosti` S, `Vuca` S, `VucaSlike` S, `VucaStatusi` S, `ZaposleniPrijavaAuto` S
- **Funkcionalnosti:**
  - „Aktivnosti po poslu“ — klik → `button1_Click` — čita: Aktivnosti, AktivnostiStavke, Delavci, Stanice, VrstaAktivnosti
  - „Sve aktivnosti“ — klik → `btnPretrazi_Click` — čita: Aktivnosti, AktivnostiStavke, Delavci, Stanice, VrstaAktivnosti
  - „Otvori prevoz“ — klik → `button2_Click` — otvara: frmNajava
  - „Otvori TrainList“ — klik → `button3_Click` — otvara: frmTrainList
  - „Otvori Teretnice“ — klik → `button4_Click` — otvara: frmTeretnica
  - „Tehnicki pregled“ — klik → `button5_Click` — otvara: frmTehnickiPregled
  - „Komercijalni pregled“ — klik → `button6_Click` — otvara: frmKomercijalniPregled
  - „LO-dnevni pregled“ — klik → `button7_Click` — otvara: frmPregledLokomotivaPrimopredaja
  - „Vuca“ — klik → `button8_Click` — otvara: frmVucaPregled
  - „Automobili“ — klik → `button9_Click`
  - „Tekuci poslovi“ — klik → `chkAktvni_Click`
  - „Otvori“ — klik → `button10_Click`

#### frmPregledArhiviranihNalogaDrumski [S]
- **Fajl:** `Drumski/frmPregledArhiviranihNalogaDrumski.cs` — „frmPregledArhiviranihNalogaDrumski“
- **Stari izgled:** Drumski › Drumski › Lista arhiviranih naloga → **frmPregledArhiviranihNalogaDrumski**
- **Otvara:** frmDrumski („Otvori“)
- **Tabele:** `Automobili` S, `Izvoz` S, `IzvozKonacna` S, `Partnerji` S, `RadniNalogDrumski` S, `SistemskePostavke` S, `StatusVozila` S, `TipKontenjera` S, `Uvoz` S, `UvozKonacna` S
- **Funkcionalnosti:**
  - „Otvori“ — klik → `button1_Click` — otvara: frmDrumski

#### frmPregledAutoPrevozniList [S]
- **Fajl:** `TrackModal/Dokumeta/frmPregledAutoPrevozniList.cs` — „Pregled autoprevoznih listova“
- **Stari izgled:** Terminal › Transport › Pregled autoprevoznički list → **frmPregledAutoPrevozniList**
- **Otvara:** frmAutoprevozniList2 („Otvori autoprevoznilist“)
- **Tabele:** `Autoprevoznilist` S
- **Funkcionalnosti:**
  - „Otvori autoprevoznilist“ — klik → `toolStripButton1_Click` — otvara: frmAutoprevozniList2
  - „Osveži“ — klik → `toolStripButton2_Click` — čita: Autoprevoznilist

#### frmPregledCarinskog [S]
- **Fajl:** `Carinsko/frmPregledCarinskog.cs` — „Pregled carinskog “
- **Stari izgled:** Carinsko › Magacinsko poslovanje › Analize carinskog → **frmPregledCarinskog**
- **Tabele:** `OtpremnicaCarinska` S, `OtpremnicaCarinskaStavke` S, `Partnerji` S, `Pozicija` S, `PrijemnicaCarinska` S, `PrijemnicaCarinskaStavke` S, `Promet` S, `Skladista` S
- **Funkcionalnosti:**
  - „Promet“ — klik → `button23_Click` — čita 8 tabela; izvoz u Excel
  - „Po prijemu“ — klik → `button1_Click` — čita: Partnerji, PrijemnicaCarinska, PrijemnicaCarinskaStavke, Skladista; izvoz u Excel

#### frmPregledDokumenataKamiona [S]
- **Fajl:** `Drumski/frmPregledDokumenataKamiona.cs` — „Dodavanje dokumenata“
- **Stari izgled:** Drumski › Finansije › Lista naloga → **frmPregledFaktura** → „Pregled dokumenata vezanih za ovaj kamion“ → **frmPregledDokumenataKamiona**
- **Tabele:** `Delavci` S, `Korisnici` S, `UploadedFiles` S
- **Procedure:** `DeleteDokumentKamionaDrumski`, `InsertDokumentaUploadedFiles`
- **Funkcionalnosti:**
  - „Odaberi jedan ili više dokumenata“ — klik → `btnOdaberi_Click`
  - „Obriši“ — klik → `btnObrisi_Click`
  - `button2` — klik → `button2_Click` — čita: UploadedFiles; proc: InsertDokumentaUploadedFiles; preko: InsertFakture.SnimiUFajlBazuKamioni
  - `dataGridView1` — klik na ćeliju → `dataGridView1_CellContentClick` — proc: DeleteDokumentKamionaDrumski; preko: InsertFakture.DelDokumentKamiona

#### frmPregledDokumenataVozaca [S]
- **Fajl:** `Drumski/frmPregledDokumenataVozaca.cs` — „frmPregledDokumenataVozaca“
- **Stari izgled:** Drumski › Finansije › Lista naloga → **frmPregledFaktura** → „Pregled skeniranih dokumenata“ → **frmPregledDokumenataVozaca**
- **Tabele:** `Automobili` S, `DokumentaFaktureDrumski` S, `DokumentaRadnogNalogaDrumski` S, `Partnerji` S, `RadniNalogDrumski` S, `UploadedFiles` S
- **Procedure:** `DeleteDokumentVozaca`, `UpdateStatusFajla`
- **Funkcionalnosti:**
  - `dataGridView1` — klik na ćeliju → `dataGridView1_CellContentClick` — proc: DeleteDokumentVozaca; preko: InsertFakture.DelDokumentVozaca
  - `dataGridView1` — CellMouseClick → `dataGridView1_CellMouseClick` — proc: UpdateStatusFajla; preko: InsertFakture.UpdateStatusFajla

#### frmPregledFajlova [N+S]
- **Fajl:** `Drumski/FormaPregledFajlova.cs` — „frmPregledFajlova“
- **Novi izgled:** Drumski transport → **Drumski1** → „Cerada“ → **Cerade** → „STATUSI VOZILA“ → **frmStatus** → `dataGridView3` (CellClick) → **frmPregledFajlova**
- **Stari izgled:** Drumski › Drumski › Pakovanje kamiona → **PakovanjeKamiona1** → `dataGridView3` (CellClick) → **frmPregledFajlova**
- **Tabele:** `DokumentaRadnogNalogaDrumski` S

#### frmPregledFaktura [S]
- **Fajl:** `Drumski/frmPregledFaktura.cs` — „Pregled Faktura“
- **Stari izgled:** Drumski › Finansije › Lista naloga → **frmPregledFaktura**
- **Otvara:** frmFaktureDetalji („Pregled i izmena podataka fakture“), frmDrumski („Pregled naloga drumskog transporta“), frmAutomobiliDrumski („Pregled detalja kamiona“), frmBrojFaktureModal („Objedinjena dokumentacija po broju izlazne fakture“), frmObjedinjenaDokumentIzlaznaFakura („Objedinjena dokumentacija po broju izlazne fakture“), frmPregledDokumenataVozaca („Pregled skeniranih dokumenata“), frmPregledDokumenataKamiona („Pregled dokumenata vezanih za ovaj kamion“), frmSeniranjeDokumenata („Dodaj skeniranu dokumentaciju“), frmIzvestajNalogZaPrevoz („Nalog za obavljanje prevoza“)
- **Tabele:** `Automobili` S, `Delavci` S, `DokumentaFaktureDrumski` S, `DokumentaRadnogNalogaDrumski` S, `FakturaDrumski` S, `FakturaDrumskiStavka` S, `Izvoz` S, `IzvozKonacna` S, `IzvozKonacnaZaglavlje` S, `KursnaLista` S, `MestaUtovara` S, `Partnerji` S, `RadniNalogDrumski` S, `RadniNalogDrumskiIzmene` S, `StatusVozila` S, `TipKontenjera` S, `UploadedFiles` S, `Uvoz` S, `UvozKonacna` S, `UvozKonacnaZaglavlje` S, `Voz` S, `VrstaVozila` S
- **Funkcionalnosti:**
  - „gridGroupingControl1“ — TableControlMouseDown → `GridGroupingControl1_TableControlMouseDown`
  - „Nalog za obavljanje prevoza“ — klik → `btnFormiranjeNaloga_Click` — otvara: frmIzvestajNalogZaPrevoz
  - „Pregled i izmena podataka fakture“ — klik → `toolStripMenuItem1_Click` — otvara: frmFaktureDetalji
  - „Pregled naloga drumskog transporta“ — klik → `toolStripPregledNaloga_Click` — otvara: frmDrumski
  - „Pregled detalja kamiona“ — klik → `toolStripPregledKamiona_Click` — otvara: frmAutomobiliDrumski
  - „Pregled skeniranih dokumenata“ — klik → `toolStripDokumentiVozaca_Click` — otvara: frmPregledDokumenataVozaca
  - „Objedinjena dokumentacija po broju izlazne fakture“ — klik → `toolStripObjedinjenaDokumenta_Click` — otvara: frmBrojFaktureModal, frmObjedinjenaDokumentIzlaznaFakura
  - „Pregled dokumenata vezanih za ovaj kamion“ — klik → `toolStripDokumentaKamiona_Click` — otvara: frmPregledDokumenataKamiona
  - „Dodaj skeniranu dokumentaciju“ — klik → `toolStripSkeniranaDokumentacija_Click` — otvara: frmSeniranjeDokumenata
  - „gridGroupingControl1“ — TableControlCurrentCellActivated → `GridGroupingControl1_TableControlCurrentCellActivated`

#### frmPregledGrupaKontejneraIzvoz [N]
- **Fajl:** `Izvoz/frmPregledGrupaKontejneraIzvoz.cs` — „frmPregledGrupaKontejneraIzvoz“
- **Novi izgled:** Logistika izvoza → **LogistikaIzvoza1** → „Formiranje naloga za otpremu“ → **LogistikaIzvoza2** → „Izmeni postojeći“ → **frmPregledGrupaKontejneraIzvoz**
- **Otvara:** frmLogTokaProcesa („Otvori“), frmGrupniUnosPoljaIzvoz („Otvori“)
- **Tabele:** `Izvoz` S, `IzvozKonacna` S, `KontejnerLog` S, `Partnerji` S, `ProdajniNalogIzvozStavke` S, `TipKontenjera` S, `uvNacinPakovanja` S, `VrstaRobeADR` S
- **Procedure:** `DeleteKontejnerIzvoz`
- **Funkcionalnosti:**
  - „Izmeni količinu stavke porudžbenice“ — klik → `btnIzmeniKolicinu_Click`
  - „Otvori“ — klik → `btnOtvori_Click` — otvara: frmLogTokaProcesa, frmGrupniUnosPoljaIzvoz
  - „gridGroupingControl1“ — TableControlMouseDown → `gridGroupingControl1_TableControlMouseDown`
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick_1` — proc: DeleteKontejnerIzvoz; preko: InsertIzvoz.DelKontejnerIzvoz
  - „Otvori“ — klik → `toolStripMenuItemOtvori_Click` — otvara: frmLogTokaProcesa, frmGrupniUnosPoljaIzvoz

#### frmPregledKamiona [N+S]
- **Fajl:** `Kapija/frmPregledKamiona.cs` — „Pregled kamiona“
- **Novi izgled:** Kapija → **frmKapija1** → „Lista SVIH naloga“ → **frmPregledKamiona**
- **Stari izgled:** Kapija › Kapije › Pregled kamiona → **frmPregledKamiona**
- **Otvara:** frmKamionDetalji („Otvori“)
- **Tabele:** `Kapija` S, `StatusKapija` S
- **Procedure:** `UpdateStatusKamionaKapija`
- **Combobox → tabela:** `cboStatus` ← `StatusKapija`
- **Funkcionalnosti:**
  - „Promena statusa“ — klik → `button3_Click` — čita: StatusKapija
  - „Otvori“ — klik → `button1_Click` — otvara: frmKamionDetalji
  - „Save“ — klik → `btnSnimi_Click` — proc: UpdateStatusKamionaKapija; preko: InsertKapija.UpdateStatusKapija
  - „Close“ — klik → `button4_Click`

#### frmPregledKontejneraDrumskeUsluge [N+S]
- **Fajl:** `Uvoz/frmPregledKontejneraDrumskeUsluge.cs` — „Pregled drumskih usluga“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Voz“ → **frmPrijemIOtpremaVozova4** → „Vozovi“ → **frmVoz** → „UVOZ - FORMIRANJE PLANA“ → **frmFormiranjePlana** → „Otvori plan“ → **frmUvozKonacna** → „DRUMSKE USLUGE“ → **frmPregledKontejneraDrumskeUsluge**
- **Stari izgled:** Uvoz › Uvoz obrada › Neraspoređeni kontejneri - Obrazac → **Uvoz** → „Drumske usluge“ → **frmPregledKontejneraDrumskeUsluge**
- **Tabele:** `Automobili` S, `organizacioneJedinice` S, `RadniNalogDrumski` S, `StatusVozila` S, `Uvoz` S, `UvozKonacna` S, `UvozKonacnaVrstaManipulacije` S, `UvozVrstaManipulacije` S, `VrstaManipulacije` S
- **Procedure:** `DodajStavkeURadniNalog`, `NapraviRadniNalogDrumski`
- **Funkcionalnosti:**
  - „Dopuna naloga“ — klik → `button2_Click` — proc: DodajStavkeURadniNalog; preko: InsertUvoz.UpdateRadniNalogDrumski
  - „Formirnje naloga“ — klik → `button1_Click` — proc: NapraviRadniNalogDrumski; preko: InsertUvoz.KreirajRadniNalogDrumski

#### frmPregledKontejneraIzvoz [S]
- **Fajl:** `Izvoz/frmPregledKontejneraIzvoz.cs` — „Pregled svih kontejnera izvoz“
- **Stari izgled:** Izvoz › Izvoz › Nerasporedjeni kontejneri tabela → **frmPregledKontejneraIzvoz**
- **Otvara:** frmIzvoz („Novi“), frmIzvozKopiranjeKontejnera („Kopiraj“)
- **Tabele:** `Carinarnice` S, `InspekciskiTretman` S, `Izvoz` S, `IzvozNHM` S, `IzvozVrstaManipulacije` S, `IzvozVrstaRobeHS` S, `KontejnerskiTerminali` S, `KrajnjaDestinacija` S, `MestaUtovara` S, `NHM` S, `Partnerji` S, `TipKontenjera` S, `uvNacinPakovanja` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaRobeHS` S, `VrstePostupakaUvoz` S
- **Procedure:** `DeleteIzvozSve`
- **Funkcionalnosti:**
  - „Refresh“ — klik → `toolStripButton1_Click`
  - „Otvori“ — klik → `toolStripButton2_Click` — otvara: frmIzvoz
  - „Novi“ — klik → `toolStripButton3_Click` — otvara: frmIzvoz
  - „Pretraži“ — klik → `button9_Click`
  - „Obriši selektovane“ — klik → `button2_Click` — proc: DeleteIzvozSve; preko: InsertIzvoz.DelIzvozSve
  - „Kopiraj“ — klik → `button1_Click` — otvara: frmIzvozKopiranjeKontejnera
  - „Refresh“ — klik → `toolStripButton1_Click`
  - „Otvori“ — klik → `toolStripButton2_Click` — otvara: frmIzvoz
  - „Novi“ — klik → `toolStripButton3_Click` — otvara: frmIzvoz
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - `this` — otvaranje forme → `frmPregledKontejneraIzvoz_Load` — čita 17 tabela; izvoz u Excel

#### frmPregledKontejneraIzvozIzUvoza [S]
- **Fajl:** `Izvoz/frmPregledKontejneraIzvozIzUvoza.cs` — „Pregled uvoznih kontejnera i prebacivanje u Izvoz“
- **Stari izgled:** Izvoz › Izvoz › Prebacivanje kontejnera iz uvoza u Plan izvoza → **frmPregledKontejneraIzvozIzUvoza**
- **Tabele:** `Brodovi` S, `Carinarnice` S, `DirigacijaKontejneraZa` S, `InspekciskiTretman` S, `Izvoz` S, `IzvozKonacnaZaglavlje` S, `IzvozNHM` S, `IzvozVrstaManipulacije` S, `IzvozVrstaRobeHS` S, `KontejnerskiTerminali` S, `KrajnjaDestinacija` S, `MestaUtovara` S, `NHM` S, `Partnerji` S, `partnerjiKontOsebaMU` S, `Predefinisaneporuke` S, `TipKontenjera` S, `uvNacinPakovanja` S, `Uvoz` S, `UvozKonacnaZaglavlje` S, `UvozNHM` S, `UvozVrstaManipulacije` S, `UvozVrstaRobeHS` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaRobeADR` S, `VrstaRobeHS` S, `VrstePostupakaUvoz` S
- **Funkcionalnosti:**
  - „Prebaci selektovane uvozne kontejnere“ — klik → `toolStripButton1_Click`
  - `this` — otvaranje forme → `frmPregledKontejneraIzvozIzUvoza_Load` — čita: UvozKonacnaZaglavlje

#### frmPregledLokomotivaPrimopredaja [S]
- **Fajl:** `Dokumenta/frmPregledLokomotivaPrimopredaja.cs` — „PREGLED SPECIFICNE AKTIVNOSTI - 60 - LO“
- **Stari izgled:** Održavanje › Popis lokomotiva › PREGLEDI LO → **frmPregledLokomotivaPrimopredaja**
- **Tabele:** `Aktivnosti` S, `AktivnostiStavke` S, `AktivnostiStavkeDokumenta` S, `Delavci` S, `LokomotivaPrijava` S, `Stanice` S, `VrstaAktivnosti` S
- **Funkcionalnosti:**
  - „Refresh“ — klik → `btn_OtvoriSliku_Click`
  - „Otvori“ — klik → `button10_Click`

#### frmPregledManipulacijaPoPartneru [S]
- **Fajl:** `Izvestaji/frmPregledManipulacijaPoPartneru.cs` — „Pregled manipulacija po partneru“
- **Stari izgled:** Terminal › Terminalske usluge › Pregled manipulacija po partneru → **frmPregledManipulacijaPoPartneru**
- **Tabele:** `NaruceneManipulacije` S, `Partnerji` S, `VrstaManipulacije` S
- **Funkcionalnosti:**
  - „Pozovi podatke“ — klik → `button1_Click`
  - „?“ — klik → `button3_Click`
  - `this` — otvaranje forme → `frmPregledManipulacijaPoPartneru_Load` — čita: Partnerji

#### frmPregledMasinovodje [S]
- **Fajl:** `SyncForm/frmPregledMasinovodje.cs` — „Pregled tableti“
- **Stari izgled:** Evidencije › Prijave i odjave › Analiza prijave odjave tablet → **frmPregledMasinovodje**
- **Tabele:** `Delavci` S, `LokomotivaPrijava` S, `Stanice` S
- **Funkcionalnosti:**
  - „gridGroupingControl1“ — SelectedRecordsChanging → `gridGroupingControl1_SelectedRecordsChanging`
  - „gridGroupingControl1“ — SelectedRecordsChanged → `gridGroupingControl1_SelectedRecordsChanged`
  - `this` — otvaranje forme → `frmPregledMasinovodje_Load` — čita: Delavci, LokomotivaPrijava, Stanice

#### frmPregledMasinovodje2 [S]
- **Fajl:** `SyncForm/frmPregledMasinovodje2.cs` — „Pregled tableti 2“
- **Stari izgled:** Evidencije › Pregledi › Pregled masinovodje 2 → **frmPregledMasinovodje2**
- **Tabele:** `Aktivnosti` S, `AktivnostiStavke` S, `Delavci` S, `LokomotivaPrijava` S, `Stanice` S
- **Funkcionalnosti:**
  - `this` — otvaranje forme → `frmPregledMasinovodje2_Load` — čita: Aktivnosti, AktivnostiStavke, Delavci, LokomotivaPrijava, Stanice

#### frmPregledMedjuskladisniPrenos [S]
- **Fajl:** `Promet/frmPregledMedjuskladisniPrenos.cs` — „Međuskladisni prenosi po prijemu“
- **Stari izgled:** Terminal › Skladišno poslovanje › Pregled međuskladišni po prijemu → **frmPregledMedjuskladisniPrenos**
- **Tabele:** `Pozicija` S, `PrijemKontejneraVoz` S, `PrijemKontejneraVozStavke` S, `Promet` S, `Skladista` S, `Voz` S
- **Funkcionalnosti:**
  - „Pretraži“ — klik → `button1_Click`
  - „Pretraži“ — klik → `btnPretrazi_Click`
  - `this` — otvaranje forme → `frmPregledSkladistePrijem_Load` — čita: PrijemKontejneraVoz, Voz
  - „Prijem kamionom:“ — klik → `label6_Click`
  - „Prijem vozom:“ — klik → `label5_Click`

#### frmPregledMobilni [S]
- **Fajl:** `SyncForm/frmPregledMobilni.cs` — „Pregled mobilnih prijava“
- **Stari izgled:** Evidencije › Prijave i odjave › Analiza terenci tel → **frmPregledMobilni**
- **Tabele:** `Delavci` S, `ZaposleniPrijava` S
- **Funkcionalnosti:**
  - `this` — otvaranje forme → `frmPregledMobilni_Load` — čita: Delavci, ZaposleniPrijava

#### frmPregledMobilni2 [S]
- **Fajl:** `SyncForm/frmPregledMobilni2.cs` — „Pregled mobilni sa poslovima“
- **Stari izgled:** Evidencije › Pregledi › Analiza aktivnosti po poslu → **frmPregledMobilni2**
- **Tabele:** `Aktivnosti` S, `AktivnostiStavke` S, `Delavci` S, `VrstaAktivnosti` S, `ZaposleniPrijava` S
- **Funkcionalnosti:**
  - `this` — otvaranje forme → `frmPregledMobilni2_Load` — čita: Aktivnosti, AktivnostiStavke, Delavci, VrstaAktivnosti, ZaposleniPrijava

#### frmPregledNalogaDrumski [N+S]
- **Fajl:** `Drumski/frmPregledNalogaDrumski.cs` — „Lista naloga“
- **Novi izgled:** Drumski transport → **Drumski1** → „Cerada“ → **Cerade** → „NALOG ZA DRUMSKI TRANSPORT“ → **NalogZaDrumski** → „TRANSPORTNI NALOZI IZVOZ“ → **frmPregledNalogaDrumski**
- **Stari izgled:** Drumski › Drumski › Lista naloga → **frmPregledNalogaDrumski**
- **Otvara:** frmDrumski1 („Otvori“), frmDrumski („Otvori“), PakovanjeKamiona1 („Dodeli kamion“), frmFormaZaVozace („Vozac“)
- **Tabele:** `Automobili` S, `Carinarnice` S, `Delavci` S, `Izvoz` S, `IzvozKonacna` S, `IzvozKonacnaZaglavlje` S, `Korisnici` S, `NapomenaZaPozicioniranje` S, `Partnerji` S, `partnerjiKontOsebaMU` S, `RadniNalogDrumski` SD p, `RadniNalogInterni` S, `SistemskePostavke` S, `StatusVozila` S, `TipKontenjera` S, `Uvoz` S, `UvozKonacna` S, `UvozKonacnaZaglavlje` S, `Voz` S, `VrstaCarinskogPostupka` S, `VrstePostupakaUvoz` S
- **Procedure:** `DeleteRadniNalogDrumski`, `DodajStavkeURadniNalogDrumski`, `DuplirajZapisRadniNalogDrumski`, `NapraviRadniNalogDrumskiIzDrumskog`, `UpdateRadniNalogDrumskiOdobrioLO`, `UpdateRadniNalogInterniZavrsen`, `UpdateStatusRadniNalogDrumski`
- **Combobox → tabela:** `cboStatus` ← `StatusVozila`
- **Funkcionalnosti:**
  - „Potvrdi LO nalog za Transport“ — klik → `btnOdobrioLO_Click` — proc: UpdateRadniNalogDrumskiOdobrioLO; preko: InsertRadniNalogDrumski.UpdateOdobrioLO
  - „Dupliraj zapis“ — klik → `button6_Click` — čita 14 tabela; proc: DuplirajZapisRadniNalogDrumski; preko: InsertRadniNalogDrumski.DuplirajRadniNalogDrumski
  - „Dopuna naloga“ — klik → `btnDopunaNaloga_Click_1` — proc: DodajStavkeURadniNalogDrumski; preko: InsertRadniNalogDrumski.UpdateNalogIDRadniNalogDrumski
  - „Formiranje naloga“ — klik → `btnFormiranjeNaloga_Click` — proc: NapraviRadniNalogDrumskiIzDrumskog; preko: InsertRadniNalogDrumski.PostaviNalogIDNaRedove
  - „Promena statusa“ — klik → `button3_Click` — čita: StatusVozila
  - „Dodeli kamion“ — klik → `button2_Click` — otvara: PakovanjeKamiona1
  - „Otvori“ — klik → `button23_Click` — otvara: frmDrumski1, frmDrumski
  - „Otvori“ — klik → `button23_Click` — otvara: frmDrumski1, frmDrumski
  - „Dupliraj zapis“ — klik → `button6_Click` — čita 14 tabela; proc: DuplirajZapisRadniNalogDrumski; preko: InsertRadniNalogDrumski.DuplirajRadniNalogDrumski
  - „Save“ — klik → `btnSnimi_Click` — čita: SistemskePostavke; proc: UpdateRadniNalogInterniZavrsen, UpdateStatusRadniNalogDrumski; preko: InsertRadniNalogDrumski.UpdateStatusRadniNalogDrumski, InsertRadniNalogInterni.UpdRadniNalogInterniZavrsen
  - „Close“ — klik → `button4_Click`
  - „gridGroupingControl1“ — TableControlMouseDown → `GridGroupingControl1_TableControlMouseDown`
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick` — proc: DeleteRadniNalogDrumski; preko: InsertRadniNalogDrumski.DelRadniNalogDrumski
  - „Vozac“ — klik → `button2_Click_1` — otvara: frmFormaZaVozace
  - „gridGroupingControl1“ — TableControlCurrentCellAcceptedChanges → `GridGroupingControl1_TableControlCurrentCellAcceptedChanges` — proc: UpdateStatusRadniNalogDrumski; preko: InsertRadniNalogDrumski.UpdateStatusRadniNalogDrumski

#### frmPregledNaloziZaPrevoz [S]
- **Fajl:** `TrackModal/Dokumeta/frmPregledNaloziZaPrevoz.cs` — „Pregled naloga za prevoz“
- **Stari izgled:** Terminal › Prevoz › Pregled nalozi za prevoz → **frmPregledNaloziZaPrevoz**
- **Otvara:** frmNalogZaPrevoz („Otvori“)
- **Tabele:** `NajavaPrevoza` S
- **Funkcionalnosti:**
  - „Pretraga“ — klik → `button1_Click`
  - „Otvori“ — klik → `button2_Click` — otvara: frmNalogZaPrevoz

#### frmPregledNarucenihManipulacija [N+S]
- **Fajl:** `Dokumenta/frmPregledNarucenihManipulacija.cs` — „Naručene usluga po prevozu“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Formiraj otpremu kamionom“ → **frmOtpremaKontejneraKamionomIzKontejnera** → „Otvori“ → **frmOtpremaKontejnera** → „Usluge pregled“ → **frmPregledNarucenihManipulacija**
- **Stari izgled:** Terminal › Terminalske usluge › Pregled manipulacija po prevozu → **frmPregledNarucenihManipulacija**
- **Tabele:** `NaruceneManipulacije` SD p, `OtpremaKontejnera` S, `Partnerji` S, `PrijemKontejneraVoz` S, `Voz` S, `VrstaManipulacije` S
- **Procedure:** `DeleteNaruceneManipulacije`, `UpdateNijeUradjenoNaruceneManipulacije`, `UpdateUradjenoNaruceneManipulacije`
- **Combobox → tabela:** `cboPrijemVozom` ← `PrijemKontejneraVoz`, `Voz`; `cboPrijemKamionom` ← `PrijemKontejneraVoz`; `cboPrijemVozom` ← `OtpremaKontejnera`, `Voz`; `cboPrijemKamionom` ← `OtpremaKontejnera`; `cboPrijemVozom` ← `PrijemKontejneraVoz`, `Voz`; `cboPrijemKamionom` ← `PrijemKontejneraVoz`; `cboPrijemVozom` ← `Voz`
- **Funkcionalnosti:**
  - „Pretraži“ — klik → `button1_Click`
  - „Pretraži“ — klik → `btnPretrazi_Click`
  - „Izbriši manipulaciju“ — klik → `button2_Click` — proc: DeleteNaruceneManipulacije; preko: InsertNaruceneManipulacije.DeleteNarManipulacija
  - „Obeleži urađeno“ — klik → `button3_Click` — proc: UpdateUradjenoNaruceneManipulacije; preko: InsertNaruceneManipulacije.UpdateUradjeno
  - „Obeleži nije urađeno“ — klik → `button4_Click` — proc: UpdateNijeUradjenoNaruceneManipulacije; preko: InsertNaruceneManipulacije.UpdateNijeUradjeno
  - „Prijem“ — čekiranje → `chkPrijem_CheckedChanged`
  - „Otprema“ — čekiranje → `chkOtprema_CheckedChanged`
  - „Pretraži po kontejneru“ — klik → `button5_Click`
  - „Pretraži po kontejneru“ — klik → `button6_Click`
  - `this` — otvaranje forme → `frmPregledNarucenihManipulacija_Load` — čita: OtpremaKontejnera, PrijemKontejneraVoz, Voz

#### frmPregledNerasporedjeni [S]
- **Fajl:** `Uvoz/frmPregledNerasporedjeni.cs` — „PREGLED NERASPOREDJENI KONTEJNERI“
- **Stari izgled:** Uvoz › Uvoz obrada › Neraspoređen kontejneri tabela → **frmPregledNerasporedjeni**
- **Otvara:** Uvoz („Novi“), frmUvozKopirajKontejner („Kopiraj“)
- **Tabele:** `Brodovi` S, `Carinarnice` S, `DirigacijaKontejneraZa` S, `KontejnerskiTerminali` S, `MestaUtovara` S, `Partnerji` S, `Predefinisaneporuke` S, `Scenario` S, `TipKontenjera` S, `uvNacinPakovanja` S, `Uvoz` SD p, `UvozNapomenePozicioniranja` S, `UvozNHM` S, `VrstaCarinskogPostupka` S, `VrstaRobeADR` S, `VrstePostupakaUvoz` S
- **Procedure:** `DeleteUvoz`
- **Funkcionalnosti:**
  - „Pošalji mail infrastrukturi“ — ItemClicked → `toolStrip1_ItemClicked`
  - „Refresh“ — klik → `toolStripButton1_Click`
  - „Otvori“ — klik → `toolStripButton2_Click` — otvara: Uvoz
  - „Novi“ — klik → `toolStripButton3_Click` — otvara: Uvoz
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - „Kopiraj“ — klik → `button1_Click` — otvara: frmUvozKopirajKontejner
  - „Refresh“ — klik → `button25_Click`
  - „Otvori“ — klik → `button24_Click` — otvara: Uvoz
  - „Novi“ — klik → `button23_Click` — otvara: Uvoz
  - „Obriši selektovane“ — klik → `button2_Click` — proc: DeleteUvoz; preko: InsertUvoz.DelUvoz

#### frmPregledOtprema [S]
- **Fajl:** `Promet/frmPregledOtprema.cs` — „frmPregledOtprema“
- **Stari izgled:** Terminal › Skladišno poslovanje › Otprema usluga → **frmSkladisteOtprema** → „Radni nalog“ → **frmPregledOtprema**
- **Funkcionalnosti:**
  - „Štampa“ — klik → `button2_Click` — izveštaj: rptNalogZaRadMO.rdlc

#### frmPregledOtpreme [N+S]
- **Fajl:** `TrackModal/Dokumeta/frmPregledOtpreme.cs` — „Pregled otpreme vozom“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Voz“ → **frmPrijemIOtpremaVozova4** → „Gate out voz“ → **frmPregledOtpreme**
- **Stari izgled:** Terminal › Prevoz › Otprema kontejnera vozom → **frmPregledOtpreme**
- **Otvara:** frmOtpremaKontejnera („Otvori voz“)
- **Tabele:** `OtpremaKontejnera` S, `OtpremaKontejneraVozStavke` S, `Voz` S
- **Funkcionalnosti:**
  - „Otvori voz“ — klik → `toolStripButton1_Click_1` — otvara: frmOtpremaKontejnera
  - „Novi voz“ — klik → `toolStripButton3_Click` — otvara: frmOtpremaKontejnera
  - „Najavljeni vozovi“ — klik → `toolStripButton5_Click`
  - „Otpremljeni vozovi“ — klik → `toolStripButton6_Click`
  - „?“ — klik → `button3_Click`
  - „?“ — klik → `button2_Click`
  - „Prijem“ — klik → `toolStripButton6_Click`
  - „Najava“ — klik → `toolStripButton5_Click`
  - „Novi voz“ — klik → `toolStripButton3_Click` — otvara: frmOtpremaKontejnera
  - „Otvori “ — klik → `toolStripButton1_Click_1` — otvara: frmOtpremaKontejnera

#### frmPregledOtpremeKamionom [S]
- **Fajl:** `TrackModal/Dokumeta/frmPregledOtpremeKamionom.cs` — „Pregled otpreme kamionom“
- **Stari izgled:** Terminal › Prevoz › Otprema kontejnera kamionom → **frmPregledOtpremeKamionom**
- **Otvara:** frmOtpremaKontejnera („Otvori kamion“)
- **Tabele:** `OtpremaKontejnera` S, `OtpremaKontejneraVozStavke` S
- **Funkcionalnosti:**
  - „Otvori kamion“ — klik → `toolStripButton7_Click`
  - „Otvori kamion UVOZ“ — klik → `toolStripButton4_Click`
  - „Otvori kamion“ — klik → `toolStripButton1_Click` — otvara: frmOtpremaKontejnera
  - „Osveži“ — klik → `toolStripButton2_Click`
  - „Novi kamion“ — klik → `toolStripButton3_Click` — otvara: frmOtpremaKontejnera
  - „Otprema najava“ — klik → `toolStripButton5_Click`
  - „Otpremljeni kamioni“ — klik → `toolStripButton6_Click`
  - „?“ — klik → `button3_Click`
  - „?“ — klik → `button2_Click`
  - „?“ — klik → `button1_Click`
  - „Otvori  kamionom“ — klik → `toolStripButton6_Click`
  - „Najava“ — klik → `toolStripButton5_Click`
  - „Novi kamion“ — klik → `toolStripButton3_Click` — otvara: frmOtpremaKontejnera
  - „Osveži“ — klik → `toolStripButton2_Click`
  - „Otvori prijem kamionom - IZVOZ“ — klik → `toolStripButton1_Click` — otvara: frmOtpremaKontejnera
  - „Otvori prijem kamionom - UVOZ“ — klik → `toolStripButton4_Click`
  - „Otvori“ — klik → `toolStripButton7_Click`

#### frmPregledPlanovaUtovara [S]
- **Fajl:** `Uvoz/frmPregledPlanovaUtovara.cs` — „Pregled Planova utovara“
- **Stari izgled:** Uvoz › Uvoz obrada › Planovi utovara → **frmPregledPlanovaUtovara**
- **Otvara:** frmUvozKonacna („Otvori“), frmUvozKonacnaZaglavlje („Novi plan“), Uvoz („DODAJ KONTEJNER PO SC15“), frmRAdniNalogInterniPUvoz („Pregled uradjeno“)
- **Tabele:** `Partnerji` S, `UvozKonacna` S, `UvozKonacnaZaglavlje` S, `Voz` S
- **Funkcionalnosti:**
  - „Pošalji mail infrastrukturi“ — ItemClicked → `toolStrip1_ItemClicked`
  - „Refresh“ — klik → `toolStripButton1_Click`
  - „Otvori“ — klik → `toolStripButton2_Click` — otvara: frmUvozKonacna
  - „Novi plan utovara“ — klik → `toolStripButton3_Click` — otvara: frmUvozKonacnaZaglavlje
  - „Po planu SC15“ — klik → `toolStripButton4_Click`
  - „Pregled uradjeno“ — klik → `button3_Click` — otvara: frmRAdniNalogInterniPUvoz
  - „DODAJ KONTEJNER PO SC15“ — klik → `toolStripButton4_Click`
  - „Novi plan“ — klik → `toolStripButton3_Click` — otvara: frmUvozKonacnaZaglavlje
  - „Otvori“ — klik → `toolStripButton2_Click` — otvara: frmUvozKonacna
  - „Refresh“ — klik → `toolStripButton1_Click`
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`

#### frmPregledPlanoviUtovaraDrumski [S]
- **Fajl:** `Uvoz/frmPregledPlanoviUtovaraDrumski.cs` — „Pregled planovi utovara Drumski“
- **Stari izgled:** Uvoz › Uvoz obrada › Svi planovi utovara -Drumski → **frmPregledPlanoviUtovaraDrumski**
- **Otvara:** frmForrmiranjePlanaDrumski („Novi plan utovara“)
- **Tabele:** `UvozKonacnaZaglavlje` S
- **Funkcionalnosti:**
  - „Refresh“ — klik → `toolStripButton1_Click`
  - „Otvori“ — klik → `toolStripButton2_Click` — otvara: frmForrmiranjePlanaDrumski
  - „Novi plan utovara“ — klik → `toolStripButton3_Click` — otvara: frmForrmiranjePlanaDrumski

#### frmPregledPopisa1 [S]
- **Fajl:** `Servis/frmPregledPopisa1.cs` — „Pregled popisa lokomotive“
- **Stari izgled:** Održavanje › Popis lokomotiva › Popis pregled 1 → **frmPregledPopisa1**
- **Tabele:** `Delavci` S, `LokomotivaPopis` S, `LokomotivaPrijava` S, `LokomotiveVrstePopisa` S, `Mesta` S, `VrstaPopisa` S
- **Funkcionalnosti:**
  - „Popisne stavke po lokomotivi 500“ — klik → `button1_Click`
  - „Zadnji popisi po lokomotivama“ — klik → `button2_Click`
  - „Neslaganja sa Referetnom kolicinom 500“ — klik → `button3_Click`
  - `this` — otvaranje forme → `frmPregledPopisa1_Load` — čita: Mesta

#### frmPregledPopisa2 [S]
- **Fajl:** `SyncForm/frmPregledPopisa2.cs` — „Pregled popisa 2“
- **Stari izgled:** Održavanje › Popis lokomotiva › Popis pregled 2 → **frmPregledPopisa2**
- **Tabele:** `Delavci` S, `LokomotivaPopis` S, `LokomotivaPrijava` S, `LokomotiveVrstePopisa` S, `VrstaPopisa` S
- **Funkcionalnosti:**
  - `this` — otvaranje forme → `frmPregledPopisa2_Load` — čita: Delavci, LokomotivaPopis, LokomotivaPrijava, LokomotiveVrstePopisa, VrstaPopisa

#### frmPregledPorudzbenica [N]
- **Fajl:** `Izvoz/frmPreglegPorudzbenica.cs` — „frmPregledPorudzbenica“
- **Novi izgled:** Logistika izvoza → **LogistikaIzvoza1** → „Porudžbenica“ → **LogistikaIzvoza0** → „Izmeni postojeći“ → **frmPregledPorudzbenica**
- **Otvara:** frmProdajniNalogIzvoz („Otvori“)
- **Tabele:** `Partnerji` S, `ProdajniNalogIzvoz` SD p, `ProdajniNalogIzvozStavke` S
- **Procedure:** `DeleteProdajniNalogIzvoz`
- **Funkcionalnosti:**
  - „gridGroupingControl1“ — TableControlMouseDown → `gridGroupingControl1_TableControlMouseDown`
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick_1` — proc: DeleteProdajniNalogIzvoz; preko: InsertProdajniNalogIzvoz.DelPorudzbenice
  - „Otvori“ — klik → `btnOtvori_Click` — otvara: frmProdajniNalogIzvoz

#### frmPregledPorukeVozacu [N+S]
- **Fajl:** `Drumski/frmPregledPorukeVozacu.cs` — „frmPregledPorukeVozacu“
- **Novi izgled:** Drumski transport → **Drumski1** → „Cerada“ → **Cerade** → „FORMIRANJE\r\nTRANSPORTNOG NALOGA“ → **PakovanjeKamionaCerade** → `dataGridView3` (CellClick) → **frmPregledPorukeVozacu**
- **Stari izgled:** Drumski › Drumski › Pakovanje kamiona → **PakovanjeKamiona1** → `dataGridView3` (CellClick) → **frmPregledPorukeVozacu**
- **Tabele:** `Delavci` S, `Korisnici` S
- **Procedure:** `InsertTokens`, `UpdateRadniNalogDrumskiPoslateInstrukcije`
- **Funkcionalnosti:**
  - „Pošalji“ — klik → `button1_Click` — proc: InsertTokens, UpdateRadniNalogDrumskiPoslateInstrukcije; preko: InsertRadniNalogDrumski.PoslateInstrukcije, InsertRadniNalogDrumski.SnimiToken
  - „Zatvori“ — klik → `button2_Click`

#### frmPregledPutnihNaloga [S]
- **Fajl:** `TrackModal/Dokumeta/frmPregledPutnihNaloga.cs` — „Putni Nalozi“
- **Stari izgled:** Terminal › Transport › Pregled puti nalozi → **frmPregledPutnihNaloga**
- **Otvara:** frmPutniNalog („Otvori putni nalog“)
- **Tabele:** `PutniNalog` S, `Vozila` S, `Zaposleni` S
- **Funkcionalnosti:**
  - „Otvori putni nalog“ — klik → `toolStripButton1_Click` — otvara: frmPutniNalog
  - „Osveži“ — klik → `toolStripButton2_Click` — čita: PutniNalog, Vozila, Zaposleni

#### frmPregledRadniNaloziTransport [S]
- **Fajl:** `TrackModal/Dokumeta/frmPregledRadniNaloziTransport.cs` — „Pregled radni nalozi transport“
- **Stari izgled:** Terminal › Transport › Pregled radni nalozi transport → **frmPregledRadniNaloziTransport**
- **Otvara:** frmRadniNalogTransport („Otvori radni nalog“)
- **Tabele:** `RadniNalogTransport` S, `Vozila` S, `Zaposleni` S
- **Funkcionalnosti:**
  - „Otvori radni nalog“ — klik → `toolStripButton1_Click` — otvara: frmRadniNalogTransport
  - „Osveži“ — klik → `toolStripButton2_Click` — čita: RadniNalogTransport, Vozila, Zaposleni

#### frmPregledRID [S]
- **Fajl:** `Dokumenta/frmPregledRID.cs` — „Pregled RID“
- **Stari izgled:** Komercijala › RID › RID → **frmPregledRID**
- **Tabele:** `Najava` S, `TeretnicaIskljuceniVagoni` S, `TeretnicaStavke` S
- **Funkcionalnosti:**
  - „P“ — klik → `button4_Click` — čita: Najava, TeretnicaIskljuceniVagoni, TeretnicaStavke
  - „P“ — klik → `button1_Click` — čita: Najava, TeretnicaIskljuceniVagoni, TeretnicaStavke

#### frmPregledSkeniranihDokumenata [S]
- **Fajl:** `Drumski/frmPregledSkeniranihDokumenata.cs` — „frmPregledSkeniranihDokumenata“
- **Stari izgled:** Drumski › Finansije › Lista naloga → **frmPregledFaktura** → „Pregled i izmena podataka fakture“ → **frmFaktureDetalji** → „Pregled skeniranih dokumenata“ → **frmPregledSkeniranihDokumenata**
- **Tabele:** `Automobili` S, `Delavci` S, `DokumentaFaktureDrumski` S, `Partnerji` S, `RadniNalogDrumski` S
- **Procedure:** `DeleteDokumentFaktureDrumski`
- **Funkcionalnosti:**
  - `dataGridView1` — klik na ćeliju → `dataGridView1_CellContentClick` — proc: DeleteDokumentFaktureDrumski; preko: InsertFakture.DelDokument

#### frmPregledSkladistaNovi [N+S]
- **Fajl:** `RadniNalozi/frmPregledSkladistaNovi.cs` — „VIZUELNI PREGLED SKLADISTA“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Voz“ → **frmPrijemIOtpremaVozova4** → „RN prijem voza“ → **RN1PrijemVoza** → „Dodela polja“ → **frmDodelaSkladista** → „MAPA SKLADISTA“ → **frmPregledSkladistaNovi**
- **Stari izgled:** Terminal › Terminalske usluge › Pregled skladista → **frmPregledSkladistaNovi**
- **Tabele:** `KontejnerTekuce` S, `Skladista` S
- **Funkcionalnosti:**
  - `this` — otvaranje forme → `frmPregledSkladistaNovi_Load` — čita: KontejnerTekuce, Skladista

#### frmPregledSkladistePrijem [S]
- **Fajl:** `Promet/frmPregledSkladistePrijem.cs` — „Pregled skladište prijem“
- **Stari izgled:** Terminal › Skladišno poslovanje › Pregled prijema raspored → **frmPregledSkladistePrijem**
- **Tabele:** `Pozicija` S, `PrijemKontejneraVoz` S, `PrijemKontejneraVozStavke` S, `Promet` S, `Skladista` S, `Voz` S
- **Funkcionalnosti:**
  - „Pretraži“ — klik → `button1_Click`
  - „Pretraži“ — klik → `btnPretrazi_Click`
  - `this` — otvaranje forme → `frmPregledSkladistePrijem_Load` — čita: PrijemKontejneraVoz, Voz

#### frmPregledTovarnihListova [S]
- **Fajl:** `TrackModal/Dokumeta/frmPregledTovarnihListova.cs` — „Pregled Tovarnih listova“
- **Stari izgled:** Terminal › Prevoz › Pregled tovarni listovi → **frmPregledTovarnihListova**
- **Otvara:** frmTovarniList („Otvori“)
- **Tabele:** `Partnerji` S, `TovarniList` S
- **Funkcionalnosti:**
  - „Tovarni listovi“ — klik → `toolStripButton1_Click` — čita: Partnerji, TovarniList
  - „Novi“ — klik → `toolStripButton2_Click`
  - „Otvori“ — klik → `toolStripButton4_Click_1`

#### frmPregledVozova [N+S]
- **Fajl:** `TrackModal/Dokumeta/frmPregledVozova.cs` — „Pregled vozova“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Voz“ → **frmPrijemIOtpremaVozova4** → „Pregled vozova“ → **frmPregledVozova**
- **Stari izgled:** Terminal › Prevoz › Pregled vozova → **frmPregledVozova**
- **Otvara:** frmVoz („Otvori“)
- **Tabele:** `Partnerji` S, `UvozKonacna` S, `UvozKonacnaZaglavlje` S, `Voz` S
- **Procedure:** `PrekopirajVoz`
- **Funkcionalnosti:**
  - „Vozovi otvori“ — klik → `toolStripButton1_Click_1` — otvara: frmVoz
  - „Dolazeći“ — klik → `toolStripButton2_Click_1` — čita: Partnerji, UvozKonacna, UvozKonacnaZaglavlje, Voz; izvoz u Excel
  - „Odlazeći“ — klik → `toolStripButton4_Click` — čita: Voz; izvoz u Excel
  - „Kopiraj voz“ — klik → `toolStripButton5_Click` — proc: PrekopirajVoz; preko: InsertVoz.PrekopirajVoz
  - „Kopiraj“ — klik → `toolStripButton5_Click` — proc: PrekopirajVoz; preko: InsertVoz.PrekopirajVoz
  - „Odlazeći“ — klik → `toolStripButton4_Click` — čita: Voz; izvoz u Excel
  - „Dolazeći“ — klik → `toolStripButton2_Click_1` — čita: Partnerji, UvozKonacna, UvozKonacnaZaglavlje, Voz; izvoz u Excel
  - „Otvori“ — klik → `toolStripButton1_Click_1` — otvara: frmVoz
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`

#### frmPrekovremeniRad [S]
- **Fajl:** `Dokumenta/frmPrekovremeniRad.cs` — „frmPrekovremeniRad“
- **Stari izgled:** Kadrovi › Osnovni podaci › Rad noću/Prekovremeni → **frmPrekovremeniRad**
- **Tabele:** `Delavci` S, `PrekovremeiRad` S, `PrekovremeniRad` SIUD p
- **Procedure:** `DeletePrekovremeniRad`, `InsertPrekovremeniRad`, `UpdatePrekovremeniRad`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertPrekovremeniRad, UpdatePrekovremeniRad; preko: InsertPrekovremeniRad.InsPrekovremeniRad, InsertPrekovremeniRad.UpdPrekovremeniRad
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeletePrekovremeniRad; preko: InsertPrekovremeniRad.DeletePrekovremeniRad
  - „Prikaži sve“ — klik → `button2_Click` — čita: Delavci, PrekovremeiRad
  - „Prikaži“ — klik → `button1_Click`
  - `this` — otvaranje forme → `frmPrekovremeniRad_Load` — čita: Delavci

#### frmPretovariSkladista [N]
- **Fajl:** `MainLeget/Pretovari/frmPretovariSkladista.cs` — „frmPretovariSkladista“
- **Novi izgled:** Operacije pretovari → **PretovariMain** → „Skladišta“ → **frmPretovariSkladista**

#### frmPretovarPrijem [N]
- **Fajl:** `MainLeget/Pretovari/frmPretovarPrijem.cs` — „Prijem“
- **Novi izgled:** Operacije pretovari → **PretovariMain** → „Pretovar“ → **PretovariPrijem** → „Prijem“ → **frmPretovarPrijem**
- **Otvara:** RN12MedjuskladisniKontejnera („Radni nalog kalmar“), frmPrijemKontejneraKamionLegetUvoz („Radni nalog kalmar“), Prijemnica („Radni nalog pretovar“), Otpremnica („Radni nalog pretovar“)
- **Tabele:** `IzvozKonacna` S, `organizacioneJedinice` S, `PrijemKontejneraVozStavke` S, `RadniNalogInterni` S, `RadniNalogInterniPotvrda` S, `RNMedjuskladisni` S
- **Procedure:** `DozvoliIzlaz`, `PromeniStatusKalmar`, `PromeniStatusPretovareno`, `spPrenesiKontejnerIzPlanaNaPrijemnicuIzvozPoIdNadredjenog`
- **Funkcionalnosti:**
  - „Kalmar“ — klik → `button5_Click` — proc: PromeniStatusKalmar, PromeniStatusPretovareno; preko: InsertRadniNalogInterni.PromeniStatusKalmar, InsertRadniNalogInterni.PromeniStatusPretovareno
  - „Refresh“ — klik → `button8_Click`
  - „Dozvoli izlaz“ — klik → `button4_Click` — proc: DozvoliIzlaz; preko: InsertRadniNalogInterni.DozvoliIzlaz
  - „Radni nalog vaga“ — klik → `button2_Click`
  - „Radni nalog pretovar“ — klik → `button1_Click` — otvara: Prijemnica, Otpremnica, RN12MedjuskladisniKontejnera; čita: RNMedjuskladisni, RadniNalogInterni
  - „Radni nalog kalmar“ — klik → `button27_Click` — otvara: RN12MedjuskladisniKontejnera; proc: spPrenesiKontejnerIzPlanaNaPrijemnicuIzvozPoIdNadredjenog; preko: InsertUvozKonacna.PrenesiKontejnerIzPlanaNaPrijemnicuIzvozIDNadredjenog

#### frmPrijavaKvara [S]
- **Fajl:** `Servis/frmPrijavaKvara.cs` — „Prijava kvara“
- **Stari izgled:** Održavanje › Obrada kvarova › Prijava kvara → **frmPrijavaKvara**
- **Tabele:** `Delavci` S, `EvidencijaKvarova` SD p, `GrupaKvarova` S, `Kvarovi` S, `Mesta` S, `StatusKvara` S
- **Procedure:** `DeleteEvidencijaKvarova`, `InsertEvidencijaKvarovaCentralno`, `UpdateEvidencijaKvarovaCentralno`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertEvidencijaKvarovaCentralno, UpdateEvidencijaKvarovaCentralno; preko: InsertPrijavaKvara.InsPrijavaKvarovi, InsertPrijavaKvara.UpdPrijavaKvarovi
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteEvidencijaKvarova; preko: InsertPrijavaKvara.DeletePrijavaKvarovi
  - `this` — otvaranje forme → `frmPrijavaKvara_Load` — čita: Delavci, GrupaKvarova, Kvarovi, Mesta, StatusKvara

#### frmPrijavaKvaraAuto [S]
- **Fajl:** `Servis/frmPrijavaKvaraAuto.cs` — „Prijava kvara automobili“
- **Stari izgled:** Održavanje › Automobili › Prijava kvara Auto → **frmPrijavaKvaraAuto**
- **Tabele:** `Automobili` S, `Delavci` S, `EvidencijaKvarovaAuto` S, `GrupaKvarovaAuto` S, `KvaroviAuto` S, `StatusKvaraAuto` S
- **Procedure:** `sp_DeletePrijavaKvarAuto`, `sp_InsertPrijavaKvaraAuto`, `sp_UpdatePrijavaKvaraAuto`
- **Funkcionalnosti:**
  - „toolStrip1“ — ItemClicked → `toolStrip1_ItemClicked`
  - „New“ — klik → `tsb_New_Click`
  - „Save“ — klik → `tsb_Save_Click` — proc: sp_InsertPrijavaKvaraAuto, sp_UpdatePrijavaKvaraAuto; preko: InsertPrijavaKvaraAuto.InsertPrijava, InsertPrijavaKvaraAuto.UpdatePrijava
  - „Delete“ — klik → `tsb_Delete_Click` — proc: sp_DeletePrijavaKvarAuto; preko: InsertPrijavaKvaraAuto.DeletePrijava
  - „Otvori“ — klik → `btn_OtvoriSliku_Click`
  - „>>“ — klik → `btn_Napred_Click`
  - „<<“ — klik → `btn_nazad_Click`
  - „Otvori folder“ — klik → `button1_Click`
  - „Promeni više“ — klik → `txt_Vise_Click` — proc: sp_UpdatePrijavaKvaraAuto; preko: InsertPrijavaKvaraAuto.UpdatePrijava

#### frmPrijavaMasinovodje [S]
- **Fajl:** `Servis/frmPrijavaMasinovodje.cs` — „Prijave - Odjave Mašinovođe“
- **Stari izgled:** Evidencije › Prijave i odjave › Prijava odjava mašinovođe → **frmPrijavaMasinovodje**
- **Tabele:** `Aktivnosti` S, `AktivnostiStavke` S, `Delavci` S, `LokomotivaPrijava` SIUD p, `Mesta` S, `Stanice` S
- **Procedure:** `DeleteLokomotivaPrijava`, `InsertLokomotivaPrijava`, `UpdateLokomotivaPrijava`
- **Funkcionalnosti:**
  - „Ažuriraj podatke“ — klik → `metroButton1_Click`
  - „Briši zapis“ — klik → `metroButton2_Click` — proc: DeleteLokomotivaPrijava; preko: InsertPrijavaMasinovodje.DeletePrijavaMasinovodje
  - „Promeni zapis“ — klik → `metroButton3_Click` — proc: UpdateLokomotivaPrijava; preko: InsertPrijavaMasinovodje.UpdatePrijavaMasinovodje
  - „Dodaj zapis“ — klik → `metroButton4_Click` — proc: InsertLokomotivaPrijava; preko: InsertPrijavaMasinovodje.InsertPrijavaMasin
  - „Odjavi prijavu“ — klik → `metroButton5_Click` — proc: InsertLokomotivaPrijava; preko: InsertPrijavaMasinovodje.InsertPrijavaMasin
  - „Prikazi zadnjih n Zapisa“ — klik → `metroButton6_Click`
  - „Prikazi zapise od datuma“ — klik → `metroButton7_Click`
  - `this` — otvaranje forme → `frmPrijavaMasinovodje_Load` — čita: Delavci, Mesta, Stanice

#### frmPrijavaMasinovodjeOld [S]
- **Fajl:** `Servis/frmPrijavaMasinovodjeOld.cs` — „Prijave masinovodja prikaz bez posla“
- **Stari izgled:** Evidencije › Prijave i odjave › Prijava odjava masinovodje stari pregled → **frmPrijavaMasinovodjeOld**
- **Tabele:** `Delavci` S, `LokomotivaPrijava` SIUD p, `Mesta` S, `Stanice` S
- **Procedure:** `DeleteLokomotivaPrijava`, `InsertLokomotivaPrijava`, `UpdateLokomotivaPrijava`
- **Funkcionalnosti:**
  - „Odjavi prijavu“ — klik → `metroButton5_Click` — proc: InsertLokomotivaPrijava; preko: InsertPrijavaMasinovodje.InsertPrijavaMasin
  - „Dodaj zapis“ — klik → `metroButton4_Click` — proc: InsertLokomotivaPrijava; preko: InsertPrijavaMasinovodje.InsertPrijavaMasin
  - „Promeni zapis“ — klik → `metroButton3_Click` — proc: UpdateLokomotivaPrijava; preko: InsertPrijavaMasinovodje.UpdatePrijavaMasinovodje
  - „Briši zapis“ — klik → `metroButton2_Click` — proc: DeleteLokomotivaPrijava; preko: InsertPrijavaMasinovodje.DeletePrijavaMasinovodje
  - „Ažuriraj podatke“ — klik → `metroButton1_Click`
  - `this` — otvaranje forme → `frmPrijavaMasinovodje_Load` — čita: Delavci, Mesta, Stanice

#### frmPrijavaSmene [S]
- **Fajl:** `Mobile/frmPrijavaSmene.cs` — „Prijava odjava smene“
- **Stari izgled:** Evidencije › Prijave i odjave › Prijave/Odjave terenci tel → **frmPrijavaSmene**
- **Tabele:** `Aktivnosti` S, `AktivnostiStavke` S, `Delavci` S, `ZaposleniPrijava` SD p
- **Procedure:** `DeleteZaposleniPrijava`
- **Funkcionalnosti:**
  - „Ažuriraj“ — klik → `metroButton1_Click`
  - „Aktivni“ — klik → `metroButton2_Click`
  - „Izbriši selektovan“ — klik → `metroButton3_Click` — proc: DeleteZaposleniPrijava; preko: InsertOpste.DeleteZaposleniZapis
  - „Prikazi zadnjih n Zapisa“ — klik → `metroButton4_Click`
  - „Prikazi od vremena“ — klik → `metroButton7_Click`

#### frmPrijavaSmeneOld [S]
- **Fajl:** `Mobile/frmPrijavaSmeneOld.cs` — „Prijavljene smene sa bez aktivnosti“
- **Stari izgled:** Evidencije › Prijave i odjave › Prijave odjave stari pogled → **frmPrijavaSmeneOld**
- **Tabele:** `Delavci` S, `ZaposleniPrijava` SD p
- **Procedure:** `DeleteZaposleniPrijava`
- **Funkcionalnosti:**
  - „Izbriši selektovan“ — klik → `metroButton3_Click` — proc: DeleteZaposleniPrijava; preko: InsertOpste.DeleteZaposleniZapis
  - „Aktivni“ — klik → `metroButton2_Click`
  - „Ažuriraj“ — klik → `metroButton1_Click`

#### frmPrijemIOtpremaCerada [N]
- **Fajl:** `MainLeget/PrijemIOtpremaKamiona/frmPrijemIOtpremaCerada.cs` — „frmPrijemIOtpremaCerada“
- **Novi izgled:** Prijem i otprema kamiona → **frmPrijemIOtpremaKamiona1** → „CERADA“ → **frmPrijemIOtpremaCerada**
- **Otvara:** frmCerada („Prijem“), frmCeradaOtprema („Otprema“), VaganjePregled („Vaga“)
- **Funkcionalnosti:**
  - „Vaga“ — klik → `btnPrijemIOtprema3_Click` — otvara: VaganjePregled
  - „Otprema“ — klik → `btnPrijemIOtprema2_Click` — otvara: frmCeradaOtprema
  - „Prijem“ — klik → `btnPrijemIOtprema1_Click` — otvara: frmCerada

#### frmPrijemIOtpremaKamiona1 [N]
- **Fajl:** `MainLeget/PrijemIOtpremaKamiona/frmPrijemIOtpremaKamiona1.cs` — „frmPrijemIOtpremaKamiona1“
- **Novi izgled:** Prijem i otprema kamiona → **frmPrijemIOtpremaKamiona1**
- **Otvara:** Pregledac („PREGLEDAČ“), frmPrijemIOtpremaPlatforma („PLATFORMA“), frmPrijemIOtpremaCerada („CERADA“)
- **Funkcionalnosti:**
  - „Prijem i otprema kamiona“ — klik → `label2_Click`
  - „PLATFORMA“ — klik → `btnPrijemIOtpremaKamiona8_Click` — otvara: frmPrijemIOtpremaPlatforma
  - „CERADA“ — klik → `btnPrijemIOtpremaKamiona9_Click` — otvara: frmPrijemIOtpremaCerada
  - „PREGLEDAČ“ — klik → `btnPrijemIOtpremaKamiona11_Click` — otvara: Pregledac

#### frmPrijemIOtpremaPlatforma [N]
- **Fajl:** `MainLeget/PrijemIOtpremaKamiona/frmPrijemIOtpremaPlatforma.cs` — „frmPrijemIOtpremaPlatforma“
- **Novi izgled:** Prijem i otprema kamiona → **frmPrijemIOtpremaKamiona1** → „PLATFORMA“ → **frmPrijemIOtpremaPlatforma**
- **Otvara:** frmPlatforma („Prijem“), VaganjePregled („Vaga“), frmPlatformaOtprema („Otprema“)
- **Funkcionalnosti:**
  - „Prijem“ — klik → `btnPrijemIOtprema1_Click` — otvara: frmPlatforma
  - „Otprema“ — klik → `btnPrijemIOtprema2_Click` — otvara: frmPlatformaOtprema
  - „Vaga“ — klik → `btnPrijemIOtprema3_Click` — otvara: VaganjePregled

#### frmPrijemIOtpremaVozova2 [N]
- **Fajl:** `MainLeget/Prijemiotpremavozova/frmPrijemIOtpremaVozova2.cs` — „frmPrijemIOtpremaVozova2“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Depo cnt“ → **frmPrijemIOtpremaVozova2**
- **Otvara:** TerminalMapFRM („Pregled skladišta“), frmPopisKonterjnera („Popis cnt“), frmCIRPregled („CIR“)
- **Funkcionalnosti:**
  - „Popis cnt“ — klik → `btnPrijemIOtpremaVozova22_Click` — otvara: frmPopisKonterjnera
  - „CIR“ — klik → `btnPrijemIOtpremaVozova23_Click` — otvara: frmCIRPregled
  - „Pregled skladišta“ — klik → `btnPrijemIOtpremaVozova21_Click` — otvara: TerminalMapFRM

#### frmPrijemIOtpremaVozova3 [N]
- **Fajl:** `MainLeget/Prijemiotpremavozova/frmPrijemIOtpremaVozova3.cs` — „frmPrijemIOtpremaVozova3“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Pregled naloga“ → **frmPrijemIOtpremaVozova3**
- **Otvara:** frmAnalizaRadnihNaloga („Pregled radnih naloga“), DogovoreniPosloviIzvoza („Dogovoreni poslovi izvoza“)
- **Funkcionalnosti:**
  - „Dogovoreni poslovi izvoza“ — klik → `btnPrijemIOtpremaVozova32_Click` — otvara: DogovoreniPosloviIzvoza
  - „Pregled radnih naloga “ — klik → `btnPrijemIOtpremaVozova31_Click` — otvara: frmAnalizaRadnihNaloga

#### frmPrijemIOtpremaVozova4 [N]
- **Fajl:** `MainLeget/Prijemiotpremavozova/frmPrijemIOtpremaVozova4.cs` — „frmPrijemIOtpremaVozova4“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Voz“ → **frmPrijemIOtpremaVozova4**
- **Otvara:** frmDopunaPlanaPraznimIzvoz („Dopuna plana praznim cnt“), RN1PrijemVoza („RN prijem voza“), RN2OtpremaVoza („RN otprema voza“), frmVoz („Vozovi“), frmPregledVozova („Pregled vozova“), frmPrijemVozomPregled („Gate in voz“), frmPregledOtpreme („Gate out voz“)
- **Funkcionalnosti:**
  - „RN prijem voza“ — klik → `btnPrijemIOtpremaVozova42_Click` — otvara: RN1PrijemVoza
  - „Dopuna plana praznim cnt“ — klik → `btnPrijemIOtpremaVozova41_Click` — otvara: frmDopunaPlanaPraznimIzvoz
  - „RN otprema voza“ — klik → `btnPrijemIOtpremaVozova43_Click` — otvara: RN2OtpremaVoza
  - „Vozovi“ — klik → `btnPrijemIOtpremaVozova44_Click` — otvara: frmVoz
  - „Pregled vozova“ — klik → `btnPrijemIOtpremaVozova45_Click` — otvara: frmPregledVozova
  - „Gate in voz“ — klik → `btnPrijemIOtpremaVozova46_Click` — otvara: frmPrijemVozomPregled
  - „Gate out voz“ — klik → `btnPrijemIOtpremaVozova47_Click` — otvara: frmPregledOtpreme

#### frmPrijemKamionaDetalji [N]
- **Fajl:** `MainLeget/PrijemIOtpremaKamiona/frmPrijemKamionaDetalji.cs` — „Prijem kamiona detalji“
- **Novi izgled:** Prijem i otprema kamiona → **frmPrijemIOtpremaKamiona1** → „PLATFORMA“ → **frmPrijemIOtpremaPlatforma** → „Prijem“ → **frmPlatforma** → „Pregled“ → **frmPrijemKamionaDetalji**
- **Tabele:** `Carinarnice` S, `InspekciskiTretman` S, `IzvozKonacna` S, `IzvozKonacnaNHM` S, `KontejnerskiTerminali` S, `MestaUtovara` S, `NHM` S, `Partnerji` S, `partnerjiKontOsebaMU` S, `TipKontenjera` S, `uvKvalitetKontejnera` S, `uvNacinPakovanja` S, `VrstaCarinskogPostupka` S, `VrstaRobeADR` S, `VrstaVozila` S
- **Combobox → tabela:** `cboNalogodavac` ← `KontejnerskiTerminali`, `MestaUtovara`, `Partnerji`; `cboKvalitetKontejnera` ← `uvKvalitetKontejnera`; `cboVrstaKamiona` ← `VrstaVozila`; `cboPolaznaCarinarnica` ← `Carinarnice`; `cboSpediterPolazna` ← `Partnerji`; `cboOdredisnaCarinarnica` ← `Carinarnice`; `cboSpediterOdredisna` ← `Partnerji`; `cboCarinskiPUnutrasniTransport` ← `VrstaCarinskogPostupka`; `cboVrstaKontejnera` ← `TipKontenjera`; `cboADR` ← `VrstaRobeADR`; `cboInspekciskiTretman` ← `InspekciskiTretman`; `cboNacinPakovanja` ← `uvNacinPakovanja`; `cboCilj` ← `partnerjiKontOsebaMU`; `cboKontakt` ← `partnerjiKontOsebaMU`

#### frmPrijemKamionaIzPlana [N+S]
- **Fajl:** `Uvoz/frmPrijemKamionaIzPlana.cs` — „Prijem kamiona iz plana“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Formiraj prijem kamionom“ → **frmPrijemKamionaIzPlana**
- **Stari izgled:** Izvoz › Zadaci › Pregled radnih naloga → **frmRadniNalogInterniPregled** → „Formiraj prijem kamionom“ → **frmPrijemKamionaIzPlana**
- **Tabele:** `DirigacijaKontejneraZa` S, `Partnerji` S, `PrijemKontejneraVoz` S, `PrijemKontejneraVozStavke` S, `TipKontenjera` S, `UvozKonacna` S, `Voz` S, `VrstePostupakaUvoz` S
- **Procedure:** `spPrenesiKontejnerIzPlanaNaPrijemnicu`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click`
  - „Uvoz“ — čekiranje → `checkBox2_CheckedChanged`
  - „Prenesi kontejenr“ — klik → `button1_Click` — proc: spPrenesiKontejnerIzPlanaNaPrijemnicu; preko: InsertUvozKonacna.PrenesiKontejnerIzPlanaNaPrijemnicu

#### frmPrijemKamionaPlatforma [S]
- **Fajl:** `Uvoz/frmPrijemKamionaPlatforma.cs` — „PRIJEM KAMIONA IZ PLATFORME“
- **Stari izgled:** Izvoz › Izvoz › Nerasporedjeni kontejneri Obrazac → **frmIzvoz** → „Formiraj prijem kam plat“ → **frmPrijemKamionaPlatforma**
- **Tabele:** `PrijemKontejneraVoz` S
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click`
  - „Prenesi kontejenr“ — klik → `button1_Click`

#### frmPrijemKontejneraKamionLegetIzvoz [N+S]
- **Fajl:** `Dokumenta/frmPrijemKontejneraKamionLegetIzvoz.cs` — „GATE IN KAMION - IZVOZ“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Potvrdi“ → **frmPrijemVozaIzPlana** → „OTVORI - GATE IN KAMION IZVOZ“ → **frmPrijemKontejneraKamionLegetIzvoz**
- **Stari izgled:** Terminal › Radni nalozi › Prijem kontejenra kamion - N → **frmPrijemKontejneraKamionLegetIzvoz**
- **Otvara:** RN4PrijemPlatforme („Radni nalozi“)
- **Tabele:** `Carinarnice` S, `DirigacijaKontejneraZa` S, `InspekciskiTretman` S, `IzvozKonacna` S, `IzvozKonacnaVrstaManipulacije` S, `KontejnerskiTerminali` S, `KontejnerStatus` S, `organizacioneJedinice` S, `Partnerji` S, `Predefinisaneporuke` S, `PrijemKontejneraVoz` S, `PrijemKontejneraVozStavke` S, `RadniNalogInterni` S, `RNPrijemCirade` S, `RNPrijemPlatforme` S, `RNPrijemPlatforme2` S, `TipKontenjera` S, `uvNacinPakovanja` S, `UvozKonacna` S, `Voz` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaRobeADR` S, `VrstePostupakaUvoz` S
- **Combobox → tabela:** `cboPosiljalac` ← `Partnerji`; `cboPrimalac` ← `Partnerji`; `cboVlasnikKontejnera` ← `Partnerji`; `cboTipKontejnera` ← `TipKontenjera`; `cboInspekciskiTretman` ← `InspekciskiTretman`; `cboBukingPrijema` ← `Voz`; `cboOrganizator` ← `Partnerji`; `cboIzvoznik` ← `Partnerji`; `cboPPCNT` ← `KontejnerskiTerminali`; `cboCarina` ← `Carinarnice`; `cboSpedicija` ← `Partnerji`; `cboReexport` ← `VrstaCarinskogPostupka`; `txtADR` ← `VrstaRobeADR`; `cboPosiljalac` ← `Partnerji`; `cboPrimalac` ← `Partnerji`; `cboVlasnikKontejnera` ← `Partnerji`; `cboTipKontejnera` ← `TipKontenjera`; `cboBukingOtpreme` ← `Voz`; `cboStatusKontejnera` ← `DirigacijaKontejneraZa`; `cboBukingPrijema` ← `Voz`; `cboOrganizator` ← `Partnerji`; `cboPredefinisanePoruke` ← `Predefinisaneporuke`; `cbPostupak` ← `VrstePostupakaUvoz`; `cbNacinPakovanja` ← `uvNacinPakovanja`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click`
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „POŠALJI MAIL“ — klik → `toolStripButton1_Click`
  - „RN ZA PRIJEM PLATFORME“ — klik → `toolStripButton6_Click`
  - „CIR urađen“ — čekiranje → `chkCIRUradjen_CheckedChanged`
  - „IZVOZ“ — čekiranje → `checkBox2_CheckedChanged`
  - „POSLAT E-MAIL PRIJEMA KAMIONA“ — čekiranje → `chkPoslatEmailNajava_CheckedChanged`
  - „IME VOZAČA“ — klik → `label29_Click`
  - „REG BR VOZILA“ — klik → `label30_Click`
  - „ATA:“ — klik → `label18_Click`
  - „STATUS PRIJEMA:“ — klik → `label2_Click`
  - „Ubaci“ — klik → `button1_Click`
  - „PERIOD SKLADIŠTANJA OD:“ — klik → `label17_Click`
  - „Radni nalozi“ — klik → `toolStripButton6_Click`
  - „Pošalji mail“ — klik → `toolStripButton1_Click`
  - `this` — otvaranje forme → `frmPrijemKontejneraKamionLegetIzvoz_Load` — čita 7 tabela

#### frmPrijemKontejneraKamionLegetUvoz [N+S]
- **Fajl:** `Dokumenta/frmPrijemKontejneraKamionLegetUvoz.cs` — „GATE IN KAMION - UVOZ“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Potvrdi“ → **frmPrijemKontejneraKamionLegetUvoz**
- **Stari izgled:** Terminal › Radni nalozi › Prijem kontejnera kamion - N → **frmPrijemKontejneraKamionLegetUvoz**
- **Otvara:** frmDokumentaPrijemKontejneraKamion („Dokumenta prijema“), frmKontejneriNaPrijemnici („?“)
- **Tabele:** `DirigacijaKontejneraZa` S, `Izvoz` S, `IzvozKonacna` S, `IzvozVrstaManipulacije` S, `KontejnerskiTerminali` S, `KontejnerStatus` S, `NHM` S, `organizacioneJedinice` S, `Partnerji` S, `Predefinisaneporuke` S, `PrijemKontejneraVoz` S, `PrijemKontejneraVozStavke` S, `Promet` SI p, `RadniNalogInterni` S, `RNPrijemCirade` S, `RNPrijemPlatforme` S, `TipKontenjera` S, `uvNacinPakovanja` S, `Uvoz` SI, `UvozKonacna` S, `UvozKonacnaNHM` S, `UvozKonacnaVrstaManipulacije` S, `UvozKonacnaZaglavlje` S, `Voz` S, `VoziloUsluga` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaRobeADR` S, `VrstePostupakaUvoz` S
- **Procedure:** `InsertPromet`
- **Combobox → tabela:** `cboPosiljalac` ← `Partnerji`; `cboPrimalac` ← `Partnerji`; `cboVlasnikKontejnera` ← `Partnerji`; `cboTipKontejnera` ← `TipKontenjera`; `cboBukingOtpreme` ← `Voz`; `cboStatusKontejnera` ← `DirigacijaKontejneraZa`; `cboBukingPrijema` ← `Voz`; `cboOrganizator` ← `Partnerji`; `cboPredefinisanePoruke` ← `Predefinisaneporuke`; `cbPostupak` ← `VrstePostupakaUvoz`; `txtADR` ← `VrstaRobeADR`; `cboTipKontejnera` ← `TipKontenjera`; `cboRLTerminal` ← `KontejnerskiTerminali`; `cboSpedicijaRTC` ← `Partnerji`; `cboUvoznik` ← `Partnerji`; `cboCarinskiPostupak` ← `VrstaCarinskogPostupka`; `cbNacinPakovanja` ← `uvNacinPakovanja`; `cboPlanUtovara` ← `UvozKonacnaZaglavlje`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click`
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „Prijem na operativno skladiste“ — klik → `toolStripButton7_Click` — proc: InsertPromet; preko: InsertPromet.InsProm
  - „CIR“ — klik → `toolStripButton5_Click` — otvara: frmCIR
  - „RADNI NALOZI“ — klik → `toolStripDropDownButton1_Click`
  - „PRIJEM PLATFORME - UVOZ“ — klik → `prijemKontejneraToolStripMenuItem_Click`
  - „PRIJEM PLATFORME BRODAR“ — klik → `pRIJEMPLATFORMEBRODARToolStripMenuItem_Click`
  - „PRIJEM  CIRADE“ — klik → `pRIJEMCIRADEToolStripMenuItem_Click`
  - „PREGLED ISPRAZNJENJOG KONTEJNERA“ — klik → `pREGLEDToolStripMenuItem_Click`
  - „DOKUMENTA PRIJEMA“ — klik → `toolStripButton3_Click`
  - „toolStripButton4“ — klik → `toolStripButton4_Click` — otvara: frmNalogZaPrevoz
  - „UVOZ“ — čekiranje → `chkUvoz_CheckedChanged`
  - „?“ — klik → `button7_Click` — otvara: frmKontejneriNaPrijemnici
  - „?“ — klik → `button6_Click`
  - „0“ — TextAlignChanged → `txtKontejnerID_TextAlignChanged`
  - „?“ — klik → `button5_Click`
  - „Promeni sve“ — klik → `button9_Click`
  - „Promeni RB“ — klik → `button4_Click`
  - „Izbriši“ — klik → `button3_Click`
  - „Promeni“ — klik → `button2_Click`
  - „Ubaci“ — klik → `button1_Click`
  - „PLATFORMA“ — čekiranje → `chkPlatforma_CheckedChanged`
  - „TERMINAL“ — čekiranje → `checkBox1_CheckedChanged` — čita: UvozKonacnaZaglavlje
  - „CIRADA“ — čekiranje → `checkBox1_CheckedChanged_1`
  - „Dokumenta prijema“ — klik → `toolStripButton3_Click`

#### frmPrijemKontejneraKamionPregled [S]
- **Fajl:** `TrackModal/Dokumeta/frmPrijemKontejneraKamionPregled.cs` — „Prijem kontejnera kamion“
- **Stari izgled:** Terminal › Prevoz › Prijem kontejnera kamionom pregled → **frmPrijemKontejneraKamionPregled**
- **Tabele:** `PrijemKontejneraVoz` S, `PrijemKontejneraVozStavke` S
- **Funkcionalnosti:**
  - „Otvori prijem kamionom - UVOZ“ — klik → `toolStripButton5_Click`
  - „Otvori prijem kamionom“ — klik → `toolStripButton1_Click` — otvara: frmPrijemKontejneraVoz
  - „Osveži“ — klik → `toolStripButton2_Click`
  - „Novi prijem“ — klik → `toolStripButton4_Click` — otvara: frmPrijemKontejneraVoz
  - „Najava“ — klik → `toolStripButton3_Click`
  - „Prijem“ — klik → `toolStripButton6_Click`
  - „Otvori prijem kamionom“ — klik → `toolStripButton7_Click`
  - „?“ — klik → `button1_Click`
  - „?“ — klik → `button2_Click`
  - „?“ — klik → `button3_Click`
  - „Otvori prijem kamionom“ — klik → `toolStripButton7_Click`
  - „Prijem“ — klik → `toolStripButton6_Click`
  - „Najava“ — klik → `toolStripButton3_Click`
  - „Novi prijem“ — klik → `toolStripButton4_Click` — otvara: frmPrijemKontejneraVoz
  - „Osveži“ — klik → `toolStripButton2_Click`
  - „Otvori prijem kamionom - IZVOZ“ — klik → `toolStripButton1_Click` — otvara: frmPrijemKontejneraVoz
  - „Otvori prijem kamionom - UVOZ“ — klik → `toolStripButton5_Click`

#### frmPrijemKontejneraLegetVozUvoz [N+S]
- **Fajl:** `Dokumenta/frmPrijemKontejneraLegetVozUvoz.cs` — „GATE IN VOZ“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Potvrdi“ → **frmPrijemVozaIzPlana** → „OTVORI - GATE IN VOZ“ → **frmPrijemKontejneraLegetVozUvoz**
- **Stari izgled:** Terminal › Radni nalozi › Prijem kontejnera voz - N → **frmPrijemKontejneraLegetVozUvoz**
- **Otvara:** frmDokumentaPrijemKontejneraVoz („Dokumenta prijema“), frmKontejneriNaPrijemnici („?“), RN1PrijemVoza („Radni nalozi“), frmDodelaSkladista („Odredi pozicije“), frmKontejnerTekuce („Tekuce stanje“), frmPovezivanjeKontejneraIVagona („Povezivanje vagona“)
- **Tabele:** `DirigacijaKontejneraZa` S, `InspekciskiTretman` S, `KontejnerskiTerminali` S, `KontejnerStatus` S, `NHM` S, `organizacioneJedinice` S, `Partnerji` S, `Predefinisaneporuke` S, `PrijemKontejneraVoz` S, `PrijemKontejneraVozStavke` S, `Promet` SI p, `RadniNalogInterni` S, `RNPrijemVoza` S, `Skladista` S, `TipKontenjera` S, `UvozKonacna` S, `UvozKonacnaNapomenePozicioniranja` S, `UvozKonacnaNHM` S, `UvozKonacnaVrstaManipulacije` S, `Voz` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaRobeADR` S, `VrstePostupakaUvoz` S
- **Procedure:** `InsertPromet`
- **Combobox → tabela:** `txtVrstaPregleda` ← `InspekciskiTretman`; `txtADR` ← `VrstaRobeADR`; `cboPosiljalac` ← `Partnerji`; `cboPrimalac` ← `Partnerji`; `cboVlasnikKontejnera` ← `Partnerji`; `cboTipKontejnera` ← `TipKontenjera`; `cboBukingOtpreme` ← `Voz`; `cboStatusKontejnera` ← `DirigacijaKontejneraZa`; `cboUvoznik` ← `Partnerji`; `cboSpedicijaRTC` ← `Partnerji`; `cboCarinskiPostupak` ← `VrstaCarinskogPostupka`; `cboBukingPrijema` ← `Voz`; `cboOrganizator` ← `Partnerji`; `cboPredefinisanePoruke` ← `Predefinisaneporuke`; `cbPostupak` ← `VrstePostupakaUvoz`; `cboOperater` ← `Partnerji`; `cboOperaterHR` ← `Partnerji`; `cboRLTerminal` ← `KontejnerskiTerminali`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click`
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „DOKUMENTACIJA PRIJEMA“ — klik → `toolStripButton3_Click` — otvara: frmDokumentaPrijemKontejneraVoz
  - „RADNI NALOZI FORMIRANJE“ — klik → `toolStripDropDownButton1_Click`
  - „RN - Prijem voza“ — klik → `formirajRadneNalogePrijemVozaToolStripMenuItem_Click`
  - „ODREDI POZICIJE“ — klik → `toolStripButton10_Click` — otvara: frmDodelaSkladista
  - „Prijem na OP S“ — klik → `toolStripButton7_Click` — proc: InsertPromet; preko: InsertPromet.InsProm
  - „CIR“ — klik → `toolStripButton5_Click` — otvara: frmCIR
  - „toolStripButton4“ — klik → `toolStripButton4_Click` — otvara: frmNalogZaPrevoz
  - „OTPREMA KAMIONOM - PLAT“ — klik → `toolStripButton6_Click_1`
  - „PREGLED RN“ — klik → `toolStripButton8_Click`
  - „PRIJEM KAMIONOM - PLAT“ — klik → `toolStripButton9_Click_1` — otvara: frmPrijemKontejneraKamionLegetUvoz
  - „POVEZIVANJE VAGONA“ — klik → `toolStripButton11_Click` — otvara: frmPovezivanjeKontejneraIVagona
  - „TEKUCE STANJE“ — klik → `toolStripButton12_Click`
  - „CIR urađen“ — čekiranje → `chkCIRUradjen_CheckedChanged`
  - „Prijem e-mail“ — čekiranje → `chkPoslatEmailPrijem_CheckedChanged`
  - „POSLAT E-MAIL PRIJEMA VOZA NA UVOZ“ — čekiranje → `chkPoslatEmailNajava_CheckedChanged`
  - „Predato transportu“ — čekiranje → `chkTransport_CheckedChanged`
  - „TARA KONT TER“ — klik → `label52_Click`
  - „?“ — klik → `button7_Click` — otvara: frmKontejneriNaPrijemnici
  - „?“ — klik → `button6_Click`
  - „?“ — klik → `button5_Click`
  - „Promeni RB“ — klik → `button4_Click`
  - „Izbriši“ — klik → `button3_Click`
  - „Promeni“ — klik → `button2_Click`
  - „Ubaci“ — klik → `button1_Click`
  - „Tekuce stanje“ — klik → `toolStripButton12_Click`
  - „Povezivanje vagona“ — klik → `toolStripButton11_Click` — otvara: frmPovezivanjeKontejneraIVagona
  - „Odredi pozicije“ — klik → `toolStripButton10_Click` — otvara: frmDodelaSkladista
  - „Radni nalozi“ — klik → `formirajRadneNalogePrijemVozaToolStripMenuItem_Click`
  - „Dokumenta prijema“ — klik → `toolStripButton3_Click` — otvara: frmDokumentaPrijemKontejneraVoz

#### frmPrijemnicaCarinskaStampa [S]
- **Fajl:** `Carinsko/frmPrijemnicaCarinskaStampa.cs` — „Štampa prijemnice“
- **Stari izgled:** Carinsko › Magacinsko poslovanje › Pregled Prijemnica → **frmPrijemnicaCarinskaTabela** → „Novi“ → **frmPrijemnicaCarinsko** → „Štampaj prijemnicu / Skladisni dokumet“ → **frmPrijemnicaCarinskaStampa**
- **Funkcionalnosti:**
  - „Štampa skladišnog dokumenta“ — klik → `button17_Click` — izveštaj: rptPrijemnicaCarinskaSkladisni.rdlc
  - „Štampa prijemnica“ — klik → `button23_Click` — izveštaj: rptPrijemnicaCarinska.rdlc

#### frmPrijemnicaCarinskaTabela [S]
- **Fajl:** `Carinsko/frmPrijemnicaCarinskaTabela.cs` — „Tabela carinskih prijemnica“
- **Stari izgled:** Carinsko › Magacinsko poslovanje › Pregled Prijemnica → **frmPrijemnicaCarinskaTabela**
- **Otvara:** frmPrijemnicaCarinsko („Novi“)
- **Tabele:** `Partnerji` S, `PrijemnicaCarinska` S, `Skladista` S
- **Funkcionalnosti:**
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - „Refresh“ — klik → `button25_Click`
  - „Otvori“ — klik → `button24_Click` — otvara: frmPrijemnicaCarinsko
  - „Novi“ — klik → `button23_Click` — otvara: frmPrijemnicaCarinsko

#### frmPrijemnicaCarinsko [S]
- **Fajl:** `Carinsko/frmPrijemnicaCarinsko.cs` — „CARINSKA PRIJEMNICA“
- **Stari izgled:** Carinsko › Magacinsko poslovanje › Pregled Prijemnica → **frmPrijemnicaCarinskaTabela** → „Novi“ → **frmPrijemnicaCarinsko**
- **Otvara:** frmMagacinskiBrojevi („Magacinski Brojevi“), frmPrijemnicaCarinskaStampa („Štampaj prijemnicu / Skladisni dokumet“)
- **Tabele:** `MagacinskiBrojevi` S, `MerskeEnote` S, `Partnerji` S, `Pozicija` S, `PrijemnicaCarinska` SUD p, `PrijemnicaCarinskaStavke` S, `Promet` S, `Skladista` S, `SkladistaGrupa` S, `Valute` S
- **Procedure:** `DeletePrijemnicaCarinska`, `DeletePrijemnicaCarinskaStavka`, `InsertPrijemnicaCarinskaStavka`, `UpdatePrijemnicaCarinska`, `UpdatePrijemnicaCarinskaStatus`
- **Funkcionalnosti:**
  - „Štampaj prijemnicu / Skladisni dokumet“ — klik → `button3_Click_1` — otvara: frmPrijemnicaCarinskaStampa
  - „Otvori“ — klik → `button4_Click` — čita: Promet; proc: DeletePromet, UpdatePrijemnicaCarinskaStatus; preko: InsertIsporuka.DeletePromet, InsertPrijemnicaCarinska.updetePrijemnicaCarinskaStatus
  - „Proknjizi“ — klik → `button2_Click` — čita: Promet; proc: InsertPromet, UpdatePrijemnicaCarinskaStatus; preko: InsertIsporuka.InsertPromet, InsertPrijemnicaCarinska.updetePrijemnicaCarinskaStatus
  - „Magacinski Brojevi“ — klik → `button17_Click` — otvara: frmMagacinskiBrojevi
  - `button20` — klik → `button20_Click` — proc: DeletePrijemnicaCarinska; preko: InsertPrijemnicaCarinskaStavke.DelPrijemnicaCarinsko
  - `button21` — klik → `button21_Click` — proc: InsertPrijemnicaCarinska, UpdatePrijemnicaCarinska; preko: InsertPrijemnicaCarinska.InsPrijemnicaCarinska, InsertPrijemnicaCarinska.UpdPrijemnicaCarinska
  - `button1` — klik → `button1_Click` — proc: InsertPrijemnicaCarinskaStavka; preko: InsertPrijemnicaCarinskaStavke.InsPrijemnicaCarinskaStavke

#### frmPrijemnicaStavke [S]
- **Fajl:** `Carinsko/frmPrijemnicaStavke.cs` — „Stavke prijemnice ne razduzene“
- **Stari izgled:** Carinsko › Magacinsko poslovanje › Pregled Otpremnica → **frmOtpremnicaCarinskoTabela** → „Novi“ → **frmOtpremnicaCarinsko** → `button3` (Click) → **frmPrijemnicaStavke**
- **Tabele:** `Partnerji` S, `PrijemnicaCarinska` S, `PrijemnicaCarinskaStavke` S, `Skladista` S
- **Procedure:** `PrebaciStavkeIzCarinskePriUOtp`
- **Funkcionalnosti:**
  - „Prenesi selektovane“ — klik → `button23_Click` — proc: PrebaciStavkeIzCarinskePriUOtp; preko: InsertOtpremnicaCarinskaStavke.PrebaciStavkeIzPrijemnice

#### frmPrijemVozaIzPlana [N+S]
- **Fajl:** `Uvoz/frmPrijemVozaIzPlana.cs` — „Formiranje prijema iz plana utovara“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Potvrdi“ → **frmPrijemVozaIzPlana**
- **Stari izgled:** Uvoz › Uvoz obrada › Prenos Plana utovara u Prijem voza Terminal → **frmPrijemVozaIzPlana**
- **Otvara:** frmPrijemKontejneraLegetVozUvoz („OTVORI - GATE IN VOZ“), frmPrijemKontejneraKamionLegetIzvoz („OTVORI - GATE IN KAMION IZVOZ“), frmPrijemKontejneraKamionLegetUvoz („OTVORI - GATE IN KAMION UVOZ“), RN1PrijemVoza (metoda `PrenesiStavkeIRN`), RN4PrijemPlatforme (metoda `PrenesiStavkeIRN`), RN5PrijemPlatforme2 (metoda `PrenesiStavkeIRN`)
- **Tabele:** `DirigacijaKontejneraZa` S, `IzvozKonacna` S, `Partnerji` S, `PrijemKontejneraVoz` S, `PrijemKontejneraVozStavke` S, `RadniNalogInterni` S, `RNPrijemPlatforme` S, `TipKontenjera` S, `UvozKonacna` S, `UvozKonacnaZaglavlje` S, `Voz` S, `VoziloUsluga` S, `VrstePostupakaUvoz` S
- **Procedure:** `spPrenesiPlanUtovaraUPrijemPlatforma`, `spPrenesiPlanUtovaraUPrijemPlatformaIzvoz`, `spPrenesiPlanUtovaraUPrijemVoz`, `UpdateRNIVozFormiran`
- **Combobox → tabela:** `cboPlanUtovara` ← `UvozKonacnaZaglavlje`, `Voz`; `cboBukingPrijema` ← `Voz`; `cboOperater` ← `Partnerji`; `cboOperaterHR` ← `Partnerji`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click`
  - „OTVORI - GATE IN VOZ“ — klik → `toolStripButton1_Click`
  - „OTVORI - GATE IN KAMION UVOZ“ — klik → `toolStripButton2_Click` — otvara: frmPrijemKontejneraKamionLegetUvoz
  - „OTVORI - GATE IN KAMION IZVOZ“ — klik → `toolStripButton3_Click` — otvara: frmPrijemKontejneraKamionLegetIzvoz
  - „OTVORI GATE IN - TERMINAL“ — klik → `toolStripButton4_Click` — otvara: frmPrijemKontejneraKamionLegetIzvoz
  - „?“ — klik → `button2_Click`
  - „OTVORI GATE IN - TERMINAL“ — klik → `toolStripButton4_Click` — otvara: frmPrijemKontejneraKamionLegetIzvoz
  - „OTVORI - GATE IN KAMION IZVOZ“ — klik → `toolStripButton3_Click` — otvara: frmPrijemKontejneraKamionLegetIzvoz
  - „OTVORI - GATE IN KAMION UVOZ“ — klik → `toolStripButton2_Click` — otvara: frmPrijemKontejneraKamionLegetUvoz
  - „OTVORI - GATE IN VOZ“ — klik → `toolStripButton1_Click`
  - `this` — otvaranje forme → `frmPrijemVozaIzPlana_Load` — čita: Partnerji, UvozKonacnaZaglavlje, Voz

#### frmPrijemVozomPregled [N+S]
- **Fajl:** `TrackModal/Dokumeta/frmPrijemVozomPregled.cs` — „Prijem vozom pregled“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Voz“ → **frmPrijemIOtpremaVozova4** → „Gate in voz“ → **frmPrijemVozomPregled**
- **Stari izgled:** Terminal › Prevoz › Prijem kontejnera vozom pregled → **frmPrijemVozomPregled**
- **Tabele:** `PrijemKontejneraVoz` S, `PrijemKontejneraVozStavke` S, `Voz` S
- **Funkcionalnosti:**
  - „Otvori prevoz“ — klik → `toolStripButton1_Click` — otvara: frmPrijemKontejneraVoz
  - „Osveži“ — klik → `toolStripButton2_Click`
  - „Novi voz“ — klik → `toolStripButton3_Click` — otvara: frmPrijemKontejneraVoz
  - „Najave“ — klik → `toolStripButton4_Click`
  - „Prijemi“ — klik → `toolStripButton5_Click`
  - „?“ — klik → `button2_Click`
  - „?“ — klik → `button3_Click`
  - „Prijem“ — klik → `toolStripButton5_Click`
  - „Najava“ — klik → `toolStripButton4_Click`
  - „Novi voz“ — klik → `toolStripButton3_Click` — otvara: frmPrijemKontejneraVoz
  - „Osveži“ — klik → `toolStripButton2_Click`
  - „Otvori prevoz“ — klik → `toolStripButton1_Click` — otvara: frmPrijemKontejneraVoz

#### frmProdajnaGrupa [S]
- **Fajl:** `Sifarnici/frmProdajnaGrupa.cs` — „Prodajne grupe“
- **Stari izgled:** Komercijala › Ugovori › Prodajne grupe → **frmProdajnaGrupa**
- **Tabele:** `ProdSkup` SID p
- **Procedure:** `DeleteProdSkup`, `InsertProdSkup`, `UpdatePodSkup`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertProdSkup, UpdatePodSkup; preko: InsertProdSkup.InsProdSkup, InsertProdSkup.UpdProdSkup
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteProdSkup; preko: InsertProdSkup.DeleteProdSkup

#### frmProdajniNalogIzvoz [N]
- **Fajl:** `Izvoz/frmProdajniNalogIzvoz.cs` — „Porudžbenica“
- **Novi izgled:** Logistika izvoza → **LogistikaIzvoza1** → „Porudžbenica“ → **LogistikaIzvoza0** → „Kreiraj novi“ → **frmProdajniNalogIzvoz**
- **Otvara:** frmProdajniNalogIzvozTabela („Izmeni količinu stavke porudžbenice“)
- **Tabele:** `KontejnerTekuce` IU p, `MerskeEnote` S, `Partnerji` S, `ProdajniNalogIzvoz` SIU p, `ProdajniNalogIzvozStavke` SIU p, `TipKontenjera` S, `uvKvalitetKontejnera` S
- **Procedure:** `InsertKontejnerTekuce`, `InsertProdajniNalogIzvoz`, `InsertProdajniNalogIzvozStavke`, `UpdateKontejnerTekuce`, `UpdateProdajniNalogIzvoz`, `UpdateProdajniNalogIzvozStavke`
- **Combobox → tabela:** `cboBrodar` ← `Partnerji`; `cboIzvoznik` ← `Partnerji`; `cboNalogodavac` ← `Partnerji`
- **Funkcionalnosti:**
  - `button21` — klik → `button21_Click` — proc: InsertProdajniNalogIzvoz, UpdateProdajniNalogIzvoz; preko: InsertProdajniNalogIzvoz.InsProdajniNalogIzvoz, InsertProdajniNalogIzvoz.UpdProdajniNalogIzvoz
  - „Izmeni količinu stavke porudžbenice“ — klik → `btnIzmeniKolicinu_Click` — otvara: frmProdajniNalogIzvozTabela

#### frmProdajniNalogIzvozTabela [N]
- **Fajl:** `Izvoz/frmProdajniNalogIzvozTabela.cs` — „Prodajni nalog izvoz tabela - otvorene stavke“
- **Novi izgled:** Logistika izvoza → **LogistikaIzvoza1** → „Formiranje naloga za otpremu“ → **LogistikaIzvoza2** → „Kreiraj novi“ → **frmProdajniNalogIzvozTabela**
- **Otvara:** NalogIzvozaZaOtpremu (`gridGroupingControl1` (TableControlCellClick)), frmGrupniUnosPoljaIzvoz (`gridGroupingControl1` (TableControlCellClick))
- **Tabele:** `Partnerji` S, `ProdajniNalogIzvoz` S, `ProdajniNalogIzvozStavke` S, `Scenario` S, `TipKontenjera` S, `uvKvalitetKontejnera` S
- **Procedure:** `UpdateProdajniNalogStavkeDrumski`, `UpdateProdajniNalogStavkeKolicina`, `UpdateProdajniNalogStavkeScenario`, `UpdateProdajniNalogStavkeStorno`
- **Funkcionalnosti:**
  - „gridGroupingControl1“ — TableControlCurrentCellEditingComplete → `gridGroupingControl1_TableControlCurrentCellEditingComplete` — proc: UpdateProdajniNalogStavkeKolicina; preko: InsertProdajniNalogIzvoz.UpdKolicinaStavke
  - „gridGroupingControl1“ — TableControlCurrentCellChanging → `gridGroupingControl1_TableControlCurrentCellChanging`
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick` — otvara: NalogIzvozaZaOtpremu, frmGrupniUnosPoljaIzvoz; proc: UpdateProdajniNalogStavkeDrumski, UpdateProdajniNalogStavkeScenario, UpdateProdajniNalogStavkeStorno; preko: InsertProdajniNalogIzvoz.UpdDrumski, InsertProdajniNalogIzvoz.UpdScenario, InsertProdajniNalogIzvoz.UpdStornirajStavku

#### frmPrometKontejnera [S]
- **Fajl:** `Promet/frmPrometKontejnera.cs` — „Promet kontejnera“
- **Stari izgled:** Terminal › Skladišno poslovanje › Pregled prometa → **frmPrometKontejnera**
- **Tabele:** `NHM` S, `Pozicija` S, `Promet` S, `Skladista` S
- **Funkcionalnosti:**
  - „Pretraži“ — klik → `btnPregled_Click` — čita: NHM, Pozicija, Promet, Skladista, pozicija, skladista

#### frmPronadjiVagon [S]
- **Fajl:** `Dokumenta/frmPronadjiVagon.cs` — „Pronađi vagon“
- **Stari izgled:** Komercijala › Najave › Poslovi → **frmNajava** → „Pronađi Vagon“ → **frmPronadjiVagon**
- **Tabele:** `Najava` S, `Partnerji` S, `Stanice` S, `TeretnicaIskljuceniVagoni` S, `TeretnicaStavke` S
- **Funkcionalnosti:**
  - „Pronađi bilo gde“ — klik → `button4_Click` — čita: TeretnicaStavke
  - „Pronađi samo u 9“ — klik → `button1_Click` — čita: Najava, TeretnicaStavke
  - `this` — otvaranje forme → `frmPronadjiVagon_Load` — čita: Partnerji

#### frmPropratnice [S]
- **Fajl:** `Dokumenta/frmPropratnice.cs` — „Propratnice“
- **Stari izgled:** Komercijala › Najave › Propratnice → **frmPropratnice**
- **Tabele:** `Delavci` S, `Korisnici` S, `Partnerji` S, `Propratnica` SIUD p, `PropratnicaRazduzenje` S, `PropratnicaZaduzenje` S, `PropratniceRazduzivanjeSlike` S, `PropratniceZaduzivanjeSlike` S
- **Procedure:** `DeletePropratnica`, `InsertPropratnica`, `UpdatePropratnica`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertPropratnica, UpdatePropratnica; preko: InsertPropratnica.InsPropratnica, InsertPropratnica.UpdPropratnica
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeletePropratnica; preko: InsertPropratnica.DeletePropratnica
  - „Sve propratnice“ — klik → `toolStripButton1_Click`
  - „Pošalji mail“ — klik → `toolStripButton2_Click` — čita: Delavci
  - `dataGridView1` — izbor → `dataGridView1_SelectionChanged` — čita: PropratniceZaduzivanjeSlike
  - `dataGridView2` — izbor → `dataGridView2_SelectionChanged` — čita: PropratniceRazduzivanjeSlike
  - „Prikaži Zaduživanje“ — klik → `btn_prikaziZ_Click`
  - „Prikaži razduživanje“ — klik → `btn_prikaziR_Click`

#### frmProtokolTransportnogNaloga [N+S]
- **Fajl:** `Drumski/frmProtokolTransportnogNaloga.cs` — „Protokol transportnog naloga“
- **Novi izgled:** Drumski transport → **Drumski1** → „Cerada“ → **Cerade** → „FORMIRANJE\r\nTRANSPORTNOG NALOGA“ → **PakovanjeKamionaCerade** → „PROTOKOL“ → **frmListaProtokola** → `subOstali` (Click) → **frmProtokolTransportnogNaloga**
- **Stari izgled:** Drumski › Drumski › Pakovanje kamiona → **PakovanjeKamiona1** → „PROTOKOL“ → **frmListaProtokola** → `subOstali` (Click) → **frmProtokolTransportnogNaloga**
- **Tabele:** `Automobili` S, `Carinarnice` S, `Izvoz` S, `IzvozKonacna` S, `Korisnici` S, `MestaUtovara` S, `Partnerji` S, `partnerjiKontOsebaMU` S, `PodtipProtokolaRazno` S, `ProtokolRadniNalogDrumski` SIUD p, `RadniNalogDrumski` S
- **Procedure:** `DeleteProtokolRadniNalogDrumski`, `InsertProtokolRadniNalogDrumski`, `UpdateProtokolRadniNalogDrumski`
- **Combobox → tabela:** `cboPrevoznik` ← `Partnerji`; `cboPrevoznikCerada` ← `Partnerji`; `cboKlijent` ← `Partnerji`; `cboMestoPreuzimanja` ← `MestaUtovara`; `cboPolaznaCarinarnica` ← `Carinarnice`; `cboOCarinarnica` ← `Carinarnice`; `cboSituacija` ← `PodtipProtokolaRazno`; `cboSituacijaCerada` ← `PodtipProtokolaRazno`; `cbOspedicija` ← `Partnerji`; `cboPolaznaSpedicija` ← `Partnerji`; `cboCilj` ← `partnerjiKontOsebaMU`; `cboKontakt` ← `partnerjiKontOsebaMU`
- **Funkcionalnosti:**
  - `button1` — klik → `button1_Click` — proc: DeleteProtokolRadniNalogDrumski; preko: InsertProtokolTransportnogNaloga.DelProtokolRadniNalogDrumski
  - `btnSave` — klik → `btnSave_Click` — proc: InsertProtokolRadniNalogDrumski, UpdateProtokolRadniNalogDrumski; preko: InsertProtokolTransportnogNaloga.InsertProtokol, InsertProtokolTransportnogNaloga.UpdateProtokol

#### frmPruge [S]
- **Fajl:** `Sifarnici/frmPruge.cs` — „Pruge“
- **Stari izgled:** Osnovni podaci › Prevozni šifarnici › Pruge → **frmPruge**
- **Otvara:** frmPrugeSpisak („Spisak pruga“)
- **Tabele:** `Pruga` S, `PrugaStavke` SIU p, `Stanice` S
- **Procedure:** `InsertPrugaStavke`, `UpdatePrugaStavke`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertPruga, UpdatePruga; preko: InsertPruga.InsPruga, InsertPruga.UpdPruga
  - „Spisak pruga“ — klik → `toolStripTextBox1_Click` — otvara: frmPrugeSpisak
  - „?“ — klik → `button2_Click` — čita: Pruga, PrugaStavke, stanice
  - „Promeni stavku“ — klik → `button3_Click` — proc: UpdatePrugaStavke; preko: InsertPrugaStavke.UpdPrugaStavke
  - „Unesi stavku“ — klik → `button1_Click` — proc: InsertPrugaStavke; preko: InsertPrugaStavke.InsPrugaStavke
  - „Koloseci:“ — klik → `label12_Click`
  - „MaxDV:“ — klik → `label13_Click`
  - „Smer B->A:“ — klik → `label14_Click`
  - „Spisak pruga“ — klik → `toolStripTextBox1_Click` — otvara: frmPrugeSpisak
  - `button21` — klik → `tsSave_Click` — proc: InsertPruga, UpdatePruga; preko: InsertPruga.InsPruga, InsertPruga.UpdPruga
  - `this` — otvaranje forme → `frmPruge_Load` — čita: Stanice

#### frmPrugeSpisak [S]
- **Fajl:** `Sifarnici/frmPrugeSpisak.cs` — „Spisak postojećih pruga“
- **Stari izgled:** Osnovni podaci › Prevozni šifarnici › Pruge → **frmPruge** → „Spisak pruga“ → **frmPrugeSpisak**
- **Tabele:** `Pruga` S

#### frmPutniNalog [N+S]
- **Fajl:** `TrackModal/Dokumeta/frmPutniNalog.cs` — „Putni nalog“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Voz“ → **frmPrijemIOtpremaVozova4** → „Gate out voz“ → **frmPregledOtpreme** → „Otvori voz“ → **frmOtpremaKontejnera** → „Nalog Za Prevoz“ → **frmNalogZaPrevoz** → „Otvori putni nalog“ → **frmPutniNalog**
- **Stari izgled:** Terminal › Transport › Pregled puti nalozi → **frmPregledPutnihNaloga** → „Otvori putni nalog“ → **frmPutniNalog**
- **Otvara:** frmRadniNalogTransport („Radni nalog“)
- **Tabele:** `PutniNalog` S, `PutniNalogGorivo` SUD p, `PutniNalogGume` S, `PutniNalogKvarovi` SIUD p, `PutniNalogRuta` S, `PutniNalogServis` S, `PutniNalogTroskovi` S, `PutniNalogZaposleni` S, `Vozila` S, `Zaposleni` S
- **Procedure:** `DeletePutniNalogGorivo`, `DeletePutniNalogKvarovi`, `InsertPutniNalogKvarovi`, `UpdatePutniNalogGorivo`, `UpdatePutniNalogKvarovi`
- **Combobox → tabela:** `cboPrikljucnoVozilo` ← `Vozila`; `cboVozilo` ← `Vozila`; `cboTransportniDispičer` ← `Zaposleni`; `cboVozac` ← `Zaposleni`; `cboTehnickaIspravnost` ← `Zaposleni`; `CboZaposleni` ← `Zaposleni`; `cboVozacRute` ← `PutniNalog`, `PutniNalogZaposleni`, `Zaposleni`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertPutniNalog, UpdatePutniNalog; preko: InsertPutniNalog.InsPutniNalog, InsertPutniNalog.UpdPutniNalog
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „Štampa Putni nalog“ — klik → `toolStripLabel1_Click` — izveštaj: rptPutniNalog.rdlc
  - „Radni nalog“ — klik → `toolStripLabel2_Click` — otvara: frmRadniNalogTransport
  - „Servis“ — klik → `tabPage1_Click`
  - „Izbriši“ — klik → `button2_Click` — proc: DeletePutniNalogServis; preko: InsertPutniNalogServis.DeletePutniNalogServis
  - „Promeni“ — klik → `button1_Click` — proc: UpdatePutniNalogServis; preko: InsertPutniNalogServis.UpdPutniNalogServis
  - „Unesi“ — klik → `btnPretrazi_Click` — proc: InsertPutniNalogServis; preko: InsertPutniNalogServis.InsPutniNalogServis
  - „?“ — klik → `button22_Click` — čita: PutniNalog, PutniNalogZaposleni, Zaposleni
  - „Izbriši“ — klik → `button3_Click` — proc: DeletePutniNalogRuta; preko: InsertPutniNalogRuta.DeletePutniNalogRuta
  - „Promeni“ — klik → `button4_Click` — proc: UpdatePutniNalogRuta; preko: InsertPutniNalogRuta.UpdPutniNalogRuta
  - „Unesi“ — klik → `button5_Click` — proc: InsertPutniNalogRuta; preko: InsertPutniNalogRuta.InsPutniNalogRuta
  - „Izbriši“ — klik → `button6_Click` — proc: DeletePutniNalogGume; preko: InsertPutniNalogGume.DeletePutniNalogGume
  - „Promeni“ — klik → `button7_Click` — proc: UpdatePutniNalogGume; preko: InsertPutniNalogGume.UpdPutniNalogGume
  - „Unesi“ — klik → `button8_Click` — proc: InsertPutniNalogGume; preko: InsertPutniNalogGume.InsPutniNalogGume
  - „Izbriši“ — klik → `button9_Click` — proc: DeletePutniNalogGorivo; preko: InsertPutniNalogGorivo.DeletePutniNalogGorivo
  - „Promeni“ — klik → `button10_Click` — proc: UpdatePutniNalogGorivo; preko: InsertPutniNalogGorivo.UpdPutniNalogGorivo
  - „Unesi“ — klik → `button11_Click` — proc: InsertPutniNalogGorivo; preko: InsertPutniNalogGorivo.InsPutniNalogGorivo
  - „Unesi“ — klik → `button14_Click` — proc: InsertPutniNalogTroskovi; preko: InsertPutniNalogTroskovi.InsPutniNalogTroskovi
  - „Lista kvarova“ — klik → `tabPage8_Click`
  - „Izbriši“ — klik → `button19_Click` — proc: DeletePutniNalogKvarovi; preko: InsertPutniNalogKvarovi.DeletePutniNalogKvarovi
  - „Promeni“ — klik → `button20_Click` — proc: UpdatePutniNalogKvarovi; preko: InsertPutniNalogKvarovi.UpdPutniNalogKvarovi
  - „Unesi“ — klik → `button21_Click` — proc: InsertPutniNalogKvarovi; preko: InsertPutniNalogKvarovi.InsPutniNalogKvarovi
  - „Pošalji mail“ — klik → `button18_Click` — proc: UpdatePutniNalogKvarovi; preko: InsertPutniNalogKvarovi.UpdPutniNalogKvarovi
  - „Izbriši“ — klik → `button15_Click` — proc: DeletePutniNalogZaposleni; preko: InsertPutniNalogZaposleni.DeletePutniNalogZaposleni
  - „Promeni“ — klik → `button16_Click` — proc: UpdatePutniNalogZaposleni; preko: InsertPutniNalogZaposleni.UpdPutniNalogZaposleni
  - „Unesi“ — klik → `button17_Click` — proc: InsertPutniNalogZaposleni; preko: InsertPutniNalogZaposleni.InsPutniNalogZaposleni
  - „Dokument“ — klik → `tabPage6_Click`
  - `this` — otvaranje forme → `frmPutniNalog_Load` — čita: Vozila, Zaposleni

#### frmPutniNalog [N+S]
- **Fajl:** `Dokumenta/frmPutniNalog.cs` — „Putni nalog“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Formiraj otpremu kamionom“ → **frmOtpremaKontejneraKamionomIzKontejnera** → „Otvori“ → **frmOtpremaKontejnera** → „Nalog Za Prevoz“ → **frmNalogZaPrevoz** → „Otvori putni nalog“ → **frmPutniNalog**
- **Stari izgled:** Terminal › Prevoz › Pregled putni nalozi → **frmPutniNalog**
- **Otvara:** frmRadniNalogTransport („Radni nalog“)
- **Tabele:** `PutniNalog` S, `PutniNalogGorivo` SUD p, `PutniNalogGume` S, `PutniNalogKvarovi` SIUD p, `PutniNalogRuta` S, `PutniNalogServis` S, `PutniNalogTroskovi` S, `PutniNalogZaposleni` S, `Vozila` S, `Zaposleni` S
- **Procedure:** `DeletePutniNalogGorivo`, `DeletePutniNalogKvarovi`, `InsertPutniNalogKvarovi`, `UpdatePutniNalogGorivo`, `UpdatePutniNalogKvarovi`
- **Combobox → tabela:** `cboPrikljucnoVozilo` ← `Vozila`; `cboVozilo` ← `Vozila`; `cboTransportniDispičer` ← `Zaposleni`; `cboVozac` ← `Zaposleni`; `cboTehnickaIspravnost` ← `Zaposleni`; `CboZaposleni` ← `Zaposleni`; `cboVozacRute` ← `PutniNalog`, `PutniNalogZaposleni`, `Zaposleni`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertPutniNalog, UpdatePutniNalog; preko: InsertPutniNalog.InsPutniNalog, InsertPutniNalog.UpdPutniNalog
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „Štampa Putni nalog“ — klik → `toolStripLabel1_Click` — izveštaj: rptPutniNalog.rdlc
  - „Radni nalog“ — klik → `toolStripLabel2_Click` — otvara: frmRadniNalogTransport
  - „Servis“ — klik → `tabPage1_Click`
  - „Izbriši“ — klik → `button2_Click` — proc: DeletePutniNalogServis; preko: InsertPutniNalogServis.DeletePutniNalogServis
  - „Promeni“ — klik → `button1_Click` — proc: UpdatePutniNalogServis; preko: InsertPutniNalogServis.UpdPutniNalogServis
  - „Unesi“ — klik → `btnPretrazi_Click` — proc: InsertPutniNalogServis; preko: InsertPutniNalogServis.InsPutniNalogServis
  - „?“ — klik → `button22_Click` — čita: PutniNalog, PutniNalogZaposleni, Zaposleni
  - „Izbriši“ — klik → `button3_Click` — proc: DeletePutniNalogRuta; preko: InsertPutniNalogRuta.DeletePutniNalogRuta
  - „Promeni“ — klik → `button4_Click` — proc: UpdatePutniNalogRuta; preko: InsertPutniNalogRuta.UpdPutniNalogRuta
  - „Unesi“ — klik → `button5_Click` — proc: InsertPutniNalogRuta; preko: InsertPutniNalogRuta.InsPutniNalogRuta
  - „Izbriši“ — klik → `button6_Click` — proc: DeletePutniNalogGume; preko: InsertPutniNalogGume.DeletePutniNalogGume
  - „Promeni“ — klik → `button7_Click` — proc: UpdatePutniNalogGume; preko: InsertPutniNalogGume.UpdPutniNalogGume
  - „Unesi“ — klik → `button8_Click` — proc: InsertPutniNalogGume; preko: InsertPutniNalogGume.InsPutniNalogGume
  - „Izbriši“ — klik → `button9_Click` — proc: DeletePutniNalogGorivo; preko: InsertPutniNalogGorivo.DeletePutniNalogGorivo
  - „Promeni“ — klik → `button10_Click` — proc: UpdatePutniNalogGorivo; preko: InsertPutniNalogGorivo.UpdPutniNalogGorivo
  - „Unesi“ — klik → `button11_Click` — proc: InsertPutniNalogGorivo; preko: InsertPutniNalogGorivo.InsPutniNalogGorivo
  - „Unesi“ — klik → `button14_Click` — proc: InsertPutniNalogTroskovi; preko: InsertPutniNalogTroskovi.InsPutniNalogTroskovi
  - „Lista kvarova“ — klik → `tabPage8_Click`
  - „Izbriši“ — klik → `button19_Click` — proc: DeletePutniNalogKvarovi; preko: InsertPutniNalogKvarovi.DeletePutniNalogKvarovi
  - „Promeni“ — klik → `button20_Click` — proc: UpdatePutniNalogKvarovi; preko: InsertPutniNalogKvarovi.UpdPutniNalogKvarovi
  - „Unesi“ — klik → `button21_Click` — proc: InsertPutniNalogKvarovi; preko: InsertPutniNalogKvarovi.InsPutniNalogKvarovi
  - „Pošalji mail“ — klik → `button18_Click` — proc: UpdatePutniNalogKvarovi; preko: InsertPutniNalogKvarovi.UpdPutniNalogKvarovi
  - „Izbriši“ — klik → `button15_Click` — proc: DeletePutniNalogZaposleni; preko: InsertPutniNalogZaposleni.DeletePutniNalogZaposleni
  - „Promeni“ — klik → `button16_Click` — proc: UpdatePutniNalogZaposleni; preko: InsertPutniNalogZaposleni.UpdPutniNalogZaposleni
  - „Unesi“ — klik → `button17_Click` — proc: InsertPutniNalogZaposleni; preko: InsertPutniNalogZaposleni.InsPutniNalogZaposleni
  - „Dokument“ — klik → `tabPage6_Click`
  - `this` — otvaranje forme → `frmPutniNalog_Load` — čita: Vozila, Zaposleni

#### frmPutniNaloziPregled [S]
- **Fajl:** `Dokumenta/frmPutniNaloziPregled.cs` — „Prenesi u Putni Nalog - Ne kreirani “
- **Stari izgled:** Kadrovi › Putni nalozi › Pregled Putni nalozi → **frmPutniNaloziPregled**
- **Tabele:** `Aktivnosti` S, `AktivnostiStavke` S, `Delavci` S, `Kraji` S, `VrstaAktivnosti` S
- **Procedure:** `InsertPotNal`, `InsertPotNalRelacije`, `InsertPotNalStavke`, `UpdateAktivnostiPutniNalog`
- **Funkcionalnosti:**
  - „Pretraži svi“ — klik → `btnPretrazi_Click` — čita: Aktivnosti, AktivnostiStavke, Delavci, Kraji, VrstaAktivnosti
  - „Prenesi u putni nalog selektovane“ — klik → `button1_Click` — proc: InsertPotNal, InsertPotNalRelacije, InsertPotNalStavke, UpdateAktivnostiPutniNalog; preko: InsertPotNal.InsPotNal, InsertPotNal.InsPotNalRelacije, InsertPotNal.InsPotNalStavke, InsertPotNal.UpdPotNal
  - „Pretraži Smederevci“ — klik → `button2_Click` — čita: Aktivnosti, AktivnostiStavke, Delavci, Kraji, VrstaAktivnosti
  - „Pretraži Kragujevac“ — klik → `button3_Click` — čita: Aktivnosti, AktivnostiStavke, Delavci, Kraji, VrstaAktivnosti
  - „Pretraži Ino“ — klik → `button4_Click` — čita: Aktivnosti, AktivnostiStavke, Delavci, Kraji, VrstaAktivnosti
  - „Remont“ — klik → `button5_Click` — čita: Aktivnosti, AktivnostiStavke, Delavci, Kraji, VrstaAktivnosti
  - „Obeleži PN bez prenosa u modul“ — klik → `button6_Click` — proc: UpdateAktivnostiPutniNalog; preko: InsertPotNal.UpdPotNal
  - „Pretraži Godišnji odmori“ — klik → `button7_Click` — čita: Aktivnosti, AktivnostiStavke, Delavci, Kraji, VrstaAktivnosti

#### frmRadniNalog [S]
- **Fajl:** `Dokumenta/frmRadniNalog.cs` — „Radni nalog“
- **Stari izgled:** Komercijala › Najave › Poslovi → **frmNajava** → „Prevozni nalog - Trase i Lokomotive“ → **frmRadniNalog**
- **Otvara:** frmRadniNalogTraseLok („Unos trasa“), frmLokomotiveNaTrasi („Lokomotive“), frmRadniNalogZaposleni („Osoblje“), frmDokumentaRadniNalog („Dokumenta“), frmTehnologija („Tehnologija“), frmRadniNalogIzNajave (metoda `KopiranjeRNIzPOsla`), frmTehnologijaPregled (metoda `OtvoriTehnologiju`)
- **Tabele:** `Delavci` S, `MAticniPodatki` S, `Najava` S, `RadniNalog` SIU p, `RadniNalogLokNaTrasi` S, `RadniNalogTeretnice` S, `RadniNalogTrase` S, `RadniNalogVezaNajave` SID p, `Stanice` S, `StatusDelNaloga` S, `Tehnologija` S, `Teretnica` S, `TeretnicaStavke` S, `Trase` S
- **Procedure:** `DeleteRadniNalogVezaNajave`, `InsertRadniNalog`, `InsertRadniNalogVezaNajave`, `UpdateRadniNalog`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertRadniNalog, UpdateRadniNalog; preko: InsertRadniNalog.InsRN, InsertRadniNalog.UpdRN
  - „Unos trasa“ — klik → `toolStripButton1_Click` — otvara: frmRadniNalogTraseLok
  - „Lokomotive“ — klik → `toolStripButton2_Click` — otvara: frmLokomotiveNaTrasi
  - „Osoblje“ — klik → `toolStripButton3_Click` — otvara: frmRadniNalogZaposleni
  - „Osveži“ — klik → `toolStripButton4_Click`
  - „Štampa RN“ — klik → `toolStripButton5_Click`
  - „Dokumenta“ — klik → `toolStripButton6_Click_1` — otvara: frmDokumentaRadniNalog
  - „Tehnologija“ — klik → `toolStripButton7_Click_1` — čita: Najava
  - „Delete“ — klik → `button2_Click` — proc: DeleteRadniNalogVezaNajave; preko: InsertRadniNalog.DeleteRNVezaNajava
  - „Sačuvaj“ — klik → `button1_Click` — proc: InsertRadniNalogVezaNajave; preko: InsertRadniNalog.InsRadniNalogVezaNajava
  - `this` — otvaranje forme → `frmRadniNalog_Load` — čita: Delavci, MaticniPodatki, Najava, RadniNalog, StatusDelNaloga, Tehnologija

#### frmRadniNalogInterniPregled [N+S]
- **Fajl:** `Uvoz/frmRadniNalogInterniPregled.cs` — „Pregled internih radnih naloga“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled**
- **Stari izgled:** Izvoz › Zadaci › Pregled radnih naloga → **frmRadniNalogInterniPregled**
- **Otvara:** frmPrijemKamionaIzPlana („Formiraj prijem kamionom“), frmOtpremaKontejneraKamionomIzKontejnera („Formiraj otpremu kamionom“), frmPrijemVozaIzPlana („Potvrdi“), frmPrijemKontejneraKamionLegetUvoz („Potvrdi“), frmOtpremaVozaIzPlana („Potvrdi“), RN12MedjuskladisniKontejnera („Potvrdi“), ZapisnikONepravilnosti („Zapisnik o nepravilnosti“), frmFormiranjePlanaIzvoz („Dodaj izvoznom vozu iz uvoza“), RN1PrijemVoza („Otvori RN“), RN2OtpremaVoza („Otvori RN“), RN4PrijemPlatforme („Otvori RN“), RN5PrijemPlatforme2 („Otvori RN“), RN6OtpremaPlatforme („Otvori RN“), RN7OtpremaPlatforme2 („Otvori RN“), frmScenarioSCI („Pregledaj podatke“)
- **Tabele:** `Brodovi` S, `Carinarnice` S, `DirigacijaKontejneraZa` S, `IzvozKonacna` S, `IzvozKonacnaVrstaManipulacije` S, `IzvozKonacnaZaglavlje` S, `KontejnerskiTerminali` S, `KontejnerStatus` S, `MestaUtovara` S, `organizacioneJedinice` S, `OtpremaKontejnera` S, `Partnerji` S, `Predefinisaneporuke` S, `PrijemKontejneraVoz` S, `ProdajniNalogIzvozStavke` S, `RadniNalogInterni` S, `RadniNalogInterniPotvrda` SI p, `Scenario` S, `TipKontenjera` S, `uvNacinPakovanja` S, `UvozKonacna` S, `UvozKonacnaNapomenePozicioniranja` S, `UvozKonacnaNHM` S, `UvozKonacnaVrstaManipulacije` S, `UvozKonacnaZaglavlje` S, `UvozNapomenePozicioniranja` S, `Voz` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaRobeADR` S, `VrstePostupakaUvoz` S
- **Procedure:** `DelRadniNalogInterniSaDokumentima`, `InsertRadniNalogInterniPotvrda`, `InsertRadniNalogInterniStorno`, `InsertRN4PrPlatformeKamIzvoz`, `InsertRN6OtPlatFormeKam`, `spPrenesiKontejnerIzPlanaNaPrijemnicuIzvozPoIdNadredjenog`, `spPrenesiKontejnerUOtpremuKamionomIzvoz`, `spPrenesiPlanUtovaraUPrijemPlatformaIzvoz`, `UpdateRadniNalogInterniZavrsen`
- **Combobox → tabela:** `cboIzdatOd` ← `organizacioneJedinice`; `cboIzdatZa` ← `organizacioneJedinice`
- **Funkcionalnosti:**
  - „Formiraj otpremu kamionom“ — klik → `button2_Click`
  - „Formiraj prijem kamionom“ — klik → `button1_Click` — otvara: frmPrijemKamionaIzPlana
  - „gridGroupingControl2“ — klik na ćeliju → `gridGroupingControl2_TableControlCellClick`
  - „gridGroupingControl2“ — SelectedRecordsChanged → `gridGroupingControl2_SelectedRecordsChanged`
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - „Pregledaj podatke“ — klik → `button7_Click`
  - „Otvori RN“ — klik → `button6_Click` — otvara: RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme, RN7OtpremaPlatforme2, RN12MedjuskladisniKontejnera
  - „Dodaj izvoznom vozu iz uvoza“ — klik → `button5_Click` — otvara: frmFormiranjePlanaIzvoz
  - „Zapisnik o nepravilnosti“ — klik → `button4_Click_1` — otvara: ZapisnikONepravilnosti
  - „Brisi naloge“ — klik → `toolStripButton9_Click`
  - „Potvrdi“ — klik → `toolStripButton8_Click` — otvara: frmPrijemVozaIzPlana, frmOtpremaVozaIzPlana; proc: InsertRN4PrPlatformeKamIzvoz, InsertRN6OtPlatFormeKam, spPrenesiKontejnerIzPlanaNaPrijemnicuIzvozPoIdNadredjenog, spPrenesiKontejnerUOtpremuKamionomIzvoz, spPrenesiPlanUtovaraUPrijemPlatformaIzvoz; preko: InsertIzvozKonacna.PrenesiKontejnerUOtpremuKamionomIzvoz, InsertRN.InsRN6OtpremaPlatformeKam, InsertRN.InsRNPrijemPlatformeKamIzvoz, InsertUvozKonacna.PrenesiKontejnerIzPlanaNaPrijemnicuIzvozIDNadredjenog, InsertUvozKonacna.PrenesiPlanUtovaraUPrijemVozIzvoz
  - „Završi“ — klik → `toolStripButton4_Click`
  - „Storniraj“ — klik → `toolStripButton3_Click` — proc: InsertRadniNalogInterniStorno; preko: InsertRadniNalogInterni.PromeniStatusStorno
  - „Izdat za OJ“ — klik → `toolStripButton2_Click` — čita: OrganizacioneJedinice, Partnerji, RadniNalogInterni, TipKontenjera, UvozKonacna
  - „Izdao OJ“ — klik → `toolStripButton1_Click` — čita 16 tabela; izvoz u Excel
  - „IZDAO OJ“ — klik → `toolStripButton1_Click_1`
  - `this` — otvaranje forme → `frmRadniNalogInterniPregled_Load` — čita: OrganizacioneJedinice

#### frmRAdniNalogInterniPUvoz [S]
- **Fajl:** `Uvoz/frmRAdniNalogInterniPUvoz.cs` — „Pregled po vozu“
- **Stari izgled:** Uvoz › Uvoz obrada › Planovi utovara → **frmPregledPlanovaUtovara** → „Pregled uradjeno“ → **frmRAdniNalogInterniPUvoz**
- **Tabele:** `KontejnerStatus` S, `organizacioneJedinice` S, `Partnerji` S, `RadniNalogInterni` S, `Scenario` S, `TipKontenjera` S, `UvozKonacna` S, `UvozKonacnaVrstaManipulacije` S, `UvozKonacnaZaglavlje` S, `Voz` S, `VrstaManipulacije` S
- **Funkcionalnosti:**
  - „Refresh“ — klik → `button23_Click`

#### frmRadniNalogIzNajave [S]
- **Fajl:** `Dokumenta/frmRadniNalogIzNajave.cs` — „Prevozni nalog iz prevoza“
- **Stari izgled:** Komercijala › Najave › Poslovi → **frmNajava** → „Prevozni nalog - Trase i Lokomotive“ → **frmRadniNalogIzNajave**
- **Otvara:** frmRadniNalog („Otvori Prevozni nalog“)
- **Tabele:** `Najava` S, `RadniNalog` S, `RadniNalogVezaNajave` S
- **Procedure:** `InsertRadniNalogIzNajaveKopiranjem`
- **Funkcionalnosti:**
  - „Formiraj novi Prevozni nalog“ — klik → `button1_Click` — proc: InsertRadniNalogIzNajaveKopiranjem; preko: InsertRadniNalog.InsRadniNalogIzNajaveKopiranjem
  - „Otvori Prevozni nalog“ — klik → `button2_Click` — otvara: frmRadniNalog

#### frmRadniNalogPregled [S]
- **Fajl:** `Dokumenta/frmRadniNalogPregled.cs` — „Radni nalozi“
- **Stari izgled:** Operatika › Prevozni nalozi › Prevoz po trasama → **frmRadniNalogPregled**
- **Otvara:** frmRadniNalog („Otvori radni nalog“)
- **Tabele:** `Delavci` S, `Najava` S, `RadniNalog` S, `RadniNalogLokNaTrasi` S, `RadniNalogTrase` S, `RadniNalogTraseLokZap` S, `RadniNalogVezaNajave` S, `Stanice` S, `Trase` S
- **Procedure:** `UpdateRadniNalogTrasePoslato`
- **Funkcionalnosti:**
  - „Pošalji mail infrastrukturi“ — ItemClicked → `toolStrip1_ItemClicked`
  - „Otvori radni nalog“ — klik → `toolStripButton1_Click` — otvara: frmRadniNalog
  - „Osveži“ — klik → `toolStripButton2_Click`
  - „Novi radni nalog“ — klik → `toolStripButton3_Click` — otvara: frmRadniNalog
  - „PR“ — čekiranje → `chkPR_CheckedChanged`
  - „RA“ — čekiranje → `chkLA_CheckedChanged`
  - „OD“ — čekiranje → `chkOD_CheckedChanged`
  - „PL“ — čekiranje → `chkPL_CheckedChanged`
  - „ST“ — čekiranje → `chkST_CheckedChanged`
  - „ZA“ — čekiranje → `chkZA_CheckedChanged`

#### frmRAdniNalogPregledPoLokomotivama [S]
- **Fajl:** `Dokumenta/frmRAdniNalogPregledPoLokomotivama.cs` — „Pregled Radnih naloga po lokomotivama“
- **Stari izgled:** Operatika › Prevozni nalozi › Prevoz po lokomotivama → **frmRAdniNalogPregledPoLokomotivama**
- **Otvara:** frmRadniNalog („Otvori radni nalog“)
- **Tabele:** `Delavci` S, `Mesta` S, `RadniNalog` S, `RadniNalogLokNaTrasi` S, `RadniNalogTrase` S, `RadniNalogTraseLokZap` S, `Stanice` S, `Trase` S
- **Funkcionalnosti:**
  - „Refresh“ — klik → `button1_Click`
  - „Otvori radni nalog“ — klik → `toolStripButton1_Click` — otvara: frmRadniNalog
  - „Osveži“ — klik → `toolStripButton2_Click`
  - „Novi radni nlog“ — klik → `toolStripButton3_Click` — otvara: frmRadniNalog
  - `this` — otvaranje forme → `frmRAdniNalogPregledPoLokomotivama_Load` — čita: Mesta

#### frmRadniNalogPregledSluzbenik [S]
- **Fajl:** `Uvoz/frmRadniNalogPregledSluzbenik.cs` — „RADNI NALOG PREGLED TERMINAL SLUŽBENIK“
- **Stari izgled:** Terminal › Terminalske usluge › ZADACI TERMINAL SLUŽBENIK → **frmRadniNalogPregledSluzbenik**
- **Otvara:** frmPrijemVozaIzPlana („FORMIRAJ OPERACIJE“), frmOtpremaKontejneraKamionomIzKontejnera („FORMIRAJ OPERACIJE“), frmPrijemKontejneraKamionLegetUvoz („FORMIRAJ OPERACIJE“), frmOtpremaVozaIzPlana („FORMIRAJ OPERACIJE“), RN12MedjuskladisniKontejnera („FORMIRAJ OPERACIJE“)
- **Tabele:** `IzvozKonacna` S, `IzvozKonacnaVrstaManipulacije` S, `KontejnerStatus` S, `KontejnerTekuce` S, `organizacioneJedinice` S, `Partnerji` S, `RadniNalogInterni` S, `TipKontenjera` S, `UvozKonacna` S, `UvozKonacnaVrstaManipulacije` S, `VrstaManipulacije` S
- **Procedure:** `InsertRadniNalogInterniStorno`, `UpdateRadniNalogInterniZavrsen`
- **Funkcionalnosti:**
  - „IZDAO OJ“ — klik → `toolStripButton1_Click` — čita 10 tabela; izvoz u Excel
  - „IZDAT ZA OJ“ — klik → `toolStripButton2_Click`
  - „STORNIRAJ SELEKTOVANE“ — klik → `toolStripButton3_Click` — proc: InsertRadniNalogInterniStorno; preko: InsertRadniNalogInterni.PromeniStatusStorno
  - „POSTAVI ZAVRSENO“ — klik → `toolStripButton4_Click`
  - „FORMIRAJ DOKUMENT“ — klik → `toolStripButton8_Click` — otvara: frmPrijemVozaIzPlana, frmOtpremaVozaIzPlana
  - „FORMIRAJ OPERACIJE“ — klik → `toolStripButton8_Click` — otvara: frmPrijemVozaIzPlana, frmOtpremaVozaIzPlana
  - „REFRESH“ — klik → `toolStripButton1_Click` — čita 10 tabela; izvoz u Excel
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`

#### frmRadniNalogTransport [N+S]
- **Fajl:** `Dokumenta/frmRadniNalogTransport.cs` — „Radni nalog transport“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Formiraj otpremu kamionom“ → **frmOtpremaKontejneraKamionomIzKontejnera** → „Otvori“ → **frmOtpremaKontejnera** → „Nalog Za Prevoz“ → **frmNalogZaPrevoz** → „Otvori putni nalog“ → **frmPutniNalog** → „Radni nalog“ → **frmRadniNalogTransport**
- **Stari izgled:** Terminal › Transport › Radni nalozi transport → **frmRadniNalogTransport**
- **Otvara:** frmAutoprevozniList2 („Autoprevozni list“)
- **Tabele:** `RadniNalogTransport` S, `Vozila` S, `Zaposleni` S
- **Procedure:** `DeleteRadniNalogTransportStavke`, `InsertRadniNalogTransportStavke`, `UpdateRadniNalogTransportStavke`, `UpdateRadniNalogZaTransport`
- **Combobox → tabela:** `cboPrikljucnoVozilo` ← `Vozila`; `cboVozilo` ← `Vozila`; `cboTransportniDispičer` ← `Zaposleni`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertRadniNalogTransport, UpdateRadniNalogZaTransport; preko: InsertRadniNalogTransport.InsRadniNalogZaTransport, InsertRadniNalogTransport.UpdRadniNalogZaTransport
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „Štampa radni nalog“ — klik → `toolStripLabel1_Click` — izveštaj: rptRadniNalogTransport.rdlc
  - „Autoprevozni list“ — klik → `toolStripLabel2_Click` — otvara: frmAutoprevozniList2
  - „Izbriši“ — klik → `button12_Click` — proc: DeleteRadniNalogTransportStavke; preko: InsertRadniNalogTransportStavke.DeleteRadniNalogTransportStavke
  - „Promeni“ — klik → `button13_Click` — proc: UpdateRadniNalogTransportStavke; preko: InsertRadniNalogTransportStavke.UpdRadniNalogTransportStavke
  - „Unesi“ — klik → `button14_Click` — proc: InsertRadniNalogTransportStavke; preko: InsertRadniNalogTransportStavke.InsRadniNalogTransportStavke
  - „Korisnik:“ — klik → `label11_Click`
  - „Stavka:“ — klik → `label8_Click`
  - „Podaci“ — klik → `tabPage1_Click`
  - `this` — otvaranje forme → `frmRadniNalogTransport_Load` — čita: Vozila, Zaposleni

#### frmRadniNalogTransport [N+S]
- **Fajl:** `TrackModal/Dokumeta/frmRadniNalogTransport.cs` — „Radni nalog transport“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Voz“ → **frmPrijemIOtpremaVozova4** → „Gate out voz“ → **frmPregledOtpreme** → „Otvori voz“ → **frmOtpremaKontejnera** → „Nalog Za Prevoz“ → **frmNalogZaPrevoz** → „Otvori putni nalog“ → **frmPutniNalog** → „Radni nalog“ → **frmRadniNalogTransport**
- **Stari izgled:** Terminal › Transport › Pregled radni nalozi transport → **frmPregledRadniNaloziTransport** → „Otvori radni nalog“ → **frmRadniNalogTransport**
- **Otvara:** frmAutoprevozniList2 („Autoprevozni list“)
- **Tabele:** `RadniNalogTransport` S, `Vozila` S, `Zaposleni` S
- **Procedure:** `DeleteRadniNalogTransportStavke`, `InsertRadniNalogTransportStavke`, `UpdateRadniNalogTransportStavke`, `UpdateRadniNalogZaTransport`
- **Combobox → tabela:** `cboPrikljucnoVozilo` ← `Vozila`; `cboVozilo` ← `Vozila`; `cboTransportniDispičer` ← `Zaposleni`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertRadniNalogTransport, UpdateRadniNalogZaTransport; preko: InsertRadniNalogTransport.InsRadniNalogZaTransport, InsertRadniNalogTransport.UpdRadniNalogZaTransport
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „Štampa radni nalog“ — klik → `toolStripLabel1_Click` — izveštaj: rptRadniNalogTransport.rdlc
  - „Autoprevozni list“ — klik → `toolStripLabel2_Click` — otvara: frmAutoprevozniList2
  - „Izbriši“ — klik → `button12_Click` — proc: DeleteRadniNalogTransportStavke; preko: InsertRadniNalogTransportStavke.DeleteRadniNalogTransportStavke
  - „Promeni“ — klik → `button13_Click` — proc: UpdateRadniNalogTransportStavke; preko: InsertRadniNalogTransportStavke.UpdRadniNalogTransportStavke
  - „Unesi“ — klik → `button14_Click` — proc: InsertRadniNalogTransportStavke; preko: InsertRadniNalogTransportStavke.InsRadniNalogTransportStavke
  - „Korisnik:“ — klik → `label11_Click`
  - „Stavka:“ — klik → `label8_Click`
  - „Podaci“ — klik → `tabPage1_Click`
  - `this` — otvaranje forme → `frmRadniNalogTransport_Load` — čita: Vozila, Zaposleni

#### frmRadniNalogTraseLok [S]
- **Fajl:** `Dokumenta/frmRadniNalogTraseLok.cs` — „Radni nalog trase “
- **Stari izgled:** Komercijala › Najave › Poslovi → **frmNajava** → „Prevozni nalog - Trase i Lokomotive“ → **frmRadniNalog** → „Unos trasa“ → **frmRadniNalogTraseLok**
- **Otvara:** frmKasnjenja („Uzroci kašnjenja“), frmPravljenjeVoza („Formiraj teretnicu“), frmPodtrase („Unos podtrasa“)
- **Tabele:** `Mesta` S, `Najava` S, `RadniNalog` S, `RadniNalogLokNaTrasi` S, `RadniNalogTrase` SUD p, `RadniNalogTraseLok` S, `RadniNalogVezaNajave` S, `Razlozi` S, `Stanice` S, `StatusTrasa` S, `Trase` S
- **Procedure:** `DeleteRadniNalogTrase`, `InserRadniNalogTrase`, `UpdateRadniNalogTrase`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InserRadniNalogTraseLok; preko: InsertRadniNalogTraseLok.InsRNTL
  - „Formiraj teretnicu“ — klik → `toolStripButton1_Click` — otvara: frmPravljenjeVoza
  - „Uzroci kašnjenja“ — klik → `toolStripButton2_Click` — otvara: frmKasnjenja
  - „Unos podtrasa“ — klik → `toolStripButton3_Click` — otvara: frmPodtrase
  - „Unesi trasu“ — klik → `button4_Click` — proc: InserRadniNalogTrase; preko: InsertRadniNalogTrase.InsRNT
  - „Promeni trasu“ — klik → `button5_Click` — proc: UpdateRadniNalogTrase; preko: InsertRadniNalogTrase.UpdRNT
  - „Izbriši trasu“ — klik → `button6_Click` — proc: DeleteRadniNalogTrase; preko: InsertRadniNalogTrase.delRNT
  - „Stare trase“ — klik → `button3_Click` — čita: Trase
  - „Nove Trase“ — klik → `button7_Click` — čita: Trase
  - `this` — otvaranje forme → `frmRadniNalogTraseLok_Load` — čita: Mesta, Razlozi, Stanice, StatusTrasa, Trase

#### frmRadniNalogZaposleni [S]
- **Fajl:** `Dokumenta/frmRadniNalogZaposleni.cs` — „Radni nalog osoblje “
- **Stari izgled:** Komercijala › Najave › Poslovi → **frmNajava** → „Prevozni nalog - Trase i Lokomotive“ → **frmRadniNalog** → „Osoblje“ → **frmRadniNalogZaposleni**
- **Tabele:** `Delavci` S, `PravoAktivnosti` S, `RadniNalogTraseLokZap` S, `RadniNalogTraseLokZapEvid` S, `Trase` S, `VrstaAktivnosti` S
- **Procedure:** `DeleteRadniNalogZaposleni`, `InsertRadniNalogZaposleni`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertRadniNalogZaposleni; preko: InsertRadniNalogZaposleni.InsRNTLZ
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteRadniNalogZaposleni; preko: InsertRadniNalogZaposleni.DelRNTLZ
  - `cboZaposleni` — SelectionChangeCommitted → `cboZaposleni_SelectionChangeCommitted` — čita: PravoAktivnosti, VrstaAktivnosti
  - „UBACI“ — klik → `btnUbaci_Click` — proc: InsertRadniNalogZapEv; preko: InsertRadniNalogZaposleniEvid.InsRNTLZEVID
  - „Pregledač“ — čekiranje → `chkPregledac_CheckedChanged`
  - `this` — otvaranje forme → `frmRadniNalogZaposleni_Load` — čita: Delavci, PravoAktivnosti, Trase, VrstaAktivnosti

#### frmRadniNaloziInterni [S]
- **Fajl:** `Uvoz/frmRadniNaloziInterni.cs` — „Radni nalozi Interni“
- **Stari izgled:** Izvoz › Zadaci › Unos RN obrazac → **frmRadniNaloziInterni**
- **Tabele:** `organizacioneJedinice` S, `RadniNalogInterni` SIUD p
- **Procedure:** `DeleteRadniNalogInterni`, `InsertRadniNalogInterni`, `UpdateRadniNalogInterni`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertRadniNalogInterni, UpdateRadniNalogInterni; preko: InsertRadniNalogInterni.InsRadniNalogInterni, InsertRadniNalogInterni.UpdRadniNalogInterni
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteRadniNalogInterni; preko: InsertRadniNalogInterni.DelRadniNalogInterni
  - „Izdat od“ — klik → `label43_Click`
  - `this` — otvaranje forme → `frmRadniNaloziInterni_Load` — čita: OrganizacioneJedinice

#### frmRaspustiVagone [S]
- **Fajl:** `Dokumenta/frmRaspustiVagone.cs` — „Raspusti vagone“
- **Stari izgled:** Operatika › Prevozni nalozi › Raspuštanje vagoni → **frmRaspustiVagone**
- **Tabele:** `RadniNalog` S, `Stanice` S, `TeretnicaIskljuceniVagoni` S, `TeretnicaStavke` S
- **Procedure:** `InsertTeretnicaStavkaRN`
- **Funkcionalnosti:**
  - „Pretraži“ — klik → `button3_Click` — čita: TeretnicaIskljuceniVagoni, TeretnicaStavke, stanice
  - „Raspusti vagone“ — klik → `button1_Click`
  - „Predaj vagone / Napravi predajnu teretnicu“ — klik → `button2_Click` — proc: InsertTeretnicaStavkaRN; preko: InsertTeretnicaStavke.InsTeretnicaStavkeRN
  - `this` — otvaranje forme → `frmRaspustiVagone_Load` — čita: RadniNalog, Stanice

#### frmRazlozi [S]
- **Fajl:** `Sifarnici/frmRazlozi.cs` — „Razlozi“
- **Stari izgled:** Osnovni podaci › Prevozi › Razlozi stajanja lokomotive → **frmRazlozi**
- **Tabele:** `Razlozi` SIU p
- **Procedure:** `InsertRazlozi`, `UpdateRazlozi`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertRazlozi, UpdateRazlozi; preko: InsertRazlozi.InsRazlozi, InsertRazlozi.UpdRazlozi

#### frmRazredNepravilnosti [S]
- **Fajl:** `Neispravnosti/frmRazredNepravilnosti.cs` — „Razred Nepravilnosti“
- **Stari izgled:** Evidencije › Vrste aktivnosti › Kolski deo → **frmRazredNepravilnosti**
- **Tabele:** `SifRazredNepravilnosti` SIUD p
- **Procedure:** `DeleteSifRazredNepravilnosti`, `InsertSifRazredNepravilnosti`, `UpdateSifRazredNepravilnosti`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertSifRazredNepravilnosti, UpdateSifRazredNepravilnosti; preko: InsertNepravilnosti.InsSifRazredNepravilnosti, InsertNepravilnosti.UpdSifRazredNepravilnosti
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteSifRazredNepravilnosti; preko: InsertNepravilnosti.DelSifRazredNepravilnosti

#### frmRegistator [S]
- **Fajl:** `Dokumenta/frmRegistator.cs` — „Delovodna knjiga  zapis“
- **Stari izgled:** Knjige › Delovodna knjiga › Novi zapis → **frmRegistator**
- **Otvara:** frmRegistratorDokumenta („Dokumenta“)
- **Tabele:** `Delavci` S, `Partnerji` S, `Registrator` SIUD p
- **Procedure:** `DeleteRegistrator`, `InsertRegistrator`, `UpdateRegistrator`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertRegistrator, UpdateRegistrator; preko: InsertRegistrator.InsRegistrator, InsertRegistrator.UpdRegistrator
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteRegistrator; preko: InsertRegistrator.DelRegistrator
  - „Štampa“ — klik → `toolStripButton1_Click` — izveštaj: rptRegistrator.rdlc
  - „Dokumenta“ — klik → `toolStripButton2_Click` — otvara: frmRegistratorDokumenta

#### frmRegistratorDokumenta [S]
- **Fajl:** `Dokumenta/frmRegistratorDokumenta.cs` — „Dokumenta delovodna knjiga“
- **Stari izgled:** Knjige › Delovodna knjiga › Novi zapis → **frmRegistator** → „Dokumenta“ → **frmRegistratorDokumenta**
- **Tabele:** `RegistratorDokumenta` SI p
- **Procedure:** `InsertRegistratorDokumenta`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „Pronadji“ — klik → `button1_Click`
  - „Otvori“ — klik → `button2_Click`
  - „Sačuvaj“ — klik → `button3_Click` — proc: InsertRegistratorDokumenta; preko: InsertRegistratorDokumenta.InsRegDokumenta

#### frmRegistratorPregled [S]
- **Fajl:** `Dokumenta/frmRegistratorPregled.cs` — „Pregled delovodna knjiga“
- **Stari izgled:** Knjige › Delovodna knjiga › Pregled → **frmRegistratorPregled**
- **Otvara:** frmRegistator („OTVORI“)
- **Tabele:** `Delavci` S, `Partnerji` S, `Registrator` S
- **Funkcionalnosti:**
  - „AŽURIRAJ“ — klik → `metroButton1_Click`
  - „OTVORI“ — klik → `metroButton2_Click`

#### frmRelacija [N]
- **Fajl:** `Izvoz/frmRelacija.cs` — „Relacija“
- **Novi izgled:** Logistika izvoza → **LogistikaIzvoza1** → „Formiranje naloga za otpremu“ → **LogistikaIzvoza2** → „Kreiraj novi“ → **frmProdajniNalogIzvozTabela** → `gridGroupingControl1` (TableControlCellClick) → **frmGrupniUnosPoljaIzvoz** → „Relacija“ → **frmRelacija**
- **Tabele:** `Izvoz` S, `KontejnerskiTerminali` S, `MestaUtovara` S, `partnerjiKontOsebaMU` S, `VrstaVozila` S
- **Procedure:** `UpdateIzvozPorudzbenicaRelacija`
- **Combobox → tabela:** `cboOdlaznaMorskaLuka1` ← `KontejnerskiTerminali`, `MestaUtovara`; `cboVrstaKamiona` ← `VrstaVozila`; `cboCilj` ← `partnerjiKontOsebaMU`; `cboKontakt` ← `partnerjiKontOsebaMU`
- **Funkcionalnosti:**
  - `button21` — klik → `button21_Click` — proc: UpdateIzvozPorudzbenicaRelacija; preko: InsertIzvoz.UpdateIzvozPorudzbenicaRelacija

#### frmRIDPoNajavama [S]
- **Fajl:** `Dokumenta/frmRIDPoNajavama.cs` — „RID po najavama“
- **Stari izgled:** Komercijala › RID › RID po najavama → **frmRIDPoNajavama**
- **Tabele:** `Najava` S, `Partnerji` S, `Stanice` S, `Teretnica` S, `TeretnicaStavke` S
- **Funkcionalnosti:**
  - „Pronađi“ — klik → `button4_Click`
  - `this` — otvaranje forme → `frmRIDPoNajavama_Load` — čita: Najava, Partnerji, Teretnica, TeretnicaStavke, stanice

#### frmRIDTeretnice [S]
- **Fajl:** `Dokumenta/frmRIDTeretnice.cs` — „Najave koje nisu u statusu 7 i 8“
- **Stari izgled:** Komercijala › RID › Teretnice bez RID-a → **frmRIDTeretnice**
- **Tabele:** `Najava` S, `TeretnicaStavke` S
- **Funkcionalnosti:**
  - `this` — otvaranje forme → `frmRIDTeretnice_Load` — čita: Najava, TeretnicaStavke

#### frmRN12InterniPrenos [N]
- **Fajl:** `TerminalMap/frmRN12InterniPrenos.cs` — „RN12 - MEdjuskladisni interni prenos“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Depo cnt“ → **frmPrijemIOtpremaVozova2** → „Pregled skladišta“ → **TerminalMapFRM** → „Interni prenos“ → **frmRN12InterniPrenos**
- **Tabele:** `KontejnerStatus` S, `KontejnerTekuce` S, `Partnerji` S, `Pozicija` S, `PredefinisanePozicije` S, `Skladista` S, `SkladistaGrupa` S, `TipKontenjera` S, `uvKvalitetKontejnera` S, `Zone` S
- **Procedure:** `InsertRN12Medjuskladisni`
- **Combobox → tabela:** `cboNaSklad` ← `Skladista`; `cboNaSklad` ← `PredefinisanePozicije`, `Skladista`; `cboNaSklad` ← `Skladista`
- **Funkcionalnosti:**
  - „Sva polja“ — klik → `button4_Click` — čita: Skladista
  - „Predefinisana polja“ — klik → `button1_Click` — čita: PredefinisanePozicije, Skladista
  - „Formiraj interni prenos“ — klik → `button2_Click` — proc: InsertRN12Medjuskladisni; preko: InsertRN.InsRN12Medjuskladisni
  - `this` — otvaranje forme → `frmRN12InterniPrenos_Load` — čita: Skladista

#### frmRutaTrase [S]
- **Fajl:** `Dokumenta/frmRutaTrase.cs` — „Ruta trase“
- **Stari izgled:** Osnovni podaci › Prevozni šifarnici › Trase → **frmTrase** → „Spisak stanica na trasi“ → **frmRutaTrase**
- **Tabele:** `RuteTrase` S, `Stanice` S, `Trase` S
- **Procedure:** `DeleteRutaTrase`, `InsertRutaTrase`, `UpdateRutaTrase`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertRutaTrase, UpdateRutaTrase; preko: InsertRutaTrase.InsRutaTrase, InsertRutaTrase.UpdRutaTrase
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteRutaTrase; preko: InsertRutaTrase.DeleteRutaTrase
  - „Stanica:“ — klik → `label3_Click`
  - `this` — otvaranje forme → `frmRutaTrase_Load` — čita: Stanice

#### frmScenario [S]
- **Fajl:** `Sifarnici/frmScenario.cs` — „Scenario “
- **Stari izgled:** Terminal › Šifarnici › Scenario → **frmScenario**
- **Otvara:** frmTerminalProces („Terminal proces“)
- **Tabele:** `KontejnerStatus` S, `Scenario` SIU p, `VrstaManipulacije` S
- **Procedure:** `InsertScenario`, `UpdateScenario`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertScenario, UpdateScenario; preko: InsertScenario.InsScenario, InsertScenario.UpdScenario
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „Terminal proces“ — klik → `toolStripButton1_Click` — otvara: frmTerminalProces
  - „Refresh“ — klik → `button1_Click`
  - „Po scenariju“ — klik → `button25_Click`
  - „Nova pozicija“ — klik → `button24_Click` — proc: InsertScenario; preko: InsertScenario.InsScenario
  - „Terminal proces“ — klik → `button23_Click` — otvara: frmTerminalProces
  - `button21` — klik → `tsSave_Click` — proc: InsertScenario, UpdateScenario; preko: InsertScenario.InsScenario, InsertScenario.UpdScenario
  - `this` — otvaranje forme → `frmScenario_Load` — čita: KontejnerStatus, VrstaManipulacije

#### frmScenarioSCI [N+S]
- **Fajl:** `RNI/frmScenarioSCI.cs` — „Po scenariju 1“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Pregledaj podatke“ → **frmScenarioSCI**
- **Stari izgled:** Izvoz › Zadaci › Pregled radnih naloga → **frmRadniNalogInterniPregled** → „Pregledaj podatke“ → **frmScenarioSCI**
- **Tabele:** `Carinarnice` S, `InspekciskiTretman` S, `IzvozKonacna` S, `IzvozKonacnaNHM` S, `IzvozNapomenePozicioniranja` S, `KontejnerskiTerminali` S, `MestaUtovara` S, `NapomenaZaPozicioniranje` S, `NHM` S, `Partnerji` S, `partnerjiKontOsebaMU` S, `ProdajniNalogIzvoz` S, `TipKontenjera` S, `uvKvalitetKontejnera` S, `uvNacinPakovanja` S, `VrstaCarinskogPostupka` S, `VrstaRobeADR` S, `VrstaVozila` S
- **Combobox → tabela:** `cboNalogodavac` ← `KontejnerskiTerminali`, `MestaUtovara`, `Partnerji`; `cboKvalitetKontejnera` ← `uvKvalitetKontejnera`; `cboVrstaKamiona` ← `VrstaVozila`; `cboPolaznaCarinarnica` ← `Carinarnice`; `cboSpediterPolazna` ← `Partnerji`; `cboOdredisnaCarinarnica` ← `Carinarnice`; `cboSpediterOdredisna` ← `Partnerji`; `cboCarinskiPUnutrasniTransport` ← `VrstaCarinskogPostupka`; `cboVrstaKontejnera` ← `TipKontenjera`; `cboADR` ← `VrstaRobeADR`; `cboInspekciskiTretman` ← `InspekciskiTretman`; `cboNacinPakovanja4` ← `uvNacinPakovanja`; `cboCilj` ← `partnerjiKontOsebaMU`; `cboKontakt` ← `partnerjiKontOsebaMU`

#### frmSelectTransport1 [S]
- **Fajl:** `Izvestaji/frmSelectTransport1.cs` — „Izveštaj o prevozu kamenog agregata“
- **Stari izgled:** Terminal › Transport › Izveštaji › Kameni agregat → **frmSelectTransport1**
- **Funkcionalnosti:**
  - „Štampaj“ — klik → `btnStampa_Click` — izveštaj: rptSelectTransport1.rdlc

#### frmSelectTransport2 [S]
- **Fajl:** `Izvestaji/frmSelectTransport2.cs` — „Izveštaj po tipu kontejnera“
- **Stari izgled:** Terminal › Transport › Izveštaji › Po tipu kontejnera → **frmSelectTransport2**
- **Funkcionalnosti:**
  - „Štampaj“ — klik → `btnStampa_Click` — izveštaj: rptSelectTransport2.rdlc

#### frmSelectTransport3 [S]
- **Fajl:** `Izvestaji/frmSelectTransport3.cs` — „Izveštaj po relaciji“
- **Stari izgled:** Terminal › Transport › Izveštaji › Ture po relaciji → **frmSelectTransport3**
- **Funkcionalnosti:**
  - „Štampaj“ — klik → `btnStampa_Click` — izveštaj: rptSelectTransport3.rdlc

#### frmSelectTransport4 [S]
- **Fajl:** `Izvestaji/frmSelectTransport4.cs` — „Izveštaj po vozilu“
- **Stari izgled:** Terminal › Transport › Izveštaji › Vozila po PN → **frmSelectTransport4**
- **Funkcionalnosti:**
  - „Štampaj“ — klik → `btnStampa_Click` — izveštaj: rptSelectTransport4.rdlc

#### frmSelectTransport5 [S]
- **Fajl:** `Izvestaji/frmSelectTransport5.cs` — „Lista tura po vozaču“
- **Stari izgled:** Terminal › Transport › Izveštaji › Vozači po rutama → **frmSelectTransport5**
- **Funkcionalnosti:**
  - „Štampaj“ — klik → `btnStampa_Click` — izveštaj: rptSelectTransport5.rdlc

#### frmSelectTransport6 [S]
- **Fajl:** `Izvestaji/frmSelectTransport6.cs` — „Spisak kvarova“
- **Stari izgled:** Terminal › Transport › Izveštaji › Kvarovi → **frmSelectTransport6**
- **Funkcionalnosti:**
  - „Štampaj“ — klik → `btnStampa_Click` — izveštaj: rptKvarovi.rdlc

#### frmSendEmailWithAttacment [S]
- **Fajl:** `frmSendEmailWithAttacment.cs` — „Send e mail“
- **Stari izgled:** Komercijala › Najave › Poslovi → **frmNajava** → „Pošalji mail“ → **frmSendEmailWithAttacment**
- **Tabele:** `Najava` S, `NHM` S, `Partnerji` S, `Stanice` S, `StatusVoza` S, `Trase` S
- **Funkcionalnosti:**
  - „Pošalji“ — klik → `button2_Click` — čita: NHM, Najava, Partnerji, StatusVoza, Trase, stanice

#### frmSeniranjeDokumenata [N+S]
- **Fajl:** `Drumski/frmSeniranjeDokumenata.cs` — „Skeniranje dokumenata“
- **Novi izgled:** Operacije skladišta → **SkladistaMain** → „Carinsko skladište“ → **CarinskoSkladiste** → „Prijem robe“ → **CarinskoSkladistePregled** → „Skeniraj“ → **frmSeniranjeDokumenata**
- **Stari izgled:** Drumski › Finansije › Lista naloga → **frmPregledFaktura** → „Dodaj skeniranu dokumentaciju“ → **frmSeniranjeDokumenata**
- **Tabele:** `Delavci` S, `DokumentaFaktureDrumski` SI p, `DokumentaSkladiste` SI p, `Korisnici` S, `RadniNalogDrumski` S
- **Procedure:** `InsertDokumentaFaktureDrumski`, `InsertDokumentaSkladiste`
- **Funkcionalnosti:**
  - „Osveži listu“ — klik → `BtnRefreshScanners_Click`
  - „Započni skeniranje“ — klik → `BtnScan_Click`
  - „Započni izmenu“ — klik → `btnEditImage_Click`
  - „Obriši stranu“ — klik → `BtnDelete_Click`
  - „Snimi kao fakturu“ — klik → `BtnSaveFaktura_Click` — proc: InsertDokumentaFaktureDrumski; preko: InsertFakture.SnimiUFajlBazu
  - „Snimi kao prevoznički dokument“ — klik → `BtnSaveTransport_Click`

#### frmSERDeo [N]
- **Fajl:** `Servis/frmSERDeo.cs`
- **Novi izgled:** Održavanje → **frmSERMeni** → kartica „Zalihe i šifarnik“ → **frmSERZalihe** → pri otvaranju forme → **frmSERDeo**
- **Tabele:** `SERDeo` S, `SERDeoPrimenljivost` SD, `SERSredstvo` S, `SERVrstaSredstva` S, `vSERDeoStanje` S, `vSERKarticaDela` S, `vSERNarudzbenica` S, `vSERNarudzbenicaStavka` S

#### frmSerijeKola [S]
- **Fajl:** `Uvoz/frmSerijeKola.cs` — „Serije kola“
- **Stari izgled:** Terminal › Šifarnici › Serije kola → **frmSerijeKola**
- **Tabele:** `SerijeKola` SIUD p
- **Procedure:** `DeleteSerijeKola`, `InsertSerijeKola`, `UpdateSerijeKola`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertSerijeKola, UpdateSerijeKola; preko: InsertSerijeKola.InsSerijeKola, InsertSerijeKola.UpdSeijeKola
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteSerijeKola; preko: InsertSerijeKola.DelSerijeKola
  - `button20` — klik → `tsDelete_Click` — proc: DeleteSerijeKola; preko: InsertSerijeKola.DelSerijeKola
  - `button21` — klik → `tsSave_Click` — proc: InsertSerijeKola, UpdateSerijeKola; preko: InsertSerijeKola.InsSerijeKola, InsertSerijeKola.UpdSeijeKola

#### frmSerijeLokomotiva [S]
- **Fajl:** `Sifarnici/frmSerijeLokomotiva.cs`
- **Stari izgled:** Osnovni podaci › Prevozni šifarnici › Serije lokomotive → **frmSerijeLokomotiva**
- **Tabele:** `LokomotivaSerija` SIUD p
- **Procedure:** `DeleteLokomotivaSerija`, `InsertLokomotivaSerija`, `UpdateLokomotivaSerija`
- **Funkcionalnosti:**
  - `tsSave` — klik → `tsSave_Click` — proc: InsertLokomotivaSerija, UpdateLokomotivaSerija; preko: InsertLokomotivaSerija.InsLokomotivaSerija, InsertLokomotivaSerija.UpdLokomotivaSerija
  - `button20` — klik → `button20_Click` — proc: DeleteLokomotivaSerija; preko: InsertLokomotivaSerija.DeleteLokomotivaSerija
  - `button21` — klik → `tsSave_Click` — proc: InsertLokomotivaSerija, UpdateLokomotivaSerija; preko: InsertLokomotivaSerija.InsLokomotivaSerija, InsertLokomotivaSerija.UpdLokomotivaSerija

#### frmSERKorekcije [N]
- **Fajl:** `Servis/frmSERKorekcije.cs`
- **Novi izgled:** Održavanje → **frmSERMeni** → kartica „Korekcije zaliha“ → **frmSERKorekcije**
- **Tabele:** `SERDeo` S, `SERDokument` S, `SERKorekcija` S, `SERStatus` S

#### frmSERMeni [N]
- **Fajl:** `Servis/frmSERMeni.cs`
- **Novi izgled:** Održavanje → **frmSERMeni**
- **Otvara:** frmSERZalihe (kartica „Zalihe i šifarnik“), frmSERPromet (kartica „Izdatnice i prijemi“), frmSERNabavka (kartica „Potrebe za nabavkom“), frmSERPopis (kartica „Popis zaliha“), frmSERNalozi (kartica „Radni nalozi“), frmSERKorekcije (kartica „Korekcije zaliha“), frmSERObavestenja (kartica „Obaveštenja“), frmSERSifarnici (kartica „Sredstva i plan održavanja“)
- **Tabele:** `SERKorekcija` S, `SERObavestenje` S, `vSERPredlogNabavke` S

#### frmSERNabavka [N]
- **Fajl:** `Servis/frmSERNabavka.cs`
- **Novi izgled:** Održavanje → **frmSERMeni** → kartica „Potrebe za nabavkom“ → **frmSERNabavka**
- **Otvara:** frmSERNarudzbenica (metoda `Otvori`)
- **Tabele:** `Podesavanja` S, `SERStatus` S, `vSERNarudzbenica` S, `vSERPredlogNabavke` S
- **Procedure:** `sp_set_session_context`

#### frmSERNalog [N]
- **Fajl:** `Servis/frmSERNalog.cs`
- **Novi izgled:** Održavanje → **frmSERMeni** → kartica „Radni nalozi“ → **frmSERNalozi** → pri otvaranju forme → **frmSERNalog**
- **Tabele:** `SERDeo` S, `SERDeoKretanje` S, `SERDeoPrimenljivost` S, `SERDeoRezervacija` S, `SERPlanStavka` S, `SERRadniNalog` S, `SERSredstvo` S, `vSERDeoStanje` S, `vSERRadniNalogDelovi` S, `vSERRadniNalogTrosak` S

#### frmSERNalozi [N]
- **Fajl:** `Servis/frmSERNalozi.cs`
- **Novi izgled:** Održavanje → **frmSERMeni** → kartica „Radni nalozi“ → **frmSERNalozi**
- **Otvara:** frmSERNalog (pri otvaranju forme)
- **Tabele:** `SERPlanStavka` S, `SERRadniNalog` S, `SERSredstvo` S, `vSERRadniNalogTrosak` S

#### frmSERNarudzbenica [N]
- **Fajl:** `Servis/frmSERNarudzbenica.cs`
- **Novi izgled:** Održavanje → **frmSERMeni** → kartica „Potrebe za nabavkom“ → **frmSERNabavka** → metoda `Otvori` → **frmSERNarudzbenica**
- **Tabele:** `SERDeo` S, `SERNarudzbenicaStavka` S, `vSERNarudzbenica` S, `vSERNarudzbenicaStavka` S

#### frmSERObavestenja [N]
- **Fajl:** `Servis/frmSERObavestenja.cs`
- **Novi izgled:** Održavanje → **frmSERMeni** → kartica „Obaveštenja“ → **frmSERObavestenja**
- **Otvara:** frmSERDeo (metoda `OtvoriDeo`)
- **Tabele:** `SERObavestenje` SU

#### frmSERPopis [N]
- **Fajl:** `Servis/frmSERPopis.cs`
- **Novi izgled:** Održavanje → **frmSERMeni** → kartica „Popis zaliha“ → **frmSERPopis**
- **Tabele:** `SERDeo` S, `SERPopisStavka` S, `vSERPopis` S

#### frmSERPrijem [N]
- **Fajl:** `Servis/frmSERPrijem.cs`
- **Novi izgled:** Održavanje → **frmSERMeni** → kartica „Izdatnice i prijemi“ → **frmSERPromet** → metoda `NoviPrijem` → **frmSERPrijem**
- **Tabele:** `Podesavanja` S, `SERDeo` S, `vSERNarudzbenica` S, `vSERNarudzbenicaStavka` S
- **Combobox → tabela:** `cbNarudzbenica` ← `vSERNarudzbenica`

#### frmSERPromet [N]
- **Fajl:** `Servis/frmSERPromet.cs`
- **Novi izgled:** Održavanje → **frmSERMeni** → kartica „Izdatnice i prijemi“ → **frmSERPromet**
- **Otvara:** frmSERPrijem (metoda `NoviPrijem`)
- **Tabele:** `SERDeo` S, `SERDeoKretanje` S, `vSERPromet` S

#### frmSERSifarnici [N]
- **Fajl:** `Servis/frmSERSifarnici.cs`
- **Novi izgled:** Održavanje → **frmSERMeni** → kartica „Sredstva i plan održavanja“ → **frmSERSifarnici**
- **Tabele:** `SERDeo` S, `SERDeoGrupa` S, `SERPlanNormativ` SUD, `SERPlanStavka` S, `SERSredstvo` S, `SERVrstaSredstva` S

#### frmSERStavka [N]
- **Fajl:** `Servis/frmSERStavka.cs`
- **Novi izgled:** Održavanje → **frmSERMeni** → kartica „Zalihe i šifarnik“ → **frmSERZalihe** → metoda `Razlog` → **frmSERStavka**

#### frmSERZalihe [N]
- **Fajl:** `Servis/frmSERZalihe.cs`
- **Novi izgled:** Održavanje → **frmSERMeni** → kartica „Zalihe i šifarnik“ → **frmSERZalihe**
- **Otvara:** frmSERDeo (pri otvaranju forme)
- **Tabele:** `SERDeo` S, `SERDeoKretanje` S, `vSERDeoStanje` S

#### frmSifarnikKontrolnihGresaka [S]
- **Fajl:** `Sifarnici/frmSifarnikKontrolnihGresaka.cs` — „Šifarnik kontrolnih grešaka“
- **Stari izgled:** Operatika › Kontrola dokumentacije › Šifarnik grešaka → **frmSifarnikKontrolnihGresaka**
- **Tabele:** `KontrolneGreske` SIUD p, `KontrolneGreskeTipDokumenta` S
- **Procedure:** `DeleteKontrolneGreske`, `InsertKontrolneGreske`, `UpdateKontrolneGreske`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertKontrolneGreske, UpdateKontrolneGreske; preko: InsertKontrolneGreske.InsKontrolneGreske, InsertKontrolneGreske.UpdKontrolneGreske
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteKontrolneGreske; preko: InsertKontrolneGreske.DeleteKontrolneGreske
  - `this` — otvaranje forme → `frmSifarnikKontrolnihGresaka_Load` — čita: KontrolneGreskeTipDokumenta

#### frmSifarnikKontrolnihTipovaDokumenta [S]
- **Fajl:** `Sifarnici/frmSifarnikKontrolnihTipovaDokumenta.cs` — „Tipovi dokumenata“
- **Stari izgled:** Operatika › Kontrola dokumentacije › Tip dokumenta → **frmSifarnikKontrolnihTipovaDokumenta**
- **Tabele:** `KontrolneGreskeTipDokumenta` SIUD p
- **Procedure:** `DeleteKontrolneGreskeTipDokumenta`, `InsertKontrolneGreskeTipDokumenta`, `UpdateKontrolneGreskeTipDokumenta`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertKontrolneGreskeTipDokumenta, UpdateKontrolneGreskeTipDokumenta; preko: InsertKontrolneGreskeTipDokumenta.InsKontrolneGreskeTipDokumenta, InsertKontrolneGreskeTipDokumenta.UpdKontrolneGreskeTipDokumenta
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteKontrolneGreskeTipDokumenta; preko: InsertKontrolneGreskeTipDokumenta.DeleteKontrolneGreskeTipDokumenta

#### frmSistemskaTabela [P]
- **Fajl:** `Administracija/frmSistemskaTabela.cs` — „ Konfiguracioni podaci“
- **Tabele:** `SistemskePostavkeHeader` S
- **Procedure:** `ReadAllImageIDs`, `ReadImage`, `SaveImage`
- **Funkcionalnosti:**
  - „Load and Save image“ — klik → `sfButton1_Click` — proc: SaveImage
  - „Display“ — klik → `sfButton5_Click` — proc: ReadImage

#### frmSkladista [N+S]
- **Fajl:** `TrackModal/Sifarnici/frmSkladista.cs` — „Polja“
- **Novi izgled:** Podešavanja sistema → **Podesavanje1** → „Polja“ → **frmSkladista**
- **Stari izgled:** Terminal › Šifarnici › Skladista → **frmSkladista**
- **Tabele:** `Partnerji` S, `Skladista` SIUD p, `SkladistaGrupa` S, `SkladisteObjekat` S, `TipKontenjera` S, `TipPalete` S, `TipSkladista` S, `uvKvalitetKontejnera` S
- **Procedure:** `DeleteSkladista`, `InsertSkladista`, `UpdateSkladista`
- **Combobox → tabela:** `cboBrodar` ← `Partnerji`; `cboKvalitet` ← `uvKvalitetKontejnera`; `cboTipKontejnera` ← `TipKontenjera`; `cboTipPalete` ← `TipPalete`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click_1`
  - „tsSave“ — klik → `tsSave_Click` — čita: SkladisteObjekat; proc: InsertSkladista, UpdateSkladista; preko: InsertSkladista.InsSkladista, InsertSkladista.UpdSkladista
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteSkladista; preko: InsertSkladista.DeleteSkladista
  - „toolStripButton1“ — klik → `tsPrvi_Click` — čita: Skladista
  - „toolStripButton1“ — klik → `tsNazad_Click` — čita: Skladista
  - „toolStripButton1“ — klik → `tsNapred_Click` — čita: Skladista
  - „toolStripButton1“ — klik → `tsPoslednja_Click` — čita: Skladista
  - „gridGroupingControl1“ — SelectedRecordsChanged → `gridGroupingControl1_SelectedRecordsChanged`
  - `button20` — klik → `tsDelete_Click` — proc: DeleteSkladista; preko: InsertSkladista.DeleteSkladista
  - `button21` — klik → `tsSave_Click` — čita: SkladisteObjekat; proc: InsertSkladista, UpdateSkladista; preko: InsertSkladista.InsSkladista, InsertSkladista.UpdSkladista
  - „Neaktivne“ — klik → `button1_Click`
  - „Aktivne“ — klik → `button23_Click`
  - „Refresh“ — klik → `button2_Click`
  - `this` — otvaranje forme → `frmSkladista_Load` — čita: Partnerji, TipKontenjera, TipPalete, uvKvalitetKontejnera

#### frmSkladisteObjekat [N]
- **Fajl:** `TrackModal/Sifarnici/frmSkladisteObjekat.cs` — „Objekti“
- **Novi izgled:** Podešavanja sistema → **Podesavanje1** → „Objekti“ → **frmSkladisteObjekat**
- **Tabele:** `Skladista` S, `SkladistaGrupa` S, `SkladisteObjekat` S, `TipSkladista` S, `Zone` S
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „Sačuvaj“ — klik → `tsSave_Click` — čita: Skladista
  - „Obriši“ — klik → `tsDelete_Click`
  - „Refresh“ — klik → `button2_Click`
  - „Neaktivne“ — klik → `button1_Click`
  - „Aktivne“ — klik → `button23_Click`
  - `button21` — klik → `tsSave_Click` — čita: Skladista
  - „gridGroupingControl1“ — SelectedRecordsChanged → `gridGroupingControl1_SelectedRecordsChanged`
  - `this` — otvaranje forme → `frmSkladisteObjekat_Load` — čita: TipSkladista

#### frmSkladisteOtprema [S]
- **Fajl:** `Promet/frmSkladisteOtprema.cs` — „frmSkladisteOtprema“
- **Stari izgled:** Terminal › Skladišno poslovanje › Otprema usluga → **frmSkladisteOtprema**
- **Otvara:** frmPregledOtprema („Radni nalog“)
- **Tabele:** `Delavci` S, `NaruceneManipulacije` S, `NHM` S, `OtpremaKontejnera` S, `OtpremaKontejneraVozStavke` S, `Partnerji` S, `Pozicija` S, `Promet` S, `PrometManipulacije` S, `Skladista` S, `TipKontenjera` S, `Voz` S, `Vozila` S, `VrstaManipulacije` S, `Zaposleni` S
- **Funkcionalnosti:**
  - „Radni nalog“ — klik → `toolStripButton3_Click`
  - „>>>>“ — klik → `button2_Click`
  - „Brisanje sredstva/Zaposlenog“ — klik → `button4_Click`
  - „Brisanje promet“ — klik → `button3_Click`
  - `cboSkladiste` — izbor → `cboSkladiste_SelectedValueChanged` — čita: Pozicija
  - „>>>>“ — klik → `btnUbaci_Click`
  - „Pretraži“ — klik → `button1_Click` — čita: NHM, OtpremaKontejnera, OtpremaKontejneraVozStavke, TipKontenjera
  - „Pretraži“ — klik → `btnPretrazi_Click` — čita: NHM, OtpremaKontejnera, OtpremaKontejneraVozStavke, TipKontenjera
  - `dataGridView1` — izbor → `dataGridView1_SelectionChanged` — čita: NaruceneManipulacije, Partnerji, VrstaManipulacije
  - `cboSkladisteU` — izbor → `cboSkladisteU_SelectedValueChanged` — čita: Pozicija
  - `this` — otvaranje forme → `frmSkladisteOtprema_Load` — čita: Delavci, OtpremaKontejnera, Skladista, Voz, Vozila

#### frmSkladistePrijem [S]
- **Fajl:** `TrackModal/Promet/frmSkladistePrijem.cs` — „Prijem kontejnera“
- **Stari izgled:** Terminal › Skladišno poslovanje › Prijem raspored → **frmSkladistePrijem**
- **Otvara:** frmSredstvoRada („Radni nalog“)
- **Tabele:** `Komitenti` S, `NaruceneManipulacije` S, `Pozicija` S, `PrijemKontejneraVoz` S, `PrijemKontejneraVozStavke` S, `Promet` SID p, `PrometManipulacije` SID p, `Skladista` S, `TipKontenjera` S, `Voz` S, `Vozila` S, `VrstaManipulacije` S, `VrstaRobe` S, `Zaposleni` S
- **Procedure:** `DeletePromet`, `DeletePrometManipulacije`, `InsertPromet`, `InsertPrometManipulacije`
- **Funkcionalnosti:**
  - „Pretraži“ — klik → `button1_Click` — čita: PrijemKontejneraVoz, PrijemKontejneraVozStavke, TipKontenjera, VrstaRobe
  - „Pretraži“ — klik → `btnPretrazi_Click` — čita: PrijemKontejneraVoz, PrijemKontejneraVozStavke, TipKontenjera, VrstaRobe
  - `dataGridView1` — izbor → `dataGridView1_SelectionChanged` — čita: Komitenti, NaruceneManipulacije, VrstaManipulacije
  - `cboSkladiste` — izbor → `cboSkladiste_SelectedValueChanged` — čita: Pozicija
  - „Datum i vreme prijema“ — klik → `label4_Click`
  - „>>>>“ — klik → `btnUbaci_Click` — proc: InsertPromet; preko: InsertPromet.InsProm
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click`
  - „toolStripButton1“ — klik → `tsPrvi_Click` — čita: Promet
  - „toolStripButton1“ — klik → `tsNazad_Click` — čita: Promet
  - „toolStripButton1“ — klik → `tsNapred_Click` — čita: Promet
  - „toolStripButton1“ — klik → `tsPoslednja_Click` — čita: Promet
  - „Radni nalog“ — klik → `toolStripButton3_Click`
  - „Brisanje promet“ — klik → `button3_Click` — proc: DeletePromet; preko: InsertPromet.DeleteProm
  - „Brisanje sredstva/Zaposlenog“ — klik → `button4_Click` — proc: DeletePrometManipulacije; preko: InsertPromet.DeletePromManipulacije
  - „>>>>“ — klik → `button2_Click_1` — proc: InsertPrometManipulacije; preko: InsertPromet.InsPromMan
  - `this` — otvaranje forme → `frmSkladistePrijem_Load` — čita: PrijemKontejneraVoz, Skladista, Voz, Vozila, Zaposleni
  - „Prijem vozom:“ — klik → `label5_Click`
  - „Kontejneri:“ — klik → `label2_Click`
  - „toolStripButton2“ — klik → `toolStripButton2_Click`
  - „Neto:“ — klik → `label26_Click`
  - „Tara:“ — klik → `label25_Click`
  - „Ukupno:“ — klik → `label9_Click`

#### frmSlobodniDani [S]
- **Fajl:** `Mobile/frmSlobodniDani.cs` — „Slobodni dani GO“
- **Stari izgled:** Kadrovi › Godisnji odmori › Pristigli zahtevi → **frmSlobodniDani**
- **Otvara:** frmEvidencijaGodišnjihOdmora („Odobri“)
- **Tabele:** `Delavci` S, `EvidencijaZahteva` SU p, `EvidencujaZahtevaVrsta` S
- **Procedure:** `UpdateEvidencijaZahteva`
- **Funkcionalnosti:**
  - „Ažuriraj“ — klik → `metroButton1_Click`
  - „Odobri“ — klik → `metroButton2_Click` — proc: UpdateEvidencijaZahteva; preko: InsertSlobodniDani.UpdSlobodniDani
  - „Odbi“ — klik → `metroButton3_Click` — proc: UpdateEvidencijaZahteva; preko: InsertSlobodniDani.UpdSlobodniDani
  - `this` — otvaranje forme → `frmSlobodniDani_Load` — čita: Delavci

#### frmSredstvoRada [S]
- **Fajl:** `TrackModal/Promet/frmSredstvoRada.cs` — „Nalog za rad“
- **Stari izgled:** Terminal › Skladišno poslovanje › Prijem raspored → **frmSkladistePrijem** → „Radni nalog“ → **frmSredstvoRada**
- **Funkcionalnosti:**
  - „Štampa“ — klik → `button2_Click` — izveštaj: rptNalogZaRad.rdlc

#### frmSredstvoRada [S]
- **Fajl:** `Promet/frmSredstvoRada.cs` — „Nalog za rad“
- **Stari izgled:** Terminal › Skladišno poslovanje › Međuskladišni prenos → **frmMedjuskladisniPrenos** → „Nalog po sredstvima rada“ → **frmSredstvoRada**
- **Funkcionalnosti:**
  - „Štampa“ — klik → `button2_Click` — izveštaj: rptNalogZaRad.rdlc

#### frmStampaRadnogNaloga [S]
- **Fajl:** `Dokumenta/frmStampaRadnogNaloga.cs` — „Štampa radnog naloga“
- **Stari izgled:** Operatika › Prevozni nalozi › Štampa prevoza → **frmStampaRadnogNaloga**
- **Funkcionalnosti:**
  - „Štampaj“ — klik → `btnStampa_Click` — izveštaj: frmStampaRadnogNaloga.rdlc

#### frmStanice [S]
- **Fajl:** `Sifarnici/frmStanice.cs` — „Stanice“
- **Stari izgled:** Osnovni podaci › Prevozni šifarnici › Stanice → **frmStanice**
- **Tabele:** `Drzave` S, `Stanice` SIUD p
- **Procedure:** `DeleteStanice`, `InsertStanice`, `UpdateStanice`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertStanice, UpdateStanice; preko: InsertStanice.InsStanice, InsertStanice.UpdStanice
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteStanice; preko: InsertStanice.DeleteStanice
  - „toolStripButton1“ — klik → `tsPrvi_Click`
  - „toolStripButton1“ — klik → `tsNazad_Click`
  - „Država“ — klik → `label2_Click`
  - `button20` — klik → `tsDelete_Click` — proc: DeleteStanice; preko: InsertStanice.DeleteStanice
  - `button21` — klik → `tsSave_Click` — proc: InsertStanice, UpdateStanice; preko: InsertStanice.InsStanice, InsertStanice.UpdStanice

#### frmStatus [N]
- **Fajl:** `Drumski/frmStatus.cs` — „frmStatus“
- **Novi izgled:** Drumski transport → **Drumski1** → „Cerada“ → **Cerade** → „STATUSI VOZILA“ → **frmStatus**
- **Otvara:** frmPregledFajlova (`dataGridView3` (CellClick))
- **Tabele:** `Automobili` S, `AutomobiliTehnickiProblem` S, `Carinarnice` S, `Delavci` S, `DokumentaRadnogNalogaDrumski` S, `Dokumenti` S, `DrumskiPozicioniranje` S, `Izvoz` S, `IzvozKonacna` S, `KontejnerskiTerminali` S, `Korisnici` S, `MapiranjeTipaNaloga` S, `MestaUtovara` S, `NapomenaZaPozicioniranje` S, `Partnerji` S, `partnerjiKontOsebaMU` S, `RadniNalogDrumski` S, `RadniNalogInterni` S, `ScenarijaTokaTransporta` S, `ScenarioTokTransporta_Statusi` S, `SistemskePostavke` S, `StatusVozila` S, `TipKontenjera` S, `UploadTokens` S, `Uvoz` S, `UvozKonacna` S, `VrstaManipulacije` S, `VrstaVozila` S
- **Procedure:** `ArhiviranRadniNalogDrumski`, `InsertDokumentaRadniNalogDrumski`, `UpdatePoslatStatus`, `UpdateRadniNalogInterniZavrsen`, `UpdateStatusRadniNalogDrumski`
- **Funkcionalnosti:**
  - `dataGridView3` — promena vrednosti → `dataGridView3_CellValueChanged` — proc: ArhiviranRadniNalogDrumski, UpdatePoslatStatus, UpdateRadniNalogInterniZavrsen, UpdateStatusRadniNalogDrumski; preko: InsertRadniNalogDrumski.ArhiviranRadniNalogDrumski, InsertRadniNalogDrumski.UpdateStatusPoslat, InsertRadniNalogDrumski.UpdateStatusRadniNalogDrumski, InsertRadniNalogInterni.UpdRadniNalogInterniZavrsen
  - „Posalji statuse“ — klik → `btnPosaljiStatuse_Click` — proc: UpdatePoslatStatus; preko: InsertRadniNalogDrumski.UpdateStatusPoslat
  - „Filtriraj“ — klik → `btnFitriraj_Click`
  - `dataGridView3` — klik na ćeliju → `dataGridView3_CellContentClick` — otvara: frmPregledFajlova; proc: InsertDokumentaRadniNalogDrumski, UpdatePoslatStatus; preko: InsertRadniNalogDrumski.SnimiUFajlBazu, InsertRadniNalogDrumski.UpdateStatusPoslat

#### frmStatus [S]
- **Fajl:** `Kapija/frmStatus.cs` — „frmStatus“
- **Stari izgled:** Kapija › Šifarnici › Status kapija → **frmStatus**
- **Tabele:** `StatusKapija` SIUD p
- **Procedure:** `DeleteStatusKapija`, `InsertStatusKapija`, `UpdateStatusKapija`
- **Funkcionalnosti:**
  - `button20` — klik → `tsDelete_Click` — proc: DeleteStatusKapija; preko: InsertStatus.DelStatus
  - `button21` — klik → `tsSave_Click` — proc: InsertStatusKapija, UpdateStatusKapija; preko: InsertStatus.InsStatus, InsertStatus.UpdeteStatus
  - „Posalji statuse“ — klik → `btnPosaljiStatuse_Click`
  - „Filtriraj“ — klik → `btnFitriraj_Click`

#### frmStatusUsluge [S]
- **Fajl:** `Sifarnici/frmStatusUsluge.cs` — „Statusi uslluga“
- **Stari izgled:** Uvoz › Šifarnici › Status uluge → **frmStatusUsluge**
- **Tabele:** `MerskeEnote` SIUD p
- **Procedure:** `DeleteMerskeEnote`, `InsertMerskeEnote`, `UpdateMerskeEnote`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertMerskeEnote, UpdateMerskeEnote; preko: InsertMerskeEnote.InsMerskeEnote, InsertMerskeEnote.UpdMerskeEnote
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteMerskeEnote; preko: InsertMerskeEnote.DelMerskeEnote
  - `button20` — klik → `tsDelete_Click` — proc: DeleteMerskeEnote; preko: InsertMerskeEnote.DelMerskeEnote
  - `button21` — klik → `tsSave_Click` — proc: InsertMerskeEnote, UpdateMerskeEnote; preko: InsertMerskeEnote.InsMerskeEnote, InsertMerskeEnote.UpdMerskeEnote

#### frmStatusVoza [S]
- **Fajl:** `Sifarnici/frmStatusVoza.cs` — „Status voza“
- **Stari izgled:** Osnovni podaci › Prevozi › Status vozova → **frmStatusVoza**
- **Tabele:** `StatusVoza` SIUD p
- **Procedure:** `DeleteStatusVoza`, `InsertStatusVoza`, `UpdateStatusVoza`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertStatusVoza, UpdateStatusVoza; preko: InsertStatusVoza.InsStatusVoza, InsertStatusVoza.UpdStanice
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteStatusVoza; preko: InsertStatusVoza.DeleteStatusVoza
  - `button20` — klik → `tsDelete_Click` — proc: DeleteStatusVoza; preko: InsertStatusVoza.DeleteStatusVoza
  - `button21` — klik → `tsSave_Click` — proc: InsertStatusVoza, UpdateStatusVoza; preko: InsertStatusVoza.InsStatusVoza, InsertStatusVoza.UpdStanice

#### frmStatusVozila [S]
- **Fajl:** `Drumski/frmStatusVozila.cs` — „frmStatusVozila“
- **Stari izgled:** Drumski › Šifarnici › Status vozila → **frmStatusVozila**
- **Otvara:** frmStatusVozilaDetalji (`button22` (Click))
- **Tabele:** `StatusVozila` SIUD p
- **Procedure:** `DeleteStatusVozila`, `InsertStatusVozila`, `UpdateStatusVozila`
- **Funkcionalnosti:**
  - `button22` — klik → `tsNew_Click` — otvara: frmStatusVozilaDetalji
  - `dataGridView1` — izbor → `dataGridView1_SelectionChanged` — otvara: frmStatusVozilaDetalji

#### frmStatusVozilaDetalji [S]
- **Fajl:** `Drumski/frmStatusVozilaDetalji.cs` — „Status vozila detalji“
- **Stari izgled:** Drumski › Šifarnici › Status vozila → **frmStatusVozila** → `button22` (Click) → **frmStatusVozilaDetalji**
- **Tabele:** `StatusVozila` IUD p
- **Procedure:** `DeleteStatusVozila`, `InsertStatusVozila`, `UpdateStatusVozila`
- **Funkcionalnosti:**
  - `button20` — klik → `button20_Click` — proc: DeleteStatusVozila; preko: InsertStatus.DelStatusUsluga
  - `button21` — klik → `button21_Click` — proc: InsertStatusVozila, UpdateStatusVozila; preko: InsertStatus.InsStatusVozila, InsertStatus.UpdStatusVozila
  - „Cerade“ — čekiranje → `chk_CheckedChanged`
  - „Platforme“ — čekiranje → `chk_CheckedChanged`
  - „Vangabaritni“ — čekiranje → `chk_CheckedChanged`

#### frmStavkePrimopredaje [S]
- **Fajl:** `Administracija/frmStavkePrimopredaje.cs` — „Stavke primopredaje“
- **Stari izgled:** Osnovni podaci › Prevozni šifarnici › Stavke primopredaje → **frmStavkePrimopredaje**
- **Tabele:** `SifStavkePrimopredaje` S
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click`
  - „toolStripButton1“ — klik → `tsDelete_Click`

#### frmTehnickiPregled [S]
- **Fajl:** `Dokumenta/frmTehnickiPregled.cs` — „Tehnicki pregled“
- **Stari izgled:** Evidencije › Evidencija aktivnosti › Pregled trenutnih aktivnosti → **frmPregledAktivnosti** → „Tehnicki pregled“ → **frmTehnickiPregled**
- **Tabele:** `SifGrupaNepravilnosti` S, `SifNeispravnostPostupak` S, `SifRazredNepravilnosti` S, `TehnickiPregled` S

#### frmTehnologija [S]
- **Fajl:** `Tehnologija/frmTehnologija.cs` — „Tehnologija“
- **Stari izgled:** Komercijala › Ugovori › Tehnologija → **frmTehnologija**
- **Tabele:** `DelovnaMesta` S, `LokomotivaSerija` S, `MAticniPodatki` S, `Stanice` S, `Tehnologija` SIUD p, `TehnologijaAktivnosti` S, `TehnologijaTrasaLokomotiva` SIUD p, `TehnologijaTrase` SIU p, `Trase` S, `VrstaAktivnosti` S
- **Procedure:** `DeleteTehnologija`, `DeleteTehnologijaTrasa`, `DeleteTehnologijaTrasaLokomotiva`, `DeleteTehnologijaTraseAktivnosti`, `InsertTehnologija`, `InsertTehnologijaTrasaAktivnost`, `InsertTehnologijaTrasaLokomotiva`, `InsertTehnologijaTrase`, `UpdateTehnologija`, `UpdateTehnologijaTrasaLokomotiva`, `UpdateTehnologijaTrase`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertTehnologija, UpdateTehnologija; preko: InsertTehnologija.InsTehnologija, InsertTehnologija.UpdTehnologija
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteTehnologija; preko: InsertTehnologija.DeleteTehnologija
  - „Promeni“ — klik → `sfButton5_Click` — proc: UpdateTehnologijaTrase; preko: InsertTehnologija.UpdTehnologijaTrase
  - „Izbaci“ — klik → `btnIzbaciTrasa_Click` — proc: DeleteTehnologijaTrasa; preko: InsertTehnologija.DelTehnologijaTrase
  - „Unesi“ — klik → `btnUnesiTrasa_Click` — proc: InsertTehnologijaTrase; preko: InsertTehnologija.InsTehnologijaTrase
  - „Promeni“ — klik → `sfButton7_Click`
  - „Izbaci“ — klik → `sfButton1_Click` — proc: DeleteTehnologijaTraseAktivnosti; preko: InsertTehnologija.DelTehnologijaTraseAktivnosti
  - „Unesi“ — klik → `sfButton2_Click` — proc: InsertTehnologijaTrasaAktivnost; preko: InsertTehnologija.InsTehnologijaTraseAktivnosti
  - „Unesi“ — klik → `sfButton3_Click` — proc: InsertTehnologijaTrasaLokomotiva; preko: InsertTehnologija.InsTehnologijaTraseLokomotiva
  - „Izbaci“ — klik → `sfButton4_Click` — proc: DeleteTehnologijaTrasaLokomotiva; preko: InsertTehnologija.DelTehnologijaTraseLokomotiva
  - „Promeni“ — klik → `sfButton6_Click` — proc: UpdateTehnologijaTrasaLokomotiva; preko: InsertTehnologija.UpdTehnologijaTraseLokomotiva
  - `this` — otvaranje forme → `frmTehnologija_Load` — čita 7 tabela

#### frmTehnologijaPregled [S]
- **Fajl:** `Tehnologija/frmTehnologijaPregled.cs` — „Pregled tehnologija“
- **Stari izgled:** Komercijala › Ugovori › Tehnologije pregled → **frmTehnologijaPregled**
- **Otvara:** frmTehnologija („Otvori“)
- **Tabele:** `MAticniPodatki` S, `Tehnologija` S
- **Funkcionalnosti:**
  - „Otvori“ — klik → `toolStripButton1_Click` — otvara: frmTehnologija
  - „Refresh“ — klik → `toolStripButton3_Click` — čita: MaticniPodatki, Tehnologija
  - `this` — otvaranje forme → `frmTehnologijaPregled_Load` — čita: MaticniPodatki, Tehnologija

#### frmTelegrami [S]
- **Fajl:** `Sifarnici/frmTelegrami.cs` — „Telegrami“
- **Stari izgled:** Operatika › Komunikacija › Telegrami → **frmTelegrami**
- **Otvara:** frmTelegramiPrikazi („Prikaži telegrame“)
- **Tabele:** `Pruga` S, `Stanice` S, `telegrami` SIUD p
- **Procedure:** `DeleteTelegrami`, `InsertTelegrami`, `UpdateTelegrami`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertTelegrami, UpdateTelegrami; preko: InsertTelegrami.InsTelegrami, InsertTelegrami.UpdTelegrami
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteTelegrami; preko: InsertTelegrami.DeleteTelegrami
  - „Prikaži telegrame“ — klik → `toolStripButton1_Click` — otvara: frmTelegramiPrikazi
  - „Narednih 7 dana“ — klik → `btn_dani_Click` — čita: Pruga, Stanice, Telegrami
  - „Aktivni“ — klik → `btn_Aktivni_Click` — čita: Pruga, Stanice, Telegrami
  - „Svi telegrami“ — klik → `btn_svi_Click` — čita: Pruga, Stanice, Telegrami
  - `timer1` — Tick → `timer1_Tick` — čita: Pruga, Stanice, Telegrami
  - `timer2` — Tick → `timer2_Tick` — čita: Pruga, Stanice, Telegrami
  - `timer3` — Tick → `timer3_Tick` — čita: Pruga, Stanice, Telegrami
  - „Pronadji PDF“ — klik → `btn_pdf_Click`
  - „Prikaži PDF“ — klik → `btn_prikazi_Click`
  - „Sačuvaj PDF“ — klik → `btn_Sacuvaj_Click` — čita: Telegrami
  - „Narocite posiljke“ — klik → `btn_narocite_Click` — čita: Pruga, Stanice, Telegrami
  - `timer4` — Tick → `timer4_Tick` — čita: Pruga, Stanice, Telegrami

#### frmTelegramiPrikazi [S]
- **Fajl:** `Sifarnici/frmTelegramiPrikazi.cs` — „Prikaz telegrama“
- **Stari izgled:** Operatika › Komunikacija › Telegrami → **frmTelegrami** → „Prikaži telegrame“ → **frmTelegramiPrikazi**
- **Tabele:** `Pruga` S, `Stanice` S, `telegrami` S
- **Funkcionalnosti:**
  - „Narednih 7 dana“ — klik → `btn_dani_Click` — čita: Pruga, Stanice, telegrami
  - „Aktivni“ — klik → `btn_Aktivni_Click` — čita: Pruga, Stanice, telegrami
  - „Svi telegrami“ — klik → `btn_svi_Click` — čita: Pruga, Stanice, telegrami
  - `timer1` — Tick → `timer1_Tick` — čita: Pruga, Stanice, telegrami
  - `timer2` — Tick → `timer2_Tick` — čita: Pruga, Stanice, telegrami
  - `timer3` — Tick → `timer3_Tick` — čita: Pruga, Stanice, telegrami
  - „Narocite posiljke“ — klik → `btn_Narocite_Click` — čita: Pruga, Stanice, telegrami

#### frmTeretnica [S]
- **Fajl:** `Dokumenta/frmTeretnica.cs` — „Teretnica“
- **Stari izgled:** Komercijala › Teretnice › Teretnice → **frmTeretnicePregled** → „Otvori teretnicu“ → **frmTeretnica**
- **Otvara:** frmTeretnicaKopiranjeStavki („Kopiranje stavki“), frmTeretnicaTerenIzmena („Teren“)
- **Tabele:** `Najava` S, `Stanice` S, `Teretnica` SUD p, `TeretnicaStavke` SUD p, `TrainList` S
- **Procedure:** `DeleteTeretnica`, `DeleteTeretnicaStavke`, `InsertTeretnicaStavka`, `InsertTeretnicaStavkeTrainList`, `UpdateTeretnica`, `UpdateTeretnicaOstaleStavke`, `UpdateTeretnicaRBUpDown`, `UpdateTeretnicaStavke`, `UpdateTeretnicaStavkeRB`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteTeretnica; preko: InsertTeretnica.DeleteTeretnica
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertTeretnica, UpdateTeretnica; preko: InsertTeretnica.InsTeretnica, InsertTeretnica.UpdTeretnica
  - „Osveži“ — klik → `tsPrvi_Click`
  - „Štampaj teretnicu“ — klik → `toolStripButton3_Click` — izveštaj: Teretnica.rdlc
  - „Štampaj K-200“ — klik → `toolStripButton1_Click` — izveštaj: K200.rdlc
  - „Štampaj k65“ — klik → `toolStripButton4_Click` — izveštaj: Kol65.rdlc
  - „Kopiranje stavki“ — klik → `toolStripButton2_Click` — otvara: frmTeretnicaKopiranjeStavki
  - „K-200 Bosna“ — klik → `toolStripButton5_Click` — izveštaj: K200Bosna.rdlc
  - „Kol-65Bosna“ — klik → `toolStripButton6_Click` — izveštaj: K65Bosna.rdlc
  - „Teren“ — klik → `toolStripButton7_Click` — otvara: frmTeretnicaTerenIzmena
  - „K-200 - 2“ — klik → `toolStripButton9_Click` — izveštaj: K200TA.rdlc
  - „Kol-65-2“ — klik → `toolStripButton10_Click` — izveštaj: KOl65TA.rdlc
  - „Uvoz teretnice iz train lista“ — klik → `toolStripButton8_Click` — proc: InsertTeretnicaStavkeTrainList; preko: InsertTeretnicaStavke.PrenesiTrainList
  - „Ubaci“ — klik → `button10_Click`
  - „Promeni sve“ — klik → `button9_Click` — proc: UpdateTeretnicaOstaleStavke; preko: InsertTeretnicaStavke.UpdateOstaleStavke
  - „Promeni RB“ — klik → `button4_Click` — proc: UpdateTeretnicaStavkeRB; preko: InsertTeretnicaStavke.UpdateRB
  - „U stanici“ — klik → `label29_Click`
  - „Izbriši“ — klik → `button3_Click` — proc: DeleteTeretnicaStavke; preko: InsertTeretnicaStavke.DeleteTeretnicaStavke
  - „Promeni“ — klik → `button2_Click` — proc: UpdateTeretnicaStavke; preko: InsertTeretnicaStavke.UpdTeretnicaStavke
  - „Ubaci“ — klik → `button1_Click` — proc: InsertTeretnicaStavka; preko: InsertTeretnicaStavke.InsTeretnicaStavke
  - „Potvrda prijema / raspuštanje“ — klik → `button5_Click` — proc: InsertTerIskljVagPrijem; preko: InsertIskljuceniVagoni.InsertIskljuceniVagPrijem
  - „>“ — klik → `button7_Click` — proc: UpdateTeretnicaRBUpDown; preko: InsertTeretnicaStavke.UpdateTeretnicaRBUpDown
  - „<“ — klik → `button8_Click` — proc: UpdateTeretnicaRBUpDown; preko: InsertTeretnicaStavke.UpdateTeretnicaRBUpDown
  - „K-200 - 3“ — klik → `toolStripButton11_Click` — izveštaj: K200TA3.rdlc
  - „K-200 - 4“ — klik → `toolStripButton12_Click` — izveštaj: K200TA4.rdlc
  - `this` — otvaranje forme → `frmTeretnica_Load` — čita: Stanice, TrainList

#### frmTeretnicaKopiranjeStavki [S]
- **Fajl:** `Dokumenta/frmTeretnicaKopiranjeStavki.cs` — „Teretnica iz“
- **Stari izgled:** Komercijala › Teretnice › Teretnice → **frmTeretnicePregled** → „Otvori teretnicu“ → **frmTeretnica** → „Kopiranje stavki“ → **frmTeretnicaKopiranjeStavki**
- **Tabele:** `Stanice` S, `Teretnica` S, `TeretnicaIskljuceniVagoni` S, `TeretnicaStavke` S
- **Procedure:** `DeleteTerIskljuceniVagoni`, `InsertTeretnicaStavkaKopiraj`
- **Funkcionalnosti:**
  - „>>>“ — klik → `button1_Click` — proc: InsertTeretnicaStavkaKopiraj; preko: InsertTeretnicaStavke.InsTeretnicaStavkeKopiranje
  - „>>>“ — klik → `button2_Click` — proc: InsertTerIskljuceniVagoni; preko: InsertIskljuceniVagoni.InsertIskljuceniVag
  - „Stanica isključenja:“ — klik → `label2_Click`
  - „/\\ /\\ /\\“ — klik → `button3_Click` — proc: DeleteTerIskljuceniVagoni, InsertTeretnicaStavkaKopiraj; preko: InsertIskljuceniVagoni.DeleteIskljuceniVagoni, InsertTeretnicaStavke.InsTeretnicaStavkeKopiranje
  - `this` — otvaranje forme → `frmTeretnicaKopiranjeStavki_Load` — čita: Stanice, Teretnica

#### frmTeretnicaTerenIzmena [S]
- **Fajl:** `Dokumenta/frmTeretnicaTerenIzmena.cs` — „TeretnicaTerenIzmena“
- **Stari izgled:** Komercijala › Teretnice › Teretnice → **frmTeretnicePregled** → „Otvori teretnicu“ → **frmTeretnica** → „Teren“ → **frmTeretnicaTerenIzmena**
- **Tabele:** `AktivnostiStavke` S, `KomercijalniPregled` S, `PopisTeretnicaStavke` S, `SifOpisNeispravnosti` S, `SifVrstaNepravilnosti` S, `Stanice` S, `TeretnicaKomercijalniPregled` S, `TeretnicaStavke` SU p
- **Procedure:** `ProveraTeretnicaKomercijalniPregled`, `UpdateTeretnicaStavke`, `UpdateTeretnicaStavkeKomercijalniPregled`
- **Funkcionalnosti:**
  - „Uporedi“ — klik → `btn_Provera_Click`
  - „Potvrdi Izmene“ — klik → `btn_Izmena_Click` — čita: Stanice; proc: UpdateTeretnicaStavke; preko: InsertTeretnicaStavke.UpdTeretnicaStavke
  - „Potvrdi Izmene sa Komercijalnog pregleda“ — klik → `button1_Click` — proc: UpdateTeretnicaStavkeKomercijalniPregled; preko: InsertTeretnicaStavke.UpdTeretnicaStavkeKomercijalniPregled
  - „Proveri izmene“ — klik → `button2_Click` — proc: ProveraTeretnicaKomercijalniPregled; preko: InsertTeretnicaStavke.ProveraTeretniceKomercijalniPregled
  - „Refresh“ — klik → `tsNew_Click`

#### frmTeretnicePregled [S]
- **Fajl:** `Dokumenta/frmTeretnicePregled.cs` — „Pregled teretnica“
- **Stari izgled:** Komercijala › Teretnice › Teretnice → **frmTeretnicePregled**
- **Otvara:** frmTeretnica („Otvori teretnicu“)
- **Tabele:** `Stanice` S, `Teretnica` S, `TeretnicaStavke` S, `TrainList` S
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „Otvori teretnicu“ — klik → `toolStripButton1_Click` — otvara: frmTeretnica
  - „Refresh“ — klik → `toolStripButton2_Click` — čita: Teretnica, TeretnicaStavke, TrainList, stanice
  - „Nova Teretnica“ — klik → `toolStripButton3_Click` — otvara: frmTeretnica
  - `this` — otvaranje forme → `frmTeretnicePregled_Load` — čita: Teretnica, TeretnicaStavke, TrainList, stanice

#### frmTerminalOperacije [S]
- **Fajl:** `Tehnologija/frmTerminalOperacije.cs` — „Terminalske operacije“
- **Stari izgled:** Terminal › Šifarnici › Terminalske operacije → **frmTerminalOperacije**
- **Tabele:** `TerminalOperacije` SIUD p
- **Procedure:** `DeleteTerminalOperacije`, `InsertTerminalOperacije`, `UpdateTerminalOperacije`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertTerminalOperacije, UpdateTerminalOperacije; preko: InsertTerminalOperacije.InsertTerminalOperacija, InsertTerminalOperacije.UpdTerminalOperacija
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteTerminalOperacije; preko: InsertTerminalOperacije.DelTerminalOperacija
  - `button20` — klik → `tsDelete_Click` — proc: DeleteTerminalOperacije; preko: InsertTerminalOperacije.DelTerminalOperacija
  - `button21` — klik → `tsSave_Click` — proc: InsertTerminalOperacije, UpdateTerminalOperacije; preko: InsertTerminalOperacije.InsertTerminalOperacija, InsertTerminalOperacije.UpdTerminalOperacija

#### frmTerminalPrivremeni [N]
- **Fajl:** `RNI/frmTerminalPrivremeni.cs` — „Terminal privremeni“
- **Novi izgled:** Intermodalni terminal → **Intermodalni1** → „Terminal temp“ → **frmTerminalPrivremeni**
- **Otvara:** frmTerminalPrivremeniExcel („Uvoz Excel“), frmTerminalPrivremeniPromeni („Grupna promena“), frmTerminalPrivremeniSlike („Zakači slike“), frmTerminalPrivremeniOpstiIzvoz („Opšti izvoz“), frmTerminalPrivremeniArhiva („Arhivirani podaci“), frmTerminalPrivremeniLog („Pregled log“), frmTerminalPrivremeniDokumenta („Zakači dokumenta“)
- **Tabele:** `TerminalPriv` IUD p
- **Procedure:** `delTerminalPriv`, `InsertTerminalPrivArhiv`, `insTerminalPriv`, `updTerminalPriv`
- **Funkcionalnosti:**
  - „gridGroupingControl1“ — TableControlCurrentCellChanged → `gridGroupingControl1_TableControlCurrentCellChanged`
  - „gridGroupingControl1“ — SelectedRecordsChanged → `gridGroupingControl1_SelectedRecordsChanged`
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - „Štampaj potvrdu“ — klik → `btnStampajPotvrdu_Click`
  - „Novi“ — klik → `btnNovi_Click`
  - „Potvrdi promenu“ — klik → `btnPromeni_Click` — proc: updTerminalPriv; preko: insertTerminalPriv.UpdTerminalPriv
  - „Obriši“ — klik → `btnObrisi_Click` — proc: delTerminalPriv; preko: insertTerminalPriv.DelTerminalPriv
  - „Arhiviraj“ — klik → `btnArhiviraj_Click` — proc: InsertTerminalPrivArhiv; preko: insertTerminalPriv.InsTerminalPrivArhiv
  - „Opšti izvoz“ — klik → `btnOpstiIzvoz_Click` — otvara: frmTerminalPrivremeniOpstiIzvoz; izvoz u Excel
  - „Poništi filtere“ — klik → `btnPonistiFiltere_Click`
  - „Arhivirani podaci“ — klik → `btnArhiviraniPodaci_Click` — otvara: frmTerminalPrivremeniArhiva
  - „Pregled log“ — klik → `btnPregledLoga_Click` — otvara: frmTerminalPrivremeniLog
  - „Zakači dokumenta“ — klik → `btnZakaciDokumenta_Click` — otvara: frmTerminalPrivremeniDokumenta
  - „Zakači slike“ — klik → `btnZakaciSlike_Click` — otvara: frmTerminalPrivremeniSlike
  - „Sačuvaj izmene“ — klik → `btnSacuvajIzmene_Click` — proc: delTerminalPriv, insTerminalPriv, updTerminalPriv; preko: insertTerminalPriv.DelTerminalPriv, insertTerminalPriv.InsTerminalPriv, insertTerminalPriv.UpdTerminalPriv
  - „Grupna promena“ — klik → `btnGrupnaPromena_Click` — otvara: frmTerminalPrivremeniPromeni
  - „Uvoz Excel“ — klik → `btnUvozExcel_Click` — otvara: frmTerminalPrivremeniExcel; izvoz u Excel
  - „gridGroupingControl1“ — TableControlCurrentCellStartEditing → `gridGroupingControl1_TableControlCurrentCellStartEditing`

#### frmTerminalPrivremeniArhiva [N]
- **Fajl:** `RNI/frmTerminalPrivremeniArhiva.cs` — „Arhivirani podaci“
- **Novi izgled:** Intermodalni terminal → **Intermodalni1** → „Terminal temp“ → **frmTerminalPrivremeni** → „Arhivirani podaci“ → **frmTerminalPrivremeniArhiva**
- **Otvara:** frmTerminalPrivremeniSlike („Zakači slike“), frmTerminalPrivremeniDokumenta („Zakači dokumenta“), frmTerminalPrivremeniLog („Pregled log“)
- **Funkcionalnosti:**
  - „Pregled log“ — klik → `btnPregledLog_Click` — otvara: frmTerminalPrivremeniLog
  - „Zakači dokumenta“ — klik → `btnZakaciDokumenta_Click` — otvara: frmTerminalPrivremeniDokumenta
  - „Zakači slike“ — klik → `btnZakaciSlike_Click` — otvara: frmTerminalPrivremeniSlike

#### frmTerminalPrivremeniDokumenta [N]
- **Fajl:** `RNI/frmTerminalPrivremeniDokumenta.cs` — „Terminal privremeni - dokumenta“
- **Novi izgled:** Intermodalni terminal → **Intermodalni1** → „Terminal temp“ → **frmTerminalPrivremeni** → „Zakači dokumenta“ → **frmTerminalPrivremeniDokumenta**
- **Funkcionalnosti:**
  - „Otvori folder“ — klik → `btnOtvoriFolder_Click`
  - „Otvori izabrani dokument“ — klik → `btnOtvoriDokument_Click`
  - „Osveži“ — klik → `btnOsvezi_Click`
  - „Dodaj dokumenta...“ — klik → `btnDodajDokumenta_Click`
  - „Prevucite dokumenta ovde (drag && drop)“ — DragDrop → `Dokumenta_DragDrop`
  - „Prevucite dokumenta ovde (drag && drop)“ — DragEnter → `Dokumenta_DragEnter`

#### frmTerminalPrivremeniExcel [N]
- **Fajl:** `RNI/frmTerminalPrivremeniExcel.cs` — „Terminal privremeni - uvoz Excel“
- **Novi izgled:** Intermodalni terminal → **Intermodalni1** → „Terminal temp“ → **frmTerminalPrivremeni** → „Uvoz Excel“ → **frmTerminalPrivremeniExcel**
- **Procedure:** `insTerminalPrivFromExcel`
- **Funkcionalnosti:**
  - „Učitaj“ — klik → `btnUcitaj_Click` — proc: insTerminalPrivFromExcel; preko: insertTerminalPriv.InsTerminalPrivFromExcel; izvoz u Excel
  - „Otvori fajl“ — klik → `btnOtvoriFajl_Click` — izvoz u Excel

#### frmTerminalPrivremeniLog [N]
- **Fajl:** `RNI/frmTerminalPrivremeniLog.cs` — „Terminal privremeni - log“
- **Novi izgled:** Intermodalni terminal → **Intermodalni1** → „Terminal temp“ → **frmTerminalPrivremeni** → „Pregled log“ → **frmTerminalPrivremeniLog**
- **Funkcionalnosti:**
  - „Prikaži po datumu“ — klik → `btnPoDatumu_Click`
  - „Prikaži zadnjih 1000“ — klik → `btnZadnjih1000_Click`
  - „Prikaži za izabrani zapis“ — klik → `btnZaZapis_Click`

#### frmTerminalPrivremeniOpstiIzvoz [N]
- **Fajl:** `RNI/frmTerminalPrivremeniOpstiIzvoz.cs` — „Opšti izvoz“
- **Novi izgled:** Intermodalni terminal → **Intermodalni1** → „Terminal temp“ → **frmTerminalPrivremeni** → „Opšti izvoz“ → **frmTerminalPrivremeniOpstiIzvoz**
- **Funkcionalnosti:**
  - „Označi sve“ — klik → `btnOznaciSve_Click`
  - „Poništi sve“ — klik → `btnPonistiSve_Click`
  - „Izvezi u Excel“ — klik → `btnIzvezi_Click`

#### frmTerminalPrivremeniPromeni [N]
- **Fajl:** `RNI/frmTerminalPrivremeniPromeni.cs` — „Grupna promena“
- **Novi izgled:** Intermodalni terminal → **Intermodalni1** → „Terminal temp“ → **frmTerminalPrivremeni** → „Grupna promena“ → **frmTerminalPrivremeniPromeni**
- **Funkcionalnosti:**
  - „Primeni“ — klik → `btnPrimeni_Click`

#### frmTerminalPrivremeniSlike [N]
- **Fajl:** `RNI/frmTerminalPrivremeniSlike.cs` — „Terminal privremeni - slike“
- **Novi izgled:** Intermodalni terminal → **Intermodalni1** → „Terminal temp“ → **frmTerminalPrivremeni** → „Zakači slike“ → **frmTerminalPrivremeniSlike**
- **Funkcionalnosti:**
  - „Otvori folder“ — klik → `btnOtvoriFolder_Click`
  - „Osveži“ — klik → `btnOsvezi_Click`
  - „Dodaj slike...“ — klik → `btnDodajSlike_Click`
  - „Prevucite slike ovde (drag && drop)“ — DragDrop → `Slike_DragDrop`
  - „Prevucite slike ovde (drag && drop)“ — DragEnter → `Slike_DragEnter`

#### frmTerminalProces [S]
- **Fajl:** `Sifarnici/frmTerminalProces.cs` — „Terminal proces“
- **Stari izgled:** Terminal › Šifarnici › Scenario → **frmScenario** → „Terminal proces“ → **frmTerminalProces**
- **Tabele:** `Scenario` S, `TerminalOperacije` S, `TerminalProces` SIUD p
- **Procedure:** `DeleteTerminalProces`, `InsertTerminalProces`, `UpdateTerminalProces`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertTerminalProces, UpdateTerminalProces; preko: InserTerminalProces.InsTerminalProces, InserTerminalProces.UpdTerminalProces
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteTerminalProces; preko: InserTerminalProces.DelTerminalProces
  - „?“ — klik → `btnRacun_Click`
  - `button20` — klik → `tsDelete_Click` — proc: DeleteTerminalProces; preko: InserTerminalProces.DelTerminalProces
  - `button21` — klik → `tsSave_Click` — proc: InsertTerminalProces, UpdateTerminalProces; preko: InserTerminalProces.InsTerminalProces, InserTerminalProces.UpdTerminalProces
  - `this` — otvaranje forme → `frmTerminalProces_Load` — čita: Scenario, TerminalOperacije

#### frmTipCenovnika [S]
- **Fajl:** `TrackModal/Sifarnici/frmTipCenovnika.cs` — „Tip Cenovnika“
- **Stari izgled:** Terminal › Šifarnici › Tip cenovnika → **frmTipCenovnika**
- **Tabele:** `TipCenovnika` SIUD p
- **Procedure:** `DeleteTipCenovnika`, `InsertTipCenovnika`, `UpdateTipCenovnika`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertTipCenovnika, UpdateTipCenovnika; preko: InsertTipCenovnika.InsTipCenovnika, InsertTipCenovnika.UpdTipCenovnika
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteTipCenovnika; preko: InsertTipCenovnika.DeleteTipCenovnikae
  - `button20` — klik → `tsDelete_Click` — proc: DeleteTipCenovnika; preko: InsertTipCenovnika.DeleteTipCenovnikae
  - `button21` — klik → `tsSave_Click` — proc: InsertTipCenovnika, UpdateTipCenovnika; preko: InsertTipCenovnika.InsTipCenovnika, InsertTipCenovnika.UpdTipCenovnika

#### frmTipKomercijalnogDokumenta [S]
- **Fajl:** `Sifarnici/frmTipKomercijalnogDokumenta.cs` — „Tip komercijalnog dokumenta“
- **Stari izgled:** Izvoz › Šifarnici › Tip komercijalnog dokumenta → **frmTipKomercijalnogDokumenta**
- **Tabele:** `TipKomercijalnogDokumenta` SD p
- **Procedure:** `DeleteTipKomercijalnogDokumenta`, `InsertTipKomercijanogDokumenta`, `UpdateKomercijlnogDokumenta`
- **Funkcionalnosti:**
  - `button20` — klik → `tsDelete_Click` — proc: DeleteTipKomercijalnogDokumenta; preko: InsertTipKomercijalnogDokumenta.DelTipKomercijalogDokumenta
  - `button21` — klik → `tsSave_Click` — proc: InsertTipKomercijanogDokumenta, UpdateKomercijlnogDokumenta; preko: InsertTipKomercijalnogDokumenta.InsTipKomercijalnogDokumenta, InsertTipKomercijalnogDokumenta.UpdTipKomercijalnogDokumenta

#### frmTipKontejnera [N+S]
- **Fajl:** `TrackModal/Sifarnici/frmTipKontejnera.cs` — „Tip kontejnera“
- **Novi izgled:** Podešavanja sistema → **Podesavanje1** → „Tipovi kontejnera“ → **frmTipKontejnera**
- **Stari izgled:** Terminal › Šifarnici › Tip kontejnera → **frmTipKontejnera**
- **Tabele:** `TipKontenjera` SIUD p
- **Procedure:** `DeleteTipKontenjera`, `InsertTipKontenjera`, `UpdateTipKontenjera`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertTipKontenjera, UpdateTipKontenjera; preko: InsertTipKontejnera.InsTipKontejnera, InsertTipKontejnera.UpdTipKontejnera
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteTipKontenjera; preko: InsertTipKontejnera.DeleteTipKontejnera
  - „toolStripButton1“ — klik → `tsPrvi_Click` — čita: TipKontenjera
  - „toolStripButton1“ — klik → `tsNazad_Click` — čita: TipKontenjera
  - „toolStripButton1“ — klik → `tsNapred_Click` — čita: TipKontenjera
  - „toolStripButton1“ — klik → `tsPoslednja_Click` — čita: TipKontenjera
  - „Dužina:“ — klik → `label11_Click`
  - `button20` — klik → `tsDelete_Click` — proc: DeleteTipKontenjera; preko: InsertTipKontejnera.DeleteTipKontejnera
  - `button21` — klik → `tsSave_Click` — proc: InsertTipKontenjera, UpdateTipKontenjera; preko: InsertTipKontejnera.InsTipKontejnera, InsertTipKontejnera.UpdTipKontejnera

#### frmTipPalete [N+S]
- **Fajl:** `TrackModal/Sifarnici/frmTipPalete.cs` — „Tip palete“
- **Novi izgled:** Operacije skladišta → **SkladistaMain** → kartica „Radni nalozi“ → **NalogZaKomercijalni** → „SKLADIŠNI NALOZI UVOZ“ → **PregledKomercijalnihNaloga** → „Otvori“ → **frmNalogPrijem** → „?“ → **frmTipPalete**
- **Stari izgled:** Terminal › Šifarnici › Tip palete → **frmTipPalete**
- **Tabele:** `TipPalete` SIUD p
- **Procedure:** `DeleteTipPalete`, `InsertTipPalete`, `UpdateTipPalete`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertTipPalete, UpdateTipPalete; preko: InsertTipPalete.InsTipPalete, InsertTipPalete.UpdTipPalete
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteTipPalete; preko: InsertTipPalete.DeleteTipPalete
  - `button20` — klik → `tsDelete_Click` — proc: DeleteTipPalete; preko: InsertTipPalete.DeleteTipPalete
  - `button21` — klik → `tsSave_Click` — proc: InsertTipPalete, UpdateTipPalete; preko: InsertTipPalete.InsTipPalete, InsertTipPalete.UpdTipPalete

#### frmTipPrevoza [S]
- **Fajl:** `Sifarnici/frmTipPrevoza.cs` — „Tipovi prevoza“
- **Stari izgled:** Osnovni podaci › Prevozi › Tip prevoza → **frmTipPrevoza**
- **Tabele:** `TipSaobPrevoza` SIU p
- **Procedure:** `InsertTipSaobPrevoza`, `UpdateTipSaobPrevoza`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertTipSaobPrevoza, UpdateTipSaobPrevoza; preko: InsertTipPrevoza.InsTipPrevoza, InsertTipPrevoza.UpdTipPrevoza
  - `this` — otvaranje forme → `frmTipPrevoza_Load` — čita: TipSAobPrevoza

#### frmTipSkladista [S]
- **Fajl:** `TrackModal/Sifarnici/frmTipSkladista.cs` — „Tip skladista“
- **Stari izgled:** Terminal › Šifarnici › Tip skladišta → **frmTipSkladista**
- **Tabele:** `TipSkladista` SIUD p
- **Procedure:** `DeleteTipSkladista`, `InsertTipSkladista`, `UpdateTipSkladista`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertTipSkladista, UpdateTipSkladista; preko: InsertTipSkladista.InsTipSkladista, InsertTipSkladista.UpdTipSkladista
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteTipSkladista; preko: InsertTipSkladista.DeleteTipSkladista
  - `button20` — klik → `tsDelete_Click` — proc: DeleteTipSkladista; preko: InsertTipSkladista.DeleteTipSkladista
  - `button21` — klik → `tsSave_Click` — proc: InsertTipSkladista, UpdateTipSkladista; preko: InsertTipSkladista.InsTipSkladista, InsertTipSkladista.UpdTipSkladista

#### frmTipTelegrama [S]
- **Fajl:** `Sifarnici/frmTipTelegrama.cs` — „Tip telegrama“
- **Stari izgled:** Osnovni podaci › Prevozi › Tipovi telegrama → **frmTipTelegrama**
- **Tabele:** `TipTelegrama` SIUD p
- **Procedure:** `DeleteTipTelegrama`, `InsertTipTelegrama`, `UpdateTipTelegrama`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertTipTelegrama, UpdateTipTelegrama; preko: InsertTipTelegrama.InsTipTelegrama, InsertTipTelegrama.UpdTipTelegrama
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteTipTelegrama; preko: InsertTipTelegrama.DeleteTipTelegrama
  - `this` — otvaranje forme → `frmTipTelegrama_Load` — čita: TipTelegrama

#### frmTipZone [S]
- **Fajl:** `TrackModal/Sifarnici/frmTipZone.cs` — „Tip zone“
- **Stari izgled:** Terminal › Šifarnici › Tip zone → **frmTipZone**
- **Tabele:** `TipZone` SIUD p
- **Procedure:** `DeleteTipZone`, `InsertTipZone`, `UpdateTipZone`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertTipZone, UpdateTipZone; preko: InsertTipZone.InsTipZone, InsertTipZone.UpdTipZone
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteTipZone; preko: InsertTipZone.DeleteTipZone
  - `button20` — klik → `tsDelete_Click` — proc: DeleteTipZone; preko: InsertTipZone.DeleteTipZone
  - `button21` — klik → `tsSave_Click` — proc: InsertTipZone, UpdateTipZone; preko: InsertTipZone.InsTipZone, InsertTipZone.UpdTipZone

#### frmTokoviDokumentacije [S]
- **Fajl:** `Mobile/frmTokoviDokumentacije.cs` — „Tokovi dokumentacije“
- **Stari izgled:** Evidencije › Prijave i odjave › Tokovi dokumentacije → **frmTokoviDokumentacije**
- **Otvara:** frmTokoviDokumentacijeSlika („Otvori slike“)
- **Tabele:** `Delavci` S, `EvidencijaTokoviDokumentacije` S
- **Funkcionalnosti:**
  - „Ažuriraj“ — klik → `metroButton1_Click`
  - „Otvori slike“ — klik → `metroButton2_Click` — otvara: frmTokoviDokumentacijeSlika

#### frmTokoviDokumentacijeSlika [S]
- **Fajl:** `Mobile/frmTokoviDokumentacijeSlika.cs` — „Tokovi dokumentacije slike“
- **Stari izgled:** Evidencije › Prijave i odjave › Tokovi dokumentacije → **frmTokoviDokumentacije** → „Otvori slike“ → **frmTokoviDokumentacijeSlika**
- **Tabele:** `EvidencijaTokoviDokumentacije` S
- **Funkcionalnosti:**
  - „Otvori“ — klik → `button2_Click`
  - „Pronađi“ — klik → `button1_Click`

#### frmTovarniList [N+S]
- **Fajl:** `Testiranje/frmTovarniList.cs` — „Tovarni list“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Formiraj otpremu kamionom“ → **frmOtpremaKontejneraKamionomIzKontejnera** → „Otvori“ → **frmOtpremaKontejnera** → „Tovarni list“ → **frmTovarniList**
- **Stari izgled:** Terminal › Prevoz › Pregled tovarni listovi → **frmPregledTovarnihListova** → „Otvori“ → **frmTovarniList**
- **Tabele:** `Partnerji` S, `TovarniList` S
- **Combobox → tabela:** `cboPosiljalac` ← `Partnerji`; `cboPrimalac` ← `Partnerji`; `cboPosiljalac` ← `Partnerji`; `cboPrimalac` ← `Partnerji`
- **Funkcionalnosti:**
  - „Štampa tovarnog lista“ — ItemClicked → `toolStrip1_ItemClicked`
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click`
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „Štampa“ — klik → `toolStripButton1_Click` — izveštaj: rptTovarniList.rdlc
  - „1“ — klik → `button3_Click`
  - „2“ — klik → `button1_Click`
  - „3“ — klik → `button2_Click`
  - „4“ — klik → `button4_Click`
  - „5“ — klik → `button5_Click`
  - „6“ — klik → `button6_Click`
  - „7“ — klik → `button7_Click`
  - „8“ — klik → `button8_Click`
  - „10“ — klik → `button10_Click`
  - „11“ — klik → `button11_Click`
  - „13“ — klik → `button13_Click`
  - „Podaci“ — klik → `tabPage1_Click`

#### frmTrainList [S]
- **Fajl:** `Dokumenta/frmTrainList.cs` — „Train List“
- **Stari izgled:** Komercijala › Najave › Train list → **frmTrainList**
- **Otvara:** frmTeretnica („Nova teretnica“)
- **Tabele:** `ATrainListStavke` S, `Teretnica` S, `this` D, `TipKontenjera` S, `TrainList` S, `TrainListStavke` S, `VagoniSerije` S
- **Procedure:** `DeleteTrainListStavka`, `DeleteTrainListSveStavke`, `MRN_GetAllBySuperiorId`, `NapuniTrainList`, `NapuniTrainList2`, `NapuniTrainList2Ter`, `NapuniTrainList3`, `NHM_GetAllBySuperiorId`, `Plombe_GetAllBySuperiorId`, `TrainList_Delete`, `TrainList_GetAll`, `TrainList_GetAllActive`, `TrainList_Insert`, `TrainList_Search`, `TrainList_Update`, `TrainListStavke_GetAllBySuperiorId`, `UN_GetAllBySuperiorId`, `UpdateNajavaIzTrainLista`
- **Funkcionalnosti:**
  - „Uvezi Train List“ — klik → `btn_ImportExcel_Click` — izvoz u Excel
  - „Add Item“ — klik → `btnAddItem_Click`
  - „Edit“ — klik → `btnEdit_Click`
  - „Delete“ — klik → `btnDelete_Click` — piše: this[D]
  - „Search“ — klik → `btnSearch_Click`
  - „Cancel“ — klik → `btnCancel_Click`
  - „Add“ — klik → `btnAddUpdate_Click`
  - „Brisanje stavki“ — klik → `button1_Click` — proc: DeleteTrainListSveStavke; preko: InsertTrainList.DelSveStavkeTrainLista
  - `tabControl1` — izbor → `tabControl1_SelectedIndexChanged` — proc: NapuniTrainList, NapuniTrainList2Ter; preko: InsertTrainList.ProveriTrainListTrainLista2Ter, InsertTrainList.ProveriTrainListTrainListaTer
  - „PredhodniPodaci“ — klik → `button2_Click` — proc: NapuniTrainList, NapuniTrainList2; preko: InsertTrainList.ProveriTrainListTrainLista, InsertTrainList.ProveriTrainListTrainLista2
  - „Primeni stare vrednosti“ — klik → `button3_Click` — proc: NapuniTrainList3; preko: InsertTrainList.PromeniVrednostiZaSvakaKola
  - „Poslednji podaci sa Teretnica“ — klik → `button4_Click` — proc: NapuniTrainList, NapuniTrainList2Ter; preko: InsertTrainList.ProveriTrainListTrainLista2Ter, InsertTrainList.ProveriTrainListTrainListaTer
  - „ Vrati ukupne vrednosti na Posao“ — klik → `button5_Click`
  - „Nova teretnica“ — klik → `toolStripButton1_Click` — otvara: frmTeretnica; čita: Teretnica
  - „Test“ — klik → `button6_Click`
  - „Brisanje selektovanog“ — klik → `button7_Click` — proc: DeleteTrainListStavka; preko: InsertTrainList.DelStavkuTrainLista

#### frmTrasaStanice [S]
- **Fajl:** `Sifarnici/frmTrasaStanice.cs` — „Trasa ruta“
- **Stari izgled:** Osnovni podaci › Prevozni šifarnici › Trase → **frmTrase** → „Trasa ruta“ → **frmTrasaStanice**
- **Tabele:** `Pruga` S, `Stanice` S, `TrasaStanice` SIUD p, `Trase` S
- **Procedure:** `DeleteTrasaStanice`, `InsertTrasaStanice`, `UpdateTrasaStanice`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertTrasaStanice, UpdateTrasaStanice; preko: InsertTrasaStanice.InsTStan, InsertTrasaStanice.UpdTStan
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteTrasaStanice; preko: InsertTrasaStanice.DelTStan
  - `this` — otvaranje forme → `frmTrasaStanice_Load` — čita: Pruga, Stanice

#### frmTrase [S]
- **Fajl:** `Sifarnici/frmTrase.cs` — „Trase“
- **Stari izgled:** Osnovni podaci › Prevozni šifarnici › Trase → **frmTrase**
- **Otvara:** frmTrasaStanice („Trasa ruta“), frmRutaTrase („Spisak stanica na trasi“)
- **Tabele:** `Partnerji` S, `Stanice` S, `Trase` SIUD p
- **Procedure:** `DeleteTrase`, `InsertTrase`, `UpdateTrase`
- **Funkcionalnosti:**
  - „Težina voza:“ — klik → `label8_Click`
  - „Spisak stanica na trasi“ — ItemClicked → `toolStrip1_ItemClicked`
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertTrase, UpdateTrase; preko: InsertTrase.InsTras, InsertTrase.UpdTrase
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteTrase; preko: InsertTrase.DeleteTrase
  - „Trasa ruta“ — klik → `toolStripButton1_Click` — otvara: frmTrasaStanice
  - „Spisak stanica na trasi“ — klik → `toolStripButton2_Click`
  - „Dizel“ — čekiranje → `chkDE_CheckedChanged`
  - „Sačuvaj“ — klik → `btnRacunSacuvaj_Click`
  - „Pronađi“ — klik → `btnRacun_Click`
  - „Spisak stanica na trasi“ — klik → `toolStripButton2_Click`
  - „Trasa ruta“ — klik → `toolStripButton1_Click` — otvara: frmTrasaStanice
  - `button20` — klik → `tsDelete_Click` — proc: DeleteTrase; preko: InsertTrase.DeleteTrase
  - `button21` — klik → `tsSave_Click` — proc: InsertTrase, UpdateTrase; preko: InsertTrase.InsTras, InsertTrase.UpdTrase
  - `this` — otvaranje forme → `frmTrase_Load` — čita: Partnerji, Stanice

#### frmTraseAnaliticki [S]
- **Fajl:** `Sifarnici/frmTraseAnaliticki.cs` — „Analize trasa“
- **Stari izgled:** Osnovni podaci › Prevozni šifarnici › Trase analitički → **frmTraseAnaliticki**
- **Tabele:** `Stanice` S, `Trase` S
- **Funkcionalnosti:**
  - `this` — otvaranje forme → `frmTraseAnaliticki_Load` — čita: Trase, stanice

#### frmUnosManipulacija [N+S]
- **Fajl:** `Uvoz/frmUnosManipulacija.cs` — „UNOS USLUGA - UVOZ“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Voz“ → **frmPrijemIOtpremaVozova4** → „Vozovi“ → **frmVoz** → „UVOZ - FORMIRANJE PLANA“ → **frmFormiranjePlana** → „Otvori plan“ → **frmUvozKonacna** → prečica na tastaturi (`frmUvozKonacna_KeyDown`) → **frmUnosManipulacija**
- **Stari izgled:** Uvoz › Uvoz obrada › Neraspoređeni kontejneri - Obrazac → **Uvoz** → prečica na tastaturi (`Uvoz_KeyDown`) → **frmUnosManipulacija**
- **Otvara:** frmVoziloUsluga („Kamion za uslugu“), frmUvozKopirajUslugeKontejnera („Kopiraj usluge“)
- **Tabele:** `Brodovi` S, `Carinarnice` S, `Cene` S, `DirigacijaKontejneraZa` S, `GrupaVrsteManipulacije` S, `KontejnerskiTerminali` S, `KontejnerStatus` S, `MestaUtovara` S, `NHM` S, `organizacioneJedinice` S, `Partnerji` S, `Predefinisaneporuke` S, `RadniNalogInterni` S, `Scenario` S, `TipKontenjera` S, `uvNacinPakovanja` S, `Uvoz` S, `UvozKonacna` S, `UvozKonacnaVrstaManipulacije` SID p, `UvozVrstaManipulacije` SIUD p, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaRobeADR` S, `VrstaRobeHS` S, `VrstePostupakaUvoz` S
- **Procedure:** `DeleteUvozKonacnaVrstaManipulacije`, `DeleteUvozVrstaManipulacije`, `InsertUvozKonacnaVrstaManipulacije`, `InsertUvozVrstaManipulacije`, `spIzbrisiKUScenario`, `spStornijaRNI`, `UpdateScenarioKontejnera`, `UpdCenaUvozKonacnaVrstaManipulacije`, `UpdPlatiocaUvozKonacnaVrstaManipulacije`, `UpdPlatiocaUvozVrstaManipulacije`, `UpdPokretFormaStatus`
- **Combobox → tabela:** `cboNalogodavac1` ← `Partnerji`; `cboNalogodavac2` ← `Partnerji`; `cboNalogodavac3` ← `Partnerji`; `cboUvoznik` ← `Partnerji`; `cboScenario` ← `Scenario`; `cboScenario` ← `Scenario`; `cboScenario` ← `Scenario`; `cboScenario` ← `Scenario`; `cboScenario` ← `Scenario`; `cboScenario` ← `Scenario`; `cboScenario` ← `Scenario`; `cboScenario` ← `Scenario`; `cboScenario` ← `Scenario`; `cboScenario` ← `Scenario`; `cboGrupaVrsteManipulacije` ← `GrupaVrsteManipulacije`; `cboStatus` ← `KontejnerStatus`
- **Funkcionalnosti:**
  - „OPŠTI“ — klik → `button11_Click`
  - „N1“ — klik → `button13_Click`
  - „N1>>>>“ — klik → `button12_Click` — čita: Cene
  - `dataGridView7` — klik na ćeliju → `dataGridView7_CellClick` — proc: UpdPDVUvozKonacnaVrstaManipulacije, UpdPDVUvozVrstaManipulacije; preko: InsertUvoz.UpdPDVUvozKonacnaUsluga, InsertUvoz.UpdPDVUvozUsluga
  - `dataGridView7` — CellLeave → `dataGridView7_CellLeave` — proc: UpdCenaUvozKonacnaVrstaManipulacije, UpdCenaUvozVrstaManipulacije; preko: InsertUvoz.UpdCENAUvozKonacnaUsluga, InsertUvoz.UpdCENAUvozUsluga
  - „N2“ — klik → `button1_Click`
  - „N3“ — klik → `button2_Click`
  - „N1-BU“ — klik → `button3_Click`
  - „N2-BU“ — klik → `button4_Click`
  - „N3-BU“ — klik → `button5_Click`
  - „N2>>>>“ — klik → `button6_Click` — čita: Cene
  - „N3>>>>“ — klik → `button7_Click` — čita: Cene
  - „Obrisi selektovane“ — klik → `button8_Click` — proc: DeleteUvozKonacnaVrstaManipulacije, DeleteUvozVrstaManipulacije; preko: InsertUvozKonacna.DelUvozKonacnaUsluga, InsertUvozKonacna.DelUvozUsluga
  - „Promeni  platioca“ — klik → `button9_Click` — proc: UpdPlatiocaUvozKonacnaVrstaManipulacije, UpdPlatiocaUvozVrstaManipulacije; preko: InsertUvozKonacna.UpdPlatiocaUvozKonacnaUsluga, InsertUvozKonacna.UpdPlatiocaUvozUsluga
  - „P>>>>“ — klik → `button10_Click` — čita: Cene
  - „ADMINISTRATIVNE“ — klik → `button15_Click`
  - „SVE USLUGE“ — klik → `button14_Click`
  - „Prikazi sve kontejnere“ — klik → `toolStripButton6_Click`
  - „Prikazi selektovani kontejner“ — klik → `toolStripButton1_Click`
  - „Kamion za uslugu“ — klik → `toolStripButton7_Click` — otvara: frmVoziloUsluga
  - „PO SCENARIJU“ — klik → `button16_Click`
  - „PO GRUPI“ — klik → `button17_Click`
  - „DODATNE“ — klik → `button18_Click`
  - „Kopiraj usluge“ — klik → `button27_Click` — otvara: frmUvozKopirajUslugeKontejnera
  - „Kamion za uslugu“ — klik → `toolStripButton7_Click` — otvara: frmVoziloUsluga
  - „Prikaži selektovani kontejner“ — klik → `toolStripButton1_Click`
  - „Svi kontejneri“ — klik → `toolStripButton6_Click`
  - „Promeni  platioca“ — klik → `button19_Click` — proc: UpdCenaUvozKonacnaVrstaManipulacije, UpdPlatiocaUvozKonacnaVrstaManipulacije, UpdPlatiocaUvozVrstaManipulacije; preko: InsertUvozKonacna.UpdCenaUvozKonacnaUsluga, InsertUvozKonacna.UpdPlatiocaUvozKonacnaUsluga, InsertUvozKonacna.UpdPlatiocaUvozUsluga
  - „Promeni  platioca“ — klik → `button21_Click` — proc: UpdCenaUvozKonacnaVrstaManipulacije, UpdPlatiocaUvozKonacnaVrstaManipulacije, UpdPlatiocaUvozVrstaManipulacije; preko: InsertUvozKonacna.UpdCenaUvozKonacnaUsluga, InsertUvozKonacna.UpdPlatiocaUvozKonacnaUsluga, InsertUvozKonacna.UpdPlatiocaUvozUsluga
  - „Promeni  platioca“ — klik → `button22_Click` — proc: UpdCenaUvozKonacnaVrstaManipulacije, UpdPlatiocaUvozKonacnaVrstaManipulacije, UpdPlatiocaUvozVrstaManipulacije; preko: InsertUvozKonacna.UpdCenaUvozKonacnaUsluga, InsertUvozKonacna.UpdPlatiocaUvozKonacnaUsluga, InsertUvozKonacna.UpdPlatiocaUvozUsluga
  - „S N I M I“ — klik → `button26_Click`
  - „Primeni“ — klik → `button28_Click` — proc: UpdPokretFormaStatus; preko: InsertUvozKonacna.UpdPokretFormaStatusi
  - „PROMENI SCENARIO“ — klik → `button29_Click` — čita: RadniNalogInterni, Scenario, UvozKonacna, VrstaManipulacije

#### frmuvKvalitetKontejnera [S]
- **Fajl:** `Uvoz/frmuvKvalitetKontejnera.cs` — „Kvalitet kontejnera“
- **Stari izgled:** Terminal › Šifarnici › Kvalitet kontejnera → **frmuvKvalitetKontejnera**
- **Tabele:** `uvKvalitetKontejnera` SIUD p
- **Procedure:** `DeleteuvKvalitetKontejnera`, `InsertuvKvalitetKontejnera`, `UpdateuvKvalitetKontejnera`
- **Funkcionalnosti:**
  - „toolStrip1“ — ItemClicked → `toolStrip1_ItemClicked`
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertuvKvalitetKontejnera, UpdateuvKvalitetKontejnera; preko: InsertuvKvalitetKontejnera.InsuvKvalitetKontejnera, InsertuvKvalitetKontejnera.UpduvKvalitetKontejnera
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteuvKvalitetKontejnera; preko: InsertuvKvalitetKontejnera.DeluvKvalitetKontejnera
  - `button20` — klik → `tsDelete_Click` — proc: DeleteuvKvalitetKontejnera; preko: InsertuvKvalitetKontejnera.DeluvKvalitetKontejnera
  - `button21` — klik → `tsSave_Click` — proc: InsertuvKvalitetKontejnera, UpdateuvKvalitetKontejnera; preko: InsertuvKvalitetKontejnera.InsuvKvalitetKontejnera, InsertuvKvalitetKontejnera.UpduvKvalitetKontejnera

#### frmUvozKonacna [N+S]
- **Fajl:** `Uvoz/UvozKonacna.cs` — „PLAN PRIJEMA VOZA UVOZ“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Voz“ → **frmPrijemIOtpremaVozova4** → „Vozovi“ → **frmVoz** → „UVOZ - FORMIRANJE PLANA“ → **frmFormiranjePlana** → „Otvori plan“ → **frmUvozKonacna**
- **Stari izgled:** Uvoz › Uvoz obrada › Planovi utovara → **frmPregledPlanovaUtovara** → „Otvori“ → **frmUvozKonacna**
- **Otvara:** frmKontaktOsobe (`button19` (Click)), UvozDokumenta (prečica na tastaturi (`frmUvozKonacna_KeyDown`)), frmUvozKonacnaTable (prečica na tastaturi (`frmUvozKonacna_KeyDown`)), frmUnosManipulacija (prečica na tastaturi (`frmUvozKonacna_KeyDown`)), frmKontaktOsobeMU (`button14` (Click)), frmFormiranjePlana („Dodaj nerasporedjene u plan“), UvozNHM („NHM/HS KODOVI“), frmUvozUvoznici („Uvezi spisak“), frmPregledKontejneraDrumskeUsluge („DRUMSKE USLUGE“)
- **Tabele:** `Brodovi` S, `Carinarnice` S, `Cene` S, `DirigacijaKontejneraZa` S, `InspekciskiTretman` S, `KontejnerskiTerminali` S, `KontejnerStatus` S, `MestaUtovara` S, `NapomenaZaPozicioniranje` S, `NHM` S, `organizacioneJedinice` S, `Partnerji` S, `partnerjiKontOseba` S, `partnerjiKontOsebaMU` S, `Predefinisaneporuke` S, `RadniNalogInterni` SI p, `Scenario` S, `TipKontenjera` S, `uvNacinPakovanja` S, `Uvoz` U, `UvozKonacna` SD p, `UvozKonacnaNapomenePozicioniranja` SID p, `UvozKonacnaNHM` SID p, `UvozKonacnaVrstaManipulacije` SI p, `UvozKonacnaVrstaRobeHS` SD p, `UvozKonacnaZaglavlje` SUD p, `UvozNapomenePozicioniranja` S, `UvozNHM` S, `UvozUvoznici` S, `UvozVrstaRobeHS` S, `Voz` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaRobe` S, `VrstaRobeADR` S, `VrstaRobeHS` S, `VrstePostupakaUvoz` S
- **Procedure:** `DeleteUvozKonacna`, `DeleteUvozKonacnaNapomenePozicioniranja`, `DeleteUvozKonacnaNHM`, `DeleteUvozKonacnaVrstaManipulacijeKamionska`, `DeleteUvozKonacnaVrstaRobeHS`, `DeleteUvozKonacnaZaglavlje`, `DeleteUvozTerminalskeManipulacijeKonacna`, `DeleteUvozVrstaManTerminalskih`, `InsertRadniNalogInterni`, `InsertUvozKonacbaVrstaRobeHS`, `InsertUvozKonacnaNapomenePozicioniranja`, `InsertUvozKonacnaNHM`, `InsertUvozKonacnaVrstaManipulacije`, `spIzbrisiKUScenario`, `spStornijaRNI`, `UbaciURadniNalogInterniBeyScenarija`, `UpdateScenarioKontejnera`, `UpdateUvozKonacnaZaglavlje`
- **Combobox → tabela:** `cbDirigacija` ← `DirigacijaKontejneraZa`; `cboCarinskiPostupak` ← `VrstaCarinskogPostupka`; `cbPostupak` ← `VrstePostupakaUvoz`; `cbNacinPakovanja` ← `uvNacinPakovanja`; `cbNapomenaPoz` ← `NapomenaZaPozicioniranje`; `cbBrod` ← `Brodovi`; `cbOcarina` ← `Carinarnice`; `cbVlasnikKont` ← `Partnerji`; `cboUvoznik` ← `Partnerji`; `cboSpedicijaG` ← `Partnerji`; `cboSpedicijaRTC` ← `Partnerji`; `cbOspedicija` ← `Partnerji`; `cboNHM` ← `NHM`; `cboVoz` ← `Voz`; `txtADR` ← `VrstaRobeADR`; `txtTipKont` ← `TipKontenjera`; `cboRLTerminal` ← `KontejnerskiTerminali`; `cboRLTerminal2` ← `KontejnerskiTerminali`; `cboRLTerminal3` ← `KontejnerskiTerminali`; `cboBrodar` ← `Partnerji`; `cboNalogodavac1` ← `Partnerji`; `cboNalogodavac2` ← `Partnerji`; `cboNalogodavac3` ← `Partnerji`; `cboBrodar` ← `Partnerji`; `txtVrstaPregleda` ← `InspekciskiTretman`; `txtMesto` ← `MestaUtovara`; `txtKontaktOsoba` ← `partnerjiKontOsebaMU`; `cboScenario` ← `Scenario`; `cboNHM` ← `NHM`; `cboNHM` ← `NHM`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: UpdateUvozKonacna; preko: InsertUvozKonacna.UpdUvozKonacna
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteUvozKonacna; preko: InsertUvozKonacna.DelUvozKonacna
  - „Excel export“ — klik → `toolStripDropDownButton1_Click`
  - „ExportTo Excel HŽ“ — klik → `exportToExcelHŽToolStripMenuItem_Click` — izvoz u Excel
  - „Export to Excel Drumski“ — klik → `exportToExcelDrumskiToolStripMenuItem_Click` — izvoz u Excel
  - „Export to Excel Magacin“ — klik → `exportToExcelMagacinToolStripMenuItem_Click` — izvoz u Excel
  - „Export to Excel Bezbednost“ — klik → `exportToExcelBezbednostToolStripMenuItem_Click` — izvoz u Excel
  - „Export to Excel Špediter“ — klik → `exportToExcelŠpediterToolStripMenuItem_Click` — izvoz u Excel
  - „Export to Excel opšte“ — klik → `exportToExcelOpšteToolStripMenuItem_Click` — izvoz u Excel
  - „Dokumentacija-SHD“ — klik → `toolStripButton6_Click` — otvara: UvozDokumenta
  - „Tabela - SHT“ — klik → `toolStripButton3_Click_1` — otvara: frmUvozKonacnaTable
  - „Usluge-SHU“ — klik → `toolStripButton4_Click_1` — otvara: frmUnosManipulacija
  - „Izdavanje naloga“ — klik → `toolStripButton10_Click` — proc: InsertRadniNalogInterni; preko: InsertRadniNalogInterni.InsRadniNalogInterni
  - „Ubacivanje nerasporedjenih u plan“ — klik → `toolStripButton1_Click_1` — otvara: frmFormiranjePlana
  - „Ažuriraj šifarnike“ — klik → `toolStripButton5_Click_1`
  - „Odredišna carinarnica“ — klik → `label26_Click`
  - „Broj plombe 1“ — klik → `label31_Click`
  - „Delete“ — klik → `button3_Click` — proc: DeleteUvozKonacnaZaglavlje; preko: InsertUvozKonacnaZaglavlje.DelUvozKonacnaZaglavlje
  - „Promena“ — klik → `button2_Click` — proc: UpdateUvozKonacnaZaglavlje; preko: InsertUvozKonacnaZaglavlje.UpdUvozKonacnaZaglavlje
  - „Unesi“ — klik → `button1_Click` — proc: InsertUvozKonacnaZaglavlje; preko: InsertUvozKonacnaZaglavlje.InsUvozKonacnaZaglavlje
  - „Delete“ — klik → `button4_Click` — proc: DeleteUvozKonacnaVrstaRobeHS; preko: InsertUvozKonacna.DelUvozKonacnaVrstaRobeHS
  - „Unesi“ — klik → `button5_Click` — proc: InsertUvozKonacbaVrstaRobeHS; preko: InsertUvozKonacna.InsUvozKonacnaVrstaRobeHS
  - „Delete“ — klik → `button6_Click` — proc: DeleteUvozKonacnaNHM; preko: InsertUvozKonacna.DelUvozKonacnaNHM
  - „Unesi“ — klik → `button7_Click` — proc: InsertUvozKonacnaNHM; preko: InsertUvozKonacna.InsUvozKonacnaNHM
  - „Delete“ — klik → `button8_Click` — proc: DeleteUvozKonacnaNapomenePozicioniranja; preko: InsertUvozKonacna.DelUvozKonacnaNapomenePozicioniranja
  - „Unesi“ — klik → `button9_Click` — proc: InsertUvozKonacnaNapomenePozicioniranja; preko: InsertUvozKonacna.InsUvozKonacnaNapomenePozicioniranja
  - „Promeni“ — klik → `button10_Click`
  - „Ref za fakturisanje 2“ — klik → `label50_Click`
  - „OPŠTI“ — klik → `button11_Click`
  - „V V V V V“ — klik → `button12_Click`
  - „NPP V V“ — klik → `button13_Click`
  - „Obrni“ — klik → `button15_Click` — čita: NHM
  - `button17` — klik → `button17_Click` — čita: partnerjiKontOsebaMU
  - „Unesi“ — klik → `button18_Click`
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - „DobijenBZ“ — čekiranje → `chkDobijenBZ_CheckedChanged`
  - „Interni“ — čekiranje → `chkInterni_CheckedChanged`
  - „DRUMSKE USLUGE“ — klik → `button38_Click` — otvara: frmPregledKontejneraDrumskeUsluge
  - „Generisi istovar voz“ — klik → `button39_Click` — proc: UbaciURadniNalogInterniBeyScenarija; preko: InsertUvozKonacna.UbaciURadniNalogInterniBeyScenarija
  - … još 20

#### frmUvozKonacnaTable [N+S]
- **Fajl:** `Uvoz/frmUvozKonacnaTable.cs` — „Uvoz konacna table“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Voz“ → **frmPrijemIOtpremaVozova4** → „Vozovi“ → **frmVoz** → „UVOZ - FORMIRANJE PLANA“ → **frmFormiranjePlana** → „Otvori plan“ → **frmUvozKonacna** → prečica na tastaturi (`frmUvozKonacna_KeyDown`) → **frmUvozKonacnaTable**
- **Stari izgled:** Uvoz › Uvoz obrada › Planovi utovara → **frmPregledPlanovaUtovara** → „Otvori“ → **frmUvozKonacna** → prečica na tastaturi (`frmUvozKonacna_KeyDown`) → **frmUvozKonacnaTable**
- **Tabele:** `Brodovi` S, `Carinarnice` S, `DirigacijaKontejneraZa` S, `InspekciskiTretman` S, `KontejnerskiTerminali` S, `MestaUtovara` S, `Partnerji` S, `partnerjiKontOsebaMU` S, `Predefinisaneporuke` S, `Scenario` S, `TipKontenjera` S, `uvNacinPakovanja` S, `Uvoz` U, `UvozKonacna` SU, `UvozNapomenePozicioniranja` S, `uvuvozkonacnaoz` U, `VrstaCarinskogPostupka` S, `VrstaRobeADR` S, `VrstePostupakaUvoz` S
- **Combobox → tabela:** `cboOpsti` ← `Partnerji`; `cboOpsti` ← `Brodovi`; `cboOpsti` ← `Partnerji`; `cboOpsti` ← `Partnerji`; `cboOpsti` ← `Partnerji`; `cboOpsti` ← `KontejnerskiTerminali`; `cboOpsti` ← `KontejnerskiTerminali`; `cboOpsti` ← `KontejnerskiTerminali`; `cboOpsti` ← `TipKontenjera`; `cboOpsti` ← `VrstaRobeADR`; `cboOpsti` ← `Partnerji`; `cboOpsti` ← `Partnerji`; `cboOpsti` ← `InspekciskiTretman`; `cboOpsti` ← `Partnerji`; `cboOpsti` ← `Partnerji`; `cboOpsti` ← `VrstaCarinskogPostupka`; `cboOpsti` ← `VrstePostupakaUvoz`; `cboOpsti` ← `Partnerji`; `cboOpsti` ← `Carinarnice`; `cboOpsti` ← `MestaUtovara`; `cboOpsti` ← `partnerjiKontOsebaMU`
- **Funkcionalnosti:**
  - „Primeni izmenu“ — klik → `button2_Click`
  - „Izberi“ — klik → `button1_Click`
  - „Refresh“ — klik → `button4_Click`
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`

#### frmUvozKonacnaZaglavlje [S]
- **Fajl:** `Uvoz/frmUvozKonacnaZaglavlje.cs` — „Plan utovara“
- **Stari izgled:** Terminal › Terminalske usluge › Grupni prijem kamiona - N → **frmUvozKonacnaZaglavlje**
- **Otvara:** frmFormiranjePlana („Dopune kontejnera“)
- **Tabele:** `Stanice` S, `UvozKonacnaZaglavlje` SIUD p, `Voz` S
- **Procedure:** `DeleteUvozKonacnaZaglavlje`, `InsertUvozKonacnaZaglavlje`, `UpdateUvozKonacnaZaglavlje`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertUvozKonacnaZaglavlje, UpdateUvozKonacnaZaglavlje; preko: InsertUvozKonacnaZaglavlje.InsUvozKonacnaZaglavlje, InsertUvozKonacnaZaglavlje.UpdUvozKonacnaZaglavlje
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteUvozKonacnaZaglavlje; preko: InsertUvozKonacnaZaglavlje.DelUvozKonacnaZaglavlje
  - „Dopuna kontejnera nerasporedjeni“ — klik → `toolStripButton1_Click` — otvara: frmFormiranjePlana
  - „Dopune kontejnera“ — klik → `toolStripButton1_Click` — otvara: frmFormiranjePlana
  - `button20` — klik → `tsDelete_Click` — proc: DeleteUvozKonacnaZaglavlje; preko: InsertUvozKonacnaZaglavlje.DelUvozKonacnaZaglavlje
  - `button21` — klik → `tsSave_Click` — proc: InsertUvozKonacnaZaglavlje, UpdateUvozKonacnaZaglavlje; preko: InsertUvozKonacnaZaglavlje.InsUvozKonacnaZaglavlje, InsertUvozKonacnaZaglavlje.UpdUvozKonacnaZaglavlje
  - `this` — otvaranje forme → `frmUvozKonacnaZaglavlje_Load` — čita: Voz

#### frmUvozKopirajKontejner [S]
- **Fajl:** `Uvoz/frmUvozKopirajKontejner.cs` — „KOPIRANJE KONTEJNERA“
- **Stari izgled:** Uvoz › Uvoz obrada › Neraspoređen kontejneri tabela → **frmPregledNerasporedjeni** → „Kopiraj“ → **frmUvozKopirajKontejner**
- **Procedure:** `KopirajKontejner`
- **Funkcionalnosti:**
  - „Kopiraj“ — klik → `button1_Click` — proc: KopirajKontejner; preko: InsertUvoz.KopirajKontejner

#### frmUvozKopirajUslugeKontejnera [N+S]
- **Fajl:** `Uvoz/frmUvozKopirajUslugeKontejnera.cs` — „Kopiraj usluge kontejnera “
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Voz“ → **frmPrijemIOtpremaVozova4** → „Vozovi“ → **frmVoz** → „UVOZ - FORMIRANJE PLANA“ → **frmFormiranjePlana** → „Otvori plan“ → **frmUvozKonacna** → prečica na tastaturi (`frmUvozKonacna_KeyDown`) → **frmUnosManipulacija** → „Kopiraj usluge“ → **frmUvozKopirajUslugeKontejnera**
- **Stari izgled:** Uvoz › Uvoz obrada › Neraspoređeni kontejneri - Obrazac → **Uvoz** → prečica na tastaturi (`Uvoz_KeyDown`) → **frmUnosManipulacija** → „Kopiraj usluge“ → **frmUvozKopirajUslugeKontejnera**
- **Tabele:** `Brodovi` S, `Carinarnice` S, `DirigacijaKontejneraZa` S, `KontejnerskiTerminali` S, `MestaUtovara` S, `Partnerji` S, `Predefinisaneporuke` S, `TipKontenjera` S, `uvNacinPakovanja` S, `Uvoz` S, `UvozKonacna` S, `VrstaCarinskogPostupka` S, `VrstaRobeADR` S, `VrstePostupakaUvoz` S
- **Funkcionalnosti:**
  - „Kopiraj“ — klik → `button23_Click`

#### frmUvozUvoznici [N+S]
- **Fajl:** `Uvoz/frmUvozUvoznici.cs` — „Lista uvoznika za kontejner“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Voz“ → **frmPrijemIOtpremaVozova4** → „Vozovi“ → **frmVoz** → „UVOZ - FORMIRANJE PLANA“ → **frmFormiranjePlana** → „Otvori plan“ → **frmUvozKonacna** → „Uvezi spisak“ → **frmUvozUvoznici**
- **Stari izgled:** Uvoz › Uvoz obrada › Neraspoređeni kontejneri - Obrazac → **Uvoz** → „Uvezi spisak“ → **frmUvozUvoznici**
- **Tabele:** `UvozUvoznici` SID p
- **Procedure:** `DeleteUvozUvoznici`, `InsertUvozUvoznici`
- **Funkcionalnosti:**
  - „Učitaj“ — klik → `button4_Click` — proc: InsertUvozUvoznici; preko: InsertUvozKonacna.InsUvozUvoznici
  - „Upload“ — klik → `button2_Click` — izvoz u Excel
  - „Izbrisi selektovane“ — klik → `button1_Click` — proc: DeleteUvozUvoznici; preko: InsertUvozKonacna.DelUvozUvoznici

#### frmVagoniSerije [S]
- **Fajl:** `Sifarnici/frmVagoniSerije.cs`
- **Stari izgled:** Osnovni podaci › Prevozni šifarnici › Serije Vagoni → **frmVagoniSerije**
- **Tabele:** `VagoniSerije` SIUD p
- **Procedure:** `DeleteVagoniSerije`, `InsertVagoniSerije`, `UpdateVagoniSerije`
- **Funkcionalnosti:**
  - `tsSave` — klik → `tsSave_Click` — proc: InsertVagoniSerije, UpdateVagoniSerije; preko: InsertVagoniSerije.InsVagoniSerije, InsertVagoniSerije.UpdVagoniSerije
  - `tsDelete` — klik → `tsDelete_Click` — proc: DeleteVagoniSerije; preko: InsertVagoniSerije.DelVagoniSerije

#### frmValute [S]
- **Fajl:** `Izvoz/frmValute.cs` — „Valute“
- **Stari izgled:** Izvoz › Šifarnici › Valute → **frmValute**
- **Tabele:** `Valute` SIUD p
- **Procedure:** `DeleteValute`, `InsertValute`, `UpdateValute`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertValute, UpdateValute; preko: InsertValute.InsValute, InsertValute.UpdValute
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteValute; preko: InsertValute.DelValute
  - `button20` — klik → `tsDelete_Click` — proc: DeleteValute; preko: InsertValute.DelValute
  - `button21` — klik → `tsSave_Click` — proc: InsertValute, UpdateValute; preko: InsertValute.InsValute, InsertValute.UpdValute

#### frmVizuelniPregledi [S]
- **Fajl:** `RadniNalozi/frmVizuelniPregledi.cs` — „Vizuelni pregledi - PREGLEDAČ“
- **Stari izgled:** Terminal › Prevoz › P - VIZUELNI PREGLEDI → **frmVizuelniPregledi**
- **Tabele:** `RNPrijemVoza` S, `TipKontenjera` S, `Voz` S
- **Funkcionalnosti:**
  - „1: AKTIVNI“ — klik → `button2_Click` — čita: RNPrijemVoza, TipKontenjera, Voz
  - „2: URAĐENI“ — klik → `button1_Click` — čita: RNPrijemVoza, TipKontenjera, Voz
  - „Uradjeni“ — klik → `button1_Click` — čita: RNPrijemVoza, TipKontenjera, Voz
  - „Aktivni“ — klik → `button2_Click` — čita: RNPrijemVoza, TipKontenjera, Voz

#### frmVoz [N+S]
- **Fajl:** `TrackModal/Dokumeta/frmVoz.cs` — „VOZOVI“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Voz“ → **frmPrijemIOtpremaVozova4** → „Vozovi“ → **frmVoz**
- **Stari izgled:** Terminal › Prevoz › Vozovi → **frmVoz**
- **Otvara:** frmFormiranjePlana („UVOZ - FORMIRANJE PLANA“), frmFormiranjePlanaIzvoz („IZVOZ - FORMIRANJE PLANA“)
- **Tabele:** `IzvozKonacna` S, `IzvozKonacnaZaglavlje` SI p, `Partnerji` S, `SerijeKola` S, `Stanice` S, `UvozKonacna` SU, `UvozKonacnaZaglavlje` S, `Voz` SUD p, `VozSerijeKola` S
- **Procedure:** `DeleteVoz`, `InsertIzvozKonacnaZaglavlje`, `UpdateVoz`
- **Combobox → tabela:** `cboStanicaOd` ← `Stanice`; `cboStanicaDo` ← `Stanice`; `cboSerijaKola` ← `SerijeKola`; `cboOperater` ← `Partnerji`; `cboOperaterSrbija` ← `Partnerji`; `cboOperaterHR` ← `Partnerji`; `cboVlasnik` ← `Partnerji`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertVoz, UpdateVoz; preko: InsertVoz.InsVoz, InsertVoz.UpdVoz
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteVoz; preko: InsertVoz.DeleteVoz
  - „UVOZ-FORMIRANJE PLANA“ — klik → `toolStripButton1_Click` — čita: UvozKonacnaZaglavlje
  - „IZVOZ - FORMIRANJE PLANA “ — klik → `toolStripButton2_Click` — otvara: frmFormiranjePlanaIzvoz; čita: IzvozKonacnaZaglavlje; proc: InsertIzvozKonacnaZaglavlje; preko: InsertIzvozKonacnaZaglavlje.InsIzvozKonacnaZaglavlje
  - „Relacija“ — klik → `btnUbaciRelaciju_Click`
  - „Dolazeći voz“ — čekiranje → `chkDolazeci_CheckedChanged`
  - „D“ — klik → `button1_Click`
  - „D“ — klik → `button2_Click`
  - „D“ — klik → `button3_Click`
  - „D“ — klik → `button4_Click`
  - „Izbaci“ — klik → `button6_Click` — proc: DeleteVozSerijeKola; preko: InsertVoz.DelSerijeKola
  - „Ubaci“ — klik → `button5_Click` — proc: InsertVozSerijeKola; preko: InsertVoz.InsSerijeKola
  - „Serija kola:“ — klik → `label27_Click`
  - „U“ — klik → `button7_Click` — piše: UvozKonacna[U]; čita: IzvozKonacna, IzvozKonacnaZaglavlje
  - „IZVOZ - FORMIRANJE PLANA “ — klik → `toolStripButton2_Click` — otvara: frmFormiranjePlanaIzvoz; čita: IzvozKonacnaZaglavlje; proc: InsertIzvozKonacnaZaglavlje; preko: InsertIzvozKonacnaZaglavlje.InsIzvozKonacnaZaglavlje
  - „UVOZ - FORMIRANJE PLANA“ — klik → `toolStripButton1_Click` — čita: UvozKonacnaZaglavlje
  - `button20` — klik → `tsDelete_Click` — proc: DeleteVoz; preko: InsertVoz.DeleteVoz
  - `button21` — klik → `tsSave_Click` — proc: InsertVoz, UpdateVoz; preko: InsertVoz.InsVoz, InsertVoz.UpdVoz
  - `this` — otvaranje forme → `frmVoz_Load` — čita: Partnerji, SerijeKola, Stanice

#### frmVozac [N]
- **Fajl:** `Skladista main/frmVozac.cs` — „frmVozac“
- **Novi izgled:** Operacije skladišta → **SkladistaMain** → kartica „Radni nalozi“ → **NalogZaKomercijalni** → „SKLADIŠNI NALOZI UVOZ“ → **PregledKomercijalnihNaloga** → „Otvori“ → **frmNalogPrijem** → „Vozač“ → **frmVozac**
- **Tabele:** `RadniNalogInterni` S, `RadniNalogInterniPotvrda` S, `RadniNalogSkladista` S, `VrstaVozila` S
- **Procedure:** `UpdateRadniNalogDirektniVozilo`
- **Funkcionalnosti:**
  - „Snimi“ — klik → `btnSnimi_Click` — proc: UpdateRadniNalogDirektniVozilo; preko: InsertCarinskoSkladiste.UpdateRadniNalogDirektniVozilo

#### frmVozila [S]
- **Fajl:** `TrackModal/Dokumeta/frmVozila.cs` — „Sredstva rada“
- **Stari izgled:** Terminal › Šifarnici › Sredstva rada → **frmVozila**
- **Otvara:** frmDokumentaVozila („Dokumentacija“)
- **Tabele:** `Vozila` SIUD p
- **Procedure:** `DeleteVozila`, `InsertVozila`, `UpdateVozila`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertVozila, UpdateVozila; preko: InsertVozila.InsVozila, InsertVozila.UpdVozila
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteVozila; preko: InsertVozila.DeleteVozila
  - „toolStripButton1“ — klik → `tsPrvi_Click`
  - „Dokumentacija“ — klik → `toolStripButton1_Click` — otvara: frmDokumentaVozila
  - „Individualni broj:“ — klik → `label5_Click`
  - „Licenca broj:“ — klik → `label3_Click`
  - „Dokumentacija“ — klik → `toolStripButton1_Click` — otvara: frmDokumentaVozila
  - `button20` — klik → `tsDelete_Click` — proc: DeleteVozila; preko: InsertVozila.DeleteVozila
  - `button21` — klik → `tsSave_Click` — proc: InsertVozila, UpdateVozila; preko: InsertVozila.InsVozila, InsertVozila.UpdVozila

#### frmVozilaPregledPrijava [S]
- **Fajl:** `Dokumenta/frmVozilaPregledPrijava.cs` — „Sredstva rada pregled prijava“
- **Stari izgled:** Održavanje › SREDSTVA RADA › Zaduženje / Razduženje → **frmVozilaPregledPrijava**
- **Tabele:** `Delavci` S, `Vozila` S, `ZaposleniPrijavaAuto` S
- **Funkcionalnosti:**
  - `this` — otvaranje forme → `frmVozilaPregledPrijava_Load` — čita: Delavci, Vozila, ZaposleniPrijavaAuto

#### frmVoziloUsluga [N+S]
- **Fajl:** `Uvoz/frmVoziloUsluga.cs` — „VOZILO ZA USLUGU“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Voz“ → **frmPrijemIOtpremaVozova4** → „Vozovi“ → **frmVoz** → „UVOZ - FORMIRANJE PLANA“ → **frmFormiranjePlana** → „Otvori plan“ → **frmUvozKonacna** → prečica na tastaturi (`frmUvozKonacna_KeyDown`) → **frmUnosManipulacija** → „Kamion za uslugu“ → **frmVoziloUsluga**
- **Stari izgled:** Uvoz › Uvoz obrada › Neraspoređeni kontejneri - Obrazac → **Uvoz** → prečica na tastaturi (`Uvoz_KeyDown`) → **frmUnosManipulacija** → „Kamion za uslugu“ → **frmVoziloUsluga**
- **Tabele:** `VoziloUsluga` SIUD p
- **Procedure:** `DeleteVoziloUsluga`, `InsertVoziloUsluga`, `UpdateVoziloUsluga`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertVoziloUsluga, UpdateVoziloUsluga; preko: InsertVoziloUsluga.InsVoziloUsluga, InsertVoziloUsluga.UpdVoziloUsliga
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteVoziloUsluga; preko: InsertVoziloUsluga.DelVoziloUsluga
  - `button20` — klik → `tsDelete_Click` — proc: DeleteVoziloUsluga; preko: InsertVoziloUsluga.DelVoziloUsluga
  - `button21` — klik → `tsSave_Click` — proc: InsertVoziloUsluga, UpdateVoziloUsluga; preko: InsertVoziloUsluga.InsVoziloUsluga, InsertVoziloUsluga.UpdVoziloUsliga

#### frmVozOtprema [N]
- **Fajl:** `MainLeget/Prijemiotpremavozova/frmVozOtprema.cs` — „frmVozOtprema“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Otprema“ → **frmVozOtprema**

#### frmVozPrijem [N]
- **Fajl:** `MainLeget/Prijemiotpremavozova/frmVozPrijem.cs` — „frmVozPrijem“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Prijem“ → **frmVozPrijem**

#### frmVrstaCarinskogPostupka [S]
- **Fajl:** `Uvoz/frmVrstaCarinskogPostupka.cs` — „Vrsta carinskih postupaka“
- **Stari izgled:** Terminal › Šifarnici › Vrsta Carinskog postupka → **frmVrstaCarinskogPostupka**
- **Tabele:** `VrstaCarinskogPostupka` SIUD p
- **Procedure:** `DeleteVrstaCarinskogPostupka`, `InsertVrstaCarinskogPostupka`, `UpdateVrstaCarinskogPostupka`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertVrstaCarinskogPostupka, UpdateVrstaCarinskogPostupka; preko: InsertVrstaCarinskogPostupka.InsVrstaCarinskogPostupka, InsertVrstaCarinskogPostupka.UpdVrstaCarinskogPostupka
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteVrstaCarinskogPostupka; preko: InsertVrstaCarinskogPostupka.DelVrstaCarinskogPostupka
  - `button20` — klik → `tsDelete_Click` — proc: DeleteVrstaCarinskogPostupka; preko: InsertVrstaCarinskogPostupka.DelVrstaCarinskogPostupka
  - `button21` — klik → `tsSave_Click` — proc: InsertVrstaCarinskogPostupka, UpdateVrstaCarinskogPostupka; preko: InsertVrstaCarinskogPostupka.InsVrstaCarinskogPostupka, InsertVrstaCarinskogPostupka.UpdVrstaCarinskogPostupka

#### frmVrstaManipulacije [S]
- **Fajl:** `TrackModal/Sifarnici/frmVrstaManipulacije.cs` — „Vrsta usluge“
- **Stari izgled:** Terminal › Šifarnici › Vrste usluga → **frmVrstaManipulacije**
- **Tabele:** `GrupaVrsteManipulacije` S, `KontejnerskiTerminali` S, `MerskeEnote` S, `organizacioneJedinice` S, `TipKontenjera` S, `TipManipulacije` S, `VrstaManipulacije` SIUD p
- **Procedure:** `DeleteVrstaManipulacije`, `InsertVrstaManipulacije`, `UpdateVrstaManipulacije`
- **Funkcionalnosti:**
  - „Štampaj izveštaj“ — ItemClicked → `toolStrip1_ItemClicked`
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertVrstaManipulacije, UpdateVrstaManipulacije; preko: InsertVrstaManipulacije.InsVrstaManipulacije, InsertVrstaManipulacije.UpdVrstaManipulacije
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteVrstaManipulacije; preko: InsertVrstaManipulacije.DeleteVrstaManipulacije
  - „toolStripButton1“ — klik → `tsPrvi_Click` — čita: VrstaManipulacije
  - „toolStripButton1“ — klik → `tsNazad_Click` — čita: VrstaManipulacije
  - „toolStripButton1“ — klik → `tsNapred_Click` — čita: VrstaManipulacije
  - „toolStripButton1“ — klik → `tsPoslednja_Click` — čita: VrstaManipulacije
  - „OJ koja izvšava zadatak:“ — klik → `label6_Click`
  - „Dodatna usluga“ — čekiranje → `chkDodatna_CheckedChanged`
  - `button20` — klik → `tsDelete_Click` — proc: DeleteVrstaManipulacije; preko: InsertVrstaManipulacije.DeleteVrstaManipulacije
  - `button21` — klik → `tsSave_Click` — proc: InsertVrstaManipulacije, UpdateVrstaManipulacije; preko: InsertVrstaManipulacije.InsVrstaManipulacije, InsertVrstaManipulacije.UpdVrstaManipulacije
  - `this` — otvaranje forme → `frmVrstaManipulacije_Load` — čita: GrupaVrsteManipulacije, KontejnerskiTerminali, MerskeEnote, OrganizacioneJedinice, TipKontenjera, TipManipulacije

#### frmVrstaNepravilnosti [S]
- **Fajl:** `Neispravnosti/frmVrstaNepravilnosti.cs` — „Vrste Nepravilnosti“
- **Stari izgled:** Evidencije › Vrste aktivnosti › Vrsta nepravilnosti → **frmVrstaNepravilnosti**
- **Tabele:** `SifVrstaNepravilnosti` SIUD p
- **Procedure:** `DeleteSifVrstaNepravilnosti`, `InsertSifVrstaNepravilnosti`, `UpdateSifVrstaNepravilnosti`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertSifVrstaNepravilnosti, UpdateSifVrstaNepravilnosti; preko: InsertNepravilnosti.InsSifVrstaNepravilnosti, InsertNepravilnosti.UpdSifVrstaNepravilnosti
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteSifVrstaNepravilnosti; preko: InsertNepravilnosti.DelSifVrstaNepravilnosti

#### frmVrstaRobe [S]
- **Fajl:** `TrackModal/Sifarnici/frmVrstaRobe.cs` — „Vrsta robe“
- **Stari izgled:** Terminal › Šifarnici › Vrsta robe HS → **frmVrstaRobe**
- **Tabele:** `VrstaRobe` SIUD p
- **Procedure:** `DeleteVrstaRobe`, `InsertVrstaRobe`, `UpdateVrstaRobe`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertVrstaRobe, UpdateVrstaRobe; preko: InsertVrstaRobe.InsVrstaRobe, InsertVrstaRobe.UpdVrstaRobe
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteVrstaRobe; preko: InsertVrstaRobe.DeleteVrstaRobe
  - „toolStripButton1“ — klik → `tsPrvi_Click` — čita: VrstaRobe
  - „toolStripButton1“ — klik → `tsNazad_Click` — čita: VrstaRobe
  - „toolStripButton1“ — klik → `tsNapred_Click` — čita: VrstaRobe
  - „toolStripButton1“ — klik → `tsPoslednja_Click` — čita: VrstaRobe
  - „>>“ — klik → `tsPoslednja_Click` — čita: VrstaRobe
  - „>“ — klik → `tsNapred_Click` — čita: VrstaRobe
  - „<“ — klik → `tsNazad_Click` — čita: VrstaRobe
  - „<<“ — klik → `tsPrvi_Click` — čita: VrstaRobe
  - `button20` — klik → `tsDelete_Click` — proc: DeleteVrstaRobe; preko: InsertVrstaRobe.DeleteVrstaRobe
  - `button21` — klik → `tsSave_Click` — proc: InsertVrstaRobe, UpdateVrstaRobe; preko: InsertVrstaRobe.InsVrstaRobe, InsertVrstaRobe.UpdVrstaRobe

#### frmVrstaRobeADR [S]
- **Fajl:** `Uvoz/frmVrstaRobeADR.cs` — „Vrsta robe ADR“
- **Stari izgled:** Terminal › Šifarnici › Vrsta robe ADR → **frmVrstaRobeADR**
- **Tabele:** `VrstaRobeADR` SIUD p
- **Procedure:** `DeleteVrstaRobeADR`, `InsertVrstaRobeADR`, `UpdateVrstaRobeADR`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertVrstaRobeADR, UpdateVrstaRobeADR; preko: insertVrstaRobeADR.InsVrstaRobeADR, insertVrstaRobeADR.UpdVrstaRobeADR
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteVrstaRobeADR; preko: insertVrstaRobeADR.DelVrstaRobeADR
  - `button20` — klik → `tsDelete_Click` — proc: DeleteVrstaRobeADR; preko: insertVrstaRobeADR.DelVrstaRobeADR
  - `button21` — klik → `tsSave_Click` — proc: InsertVrstaRobeADR, UpdateVrstaRobeADR; preko: insertVrstaRobeADR.InsVrstaRobeADR, insertVrstaRobeADR.UpdVrstaRobeADR

#### frmVrstaRobeHS [S]
- **Fajl:** `Uvoz/frmVrstaRobeHS.cs` — „Vrsta robe“
- **Stari izgled:** Izvoz › Šifarnici › Vrsta robe HS → **frmVrstaRobeHS**
- **Tabele:** `VrstaRobeADR` SD p, `VrstaRobeHS` SIU p
- **Procedure:** `DeleteVrstaRobeADR`, `InsertVrstaRobeHS`, `UpdateVrstaRobeHS`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertVrstaRobeHS, UpdateVrstaRobeHS; preko: InsertVrstaRobeHS.InsVrstaRobeHS, InsertVrstaRobeHS.UpdVrstaRobeHS
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteVrstaRobeADR; preko: InsertVrstaRobeHS.DelVrstaRobeHS
  - `button20` — klik → `tsDelete_Click` — proc: DeleteVrstaRobeADR; preko: InsertVrstaRobeHS.DelVrstaRobeHS
  - `button21` — klik → `tsSave_Click` — proc: InsertVrstaRobeHS, UpdateVrstaRobeHS; preko: InsertVrstaRobeHS.InsVrstaRobeHS, InsertVrstaRobeHS.UpdVrstaRobeHS
  - `this` — otvaranje forme → `frmVrstaRobeHS_Load` — čita: VrstaRobeADR

#### frmVrstaVozila [S]
- **Fajl:** `Drumski/frmVrstaVozila.cs` — „frmVrstaVozila“
- **Stari izgled:** Drumski › Šifarnici › Vrsta vozila → **frmVrstaVozila**
- **Otvara:** frmVrstaVozilaEdit (`button19` (Click))
- **Tabele:** `VrstaVozila` SIUD p
- **Procedure:** `DeleteVrstaVozila`, `InsertVrstaVozila`, `UpdateVrstaVozila`
- **Funkcionalnosti:**
  - `dataGridView1` — izbor → `dataGridView1_SelectionChanged` — otvara: frmVrstaVozilaEdit
  - `button20` — klik → `tsDelete_Click` — proc: DeleteVrstaVozila; preko: InsertVrstaVozila.DelVrstaUsluga
  - `button21` — klik → `tsSave_Click` — proc: InsertVrstaVozila, UpdateVrstaVozila; preko: InsertVrstaVozila.InsVrstaVozila, InsertVrstaVozila.UpdVrstaVozila
  - `button19` — klik → `tsNew_Click` — otvara: frmVrstaVozilaEdit

#### frmVrstaVozilaEdit [S]
- **Fajl:** `Drumski/frmVrstaVozilaEdit.cs` — „Vrsta vozila detalji“
- **Stari izgled:** Drumski › Šifarnici › Vrsta vozila → **frmVrstaVozila** → `button19` (Click) → **frmVrstaVozilaEdit**
- **Tabele:** `VrstaVozila` IUD p
- **Procedure:** `DeleteVrstaVozila`, `InsertVrstaVozila`, `UpdateVrstaVozila`
- **Funkcionalnosti:**
  - `button20` — klik → `button20_Click` — proc: DeleteVrstaVozila; preko: InsertVrstaVozila.DelVrstaUsluga
  - `button21` — klik → `button21_Click` — proc: InsertVrstaVozila, UpdateVrstaVozila; preko: InsertVrstaVozila.InsVrstaVozila, InsertVrstaVozila.UpdVrstaVozila

#### frmVrsteAktivnosti [S]
- **Fajl:** `Sifarnici/frmVrsteAktivnosti.cs` — „Vrste aktivnosti“
- **Stari izgled:** Evidencije › Vrste aktivnosti › Vrste → **frmVrsteAktivnosti**
- **Tabele:** `VrstaAktivnosti` SIUD p
- **Procedure:** `DeleteVrstaAktivnosti`, `InsertVrstaAktivnosti`, `UpdateVrstaAktivnosti`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertVrstaAktivnosti, UpdateVrstaAktivnosti; preko: InsertVrstaAktivnosti.InsVrstaAktivnosti, InsertVrstaAktivnosti.UpdVrstaAktivnosti
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteVrstaAktivnosti; preko: InsertVrstaAktivnosti.DeleteVrstaAktivnosti

#### frmVrstePlombi [S]
- **Fajl:** `Izvoz/frmVrstePlombi.cs` — „Vrste plombi“
- **Stari izgled:** Terminal › Šifarnici › Vrste plombi → **frmVrstePlombi**
- **Tabele:** `VrstePlombi` SIUD p
- **Procedure:** `DeleteVrstePlombi`, `InsertVrstePlombi`, `UpdateVrstePlombi`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertVrstePlombi, UpdateVrstePlombi; preko: InsertVrstePlombi.InsVrstePlombi, InsertVrstePlombi.UpdVrstePlombi
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteVrstePlombi; preko: InsertVrstePlombi.DelVrstePlombi
  - `button20` — klik → `tsDelete_Click` — proc: DeleteVrstePlombi; preko: InsertVrstePlombi.DelVrstePlombi
  - `button21` — klik → `tsSave_Click` — proc: InsertVrstePlombi, UpdateVrstePlombi; preko: InsertVrstePlombi.InsVrstePlombi, InsertVrstePlombi.UpdVrstePlombi

#### frmVrstePopisa [S]
- **Fajl:** `Servis/frmVrstePopisa.cs` — „Vrste popisa“
- **Stari izgled:** Održavanje › Lokomotive osnovno › Vrsta popisa → **frmVrstePopisa**
- **Tabele:** `MAticniPodatki` S, `VrstaPopisa` SIUD p
- **Procedure:** `DeleteVrstaPopisa`, `InsertVrstaPopisa`, `UpdateVrstaPopisa`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertVrstaPopisa, UpdateVrstaPopisa; preko: InsertVrstaPopisa.InsVrstaPopisa, InsertVrstaPopisa.UpdVrstaPopisa
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteVrstaPopisa; preko: InsertVrstaPopisa.DelVrstaPopisa
  - `this` — otvaranje forme → `frmVrstePopisa_Load` — čita: MaticniPodatki

#### frmVrstePostupakaUvoz [S]
- **Fajl:** `Uvoz/frmVrstePostupakaUvoz.cs` — „Postupanje sa robom/kontejerom“
- **Stari izgled:** Terminal › Šifarnici › Predefinisane poruke - treba → **frmVrstePostupakaUvoz**
- **Tabele:** `VrstePostupakaUvoz` SIUD p
- **Procedure:** `DeleteVrstePostupakaUvoz`, `InsertVrstePostupakaUvoz`, `UpdateVrstePostupakaUvoz`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertVrstePostupakaUvoz, UpdateVrstePostupakaUvoz; preko: InsertVrstePostupakaUvoz.InsVrstePostupakaUvoz, InsertVrstePostupakaUvoz.UpdVrstePostupakaUvoz
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteVrstePostupakaUvoz; preko: InsertVrstePostupakaUvoz.DelVrstePostupakaUvoz
  - `button20` — klik → `tsDelete_Click` — proc: DeleteVrstePostupakaUvoz; preko: InsertVrstePostupakaUvoz.DelVrstePostupakaUvoz
  - `button21` — klik → `tsSave_Click` — proc: InsertVrstePostupakaUvoz, UpdateVrstePostupakaUvoz; preko: InsertVrstePostupakaUvoz.InsVrstePostupakaUvoz, InsertVrstePostupakaUvoz.UpdVrstePostupakaUvoz

#### frmVucaPregled [S]
- **Fajl:** `Dokumenta/frmVucaPregled.cs` — „VucaPregled“
- **Stari izgled:** Evidencije › Evidencija aktivnosti › Pregled trenutnih aktivnosti → **frmPregledAktivnosti** → „Vuca“ → **frmVucaPregled**
- **Tabele:** `Aktivnosti` S, `AktivnostiStavke` S, `Delavci` S, `Najava` S, `SifVucaStatusi` S, `Stanice` S, `Vuca` S, `VucaStatusi` S
- **Funkcionalnosti:**
  - „Refresh sve“ — klik → `btn_OtvoriSliku_Click`
  - „Refresh samo aktivni poslovi“ — klik → `button1_Click`

#### frmVucaStatusi [S]
- **Fajl:** `Dokumenta/frmVucaStatusi.cs` — „Vuča statusi“
- **Stari izgled:** Osnovni podaci › Prevozni šifarnici › Vuča statusi → **frmVucaStatusi**
- **Tabele:** `SifVucaStatusi` S
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click`
  - „toolStripButton1“ — klik → `tsDelete_Click`

#### frmZarade [S]
- **Fajl:** `Dokumenta/frmZarade.cs` — „Obračun po satu“
- **Stari izgled:** Evidencije › Evidencija aktivnosti › Izveštaj zarade → **frmZarade**
- **Tabele:** `Delavci` S
- **Funkcionalnosti:**
  - „Štampaj“ — klik → `btnStampa_Click` — izveštaj: Zarada.rdlc
  - `this` — otvaranje forme → `frmZarade_Load` — čita: Delavci

#### frmZavrsnaDokumentacija [S]
- **Fajl:** `Mobile/frmZavrsnaDokumentacija.cs` — „Završna dokumentacija“
- **Stari izgled:** Operatika › Kontrola dokumentacije › Završna dokumentacija → **frmZavrsnaDokumentacija**
- **Tabele:** `Delavci` S, `TipZavrsnogDokumentaID` S, `ZavrsnaDokumenta` S
- **Funkcionalnosti:**
  - „Ažuriraj sve“ — klik → `metroButton1_Click`

#### frmZone [N+S]
- **Fajl:** `TrackModal/Sifarnici/frmZone.cs` — „Zone“
- **Novi izgled:** Podešavanja sistema → **Podesavanje1** → „Zone“ → **frmZone**
- **Stari izgled:** Terminal › Šifarnici › Zone → **frmZone**
- **Tabele:** `TipZone` S, `Zone` SIUD p
- **Procedure:** `DeleteZone`, `InsertZone`, `UpdateZone`
- **Combobox → tabela:** `cboTipZoneID` ← `TipZone`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertZone, UpdateZone; preko: InsertZone.InsZone, InsertZone.UpdZone
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteZone; preko: InsertZone.DeleteZone
  - `button20` — klik → `tsDelete_Click` — proc: DeleteZone; preko: InsertZone.DeleteZone
  - `button21` — klik → `tsSave_Click` — proc: InsertZone, UpdateZone; preko: InsertZone.InsZone, InsertZone.UpdZone
  - „gridGroupingControl1“ — SelectedRecordsChanged → `gridGroupingControl1_SelectedRecordsChanged`
  - „Refresh“ — klik → `button2_Click`
  - „Neaktivne“ — klik → `button1_Click`
  - „Aktivne“ — klik → `button23_Click`

#### fruvNacinPakovanja [S]
- **Fajl:** `Uvoz/fruvNacinPakovanja.cs` — „Način pakovanja“
- **Stari izgled:** Izvoz › Šifarnici › Način pakovanja → **fruvNacinPakovanja**
- **Tabele:** `uvNacinPakovanja` SIUD p
- **Procedure:** `DeleteuvNacinPakovanja`, `InsertuvNacinPakovanja`, `UpdateuvNacinPakovanja`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertuvNacinPakovanja, UpdateuvNacinPakovanja; preko: InsertuvNacinPakovanja.InsuvNacinPakovanja, InsertuvNacinPakovanja.UpduvNacinPakovanja
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteuvNacinPakovanja; preko: InsertuvNacinPakovanja.DeluvNacinPakovanja
  - `button20` — klik → `tsDelete_Click` — proc: DeleteuvNacinPakovanja; preko: InsertuvNacinPakovanja.DeluvNacinPakovanja
  - `button21` — klik → `tsSave_Click` — proc: InsertuvNacinPakovanja, UpdateuvNacinPakovanja; preko: InsertuvNacinPakovanja.InsuvNacinPakovanja, InsertuvNacinPakovanja.UpduvNacinPakovanja

#### GlavniEkran [N]
- **Fajl:** `MainLeget/GlavniEkran.cs` — „GlavniEkran“
- **Novi izgled:** metoda `ShowHome` → **GlavniEkran**
- **Tabele:** `TerminalPriv` S

#### Intermodalni1 [N]
- **Fajl:** `MainLeget/Intermodalni/Intermodalni1.cs` — „Intermodalni1“
- **Novi izgled:** Intermodalni terminal → **Intermodalni1**
- **Otvara:** IntermodalniKomN („Komercijalni nalozi“), VozniPlanovi („Vozni planovi“), frmTerminalPrivremeni („Terminal temp“)
- **Funkcionalnosti:**
  - „Komercijalni nalozi“ — klik → `btnIntermodalni11_Click` — otvara: IntermodalniKomN
  - „Vozni planovi“ — klik → `btnIntermodalni13_Click` — otvara: VozniPlanovi
  - „Terminal temp“ — klik → `btnIntermodalni15_Click`

#### IntermodalniKomN [N]
- **Fajl:** `MainLeget/Intermodalni/IntermodalniKomN.cs` — „IntermodalniKomN“
- **Novi izgled:** Intermodalni terminal → **Intermodalni1** → „Komercijalni nalozi“ → **IntermodalniKomN**
- **Otvara:** frmRadniNalogInterniPregled („Lista svih naloga“)
- **Funkcionalnosti:**
  - „Lista otvorenih naloga“ — klik → `btmKomercijalni4_Click` — otvara: frmRadniNalogInterniPregled
  - „Lista svih naloga“ — klik → `btmKomercijalni6_Click` — otvara: frmRadniNalogInterniPregled

#### InterniPoslovi [N]
- **Fajl:** `Skladista main/Dokumenta/InterniPoslovi.cs` — „InterniPoslovi“
- **Novi izgled:** Operacije skladišta → **SkladistaMain** → kartica „Radni nalozi“ → **NalogZaKomercijalni** → „Kreiraj novi“ → **TipSkladista** → „Carinsko skladište“ → **RadniNaloziSkladista** → „Interni poslovi“ → **InterniPoslovi**
- **Tabele:** `Carinarnice` S, `MagacinskiBrojCarinski` SIU p, `MestaUtovara` S, `Partnerji` S, `RadniNalogSkladista` SIU p, `RNCarinskoSkladisteDodatneUsluge` SID p, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaVozila` S, `VrstePrevoznogSredstva` S
- **Procedure:** `DeleteRnCarinskoSkladisteDodatneUsluge`, `InsertMagacinskiBrojCarinski`, `InsertRadniNalogInterniSkladista`, `InsertRadniNalogSkladista`, `InsertRnCarinskoSkladisteDodatneUsluge`, `UpdateMagacinskiBrojCarinski`, `UpdateRadniNalogSkladista`, `UpdateRNInterniSkladista`
- **Combobox → tabela:** `cboDodatneUsluge` ← `VrstaManipulacije`; `cboNalogodavac` ← `Partnerji`; `cboVlasnikRobe` ← `Partnerji`; `cboCarinskiPostupak` ← `VrstaCarinskogPostupka`; `cboSpediterOtprema` ← `Partnerji`; `cboTipTransportaOtprema` ← `VrstePrevoznogSredstva`; `cboVrstaKamionaOtprema` ← `VrstaVozila`; `cboCarinarnicaOtprema` ← `Carinarnice`; `cboMestoIstovaraOtprema` ← `MestaUtovara`; `cboMagacinskiBroj` ← `MagacinskiBrojCarinski`
- **Funkcionalnosti:**
  - „Izbaci“ — klik → `btnIzbaci_Click`
  - „Nazad“ — klik → `btnMbNazad_Click`
  - „Sačuvaj“ — klik → `btnMbSave_Click` — čita: MagacinskiBrojCarinski; proc: InsertMagacinskiBrojCarinski, UpdateMagacinskiBrojCarinski; preko: InsertSkladista.InsertMagacinskiBrojCarinski, InsertSkladista.UpdateMagacinskiBrojCarinski
  - „Dodaj“ — klik → `btnDodaj_Click`
  - „FORMIRAN RN“ — klik → `btnFormiranRN_Click` — čita: RNCarinskoSkladisteDodatneUsluge; proc: DeleteRnCarinskoSkladisteDodatneUsluge, InsertRnCarinskoSkladisteDodatneUsluge, UpdateRadniNalogSkladista; preko: InsertSkladista.DeleteDodatneUsluge, InsertSkladista.InsertDodatneUsluge, InsertSkladista.UpdateRadniNalog
  - „SNIMI“ — klik → `btnSnimi_Click` — čita: RNCarinskoSkladisteDodatneUsluge, RadniNalogSkladista; proc: DeleteRnCarinskoSkladisteDodatneUsluge, InsertRadniNalogInterniSkladista, InsertRadniNalogSkladista, InsertRnCarinskoSkladisteDodatneUsluge, UpdateRNInterniSkladista, UpdateRadniNalogSkladista; preko: InsertSkladista.DeleteDodatneUsluge, InsertSkladista.InsertDodatneUsluge, InsertSkladista.InsertRNInterni, InsertSkladista.InsertRadniNalog, InsertSkladista.UpdateRNInterni
  - `cboVlasnikRobe` — SelectionChangeCommitted → `cboVlasnikRobe_SelectionChangeCommitted` — čita: Partnerji
  - „?“ — klik → `btnMagacinskiBroj_Click`

#### InterniPrenos [N]
- **Fajl:** `Skladista main/Dokumenta/InterniPrenos.cs` — „InterniPrenos“
- **Novi izgled:** Operacije skladišta → **SkladistaMain** → kartica „Radni nalozi“ → **NalogZaKomercijalni** → „Kreiraj novi“ → **TipSkladista** → „Carinsko skladište“ → **RadniNaloziSkladista** → „Interni prenos“ → **InterniPrenos**
- **Tabele:** `Carinarnice` S, `MagacinskiBrojCarinski` SIU p, `MestaUtovara` S, `Partnerji` S, `RadniNalogSkladista` SIU p, `RNCarinskoSkladisteDodatneUsluge` SID p, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaVozila` S, `VrstePrevoznogSredstva` S
- **Procedure:** `DeleteRnCarinskoSkladisteDodatneUsluge`, `InsertMagacinskiBrojCarinski`, `InsertRadniNalogInterniSkladista`, `InsertRadniNalogSkladista`, `InsertRnCarinskoSkladisteDodatneUsluge`, `UpdateMagacinskiBrojCarinski`, `UpdateRadniNalogSkladista`, `UpdateRNInterniSkladista`
- **Combobox → tabela:** `cboDodatneUsluge` ← `VrstaManipulacije`; `cboNalogodavac` ← `Partnerji`; `cboVlasnikRobe` ← `Partnerji`; `cboCarinskiPostupak` ← `VrstaCarinskogPostupka`; `cboSpediterOtprema` ← `Partnerji`; `cboTipTransportaOtprema` ← `VrstePrevoznogSredstva`; `cboVrstaKamionaOtprema` ← `VrstaVozila`; `cboCarinarnicaOtprema` ← `Carinarnice`; `cboMestoIstovaraOtprema` ← `MestaUtovara`; `cboMagacinskiBroj` ← `MagacinskiBrojCarinski`
- **Funkcionalnosti:**
  - „Izbaci“ — klik → `btnIzbaci_Click`
  - „Nazad“ — klik → `btnMbNazad_Click`
  - „Sačuvaj“ — klik → `btnMbSave_Click` — čita: MagacinskiBrojCarinski; proc: InsertMagacinskiBrojCarinski, UpdateMagacinskiBrojCarinski; preko: InsertSkladista.InsertMagacinskiBrojCarinski, InsertSkladista.UpdateMagacinskiBrojCarinski
  - „Dodaj“ — klik → `btnDodaj_Click`
  - „FORMIRAN RN“ — klik → `btnFormiranRN_Click` — čita: RNCarinskoSkladisteDodatneUsluge; proc: DeleteRnCarinskoSkladisteDodatneUsluge, InsertRnCarinskoSkladisteDodatneUsluge, UpdateRadniNalogSkladista; preko: InsertSkladista.DeleteDodatneUsluge, InsertSkladista.InsertDodatneUsluge, InsertSkladista.UpdateRadniNalog
  - „SNIMI“ — klik → `btnSnimi_Click` — čita: RNCarinskoSkladisteDodatneUsluge, RadniNalogSkladista; proc: DeleteRnCarinskoSkladisteDodatneUsluge, InsertRadniNalogInterniSkladista, InsertRadniNalogSkladista, InsertRnCarinskoSkladisteDodatneUsluge, UpdateRNInterniSkladista, UpdateRadniNalogSkladista; preko: InsertSkladista.DeleteDodatneUsluge, InsertSkladista.InsertDodatneUsluge, InsertSkladista.InsertRNInterni, InsertSkladista.InsertRadniNalog, InsertSkladista.UpdateRNInterni
  - `cboVlasnikRobe` — SelectionChangeCommitted → `cboVlasnikRobe_SelectionChangeCommitted` — čita: Partnerji
  - „?“ — klik → `btnMagacinskiBroj_Click`

#### IzlazneFakture [S]
- **Fajl:** `Pantheon Export/IzlazneFakture.cs` — „Izlazne Fakture“
- **Stari izgled:** Komercijala › CRM › Izlazne fakture → **IzlazneFakture**
- **Tabele:** `Delavci` S, `Faktura` SIU p, `FakturaPostav` SID p, `Izjave` S, `Korisnici` S, `MAticniPodatki` S, `MerskeEnote` S, `NosiociTroskova` S, `Partnerji` S, `UlFak` S, `Valute` S
- **Procedure:** `DeleteFakturaPostav`, `InsertFaktura`, `InsertFakturaPostav`, `UpdateFaktura`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click` — čita: Faktura, faktura, ulfak
  - „tsSave“ — klik → `tsSave_Click` — čita: Partnerji; proc: InsertFaktura, UpdateFaktura; preko: InsertPatheonExport.InstFaktura, InsertPatheonExport.UpdFaktura
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „Obrisi“ — klik → `btnObrisi_Click` — proc: DeleteFakturaPostav; preko: InsertPatheonExport.DelFakturaPostav
  - „Dodaj“ — klik → `button1_Click` — proc: InsertFakturaPostav; preko: InsertPatheonExport.InsFakturaPostav

#### IzlazneFakturePregled [S]
- **Fajl:** `Pantheon Export/IzlazneFakturePregled.cs` — „Pregled Izlaznih faktura“
- **Stari izgled:** Komercijala › CRM › Izlazne fakture pregled → **IzlazneFakturePregled**
- **Otvara:** Valuta („Export Pantheon“), IzlazneFakture („Nova“)
- **Tabele:** `ApiLogovi` I p, `Delavci` S, `Faktura` SUD p, `FakturaPostav` S, `Izjave` S, `MAticniPodatki` S, `MerskeEnote` S, `Najava` U, `NosiociTroskova` S, `Partnerji` S
- **Procedure:** `DeleteFaktura`, `InsertApiLogovi`
- **Funkcionalnosti:**
  - `dataGridView1` — ColumnHeaderMouseClick → `dataGridView1_ColumnHeaderMouseClick` — čita: Delavci, Faktura, FakturaPostav, Izjave, NosiociTroskova, Partnerji
  - „Nova“ — klik → `button1_Click` — otvara: IzlazneFakture
  - „Otvori“ — klik → `button2_Click` — otvara: IzlazneFakture
  - „Get Uplate“ — klik → `btnGetUplate_Click` — piše: Faktura[U]
  - „Export Pantheon“ — klik → `btnExportProd_Click` — otvara: Valuta; čita: Partnerji
  - „Obriši“ — klik → `btnObrisi_Click` — proc: DeleteFaktura; preko: InsertPatheonExport.DeleteFaktura
  - `btnFilterNT` — klik → `btnFilterNT_Click` — čita: Delavci, Faktura, FakturaPostav, Izjave, NosiociTroskova, Partnerji
  - `button3` — klik → `button3_Click` — čita: Delavci, Faktura, FakturaPostav, Izjave, NosiociTroskova, Partnerji
  - `button4` — klik → `button4_Click` — čita: Delavci, Faktura, FakturaPostav, Izjave, NosiociTroskova, Partnerji
  - „Refresh“ — klik → `button5_Click` — čita: Delavci, Faktura, FakturaPostav, Izjave, NosiociTroskova, Partnerji

#### IzvestajVaganja [N+S]
- **Fajl:** `Izvoz/IzvestajVaganja.cs` — „IzvestajVaganja“
- **Novi izgled:** Prijem i otprema kamiona → **frmPrijemIOtpremaKamiona1** → „PLATFORMA“ → **frmPrijemIOtpremaPlatforma** → „Vaga“ → **VaganjePregled** → „Otvori“ → **Vaganje** → „Izveštaj“ → **IzvestajVaganja**
- **Stari izgled:** Terminal › Skladišno poslovanje › VAGANJE PREGLED → **VaganjePregled** → „Otvori“ → **Vaganje** → „Izveštaj“ → **IzvestajVaganja**
- **Funkcionalnosti:**
  - `this` — otvaranje forme → `IzvestajVaganja_Load` — izveštaj: VaganjeRpt.rdlc, VaganjeRptCirada.rdlc

#### IzvozOpredeljenje [S]
- **Fajl:** `Izvoz/IzvozOpredeljenje.cs` — „Kontejneri sa skladista - Izvoz opredeljenje“
- **Stari izgled:** Izvoz › Izvoz › Nerasporedjeni kontejneri Obrazac → **frmIzvoz** → „Kontejner na skladistu“ → **IzvozOpredeljenje**
- **Tabele:** `KontejnerskiTerminali` S, `KontejnerStatus` S, `KontejnerTekuce` S, `Partnerji` S, `Skladista` S
- **Funkcionalnosti:**
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`

#### KomercijalnoSkladiste [N]
- **Fajl:** `Skladista main/KomercijalnoSkladiste.cs` — „CarinskoSkladiste“
- **Novi izgled:** Operacije skladišta → **SkladistaMain** → „Komercijalno skladište“ → **KomercijalnoSkladiste**
- **Otvara:** KomercijalnoSkladistePregled („Prijem robe“)
- **Funkcionalnosti:**
  - „Prijem robe“ — klik → `btnPrijemRobe_Click` — otvara: KomercijalnoSkladistePregled

#### KomercijalnoSkladistePregled [N]
- **Fajl:** `Skladista main/KomercijalnoSkladistePregled.cs` — „KomercijalnoSkladistePregled“
- **Novi izgled:** Operacije skladišta → **SkladistaMain** → „Komercijalno skladište“ → **KomercijalnoSkladiste** → „Prijem robe“ → **KomercijalnoSkladistePregled**
- **Otvara:** frmNalogPrijem („Magacinski predmet“), Otprema („Magacinski predmet“), Prijemnica („Prijemnica“)
- **Tabele:** `Partnerji` S, `RadniNalogInterni` S, `RadniNalogInterniPotvrda` S, `RadniNalogSkladista` S
- **Funkcionalnosti:**
  - „Prijemnica“ — klik → `button25_Click` — otvara: Prijemnica
  - „Magacinski predmet“ — klik → `button24_Click` — otvara: frmNalogPrijem, Otprema; čita: RadniNalogInterni

#### KrajnjeDestinacije [S]
- **Fajl:** `Sifarnici/KrajnjeDestinacije.cs` — „Krajnje destinacije“
- **Stari izgled:** Izvoz › Šifarnici › Krajnje destinacije → **KrajnjeDestinacije**
- **Tabele:** `Drzave` S, `KrajnjaDestinacija` SIUD p
- **Procedure:** `DeleteKrajnjaDestinacija`, `InsertKrajnjaDestinacija`, `UpdateKrajnjaDestinacija`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertKrajnjaDestinacija, UpdateKrajnjaDestinacija; preko: InsertKrajnjaDestinacija.InsKrajnjaDestinacija, InsertKrajnjaDestinacija.UpdKrajnjaDestinacija
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteKrajnjaDestinacija; preko: InsertKrajnjaDestinacija.DelKrajnjaDestinacija
  - `button20` — klik → `tsDelete_Click` — proc: DeleteKrajnjaDestinacija; preko: InsertKrajnjaDestinacija.DelKrajnjaDestinacija
  - `button21` — klik → `tsSave_Click` — proc: InsertKrajnjaDestinacija, UpdateKrajnjaDestinacija; preko: InsertKrajnjaDestinacija.InsKrajnjaDestinacija, InsertKrajnjaDestinacija.UpdKrajnjaDestinacija

#### LogistikaIzvoza0 [N]
- **Fajl:** `MainLeget/Izvoz/LogistikaIzvoza0.cs` — „LogistikaIzvoza0“
- **Novi izgled:** Logistika izvoza → **LogistikaIzvoza1** → „Porudžbenica“ → **LogistikaIzvoza0**
- **Otvara:** frmProdajniNalogIzvoz („Kreiraj novi“), frmPregledPorudzbenica („Izmeni postojeći“)
- **Funkcionalnosti:**
  - „Kreiraj novi“ — klik → `sfButton1_Click`
  - „Izmeni postojeći“ — klik → `sfButton2_Click`
  - „Otkaži postojeći“ — klik → `sfButton3_Click`

#### LogistikaIzvoza1 [N]
- **Fajl:** `MainLeget/Izvoz/LogistikaIzvoza1.cs` — „MainLeget“
- **Novi izgled:** Logistika izvoza → **LogistikaIzvoza1**
- **Otvara:** LogistikaIzvoza0 („Porudžbenica“), LogistikaIzvoza2 („Formiranje naloga za otpremu“)
- **Funkcionalnosti:**
  - „Formiranje naloga za otpremu“ — klik → `btnLogistikaIzvozaPM2_Click` — otvara: LogistikaIzvoza2
  - „Nalog za fakturisanje“ — klik → `btnLogistikaIzvozaPM6_Click`
  - „Formiranje praznog voza“ — klik → `btnLogistikaIzvozaPM5_Click`
  - „Izvoz prazne opreme“ — klik → `btnLogistikaIzvozaPM4_Click`
  - „Prijem i izdavanje robe“ — klik → `btnLogistikaIzvozaPM3_Click`
  - „Porudžbenica“ — klik → `btnLogistikaIzvozaPM1_Click`

#### LogistikaIzvoza2 [N]
- **Fajl:** `MainLeget/Izvoz/LogistikaIzvoza2.cs` — „MainLeget“
- **Novi izgled:** Logistika izvoza → **LogistikaIzvoza1** → „Formiranje naloga za otpremu“ → **LogistikaIzvoza2**
- **Otvara:** frmProdajniNalogIzvozTabela („Kreiraj novi“), frmPregledGrupaKontejneraIzvoz („Izmeni postojeći“)
- **Funkcionalnosti:**
  - „Kreiraj novi“ — klik → `sfButton1_Click_1`
  - „Izmeni postojeći“ — klik → `sfButton2_Click` — otvara: frmPregledGrupaKontejneraIzvoz, frmProdajniNalogIzvozTabela
  - „Otkaži postojeći“ — klik → `sfButton3_Click` — otvara: frmPregledGrupaKontejneraIzvoz
  - „Lista OTVORENIH naloga“ — klik → `sfButton4_Click`
  - „Lista SVIH naloga“ — klik → `sfButton6_Click` — otvara: frmPregledGrupaKontejneraIzvoz

#### MagacinskaKnjigaCarinska [N]
- **Fajl:** `Skladista main/MagacinskaKnjigaCarinska.cs` — „MagacinskaKnjigaCarinska“
- **Novi izgled:** Operacije skladišta → **SkladistaMain** → „Carinsko skladište“ → **CarinskoSkladiste** → „Magacinska knjiga“ → **MagacinskaKnjigaCarinska**
- **Tabele:** `MagacinskaKnjigaCarinska` S

#### MainP [S]
- **Fajl:** `MainP.cs` — „Integrated Logistics Managament system“
- **Otvara:** frmStanice („Stanice“), frmTrase („Trase“), frmPruge („Pruge“), frmLokomotive („Lokomotive“), frmSerijeLokomotiva („Serije lokomotive“), frmStatusVoza („Status vozova“), frmRazlozi („Razlozi stajanja lokomotive“), frmTipPrevoza („Tip prevoza“), frmPartnerji („Partneri“), frmNajava („Poslovi“), frmNajava20 („Poslovi tabla“), frmNajavaArhivaAnaliza („Svi poslovi“), frmAnalizaPorudzbina („Dogovoreno“), frmTeretnicePregled („Teretnice“), frmIskljuceniVagoni („Isključeni vagoni“), frmNajaveBezTeretnice („Teretnice bez najave“), frmPregledRID („RID“), frmRIDPoNajavama („RID po najavama“), frmRIDTeretnice („Teretnice bez RID-a“), frmMapa („Markeri“), frmRadniNalogPregled („Prevoz po trasama“), frmRAdniNalogPregledPoLokomotivama („Prevoz po lokomotivama“), frmStampaRadnogNaloga („Štampa prevoza“), frmRaspustiVagone („Raspuštanje vagoni“), frmMUPDozvola („MUP dozvole“) …
- **Tabele:** `Kartice` S, `KarticeForme` S, `KarticeKorisnik` S, `KarticeMain` S
- **Procedure:** `sp_AzuriranjeOdmora`
- **Funkcionalnosti:**
  - „Integrated information Management system“ — klik → `ribbonControlAdv1_Click`
  - „    Logistika uvoza“ — klik → `backStageTab3_Click`
  - „    Logistika uvoza“ — MouseClick → `backStageTab3_MouseClick`
  - „Integrated Logistics Management system“ — MouseClick → `txtModul_MouseClick`
  - „   Logistika direktnih klijenata“ — klik → `backStageTab1_Click_1`
  - „   Logistika direktnih klijenata“ — MouseClick → `backStageTab1_MouseClick`
  - „backStageButton1“ — klik → `backStageButton1_Click`
  - „Prevozni šifarnici“ — ItemClicked → `toolStripEx1_ItemClicked`
  - „Stanice“ — klik → `toolStripButton1_Click`
  - „Trase“ — klik → `toolStripButton2_Click`
  - „Pruge“ — klik → `toolStripButton3_Click`
  - „Lokomotive“ — klik → `toolStripButton4_Click`
  - „Serije lokomotive“ — klik → `toolStripButton5_Click`
  - „Trase analitički“ — klik → `toolStripButton82_Click`
  - „Serije Vagoni“ — klik → `toolStripButton220_Click` — otvara: frmVagoniSerije
  - „Vuča statusi“ — klik → `toolStripButton233_Click`
  - „Stavke primopredaje“ — klik → `toolStripButton234_Click`
  - „Status vozova“ — klik → `toolStripButton6_Click`
  - „Tip prevoza“ — klik → `toolStripButton7_Click`
  - „Razlozi stajanja lokomotive“ — klik → `toolStripButton8_Click`
  - „NHM“ — klik → `toolStripButton28_Click_1`
  - „Tipovi telegrama“ — klik → `toolStripButton10_Click_1`
  - „Tipovi kontejnera“ — klik → `toolStripButton232_Click` — otvara: frmTipKontejnera
  - „Partneri“ — klik → `toolStripButton11_Click`
  - „Kontakt osobe“ — klik → `toolStripButton175_Click`
  - „Komercijala“ — klik → `toolStripTabItem2_Click`
  - „eFakture“ — klik → `toolStripButton297_Click` — otvara: EFaktura
  - „eOdobrenja“ — klik → `toolStripButton298_Click` — otvara: EOdobrenje
  - „eZaduženja“ — klik → `toolStripButton299_Click` — otvara: EZaduzenje
  - „eAvansni“ — klik → `toolStripButton300_Click` — otvara: EAvansni
  - „Poslata dokumenta“ — klik → `toolStripButton301_Click` — otvara: PoslataDokumenta
  - „Primljena dokumenta“ — klik → `toolStripButton302_Click` — otvara: PrimljenaDokumenta
  - „Opportunity“ — klik → `toolStripButton339_Click`
  - „Pregled dogovora“ — klik → `toolStripButton12_Click_1`
  - „Unos ugovora“ — klik → `toolStripButton36_Click_1`
  - „Dogovoreno“ — klik → `toolStripButton14_Click`
  - „Tehnologija“ — klik → `toolStripButton81_Click`
  - „Tehnologije pregled“ — klik → `toolStripButton83_Click`
  - „Matični podaci“ — klik → `toolStripButton168_Click` — otvara: frmMaticniPodatki
  - „Prodajne grupe“ — klik → `toolStripButton171_Click` — otvara: frmProdajnaGrupa
  - … još 388

#### NalogIzvozaZaOtpremu [N]
- **Fajl:** `MainLeget/Izvoz/NalogIzvozaZaOtpremu.cs` — „NalogIzvozaZaOtpremu“
- **Novi izgled:** Logistika izvoza → **LogistikaIzvoza1** → „Formiranje naloga za otpremu“ → **LogistikaIzvoza2** → „Kreiraj novi“ → **frmProdajniNalogIzvozTabela** → `gridGroupingControl1` (TableControlCellClick) → **NalogIzvozaZaOtpremu**
- **Funkcionalnosti:**
  - „PLATFORMA PRAZAN PUN“ — klik → `btn7_Click`
  - „CERADA  SKLADISTE PUN“ — klik → `btn9_Click`
  - „CERADA PRETOVAR PUN“ — klik → `btn8_Click`
  - „PLATFORMA DIREKTNO PUN“ — klik → `btn13_Click`

#### NalogZaDrumski [N]
- **Fajl:** `MainLeget/Drumski/NalogZaDrumski.cs` — „NalogZaDrumski“
- **Novi izgled:** Drumski transport → **Drumski1** → „Cerada“ → **Cerade** → „NALOG ZA DRUMSKI TRANSPORT“ → **NalogZaDrumski**
- **Otvara:** frmPregledNalogaDrumski („TRANSPORTNI NALOZI IZVOZ“), frmDrumski1 („Kreiraj novi“)
- **Funkcionalnosti:**
  - „Kreiraj novi“ — klik → `btnNovi_Click` — otvara: frmDrumski1
  - „Izmeni postojeći“ — klik → `btnIzmeni_Click` — otvara: frmPregledNalogaDrumski
  - „Otkaži postojeći“ — klik → `btnOtkazi_Click` — otvara: frmPregledNalogaDrumski
  - „TRANSPORTNI NALOZI UVOZ“ — klik → `btnTransportUvoz_Click` — otvara: frmPregledNalogaDrumski
  - „TRANSPORTNI NALOZI IZVOZ“ — klik → `btnTransportIzvoz_Click` — otvara: frmPregledNalogaDrumski
  - „TRANSPORTNI NALOZI DIREKTNI (3P)“ — klik → `btnTransportDirektni_Click` — otvara: frmPregledNalogaDrumski

#### NalogZaKomercijalni [N]
- **Fajl:** `Skladista main/NalogZaKomercijalni.cs` — „NalogZaKomercijalni“
- **Novi izgled:** Operacije skladišta → **SkladistaMain** → kartica „Radni nalozi“ → **NalogZaKomercijalni**
- **Otvara:** PregledKomercijalnihNaloga („SKLADIŠNI NALOZI UVOZ“), TipSkladista („Kreiraj novi“)
- **Funkcionalnosti:**
  - „SKLADIŠNI NALOZI DIREKTNI (3P)“ — klik → `btnTransportDirektni_Click` — otvara: PregledKomercijalnihNaloga
  - „SKLADIŠNI NALOZI IZVOZ“ — klik → `btnTransportIzvoz_Click` — otvara: PregledKomercijalnihNaloga
  - „SKLADIŠNI NALOZI UVOZ“ — klik → `btnTransportUvoz_Click` — otvara: PregledKomercijalnihNaloga
  - „Kreiraj novi“ — klik → `btnNovi_Click` — otvara: TipSkladista

#### NewMain [N]
- **Fajl:** `MainLeget/NewMain.cs` — „MainLeget“
- **Otvara:** LogistikaIzvoza1 („Logistika izvoza“), Podesavanje1 („Podešavanja sistema“), frmHRMeni („HR modul“), Administracija1 („Administracija“), Drumski1 („Drumski transport“), SkladistaMain („Operacije skladišta“), PretovariMain („Operacije pretovari“), PrijemIOtpremaVozova1 („Prijem i otprema vozova“), frmPrijemIOtpremaKamiona1 („Prijem i otprema kamiona“), frmSERMeni („Održavanje“), frmKapija1 („Kapija“), AdministracijaPravoPristupa („!!!“), DepoMain („Depo“), Intermodalni1 („Intermodalni terminal“), GlavniEkran (metoda `ShowHome`)
- **Tabele:** `MainNovi` S, `mainNoviKartice` S, `MainNoviPrava` S
- **Funkcionalnosti:**
  - „INTEGRATED LOGISTICS MANAGEMENT SYSTEM“ — klik → `lblNaslov_Click`
  - „_“ — klik → `btnMinimize_Click`
  - „▢“ — klik → `btnMaximize_Click`
  - „X“ — klik → `btnClose_Click`
  - „!!!“ — klik → `button1_Click` — otvara: AdministracijaPravoPristupa
  - „≡“ — klik → `btnToggleLeftPanel_Click`
  - „Logistika uvoza“ — klik → `btnLogistikaUvoza_Click`
  - „Logistika izvoza“ — klik → `btnLogistikaIzvoza_Click`
  - „Logistika direktnih klijenata“ — klik → `btnLogistikaDirektnih_Click`
  - „Drumski transport“ — klik → `btnDrumski_Click` — otvara: Drumski1
  - „Železnički transport“ — klik → `btnZeleznicki_Click`
  - „Operacije skladišta“ — klik → `btnSkladista_Click`
  - „Intermodalni terminal“ — klik → `btnIntermodalni_Click` — otvara: Intermodalni1
  - „Prijem i otprema vozova“ — klik → `btnPrijemIOtpremaVozova_Click` — otvara: PrijemIOtpremaVozova1
  - „Prijem i otprema kamiona“ — klik → `btnPrijemIOtpremaKamiona_Click` — otvara: frmPrijemIOtpremaKamiona1
  - „Depo “ — klik → `btnDepocnt_Click` — otvara: DepoMain
  - „Operacije pretovari“ — klik → `btnPretovari_Click` — otvara: PretovariMain
  - „PTI“ — klik → `btnPTI_Click`
  - „Održavanje“ — klik → `btnOdrzavanje_Click`
  - „Kapija“ — klik → `btnKapija_Click`
  - „Finansije“ — klik → `btnFinansije_Click`
  - „HR modul“ — klik → `btnHR_Click`
  - „Podešavanja sistema“ — klik → `btnPodesavanja_Click` — otvara: Podesavanje1
  - „Administracija“ — klik → `btnAdministracija_Click`
  - „VSD“ — klik → `sfButton1_Click`
  - „Logout“ — klik → `btnLogout_Click`
  - „Povratak na predhodni meni“ — klik → `BtnNazad_Click`
  - „Početni meni“ — klik → `btnHome_Click`
  - „Dashboard“ — klik → `btnDashboard_Click`

#### NosiociTroskova [S]
- **Fajl:** `Pantheon Export/NosiociTroskova.cs` — „NosiociTroskova“
- **Stari izgled:** Komercijala › CRM › Nosioci troškova → **NosiociTroskova**
- **Tabele:** `ApiLogovi` I p, `Najava` S, `NosiociTroskova` SIU p, `Odeljenja` S, `Opportunity` S, `Partnerji` S, `Predvidjanje` S
- **Procedure:** `DeleteNosiociTroska`, `InsertApiLogovi`, `InsertNosiociTroskova`, `RefreshPredvidjanja`, `UpdateNosiociTroska`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click` — čita: NosiociTroskova
  - „tsSave“ — klik → `tsSave_Click` — čita: NosiociTroskova, Predvidjanje; proc: InsertNosiociTroskova, RefreshPredvidjanja, UpdateNosiociTroska; preko: InsertPatheonExport.InsNosiociTroskova, InsertPatheonExport.RefreshPredvidjanje, InsertPatheonExport.UpdNosiociTroskova
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteNosiociTroska; preko: InsertPatheonExport.DelNosiociTroskova
  - „Pregled NT“ — klik → `toolStripButton2_Click` — čita: NosiociTroskova, Odeljenja, Opportunity, Partnerji
  - „Export Pantheon“ — klik → `toolStripButton3_Click` — piše: NosiociTroskova[U]; proc: InsertApiLogovi; preko: InsertPatheonExport.InsApiLog
  - `dataGridView2` — ColumnHeaderMouseClick → `dataGridView2_ColumnHeaderMouseClick` — čita: NosiociTroskova, Odeljenja, Opportunity, Partnerji
  - „Nazad“ — klik → `button1_Click`
  - `cboOpportunity` — SelectionChangeCommitted → `cboOpportunity_SelectionChangeCommitted` — čita: Opportunity
  - `dataGridView1` — ColumnHeaderMouseClick → `dataGridView1_ColumnHeaderMouseClick` — čita: NosiociTroskova, Odeljenja, Opportunity, Partnerji

#### Obavestenje [S]
- **Fajl:** `Administracija/Obavestenje.cs` — „Obavestenje“
- **Stari izgled:** Administrator › Dodela prava › Forme Prava → **Prava** → „Dodeli prava“ → **Obavestenje**

#### ObradaUvoznihUsluga [S]
- **Fajl:** `Finansije/ObradaUvoznihUsluga.cs` — „Obrada uvoznih usluga“
- **Stari izgled:** Uvoz › FINANSIJE › Obrada uvoznih usluga → **ObradaUvoznihUsluga**
- **Tabele:** `Partnerji` S, `Scenario` S, `TipKontenjera` S, `UvozKonacna` S, `UvozKonacnaVrstaManipulacije` S, `UvozKonacnaZaglavlje` S, `Voz` S, `VrstaManipulacije` S
- **Procedure:** `UpdateUvozKonacnaManFaktura`
- **Funkcionalnosti:**
  - „Proglasi fakturisano“ — klik → `button24_Click` — proc: UpdateUvozKonacnaManFaktura; preko: InsertUvozKonacna.UpdUvozKonacnaManfaktura
  - „Refresh usluge“ — klik → `button1_Click`

#### Obrasci [S]
- **Fajl:** `Testiranje/Obrasci.cs` — „Obrasci“
- **Stari izgled:** Kadrovi › Testiranje radnika › Posebni obrasci → **Obrasci**
- **Tabele:** `Delavci` S
- **Funkcionalnosti:**
  - „Štampaj obrazac  br 6“ — klik → `sfButton1_Click`
  - `this` — otvaranje forme → `Obrasci_Load` — čita: Delavci

#### Opportunity [S]
- **Fajl:** `Pantheon Export/Opportunity.cs` — „Opportunity“
- **Stari izgled:** Komercijala › Ugovori › Opportunity → **Opportunity**
- **Tabele:** `Odeljenja` S, `Opportunity` SIUD p, `Partnerji` S, `Stanice` S, `Valute` S
- **Procedure:** `DeleteOpportunity`, `InsertOpportunity`, `UpdateOpportunity`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click` — čita: Opportunity
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertOpportunity, UpdateOpportunity; preko: InsertPatheonExport.InsOpportunity, InsertPatheonExport.UpdOpportunity
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteOpportunity; preko: InsertPatheonExport.DelOpportunity
  - „Opportunity-Won“ — klik → `toolStripButton1_Click` — čita: Odeljenja, Opportunity, Partnerji, Stanice
  - „Opportunity-Lost“ — klik → `toolStripButton2_Click` — čita: Odeljenja, Opportunity, Partnerji, Stanice
  - „Naziv posla“ — klik → `label1_Click`
  - „Valuta“ — klik → `label10_Click`
  - „Izaberi“ — klik → `button1_Click` — čita: Odeljenja, Opportunity, Partnerji, Stanice

#### Otprema [N]
- **Fajl:** `Skladista main/Dokumenta/Otprema.cs` — „Otprema“
- **Novi izgled:** Operacije skladišta → **SkladistaMain** → kartica „Radni nalozi“ → **NalogZaKomercijalni** → „SKLADIŠNI NALOZI UVOZ“ → **PregledKomercijalnihNaloga** → „Otvori“ → **Otprema**
- **Otvara:** OtpremaLager („IZABERI SA LAGERA“), Otpremnica („IDI NA OTPREMNICU“)
- **Tabele:** `Carinarnice` S, `Lager` S, `MagacinskiBrojCarinski` SIU p, `MestaUtovara` S, `Partnerji` S, `PomOstali` S, `Primljeno` S, `Promet` S, `RadniNalogSkladista` SIU p, `RNCarinskoPrijemnicaStavke` S, `RNCarinskoSkladisteDodatneUsluge` SID p, `RNCarinskoSkladisteOtpremaStavkePom` SID p, `RNCarinskoSkladisteOtpremnica` S, `RNCarinskoSkladisteOtpremnicaStavke` S, `RNCarinskoSkladistePrijemnica` S, `Skladista` S, `TipPalete` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaVozila` S, `VrstePrevoznogSredstva` S, `Zavrseno` S
- **Procedure:** `DeleteRnCarinskoSkladisteDodatneUsluge`, `InsertMagacinskiBrojCarinski`, `InsertRadniNalogSkladista`, `InsertRnCarinskoSkladisteDodatneUsluge`, `InsertRnCarinskoSkladisteOtpremaStavkePom`, `UpdateMagacinskiBrojCarinski`, `UpdateRadniNalogSkladista`
- **Combobox → tabela:** `cboDodatneUsluge` ← `VrstaManipulacije`; `cboNalogodavac` ← `Partnerji`; `cboVlasnikRobe` ← `Partnerji`; `cboCarinskiPostupak` ← `VrstaCarinskogPostupka`; `cboSpediterOtprema` ← `Partnerji`; `cboTipTransportaOtprema` ← `VrstePrevoznogSredstva`; `cboVrstaKamionaOtprema` ← `VrstaVozila`; `cboCarinarnicaOtprema` ← `Carinarnice`; `cboMestoIstovaraOtprema` ← `MestaUtovara`; `cboMagacinskiBroj` ← `MagacinskiBrojCarinski`, `RNCarinskoSkladistePrijemnica`, `RadniNalogSkladista`
- **Funkcionalnosti:**
  - „Izbaci“ — klik → `btnIzbaci_Click`
  - „Nazad“ — klik → `btnMbNazad_Click`
  - „Sačuvaj“ — klik → `btnMbSave_Click` — čita: MagacinskiBrojCarinski; proc: InsertMagacinskiBrojCarinski, UpdateMagacinskiBrojCarinski; preko: InsertSkladista.InsertMagacinskiBrojCarinski, InsertSkladista.UpdateMagacinskiBrojCarinski
  - „Dodaj“ — klik → `btnDodaj_Click`
  - „IDI NA OTPREMNICU“ — klik → `btnOtpremnica_Click` — otvara: Otpremnica
  - „FORMIRAN RN“ — klik → `btnFormiranRN_Click` — piše: RNCarinskoSkladisteOtpremaStavkePom[D]; čita: RNCarinskoSkladisteDodatneUsluge; proc: DeleteRnCarinskoSkladisteDodatneUsluge, InsertRnCarinskoSkladisteDodatneUsluge, InsertRnCarinskoSkladisteOtpremaStavkePom, UpdateRadniNalogSkladista; preko: InsertSkladista.DeleteDodatneUsluge, InsertSkladista.InsertDodatneUsluge, InsertSkladista.InsertOtpremaStavke, InsertSkladista.UpdateRadniNalog
  - „SNIMI“ — klik → `btnSnimi_Click` — piše: RNCarinskoSkladisteOtpremaStavkePom[D]; čita: RNCarinskoSkladisteDodatneUsluge, RadniNalogSkladista; proc: DeleteRnCarinskoSkladisteDodatneUsluge, InsertRadniNalogSkladista, InsertRnCarinskoSkladisteDodatneUsluge, InsertRnCarinskoSkladisteOtpremaStavkePom, UpdateRadniNalogSkladista; preko: InsertSkladista.DeleteDodatneUsluge, InsertSkladista.InsertDodatneUsluge, InsertSkladista.InsertOtpremaStavke, InsertSkladista.InsertRadniNalog, InsertSkladista.UpdateRadniNalog
  - `cboVlasnikRobe` — SelectionChangeCommitted → `cboVlasnikRobe_SelectionChangeCommitted` — čita: Partnerji
  - „?“ — klik → `btnMagacinskiBroj_Click`
  - `cboMagacinskiBroj` — SelectionChangeCommitted → `cboMagacinskiBroj_SelectionChangeCommitted` — čita: RNCarinskoSkladistePrijemnica, RadniNalogSkladista
  - „?“ — klik → `btnPrijemnica_Click`
  - „Nazad“ — klik → `btnPrijemnicaNazad_Click`
  - „Potvrdi“ — klik → `btnPotvrdiPrijemnicu_Click` — čita: RadniNalogSkladista
  - „gridGroupingControl1“ — klik → `gridGroupingControl1_Click`
  - „RAZDUŽI SVE“ — klik → `btnRazduzi_Click` — čita 12 tabela
  - „IZABERI SA LAGERA“ — klik → `button1_Click`
  - „Izbaci“ — klik → `btnIzbaciStavku_Click`
  - „Izmeni“ — klik → `btnIzmeniStavku_Click`
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`

#### OtpremaLager [N]
- **Fajl:** `Skladista main/OtpremaLager.cs` — „OtpremaLager“
- **Novi izgled:** Operacije skladišta → **SkladistaMain** → kartica „Radni nalozi“ → **NalogZaKomercijalni** → „SKLADIŠNI NALOZI UVOZ“ → **PregledKomercijalnihNaloga** → „Otvori“ → **Otprema** → „IZABERI SA LAGERA“ → **OtpremaLager**
- **Tabele:** `MagacinskiBrojCarinski` S, `Partnerji` S, `PomOstali` S, `Primljeno` S, `Promet` S, `RadniNalogSkladista` S, `RNCarinskoPrijemnicaStavke` S, `RNCarinskoSkladisteOtpremaStavkePom` S, `RNCarinskoSkladisteOtpremnica` S, `RNCarinskoSkladisteOtpremnicaStavke` S, `RNCarinskoSkladistePrijemnica` S, `Skladista` S, `Stock` S, `TipPalete` S, `Zavrseno` S
- **Combobox → tabela:** `cboMB` ← `MagacinskiBrojCarinski`, `Promet`, `RNCarinskoPrijemnicaStavke`, `RNCarinskoSkladistePrijemnica`, `RadniNalogSkladista`; `cboVlasnik` ← `Partnerji`, `Promet`, `RNCarinskoPrijemnicaStavke`, `RNCarinskoSkladistePrijemnica`, `RadniNalogSkladista`; `cboMB` ← `MagacinskiBrojCarinski`, `Partnerji`, `Promet`, `RNCarinskoPrijemnicaStavke`, `RNCarinskoSkladistePrijemnica`, `RadniNalogSkladista`
- **Funkcionalnosti:**
  - „KREIRAJ“ — klik → `btnKreiraj_Click`
  - „ODUSTANI“ — klik → `btnOdustani_Click`
  - „Izbaci“ — klik → `btnIzbaciStavku_Click`
  - „Izmeni“ — klik → `btnIzmeniStavku_Click`

#### Otpremnica [N]
- **Fajl:** `Skladista main/Dokumenta/Otpremnica.cs` — „Otpremnica“
- **Novi izgled:** Operacije skladišta → **SkladistaMain** → kartica „Radni nalozi“ → **NalogZaKomercijalni** → „SKLADIŠNI NALOZI UVOZ“ → **PregledKomercijalnihNaloga** → „Otvori“ → **Otprema** → „IDI NA OTPREMNICU“ → **Otpremnica**
- **Tabele:** `Carinarnice` S, `Delavci` S, `MagacinskiBrojCarinski` S, `MestaUtovara` S, `Partnerji` S, `RadniNalogSkladista` S, `RNCarinskoSkladisteDodatneUsluge` S, `RNCarinskoSkladisteOtpremaStavkePom` S, `RNCarinskoSkladisteOtpremnica` SIU p, `RNCarinskoSkladisteOtpremnicaStavke` S, `RNCarinskoSkladistePrijemnica` S, `RNCarinskoSkladisteRukovalac` S, `TipPalete` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaVozila` S, `VrstePrevoznogSredstva` S
- **Procedure:** `InsertRNCarinskoRukovalac`, `InsertRNCarinskoSkladisteOtpremnica`, `UpdateRNCarinskoSkladisteOtpremnica`
- **Combobox → tabela:** `cboMagacinskiBroj` ← `MagacinskiBrojCarinski`, `RNCarinskoSkladistePrijemnica`, `RadniNalogSkladista`; `cboDodatneUsluge` ← `VrstaManipulacije`; `cboNalogodavac` ← `Partnerji`; `cboVlasnikRobe` ← `Partnerji`; `cboCarinskiPostupak` ← `VrstaCarinskogPostupka`; `cboSpediterOtprema` ← `Partnerji`; `cboTipTransportaOtprema` ← `VrstePrevoznogSredstva`; `cboVrstaKamionaOtprema` ← `VrstaVozila`; `cboCarinarnicaOtprema` ← `Carinarnice`; `cboMestoIstovaraOtprema` ← `MestaUtovara`; `cboRukovalac` ← `Delavci`
- **Funkcionalnosti:**
  - „Izbaci“ — klik → `btnIzbaci_Click`
  - „Dodaj“ — klik → `btnDodaj_Click`
  - „NAPOMENA“ — klik → `label27_Click`
  - „?“ — klik → `btnMagacinskiBroj_Click`
  - „?“ — klik → `btnPrijemnica_Click`
  - „Nazad“ — klik → `btnVratiNalog_Click`
  - „Izdaj nalog“ — klik → `btnIzdajNalog_Click` — čita: RNCarinskoSkladisteDodatneUsluge, RNCarinskoSkladisteRukovalac; proc: InsertRNCarinskoRukovalac; preko: InsertSkladista.InsertNalogRukovalac
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - „Nalog za istovar rukovaocima“ — klik → `btnNalog_Click`
  - „Snimi“ — klik → `btnSnimi_Click` — čita: RNCarinskoSkladisteOtpremnica; proc: InsertRNCarinskoSkladisteOtpremnica, UpdateRNCarinskoSkladisteOtpremnica; preko: InsertSkladista.InsertCarinskaOtpremnica, InsertSkladista.UpdateCarinskaOtpremnica

#### Otpremnica [N+S]
- **Fajl:** `RadniNalozi/Otpremnica.cs` — „Otpremnica“
- **Novi izgled:** Operacije pretovari → **PretovariMain** → „Pretovar“ → **PretovariPrijem** → „Prijem“ → **frmPretovarPrijem** → „Radni nalog pretovar“ → **Otpremnica**
- **Stari izgled:** Terminal › Terminalske usluge › ZADACI IPU DISPEČER → **PlaniraniPretovar** → „Formiraj otpremnicu robe“ → **Otpremnica**
- **Otvara:** OtpremnicaPregled („Otpremnice pregled“)
- **Tabele:** `Delavci` S, `DObavnica` I p, `DobavnicaPostav` I p, `Lokac` S, `MAticniPodatki` S, `Mesta` S, `Narocilo` S, `NarociloPostav` S, `NHM` S, `Partnerji` S, `Pozicija` S, `Promet` SI p, `Sklad` S, `Skladista` S, `Skladiste_Korisnik` S, `Vozila` S
- **Procedure:** `InsertDobavnica`, `InsertDobavnicaPostav`, `InsertPromet`, `PromenaBrojaKontejnera`
- **Combobox → tabela:** `cbo_Skladiste` ← `Lokac`, `Pozicija`, `Sklad`, `Skladista`; `cbo_Partner` ← `Partnerji`; `comboBox1` ← `Delavci`; `cboVozac` ← `Delavci`; `cboVozilo` ← `Vozila`; `cbo_MestoTroska` ← `Mesta`
- **Funkcionalnosti:**
  - „Izbaci“ — klik → `btnIzbaci_Click`
  - „Dodaj“ — klik → `btnDodaj_Click`
  - „NAPOMENA“ — klik → `label27_Click`
  - „?“ — klik → `btnMagacinskiBroj_Click`
  - „?“ — klik → `btnPrijemnica_Click`
  - „Nazad“ — klik → `btnVratiNalog_Click`
  - „Izdaj nalog“ — klik → `btnIzdajNalog_Click`
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - „Nalog za istovar rukovaocima“ — klik → `btnNalog_Click`
  - „Snimi“ — klik → `btnSnimi_Click`
  - `this` — otvaranje forme → `Otpremnica_Load` — čita: NHM, Promet
  - `button1` — klik → `button1_Click` — otvara: OtpremnicaPregled; čita: Promet; proc: InsertDobavnica, InsertDobavnicaPostav, InsertPromet; preko: InsertIsporuka.InsertDobavnica, InsertIsporuka.InsertDobavnicaPostav, InsertIsporuka.InsertPromet
  - „OPIS ROBE“ — klik → `label6_Click`
  - `btn_Povuci` — klik → `btn_Povuci_Click` — čita: Narocilo, Skladiste_Korisnik, sklad
  - „OSTALA SKLADIŠTA“ — klik → `label6_Click`
  - „BROJ TELEFONA“ — klik → `label6_Click`
  - `button3` — klik → `button3_Click_1` — otvara: OtpremnicaPregled
  - `button4` — klik → `button4_Click` — proc: PromenaBrojaKontejnera; preko: InsertIsporuka.PromenaBrojaKontejnera
  - `button23` — klik → `button3_Click_1` — otvara: OtpremnicaPregled
  - `button22` — klik → `button1_Click` — otvara: OtpremnicaPregled; čita: Promet; proc: InsertDobavnica, InsertDobavnicaPostav, InsertPromet; preko: InsertIsporuka.InsertDobavnica, InsertIsporuka.InsertDobavnicaPostav, InsertIsporuka.InsertPromet
  - `button5` — klik → `button4_Click` — proc: PromenaBrojaKontejnera; preko: InsertIsporuka.PromenaBrojaKontejnera

#### OtpremnicaPregled [N+S]
- **Fajl:** `RadniNalozi/OtpremnicaPregled.cs`
- **Novi izgled:** Operacije pretovari → **PretovariMain** → „Pretovar“ → **PretovariPrijem** → „Prijem“ → **frmPretovarPrijem** → „Radni nalog pretovar“ → **Otpremnica** → „Otpremnice pregled“ → **OtpremnicaPregled**
- **Stari izgled:** Terminal › Skladišno poslovanje › OTPREMA ROBE PRETOVAR → **OtpremnicaPregled**
- **Tabele:** `DObavnica` S, `DobavnicaPostav` S, `Partnerji` S, `Pozicija` S, `Promet` S, `Skladista` S
- **Funkcionalnosti:**
  - `button1` — klik → `button1_Click` — izveštaj: rptOtpremnica.rdlc

#### PakovanjeKamiona1 [N+S]
- **Fajl:** `Drumski/PakovanjeKamiona1.cs` — „PakovanjeKamiona1“
- **Novi izgled:** Drumski transport → **Drumski1** → „Platforme“ → **Platforma** → „FORMIRANJE\r\nTRANSPORTNOG NALOGA“ → **PakovanjeKamiona1**
- **Stari izgled:** Drumski › Drumski › Pakovanje kamiona → **PakovanjeKamiona1**
- **Otvara:** frmPregledPorukeVozacu (`dataGridView3` (CellClick)), frmPregledFajlova (`dataGridView3` (CellClick)), frmPregledNalogaDrumski („Pregled naloga“), frmDrumski1 („Kreiraj nov nalog“), frmListaProtokola („PROTOKOL“)
- **Tabele:** `Automobili` S, `AutomobiliTehnickiProblem` S, `Carinarnice` S, `Delavci` S, `DrumskiPozicioniranje` S, `FakturaDrumski` SI p, `Izvoz` S, `IzvozKonacna` S, `KontejnerskiTerminali` S, `Korisnici` S, `MapiranjeTipaNaloga` S, `MestaUtovara` S, `NapomenaZaPozicioniranje` S, `Partnerji` S, `partnerjiKontOsebaMU` S, `ProtokolRadniNalogDrumski` S, `RadniNalogDrumski` S, `RadniNalogInterni` S, `ScenarijaTokaTransporta` S, `ScenarioTokTransporta_Statusi` S, `SistemskePostavke` S, `StatusVozila` S, `TipKontenjera` S, `UploadTokens` S, `Uvoz` S, `UvozKonacna` S, `VrstaManipulacije` S, `VrstaVozila` S
- **Procedure:** `ArhiviranRadniNalogDrumski`, `DodeliKamionZaRadniNalogDrumskiP`, `DodeliKamionZaViseRadnihNalogaDrumskiP`, `InsertDokumentaRadniNalogDrumski`, `InsertFakturaDrumski`, `UpdateRadniNalogDrumskiListaKamionID`, `UpdateRadniNalogDrumskiPoslataNajava`, `UpdateRadniNalogInterniZavrsen`, `UpdateStatusRadniNalogDrumski`
- **Combobox → tabela:** `cboTipVozila` ← `VrstaVozila`; `cboPrevoznik` ← `Partnerji`; `cboRegistracija` ← `Automobili`
- **Funkcionalnosti:**
  - „R“ — čekiranje → `chkR_CheckedChanged`
  - „N“ — čekiranje → `chkR_CheckedChanged`
  - „Osveži prikaz“ — klik → `btnResetuj_Click`
  - „Pregled naloga“ — klik → `toolStripItemPregledNaloga_Click` — otvara: frmPregledNalogaDrumski
  - „Ukloni dodeljen kamion“ — klik → `ukloniDodeljenKamionToolStripMenuItem_Click`
  - „Kreiraj nov nalog“ — klik → `btnKreiraj_Click` — otvara: frmDrumski1
  - `dataGridView3` — promena vrednosti → `dataGridView3_CellValueChanged` — proc: ArhiviranRadniNalogDrumski, UpdateRadniNalogInterniZavrsen, UpdateStatusRadniNalogDrumski; preko: InsertRadniNalogDrumski.ArhiviranRadniNalogDrumski, InsertRadniNalogDrumski.UpdateStatusRadniNalogDrumski, InsertRadniNalogInterni.UpdRadniNalogInterniZavrsen
  - „ARHIVIRAJ“ — klik → `btnArhiva_Click` — proc: ArhiviranRadniNalogDrumski; preko: InsertRadniNalogDrumski.ArhiviranRadniNalogDrumski
  - „UPARI KAMION I KONTEJNER“ — klik → `button2_Click` — proc: DodeliKamionZaRadniNalogDrumskiP, DodeliKamionZaViseRadnihNalogaDrumskiP; preko: InsertRadniNalogDrumski.DodeliKamionP
  - „VRATI U NERASPOREĐENE“ — klik → `button1_Click` — proc: UpdateRadniNalogDrumskiListaKamionID; preko: InsertRadniNalogDrumski.VratiKamionUNerasporedjeneLista
  - „NAJAVA VOZILA“ — klik → `btnNajava_Click` — proc: InsertFakturaDrumski, UpdateRadniNalogDrumskiPoslataNajava; preko: InsertFakture.InsFaktura, InsertRadniNalogDrumski.UpdateRadniNalogDrumskiPoslataNajava
  - „PROTOKOL“ — klik → `button3_Click` — otvara: frmListaProtokola
  - `dataGridView3` — klik na ćeliju → `dataGridView3_CellContentClick` — otvara: frmPregledPorukeVozacu, frmPregledFajlova; proc: InsertDokumentaRadniNalogDrumski; preko: InsertRadniNalogDrumski.SnimiUFajlBazu

#### PakovanjeKamionaCerade [N]
- **Fajl:** `Drumski/PakovanjeKamionaCerade.cs` — „PakovanjeKamionaCerade“
- **Novi izgled:** Drumski transport → **Drumski1** → „Cerada“ → **Cerade** → „FORMIRANJE\r\nTRANSPORTNOG NALOGA“ → **PakovanjeKamionaCerade**
- **Otvara:** frmPregledPorukeVozacu (`dataGridView3` (CellClick)), frmPregledFajlova (`dataGridView3` (CellClick)), frmPregledNalogaDrumski („Pregled naloga“), frmDrumski1 („Kreiraj nov nalog“), frmListaProtokola („PROTOKOL“)
- **Tabele:** `Automobili` S, `AutomobiliTehnickiProblem` S, `Carinarnice` S, `Delavci` S, `DrumskiPozicioniranje` S, `FakturaDrumski` SI p, `Izvoz` S, `IzvozKonacna` S, `Korisnici` S, `MapiranjeTipaNaloga` S, `MestaUtovara` S, `NapomenaZaPozicioniranje` S, `Partnerji` S, `partnerjiKontOsebaMU` S, `ProtokolRadniNalogDrumski` S, `RadniNalogDrumski` S, `RadniNalogInterni` S, `ScenarijaTokaTransporta` S, `ScenarioTokTransporta_Statusi` S, `SistemskePostavke` S, `StatusVozila` S, `TipKontenjera` S, `UploadTokens` S, `Uvoz` S, `UvozKonacna` S, `VrstaManipulacije` S, `VrstaVozila` S
- **Procedure:** `ArhiviranRadniNalogDrumski`, `DodeliKamionZaRadniNalogDrumskiP`, `DodeliKamionZaViseRadnihNalogaDrumskiP`, `InsertDokumentaRadniNalogDrumski`, `InsertFakturaDrumski`, `UpdateRadniNalogDrumskiListaKamionID`, `UpdateRadniNalogDrumskiPoslataNajava`, `UpdateRadniNalogInterniZavrsen`, `UpdateStatusRadniNalogDrumski`
- **Combobox → tabela:** `cboTipVozila` ← `VrstaVozila`; `cboPrevoznik` ← `Partnerji`; `cboRegistracija` ← `Automobili`
- **Funkcionalnosti:**
  - „R“ — čekiranje → `chkR_CheckedChanged`
  - „N“ — čekiranje → `chkR_CheckedChanged`
  - „Osveži prikaz“ — klik → `btnResetuj_Click`
  - „Pregled naloga“ — klik → `toolStripItemPregledNaloga_Click` — otvara: frmPregledNalogaDrumski
  - „Ukloni dodeljen kamion“ — klik → `ukloniDodeljenKamionToolStripMenuItem_Click`
  - „Kreiraj nov nalog“ — klik → `btnKreiraj_Click` — otvara: frmDrumski1
  - `dataGridView3` — promena vrednosti → `dataGridView3_CellValueChanged` — proc: ArhiviranRadniNalogDrumski, UpdateRadniNalogInterniZavrsen, UpdateStatusRadniNalogDrumski; preko: InsertRadniNalogDrumski.ArhiviranRadniNalogDrumski, InsertRadniNalogDrumski.UpdateStatusRadniNalogDrumski, InsertRadniNalogInterni.UpdRadniNalogInterniZavrsen
  - „ARHIVIRAJ“ — klik → `btnArhiva_Click` — proc: ArhiviranRadniNalogDrumski; preko: InsertRadniNalogDrumski.ArhiviranRadniNalogDrumski
  - „UPARI VOZILO I NALOG“ — klik → `button2_Click` — proc: DodeliKamionZaRadniNalogDrumskiP, DodeliKamionZaViseRadnihNalogaDrumskiP; preko: InsertRadniNalogDrumski.DodeliKamionP
  - „VRATI U NERASPOREĐENE“ — klik → `button1_Click` — proc: UpdateRadniNalogDrumskiListaKamionID; preko: InsertRadniNalogDrumski.VratiKamionUNerasporedjeneLista
  - „NAJAVA VOZILA“ — klik → `btnNajava_Click` — proc: InsertFakturaDrumski, UpdateRadniNalogDrumskiPoslataNajava; preko: InsertFakture.InsFaktura, InsertRadniNalogDrumski.UpdateRadniNalogDrumskiPoslataNajava
  - „PROTOKOL“ — klik → `button3_Click` — otvara: frmListaProtokola
  - `dataGridView3` — klik na ćeliju → `dataGridView3_CellContentClick` — otvara: frmPregledPorukeVozacu, frmPregledFajlova; proc: InsertDokumentaRadniNalogDrumski; preko: InsertRadniNalogDrumski.SnimiUFajlBazu

#### PaletePrebacivanje [N]
- **Fajl:** `Skladista main/PaletePrebacivanje.cs` — „PaletePrebacivanje“
- **Novi izgled:** Operacije skladišta → **SkladistaMain** → „Carinsko skladište“ → **CarinskoSkladiste** → „Prijem robe“ → **CarinskoSkladistePregled** → „Prijemnica“ → **Prijemnica** → „Prebaci na palete“ → **PaletePrebacivanje**

#### Pitanja [S]
- **Fajl:** `Testiranje/Pitanja.cs` — „Pitanja“
- **Stari izgled:** Kadrovi › Testiranje radnika › Formiranje testa → **Pitanja**
- **Tabele:** `Delavci` S, `testovi` S, `testovipitanja` S, `TestoviPitanjaOdgovor` S
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click`
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „Promeni“ — klik → `sfButton5_Click`
  - „Izbaci“ — klik → `btnIzbaciTrasa_Click`
  - „Unesi“ — klik → `btnUnesiTrasa_Click`
  - „Unesi“ — klik → `sfButton1_Click`
  - „Izbaci“ — klik → `sfButton2_Click`
  - `this` — otvaranje forme → `Pitanja_Load` — čita: Delavci

#### PlaniraniPretovar [S]
- **Fajl:** `Uvoz/PlaniraniPretovar.cs` — „PRETOVAR KONTEJNERA“
- **Stari izgled:** Terminal › Terminalske usluge › ZADACI IPU DISPEČER → **PlaniraniPretovar**
- **Otvara:** Prijemnica („PRETOVAR ROBE - PRIJEM“), Otpremnica („Formiraj otpremnicu robe“), frmPrijemKontejneraKamionLegetUvoz („OTVORI PP“), frmPrijemKontejneraKamionLegetIzvoz („OTVORI PP“), RN12MedjuskladisniKontejnera („PLANIRANI PRETOVAR - POSTAVKA KONTEJNERA“), frmDodatneUsluge („Evidencija dodatnih usluga“)
- **Tabele:** `IzvozKonacna` S, `IzvozKonacnaVrstaManipulacije` S, `KontejnerStatus` S, `KontejnerTekuce` S, `KontejnerTekuceOperacije` SI p, `organizacioneJedinice` S, `OtpremaKontejnera` S, `Partnerji` S, `Pozicija` S, `PrijemKontejneraVoz` S, `Promet` S, `RadniNalogInterni` S, `RNOtpremaCirade` S, `RNPrijemCirade` S, `Scenario` S, `Skladista` S, `TipKontenjera` S, `UvozKonacna` S, `UvozKonacnaVrstaManipulacije` S, `VrstaManipulacije` S
- **Procedure:** `InsertKontejnerTekuceOperacije`, `InsertRN6OtPlatFormeKam`, `InsertRN8OtpremaCiradeKam`, `InsertRN8OtpremaCiradeKamIzvoz`, `PokreniSkladisninuKontejnera`, `spPrenesiKontejnerIzPlanaNaPrijemnicu`, `spPrenesiKontejnerIzPlanaNaPrijemnicuIzvoz`, `UpdateRadniNalogInterniGenerisan`, `UpdateRadniNalogInterniZavrsen`, `UpdateUvozKonacnaPK`, `UpdateUvozKonacnaUC`, `ZatvoriSkladisninuKontejnera`
- **Funkcionalnosti:**
  - „PLANIRANI PRETOVARI KON CIRADA“ — klik → `button1_Click` — proc: UpdateRadniNalogInterniGenerisan, spPrenesiKontejnerIzPlanaNaPrijemnicu, spPrenesiKontejnerIzPlanaNaPrijemnicuIzvoz; preko: InsertRadniNalogInterni.UpdRadniNalogInterniGenerisan, InsertUvozKonacna.PrenesiKontejnerIzPlanaNaPrijemnicu, InsertUvozKonacna.PrenesiKontejnerIzPlanaNaPrijemnicuIzvoz
  - „PRETOVAR ROBE - PRIJEM“ — klik → `button2_Click` — otvara: Prijemnica
  - „Formiraj otpremnicu kontejnera“ — klik → `btnOtpremnica_Click`
  - „Formiraj otpremnicu robe“ — klik → `btnOtpremnicaRoba_Click` — otvara: Otpremnica
  - „Skladisnina kontejnera pokreni“ — klik → `button3_Click` — proc: PokreniSkladisninuKontejnera; preko: InsertRN.PokreniSkladisninuKontejnera
  - „Skladisnina KONTEJNERA zatvori“ — klik → `button4_Click` — proc: ZatvoriSkladisninuKontejnera; preko: InsertRN.ZatvoriSkladisninuKontejnera
  - „ZAVRŠEN PRETOVAR“ — klik → `button6_Click` — proc: InsertRN6OtPlatFormeKam, InsertRN8OtpremaCiradeKam, InsertRN8OtpremaCiradeKamIzvoz, UpdateRadniNalogInterniGenerisan, spPrenesiKontejnerUOtpremuKamionom, spPrenesiKontejnerUOtpremuKamionomIzvoz; preko: InsertIzvozKonacna.PrenesiKontejnerUOtpremuKamionomIzvoz, InsertIzvozKonacna.PrenesiKontejnerUOtpremuKamionomUvoz, InsertRN.InsRN6OtpremaPlatformeKam, InsertRN.InsRN8OtpremaCiradeKam, InsertRN.InsRN8OtpremaCiradeKamIzvoz
  - „OTVORI PP“ — klik → `button7_Click`
  - „RN PC“ — klik → `button8_Click`
  - „PLANIRANI PRETOVARI KONT- RN KALMARISTA“ — klik → `button9_Click` — proc: UpdateRadniNalogInterniGenerisan; preko: InsertRadniNalogInterni.UpdRadniNalogInterniGenerisan
  - „Refresh“ — klik → `toolStripButton6_Click`
  - „Evidencija dodatnih usluga“ — klik → `toolStripButton1_Click` — otvara: frmDodatneUsluge
  - „Otvori prijem“ — klik → `toolStripButton4_Click` — otvara: Prijemnica
  - „Otvori otpremu“ — klik → `toolStripButton3_Click` — otvara: Otpremnica
  - „Uradila carina“ — klik → `toolStripButton5_Click` — proc: UpdateUvozKonacnaUC; preko: InsertUvozKonacna.UpdUvozKonacnaUC
  - „Potvrdio klijent“ — klik → `toolStripButton2_Click` — proc: UpdateUvozKonacnaPK; preko: InsertUvozKonacna.UpdUvozKonacnaPK
  - „Z“ — klik → `button21_Click` — proc: UpdateRadniNalogInterniZavrsen; preko: InsertRadniNalogInterni.UpdRadniNalogInterniZavrsen
  - „Z“ — klik → `button20_Click` — proc: UpdateRadniNalogInterniZavrsen; preko: InsertRadniNalogInterni.UpdRadniNalogInterniZavrsen
  - „KALMARISTA PREMESTAJ KONTEJNERA / CIR“ — klik → `button6_Click` — proc: InsertRN6OtPlatFormeKam, InsertRN8OtpremaCiradeKam, InsertRN8OtpremaCiradeKamIzvoz, UpdateRadniNalogInterniGenerisan, spPrenesiKontejnerUOtpremuKamionom, spPrenesiKontejnerUOtpremuKamionomIzvoz; preko: InsertIzvozKonacna.PrenesiKontejnerUOtpremuKamionomIzvoz, InsertIzvozKonacna.PrenesiKontejnerUOtpremuKamionomUvoz, InsertRN.InsRN6OtpremaPlatformeKam, InsertRN.InsRN8OtpremaCiradeKam, InsertRN.InsRN8OtpremaCiradeKamIzvoz
  - „ZAVRSEN PRETOVAR UTOVAR CIRADA“ — klik → `button13_Click` — proc: spPrenesiKontejnerUOtpremuKamionom; preko: InsertIzvozKonacna.PrenesiKontejnerUOtpremuKamionomUvoz
  - „OTPREMA UTOVAR CIRADA“ — klik → `button12_Click` — otvara: Otpremnica
  - „PRETOVAR ROBE - PRIJEM / SKLADISTENJE“ — klik → `button11_Click` — otvara: Prijemnica
  - „PLANIRANI PRETOVAR - POSTAVKA KONTEJNERA“ — klik → `button1_Click` — proc: UpdateRadniNalogInterniGenerisan, spPrenesiKontejnerIzPlanaNaPrijemnicu, spPrenesiKontejnerIzPlanaNaPrijemnicuIzvoz; preko: InsertRadniNalogInterni.UpdRadniNalogInterniGenerisan, InsertUvozKonacna.PrenesiKontejnerIzPlanaNaPrijemnicu, InsertUvozKonacna.PrenesiKontejnerIzPlanaNaPrijemnicuIzvoz
  - „Z“ — klik → `button23_Click` — proc: UpdateRadniNalogInterniZavrsen; preko: InsertRadniNalogInterni.UpdRadniNalogInterniZavrsen
  - „Z“ — klik → `button22_Click` — proc: UpdateRadniNalogInterniZavrsen; preko: InsertRadniNalogInterni.UpdRadniNalogInterniZavrsen
  - „PRIJEM ROBE - ISTOVAR ROBE“ — klik → `button16_Click` — otvara: Prijemnica
  - „KALMARISTA PREMESTAJ KONTEJNERA“ — klik → `button15_Click` — otvara: RN12MedjuskladisniKontejnera
  - „OTPREMA / UTOVAR U KONTEJNER“ — klik → `button17_Click` — otvara: Otpremnica
  - „DOLAZAK KAMION CIRADA“ — klik → `button18_Click` — proc: spPrenesiKontejnerIzPlanaNaPrijemnicuIzvoz; preko: InsertUvozKonacna.PrenesiKontejnerIzPlanaNaPrijemnicuIzvoz
  - „PLANIRANI PRETOVAR - POSTAVKA KONTEJNERA“ — klik → `button19_Click` — otvara: RN12MedjuskladisniKontejnera
  - „ZAVRSEN PRETOVAR UTOVAR CIRADA“ — klik → `button24_Click` — proc: InsertKontejnerTekuceOperacije; preko: InsertKontejnerTekuceOperacije.InsKontejnerTekuceOperacije
  - „OTPREMA UTOVAR CIRADA“ — klik → `button5_Click_1` — otvara: Otpremnica
  - „PRIJEM ROBE - ISTOVAR ROBE“ — klik → `button25_Click` — otvara: Prijemnica
  - „DOLAZAK KAMION CIRADA“ — klik → `button28_Click` — proc: InsertKontejnerTekuceOperacije, spPrenesiKontejnerIzPlanaNaPrijemnicuIzvoz; preko: InsertKontejnerTekuceOperacije.InsKontejnerTekuceOperacije, InsertUvozKonacna.PrenesiKontejnerIzPlanaNaPrijemnicuIzvoz
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`

#### Platforma [N]
- **Fajl:** `MainLeget/Drumski/Platforma.cs` — „Platforma“
- **Novi izgled:** Drumski transport → **Drumski1** → „Platforme“ → **Platforma**
- **Otvara:** frmStatus („STATUSI VOZILA“), frmPodesavanjeRaspolozivosti („PROVERA \r\nRASPOLOŽIVOSTI VOZILA“), PakovanjeKamiona1 („FORMIRANJE\r\nTRANSPORTNOG NALOGA“), NalogZaDrumski („NALOG ZA DRUMSKI TRANSPORT“), frmAutomobiliDrumski („UNOS PREVOZNIK/KAMIONER“), frmNalogZaFakturisanje („NALOG ZA FAKTURISANJE“)
- **Funkcionalnosti:**
  - „NALOG ZA DRUMSKI TRANSPORT“ — klik → `btnNalogDrumski_Click` — otvara: NalogZaDrumski
  - „UNOS PREVOZNIK/KAMIONER“ — klik → `btnPonude_Click` — otvara: frmAutomobiliDrumski
  - „FORMIRANJE\r\nTRANSPORTNOG NALOGA“ — klik → `btnFormiranjeNaloga_Click` — otvara: PakovanjeKamiona1
  - „PROVERA \r\nRASPOLOŽIVOSTI VOZILA“ — klik → `btnProvera_Click` — otvara: frmPodesavanjeRaspolozivosti
  - „NALOG ZA FAKTURISANJE“ — klik → `btnNalogFakturisanje_Click` — otvara: frmNalogZaFakturisanje
  - „STATUSI VOZILA“ — klik → `btnStatusi_Click` — otvara: frmStatus

#### Podesavanje1 [N]
- **Fajl:** `MainLeget/Podesavanjesistema/Podesavanje1.cs` — „MainLeget“
- **Novi izgled:** Podešavanja sistema → **Podesavanje1**
- **Otvara:** frmZone („Zone“), SkladisteGrupa („Lokacije“), frmSkladisteObjekat („Objekti“), frmSkladista („Polja“), frmPozicija („Pozicije“), frmTipKontejnera („Tipovi kontejnera“), frmPartnerji („Partneri“)
- **Funkcionalnosti:**
  - „Lokacije“ — klik → `btnLogistikaIzvozaPM2_Click`
  - „Objekti“ — klik → `btnObjekti_Click`
  - „Tipovi kontejnera“ — klik → `btnLogistikaIzvozaPM5_Click` — otvara: frmTipKontejnera
  - „Pozicije“ — klik → `btnLogistikaIzvozaPM4_Click` — otvara: frmPozicija
  - „Polja“ — klik → `btnLogistikaIzvozaPM3_Click` — otvara: frmSkladista
  - „Zone“ — klik → `btnLogistikaIzvozaPM1_Click`
  - „Partneri“ — klik → `sfPartnerji_Click`

#### PoLokomotivama [S]
- **Fajl:** `ReportingOperatika/PoLokomotivama.cs` — „Pregled po lokomotivama“
- **Stari izgled:** Evidencije › Pregledi › Pregled po Lokomotivi → **PoLokomotivama**
- **Tabele:** `Aktivnosti` S, `AktivnostiStavke` S, `Delavci` S, `Vuca` S
- **Funkcionalnosti:**
  - „Prikazi“ — klik → `btnIzracunaj_Click`

#### PoslataDokumenta [S]
- **Fajl:** `EDokumenta/PoslataDokumenta.cs` — „PoslataDokumenta“
- **Stari izgled:** Komercijala › eDokumenta › Poslata dokumenta → **PoslataDokumenta**
- **Funkcionalnosti:**
  - „Pretrazi po datumu“ — klik → `btn_Datum_Click`
  - „Pretrazi po statusu“ — klik → `btn_Status_Click`

#### Prava [S]
- **Fajl:** `Administracija/Prava.cs` — „Prava“
- **Stari izgled:** Administrator › Dodela prava › Forme Prava → **Prava**
- **Otvara:** Obavestenje („Dodeli prava“)
- **Tabele:** `Delavci` S, `Kartice` S, `KarticeForme` S, `KarticeKorisnik` S, `KarticeMain` S, `Korisnici` S
- **Procedure:** `DeletePravo`, `InsertPravo`
- **Funkcionalnosti:**
  - „Zaposleni“ — klik → `button1_Click`
  - „Dodeli prava“ — klik → `button2_Click` — otvara: Obavestenje; čita: Kartice, KarticeForme; proc: DeletePravo, InsertPravo; preko: InsertKorisnici.DeletePravo, InsertKorisnici.InsertPravo

#### Predvidjanje [S]
- **Fajl:** `Pantheon Export/Predvidjanje.cs` — „Predvidjanja“
- **Stari izgled:** Komercijala › CRM › Predviđanje → **Predvidjanje**
- **Tabele:** `ApiLogovi` I p, `MAticniPodatki` S, `MerskeEnote` S, `NosiociTroskova` S, `Odeljenja` S, `Partnerji` S, `Predvidjanje` SIUD p, `Valute` S
- **Procedure:** `DeletePredvidjanje`, `InsertApiLogovi`, `InsertPredvidjanje`, `UpdatePredvidjanje`, `VratiPredvidjanjeStatus`
- **Funkcionalnosti:**
  - `dataGridView1` — ColumnHeaderMouseClick → `dataGridView1_ColumnHeaderMouseClick` — čita: MaticniPodatki, NosiociTroskova, Odeljenja, Partnerji, Predvidjanje
  - „Novi“ — klik → `tsNew_Click` — čita: Predvidjanje
  - „tsSave“ — klik → `tsSave_Click` — čita: NosiociTroskova, Partnerji; proc: InsertPredvidjanje, UpdatePredvidjanje; preko: InsertPatheonExport.InsPredvidjanje, InsertPatheonExport.UpdPredvidjanje
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeletePredvidjanje; preko: InsertPatheonExport.DelPredvidjanje
  - „Sva predvidjanja“ — klik → `toolStripButton2_Click` — čita: MaticniPodatki, NosiociTroskova, Odeljenja, Partnerji, Predvidjanje
  - „Export Pantheon“ — klik → `toolStripButton3_Click` — piše: Predvidjanje[U]; proc: InsertApiLogovi; preko: InsertPatheonExport.InsApiLog
  - „Refresh“ — klik → `btnRefresh_Click` — čita: MaticniPodatki, NosiociTroskova, Odeljenja, Partnerji, Predvidjanje
  - `btnPredvidjanjeFilter` — klik → `btnPredvidjanjeFilter_Click` — čita: MaticniPodatki, NosiociTroskova, Odeljenja, Partnerji, Predvidjanje
  - `btnNTNazivFilter` — klik → `btnNTNazivFilter_Click` — čita: MaticniPodatki, NosiociTroskova, Odeljenja, Partnerji, Predvidjanje
  - `btnNTFilter` — klik → `btnNTFilter_Click` — čita: MaticniPodatki, NosiociTroskova, Odeljenja, Partnerji, Predvidjanje
  - „Vrati na status 0“ — klik → `button3_Click` — proc: VratiPredvidjanjeStatus; preko: InsertPatheonExport.VratiPredvidjenjeStatus
  - „Nazad“ — klik → `button1_Click`
  - `dataGridView2` — ColumnHeaderMouseClick → `dataGridView2_ColumnHeaderMouseClick` — čita: MaticniPodatki, NosiociTroskova, Odeljenja, Partnerji, Predvidjanje
  - „Dodaj novu poziciju“ — klik → `button2_Click` — čita: Predvidjanje; proc: InsertPredvidjanje; preko: InsertPatheonExport.InsPredvidjanje

#### Pregledac [N]
- **Fajl:** `MainLeget/PrijemIOtpremaKamiona/Pregledac.cs` — „Pregledac“
- **Novi izgled:** Prijem i otprema kamiona → **frmPrijemIOtpremaKamiona1** → „PREGLEDAČ“ → **Pregledac**
- **Otvara:** ZapisnikONepravilnosti („Evidencija nepravilnosti“), frmCIR („Izradi CIR“), frmDodatneUsluge („Dodatne usluge“), frmFotografijePregledac („Fotografije“)
- **Tabele:** `IzvozKonacna` S, `Kapija` S, `OtpremaKontejneraVozStavke` S, `Partnerji` S, `PrijemKontejneraVozStavke` S, `ProdajniNalogIzvozStavke` S, `RadniNalogInterni` S, `RadniNalogInterniPotvrda` S, `RNOtpremaPlatforme` S, `RNPrijemPlatforme` S, `Scenario` S, `TipKontenjera` S
- **Procedure:** `UpdateRN4PotrebanCIR`, `UpdateRN4UradjeneVizuelni`, `UpdateRN6PotrebanCIR`, `UpdateRN6UradjeneVizuelni`
- **Funkcionalnosti:**
  - „Fotografije“ — klik → `button5_Click` — otvara: frmFotografijePregledac
  - „Dodatne usluge“ — klik → `button4_Click` — otvara: frmDodatneUsluge
  - „Izradi CIR“ — klik → `button3_Click` — otvara: frmCIR; proc: UpdateRN4PotrebanCIR, UpdateRN6PotrebanCIR; preko: InsertRN.UpdateRN4PotrebanCIR, InsertRN.UpdateRN6PotrebanCIR
  - „Evidencija nepravilnosti“ — klik → `button8_Click` — otvara: ZapisnikONepravilnosti
  - „Potvrdi VP“ — klik → `button1_Click` — proc: UpdateRN4UradjeneVizuelni, UpdateRN6UradjeneVizuelni; preko: InsertRN.UpdateRN4UradjeneVizuelni, InsertRN.UpdateRN6UradjeneVizuelni
  - „Refresh“ — klik → `button2_Click`

#### PregledKomercijalnihNaloga [N]
- **Fajl:** `Skladista main/PregledKomercijalnihNaloga.cs` — „PregledKomercijalnihNaloga“
- **Novi izgled:** Operacije skladišta → **SkladistaMain** → kartica „Radni nalozi“ → **NalogZaKomercijalni** → „SKLADIŠNI NALOZI UVOZ“ → **PregledKomercijalnihNaloga**
- **Otvara:** frmNalogPrijem („Otvori“), Otprema („Otvori“), TipSkladista („Otvori“), frmScenarioSCI („Pregledaj podatke“)
- **Tabele:** `IzvozKonacna` S, `IzvozKonacnaVrstaManipulacije` S, `IzvozKonacnaZaglavlje` S, `KontejnerStatus` S, `organizacioneJedinice` S, `Partnerji` S, `ProdajniNalogIzvoz` S, `RadniNalogInterni` S, `RadniNalogInterniPotvrda` S, `RadniNalogInterniSkladistePotvrda` S, `RadniNalogSkladista` SI p, `Scenario` S, `TipKontenjera` S, `UvozKonacna` S, `UvozKonacnaNapomenePozicioniranja` S, `UvozKonacnaVrstaManipulacije` S, `UvozKonacnaZaglavlje` S, `Voz` S, `VrstaManipulacije` S
- **Procedure:** `InsertRadniNalogSkladista`, `UpdateRNInterniSkladista`
- **Funkcionalnosti:**
  - „Potvrdi“ — klik → `btnAktiviraj_Click` — čita: IzvozKonacna, ProdajniNalogIzvoz, RadniNalogSkladista; proc: InsertRadniNalogSkladista, UpdateRNInterniSkladista; preko: InsertSkladista.InsertRadniNalog, InsertSkladista.UpdateRNInterni
  - „Refresh“ — klik → `button25_Click`
  - „Otvori“ — klik → `button24_Click` — otvara: frmNalogPrijem, TipSkladista; čita: IzvozKonacna, RadniNalogInterni, RadniNalogSkladista
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - „Pregledaj podatke“ — klik → `button1_Click`

#### PregledLagera [N]
- **Fajl:** `Skladista main/Dokumenta/PregledLagera.cs` — „PregledLagera“
- **Novi izgled:** Operacije skladišta → **SkladistaMain** → „Carinsko skladište“ → **CarinskoSkladiste** → „Pregled lagera robe“ → **PregledLagera**
- **Tabele:** `MagacinskiBrojCarinski` S, `Partnerji` S, `Primljeno` S, `Promet` S, `RadniNalogSkladista` S, `Rezervisano` S, `RNCarinskoPrijemnicaStavke` S, `RNCarinskoSkladisteOtpremaStavkePom` S, `RNCarinskoSkladisteOtpremnica` S, `RNCarinskoSkladisteOtpremnicaStavke` S, `RNCarinskoSkladistePrijemnica` S, `Skladista` S, `Stock` S, `TipPalete` S, `Zavrseno` S
- **Combobox → tabela:** `cboPrvi` ← `Stock`; `cboPrvi` ← `Stock`; `cboFilter` ← `Stock`; `cboFilter` ← `Stock`

#### Pretovar [N]
- **Fajl:** `Skladista main/Dokumenta/Pretovar.cs` — „Pretovar“
- **Novi izgled:** Operacije skladišta → **SkladistaMain** → kartica „Radni nalozi“ → **NalogZaKomercijalni** → „Kreiraj novi“ → **TipSkladista** → „Carinsko skladište“ → **RadniNaloziSkladista** → „Pretovar“ → **Pretovar**

#### PretovariMain [N]
- **Fajl:** `MainLeget/Pretovari/PretovariMain.cs` — „PretovariMain“
- **Novi izgled:** Operacije pretovari → **PretovariMain**
- **Otvara:** frmPretovariSkladista („Skladišta“), PretovariPrijem („Pretovar“)
- **Funkcionalnosti:**
  - „Pretovar“ — klik → `btnPretovarPrijem_Click` — otvara: PretovariPrijem
  - „Skladišta“ — klik → `btnPretovarSkladista_Click` — otvara: frmPretovariSkladista

#### PretovariPrijem [N]
- **Fajl:** `MainLeget/Pretovari/PretovariPrijem.cs` — „PretovariPrijem“
- **Novi izgled:** Operacije pretovari → **PretovariMain** → „Pretovar“ → **PretovariPrijem**
- **Otvara:** frmPretovarPrijem („Prijem“)
- **Funkcionalnosti:**
  - „Prijem “ — klik → `btnPretovarPrijem_Click` — otvara: frmPretovarPrijem

#### PrihodiPosla [S]
- **Fajl:** `Pantheon Export/PrihodiPosla.cs` — „PrihodiPosla“
- **Stari izgled:** Komercijala › CRM › Izvestaj posla → **PrihodiPosla**
- **Tabele:** `Aktivnosti` S, `AktivnostiStavke` S, `Delavci` S, `Faktura` S, `FakturaPostav` S, `MAticniPodatki` S, `Najava` S, `NosiociTroskova` S, `Odeljenja` S, `Partnerji` S, `Predvidjanje` S, `RadniNalogTrase` S, `RadniNalogVezaNajave` S, `Stanice` S, `Trase` S, `VrstaAktivnosti` S
- **Funkcionalnosti:**
  - „Otvori“ — klik → `button1_Click`

#### PrijemIOtpremaVozova1 [N]
- **Fajl:** `MainLeget/Prijemiotpremavozova/PrijemIOtpremaVozova1.cs` — „PrijemIOtpremaVozova1“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1**
- **Otvara:** frmPrijemIOtpremaVozova2 („Depo cnt“), frmPrijemIOtpremaVozova3 („Pregled naloga“), frmPrijemIOtpremaVozova4 („Voz“), frmRadniNalogInterniPregled („Zadaci intermodalni dispećer“), frmKontejnerTekuceArhiv („Terminal central (arhiv)“), frmDodatneUsluge („Dodatne usluge“), frmVozPrijem („Prijem“), frmVozOtprema („Otprema“)
- **Funkcionalnosti:**
  - „Depo cnt“ — klik → `btnPrijemIOtpremaVozova2_Click` — otvara: frmPrijemIOtpremaVozova2
  - „Voz“ — klik → `btnPrijemIOtpremaVozova6_Click` — otvara: frmPrijemIOtpremaVozova4
  - „Dodatne usluge“ — klik → `btnPrijemIOtpremaVozova5_Click` — otvara: frmDodatneUsluge
  - „Pregled naloga“ — klik → `btnPrijemIOtpremaVozova4_Click` — otvara: frmPrijemIOtpremaVozova3
  - „Terminal central (arhiv)“ — klik → `btnPrijemIOtpremaVozova3_Click` — otvara: frmKontejnerTekuceArhiv
  - „Zadaci intermodalni dispećer“ — klik → `btnPrijemIOtpremaVozova1_Click` — otvara: frmRadniNalogInterniPregled
  - „Prijem“ — klik → `btnPrijemIOtpremaVozova7_Click` — otvara: frmVozPrijem
  - „Otprema“ — klik → `btnPrijemIOtpremaVozova8_Click` — otvara: frmVozOtprema

#### Prijemnica [N]
- **Fajl:** `Skladista main/Dokumenta/Prijemnica.Palete.cs` — „Prijemnica“
- **Novi izgled:** Operacije skladišta → **SkladistaMain** → „Carinsko skladište“ → **CarinskoSkladiste** → „Prijem robe“ → **CarinskoSkladistePregled** → „Prijemnica“ → **Prijemnica**
- **Otvara:** TerminalMapFRM („Mapa pozicija“), frmTipPalete („?“), PaletePrebacivanje („Prebaci na palete“)
- **Tabele:** `Artikli` SU, `ArtikliCetvrtiNivo` S, `ArtikliDrugiNivo` S, `ArtikliPrviNivo` S, `ArtikliTreciNivo` S, `Carinarnice` S, `Delavci` S, `MagacinskiBrojCarinski` S, `MerskeEnote` S, `MestaUtovara` S, `NHM` S, `Partnerji` S, `RadniNalogInterni` S, `RadniNalogSkladista` S, `RNCarinskoPackingLista` SIU, `RNCarinskoPackingListaStavka` SIU, `RNCarinskoPaleta` S, `RNCarinskoPaletaStavka` S, `RNCarinskoPrijemnicaStavke` SID p, `RNCarinskoSkladisteDodatneUsluge` S, `RNCarinskoSkladistePrijemnica` SIU p, `RNCarinskoSkladisteRukovalac` S, `Skladista` S, `TipPalete` S, `Valute` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaVozila` S, `VrstePrevoznogSredstva` S
- **Procedure:** `InsertRNCarinskoPrijemnicaStavke`, `InsertRNCarinskoRukovalac`, `InsertRNCarinskoSkladistePrijemnica`, `PotvrdiPrijem`, `sp_PL_Analiziraj`, `sp_PL_FormirajArtikle`, `sp_PrijemnicaStavka_Snimi`, `UpdateRNCarinskoSkladistePrijemnica`
- **Combobox → tabela:** `cboMagacinskiBroj` ← `MagacinskiBrojCarinski`; `cboDodatneUsluge` ← `VrstaManipulacije`; `cboNalogodavac` ← `Partnerji`; `cboVlasnikRobe` ← `Partnerji`; `cboSpediterPrijem` ← `Partnerji`; `cboTipTransportaPrijem` ← `VrstePrevoznogSredstva`; `cboVrstaKamionaPrijem` ← `VrstaVozila`; `cboCarinarnicaPrijem` ← `Carinarnice`; `cboMestoIstovaraPrijem` ← `MestaUtovara`; `cboJM` ← `MerskeEnote`; `cboValutaStavka` ← `Valute`; `cboVrstaPaleta` ← `TipPalete`; `cboCarinskiPostupak` ← `VrstaCarinskogPostupka`; `cboPosiljalac` ← `Partnerji`; `cboPozicija` ← `Skladista`; `cboRukovalac` ← `Delavci`; `cboPLKlasifikacija` ← `ArtikliCetvrtiNivo`, `ArtikliDrugiNivo`, `ArtikliPrviNivo`, `ArtikliTreciNivo`
- **Funkcionalnosti:**
  - „Izbaci“ — klik → `btnIzbaci_Click`
  - „Dodaj“ — klik → `btnDodajUsluge_Click`
  - „Mapa pozicija“ — klik → `btnMapa_Click`
  - „Uvezi packing listu“ — klik → `btnPackingLista_Click` — izvoz u Excel
  - „Nalog za istovar rukovaocima“ — klik → `btnNalog_Click`
  - „Snimi - Staro“ — klik → `btnSnimi_Click`
  - „Zapisnik“ — klik → `btnZapisnik_Click`
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - „?“ — klik → `btnPaleta_Click` — otvara: frmTipPalete
  - „NHM“ — klik → `btnNHM_Click`
  - „Ukloni“ — klik → `btnUkloni_Click`
  - „Dodaj“ — klik → `btnDodaj_Click`
  - „Nazad“ — klik → `btnNHMNazad_Click`
  - „Dodaj“ — klik → `btnNHMDodaj_Click`
  - „Nazad“ — klik → `btnVratiNalog_Click`
  - „Izdaj nalog“ — klik → `btnIzdajNalog_Click` — čita: RNCarinskoSkladisteDodatneUsluge, RNCarinskoSkladisteRukovalac; proc: InsertRNCarinskoRukovalac; preko: InsertSkladista.InsertNalogRukovalac
  - „Nalog rukovaocima“ — klik → `button2_Click`
  - „Mapa pozicija“ — klik → `button1_Click`
  - „Snimi“ — klik → `button24_Click` — piše: RNCarinskoPrijemnicaStavke[D]; čita: RNCarinskoSkladistePrijemnica; proc: InsertRNCarinskoPrijemnicaStavke, InsertRNCarinskoSkladistePrijemnica, UpdateRNCarinskoSkladistePrijemnica; preko: InsertSkladista.InsertPrijemnicaCarinska, InsertSkladista.InsertPrijemnicaCarinskaStavke, InsertSkladista.UpdatePrijemnicaCarinska
  - „Dodaj“ — klik → `btnDodajArtikal_Click`
  - „Šifra“ — čekiranje → `chkPrviNivo_CheckedChanged`
  - „Šifra“ — čekiranje → `chkDrugiNivo_CheckedChanged`
  - „Šifra“ — čekiranje → `chkTreciNivo_CheckedChanged`
  - „Šifra“ — čekiranje → `chkCetvrtiNivo_CheckedChanged`
  - „Nazad“ — klik → `btnNazadArtikal_Click`
  - „Artikli“ — klik → `btnArtikli_Click`
  - „Šifra“ — čekiranje → `chkArtikal_CheckedChanged`
  - „Prebaci na palete“ — klik → `btnPaleteNovo_Click` — otvara: PaletePrebacivanje
  - `dgvPL` — kraj unosa u ćeliji → `DgvPL_CellEndEdit` — piše: Artikli[U], RNCarinskoPackingListaStavka[U]; proc: sp_PL_Analiziraj
  - „gridGroupingControl1“ — dupli klik na ćeliju → `GridStavke_CellDoubleClick`

#### Prijemnica [N+S]
- **Fajl:** `RadniNalozi/Prijemnica.cs` — „Prijemnica“
- **Novi izgled:** Operacije pretovari → **PretovariMain** → „Pretovar“ → **PretovariPrijem** → „Prijem“ → **frmPretovarPrijem** → „Radni nalog pretovar“ → **Prijemnica**
- **Stari izgled:** Terminal › Terminalske usluge › ZADACI IPU DISPEČER → **PlaniraniPretovar** → „PRETOVAR ROBE - PRIJEM“ → **Prijemnica**
- **Otvara:** PrijemnicaPregled („PRETOVAR PREGLED“), ZapisnikOOstecenjuRobe („Zapisnik o oštećenju“)
- **Tabele:** `Delavci` S, `Lokac` S, `MAticniPodatki` S, `MerskeEnote` S, `Mesta` S, `NHM` S, `NNal` S, `NNalP` S, `NPre` S, `NPreP` S, `Partnerji` S, `Pozicija` S, `Promet` SI p, `RadniNalogInterni` S, `Sklad` S, `Skladista` S, `SkladistaGrupa` S, `TipRobe` S, `UvozKonacnaNHM` S
- **Procedure:** `InsertPrijemnica`, `InsertPrijemnicaPostav`, `InsertPromet`, `UpdatePorudzbenica`, `UpdateRN9PretovarCIRADE`
- **Combobox → tabela:** `cbo_Skladiste` ← `Sklad`, `Skladista`; `cbo_Partner` ← `Partnerji`; `cbo_Referent` ← `Delavci`; `cboPrimio` ← `Delavci`; `cbo_Lokacija` ← `Lokac`; `cboGrupaPolja` ← `SkladistaGrupa`; `cbo_Lokacija` ← `Pozicija`; `cbo_MestoTroska` ← `Mesta`; `cbo3` ← `MerskeEnote`; `cbo31` ← `Skladista`
- **Funkcionalnosti:**
  - „Izbaci“ — klik → `btnIzbaci_Click`
  - „Dodaj“ — klik → `btnDodajUsluge_Click`
  - „Mapa pozicija“ — klik → `btnMapa_Click`
  - „Uvezi packing listu“ — klik → `btnPackingLista_Click`
  - „Nalog za istovar rukovaocima“ — klik → `btnNalog_Click`
  - „Snimi - Staro“ — klik → `btnSnimi_Click`
  - „Zapisnik“ — klik → `btnZapisnik_Click`
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - „?“ — klik → `btnPaleta_Click`
  - „NHM“ — klik → `btnNHM_Click`
  - „Ukloni“ — klik → `btnUkloni_Click`
  - „Dodaj“ — klik → `btnDodaj_Click`
  - „Nazad“ — klik → `btnNHMNazad_Click`
  - „Dodaj“ — klik → `btnNHMDodaj_Click`
  - „Nazad“ — klik → `btnVratiNalog_Click`
  - „Izdaj nalog“ — klik → `btnIzdajNalog_Click`
  - „Nalog rukovaocima“ — klik → `button2_Click`
  - „Mapa pozicija“ — klik → `button1_Click`
  - „Snimi“ — klik → `button24_Click`
  - „Dodaj“ — klik → `btnDodajArtikal_Click`
  - „Šifra“ — čekiranje → `chkPrviNivo_CheckedChanged`
  - „Šifra“ — čekiranje → `chkDrugiNivo_CheckedChanged`
  - „Šifra“ — čekiranje → `chkTreciNivo_CheckedChanged`
  - „Šifra“ — čekiranje → `chkCetvrtiNivo_CheckedChanged`
  - „Nazad“ — klik → `btnNazadArtikal_Click`
  - „Artikli“ — klik → `btnArtikli_Click`
  - „Šifra“ — čekiranje → `chkArtikal_CheckedChanged`
  - „Prebaci na palete“ — klik → `btnPaleteNovo_Click`
  - „Mapa pozicija“ — klik → `button1_Click_1` — otvara: PrijemnicaPregled; čita: Promet; proc: InsertPrijemnica, InsertPrijemnicaPostav, InsertPromet, UpdatePorudzbenica, UpdateRN9PretovarCIRADE; preko: InsertIsporuka.InsertPrijemnica, InsertIsporuka.InsertPrijemnicaPostav, InsertIsporuka.InsertPromet, InsertIsporuka.UpdPorudzbenica, InsertRN.PotvrdiUradjenPretovarCirade
  - `btn_Povuci` — klik → `btn_Povuci_Click` — čita: Delavci, NNal, NPre, NPreP, sklad
  - „Nalog rukovaocima“ — klik → `button2_Click_1`
  - `dataGridView2` — izbor → `dataGridView2_SelectionChanged` — čita: MaticniPodatki, NNalP
  - `btn_Izaberi` — klik → `btn_Izaberi_Click` — čita: MaticniPodatki, NNal, NNalP, sklad
  - „OPIS ROBE“ — klik → `label6_Click`
  - „Zapisnik“ — klik → `button3_Click_1` — otvara: PrijemnicaPregled
  - `cboGrupaPolja` — izbor → `cboGrupaPolja_SelectedIndexChanged` — čita: Skladista
  - `button6` — klik → `button3_Click_1` — otvara: PrijemnicaPregled
  - `button23` — klik → `button23_Click` — otvara: ZapisnikOOstecenjuRobe
  - `button22` — klik → `button1_Click_1` — otvara: PrijemnicaPregled; čita: Promet; proc: InsertPrijemnica, InsertPrijemnicaPostav, InsertPromet, UpdatePorudzbenica, UpdateRN9PretovarCIRADE; preko: InsertIsporuka.InsertPrijemnica, InsertIsporuka.InsertPrijemnicaPostav, InsertIsporuka.InsertPromet, InsertIsporuka.UpdPorudzbenica, InsertRN.PotvrdiUradjenPretovarCirade

#### PrijemnicaPregled [N+S]
- **Fajl:** `RadniNalozi/PrijemnicaPregled.cs`
- **Novi izgled:** Operacije pretovari → **PretovariMain** → „Pretovar“ → **PretovariPrijem** → „Prijem“ → **frmPretovarPrijem** → „Radni nalog pretovar“ → **Prijemnica** → „PRETOVAR PREGLED“ → **PrijemnicaPregled**
- **Stari izgled:** Terminal › Skladišno poslovanje › PRIJEMI ROBE PRETOVAR → **PrijemnicaPregled**
- **Tabele:** `Delavci` S, `NPre` S, `NPreP` S, `Partnerji` S, `Pozicija` S, `Promet` S, `Skladista` S
- **Funkcionalnosti:**
  - `button1` — klik → `button1_Click` — izveštaj: rptPrijemnica.rdlc

#### PrimljenaDokumenta [S]
- **Fajl:** `EDokumenta/PrimljenaDokumenta.cs` — „PrimljenaDokumenta“
- **Stari izgled:** Komercijala › eDokumenta › Primljena dokumenta → **PrimljenaDokumenta**
- **Funkcionalnosti:**
  - „Pretrazi po datumu“ — klik → `btn_Datum_Click`
  - „Ospori“ — klik → `btn_Ospori_Click`
  - „Preuzmi“ — klik → `btn_Preuzmi_Click`
  - „Pretrazi po statusu“ — klik → `btn_Status_Click`

#### RadniNaloziSkladista [N]
- **Fajl:** `Skladista main/RadniNaloziSkladista.cs` — „RadniNaloziSkladista“
- **Novi izgled:** Operacije skladišta → **SkladistaMain** → kartica „Radni nalozi“ → **NalogZaKomercijalni** → „Kreiraj novi“ → **TipSkladista** → „Carinsko skladište“ → **RadniNaloziSkladista**
- **Otvara:** frmNalogPrijem („Prijem“), Otprema („Otprema“), Pretovar („Pretovar“), InterniPrenos („Interni prenos“), VanredniPoslovi („Vanredni poslovi“), InterniPoslovi („Interni poslovi“)
- **Funkcionalnosti:**
  - „Interni poslovi“ — klik → `btnInterniPoslovi_Click`
  - „Vanredni poslovi“ — klik → `btnVanredniPoslovi_Click`
  - „Interni prenos“ — klik → `btnInterniPrenos_Click`
  - „Pretovar“ — klik → `btnPretovar_Click`
  - „Otprema“ — klik → `btnOtprema_Click`
  - „Prijem“ — klik → `btnPrijem_Click` — otvara: frmNalogPrijem

#### RN10PregledIspraznjenogKontejnera [S]
- **Fajl:** `RadniNalozi/RN10PregledIspraznjenogKontejnera.cs` — „RN10cs“
- **Stari izgled:** Terminal › Radni nalozi › Pregled ispraznjenog kontejnera - N → **RN10PregledIspraznjenogKontejnera**
- **Tabele:** `DirigacijaKontejneraZa` S, `NHM` S, `Partnerji` S, `Pozicija` S, `PrijemKontejneraVozStavke` S, `RadniNalogInterni` S, `RNPregledIspraznjenogKontejnera` SIUD p, `Sklad` S, `Skladista` S, `TipKontenjera` S, `UvozKonacnaVrstaManipulacije` S, `Voz` S, `VrstaManipulacije` S, `VrstePostupakaUvoz` S
- **Procedure:** `DeleteRNPregledIspraznjenogKontejnera`, `InsertRN10PregledCeoVoz`, `InsertRNPregledIspraznjenogKontejnera`, `UpdateRNPregledIspraznjenogKontejnera`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertRNPregledIspraznjenogKontejnera, UpdateRNPregledIspraznjenogKontejnera; preko: InsertRN.InsRNPregledIspraznjenogKontejnera, InsertRN.UpdRNPregledIspraznjenogKontejnera
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteRNPregledIspraznjenogKontejnera; preko: InsertRN.DelRNPregledIspraznjenogKontejnera
  - „FORMIRAJ RADNI NALOG ZA PREGLED ISPRAŽNJENOG  KONTEJNERA SVIH KONTEJNERA“ — klik → `toolStripButton1_Click` — proc: InsertRN10PregledCeoVoz; preko: InsertRN.InsRN10PregledCeoVoz

#### RN11PreglediPostavkaKontejnera [S]
- **Fajl:** `RadniNalozi/RN11PreglediPostavkaKontejnera.cs` — „RN11 - PREGLED POSTAVKA KONTEJNERA“
- **Stari izgled:** Terminal › Radni nalozi › Pregled i postavka kontejnera - N → **RN11PreglediPostavkaKontejnera**
- **Tabele:** `NHM` S, `Partnerji` S, `Pozicija` S, `RNPregledIpostavkaKontejnera` SIUD p, `Sklad` S, `Skladista` S, `TipKontenjera` S, `VrstaManipulacije` S
- **Procedure:** `DeleteRNPregledIpostavkaKontejnera`, `InsertRN10PregledCeoVoz`, `InsertRNPregledIpostavkaKontejnera`, `UpdateRNPregledIpostavkaKontejnera`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertRNPregledIpostavkaKontejnera, UpdateRNPregledIpostavkaKontejnera; preko: InsertRN.InsRNPregledIpostavkaKontejnera, InsertRN.UpdRNPregledIpostavkaKontejnera
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteRNPregledIpostavkaKontejnera; preko: InsertRN.DelRNPregledIpostavkaKontejnera
  - „FORMIRAJ RN POSTAVKA KONTEJNERA“ — klik → `toolStripButton1_Click` — proc: InsertRN10PregledCeoVoz; preko: InsertRN.InsRN10PregledCeoVoz

#### RN12MedjuskladisniKontejnera [N+S]
- **Fajl:** `RadniNalozi/RN12MedjuskladisniKontejnera.cs` — „RN12 - MEDJUSKLADISNI PRENOS“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Potvrdi“ → **RN12MedjuskladisniKontejnera**
- **Stari izgled:** Terminal › Radni nalozi › RN12 - INTERNI PRENOS → **RN12MedjuskladisniKontejnera**
- **Otvara:** frmKontejnerTekuce („KONTEJNER NA SKLADISTU“), frmDodelaSkladista („Dodela polja“)
- **Tabele:** `KontejnerTekuce` S, `NHM` S, `Partnerji` S, `Pozicija` S, `RadniNalogInterni` S, `RNMedjuskladisni` SUD p, `RNPrijemVoza` S, `Skladista` S, `TipKontenjera` S, `VrstaManipulacije` S
- **Procedure:** `DeleteRNMedjuskladisni`, `InsertRN12Medjuskladisni`, `UpdateRN12Uradjene`, `UpdateRNMedjuskladisni`
- **Combobox → tabela:** `cboUsluga` ← `RadniNalogInterni`, `VrstaManipulacije`; `cboVrstaKontejnera` ← `TipKontenjera`; `cboBrodar` ← `Partnerji`; `cboVrstaRobe` ← `NHM`; `cboUsluga` ← `VrstaManipulacije`; `cboNaSklad` ← `Skladista`; `cboNaPoz` ← `Pozicija`; `cboSaSklad` ← `Skladista`; `cboSaPoz` ← `Pozicija`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertRN12Medjuskladisni, UpdateRNMedjuskladisni; preko: InsertRN.InsRnMedjuskladisni, InsertRN.UpdRnMedjuskladisni
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteRNMedjuskladisni; preko: InsertRN.DelRnMedjuskladisni
  - „FORMIRAJ RADNE NALOGE ZA PRIJEM VOZA / AKTIVIRAJ“ — klik → `toolStripButton4_Click` — proc: InsertRN12Medjuskladisni; preko: InsertRN.InsRN12Medjuskladisni
  - „POTVRDI ZAVRSEN“ — klik → `toolStripButton3_Click` — proc: UpdateRN12Uradjene; preko: InsertRN.PotvrdiUradjenRN12
  - „KONTEJNER NA SKLADIŠTU“ — klik → `toolStripButton2_Click`
  - „T“ — klik → `button1_Click`
  - „V“ — klik → `button2_Click`
  - „Dodela polja“ — klik → `button6_Click` — otvara: frmDodelaSkladista
  - „KONTEJNER NA SKLADISTU“ — klik → `toolStripButton2_Click`
  - „POTVRDI ZAVRŠEN “ — klik → `toolStripButton3_Click` — proc: UpdateRN12Uradjene; preko: InsertRN.PotvrdiUradjenRN12
  - „FORMIRAJ RADNE NALOGE “ — klik → `toolStripButton4_Click` — proc: InsertRN12Medjuskladisni; preko: InsertRN.InsRN12Medjuskladisni
  - `button20` — klik → `tsDelete_Click` — proc: DeleteRNMedjuskladisni; preko: InsertRN.DelRnMedjuskladisni
  - `button21` — klik → `tsSave_Click` — proc: InsertRN12Medjuskladisni, UpdateRNMedjuskladisni; preko: InsertRN.InsRnMedjuskladisni, InsertRN.UpdRnMedjuskladisni

#### RN1PrijemVoza [N+S]
- **Fajl:** `RadniNalozi/RN1PrijemVoza.cs` — „RADNI NALOZI - PRIJEM VOZA - RN ZA VIZUELNI PREGLED I KALMARISTU“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Voz“ → **frmPrijemIOtpremaVozova4** → „RN prijem voza“ → **RN1PrijemVoza**
- **Stari izgled:** Terminal › Radni nalozi → **RN1PrijemVoza**
- **Otvara:** frmDodelaSkladista („Dodela polja“)
- **Tabele:** `NHM` S, `Partnerji` S, `Pozicija` S, `RadniNalogInterni` S, `RNPrijemVoza` SIUD p, `Skladista` S, `TipKontenjera` S, `UvozKonacnaNHM` S, `Voz` S, `VrstaManipulacije` S
- **Procedure:** `DeleteRNPrijemVoza`, `InsertRNPrijemVoza`, `InsertRNPrijemVozaCeoVoz`, `UpdateRN1Uradjene`, `UpdateRN1UradjeneCIR`, `UpdateRN1UradjeneVP`, `UpdateRNPrijemVoza`
- **Combobox → tabela:** `cboUsluge` ← `RadniNalogInterni`, `VrstaManipulacije`; `cbovrstakontejnera` ← `TipKontenjera`; `cboSaVoznog` ← `Voz`; `cboUvoznik` ← `Partnerji`; `cboBrodar` ← `Partnerji`; `cboNaSkladiste` ← `Skladista`; `cboNaPoziciju` ← `Pozicija`; `cboUsluge` ← `VrstaManipulacije`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertRNPrijemVoza, UpdateRNPrijemVoza; preko: InsertRN.InsRNPPrijemVoza, InsertRN.UpdRNPPrijemVoza
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteRNPrijemVoza; preko: InsertRN.DelRNPPrijemVoza
  - „Refresh DG“ — klik → `toolStripButton7_Click`
  - „FORMIRAJ RADNE NALOGE ZA PRIJEM VOZA / AKTIVIRAJ“ — klik → `toolStripButton1_Click` — proc: InsertRNPrijemVozaCeoVoz; preko: InsertRN.InsRNPPrijemVozaCeoVoz
  - „Potvrdi završen VP“ — klik → `toolStripButton5_Click` — proc: UpdateRN1UradjeneVP; preko: InsertRN.PotvrdiUradjenRN1VP
  - „Potvrdi završen CIR“ — klik → `toolStripButton6_Click` — proc: UpdateRN1UradjeneCIR; preko: InsertRN.PotvrdiUradjenRN1CIr
  - „Potvrdi završen “ — klik → `toolStripButton2_Click` — proc: UpdateRN1Uradjene; preko: InsertRN.PotvrdiUradjenRN1
  - „Pregled radnih naloga Central“ — klik → `toolStripButton4_Click` — otvara: frmAnalizaRadnihNaloga
  - „Dodela polja“ — klik → `button2_Click` — otvara: frmDodelaSkladista
  - „Potvrdi završen“ — klik → `toolStripButton2_Click` — proc: UpdateRN1Uradjene; preko: InsertRN.PotvrdiUradjenRN1
  - „Potvrdi završen CIR“ — klik → `toolStripButton6_Click` — proc: UpdateRN1UradjeneCIR; preko: InsertRN.PotvrdiUradjenRN1CIr
  - „Potvrdi završen VP“ — klik → `toolStripButton5_Click` — proc: UpdateRN1UradjeneVP; preko: InsertRN.PotvrdiUradjenRN1VP
  - „FORMIRAJ RADNE NALOGE ZA PRIJEM VOZA / AKTIVIRAJ“ — klik → `toolStripButton1_Click` — proc: InsertRNPrijemVozaCeoVoz; preko: InsertRN.InsRNPPrijemVozaCeoVoz
  - „Refresh“ — klik → `toolStripButton7_Click`
  - `button20` — klik → `tsDelete_Click` — proc: DeleteRNPrijemVoza; preko: InsertRN.DelRNPPrijemVoza
  - `button21` — klik → `tsSave_Click` — proc: InsertRNPrijemVoza, UpdateRNPrijemVoza; preko: InsertRN.InsRNPPrijemVoza, InsertRN.UpdRNPPrijemVoza

#### RN2OtpremaVoza [N+S]
- **Fajl:** `RadniNalozi/RN2OtpremaVoza.cs` — „RN2-OTPREMA VOZA“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Voz“ → **frmPrijemIOtpremaVozova4** → „RN otprema voza“ → **RN2OtpremaVoza**
- **Stari izgled:** Terminal › Radni nalozi › RN2 - OTPREMA VOZA → **RN2OtpremaVoza**
- **Tabele:** `KontejnerTekuce` S, `NHM` S, `Partnerji` S, `Pozicija` S, `RNOtpremaVoza` SIUD p, `Skladista` S, `TipKontenjera` S, `Voz` S, `VrstaManipulacije` S
- **Procedure:** `DeleteRNOtpremaVoza`, `InsertRNOtpremaVoza`, `InsertRNOtpremaVozaCeoVoz`, `PrenesiKontejnerTekuceUArhiv`, `UpdateRN2Uradjene`, `UpdateRNOtpremaVoza`
- **Combobox → tabela:** `cboVrstaKontejnera` ← `TipKontenjera`; `cboNaSredstvo` ← `Voz`; `cboBrodar` ← `Partnerji`; `cboRoba` ← `NHM`; `cboUsluga` ← `VrstaManipulacije`; `cboSaSklad` ← `Skladista`; `cboSaPoz` ← `Pozicija`
- **Funkcionalnosti:**
  - „Datum rasporeda“ — klik → `label7_Click`
  - „ID i naziv planirane usluge“ — klik → `label10_Click`
  - „Datum realizacije“ — klik → `label9_Click`
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertRNOtpremaVoza, UpdateRNOtpremaVoza; preko: InsertRN.InsRNOtpremaVoza, InsertRN.UpdRNOtpremaVoza
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteRNOtpremaVoza; preko: InsertRN.DelRNOtpremaVoza
  - „FORMIRAJ RADNE NALOGE OTPREME VOZA“ — klik → `toolStripButton1_Click` — proc: InsertRNOtpremaVozaCeoVoz; preko: InsertRN.InsRNOtpremaVozaCeoVoz
  - „POTVRDI ZAVRŠEN / ARHIVIRAJ“ — klik → `toolStripButton2_Click` — proc: PrenesiKontejnerTekuceUArhiv, UpdateRN2Uradjene; preko: InsertRN.ArhivirajKontejner, InsertRN.PotvrdiUradjenRN2
  - „POTVRDI ZAVRŠEN / ARHIVIRAJ“ — klik → `toolStripButton2_Click` — proc: PrenesiKontejnerTekuceUArhiv, UpdateRN2Uradjene; preko: InsertRN.ArhivirajKontejner, InsertRN.PotvrdiUradjenRN2
  - „FORMIRAJ RADNE NALOGE “ — klik → `toolStripButton1_Click` — proc: InsertRNOtpremaVozaCeoVoz; preko: InsertRN.InsRNOtpremaVozaCeoVoz
  - `button20` — klik → `tsDelete_Click` — proc: DeleteRNOtpremaVoza; preko: InsertRN.DelRNOtpremaVoza
  - `button21` — klik → `tsSave_Click` — proc: InsertRNOtpremaVoza, UpdateRNOtpremaVoza; preko: InsertRN.InsRNOtpremaVoza, InsertRN.UpdRNOtpremaVoza

#### RN4PrijemPlatforme [N+S]
- **Fajl:** `RadniNalozi/RN4PrijemPlatforme.cs` — „RN4 - PRIJEM PLATFORME KAMIONOM“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Otvori RN“ → **RN4PrijemPlatforme**
- **Stari izgled:** Terminal › Radni nalozi › RN4 - PRIJEM PLATFORME → **RN4PrijemPlatforme**
- **Otvara:** frmDodelaSkladista („Dodela polja“)
- **Tabele:** `InspekciskiTretman` S, `Partnerji` S, `Pozicija` S, `RadniNalogInterni` S, `RNPrijemPlatforme` SIUD p, `Skladista` S, `TipKontenjera` S, `Voz` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S
- **Procedure:** `DeleteRNPrijemPlatforme`, `InsertRN4PrPlatformeKamIzvoz`, `InsertRN4PrPlatformeKamUvoz`, `InsertRNPrijemPlatforme`, `UpdateRN4Uradjene`, `UpdateRN4UradjeneCIR`, `UpdateRN4UradjenePostavljen`, `UpdateRNPrijemPlatforme`
- **Combobox → tabela:** `cboUsluga` ← `RadniNalogInterni`, `VrstaManipulacije`; `cboVrstaKontejnera` ← `TipKontenjera`; `cboSaSredstva` ← `Voz`; `cboIzvoznik` ← `Partnerji`; `cboPostupak` ← `VrstaCarinskogPostupka`; `cboInspekcijski` ← `InspekciskiTretman`; `cboBrodar` ← `Partnerji`; `cboUsluga` ← `VrstaManipulacije`; `cboNaSklad` ← `Skladista`; `cboNaPoz` ← `Pozicija`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertRNPrijemPlatforme, UpdateRNPrijemPlatforme; preko: InsertRN.InsRNPPrijemPlatforme, InsertRN.UpdRNPPrijemPlatforme
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteRNPrijemPlatforme; preko: InsertRN.DelRNPPrijemPlatforme
  - „FORMIRAJ RADNI NALOG PRIJEM PLATFORMA“ — klik → `toolStripButton1_Click` — proc: InsertRN4PrPlatformeKamIzvoz, InsertRN4PrPlatformeKamUvoz; preko: InsertRN.InsRNPrijemPlatformeKamIzvoz, InsertRN.InsRNPrijemPlatformeKamUvoz
  - „Potvrdi završen vizuelni“ — klik → `toolStripButton3_Click` — proc: UpdateRN4Uradjene; preko: InsertRN.PotvrdiUradjenRN4
  - „Potvrdi završen CIR“ — klik → `toolStripButton4_Click` — proc: UpdateRN4UradjeneCIR; preko: InsertRN.PotvrdiUradjenRN4CIR
  - „Potvrdi premesten“ — klik → `toolStripButton2_Click` — proc: UpdateRN4UradjenePostavljen; preko: InsertRN.PotvrdiUradjenRN4Premesten
  - „Dodela polja“ — klik → `button2_Click` — otvara: frmDodelaSkladista
  - „POTVRDI ZAVRŠEN “ — klik → `toolStripButton2_Click` — proc: UpdateRN4UradjenePostavljen; preko: InsertRN.PotvrdiUradjenRN4Premesten
  - „POTVRDI ZAVRŠEN CIR“ — klik → `toolStripButton4_Click` — proc: UpdateRN4UradjeneCIR; preko: InsertRN.PotvrdiUradjenRN4CIR
  - „POTVRDI ZAVRŠEN VIZUELNI“ — klik → `toolStripButton3_Click` — proc: UpdateRN4Uradjene; preko: InsertRN.PotvrdiUradjenRN4
  - „FORMIRAJ RADNE NALOGE “ — klik → `toolStripButton1_Click` — proc: InsertRN4PrPlatformeKamIzvoz, InsertRN4PrPlatformeKamUvoz; preko: InsertRN.InsRNPrijemPlatformeKamIzvoz, InsertRN.InsRNPrijemPlatformeKamUvoz
  - `button20` — klik → `tsDelete_Click` — proc: DeleteRNPrijemPlatforme; preko: InsertRN.DelRNPPrijemPlatforme
  - `button21` — klik → `tsSave_Click` — proc: InsertRNPrijemPlatforme, UpdateRNPrijemPlatforme; preko: InsertRN.InsRNPPrijemPlatforme, InsertRN.UpdRNPPrijemPlatforme

#### RN5PrijemPlatforme2 [N+S]
- **Fajl:** `RadniNalozi/RN5PrijemPlatforme2.cs` — „RN5 - PRIJEM PLATFORME BRODAR“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Otvori RN“ → **RN5PrijemPlatforme2**
- **Stari izgled:** Terminal › Radni nalozi › RN5 - PRIJEM PLAT BRODAR → **RN5PrijemPlatforme2**
- **Tabele:** `NHM` S, `Partnerji` S, `Pozicija` S, `RadniNalogInterni` S, `RNPrijemPlatforme2` SIUD p, `Skladista` S, `TipKontenjera` S, `Voz` S, `VrstaManipulacije` S
- **Procedure:** `DeleteRNPrijemPlatforme2`, `InsertRN5PrPlatFormeKam`, `InsertRNPrijemPlatforme2`, `UpdateRN5Uradjene`, `UpdateRN5UradjeneCIR`, `UpdateRN5UradjeneS`, `UpdateRNPrijemPlatforme2`
- **Combobox → tabela:** `cboUsluga` ← `RadniNalogInterni`, `VrstaManipulacije`; `cboVrstaKontejnera` ← `TipKontenjera`; `cboSaSredstva` ← `Voz`; `cboBrodar` ← `Partnerji`; `cboVrstaRobe` ← `NHM`; `cboUsluga` ← `VrstaManipulacije`; `cboNaSklad` ← `Skladista`; `cboNaPoz` ← `Pozicija`
- **Funkcionalnosti:**
  - „Datum rasporeda“ — klik → `label1_Click`
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertRNPrijemPlatforme2, UpdateRNPrijemPlatforme2; preko: InsertRN.InsRNPPrijemPlatforme2, InsertRN.UpdRNPPrijemPlatforme2
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteRNPrijemPlatforme2; preko: InsertRN.DelRNPPrijemPlatforme2
  - „FORMIRAJ RADNI NALOG PLATFORMA BRODAR“ — klik → `toolStripButton1_Click` — proc: InsertRN5PrPlatFormeKam; preko: InsertRN.InsRN5PrijemPlatformeKam
  - „Ubaci u tekuce - NE TREBA“ — klik → `toolStripButton4_Click` — proc: UpdateRN5UradjeneS; preko: InsertRN.PotvrdiUradjenRN5S
  - „Potvrdi završen CIR“ — klik → `toolStripButton3_Click` — proc: UpdateRN5UradjeneCIR; preko: InsertRN.PotvrdiUradjenRN5CIR
  - „Potvrdi završen“ — klik → `toolStripButton2_Click` — proc: UpdateRN5Uradjene; preko: InsertRN.PotvrdiUradjenRN5
  - „ID“ — klik → `label1_Click`
  - „POTVRDI ZAVRŠEN “ — klik → `toolStripButton2_Click` — proc: UpdateRN5Uradjene; preko: InsertRN.PotvrdiUradjenRN5
  - „POTVRDI ZAVRŠEN CIR“ — klik → `toolStripButton3_Click` — proc: UpdateRN5UradjeneCIR; preko: InsertRN.PotvrdiUradjenRN5CIR
  - „FORMIRAJ RADNE NALOGE “ — klik → `toolStripButton1_Click` — proc: InsertRN5PrPlatFormeKam; preko: InsertRN.InsRN5PrijemPlatformeKam
  - `button20` — klik → `tsDelete_Click` — proc: DeleteRNPrijemPlatforme2; preko: InsertRN.DelRNPPrijemPlatforme2
  - `button21` — klik → `tsSave_Click` — proc: InsertRNPrijemPlatforme2, UpdateRNPrijemPlatforme2; preko: InsertRN.InsRNPPrijemPlatforme2, InsertRN.UpdRNPPrijemPlatforme2

#### RN6OtpremaPlatforme [N+S]
- **Fajl:** `RadniNalozi/RN6OtpremaPlatforme.cs` — „RN6 OTPREMA PLATFORME“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Otvori RN“ → **RN6OtpremaPlatforme**
- **Stari izgled:** Terminal › Radni nalozi › RN6 - OTPREMA PLATFORME → **RN6OtpremaPlatforme**
- **Tabele:** `KontejnerTekuce` S, `NHM` S, `Partnerji` S, `Pozicija` S, `RadniNalogInterni` S, `RNOtpremaPlatforme` SIUD p, `Skladista` S, `TipKontenjera` S, `UvozKonacnaNHM` S, `Voz` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S
- **Procedure:** `DeleteRNOtpremaPlatforme`, `InsertRN6OtPlatFormeKam`, `InsertRNOtpremaPlatforme`, `UpdateRN6Uradjene`, `UpdateRNOtpremaPlatforme`
- **Combobox → tabela:** `cboUsluga` ← `RadniNalogInterni`, `VrstaManipulacije`; `cboSpedicija` ← `Partnerji`; `cboPostupak` ← `VrstaCarinskogPostupka`; `cboVrstaKontejnera` ← `TipKontenjera`; `cboNaSredstvo` ← `Voz`; `cboUvoznik` ← `Partnerji`; `cboPostupak` ← `VrstaCarinskogPostupka`; `cboSpedicija` ← `Partnerji`; `cboBrodar` ← `Partnerji`; `cboVrstaRobe` ← `NHM`; `cboUsluga` ← `VrstaManipulacije`; `cboSaSklad` ← `Skladista`; `cboSaPoz` ← `Pozicija`; `cboIzvoznik` ← `Partnerji`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertRNOtpremaPlatforme, UpdateRNOtpremaPlatforme; preko: InsertRN.InsRNOtpremaPlatforme, InsertRN.UpdRNOtpremaPlatforme
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteRNOtpremaPlatforme; preko: InsertRN.DelRNOtpremaPlatforme
  - „FORMIRAJ RADNI NALOG OTPREMA PLATFORME“ — klik → `toolStripButton1_Click` — proc: InsertRN6OtPlatFormeKam; preko: InsertRN.InsRN6OtpremaPlatformeKam
  - „Potvrdi završen“ — klik → `toolStripButton2_Click` — proc: UpdateRN6Uradjene; preko: InsertRN.PotvrdiUradjenRN6
  - „Dodela polja“ — klik → `button2_Click_1`
  - „POTVRDI ZAVRŠEN “ — klik → `toolStripButton2_Click` — proc: UpdateRN6Uradjene; preko: InsertRN.PotvrdiUradjenRN6
  - „FORMIRAJ RADNE NALOGE “ — klik → `toolStripButton1_Click` — proc: InsertRN6OtPlatFormeKam; preko: InsertRN.InsRN6OtpremaPlatformeKam
  - `button20` — klik → `tsDelete_Click` — proc: DeleteRNOtpremaPlatforme; preko: InsertRN.DelRNOtpremaPlatforme
  - `button21` — klik → `tsSave_Click` — proc: InsertRNOtpremaPlatforme, UpdateRNOtpremaPlatforme; preko: InsertRN.InsRNOtpremaPlatforme, InsertRN.UpdRNOtpremaPlatforme

#### RN7OtpremaPlatforme2 [N+S]
- **Fajl:** `RadniNalozi/RN7OtpremaPlatforme2.cs` — „RN7 - OTPREMA PLATFORME“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Otvori RN“ → **RN7OtpremaPlatforme2**
- **Stari izgled:** Terminal › Radni nalozi › RN7 - OTP PLAT BRODAR → **RN7OtpremaPlatforme2**
- **Tabele:** `KontejnerTekuce` S, `NHM` S, `Partnerji` S, `Pozicija` S, `RadniNalogInterni` S, `RNOtpremaPlatforme2` SIUD p, `Skladista` S, `TipKontenjera` S, `Voz` S, `VrstaManipulacije` S
- **Procedure:** `DeleteRNOtpremaPlatforme2`, `InsertRN7OtpremaOtpremaPlatKam`, `InsertRNOtpremaPlatforme2`, `PrenesiKontejnerTekuceUArhiv`, `UpdateRN7Uradjene`, `UpdateRNOtpremaPlatforme2`
- **Combobox → tabela:** `cboUsluga` ← `RadniNalogInterni`, `VrstaManipulacije`; `cboVrstaKontejnera` ← `TipKontenjera`; `cboNaSredstvo` ← `Voz`; `cboBrodar` ← `Partnerji`; `cboVrstaRobe` ← `NHM`; `cboUsluga` ← `VrstaManipulacije`; `cboSaSklad` ← `Skladista`; `cboSaPoz` ← `Pozicija`
- **Funkcionalnosti:**
  - „Naziv brodara“ — klik → `txtnazivbrodara_Click`
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertRNOtpremaPlatforme2, UpdateRNOtpremaPlatforme2; preko: InsertRN.InsRNOtpremaPlatforme2, InsertRN.UpdRNOtpremaPlatforme2
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteRNOtpremaPlatforme2; preko: InsertRN.DelRNOtpremaPlatforme2
  - „FORMIRAJ OTPREMA PLATFORME - BRODAR“ — klik → `toolStripButton1_Click` — proc: InsertRN7OtpremaOtpremaPlatKam; preko: InsertRN.InsRN7OtpremaPlatformeKam
  - „Potvrdi završen“ — klik → `toolStripButton2_Click` — proc: UpdateRN7Uradjene; preko: InsertRN.PotvrdiUradjenRN7
  - „ARHIVIRAJ KONTEJNER“ — klik → `toolStripButton3_Click` — proc: PrenesiKontejnerTekuceUArhiv; preko: InsertRN.ArhivirajKontejner
  - „Kamion“ — klik → `label2_Click`
  - „Otprema ID“ — klik → `label5_Click`
  - „ARHIVIRAJ KONTEJNER“ — klik → `toolStripButton3_Click` — proc: PrenesiKontejnerTekuceUArhiv; preko: InsertRN.ArhivirajKontejner
  - „POTVRDI ZAVRŠEN “ — klik → `toolStripButton2_Click` — proc: UpdateRN7Uradjene; preko: InsertRN.PotvrdiUradjenRN7
  - „FORMIRAJ RADNE NALOGE “ — klik → `toolStripButton1_Click` — proc: InsertRN7OtpremaOtpremaPlatKam; preko: InsertRN.InsRN7OtpremaPlatformeKam
  - `button20` — klik → `tsDelete_Click` — proc: DeleteRNOtpremaPlatforme2; preko: InsertRN.DelRNOtpremaPlatforme2
  - `button21` — klik → `tsSave_Click` — proc: InsertRNOtpremaPlatforme2, UpdateRNOtpremaPlatforme2; preko: InsertRN.InsRNOtpremaPlatforme2, InsertRN.UpdRNOtpremaPlatforme2

#### RN8OtpremaCirade [S]
- **Fajl:** `RadniNalozi/RN8OtpremaCirade.cs` — „RN8 - OTPREMA CIRADE “
- **Stari izgled:** Terminal › Radni nalozi › RN8 - OTPREMA CIRADE → **RN8OtpremaCirade**
- **Tabele:** `NHM` S, `Partnerji` S, `Pozicija` S, `RadniNalogInterni` S, `RNOtpremaCirade` SIUD p, `Skladista` S, `UvozKonacnaNHM` S, `Voz` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S
- **Procedure:** `DeleteRNOtpremaCirade`, `InsertRN8OtpremaCiradeKam`, `InsertRNOtpremaCirade`, `UpdateRN8Uradjene`, `UpdateRN8UradjeneCIR`, `UpdateRNOtpremaCirade`
- **Funkcionalnosti:**
  - „Carinski postupak“ — klik → `txtcarinskipostupak_Click`
  - „Vrsta robe“ — klik → `txtvrstarobe_Click`
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertRNOtpremaCirade, UpdateRNOtpremaCirade; preko: InsertRN.InsRnOtpremaCirade, InsertRN.UpdRnOtpremaCirade
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteRNOtpremaCirade; preko: InsertRN.DelRnOtpremaCirade
  - „FORMIRAJ RADNI NALOG OTPREMA CIRADE“ — klik → `toolStripButton1_Click` — proc: InsertRN8OtpremaCiradeKam; preko: InsertRN.InsRN8OtpremaCiradeKam
  - „Potvrdi završen“ — klik → `toolStripButton2_Click` — proc: UpdateRN8Uradjene; preko: InsertRN.PotvrdiUradjenRN8
  - „OTPREMNICA ROBE“ — klik → `toolStripButton3_Click` — otvara: Otpremnica
  - „OTPREMNICA ROBE PREGLED“ — klik → `toolStripButton5_Click` — otvara: OtpremnicaPregled
  - „Potvrdi završen CIR“ — klik → `toolStripButton6_Click` — proc: UpdateRN8UradjeneCIR; preko: InsertRN.PotvrdiUradjenRN8CIr
  - „POTVRDI ZAVRŠEN CIR“ — klik → `toolStripButton6_Click` — proc: UpdateRN8UradjeneCIR; preko: InsertRN.PotvrdiUradjenRN8CIr
  - „POTVRDI ZAVRŠEN “ — klik → `toolStripButton2_Click` — proc: UpdateRN8Uradjene; preko: InsertRN.PotvrdiUradjenRN8
  - „FORMIRAJ RADNE NALOGE “ — klik → `toolStripButton1_Click` — proc: InsertRN8OtpremaCiradeKam; preko: InsertRN.InsRN8OtpremaCiradeKam
  - `button20` — klik → `tsDelete_Click` — proc: DeleteRNOtpremaCirade; preko: InsertRN.DelRnOtpremaCirade
  - `button21` — klik → `tsSave_Click` — proc: InsertRNOtpremaCirade, UpdateRNOtpremaCirade; preko: InsertRN.InsRnOtpremaCirade, InsertRN.UpdRnOtpremaCirade

#### RN9PrijemCirade [S]
- **Fajl:** `RadniNalozi/RN9PrijemCirade.cs` — „RN PRIJEM CIRADE“
- **Stari izgled:** Terminal › Radni nalozi › RN9 - PRIJEM CIRADE → **RN9PrijemCirade**
- **Tabele:** `InspekciskiTretman` S, `NHM` S, `Partnerji` S, `Pozicija` S, `RNPrijemCirade` SIUD p, `Skladista` S, `TipKontenjera` S, `Voz` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S
- **Procedure:** `DeleteRNPrijemCirade`, `InsertRN9PrijemCiradeKam`, `InsertRNPrijemCirade`, `UpdateRN9Uradjen`, `UpdateRNPrijemCirade`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: InsertRNPrijemCirade, UpdateRNPrijemCirade; preko: InsertRN.InsRNPPrijemCirade, InsertRN.UpdRNPPrijemCirade
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteRNPrijemCirade; preko: InsertRN.DelRNPPrijemCirade
  - „FORMIRAJ RADNE NALOGE  ZA PRIJEM CIRADE“ — klik → `toolStripButton1_Click` — proc: InsertRN9PrijemCiradeKam; preko: InsertRN.InsRN9PrijmCiradeKam
  - „POTVRDI ZAVRŠEN“ — klik → `toolStripButton4_Click` — proc: UpdateRN9Uradjen; preko: InsertRN.PotvrdiUradjenRN9
  - „Prijemnica robe“ — klik → `toolStripButton5_Click` — otvara: Prijemnica
  - „Pregled prijema robe“ — klik → `toolStripButton3_Click` — otvara: PrijemnicaPregled
  - „POTVRDI ZAVRŠEN “ — klik → `toolStripButton4_Click` — proc: UpdateRN9Uradjen; preko: InsertRN.PotvrdiUradjenRN9
  - „FORMIRAJ RADNE NALOGE “ — klik → `toolStripButton1_Click` — proc: InsertRN9PrijemCiradeKam; preko: InsertRN.InsRN9PrijmCiradeKam
  - `button20` — klik → `tsDelete_Click` — proc: DeleteRNPrijemCirade; preko: InsertRN.DelRNPPrijemCirade
  - `button21` — klik → `tsSave_Click` — proc: InsertRNPrijemCirade, UpdateRNPrijemCirade; preko: InsertRN.InsRNPPrijemCirade, InsertRN.UpdRNPPrijemCirade

#### SkladistaMain [N]
- **Fajl:** `Skladista main/SkladistaMain.cs` — „SkladistaMain“
- **Novi izgled:** Operacije skladišta → **SkladistaMain**
- **Otvara:** NalogZaKomercijalni (kartica „Radni nalozi“), CarinskoSkladiste („Carinsko skladište“), KomercijalnoSkladiste („Komercijalno skladište“)
- **Funkcionalnosti:**
  - „Komercijalni nalozi“ — klik → `btnRadniNalozi_Click` — otvara: NalogZaKomercijalni
  - „Komercijalno skladište“ — klik → `btnKomerijalnoSkladiste_Click` — otvara: KomercijalnoSkladiste
  - „Carinsko skladište“ — klik → `btnCarinskoSkladiste_Click` — otvara: CarinskoSkladiste

#### SkladisteGrupa [N+S]
- **Fajl:** `TrackModal/Sifarnici/SkladisteGrupa.cs` — „Lokacije“
- **Novi izgled:** Podešavanja sistema → **Podesavanje1** → „Lokacije“ → **SkladisteGrupa**
- **Stari izgled:** Terminal › Šifarnici › Lokacije → **SkladisteGrupa**
- **Tabele:** `Carinarnice` S, `SkladistaGrupa` SIU p, `TipSkladista` S, `TipZone` S, `Zone` S
- **Procedure:** `DeleteSkladisteGrupa`, `InsertSkladistaGrupa`, `UpdateSkladistaGrupa`
- **Combobox → tabela:** `cboCarinskaIspostava` ← `TipSkladista`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — čita: Zone; proc: InsertSkladistaGrupa, UpdateSkladistaGrupa; preko: InsertSkladistaGrupa.InsLokacija, InsertSkladistaGrupa.UpdLokacija
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteSkladisteGrupa; preko: InsertSkladistaGrupa.DeleteLokacija
  - `button20` — klik → `tsDelete_Click` — proc: DeleteSkladisteGrupa; preko: InsertSkladistaGrupa.DeleteLokacija
  - `button21` — klik → `tsSave_Click` — čita: Zone; proc: InsertSkladistaGrupa, UpdateSkladistaGrupa; preko: InsertSkladistaGrupa.InsLokacija, InsertSkladistaGrupa.UpdLokacija
  - „gridGroupingControl1“ — SelectedRecordsChanged → `gridGroupingControl1_SelectedRecordsChanged`
  - „Carinsko skladište“ — čekiranje → `chkCarinsko_CheckedChanged`
  - „Neaktivne“ — klik → `button1_Click`
  - „Aktivne“ — klik → `button23_Click`
  - „Refresh“ — klik → `button2_Click`

#### TerminalMapFRM [N]
- **Fajl:** `TerminalMap/TerminalMapFRM.cs` — „TerminalMapFRM“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Depo cnt“ → **frmPrijemIOtpremaVozova2** → „Pregled skladišta“ → **TerminalMapFRM**
- **Otvara:** frmKontejnerTekuce („Terminal central“), frmRN12InterniPrenos („Interni prenos“), frmDodatneUsluge („Dodatne usluge“), frmDefinisiPoziciju („Definiši/Predefiniši“)
- **Tabele:** `KontejnerStatus` S, `KontejnerTekuce` S, `Partnerji` S, `Pozicija` S, `Skladista` SU, `SkladistaGrupa` S, `TipKontenjera` S, `uvKvalitetKontejnera` S, `Zone` S
- **Funkcionalnosti:**
  - „Pozadina:ON“ — klik → `pozadinaONToolStripMenuItem_Click`
  - „Skladište:“ — klik → `lblHangar_Click`
  - „Aktivan“ — čekiranje → `cbAktivan_CheckStateChanged`
  - „Dodatne usluge“ — klik → `sfButton5_Click` — otvara: frmDodatneUsluge
  - „Prazno“ — čekiranje → `cbPrazno_CheckStateChanged`
  - „Interni prenos“ — klik → `sfButton4_Click` — otvara: frmRN12InterniPrenos
  - „Terminal central“ — klik → `sfButton3_Click`
  - „Popis cnt detaljan“ — klik → `sfButton2_Click`
  - „Definiši/Predefiniši“ — klik → `sfButton1_Click` — otvara: frmDefinisiPoziciju; čita: Skladista
  - „terminalMapControl1“ — klik → `map1_Click`
  - „Edit: OFF“ — klik → `BtnEditMode_Click`
  - „Save regions“ — klik → `BtnSaveRegions_Click`
  - „Reset view“ — klik → `BtnResetViews_Click`

#### TestiranjeStampa [S]
- **Fajl:** `Testiranje/TestiranjeStampa.cs` — „Rezultati testiranja“
- **Stari izgled:** Kadrovi › Testiranje radnika › Štampa rezultata → **TestiranjeStampa**
- **Tabele:** `Korisnici` S, `testovi` S
- **Funkcionalnosti:**
  - „Štampaj rezultat“ — klik → `sfButton1_Click` — izveštaj: rptTestiranjeIzvestaj.rdlc
  - „Štampaj Z8“ — klik → `sfButton2_Click` — izveštaj: rptZ8.rdlc
  - `this` — otvaranje forme → `TestiranjeStampa_Load` — čita: Korisnici, Testovi

#### TipSkladista [N]
- **Fajl:** `Skladista main/TipSkladista.cs` — „TipSkladista“
- **Novi izgled:** Operacije skladišta → **SkladistaMain** → kartica „Radni nalozi“ → **NalogZaKomercijalni** → „Kreiraj novi“ → **TipSkladista**
- **Otvara:** RadniNaloziSkladista („Carinsko skladište“)
- **Funkcionalnosti:**
  - „Komercijalno skladište“ — klik → `btnKomercijalnoSkladiste_Click` — otvara: RadniNaloziSkladista
  - „Carinsko skladište“ — klik → `btnCarinskoSkladiste_Click` — otvara: RadniNaloziSkladista

#### UlazneFakture [S]
- **Fajl:** `Pantheon Export/UlazneFakture.cs` — „Ulazne fakture“
- **Stari izgled:** Komercijala › CRM › Ulazne fakture → **UlazneFakture**
- **Tabele:** `Delavci` S, `Faktura` S, `Korisnici` S, `MAticniPodatki` S, `MerskeEnote` S, `Najava` S, `NosiociTroskova` S, `Partnerji` S, `Predvidjanje` S, `UlFak` SIU p, `UlFakPostav` SID p, `Valute` S
- **Procedure:** `DeleteUlFakPostav`, `InsertUlFak`, `InsertUlFakPostav`, `PoveziPredvidjanjeFaktura`, `UpdateUlFak`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click` — čita: UlFak, faktura, ulfak
  - „tsSave“ — klik → `tsSave_Click` — čita: Partnerji; proc: InsertUlFak, UpdateUlFak; preko: InsertPatheonExport.InsUlFak, InsertPatheonExport.UpdUlFak
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „Obriši“ — klik → `btnObrisi_Click` — proc: DeleteUlFakPostav; preko: InsertPatheonExport.DelUlFakPostav
  - „Dodaj“ — klik → `button1_Click` — proc: InsertUlFakPostav; preko: InsertPatheonExport.InsUlFakPostav
  - „Povuci iz predvidjanja“ — klik → `button2_Click` — piše: UlFakPostav[D]; čita: Predvidjanje, UlFak; proc: PoveziPredvidjanjeFaktura; preko: InsertPatheonExport.PoveziPredvidjanje

#### UlFakPregled [S]
- **Fajl:** `Pantheon Export/UlFakPregled.cs` — „Pregled ulaznih faktura“
- **Stari izgled:** Komercijala › CRM › Ulazne fakture pregled → **UlFakPregled**
- **Otvara:** Valuta („Export Pantheon“), UlazneFakture („Otvori“)
- **Tabele:** `ApiLogovi` I p, `Delavci` S, `MAticniPodatki` S, `NosiociTroskova` S, `Partnerji` S, `Predvidjanje` S, `UlFak` SUD p, `UlFakPostav` S
- **Procedure:** `DeleteUlFak`, `InsertApiLogovi`
- **Funkcionalnosti:**
  - `dataGridView1` — ColumnHeaderMouseClick → `dataGridView1_ColumnHeaderMouseClick` — čita: Delavci, NosiociTroskova, Partnerji, Predvidjanje, UlFak, UlFakPostav
  - „Otvori“ — klik → `button1_Click` — otvara: UlazneFakture
  - „Nova“ — klik → `button2_Click` — otvara: UlazneFakture
  - „Get placanja“ — klik → `btnGetPlacanja_Click` — piše: UlFak[U]
  - „Export Pantheon“ — klik → `btnExportProd_Click` — otvara: Valuta; čita: Partnerji
  - „Obriši“ — klik → `btnObrisi_Click` — proc: DeleteUlFak; preko: InsertPatheonExport.DeleteUlFak
  - „Refresh“ — klik → `button5_Click` — čita: Delavci, NosiociTroskova, Partnerji, Predvidjanje, UlFak, UlFakPostav
  - `button4` — klik → `button4_Click` — čita: Delavci, NosiociTroskova, Partnerji, Predvidjanje, UlFak, UlFakPostav
  - `button3` — klik → `button3_Click` — čita: Delavci, NosiociTroskova, Partnerji, Predvidjanje, UlFak, UlFakPostav
  - `btnFilterNT` — klik → `btnFilterNT_Click` — čita: Delavci, NosiociTroskova, Partnerji, Predvidjanje, UlFak, UlFakPostav

#### Uvoz [S]
- **Fajl:** `Uvoz/Uvoz.cs` — „NERASPOREDJENI KONTEJNERI“
- **Stari izgled:** Uvoz › Uvoz obrada › Neraspoređeni kontejneri - Obrazac → **Uvoz**
- **Otvara:** frmKontaktOsobe (`button14` (Click)), UvozDokumenta (prečica na tastaturi (`Uvoz_KeyDown`)), UvozTable (prečica na tastaturi (`Uvoz_KeyDown`)), frmUnosManipulacija (prečica na tastaturi (`Uvoz_KeyDown`)), frmKontaktOsobeMU (`button18` (Click)), UvozNHM („NHM/HS KODOVI“), frmUvozUvoznici („Uvezi spisak“), UvozKonacnaPronadji („Pronadji kontejner“), frmPregledKontejneraDrumskeUsluge („Drumske usluge“)
- **Tabele:** `Brodovi` S, `Carinarnice` S, `Cene` S, `DirigacijaKontejneraZa` S, `InspekciskiTretman` S, `KontejnerskiTerminali` S, `KontejnerStatus` S, `MestaUtovara` S, `Nalogodavci` S, `NapomenaZaPozicioniranje` S, `NHM` S, `organizacioneJedinice` S, `Partnerji` S, `partnerjiKontOseba` S, `partnerjiKontOsebaMU` S, `Predefinisaneporuke` S, `RadniNalogInterni` I p, `Scenario` S, `TipKontenjera` S, `uvNacinPakovanja` S, `Uvoz` SIUD p, `UvozKonacna` I p, `UvozKonacnaZaglavlje` S, `UvozNapomenePozicioniranja` SID p, `UvozNHM` SD p, `UvozUvoznici` S, `UvozVrstaManipulacije` SI p, `UvozVrstaRobeHS` SID p, `Voz` S, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaRobeADR` S, `VrstaRobeHS` S, `VrstePostupakaUvoz` S
- **Procedure:** `DeleteUvoz`, `DeleteUvozNapomenePozicioniranja`, `DeleteUvozNHM`, `DeleteUvozTerminalskeManipulacije`, `DeleteUvozVrstaManipulacijeKamionska`, `DeleteUvozVrstaManTerminalskih`, `DeleteUvozVrstaRobeHS`, `InsertRadniNalogInterni`, `InsertUvozKonacna`, `InsertUvozNapomenePozicioniranja`, `InsertUvozVrstaManipulacije`, `InsertUvozVrstaRobeHS`, `PrebaciNHMUvozUvozKonacna`, `PrebaciVrsterobeHSUvozUvozKonacna`, `PrenesiUPlanUtovaraSelektovano`, `UpdateScenarioKontejnera`
- **Funkcionalnosti:**
  - „toolStrip1“ — ItemClicked → `toolStrip1_ItemClicked`
  - „Novi“ — klik → `tsNew_Click` — piše: Uvoz[I]
  - „tsSave“ — klik → `tsSave_Click` — proc: UpdateUvoz; preko: InsertUvoz.UpdUvoz
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteUvoz; preko: InsertUvoz.DelUvoz
  - „Dokumentacija-SHD“ — klik → `toolStripButton6_Click` — otvara: UvozDokumenta
  - „Proglasi konacno“ — klik → `toolStripButton4_Click` — piše: Uvoz[D]; proc: InsertUvozKonacna, PrebaciNHMUvozUvozKonacna, PrebaciVrsterobeHSUvozUvozKonacna; preko: InsertUvozKonacna.InsUvozKonacna, InsertUvozKonacna.PrebaciNHMUvozUvozKonacna, InsertUvozKonacna.PrebaciVrsterobeHSUvozUvozKonacna
  - „Usluge-SHU“ — klik → `toolStripButton2_Click_1` — otvara: frmUnosManipulacija
  - „Uvoz konačne“ — klik → `toolStripButton5_Click` — otvara: frmUvozKonacna
  - „Tabela-SHT“ — klik → `toolStripButton1_Click_1` — otvara: UvozTable
  - „Ažuriraj šifarnike“ — klik → `toolStripButton3_Click_1`
  - „Napravi usluge SC15“ — klik → `toolStripButton7_Click` — proc: InsertRadniNalogInterni, InsertUvozVrstaManipulacije, PrenesiUPlanUtovaraSelektovano; preko: InsertRadniNalogInterni.InsRadniNalogInterni, InsertUvozKonacna.InsUbaciUslugu, InsertUvozKonacna.PrenesiUPlanUtovara
  - „Prenesi u plan“ — klik → `toolStripButton8_Click` — proc: PrenesiUPlanUtovaraSelektovano; preko: InsertUvozKonacna.PrenesiUPlanUtovara
  - „Drumske usluge“ — klik → `button29_Click` — otvara: frmPregledKontejneraDrumskeUsluge
  - „Pronadji kontejner“ — klik → `button17_Click_1` — otvara: UvozKonacnaPronadji
  - „SC15 - Napravi usluge“ — klik → `button27_Click` — proc: InsertRadniNalogInterni, InsertUvozVrstaManipulacije, PrenesiUPlanUtovaraSelektovano, UpdateScenarioKontejnera; preko: InsertRadniNalogInterni.InsRadniNalogInterni, InsertScenario.UpdScenarioKontejnera, InsertUvozKonacna.InsUbaciUslugu, InsertUvozKonacna.PrenesiUPlanUtovara
  - „Ažuriraj šifarnike“ — klik → `button26_Click`
  - „Tabela“ — klik → `button25_Click` — otvara: UvozTable
  - „Usluge“ — klik → `button24_Click` — otvara: frmUnosManipulacija
  - „Dokumentacija“ — klik → `button23_Click` — otvara: UvozDokumenta
  - `button20` — klik → `button20_Click` — proc: DeleteUvoz; preko: InsertUvoz.DelUvoz
  - `button21` — klik → `button21_Click` — proc: UpdateUvoz; preko: InsertUvoz.UpdUvoz
  - `button22` — klik → `button22_Click` — piše: Uvoz[I]
  - „Unesi“ — klik → `button33_Click`
  - „Moguci“ — klik → `button32_Click` — čita: Scenario
  - „Drumska usluga“ — čekiranje → `chkDrumskaUsluga_CheckedChanged` — proc: DeleteUvozVrstaManipulacijeKamionska, InsertUvozVrstaManipulacije; preko: InsertUvozKonacna.DelUvozUslugaKamionska, InsertUvozKonacna.InsUbaciUslugu
  - „Dobijen“ — klik → `chkDobijenNalogodavac3_Click`
  - „Dobijen“ — klik → `chkDobijenNalogodavac2_Click`
  - „Dobijen nalog brodara“ — klik → `chkDobijenNalogBrodara_Click`
  - „Dobijen“ — klik → `chkDobijenNalogodavac1_Click`
  - „Uvezi spisak“ — klik → `button31_Click` — otvara: frmUvozUvoznici
  - „NHM/HS KODOVI“ — klik → `button30_Click` — otvara: UvozNHM
  - „ODOBREN POČETAK – BEZ DODATNIH INSTRUKCIJA “ — čekiranje → `chkPotvrdioKlijent2BDI_CheckedChanged`
  - „OPŠTI“ — klik → `button10_Click`
  - „V V V V V“ — klik → `button9_Click`
  - „NPP V V“ — klik → `button8_Click`
  - „Unesi“ — klik → `button1_Click`
  - „Izbrisi“ — klik → `button3_Click` — proc: DeleteUvozNHM; preko: InsertUvozKonacna.DelUvozNHM
  - „Interni“ — čekiranje → `chkInterni_CheckedChanged`
  - „Interni“ — klik → `chkInterni_Click`
  - „Obrni“ — klik → `button11_Click` — čita: NHM
  - … još 22

#### UvozDokumenta [N+S]
- **Fajl:** `Uvoz/UvozDokumenta.cs` — „Uvoz dokumenta“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Dodatne usluge“ → **frmDodatneUsluge** → „Dodeli slike“ → **UvozDokumenta**
- **Stari izgled:** Uvoz › Uvoz obrada › Neraspoređeni kontejneri - Obrazac → **Uvoz** → prečica na tastaturi (`Uvoz_KeyDown`) → **UvozDokumenta**
- **Tabele:** `TipKomercijalnogDokumenta` S, `Uvoz` S, `UvozDokumenta` SID p, `UvozDokumentaCeoVoz` SID p, `UvozDokumentaUsluge` SID p, `UvozKonacna` S, `UvozKonacnaVrstaManipulacije` S, `UvozVrstaManipulacije` S, `VrstaManipulacije` S
- **Procedure:** `DeleteUvozDokumenta`, `DeleteUvozDokumentaCeoVoz`, `DeleteUvozDokumentaUsluge`, `InsertUvozDokumenta`, `InsertUvozDokumentaCeoVoz`, `InsertUvozDokumentaUsluge`
- **Combobox → tabela:** `cboTipDokumenta` ← `TipKomercijalnogDokumenta`
- **Funkcionalnosti:**
  - „Novi“ — klik → `tsNew_Click`
  - „Dokumentacija“ — klik → `toolStripButton1_Click`
  - „Izbrisi“ — klik → `button3_Click` — proc: DeleteUvozDokumenta, DeleteUvozDokumentaCeoVoz, DeleteUvozDokumentaUsluge; preko: InsertUvozDokumenta.DelUvozDokumenta, InsertUvozDokumenta.DelUvozDokumentaCeoVoz, InsertUvozDokumenta.DelUvozDokumentaUsluga
  - „Sacuvaj“ — klik → `button4_Click` — proc: InsertUvozDokumenta, InsertUvozDokumentaCeoVoz, InsertUvozDokumentaUsluge; preko: InsertUvozDokumenta.InsUvozDokumenta, InsertUvozDokumenta.InsUvozDokumentaCeoVoz, InsertUvozDokumenta.InsUvozDokumentaUsluga
  - „Otvori“ — klik → `button2_Click`
  - „Pronadji“ — klik → `button1_Click`
  - „Dokument se odnosi na ceo voz“ — čekiranje → `chkZaVoz_CheckedChanged_1`

#### UvozKonacnaPronadji [S]
- **Fajl:** `Uvoz/UvozKonacnaPronadji.cs` — „Pronalayak u istoriji“
- **Stari izgled:** Uvoz › Uvoz obrada › Neraspoređeni kontejneri - Obrazac → **Uvoz** → „Pronadji kontejner“ → **UvozKonacnaPronadji**
- **Tabele:** `Partnerji` S, `UvozKonacna` S, `UvozKonacnaNHM` S, `UvozUvoznici` S
- **Procedure:** `KopirajKontejnerIzKonacne`
- **Funkcionalnosti:**
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - „Kopiraj“ — klik → `button1_Click` — proc: KopirajKontejnerIzKonacne; preko: InsertUvoz.KopirajKontejnerIzKonacne
  - „Refresh“ — klik → `button25_Click`
  - `this` — otvaranje forme → `UvozKonacnaPronadji_Load` — čita: Partnerji

#### UvozNHM [N+S]
- **Fajl:** `Uvoz/UvozNHM.cs` — „UvozNHM“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Voz“ → **frmPrijemIOtpremaVozova4** → „Vozovi“ → **frmVoz** → „UVOZ - FORMIRANJE PLANA“ → **frmFormiranjePlana** → „Otvori plan“ → **frmUvozKonacna** → „NHM/HS KODOVI“ → **UvozNHM**
- **Stari izgled:** Uvoz › Uvoz obrada › Neraspoređeni kontejneri - Obrazac → **Uvoz** → „NHM/HS KODOVI“ → **UvozNHM**
- **Tabele:** `NHM` S, `UvozKonacnaNHM` SD p, `UvozNHM` SID p
- **Procedure:** `DeleteUvozKonacnaNHM`, `DeleteUvozkonacnaNHMDiana`, `DeleteUvozNHM`, `DeleteUvozNHMDiana`, `InsertUvozKonacnaNHMDiana`, `InsertUvozNHM`, `InsertUvozNHMDiana`, `UpdateUvozNHMTezine`
- **Combobox → tabela:** `cboNHM` ← `NHM`; `cboNHM` ← `NHM`; `cboNHM` ← `NHM`; `cboNHM` ← `NHM`; `cboNHM` ← `NHM`; `cboNHM` ← `NHM`; `cboNHM` ← `NHM`; `cboNHM` ← `NHM`; `cboNHM` ← `NHM`
- **Funkcionalnosti:**
  - „Opšti“ — čekiranje → `chkOpsti_CheckedChanged`
  - „Obrni“ — klik → `button11_Click` — čita: NHM
  - „Interni“ — čekiranje → `chkInterni_CheckedChanged`
  - „Unesi“ — klik → `button1_Click` — proc: InsertUvozNHM; preko: InsertUvozKonacna.InsUvozNHM
  - „Izbrisi“ — klik → `button3_Click` — proc: DeleteUvozKonacnaNHM, DeleteUvozNHM; preko: InsertUvozKonacna.DelUvozKonacnaNHM, InsertUvozKonacna.DelUvozNHM
  - „Upload“ — klik → `button2_Click` — izvoz u Excel
  - „Učitaj“ — klik → `button4_Click` — proc: InsertUvozKonacnaNHMDiana, InsertUvozNHMDiana, UpdateUvozNHMTezine; preko: InsertUvozKonacna.InsUvozKonacnaNHMDiana, InsertUvozKonacna.InsUvozNHMDiana, InsertUvozKonacna.UpdUvozNHMTezine
  - „Izbriši učitane“ — klik → `button5_Click` — proc: DeleteUvozNHMDiana, DeleteUvozkonacnaNHMDiana; preko: InsertUvozKonacna.DelUvozKonacnaNHMDiana, InsertUvozKonacna.DelUvozNHMDiana
  - `this` — otvaranje forme → `UvozNHM_Load` — čita: NHM

#### UvozTable [S]
- **Fajl:** `Uvoz/UvozTable.cs` — „TABELA NERASPOREDJENIH KONTEJNERA“
- **Stari izgled:** Uvoz › Uvoz obrada › Neraspoređeni kontejneri - Obrazac → **Uvoz** → prečica na tastaturi (`Uvoz_KeyDown`) → **UvozTable**
- **Otvara:** frmUvozKopirajKontejner („Kopiranje“)
- **Tabele:** `Brodovi` S, `Carinarnice` S, `DirigacijaKontejneraZa` S, `InspekciskiTretman` S, `KontejnerskiTerminali` S, `MestaUtovara` S, `Partnerji` S, `partnerjiKontOsebaMU` S, `Predefinisaneporuke` S, `Scenario` S, `TipKontenjera` S, `uvNacinPakovanja` S, `Uvoz` SU, `UvozNapomenePozicioniranja` S, `VrstaCarinskogPostupka` S, `VrstaRobeADR` S, `VrstePostupakaUvoz` S
- **Funkcionalnosti:**
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - „Primeni “ — klik → `button2_Click`
  - „Izberi“ — klik → `button1_Click`
  - „Refresh“ — klik → `button3_Click`
  - „Kopiranje “ — klik → `button25_Click` — otvara: frmUvozKopirajKontejner

#### UvozZapisniciONepravilnostima [S]
- **Fajl:** `Uvoz/UvozZapisniciONepravilnostima.cs` — „Zapisnici“
- **Stari izgled:** Uvoz › Uvoz obrada › Zapisnici o nepravilnostima/ostećenima → **UvozZapisniciONepravilnostima**
- **Tabele:** `RadniNalogInterni` S, `UvozKonacna` S, `ZapisnikOOstecenju` S
- **Funkcionalnosti:**
  - „Refresh“ — klik → `button25_Click` — čita: RadniNalogInterni, UvozKonacna, ZapisnikOOstecenju; izvoz u Excel

#### Vaganje [N+S]
- **Fajl:** `Izvoz/Vaganje.cs` — „VAGANJE“
- **Novi izgled:** Prijem i otprema kamiona → **frmPrijemIOtpremaKamiona1** → „PLATFORMA“ → **frmPrijemIOtpremaPlatforma** → „Vaga“ → **VaganjePregled** → „Otvori“ → **Vaganje**
- **Stari izgled:** Terminal › Skladišno poslovanje › VAGANJE PREGLED → **VaganjePregled** → „Otvori“ → **Vaganje**
- **Otvara:** IzvestajVaganja („Izveštaj“)
- **Tabele:** `IzvozKonacna` S, `KontejnerLog` I p, `MestaUtovara` S, `RadniNalogInterni` S, `TipKontenjera` S, `Vaganje` SIU p
- **Procedure:** `InsertKontejnerLog`, `InsertVaganje`, `UpdateIzvozKonacnaVaganjeCirada`, `UpdateIzvozKonacnaVaganjePlatforma`, `UpdateVaganje`
- **Combobox → tabela:** `cbovrstakontejnera` ← `TipKontenjera`
- **Funkcionalnosti:**
  - „Potvrdi završeno vaganje“ — klik → `btnZavrsenoVaganje_Click`
  - „Izveštaj“ — klik → `button23_Click` — otvara: IzvestajVaganja
  - `button21` — klik → `button21_Click` — proc: InsertVaganje, UpdateIzvozKonacnaVaganjeCirada, UpdateIzvozKonacnaVaganjePlatforma, UpdateVaganje; preko: InsertIzvoz.InsVaganje, InsertIzvoz.UpdVaganje, InsertIzvoz.UpdateIzvozKonacnaCirada, InsertIzvoz.UpdateIzvozKonacnaPlatforma
  - `this` — otvaranje forme → `Vaganje_Load` — čita: TipKontenjera

#### VaganjePregled [N+S]
- **Fajl:** `Izvoz/VaganjePregled.cs` — „Kontejneri za vaganje“
- **Novi izgled:** Prijem i otprema kamiona → **frmPrijemIOtpremaKamiona1** → „PLATFORMA“ → **frmPrijemIOtpremaPlatforma** → „Vaga“ → **VaganjePregled**
- **Stari izgled:** Terminal › Skladišno poslovanje › VAGANJE PREGLED → **VaganjePregled**
- **Otvara:** Vaganje („Otvori“)
- **Tabele:** `IzvozKonacna` S, `RadniNalogInterni` S, `RadniNalogInterniPotvrda` S, `Vaganje` S
- **Funkcionalnosti:**
  - „Refresh“ — klik → `toolStripButton1_Click`
  - „Otvori“ — klik → `toolStripButton2_Click` — otvara: Vaganje
  - „Izvagani kontejneri“ — klik → `toolStripButton3_Click` — čita: IzvozKonacna, Vaganje
  - „Izvagani kontejneri“ — klik → `button5_Click` — čita: IzvozKonacna, Vaganje
  - „Otvori“ — klik → `button4_Click` — otvara: Vaganje
  - „Refresh“ — klik → `button23_Click`

#### Valuta [S]
- **Fajl:** `Pantheon Export/Valuta.cs` — „Valuta“
- **Stari izgled:** Komercijala › CRM › Ulazne fakture pregled → **UlFakPregled** → „Export Pantheon“ → **Valuta**
- **Tabele:** `ApiLogovi` I p, `Faktura` SU, `FakturaPostav` S, `Izjave` S, `MAticniPodatki` S, `MerskeEnote` S, `Najava` U, `NosiociTroskova` S, `Partnerji` S, `Predvidjanje` S, `UlFak` SU, `UlFakPostav` S
- **Procedure:** `InsertApiLogovi`
- **Funkcionalnosti:**
  - „RSD“ — klik → `btnRSD_Click` — piše: Faktura[U], Najava[U], UlFak[U]; čita 8 tabela; proc: InsertApiLogovi; preko: InsertPatheonExport.InsApiLog
  - „button1“ — klik → `btnValuta_Click` — piše: Faktura[U], Najava[U], UlFak[U]; čita 8 tabela; proc: InsertApiLogovi; preko: InsertPatheonExport.InsApiLog

#### VanGabaritni [N]
- **Fajl:** `MainLeget/Drumski/VanGabaritni.cs` — „VanGabaritni“
- **Novi izgled:** Drumski transport → **Drumski1** → „Van Gabaritni“ → **VanGabaritni**
- **Otvara:** frmStatus („STATUSI VOZILA“), frmPodesavanjeRaspolozivosti („PROVERA \r\nRASPOLOŽIVOSTI VOZILA“), PakovanjeKamiona1 („FORMIRANJE\r\nTRANSPORTNOG NALOGA“), NalogZaDrumski („NALOG ZA DRUMSKI TRANSPORT“), frmAutomobiliDrumski („UNOS PREVOZNIK/KAMIONER“)
- **Funkcionalnosti:**
  - „NALOG ZA DRUMSKI TRANSPORT“ — klik → `btnNalogDrumski_Click` — otvara: NalogZaDrumski
  - „UNOS PREVOZNIK/KAMIONER“ — klik → `btnPonude_Click` — otvara: frmAutomobiliDrumski
  - „FORMIRANJE\r\nTRANSPORTNOG NALOGA“ — klik → `btnFormiranjeNaloga_Click` — otvara: PakovanjeKamiona1
  - „PROVERA \r\nRASPOLOŽIVOSTI VOZILA“ — klik → `btnProvera_Click` — otvara: frmPodesavanjeRaspolozivosti
  - „STATUSI VOZILA“ — klik → `btnStatusi_Click` — otvara: frmStatus

#### VanredniPoslovi [N]
- **Fajl:** `Skladista main/Dokumenta/VanredniPoslovi.cs` — „VanredniPoslovi“
- **Novi izgled:** Operacije skladišta → **SkladistaMain** → kartica „Radni nalozi“ → **NalogZaKomercijalni** → „Kreiraj novi“ → **TipSkladista** → „Carinsko skladište“ → **RadniNaloziSkladista** → „Vanredni poslovi“ → **VanredniPoslovi**
- **Tabele:** `Carinarnice` S, `MagacinskiBrojCarinski` SIU p, `MestaUtovara` S, `Partnerji` S, `RadniNalogSkladista` SIU p, `RNCarinskoSkladisteDodatneUsluge` SID p, `VrstaCarinskogPostupka` S, `VrstaManipulacije` S, `VrstaVozila` S, `VrstePrevoznogSredstva` S
- **Procedure:** `DeleteRnCarinskoSkladisteDodatneUsluge`, `InsertMagacinskiBrojCarinski`, `InsertRadniNalogInterniSkladista`, `InsertRadniNalogSkladista`, `InsertRnCarinskoSkladisteDodatneUsluge`, `UpdateMagacinskiBrojCarinski`, `UpdateRadniNalogSkladista`, `UpdateRNInterniSkladista`
- **Combobox → tabela:** `cboDodatneUsluge` ← `VrstaManipulacije`; `cboNalogodavac` ← `Partnerji`; `cboVlasnikRobe` ← `Partnerji`; `cboCarinskiPostupak` ← `VrstaCarinskogPostupka`; `cboSpediterOtprema` ← `Partnerji`; `cboTipTransportaOtprema` ← `VrstePrevoznogSredstva`; `cboVrstaKamionaOtprema` ← `VrstaVozila`; `cboCarinarnicaOtprema` ← `Carinarnice`; `cboMestoIstovaraOtprema` ← `MestaUtovara`; `cboMagacinskiBroj` ← `MagacinskiBrojCarinski`
- **Funkcionalnosti:**
  - „Izbaci“ — klik → `btnIzbaci_Click`
  - „Nazad“ — klik → `btnMbNazad_Click`
  - „Sačuvaj“ — klik → `btnMbSave_Click` — čita: MagacinskiBrojCarinski; proc: InsertMagacinskiBrojCarinski, UpdateMagacinskiBrojCarinski; preko: InsertSkladista.InsertMagacinskiBrojCarinski, InsertSkladista.UpdateMagacinskiBrojCarinski
  - „Dodaj“ — klik → `btnDodaj_Click`
  - „FORMIRAN RN“ — klik → `btnFormiranRN_Click` — čita: RNCarinskoSkladisteDodatneUsluge; proc: DeleteRnCarinskoSkladisteDodatneUsluge, InsertRnCarinskoSkladisteDodatneUsluge, UpdateRadniNalogSkladista; preko: InsertSkladista.DeleteDodatneUsluge, InsertSkladista.InsertDodatneUsluge, InsertSkladista.UpdateRadniNalog
  - „SNIMI“ — klik → `btnSnimi_Click` — čita: RNCarinskoSkladisteDodatneUsluge, RadniNalogSkladista; proc: DeleteRnCarinskoSkladisteDodatneUsluge, InsertRadniNalogInterniSkladista, InsertRadniNalogSkladista, InsertRnCarinskoSkladisteDodatneUsluge, UpdateRNInterniSkladista, UpdateRadniNalogSkladista; preko: InsertSkladista.DeleteDodatneUsluge, InsertSkladista.InsertDodatneUsluge, InsertSkladista.InsertRNInterni, InsertSkladista.InsertRadniNalog, InsertSkladista.UpdateRNInterni
  - `cboVlasnikRobe` — SelectionChangeCommitted → `cboVlasnikRobe_SelectionChangeCommitted` — čita: Partnerji
  - „?“ — klik → `btnMagacinskiBroj_Click`

#### ViljuskaristiPregled [N]
- **Fajl:** `Skladista main/ViljuskaristiPregled.cs` — „Viljuskaristi Pregled“
- **Novi izgled:** Operacije skladišta → **SkladistaMain** → „Carinsko skladište“ → **CarinskoSkladiste** → „Prijem robe“ → **CarinskoSkladistePregled** → „Radni nalozi“ → **ViljuskaristiPregled**
- **Tabele:** `Delavci` S, `RadniNalogInterni` S, `RadniNalogInterniSkladistePotvrda` S, `RadniNalogSkladista` S, `RNCarinskoSkladisteDodatneUsluge` S, `RNCarinskoSkladistePrijemnica` S, `RNCarinskoSkladisteRukovalac` S, `TipPalete` S, `VrstaManipulacije` S
- **Funkcionalnosti:**
  - „Potvrdi skladistar“ — klik → `button3_Click`
  - „Refresh“ — klik → `button2_Click`
  - „gridGroupingControl2“ — klik na ćeliju → `gridGroupingControl2_TableControlCellClick` — čita: RNCarinskoSkladisteDodatneUsluge, RNCarinskoSkladistePrijemnica, RadniNalogSkladista, VrstaManipulacije

#### VozniPlanovi [N]
- **Fajl:** `MainLeget/Intermodalni/VozniPlanovi.cs` — „VozniPlanovi“
- **Novi izgled:** Intermodalni terminal → **Intermodalni1** → „Vozni planovi“ → **VozniPlanovi**

#### ZapisnikONepravilnosti [N+S]
- **Fajl:** `Uvoz/ZapisnikONepravilnosti.cs` — „Zapisnik o nepravilnosti“
- **Novi izgled:** Prijem i otprema vozova → **PrijemIOtpremaVozova1** → „Zadaci intermodalni dispećer“ → **frmRadniNalogInterniPregled** → „Zapisnik o nepravilnosti“ → **ZapisnikONepravilnosti**
- **Stari izgled:** Izvoz › Zadaci › Pregled radnih naloga → **frmRadniNalogInterniPregled** → „Zapisnik o nepravilnosti“ → **ZapisnikONepravilnosti**
- **Tabele:** `TipKontenjera` S, `ZapisnikONepravilnosti` SID p
- **Procedure:** `DeleteZapisnikONepravilnosti`, `InsertZapisnikONepravilnosti`
- **Combobox → tabela:** `cboVrstaKontejnera` ← `TipKontenjera`
- **Funkcionalnosti:**
  - „Štampaj“ — klik → `button23_Click` — izveštaj: rptZapisnikONepravilnosti.rdlc
  - `button20` — klik → `button20_Click` — proc: DeleteZapisnikONepravilnosti; preko: InsertZapisnikONepravilnosti.DelZapisnikONepravilnosti
  - `button21` — klik → `button21_Click` — proc: InsertZapisnikONepravilnosti; preko: InsertZapisnikONepravilnosti.InsZapisnikONepravilnosti

#### ZapisnikOOstecenjuRobe [N+S]
- **Fajl:** `Uvoz/ZapisnikOOstecenjuRobe.cs` — „Zapisnik o oštećenju robe“
- **Novi izgled:** Operacije pretovari → **PretovariMain** → „Pretovar“ → **PretovariPrijem** → „Prijem“ → **frmPretovarPrijem** → „Radni nalog pretovar“ → **Prijemnica** → „Zapisnik o oštećenju“ → **ZapisnikOOstecenjuRobe**
- **Stari izgled:** Terminal › Terminalske usluge › ZADACI IPU DISPEČER → **PlaniraniPretovar** → „PRETOVAR ROBE - PRIJEM“ → **Prijemnica** → „Zapisnik o oštećenju“ → **ZapisnikOOstecenjuRobe**
- **Tabele:** `ZapisnikOOstecenju` SID p
- **Procedure:** `DeleteZapisnikOOstecenju`, `InsertZapisnikOOstecenju`
- **Funkcionalnosti:**
  - „Štampaj“ — klik → `button23_Click` — izveštaj: rptZapisnikOOstecenju.rdlc
  - `button20` — klik → `button20_Click` — proc: DeleteZapisnikOOstecenju; preko: InsertZapisnikOOstecenju.DelZapisnikOOstecenju
  - `button21` — klik → `button21_Click` — proc: InsertZapisnikOOstecenju; preko: InsertZapisnikOOstecenju.InsZapisnikOOStecenju

## 9. Metod i ograničenja

- Dostupnost formi: `tools/analiza-formi/analiza.py`, pokrenut tri puta (start `MainP`, `NewMain`, `frmLogovanjeSecond`) za firmu Leget, nad trenutnim kodom.
- Tabele: regex nad SQL stringovima forme i pomoćnih klasa koje forma poziva (1 nivo); `FROM`, `JOIN`, `INSERT INTO`, `UPDATE … SET`, `DELETE FROM`. Upis preko procedura je prepoznat samo po imenu procedure (`Insert/Update/Delete` + ime tabele); šta procedura stvarno radi nije provereno, jer baza nije dostupna.
- Combobox izvori: `DataSource` dodela na kontroli tipa Combo/DropDown i `SELECT` u istoj metodi pre dodele. Ne vide se combo-i u gridovima (kolone tipa ComboBox), combo-i punjeni iz pomoćnih metoda u drugim klasama i liste napunjene ručno (`Items.Add`).
- „Nema forme ni u jednom izgledu“ ne znači da tabela nema održavanje: može da je puni Pantheon, procedura ili druga aplikacija.
- Prava korisnika iz baze (`MainNovi`, `MainNoviPrava`, provere u `MainP`) nisu uzeta u obzir.
