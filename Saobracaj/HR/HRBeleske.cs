using Saobracaj.Theme;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Saobracaj.HR
{
    // Beleške o radu (specifikacija 6): vide ih autor, njegovi nadređeni i HR; piše nadređeni iz lanca iznad
    // zaposlenog ili HR; menja i briše samo autor. Provere su u procedurama sp_BeleskaSnimi / sp_BeleskaObrisi.
    public static class HRBeleske
    {
        public const string Tipovi =
            "SELECT 'POHVALA' AS Tip, N'Pohvala' AS Naziv UNION ALL SELECT 'PRIMEDBA', N'Primedba' " +
            "UNION ALL SELECT 'UPOZORENJE', N'Upozorenje' UNION ALL SELECT 'INFO', N'Informacija'";

        // Uslov vidljivosti beleške (b = alias tabele BeleskaORadu)
        public static string Vidljivost(bool hr, int ja)
        {
            if (hr)
                return "1 = 1";
            return "(b.AutorSifra = " + ja + " OR b.AutorSifra IN (SELECT Sifra FROM dbo.fn_Podredjeni(" + ja + ")))";
        }

        // datum == null: sve beleške zaposlenog (kartica zaposlenog); inače beleške jednog dana (prozor Dan)
        public static HRTabelaDef Def(int zaposleni, DateTime? datum, int ja, bool hr)
        {
            return Def(() => zaposleni, datum, ja, hr);
        }

        // zaposleni se čita pri snimanju (forma novog zaposlenog dobija šifru tek posle prvog snimanja)
        public static HRTabelaDef Def(Func<int> zaposleni, DateTime? datum, int ja, bool hr)
        {
            string uslovDana = datum.HasValue ? " AND b.Datum = '" + datum.Value.ToString("yyyyMMdd") + "'" : "";
            var polja = new List<HRPolje>();
            if (!datum.HasValue)
                polja.Add(HRPolje.Datum("Datum", "Datum", true));
            polja.Add(HRPolje.Lista("Tip", "Tip", Tipovi, true));
            polja.Add(new HRPolje("Tekst", "Tekst", HRTip.VelikiTekst, true, 4000) { Raspon = 2 });
            polja.Add(HRPolje.DaNe("VidljivoZaposlenom", "Vidljivo zaposlenom"));

            return new HRTabelaDef
            {
                Naslov = "Beleška o radu",
                Tabela = "BeleskaORadu",
                Kljuc = "ID",
                RoditeljKolona = "ZaposleniSifra",
                Upit = @"SELECT b.ID, b.Datum, b.Tip, b.Tekst, b.AutorSifra,
                                LTRIM(RTRIM(ISNULL(a.Prezime, N'') + N' ' + ISNULL(a.Ime, N''))) AS Autor,
                                CASE WHEN b.VidljivoZaposlenom = 1 THEN N'Da' ELSE N'Ne' END AS Vidljivo
                         FROM dbo.BeleskaORadu b LEFT JOIN dbo.Zaposleni a ON a.ID = b.AutorSifra
                         WHERE b.ZaposleniSifra = @Roditelj" + uslovDana + " AND " + Vidljivost(hr, ja) + @"
                         ORDER BY b.Datum DESC, b.ID DESC",
                Filteri = datum.HasValue ? null : new List<Tuple<string, string>>
                {
                    Tuple.Create("Sve", ""),
                    Tuple.Create("Pohvale", "Tip = 'POHVALA'"),
                    Tuple.Create("Primedbe", "Tip = 'PRIMEDBA'"),
                    Tuple.Create("Upozorenja", "Tip = 'UPOZORENJE'"),
                    Tuple.Create("Informacije", "Tip = 'INFO'")
                },
                Kolone = new[]
                {
                    new HRKolona("Datum", "Datum", 100, "datum"), new HRKolona("Tip", "Tip", 110),
                    new HRKolona("Tekst", "Tekst", 460), new HRKolona("Autor", "Autor", 180), new HRKolona("Vidljivo", "Vidi zaposleni", 110)
                },
                Polja = polja,
                NoviPodrazumevano = () => new HRVrednosti { ["Datum"] = datum ?? DateTime.Today, ["Tip"] = "INFO" },
                Snimi = (v, id) =>
                {
                    HRBaza.Procedura("dbo.sp_BeleskaSnimi",
                        HRBaza.P("@ID", id), HRBaza.P("@ZaposleniSifra", zaposleni()),
                        HRBaza.P("@Datum", datum.HasValue ? (object)datum.Value.Date : v.Uzmi("Datum")),
                        HRBaza.P("@AutorSifra", ja), HRBaza.P("@Tip", v.Uzmi("Tip")), HRBaza.P("@Tekst", v.Uzmi("Tekst")),
                        HRBaza.P("@VidljivoZaposlenom", v.Uzmi("VidljivoZaposlenom") ?? false), HRBaza.P("@HR", hr), HRBaza.P("@Korisnik", HRBaza.Korisnik));
                    return null;
                },
                Obrisi = id =>
                {
                    HRBaza.Procedura("dbo.sp_BeleskaObrisi", HRBaza.P("@ID", id), HRBaza.P("@AutorSifra", ja));
                    return null;
                },
                Dozvoljeno = () => ja > 0 || hr,
                PorukaZabranjeno = "Prijavljeni korisnik nije povezan sa zaposlenim (Korisnici.ZaposleniSifra), pa ne može da piše beleške.",
                BojaTeksta = (r, k) =>
                {
                    if (k != "Tip")
                        return null;
                    switch (Convert.ToString(r.GetValue("Tip")))
                    {
                        case "POHVALA": return HRStil.StatusVazi;
                        case "UPOZORENJE": return SavaTheme.Danger;
                        case "PRIMEDBA": return HRStil.StatusIstice;
                        default: return (Color?)null;
                    }
                }
            };
        }
    }
}
