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

        // null = ispravno; inače poruka za korisnika
        public static string Provera(string broj)
        {
            if (string.IsNullOrEmpty(broj))
                return null;
            if (!broj.All(char.IsDigit))
                return "Tarifni broj sme da sadrži samo cifre.";
            if (broj.Length == 8)
                return null;
            if (broj.Length == 10)
                return KolonaJeTekst ? null : "Tarifni broj od 10 cifara se prima tek posle skripte Carina_TarifniBroj_2026_11.sql. Unesite 8 cifara.";
            return "Tarifni broj mora imati 8 cifara (ili 10 za TARIC).";
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
