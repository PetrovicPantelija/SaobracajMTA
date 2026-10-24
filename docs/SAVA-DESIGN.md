# Sava – vizuelni stil za Leget WinForms forme

Ovaj dokument opisuje vizuelni stil **Sava** za sve forme u aplikaciji. Raspored kontrola i poslovna logika ostaju isti. Menjaju se samo boje, fontovi, izgled dugmadi i tabela, kao i tekstovi oznaka.

Osnovna ideja: zaglavlje je u Leget plavoj, bočni meni je tamnoplav, a radna površina svetla i mirna. U celoj aplikaciji važe sledeća pravila:
- **Jedan font**: Segoe UI.
- **Jedna boja za akcije**: Leget plava, i to samo za glavno dugme na formi.
- **Cijan iz logotipa** koristi se samo za fokus i izabrani red.

---

## 1. Boje

U kodu koristi `ColorTranslator.FromHtml("#...")`. Ne prepisuj RGB vrednosti ručno.

### Osnovna paleta

| Naziv | Hex | Gde se koristi |
|---|---|---|
| Leget plava | `#0A66B8` | Zaglavlje aplikacije, aktivna stavka menija, glavno dugme na formi |
| Noćna Sava | `#12304D` | Bočni meni, donja navigaciona traka |
| Cijan krst | `#3DB5E6` | Okvir polja u fokusu, oznaka izabranog reda |
| Radna površina | `#F4F7FA` | Pozadina formi, pozadina polja samo za čitanje |
| Linija | `#D4DDE6` | Okviri panela, razdelnici, donja ivica toolbar-a |
| Tekst | `#172433` | Sav uneti tekst i tekst u tabelama |

### Izvedene boje

| Naziv | Hex | Gde se koristi |
|---|---|---|
| Oznaka | `#4C5D6E` | Tekst oznaka (labela) iznad polja, sporedni tekst |
| Okvir polja | `#B9C6D3` | Ivica TextBox/ComboBox/NumericUpDown, ivica sporednih dugmadi |
| Zaglavlje tabele | `#EAF0F6` | Pozadina zaglavlja kolona |
| Linija tabele | `#E6ECF1` | Horizontalne linije između redova |
| Izabrani red | `#DDF1FB` | Pozadina selektovanog reda u tabeli |
| Opasnost | `#B3261E` | Tekst dugmadi Obriši/Storniraj |
| Meni tekst | `#C7D3E0` | Tekst neaktivnih stavki bočnog menija |
| Bela | `#FFFFFF` | Paneli sa poljima, polja za unos, toolbar, tabela |
| Plava hover | `#0958A0` | Glavno dugme kad je miš iznad njega |
| Svetli hover | `#EEF3F8` | Sporedna dugmad kad je miš iznad njih |
| Opasnost hover | `#FBEAEA` | Dugme Obriši kad je miš iznad njega |

### Pravila za boje

- Na formi je **samo jedno** dugme ispunjeno plavom bojom: glavna akcija (najčešće „Sačuvaj“). Sva ostala dugmad su bela sa sivim okvirom.
- Nema zelenih, plavih i crvenih okruglih ikonica na toolbar-u. Dugmad imaju tekst, a ikonica je opciona i jednobojna.
- Oznake i polja nikad nisu izbledelo sivi. Polje samo za čitanje ima pozadinu `#F4F7FA`, ali tekst ostaje `#172433`, da bude čitljiv.
- Crvena se koristi samo za brisanje i storniranje.

---

## 2. Fontovi

Font je **Segoe UI**. Ugrađen je u svaki Windows, pa ništa ne treba instalirati.

| Element | Font | Veličina |
|---|---|---|
| Naslov u zaglavlju aplikacije | `Segoe UI Semibold` | 11 pt, bela boja |
| Naslov unutar forme (ako postoji) | `Segoe UI Semibold` | 14 pt |
| Polja za unos, ComboBox, tabela | `Segoe UI` | 10 pt |
| Oznake iznad polja | `Segoe UI` | 9 pt, boja `#4C5D6E` |
| Zaglavlje kolona u tabeli | `Segoe UI Semibold` | 9 pt |
| Dugmad | `Segoe UI` | 10 pt (glavno dugme `Segoe UI Semibold`) |
| Bočni meni | `Segoe UI` | 10 pt (aktivna stavka `Segoe UI Semibold`) |
| Izračunata vrednost (npr. Neto) | `Segoe UI` Bold | 10 pt |

Polu-masni font u WinForms-u se dobija kao posebna porodica: `new Font("Segoe UI Semibold", 9f)`. `FontStyle.Bold` daje pun bold, a to nije isto.

### Važno: fontovi i skaliranje

- **Ne menjaj `Font` same forme u runtime-u.** Ako forma ima `AutoScaleMode.Font`, promena fonta forme pokreće automatsko skaliranje i kontrole se pomeraju i menjaju veličinu. Fontove postavljaj na pojedinačne kontrole.
- TextBox postaje viši kad mu se poveća font. Na zbijenim formama, gde su polja blizu jedno drugom, posle promene proveri da se ništa ne preklapa. Ako nema mesta, za tu formu ostavi polja na 9 pt.

---

## 3. Mere i raspored

- Spoljna margina sadržaja forme: **20 px**
- Razmak između kolona polja: **24 px**. Razmak između redova polja: **14 px**
- Oznaka stoji **iznad** polja, sa razmakom od 4 px, i poravnata je levo
- Toolbar: visina **52 px**, bela pozadina, donja linija `#D4DDE6`, dugmad visine **32 px**
- Tabela: zaglavlje **32 px**, redovi **30–32 px**
- Brojevi (težine, iznosi) su **poravnati desno**. Tekst i šifre su poravnati levo
- Jedinice (kg) se pišu u zaglavlju kolone ili pored polja, ne uz svaku vrednost

---

## 4. Kontrole – kako ih podesiti

### Forma
```csharp
form.BackColor = SavaTheme.Surface;   // #F4F7FA
```

### Panel sa poljima (umesto „golih“ kontrola na pozadini)
Bela pozadina i okvir `#D4DDE6`. Panel nema boju okvira, pa se okvir crta u Paint događaju:
```csharp
panel.BackColor = Color.White;
panel.Paint += (s, e) => ControlPaint.DrawBorder(e.Graphics, panel.ClientRectangle,
    SavaTheme.Line, ButtonBorderStyle.Solid);
```
GroupBox ne zamenjuj ako to zahteva veće izmene rasporeda. U tom slučaju mu samo postavi `ForeColor = SavaTheme.Label`.

### Oznake (Label)
```csharp
label.ForeColor = SavaTheme.Label;    // #4C5D6E
label.Font = SavaTheme.LabelFont;     // Segoe UI 9
```
Tekst oznake pišeš normalnim slovima, ne velikim: „Broj kontejnera“, a ne „BROJ KONTEJNERA“ ni „Broj kontejnera:“ sa dvotačkom.

### TextBox, NumericUpDown, ComboBox
```csharp
textBox.BorderStyle = BorderStyle.FixedSingle;
textBox.Font = SavaTheme.FieldFont;   // Segoe UI 10
textBox.ForeColor = SavaTheme.Text;
textBox.BackColor = textBox.ReadOnly ? SavaTheme.Surface : Color.White;

numeric.TextAlign = HorizontalAlignment.Right;
```
Standardni TextBox ne dozvoljava promenu boje okvira. Obojeni okvir (`#B9C6D3`) i cijan okvir u fokusu dobijaju se samo posebnom kontrolom: Panel sa okvirom koji sadrži TextBox sa `BorderStyle.None`. To je **opcioni drugi korak**. Ne uvodi ga u prvom prolazu.

### Dugmad
Uloga dugmeta se određuje preko `Tag` svojstva: `"primary"`, `"danger"`. Sva ostala dugmad su sporedna.
```csharp
// zajedničko
btn.FlatStyle = FlatStyle.Flat;
btn.Height = 32;
btn.Font = SavaTheme.ButtonFont;
btn.Cursor = Cursors.Hand;

// glavno (Tag = "primary") – najviše jedno po formi
btn.BackColor = SavaTheme.Primary;                  // #0A66B8
btn.ForeColor = Color.White;
btn.Font = SavaTheme.ButtonFontSemibold;
btn.FlatAppearance.BorderColor = SavaTheme.Primary;
btn.FlatAppearance.MouseOverBackColor = SavaTheme.PrimaryHover;   // #0958A0

// sporedno
btn.BackColor = Color.White;
btn.ForeColor = SavaTheme.Text;
btn.FlatAppearance.BorderColor = SavaTheme.FieldBorder;          // #B9C6D3
btn.FlatAppearance.MouseOverBackColor = SavaTheme.HoverLight;    // #EEF3F8

// opasno (Tag = "danger")
btn.BackColor = Color.White;
btn.ForeColor = SavaTheme.Danger;                                // #B3261E
btn.FlatAppearance.BorderSize = 0;
btn.FlatAppearance.MouseOverBackColor = SavaTheme.DangerHover;   // #FBEAEA
```

### DataGridView
```csharp
grid.EnableHeadersVisualStyles = false;          // bez ovoga se boje zaglavlja ignorišu
grid.BackgroundColor = Color.White;
grid.BorderStyle = BorderStyle.None;
grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
grid.GridColor = SavaTheme.GridLine;             // #E6ECF1

grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
grid.ColumnHeadersHeight = 32;
grid.ColumnHeadersDefaultCellStyle.BackColor = SavaTheme.GridHeader;     // #EAF0F6
grid.ColumnHeadersDefaultCellStyle.ForeColor = SavaTheme.Text;
grid.ColumnHeadersDefaultCellStyle.Font = SavaTheme.GridHeaderFont;      // Segoe UI Semibold 9
grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = SavaTheme.GridHeader;

grid.RowTemplate.Height = 30;
grid.DefaultCellStyle.Font = SavaTheme.FieldFont;
grid.DefaultCellStyle.ForeColor = SavaTheme.Text;
grid.DefaultCellStyle.BackColor = Color.White;
grid.DefaultCellStyle.SelectionBackColor = SavaTheme.SelectedRow;        // #DDF1FB
grid.DefaultCellStyle.SelectionForeColor = SavaTheme.Text;
grid.AlternatingRowsDefaultCellStyle.BackColor = Color.White;            // bez zebra redova

grid.RowHeadersVisible = false;   // samo ako se row header (strelica levo) ne koristi u logici
```
Numeričke kolone (Bruto, Tara, Neto, iznosi): `col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;`. Postojeći format brojeva ostaje isti.

Zaglavlja kolona pišeš kao reči, ne kao nazive iz baze: „Vag. potvrda“ umesto „VagarskaPotvrdaBr“, „Datum merenja“ umesto „DatumMerenja“, „Bruto kg“ umesto „Bruto“.

Opciono: cijan traka širine 3 px na levoj ivici izabranog reda, iscrtana u `RowPostPaint` bojom `#3DB5E6`.

### CheckBox
Ravan, sa okvirom iste boje kao polja za unos. Kvačica je u boji teksta. Plava se ne koristi, jer je rezervisana za glavno dugme. Čekiran i nečekiran checkbox razlikuju se samo po kvačici, a pod mišem dobija svetlu pozadinu.
```csharp
checkBox.FlatStyle = FlatStyle.Flat;
checkBox.FlatAppearance.BorderSize = 1;
checkBox.FlatAppearance.BorderColor = SavaTheme.FieldBorder;        // #B9C6D3
checkBox.FlatAppearance.CheckedBackColor = Color.White;
checkBox.FlatAppearance.MouseOverBackColor = SavaTheme.HoverLight;  // #EEF3F8
checkBox.FlatAppearance.MouseDownBackColor = SavaTheme.HoverLight;
checkBox.Font = SavaTheme.FieldFont;                                 // Segoe UI 10
checkBox.ForeColor = SavaTheme.Text;                                 // #172433, boja kvačice i teksta
checkBox.BackColor = Color.Transparent;
checkBox.UseVisualStyleBackColor = false;
checkBox.Cursor = Cursors.Hand;
```
Tekst checkbox-a piše se normalnim slovima, bez dvotačke: „Aktivna“, „Carinsko skladište“. U kodu se primenjuje preko `ThemeHelper.StyleCheckBox(checkBox)`; `ThemeHelper.Apply(this)` to radi za sve checkbox-ove na formi.

### DateTimePicker
Stil ograničeno može da se menja. Postavi samo `Font = SavaTheme.FieldFont`. Format datuma ostaje `dd.MM.yyyy. HH:mm`.

### Kontrole treće strane
Neke forme koriste grid sa trakom „Drag a column header here to group by that column“ i posebne datumske pikere. Pre izmena utvrdi iz kojih biblioteka su te kontrole (npr. DevExpress, Telerik, Infragistics) i primeni iste boje i fontove kroz njihov sistem izgleda (Appearance/Theme). Ne zamenjuj ih standardnim kontrolama.

---

## 5. Glavni prozor (zaglavlje i bočni meni)

- **Zaglavlje**: pozadina `#0A66B8`, logo levo, naslov forme centriran (Segoe UI Semibold 11, bela boja), dugmad prozora desno
- **Bočni meni**: pozadina `#12304D`
  - neaktivna stavka: tekst `#C7D3E0`, Segoe UI 10, bez pozadine
  - aktivna stavka: pozadina `#0A66B8`, tekst beo, Segoe UI Semibold 10
  - visina stavke oko 40 px, levi padding 20 px
- **Donja navigaciona traka**: pozadina `#12304D`, tekst `#C7D3E0`

---

## 6. Tekstovi u interfejsu

- Dugmad kažu tačno šta rade: „Sačuvaj“, „Novo“, „Obriši“, „Potvrdi vaganje“ (umesto „Potvrdi završeno“), „Osveži“ (umesto „Refresh“)
- Ista akcija ima isti naziv u celoj aplikaciji
- Oznake se pišu normalnim slovima, bez dvotačke na kraju
- Poruke o greškama kažu šta nije u redu i kako se popravlja: „Unesi broj kontejnera.“, a ne „Greška!“
- **Pre nego što promeniš tekst dugmeta ili oznake, proveri da kod ne poredi taj tekst** (npr. `if (btn.Text == "...")`). Ako ga poredi, tekst ostavi kakav jeste i to prijavi.

---

## 7. Kako primeniti u kodu

1. Napravi `Theme/SavaTheme.cs`. To je statička klasa sa svim bojama iz sekcije 1 (`static readonly Color`) i fontovima iz sekcije 2 (`static readonly Font`, kreiraju se jednom).
2. Napravi `Theme/ThemeHelper.cs` sa metodom `public static void Apply(Control root)`. Metoda rekurzivno prolazi kroz `root.Controls` i stilizuje kontrole po tipu, a dugmad i po `Tag`-u, prema sekciji 4.
3. Na kraju konstruktora svake forme, posle `InitializeComponent()`, dodaj poziv `ThemeHelper.Apply(this);`.
4. Glavnom dugmetu svake forme postavi `Tag = "primary"`, a dugmadi za brisanje i storniranje `Tag = "danger"`.
5. Ne menjaj ručno raspored u `.Designer.cs` fajlovima, osim kad je to potrebno zbog preklapanja posle promene fonta.

### Šta se NE menja
- poslovna logika, upiti, validacija
- nazivi kontrola i event handleri
- redosled tabova (TabIndex), osim ako je očigledno pogrešan
- format brojeva i datuma

### Redosled uvođenja
1. Pilot: forma za vaganje i glavni prozor. Pregled sa korisnikom.
2. Ostale forme, jedna po jedna. Posle svake proveri da nema preklapanja i da su tekstovi čitljivi.
3. Opciono: posebna kontrola za TextBox sa obojenim okvirom i cijan fokusom.
