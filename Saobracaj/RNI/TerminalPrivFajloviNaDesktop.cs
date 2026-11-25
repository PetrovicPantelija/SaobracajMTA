using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.RNI
{
    // Izvoz slika i dokumenata zapisa (TerminalPriv ili TerminalPrivArhiv) na Desktop lokalnog računara, za slanje komitentima.
    // Za svaki kontejner pravi se folder Desktop\<kontejner> sa svim slikama i dokumentima zapisa. Fajl istog imena iz ranijeg
    // izvoza se zamenjuje, pa se izvoz može ponoviti.
    public static class TerminalPrivFajloviNaDesktop
    {
        private const string Naslov = "Izvezi slike i dokumenta";

        // zapisi: ID zapisa i kontejner
        public static void Izvezi(IWin32Window vlasnik, IList<KeyValuePair<int, string>> zapisi)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            var izvori = new[] { frmTerminalPrivremeniSlike.OsnovniFolder, frmTerminalPrivremeniDokumenta.OsnovniFolder };

            foreach (string osnovni in izvori)
            {
                if (!Directory.Exists(osnovni))
                {
                    MessageBox.Show(vlasnik, "Folder " + osnovni + " nije dostupan. Proverite vezu sa serverom.", Naslov,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            var folderi = new List<string>();     // folderi na Desktop-u u koje je nešto izvezeno
            var bezFajlova = new List<string>();
            var greske = new List<string>();
            int brojFajlova = 0;

            // imena već iskopirana u ovom izvozu, po folderu: slika i dokument istog imena (ili dva zapisa istog kontejnera)
            // ne zamenjuju jedan drugog
            var kopirano = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                foreach (KeyValuePair<int, string> zapis in zapisi)
                {
                    string ime = ImeFoldera(zapis.Key, zapis.Value);
                    List<string> fajlovi = izvori.SelectMany(osnovni => Fajlovi(Path.Combine(osnovni, zapis.Key.ToString()))).ToList();
                    if (fajlovi.Count == 0)
                    {
                        bezFajlova.Add(ime);
                        continue;
                    }

                    string cilj = Path.Combine(desktop, ime);
                    try
                    {
                        Directory.CreateDirectory(cilj);
                    }
                    catch (Exception ex)
                    {
                        greske.Add(cilj + ": " + ex.Message);
                        continue;
                    }

                    HashSet<string> imena;
                    if (!kopirano.TryGetValue(cilj, out imena))
                    {
                        imena = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                        kopirano[cilj] = imena;
                        folderi.Add(cilj);
                    }

                    foreach (string izvor in fajlovi)
                    {
                        string imeFajla = SlobodnoIme(imena, Path.GetFileName(izvor));
                        try
                        {
                            File.Copy(izvor, Path.Combine(cilj, imeFajla), true);
                            imena.Add(imeFajla);
                            brojFajlova++;
                        }
                        catch (Exception ex)
                        {
                            greske.Add(ime + "\\" + Path.GetFileName(izvor) + ": " + ex.Message);
                        }
                    }
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

            PrikaziRezultat(vlasnik, brojFajlova, folderi, bezFajlova, greske);
        }

        private static void PrikaziRezultat(IWin32Window vlasnik, int brojFajlova, List<string> folderi, List<string> bezFajlova, List<string> greske)
        {
            if (brojFajlova == 0 && greske.Count == 0)
            {
                MessageBox.Show(vlasnik, (bezFajlova.Count == 1 ? "Kontejner " + bezFajlova[0] + " nema" : "Izabrani kontejneri nemaju")
                    + " slika ni dokumenata.", Naslov, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var poruka = new System.Text.StringBuilder();
            poruka.AppendLine("Izvezeno fajlova: " + brojFajlova);
            poruka.AppendLine((folderi.Count == 1 ? "Folder: " : "Folderi: ") + string.Join(", ", folderi));
            if (bezFajlova.Count > 0)
                poruka.AppendLine("Bez slika i dokumenata: " + string.Join(", ", bezFajlova));
            if (greske.Count > 0)
            {
                poruka.AppendLine();
                poruka.AppendLine("Nije izvezeno:");
                foreach (string greska in greske.Take(10))
                    poruka.AppendLine(greska);
                if (greske.Count > 10)
                    poruka.AppendLine("... i još " + (greske.Count - 10));
            }

            if (folderi.Count == 1)
            {
                poruka.AppendLine();
                poruka.Append("Otvoriti folder?");
                if (MessageBox.Show(vlasnik, poruka.ToString(), Naslov, MessageBoxButtons.YesNo,
                    greske.Count > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Question) == DialogResult.Yes)
                    Process.Start("explorer.exe", "\"" + folderi[0] + "\"");
                return;
            }

            MessageBox.Show(vlasnik, poruka.ToString(), Naslov, MessageBoxButtons.OK,
                greske.Count > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
        }

        // Fajlovi foldera zapisa, bez skrivenih i sistemskih (Thumbs.db, desktop.ini)
        private static IEnumerable<string> Fajlovi(string folder)
        {
            if (!Directory.Exists(folder))
                return new string[0];

            return Directory.GetFiles(folder)
                .Where(f => (File.GetAttributes(f) & (FileAttributes.Hidden | FileAttributes.System)) == 0)
                .OrderBy(f => f, StringComparer.OrdinalIgnoreCase);
        }

        // Ime foldera je kontejner, bez znakova koje Windows ne dozvoljava; zapis bez kontejnera dobija "ID <broj>"
        private static string ImeFoldera(int id, string kontejner)
        {
            string ime = new string((kontejner ?? "").Trim().Where(z => !Path.GetInvalidFileNameChars().Contains(z)).ToArray()).Trim().TrimEnd('.');
            return ime.Length > 0 ? ime : "ID " + id;
        }

        // Ime koje u ovom izvozu još nije iskorišćeno u folderu: slika.jpg, slika (2).jpg, ...
        private static string SlobodnoIme(HashSet<string> imena, string imeFajla)
        {
            if (!imena.Contains(imeFajla))
                return imeFajla;

            string ime = Path.GetFileNameWithoutExtension(imeFajla);
            string ekstenzija = Path.GetExtension(imeFajla);
            for (int i = 2; ; i++)
            {
                string novo = ime + " (" + i + ")" + ekstenzija;
                if (!imena.Contains(novo))
                    return novo;
            }
        }
    }
}
