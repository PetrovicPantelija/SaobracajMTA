using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Saobracaj.HR
{
    // Vrsta rada iz šifarnika VrstaRada
    public class HRVrstaRada
    {
        public int ID;
        public string Oznaka;
        public string Naziv;
        public string Grupa;
        public bool CeoDan;
        public bool UlaziUFond;
        public bool SamoKrozZahtev;
        public Color? Boja;

        public static Dictionary<string, HRVrstaRada> Ucitaj()
        {
            var d = new Dictionary<string, HRVrstaRada>(StringComparer.OrdinalIgnoreCase);
            foreach (DataRow r in HRBaza.Tabela("SELECT ID, Oznaka, Naziv, Grupa, CeoDan, UlaziUFond, SamoKrozZahtev, Boja FROM dbo.VrstaRada WHERE Aktivno = 1 ORDER BY ISNULL(Redosled, 999), Oznaka").Rows)
            {
                var v = new HRVrstaRada
                {
                    ID = HRBaza.CeoBroj(r["ID"]),
                    Oznaka = Convert.ToString(r["Oznaka"]).Trim().ToUpperInvariant(),
                    Naziv = Convert.ToString(r["Naziv"]),
                    Grupa = Convert.ToString(r["Grupa"]).Trim(),
                    CeoDan = Convert.ToBoolean(r["CeoDan"]),
                    UlaziUFond = Convert.ToBoolean(r["UlaziUFond"]),
                    SamoKrozZahtev = Convert.ToBoolean(r["SamoKrozZahtev"])
                };
                if (r["Boja"] != DBNull.Value)
                {
                    int b = HRBaza.CeoBroj(r["Boja"]);
                    v.Boja = Color.FromArgb(255, (b >> 16) & 255, (b >> 8) & 255, b & 255);
                }
                d[v.Oznaka] = v;
            }
            return d;
        }
    }

    // Jedan red evidencije za prikaz u matrici
    public class HRStavkaDana
    {
        public int ID;
        public string Oznaka;
        public decimal Sati;
        public char Izvor;
    }

    // Rezultat tumačenja upisa u ćeliju
    public class HRUnosDana
    {
        public string Oznaka1;
        public decimal Sati1;
        public string Oznaka2;
        public decimal Sati2;
        public bool Brisanje;          // prazna ćelija ili Delete: briše se ručni unos
        public bool ZahtevGO;          // GO ide kroz zahtev
        public bool CeoDan;            // za opseg: vikendi i praznici se preskaču
    }

    public static class HREvidencija
    {
        public static readonly CultureInfo Srpski = CultureInfo.GetCultureInfo("sr-Latn-RS");
        public static readonly string[] DaniNedelje = { "Pon", "Uto", "Sre", "Čet", "Pet", "Sub", "Ned" };

        // 0 = ponedeljak … 6 = nedelja
        public static int DanUNedelji(DateTime d) => ((int)d.DayOfWeek + 6) % 7;

        public static string Broj(decimal v) => v.ToString("0.##", Srpski);

        // Kratke oznake iz specifikacije (5.3):
        //   8      -> RR 8 h (na vikend ili praznik: PR / RP)
        //   7,5K   -> RR 7,5 h + KS do dnevnog fonda
        //   8+2    -> RR 8 h + PR 2 h
        //   4P     -> PR 4 h (na praznik RP 4 h)
        //   BO ... -> ceo dan te vrste, sati = dnevni fond
        public static string Protumaci(string unos, decimal fond, bool neradan, bool praznik,
                                       Dictionary<string, HRVrstaRada> vrste, decimal podrazumevaniFond, out HRUnosDana u)
        {
            u = new HRUnosDana();
            string t = (unos ?? "").Trim().ToUpperInvariant().Replace(" ", "").Replace('.', ',');
            if (t == "")
            {
                u.Brisanje = true;
                return null;
            }
            decimal fondDana = fond > 0 ? fond : podrazumevaniFond;

            if (Regex.IsMatch(t, "^[A-ZČĆŠĐŽ]{2,5}$"))
            {
                if (!vrste.TryGetValue(t, out HRVrstaRada v))
                    return "Nepoznata oznaka „" + t + "“. Oznake su u šifarniku Vrste rada.";
                if (v.SamoKrozZahtev)
                {
                    u.ZahtevGO = true;
                    return null;
                }
                u.Oznaka1 = v.Oznaka;
                u.Sati1 = v.CeoDan ? fondDana : fond;
                u.CeoDan = v.CeoDan;
                if (!v.CeoDan && fond <= 0)
                    return "Za oznaku " + v.Oznaka + " upišite i broj sati (npr. 4" + (v.Oznaka == "PR" ? "P" : "") + ").";
                return null;
            }

            Match m;
            if ((m = Regex.Match(t, @"^(\d+(,\d+)?)K$")).Success)
            {
                decimal h = Dec(m.Groups[1].Value);
                u.Oznaka1 = "RR";
                u.Sati1 = h;
                if (fondDana - h > 0)
                {
                    u.Oznaka2 = "KS";
                    u.Sati2 = fondDana - h;
                }
                return Proveri(u.Sati1 + u.Sati2);
            }
            if ((m = Regex.Match(t, @"^(\d+(,\d+)?)\+(\d+(,\d+)?)$")).Success)
            {
                u.Oznaka1 = "RR";
                u.Sati1 = Dec(m.Groups[1].Value);
                u.Oznaka2 = "PR";
                u.Sati2 = Dec(m.Groups[3].Value);
                return Proveri(u.Sati1 + u.Sati2);
            }
            if ((m = Regex.Match(t, @"^(\d+(,\d+)?)P$")).Success)
            {
                u.Oznaka1 = praznik ? "RP" : "PR";
                u.Sati1 = Dec(m.Groups[1].Value);
                return Proveri(u.Sati1);
            }
            if ((m = Regex.Match(t, @"^(\d+(,\d+)?)$")).Success)
            {
                decimal h = Dec(m.Groups[1].Value);
                u.Oznaka1 = praznik ? "RP" : neradan ? "PR" : "RR";
                u.Sati1 = h;
                return Proveri(h);
            }
            return "Upis „" + unos + "“ nije prepoznat. Primeri: 8, 7,5K, 8+2, 4P, BO, PL, SD.";
        }

        private static string Proveri(decimal sati)
        {
            if (sati <= 0)
                return "Broj sati mora biti veći od nule.";
            return sati > 24 ? "Dan nema više od 24 sata." : null;
        }

        private static decimal Dec(string s) => decimal.Parse(s.Replace(',', '.'), CultureInfo.InvariantCulture);

        // Skraćeni prikaz dana u ćeliji (obrnuto od Protumaci)
        public static string Prikaz(List<HRStavkaDana> stavke, Dictionary<string, HRVrstaRada> vrste)
        {
            if (stavke == null || stavke.Count == 0)
                return "";
            HRStavkaDana celi = stavke.FirstOrDefault(s => vrste.TryGetValue(s.Oznaka, out HRVrstaRada v) && v.CeoDan);
            if (celi != null && stavke.Count == 1)
                return celi.Oznaka;

            decimal rr = stavke.Where(s => s.Oznaka == "RR").Sum(s => s.Sati);
            decimal pr = stavke.Where(s => s.Oznaka == "PR" || s.Oznaka == "RP").Sum(s => s.Sati);
            decimal ks = stavke.Where(s => s.Oznaka == "KS").Sum(s => s.Sati);
            bool samoPoznate = stavke.All(s => s.Oznaka == "RR" || s.Oznaka == "PR" || s.Oznaka == "RP" || s.Oznaka == "KS");
            if (samoPoznate)
            {
                if (rr > 0 && ks > 0 && pr == 0) return Broj(rr) + "K";
                if (rr > 0 && pr > 0 && ks == 0) return Broj(rr) + "+" + Broj(pr);
                if (rr == 0 && pr > 0 && ks == 0) return Broj(pr) + "P";
                if (rr > 0 && pr == 0 && ks == 0) return Broj(rr);
            }
            return string.Join(" ", stavke.Select(s => vrste.TryGetValue(s.Oznaka, out HRVrstaRada v) && v.CeoDan ? s.Oznaka : s.Oznaka + Broj(s.Sati)));
        }

        public static string ImeMeseca(int mesec)
        {
            return Srpski.DateTimeFormat.GetMonthName(mesec);
        }
    }
}
