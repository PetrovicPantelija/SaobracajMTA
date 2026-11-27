using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Saobracaj.Skladista_main
{
    // Broj C7 (smeštajni dokument) u obliku CI/C7/broj/godina, npr. 42072/C7/15622/2025:
    // 42072 – broj carinske ispostave, C7 – tip dokumenta, 15622 – broj dokumenta tog tipa, 2025 – godina.
    // Korisnik može da unese samo broj (15622), broj i godinu (15622/2025) ili stari oblik sa datumom
    // (42072/15622/15.04.2025); program dopunjuje ostalo. Tekst koji ne liči ni na jedan oblik ostaje kako je unet.
    internal static class BrojC7
    {
        public const string Tip = "C7";

        public static string Formiraj(string ci, string broj, int godina)
        {
            return (ci ?? "").Trim() + "/" + Tip + "/" + (broj ?? "").Trim() + "/" + godina;
        }

        // ci: oznaka carinske ispostave (može biti prazna: tada se ne dopunjuje ništa što traži ispostavu);
        // godina: podrazumevana godina kad je unos nema
        public static string Normalizuj(string unos, string ci, int godina)
        {
            string t = Regex.Replace((unos ?? "").Trim(), @"\s*/\s*", "/");
            if (t == "")
                return "";
            ci = (ci ?? "").Trim();
            bool imaCI = ci != "";
            Match m;

            // već u novom obliku (velika slova za tip)
            if ((m = Regex.Match(t, @"^(\d+)/C7/(\d+)/(\d{4})$", RegexOptions.IgnoreCase)).Success)
                return Formiraj(m.Groups[1].Value, m.Groups[2].Value, int.Parse(m.Groups[3].Value));
            // samo broj
            if (imaCI && (m = Regex.Match(t, @"^(?:C7/)?(\d+)$", RegexOptions.IgnoreCase)).Success)
                return Formiraj(ci, m.Groups[1].Value, godina);
            // broj/godina
            if (imaCI && (m = Regex.Match(t, @"^(?:C7/)?(\d+)/(\d{4})$", RegexOptions.IgnoreCase)).Success)
                return Formiraj(ci, m.Groups[1].Value, int.Parse(m.Groups[2].Value));
            // stari oblik: CI/broj/datum (42072/1296/15.04.2025) ili CI/broj/godina
            if ((m = Regex.Match(t, @"^(\d{4,6})/(\d+)/(?:\d{1,2}\.\d{1,2}\.)?(\d{4})\.?$")).Success)
                return Formiraj(m.Groups[1].Value, m.Groups[2].Value, int.Parse(m.Groups[3].Value));
            return t;
        }

        // oznaka carinske ispostave (Carinarnice.CIOznaka) za izabranu carinarnicu; "" kad je nema
        public static string OznakaCI(string connection, object idCarinarnice)
        {
            int id;
            if (idCarinarnice == null || !int.TryParse(idCarinarnice.ToString(), out id) || id <= 0)
                return "";
            try
            {
                using (var conn = new SqlConnection(connection))
                using (var cmd = new SqlCommand("SELECT CIOznaka FROM Carinarnice WHERE ID = @ID", conn))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    conn.Open();
                    object v = cmd.ExecuteScalar();
                    return v == null || v == DBNull.Value ? "" : v.ToString().Trim();
                }
            }
            catch
            {
                return "";
            }
        }

        public const string Opis = "Broj C7: carinska ispostava / C7 / broj / godina, npr. 42072/C7/15622/2025.\n" +
                                   "Dovoljno je uneti broj (15622): ispostava se uzima iz odredišne carinarnice, a godina je tekuća.";
    }
}
