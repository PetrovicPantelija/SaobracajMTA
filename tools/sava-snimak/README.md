# Sava snimak

Provera redizajna Sava bez baze: pravi formu (samo `InitializeComponent`, bez konstruktora i `Load`), snima sliku pre i posle `PrimeniSava()` i prijavljuje nova preklapanja, odsečen tekst na dugmadima i oznakama, kontrole van roditelja i plava dugmad (sme da bude najviše jedno).

Forma se prikazuje kao u `NewMain` (bez okvira, 1600×860), a sa nastavkom `:own` kao samostalan prozor.

Prevođenje (jednom):

```
"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\Roslyn\csc.exe" -nologo -target:winexe -out:SavaSnimak.exe -r:System.Windows.Forms.dll -r:System.Drawing.dll SavaSnimak.cs
```

Pokretanje (prvo Debug build projekta):

```
SavaSnimak.exe ..\..\Saobracaj\bin\Debug <izlazni folder> Saobracaj.Skladista_main.SkladistaMain Saobracaj.Skladista_main.frmVozac:own
```

U izlaznom folderu su `Forma.pre.png`, `Forma.posle.png` i `izvestaj.txt`. Forme koje se prave u kodu (bez `InitializeComponent`) alat ne može da napravi.

Alat čita Syncfusion licencu iz `Saobracaj\Program.cs` (putanja je u kodu, `RegistrujLicencu`).
