using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Saobracaj.HR
{
    // Jednostavan dokument za štampu (revers, karton zaposlenog, zahtev za nabavku): blokovi teksta i tabele,
    // tabela se prelama na sledeću stranu i ponavlja zaglavlje. Pregled pre štampe, A4.
    public class HRDokument
    {
        private abstract class Blok { }
        private class TekstBlok : Blok { public string Tekst; public Font Font; public bool Desno; public Color Boja = Color.Black; }
        private class RazmakBlok : Blok { public float Visina; }
        private class TabelaBlok : Blok { public string[] Naslovi; public float[] Sirine; public bool[] Desno; public List<string[]> Redovi = new List<string[]>(); public bool PoslednjiPodebljan; }
        private class PotpisiBlok : Blok { public string Levo, Desno; }

        private readonly List<Blok> blokovi = new List<Blok>();
        private readonly string naslovDokumenta;
        public string Pecat;   // npr. "KOPIJA": dijagonalno, preko prve strane

        private static readonly Font Obican = new Font("Segoe UI", 10f);
        private static readonly Font Mali = new Font("Segoe UI", 9f);
        private static readonly Font Podebljan = new Font("Segoe UI Semibold", 10f);
        public static readonly Font Naslov = new Font("Segoe UI Semibold", 16f);
        public static readonly Font Podnaslov = new Font("Segoe UI Semibold", 12f);

        // stanje štampe
        private int blok, red, strana;

        public HRDokument(string naslovDokumenta)
        {
            this.naslovDokumenta = naslovDokumenta;
        }

        public static string NazivFirme()
        {
            try
            {
                CompanyConfiguration c = ConfigManager.GetCompanyConfiguration(Sifarnici.frmLogovanje.company);
                if (c != null && !string.IsNullOrWhiteSpace(c.Name_Value))
                    return c.Name_Value.Trim();
            }
            catch
            {
            }
            return Sifarnici.frmLogovanje.company ?? "";
        }

        public HRDokument Tekst(string tekst, Font font = null, bool desno = false, Color? boja = null)
        {
            blokovi.Add(new TekstBlok { Tekst = tekst ?? "", Font = font ?? Obican, Desno = desno, Boja = boja ?? Color.Black });
            return this;
        }

        public HRDokument Razmak(float visina = 10)
        {
            blokovi.Add(new RazmakBlok { Visina = visina });
            return this;
        }

        // sirine: relativne (zbir ne mora biti 1); desno: kolone poravnate desno (brojevi)
        public HRDokument Tabela(string[] naslovi, float[] sirine, bool[] desno, IEnumerable<string[]> redovi, bool poslednjiPodebljan = false)
        {
            var t = new TabelaBlok { Naslovi = naslovi, Sirine = sirine, Desno = desno ?? new bool[naslovi.Length], PoslednjiPodebljan = poslednjiPodebljan };
            t.Redovi.AddRange(redovi);
            blokovi.Add(t);
            return this;
        }

        public HRDokument Potpisi(string levo, string desno)
        {
            blokovi.Add(new PotpisiBlok { Levo = levo, Desno = desno });
            return this;
        }

        public void Prikazi(IWin32Window vlasnik)
        {
            using (var pd = new PrintDocument())
            {
                pd.DocumentName = naslovDokumenta;
                pd.DefaultPageSettings.Margins = new Margins(60, 60, 60, 60);
                pd.BeginPrint += (s, e) => { blok = 0; red = 0; strana = 0; };
                pd.PrintPage += Strana;
                using (var pregled = new PrintPreviewDialog { Document = pd, Width = 1000, Height = 800, UseAntiAlias = true })
                    pregled.ShowDialog(vlasnik);
            }
        }

        private void Strana(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            RectangleF m = e.MarginBounds;
            float y = m.Top;
            strana++;

            if (strana == 1 && !string.IsNullOrEmpty(Pecat))
                CrtajPecat(g, m);

            while (blok < blokovi.Count)
            {
                Blok b = blokovi[blok];
                if (b is RazmakBlok rz)
                {
                    y += rz.Visina;
                }
                else if (b is TekstBlok tb)
                {
                    SizeF vel = g.MeasureString(tb.Tekst, tb.Font, (int)m.Width);
                    if (y + vel.Height > m.Bottom && y > m.Top)
                    {
                        e.HasMorePages = true;
                        return;
                    }
                    var format = new StringFormat { Alignment = tb.Desno ? StringAlignment.Far : StringAlignment.Near };
                    using (var cetka = new SolidBrush(tb.Boja))
                        g.DrawString(tb.Tekst, tb.Font, cetka, new RectangleF(m.Left, y, m.Width, vel.Height + 2), format);
                    y += vel.Height + 2;
                }
                else if (b is PotpisiBlok pb)
                {
                    if (y + 60 > m.Bottom)
                    {
                        e.HasMorePages = true;
                        return;
                    }
                    y += 30;
                    float pola = m.Width / 2;
                    g.DrawString(pb.Levo + " ______________________", Obican, Brushes.Black, m.Left, y);
                    g.DrawString(pb.Desno + " ______________________", Obican, Brushes.Black, m.Left + pola + 20, y);
                    y += 30;
                }
                else if (b is TabelaBlok t)
                {
                    if (!CrtajTabelu(g, m, t, ref y))
                    {
                        e.HasMorePages = true;
                        return;
                    }
                }
                blok++;
                red = 0;
            }

            using (var p = new Pen(Color.Gray))
                g.DrawString(naslovDokumenta + " · strana " + strana, Mali, Brushes.Gray, m.Left, m.Bottom + 15);
            e.HasMorePages = false;
        }

        // Vraća false kad tabela nije stala (nastavlja se na sledećoj strani od reda 'red')
        private bool CrtajTabelu(Graphics g, RectangleF m, TabelaBlok t, ref float y)
        {
            float ukupno = 0;
            foreach (float s in t.Sirine) ukupno += s;
            float[] x = new float[t.Sirine.Length + 1];
            x[0] = m.Left;
            for (int i = 0; i < t.Sirine.Length; i++)
                x[i + 1] = x[i] + m.Width * t.Sirine[i] / ukupno;

            const float visinaReda = 24;
            if (y + visinaReda * 2 > m.Bottom)
                return false;

            // zaglavlje
            g.FillRectangle(new SolidBrush(Color.FromArgb(234, 240, 246)), m.Left, y, m.Width, visinaReda);
            for (int i = 0; i < t.Naslovi.Length; i++)
                Celija(g, t.Naslovi[i], Podebljan, x[i], x[i + 1], y, visinaReda, t.Desno[i]);
            g.DrawRectangle(Pens.Gray, m.Left, y, m.Width, visinaReda);
            y += visinaReda;

            for (; red < t.Redovi.Count; red++)
            {
                string[] r = t.Redovi[red];
                float visina = visinaReda;
                for (int i = 0; i < r.Length && i < t.Naslovi.Length; i++)
                {
                    SizeF s = g.MeasureString(r[i] ?? "", Obican, (int)(x[i + 1] - x[i] - 8));
                    visina = Math.Max(visina, s.Height + 8);
                }
                if (y + visina > m.Bottom)
                    return false;
                Font f = t.PoslednjiPodebljan && red == t.Redovi.Count - 1 ? Podebljan : Obican;
                for (int i = 0; i < r.Length && i < t.Naslovi.Length; i++)
                    Celija(g, r[i], f, x[i], x[i + 1], y, visina, t.Desno[i]);
                g.DrawRectangle(Pens.Gray, m.Left, y, m.Width, visina);
                y += visina;
            }
            for (int i = 1; i < t.Sirine.Length; i++)
                g.DrawLine(Pens.LightGray, x[i], y - 1, x[i], y - 1);
            y += 6;
            return true;
        }

        private static void Celija(Graphics g, string tekst, Font f, float x1, float x2, float y, float visina, bool desno)
        {
            var format = new StringFormat { Alignment = desno ? StringAlignment.Far : StringAlignment.Near, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisWord };
            g.DrawString(tekst ?? "", f, Brushes.Black, new RectangleF(x1 + 4, y, x2 - x1 - 8, visina), format);
            g.DrawLine(Pens.Gray, x1, y, x1, y + visina);
        }

        private void CrtajPecat(Graphics g, RectangleF m)
        {
            GraphicsState stanje = g.Save();
            g.TranslateTransform(m.Right - 130, m.Top + 30);
            g.RotateTransform(-12);
            using (var f = new Font("Segoe UI Black", 22f))
            using (var olovka = new Pen(Color.FromArgb(200, 180, 83, 9), 3))
            using (var cetka = new SolidBrush(Color.FromArgb(200, 180, 83, 9)))
            {
                SizeF s = g.MeasureString(Pecat, f);
                g.DrawRectangle(olovka, -s.Width / 2 - 8, -s.Height / 2 - 2, s.Width + 16, s.Height + 4);
                g.DrawString(Pecat, f, cetka, -s.Width / 2, -s.Height / 2);
            }
            g.Restore(stanje);
        }
    }

    public static class HRStampa
    {
        // HTZ revers (7.4): firma, broj i datum, zaposleni (ime, Code, radno mesto), stavke, izjava, potpisi
        public static void Revers(IWin32Window vlasnik, int revers, bool kopija)
        {
            DataRow z = HRBaza.Red(@"
                SELECT r.BrojReversa, r.Datum, r.Storniran, t.PrezimeIme, t.Code, t.RadnoMestoNaziv,
                       LTRIM(RTRIM(ISNULL(i.Prezime, N'') + N' ' + ISNULL(i.Ime, N''))) AS Izdao
                FROM dbo.HTZReversi r
                JOIN dbo.vZaposleniTrenutno t ON t.Zaposleni = r.Zaposleni
                LEFT JOIN dbo.Zaposleni i ON i.ID = r.Izdao
                WHERE r.Sifra = @R", HRBaza.P("@R", revers));
            if (z == null)
                return;
            DataTable stavke = HRBaza.Tabela(@"
                SELECT o.Naziv, o.JedinicaMere, s.Kolicina, s.Velicina, s.RokZamene
                FROM dbo.ZaposleniHTZZaduzenja s JOIN dbo.HTZOprema o ON o.Sifra = s.HTZOprema
                WHERE s.Revers = @R ORDER BY s.Sifra", HRBaza.P("@R", revers));

            var redovi = new List<string[]>();
            int rb = 0;
            foreach (DataRow s in stavke.Rows)
                redovi.Add(new[] { (++rb).ToString(), Convert.ToString(s["Naziv"]), Convert.ToString(s["JedinicaMere"]),
                                   HRBaza.Decimalni(s["Kolicina"]).ToString("0.##"), Convert.ToString(s["Velicina"]), HRBaza.DatumTekst(s["RokZamene"]) });

            string radnoMesto = Convert.ToString(z["RadnoMestoNaziv"]);
            var dok = new HRDokument("Revers " + z["BrojReversa"])
            {
                Pecat = Convert.ToBoolean(z["Storniran"]) ? "STORNIRAN" : (kopija ? "KOPIJA" : null)
            };
            dok.Tekst(HRDokument.NazivFirme(), null, false, Color.DimGray)
               .Razmak(16)
               .Tekst("REVERS br. " + z["BrojReversa"], HRDokument.Naslov)
               .Tekst("Datum: " + HRBaza.DatumTekst(z["Datum"]) + ".")
               .Tekst(z["PrezimeIme"] + " · " + z["Code"] + (radnoMesto != "" ? " · " + radnoMesto : ""))
               .Razmak(12)
               .Tabela(new[] { "R.br.", "Oprema", "JM", "Kol.", "Vel.", "Rok zamene" },
                       new[] { 0.7f, 4f, 0.9f, 0.9f, 0.9f, 1.6f },
                       new[] { true, false, false, true, false, false }, redovi)
               .Razmak(14)
               .Tekst("Potvrđujem da sam primio navedenu zaštitnu opremu, da ću je koristiti u skladu sa namenom i uputstvom, " +
                      "čuvati je od oštećenja i gubitka i vratiti je pri zameni ili prestanku radnog odnosa.")
               .Potpisi("Izdao: " + z["Izdao"], "Primio:");
            dok.Prikazi(vlasnik);
        }

        // Karton zaposlenog: osnovni podaci, trenutno radno mesto, istorija radnih mesta, dokumenti
        public static void Karton(IWin32Window vlasnik, int zaposleni)
        {
            DataRow t = HRBaza.Red("SELECT * FROM dbo.vZaposleniTrenutno WHERE Zaposleni = @Z", HRBaza.P("@Z", zaposleni));
            DataRow z = HRBaza.Red("SELECT * FROM dbo.Zaposleni WHERE ID = @Z", HRBaza.P("@Z", zaposleni));
            if (t == null || z == null)
                return;
            int staz = HRBaza.CeoBroj(t["StazMeseci"]);

            var dok = new HRDokument("Karton zaposlenog " + t["PrezimeIme"]);
            dok.Tekst(HRDokument.NazivFirme(), null, false, Color.DimGray)
               .Razmak(10)
               .Tekst("KARTON ZAPOSLENOG", HRDokument.Naslov)
               .Tekst(t["PrezimeIme"] + " · " + t["Code"], HRDokument.Podnaslov)
               .Razmak(8)
               .Tabela(new[] { "Podatak", "Vrednost", "Podatak", "Vrednost" }, new[] { 1.4f, 2f, 1.4f, 2f }, null, new[]
               {
                   new[] { "JMBG", Convert.ToString(z["JMBG"]), "Datum rođenja", HRBaza.DatumTekst(z["DatumRodjenja"]) },
                   new[] { "Ime oca", Convert.ToString(z["ImeOca"]), "Mesto rođenja", Convert.ToString(z["MestoRodjenja"]) },
                   new[] { "Telefon", Convert.ToString(z["Telefon"]), "Email", Convert.ToString(z["email"]) },
                   new[] { "Stalna adresa", Convert.ToString(z["AdresaStalnoPreb"]), "Šifra ERP", Convert.ToString(z["SifraERP"]) },
                   new[] { "Datum ulaska", HRBaza.DatumTekst(z["DatumUlaska"]), "Datum izlaska", HRBaza.DatumTekst(z["DatumIzlaska"]) },
                   new[] { "Radno mesto", Convert.ToString(t["RadnoMestoNaziv"]), "Org. jedinica", Convert.ToString(t["OrganizacionaJedinicaNaziv"]) },
                   new[] { "Matično mesto", Convert.ToString(t["SluzbenoMestoNaziv"]), "Mentor", Convert.ToString(t["MentorNaziv"]) },
                   new[] { "Obrazovanje", Convert.ToString(t["StepenObrazovanjaNaziv"]), "Staž", (staz / 12) + " g " + (staz % 12) + " m" }
               })
               .Razmak(12)
               .Tekst("Radna mesta", HRDokument.Podnaslov);

            var rm = new List<string[]>();
            foreach (DataRow r in HRBaza.Tabela(@"
                SELECT r.Naziv, o.Naziv AS Odnos, j.Naziv AS Jedinica, rm.DatumOd, rm.DatumDo
                FROM dbo.ZaposleniRadnaMesta rm
                JOIN dbo.RadnaMesta r ON r.Sifra = rm.RadnoMesto
                JOIN dbo.RadniOdnosi o ON o.Sifra = rm.VrstaRadnogOdnosa
                LEFT JOIN dbo.OrganizacioneJedinice j ON j.ID = rm.OrganizacionaJedinica
                WHERE rm.Zaposleni = @Z ORDER BY rm.DatumOd DESC", HRBaza.P("@Z", zaposleni)).Rows)
                rm.Add(new[] { Convert.ToString(r["Naziv"]), Convert.ToString(r["Odnos"]), Convert.ToString(r["Jedinica"]), HRBaza.DatumTekst(r["DatumOd"]), HRBaza.DatumTekst(r["DatumDo"]) });
            dok.Tabela(new[] { "Radno mesto", "Radni odnos", "Org. jedinica", "Od", "Do" }, new[] { 2.4f, 1.6f, 2f, 1f, 1f }, null, rm)
               .Razmak(12)
               .Tekst("Dokumenti", HRDokument.Podnaslov);

            var dokumenti = new List<string[]>();
            foreach (DataRow r in HRBaza.Tabela(@"
                SELECT v.Naziv, d.BrojDokumenta, d.Kategorije, d.DatumIzdavanja, d.VaziDo
                FROM dbo.ZaposleniDokumenti d JOIN dbo.VrsteDokumenta v ON v.Sifra = d.VrstaDokumenta
                WHERE d.Zaposleni = @Z ORDER BY v.Naziv", HRBaza.P("@Z", zaposleni)).Rows)
                dokumenti.Add(new[] { Convert.ToString(r["Naziv"]), Convert.ToString(r["BrojDokumenta"]), Convert.ToString(r["Kategorije"]), HRBaza.DatumTekst(r["DatumIzdavanja"]), HRBaza.DatumTekst(r["VaziDo"]) });
            dok.Tabela(new[] { "Vrsta", "Broj", "Kat.", "Izdat", "Važi do" }, new[] { 2.4f, 1.6f, 0.8f, 1f, 1f }, null, dokumenti);
            dok.Prikazi(vlasnik);
        }

        // Zahtev za nabavku (7.5)
        public static void Zahtev(IWin32Window vlasnik, int zahtev)
        {
            DataRow z = HRBaza.Red(@"
                SELECT z.*, LTRIM(RTRIM(ISNULL(p.Prezime, N'') + N' ' + ISNULL(p.Ime, N''))) AS PodnosilacNaziv, j.Naziv AS JedinicaNaziv,
                       v.Naziv AS VrstaNaziv, pr.Naziv AS PrioritetNaziv, s.Naziv AS StatusNaziv
                FROM dbo.ZahteviZaNabavku z
                JOIN dbo.Zaposleni p ON p.ID = z.Podnosilac
                LEFT JOIN dbo.OrganizacioneJedinice j ON j.ID = z.OrganizacionaJedinica
                JOIN dbo.VrsteNabavke v ON v.Sifra = z.VrstaNabavke
                JOIN dbo.PrioritetiNabavke pr ON pr.Sifra = z.Prioritet
                JOIN dbo.StatusiZahtevaZaNabavku s ON s.Sifra = z.Status
                WHERE z.Sifra = @Z", HRBaza.P("@Z", zahtev));
            if (z == null)
                return;

            var stavke = new List<string[]>();
            decimal ukupno = 0;
            foreach (DataRow s in HRBaza.Tabela("SELECT * FROM dbo.ZahtevZaNabavkuStavke WHERE Zahtev = @Z ORDER BY RedniBroj", HRBaza.P("@Z", zahtev)).Rows)
            {
                decimal vrednost = HRBaza.Decimalni(s["ProcenjenaVrednost"]);
                ukupno += vrednost;
                stavke.Add(new[] { Convert.ToString(s["RedniBroj"]), Convert.ToString(s["Opis"]), Convert.ToString(s["JedinicaMere"]),
                                   HRBaza.Decimalni(s["Kolicina"]).ToString("0.###"),
                                   s["ProcenjenaCena"] == DBNull.Value ? "" : HRBaza.Iznos(HRBaza.Decimalni(s["ProcenjenaCena"])),
                                   HRBaza.Iznos(vrednost) });
            }
            stavke.Add(new[] { "", "Ukupno (" + z["Valuta"] + ")", "", "", "", HRBaza.Iznos(ukupno) });

            var dok = new HRDokument("Zahtev " + z["BrojZahteva"]);
            dok.Tekst(HRDokument.NazivFirme(), null, false, Color.DimGray)
               .Razmak(10)
               .Tekst("ZAHTEV ZA NABAVKU " + z["BrojZahteva"], HRDokument.Naslov)
               .Tekst("Datum: " + HRBaza.DatumTekst(z["Datum"]) + " · Status: " + z["StatusNaziv"])
               .Razmak(8)
               .Tabela(new[] { "Podatak", "Vrednost", "Podatak", "Vrednost" }, new[] { 1.3f, 2.2f, 1.3f, 2.2f }, null, new[]
               {
                   new[] { "Podnosilac", Convert.ToString(z["PodnosilacNaziv"]), "Org. jedinica", Convert.ToString(z["JedinicaNaziv"]) },
                   new[] { "Vrsta nabavke", Convert.ToString(z["VrstaNaziv"]), "Prioritet", Convert.ToString(z["PrioritetNaziv"]) },
                   new[] { "Sredstvo", Convert.ToString(z["SredstvoOpis"]), "Radni nalog", Convert.ToString(z["RadniNalogOpis"]) },
                   new[] { "Potrebno do", HRBaza.DatumTekst(z["PotrebnoDo"]), "Isporuka", Convert.ToString(z["MestoIsporuke"]) }
               })
               .Tekst("Svrha: " + z["Svrha"])
               .Razmak(10)
               .Tabela(new[] { "#", "Opis", "JM", "Kol.", "Cena", "Vrednost" }, new[] { 0.5f, 4f, 0.8f, 1f, 1.3f, 1.4f },
                       new[] { true, false, false, true, true, true }, stavke, true)
               .Razmak(10);

            DataRow ponuda = HRBaza.Red(@"
                SELECT p.*, RTRIM(pa.PaNaziv) AS PartnerNaziv FROM dbo.ZahtevZaNabavkuPonude p
                LEFT JOIN dbo.Partnerji pa ON pa.PaSifra = p.Partner
                WHERE p.Zahtev = @Z AND p.Izabrana = 1", HRBaza.P("@Z", zahtev));
            if (ponuda != null)
                dok.Tekst("Izabrana ponuda: " + ponuda["PartnerNaziv"] + ", br. " + ponuda["BrojPonude"] + ", " +
                          HRBaza.Iznos(HRBaza.Decimalni(ponuda["Iznos"])) + " " + ponuda["Valuta"] + " bez PDV-a" +
                          (ponuda["RazlogIzbora"] == DBNull.Value ? "" : " (obrazloženje: " + ponuda["RazlogIzbora"] + ")"))
                   .Razmak(10);

            var odobrenja = new List<string[]>();
            foreach (DataRow o in HRBaza.Tabela(@"
                SELECT o.Nivo, LTRIM(RTRIM(ISNULL(z.Prezime, N'') + N' ' + ISNULL(z.Ime, N''))) AS Odobravalac, o.Odluka, o.DatumOdluke, o.Komentar
                FROM dbo.ZahtevZaNabavkuOdobrenja o JOIN dbo.Zaposleni z ON z.ID = o.Odobravalac
                WHERE o.Zahtev = @Z AND o.Odluka IS NOT NULL ORDER BY o.Sifra", HRBaza.P("@Z", zahtev)).Rows)
                odobrenja.Add(new[] { Convert.ToString(o["Nivo"]), Convert.ToString(o["Odobravalac"]), OpisOdluke(Convert.ToString(o["Odluka"])),
                                      o["DatumOdluke"] == DBNull.Value ? "" : Convert.ToDateTime(o["DatumOdluke"]).ToString("dd.MM.yyyy HH:mm"), Convert.ToString(o["Komentar"]) });
            dok.Tekst("Istorija odobravanja", HRDokument.Podnaslov)
               .Tabela(new[] { "Nivo", "Odobravalac", "Odluka", "Datum", "Komentar" }, new[] { 0.6f, 2f, 1.2f, 1.5f, 2.7f }, null, odobrenja);
            dok.Prikazi(vlasnik);
        }

        public static string OpisOdluke(string odluka)
        {
            switch (odluka)
            {
                case "O": return "Odobreno";
                case "V": return "Vraćeno na doradu";
                case "D": return "Odbijeno";
                default: return "Čeka odluku";
            }
        }
    }
}
