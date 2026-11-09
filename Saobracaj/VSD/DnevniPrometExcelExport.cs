using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;

namespace Saobracaj.VSD
{
    // Grupa kolone komercijaliste na listu "Dnevni promet"
    public enum GrupaKomercijaliste
    {
        RM,
        VIP,
        ONLINE
    }

    // Jedna kolona komercijaliste na listu
    public class KolonaKomercijaliste
    {
        public string Naslov;
        public string Kljuc;    // normalizovano ime komercijaliste (vidi DnevniPrometExcelExport.Normalizuj)
        public GrupaKomercijaliste Grupa;
        public decimal Plan;
    }

    // Podaci za izvoz: plan, kolone komercijalista i prodaja po danu
    public class DnevniPrometModel
    {
        public string NazivPlana;
        public int Godina;
        public int Mesec;
        public int UkupnoDana;
        public int TekuceDana;
        public DateTime DoDatuma;
        public List<KolonaKomercijaliste> Kolone = new List<KolonaKomercijaliste>();
        public Dictionary<DateTime, Dictionary<string, decimal>> Prodaja = new Dictionary<DateTime, Dictionary<string, decimal>>();
    }

    // Izvoz lista "Dnevni promet" u .xlsx, u istom rasporedu kao Excel koji klijent vodi ručno
    public class DnevniPrometExcelExport
    {
        // Redosled i naslovi kolona su fiksni (kao u fajlu klijenta). Komercijalista koji nije ovde ide u RM posle Srećka.
        public static readonly (string Naslov, string Komercijalista, GrupaKomercijaliste Grupa)[] MapaKolona =
        {
            ("Aca", "ALEKSANDAR.PETROVIC", GrupaKomercijaliste.RM),
            ("Vlada", "IVAN.VRDOLJAK", GrupaKomercijaliste.RM),
            ("Đole", "DJORDJE.LOZANAC", GrupaKomercijaliste.RM),
            ("Čombe", "DRAGAN.BOGICEVIC", GrupaKomercijaliste.RM),
            ("Dragan", "DRAGAN.LILIC", GrupaKomercijaliste.RM),
            ("Enisa", "ENISA.PASANOVIC", GrupaKomercijaliste.RM),
            ("Ivan", "IVAN.LAPCEVIC", GrupaKomercijaliste.RM),
            ("Miljko", "MILJKO.RADONJIC", GrupaKomercijaliste.RM),
            ("Reni", "RENALDO.TOMIC", GrupaKomercijaliste.RM),
            ("Srećko", "SRECKO.MANIC", GrupaKomercijaliste.RM),
            ("VIP", "ALEKSANDAR.JOVIC", GrupaKomercijaliste.VIP),
            ("ONLINE prodaja", "TIM", GrupaKomercijaliste.ONLINE),
        };

        public const string NazivLista = "Dnevni promet";

        private static readonly string[] MeseciSkraceno = { "jan", "feb", "mar", "apr", "maj", "jun", "jul", "avg", "sep", "okt", "nov", "dec" };
        private static readonly string[] MeseciNaziv = { "JANUAR", "FEBRUAR", "MART", "APRIL", "MAJ", "JUN", "JUL", "AVGUST", "SEPTEMBAR", "OKTOBAR", "NOVEMBAR", "DECEMBAR" };
        private static readonly string[] DaniSkraceno = { "ned", "pon", "uto", "sre", "čet", "pet", "sub" };   // po DayOfWeek

        private static readonly Color Breskva = ColorTranslator.FromHtml("#FCE4D6");
        private static readonly Color Zuta = ColorTranslator.FromHtml("#FFFF00");
        private static readonly Color Siva = ColorTranslator.FromHtml("#A6A6A6");
        private static readonly Color Crvena = ColorTranslator.FromHtml("#FF0000");
        private static readonly Color Zelena = ColorTranslator.FromHtml("#00B050");
        private static readonly Color Plava = ColorTranslator.FromHtml("#0070C0");

        private const string FormatIznos = "#,##0";
        private const string FormatProcenat = "0%";

        // Posle izvoza: da li je za izabrane filtere bilo prometa
        public bool ImaPrometa { get; private set; }

        private readonly string connectionString;

        public DnevniPrometExcelExport(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Export(int planId, string brend /* null = svi */, DateTime doDatuma, string putanja)
        {
            if (string.IsNullOrWhiteSpace(brend))
                brend = null;   // svi brendovi

            DnevniPrometModel model = Ucitaj(planId, brend, doDatuma.Date);
            ImaPrometa = model.Prodaja.Count > 0;
            Snimi(model, putanja);
        }

        // ---------------------------------------------------------------------------------
        // Podaci
        // ---------------------------------------------------------------------------------
        // Ime komercijaliste za poređenje: bez razmaka sa krajeva, velika slova, razmak kao tačka
        public static string Normalizuj(string ime)
        {
            string s = (ime ?? "").Trim().ToUpperInvariant();
            return string.Join(".", s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
        }

        // Mesec plana: broj (6) ili srpski naziv (MART); 0 ako ne može da se odredi
        public static int MesecIzPlana(string mesec)
        {
            string s = (mesec ?? "").Trim();
            int broj;
            if (int.TryParse(s, out broj) && broj >= 1 && broj <= 12)
                return broj;

            int indeks = Array.FindIndex(MeseciNaziv, m => string.Equals(m, s, StringComparison.OrdinalIgnoreCase));
            return indeks >= 0 ? indeks + 1 : 0;
        }

        // Godina i mesec plana (za proveru da je izabrani datum u mesecu plana); null ako mesec ne može da se odredi
        public Tuple<int, int> MesecPlana(int planId)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("SELECT Godina, Mesec FROM dbo.[Plan] WHERE ID = @PlanID", conn))
            {
                cmd.Parameters.AddWithValue("@PlanID", planId);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read())
                        return null;
                    int godina;
                    int mesec = MesecIzPlana(Convert.ToString(rd["Mesec"]));
                    if (!int.TryParse(Convert.ToString(rd["Godina"]).Trim(), out godina) || mesec == 0)
                        return null;
                    return Tuple.Create(godina, mesec);
                }
            }
        }

        public DnevniPrometModel Ucitaj(int planId, string brend, DateTime doDatuma)
        {
            var model = new DnevniPrometModel { DoDatuma = doDatuma.Date };
            var planPoKljucu = new Dictionary<string, decimal>();
            var imenaVanMape = new Dictionary<string, string>();   // kljuc -> ime iz baze (za naslov)

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // 1. zaglavlje plana
                using (var cmd = new SqlCommand("SELECT ID, Godina, Mesec, UkupnoDana, TekuceDana, Naziv FROM dbo.[Plan] WHERE ID = @PlanID", conn))
                {
                    cmd.Parameters.AddWithValue("@PlanID", planId);
                    using (var rd = cmd.ExecuteReader())
                    {
                        if (!rd.Read())
                            throw new InvalidOperationException("Plan nije pronađen.");

                        model.NazivPlana = Convert.ToString(rd["Naziv"]).Trim();
                        int godina;
                        model.Godina = int.TryParse(Convert.ToString(rd["Godina"]).Trim(), out godina) ? godina : doDatuma.Year;
                        int mesec = MesecIzPlana(Convert.ToString(rd["Mesec"]));
                        model.Mesec = mesec > 0 ? mesec : doDatuma.Month;
                        model.UkupnoDana = rd["UkupnoDana"] == DBNull.Value ? 0 : Convert.ToInt32(rd["UkupnoDana"]);
                    }
                }

                // 2. plan po komercijalisti (za ceo mesec, nije po brendu)
                using (var cmd = new SqlCommand("SELECT Komercijalista, SUM(PlaniranaVrednost) AS PlanVrednost FROM dbo.PlanStavke WHERE PlanID = @PlanID GROUP BY Komercijalista", conn))
                {
                    cmd.Parameters.AddWithValue("@PlanID", planId);
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            string ime = Convert.ToString(rd["Komercijalista"]);
                            string kljuc = Normalizuj(ime);
                            if (kljuc.Length == 0)
                                continue;   // red bez imena komercijaliste se ne prikazuje
                            decimal plan = rd["PlanVrednost"] == DBNull.Value ? 0 : Convert.ToDecimal(rd["PlanVrednost"]);
                            planPoKljucu[kljuc] = (planPoKljucu.ContainsKey(kljuc) ? planPoKljucu[kljuc] : 0) + plan;
                            if (!imenaVanMape.ContainsKey(kljuc))
                                imenaVanMape[kljuc] = ime.Trim();
                        }
                    }
                }

                // 3. prodaja po danu i komercijalisti
                const string sqlProdaja =
                    "SELECT CAST(Datum AS date) AS Dan, Komercijalista, SUM(PVrednost) AS Prodaja " +
                    "FROM dbo.DnevniERP " +
                    "WHERE PlanID = @PlanID AND Datum < DATEADD(day, 1, @DoDatuma) AND (@Brend IS NULL OR Brend = @Brend) " +
                    "GROUP BY CAST(Datum AS date), Komercijalista";
                using (var cmd = new SqlCommand(sqlProdaja, conn))
                {
                    cmd.Parameters.AddWithValue("@PlanID", planId);
                    cmd.Parameters.Add("@DoDatuma", System.Data.SqlDbType.Date).Value = doDatuma.Date;
                    cmd.Parameters.Add("@Brend", System.Data.SqlDbType.NVarChar, 50).Value = (object)brend ?? DBNull.Value;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            DateTime dan = Convert.ToDateTime(rd["Dan"]).Date;
                            string ime = Convert.ToString(rd["Komercijalista"]);
                            string kljuc = Normalizuj(ime);
                            if (kljuc.Length == 0)
                                continue;   // red bez imena komercijaliste se ne prikazuje
                            decimal prodaja = rd["Prodaja"] == DBNull.Value ? 0 : Convert.ToDecimal(rd["Prodaja"]);

                            Dictionary<string, decimal> poDanu;
                            if (!model.Prodaja.TryGetValue(dan, out poDanu))
                                model.Prodaja[dan] = poDanu = new Dictionary<string, decimal>();
                            poDanu[kljuc] = (poDanu.ContainsKey(kljuc) ? poDanu[kljuc] : 0) + prodaja;

                            if (!imenaVanMape.ContainsKey(kljuc))
                                imenaVanMape[kljuc] = ime.Trim();
                        }
                    }
                }

                // 4. tekuće dana = broj uvezenih dana do izabranog datuma (bez filtera brenda)
                using (var cmd = new SqlCommand("SELECT COUNT(DISTINCT CAST(Datum AS date)) FROM dbo.DnevniERP WHERE PlanID = @PlanID AND Datum < DATEADD(day, 1, @DoDatuma)", conn))
                {
                    cmd.Parameters.AddWithValue("@PlanID", planId);
                    cmd.Parameters.Add("@DoDatuma", System.Data.SqlDbType.Date).Value = doDatuma.Date;
                    model.TekuceDana = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            // kolone: RM iz mape, RM van mape (posle Srećka), VIP, ONLINE
            var kljuceviIzMape = new HashSet<string>(MapaKolona.Select(m => Normalizuj(m.Komercijalista)));
            Func<string, decimal> planZa = k => planPoKljucu.ContainsKey(k) ? planPoKljucu[k] : 0;

            foreach (var m in MapaKolona.Where(m => m.Grupa == GrupaKomercijaliste.RM))
                model.Kolone.Add(new KolonaKomercijaliste { Naslov = m.Naslov, Kljuc = Normalizuj(m.Komercijalista), Grupa = m.Grupa, Plan = planZa(Normalizuj(m.Komercijalista)) });

            foreach (var par in imenaVanMape.Where(p => !kljuceviIzMape.Contains(p.Key)).OrderBy(p => p.Key, StringComparer.Ordinal))
            {
                string naslov = par.Value;
                model.Kolone.Add(new KolonaKomercijaliste { Naslov = naslov, Kljuc = par.Key, Grupa = GrupaKomercijaliste.RM, Plan = planZa(par.Key) });
            }

            foreach (var m in MapaKolona.Where(m => m.Grupa != GrupaKomercijaliste.RM))
                model.Kolone.Add(new KolonaKomercijaliste { Naslov = m.Naslov, Kljuc = Normalizuj(m.Komercijalista), Grupa = m.Grupa, Plan = planZa(Normalizuj(m.Komercijalista)) });

            return model;
        }

        // ---------------------------------------------------------------------------------
        // Excel
        // ---------------------------------------------------------------------------------
        private static string Kol(int kolona)
        {
            string naziv = "";
            while (kolona > 0)
            {
                kolona--;
                naziv = (char)('A' + kolona % 26) + naziv;
                kolona /= 26;
            }
            return naziv;
        }

        private static string Adr(int kolona, int red)
        {
            return Kol(kolona) + red;
        }

        private static string BezGreske(string izraz)
        {
            return "=IFERROR(" + izraz + ",\"\")";
        }

        // XlsIO čita brojeve u formulama po regionalnim podešavanjima (sa srpskim "0.8" nije broj, a "4/5" je datum),
        // pa se fajl piše sa neutralnim podešavanjima i posle se vraćaju korisnikova
        public void Snimi(DnevniPrometModel model, string putanja)
        {
            CultureInfo kultura = System.Threading.Thread.CurrentThread.CurrentCulture;
            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                SnimiList(model, putanja);
            }
            finally
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = kultura;
            }
        }

        private void SnimiList(DnevniPrometModel model, string putanja)
        {
            List<KolonaKomercijaliste> rm = model.Kolone.Where(k => k.Grupa == GrupaKomercijaliste.RM).ToList();
            KolonaKomercijaliste vip = model.Kolone.First(k => k.Grupa == GrupaKomercijaliste.VIP);
            KolonaKomercijaliste onl = model.Kolone.First(k => k.Grupa == GrupaKomercijaliste.ONLINE);

            // kolone: A, B, C, RM (D...), N, VIP, ONLINE, Q
            const int kA = 1, kB = 2, kC = 3, kR1 = 4;
            int kRn = kR1 + rm.Count - 1;
            int kN = kRn + 1, kVip = kN + 1, kOnl = kN + 2, kQ = kN + 3;

            var kolonaZa = new Dictionary<KolonaKomercijaliste, int>();
            for (int i = 0; i < rm.Count; i++)
                kolonaZa[rm[i]] = kR1 + i;
            kolonaZa[vip] = kVip;
            kolonaZa[onl] = kOnl;
            List<int> sveKolone = kolonaZa.Values.OrderBy(k => k).ToList();   // R1..Rn, VIP, ONL

            int brojDana = DateTime.DaysInMonth(model.Godina, model.Mesec);
            int E = 4 + brojDana, T = E + 1;
            string rR = Kol(kR1), rRn = Kol(kRn);

            using (ExcelEngine engine = new ExcelEngine())
            {
                IApplication app = engine.Excel;
                app.DefaultVersion = ExcelVersion.Xlsx;
                IWorkbook wb = app.Workbooks.Create(1);
                IWorksheet ws = wb.Worksheets[0];
                ws.Name = NazivLista;

                // --- red 1: naslovi
                ws.Range[1, kC].Text = MeseciSkraceno[model.Mesec - 1] + "." + (model.Godina % 100).ToString("00");
                foreach (var k in model.Kolone)
                    ws.Range[1, kolonaZa[k]].Text = k.Naslov;
                ws.Range[1, kN].Text = "broj radnih dana";
                ws.Range[1, kQ].Text = "ukupan broj radnih dana";
                IRange red1 = ws.Range[1, kA, 1, kQ];
                red1.CellStyle.Font.Bold = true;
                red1.CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                red1.CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
                red1.CellStyle.WrapText = true;
                ws.Range[1, kC].CellStyle.Color = Breskva;
                ws.Range[1, kN].CellStyle.Font.RGBColor = Plava;
                ws.Range[1, kQ].CellStyle.Font.RGBColor = Plava;
                ws.SetRowHeight(1, 30);

                // --- red 2: preneseno
                ws.Range[2, kA].Text = "preneseno";
                ws.Range[2, kC].Number = 0;

                // --- red 3: mesečni plan
                ws.Range[3, kA].Text = "mesečni plan";
                ws.Range[3, kC].Formula = "=SUM(" + rR + "3:" + rRn + "3)";
                foreach (var k in model.Kolone)
                    ws.Range[3, kolonaZa[k]].Number = (double)k.Plan;
                ws.Range[3, kQ].Number = model.UkupnoDana;
                IRange red3 = ws.Range[3, kA, 3, kQ];
                red3.CellStyle.Font.Bold = true;
                red3.CellStyle.Font.RGBColor = Crvena;
                red3.CellStyle.Color = Breskva;

                // --- red 4: dnevni plan
                ws.Range[4, kA].Text = "dnevni plan";
                ws.Range[4, kC].Formula = "=SUM(" + rR + "4:" + rRn + "4)";
                foreach (int k in sveKolone)
                    ws.Range[4, k].Formula = BezGreske(Adr(k, 3) + "/$" + Kol(kQ) + "$3");
                ws.Range[4, kN].Number = model.TekuceDana;
                IRange red4 = ws.Range[4, kA, 4, kQ];
                red4.CellStyle.Font.Bold = true;
                red4.CellStyle.Font.RGBColor = Zelena;

                // --- redovi 5..E: dani u mesecu
                for (int dan = 1; dan <= brojDana; dan++)
                {
                    int r = 4 + dan;
                    var datum = new DateTime(model.Godina, model.Mesec, dan);
                    ws.Range[r, kA].Text = dan + "." + MeseciSkraceno[model.Mesec - 1];
                    ws.Range[r, kB].Text = DaniSkraceno[(int)datum.DayOfWeek];

                    if (datum.DayOfWeek == DayOfWeek.Saturday || datum.DayOfWeek == DayOfWeek.Sunday)
                    {
                        ws.Range[r, kA, r, kQ].CellStyle.Color = Breskva;
                        ws.Range[r, kB].CellStyle.Font.RGBColor = Crvena;
                        continue;
                    }

                    ws.Range[r, kC].Formula = "=SUM(" + rR + r + ":" + rRn + r + ")";

                    Dictionary<string, decimal> poDanu;
                    if (datum > model.DoDatuma || !model.Prodaja.TryGetValue(datum, out poDanu))
                        continue;   // posle izabranog datuma ili bez uvoza: ćelije komercijalista prazne

                    foreach (var k in model.Kolone)
                    {
                        decimal prodaja;
                        if (poDanu.TryGetValue(k.Kljuc, out prodaja))
                            ws.Range[r, kolonaZa[k]].Number = (double)prodaja;
                    }
                }

                // --- red T: zbir
                ws.Range[T, kC].Formula = "=SUM(C5:C" + E + ")";
                foreach (int k in sveKolone)
                    ws.Range[T, k].Formula = "=SUM(" + Adr(k, 5) + ":" + Adr(k, E) + ")";
                ws.Range[T, kA, T, kQ].CellStyle.Font.Bold = true;
                ws.Range[T, kC].CellStyle.Font.Underline = ExcelUnderline.Single;

                // --- T+1: % ostvarenja (C) i prosek po danu
                string n4 = "$" + Kol(kN) + "$4", q3 = "$" + Kol(kQ) + "$3";
                ws.Range[T + 1, kC].Formula = BezGreske("C" + T + "/C3");
                foreach (int k in sveKolone)
                    ws.Range[T + 1, k].Formula = BezGreske(Adr(k, T) + "/" + n4);
                ws.Range[T + 1, kA, T + 1, kQ].CellStyle.Color = Breskva;

                // --- T+2: % ispunjenosti
                foreach (int k in sveKolone)
                    ws.Range[T + 2, k].Formula = BezGreske(Adr(k, T) + "/" + Adr(k, 3));

                // --- T+3: Plan RM (preostalo)
                ws.Range[T + 3, kA].Text = "Plan RM";
                ws.Range[T + 3, kC].Formula = "=C3";
                foreach (int k in sveKolone)
                    ws.Range[T + 3, k].Formula = "=" + Adr(k, 3) + "-" + Adr(k, T);
                ws.Range[T + 3, kA, T + 3, kQ].CellStyle.Color = Breskva;

                // --- T+5, T+6: projekcija
                ws.Range[T + 5, kA].Text = "Projekcija cifra";
                ws.Range[T + 5, kC].Formula = BezGreske("C" + T + "/" + n4 + "*" + q3);
                ws.Range[T + 6, kA].Text = "Projekcija %";
                ws.Range[T + 6, kC].Formula = BezGreske("C" + (T + 5) + "/C" + (T + 3));
                foreach (int k in sveKolone)
                    ws.Range[T + 6, k].Formula = BezGreske(Adr(k, T + 1) + "*" + q3 + "/" + Adr(k, 3));

                // --- T+8..T+10: Plan RM+VIP
                string kv = Kol(kVip);
                ws.Range[T + 8, kA].Text = "Plan RM+VIP";
                ws.Range[T + 8, kC].Formula = "=C3+" + kv + "3";
                ws.Range[T + 9, kC].Formula = "=C" + T + "+" + kv + T;
                ws.Range[T + 10, kC].Formula = BezGreske("C" + (T + 9) + "/C" + (T + 8));
                ws.Range[T + 8, kA].CellStyle.Font.Bold = true;
                ws.Range[T + 6, kA].CellStyle.Font.Bold = true;

                // --- formati brojeva
                ws.Range[2, kC, T + 10, kQ].NumberFormat = FormatIznos;
                ws.Range[T + 1, kC].NumberFormat = FormatProcenat;
                ws.Range[T + 6, kC].NumberFormat = FormatProcenat;
                ws.Range[T + 10, kC].NumberFormat = FormatProcenat;
                foreach (int k in sveKolone)
                {
                    ws.Range[T + 2, k].NumberFormat = FormatProcenat;
                    ws.Range[T + 6, k].NumberFormat = FormatProcenat;
                }
                ws.Range[T + 1, kC].CellStyle.Font.RGBColor = Crvena;
                ws.Range[T + 6, kC].CellStyle.Font.RGBColor = Crvena;
                ws.Range[T + 10, kC].CellStyle.Font.RGBColor = Crvena;

                // --- uslovni format za % ispunjenosti i projekciju
                foreach (int red in new[] { T + 2, T + 6 })
                {
                    foreach (int k in sveKolone)
                    {
                        IConditionalFormats cfs = ws.Range[red, k].ConditionalFormats;
                        // za tačno 100% važi prvo pravilo jer ima prednost
                        DodajUslov(cfs, ExcelComparisonOperator.GreaterOrEqual, "1", null, Zuta, Crvena);
                        DodajUslov(cfs, ExcelComparisonOperator.Between, "0.8", "1", Zuta, Color.Black);
                        DodajUslov(cfs, ExcelComparisonOperator.Less, "0.8", null, Siva, Color.Black);
                    }
                }

                // --- ivice: tabela dana (A..poslednja RM) i blok VIP/ONLINE; kolona N bez ivica
                IRange tabela = ws.Range[1, kA, E, kRn];
                tabela.BorderInside(ExcelLineStyle.Thin);
                tabela.BorderAround(ExcelLineStyle.Thin);
                IRange vipBlok = ws.Range[1, kVip, E, kOnl];
                vipBlok.BorderInside(ExcelLineStyle.Thin);
                vipBlok.BorderAround(ExcelLineStyle.Thin);

                // --- širine kolona i zamrzavanje okana
                ws.SetColumnWidth(kA, 7);
                ws.SetColumnWidth(kB, 5);
                ws.SetColumnWidth(kC, 12);
                for (int k = kR1; k <= kRn; k++)
                    ws.SetColumnWidth(k, 11);
                ws.SetColumnWidth(kN, 8);
                ws.SetColumnWidth(kVip, 11);
                ws.SetColumnWidth(kOnl, 11);
                ws.SetColumnWidth(kQ, 12);
                ws.Range[5, kR1].FreezePanes();   // ispod reda 4 i desno od kolone C

                wb.SaveAs(putanja);
                wb.Close();
            }
        }

        private static void DodajUslov(IConditionalFormats cfs, ExcelComparisonOperator operacija, string prva, string druga, Color pozadina, Color slova)
        {
            IConditionalFormat cf = cfs.AddCondition();
            cf.FormatType = ExcelCFType.CellValue;
            cf.Operator = operacija;
            cf.FirstFormula = prva;
            if (druga != null)
                cf.SecondFormula = druga;
            cf.BackColorRGB = pozadina;
            cf.FontColorRGB = slova;
            cf.IsBold = true;
        }

        // Predlog imena fajla bez znakova koji nisu dozvoljeni u imenu
        public static string PredlogImena(string nazivPlana, string brend, DateTime doDatuma)
        {
            string ime = "Dnevni promet - " + nazivPlana + " - " + (brend ?? "Svi brendovi") + " - do " + doDatuma.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture) + ".xlsx";
            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
                ime = ime.Replace(c.ToString(), "");
            return ime;
        }
    }
}
