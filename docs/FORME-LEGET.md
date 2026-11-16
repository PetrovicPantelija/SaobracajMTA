# Forme dostupne za Leget (ulaz: NewMain)

Ovaj dokument pokazuje koje forme korisnik firme Leget može da otvori iz novog glavnog menija `NewMain`, i kojim putem. Napravljen je statičkom analizom koda (opis na kraju) i ne menja ništa u projektu.

## Ukratko

- U projektu postoji **698 formi**. Iz `NewMain` je za Leget dostupno **198** (28 %).
- Ostale forme se iz novog menija za Leget ne mogu otvoriti: pripadaju starom meniju `MainP`, drugim firmama ili su mrtav kod.
- Šta korisnik zaista vidi zavisi još i od njegovih prava u bazi (tabele `MainNovi`, `MainNoviPrava` i kartice). Analiza pokazuje šta kod dozvoljava.
- Za redizajn u stilu Sava ovo je realan obim posla: 198 formi umesto 698.

## Put do NewMain

1. `Program.cs` u folderu programa nalazi `Leget.txt` i otvara `Sifarnici/frmLogovanjeSecond` („Prijava na Leget plus nalog“).
2. Na prijavi postoje dva polja za izbor izgleda:
   - „Stari izgled“ (podrazumevano uključeno) otvara stari glavni meni `MainP`. On nije deo ove analize.
   - „Novi izgled“ otvara `MainLeget/NewMain`.
   - Ako su uključena oba, otvaraju se oba menija.
3. Klik na naslov „Prijava na Leget plus nalog“ otvara `Administracija/frmSistemskaTabela`. To je skrivena prečica, dostupna i pre prijave.

## Levi meni u NewMain

| Dugme | Prva forma | Formi u modulu |
|---|---|---|
| Logistika uvoza | — | 0 (dugme samo menja boju, ne otvara formu) |
| Logistika izvoza | `LogistikaIzvoza1` | 16 |
| Logistika direktnih klijenata | — | 0 (dugme samo menja boju, ne otvara formu) |
| Drumski transport | `Drumski1` | 25 |
| Železnički transport | — | 0 (dugme samo menja boju, ne otvara formu) |
| Operacije skladišta | `SkladistaMain` | 32 |
| Intermodalni terminal | `Intermodalni1` | 59 |
| Prijem i otprema vozova | `PrijemIOtpremaVozova1` | 89 |
| Prijem i otprema kamiona | `frmPrijemIOtpremaKamiona1` | 20 |
| Depo | `DepoMain` | 1 |
| Operacije pretovari | `PretovariMain` | 16 |
| PTI | — | 0 (dugme samo menja boju, ne otvara formu) |
| Održavanje | — | 0 (dugme samo menja boju, ne otvara formu) |
| Kapija | `frmKapija1` | 37 |
| Finansije | — | 0 (dugme samo menja boju, ne otvara formu) |
| Podešavanja sistema | `Podesavanje1` | 10 |
| Administracija | `Administracija1` | 3 |

Još u istom prozoru:

- Dugme „!!!“ u zaglavlju otvara `AdministracijaPravoPristupa` (1 forma). Vidljivo je samo korisniku `test`.
- Početni ekran (Dashboard): `GlavniEkran`.
- Dugme „VSD“ otvara `VSD/VSDMain`, ali je sakriveno za sve firme osim VSD. Zato ga ova lista ne računa.
- „Početni meni“, „Dashboard“, „Povratak na predhodni meni“ i „Logout“ ne otvaraju nove forme.

## Forme po modulima

Stablo pokazuje prvi put kojim se stiže do forme: ispod svake forme su forme koje ona otvara, uz natpis dugmeta ili naziv kartice. Do iste forme se često stiže i drugim putem; sva mesta su u abecednoj listi ispod.

### Logistika izvoza (16)

- **LogistikaIzvoza1** — `MainLeget/Izvoz/LogistikaIzvoza1.cs`
  - **LogistikaIzvoza0** — `MainLeget/Izvoz/LogistikaIzvoza0.cs` — „Porudžbenica“
    - **frmPregledPorudzbenica** — `Izvoz/frmPreglegPorudzbenica.cs` — „Izmeni postojeći“
    - **frmProdajniNalogIzvoz** — `Izvoz/frmProdajniNalogIzvoz.cs` — „Kreiraj novi“
  - **LogistikaIzvoza2** — `MainLeget/Izvoz/LogistikaIzvoza2.cs` — „Formiranje naloga za otpremu“
    - **frmPregledGrupaKontejneraIzvoz** — `Izvoz/frmPregledGrupaKontejneraIzvoz.cs` — „Izmeni postojeći“
      - **frmLogTokaProcesa** — `Izvoz/frmLogTokaProcesa.cs` — „Otvori“
    - **frmProdajniNalogIzvozTabela** — `Izvoz/frmProdajniNalogIzvozTabela.cs` — „Kreiraj novi“
      - **frmGrupniUnosPoljaIzvoz** — `Izvoz/frmGrupniUnosPoljaIzvoz.cs` — `gridGroupingControl1` (TableControlCellClick)
        - **frmCarinskiPostupak** — `Izvoz/frmCarinskiPostupak.cs` — „Carinski postupak“
          - **frmKontaktOsobe** — `Dokumenta/frmKontaktOsobe.cs` — `btnPolazna` (Click)
        - **frmIzvozPregledKontejneraDrumskeUsluge** — `Izvoz/frmIzvozPregledKontejneraDrumskeUsluge.cs` — „Pusti Drumski uslugu“
        - **frmIzvozUnosManipulacije** — `Izvoz/frmIzvozUnosManipulacije.cs` — „Dodaj usluge“
          - **frmIzvozKopiranjeUslugaKontejnera1** — `Izvoz/frmIzvozKopiranjeUslugaKontejnera1.cs` — „Kopiraj usluge“
        - **frmRelacija** — `Izvoz/frmRelacija.cs` — „Relacija“
      - **NalogIzvozaZaOtpremu** — `MainLeget/Izvoz/NalogIzvozaZaOtpremu.cs` — `gridGroupingControl1` (TableControlCellClick)

### Podešavanja sistema (10)

- **Podesavanje1** — `MainLeget/Podesavanjesistema/Podesavanje1.cs`
  - **frmPartnerji** — `Sifarnici/frmPartnerji.cs` — „Partneri“
    - **frmKontaktOsobe** — `Dokumenta/frmKontaktOsobe.cs` — „Kontakt osobe“
    - **frmPartnerjiFirme** — `Sifarnici/frmPartnerjiFirme.cs` — „Firme“
  - **frmPozicija** — `TrackModal/Sifarnici/frmPozicija.cs` — „Pozicije“
  - **frmSkladista** — `TrackModal/Sifarnici/frmSkladista.cs` — „Polja“
  - **frmSkladisteObjekat** — `TrackModal/Sifarnici/frmSkladisteObjekat.cs` — „Objekti“
  - **frmTipKontejnera** — `TrackModal/Sifarnici/frmTipKontejnera.cs` — „Tipovi kontejnera“
  - **frmZone** — `TrackModal/Sifarnici/frmZone.cs` — „Zone“
  - **SkladisteGrupa** — `TrackModal/Sifarnici/SkladisteGrupa.cs` — „Lokacije“

### Administracija (3)

- **Administracija1** — `Administracija/Administracija1.cs`
  - **AdministracijaPravoPristupa** — `MainLeget/AdministracijaPravoPristupa.cs` — „Prava“
  - **frmKorisnici** — `Administracija/frmKorisnici.cs` — „Korisnici“

### Drumski transport (25)

- **Drumski1** — `MainLeget/Drumski/Drumski1.cs`
  - **Cerade** — `MainLeget/Drumski/Cerade.cs` — „Cerada“
    - **frmAutomobiliDrumski** — `Drumski/frmAutomobiliDrumski.cs` — „UNOS PREVOZNIK/KAMIONER“
      - **frmAutomobilEdit** — `Drumski/frmAutomobilEdit.cs` — „Novi“
    - **frmPodesavanjeRaspolozivosti** — `Drumski/frmPodesavanjeRaspolozivosti.cs` — „PROVERA \r\nRASPOLOŽIVOSTI VOZILA“
    - **frmStatus** — `Drumski/frmStatus.cs` — „STATUSI VOZILA“
      - **frmPregledFajlova** — `Drumski/FormaPregledFajlova.cs` — `dataGridView3` (CellClick)
    - **NalogZaDrumski** — `MainLeget/Drumski/NalogZaDrumski.cs` — „NALOG ZA DRUMSKI TRANSPORT“
      - **frmDrumski1** — `Drumski/frmDrumski1.cs` — „Kreiraj novi“
        - **frmPartnerji** — `Sifarnici/frmPartnerji.cs` — „Nova špedicija“
          - **frmKontaktOsobe** — `Dokumenta/frmKontaktOsobe.cs` — „Kontakt osobe“
          - **frmPartnerjiFirme** — `Sifarnici/frmPartnerjiFirme.cs` — „Firme“
      - **frmPregledNalogaDrumski** — `Drumski/frmPregledNalogaDrumski.cs` — „TRANSPORTNI NALOZI IZVOZ“
        - **frmDrumski** — `Drumski/frmDrumski.cs` — „Otvori“
        - **frmFormaZaVozace** — `DrumskiApp/frmFormaZaVozace.cs` — „Vozac“
    - **PakovanjeKamionaCerade** — `Drumski/PakovanjeKamionaCerade.cs` — „FORMIRANJE\r\nTRANSPORTNOG NALOGA“
      - **frmListaProtokola** — `Drumski/frmListaProtokola.cs` — „PROTOKOL“
        - **frmProtokolTransportnogNaloga** — `Drumski/frmProtokolTransportnogNaloga.cs` — `subOstali` (Click)
      - **frmPregledPorukeVozacu** — `Drumski/frmPregledPorukeVozacu.cs` — `dataGridView3` (CellClick)
  - **frmNalogZaFakturisanje** — `Drumski/frmNalogZaFakturisanje.cs` — „NALOG ZA FAKTURISANJE“
    - **frmBrojFaktureModal** — `Drumski/frmBrojFaktureModal.cs` — „Objedinjena dokumenta po izlaznoj fakturi“
  - **frmObradaUlaznihFaktura** — `Drumski/frmObradaUlaznihFaktura.cs` — „Kontrola i obračun transporta“
  - **Platforma** — `MainLeget/Drumski/Platforma.cs` — „Platforme“
    - **PakovanjeKamiona1** — `Drumski/PakovanjeKamiona1.cs` — „FORMIRANJE\r\nTRANSPORTNOG NALOGA“
  - **VanGabaritni** — `MainLeget/Drumski/VanGabaritni.cs` — „Van Gabaritni“

### Operacije skladišta (32)

- **SkladistaMain** — `Skladista main/SkladistaMain.cs`
  - **CarinskoSkladiste** — `Skladista main/CarinskoSkladiste.cs` — „Carinsko skladište“
    - **CarinskoSkladistePregled** — `Skladista main/CarinskoSkladistePregled.cs` — „Prijem robe“
      - **frmIzvestajCarinskoObelezje** — `Skladista/frmIzvestajCarinskoObelezje.cs` — „Štampaj“
      - **frmSeniranjeDokumenata** — `Drumski/frmSeniranjeDokumenata.cs` — „Skeniraj“
      - **Prijemnica** — `Skladista main/Dokumenta/Prijemnica.cs` — „Prijemnica“
        - **frmTipPalete** — `TrackModal/Sifarnici/frmTipPalete.cs` — „?“
        - **PaletePrebacivanje** — `Skladista main/PaletePrebacivanje.cs` — „Prebaci na palete“
        - **TerminalMapFRM** — `TerminalMap/TerminalMapFRM.cs` — „Mapa pozicija“
          - **frmDefinisiPoziciju** — `Tehnologija/frmDefinisiPoziciju.cs` — „Definiši/Predefiniši“
          - **frmDodatneUsluge** — `Uvoz/frmDodatneUsluge.cs` — „Dodatne usluge“
            - **frmIzvozDokumenta** — `Izvoz/frmIzvozDokumenta.cs` — „Dodeli slike“
            - **UvozDokumenta** — `Uvoz/UvozDokumenta.cs` — „Dodeli slike“
          - **frmKontejnerTekuce** — `Uvoz/frmKontejnerTekuce.cs` — „Terminal central“
          - **frmRN12InterniPrenos** — `TerminalMap/frmRN12InterniPrenos.cs` — „Interni prenos“
      - **ViljuskaristiPregled** — `Skladista main/ViljuskaristiPregled.cs` — „Radni nalozi“
    - **MagacinskaKnjigaCarinska** — `Skladista main/MagacinskaKnjigaCarinska.cs` — „Magacinska knjiga“
    - **PregledLagera** — `Skladista main/Dokumenta/PregledLagera.cs` — „Pregled lagera robe“
  - **KomercijalnoSkladiste** — `Skladista main/KomercijalnoSkladiste.cs` — „Komercijalno skladište“
  - **NalogZaKomercijalni** — `Skladista main/NalogZaKomercijalni.cs` — kartica „Radni nalozi“
    - **PregledKomercijalnihNaloga** — `Skladista main/PregledKomercijalnihNaloga.cs` — „SKLADIŠNI NALOZI UVOZ“
      - **frmScenarioSCI** — `RNI/frmScenarioSCI.cs` — „Pregledaj podatke“
      - **Otprema** — `Skladista main/Dokumenta/Otprema.cs` — „Otvori“
        - **OtpremaLager** — `Skladista main/OtpremaLager.cs` — „IZABERI SA LAGERA“
        - **Otpremnica** — `Skladista main/Dokumenta/Otpremnica.cs` — „IDI NA OTPREMNICU“
      - **Prijem** — `Skladista main/Dokumenta/Prijem.cs` — „Otvori“
    - **TipSkladista** — `Skladista main/TipSkladista.cs` — „Kreiraj novi“
      - **RadniNaloziSkladista** — `Skladista main/RadniNaloziSkladista.cs` — „Carinsko skladište“
        - **InterniPoslovi** — `Skladista main/Dokumenta/InterniPoslovi.cs` — „Interni poslovi“
        - **InterniPrenos** — `Skladista main/Dokumenta/InterniPrenos.cs` — „Interni prenos“
        - **Pretovar** — `Skladista main/Dokumenta/Pretovar.cs` — „Pretovar“
        - **VanredniPoslovi** — `Skladista main/Dokumenta/VanredniPoslovi.cs` — „Vanredni poslovi“

### Operacije pretovari (16)

- **PretovariMain** — `MainLeget/Pretovari/PretovariMain.cs`
  - **frmPretovariSkladista** — `MainLeget/Pretovari/frmPretovariSkladista.cs` — „Skladišta“
  - **PretovariPrijem** — `MainLeget/Pretovari/PretovariPrijem.cs` — „Pretovar“
    - **frmPretovarPrijem** — `MainLeget/Pretovari/frmPretovarPrijem.cs` — „Prijem“
      - **frmPrijemKontejneraKamionLegetUvoz** — `Dokumenta/frmPrijemKontejneraKamionLegetUvoz.cs` — „Radni nalog kalmar“
        - **frmDokumentaPrijemKontejneraKamion** — `Dokumenta/frmDokumentaPrijemKontejneraKamion.cs` — „Dokumenta prijema“
        - **frmKontejneriNaPrijemnici** — `Dokumenta/frmKontejneriNaPrijemnici.cs` — „?“
      - **Otpremnica** — `RadniNalozi/Otpremnica.cs` — „Radni nalog pretovar“
        - **OtpremnicaPregled** — `RadniNalozi/OtpremnicaPregled.cs` — „Otpremnice pregled“
      - **Prijemnica** — `RadniNalozi/Prijemnica.cs` — „Radni nalog pretovar“
        - **PrijemnicaPregled** — `RadniNalozi/PrijemnicaPregled.cs` — „PRETOVAR PREGLED“
        - **ZapisnikOOstecenjuRobe** — `Uvoz/ZapisnikOOstecenjuRobe.cs` — „Zapisnik o oštećenju“
      - **RN12MedjuskladisniKontejnera** — `RadniNalozi/RN12MedjuskladisniKontejnera.cs` — „Radni nalog kalmar“
        - **frmDodelaSkladista** — `RadniNalozi/frmDodelaSkladista.cs` — „Dodela polja“
          - **frmPregledSkladistaNovi** — `RadniNalozi/frmPregledSkladistaNovi.cs` — „MAPA SKLADISTA“
        - **frmKontejnerTekuce** — `Uvoz/frmKontejnerTekuce.cs` — „KONTEJNER NA SKLADISTU“

### Prijem i otprema vozova (89)

- **PrijemIOtpremaVozova1** — `MainLeget/Prijemiotpremavozova/PrijemIOtpremaVozova1.cs`
  - **frmDodatneUsluge** — `Uvoz/frmDodatneUsluge.cs` — „Dodatne usluge“
    - **frmIzvozDokumenta** — `Izvoz/frmIzvozDokumenta.cs` — „Dodeli slike“
    - **UvozDokumenta** — `Uvoz/UvozDokumenta.cs` — „Dodeli slike“
  - **frmKontejnerTekuceArhiv** — `Uvoz/frmKontejnerTekuceArhiv.cs` — „Terminal central (arhiv)“
  - **frmPrijemIOtpremaVozova2** — `MainLeget/Prijemiotpremavozova/frmPrijemIOtpremaVozova2.cs` — „Depo cnt“
    - **frmCIRPregled** — `TrackModal/Dokumeta/frmCIRPregled.cs` — „CIR“
      - **frmCIR** — `Dokumenta/frmCIR.cs` — „Otvori“
    - **frmPopisKonterjnera** — `Dokumenta/frmPopisKonterjnera.cs` — „Popis cnt“
      - **frmPopisKontejneraStampa** — `Dokumenta/frmPopisKontejneraStampa.cs` — „Štampa“
    - **TerminalMapFRM** — `TerminalMap/TerminalMapFRM.cs` — „Pregled skladišta“
      - **frmDefinisiPoziciju** — `Tehnologija/frmDefinisiPoziciju.cs` — „Definiši/Predefiniši“
      - **frmKontejnerTekuce** — `Uvoz/frmKontejnerTekuce.cs` — „Terminal central“
      - **frmRN12InterniPrenos** — `TerminalMap/frmRN12InterniPrenos.cs` — „Interni prenos“
  - **frmPrijemIOtpremaVozova3** — `MainLeget/Prijemiotpremavozova/frmPrijemIOtpremaVozova3.cs` — „Pregled naloga“
    - **DogovoreniPosloviIzvoza** — `Izvoz/DogovoreniPosloviIzvoza.cs` — „Dogovoreni poslovi izvoza“
    - **frmAnalizaRadnihNaloga** — `RadniNalozi/frmAnalizaRadnihNaloga.cs` — „Pregled radnih naloga“
  - **frmPrijemIOtpremaVozova4** — `MainLeget/Prijemiotpremavozova/frmPrijemIOtpremaVozova4.cs` — „Voz“
    - **frmDopunaPlanaPraznimIzvoz** — `Izvoz/frmDopunaPlanaPraznimIzvoz.cs` — „Dopuna plana praznim cnt“
    - **frmPregledOtpreme** — `TrackModal/Dokumeta/frmPregledOtpreme.cs` — „Gate out voz“
      - **frmOtpremaKontejnera** — `TrackModal/Dokumeta/frmOtpremaKontejnera.cs` — „Otvori voz“
        - **frmDodatniList** — `TrackModal/Izvestaji/frmDodatniList.cs` — „Štampa dodatni list“
        - **frmDokumentaOtpremaKontejnera** — `TrackModal/Dokumeta/frmDokumentaOtpremaKontejnera.cs` — „Dokumenta otpreme“
        - **frmManipulacije** — `TrackModal/Dokumeta/frmManipulacije.cs` — „Raspored manipulacija“
          - **frmNajavaKomitemtaDokument** — `TrackModal/Dokumeta/frmNajavaKomitemtaDokument.cs` — „Nalog komitenta“
        - **frmNalogZaPrevoz** — `TrackModal/Dokumeta/frmNalogZaPrevoz.cs` — „Nalog Za Prevoz“
          - **frmPutniNalog** — `TrackModal/Dokumeta/frmPutniNalog.cs` — „Otvori putni nalog“
            - **frmRadniNalogTransport** — `TrackModal/Dokumeta/frmRadniNalogTransport.cs` — „Radni nalog“
              - **frmAutoprevozniList2** — `TrackModal/Dokumeta/frmAutoprevozniList2.cs` — „Autoprevozni list“
    - **frmPregledVozova** — `TrackModal/Dokumeta/frmPregledVozova.cs` — „Pregled vozova“
    - **frmPrijemVozomPregled** — `TrackModal/Dokumeta/frmPrijemVozomPregled.cs` — „Gate in voz“
    - **frmVoz** — `TrackModal/Dokumeta/frmVoz.cs` — „Vozovi“
      - **frmFormiranjePlana** — `Uvoz/frmFormiranjePlana.cs` — „UVOZ - FORMIRANJE PLANA“
        - **frmUvozKonacna** — `Uvoz/UvozKonacna.cs` — „Otvori plan“
          - **frmPregledKontejneraDrumskeUsluge** — `Uvoz/frmPregledKontejneraDrumskeUsluge.cs` — „DRUMSKE USLUGE“
          - **frmUnosManipulacija** — `Uvoz/frmUnosManipulacija.cs` — prečica na tastaturi (`frmUvozKonacna_KeyDown`)
            - **frmUvozKopirajUslugeKontejnera** — `Uvoz/frmUvozKopirajUslugeKontejnera.cs` — „Kopiraj usluge“
            - **frmVoziloUsluga** — `Uvoz/frmVoziloUsluga.cs` — „Kamion za uslugu“
          - **frmUvozKonacnaTable** — `Uvoz/frmUvozKonacnaTable.cs` — prečica na tastaturi (`frmUvozKonacna_KeyDown`)
          - **frmUvozUvoznici** — `Uvoz/frmUvozUvoznici.cs` — „Uvezi spisak“
          - **UvozNHM** — `Uvoz/UvozNHM.cs` — „NHM/HS KODOVI“
    - **RN1PrijemVoza** — `RadniNalozi/RN1PrijemVoza.cs` — „RN prijem voza“
      - **frmDodelaSkladista** — `RadniNalozi/frmDodelaSkladista.cs` — „Dodela polja“
        - **frmPregledSkladistaNovi** — `RadniNalozi/frmPregledSkladistaNovi.cs` — „MAPA SKLADISTA“
    - **RN2OtpremaVoza** — `RadniNalozi/RN2OtpremaVoza.cs` — „RN otprema voza“
  - **frmRadniNalogInterniPregled** — `Uvoz/frmRadniNalogInterniPregled.cs` — „Zadaci intermodalni dispećer“
    - **frmFormiranjePlanaIzvoz** — `Izvoz/frmFormiranjePlanaIzvoz.cs` — „Dodaj izvoznom vozu iz uvoza“
      - **frmIzvozKonacna** — `Izvoz/frmIzvozKonacna.cs` — „Otvori plan“
        - **frmIzvozKonacnaTable** — `Izvoz/frmIzvozKonacnaTable.cs` — „Tabela“
        - **frmIzvozPregledKontejneraDrumskeUsluge** — `Izvoz/frmIzvozPregledKontejneraDrumskeUsluge.cs` — „Drumske usluge“
        - **frmIzvozUnosManipulacije** — `Izvoz/frmIzvozUnosManipulacije.cs` — „Unos usluga“
          - **frmIzvozKopiranjeUslugaKontejnera1** — `Izvoz/frmIzvozKopiranjeUslugaKontejnera1.cs` — „Kopiraj usluge“
        - **frmKontaktOsobe** — `Dokumenta/frmKontaktOsobe.cs` — `button16` (Click)
        - **frmKontaktOsobeMU** — `Izvoz/frmKontaktOsobeMU.cs` — `button22` (Click)
    - **frmOtpremaKontejneraKamionomIzKontejnera** — `Izvoz/frmOtpremaKontejneraKamionomIzKontejnera.cs` — „Formiraj otpremu kamionom“
      - **frmOtpremaKontejnera** — `Dokumenta/frmOtpremaKontejnera.cs` — „Otvori“
        - **frmDodatniList** — `Izvestaji/frmDodatniList.cs` — „Štampa dodatni list“
        - **frmManipulacije** — `Dokumenta/frmManipulacije.cs` — „Raspored usluga“
          - **frmNajavaKomitemtaDokument** — `Dokumenta/frmNajavaKomitemtaDokument.cs` — „Nalog komitenta“
        - **frmNalogZaPrevoz** — `Dokumenta/frmNalogZaPrevoz.cs` — „Nalog Za Prevoz“
          - **frmPutniNalog** — `Dokumenta/frmPutniNalog.cs` — „Otvori putni nalog“
            - **frmRadniNalogTransport** — `Dokumenta/frmRadniNalogTransport.cs` — „Radni nalog“
              - **frmAutoprevozniList2** — `Dokumenta/frmAutoprevozniList2.cs` — „Autoprevozni list“
        - **frmPregledNarucenihManipulacija** — `Dokumenta/frmPregledNarucenihManipulacija.cs` — „Usluge pregled“
        - **frmTovarniList** — `Testiranje/frmTovarniList.cs` — „Tovarni list“
      - **frmOtpremaKontejneraIzvozKamion** — `Dokumenta/frmOtpremaKontejneraIzvozKamion.cs` — „Otvori“
        - **frmDokumentaOtpremaKontejnera** — `Dokumenta/frmDokumentaOtpremaKontejnera.cs` — „Dokumenta otpreme“
      - **frmOtpremaKontejneraUvozKamion** — `Dokumenta/frmOtpremaKontejneraUvozKamion.cs` — „Otvori“
        - **frmLagerOperater** — `Promet/frmLagerOperater.cs` — „MAPA SKLADIŠTA“
    - **frmOtpremaVozaIzPlana** — `Izvoz/frmOtpremaVozaIzPlana.cs` — „Potvrdi“
      - **frmOtpremaKontejneraLegetIZVOZ** — `Dokumenta/frmOtpremaKontejneraLegetIZVOZ.cs` — „Otvori“
    - **frmPrijemKamionaIzPlana** — `Uvoz/frmPrijemKamionaIzPlana.cs` — „Formiraj prijem kamionom“
    - **frmPrijemKontejneraKamionLegetUvoz** — `Dokumenta/frmPrijemKontejneraKamionLegetUvoz.cs` — „Potvrdi“
      - **frmDokumentaPrijemKontejneraKamion** — `Dokumenta/frmDokumentaPrijemKontejneraKamion.cs` — „Dokumenta prijema“
      - **frmKontejneriNaPrijemnici** — `Dokumenta/frmKontejneriNaPrijemnici.cs` — „?“
    - **frmPrijemVozaIzPlana** — `Uvoz/frmPrijemVozaIzPlana.cs` — „Potvrdi“
      - **frmPrijemKontejneraKamionLegetIzvoz** — `Dokumenta/frmPrijemKontejneraKamionLegetIzvoz.cs` — „OTVORI - GATE IN KAMION IZVOZ“
      - **frmPrijemKontejneraLegetVozUvoz** — `Dokumenta/frmPrijemKontejneraLegetVozUvoz.cs` — „OTVORI - GATE IN VOZ“
        - **frmDokumentaPrijemKontejneraVoz** — `Dokumenta/frmDokumentaPrijemKontejneraVoz.cs` — „Dokumenta prijema“
        - **frmPovezivanjeKontejneraIVagona** — `Dokumenta/frmPovezivanjeKontejneraIVagona.cs` — „Povezivanje vagona“
    - **frmScenarioSCI** — `RNI/frmScenarioSCI.cs` — „Pregledaj podatke“
    - **RN12MedjuskladisniKontejnera** — `RadniNalozi/RN12MedjuskladisniKontejnera.cs` — „Potvrdi“
    - **RN4PrijemPlatforme** — `RadniNalozi/RN4PrijemPlatforme.cs` — „Otvori RN“
    - **RN5PrijemPlatforme2** — `RadniNalozi/RN5PrijemPlatforme2.cs` — „Otvori RN“
    - **RN6OtpremaPlatforme** — `RadniNalozi/RN6OtpremaPlatforme.cs` — „Otvori RN“
    - **RN7OtpremaPlatforme2** — `RadniNalozi/RN7OtpremaPlatforme2.cs` — „Otvori RN“
    - **ZapisnikONepravilnosti** — `Uvoz/ZapisnikONepravilnosti.cs` — „Zapisnik o nepravilnosti“
  - **frmVozOtprema** — `MainLeget/Prijemiotpremavozova/frmVozOtprema.cs` — „Otprema“
  - **frmVozPrijem** — `MainLeget/Prijemiotpremavozova/frmVozPrijem.cs` — „Prijem“

### Prijem i otprema kamiona (20)

- **frmPrijemIOtpremaKamiona1** — `MainLeget/PrijemIOtpremaKamiona/frmPrijemIOtpremaKamiona1.cs`
  - **frmPrijemIOtpremaCerada** — `MainLeget/PrijemIOtpremaKamiona/frmPrijemIOtpremaCerada.cs` — „CERADA“
    - **frmCerada** — `MainLeget/PrijemIOtpremaKamiona/frmCerada.cs` — „Prijem“
    - **frmCeradaOtprema** — `MainLeget/PrijemIOtpremaKamiona/frmCeradaOtprema.cs` — „Otprema“
  - **frmPrijemIOtpremaPlatforma** — `MainLeget/PrijemIOtpremaKamiona/frmPrijemIOtpremaPlatforma.cs` — „PLATFORMA“
    - **frmPlatforma** — `MainLeget/PrijemIOtpremaKamiona/frmPlatforma.cs` — „Prijem“
      - **frmPrijemKamionaDetalji** — `MainLeget/PrijemIOtpremaKamiona/frmPrijemKamionaDetalji.cs` — „Pregled“
    - **frmPlatformaOtprema** — `MainLeget/PrijemIOtpremaKamiona/frmPlatformaOtprema.cs` — „Otprema“
    - **VaganjePregled** — `Izvoz/VaganjePregled.cs` — „Vaga“
      - **Vaganje** — `Izvoz/Vaganje.cs` — „Otvori“
        - **IzvestajVaganja** — `Izvoz/IzvestajVaganja.cs` — „Izveštaj“
  - **Pregledac** — `MainLeget/PrijemIOtpremaKamiona/Pregledac.cs` — „PREGLEDAČ“
    - **frmCIR** — `Dokumenta/frmCIR.cs` — „Izradi CIR“
      - **frmDodelaSkladista** — `RadniNalozi/frmDodelaSkladista.cs` — „ODREDI PRIVREMENU POZICIJU“
        - **frmPregledSkladistaNovi** — `RadniNalozi/frmPregledSkladistaNovi.cs` — „MAPA SKLADISTA“
    - **frmDodatneUsluge** — `Uvoz/frmDodatneUsluge.cs` — „Dodatne usluge“
      - **frmIzvozDokumenta** — `Izvoz/frmIzvozDokumenta.cs` — „Dodeli slike“
      - **UvozDokumenta** — `Uvoz/UvozDokumenta.cs` — „Dodeli slike“
    - **frmFotografijePregledac** — `MainLeget/PrijemIOtpremaKamiona/frmFotografijePregledac.cs` — „Fotografije“
    - **ZapisnikONepravilnosti** — `Uvoz/ZapisnikONepravilnosti.cs` — „Evidencija nepravilnosti“

### Kapija (37)

- **frmKapija1** — `MainLeget/Kapija/frmKapija1.cs`
  - **frmKamionDetalji** — `Kapija/frmKamionDetalji.cs` — „Novi dolazak“
  - **frmKapijaOtvoreniNalozi** — `Kapija/frmKapijaOtvoreniNalozi.cs` — „Lista naloga u NAJAVI“
    - **frmOtpremaKontejneraKamionomIzKontejnera** — `Izvoz/frmOtpremaKontejneraKamionomIzKontejnera.cs` — metoda `FormirajRadniNalog`
      - **frmOtpremaKontejnera** — `Dokumenta/frmOtpremaKontejnera.cs` — „Otvori“
        - **frmDodatniList** — `Izvestaji/frmDodatniList.cs` — „Štampa dodatni list“
        - **frmManipulacije** — `Dokumenta/frmManipulacije.cs` — „Raspored usluga“
          - **frmNajavaKomitemtaDokument** — `Dokumenta/frmNajavaKomitemtaDokument.cs` — „Nalog komitenta“
        - **frmNalogZaPrevoz** — `Dokumenta/frmNalogZaPrevoz.cs` — „Nalog Za Prevoz“
          - **frmKontaktOsobe** — `Dokumenta/frmKontaktOsobe.cs` — „?“
          - **frmPutniNalog** — `Dokumenta/frmPutniNalog.cs` — „Otvori putni nalog“
            - **frmRadniNalogTransport** — `Dokumenta/frmRadniNalogTransport.cs` — „Radni nalog“
              - **frmAutoprevozniList2** — `Dokumenta/frmAutoprevozniList2.cs` — „Autoprevozni list“
        - **frmPregledNarucenihManipulacija** — `Dokumenta/frmPregledNarucenihManipulacija.cs` — „Usluge pregled“
        - **frmTovarniList** — `Testiranje/frmTovarniList.cs` — „Tovarni list“
      - **frmOtpremaKontejneraIzvozKamion** — `Dokumenta/frmOtpremaKontejneraIzvozKamion.cs` — „Otvori“
        - **frmDokumentaOtpremaKontejnera** — `Dokumenta/frmDokumentaOtpremaKontejnera.cs` — „Dokumenta otpreme“
      - **frmOtpremaKontejneraUvozKamion** — `Dokumenta/frmOtpremaKontejneraUvozKamion.cs` — „Otvori“
        - **frmCIR** — `Dokumenta/frmCIR.cs` — „CIR“
        - **frmLagerOperater** — `Promet/frmLagerOperater.cs` — „MAPA SKLADIŠTA“
      - **RN6OtpremaPlatforme** — `RadniNalozi/RN6OtpremaPlatforme.cs` — `button21` (Click)
      - **RN7OtpremaPlatforme2** — `RadniNalozi/RN7OtpremaPlatforme2.cs` — metoda `PrenesiRN7`
    - **frmPrijemVozaIzPlana** — `Uvoz/frmPrijemVozaIzPlana.cs` — metoda `FormirajRadniNalog`
      - **frmPrijemKontejneraKamionLegetIzvoz** — `Dokumenta/frmPrijemKontejneraKamionLegetIzvoz.cs` — „OTVORI - GATE IN KAMION IZVOZ“
      - **frmPrijemKontejneraKamionLegetUvoz** — `Dokumenta/frmPrijemKontejneraKamionLegetUvoz.cs` — „OTVORI - GATE IN KAMION UVOZ“
        - **frmDokumentaPrijemKontejneraKamion** — `Dokumenta/frmDokumentaPrijemKontejneraKamion.cs` — „Dokumenta prijema“
      - **frmPrijemKontejneraLegetVozUvoz** — `Dokumenta/frmPrijemKontejneraLegetVozUvoz.cs` — „OTVORI - GATE IN VOZ“
        - **frmDodelaSkladista** — `RadniNalozi/frmDodelaSkladista.cs` — „Odredi pozicije“
          - **frmPregledSkladistaNovi** — `RadniNalozi/frmPregledSkladistaNovi.cs` — „MAPA SKLADISTA“
        - **frmDokumentaPrijemKontejneraVoz** — `Dokumenta/frmDokumentaPrijemKontejneraVoz.cs` — „Dokumenta prijema“
        - **frmKontejneriNaPrijemnici** — `Dokumenta/frmKontejneriNaPrijemnici.cs` — „?“
        - **frmKontejnerTekuce** — `Uvoz/frmKontejnerTekuce.cs` — „Tekuce stanje“
        - **frmPovezivanjeKontejneraIVagona** — `Dokumenta/frmPovezivanjeKontejneraIVagona.cs` — „Povezivanje vagona“
      - **RN1PrijemVoza** — `RadniNalozi/RN1PrijemVoza.cs` — metoda `PrenesiStavkeIRN`
      - **RN4PrijemPlatforme** — `RadniNalozi/RN4PrijemPlatforme.cs` — metoda `PrenesiStavkeIRN`
      - **RN5PrijemPlatforme2** — `RadniNalozi/RN5PrijemPlatforme2.cs` — metoda `PrenesiStavkeIRN`
  - **frmPregledKamiona** — `Kapija/frmPregledKamiona.cs` — „Lista SVIH naloga“

### Dugme „!!!“ (samo korisnik test) (1)

- **AdministracijaPravoPristupa** — `MainLeget/AdministracijaPravoPristupa.cs`

### Depo (1)

- **DepoMain** — `MainLeget/Depocnt/DepoMain.cs`

### Intermodalni terminal (59)

- **Intermodalni1** — `MainLeget/Intermodalni/Intermodalni1.cs`
  - **frmTerminalPrivremeni** — `RNI/frmTerminalPrivremeni.cs` — „Terminal temp“
    - **frmTerminalPrivremeniArhiva** — `RNI/frmTerminalPrivremeniArhiva.cs` — „Arhivirani podaci“
    - **frmTerminalPrivremeniDokumenta** — `RNI/frmTerminalPrivremeniDokumenta.cs` — „Zakači dokumenta“
    - **frmTerminalPrivremeniExcel** — `RNI/frmTerminalPrivremeniExcel.cs` — „Uvoz Excel“
    - **frmTerminalPrivremeniLog** — `RNI/frmTerminalPrivremeniLog.cs` — „Pregled log“
    - **frmTerminalPrivremeniPromeni** — `RNI/frmTerminalPrivremeniPromeni.cs` — „Grupna promena“
    - **frmTerminalPrivremeniSlike** — `RNI/frmTerminalPrivremeniSlike.cs` — „Zakači slike“
  - **IntermodalniKomN** — `MainLeget/Intermodalni/IntermodalniKomN.cs` — „Komercijalni nalozi“
    - **frmRadniNalogInterniPregled** — `Uvoz/frmRadniNalogInterniPregled.cs` — „Lista svih naloga“
      - **frmFormiranjePlanaIzvoz** — `Izvoz/frmFormiranjePlanaIzvoz.cs` — „Dodaj izvoznom vozu iz uvoza“
        - **frmIzvozKonacna** — `Izvoz/frmIzvozKonacna.cs` — „Otvori plan“
          - **frmIzvozDokumenta** — `Izvoz/frmIzvozDokumenta.cs` — „Dokumentacija“
          - **frmIzvozKonacnaTable** — `Izvoz/frmIzvozKonacnaTable.cs` — „Tabela“
          - **frmIzvozPregledKontejneraDrumskeUsluge** — `Izvoz/frmIzvozPregledKontejneraDrumskeUsluge.cs` — „Drumske usluge“
          - **frmIzvozUnosManipulacije** — `Izvoz/frmIzvozUnosManipulacije.cs` — „Unos usluga“
            - **frmIzvozKopiranjeUslugaKontejnera1** — `Izvoz/frmIzvozKopiranjeUslugaKontejnera1.cs` — „Kopiraj usluge“
          - **frmKontaktOsobe** — `Dokumenta/frmKontaktOsobe.cs` — `button16` (Click)
          - **frmKontaktOsobeMU** — `Izvoz/frmKontaktOsobeMU.cs` — `button22` (Click)
      - **frmOtpremaKontejneraKamionomIzKontejnera** — `Izvoz/frmOtpremaKontejneraKamionomIzKontejnera.cs` — „Formiraj otpremu kamionom“
        - **frmOtpremaKontejnera** — `Dokumenta/frmOtpremaKontejnera.cs` — „Otvori“
          - **frmDodatniList** — `Izvestaji/frmDodatniList.cs` — „Štampa dodatni list“
          - **frmManipulacije** — `Dokumenta/frmManipulacije.cs` — „Raspored usluga“
            - **frmNajavaKomitemtaDokument** — `Dokumenta/frmNajavaKomitemtaDokument.cs` — „Nalog komitenta“
          - **frmNalogZaPrevoz** — `Dokumenta/frmNalogZaPrevoz.cs` — „Nalog Za Prevoz“
            - **frmPutniNalog** — `Dokumenta/frmPutniNalog.cs` — „Otvori putni nalog“
              - **frmRadniNalogTransport** — `Dokumenta/frmRadniNalogTransport.cs` — „Radni nalog“
                - **frmAutoprevozniList2** — `Dokumenta/frmAutoprevozniList2.cs` — „Autoprevozni list“
          - **frmPregledNarucenihManipulacija** — `Dokumenta/frmPregledNarucenihManipulacija.cs` — „Usluge pregled“
          - **frmTovarniList** — `Testiranje/frmTovarniList.cs` — „Tovarni list“
        - **frmOtpremaKontejneraIzvozKamion** — `Dokumenta/frmOtpremaKontejneraIzvozKamion.cs` — „Otvori“
          - **frmDokumentaOtpremaKontejnera** — `Dokumenta/frmDokumentaOtpremaKontejnera.cs` — „Dokumenta otpreme“
        - **frmOtpremaKontejneraUvozKamion** — `Dokumenta/frmOtpremaKontejneraUvozKamion.cs` — „Otvori“
          - **frmCIR** — `Dokumenta/frmCIR.cs` — „CIR“
          - **frmLagerOperater** — `Promet/frmLagerOperater.cs` — „MAPA SKLADIŠTA“
      - **frmOtpremaVozaIzPlana** — `Izvoz/frmOtpremaVozaIzPlana.cs` — „Potvrdi“
        - **frmOtpremaKontejneraLegetIZVOZ** — `Dokumenta/frmOtpremaKontejneraLegetIZVOZ.cs` — „Otvori“
      - **frmPrijemKamionaIzPlana** — `Uvoz/frmPrijemKamionaIzPlana.cs` — „Formiraj prijem kamionom“
      - **frmPrijemKontejneraKamionLegetUvoz** — `Dokumenta/frmPrijemKontejneraKamionLegetUvoz.cs` — „Potvrdi“
        - **frmDokumentaPrijemKontejneraKamion** — `Dokumenta/frmDokumentaPrijemKontejneraKamion.cs` — „Dokumenta prijema“
        - **frmKontejneriNaPrijemnici** — `Dokumenta/frmKontejneriNaPrijemnici.cs` — „?“
      - **frmPrijemVozaIzPlana** — `Uvoz/frmPrijemVozaIzPlana.cs` — „Potvrdi“
        - **frmPrijemKontejneraKamionLegetIzvoz** — `Dokumenta/frmPrijemKontejneraKamionLegetIzvoz.cs` — „OTVORI - GATE IN KAMION IZVOZ“
        - **frmPrijemKontejneraLegetVozUvoz** — `Dokumenta/frmPrijemKontejneraLegetVozUvoz.cs` — „OTVORI - GATE IN VOZ“
          - **frmDokumentaPrijemKontejneraVoz** — `Dokumenta/frmDokumentaPrijemKontejneraVoz.cs` — „Dokumenta prijema“
          - **frmPovezivanjeKontejneraIVagona** — `Dokumenta/frmPovezivanjeKontejneraIVagona.cs` — „Povezivanje vagona“
      - **frmScenarioSCI** — `RNI/frmScenarioSCI.cs` — „Pregledaj podatke“
      - **RN12MedjuskladisniKontejnera** — `RadniNalozi/RN12MedjuskladisniKontejnera.cs` — „Potvrdi“
        - **frmDodelaSkladista** — `RadniNalozi/frmDodelaSkladista.cs` — „Dodela polja“
          - **frmPregledSkladistaNovi** — `RadniNalozi/frmPregledSkladistaNovi.cs` — „MAPA SKLADISTA“
        - **frmKontejnerTekuce** — `Uvoz/frmKontejnerTekuce.cs` — „KONTEJNER NA SKLADISTU“
      - **RN1PrijemVoza** — `RadniNalozi/RN1PrijemVoza.cs` — „Otvori RN“
      - **RN2OtpremaVoza** — `RadniNalozi/RN2OtpremaVoza.cs` — „Otvori RN“
      - **RN4PrijemPlatforme** — `RadniNalozi/RN4PrijemPlatforme.cs` — „Otvori RN“
      - **RN5PrijemPlatforme2** — `RadniNalozi/RN5PrijemPlatforme2.cs` — „Otvori RN“
      - **RN6OtpremaPlatforme** — `RadniNalozi/RN6OtpremaPlatforme.cs` — „Otvori RN“
      - **RN7OtpremaPlatforme2** — `RadniNalozi/RN7OtpremaPlatforme2.cs` — „Otvori RN“
      - **ZapisnikONepravilnosti** — `Uvoz/ZapisnikONepravilnosti.cs` — „Zapisnik o nepravilnosti“
  - **VozniPlanovi** — `MainLeget/Intermodalni/VozniPlanovi.cs` — „Vozni planovi“

## Sve dostupne forme (abecedno)

| Forma | Fajl | Modul |
|---|---|---|
| Administracija1 | `Administracija/Administracija1.cs` | Administracija |
| AdministracijaPravoPristupa | `MainLeget/AdministracijaPravoPristupa.cs` | Administracija, Dugme „!!!“ (samo korisnik test) |
| CarinskoSkladiste | `Skladista main/CarinskoSkladiste.cs` | Operacije skladišta |
| CarinskoSkladistePregled | `Skladista main/CarinskoSkladistePregled.cs` | Operacije skladišta |
| Cerade | `MainLeget/Drumski/Cerade.cs` | Drumski transport |
| DepoMain | `MainLeget/Depocnt/DepoMain.cs` | Depo |
| DogovoreniPosloviIzvoza | `Izvoz/DogovoreniPosloviIzvoza.cs` | Prijem i otprema vozova |
| Drumski1 | `MainLeget/Drumski/Drumski1.cs` | Drumski transport |
| frmAnalizaRadnihNaloga | `RadniNalozi/frmAnalizaRadnihNaloga.cs` | Prijem i otprema vozova |
| frmAutomobilEdit | `Drumski/frmAutomobilEdit.cs` | Drumski transport |
| frmAutomobiliDrumski | `Drumski/frmAutomobiliDrumski.cs` | Drumski transport |
| frmAutoprevozniList2 | `TrackModal/Dokumeta/frmAutoprevozniList2.cs` | Prijem i otprema vozova |
| frmAutoprevozniList2 | `Dokumenta/frmAutoprevozniList2.cs` | Intermodalni terminal, Kapija, Prijem i otprema vozova |
| frmBrojFaktureModal | `Drumski/frmBrojFaktureModal.cs` | Drumski transport |
| frmCarinskiPostupak | `Izvoz/frmCarinskiPostupak.cs` | Logistika izvoza |
| frmCerada | `MainLeget/PrijemIOtpremaKamiona/frmCerada.cs` | Prijem i otprema kamiona |
| frmCeradaOtprema | `MainLeget/PrijemIOtpremaKamiona/frmCeradaOtprema.cs` | Prijem i otprema kamiona |
| frmCIR | `Dokumenta/frmCIR.cs` | Intermodalni terminal, Kapija, Prijem i otprema kamiona, Prijem i otprema vozova |
| frmCIRPregled | `TrackModal/Dokumeta/frmCIRPregled.cs` | Prijem i otprema vozova |
| frmDefinisiPoziciju | `Tehnologija/frmDefinisiPoziciju.cs` | Operacije skladišta, Prijem i otprema vozova |
| frmDodatneUsluge | `Uvoz/frmDodatneUsluge.cs` | Operacije skladišta, Prijem i otprema kamiona, Prijem i otprema vozova |
| frmDodatniList | `TrackModal/Izvestaji/frmDodatniList.cs` | Prijem i otprema vozova |
| frmDodatniList | `Izvestaji/frmDodatniList.cs` | Intermodalni terminal, Kapija, Prijem i otprema vozova |
| frmDodelaSkladista | `RadniNalozi/frmDodelaSkladista.cs` | Intermodalni terminal, Kapija, Operacije pretovari, Prijem i otprema kamiona, Prijem i otprema vozova |
| frmDokumentaOtpremaKontejnera | `TrackModal/Dokumeta/frmDokumentaOtpremaKontejnera.cs` | Prijem i otprema vozova |
| frmDokumentaOtpremaKontejnera | `Dokumenta/frmDokumentaOtpremaKontejnera.cs` | Intermodalni terminal, Kapija, Prijem i otprema vozova |
| frmDokumentaPrijemKontejneraKamion | `Dokumenta/frmDokumentaPrijemKontejneraKamion.cs` | Intermodalni terminal, Kapija, Operacije pretovari, Prijem i otprema vozova |
| frmDokumentaPrijemKontejneraVoz | `Dokumenta/frmDokumentaPrijemKontejneraVoz.cs` | Intermodalni terminal, Kapija, Prijem i otprema vozova |
| frmDopunaPlanaPraznimIzvoz | `Izvoz/frmDopunaPlanaPraznimIzvoz.cs` | Prijem i otprema vozova |
| frmDrumski | `Drumski/frmDrumski.cs` | Drumski transport |
| frmDrumski1 | `Drumski/frmDrumski1.cs` | Drumski transport |
| frmFormaZaVozace | `DrumskiApp/frmFormaZaVozace.cs` | Drumski transport |
| frmFormiranjePlana | `Uvoz/frmFormiranjePlana.cs` | Prijem i otprema vozova |
| frmFormiranjePlanaIzvoz | `Izvoz/frmFormiranjePlanaIzvoz.cs` | Intermodalni terminal, Prijem i otprema vozova |
| frmFotografijePregledac | `MainLeget/PrijemIOtpremaKamiona/frmFotografijePregledac.cs` | Prijem i otprema kamiona |
| frmGrupniUnosPoljaIzvoz | `Izvoz/frmGrupniUnosPoljaIzvoz.cs` | Logistika izvoza |
| frmIzvestajCarinskoObelezje | `Skladista/frmIzvestajCarinskoObelezje.cs` | Operacije skladišta |
| frmIzvozDokumenta | `Izvoz/frmIzvozDokumenta.cs` | Intermodalni terminal, Operacije skladišta, Prijem i otprema kamiona, Prijem i otprema vozova |
| frmIzvozKonacna | `Izvoz/frmIzvozKonacna.cs` | Intermodalni terminal, Prijem i otprema vozova |
| frmIzvozKonacnaTable | `Izvoz/frmIzvozKonacnaTable.cs` | Intermodalni terminal, Prijem i otprema vozova |
| frmIzvozKopiranjeUslugaKontejnera1 | `Izvoz/frmIzvozKopiranjeUslugaKontejnera1.cs` | Intermodalni terminal, Logistika izvoza, Prijem i otprema vozova |
| frmIzvozPregledKontejneraDrumskeUsluge | `Izvoz/frmIzvozPregledKontejneraDrumskeUsluge.cs` | Intermodalni terminal, Logistika izvoza, Prijem i otprema vozova |
| frmIzvozUnosManipulacije | `Izvoz/frmIzvozUnosManipulacije.cs` | Intermodalni terminal, Logistika izvoza, Prijem i otprema vozova |
| frmKamionDetalji | `Kapija/frmKamionDetalji.cs` | Kapija |
| frmKapija1 | `MainLeget/Kapija/frmKapija1.cs` | Kapija |
| frmKapijaOtvoreniNalozi | `Kapija/frmKapijaOtvoreniNalozi.cs` | Kapija |
| frmKontaktOsobe | `Dokumenta/frmKontaktOsobe.cs` | Drumski transport, Intermodalni terminal, Kapija, Logistika izvoza, Podešavanja sistema, Prijem i otprema vozova |
| frmKontaktOsobeMU | `Izvoz/frmKontaktOsobeMU.cs` | Intermodalni terminal, Prijem i otprema vozova |
| frmKontejneriNaPrijemnici | `Dokumenta/frmKontejneriNaPrijemnici.cs` | Intermodalni terminal, Kapija, Operacije pretovari, Prijem i otprema vozova |
| frmKontejnerTekuce | `Uvoz/frmKontejnerTekuce.cs` | Intermodalni terminal, Kapija, Operacije pretovari, Operacije skladišta, Prijem i otprema vozova |
| frmKontejnerTekuceArhiv | `Uvoz/frmKontejnerTekuceArhiv.cs` | Prijem i otprema vozova |
| frmKorisnici | `Administracija/frmKorisnici.cs` | Administracija |
| frmLagerOperater | `Promet/frmLagerOperater.cs` | Intermodalni terminal, Kapija, Prijem i otprema vozova |
| frmListaProtokola | `Drumski/frmListaProtokola.cs` | Drumski transport |
| frmLogTokaProcesa | `Izvoz/frmLogTokaProcesa.cs` | Logistika izvoza |
| frmManipulacije | `TrackModal/Dokumeta/frmManipulacije.cs` | Prijem i otprema vozova |
| frmManipulacije | `Dokumenta/frmManipulacije.cs` | Intermodalni terminal, Kapija, Prijem i otprema vozova |
| frmNajavaKomitemtaDokument | `TrackModal/Dokumeta/frmNajavaKomitemtaDokument.cs` | Prijem i otprema vozova |
| frmNajavaKomitemtaDokument | `Dokumenta/frmNajavaKomitemtaDokument.cs` | Intermodalni terminal, Kapija, Prijem i otprema vozova |
| frmNalogZaFakturisanje | `Drumski/frmNalogZaFakturisanje.cs` | Drumski transport |
| frmNalogZaPrevoz | `TrackModal/Dokumeta/frmNalogZaPrevoz.cs` | Prijem i otprema vozova |
| frmNalogZaPrevoz | `Dokumenta/frmNalogZaPrevoz.cs` | Intermodalni terminal, Kapija, Prijem i otprema vozova |
| frmObradaUlaznihFaktura | `Drumski/frmObradaUlaznihFaktura.cs` | Drumski transport |
| frmOtpremaKontejnera | `TrackModal/Dokumeta/frmOtpremaKontejnera.cs` | Prijem i otprema vozova |
| frmOtpremaKontejnera | `Dokumenta/frmOtpremaKontejnera.cs` | Intermodalni terminal, Kapija, Prijem i otprema vozova |
| frmOtpremaKontejneraIzvozKamion | `Dokumenta/frmOtpremaKontejneraIzvozKamion.cs` | Intermodalni terminal, Kapija, Prijem i otprema vozova |
| frmOtpremaKontejneraKamionomIzKontejnera | `Izvoz/frmOtpremaKontejneraKamionomIzKontejnera.cs` | Intermodalni terminal, Kapija, Prijem i otprema vozova |
| frmOtpremaKontejneraLegetIZVOZ | `Dokumenta/frmOtpremaKontejneraLegetIZVOZ.cs` | Intermodalni terminal, Prijem i otprema vozova |
| frmOtpremaKontejneraUvozKamion | `Dokumenta/frmOtpremaKontejneraUvozKamion.cs` | Intermodalni terminal, Kapija, Prijem i otprema vozova |
| frmOtpremaVozaIzPlana | `Izvoz/frmOtpremaVozaIzPlana.cs` | Intermodalni terminal, Prijem i otprema vozova |
| frmPartnerji | `Sifarnici/frmPartnerji.cs` | Drumski transport, Podešavanja sistema |
| frmPartnerjiFirme | `Sifarnici/frmPartnerjiFirme.cs` | Drumski transport, Podešavanja sistema |
| frmPlatforma | `MainLeget/PrijemIOtpremaKamiona/frmPlatforma.cs` | Prijem i otprema kamiona |
| frmPlatformaOtprema | `MainLeget/PrijemIOtpremaKamiona/frmPlatformaOtprema.cs` | Prijem i otprema kamiona |
| frmPodesavanjeRaspolozivosti | `Drumski/frmPodesavanjeRaspolozivosti.cs` | Drumski transport |
| frmPopisKontejneraStampa | `Dokumenta/frmPopisKontejneraStampa.cs` | Prijem i otprema vozova |
| frmPopisKonterjnera | `Dokumenta/frmPopisKonterjnera.cs` | Prijem i otprema vozova |
| frmPovezivanjeKontejneraIVagona | `Dokumenta/frmPovezivanjeKontejneraIVagona.cs` | Intermodalni terminal, Kapija, Prijem i otprema vozova |
| frmPozicija | `TrackModal/Sifarnici/frmPozicija.cs` | Podešavanja sistema |
| frmPregledFajlova | `Drumski/FormaPregledFajlova.cs` | Drumski transport |
| frmPregledGrupaKontejneraIzvoz | `Izvoz/frmPregledGrupaKontejneraIzvoz.cs` | Logistika izvoza |
| frmPregledKamiona | `Kapija/frmPregledKamiona.cs` | Kapija |
| frmPregledKontejneraDrumskeUsluge | `Uvoz/frmPregledKontejneraDrumskeUsluge.cs` | Prijem i otprema vozova |
| frmPregledNalogaDrumski | `Drumski/frmPregledNalogaDrumski.cs` | Drumski transport |
| frmPregledNarucenihManipulacija | `Dokumenta/frmPregledNarucenihManipulacija.cs` | Intermodalni terminal, Kapija, Prijem i otprema vozova |
| frmPregledOtpreme | `TrackModal/Dokumeta/frmPregledOtpreme.cs` | Prijem i otprema vozova |
| frmPregledPorudzbenica | `Izvoz/frmPreglegPorudzbenica.cs` | Logistika izvoza |
| frmPregledPorukeVozacu | `Drumski/frmPregledPorukeVozacu.cs` | Drumski transport |
| frmPregledSkladistaNovi | `RadniNalozi/frmPregledSkladistaNovi.cs` | Intermodalni terminal, Kapija, Operacije pretovari, Prijem i otprema kamiona, Prijem i otprema vozova |
| frmPregledVozova | `TrackModal/Dokumeta/frmPregledVozova.cs` | Prijem i otprema vozova |
| frmPretovariSkladista | `MainLeget/Pretovari/frmPretovariSkladista.cs` | Operacije pretovari |
| frmPretovarPrijem | `MainLeget/Pretovari/frmPretovarPrijem.cs` | Operacije pretovari |
| frmPrijemIOtpremaCerada | `MainLeget/PrijemIOtpremaKamiona/frmPrijemIOtpremaCerada.cs` | Prijem i otprema kamiona |
| frmPrijemIOtpremaKamiona1 | `MainLeget/PrijemIOtpremaKamiona/frmPrijemIOtpremaKamiona1.cs` | Prijem i otprema kamiona |
| frmPrijemIOtpremaPlatforma | `MainLeget/PrijemIOtpremaKamiona/frmPrijemIOtpremaPlatforma.cs` | Prijem i otprema kamiona |
| frmPrijemIOtpremaVozova2 | `MainLeget/Prijemiotpremavozova/frmPrijemIOtpremaVozova2.cs` | Prijem i otprema vozova |
| frmPrijemIOtpremaVozova3 | `MainLeget/Prijemiotpremavozova/frmPrijemIOtpremaVozova3.cs` | Prijem i otprema vozova |
| frmPrijemIOtpremaVozova4 | `MainLeget/Prijemiotpremavozova/frmPrijemIOtpremaVozova4.cs` | Prijem i otprema vozova |
| frmPrijemKamionaDetalji | `MainLeget/PrijemIOtpremaKamiona/frmPrijemKamionaDetalji.cs` | Prijem i otprema kamiona |
| frmPrijemKamionaIzPlana | `Uvoz/frmPrijemKamionaIzPlana.cs` | Intermodalni terminal, Prijem i otprema vozova |
| frmPrijemKontejneraKamionLegetIzvoz | `Dokumenta/frmPrijemKontejneraKamionLegetIzvoz.cs` | Intermodalni terminal, Kapija, Prijem i otprema vozova |
| frmPrijemKontejneraKamionLegetUvoz | `Dokumenta/frmPrijemKontejneraKamionLegetUvoz.cs` | Intermodalni terminal, Kapija, Operacije pretovari, Prijem i otprema vozova |
| frmPrijemKontejneraLegetVozUvoz | `Dokumenta/frmPrijemKontejneraLegetVozUvoz.cs` | Intermodalni terminal, Kapija, Prijem i otprema vozova |
| frmPrijemVozaIzPlana | `Uvoz/frmPrijemVozaIzPlana.cs` | Intermodalni terminal, Kapija, Prijem i otprema vozova |
| frmPrijemVozomPregled | `TrackModal/Dokumeta/frmPrijemVozomPregled.cs` | Prijem i otprema vozova |
| frmProdajniNalogIzvoz | `Izvoz/frmProdajniNalogIzvoz.cs` | Logistika izvoza |
| frmProdajniNalogIzvozTabela | `Izvoz/frmProdajniNalogIzvozTabela.cs` | Logistika izvoza |
| frmProtokolTransportnogNaloga | `Drumski/frmProtokolTransportnogNaloga.cs` | Drumski transport |
| frmPutniNalog | `TrackModal/Dokumeta/frmPutniNalog.cs` | Prijem i otprema vozova |
| frmPutniNalog | `Dokumenta/frmPutniNalog.cs` | Intermodalni terminal, Kapija, Prijem i otprema vozova |
| frmRadniNalogInterniPregled | `Uvoz/frmRadniNalogInterniPregled.cs` | Intermodalni terminal, Prijem i otprema vozova |
| frmRadniNalogTransport | `TrackModal/Dokumeta/frmRadniNalogTransport.cs` | Prijem i otprema vozova |
| frmRadniNalogTransport | `Dokumenta/frmRadniNalogTransport.cs` | Intermodalni terminal, Kapija, Prijem i otprema vozova |
| frmRelacija | `Izvoz/frmRelacija.cs` | Logistika izvoza |
| frmRN12InterniPrenos | `TerminalMap/frmRN12InterniPrenos.cs` | Operacije skladišta, Prijem i otprema vozova |
| frmScenarioSCI | `RNI/frmScenarioSCI.cs` | Intermodalni terminal, Operacije skladišta, Prijem i otprema vozova |
| frmSeniranjeDokumenata | `Drumski/frmSeniranjeDokumenata.cs` | Operacije skladišta |
| frmSkladista | `TrackModal/Sifarnici/frmSkladista.cs` | Podešavanja sistema |
| frmSkladisteObjekat | `TrackModal/Sifarnici/frmSkladisteObjekat.cs` | Podešavanja sistema |
| frmStatus | `Drumski/frmStatus.cs` | Drumski transport |
| frmTerminalPrivremeni | `RNI/frmTerminalPrivremeni.cs` | Intermodalni terminal |
| frmTerminalPrivremeniArhiva | `RNI/frmTerminalPrivremeniArhiva.cs` | Intermodalni terminal |
| frmTerminalPrivremeniDokumenta | `RNI/frmTerminalPrivremeniDokumenta.cs` | Intermodalni terminal |
| frmTerminalPrivremeniExcel | `RNI/frmTerminalPrivremeniExcel.cs` | Intermodalni terminal |
| frmTerminalPrivremeniLog | `RNI/frmTerminalPrivremeniLog.cs` | Intermodalni terminal |
| frmTerminalPrivremeniPromeni | `RNI/frmTerminalPrivremeniPromeni.cs` | Intermodalni terminal |
| frmTerminalPrivremeniSlike | `RNI/frmTerminalPrivremeniSlike.cs` | Intermodalni terminal |
| frmTipKontejnera | `TrackModal/Sifarnici/frmTipKontejnera.cs` | Podešavanja sistema |
| frmTipPalete | `TrackModal/Sifarnici/frmTipPalete.cs` | Operacije skladišta |
| frmTovarniList | `Testiranje/frmTovarniList.cs` | Intermodalni terminal, Kapija, Prijem i otprema vozova |
| frmUnosManipulacija | `Uvoz/frmUnosManipulacija.cs` | Prijem i otprema vozova |
| frmUvozKonacna | `Uvoz/UvozKonacna.cs` | Prijem i otprema vozova |
| frmUvozKonacnaTable | `Uvoz/frmUvozKonacnaTable.cs` | Prijem i otprema vozova |
| frmUvozKopirajUslugeKontejnera | `Uvoz/frmUvozKopirajUslugeKontejnera.cs` | Prijem i otprema vozova |
| frmUvozUvoznici | `Uvoz/frmUvozUvoznici.cs` | Prijem i otprema vozova |
| frmVoz | `TrackModal/Dokumeta/frmVoz.cs` | Prijem i otprema vozova |
| frmVoziloUsluga | `Uvoz/frmVoziloUsluga.cs` | Prijem i otprema vozova |
| frmVozOtprema | `MainLeget/Prijemiotpremavozova/frmVozOtprema.cs` | Prijem i otprema vozova |
| frmVozPrijem | `MainLeget/Prijemiotpremavozova/frmVozPrijem.cs` | Prijem i otprema vozova |
| frmZone | `TrackModal/Sifarnici/frmZone.cs` | Podešavanja sistema |
| GlavniEkran | `MainLeget/GlavniEkran.cs` | Početni ekran (Dashboard) |
| Intermodalni1 | `MainLeget/Intermodalni/Intermodalni1.cs` | Intermodalni terminal |
| IntermodalniKomN | `MainLeget/Intermodalni/IntermodalniKomN.cs` | Intermodalni terminal |
| InterniPoslovi | `Skladista main/Dokumenta/InterniPoslovi.cs` | Operacije skladišta |
| InterniPrenos | `Skladista main/Dokumenta/InterniPrenos.cs` | Operacije skladišta |
| IzvestajVaganja | `Izvoz/IzvestajVaganja.cs` | Prijem i otprema kamiona |
| KomercijalnoSkladiste | `Skladista main/KomercijalnoSkladiste.cs` | Operacije skladišta |
| LogistikaIzvoza0 | `MainLeget/Izvoz/LogistikaIzvoza0.cs` | Logistika izvoza |
| LogistikaIzvoza1 | `MainLeget/Izvoz/LogistikaIzvoza1.cs` | Logistika izvoza |
| LogistikaIzvoza2 | `MainLeget/Izvoz/LogistikaIzvoza2.cs` | Logistika izvoza |
| MagacinskaKnjigaCarinska | `Skladista main/MagacinskaKnjigaCarinska.cs` | Operacije skladišta |
| NalogIzvozaZaOtpremu | `MainLeget/Izvoz/NalogIzvozaZaOtpremu.cs` | Logistika izvoza |
| NalogZaDrumski | `MainLeget/Drumski/NalogZaDrumski.cs` | Drumski transport |
| NalogZaKomercijalni | `Skladista main/NalogZaKomercijalni.cs` | Operacije skladišta |
| NewMain | `MainLeget/NewMain.cs` | — |
| Otprema | `Skladista main/Dokumenta/Otprema.cs` | Operacije skladišta |
| OtpremaLager | `Skladista main/OtpremaLager.cs` | Operacije skladišta |
| Otpremnica | `RadniNalozi/Otpremnica.cs` | Operacije pretovari |
| Otpremnica | `Skladista main/Dokumenta/Otpremnica.cs` | Operacije skladišta |
| OtpremnicaPregled | `RadniNalozi/OtpremnicaPregled.cs` | Operacije pretovari |
| PakovanjeKamiona1 | `Drumski/PakovanjeKamiona1.cs` | Drumski transport |
| PakovanjeKamionaCerade | `Drumski/PakovanjeKamionaCerade.cs` | Drumski transport |
| PaletePrebacivanje | `Skladista main/PaletePrebacivanje.cs` | Operacije skladišta |
| Platforma | `MainLeget/Drumski/Platforma.cs` | Drumski transport |
| Podesavanje1 | `MainLeget/Podesavanjesistema/Podesavanje1.cs` | Podešavanja sistema |
| Pregledac | `MainLeget/PrijemIOtpremaKamiona/Pregledac.cs` | Prijem i otprema kamiona |
| PregledKomercijalnihNaloga | `Skladista main/PregledKomercijalnihNaloga.cs` | Operacije skladišta |
| PregledLagera | `Skladista main/Dokumenta/PregledLagera.cs` | Operacije skladišta |
| Pretovar | `Skladista main/Dokumenta/Pretovar.cs` | Operacije skladišta |
| PretovariMain | `MainLeget/Pretovari/PretovariMain.cs` | Operacije pretovari |
| PretovariPrijem | `MainLeget/Pretovari/PretovariPrijem.cs` | Operacije pretovari |
| Prijem | `Skladista main/Dokumenta/Prijem.cs` | Operacije skladišta |
| PrijemIOtpremaVozova1 | `MainLeget/Prijemiotpremavozova/PrijemIOtpremaVozova1.cs` | Prijem i otprema vozova |
| Prijemnica | `Skladista main/Dokumenta/Prijemnica.cs` | Operacije skladišta |
| Prijemnica | `RadniNalozi/Prijemnica.cs` | Operacije pretovari |
| PrijemnicaPregled | `RadniNalozi/PrijemnicaPregled.cs` | Operacije pretovari |
| RadniNaloziSkladista | `Skladista main/RadniNaloziSkladista.cs` | Operacije skladišta |
| RN12MedjuskladisniKontejnera | `RadniNalozi/RN12MedjuskladisniKontejnera.cs` | Intermodalni terminal, Operacije pretovari, Prijem i otprema vozova |
| RN1PrijemVoza | `RadniNalozi/RN1PrijemVoza.cs` | Intermodalni terminal, Kapija, Prijem i otprema vozova |
| RN2OtpremaVoza | `RadniNalozi/RN2OtpremaVoza.cs` | Intermodalni terminal, Prijem i otprema vozova |
| RN4PrijemPlatforme | `RadniNalozi/RN4PrijemPlatforme.cs` | Intermodalni terminal, Kapija, Prijem i otprema vozova |
| RN5PrijemPlatforme2 | `RadniNalozi/RN5PrijemPlatforme2.cs` | Intermodalni terminal, Kapija, Prijem i otprema vozova |
| RN6OtpremaPlatforme | `RadniNalozi/RN6OtpremaPlatforme.cs` | Intermodalni terminal, Kapija, Prijem i otprema vozova |
| RN7OtpremaPlatforme2 | `RadniNalozi/RN7OtpremaPlatforme2.cs` | Intermodalni terminal, Kapija, Prijem i otprema vozova |
| SkladistaMain | `Skladista main/SkladistaMain.cs` | Operacije skladišta |
| SkladisteGrupa | `TrackModal/Sifarnici/SkladisteGrupa.cs` | Podešavanja sistema |
| TerminalMapFRM | `TerminalMap/TerminalMapFRM.cs` | Operacije skladišta, Prijem i otprema vozova |
| TipSkladista | `Skladista main/TipSkladista.cs` | Operacije skladišta |
| UvozDokumenta | `Uvoz/UvozDokumenta.cs` | Operacije skladišta, Prijem i otprema kamiona, Prijem i otprema vozova |
| UvozNHM | `Uvoz/UvozNHM.cs` | Prijem i otprema vozova |
| Vaganje | `Izvoz/Vaganje.cs` | Prijem i otprema kamiona |
| VaganjePregled | `Izvoz/VaganjePregled.cs` | Prijem i otprema kamiona |
| VanGabaritni | `MainLeget/Drumski/VanGabaritni.cs` | Drumski transport |
| VanredniPoslovi | `Skladista main/Dokumenta/VanredniPoslovi.cs` | Operacije skladišta |
| ViljuskaristiPregled | `Skladista main/ViljuskaristiPregled.cs` | Operacije skladišta |
| VozniPlanovi | `MainLeget/Intermodalni/VozniPlanovi.cs` | Intermodalni terminal |
| ZapisnikONepravilnosti | `Uvoz/ZapisnikONepravilnosti.cs` | Intermodalni terminal, Prijem i otprema kamiona, Prijem i otprema vozova |
| ZapisnikOOstecenjuRobe | `Uvoz/ZapisnikOOstecenjuRobe.cs` | Operacije pretovari |

## Forme do kojih kod vodi, ali za Leget nisu dostupne

Ove forme se ne mogu otvoriti iz `NewMain` za Leget, iako ih neka dostupna forma pominje. U listi su samo forme kojima nijedan drugi put nije otvoren.

**Kontrola je sakrivena za Leget.** Najčešće je to stara traka menija `meniHeader`, koja se za Leget krije, a umesto nje se prikazuje `panelHeader` sa dugmadima.

| Forma | Kroz | Otvara |
|---|---|---|
| frmAutomobiliDokumenta | frmAutomobiliDrumski → „Skenirana dokumentacija“ | `Dokumenta/frmAutomobiliDokumenta.cs` |
| frmAutomobiliRegistracija | frmAutomobiliDrumski → „Evidencija registracija“ | `Dokumenta/frmAutomobiliRegistracija.cs` |
| frmAutomobiliServis | frmAutomobiliDrumski → „Evidencija Servisa“ | `Dokumenta/frmAutomobiliServis.cs` |
| frmEvidencijaKvarova | frmAutomobiliDrumski → „Evidencija kvarova“ | `Dokumenta/frmEvidencijaKvarova.cs` |
| frmEvidencijaTroskova | frmAutomobiliDrumski → „Evidencija troškova iz UF“ | `Dokumenta/frmEvidencijaTroskova.cs` |
| RN10PregledIspraznjenogKontejnera | frmPrijemKontejneraKamionLegetUvoz → „PREGLED ISPRAZNJENJOG KONTEJNERA“ | `RadniNalozi/RN10PregledIspraznjenogKontejnera.cs` |
| RN11PreglediPostavkaKontejnera | frmOtpremaKontejneraLegetIZVOZ → „PREGLEDI I POSTAVKA KONTEJNERA“ | `RadniNalozi/RN11PreglediPostavkaKontejnera.cs` |
| RN8OtpremaCirade | frmOtpremaKontejneraUvozKamion → „OTPREMA CIRADE“ | `RadniNalozi/RN8OtpremaCirade.cs` |
| RN9PrijemCirade | frmPrijemKontejneraKamionLegetUvoz → „PRIJEM CIRADE“ | `RadniNalozi/RN9PrijemCirade.cs` |
| VSDMain | NewMain → „VSD“ | `VSD/VSDMain.cs` |

**Kod se izvršava samo za druge firme.** Grana `if (Firma == ...)` koja za Leget ne važi.

| Forma | Kroz | Otvara |
|---|---|---|
| frmLogovanje | frmCIR → `toolStripLabel1_Click` | `Sifarnici/frmLogovanje.cs` |
| frmLogovanje | frmNalogZaPrevoz → `toolStripLabel1_Click` | `Sifarnici/frmLogovanje.cs` |

**Handler nije zakačen ni za jednu kontrolu (mrtav kod).** Metoda postoji i otvara formu, ali je ništa ne poziva.

| Forma | Mrtva metoda | Otvara |
|---|---|---|
| frmPrijemKontejneraVoz | frmPrijemVozomPregled → `toolStripButton1_Click` | `TrackModal/Dokumeta/frmPrijemKontejneraVoz.cs` |
| frmPrijemKontejneraVoz | frmPrijemVozomPregled → `toolStripButton3_Click` | `TrackModal/Dokumeta/frmPrijemKontejneraVoz.cs` |
| RN10PregledIspraznjenogKontejnera | frmPrijemKontejneraLegetVozUvoz → `formirajRadneNalogePREGLEDUVOZToolStripMenuItem_Click` | `RadniNalozi/RN10PregledIspraznjenogKontejnera.cs` |
| RN3PrijemVoza2 | frmPrijemKontejneraLegetVozUvoz → `formirajRadneNalogePunjenjeToolStripMenuItem_Click` | `RadniNalozi/RN3PrijemVoza2.cs` |
| Uvoz | frmUvozKonacna → `toolStripButton4_Click` | `Uvoz/Uvoz.cs` |

## Kako je rađena analiza

- Analiza polazi od `NewMain` i prati svaki `new Forma(...)`, uključujući i one u lambdama, npr. `OtvoriFormuSaPravom("Kartica", () => new X())`. Prati i pomoćne klase koje otvaraju forme.
- Komentarisan kod se ne računa.
- Metoda se računa samo ako je zakačena za događaj kontrole (`+=` u Designer-u) ili je poziva drugi kod koji se izvršava.
- Uslovi `Firma == "..."` i `company == "..."` se računaju kao za Leget. Grane za druge firme se ne računaju.
- Kontrole koje se za Leget sakrivaju (`X.Visible = false` u Leget grani) ne računaju se, kao ni sve unutar njih: meniji, stavke, kontekstni meniji.
- Ograničenja:
  - Prava korisnika iz baze nisu uzeta u obzir.
  - Kontrola koja se sakriva ili prikazuje u zavisnosti od podataka u toku rada računa se kao vidljiva.
  - Do iste forme može da se stigne i iz starog menija `MainP`, ali to ovde nije prikazano.
- Analiza je rađena nad verzijom koda 23ae378 od 24.10.2026. (grana `master`). Skripta je u `tools/analiza-formi/` i može ponovo da se pokrene posle izmena u kodu.

