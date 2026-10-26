using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Saobracaj.Administracija
{
    // Tabela Podesavanja (ključ–vrednost) za celu aplikaciju: učitava se u memoriju posle prijave,
    // posle izmene na formi Podešavanja poziva se Reload(). Ključ koji ne postoji ili je prazan
    // prijavljuje se porukom "Nedostaje podešavanje ..." umesto tihe vrednosti iz koda.
    public static class Podesavanja
    {
        public class Stavka
        {
            public int ID;
            public string Grupa;
            public string Kljuc;
            public string Vrednost;      // lozinka je ovde već dešifrovana
            public string Tip;
            public string Opis;
        }

        private static Dictionary<string, Stavka> stavke = new Dictionary<string, Stavka>(StringComparer.OrdinalIgnoreCase);
        public static bool Ucitano { get; private set; }

        private static string Konekcija => Sifarnici.frmLogovanje.connectionString;

        // Poziva se posle prijave. Ako tabela još ne postoji (skripta nije puštena), program radi kao do sada.
        public static void Ucitaj()
        {
            try
            {
                PopuniPodrazumevane();
                Reload();
                Primeni();
            }
            catch (Exception)
            {
                // tabela još ne postoji ili baza nije dostupna: program radi sa vrednostima iz koda
                Ucitano = false;
            }
        }

        public static void Reload()
        {
            var nove = new Dictionary<string, Stavka>(StringComparer.OrdinalIgnoreCase);
            using (var con = new SqlConnection(Konekcija))
            using (var cmd = new SqlCommand("SELECT ID, Grupa, Kljuc, Vrednost, TipVrednosti, Opis FROM dbo.Podesavanja", con))
            {
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                    while (dr.Read())
                    {
                        var s = new Stavka
                        {
                            ID = dr.GetInt32(0),
                            Grupa = dr.GetString(1),
                            Kljuc = dr.GetString(2),
                            Vrednost = dr.IsDBNull(3) ? null : dr.GetString(3),
                            Tip = dr.GetString(4).Trim().ToUpperInvariant(),
                            Opis = dr.IsDBNull(5) ? null : dr.GetString(5)
                        };
                        if (s.Tip == "PASSWORD")
                            s.Vrednost = Desifruj(s.Vrednost);
                        nove[s.Kljuc] = s;
                    }
            }
            stavke = nove;
            Ucitano = true;
        }

        public static IEnumerable<Stavka> Sve => stavke.Values;

        public static bool Postoji(string kljuc)
        {
            return stavke.TryGetValue(kljuc, out Stavka s) && !string.IsNullOrWhiteSpace(s.Vrednost);
        }

        public static string Get(string kljuc)
        {
            if (!stavke.TryGetValue(kljuc, out Stavka s) || string.IsNullOrWhiteSpace(s.Vrednost))
                throw new InvalidOperationException("Nedostaje podešavanje " + kljuc + " (Administracija > Podešavanja).");
            return s.Vrednost.Trim();
        }

        public static string Get(string kljuc, string akoNema)
        {
            return Postoji(kljuc) ? stavke[kljuc].Vrednost.Trim() : akoNema;
        }

        public static int GetInt(string kljuc)
        {
            string v = Get(kljuc);
            if (!int.TryParse(v, NumberStyles.Integer, CultureInfo.InvariantCulture, out int n))
                throw new InvalidOperationException("Podešavanje " + kljuc + " nije ceo broj: " + v);
            return n;
        }

        public static decimal GetDecimal(string kljuc)
        {
            string v = Get(kljuc).Replace(',', '.');
            if (!decimal.TryParse(v, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal d))
                throw new InvalidOperationException("Podešavanje " + kljuc + " nije broj: " + v);
            return d;
        }

        public static bool GetBool(string kljuc)
        {
            string v = Get(kljuc).ToUpperInvariant();
            return v == "1" || v == "TRUE" || v == "DA" || v == "YES";
        }

        public static DateTime GetDate(string kljuc)
        {
            string v = Get(kljuc);
            if (DateTime.TryParse(v, CultureInfo.GetCultureInfo("sr-Latn-RS"), DateTimeStyles.None, out DateTime d)
                || DateTime.TryParse(v, CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
                return d;
            throw new InvalidOperationException("Podešavanje " + kljuc + " nije datum: " + v);
        }

        // Snimanje jedne vrednosti (lozinka se šifruje); posle snimanja Reload
        public static void Set(string kljuc, string vrednost)
        {
            stavke.TryGetValue(kljuc, out Stavka s);
            string upis = s != null && s.Tip == "PASSWORD" ? Sifruj(vrednost) : vrednost;
            using (var con = new SqlConnection(Konekcija))
            using (var cmd = new SqlCommand(
                "UPDATE dbo.Podesavanja SET Vrednost = @V, NameOper = @U, DatumIzmene = GETDATE() WHERE Kljuc = @K", con))
            {
                cmd.Parameters.AddWithValue("@V", (object)upis ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@U", Korisnik());
                cmd.Parameters.AddWithValue("@K", kljuc);
                Otvori(con);
                cmd.ExecuteNonQuery();
            }
            Reload();
        }

        private static string Korisnik()
        {
            string k = (Sifarnici.frmLogovanje.user ?? "").Trim();
            return k.Length > 20 ? k.Substring(0, 20) : k;
        }

        private static void Otvori(SqlConnection con)
        {
            con.Open();
            try
            {
                using (var cmd = new SqlCommand("EXEC sp_set_session_context N'Korisnik', @k", con))
                {
                    cmd.Parameters.AddWithValue("@k", Korisnik());
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
            }
        }

        // Prazni ključevi firme i putanja Terminala se popunjavaju jednom, iz Config.cs i dosadašnjih
        // vrednosti u kodu, da ponašanje programa ostane isto dok se ne promene na formi.
        private static void PopuniPodrazumevane()
        {
            var vrednosti = new Dictionary<string, string>();
            try
            {
                CompanyConfiguration c = ConfigManager.GetCompanyConfiguration(Sifarnici.frmLogovanje.company);
                if (c != null)
                {
                    vrednosti["Firma.Naziv"] = c.Name_Value;
                    vrednosti["Firma.Adresa"] = c.Ulica_Value;
                    vrednosti["Firma.Mesto"] = c.Grad_Value;
                    vrednosti["Firma.MaticniBroj"] = c.MB_Value;
                    vrednosti["Firma.PIB"] = c.PIB;
                    if (!string.IsNullOrWhiteSpace(c.Dokumenta))
                        vrednosti["Putanje.HRResenja"] = Path.Combine(c.Dokumenta, "HR", "Resenja");
                }
            }
            catch
            {
                // nepoznata firma: podaci se unose na formi
            }
            vrednosti["Putanje.TerminalSlike"] = RNI.frmTerminalPrivremeniSlike.OsnovniFolder;
            vrednosti["Putanje.TerminalDokumenta"] = RNI.frmTerminalPrivremeniDokumenta.OsnovniFolder;

            using (var con = new SqlConnection(Konekcija))
            {
                Otvori(con);
                foreach (var kv in vrednosti)
                {
                    if (string.IsNullOrWhiteSpace(kv.Value))
                        continue;
                    using (var cmd = new SqlCommand(
                        "UPDATE dbo.Podesavanja SET Vrednost = @V, NameOper = 'program', DatumIzmene = GETDATE() " +
                        "WHERE Kljuc = @K AND (Vrednost IS NULL OR LTRIM(RTRIM(Vrednost)) = '')", con))
                    {
                        cmd.Parameters.AddWithValue("@V", kv.Value.Trim());
                        cmd.Parameters.AddWithValue("@K", kv.Key);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        // Putanje Terminala privremenog se čitaju iz tabele
        public static void Primeni()
        {
            if (Postoji("Putanje.TerminalSlike"))
                RNI.frmTerminalPrivremeniSlike.OsnovniFolder = SaKosomCrtom(Get("Putanje.TerminalSlike"));
            if (Postoji("Putanje.TerminalDokumenta"))
                RNI.frmTerminalPrivremeniDokumenta.OsnovniFolder = SaKosomCrtom(Get("Putanje.TerminalDokumenta"));
        }

        private static string SaKosomCrtom(string putanja)
        {
            return putanja.EndsWith("\\") ? putanja : putanja + "\\";
        }

        // ------------------------------------------------------------------ lozinke
        // AES sa ključem iz programa: lozinka nije čitljiva u bazi, a radi na svakom računaru.
        private static readonly byte[] Kljuc = Encoding.UTF8.GetBytes("SaobracajMTA#Podesavanja#2026!!!"); // 32 bajta
        private const string Prefiks = "enc:";

        public static string Sifruj(string tekst)
        {
            if (string.IsNullOrEmpty(tekst))
                return tekst;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Kljuc;
                aes.GenerateIV();
                using (ICryptoTransform t = aes.CreateEncryptor())
                {
                    byte[] ulaz = Encoding.UTF8.GetBytes(tekst);
                    byte[] izlaz = t.TransformFinalBlock(ulaz, 0, ulaz.Length);
                    byte[] sve = new byte[aes.IV.Length + izlaz.Length];
                    Buffer.BlockCopy(aes.IV, 0, sve, 0, aes.IV.Length);
                    Buffer.BlockCopy(izlaz, 0, sve, aes.IV.Length, izlaz.Length);
                    return Prefiks + Convert.ToBase64String(sve);
                }
            }
        }

        public static string Desifruj(string sifrovano)
        {
            if (string.IsNullOrEmpty(sifrovano) || !sifrovano.StartsWith(Prefiks))
                return sifrovano;   // upisano ručno u bazi, nešifrovano
            try
            {
                byte[] sve = Convert.FromBase64String(sifrovano.Substring(Prefiks.Length));
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Kljuc;
                    byte[] iv = new byte[16];
                    Buffer.BlockCopy(sve, 0, iv, 0, 16);
                    aes.IV = iv;
                    using (ICryptoTransform t = aes.CreateDecryptor())
                        return Encoding.UTF8.GetString(t.TransformFinalBlock(sve, 16, sve.Length - 16));
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
