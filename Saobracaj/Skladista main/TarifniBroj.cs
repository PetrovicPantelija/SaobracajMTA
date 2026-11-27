using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Saobracaj.Skladista_main
{
    // Tarifni broj (kolona NHM na naimenovanju, prijemnici i otpremi) je tekst: 8 cifara, ili 10 (TARIC).
    // Vodeća nula se čuva (02071100). Pre puštanja skripte Carina_TarifniBroj_2026_11.sql kolone su int:
    // tada se prazno šalje kao 0, a broj od 10 cifara se ne prima (ne staje u int).
    // RAZ-01, RAZ-02, RAZ-03 (Carina-Izlaz-Tarife-Vlasnik-Kontejneri-Knjiga-Predlog v1.1).
    public static class TarifniBroj
    {
        public const int MaxDuzina = 10;

        static bool? kolonaJeTekst;

        // true kad je RNCarinskoPrijemnicaStavke.NHM tekst (skripta je puštena); proverava se jednom po pokretanju
        public static bool KolonaJeTekst
        {
            get
            {
                if (kolonaJeTekst.HasValue)
                    return kolonaJeTekst.Value;
                try
                {
                    using (var conn = new SqlConnection(Saobracaj.Sifarnici.frmLogovanje.connectionString))
                    using (var cmd = new SqlCommand(@"SELECT DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS
                        WHERE TABLE_NAME = 'RNCarinskoPrijemnicaStavke' AND COLUMN_NAME = 'NHM'", conn))
                    {
                        conn.Open();
                        string tip = Convert.ToString(cmd.ExecuteScalar());
                        kolonaJeTekst = tip.IndexOf("char", StringComparison.OrdinalIgnoreCase) >= 0;
                    }
                }
                catch
                {
                    return false;
                }
                return kolonaJeTekst.Value;
            }
        }

        // vrednost iz baze, Excela ili polja: samo cifre; 0 i prazno su "nije unet" ("")
        public static string Normalizuj(object v)
        {
            if (v == null || v == DBNull.Value)
                return "";
            string s = Convert.ToString(v).Trim();
            // Excel broj može doći kao 73211900.0
            if (s.EndsWith(".0") || s.EndsWith(",0"))
                s = s.Substring(0, s.Length - 2);
            s = new string(s.Where(char.IsDigit).ToArray());
            if (s.Trim('0') == "")
                return "";
            return s;
        }

        public const string Pravilo = "Tarifni broj ima 8 cifara, ili 10 za TARIC (npr. 73211900); vodeća nula se piše (02071100).";

        // tekst iz polja bez razmaka i znakova razdvajanja ("7321 19 00", "7321.19.00"); slova ostaju, da ih Provera prijavi
        public static string BezRazdvajanja(string tekst)
        {
            if (string.IsNullOrEmpty(tekst))
                return "";
            return new string(tekst.Where(c => !char.IsWhiteSpace(c) && c != '.' && c != '-' && c != '/').ToArray());
        }

        // null = ispravno; inače poruka za korisnika
        public static string Provera(string broj)
        {
            if (string.IsNullOrEmpty(broj))
                return null;
            if (!broj.All(char.IsDigit))
                return "Tarifni broj sme da sadrži samo cifre (uneto: " + broj + ").\n\n" + Pravilo;
            if (broj.Length != 8 && broj.Length != 10)
                return "Tarifni broj " + broj + " ima " + Cifara(broj.Length) + ".\n\n" + Pravilo;
            if (broj.Length == 10 && !KolonaJeTekst)
                return "Tarifni broj od 10 cifara se prima tek posle skripte Carina_TarifniBroj_2026_11.sql. Unesite 8 cifara.";
            // dok je kolona int, 02071100 bi se upisao kao 2071100
            if (broj[0] == '0' && !KolonaJeTekst)
                return "Tarifni broj sa vodećom nulom (" + broj + ") se čuva tek posle skripte Carina_TarifniBroj_2026_11.sql; " +
                       "do tada bi se upisao kao " + broj.TrimStart('0') + ".";
            return null;
        }

        static string Cifara(int n)
        {
            int d = n % 10;
            if (d == 1 && n % 100 != 11) return n + " cifru";
            if (d >= 2 && d <= 4 && (n % 100 < 12 || n % 100 > 14)) return n + " cifre";
            return n + " cifara";
        }

        // parametar procedure: VarChar(10); prazno je NULL, a dok je kolona int, 0
        public static SqlParameter Parametar(string ime, string broj, bool praznoKaoNula)
        {
            var p = new SqlParameter(ime, SqlDbType.VarChar, MaxDuzina);
            p.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(broj))
                p.Value = praznoKaoNula && !KolonaJeTekst ? (object)"0" : DBNull.Value;
            else
                p.Value = broj;
            return p;
        }
    }
}
