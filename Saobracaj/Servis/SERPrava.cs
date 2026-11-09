using System.Windows.Forms;

namespace Saobracaj.Servis
{
    // Prava u modulu Održavanje se čitaju iz istih tabela kao u glavnom prozoru (MainNovi, mainNoviKartice, MainNoviPrava).
    // Kartice su samo prava (poglavlje 7 specifikacije i nivoi odobravanja MD-R07); forme se otvaraju sa pravom Delovi.
    public static class SERPrava
    {
        public const string Modul = "Održavanje";
        public const string Pregled = "Delovi";
        public const string Magacioner = "Delovi magacioner";
        public const string Nabavka = "Delovi nabavka";
        public const string Korekcija = "Delovi korekcija";
        public const string OdobravanjeSef = "Odobravanje šef tehničke službe";
        public const string OdobravanjeTehnicki = "Odobravanje tehnički direktor";
        public const string OdobravanjeDirektor = "Odobravanje direktor";

        public static bool ImaPravo(string kartica)
        {
            try
            {
                object n = SERBaza.Skalar(@"
                    SELECT COUNT(*)
                    FROM dbo.MainNoviPrava p
                    JOIN dbo.mainNoviKartice k ON k.ID = p.KarticaID AND k.MainID = p.MainID
                    JOIN dbo.MainNovi m ON m.ID = p.MainID
                    WHERE LOWER(RTRIM(p.Korisnik)) = LOWER(RTRIM(@Korisnik))
                      AND LTRIM(RTRIM(m.Naziv)) = @Modul
                      AND LTRIM(RTRIM(k.Naziv)) = @Kartica",
                    SERBaza.P("@Korisnik", SERBaza.Korisnik), SERBaza.P("@Modul", Modul), SERBaza.P("@Kartica", kartica));
                return SERBaza.CeoBroj(n) > 0;
            }
            catch
            {
                return false;
            }
        }

        // Provera pre akcije: poruka korisniku ako pravo ne postoji
        public static bool Proveri(IWin32Window vlasnik, string kartica)
        {
            if (ImaPravo(kartica))
                return true;
            MessageBox.Show(vlasnik, "Za ovu akciju je potrebno pravo „" + kartica + "“ u modulu Održavanje.",
                "Pristup zabranjen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        // Odobravanje popisa i korekcije (MD-R10, MD-R11): šef tehničke službe ili viši nivo (tehnički direktor, direktor)
        public static bool ProveriOdobravanje(IWin32Window vlasnik)
        {
            if (NivoOdobravanja() >= 1)
                return true;
            MessageBox.Show(vlasnik, "Za odobravanje je potrebno pravo „" + OdobravanjeSef + "“ (ili „" + OdobravanjeTehnicki + "“, „" + OdobravanjeDirektor + "“) u modulu Održavanje.",
                "Pristup zabranjen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        // Najviši nivo odobravanja narudžbenice koji korisnik ima: 3 direktor, 2 tehnički direktor, 1 šef tehničke službe, 0 nijedan
        public static int NivoOdobravanja()
        {
            if (ImaPravo(OdobravanjeDirektor)) return 3;
            if (ImaPravo(OdobravanjeTehnicki)) return 2;
            if (ImaPravo(OdobravanjeSef)) return 1;
            return 0;
        }
    }
}
