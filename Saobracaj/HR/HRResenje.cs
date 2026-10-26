using Saobracaj.Administracija;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Saobracaj.HR
{
    // Rešenje o korišćenju godišnjeg odmora (specifikacija 8.3): tekst iz šablona HR.ResenjeSablon,
    // {Ključ} = podešavanje (firma, pravni osnov, pouka), [Polje] = podatak iz zahteva i kartice zaposlenog.
    // PDF ide u Putanje.HRResenja\<godina>\, šalje se mailom; štampa se kad zaposleni nema email.
    public static class HRResenje
    {
        public static DataRow Podaci(int stavka)
        {
            return HRBaza.Red(@"
                SELECT s.ID, s.DatumOd, s.DatumDo, s.BrojDana, s.DatumPovratka, s.DatumZahteva, s.BrojResenja, s.DatumResenja,
                       s.PutanjaResenja, s.StatusID, d.Godina, d.UkupnoDana, d.PotrosenoDana, t.PrezimeIme, z.JMBG, z.email,
                       LTRIM(RTRIM(ISNULL(z.AdresaStalnoPreb, N'') + ISNULL(N', ' + o.Naziv, N''))) AS Prebivaliste,
                       ISNULL(t.RadnoMestoNaziv, N'') AS RadnoMesto
                FROM dbo.GODopustStavke s
                JOIN dbo.GODopust d ON d.ID = s.DopustID
                JOIN dbo.Zaposleni z ON z.ID = d.ZaposleniSifra
                JOIN dbo.vZaposleniTrenutno t ON t.Zaposleni = z.ID
                LEFT JOIN dbo.Opstine o ON o.Sifra = z.OpstinaStalnoPreb
                WHERE s.ID = @S", HRBaza.P("@S", stavka));
        }

        private static string D(object v) => v == null || v == DBNull.Value ? "" : Convert.ToDateTime(v).ToString("dd.MM.yyyy.");

        public static string Tekst(DataRow r)
        {
            if (!Podesavanja.Ucitano)
                Podesavanja.Reload();
            string t = Podesavanja.Get("HR.ResenjeSablon");
            // podešavanja: {Firma.Naziv}, {HR.ResenjePravniOsnov} ... (nedostajuće prijavljuje Podesavanja.Get)
            t = Regex.Replace(t, @"\{([A-Za-z]+\.[A-Za-z0-9]+)\}", m => Podesavanja.Get(m.Groups[1].Value));

            int brojDana = HRBaza.CeoBroj(r["BrojDana"]);
            int iskoristio = Math.Max(0, HRBaza.CeoBroj(r["PotrosenoDana"]) - brojDana);
            var polja = new System.Collections.Generic.Dictionary<string, string>
            {
                ["Zaposleni"] = Convert.ToString(r["PrezimeIme"]),
                ["Prebivaliste"] = Convert.ToString(r["Prebivaliste"]),
                ["JMBG"] = Convert.ToString(r["JMBG"]).Trim(),
                ["RadnoMesto"] = Convert.ToString(r["RadnoMesto"]),
                ["Godina"] = Convert.ToString(r["Godina"]),
                ["BrojResenja"] = Convert.ToString(r["BrojResenja"]),
                ["Dana"] = brojDana.ToString(),
                ["DatumOd"] = D(r["DatumOd"]),
                ["DatumDo"] = D(r["DatumDo"]),
                ["DatumPovratka"] = D(r["DatumPovratka"]),
                ["DatumZahteva"] = D(r["DatumZahteva"]),
                ["DatumResenja"] = D(r["DatumResenja"] == DBNull.Value ? (object)DateTime.Today : r["DatumResenja"]),
                ["ExecutionTime"] = D(r["DatumResenja"] == DBNull.Value ? (object)DateTime.Today : r["DatumResenja"]),
                ["DanaGodisnjeg"] = Convert.ToString(r["UkupnoDana"]),
                ["DanaIskoristio"] = iskoristio.ToString()
            };
            return Regex.Replace(t, @"\[&?([A-Za-z]+)\]", m => polja.TryGetValue(m.Groups[1].Value, out string v) ? v : m.Value);
        }

        // Pravi PDF i upisuje putanju; ponovna štampa daje isti datum (DatumResenja je snimljen)
        public static string Napravi(int stavka)
        {
            DataRow r = Podaci(stavka);
            if (r == null)
                throw new InvalidOperationException("Zahtev ne postoji.");
            if (Convert.ToString(r["BrojResenja"]) == "")
                throw new InvalidOperationException("Zahtev još nije odobren; rešenje nema broj.");
            string tekst = Tekst(r);

            string folder = Path.Combine(Podesavanja.Get("Putanje.HRResenja"), Convert.ToString(r["Godina"]));
            Directory.CreateDirectory(folder);
            string ime = Convert.ToString(r["BrojResenja"]) + "_" + Convert.ToString(r["PrezimeIme"]);
            ime = string.Concat(ime.Select(c => Path.GetInvalidFileNameChars().Contains(c) ? '-' : c)).Replace(' ', '_');
            string putanja = Path.Combine(folder, ime + ".pdf");

            using (var doc = new PdfDocument())
            {
                doc.PageSettings.Margins.All = 50;
                PdfPage strana = doc.Pages.Add();
                var obican = new PdfTrueTypeFont(new Font("Arial", 10.5f), true);
                var naslov = new PdfTrueTypeFont(new Font("Arial", 12f, FontStyle.Bold), true);
                float sirina = strana.GetClientSize().Width;
                float y = 0;

                string logo = Podesavanja.Get("Firma.Logo", "");
                if (logo != "" && File.Exists(logo))
                {
                    try
                    {
                        var slika = new PdfBitmap(logo);
                        float visina = 40, sir = slika.Width * visina / slika.Height;
                        strana.Graphics.DrawImage(slika, 0, 0, sir, visina);
                        y = visina + 14;
                    }
                    catch
                    {
                        // logo je ukras; rešenje se pravi i bez njega
                    }
                }

                var format = new PdfLayoutFormat { Layout = PdfLayoutType.Paginate, Break = PdfLayoutBreakType.FitPage };
                foreach (string pasus in tekst.Replace("\r\n", "\n").Split('\n'))
                {
                    if (pasus.Trim() == "")
                    {
                        y += 8;
                        continue;
                    }
                    bool jeNaslov = pasus.StartsWith("^");
                    var element = new PdfTextElement(jeNaslov ? pasus.Substring(1).Trim() : pasus, jeNaslov ? naslov : obican, PdfBrushes.Black)
                    {
                        StringFormat = new PdfStringFormat { Alignment = jeNaslov ? PdfTextAlignment.Center : PdfTextAlignment.Justify, LineSpacing = 3 }
                    };
                    float visinaStrane = strana.GetClientSize().Height;
                    if (y > visinaStrane - 30)
                    {
                        strana = doc.Pages.Add();
                        y = 0;
                    }
                    PdfLayoutResult rez = element.Draw(strana, new RectangleF(0, y, sirina, visinaStrane - y), format);
                    strana = rez.Page;
                    y = rez.Bounds.Bottom + 4;
                }
                doc.Save(putanja);
                doc.Close(true);
            }

            HRBaza.Izvrsi("UPDATE dbo.GODopustStavke SET PutanjaResenja = @P, DatumIzmene = GETDATE() WHERE ID = @S",
                HRBaza.P("@P", putanja), HRBaza.P("@S", stavka));
            return putanja;
        }

        // Slanje mailom sa PDF-om; vraća null ili razlog zašto nije poslato
        public static string Posalji(int stavka)
        {
            DataRow r = Podaci(stavka);
            string email = Convert.ToString(r["email"]).Trim();
            if (email == "")
                return "Zaposleni nema email; rešenje treba odštampati.";
            string putanja = Convert.ToString(r["PutanjaResenja"]);
            if (putanja == "" || !File.Exists(putanja))
                putanja = Napravi(stavka);
            string firma = Podesavanja.Get("Firma.Naziv", "");
            HRMail.Posalji(email, "Rešenje o korišćenju godišnjeg odmora " + r["BrojResenja"],
                "Poštovani,\n\nu prilogu je rešenje o korišćenju godišnjeg odmora od " + D(r["DatumOd"]) + " do " + D(r["DatumDo"]) +
                " (" + r["BrojDana"] + " radnih dana).\n\n" + firma, putanja, true);
            HRBaza.Izvrsi("UPDATE dbo.GODopustStavke SET ResenjePoslatoMail = 1, DatumSlanjaMaila = GETDATE(), DatumIzmene = GETDATE() WHERE ID = @S",
                HRBaza.P("@S", stavka));
            return null;
        }

        public static void Stampaj(int stavka)
        {
            DataRow r = Podaci(stavka);
            string putanja = Convert.ToString(r["PutanjaResenja"]);
            if (putanja == "" || !File.Exists(putanja))
                putanja = Napravi(stavka);
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(putanja) { Verb = "print", UseShellExecute = true });
            }
            catch
            {
                System.Diagnostics.Process.Start(putanja);
            }
            HRBaza.Izvrsi("UPDATE dbo.GODopustStavke SET ResenjeStampano = 1, DatumIzmene = GETDATE() WHERE ID = @S", HRBaza.P("@S", stavka));
        }

        public static void Otvori(int stavka)
        {
            DataRow r = Podaci(stavka);
            string putanja = Convert.ToString(r["PutanjaResenja"]);
            if (putanja == "" || !File.Exists(putanja))
                putanja = Napravi(stavka);
            System.Diagnostics.Process.Start(putanja);
        }

        // Posle odobrenja: PDF, pa mail (ili štampa kad nema emaila). Neuspelo slanje ne poništava odobrenje.
        public static void PosleOdobrenja(IWin32Window vlasnik, int stavka)
        {
            try
            {
                Napravi(stavka);
            }
            catch (Exception ex)
            {
                MessageBox.Show(vlasnik, "Zahtev je odobren, ali rešenje nije napravljeno: " + ex.Message +
                    "\n\nPopravite podešavanje i upotrebite Rešenje ili Pošalji ponovo.", "Rešenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string razlog;
            try
            {
                razlog = Posalji(stavka);
            }
            catch (Exception ex)
            {
                MessageBox.Show(vlasnik, "Zahtev je odobren i rešenje je napravljeno, ali mail nije poslat: " + ex.Message +
                    "\n\nUpotrebite Pošalji ponovo.", "Rešenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (razlog != null &&
                MessageBox.Show(vlasnik, razlog + "\n\nOdštampati rešenje sada?", "Rešenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Stampaj(stavka);
        }
    }
}
