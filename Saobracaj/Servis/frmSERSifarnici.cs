using Saobracaj.Theme;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.Servis
{
    // Šifarnici servisa: vrste sredstava, sredstva, plan održavanja sa normativom delova, grupe delova
    public class frmSERSifarnici : Form
    {
        // Jedan šifarnik: tabela, upit za prikaz, kolone, polja dijaloga
        private class Sifarnik
        {
            public string Naslov, Tabela, Upit;
            public SERKolona[] Kolone;
            public Func<List<SERPolje>> Polja;
            public GridGroupingControl Grid;
        }

        private readonly List<Sifarnik> sifarnici = new List<Sifarnik>();
        private readonly SERKartice kartice;
        private GridGroupingControl gridNormativ;
        private Sifarnik plan;

        public frmSERSifarnici()
        {
            SERStil.PripremiFormu(this, "Sredstva i plan održavanja");
            kartice = new SERKartice();

            Dodaj(new Sifarnik
            {
                Naslov = "Vrste sredstava",
                Tabela = "SERVrstaSredstva",
                Upit = @"SELECT v.ID, v.Naziv, ISNULL(v.Napomena, N'') AS Napomena, v.Aktivno,
                                (SELECT COUNT(*) FROM dbo.SERSredstvo s WHERE s.VrstaSredstvaID = v.ID) AS BrojSredstava,
                                (SELECT COUNT(*) FROM dbo.SERPlanStavka p WHERE p.VrstaSredstvaID = v.ID) AS BrojServisa,
                                (SELECT COUNT(*) FROM dbo.SERPlanNormativ n JOIN dbo.SERPlanStavka p ON p.ID = n.PlanStavkaID
                                 WHERE p.VrstaSredstvaID = v.ID) AS BrojNormativa,
                                CASE WHEN v.Aktivno = 1 THEN N'Da' ELSE N'Ne' END AS AktivnoTekst
                         FROM dbo.SERVrstaSredstva v ORDER BY v.Naziv",
                Kolone = new[]
                {
                    new SERKolona("Naziv", "Naziv", 260), new SERKolona("BrojSredstava", "Sredstava", 90, "broj"),
                    new SERKolona("BrojServisa", "Servisa u planu", 120, "broj"), new SERKolona("BrojNormativa", "Stavki normativa", 130, "broj"),
                    new SERKolona("AktivnoTekst", "Aktivno", 70), new SERKolona("Napomena", "Napomena", 300)
                },
                Polja = () => new List<SERPolje>
                {
                    new SERPolje("Naziv", "Naziv", SERTip.Tekst, true, 100) { Raspon = 2 },
                    SERPolje.DaNe("Aktivno", "Aktivno", true),
                    SERPolje.Napomena()
                }
            }, kartice);

            Dodaj(new Sifarnik
            {
                Naslov = "Sredstva",
                Tabela = "SERSredstvo",
                Upit = @"SELECT s.ID, s.Oznaka, s.Naziv, s.VrstaSredstvaID, v.Naziv AS Vrsta, ISNULL(s.SerijskiBroj, N'') AS SerijskiBroj,
                                ISNULL(s.Napomena, N'') AS Napomena, s.Aktivno, CASE WHEN s.Aktivno = 1 THEN N'Da' ELSE N'Ne' END AS AktivnoTekst
                         FROM dbo.SERSredstvo s JOIN dbo.SERVrstaSredstva v ON v.ID = s.VrstaSredstvaID ORDER BY s.Oznaka",
                Kolone = new[]
                {
                    new SERKolona("Oznaka", "Oznaka", 120), new SERKolona("Naziv", "Naziv", 240), new SERKolona("Vrsta", "Vrsta sredstva", 200),
                    new SERKolona("SerijskiBroj", "Serijski broj", 140), new SERKolona("AktivnoTekst", "Aktivno", 70), new SERKolona("Napomena", "Napomena", 260)
                },
                Polja = () => new List<SERPolje>
                {
                    SERPolje.Tekst("Oznaka", "Oznaka", 30, true),
                    SERPolje.Tekst("Naziv", "Naziv", 100, true),
                    SERPolje.Lista("VrstaSredstvaID", "Vrsta sredstva", SERListe.VrsteSredstava, true),
                    SERPolje.Tekst("SerijskiBroj", "Serijski broj", 50),
                    SERPolje.DaNe("Aktivno", "Aktivno", true),
                    SERPolje.Napomena()
                }
            }, kartice);

            plan = new Sifarnik
            {
                Naslov = "Plan održavanja",
                Tabela = "SERPlanStavka",
                Upit = @"SELECT p.ID, p.VrstaSredstvaID, v.Naziv AS Vrsta, p.Naziv, p.IntervalDana, p.IntervalSati, p.IntervalKm,
                                (SELECT COUNT(*) FROM dbo.SERPlanNormativ n WHERE n.PlanStavkaID = p.ID) AS BrojNormativa,
                                ISNULL(p.Napomena, N'') AS Napomena, p.Aktivno, CASE WHEN p.Aktivno = 1 THEN N'Da' ELSE N'Ne' END AS AktivnoTekst
                         FROM dbo.SERPlanStavka p JOIN dbo.SERVrstaSredstva v ON v.ID = p.VrstaSredstvaID ORDER BY v.Naziv, p.Naziv",
                Kolone = new[]
                {
                    new SERKolona("Vrsta", "Vrsta sredstva", 200), new SERKolona("Naziv", "Servis", 220),
                    new SERKolona("IntervalDana", "Na dana", 80, "broj"), new SERKolona("IntervalSati", "Na mč", 80, "broj"),
                    new SERKolona("IntervalKm", "Na km", 80, "broj"), new SERKolona("BrojNormativa", "Stavki normativa", 130, "broj"),
                    new SERKolona("AktivnoTekst", "Aktivno", 70)
                },
                Polja = () => new List<SERPolje>
                {
                    SERPolje.Lista("VrstaSredstvaID", "Vrsta sredstva", SERListe.VrsteSredstava, true),
                    SERPolje.Tekst("Naziv", "Naziv servisa", 100, true),
                    SERPolje.Broj("IntervalDana", "Interval (dana)"),
                    SERPolje.Broj("IntervalSati", "Interval (moto-sati)"),
                    SERPolje.Broj("IntervalKm", "Interval (km)"),
                    SERPolje.DaNe("Aktivno", "Aktivno", true),
                    SERPolje.Napomena()
                }
            };
            Dodaj(plan, kartice);

            Dodaj(new Sifarnik
            {
                Naslov = "Grupe delova",
                Tabela = "SERDeoGrupa",
                Upit = @"SELECT g.ID, g.Naziv, g.Aktivno, CASE WHEN g.Aktivno = 1 THEN N'Da' ELSE N'Ne' END AS AktivnoTekst,
                                (SELECT COUNT(*) FROM dbo.SERDeo d WHERE d.GrupaID = g.ID) AS BrojDelova
                         FROM dbo.SERDeoGrupa g ORDER BY g.Naziv",
                Kolone = new[] { new SERKolona("Naziv", "Naziv", 300), new SERKolona("BrojDelova", "Delova", 90, "broj"), new SERKolona("AktivnoTekst", "Aktivno", 70) },
                Polja = () => new List<SERPolje> { new SERPolje("Naziv", "Naziv", SERTip.Tekst, true, 100) { Raspon = 2 }, SERPolje.DaNe("Aktivno", "Aktivno", true) }
            }, kartice);

            SERStil.NaslovnaTraka(this, "Servis › Sredstva i plan održavanja");
            SERStil.StatusnaLinija(this).Text = "Dupli klik menja zapis · Zapis se ne briše, nego deaktivira · Normativ: delovi koje redovni servis rezerviše na nalogu";

            Controls.Add(kartice);
            kartice.BringToFront();
            SERStil.PrimeniTemu(this);
            kartice.Promenjena += k => Ucitaj(sifarnici.FirstOrDefault(s => s.Naslov == k));
            VisibleChanged += (s, e) => { if (Visible) Ucitaj(sifarnici.FirstOrDefault(x => x.Naslov == TrenutniNaslov())); };
        }

        private string TrenutniNaslov()
        {
            return kartice.Izabrana >= 0 && kartice.Izabrana < sifarnici.Count ? sifarnici[kartice.Izabrana].Naslov : null;
        }

        private static FlowLayoutPanel Traka()
        {
            return new FlowLayoutPanel { Dock = DockStyle.Top, Height = 46, BackColor = SavaTheme.Surface, WrapContents = false };
        }

        private static Button Dugme(string tekst, EventHandler klik, string uloga = null)
        {
            var b = SERStil.Dugme(tekst, uloga);
            b.Click += klik;
            return b;
        }

        private static Control Okvir(Control grid)
        {
            var okvir = SERStil.BeliPanel();
            okvir.Dock = DockStyle.Fill;
            grid.Dock = DockStyle.Fill;
            okvir.Controls.Add(grid);
            return okvir;
        }

        private void Dodaj(Sifarnik s, SERKartice k)
        {
            s.Grid = SERGrid.Novi();
            SERGrid.Kolone(s.Grid, s.Kolone);
            SERGrid.BojaTeksta(s.Grid, (r, kol) => Convert.ToString(r.GetValue("AktivnoTekst")) == "Ne" ? SavaTheme.Label : (Color?)null);
            SERGrid.DuplikKlik(s.Grid, () => Izmeni(s, SERGrid.IzabraniId(s.Grid)));

            var strana = new Panel { BackColor = SavaTheme.Surface };
            var traka = Traka();
            traka.Controls.Add(Dugme("+ Dodaj", (o, e) => Izmeni(s, 0), "primary"));
            traka.Controls.Add(Dugme("Izmeni", (o, e) => Izmeni(s, SERGrid.IzabraniId(s.Grid))));
            traka.Controls.Add(Dugme("Osveži", (o, e) => Ucitaj(s)));

            if (s == plan)
            {
                gridNormativ = SERGrid.Novi();
                SERGrid.Kolone(gridNormativ,
                    new SERKolona("Sifra", "Šifra", 100), new SERKolona("Naziv", "Deo", 260), new SERKolona("Kolicina", "Količina", 85, "kolicina"),
                    new SERKolona("JM", "JM", 50), new SERKolona("Raspolozivo", "Raspoloživo", 95, "kolicina"), new SERKolona("Dostupnost", "Dostupnost", 120));
                SERGrid.BojaTeksta(gridNormativ, (r, kol) => kol == "Dostupnost" && Convert.ToString(r.GetValue("Dostupnost")) != "Ima na stanju" ? SERStil.StatusIsteklo : (Color?)null);
                s.Grid.SelectedRecordsChanged += (o, e) => UcitajNormativ();
                var normativ = new Panel { BackColor = SavaTheme.Surface };
                var trakaN = Traka();
                trakaN.Controls.Add(Dugme("Dodaj deo u normativ", (o, e) => Normativ(0)));
                trakaN.Controls.Add(Dugme("Izmeni količinu", (o, e) => Normativ(SERGrid.IzabraniId(gridNormativ))));
                trakaN.Controls.Add(Dugme("Ukloni iz normativa", (o, e) => UkloniNormativ(), "danger"));
                normativ.Controls.Add(Okvir(gridNormativ));
                normativ.Controls.Add(trakaN);
                var podela = SERStil.Podeljeno("Servisi", Okvir(s.Grid), "Normativ delova izabranog servisa", normativ, 50);
                podela.Dock = DockStyle.Fill;
                strana.Controls.Add(podela);
            }
            else
                strana.Controls.Add(Okvir(s.Grid));
            strana.Controls.Add(traka);
            sifarnici.Add(s);
            k.Dodaj(s.Naslov, strana);
        }

        private void Ucitaj(Sifarnik s)
        {
            if (s == null)
                return;
            try
            {
                SERGrid.Napuni(s.Grid, SERBaza.Tabela(s.Upit));
            }
            catch (Exception ex)
            {
                SERDijalog.Greska(this, Text, ex);
                return;
            }
            if (s == plan)
                UcitajNormativ();
        }

        private void Izmeni(Sifarnik s, int id)
        {
            if (!SERPrava.Proveri(this, SERPrava.Magacioner))
                return;
            List<SERPolje> polja = s.Polja();
            SERVrednosti pocetne = id > 0
                ? new SERVrednosti(SERBaza.RedUMapu(SERBaza.Red("SELECT * FROM dbo." + s.Tabela + " WHERE ID = @ID", SERBaza.P("@ID", id))))
                : new SERVrednosti { ["Aktivno"] = true };
            // plan održavanja: bar jedan interval (dani, moto-sati, km) veći od 0; interval 0 se upisuje kao prazan
            Func<SERVrednosti, string> validacija = s != plan ? (Func<SERVrednosti, string>)null : v =>
                v.Broj("IntervalDana") > 0 || v.Broj("IntervalSati") > 0 || v.Broj("IntervalKm") > 0
                    ? null
                    : "Unesite bar jedan interval servisa veći od 0 (dani, moto-sati ili km).";
            if (SERDijalog.Unos(this, (id > 0 ? "Izmena: " : "Novo: ") + s.Naslov, polja, pocetne, v =>
                {
                    if (s == plan)
                        foreach (string k in new[] { "IntervalDana", "IntervalSati", "IntervalKm" })
                            if (v.Broj(k) == 0)
                                v[k] = null;
                    Snimi(s.Tabela, polja, v, id);
                }, 2, null, validacija))
                Ucitaj(s);
        }

        // INSERT ili UPDATE po poljima dijaloga (nazivi kolona su iz koda, vrednosti su parametri)
        private static void Snimi(string tabela, List<SERPolje> polja, SERVrednosti v, int id)
        {
            var parametri = new List<System.Data.SqlClient.SqlParameter> { SERBaza.P("@Korisnik", SERBaza.Korisnik), SERBaza.P("@ID", id) };
            foreach (SERPolje p in polja)
            {
                object x = v.Uzmi(p.Kolona);
                parametri.Add(SERBaza.P("@" + p.Kolona, x is string t ? SERBaza.Tekst(t) : x));
            }
            string kolone = string.Join(", ", polja.Select(p => p.Kolona));
            string upit = id == 0
                ? "INSERT dbo." + tabela + " (" + kolone + ", KreiraoKorisnik) VALUES (" + string.Join(", ", polja.Select(p => "@" + p.Kolona)) + ", @Korisnik)"
                : "UPDATE dbo." + tabela + " SET " + string.Join(", ", polja.Select(p => p.Kolona + " = @" + p.Kolona))
                  + ", IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime() WHERE ID = @ID";
            SERBaza.Izvrsi(upit, parametri.ToArray());
        }

        private int PlanStavka => SERGrid.IzabraniId(plan.Grid);

        private void UcitajNormativ()
        {
            try
            {
                SERGrid.Napuni(gridNormativ, SERBaza.Tabela(@"
                    SELECT n.ID, d.Sifra, d.Naziv, n.Kolicina, d.JM, s.Raspolozivo,
                           CASE WHEN s.Raspolozivo >= n.Kolicina THEN N'Ima na stanju'
                                WHEN s.Raspolozivo + s.Naruceno >= n.Kolicina THEN N'Naručeno'
                                ELSE N'Nema dovoljno' END AS Dostupnost
                    FROM dbo.SERPlanNormativ n JOIN dbo.SERDeo d ON d.ID = n.DeoID CROSS APPLY dbo.fnSER_Stanje(n.DeoID) s
                    WHERE n.PlanStavkaID = @P ORDER BY d.Sifra", SERBaza.P("@P", PlanStavka)));
            }
            catch (Exception ex)
            {
                SERDijalog.Greska(this, Text, ex);
            }
        }

        private void Normativ(int normativID)
        {
            if (PlanStavka <= 0 || !SERPrava.Proveri(this, SERPrava.Magacioner))
                return;
            var polja = new List<SERPolje>
            {
                new SERPolje("DeoID", "Deo", SERTip.Lista, true) { ListaUpit = SERListe.AktivniDelovi, Raspon = 2, SamoCitanje = normativID > 0 },
                SERPolje.Kolicina("Kolicina", "Količina po servisu")
            };
            SERVrednosti pocetne = normativID > 0
                ? new SERVrednosti(SERBaza.RedUMapu(SERBaza.Red("SELECT DeoID, Kolicina FROM dbo.SERPlanNormativ WHERE ID = @ID", SERBaza.P("@ID", normativID))))
                : null;
            int plStavka = PlanStavka;
            if (SERDijalog.Unos(this, "Normativ delova", polja, pocetne, v =>
                {
                    if (normativID > 0)
                        SERBaza.Izvrsi("UPDATE dbo.SERPlanNormativ SET Kolicina = @Kol, IzmenioKorisnik = @K, DatumIzmene = sysdatetime() WHERE ID = @ID",
                            SERBaza.P("@Kol", v.Uzmi("Kolicina")), SERBaza.P("@K", SERBaza.Korisnik), SERBaza.P("@ID", normativID));
                    else
                        SERBaza.Izvrsi("INSERT dbo.SERPlanNormativ (PlanStavkaID, DeoID, Kolicina, KreiraoKorisnik) VALUES (@P, @D, @Kol, @K)",
                            SERBaza.P("@P", plStavka), SERBaza.P("@D", v.Uzmi("DeoID")), SERBaza.P("@Kol", v.Uzmi("Kolicina")), SERBaza.P("@K", SERBaza.Korisnik));
                }))
                OsveziPlan();
        }

        private void UkloniNormativ()
        {
            int n = SERGrid.IzabraniId(gridNormativ);
            if (n <= 0 || !SERPrava.Proveri(this, SERPrava.Magacioner))
                return;
            if (SERDijalog.Potvrdi(this, "Ukloniti deo iz normativa ovog servisa?", Text)
                && SERDijalog.Pokusaj(this, Text, () => SERBaza.Izvrsi("DELETE dbo.SERPlanNormativ WHERE ID = @ID", SERBaza.P("@ID", n))))
                OsveziPlan();
        }

        // Posle izmene normativa: ponovo puni listu servisa (kolona „Stavki normativa“), izabrani servis ostaje izabran
        private void OsveziPlan()
        {
            int izabran = PlanStavka;
            Ucitaj(plan);
            if (SERGrid.Izaberi(plan.Grid, "ID", izabran))
                UcitajNormativ();
        }
    }
}
