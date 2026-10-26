using System;
using System.Collections.Generic;

namespace Saobracaj.HR
{
    public enum HRTip
    {
        Tekst,
        VelikiTekst,
        CeoBroj,
        Decimalni,
        Datum,
        DatumVreme,
        Lista,       // ComboBox iz upita: prva kolona vrednost, druga tekst
        DaNe
    }

    // Opis jednog polja u dijalogu za unos (frmHRStavka) i kolone u bazi
    public class HRPolje
    {
        public string Kolona;
        public string Oznaka;
        public HRTip Tip;
        public bool Obavezno;
        public int Duzina;              // najveći broj znakova za tekst
        public string ListaUpit;        // za HRTip.Lista
        public bool SamoCitanje;
        public object Podrazumevano;
        public int Raspon = 1;          // broj kolona rasporeda koje polje zauzima
        public bool Snima = true;       // false: polje se samo prikazuje, ne upisuje u bazu
        public decimal Min = 0;
        public decimal Max = 999999999;

        public HRPolje(string kolona, string oznaka, HRTip tip, bool obavezno = false, int duzina = 0)
        {
            Kolona = kolona;
            Oznaka = oznaka;
            Tip = tip;
            Obavezno = obavezno;
            Duzina = duzina;
        }

        public static HRPolje Tekst(string kolona, string oznaka, int duzina, bool obavezno = false)
        {
            return new HRPolje(kolona, oznaka, HRTip.Tekst, obavezno, duzina);
        }

        public static HRPolje Napomena(string kolona = "Napomena", string oznaka = "Napomena", int duzina = 500)
        {
            return new HRPolje(kolona, oznaka, HRTip.VelikiTekst, false, duzina) { Raspon = 2 };
        }

        public static HRPolje Datum(string kolona, string oznaka, bool obavezno = false)
        {
            return new HRPolje(kolona, oznaka, HRTip.Datum, obavezno);
        }

        public static HRPolje Lista(string kolona, string oznaka, string upit, bool obavezno = false)
        {
            return new HRPolje(kolona, oznaka, HRTip.Lista, obavezno) { ListaUpit = upit };
        }

        public static HRPolje Broj(string kolona, string oznaka, bool obavezno = false, decimal min = 0, decimal max = 999999999)
        {
            return new HRPolje(kolona, oznaka, HRTip.CeoBroj, obavezno) { Min = min, Max = max };
        }

        public static HRPolje Decimalni(string kolona, string oznaka, bool obavezno = false)
        {
            return new HRPolje(kolona, oznaka, HRTip.Decimalni, obavezno);
        }

        public static HRPolje DaNe(string kolona, string oznaka, bool podrazumevano = false)
        {
            return new HRPolje(kolona, oznaka, HRTip.DaNe) { Podrazumevano = podrazumevano };
        }
    }

    // Liste koje se ponavljaju u više formi
    public static class HRListe
    {
        public const string AktivniZaposleni =
            "SELECT ID, LTRIM(RTRIM(ISNULL(Prezime, N'') + N' ' + ISNULL(Ime, N''))) + ISNULL(N' (' + Code + N')', N'') AS Naziv FROM dbo.Zaposleni WHERE Status = 'A' ORDER BY 2";
        public const string SviZaposleni =
            "SELECT ID, LTRIM(RTRIM(ISNULL(Prezime, N'') + N' ' + ISNULL(Ime, N''))) + ISNULL(N' (' + Code + N')', N'') AS Naziv FROM dbo.Zaposleni ORDER BY 2";
        public const string OrganizacioneJedinice = "SELECT ID, Naziv FROM dbo.OrganizacioneJedinice ORDER BY Naziv";
        public const string SluzbenaMesta = "SELECT ID, RTRIM(Opis) AS Naziv FROM dbo.Stanice ORDER BY Opis";
        public const string Drzave = "SELECT RTRIM(DrSifra) AS Sifra, RTRIM(DrNaziv) AS Naziv FROM dbo.Drzave ORDER BY DrNaziv";
        public const string Opstine = "SELECT Sifra, Naziv FROM dbo.Opstine WHERE Aktivan = 1 ORDER BY Naziv";
        public const string Partneri = "SELECT PaSifra, RTRIM(PaNaziv) AS Naziv FROM dbo.Partnerji ORDER BY PaNaziv";
        public const string Valute = "SELECT RTRIM(VaSifra) AS Sifra, RTRIM(VaSifra) AS Naziv FROM dbo.Valute ORDER BY VaSifra";

        // Šifarnik sa kolonama Sifra, Naziv, Aktivan
        public static string Sifarnik(string tabela)
        {
            return "SELECT Sifra, Naziv FROM dbo." + tabela + " WHERE Aktivan = 1 ORDER BY Naziv";
        }
    }

    // Vrednosti dijaloga po nazivu kolone
    public class HRVrednosti : Dictionary<string, object>
    {
        public HRVrednosti() : base(StringComparer.OrdinalIgnoreCase) { }

        public HRVrednosti(IDictionary<string, object> izvor) : base(izvor, StringComparer.OrdinalIgnoreCase) { }

        public object Uzmi(string kolona)
        {
            return TryGetValue(kolona, out object v) ? v : null;
        }

        public int Broj(string kolona)
        {
            return HRBaza.CeoBroj(Uzmi(kolona));
        }

        public DateTime? Datum(string kolona)
        {
            return HRBaza.Datum(Uzmi(kolona));
        }

        public string Tekst(string kolona)
        {
            object v = Uzmi(kolona);
            return v == null || v == DBNull.Value ? "" : Convert.ToString(v);
        }
    }
}
