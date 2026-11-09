using System;
using System.Collections.Generic;

namespace Saobracaj.Servis
{
    public enum SERTip
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

    // Opis jednog polja u dijalogu za unos (frmSERStavka) i kolone u bazi
    public class SERPolje
    {
        public string Kolona;
        public string Oznaka;
        public SERTip Tip;
        public bool Obavezno;
        public int Duzina;              // najveći broj znakova za tekst
        public string ListaUpit;        // za SERTip.Lista
        public bool SamoCitanje;
        public object Podrazumevano;
        public int Raspon = 1;          // broj kolona rasporeda koje polje zauzima
        public bool Snima = true;       // false: polje se samo prikazuje, ne upisuje u bazu
        public decimal Min = 0;
        public decimal Max = 999999999;
        public string NazivUPoruci;     // naziv polja u porukama o grešci, ako se razlikuje od oznake (npr. oznaka sadrži gornju granicu)
        public bool Pozitivan;          // mora biti veće od 0 (količina), a ne samo ≥ Min
        public bool NeNula;             // sme biti pozitivno ili negativno, ali ne 0 (korekcija zalihe)

        public SERPolje(string kolona, string oznaka, SERTip tip, bool obavezno = false, int duzina = 0)
        {
            Kolona = kolona;
            Oznaka = oznaka;
            Tip = tip;
            Obavezno = obavezno;
            Duzina = duzina;
        }

        public static SERPolje Tekst(string kolona, string oznaka, int duzina, bool obavezno = false)
        {
            return new SERPolje(kolona, oznaka, SERTip.Tekst, obavezno, duzina);
        }

        public static SERPolje Napomena(string kolona = "Napomena", string oznaka = "Napomena", int duzina = 500)
        {
            return new SERPolje(kolona, oznaka, SERTip.VelikiTekst, false, duzina) { Raspon = 2 };
        }

        public static SERPolje Datum(string kolona, string oznaka, bool obavezno = false)
        {
            return new SERPolje(kolona, oznaka, SERTip.Datum, obavezno);
        }

        public static SERPolje Lista(string kolona, string oznaka, string upit, bool obavezno = false)
        {
            return new SERPolje(kolona, oznaka, SERTip.Lista, obavezno) { ListaUpit = upit };
        }

        public static SERPolje Broj(string kolona, string oznaka, bool obavezno = false, decimal min = 0, decimal max = 999999999)
        {
            return new SERPolje(kolona, oznaka, SERTip.CeoBroj, obavezno) { Min = min, Max = max };
        }

        public static SERPolje Decimalni(string kolona, string oznaka, bool obavezno = false)
        {
            return new SERPolje(kolona, oznaka, SERTip.Decimalni, obavezno);
        }

        // Količina: obavezna i veća od 0; prazno, nula, negativno i tekst se odbijaju u formi, pre poziva baze
        public static SERPolje Kolicina(string kolona, string oznaka, decimal max = 999999999)
        {
            return new SERPolje(kolona, oznaka, SERTip.Decimalni, true) { Pozitivan = true, Max = max };
        }

        public static SERPolje DaNe(string kolona, string oznaka, bool podrazumevano = false)
        {
            return new SERPolje(kolona, oznaka, SERTip.DaNe) { Podrazumevano = podrazumevano };
        }
    }

    // Liste koje se ponavljaju u više formi
    public static class SERListe
    {
        public const string Partneri = "SELECT PaSifra, RTRIM(PaNaziv) AS Naziv FROM dbo.Partnerji ORDER BY PaNaziv";
        public const string Grupe = "SELECT ID, Naziv FROM dbo.SERDeoGrupa WHERE Aktivno = 1 ORDER BY Naziv";
        public const string VrsteSredstava = "SELECT ID, Naziv FROM dbo.SERVrstaSredstva WHERE Aktivno = 1 ORDER BY Naziv";
        public const string Sredstva = "SELECT ID, Oznaka + N' – ' + Naziv AS Naziv FROM dbo.SERSredstvo WHERE Aktivno = 1 ORDER BY Oznaka";
        public const string PlanStavke = "SELECT p.ID, v.Naziv + N' – ' + p.Naziv AS Naziv FROM dbo.SERPlanStavka p JOIN dbo.SERVrstaSredstva v ON v.ID = p.VrstaSredstvaID WHERE p.Aktivno = 1 ORDER BY 2";
        public const string AktivniDelovi = "SELECT ID, Sifra + N' – ' + Naziv AS Naziv FROM dbo.SERDeo WHERE Aktivno = 1 ORDER BY Sifra";
    }

    // Vrednosti dijaloga po nazivu kolone
    public class SERVrednosti : Dictionary<string, object>
    {
        public SERVrednosti() : base(StringComparer.OrdinalIgnoreCase) { }

        public SERVrednosti(IDictionary<string, object> izvor) : base(izvor, StringComparer.OrdinalIgnoreCase) { }

        public object Uzmi(string kolona)
        {
            return TryGetValue(kolona, out object v) ? v : null;
        }

        public int Broj(string kolona)
        {
            return SERBaza.CeoBroj(Uzmi(kolona));
        }

        public DateTime? Datum(string kolona)
        {
            return SERBaza.Datum(Uzmi(kolona));
        }

        public string Tekst(string kolona)
        {
            object v = Uzmi(kolona);
            return v == null || v == DBNull.Value ? "" : Convert.ToString(v);
        }
    }
}
