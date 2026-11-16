# Klijent

Fajlovi koji se razmenjuju sa klijentima (Leget, Transagent, DPT, VSD).

| Folder | Šta ide ovde |
|---|---|
| `Ulaz/Specifikacije` | Zahtevi, opisi procesa, dopisi i mejlovi sa zahtevima |
| `Ulaz/SlikeEkrana` | Slike ekrana i snimci grešaka koje je klijent poslao |
| `Ulaz/Primeri` | Primeri dokumenata: carinske deklaracije, Excel tabele, PDF-ovi, XML |
| `Izlaz/Isporuke` | Instalacije i verzije poslate klijentu |
| `Izlaz/Dokumentacija` | Uputstva, zapisnici, ponude i ostalo što šaljemo klijentu |

Fajl se imenuje sa datumom na početku, npr. `2026-10-24 Vaganje - zahtev.pdf`.
Kad fajl važi samo za jednog klijenta, napravi podfolder sa nazivom klijenta, npr. `Ulaz/Primeri/Leget/`.

Sadržaj ovih foldera se **ne čuva u git-u**, jer klijentski dokumenti mogu sadržati poverljive podatke.
U git-u su samo struktura foldera i README fajlovi (pravilo je u `Klijent/.gitignore`).
