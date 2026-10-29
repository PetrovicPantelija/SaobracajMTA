using System;

namespace Saobracaj.HR
{
    // Prava u HR modulu se čitaju iz istih tabela kao u glavnom prozoru (MainNovi, mainNoviKartice, MainNoviPrava).
    // Kartice 'Osetljivi podaci' i 'Obrada nabavke' nemaju formu; služe samo kao pravo.
    public static class HRPrava
    {
        public const string Modul = "HR modul";
        public const string OsetljiviPodaci = "Osetljivi podaci";
        public const string ObradaNabavke = "Obrada nabavke";
        public const string EvidencijaRada = "Evidencija rada";
        public const string ZakljucavanjeMeseca = "Zaključavanje meseca";
        public const string OdobravanjeZahteva = "Odobravanje zahteva";
        public const string GodisnjiOdmori = "Godišnji odmori";
        public const string BeleskeORadu = "Beleške o radu";

        // HR vidi sve zaposlene; šef vidi samo svoj tim (fn_Podredjeni)
        public static bool JeHR()
        {
            return ImaPravo(ZakljucavanjeMeseca) || ImaPravo(GodisnjiOdmori);
        }

        public static bool ImaPravo(string kartica)
        {
            try
            {
                object n = HRBaza.Skalar(@"
                    SELECT COUNT(*)
                    FROM dbo.MainNoviPrava p
                    JOIN dbo.mainNoviKartice k ON k.ID = p.KarticaID AND k.MainID = p.MainID
                    JOIN dbo.MainNovi m ON m.ID = p.MainID
                    WHERE LOWER(RTRIM(p.Korisnik)) = LOWER(RTRIM(@Korisnik))
                      AND LTRIM(RTRIM(m.Naziv)) = @Modul
                      AND LTRIM(RTRIM(k.Naziv)) = @Kartica",
                    HRBaza.P("@Korisnik", HRBaza.Korisnik), HRBaza.P("@Modul", Modul), HRBaza.P("@Kartica", kartica));
                return HRBaza.CeoBroj(n) > 0;
            }
            catch
            {
                return false;
            }
        }

        // Zaposleni povezan sa prijavljenim korisnikom: Korisnici.ZaposleniSifra, a ako veza tamo nije upisana,
        // Zaposleni.KorisnickoIme; 0 ako veza ne postoji
        public static int TrenutniZaposleni()
        {
            object id = null;
            try
            {
                id = HRBaza.Skalar(
                    "SELECT TOP (1) ZaposleniSifra FROM dbo.Korisnici WHERE LOWER(RTRIM(Korisnik)) = LOWER(RTRIM(@Korisnik)) AND ZaposleniSifra IS NOT NULL",
                    HRBaza.P("@Korisnik", HRBaza.Korisnik));
            }
            catch (System.Data.SqlClient.SqlException)
            {
                // kolona Korisnici.ZaposleniSifra još ne postoji (skripta faze 2 nije puštena)
            }
            if (HRBaza.CeoBroj(id) > 0)
                return HRBaza.CeoBroj(id);
            id = HRBaza.Skalar(
                "SELECT TOP (1) ID FROM dbo.Zaposleni WHERE LOWER(RTRIM(KorisnickoIme)) = LOWER(RTRIM(@Korisnik)) ORDER BY ID",
                HRBaza.P("@Korisnik", HRBaza.Korisnik));
            return HRBaza.CeoBroj(id);
        }

        // Zaposleni koje prijavljeni korisnik vidi: HR sve, šef svoj tim (ceo lanac ispod sebe)
        public static string FilterTima(string kolonaZaposlenog, bool hr, int ja)
        {
            if (hr)
                return "1 = 1";
            return kolonaZaposlenog + " IN (SELECT Sifra FROM dbo.fn_Podredjeni(" + ja + "))";
        }

        // Zaštita podataka (7.5): svaki pristup osetljivoj kartici se beleži
        public static void ZabeleziPristup(int zaposleni, string kartica)
        {
            try
            {
                HRBaza.Izvrsi("INSERT dbo.ZaposleniPristupLog (Zaposleni, Kartica, Korisnik) VALUES (@Z, @K, @U)",
                    HRBaza.P("@Z", zaposleni), HRBaza.P("@K", kartica), HRBaza.P("@U", HRBaza.Korisnik));
            }
            catch (Exception)
            {
                // beleženje ne sme da spreči rad sa formom
            }
        }
    }
}
