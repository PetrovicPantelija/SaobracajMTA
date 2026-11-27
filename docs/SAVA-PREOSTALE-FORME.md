# Forme iz NewMain bez redizajna Sava

Forme koje korisnik firme Leget može da otvori iz `NewMain`, a koje još nisu u stilu Sava. Lista je napravljena statičkom analizom koda i ništa u projektu ne menja.

## Ukratko

- Iz `NewMain` je za Leget dostupno **236 formi**.
- U stilu Sava je **74**, bez redizajna je **162**.
- Uključeni su i glavni prozor `NewMain` i forma za vaganje `Vaganje`. Po dogovoru, sa njima redizajn počinje.
- Modul Operacije skladišta je urađen 27.11.2026.

| Modul | Bez redizajna |
|---|---|
| Početni ekran (Dashboard) | 1 |
| Logistika izvoza | 14 |
| Drumski transport | 24 |
| Intermodalni terminal | 55 |
| Prijem i otprema vozova | 35 |
| Prijem i otprema kamiona | 13 |
| Depo | 1 |
| Operacije pretovari | 9 |
| Kapija | 4 |
| Podešavanja sistema | 2 |
| Administracija | 3 |
| Glavni prozor | 1 |
| **Ukupno** | **162** |

Forma se otvara iz više modula, a broji se jednom: u prvom modulu po redosledu levog menija. Ostali moduli su u koloni „Otvara se i iz“.

Jedanaest formi ima isto ime u dva foldera, npr. `frmManipulacije` u `Dokumenta/` i u `TrackModal/Dokumeta/`, ili `Otpremnica` u `Skladista main/Dokumenta/` i u `RadniNalozi/`. To su različite klase i svaka se redizajnira posebno, zato se obe nalaze u listi. Razlikuju se po koloni „Fajl“.

## Forme po modulima

### Glavni prozor (1)

| Forma | Fajl | Otvara se i iz |
|---|---|---|
| NewMain | `MainLeget/NewMain.cs` |  |

### Početni ekran (Dashboard) (1)

| Forma | Fajl | Otvara se i iz |
|---|---|---|
| GlavniEkran | `MainLeget/GlavniEkran.cs` |  |

### Logistika izvoza (14)

| Forma | Fajl | Otvara se i iz |
|---|---|---|
| frmCarinskiPostupak | `Izvoz/frmCarinskiPostupak.cs` |  |
| frmGrupniUnosPoljaIzvoz | `Izvoz/frmGrupniUnosPoljaIzvoz.cs` |  |
| frmIzvozPregledKontejneraDrumskeUsluge | `Izvoz/frmIzvozPregledKontejneraDrumskeUsluge.cs` | Intermodalni terminal, Prijem i otprema vozova |
| frmKontaktOsobe | `Dokumenta/frmKontaktOsobe.cs` | Drumski transport, Intermodalni terminal, Prijem i otprema vozova, Kapija, Podešavanja sistema |
| frmLogTokaProcesa | `Izvoz/frmLogTokaProcesa.cs` |  |
| frmPregledGrupaKontejneraIzvoz | `Izvoz/frmPregledGrupaKontejneraIzvoz.cs` |  |
| frmPregledPorudzbenica | `Izvoz/frmPreglegPorudzbenica.cs` |  |
| frmProdajniNalogIzvoz | `Izvoz/frmProdajniNalogIzvoz.cs` |  |
| frmProdajniNalogIzvozTabela | `Izvoz/frmProdajniNalogIzvozTabela.cs` |  |
| frmRelacija | `Izvoz/frmRelacija.cs` |  |
| LogistikaIzvoza0 | `MainLeget/Izvoz/LogistikaIzvoza0.cs` |  |
| LogistikaIzvoza1 | `MainLeget/Izvoz/LogistikaIzvoza1.cs` |  |
| LogistikaIzvoza2 | `MainLeget/Izvoz/LogistikaIzvoza2.cs` |  |
| NalogIzvozaZaOtpremu | `MainLeget/Izvoz/NalogIzvozaZaOtpremu.cs` |  |

### Drumski transport (24)

| Forma | Fajl | Otvara se i iz |
|---|---|---|
| Cerade | `MainLeget/Drumski/Cerade.cs` |  |
| Drumski1 | `MainLeget/Drumski/Drumski1.cs` |  |
| frmAutomobilEdit | `Drumski/frmAutomobilEdit.cs` |  |
| frmAutomobiliDrumski | `Drumski/frmAutomobiliDrumski.cs` |  |
| frmBrojFaktureModal | `Drumski/frmBrojFaktureModal.cs` |  |
| frmDrumski | `Drumski/frmDrumski.cs` |  |
| frmDrumski1 | `Drumski/frmDrumski1.cs` |  |
| frmFormaZaVozace | `DrumskiApp/frmFormaZaVozace.cs` |  |
| frmListaProtokola | `Drumski/frmListaProtokola.cs` |  |
| frmNalogZaFakturisanje | `Drumski/frmNalogZaFakturisanje.cs` |  |
| frmObradaUlaznihFaktura | `Drumski/frmObradaUlaznihFaktura.cs` |  |
| frmPartnerji | `Sifarnici/frmPartnerji.cs` | Podešavanja sistema |
| frmPartnerjiFirme | `Sifarnici/frmPartnerjiFirme.cs` | Podešavanja sistema |
| frmPodesavanjeRaspolozivosti | `Drumski/frmPodesavanjeRaspolozivosti.cs` |  |
| frmPregledFajlova | `Drumski/frmPregledFajlova.cs` |  |
| frmPregledNalogaDrumski | `Drumski/frmPregledNalogaDrumski.cs` |  |
| frmPregledPorukeVozacu | `Drumski/frmPregledPorukeVozacu.cs` |  |
| frmProtokolTransportnogNaloga | `Drumski/frmProtokolTransportnogNaloga.cs` |  |
| frmStatus | `Drumski/frmStatus.cs` |  |
| NalogZaDrumski | `MainLeget/Drumski/NalogZaDrumski.cs` |  |
| PakovanjeKamiona1 | `Drumski/PakovanjeKamiona1.cs` |  |
| PakovanjeKamionaCerade | `Drumski/PakovanjeKamionaCerade.cs` |  |
| Platforma | `MainLeget/Drumski/Platforma.cs` |  |
| VanGabaritni | `MainLeget/Drumski/VanGabaritni.cs` |  |

### Intermodalni terminal (55)

| Forma | Fajl | Otvara se i iz |
|---|---|---|
| frmAutoprevozniList2 | `Dokumenta/frmAutoprevozniList2.cs` | Prijem i otprema vozova, Kapija |
| frmCIR | `Dokumenta/frmCIR.cs` | Prijem i otprema vozova, Prijem i otprema kamiona, Kapija |
| frmDodatniList | `Izvestaji/frmDodatniList.cs` | Prijem i otprema vozova, Kapija |
| frmDodelaSkladista | `RadniNalozi/frmDodelaSkladista.cs` | Prijem i otprema vozova, Prijem i otprema kamiona, Operacije pretovari, Kapija |
| frmDokumentaOtpremaKontejnera | `Dokumenta/frmDokumentaOtpremaKontejnera.cs` | Prijem i otprema vozova, Kapija |
| frmDokumentaPrijemKontejneraKamion | `Dokumenta/frmDokumentaPrijemKontejneraKamion.cs` | Prijem i otprema vozova, Operacije pretovari, Kapija |
| frmDokumentaPrijemKontejneraVoz | `Dokumenta/frmDokumentaPrijemKontejneraVoz.cs` | Prijem i otprema vozova, Kapija |
| frmFormiranjePlanaIzvoz | `Izvoz/frmFormiranjePlanaIzvoz.cs` | Prijem i otprema vozova |
| frmIzvozKonacna | `Izvoz/frmIzvozKonacna.cs` | Prijem i otprema vozova |
| frmIzvozKonacnaTable | `Izvoz/frmIzvozKonacnaTable.cs` | Prijem i otprema vozova |
| frmIzvozKopiranjeUslugaKontejnera1 | `Izvoz/frmIzvozKopiranjeUslugaKontejnera1.cs` | Prijem i otprema vozova |
| frmIzvozUnosManipulacije | `Izvoz/frmIzvozUnosManipulacije.cs` | Prijem i otprema vozova |
| frmKontaktOsobeMU | `Izvoz/frmKontaktOsobeMU.cs` | Prijem i otprema vozova |
| frmKontejneriNaPrijemnici | `Dokumenta/frmKontejneriNaPrijemnici.cs` | Prijem i otprema vozova, Operacije pretovari, Kapija |
| frmLagerOperater | `Promet/frmLagerOperater.cs` | Prijem i otprema vozova, Kapija |
| frmManipulacije | `Dokumenta/frmManipulacije.cs` | Prijem i otprema vozova, Kapija |
| frmNajavaKomitemtaDokument | `Dokumenta/frmNajavaKomitemtaDokument.cs` | Prijem i otprema vozova, Kapija |
| frmNalogZaPrevoz | `Dokumenta/frmNalogZaPrevoz.cs` | Prijem i otprema vozova, Kapija |
| frmOtpremaKontejnera | `Dokumenta/frmOtpremaKontejnera.cs` | Prijem i otprema vozova, Kapija |
| frmOtpremaKontejneraIzvozKamion | `Dokumenta/frmOtpremaKontejneraIzvozKamion.cs` | Prijem i otprema vozova, Kapija |
| frmOtpremaKontejneraKamionomIzKontejnera | `Izvoz/frmOtpremaKontejneraKamionomIzKontejnera.cs` | Prijem i otprema vozova, Kapija |
| frmOtpremaKontejneraLegetIZVOZ | `Dokumenta/frmOtpremaKontejneraLegetIZVOZ.cs` | Prijem i otprema vozova |
| frmOtpremaKontejneraUvozKamion | `Dokumenta/frmOtpremaKontejneraUvozKamion.cs` | Prijem i otprema vozova, Kapija |
| frmOtpremaVozaIzPlana | `Izvoz/frmOtpremaVozaIzPlana.cs` | Prijem i otprema vozova |
| frmPovezivanjeKontejneraIVagona | `Dokumenta/frmPovezivanjeKontejneraIVagona.cs` | Prijem i otprema vozova, Kapija |
| frmPregledNarucenihManipulacija | `Dokumenta/frmPregledNarucenihManipulacija.cs` | Prijem i otprema vozova, Kapija |
| frmPregledSkladistaNovi | `RadniNalozi/frmPregledSkladistaNovi.cs` | Prijem i otprema vozova, Prijem i otprema kamiona, Operacije pretovari, Kapija |
| frmPrijemKamionaIzPlana | `Uvoz/frmPrijemKamionaIzPlana.cs` | Prijem i otprema vozova |
| frmPrijemKontejneraKamionLegetIzvoz | `Dokumenta/frmPrijemKontejneraKamionLegetIzvoz.cs` | Prijem i otprema vozova, Kapija |
| frmPrijemKontejneraKamionLegetUvoz | `Dokumenta/frmPrijemKontejneraKamionLegetUvoz.cs` | Prijem i otprema vozova, Operacije pretovari, Kapija |
| frmPrijemKontejneraLegetVozUvoz | `Dokumenta/frmPrijemKontejneraLegetVozUvoz.cs` | Prijem i otprema vozova, Kapija |
| frmPrijemVozaIzPlana | `Uvoz/frmPrijemVozaIzPlana.cs` | Prijem i otprema vozova, Kapija |
| frmPutniNalog | `Dokumenta/frmPutniNalog.cs` | Prijem i otprema vozova, Kapija |
| frmRadniNalogInterniPregled | `Uvoz/frmRadniNalogInterniPregled.cs` | Prijem i otprema vozova |
| frmRadniNalogTransport | `Dokumenta/frmRadniNalogTransport.cs` | Prijem i otprema vozova, Kapija |
| frmTerminalPrivremeni | `RNI/frmTerminalPrivremeni.cs` |  |
| frmTerminalPrivremeniArhiva | `RNI/frmTerminalPrivremeniArhiva.cs` |  |
| frmTerminalPrivremeniDokumenta | `RNI/frmTerminalPrivremeniDokumenta.cs` |  |
| frmTerminalPrivremeniExcel | `RNI/frmTerminalPrivremeniExcel.cs` |  |
| frmTerminalPrivremeniLog | `RNI/frmTerminalPrivremeniLog.cs` |  |
| frmTerminalPrivremeniOpstiIzvoz | `RNI/frmTerminalPrivremeniOpstiIzvoz.cs` |  |
| frmTerminalPrivremeniPromeni | `RNI/frmTerminalPrivremeniPromeni.cs` |  |
| frmTerminalPrivremeniSlike | `RNI/frmTerminalPrivremeniSlike.cs` |  |
| frmTovarniList | `Testiranje/frmTovarniList.cs` | Prijem i otprema vozova, Kapija |
| Intermodalni1 | `MainLeget/Intermodalni/Intermodalni1.cs` |  |
| IntermodalniKomN | `MainLeget/Intermodalni/IntermodalniKomN.cs` |  |
| RN12MedjuskladisniKontejnera | `RadniNalozi/RN12MedjuskladisniKontejnera.cs` | Prijem i otprema vozova, Operacije pretovari |
| RN1PrijemVoza | `RadniNalozi/RN1PrijemVoza.cs` | Prijem i otprema vozova, Kapija |
| RN2OtpremaVoza | `RadniNalozi/RN2OtpremaVoza.cs` | Prijem i otprema vozova |
| RN4PrijemPlatforme | `RadniNalozi/RN4PrijemPlatforme.cs` | Prijem i otprema vozova, Kapija |
| RN5PrijemPlatforme2 | `RadniNalozi/RN5PrijemPlatforme2.cs` | Prijem i otprema vozova, Kapija |
| RN6OtpremaPlatforme | `RadniNalozi/RN6OtpremaPlatforme.cs` | Prijem i otprema vozova, Kapija |
| RN7OtpremaPlatforme2 | `RadniNalozi/RN7OtpremaPlatforme2.cs` | Prijem i otprema vozova, Kapija |
| VozniPlanovi | `MainLeget/Intermodalni/VozniPlanovi.cs` |  |
| ZapisnikONepravilnosti | `Uvoz/ZapisnikONepravilnosti.cs` | Prijem i otprema vozova, Prijem i otprema kamiona |

### Prijem i otprema vozova (35)

| Forma | Fajl | Otvara se i iz |
|---|---|---|
| DogovoreniPosloviIzvoza | `Izvoz/DogovoreniPosloviIzvoza.cs` |  |
| frmAnalizaRadnihNaloga | `RadniNalozi/frmAnalizaRadnihNaloga.cs` |  |
| frmAutoprevozniList2 | `TrackModal/Dokumeta/frmAutoprevozniList2.cs` |  |
| frmCIRPregled | `TrackModal/Dokumeta/frmCIRPregled.cs` |  |
| frmDodatniList | `TrackModal/Izvestaji/frmDodatniList.cs` |  |
| frmDokumentaOtpremaKontejnera | `TrackModal/Dokumeta/frmDokumentaOtpremaKontejnera.cs` |  |
| frmDopunaPlanaPraznimIzvoz | `Izvoz/frmDopunaPlanaPraznimIzvoz.cs` |  |
| frmFormiranjePlana | `Uvoz/frmFormiranjePlana.cs` |  |
| frmKontejnerTekuceArhiv | `Uvoz/frmKontejnerTekuceArhiv.cs` |  |
| frmManipulacije | `TrackModal/Dokumeta/frmManipulacije.cs` |  |
| frmNajavaKomitemtaDokument | `TrackModal/Dokumeta/frmNajavaKomitemtaDokument.cs` |  |
| frmNalogZaPrevoz | `TrackModal/Dokumeta/frmNalogZaPrevoz.cs` |  |
| frmOtpremaKontejnera | `TrackModal/Dokumeta/frmOtpremaKontejnera.cs` |  |
| frmPopisKontejneraStampa | `Dokumenta/frmPopisKontejneraStampa.cs` |  |
| frmPopisKonterjnera | `Dokumenta/frmPopisKonterjnera.cs` |  |
| frmPregledKontejneraDrumskeUsluge | `Uvoz/frmPregledKontejneraDrumskeUsluge.cs` |  |
| frmPregledOtpreme | `TrackModal/Dokumeta/frmPregledOtpreme.cs` |  |
| frmPregledVozova | `TrackModal/Dokumeta/frmPregledVozova.cs` |  |
| frmPrijemIOtpremaVozova2 | `MainLeget/Prijemiotpremavozova/frmPrijemIOtpremaVozova2.cs` |  |
| frmPrijemIOtpremaVozova3 | `MainLeget/Prijemiotpremavozova/frmPrijemIOtpremaVozova3.cs` |  |
| frmPrijemIOtpremaVozova4 | `MainLeget/Prijemiotpremavozova/frmPrijemIOtpremaVozova4.cs` |  |
| frmPrijemVozomPregled | `TrackModal/Dokumeta/frmPrijemVozomPregled.cs` |  |
| frmPutniNalog | `TrackModal/Dokumeta/frmPutniNalog.cs` |  |
| frmRadniNalogTransport | `TrackModal/Dokumeta/frmRadniNalogTransport.cs` |  |
| frmUnosManipulacija | `Uvoz/frmUnosManipulacija.cs` |  |
| frmUvozKonacna | `Uvoz/UvozKonacna.cs` |  |
| frmUvozKonacnaTable | `Uvoz/frmUvozKonacnaTable.cs` |  |
| frmUvozKopirajUslugeKontejnera | `Uvoz/frmUvozKopirajUslugeKontejnera.cs` |  |
| frmUvozUvoznici | `Uvoz/frmUvozUvoznici.cs` |  |
| frmVoz | `TrackModal/Dokumeta/frmVoz.cs` |  |
| frmVoziloUsluga | `Uvoz/frmVoziloUsluga.cs` |  |
| frmVozOtprema | `MainLeget/Prijemiotpremavozova/frmVozOtprema.cs` |  |
| frmVozPrijem | `MainLeget/Prijemiotpremavozova/frmVozPrijem.cs` |  |
| PrijemIOtpremaVozova1 | `MainLeget/Prijemiotpremavozova/PrijemIOtpremaVozova1.cs` |  |
| UvozNHM | `Uvoz/UvozNHM.cs` |  |

### Prijem i otprema kamiona (13)

| Forma | Fajl | Otvara se i iz |
|---|---|---|
| frmCerada | `MainLeget/PrijemIOtpremaKamiona/frmCerada.cs` |  |
| frmCeradaOtprema | `MainLeget/PrijemIOtpremaKamiona/frmCeradaOtprema.cs` |  |
| frmFotografijePregledac | `MainLeget/PrijemIOtpremaKamiona/frmFotografijePregledac.cs` |  |
| frmPlatforma | `MainLeget/PrijemIOtpremaKamiona/frmPlatforma.cs` |  |
| frmPlatformaOtprema | `MainLeget/PrijemIOtpremaKamiona/frmPlatformaOtprema.cs` |  |
| frmPrijemIOtpremaCerada | `MainLeget/PrijemIOtpremaKamiona/frmPrijemIOtpremaCerada.cs` |  |
| frmPrijemIOtpremaKamiona1 | `MainLeget/PrijemIOtpremaKamiona/frmPrijemIOtpremaKamiona1.cs` |  |
| frmPrijemIOtpremaPlatforma | `MainLeget/PrijemIOtpremaKamiona/frmPrijemIOtpremaPlatforma.cs` |  |
| frmPrijemKamionaDetalji | `MainLeget/PrijemIOtpremaKamiona/frmPrijemKamionaDetalji.cs` |  |
| IzvestajVaganja | `Izvoz/IzvestajVaganja.cs` |  |
| Pregledac | `MainLeget/PrijemIOtpremaKamiona/Pregledac.cs` |  |
| Vaganje | `Izvoz/Vaganje.cs` |  |
| VaganjePregled | `Izvoz/VaganjePregled.cs` |  |

### Depo (1)

| Forma | Fajl | Otvara se i iz |
|---|---|---|
| DepoMain | `MainLeget/Depocnt/DepoMain.cs` |  |

### Operacije pretovari (9)

| Forma | Fajl | Otvara se i iz |
|---|---|---|
| frmPretovariSkladista | `MainLeget/Pretovari/frmPretovariSkladista.cs` |  |
| frmPretovarPrijem | `MainLeget/Pretovari/frmPretovarPrijem.cs` |  |
| Otpremnica | `RadniNalozi/Otpremnica.cs` |  |
| OtpremnicaPregled | `RadniNalozi/OtpremnicaPregled.cs` |  |
| PretovariMain | `MainLeget/Pretovari/PretovariMain.cs` |  |
| PretovariPrijem | `MainLeget/Pretovari/PretovariPrijem.cs` |  |
| Prijemnica | `RadniNalozi/Prijemnica.cs` |  |
| PrijemnicaPregled | `RadniNalozi/PrijemnicaPregled.cs` |  |
| ZapisnikOOstecenjuRobe | `Uvoz/ZapisnikOOstecenjuRobe.cs` |  |

### Kapija (4)

| Forma | Fajl | Otvara se i iz |
|---|---|---|
| frmKamionDetalji | `Kapija/frmKamionDetalji.cs` |  |
| frmKapija1 | `MainLeget/Kapija/frmKapija1.cs` |  |
| frmKapijaOtvoreniNalozi | `Kapija/frmKapijaOtvoreniNalozi.cs` |  |
| frmPregledKamiona | `Kapija/frmPregledKamiona.cs` |  |

### Podešavanja sistema (2)

| Forma | Fajl | Otvara se i iz |
|---|---|---|
| frmTipKontejnera | `TrackModal/Sifarnici/frmTipKontejnera.cs` |  |
| Podesavanje1 | `MainLeget/Podesavanjesistema/Podesavanje1.cs` |  |

### Administracija (3)

| Forma | Fajl | Otvara se i iz |
|---|---|---|
| Administracija1 | `Administracija/Administracija1.cs` |  |
| AdministracijaPravoPristupa | `MainLeget/AdministracijaPravoPristupa.cs` | Dugme „!!!“ (samo korisnik test) |
| frmKorisnici | `Administracija/frmKorisnici.cs` |  |

## Forme koje su već u stilu Sava (74)

| Forma | Fajl | Modul | Napomena |
|---|---|---|---|
| CarinskoSkladiste | `Skladista main/CarinskoSkladiste.cs` | Operacije skladišta |  |
| CarinskoSkladistePregled | `Skladista main/CarinskoSkladistePregled.cs` | Operacije skladišta |  |
| frmCarinskaKnjiga | `Skladista main/frmCarinskaKnjiga.cs` | Operacije skladišta |  |
| frmDefinisiPoziciju | `Tehnologija/frmDefinisiPoziciju.cs` | Operacije skladišta, Prijem i otprema vozova |  |
| frmDodatneUsluge | `Uvoz/frmDodatneUsluge.cs` | Operacije skladišta, Prijem i otprema vozova, Prijem i otprema kamiona |  |
| frmHRDan | `HR/frmHRDan.cs` | HR modul |  |
| frmHREvidencijaRada | `HR/frmHREvidencijaRada.cs` | HR modul |  |
| frmHRGOPravo | `HR/frmHRGOPravo.cs` | HR modul |  |
| frmHRGOZahtevi | `HR/frmHRGOZahtevi.cs` | HR modul |  |
| frmHRGrupniUnos | `HR/frmHRGrupniUnos.cs` | HR modul |  |
| frmHRKontrolnaTabla | `HR/frmHRKontrolnaTabla.cs` | HR modul |  |
| frmHRLog | `HR/frmHRLog.cs` | HR modul |  |
| frmHRMeni | `HR/frmHRMeni.cs` | HR modul |  |
| frmHRRevers | `HR/frmHRRevers.cs` | HR modul |  |
| frmHRSifarnici | `HR/frmHRSifarnici.cs` | HR modul |  |
| frmHRStavka | `HR/frmHRStavka.cs` | Administracija, HR modul |  |
| frmHRZahtev | `HR/frmHRZahtev.cs` | HR modul |  |
| frmHRZahteviLista | `HR/frmHRZahteviLista.cs` | HR modul |  |
| frmHRZaposleni | `HR/frmHRZaposleni.cs` | HR modul |  |
| frmHRZaposleniLista | `HR/frmHRZaposleniLista.cs` | HR modul |  |
| frmIzvestajCarinskoObelezje | `Skladista/frmIzvestajCarinskoObelezje.cs` | Operacije skladišta | samo ReportViewer sa svojom trakom; nema kontrola za stil |
| frmIzvozDokumenta | `Izvoz/frmIzvozDokumenta.cs` | Operacije skladišta, Intermodalni terminal, Prijem i otprema vozova, Prijem i otprema kamiona |  |
| frmIzvozUslugeNaloga | `Izvoz/frmIzvozUslugeNaloga.cs` | Logistika izvoza |  |
| frmKontejnerTekuce | `Uvoz/frmKontejnerTekuce.cs` | Operacije skladišta, Intermodalni terminal, Prijem i otprema vozova, Operacije pretovari, Kapija |  |
| frmMestoIsporuke | `Skladista main/frmMestoIsporuke.cs` | Operacije skladišta |  |
| frmNalogPrijem | `Skladista main/frmNalogPrijem.cs` | Operacije skladišta |  |
| frmPodesavanja | `Administracija/frmPodesavanja.cs` | Administracija |  |
| frmPozicija | `TrackModal/Sifarnici/frmPozicija.cs` | Podešavanja sistema |  |
| frmRazduzenje | `Skladista main/Dokumenta/frmRazduzenje.cs` | Operacije skladišta |  |
| frmRN12InterniPrenos | `TerminalMap/frmRN12InterniPrenos.cs` | Operacije skladišta, Prijem i otprema vozova |  |
| frmScenarioSCI | `RNI/frmScenarioSCI.cs` | Operacije skladišta, Intermodalni terminal, Prijem i otprema vozova |  |
| frmSeniranjeDokumenata | `Drumski/frmSeniranjeDokumenata.cs` | Operacije skladišta |  |
| frmSERDeo | `Servis/frmSERDeo.cs` | Održavanje |  |
| frmSERKorekcije | `Servis/frmSERKorekcije.cs` | Održavanje |  |
| frmSERMeni | `Servis/frmSERMeni.cs` | Održavanje |  |
| frmSERNabavka | `Servis/frmSERNabavka.cs` | Održavanje |  |
| frmSERNalog | `Servis/frmSERNalog.cs` | Održavanje |  |
| frmSERNalozi | `Servis/frmSERNalozi.cs` | Održavanje |  |
| frmSERNarudzbenica | `Servis/frmSERNarudzbenica.cs` | Održavanje |  |
| frmSERObavestenja | `Servis/frmSERObavestenja.cs` | Održavanje |  |
| frmSERPopis | `Servis/frmSERPopis.cs` | Održavanje |  |
| frmSERPrijem | `Servis/frmSERPrijem.cs` | Održavanje |  |
| frmSERPromet | `Servis/frmSERPromet.cs` | Održavanje |  |
| frmSERSifarnici | `Servis/frmSERSifarnici.cs` | Održavanje |  |
| frmSERStavka | `Servis/frmSERStavka.cs` | Održavanje |  |
| frmSERZalihe | `Servis/frmSERZalihe.cs` | Održavanje |  |
| frmSkladista | `TrackModal/Sifarnici/frmSkladista.cs` | Podešavanja sistema |  |
| frmSkladisteObjekat | `TrackModal/Sifarnici/frmSkladisteObjekat.cs` | Podešavanja sistema |  |
| frmTipPalete | `TrackModal/Sifarnici/frmTipPalete.cs` | Operacije skladišta |  |
| frmVlasnikRobeNovi | `Skladista main/frmVlasnikRobeNovi.cs` | Operacije skladišta |  |
| frmVozac | `Skladista main/frmVozac.cs` | Operacije skladišta |  |
| frmZone | `TrackModal/Sifarnici/frmZone.cs` | Podešavanja sistema |  |
| InterniPoslovi | `Skladista main/Dokumenta/InterniPoslovi.cs` | Operacije skladišta |  |
| InterniPrenos | `Skladista main/Dokumenta/InterniPrenos.cs` | Operacije skladišta |  |
| KomercijalnoSkladiste | `Skladista main/KomercijalnoSkladiste.cs` | Operacije skladišta |  |
| KomercijalnoSkladistePregled | `Skladista main/KomercijalnoSkladistePregled.cs` | Operacije skladišta |  |
| MagacinskaKnjigaCarinska | `Skladista main/MagacinskaKnjigaCarinska.cs` | Operacije skladišta |  |
| NalogZaKomercijalni | `Skladista main/NalogZaKomercijalni.cs` | Operacije skladišta |  |
| Otprema | `Skladista main/Dokumenta/Otprema.cs` | Operacije skladišta |  |
| OtpremaLager | `Skladista main/OtpremaLager.cs` | Operacije skladišta |  |
| Otpremnica | `Skladista main/Dokumenta/Otpremnica.cs` | Operacije skladišta |  |
| PaletePrebacivanje | `Skladista main/PaletePrebacivanje.cs` | Operacije skladišta | prazna forma bez kontrola |
| PregledKomercijalnihNaloga | `Skladista main/PregledKomercijalnihNaloga.cs` | Operacije skladišta |  |
| PregledLagera | `Skladista main/Dokumenta/PregledLagera.cs` | Operacije skladišta |  |
| Pretovar | `Skladista main/Dokumenta/Pretovar.cs` | Operacije skladišta |  |
| Prijemnica | `Skladista main/Dokumenta/Prijemnica.cs` | Operacije skladišta |  |
| RadniNaloziSkladista | `Skladista main/RadniNaloziSkladista.cs` | Operacije skladišta |  |
| SkladistaMain | `Skladista main/SkladistaMain.cs` | Operacije skladišta |  |
| SkladisteGrupa | `TrackModal/Sifarnici/SkladisteGrupa.cs` | Podešavanja sistema |  |
| TerminalMapFRM | `TerminalMap/TerminalMapFRM.cs` | Operacije skladišta, Prijem i otprema vozova |  |
| TipSkladista | `Skladista main/TipSkladista.cs` | Operacije skladišta |  |
| UvozDokumenta | `Uvoz/UvozDokumenta.cs` | Operacije skladišta, Prijem i otprema vozova, Prijem i otprema kamiona |  |
| VanredniPoslovi | `Skladista main/Dokumenta/VanredniPoslovi.cs` | Operacije skladišta |  |
| ViljuskaristiPregled | `Skladista main/ViljuskaristiPregled.cs` | Operacije skladišta |  |

## Kako je napravljena lista

- Dostupne forme daje skripta `tools/analiza-formi` (isti metod kao `docs/FORME-LEGET.md`): polazi od `NewMain`, prati samo kod koji se izvršava i grane za Leget, a preskače kontrole sakrivene za Leget.
- Forma se računa kao urađena ako neki njen fajl (uključujući partial fajlove i baznu klasu) poziva `ThemeHelper`, `SavaTheme`, `SavaForme` ili pomoćne klase stila izvedene iz njih: `SifarnikTema` i `SifarnikGrid` (šifarnici skladišta), `HRStil` i `HRGrid` (HR), `SERStil` i `SERGrid` (Održavanje). Komentarisan kod se ne računa.
- Redizajn forme je u posebnom fajlu `Forma.Sava.cs` (partial, metoda `PrimeniSava`), koji se poziva na kraju konstruktora. Zajedničke metode za postojeće forme (pločice, traka u jednom redu, kartice, dugmad po tekstu) su u `Theme/SavaForme.cs`.
- Alat `tools/sava-snimak` pravi sliku forme pre i posle `PrimeniSava` bez baze i prijavljuje preklapanja i odsečen tekst.
- Lista ne proverava koliko je forma potpuno sređena, samo da li koristi stil Sava.
- Prava korisnika iz baze nisu uzeta u obzir.
- Analiza je rađena nad verzijom koda 2d91040 od 26.11.2026. (grana `master`) i nekomitovanim izmenama u radnom folderu.
