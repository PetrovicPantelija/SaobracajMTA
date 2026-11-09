using Saobracaj.Theme;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.HR
{
    // Šifarnici HR modula (lista levo, tabela desno) i pravila odobravanja nabavke.
    // Šifarnik koji se koristi ne briše se, nego se isključuje polje Aktivan.
    public class frmHRSifarnici : Form
    {
        private readonly Panel sadrzaj;
        private readonly Dictionary<string, Func<HRTabelaDef>> definicije = new Dictionary<string, Func<HRTabelaDef>>();
        private readonly Dictionary<string, HRTabelaKartica> otvorene = new Dictionary<string, HRTabelaKartica>();
        private readonly Dictionary<string, Action<HRTabelaKartica>> dodatno = new Dictionary<string, Action<HRTabelaKartica>>();

        public frmHRSifarnici(bool samoPravila)
        {
            HRStil.PripremiFormu(this, samoPravila ? "Pravila odobravanja" : "Šifarnici HR");

            sadrzaj = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 16, 20, 16), BackColor = SavaTheme.Surface };
            Controls.Add(sadrzaj);

            if (samoPravila)
            {
                definicije["Pravila odobravanja"] = DefPravila;
                Prikazi("Pravila odobravanja");
                HRStil.NaslovnaTraka(this, "Nabavka › Pravila odobravanja");
                HRStil.StatusnaLinija(this).Text = "Za svaki nivo primenjuje se pravilo sa najviše popunjenih uslova (vrsta, jedinica). " +
                    "Nivo je potreban kad je iznos zahteva između Iznos od i Iznos do. Odobravalac je osoba ili aktivan zaposleni na radnom mestu iz pravila.";
            }
            else
            {
                Dodaj("Radna mesta", () => Sifarnik("RadnaMesta", "Radno mesto"));
                Dodaj("Organizacione jedinice", DefOrganizacioneJedinice);
                Dodaj("Radni odnosi", () => Sifarnik("RadniOdnosi", "Radni odnos"));
                Dodaj("Vrste ugovora", () => Sifarnik("VrsteUgovora", "Vrsta ugovora"));
                Dodaj("Stepen obrazovanja", () => Sifarnik("StepenObrazovanja", "Stepen obrazovanja"));
                Dodaj("Grupe dokumenata", () => Sifarnik("GrupeDokumenata", "Grupa dokumenata"));
                Dodaj("Vrste dokumenata", DefVrsteDokumenta);
                Dodaj("Vrste pregleda", DefVrstePregleda);
                Dodaj("Ocene sposobnosti", () => Sifarnik("OceneSposobnosti", "Ocena sposobnosti"));
                Dodaj("HTZ grupe", () => Sifarnik("HTZGrupe", "HTZ grupa"));
                Dodaj("HTZ oprema", DefHTZOprema);
                Dodaj("Razlozi razduženja", () => Sifarnik("RazloziRazduzenja", "Razlog razduženja"));
                Dodaj("Obavezni dokumenti radnog mesta", DefObavezniDokumenti);
                Dodaj("Obavezni pregledi radnog mesta", DefObavezniPregledi);
                Dodaj("HTZ normativ radnog mesta", DefNormativHTZ);
                Dodaj("Srodstva", () => Sifarnik("Srodstva", "Srodstvo"));
                Dodaj("Težine povreda", () => Sifarnik("TezinePovreda", "Težina povrede"));
                Dodaj("Vrste disciplinskih mera", () => Sifarnik("VrsteDisciplinskihMera", "Vrsta disciplinske mere"));
                Dodaj("Opštine", DefOpstine);
                Dodaj("Vrste nabavke", DefVrsteNabavke);
                Dodaj("Prioriteti nabavke", DefPrioriteti);
                Dodaj("Mesta troška", DefMestaTroska);
                Dodaj("Vrste rada", DefVrsteRada);
                Dodaj("Praznici", DefPraznici);
                Dodaj("Raspored rada", DefRasporedRada);
                Dodaj("Osnovi godišnjeg odmora", DefOsnovGO);
                dodatno["Praznici"] = t => t.DodajKomandu("Predloži praznike za godinu", (s, e) => PredloziPraznike(t));

                var lista = new ListBox
                {
                    Dock = DockStyle.Left,
                    Width = 280,
                    BorderStyle = BorderStyle.None,
                    IntegralHeight = false,
                    ItemHeight = 30,
                    DrawMode = DrawMode.OwnerDrawFixed
                };
                lista.Items.AddRange(definicije.Keys.Cast<object>().ToArray());
                lista.DrawItem += (s, e) =>
                {
                    if (e.Index < 0) return;
                    bool izabran = (e.State & DrawItemState.Selected) != 0;
                    using (var b = new SolidBrush(izabran ? SavaTheme.SelectedRow : SavaTheme.White))
                        e.Graphics.FillRectangle(b, e.Bounds);
                    if (izabran)
                        using (var b = new SolidBrush(SavaTheme.Cyan))
                            e.Graphics.FillRectangle(b, e.Bounds.X, e.Bounds.Y, 3, e.Bounds.Height);
                    TextRenderer.DrawText(e.Graphics, lista.Items[e.Index].ToString(), izabran ? SavaTheme.ButtonFontSemibold : SavaTheme.FieldFont,
                        new Rectangle(e.Bounds.X + 14, e.Bounds.Y, e.Bounds.Width - 14, e.Bounds.Height), SavaTheme.Text, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                };
                lista.SelectedIndexChanged += (s, e) => { if (lista.SelectedItem != null) Prikazi(lista.SelectedItem.ToString()); };
                var levo = HRStil.BeliPanel();
                levo.Dock = DockStyle.Left;
                levo.Width = 282;
                levo.Controls.Add(lista);
                sadrzaj.Padding = new Padding(16, 16, 20, 16);
                var omotac = new Panel { Dock = DockStyle.Left, Width = 302, Padding = new Padding(20, 16, 0, 16), BackColor = SavaTheme.Surface };
                omotac.Controls.Add(levo);
                Controls.Add(omotac);
                HRStil.NaslovnaTraka(this, "HR modul › Šifarnici");
                HRStil.StatusnaLinija(this).Text = "Šifarnik koji se koristi ne briše se: isključite polje Aktivan i stavka više neće biti ponuđena u listama.";
                Load += (s, e) => lista.SelectedIndex = 0;
            }

            HRStil.PrimeniTemu(this);
        }

        private void Dodaj(string naziv, Func<HRTabelaDef> def)
        {
            definicije[naziv] = def;
        }

        private void Prikazi(string naziv)
        {
            foreach (HRTabelaKartica t in otvorene.Values)
                t.Visible = false;
            if (!otvorene.TryGetValue(naziv, out HRTabelaKartica kartica))
            {
                kartica = new HRTabelaKartica(definicije[naziv]()) { Dock = DockStyle.Fill };
                otvorene[naziv] = kartica;
                if (dodatno.TryGetValue(naziv, out Action<HRTabelaKartica> dodaj))
                    dodaj(kartica);
                sadrzaj.Controls.Add(kartica);
                HRStil.PrimeniTemu(kartica);
                kartica.Ucitaj(0);
            }
            kartica.Visible = true;
            kartica.BringToFront();
        }

        private static readonly HRKolona KolonaAktivan = new HRKolona("AktivanTekst", "Aktivan", 80);

        private static string Aktivan(string alias = "") =>
            "CASE WHEN " + alias + "Aktivan = 1 THEN N'Da' ELSE N'Ne' END AS AktivanTekst";

        private static System.Drawing.Color? BojaAktivnog(Syncfusion.Grouping.Record r, string k) =>
            k == "AktivanTekst" && Convert.ToString(r.GetValue("AktivanTekst")) == "Ne" ? SavaTheme.Label : (System.Drawing.Color?)null;

        // Šifarnik sa kolonama Sifra, Naziv, Aktivan
        private static HRTabelaDef Sifarnik(string tabela, string naslov)
        {
            return new HRTabelaDef
            {
                Naslov = naslov,
                Tabela = tabela,
                Audit = false,
                BrojKolonaDijaloga = 1,
                Upit = "SELECT Sifra, Naziv, " + Aktivan() + " FROM dbo." + tabela + " ORDER BY Naziv",
                Kolone = new[] { new HRKolona("Naziv", "Naziv", 360), KolonaAktivan },
                Polja = new List<HRPolje> { HRPolje.Tekst("Naziv", "Naziv", 100, true), HRPolje.DaNe("Aktivan", "Aktivan", true) },
                NoviPodrazumevano = () => new HRVrednosti { ["Aktivan"] = true },
                BojaTeksta = BojaAktivnog
            };
        }

        private static HRTabelaDef DefOrganizacioneJedinice()
        {
            return new HRTabelaDef
            {
                Naslov = "Organizaciona jedinica",
                Tabela = "OrganizacioneJedinice",
                Kljuc = "ID",
                Audit = false,
                BrojKolonaDijaloga = 1,
                Upit = @"SELECT o.ID, o.Naziv, n.Naziv AS Nadredjena FROM dbo.OrganizacioneJedinice o
                         LEFT JOIN dbo.OrganizacioneJedinice n ON n.ID = o.NadredjenaJedinica ORDER BY o.Naziv",
                Kolone = new[] { new HRKolona("Naziv", "Naziv", 300), new HRKolona("Nadredjena", "Nadređena jedinica", 300) },
                Polja = new List<HRPolje>
                {
                    HRPolje.Tekst("Naziv", "Naziv", 100, true),
                    HRPolje.Lista("NadredjenaJedinica", "Nadređena jedinica", HRListe.OrganizacioneJedinice)
                },
                Validacija = null
            };
        }

        private static HRTabelaDef DefVrsteDokumenta()
        {
            return new HRTabelaDef
            {
                Naslov = "Vrsta dokumenta",
                Tabela = "VrsteDokumenta",
                Audit = false,
                Upit = @"SELECT v.Sifra, v.Naziv, g.Naziv AS Grupa, CASE WHEN v.ImaRokVazenja = 1 THEN N'Da' ELSE N'Ne' END AS Rok, v.DanaUpozorenja,
                                CASE WHEN v.ImaKategorije = 1 THEN N'Da' ELSE N'Ne' END AS Kategorije, " + Aktivan("v.") + @"
                         FROM dbo.VrsteDokumenta v JOIN dbo.GrupeDokumenata g ON g.Sifra = v.GrupaDokumenta ORDER BY g.Naziv, v.Naziv",
                Kolone = new[]
                {
                    new HRKolona("Naziv", "Naziv", 220), new HRKolona("Grupa", "Grupa", 160), new HRKolona("Rok", "Ima rok važenja", 120),
                    new HRKolona("DanaUpozorenja", "Upozorenje (dana pre isteka)", 190, "broj"), new HRKolona("Kategorije", "Ima kategorije", 110), KolonaAktivan
                },
                Polja = new List<HRPolje>
                {
                    HRPolje.Tekst("Naziv", "Naziv", 50, true),
                    HRPolje.Lista("GrupaDokumenta", "Grupa dokumenata", HRListe.Sifarnik("GrupeDokumenata"), true),
                    HRPolje.DaNe("ImaRokVazenja", "Ima rok važenja"),
                    HRPolje.Broj("DanaUpozorenja", "Upozoriti dana pre isteka", false, 0, 365),
                    HRPolje.DaNe("ImaKategorije", "Ima kategorije (vozačka i slično)"),
                    HRPolje.DaNe("Aktivan", "Aktivan", true)
                },
                NoviPodrazumevano = () => new HRVrednosti { ["Aktivan"] = true, ["ImaRokVazenja"] = true, ["DanaUpozorenja"] = 30 },
                PriPromeni = (dlg, k) => { if (k == "ImaRokVazenja") dlg.Omoguci("DanaUpozorenja", Convert.ToBoolean(dlg.Vrednost("ImaRokVazenja") ?? false)); },
                BojaTeksta = BojaAktivnog
            };
        }

        private static HRTabelaDef DefVrstePregleda()
        {
            return new HRTabelaDef
            {
                Naslov = "Vrsta pregleda",
                Tabela = "VrstePregleda",
                Audit = false,
                BrojKolonaDijaloga = 1,
                Upit = "SELECT Sifra, Naziv, PeriodMeseci, " + Aktivan() + " FROM dbo.VrstePregleda ORDER BY Naziv",
                Kolone = new[] { new HRKolona("Naziv", "Naziv", 260), new HRKolona("PeriodMeseci", "Period (meseci)", 130, "broj"), KolonaAktivan },
                Polja = new List<HRPolje>
                {
                    HRPolje.Tekst("Naziv", "Naziv", 100, true),
                    HRPolje.Broj("PeriodMeseci", "Period do sledećeg pregleda (meseci)", false, 0, 120),
                    HRPolje.DaNe("Aktivan", "Aktivan", true)
                },
                NoviPodrazumevano = () => new HRVrednosti { ["Aktivan"] = true },
                BojaTeksta = BojaAktivnog
            };
        }

        private static HRTabelaDef DefHTZOprema()
        {
            return new HRTabelaDef
            {
                Naslov = "HTZ oprema",
                Tabela = "HTZOprema",
                Audit = false,
                Upit = @"SELECT o.Sifra, o.Naziv, g.Naziv AS Grupa, o.JedinicaMere, o.RokTrajanjaMeseci,
                                CASE WHEN o.ImaVelicinu = 1 THEN N'Da' ELSE N'Ne' END AS Velicina, " + Aktivan("o.") + @"
                         FROM dbo.HTZOprema o LEFT JOIN dbo.HTZGrupe g ON g.Sifra = o.GrupaHTZ ORDER BY o.Naziv",
                Kolone = new[]
                {
                    new HRKolona("Naziv", "Naziv", 260), new HRKolona("Grupa", "Grupa", 160), new HRKolona("JedinicaMere", "JM", 60),
                    new HRKolona("RokTrajanjaMeseci", "Rok trajanja (meseci)", 160, "broj"), new HRKolona("Velicina", "Ima veličinu", 100), KolonaAktivan
                },
                Polja = new List<HRPolje>
                {
                    HRPolje.Tekst("Naziv", "Naziv", 100, true),
                    HRPolje.Lista("GrupaHTZ", "Grupa", HRListe.Sifarnik("HTZGrupe")),
                    HRPolje.Tekst("JedinicaMere", "Jedinica mere (kom, par…)", 10, true),
                    HRPolje.Broj("RokTrajanjaMeseci", "Rok trajanja (meseci)", false, 0, 240),
                    HRPolje.DaNe("ImaVelicinu", "Traži veličinu pri zaduženju"),
                    HRPolje.DaNe("Aktivan", "Aktivan", true)
                },
                NoviPodrazumevano = () => new HRVrednosti { ["Aktivan"] = true, ["JedinicaMere"] = "kom" },
                BojaTeksta = BojaAktivnog
            };
        }

        private static HRTabelaDef DefObavezniDokumenti()
        {
            return new HRTabelaDef
            {
                Naslov = "Obavezan dokument radnog mesta",
                Tabela = "RadnoMestoObavezniDokumenti",
                Audit = false,
                BrojKolonaDijaloga = 1,
                Upit = @"SELECT o.Sifra, r.Naziv AS RadnoMesto, v.Naziv AS Dokument FROM dbo.RadnoMestoObavezniDokumenti o
                         JOIN dbo.RadnaMesta r ON r.Sifra = o.RadnoMesto JOIN dbo.VrsteDokumenta v ON v.Sifra = o.VrstaDokumenta ORDER BY r.Naziv, v.Naziv",
                Kolone = new[] { new HRKolona("RadnoMesto", "Radno mesto", 260), new HRKolona("Dokument", "Obavezan dokument", 260) },
                Polja = new List<HRPolje>
                {
                    HRPolje.Lista("RadnoMesto", "Radno mesto", HRListe.Sifarnik("RadnaMesta"), true),
                    HRPolje.Lista("VrstaDokumenta", "Vrsta dokumenta", HRListe.Sifarnik("VrsteDokumenta"), true)
                },
                Napomena = "Ako zaposlenom na tom radnom mestu nedostaje važeći dokument, prikazuje se upozorenje (lista, forma, kontrolna tabla)."
            };
        }

        private static HRTabelaDef DefObavezniPregledi()
        {
            return new HRTabelaDef
            {
                Naslov = "Obavezan pregled radnog mesta",
                Tabela = "RadnoMestoObavezniPregledi",
                Audit = false,
                BrojKolonaDijaloga = 1,
                Upit = @"SELECT o.Sifra, r.Naziv AS RadnoMesto, v.Naziv AS Pregled FROM dbo.RadnoMestoObavezniPregledi o
                         JOIN dbo.RadnaMesta r ON r.Sifra = o.RadnoMesto JOIN dbo.VrstePregleda v ON v.Sifra = o.VrstaPregleda ORDER BY r.Naziv, v.Naziv",
                Kolone = new[] { new HRKolona("RadnoMesto", "Radno mesto", 260), new HRKolona("Pregled", "Obavezan pregled", 260) },
                Polja = new List<HRPolje>
                {
                    HRPolje.Lista("RadnoMesto", "Radno mesto", HRListe.Sifarnik("RadnaMesta"), true),
                    HRPolje.Lista("VrstaPregleda", "Vrsta pregleda", HRListe.Sifarnik("VrstePregleda"), true)
                }
            };
        }

        private static HRTabelaDef DefNormativHTZ()
        {
            return new HRTabelaDef
            {
                Naslov = "HTZ normativ radnog mesta",
                Tabela = "RadnoMestoNormativHTZ",
                Audit = false,
                BrojKolonaDijaloga = 1,
                Upit = @"SELECT n.Sifra, r.Naziv AS RadnoMesto, o.Naziv AS Oprema, n.Kolicina FROM dbo.RadnoMestoNormativHTZ n
                         JOIN dbo.RadnaMesta r ON r.Sifra = n.RadnoMesto JOIN dbo.HTZOprema o ON o.Sifra = n.HTZOprema ORDER BY r.Naziv, o.Naziv",
                Kolone = new[] { new HRKolona("RadnoMesto", "Radno mesto", 240), new HRKolona("Oprema", "Oprema", 260), new HRKolona("Kolicina", "Količina", 90, "kolicina") },
                Polja = new List<HRPolje>
                {
                    HRPolje.Lista("RadnoMesto", "Radno mesto", HRListe.Sifarnik("RadnaMesta"), true),
                    HRPolje.Lista("HTZOprema", "HTZ oprema", HRListe.Sifarnik("HTZOprema"), true),
                    new HRPolje("Kolicina", "Količina", HRTip.Decimalni, true) { Min = 0.01m, Podrazumevano = 1m }
                }
            };
        }

        private static HRTabelaDef DefOpstine()
        {
            return new HRTabelaDef
            {
                Naslov = "Opština",
                Tabela = "Opstine",
                Audit = false,
                BrojKolonaDijaloga = 1,
                Upit = @"SELECT o.Sifra, o.Naziv, RTRIM(d.DrNaziv) AS Drzava, " + Aktivan("o.") + @"
                         FROM dbo.Opstine o LEFT JOIN dbo.Drzave d ON RTRIM(d.DrSifra) = o.Drzava ORDER BY o.Naziv",
                Kolone = new[] { new HRKolona("Naziv", "Naziv", 260), new HRKolona("Drzava", "Država", 200), KolonaAktivan },
                Polja = new List<HRPolje>
                {
                    HRPolje.Tekst("Naziv", "Naziv", 50, true),
                    HRPolje.Lista("Drzava", "Država", HRListe.Drzave),
                    HRPolje.DaNe("Aktivan", "Aktivan", true)
                },
                NoviPodrazumevano = () => new HRVrednosti { ["Aktivan"] = true },
                BojaTeksta = BojaAktivnog
            };
        }

        private static HRTabelaDef DefVrsteNabavke()
        {
            return new HRTabelaDef
            {
                Naslov = "Vrsta nabavke",
                Tabela = "VrsteNabavke",
                Audit = false,
                BrojKolonaDijaloga = 1,
                Upit = "SELECT Sifra, Naziv, CASE WHEN ZahtevaSredstvo = 1 THEN N'Da' ELSE N'Ne' END AS Sredstvo, " + Aktivan() + " FROM dbo.VrsteNabavke ORDER BY Naziv",
                Kolone = new[] { new HRKolona("Naziv", "Naziv", 260), new HRKolona("Sredstvo", "Zahteva sredstvo", 140), KolonaAktivan },
                Polja = new List<HRPolje>
                {
                    HRPolje.Tekst("Naziv", "Naziv", 100, true),
                    HRPolje.DaNe("ZahtevaSredstvo", "Zahteva sredstvo"),
                    HRPolje.DaNe("Aktivan", "Aktivan", true)
                },
                NoviPodrazumevano = () => new HRVrednosti { ["Aktivan"] = true },
                BojaTeksta = BojaAktivnog
            };
        }

        private static HRTabelaDef DefPrioriteti()
        {
            return new HRTabelaDef
            {
                Naslov = "Prioritet nabavke",
                Tabela = "PrioritetiNabavke",
                Audit = false,
                BrojKolonaDijaloga = 1,
                Upit = "SELECT Sifra, Naziv, DanaZaRealizaciju, " + Aktivan() + " FROM dbo.PrioritetiNabavke ORDER BY Sifra",
                Kolone = new[] { new HRKolona("Naziv", "Naziv", 220), new HRKolona("DanaZaRealizaciju", "Dana za realizaciju", 160, "broj"), KolonaAktivan },
                Polja = new List<HRPolje>
                {
                    HRPolje.Tekst("Naziv", "Naziv", 100, true),
                    HRPolje.Broj("DanaZaRealizaciju", "Dana za realizaciju", false, 0, 365),
                    HRPolje.DaNe("Aktivan", "Aktivan", true)
                },
                NoviPodrazumevano = () => new HRVrednosti { ["Aktivan"] = true },
                BojaTeksta = BojaAktivnog
            };
        }

        private static HRTabelaDef DefMestaTroska()
        {
            return new HRTabelaDef
            {
                Naslov = "Mesto troška",
                Tabela = "MestaTroska",
                Audit = false,
                BrojKolonaDijaloga = 1,
                Upit = "SELECT Sifra, SifraERP, Naziv, " + Aktivan() + " FROM dbo.MestaTroska ORDER BY Naziv",
                Kolone = new[] { new HRKolona("SifraERP", "Šifra u ERP-u", 130), new HRKolona("Naziv", "Naziv", 260), KolonaAktivan },
                Polja = new List<HRPolje>
                {
                    HRPolje.Tekst("SifraERP", "Šifra u ERP-u", 20),
                    HRPolje.Tekst("Naziv", "Naziv", 100, true),
                    HRPolje.DaNe("Aktivan", "Aktivan", true)
                },
                NoviPodrazumevano = () => new HRVrednosti { ["Aktivan"] = true },
                BojaTeksta = BojaAktivnog
            };
        }

        private const string Boje =
            "SELECT 14939366 AS Boja, N'Svetlozelena' AS Naziv UNION ALL SELECT 16774870, N'Svetložuta' UNION ALL SELECT 14938107, N'Svetloplava' " +
            "UNION ALL SELECT 15591414, N'Svetloljubičasta' UNION ALL SELECT 14676977, N'Svetlotirkizna' UNION ALL SELECT 16507875, N'Svetlocrvena' " +
            "UNION ALL SELECT 15133422, N'Siva'";

        private static HRTabelaDef DefVrsteRada()
        {
            return new HRTabelaDef
            {
                Naslov = "Vrsta rada",
                Tabela = "VrstaRada",
                Kljuc = "ID",
                Audit = false,
                Upit = @"SELECT ID, Oznaka, Naziv, Grupa, CASE WHEN CeoDan = 1 THEN N'Da' ELSE N'' END AS CeoDanTekst,
                                CASE WHEN UlaziUFond = 1 THEN N'Da' ELSE N'' END AS FondTekst, CASE WHEN Placeno = 1 THEN N'Da' ELSE N'' END AS PlacenoTekst,
                                ProcenatNaknade, CASE WHEN SamoKrozZahtev = 1 THEN N'Da' ELSE N'' END AS ZahtevTekst, Redosled,
                                CASE WHEN Aktivno = 1 THEN N'Da' ELSE N'Ne' END AS AktivanTekst
                         FROM dbo.VrstaRada ORDER BY ISNULL(Redosled, 999), Oznaka",
                Kolone = new[]
                {
                    new HRKolona("Oznaka", "Oznaka", 70), new HRKolona("Naziv", "Naziv", 220), new HRKolona("Grupa", "Grupa", 100),
                    new HRKolona("CeoDanTekst", "Ceo dan", 70), new HRKolona("FondTekst", "Ulazi u fond", 95), new HRKolona("PlacenoTekst", "Plaćeno", 70),
                    new HRKolona("ProcenatNaknade", "Naknada %", 90, "iznos"), new HRKolona("ZahtevTekst", "Samo kroz zahtev", 120),
                    new HRKolona("Redosled", "Redosled", 80, "broj"), KolonaAktivan
                },
                Polja = new List<HRPolje>
                {
                    HRPolje.Tekst("Oznaka", "Oznaka (npr. RR, BO)", 5, true),
                    HRPolje.Tekst("Naziv", "Naziv", 100, true),
                    HRPolje.Lista("Grupa", "Grupa", "SELECT 'RAD' AS G, N'Rad' AS N UNION ALL SELECT 'ODSUSTVO', N'Odsustvo' UNION ALL SELECT 'PRAZNIK', N'Praznik'", true),
                    HRPolje.Lista("Boja", "Boja ćelije u matrici", Boje),
                    HRPolje.DaNe("CeoDan", "Unosi se kao ceo dan"),
                    HRPolje.DaNe("UlaziUFond", "Pokriva fond sati", true),
                    HRPolje.DaNe("Placeno", "Plaćeno", true),
                    HRPolje.Decimalni("ProcenatNaknade", "Procenat naknade (računovodstvo)"),
                    HRPolje.DaNe("SamoKrozZahtev", "Samo kroz zahtev (ne kuca se u matrici)"),
                    HRPolje.Broj("Redosled", "Redosled", false, 0, 999),
                    HRPolje.DaNe("Aktivno", "Aktivno", true)
                },
                NoviPodrazumevano = () => new HRVrednosti { ["Aktivno"] = true, ["UlaziUFond"] = true, ["Placeno"] = true, ["Grupa"] = "RAD" },
                Validacija = v => v.Tekst("Oznaka").Contains(" ") ? "Oznaka je bez razmaka." : null,
                BojaTeksta = BojaAktivnog
            };
        }

        private static HRTabelaDef DefPraznici()
        {
            return new HRTabelaDef
            {
                Naslov = "Praznik",
                Tabela = "Praznici",
                Kljuc = "ID",
                Audit = false,
                BrojKolonaDijaloga = 1,
                Upit = @"SELECT ID, Datum, Naziv, CASE WHEN Neradni = 1 THEN N'Neradni' ELSE N'Verski (odsustvo na zahtev)' END AS Vrsta,
                                YEAR(Datum) AS Godina FROM dbo.Praznici ORDER BY Datum DESC",
                Kolone = new[] { new HRKolona("Datum", "Datum", 110, "datum"), new HRKolona("Naziv", "Naziv", 320), new HRKolona("Vrsta", "Vrsta", 220) },
                Polja = new List<HRPolje>
                {
                    HRPolje.Datum("Datum", "Datum", true),
                    HRPolje.Tekst("Naziv", "Naziv", 100, true),
                    HRPolje.DaNe("Neradni", "Neradni dan", true)
                },
                NoviPodrazumevano = () => new HRVrednosti { ["Datum"] = DateTime.Today, ["Neradni"] = true },
                Napomena = "Tabela se puni jednom godišnje. Predloži praznike upisuje zakonske praznike za godinu, pravoslavni Uskrs i pomerene dane kad praznik padne u nedelju."
            };
        }

        // Neradni praznici u Republici Srbiji za godinu: fiksni datumi, pravoslavni Uskrs (petak–ponedeljak)
        // i pomereni dan kad praznik padne u nedelju (sledeći radni dan)
        private void PredloziPraznike(HRTabelaKartica t)
        {
            var polja = new[] { new HRPolje("Godina", "Godina", HRTip.CeoBroj, true) { Min = 2000, Max = 2100 } };
            using (var dlg = new frmHRStavka("Predloži praznike za godinu", polja, new HRVrednosti { ["Godina"] = DateTime.Today.Year + (DateTime.Today.Month >= 11 ? 1 : 0) }, 1))
            {
                int dodato = 0;
                dlg.Snimanje = v =>
                {
                    int g = v.Broj("Godina");
                    var lista = new List<Tuple<DateTime, string>>();
                    var grupe = new[]
                    {
                        new[] { Tuple.Create(new DateTime(g, 1, 1), "Nova godina"), Tuple.Create(new DateTime(g, 1, 2), "Nova godina") },
                        new[] { Tuple.Create(new DateTime(g, 2, 15), "Dan državnosti"), Tuple.Create(new DateTime(g, 2, 16), "Dan državnosti") },
                        new[] { Tuple.Create(new DateTime(g, 5, 1), "Praznik rada"), Tuple.Create(new DateTime(g, 5, 2), "Praznik rada") },
                        new[] { Tuple.Create(new DateTime(g, 11, 11), "Dan primirja u Prvom svetskom ratu") }
                    };
                    foreach (var grupa in grupe)
                    {
                        lista.AddRange(grupa);
                        // praznik u nedelju: ne radi se prvog sledećeg radnog dana
                        DateTime poslednji = grupa[grupa.Length - 1].Item1;
                        if (grupa.Any(x => x.Item1.DayOfWeek == DayOfWeek.Sunday))
                        {
                            DateTime d = poslednji.AddDays(1);
                            while (d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday)
                                d = d.AddDays(1);
                            lista.Add(Tuple.Create(d, grupa[0].Item2 + " (pomereni dan)"));
                        }
                    }
                    lista.Add(Tuple.Create(new DateTime(g, 1, 7), "Božić (pravoslavni)"));
                    DateTime uskrs = PravoslavniUskrs(g);
                    lista.Add(Tuple.Create(uskrs.AddDays(-2), "Veliki petak"));
                    lista.Add(Tuple.Create(uskrs.AddDays(-1), "Velika subota"));
                    lista.Add(Tuple.Create(uskrs, "Vaskrs"));
                    lista.Add(Tuple.Create(uskrs.AddDays(1), "Vaskršnji ponedeljak"));
                    foreach (var p in lista)
                        dodato += Math.Max(0, HRBaza.Izvrsi("IF NOT EXISTS (SELECT 1 FROM dbo.Praznici WHERE Datum = @D) INSERT dbo.Praznici (Datum, Naziv, Neradni) VALUES (@D, @N, 1)",
                            HRBaza.P("@D", p.Item1.Date), HRBaza.P("@N", p.Item2)));
                    return null;
                };
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    t.Osvezi();
                    MessageBox.Show(this, "Dodato praznika: " + dodato +". Proverite spisak; verske praznike (neradni samo za vernike) dodajte ručno kao „ne neradni“.",
                        "Praznici", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // Pravoslavni Uskrs: julijanski kalendar (Meeus) + 13 dana za 1900–2099
        public static DateTime PravoslavniUskrs(int godina)
        {
            int a = godina % 4, b = godina % 7, c = godina % 19;
            int d = (19 * c + 15) % 30;
            int e = (2 * a + 4 * b - d + 34) % 7;
            int mesec = (d + e + 114) / 31;
            int dan = (d + e + 114) % 31 + 1;
            return new DateTime(godina, mesec, dan).AddDays(13);
        }

        private static HRTabelaDef DefRasporedRada()
        {
            var polja = new List<HRPolje> { new HRPolje("Naziv", "Naziv (npr. Pon–Pet 8 h)", HRTip.Tekst, true, 50) { Raspon = 2 } };
            foreach (var d in new[] { "Pon", "Uto", "Sre", "Cet", "Pet", "Sub", "Ned" })
                polja.Add(new HRPolje(d, d == "Cet" ? "Čet (h)" : d + " (h)", HRTip.Decimalni, true) { Min = 0, Max = 24 });
            polja.Add(HRPolje.Tekst("VremeOd", "Početak rada (HH:mm, za kašnjenje)", 8));
            polja.Add(HRPolje.Tekst("VremeDo", "Kraj rada (HH:mm)", 8));
            return new HRTabelaDef
            {
                Naslov = "Raspored rada",
                Tabela = "RasporedRada",
                Kljuc = "ID",
                Audit = false,
                Upit = @"SELECT ID, Naziv, Pon, Uto, Sre, Cet, Pet, Sub, Ned, Pon + Uto + Sre + Cet + Pet + Sub + Ned AS Nedeljno,
                                CONVERT(varchar(5), VremeOd, 108) AS Od, CONVERT(varchar(5), VremeDo, 108) AS Do
                         FROM dbo.RasporedRada ORDER BY Naziv",
                Kolone = new[]
                {
                    new HRKolona("Naziv", "Naziv", 200), new HRKolona("Pon", "Pon", 55, "kolicina"), new HRKolona("Uto", "Uto", 55, "kolicina"),
                    new HRKolona("Sre", "Sre", 55, "kolicina"), new HRKolona("Cet", "Čet", 55, "kolicina"), new HRKolona("Pet", "Pet", 55, "kolicina"),
                    new HRKolona("Sub", "Sub", 55, "kolicina"), new HRKolona("Ned", "Ned", 55, "kolicina"), new HRKolona("Nedeljno", "Nedeljno (h)", 100, "kolicina"),
                    new HRKolona("Od", "Od", 60), new HRKolona("Do", "Do", 60)
                },
                Polja = polja,
                NoviPodrazumevano = () => new HRVrednosti { ["Pon"] = 8m, ["Uto"] = 8m, ["Sre"] = 8m, ["Cet"] = 8m, ["Pet"] = 8m, ["Sub"] = 0m, ["Ned"] = 0m },
                Validacija = v =>
                {
                    foreach (string k in new[] { "VremeOd", "VremeDo" })
                        if (v.Tekst(k) != "" && !TimeSpan.TryParse(v.Tekst(k), out _))
                            return "Vreme upišite kao HH:mm (npr. 07:00).";
                    return null;
                },
                Napomena = "Zaposleni bez rasporeda radi po podešavanju HR.DnevniFondSati (8 h) od ponedeljka do petka. Raspored se dodeljuje na formi zaposlenog."
            };
        }

        private static HRTabelaDef DefOsnovGO()
        {
            return new HRTabelaDef
            {
                Naslov = "Osnov godišnjeg odmora",
                Tabela = "OsnovGO",
                Kljuc = "ID",
                Audit = false,
                BrojKolonaDijaloga = 1,
                Upit = @"SELECT ID, Oznaka, Naziv, BrojDana, CASE WHEN Osnovni = 1 THEN N'Da' ELSE N'' END AS OsnovniTekst, Napomena,
                                CASE WHEN Aktivno = 1 THEN N'Da' ELSE N'Ne' END AS AktivanTekst
                         FROM dbo.OsnovGO ORDER BY Osnovni DESC, Oznaka",
                Kolone = new[]
                {
                    new HRKolona("Oznaka", "Oznaka", 80), new HRKolona("Naziv", "Naziv", 260), new HRKolona("BrojDana", "Broj dana", 90, "broj"),
                    new HRKolona("OsnovniTekst", "Zakonski minimum", 130), new HRKolona("Napomena", "Napomena", 240), KolonaAktivan
                },
                Polja = new List<HRPolje>
                {
                    HRPolje.Tekst("Oznaka", "Oznaka", 10, true),
                    HRPolje.Tekst("Naziv", "Naziv", 100, true),
                    HRPolje.Broj("BrojDana", "Podrazumevani broj dana", true, 0, 60),
                    HRPolje.DaNe("Osnovni", "Zakonski minimum (dodeljuje se svima)"),
                    HRPolje.Tekst("Napomena", "Napomena", 250),
                    HRPolje.DaNe("Aktivno", "Aktivno", true)
                },
                NoviPodrazumevano = () => new HRVrednosti { ["Aktivno"] = true },
                BojaTeksta = BojaAktivnog
            };
        }

        private static HRTabelaDef DefPravila()
        {
            return new HRTabelaDef
            {
                Naslov = "Pravilo odobravanja",
                Tabela = "PravilaOdobravanjaNabavke",
                Audit = false,
                Upit = @"SELECT p.Sifra, p.Nivo, ISNULL(v.Naziv, N'sve vrste') AS Vrsta, ISNULL(j.Naziv, N'sve jedinice') AS Jedinica,
                                p.IznosOd, p.IznosDo, rm.Naziv AS RadnoMesto,
                                LTRIM(RTRIM(ISNULL(o.Prezime, N'') + N' ' + ISNULL(o.Ime, N''))) AS Odobravalac,
                                LTRIM(RTRIM(ISNULL(z.Prezime, N'') + N' ' + ISNULL(z.Ime, N''))) AS Zamenik, " + Aktivan("p.") + @"
                         FROM dbo.PravilaOdobravanjaNabavke p
                         LEFT JOIN dbo.VrsteNabavke v ON v.Sifra = p.VrstaNabavke
                         LEFT JOIN dbo.OrganizacioneJedinice j ON j.ID = p.OrganizacionaJedinica
                         LEFT JOIN dbo.RadnaMesta rm ON rm.Sifra = p.RadnoMestoOdobravaoca
                         LEFT JOIN dbo.Zaposleni o ON o.ID = p.Odobravalac
                         LEFT JOIN dbo.Zaposleni z ON z.ID = p.Zamenik
                         ORDER BY p.Nivo, p.IznosOd",
                Kolone = new[]
                {
                    new HRKolona("Nivo", "Nivo", 60, "broj"), new HRKolona("Vrsta", "Vrsta nabavke", 170), new HRKolona("Jedinica", "Org. jedinica", 170),
                    new HRKolona("IznosOd", "Iznos od", 120, "iznos"), new HRKolona("IznosDo", "Iznos do", 120, "iznos"),
                    new HRKolona("RadnoMesto", "Radno mesto odobravaoca", 190), new HRKolona("Odobravalac", "Odobravalac", 170),
                    new HRKolona("Zamenik", "Zamenik", 170), KolonaAktivan
                },
                Polja = new List<HRPolje>
                {
                    new HRPolje("Nivo", "Nivo (1, 2, 3…)", HRTip.CeoBroj, true) { Min = 1, Max = 9 },
                    HRPolje.Lista("VrstaNabavke", "Vrsta nabavke (prazno = sve)", HRListe.Sifarnik("VrsteNabavke")),
                    HRPolje.Lista("OrganizacionaJedinica", "Org. jedinica (prazno = sve)", HRListe.OrganizacioneJedinice),
                    HRPolje.DaNe("Aktivan", "Aktivno", true),
                    new HRPolje("IznosOd", "Iznos od", HRTip.Decimalni, true) { Min = 0 },
                    HRPolje.Decimalni("IznosDo", "Iznos do (prazno = bez granice)"),
                    HRPolje.Lista("RadnoMestoOdobravaoca", "Radno mesto odobravaoca", HRListe.Sifarnik("RadnaMesta")),
                    HRPolje.Lista("Odobravalac", "Odobravalac (konkretna osoba)", HRListe.AktivniZaposleni),
                    HRPolje.Lista("Zamenik", "Zamenik (kad je odobravalac podnosilac ili odsutan)", HRListe.AktivniZaposleni)
                },
                NoviPodrazumevano = () => new HRVrednosti { ["Aktivan"] = true, ["IznosOd"] = 0m, ["Nivo"] = 1 },
                Validacija = v =>
                {
                    if (v.Uzmi("RadnoMestoOdobravaoca") == null && v.Uzmi("Odobravalac") == null)
                        return "Unesite radno mesto odobravaoca ili konkretnog odobravaoca.";
                    if (v.Uzmi("IznosDo") != null && HRBaza.Decimalni(v.Uzmi("IznosDo")) < HRBaza.Decimalni(v.Uzmi("IznosOd")))
                        return "Iznos do ne može biti manji od iznosa od.";
                    return null;
                },
                BojaTeksta = BojaAktivnog
            };
        }
    }
}
