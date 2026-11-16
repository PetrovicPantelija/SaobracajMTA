# Saobraćaj (MTA)

WinForms aplikacija za logistiku, transport i kontejnerski terminal. Isti kod koriste više firmi: Leget, Transagent (TA), DPT, VSD i KP.

## Osnovna pravila

- **Postojeći kod i projekat se ne menjaju bez izričitog zahteva.** To važi za `.cs` fajlove, `Saobracaj.csproj`, `App.config`, `Config.cs`, `.rdlc` izveštaje i `setup.iss`. Ako zadatak zahteva takvu izmenu, prvo pitaj.
- Namespace ostaje **`Saobracaj`**. Podfolderi dodaju nastavak, npr. `Saobracaj.Kapija`, `Saobracaj.Dokumenta`. Fajlovi se ne premeštaju i namespace se ne preimenuje.
- Nov fajl koristi namespace po folderu u kom se nalazi, isto kao postojeći kod.

## Struktura

```
SaobracajMTA/
├─ Saobracaj/          kod aplikacije (jedan projekat, .NET Framework 4.8)
│  ├─ Kapija/, Uvoz/, Izvoz/, Drumski/, Carinsko/, Dokumenta/, Finansije/, Sifarnici/, ...
│  ├─ Config.cs        podešavanja po firmi (baza, mrežni folder Dokumenta, podaci firme)
│  └─ Program.cs       ulazna tačka; bira firmu i formu za prijavu
├─ Klijent/            fajlovi razmenjeni sa klijentima (ne čuvaju se u git-u)
│  ├─ Ulaz/            Specifikacije, SlikeEkrana, Primeri
│  └─ Izlaz/           Isporuke, Dokumentacija
├─ docs/               dokumentacija za razvoj
│  ├─ SAVA-DESIGN.md   vizuelni stil formi
│  └─ FORME-LEGET.md   forme dostupne za Leget iz NewMain, sa putem do svake
├─ Output/             instalacije koje pravi setup.iss (Inno Setup)
├─ tools/              nuget.exe, build-installer.ps1, analiza-formi/ (pravi FORME-LEGET.md)
└─ packages/           NuGet paketi (packages.config)
```

Kad zadatak pominje zahtev, sliku ekrana ili primer dokumenta od klijenta, prvo pogledaj `Klijent/Ulaz/`.
Kad zadatak pominje formu ili ekran za Leget, u `docs/FORME-LEGET.md` proveri da li je forma uopšte dostupna i kojim putem se otvara.

## Kako aplikacija bira firmu

`Program.cs` u folderu sa `.exe` fajlom traži `.txt` fajl. Ime tog fajla određuje firmu (npr. `Leget.txt`).
`ConfigManager.GetCompanyConfiguration(naziv)` u `Config.cs` vraća konekciju na bazu, mrežni folder `Dokumenta` i podatke firme.
Svaka izmena mora da radi za sve firme, ne samo za Leget.

## Build

Projekat koristi stari format `.csproj`: **svaki nov fajl mora ručno da se doda u `Saobracaj.csproj`** (`<Compile Include="..."/>`).

```
tools\nuget.exe restore Saobracaj.sln
"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" Saobracaj\Saobracaj.csproj /p:Configuration=Debug /p:Platform=AnyCPU /m /v:minimal
```

Gradi se direktno `.csproj`, jer `.sln` mapira `Debug|Any CPU` na Release. Isto rade VS Code taskovi `restore` i `build` u `.vscode/tasks.json`.

Instalacija: Release build, pa `ISCC.exe setup.iss`. Rezultat ide u `Output/`. Ono što je stvarno poslato klijentu kopira se u `Klijent/Izlaz/Isporuke/`.

## Na šta treba paziti

- **Izveštaji (.rdlc)** se učitavaju na dva načina: preko `ReportEmbeddedResource` po punom imenu resursa (npr. `"Saobracaj.Izvestaji.Najava.rdlc"`) i preko `ReportPath` po imenu fajla (npr. `"rptOtpremnicaCarinska.rdlc"`). Premeštanje ili preimenovanje `.rdlc` fajla, foldera ili namespace-a kvari izveštaj. Build ne prijavljuje grešku; ona se vidi tek kad korisnik otvori izveštaj.
- **Kontrole treće strane**: Syncfusion 19.4 (licenca se registruje u `Program.cs`), Bunifu, MetroFramework, ReportViewer, GMap.NET. Pri izmeni izgleda koristi njihov sistem stilova i ne zamenjuj ih standardnim kontrolama.
- `.Designer.cs` fajlove generiše Visual Studio dizajner. Ručne izmene u njima radi samo kad je neophodno.

## Vizuelni stil: Sava

Od četiri predložena pravca (Sava, Terminal, Jadran, Kontejner) izabran je **Sava**: današnji izgled aplikacije, sređen. Primenjuje se tek kad se redizajn izričito zatraži. Detalji za svaku kontrolu, sa primerima koda, su u `docs/SAVA-DESIGN.md` i treba ih pročitati pre svake izmene izgleda.

Ukratko:
- jedan font: **Segoe UI**
- jedna boja za akcije: **Leget plava**, i to samo za glavno dugme na formi
- **cijan iz logotipa** samo za polje u fokusu i izabrani red u tabeli

### Boje

| Naziv | Hex | Gde se koristi |
|---|---|---|
| Leget plava | `#0A66B8` | Zaglavlje aplikacije, aktivna stavka menija, glavno dugme |
| Noćna Sava | `#12304D` | Bočni meni, donja navigaciona traka |
| Cijan krst | `#3DB5E6` | Fokus polja, oznaka izabranog reda |
| Radna površina | `#F4F7FA` | Pozadina formi i polja samo za čitanje |
| Linija | `#D4DDE6` | Okviri panela, razdelnici |
| Tekst | `#172433` | Uneti tekst i tekst u tabelama |
| Oznaka | `#4C5D6E` | Oznake iznad polja, sporedni tekst |
| Okvir polja | `#B9C6D3` | Ivice polja i sporednih dugmadi |
| Zaglavlje tabele | `#EAF0F6` | Pozadina zaglavlja kolona |
| Izabrani red | `#DDF1FB` | Pozadina selektovanog reda |
| Opasnost | `#B3261E` | Tekst dugmadi Obriši i Storniraj |
| Meni tekst | `#C7D3E0` | Neaktivne stavke bočnog menija |

### Fontovi

| Element | Font |
|---|---|
| Naslov u zaglavlju aplikacije | Segoe UI Semibold 11 pt, bela boja |
| Naslov unutar forme | Segoe UI Semibold 14 pt |
| Polja, ComboBox, tabela, dugmad, meni | Segoe UI 10 pt |
| Oznake iznad polja | Segoe UI 9 pt, boja `#4C5D6E` |
| Zaglavlje kolona | Segoe UI Semibold 9 pt |

Polu-masni font je posebna porodica: `new Font("Segoe UI Semibold", 9f)`. `FontStyle.Bold` daje pun bold, a to nije isto.

### Pravila

- Na formi je najviše **jedno** plavo dugme: glavna akcija, najčešće „Sačuvaj“. Ostala dugmad su bela sa sivim okvirom. Crvena boja se koristi samo za brisanje i storniranje.
- Na toolbar-u nema šarenih okruglih ikonica. Dugmad imaju tekst, a ikonica je opciona i jednobojna.
- Oznaka stoji iznad polja i piše se normalnim slovima, bez dvotačke: „Broj kontejnera“.
- Polje samo za čitanje ima pozadinu `#F4F7FA`, ali tekst ostaje taman i čitljiv, nikad izbledeo.
- Brojevi (težine, iznosi) su poravnati desno. Jedinica ide u zaglavlje kolone („Bruto kg“), ne uz svaku vrednost.
- Zaglavlja kolona su reči, a ne nazivi iz baze: „Datum merenja“, ne „DatumMerenja“.
- Dugme kaže tačno šta radi: „Sačuvaj“, „Potvrdi vaganje“, „Osveži“. Ista akcija ima isti naziv u celoj aplikaciji.
- Mere: margina forme 20 px, toolbar visine 52 px, dugmad 32 px, redovi tabele 30–32 px.

### Primena (kad se zatraži)

- Boje i fontovi idu u `Saobracaj/Theme/SavaTheme.cs`, a primena u `Saobracaj/Theme/ThemeHelper.cs` (namespace `Saobracaj.Theme`). Oba fajla se dodaju u `Saobracaj.csproj`.
- Forma poziva `ThemeHelper.Apply(this)` posle `InitializeComponent()`. Glavno dugme dobija `Tag = "primary"`, a dugmad za brisanje `Tag = "danger"`.
- Projekat koristi **Syncfusion** kontrole (grid, `RibbonForm`), Bunifu i MetroFramework. Njih stilizuj kroz njihova svojstva za izgled; ne zamenjuj ih standardnim kontrolama.
- Ne menjaj `Font` same forme u toku rada. Ako forma ima `AutoScaleMode.Font`, kontrole se pomeraju i menjaju veličinu.
- Pre promene teksta dugmeta ili oznake proveri da kod ne poredi taj tekst (npr. `if (btn.Text == "...")`).
- Redosled: prvo forma za vaganje i glavni prozor, zatim pregled sa korisnikom, pa ostale forme jedna po jedna. Posle svake forme proveri da se polja ne preklapaju.

## Dogovoreno, još nije u kodu

- Ulazni i izlazni fajlovi aplikacije u radu (uvoz, izveštaji, izvoz): putanja će se čitati iz `App.config`. Ključ i podrazumevana vrednost se dogovaraju kad se krene sa implementacijom.
