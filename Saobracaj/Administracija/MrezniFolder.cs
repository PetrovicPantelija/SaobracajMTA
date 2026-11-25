using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;

namespace Saobracaj.Administracija
{
    // Prijava na deljeni folder na serveru (\\server\deljeni) korisnikom i lozinkom iz Podešavanja (Mreza.Korisnik,
    // Mreza.Lozinka), da Windows ne traži prijavu. Veza važi dok je korisnik prijavljen na Windows i za ceo server: koriste
    // je i ostali delovi programa i Explorer ('Otvori folder'). Ako Windows već ima pristup, ništa se ne radi.
    public static class MrezniFolder
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct NETRESOURCE
        {
            public int dwScope;
            public int dwType;
            public int dwDisplayType;
            public int dwUsage;
            public string lpLocalName;
            public string lpRemoteName;
            public string lpComment;
            public string lpProvider;
        }

        [DllImport("mpr.dll", CharSet = CharSet.Unicode)]
        private static extern int WNetAddConnection2(ref NETRESOURCE resurs, string lozinka, string korisnik, int opcije);

        private const int RESOURCETYPE_DISK = 1;
        private const int CONNECT_TEMPORARY = 4;   // veza se ne pamti posle odjave sa Windows-a

        private static readonly object zakljucavanje = new object();
        private static readonly HashSet<string> dostupni = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        // Obezbeđuje pristup deljenom folderu iz putanje. Ako pristup ne može da se dobije, baca IOException sa porukom za
        // korisnika (forme je prikazuju kao 'Folder nije dostupan: ...'). Lokalna putanja se ne proverava.
        public static void Povezi(string putanja)
        {
            string deljeni = DeljeniFolder(putanja);
            if (deljeni == null)
                return;

            lock (zakljucavanje)
            {
                if (dostupni.Contains(deljeni))
                    return;

                if (Directory.Exists(deljeni))   // Windows već ima pristup (sačuvana prijava, isti korisnik na serveru...)
                {
                    dostupni.Add(deljeni);
                    return;
                }

                string korisnik = Podesavanja.Get("Mreza.Korisnik", null);
                string lozinka = Podesavanja.Get("Mreza.Lozinka", null);
                if (korisnik == null)
                    throw new IOException("Nema pristupa folderu " + deljeni + ", a korisnik za mrežu nije unet "
                        + "(Administracija > Podešavanja, grupa Mreza: Mreza.Korisnik i Mreza.Lozinka).");

                var resurs = new NETRESOURCE { dwType = RESOURCETYPE_DISK, lpRemoteName = deljeni };
                int rezultat = WNetAddConnection2(ref resurs, lozinka, korisnik, CONNECT_TEMPORARY);
                if (rezultat != 0)
                    throw new IOException("Prijava na " + deljeni + " kao " + korisnik + " nije uspela: "
                        + new Win32Exception(rezultat).Message + " (kod " + rezultat + ")." + Savet(rezultat));

                dostupni.Add(deljeni);
            }
        }

        // \\server\deljeni iz \\server\deljeni\folder\...; null za lokalnu putanju
        private static string DeljeniFolder(string putanja)
        {
            if (string.IsNullOrEmpty(putanja) || !putanja.StartsWith(@"\\"))
                return null;

            string[] delovi = putanja.Substring(2).Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            return delovi.Length < 2 ? null : @"\\" + delovi[0] + @"\" + delovi[1];
        }

        private static string Savet(int kod)
        {
            switch (kod)
            {
                case 86:     // pogrešna lozinka
                case 1326:   // pogrešan korisnik ili lozinka
                    return " Proverite Mreza.Korisnik i Mreza.Lozinka u Podešavanjima; korisnik može da se upiše i kao server\\korisnik.";
                case 1219:   // Windows je na taj server već povezan drugim korisnikom
                    return " Windows je na ovaj server već povezan drugim korisnikom: odjavite se sa Windows-a i prijavite ponovo, pa pokušajte opet.";
                case 53:     // server nije pronađen
                case 67:     // deljeni folder nije pronađen
                    return " Proverite vezu sa serverom i putanju u Podešavanjima (grupa Putanje).";
                default:
                    return "";
            }
        }
    }
}
