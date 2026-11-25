# Leget — tabele, forme i funkcionalnosti (SaobracajMTA)

Izvučeno statičkom analizom izvornog koda iz `D:\Cloude\LG\SaobracajMTA` (ništa u projektu nije menjano). Obuhvata forme do kojih korisnik firme **Leget** dolazi iz novog menija `NewMain` (pravila: Leget grane `Firma == "Leget"`, bez mrtvog koda i sakrivenih kontrola — isti metod kao u `docs/FORME-LEGET.md`, ali pokrenut nad trenutnim kodom).

## Sadržaj

1. Ukratko
2. Ulaz u aplikaciju i glavni meni
3. Stablo formi po modulima (ko koga otvara)
4. Tabele (abecedno): ko ih koristi i kako
5. Stored procedure
6. Forme: tabele i funkcionalnosti po formi
7. Metod i ograničenja

## 1. Ukratko

- Forme dostupne za Leget: **232**
- Različitih tabela/pogleda u SQL upitima tih formi (uključujući pomoćne klase koje forme pozivaju): **309**
- Različitih stored procedura: **568**
- Od toga tabela u koje forme upisuju/menjaju/brišu: **37**
- Pristup bazi je uglavnom ručni SQL (`SqlCommand`) u formama i u pomoćnim klasama tipa `InsertXxx`/`UpdateXxx`; zato su tabele iz pomoćnih klasa pripisane formi koja ih poziva (oznaka „preko“).

Oznake operacija: **S** = čitanje (SELECT/JOIN), **I** = INSERT, **U** = UPDATE, **D** = DELETE.

## 2. Ulaz u aplikaciju i glavni meni


1. `Program.cs` u folderu programa nalazi `Leget.txt` i otvara `Sifarnici/frmLogovanjeSecond` („Prijava na Leget plus nalog“).
2. Na prijavi postoje dva polja za izbor izgleda:
   - „Stari izgled“ (podrazumevano uključeno) otvara stari glavni meni `MainP`. On nije deo ove analize.
   - „Novi izgled“ otvara `MainLeget/NewMain`.
   - Ako su uključena oba, otvaraju se oba menija.
3. Klik na naslov „Prijava na Leget plus nalog“ otvara `Administracija/frmSistemskaTabela`. To je skrivena prečica, dostupna i pre prijave.


| Dugme | Prva forma | Formi u modulu |
|---|---|---|
| Logistika uvoza | — | 0 (dugme samo menja boju, ne otvara formu) |
| Logistika izvoza | `LogistikaIzvoza1` | 16 |
| Logistika direktnih klijenata | — | 0 (dugme samo menja boju, ne otvara formu) |
| Drumski transport | `Drumski1` | 25 |
| Železnički transport | — | 0 (dugme samo menja boju, ne otvara formu) |
| Operacije skladišta | `SkladistaMain` | 35 |
| Intermodalni terminal | `Intermodalni1` | 60 |
| Prijem i otprema vozova | `PrijemIOtpremaVozova1` | 89 |
| Prijem i otprema kamiona | `frmPrijemIOtpremaKamiona1` | 20 |
| Depo | `DepoMain` | 1 |
| Operacije pretovari | `PretovariMain` | 16 |
| PTI | — | 0 (dugme samo menja boju, ne otvara formu) |
| Održavanje | `frmSERMeni` | 14 |
| Kapija | `frmKapija1` | 37 |
| Finansije | — | 0 (dugme samo menja boju, ne otvara formu) |
| Podešavanja sistema | `Podesavanje1` | 10 |
| Administracija | `Administracija1` | 5 |

Još u istom prozoru:

- HR modul: `frmHRMeni`.
- Dugme „!!!“ u zaglavlju otvara `AdministracijaPravoPristupa` (1 forma). Vidljivo je samo korisniku `test`.
- Početni ekran (Dashboard): `GlavniEkran`.
- Dugme „VSD“ otvara `VSD/VSDMain`, ali je sakriveno za sve firme osim VSD. Zato ga ova lista ne računa.
- „Početni meni“, „Dashboard“, „Povratak na predhodni meni“ i „Logout“ ne otvaraju nove forme.


## 3. Stablo formi po modulima (ko koga otvara)


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

### HR modul (15)

- **frmHRMeni** — `HR/frmHRMeni.cs`
  - **frmHREvidencijaRada** — `HR/frmHREvidencijaRada.cs` — pri otvaranju forme
    - **frmHRDan** — `HR/frmHRDan.cs` — metoda `OtvoriDan`
    - **frmHRGrupniUnos** — `HR/frmHRGrupniUnos.cs` — metoda `GrupniUnos`
  - **frmHRGOPravo** — `HR/frmHRGOPravo.cs` — pri otvaranju forme
    - **frmHRStavka** — `HR/frmHRStavka.cs` — metoda `Generisi`
  - **frmHRGOZahtevi** — `HR/frmHRGOZahtevi.cs` — pri otvaranju forme
  - **frmHRKontrolnaTabla** — `HR/frmHRKontrolnaTabla.cs` — kartica „Kontrolna tabla rokova“
  - **frmHRLog** — `HR/frmHRLog.cs` — kartica „HR log“
  - **frmHRSifarnici** — `HR/frmHRSifarnici.cs` — kartica „Pravila odobravanja“
  - **frmHRZahteviLista** — `HR/frmHRZahteviLista.cs` — kartica „Zahtevi za nabavku“
    - **frmHRZahtev** — `HR/frmHRZahtev.cs` — pri otvaranju forme
  - **frmHRZaposleniLista** — `HR/frmHRZaposleniLista.cs` — kartica „Zaposleni“
    - **frmHRZaposleni** — `HR/frmHRZaposleni.cs` — pri otvaranju forme
      - **frmHRRevers** — `HR/frmHRRevers.cs` — metoda `PodesiDodatneKomande`

### Administracija (5)

- **Administracija1** — `Administracija/Administracija1.cs`
  - **AdministracijaPravoPristupa** — `MainLeget/AdministracijaPravoPristupa.cs` — „Prava“
  - **frmKorisnici** — `Administracija/frmKorisnici.cs` — „Korisnici“
  - **frmPodesavanja** — `Administracija/frmPodesavanja.cs` — „Podešavanja“
    - **frmHRStavka** — `HR/frmHRStavka.cs` — metoda `Izmeni`

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

### Operacije skladišta (35)

- **SkladistaMain** — `Skladista main/SkladistaMain.cs`
  - **CarinskoSkladiste** — `Skladista main/CarinskoSkladiste.cs` — „Carinsko skladište“
    - **CarinskoSkladistePregled** — `Skladista main/CarinskoSkladistePregled.cs` — „Prijem robe“
      - **frmIzvestajCarinskoObelezje** — `Skladista/frmIzvestajCarinskoObelezje.cs` — „Štampaj“
      - **frmSeniranjeDokumenata** — `Drumski/frmSeniranjeDokumenata.cs` — „Skeniraj“
      - **Prijemnica** — `Skladista main/Dokumenta/Prijemnica.Palete.cs` — „Prijemnica“
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
    - **KomercijalnoSkladistePregled** — `Skladista main/KomercijalnoSkladistePregled.cs` — „Prijem robe“
  - **NalogZaKomercijalni** — `Skladista main/NalogZaKomercijalni.cs` — kartica „Radni nalozi“
    - **PregledKomercijalnihNaloga** — `Skladista main/PregledKomercijalnihNaloga.cs` — „SKLADIŠNI NALOZI UVOZ“
      - **frmNalogPrijem** — `Skladista main/frmNalogPrijem.Ispravke.cs` — „Otvori“
        - **frmMestoIsporuke** — `Skladista main/frmMestoIsporuke.cs` — „Mesto Istovara“
        - **frmTipPalete** — `TrackModal/Sifarnici/frmTipPalete.cs` — „?“
        - **frmVozac** — `Skladista main/frmVozac.cs` — „Vozač“
      - **frmScenarioSCI** — `RNI/frmScenarioSCI.cs` — „Pregledaj podatke“
      - **Otprema** — `Skladista main/Dokumenta/Otprema.cs` — „Otvori“
        - **OtpremaLager** — `Skladista main/OtpremaLager.cs` — „IZABERI SA LAGERA“
        - **Otpremnica** — `Skladista main/Dokumenta/Otpremnica.cs` — „IDI NA OTPREMNICU“
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

### Održavanje (14)

- **frmSERMeni** — `Servis/frmSERMeni.cs`
  - **frmSERKorekcije** — `Servis/frmSERKorekcije.cs` — kartica „Korekcije zaliha“
  - **frmSERNabavka** — `Servis/frmSERNabavka.cs` — kartica „Potrebe za nabavkom“
    - **frmSERNarudzbenica** — `Servis/frmSERNarudzbenica.cs` — metoda `Otvori`
  - **frmSERNalozi** — `Servis/frmSERNalozi.cs` — kartica „Radni nalozi“
    - **frmSERNalog** — `Servis/frmSERNalog.cs` — pri otvaranju forme
  - **frmSERObavestenja** — `Servis/frmSERObavestenja.cs` — kartica „Obaveštenja“
  - **frmSERPopis** — `Servis/frmSERPopis.cs` — kartica „Popis zaliha“
  - **frmSERPromet** — `Servis/frmSERPromet.cs` — kartica „Izdatnice i prijemi“
    - **frmSERPrijem** — `Servis/frmSERPrijem.cs` — metoda `NoviPrijem`
  - **frmSERSifarnici** — `Servis/frmSERSifarnici.cs` — kartica „Sredstva i plan održavanja“
  - **frmSERZalihe** — `Servis/frmSERZalihe.cs` — kartica „Zalihe i šifarnik“
    - **frmSERDeo** — `Servis/frmSERDeo.cs` — pri otvaranju forme
    - **frmSERStavka** — `Servis/frmSERStavka.cs` — metoda `PocetnoStanje`

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

### Intermodalni terminal (60)

- **Intermodalni1** — `MainLeget/Intermodalni/Intermodalni1.cs`
  - **frmTerminalPrivremeni** — `RNI/frmTerminalPrivremeni.cs` — „Terminal temp“
    - **frmTerminalPrivremeniArhiva** — `RNI/frmTerminalPrivremeniArhiva.cs` — „Arhivirani podaci“
    - **frmTerminalPrivremeniDokumenta** — `RNI/frmTerminalPrivremeniDokumenta.cs` — „Zakači dokumenta“
    - **frmTerminalPrivremeniExcel** — `RNI/frmTerminalPrivremeniExcel.cs` — „Uvoz Excel“
    - **frmTerminalPrivremeniLog** — `RNI/frmTerminalPrivremeniLog.cs` — „Pregled log“
    - **frmTerminalPrivremeniOpstiIzvoz** — `RNI/frmTerminalPrivremeniOpstiIzvoz.cs` — „Opšti izvoz“
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


## 4. Tabele (abecedno)

Kolona „Operacije“ je unija nad direktnim SQL upitima. Upisi preko stored procedura nisu vidljivi u upitima, zato kolona „Procedure (po imenu)“ povezuje procedure Insert/Update/Delete/Select + ime tabele sa tabelom — to je poklapanje imena, ne provera sadržaja procedure. „Forme“ navodi do 8 formi, ostale su u odeljku 6.

| Tabela | Operacije | Procedure (po imenu) | Br. formi | Forme |
|---|---|---|---|---|
| Artikli | SU |  | 1 | Prijemnica |
| ArtikliCetvrtiNivo | S |  | 1 | Prijemnica |
| ArtikliDrugiNivo | S |  | 1 | Prijemnica |
| ArtikliKatalog | S |  | 1 | Prijemnica |
| ArtikliKatalogUvoz | I |  | 1 | Prijemnica |
| ArtikliPrviNivo | S |  | 1 | Prijemnica |
| ArtikliTreciNivo | S |  | 1 | Prijemnica |
| Automobili | S | DeleteAutomobili, InsertAutomobili, UpdateAutomobili | 13 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmAutomobiliDrumski, frmDrumski, frmDrumski1, frmIzvozPregledKontejneraDrumskeUsluge, frmNalogZaFakturisanje, frmObradaUlaznihFaktura … (+5) |
| AutomobiliTehnickiProblem | S | DeleteAutomobiliTehnickiProblem, InsertAutomobiliTehnickiProblem | 5 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmAutomobiliDrumski, frmPodesavanjeRaspolozivosti, frmStatus |
| Autoprevoznilist | S | DeleteAutoprevozniList, InsertAutoPrevozniList, UpdateAutoprevoznilist | 1 | frmAutoprevozniList2 |
| BeleskaORadu | S |  | 3 | frmHRDan, frmHREvidencijaRada, frmHRZaposleni |
| Brodovi | S |  | 6 | frmFormiranjePlana, frmRadniNalogInterniPregled, frmUnosManipulacija, frmUvozKonacna, frmUvozKonacnaTable, frmUvozKopirajUslugeKontejnera |
| Carinarnice | S |  | 33 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PakovanjeKamiona1, PakovanjeKamionaCerade, PregledLagera, Prijemnica … (+25) |
| Cene | S |  | 5 | frmIzvozKonacna, frmIzvozUnosManipulacije, frmNalogZaPrevoz, frmUnosManipulacija, frmUvozKonacna |
| CIR | S |  | 2 | frmCIR, frmCIRPregled |
| CIRGreske | S |  | 1 | frmCIR |
| Delavci | S |  | 18 | Otpremnica, PakovanjeKamiona1, PakovanjeKamionaCerade, Prijemnica, PrijemnicaPregled, ViljuskaristiPregled, frmAutomobilEdit, frmAutomobiliDrumski … (+10) |
| Delovi | S |  | 1 | frmCIR |
| DirigacijaKontejneraZa | S |  | 13 | frmFormiranjePlana, frmOtpremaKontejneraLegetIZVOZ, frmOtpremaKontejneraUvozKamion, frmPrijemKamionaIzPlana, frmPrijemKontejneraKamionLegetIzvoz, frmPrijemKontejneraKamionLegetUvoz, frmPrijemKontejneraLegetVozUvoz, frmPrijemVozaIzPlana … (+5) |
| DObavnica | S | InsertDobavnica | 1 | OtpremnicaPregled |
| DobavnicaPostav | S | InsertDobavnicaPostav | 1 | OtpremnicaPregled |
| DogovorenoIzvoz | S | DeleteDogovorenoIzvoz, InsertDogovorenoIzvoz, UpdateDogovorenoIzvoz | 1 | DogovoreniPosloviIzvoza |
| DokumentaFaktureDrumski | S | InsertDokumentaFaktureDrumski, UpdateDokumentaFaktureDrumski | 1 | frmSeniranjeDokumenata |
| DokumentaNajavaKomitenta | S | InsertDokumentaNajavaKomitenta | 1 | frmNajavaKomitemtaDokument |
| DokumentaOtpremaKontejneraVoz | S | DeleteDokumentaOtpremaKontejneraVoz, InsertDokumentaOtpremaKontejneraVoz | 1 | frmDokumentaOtpremaKontejnera |
| DokumentaPregledac | S |  | 1 | frmFotografijePregledac |
| DokumentaPrijemKontejneraKamion | S | InsertDokumentaPrijemKontejneraKamion | 1 | frmDokumentaPrijemKontejneraKamion |
| DokumentaPrijemKontejneraVoz | S |  | 1 | frmDokumentaPrijemKontejneraVoz |
| DokumentaRadnogNalogaDrumski | S |  | 4 | frmNalogZaFakturisanje, frmObradaUlaznihFaktura, frmPregledFajlova, frmStatus |
| DokumentaSkladiste | S | InsertDokumentaSkladiste | 1 | frmSeniranjeDokumenata |
| Dokumenti | S |  | 3 | frmNalogZaFakturisanje, frmObradaUlaznihFaktura, frmStatus |
| DrumskiPozicioniranje | S |  | 5 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmDrumski, frmDrumski1, frmStatus |
| Drzave | S | InsertDrzave | 9 | frmHRDan, frmHRGOPravo, frmHRRevers, frmHRSifarnici, frmHRStavka, frmHRZahtev, frmHRZaposleni, frmPartnerji … (+1) |
| EvidencijaMesec | S |  | 1 | frmHREvidencijaRada |
| EvidencijaRada | S |  | 2 | frmHRDan, frmHREvidencijaRada |
| FakturaDrumski | S | InsertFakturaDrumski | 4 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmNalogZaFakturisanje, frmObradaUlaznihFaktura |
| FakturaDrumskiStavka | S | DeleteFakturaDrumskiStavka, InsertFakturaDrumskiStavka, UpdateFakturaDrumskiStavka | 2 | frmNalogZaFakturisanje, frmObradaUlaznihFaktura |
| fn_Podredjeni | S |  | 10 | frmHRDan, frmHREvidencijaRada, frmHRGOPravo, frmHRGOZahtevi, frmHRKontrolnaTabla, frmHRMeni, frmHRRevers, frmHRZahtev … (+2) |
| fnHR_ZahtevPlanOdobravanja | S |  | 3 | frmHRGrupniUnos, frmHRZahtev, frmHRZahteviLista |
| GODopust | S |  | 5 | frmHREvidencijaRada, frmHRGOPravo, frmHRGOZahtevi, frmHRMeni, frmHRZaposleni |
| GODopustOsnov | S |  | 1 | frmHRGOPravo |
| GODopustStavke | SU |  | 5 | frmHREvidencijaRada, frmHRGOPravo, frmHRGOZahtevi, frmHRMeni, frmHRZaposleni |
| Greske | S |  | 1 | frmCIR |
| GrupaVrsteManipulacije | S |  | 2 | frmIzvozUnosManipulacije, frmUnosManipulacija |
| GrupeDokumenata | S |  | 1 | frmHRZaposleni |
| HRLog | S |  | 1 | frmHRLog |
| HTZGrupe | S |  | 1 | frmHRRevers |
| HTZOprema | S |  | 5 | frmHRGrupniUnos, frmHRRevers, frmHRSifarnici, frmHRZahtev, frmHRZaposleni |
| HTZReversi | SU |  | 3 | frmHRRevers, frmHRZahtev, frmHRZaposleni |
| InspekciskiTretman | S |  | 17 | RN4PrijemPlatforme, frmDopunaPlanaPraznimIzvoz, frmDrumski1, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKonacnaTable, frmIzvozUnosManipulacije … (+9) |
| Izvoz | SIU | UpdateIzvoz | 18 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmAnalizaRadnihNaloga, frmCarinskiPostupak, frmDrumski, frmDrumski1, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz … (+10) |
| IzvozDokumenta | S | DeleteIzvozDokumenta, InsertIzvozDokumenta | 1 | frmIzvozDokumenta |
| IzvozKonacna | SU | UpdateIzvozKonacna | 41 | PakovanjeKamiona1, PakovanjeKamionaCerade, PregledKomercijalnihNaloga, Pregledac, Vaganje, VaganjePregled, frmAnalizaRadnihNaloga, frmDefinisiPoziciju … (+33) |
| IzvozKonacnaNapomenePozicioniranja | S | DeleteIzvozKonacnaNapomenePozicioniranja, InsertIzvozKonacnaNapomenePozicioniranja | 2 | frmIzvozKonacna, frmOtpremaKontejneraLegetIZVOZ |
| IzvozKonacnaNHM | S | DeleteIzvozKonacnaNHM, InsertIzvozKonacnaNHM | 6 | frmIzvozKonacna, frmIzvozKonacnaTable, frmIzvozUnosManipulacije, frmOtpremaKontejneraLegetIZVOZ, frmPrijemKamionaDetalji, frmScenarioSCI |
| IzvozKonacnaVrstaManipulacije | S | DeleteIzvozKonacnaVrstaManipulacije, InsertIzvozKonacnaVrstaManipulacije | 9 | PregledKomercijalnihNaloga, frmIzvozKonacna, frmIzvozKonacnaTable, frmIzvozPregledKontejneraDrumskeUsluge, frmIzvozUnosManipulacije, frmOtpremaKontejneraIzvozKamion, frmOtpremaKontejneraLegetIZVOZ, frmPrijemKontejneraKamionLegetIzvoz … (+1) |
| IzvozKonacnaVrstaRobeHS | S | DeleteIzvozKonacnaVrstaRobeHS, InsertIzvozkonacnaVrstaRobeHS | 3 | frmIzvozKonacna, frmIzvozKonacnaTable, frmIzvozUnosManipulacije |
| IzvozKonacnaZaglavlje | S | DeleteIzvozKonacnaZaglavlje, InsertIzvozKonacnaZaglavlje, UpdateIzvozKonacnaZaglavlje | 10 | PregledKomercijalnihNaloga, frmDopunaPlanaPraznimIzvoz, frmDrumski, frmDrumski1, frmFormiranjePlanaIzvoz, frmIzvozKonacna, frmOtpremaVozaIzPlana, frmPregledNalogaDrumski … (+2) |
| IzvozNapomenePozicioniranja | S | DeleteIzvozNapomenePozicioniranja, InsertIzvozNapomenePozicioniranja | 3 | frmDrumski1, frmGrupniUnosPoljaIzvoz, frmScenarioSCI |
| IzvozNHM | S | DeleteIzvozNHM, InsertIzvozNHM | 6 | frmDopunaPlanaPraznimIzvoz, frmDrumski1, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozUnosManipulacije |
| IzvozVrstaManipulacije | SU | DeleteIzvozVrstaManipulacije, InsertIzvozVrstaManipulacije | 8 | frmDopunaPlanaPraznimIzvoz, frmDrumski1, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozPregledKontejneraDrumskeUsluge, frmIzvozUnosManipulacije, frmPrijemKontejneraKamionLegetUvoz |
| IzvozVrstaRobeHS | S | DeleteIzvozVrstaRobeHS, InsertIzvozVrstaRobeHS | 4 | frmDopunaPlanaPraznimIzvoz, frmFormiranjePlanaIzvoz, frmIzvozKonacna, frmIzvozUnosManipulacije |
| Kapija | S | DeleteKapija, InsertKapija, UpdateKapija | 5 | Pregledac, frmKamionDetalji, frmKapijaOtvoreniNalozi, frmPlatforma, frmPregledKamiona |
| Komitenti | S |  | 9 | frmAutoprevozniList2, frmLagerOperater, frmManipulacije, frmNalogZaPrevoz, frmOtpremaKontejnera, frmOtpremaKontejneraIzvozKamion, frmOtpremaKontejneraLegetIZVOZ, frmOtpremaKontejneraUvozKamion … (+1) |
| KontejnerLog | S | InsertKontejnerLog | 2 | frmLogTokaProcesa, frmPregledGrupaKontejneraIzvoz |
| KontejnerskiTerminali | S |  | 26 | PakovanjeKamiona1, frmAnalizaRadnihNaloga, frmDopunaPlanaPraznimIzvoz, frmDrumski1, frmFormaZaVozace, frmFormiranjePlana, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz … (+18) |
| KontejnerStatus | S |  | 19 | PregledKomercijalnihNaloga, TerminalMapFRM, frmAnalizaRadnihNaloga, frmDodatneUsluge, frmDopunaPlanaPraznimIzvoz, frmFormiranjePlanaIzvoz, frmIzvozKonacna, frmIzvozUnosManipulacije … (+11) |
| KontejnerTekuce | S | InsertKontejnerTekuce, UpdateKontejnerTekuce | 12 | RN12MedjuskladisniKontejnera, RN2OtpremaVoza, RN6OtpremaPlatforme, RN7OtpremaPlatforme2, TerminalMapFRM, frmAnalizaRadnihNaloga, frmDodatneUsluge, frmDodelaSkladista … (+4) |
| KontejnerTekuceArhiv | S |  | 1 | frmKontejnerTekuceArhiv |
| Korisnici | S | DeleteKorisnici, InsertKorisnici | 24 | AdministracijaPravoPristupa, PakovanjeKamiona1, PakovanjeKamionaCerade, frmAutomobilEdit, frmAutomobiliDrumski, frmDrumski, frmDrumski1, frmHREvidencijaRada … (+16) |
| KrajnjaDestinacija | S |  | 6 | frmDopunaPlanaPraznimIzvoz, frmFormiranjePlanaIzvoz, frmIzvozKonacna, frmIzvozKonacnaTable, frmIzvozUnosManipulacije, frmOtpremaKontejneraLegetIZVOZ |
| Lager | S |  | 2 | Otprema, PregledLagera |
| Lokac | S |  | 2 | Otpremnica, Prijemnica |
| MagacinskaKnjigaCarinska | S |  | 1 | MagacinskaKnjigaCarinska |
| MagacinskiBrojCarinski | S | DeleteMagacinskiBrojCarinski, InsertMagacinskiBrojCarinski, UpdateMagacinskiBrojCarinski | 9 | InterniPoslovi, InterniPrenos, Otprema, OtpremaLager, Otpremnica, PregledLagera, Prijemnica, VanredniPoslovi … (+1) |
| MagacinskiPredmet | S | InsertMagacinskiPredmet, UpdateMagacinskiPredmet | 1 | frmNalogPrijem |
| MainNovi | S |  | 20 | AdministracijaPravoPristupa, NewMain, frmHREvidencijaRada, frmHRGOZahtevi, frmHRKontrolnaTabla, frmHRMeni, frmHRRevers, frmHRZahtev … (+12) |
| mainNoviKartice | SID |  | 20 | AdministracijaPravoPristupa, NewMain, frmHREvidencijaRada, frmHRGOZahtevi, frmHRKontrolnaTabla, frmHRMeni, frmHRRevers, frmHRZahtev … (+12) |
| MainNoviPrava | SID |  | 20 | AdministracijaPravoPristupa, NewMain, frmHREvidencijaRada, frmHRGOZahtevi, frmHRKontrolnaTabla, frmHRMeni, frmHRRevers, frmHRZahtev … (+12) |
| MapiranjeTipaNaloga | S |  | 3 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmStatus |
| MaticniPodatki | S |  | 2 | Otpremnica, Prijemnica |
| MerskeEnote | S |  | 3 | Prijemnica, frmNalogPrijem, frmProdajniNalogIzvoz |
| Mesta | S |  | 2 | Otpremnica, Prijemnica |
| MestaUtovara | S | DeleteMestaUtovara, InsertMestaUtovara, UpdateMestaUtovara | 37 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PakovanjeKamiona1, PakovanjeKamionaCerade, PregledLagera, Prijemnica … (+29) |
| NajavaPrevoza | S |  | 2 | frmAutoprevozniList2, frmNalogZaPrevoz |
| NajavaPrevozaTroskovi | S | DeleteNajavaPrevozaTroskovi | 1 | frmNalogZaPrevoz |
| NapomenaZaPozicioniranje | S |  | 9 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmDrumski, frmDrumski1, frmGrupniUnosPoljaIzvoz, frmPregledNalogaDrumski, frmScenarioSCI, frmStatus … (+1) |
| Narocilo | S |  | 1 | Otpremnica |
| NarociloPostav | S |  | 1 | Otpremnica |
| NaruceneManipulacije | S | DeleteNaruceneManipulacije, InsertNaruceneManipulacije | 3 | frmCIR, frmManipulacije, frmPregledNarucenihManipulacija |
| NHM | S |  | 26 | Otpremnica, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN5PrijemPlatforme2, RN6OtpremaPlatforme, RN7OtpremaPlatforme2 … (+18) |
| NNal | S |  | 1 | Prijemnica |
| NNalP | S |  | 1 | Prijemnica |
| NPre | S |  | 2 | Prijemnica, PrijemnicaPregled |
| NPreP | S |  | 2 | Prijemnica, PrijemnicaPregled |
| OceneSposobnosti | S |  | 1 | frmHRZaposleni |
| Opstine | S |  | 9 | frmHRDan, frmHRGOPravo, frmHRGOZahtevi, frmHRRevers, frmHRSifarnici, frmHRStavka, frmHRZahtev, frmHRZaposleni … (+1) |
| organizacioneJedinice | S |  | 30 | PregledKomercijalnihNaloga, frmCerada, frmCeradaOtprema, frmFormiranjePlanaIzvoz, frmHRDan, frmHRGOPravo, frmHRGrupniUnos, frmHRRevers … (+22) |
| OsnovGO | S |  | 3 | frmHRGOPravo, frmHRSifarnici, frmHRZaposleni |
| OtpremaKontejnera | S | DeleteOtpremaKontejnera, InsertOtpremaKontejnera, UpdateOtpremaKontejnera | 12 | frmAnalizaRadnihNaloga, frmCIR, frmManipulacije, frmOtpremaKontejnera, frmOtpremaKontejneraIzvozKamion, frmOtpremaKontejneraKamionomIzKontejnera, frmOtpremaKontejneraLegetIZVOZ, frmOtpremaKontejneraUvozKamion … (+4) |
| OtpremaKontejneraVozStavke | S | DeleteOtpremaKontejneraVozStavke, InsertOtpremaKontejneraVozStavke, UpdateOtpremaKontejneraVozStavke | 13 | Pregledac, frmAnalizaRadnihNaloga, frmCIR, frmCeradaOtprema, frmManipulacije, frmOtpremaKontejnera, frmOtpremaKontejneraIzvozKamion, frmOtpremaKontejneraLegetIZVOZ … (+5) |
| OtpremaKontejneraVozz | S |  | 1 | frmCIR |
| Partnerji | SU | DeletePartnerji | 106 | CarinskoSkladistePregled, DogovoreniPosloviIzvoza, InterniPoslovi, InterniPrenos, KomercijalnoSkladistePregled, Otprema, OtpremaLager, Otpremnica … (+98) |
| PartnerjiFirma | S |  | 2 | frmPartnerji, frmPartnerjiFirme |
| partnerjiKontOseba | S |  | 5 | frmIzvozKonacna, frmKontaktOsobe, frmNalogPrijem, frmPartnerji, frmUvozKonacna |
| partnerjiKontOsebaMU | S |  | 15 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmDrumski, frmDrumski1, frmIzvozKonacna, frmKontaktOsobeMU, frmMestoIsporuke, frmPregledNalogaDrumski … (+7) |
| Podesavanja | SU |  | 5 | frmHREvidencijaRada, frmHRGrupniUnos, frmPodesavanja, frmSERNabavka, frmSERPrijem |
| PodtipProtokolaRazno | S |  | 1 | frmProtokolTransportnogNaloga |
| PomOstali | S |  | 3 | Otprema, OtpremaLager, PregledLagera |
| PopisKontejnera | S | DeletePopisKontejnera, InsertPopisKontejnera, UpdatePopisKontejnera | 1 | frmPopisKonterjnera |
| PopisKontejneraStavke | S | DeletePopisKontejneraStavke, InsertPopisKontejneraStavke, UpdatePopisKontejneraStavke | 1 | frmPopisKonterjnera |
| PosebniUsoviSmestaja | S |  | 1 | frmNalogPrijem |
| Poste | S | InsertPoste | 1 | frmPartnerji |
| PostupakStavkeOtpremaVeza | S |  | 1 | frmOtpremaKontejnera |
| Pozicija | S | DeletePozicija, InsertPozicija, UpdatePozicija | 26 | Otpremnica, OtpremnicaPregled, Prijemnica, PrijemnicaPregled, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme … (+18) |
| PraviloArtikla | S |  | 2 | frmNalogZaFakturisanje, frmObradaUlaznihFaktura |
| Praznici | S |  | 2 | frmHREvidencijaRada, frmHRSifarnici |
| Predefinisaneporuke | S |  | 13 | frmFormiranjePlana, frmIzvozKonacna, frmOtpremaKontejnera, frmOtpremaKontejneraLegetIZVOZ, frmOtpremaKontejneraUvozKamion, frmPrijemKontejneraKamionLegetIzvoz, frmPrijemKontejneraKamionLegetUvoz, frmPrijemKontejneraLegetVozUvoz … (+5) |
| PredefinisanePozicije | S | InsertPredefinisanePozicije, UpdatePredefinisanePozicije | 2 | frmDefinisiPoziciju, frmRN12InterniPrenos |
| PrijemKontejneraVoz | S | DeletePrijemKontejneraVoz, InsertPrijemKontejneraVoz, UpdatePrijemKontejneraVoz | 14 | frmAnalizaRadnihNaloga, frmCIR, frmKapijaOtvoreniNalozi, frmManipulacije, frmOtpremaKontejneraUvozKamion, frmPovezivanjeKontejneraIVagona, frmPregledNarucenihManipulacija, frmPrijemKamionaIzPlana … (+6) |
| PrijemKontejneraVozStavke | S | DeletePrijemKontejneraVozStavke, InsertPrijemKontejneraVozStavke, UpdatePrijemKontejneraVozStavke | 17 | Pregledac, frmAnalizaRadnihNaloga, frmCIR, frmCerada, frmKontejneriNaPrijemnici, frmLagerOperater, frmManipulacije, frmOtpremaKontejneraUvozKamion … (+9) |
| Primljeno | S |  | 3 | Otprema, OtpremaLager, PregledLagera |
| PrioritetiNabavke | S |  | 4 | frmHRGrupniUnos, frmHRRevers, frmHRZahtev, frmHRZaposleni |
| ProdajniNalogIzvoz | S | DeleteProdajniNalogIzvoz, InsertProdajniNalogIzvoz, UpdateProdajniNalogIzvoz | 7 | PregledKomercijalnihNaloga, frmDrumski1, frmGrupniUnosPoljaIzvoz, frmPregledPorudzbenica, frmProdajniNalogIzvoz, frmProdajniNalogIzvozTabela, frmScenarioSCI |
| ProdajniNalogIzvozStavke | S | InsertProdajniNalogIzvozStavke, UpdateProdajniNalogIzvozStavke | 8 | Pregledac, frmDrumski1, frmGrupniUnosPoljaIzvoz, frmPregledGrupaKontejneraIzvoz, frmPregledPorudzbenica, frmProdajniNalogIzvoz, frmProdajniNalogIzvozTabela, frmRadniNalogInterniPregled |
| Promet | S | DeletePromet, InsertPromet | 13 | Otprema, OtpremaLager, Otpremnica, OtpremnicaPregled, PregledLagera, Prijemnica, PrijemnicaPregled, frmLagerOperater … (+5) |
| ProtokolRadniNalogDrumski | S | DeleteProtokolRadniNalogDrumski, InsertProtokolRadniNalogDrumski, UpdateProtokolRadniNalogDrumski | 6 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmListaProtokola, frmNalogZaFakturisanje, frmObradaUlaznihFaktura, frmProtokolTransportnogNaloga |
| PutniNalog | S | DeletePutniNalog, InsertPutniNalog, UpdatePutniNalog | 2 | frmAutoprevozniList2, frmPutniNalog |
| PutniNalogGorivo | S | DeletePutniNalogGorivo, InsertPutniNalogGorivo, UpdatePutniNalogGorivo | 1 | frmPutniNalog |
| PutniNalogGume | S | DeletePutniNalogGume, InsertPutniNalogGume, UpdatePutniNalogGume | 1 | frmPutniNalog |
| PutniNalogKvarovi | S | DeletePutniNalogKvarovi, InsertPutniNalogKvarovi, UpdatePutniNalogKvarovi | 1 | frmPutniNalog |
| PutniNalogRuta | S | DeletePutniNalogRuta, InsertPutniNalogRuta, UpdatePutniNalogRuta | 1 | frmPutniNalog |
| PutniNalogServis | S | DeletePutniNalogServis, InsertPutniNalogServis, UpdatePutniNalogServis | 1 | frmPutniNalog |
| PutniNalogTroskovi | S | DeletePutniNalogTroskovi, InsertPutniNalogTroskovi, UpdatePutniNalogTroskovi | 1 | frmPutniNalog |
| PutniNalogZaposleni | S | DeletePutniNalogZaposleni, InsertPutniNalogZaposleni, UpdatePutniNalogZaposleni | 1 | frmPutniNalog |
| RadnaMesta | S |  | 4 | frmHRRevers, frmHRSifarnici, frmHRZahtev, frmHRZaposleni |
| RadniNalogDrumski | S | DeleteRadniNalogDrumski, InsertRadniNalogDrumski, UpdateRadniNalogDrumski | 15 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmAutomobiliDrumski, frmDrumski, frmDrumski1, frmFormaZaVozace, frmIzvozPregledKontejneraDrumskeUsluge, frmListaProtokola … (+7) |
| RadniNalogDrumskiNHM | S | InsertRadniNalogDrumskiNHM | 1 | frmDrumski1 |
| radninaloginterni | S | DeleteRadniNalogInterni, InsertRadniNalogInterni, UpdateRadniNalogInterni | 43 | CarinskoSkladistePregled, KomercijalnoSkladistePregled, PakovanjeKamiona1, PakovanjeKamionaCerade, PregledKomercijalnihNaloga, Pregledac, Prijemnica, RN12MedjuskladisniKontejnera … (+35) |
| RadniNalogInterniPotvrda | S | InsertRadniNalogInterniPotvrda | 15 | CarinskoSkladistePregled, KomercijalnoSkladistePregled, PregledKomercijalnihNaloga, Pregledac, VaganjePregled, frmCerada, frmCeradaOtprema, frmKapijaOtvoreniNalozi … (+7) |
| RadniNalogInterniSkladistePotvrda | S | InsertRadniNalogInterniSkladistePotvrda | 2 | PregledKomercijalnihNaloga, ViljuskaristiPregled |
| RadniNalogSkladista | S | InsertRadniNalogSkladista, UpdateRadniNalogSkladista | 15 | CarinskoSkladistePregled, InterniPoslovi, InterniPrenos, KomercijalnoSkladistePregled, Otprema, OtpremaLager, Otpremnica, PregledKomercijalnihNaloga … (+7) |
| RadniNalogTransport | S | InsertRadniNalogTransport | 2 | frmAutoprevozniList2, frmRadniNalogTransport |
| RadniOdnosi | S |  | 3 | frmHRRevers, frmHRZahtev, frmHRZaposleni |
| RadnoMestoNormativHTZ | S |  | 1 | frmHRSifarnici |
| RadnoMestoObavezniDokumenti | S |  | 1 | frmHRSifarnici |
| RadnoMestoObavezniPregledi | S |  | 1 | frmHRSifarnici |
| RasporedRada | S |  | 3 | frmHREvidencijaRada, frmHRSifarnici, frmHRZaposleni |
| RazloziRazduzenja | S |  | 1 | frmHRZaposleni |
| Rezervisano | S |  | 2 | Otprema, PregledLagera |
| RNCarinskoPackingLista | SIU |  | 1 | Prijemnica |
| RNCarinskoPackingListaStavka | SIU |  | 1 | Prijemnica |
| RNCarinskoPaleta | S |  | 1 | Prijemnica |
| RNCarinskoPaletaStavka | S |  | 1 | Prijemnica |
| RNCarinskoPrijemnicaStavke | SD | InsertRNCarinskoPrijemnicaStavke | 4 | Otprema, OtpremaLager, PregledLagera, Prijemnica |
| RNCarinskoSkladisteDodatneUsluge | S | DeleteRnCarinskoSkladisteDodatneUsluge, InsertRnCarinskoSkladisteDodatneUsluge | 8 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PregledLagera, Prijemnica, VanredniPoslovi, ViljuskaristiPregled |
| RNCarinskoSkladisteOtpremaStavkePom | SD | InsertRnCarinskoSkladisteOtpremaStavkePom | 4 | Otprema, OtpremaLager, Otpremnica, PregledLagera |
| RNCarinskoSkladisteOtpremnica | S | InsertRNCarinskoSkladisteOtpremnica, UpdateRNCarinskoSkladisteOtpremnica | 4 | Otprema, OtpremaLager, Otpremnica, PregledLagera |
| RNCarinskoSkladisteOtpremnicaStavke | S |  | 4 | Otprema, OtpremaLager, Otpremnica, PregledLagera |
| RNCarinskoSkladistePrijemnica | S | InsertRNCarinskoSkladistePrijemnica, UpdateRNCarinskoSkladistePrijemnica | 6 | Otprema, OtpremaLager, Otpremnica, PregledLagera, Prijemnica, ViljuskaristiPregled |
| RNCarinskoSkladisteRukovalac | S |  | 3 | Otpremnica, Prijemnica, ViljuskaristiPregled |
| RNCarinskoStavke | S |  | 1 | frmNalogPrijem |
| RNMedjuskladisni | S | DeleteRNMedjuskladisni, UpdateRNMedjuskladisni | 3 | RN12MedjuskladisniKontejnera, frmDodelaSkladista, frmPretovarPrijem |
| RNOtpremaCirade | S | DeleteRNOtpremaCirade, InsertRNOtpremaCirade, UpdateRNOtpremaCirade | 3 | frmAnalizaRadnihNaloga, frmOtpremaKontejneraIzvozKamion, frmOtpremaKontejneraUvozKamion |
| RNOtpremaPlatforme | S | DeleteRNOtpremaPlatforme, InsertRNOtpremaPlatforme, UpdateRNOtpremaPlatforme | 5 | Pregledac, RN6OtpremaPlatforme, frmAnalizaRadnihNaloga, frmDodelaSkladista, frmOtpremaKontejneraUvozKamion |
| RNOtpremaPlatforme2 | S | DeleteRNOtpremaPlatforme2, InsertRNOtpremaPlatforme2, UpdateRNOtpremaPlatforme2 | 2 | RN7OtpremaPlatforme2, frmAnalizaRadnihNaloga |
| RNOtpremaVoza | S | DeleteRNOtpremaVoza, InsertRNOtpremaVoza, UpdateRNOtpremaVoza | 3 | RN2OtpremaVoza, frmAnalizaRadnihNaloga, frmOtpremaKontejneraLegetIZVOZ |
| RNPregledIpostavkaKontejnera | S | DeleteRNPregledIpostavkaKontejnera, InsertRNPregledIpostavkaKontejnera, UpdateRNPregledIpostavkaKontejnera | 1 | frmAnalizaRadnihNaloga |
| RNPregledIspraznjenogKontejnera | S | DeleteRNPregledIspraznjenogKontejnera, InsertRNPregledIspraznjenogKontejnera, UpdateRNPregledIspraznjenogKontejnera | 1 | frmAnalizaRadnihNaloga |
| RNPrijemCirade | S | DeleteRNPrijemCirade, InsertRNPrijemCirade, UpdateRNPrijemCirade | 3 | frmAnalizaRadnihNaloga, frmPrijemKontejneraKamionLegetIzvoz, frmPrijemKontejneraKamionLegetUvoz |
| RNPrijemPlatforme | S | DeleteRNPrijemPlatforme, InsertRNPrijemPlatforme, UpdateRNPrijemPlatforme | 8 | Pregledac, RN4PrijemPlatforme, frmAnalizaRadnihNaloga, frmDodelaSkladista, frmPlatforma, frmPrijemKontejneraKamionLegetIzvoz, frmPrijemKontejneraKamionLegetUvoz, frmPrijemVozaIzPlana |
| RNPrijemPlatforme2 | S | DeleteRNPrijemPlatforme2, InsertRNPrijemPlatforme2, UpdateRNPrijemPlatforme2 | 3 | RN5PrijemPlatforme2, frmDodelaSkladista, frmPrijemKontejneraKamionLegetIzvoz |
| RNPrijemVoza | S | DeleteRNPrijemVoza, InsertRNPrijemVoza, UpdateRNPrijemVoza | 6 | RN12MedjuskladisniKontejnera, RN1PrijemVoza, frmAnalizaRadnihNaloga, frmDodelaSkladista, frmOtpremaKontejneraIzvozKamion, frmPrijemKontejneraLegetVozUvoz |
| ScenarijaTokaTransporta | S |  | 3 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmStatus |
| Scenario | S | DeleteScenario, InsertScenario, UpdateScenario | 14 | PregledKomercijalnihNaloga, Pregledac, frmDrumski1, frmFormiranjePlana, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozUnosManipulacije … (+6) |
| ScenarioTokTransporta_Statusi | S |  | 3 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmStatus |
| SERDeo | S |  | 12 | frmSERDeo, frmSERKorekcije, frmSERNabavka, frmSERNalog, frmSERNalozi, frmSERNarudzbenica, frmSERPopis, frmSERPrijem … (+4) |
| SERDeoGrupa | S |  | 11 | frmSERDeo, frmSERKorekcije, frmSERNabavka, frmSERNalog, frmSERNalozi, frmSERNarudzbenica, frmSERPopis, frmSERPrijem … (+3) |
| SERDeoKretanje | S |  | 3 | frmSERNalog, frmSERPromet, frmSERZalihe |
| SERDeoPrimenljivost | SD |  | 2 | frmSERDeo, frmSERNalog |
| SERDeoRezervacija | S |  | 1 | frmSERNalog |
| SERDokument | S |  | 1 | frmSERKorekcije |
| SerijeKola | S |  | 4 | frmDopunaPlanaPraznimIzvoz, frmFormiranjePlana, frmFormiranjePlanaIzvoz, frmVoz |
| SERKorekcija | S |  | 2 | frmSERKorekcije, frmSERMeni |
| SERNarudzbenicaStavka | S |  | 1 | frmSERNarudzbenica |
| SERObavestenje | SU |  | 2 | frmSERMeni, frmSERObavestenja |
| SERPlanNormativ | SUD |  | 1 | frmSERSifarnici |
| SERPlanStavka | S |  | 11 | frmSERDeo, frmSERKorekcije, frmSERNabavka, frmSERNalog, frmSERNalozi, frmSERNarudzbenica, frmSERPopis, frmSERPrijem … (+3) |
| SERPopisStavka | S |  | 1 | frmSERPopis |
| SERRadniNalog | S |  | 2 | frmSERNalog, frmSERNalozi |
| SERSredstvo | S |  | 11 | frmSERDeo, frmSERKorekcije, frmSERNabavka, frmSERNalog, frmSERNalozi, frmSERNarudzbenica, frmSERPopis, frmSERPrijem … (+3) |
| SERStatus | S |  | 2 | frmSERKorekcije, frmSERNabavka |
| SERVrstaSredstva | S |  | 11 | frmSERDeo, frmSERKorekcije, frmSERNabavka, frmSERNalog, frmSERNalozi, frmSERNarudzbenica, frmSERPopis, frmSERPrijem … (+3) |
| SistemskePostavke | S |  | 7 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmAutomobiliDrumski, frmDrumski, frmDrumski1, frmPregledNalogaDrumski, frmStatus |
| Sklad | S |  | 2 | Otpremnica, Prijemnica |
| Skladista | SU | DeleteSkladista, InsertSkladista, UpdateSkladista | 36 | Otprema, OtpremaLager, Otpremnica, OtpremnicaPregled, PregledLagera, Prijemnica, PrijemnicaPregled, RN12MedjuskladisniKontejnera … (+28) |
| SkladistaGrupa | S | InsertSkladistaGrupa, UpdateSkladistaGrupa | 7 | Prijemnica, SkladisteGrupa, TerminalMapFRM, frmPozicija, frmRN12InterniPrenos, frmSkladista, frmSkladisteObjekat |
| Skladiste_Korisnik | S |  | 1 | Otpremnica |
| SkladisteObjekat | S |  | 3 | frmPozicija, frmSkladista, frmSkladisteObjekat |
| Srodstva | S |  | 1 | frmHRZaposleni |
| Stanice | S |  | 12 | frmHRDan, frmHRGOPravo, frmHRGrupniUnos, frmHRRevers, frmHRSifarnici, frmHRStavka, frmHRZahtev, frmHRZaposleni … (+4) |
| StatusiStavkiNabavke | S |  | 2 | frmHRGrupniUnos, frmHRZahtev |
| StatusiZahtevaZaNabavku | S |  | 5 | frmHRGrupniUnos, frmHRRevers, frmHRZahtev, frmHRZahteviLista, frmHRZaposleni |
| StatusKapija | S | DeleteStatusKapija, InsertStatusKapija, UpdateStatusKapija | 3 | frmKamionDetalji, frmPregledKamiona, frmStatus |
| StatusRobe | S |  | 1 | frmOtpremaKontejnera |
| StatusVozila | S | DeleteStatusVozila, InsertStatusVozila, UpdateStatusVozila | 10 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmDrumski, frmDrumski1, frmIzvozPregledKontejneraDrumskeUsluge, frmNalogZaFakturisanje, frmObradaUlaznihFaktura, frmPregledKontejneraDrumskeUsluge … (+2) |
| StatusZahteva | S |  | 2 | frmHRGOZahtevi, frmHRZaposleni |
| StepenObrazovanja | S |  | 1 | frmHRZaposleni |
| Stock | S |  | 3 | Otprema, OtpremaLager, PregledLagera |
| TerminalPriv | S |  | 1 | GlavniEkran |
| TerminalPrivCMA | U |  | 1 | frmTerminalPrivremeni |
| TerminalPrivCOSCO | U |  | 1 | frmTerminalPrivremeni |
| TerminalPrivFLEXBOX | U |  | 1 | frmTerminalPrivremeni |
| TerminalPrivHAPAG | U |  | 1 | frmTerminalPrivremeni |
| TerminalPrivHMM | U |  | 1 | frmTerminalPrivremeni |
| TerminalPrivMaersk | U |  | 1 | frmTerminalPrivremeni |
| TerminalPrivMSC | U |  | 1 | frmTerminalPrivremeni |
| TerminalPrivOOCL | U |  | 1 | frmTerminalPrivremeni |
| TerminalRadniNalozi | S |  | 1 | frmAnalizaRadnihNaloga |
| TezinePovreda | S |  | 1 | frmHRZaposleni |
| TipKomercijalnogDokumenta | S |  | 2 | UvozDokumenta, frmIzvozDokumenta |
| TipKontenjera | S | DeleteTipKontenjera, InsertTipKontenjera, UpdateTipKontenjera | 58 | PakovanjeKamiona1, PakovanjeKamionaCerade, PregledKomercijalnihNaloga, Pregledac, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme … (+50) |
| TipPalete | S | DeleteTipPalete, InsertTipPalete, UpdateTipPalete | 9 | Otprema, OtpremaLager, Otpremnica, PregledLagera, Prijemnica, ViljuskaristiPregled, frmNalogPrijem, frmSkladista … (+1) |
| TipProtokola | S |  | 3 | frmListaProtokola, frmNalogZaFakturisanje, frmObradaUlaznihFaktura |
| TipRobe | S |  | 1 | Prijemnica |
| TipSkladista | S |  | 3 | SkladisteGrupa, frmSkladista, frmSkladisteObjekat |
| TipZone | S |  | 5 | SkladisteGrupa, frmPozicija, frmSkladista, frmSkladisteObjekat, frmZone |
| TovarniList | S |  | 1 | frmTovarniList |
| UploadTokens | S |  | 3 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmStatus |
| uvKvalitetKontejnera | S |  | 12 | TerminalMapFRM, frmCIR, frmDefinisiPoziciju, frmDrumski1, frmGrupniUnosPoljaIzvoz, frmPopisKonterjnera, frmPrijemKamionaDetalji, frmProdajniNalogIzvoz … (+4) |
| uvNacinPakovanja | S |  | 20 | frmDopunaPlanaPraznimIzvoz, frmDrumski1, frmFormiranjePlana, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKonacnaTable, frmIzvozUnosManipulacije … (+12) |
| Uvoz | SIU | DeleteUvoz, UpdateUvoz | 16 | PakovanjeKamiona1, PakovanjeKamionaCerade, UvozDokumenta, frmDrumski, frmDrumski1, frmFormiranjePlana, frmNalogZaFakturisanje, frmObradaUlaznihFaktura … (+8) |
| UvozDokumenta | S | DeleteUvozDokumenta, InsertUvozDokumenta | 1 | UvozDokumenta |
| UvozDokumentaCeoVoz | S | DeleteUvozDokumentaCeoVoz, InsertUvozDokumentaCeoVoz | 1 | UvozDokumenta |
| UvozDokumentaUsluge | S | DeleteUvozDokumentaUsluge, InsertUvozDokumentaUsluge | 1 | UvozDokumenta |
| UvozKonacna | SU | DeleteUvozKonacna, InsertUvozKonacna, UpdateUvozKonacna | 32 | PakovanjeKamiona1, PakovanjeKamionaCerade, PregledKomercijalnihNaloga, UvozDokumenta, frmAnalizaRadnihNaloga, frmDodatneUsluge, frmDrumski, frmDrumski1 … (+24) |
| UvozKonacnaNapomenePozicioniranja | S | DeleteUvozKonacnaNapomenePozicioniranja, InsertUvozKonacnaNapomenePozicioniranja | 6 | PregledKomercijalnihNaloga, frmFormiranjePlanaIzvoz, frmOtpremaKontejneraUvozKamion, frmPrijemKontejneraLegetVozUvoz, frmRadniNalogInterniPregled, frmUvozKonacna |
| UvozKonacnaNHM | S | DeleteUvozKonacnaNHM, InsertUvozKonacnaNHM | 9 | Prijemnica, RN1PrijemVoza, RN6OtpremaPlatforme, UvozNHM, frmOtpremaKontejneraUvozKamion, frmPrijemKontejneraKamionLegetUvoz, frmPrijemKontejneraLegetVozUvoz, frmRadniNalogInterniPregled … (+1) |
| UvozKonacnaVrstaManipulacije | S | DeleteUvozKonacnaVrstaManipulacije, InsertUvozKonacnaVrstaManipulacije | 12 | PregledKomercijalnihNaloga, UvozDokumenta, frmFormiranjePlanaIzvoz, frmIzvozKonacna, frmIzvozUnosManipulacije, frmOtpremaKontejneraUvozKamion, frmPregledKontejneraDrumskeUsluge, frmPrijemKontejneraKamionLegetUvoz … (+4) |
| UvozKonacnaVrstaRobeHS | S | DeleteUvozKonacnaVrstaRobeHS | 1 | frmUvozKonacna |
| UvozKonacnaZaglavlje | S | DeleteUvozKonacnaZaglavlje, InsertUvozKonacnaZaglavlje, UpdateUvozKonacnaZaglavlje | 14 | PregledKomercijalnihNaloga, frmDrumski, frmDrumski1, frmFormiranjePlana, frmFormiranjePlanaIzvoz, frmOtpremaKontejneraUvozKamion, frmPregledNalogaDrumski, frmPregledVozova … (+6) |
| UvozNapomenePozicioniranja | S | DeleteUvozNapomenePozicioniranja, InsertUvozNapomenePozicioniranja | 4 | frmFormiranjePlana, frmRadniNalogInterniPregled, frmUvozKonacna, frmUvozKonacnaTable |
| UvozNHM | S | DeleteUvozNHM, InsertUvozNHM | 2 | UvozNHM, frmUvozKonacna |
| UvozUvoznici | S | DeleteUvozUvoznici, InsertUvozUvoznici | 2 | frmUvozKonacna, frmUvozUvoznici |
| UvozVrstaManipulacije | SU | DeleteUvozVrstaManipulacije, InsertUvozVrstaManipulacije | 4 | UvozDokumenta, frmIzvozUnosManipulacije, frmPregledKontejneraDrumskeUsluge, frmUnosManipulacija |
| UvozVrstaRobeHS | S | DeleteUvozVrstaRobeHS, InsertUvozVrstaRobeHS | 1 | frmUvozKonacna |
| uvuvozkonacnaoz | U |  | 1 | frmUvozKonacnaTable |
| Vaganje | S | DeleteVaganje, InsertVaganje, UpdateVaganje | 2 | Vaganje, VaganjePregled |
| Valute | S | DeleteValute | 15 | Prijemnica, frmDrumski, frmDrumski1, frmHRDan, frmHRGOPravo, frmHRRevers, frmHRSifarnici, frmHRStavka … (+7) |
| VlasniciRobe | S |  | 2 | frmMestoIsporuke, frmNalogPrijem |
| Voz | S | DeleteVoz, InsertVoz, UpdateVoz | 36 | PregledKomercijalnihNaloga, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme, RN7OtpremaPlatforme2, frmAnalizaRadnihNaloga … (+28) |
| Vozila | S | UpdateVozila | 4 | Otpremnica, frmAutoprevozniList2, frmPutniNalog, frmRadniNalogTransport |
| VoziloUsluga | S | DeleteVoziloUsluga, InsertVoziloUsluga, UpdateVoziloUsluga | 4 | frmOtpremaKontejneraKamionomIzKontejnera, frmPrijemKontejneraKamionLegetUvoz, frmPrijemVozaIzPlana, frmVoziloUsluga |
| VozSerijeKola | S | DeleteVozSerijeKola, InsertVozSerijeKola | 4 | frmDopunaPlanaPraznimIzvoz, frmFormiranjePlana, frmFormiranjePlanaIzvoz, frmVoz |
| VrstaCarinskogPostupka | S |  | 33 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PregledLagera, Prijemnica, RN4PrijemPlatforme, RN6OtpremaPlatforme … (+25) |
| VrstaManipulacije | S |  | 46 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PakovanjeKamiona1, PakovanjeKamionaCerade, PregledKomercijalnihNaloga, PregledLagera … (+38) |
| VrstaRada | S |  | 4 | frmHRDan, frmHREvidencijaRada, frmHRGrupniUnos, frmHRSifarnici |
| VrstaRobe | S |  | 9 | frmLagerOperater, frmNalogZaPrevoz, frmOtpremaKontejnera, frmOtpremaKontejneraIzvozKamion, frmOtpremaKontejneraLegetIZVOZ, frmOtpremaKontejneraUvozKamion, frmOtpremaVozaIzPlana, frmPovezivanjeKontejneraIVagona … (+1) |
| VrstaRobeADR | S |  | 20 | frmDrumski1, frmFormaZaVozace, frmFormiranjePlana, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmNalogPrijem, frmNalogZaPrevoz, frmOtpremaKontejneraIzvozKamion … (+12) |
| VrstaRobeHS | S |  | 7 | frmDopunaPlanaPraznimIzvoz, frmFormiranjePlanaIzvoz, frmIzvozKonacna, frmIzvozKonacnaTable, frmIzvozUnosManipulacije, frmUnosManipulacija, frmUvozKonacna |
| VrstaVozila | S |  | 21 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PakovanjeKamiona1, PakovanjeKamionaCerade, PregledLagera, Prijemnica … (+13) |
| VrsteDisciplinskihMera | S |  | 1 | frmHRZaposleni |
| VrsteDokumenta | S |  | 4 | frmHRRevers, frmHRSifarnici, frmHRZahtev, frmHRZaposleni |
| VrsteNabavke | S |  | 5 | frmHRGrupniUnos, frmHRRevers, frmHRZahtev, frmHRZahteviLista, frmHRZaposleni |
| VrstePostupakaUvoz | S |  | 21 | frmDopunaPlanaPraznimIzvoz, frmDrumski, frmDrumski1, frmFormiranjePlana, frmFormiranjePlanaIzvoz, frmIzvozKonacna, frmIzvozKonacnaTable, frmIzvozUnosManipulacije … (+13) |
| VrstePregleda | S |  | 2 | frmHRSifarnici, frmHRZaposleni |
| VrstePrevoznogSredstva | S |  | 8 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PregledLagera, Prijemnica, VanredniPoslovi, frmNalogPrijem |
| VrsteUgovora | S |  | 1 | frmHRZaposleni |
| vSERDeoStanje | S |  | 3 | frmSERDeo, frmSERNalog, frmSERZalihe |
| vSERKarticaDela | S |  | 1 | frmSERDeo |
| vSERNarudzbenica | S |  | 4 | frmSERDeo, frmSERNabavka, frmSERNarudzbenica, frmSERPrijem |
| vSERNarudzbenicaStavka | S |  | 3 | frmSERDeo, frmSERNarudzbenica, frmSERPrijem |
| vSERPopis | S |  | 1 | frmSERPopis |
| vSERPredlogNabavke | S |  | 2 | frmSERMeni, frmSERNabavka |
| vSERPromet | S |  | 1 | frmSERPromet |
| vSERRadniNalogDelovi | S |  | 1 | frmSERNalog |
| vSERRadniNalogTrosak | S |  | 2 | frmSERNalog, frmSERNalozi |
| vw_EvidencijaZaObracun | S |  | 1 | frmHREvidencijaRada |
| vZaposleniRokovi | S |  | 1 | frmHRZaposleni |
| vZaposleniTrenutno | S |  | 10 | frmHREvidencijaRada, frmHRGOPravo, frmHRGOZahtevi, frmHRGrupniUnos, frmHRKontrolnaTabla, frmHRRevers, frmHRZahtev, frmHRZahteviLista … (+2) |
| vZaposleniUpozorenja | S |  | 2 | frmHRZaposleni, frmHRZaposleniLista |
| ZahteviZaNabavku | S |  | 5 | frmHRGrupniUnos, frmHRRevers, frmHRZahtev, frmHRZahteviLista, frmHRZaposleni |
| ZahtevZaNabavkuIstorijaStatusa | S |  | 2 | frmHRGrupniUnos, frmHRZahtev |
| ZahtevZaNabavkuOdobrenja | S |  | 5 | frmHRGrupniUnos, frmHRRevers, frmHRZahtev, frmHRZahteviLista, frmHRZaposleni |
| ZahtevZaNabavkuPonude | SD |  | 4 | frmHRGrupniUnos, frmHRRevers, frmHRZahtev, frmHRZaposleni |
| ZahtevZaNabavkuPrilozi | SD |  | 2 | frmHRGrupniUnos, frmHRZahtev |
| ZahtevZaNabavkuStavke | SUD |  | 5 | frmHRGrupniUnos, frmHRRevers, frmHRZahtev, frmHRZahteviLista, frmHRZaposleni |
| ZapisnikONepravilnosti | S | DeleteZapisnikONepravilnosti, InsertZapisnikONepravilnosti | 1 | ZapisnikONepravilnosti |
| ZapisnikOOStecenju | S | DeleteZapisnikOOstecenju, InsertZapisnikOOstecenju | 1 | ZapisnikOOstecenjuRobe |
| Zaposleni | SIU |  | 17 | frmAutoprevozniList2, frmHRDan, frmHREvidencijaRada, frmHRGOPravo, frmHRGOZahtevi, frmHRGrupniUnos, frmHRKontrolnaTabla, frmHRMeni … (+9) |
| ZaposleniClanoviPorodice | S |  | 1 | frmHRZaposleni |
| ZaposleniDisciplinske | S |  | 1 | frmHRZaposleni |
| ZaposleniDokumenti | S |  | 3 | frmHRRevers, frmHRZahtev, frmHRZaposleni |
| ZaposleniHitniKontakti | S |  | 1 | frmHRZaposleni |
| ZaposleniHTZZaduzenja | S |  | 3 | frmHRRevers, frmHRZahtev, frmHRZaposleni |
| ZaposleniMentorBeleske | S |  | 1 | frmHRZaposleni |
| ZaposleniMentori | S |  | 1 | frmHRZaposleni |
| ZaposleniOcene | S |  | 1 | frmHRZaposleni |
| ZaposleniOsnovGO | S |  | 1 | frmHRZaposleni |
| ZaposleniPovrede | S |  | 1 | frmHRZaposleni |
| ZaposleniPrilozi | SD |  | 1 | frmHRZaposleni |
| ZaposleniRadnaMesta | S |  | 3 | frmHRRevers, frmHRZahtev, frmHRZaposleni |
| ZaposleniSkolovanje | S |  | 1 | frmHRZaposleni |
| ZaposleniSlike | SD |  | 1 | frmHRZaposleni |
| ZaposleniStatusi | S |  | 1 | frmHRZaposleni |
| ZaposleniUgovori | S |  | 1 | frmHRZaposleni |
| ZaposleniZdravstveniPregledi | S |  | 1 | frmHRZaposleni |
| Zavrseno | S |  | 3 | Otprema, OtpremaLager, PregledLagera |
| Zone | S | DeleteZone, InsertZone, UpdateZone | 5 | SkladisteGrupa, TerminalMapFRM, frmRN12InterniPrenos, frmSkladisteObjekat, frmZone |

### 4.1 Tabele u koje se upisuje (I/U/D)

| Tabela | Operacije | Forme koje pišu |
|---|---|---|
| Artikli | U | Prijemnica |
| ArtikliKatalogUvoz | I | Prijemnica |
| GODopustStavke | U | frmHRGOZahtevi |
| HTZReversi | U | frmHRRevers |
| Izvoz | IU | frmFormiranjePlanaIzvoz, frmIzvozUnosManipulacije |
| IzvozKonacna | U | frmFormiranjePlanaIzvoz, frmIzvozKonacnaTable, frmIzvozUnosManipulacije |
| IzvozVrstaManipulacije | U | frmIzvozUnosManipulacije |
| mainNoviKartice | ID | AdministracijaPravoPristupa |
| MainNoviPrava | ID | AdministracijaPravoPristupa |
| Partnerji | U | frmPartnerji |
| Podesavanja | U | frmHREvidencijaRada, frmHRGrupniUnos, frmPodesavanja |
| RNCarinskoPackingLista | IU | Prijemnica |
| RNCarinskoPackingListaStavka | IU | Prijemnica |
| RNCarinskoPrijemnicaStavke | D | Prijemnica |
| RNCarinskoSkladisteOtpremaStavkePom | D | Otprema, PregledLagera |
| SERDeoPrimenljivost | D | frmSERDeo |
| SERObavestenje | U | frmSERObavestenja |
| SERPlanNormativ | UD | frmSERSifarnici |
| Skladista | U | TerminalMapFRM |
| TerminalPrivCMA | U | frmTerminalPrivremeni |
| TerminalPrivCOSCO | U | frmTerminalPrivremeni |
| TerminalPrivFLEXBOX | U | frmTerminalPrivremeni |
| TerminalPrivHAPAG | U | frmTerminalPrivremeni |
| TerminalPrivHMM | U | frmTerminalPrivremeni |
| TerminalPrivMaersk | U | frmTerminalPrivremeni |
| TerminalPrivMSC | U | frmTerminalPrivremeni |
| TerminalPrivOOCL | U | frmTerminalPrivremeni |
| Uvoz | IU | frmPrijemKontejneraKamionLegetUvoz, frmUvozKonacna, frmUvozKonacnaTable |
| UvozKonacna | U | frmUvozKonacnaTable, frmVoz |
| UvozVrstaManipulacije | U | frmUnosManipulacija |
| uvuvozkonacnaoz | U | frmUvozKonacnaTable |
| ZahtevZaNabavkuPonude | D | frmHRGrupniUnos, frmHRZahtev |
| ZahtevZaNabavkuPrilozi | D | frmHRGrupniUnos, frmHRZahtev |
| ZahtevZaNabavkuStavke | UD | frmHRGrupniUnos, frmHRZahtev |
| Zaposleni | IU | frmHRZaposleni |
| ZaposleniPrilozi | D | frmHRZaposleni |
| ZaposleniSlike | D | frmHRZaposleni |

## 5. Stored procedure

Vrsta je izvedena iz prefiksa imena procedure (Insert/Update/Delete/Select), sadržaj procedura nije analiziran.

| Procedura | Vrsta (po imenu) | Br. formi | Forme |
|---|---|---|---|
| AktivirajPozicija | ostalo | 1 | frmPozicija |
| AktivirajSkladista | ostalo | 9 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PregledKomercijalnihNaloga, Prijemnica, VanredniPoslovi, frmNalogPrijem … (+1) |
| AktivirajSkladistaGrupa | ostalo | 1 | SkladisteGrupa |
| AktivirajZonaPozicije | ostalo | 1 | frmZone |
| ArhiviranRadniNalogDrumski | ostalo | 9 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmDrumski, frmDrumski1, frmFormaZaVozace, frmObradaUlaznihFaktura, frmPregledNalogaDrumski, frmPregledPorukeVozacu … (+1) |
| DeAktivirajPozicija | ostalo | 1 | frmPozicija |
| DeAktivirajSkladista | ostalo | 9 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PregledKomercijalnihNaloga, Prijemnica, VanredniPoslovi, frmNalogPrijem … (+1) |
| DeAktivirajSkladistaGrupa | ostalo | 1 | SkladisteGrupa |
| DeAktivirajZonaPozicije | ostalo | 1 | frmZone |
| DeleteAutomobili | brisanje | 3 | frmAutomobilEdit, frmAutomobiliDrumski, frmPodesavanjeRaspolozivosti |
| DeleteAutomobiliServis | brisanje | 3 | frmAutomobilEdit, frmAutomobiliDrumski, frmPodesavanjeRaspolozivosti |
| DeleteAutomobiliTehnickiProblem | brisanje | 3 | frmAutomobilEdit, frmAutomobiliDrumski, frmPodesavanjeRaspolozivosti |
| DeleteAutoprevozniList | brisanje | 1 | frmAutoprevozniList2 |
| DeleteAutoprevozniListGeneralni | brisanje | 1 | frmAutoprevozniList2 |
| DeleteAutoprevozniListKameni | brisanje | 1 | frmAutoprevozniList2 |
| DeleteCarinskoStavke | brisanje | 9 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PregledKomercijalnihNaloga, Prijemnica, VanredniPoslovi, frmNalogPrijem … (+1) |
| DeleteDefinisiPoziciju | brisanje | 1 | frmDefinisiPoziciju |
| DeleteDogovorenoIzvoz | brisanje | 1 | DogovoreniPosloviIzvoza |
| DeleteDokumentaOtpremaKontejneraVoz | brisanje | 2 | frmDokumentaOtpremaKontejnera, frmNajavaKomitemtaDokument |
| DeleteDokumentFaktureDrumski | brisanje | 5 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmNalogZaFakturisanje, frmObradaUlaznihFaktura, frmSeniranjeDokumenata |
| DeleteDokumentKamionaDrumski | brisanje | 5 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmNalogZaFakturisanje, frmObradaUlaznihFaktura, frmSeniranjeDokumenata |
| DeleteDokumentVozaca | brisanje | 5 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmNalogZaFakturisanje, frmObradaUlaznihFaktura, frmSeniranjeDokumenata |
| DeleteFaktura | brisanje | 1 | frmPartnerji |
| DeleteFakturaDrumskiStavka | brisanje | 5 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmNalogZaFakturisanje, frmObradaUlaznihFaktura, frmSeniranjeDokumenata |
| DeleteFakturaPostav | brisanje | 1 | frmPartnerji |
| DeleteIzvozDokumenta | brisanje | 1 | frmIzvozDokumenta |
| DeleteIzvozKonacnaNapomenePozicioniranja | brisanje | 8 | frmDopunaPlanaPraznimIzvoz, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmOtpremaKontejneraKamionomIzKontejnera, frmOtpremaVozaIzPlana, frmPovezivanjeKontejneraIVagona, frmRadniNalogInterniPregled |
| DeleteIzvozKonacnaNHM | brisanje | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| DeleteIzvozKonacnaSve | brisanje | 8 | frmDopunaPlanaPraznimIzvoz, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmOtpremaKontejneraKamionomIzKontejnera, frmOtpremaVozaIzPlana, frmPovezivanjeKontejneraIVagona, frmRadniNalogInterniPregled |
| DeleteIzvozKonacnaVrstaManipulacije | brisanje | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| DeleteIzvozKonacnaVrstaRobeHS | brisanje | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| DeleteIzvozKonacnaZaglavlje | brisanje | 2 | frmIzvozKonacna, frmVoz |
| DeleteIzvozNapomenePozicioniranja | brisanje | 8 | frmDopunaPlanaPraznimIzvoz, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmOtpremaKontejneraKamionomIzKontejnera, frmOtpremaVozaIzPlana, frmPovezivanjeKontejneraIVagona, frmRadniNalogInterniPregled |
| DeleteIzvozNHM | brisanje | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| DeleteIzvozSve | brisanje | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| DeleteIzvozUslugaDokumenta | brisanje | 8 | frmDopunaPlanaPraznimIzvoz, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmOtpremaKontejneraKamionomIzKontejnera, frmOtpremaVozaIzPlana, frmPovezivanjeKontejneraIVagona, frmRadniNalogInterniPregled |
| DeleteIzvozVrstaManipulacije | brisanje | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| DeleteIzvozVrstaManTerminalskih | brisanje | 8 | frmDopunaPlanaPraznimIzvoz, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmOtpremaKontejneraKamionomIzKontejnera, frmOtpremaVozaIzPlana, frmPovezivanjeKontejneraIVagona, frmRadniNalogInterniPregled |
| DeleteIzvozVrstaRobeHS | brisanje | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| DeleteKapija | brisanje | 3 | frmKamionDetalji, frmKapijaOtvoreniNalozi, frmPregledKamiona |
| DeleteKontejnerIzvoz | brisanje | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| DeleteKorisnici | brisanje | 1 | frmKorisnici |
| DeleteMagacinskiBrojCarinski | brisanje | 11 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PregledKomercijalnihNaloga, Prijemnica, VanredniPoslovi, frmMestoIsporuke … (+3) |
| DeleteMestaUtovara | brisanje | 2 | frmDrumski, frmDrumski1 |
| DeleteNajavaDokumenta | brisanje | 1 | frmFotografijePregledac |
| DeleteNajavaPrevozaTroskovi | brisanje | 1 | frmNalogZaPrevoz |
| DeleteNalogZaPrevoz | brisanje | 1 | frmNalogZaPrevoz |
| DeleteNaruceneManipulacije | brisanje | 2 | frmManipulacije, frmPregledNarucenihManipulacija |
| DeleteNosiociTroska | brisanje | 1 | frmPartnerji |
| DeleteOpportunity | brisanje | 1 | frmPartnerji |
| DeleteOtpremaKontejnera | brisanje | 3 | frmOtpremaKontejnera, frmOtpremaKontejneraIzvozKamion, frmOtpremaKontejneraLegetIZVOZ |
| DeleteOtpremaKontejneraVozStavke | brisanje | 4 | frmOtpremaKontejnera, frmOtpremaKontejneraIzvozKamion, frmOtpremaKontejneraLegetIZVOZ, frmPovezivanjeKontejneraIVagona |
| DeletePartnerji | brisanje | 2 | frmDrumski, frmPartnerji |
| DeletePopisKontejnera | brisanje | 1 | frmPopisKonterjnera |
| DeletePopisKontejneraStavke | brisanje | 1 | frmPopisKonterjnera |
| DeletePostupakStavke | brisanje | 1 | frmOtpremaKontejnera |
| DeletePostupakStavkeOtprema | brisanje | 1 | frmOtpremaKontejnera |
| DeletePozicija | brisanje | 1 | frmPozicija |
| DeletePozicioniranje | brisanje | 2 | frmDrumski, frmDrumski1 |
| DeletePravo | brisanje | 1 | frmKorisnici |
| DeletePredvidjanje | brisanje | 1 | frmPartnerji |
| DeletePrijemKontejneraVoz | brisanje | 1 | frmOtpremaKontejnera |
| DeletePrijemKontejneraVozStavke | brisanje | 1 | frmOtpremaKontejnera |
| DeleteProdajniNalogIzvoz | brisanje | 4 | frmPregledNalogaDrumski, frmPregledPorudzbenica, frmProdajniNalogIzvoz, frmProdajniNalogIzvozTabela |
| DeletePromet | brisanje | 4 | Otpremnica, Prijemnica, frmPrijemKontejneraKamionLegetUvoz, frmPrijemKontejneraLegetVozUvoz |
| DeletePrometManipulacije | brisanje | 2 | frmPrijemKontejneraKamionLegetUvoz, frmPrijemKontejneraLegetVozUvoz |
| DeleteProtokolRadniNalogDrumski | brisanje | 2 | frmListaProtokola, frmProtokolTransportnogNaloga |
| DeletePutniNalog | brisanje | 1 | frmPutniNalog |
| DeletePutniNalogGorivo | brisanje | 1 | frmPutniNalog |
| DeletePutniNalogGume | brisanje | 1 | frmPutniNalog |
| DeletePutniNalogKvarovi | brisanje | 1 | frmPutniNalog |
| DeletePutniNalogRuta | brisanje | 1 | frmPutniNalog |
| DeletePutniNalogServis | brisanje | 1 | frmPutniNalog |
| DeletePutniNalogTroskovi | brisanje | 1 | frmPutniNalog |
| DeletePutniNalogZaposleni | brisanje | 1 | frmPutniNalog |
| DeleteRadniNalogDrumski | brisanje | 9 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmDrumski, frmDrumski1, frmFormaZaVozace, frmObradaUlaznihFaktura, frmPregledNalogaDrumski, frmPregledPorukeVozacu … (+1) |
| DeleteRadniNalogInterni | brisanje | 14 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmCerada, frmDrumski, frmDrumski1, frmKapijaOtvoreniNalozi, frmPlatforma, frmPlatformaOtprema … (+6) |
| DeleteRadniNalogSkladisteDirektni | brisanje | 3 | frmMestoIsporuke, frmNalogPrijem, frmVozac |
| DeleteRadniNalogTransportStavke | brisanje | 1 | frmRadniNalogTransport |
| DeleteRadniNalogZaTransport | brisanje | 1 | frmRadniNalogTransport |
| DeleteRnCarinskoSkladisteDodatneUsluge | brisanje | 11 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PregledKomercijalnihNaloga, Prijemnica, VanredniPoslovi, frmMestoIsporuke … (+3) |
| DeleteRNMedjuskladisni | brisanje | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| DeleteRNOtpremaCirade | brisanje | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| DeleteRNOtpremaPlatforme | brisanje | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| DeleteRNOtpremaPlatforme2 | brisanje | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| DeleteRNOtpremaVoza | brisanje | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| DeleteRNPregledIpostavkaKontejnera | brisanje | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| DeleteRNPregledIspraznjenogKontejnera | brisanje | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| DeleteRNPrijemCirade | brisanje | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| DeleteRNPrijemPlatforme | brisanje | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| DeleteRNPrijemPlatforme2 | brisanje | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| DeleteRNPrijemVoza | brisanje | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| DeleteRNPrijemVoza2 | brisanje | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| DeleteScenario | brisanje | 3 | frmIzvozUnosManipulacije, frmUnosManipulacija, frmUvozKonacna |
| DeleteSkladista | brisanje | 9 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PregledKomercijalnihNaloga, Prijemnica, VanredniPoslovi, frmNalogPrijem … (+1) |
| DeleteSkladisteGrupa | brisanje | 1 | SkladisteGrupa |
| DeleteStatusKapija | brisanje | 1 | frmStatus |
| DeleteStatusVozila | brisanje | 1 | frmStatus |
| DeleteTipKontenjera | brisanje | 1 | frmTipKontejnera |
| DeleteTipPalete | brisanje | 1 | frmTipPalete |
| DeleteUlFak | brisanje | 1 | frmPartnerji |
| DeleteUlFakPostav | brisanje | 1 | frmPartnerji |
| DeleteUvoz | brisanje | 5 | frmIzvozPregledKontejneraDrumskeUsluge, frmNalogPrijem, frmPregledKontejneraDrumskeUsluge, frmUnosManipulacija, frmUvozKopirajUslugeKontejnera |
| DeleteUvozDokumenta | brisanje | 1 | UvozDokumenta |
| DeleteUvozDokumentaCeoVoz | brisanje | 1 | UvozDokumenta |
| DeleteUvozDokumentaUsluge | brisanje | 1 | UvozDokumenta |
| DeleteUvozKonacna | brisanje | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| DeleteUvozKonacnaNapomenePozicioniranja | brisanje | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| DeleteUvozKonacnaNHM | brisanje | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| DeleteUvozkonacnaNHMDiana | brisanje | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| DeleteUvozKonacnaVrstaManipulacije | brisanje | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| DeleteUvozKonacnaVrstaManipulacijeKamionska | brisanje | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| DeleteUvozKonacnaVrstaRobeHS | brisanje | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| DeleteUvozKonacnaZaglavlje | brisanje | 1 | frmUvozKonacna |
| DeleteUvozNapomenePozicioniranja | brisanje | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| DeleteUvozNHM | brisanje | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| DeleteUvozNHMDiana | brisanje | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| DeleteUvozTerminalskeManipulacije | brisanje | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| DeleteUvozTerminalskeManipulacijeKonacna | brisanje | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| DeleteUvozUslugaDokumenta | brisanje | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| DeleteUvozUvoznici | brisanje | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| DeleteUvozVrstaManipulacije | brisanje | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| DeleteUvozVrstaManipulacijeKamionska | brisanje | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| DeleteUvozVrstaManTerminalskih | brisanje | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| DeleteUvozVrstaRobeHS | brisanje | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| DeleteVaganje | brisanje | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| DeleteValute | brisanje | 4 | frmPregledNalogaDrumski, frmPregledPorudzbenica, frmProdajniNalogIzvoz, frmProdajniNalogIzvozTabela |
| DeleteVoz | brisanje | 2 | frmPregledVozova, frmVoz |
| DeleteVoziloUsluga | brisanje | 1 | frmVoziloUsluga |
| DeleteVozSerijeKola | brisanje | 2 | frmPregledVozova, frmVoz |
| DeleteZapisnikONepravilnosti | brisanje | 1 | ZapisnikONepravilnosti |
| DeleteZapisnikOOstecenju | brisanje | 1 | ZapisnikOOstecenjuRobe |
| DeleteZone | brisanje | 1 | frmZone |
| DelRadniNalogInterniSaDokumentima | brisanje | 14 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmCerada, frmDrumski, frmDrumski1, frmKapijaOtvoreniNalozi, frmPlatforma, frmPlatformaOtprema … (+6) |
| delTerminalPriv | brisanje | 3 | frmTerminalPrivremeni, frmTerminalPrivremeniExcel, frmTerminalPrivremeniSlike |
| DodajStavkeURadniNalog | ostalo | 5 | frmIzvozPregledKontejneraDrumskeUsluge, frmNalogPrijem, frmPregledKontejneraDrumskeUsluge, frmUnosManipulacija, frmUvozKopirajUslugeKontejnera |
| DodajStavkeURadniNalogDrumski | ostalo | 9 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmDrumski, frmDrumski1, frmFormaZaVozace, frmObradaUlaznihFaktura, frmPregledNalogaDrumski, frmPregledPorukeVozacu … (+1) |
| DodeliKamionZaRadniNalogDrumski | ostalo | 9 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmDrumski, frmDrumski1, frmFormaZaVozace, frmObradaUlaznihFaktura, frmPregledNalogaDrumski, frmPregledPorukeVozacu … (+1) |
| DodeliKamionZaRadniNalogDrumskiP | ostalo | 9 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmDrumski, frmDrumski1, frmFormaZaVozace, frmObradaUlaznihFaktura, frmPregledNalogaDrumski, frmPregledPorukeVozacu … (+1) |
| DodeliKamionZaViseRadnihNalogaDrumskiP | ostalo | 9 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmDrumski, frmDrumski1, frmFormaZaVozace, frmObradaUlaznihFaktura, frmPregledNalogaDrumski, frmPregledPorukeVozacu … (+1) |
| DokumentaPrijemKontejneraKamion | ostalo | 1 | frmDokumentaPrijemKontejneraKamion |
| DozvoliIzlaz | ostalo | 14 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmCerada, frmDrumski, frmDrumski1, frmKapijaOtvoreniNalozi, frmPlatforma, frmPlatformaOtprema … (+6) |
| DuplirajZapisRadniNalogDrumski | ostalo | 9 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmDrumski, frmDrumski1, frmFormaZaVozace, frmObradaUlaznihFaktura, frmPregledNalogaDrumski, frmPregledPorukeVozacu … (+1) |
| HR_ZahtevPreracunaj | ostalo | 2 | frmHRGrupniUnos, frmHRZahtev |
| InsertApiLogovi | upis | 1 | frmPartnerji |
| InsertAutomobili | upis | 3 | frmAutomobilEdit, frmAutomobiliDrumski, frmPodesavanjeRaspolozivosti |
| InsertAutomobiliDrumski | upis | 3 | frmAutomobilEdit, frmAutomobiliDrumski, frmPodesavanjeRaspolozivosti |
| InsertAutomobiliServis | upis | 3 | frmAutomobilEdit, frmAutomobiliDrumski, frmPodesavanjeRaspolozivosti |
| InsertAutomobiliTehnickiProblem | upis | 3 | frmAutomobilEdit, frmAutomobiliDrumski, frmPodesavanjeRaspolozivosti |
| InsertAutoPrevozniList | upis | 1 | frmAutoprevozniList2 |
| InsertAutoPrevozniListGeneralni | upis | 1 | frmAutoprevozniList2 |
| InsertAutoPrevozniListKameni | upis | 1 | frmAutoprevozniList2 |
| InsertCarinskoStavke | upis | 9 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PregledKomercijalnihNaloga, Prijemnica, VanredniPoslovi, frmNalogPrijem … (+1) |
| InsertDobavnica | upis | 2 | Otpremnica, Prijemnica |
| InsertDobavnicaPostav | upis | 2 | Otpremnica, Prijemnica |
| InsertDogovorenoIzvoz | upis | 1 | DogovoreniPosloviIzvoza |
| InsertDokumentaFaktureDrumski | upis | 5 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmNalogZaFakturisanje, frmObradaUlaznihFaktura, frmSeniranjeDokumenata |
| InsertDokumentaNajavaKomitenta | upis | 1 | frmNajavaKomitemtaDokument |
| InsertDokumentaOtpremaKontejneraVoz | upis | 1 | frmDokumentaOtpremaKontejnera |
| InsertDokumentaPrijemKontejneraKamion | upis | 1 | frmDokumentaPrijemKontejneraKamion |
| InsertDokumentaRadniNalogDrumski | upis | 9 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmDrumski, frmDrumski1, frmFormaZaVozace, frmObradaUlaznihFaktura, frmPregledNalogaDrumski, frmPregledPorukeVozacu … (+1) |
| InsertDokumentaSkladiste | upis | 5 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmNalogZaFakturisanje, frmObradaUlaznihFaktura, frmSeniranjeDokumenata |
| InsertDokumentaUploadedFiles | upis | 5 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmNalogZaFakturisanje, frmObradaUlaznihFaktura, frmSeniranjeDokumenata |
| InsertDrzave | upis | 2 | frmDrumski, frmPartnerji |
| InsertFaktura | upis | 1 | frmPartnerji |
| InsertFakturaDrumski | upis | 5 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmNalogZaFakturisanje, frmObradaUlaznihFaktura, frmSeniranjeDokumenata |
| InsertFakturaDrumskiStavka | upis | 5 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmNalogZaFakturisanje, frmObradaUlaznihFaktura, frmSeniranjeDokumenata |
| InsertFakturaPostav | upis | 1 | frmPartnerji |
| InsertIzvozDokumenta | upis | 1 | frmIzvozDokumenta |
| InsertIzvozKonacnaNapomenePozicioniranja | upis | 8 | frmDopunaPlanaPraznimIzvoz, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmOtpremaKontejneraKamionomIzKontejnera, frmOtpremaVozaIzPlana, frmPovezivanjeKontejneraIVagona, frmRadniNalogInterniPregled |
| InsertIzvozKonacnaNHM | upis | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| InsertIzvozKonacnaVMIzUvoza | upis | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| InsertIzvozKonacnaVrstaManipulacije | upis | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| InsertIzvozkonacnaVrstaRobeHS | upis | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| InsertIzvozKonacnaZaglavlje | upis | 2 | frmIzvozKonacna, frmVoz |
| InsertIzvozNapomenePozicioniranja | upis | 8 | frmDopunaPlanaPraznimIzvoz, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmOtpremaKontejneraKamionomIzKontejnera, frmOtpremaVozaIzPlana, frmPovezivanjeKontejneraIVagona, frmRadniNalogInterniPregled |
| InsertIzvozNHM | upis | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| InsertIzvozPorudzbenica | upis | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| InsertIzvozUslugaDokument | upis | 8 | frmDopunaPlanaPraznimIzvoz, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmOtpremaKontejneraKamionomIzKontejnera, frmOtpremaVozaIzPlana, frmPovezivanjeKontejneraIVagona, frmRadniNalogInterniPregled |
| InsertIzvozVMIzUvoza | upis | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| InsertIzvozVrstaManipulacije | upis | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| InsertIzvozVrstaRobeHS | upis | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| InsertKapija | upis | 3 | frmKamionDetalji, frmKapijaOtvoreniNalozi, frmPregledKamiona |
| InsertKontejnerLog | upis | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| InsertKontejnerTekuce | upis | 1 | frmProdajniNalogIzvoz |
| InsertKorisnici | upis | 1 | frmKorisnici |
| InsertMagacinskiBrojCarinski | upis | 11 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PregledKomercijalnihNaloga, Prijemnica, VanredniPoslovi, frmMestoIsporuke … (+3) |
| InsertMagacinskiPredmet | upis | 3 | frmMestoIsporuke, frmNalogPrijem, frmVozac |
| InsertMestaUtovara | upis | 2 | frmDrumski, frmDrumski1 |
| InsertNaloziZaPrevoz | upis | 1 | frmNalogZaPrevoz |
| InsertNaloziZaPrevozTrosak | upis | 1 | frmNalogZaPrevoz |
| InsertNaruceneManipulacije | upis | 2 | frmManipulacije, frmPregledNarucenihManipulacija |
| InsertNosiociTroskova | upis | 1 | frmPartnerji |
| InsertOpportunity | upis | 1 | frmPartnerji |
| InsertOtpremaKontejnera | upis | 3 | frmOtpremaKontejnera, frmOtpremaKontejneraIzvozKamion, frmOtpremaKontejneraLegetIZVOZ |
| InsertOtpremaKontejneraVozStavke | upis | 4 | frmOtpremaKontejnera, frmOtpremaKontejneraIzvozKamion, frmOtpremaKontejneraLegetIZVOZ, frmPovezivanjeKontejneraIVagona |
| InsertParnerji | upis | 2 | frmDrumski, frmPartnerji |
| InsertPopisKontejnera | upis | 1 | frmPopisKonterjnera |
| InsertPopisKontejneraKontTekuce | upis | 1 | frmPopisKonterjnera |
| InsertPopisKontejneraStavke | upis | 1 | frmPopisKonterjnera |
| InsertPoste | upis | 2 | frmDrumski, frmPartnerji |
| InsertPostupakStavke | upis | 1 | frmOtpremaKontejnera |
| InsertPostupakStavkeOtprema | upis | 1 | frmOtpremaKontejnera |
| InsertPozicija | upis | 1 | frmPozicija |
| InsertPozicioniranje | upis | 2 | frmDrumski, frmDrumski1 |
| InsertPravo | upis | 1 | frmKorisnici |
| InsertPredefinisanePozicije | upis | 1 | frmDefinisiPoziciju |
| InsertPredvidjanje | upis | 1 | frmPartnerji |
| InsertPrijemKontejneraVoz | upis | 1 | frmOtpremaKontejnera |
| InsertPrijemKontejneraVozStavke | upis | 1 | frmOtpremaKontejnera |
| InsertPrijemnica | upis | 2 | Otpremnica, Prijemnica |
| InsertPrijemnicaPalete | upis | 9 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PregledKomercijalnihNaloga, Prijemnica, VanredniPoslovi, frmNalogPrijem … (+1) |
| InsertPrijemnicaPostav | upis | 2 | Otpremnica, Prijemnica |
| InsertProdajniNalogIzvoz | upis | 4 | frmPregledNalogaDrumski, frmPregledPorudzbenica, frmProdajniNalogIzvoz, frmProdajniNalogIzvozTabela |
| InsertProdajniNalogIzvozStavke | upis | 4 | frmPregledNalogaDrumski, frmPregledPorudzbenica, frmProdajniNalogIzvoz, frmProdajniNalogIzvozTabela |
| InsertPromet | upis | 4 | Otpremnica, Prijemnica, frmPrijemKontejneraKamionLegetUvoz, frmPrijemKontejneraLegetVozUvoz |
| InsertPrometManipulacije | upis | 2 | frmPrijemKontejneraKamionLegetUvoz, frmPrijemKontejneraLegetVozUvoz |
| InsertPrometTrans | upis | 2 | Otpremnica, Prijemnica |
| InsertProtokolRadniNalogDrumski | upis | 2 | frmListaProtokola, frmProtokolTransportnogNaloga |
| InsertPutniNalog | upis | 1 | frmPutniNalog |
| InsertPutniNalogGorivo | upis | 1 | frmPutniNalog |
| InsertPutniNalogGume | upis | 1 | frmPutniNalog |
| InsertPutniNalogKvarovi | upis | 1 | frmPutniNalog |
| InsertPutniNalogRuta | upis | 1 | frmPutniNalog |
| InsertPutniNalogServis | upis | 1 | frmPutniNalog |
| InsertPutniNalogTroskovi | upis | 1 | frmPutniNalog |
| InsertPutniNalogZaposleni | upis | 1 | frmPutniNalog |
| InsertRadniNalogDrumski | upis | 9 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmDrumski, frmDrumski1, frmFormaZaVozace, frmObradaUlaznihFaktura, frmPregledNalogaDrumski, frmPregledPorukeVozacu … (+1) |
| InsertRadniNalogDrumskiNHM | upis | 9 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmDrumski, frmDrumski1, frmFormaZaVozace, frmObradaUlaznihFaktura, frmPregledNalogaDrumski, frmPregledPorukeVozacu … (+1) |
| InsertRadniNalogDrumskiNovi | upis | 9 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmDrumski, frmDrumski1, frmFormaZaVozace, frmObradaUlaznihFaktura, frmPregledNalogaDrumski, frmPregledPorukeVozacu … (+1) |
| InsertRadniNalogInterni | upis | 14 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmCerada, frmDrumski, frmDrumski1, frmKapijaOtvoreniNalozi, frmPlatforma, frmPlatformaOtprema … (+6) |
| InsertRadniNalogInterni2 | upis | 14 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmCerada, frmDrumski, frmDrumski1, frmKapijaOtvoreniNalozi, frmPlatforma, frmPlatformaOtprema … (+6) |
| InsertRadniNalogInterniIzvoz | upis | 14 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmCerada, frmDrumski, frmDrumski1, frmKapijaOtvoreniNalozi, frmPlatforma, frmPlatformaOtprema … (+6) |
| InsertRadniNalogInterniIzvoz2 | upis | 14 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmCerada, frmDrumski, frmDrumski1, frmKapijaOtvoreniNalozi, frmPlatforma, frmPlatformaOtprema … (+6) |
| InsertRadniNalogInterniIzvozDodatne | upis | 14 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmCerada, frmDrumski, frmDrumski1, frmKapijaOtvoreniNalozi, frmPlatforma, frmPlatformaOtprema … (+6) |
| InsertRadniNalogInterniPotvrda | upis | 14 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmCerada, frmDrumski, frmDrumski1, frmKapijaOtvoreniNalozi, frmPlatforma, frmPlatformaOtprema … (+6) |
| InsertRadniNalogInterniSkladista | upis | 9 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PregledKomercijalnihNaloga, Prijemnica, VanredniPoslovi, frmNalogPrijem … (+1) |
| InsertRadniNalogInterniSkladistePotvrda | upis | 14 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmCerada, frmDrumski, frmDrumski1, frmKapijaOtvoreniNalozi, frmPlatforma, frmPlatformaOtprema … (+6) |
| InsertRadniNalogInterniStorno | upis | 14 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmCerada, frmDrumski, frmDrumski1, frmKapijaOtvoreniNalozi, frmPlatforma, frmPlatformaOtprema … (+6) |
| InsertRadniNalogSkladista | upis | 11 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PregledKomercijalnihNaloga, Prijemnica, VanredniPoslovi, frmMestoIsporuke … (+3) |
| InsertRadniNalogSkladistaDirektni | upis | 3 | frmMestoIsporuke, frmNalogPrijem, frmVozac |
| InsertRadniNalogTransport | upis | 1 | frmRadniNalogTransport |
| InsertRadniNalogTransportStavke | upis | 1 | frmRadniNalogTransport |
| InsertRN10PregledCeoVoz | upis | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| InsertRN11PregledCeoVozOtprema | upis | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| InsertRN12Medjuskladisni | upis | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| InsertRN4PrPlatformeKamIzvoz | upis | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| InsertRN4PrPlatformeKamUvoz | upis | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| InsertRN5PrPlatFormeKam | upis | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| InsertRN6OtPlatFormeKam | upis | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| InsertRN7OtpremaOtpremaPlatKam | upis | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| InsertRN8OtpremaCiradeKam | upis | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| InsertRN8OtpremaCiradeKamIzvoz | upis | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| InsertRN9PrijemCiradeKam | upis | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| InsertRNCarinskoPrijemnicaStavke | upis | 11 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PregledKomercijalnihNaloga, Prijemnica, VanredniPoslovi, frmMestoIsporuke … (+3) |
| InsertRNCarinskoRukovalac | upis | 11 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PregledKomercijalnihNaloga, Prijemnica, VanredniPoslovi, frmMestoIsporuke … (+3) |
| InsertRNCarinskoSkladiste | upis | 3 | frmMestoIsporuke, frmNalogPrijem, frmVozac |
| InsertRnCarinskoSkladisteDodatneUsluge | upis | 11 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PregledKomercijalnihNaloga, Prijemnica, VanredniPoslovi, frmMestoIsporuke … (+3) |
| InsertRnCarinskoSkladisteOtpremaStavkePom | upis | 9 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PregledKomercijalnihNaloga, Prijemnica, VanredniPoslovi, frmNalogPrijem … (+1) |
| InsertRNCarinskoSkladisteOtpremnica | upis | 9 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PregledKomercijalnihNaloga, Prijemnica, VanredniPoslovi, frmNalogPrijem … (+1) |
| InsertRNCarinskoSkladistePrijemnica | upis | 11 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PregledKomercijalnihNaloga, Prijemnica, VanredniPoslovi, frmMestoIsporuke … (+3) |
| InsertRNInterniSkladisteDodatneUsluge | upis | 3 | frmMestoIsporuke, frmNalogPrijem, frmVozac |
| InsertRNOtpremaCirade | upis | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| InsertRNOtpremaPlatforme | upis | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| InsertRNOtpremaPlatforme2 | upis | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| InsertRNOtpremaVoza | upis | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| InsertRNOtpremaVozaCeoVoz | upis | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| InsertRNPregledIpostavkaKontejnera | upis | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| InsertRNPregledIspraznjenogKontejnera | upis | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| InsertRNPrijemCirade | upis | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| InsertRNPrijemPlatforme | upis | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| InsertRNPrijemPlatforme2 | upis | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| InsertRNPrijemVoza | upis | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| InsertRNPrijemVoza2 | upis | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| InsertRNPrijemVozaCeoVoz | upis | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| InsertRNPrijemVozaCeoVozPretovar | upis | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| InsertScenario | upis | 3 | frmIzvozUnosManipulacije, frmUnosManipulacija, frmUvozKonacna |
| InsertSkladista | upis | 9 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PregledKomercijalnihNaloga, Prijemnica, VanredniPoslovi, frmNalogPrijem … (+1) |
| InsertSkladistaGrupa | upis | 1 | SkladisteGrupa |
| InsertStatusKapija | upis | 1 | frmStatus |
| InsertStatusVozila | upis | 1 | frmStatus |
| InsertTerminalPrivArhiv | upis | 3 | frmTerminalPrivremeni, frmTerminalPrivremeniExcel, frmTerminalPrivremeniSlike |
| InsertTipKontenjera | upis | 1 | frmTipKontejnera |
| InsertTipPalete | upis | 1 | frmTipPalete |
| InsertTokens | upis | 9 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmDrumski, frmDrumski1, frmFormaZaVozace, frmObradaUlaznihFaktura, frmPregledNalogaDrumski, frmPregledPorukeVozacu … (+1) |
| InsertTransPosPostav | upis | 2 | Otpremnica, Prijemnica |
| InsertUlFak | upis | 1 | frmPartnerji |
| InsertUlFakPostav | upis | 1 | frmPartnerji |
| InsertUvozDokumenta | upis | 1 | UvozDokumenta |
| InsertUvozDokumentaCeoVoz | upis | 1 | UvozDokumenta |
| InsertUvozDokumentaUsluge | upis | 1 | UvozDokumenta |
| InsertUvozKonacbaVrstaRobeHS | upis | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| InsertUvozKonacna | upis | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| InsertUvozKonacnaNapomenePozicioniranja | upis | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| InsertUvozKonacnaNHM | upis | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| InsertUvozKonacnaNHMDiana | upis | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| InsertUvozKonacnaVrstaManipulacije | upis | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| InsertUvozKonacnaZaglavlje | upis | 1 | frmUvozKonacna |
| InsertUvozNapomenePozicioniranja | upis | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| InsertUvozNHM | upis | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| InsertUvozNHMDiana | upis | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| InsertUvozUslugaDokument | upis | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| InsertUvozUvoznici | upis | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| InsertUvozVrstaManipulacije | upis | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| InsertUvozVrstaRobeHS | upis | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| InsertVaganje | upis | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| InsertVoz | upis | 2 | frmPregledVozova, frmVoz |
| InsertVoziloUsluga | upis | 1 | frmVoziloUsluga |
| InsertVozSerijeKola | upis | 2 | frmPregledVozova, frmVoz |
| InsertZapisnikONepravilnosti | upis | 1 | ZapisnikONepravilnosti |
| InsertZapisnikOOstecenju | upis | 1 | ZapisnikOOstecenjuRobe |
| InsertZone | upis | 1 | frmZone |
| insTerminalPriv | upis | 3 | frmTerminalPrivremeni, frmTerminalPrivremeniExcel, frmTerminalPrivremeniSlike |
| insTerminalPrivFromExcel | upis | 3 | frmTerminalPrivremeni, frmTerminalPrivremeniExcel, frmTerminalPrivremeniSlike |
| InsUbaciDodatnuUsluguIzvoz | upis | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| InsUbaciDodatnuUsluguUvoz | upis | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| KopirajKontejner | ostalo | 5 | frmIzvozPregledKontejneraDrumskeUsluge, frmNalogPrijem, frmPregledKontejneraDrumskeUsluge, frmUnosManipulacija, frmUvozKopirajUslugeKontejnera |
| KopirajKontejnerIzKonacne | ostalo | 5 | frmIzvozPregledKontejneraDrumskeUsluge, frmNalogPrijem, frmPregledKontejneraDrumskeUsluge, frmUnosManipulacija, frmUvozKopirajUslugeKontejnera |
| KopirajKontejnerIzvoz | ostalo | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| NapraviRadniNalogDrumski | ostalo | 5 | frmIzvozPregledKontejneraDrumskeUsluge, frmNalogPrijem, frmPregledKontejneraDrumskeUsluge, frmUnosManipulacija, frmUvozKopirajUslugeKontejnera |
| NapraviRadniNalogDrumskiIzDrumskog | ostalo | 9 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmDrumski, frmDrumski1, frmFormaZaVozace, frmObradaUlaznihFaktura, frmPregledNalogaDrumski, frmPregledPorukeVozacu … (+1) |
| ObavljenPregled | ostalo | 14 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmCerada, frmDrumski, frmDrumski1, frmKapijaOtvoreniNalozi, frmPlatforma, frmPlatformaOtprema … (+6) |
| OdrediKontejnerTerminal | ostalo | 8 | frmDopunaPlanaPraznimIzvoz, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmOtpremaKontejneraKamionomIzKontejnera, frmOtpremaVozaIzPlana, frmPovezivanjeKontejneraIVagona, frmRadniNalogInterniPregled |
| PbrisiKamionZaRadniNalogDrumski | ostalo | 9 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmDrumski, frmDrumski1, frmFormaZaVozace, frmObradaUlaznihFaktura, frmPregledNalogaDrumski, frmPregledPorukeVozacu … (+1) |
| PokreniSkladisninuKontejnera | ostalo | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| PotvrdiPrijem | ostalo | 1 | Prijemnica |
| PoveziPredvidjanjeFaktura | ostalo | 1 | frmPartnerji |
| PrebaciNHMUvozUvozKonacna | ostalo | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| PrebaciVrsterobeHSUvozUvozKonacna | ostalo | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| PrekopirajStavke | ostalo | 4 | frmOtpremaKontejnera, frmOtpremaKontejneraIzvozKamion, frmOtpremaKontejneraLegetIZVOZ, frmPovezivanjeKontejneraIVagona |
| PrekopirajVoz | ostalo | 2 | frmPregledVozova, frmVoz |
| PrenesiIzPlanIzvozaUPlanIzvoza | ostalo | 8 | frmDopunaPlanaPraznimIzvoz, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmOtpremaKontejneraKamionomIzKontejnera, frmOtpremaVozaIzPlana, frmPovezivanjeKontejneraIVagona, frmRadniNalogInterniPregled |
| PrenesiIzPlanUtovaraUPlanUtovara | ostalo | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| PrenesiKontejnerTekuceUArhiv | ostalo | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| PrenesiUPlanUtovaraSelektovano | ostalo | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| PrenesiUPlanUtovaraSelektovanoIzvoz | ostalo | 8 | frmDopunaPlanaPraznimIzvoz, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmOtpremaKontejneraKamionomIzKontejnera, frmOtpremaVozaIzPlana, frmPovezivanjeKontejneraIVagona, frmRadniNalogInterniPregled |
| PrenesiUPlanUtovaraSelektovanoIzvozBrodar | ostalo | 8 | frmDopunaPlanaPraznimIzvoz, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmOtpremaKontejneraKamionomIzKontejnera, frmOtpremaVozaIzPlana, frmPovezivanjeKontejneraIVagona, frmRadniNalogInterniPregled |
| PrenesiUPlanUtovaraSelektovanoIzvozTErminal | ostalo | 8 | frmDopunaPlanaPraznimIzvoz, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmOtpremaKontejneraKamionomIzKontejnera, frmOtpremaVozaIzPlana, frmPovezivanjeKontejneraIVagona, frmRadniNalogInterniPregled |
| PromenaBrojaKontejnera | ostalo | 2 | Otpremnica, Prijemnica |
| PromeniBrojVagona | ostalo | 4 | frmOtpremaKontejnera, frmOtpremaKontejneraIzvozKamion, frmOtpremaKontejneraLegetIZVOZ, frmPovezivanjeKontejneraIVagona |
| PromeniSaPrijemniceUK | ostalo | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| PromeniStatusKalmar | ostalo | 14 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmCerada, frmDrumski, frmDrumski1, frmKapijaOtvoreniNalozi, frmPlatforma, frmPlatformaOtprema … (+6) |
| PromeniStatusKapijaOdlazak | ostalo | 14 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmCerada, frmDrumski, frmDrumski1, frmKapijaOtvoreniNalozi, frmPlatforma, frmPlatformaOtprema … (+6) |
| PromeniStatusPretovareno | ostalo | 14 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmCerada, frmDrumski, frmDrumski1, frmKapijaOtvoreniNalozi, frmPlatforma, frmPlatformaOtprema … (+6) |
| PromeniStatusPrijemPotvrdioKapiju | ostalo | 14 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmCerada, frmDrumski, frmDrumski1, frmKapijaOtvoreniNalozi, frmPlatforma, frmPlatformaOtprema … (+6) |
| PromeniStatusPrijemPotvrdioVP | ostalo | 14 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmCerada, frmDrumski, frmDrumski1, frmKapijaOtvoreniNalozi, frmPlatforma, frmPlatformaOtprema … (+6) |
| RefreshPredvidjanja | ostalo | 1 | frmPartnerji |
| sp_ArtikliKatalog_Uvezi | ostalo | 1 | Prijemnica |
| sp_GO_Otkazi | ostalo | 4 | frmHREvidencijaRada, frmHRGOPravo, frmHRGOZahtevi, frmHRZaposleni |
| sp_GO_PodnesiZahtev | ostalo | 4 | frmHREvidencijaRada, frmHRGOPravo, frmHRGOZahtevi, frmHRZaposleni |
| sp_PL_Analiziraj | ostalo | 1 | Prijemnica |
| sp_PL_FormirajArtikle | ostalo | 1 | Prijemnica |
| sp_PrijemnicaStavka_Snimi | ostalo | 1 | Prijemnica |
| sp_set_session_context | ostalo | 28 | frmHRDan, frmHREvidencijaRada, frmHRGOPravo, frmHRGOZahtevi, frmHRGrupniUnos, frmHRKontrolnaTabla, frmHRLog, frmHRMeni … (+20) |
| spIzbrisiKUScenario | ostalo | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| spPrenesiIzPlanUtovaraUOtpremaVoz | ostalo | 8 | frmDopunaPlanaPraznimIzvoz, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmOtpremaKontejneraKamionomIzKontejnera, frmOtpremaVozaIzPlana, frmPovezivanjeKontejneraIVagona, frmRadniNalogInterniPregled |
| spPrenesiIzUvozaUIzvoz | ostalo | 8 | frmDopunaPlanaPraznimIzvoz, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmOtpremaKontejneraKamionomIzKontejnera, frmOtpremaVozaIzPlana, frmPovezivanjeKontejneraIVagona, frmRadniNalogInterniPregled |
| spPrenesiKontejnerIzNerasporedjenihUPrijPlatforma | ostalo | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| spPrenesiKontejnerIzPlanaNaPrijemnicu | ostalo | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| spPrenesiKontejnerIzPlanaNaPrijemnicuIzvoz | ostalo | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| spPrenesiKontejnerIzPlanaNaPrijemnicuIzvozPoIdNadredjenog | ostalo | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| spPrenesiKontejnerUOtpremuKamionom | ostalo | 8 | frmDopunaPlanaPraznimIzvoz, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmOtpremaKontejneraKamionomIzKontejnera, frmOtpremaVozaIzPlana, frmPovezivanjeKontejneraIVagona, frmRadniNalogInterniPregled |
| spPrenesiKontejnerUOtpremuKamionomIzvoz | ostalo | 8 | frmDopunaPlanaPraznimIzvoz, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmOtpremaKontejneraKamionomIzKontejnera, frmOtpremaVozaIzPlana, frmPovezivanjeKontejneraIVagona, frmRadniNalogInterniPregled |
| spPrenesiPlanUtovaraUPrijemPlatforma | ostalo | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| spPrenesiPlanUtovaraUPrijemPlatformaIzvoz | ostalo | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| spPrenesiPlanUtovaraUPrijemVoz | ostalo | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| spStornijaRNI | ostalo | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| StornoRadniNalogInterni | ostalo | 9 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PregledKomercijalnihNaloga, Prijemnica, VanredniPoslovi, frmNalogPrijem … (+1) |
| UbaciURadniNalogInterniBeyScenarija | ostalo | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| UpdateAutomobili | izmena | 3 | frmAutomobilEdit, frmAutomobiliDrumski, frmPodesavanjeRaspolozivosti |
| UpdateAutomobiliDrumski | izmena | 3 | frmAutomobilEdit, frmAutomobiliDrumski, frmPodesavanjeRaspolozivosti |
| UpdateAutomobiliServis | izmena | 3 | frmAutomobilEdit, frmAutomobiliDrumski, frmPodesavanjeRaspolozivosti |
| UpdateAutoprevoznilist | izmena | 1 | frmAutoprevozniList2 |
| UpdateAutoprevoznilistGeneralni | izmena | 1 | frmAutoprevozniList2 |
| UpdateAutoprevoznilistKameni | izmena | 1 | frmAutoprevozniList2 |
| UpdateBrojKolaIzExcela | izmena | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| UpdateBrojKolaIzExcelaVoz | izmena | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| UpdateCarinskoStavke | izmena | 9 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PregledKomercijalnihNaloga, Prijemnica, VanredniPoslovi, frmNalogPrijem … (+1) |
| UpdateDogovorenoIzvoz | izmena | 1 | DogovoreniPosloviIzvoza |
| UpdateDokumentaFaktureDrumski | izmena | 5 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmNalogZaFakturisanje, frmObradaUlaznihFaktura, frmSeniranjeDokumenata |
| UpdateEmailOtpremaNajava | izmena | 3 | frmOtpremaKontejnera, frmOtpremaKontejneraIzvozKamion, frmOtpremaKontejneraLegetIZVOZ |
| UpdateEmailOtpremaOtprema | izmena | 3 | frmOtpremaKontejnera, frmOtpremaKontejneraIzvozKamion, frmOtpremaKontejneraLegetIZVOZ |
| UpdateEmailPrijemNajava | izmena | 1 | frmOtpremaKontejnera |
| UpdateEmailPrijemPrijem | izmena | 1 | frmOtpremaKontejnera |
| UpdateFaktura | izmena | 1 | frmPartnerji |
| UpdateFakturaDrumskiStavka | izmena | 5 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmNalogZaFakturisanje, frmObradaUlaznihFaktura, frmSeniranjeDokumenata |
| UpdateIZCIRA | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateIzvoz | izmena | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| UpdateIzvozKonacna | izmena | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| UpdateIzvozKonacnaVaganjeCirada | izmena | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| UpdateIzvozKonacnaVaganjePlatforma | izmena | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| UpdateIzvozKonacnaVagon | izmena | 8 | frmDopunaPlanaPraznimIzvoz, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmOtpremaKontejneraKamionomIzKontejnera, frmOtpremaVozaIzPlana, frmPovezivanjeKontejneraIVagona, frmRadniNalogInterniPregled |
| UpdateIzvozKonacnaZaglavlje | izmena | 2 | frmIzvozKonacna, frmVoz |
| UpdateIzvozPorudzbenica | izmena | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| UpdateIzvozPorudzbenicaCarina | izmena | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| UpdateIzvozPorudzbenicaPojedinacni | izmena | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| UpdateIzvozPorudzbenicaRelacija | izmena | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| UpdateIzvozStatus | izmena | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| UpdateIzvozStatusTerminal | izmena | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| UpdateIzvozTerminalIzbor | izmena | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| UpdateKapija | izmena | 3 | frmKamionDetalji, frmKapijaOtvoreniNalozi, frmPregledKamiona |
| UpdateKapijaNalogID | izmena | 3 | frmKamionDetalji, frmKapijaOtvoreniNalozi, frmPregledKamiona |
| UpdateKapijaNeslaganje | izmena | 3 | frmKamionDetalji, frmKapijaOtvoreniNalozi, frmPregledKamiona |
| UpdateKontejnerTekuce | izmena | 1 | frmProdajniNalogIzvoz |
| UpdateKorakPotvrdeVozaca | izmena | 9 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmDrumski, frmDrumski1, frmFormaZaVozace, frmObradaUlaznihFaktura, frmPregledNalogaDrumski, frmPregledPorukeVozacu … (+1) |
| UpdateMagacinskiBrojCarinski | izmena | 11 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PregledKomercijalnihNaloga, Prijemnica, VanredniPoslovi, frmMestoIsporuke … (+3) |
| UpdateMagacinskiPredmet | izmena | 3 | frmMestoIsporuke, frmNalogPrijem, frmVozac |
| UpdateMestaUtovara | izmena | 2 | frmDrumski, frmDrumski1 |
| UpdateNajavaCIM | izmena | 1 | frmFotografijePregledac |
| UpdateNaloziZaprevoz | izmena | 1 | frmNalogZaPrevoz |
| UpdateNaloziZaPrevozTrosak | izmena | 1 | frmNalogZaPrevoz |
| UpdateNijeUradjenoNaruceneManipulacije | izmena | 2 | frmManipulacije, frmPregledNarucenihManipulacija |
| UpdateNosiociTroska | izmena | 1 | frmPartnerji |
| UpdateOpportunity | izmena | 1 | frmPartnerji |
| UpdateOtpremaKontejnera | izmena | 3 | frmOtpremaKontejnera, frmOtpremaKontejneraIzvozKamion, frmOtpremaKontejneraLegetIZVOZ |
| UpdateOtpremaKontejneraVozStavke | izmena | 4 | frmOtpremaKontejnera, frmOtpremaKontejneraIzvozKamion, frmOtpremaKontejneraLegetIZVOZ, frmPovezivanjeKontejneraIVagona |
| UpdateOtpremaKontejneraVozStavkeRB | izmena | 1 | frmOtpremaKontejnera |
| UpdateOtpremaKontVozStavkeVagon | izmena | 4 | frmOtpremaKontejnera, frmOtpremaKontejneraIzvozKamion, frmOtpremaKontejneraLegetIZVOZ, frmPovezivanjeKontejneraIVagona |
| UpdatePartneri | izmena | 2 | frmDrumski, frmPartnerji |
| UpdatePopisKontejnera | izmena | 1 | frmPopisKonterjnera |
| UpdatePopisKontejneraStavke | izmena | 1 | frmPopisKonterjnera |
| UpdatePorudzbenica | izmena | 2 | Otpremnica, Prijemnica |
| UpdatePoslatStatus | izmena | 9 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmDrumski, frmDrumski1, frmFormaZaVozace, frmObradaUlaznihFaktura, frmPregledNalogaDrumski, frmPregledPorukeVozacu … (+1) |
| UpdatePozicija | izmena | 1 | frmPozicija |
| UpdatePozicioniranje | izmena | 2 | frmDrumski, frmDrumski1 |
| UpdatePredefinisanePozicije | izmena | 1 | frmDefinisiPoziciju |
| UpdatePredvidjanje | izmena | 1 | frmPartnerji |
| UpdatePrijemKontejneraVoz | izmena | 1 | frmOtpremaKontejnera |
| UpdatePrijemKontejneraVozStavke | izmena | 1 | frmOtpremaKontejnera |
| UpdatePrijemKontejneraVozStavkeRB | izmena | 1 | frmOtpremaKontejnera |
| UpdateProdajniNalogIzvoz | izmena | 4 | frmPregledNalogaDrumski, frmPregledPorudzbenica, frmProdajniNalogIzvoz, frmProdajniNalogIzvozTabela |
| UpdateProdajniNalogIzvozStavke | izmena | 4 | frmPregledNalogaDrumski, frmPregledPorudzbenica, frmProdajniNalogIzvoz, frmProdajniNalogIzvozTabela |
| UpdateProdajniNalogStavkeDrumski | izmena | 4 | frmPregledNalogaDrumski, frmPregledPorudzbenica, frmProdajniNalogIzvoz, frmProdajniNalogIzvozTabela |
| UpdateProdajniNalogStavkeKolicina | izmena | 4 | frmPregledNalogaDrumski, frmPregledPorudzbenica, frmProdajniNalogIzvoz, frmProdajniNalogIzvozTabela |
| UpdateProdajniNalogStavkeKolicinaVecFormirano | izmena | 4 | frmPregledNalogaDrumski, frmPregledPorudzbenica, frmProdajniNalogIzvoz, frmProdajniNalogIzvozTabela |
| UpdateProdajniNalogStavkeScenario | izmena | 4 | frmPregledNalogaDrumski, frmPregledPorudzbenica, frmProdajniNalogIzvoz, frmProdajniNalogIzvozTabela |
| UpdateProdajniNalogStavkeStorno | izmena | 4 | frmPregledNalogaDrumski, frmPregledPorudzbenica, frmProdajniNalogIzvoz, frmProdajniNalogIzvozTabela |
| UpdateProtokolRadniNalogDrumski | izmena | 2 | frmListaProtokola, frmProtokolTransportnogNaloga |
| UpdatePutniNalog | izmena | 1 | frmPutniNalog |
| UpdatePutniNalogGorivo | izmena | 1 | frmPutniNalog |
| UpdatePutniNalogGume | izmena | 1 | frmPutniNalog |
| UpdatePutniNalogKvarovi | izmena | 1 | frmPutniNalog |
| UpdatePutniNalogRuta | izmena | 1 | frmPutniNalog |
| UpdatePutniNalogServis | izmena | 1 | frmPutniNalog |
| UpdatePutniNalogTroskovi | izmena | 1 | frmPutniNalog |
| UpdatePutniNalogZaposleni | izmena | 1 | frmPutniNalog |
| UpdateRadniNalogDirektniIstovar | izmena | 3 | frmMestoIsporuke, frmNalogPrijem, frmVozac |
| UpdateRadniNalogDirektniVozilo | izmena | 3 | frmMestoIsporuke, frmNalogPrijem, frmVozac |
| UpdateRadniNalogDrumski | izmena | 9 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmDrumski, frmDrumski1, frmFormaZaVozace, frmObradaUlaznihFaktura, frmPregledNalogaDrumski, frmPregledPorukeVozacu … (+1) |
| UpdateRadniNalogDrumskiIzvoz | izmena | 9 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmDrumski, frmDrumski1, frmFormaZaVozace, frmObradaUlaznihFaktura, frmPregledNalogaDrumski, frmPregledPorukeVozacu … (+1) |
| UpdateRadniNalogDrumskiKamionID | izmena | 9 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmDrumski, frmDrumski1, frmFormaZaVozace, frmObradaUlaznihFaktura, frmPregledNalogaDrumski, frmPregledPorukeVozacu … (+1) |
| UpdateRadniNalogDrumskiListaKamionID | izmena | 9 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmDrumski, frmDrumski1, frmFormaZaVozace, frmObradaUlaznihFaktura, frmPregledNalogaDrumski, frmPregledPorukeVozacu … (+1) |
| UpdateRadniNalogDrumskiNovi | izmena | 9 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmDrumski, frmDrumski1, frmFormaZaVozace, frmObradaUlaznihFaktura, frmPregledNalogaDrumski, frmPregledPorukeVozacu … (+1) |
| UpdateRadniNalogDrumskiOdobrioLO | izmena | 9 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmDrumski, frmDrumski1, frmFormaZaVozace, frmObradaUlaznihFaktura, frmPregledNalogaDrumski, frmPregledPorukeVozacu … (+1) |
| UpdateRadniNalogDrumskiOtkazi | izmena | 9 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmDrumski, frmDrumski1, frmFormaZaVozace, frmObradaUlaznihFaktura, frmPregledNalogaDrumski, frmPregledPorukeVozacu … (+1) |
| UpdateRadniNalogDrumskiPoslataNajava | izmena | 9 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmDrumski, frmDrumski1, frmFormaZaVozace, frmObradaUlaznihFaktura, frmPregledNalogaDrumski, frmPregledPorukeVozacu … (+1) |
| UpdateRadniNalogDrumskiPoslateInstrukcije | izmena | 9 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmDrumski, frmDrumski1, frmFormaZaVozace, frmObradaUlaznihFaktura, frmPregledNalogaDrumski, frmPregledPorukeVozacu … (+1) |
| UpdateRadniNalogInterni | izmena | 14 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmCerada, frmDrumski, frmDrumski1, frmKapijaOtvoreniNalozi, frmPlatforma, frmPlatformaOtprema … (+6) |
| UpdateRadniNalogInterniFormiran | izmena | 14 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmCerada, frmDrumski, frmDrumski1, frmKapijaOtvoreniNalozi, frmPlatforma, frmPlatformaOtprema … (+6) |
| UpdateRadniNalogInterniGenerisan | izmena | 14 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmCerada, frmDrumski, frmDrumski1, frmKapijaOtvoreniNalozi, frmPlatforma, frmPlatformaOtprema … (+6) |
| UpdateRadniNalogInterniZavrsen | izmena | 14 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmCerada, frmDrumski, frmDrumski1, frmKapijaOtvoreniNalozi, frmPlatforma, frmPlatformaOtprema … (+6) |
| UpdateRadniNalogSkladista | izmena | 11 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PregledKomercijalnihNaloga, Prijemnica, VanredniPoslovi, frmMestoIsporuke … (+3) |
| UpdateRadniNalogSkladistaDirektni | izmena | 3 | frmMestoIsporuke, frmNalogPrijem, frmVozac |
| UpdateRadniNalogTransportStavke | izmena | 1 | frmRadniNalogTransport |
| UpdateRadniNalogZaTransport | izmena | 1 | frmRadniNalogTransport |
| UpdateRN12MedjuskladisniSkladiste | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRN12Uradjene | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRN1Uradjene | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRN1UradjeneCIR | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRN1UradjeneS | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRN1UradjeneVP | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRN2Uradjene | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRN3Uradjene | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRN4PotrebanCIR | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRN4PrivremenoSkladiste | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRN4SkladisteIzCira | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRN4Uradjene | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRN4UradjeneCIR | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRN4UradjenePostavljen | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRN4UradjeneVizuelni | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRN5PrijemSkladiste | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRN5Uradjene | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRN5UradjeneCIR | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRN5UradjeneS | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRN6OtpremaSkladiste | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRN6PotrebanCIR | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRN6Uradjene | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRN6UradjeneVizuelni | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRN7Uradjene | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRN8Uradjene | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRN8UradjeneCIR | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRN9PretovarCIRADE | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRN9Uradjen | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRNCarinskoSkladiste | izmena | 3 | frmMestoIsporuke, frmNalogPrijem, frmVozac |
| UpdateRNCarinskoSkladisteOtpremnica | izmena | 9 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PregledKomercijalnihNaloga, Prijemnica, VanredniPoslovi, frmNalogPrijem … (+1) |
| UpdateRNCarinskoSkladistePrijemnica | izmena | 11 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PregledKomercijalnihNaloga, Prijemnica, VanredniPoslovi, frmMestoIsporuke … (+3) |
| UpdateRNInterniSkladista | izmena | 9 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PregledKomercijalnihNaloga, Prijemnica, VanredniPoslovi, frmNalogPrijem … (+1) |
| UpdateRNIVozFormiran | izmena | 14 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmCerada, frmDrumski, frmDrumski1, frmKapijaOtvoreniNalozi, frmPlatforma, frmPlatformaOtprema … (+6) |
| UpdateRNMedjuskladisni | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRNOtpremaCirade | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRNOtpremaPlatforme | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRNOtpremaPlatforme2 | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRNOtpremaVoza | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRNPregledIpostavkaKontejnera | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRNPregledIspraznjenogKontejnera | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRNPrijemCirade | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRNPrijemPlatforme | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRNPrijemPlatforme2 | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRNPrijemVoza | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRNPrijemVoza2 | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateRNPrijemVozaSkladiste | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| UpdateScenario | izmena | 3 | frmIzvozUnosManipulacije, frmUnosManipulacija, frmUvozKonacna |
| UpdateScenarioKontejnera | izmena | 3 | frmIzvozUnosManipulacije, frmUnosManipulacija, frmUvozKonacna |
| UpdateSkladista | izmena | 9 | InterniPoslovi, InterniPrenos, Otprema, Otpremnica, PregledKomercijalnihNaloga, Prijemnica, VanredniPoslovi, frmNalogPrijem … (+1) |
| UpdateSkladistaGrupa | izmena | 1 | SkladisteGrupa |
| UpdateStatusFajla | izmena | 5 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmNalogZaFakturisanje, frmObradaUlaznihFaktura, frmSeniranjeDokumenata |
| UpdateStatusKamionaKapija | izmena | 3 | frmKamionDetalji, frmKapijaOtvoreniNalozi, frmPregledKamiona |
| UpdateStatusKapija | izmena | 1 | frmStatus |
| UpdateStatusRadniNalogDrumski | izmena | 9 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmDrumski, frmDrumski1, frmFormaZaVozace, frmObradaUlaznihFaktura, frmPregledNalogaDrumski, frmPregledPorukeVozacu … (+1) |
| UpdateStatusVozila | izmena | 1 | frmStatus |
| UpdateTipKontenjera | izmena | 1 | frmTipKontejnera |
| UpdateTipPalete | izmena | 1 | frmTipPalete |
| UpdateUlaznaFakturaStavka | izmena | 5 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmNalogZaFakturisanje, frmObradaUlaznihFaktura, frmSeniranjeDokumenata |
| UpdateUlFak | izmena | 1 | frmPartnerji |
| UpdateUradjenoNaruceneManipulacije | izmena | 2 | frmManipulacije, frmPregledNarucenihManipulacija |
| UpdateUvoz | izmena | 5 | frmIzvozPregledKontejneraDrumskeUsluge, frmNalogPrijem, frmPregledKontejneraDrumskeUsluge, frmUnosManipulacija, frmUvozKopirajUslugeKontejnera |
| UpdateUvozKonacna | izmena | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| UpdateUvozKonacnaManFaktura | izmena | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| UpdateUvozKonacnaManZaFakturisanje | izmena | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| UpdateUvozKonacnaObjedinjenja | izmena | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| UpdateUvozKonacnaPK | izmena | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| UpdateUvozKonacnaUC | izmena | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| UpdateUvozKonacnaZaglavlje | izmena | 1 | frmUvozKonacna |
| UpdateUvozNHMTezine | izmena | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| UpdateVaganje | izmena | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| UpdateVoz | izmena | 2 | frmPregledVozova, frmVoz |
| UpdateVozila | izmena | 2 | frmPrijemKontejneraKamionLegetUvoz, frmPrijemKontejneraLegetVozUvoz |
| UpdateVoziloUsluga | izmena | 1 | frmVoziloUsluga |
| UpdateZatvoren | izmena | 2 | frmPrijemKontejneraKamionLegetUvoz, frmPrijemKontejneraLegetVozUvoz |
| UpdateZatvorenOtprema | izmena | 2 | frmPrijemKontejneraKamionLegetUvoz, frmPrijemKontejneraLegetVozUvoz |
| UpdateZatvorenOtpremaCheck | izmena | 3 | frmOtpremaKontejnera, frmOtpremaKontejneraIzvozKamion, frmOtpremaKontejneraLegetIZVOZ |
| UpdateZatvorenOtpremaPBRK | izmena | 2 | frmPrijemKontejneraKamionLegetUvoz, frmPrijemKontejneraLegetVozUvoz |
| UpdateZone | izmena | 1 | frmZone |
| UpdCenaIzvozKonacnaVrstaManipulacije | izmena | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| UpdCenaIzvozVrstaManipulacije | izmena | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| UpdCenaUvozKonacnaVrstaManipulacije | izmena | 15 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmIzvozPregledKontejneraDrumskeUsluge, frmKapijaOtvoreniNalozi, frmNalogPrijem, frmPregledKontejneraDrumskeUsluge, frmPretovarPrijem … (+7) |
| UpdCenaUvozVrstaManipulacije | izmena | 15 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmIzvozPregledKontejneraDrumskeUsluge, frmKapijaOtvoreniNalozi, frmNalogPrijem, frmPregledKontejneraDrumskeUsluge, frmPretovarPrijem … (+7) |
| UpdPDVIzvozKonacnaVrstaManipulacije | izmena | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| UpdPDVIzvozVrstaManipulacije | izmena | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| UpdPDVUvozKonacnaVrstaManipulacije | izmena | 5 | frmIzvozPregledKontejneraDrumskeUsluge, frmNalogPrijem, frmPregledKontejneraDrumskeUsluge, frmUnosManipulacija, frmUvozKopirajUslugeKontejnera |
| UpdPDVUvozVrstaManipulacije | izmena | 5 | frmIzvozPregledKontejneraDrumskeUsluge, frmNalogPrijem, frmPregledKontejneraDrumskeUsluge, frmUnosManipulacija, frmUvozKopirajUslugeKontejnera |
| UpdPlatiocaIzvozKonacnaVrstaManipulacije | izmena | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| UpdPlatiocaIzvozVrstaManipulacije | izmena | 13 | PakovanjeKamiona1, Vaganje, frmCarinskiPostupak, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmIzvozKopiranjeUslugaKontejnera1, frmIzvozUnosManipulacije … (+5) |
| UpdPlatiocaUvozKonacnaVrstaManipulacije | izmena | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| UpdPlatiocaUvozVrstaManipulacije | izmena | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| UpdPokretFormaStatus | izmena | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| updRadniNalogInterniPrijemnicaPotvrdjen | izmena | 14 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmCerada, frmDrumski, frmDrumski1, frmKapijaOtvoreniNalozi, frmPlatforma, frmPlatformaOtprema … (+6) |
| updRadniNalogInterniRukovalacPotvrdjen | izmena | 14 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmCerada, frmDrumski, frmDrumski1, frmKapijaOtvoreniNalozi, frmPlatforma, frmPlatformaOtprema … (+6) |
| updRadniNalogInterniSkladistarPotvrdio | izmena | 14 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmCerada, frmDrumski, frmDrumski1, frmKapijaOtvoreniNalozi, frmPlatforma, frmPlatformaOtprema … (+6) |
| updRadniNalogInterniSkladistePotvrdjen | izmena | 14 | PakovanjeKamiona1, PakovanjeKamionaCerade, frmCerada, frmDrumski, frmDrumski1, frmKapijaOtvoreniNalozi, frmPlatforma, frmPlatformaOtprema … (+6) |
| updRN4PrPlatformeKamIzvozPusti | izmena | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| updTerminalPriv | izmena | 3 | frmTerminalPrivremeni, frmTerminalPrivremeniExcel, frmTerminalPrivremeniSlike |
| VratiIzPlanaUtovaraSelektovano | ostalo | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| VratiKontejnerNerasporedjenoSelektovanoIzvoz | ostalo | 8 | frmDopunaPlanaPraznimIzvoz, frmFormiranjePlanaIzvoz, frmGrupniUnosPoljaIzvoz, frmIzvozKonacna, frmOtpremaKontejneraKamionomIzKontejnera, frmOtpremaVozaIzPlana, frmPovezivanjeKontejneraIVagona, frmRadniNalogInterniPregled |
| VratiPredvidjanjeStatus | ostalo | 1 | frmPartnerji |
| ZatvoriSkladisninuKontejnera | ostalo | 15 | Pregledac, Prijemnica, RN12MedjuskladisniKontejnera, RN1PrijemVoza, RN2OtpremaVoza, RN4PrijemPlatforme, RN5PrijemPlatforme2, RN6OtpremaPlatforme … (+7) |
| ZavrsiDodatnuUsluguIzvozZadata | ostalo | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |
| ZavrsiDodatnuUsluguUvozZadata | ostalo | 11 | UvozNHM, frmDodatneUsluge, frmFormiranjePlana, frmKapijaOtvoreniNalozi, frmPretovarPrijem, frmPrijemKamionaIzPlana, frmPrijemVozaIzPlana, frmRadniNalogInterniPregled … (+3) |

## 6. Forme: tabele i funkcionalnosti

### Administracija (5)

#### Administracija1
- **Fajl:** `Administracija/Administracija1.cs`  
- **Naslov forme:** „Administracija“
- **Otvara forme:** AdministracijaPravoPristupa („Prava“), frmKorisnici („Korisnici“), frmPodesavanja („Podešavanja“)
- **Funkcionalnosti (kontrola → handler):**
  - „Korisnici“ — klik → `btnKorisnici_Click` — otvara: frmKorisnici
  - „Prava“ — klik → `btnPrava_Click` — otvara: AdministracijaPravoPristupa
  - „Podešavanja“ — klik → `btnPodesavanja_Click` — otvara: frmPodesavanja

#### AdministracijaPravoPristupa
- **Fajl:** `MainLeget/AdministracijaPravoPristupa.cs`  
- **Naslov forme:** „AdministracijaPravoPristupa“
- **Modul:** Administracija, Dugme „!!!“ (samo korisnik test)
- **Tabele:**
  - `Korisnici` — S
  - `MainNovi` — S
  - `mainNoviKartice` — SID
  - `MainNoviPrava` — SID
- **Funkcionalnosti (kontrola → handler):**
  - „Dodeli pravo korisniku“ — klik → `button1_Click` — piše: MainNoviPrava[DI]
  - `miDodaj` — klik → `MiDodajFormu_Click` — piše: MainNoviPrava[DI], mainNoviKartice[DI]; čita: Korisnici, MainNovi; preko: NodeInfo
  - `miObrisi` — klik → `MiObrisiFormu_Click` — piše: MainNoviPrava[D], mainNoviKartice[D]

#### frmHRStavka
- **Fajl:** `HR/frmHRStavka.cs`
- **Modul:** Administracija, HR modul
- **Tabele:**
  - `Drzave` — S (preko HRVrednosti)
  - `Opstine` — S (preko HRVrednosti)
  - `OrganizacioneJedinice` — S (preko HRVrednosti)
  - `Partnerji` — S (preko HRVrednosti)
  - `Stanice` — S (preko HRVrednosti)
  - `Valute` — S (preko HRVrednosti)
  - `Zaposleni` — S (preko HRVrednosti)

#### frmKorisnici
- **Fajl:** `Administracija/frmKorisnici.cs`  
- **Naslov forme:** „Korisnici“
- **Tabele:**
  - `Delavci` — S
  - `Korisnici` — S
- **Procedure:** `DeleteKorisnici`, `DeletePravo`, `InsertKorisnici`, `InsertPravo`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: DeleteKorisnici, DeletePravo, InsertKorisnici, InsertPravo; preko: InsertKorisnici
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteKorisnici, DeletePravo, InsertKorisnici, InsertPravo; preko: InsertKorisnici

#### frmPodesavanja
- **Fajl:** `Administracija/frmPodesavanja.cs`
- **Otvara forme:** frmHRStavka (metoda `Izmeni`)
- **Tabele:**
  - `Drzave` — S (preko HRPolje, HRVrednosti)
  - `Opstine` — S (preko HRPolje, HRVrednosti)
  - `OrganizacioneJedinice` — S (preko HRPolje, HRVrednosti)
  - `Partnerji` — S (preko HRPolje, HRVrednosti)
  - `Podesavanja` — SU
  - `Stanice` — S (preko HRPolje, HRVrednosti)
  - `Valute` — S (preko HRPolje, HRVrednosti)
  - `Zaposleni` — S (preko HRPolje, HRVrednosti)
- **Procedure:** `sp_set_session_context`

### Operacije skladišta (32)

#### CarinskoSkladiste
- **Fajl:** `Skladista main/CarinskoSkladiste.cs`  
- **Naslov forme:** „CarinskoSkladiste“
- **Otvara forme:** CarinskoSkladistePregled („Prijem robe“), MagacinskaKnjigaCarinska („Magacinska knjiga“), PregledLagera („Pregled lagera robe“)
- **Funkcionalnosti (kontrola → handler):**
  - „Pretovar robe“ — klik → `btnPretovarRobe_Click` — otvara: CarinskoSkladistePregled
  - „Pregled kartica po klijentu“ — klik → `btnPregledKartica_Click` — otvara: PregledLagera
  - „Pregled lagera robe“ — klik → `btnPregledLagera_Click` — otvara: PregledLagera
  - „Magacinska knjiga“ — klik → `btnMagacinskaKnjiga_Click` — otvara: MagacinskaKnjigaCarinska
  - „Otprema robe“ — klik → `btnOtpremaRobe_Click` — otvara: CarinskoSkladistePregled
  - „Prijem robe“ — klik → `btnPrijemRobe_Click` — otvara: CarinskoSkladistePregled

#### CarinskoSkladistePregled
- **Fajl:** `Skladista main/CarinskoSkladistePregled.cs`  
- **Naslov forme:** „CarinskoSkladistePregled“
- **Otvara forme:** frmIzvestajCarinskoObelezje („Štampaj“), frmSeniranjeDokumenata („Skeniraj“), Prijemnica („Prijemnica“), ViljuskaristiPregled („Radni nalozi“)
- **Tabele:**
  - `Partnerji` — S
  - `radninaloginterni` — S
  - `RadniNalogInterni` — S
  - `RadniNalogInterniPotvrda` — S
  - `RadniNalogSkladista` — S
- **Funkcionalnosti (kontrola → handler):**
  - „Skeniraj“ — klik → `button5_Click` — otvara: frmSeniranjeDokumenata
  - „Štampaj“ — klik → `button4_Click` — otvara: frmIzvestajCarinskoObelezje
  - „Radni nalozi“ — klik → `btnViljuskarista_Click` — otvara: ViljuskaristiPregled
  - „Prijemnica“ — klik → `button25_Click` — otvara: Prijemnica
  - „Magacinski predmet“ — klik → `button24_Click` — otvara: frmNalogPrijem, Otprema; čita: RadniNalogInterni

#### frmDefinisiPoziciju
- **Fajl:** `Tehnologija/frmDefinisiPoziciju.cs`  
- **Naslov forme:** „DEFINISANJE POZICIJE“
- **Modul:** Operacije skladišta, Prijem i otprema vozova
- **Tabele:**
  - `IzvozKonacna` — S
  - `Partnerji` — S
  - `PredefinisanePozicije` — S
  - `Skladista` — S
  - `TipKontenjera` — S
  - `uvKvalitetKontejnera` — S
- **Procedure:** `DeleteDefinisiPoziciju`, `InsertPredefinisanePozicije`, `UpdatePredefinisanePozicije`
- **Funkcionalnosti (kontrola → handler):**
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - „gridGroupingControl2“ — klik na ćeliju → `gridGroupingControl2_TableControlCellClick`
  - „SNIMI PRAZAN“ — klik → `toolStripButton1_Click` — proc: DeleteDefinisiPoziciju, InsertPredefinisanePozicije, UpdatePredefinisanePozicije; preko: InsertDefinisiPoziciju
  - „SNIMI PUN“ — klik → `toolStripButton3_Click` — proc: DeleteDefinisiPoziciju, InsertPredefinisanePozicije, UpdatePredefinisanePozicije; preko: InsertDefinisiPoziciju
  - „REFRESH TEKUCE“ — klik → `toolStripButton5_Click`
  - „Refresh“ — klik → `toolStripButton5_Click`
  - „Snimi pun“ — klik → `toolStripButton3_Click` — proc: DeleteDefinisiPoziciju, InsertPredefinisanePozicije, UpdatePredefinisanePozicije; preko: InsertDefinisiPoziciju
  - „Snimi Prazan“ — klik → `toolStripButton1_Click` — proc: DeleteDefinisiPoziciju, InsertPredefinisanePozicije, UpdatePredefinisanePozicije; preko: InsertDefinisiPoziciju
  - „Vrati sve“ — klik → `button1_Click`
  - `this` — otvaranje forme → `frmDefinisiPoziciju_Load` — čita: IzvozKonacna, Partnerji, Skladista, TipKontenjera, uvKvalitetKontejnera

#### frmDodatneUsluge
- **Fajl:** `Uvoz/frmDodatneUsluge.cs`  
- **Naslov forme:** „UNOS DODATNIH USLUGA“
- **Modul:** Operacije skladišta, Prijem i otprema kamiona, Prijem i otprema vozova
- **Otvara forme:** frmIzvozDokumenta („Dodeli slike“), UvozDokumenta („Dodeli slike“), frmIzvozDokumenta („Dodeli slike“), UvozDokumenta („Dodeli slike“), frmIzvozDokumenta („Dodeli slike“), UvozDokumenta („Dodeli slike“)
- **Tabele:**
  - `IzvozKonacna` — S
  - `KontejnerStatus` — S
  - `KontejnerTekuce` — S
  - `RadniNalogInterni` — S
  - `Skladista` — S
  - `TipKontenjera` — S
  - `UvozKonacna` — S
  - `Voz` — S
  - `VrstaManipulacije` — S
- **Procedure:** `DeleteUvozKonacna`, `DeleteUvozKonacnaNHM`, `DeleteUvozKonacnaNapomenePozicioniranja`, `DeleteUvozKonacnaVrstaManipulacije`, `DeleteUvozKonacnaVrstaManipulacijeKamionska`, `DeleteUvozKonacnaVrstaRobeHS`, `DeleteUvozNHM`, `DeleteUvozNHMDiana`, `DeleteUvozNapomenePozicioniranja`, `DeleteUvozTerminalskeManipulacije`, `DeleteUvozTerminalskeManipulacijeKonacna`, `DeleteUvozUslugaDokumenta`, `DeleteUvozUvoznici`, `DeleteUvozVrstaManTerminalskih`, `DeleteUvozVrstaManipulacije`, `DeleteUvozVrstaManipulacijeKamionska`, `DeleteUvozVrstaRobeHS`, `DeleteUvozkonacnaNHMDiana`, `InsUbaciDodatnuUsluguIzvoz`, `InsUbaciDodatnuUsluguUvoz`, `InsertUvozKonacbaVrstaRobeHS`, `InsertUvozKonacna`, `InsertUvozKonacnaNHM`, `InsertUvozKonacnaNHMDiana`, `InsertUvozKonacnaNapomenePozicioniranja`, `InsertUvozKonacnaVrstaManipulacije`, `InsertUvozNHM`, `InsertUvozNHMDiana`, `InsertUvozNapomenePozicioniranja`, `InsertUvozUslugaDokument`, `InsertUvozUvoznici`, `InsertUvozVrstaManipulacije`, `InsertUvozVrstaRobeHS`, `PrebaciNHMUvozUvozKonacna`, `PrebaciVrsterobeHSUvozUvozKonacna`, `PrenesiIzPlanUtovaraUPlanUtovara`, `PrenesiUPlanUtovaraSelektovano`, `PromeniSaPrijemniceUK`, `UbaciURadniNalogInterniBeyScenarija`, `UpdCenaUvozKonacnaVrstaManipulacije`, `UpdCenaUvozVrstaManipulacije`, `UpdPlatiocaUvozKonacnaVrstaManipulacije`, `UpdPlatiocaUvozVrstaManipulacije`, `UpdPokretFormaStatus`, `UpdateBrojKolaIzExcela`, `UpdateBrojKolaIzExcelaVoz`, `UpdateUvozKonacna`, `UpdateUvozKonacnaManFaktura`, `UpdateUvozKonacnaManZaFakturisanje`, `UpdateUvozKonacnaObjedinjenja`, `UpdateUvozKonacnaPK`, `UpdateUvozKonacnaUC`, `UpdateUvozNHMTezine`, `VratiIzPlanaUtovaraSelektovano`, `ZavrsiDodatnuUsluguIzvozZadata`, `ZavrsiDodatnuUsluguUvozZadata`, `spIzbrisiKUScenario`, `spPrenesiKontejnerIzPlanaNaPrijemnicu`, `spPrenesiKontejnerIzPlanaNaPrijemnicuIzvoz`, `spPrenesiKontejnerIzPlanaNaPrijemnicuIzvozPoIdNadredjenog`, `spPrenesiPlanUtovaraUPrijemPlatforma`, `spPrenesiPlanUtovaraUPrijemPlatformaIzvoz`, `spPrenesiPlanUtovaraUPrijemVoz`, `spStornijaRNI`
- **Funkcionalnosti (kontrola → handler):**
  - „Dodeli slike“ — klik → `button1_Click_1` — otvara: frmIzvozDokumenta, UvozDokumenta
  - „Sačuvaj promenu“ — klik → `button4_Click` — proc: DeleteUvozKonacna, DeleteUvozKonacnaNHM, DeleteUvozKonacnaNapomenePozicioniranja, DeleteUvozKonacnaVrstaManipulacije, DeleteUvozKonacnaVrstaManipulacijeKamionska, DeleteUvozKonacnaVrstaRobeHS; preko: InsertUvozKonacna
  - „Pozovi“ — klik → `button3_Click` — čita 8 tabela
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - `this` — otvaranje forme → `frmDodatneUsluge_Load` — čita: Voz, VrstaManipulacije

#### frmIzvestajCarinskoObelezje
- **Fajl:** `Skladista/frmIzvestajCarinskoObelezje.cs`  
- **Naslov forme:** „Carinsko obeležje“

#### frmMestoIsporuke
- **Fajl:** `Skladista main/frmMestoIsporuke.cs`  
- **Naslov forme:** „frmMestoIsporuke“
- **Tabele:**
  - `MestaUtovara` — S
  - `partnerjiKontOsebaMU` — S
  - `RadniNalogInterni` — S
  - `RadniNalogInterniPotvrda` — S
  - `RadniNalogSkladista` — S
  - `VlasniciRobe` — S
- **Procedure:** `DeleteMagacinskiBrojCarinski`, `DeleteRadniNalogSkladisteDirektni`, `DeleteRnCarinskoSkladisteDodatneUsluge`, `InsertMagacinskiBrojCarinski`, `InsertMagacinskiPredmet`, `InsertRNCarinskoPrijemnicaStavke`, `InsertRNCarinskoRukovalac`, `InsertRNCarinskoSkladiste`, `InsertRNCarinskoSkladistePrijemnica`, `InsertRNInterniSkladisteDodatneUsluge`, `InsertRadniNalogSkladista`, `InsertRadniNalogSkladistaDirektni`, `InsertRnCarinskoSkladisteDodatneUsluge`, `UpdateMagacinskiBrojCarinski`, `UpdateMagacinskiPredmet`, `UpdateRNCarinskoSkladiste`, `UpdateRNCarinskoSkladistePrijemnica`, `UpdateRadniNalogDirektniIstovar`, `UpdateRadniNalogDirektniVozilo`, `UpdateRadniNalogSkladista`, `UpdateRadniNalogSkladistaDirektni`
- **Funkcionalnosti (kontrola → handler):**
  - „Snimi“ — klik → `btnSnimi_Click` — proc: DeleteMagacinskiBrojCarinski, DeleteRadniNalogSkladisteDirektni, DeleteRnCarinskoSkladisteDodatneUsluge, InsertMagacinskiBrojCarinski, InsertMagacinskiPredmet, InsertRNCarinskoPrijemnicaStavke; preko: InsertCarinskoSkladiste

#### frmNalogPrijem
- **Fajl:** `Skladista main/frmNalogPrijem.Ispravke.cs`  
- **Naslov forme:** „frmNalogPrijem“
- **Otvara forme:** frmMestoIsporuke („Mesto Istovara“), frmTipPalete („?“), frmVozac („Vozač“)
- **Tabele:**
  - `Carinarnice` — S
  - `InspekciskiTretman` — S
  - `MagacinskiBrojCarinski` — S
  - `MagacinskiPredmet` — S
  - `MerskeEnote` — S
  - `Partnerji` — S
  - `partnerjiKontOseba` — S
  - `PosebniUsoviSmestaja` — S
  - `RadniNalogInterni` — S
  - `RadniNalogInterniPotvrda` — S
  - `RadniNalogSkladista` — S
  - `RNCarinskoStavke` — S
  - `TipPalete` — S
  - `uvNacinPakovanja` — S
  - `Valute` — S
  - `VlasniciRobe` — S
  - `VrstaCarinskogPostupka` — S
  - `VrstaManipulacije` — S
  - `VrstaRobeADR` — S
  - `VrstePrevoznogSredstva` — S
- **Procedure:** `AktivirajSkladista`, `DeAktivirajSkladista`, `DeleteCarinskoStavke`, `DeleteMagacinskiBrojCarinski`, `DeleteRadniNalogSkladisteDirektni`, `DeleteRnCarinskoSkladisteDodatneUsluge`, `DeleteSkladista`, `DeleteUvoz`, `DodajStavkeURadniNalog`, `InsertCarinskoStavke`, `InsertMagacinskiBrojCarinski`, `InsertMagacinskiPredmet`, `InsertPrijemnicaPalete`, `InsertRNCarinskoPrijemnicaStavke`, `InsertRNCarinskoRukovalac`, `InsertRNCarinskoSkladiste`, `InsertRNCarinskoSkladisteOtpremnica`, `InsertRNCarinskoSkladistePrijemnica`, `InsertRNInterniSkladisteDodatneUsluge`, `InsertRadniNalogInterniSkladista`, `InsertRadniNalogSkladista`, `InsertRadniNalogSkladistaDirektni`, `InsertRnCarinskoSkladisteDodatneUsluge`, `InsertRnCarinskoSkladisteOtpremaStavkePom`, `InsertSkladista`, `KopirajKontejner`, `KopirajKontejnerIzKonacne`, `NapraviRadniNalogDrumski`, `StornoRadniNalogInterni`, `UpdCenaUvozKonacnaVrstaManipulacije`, `UpdCenaUvozVrstaManipulacije`, `UpdPDVUvozKonacnaVrstaManipulacije`, `UpdPDVUvozVrstaManipulacije`, `UpdateCarinskoStavke`, `UpdateMagacinskiBrojCarinski`, `UpdateMagacinskiPredmet`, `UpdateRNCarinskoSkladiste`, `UpdateRNCarinskoSkladisteOtpremnica`, `UpdateRNCarinskoSkladistePrijemnica`, `UpdateRNInterniSkladista`, `UpdateRadniNalogDirektniIstovar`, `UpdateRadniNalogDirektniVozilo`, `UpdateRadniNalogSkladista`, `UpdateRadniNalogSkladistaDirektni`, `UpdateSkladista`, `UpdateUvoz`
- **Funkcionalnosti (kontrola → handler):**
  - „Izbriši“ — klik → `btnDelete_Click` — proc: DeleteMagacinskiBrojCarinski, DeleteRadniNalogSkladisteDirektni, DeleteRnCarinskoSkladisteDodatneUsluge, InsertMagacinskiBrojCarinski, InsertMagacinskiPredmet, InsertRNCarinskoPrijemnicaStavke; preko: InsertCarinskoSkladiste
  - „Pregled“ — klik → `btnPregled_Click`
  - „Potvrdi: LO nalog“ — klik → `button25_Click`
  - „Mesto Istovara“ — klik → `button2_Click` — otvara: frmMestoIsporuke
  - „Vozač“ — klik → `button1_Click` — otvara: frmVozac
  - „Snimi“ — klik → `button24_Click` — proc: AktivirajSkladista, DeAktivirajSkladista, DeleteCarinskoStavke, DeleteMagacinskiBrojCarinski, DeleteRadniNalogSkladisteDirektni, DeleteRnCarinskoSkladisteDodatneUsluge; preko: InsertCarinskoSkladiste, InsertSkladista, InsertUvoz
  - „?“ — klik → `btnMagacinskiBroj_Click`
  - „PLANIRANI \r\nDATUM/VREME REALIZACIJE“ — klik → `label18_Click`
  - „NAZAD“ — klik → `btnMbNazad_Click`
  - „SNIMI“ — klik → `btnMbSave_Click` — čita: MagacinskiBrojCarinski; proc: DeleteMagacinskiBrojCarinski, DeleteRadniNalogSkladisteDirektni, DeleteRnCarinskoSkladisteDodatneUsluge, InsertMagacinskiBrojCarinski, InsertMagacinskiPredmet, InsertRNCarinskoPrijemnicaStavke; preko: InsertCarinskoSkladiste
  - „Izbaci“ — klik → `btnIzbaci_Click`
  - „Dodaj“ — klik → `btnDodaj_Click`
  - „gridGroupingControl1“ — TableControlCurrentCellControlDoubleClick → `gridGroupingControl1_TableControlCurrentCellControlDoubleClick`
  - „Izbriši“ — klik → `button6_Click` — proc: AktivirajSkladista, DeAktivirajSkladista, DeleteCarinskoStavke, DeleteMagacinskiBrojCarinski, DeleteRnCarinskoSkladisteDodatneUsluge, DeleteSkladista; preko: InsertSkladista
  - „Izmeni“ — klik → `btnIzmeniStavku_Click` — proc: AktivirajSkladista, DeAktivirajSkladista, DeleteCarinskoStavke, DeleteMagacinskiBrojCarinski, DeleteRnCarinskoSkladisteDodatneUsluge, DeleteSkladista; preko: InsertSkladista
  - „Dodaj“ — klik → `btnDodajStavku_Click` — proc: AktivirajSkladista, DeAktivirajSkladista, DeleteCarinskoStavke, DeleteMagacinskiBrojCarinski, DeleteRnCarinskoSkladisteDodatneUsluge, DeleteSkladista; preko: InsertSkladista
  - „?“ — klik → `btnPaleta_Click` — otvara: frmTipPalete
  - „gridGroupingControl1“ — QueryCustomSummary → `GridGroupingControl1_QueryCustomSummary`

#### frmRN12InterniPrenos
- **Fajl:** `TerminalMap/frmRN12InterniPrenos.cs`  
- **Naslov forme:** „RN12 - MEdjuskladisni interni prenos“
- **Modul:** Operacije skladišta, Prijem i otprema vozova
- **Tabele:**
  - `KontejnerStatus` — S
  - `KontejnerTekuce` — S
  - `PArtnerji` — S
  - `Pozicija` — S
  - `PredefinisanePozicije` — S
  - `Skladista` — S
  - `SkladistaGrupa` — S
  - `TipKontenjera` — S
  - `uvKvalitetKontejnera` — S
  - `Zone` — S
- **Procedure:** `DeleteRNMedjuskladisni`, `DeleteRNOtpremaCirade`, `DeleteRNOtpremaPlatforme`, `DeleteRNOtpremaPlatforme2`, `DeleteRNOtpremaVoza`, `DeleteRNPregledIpostavkaKontejnera`, `DeleteRNPregledIspraznjenogKontejnera`, `DeleteRNPrijemCirade`, `DeleteRNPrijemPlatforme`, `DeleteRNPrijemPlatforme2`, `DeleteRNPrijemVoza`, `DeleteRNPrijemVoza2`, `InsertRN10PregledCeoVoz`, `InsertRN11PregledCeoVozOtprema`, `InsertRN12Medjuskladisni`, `InsertRN4PrPlatformeKamIzvoz`, `InsertRN4PrPlatformeKamUvoz`, `InsertRN5PrPlatFormeKam`, `InsertRN6OtPlatFormeKam`, `InsertRN7OtpremaOtpremaPlatKam`, `InsertRN8OtpremaCiradeKam`, `InsertRN8OtpremaCiradeKamIzvoz`, `InsertRN9PrijemCiradeKam`, `InsertRNOtpremaCirade`, `InsertRNOtpremaPlatforme`, `InsertRNOtpremaPlatforme2`, `InsertRNOtpremaVoza`, `InsertRNOtpremaVozaCeoVoz`, `InsertRNPregledIpostavkaKontejnera`, `InsertRNPregledIspraznjenogKontejnera`, `InsertRNPrijemCirade`, `InsertRNPrijemPlatforme`, `InsertRNPrijemPlatforme2`, `InsertRNPrijemVoza`, `InsertRNPrijemVoza2`, `InsertRNPrijemVozaCeoVoz`, `InsertRNPrijemVozaCeoVozPretovar`, `PokreniSkladisninuKontejnera`, `PrenesiKontejnerTekuceUArhiv`, `UpdateIZCIRA`, `UpdateRN12MedjuskladisniSkladiste`, `UpdateRN12Uradjene`, `UpdateRN1Uradjene`, `UpdateRN1UradjeneCIR`, `UpdateRN1UradjeneS`, `UpdateRN1UradjeneVP`, `UpdateRN2Uradjene`, `UpdateRN3Uradjene`, `UpdateRN4PotrebanCIR`, `UpdateRN4PrivremenoSkladiste`, `UpdateRN4SkladisteIzCira`, `UpdateRN4Uradjene`, `UpdateRN4UradjeneCIR`, `UpdateRN4UradjenePostavljen`, `UpdateRN4UradjeneVizuelni`, `UpdateRN5PrijemSkladiste`, `UpdateRN5Uradjene`, `UpdateRN5UradjeneCIR`, `UpdateRN5UradjeneS`, `UpdateRN6OtpremaSkladiste`, `UpdateRN6PotrebanCIR`, `UpdateRN6Uradjene`, `UpdateRN6UradjeneVizuelni`, `UpdateRN7Uradjene`, `UpdateRN8Uradjene`, `UpdateRN8UradjeneCIR`, `UpdateRN9PretovarCIRADE`, `UpdateRN9Uradjen`, `UpdateRNMedjuskladisni`, `UpdateRNOtpremaCirade`, `UpdateRNOtpremaPlatforme`, `UpdateRNOtpremaPlatforme2`, `UpdateRNOtpremaVoza`, `UpdateRNPregledIpostavkaKontejnera`, `UpdateRNPregledIspraznjenogKontejnera`, `UpdateRNPrijemCirade`, `UpdateRNPrijemPlatforme`, `UpdateRNPrijemPlatforme2`, `UpdateRNPrijemVoza`, `UpdateRNPrijemVoza2`, `UpdateRNPrijemVozaSkladiste`, `ZatvoriSkladisninuKontejnera`, `updRN4PrPlatformeKamIzvozPusti`
- **Funkcionalnosti (kontrola → handler):**
  - „Sva polja“ — klik → `button4_Click` — čita: Skladista
  - „Predefinisana polja“ — klik → `button1_Click` — čita: PredefinisanePozicije, Skladista
  - „Formiraj interni prenos“ — klik → `button2_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - `this` — otvaranje forme → `frmRN12InterniPrenos_Load` — čita: Skladista

#### frmSeniranjeDokumenata
- **Fajl:** `Drumski/frmSeniranjeDokumenata.cs`  
- **Naslov forme:** „Skeniranje dokumenata“
- **Tabele:**
  - `Delavci` — S
  - `DokumentaFaktureDrumski` — S
  - `DokumentaSkladiste` — S
  - `Korisnici` — S
  - `RadniNalogDrumski` — S
- **Procedure:** `DeleteDokumentFaktureDrumski`, `DeleteDokumentKamionaDrumski`, `DeleteDokumentVozaca`, `DeleteFakturaDrumskiStavka`, `InsertDokumentaFaktureDrumski`, `InsertDokumentaSkladiste`, `InsertDokumentaUploadedFiles`, `InsertFakturaDrumski`, `InsertFakturaDrumskiStavka`, `UpdateDokumentaFaktureDrumski`, `UpdateFakturaDrumskiStavka`, `UpdateStatusFajla`, `UpdateUlaznaFakturaStavka`
- **Funkcionalnosti (kontrola → handler):**
  - „Osveži listu“ — klik → `BtnRefreshScanners_Click`
  - „Započni skeniranje“ — klik → `BtnScan_Click`
  - „Započni izmenu“ — klik → `btnEditImage_Click`
  - „Obriši stranu“ — klik → `BtnDelete_Click`
  - „Snimi kao fakturu“ — klik → `BtnSaveFaktura_Click` — proc: DeleteDokumentFaktureDrumski, DeleteDokumentKamionaDrumski, DeleteDokumentVozaca, DeleteFakturaDrumskiStavka, InsertDokumentaFaktureDrumski, InsertDokumentaSkladiste; preko: InsertFakture
  - „Snimi kao prevoznički dokument“ — klik → `BtnSaveTransport_Click`

#### frmTipPalete
- **Fajl:** `TrackModal/Sifarnici/frmTipPalete.cs`  
- **Naslov forme:** „Tip palete“
- **Tabele:**
  - `TipPalete` — S
- **Procedure:** `DeleteTipPalete`, `InsertTipPalete`, `UpdateTipPalete`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: DeleteTipPalete, InsertTipPalete, UpdateTipPalete; preko: InsertTipPalete
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteTipPalete, InsertTipPalete, UpdateTipPalete; preko: InsertTipPalete
  - `button20` — klik → `tsDelete_Click` — proc: DeleteTipPalete, InsertTipPalete, UpdateTipPalete; preko: InsertTipPalete
  - `button21` — klik → `tsSave_Click` — proc: DeleteTipPalete, InsertTipPalete, UpdateTipPalete; preko: InsertTipPalete

#### frmVozac
- **Fajl:** `Skladista main/frmVozac.cs`  
- **Naslov forme:** „frmVozac“
- **Tabele:**
  - `RadniNalogInterni` — S
  - `RadniNalogInterniPotvrda` — S
  - `RadniNalogSkladista` — S
  - `VrstaVozila` — S
- **Procedure:** `DeleteMagacinskiBrojCarinski`, `DeleteRadniNalogSkladisteDirektni`, `DeleteRnCarinskoSkladisteDodatneUsluge`, `InsertMagacinskiBrojCarinski`, `InsertMagacinskiPredmet`, `InsertRNCarinskoPrijemnicaStavke`, `InsertRNCarinskoRukovalac`, `InsertRNCarinskoSkladiste`, `InsertRNCarinskoSkladistePrijemnica`, `InsertRNInterniSkladisteDodatneUsluge`, `InsertRadniNalogSkladista`, `InsertRadniNalogSkladistaDirektni`, `InsertRnCarinskoSkladisteDodatneUsluge`, `UpdateMagacinskiBrojCarinski`, `UpdateMagacinskiPredmet`, `UpdateRNCarinskoSkladiste`, `UpdateRNCarinskoSkladistePrijemnica`, `UpdateRadniNalogDirektniIstovar`, `UpdateRadniNalogDirektniVozilo`, `UpdateRadniNalogSkladista`, `UpdateRadniNalogSkladistaDirektni`
- **Funkcionalnosti (kontrola → handler):**
  - „Snimi“ — klik → `btnSnimi_Click` — proc: DeleteMagacinskiBrojCarinski, DeleteRadniNalogSkladisteDirektni, DeleteRnCarinskoSkladisteDodatneUsluge, InsertMagacinskiBrojCarinski, InsertMagacinskiPredmet, InsertRNCarinskoPrijemnicaStavke; preko: InsertCarinskoSkladiste

#### InterniPoslovi
- **Fajl:** `Skladista main/Dokumenta/InterniPoslovi.cs`  
- **Naslov forme:** „InterniPoslovi“
- **Tabele:**
  - `Carinarnice` — S
  - `MagacinskiBrojCarinski` — S
  - `MestaUtovara` — S
  - `Partnerji` — S
  - `RadniNalogSkladista` — S
  - `RNCarinskoSkladisteDodatneUsluge` — S
  - `VrstaCarinskogPostupka` — S
  - `VrstaManipulacije` — S
  - `VrstaVozila` — S
  - `VrstePrevoznogSredstva` — S
- **Procedure:** `AktivirajSkladista`, `DeAktivirajSkladista`, `DeleteCarinskoStavke`, `DeleteMagacinskiBrojCarinski`, `DeleteRnCarinskoSkladisteDodatneUsluge`, `DeleteSkladista`, `InsertCarinskoStavke`, `InsertMagacinskiBrojCarinski`, `InsertPrijemnicaPalete`, `InsertRNCarinskoPrijemnicaStavke`, `InsertRNCarinskoRukovalac`, `InsertRNCarinskoSkladisteOtpremnica`, `InsertRNCarinskoSkladistePrijemnica`, `InsertRadniNalogInterniSkladista`, `InsertRadniNalogSkladista`, `InsertRnCarinskoSkladisteDodatneUsluge`, `InsertRnCarinskoSkladisteOtpremaStavkePom`, `InsertSkladista`, `StornoRadniNalogInterni`, `UpdateCarinskoStavke`, `UpdateMagacinskiBrojCarinski`, `UpdateRNCarinskoSkladisteOtpremnica`, `UpdateRNCarinskoSkladistePrijemnica`, `UpdateRNInterniSkladista`, `UpdateRadniNalogSkladista`, `UpdateSkladista`
- **Funkcionalnosti (kontrola → handler):**
  - „Izbaci“ — klik → `btnIzbaci_Click`
  - „Nazad“ — klik → `btnMbNazad_Click`
  - „Sačuvaj“ — klik → `btnMbSave_Click` — čita: MagacinskiBrojCarinski; proc: AktivirajSkladista, DeAktivirajSkladista, DeleteCarinskoStavke, DeleteMagacinskiBrojCarinski, DeleteRnCarinskoSkladisteDodatneUsluge, DeleteSkladista; preko: InsertSkladista
  - „Dodaj“ — klik → `btnDodaj_Click`
  - „FORMIRAN RN“ — klik → `btnFormiranRN_Click` — čita: RNCarinskoSkladisteDodatneUsluge; proc: AktivirajSkladista, DeAktivirajSkladista, DeleteCarinskoStavke, DeleteMagacinskiBrojCarinski, DeleteRnCarinskoSkladisteDodatneUsluge, DeleteSkladista; preko: InsertSkladista
  - „SNIMI“ — klik → `btnSnimi_Click` — čita: RNCarinskoSkladisteDodatneUsluge, RadniNalogSkladista; proc: AktivirajSkladista, DeAktivirajSkladista, DeleteCarinskoStavke, DeleteMagacinskiBrojCarinski, DeleteRnCarinskoSkladisteDodatneUsluge, DeleteSkladista; preko: InsertSkladista
  - `cboVlasnikRobe` — SelectionChangeCommitted → `cboVlasnikRobe_SelectionChangeCommitted` — čita: Partnerji
  - „?“ — klik → `btnMagacinskiBroj_Click`

#### InterniPrenos
- **Fajl:** `Skladista main/Dokumenta/InterniPrenos.cs`  
- **Naslov forme:** „InterniPrenos“
- **Tabele:**
  - `Carinarnice` — S
  - `MagacinskiBrojCarinski` — S
  - `MestaUtovara` — S
  - `Partnerji` — S
  - `RadniNalogSkladista` — S
  - `RNCarinskoSkladisteDodatneUsluge` — S
  - `VrstaCarinskogPostupka` — S
  - `VrstaManipulacije` — S
  - `VrstaVozila` — S
  - `VrstePrevoznogSredstva` — S
- **Procedure:** `AktivirajSkladista`, `DeAktivirajSkladista`, `DeleteCarinskoStavke`, `DeleteMagacinskiBrojCarinski`, `DeleteRnCarinskoSkladisteDodatneUsluge`, `DeleteSkladista`, `InsertCarinskoStavke`, `InsertMagacinskiBrojCarinski`, `InsertPrijemnicaPalete`, `InsertRNCarinskoPrijemnicaStavke`, `InsertRNCarinskoRukovalac`, `InsertRNCarinskoSkladisteOtpremnica`, `InsertRNCarinskoSkladistePrijemnica`, `InsertRadniNalogInterniSkladista`, `InsertRadniNalogSkladista`, `InsertRnCarinskoSkladisteDodatneUsluge`, `InsertRnCarinskoSkladisteOtpremaStavkePom`, `InsertSkladista`, `StornoRadniNalogInterni`, `UpdateCarinskoStavke`, `UpdateMagacinskiBrojCarinski`, `UpdateRNCarinskoSkladisteOtpremnica`, `UpdateRNCarinskoSkladistePrijemnica`, `UpdateRNInterniSkladista`, `UpdateRadniNalogSkladista`, `UpdateSkladista`
- **Funkcionalnosti (kontrola → handler):**
  - „Izbaci“ — klik → `btnIzbaci_Click`
  - „Nazad“ — klik → `btnMbNazad_Click`
  - „Sačuvaj“ — klik → `btnMbSave_Click` — čita: MagacinskiBrojCarinski; proc: AktivirajSkladista, DeAktivirajSkladista, DeleteCarinskoStavke, DeleteMagacinskiBrojCarinski, DeleteRnCarinskoSkladisteDodatneUsluge, DeleteSkladista; preko: InsertSkladista
  - „Dodaj“ — klik → `btnDodaj_Click`
  - „FORMIRAN RN“ — klik → `btnFormiranRN_Click` — čita: RNCarinskoSkladisteDodatneUsluge; proc: AktivirajSkladista, DeAktivirajSkladista, DeleteCarinskoStavke, DeleteMagacinskiBrojCarinski, DeleteRnCarinskoSkladisteDodatneUsluge, DeleteSkladista; preko: InsertSkladista
  - „SNIMI“ — klik → `btnSnimi_Click` — čita: RNCarinskoSkladisteDodatneUsluge, RadniNalogSkladista; proc: AktivirajSkladista, DeAktivirajSkladista, DeleteCarinskoStavke, DeleteMagacinskiBrojCarinski, DeleteRnCarinskoSkladisteDodatneUsluge, DeleteSkladista; preko: InsertSkladista
  - `cboVlasnikRobe` — SelectionChangeCommitted → `cboVlasnikRobe_SelectionChangeCommitted` — čita: Partnerji
  - „?“ — klik → `btnMagacinskiBroj_Click`

#### KomercijalnoSkladiste
- **Fajl:** `Skladista main/KomercijalnoSkladiste.cs`  
- **Naslov forme:** „CarinskoSkladiste“
- **Otvara forme:** KomercijalnoSkladistePregled („Prijem robe“)
- **Funkcionalnosti (kontrola → handler):**
  - „Prijem robe“ — klik → `btnPrijemRobe_Click` — otvara: KomercijalnoSkladistePregled

#### KomercijalnoSkladistePregled
- **Fajl:** `Skladista main/KomercijalnoSkladistePregled.cs`  
- **Naslov forme:** „KomercijalnoSkladistePregled“
- **Tabele:**
  - `Partnerji` — S
  - `radninaloginterni` — S
  - `RadniNalogInterni` — S
  - `RadniNalogInterniPotvrda` — S
  - `RadniNalogSkladista` — S
- **Funkcionalnosti (kontrola → handler):**
  - „Prijemnica“ — klik → `button25_Click` — otvara: Prijemnica
  - „Magacinski predmet“ — klik → `button24_Click` — otvara: frmNalogPrijem, Otprema; čita: RadniNalogInterni

#### MagacinskaKnjigaCarinska
- **Fajl:** `Skladista main/MagacinskaKnjigaCarinska.cs`  
- **Naslov forme:** „MagacinskaKnjigaCarinska“
- **Tabele:**
  - `MagacinskaKnjigaCarinska` — S

#### NalogZaKomercijalni
- **Fajl:** `Skladista main/NalogZaKomercijalni.cs`  
- **Naslov forme:** „NalogZaKomercijalni“
- **Otvara forme:** PregledKomercijalnihNaloga („SKLADIŠNI NALOZI UVOZ“), TipSkladista („Kreiraj novi“)
- **Funkcionalnosti (kontrola → handler):**
  - „SKLADIŠNI NALOZI DIREKTNI (3P)“ — klik → `btnTransportDirektni_Click` — otvara: PregledKomercijalnihNaloga
  - „SKLADIŠNI NALOZI IZVOZ“ — klik → `btnTransportIzvoz_Click` — otvara: PregledKomercijalnihNaloga
  - „SKLADIŠNI NALOZI UVOZ“ — klik → `btnTransportUvoz_Click` — otvara: PregledKomercijalnihNaloga
  - „Kreiraj novi“ — klik → `btnNovi_Click` — otvara: TipSkladista

#### Otprema
- **Fajl:** `Skladista main/Dokumenta/Otprema.cs`  
- **Naslov forme:** „Otprema“
- **Otvara forme:** OtpremaLager („IZABERI SA LAGERA“), Otpremnica („IDI NA OTPREMNICU“)
- **Tabele:**
  - `Carinarnice` — S
  - `Lager` — S
  - `MagacinskiBrojCarinski` — S
  - `MestaUtovara` — S
  - `Partnerji` — S
  - `PomOstali` — S
  - `Primljeno` — S
  - `Promet` — S
  - `RadniNalogSkladista` — S
  - `Rezervisano` — S (preko OtpremaStavke)
  - `RNCarinskoPrijemnicaStavke` — S
  - `RNCarinskoSkladisteDodatneUsluge` — S
  - `RNCarinskoSkladisteOtpremaStavkePom` — SD
  - `RNCarinskoSkladisteOtpremnica` — S
  - `RNCarinskoSkladisteOtpremnicaStavke` — S
  - `RNCarinskoSkladistePrijemnica` — S
  - `Skladista` — S
  - `Stock` — S (preko OtpremaStavke)
  - `TipPalete` — S
  - `VrstaCarinskogPostupka` — S
  - `VrstaManipulacije` — S
  - `VrstaVozila` — S
  - `VrstePrevoznogSredstva` — S
  - `Zavrseno` — S
- **Procedure:** `AktivirajSkladista`, `DeAktivirajSkladista`, `DeleteCarinskoStavke`, `DeleteMagacinskiBrojCarinski`, `DeleteRnCarinskoSkladisteDodatneUsluge`, `DeleteSkladista`, `InsertCarinskoStavke`, `InsertMagacinskiBrojCarinski`, `InsertPrijemnicaPalete`, `InsertRNCarinskoPrijemnicaStavke`, `InsertRNCarinskoRukovalac`, `InsertRNCarinskoSkladisteOtpremnica`, `InsertRNCarinskoSkladistePrijemnica`, `InsertRadniNalogInterniSkladista`, `InsertRadniNalogSkladista`, `InsertRnCarinskoSkladisteDodatneUsluge`, `InsertRnCarinskoSkladisteOtpremaStavkePom`, `InsertSkladista`, `StornoRadniNalogInterni`, `UpdateCarinskoStavke`, `UpdateMagacinskiBrojCarinski`, `UpdateRNCarinskoSkladisteOtpremnica`, `UpdateRNCarinskoSkladistePrijemnica`, `UpdateRNInterniSkladista`, `UpdateRadniNalogSkladista`, `UpdateSkladista`
- **Funkcionalnosti (kontrola → handler):**
  - „Izbaci“ — klik → `btnIzbaci_Click`
  - „Nazad“ — klik → `btnMbNazad_Click`
  - „Sačuvaj“ — klik → `btnMbSave_Click` — čita: MagacinskiBrojCarinski; proc: AktivirajSkladista, DeAktivirajSkladista, DeleteCarinskoStavke, DeleteMagacinskiBrojCarinski, DeleteRnCarinskoSkladisteDodatneUsluge, DeleteSkladista; preko: InsertSkladista
  - „Dodaj“ — klik → `btnDodaj_Click`
  - „IDI NA OTPREMNICU“ — klik → `btnOtpremnica_Click` — otvara: Otpremnica
  - „FORMIRAN RN“ — klik → `btnFormiranRN_Click` — piše: RNCarinskoSkladisteOtpremaStavkePom[D]; čita: RNCarinskoSkladisteDodatneUsluge; proc: AktivirajSkladista, DeAktivirajSkladista, DeleteCarinskoStavke, DeleteMagacinskiBrojCarinski, DeleteRnCarinskoSkladisteDodatneUsluge, DeleteSkladista; preko: InsertSkladista
  - „SNIMI“ — klik → `btnSnimi_Click` — piše: RNCarinskoSkladisteOtpremaStavkePom[D]; čita: RNCarinskoSkladisteDodatneUsluge, RadniNalogSkladista; proc: AktivirajSkladista, DeAktivirajSkladista, DeleteCarinskoStavke, DeleteMagacinskiBrojCarinski, DeleteRnCarinskoSkladisteDodatneUsluge, DeleteSkladista; preko: InsertSkladista
  - `cboVlasnikRobe` — SelectionChangeCommitted → `cboVlasnikRobe_SelectionChangeCommitted` — čita: Partnerji
  - „?“ — klik → `btnMagacinskiBroj_Click`
  - `cboMagacinskiBroj` — SelectionChangeCommitted → `cboMagacinskiBroj_SelectionChangeCommitted` — čita: RNCarinskoSkladistePrijemnica, RadniNalogSkladista
  - „?“ — klik → `btnPrijemnica_Click`
  - „Nazad“ — klik → `btnPrijemnicaNazad_Click`
  - „Potvrdi“ — klik → `btnPotvrdiPrijemnicu_Click` — čita: RadniNalogSkladista
  - „gridGroupingControl1“ — klik → `gridGroupingControl1_Click`
  - „RAZDUŽI SVE“ — klik → `btnRazduzi_Click` — piše: RNCarinskoSkladisteOtpremaStavkePom[D]; čita 23 tabela; preko: OtpremaStavke
  - „IZABERI SA LAGERA“ — klik → `button1_Click`
  - „Izbaci“ — klik → `btnIzbaciStavku_Click`
  - „Izmeni“ — klik → `btnIzmeniStavku_Click`
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`

#### OtpremaLager
- **Fajl:** `Skladista main/OtpremaLager.cs`  
- **Naslov forme:** „OtpremaLager“
- **Tabele:**
  - `MagacinskiBrojCarinski` — S
  - `Partnerji` — S
  - `PomOstali` — S
  - `Primljeno` — S
  - `Promet` — S
  - `RadniNalogSkladista` — S
  - `RNCarinskoPrijemnicaStavke` — S
  - `RNCarinskoSkladisteOtpremaStavkePom` — S
  - `RNCarinskoSkladisteOtpremnica` — S
  - `RNCarinskoSkladisteOtpremnicaStavke` — S
  - `RNCarinskoSkladistePrijemnica` — S
  - `Skladista` — S
  - `Stock` — S
  - `TipPalete` — S
  - `Zavrseno` — S
- **Funkcionalnosti (kontrola → handler):**
  - „KREIRAJ“ — klik → `btnKreiraj_Click`
  - „ODUSTANI“ — klik → `btnOdustani_Click`
  - „Izbaci“ — klik → `btnIzbaciStavku_Click`
  - „Izmeni“ — klik → `btnIzmeniStavku_Click`

#### Otpremnica
- **Fajl:** `Skladista main/Dokumenta/Otpremnica.cs`  
- **Naslov forme:** „Otpremnica“
- **Otvara forme:** OtpremnicaPregled („Otpremnice pregled“)
- **Tabele:**
  - `Carinarnice` — S
  - `Delavci` — S
  - `Lokac` — S
  - `MagacinskiBrojCarinski` — S
  - `MaticniPodatki` — S
  - `Mesta` — S
  - `MestaUtovara` — S
  - `Narocilo` — S
  - `NarociloPostav` — S
  - `NHM` — S
  - `Partnerji` — S
  - `Pozicija` — S
  - `Promet` — S
  - `RadniNalogSkladista` — S
  - `RNCarinskoSkladisteDodatneUsluge` — S
  - `RNCarinskoSkladisteOtpremaStavkePom` — S
  - `RNCarinskoSkladisteOtpremnica` — S
  - `RNCarinskoSkladisteOtpremnicaStavke` — S
  - `RNCarinskoSkladistePrijemnica` — S
  - `RNCarinskoSkladisteRukovalac` — S
  - `Sklad` — S
  - `sklad` — S
  - `Skladista` — S
  - `Skladiste_Korisnik` — S
  - `TipPalete` — S
  - `Vozila` — S
  - `VrstaCarinskogPostupka` — S
  - `VrstaManipulacije` — S
  - `VrstaVozila` — S
  - `VrstePrevoznogSredstva` — S
- **Procedure:** `AktivirajSkladista`, `DeAktivirajSkladista`, `DeleteCarinskoStavke`, `DeleteMagacinskiBrojCarinski`, `DeletePromet`, `DeleteRnCarinskoSkladisteDodatneUsluge`, `DeleteSkladista`, `InsertCarinskoStavke`, `InsertDobavnica`, `InsertDobavnicaPostav`, `InsertMagacinskiBrojCarinski`, `InsertPrijemnica`, `InsertPrijemnicaPalete`, `InsertPrijemnicaPostav`, `InsertPromet`, `InsertPrometTrans`, `InsertRNCarinskoPrijemnicaStavke`, `InsertRNCarinskoRukovalac`, `InsertRNCarinskoSkladisteOtpremnica`, `InsertRNCarinskoSkladistePrijemnica`, `InsertRadniNalogInterniSkladista`, `InsertRadniNalogSkladista`, `InsertRnCarinskoSkladisteDodatneUsluge`, `InsertRnCarinskoSkladisteOtpremaStavkePom`, `InsertSkladista`, `InsertTransPosPostav`, `PromenaBrojaKontejnera`, `StornoRadniNalogInterni`, `UpdateCarinskoStavke`, `UpdateMagacinskiBrojCarinski`, `UpdatePorudzbenica`, `UpdateRNCarinskoSkladisteOtpremnica`, `UpdateRNCarinskoSkladistePrijemnica`, `UpdateRNInterniSkladista`, `UpdateRadniNalogSkladista`, `UpdateSkladista`
- **Funkcionalnosti (kontrola → handler):**
  - „Izbaci“ — klik → `btnIzbaci_Click`
  - „Dodaj“ — klik → `btnDodaj_Click`
  - „NAPOMENA“ — klik → `label27_Click`
  - „?“ — klik → `btnMagacinskiBroj_Click`
  - „?“ — klik → `btnPrijemnica_Click`
  - „Nazad“ — klik → `btnVratiNalog_Click`
  - „Izdaj nalog“ — klik → `btnIzdajNalog_Click` — čita: RNCarinskoSkladisteDodatneUsluge, RNCarinskoSkladisteRukovalac; proc: AktivirajSkladista, DeAktivirajSkladista, DeleteCarinskoStavke, DeleteMagacinskiBrojCarinski, DeleteRnCarinskoSkladisteDodatneUsluge, DeleteSkladista; preko: InsertSkladista
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - „Nalog za istovar rukovaocima“ — klik → `btnNalog_Click`
  - „Snimi“ — klik → `btnSnimi_Click` — čita: RNCarinskoSkladisteOtpremnica; proc: AktivirajSkladista, DeAktivirajSkladista, DeleteCarinskoStavke, DeleteMagacinskiBrojCarinski, DeleteRnCarinskoSkladisteDodatneUsluge, DeleteSkladista; preko: InsertSkladista
  - `button1` — klik → `button1_Click` — otvara: OtpremnicaPregled; čita: Promet; proc: DeletePromet, InsertDobavnica, InsertDobavnicaPostav, InsertPrijemnica, InsertPrijemnicaPostav, InsertPromet; preko: InsertIsporuka
  - „OPIS ROBE“ — klik → `label6_Click`
  - `btn_Povuci` — klik → `btn_Povuci_Click` — čita: Narocilo, Skladiste_Korisnik, sklad
  - „OSTALA SKLADIŠTA“ — klik → `label6_Click`
  - „BROJ TELEFONA“ — klik → `label6_Click`
  - `button3` — klik → `button3_Click_1` — otvara: OtpremnicaPregled
  - `button4` — klik → `button4_Click` — proc: DeletePromet, InsertDobavnica, InsertDobavnicaPostav, InsertPrijemnica, InsertPrijemnicaPostav, InsertPromet; preko: InsertIsporuka
  - `button23` — klik → `button3_Click_1` — otvara: OtpremnicaPregled
  - `button22` — klik → `button1_Click` — otvara: OtpremnicaPregled; čita: Promet; proc: DeletePromet, InsertDobavnica, InsertDobavnicaPostav, InsertPrijemnica, InsertPrijemnicaPostav, InsertPromet; preko: InsertIsporuka
  - `button5` — klik → `button4_Click` — proc: DeletePromet, InsertDobavnica, InsertDobavnicaPostav, InsertPrijemnica, InsertPrijemnicaPostav, InsertPromet; preko: InsertIsporuka

#### PaletePrebacivanje
- **Fajl:** `Skladista main/PaletePrebacivanje.cs`  
- **Naslov forme:** „PaletePrebacivanje“

#### PregledKomercijalnihNaloga
- **Fajl:** `Skladista main/PregledKomercijalnihNaloga.cs`  
- **Naslov forme:** „PregledKomercijalnihNaloga“
- **Otvara forme:** frmNalogPrijem („Otvori“), frmScenarioSCI („Pregledaj podatke“), Otprema („Otvori“)
- **Tabele:**
  - `IzvozKonacna` — S
  - `IzvozKonacnaVrstaManipulacije` — S
  - `IzvozKonacnaZaglavlje` — S
  - `KontejnerStatus` — S
  - `OrganizacioneJedinice` — S
  - `Partnerji` — S
  - `ProdajniNalogIzvoz` — S
  - `RadniNalogInterni` — S
  - `RadniNalogInterniPotvrda` — S
  - `RadniNalogInterniSkladistePotvrda` — S
  - `RadniNalogSkladista` — S
  - `Scenario` — S
  - `TipKontenjera` — S
  - `UvozKonacna` — S
  - `UvozKonacnaNapomenePozicioniranja` — S
  - `UvozKonacnaVrstaManipulacije` — S
  - `UvozKonacnaZaglavlje` — S
  - `Voz` — S
  - `VrstaManipulacije` — S
- **Procedure:** `AktivirajSkladista`, `DeAktivirajSkladista`, `DeleteCarinskoStavke`, `DeleteMagacinskiBrojCarinski`, `DeleteRnCarinskoSkladisteDodatneUsluge`, `DeleteSkladista`, `InsertCarinskoStavke`, `InsertMagacinskiBrojCarinski`, `InsertPrijemnicaPalete`, `InsertRNCarinskoPrijemnicaStavke`, `InsertRNCarinskoRukovalac`, `InsertRNCarinskoSkladisteOtpremnica`, `InsertRNCarinskoSkladistePrijemnica`, `InsertRadniNalogInterniSkladista`, `InsertRadniNalogSkladista`, `InsertRnCarinskoSkladisteDodatneUsluge`, `InsertRnCarinskoSkladisteOtpremaStavkePom`, `InsertSkladista`, `StornoRadniNalogInterni`, `UpdateCarinskoStavke`, `UpdateMagacinskiBrojCarinski`, `UpdateRNCarinskoSkladisteOtpremnica`, `UpdateRNCarinskoSkladistePrijemnica`, `UpdateRNInterniSkladista`, `UpdateRadniNalogSkladista`, `UpdateSkladista`
- **Funkcionalnosti (kontrola → handler):**
  - „Potvrdi“ — klik → `btnAktiviraj_Click` — čita: IzvozKonacna, ProdajniNalogIzvoz, RadniNalogSkladista; proc: AktivirajSkladista, DeAktivirajSkladista, DeleteCarinskoStavke, DeleteMagacinskiBrojCarinski, DeleteRnCarinskoSkladisteDodatneUsluge, DeleteSkladista; preko: InsertSkladista
  - „Refresh“ — klik → `button25_Click`
  - „Otvori“ — klik → `button24_Click` — otvara: frmNalogPrijem, TipSkladista; čita: IzvozKonacna, RadniNalogInterni, RadniNalogSkladista
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - „Pregledaj podatke“ — klik → `button1_Click`

#### PregledLagera
- **Fajl:** `Skladista main/Dokumenta/PregledLagera.cs`  
- **Naslov forme:** „PregledLagera“
- **Tabele:**
  - `Carinarnice` — S (preko OtpremaStavke)
  - `Lager` — S (preko OtpremaStavke)
  - `MagacinskiBrojCarinski` — S
  - `MestaUtovara` — S (preko OtpremaStavke)
  - `Partnerji` — S
  - `PomOstali` — S (preko OtpremaStavke)
  - `Primljeno` — S
  - `Promet` — S
  - `RadniNalogSkladista` — S
  - `Rezervisano` — S
  - `RNCarinskoPrijemnicaStavke` — S
  - `RNCarinskoSkladisteDodatneUsluge` — S (preko OtpremaStavke)
  - `RNCarinskoSkladisteOtpremaStavkePom` — SD
  - `RNCarinskoSkladisteOtpremnica` — S
  - `RNCarinskoSkladisteOtpremnicaStavke` — S
  - `RNCarinskoSkladistePrijemnica` — S
  - `Skladista` — S
  - `Stock` — S
  - `TipPalete` — S
  - `VrstaCarinskogPostupka` — S (preko OtpremaStavke)
  - `VrstaManipulacije` — S (preko OtpremaStavke)
  - `VrstaVozila` — S (preko OtpremaStavke)
  - `VrstePrevoznogSredstva` — S (preko OtpremaStavke)
  - `Zavrseno` — S

#### Pretovar
- **Fajl:** `Skladista main/Dokumenta/Pretovar.cs`  
- **Naslov forme:** „Pretovar“

#### Prijemnica
- **Fajl:** `Skladista main/Dokumenta/Prijemnica.Palete.cs`  
- **Naslov forme:** „Prijemnica“
- **Otvara forme:** PaletePrebacivanje („Prebaci na palete“), TerminalMapFRM („Mapa pozicija“), PrijemnicaPregled („PRETOVAR PREGLED“), ZapisnikOOstecenjuRobe („Zapisnik o oštećenju“)
- **Tabele:**
  - `Artikli` — SU
  - `ArtikliCetvrtiNivo` — S
  - `ArtikliDrugiNivo` — S
  - `ArtikliKatalog` — S (preko ArtikliKatalogDijalozi)
  - `ArtikliKatalogUvoz` — I (preko ArtikliKatalogDijalozi)
  - `ArtikliPrviNivo` — S
  - `ArtikliTreciNivo` — S
  - `Carinarnice` — S
  - `Delavci` — S
  - `Lokac` — S
  - `MagacinskiBrojCarinski` — S
  - `MaticniPodatki` — S
  - `MerskeEnote` — S
  - `Mesta` — S
  - `MestaUtovara` — S
  - `NHM` — S
  - `NNal` — S
  - `NNalP` — S
  - `NPre` — S
  - `NPreP` — S
  - `Partnerji` — S
  - `Pozicija` — S
  - `Promet` — S
  - `RadniNalogInterni` — S
  - `RadniNalogSkladista` — S
  - `RNCarinskoPackingLista` — SIU
  - `RNCarinskoPackingListaStavka` — SIU
  - `RNCarinskoPaleta` — S
  - `RNCarinskoPaletaStavka` — S
  - `RNCarinskoPrijemnicaStavke` — SD
  - `RNCarinskoSkladisteDodatneUsluge` — S
  - `RNCarinskoSkladistePrijemnica` — S
  - `RNCarinskoSkladisteRukovalac` — S
  - `Sklad` — S
  - `sklad` — S
  - `Skladista` — S
  - `SkladistaGrupa` — S
  - `TipPalete` — S
  - `TipRobe` — S
  - `UvozKonacnaNHM` — S
  - `Valute` — S
  - `VrstaCarinskogPostupka` — S
  - `VrstaManipulacije` — S
  - `VrstaVozila` — S
  - `VrstePrevoznogSredstva` — S
- **Procedure:** `AktivirajSkladista`, `DeAktivirajSkladista`, `DeleteCarinskoStavke`, `DeleteMagacinskiBrojCarinski`, `DeletePromet`, `DeleteRNMedjuskladisni`, `DeleteRNOtpremaCirade`, `DeleteRNOtpremaPlatforme`, `DeleteRNOtpremaPlatforme2`, `DeleteRNOtpremaVoza`, `DeleteRNPregledIpostavkaKontejnera`, `DeleteRNPregledIspraznjenogKontejnera`, `DeleteRNPrijemCirade`, `DeleteRNPrijemPlatforme`, `DeleteRNPrijemPlatforme2`, `DeleteRNPrijemVoza`, `DeleteRNPrijemVoza2`, `DeleteRnCarinskoSkladisteDodatneUsluge`, `DeleteSkladista`, `InsertCarinskoStavke`, `InsertDobavnica`, `InsertDobavnicaPostav`, `InsertMagacinskiBrojCarinski`, `InsertPrijemnica`, `InsertPrijemnicaPalete`, `InsertPrijemnicaPostav`, `InsertPromet`, `InsertPrometTrans`, `InsertRN10PregledCeoVoz`, `InsertRN11PregledCeoVozOtprema`, `InsertRN12Medjuskladisni`, `InsertRN4PrPlatformeKamIzvoz`, `InsertRN4PrPlatformeKamUvoz`, `InsertRN5PrPlatFormeKam`, `InsertRN6OtPlatFormeKam`, `InsertRN7OtpremaOtpremaPlatKam`, `InsertRN8OtpremaCiradeKam`, `InsertRN8OtpremaCiradeKamIzvoz`, `InsertRN9PrijemCiradeKam`, `InsertRNCarinskoPrijemnicaStavke`, `InsertRNCarinskoRukovalac`, `InsertRNCarinskoSkladisteOtpremnica`, `InsertRNCarinskoSkladistePrijemnica`, `InsertRNOtpremaCirade`, `InsertRNOtpremaPlatforme`, `InsertRNOtpremaPlatforme2`, `InsertRNOtpremaVoza`, `InsertRNOtpremaVozaCeoVoz`, `InsertRNPregledIpostavkaKontejnera`, `InsertRNPregledIspraznjenogKontejnera`, `InsertRNPrijemCirade`, `InsertRNPrijemPlatforme`, `InsertRNPrijemPlatforme2`, `InsertRNPrijemVoza`, `InsertRNPrijemVoza2`, `InsertRNPrijemVozaCeoVoz`, `InsertRNPrijemVozaCeoVozPretovar`, `InsertRadniNalogInterniSkladista`, `InsertRadniNalogSkladista`, `InsertRnCarinskoSkladisteDodatneUsluge`, `InsertRnCarinskoSkladisteOtpremaStavkePom`, `InsertSkladista`, `InsertTransPosPostav`, `PokreniSkladisninuKontejnera`, `PotvrdiPrijem`, `PrenesiKontejnerTekuceUArhiv`, `PromenaBrojaKontejnera`, `StornoRadniNalogInterni`, `UpdateCarinskoStavke`, `UpdateIZCIRA`, `UpdateMagacinskiBrojCarinski`, `UpdatePorudzbenica`, `UpdateRN12MedjuskladisniSkladiste`, `UpdateRN12Uradjene`, `UpdateRN1Uradjene`, `UpdateRN1UradjeneCIR`, `UpdateRN1UradjeneS`, `UpdateRN1UradjeneVP`, `UpdateRN2Uradjene`, `UpdateRN3Uradjene`, `UpdateRN4PotrebanCIR`, `UpdateRN4PrivremenoSkladiste`, `UpdateRN4SkladisteIzCira`, `UpdateRN4Uradjene`, `UpdateRN4UradjeneCIR`, `UpdateRN4UradjenePostavljen`, `UpdateRN4UradjeneVizuelni`, `UpdateRN5PrijemSkladiste`, `UpdateRN5Uradjene`, `UpdateRN5UradjeneCIR`, `UpdateRN5UradjeneS`, `UpdateRN6OtpremaSkladiste`, `UpdateRN6PotrebanCIR`, `UpdateRN6Uradjene`, `UpdateRN6UradjeneVizuelni`, `UpdateRN7Uradjene`, `UpdateRN8Uradjene`, `UpdateRN8UradjeneCIR`, `UpdateRN9PretovarCIRADE`, `UpdateRN9Uradjen`, `UpdateRNCarinskoSkladisteOtpremnica`, `UpdateRNCarinskoSkladistePrijemnica`, `UpdateRNInterniSkladista`, `UpdateRNMedjuskladisni`, `UpdateRNOtpremaCirade`, `UpdateRNOtpremaPlatforme`, `UpdateRNOtpremaPlatforme2`, `UpdateRNOtpremaVoza`, `UpdateRNPregledIpostavkaKontejnera`, `UpdateRNPregledIspraznjenogKontejnera`, `UpdateRNPrijemCirade`, `UpdateRNPrijemPlatforme`, `UpdateRNPrijemPlatforme2`, `UpdateRNPrijemVoza`, `UpdateRNPrijemVoza2`, `UpdateRNPrijemVozaSkladiste`, `UpdateRadniNalogSkladista`, `UpdateSkladista`, `ZatvoriSkladisninuKontejnera`, `sp_ArtikliKatalog_Uvezi`, `sp_PL_Analiziraj`, `sp_PL_FormirajArtikle`, `sp_PrijemnicaStavka_Snimi`, `updRN4PrPlatformeKamIzvozPusti`
- **Funkcionalnosti (kontrola → handler):**
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
  - „Dodaj“ — klik → `btnDodaj_Click` — piše: RNCarinskoPrijemnicaStavke[D]; čita 19 tabela; preko: PrijemnicaStavke
  - „Nazad“ — klik → `btnNHMNazad_Click`
  - „Dodaj“ — klik → `btnNHMDodaj_Click`
  - „Nazad“ — klik → `btnVratiNalog_Click`
  - „Izdaj nalog“ — klik → `btnIzdajNalog_Click` — čita: RNCarinskoSkladisteDodatneUsluge, RNCarinskoSkladisteRukovalac; proc: AktivirajSkladista, DeAktivirajSkladista, DeleteCarinskoStavke, DeleteMagacinskiBrojCarinski, DeleteRnCarinskoSkladisteDodatneUsluge, DeleteSkladista; preko: InsertSkladista
  - „Nalog rukovaocima“ — klik → `button2_Click`
  - „Mapa pozicija“ — klik → `button1_Click`
  - „Snimi“ — klik → `button24_Click` — piše: RNCarinskoPrijemnicaStavke[D]; čita: RNCarinskoSkladistePrijemnica; proc: AktivirajSkladista, DeAktivirajSkladista, DeleteCarinskoStavke, DeleteMagacinskiBrojCarinski, DeleteRnCarinskoSkladisteDodatneUsluge, DeleteSkladista; preko: InsertSkladista
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
  - „Mapa pozicija“ — klik → `button1_Click_1` — otvara: PrijemnicaPregled; čita: Promet; proc: DeletePromet, DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza; preko: InsertIsporuka, InsertRN
  - `btn_Povuci` — klik → `btn_Povuci_Click` — čita: Delavci, NNal, NPre, NPreP, sklad
  - „Nalog rukovaocima“ — klik → `button2_Click_1`
  - `dataGridView2` — izbor → `dataGridView2_SelectionChanged` — čita: MaticniPodatki, NNalP
  - `btn_Izaberi` — klik → `btn_Izaberi_Click` — čita: MaticniPodatki, NNal, NNalP, sklad
  - „OPIS ROBE“ — klik → `label6_Click`
  - „Zapisnik“ — klik → `button3_Click_1` — otvara: PrijemnicaPregled
  - `cboGrupaPolja` — izbor → `cboGrupaPolja_SelectedIndexChanged` — čita: Skladista
  - `button6` — klik → `button3_Click_1` — otvara: PrijemnicaPregled
  - `button23` — klik → `button23_Click` — otvara: ZapisnikOOstecenjuRobe
  - `button22` — klik → `button1_Click_1` — otvara: PrijemnicaPregled; čita: Promet; proc: DeletePromet, DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza; preko: InsertIsporuka, InsertRN

#### RadniNaloziSkladista
- **Fajl:** `Skladista main/RadniNaloziSkladista.cs`  
- **Naslov forme:** „RadniNaloziSkladista“
- **Otvara forme:** InterniPoslovi („Interni poslovi“), InterniPrenos („Interni prenos“), Pretovar („Pretovar“), VanredniPoslovi („Vanredni poslovi“)
- **Funkcionalnosti (kontrola → handler):**
  - „Interni poslovi“ — klik → `btnInterniPoslovi_Click`
  - „Vanredni poslovi“ — klik → `btnVanredniPoslovi_Click`
  - „Interni prenos“ — klik → `btnInterniPrenos_Click`
  - „Pretovar“ — klik → `btnPretovar_Click`
  - „Otprema“ — klik → `btnOtprema_Click`
  - „Prijem“ — klik → `btnPrijem_Click` — otvara: frmNalogPrijem

#### SkladistaMain
- **Fajl:** `Skladista main/SkladistaMain.cs`  
- **Naslov forme:** „SkladistaMain“
- **Otvara forme:** CarinskoSkladiste („Carinsko skladište“), KomercijalnoSkladiste („Komercijalno skladište“), NalogZaKomercijalni (kartica „Radni nalozi“)
- **Funkcionalnosti (kontrola → handler):**
  - „Komercijalni nalozi“ — klik → `btnRadniNalozi_Click` — otvara: NalogZaKomercijalni
  - „Komercijalno skladište“ — klik → `btnKomerijalnoSkladiste_Click` — otvara: KomercijalnoSkladiste
  - „Carinsko skladište“ — klik → `btnCarinskoSkladiste_Click` — otvara: CarinskoSkladiste

#### TerminalMapFRM
- **Fajl:** `TerminalMap/TerminalMapFRM.cs`  
- **Naslov forme:** „TerminalMapFRM“
- **Modul:** Operacije skladišta, Prijem i otprema vozova
- **Otvara forme:** frmDefinisiPoziciju („Definiši/Predefiniši“), frmDodatneUsluge („Dodatne usluge“), frmKontejnerTekuce („Terminal central“), frmRN12InterniPrenos („Interni prenos“), frmDefinisiPoziciju („Definiši/Predefiniši“), frmKontejnerTekuce („Terminal central“), frmRN12InterniPrenos („Interni prenos“)
- **Tabele:**
  - `KontejnerStatus` — S
  - `KontejnerTekuce` — S
  - `PArtnerji` — S
  - `Pozicija` — S
  - `Skladista` — SU
  - `SkladistaGrupa` — S
  - `TipKontenjera` — S
  - `uvKvalitetKontejnera` — S
  - `Zone` — S
- **Funkcionalnosti (kontrola → handler):**
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

#### TipSkladista
- **Fajl:** `Skladista main/TipSkladista.cs`  
- **Naslov forme:** „TipSkladista“
- **Otvara forme:** RadniNaloziSkladista („Carinsko skladište“)
- **Funkcionalnosti (kontrola → handler):**
  - „Komercijalno skladište“ — klik → `btnKomercijalnoSkladiste_Click` — otvara: RadniNaloziSkladista
  - „Carinsko skladište“ — klik → `btnCarinskoSkladiste_Click` — otvara: RadniNaloziSkladista

#### UvozDokumenta
- **Fajl:** `Uvoz/UvozDokumenta.cs`  
- **Naslov forme:** „Uvoz dokumenta“
- **Modul:** Operacije skladišta, Prijem i otprema kamiona, Prijem i otprema vozova
- **Tabele:**
  - `TipKomercijalnogDokumenta` — S
  - `Uvoz` — S
  - `UvozDokumenta` — S
  - `UvozDokumentaCeoVoz` — S
  - `UvozDokumentaUsluge` — S
  - `UvozKonacna` — S
  - `UvozKonacnaVrstaManipulacije` — S
  - `UvozVrstaManipulacije` — S
  - `VrstaManipulacije` — S
  - `VrstaManipulACIJE` — S
- **Procedure:** `DeleteUvozDokumenta`, `DeleteUvozDokumentaCeoVoz`, `DeleteUvozDokumentaUsluge`, `InsertUvozDokumenta`, `InsertUvozDokumentaCeoVoz`, `InsertUvozDokumentaUsluge`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „Dokumentacija“ — klik → `toolStripButton1_Click`
  - „Izbrisi“ — klik → `button3_Click` — proc: DeleteUvozDokumenta, DeleteUvozDokumentaCeoVoz, DeleteUvozDokumentaUsluge, InsertUvozDokumenta, InsertUvozDokumentaCeoVoz, InsertUvozDokumentaUsluge; preko: InsertUvozDokumenta
  - „Sacuvaj“ — klik → `button4_Click` — proc: DeleteUvozDokumenta, DeleteUvozDokumentaCeoVoz, DeleteUvozDokumentaUsluge, InsertUvozDokumenta, InsertUvozDokumentaCeoVoz, InsertUvozDokumentaUsluge; preko: InsertUvozDokumenta
  - „Otvori“ — klik → `button2_Click`
  - „Pronadji“ — klik → `button1_Click`
  - „Dokument se odnosi na ceo voz“ — čekiranje → `chkZaVoz_CheckedChanged_1`

#### VanredniPoslovi
- **Fajl:** `Skladista main/Dokumenta/VanredniPoslovi.cs`  
- **Naslov forme:** „VanredniPoslovi“
- **Tabele:**
  - `Carinarnice` — S
  - `MagacinskiBrojCarinski` — S
  - `MestaUtovara` — S
  - `Partnerji` — S
  - `RadniNalogSkladista` — S
  - `RNCarinskoSkladisteDodatneUsluge` — S
  - `VrstaCarinskogPostupka` — S
  - `VrstaManipulacije` — S
  - `VrstaVozila` — S
  - `VrstePrevoznogSredstva` — S
- **Procedure:** `AktivirajSkladista`, `DeAktivirajSkladista`, `DeleteCarinskoStavke`, `DeleteMagacinskiBrojCarinski`, `DeleteRnCarinskoSkladisteDodatneUsluge`, `DeleteSkladista`, `InsertCarinskoStavke`, `InsertMagacinskiBrojCarinski`, `InsertPrijemnicaPalete`, `InsertRNCarinskoPrijemnicaStavke`, `InsertRNCarinskoRukovalac`, `InsertRNCarinskoSkladisteOtpremnica`, `InsertRNCarinskoSkladistePrijemnica`, `InsertRadniNalogInterniSkladista`, `InsertRadniNalogSkladista`, `InsertRnCarinskoSkladisteDodatneUsluge`, `InsertRnCarinskoSkladisteOtpremaStavkePom`, `InsertSkladista`, `StornoRadniNalogInterni`, `UpdateCarinskoStavke`, `UpdateMagacinskiBrojCarinski`, `UpdateRNCarinskoSkladisteOtpremnica`, `UpdateRNCarinskoSkladistePrijemnica`, `UpdateRNInterniSkladista`, `UpdateRadniNalogSkladista`, `UpdateSkladista`
- **Funkcionalnosti (kontrola → handler):**
  - „Izbaci“ — klik → `btnIzbaci_Click`
  - „Nazad“ — klik → `btnMbNazad_Click`
  - „Sačuvaj“ — klik → `btnMbSave_Click` — čita: MagacinskiBrojCarinski; proc: AktivirajSkladista, DeAktivirajSkladista, DeleteCarinskoStavke, DeleteMagacinskiBrojCarinski, DeleteRnCarinskoSkladisteDodatneUsluge, DeleteSkladista; preko: InsertSkladista
  - „Dodaj“ — klik → `btnDodaj_Click`
  - „FORMIRAN RN“ — klik → `btnFormiranRN_Click` — čita: RNCarinskoSkladisteDodatneUsluge; proc: AktivirajSkladista, DeAktivirajSkladista, DeleteCarinskoStavke, DeleteMagacinskiBrojCarinski, DeleteRnCarinskoSkladisteDodatneUsluge, DeleteSkladista; preko: InsertSkladista
  - „SNIMI“ — klik → `btnSnimi_Click` — čita: RNCarinskoSkladisteDodatneUsluge, RadniNalogSkladista; proc: AktivirajSkladista, DeAktivirajSkladista, DeleteCarinskoStavke, DeleteMagacinskiBrojCarinski, DeleteRnCarinskoSkladisteDodatneUsluge, DeleteSkladista; preko: InsertSkladista
  - `cboVlasnikRobe` — SelectionChangeCommitted → `cboVlasnikRobe_SelectionChangeCommitted` — čita: Partnerji
  - „?“ — klik → `btnMagacinskiBroj_Click`

#### ViljuskaristiPregled
- **Fajl:** `Skladista main/ViljuskaristiPregled.cs`  
- **Naslov forme:** „Viljuskaristi Pregled“
- **Tabele:**
  - `Delavci` — S
  - `RadniNalogInterni` — S
  - `RadniNalogInterniSkladistePotvrda` — S
  - `RadniNalogSkladista` — S
  - `RNCarinskoSkladisteDodatneUsluge` — S
  - `RNCarinskoSkladistePrijemnica` — S
  - `RNCarinskoSkladisteRukovalac` — S
  - `TipPalete` — S
  - `VrstaManipulacije` — S
- **Funkcionalnosti (kontrola → handler):**
  - „Potvrdi skladistar“ — klik → `button3_Click`
  - „Refresh“ — klik → `button2_Click`
  - „gridGroupingControl2“ — klik na ćeliju → `gridGroupingControl2_TableControlCellClick` — čita: RNCarinskoSkladisteDodatneUsluge, RNCarinskoSkladistePrijemnica, RadniNalogSkladista, VrstaManipulacije

### Drumski transport (25)

#### Cerade
- **Fajl:** `MainLeget/Drumski/Cerade.cs`  
- **Naslov forme:** „Cerade“
- **Otvara forme:** frmAutomobiliDrumski („UNOS PREVOZNIK/KAMIONER“), frmPodesavanjeRaspolozivosti („PROVERA \r\nRASPOLOŽIVOSTI VOZILA“), frmStatus („STATUSI VOZILA“), NalogZaDrumski („NALOG ZA DRUMSKI TRANSPORT“), PakovanjeKamionaCerade („FORMIRANJE\r\nTRANSPORTNOG NALOGA“)
- **Funkcionalnosti (kontrola → handler):**
  - „STATUSI VOZILA“ — klik → `btnStatusi_Click` — otvara: frmStatus
  - „PROVERA \r\nRASPOLOŽIVOSTI VOZILA“ — klik → `btnProvera_Click` — otvara: frmPodesavanjeRaspolozivosti
  - „FORMIRANJE\r\nTRANSPORTNOG NALOGA“ — klik → `btnFormiranjeNaloga_Click` — otvara: PakovanjeKamionaCerade
  - „NALOG ZA FAKTURISANJE“ — klik → `btnNalogFakturisanje_Click` — otvara: frmNalogZaFakturisanje
  - „UNOS PREVOZNIK/KAMIONER“ — klik → `btnPonude_Click` — otvara: frmAutomobiliDrumski
  - „NALOG ZA DRUMSKI TRANSPORT“ — klik → `btnNalogDrumski_Click` — otvara: NalogZaDrumski

#### Drumski1
- **Fajl:** `MainLeget/Drumski/Drumski1.cs`  
- **Naslov forme:** „MainLeget“
- **Otvara forme:** Cerade („Cerada“), frmNalogZaFakturisanje („NALOG ZA FAKTURISANJE“), frmObradaUlaznihFaktura („Kontrola i obračun transporta“), Platforma („Platforme“), VanGabaritni („Van Gabaritni“)
- **Funkcionalnosti (kontrola → handler):**
  - „Cerada“ — klik → `btnForm1_Click` — otvara: Cerade
  - „Platforme“ — klik → `sfButton7_Click` — otvara: Platforma
  - „Van Gabaritni“ — klik → `sfButton6_Click` — otvara: VanGabaritni
  - „Kontrola i obračun transporta“ — klik → `sfButton1_Click_1` — otvara: frmObradaUlaznihFaktura
  - „NALOG ZA FAKTURISANJE“ — klik → `btnNalogFakturisanje_Click` — otvara: frmNalogZaFakturisanje

#### frmAutomobilEdit
- **Fajl:** `Drumski/frmAutomobilEdit.cs`  
- **Naslov forme:** „Automobil detalji“
- **Tabele:**
  - `Delavci` — S
  - `Korisnici` — S
  - `Partnerji` — S
  - `VrstaVozila` — S
- **Procedure:** `DeleteAutomobili`, `DeleteAutomobiliServis`, `DeleteAutomobiliTehnickiProblem`, `InsertAutomobili`, `InsertAutomobiliDrumski`, `InsertAutomobiliServis`, `InsertAutomobiliTehnickiProblem`, `UpdateAutomobili`, `UpdateAutomobiliDrumski`, `UpdateAutomobiliServis`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi prevoznik/kamioner“ — klik → `btnDodavanjePartnera_Click` — otvara: frmPartnerji
  - `button1` — klik → `button1_Click` — proc: DeleteAutomobili, DeleteAutomobiliServis, DeleteAutomobiliTehnickiProblem, InsertAutomobili, InsertAutomobiliDrumski, InsertAutomobiliServis; preko: InsertAutomobili
  - `button2` — klik → `button2_Click` — proc: DeleteAutomobili, DeleteAutomobiliServis, DeleteAutomobiliTehnickiProblem, InsertAutomobili, InsertAutomobiliDrumski, InsertAutomobiliServis; preko: InsertAutomobili

#### frmAutomobiliDrumski
- **Fajl:** `Drumski/frmAutomobiliDrumski.cs`  
- **Naslov forme:** „Automobili“
- **Otvara forme:** frmAutomobilEdit („Novi“)
- **Tabele:**
  - `Automobili` — S
  - `AutomobiliTehnickiProblem` — S
  - `Delavci` — S
  - `Korisnici` — S
  - `Partnerji` — S
  - `RadniNalogDrumski` — S
  - `SistemskePostavke` — S
  - `VrstaVozila` — S
- **Procedure:** `DeleteAutomobili`, `DeleteAutomobiliServis`, `DeleteAutomobiliTehnickiProblem`, `InsertAutomobili`, `InsertAutomobiliDrumski`, `InsertAutomobiliServis`, `InsertAutomobiliTehnickiProblem`, `UpdateAutomobili`, `UpdateAutomobiliDrumski`, `UpdateAutomobiliServis`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click` — otvara: frmAutomobilEdit
  - „tsSave“ — klik → `tsSave_Click` — proc: DeleteAutomobili, DeleteAutomobiliServis, DeleteAutomobiliTehnickiProblem, InsertAutomobili, InsertAutomobiliDrumski, InsertAutomobiliServis; preko: InsertAutomobili
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteAutomobili, DeleteAutomobiliServis, DeleteAutomobiliTehnickiProblem, InsertAutomobili, InsertAutomobiliDrumski, InsertAutomobiliServis; preko: InsertAutomobili
  - „Skenirana dokumentacija“ — klik → `toolStripButton1_Click` — otvara: frmAutomobiliDokumenta
  - „Evidencija Servisa“ — klik → `toolStripButton2_Click` — otvara: frmAutomobiliServis
  - „Evidencija registracija“ — klik → `toolStripButton3_Click` — otvara: frmAutomobiliRegistracija
  - „Evidencija troškova iz UF“ — klik → `toolStripButton4_Click` — otvara: frmEvidencijaTroskova
  - „Evidencija kvarova“ — klik → `toolStripButton5_Click` — otvara: frmEvidencijaKvarova
  - „Raspoloživost“ — klik → `button4_Click`
  - `button2` — klik → `tsSave_Click` — proc: DeleteAutomobili, DeleteAutomobiliServis, DeleteAutomobiliTehnickiProblem, InsertAutomobili, InsertAutomobiliDrumski, InsertAutomobiliServis; preko: InsertAutomobili
  - `button1` — klik → `tsDelete_Click` — proc: DeleteAutomobili, DeleteAutomobiliServis, DeleteAutomobiliTehnickiProblem, InsertAutomobili, InsertAutomobiliDrumski, InsertAutomobiliServis; preko: InsertAutomobili
  - `button3` — klik → `tsNew_Click` — otvara: frmAutomobilEdit
  - „Detalji“ — klik → `otvoriToolStripMenuItem_Click` — otvara: frmAutomobilEdit
  - „Sutra“ — čekiranje → `chkDatumD_CheckedChanged`
  - „Danas“ — čekiranje → `chkDatumD_CheckedChanged`
  - „Filtriraj“ — klik → `btnFiltriraj_Click`

#### frmBrojFaktureModal
- **Fajl:** `Drumski/frmBrojFaktureModal.cs`  
- **Naslov forme:** „Unosna forma“
- **Funkcionalnosti (kontrola → handler):**
  - „OK“ — klik → `button5_Click`

#### frmDrumski
- **Fajl:** `Drumski/frmDrumski.cs`  
- **Naslov forme:** „DRUMSKI UNOSNA FORMA“
- **Tabele:**
  - `Automobili` — S
  - `Carinarnice` — S
  - `Delavci` — S
  - `DrumskiPozicioniranje` — S
  - `Izvoz` — S
  - `IzvozKonacna` — S
  - `IzvozKonacnaZaglavlje` — S
  - `Korisnici` — S
  - `MestaUtovara` — S
  - `NapomenaZaPozicioniranje` — S
  - `Partnerji` — S
  - `partnerjiKontOsebaMU` — S
  - `RadniNalogDrumski` — S
  - `RadniNalogInterni` — S
  - `SistemskePostavke` — S
  - `StatusVozila` — S
  - `TipKontenjera` — S
  - `Uvoz` — S
  - `UvozKonacna` — S
  - `UvozKonacnaZaglavlje` — S
  - `Valute` — S
  - `Voz` — S
  - `VrstaCarinskogPostupka` — S
  - `VrstaVozila` — S
  - `VrstePostupakaUvoz` — S
- **Procedure:** `ArhiviranRadniNalogDrumski`, `DelRadniNalogInterniSaDokumentima`, `DeleteMestaUtovara`, `DeletePartnerji`, `DeletePozicioniranje`, `DeleteRadniNalogDrumski`, `DeleteRadniNalogInterni`, `DodajStavkeURadniNalogDrumski`, `DodeliKamionZaRadniNalogDrumski`, `DodeliKamionZaRadniNalogDrumskiP`, `DodeliKamionZaViseRadnihNalogaDrumskiP`, `DozvoliIzlaz`, `DuplirajZapisRadniNalogDrumski`, `InsertDokumentaRadniNalogDrumski`, `InsertDrzave`, `InsertMestaUtovara`, `InsertParnerji`, `InsertPoste`, `InsertPozicioniranje`, `InsertRadniNalogDrumski`, `InsertRadniNalogDrumskiNHM`, `InsertRadniNalogDrumskiNovi`, `InsertRadniNalogInterni`, `InsertRadniNalogInterni2`, `InsertRadniNalogInterniIzvoz`, `InsertRadniNalogInterniIzvoz2`, `InsertRadniNalogInterniIzvozDodatne`, `InsertRadniNalogInterniPotvrda`, `InsertRadniNalogInterniSkladistePotvrda`, `InsertRadniNalogInterniStorno`, `InsertTokens`, `NapraviRadniNalogDrumskiIzDrumskog`, `ObavljenPregled`, `PbrisiKamionZaRadniNalogDrumski`, `PromeniStatusKalmar`, `PromeniStatusKapijaOdlazak`, `PromeniStatusPretovareno`, `PromeniStatusPrijemPotvrdioKapiju`, `PromeniStatusPrijemPotvrdioVP`, `UpdateKorakPotvrdeVozaca`, `UpdateMestaUtovara`, `UpdatePartneri`, `UpdatePoslatStatus`, `UpdatePozicioniranje`, `UpdateRNIVozFormiran`, `UpdateRadniNalogDrumski`, `UpdateRadniNalogDrumskiIzvoz`, `UpdateRadniNalogDrumskiKamionID`, `UpdateRadniNalogDrumskiListaKamionID`, `UpdateRadniNalogDrumskiNovi`, `UpdateRadniNalogDrumskiOdobrioLO`, `UpdateRadniNalogDrumskiOtkazi`, `UpdateRadniNalogDrumskiPoslataNajava`, `UpdateRadniNalogDrumskiPoslateInstrukcije`, `UpdateRadniNalogInterni`, `UpdateRadniNalogInterniFormiran`, `UpdateRadniNalogInterniGenerisan`, `UpdateRadniNalogInterniZavrsen`, `UpdateStatusRadniNalogDrumski`, `updRadniNalogInterniPrijemnicaPotvrdjen`, `updRadniNalogInterniRukovalacPotvrdjen`, `updRadniNalogInterniSkladistarPotvrdio`, `updRadniNalogInterniSkladistePotvrdjen`
- **Funkcionalnosti (kontrola → handler):**
  - „Nova špedicija“ — klik → `btnPartner_Click` — otvara: frmPartnerji
  - „Otvori dokumenta“ — klik → `button4_Click` — otvara: frmPregledFajlova
  - „Dodaj dokument“ — klik → `button1_Click` — proc: ArhiviranRadniNalogDrumski, DeleteRadniNalogDrumski, DodajStavkeURadniNalogDrumski, DodeliKamionZaRadniNalogDrumski, DodeliKamionZaRadniNalogDrumskiP, DodeliKamionZaViseRadnihNalogaDrumskiP; preko: InsertRadniNalogDrumski
  - „Formiranje naloga“ — klik → `btnFormiranjeNaloga_Click` — proc: ArhiviranRadniNalogDrumski, DeleteRadniNalogDrumski, DodajStavkeURadniNalogDrumski, DodeliKamionZaRadniNalogDrumski, DodeliKamionZaRadniNalogDrumskiP, DodeliKamionZaViseRadnihNalogaDrumskiP; preko: InsertRadniNalogDrumski
  - `button21` — klik → `button21_Click_1` — proc: ArhiviranRadniNalogDrumski, DeleteRadniNalogDrumski, DodajStavkeURadniNalogDrumski, DodeliKamionZaRadniNalogDrumski, DodeliKamionZaRadniNalogDrumskiP, DodeliKamionZaViseRadnihNalogaDrumskiP; preko: InsertRadniNalogDrumski
  - `button2` — klik → `button21_Click` — čita: RadniNalogDrumski, RadniNalogInterni, SistemskePostavke; proc: ArhiviranRadniNalogDrumski, DelRadniNalogInterniSaDokumentima, DeleteRadniNalogDrumski, DeleteRadniNalogInterni, DodajStavkeURadniNalogDrumski, DodeliKamionZaRadniNalogDrumski; preko: InsertRadniNalogDrumski, InsertRadniNalogInterni

#### frmDrumski1
- **Fajl:** `Drumski/frmDrumski1.cs`  
- **Naslov forme:** „DRUMSKI UNOSNA FORMA“
- **Otvara forme:** frmPartnerji („Nova špedicija“)
- **Tabele:**
  - `Automobili` — S
  - `Carinarnice` — S
  - `Delavci` — S
  - `DrumskiPozicioniranje` — S
  - `InspekciskiTretman` — S (preko PrivremeniNHM)
  - `Izvoz` — S
  - `IzvozKonacna` — S
  - `IzvozKonacnaZaglavlje` — S
  - `IzvozNapomenePozicioniranja` — S
  - `IzvozNHM` — S
  - `IzvozVrstaManipulacije` — S (preko PrivremeniNHM)
  - `KontejnerskiTerminali` — S
  - `Korisnici` — S
  - `MestaUtovara` — S
  - `NapomenaZaPozicioniranje` — S
  - `NHM` — S
  - `Partnerji` — S
  - `partnerjiKontOsebaMU` — S
  - `ProdajniNalogIzvoz` — S (preko PrivremeniNHM)
  - `ProdajniNalogIzvozStavke` — S (preko PrivremeniNHM)
  - `RadniNalogDrumski` — S
  - `RadniNalogDrumskiNHM` — S
  - `RadniNalogInterni` — S
  - `Scenario` — S (preko PrivremeniNHM)
  - `SistemskePostavke` — S
  - `StatusVozila` — S
  - `TipKontenjera` — S
  - `uvKvalitetKontejnera` — S
  - `uvNacinPakovanja` — S
  - `Uvoz` — S
  - `UvozKonacna` — S
  - `UvozKonacnaZaglavlje` — S
  - `Valute` — S
  - `Voz` — S
  - `VrstaCarinskogPostupka` — S
  - `VrstaManipulacije` — S (preko PrivremeniNHM)
  - `VrstaRobeADR` — S
  - `VrstaVozila` — S
  - `VrstePostupakaUvoz` — S
- **Procedure:** `ArhiviranRadniNalogDrumski`, `DelRadniNalogInterniSaDokumentima`, `DeleteMestaUtovara`, `DeletePozicioniranje`, `DeleteRadniNalogDrumski`, `DeleteRadniNalogInterni`, `DodajStavkeURadniNalogDrumski`, `DodeliKamionZaRadniNalogDrumski`, `DodeliKamionZaRadniNalogDrumskiP`, `DodeliKamionZaViseRadnihNalogaDrumskiP`, `DozvoliIzlaz`, `DuplirajZapisRadniNalogDrumski`, `InsertDokumentaRadniNalogDrumski`, `InsertMestaUtovara`, `InsertPozicioniranje`, `InsertRadniNalogDrumski`, `InsertRadniNalogDrumskiNHM`, `InsertRadniNalogDrumskiNovi`, `InsertRadniNalogInterni`, `InsertRadniNalogInterni2`, `InsertRadniNalogInterniIzvoz`, `InsertRadniNalogInterniIzvoz2`, `InsertRadniNalogInterniIzvozDodatne`, `InsertRadniNalogInterniPotvrda`, `InsertRadniNalogInterniSkladistePotvrda`, `InsertRadniNalogInterniStorno`, `InsertTokens`, `NapraviRadniNalogDrumskiIzDrumskog`, `ObavljenPregled`, `PbrisiKamionZaRadniNalogDrumski`, `PromeniStatusKalmar`, `PromeniStatusKapijaOdlazak`, `PromeniStatusPretovareno`, `PromeniStatusPrijemPotvrdioKapiju`, `PromeniStatusPrijemPotvrdioVP`, `UpdateKorakPotvrdeVozaca`, `UpdateMestaUtovara`, `UpdatePoslatStatus`, `UpdatePozicioniranje`, `UpdateRNIVozFormiran`, `UpdateRadniNalogDrumski`, `UpdateRadniNalogDrumskiIzvoz`, `UpdateRadniNalogDrumskiKamionID`, `UpdateRadniNalogDrumskiListaKamionID`, `UpdateRadniNalogDrumskiNovi`, `UpdateRadniNalogDrumskiOdobrioLO`, `UpdateRadniNalogDrumskiOtkazi`, `UpdateRadniNalogDrumskiPoslataNajava`, `UpdateRadniNalogDrumskiPoslateInstrukcije`, `UpdateRadniNalogInterni`, `UpdateRadniNalogInterniFormiran`, `UpdateRadniNalogInterniGenerisan`, `UpdateRadniNalogInterniZavrsen`, `UpdateStatusRadniNalogDrumski`, `updRadniNalogInterniPrijemnicaPotvrdjen`, `updRadniNalogInterniRukovalacPotvrdjen`, `updRadniNalogInterniSkladistarPotvrdio`, `updRadniNalogInterniSkladistePotvrdjen`
- **Funkcionalnosti (kontrola → handler):**
  - `button2` — klik → `button2_Click` — čita: RadniNalogDrumski, RadniNalogInterni, SistemskePostavke; proc: ArhiviranRadniNalogDrumski, DelRadniNalogInterniSaDokumentima, DeleteRadniNalogDrumski, DeleteRadniNalogInterni, DodajStavkeURadniNalogDrumski, DodeliKamionZaRadniNalogDrumski; preko: InsertRadniNalogDrumski, InsertRadniNalogInterni
  - „Formiranje naloga“ — klik → `btnFormiranjeNaloga_Click` — proc: ArhiviranRadniNalogDrumski, DeleteRadniNalogDrumski, DodajStavkeURadniNalogDrumski, DodeliKamionZaRadniNalogDrumski, DodeliKamionZaRadniNalogDrumskiP, DodeliKamionZaViseRadnihNalogaDrumskiP; preko: InsertRadniNalogDrumski
  - „Dodaj dokument“ — klik → `button1_Click` — proc: ArhiviranRadniNalogDrumski, DeleteRadniNalogDrumski, DodajStavkeURadniNalogDrumski, DodeliKamionZaRadniNalogDrumski, DodeliKamionZaRadniNalogDrumskiP, DodeliKamionZaViseRadnihNalogaDrumskiP; preko: InsertRadniNalogDrumski
  - „Otvori dokumenta“ — klik → `button4_Click` — otvara: frmPregledFajlova
  - „Nova špedicija“ — klik → `btnPartner_Click` — otvara: frmPartnerji
  - „Potvrdi LO nalog za Transport“ — klik → `btnPovrdiLO_Click` — proc: ArhiviranRadniNalogDrumski, DeleteRadniNalogDrumski, DodajStavkeURadniNalogDrumski, DodeliKamionZaRadniNalogDrumski, DodeliKamionZaRadniNalogDrumskiP, DodeliKamionZaViseRadnihNalogaDrumskiP; preko: InsertRadniNalogDrumski
  - „Unesi“ — klik → `btnUnesiNHM_Click` — čita 19 tabela; preko: PrivremeniNHM
  - „Unesi“ — klik → `btnUnesiNHM_Click` — čita 19 tabela; preko: PrivremeniNHM

#### frmFormaZaVozace
- **Fajl:** `DrumskiApp/frmFormaZaVozace.cs`  
- **Naslov forme:** „frmFormaZaVozace“
- **Tabele:**
  - `KontejnerskiTerminali` — S
  - `MestaUtovara` — S
  - `RadniNalogDrumski` — S
  - `TipKontenjera` — S
  - `VrstaRobeADR` — S
- **Procedure:** `ArhiviranRadniNalogDrumski`, `DeleteRadniNalogDrumski`, `DodajStavkeURadniNalogDrumski`, `DodeliKamionZaRadniNalogDrumski`, `DodeliKamionZaRadniNalogDrumskiP`, `DodeliKamionZaViseRadnihNalogaDrumskiP`, `DuplirajZapisRadniNalogDrumski`, `InsertDokumentaRadniNalogDrumski`, `InsertRadniNalogDrumski`, `InsertRadniNalogDrumskiNHM`, `InsertRadniNalogDrumskiNovi`, `InsertTokens`, `NapraviRadniNalogDrumskiIzDrumskog`, `PbrisiKamionZaRadniNalogDrumski`, `UpdateKorakPotvrdeVozaca`, `UpdatePoslatStatus`, `UpdateRadniNalogDrumski`, `UpdateRadniNalogDrumskiIzvoz`, `UpdateRadniNalogDrumskiKamionID`, `UpdateRadniNalogDrumskiListaKamionID`, `UpdateRadniNalogDrumskiNovi`, `UpdateRadniNalogDrumskiOdobrioLO`, `UpdateRadniNalogDrumskiOtkazi`, `UpdateRadniNalogDrumskiPoslataNajava`, `UpdateRadniNalogDrumskiPoslateInstrukcije`, `UpdateStatusRadniNalogDrumski`
- **Funkcionalnosti (kontrola → handler):**
  - „POTVRDA“ — klik → `button2_Click` — proc: ArhiviranRadniNalogDrumski, DeleteRadniNalogDrumski, DodajStavkeURadniNalogDrumski, DodeliKamionZaRadniNalogDrumski, DodeliKamionZaRadniNalogDrumskiP, DodeliKamionZaViseRadnihNalogaDrumskiP; preko: InsertRadniNalogDrumski
  - „DODAJ DOKUMENTACIJU“ — klik → `btnDodajDokumentaciju_Click`

#### frmKontaktOsobe
- **Fajl:** `Dokumenta/frmKontaktOsobe.cs`  
- **Naslov forme:** „Kontakt osobe“
- **Modul:** Drumski transport, Intermodalni terminal, Kapija, Logistika izvoza, Podešavanja sistema, Prijem i otprema vozova
- **Tabele:**
  - `Carinarnice` — S
  - `Partnerji` — S
  - `partnerjiKontOseba` — S
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click`
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „Svi kontakti“ — klik → `toolStripButton1_Click`
  - „Svi kontakti“ — klik → `toolStripButton1_Click`
  - `this` — otvaranje forme → `frmKontaktOsobe_Load` — čita: Carinarnice, Partnerji

#### frmListaProtokola
- **Fajl:** `Drumski/frmListaProtokola.cs`  
- **Naslov forme:** „Protokoli“
- **Otvara forme:** frmProtokolTransportnogNaloga (`subOstali` (Click))
- **Tabele:**
  - `Izvoz` — S
  - `IzvozKonacna` — S
  - `Partnerji` — S
  - `ProtokolRadniNalogDrumski` — S
  - `RadniNalogDrumski` — S
  - `TipProtokola` — S
- **Procedure:** `DeleteProtokolRadniNalogDrumski`, `InsertProtokolRadniNalogDrumski`, `UpdateProtokolRadniNalogDrumski`
- **Funkcionalnosti (kontrola → handler):**
  - „Otvori“ — klik → `button1_Click` — otvara: frmProtokolTransportnogNaloga
  - „Kreiraj protokol“ — klik → `btnKreirajNovi_Click`
  - „Otvori“ — klik → `otvoriToolStripMenuItem_Click` — otvara: frmProtokolTransportnogNaloga
  - „Storniraj“ — klik → `stornirajToolStripMenuItem_Click` — proc: DeleteProtokolRadniNalogDrumski, InsertProtokolRadniNalogDrumski, UpdateProtokolRadniNalogDrumski; preko: InsertProtokolTransportnogNaloga
  - `subAutodan` — klik → `SubAutodan_Click` — otvara: frmProtokolTransportnogNaloga
  - `subDodatnaRuta` — klik → `SubDodatnaRuta_Click` — otvara: frmProtokolTransportnogNaloga
  - `subOstali` — klik → `SubOstali_Click` — otvara: frmProtokolTransportnogNaloga

#### frmNalogZaFakturisanje
- **Fajl:** `Drumski/frmNalogZaFakturisanje.cs`  
- **Naslov forme:** „frmNalogZaFakturisanje“
- **Otvara forme:** frmBrojFaktureModal („Objedinjena dokumenta po izlaznoj fakturi“)
- **Tabele:**
  - `Automobili` — S
  - `Delavci` — S
  - `DokumentaRadnogNalogaDrumski` — S
  - `Dokumenti` — S
  - `FakturaDrumski` — S
  - `FakturaDrumskiStavka` — S
  - `Izvoz` — S
  - `IzvozKonacna` — S
  - `Korisnici` — S
  - `MestaUtovara` — S
  - `Partnerji` — S
  - `PraviloArtikla` — S
  - `ProtokolRadniNalogDrumski` — S
  - `RadniNalogDrumski` — S
  - `radniNalogDrumski` — S
  - `StatusVozila` — S
  - `TipProtokola` — S
  - `Uvoz` — S
  - `UvozKonacna` — S
  - `VrstaVozila` — S
- **Procedure:** `DeleteDokumentFaktureDrumski`, `DeleteDokumentKamionaDrumski`, `DeleteDokumentVozaca`, `DeleteFakturaDrumskiStavka`, `InsertDokumentaFaktureDrumski`, `InsertDokumentaSkladiste`, `InsertDokumentaUploadedFiles`, `InsertFakturaDrumski`, `InsertFakturaDrumskiStavka`, `UpdateDokumentaFaktureDrumski`, `UpdateFakturaDrumskiStavka`, `UpdateStatusFajla`, `UpdateUlaznaFakturaStavka`
- **Funkcionalnosti (kontrola → handler):**
  - „Objedinjena dokument po broju naloga“ — klik → `btnObjedinjenaDokumentaNalogID_Click` — otvara: frmBrojFaktureModal; čita: DokumentaRadnogNalogaDrumski, radniNalogDrumski
  - „Objedinjena dokumenta po izlaznoj fakturi“ — klik → `btnObjedinjenaDokumentaIFaktura_Click` — otvara: frmBrojFaktureModal; čita: DokumentaRadnogNalogaDrumski, FakturaDrumski, FakturaDrumskiStavka, radniNalogDrumski
  - „Nalozi bez izlazne fakture“ — klik → `btnBezIzlazneFakture_Click_1`
  - „Pretraži“ — klik → `button1_Click`
  - „gridGroupingControl1“ — TableControlMouseDown → `GridGroupingControl1_TableControlMouseDown`
  - „Izađi bez promena“ — klik → `btnIzadjiBezPromena_Click_1`
  - „Potvrdi izlaznu fakturu“ — klik → `btnPotvrdi_Click`
  - „Detalji“ — klik → `detaljiToolStripMenuItem_Click`

#### frmObradaUlaznihFaktura
- **Fajl:** `Drumski/frmObradaUlaznihFaktura.cs`  
- **Naslov forme:** „frmObradaUlaznihFaktura“
- **Tabele:**
  - `Automobili` — S
  - `Delavci` — S
  - `DokumentaRadnogNalogaDrumski` — S
  - `Dokumenti` — S
  - `FakturaDrumski` — S
  - `FakturaDrumskiStavka` — S
  - `Izvoz` — S
  - `IzvozKonacna` — S
  - `Korisnici` — S
  - `MestaUtovara` — S
  - `Partnerji` — S
  - `PraviloArtikla` — S
  - `ProtokolRadniNalogDrumski` — S
  - `RadniNalogDrumski` — S
  - `StatusVozila` — S
  - `TipProtokola` — S
  - `Uvoz` — S
  - `UvozKonacna` — S
  - `VrstaVozila` — S
- **Procedure:** `ArhiviranRadniNalogDrumski`, `DeleteDokumentFaktureDrumski`, `DeleteDokumentKamionaDrumski`, `DeleteDokumentVozaca`, `DeleteFakturaDrumskiStavka`, `DeleteRadniNalogDrumski`, `DodajStavkeURadniNalogDrumski`, `DodeliKamionZaRadniNalogDrumski`, `DodeliKamionZaRadniNalogDrumskiP`, `DodeliKamionZaViseRadnihNalogaDrumskiP`, `DuplirajZapisRadniNalogDrumski`, `InsertDokumentaFaktureDrumski`, `InsertDokumentaRadniNalogDrumski`, `InsertDokumentaSkladiste`, `InsertDokumentaUploadedFiles`, `InsertFakturaDrumski`, `InsertFakturaDrumskiStavka`, `InsertRadniNalogDrumski`, `InsertRadniNalogDrumskiNHM`, `InsertRadniNalogDrumskiNovi`, `InsertTokens`, `NapraviRadniNalogDrumskiIzDrumskog`, `PbrisiKamionZaRadniNalogDrumski`, `UpdateDokumentaFaktureDrumski`, `UpdateFakturaDrumskiStavka`, `UpdateKorakPotvrdeVozaca`, `UpdatePoslatStatus`, `UpdateRadniNalogDrumski`, `UpdateRadniNalogDrumskiIzvoz`, `UpdateRadniNalogDrumskiKamionID`, `UpdateRadniNalogDrumskiListaKamionID`, `UpdateRadniNalogDrumskiNovi`, `UpdateRadniNalogDrumskiOdobrioLO`, `UpdateRadniNalogDrumskiOtkazi`, `UpdateRadniNalogDrumskiPoslataNajava`, `UpdateRadniNalogDrumskiPoslateInstrukcije`, `UpdateStatusFajla`, `UpdateStatusRadniNalogDrumski`, `UpdateUlaznaFakturaStavka`
- **Funkcionalnosti (kontrola → handler):**
  - „Nalozi  bez ulazne fakture“ — klik → `btnBezUlazneFakture_Click`
  - „Pretraži“ — klik → `button1_Click`
  - „gridGroupingControl1“ — TableControlMouseDown → `GridGroupingControl1_TableControlMouseDown`
  - „gridGroupingControl1“ — dupli klik → `gridGroupingControl1_DoubleClick`
  - „Dodaj“ — klik → `btnDodajDokumenta_Click` — proc: ArhiviranRadniNalogDrumski, DeleteRadniNalogDrumski, DodajStavkeURadniNalogDrumski, DodeliKamionZaRadniNalogDrumski, DodeliKamionZaRadniNalogDrumskiP, DodeliKamionZaViseRadnihNalogaDrumskiP; preko: InsertRadniNalogDrumski
  - „Izađi bez promena“ — klik → `button3_Click`
  - „Potvrdi ulaznu fakturu“ — klik → `button2_Click`
  - „Detalji“ — klik → `detaljiToolStripMenuItem_Click`

#### frmPartnerji
- **Fajl:** `Sifarnici/frmPartnerji.cs`  
- **Naslov forme:** „ PARTNERI“
- **Modul:** Drumski transport, Podešavanja sistema
- **Otvara forme:** frmKontaktOsobe („Kontakt osobe“), frmPartnerjiFirme („Firme“), frmKontaktOsobe („Kontakt osobe“), frmPartnerjiFirme („Firme“)
- **Tabele:**
  - `Drzave` — S
  - `Korisnici` — S
  - `Partnerji` — SU
  - `PartnerjiFirma` — S
  - `PartnerjiKOntOseba` — S
  - `Poste` — S
  - `Valute` — S
- **Procedure:** `DeleteFaktura`, `DeleteFakturaPostav`, `DeleteNosiociTroska`, `DeleteOpportunity`, `DeletePartnerji`, `DeletePredvidjanje`, `DeleteUlFak`, `DeleteUlFakPostav`, `InsertApiLogovi`, `InsertDrzave`, `InsertFaktura`, `InsertFakturaPostav`, `InsertNosiociTroskova`, `InsertOpportunity`, `InsertParnerji`, `InsertPoste`, `InsertPredvidjanje`, `InsertUlFak`, `InsertUlFakPostav`, `PoveziPredvidjanjeFaktura`, `RefreshPredvidjanja`, `UpdateFaktura`, `UpdateNosiociTroska`, `UpdateOpportunity`, `UpdatePartneri`, `UpdatePredvidjanje`, `UpdateUlFak`, `VratiPredvidjanjeStatus`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click_1` — čita: Korisnici; proc: DeletePartnerji, InsertDrzave, InsertParnerji, InsertPoste, UpdatePartneri; preko: InsertPartnerji
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeletePartnerji, InsertDrzave, InsertParnerji, InsertPoste, UpdatePartneri; preko: InsertPartnerji
  - „Kontakt osobe“ — klik → `toolStripButton1_Click`
  - „Export Pantheon“ — klik → `toolStripButton2_Click` — piše: Partnerji[U]; čita: Drzave; proc: DeleteFaktura, DeleteFakturaPostav, DeleteNosiociTroska, DeleteOpportunity, DeletePredvidjanje, DeleteUlFak; preko: InsertPatheonExport
  - „button1“ — klik → `btnDrzava_Click`
  - „Unesi poštu“ — klik → `button4_Click`
  - „Unesi državu“ — klik → `button3_Click`
  - „Unesi poštu“ — klik → `button5_Click` — proc: DeletePartnerji, InsertDrzave, InsertParnerji, InsertPoste, UpdatePartneri; preko: InsertPartnerji
  - „Mesto“ — klik → `label30_Click`
  - „Unesi državu“ — klik → `button2_Click` — proc: DeletePartnerji, InsertDrzave, InsertParnerji, InsertPoste, UpdatePartneri; preko: InsertPartnerji
  - „Potvrdi“ — klik → `button1_Click_1`
  - „--“ — klik → `btnMin_Click`
  - „Firme“ — klik → `btnFirme_Click`
  - „Kontakt osobe“ — klik → `toolStripButton1_Click`
  - `button20` — klik → `tsDelete_Click` — proc: DeletePartnerji, InsertDrzave, InsertParnerji, InsertPoste, UpdatePartneri; preko: InsertPartnerji
  - `button21` — klik → `tsSave_Click_1` — čita: Korisnici; proc: DeletePartnerji, InsertDrzave, InsertParnerji, InsertPoste, UpdatePartneri; preko: InsertPartnerji
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - `this` — otvaranje forme → `frmPartnerji_Load` — čita: Valute

#### frmPartnerjiFirme
- **Fajl:** `Sifarnici/frmPartnerjiFirme.cs`  
- **Naslov forme:** „APP šifre partnera“
- **Modul:** Drumski transport, Podešavanja sistema
- **Tabele:**
  - `Partnerji` — S
  - `PartnerjiFirma` — S
- **Funkcionalnosti (kontrola → handler):**
  - „Sve firme“ — klik → `button23_Click`
  - `this` — otvaranje forme → `frmPartnerjiFirme_Load` — čita: Partnerji

#### frmPodesavanjeRaspolozivosti
- **Fajl:** `Drumski/frmPodesavanjeRaspolozivosti.cs`  
- **Naslov forme:** „frmPodesavanjeRaspolozivosti“
- **Tabele:**
  - `Automobili` — S
  - `AutomobiliTehnickiProblem` — S
  - `Delavci` — S
  - `Partnerji` — S
  - `VrstaVozila` — S
- **Procedure:** `DeleteAutomobili`, `DeleteAutomobiliServis`, `DeleteAutomobiliTehnickiProblem`, `InsertAutomobili`, `InsertAutomobiliDrumski`, `InsertAutomobiliServis`, `InsertAutomobiliTehnickiProblem`, `UpdateAutomobili`, `UpdateAutomobiliDrumski`, `UpdateAutomobiliServis`
- **Funkcionalnosti (kontrola → handler):**
  - „VRATI U RASPOLOŽIVE“ — klik → `button1_Click` — proc: DeleteAutomobili, DeleteAutomobiliServis, DeleteAutomobiliTehnickiProblem, InsertAutomobili, InsertAutomobiliDrumski, InsertAutomobiliServis; preko: InsertAutomobili
  - „OZNAČI KAO NERASPOLOŽIVE“ — klik → `button2_Click` — čita: AutomobiliTehnickiProblem; proc: DeleteAutomobili, DeleteAutomobiliServis, DeleteAutomobiliTehnickiProblem, InsertAutomobili, InsertAutomobiliDrumski, InsertAutomobiliServis; preko: InsertAutomobili
  - „Filtriraj“ — klik → `btnFiltriraj_Click`

#### frmPregledFajlova
- **Fajl:** `Drumski/FormaPregledFajlova.cs`  
- **Naslov forme:** „frmPregledFajlova“
- **Tabele:**
  - `DokumentaRadnogNalogaDrumski` — S

#### frmPregledNalogaDrumski
- **Fajl:** `Drumski/frmPregledNalogaDrumski.cs`  
- **Naslov forme:** „Lista naloga“
- **Otvara forme:** frmDrumski („Otvori“), frmFormaZaVozace („Vozac“)
- **Tabele:**
  - `Automobili` — S
  - `Carinarnice` — S
  - `Delavci` — S
  - `Izvoz` — S
  - `IzvozKonacna` — S
  - `IzvozKonacnaZaglavlje` — S
  - `Korisnici` — S
  - `NapomenaZaPozicioniranje` — S
  - `Partnerji` — S
  - `partnerjiKontOsebaMU` — S
  - `RadniNalogDrumski` — S
  - `Radninaloginterni` — S
  - `SistemskePostavke` — S
  - `StatusVozila` — S
  - `TipKontenjera` — S
  - `Uvoz` — S
  - `UvozKonacna` — S
  - `UvozKonacnaZaglavlje` — S
  - `Voz` — S
  - `VrstaCarinskogPostupka` — S
  - `VrstePostupakaUvoz` — S
- **Procedure:** `ArhiviranRadniNalogDrumski`, `DelRadniNalogInterniSaDokumentima`, `DeleteProdajniNalogIzvoz`, `DeleteRadniNalogDrumski`, `DeleteRadniNalogInterni`, `DeleteValute`, `DodajStavkeURadniNalogDrumski`, `DodeliKamionZaRadniNalogDrumski`, `DodeliKamionZaRadniNalogDrumskiP`, `DodeliKamionZaViseRadnihNalogaDrumskiP`, `DozvoliIzlaz`, `DuplirajZapisRadniNalogDrumski`, `InsertDokumentaRadniNalogDrumski`, `InsertProdajniNalogIzvoz`, `InsertProdajniNalogIzvozStavke`, `InsertRadniNalogDrumski`, `InsertRadniNalogDrumskiNHM`, `InsertRadniNalogDrumskiNovi`, `InsertRadniNalogInterni`, `InsertRadniNalogInterni2`, `InsertRadniNalogInterniIzvoz`, `InsertRadniNalogInterniIzvoz2`, `InsertRadniNalogInterniIzvozDodatne`, `InsertRadniNalogInterniPotvrda`, `InsertRadniNalogInterniSkladistePotvrda`, `InsertRadniNalogInterniStorno`, `InsertTokens`, `NapraviRadniNalogDrumskiIzDrumskog`, `ObavljenPregled`, `PbrisiKamionZaRadniNalogDrumski`, `PromeniStatusKalmar`, `PromeniStatusKapijaOdlazak`, `PromeniStatusPretovareno`, `PromeniStatusPrijemPotvrdioKapiju`, `PromeniStatusPrijemPotvrdioVP`, `UpdateKorakPotvrdeVozaca`, `UpdatePoslatStatus`, `UpdateProdajniNalogIzvoz`, `UpdateProdajniNalogIzvozStavke`, `UpdateProdajniNalogStavkeDrumski`, `UpdateProdajniNalogStavkeKolicina`, `UpdateProdajniNalogStavkeKolicinaVecFormirano`, `UpdateProdajniNalogStavkeScenario`, `UpdateProdajniNalogStavkeStorno`, `UpdateRNIVozFormiran`, `UpdateRadniNalogDrumski`, `UpdateRadniNalogDrumskiIzvoz`, `UpdateRadniNalogDrumskiKamionID`, `UpdateRadniNalogDrumskiListaKamionID`, `UpdateRadniNalogDrumskiNovi`, `UpdateRadniNalogDrumskiOdobrioLO`, `UpdateRadniNalogDrumskiOtkazi`, `UpdateRadniNalogDrumskiPoslataNajava`, `UpdateRadniNalogDrumskiPoslateInstrukcije`, `UpdateRadniNalogInterni`, `UpdateRadniNalogInterniFormiran`, `UpdateRadniNalogInterniGenerisan`, `UpdateRadniNalogInterniZavrsen`, `UpdateStatusRadniNalogDrumski`, `updRadniNalogInterniPrijemnicaPotvrdjen`, `updRadniNalogInterniRukovalacPotvrdjen`, `updRadniNalogInterniSkladistarPotvrdio`, `updRadniNalogInterniSkladistePotvrdjen`
- **Funkcionalnosti (kontrola → handler):**
  - „Potvrdi LO nalog za Transport“ — klik → `btnOdobrioLO_Click` — proc: ArhiviranRadniNalogDrumski, DeleteRadniNalogDrumski, DodajStavkeURadniNalogDrumski, DodeliKamionZaRadniNalogDrumski, DodeliKamionZaRadniNalogDrumskiP, DodeliKamionZaViseRadnihNalogaDrumskiP; preko: InsertRadniNalogDrumski
  - „Dupliraj zapis“ — klik → `button6_Click` — čita 14 tabela; proc: ArhiviranRadniNalogDrumski, DeleteRadniNalogDrumski, DodajStavkeURadniNalogDrumski, DodeliKamionZaRadniNalogDrumski, DodeliKamionZaRadniNalogDrumskiP, DodeliKamionZaViseRadnihNalogaDrumskiP; preko: InsertRadniNalogDrumski
  - „Dopuna naloga“ — klik → `btnDopunaNaloga_Click_1` — proc: ArhiviranRadniNalogDrumski, DeleteRadniNalogDrumski, DodajStavkeURadniNalogDrumski, DodeliKamionZaRadniNalogDrumski, DodeliKamionZaRadniNalogDrumskiP, DodeliKamionZaViseRadnihNalogaDrumskiP; preko: InsertRadniNalogDrumski
  - „Formiranje naloga“ — klik → `btnFormiranjeNaloga_Click` — proc: ArhiviranRadniNalogDrumski, DeleteRadniNalogDrumski, DodajStavkeURadniNalogDrumski, DodeliKamionZaRadniNalogDrumski, DodeliKamionZaRadniNalogDrumskiP, DodeliKamionZaViseRadnihNalogaDrumskiP; preko: InsertRadniNalogDrumski
  - „Promena statusa“ — klik → `button3_Click` — čita: StatusVozila
  - „Dodeli kamion“ — klik → `button2_Click` — otvara: PakovanjeKamiona1
  - „Otvori“ — klik → `button23_Click` — otvara: frmDrumski1, frmDrumski
  - „Otvori“ — klik → `button23_Click` — otvara: frmDrumski1, frmDrumski
  - „Dupliraj zapis“ — klik → `button6_Click` — čita 14 tabela; proc: ArhiviranRadniNalogDrumski, DeleteRadniNalogDrumski, DodajStavkeURadniNalogDrumski, DodeliKamionZaRadniNalogDrumski, DodeliKamionZaRadniNalogDrumskiP, DodeliKamionZaViseRadnihNalogaDrumskiP; preko: InsertRadniNalogDrumski
  - „Save“ — klik → `btnSnimi_Click` — čita: SistemskePostavke; proc: ArhiviranRadniNalogDrumski, DelRadniNalogInterniSaDokumentima, DeleteRadniNalogDrumski, DeleteRadniNalogInterni, DodajStavkeURadniNalogDrumski, DodeliKamionZaRadniNalogDrumski; preko: InsertRadniNalogDrumski, InsertRadniNalogInterni
  - „Close“ — klik → `button4_Click`
  - „gridGroupingControl1“ — TableControlMouseDown → `GridGroupingControl1_TableControlMouseDown`
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick` — proc: ArhiviranRadniNalogDrumski, DeleteProdajniNalogIzvoz, DeleteRadniNalogDrumski, DeleteValute, DodajStavkeURadniNalogDrumski, DodeliKamionZaRadniNalogDrumski; preko: InsertProdajniNalogIzvoz, InsertRadniNalogDrumski
  - „Vozac“ — klik → `button2_Click_1` — otvara: frmFormaZaVozace
  - „gridGroupingControl1“ — TableControlCurrentCellAcceptedChanges → `GridGroupingControl1_TableControlCurrentCellAcceptedChanges` — proc: ArhiviranRadniNalogDrumski, DelRadniNalogInterniSaDokumentima, DeleteRadniNalogDrumski, DeleteRadniNalogInterni, DodajStavkeURadniNalogDrumski, DodeliKamionZaRadniNalogDrumski; preko: InsertRadniNalogDrumski, InsertRadniNalogInterni

#### frmPregledPorukeVozacu
- **Fajl:** `Drumski/frmPregledPorukeVozacu.cs`  
- **Naslov forme:** „frmPregledPorukeVozacu“
- **Tabele:**
  - `Delavci` — S
  - `Korisnici` — S
- **Procedure:** `ArhiviranRadniNalogDrumski`, `DeleteRadniNalogDrumski`, `DodajStavkeURadniNalogDrumski`, `DodeliKamionZaRadniNalogDrumski`, `DodeliKamionZaRadniNalogDrumskiP`, `DodeliKamionZaViseRadnihNalogaDrumskiP`, `DuplirajZapisRadniNalogDrumski`, `InsertDokumentaRadniNalogDrumski`, `InsertRadniNalogDrumski`, `InsertRadniNalogDrumskiNHM`, `InsertRadniNalogDrumskiNovi`, `InsertTokens`, `NapraviRadniNalogDrumskiIzDrumskog`, `PbrisiKamionZaRadniNalogDrumski`, `UpdateKorakPotvrdeVozaca`, `UpdatePoslatStatus`, `UpdateRadniNalogDrumski`, `UpdateRadniNalogDrumskiIzvoz`, `UpdateRadniNalogDrumskiKamionID`, `UpdateRadniNalogDrumskiListaKamionID`, `UpdateRadniNalogDrumskiNovi`, `UpdateRadniNalogDrumskiOdobrioLO`, `UpdateRadniNalogDrumskiOtkazi`, `UpdateRadniNalogDrumskiPoslataNajava`, `UpdateRadniNalogDrumskiPoslateInstrukcije`, `UpdateStatusRadniNalogDrumski`
- **Funkcionalnosti (kontrola → handler):**
  - „Pošalji“ — klik → `button1_Click` — proc: ArhiviranRadniNalogDrumski, DeleteRadniNalogDrumski, DodajStavkeURadniNalogDrumski, DodeliKamionZaRadniNalogDrumski, DodeliKamionZaRadniNalogDrumskiP, DodeliKamionZaViseRadnihNalogaDrumskiP; preko: InsertRadniNalogDrumski
  - „Zatvori“ — klik → `button2_Click`

#### frmProtokolTransportnogNaloga
- **Fajl:** `Drumski/frmProtokolTransportnogNaloga.cs`  
- **Naslov forme:** „Protokol transportnog naloga“
- **Tabele:**
  - `Automobili` — S
  - `Carinarnice` — S
  - `Izvoz` — S
  - `IzvozKonacna` — S
  - `Korisnici` — S
  - `MestaUtovara` — S
  - `Partnerji` — S
  - `partnerjiKontOsebaMU` — S
  - `PodtipProtokolaRazno` — S
  - `ProtokolRadniNalogDrumski` — S
  - `RadniNalogDrumski` — S
- **Procedure:** `DeleteProtokolRadniNalogDrumski`, `InsertProtokolRadniNalogDrumski`, `UpdateProtokolRadniNalogDrumski`
- **Funkcionalnosti (kontrola → handler):**
  - `button1` — klik → `button1_Click` — proc: DeleteProtokolRadniNalogDrumski, InsertProtokolRadniNalogDrumski, UpdateProtokolRadniNalogDrumski; preko: InsertProtokolTransportnogNaloga
  - `btnSave` — klik → `btnSave_Click` — proc: DeleteProtokolRadniNalogDrumski, InsertProtokolRadniNalogDrumski, UpdateProtokolRadniNalogDrumski; preko: InsertProtokolTransportnogNaloga

#### frmStatus
- **Fajl:** `Drumski/frmStatus.cs`  
- **Naslov forme:** „frmStatus“
- **Otvara forme:** frmPregledFajlova (`dataGridView3` (CellClick))
- **Tabele:**
  - `Automobili` — S
  - `AutomobiliTehnickiProblem` — S
  - `Carinarnice` — S
  - `Delavci` — S
  - `DokumentaRadnogNalogaDrumski` — S
  - `Dokumenti` — S
  - `DrumskiPozicioniranje` — S
  - `Izvoz` — S
  - `IzvozKonacna` — S
  - `izvozkonacna` — S
  - `KontejnerskiTerminali` — S
  - `Korisnici` — S
  - `MapiranjeTipaNaloga` — S
  - `MestaUtovara` — S
  - `NapomenaZaPozicioniranje` — S
  - `Partnerji` — S
  - `partnerjiKontOsebaMU` — S
  - `RadniNalogDrumski` — S
  - `RadninalogDrumski` — S
  - `RadniNalogInterni` — S
  - `ScenarijaTokaTransporta` — S
  - `ScenarioTokTransporta_Statusi` — S
  - `SistemskePostavke` — S
  - `StatusKapija` — S
  - `StatusVozila` — S
  - `TipKontenjera` — S
  - `UploadTokens` — S
  - `Uvoz` — S
  - `UvozKonacna` — S
  - `VrstaManipulacije` — S
  - `VrstaVozila` — S
- **Procedure:** `ArhiviranRadniNalogDrumski`, `DelRadniNalogInterniSaDokumentima`, `DeleteIzvozKonacnaNHM`, `DeleteIzvozKonacnaVrstaManipulacije`, `DeleteIzvozKonacnaVrstaRobeHS`, `DeleteIzvozNHM`, `DeleteIzvozSve`, `DeleteIzvozVrstaManipulacije`, `DeleteIzvozVrstaRobeHS`, `DeleteKontejnerIzvoz`, `DeleteRadniNalogDrumski`, `DeleteRadniNalogInterni`, `DeleteStatusKapija`, `DeleteStatusVozila`, `DeleteVaganje`, `DodajStavkeURadniNalogDrumski`, `DodeliKamionZaRadniNalogDrumski`, `DodeliKamionZaRadniNalogDrumskiP`, `DodeliKamionZaViseRadnihNalogaDrumskiP`, `DozvoliIzlaz`, `DuplirajZapisRadniNalogDrumski`, `InsertDokumentaRadniNalogDrumski`, `InsertIzvozKonacnaNHM`, `InsertIzvozKonacnaVMIzUvoza`, `InsertIzvozKonacnaVrstaManipulacije`, `InsertIzvozNHM`, `InsertIzvozPorudzbenica`, `InsertIzvozVMIzUvoza`, `InsertIzvozVrstaManipulacije`, `InsertIzvozVrstaRobeHS`, `InsertIzvozkonacnaVrstaRobeHS`, `InsertKontejnerLog`, `InsertRadniNalogDrumski`, `InsertRadniNalogDrumskiNHM`, `InsertRadniNalogDrumskiNovi`, `InsertRadniNalogInterni`, `InsertRadniNalogInterni2`, `InsertRadniNalogInterniIzvoz`, `InsertRadniNalogInterniIzvoz2`, `InsertRadniNalogInterniIzvozDodatne`, `InsertRadniNalogInterniPotvrda`, `InsertRadniNalogInterniSkladistePotvrda`, `InsertRadniNalogInterniStorno`, `InsertStatusKapija`, `InsertStatusVozila`, `InsertTokens`, `InsertVaganje`, `KopirajKontejnerIzvoz`, `NapraviRadniNalogDrumskiIzDrumskog`, `ObavljenPregled`, `PbrisiKamionZaRadniNalogDrumski`, `PromeniStatusKalmar`, `PromeniStatusKapijaOdlazak`, `PromeniStatusPretovareno`, `PromeniStatusPrijemPotvrdioKapiju`, `PromeniStatusPrijemPotvrdioVP`, `UpdCenaIzvozKonacnaVrstaManipulacije`, `UpdCenaIzvozVrstaManipulacije`, `UpdPDVIzvozKonacnaVrstaManipulacije`, `UpdPDVIzvozVrstaManipulacije`, `UpdPlatiocaIzvozKonacnaVrstaManipulacije`, `UpdPlatiocaIzvozVrstaManipulacije`, `UpdateIzvoz`, `UpdateIzvozKonacna`, `UpdateIzvozKonacnaVaganjeCirada`, `UpdateIzvozKonacnaVaganjePlatforma`, `UpdateIzvozPorudzbenica`, `UpdateIzvozPorudzbenicaCarina`, `UpdateIzvozPorudzbenicaPojedinacni`, `UpdateIzvozPorudzbenicaRelacija`, `UpdateIzvozStatus`, `UpdateIzvozStatusTerminal`, `UpdateIzvozTerminalIzbor`, `UpdateKorakPotvrdeVozaca`, `UpdatePoslatStatus`, `UpdateRNIVozFormiran`, `UpdateRadniNalogDrumski`, `UpdateRadniNalogDrumskiIzvoz`, `UpdateRadniNalogDrumskiKamionID`, `UpdateRadniNalogDrumskiListaKamionID`, `UpdateRadniNalogDrumskiNovi`, `UpdateRadniNalogDrumskiOdobrioLO`, `UpdateRadniNalogDrumskiOtkazi`, `UpdateRadniNalogDrumskiPoslataNajava`, `UpdateRadniNalogDrumskiPoslateInstrukcije`, `UpdateRadniNalogInterni`, `UpdateRadniNalogInterniFormiran`, `UpdateRadniNalogInterniGenerisan`, `UpdateRadniNalogInterniZavrsen`, `UpdateStatusKapija`, `UpdateStatusRadniNalogDrumski`, `UpdateStatusVozila`, `UpdateVaganje`, `spPrenesiKontejnerIzNerasporedjenihUPrijPlatforma`, `updRadniNalogInterniPrijemnicaPotvrdjen`, `updRadniNalogInterniRukovalacPotvrdjen`, `updRadniNalogInterniSkladistarPotvrdio`, `updRadniNalogInterniSkladistePotvrdjen`
- **Funkcionalnosti (kontrola → handler):**
  - `button20` — klik → `tsDelete_Click` — proc: DeleteStatusKapija, DeleteStatusVozila, InsertStatusKapija, InsertStatusVozila, UpdateStatusKapija, UpdateStatusVozila; preko: InsertStatus
  - `button21` — klik → `tsSave_Click` — proc: DeleteStatusKapija, DeleteStatusVozila, InsertStatusKapija, InsertStatusVozila, UpdateStatusKapija, UpdateStatusVozila; preko: InsertStatus
  - `dataGridView3` — promena vrednosti → `dataGridView3_CellValueChanged` — proc: ArhiviranRadniNalogDrumski, DelRadniNalogInterniSaDokumentima, DeleteRadniNalogDrumski, DeleteRadniNalogInterni, DodajStavkeURadniNalogDrumski, DodeliKamionZaRadniNalogDrumski; preko: InsertRadniNalogDrumski, InsertRadniNalogInterni
  - „Posalji statuse“ — klik → `btnPosaljiStatuse_Click` — proc: ArhiviranRadniNalogDrumski, DeleteRadniNalogDrumski, DodajStavkeURadniNalogDrumski, DodeliKamionZaRadniNalogDrumski, DodeliKamionZaRadniNalogDrumskiP, DodeliKamionZaViseRadnihNalogaDrumskiP; preko: InsertRadniNalogDrumski
  - „Filtriraj“ — klik → `btnFitriraj_Click`
  - `dataGridView3` — klik na ćeliju → `dataGridView3_CellContentClick` — otvara: frmPregledFajlova; proc: ArhiviranRadniNalogDrumski, DeleteRadniNalogDrumski, DodajStavkeURadniNalogDrumski, DodeliKamionZaRadniNalogDrumski, DodeliKamionZaRadniNalogDrumskiP, DodeliKamionZaViseRadnihNalogaDrumskiP; preko: InsertRadniNalogDrumski

#### NalogZaDrumski
- **Fajl:** `MainLeget/Drumski/NalogZaDrumski.cs`  
- **Naslov forme:** „NalogZaDrumski“
- **Otvara forme:** frmDrumski1 („Kreiraj novi“), frmPregledNalogaDrumski („TRANSPORTNI NALOZI IZVOZ“)
- **Funkcionalnosti (kontrola → handler):**
  - „Kreiraj novi“ — klik → `btnNovi_Click` — otvara: frmDrumski1
  - „Izmeni postojeći“ — klik → `btnIzmeni_Click` — otvara: frmPregledNalogaDrumski
  - „Otkaži postojeći“ — klik → `btnOtkazi_Click` — otvara: frmPregledNalogaDrumski
  - „TRANSPORTNI NALOZI UVOZ“ — klik → `btnTransportUvoz_Click` — otvara: frmPregledNalogaDrumski
  - „TRANSPORTNI NALOZI IZVOZ“ — klik → `btnTransportIzvoz_Click` — otvara: frmPregledNalogaDrumski
  - „TRANSPORTNI NALOZI DIREKTNI (3P)“ — klik → `btnTransportDirektni_Click` — otvara: frmPregledNalogaDrumski

#### PakovanjeKamiona1
- **Fajl:** `Drumski/PakovanjeKamiona1.cs`  
- **Naslov forme:** „PakovanjeKamiona1“
- **Tabele:**
  - `Automobili` — S
  - `AutomobiliTehnickiProblem` — S
  - `Carinarnice` — S
  - `Delavci` — S
  - `DrumskiPozicioniranje` — S
  - `FakturaDrumski` — S
  - `Izvoz` — S
  - `IzvozKonacna` — S
  - `izvozkonacna` — S
  - `KontejnerskiTerminali` — S
  - `Korisnici` — S
  - `MapiranjeTipaNaloga` — S
  - `MestaUtovara` — S
  - `NapomenaZaPozicioniranje` — S
  - `Partnerji` — S
  - `partnerjiKontOsebaMU` — S
  - `ProtokolRadniNalogDrumski` — S
  - `RadniNalogDrumski` — S
  - `RadninalogDrumski` — S
  - `RadniNalogInterni` — S
  - `ScenarijaTokaTransporta` — S
  - `ScenarioTokTransporta_Statusi` — S
  - `SistemskePostavke` — S
  - `StatusVozila` — S
  - `TipKontenjera` — S
  - `UploadTokens` — S
  - `Uvoz` — S
  - `UvozKonacna` — S
  - `VrstaManipulacije` — S
  - `VrstaVozila` — S
- **Procedure:** `ArhiviranRadniNalogDrumski`, `DelRadniNalogInterniSaDokumentima`, `DeleteDokumentFaktureDrumski`, `DeleteDokumentKamionaDrumski`, `DeleteDokumentVozaca`, `DeleteFakturaDrumskiStavka`, `DeleteIzvozKonacnaNHM`, `DeleteIzvozKonacnaVrstaManipulacije`, `DeleteIzvozKonacnaVrstaRobeHS`, `DeleteIzvozNHM`, `DeleteIzvozSve`, `DeleteIzvozVrstaManipulacije`, `DeleteIzvozVrstaRobeHS`, `DeleteKontejnerIzvoz`, `DeleteRadniNalogDrumski`, `DeleteRadniNalogInterni`, `DeleteVaganje`, `DodajStavkeURadniNalogDrumski`, `DodeliKamionZaRadniNalogDrumski`, `DodeliKamionZaRadniNalogDrumskiP`, `DodeliKamionZaViseRadnihNalogaDrumskiP`, `DozvoliIzlaz`, `DuplirajZapisRadniNalogDrumski`, `InsertDokumentaFaktureDrumski`, `InsertDokumentaRadniNalogDrumski`, `InsertDokumentaSkladiste`, `InsertDokumentaUploadedFiles`, `InsertFakturaDrumski`, `InsertFakturaDrumskiStavka`, `InsertIzvozKonacnaNHM`, `InsertIzvozKonacnaVMIzUvoza`, `InsertIzvozKonacnaVrstaManipulacije`, `InsertIzvozNHM`, `InsertIzvozPorudzbenica`, `InsertIzvozVMIzUvoza`, `InsertIzvozVrstaManipulacije`, `InsertIzvozVrstaRobeHS`, `InsertIzvozkonacnaVrstaRobeHS`, `InsertKontejnerLog`, `InsertRadniNalogDrumski`, `InsertRadniNalogDrumskiNHM`, `InsertRadniNalogDrumskiNovi`, `InsertRadniNalogInterni`, `InsertRadniNalogInterni2`, `InsertRadniNalogInterniIzvoz`, `InsertRadniNalogInterniIzvoz2`, `InsertRadniNalogInterniIzvozDodatne`, `InsertRadniNalogInterniPotvrda`, `InsertRadniNalogInterniSkladistePotvrda`, `InsertRadniNalogInterniStorno`, `InsertTokens`, `InsertVaganje`, `KopirajKontejnerIzvoz`, `NapraviRadniNalogDrumskiIzDrumskog`, `ObavljenPregled`, `PbrisiKamionZaRadniNalogDrumski`, `PromeniStatusKalmar`, `PromeniStatusKapijaOdlazak`, `PromeniStatusPretovareno`, `PromeniStatusPrijemPotvrdioKapiju`, `PromeniStatusPrijemPotvrdioVP`, `UpdCenaIzvozKonacnaVrstaManipulacije`, `UpdCenaIzvozVrstaManipulacije`, `UpdPDVIzvozKonacnaVrstaManipulacije`, `UpdPDVIzvozVrstaManipulacije`, `UpdPlatiocaIzvozKonacnaVrstaManipulacije`, `UpdPlatiocaIzvozVrstaManipulacije`, `UpdateDokumentaFaktureDrumski`, `UpdateFakturaDrumskiStavka`, `UpdateIzvoz`, `UpdateIzvozKonacna`, `UpdateIzvozKonacnaVaganjeCirada`, `UpdateIzvozKonacnaVaganjePlatforma`, `UpdateIzvozPorudzbenica`, `UpdateIzvozPorudzbenicaCarina`, `UpdateIzvozPorudzbenicaPojedinacni`, `UpdateIzvozPorudzbenicaRelacija`, `UpdateIzvozStatus`, `UpdateIzvozStatusTerminal`, `UpdateIzvozTerminalIzbor`, `UpdateKorakPotvrdeVozaca`, `UpdatePoslatStatus`, `UpdateRNIVozFormiran`, `UpdateRadniNalogDrumski`, `UpdateRadniNalogDrumskiIzvoz`, `UpdateRadniNalogDrumskiKamionID`, `UpdateRadniNalogDrumskiListaKamionID`, `UpdateRadniNalogDrumskiNovi`, `UpdateRadniNalogDrumskiOdobrioLO`, `UpdateRadniNalogDrumskiOtkazi`, `UpdateRadniNalogDrumskiPoslataNajava`, `UpdateRadniNalogDrumskiPoslateInstrukcije`, `UpdateRadniNalogInterni`, `UpdateRadniNalogInterniFormiran`, `UpdateRadniNalogInterniGenerisan`, `UpdateRadniNalogInterniZavrsen`, `UpdateStatusFajla`, `UpdateStatusRadniNalogDrumski`, `UpdateUlaznaFakturaStavka`, `UpdateVaganje`, `spPrenesiKontejnerIzNerasporedjenihUPrijPlatforma`, `updRadniNalogInterniPrijemnicaPotvrdjen`, `updRadniNalogInterniRukovalacPotvrdjen`, `updRadniNalogInterniSkladistarPotvrdio`, `updRadniNalogInterniSkladistePotvrdjen`
- **Funkcionalnosti (kontrola → handler):**
  - „R“ — čekiranje → `chkR_CheckedChanged`
  - „N“ — čekiranje → `chkR_CheckedChanged`
  - „Osveži prikaz“ — klik → `btnResetuj_Click`
  - „Pregled naloga“ — klik → `toolStripItemPregledNaloga_Click` — otvara: frmPregledNalogaDrumski
  - „Ukloni dodeljen kamion“ — klik → `ukloniDodeljenKamionToolStripMenuItem_Click`
  - „Kreiraj nov nalog“ — klik → `btnKreiraj_Click` — otvara: frmDrumski1
  - `dataGridView3` — promena vrednosti → `dataGridView3_CellValueChanged` — proc: ArhiviranRadniNalogDrumski, DelRadniNalogInterniSaDokumentima, DeleteRadniNalogDrumski, DeleteRadniNalogInterni, DodajStavkeURadniNalogDrumski, DodeliKamionZaRadniNalogDrumski; preko: InsertRadniNalogDrumski, InsertRadniNalogInterni
  - „ARHIVIRAJ“ — klik → `btnArhiva_Click` — proc: ArhiviranRadniNalogDrumski, DeleteRadniNalogDrumski, DodajStavkeURadniNalogDrumski, DodeliKamionZaRadniNalogDrumski, DodeliKamionZaRadniNalogDrumskiP, DodeliKamionZaViseRadnihNalogaDrumskiP; preko: InsertRadniNalogDrumski
  - „UPARI KAMION I KONTEJNER“ — klik → `button2_Click` — proc: ArhiviranRadniNalogDrumski, DeleteRadniNalogDrumski, DodajStavkeURadniNalogDrumski, DodeliKamionZaRadniNalogDrumski, DodeliKamionZaRadniNalogDrumskiP, DodeliKamionZaViseRadnihNalogaDrumskiP; preko: InsertRadniNalogDrumski
  - „VRATI U NERASPOREĐENE“ — klik → `button1_Click` — proc: ArhiviranRadniNalogDrumski, DeleteRadniNalogDrumski, DodajStavkeURadniNalogDrumski, DodeliKamionZaRadniNalogDrumski, DodeliKamionZaRadniNalogDrumskiP, DodeliKamionZaViseRadnihNalogaDrumskiP; preko: InsertRadniNalogDrumski
  - „NAJAVA VOZILA“ — klik → `btnNajava_Click` — proc: ArhiviranRadniNalogDrumski, DeleteDokumentFaktureDrumski, DeleteDokumentKamionaDrumski, DeleteDokumentVozaca, DeleteFakturaDrumskiStavka, DeleteRadniNalogDrumski; preko: InsertFakture, InsertRadniNalogDrumski
  - „PROTOKOL“ — klik → `button3_Click` — otvara: frmListaProtokola
  - `dataGridView3` — klik na ćeliju → `dataGridView3_CellContentClick` — otvara: frmPregledPorukeVozacu, frmPregledFajlova; proc: ArhiviranRadniNalogDrumski, DeleteRadniNalogDrumski, DodajStavkeURadniNalogDrumski, DodeliKamionZaRadniNalogDrumski, DodeliKamionZaRadniNalogDrumskiP, DodeliKamionZaViseRadnihNalogaDrumskiP; preko: InsertRadniNalogDrumski

#### PakovanjeKamionaCerade
- **Fajl:** `Drumski/PakovanjeKamionaCerade.cs`  
- **Naslov forme:** „PakovanjeKamionaCerade“
- **Otvara forme:** frmListaProtokola („PROTOKOL“), frmPregledPorukeVozacu (`dataGridView3` (CellClick))
- **Tabele:**
  - `Automobili` — S
  - `AutomobiliTehnickiProblem` — S
  - `Carinarnice` — S
  - `Delavci` — S
  - `DrumskiPozicioniranje` — S
  - `FakturaDrumski` — S
  - `Izvoz` — S
  - `IzvozKonacna` — S
  - `Korisnici` — S
  - `MapiranjeTipaNaloga` — S
  - `MestaUtovara` — S
  - `NapomenaZaPozicioniranje` — S
  - `Partnerji` — S
  - `partnerjiKontOsebaMU` — S
  - `ProtokolRadniNalogDrumski` — S
  - `RadniNalogDrumski` — S
  - `RadniNalogInterni` — S
  - `ScenarijaTokaTransporta` — S
  - `ScenarioTokTransporta_Statusi` — S
  - `SistemskePostavke` — S
  - `StatusVozila` — S
  - `TipKontenjera` — S
  - `UploadTokens` — S
  - `Uvoz` — S
  - `UvozKonacna` — S
  - `VrstaManipulacije` — S
  - `VrstaVozila` — S
- **Procedure:** `ArhiviranRadniNalogDrumski`, `DelRadniNalogInterniSaDokumentima`, `DeleteDokumentFaktureDrumski`, `DeleteDokumentKamionaDrumski`, `DeleteDokumentVozaca`, `DeleteFakturaDrumskiStavka`, `DeleteRadniNalogDrumski`, `DeleteRadniNalogInterni`, `DodajStavkeURadniNalogDrumski`, `DodeliKamionZaRadniNalogDrumski`, `DodeliKamionZaRadniNalogDrumskiP`, `DodeliKamionZaViseRadnihNalogaDrumskiP`, `DozvoliIzlaz`, `DuplirajZapisRadniNalogDrumski`, `InsertDokumentaFaktureDrumski`, `InsertDokumentaRadniNalogDrumski`, `InsertDokumentaSkladiste`, `InsertDokumentaUploadedFiles`, `InsertFakturaDrumski`, `InsertFakturaDrumskiStavka`, `InsertRadniNalogDrumski`, `InsertRadniNalogDrumskiNHM`, `InsertRadniNalogDrumskiNovi`, `InsertRadniNalogInterni`, `InsertRadniNalogInterni2`, `InsertRadniNalogInterniIzvoz`, `InsertRadniNalogInterniIzvoz2`, `InsertRadniNalogInterniIzvozDodatne`, `InsertRadniNalogInterniPotvrda`, `InsertRadniNalogInterniSkladistePotvrda`, `InsertRadniNalogInterniStorno`, `InsertTokens`, `NapraviRadniNalogDrumskiIzDrumskog`, `ObavljenPregled`, `PbrisiKamionZaRadniNalogDrumski`, `PromeniStatusKalmar`, `PromeniStatusKapijaOdlazak`, `PromeniStatusPretovareno`, `PromeniStatusPrijemPotvrdioKapiju`, `PromeniStatusPrijemPotvrdioVP`, `UpdateDokumentaFaktureDrumski`, `UpdateFakturaDrumskiStavka`, `UpdateKorakPotvrdeVozaca`, `UpdatePoslatStatus`, `UpdateRNIVozFormiran`, `UpdateRadniNalogDrumski`, `UpdateRadniNalogDrumskiIzvoz`, `UpdateRadniNalogDrumskiKamionID`, `UpdateRadniNalogDrumskiListaKamionID`, `UpdateRadniNalogDrumskiNovi`, `UpdateRadniNalogDrumskiOdobrioLO`, `UpdateRadniNalogDrumskiOtkazi`, `UpdateRadniNalogDrumskiPoslataNajava`, `UpdateRadniNalogDrumskiPoslateInstrukcije`, `UpdateRadniNalogInterni`, `UpdateRadniNalogInterniFormiran`, `UpdateRadniNalogInterniGenerisan`, `UpdateRadniNalogInterniZavrsen`, `UpdateStatusFajla`, `UpdateStatusRadniNalogDrumski`, `UpdateUlaznaFakturaStavka`, `updRadniNalogInterniPrijemnicaPotvrdjen`, `updRadniNalogInterniRukovalacPotvrdjen`, `updRadniNalogInterniSkladistarPotvrdio`, `updRadniNalogInterniSkladistePotvrdjen`
- **Funkcionalnosti (kontrola → handler):**
  - „R“ — čekiranje → `chkR_CheckedChanged`
  - „N“ — čekiranje → `chkR_CheckedChanged`
  - „Osveži prikaz“ — klik → `btnResetuj_Click`
  - „Pregled naloga“ — klik → `toolStripItemPregledNaloga_Click` — otvara: frmPregledNalogaDrumski
  - „Ukloni dodeljen kamion“ — klik → `ukloniDodeljenKamionToolStripMenuItem_Click`
  - „Kreiraj nov nalog“ — klik → `btnKreiraj_Click` — otvara: frmDrumski1
  - `dataGridView3` — promena vrednosti → `dataGridView3_CellValueChanged` — proc: ArhiviranRadniNalogDrumski, DelRadniNalogInterniSaDokumentima, DeleteRadniNalogDrumski, DeleteRadniNalogInterni, DodajStavkeURadniNalogDrumski, DodeliKamionZaRadniNalogDrumski; preko: InsertRadniNalogDrumski, InsertRadniNalogInterni
  - „ARHIVIRAJ“ — klik → `btnArhiva_Click` — proc: ArhiviranRadniNalogDrumski, DeleteRadniNalogDrumski, DodajStavkeURadniNalogDrumski, DodeliKamionZaRadniNalogDrumski, DodeliKamionZaRadniNalogDrumskiP, DodeliKamionZaViseRadnihNalogaDrumskiP; preko: InsertRadniNalogDrumski
  - „UPARI VOZILO I NALOG“ — klik → `button2_Click` — proc: ArhiviranRadniNalogDrumski, DeleteRadniNalogDrumski, DodajStavkeURadniNalogDrumski, DodeliKamionZaRadniNalogDrumski, DodeliKamionZaRadniNalogDrumskiP, DodeliKamionZaViseRadnihNalogaDrumskiP; preko: InsertRadniNalogDrumski
  - „VRATI U NERASPOREĐENE“ — klik → `button1_Click` — proc: ArhiviranRadniNalogDrumski, DeleteRadniNalogDrumski, DodajStavkeURadniNalogDrumski, DodeliKamionZaRadniNalogDrumski, DodeliKamionZaRadniNalogDrumskiP, DodeliKamionZaViseRadnihNalogaDrumskiP; preko: InsertRadniNalogDrumski
  - „NAJAVA VOZILA“ — klik → `btnNajava_Click` — proc: ArhiviranRadniNalogDrumski, DeleteDokumentFaktureDrumski, DeleteDokumentKamionaDrumski, DeleteDokumentVozaca, DeleteFakturaDrumskiStavka, DeleteRadniNalogDrumski; preko: InsertFakture, InsertRadniNalogDrumski
  - „PROTOKOL“ — klik → `button3_Click` — otvara: frmListaProtokola
  - `dataGridView3` — klik na ćeliju → `dataGridView3_CellContentClick` — otvara: frmPregledPorukeVozacu, frmPregledFajlova; proc: ArhiviranRadniNalogDrumski, DeleteRadniNalogDrumski, DodajStavkeURadniNalogDrumski, DodeliKamionZaRadniNalogDrumski, DodeliKamionZaRadniNalogDrumskiP, DodeliKamionZaViseRadnihNalogaDrumskiP; preko: InsertRadniNalogDrumski

#### Platforma
- **Fajl:** `MainLeget/Drumski/Platforma.cs`  
- **Naslov forme:** „Platforma“
- **Otvara forme:** PakovanjeKamiona1 („FORMIRANJE\r\nTRANSPORTNOG NALOGA“)
- **Funkcionalnosti (kontrola → handler):**
  - „NALOG ZA DRUMSKI TRANSPORT“ — klik → `btnNalogDrumski_Click` — otvara: NalogZaDrumski
  - „UNOS PREVOZNIK/KAMIONER“ — klik → `btnPonude_Click` — otvara: frmAutomobiliDrumski
  - „FORMIRANJE\r\nTRANSPORTNOG NALOGA“ — klik → `btnFormiranjeNaloga_Click` — otvara: PakovanjeKamiona1
  - „PROVERA \r\nRASPOLOŽIVOSTI VOZILA“ — klik → `btnProvera_Click` — otvara: frmPodesavanjeRaspolozivosti
  - „NALOG ZA FAKTURISANJE“ — klik → `btnNalogFakturisanje_Click` — otvara: frmNalogZaFakturisanje
  - „STATUSI VOZILA“ — klik → `btnStatusi_Click` — otvara: frmStatus

#### VanGabaritni
- **Fajl:** `MainLeget/Drumski/VanGabaritni.cs`  
- **Naslov forme:** „VanGabaritni“
- **Funkcionalnosti (kontrola → handler):**
  - „NALOG ZA DRUMSKI TRANSPORT“ — klik → `btnNalogDrumski_Click` — otvara: NalogZaDrumski
  - „UNOS PREVOZNIK/KAMIONER“ — klik → `btnPonude_Click` — otvara: frmAutomobiliDrumski
  - „FORMIRANJE\r\nTRANSPORTNOG NALOGA“ — klik → `btnFormiranjeNaloga_Click` — otvara: PakovanjeKamiona1
  - „PROVERA \r\nRASPOLOŽIVOSTI VOZILA“ — klik → `btnProvera_Click` — otvara: frmPodesavanjeRaspolozivosti
  - „STATUSI VOZILA“ — klik → `btnStatusi_Click` — otvara: frmStatus

### Depo (1)

#### DepoMain
- **Fajl:** `MainLeget/Depocnt/DepoMain.cs`  
- **Naslov forme:** „DepoMain“

### Prijem i otprema vozova (35)

#### DogovoreniPosloviIzvoza
- **Fajl:** `Izvoz/DogovoreniPosloviIzvoza.cs`  
- **Naslov forme:** „DOGOVORENI POSLOVI “
- **Tabele:**
  - `DogovorenoIzvoz` — S
  - `PArtnerji` — S
- **Procedure:** `DeleteDogovorenoIzvoz`, `InsertDogovorenoIzvoz`, `UpdateDogovorenoIzvoz`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: DeleteDogovorenoIzvoz, InsertDogovorenoIzvoz, UpdateDogovorenoIzvoz; preko: InsertDogovorenoIzvoz
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteDogovorenoIzvoz, InsertDogovorenoIzvoz, UpdateDogovorenoIzvoz; preko: InsertDogovorenoIzvoz
  - `button20` — klik → `tsDelete_Click` — proc: DeleteDogovorenoIzvoz, InsertDogovorenoIzvoz, UpdateDogovorenoIzvoz; preko: InsertDogovorenoIzvoz
  - `button21` — klik → `tsSave_Click` — proc: DeleteDogovorenoIzvoz, InsertDogovorenoIzvoz, UpdateDogovorenoIzvoz; preko: InsertDogovorenoIzvoz
  - `this` — otvaranje forme → `DogovoreniPosloviIzvoza_Load` — čita: PArtnerji

#### frmAnalizaRadnihNaloga
- **Fajl:** `RadniNalozi/frmAnalizaRadnihNaloga.cs`  
- **Naslov forme:** „TERMINAL CENTRAL“
- **Tabele:**
  - `Izvoz` — S
  - `IzvozKonacna` — S
  - `KontejnerskiTerminali` — S
  - `KontejnerStatus` — S
  - `KontejnerTekuce` — S
  - `OtpremaKontejnera` — S
  - `OtpremaKOntejnera` — S
  - `OtpremaKontejneraVozStavke` — S
  - `PArtnerji` — S
  - `Pozicija` — S
  - `PrijemKontejneraVoz` — S
  - `PrijemKontejneraVozStavke` — S
  - `RNOtpremaCirade` — S
  - `RNOtpremaPlatforme` — S
  - `RNOtpremaPlatforme2` — S
  - `RNOtpremaVoza` — S
  - `RNPregledIpostavkaKontejnera` — S
  - `RNPregledIspraznjenogKontejnera` — S
  - `RNPrijemCirade` — S
  - `RNPrijemPlatforme` — S
  - `RNPrijemVoza` — S
  - `Skladista` — S
  - `TerminalRadniNalozi` — S
  - `TipKontenjera` — S
  - `UvozKonacna` — S
  - `Voz` — S
  - `VrstaManipulacije` — S
- **Funkcionalnosti (kontrola → handler):**
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

#### frmAutoprevozniList2
- **Fajl:** `TrackModal/Dokumeta/frmAutoprevozniList2.cs`  
- **Naslov forme:** „AutoPrevozni list“
- **Tabele:**
  - `Autoprevoznilist` — S
  - `AutoPrevozniList` — S
  - `Komitenti` — S
  - `NajavaPrevoza` — S
  - `PutniNalog` — S
  - `RadniNalogTransport` — S
  - `Vozila` — S
  - `Zaposleni` — S
- **Procedure:** `DeleteAutoprevozniList`, `DeleteAutoprevozniListGeneralni`, `DeleteAutoprevozniListKameni`, `InsertAutoPrevozniList`, `InsertAutoPrevozniListGeneralni`, `InsertAutoPrevozniListKameni`, `UpdateAutoprevoznilist`, `UpdateAutoprevoznilistGeneralni`, `UpdateAutoprevoznilistKameni`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: DeleteAutoprevozniList, DeleteAutoprevozniListGeneralni, DeleteAutoprevozniListKameni, InsertAutoPrevozniList, InsertAutoPrevozniListGeneralni, InsertAutoPrevozniListKameni; preko: InsertAutoprevozniList
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „Štampa Autoprevozni list“ — klik → `toolStripButton1_Click` — izveštaj: rptAutoprevozniList.rdlc
  - „Izbriši“ — klik → `button1_Click` — proc: DeleteAutoprevozniList, DeleteAutoprevozniListGeneralni, DeleteAutoprevozniListKameni, InsertAutoPrevozniList, InsertAutoPrevozniListGeneralni, InsertAutoPrevozniListKameni; preko: InsertAutoprevozniList
  - „Promeni“ — klik → `button2_Click` — proc: DeleteAutoprevozniList, DeleteAutoprevozniListGeneralni, DeleteAutoprevozniListKameni, InsertAutoPrevozniList, InsertAutoPrevozniListGeneralni, InsertAutoPrevozniListKameni; preko: InsertAutoprevozniList
  - „Unesi“ — klik → `button3_Click` — proc: DeleteAutoprevozniList, DeleteAutoprevozniListGeneralni, DeleteAutoprevozniListKameni, InsertAutoPrevozniList, InsertAutoPrevozniListGeneralni, InsertAutoPrevozniListKameni; preko: InsertAutoprevozniList
  - „Izbriši“ — klik → `button4_Click` — proc: DeleteAutoprevozniList, DeleteAutoprevozniListGeneralni, DeleteAutoprevozniListKameni, InsertAutoPrevozniList, InsertAutoPrevozniListGeneralni, InsertAutoPrevozniListKameni; preko: InsertAutoprevozniList
  - „Promeni“ — klik → `button5_Click` — proc: DeleteAutoprevozniList, DeleteAutoprevozniListGeneralni, DeleteAutoprevozniListKameni, InsertAutoPrevozniList, InsertAutoPrevozniListGeneralni, InsertAutoPrevozniListKameni; preko: InsertAutoprevozniList
  - „Unesi“ — klik → `button6_Click` — proc: DeleteAutoprevozniList, DeleteAutoprevozniListGeneralni, DeleteAutoprevozniListKameni, InsertAutoPrevozniList, InsertAutoPrevozniListGeneralni, InsertAutoPrevozniListKameni; preko: InsertAutoprevozniList
  - `this` — otvaranje forme → `frmAutoprevozniList2_Load` — čita: Komitenti, Vozila

#### frmCIRPregled
- **Fajl:** `TrackModal/Dokumeta/frmCIRPregled.cs`  
- **Naslov forme:** „PREGLED POSTOJECIH CIR“
- **Otvara forme:** frmCIR („Otvori“)
- **Tabele:**
  - `CIR` — S
- **Funkcionalnosti (kontrola → handler):**
  - „Pozovi postojeći“ — klik → `toolStripLabel1_Click`
  - „Refresh“ — klik → `toolStripLabel2_Click`
  - „Refresh“ — klik → `toolStripLabel2_Click`
  - „Otvori“ — klik → `toolStripLabel1_Click`
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`

#### frmDodatniList
- **Fajl:** `TrackModal/Izvestaji/frmDodatniList.cs`  
- **Naslov forme:** „Dodatni list“
- **Funkcionalnosti (kontrola → handler):**
  - „Štampaj“ — klik → `btnStampa_Click` — izveštaj: rptDodatniList.rdlc

#### frmDokumentaOtpremaKontejnera
- **Fajl:** `TrackModal/Dokumeta/frmDokumentaOtpremaKontejnera.cs`  
- **Naslov forme:** „Dokumenta otprema kontejnera“
- **Tabele:**
  - `DokumentaOtpremaKontejneraVoz` — S
- **Procedure:** `DeleteDokumentaOtpremaKontejneraVoz`, `InsertDokumentaOtpremaKontejneraVoz`
- **Funkcionalnosti (kontrola → handler):**
  - „Sačuvaj“ — klik → `button4_Click` — proc: DeleteDokumentaOtpremaKontejneraVoz, InsertDokumentaOtpremaKontejneraVoz; preko: InsertDokumentaOtpremaVoz
  - „Otvori“ — klik → `button2_Click`
  - „Pronađi“ — klik → `button1_Click`
  - „Brisanje“ — klik → `button3_Click` — proc: DeleteDokumentaOtpremaKontejneraVoz, InsertDokumentaOtpremaKontejneraVoz; preko: InsertDokumentaOtpremaVoz

#### frmDopunaPlanaPraznimIzvoz
- **Fajl:** `Izvoz/frmDopunaPlanaPraznimIzvoz.cs`  
- **Naslov forme:** „DOPUNA PLANA PRAYNIM KONTEJNERIMA IZVOZ ILI TERMINAL“
- **Tabele:**
  - `Carinarnice` — S
  - `InspekciskiTretman` — S
  - `IzvozKonacna` — S
  - `IzvozKonacnaZaglavlje` — S
  - `IzvozNHM` — S
  - `IzvozVrstaManipulacije` — S
  - `IzvozVrstaRobeHS` — S
  - `KontejnerskiTerminali` — S
  - `KontejnerStatus` — S
  - `KontejnerTekuce` — S
  - `KrajnjaDestinacija` — S
  - `MestaUtovara` — S
  - `NHM` — S
  - `Partnerji` — S
  - `SerijeKola` — S
  - `Skladista` — S
  - `TipKontenjera` — S
  - `uvNacinPakovanja` — S
  - `Voz` — S
  - `VozSerijeKola` — S
  - `VrstaCarinskogPostupka` — S
  - `VrstaManipulacije` — S
  - `VrstaRobeHS` — S
  - `VrstePostupakaUvoz` — S
- **Procedure:** `DeleteIzvozKonacnaNapomenePozicioniranja`, `DeleteIzvozKonacnaSve`, `DeleteIzvozNapomenePozicioniranja`, `DeleteIzvozUslugaDokumenta`, `DeleteIzvozVrstaManTerminalskih`, `InsertIzvozKonacnaNapomenePozicioniranja`, `InsertIzvozNapomenePozicioniranja`, `InsertIzvozUslugaDokument`, `OdrediKontejnerTerminal`, `PrenesiIzPlanIzvozaUPlanIzvoza`, `PrenesiUPlanUtovaraSelektovanoIzvoz`, `PrenesiUPlanUtovaraSelektovanoIzvozBrodar`, `PrenesiUPlanUtovaraSelektovanoIzvozTErminal`, `UpdateIzvozKonacnaVagon`, `VratiKontejnerNerasporedjenoSelektovanoIzvoz`, `spPrenesiIzPlanUtovaraUOtpremaVoz`, `spPrenesiIzUvozaUIzvoz`, `spPrenesiKontejnerUOtpremuKamionom`, `spPrenesiKontejnerUOtpremuKamionomIzvoz`
- **Funkcionalnosti (kontrola → handler):**
  - „Prenesi selektovano“ — klik → `button1_Click` — proc: DeleteIzvozKonacnaNapomenePozicioniranja, DeleteIzvozKonacnaSve, DeleteIzvozNapomenePozicioniranja, DeleteIzvozUslugaDokumenta, DeleteIzvozVrstaManTerminalskih, InsertIzvozKonacnaNapomenePozicioniranja; preko: InsertIzvozKonacna
  - „Refresh“ — klik → `button2_Click`

#### frmFormiranjePlana
- **Fajl:** `Uvoz/frmFormiranjePlana.cs`  
- **Naslov forme:** „Formiranje plana utovara ubacivanje stavki“
- **Otvara forme:** frmUvozKonacna („Otvori plan“)
- **Tabele:**
  - `Brodovi` — S
  - `Carinarnice` — S
  - `DirigacijaKontejneraZa` — S
  - `KontejnerskiTerminali` — S
  - `MestaUtovara` — S
  - `Partnerji` — S
  - `Predefinisaneporuke` — S
  - `Scenario` — S
  - `SerijeKola` — S
  - `TipKontenjera` — S
  - `uvNacinPakovanja` — S
  - `Uvoz` — S
  - `UvozKonacna` — S
  - `UvozKonacnaZaglavlje` — S
  - `UvozNapomenePozicioniranja` — S
  - `Voz` — S
  - `VozSerijeKola` — S
  - `VrstaCarinskogPostupka` — S
  - `VrstaRobeADR` — S
  - `VrstePostupakaUvoz` — S
- **Procedure:** `DeleteUvozKonacna`, `DeleteUvozKonacnaNHM`, `DeleteUvozKonacnaNapomenePozicioniranja`, `DeleteUvozKonacnaVrstaManipulacije`, `DeleteUvozKonacnaVrstaManipulacijeKamionska`, `DeleteUvozKonacnaVrstaRobeHS`, `DeleteUvozNHM`, `DeleteUvozNHMDiana`, `DeleteUvozNapomenePozicioniranja`, `DeleteUvozTerminalskeManipulacije`, `DeleteUvozTerminalskeManipulacijeKonacna`, `DeleteUvozUslugaDokumenta`, `DeleteUvozUvoznici`, `DeleteUvozVrstaManTerminalskih`, `DeleteUvozVrstaManipulacije`, `DeleteUvozVrstaManipulacijeKamionska`, `DeleteUvozVrstaRobeHS`, `DeleteUvozkonacnaNHMDiana`, `InsUbaciDodatnuUsluguIzvoz`, `InsUbaciDodatnuUsluguUvoz`, `InsertUvozKonacbaVrstaRobeHS`, `InsertUvozKonacna`, `InsertUvozKonacnaNHM`, `InsertUvozKonacnaNHMDiana`, `InsertUvozKonacnaNapomenePozicioniranja`, `InsertUvozKonacnaVrstaManipulacije`, `InsertUvozNHM`, `InsertUvozNHMDiana`, `InsertUvozNapomenePozicioniranja`, `InsertUvozUslugaDokument`, `InsertUvozUvoznici`, `InsertUvozVrstaManipulacije`, `InsertUvozVrstaRobeHS`, `PrebaciNHMUvozUvozKonacna`, `PrebaciVrsterobeHSUvozUvozKonacna`, `PrenesiIzPlanUtovaraUPlanUtovara`, `PrenesiUPlanUtovaraSelektovano`, `PromeniSaPrijemniceUK`, `UbaciURadniNalogInterniBeyScenarija`, `UpdCenaUvozKonacnaVrstaManipulacije`, `UpdCenaUvozVrstaManipulacije`, `UpdPlatiocaUvozKonacnaVrstaManipulacije`, `UpdPlatiocaUvozVrstaManipulacije`, `UpdPokretFormaStatus`, `UpdateBrojKolaIzExcela`, `UpdateBrojKolaIzExcelaVoz`, `UpdateUvozKonacna`, `UpdateUvozKonacnaManFaktura`, `UpdateUvozKonacnaManZaFakturisanje`, `UpdateUvozKonacnaObjedinjenja`, `UpdateUvozKonacnaPK`, `UpdateUvozKonacnaUC`, `UpdateUvozNHMTezine`, `VratiIzPlanaUtovaraSelektovano`, `ZavrsiDodatnuUsluguIzvozZadata`, `ZavrsiDodatnuUsluguUvozZadata`, `spIzbrisiKUScenario`, `spPrenesiKontejnerIzPlanaNaPrijemnicu`, `spPrenesiKontejnerIzPlanaNaPrijemnicuIzvoz`, `spPrenesiKontejnerIzPlanaNaPrijemnicuIzvozPoIdNadredjenog`, `spPrenesiPlanUtovaraUPrijemPlatforma`, `spPrenesiPlanUtovaraUPrijemPlatformaIzvoz`, `spPrenesiPlanUtovaraUPrijemVoz`, `spStornijaRNI`
- **Funkcionalnosti (kontrola → handler):**
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - „button4“ — klik → `button4_Click`
  - „Otvori plan“ — klik → `button5_Click` — otvara: frmUvozKonacna
  - „Vrati iz plana u neraspoređene“ — klik → `button3_Click` — proc: DeleteUvozKonacna, DeleteUvozKonacnaNHM, DeleteUvozKonacnaNapomenePozicioniranja, DeleteUvozKonacnaVrstaManipulacije, DeleteUvozKonacnaVrstaManipulacijeKamionska, DeleteUvozKonacnaVrstaRobeHS; preko: InsertUvozKonacna
  - „Ukupan broj mesta voz:“ — klik → `label29_Click`
  - „Refresh“ — klik → `button2_Click`
  - „Prenesi selektovan“ — klik → `button1_Click` — proc: DeleteUvozKonacna, DeleteUvozKonacnaNHM, DeleteUvozKonacnaNapomenePozicioniranja, DeleteUvozKonacnaVrstaManipulacije, DeleteUvozKonacnaVrstaManipulacijeKamionska, DeleteUvozKonacnaVrstaRobeHS; preko: InsertUvozKonacna

#### frmKontejnerTekuceArhiv
- **Fajl:** `Uvoz/frmKontejnerTekuceArhiv.cs`  
- **Naslov forme:** „BAZA ZAVRSENIH KONTEJNERA“
- **Tabele:**
  - `KontejnerskiTerminali` — S
  - `KontejnerStatus` — S
  - `KontejnerTekuceArhiv` — S
  - `PArtnerji` — S
  - `Pozicija` — S
  - `Skladista` — S
- **Funkcionalnosti (kontrola → handler):**
  - „Refresh“ — klik → `button1_Click` — čita: KontejnerStatus, KontejnerTekuceArhiv, KontejnerskiTerminali, PArtnerji, Pozicija, Skladista

#### frmManipulacije
- **Fajl:** `TrackModal/Dokumeta/frmManipulacije.cs`  
- **Naslov forme:** „Usluge“
- **Otvara forme:** frmNajavaKomitemtaDokument („Nalog komitenta“), frmNajavaKomitemtaDokument („Nalog komitenta“), frmNajavaKomitemtaDokument („Nalog komitenta“), frmNajavaKomitemtaDokument („Nalog komitenta“)
- **Tabele:**
  - `Komitenti` — S
  - `NaruceneManipulacije` — S
  - `OtpremaKontejnera` — S
  - `OtpremaKontejneraVozStavke` — S
  - `Partnerji` — S
  - `Pozicija` — S
  - `PrijemKontejneraVoz` — S
  - `PrijemKontejneraVozStavke` — S
  - `Promet` — S
  - `Skladista` — S
  - `Voz` — S
  - `VrstaManipulacije` — S
- **Procedure:** `DeleteNaruceneManipulacije`, `InsertNaruceneManipulacije`, `UpdateNijeUradjenoNaruceneManipulacije`, `UpdateUradjenoNaruceneManipulacije`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „POTVRDI URADJENA USLUGA SA PROMETOM“ — klik → `toolStripButton2_Click`
  - „Nalog komitenta “ — klik → `toolStripButton1_Click` — otvara: frmNajavaKomitemtaDokument
  - „POTVRDI URADJENA USLUGA BEZ PROMETA“ — klik → `toolStripButton3_Click`
  - „>>“ — klik → `btnUnesi_Click` — proc: DeleteNaruceneManipulacije, InsertNaruceneManipulacije, UpdateNijeUradjenoNaruceneManipulacije, UpdateUradjenoNaruceneManipulacije; preko: InsertNaruceneManipulacije
  - „Period od:“ — klik → `label21_Click`
  - „Pretraži“ — klik → `btnPretrazi_Click` — čita: OtpremaKontejneraVozStavke, PrijemKontejneraVozStavke
  - „Pretraži“ — klik → `button1_Click` — čita: OtpremaKontejneraVozStavke, PrijemKontejneraVozStavke
  - „Platilac:“ — klik → `label8_Click`
  - „Izbriši manipulaciju“ — klik → `button2_Click` — proc: DeleteNaruceneManipulacije, InsertNaruceneManipulacije, UpdateNijeUradjenoNaruceneManipulacije, UpdateUradjenoNaruceneManipulacije; preko: InsertNaruceneManipulacije
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

#### frmNajavaKomitemtaDokument
- **Fajl:** `TrackModal/Dokumeta/frmNajavaKomitemtaDokument.cs`  
- **Naslov forme:** „Najava Komitenta Dokument“
- **Tabele:**
  - `DokumentaNajavaKomitenta` — S
- **Procedure:** `DeleteDokumentaOtpremaKontejneraVoz`, `InsertDokumentaNajavaKomitenta`
- **Funkcionalnosti (kontrola → handler):**
  - „Sačuvaj“ — klik → `button4_Click` — proc: DeleteDokumentaOtpremaKontejneraVoz, InsertDokumentaNajavaKomitenta; preko: InsertDokumentaNajava
  - „Otvori“ — klik → `button2_Click`
  - „Pronađi“ — klik → `button1_Click`

#### frmNalogZaPrevoz
- **Fajl:** `TrackModal/Dokumeta/frmNalogZaPrevoz.cs`  
- **Naslov forme:** „Nalog za prevoz“
- **Otvara forme:** frmPutniNalog („Otvori putni nalog“), frmPutniNalog („Otvori putni nalog“), frmKontaktOsobe („?“), frmPutniNalog („Otvori putni nalog“), frmPutniNalog („Otvori putni nalog“)
- **Tabele:**
  - `Cene` — S
  - `Komitenti` — S
  - `MestaUtovara` — S
  - `NajavaPrevoza` — S
  - `NajavaPrevozaTroskovi` — S
  - `OrganizacioneJedinice` — S
  - `Partnerji` — S
  - `VrstaManipulacije` — S
  - `VrstaRobe` — S
  - `VrstaRobeADR` — S
- **Procedure:** `DeleteNajavaPrevozaTroskovi`, `DeleteNalogZaPrevoz`, `InsertNaloziZaPrevoz`, `InsertNaloziZaPrevozTrosak`, `UpdateNaloziZaPrevozTrosak`, `UpdateNaloziZaprevoz`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: DeleteNajavaPrevozaTroskovi, DeleteNalogZaPrevoz, InsertNaloziZaPrevoz, InsertNaloziZaPrevozTrosak, UpdateNaloziZaPrevozTrosak, UpdateNaloziZaprevoz; preko: InsertNalogZaPrevoz
  - „Štampa Nalog za prevoz“ — klik → `toolStripLabel1_Click` — izveštaj: rptNalogZaPrevoz.rdlc, rptNalogZaPrevozTA.rdlc
  - „Otvori putni nalog“ — klik → `toolStripLabel2_Click` — otvara: frmPutniNalog
  - „>>“ — klik → `btnUnesi_Click` — proc: DeleteNajavaPrevozaTroskovi, DeleteNalogZaPrevoz, InsertNaloziZaPrevoz, InsertNaloziZaPrevozTrosak, UpdateNaloziZaPrevozTrosak, UpdateNaloziZaprevoz; preko: InsertNalogZaPrevoz
  - „?“ — klik → `button1_Click` — proc: DeleteNajavaPrevozaTroskovi, DeleteNalogZaPrevoz, InsertNaloziZaPrevoz, InsertNaloziZaPrevozTrosak, UpdateNaloziZaPrevozTrosak, UpdateNaloziZaprevoz; preko: InsertNalogZaPrevoz
  - „Dokument“ — klik → `tabPage1_Click`
  - „?“ — klik → `button3_Click`
  - „Briši stavku“ — klik → `button2_Click` — proc: DeleteNajavaPrevozaTroskovi, DeleteNalogZaPrevoz, InsertNaloziZaPrevoz, InsertNaloziZaPrevozTrosak, UpdateNaloziZaPrevozTrosak, UpdateNaloziZaprevoz; preko: InsertNalogZaPrevoz
  - „>>“ — klik → `button4_Click`
  - `this` — otvaranje forme → `frmNalogZaPrevoz_Load` — čita: MestaUtovara, OrganizacioneJedinice, Partnerji, VrstaManipulacije, VrstaRobeADR

#### frmOtpremaKontejnera
- **Fajl:** `TrackModal/Dokumeta/frmOtpremaKontejnera.cs`  
- **Naslov forme:** „Otprema kontejnera“
- **Otvara forme:** frmDodatniList („Štampa dodatni list“), frmDokumentaOtpremaKontejnera („Dokumenta otpreme“), frmManipulacije („Raspored manipulacija“), frmNalogZaPrevoz („Nalog Za Prevoz“), frmDodatniList („Štampa dodatni list“), frmManipulacije („Raspored usluga“), frmNalogZaPrevoz („Nalog Za Prevoz“), frmPregledNarucenihManipulacija („Usluge pregled“), frmTovarniList („Tovarni list“), frmDodatniList („Štampa dodatni list“), frmManipulacije („Raspored usluga“), frmNalogZaPrevoz („Nalog Za Prevoz“), frmPregledNarucenihManipulacija („Usluge pregled“), frmTovarniList („Tovarni list“), frmDodatniList („Štampa dodatni list“), frmManipulacije („Raspored usluga“), frmNalogZaPrevoz („Nalog Za Prevoz“), frmPregledNarucenihManipulacija („Usluge pregled“), frmTovarniList („Tovarni list“)
- **Tabele:**
  - `Komitenti` — S
  - `NHM` — S
  - `OtpremaKontejnera` — S
  - `OtpremaKontejneraVozStavke` — S
  - `Partnerji` — S
  - `PArtnerji` — S
  - `PostupakStavkeOtpremaVeza` — S
  - `PredefinisanePoruke` — S
  - `Promet` — S
  - `Stanice` — S
  - `StatusRobe` — S
  - `TipKontenjera` — S
  - `Voz` — S
  - `VrstaRobe` — S
  - `VrstePostupakaUvoz` — S
- **Procedure:** `DeleteOtpremaKontejnera`, `DeleteOtpremaKontejneraVozStavke`, `DeletePostupakStavke`, `DeletePostupakStavkeOtprema`, `DeletePrijemKontejneraVoz`, `DeletePrijemKontejneraVozStavke`, `InsertOtpremaKontejnera`, `InsertOtpremaKontejneraVozStavke`, `InsertPostupakStavke`, `InsertPostupakStavkeOtprema`, `InsertPrijemKontejneraVoz`, `InsertPrijemKontejneraVozStavke`, `PrekopirajStavke`, `PromeniBrojVagona`, `UpdateEmailOtpremaNajava`, `UpdateEmailOtpremaOtprema`, `UpdateEmailPrijemNajava`, `UpdateEmailPrijemPrijem`, `UpdateOtpremaKontVozStavkeVagon`, `UpdateOtpremaKontejnera`, `UpdateOtpremaKontejneraVozStavke`, `UpdateOtpremaKontejneraVozStavkeRB`, `UpdatePrijemKontejneraVoz`, `UpdatePrijemKontejneraVozStavke`, `UpdatePrijemKontejneraVozStavkeRB`, `UpdateZatvorenOtpremaCheck`
- **Funkcionalnosti (kontrola → handler):**
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
  - „Obriši“ — klik → `btnObrisiPostupak_Click` — proc: DeletePostupakStavke, DeletePostupakStavkeOtprema, DeletePrijemKontejneraVoz, InsertPostupakStavke, InsertPostupakStavkeOtprema, InsertPrijemKontejneraVoz; preko: InsertPrijemKontejneraVoz
  - „Dodaj“ — klik → `btnDodajPostupak_Click` — proc: DeletePostupakStavke, DeletePostupakStavkeOtprema, DeletePrijemKontejneraVoz, InsertPostupakStavke, InsertPostupakStavkeOtprema, InsertPrijemKontejneraVoz; preko: InsertPrijemKontejneraVoz
  - `this` — otvaranje forme → `frmOtpremaKontejnera_Load` — čita: Partnerji, PredefinisanePoruke, StatusRobe, Voz, VrstePostupakaUvoz

#### frmPopisKontejneraStampa
- **Fajl:** `Dokumenta/frmPopisKontejneraStampa.cs`  
- **Naslov forme:** „Popis kontejnera štampa“
- **Funkcionalnosti (kontrola → handler):**
  - „Štampa“ — klik → `button2_Click` — izveštaj: rptPopisnaListaKontejnera.rdlc

#### frmPopisKonterjnera
- **Fajl:** `Dokumenta/frmPopisKonterjnera.cs`  
- **Naslov forme:** „Popis kontejnera“
- **Otvara forme:** frmPopisKontejneraStampa („Štampa“)
- **Tabele:**
  - `KontejnerStatus` — S
  - `Partnerji` — S
  - `PopisKontejnera` — S
  - `PopisKontejneraStavke` — S
  - `Pozicija` — S
  - `Skladista` — S
  - `TipKontenjera` — S
  - `uvKvalitetKontejnera` — S
- **Procedure:** `DeletePopisKontejnera`, `DeletePopisKontejneraStavke`, `InsertPopisKontejnera`, `InsertPopisKontejneraKontTekuce`, `InsertPopisKontejneraStavke`, `UpdatePopisKontejnera`, `UpdatePopisKontejneraStavke`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click_1`
  - „tsSave“ — klik → `tsSave_Click_1` — proc: DeletePopisKontejnera, DeletePopisKontejneraStavke, InsertPopisKontejnera, InsertPopisKontejneraKontTekuce, InsertPopisKontejneraStavke, UpdatePopisKontejnera; preko: InsertPopisKontejnera
  - „Generisi stavke sa skladista“ — klik → `toolStripButton2_Click_1` — proc: DeletePopisKontejnera, DeletePopisKontejneraStavke, InsertPopisKontejnera, InsertPopisKontejneraKontTekuce, InsertPopisKontejneraStavke, UpdatePopisKontejnera; preko: InsertPopisKontejnera
  - „Osveži“ — klik → `toolStripButton1_Click_1`
  - „Štampa“ — klik → `toolStripButton3_Click_1` — otvara: frmPopisKontejneraStampa
  - „Štampa“ — klik → `toolStripButton3_Click_1` — otvara: frmPopisKontejneraStampa
  - „Refresh“ — klik → `toolStripButton1_Click_1`
  - „Generiši stavke popisa“ — klik → `toolStripButton2_Click_1` — proc: DeletePopisKontejnera, DeletePopisKontejneraStavke, InsertPopisKontejnera, InsertPopisKontejneraKontTekuce, InsertPopisKontejneraStavke, UpdatePopisKontejnera; preko: InsertPopisKontejnera
  - `button21` — klik → `tsSave_Click_1` — proc: DeletePopisKontejnera, DeletePopisKontejneraStavke, InsertPopisKontejnera, InsertPopisKontejneraKontTekuce, InsertPopisKontejneraStavke, UpdatePopisKontejnera; preko: InsertPopisKontejnera
  - „Izbriši“ — klik → `button3_Click` — proc: DeletePopisKontejnera, DeletePopisKontejneraStavke, InsertPopisKontejnera, InsertPopisKontejneraKontTekuce, InsertPopisKontejneraStavke, UpdatePopisKontejnera; preko: InsertPopisKontejnera
  - „Promeni“ — klik → `button2_Click` — proc: DeletePopisKontejnera, DeletePopisKontejneraStavke, InsertPopisKontejnera, InsertPopisKontejneraKontTekuce, InsertPopisKontejneraStavke, UpdatePopisKontejnera; preko: InsertPopisKontejnera
  - „Ubaci“ — klik → `button1_Click` — proc: DeletePopisKontejnera, DeletePopisKontejneraStavke, InsertPopisKontejnera, InsertPopisKontejneraKontTekuce, InsertPopisKontejneraStavke, UpdatePopisKontejnera; preko: InsertPopisKontejnera
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - `this` — otvaranje forme → `frmPopisKonterjnera_Load` — čita: KontejnerStatus, Partnerji, Pozicija, Skladista, TipKontenjera, uvKvalitetKontejnera

#### frmPregledKontejneraDrumskeUsluge
- **Fajl:** `Uvoz/frmPregledKontejneraDrumskeUsluge.cs`  
- **Naslov forme:** „Pregled drumskih usluga“
- **Tabele:**
  - `Automobili` — S
  - `OrganizacioneJedinice` — S
  - `RadniNalogDrumski` — S
  - `StatusVozila` — S
  - `Uvoz` — S
  - `UvozKonacna` — S
  - `UvozKonacnaVrstaManipulacije` — S
  - `UvozVrstaManipulacije` — S
  - `VrstaManipulacije` — S
- **Procedure:** `DeleteUvoz`, `DodajStavkeURadniNalog`, `KopirajKontejner`, `KopirajKontejnerIzKonacne`, `NapraviRadniNalogDrumski`, `UpdCenaUvozKonacnaVrstaManipulacije`, `UpdCenaUvozVrstaManipulacije`, `UpdPDVUvozKonacnaVrstaManipulacije`, `UpdPDVUvozVrstaManipulacije`, `UpdateUvoz`
- **Funkcionalnosti (kontrola → handler):**
  - „Dopuna naloga“ — klik → `button2_Click` — proc: DeleteUvoz, DodajStavkeURadniNalog, KopirajKontejner, KopirajKontejnerIzKonacne, NapraviRadniNalogDrumski, UpdCenaUvozKonacnaVrstaManipulacije; preko: InsertUvoz
  - „Formirnje naloga“ — klik → `button1_Click` — proc: DeleteUvoz, DodajStavkeURadniNalog, KopirajKontejner, KopirajKontejnerIzKonacne, NapraviRadniNalogDrumski, UpdCenaUvozKonacnaVrstaManipulacije; preko: InsertUvoz

#### frmPregledOtpreme
- **Fajl:** `TrackModal/Dokumeta/frmPregledOtpreme.cs`  
- **Naslov forme:** „Pregled otpreme vozom“
- **Otvara forme:** frmOtpremaKontejnera („Otvori voz“)
- **Tabele:**
  - `OtpremaKontejnera` — S
  - `OtpremaKontejneraVozStavke` — S
  - `Voz` — S
- **Funkcionalnosti (kontrola → handler):**
  - „Otvori voz“ — klik → `toolStripButton1_Click_1`
  - „Novi voz“ — klik → `toolStripButton3_Click`
  - „Najavljeni vozovi“ — klik → `toolStripButton5_Click`
  - „Otpremljeni vozovi“ — klik → `toolStripButton6_Click`
  - „?“ — klik → `button3_Click`
  - „?“ — klik → `button2_Click`
  - „Prijem“ — klik → `toolStripButton6_Click`
  - „Najava“ — klik → `toolStripButton5_Click`
  - „Novi voz“ — klik → `toolStripButton3_Click`
  - „Otvori “ — klik → `toolStripButton1_Click_1`

#### frmPregledVozova
- **Fajl:** `TrackModal/Dokumeta/frmPregledVozova.cs`  
- **Naslov forme:** „Pregled vozova“
- **Tabele:**
  - `Partnerji` — S
  - `UvozKonacna` — S
  - `UvozKonacnaZaglavlje` — S
  - `Voz` — S
- **Procedure:** `DeleteVoz`, `DeleteVozSerijeKola`, `InsertVoz`, `InsertVozSerijeKola`, `PrekopirajVoz`, `UpdateVoz`
- **Funkcionalnosti (kontrola → handler):**
  - „Vozovi otvori“ — klik → `toolStripButton1_Click_1` — otvara: frmVoz
  - „Dolazeći“ — klik → `toolStripButton2_Click_1` — čita: Partnerji, UvozKonacna, UvozKonacnaZaglavlje, Voz; izvoz u Excel
  - „Odlazeći“ — klik → `toolStripButton4_Click` — čita: Voz; izvoz u Excel
  - „Kopiraj voz“ — klik → `toolStripButton5_Click` — proc: DeleteVoz, DeleteVozSerijeKola, InsertVoz, InsertVozSerijeKola, PrekopirajVoz, UpdateVoz; preko: InsertVoz
  - „Kopiraj“ — klik → `toolStripButton5_Click` — proc: DeleteVoz, DeleteVozSerijeKola, InsertVoz, InsertVozSerijeKola, PrekopirajVoz, UpdateVoz; preko: InsertVoz
  - „Odlazeći“ — klik → `toolStripButton4_Click` — čita: Voz; izvoz u Excel
  - „Dolazeći“ — klik → `toolStripButton2_Click_1` — čita: Partnerji, UvozKonacna, UvozKonacnaZaglavlje, Voz; izvoz u Excel
  - „Otvori“ — klik → `toolStripButton1_Click_1` — otvara: frmVoz
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`

#### frmPrijemIOtpremaVozova2
- **Fajl:** `MainLeget/Prijemiotpremavozova/frmPrijemIOtpremaVozova2.cs`  
- **Naslov forme:** „frmPrijemIOtpremaVozova2“
- **Otvara forme:** frmCIRPregled („CIR“), frmPopisKonterjnera („Popis cnt“), TerminalMapFRM („Pregled skladišta“)
- **Funkcionalnosti (kontrola → handler):**
  - „Popis cnt“ — klik → `btnPrijemIOtpremaVozova22_Click` — otvara: frmPopisKonterjnera
  - „CIR“ — klik → `btnPrijemIOtpremaVozova23_Click` — otvara: frmCIRPregled
  - „Pregled skladišta“ — klik → `btnPrijemIOtpremaVozova21_Click` — otvara: TerminalMapFRM

#### frmPrijemIOtpremaVozova3
- **Fajl:** `MainLeget/Prijemiotpremavozova/frmPrijemIOtpremaVozova3.cs`  
- **Naslov forme:** „frmPrijemIOtpremaVozova3“
- **Otvara forme:** DogovoreniPosloviIzvoza („Dogovoreni poslovi izvoza“), frmAnalizaRadnihNaloga („Pregled radnih naloga“)
- **Funkcionalnosti (kontrola → handler):**
  - „Dogovoreni poslovi izvoza“ — klik → `btnPrijemIOtpremaVozova32_Click` — otvara: DogovoreniPosloviIzvoza
  - „Pregled radnih naloga “ — klik → `btnPrijemIOtpremaVozova31_Click` — otvara: frmAnalizaRadnihNaloga

#### frmPrijemIOtpremaVozova4
- **Fajl:** `MainLeget/Prijemiotpremavozova/frmPrijemIOtpremaVozova4.cs`  
- **Naslov forme:** „frmPrijemIOtpremaVozova4“
- **Otvara forme:** frmDopunaPlanaPraznimIzvoz („Dopuna plana praznim cnt“), frmPregledOtpreme („Gate out voz“), frmPregledVozova („Pregled vozova“), frmPrijemVozomPregled („Gate in voz“), frmVoz („Vozovi“), RN1PrijemVoza („RN prijem voza“), RN2OtpremaVoza („RN otprema voza“)
- **Funkcionalnosti (kontrola → handler):**
  - „RN prijem voza“ — klik → `btnPrijemIOtpremaVozova42_Click` — otvara: RN1PrijemVoza
  - „Dopuna plana praznim cnt“ — klik → `btnPrijemIOtpremaVozova41_Click` — otvara: frmDopunaPlanaPraznimIzvoz
  - „RN otprema voza“ — klik → `btnPrijemIOtpremaVozova43_Click` — otvara: RN2OtpremaVoza
  - „Vozovi“ — klik → `btnPrijemIOtpremaVozova44_Click` — otvara: frmVoz
  - „Pregled vozova“ — klik → `btnPrijemIOtpremaVozova45_Click` — otvara: frmPregledVozova
  - „Gate in voz“ — klik → `btnPrijemIOtpremaVozova46_Click` — otvara: frmPrijemVozomPregled
  - „Gate out voz“ — klik → `btnPrijemIOtpremaVozova47_Click` — otvara: frmPregledOtpreme

#### frmPrijemVozomPregled
- **Fajl:** `TrackModal/Dokumeta/frmPrijemVozomPregled.cs`  
- **Naslov forme:** „Prijem vozom pregled“
- **Tabele:**
  - `Partnerji` — S
  - `PrijemKontejneraVoz` — S
  - `prijemKontejneraVozstavke` — S
  - `PrijemKontejneraVozStavke` — S
  - `UvozKonacna` — S
  - `UvozKonacnaZaglavlje` — S
  - `Voz` — S
- **Funkcionalnosti (kontrola → handler):**
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

#### frmPutniNalog
- **Fajl:** `TrackModal/Dokumeta/frmPutniNalog.cs`  
- **Naslov forme:** „Putni nalog“
- **Otvara forme:** frmRadniNalogTransport („Radni nalog“), frmRadniNalogTransport („Radni nalog“), frmRadniNalogTransport („Radni nalog“), frmRadniNalogTransport („Radni nalog“)
- **Tabele:**
  - `PutniNalog` — S
  - `PutniNalogGorivo` — S
  - `PutniNalogGume` — S
  - `PutniNalogKvarovi` — S
  - `PutniNalogRuta` — S
  - `PutniNalogServis` — S
  - `PutniNalogTroskovi` — S
  - `PutniNalogZaposleni` — S
  - `Vozila` — S
  - `Zaposleni` — S
- **Procedure:** `DeletePutniNalog`, `DeletePutniNalogGorivo`, `DeletePutniNalogGume`, `DeletePutniNalogKvarovi`, `DeletePutniNalogRuta`, `DeletePutniNalogServis`, `DeletePutniNalogTroskovi`, `DeletePutniNalogZaposleni`, `InsertPutniNalog`, `InsertPutniNalogGorivo`, `InsertPutniNalogGume`, `InsertPutniNalogKvarovi`, `InsertPutniNalogRuta`, `InsertPutniNalogServis`, `InsertPutniNalogTroskovi`, `InsertPutniNalogZaposleni`, `UpdatePutniNalog`, `UpdatePutniNalogGorivo`, `UpdatePutniNalogGume`, `UpdatePutniNalogKvarovi`, `UpdatePutniNalogRuta`, `UpdatePutniNalogServis`, `UpdatePutniNalogTroskovi`, `UpdatePutniNalogZaposleni`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: DeletePutniNalog, InsertPutniNalog, UpdatePutniNalog; preko: InsertPutniNalog
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „Štampa Putni nalog“ — klik → `toolStripLabel1_Click` — izveštaj: rptPutniNalog.rdlc
  - „Radni nalog“ — klik → `toolStripLabel2_Click` — otvara: frmRadniNalogTransport
  - „Servis“ — klik → `tabPage1_Click`
  - „Izbriši“ — klik → `button2_Click` — proc: DeletePutniNalogServis, InsertPutniNalogServis, UpdatePutniNalogServis; preko: InsertPutniNalogServis
  - „Promeni“ — klik → `button1_Click` — proc: DeletePutniNalogServis, InsertPutniNalogServis, UpdatePutniNalogServis; preko: InsertPutniNalogServis
  - „Unesi“ — klik → `btnPretrazi_Click` — proc: DeletePutniNalogServis, InsertPutniNalogServis, UpdatePutniNalogServis; preko: InsertPutniNalogServis
  - „?“ — klik → `button22_Click` — čita: PutniNalog, PutniNalogZaposleni, Zaposleni
  - „Izbriši“ — klik → `button3_Click` — proc: DeletePutniNalogRuta, InsertPutniNalogRuta, UpdatePutniNalogRuta; preko: InsertPutniNalogRuta
  - „Promeni“ — klik → `button4_Click` — proc: DeletePutniNalogRuta, InsertPutniNalogRuta, UpdatePutniNalogRuta; preko: InsertPutniNalogRuta
  - „Unesi“ — klik → `button5_Click` — proc: DeletePutniNalogRuta, InsertPutniNalogRuta, UpdatePutniNalogRuta; preko: InsertPutniNalogRuta
  - „Izbriši“ — klik → `button6_Click` — proc: DeletePutniNalogGume, InsertPutniNalogGume, UpdatePutniNalogGume; preko: InsertPutniNalogGume
  - „Promeni“ — klik → `button7_Click` — proc: DeletePutniNalogGume, InsertPutniNalogGume, UpdatePutniNalogGume; preko: InsertPutniNalogGume
  - „Unesi“ — klik → `button8_Click` — proc: DeletePutniNalogGume, InsertPutniNalogGume, UpdatePutniNalogGume; preko: InsertPutniNalogGume
  - „Izbriši“ — klik → `button9_Click` — proc: DeletePutniNalogGorivo, InsertPutniNalogGorivo, UpdatePutniNalogGorivo; preko: InsertPutniNalogGorivo
  - „Promeni“ — klik → `button10_Click` — proc: DeletePutniNalogGorivo, InsertPutniNalogGorivo, UpdatePutniNalogGorivo; preko: InsertPutniNalogGorivo
  - „Unesi“ — klik → `button11_Click` — proc: DeletePutniNalogGorivo, InsertPutniNalogGorivo, UpdatePutniNalogGorivo; preko: InsertPutniNalogGorivo
  - „Unesi“ — klik → `button14_Click` — proc: DeletePutniNalogTroskovi, InsertPutniNalogTroskovi, UpdatePutniNalogTroskovi; preko: InsertPutniNalogTroskovi
  - „Lista kvarova“ — klik → `tabPage8_Click`
  - „Izbriši“ — klik → `button19_Click` — proc: DeletePutniNalogKvarovi, InsertPutniNalogKvarovi, UpdatePutniNalogKvarovi; preko: InsertPutniNalogKvarovi
  - „Promeni“ — klik → `button20_Click` — proc: DeletePutniNalogKvarovi, InsertPutniNalogKvarovi, UpdatePutniNalogKvarovi; preko: InsertPutniNalogKvarovi
  - „Unesi“ — klik → `button21_Click` — proc: DeletePutniNalogKvarovi, InsertPutniNalogKvarovi, UpdatePutniNalogKvarovi; preko: InsertPutniNalogKvarovi
  - „Pošalji mail“ — klik → `button18_Click` — proc: DeletePutniNalogKvarovi, InsertPutniNalogKvarovi, UpdatePutniNalogKvarovi; preko: InsertPutniNalogKvarovi
  - „Izbriši“ — klik → `button15_Click` — proc: DeletePutniNalogZaposleni, InsertPutniNalogZaposleni, UpdatePutniNalogZaposleni; preko: InsertPutniNalogZaposleni
  - „Promeni“ — klik → `button16_Click` — proc: DeletePutniNalogZaposleni, InsertPutniNalogZaposleni, UpdatePutniNalogZaposleni; preko: InsertPutniNalogZaposleni
  - „Unesi“ — klik → `button17_Click` — proc: DeletePutniNalogZaposleni, InsertPutniNalogZaposleni, UpdatePutniNalogZaposleni; preko: InsertPutniNalogZaposleni
  - „Dokument“ — klik → `tabPage6_Click`
  - `this` — otvaranje forme → `frmPutniNalog_Load` — čita: Vozila, Zaposleni

#### frmRadniNalogTransport
- **Fajl:** `TrackModal/Dokumeta/frmRadniNalogTransport.cs`  
- **Naslov forme:** „Radni nalog transport“
- **Otvara forme:** frmAutoprevozniList2 („Autoprevozni list“), frmAutoprevozniList2 („Autoprevozni list“), frmAutoprevozniList2 („Autoprevozni list“), frmAutoprevozniList2 („Autoprevozni list“)
- **Tabele:**
  - `RadniNalogTransport` — S
  - `Vozila` — S
  - `Zaposleni` — S
- **Procedure:** `DeleteRadniNalogTransportStavke`, `DeleteRadniNalogZaTransport`, `InsertRadniNalogTransport`, `InsertRadniNalogTransportStavke`, `UpdateRadniNalogTransportStavke`, `UpdateRadniNalogZaTransport`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: DeleteRadniNalogZaTransport, InsertRadniNalogTransport, UpdateRadniNalogZaTransport; preko: InsertRadniNalogTransport
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „Štampa radni nalog“ — klik → `toolStripLabel1_Click` — izveštaj: rptRadniNalogTransport.rdlc
  - „Autoprevozni list“ — klik → `toolStripLabel2_Click` — otvara: frmAutoprevozniList2
  - „Izbriši“ — klik → `button12_Click` — proc: DeleteRadniNalogTransportStavke, InsertRadniNalogTransportStavke, UpdateRadniNalogTransportStavke; preko: InsertRadniNalogTransportStavke
  - „Promeni“ — klik → `button13_Click` — proc: DeleteRadniNalogTransportStavke, InsertRadniNalogTransportStavke, UpdateRadniNalogTransportStavke; preko: InsertRadniNalogTransportStavke
  - „Unesi“ — klik → `button14_Click` — proc: DeleteRadniNalogTransportStavke, InsertRadniNalogTransportStavke, UpdateRadniNalogTransportStavke; preko: InsertRadniNalogTransportStavke
  - „Korisnik:“ — klik → `label11_Click`
  - „Stavka:“ — klik → `label8_Click`
  - „Podaci“ — klik → `tabPage1_Click`
  - `this` — otvaranje forme → `frmRadniNalogTransport_Load` — čita: Vozila, Zaposleni

#### frmUnosManipulacija
- **Fajl:** `Uvoz/frmUnosManipulacija.cs`  
- **Naslov forme:** „UNOS USLUGA - UVOZ“
- **Otvara forme:** frmUvozKopirajUslugeKontejnera („Kopiraj usluge“), frmVoziloUsluga („Kamion za uslugu“)
- **Tabele:**
  - `Brodovi` — S
  - `Carinarnice` — S
  - `Cene` — S
  - `DirigacijaKontejneraZa` — S
  - `GrupaVrsteManipulacije` — S
  - `KontejnerskiTerminali` — S
  - `kontejnerStatus` — S
  - `KontejnerStatus` — S
  - `MestaUtovara` — S
  - `NHM` — S
  - `OrganizacioneJedinice` — S
  - `PArtnerji` — S
  - `Partnerji` — S
  - `Predefinisaneporuke` — S
  - `RadniNalogInterni` — S
  - `Scenario` — S
  - `TipKontenjera` — S
  - `uvNacinPakovanja` — S
  - `Uvoz` — S
  - `UvozKonacna` — S
  - `UvozKonacnaVrstaManipulacije` — S
  - `UvozVrstaManipulacije` — SU
  - `VrstaCarinskogPostupka` — S
  - `VrstaManipulacije` — S
  - `vrstamanipulacije` — S
  - `VrstaRobeADR` — S
  - `VrstaRobeHS` — S
  - `VrstePostupakaUvoz` — S
- **Procedure:** `DeleteScenario`, `DeleteUvoz`, `DeleteUvozKonacna`, `DeleteUvozKonacnaNHM`, `DeleteUvozKonacnaNapomenePozicioniranja`, `DeleteUvozKonacnaVrstaManipulacije`, `DeleteUvozKonacnaVrstaManipulacijeKamionska`, `DeleteUvozKonacnaVrstaRobeHS`, `DeleteUvozNHM`, `DeleteUvozNHMDiana`, `DeleteUvozNapomenePozicioniranja`, `DeleteUvozTerminalskeManipulacije`, `DeleteUvozTerminalskeManipulacijeKonacna`, `DeleteUvozUslugaDokumenta`, `DeleteUvozUvoznici`, `DeleteUvozVrstaManTerminalskih`, `DeleteUvozVrstaManipulacije`, `DeleteUvozVrstaManipulacijeKamionska`, `DeleteUvozVrstaRobeHS`, `DeleteUvozkonacnaNHMDiana`, `DodajStavkeURadniNalog`, `InsUbaciDodatnuUsluguIzvoz`, `InsUbaciDodatnuUsluguUvoz`, `InsertScenario`, `InsertUvozKonacbaVrstaRobeHS`, `InsertUvozKonacna`, `InsertUvozKonacnaNHM`, `InsertUvozKonacnaNHMDiana`, `InsertUvozKonacnaNapomenePozicioniranja`, `InsertUvozKonacnaVrstaManipulacije`, `InsertUvozNHM`, `InsertUvozNHMDiana`, `InsertUvozNapomenePozicioniranja`, `InsertUvozUslugaDokument`, `InsertUvozUvoznici`, `InsertUvozVrstaManipulacije`, `InsertUvozVrstaRobeHS`, `KopirajKontejner`, `KopirajKontejnerIzKonacne`, `NapraviRadniNalogDrumski`, `PrebaciNHMUvozUvozKonacna`, `PrebaciVrsterobeHSUvozUvozKonacna`, `PrenesiIzPlanUtovaraUPlanUtovara`, `PrenesiUPlanUtovaraSelektovano`, `PromeniSaPrijemniceUK`, `UbaciURadniNalogInterniBeyScenarija`, `UpdCenaUvozKonacnaVrstaManipulacije`, `UpdCenaUvozVrstaManipulacije`, `UpdPDVUvozKonacnaVrstaManipulacije`, `UpdPDVUvozVrstaManipulacije`, `UpdPlatiocaUvozKonacnaVrstaManipulacije`, `UpdPlatiocaUvozVrstaManipulacije`, `UpdPokretFormaStatus`, `UpdateBrojKolaIzExcela`, `UpdateBrojKolaIzExcelaVoz`, `UpdateScenario`, `UpdateScenarioKontejnera`, `UpdateUvoz`, `UpdateUvozKonacna`, `UpdateUvozKonacnaManFaktura`, `UpdateUvozKonacnaManZaFakturisanje`, `UpdateUvozKonacnaObjedinjenja`, `UpdateUvozKonacnaPK`, `UpdateUvozKonacnaUC`, `UpdateUvozNHMTezine`, `VratiIzPlanaUtovaraSelektovano`, `ZavrsiDodatnuUsluguIzvozZadata`, `ZavrsiDodatnuUsluguUvozZadata`, `spIzbrisiKUScenario`, `spPrenesiKontejnerIzPlanaNaPrijemnicu`, `spPrenesiKontejnerIzPlanaNaPrijemnicuIzvoz`, `spPrenesiKontejnerIzPlanaNaPrijemnicuIzvozPoIdNadredjenog`, `spPrenesiPlanUtovaraUPrijemPlatforma`, `spPrenesiPlanUtovaraUPrijemPlatformaIzvoz`, `spPrenesiPlanUtovaraUPrijemVoz`, `spStornijaRNI`
- **Funkcionalnosti (kontrola → handler):**
  - „OPŠTI“ — klik → `button11_Click`
  - „N1“ — klik → `button13_Click`
  - „N1>>>>“ — klik → `button12_Click` — čita: Cene
  - `dataGridView7` — klik na ćeliju → `dataGridView7_CellClick` — proc: DeleteUvoz, DodajStavkeURadniNalog, KopirajKontejner, KopirajKontejnerIzKonacne, NapraviRadniNalogDrumski, UpdCenaUvozKonacnaVrstaManipulacije; preko: InsertUvoz
  - `dataGridView7` — CellLeave → `dataGridView7_CellLeave` — proc: DeleteUvoz, DodajStavkeURadniNalog, KopirajKontejner, KopirajKontejnerIzKonacne, NapraviRadniNalogDrumski, UpdCenaUvozKonacnaVrstaManipulacije; preko: InsertUvoz
  - „N2“ — klik → `button1_Click`
  - „N3“ — klik → `button2_Click`
  - „N1-BU“ — klik → `button3_Click`
  - „N2-BU“ — klik → `button4_Click`
  - „N3-BU“ — klik → `button5_Click`
  - „N2>>>>“ — klik → `button6_Click` — čita: Cene
  - „N3>>>>“ — klik → `button7_Click` — čita: Cene
  - „Obrisi selektovane“ — klik → `button8_Click` — proc: DeleteUvozKonacna, DeleteUvozKonacnaNHM, DeleteUvozKonacnaNapomenePozicioniranja, DeleteUvozKonacnaVrstaManipulacije, DeleteUvozKonacnaVrstaManipulacijeKamionska, DeleteUvozKonacnaVrstaRobeHS; preko: InsertUvozKonacna
  - „Promeni  platioca“ — klik → `button9_Click` — proc: DeleteUvozKonacna, DeleteUvozKonacnaNHM, DeleteUvozKonacnaNapomenePozicioniranja, DeleteUvozKonacnaVrstaManipulacije, DeleteUvozKonacnaVrstaManipulacijeKamionska, DeleteUvozKonacnaVrstaRobeHS; preko: InsertUvozKonacna
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
  - „Promeni  platioca“ — klik → `button19_Click` — proc: DeleteUvozKonacna, DeleteUvozKonacnaNHM, DeleteUvozKonacnaNapomenePozicioniranja, DeleteUvozKonacnaVrstaManipulacije, DeleteUvozKonacnaVrstaManipulacijeKamionska, DeleteUvozKonacnaVrstaRobeHS; preko: InsertUvozKonacna
  - „Promeni  platioca“ — klik → `button21_Click` — proc: DeleteUvozKonacna, DeleteUvozKonacnaNHM, DeleteUvozKonacnaNapomenePozicioniranja, DeleteUvozKonacnaVrstaManipulacije, DeleteUvozKonacnaVrstaManipulacijeKamionska, DeleteUvozKonacnaVrstaRobeHS; preko: InsertUvozKonacna
  - „Promeni  platioca“ — klik → `button22_Click` — proc: DeleteUvozKonacna, DeleteUvozKonacnaNHM, DeleteUvozKonacnaNapomenePozicioniranja, DeleteUvozKonacnaVrstaManipulacije, DeleteUvozKonacnaVrstaManipulacijeKamionska, DeleteUvozKonacnaVrstaRobeHS; preko: InsertUvozKonacna
  - „S N I M I“ — klik → `button26_Click`
  - „Primeni“ — klik → `button28_Click` — proc: DeleteUvozKonacna, DeleteUvozKonacnaNHM, DeleteUvozKonacnaNapomenePozicioniranja, DeleteUvozKonacnaVrstaManipulacije, DeleteUvozKonacnaVrstaManipulacijeKamionska, DeleteUvozKonacnaVrstaRobeHS; preko: InsertUvozKonacna
  - „PROMENI SCENARIO“ — klik → `button29_Click` — čita: RadniNalogInterni, Scenario, UvozKonacna, VrstaManipulacije

#### frmUvozKonacna
- **Fajl:** `Uvoz/UvozKonacna.cs`  
- **Naslov forme:** „PLAN PRIJEMA VOZA UVOZ“
- **Otvara forme:** frmPregledKontejneraDrumskeUsluge („DRUMSKE USLUGE“), frmUnosManipulacija (prečica na tastaturi (`frmUvozKonacna_KeyDown`)), frmUvozKonacnaTable (prečica na tastaturi (`frmUvozKonacna_KeyDown`)), frmUvozUvoznici („Uvezi spisak“), UvozNHM („NHM/HS KODOVI“)
- **Tabele:**
  - `Brodovi` — S
  - `Carinarnice` — S
  - `Cene` — S
  - `DirigacijaKontejneraZa` — S
  - `InspekciskiTretman` — S
  - `KontejnerskiTerminali` — S
  - `KontejnerStatus` — S
  - `kontejnerStatus` — S
  - `MestaUtovara` — S
  - `NapomenaZaPozicioniranje` — S
  - `NHM` — S
  - `OrganizacioneJedinice` — S
  - `Partnerji` — S
  - `PArtnerji` — S
  - `partnerjiKontOseba` — S
  - `partnerjiKontOsebaMU` — S
  - `PredefinisanePoruke` — S
  - `Predefinisaneporuke` — S
  - `RadniNalogInterni` — S
  - `Scenario` — S
  - `TipKontenjera` — S
  - `uvNacinPakovanja` — S
  - `uvoz` — U
  - `UvozKonacna` — S
  - `UvozKonacnaNapomenePozicioniranja` — S
  - `UvozKonacnaNHM` — S
  - `UvozKonacnaVrstaManipulacije` — S
  - `UvozKonacnaVrstaRobeHS` — S
  - `UvozKonacnaZaglavlje` — S
  - `UvozNapomenePozicioniranja` — S
  - `UvozNHM` — S
  - `UvozUvoznici` — S
  - `UvozVrstaRobeHS` — S
  - `Voz` — S
  - `VrstaCarinskogPostupka` — S
  - `VrstaManipulacije` — S
  - `vrstamanipulacije` — S
  - `VrstaRobe` — S
  - `VrstaRobeADR` — S
  - `VrstaRobeHS` — S
  - `VrstePostupakaUvoz` — S
- **Procedure:** `DelRadniNalogInterniSaDokumentima`, `DeleteRadniNalogInterni`, `DeleteScenario`, `DeleteUvozKonacna`, `DeleteUvozKonacnaNHM`, `DeleteUvozKonacnaNapomenePozicioniranja`, `DeleteUvozKonacnaVrstaManipulacije`, `DeleteUvozKonacnaVrstaManipulacijeKamionska`, `DeleteUvozKonacnaVrstaRobeHS`, `DeleteUvozKonacnaZaglavlje`, `DeleteUvozNHM`, `DeleteUvozNHMDiana`, `DeleteUvozNapomenePozicioniranja`, `DeleteUvozTerminalskeManipulacije`, `DeleteUvozTerminalskeManipulacijeKonacna`, `DeleteUvozUslugaDokumenta`, `DeleteUvozUvoznici`, `DeleteUvozVrstaManTerminalskih`, `DeleteUvozVrstaManipulacije`, `DeleteUvozVrstaManipulacijeKamionska`, `DeleteUvozVrstaRobeHS`, `DeleteUvozkonacnaNHMDiana`, `DozvoliIzlaz`, `InsUbaciDodatnuUsluguIzvoz`, `InsUbaciDodatnuUsluguUvoz`, `InsertRadniNalogInterni`, `InsertRadniNalogInterni2`, `InsertRadniNalogInterniIzvoz`, `InsertRadniNalogInterniIzvoz2`, `InsertRadniNalogInterniIzvozDodatne`, `InsertRadniNalogInterniPotvrda`, `InsertRadniNalogInterniSkladistePotvrda`, `InsertRadniNalogInterniStorno`, `InsertScenario`, `InsertUvozKonacbaVrstaRobeHS`, `InsertUvozKonacna`, `InsertUvozKonacnaNHM`, `InsertUvozKonacnaNHMDiana`, `InsertUvozKonacnaNapomenePozicioniranja`, `InsertUvozKonacnaVrstaManipulacije`, `InsertUvozKonacnaZaglavlje`, `InsertUvozNHM`, `InsertUvozNHMDiana`, `InsertUvozNapomenePozicioniranja`, `InsertUvozUslugaDokument`, `InsertUvozUvoznici`, `InsertUvozVrstaManipulacije`, `InsertUvozVrstaRobeHS`, `ObavljenPregled`, `PrebaciNHMUvozUvozKonacna`, `PrebaciVrsterobeHSUvozUvozKonacna`, `PrenesiIzPlanUtovaraUPlanUtovara`, `PrenesiUPlanUtovaraSelektovano`, `PromeniSaPrijemniceUK`, `PromeniStatusKalmar`, `PromeniStatusKapijaOdlazak`, `PromeniStatusPretovareno`, `PromeniStatusPrijemPotvrdioKapiju`, `PromeniStatusPrijemPotvrdioVP`, `UbaciURadniNalogInterniBeyScenarija`, `UpdCenaUvozKonacnaVrstaManipulacije`, `UpdCenaUvozVrstaManipulacije`, `UpdPlatiocaUvozKonacnaVrstaManipulacije`, `UpdPlatiocaUvozVrstaManipulacije`, `UpdPokretFormaStatus`, `UpdateBrojKolaIzExcela`, `UpdateBrojKolaIzExcelaVoz`, `UpdateRNIVozFormiran`, `UpdateRadniNalogInterni`, `UpdateRadniNalogInterniFormiran`, `UpdateRadniNalogInterniGenerisan`, `UpdateRadniNalogInterniZavrsen`, `UpdateScenario`, `UpdateScenarioKontejnera`, `UpdateUvozKonacna`, `UpdateUvozKonacnaManFaktura`, `UpdateUvozKonacnaManZaFakturisanje`, `UpdateUvozKonacnaObjedinjenja`, `UpdateUvozKonacnaPK`, `UpdateUvozKonacnaUC`, `UpdateUvozKonacnaZaglavlje`, `UpdateUvozNHMTezine`, `VratiIzPlanaUtovaraSelektovano`, `ZavrsiDodatnuUsluguIzvozZadata`, `ZavrsiDodatnuUsluguUvozZadata`, `spIzbrisiKUScenario`, `spPrenesiKontejnerIzPlanaNaPrijemnicu`, `spPrenesiKontejnerIzPlanaNaPrijemnicuIzvoz`, `spPrenesiKontejnerIzPlanaNaPrijemnicuIzvozPoIdNadredjenog`, `spPrenesiPlanUtovaraUPrijemPlatforma`, `spPrenesiPlanUtovaraUPrijemPlatformaIzvoz`, `spPrenesiPlanUtovaraUPrijemVoz`, `spStornijaRNI`, `updRadniNalogInterniPrijemnicaPotvrdjen`, `updRadniNalogInterniRukovalacPotvrdjen`, `updRadniNalogInterniSkladistarPotvrdio`, `updRadniNalogInterniSkladistePotvrdjen`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: DeleteUvozKonacna, DeleteUvozKonacnaNHM, DeleteUvozKonacnaNapomenePozicioniranja, DeleteUvozKonacnaVrstaManipulacije, DeleteUvozKonacnaVrstaManipulacijeKamionska, DeleteUvozKonacnaVrstaRobeHS; preko: InsertUvozKonacna
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteUvozKonacna, DeleteUvozKonacnaNHM, DeleteUvozKonacnaNapomenePozicioniranja, DeleteUvozKonacnaVrstaManipulacije, DeleteUvozKonacnaVrstaManipulacijeKamionska, DeleteUvozKonacnaVrstaRobeHS; preko: InsertUvozKonacna
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
  - „Izdavanje naloga“ — klik → `toolStripButton10_Click` — proc: DelRadniNalogInterniSaDokumentima, DeleteRadniNalogInterni, DozvoliIzlaz, InsertRadniNalogInterni, InsertRadniNalogInterni2, InsertRadniNalogInterniIzvoz; preko: InsertRadniNalogInterni
  - „Ubacivanje nerasporedjenih u plan“ — klik → `toolStripButton1_Click_1` — otvara: frmFormiranjePlana
  - „Ažuriraj šifarnike“ — klik → `toolStripButton5_Click_1`
  - „Odredišna carinarnica“ — klik → `label26_Click`
  - „Broj plombe 1“ — klik → `label31_Click`
  - „Delete“ — klik → `button3_Click` — proc: DeleteUvozKonacnaZaglavlje, InsertUvozKonacnaZaglavlje, UpdateUvozKonacnaZaglavlje; preko: InsertUvozKonacnaZaglavlje
  - „Promena“ — klik → `button2_Click` — proc: DeleteUvozKonacnaZaglavlje, InsertUvozKonacnaZaglavlje, UpdateUvozKonacnaZaglavlje; preko: InsertUvozKonacnaZaglavlje
  - „Unesi“ — klik → `button1_Click` — proc: DeleteUvozKonacnaZaglavlje, InsertUvozKonacnaZaglavlje, UpdateUvozKonacnaZaglavlje; preko: InsertUvozKonacnaZaglavlje
  - „Delete“ — klik → `button4_Click` — proc: DeleteUvozKonacna, DeleteUvozKonacnaNHM, DeleteUvozKonacnaNapomenePozicioniranja, DeleteUvozKonacnaVrstaManipulacije, DeleteUvozKonacnaVrstaManipulacijeKamionska, DeleteUvozKonacnaVrstaRobeHS; preko: InsertUvozKonacna
  - „Unesi“ — klik → `button5_Click` — proc: DeleteUvozKonacna, DeleteUvozKonacnaNHM, DeleteUvozKonacnaNapomenePozicioniranja, DeleteUvozKonacnaVrstaManipulacije, DeleteUvozKonacnaVrstaManipulacijeKamionska, DeleteUvozKonacnaVrstaRobeHS; preko: InsertUvozKonacna
  - „Delete“ — klik → `button6_Click` — proc: DeleteUvozKonacna, DeleteUvozKonacnaNHM, DeleteUvozKonacnaNapomenePozicioniranja, DeleteUvozKonacnaVrstaManipulacije, DeleteUvozKonacnaVrstaManipulacijeKamionska, DeleteUvozKonacnaVrstaRobeHS; preko: InsertUvozKonacna
  - „Unesi“ — klik → `button7_Click` — proc: DeleteUvozKonacna, DeleteUvozKonacnaNHM, DeleteUvozKonacnaNapomenePozicioniranja, DeleteUvozKonacnaVrstaManipulacije, DeleteUvozKonacnaVrstaManipulacijeKamionska, DeleteUvozKonacnaVrstaRobeHS; preko: InsertUvozKonacna
  - „Delete“ — klik → `button8_Click` — proc: DeleteUvozKonacna, DeleteUvozKonacnaNHM, DeleteUvozKonacnaNapomenePozicioniranja, DeleteUvozKonacnaVrstaManipulacije, DeleteUvozKonacnaVrstaManipulacijeKamionska, DeleteUvozKonacnaVrstaRobeHS; preko: InsertUvozKonacna
  - „Unesi“ — klik → `button9_Click` — proc: DeleteUvozKonacna, DeleteUvozKonacnaNHM, DeleteUvozKonacnaNapomenePozicioniranja, DeleteUvozKonacnaVrstaManipulacije, DeleteUvozKonacnaVrstaManipulacijeKamionska, DeleteUvozKonacnaVrstaRobeHS; preko: InsertUvozKonacna
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
  - „Generisi istovar voz“ — klik → `button39_Click` — proc: DeleteUvozKonacna, DeleteUvozKonacnaNHM, DeleteUvozKonacnaNapomenePozicioniranja, DeleteUvozKonacnaVrstaManipulacije, DeleteUvozKonacnaVrstaManipulacijeKamionska, DeleteUvozKonacnaVrstaRobeHS; preko: InsertUvozKonacna
  - „Dodaj nerasporedjene u plan“ — klik → `button29_Click` — otvara: frmFormiranjePlana
  - „Izdavanje naloga“ — klik → `button27_Click` — proc: DelRadniNalogInterniSaDokumentima, DeleteRadniNalogInterni, DozvoliIzlaz, InsertRadniNalogInterni, InsertRadniNalogInterni2, InsertRadniNalogInterniIzvoz; preko: InsertRadniNalogInterni
  - „Ažuriraj šifarnike“ — klik → `button26_Click`
  - „Tabela“ — klik → `button25_Click` — otvara: frmUvozKonacnaTable
  - „Usluge“ — klik → `button24_Click` — otvara: frmUnosManipulacija
  - „Dokumentacija“ — klik → `button23_Click` — otvara: UvozDokumenta
  - `button21` — klik → `tsDelete_Click` — proc: DeleteUvozKonacna, DeleteUvozKonacnaNHM, DeleteUvozKonacnaNapomenePozicioniranja, DeleteUvozKonacnaVrstaManipulacije, DeleteUvozKonacnaVrstaManipulacijeKamionska, DeleteUvozKonacnaVrstaRobeHS; preko: InsertUvozKonacna
  - `button22` — klik → `tsSave_Click` — proc: DeleteUvozKonacna, DeleteUvozKonacnaNHM, DeleteUvozKonacnaNapomenePozicioniranja, DeleteUvozKonacnaVrstaManipulacije, DeleteUvozKonacnaVrstaManipulacijeKamionska, DeleteUvozKonacnaVrstaRobeHS; preko: InsertUvozKonacna
  - „Promeni“ — klik → `button42_Click` — čita: RadniNalogInterni, Scenario, UvozKonacna, VrstaManipulacije; proc: DeleteScenario, InsertScenario, UpdateScenario, UpdateScenarioKontejnera; preko: InsertScenario
  - „Moguci“ — klik → `button41_Click` — čita: Scenario
  - „Drumska usluga“ — čekiranje → `chkDrumskaUsluga_CheckedChanged` — proc: DeleteUvozKonacna, DeleteUvozKonacnaNHM, DeleteUvozKonacnaNapomenePozicioniranja, DeleteUvozKonacnaVrstaManipulacije, DeleteUvozKonacnaVrstaManipulacijeKamionska, DeleteUvozKonacnaVrstaRobeHS; preko: InsertUvozKonacna
  - „Opšti“ — čekiranje → `chkOpsti_CheckedChanged`
  - „Uvezi spisak“ — klik → `button28_Click` — otvara: frmUvozUvoznici
  - „NHM/HS KODOVI“ — klik → `button33_Click` — otvara: UvozNHM
  - „ODOBREN POČETAK – BEZ DODATNIH INSTRUKCIJA “ — čekiranje → `chkPotvrdioKlijent2BDI_CheckedChanged`
  - „ČEKA SE “ — čekiranje → `chkCekaSeKlijent_CheckedChanged`
  - „ODOBREN POČETAK – UZ DODATNE INSTRUKCIJE“ — čekiranje → `chkPotvrdioKlijent_CheckedChanged`
  - „Potvrđen carinski postupak“ — čekiranje → `chkUradilaCarina_CheckedChanged`
  - „Čeka se“ — čekiranje → `chkCekaSeCarina_CheckedChanged`
  - `button31` — klik → `button31_Click` — čita: Partnerji, partnerjiKontOseba

#### frmUvozKonacnaTable
- **Fajl:** `Uvoz/frmUvozKonacnaTable.cs`  
- **Naslov forme:** „Uvoz konacna table“
- **Tabele:**
  - `Brodovi` — S
  - `Carinarnice` — S
  - `DirigacijaKontejneraZa` — S
  - `InspekciskiTretman` — S
  - `KontejnerskiTerminali` — S
  - `MestaUtovara` — S
  - `Partnerji` — S
  - `partnerjiKontOsebaMU` — S
  - `Predefinisaneporuke` — S
  - `Scenario` — S
  - `TipKontenjera` — S
  - `uvNacinPakovanja` — S
  - `uvoz` — U
  - `uvozkonacna` — U
  - `UvozKonacna` — S
  - `UvozNapomenePozicioniranja` — S
  - `uvuvozkonacnaoz` — U
  - `VrstaCarinskogPostupka` — S
  - `VrstaRobeADR` — S
  - `VrstePostupakaUvoz` — S
- **Funkcionalnosti (kontrola → handler):**
  - „Primeni izmenu“ — klik → `button2_Click`
  - „Izberi“ — klik → `button1_Click`
  - „Refresh“ — klik → `button4_Click`
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`

#### frmUvozKopirajUslugeKontejnera
- **Fajl:** `Uvoz/frmUvozKopirajUslugeKontejnera.cs`  
- **Naslov forme:** „Kopiraj usluge kontejnera “
- **Tabele:**
  - `Brodovi` — S
  - `Carinarnice` — S
  - `DirigacijaKontejneraZa` — S
  - `KontejnerskiTerminali` — S
  - `MestaUtovara` — S
  - `Partnerji` — S
  - `Predefinisaneporuke` — S
  - `TipKontenjera` — S
  - `uvNacinPakovanja` — S
  - `Uvoz` — S
  - `UvozKonacna` — S
  - `VrstaCarinskogPostupka` — S
  - `VrstaRobeADR` — S
  - `VrstePostupakaUvoz` — S
- **Procedure:** `DeleteUvoz`, `DodajStavkeURadniNalog`, `KopirajKontejner`, `KopirajKontejnerIzKonacne`, `NapraviRadniNalogDrumski`, `UpdCenaUvozKonacnaVrstaManipulacije`, `UpdCenaUvozVrstaManipulacije`, `UpdPDVUvozKonacnaVrstaManipulacije`, `UpdPDVUvozVrstaManipulacije`, `UpdateUvoz`
- **Funkcionalnosti (kontrola → handler):**
  - „Kopiraj“ — klik → `button23_Click` — proc: DeleteUvoz, DodajStavkeURadniNalog, KopirajKontejner, KopirajKontejnerIzKonacne, NapraviRadniNalogDrumski, UpdCenaUvozKonacnaVrstaManipulacije; preko: InsertUvoz

#### frmUvozUvoznici
- **Fajl:** `Uvoz/frmUvozUvoznici.cs`  
- **Naslov forme:** „Lista uvoznika za kontejner“
- **Tabele:**
  - `UvozUvoznici` — S
- **Procedure:** `DeleteUvozKonacna`, `DeleteUvozKonacnaNHM`, `DeleteUvozKonacnaNapomenePozicioniranja`, `DeleteUvozKonacnaVrstaManipulacije`, `DeleteUvozKonacnaVrstaManipulacijeKamionska`, `DeleteUvozKonacnaVrstaRobeHS`, `DeleteUvozNHM`, `DeleteUvozNHMDiana`, `DeleteUvozNapomenePozicioniranja`, `DeleteUvozTerminalskeManipulacije`, `DeleteUvozTerminalskeManipulacijeKonacna`, `DeleteUvozUslugaDokumenta`, `DeleteUvozUvoznici`, `DeleteUvozVrstaManTerminalskih`, `DeleteUvozVrstaManipulacije`, `DeleteUvozVrstaManipulacijeKamionska`, `DeleteUvozVrstaRobeHS`, `DeleteUvozkonacnaNHMDiana`, `InsUbaciDodatnuUsluguIzvoz`, `InsUbaciDodatnuUsluguUvoz`, `InsertUvozKonacbaVrstaRobeHS`, `InsertUvozKonacna`, `InsertUvozKonacnaNHM`, `InsertUvozKonacnaNHMDiana`, `InsertUvozKonacnaNapomenePozicioniranja`, `InsertUvozKonacnaVrstaManipulacije`, `InsertUvozNHM`, `InsertUvozNHMDiana`, `InsertUvozNapomenePozicioniranja`, `InsertUvozUslugaDokument`, `InsertUvozUvoznici`, `InsertUvozVrstaManipulacije`, `InsertUvozVrstaRobeHS`, `PrebaciNHMUvozUvozKonacna`, `PrebaciVrsterobeHSUvozUvozKonacna`, `PrenesiIzPlanUtovaraUPlanUtovara`, `PrenesiUPlanUtovaraSelektovano`, `PromeniSaPrijemniceUK`, `UbaciURadniNalogInterniBeyScenarija`, `UpdCenaUvozKonacnaVrstaManipulacije`, `UpdCenaUvozVrstaManipulacije`, `UpdPlatiocaUvozKonacnaVrstaManipulacije`, `UpdPlatiocaUvozVrstaManipulacije`, `UpdPokretFormaStatus`, `UpdateBrojKolaIzExcela`, `UpdateBrojKolaIzExcelaVoz`, `UpdateUvozKonacna`, `UpdateUvozKonacnaManFaktura`, `UpdateUvozKonacnaManZaFakturisanje`, `UpdateUvozKonacnaObjedinjenja`, `UpdateUvozKonacnaPK`, `UpdateUvozKonacnaUC`, `UpdateUvozNHMTezine`, `VratiIzPlanaUtovaraSelektovano`, `ZavrsiDodatnuUsluguIzvozZadata`, `ZavrsiDodatnuUsluguUvozZadata`, `spIzbrisiKUScenario`, `spPrenesiKontejnerIzPlanaNaPrijemnicu`, `spPrenesiKontejnerIzPlanaNaPrijemnicuIzvoz`, `spPrenesiKontejnerIzPlanaNaPrijemnicuIzvozPoIdNadredjenog`, `spPrenesiPlanUtovaraUPrijemPlatforma`, `spPrenesiPlanUtovaraUPrijemPlatformaIzvoz`, `spPrenesiPlanUtovaraUPrijemVoz`, `spStornijaRNI`
- **Funkcionalnosti (kontrola → handler):**
  - „Učitaj“ — klik → `button4_Click` — proc: DeleteUvozKonacna, DeleteUvozKonacnaNHM, DeleteUvozKonacnaNapomenePozicioniranja, DeleteUvozKonacnaVrstaManipulacije, DeleteUvozKonacnaVrstaManipulacijeKamionska, DeleteUvozKonacnaVrstaRobeHS; preko: InsertUvozKonacna
  - „Upload“ — klik → `button2_Click` — izvoz u Excel
  - „Izbrisi selektovane“ — klik → `button1_Click` — proc: DeleteUvozKonacna, DeleteUvozKonacnaNHM, DeleteUvozKonacnaNapomenePozicioniranja, DeleteUvozKonacnaVrstaManipulacije, DeleteUvozKonacnaVrstaManipulacijeKamionska, DeleteUvozKonacnaVrstaRobeHS; preko: InsertUvozKonacna

#### frmVoz
- **Fajl:** `TrackModal/Dokumeta/frmVoz.cs`  
- **Naslov forme:** „VOZOVI“
- **Otvara forme:** frmFormiranjePlana („UVOZ - FORMIRANJE PLANA“)
- **Tabele:**
  - `IzvozKonacna` — S
  - `IzvozKonacnaZaglavlje` — S
  - `Partnerji` — S
  - `SerijeKola` — S
  - `Stanice` — S
  - `UvozKonacna` — SU
  - `UvozKonacnaZaglavlje` — S
  - `Voz` — S
  - `VozSerijeKola` — S
- **Procedure:** `DeleteIzvozKonacnaZaglavlje`, `DeleteVoz`, `DeleteVozSerijeKola`, `InsertIzvozKonacnaZaglavlje`, `InsertVoz`, `InsertVozSerijeKola`, `PrekopirajVoz`, `UpdateIzvozKonacnaZaglavlje`, `UpdateVoz`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: DeleteVoz, DeleteVozSerijeKola, InsertVoz, InsertVozSerijeKola, PrekopirajVoz, UpdateVoz; preko: InsertVoz
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteVoz, DeleteVozSerijeKola, InsertVoz, InsertVozSerijeKola, PrekopirajVoz, UpdateVoz; preko: InsertVoz
  - „UVOZ-FORMIRANJE PLANA“ — klik → `toolStripButton1_Click` — čita: UvozKonacnaZaglavlje
  - „IZVOZ - FORMIRANJE PLANA “ — klik → `toolStripButton2_Click` — otvara: frmFormiranjePlanaIzvoz; čita: IzvozKonacnaZaglavlje; proc: DeleteIzvozKonacnaZaglavlje, InsertIzvozKonacnaZaglavlje, UpdateIzvozKonacnaZaglavlje; preko: InsertIzvozKonacnaZaglavlje
  - „Relacija“ — klik → `btnUbaciRelaciju_Click`
  - „Dolazeći voz“ — čekiranje → `chkDolazeci_CheckedChanged`
  - „D“ — klik → `button1_Click`
  - „D“ — klik → `button2_Click`
  - „D“ — klik → `button3_Click`
  - „D“ — klik → `button4_Click`
  - „Izbaci“ — klik → `button6_Click` — proc: DeleteVoz, DeleteVozSerijeKola, InsertVoz, InsertVozSerijeKola, PrekopirajVoz, UpdateVoz; preko: InsertVoz
  - „Ubaci“ — klik → `button5_Click` — proc: DeleteVoz, DeleteVozSerijeKola, InsertVoz, InsertVozSerijeKola, PrekopirajVoz, UpdateVoz; preko: InsertVoz
  - „Serija kola:“ — klik → `label27_Click`
  - „U“ — klik → `button7_Click` — piše: UvozKonacna[U]; čita: IzvozKonacna, IzvozKonacnaZaglavlje
  - „IZVOZ - FORMIRANJE PLANA “ — klik → `toolStripButton2_Click` — otvara: frmFormiranjePlanaIzvoz; čita: IzvozKonacnaZaglavlje; proc: DeleteIzvozKonacnaZaglavlje, InsertIzvozKonacnaZaglavlje, UpdateIzvozKonacnaZaglavlje; preko: InsertIzvozKonacnaZaglavlje
  - „UVOZ - FORMIRANJE PLANA“ — klik → `toolStripButton1_Click` — čita: UvozKonacnaZaglavlje
  - `button20` — klik → `tsDelete_Click` — proc: DeleteVoz, DeleteVozSerijeKola, InsertVoz, InsertVozSerijeKola, PrekopirajVoz, UpdateVoz; preko: InsertVoz
  - `button21` — klik → `tsSave_Click` — proc: DeleteVoz, DeleteVozSerijeKola, InsertVoz, InsertVozSerijeKola, PrekopirajVoz, UpdateVoz; preko: InsertVoz
  - `this` — otvaranje forme → `frmVoz_Load` — čita: Partnerji, SerijeKola, Stanice

#### frmVoziloUsluga
- **Fajl:** `Uvoz/frmVoziloUsluga.cs`  
- **Naslov forme:** „VOZILO ZA USLUGU“
- **Tabele:**
  - `VoziloUsluga` — S
- **Procedure:** `DeleteVoziloUsluga`, `InsertVoziloUsluga`, `UpdateVoziloUsluga`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: DeleteVoziloUsluga, InsertVoziloUsluga, UpdateVoziloUsluga; preko: InsertVoziloUsluga
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteVoziloUsluga, InsertVoziloUsluga, UpdateVoziloUsluga; preko: InsertVoziloUsluga
  - `button20` — klik → `tsDelete_Click` — proc: DeleteVoziloUsluga, InsertVoziloUsluga, UpdateVoziloUsluga; preko: InsertVoziloUsluga
  - `button21` — klik → `tsSave_Click` — proc: DeleteVoziloUsluga, InsertVoziloUsluga, UpdateVoziloUsluga; preko: InsertVoziloUsluga

#### frmVozOtprema
- **Fajl:** `MainLeget/Prijemiotpremavozova/frmVozOtprema.cs`  
- **Naslov forme:** „frmVozOtprema“

#### frmVozPrijem
- **Fajl:** `MainLeget/Prijemiotpremavozova/frmVozPrijem.cs`  
- **Naslov forme:** „frmVozPrijem“

#### PrijemIOtpremaVozova1
- **Fajl:** `MainLeget/Prijemiotpremavozova/PrijemIOtpremaVozova1.cs`  
- **Naslov forme:** „PrijemIOtpremaVozova1“
- **Otvara forme:** frmDodatneUsluge („Dodatne usluge“), frmKontejnerTekuceArhiv („Terminal central (arhiv)“), frmPrijemIOtpremaVozova2 („Depo cnt“), frmPrijemIOtpremaVozova3 („Pregled naloga“), frmPrijemIOtpremaVozova4 („Voz“), frmRadniNalogInterniPregled („Zadaci intermodalni dispećer“), frmVozOtprema („Otprema“), frmVozPrijem („Prijem“)
- **Funkcionalnosti (kontrola → handler):**
  - „Depo cnt“ — klik → `btnPrijemIOtpremaVozova2_Click` — otvara: frmPrijemIOtpremaVozova2
  - „Voz“ — klik → `btnPrijemIOtpremaVozova6_Click` — otvara: frmPrijemIOtpremaVozova4
  - „Dodatne usluge“ — klik → `btnPrijemIOtpremaVozova5_Click` — otvara: frmDodatneUsluge
  - „Pregled naloga“ — klik → `btnPrijemIOtpremaVozova4_Click` — otvara: frmPrijemIOtpremaVozova3
  - „Terminal central (arhiv)“ — klik → `btnPrijemIOtpremaVozova3_Click` — otvara: frmKontejnerTekuceArhiv
  - „Zadaci intermodalni dispećer“ — klik → `btnPrijemIOtpremaVozova1_Click` — otvara: frmRadniNalogInterniPregled
  - „Prijem“ — klik → `btnPrijemIOtpremaVozova7_Click` — otvara: frmVozPrijem
  - „Otprema“ — klik → `btnPrijemIOtpremaVozova8_Click` — otvara: frmVozOtprema

#### UvozNHM
- **Fajl:** `Uvoz/UvozNHM.cs`  
- **Naslov forme:** „UvozNHM“
- **Tabele:**
  - `NHM` — S
  - `UvozKonacnaNHM` — S
  - `UvozNHM` — S
- **Procedure:** `DeleteUvozKonacna`, `DeleteUvozKonacnaNHM`, `DeleteUvozKonacnaNapomenePozicioniranja`, `DeleteUvozKonacnaVrstaManipulacije`, `DeleteUvozKonacnaVrstaManipulacijeKamionska`, `DeleteUvozKonacnaVrstaRobeHS`, `DeleteUvozNHM`, `DeleteUvozNHMDiana`, `DeleteUvozNapomenePozicioniranja`, `DeleteUvozTerminalskeManipulacije`, `DeleteUvozTerminalskeManipulacijeKonacna`, `DeleteUvozUslugaDokumenta`, `DeleteUvozUvoznici`, `DeleteUvozVrstaManTerminalskih`, `DeleteUvozVrstaManipulacije`, `DeleteUvozVrstaManipulacijeKamionska`, `DeleteUvozVrstaRobeHS`, `DeleteUvozkonacnaNHMDiana`, `InsUbaciDodatnuUsluguIzvoz`, `InsUbaciDodatnuUsluguUvoz`, `InsertUvozKonacbaVrstaRobeHS`, `InsertUvozKonacna`, `InsertUvozKonacnaNHM`, `InsertUvozKonacnaNHMDiana`, `InsertUvozKonacnaNapomenePozicioniranja`, `InsertUvozKonacnaVrstaManipulacije`, `InsertUvozNHM`, `InsertUvozNHMDiana`, `InsertUvozNapomenePozicioniranja`, `InsertUvozUslugaDokument`, `InsertUvozUvoznici`, `InsertUvozVrstaManipulacije`, `InsertUvozVrstaRobeHS`, `PrebaciNHMUvozUvozKonacna`, `PrebaciVrsterobeHSUvozUvozKonacna`, `PrenesiIzPlanUtovaraUPlanUtovara`, `PrenesiUPlanUtovaraSelektovano`, `PromeniSaPrijemniceUK`, `UbaciURadniNalogInterniBeyScenarija`, `UpdCenaUvozKonacnaVrstaManipulacije`, `UpdCenaUvozVrstaManipulacije`, `UpdPlatiocaUvozKonacnaVrstaManipulacije`, `UpdPlatiocaUvozVrstaManipulacije`, `UpdPokretFormaStatus`, `UpdateBrojKolaIzExcela`, `UpdateBrojKolaIzExcelaVoz`, `UpdateUvozKonacna`, `UpdateUvozKonacnaManFaktura`, `UpdateUvozKonacnaManZaFakturisanje`, `UpdateUvozKonacnaObjedinjenja`, `UpdateUvozKonacnaPK`, `UpdateUvozKonacnaUC`, `UpdateUvozNHMTezine`, `VratiIzPlanaUtovaraSelektovano`, `ZavrsiDodatnuUsluguIzvozZadata`, `ZavrsiDodatnuUsluguUvozZadata`, `spIzbrisiKUScenario`, `spPrenesiKontejnerIzPlanaNaPrijemnicu`, `spPrenesiKontejnerIzPlanaNaPrijemnicuIzvoz`, `spPrenesiKontejnerIzPlanaNaPrijemnicuIzvozPoIdNadredjenog`, `spPrenesiPlanUtovaraUPrijemPlatforma`, `spPrenesiPlanUtovaraUPrijemPlatformaIzvoz`, `spPrenesiPlanUtovaraUPrijemVoz`, `spStornijaRNI`
- **Funkcionalnosti (kontrola → handler):**
  - „Opšti“ — čekiranje → `chkOpsti_CheckedChanged`
  - „Obrni“ — klik → `button11_Click` — čita: NHM
  - „Interni“ — čekiranje → `chkInterni_CheckedChanged`
  - „Unesi“ — klik → `button1_Click` — proc: DeleteUvozKonacna, DeleteUvozKonacnaNHM, DeleteUvozKonacnaNapomenePozicioniranja, DeleteUvozKonacnaVrstaManipulacije, DeleteUvozKonacnaVrstaManipulacijeKamionska, DeleteUvozKonacnaVrstaRobeHS; preko: InsertUvozKonacna
  - „Izbrisi“ — klik → `button3_Click` — proc: DeleteUvozKonacna, DeleteUvozKonacnaNHM, DeleteUvozKonacnaNapomenePozicioniranja, DeleteUvozKonacnaVrstaManipulacije, DeleteUvozKonacnaVrstaManipulacijeKamionska, DeleteUvozKonacnaVrstaRobeHS; preko: InsertUvozKonacna
  - „Upload“ — klik → `button2_Click` — izvoz u Excel
  - „Učitaj“ — klik → `button4_Click` — proc: DeleteUvozKonacna, DeleteUvozKonacnaNHM, DeleteUvozKonacnaNapomenePozicioniranja, DeleteUvozKonacnaVrstaManipulacije, DeleteUvozKonacnaVrstaManipulacijeKamionska, DeleteUvozKonacnaVrstaRobeHS; preko: InsertUvozKonacna
  - „Izbriši učitane“ — klik → `button5_Click` — proc: DeleteUvozKonacna, DeleteUvozKonacnaNHM, DeleteUvozKonacnaNapomenePozicioniranja, DeleteUvozKonacnaVrstaManipulacije, DeleteUvozKonacnaVrstaManipulacijeKamionska, DeleteUvozKonacnaVrstaRobeHS; preko: InsertUvozKonacna
  - `this` — otvaranje forme → `UvozNHM_Load` — čita: NHM

### Intermodalni terminal (59)

#### frmAutoprevozniList2
- **Fajl:** `Dokumenta/frmAutoprevozniList2.cs`  
- **Naslov forme:** „AutoPrevozni list“
- **Modul:** Intermodalni terminal, Kapija, Prijem i otprema vozova
- **Tabele:**
  - `Autoprevoznilist` — S
  - `AutoPrevozniList` — S
  - `Komitenti` — S
  - `NajavaPrevoza` — S
  - `PutniNalog` — S
  - `RadniNalogTransport` — S
  - `Vozila` — S
  - `Zaposleni` — S
- **Procedure:** `DeleteAutoprevozniList`, `DeleteAutoprevozniListGeneralni`, `DeleteAutoprevozniListKameni`, `InsertAutoPrevozniList`, `InsertAutoPrevozniListGeneralni`, `InsertAutoPrevozniListKameni`, `UpdateAutoprevoznilist`, `UpdateAutoprevoznilistGeneralni`, `UpdateAutoprevoznilistKameni`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: DeleteAutoprevozniList, DeleteAutoprevozniListGeneralni, DeleteAutoprevozniListKameni, InsertAutoPrevozniList, InsertAutoPrevozniListGeneralni, InsertAutoPrevozniListKameni; preko: InsertAutoprevozniList
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „Štampa Autoprevozni list“ — klik → `toolStripButton1_Click` — izveštaj: rptAutoprevozniList.rdlc
  - „Izbriši“ — klik → `button1_Click` — proc: DeleteAutoprevozniList, DeleteAutoprevozniListGeneralni, DeleteAutoprevozniListKameni, InsertAutoPrevozniList, InsertAutoPrevozniListGeneralni, InsertAutoPrevozniListKameni; preko: InsertAutoprevozniList
  - „Promeni“ — klik → `button2_Click` — proc: DeleteAutoprevozniList, DeleteAutoprevozniListGeneralni, DeleteAutoprevozniListKameni, InsertAutoPrevozniList, InsertAutoPrevozniListGeneralni, InsertAutoPrevozniListKameni; preko: InsertAutoprevozniList
  - „Unesi“ — klik → `button3_Click` — proc: DeleteAutoprevozniList, DeleteAutoprevozniListGeneralni, DeleteAutoprevozniListKameni, InsertAutoPrevozniList, InsertAutoPrevozniListGeneralni, InsertAutoPrevozniListKameni; preko: InsertAutoprevozniList
  - „Izbriši“ — klik → `button4_Click` — proc: DeleteAutoprevozniList, DeleteAutoprevozniListGeneralni, DeleteAutoprevozniListKameni, InsertAutoPrevozniList, InsertAutoPrevozniListGeneralni, InsertAutoPrevozniListKameni; preko: InsertAutoprevozniList
  - „Promeni“ — klik → `button5_Click` — proc: DeleteAutoprevozniList, DeleteAutoprevozniListGeneralni, DeleteAutoprevozniListKameni, InsertAutoPrevozniList, InsertAutoPrevozniListGeneralni, InsertAutoPrevozniListKameni; preko: InsertAutoprevozniList
  - „Unesi“ — klik → `button6_Click` — proc: DeleteAutoprevozniList, DeleteAutoprevozniListGeneralni, DeleteAutoprevozniListKameni, InsertAutoPrevozniList, InsertAutoPrevozniListGeneralni, InsertAutoPrevozniListKameni; preko: InsertAutoprevozniList
  - `this` — otvaranje forme → `frmAutoprevozniList2_Load` — čita: Komitenti, Vozila

#### frmCIR
- **Fajl:** `Dokumenta/frmCIR.cs`  
- **Naslov forme:** „CIR“
- **Modul:** Intermodalni terminal, Kapija, Prijem i otprema kamiona, Prijem i otprema vozova
- **Otvara forme:** frmDodelaSkladista („ODREDI PRIVREMENU POZICIJU“)
- **Tabele:**
  - `CIR` — S
  - `CIRGreske` — S
  - `Delovi` — S
  - `Greske` — S
  - `NaruceneManipulacije` — S
  - `OtpremaKontejnera` — S
  - `OtpremaKontejneraVozStavke` — S
  - `OtpremaKontejneraVozz` — S
  - `PrijemKontejneraVoz` — S
  - `PrijemKOntejneraVozStavke` — S
  - `PrijemKontejneraVozStavke` — S
  - `TipKontenjera` — S
  - `uvKvalitetKontejnera` — S
  - `VrstaManipulacije` — S
- **Funkcionalnosti (kontrola → handler):**
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

#### frmDodatniList
- **Fajl:** `Izvestaji/frmDodatniList.cs`  
- **Naslov forme:** „Dodatni list“
- **Modul:** Intermodalni terminal, Kapija, Prijem i otprema vozova
- **Funkcionalnosti (kontrola → handler):**
  - „Štampaj“ — klik → `btnStampa_Click` — izveštaj: rptDodatniList.rdlc

#### frmDodelaSkladista
- **Fajl:** `RadniNalozi/frmDodelaSkladista.cs`  
- **Naslov forme:** „ODREDI POZICIJU“
- **Modul:** Intermodalni terminal, Kapija, Operacije pretovari, Prijem i otprema kamiona, Prijem i otprema vozova
- **Otvara forme:** frmPregledSkladistaNovi („MAPA SKLADISTA“), frmPregledSkladistaNovi („MAPA SKLADISTA“), frmPregledSkladistaNovi („MAPA SKLADISTA“), frmPregledSkladistaNovi („MAPA SKLADISTA“), frmPregledSkladistaNovi („MAPA SKLADISTA“)
- **Tabele:**
  - `KontejnerTekuce` — S
  - `Partnerji` — S
  - `Pozicija` — S
  - `RNMedjuskladisni` — S
  - `RNOtpremaPlatforme` — S
  - `RnPrijemPlatforme` — S
  - `RNPrijemPlatforme` — S
  - `RNPrijemPlatforme2` — S
  - `RNPrijemVoza` — S
  - `Skladista` — S
  - `skladista` — S
  - `TipKontenjera` — S
  - `Voz` — S
  - `VrstaCarinskogPostupka` — S
  - `VrstaManipulacije` — S
- **Procedure:** `DeleteRNMedjuskladisni`, `DeleteRNOtpremaCirade`, `DeleteRNOtpremaPlatforme`, `DeleteRNOtpremaPlatforme2`, `DeleteRNOtpremaVoza`, `DeleteRNPregledIpostavkaKontejnera`, `DeleteRNPregledIspraznjenogKontejnera`, `DeleteRNPrijemCirade`, `DeleteRNPrijemPlatforme`, `DeleteRNPrijemPlatforme2`, `DeleteRNPrijemVoza`, `DeleteRNPrijemVoza2`, `InsertRN10PregledCeoVoz`, `InsertRN11PregledCeoVozOtprema`, `InsertRN12Medjuskladisni`, `InsertRN4PrPlatformeKamIzvoz`, `InsertRN4PrPlatformeKamUvoz`, `InsertRN5PrPlatFormeKam`, `InsertRN6OtPlatFormeKam`, `InsertRN7OtpremaOtpremaPlatKam`, `InsertRN8OtpremaCiradeKam`, `InsertRN8OtpremaCiradeKamIzvoz`, `InsertRN9PrijemCiradeKam`, `InsertRNOtpremaCirade`, `InsertRNOtpremaPlatforme`, `InsertRNOtpremaPlatforme2`, `InsertRNOtpremaVoza`, `InsertRNOtpremaVozaCeoVoz`, `InsertRNPregledIpostavkaKontejnera`, `InsertRNPregledIspraznjenogKontejnera`, `InsertRNPrijemCirade`, `InsertRNPrijemPlatforme`, `InsertRNPrijemPlatforme2`, `InsertRNPrijemVoza`, `InsertRNPrijemVoza2`, `InsertRNPrijemVozaCeoVoz`, `InsertRNPrijemVozaCeoVozPretovar`, `PokreniSkladisninuKontejnera`, `PrenesiKontejnerTekuceUArhiv`, `UpdateIZCIRA`, `UpdateRN12MedjuskladisniSkladiste`, `UpdateRN12Uradjene`, `UpdateRN1Uradjene`, `UpdateRN1UradjeneCIR`, `UpdateRN1UradjeneS`, `UpdateRN1UradjeneVP`, `UpdateRN2Uradjene`, `UpdateRN3Uradjene`, `UpdateRN4PotrebanCIR`, `UpdateRN4PrivremenoSkladiste`, `UpdateRN4SkladisteIzCira`, `UpdateRN4Uradjene`, `UpdateRN4UradjeneCIR`, `UpdateRN4UradjenePostavljen`, `UpdateRN4UradjeneVizuelni`, `UpdateRN5PrijemSkladiste`, `UpdateRN5Uradjene`, `UpdateRN5UradjeneCIR`, `UpdateRN5UradjeneS`, `UpdateRN6OtpremaSkladiste`, `UpdateRN6PotrebanCIR`, `UpdateRN6Uradjene`, `UpdateRN6UradjeneVizuelni`, `UpdateRN7Uradjene`, `UpdateRN8Uradjene`, `UpdateRN8UradjeneCIR`, `UpdateRN9PretovarCIRADE`, `UpdateRN9Uradjen`, `UpdateRNMedjuskladisni`, `UpdateRNOtpremaCirade`, `UpdateRNOtpremaPlatforme`, `UpdateRNOtpremaPlatforme2`, `UpdateRNOtpremaVoza`, `UpdateRNPregledIpostavkaKontejnera`, `UpdateRNPregledIspraznjenogKontejnera`, `UpdateRNPrijemCirade`, `UpdateRNPrijemPlatforme`, `UpdateRNPrijemPlatforme2`, `UpdateRNPrijemVoza`, `UpdateRNPrijemVoza2`, `UpdateRNPrijemVozaSkladiste`, `ZatvoriSkladisninuKontejnera`, `updRN4PrPlatformeKamIzvozPusti`
- **Funkcionalnosti (kontrola → handler):**
  - „Seleckovano polje >> Selectovan zapis“ — klik → `btnUnesi_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „MAPA SKLADISTA“ — klik → `toolStripButton7_Click` — otvara: frmPregledSkladistaNovi

#### frmDokumentaOtpremaKontejnera
- **Fajl:** `Dokumenta/frmDokumentaOtpremaKontejnera.cs`  
- **Naslov forme:** „Dokumenta otprema kontejnera“
- **Modul:** Intermodalni terminal, Kapija, Prijem i otprema vozova
- **Tabele:**
  - `DokumentaOtpremaKontejneraVoz` — S
- **Procedure:** `DeleteDokumentaOtpremaKontejneraVoz`, `InsertDokumentaOtpremaKontejneraVoz`
- **Funkcionalnosti (kontrola → handler):**
  - „Sačuvaj“ — klik → `button4_Click` — proc: DeleteDokumentaOtpremaKontejneraVoz, InsertDokumentaOtpremaKontejneraVoz; preko: InsertDokumentaOtpremaVoz
  - „Otvori“ — klik → `button2_Click`
  - „Pronađi“ — klik → `button1_Click`
  - „Brisanje“ — klik → `button3_Click` — proc: DeleteDokumentaOtpremaKontejneraVoz, InsertDokumentaOtpremaKontejneraVoz; preko: InsertDokumentaOtpremaVoz

#### frmDokumentaPrijemKontejneraKamion
- **Fajl:** `Dokumenta/frmDokumentaPrijemKontejneraKamion.cs`  
- **Naslov forme:** „Dokumenta prijem kontejnera kamionom“
- **Modul:** Intermodalni terminal, Kapija, Operacije pretovari, Prijem i otprema vozova
- **Tabele:**
  - `DokumentaPrijemKontejneraKamion` — S
- **Procedure:** `DokumentaPrijemKontejneraKamion`, `InsertDokumentaPrijemKontejneraKamion`
- **Funkcionalnosti (kontrola → handler):**
  - „Sačuvaj“ — klik → `button4_Click` — proc: DokumentaPrijemKontejneraKamion, InsertDokumentaPrijemKontejneraKamion; preko: InsertDokumentaPrijemKontejneraKamion
  - „Otvori“ — klik → `button2_Click`
  - „Pronađi“ — klik → `button1_Click`

#### frmDokumentaPrijemKontejneraVoz
- **Fajl:** `Dokumenta/frmDokumentaPrijemKontejneraVoz.cs`  
- **Naslov forme:** „Dokumenta prijema“
- **Modul:** Intermodalni terminal, Kapija, Prijem i otprema vozova
- **Tabele:**
  - `DokumentaPrijemKontejneraVoz` — S
- **Funkcionalnosti (kontrola → handler):**
  - „Sačuvaj“ — klik → `button4_Click`
  - „Otvori“ — klik → `button2_Click`
  - „Pronađi“ — klik → `button1_Click`
  - „Izbriši“ — klik → `button3_Click`

#### frmFormiranjePlanaIzvoz
- **Fajl:** `Izvoz/frmFormiranjePlanaIzvoz.cs`  
- **Naslov forme:** „Formiranje plana izvoz - popunjavanje kontejnerima“
- **Modul:** Intermodalni terminal, Prijem i otprema vozova
- **Otvara forme:** frmIzvozKonacna („Otvori plan“), frmIzvozKonacna („Otvori plan“)
- **Tabele:**
  - `Carinarnice` — S
  - `InspekciskiTretman` — S
  - `Izvoz` — SIU
  - `IzvozKonacna` — SU
  - `IzvozKonacnaZaglavlje` — S
  - `IzvozNHM` — S
  - `IzvozVrstaManipulacije` — S
  - `IzvozVrstaRobeHS` — S
  - `KontejnerskiTerminali` — S
  - `KontejnerStatus` — S
  - `KrajnjaDestinacija` — S
  - `MestaUtovara` — S
  - `NHM` — S
  - `OrganizacioneJedinice` — S
  - `Partnerji` — S
  - `RadniNalogInterni` — S
  - `Scenario` — S
  - `SerijeKola` — S
  - `TipKontenjera` — S
  - `uvNacinPakovanja` — S
  - `UvozKonacna` — S
  - `UvozKonacnaNapomenePozicioniranja` — S
  - `UvozKonacnaVrstaManipulacije` — S
  - `UvozKonacnaZaglavlje` — S
  - `Voz` — S
  - `VozSerijeKola` — S
  - `VrstaCarinskogPostupka` — S
  - `VrstaManipulacije` — S
  - `VrstaRobeHS` — S
  - `VrstePostupakaUvoz` — S
- **Procedure:** `DeleteIzvozKonacnaNHM`, `DeleteIzvozKonacnaNapomenePozicioniranja`, `DeleteIzvozKonacnaSve`, `DeleteIzvozKonacnaVrstaManipulacije`, `DeleteIzvozKonacnaVrstaRobeHS`, `DeleteIzvozNHM`, `DeleteIzvozNapomenePozicioniranja`, `DeleteIzvozSve`, `DeleteIzvozUslugaDokumenta`, `DeleteIzvozVrstaManTerminalskih`, `DeleteIzvozVrstaManipulacije`, `DeleteIzvozVrstaRobeHS`, `DeleteKontejnerIzvoz`, `DeleteVaganje`, `InsertIzvozKonacnaNHM`, `InsertIzvozKonacnaNapomenePozicioniranja`, `InsertIzvozKonacnaVMIzUvoza`, `InsertIzvozKonacnaVrstaManipulacije`, `InsertIzvozNHM`, `InsertIzvozNapomenePozicioniranja`, `InsertIzvozPorudzbenica`, `InsertIzvozUslugaDokument`, `InsertIzvozVMIzUvoza`, `InsertIzvozVrstaManipulacije`, `InsertIzvozVrstaRobeHS`, `InsertIzvozkonacnaVrstaRobeHS`, `InsertKontejnerLog`, `InsertVaganje`, `KopirajKontejnerIzvoz`, `OdrediKontejnerTerminal`, `PrenesiIzPlanIzvozaUPlanIzvoza`, `PrenesiUPlanUtovaraSelektovanoIzvoz`, `PrenesiUPlanUtovaraSelektovanoIzvozBrodar`, `PrenesiUPlanUtovaraSelektovanoIzvozTErminal`, `UpdCenaIzvozKonacnaVrstaManipulacije`, `UpdCenaIzvozVrstaManipulacije`, `UpdPDVIzvozKonacnaVrstaManipulacije`, `UpdPDVIzvozVrstaManipulacije`, `UpdPlatiocaIzvozKonacnaVrstaManipulacije`, `UpdPlatiocaIzvozVrstaManipulacije`, `UpdateIzvoz`, `UpdateIzvozKonacna`, `UpdateIzvozKonacnaVaganjeCirada`, `UpdateIzvozKonacnaVaganjePlatforma`, `UpdateIzvozKonacnaVagon`, `UpdateIzvozPorudzbenica`, `UpdateIzvozPorudzbenicaCarina`, `UpdateIzvozPorudzbenicaPojedinacni`, `UpdateIzvozPorudzbenicaRelacija`, `UpdateIzvozStatus`, `UpdateIzvozStatusTerminal`, `UpdateIzvozTerminalIzbor`, `UpdateVaganje`, `VratiKontejnerNerasporedjenoSelektovanoIzvoz`, `spPrenesiIzPlanUtovaraUOtpremaVoz`, `spPrenesiIzUvozaUIzvoz`, `spPrenesiKontejnerIzNerasporedjenihUPrijPlatforma`, `spPrenesiKontejnerUOtpremuKamionom`, `spPrenesiKontejnerUOtpremuKamionomIzvoz`
- **Funkcionalnosti (kontrola → handler):**
  - „Refresh“ — klik → `button2_Click`
  - „Prenesi selektovano“ — klik → `button1_Click` — piše: IzvozKonacna[U], Izvoz[U]; proc: DeleteIzvozKonacnaNHM, DeleteIzvozKonacnaNapomenePozicioniranja, DeleteIzvozKonacnaSve, DeleteIzvozKonacnaVrstaManipulacije, DeleteIzvozKonacnaVrstaRobeHS, DeleteIzvozNHM; preko: InsertIzvoz, InsertIzvozKonacna
  - „Otvori plan“ — klik → `button4_Click` — otvara: frmIzvozKonacna
  - „Vrati selektovano“ — klik → `button3_Click` — proc: DeleteIzvozKonacnaNapomenePozicioniranja, DeleteIzvozKonacnaSve, DeleteIzvozNapomenePozicioniranja, DeleteIzvozUslugaDokumenta, DeleteIzvozVrstaManTerminalskih, InsertIzvozKonacnaNapomenePozicioniranja; preko: InsertIzvozKonacna
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`

#### frmIzvozDokumenta
- **Fajl:** `Izvoz/frmIzvozDokumenta.cs`  
- **Naslov forme:** „Izvoz dokumentacija“
- **Modul:** Intermodalni terminal, Operacije skladišta, Prijem i otprema kamiona, Prijem i otprema vozova
- **Tabele:**
  - `IzvozDokumenta` — S
  - `TipKomercijalnogDokumenta` — S
- **Procedure:** `DeleteIzvozDokumenta`, `InsertIzvozDokumenta`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „Dokumentacija“ — klik → `toolStripButton1_Click`
  - „Izbriši“ — klik → `button3_Click` — proc: DeleteIzvozDokumenta, InsertIzvozDokumenta; preko: InsertIzvozDokumenta
  - „Sačuvaj“ — klik → `button4_Click` — proc: DeleteIzvozDokumenta, InsertIzvozDokumenta; preko: InsertIzvozDokumenta
  - „Otvori“ — klik → `button2_Click`
  - „Pronađi“ — klik → `button1_Click`
  - „Izbriši“ — klik → `button5_Click` — proc: DeleteIzvozDokumenta, InsertIzvozDokumenta; preko: InsertIzvozDokumenta
  - „Pronađi“ — klik → `button6_Click`
  - „Sačuvaj“ — klik → `button7_Click` — proc: DeleteIzvozDokumenta, InsertIzvozDokumenta; preko: InsertIzvozDokumenta
  - „Otvori“ — klik → `button8_Click`
  - „Izbriši“ — klik → `button9_Click` — proc: DeleteIzvozDokumenta, InsertIzvozDokumenta; preko: InsertIzvozDokumenta
  - „Pronađi“ — klik → `button10_Click`
  - „Sačuvaj“ — klik → `button11_Click` — proc: DeleteIzvozDokumenta, InsertIzvozDokumenta; preko: InsertIzvozDokumenta
  - „Otvori“ — klik → `button12_Click`

#### frmIzvozKonacna
- **Fajl:** `Izvoz/frmIzvozKonacna.cs`  
- **Naslov forme:** „Izvoz konačna“
- **Modul:** Intermodalni terminal, Prijem i otprema vozova
- **Otvara forme:** frmIzvozKonacnaTable („Tabela“), frmIzvozPregledKontejneraDrumskeUsluge („Drumske usluge“), frmIzvozUnosManipulacije („Unos usluga“), frmKontaktOsobe (`button16` (Click)), frmKontaktOsobeMU (`button22` (Click)), frmIzvozDokumenta („Dokumentacija“), frmIzvozKonacnaTable („Tabela“), frmIzvozPregledKontejneraDrumskeUsluge („Drumske usluge“), frmIzvozUnosManipulacije („Unos usluga“), frmKontaktOsobe (`button16` (Click)), frmKontaktOsobeMU (`button22` (Click))
- **Tabele:**
  - `Carinarnice` — S
  - `Cene` — S
  - `InspekciskiTretman` — S
  - `IzvozKonacna` — S
  - `IzvozKonacnaNapomenePozicioniranja` — S
  - `IzvozKonacnaNHM` — S
  - `IzvozkonacnaNHM` — S
  - `IzvozKonacnaVrstaManipulacije` — S
  - `IzvozKonacnaVrstaRobeHS` — S
  - `IzvozKonacnaZaglavlje` — S
  - `IzvozNHM` — S
  - `IzvozVrstaManipulacije` — S
  - `IzvozVrstaRobeHS` — S
  - `KontejnerskiTerminali` — S
  - `KontejnerStatus` — S
  - `KrajnjaDestinacija` — S
  - `MestaUtovara` — S
  - `NHM` — S
  - `OrganizacioneJedinice` — S
  - `Partnerji` — S
  - `PArtnerji` — S
  - `partnerjiKontOseba` — S
  - `partnerjiKontOsebaMU` — S
  - `PredefinisanePoruke` — S
  - `RAdniNalogInterni` — S
  - `Scenario` — S
  - `TipKontenjera` — S
  - `uvNacinPakovanja` — S
  - `UvozKonacna` — S
  - `UvozKonacnaVrstaManipulacije` — S
  - `Valute` — S
  - `Voz` — S
  - `VrstaCarinskogPostupka` — S
  - `VrstaManipulacije` — S
  - `VrstaRobeADR` — S
  - `VrstaRobeHS` — S
  - `VrstePostupakaUvoz` — S
- **Procedure:** `DeleteIzvozKonacnaNHM`, `DeleteIzvozKonacnaNapomenePozicioniranja`, `DeleteIzvozKonacnaSve`, `DeleteIzvozKonacnaVrstaManipulacije`, `DeleteIzvozKonacnaVrstaRobeHS`, `DeleteIzvozKonacnaZaglavlje`, `DeleteIzvozNHM`, `DeleteIzvozNapomenePozicioniranja`, `DeleteIzvozSve`, `DeleteIzvozUslugaDokumenta`, `DeleteIzvozVrstaManTerminalskih`, `DeleteIzvozVrstaManipulacije`, `DeleteIzvozVrstaRobeHS`, `DeleteKontejnerIzvoz`, `DeleteVaganje`, `InsertIzvozKonacnaNHM`, `InsertIzvozKonacnaNapomenePozicioniranja`, `InsertIzvozKonacnaVMIzUvoza`, `InsertIzvozKonacnaVrstaManipulacije`, `InsertIzvozKonacnaZaglavlje`, `InsertIzvozNHM`, `InsertIzvozNapomenePozicioniranja`, `InsertIzvozPorudzbenica`, `InsertIzvozUslugaDokument`, `InsertIzvozVMIzUvoza`, `InsertIzvozVrstaManipulacije`, `InsertIzvozVrstaRobeHS`, `InsertIzvozkonacnaVrstaRobeHS`, `InsertKontejnerLog`, `InsertVaganje`, `KopirajKontejnerIzvoz`, `OdrediKontejnerTerminal`, `PrenesiIzPlanIzvozaUPlanIzvoza`, `PrenesiUPlanUtovaraSelektovanoIzvoz`, `PrenesiUPlanUtovaraSelektovanoIzvozBrodar`, `PrenesiUPlanUtovaraSelektovanoIzvozTErminal`, `UpdCenaIzvozKonacnaVrstaManipulacije`, `UpdCenaIzvozVrstaManipulacije`, `UpdPDVIzvozKonacnaVrstaManipulacije`, `UpdPDVIzvozVrstaManipulacije`, `UpdPlatiocaIzvozKonacnaVrstaManipulacije`, `UpdPlatiocaIzvozVrstaManipulacije`, `UpdateIzvoz`, `UpdateIzvozKonacna`, `UpdateIzvozKonacnaVaganjeCirada`, `UpdateIzvozKonacnaVaganjePlatforma`, `UpdateIzvozKonacnaVagon`, `UpdateIzvozKonacnaZaglavlje`, `UpdateIzvozPorudzbenica`, `UpdateIzvozPorudzbenicaCarina`, `UpdateIzvozPorudzbenicaPojedinacni`, `UpdateIzvozPorudzbenicaRelacija`, `UpdateIzvozStatus`, `UpdateIzvozStatusTerminal`, `UpdateIzvozTerminalIzbor`, `UpdateVaganje`, `VratiKontejnerNerasporedjenoSelektovanoIzvoz`, `spPrenesiIzPlanUtovaraUOtpremaVoz`, `spPrenesiIzUvozaUIzvoz`, `spPrenesiKontejnerIzNerasporedjenihUPrijPlatforma`, `spPrenesiKontejnerUOtpremuKamionom`, `spPrenesiKontejnerUOtpremuKamionomIzvoz`
- **Funkcionalnosti (kontrola → handler):**
  - „tsSave“ — klik → `tsSave_Click` — proc: DeleteIzvozKonacnaNHM, DeleteIzvozKonacnaVrstaManipulacije, DeleteIzvozKonacnaVrstaRobeHS, DeleteIzvozNHM, DeleteIzvozSve, DeleteIzvozVrstaManipulacije; preko: InsertIzvoz
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteIzvozKonacnaNapomenePozicioniranja, DeleteIzvozKonacnaSve, DeleteIzvozNapomenePozicioniranja, DeleteIzvozUslugaDokumenta, DeleteIzvozVrstaManTerminalskih, InsertIzvozKonacnaNapomenePozicioniranja; preko: InsertIzvozKonacna
  - „Dokumentacija-SHD“ — klik → `toolStripButton6_Click` — otvara: frmIzvozDokumenta
  - „Tabela - SH-T“ — klik → `toolStripButton5_Click` — otvara: frmIzvozKonacnaTable
  - „Usluge - SH - U“ — klik → `toolStripButton4_Click` — otvara: frmIzvozUnosManipulacije
  - „Izdavanje naloga“ — klik → `toolStripButton1_Click`
  - „Dodavanje nerasporedjenih u plan“ — klik → `toolStripButton2_Click` — otvara: frmFormiranjePlanaIzvoz
  - „Vrati kontejner u nerasporedjene“ — klik → `toolStripButton3_Click` — proc: DeleteIzvozKonacnaNapomenePozicioniranja, DeleteIzvozKonacnaSve, DeleteIzvozNapomenePozicioniranja, DeleteIzvozUslugaDokumenta, DeleteIzvozVrstaManTerminalskih, InsertIzvozKonacnaNapomenePozicioniranja; preko: InsertIzvozKonacna
  - „Otprema kontejnera Terminal“ — klik → `toolStripButton8_Click` — otvara: frmOtpremaVozaIzPlana
  - „OPŠTI“ — klik → `button10_Click`
  - „V V V V V“ — klik → `button9_Click`
  - „NPP V V“ — klik → `button8_Click`
  - „Vaganje“ — čekiranje → `chkVaganje_CheckedChanged`
  - „Direktno “ — čekiranje → `chkNacinPretovara_CheckedChanged`
  - „Najava vozila/cnt i vozača“ — čekiranje → `chkNajavaVozila_CheckedChanged`
  - „Delete“ — klik → `button2_Click` — proc: DeleteIzvozKonacnaNHM, DeleteIzvozKonacnaVrstaManipulacije, DeleteIzvozKonacnaVrstaRobeHS, DeleteIzvozNHM, DeleteIzvozSve, DeleteIzvozVrstaManipulacije; preko: InsertIzvoz
  - „Unesi“ — klik → `button4_Click` — proc: DeleteIzvozKonacnaNHM, DeleteIzvozKonacnaVrstaManipulacije, DeleteIzvozKonacnaVrstaRobeHS, DeleteIzvozNHM, DeleteIzvozSve, DeleteIzvozVrstaManipulacije; preko: InsertIzvoz
  - „Izbriši“ — klik → `button3_Click` — proc: DeleteIzvozKonacnaNHM, DeleteIzvozKonacnaVrstaManipulacije, DeleteIzvozKonacnaVrstaRobeHS, DeleteIzvozNHM, DeleteIzvozSve, DeleteIzvozVrstaManipulacije; preko: InsertIzvoz
  - „Unesi“ — klik → `button1_Click` — proc: DeleteIzvozKonacnaNHM, DeleteIzvozKonacnaVrstaManipulacije, DeleteIzvozKonacnaVrstaRobeHS, DeleteIzvozNHM, DeleteIzvozSve, DeleteIzvozVrstaManipulacije; preko: InsertIzvoz
  - „Unos usluga“ — klik → `button14_Click` — otvara: frmIzvozUnosManipulacije
  - „Unesi“ — klik → `button11_Click` — čita: NHM
  - „Unesi“ — klik → `button15_Click` — čita: NHM
  - `button12` — klik → `button12_Click` — čita: partnerjiKontOsebaMU
  - „?“ — klik → `button17_Click`
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - „Izbriši“ — klik → `button20_Click` — proc: DeleteIzvozKonacnaNapomenePozicioniranja, DeleteIzvozKonacnaSve, DeleteIzvozNapomenePozicioniranja, DeleteIzvozUslugaDokumenta, DeleteIzvozVrstaManTerminalskih, InsertIzvozKonacnaNapomenePozicioniranja; preko: InsertIzvozKonacna
  - „Unesi“ — klik → `button21_Click` — proc: DeleteIzvozKonacnaNapomenePozicioniranja, DeleteIzvozKonacnaSve, DeleteIzvozNapomenePozicioniranja, DeleteIzvozUslugaDokumenta, DeleteIzvozVrstaManTerminalskih, InsertIzvozKonacnaNapomenePozicioniranja; preko: InsertIzvozKonacna
  - „Indirektno“ — čekiranje → `chkIndirektno_CheckedChanged`
  - `button22` — klik → `button22_Click` — otvara: frmKontaktOsobeMU; čita: partnerjiKontOsebaMU
  - „Terminal“ — čekiranje → `checkBox1_CheckedChanged`
  - „Drumske usluge“ — klik → `button32_Click` — otvara: frmIzvozPregledKontejneraDrumskeUsluge
  - „Pakovanje voza“ — klik → `button31_Click` — otvara: frmPovezivanjeKontejneraIVagona
  - „Otprema kontejnera Terminal“ — klik → `toolStripButton8_Click` — otvara: frmOtpremaVozaIzPlana
  - „Vrati u nerasporedjene“ — klik → `toolStripButton3_Click` — proc: DeleteIzvozKonacnaNapomenePozicioniranja, DeleteIzvozKonacnaSve, DeleteIzvozNapomenePozicioniranja, DeleteIzvozUslugaDokumenta, DeleteIzvozVrstaManTerminalskih, InsertIzvozKonacnaNapomenePozicioniranja; preko: InsertIzvozKonacna
  - „Dodaj nerasporedjene“ — klik → `toolStripButton2_Click` — otvara: frmFormiranjePlanaIzvoz
  - „Izdavanje naloga“ — klik → `toolStripButton1_Click`
  - „Tabela“ — klik → `toolStripButton5_Click` — otvara: frmIzvozKonacnaTable
  - „Usluge“ — klik → `toolStripButton4_Click` — otvara: frmIzvozUnosManipulacije
  - „Dokumentacija“ — klik → `toolStripButton6_Click` — otvara: frmIzvozDokumenta
  - `button6` — klik → `tsDelete_Click` — proc: DeleteIzvozKonacnaNapomenePozicioniranja, DeleteIzvozKonacnaSve, DeleteIzvozNapomenePozicioniranja, DeleteIzvozUslugaDokumenta, DeleteIzvozVrstaManTerminalskih, InsertIzvozKonacnaNapomenePozicioniranja; preko: InsertIzvozKonacna
  - `button7` — klik → `tsSave_Click` — proc: DeleteIzvozKonacnaNHM, DeleteIzvozKonacnaVrstaManipulacije, DeleteIzvozKonacnaVrstaRobeHS, DeleteIzvozNHM, DeleteIzvozSve, DeleteIzvozVrstaManipulacije; preko: InsertIzvoz
  - `button30` — klik → `button30_Click` — čita: Partnerji, partnerjiKontOseba

#### frmIzvozKonacnaTable
- **Fajl:** `Izvoz/frmIzvozKonacnaTable.cs`  
- **Naslov forme:** „Tabela izvoz konačna“
- **Modul:** Intermodalni terminal, Prijem i otprema vozova
- **Tabele:**
  - `Carinarnice` — S
  - `InspekciskiTretman` — S
  - `IzvozKonacna` — SU
  - `IzvozKonacnaNHM` — S
  - `IzvozKonacnaVrstaManipulacije` — S
  - `IzvozKonacnaVrstaRobeHS` — S
  - `KontejnerskiTerminali` — S
  - `KrajnjaDestinacija` — S
  - `MestaUtovara` — S
  - `NHM` — S
  - `Partnerji` — S
  - `TipKontenjera` — S
  - `uvNacinPakovanja` — S
  - `Valute` — S
  - `VrstaCarinskogPostupka` — S
  - `VrstaManipulacije` — S
  - `VrstaRobeHS` — S
  - `VrstePostupakaUvoz` — S
- **Funkcionalnosti (kontrola → handler):**
  - „IZLAZ“ — klik → `button1_Click`
  - „Primeni izmenu“ — klik → `button2_Click`
  - „Izberi“ — klik → `button3_Click`
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - „Refresh“ — klik → `button4_Click`

#### frmIzvozKopiranjeUslugaKontejnera1
- **Fajl:** `Izvoz/frmIzvozKopiranjeUslugaKontejnera1.cs`  
- **Naslov forme:** „Izvoz kopiranje usluga kontejnera“
- **Modul:** Intermodalni terminal, Logistika izvoza, Prijem i otprema vozova
- **Tabele:**
  - `IzvozKonacna` — S
- **Procedure:** `DeleteIzvozKonacnaNHM`, `DeleteIzvozKonacnaVrstaManipulacije`, `DeleteIzvozKonacnaVrstaRobeHS`, `DeleteIzvozNHM`, `DeleteIzvozSve`, `DeleteIzvozVrstaManipulacije`, `DeleteIzvozVrstaRobeHS`, `DeleteKontejnerIzvoz`, `DeleteVaganje`, `InsertIzvozKonacnaNHM`, `InsertIzvozKonacnaVMIzUvoza`, `InsertIzvozKonacnaVrstaManipulacije`, `InsertIzvozNHM`, `InsertIzvozPorudzbenica`, `InsertIzvozVMIzUvoza`, `InsertIzvozVrstaManipulacije`, `InsertIzvozVrstaRobeHS`, `InsertIzvozkonacnaVrstaRobeHS`, `InsertKontejnerLog`, `InsertVaganje`, `KopirajKontejnerIzvoz`, `UpdCenaIzvozKonacnaVrstaManipulacije`, `UpdCenaIzvozVrstaManipulacije`, `UpdPDVIzvozKonacnaVrstaManipulacije`, `UpdPDVIzvozVrstaManipulacije`, `UpdPlatiocaIzvozKonacnaVrstaManipulacije`, `UpdPlatiocaIzvozVrstaManipulacije`, `UpdateIzvoz`, `UpdateIzvozKonacna`, `UpdateIzvozKonacnaVaganjeCirada`, `UpdateIzvozKonacnaVaganjePlatforma`, `UpdateIzvozPorudzbenica`, `UpdateIzvozPorudzbenicaCarina`, `UpdateIzvozPorudzbenicaPojedinacni`, `UpdateIzvozPorudzbenicaRelacija`, `UpdateIzvozStatus`, `UpdateIzvozStatusTerminal`, `UpdateIzvozTerminalIzbor`, `UpdateVaganje`, `spPrenesiKontejnerIzNerasporedjenihUPrijPlatforma`
- **Funkcionalnosti (kontrola → handler):**
  - „Kopiraj“ — klik → `button23_Click` — proc: DeleteIzvozKonacnaNHM, DeleteIzvozKonacnaVrstaManipulacije, DeleteIzvozKonacnaVrstaRobeHS, DeleteIzvozNHM, DeleteIzvozSve, DeleteIzvozVrstaManipulacije; preko: InsertIzvoz

#### frmIzvozPregledKontejneraDrumskeUsluge
- **Fajl:** `Izvoz/frmIzvozPregledKontejneraDrumskeUsluge.cs`  
- **Naslov forme:** „Generisanje drumske usluge“
- **Modul:** Intermodalni terminal, Logistika izvoza, Prijem i otprema vozova
- **Tabele:**
  - `Automobili` — S
  - `IzvozKonacna` — S
  - `IzvozKonacnaVrstaManipulacije` — S
  - `IzvozVrstaManipulacije` — S
  - `OrganizacioneJedinice` — S
  - `RadniNalogDrumski` — S
  - `StatusVozila` — S
  - `VrstaManipulacije` — S
- **Procedure:** `DeleteUvoz`, `DodajStavkeURadniNalog`, `KopirajKontejner`, `KopirajKontejnerIzKonacne`, `NapraviRadniNalogDrumski`, `UpdCenaUvozKonacnaVrstaManipulacije`, `UpdCenaUvozVrstaManipulacije`, `UpdPDVUvozKonacnaVrstaManipulacije`, `UpdPDVUvozVrstaManipulacije`, `UpdateUvoz`
- **Funkcionalnosti (kontrola → handler):**
  - „Dopuna naloga“ — klik → `button1_Click` — proc: DeleteUvoz, DodajStavkeURadniNalog, KopirajKontejner, KopirajKontejnerIzKonacne, NapraviRadniNalogDrumski, UpdCenaUvozKonacnaVrstaManipulacije; preko: InsertUvoz
  - „Formiranje naloga“ — klik → `button23_Click` — proc: DeleteUvoz, DodajStavkeURadniNalog, KopirajKontejner, KopirajKontejnerIzKonacne, NapraviRadniNalogDrumski, UpdCenaUvozKonacnaVrstaManipulacije; preko: InsertUvoz

#### frmIzvozUnosManipulacije
- **Fajl:** `Izvoz/frmIzvozUnosManipulacije.cs`  
- **Naslov forme:** „Unos usluge izvoz“
- **Modul:** Intermodalni terminal, Logistika izvoza, Prijem i otprema vozova
- **Otvara forme:** frmIzvozKopiranjeUslugaKontejnera1 („Kopiraj usluge“), frmIzvozKopiranjeUslugaKontejnera1 („Kopiraj usluge“), frmIzvozKopiranjeUslugaKontejnera1 („Kopiraj usluge“)
- **Tabele:**
  - `Carinarnice` — S
  - `Cene` — S
  - `GrupaVrsteManipulacije` — S
  - `InspekciskiTretman` — S
  - `Izvoz` — SU
  - `IzvozKonacna` — SU
  - `IzvozKonacnaNHM` — S
  - `IzvozKonacnaVrstaManipulacije` — S
  - `IzvozKonacnaVrstaRobeHS` — S
  - `IzvozNHM` — S
  - `IzvozVrstaManipulacije` — SU
  - `IzvozVrstaRobeHS` — S
  - `KontejnerskiTerminali` — S
  - `KontejnerStatus` — S
  - `kontejnerStatus` — S
  - `KrajnjaDestinacija` — S
  - `MestaUtovara` — S
  - `NHM` — S
  - `OrganizacioneJedinice` — S
  - `PArtnerji` — S
  - `Partnerji` — S
  - `Scenario` — S
  - `TipKontenjera` — S
  - `uvNacinPakovanja` — S
  - `UvozKonacnaVrstaManipulacije` — S
  - `UvozVrstaManipulacije` — S
  - `VrstaCarinskogPostupka` — S
  - `vrstamanipulacije` — S
  - `VrstaManipulacije` — S
  - `VrstaRobeHS` — S
  - `VrstePostupakaUvoz` — S
- **Procedure:** `DeleteIzvozKonacnaNHM`, `DeleteIzvozKonacnaVrstaManipulacije`, `DeleteIzvozKonacnaVrstaRobeHS`, `DeleteIzvozNHM`, `DeleteIzvozSve`, `DeleteIzvozVrstaManipulacije`, `DeleteIzvozVrstaRobeHS`, `DeleteKontejnerIzvoz`, `DeleteScenario`, `DeleteVaganje`, `InsertIzvozKonacnaNHM`, `InsertIzvozKonacnaVMIzUvoza`, `InsertIzvozKonacnaVrstaManipulacije`, `InsertIzvozNHM`, `InsertIzvozPorudzbenica`, `InsertIzvozVMIzUvoza`, `InsertIzvozVrstaManipulacije`, `InsertIzvozVrstaRobeHS`, `InsertIzvozkonacnaVrstaRobeHS`, `InsertKontejnerLog`, `InsertScenario`, `InsertVaganje`, `KopirajKontejnerIzvoz`, `UpdCenaIzvozKonacnaVrstaManipulacije`, `UpdCenaIzvozVrstaManipulacije`, `UpdPDVIzvozKonacnaVrstaManipulacije`, `UpdPDVIzvozVrstaManipulacije`, `UpdPlatiocaIzvozKonacnaVrstaManipulacije`, `UpdPlatiocaIzvozVrstaManipulacije`, `UpdateIzvoz`, `UpdateIzvozKonacna`, `UpdateIzvozKonacnaVaganjeCirada`, `UpdateIzvozKonacnaVaganjePlatforma`, `UpdateIzvozPorudzbenica`, `UpdateIzvozPorudzbenicaCarina`, `UpdateIzvozPorudzbenicaPojedinacni`, `UpdateIzvozPorudzbenicaRelacija`, `UpdateIzvozStatus`, `UpdateIzvozStatusTerminal`, `UpdateIzvozTerminalIzbor`, `UpdateScenario`, `UpdateScenarioKontejnera`, `UpdateVaganje`, `spPrenesiKontejnerIzNerasporedjenihUPrijPlatforma`
- **Funkcionalnosti (kontrola → handler):**
  - „N3-BU“ — klik → `button5_Click`
  - „N2-BU“ — klik → `button4_Click`
  - „N1-BU“ — klik → `button3_Click`
  - „N3“ — klik → `button2_Click`
  - „N2“ — klik → `button1_Click`
  - `dataGridView7` — klik na ćeliju → `dataGridView7_CellClick` — proc: DeleteIzvozKonacnaNHM, DeleteIzvozKonacnaVrstaManipulacije, DeleteIzvozKonacnaVrstaRobeHS, DeleteIzvozNHM, DeleteIzvozSve, DeleteIzvozVrstaManipulacije; preko: InsertIzvoz
  - `dataGridView7` — CellLeave → `dataGridView7_CellLeave` — proc: DeleteIzvozKonacnaNHM, DeleteIzvozKonacnaVrstaManipulacije, DeleteIzvozKonacnaVrstaRobeHS, DeleteIzvozNHM, DeleteIzvozSve, DeleteIzvozVrstaManipulacije; preko: InsertIzvoz
  - „N1 >>>>“ — klik → `button12_Click` — čita: Cene
  - „OPŠTI“ — klik → `button11_Click`
  - „N1“ — klik → `button13_Click`
  - „N2 >>>>“ — klik → `button6_Click` — čita: Cene
  - „N3 >>>>“ — klik → `button7_Click` — čita: Cene
  - „Obrisi selektovane“ — klik → `button8_Click` — proc: DeleteIzvozKonacnaNHM, DeleteIzvozKonacnaVrstaManipulacije, DeleteIzvozKonacnaVrstaRobeHS, DeleteIzvozNHM, DeleteIzvozSve, DeleteIzvozVrstaManipulacije; preko: InsertIzvoz
  - „Promeni  platioca“ — klik → `button9_Click` — proc: DeleteIzvozKonacnaNHM, DeleteIzvozKonacnaVrstaManipulacije, DeleteIzvozKonacnaVrstaRobeHS, DeleteIzvozNHM, DeleteIzvozSve, DeleteIzvozVrstaManipulacije; preko: InsertIzvoz
  - „P >>>>“ — klik → `button10_Click` — čita: Cene
  - „SVE USLUGE“ — klik → `button14_Click`
  - „ADMINISTRATIVNE“ — klik → `button15_Click`
  - „Prikazi sve kontejnere“ — klik → `toolStripButton6_Click`
  - „Prikazi selektovani kontejner“ — klik → `toolStripButton1_Click`
  - „Prenesi uvozne usluge pakovanja voza - Pakovanje voza“ — klik → `toolStripButton2_Click` — piše: IzvozKonacna[U], Izvoz[U]; proc: DeleteIzvozKonacnaNHM, DeleteIzvozKonacnaVrstaManipulacije, DeleteIzvozKonacnaVrstaRobeHS, DeleteIzvozNHM, DeleteIzvozSve, DeleteIzvozVrstaManipulacije; preko: InsertIzvoz
  - „PO SCENARIJU“ — klik → `button16_Click`
  - „DODATNE“ — klik → `button17_Click`
  - „PO GRUPI“ — klik → `button18_Click`
  - „Promeni  platioca“ — klik → `button19_Click` — proc: DeleteIzvozKonacnaNHM, DeleteIzvozKonacnaVrstaManipulacije, DeleteIzvozKonacnaVrstaRobeHS, DeleteIzvozNHM, DeleteIzvozSve, DeleteIzvozVrstaManipulacije; preko: InsertIzvoz
  - „Promeni  platioca“ — klik → `button20_Click` — proc: DeleteIzvozKonacnaNHM, DeleteIzvozKonacnaVrstaManipulacije, DeleteIzvozKonacnaVrstaRobeHS, DeleteIzvozNHM, DeleteIzvozSve, DeleteIzvozVrstaManipulacije; preko: InsertIzvoz
  - „Promeni  platioca“ — klik → `button21_Click` — proc: DeleteIzvozKonacnaNHM, DeleteIzvozKonacnaVrstaManipulacije, DeleteIzvozKonacnaVrstaRobeHS, DeleteIzvozNHM, DeleteIzvozSve, DeleteIzvozVrstaManipulacije; preko: InsertIzvoz
  - „S N I M I“ — klik → `button22_Click`
  - „Kopiraj usluge“ — klik → `button27_Click` — otvara: frmIzvozKopiranjeUslugaKontejnera1
  - „Prenesi uvozne usluge“ — klik → `toolStripButton2_Click` — piše: IzvozKonacna[U], Izvoz[U]; proc: DeleteIzvozKonacnaNHM, DeleteIzvozKonacnaVrstaManipulacije, DeleteIzvozKonacnaVrstaRobeHS, DeleteIzvozNHM, DeleteIzvozSve, DeleteIzvozVrstaManipulacije; preko: InsertIzvoz
  - „Prikaži selektovani kontejner“ — klik → `toolStripButton1_Click`
  - „Prikaži sve kontejnere“ — klik → `toolStripButton6_Click`
  - `this` — otvaranje forme → `frmIzvozUnosManipulacije_Load` — čita: GrupaVrsteManipulacije

#### frmKontaktOsobeMU
- **Fajl:** `Izvoz/frmKontaktOsobeMU.cs`  
- **Naslov forme:** „Kontakt osobe na mestu utovara“
- **Modul:** Intermodalni terminal, Prijem i otprema vozova
- **Tabele:**
  - `MestaUtovara` — S
  - `partnerjiKontOsebaMU` — S
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click`
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - `this` — otvaranje forme → `frmKontaktOsobeMU_Load` — čita: MestaUtovara

#### frmKontejneriNaPrijemnici
- **Fajl:** `Dokumenta/frmKontejneriNaPrijemnici.cs`  
- **Naslov forme:** „Kontejner na prijemnicama“
- **Modul:** Intermodalni terminal, Kapija, Operacije pretovari, Prijem i otprema vozova
- **Tabele:**
  - `PrijemKontejneraVozStavke` — S

#### frmKontejnerTekuce
- **Fajl:** `Uvoz/frmKontejnerTekuce.cs`  
- **Naslov forme:** „Tekuci status kontejnera“
- **Modul:** Intermodalni terminal, Kapija, Operacije pretovari, Operacije skladišta, Prijem i otprema vozova
- **Tabele:**
  - `KontejnerskiTerminali` — S
  - `KontejnerStatus` — S
  - `KontejnerTekuce` — S
  - `PArtnerji` — S
  - `Pozicija` — S
  - `Skladista` — S
- **Funkcionalnosti (kontrola → handler):**
  - „TEKUCE STANJE - REFRESH“ — klik → `button1_Click` — čita: KontejnerStatus, KontejnerTekuce, KontejnerskiTerminali, PArtnerji, Pozicija, Skladista; izvoz u Excel
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - „Refresh“ — klik → `button1_Click` — čita: KontejnerStatus, KontejnerTekuce, KontejnerskiTerminali, PArtnerji, Pozicija, Skladista; izvoz u Excel

#### frmLagerOperater
- **Fajl:** `Promet/frmLagerOperater.cs`  
- **Naslov forme:** „Lager operater“
- **Modul:** Intermodalni terminal, Kapija, Prijem i otprema vozova
- **Tabele:**
  - `Komitenti` — S
  - `NHM` — S
  - `Partnerji` — S
  - `Pozicija` — S
  - `pozicija` — S
  - `PrijemKontejneraVozStavke` — S
  - `Promet` — S
  - `Skladista` — S
  - `skladista` — S
  - `TipKontenjera` — S
  - `VrstaRobe` — S
- **Funkcionalnosti (kontrola → handler):**
  - „?“ — klik → `button3_Click` — čita: Pozicija, PrijemKontejneraVozStavke, Promet, Skladista, pozicija, skladista
  - „?“ — klik → `button2_Click` — čita: Pozicija, PrijemKontejneraVozStavke, Promet, Skladista, pozicija, skladista
  - „?“ — klik → `button1_Click` — čita: Pozicija, PrijemKontejneraVozStavke, Promet, Skladista, pozicija, skladista
  - „Pretraži“ — klik → `btnPregled_Click` — čita: Pozicija, Promet, Skladista, pozicija, skladista
  - `this` — otvaranje forme → `frmLagerOperater_Load` — čita: Komitenti, Skladista, TipKontenjera, VrstaRobe

#### frmManipulacije
- **Fajl:** `Dokumenta/frmManipulacije.cs`  
- **Naslov forme:** „Usluge“
- **Modul:** Intermodalni terminal, Kapija, Prijem i otprema vozova
- **Otvara forme:** frmNajavaKomitemtaDokument („Nalog komitenta“), frmNajavaKomitemtaDokument („Nalog komitenta“), frmNajavaKomitemtaDokument („Nalog komitenta“), frmNajavaKomitemtaDokument („Nalog komitenta“)
- **Tabele:**
  - `Komitenti` — S
  - `NaruceneManipulacije` — S
  - `OtpremaKontejnera` — S
  - `OtpremaKontejneraVozStavke` — S
  - `Partnerji` — S
  - `Pozicija` — S
  - `PrijemKontejneraVoz` — S
  - `PrijemKontejneraVozStavke` — S
  - `Promet` — S
  - `Skladista` — S
  - `Voz` — S
  - `VrstaManipulacije` — S
- **Procedure:** `DeleteNaruceneManipulacije`, `InsertNaruceneManipulacije`, `UpdateNijeUradjenoNaruceneManipulacije`, `UpdateUradjenoNaruceneManipulacije`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „POTVRDI URADJENA USLUGA SA PROMETOM“ — klik → `toolStripButton2_Click`
  - „Nalog komitenta “ — klik → `toolStripButton1_Click` — otvara: frmNajavaKomitemtaDokument
  - „POTVRDI URADJENA USLUGA BEZ PROMETA“ — klik → `toolStripButton3_Click`
  - „>>“ — klik → `btnUnesi_Click` — proc: DeleteNaruceneManipulacije, InsertNaruceneManipulacije, UpdateNijeUradjenoNaruceneManipulacije, UpdateUradjenoNaruceneManipulacije; preko: InsertNaruceneManipulacije
  - „Period od:“ — klik → `label21_Click`
  - „Pretraži“ — klik → `btnPretrazi_Click` — čita: OtpremaKontejneraVozStavke, PrijemKontejneraVozStavke
  - „Pretraži“ — klik → `button1_Click` — čita: OtpremaKontejneraVozStavke, PrijemKontejneraVozStavke
  - „Platilac:“ — klik → `label8_Click`
  - „Izbriši manipulaciju“ — klik → `button2_Click` — proc: DeleteNaruceneManipulacije, InsertNaruceneManipulacije, UpdateNijeUradjenoNaruceneManipulacije, UpdateUradjenoNaruceneManipulacije; preko: InsertNaruceneManipulacije
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

#### frmNajavaKomitemtaDokument
- **Fajl:** `Dokumenta/frmNajavaKomitemtaDokument.cs`  
- **Naslov forme:** „Najava Komitenta Dokument“
- **Modul:** Intermodalni terminal, Kapija, Prijem i otprema vozova
- **Tabele:**
  - `DokumentaNajavaKomitenta` — S
- **Procedure:** `DeleteDokumentaOtpremaKontejneraVoz`, `InsertDokumentaNajavaKomitenta`
- **Funkcionalnosti (kontrola → handler):**
  - „Sačuvaj“ — klik → `button4_Click` — proc: DeleteDokumentaOtpremaKontejneraVoz, InsertDokumentaNajavaKomitenta; preko: InsertDokumentaNajava
  - „Otvori“ — klik → `button2_Click`
  - „Pronađi“ — klik → `button1_Click`

#### frmNalogZaPrevoz
- **Fajl:** `Dokumenta/frmNalogZaPrevoz.cs`  
- **Naslov forme:** „Nalog za prevoz“
- **Modul:** Intermodalni terminal, Kapija, Prijem i otprema vozova
- **Otvara forme:** frmPutniNalog („Otvori putni nalog“), frmPutniNalog („Otvori putni nalog“), frmKontaktOsobe („?“), frmPutniNalog („Otvori putni nalog“), frmPutniNalog („Otvori putni nalog“)
- **Tabele:**
  - `Cene` — S
  - `Komitenti` — S
  - `MestaUtovara` — S
  - `NajavaPrevoza` — S
  - `NajavaPrevozaTroskovi` — S
  - `OrganizacioneJedinice` — S
  - `Partnerji` — S
  - `VrstaManipulacije` — S
  - `VrstaRobe` — S
  - `VrstaRobeADR` — S
- **Procedure:** `DeleteNajavaPrevozaTroskovi`, `DeleteNalogZaPrevoz`, `InsertNaloziZaPrevoz`, `InsertNaloziZaPrevozTrosak`, `UpdateNaloziZaPrevozTrosak`, `UpdateNaloziZaprevoz`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: DeleteNajavaPrevozaTroskovi, DeleteNalogZaPrevoz, InsertNaloziZaPrevoz, InsertNaloziZaPrevozTrosak, UpdateNaloziZaPrevozTrosak, UpdateNaloziZaprevoz; preko: InsertNalogZaPrevoz
  - „Štampa Nalog za prevoz“ — klik → `toolStripLabel1_Click` — izveštaj: rptNalogZaPrevoz.rdlc, rptNalogZaPrevozTA.rdlc
  - „Otvori putni nalog“ — klik → `toolStripLabel2_Click` — otvara: frmPutniNalog
  - „>>“ — klik → `btnUnesi_Click` — proc: DeleteNajavaPrevozaTroskovi, DeleteNalogZaPrevoz, InsertNaloziZaPrevoz, InsertNaloziZaPrevozTrosak, UpdateNaloziZaPrevozTrosak, UpdateNaloziZaprevoz; preko: InsertNalogZaPrevoz
  - „?“ — klik → `button1_Click` — proc: DeleteNajavaPrevozaTroskovi, DeleteNalogZaPrevoz, InsertNaloziZaPrevoz, InsertNaloziZaPrevozTrosak, UpdateNaloziZaPrevozTrosak, UpdateNaloziZaprevoz; preko: InsertNalogZaPrevoz
  - „Dokument“ — klik → `tabPage1_Click`
  - „?“ — klik → `button3_Click`
  - „Briši stavku“ — klik → `button2_Click` — proc: DeleteNajavaPrevozaTroskovi, DeleteNalogZaPrevoz, InsertNaloziZaPrevoz, InsertNaloziZaPrevozTrosak, UpdateNaloziZaPrevozTrosak, UpdateNaloziZaprevoz; preko: InsertNalogZaPrevoz
  - „>>“ — klik → `button4_Click`
  - `this` — otvaranje forme → `frmNalogZaPrevoz_Load` — čita: MestaUtovara, OrganizacioneJedinice, Partnerji, VrstaManipulacije, VrstaRobeADR

#### frmOtpremaKontejnera
- **Fajl:** `Dokumenta/frmOtpremaKontejnera.cs`  
- **Naslov forme:** „Otprema kontejnera“
- **Modul:** Intermodalni terminal, Kapija, Prijem i otprema vozova
- **Otvara forme:** frmDodatniList („Štampa dodatni list“), frmDokumentaOtpremaKontejnera („Dokumenta otpreme“), frmManipulacije („Raspored manipulacija“), frmNalogZaPrevoz („Nalog Za Prevoz“), frmDodatniList („Štampa dodatni list“), frmManipulacije („Raspored usluga“), frmNalogZaPrevoz („Nalog Za Prevoz“), frmPregledNarucenihManipulacija („Usluge pregled“), frmTovarniList („Tovarni list“), frmDodatniList („Štampa dodatni list“), frmManipulacije („Raspored usluga“), frmNalogZaPrevoz („Nalog Za Prevoz“), frmPregledNarucenihManipulacija („Usluge pregled“), frmTovarniList („Tovarni list“), frmDodatniList („Štampa dodatni list“), frmManipulacije („Raspored usluga“), frmNalogZaPrevoz („Nalog Za Prevoz“), frmPregledNarucenihManipulacija („Usluge pregled“), frmTovarniList („Tovarni list“)
- **Tabele:**
  - `Komitenti` — S
  - `NHM` — S
  - `OtpremaKontejnera` — S
  - `OtpremaKontejneraVozStavke` — S
  - `Partnerji` — S
  - `PArtnerji` — S
  - `PostupakStavkeOtpremaVeza` — S
  - `PredefinisanePoruke` — S
  - `Promet` — S
  - `Stanice` — S
  - `StatusRobe` — S
  - `TipKontenjera` — S
  - `Voz` — S
  - `VrstaRobe` — S
  - `VrstePostupakaUvoz` — S
- **Procedure:** `DeleteOtpremaKontejnera`, `DeleteOtpremaKontejneraVozStavke`, `DeletePostupakStavke`, `DeletePostupakStavkeOtprema`, `DeletePrijemKontejneraVoz`, `DeletePrijemKontejneraVozStavke`, `InsertOtpremaKontejnera`, `InsertOtpremaKontejneraVozStavke`, `InsertPostupakStavke`, `InsertPostupakStavkeOtprema`, `InsertPrijemKontejneraVoz`, `InsertPrijemKontejneraVozStavke`, `PrekopirajStavke`, `PromeniBrojVagona`, `UpdateEmailOtpremaNajava`, `UpdateEmailOtpremaOtprema`, `UpdateEmailPrijemNajava`, `UpdateEmailPrijemPrijem`, `UpdateOtpremaKontVozStavkeVagon`, `UpdateOtpremaKontejnera`, `UpdateOtpremaKontejneraVozStavke`, `UpdateOtpremaKontejneraVozStavkeRB`, `UpdatePrijemKontejneraVoz`, `UpdatePrijemKontejneraVozStavke`, `UpdatePrijemKontejneraVozStavkeRB`, `UpdateZatvorenOtpremaCheck`
- **Funkcionalnosti (kontrola → handler):**
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
  - „Obriši“ — klik → `btnObrisiPostupak_Click` — proc: DeletePostupakStavke, DeletePostupakStavkeOtprema, DeletePrijemKontejneraVoz, InsertPostupakStavke, InsertPostupakStavkeOtprema, InsertPrijemKontejneraVoz; preko: InsertPrijemKontejneraVoz
  - „Dodaj“ — klik → `btnDodajPostupak_Click` — proc: DeletePostupakStavke, DeletePostupakStavkeOtprema, DeletePrijemKontejneraVoz, InsertPostupakStavke, InsertPostupakStavkeOtprema, InsertPrijemKontejneraVoz; preko: InsertPrijemKontejneraVoz
  - `this` — otvaranje forme → `frmOtpremaKontejnera_Load` — čita: Partnerji, PredefinisanePoruke, StatusRobe, Voz, VrstePostupakaUvoz

#### frmOtpremaKontejneraIzvozKamion
- **Fajl:** `Dokumenta/frmOtpremaKontejneraIzvozKamion.cs`  
- **Naslov forme:** „GATE OUT KAMION“
- **Modul:** Intermodalni terminal, Kapija, Prijem i otprema vozova
- **Otvara forme:** frmDokumentaOtpremaKontejnera („Dokumenta otpreme“), frmDokumentaOtpremaKontejnera („Dokumenta otpreme“), frmDokumentaOtpremaKontejnera („Dokumenta otpreme“)
- **Tabele:**
  - `InspekciskiTretman` — S
  - `izvozKOnacna` — S
  - `IzvozKonacna` — S
  - `IzvozKonacnaVrstaManipulacije` — S
  - `Komitenti` — S
  - `OrganizacioneJedinice` — S
  - `OtpremaKontejnera` — S
  - `OtpremaKontejneraVozStavke` — S
  - `Partnerji` — S
  - `PArtnerji` — S
  - `Promet` — S
  - `RadniNalogInterni` — S
  - `RnOtpremaCirade` — S
  - `RNPrijemVoza` — S
  - `Skladista` — S
  - `TipKontenjera` — S
  - `Voz` — S
  - `VrstaCarinskogPostupka` — S
  - `VrstaManipulacije` — S
  - `VrstaRobe` — S
  - `VrstaRobeADR` — S
- **Procedure:** `DeleteOtpremaKontejnera`, `DeleteOtpremaKontejneraVozStavke`, `InsertOtpremaKontejnera`, `InsertOtpremaKontejneraVozStavke`, `PrekopirajStavke`, `PromeniBrojVagona`, `UpdateEmailOtpremaNajava`, `UpdateEmailOtpremaOtprema`, `UpdateOtpremaKontVozStavkeVagon`, `UpdateOtpremaKontejnera`, `UpdateOtpremaKontejneraVozStavke`, `UpdateZatvorenOtpremaCheck`
- **Funkcionalnosti (kontrola → handler):**
  - „Manipulacije kontejnerom“ — ItemClicked → `btnManipulacija_ItemClicked`
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: DeleteOtpremaKontejnera, InsertOtpremaKontejnera, UpdateEmailOtpremaNajava, UpdateEmailOtpremaOtprema, UpdateOtpremaKontejnera, UpdateZatvorenOtpremaCheck; preko: InsertOtprema
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteOtpremaKontejnera, InsertOtpremaKontejnera, UpdateEmailOtpremaNajava, UpdateEmailOtpremaOtprema, UpdateOtpremaKontejnera, UpdateZatvorenOtpremaCheck; preko: InsertOtprema
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
  - „Izbriši“ — klik → `button3_Click` — proc: DeleteOtpremaKontejneraVozStavke, InsertOtpremaKontejneraVozStavke, PrekopirajStavke, PromeniBrojVagona, UpdateOtpremaKontVozStavkeVagon, UpdateOtpremaKontejneraVozStavke; preko: InsertOtpremaKontejneraStavke
  - „Promeni“ — klik → `button2_Click` — proc: DeleteOtpremaKontejneraVozStavke, InsertOtpremaKontejneraVozStavke, PrekopirajStavke, PromeniBrojVagona, UpdateOtpremaKontVozStavkeVagon, UpdateOtpremaKontejneraVozStavke; preko: InsertOtpremaKontejneraStavke
  - „Ubaci“ — klik → `button1_Click` — čita: OtpremaKontejneraVozStavke, Promet; proc: DeleteOtpremaKontejneraVozStavke, InsertOtpremaKontejneraVozStavke, PrekopirajStavke, PromeniBrojVagona, UpdateOtpremaKontVozStavkeVagon, UpdateOtpremaKontejneraVozStavke; preko: InsertOtpremaKontejneraStavke
  - „Radni nalog Otprema“ — klik → `toolStripButton8_Click`
  - „Radni nalozi BRODAR“ — klik → `toolStripButton6_Click`
  - „Dokumenta otpreme“ — klik → `toolStripButton2_Click` — otvara: frmDokumentaOtpremaKontejnera
  - `button20` — klik → `tsDelete_Click` — proc: DeleteOtpremaKontejnera, InsertOtpremaKontejnera, UpdateEmailOtpremaNajava, UpdateEmailOtpremaOtprema, UpdateOtpremaKontejnera, UpdateZatvorenOtpremaCheck; preko: InsertOtprema
  - `button21` — klik → `tsSave_Click` — proc: DeleteOtpremaKontejnera, InsertOtpremaKontejnera, UpdateEmailOtpremaNajava, UpdateEmailOtpremaOtprema, UpdateOtpremaKontejnera, UpdateZatvorenOtpremaCheck; preko: InsertOtprema

#### frmOtpremaKontejneraKamionomIzKontejnera
- **Fajl:** `Izvoz/frmOtpremaKontejneraKamionomIzKontejnera.cs`  
- **Naslov forme:** „Otprema kontejnera kamionom - FORMIRANJE“
- **Modul:** Intermodalni terminal, Kapija, Prijem i otprema vozova
- **Otvara forme:** frmOtpremaKontejnera („Otvori“), frmOtpremaKontejneraIzvozKamion („Otvori“), frmOtpremaKontejneraUvozKamion („Otvori“), frmOtpremaKontejnera („Otvori“), frmOtpremaKontejneraIzvozKamion („Otvori“), frmOtpremaKontejneraUvozKamion („Otvori“), RN6OtpremaPlatforme (`button21` (Click)), RN7OtpremaPlatforme2 (metoda `PrenesiRN7`), frmOtpremaKontejnera („Otvori“), frmOtpremaKontejneraIzvozKamion („Otvori“), frmOtpremaKontejneraUvozKamion („Otvori“)
- **Tabele:**
  - `IzvozKonacna` — S
  - `OtpremaKontejnera` — S
  - `RadniNalogInterni` — S
  - `UvozKOnacna` — S
  - `VoziloUsluga` — S
- **Procedure:** `DeleteIzvozKonacnaNapomenePozicioniranja`, `DeleteIzvozKonacnaSve`, `DeleteIzvozNapomenePozicioniranja`, `DeleteIzvozUslugaDokumenta`, `DeleteIzvozVrstaManTerminalskih`, `InsertIzvozKonacnaNapomenePozicioniranja`, `InsertIzvozNapomenePozicioniranja`, `InsertIzvozUslugaDokument`, `OdrediKontejnerTerminal`, `PrenesiIzPlanIzvozaUPlanIzvoza`, `PrenesiUPlanUtovaraSelektovanoIzvoz`, `PrenesiUPlanUtovaraSelektovanoIzvozBrodar`, `PrenesiUPlanUtovaraSelektovanoIzvozTErminal`, `UpdateIzvozKonacnaVagon`, `VratiKontejnerNerasporedjenoSelektovanoIzvoz`, `spPrenesiIzPlanUtovaraUOtpremaVoz`, `spPrenesiIzUvozaUIzvoz`, `spPrenesiKontejnerUOtpremuKamionom`, `spPrenesiKontejnerUOtpremuKamionomIzvoz`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: DeleteIzvozKonacnaNapomenePozicioniranja, DeleteIzvozKonacnaSve, DeleteIzvozNapomenePozicioniranja, DeleteIzvozUslugaDokumenta, DeleteIzvozVrstaManTerminalskih, InsertIzvozKonacnaNapomenePozicioniranja; preko: InsertIzvozKonacna
  - „OTVORI “ — klik → `toolStripButton1_Click`
  - „Otvori“ — klik → `toolStripButton1_Click`
  - `button21` — klik → `tsSave_Click` — proc: DeleteIzvozKonacnaNapomenePozicioniranja, DeleteIzvozKonacnaSve, DeleteIzvozNapomenePozicioniranja, DeleteIzvozUslugaDokumenta, DeleteIzvozVrstaManTerminalskih, InsertIzvozKonacnaNapomenePozicioniranja; preko: InsertIzvozKonacna

#### frmOtpremaKontejneraLegetIZVOZ
- **Fajl:** `Dokumenta/frmOtpremaKontejneraLegetIZVOZ.cs`  
- **Naslov forme:** „GATE OUT VOZ - IZVOZ“
- **Modul:** Intermodalni terminal, Prijem i otprema vozova
- **Tabele:**
  - `Carinarnice` — S
  - `DirigacijaKontejneraZa` — S
  - `InspekciskiTretman` — S
  - `IzvozKonacna` — S
  - `IzvozKonacnaNapomenePozicioniranja` — S
  - `IzvozKonacnaNHM` — S
  - `IzvozKonacnaVrstaManipulacije` — S
  - `Komitenti` — S
  - `KontejnerskiTerminali` — S
  - `KrajnjaDestinacija` — S
  - `NHM` — S
  - `OrganizacioneJedinice` — S
  - `OtpremaKontejnera` — S
  - `OtpremaKontejneraVozStavke` — S
  - `Partnerji` — S
  - `PArtnerji` — S
  - `PredefinisanePoruke` — S
  - `RNOtpremaVoza` — S
  - `Skladista` — S
  - `TipKontenjera` — S
  - `Voz` — S
  - `VrstaCarinskogPostupka` — S
  - `VrstaManipulacije` — S
  - `VrstaRobe` — S
  - `VrstaRobeADR` — S
  - `VrstePostupakaUvoz` — S
- **Procedure:** `DeleteOtpremaKontejnera`, `DeleteOtpremaKontejneraVozStavke`, `InsertOtpremaKontejnera`, `InsertOtpremaKontejneraVozStavke`, `PrekopirajStavke`, `PromeniBrojVagona`, `UpdateEmailOtpremaNajava`, `UpdateEmailOtpremaOtprema`, `UpdateOtpremaKontVozStavkeVagon`, `UpdateOtpremaKontejnera`, `UpdateOtpremaKontejneraVozStavke`, `UpdateZatvorenOtpremaCheck`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: DeleteOtpremaKontejnera, InsertOtpremaKontejnera, UpdateEmailOtpremaNajava, UpdateEmailOtpremaOtprema, UpdateOtpremaKontejnera, UpdateZatvorenOtpremaCheck; preko: InsertOtprema
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteOtpremaKontejnera, InsertOtpremaKontejnera, UpdateEmailOtpremaNajava, UpdateEmailOtpremaOtprema, UpdateOtpremaKontejnera, UpdateZatvorenOtpremaCheck; preko: InsertOtprema
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
  - „Izbriši“ — klik → `button3_Click` — proc: DeleteOtpremaKontejneraVozStavke, InsertOtpremaKontejneraVozStavke, PrekopirajStavke, PromeniBrojVagona, UpdateOtpremaKontVozStavkeVagon, UpdateOtpremaKontejneraVozStavke; preko: InsertOtpremaKontejneraStavke
  - „Promeni“ — klik → `button2_Click` — proc: DeleteOtpremaKontejneraVozStavke, InsertOtpremaKontejneraVozStavke, PrekopirajStavke, PromeniBrojVagona, UpdateOtpremaKontVozStavkeVagon, UpdateOtpremaKontejneraVozStavke; preko: InsertOtpremaKontejneraStavke
  - „Ubaci“ — klik → `button1_Click` — proc: DeleteOtpremaKontejneraVozStavke, InsertOtpremaKontejneraVozStavke, PrekopirajStavke, PromeniBrojVagona, UpdateOtpremaKontVozStavkeVagon, UpdateOtpremaKontejneraVozStavke; preko: InsertOtpremaKontejneraStavke
  - „Poveži vagone“ — klik → `toolStripButton6_Click_1` — otvara: frmPovezivanjeKontejneraIVagona
  - „Štampa dodatni list“ — klik → `toolStripLabel1_Click`
  - „Radni nalozi “ — klik → `toolStripDropDownButton1_Click`
  - „Dokumenta otpreme“ — klik → `toolStripButton2_Click` — otvara: frmDokumentaOtpremaKontejnera
  - `button20` — klik → `tsDelete_Click` — proc: DeleteOtpremaKontejnera, InsertOtpremaKontejnera, UpdateEmailOtpremaNajava, UpdateEmailOtpremaOtprema, UpdateOtpremaKontejnera, UpdateZatvorenOtpremaCheck; preko: InsertOtprema
  - `button21` — klik → `tsSave_Click` — proc: DeleteOtpremaKontejnera, InsertOtpremaKontejnera, UpdateEmailOtpremaNajava, UpdateEmailOtpremaOtprema, UpdateOtpremaKontejnera, UpdateZatvorenOtpremaCheck; preko: InsertOtprema
  - `this` — otvaranje forme → `frmOtpremaKontejneraLegetIZVOZ_Load` — čita 9 tabela

#### frmOtpremaKontejneraUvozKamion
- **Fajl:** `Dokumenta/frmOtpremaKontejneraUvozKamion.cs`  
- **Naslov forme:** „GATE OUT KAMION - UVOZ“
- **Modul:** Intermodalni terminal, Kapija, Prijem i otprema vozova
- **Otvara forme:** frmLagerOperater („MAPA SKLADIŠTA“), frmCIR („CIR“), frmLagerOperater („MAPA SKLADIŠTA“), frmCIR („CIR“), frmLagerOperater („MAPA SKLADIŠTA“)
- **Tabele:**
  - `DirigacijaKontejneraZa` — S
  - `Komitenti` — S
  - `KontejnerStatus` — S
  - `NHM` — S
  - `OrganizacioneJedinice` — S
  - `OtpremaKontejnera` — S
  - `OtpremaKontejneraVozStavke` — S
  - `Partnerji` — S
  - `PArtnerji` — S
  - `PredefinisanePoruke` — S
  - `PrijemKontejneraVoz` — S
  - `PrijemKontejneraVozStavke` — S
  - `RadniNalogInterni` — S
  - `RAdniNalogInterni` — S
  - `RNOtpremaCirade` — S
  - `RNOtpremaPlatforme` — S
  - `Stanice` — S
  - `TipKontenjera` — S
  - `uvNacinPakovanja` — S
  - `UvozKonacna` — S
  - `UvozKonacnaNapomenePozicioniranja` — S
  - `UvozKonacnaNHM` — S
  - `UvozKonacnaVrstaManipulacije` — S
  - `UvozKonacnaZaglavlje` — S
  - `Voz` — S
  - `VrstaManipulacije` — S
  - `VrstaRobe` — S
- **Funkcionalnosti (kontrola → handler):**
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

#### frmOtpremaVozaIzPlana
- **Fajl:** `Izvoz/frmOtpremaVozaIzPlana.cs`  
- **Naslov forme:** „Formiranje otpreme voza iz plana“
- **Modul:** Intermodalni terminal, Prijem i otprema vozova
- **Otvara forme:** frmOtpremaKontejneraLegetIZVOZ („Otvori“), frmOtpremaKontejneraLegetIZVOZ („Otvori“)
- **Tabele:**
  - `IzvozKonacna` — S
  - `IzvozKonacnaZaglavlje` — S
  - `OtpremaKontejnera` — S
  - `OtpremaKontejneraVozStavke` — S
  - `Partnerji` — S
  - `RadniNalogInterni` — S
  - `TipKontenjera` — S
  - `UvozKOnacna` — S
  - `Voz` — S
  - `VrstaRobe` — S
- **Procedure:** `DeleteIzvozKonacnaNapomenePozicioniranja`, `DeleteIzvozKonacnaSve`, `DeleteIzvozNapomenePozicioniranja`, `DeleteIzvozUslugaDokumenta`, `DeleteIzvozVrstaManTerminalskih`, `InsertIzvozKonacnaNapomenePozicioniranja`, `InsertIzvozNapomenePozicioniranja`, `InsertIzvozUslugaDokument`, `OdrediKontejnerTerminal`, `PrenesiIzPlanIzvozaUPlanIzvoza`, `PrenesiUPlanUtovaraSelektovanoIzvoz`, `PrenesiUPlanUtovaraSelektovanoIzvozBrodar`, `PrenesiUPlanUtovaraSelektovanoIzvozTErminal`, `UpdateIzvozKonacnaVagon`, `VratiKontejnerNerasporedjenoSelektovanoIzvoz`, `spPrenesiIzPlanUtovaraUOtpremaVoz`, `spPrenesiIzUvozaUIzvoz`, `spPrenesiKontejnerUOtpremuKamionom`, `spPrenesiKontejnerUOtpremuKamionomIzvoz`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click`
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „OTVORI VOZ“ — klik → `toolStripButton1_Click`
  - „Voz iz plana“ — klik → `button2_Click`
  - „Prenesi stavke iz plana“ — klik → `button1_Click` — proc: DeleteIzvozKonacnaNapomenePozicioniranja, DeleteIzvozKonacnaSve, DeleteIzvozNapomenePozicioniranja, DeleteIzvozUslugaDokumenta, DeleteIzvozVrstaManTerminalskih, InsertIzvozKonacnaNapomenePozicioniranja; preko: InsertIzvozKonacna
  - „Otvori“ — klik → `toolStripButton1_Click`
  - `this` — otvaranje forme → `frmOtpremaVozaIzPlana_Load` — čita: IzvozKonacnaZaglavlje, Partnerji, Voz

#### frmPovezivanjeKontejneraIVagona
- **Fajl:** `Dokumenta/frmPovezivanjeKontejneraIVagona.cs`  
- **Naslov forme:** „PAKOVANJE VOZA“
- **Modul:** Intermodalni terminal, Kapija, Prijem i otprema vozova
- **Tabele:**
  - `IzvozKonacna` — S
  - `OtpremaKontejneraVozStavke` — S
  - `Partnerji` — S
  - `PrijemKontejneraVoz` — S
  - `PrijemKontejneraVozStavke` — S
  - `TipKontenjera` — S
  - `Voz` — S
  - `VrstaRobe` — S
- **Procedure:** `DeleteIzvozKonacnaNapomenePozicioniranja`, `DeleteIzvozKonacnaSve`, `DeleteIzvozNapomenePozicioniranja`, `DeleteIzvozUslugaDokumenta`, `DeleteIzvozVrstaManTerminalskih`, `DeleteOtpremaKontejneraVozStavke`, `InsertIzvozKonacnaNapomenePozicioniranja`, `InsertIzvozNapomenePozicioniranja`, `InsertIzvozUslugaDokument`, `InsertOtpremaKontejneraVozStavke`, `OdrediKontejnerTerminal`, `PrekopirajStavke`, `PrenesiIzPlanIzvozaUPlanIzvoza`, `PrenesiUPlanUtovaraSelektovanoIzvoz`, `PrenesiUPlanUtovaraSelektovanoIzvozBrodar`, `PrenesiUPlanUtovaraSelektovanoIzvozTErminal`, `PromeniBrojVagona`, `UpdateIzvozKonacnaVagon`, `UpdateOtpremaKontVozStavkeVagon`, `UpdateOtpremaKontejneraVozStavke`, `VratiKontejnerNerasporedjenoSelektovanoIzvoz`, `spPrenesiIzPlanUtovaraUOtpremaVoz`, `spPrenesiIzUvozaUIzvoz`, `spPrenesiKontejnerUOtpremuKamionom`, `spPrenesiKontejnerUOtpremuKamionomIzvoz`
- **Funkcionalnosti (kontrola → handler):**
  - „Sačuvaj“ — klik → `button1_Click` — proc: DeleteIzvozKonacnaNapomenePozicioniranja, DeleteIzvozKonacnaSve, DeleteIzvozNapomenePozicioniranja, DeleteIzvozUslugaDokumenta, DeleteIzvozVrstaManTerminalskih, DeleteOtpremaKontejneraVozStavke; preko: InsertIzvozKonacna, InsertOtpremaKontejneraStavke
  - „?“ — klik → `button5_Click` — čita: PrijemKontejneraVoz, PrijemKontejneraVozStavke
  - „PRIKAŽI ISTORISKI PODACI“ — čekiranje → `chkPostojeci_CheckedChanged`
  - `this` — otvaranje forme → `frmPovezivanjeKontejneraIVagona_Load` — čita: Voz

#### frmPregledNarucenihManipulacija
- **Fajl:** `Dokumenta/frmPregledNarucenihManipulacija.cs`  
- **Naslov forme:** „Naručene usluga po prevozu“
- **Modul:** Intermodalni terminal, Kapija, Prijem i otprema vozova
- **Tabele:**
  - `Komitenti` — S
  - `NaruceneManipulacije` — S
  - `OtpremaKontejnera` — S
  - `Partnerji` — S
  - `PrijemKontejneraVoz` — S
  - `Voz` — S
  - `VrstaManipulacije` — S
- **Procedure:** `DeleteNaruceneManipulacije`, `InsertNaruceneManipulacije`, `UpdateNijeUradjenoNaruceneManipulacije`, `UpdateUradjenoNaruceneManipulacije`
- **Funkcionalnosti (kontrola → handler):**
  - „Pretraži“ — klik → `button1_Click`
  - „Pretraži“ — klik → `btnPretrazi_Click`
  - „Izbriši manipulaciju“ — klik → `button2_Click` — proc: DeleteNaruceneManipulacije, InsertNaruceneManipulacije, UpdateNijeUradjenoNaruceneManipulacije, UpdateUradjenoNaruceneManipulacije; preko: InsertNaruceneManipulacije
  - „Obeleži urađeno“ — klik → `button3_Click` — proc: DeleteNaruceneManipulacije, InsertNaruceneManipulacije, UpdateNijeUradjenoNaruceneManipulacije, UpdateUradjenoNaruceneManipulacije; preko: InsertNaruceneManipulacije
  - „Obeleži nije urađeno“ — klik → `button4_Click` — proc: DeleteNaruceneManipulacije, InsertNaruceneManipulacije, UpdateNijeUradjenoNaruceneManipulacije, UpdateUradjenoNaruceneManipulacije; preko: InsertNaruceneManipulacije
  - „Prijem“ — čekiranje → `chkPrijem_CheckedChanged`
  - „Otprema“ — čekiranje → `chkOtprema_CheckedChanged`
  - „Pretraži po kontejneru“ — klik → `button5_Click`
  - „Pretraži po kontejneru“ — klik → `button6_Click`
  - `this` — otvaranje forme → `frmPregledNarucenihManipulacija_Load` — čita: OtpremaKontejnera, PrijemKontejneraVoz, Voz

#### frmPregledSkladistaNovi
- **Fajl:** `RadniNalozi/frmPregledSkladistaNovi.cs`  
- **Naslov forme:** „VIZUELNI PREGLED SKLADISTA“
- **Modul:** Intermodalni terminal, Kapija, Operacije pretovari, Prijem i otprema kamiona, Prijem i otprema vozova
- **Tabele:**
  - `KontejnerTekuce` — S
  - `Skladista` — S
- **Funkcionalnosti (kontrola → handler):**
  - `this` — otvaranje forme → `frmPregledSkladistaNovi_Load` — čita: KontejnerTekuce, Skladista

#### frmPrijemKamionaIzPlana
- **Fajl:** `Uvoz/frmPrijemKamionaIzPlana.cs`  
- **Naslov forme:** „Prijem kamiona iz plana“
- **Modul:** Intermodalni terminal, Prijem i otprema vozova
- **Tabele:**
  - `DirigacijaKontejneraZa` — S
  - `Partnerji` — S
  - `PrijemKontejneraVoz` — S
  - `PrijemKontejneraVozStavke` — S
  - `TipKontenjera` — S
  - `UvozKonacna` — S
  - `Voz` — S
  - `VrstePostupakaUvoz` — S
- **Procedure:** `DeleteUvozKonacna`, `DeleteUvozKonacnaNHM`, `DeleteUvozKonacnaNapomenePozicioniranja`, `DeleteUvozKonacnaVrstaManipulacije`, `DeleteUvozKonacnaVrstaManipulacijeKamionska`, `DeleteUvozKonacnaVrstaRobeHS`, `DeleteUvozNHM`, `DeleteUvozNHMDiana`, `DeleteUvozNapomenePozicioniranja`, `DeleteUvozTerminalskeManipulacije`, `DeleteUvozTerminalskeManipulacijeKonacna`, `DeleteUvozUslugaDokumenta`, `DeleteUvozUvoznici`, `DeleteUvozVrstaManTerminalskih`, `DeleteUvozVrstaManipulacije`, `DeleteUvozVrstaManipulacijeKamionska`, `DeleteUvozVrstaRobeHS`, `DeleteUvozkonacnaNHMDiana`, `InsUbaciDodatnuUsluguIzvoz`, `InsUbaciDodatnuUsluguUvoz`, `InsertUvozKonacbaVrstaRobeHS`, `InsertUvozKonacna`, `InsertUvozKonacnaNHM`, `InsertUvozKonacnaNHMDiana`, `InsertUvozKonacnaNapomenePozicioniranja`, `InsertUvozKonacnaVrstaManipulacije`, `InsertUvozNHM`, `InsertUvozNHMDiana`, `InsertUvozNapomenePozicioniranja`, `InsertUvozUslugaDokument`, `InsertUvozUvoznici`, `InsertUvozVrstaManipulacije`, `InsertUvozVrstaRobeHS`, `PrebaciNHMUvozUvozKonacna`, `PrebaciVrsterobeHSUvozUvozKonacna`, `PrenesiIzPlanUtovaraUPlanUtovara`, `PrenesiUPlanUtovaraSelektovano`, `PromeniSaPrijemniceUK`, `UbaciURadniNalogInterniBeyScenarija`, `UpdCenaUvozKonacnaVrstaManipulacije`, `UpdCenaUvozVrstaManipulacije`, `UpdPlatiocaUvozKonacnaVrstaManipulacije`, `UpdPlatiocaUvozVrstaManipulacije`, `UpdPokretFormaStatus`, `UpdateBrojKolaIzExcela`, `UpdateBrojKolaIzExcelaVoz`, `UpdateUvozKonacna`, `UpdateUvozKonacnaManFaktura`, `UpdateUvozKonacnaManZaFakturisanje`, `UpdateUvozKonacnaObjedinjenja`, `UpdateUvozKonacnaPK`, `UpdateUvozKonacnaUC`, `UpdateUvozNHMTezine`, `VratiIzPlanaUtovaraSelektovano`, `ZavrsiDodatnuUsluguIzvozZadata`, `ZavrsiDodatnuUsluguUvozZadata`, `spIzbrisiKUScenario`, `spPrenesiKontejnerIzPlanaNaPrijemnicu`, `spPrenesiKontejnerIzPlanaNaPrijemnicuIzvoz`, `spPrenesiKontejnerIzPlanaNaPrijemnicuIzvozPoIdNadredjenog`, `spPrenesiPlanUtovaraUPrijemPlatforma`, `spPrenesiPlanUtovaraUPrijemPlatformaIzvoz`, `spPrenesiPlanUtovaraUPrijemVoz`, `spStornijaRNI`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click`
  - „Uvoz“ — čekiranje → `checkBox2_CheckedChanged`
  - „Prenesi kontejenr“ — klik → `button1_Click` — proc: DeleteUvozKonacna, DeleteUvozKonacnaNHM, DeleteUvozKonacnaNapomenePozicioniranja, DeleteUvozKonacnaVrstaManipulacije, DeleteUvozKonacnaVrstaManipulacijeKamionska, DeleteUvozKonacnaVrstaRobeHS; preko: InsertUvozKonacna

#### frmPrijemKontejneraKamionLegetIzvoz
- **Fajl:** `Dokumenta/frmPrijemKontejneraKamionLegetIzvoz.cs`  
- **Naslov forme:** „GATE IN KAMION - IZVOZ“
- **Modul:** Intermodalni terminal, Kapija, Prijem i otprema vozova
- **Tabele:**
  - `Carinarnice` — S
  - `DirigacijaKontejneraZa` — S
  - `InspekciskiTretman` — S
  - `izvozKOnacna` — S
  - `IzvozKonacna` — S
  - `IzvozKonacnaVrstaManipulacije` — S
  - `KontejnerskiTerminali` — S
  - `KontejnerStatus` — S
  - `OrganizacioneJedinice` — S
  - `Partnerji` — S
  - `PArtnerji` — S
  - `PredefinisanePoruke` — S
  - `PrijemKontejneraVoz` — S
  - `PrijemKontejneraVozStavke` — S
  - `RadniNalogInterni` — S
  - `RNPrijemCirade` — S
  - `RNPrijemPlatforme` — S
  - `RNPrijemPlatforme2` — S
  - `TipKontenjera` — S
  - `uvNacinPakovanja` — S
  - `UvozKonacna` — S
  - `Voz` — S
  - `VrstaCarinskogPostupka` — S
  - `VrstaManipulacije` — S
  - `VrstaRobeADR` — S
  - `VrstePostupakaUvoz` — S
- **Funkcionalnosti (kontrola → handler):**
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

#### frmPrijemKontejneraKamionLegetUvoz
- **Fajl:** `Dokumenta/frmPrijemKontejneraKamionLegetUvoz.cs`  
- **Naslov forme:** „GATE IN KAMION - UVOZ“
- **Modul:** Intermodalni terminal, Kapija, Operacije pretovari, Prijem i otprema vozova
- **Otvara forme:** frmDokumentaPrijemKontejneraKamion („Dokumenta prijema“), frmKontejneriNaPrijemnici („?“), frmDokumentaPrijemKontejneraKamion („Dokumenta prijema“), frmKontejneriNaPrijemnici („?“), frmDokumentaPrijemKontejneraKamion („Dokumenta prijema“), frmDokumentaPrijemKontejneraKamion („Dokumenta prijema“), frmKontejneriNaPrijemnici („?“)
- **Tabele:**
  - `DirigacijaKontejneraZa` — S
  - `Izvoz` — S
  - `IzvozKonacna` — S
  - `IzvozVrstaManipulacije` — S
  - `KontejnerskiTerminali` — S
  - `KontejnerStatus` — S
  - `NHM` — S
  - `OrganizacioneJedinice` — S
  - `Partnerji` — S
  - `PArtnerji` — S
  - `PredefinisanePoruke` — S
  - `PrijemKontejneraVoz` — S
  - `PrijemKontejneraVozStavke` — S
  - `Promet` — S
  - `RadniNalogInterni` — S
  - `RAdniNalogInterni` — S
  - `RNPrijemCirade` — S
  - `RNPrijemPlatforme` — S
  - `TipKontenjera` — S
  - `uvNacinPakovanja` — S
  - `Uvoz` — SI
  - `UvozKonacna` — S
  - `UvozKonacnaNHM` — S
  - `UvozKonacnaVrstaManipulacije` — S
  - `UvozKonacnaZaglavlje` — S
  - `Voz` — S
  - `VoziloUsluga` — S
  - `VrstaCarinskogPostupka` — S
  - `VrstaManipulacije` — S
  - `VrstaRobeADR` — S
  - `VrstePostupakaUvoz` — S
- **Procedure:** `DeletePromet`, `DeletePrometManipulacije`, `InsertPromet`, `InsertPrometManipulacije`, `UpdateVozila`, `UpdateZatvoren`, `UpdateZatvorenOtprema`, `UpdateZatvorenOtpremaPBRK`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click`
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „Prijem na operativno skladiste“ — klik → `toolStripButton7_Click` — proc: DeletePromet, DeletePrometManipulacije, InsertPromet, InsertPrometManipulacije, UpdateVozila, UpdateZatvoren; preko: InsertPromet
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

#### frmPrijemKontejneraLegetVozUvoz
- **Fajl:** `Dokumenta/frmPrijemKontejneraLegetVozUvoz.cs`  
- **Naslov forme:** „GATE IN VOZ“
- **Modul:** Intermodalni terminal, Kapija, Prijem i otprema vozova
- **Otvara forme:** frmDokumentaPrijemKontejneraVoz („Dokumenta prijema“), frmPovezivanjeKontejneraIVagona („Povezivanje vagona“), frmDodelaSkladista („Odredi pozicije“), frmDokumentaPrijemKontejneraVoz („Dokumenta prijema“), frmKontejneriNaPrijemnici („?“), frmKontejnerTekuce („Tekuce stanje“), frmPovezivanjeKontejneraIVagona („Povezivanje vagona“), frmDokumentaPrijemKontejneraVoz („Dokumenta prijema“), frmPovezivanjeKontejneraIVagona („Povezivanje vagona“)
- **Tabele:**
  - `DirigacijaKontejneraZa` — S
  - `InspekciskiTretman` — S
  - `KontejnerskiTerminali` — S
  - `KontejnerStatus` — S
  - `NHM` — S
  - `OrganizacioneJedinice` — S
  - `Partnerji` — S
  - `PArtnerji` — S
  - `PredefinisanePoruke` — S
  - `PrijemKontejneraVoz` — S
  - `PrijemKontejneraVozStavke` — S
  - `Promet` — S
  - `RadniNalogInterni` — S
  - `RNPrijemVoza` — S
  - `Skladista` — S
  - `TipKontenjera` — S
  - `UvozKonacna` — S
  - `UvozKonacnaNapomenePozicioniranja` — S
  - `UvozKonacnaNHM` — S
  - `UvozKonacnaVrstaManipulacije` — S
  - `Voz` — S
  - `VrstaCarinskogPostupka` — S
  - `VrstaManipulacije` — S
  - `VrstaRobeADR` — S
  - `VrstePostupakaUvoz` — S
- **Procedure:** `DeletePromet`, `DeletePrometManipulacije`, `InsertPromet`, `InsertPrometManipulacije`, `UpdateVozila`, `UpdateZatvoren`, `UpdateZatvorenOtprema`, `UpdateZatvorenOtpremaPBRK`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click`
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „DOKUMENTACIJA PRIJEMA“ — klik → `toolStripButton3_Click` — otvara: frmDokumentaPrijemKontejneraVoz
  - „RADNI NALOZI FORMIRANJE“ — klik → `toolStripDropDownButton1_Click`
  - „RN - Prijem voza“ — klik → `formirajRadneNalogePrijemVozaToolStripMenuItem_Click`
  - „ODREDI POZICIJE“ — klik → `toolStripButton10_Click` — otvara: frmDodelaSkladista
  - „Prijem na OP S“ — klik → `toolStripButton7_Click` — proc: DeletePromet, DeletePrometManipulacije, InsertPromet, InsertPrometManipulacije, UpdateVozila, UpdateZatvoren; preko: InsertPromet
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

#### frmPrijemVozaIzPlana
- **Fajl:** `Uvoz/frmPrijemVozaIzPlana.cs`  
- **Naslov forme:** „Formiranje prijema iz plana utovara“
- **Modul:** Intermodalni terminal, Kapija, Prijem i otprema vozova
- **Otvara forme:** frmPrijemKontejneraKamionLegetIzvoz („OTVORI - GATE IN KAMION IZVOZ“), frmPrijemKontejneraLegetVozUvoz („OTVORI - GATE IN VOZ“), frmPrijemKontejneraKamionLegetIzvoz („OTVORI - GATE IN KAMION IZVOZ“), frmPrijemKontejneraKamionLegetUvoz („OTVORI - GATE IN KAMION UVOZ“), frmPrijemKontejneraLegetVozUvoz („OTVORI - GATE IN VOZ“), RN1PrijemVoza (metoda `PrenesiStavkeIRN`), RN4PrijemPlatforme (metoda `PrenesiStavkeIRN`), RN5PrijemPlatforme2 (metoda `PrenesiStavkeIRN`), frmPrijemKontejneraKamionLegetIzvoz („OTVORI - GATE IN KAMION IZVOZ“), frmPrijemKontejneraLegetVozUvoz („OTVORI - GATE IN VOZ“)
- **Tabele:**
  - `DirigacijaKontejneraZa` — S
  - `IzvozKonacna` — S
  - `Partnerji` — S
  - `PrijemKontejneraVoz` — S
  - `PrijemKontejneraVozStavke` — S
  - `RadniNalogInterni` — S
  - `RNPrijemPlatforme` — S
  - `TipKontenjera` — S
  - `UvozKonacna` — S
  - `UvozKOnacna` — S
  - `UvozKonacnaZaglavlje` — S
  - `Voz` — S
  - `VoziloUsluga` — S
  - `VrstePostupakaUvoz` — S
- **Procedure:** `DelRadniNalogInterniSaDokumentima`, `DeleteRadniNalogInterni`, `DeleteUvozKonacna`, `DeleteUvozKonacnaNHM`, `DeleteUvozKonacnaNapomenePozicioniranja`, `DeleteUvozKonacnaVrstaManipulacije`, `DeleteUvozKonacnaVrstaManipulacijeKamionska`, `DeleteUvozKonacnaVrstaRobeHS`, `DeleteUvozNHM`, `DeleteUvozNHMDiana`, `DeleteUvozNapomenePozicioniranja`, `DeleteUvozTerminalskeManipulacije`, `DeleteUvozTerminalskeManipulacijeKonacna`, `DeleteUvozUslugaDokumenta`, `DeleteUvozUvoznici`, `DeleteUvozVrstaManTerminalskih`, `DeleteUvozVrstaManipulacije`, `DeleteUvozVrstaManipulacijeKamionska`, `DeleteUvozVrstaRobeHS`, `DeleteUvozkonacnaNHMDiana`, `DozvoliIzlaz`, `InsUbaciDodatnuUsluguIzvoz`, `InsUbaciDodatnuUsluguUvoz`, `InsertRadniNalogInterni`, `InsertRadniNalogInterni2`, `InsertRadniNalogInterniIzvoz`, `InsertRadniNalogInterniIzvoz2`, `InsertRadniNalogInterniIzvozDodatne`, `InsertRadniNalogInterniPotvrda`, `InsertRadniNalogInterniSkladistePotvrda`, `InsertRadniNalogInterniStorno`, `InsertUvozKonacbaVrstaRobeHS`, `InsertUvozKonacna`, `InsertUvozKonacnaNHM`, `InsertUvozKonacnaNHMDiana`, `InsertUvozKonacnaNapomenePozicioniranja`, `InsertUvozKonacnaVrstaManipulacije`, `InsertUvozNHM`, `InsertUvozNHMDiana`, `InsertUvozNapomenePozicioniranja`, `InsertUvozUslugaDokument`, `InsertUvozUvoznici`, `InsertUvozVrstaManipulacije`, `InsertUvozVrstaRobeHS`, `ObavljenPregled`, `PrebaciNHMUvozUvozKonacna`, `PrebaciVrsterobeHSUvozUvozKonacna`, `PrenesiIzPlanUtovaraUPlanUtovara`, `PrenesiUPlanUtovaraSelektovano`, `PromeniSaPrijemniceUK`, `PromeniStatusKalmar`, `PromeniStatusKapijaOdlazak`, `PromeniStatusPretovareno`, `PromeniStatusPrijemPotvrdioKapiju`, `PromeniStatusPrijemPotvrdioVP`, `UbaciURadniNalogInterniBeyScenarija`, `UpdCenaUvozKonacnaVrstaManipulacije`, `UpdCenaUvozVrstaManipulacije`, `UpdPlatiocaUvozKonacnaVrstaManipulacije`, `UpdPlatiocaUvozVrstaManipulacije`, `UpdPokretFormaStatus`, `UpdateBrojKolaIzExcela`, `UpdateBrojKolaIzExcelaVoz`, `UpdateRNIVozFormiran`, `UpdateRadniNalogInterni`, `UpdateRadniNalogInterniFormiran`, `UpdateRadniNalogInterniGenerisan`, `UpdateRadniNalogInterniZavrsen`, `UpdateUvozKonacna`, `UpdateUvozKonacnaManFaktura`, `UpdateUvozKonacnaManZaFakturisanje`, `UpdateUvozKonacnaObjedinjenja`, `UpdateUvozKonacnaPK`, `UpdateUvozKonacnaUC`, `UpdateUvozNHMTezine`, `VratiIzPlanaUtovaraSelektovano`, `ZavrsiDodatnuUsluguIzvozZadata`, `ZavrsiDodatnuUsluguUvozZadata`, `spIzbrisiKUScenario`, `spPrenesiKontejnerIzPlanaNaPrijemnicu`, `spPrenesiKontejnerIzPlanaNaPrijemnicuIzvoz`, `spPrenesiKontejnerIzPlanaNaPrijemnicuIzvozPoIdNadredjenog`, `spPrenesiPlanUtovaraUPrijemPlatforma`, `spPrenesiPlanUtovaraUPrijemPlatformaIzvoz`, `spPrenesiPlanUtovaraUPrijemVoz`, `spStornijaRNI`, `updRadniNalogInterniPrijemnicaPotvrdjen`, `updRadniNalogInterniRukovalacPotvrdjen`, `updRadniNalogInterniSkladistarPotvrdio`, `updRadniNalogInterniSkladistePotvrdjen`
- **Funkcionalnosti (kontrola → handler):**
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

#### frmPutniNalog
- **Fajl:** `Dokumenta/frmPutniNalog.cs`  
- **Naslov forme:** „Putni nalog“
- **Modul:** Intermodalni terminal, Kapija, Prijem i otprema vozova
- **Otvara forme:** frmRadniNalogTransport („Radni nalog“), frmRadniNalogTransport („Radni nalog“), frmRadniNalogTransport („Radni nalog“), frmRadniNalogTransport („Radni nalog“)
- **Tabele:**
  - `PutniNalog` — S
  - `PutniNalogGorivo` — S
  - `PutniNalogGume` — S
  - `PutniNalogKvarovi` — S
  - `PutniNalogRuta` — S
  - `PutniNalogServis` — S
  - `PutniNalogTroskovi` — S
  - `PutniNalogZaposleni` — S
  - `Vozila` — S
  - `Zaposleni` — S
- **Procedure:** `DeletePutniNalog`, `DeletePutniNalogGorivo`, `DeletePutniNalogGume`, `DeletePutniNalogKvarovi`, `DeletePutniNalogRuta`, `DeletePutniNalogServis`, `DeletePutniNalogTroskovi`, `DeletePutniNalogZaposleni`, `InsertPutniNalog`, `InsertPutniNalogGorivo`, `InsertPutniNalogGume`, `InsertPutniNalogKvarovi`, `InsertPutniNalogRuta`, `InsertPutniNalogServis`, `InsertPutniNalogTroskovi`, `InsertPutniNalogZaposleni`, `UpdatePutniNalog`, `UpdatePutniNalogGorivo`, `UpdatePutniNalogGume`, `UpdatePutniNalogKvarovi`, `UpdatePutniNalogRuta`, `UpdatePutniNalogServis`, `UpdatePutniNalogTroskovi`, `UpdatePutniNalogZaposleni`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: DeletePutniNalog, InsertPutniNalog, UpdatePutniNalog; preko: InsertPutniNalog
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „Štampa Putni nalog“ — klik → `toolStripLabel1_Click` — izveštaj: rptPutniNalog.rdlc
  - „Radni nalog“ — klik → `toolStripLabel2_Click` — otvara: frmRadniNalogTransport
  - „Servis“ — klik → `tabPage1_Click`
  - „Izbriši“ — klik → `button2_Click` — proc: DeletePutniNalogServis, InsertPutniNalogServis, UpdatePutniNalogServis; preko: InsertPutniNalogServis
  - „Promeni“ — klik → `button1_Click` — proc: DeletePutniNalogServis, InsertPutniNalogServis, UpdatePutniNalogServis; preko: InsertPutniNalogServis
  - „Unesi“ — klik → `btnPretrazi_Click` — proc: DeletePutniNalogServis, InsertPutniNalogServis, UpdatePutniNalogServis; preko: InsertPutniNalogServis
  - „?“ — klik → `button22_Click` — čita: PutniNalog, PutniNalogZaposleni, Zaposleni
  - „Izbriši“ — klik → `button3_Click` — proc: DeletePutniNalogRuta, InsertPutniNalogRuta, UpdatePutniNalogRuta; preko: InsertPutniNalogRuta
  - „Promeni“ — klik → `button4_Click` — proc: DeletePutniNalogRuta, InsertPutniNalogRuta, UpdatePutniNalogRuta; preko: InsertPutniNalogRuta
  - „Unesi“ — klik → `button5_Click` — proc: DeletePutniNalogRuta, InsertPutniNalogRuta, UpdatePutniNalogRuta; preko: InsertPutniNalogRuta
  - „Izbriši“ — klik → `button6_Click` — proc: DeletePutniNalogGume, InsertPutniNalogGume, UpdatePutniNalogGume; preko: InsertPutniNalogGume
  - „Promeni“ — klik → `button7_Click` — proc: DeletePutniNalogGume, InsertPutniNalogGume, UpdatePutniNalogGume; preko: InsertPutniNalogGume
  - „Unesi“ — klik → `button8_Click` — proc: DeletePutniNalogGume, InsertPutniNalogGume, UpdatePutniNalogGume; preko: InsertPutniNalogGume
  - „Izbriši“ — klik → `button9_Click` — proc: DeletePutniNalogGorivo, InsertPutniNalogGorivo, UpdatePutniNalogGorivo; preko: InsertPutniNalogGorivo
  - „Promeni“ — klik → `button10_Click` — proc: DeletePutniNalogGorivo, InsertPutniNalogGorivo, UpdatePutniNalogGorivo; preko: InsertPutniNalogGorivo
  - „Unesi“ — klik → `button11_Click` — proc: DeletePutniNalogGorivo, InsertPutniNalogGorivo, UpdatePutniNalogGorivo; preko: InsertPutniNalogGorivo
  - „Unesi“ — klik → `button14_Click` — proc: DeletePutniNalogTroskovi, InsertPutniNalogTroskovi, UpdatePutniNalogTroskovi; preko: InsertPutniNalogTroskovi
  - „Lista kvarova“ — klik → `tabPage8_Click`
  - „Izbriši“ — klik → `button19_Click` — proc: DeletePutniNalogKvarovi, InsertPutniNalogKvarovi, UpdatePutniNalogKvarovi; preko: InsertPutniNalogKvarovi
  - „Promeni“ — klik → `button20_Click` — proc: DeletePutniNalogKvarovi, InsertPutniNalogKvarovi, UpdatePutniNalogKvarovi; preko: InsertPutniNalogKvarovi
  - „Unesi“ — klik → `button21_Click` — proc: DeletePutniNalogKvarovi, InsertPutniNalogKvarovi, UpdatePutniNalogKvarovi; preko: InsertPutniNalogKvarovi
  - „Pošalji mail“ — klik → `button18_Click` — proc: DeletePutniNalogKvarovi, InsertPutniNalogKvarovi, UpdatePutniNalogKvarovi; preko: InsertPutniNalogKvarovi
  - „Izbriši“ — klik → `button15_Click` — proc: DeletePutniNalogZaposleni, InsertPutniNalogZaposleni, UpdatePutniNalogZaposleni; preko: InsertPutniNalogZaposleni
  - „Promeni“ — klik → `button16_Click` — proc: DeletePutniNalogZaposleni, InsertPutniNalogZaposleni, UpdatePutniNalogZaposleni; preko: InsertPutniNalogZaposleni
  - „Unesi“ — klik → `button17_Click` — proc: DeletePutniNalogZaposleni, InsertPutniNalogZaposleni, UpdatePutniNalogZaposleni; preko: InsertPutniNalogZaposleni
  - „Dokument“ — klik → `tabPage6_Click`
  - `this` — otvaranje forme → `frmPutniNalog_Load` — čita: Vozila, Zaposleni

#### frmRadniNalogInterniPregled
- **Fajl:** `Uvoz/frmRadniNalogInterniPregled.cs`  
- **Naslov forme:** „Pregled internih radnih naloga“
- **Modul:** Intermodalni terminal, Prijem i otprema vozova
- **Otvara forme:** frmFormiranjePlanaIzvoz („Dodaj izvoznom vozu iz uvoza“), frmOtpremaKontejneraKamionomIzKontejnera („Formiraj otpremu kamionom“), frmOtpremaVozaIzPlana („Potvrdi“), frmPrijemKamionaIzPlana („Formiraj prijem kamionom“), frmPrijemKontejneraKamionLegetUvoz („Potvrdi“), frmPrijemVozaIzPlana („Potvrdi“), frmScenarioSCI („Pregledaj podatke“), RN12MedjuskladisniKontejnera („Potvrdi“), RN4PrijemPlatforme („Otvori RN“), RN5PrijemPlatforme2 („Otvori RN“), RN6OtpremaPlatforme („Otvori RN“), RN7OtpremaPlatforme2 („Otvori RN“), ZapisnikONepravilnosti („Zapisnik o nepravilnosti“), frmFormiranjePlanaIzvoz („Dodaj izvoznom vozu iz uvoza“), frmOtpremaKontejneraKamionomIzKontejnera („Formiraj otpremu kamionom“), frmOtpremaVozaIzPlana („Potvrdi“), frmPrijemKamionaIzPlana („Formiraj prijem kamionom“), frmPrijemKontejneraKamionLegetUvoz („Potvrdi“), frmPrijemVozaIzPlana („Potvrdi“), frmScenarioSCI („Pregledaj podatke“), RN12MedjuskladisniKontejnera („Potvrdi“), RN1PrijemVoza („Otvori RN“), RN2OtpremaVoza („Otvori RN“), RN4PrijemPlatforme („Otvori RN“), RN5PrijemPlatforme2 („Otvori RN“), RN6OtpremaPlatforme („Otvori RN“), RN7OtpremaPlatforme2 („Otvori RN“), ZapisnikONepravilnosti („Zapisnik o nepravilnosti“)
- **Tabele:**
  - `Brodovi` — S
  - `Carinarnice` — S
  - `DirigacijaKontejneraZa` — S
  - `IzvozKonacna` — S
  - `IzvozKonacnaVrstaManipulacije` — S
  - `IzvozKonacnaZaglavlje` — S
  - `KontejnerskiTerminali` — S
  - `KontejnerStatus` — S
  - `MestaUtovara` — S
  - `OrganizacioneJedinice` — S
  - `OtpremaKontejnera` — S
  - `Partnerji` — S
  - `Predefinisaneporuke` — S
  - `PrijemKontejneraVoz` — S
  - `ProdajniNalogIzvozStavke` — S
  - `RadniNalogInterni` — S
  - `RadniNalogInterniPotvrda` — S
  - `Scenario` — S
  - `TipKontenjera` — S
  - `uvNacinPakovanja` — S
  - `UvozKonacna` — S
  - `UvozKonacnaNapomenePozicioniranja` — S
  - `UvozKonacnaNHM` — S
  - `UvozKonacnaVrstaManipulacije` — S
  - `UvozKonacnaZaglavlje` — S
  - `UvozNapomenePozicioniranja` — S
  - `Voz` — S
  - `VrstaCarinskogPostupka` — S
  - `VrstaManipulacije` — S
  - `VrstaRobeADR` — S
  - `VrstePostupakaUvoz` — S
- **Procedure:** `DelRadniNalogInterniSaDokumentima`, `DeleteIzvozKonacnaNapomenePozicioniranja`, `DeleteIzvozKonacnaSve`, `DeleteIzvozNapomenePozicioniranja`, `DeleteIzvozUslugaDokumenta`, `DeleteIzvozVrstaManTerminalskih`, `DeleteRNMedjuskladisni`, `DeleteRNOtpremaCirade`, `DeleteRNOtpremaPlatforme`, `DeleteRNOtpremaPlatforme2`, `DeleteRNOtpremaVoza`, `DeleteRNPregledIpostavkaKontejnera`, `DeleteRNPregledIspraznjenogKontejnera`, `DeleteRNPrijemCirade`, `DeleteRNPrijemPlatforme`, `DeleteRNPrijemPlatforme2`, `DeleteRNPrijemVoza`, `DeleteRNPrijemVoza2`, `DeleteRadniNalogInterni`, `DeleteUvozKonacna`, `DeleteUvozKonacnaNHM`, `DeleteUvozKonacnaNapomenePozicioniranja`, `DeleteUvozKonacnaVrstaManipulacije`, `DeleteUvozKonacnaVrstaManipulacijeKamionska`, `DeleteUvozKonacnaVrstaRobeHS`, `DeleteUvozNHM`, `DeleteUvozNHMDiana`, `DeleteUvozNapomenePozicioniranja`, `DeleteUvozTerminalskeManipulacije`, `DeleteUvozTerminalskeManipulacijeKonacna`, `DeleteUvozUslugaDokumenta`, `DeleteUvozUvoznici`, `DeleteUvozVrstaManTerminalskih`, `DeleteUvozVrstaManipulacije`, `DeleteUvozVrstaManipulacijeKamionska`, `DeleteUvozVrstaRobeHS`, `DeleteUvozkonacnaNHMDiana`, `DozvoliIzlaz`, `InsUbaciDodatnuUsluguIzvoz`, `InsUbaciDodatnuUsluguUvoz`, `InsertIzvozKonacnaNapomenePozicioniranja`, `InsertIzvozNapomenePozicioniranja`, `InsertIzvozUslugaDokument`, `InsertRN10PregledCeoVoz`, `InsertRN11PregledCeoVozOtprema`, `InsertRN12Medjuskladisni`, `InsertRN4PrPlatformeKamIzvoz`, `InsertRN4PrPlatformeKamUvoz`, `InsertRN5PrPlatFormeKam`, `InsertRN6OtPlatFormeKam`, `InsertRN7OtpremaOtpremaPlatKam`, `InsertRN8OtpremaCiradeKam`, `InsertRN8OtpremaCiradeKamIzvoz`, `InsertRN9PrijemCiradeKam`, `InsertRNOtpremaCirade`, `InsertRNOtpremaPlatforme`, `InsertRNOtpremaPlatforme2`, `InsertRNOtpremaVoza`, `InsertRNOtpremaVozaCeoVoz`, `InsertRNPregledIpostavkaKontejnera`, `InsertRNPregledIspraznjenogKontejnera`, `InsertRNPrijemCirade`, `InsertRNPrijemPlatforme`, `InsertRNPrijemPlatforme2`, `InsertRNPrijemVoza`, `InsertRNPrijemVoza2`, `InsertRNPrijemVozaCeoVoz`, `InsertRNPrijemVozaCeoVozPretovar`, `InsertRadniNalogInterni`, `InsertRadniNalogInterni2`, `InsertRadniNalogInterniIzvoz`, `InsertRadniNalogInterniIzvoz2`, `InsertRadniNalogInterniIzvozDodatne`, `InsertRadniNalogInterniPotvrda`, `InsertRadniNalogInterniSkladistePotvrda`, `InsertRadniNalogInterniStorno`, `InsertUvozKonacbaVrstaRobeHS`, `InsertUvozKonacna`, `InsertUvozKonacnaNHM`, `InsertUvozKonacnaNHMDiana`, `InsertUvozKonacnaNapomenePozicioniranja`, `InsertUvozKonacnaVrstaManipulacije`, `InsertUvozNHM`, `InsertUvozNHMDiana`, `InsertUvozNapomenePozicioniranja`, `InsertUvozUslugaDokument`, `InsertUvozUvoznici`, `InsertUvozVrstaManipulacije`, `InsertUvozVrstaRobeHS`, `ObavljenPregled`, `OdrediKontejnerTerminal`, `PokreniSkladisninuKontejnera`, `PrebaciNHMUvozUvozKonacna`, `PrebaciVrsterobeHSUvozUvozKonacna`, `PrenesiIzPlanIzvozaUPlanIzvoza`, `PrenesiIzPlanUtovaraUPlanUtovara`, `PrenesiKontejnerTekuceUArhiv`, `PrenesiUPlanUtovaraSelektovano`, `PrenesiUPlanUtovaraSelektovanoIzvoz`, `PrenesiUPlanUtovaraSelektovanoIzvozBrodar`, `PrenesiUPlanUtovaraSelektovanoIzvozTErminal`, `PromeniSaPrijemniceUK`, `PromeniStatusKalmar`, `PromeniStatusKapijaOdlazak`, `PromeniStatusPretovareno`, `PromeniStatusPrijemPotvrdioKapiju`, `PromeniStatusPrijemPotvrdioVP`, `UbaciURadniNalogInterniBeyScenarija`, `UpdCenaUvozKonacnaVrstaManipulacije`, `UpdCenaUvozVrstaManipulacije`, `UpdPlatiocaUvozKonacnaVrstaManipulacije`, `UpdPlatiocaUvozVrstaManipulacije`, `UpdPokretFormaStatus`, `UpdateBrojKolaIzExcela`, `UpdateBrojKolaIzExcelaVoz`, `UpdateIZCIRA`, `UpdateIzvozKonacnaVagon`, `UpdateRN12MedjuskladisniSkladiste`, `UpdateRN12Uradjene`, `UpdateRN1Uradjene`, `UpdateRN1UradjeneCIR`, `UpdateRN1UradjeneS`, `UpdateRN1UradjeneVP`, `UpdateRN2Uradjene`, `UpdateRN3Uradjene`, `UpdateRN4PotrebanCIR`, `UpdateRN4PrivremenoSkladiste`, `UpdateRN4SkladisteIzCira`, `UpdateRN4Uradjene`, `UpdateRN4UradjeneCIR`, `UpdateRN4UradjenePostavljen`, `UpdateRN4UradjeneVizuelni`, `UpdateRN5PrijemSkladiste`, `UpdateRN5Uradjene`, `UpdateRN5UradjeneCIR`, `UpdateRN5UradjeneS`, `UpdateRN6OtpremaSkladiste`, `UpdateRN6PotrebanCIR`, `UpdateRN6Uradjene`, `UpdateRN6UradjeneVizuelni`, `UpdateRN7Uradjene`, `UpdateRN8Uradjene`, `UpdateRN8UradjeneCIR`, `UpdateRN9PretovarCIRADE`, `UpdateRN9Uradjen`, `UpdateRNIVozFormiran`, `UpdateRNMedjuskladisni`, `UpdateRNOtpremaCirade`, `UpdateRNOtpremaPlatforme`, `UpdateRNOtpremaPlatforme2`, `UpdateRNOtpremaVoza`, `UpdateRNPregledIpostavkaKontejnera`, `UpdateRNPregledIspraznjenogKontejnera`, `UpdateRNPrijemCirade`, `UpdateRNPrijemPlatforme`, `UpdateRNPrijemPlatforme2`, `UpdateRNPrijemVoza`, `UpdateRNPrijemVoza2`, `UpdateRNPrijemVozaSkladiste`, `UpdateRadniNalogInterni`, `UpdateRadniNalogInterniFormiran`, `UpdateRadniNalogInterniGenerisan`, `UpdateRadniNalogInterniZavrsen`, `UpdateUvozKonacna`, `UpdateUvozKonacnaManFaktura`, `UpdateUvozKonacnaManZaFakturisanje`, `UpdateUvozKonacnaObjedinjenja`, `UpdateUvozKonacnaPK`, `UpdateUvozKonacnaUC`, `UpdateUvozNHMTezine`, `VratiIzPlanaUtovaraSelektovano`, `VratiKontejnerNerasporedjenoSelektovanoIzvoz`, `ZatvoriSkladisninuKontejnera`, `ZavrsiDodatnuUsluguIzvozZadata`, `ZavrsiDodatnuUsluguUvozZadata`, `spIzbrisiKUScenario`, `spPrenesiIzPlanUtovaraUOtpremaVoz`, `spPrenesiIzUvozaUIzvoz`, `spPrenesiKontejnerIzPlanaNaPrijemnicu`, `spPrenesiKontejnerIzPlanaNaPrijemnicuIzvoz`, `spPrenesiKontejnerIzPlanaNaPrijemnicuIzvozPoIdNadredjenog`, `spPrenesiKontejnerUOtpremuKamionom`, `spPrenesiKontejnerUOtpremuKamionomIzvoz`, `spPrenesiPlanUtovaraUPrijemPlatforma`, `spPrenesiPlanUtovaraUPrijemPlatformaIzvoz`, `spPrenesiPlanUtovaraUPrijemVoz`, `spStornijaRNI`, `updRN4PrPlatformeKamIzvozPusti`, `updRadniNalogInterniPrijemnicaPotvrdjen`, `updRadniNalogInterniRukovalacPotvrdjen`, `updRadniNalogInterniSkladistarPotvrdio`, `updRadniNalogInterniSkladistePotvrdjen`
- **Funkcionalnosti (kontrola → handler):**
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
  - „Potvrdi“ — klik → `toolStripButton8_Click` — otvara: frmPrijemVozaIzPlana, frmOtpremaVozaIzPlana; proc: DeleteIzvozKonacnaNapomenePozicioniranja, DeleteIzvozKonacnaSve, DeleteIzvozNapomenePozicioniranja, DeleteIzvozUslugaDokumenta, DeleteIzvozVrstaManTerminalskih, DeleteRNMedjuskladisni; preko: InsertIzvozKonacna, InsertRN, InsertUvozKonacna
  - „Završi“ — klik → `toolStripButton4_Click`
  - „Storniraj“ — klik → `toolStripButton3_Click` — proc: DelRadniNalogInterniSaDokumentima, DeleteRadniNalogInterni, DozvoliIzlaz, InsertRadniNalogInterni, InsertRadniNalogInterni2, InsertRadniNalogInterniIzvoz; preko: InsertRadniNalogInterni
  - „Izdat za OJ“ — klik → `toolStripButton2_Click` — čita: OrganizacioneJedinice, Partnerji, RadniNalogInterni, TipKontenjera, UvozKonacna
  - „Izdao OJ“ — klik → `toolStripButton1_Click` — čita 16 tabela; izvoz u Excel
  - „IZDAO OJ“ — klik → `toolStripButton1_Click_1`
  - `this` — otvaranje forme → `frmRadniNalogInterniPregled_Load` — čita: OrganizacioneJedinice

#### frmRadniNalogTransport
- **Fajl:** `Dokumenta/frmRadniNalogTransport.cs`  
- **Naslov forme:** „Radni nalog transport“
- **Modul:** Intermodalni terminal, Kapija, Prijem i otprema vozova
- **Otvara forme:** frmAutoprevozniList2 („Autoprevozni list“), frmAutoprevozniList2 („Autoprevozni list“), frmAutoprevozniList2 („Autoprevozni list“), frmAutoprevozniList2 („Autoprevozni list“)
- **Tabele:**
  - `RadniNalogTransport` — S
  - `Vozila` — S
  - `Zaposleni` — S
- **Procedure:** `DeleteRadniNalogTransportStavke`, `DeleteRadniNalogZaTransport`, `InsertRadniNalogTransport`, `InsertRadniNalogTransportStavke`, `UpdateRadniNalogTransportStavke`, `UpdateRadniNalogZaTransport`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: DeleteRadniNalogZaTransport, InsertRadniNalogTransport, UpdateRadniNalogZaTransport; preko: InsertRadniNalogTransport
  - „toolStripButton1“ — klik → `tsDelete_Click`
  - „Štampa radni nalog“ — klik → `toolStripLabel1_Click` — izveštaj: rptRadniNalogTransport.rdlc
  - „Autoprevozni list“ — klik → `toolStripLabel2_Click` — otvara: frmAutoprevozniList2
  - „Izbriši“ — klik → `button12_Click` — proc: DeleteRadniNalogTransportStavke, InsertRadniNalogTransportStavke, UpdateRadniNalogTransportStavke; preko: InsertRadniNalogTransportStavke
  - „Promeni“ — klik → `button13_Click` — proc: DeleteRadniNalogTransportStavke, InsertRadniNalogTransportStavke, UpdateRadniNalogTransportStavke; preko: InsertRadniNalogTransportStavke
  - „Unesi“ — klik → `button14_Click` — proc: DeleteRadniNalogTransportStavke, InsertRadniNalogTransportStavke, UpdateRadniNalogTransportStavke; preko: InsertRadniNalogTransportStavke
  - „Korisnik:“ — klik → `label11_Click`
  - „Stavka:“ — klik → `label8_Click`
  - „Podaci“ — klik → `tabPage1_Click`
  - `this` — otvaranje forme → `frmRadniNalogTransport_Load` — čita: Vozila, Zaposleni

#### frmScenarioSCI
- **Fajl:** `RNI/frmScenarioSCI.cs`  
- **Naslov forme:** „Po scenariju 1“
- **Modul:** Intermodalni terminal, Operacije skladišta, Prijem i otprema vozova
- **Tabele:**
  - `Carinarnice` — S
  - `InspekciskiTretman` — S
  - `IzvozKonacna` — S
  - `IzvozKonacnaNHM` — S
  - `IzvozNapomenePozicioniranja` — S
  - `KontejnerskiTerminali` — S
  - `MestaUtovara` — S
  - `NapomenaZaPozicioniranje` — S
  - `NHM` — S
  - `Partnerji` — S
  - `partnerjiKontOsebaMU` — S
  - `ProdajniNalogIzvoz` — S
  - `TipKontenjera` — S
  - `uvKvalitetKontejnera` — S
  - `uvNacinPakovanja` — S
  - `VrstaCarinskogPostupka` — S
  - `VrstaRobeADR` — S
  - `VrstaVozila` — S

#### frmTerminalPrivremeni
- **Fajl:** `RNI/frmTerminalPrivremeni.cs`  
- **Naslov forme:** „Terminal privremeni“
- **Otvara forme:** frmTerminalPrivremeniArhiva („Arhivirani podaci“), frmTerminalPrivremeniDokumenta („Zakači dokumenta“), frmTerminalPrivremeniExcel („Uvoz Excel“), frmTerminalPrivremeniLog („Pregled log“), frmTerminalPrivremeniOpstiIzvoz („Opšti izvoz“), frmTerminalPrivremeniPromeni („Grupna promena“), frmTerminalPrivremeniSlike („Zakači slike“)
- **Tabele:**
  - `TerminalPrivCMA` — U (preko TerminalPrivCMAIzvoz)
  - `TerminalPrivCOSCO` — U (preko TerminalPrivCOSCOIzvoz)
  - `TerminalPrivFLEXBOX` — U (preko TerminalPrivFLEXBOXIzvoz)
  - `TerminalPrivHAPAG` — U (preko TerminalPrivHAPAGIzvoz)
  - `TerminalPrivHMM` — U (preko TerminalPrivHMMIzvoz)
  - `TerminalPrivMaersk` — U (preko TerminalPrivMaerskIzvoz)
  - `TerminalPrivMSC` — U (preko TerminalPrivMSCIzvoz)
  - `TerminalPrivOOCL` — U (preko TerminalPrivOOCLIzvoz, TerminalPrivOOCLMailIzvoz)
- **Procedure:** `InsertTerminalPrivArhiv`, `delTerminalPriv`, `insTerminalPriv`, `insTerminalPrivFromExcel`, `updTerminalPriv`
- **Funkcionalnosti (kontrola → handler):**
  - „gridGroupingControl1“ — TableControlCurrentCellChanged → `gridGroupingControl1_TableControlCurrentCellChanged`
  - „gridGroupingControl1“ — SelectedRecordsChanged → `gridGroupingControl1_SelectedRecordsChanged`
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - „Štampaj potvrdu“ — klik → `btnStampajPotvrdu_Click`
  - „Novi“ — klik → `btnNovi_Click`
  - „Potvrdi promenu“ — klik → `btnPromeni_Click` — proc: InsertTerminalPrivArhiv, delTerminalPriv, insTerminalPriv, insTerminalPrivFromExcel, updTerminalPriv; preko: insertTerminalPriv
  - „Obriši“ — klik → `btnObrisi_Click` — proc: InsertTerminalPrivArhiv, delTerminalPriv, insTerminalPriv, insTerminalPrivFromExcel, updTerminalPriv; preko: insertTerminalPriv
  - „Arhiviraj“ — klik → `btnArhiviraj_Click` — proc: InsertTerminalPrivArhiv, delTerminalPriv, insTerminalPriv, insTerminalPrivFromExcel, updTerminalPriv; preko: insertTerminalPriv
  - „Opšti izvoz“ — klik → `btnOpstiIzvoz_Click` — otvara: frmTerminalPrivremeniOpstiIzvoz; izvoz u Excel
  - „Poništi filtere“ — klik → `btnPonistiFiltere_Click`
  - „Arhivirani podaci“ — klik → `btnArhiviraniPodaci_Click` — otvara: frmTerminalPrivremeniArhiva
  - „Pregled log“ — klik → `btnPregledLoga_Click` — otvara: frmTerminalPrivremeniLog
  - „Zakači dokumenta“ — klik → `btnZakaciDokumenta_Click` — otvara: frmTerminalPrivremeniDokumenta
  - „Zakači slike“ — klik → `btnZakaciSlike_Click` — otvara: frmTerminalPrivremeniSlike
  - „Sačuvaj izmene“ — klik → `btnSacuvajIzmene_Click` — proc: InsertTerminalPrivArhiv, delTerminalPriv, insTerminalPriv, insTerminalPrivFromExcel, updTerminalPriv; preko: insertTerminalPriv
  - „Grupna promena“ — klik → `btnGrupnaPromena_Click` — otvara: frmTerminalPrivremeniPromeni
  - „Uvoz Excel“ — klik → `btnUvozExcel_Click` — otvara: frmTerminalPrivremeniExcel; izvoz u Excel
  - „gridGroupingControl1“ — TableControlCurrentCellStartEditing → `gridGroupingControl1_TableControlCurrentCellStartEditing`

#### frmTerminalPrivremeniArhiva
- **Fajl:** `RNI/frmTerminalPrivremeniArhiva.cs`  
- **Naslov forme:** „Arhivirani podaci“
- **Funkcionalnosti (kontrola → handler):**
  - „Pregled log“ — klik → `btnPregledLog_Click` — otvara: frmTerminalPrivremeniLog
  - „Zakači dokumenta“ — klik → `btnZakaciDokumenta_Click` — otvara: frmTerminalPrivremeniDokumenta
  - „Zakači slike“ — klik → `btnZakaciSlike_Click` — otvara: frmTerminalPrivremeniSlike

#### frmTerminalPrivremeniDokumenta
- **Fajl:** `RNI/frmTerminalPrivremeniDokumenta.cs`  
- **Naslov forme:** „Terminal privremeni - dokumenta“
- **Funkcionalnosti (kontrola → handler):**
  - „Otvori folder“ — klik → `btnOtvoriFolder_Click`
  - „Otvori izabrani dokument“ — klik → `btnOtvoriDokument_Click`
  - „Osveži“ — klik → `btnOsvezi_Click`
  - „Dodaj dokumenta...“ — klik → `btnDodajDokumenta_Click`
  - „Prevucite dokumenta ovde (drag && drop)“ — DragDrop → `Dokumenta_DragDrop`
  - „Prevucite dokumenta ovde (drag && drop)“ — DragEnter → `Dokumenta_DragEnter`

#### frmTerminalPrivremeniExcel
- **Fajl:** `RNI/frmTerminalPrivremeniExcel.cs`  
- **Naslov forme:** „Terminal privremeni - uvoz Excel“
- **Procedure:** `InsertTerminalPrivArhiv`, `delTerminalPriv`, `insTerminalPriv`, `insTerminalPrivFromExcel`, `updTerminalPriv`
- **Funkcionalnosti (kontrola → handler):**
  - „Učitaj“ — klik → `btnUcitaj_Click` — proc: InsertTerminalPrivArhiv, delTerminalPriv, insTerminalPriv, insTerminalPrivFromExcel, updTerminalPriv; preko: insertTerminalPriv; izvoz u Excel
  - „Otvori fajl“ — klik → `btnOtvoriFajl_Click` — izvoz u Excel

#### frmTerminalPrivremeniLog
- **Fajl:** `RNI/frmTerminalPrivremeniLog.cs`  
- **Naslov forme:** „Terminal privremeni - log“
- **Funkcionalnosti (kontrola → handler):**
  - „Prikaži po datumu“ — klik → `btnPoDatumu_Click`
  - „Prikaži zadnjih 1000“ — klik → `btnZadnjih1000_Click`
  - „Prikaži za izabrani zapis“ — klik → `btnZaZapis_Click`

#### frmTerminalPrivremeniOpstiIzvoz
- **Fajl:** `RNI/frmTerminalPrivremeniOpstiIzvoz.cs`  
- **Naslov forme:** „Opšti izvoz“
- **Funkcionalnosti (kontrola → handler):**
  - „Označi sve“ — klik → `btnOznaciSve_Click`
  - „Poništi sve“ — klik → `btnPonistiSve_Click`
  - „Izvezi u Excel“ — klik → `btnIzvezi_Click`

#### frmTerminalPrivremeniPromeni
- **Fajl:** `RNI/frmTerminalPrivremeniPromeni.cs`  
- **Naslov forme:** „Grupna promena“
- **Funkcionalnosti (kontrola → handler):**
  - „Primeni“ — klik → `btnPrimeni_Click`

#### frmTerminalPrivremeniSlike
- **Fajl:** `RNI/frmTerminalPrivremeniSlike.cs`  
- **Naslov forme:** „Terminal privremeni - slike“
- **Procedure:** `InsertTerminalPrivArhiv`, `delTerminalPriv`, `insTerminalPriv`, `insTerminalPrivFromExcel`, `updTerminalPriv`
- **Funkcionalnosti (kontrola → handler):**
  - „Otvori folder“ — klik → `btnOtvoriFolder_Click`
  - „Osveži“ — klik → `btnOsvezi_Click`
  - „Dodaj slike...“ — klik → `btnDodajSlike_Click`
  - „Prevucite slike ovde (drag && drop)“ — DragDrop → `Slike_DragDrop`
  - „Prevucite slike ovde (drag && drop)“ — DragEnter → `Slike_DragEnter`

#### frmTovarniList
- **Fajl:** `Testiranje/frmTovarniList.cs`  
- **Naslov forme:** „Tovarni list“
- **Modul:** Intermodalni terminal, Kapija, Prijem i otprema vozova
- **Tabele:**
  - `Partnerji` — S
  - `TovarniList` — S
- **Funkcionalnosti (kontrola → handler):**
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

#### Intermodalni1
- **Fajl:** `MainLeget/Intermodalni/Intermodalni1.cs`  
- **Naslov forme:** „Intermodalni1“
- **Otvara forme:** frmTerminalPrivremeni („Terminal temp“), IntermodalniKomN („Komercijalni nalozi“), VozniPlanovi („Vozni planovi“)
- **Funkcionalnosti (kontrola → handler):**
  - „Komercijalni nalozi“ — klik → `btnIntermodalni11_Click` — otvara: IntermodalniKomN
  - „Vozni planovi“ — klik → `btnIntermodalni13_Click` — otvara: VozniPlanovi
  - „Terminal temp“ — klik → `btnIntermodalni15_Click`

#### IntermodalniKomN
- **Fajl:** `MainLeget/Intermodalni/IntermodalniKomN.cs`  
- **Naslov forme:** „IntermodalniKomN“
- **Otvara forme:** frmRadniNalogInterniPregled („Lista svih naloga“)
- **Funkcionalnosti (kontrola → handler):**
  - „Lista otvorenih naloga“ — klik → `btmKomercijalni4_Click` — otvara: frmRadniNalogInterniPregled
  - „Lista svih naloga“ — klik → `btmKomercijalni6_Click` — otvara: frmRadniNalogInterniPregled

#### RN12MedjuskladisniKontejnera
- **Fajl:** `RadniNalozi/RN12MedjuskladisniKontejnera.cs`  
- **Naslov forme:** „RN12 - MEDJUSKLADISNI PRENOS“
- **Modul:** Intermodalni terminal, Operacije pretovari, Prijem i otprema vozova
- **Otvara forme:** frmDodelaSkladista („Dodela polja“), frmKontejnerTekuce („KONTEJNER NA SKLADISTU“), frmDodelaSkladista („Dodela polja“), frmKontejnerTekuce („KONTEJNER NA SKLADISTU“)
- **Tabele:**
  - `KontejnerTekuce` — S
  - `NHM` — S
  - `Partnerji` — S
  - `Pozicija` — S
  - `RadniNalogInterni` — S
  - `RNMedjuskladisni` — S
  - `RNPrijemVoza` — S
  - `Skladista` — S
  - `TipKontenjera` — S
  - `VrstaManipulacije` — S
- **Procedure:** `DeleteRNMedjuskladisni`, `DeleteRNOtpremaCirade`, `DeleteRNOtpremaPlatforme`, `DeleteRNOtpremaPlatforme2`, `DeleteRNOtpremaVoza`, `DeleteRNPregledIpostavkaKontejnera`, `DeleteRNPregledIspraznjenogKontejnera`, `DeleteRNPrijemCirade`, `DeleteRNPrijemPlatforme`, `DeleteRNPrijemPlatforme2`, `DeleteRNPrijemVoza`, `DeleteRNPrijemVoza2`, `InsertRN10PregledCeoVoz`, `InsertRN11PregledCeoVozOtprema`, `InsertRN12Medjuskladisni`, `InsertRN4PrPlatformeKamIzvoz`, `InsertRN4PrPlatformeKamUvoz`, `InsertRN5PrPlatFormeKam`, `InsertRN6OtPlatFormeKam`, `InsertRN7OtpremaOtpremaPlatKam`, `InsertRN8OtpremaCiradeKam`, `InsertRN8OtpremaCiradeKamIzvoz`, `InsertRN9PrijemCiradeKam`, `InsertRNOtpremaCirade`, `InsertRNOtpremaPlatforme`, `InsertRNOtpremaPlatforme2`, `InsertRNOtpremaVoza`, `InsertRNOtpremaVozaCeoVoz`, `InsertRNPregledIpostavkaKontejnera`, `InsertRNPregledIspraznjenogKontejnera`, `InsertRNPrijemCirade`, `InsertRNPrijemPlatforme`, `InsertRNPrijemPlatforme2`, `InsertRNPrijemVoza`, `InsertRNPrijemVoza2`, `InsertRNPrijemVozaCeoVoz`, `InsertRNPrijemVozaCeoVozPretovar`, `PokreniSkladisninuKontejnera`, `PrenesiKontejnerTekuceUArhiv`, `UpdateIZCIRA`, `UpdateRN12MedjuskladisniSkladiste`, `UpdateRN12Uradjene`, `UpdateRN1Uradjene`, `UpdateRN1UradjeneCIR`, `UpdateRN1UradjeneS`, `UpdateRN1UradjeneVP`, `UpdateRN2Uradjene`, `UpdateRN3Uradjene`, `UpdateRN4PotrebanCIR`, `UpdateRN4PrivremenoSkladiste`, `UpdateRN4SkladisteIzCira`, `UpdateRN4Uradjene`, `UpdateRN4UradjeneCIR`, `UpdateRN4UradjenePostavljen`, `UpdateRN4UradjeneVizuelni`, `UpdateRN5PrijemSkladiste`, `UpdateRN5Uradjene`, `UpdateRN5UradjeneCIR`, `UpdateRN5UradjeneS`, `UpdateRN6OtpremaSkladiste`, `UpdateRN6PotrebanCIR`, `UpdateRN6Uradjene`, `UpdateRN6UradjeneVizuelni`, `UpdateRN7Uradjene`, `UpdateRN8Uradjene`, `UpdateRN8UradjeneCIR`, `UpdateRN9PretovarCIRADE`, `UpdateRN9Uradjen`, `UpdateRNMedjuskladisni`, `UpdateRNOtpremaCirade`, `UpdateRNOtpremaPlatforme`, `UpdateRNOtpremaPlatforme2`, `UpdateRNOtpremaVoza`, `UpdateRNPregledIpostavkaKontejnera`, `UpdateRNPregledIspraznjenogKontejnera`, `UpdateRNPrijemCirade`, `UpdateRNPrijemPlatforme`, `UpdateRNPrijemPlatforme2`, `UpdateRNPrijemVoza`, `UpdateRNPrijemVoza2`, `UpdateRNPrijemVozaSkladiste`, `ZatvoriSkladisninuKontejnera`, `updRN4PrPlatformeKamIzvozPusti`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „FORMIRAJ RADNE NALOGE ZA PRIJEM VOZA / AKTIVIRAJ“ — klik → `toolStripButton4_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „POTVRDI ZAVRSEN“ — klik → `toolStripButton3_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „KONTEJNER NA SKLADIŠTU“ — klik → `toolStripButton2_Click`
  - „T“ — klik → `button1_Click`
  - „V“ — klik → `button2_Click`
  - „Dodela polja“ — klik → `button6_Click` — otvara: frmDodelaSkladista
  - „KONTEJNER NA SKLADISTU“ — klik → `toolStripButton2_Click`
  - „POTVRDI ZAVRŠEN “ — klik → `toolStripButton3_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „FORMIRAJ RADNE NALOGE “ — klik → `toolStripButton4_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - `button20` — klik → `tsDelete_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - `button21` — klik → `tsSave_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN

#### RN1PrijemVoza
- **Fajl:** `RadniNalozi/RN1PrijemVoza.cs`  
- **Naslov forme:** „RADNI NALOZI - PRIJEM VOZA - RN ZA VIZUELNI PREGLED I KALMARISTU“
- **Modul:** Intermodalni terminal, Kapija, Prijem i otprema vozova
- **Otvara forme:** frmDodelaSkladista („Dodela polja“)
- **Tabele:**
  - `NHM` — S
  - `Partnerji` — S
  - `Pozicija` — S
  - `RadniNalogInterni` — S
  - `RNPrijemVoza` — S
  - `Skladista` — S
  - `TipKontenjera` — S
  - `UvozKonacnaNHM` — S
  - `Voz` — S
  - `VrstaManipulacije` — S
- **Procedure:** `DeleteRNMedjuskladisni`, `DeleteRNOtpremaCirade`, `DeleteRNOtpremaPlatforme`, `DeleteRNOtpremaPlatforme2`, `DeleteRNOtpremaVoza`, `DeleteRNPregledIpostavkaKontejnera`, `DeleteRNPregledIspraznjenogKontejnera`, `DeleteRNPrijemCirade`, `DeleteRNPrijemPlatforme`, `DeleteRNPrijemPlatforme2`, `DeleteRNPrijemVoza`, `DeleteRNPrijemVoza2`, `InsertRN10PregledCeoVoz`, `InsertRN11PregledCeoVozOtprema`, `InsertRN12Medjuskladisni`, `InsertRN4PrPlatformeKamIzvoz`, `InsertRN4PrPlatformeKamUvoz`, `InsertRN5PrPlatFormeKam`, `InsertRN6OtPlatFormeKam`, `InsertRN7OtpremaOtpremaPlatKam`, `InsertRN8OtpremaCiradeKam`, `InsertRN8OtpremaCiradeKamIzvoz`, `InsertRN9PrijemCiradeKam`, `InsertRNOtpremaCirade`, `InsertRNOtpremaPlatforme`, `InsertRNOtpremaPlatforme2`, `InsertRNOtpremaVoza`, `InsertRNOtpremaVozaCeoVoz`, `InsertRNPregledIpostavkaKontejnera`, `InsertRNPregledIspraznjenogKontejnera`, `InsertRNPrijemCirade`, `InsertRNPrijemPlatforme`, `InsertRNPrijemPlatforme2`, `InsertRNPrijemVoza`, `InsertRNPrijemVoza2`, `InsertRNPrijemVozaCeoVoz`, `InsertRNPrijemVozaCeoVozPretovar`, `PokreniSkladisninuKontejnera`, `PrenesiKontejnerTekuceUArhiv`, `UpdateIZCIRA`, `UpdateRN12MedjuskladisniSkladiste`, `UpdateRN12Uradjene`, `UpdateRN1Uradjene`, `UpdateRN1UradjeneCIR`, `UpdateRN1UradjeneS`, `UpdateRN1UradjeneVP`, `UpdateRN2Uradjene`, `UpdateRN3Uradjene`, `UpdateRN4PotrebanCIR`, `UpdateRN4PrivremenoSkladiste`, `UpdateRN4SkladisteIzCira`, `UpdateRN4Uradjene`, `UpdateRN4UradjeneCIR`, `UpdateRN4UradjenePostavljen`, `UpdateRN4UradjeneVizuelni`, `UpdateRN5PrijemSkladiste`, `UpdateRN5Uradjene`, `UpdateRN5UradjeneCIR`, `UpdateRN5UradjeneS`, `UpdateRN6OtpremaSkladiste`, `UpdateRN6PotrebanCIR`, `UpdateRN6Uradjene`, `UpdateRN6UradjeneVizuelni`, `UpdateRN7Uradjene`, `UpdateRN8Uradjene`, `UpdateRN8UradjeneCIR`, `UpdateRN9PretovarCIRADE`, `UpdateRN9Uradjen`, `UpdateRNMedjuskladisni`, `UpdateRNOtpremaCirade`, `UpdateRNOtpremaPlatforme`, `UpdateRNOtpremaPlatforme2`, `UpdateRNOtpremaVoza`, `UpdateRNPregledIpostavkaKontejnera`, `UpdateRNPregledIspraznjenogKontejnera`, `UpdateRNPrijemCirade`, `UpdateRNPrijemPlatforme`, `UpdateRNPrijemPlatforme2`, `UpdateRNPrijemVoza`, `UpdateRNPrijemVoza2`, `UpdateRNPrijemVozaSkladiste`, `ZatvoriSkladisninuKontejnera`, `updRN4PrPlatformeKamIzvozPusti`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „Refresh DG“ — klik → `toolStripButton7_Click`
  - „FORMIRAJ RADNE NALOGE ZA PRIJEM VOZA / AKTIVIRAJ“ — klik → `toolStripButton1_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „Potvrdi završen VP“ — klik → `toolStripButton5_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „Potvrdi završen CIR“ — klik → `toolStripButton6_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „Potvrdi završen “ — klik → `toolStripButton2_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „Pregled radnih naloga Central“ — klik → `toolStripButton4_Click` — otvara: frmAnalizaRadnihNaloga
  - „Dodela polja“ — klik → `button2_Click` — otvara: frmDodelaSkladista
  - „Potvrdi završen“ — klik → `toolStripButton2_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „Potvrdi završen CIR“ — klik → `toolStripButton6_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „Potvrdi završen VP“ — klik → `toolStripButton5_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „FORMIRAJ RADNE NALOGE ZA PRIJEM VOZA / AKTIVIRAJ“ — klik → `toolStripButton1_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „Refresh“ — klik → `toolStripButton7_Click`
  - `button20` — klik → `tsDelete_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - `button21` — klik → `tsSave_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN

#### RN2OtpremaVoza
- **Fajl:** `RadniNalozi/RN2OtpremaVoza.cs`  
- **Naslov forme:** „RN2-OTPREMA VOZA“
- **Modul:** Intermodalni terminal, Prijem i otprema vozova
- **Tabele:**
  - `KontejnerTekuce` — S
  - `NHM` — S
  - `Partnerji` — S
  - `Pozicija` — S
  - `RNOtpremaVoza` — S
  - `Skladista` — S
  - `TipKontenjera` — S
  - `Voz` — S
  - `VrstaManipulacije` — S
- **Procedure:** `DeleteRNMedjuskladisni`, `DeleteRNOtpremaCirade`, `DeleteRNOtpremaPlatforme`, `DeleteRNOtpremaPlatforme2`, `DeleteRNOtpremaVoza`, `DeleteRNPregledIpostavkaKontejnera`, `DeleteRNPregledIspraznjenogKontejnera`, `DeleteRNPrijemCirade`, `DeleteRNPrijemPlatforme`, `DeleteRNPrijemPlatforme2`, `DeleteRNPrijemVoza`, `DeleteRNPrijemVoza2`, `InsertRN10PregledCeoVoz`, `InsertRN11PregledCeoVozOtprema`, `InsertRN12Medjuskladisni`, `InsertRN4PrPlatformeKamIzvoz`, `InsertRN4PrPlatformeKamUvoz`, `InsertRN5PrPlatFormeKam`, `InsertRN6OtPlatFormeKam`, `InsertRN7OtpremaOtpremaPlatKam`, `InsertRN8OtpremaCiradeKam`, `InsertRN8OtpremaCiradeKamIzvoz`, `InsertRN9PrijemCiradeKam`, `InsertRNOtpremaCirade`, `InsertRNOtpremaPlatforme`, `InsertRNOtpremaPlatforme2`, `InsertRNOtpremaVoza`, `InsertRNOtpremaVozaCeoVoz`, `InsertRNPregledIpostavkaKontejnera`, `InsertRNPregledIspraznjenogKontejnera`, `InsertRNPrijemCirade`, `InsertRNPrijemPlatforme`, `InsertRNPrijemPlatforme2`, `InsertRNPrijemVoza`, `InsertRNPrijemVoza2`, `InsertRNPrijemVozaCeoVoz`, `InsertRNPrijemVozaCeoVozPretovar`, `PokreniSkladisninuKontejnera`, `PrenesiKontejnerTekuceUArhiv`, `UpdateIZCIRA`, `UpdateRN12MedjuskladisniSkladiste`, `UpdateRN12Uradjene`, `UpdateRN1Uradjene`, `UpdateRN1UradjeneCIR`, `UpdateRN1UradjeneS`, `UpdateRN1UradjeneVP`, `UpdateRN2Uradjene`, `UpdateRN3Uradjene`, `UpdateRN4PotrebanCIR`, `UpdateRN4PrivremenoSkladiste`, `UpdateRN4SkladisteIzCira`, `UpdateRN4Uradjene`, `UpdateRN4UradjeneCIR`, `UpdateRN4UradjenePostavljen`, `UpdateRN4UradjeneVizuelni`, `UpdateRN5PrijemSkladiste`, `UpdateRN5Uradjene`, `UpdateRN5UradjeneCIR`, `UpdateRN5UradjeneS`, `UpdateRN6OtpremaSkladiste`, `UpdateRN6PotrebanCIR`, `UpdateRN6Uradjene`, `UpdateRN6UradjeneVizuelni`, `UpdateRN7Uradjene`, `UpdateRN8Uradjene`, `UpdateRN8UradjeneCIR`, `UpdateRN9PretovarCIRADE`, `UpdateRN9Uradjen`, `UpdateRNMedjuskladisni`, `UpdateRNOtpremaCirade`, `UpdateRNOtpremaPlatforme`, `UpdateRNOtpremaPlatforme2`, `UpdateRNOtpremaVoza`, `UpdateRNPregledIpostavkaKontejnera`, `UpdateRNPregledIspraznjenogKontejnera`, `UpdateRNPrijemCirade`, `UpdateRNPrijemPlatforme`, `UpdateRNPrijemPlatforme2`, `UpdateRNPrijemVoza`, `UpdateRNPrijemVoza2`, `UpdateRNPrijemVozaSkladiste`, `ZatvoriSkladisninuKontejnera`, `updRN4PrPlatformeKamIzvozPusti`
- **Funkcionalnosti (kontrola → handler):**
  - „Datum rasporeda“ — klik → `label7_Click`
  - „ID i naziv planirane usluge“ — klik → `label10_Click`
  - „Datum realizacije“ — klik → `label9_Click`
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „FORMIRAJ RADNE NALOGE OTPREME VOZA“ — klik → `toolStripButton1_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „POTVRDI ZAVRŠEN / ARHIVIRAJ“ — klik → `toolStripButton2_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „POTVRDI ZAVRŠEN / ARHIVIRAJ“ — klik → `toolStripButton2_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „FORMIRAJ RADNE NALOGE “ — klik → `toolStripButton1_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - `button20` — klik → `tsDelete_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - `button21` — klik → `tsSave_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN

#### RN4PrijemPlatforme
- **Fajl:** `RadniNalozi/RN4PrijemPlatforme.cs`  
- **Naslov forme:** „RN4 - PRIJEM PLATFORME KAMIONOM“
- **Modul:** Intermodalni terminal, Kapija, Prijem i otprema vozova
- **Tabele:**
  - `InspekciskiTretman` — S
  - `Partnerji` — S
  - `Pozicija` — S
  - `RadniNalogInterni` — S
  - `RNPrijemPlatforme` — S
  - `Skladista` — S
  - `TipKontenjera` — S
  - `Voz` — S
  - `VrstaCarinskogPostupka` — S
  - `VrstaManipulacije` — S
- **Procedure:** `DeleteRNMedjuskladisni`, `DeleteRNOtpremaCirade`, `DeleteRNOtpremaPlatforme`, `DeleteRNOtpremaPlatforme2`, `DeleteRNOtpremaVoza`, `DeleteRNPregledIpostavkaKontejnera`, `DeleteRNPregledIspraznjenogKontejnera`, `DeleteRNPrijemCirade`, `DeleteRNPrijemPlatforme`, `DeleteRNPrijemPlatforme2`, `DeleteRNPrijemVoza`, `DeleteRNPrijemVoza2`, `InsertRN10PregledCeoVoz`, `InsertRN11PregledCeoVozOtprema`, `InsertRN12Medjuskladisni`, `InsertRN4PrPlatformeKamIzvoz`, `InsertRN4PrPlatformeKamUvoz`, `InsertRN5PrPlatFormeKam`, `InsertRN6OtPlatFormeKam`, `InsertRN7OtpremaOtpremaPlatKam`, `InsertRN8OtpremaCiradeKam`, `InsertRN8OtpremaCiradeKamIzvoz`, `InsertRN9PrijemCiradeKam`, `InsertRNOtpremaCirade`, `InsertRNOtpremaPlatforme`, `InsertRNOtpremaPlatforme2`, `InsertRNOtpremaVoza`, `InsertRNOtpremaVozaCeoVoz`, `InsertRNPregledIpostavkaKontejnera`, `InsertRNPregledIspraznjenogKontejnera`, `InsertRNPrijemCirade`, `InsertRNPrijemPlatforme`, `InsertRNPrijemPlatforme2`, `InsertRNPrijemVoza`, `InsertRNPrijemVoza2`, `InsertRNPrijemVozaCeoVoz`, `InsertRNPrijemVozaCeoVozPretovar`, `PokreniSkladisninuKontejnera`, `PrenesiKontejnerTekuceUArhiv`, `UpdateIZCIRA`, `UpdateRN12MedjuskladisniSkladiste`, `UpdateRN12Uradjene`, `UpdateRN1Uradjene`, `UpdateRN1UradjeneCIR`, `UpdateRN1UradjeneS`, `UpdateRN1UradjeneVP`, `UpdateRN2Uradjene`, `UpdateRN3Uradjene`, `UpdateRN4PotrebanCIR`, `UpdateRN4PrivremenoSkladiste`, `UpdateRN4SkladisteIzCira`, `UpdateRN4Uradjene`, `UpdateRN4UradjeneCIR`, `UpdateRN4UradjenePostavljen`, `UpdateRN4UradjeneVizuelni`, `UpdateRN5PrijemSkladiste`, `UpdateRN5Uradjene`, `UpdateRN5UradjeneCIR`, `UpdateRN5UradjeneS`, `UpdateRN6OtpremaSkladiste`, `UpdateRN6PotrebanCIR`, `UpdateRN6Uradjene`, `UpdateRN6UradjeneVizuelni`, `UpdateRN7Uradjene`, `UpdateRN8Uradjene`, `UpdateRN8UradjeneCIR`, `UpdateRN9PretovarCIRADE`, `UpdateRN9Uradjen`, `UpdateRNMedjuskladisni`, `UpdateRNOtpremaCirade`, `UpdateRNOtpremaPlatforme`, `UpdateRNOtpremaPlatforme2`, `UpdateRNOtpremaVoza`, `UpdateRNPregledIpostavkaKontejnera`, `UpdateRNPregledIspraznjenogKontejnera`, `UpdateRNPrijemCirade`, `UpdateRNPrijemPlatforme`, `UpdateRNPrijemPlatforme2`, `UpdateRNPrijemVoza`, `UpdateRNPrijemVoza2`, `UpdateRNPrijemVozaSkladiste`, `ZatvoriSkladisninuKontejnera`, `updRN4PrPlatformeKamIzvozPusti`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „FORMIRAJ RADNI NALOG PRIJEM PLATFORMA“ — klik → `toolStripButton1_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „Potvrdi završen vizuelni“ — klik → `toolStripButton3_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „Potvrdi završen CIR“ — klik → `toolStripButton4_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „Potvrdi premesten“ — klik → `toolStripButton2_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „Dodela polja“ — klik → `button2_Click` — otvara: frmDodelaSkladista
  - „POTVRDI ZAVRŠEN “ — klik → `toolStripButton2_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „POTVRDI ZAVRŠEN CIR“ — klik → `toolStripButton4_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „POTVRDI ZAVRŠEN VIZUELNI“ — klik → `toolStripButton3_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „FORMIRAJ RADNE NALOGE “ — klik → `toolStripButton1_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - `button20` — klik → `tsDelete_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - `button21` — klik → `tsSave_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN

#### RN5PrijemPlatforme2
- **Fajl:** `RadniNalozi/RN5PrijemPlatforme2.cs`  
- **Naslov forme:** „RN5 - PRIJEM PLATFORME BRODAR“
- **Modul:** Intermodalni terminal, Kapija, Prijem i otprema vozova
- **Tabele:**
  - `NHM` — S
  - `Partnerji` — S
  - `Pozicija` — S
  - `RadniNalogInterni` — S
  - `RNPrijemPlatforme2` — S
  - `Skladista` — S
  - `TipKontenjera` — S
  - `Voz` — S
  - `VrstaManipulacije` — S
- **Procedure:** `DeleteRNMedjuskladisni`, `DeleteRNOtpremaCirade`, `DeleteRNOtpremaPlatforme`, `DeleteRNOtpremaPlatforme2`, `DeleteRNOtpremaVoza`, `DeleteRNPregledIpostavkaKontejnera`, `DeleteRNPregledIspraznjenogKontejnera`, `DeleteRNPrijemCirade`, `DeleteRNPrijemPlatforme`, `DeleteRNPrijemPlatforme2`, `DeleteRNPrijemVoza`, `DeleteRNPrijemVoza2`, `InsertRN10PregledCeoVoz`, `InsertRN11PregledCeoVozOtprema`, `InsertRN12Medjuskladisni`, `InsertRN4PrPlatformeKamIzvoz`, `InsertRN4PrPlatformeKamUvoz`, `InsertRN5PrPlatFormeKam`, `InsertRN6OtPlatFormeKam`, `InsertRN7OtpremaOtpremaPlatKam`, `InsertRN8OtpremaCiradeKam`, `InsertRN8OtpremaCiradeKamIzvoz`, `InsertRN9PrijemCiradeKam`, `InsertRNOtpremaCirade`, `InsertRNOtpremaPlatforme`, `InsertRNOtpremaPlatforme2`, `InsertRNOtpremaVoza`, `InsertRNOtpremaVozaCeoVoz`, `InsertRNPregledIpostavkaKontejnera`, `InsertRNPregledIspraznjenogKontejnera`, `InsertRNPrijemCirade`, `InsertRNPrijemPlatforme`, `InsertRNPrijemPlatforme2`, `InsertRNPrijemVoza`, `InsertRNPrijemVoza2`, `InsertRNPrijemVozaCeoVoz`, `InsertRNPrijemVozaCeoVozPretovar`, `PokreniSkladisninuKontejnera`, `PrenesiKontejnerTekuceUArhiv`, `UpdateIZCIRA`, `UpdateRN12MedjuskladisniSkladiste`, `UpdateRN12Uradjene`, `UpdateRN1Uradjene`, `UpdateRN1UradjeneCIR`, `UpdateRN1UradjeneS`, `UpdateRN1UradjeneVP`, `UpdateRN2Uradjene`, `UpdateRN3Uradjene`, `UpdateRN4PotrebanCIR`, `UpdateRN4PrivremenoSkladiste`, `UpdateRN4SkladisteIzCira`, `UpdateRN4Uradjene`, `UpdateRN4UradjeneCIR`, `UpdateRN4UradjenePostavljen`, `UpdateRN4UradjeneVizuelni`, `UpdateRN5PrijemSkladiste`, `UpdateRN5Uradjene`, `UpdateRN5UradjeneCIR`, `UpdateRN5UradjeneS`, `UpdateRN6OtpremaSkladiste`, `UpdateRN6PotrebanCIR`, `UpdateRN6Uradjene`, `UpdateRN6UradjeneVizuelni`, `UpdateRN7Uradjene`, `UpdateRN8Uradjene`, `UpdateRN8UradjeneCIR`, `UpdateRN9PretovarCIRADE`, `UpdateRN9Uradjen`, `UpdateRNMedjuskladisni`, `UpdateRNOtpremaCirade`, `UpdateRNOtpremaPlatforme`, `UpdateRNOtpremaPlatforme2`, `UpdateRNOtpremaVoza`, `UpdateRNPregledIpostavkaKontejnera`, `UpdateRNPregledIspraznjenogKontejnera`, `UpdateRNPrijemCirade`, `UpdateRNPrijemPlatforme`, `UpdateRNPrijemPlatforme2`, `UpdateRNPrijemVoza`, `UpdateRNPrijemVoza2`, `UpdateRNPrijemVozaSkladiste`, `ZatvoriSkladisninuKontejnera`, `updRN4PrPlatformeKamIzvozPusti`
- **Funkcionalnosti (kontrola → handler):**
  - „Datum rasporeda“ — klik → `label1_Click`
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „FORMIRAJ RADNI NALOG PLATFORMA BRODAR“ — klik → `toolStripButton1_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „Ubaci u tekuce - NE TREBA“ — klik → `toolStripButton4_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „Potvrdi završen CIR“ — klik → `toolStripButton3_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „Potvrdi završen“ — klik → `toolStripButton2_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „ID“ — klik → `label1_Click`
  - „POTVRDI ZAVRŠEN “ — klik → `toolStripButton2_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „POTVRDI ZAVRŠEN CIR“ — klik → `toolStripButton3_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „FORMIRAJ RADNE NALOGE “ — klik → `toolStripButton1_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - `button20` — klik → `tsDelete_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - `button21` — klik → `tsSave_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN

#### RN6OtpremaPlatforme
- **Fajl:** `RadniNalozi/RN6OtpremaPlatforme.cs`  
- **Naslov forme:** „RN6 OTPREMA PLATFORME“
- **Modul:** Intermodalni terminal, Kapija, Prijem i otprema vozova
- **Tabele:**
  - `KontejnerTekuce` — S
  - `NHM` — S
  - `Partnerji` — S
  - `Pozicija` — S
  - `RadniNalogInterni` — S
  - `RNOtpremaPlatforme` — S
  - `Skladista` — S
  - `TipKontenjera` — S
  - `UvozKonacnaNHM` — S
  - `Voz` — S
  - `VrstaCarinskogPostupka` — S
  - `VrstaManipulacije` — S
- **Procedure:** `DeleteRNMedjuskladisni`, `DeleteRNOtpremaCirade`, `DeleteRNOtpremaPlatforme`, `DeleteRNOtpremaPlatforme2`, `DeleteRNOtpremaVoza`, `DeleteRNPregledIpostavkaKontejnera`, `DeleteRNPregledIspraznjenogKontejnera`, `DeleteRNPrijemCirade`, `DeleteRNPrijemPlatforme`, `DeleteRNPrijemPlatforme2`, `DeleteRNPrijemVoza`, `DeleteRNPrijemVoza2`, `InsertRN10PregledCeoVoz`, `InsertRN11PregledCeoVozOtprema`, `InsertRN12Medjuskladisni`, `InsertRN4PrPlatformeKamIzvoz`, `InsertRN4PrPlatformeKamUvoz`, `InsertRN5PrPlatFormeKam`, `InsertRN6OtPlatFormeKam`, `InsertRN7OtpremaOtpremaPlatKam`, `InsertRN8OtpremaCiradeKam`, `InsertRN8OtpremaCiradeKamIzvoz`, `InsertRN9PrijemCiradeKam`, `InsertRNOtpremaCirade`, `InsertRNOtpremaPlatforme`, `InsertRNOtpremaPlatforme2`, `InsertRNOtpremaVoza`, `InsertRNOtpremaVozaCeoVoz`, `InsertRNPregledIpostavkaKontejnera`, `InsertRNPregledIspraznjenogKontejnera`, `InsertRNPrijemCirade`, `InsertRNPrijemPlatforme`, `InsertRNPrijemPlatforme2`, `InsertRNPrijemVoza`, `InsertRNPrijemVoza2`, `InsertRNPrijemVozaCeoVoz`, `InsertRNPrijemVozaCeoVozPretovar`, `PokreniSkladisninuKontejnera`, `PrenesiKontejnerTekuceUArhiv`, `UpdateIZCIRA`, `UpdateRN12MedjuskladisniSkladiste`, `UpdateRN12Uradjene`, `UpdateRN1Uradjene`, `UpdateRN1UradjeneCIR`, `UpdateRN1UradjeneS`, `UpdateRN1UradjeneVP`, `UpdateRN2Uradjene`, `UpdateRN3Uradjene`, `UpdateRN4PotrebanCIR`, `UpdateRN4PrivremenoSkladiste`, `UpdateRN4SkladisteIzCira`, `UpdateRN4Uradjene`, `UpdateRN4UradjeneCIR`, `UpdateRN4UradjenePostavljen`, `UpdateRN4UradjeneVizuelni`, `UpdateRN5PrijemSkladiste`, `UpdateRN5Uradjene`, `UpdateRN5UradjeneCIR`, `UpdateRN5UradjeneS`, `UpdateRN6OtpremaSkladiste`, `UpdateRN6PotrebanCIR`, `UpdateRN6Uradjene`, `UpdateRN6UradjeneVizuelni`, `UpdateRN7Uradjene`, `UpdateRN8Uradjene`, `UpdateRN8UradjeneCIR`, `UpdateRN9PretovarCIRADE`, `UpdateRN9Uradjen`, `UpdateRNMedjuskladisni`, `UpdateRNOtpremaCirade`, `UpdateRNOtpremaPlatforme`, `UpdateRNOtpremaPlatforme2`, `UpdateRNOtpremaVoza`, `UpdateRNPregledIpostavkaKontejnera`, `UpdateRNPregledIspraznjenogKontejnera`, `UpdateRNPrijemCirade`, `UpdateRNPrijemPlatforme`, `UpdateRNPrijemPlatforme2`, `UpdateRNPrijemVoza`, `UpdateRNPrijemVoza2`, `UpdateRNPrijemVozaSkladiste`, `ZatvoriSkladisninuKontejnera`, `updRN4PrPlatformeKamIzvozPusti`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „FORMIRAJ RADNI NALOG OTPREMA PLATFORME“ — klik → `toolStripButton1_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „Potvrdi završen“ — klik → `toolStripButton2_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „Dodela polja“ — klik → `button2_Click_1`
  - „POTVRDI ZAVRŠEN “ — klik → `toolStripButton2_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „FORMIRAJ RADNE NALOGE “ — klik → `toolStripButton1_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - `button20` — klik → `tsDelete_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - `button21` — klik → `tsSave_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN

#### RN7OtpremaPlatforme2
- **Fajl:** `RadniNalozi/RN7OtpremaPlatforme2.cs`  
- **Naslov forme:** „RN7 - OTPREMA PLATFORME“
- **Modul:** Intermodalni terminal, Kapija, Prijem i otprema vozova
- **Tabele:**
  - `KontejnerTekuce` — S
  - `NHM` — S
  - `Partnerji` — S
  - `Pozicija` — S
  - `RadniNalogInterni` — S
  - `RNOtpremaPlatforme2` — S
  - `Skladista` — S
  - `TipKontenjera` — S
  - `Voz` — S
  - `VrstaManipulacije` — S
- **Procedure:** `DeleteRNMedjuskladisni`, `DeleteRNOtpremaCirade`, `DeleteRNOtpremaPlatforme`, `DeleteRNOtpremaPlatforme2`, `DeleteRNOtpremaVoza`, `DeleteRNPregledIpostavkaKontejnera`, `DeleteRNPregledIspraznjenogKontejnera`, `DeleteRNPrijemCirade`, `DeleteRNPrijemPlatforme`, `DeleteRNPrijemPlatforme2`, `DeleteRNPrijemVoza`, `DeleteRNPrijemVoza2`, `InsertRN10PregledCeoVoz`, `InsertRN11PregledCeoVozOtprema`, `InsertRN12Medjuskladisni`, `InsertRN4PrPlatformeKamIzvoz`, `InsertRN4PrPlatformeKamUvoz`, `InsertRN5PrPlatFormeKam`, `InsertRN6OtPlatFormeKam`, `InsertRN7OtpremaOtpremaPlatKam`, `InsertRN8OtpremaCiradeKam`, `InsertRN8OtpremaCiradeKamIzvoz`, `InsertRN9PrijemCiradeKam`, `InsertRNOtpremaCirade`, `InsertRNOtpremaPlatforme`, `InsertRNOtpremaPlatforme2`, `InsertRNOtpremaVoza`, `InsertRNOtpremaVozaCeoVoz`, `InsertRNPregledIpostavkaKontejnera`, `InsertRNPregledIspraznjenogKontejnera`, `InsertRNPrijemCirade`, `InsertRNPrijemPlatforme`, `InsertRNPrijemPlatforme2`, `InsertRNPrijemVoza`, `InsertRNPrijemVoza2`, `InsertRNPrijemVozaCeoVoz`, `InsertRNPrijemVozaCeoVozPretovar`, `PokreniSkladisninuKontejnera`, `PrenesiKontejnerTekuceUArhiv`, `UpdateIZCIRA`, `UpdateRN12MedjuskladisniSkladiste`, `UpdateRN12Uradjene`, `UpdateRN1Uradjene`, `UpdateRN1UradjeneCIR`, `UpdateRN1UradjeneS`, `UpdateRN1UradjeneVP`, `UpdateRN2Uradjene`, `UpdateRN3Uradjene`, `UpdateRN4PotrebanCIR`, `UpdateRN4PrivremenoSkladiste`, `UpdateRN4SkladisteIzCira`, `UpdateRN4Uradjene`, `UpdateRN4UradjeneCIR`, `UpdateRN4UradjenePostavljen`, `UpdateRN4UradjeneVizuelni`, `UpdateRN5PrijemSkladiste`, `UpdateRN5Uradjene`, `UpdateRN5UradjeneCIR`, `UpdateRN5UradjeneS`, `UpdateRN6OtpremaSkladiste`, `UpdateRN6PotrebanCIR`, `UpdateRN6Uradjene`, `UpdateRN6UradjeneVizuelni`, `UpdateRN7Uradjene`, `UpdateRN8Uradjene`, `UpdateRN8UradjeneCIR`, `UpdateRN9PretovarCIRADE`, `UpdateRN9Uradjen`, `UpdateRNMedjuskladisni`, `UpdateRNOtpremaCirade`, `UpdateRNOtpremaPlatforme`, `UpdateRNOtpremaPlatforme2`, `UpdateRNOtpremaVoza`, `UpdateRNPregledIpostavkaKontejnera`, `UpdateRNPregledIspraznjenogKontejnera`, `UpdateRNPrijemCirade`, `UpdateRNPrijemPlatforme`, `UpdateRNPrijemPlatforme2`, `UpdateRNPrijemVoza`, `UpdateRNPrijemVoza2`, `UpdateRNPrijemVozaSkladiste`, `ZatvoriSkladisninuKontejnera`, `updRN4PrPlatformeKamIzvozPusti`
- **Funkcionalnosti (kontrola → handler):**
  - „Naziv brodara“ — klik → `txtnazivbrodara_Click`
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „FORMIRAJ OTPREMA PLATFORME - BRODAR“ — klik → `toolStripButton1_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „Potvrdi završen“ — klik → `toolStripButton2_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „ARHIVIRAJ KONTEJNER“ — klik → `toolStripButton3_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „Kamion“ — klik → `label2_Click`
  - „Otprema ID“ — klik → `label5_Click`
  - „ARHIVIRAJ KONTEJNER“ — klik → `toolStripButton3_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „POTVRDI ZAVRŠEN “ — klik → `toolStripButton2_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „FORMIRAJ RADNE NALOGE “ — klik → `toolStripButton1_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - `button20` — klik → `tsDelete_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - `button21` — klik → `tsSave_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN

#### VozniPlanovi
- **Fajl:** `MainLeget/Intermodalni/VozniPlanovi.cs`  
- **Naslov forme:** „VozniPlanovi“

#### ZapisnikONepravilnosti
- **Fajl:** `Uvoz/ZapisnikONepravilnosti.cs`  
- **Naslov forme:** „Zapisnik o nepravilnosti“
- **Modul:** Intermodalni terminal, Prijem i otprema kamiona, Prijem i otprema vozova
- **Tabele:**
  - `TipKontenjera` — S
  - `ZapisnikONepravilnosti` — S
- **Procedure:** `DeleteZapisnikONepravilnosti`, `InsertZapisnikONepravilnosti`
- **Funkcionalnosti (kontrola → handler):**
  - „Štampaj“ — klik → `button23_Click` — izveštaj: rptZapisnikONepravilnosti.rdlc
  - `button20` — klik → `button20_Click` — proc: DeleteZapisnikONepravilnosti, InsertZapisnikONepravilnosti; preko: InsertZapisnikONepravilnosti
  - `button21` — klik → `button21_Click` — proc: DeleteZapisnikONepravilnosti, InsertZapisnikONepravilnosti; preko: InsertZapisnikONepravilnosti

### Logistika izvoza (12)

#### frmCarinskiPostupak
- **Fajl:** `Izvoz/frmCarinskiPostupak.cs`  
- **Naslov forme:** „Carinski postupak“
- **Otvara forme:** frmKontaktOsobe (`btnPolazna` (Click))
- **Tabele:**
  - `Carinarnice` — S
  - `Izvoz` — S
  - `Partnerji` — S
  - `VrstaCarinskogPostupka` — S
- **Procedure:** `DeleteIzvozKonacnaNHM`, `DeleteIzvozKonacnaVrstaManipulacije`, `DeleteIzvozKonacnaVrstaRobeHS`, `DeleteIzvozNHM`, `DeleteIzvozSve`, `DeleteIzvozVrstaManipulacije`, `DeleteIzvozVrstaRobeHS`, `DeleteKontejnerIzvoz`, `DeleteVaganje`, `InsertIzvozKonacnaNHM`, `InsertIzvozKonacnaVMIzUvoza`, `InsertIzvozKonacnaVrstaManipulacije`, `InsertIzvozNHM`, `InsertIzvozPorudzbenica`, `InsertIzvozVMIzUvoza`, `InsertIzvozVrstaManipulacije`, `InsertIzvozVrstaRobeHS`, `InsertIzvozkonacnaVrstaRobeHS`, `InsertKontejnerLog`, `InsertVaganje`, `KopirajKontejnerIzvoz`, `UpdCenaIzvozKonacnaVrstaManipulacije`, `UpdCenaIzvozVrstaManipulacije`, `UpdPDVIzvozKonacnaVrstaManipulacije`, `UpdPDVIzvozVrstaManipulacije`, `UpdPlatiocaIzvozKonacnaVrstaManipulacije`, `UpdPlatiocaIzvozVrstaManipulacije`, `UpdateIzvoz`, `UpdateIzvozKonacna`, `UpdateIzvozKonacnaVaganjeCirada`, `UpdateIzvozKonacnaVaganjePlatforma`, `UpdateIzvozPorudzbenica`, `UpdateIzvozPorudzbenicaCarina`, `UpdateIzvozPorudzbenicaPojedinacni`, `UpdateIzvozPorudzbenicaRelacija`, `UpdateIzvozStatus`, `UpdateIzvozStatusTerminal`, `UpdateIzvozTerminalIzbor`, `UpdateVaganje`, `spPrenesiKontejnerIzNerasporedjenihUPrijPlatforma`
- **Funkcionalnosti (kontrola → handler):**
  - `button21` — klik → `button21_Click` — proc: DeleteIzvozKonacnaNHM, DeleteIzvozKonacnaVrstaManipulacije, DeleteIzvozKonacnaVrstaRobeHS, DeleteIzvozNHM, DeleteIzvozSve, DeleteIzvozVrstaManipulacije; preko: InsertIzvoz

#### frmGrupniUnosPoljaIzvoz
- **Fajl:** `Izvoz/frmGrupniUnosPoljaIzvoz.cs`  
- **Naslov forme:** „Nalog za otpremu“
- **Otvara forme:** frmCarinskiPostupak („Carinski postupak“), frmIzvozPregledKontejneraDrumskeUsluge („Pusti Drumski uslugu“), frmIzvozUnosManipulacije („Dodaj usluge“), frmRelacija („Relacija“)
- **Tabele:**
  - `InspekciskiTretman` — S
  - `Izvoz` — S
  - `IzvozKonacna` — S
  - `IzvozNapomenePozicioniranja` — S
  - `IzvozNHM` — S
  - `IzvozVrstaManipulacije` — S
  - `KontejnerskiTerminali` — S
  - `MestaUtovara` — S
  - `NapomenaZaPozicioniranje` — S
  - `NHM` — S
  - `Partnerji` — S
  - `ProdajniNalogIzvoz` — S
  - `ProdajniNalogIzvozStavke` — S
  - `Scenario` — S
  - `TipKontenjera` — S
  - `uvKvalitetKontejnera` — S
  - `uvNacinPakovanja` — S
  - `VrstaManipulacije` — S
  - `VrstaRobeADR` — S
- **Procedure:** `DeleteIzvozKonacnaNHM`, `DeleteIzvozKonacnaNapomenePozicioniranja`, `DeleteIzvozKonacnaSve`, `DeleteIzvozKonacnaVrstaManipulacije`, `DeleteIzvozKonacnaVrstaRobeHS`, `DeleteIzvozNHM`, `DeleteIzvozNapomenePozicioniranja`, `DeleteIzvozSve`, `DeleteIzvozUslugaDokumenta`, `DeleteIzvozVrstaManTerminalskih`, `DeleteIzvozVrstaManipulacije`, `DeleteIzvozVrstaRobeHS`, `DeleteKontejnerIzvoz`, `DeleteVaganje`, `InsertIzvozKonacnaNHM`, `InsertIzvozKonacnaNapomenePozicioniranja`, `InsertIzvozKonacnaVMIzUvoza`, `InsertIzvozKonacnaVrstaManipulacije`, `InsertIzvozNHM`, `InsertIzvozNapomenePozicioniranja`, `InsertIzvozPorudzbenica`, `InsertIzvozUslugaDokument`, `InsertIzvozVMIzUvoza`, `InsertIzvozVrstaManipulacije`, `InsertIzvozVrstaRobeHS`, `InsertIzvozkonacnaVrstaRobeHS`, `InsertKontejnerLog`, `InsertVaganje`, `KopirajKontejnerIzvoz`, `OdrediKontejnerTerminal`, `PrenesiIzPlanIzvozaUPlanIzvoza`, `PrenesiUPlanUtovaraSelektovanoIzvoz`, `PrenesiUPlanUtovaraSelektovanoIzvozBrodar`, `PrenesiUPlanUtovaraSelektovanoIzvozTErminal`, `UpdCenaIzvozKonacnaVrstaManipulacije`, `UpdCenaIzvozVrstaManipulacije`, `UpdPDVIzvozKonacnaVrstaManipulacije`, `UpdPDVIzvozVrstaManipulacije`, `UpdPlatiocaIzvozKonacnaVrstaManipulacije`, `UpdPlatiocaIzvozVrstaManipulacije`, `UpdateIzvoz`, `UpdateIzvozKonacna`, `UpdateIzvozKonacnaVaganjeCirada`, `UpdateIzvozKonacnaVaganjePlatforma`, `UpdateIzvozKonacnaVagon`, `UpdateIzvozPorudzbenica`, `UpdateIzvozPorudzbenicaCarina`, `UpdateIzvozPorudzbenicaPojedinacni`, `UpdateIzvozPorudzbenicaRelacija`, `UpdateIzvozStatus`, `UpdateIzvozStatusTerminal`, `UpdateIzvozTerminalIzbor`, `UpdateVaganje`, `VratiKontejnerNerasporedjenoSelektovanoIzvoz`, `spPrenesiIzPlanUtovaraUOtpremaVoz`, `spPrenesiIzUvozaUIzvoz`, `spPrenesiKontejnerIzNerasporedjenihUPrijPlatforma`, `spPrenesiKontejnerUOtpremuKamionom`, `spPrenesiKontejnerUOtpremuKamionomIzvoz`
- **Funkcionalnosti (kontrola → handler):**
  - „Formiraj KN II Faza“ — klik → `button4_Click`
  - „Formiraj KN I Faza“ — klik → `button2_Click` — proc: DeleteIzvozKonacnaNapomenePozicioniranja, DeleteIzvozKonacnaSve, DeleteIzvozNapomenePozicioniranja, DeleteIzvozUslugaDokumenta, DeleteIzvozVrstaManTerminalskih, InsertIzvozKonacnaNapomenePozicioniranja; preko: InsertIzvozKonacna
  - „Dodaj usluge“ — klik → `btnUsluge_Click` — otvara: frmIzvozUnosManipulacije; čita: IzvozVrstaManipulacije; proc: DeleteIzvozKonacnaNHM, DeleteIzvozKonacnaVrstaManipulacije, DeleteIzvozKonacnaVrstaRobeHS, DeleteIzvozNHM, DeleteIzvozSve, DeleteIzvozVrstaManipulacije; preko: InsertIzvoz
  - „Carinski postupak“ — klik → `button1_Click` — otvara: frmCarinskiPostupak
  - „Relacija“ — klik → `button27_Click` — otvara: frmRelacija
  - `btnSave` — klik → `btnSave_Click` — proc: DeleteIzvozKonacnaNHM, DeleteIzvozKonacnaNapomenePozicioniranja, DeleteIzvozKonacnaSve, DeleteIzvozKonacnaVrstaManipulacije, DeleteIzvozKonacnaVrstaRobeHS, DeleteIzvozNHM; preko: InsertIzvoz, InsertIzvozKonacna
  - „Unesi“ — klik → `btnUnesiNHM_Click` — čita 19 tabela; preko: PrivremeniNHM
  - „Unesi“ — klik → `button3_Click` — čita 19 tabela; preko: PrivremeniNapomena
  - „Pusti Drumski uslugu“ — klik → `button5_Click` — otvara: frmIzvozPregledKontejneraDrumskeUsluge

#### frmLogTokaProcesa
- **Fajl:** `Izvoz/frmLogTokaProcesa.cs`  
- **Naslov forme:** „frmLogTokaProcesa“
- **Tabele:**
  - `KontejnerLog` — S

#### frmPregledGrupaKontejneraIzvoz
- **Fajl:** `Izvoz/frmPregledGrupaKontejneraIzvoz.cs`  
- **Naslov forme:** „frmPregledGrupaKontejneraIzvoz“
- **Otvara forme:** frmLogTokaProcesa („Otvori“)
- **Tabele:**
  - `Izvoz` — S
  - `IzvozKonacna` — S
  - `KontejnerLog` — S
  - `Partnerji` — S
  - `ProdajniNalogIzvozStavke` — S
  - `TipKontenjera` — S
  - `uvNacinPakovanja` — S
  - `VrstaRobeADR` — S
- **Procedure:** `DeleteIzvozKonacnaNHM`, `DeleteIzvozKonacnaVrstaManipulacije`, `DeleteIzvozKonacnaVrstaRobeHS`, `DeleteIzvozNHM`, `DeleteIzvozSve`, `DeleteIzvozVrstaManipulacije`, `DeleteIzvozVrstaRobeHS`, `DeleteKontejnerIzvoz`, `DeleteVaganje`, `InsertIzvozKonacnaNHM`, `InsertIzvozKonacnaVMIzUvoza`, `InsertIzvozKonacnaVrstaManipulacije`, `InsertIzvozNHM`, `InsertIzvozPorudzbenica`, `InsertIzvozVMIzUvoza`, `InsertIzvozVrstaManipulacije`, `InsertIzvozVrstaRobeHS`, `InsertIzvozkonacnaVrstaRobeHS`, `InsertKontejnerLog`, `InsertVaganje`, `KopirajKontejnerIzvoz`, `UpdCenaIzvozKonacnaVrstaManipulacije`, `UpdCenaIzvozVrstaManipulacije`, `UpdPDVIzvozKonacnaVrstaManipulacije`, `UpdPDVIzvozVrstaManipulacije`, `UpdPlatiocaIzvozKonacnaVrstaManipulacije`, `UpdPlatiocaIzvozVrstaManipulacije`, `UpdateIzvoz`, `UpdateIzvozKonacna`, `UpdateIzvozKonacnaVaganjeCirada`, `UpdateIzvozKonacnaVaganjePlatforma`, `UpdateIzvozPorudzbenica`, `UpdateIzvozPorudzbenicaCarina`, `UpdateIzvozPorudzbenicaPojedinacni`, `UpdateIzvozPorudzbenicaRelacija`, `UpdateIzvozStatus`, `UpdateIzvozStatusTerminal`, `UpdateIzvozTerminalIzbor`, `UpdateVaganje`, `spPrenesiKontejnerIzNerasporedjenihUPrijPlatforma`
- **Funkcionalnosti (kontrola → handler):**
  - „Izmeni količinu stavke porudžbenice“ — klik → `btnIzmeniKolicinu_Click`
  - „Otvori“ — klik → `btnOtvori_Click` — otvara: frmLogTokaProcesa, frmGrupniUnosPoljaIzvoz
  - „gridGroupingControl1“ — TableControlMouseDown → `gridGroupingControl1_TableControlMouseDown`
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick_1` — proc: DeleteIzvozKonacnaNHM, DeleteIzvozKonacnaVrstaManipulacije, DeleteIzvozKonacnaVrstaRobeHS, DeleteIzvozNHM, DeleteIzvozSve, DeleteIzvozVrstaManipulacije; preko: InsertIzvoz
  - „Otvori“ — klik → `toolStripMenuItemOtvori_Click` — otvara: frmLogTokaProcesa, frmGrupniUnosPoljaIzvoz

#### frmPregledPorudzbenica
- **Fajl:** `Izvoz/frmPreglegPorudzbenica.cs`  
- **Naslov forme:** „frmPregledPorudzbenica“
- **Tabele:**
  - `Partnerji` — S
  - `ProdajniNalogIzvoz` — S
  - `ProdajniNalogIzvozStavke` — S
- **Procedure:** `DeleteProdajniNalogIzvoz`, `DeleteValute`, `InsertProdajniNalogIzvoz`, `InsertProdajniNalogIzvozStavke`, `UpdateProdajniNalogIzvoz`, `UpdateProdajniNalogIzvozStavke`, `UpdateProdajniNalogStavkeDrumski`, `UpdateProdajniNalogStavkeKolicina`, `UpdateProdajniNalogStavkeKolicinaVecFormirano`, `UpdateProdajniNalogStavkeScenario`, `UpdateProdajniNalogStavkeStorno`
- **Funkcionalnosti (kontrola → handler):**
  - „gridGroupingControl1“ — TableControlMouseDown → `gridGroupingControl1_TableControlMouseDown`
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick_1` — proc: DeleteProdajniNalogIzvoz, DeleteValute, InsertProdajniNalogIzvoz, InsertProdajniNalogIzvozStavke, UpdateProdajniNalogIzvoz, UpdateProdajniNalogIzvozStavke; preko: InsertProdajniNalogIzvoz
  - „Otvori“ — klik → `btnOtvori_Click` — otvara: frmProdajniNalogIzvoz

#### frmProdajniNalogIzvoz
- **Fajl:** `Izvoz/frmProdajniNalogIzvoz.cs`  
- **Naslov forme:** „Porudžbenica“
- **Tabele:**
  - `MerskeEnote` — S
  - `Partnerji` — S
  - `ProdajniNalogIzvoz` — S
  - `ProdajniNalogIzvozStavke` — S
  - `TipKontenjera` — S
  - `uvKvalitetKontejnera` — S
- **Procedure:** `DeleteProdajniNalogIzvoz`, `DeleteValute`, `InsertKontejnerTekuce`, `InsertProdajniNalogIzvoz`, `InsertProdajniNalogIzvozStavke`, `UpdateKontejnerTekuce`, `UpdateProdajniNalogIzvoz`, `UpdateProdajniNalogIzvozStavke`, `UpdateProdajniNalogStavkeDrumski`, `UpdateProdajniNalogStavkeKolicina`, `UpdateProdajniNalogStavkeKolicinaVecFormirano`, `UpdateProdajniNalogStavkeScenario`, `UpdateProdajniNalogStavkeStorno`
- **Funkcionalnosti (kontrola → handler):**
  - `button21` — klik → `button21_Click` — proc: DeleteProdajniNalogIzvoz, DeleteValute, InsertProdajniNalogIzvoz, InsertProdajniNalogIzvozStavke, UpdateProdajniNalogIzvoz, UpdateProdajniNalogIzvozStavke; preko: InsertProdajniNalogIzvoz
  - „Izmeni količinu stavke porudžbenice“ — klik → `btnIzmeniKolicinu_Click` — otvara: frmProdajniNalogIzvozTabela

#### frmProdajniNalogIzvozTabela
- **Fajl:** `Izvoz/frmProdajniNalogIzvozTabela.cs`  
- **Naslov forme:** „Prodajni nalog izvoz tabela - otvorene stavke“
- **Otvara forme:** frmGrupniUnosPoljaIzvoz (`gridGroupingControl1` (TableControlCellClick)), NalogIzvozaZaOtpremu (`gridGroupingControl1` (TableControlCellClick))
- **Tabele:**
  - `PArtnerji` — S
  - `ProdajniNalogIzvoz` — S
  - `ProdajniNalogIzvozStavke` — S
  - `Scenario` — S
  - `TipKontenjera` — S
  - `uvKvalitetKontejnera` — S
- **Procedure:** `DeleteProdajniNalogIzvoz`, `DeleteValute`, `InsertProdajniNalogIzvoz`, `InsertProdajniNalogIzvozStavke`, `UpdateProdajniNalogIzvoz`, `UpdateProdajniNalogIzvozStavke`, `UpdateProdajniNalogStavkeDrumski`, `UpdateProdajniNalogStavkeKolicina`, `UpdateProdajniNalogStavkeKolicinaVecFormirano`, `UpdateProdajniNalogStavkeScenario`, `UpdateProdajniNalogStavkeStorno`
- **Funkcionalnosti (kontrola → handler):**
  - „gridGroupingControl1“ — TableControlCurrentCellEditingComplete → `gridGroupingControl1_TableControlCurrentCellEditingComplete` — proc: DeleteProdajniNalogIzvoz, DeleteValute, InsertProdajniNalogIzvoz, InsertProdajniNalogIzvozStavke, UpdateProdajniNalogIzvoz, UpdateProdajniNalogIzvozStavke; preko: InsertProdajniNalogIzvoz
  - „gridGroupingControl1“ — TableControlCurrentCellChanging → `gridGroupingControl1_TableControlCurrentCellChanging`
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick` — otvara: NalogIzvozaZaOtpremu, frmGrupniUnosPoljaIzvoz; proc: DeleteProdajniNalogIzvoz, DeleteValute, InsertProdajniNalogIzvoz, InsertProdajniNalogIzvozStavke, UpdateProdajniNalogIzvoz, UpdateProdajniNalogIzvozStavke; preko: InsertProdajniNalogIzvoz

#### frmRelacija
- **Fajl:** `Izvoz/frmRelacija.cs`  
- **Naslov forme:** „Relacija“
- **Tabele:**
  - `Izvoz` — S
  - `KontejnerskiTerminali` — S
  - `MestaUtovara` — S
  - `partnerjiKontOsebaMU` — S
  - `VrstaVozila` — S
- **Procedure:** `DeleteIzvozKonacnaNHM`, `DeleteIzvozKonacnaVrstaManipulacije`, `DeleteIzvozKonacnaVrstaRobeHS`, `DeleteIzvozNHM`, `DeleteIzvozSve`, `DeleteIzvozVrstaManipulacije`, `DeleteIzvozVrstaRobeHS`, `DeleteKontejnerIzvoz`, `DeleteVaganje`, `InsertIzvozKonacnaNHM`, `InsertIzvozKonacnaVMIzUvoza`, `InsertIzvozKonacnaVrstaManipulacije`, `InsertIzvozNHM`, `InsertIzvozPorudzbenica`, `InsertIzvozVMIzUvoza`, `InsertIzvozVrstaManipulacije`, `InsertIzvozVrstaRobeHS`, `InsertIzvozkonacnaVrstaRobeHS`, `InsertKontejnerLog`, `InsertVaganje`, `KopirajKontejnerIzvoz`, `UpdCenaIzvozKonacnaVrstaManipulacije`, `UpdCenaIzvozVrstaManipulacije`, `UpdPDVIzvozKonacnaVrstaManipulacije`, `UpdPDVIzvozVrstaManipulacije`, `UpdPlatiocaIzvozKonacnaVrstaManipulacije`, `UpdPlatiocaIzvozVrstaManipulacije`, `UpdateIzvoz`, `UpdateIzvozKonacna`, `UpdateIzvozKonacnaVaganjeCirada`, `UpdateIzvozKonacnaVaganjePlatforma`, `UpdateIzvozPorudzbenica`, `UpdateIzvozPorudzbenicaCarina`, `UpdateIzvozPorudzbenicaPojedinacni`, `UpdateIzvozPorudzbenicaRelacija`, `UpdateIzvozStatus`, `UpdateIzvozStatusTerminal`, `UpdateIzvozTerminalIzbor`, `UpdateVaganje`, `spPrenesiKontejnerIzNerasporedjenihUPrijPlatforma`
- **Funkcionalnosti (kontrola → handler):**
  - `button21` — klik → `button21_Click` — proc: DeleteIzvozKonacnaNHM, DeleteIzvozKonacnaVrstaManipulacije, DeleteIzvozKonacnaVrstaRobeHS, DeleteIzvozNHM, DeleteIzvozSve, DeleteIzvozVrstaManipulacije; preko: InsertIzvoz

#### LogistikaIzvoza0
- **Fajl:** `MainLeget/Izvoz/LogistikaIzvoza0.cs`  
- **Naslov forme:** „LogistikaIzvoza0“
- **Otvara forme:** frmPregledPorudzbenica („Izmeni postojeći“), frmProdajniNalogIzvoz („Kreiraj novi“)
- **Funkcionalnosti (kontrola → handler):**
  - „Kreiraj novi“ — klik → `sfButton1_Click`
  - „Izmeni postojeći“ — klik → `sfButton2_Click`
  - „Otkaži postojeći“ — klik → `sfButton3_Click`

#### LogistikaIzvoza1
- **Fajl:** `MainLeget/Izvoz/LogistikaIzvoza1.cs`  
- **Naslov forme:** „MainLeget“
- **Otvara forme:** LogistikaIzvoza0 („Porudžbenica“), LogistikaIzvoza2 („Formiranje naloga za otpremu“)
- **Funkcionalnosti (kontrola → handler):**
  - „Formiranje naloga za otpremu“ — klik → `btnLogistikaIzvozaPM2_Click` — otvara: LogistikaIzvoza2
  - „Nalog za fakturisanje“ — klik → `btnLogistikaIzvozaPM6_Click`
  - „Formiranje praznog voza“ — klik → `btnLogistikaIzvozaPM5_Click`
  - „Izvoz prazne opreme“ — klik → `btnLogistikaIzvozaPM4_Click`
  - „Prijem i izdavanje robe“ — klik → `btnLogistikaIzvozaPM3_Click`
  - „Porudžbenica“ — klik → `btnLogistikaIzvozaPM1_Click`

#### LogistikaIzvoza2
- **Fajl:** `MainLeget/Izvoz/LogistikaIzvoza2.cs`  
- **Naslov forme:** „MainLeget“
- **Otvara forme:** frmPregledGrupaKontejneraIzvoz („Izmeni postojeći“), frmProdajniNalogIzvozTabela („Kreiraj novi“)
- **Funkcionalnosti (kontrola → handler):**
  - „Kreiraj novi“ — klik → `sfButton1_Click_1`
  - „Izmeni postojeći“ — klik → `sfButton2_Click` — otvara: frmPregledGrupaKontejneraIzvoz, frmProdajniNalogIzvozTabela
  - „Otkaži postojeći“ — klik → `sfButton3_Click` — otvara: frmPregledGrupaKontejneraIzvoz
  - „Lista OTVORENIH naloga“ — klik → `sfButton4_Click`
  - „Lista SVIH naloga“ — klik → `sfButton6_Click` — otvara: frmPregledGrupaKontejneraIzvoz

#### NalogIzvozaZaOtpremu
- **Fajl:** `MainLeget/Izvoz/NalogIzvozaZaOtpremu.cs`  
- **Naslov forme:** „NalogIzvozaZaOtpremu“
- **Funkcionalnosti (kontrola → handler):**
  - „PLATFORMA PRAZAN PUN“ — klik → `btn7_Click`
  - „CERADA  SKLADISTE PUN“ — klik → `btn9_Click`
  - „CERADA PRETOVAR PUN“ — klik → `btn8_Click`
  - „PLATFORMA DIREKTNO PUN“ — klik → `btn13_Click`

### Prijem i otprema kamiona (13)

#### frmCerada
- **Fajl:** `MainLeget/PrijemIOtpremaKamiona/frmCerada.cs`  
- **Naslov forme:** „frmCerada“
- **Tabele:**
  - `organizacioneJedinice` — S
  - `PrijemKontejneraVozStavke` — S
  - `RadniNalogInterni` — S
  - `RadniNalogInterniPotvrda` — S
- **Procedure:** `DelRadniNalogInterniSaDokumentima`, `DeleteRadniNalogInterni`, `DozvoliIzlaz`, `InsertRadniNalogInterni`, `InsertRadniNalogInterni2`, `InsertRadniNalogInterniIzvoz`, `InsertRadniNalogInterniIzvoz2`, `InsertRadniNalogInterniIzvozDodatne`, `InsertRadniNalogInterniPotvrda`, `InsertRadniNalogInterniSkladistePotvrda`, `InsertRadniNalogInterniStorno`, `ObavljenPregled`, `PromeniStatusKalmar`, `PromeniStatusKapijaOdlazak`, `PromeniStatusPretovareno`, `PromeniStatusPrijemPotvrdioKapiju`, `PromeniStatusPrijemPotvrdioVP`, `UpdateRNIVozFormiran`, `UpdateRadniNalogInterni`, `UpdateRadniNalogInterniFormiran`, `UpdateRadniNalogInterniGenerisan`, `UpdateRadniNalogInterniZavrsen`, `updRadniNalogInterniPrijemnicaPotvrdjen`, `updRadniNalogInterniRukovalacPotvrdjen`, `updRadniNalogInterniSkladistarPotvrdio`, `updRadniNalogInterniSkladistePotvrdjen`
- **Funkcionalnosti (kontrola → handler):**
  - „Refresh“ — klik → `button8_Click`
  - „Potvrdi kapiju“ — klik → `button27_Click` — proc: DelRadniNalogInterniSaDokumentima, DeleteRadniNalogInterni, DozvoliIzlaz, InsertRadniNalogInterni, InsertRadniNalogInterni2, InsertRadniNalogInterniIzvoz; preko: InsertRadniNalogInterni

#### frmCeradaOtprema
- **Fajl:** `MainLeget/PrijemIOtpremaKamiona/frmCeradaOtprema.cs`  
- **Naslov forme:** „frmCeradaOtprema“
- **Tabele:**
  - `organizacioneJedinice` — S
  - `OtpremaKontejneraVozStavke` — S
  - `RadniNalogInterni` — S
  - `RadniNalogInterniPotvrda` — S
- **Funkcionalnosti (kontrola → handler):**
  - „Refresh“ — klik → `button8_Click`

#### frmFotografijePregledac
- **Fajl:** `MainLeget/PrijemIOtpremaKamiona/frmFotografijePregledac.cs`  
- **Naslov forme:** „frmFotografijePregledac“
- **Tabele:**
  - `DokumentaPregledac` — S
- **Procedure:** `DeleteNajavaDokumenta`, `UpdateNajavaCIM`
- **Funkcionalnosti (kontrola → handler):**
  - „Sačuvaj“ — klik → `btnSacuvajTovarniList_Click` — proc: DeleteNajavaDokumenta, UpdateNajavaCIM; preko: InsertFotografijeDokumenta
  - „Pronađi“ — klik → `btnTovarniList_Click`
  - „Pronađi“ — klik → `button3_Click`
  - „Otvori“ — klik → `button2_Click`
  - „Pronađi“ — klik → `button1_Click`

#### frmPlatforma
- **Fajl:** `MainLeget/PrijemIOtpremaKamiona/frmPlatforma.cs`  
- **Naslov forme:** „Platforma“
- **Otvara forme:** frmPrijemKamionaDetalji („Pregled“)
- **Tabele:**
  - `IzvozKonacna` — S
  - `Kapija` — S
  - `MestaUtovara` — S
  - `organizacioneJedinice` — S
  - `PrijemKontejneraVozStavke` — S
  - `RadniNalogInterni` — S
  - `RadniNalogInterniPotvrda` — S
  - `RNPrijemPlatforme` — S
- **Procedure:** `DelRadniNalogInterniSaDokumentima`, `DeleteIzvozKonacnaNHM`, `DeleteIzvozKonacnaVrstaManipulacije`, `DeleteIzvozKonacnaVrstaRobeHS`, `DeleteIzvozNHM`, `DeleteIzvozSve`, `DeleteIzvozVrstaManipulacije`, `DeleteIzvozVrstaRobeHS`, `DeleteKontejnerIzvoz`, `DeleteRNMedjuskladisni`, `DeleteRNOtpremaCirade`, `DeleteRNOtpremaPlatforme`, `DeleteRNOtpremaPlatforme2`, `DeleteRNOtpremaVoza`, `DeleteRNPregledIpostavkaKontejnera`, `DeleteRNPregledIspraznjenogKontejnera`, `DeleteRNPrijemCirade`, `DeleteRNPrijemPlatforme`, `DeleteRNPrijemPlatforme2`, `DeleteRNPrijemVoza`, `DeleteRNPrijemVoza2`, `DeleteRadniNalogInterni`, `DeleteVaganje`, `DozvoliIzlaz`, `InsertIzvozKonacnaNHM`, `InsertIzvozKonacnaVMIzUvoza`, `InsertIzvozKonacnaVrstaManipulacije`, `InsertIzvozNHM`, `InsertIzvozPorudzbenica`, `InsertIzvozVMIzUvoza`, `InsertIzvozVrstaManipulacije`, `InsertIzvozVrstaRobeHS`, `InsertIzvozkonacnaVrstaRobeHS`, `InsertKontejnerLog`, `InsertRN10PregledCeoVoz`, `InsertRN11PregledCeoVozOtprema`, `InsertRN12Medjuskladisni`, `InsertRN4PrPlatformeKamIzvoz`, `InsertRN4PrPlatformeKamUvoz`, `InsertRN5PrPlatFormeKam`, `InsertRN6OtPlatFormeKam`, `InsertRN7OtpremaOtpremaPlatKam`, `InsertRN8OtpremaCiradeKam`, `InsertRN8OtpremaCiradeKamIzvoz`, `InsertRN9PrijemCiradeKam`, `InsertRNOtpremaCirade`, `InsertRNOtpremaPlatforme`, `InsertRNOtpremaPlatforme2`, `InsertRNOtpremaVoza`, `InsertRNOtpremaVozaCeoVoz`, `InsertRNPregledIpostavkaKontejnera`, `InsertRNPregledIspraznjenogKontejnera`, `InsertRNPrijemCirade`, `InsertRNPrijemPlatforme`, `InsertRNPrijemPlatforme2`, `InsertRNPrijemVoza`, `InsertRNPrijemVoza2`, `InsertRNPrijemVozaCeoVoz`, `InsertRNPrijemVozaCeoVozPretovar`, `InsertRadniNalogInterni`, `InsertRadniNalogInterni2`, `InsertRadniNalogInterniIzvoz`, `InsertRadniNalogInterniIzvoz2`, `InsertRadniNalogInterniIzvozDodatne`, `InsertRadniNalogInterniPotvrda`, `InsertRadniNalogInterniSkladistePotvrda`, `InsertRadniNalogInterniStorno`, `InsertVaganje`, `KopirajKontejnerIzvoz`, `ObavljenPregled`, `PokreniSkladisninuKontejnera`, `PrenesiKontejnerTekuceUArhiv`, `PromeniStatusKalmar`, `PromeniStatusKapijaOdlazak`, `PromeniStatusPretovareno`, `PromeniStatusPrijemPotvrdioKapiju`, `PromeniStatusPrijemPotvrdioVP`, `UpdCenaIzvozKonacnaVrstaManipulacije`, `UpdCenaIzvozVrstaManipulacije`, `UpdPDVIzvozKonacnaVrstaManipulacije`, `UpdPDVIzvozVrstaManipulacije`, `UpdPlatiocaIzvozKonacnaVrstaManipulacije`, `UpdPlatiocaIzvozVrstaManipulacije`, `UpdateIZCIRA`, `UpdateIzvoz`, `UpdateIzvozKonacna`, `UpdateIzvozKonacnaVaganjeCirada`, `UpdateIzvozKonacnaVaganjePlatforma`, `UpdateIzvozPorudzbenica`, `UpdateIzvozPorudzbenicaCarina`, `UpdateIzvozPorudzbenicaPojedinacni`, `UpdateIzvozPorudzbenicaRelacija`, `UpdateIzvozStatus`, `UpdateIzvozStatusTerminal`, `UpdateIzvozTerminalIzbor`, `UpdateRN12MedjuskladisniSkladiste`, `UpdateRN12Uradjene`, `UpdateRN1Uradjene`, `UpdateRN1UradjeneCIR`, `UpdateRN1UradjeneS`, `UpdateRN1UradjeneVP`, `UpdateRN2Uradjene`, `UpdateRN3Uradjene`, `UpdateRN4PotrebanCIR`, `UpdateRN4PrivremenoSkladiste`, `UpdateRN4SkladisteIzCira`, `UpdateRN4Uradjene`, `UpdateRN4UradjeneCIR`, `UpdateRN4UradjenePostavljen`, `UpdateRN4UradjeneVizuelni`, `UpdateRN5PrijemSkladiste`, `UpdateRN5Uradjene`, `UpdateRN5UradjeneCIR`, `UpdateRN5UradjeneS`, `UpdateRN6OtpremaSkladiste`, `UpdateRN6PotrebanCIR`, `UpdateRN6Uradjene`, `UpdateRN6UradjeneVizuelni`, `UpdateRN7Uradjene`, `UpdateRN8Uradjene`, `UpdateRN8UradjeneCIR`, `UpdateRN9PretovarCIRADE`, `UpdateRN9Uradjen`, `UpdateRNIVozFormiran`, `UpdateRNMedjuskladisni`, `UpdateRNOtpremaCirade`, `UpdateRNOtpremaPlatforme`, `UpdateRNOtpremaPlatforme2`, `UpdateRNOtpremaVoza`, `UpdateRNPregledIpostavkaKontejnera`, `UpdateRNPregledIspraznjenogKontejnera`, `UpdateRNPrijemCirade`, `UpdateRNPrijemPlatforme`, `UpdateRNPrijemPlatforme2`, `UpdateRNPrijemVoza`, `UpdateRNPrijemVoza2`, `UpdateRNPrijemVozaSkladiste`, `UpdateRadniNalogInterni`, `UpdateRadniNalogInterniFormiran`, `UpdateRadniNalogInterniGenerisan`, `UpdateRadniNalogInterniZavrsen`, `UpdateVaganje`, `ZatvoriSkladisninuKontejnera`, `spPrenesiKontejnerIzNerasporedjenihUPrijPlatforma`, `updRN4PrPlatformeKamIzvozPusti`, `updRadniNalogInterniPrijemnicaPotvrdjen`, `updRadniNalogInterniRukovalacPotvrdjen`, `updRadniNalogInterniSkladistarPotvrdio`, `updRadniNalogInterniSkladistePotvrdjen`
- **Funkcionalnosti (kontrola → handler):**
  - „Refresh“ — klik → `button8_Click`
  - „Štampaj“ — klik → `button8_Click`
  - „Radni nalog Vaga“ — klik → `button5_Click`
  - „Radni nalog kalmar“ — klik → `button4_Click` — otvara: frmDodelaSkladista; proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „Skeniraj dokumenta“ — klik → `button3_Click`
  - „Potvrdi pregled“ — klik → `button21_Click`
  - „Potvrdi kapiju“ — klik → `button27_Click` — proc: DelRadniNalogInterniSaDokumentima, DeleteRadniNalogInterni, DozvoliIzlaz, InsertRadniNalogInterni, InsertRadniNalogInterni2, InsertRadniNalogInterniIzvoz; preko: InsertRadniNalogInterni
  - „Potvrdi pregled“ — klik → `button1_Click` — proc: DelRadniNalogInterniSaDokumentima, DeleteRadniNalogInterni, DozvoliIzlaz, InsertRadniNalogInterni, InsertRadniNalogInterni2, InsertRadniNalogInterniIzvoz; preko: InsertRadniNalogInterni
  - „Pregled“ — klik → `button9_Click` — otvara: frmPrijemKamionaDetalji

#### frmPlatformaOtprema
- **Fajl:** `MainLeget/PrijemIOtpremaKamiona/frmPlatformaOtprema.cs`  
- **Naslov forme:** „frmPlatformaOtprema“
- **Tabele:**
  - `organizacioneJedinice` — S
  - `OtpremaKontejneraVozStavke` — S
  - `RadniNalogInterni` — S
  - `RadniNalogInterniPotvrda` — S
- **Procedure:** `DelRadniNalogInterniSaDokumentima`, `DeleteRNMedjuskladisni`, `DeleteRNOtpremaCirade`, `DeleteRNOtpremaPlatforme`, `DeleteRNOtpremaPlatforme2`, `DeleteRNOtpremaVoza`, `DeleteRNPregledIpostavkaKontejnera`, `DeleteRNPregledIspraznjenogKontejnera`, `DeleteRNPrijemCirade`, `DeleteRNPrijemPlatforme`, `DeleteRNPrijemPlatforme2`, `DeleteRNPrijemVoza`, `DeleteRNPrijemVoza2`, `DeleteRadniNalogInterni`, `DozvoliIzlaz`, `InsertRN10PregledCeoVoz`, `InsertRN11PregledCeoVozOtprema`, `InsertRN12Medjuskladisni`, `InsertRN4PrPlatformeKamIzvoz`, `InsertRN4PrPlatformeKamUvoz`, `InsertRN5PrPlatFormeKam`, `InsertRN6OtPlatFormeKam`, `InsertRN7OtpremaOtpremaPlatKam`, `InsertRN8OtpremaCiradeKam`, `InsertRN8OtpremaCiradeKamIzvoz`, `InsertRN9PrijemCiradeKam`, `InsertRNOtpremaCirade`, `InsertRNOtpremaPlatforme`, `InsertRNOtpremaPlatforme2`, `InsertRNOtpremaVoza`, `InsertRNOtpremaVozaCeoVoz`, `InsertRNPregledIpostavkaKontejnera`, `InsertRNPregledIspraznjenogKontejnera`, `InsertRNPrijemCirade`, `InsertRNPrijemPlatforme`, `InsertRNPrijemPlatforme2`, `InsertRNPrijemVoza`, `InsertRNPrijemVoza2`, `InsertRNPrijemVozaCeoVoz`, `InsertRNPrijemVozaCeoVozPretovar`, `InsertRadniNalogInterni`, `InsertRadniNalogInterni2`, `InsertRadniNalogInterniIzvoz`, `InsertRadniNalogInterniIzvoz2`, `InsertRadniNalogInterniIzvozDodatne`, `InsertRadniNalogInterniPotvrda`, `InsertRadniNalogInterniSkladistePotvrda`, `InsertRadniNalogInterniStorno`, `ObavljenPregled`, `PokreniSkladisninuKontejnera`, `PrenesiKontejnerTekuceUArhiv`, `PromeniStatusKalmar`, `PromeniStatusKapijaOdlazak`, `PromeniStatusPretovareno`, `PromeniStatusPrijemPotvrdioKapiju`, `PromeniStatusPrijemPotvrdioVP`, `UpdateIZCIRA`, `UpdateRN12MedjuskladisniSkladiste`, `UpdateRN12Uradjene`, `UpdateRN1Uradjene`, `UpdateRN1UradjeneCIR`, `UpdateRN1UradjeneS`, `UpdateRN1UradjeneVP`, `UpdateRN2Uradjene`, `UpdateRN3Uradjene`, `UpdateRN4PotrebanCIR`, `UpdateRN4PrivremenoSkladiste`, `UpdateRN4SkladisteIzCira`, `UpdateRN4Uradjene`, `UpdateRN4UradjeneCIR`, `UpdateRN4UradjenePostavljen`, `UpdateRN4UradjeneVizuelni`, `UpdateRN5PrijemSkladiste`, `UpdateRN5Uradjene`, `UpdateRN5UradjeneCIR`, `UpdateRN5UradjeneS`, `UpdateRN6OtpremaSkladiste`, `UpdateRN6PotrebanCIR`, `UpdateRN6Uradjene`, `UpdateRN6UradjeneVizuelni`, `UpdateRN7Uradjene`, `UpdateRN8Uradjene`, `UpdateRN8UradjeneCIR`, `UpdateRN9PretovarCIRADE`, `UpdateRN9Uradjen`, `UpdateRNIVozFormiran`, `UpdateRNMedjuskladisni`, `UpdateRNOtpremaCirade`, `UpdateRNOtpremaPlatforme`, `UpdateRNOtpremaPlatforme2`, `UpdateRNOtpremaVoza`, `UpdateRNPregledIpostavkaKontejnera`, `UpdateRNPregledIspraznjenogKontejnera`, `UpdateRNPrijemCirade`, `UpdateRNPrijemPlatforme`, `UpdateRNPrijemPlatforme2`, `UpdateRNPrijemVoza`, `UpdateRNPrijemVoza2`, `UpdateRNPrijemVozaSkladiste`, `UpdateRadniNalogInterni`, `UpdateRadniNalogInterniFormiran`, `UpdateRadniNalogInterniGenerisan`, `UpdateRadniNalogInterniZavrsen`, `ZatvoriSkladisninuKontejnera`, `updRN4PrPlatformeKamIzvozPusti`, `updRadniNalogInterniPrijemnicaPotvrdjen`, `updRadniNalogInterniRukovalacPotvrdjen`, `updRadniNalogInterniSkladistarPotvrdio`, `updRadniNalogInterniSkladistePotvrdjen`
- **Funkcionalnosti (kontrola → handler):**
  - „Kalmar“ — klik → `button6_Click` — proc: DelRadniNalogInterniSaDokumentima, DeleteRadniNalogInterni, DozvoliIzlaz, InsertRadniNalogInterni, InsertRadniNalogInterni2, InsertRadniNalogInterniIzvoz; preko: InsertRadniNalogInterni
  - „Dozvoli izlaz“ — klik → `button1_Click` — proc: DelRadniNalogInterniSaDokumentima, DeleteRadniNalogInterni, DozvoliIzlaz, InsertRadniNalogInterni, InsertRadniNalogInterni2, InsertRadniNalogInterniIzvoz; preko: InsertRadniNalogInterni
  - „Refresh“ — klik → `button9_Click`
  - „Štampaj“ — klik → `button8_Click`
  - „Radni nalog kalmar“ — klik → `button5_Click` — otvara: frmDodelaSkladista; proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „Potvrdi pregled“ — klik → `button2_Click` — proc: DelRadniNalogInterniSaDokumentima, DeleteRadniNalogInterni, DozvoliIzlaz, InsertRadniNalogInterni, InsertRadniNalogInterni2, InsertRadniNalogInterniIzvoz; preko: InsertRadniNalogInterni
  - „Potvrdi kapiju“ — klik → `button27_Click` — proc: DelRadniNalogInterniSaDokumentima, DeleteRadniNalogInterni, DozvoliIzlaz, InsertRadniNalogInterni, InsertRadniNalogInterni2, InsertRadniNalogInterniIzvoz; preko: InsertRadniNalogInterni

#### frmPrijemIOtpremaCerada
- **Fajl:** `MainLeget/PrijemIOtpremaKamiona/frmPrijemIOtpremaCerada.cs`  
- **Naslov forme:** „frmPrijemIOtpremaCerada“
- **Otvara forme:** frmCerada („Prijem“), frmCeradaOtprema („Otprema“)
- **Funkcionalnosti (kontrola → handler):**
  - „Vaga“ — klik → `btnPrijemIOtprema3_Click` — otvara: VaganjePregled
  - „Otprema“ — klik → `btnPrijemIOtprema2_Click` — otvara: frmCeradaOtprema
  - „Prijem“ — klik → `btnPrijemIOtprema1_Click` — otvara: frmCerada

#### frmPrijemIOtpremaKamiona1
- **Fajl:** `MainLeget/PrijemIOtpremaKamiona/frmPrijemIOtpremaKamiona1.cs`  
- **Naslov forme:** „frmPrijemIOtpremaKamiona1“
- **Otvara forme:** frmPrijemIOtpremaCerada („CERADA“), frmPrijemIOtpremaPlatforma („PLATFORMA“), Pregledac („PREGLEDAČ“)
- **Funkcionalnosti (kontrola → handler):**
  - „Prijem i otprema kamiona“ — klik → `label2_Click`
  - „PLATFORMA“ — klik → `btnPrijemIOtpremaKamiona8_Click` — otvara: frmPrijemIOtpremaPlatforma
  - „CERADA“ — klik → `btnPrijemIOtpremaKamiona9_Click` — otvara: frmPrijemIOtpremaCerada
  - „PREGLEDAČ“ — klik → `btnPrijemIOtpremaKamiona11_Click` — otvara: Pregledac

#### frmPrijemIOtpremaPlatforma
- **Fajl:** `MainLeget/PrijemIOtpremaKamiona/frmPrijemIOtpremaPlatforma.cs`  
- **Naslov forme:** „frmPrijemIOtpremaPlatforma“
- **Otvara forme:** frmPlatforma („Prijem“), frmPlatformaOtprema („Otprema“), VaganjePregled („Vaga“)
- **Funkcionalnosti (kontrola → handler):**
  - „Prijem“ — klik → `btnPrijemIOtprema1_Click` — otvara: frmPlatforma
  - „Otprema“ — klik → `btnPrijemIOtprema2_Click` — otvara: frmPlatformaOtprema
  - „Vaga“ — klik → `btnPrijemIOtprema3_Click` — otvara: VaganjePregled

#### frmPrijemKamionaDetalji
- **Fajl:** `MainLeget/PrijemIOtpremaKamiona/frmPrijemKamionaDetalji.cs`  
- **Naslov forme:** „Prijem kamiona detalji“
- **Tabele:**
  - `Carinarnice` — S
  - `InspekciskiTretman` — S
  - `IzvozKonacna` — S
  - `IzvozKonacnaNHM` — S
  - `KontejnerskiTerminali` — S
  - `MestaUtovara` — S
  - `NHM` — S
  - `Partnerji` — S
  - `partnerjiKontOsebaMU` — S
  - `TipKontenjera` — S
  - `uvKvalitetKontejnera` — S
  - `uvNacinPakovanja` — S
  - `VrstaCarinskogPostupka` — S
  - `VrstaRobeADR` — S
  - `VrstaVozila` — S

#### IzvestajVaganja
- **Fajl:** `Izvoz/IzvestajVaganja.cs`  
- **Naslov forme:** „IzvestajVaganja“
- **Funkcionalnosti (kontrola → handler):**
  - `this` — otvaranje forme → `IzvestajVaganja_Load` — izveštaj: VaganjeRpt.rdlc, VaganjeRptCirada.rdlc

#### Pregledac
- **Fajl:** `MainLeget/PrijemIOtpremaKamiona/Pregledac.cs`  
- **Naslov forme:** „Pregledac“
- **Otvara forme:** frmCIR („Izradi CIR“), frmDodatneUsluge („Dodatne usluge“), frmFotografijePregledac („Fotografije“), ZapisnikONepravilnosti („Evidencija nepravilnosti“)
- **Tabele:**
  - `IzvozKonacna` — S
  - `Kapija` — S
  - `OtpremaKontejneraVozStavke` — S
  - `Partnerji` — S
  - `PrijemKontejneraVozStavke` — S
  - `ProdajniNalogIzvozStavke` — S
  - `RadniNalogInterni` — S
  - `RadniNalogInterniPotvrda` — S
  - `RNOtpremaPlatforme` — S
  - `RNPrijemPlatforme` — S
  - `Scenario` — S
  - `TipKontenjera` — S
- **Procedure:** `DeleteRNMedjuskladisni`, `DeleteRNOtpremaCirade`, `DeleteRNOtpremaPlatforme`, `DeleteRNOtpremaPlatforme2`, `DeleteRNOtpremaVoza`, `DeleteRNPregledIpostavkaKontejnera`, `DeleteRNPregledIspraznjenogKontejnera`, `DeleteRNPrijemCirade`, `DeleteRNPrijemPlatforme`, `DeleteRNPrijemPlatforme2`, `DeleteRNPrijemVoza`, `DeleteRNPrijemVoza2`, `InsertRN10PregledCeoVoz`, `InsertRN11PregledCeoVozOtprema`, `InsertRN12Medjuskladisni`, `InsertRN4PrPlatformeKamIzvoz`, `InsertRN4PrPlatformeKamUvoz`, `InsertRN5PrPlatFormeKam`, `InsertRN6OtPlatFormeKam`, `InsertRN7OtpremaOtpremaPlatKam`, `InsertRN8OtpremaCiradeKam`, `InsertRN8OtpremaCiradeKamIzvoz`, `InsertRN9PrijemCiradeKam`, `InsertRNOtpremaCirade`, `InsertRNOtpremaPlatforme`, `InsertRNOtpremaPlatforme2`, `InsertRNOtpremaVoza`, `InsertRNOtpremaVozaCeoVoz`, `InsertRNPregledIpostavkaKontejnera`, `InsertRNPregledIspraznjenogKontejnera`, `InsertRNPrijemCirade`, `InsertRNPrijemPlatforme`, `InsertRNPrijemPlatforme2`, `InsertRNPrijemVoza`, `InsertRNPrijemVoza2`, `InsertRNPrijemVozaCeoVoz`, `InsertRNPrijemVozaCeoVozPretovar`, `PokreniSkladisninuKontejnera`, `PrenesiKontejnerTekuceUArhiv`, `UpdateIZCIRA`, `UpdateRN12MedjuskladisniSkladiste`, `UpdateRN12Uradjene`, `UpdateRN1Uradjene`, `UpdateRN1UradjeneCIR`, `UpdateRN1UradjeneS`, `UpdateRN1UradjeneVP`, `UpdateRN2Uradjene`, `UpdateRN3Uradjene`, `UpdateRN4PotrebanCIR`, `UpdateRN4PrivremenoSkladiste`, `UpdateRN4SkladisteIzCira`, `UpdateRN4Uradjene`, `UpdateRN4UradjeneCIR`, `UpdateRN4UradjenePostavljen`, `UpdateRN4UradjeneVizuelni`, `UpdateRN5PrijemSkladiste`, `UpdateRN5Uradjene`, `UpdateRN5UradjeneCIR`, `UpdateRN5UradjeneS`, `UpdateRN6OtpremaSkladiste`, `UpdateRN6PotrebanCIR`, `UpdateRN6Uradjene`, `UpdateRN6UradjeneVizuelni`, `UpdateRN7Uradjene`, `UpdateRN8Uradjene`, `UpdateRN8UradjeneCIR`, `UpdateRN9PretovarCIRADE`, `UpdateRN9Uradjen`, `UpdateRNMedjuskladisni`, `UpdateRNOtpremaCirade`, `UpdateRNOtpremaPlatforme`, `UpdateRNOtpremaPlatforme2`, `UpdateRNOtpremaVoza`, `UpdateRNPregledIpostavkaKontejnera`, `UpdateRNPregledIspraznjenogKontejnera`, `UpdateRNPrijemCirade`, `UpdateRNPrijemPlatforme`, `UpdateRNPrijemPlatforme2`, `UpdateRNPrijemVoza`, `UpdateRNPrijemVoza2`, `UpdateRNPrijemVozaSkladiste`, `ZatvoriSkladisninuKontejnera`, `updRN4PrPlatformeKamIzvozPusti`
- **Funkcionalnosti (kontrola → handler):**
  - „Fotografije“ — klik → `button5_Click` — otvara: frmFotografijePregledac
  - „Dodatne usluge“ — klik → `button4_Click` — otvara: frmDodatneUsluge
  - „Izradi CIR“ — klik → `button3_Click` — otvara: frmCIR; proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „Evidencija nepravilnosti“ — klik → `button8_Click` — otvara: ZapisnikONepravilnosti
  - „Potvrdi VP“ — klik → `button1_Click` — proc: DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza, DeleteRNPregledIpostavkaKontejnera; preko: InsertRN
  - „Refresh“ — klik → `button2_Click`

#### Vaganje
- **Fajl:** `Izvoz/Vaganje.cs`  
- **Naslov forme:** „VAGANJE“
- **Otvara forme:** IzvestajVaganja („Izveštaj“)
- **Tabele:**
  - `IzvozKonacna` — S
  - `MestaUtovara` — S
  - `RadniNalogInterni` — S
  - `TipKontenjera` — S
  - `Vaganje` — S
- **Procedure:** `DeleteIzvozKonacnaNHM`, `DeleteIzvozKonacnaVrstaManipulacije`, `DeleteIzvozKonacnaVrstaRobeHS`, `DeleteIzvozNHM`, `DeleteIzvozSve`, `DeleteIzvozVrstaManipulacije`, `DeleteIzvozVrstaRobeHS`, `DeleteKontejnerIzvoz`, `DeleteVaganje`, `InsertIzvozKonacnaNHM`, `InsertIzvozKonacnaVMIzUvoza`, `InsertIzvozKonacnaVrstaManipulacije`, `InsertIzvozNHM`, `InsertIzvozPorudzbenica`, `InsertIzvozVMIzUvoza`, `InsertIzvozVrstaManipulacije`, `InsertIzvozVrstaRobeHS`, `InsertIzvozkonacnaVrstaRobeHS`, `InsertKontejnerLog`, `InsertVaganje`, `KopirajKontejnerIzvoz`, `UpdCenaIzvozKonacnaVrstaManipulacije`, `UpdCenaIzvozVrstaManipulacije`, `UpdPDVIzvozKonacnaVrstaManipulacije`, `UpdPDVIzvozVrstaManipulacije`, `UpdPlatiocaIzvozKonacnaVrstaManipulacije`, `UpdPlatiocaIzvozVrstaManipulacije`, `UpdateIzvoz`, `UpdateIzvozKonacna`, `UpdateIzvozKonacnaVaganjeCirada`, `UpdateIzvozKonacnaVaganjePlatforma`, `UpdateIzvozPorudzbenica`, `UpdateIzvozPorudzbenicaCarina`, `UpdateIzvozPorudzbenicaPojedinacni`, `UpdateIzvozPorudzbenicaRelacija`, `UpdateIzvozStatus`, `UpdateIzvozStatusTerminal`, `UpdateIzvozTerminalIzbor`, `UpdateVaganje`, `spPrenesiKontejnerIzNerasporedjenihUPrijPlatforma`
- **Funkcionalnosti (kontrola → handler):**
  - „Potvrdi završeno vaganje“ — klik → `btnZavrsenoVaganje_Click`
  - „Izveštaj“ — klik → `button23_Click` — otvara: IzvestajVaganja
  - `button21` — klik → `button21_Click` — proc: DeleteIzvozKonacnaNHM, DeleteIzvozKonacnaVrstaManipulacije, DeleteIzvozKonacnaVrstaRobeHS, DeleteIzvozNHM, DeleteIzvozSve, DeleteIzvozVrstaManipulacije; preko: InsertIzvoz
  - `this` — otvaranje forme → `Vaganje_Load` — čita: TipKontenjera

#### VaganjePregled
- **Fajl:** `Izvoz/VaganjePregled.cs`  
- **Naslov forme:** „Kontejneri za vaganje“
- **Otvara forme:** Vaganje („Otvori“)
- **Tabele:**
  - `IzvozKonacna` — S
  - `RadniNalogInterni` — S
  - `RadniNalogInterniPotvrda` — S
  - `Vaganje` — S
- **Funkcionalnosti (kontrola → handler):**
  - „Refresh“ — klik → `toolStripButton1_Click`
  - „Otvori“ — klik → `toolStripButton2_Click` — otvara: Vaganje
  - „Izvagani kontejneri“ — klik → `toolStripButton3_Click` — čita: IzvozKonacna, Vaganje
  - „Izvagani kontejneri“ — klik → `button5_Click` — čita: IzvozKonacna, Vaganje
  - „Otvori“ — klik → `button4_Click` — otvara: Vaganje
  - „Refresh“ — klik → `button23_Click`

### HR modul (14)

#### frmHRDan
- **Fajl:** `HR/frmHRDan.cs`
- **Tabele:**
  - `BeleskaORadu` — S (preko HRBeleske)
  - `Drzave` — S (preko HRPolje, HRVrednosti)
  - `EvidencijaRada` — S
  - `fn_Podredjeni` — S (preko HRBeleske)
  - `Opstine` — S (preko HRPolje, HRVrednosti)
  - `OrganizacioneJedinice` — S (preko HRPolje, HRVrednosti)
  - `Partnerji` — S (preko HRPolje, HRVrednosti)
  - `Stanice` — S (preko HRPolje, HRVrednosti)
  - `Valute` — S (preko HRPolje, HRVrednosti)
  - `VrstaRada` — S
  - `Zaposleni` — S (preko HRBeleske, HRPolje, HRVrednosti)
- **Procedure:** `sp_set_session_context`

#### frmHREvidencijaRada
- **Fajl:** `HR/frmHREvidencijaRada.cs`
- **Otvara forme:** frmHRDan (metoda `OtvoriDan`), frmHRGrupniUnos (metoda `GrupniUnos`)
- **Tabele:**
  - `BeleskaORadu` — S
  - `EvidencijaMesec` — S
  - `EvidencijaRada` — S
  - `fn_Podredjeni` — S (preko HRBeleske, HRGO, HRPrava)
  - `GODopust` — S (preko HRGO)
  - `GODopustStavke` — S (preko HRGO)
  - `Korisnici` — S (preko HRPrava)
  - `MainNovi` — S (preko HRPrava)
  - `mainNoviKartice` — S (preko HRPrava)
  - `MainNoviPrava` — S (preko HRPrava)
  - `Podesavanja` — SU (preko Podesavanja)
  - `Praznici` — S
  - `RasporedRada` — S
  - `VrstaRada` — S
  - `vw_EvidencijaZaObracun` — S
  - `vZaposleniTrenutno` — S
  - `Zaposleni` — S
- **Procedure:** `sp_GO_Otkazi`, `sp_GO_PodnesiZahtev`, `sp_set_session_context`

#### frmHRGOPravo
- **Fajl:** `HR/frmHRGOPravo.cs`
- **Otvara forme:** frmHRStavka (metoda `Generisi`)
- **Tabele:**
  - `Drzave` — S (preko HRPolje, HRVrednosti)
  - `fn_Podredjeni` — S (preko HRGO)
  - `GODopust` — S
  - `GODopustOsnov` — S
  - `GODopustStavke` — S
  - `Opstine` — S (preko HRPolje, HRVrednosti)
  - `OrganizacioneJedinice` — S (preko HRPolje, HRVrednosti)
  - `OsnovGO` — S
  - `Partnerji` — S (preko HRPolje, HRVrednosti)
  - `Stanice` — S (preko HRPolje, HRVrednosti)
  - `Valute` — S (preko HRPolje, HRVrednosti)
  - `vZaposleniTrenutno` — S
  - `Zaposleni` — S
- **Procedure:** `sp_GO_Otkazi`, `sp_GO_PodnesiZahtev`, `sp_set_session_context`

#### frmHRGOZahtevi
- **Fajl:** `HR/frmHRGOZahtevi.cs`
- **Tabele:**
  - `fn_Podredjeni` — S (preko HRGO, HRPrava)
  - `GODopust` — S
  - `GODopustStavke` — SU
  - `Korisnici` — S (preko HRPrava)
  - `MainNovi` — S (preko HRPrava)
  - `mainNoviKartice` — S (preko HRPrava)
  - `MainNoviPrava` — S (preko HRPrava)
  - `Opstine` — S (preko HRResenje)
  - `StatusZahteva` — S
  - `vZaposleniTrenutno` — S (preko HRResenje)
  - `Zaposleni` — S
- **Procedure:** `sp_GO_Otkazi`, `sp_GO_PodnesiZahtev`, `sp_set_session_context`

#### frmHRGrupniUnos
- **Fajl:** `HR/frmHRGrupniUnos.cs`
- **Tabele:**
  - `fnHR_ZahtevPlanOdobravanja` — S (preko Stavka)
  - `HTZOprema` — S (preko Stavka)
  - `OrganizacioneJedinice` — S (preko Stavka)
  - `Partnerji` — S (preko Stavka)
  - `Podesavanja` — SU (preko Stavka)
  - `Pozicija` — S (preko Stavka)
  - `PrioritetiNabavke` — S (preko Stavka)
  - `Skladista` — S (preko Stavka)
  - `Stanice` — S (preko Stavka)
  - `StatusiStavkiNabavke` — S (preko Stavka)
  - `StatusiZahtevaZaNabavku` — S (preko Stavka)
  - `VrstaRada` — S (preko HRUnosDana)
  - `VrsteNabavke` — S (preko Stavka)
  - `vZaposleniTrenutno` — S (preko Stavka)
  - `ZahteviZaNabavku` — S (preko Stavka)
  - `ZahtevZaNabavkuIstorijaStatusa` — S (preko Stavka)
  - `ZahtevZaNabavkuOdobrenja` — S (preko Stavka)
  - `ZahtevZaNabavkuPonude` — SD (preko Stavka)
  - `ZahtevZaNabavkuPrilozi` — SD (preko Stavka)
  - `ZahtevZaNabavkuStavke` — SUD (preko Stavka)
  - `Zaposleni` — S (preko Stavka)
- **Procedure:** `HR_ZahtevPreracunaj`, `sp_set_session_context`

#### frmHRKontrolnaTabla
- **Fajl:** `HR/frmHRKontrolnaTabla.cs`
- **Tabele:**
  - `fn_Podredjeni` — S (preko HRPrava)
  - `Korisnici` — S (preko HRPrava)
  - `MainNovi` — S (preko HRPrava)
  - `mainNoviKartice` — S (preko HRPrava)
  - `MainNoviPrava` — S (preko HRPrava)
  - `vZaposleniTrenutno` — S
  - `Zaposleni` — S (preko HRPrava)
- **Procedure:** `sp_set_session_context`

#### frmHRLog
- **Fajl:** `HR/frmHRLog.cs`
- **Tabele:**
  - `HRLog` — S
- **Procedure:** `sp_set_session_context`

#### frmHRMeni
- **Fajl:** `HR/frmHRMeni.cs`
- **Otvara forme:** frmHREvidencijaRada (pri otvaranju forme), frmHRGOPravo (pri otvaranju forme), frmHRGOZahtevi (pri otvaranju forme), frmHRKontrolnaTabla (kartica „Kontrolna tabla rokova“), frmHRLog (kartica „HR log“), frmHRSifarnici (kartica „Pravila odobravanja“), frmHRZahteviLista (kartica „Zahtevi za nabavku“), frmHRZaposleniLista (kartica „Zaposleni“)
- **Tabele:**
  - `fn_Podredjeni` — S (preko HRPrava)
  - `GODopust` — S
  - `GODopustStavke` — S
  - `Korisnici` — S (preko HRPrava)
  - `MainNovi` — S (preko HRPrava)
  - `mainNoviKartice` — S (preko HRPrava)
  - `MainNoviPrava` — S (preko HRPrava)
  - `Zaposleni` — S
- **Procedure:** `sp_set_session_context`

#### frmHRRevers
- **Fajl:** `HR/frmHRRevers.cs`
- **Tabele:**
  - `Drzave` — S (preko HRPolje, HRVrednosti)
  - `fn_Podredjeni` — S (preko HRPrava)
  - `HTZGrupe` — S
  - `HTZOprema` — S
  - `HTZReversi` — SU
  - `Korisnici` — S (preko HRPrava)
  - `MainNovi` — S (preko HRPrava)
  - `mainNoviKartice` — S (preko HRPrava)
  - `MainNoviPrava` — S (preko HRPrava)
  - `Opstine` — S (preko HRPolje, HRVrednosti)
  - `OrganizacioneJedinice` — S (preko HRPolje, HRStampa, HRVrednosti)
  - `Partnerji` — S (preko HRPolje, HRStampa, HRVrednosti)
  - `PrioritetiNabavke` — S (preko HRStampa)
  - `RadnaMesta` — S (preko HRStampa)
  - `RadniOdnosi` — S (preko HRStampa)
  - `Stanice` — S (preko HRPolje, HRVrednosti)
  - `StatusiZahtevaZaNabavku` — S (preko HRStampa)
  - `Valute` — S (preko HRPolje, HRVrednosti)
  - `VrsteDokumenta` — S (preko HRStampa)
  - `VrsteNabavke` — S (preko HRStampa)
  - `vZaposleniTrenutno` — S
  - `ZahteviZaNabavku` — S (preko HRStampa)
  - `ZahtevZaNabavkuOdobrenja` — S (preko HRStampa)
  - `ZahtevZaNabavkuPonude` — S (preko HRStampa)
  - `ZahtevZaNabavkuStavke` — S (preko HRStampa)
  - `Zaposleni` — S
  - `ZaposleniDokumenti` — S (preko HRStampa)
  - `ZaposleniHTZZaduzenja` — S
  - `ZaposleniRadnaMesta` — S (preko HRStampa)
- **Procedure:** `sp_set_session_context`

#### frmHRSifarnici
- **Fajl:** `HR/frmHRSifarnici.cs`
- **Tabele:**
  - `Drzave` — S (preko HRListe, HRPolje, HRVrednosti)
  - `HTZOprema` — S
  - `Opstine` — S (preko HRListe, HRPolje, HRVrednosti)
  - `OrganizacioneJedinice` — S
  - `OsnovGO` — S
  - `Partnerji` — S (preko HRListe, HRPolje, HRVrednosti)
  - `Praznici` — S
  - `RadnaMesta` — S
  - `RadnoMestoNormativHTZ` — S
  - `RadnoMestoObavezniDokumenti` — S
  - `RadnoMestoObavezniPregledi` — S
  - `RasporedRada` — S
  - `Stanice` — S (preko HRListe, HRPolje, HRVrednosti)
  - `Valute` — S (preko HRListe, HRPolje, HRVrednosti)
  - `VrstaRada` — S
  - `VrsteDokumenta` — S
  - `VrstePregleda` — S
  - `Zaposleni` — S (preko HRListe, HRPolje, HRVrednosti)
- **Procedure:** `sp_set_session_context`

#### frmHRZahtev
- **Fajl:** `HR/frmHRZahtev.cs`
- **Tabele:**
  - `Drzave` — S (preko HRListe, HRPolje, HRVrednosti)
  - `fn_Podredjeni` — S (preko HRPrava)
  - `fnHR_ZahtevPlanOdobravanja` — S
  - `HTZOprema` — S
  - `HTZReversi` — S (preko HRStampa)
  - `Korisnici` — S (preko HRPrava)
  - `MainNovi` — S (preko HRPrava)
  - `mainNoviKartice` — S (preko HRPrava)
  - `MainNoviPrava` — S (preko HRPrava)
  - `Opstine` — S (preko HRListe, HRPolje, HRVrednosti)
  - `OrganizacioneJedinice` — S
  - `Partnerji` — S
  - `PrioritetiNabavke` — S
  - `RadnaMesta` — S (preko HRStampa)
  - `RadniOdnosi` — S (preko HRStampa)
  - `Stanice` — S
  - `StatusiStavkiNabavke` — S
  - `StatusiZahtevaZaNabavku` — S
  - `Valute` — S (preko HRListe, HRPolje, HRVrednosti)
  - `VrsteDokumenta` — S (preko HRStampa)
  - `VrsteNabavke` — S
  - `vZaposleniTrenutno` — S
  - `ZahteviZaNabavku` — S
  - `ZahtevZaNabavkuIstorijaStatusa` — S
  - `ZahtevZaNabavkuOdobrenja` — S
  - `ZahtevZaNabavkuPonude` — SD
  - `ZahtevZaNabavkuPrilozi` — SD
  - `ZahtevZaNabavkuStavke` — SUD
  - `Zaposleni` — S
  - `ZaposleniDokumenti` — S (preko HRStampa)
  - `ZaposleniHTZZaduzenja` — S (preko HRStampa)
  - `ZaposleniRadnaMesta` — S (preko HRStampa)
- **Procedure:** `HR_ZahtevPreracunaj`, `sp_set_session_context`
- **Funkcionalnosti (kontrola → handler):**
  - `podaci` — Promena → `PodaciPromena` — čita: PrioritetiNabavke, VrsteNabavke, vZaposleniTrenutno; proc: sp_set_session_context; preko: HRBaza
  - `stavke` — Promenjeno → `Ucitaj` — čita 18 tabela; proc: sp_set_session_context; preko: HRBaza, HRPrava, HRVrednosti

#### frmHRZahteviLista
- **Fajl:** `HR/frmHRZahteviLista.cs`
- **Otvara forme:** frmHRZahtev (pri otvaranju forme)
- **Tabele:**
  - `fn_Podredjeni` — S (preko HRPrava)
  - `fnHR_ZahtevPlanOdobravanja` — S
  - `Korisnici` — S (preko HRPrava)
  - `MainNovi` — S (preko HRPrava)
  - `mainNoviKartice` — S (preko HRPrava)
  - `MainNoviPrava` — S (preko HRPrava)
  - `StatusiZahtevaZaNabavku` — S
  - `VrsteNabavke` — S
  - `vZaposleniTrenutno` — S
  - `ZahteviZaNabavku` — S
  - `ZahtevZaNabavkuOdobrenja` — S
  - `ZahtevZaNabavkuStavke` — S
  - `Zaposleni` — S
- **Procedure:** `sp_set_session_context`

#### frmHRZaposleni
- **Fajl:** `HR/frmHRZaposleni.cs`
- **Otvara forme:** frmHRRevers (metoda `PodesiDodatneKomande`)
- **Tabele:**
  - `BeleskaORadu` — S (preko HRBeleske)
  - `Drzave` — S (preko HRListe, HRPolje, HRVrednosti)
  - `fn_Podredjeni` — S (preko HRBeleske, HRGO, HRPrava)
  - `GODopust` — S
  - `GODopustStavke` — S
  - `GrupeDokumenata` — S
  - `HTZOprema` — S
  - `HTZReversi` — S
  - `Korisnici` — S (preko HRPrava)
  - `MainNovi` — S (preko HRPrava)
  - `mainNoviKartice` — S (preko HRPrava)
  - `MainNoviPrava` — S (preko HRPrava)
  - `OceneSposobnosti` — S
  - `Opstine` — S (preko HRListe, HRPolje, HRVrednosti)
  - `OrganizacioneJedinice` — S
  - `OsnovGO` — S
  - `Partnerji` — S (preko HRListe, HRPolje, HRStampa, HRVrednosti)
  - `PrioritetiNabavke` — S (preko HRStampa)
  - `RadnaMesta` — S
  - `RadniOdnosi` — S
  - `RasporedRada` — S
  - `RazloziRazduzenja` — S
  - `Srodstva` — S
  - `Stanice` — S
  - `StatusiZahtevaZaNabavku` — S (preko HRStampa)
  - `StatusZahteva` — S
  - `StepenObrazovanja` — S
  - `TezinePovreda` — S
  - `Valute` — S (preko HRListe, HRPolje, HRVrednosti)
  - `VrsteDisciplinskihMera` — S
  - `VrsteDokumenta` — S
  - `VrsteNabavke` — S (preko HRStampa)
  - `VrstePregleda` — S
  - `VrsteUgovora` — S
  - `vZaposleniRokovi` — S
  - `vZaposleniTrenutno` — S
  - `vZaposleniUpozorenja` — S
  - `ZahteviZaNabavku` — S (preko HRStampa)
  - `ZahtevZaNabavkuOdobrenja` — S (preko HRStampa)
  - `ZahtevZaNabavkuPonude` — S (preko HRStampa)
  - `ZahtevZaNabavkuStavke` — S (preko HRStampa)
  - `Zaposleni` — SIU
  - `ZaposleniClanoviPorodice` — S
  - `ZaposleniDisciplinske` — S
  - `ZaposleniDokumenti` — S
  - `ZaposleniHitniKontakti` — S
  - `ZaposleniHTZZaduzenja` — S
  - `ZaposleniMentorBeleske` — S
  - `ZaposleniMentori` — S
  - `ZaposleniOcene` — S
  - `ZaposleniOsnovGO` — S
  - `ZaposleniPovrede` — S
  - `ZaposleniPrilozi` — SD
  - `ZaposleniRadnaMesta` — S
  - `ZaposleniSkolovanje` — S
  - `ZaposleniSlike` — SD
  - `ZaposleniStatusi` — S
  - `ZaposleniUgovori` — S
  - `ZaposleniZdravstveniPregledi` — S
- **Procedure:** `sp_GO_Otkazi`, `sp_GO_PodnesiZahtev`, `sp_set_session_context`
- **Funkcionalnosti (kontrola → handler):**
  - `tUgovori` — Promenjeno → `OsveziZaglavlje` — otvara: HRBedz; čita: Zaposleni, vZaposleniTrenutno, vZaposleniUpozorenja; proc: sp_set_session_context; preko: HRBaza
  - `t` — Promenjeno → `OsveziZaglavlje` — otvara: HRBedz; čita: Zaposleni, vZaposleniTrenutno, vZaposleniUpozorenja; proc: sp_set_session_context; preko: HRBaza

#### frmHRZaposleniLista
- **Fajl:** `HR/frmHRZaposleniLista.cs`
- **Otvara forme:** frmHRZaposleni (pri otvaranju forme)
- **Tabele:**
  - `vZaposleniTrenutno` — S
  - `vZaposleniUpozorenja` — S
- **Procedure:** `sp_set_session_context`

### Kapija (4)

#### frmKamionDetalji
- **Fajl:** `Kapija/frmKamionDetalji.cs`  
- **Naslov forme:** „Kamion unosna forma“
- **Tabele:**
  - `Kapija` — S
  - `StatusKapija` — S
- **Procedure:** `DeleteKapija`, `InsertKapija`, `UpdateKapija`, `UpdateKapijaNalogID`, `UpdateKapijaNeslaganje`, `UpdateStatusKamionaKapija`
- **Funkcionalnosti (kontrola → handler):**
  - `button20` — klik → `button20_Click` — proc: DeleteKapija, InsertKapija, UpdateKapija, UpdateKapijaNalogID, UpdateKapijaNeslaganje, UpdateStatusKamionaKapija; preko: InsertKapija
  - `button21` — klik → `button21_Click` — proc: DeleteKapija, InsertKapija, UpdateKapija, UpdateKapijaNalogID, UpdateKapijaNeslaganje, UpdateStatusKamionaKapija; preko: InsertKapija

#### frmKapija1
- **Fajl:** `MainLeget/Kapija/frmKapija1.cs`  
- **Naslov forme:** „frmKapija1“
- **Otvara forme:** frmKamionDetalji („Novi dolazak“), frmKapijaOtvoreniNalozi („Lista naloga u NAJAVI“), frmPregledKamiona („Lista SVIH naloga“)
- **Funkcionalnosti (kontrola → handler):**
  - „Novi dolazak“ — klik → `sfButton1_Click`
  - „Lista SVIH naloga“ — klik → `sfButton6_Click`
  - „Lista naloga u NAJAVI“ — klik → `sfButton4_Click` — otvara: frmKapijaOtvoreniNalozi

#### frmKapijaOtvoreniNalozi
- **Fajl:** `Kapija/frmKapijaOtvoreniNalozi.cs`  
- **Naslov forme:** „Kapija nalozi“
- **Otvara forme:** frmOtpremaKontejneraKamionomIzKontejnera (metoda `FormirajRadniNalog`), frmPrijemVozaIzPlana (metoda `FormirajRadniNalog`)
- **Tabele:**
  - `IzvozKonacna` — S
  - `Kapija` — S
  - `MestaUtovara` — S
  - `PrijemKontejneraVoz` — S
  - `RadniNalogInterni` — S
  - `RadniNalogInterniPotvrda` — S
  - `Scenario` — S
  - `TipKontenjera` — S
- **Procedure:** `DelRadniNalogInterniSaDokumentima`, `DeleteIzvozKonacnaNHM`, `DeleteIzvozKonacnaVrstaManipulacije`, `DeleteIzvozKonacnaVrstaRobeHS`, `DeleteIzvozNHM`, `DeleteIzvozSve`, `DeleteIzvozVrstaManipulacije`, `DeleteIzvozVrstaRobeHS`, `DeleteKapija`, `DeleteKontejnerIzvoz`, `DeleteRNMedjuskladisni`, `DeleteRNOtpremaCirade`, `DeleteRNOtpremaPlatforme`, `DeleteRNOtpremaPlatforme2`, `DeleteRNOtpremaVoza`, `DeleteRNPregledIpostavkaKontejnera`, `DeleteRNPregledIspraznjenogKontejnera`, `DeleteRNPrijemCirade`, `DeleteRNPrijemPlatforme`, `DeleteRNPrijemPlatforme2`, `DeleteRNPrijemVoza`, `DeleteRNPrijemVoza2`, `DeleteRadniNalogInterni`, `DeleteUvozKonacna`, `DeleteUvozKonacnaNHM`, `DeleteUvozKonacnaNapomenePozicioniranja`, `DeleteUvozKonacnaVrstaManipulacije`, `DeleteUvozKonacnaVrstaManipulacijeKamionska`, `DeleteUvozKonacnaVrstaRobeHS`, `DeleteUvozNHM`, `DeleteUvozNHMDiana`, `DeleteUvozNapomenePozicioniranja`, `DeleteUvozTerminalskeManipulacije`, `DeleteUvozTerminalskeManipulacijeKonacna`, `DeleteUvozUslugaDokumenta`, `DeleteUvozUvoznici`, `DeleteUvozVrstaManTerminalskih`, `DeleteUvozVrstaManipulacije`, `DeleteUvozVrstaManipulacijeKamionska`, `DeleteUvozVrstaRobeHS`, `DeleteUvozkonacnaNHMDiana`, `DeleteVaganje`, `DozvoliIzlaz`, `InsUbaciDodatnuUsluguIzvoz`, `InsUbaciDodatnuUsluguUvoz`, `InsertIzvozKonacnaNHM`, `InsertIzvozKonacnaVMIzUvoza`, `InsertIzvozKonacnaVrstaManipulacije`, `InsertIzvozNHM`, `InsertIzvozPorudzbenica`, `InsertIzvozVMIzUvoza`, `InsertIzvozVrstaManipulacije`, `InsertIzvozVrstaRobeHS`, `InsertIzvozkonacnaVrstaRobeHS`, `InsertKapija`, `InsertKontejnerLog`, `InsertRN10PregledCeoVoz`, `InsertRN11PregledCeoVozOtprema`, `InsertRN12Medjuskladisni`, `InsertRN4PrPlatformeKamIzvoz`, `InsertRN4PrPlatformeKamUvoz`, `InsertRN5PrPlatFormeKam`, `InsertRN6OtPlatFormeKam`, `InsertRN7OtpremaOtpremaPlatKam`, `InsertRN8OtpremaCiradeKam`, `InsertRN8OtpremaCiradeKamIzvoz`, `InsertRN9PrijemCiradeKam`, `InsertRNOtpremaCirade`, `InsertRNOtpremaPlatforme`, `InsertRNOtpremaPlatforme2`, `InsertRNOtpremaVoza`, `InsertRNOtpremaVozaCeoVoz`, `InsertRNPregledIpostavkaKontejnera`, `InsertRNPregledIspraznjenogKontejnera`, `InsertRNPrijemCirade`, `InsertRNPrijemPlatforme`, `InsertRNPrijemPlatforme2`, `InsertRNPrijemVoza`, `InsertRNPrijemVoza2`, `InsertRNPrijemVozaCeoVoz`, `InsertRNPrijemVozaCeoVozPretovar`, `InsertRadniNalogInterni`, `InsertRadniNalogInterni2`, `InsertRadniNalogInterniIzvoz`, `InsertRadniNalogInterniIzvoz2`, `InsertRadniNalogInterniIzvozDodatne`, `InsertRadniNalogInterniPotvrda`, `InsertRadniNalogInterniSkladistePotvrda`, `InsertRadniNalogInterniStorno`, `InsertUvozKonacbaVrstaRobeHS`, `InsertUvozKonacna`, `InsertUvozKonacnaNHM`, `InsertUvozKonacnaNHMDiana`, `InsertUvozKonacnaNapomenePozicioniranja`, `InsertUvozKonacnaVrstaManipulacije`, `InsertUvozNHM`, `InsertUvozNHMDiana`, `InsertUvozNapomenePozicioniranja`, `InsertUvozUslugaDokument`, `InsertUvozUvoznici`, `InsertUvozVrstaManipulacije`, `InsertUvozVrstaRobeHS`, `InsertVaganje`, `KopirajKontejnerIzvoz`, `ObavljenPregled`, `PokreniSkladisninuKontejnera`, `PrebaciNHMUvozUvozKonacna`, `PrebaciVrsterobeHSUvozUvozKonacna`, `PrenesiIzPlanUtovaraUPlanUtovara`, `PrenesiKontejnerTekuceUArhiv`, `PrenesiUPlanUtovaraSelektovano`, `PromeniSaPrijemniceUK`, `PromeniStatusKalmar`, `PromeniStatusKapijaOdlazak`, `PromeniStatusPretovareno`, `PromeniStatusPrijemPotvrdioKapiju`, `PromeniStatusPrijemPotvrdioVP`, `UbaciURadniNalogInterniBeyScenarija`, `UpdCenaIzvozKonacnaVrstaManipulacije`, `UpdCenaIzvozVrstaManipulacije`, `UpdCenaUvozKonacnaVrstaManipulacije`, `UpdCenaUvozVrstaManipulacije`, `UpdPDVIzvozKonacnaVrstaManipulacije`, `UpdPDVIzvozVrstaManipulacije`, `UpdPlatiocaIzvozKonacnaVrstaManipulacije`, `UpdPlatiocaIzvozVrstaManipulacije`, `UpdPlatiocaUvozKonacnaVrstaManipulacije`, `UpdPlatiocaUvozVrstaManipulacije`, `UpdPokretFormaStatus`, `UpdateBrojKolaIzExcela`, `UpdateBrojKolaIzExcelaVoz`, `UpdateIZCIRA`, `UpdateIzvoz`, `UpdateIzvozKonacna`, `UpdateIzvozKonacnaVaganjeCirada`, `UpdateIzvozKonacnaVaganjePlatforma`, `UpdateIzvozPorudzbenica`, `UpdateIzvozPorudzbenicaCarina`, `UpdateIzvozPorudzbenicaPojedinacni`, `UpdateIzvozPorudzbenicaRelacija`, `UpdateIzvozStatus`, `UpdateIzvozStatusTerminal`, `UpdateIzvozTerminalIzbor`, `UpdateKapija`, `UpdateKapijaNalogID`, `UpdateKapijaNeslaganje`, `UpdateRN12MedjuskladisniSkladiste`, `UpdateRN12Uradjene`, `UpdateRN1Uradjene`, `UpdateRN1UradjeneCIR`, `UpdateRN1UradjeneS`, `UpdateRN1UradjeneVP`, `UpdateRN2Uradjene`, `UpdateRN3Uradjene`, `UpdateRN4PotrebanCIR`, `UpdateRN4PrivremenoSkladiste`, `UpdateRN4SkladisteIzCira`, `UpdateRN4Uradjene`, `UpdateRN4UradjeneCIR`, `UpdateRN4UradjenePostavljen`, `UpdateRN4UradjeneVizuelni`, `UpdateRN5PrijemSkladiste`, `UpdateRN5Uradjene`, `UpdateRN5UradjeneCIR`, `UpdateRN5UradjeneS`, `UpdateRN6OtpremaSkladiste`, `UpdateRN6PotrebanCIR`, `UpdateRN6Uradjene`, `UpdateRN6UradjeneVizuelni`, `UpdateRN7Uradjene`, `UpdateRN8Uradjene`, `UpdateRN8UradjeneCIR`, `UpdateRN9PretovarCIRADE`, `UpdateRN9Uradjen`, `UpdateRNIVozFormiran`, `UpdateRNMedjuskladisni`, `UpdateRNOtpremaCirade`, `UpdateRNOtpremaPlatforme`, `UpdateRNOtpremaPlatforme2`, `UpdateRNOtpremaVoza`, `UpdateRNPregledIpostavkaKontejnera`, `UpdateRNPregledIspraznjenogKontejnera`, `UpdateRNPrijemCirade`, `UpdateRNPrijemPlatforme`, `UpdateRNPrijemPlatforme2`, `UpdateRNPrijemVoza`, `UpdateRNPrijemVoza2`, `UpdateRNPrijemVozaSkladiste`, `UpdateRadniNalogInterni`, `UpdateRadniNalogInterniFormiran`, `UpdateRadniNalogInterniGenerisan`, `UpdateRadniNalogInterniZavrsen`, `UpdateStatusKamionaKapija`, `UpdateUvozKonacna`, `UpdateUvozKonacnaManFaktura`, `UpdateUvozKonacnaManZaFakturisanje`, `UpdateUvozKonacnaObjedinjenja`, `UpdateUvozKonacnaPK`, `UpdateUvozKonacnaUC`, `UpdateUvozNHMTezine`, `UpdateVaganje`, `VratiIzPlanaUtovaraSelektovano`, `ZatvoriSkladisninuKontejnera`, `ZavrsiDodatnuUsluguIzvozZadata`, `ZavrsiDodatnuUsluguUvozZadata`, `spIzbrisiKUScenario`, `spPrenesiKontejnerIzNerasporedjenihUPrijPlatforma`, `spPrenesiKontejnerIzPlanaNaPrijemnicu`, `spPrenesiKontejnerIzPlanaNaPrijemnicuIzvoz`, `spPrenesiKontejnerIzPlanaNaPrijemnicuIzvozPoIdNadredjenog`, `spPrenesiPlanUtovaraUPrijemPlatforma`, `spPrenesiPlanUtovaraUPrijemPlatformaIzvoz`, `spPrenesiPlanUtovaraUPrijemVoz`, `spStornijaRNI`, `updRN4PrPlatformeKamIzvozPusti`, `updRadniNalogInterniPrijemnicaPotvrdjen`, `updRadniNalogInterniRukovalacPotvrdjen`, `updRadniNalogInterniSkladistarPotvrdio`, `updRadniNalogInterniSkladistePotvrdjen`
- **Funkcionalnosti (kontrola → handler):**
  - „Potvrdi odlazak“ — klik → `button3_Click` — čita: RadniNalogInterni, RadniNalogInterniPotvrda; proc: DelRadniNalogInterniSaDokumentima, DeleteRadniNalogInterni, DozvoliIzlaz, InsertRadniNalogInterni, InsertRadniNalogInterni2, InsertRadniNalogInterniIzvoz; preko: InsertRadniNalogInterni
  - „Refresh“ — klik → `button2_Click`
  - „Prijavi neslaganje“ — klik → `button1_Click` — proc: DeleteKapija, InsertKapija, UpdateKapija, UpdateKapijaNalogID, UpdateKapijaNeslaganje, UpdateStatusKamionaKapija; preko: InsertKapija
  - „Potvrdi dolazak“ — klik → `button27_Click` — proc: DeleteKapija, InsertKapija, UpdateKapija, UpdateKapijaNalogID, UpdateKapijaNeslaganje, UpdateStatusKamionaKapija; preko: InsertKapija
  - „gridGroupingControl2“ — klik na ćeliju → `gridGroupingControl2_TableControlCellClick`

#### frmPregledKamiona
- **Fajl:** `Kapija/frmPregledKamiona.cs`  
- **Naslov forme:** „Pregled kamiona“
- **Tabele:**
  - `Kapija` — S
  - `StatusKapija` — S
- **Procedure:** `DeleteKapija`, `InsertKapija`, `UpdateKapija`, `UpdateKapijaNalogID`, `UpdateKapijaNeslaganje`, `UpdateStatusKamionaKapija`
- **Funkcionalnosti (kontrola → handler):**
  - „Promena statusa“ — klik → `button3_Click` — čita: StatusKapija
  - „Otvori“ — klik → `button1_Click` — otvara: frmKamionDetalji
  - „Save“ — klik → `btnSnimi_Click` — proc: DeleteKapija, InsertKapija, UpdateKapija, UpdateKapijaNalogID, UpdateKapijaNeslaganje, UpdateStatusKamionaKapija; preko: InsertKapija
  - „Close“ — klik → `button4_Click`

### Podešavanja sistema (7)

#### frmPozicija
- **Fajl:** `TrackModal/Sifarnici/frmPozicija.cs`  
- **Naslov forme:** „Pozicije“
- **Tabele:**
  - `Pozicija` — S
  - `Skladista` — S
  - `SkladistaGrupa` — S
  - `SkladisteObjekat` — S
  - `TipZone` — S (preko HijerarhijaSkladista)
- **Procedure:** `AktivirajPozicija`, `DeAktivirajPozicija`, `DeletePozicija`, `InsertPozicija`, `UpdatePozicija`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click` — čita: Pozicija, Skladista, TipZone; preko: HijerarhijaSkladista
  - „tsSave“ — klik → `tsSave_Click` — čita: Pozicija, Skladista, TipZone; proc: AktivirajPozicija, DeAktivirajPozicija, DeletePozicija, InsertPozicija, UpdatePozicija; preko: HijerarhijaSkladista, InsertPozicija
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: AktivirajPozicija, DeAktivirajPozicija, DeletePozicija, InsertPozicija, UpdatePozicija; preko: InsertPozicija
  - „toolStripButton1“ — klik → `tsPrvi_Click` — čita: Pozicija
  - „toolStripButton1“ — klik → `tsNazad_Click` — čita: Pozicija
  - „toolStripButton1“ — klik → `tsNapred_Click` — čita: Pozicija
  - „toolStripButton1“ — klik → `tsPoslednja_Click` — čita: Pozicija
  - „gridGroupingControl1“ — SelectedRecordsChanged → `gridGroupingControl1_SelectedRecordsChanged`
  - „?“ — klik → `btnPretraziPoSkladistu_Click` — čita: Pozicija, Skladista
  - „Refresh“ — klik → `button2_Click`
  - „Neaktivne“ — klik → `button1_Click`
  - „Aktivne“ — klik → `button23_Click`
  - `button20` — klik → `tsDelete_Click` — proc: AktivirajPozicija, DeAktivirajPozicija, DeletePozicija, InsertPozicija, UpdatePozicija; preko: InsertPozicija
  - `button21` — klik → `tsSave_Click` — čita: Pozicija, Skladista, TipZone; proc: AktivirajPozicija, DeAktivirajPozicija, DeletePozicija, InsertPozicija, UpdatePozicija; preko: HijerarhijaSkladista, InsertPozicija
  - `button22` — klik → `tsNew_Click` — čita: Pozicija, Skladista, TipZone; preko: HijerarhijaSkladista
  - `this` — otvaranje forme → `frmPozicija_Load` — čita: Pozicija, Skladista, TipZone; preko: HijerarhijaSkladista
  - `cboSkladiste` — SelectionChangeCommitted → `cboSkladiste_SelectedIndexChanged` — čita: Pozicija, Skladista, TipZone; preko: HijerarhijaSkladista

#### frmSkladista
- **Fajl:** `TrackModal/Sifarnici/frmSkladista.cs`  
- **Naslov forme:** „Polja“
- **Tabele:**
  - `Partnerji` — S
  - `Pozicija` — S (preko HijerarhijaSkladista)
  - `Skladista` — S
  - `SkladistaGrupa` — S
  - `SkladisteObjekat` — S
  - `TipKontenjera` — S
  - `TipPalete` — S
  - `TipSkladista` — S
  - `TipZone` — S (preko HijerarhijaSkladista)
  - `uvKvalitetKontejnera` — S
- **Procedure:** `AktivirajSkladista`, `DeAktivirajSkladista`, `DeleteCarinskoStavke`, `DeleteMagacinskiBrojCarinski`, `DeleteRnCarinskoSkladisteDodatneUsluge`, `DeleteSkladista`, `InsertCarinskoStavke`, `InsertMagacinskiBrojCarinski`, `InsertPrijemnicaPalete`, `InsertRNCarinskoPrijemnicaStavke`, `InsertRNCarinskoRukovalac`, `InsertRNCarinskoSkladisteOtpremnica`, `InsertRNCarinskoSkladistePrijemnica`, `InsertRadniNalogInterniSkladista`, `InsertRadniNalogSkladista`, `InsertRnCarinskoSkladisteDodatneUsluge`, `InsertRnCarinskoSkladisteOtpremaStavkePom`, `InsertSkladista`, `StornoRadniNalogInterni`, `UpdateCarinskoStavke`, `UpdateMagacinskiBrojCarinski`, `UpdateRNCarinskoSkladisteOtpremnica`, `UpdateRNCarinskoSkladistePrijemnica`, `UpdateRNInterniSkladista`, `UpdateRadniNalogSkladista`, `UpdateSkladista`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click_1`
  - „tsSave“ — klik → `tsSave_Click` — čita: Pozicija, Skladista, SkladisteObjekat, TipZone; proc: AktivirajSkladista, DeAktivirajSkladista, DeleteCarinskoStavke, DeleteMagacinskiBrojCarinski, DeleteRnCarinskoSkladisteDodatneUsluge, DeleteSkladista; preko: HijerarhijaSkladista, InsertSkladista
  - „toolStripButton1“ — klik → `tsDelete_Click` — čita: Pozicija, Skladista, TipZone; proc: AktivirajSkladista, DeAktivirajSkladista, DeleteCarinskoStavke, DeleteMagacinskiBrojCarinski, DeleteRnCarinskoSkladisteDodatneUsluge, DeleteSkladista; preko: HijerarhijaSkladista, InsertSkladista
  - „toolStripButton1“ — klik → `tsPrvi_Click` — čita: Skladista
  - „toolStripButton1“ — klik → `tsNazad_Click` — čita: Skladista
  - „toolStripButton1“ — klik → `tsNapred_Click` — čita: Skladista
  - „toolStripButton1“ — klik → `tsPoslednja_Click` — čita: Skladista
  - „gridGroupingControl1“ — SelectedRecordsChanged → `gridGroupingControl1_SelectedRecordsChanged`
  - `button20` — klik → `tsDelete_Click` — čita: Pozicija, Skladista, TipZone; proc: AktivirajSkladista, DeAktivirajSkladista, DeleteCarinskoStavke, DeleteMagacinskiBrojCarinski, DeleteRnCarinskoSkladisteDodatneUsluge, DeleteSkladista; preko: HijerarhijaSkladista, InsertSkladista
  - `button21` — klik → `tsSave_Click` — čita: Pozicija, Skladista, SkladisteObjekat, TipZone; proc: AktivirajSkladista, DeAktivirajSkladista, DeleteCarinskoStavke, DeleteMagacinskiBrojCarinski, DeleteRnCarinskoSkladisteDodatneUsluge, DeleteSkladista; preko: HijerarhijaSkladista, InsertSkladista
  - `button22` — klik → `tsNew_Click` — čita: Pozicija, Skladista, TipZone; preko: HijerarhijaSkladista
  - „Neaktivne“ — klik → `button1_Click`
  - „Aktivne“ — klik → `button23_Click`
  - „Refresh“ — klik → `button2_Click`
  - `this` — otvaranje forme → `frmSkladista_Load` — čita 7 tabela; preko: HijerarhijaSkladista
  - `cboGrupaPolja` — SelectionChangeCommitted → `cboGrupaPolja_SelectedIndexChanged` — čita: Pozicija, Skladista, TipZone; preko: HijerarhijaSkladista

#### frmSkladisteObjekat
- **Fajl:** `TrackModal/Sifarnici/frmSkladisteObjekat.cs`  
- **Naslov forme:** „Objekti“
- **Tabele:**
  - `Pozicija` — S (preko HijerarhijaSkladista)
  - `Skladista` — S
  - `SkladistaGrupa` — S
  - `SkladisteObjekat` — S
  - `TipSkladista` — S
  - `TipZone` — S (preko HijerarhijaSkladista)
  - `Zone` — S
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click` — čita: Pozicija, Skladista, TipZone; preko: HijerarhijaSkladista
  - „Sačuvaj“ — klik → `tsSave_Click` — čita: Pozicija, Skladista, TipZone; preko: HijerarhijaSkladista
  - „Obriši“ — klik → `tsDelete_Click` — čita: Pozicija, Skladista, TipZone; preko: HijerarhijaSkladista
  - „Refresh“ — klik → `button2_Click`
  - „Neaktivne“ — klik → `button1_Click`
  - „Aktivne“ — klik → `button23_Click`
  - `button20` — klik → `tsDelete_Click` — čita: Pozicija, Skladista, TipZone; preko: HijerarhijaSkladista
  - `button21` — klik → `tsSave_Click` — čita: Pozicija, Skladista, TipZone; preko: HijerarhijaSkladista
  - `button22` — klik → `tsNew_Click` — čita: Pozicija, Skladista, TipZone; preko: HijerarhijaSkladista
  - `cboLokacija` — izbor → `cboLokacija_SelectedIndexChanged` — čita: Pozicija, Skladista, TipZone; preko: HijerarhijaSkladista
  - „gridGroupingControl1“ — SelectedRecordsChanged → `gridGroupingControl1_SelectedRecordsChanged`
  - `this` — otvaranje forme → `frmSkladisteObjekat_Load` — čita: Pozicija, Skladista, TipSkladista, TipZone; preko: HijerarhijaSkladista

#### frmTipKontejnera
- **Fajl:** `TrackModal/Sifarnici/frmTipKontejnera.cs`  
- **Naslov forme:** „Tip kontejnera“
- **Tabele:**
  - `TipKontenjera` — S
- **Procedure:** `DeleteTipKontenjera`, `InsertTipKontenjera`, `UpdateTipKontenjera`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — proc: DeleteTipKontenjera, InsertTipKontenjera, UpdateTipKontenjera; preko: InsertTipKontejnera
  - „toolStripButton1“ — klik → `tsDelete_Click` — proc: DeleteTipKontenjera, InsertTipKontenjera, UpdateTipKontenjera; preko: InsertTipKontejnera
  - „toolStripButton1“ — klik → `tsPrvi_Click` — čita: TipKontenjera
  - „toolStripButton1“ — klik → `tsNazad_Click` — čita: TipKontenjera
  - „toolStripButton1“ — klik → `tsNapred_Click` — čita: TipKontenjera
  - „toolStripButton1“ — klik → `tsPoslednja_Click` — čita: TipKontenjera
  - „Dužina:“ — klik → `label11_Click`
  - `button20` — klik → `tsDelete_Click` — proc: DeleteTipKontenjera, InsertTipKontenjera, UpdateTipKontenjera; preko: InsertTipKontejnera
  - `button21` — klik → `tsSave_Click` — proc: DeleteTipKontenjera, InsertTipKontenjera, UpdateTipKontenjera; preko: InsertTipKontejnera

#### frmZone
- **Fajl:** `TrackModal/Sifarnici/frmZone.cs`  
- **Naslov forme:** „Zone“
- **Tabele:**
  - `Pozicija` — S (preko HijerarhijaSkladista)
  - `Skladista` — S (preko HijerarhijaSkladista)
  - `TipZone` — S
  - `Zone` — S
- **Procedure:** `AktivirajZonaPozicije`, `DeAktivirajZonaPozicije`, `DeleteZone`, `InsertZone`, `UpdateZone`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click`
  - „tsSave“ — klik → `tsSave_Click` — čita: Pozicija, Skladista, TipZone; proc: AktivirajZonaPozicije, DeAktivirajZonaPozicije, DeleteZone, InsertZone, UpdateZone; preko: HijerarhijaSkladista, InsertZone
  - „toolStripButton1“ — klik → `tsDelete_Click` — čita: Pozicija, Skladista, TipZone; proc: AktivirajZonaPozicije, DeAktivirajZonaPozicije, DeleteZone, InsertZone, UpdateZone; preko: HijerarhijaSkladista, InsertZone
  - `button20` — klik → `tsDelete_Click` — čita: Pozicija, Skladista, TipZone; proc: AktivirajZonaPozicije, DeAktivirajZonaPozicije, DeleteZone, InsertZone, UpdateZone; preko: HijerarhijaSkladista, InsertZone
  - `button21` — klik → `tsSave_Click` — čita: Pozicija, Skladista, TipZone; proc: AktivirajZonaPozicije, DeAktivirajZonaPozicije, DeleteZone, InsertZone, UpdateZone; preko: HijerarhijaSkladista, InsertZone
  - „gridGroupingControl1“ — SelectedRecordsChanged → `gridGroupingControl1_SelectedRecordsChanged`
  - „Refresh“ — klik → `button2_Click`
  - „Neaktivne“ — klik → `button1_Click` — čita: Pozicija, Skladista, TipZone; preko: HijerarhijaSkladista
  - „Aktivne“ — klik → `button23_Click` — čita: Pozicija, Skladista, TipZone; preko: HijerarhijaSkladista

#### Podesavanje1
- **Fajl:** `MainLeget/Podesavanjesistema/Podesavanje1.cs`  
- **Naslov forme:** „MainLeget“
- **Otvara forme:** frmPartnerji („Partneri“), frmPozicija („Pozicije“), frmSkladista („Polja“), frmSkladisteObjekat („Objekti“), frmTipKontejnera („Tipovi kontejnera“), frmZone („Zone“), SkladisteGrupa („Lokacije“)
- **Funkcionalnosti (kontrola → handler):**
  - „Lokacije“ — klik → `btnLogistikaIzvozaPM2_Click`
  - „Objekti“ — klik → `btnObjekti_Click`
  - „Tipovi kontejnera“ — klik → `btnLogistikaIzvozaPM5_Click` — otvara: frmTipKontejnera
  - „Pozicije“ — klik → `btnLogistikaIzvozaPM4_Click` — otvara: frmPozicija
  - „Polja“ — klik → `btnLogistikaIzvozaPM3_Click` — otvara: frmSkladista
  - „Zone“ — klik → `btnLogistikaIzvozaPM1_Click`
  - „Partneri“ — klik → `sfPartnerji_Click`

#### SkladisteGrupa
- **Fajl:** `TrackModal/Sifarnici/SkladisteGrupa.cs`  
- **Naslov forme:** „Lokacije“
- **Tabele:**
  - `Carinarnice` — S
  - `Pozicija` — S (preko HijerarhijaSkladista)
  - `Skladista` — S (preko HijerarhijaSkladista)
  - `SkladistaGrupa` — S
  - `TipSkladista` — S
  - `TipZone` — S
  - `Zone` — S
- **Procedure:** `AktivirajSkladistaGrupa`, `DeAktivirajSkladistaGrupa`, `DeleteSkladisteGrupa`, `InsertSkladistaGrupa`, `UpdateSkladistaGrupa`
- **Funkcionalnosti (kontrola → handler):**
  - „Novi“ — klik → `tsNew_Click` — čita: Pozicija, Skladista, TipZone; preko: HijerarhijaSkladista
  - „tsSave“ — klik → `tsSave_Click` — čita: Pozicija, Skladista, TipZone, Zone; proc: AktivirajSkladistaGrupa, DeAktivirajSkladistaGrupa, DeleteSkladisteGrupa, InsertSkladistaGrupa, UpdateSkladistaGrupa; preko: HijerarhijaSkladista, InsertSkladistaGrupa
  - „toolStripButton1“ — klik → `tsDelete_Click` — čita: Pozicija, Skladista, TipZone; proc: AktivirajSkladistaGrupa, DeAktivirajSkladistaGrupa, DeleteSkladisteGrupa, InsertSkladistaGrupa, UpdateSkladistaGrupa; preko: HijerarhijaSkladista, InsertSkladistaGrupa
  - `button20` — klik → `tsDelete_Click` — čita: Pozicija, Skladista, TipZone; proc: AktivirajSkladistaGrupa, DeAktivirajSkladistaGrupa, DeleteSkladisteGrupa, InsertSkladistaGrupa, UpdateSkladistaGrupa; preko: HijerarhijaSkladista, InsertSkladistaGrupa
  - `button21` — klik → `tsSave_Click` — čita: Pozicija, Skladista, TipZone, Zone; proc: AktivirajSkladistaGrupa, DeAktivirajSkladistaGrupa, DeleteSkladisteGrupa, InsertSkladistaGrupa, UpdateSkladistaGrupa; preko: HijerarhijaSkladista, InsertSkladistaGrupa
  - `button22` — klik → `tsNew_Click` — čita: Pozicija, Skladista, TipZone; preko: HijerarhijaSkladista
  - „gridGroupingControl1“ — SelectedRecordsChanged → `gridGroupingControl1_SelectedRecordsChanged`
  - „Carinsko skladište“ — čekiranje → `chkCarinsko_CheckedChanged`
  - „Neaktivne“ — klik → `button1_Click`
  - „Aktivne“ — klik → `button23_Click`
  - „Refresh“ — klik → `button2_Click`
  - `cboZonaID` — izbor → `cboZonaID_SelectedIndexChanged` — čita: Pozicija, Skladista, TipZone; preko: HijerarhijaSkladista

### Operacije pretovari (9)

#### frmPretovariSkladista
- **Fajl:** `MainLeget/Pretovari/frmPretovariSkladista.cs`  
- **Naslov forme:** „frmPretovariSkladista“

#### frmPretovarPrijem
- **Fajl:** `MainLeget/Pretovari/frmPretovarPrijem.cs`  
- **Naslov forme:** „Prijem“
- **Otvara forme:** frmPrijemKontejneraKamionLegetUvoz („Radni nalog kalmar“), Otpremnica („Radni nalog pretovar“), Prijemnica („Radni nalog pretovar“), RN12MedjuskladisniKontejnera („Radni nalog kalmar“)
- **Tabele:**
  - `IzvozKonacna` — S
  - `organizacioneJedinice` — S
  - `PrijemKontejneraVozStavke` — S
  - `RadniNalogInterni` — S
  - `radninaloginterni` — S
  - `RadniNalogInterniPotvrda` — S
  - `RNMedjuskladisni` — S
- **Procedure:** `DelRadniNalogInterniSaDokumentima`, `DeleteRadniNalogInterni`, `DeleteUvozKonacna`, `DeleteUvozKonacnaNHM`, `DeleteUvozKonacnaNapomenePozicioniranja`, `DeleteUvozKonacnaVrstaManipulacije`, `DeleteUvozKonacnaVrstaManipulacijeKamionska`, `DeleteUvozKonacnaVrstaRobeHS`, `DeleteUvozNHM`, `DeleteUvozNHMDiana`, `DeleteUvozNapomenePozicioniranja`, `DeleteUvozTerminalskeManipulacije`, `DeleteUvozTerminalskeManipulacijeKonacna`, `DeleteUvozUslugaDokumenta`, `DeleteUvozUvoznici`, `DeleteUvozVrstaManTerminalskih`, `DeleteUvozVrstaManipulacije`, `DeleteUvozVrstaManipulacijeKamionska`, `DeleteUvozVrstaRobeHS`, `DeleteUvozkonacnaNHMDiana`, `DozvoliIzlaz`, `InsUbaciDodatnuUsluguIzvoz`, `InsUbaciDodatnuUsluguUvoz`, `InsertRadniNalogInterni`, `InsertRadniNalogInterni2`, `InsertRadniNalogInterniIzvoz`, `InsertRadniNalogInterniIzvoz2`, `InsertRadniNalogInterniIzvozDodatne`, `InsertRadniNalogInterniPotvrda`, `InsertRadniNalogInterniSkladistePotvrda`, `InsertRadniNalogInterniStorno`, `InsertUvozKonacbaVrstaRobeHS`, `InsertUvozKonacna`, `InsertUvozKonacnaNHM`, `InsertUvozKonacnaNHMDiana`, `InsertUvozKonacnaNapomenePozicioniranja`, `InsertUvozKonacnaVrstaManipulacije`, `InsertUvozNHM`, `InsertUvozNHMDiana`, `InsertUvozNapomenePozicioniranja`, `InsertUvozUslugaDokument`, `InsertUvozUvoznici`, `InsertUvozVrstaManipulacije`, `InsertUvozVrstaRobeHS`, `ObavljenPregled`, `PrebaciNHMUvozUvozKonacna`, `PrebaciVrsterobeHSUvozUvozKonacna`, `PrenesiIzPlanUtovaraUPlanUtovara`, `PrenesiUPlanUtovaraSelektovano`, `PromeniSaPrijemniceUK`, `PromeniStatusKalmar`, `PromeniStatusKapijaOdlazak`, `PromeniStatusPretovareno`, `PromeniStatusPrijemPotvrdioKapiju`, `PromeniStatusPrijemPotvrdioVP`, `UbaciURadniNalogInterniBeyScenarija`, `UpdCenaUvozKonacnaVrstaManipulacije`, `UpdCenaUvozVrstaManipulacije`, `UpdPlatiocaUvozKonacnaVrstaManipulacije`, `UpdPlatiocaUvozVrstaManipulacije`, `UpdPokretFormaStatus`, `UpdateBrojKolaIzExcela`, `UpdateBrojKolaIzExcelaVoz`, `UpdateRNIVozFormiran`, `UpdateRadniNalogInterni`, `UpdateRadniNalogInterniFormiran`, `UpdateRadniNalogInterniGenerisan`, `UpdateRadniNalogInterniZavrsen`, `UpdateUvozKonacna`, `UpdateUvozKonacnaManFaktura`, `UpdateUvozKonacnaManZaFakturisanje`, `UpdateUvozKonacnaObjedinjenja`, `UpdateUvozKonacnaPK`, `UpdateUvozKonacnaUC`, `UpdateUvozNHMTezine`, `VratiIzPlanaUtovaraSelektovano`, `ZavrsiDodatnuUsluguIzvozZadata`, `ZavrsiDodatnuUsluguUvozZadata`, `spIzbrisiKUScenario`, `spPrenesiKontejnerIzPlanaNaPrijemnicu`, `spPrenesiKontejnerIzPlanaNaPrijemnicuIzvoz`, `spPrenesiKontejnerIzPlanaNaPrijemnicuIzvozPoIdNadredjenog`, `spPrenesiPlanUtovaraUPrijemPlatforma`, `spPrenesiPlanUtovaraUPrijemPlatformaIzvoz`, `spPrenesiPlanUtovaraUPrijemVoz`, `spStornijaRNI`, `updRadniNalogInterniPrijemnicaPotvrdjen`, `updRadniNalogInterniRukovalacPotvrdjen`, `updRadniNalogInterniSkladistarPotvrdio`, `updRadniNalogInterniSkladistePotvrdjen`
- **Funkcionalnosti (kontrola → handler):**
  - „Kalmar“ — klik → `button5_Click` — proc: DelRadniNalogInterniSaDokumentima, DeleteRadniNalogInterni, DozvoliIzlaz, InsertRadniNalogInterni, InsertRadniNalogInterni2, InsertRadniNalogInterniIzvoz; preko: InsertRadniNalogInterni
  - „Refresh“ — klik → `button8_Click`
  - „Dozvoli izlaz“ — klik → `button4_Click` — proc: DelRadniNalogInterniSaDokumentima, DeleteRadniNalogInterni, DozvoliIzlaz, InsertRadniNalogInterni, InsertRadniNalogInterni2, InsertRadniNalogInterniIzvoz; preko: InsertRadniNalogInterni
  - „Radni nalog vaga“ — klik → `button2_Click`
  - „Radni nalog pretovar“ — klik → `button1_Click` — otvara: Prijemnica, Otpremnica, RN12MedjuskladisniKontejnera; čita: RNMedjuskladisni, RadniNalogInterni
  - „Radni nalog kalmar“ — klik → `button27_Click` — otvara: RN12MedjuskladisniKontejnera; proc: DeleteUvozKonacna, DeleteUvozKonacnaNHM, DeleteUvozKonacnaNapomenePozicioniranja, DeleteUvozKonacnaVrstaManipulacije, DeleteUvozKonacnaVrstaManipulacijeKamionska, DeleteUvozKonacnaVrstaRobeHS; preko: InsertUvozKonacna

#### Otpremnica
- **Fajl:** `RadniNalozi/Otpremnica.cs`  
- **Naslov forme:** „Otpremnica“
- **Otvara forme:** OtpremnicaPregled („Otpremnice pregled“)
- **Tabele:**
  - `Carinarnice` — S
  - `Delavci` — S
  - `Lokac` — S
  - `MagacinskiBrojCarinski` — S
  - `MaticniPodatki` — S
  - `Mesta` — S
  - `MestaUtovara` — S
  - `Narocilo` — S
  - `NarociloPostav` — S
  - `NHM` — S
  - `Partnerji` — S
  - `Pozicija` — S
  - `Promet` — S
  - `RadniNalogSkladista` — S
  - `RNCarinskoSkladisteDodatneUsluge` — S
  - `RNCarinskoSkladisteOtpremaStavkePom` — S
  - `RNCarinskoSkladisteOtpremnica` — S
  - `RNCarinskoSkladisteOtpremnicaStavke` — S
  - `RNCarinskoSkladistePrijemnica` — S
  - `RNCarinskoSkladisteRukovalac` — S
  - `Sklad` — S
  - `sklad` — S
  - `Skladista` — S
  - `Skladiste_Korisnik` — S
  - `TipPalete` — S
  - `Vozila` — S
  - `VrstaCarinskogPostupka` — S
  - `VrstaManipulacije` — S
  - `VrstaVozila` — S
  - `VrstePrevoznogSredstva` — S
- **Procedure:** `AktivirajSkladista`, `DeAktivirajSkladista`, `DeleteCarinskoStavke`, `DeleteMagacinskiBrojCarinski`, `DeletePromet`, `DeleteRnCarinskoSkladisteDodatneUsluge`, `DeleteSkladista`, `InsertCarinskoStavke`, `InsertDobavnica`, `InsertDobavnicaPostav`, `InsertMagacinskiBrojCarinski`, `InsertPrijemnica`, `InsertPrijemnicaPalete`, `InsertPrijemnicaPostav`, `InsertPromet`, `InsertPrometTrans`, `InsertRNCarinskoPrijemnicaStavke`, `InsertRNCarinskoRukovalac`, `InsertRNCarinskoSkladisteOtpremnica`, `InsertRNCarinskoSkladistePrijemnica`, `InsertRadniNalogInterniSkladista`, `InsertRadniNalogSkladista`, `InsertRnCarinskoSkladisteDodatneUsluge`, `InsertRnCarinskoSkladisteOtpremaStavkePom`, `InsertSkladista`, `InsertTransPosPostav`, `PromenaBrojaKontejnera`, `StornoRadniNalogInterni`, `UpdateCarinskoStavke`, `UpdateMagacinskiBrojCarinski`, `UpdatePorudzbenica`, `UpdateRNCarinskoSkladisteOtpremnica`, `UpdateRNCarinskoSkladistePrijemnica`, `UpdateRNInterniSkladista`, `UpdateRadniNalogSkladista`, `UpdateSkladista`
- **Funkcionalnosti (kontrola → handler):**
  - „Izbaci“ — klik → `btnIzbaci_Click`
  - „Dodaj“ — klik → `btnDodaj_Click`
  - „NAPOMENA“ — klik → `label27_Click`
  - „?“ — klik → `btnMagacinskiBroj_Click`
  - „?“ — klik → `btnPrijemnica_Click`
  - „Nazad“ — klik → `btnVratiNalog_Click`
  - „Izdaj nalog“ — klik → `btnIzdajNalog_Click` — čita: RNCarinskoSkladisteDodatneUsluge, RNCarinskoSkladisteRukovalac; proc: AktivirajSkladista, DeAktivirajSkladista, DeleteCarinskoStavke, DeleteMagacinskiBrojCarinski, DeleteRnCarinskoSkladisteDodatneUsluge, DeleteSkladista; preko: InsertSkladista
  - „gridGroupingControl1“ — klik na ćeliju → `gridGroupingControl1_TableControlCellClick`
  - „Nalog za istovar rukovaocima“ — klik → `btnNalog_Click`
  - „Snimi“ — klik → `btnSnimi_Click` — čita: RNCarinskoSkladisteOtpremnica; proc: AktivirajSkladista, DeAktivirajSkladista, DeleteCarinskoStavke, DeleteMagacinskiBrojCarinski, DeleteRnCarinskoSkladisteDodatneUsluge, DeleteSkladista; preko: InsertSkladista
  - `button1` — klik → `button1_Click` — otvara: OtpremnicaPregled; čita: Promet; proc: DeletePromet, InsertDobavnica, InsertDobavnicaPostav, InsertPrijemnica, InsertPrijemnicaPostav, InsertPromet; preko: InsertIsporuka
  - „OPIS ROBE“ — klik → `label6_Click`
  - `btn_Povuci` — klik → `btn_Povuci_Click` — čita: Narocilo, Skladiste_Korisnik, sklad
  - „OSTALA SKLADIŠTA“ — klik → `label6_Click`
  - „BROJ TELEFONA“ — klik → `label6_Click`
  - `button3` — klik → `button3_Click_1` — otvara: OtpremnicaPregled
  - `button4` — klik → `button4_Click` — proc: DeletePromet, InsertDobavnica, InsertDobavnicaPostav, InsertPrijemnica, InsertPrijemnicaPostav, InsertPromet; preko: InsertIsporuka
  - `button23` — klik → `button3_Click_1` — otvara: OtpremnicaPregled
  - `button22` — klik → `button1_Click` — otvara: OtpremnicaPregled; čita: Promet; proc: DeletePromet, InsertDobavnica, InsertDobavnicaPostav, InsertPrijemnica, InsertPrijemnicaPostav, InsertPromet; preko: InsertIsporuka
  - `button5` — klik → `button4_Click` — proc: DeletePromet, InsertDobavnica, InsertDobavnicaPostav, InsertPrijemnica, InsertPrijemnicaPostav, InsertPromet; preko: InsertIsporuka

#### OtpremnicaPregled
- **Fajl:** `RadniNalozi/OtpremnicaPregled.cs`
- **Tabele:**
  - `DObavnica` — S
  - `DobavnicaPostav` — S
  - `Partnerji` — S
  - `Pozicija` — S
  - `Promet` — S
  - `Skladista` — S
- **Funkcionalnosti (kontrola → handler):**
  - `button1` — klik → `button1_Click` — izveštaj: rptOtpremnica.rdlc

#### PretovariMain
- **Fajl:** `MainLeget/Pretovari/PretovariMain.cs`  
- **Naslov forme:** „PretovariMain“
- **Otvara forme:** frmPretovariSkladista („Skladišta“), PretovariPrijem („Pretovar“)
- **Funkcionalnosti (kontrola → handler):**
  - „Pretovar“ — klik → `btnPretovarPrijem_Click` — otvara: PretovariPrijem
  - „Skladišta“ — klik → `btnPretovarSkladista_Click` — otvara: frmPretovariSkladista

#### PretovariPrijem
- **Fajl:** `MainLeget/Pretovari/PretovariPrijem.cs`  
- **Naslov forme:** „PretovariPrijem“
- **Otvara forme:** frmPretovarPrijem („Prijem“)
- **Funkcionalnosti (kontrola → handler):**
  - „Prijem “ — klik → `btnPretovarPrijem_Click` — otvara: frmPretovarPrijem

#### Prijemnica
- **Fajl:** `RadniNalozi/Prijemnica.cs`  
- **Naslov forme:** „Prijemnica“
- **Otvara forme:** PaletePrebacivanje („Prebaci na palete“), TerminalMapFRM („Mapa pozicija“), PrijemnicaPregled („PRETOVAR PREGLED“), ZapisnikOOstecenjuRobe („Zapisnik o oštećenju“)
- **Tabele:**
  - `Artikli` — SU
  - `ArtikliCetvrtiNivo` — S
  - `ArtikliDrugiNivo` — S
  - `ArtikliKatalog` — S (preko ArtikliKatalogDijalozi)
  - `ArtikliKatalogUvoz` — I (preko ArtikliKatalogDijalozi)
  - `ArtikliPrviNivo` — S
  - `ArtikliTreciNivo` — S
  - `Carinarnice` — S
  - `Delavci` — S
  - `Lokac` — S
  - `MagacinskiBrojCarinski` — S
  - `MaticniPodatki` — S
  - `MerskeEnote` — S
  - `Mesta` — S
  - `MestaUtovara` — S
  - `NHM` — S
  - `NNal` — S
  - `NNalP` — S
  - `NPre` — S
  - `NPreP` — S
  - `Partnerji` — S
  - `Pozicija` — S
  - `Promet` — S
  - `RadniNalogInterni` — S
  - `RadniNalogSkladista` — S
  - `RNCarinskoPackingLista` — SIU
  - `RNCarinskoPackingListaStavka` — SIU
  - `RNCarinskoPaleta` — S
  - `RNCarinskoPaletaStavka` — S
  - `RNCarinskoPrijemnicaStavke` — SD
  - `RNCarinskoSkladisteDodatneUsluge` — S
  - `RNCarinskoSkladistePrijemnica` — S
  - `RNCarinskoSkladisteRukovalac` — S
  - `Sklad` — S
  - `sklad` — S
  - `Skladista` — S
  - `SkladistaGrupa` — S
  - `TipPalete` — S
  - `TipRobe` — S
  - `UvozKonacnaNHM` — S
  - `Valute` — S
  - `VrstaCarinskogPostupka` — S
  - `VrstaManipulacije` — S
  - `VrstaVozila` — S
  - `VrstePrevoznogSredstva` — S
- **Procedure:** `AktivirajSkladista`, `DeAktivirajSkladista`, `DeleteCarinskoStavke`, `DeleteMagacinskiBrojCarinski`, `DeletePromet`, `DeleteRNMedjuskladisni`, `DeleteRNOtpremaCirade`, `DeleteRNOtpremaPlatforme`, `DeleteRNOtpremaPlatforme2`, `DeleteRNOtpremaVoza`, `DeleteRNPregledIpostavkaKontejnera`, `DeleteRNPregledIspraznjenogKontejnera`, `DeleteRNPrijemCirade`, `DeleteRNPrijemPlatforme`, `DeleteRNPrijemPlatforme2`, `DeleteRNPrijemVoza`, `DeleteRNPrijemVoza2`, `DeleteRnCarinskoSkladisteDodatneUsluge`, `DeleteSkladista`, `InsertCarinskoStavke`, `InsertDobavnica`, `InsertDobavnicaPostav`, `InsertMagacinskiBrojCarinski`, `InsertPrijemnica`, `InsertPrijemnicaPalete`, `InsertPrijemnicaPostav`, `InsertPromet`, `InsertPrometTrans`, `InsertRN10PregledCeoVoz`, `InsertRN11PregledCeoVozOtprema`, `InsertRN12Medjuskladisni`, `InsertRN4PrPlatformeKamIzvoz`, `InsertRN4PrPlatformeKamUvoz`, `InsertRN5PrPlatFormeKam`, `InsertRN6OtPlatFormeKam`, `InsertRN7OtpremaOtpremaPlatKam`, `InsertRN8OtpremaCiradeKam`, `InsertRN8OtpremaCiradeKamIzvoz`, `InsertRN9PrijemCiradeKam`, `InsertRNCarinskoPrijemnicaStavke`, `InsertRNCarinskoRukovalac`, `InsertRNCarinskoSkladisteOtpremnica`, `InsertRNCarinskoSkladistePrijemnica`, `InsertRNOtpremaCirade`, `InsertRNOtpremaPlatforme`, `InsertRNOtpremaPlatforme2`, `InsertRNOtpremaVoza`, `InsertRNOtpremaVozaCeoVoz`, `InsertRNPregledIpostavkaKontejnera`, `InsertRNPregledIspraznjenogKontejnera`, `InsertRNPrijemCirade`, `InsertRNPrijemPlatforme`, `InsertRNPrijemPlatforme2`, `InsertRNPrijemVoza`, `InsertRNPrijemVoza2`, `InsertRNPrijemVozaCeoVoz`, `InsertRNPrijemVozaCeoVozPretovar`, `InsertRadniNalogInterniSkladista`, `InsertRadniNalogSkladista`, `InsertRnCarinskoSkladisteDodatneUsluge`, `InsertRnCarinskoSkladisteOtpremaStavkePom`, `InsertSkladista`, `InsertTransPosPostav`, `PokreniSkladisninuKontejnera`, `PotvrdiPrijem`, `PrenesiKontejnerTekuceUArhiv`, `PromenaBrojaKontejnera`, `StornoRadniNalogInterni`, `UpdateCarinskoStavke`, `UpdateIZCIRA`, `UpdateMagacinskiBrojCarinski`, `UpdatePorudzbenica`, `UpdateRN12MedjuskladisniSkladiste`, `UpdateRN12Uradjene`, `UpdateRN1Uradjene`, `UpdateRN1UradjeneCIR`, `UpdateRN1UradjeneS`, `UpdateRN1UradjeneVP`, `UpdateRN2Uradjene`, `UpdateRN3Uradjene`, `UpdateRN4PotrebanCIR`, `UpdateRN4PrivremenoSkladiste`, `UpdateRN4SkladisteIzCira`, `UpdateRN4Uradjene`, `UpdateRN4UradjeneCIR`, `UpdateRN4UradjenePostavljen`, `UpdateRN4UradjeneVizuelni`, `UpdateRN5PrijemSkladiste`, `UpdateRN5Uradjene`, `UpdateRN5UradjeneCIR`, `UpdateRN5UradjeneS`, `UpdateRN6OtpremaSkladiste`, `UpdateRN6PotrebanCIR`, `UpdateRN6Uradjene`, `UpdateRN6UradjeneVizuelni`, `UpdateRN7Uradjene`, `UpdateRN8Uradjene`, `UpdateRN8UradjeneCIR`, `UpdateRN9PretovarCIRADE`, `UpdateRN9Uradjen`, `UpdateRNCarinskoSkladisteOtpremnica`, `UpdateRNCarinskoSkladistePrijemnica`, `UpdateRNInterniSkladista`, `UpdateRNMedjuskladisni`, `UpdateRNOtpremaCirade`, `UpdateRNOtpremaPlatforme`, `UpdateRNOtpremaPlatforme2`, `UpdateRNOtpremaVoza`, `UpdateRNPregledIpostavkaKontejnera`, `UpdateRNPregledIspraznjenogKontejnera`, `UpdateRNPrijemCirade`, `UpdateRNPrijemPlatforme`, `UpdateRNPrijemPlatforme2`, `UpdateRNPrijemVoza`, `UpdateRNPrijemVoza2`, `UpdateRNPrijemVozaSkladiste`, `UpdateRadniNalogSkladista`, `UpdateSkladista`, `ZatvoriSkladisninuKontejnera`, `sp_ArtikliKatalog_Uvezi`, `sp_PL_Analiziraj`, `sp_PL_FormirajArtikle`, `sp_PrijemnicaStavka_Snimi`, `updRN4PrPlatformeKamIzvozPusti`
- **Funkcionalnosti (kontrola → handler):**
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
  - „Dodaj“ — klik → `btnDodaj_Click` — piše: RNCarinskoPrijemnicaStavke[D]; čita 19 tabela; preko: PrijemnicaStavke
  - „Nazad“ — klik → `btnNHMNazad_Click`
  - „Dodaj“ — klik → `btnNHMDodaj_Click`
  - „Nazad“ — klik → `btnVratiNalog_Click`
  - „Izdaj nalog“ — klik → `btnIzdajNalog_Click` — čita: RNCarinskoSkladisteDodatneUsluge, RNCarinskoSkladisteRukovalac; proc: AktivirajSkladista, DeAktivirajSkladista, DeleteCarinskoStavke, DeleteMagacinskiBrojCarinski, DeleteRnCarinskoSkladisteDodatneUsluge, DeleteSkladista; preko: InsertSkladista
  - „Nalog rukovaocima“ — klik → `button2_Click`
  - „Mapa pozicija“ — klik → `button1_Click`
  - „Snimi“ — klik → `button24_Click` — piše: RNCarinskoPrijemnicaStavke[D]; čita: RNCarinskoSkladistePrijemnica; proc: AktivirajSkladista, DeAktivirajSkladista, DeleteCarinskoStavke, DeleteMagacinskiBrojCarinski, DeleteRnCarinskoSkladisteDodatneUsluge, DeleteSkladista; preko: InsertSkladista
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
  - „Mapa pozicija“ — klik → `button1_Click_1` — otvara: PrijemnicaPregled; čita: Promet; proc: DeletePromet, DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza; preko: InsertIsporuka, InsertRN
  - `btn_Povuci` — klik → `btn_Povuci_Click` — čita: Delavci, NNal, NPre, NPreP, sklad
  - „Nalog rukovaocima“ — klik → `button2_Click_1`
  - `dataGridView2` — izbor → `dataGridView2_SelectionChanged` — čita: MaticniPodatki, NNalP
  - `btn_Izaberi` — klik → `btn_Izaberi_Click` — čita: MaticniPodatki, NNal, NNalP, sklad
  - „OPIS ROBE“ — klik → `label6_Click`
  - „Zapisnik“ — klik → `button3_Click_1` — otvara: PrijemnicaPregled
  - `cboGrupaPolja` — izbor → `cboGrupaPolja_SelectedIndexChanged` — čita: Skladista
  - `button6` — klik → `button3_Click_1` — otvara: PrijemnicaPregled
  - `button23` — klik → `button23_Click` — otvara: ZapisnikOOstecenjuRobe
  - `button22` — klik → `button1_Click_1` — otvara: PrijemnicaPregled; čita: Promet; proc: DeletePromet, DeleteRNMedjuskladisni, DeleteRNOtpremaCirade, DeleteRNOtpremaPlatforme, DeleteRNOtpremaPlatforme2, DeleteRNOtpremaVoza; preko: InsertIsporuka, InsertRN

#### PrijemnicaPregled
- **Fajl:** `RadniNalozi/PrijemnicaPregled.cs`
- **Tabele:**
  - `Delavci` — S
  - `NPre` — S
  - `NpreP` — S
  - `Partnerji` — S
  - `Pozicija` — S
  - `Promet` — S
  - `Skladista` — S
- **Funkcionalnosti (kontrola → handler):**
  - `button1` — klik → `button1_Click` — izveštaj: rptPrijemnica.rdlc

#### ZapisnikOOstecenjuRobe
- **Fajl:** `Uvoz/ZapisnikOOstecenjuRobe.cs`  
- **Naslov forme:** „Zapisnik o oštećenju robe“
- **Tabele:**
  - `ZapisnikOOStecenju` — S
- **Procedure:** `DeleteZapisnikOOstecenju`, `InsertZapisnikOOstecenju`
- **Funkcionalnosti (kontrola → handler):**
  - „Štampaj“ — klik → `button23_Click` — izveštaj: rptZapisnikOOstecenju.rdlc
  - `button20` — klik → `button20_Click` — proc: DeleteZapisnikOOstecenju, InsertZapisnikOOstecenju; preko: InsertZapisnikOOstecenju
  - `button21` — klik → `button21_Click` — proc: DeleteZapisnikOOstecenju, InsertZapisnikOOstecenju; preko: InsertZapisnikOOstecenju

### Održavanje (14)

#### frmSERDeo
- **Fajl:** `Servis/frmSERDeo.cs`
- **Tabele:**
  - `MainNovi` — S (preko SERPrava)
  - `mainNoviKartice` — S (preko SERPrava)
  - `MainNoviPrava` — S (preko SERPrava)
  - `Partnerji` — S (preko SERPolje, SERVrednosti)
  - `SERDeo` — S
  - `SERDeoGrupa` — S (preko SERPolje, SERVrednosti)
  - `SERDeoPrimenljivost` — SD
  - `SERPlanStavka` — S (preko SERPolje, SERVrednosti)
  - `SERSredstvo` — S
  - `SERVrstaSredstva` — S
  - `vSERDeoStanje` — S
  - `vSERKarticaDela` — S
  - `vSERNarudzbenica` — S
  - `vSERNarudzbenicaStavka` — S
- **Procedure:** `sp_set_session_context`

#### frmSERKorekcije
- **Fajl:** `Servis/frmSERKorekcije.cs`
- **Tabele:**
  - `MainNovi` — S (preko SERPrava)
  - `mainNoviKartice` — S (preko SERPrava)
  - `MainNoviPrava` — S (preko SERPrava)
  - `Partnerji` — S (preko SERPolje, SERVrednosti)
  - `SERDeo` — S
  - `SERDeoGrupa` — S (preko SERPolje, SERVrednosti)
  - `SERDokument` — S
  - `SERKorekcija` — S
  - `SERPlanStavka` — S (preko SERPolje, SERVrednosti)
  - `SERSredstvo` — S (preko SERPolje, SERVrednosti)
  - `SERStatus` — S
  - `SERVrstaSredstva` — S (preko SERPolje, SERVrednosti)
- **Procedure:** `sp_set_session_context`

#### frmSERMeni
- **Fajl:** `Servis/frmSERMeni.cs`
- **Otvara forme:** frmSERKorekcije (kartica „Korekcije zaliha“), frmSERNabavka (kartica „Potrebe za nabavkom“), frmSERNalozi (kartica „Radni nalozi“), frmSERObavestenja (kartica „Obaveštenja“), frmSERPopis (kartica „Popis zaliha“), frmSERPromet (kartica „Izdatnice i prijemi“), frmSERSifarnici (kartica „Sredstva i plan održavanja“), frmSERZalihe (kartica „Zalihe i šifarnik“)
- **Tabele:**
  - `SERKorekcija` — S
  - `SERObavestenje` — S
  - `vSERPredlogNabavke` — S
- **Procedure:** `sp_set_session_context`

#### frmSERNabavka
- **Fajl:** `Servis/frmSERNabavka.cs`
- **Otvara forme:** frmSERNarudzbenica (metoda `Otvori`)
- **Tabele:**
  - `MainNovi` — S (preko SERPrava)
  - `mainNoviKartice` — S (preko SERPrava)
  - `MainNoviPrava` — S (preko SERPrava)
  - `Partnerji` — S (preko SERPolje, SERVrednosti)
  - `Podesavanja` — S
  - `SERDeo` — S (preko SERPolje, SERVrednosti)
  - `SERDeoGrupa` — S (preko SERPolje, SERVrednosti)
  - `SERPlanStavka` — S (preko SERPolje, SERVrednosti)
  - `SERSredstvo` — S (preko SERPolje, SERVrednosti)
  - `SERStatus` — S
  - `SERVrstaSredstva` — S (preko SERPolje, SERVrednosti)
  - `vSERNarudzbenica` — S
  - `vSERPredlogNabavke` — S
- **Procedure:** `sp_set_session_context`

#### frmSERNalog
- **Fajl:** `Servis/frmSERNalog.cs`
- **Tabele:**
  - `MainNovi` — S (preko SERPrava)
  - `mainNoviKartice` — S (preko SERPrava)
  - `MainNoviPrava` — S (preko SERPrava)
  - `Partnerji` — S (preko SERPolje, SERVrednosti)
  - `SERDeo` — S
  - `SERDeoGrupa` — S (preko SERPolje, SERVrednosti)
  - `SERDeoKretanje` — S
  - `SERDeoPrimenljivost` — S
  - `SERDeoRezervacija` — S
  - `SERPlanStavka` — S
  - `SERRadniNalog` — S
  - `SERSredstvo` — S
  - `SERVrstaSredstva` — S (preko SERPolje, SERVrednosti)
  - `vSERDeoStanje` — S
  - `vSERRadniNalogDelovi` — S
  - `vSERRadniNalogTrosak` — S
- **Procedure:** `sp_set_session_context`

#### frmSERNalozi
- **Fajl:** `Servis/frmSERNalozi.cs`
- **Otvara forme:** frmSERNalog (pri otvaranju forme)
- **Tabele:**
  - `Partnerji` — S (preko SERPolje, SERVrednosti)
  - `SERDeo` — S (preko SERPolje, SERVrednosti)
  - `SERDeoGrupa` — S (preko SERPolje, SERVrednosti)
  - `SERPlanStavka` — S
  - `SERRadniNalog` — S
  - `SERSredstvo` — S
  - `SERVrstaSredstva` — S (preko SERPolje, SERVrednosti)
  - `vSERRadniNalogTrosak` — S
- **Procedure:** `sp_set_session_context`

#### frmSERNarudzbenica
- **Fajl:** `Servis/frmSERNarudzbenica.cs`
- **Tabele:**
  - `MainNovi` — S (preko SERPrava)
  - `mainNoviKartice` — S (preko SERPrava)
  - `MainNoviPrava` — S (preko SERPrava)
  - `Partnerji` — S (preko SERPolje, SERVrednosti)
  - `SERDeo` — S
  - `SERDeoGrupa` — S (preko SERPolje, SERVrednosti)
  - `SERNarudzbenicaStavka` — S
  - `SERPlanStavka` — S (preko SERPolje, SERVrednosti)
  - `SERSredstvo` — S (preko SERPolje, SERVrednosti)
  - `SERVrstaSredstva` — S (preko SERPolje, SERVrednosti)
  - `vSERNarudzbenica` — S
  - `vSERNarudzbenicaStavka` — S
- **Procedure:** `sp_set_session_context`

#### frmSERObavestenja
- **Fajl:** `Servis/frmSERObavestenja.cs`
- **Tabele:**
  - `SERObavestenje` — SU
- **Procedure:** `sp_set_session_context`

#### frmSERPopis
- **Fajl:** `Servis/frmSERPopis.cs`
- **Tabele:**
  - `MainNovi` — S (preko SERPrava)
  - `mainNoviKartice` — S (preko SERPrava)
  - `MainNoviPrava` — S (preko SERPrava)
  - `Partnerji` — S (preko SERPolje, SERVrednosti)
  - `SERDeo` — S
  - `SERDeoGrupa` — S (preko SERPolje, SERVrednosti)
  - `SERPlanStavka` — S (preko SERPolje, SERVrednosti)
  - `SERPopisStavka` — S
  - `SERSredstvo` — S (preko SERPolje, SERVrednosti)
  - `SERVrstaSredstva` — S (preko SERPolje, SERVrednosti)
  - `vSERPopis` — S
- **Procedure:** `sp_set_session_context`

#### frmSERPrijem
- **Fajl:** `Servis/frmSERPrijem.cs`
- **Tabele:**
  - `MainNovi` — S (preko SERPrava)
  - `mainNoviKartice` — S (preko SERPrava)
  - `MainNoviPrava` — S (preko SERPrava)
  - `Partnerji` — S (preko SERPolje)
  - `Podesavanja` — S
  - `SERDeo` — S
  - `SERDeoGrupa` — S (preko SERPolje)
  - `SERPlanStavka` — S (preko SERPolje)
  - `SERSredstvo` — S (preko SERPolje)
  - `SERVrstaSredstva` — S (preko SERPolje)
  - `vSERNarudzbenica` — S
  - `vSERNarudzbenicaStavka` — S
- **Procedure:** `sp_set_session_context`

#### frmSERPromet
- **Fajl:** `Servis/frmSERPromet.cs`
- **Otvara forme:** frmSERPrijem (metoda `NoviPrijem`)
- **Tabele:**
  - `MainNovi` — S (preko SERPrava)
  - `mainNoviKartice` — S (preko SERPrava)
  - `MainNoviPrava` — S (preko SERPrava)
  - `SERDeo` — S
  - `SERDeoKretanje` — S
  - `vSERPromet` — S
- **Procedure:** `sp_set_session_context`

#### frmSERSifarnici
- **Fajl:** `Servis/frmSERSifarnici.cs`
- **Tabele:**
  - `MainNovi` — S (preko SERPrava)
  - `mainNoviKartice` — S (preko SERPrava)
  - `MainNoviPrava` — S (preko SERPrava)
  - `Partnerji` — S (preko SERPolje, SERVrednosti)
  - `SERDeo` — S
  - `SERDeoGrupa` — S
  - `SERPlanNormativ` — SUD
  - `SERPlanStavka` — S
  - `SERSredstvo` — S
  - `SERVrstaSredstva` — S
- **Procedure:** `sp_set_session_context`

#### frmSERStavka
- **Fajl:** `Servis/frmSERStavka.cs`
- **Tabele:**
  - `Partnerji` — S (preko SERVrednosti)
  - `SERDeo` — S (preko SERVrednosti)
  - `SERDeoGrupa` — S (preko SERVrednosti)
  - `SERPlanStavka` — S (preko SERVrednosti)
  - `SERSredstvo` — S (preko SERVrednosti)
  - `SERVrstaSredstva` — S (preko SERVrednosti)

#### frmSERZalihe
- **Fajl:** `Servis/frmSERZalihe.cs`
- **Otvara forme:** frmSERDeo (pri otvaranju forme), frmSERStavka (metoda `PocetnoStanje`)
- **Tabele:**
  - `MainNovi` — S (preko SERPrava)
  - `mainNoviKartice` — S (preko SERPrava)
  - `MainNoviPrava` — S (preko SERPrava)
  - `Partnerji` — S (preko SERPolje, SERVrednosti)
  - `SERDeo` — S
  - `SERDeoGrupa` — S (preko SERPolje, SERVrednosti)
  - `SERDeoKretanje` — S
  - `SERPlanStavka` — S (preko SERPolje, SERVrednosti)
  - `SERSredstvo` — S (preko SERPolje, SERVrednosti)
  - `SERVrstaSredstva` — S (preko SERPolje, SERVrednosti)
  - `vSERDeoStanje` — S
- **Procedure:** `sp_set_session_context`

### Početni ekran (Dashboard) (1)

#### GlavniEkran
- **Fajl:** `MainLeget/GlavniEkran.cs`  
- **Naslov forme:** „GlavniEkran“
- **Tabele:**
  - `TerminalPriv` — S

### — (1)

#### NewMain
- **Fajl:** `MainLeget/NewMain.cs`  
- **Naslov forme:** „MainLeget“
- **Tabele:**
  - `MainNovi` — S
  - `mainNoviKartice` — S
  - `MainNoviPrava` — S
- **Funkcionalnosti (kontrola → handler):**
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

## 7. Metod i ograničenja

- Skup formi: isti algoritam kao `tools/analiza-formi` (graf `new Forma(...)` od `NewMain`, samo izvršni kod, Leget grane). Pokrenut nad trenutnim kodom, pa je broj formi veći nego u `docs/FORME-LEGET.md` (taj dokument je rađen nad verzijom 23ae378).
- Tabele: regex nad SQL stringovima u `.cs` fajlovima forme (uključujući `.Designer.cs`), sa spajanjem `"..." + "..."`; plus pomoćne klase koje forma instancira ili poziva statički (do 2 nivoa). Prepoznaju se `FROM`, `JOIN`, `INSERT INTO`, `UPDATE … SET`, `DELETE FROM`.
- Ne vide se: tabele iz dinamički sastavljenog SQL-a (ime tabele u promenljivoj), iz `.xsd` typed DataSet-ova, iz `.rdlc` izveštaja i iz definicija u bazi (view-ovi i procedure su navedeni samo po imenu; šta procedura interno dira nije analizirano). `uv*` imena su verovatno pogledi (views), ali kod to ne potvrđuje.
- Pomoćna klasa koju koristi više formi (npr. `InsertX`) prijavljuje svoje tabele svakoj formi koja je poziva, pa „preko“ znači posredno, ne nužno u toj formi.
- Funkcionalnosti su izvedene iz natpisa kontrola (Designer) i tela handlera; ne uzimaju se u obzir prava korisnika iz baze (`MainNovi`, `MainNoviPrava`).
