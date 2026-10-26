using Saobracaj.Theme;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.HR
{
    // Zahtev za nabavku (izgled formi, strane 9 i 10): zaglavlje sa ključnim podacima, kartice Podaci, Stavke, Ponude,
    // Prilozi i Istorija, panel toka odobravanja desno. Komande na naslovnoj traci zavise od statusa i uloge korisnika.
    public class frmHRZahtev : Form
    {
        private int id;
        private int status;
        private int ja;
        private bool cekaMene, nabavka;
        private DataRow zahtev;

        private readonly HRNaslovnaTraka naslov;
        private readonly TableLayoutPanel zaglavlje;
        private readonly HRKartice kartice;
        private readonly HRObrazac podaci;
        private readonly HRTabelaKartica stavke, prilozi, istorija, odluke;
        private readonly FlowLayoutPanel ponude;
        private readonly Panel ponudeUpozorenje;
        private readonly HRTekst ponudeUpozorenjeTekst;
        private readonly FlowLayoutPanel ponudeKomande;
        private readonly HROdobravanjePanel odobravanje;
        private readonly Label statusna;
        private int izabranaPonuda;
        private bool prvoUcitavanje = true;

        public frmHRZahtev(int zahtevId)
        {
            id = zahtevId;
            HRStil.PripremiFormu(this, "Zahtev za nabavku");
            nabavka = HRPrava.ImaPravo(HRPrava.ObradaNabavke);

            // kartice
            kartice = new HRKartice();
            podaci = new HRObrazac(PoljaPodaci(), 4);
            podaci.Promena += PodaciPromena;
            var podaciPanel = new Panel { AutoScroll = true };
            podaci.Panel.Dock = DockStyle.Top;
            podaciPanel.Controls.Add(podaci.Panel);
            kartice.Dodaj("Podaci", podaciPanel);

            stavke = new HRTabelaKartica(DefStavke());
            stavke.Promenjeno += Ucitaj;
            stavke.DodajKomandu("Prijem robe", (s, e) => Prijem());
            kartice.Dodaj("Stavke", stavke);

            ponude = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true, BackColor = SavaTheme.Surface, Padding = new Padding(0, 4, 0, 0) };
            ponudeKomande = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 44, FlowDirection = FlowDirection.RightToLeft, WrapContents = false, BackColor = Color.Transparent };
            ponudeUpozorenje = new Panel { Dock = DockStyle.Bottom, Height = 40, BackColor = HRStil.PozadinaIstice, Visible = false, Padding = new Padding(16, 0, 16, 0) };
            ponudeUpozorenjeTekst = new HRTekst { Dock = DockStyle.Fill, ForeColor = ColorTranslator.FromHtml("#7A3E00"), Font = SavaTheme.FieldFont };
            ponudeUpozorenje.Controls.Add(ponudeUpozorenjeTekst);
            var ponudePanel = new Panel { BackColor = SavaTheme.Surface };
            ponudePanel.Controls.Add(ponude);
            ponudePanel.Controls.Add(ponudeUpozorenje);
            ponudePanel.Controls.Add(ponudeKomande);
            kartice.Dodaj("Ponude", ponudePanel);
            PodesiKomandePonuda();

            prilozi = new HRTabelaKartica(DefPrilozi());
            prilozi.DodajKomandu("Obriši", (s, e) => ObrisiPrilog(), "danger");
            prilozi.DodajKomandu("Otvori", (s, e) => { if (prilozi.IzabraniId() > 0) HRPrilozi.Otvori("ZahtevZaNabavkuPrilozi", prilozi.IzabraniId()); });
            prilozi.DodajKomandu("+ Dodaj prilog", (s, e) => { if (id > 0 && HRPrilozi.Dodaj(this, "ZahtevZaNabavkuPrilozi", "Zahtev", id, null)) prilozi.Osvezi(); });
            HRGrid.DuplikKlik(prilozi.Grid, () => { if (prilozi.IzabraniId() > 0) HRPrilozi.Otvori("ZahtevZaNabavkuPrilozi", prilozi.IzabraniId()); });
            kartice.Dodaj("Prilozi", prilozi);

            istorija = new HRTabelaKartica(DefIstorija());
            odluke = new HRTabelaKartica(DefOdluke());
            kartice.Dodaj("Istorija", HRStil.Podeljeno("Promene statusa", istorija, "Odluke odobravalaca", odluke, 50));

            // panel toka odobravanja desno
            odobravanje = new HROdobravanjePanel { Dock = DockStyle.Right, Width = 380 };
            var sredina = new Panel { Dock = DockStyle.Fill, BackColor = SavaTheme.Surface };
            sredina.Controls.Add(kartice);
            var desno = new Panel { Dock = DockStyle.Right, Width = 400, Padding = new Padding(0, 16, 20, 16), BackColor = SavaTheme.Surface };
            desno.Controls.Add(odobravanje);
            odobravanje.Dock = DockStyle.Fill;

            // zaglavlje sa ključnim podacima
            zaglavlje = new TableLayoutPanel { Dock = DockStyle.Top, Height = 160, ColumnCount = 4, RowCount = 3, BackColor = HRStil.Zaglavlje, Padding = new Padding(20, 12, 20, 6) };
            for (int i = 0; i < 4; i++)
                zaglavlje.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            zaglavlje.RowStyles.Add(new RowStyle(SizeType.Absolute, 54));
            zaglavlje.RowStyles.Add(new RowStyle(SizeType.Absolute, 54));
            zaglavlje.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            Controls.Add(sredina);
            Controls.Add(desno);
            Controls.Add(zaglavlje);
            naslov = HRStil.NaslovnaTraka(this, "Novi zahtev za nabavku");
            statusna = HRStil.StatusnaLinija(this);

            HRStil.PrimeniTemu(this);
            Load += (s, e) => Ucitaj();
        }

        // ------------------------------------------------------------------ opisi

        private static List<HRPolje> PoljaPodaci()
        {
            return new List<HRPolje>
            {
                HRPolje.Datum("Datum", "Datum", true),
                HRPolje.Lista("Podnosilac", "Podnosilac", HRListe.AktivniZaposleni, true),
                HRPolje.Lista("OrganizacionaJedinica", "Organizaciona jedinica", HRListe.OrganizacioneJedinice, true),
                HRPolje.Lista("MestoTroska", "Mesto troška", HRListe.Sifarnik("MestaTroska")),

                HRPolje.Lista("VrstaNabavke", "Vrsta nabavke", HRListe.Sifarnik("VrsteNabavke"), true),
                HRPolje.Lista("Prioritet", "Prioritet", "SELECT Sifra, Naziv FROM dbo.PrioritetiNabavke WHERE Aktivan = 1 ORDER BY Sifra", true),
                HRPolje.Datum("PotrebnoDo", "Potrebno do"),
                new HRPolje("Valuta", "Valuta", HRTip.Lista, true) { ListaUpit = HRListe.Valute, Podrazumevano = "RSD" },

                HRPolje.Tekst("SredstvoOpis", "Sredstvo (lokomotiva, kamion…)", 100),
                HRPolje.Tekst("RadniNalogOpis", "Radni nalog", 30),
                HRPolje.Tekst("MestoIsporuke", "Mesto isporuke", 100),
                HRPolje.Lista("SluzbenoMesto", "Službeno mesto isporuke", HRListe.SluzbenaMesta),

                new HRPolje("PredlozeniDobavljac", "Predloženi dobavljač", HRTip.Lista) { ListaUpit = HRListe.Partneri, Raspon = 2 },
                new HRPolje("Svrha", "Svrha (zašto je nabavka potrebna)", HRTip.VelikiTekst, true, 500) { Raspon = 4 },
                new HRPolje("Napomena", "Napomena", HRTip.VelikiTekst, false, 1000) { Raspon = 4 }
            };
        }

        private void PodaciPromena(string kolona)
        {
            if (kolona == "Podnosilac" && podaci.Vrednost("OrganizacionaJedinica") == null && podaci.Vrednost("Podnosilac") != null)
            {
                object oj = HRBaza.Skalar("SELECT OrganizacionaJedinica FROM dbo.vZaposleniTrenutno WHERE Zaposleni = @Z", HRBaza.P("@Z", podaci.Vrednost("Podnosilac")));
                if (oj != null)
                    podaci.Postavi("OrganizacionaJedinica", oj);
            }
            if (kolona == "Prioritet" && podaci.Vrednost("PotrebnoDo") == null && podaci.Vrednost("Prioritet") != null)
            {
                int dana = HRBaza.CeoBroj(HRBaza.Skalar("SELECT DanaZaRealizaciju FROM dbo.PrioritetiNabavke WHERE Sifra = @S", HRBaza.P("@S", podaci.Vrednost("Prioritet"))));
                DateTime? datum = HRBaza.Datum(podaci.Vrednost("Datum"));
                if (dana > 0 && datum.HasValue)
                    podaci.Postavi("PotrebnoDo", datum.Value.AddDays(dana));
            }
            if (kolona == "VrstaNabavke" && podaci.Vrednost("VrstaNabavke") != null)
            {
                bool sredstvo = Convert.ToBoolean(HRBaza.Skalar("SELECT ZahtevaSredstvo FROM dbo.VrsteNabavke WHERE Sifra = @S", HRBaza.P("@S", podaci.Vrednost("VrstaNabavke"))) ?? false);
                podaci.PostaviOznaku("SredstvoOpis", sredstvo ? "Sredstvo (lokomotiva, kamion…) *" : "Sredstvo (lokomotiva, kamion…)");
            }
        }

        private HRTabelaDef DefStavke()
        {
            return new HRTabelaDef
            {
                Naslov = "Stavka zahteva",
                Tabela = "ZahtevZaNabavkuStavke",
                RoditeljKolona = "Zahtev",
                PorukaBezRoditelja = "Prvo sačuvajte zahtev (kartica Podaci).",
                Dozvoljeno = () => id > 0 && (MenjaPodnosilac || (status == 5 && nabavka)),
                PorukaZabranjeno = "Posle podnošenja stavke se ne menjaju, osim kad je zahtev vraćen na doradu (ili nabavka menja odobren zahtev).",
                Audit = false,
                Upit = @"SELECT s.Sifra, s.RedniBroj, s.Opis, s.JedinicaMere, s.Kolicina, s.ProcenjenaCena, s.ProcenjenaVrednost,
                                st.Naziv AS StatusStavke, s.StatusStavke AS StatusStavkeSifra, s.PrimljenaKolicina, s.Napomena
                         FROM dbo.ZahtevZaNabavkuStavke s JOIN dbo.StatusiStavkiNabavke st ON st.Sifra = s.StatusStavke
                         WHERE s.Zahtev = @Roditelj ORDER BY s.RedniBroj",
                Kolone = new[]
                {
                    new HRKolona("RedniBroj", "#", 50, "broj"), new HRKolona("Opis", "Opis", 300), new HRKolona("JedinicaMere", "JM", 60),
                    new HRKolona("Kolicina", "Kol.", 80, "kolicina"), new HRKolona("ProcenjenaCena", "Cena", 110, "iznos"),
                    new HRKolona("ProcenjenaVrednost", "Vrednost", 120, "iznos"), new HRKolona("StatusStavke", "Status stavke", 140),
                    new HRKolona("PrimljenaKolicina", "Primljeno", 90, "kolicina")
                },
                Polja = new List<HRPolje>
                {
                    HRPolje.Lista("HTZOprema", "HTZ oprema (za vrstu HTZ oprema)", "SELECT Sifra, Naziv FROM dbo.HTZOprema WHERE Aktivan = 1 ORDER BY Naziv"),
                    HRPolje.Tekst("JedinicaMere", "Jedinica mere", 10, true),
                    new HRPolje("Opis", "Opis (naziv robe ili usluge, kataloški broj)", HRTip.Tekst, true, 250) { Raspon = 2 },
                    new HRPolje("Kolicina", "Količina", HRTip.Decimalni, true) { Min = 0.001m, Max = 999999999 },
                    HRPolje.Decimalni("ProcenjenaCena", "Procenjena cena"),
                    HRPolje.Napomena()
                },
                NoviPodrazumevano = () => new HRVrednosti { ["JedinicaMere"] = "kom", ["Kolicina"] = 1m },
                PriPromeni = (dlg, kolona) =>
                {
                    if (kolona != "HTZOprema" || dlg.Vrednost("HTZOprema") == null)
                        return;
                    DataRow o = HRBaza.Red("SELECT Naziv, JedinicaMere FROM dbo.HTZOprema WHERE Sifra = @S", HRBaza.P("@S", dlg.Vrednost("HTZOprema")));
                    if (o == null)
                        return;
                    if (dlg.Vrednost("Opis") == null)
                        dlg.Postavi("Opis", o["Naziv"]);
                    dlg.Postavi("JedinicaMere", o["JedinicaMere"]);
                },
                Snimi = (v, sifra) =>
                {
                    var p = new[]
                    {
                        HRBaza.P("@Zahtev", id), HRBaza.P("@Sifra", sifra), HRBaza.P("@HTZ", v.Uzmi("HTZOprema")), HRBaza.P("@Opis", v.Uzmi("Opis")),
                        HRBaza.P("@JM", v.Uzmi("JedinicaMere")), HRBaza.P("@Kolicina", v.Uzmi("Kolicina")), HRBaza.P("@Cena", v.Uzmi("ProcenjenaCena")),
                        HRBaza.P("@Napomena", v.Uzmi("Napomena")), HRBaza.P("@Korisnik", HRBaza.Korisnik)
                    };
                    HRBaza.Izvrsi(@"
                        IF @Sifra = 0
                            INSERT dbo.ZahtevZaNabavkuStavke (Zahtev, RedniBroj, HTZOprema, Opis, JedinicaMere, Kolicina, ProcenjenaCena, Napomena)
                            VALUES (@Zahtev, ISNULL((SELECT MAX(RedniBroj) FROM dbo.ZahtevZaNabavkuStavke WHERE Zahtev = @Zahtev), 0) + 1,
                                    @HTZ, @Opis, @JM, @Kolicina, @Cena, @Napomena);
                        ELSE
                            UPDATE dbo.ZahtevZaNabavkuStavke
                            SET HTZOprema = @HTZ, Opis = @Opis, JedinicaMere = @JM, Kolicina = @Kolicina, ProcenjenaCena = @Cena, Napomena = @Napomena
                            WHERE Sifra = @Sifra AND Zahtev = @Zahtev;
                        EXEC dbo.HR_ZahtevPreracunaj @Zahtev, @Korisnik;", p);
                    return null;
                },
                Obrisi = sifra =>
                {
                    HRBaza.Izvrsi("DELETE dbo.ZahtevZaNabavkuStavke WHERE Sifra = @S AND Zahtev = @Z; EXEC dbo.HR_ZahtevPreracunaj @Z, @K;",
                        HRBaza.P("@S", sifra), HRBaza.P("@Z", id), HRBaza.P("@K", HRBaza.Korisnik));
                    return null;
                },
                BojaTeksta = (r, k) =>
                {
                    if (k != "StatusStavke")
                        return null;
                    switch (HRBaza.CeoBroj(r.GetValue("StatusStavkeSifra")))
                    {
                        case 3: return HRStil.StatusIstice;
                        case 4: return HRStil.StatusVazi;
                        case 5: return HRStil.StatusIsteklo;
                        default: return null;
                    }
                }
            };
        }

        private static HRTabelaDef DefPonude()
        {
            return new HRTabelaDef
            {
                Naslov = "Ponuda",
                Tabela = "ZahtevZaNabavkuPonude",
                RoditeljKolona = "Zahtev",
                Polja = new List<HRPolje>
                {
                    new HRPolje("Partner", "Dobavljač", HRTip.Lista, true) { ListaUpit = HRListe.Partneri, Raspon = 2 },
                    HRPolje.Tekst("BrojPonude", "Broj ponude", 30),
                    HRPolje.Datum("DatumPonude", "Datum ponude", true),
                    HRPolje.Datum("VaziDo", "Važi do"),
                    new HRPolje("Iznos", "Iznos bez PDV-a", HRTip.Decimalni, true) { Min = 0 },
                    new HRPolje("Valuta", "Valuta", HRTip.Lista, true) { ListaUpit = HRListe.Valute, Podrazumevano = "RSD" },
                    HRPolje.Broj("RokIsporukeDana", "Rok isporuke (dana)", false, 0, 3650),
                    HRPolje.Tekst("UsloviPlacanja", "Uslovi plaćanja", 100),
                    HRPolje.Napomena()
                }
            };
        }

        private static HRTabelaDef DefPrilozi()
        {
            return new HRTabelaDef
            {
                Naslov = "Prilog",
                Tabela = "ZahtevZaNabavkuPrilozi",
                RoditeljKolona = "Zahtev",
                SamoCitanje = true,
                Upit = @"SELECT p.Sifra, p.Naziv, p.NazivFajla, ISNULL(N'Ponuda ' + po.BrojPonude, CASE WHEN p.Ponuda IS NOT NULL THEN N'Ponuda' END) AS Uz,
                                p.DatumKreiranja, CAST(DATALENGTH(p.Sadrzaj) / 1024 AS int) AS KB, p.KreiraoKorisnik
                         FROM dbo.ZahtevZaNabavkuPrilozi p LEFT JOIN dbo.ZahtevZaNabavkuPonude po ON po.Sifra = p.Ponuda
                         WHERE p.Zahtev = @Roditelj ORDER BY p.Sifra DESC",
                Kolone = new[]
                {
                    new HRKolona("Naziv", "Naziv", 240), new HRKolona("NazivFajla", "Fajl", 220), new HRKolona("Uz", "Uz", 120),
                    new HRKolona("DatumKreiranja", "Datum", 120, "datum"), new HRKolona("KB", "KB", 70, "broj"), new HRKolona("KreiraoKorisnik", "Priložio", 120)
                },
                Napomena = "Ponude u PDF-u, specifikacije, fotografije neispravnog dela. Dupli klik otvara prilog."
            };
        }

        private static HRTabelaDef DefIstorija()
        {
            return new HRTabelaDef
            {
                Naslov = "Istorija",
                Tabela = "ZahtevZaNabavkuIstorijaStatusa",
                RoditeljKolona = "Zahtev",
                SamoCitanje = true,
                Upit = @"SELECT i.Sifra, i.DatumVreme, so.Naziv AS Od, sd.Naziv AS Do, i.Korisnik, i.Komentar
                         FROM dbo.ZahtevZaNabavkuIstorijaStatusa i
                         LEFT JOIN dbo.StatusiZahtevaZaNabavku so ON so.Sifra = i.StatusOd
                         JOIN dbo.StatusiZahtevaZaNabavku sd ON sd.Sifra = i.StatusDo
                         WHERE i.Zahtev = @Roditelj ORDER BY i.DatumVreme DESC, i.Sifra DESC",
                Kolone = new[]
                {
                    new HRKolona("DatumVreme", "Vreme", 140, "datumvreme"), new HRKolona("Od", "Iz statusa", 160), new HRKolona("Do", "U status", 160),
                    new HRKolona("Korisnik", "Korisnik", 120), new HRKolona("Komentar", "Komentar", 320)
                }
            };
        }

        private static HRTabelaDef DefOdluke()
        {
            return new HRTabelaDef
            {
                Naslov = "Odluke",
                Tabela = "ZahtevZaNabavkuOdobrenja",
                RoditeljKolona = "Zahtev",
                SamoCitanje = true,
                Upit = @"SELECT o.Sifra, o.Nivo, LTRIM(RTRIM(ISNULL(z.Prezime, N'') + N' ' + ISNULL(z.Ime, N''))) AS Odobravalac,
                                CASE o.Odluka WHEN 'O' THEN N'Odobreno' WHEN 'V' THEN N'Vraćeno na doradu' WHEN 'D' THEN N'Odbijeno' ELSE N'Čeka odluku' END AS Odluka,
                                o.DatumZadavanja, o.DatumOdluke, o.Komentar
                         FROM dbo.ZahtevZaNabavkuOdobrenja o JOIN dbo.Zaposleni z ON z.ID = o.Odobravalac
                         WHERE o.Zahtev = @Roditelj ORDER BY o.Sifra DESC",
                Kolone = new[]
                {
                    new HRKolona("Nivo", "Nivo", 60, "broj"), new HRKolona("Odobravalac", "Odobravalac", 180), new HRKolona("Odluka", "Odluka", 140),
                    new HRKolona("DatumZadavanja", "Stiglo", 140, "datumvreme"), new HRKolona("DatumOdluke", "Odlučeno", 140, "datumvreme"),
                    new HRKolona("Komentar", "Komentar", 280)
                }
            };
        }

        // ------------------------------------------------------------------ učitavanje i stanje

        private void Ucitaj()
        {
            ja = HRPrava.TrenutniZaposleni();
            if (id == 0)
            {
                zahtev = null;
                status = 0;
                cekaMene = false;
                podaci.PostaviSve(new HRVrednosti
                {
                    ["Datum"] = DateTime.Today,
                    ["Podnosilac"] = ja > 0 ? (object)ja : null,
                    ["Valuta"] = "RSD",
                    ["Prioritet"] = HRBaza.Skalar("SELECT TOP (1) Sifra FROM dbo.PrioritetiNabavke WHERE Naziv = N'Redovno'")
                });
                if (ja > 0)
                    podaci.Postavi("OrganizacionaJedinica", HRBaza.Skalar("SELECT OrganizacionaJedinica FROM dbo.vZaposleniTrenutno WHERE Zaposleni = @Z", HRBaza.P("@Z", ja)));
                kartice.Izaberi("Podaci");
            }
            else
            {
                try
                {
                    zahtev = HRBaza.Red(@"
                        SELECT z.*, LTRIM(RTRIM(ISNULL(p.Prezime, N'') + N' ' + ISNULL(p.Ime, N''))) AS PodnosilacNaziv, j.Naziv AS JedinicaNaziv,
                               v.Naziv AS VrstaNaziv, pr.Naziv AS PrioritetNaziv, s.Naziv AS StatusNaziv, RTRIM(st.Opis) AS SluzbenoMestoNaziv,
                               CASE WHEN EXISTS (SELECT 1 FROM dbo.ZahtevZaNabavkuOdobrenja o WHERE o.Zahtev = z.Sifra AND o.Odluka IS NULL AND o.Odobravalac = @Ja) THEN 1 ELSE 0 END AS CekaMene
                        FROM dbo.ZahteviZaNabavku z
                        JOIN dbo.Zaposleni p ON p.ID = z.Podnosilac
                        LEFT JOIN dbo.OrganizacioneJedinice j ON j.ID = z.OrganizacionaJedinica
                        JOIN dbo.VrsteNabavke v ON v.Sifra = z.VrstaNabavke
                        JOIN dbo.PrioritetiNabavke pr ON pr.Sifra = z.Prioritet
                        JOIN dbo.StatusiZahtevaZaNabavku s ON s.Sifra = z.Status
                        LEFT JOIN dbo.Stanice st ON st.ID = z.SluzbenoMesto
                        WHERE z.Sifra = @Z", HRBaza.P("@Z", id), HRBaza.P("@Ja", ja));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Zahtev nije učitan. Proverite da li je na bazi pokrenuta skripta HR_Modul_2026_10.sql.\n\n" + ex.Message,
                        "Zahtev za nabavku", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (zahtev == null)
                    return;
                status = HRBaza.CeoBroj(zahtev["Status"]);
                cekaMene = HRBaza.CeoBroj(zahtev["CekaMene"]) == 1;
                podaci.PostaviSve(new HRVrednosti(HRBaza.RedUMapu(zahtev)));
                if (prvoUcitavanje)
                    kartice.Izaberi("Stavke");
            }

            stavke.Ucitaj(id);
            prilozi.Ucitaj(id);
            istorija.Ucitaj(id);
            odluke.Ucitaj(id);
            UcitajPonude();
            PrikaziZaglavlje();
            PrikaziOdobravanje();
            PostaviKomande();
            OsveziUkupno();
            prvoUcitavanje = false;
        }

        private bool MenjaPodnosilac => id == 0 || status == 1 || status == 4;

        private void PostaviKomande()
        {
            naslov.ObrisiDugmad();
            if (id > 0)
            {
                naslov.DodajDugme("Kopiraj", false, (s, e) => Kopiraj());
                naslov.DodajDugme("Štampa", false, (s, e) => HRStampa.Zahtev(this, id));
            }

            if (MenjaPodnosilac)
            {
                if (id > 0)
                {
                    naslov.DodajDugme("Storniraj", false, (s, e) => Storniraj());
                    naslov.DodajDugme("Podnesi", false, (s, e) => Podnesi());
                }
                naslov.DodajDugme("Sačuvaj", true, (s, e) => Sacuvaj());
            }
            else if (status == 3 && cekaMene)
            {
                naslov.DodajDugme("Odbij", false, (s, e) => Odluka("D"));
                naslov.DodajDugme("Vrati na doradu", false, (s, e) => Odluka("V"));
                naslov.DodajDugme("Odobri", true, (s, e) => Odluka("O"));
            }
            else if (status == 5 && nabavka)
            {
                naslov.DodajDugme("Storniraj", false, (s, e) => Storniraj());
                naslov.DodajDugme("Pošalji u nabavku", true, (s, e) => UNabavku());
            }
            else if ((status == 6 || status == 7) && nabavka)
                naslov.DodajDugme("Storniraj", false, (s, e) => Storniraj());

            foreach (HRPolje p in podaci.Polja)
                podaci.Omoguci(p.Kolona, MenjaPodnosilac);

            bool stavkeMenja = id > 0 && (MenjaPodnosilac || (status == 5 && nabavka));
            foreach (Control c in stavke.Komande.Controls)
                c.Visible = c.Text == "Prijem robe" ? (status == 6 || status == 7) && nabavka : stavkeMenja;
            bool ponudeMenja = id > 0 && status != 8 && status != 9 && status != 10 && (nabavka || MenjaPodnosilac);
            ponudeKomande.Visible = ponudeMenja;
            prilozi.Komande.Visible = id > 0;
        }

        private void PrikaziZaglavlje()
        {
            zaglavlje.Controls.Clear();
            if (zahtev == null)
            {
                naslov.Naslov.Text = "Novi zahtev za nabavku";
                Text = "Novi zahtev";
                var t = new HRTekst { Text = "Popunite podatke i sačuvajte zahtev; broj zahteva se dodeljuje automatski (ZN-godina/broj). Zatim dodajte stavke i podnesite zahtev.", Dock = DockStyle.Fill, ForeColor = SavaTheme.Label };
                zaglavlje.Controls.Add(t, 0, 0);
                zaglavlje.SetColumnSpan(t, 4);
                statusna.Text = "* obavezno polje";
                return;
            }
            naslov.Naslov.Text = "Zahtev " + zahtev["BrojZahteva"];
            Text = Convert.ToString(zahtev["BrojZahteva"]);
            string statusTekst = Convert.ToString(zahtev["StatusNaziv"]) + NivoTekst();
            Polje(0, 0, "Podnosilac", Convert.ToString(zahtev["PodnosilacNaziv"]));
            Polje(1, 0, "Org. jedinica", Convert.ToString(zahtev["JedinicaNaziv"]));
            Polje(2, 0, "Vrsta / prioritet", zahtev["VrstaNaziv"] + " · " + zahtev["PrioritetNaziv"]);
            Polje(3, 0, "Status", statusTekst, HRGrid.BojaStatusaZahteva(status));
            Polje(0, 1, "Sredstvo", Prazno(zahtev["SredstvoOpis"]));
            Polje(1, 1, "Radni nalog", Prazno(zahtev["RadniNalogOpis"]));
            Polje(2, 1, "Potrebno do", zahtev["PotrebnoDo"] == DBNull.Value ? "–" : HRBaza.DatumTekst(zahtev["PotrebnoDo"]) + ".");
            Polje(3, 1, "Isporuka", Prazno(zahtev["MestoIsporuke"] != DBNull.Value ? zahtev["MestoIsporuke"] : zahtev["SluzbenoMestoNaziv"]));
            var svrha = new HRTekst { Text = "Svrha: " + zahtev["Svrha"], Dock = DockStyle.Fill, ForeColor = SavaTheme.Label, AutoSirina = false };
            zaglavlje.Controls.Add(svrha, 0, 2);
            zaglavlje.SetColumnSpan(svrha, 4);

            int nivoa = 0;
            try { nivoa = HRBaza.CeoBroj(HRBaza.Skalar("SELECT COUNT(*) FROM dbo.fnHR_ZahtevPlanOdobravanja(@Z) WHERE Preskocen = 0", HRBaza.P("@Z", id))); } catch { }
            decimal iznos = HRBaza.Decimalni(zahtev["ProcenjenaVrednost"]);
            statusna.Text = "Iznos " + HRBaza.Iznos(iznos) + " " + zahtev["Valuta"] + ": po pravilima " +
                            (nivoa == 0 ? "nema nivoa odobravanja" : "potrebno je " + nivoa + (nivoa == 1 ? " nivo" : nivoa < 5 ? " nivoa" : " nivoa") + " odobravanja") +
                            " · Kreirao: " + zahtev["KreiraoKorisnik"] + " " + Convert.ToDateTime(zahtev["DatumKreiranja"]).ToString("dd.MM.yyyy") +
                            (zahtev["IzmenioKorisnik"] != DBNull.Value ? " · Izmenio: " + zahtev["IzmenioKorisnik"] + " " + Convert.ToDateTime(zahtev["DatumIzmene"]).ToString("dd.MM.yyyy") : "");
        }

        private static string Prazno(object v)
        {
            string s = v == null || v == DBNull.Value ? "" : Convert.ToString(v).Trim();
            return s == "" ? "–" : s;
        }

        private string NivoTekst()
        {
            if (status != 3)
                return "";
            try
            {
                DataRow n = HRBaza.Red(@"SELECT SUM(CASE WHEN Nivo <= @Nivo THEN 1 ELSE 0 END) AS Trenutni, COUNT(*) AS Ukupno
                                         FROM dbo.fnHR_ZahtevPlanOdobravanja(@Z) WHERE Preskocen = 0",
                    HRBaza.P("@Z", id), HRBaza.P("@Nivo", zahtev["TrenutniNivoOdobravanja"]));
                return n == null ? "" : " (" + n["Trenutni"] + "/" + n["Ukupno"] + ")";
            }
            catch
            {
                return "";
            }
        }

        private void Polje(int kolona, int red, string oznaka, string vrednost, Color? boja = null)
        {
            var p = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent, Margin = new Padding(0) };
            var v = HRStil.Vrednost(vrednost);
            v.Dock = DockStyle.Top;
            v.Height = 24;
            if (boja.HasValue)
                v.ForeColor = boja.Value;
            var o = new HRTekst { Text = oznaka, Dock = DockStyle.Top, Height = 22, Font = SavaTheme.LabelFont, ForeColor = SavaTheme.Label };
            p.Controls.Add(v);
            p.Controls.Add(o);
            zaglavlje.Controls.Add(p, kolona, red);
        }

        // Panel Odobravanje: plan po pravilima i odluke tekućeg kruga
        private void PrikaziOdobravanje()
        {
            var stavkeToka = new List<HROdobravanjePanel.Stavka>();
            if (id == 0)
            {
                odobravanje.Postavi(stavkeToka, "Tok odobravanja se određuje kad se zahtev sačuva i doda stavke.");
                return;
            }
            DataTable tok;
            try
            {
                tok = HRBaza.ProceduraTabela("dbo.HR_ZahtevTokOdobravanja", HRBaza.P("@Zahtev", id));
            }
            catch (Exception ex)
            {
                odobravanje.Postavi(stavkeToka, ex.Message);
                return;
            }
            int prethodni = 0;
            bool bilaCeka = false;
            foreach (DataRow r in tok.Rows)
            {
                int nivo = HRBaza.CeoBroj(r["Nivo"]);
                string ime = Convert.ToString(r["OdobravalacNaziv"]);
                string odluka = Convert.ToString(r["Odluka"]);
                string datum = r["DatumOdluke"] == DBNull.Value ? "" : " " + Convert.ToDateTime(r["DatumOdluke"]).ToString("dd.MM.");
                var s = new HROdobravanjePanel.Stavka { Naslov = nivo + " · " + r["Opis"] };
                if (Convert.ToBoolean(r["Preskocen"]))
                {
                    s.Opis = "preskočen (odobravalac je podnosilac, a nema zamenika)";
                    s.Boja = SavaTheme.Label;
                }
                else if (odluka == "O")
                {
                    s.Opis = ime + " · odobreno" + datum;
                    s.Boja = HRStil.StatusVazi;
                }
                else if (odluka == "V" || odluka == "D")
                {
                    s.Opis = ime + (odluka == "V" ? " · vraćeno na doradu" : " · odbijeno") + datum;
                    s.Boja = SavaTheme.Danger;
                }
                else if (Convert.ToBoolean(r["Ceka"]))
                {
                    s.Opis = ime + " · čeka odluku";
                    s.Boja = HRStil.StatusIstice;
                    bilaCeka = true;
                }
                else
                {
                    s.Opis = status == 3 && bilaCeka ? "Na redu posle nivoa " + prethodni : (ime == "" ? "odobravalac nije određen" : ime);
                    s.Boja = SavaTheme.Label;
                }
                stavkeToka.Add(s);
                prethodni = nivo;
            }
            odobravanje.Postavi(stavkeToka, stavkeToka.Count == 0
                ? "Nijedno pravilo odobravanja ne odgovara ovom zahtevu (vrsta, jedinica, iznos). Pravila se podešavaju u HR modulu, kartica Pravila odobravanja."
                : null);
        }

        private void OsveziUkupno()
        {
            if (stavke.Podaci == null || stavke.Podaci.Rows.Count == 0)
            {
                stavke.PostaviNapomenu(id == 0 ? "Stavke se dodaju posle snimanja zahteva." : "Zahtev nema stavki.");
                return;
            }
            decimal ukupno = stavke.Podaci.Rows.Cast<DataRow>().Sum(r => HRBaza.Decimalni(r["ProcenjenaVrednost"]));
            stavke.PostaviNapomenu("Ukupno (" + (zahtev?["Valuta"] ?? "RSD") + "): " + HRBaza.Iznos(ukupno), SavaTheme.Text);
        }

        // ------------------------------------------------------------------ akcije

        private string Proveri(HRVrednosti v)
        {
            object vrsta = v.Uzmi("VrstaNabavke");
            if (vrsta != null && Convert.ToBoolean(HRBaza.Skalar("SELECT ZahtevaSredstvo FROM dbo.VrsteNabavke WHERE Sifra = @S", HRBaza.P("@S", vrsta)) ?? false)
                && v.Tekst("SredstvoOpis") == "" && v.Tekst("RadniNalogOpis") == "")
                return "Za ovu vrstu nabavke unesite sredstvo (ili radni nalog).";
            if (v.Datum("PotrebnoDo").HasValue && v.Datum("PotrebnoDo") < v.Datum("Datum"))
                return "Potrebno do ne može biti pre datuma zahteva.";
            return null;
        }

        private bool Sacuvaj()
        {
            string greska = podaci.Ocitaj(out HRVrednosti v) ?? Proveri(v);
            if (greska != null)
            {
                kartice.Izaberi("Podaci");
                MessageBox.Show(greska, "Zahtev za nabavku", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            try
            {
                bool nov = id == 0;
                if (nov)
                    id = NoviZahtev(v);
                var def = new HRTabelaDef { Tabela = "ZahteviZaNabavku", Polja = podaci.Polja.ToList() };
                HRTabelaKartica.SnimiOpste(def, 0, v, id);
                if (nov)
                    kartice.Izaberi("Stavke");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Zahtev za nabavku", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            Ucitaj();
            return true;
        }

        private static int NoviZahtev(HRVrednosti v)
        {
            var sifra = HRBaza.Izlaz("@Sifra", SqlDbType.Int);
            var broj = HRBaza.Izlaz("@BrojZahteva", SqlDbType.NVarChar, 20);
            HRBaza.Procedura("dbo.HR_ZahtevNovi",
                HRBaza.P("@Datum", v.Uzmi("Datum")), HRBaza.P("@Podnosilac", v.Uzmi("Podnosilac")),
                HRBaza.P("@OrganizacionaJedinica", v.Uzmi("OrganizacionaJedinica")), HRBaza.P("@VrstaNabavke", v.Uzmi("VrstaNabavke")),
                HRBaza.P("@Prioritet", v.Uzmi("Prioritet")), HRBaza.P("@Svrha", v.Uzmi("Svrha")), HRBaza.P("@Korisnik", HRBaza.Korisnik), sifra, broj);
            return HRBaza.CeoBroj(sifra.Value);
        }

        private void Podnesi()
        {
            if (!Sacuvaj())
                return;
            try
            {
                HRBaza.Procedura("dbo.HR_ZahtevPodnesi", HRBaza.P("@Zahtev", id), HRBaza.P("@Korisnik", HRBaza.Korisnik), HRBaza.P("@Komentar", DBNull.Value));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Podnošenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Ucitaj();
        }

        private void Odluka(string odluka)
        {
            string naslovOdluke = odluka == "O" ? "Odobravanje" : odluka == "V" ? "Vraćanje na doradu" : "Odbijanje";
            string komentar = HRPrilozi.Unos(this, naslovOdluke + ": " + zahtev["BrojZahteva"],
                odluka == "O" ? "Komentar (nije obavezan)" : "Komentar (obavezan)", "", 1000, true, odluka != "O");
            if (komentar == null)
                return;
            try
            {
                HRBaza.Procedura("dbo.HR_ZahtevOdluka", HRBaza.P("@Zahtev", id), HRBaza.P("@Odobravalac", ja), HRBaza.P("@Odluka", odluka),
                    HRBaza.P("@Komentar", HRBaza.Tekst(komentar)), HRBaza.P("@Korisnik", HRBaza.Korisnik));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, naslovOdluke, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Ucitaj();
        }

        private void Storniraj()
        {
            string komentar = HRPrilozi.Unos(this, "Storniranje: " + zahtev["BrojZahteva"], "Razlog storniranja", "", 500, true, false);
            if (komentar == null)
                return;
            try
            {
                HRBaza.Procedura("dbo.HR_ZahtevStorniraj", HRBaza.P("@Zahtev", id), HRBaza.P("@Korisnik", HRBaza.Korisnik), HRBaza.P("@Komentar", HRBaza.Tekst(komentar)));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Storniranje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Ucitaj();
        }

        private void UNabavku()
        {
            try
            {
                HRBaza.Procedura("dbo.HR_ZahtevUNabavku", HRBaza.P("@Zahtev", id), HRBaza.P("@Korisnik", HRBaza.Korisnik));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "U nabavku", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Ucitaj();
        }

        private void Prijem()
        {
            int stavka = stavke.IzabraniId();
            if (stavka <= 0)
            {
                MessageBox.Show("Izaberite stavku.", "Prijem robe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            decimal preostalo = HRBaza.Decimalni(HRGrid.Vrednost(stavke.Grid, "Kolicina")) - HRBaza.Decimalni(HRGrid.Vrednost(stavke.Grid, "PrimljenaKolicina"));
            var polja = new[]
            {
                new HRPolje("Kolicina", "Primljena količina", HRTip.Decimalni, true) { Min = 0 },
                HRPolje.DaNe("Odbijena", "Stavka je odbijena (neće biti isporučena)")
            };
            using (var dlg = new frmHRStavka("Prijem: " + HRGrid.Vrednost(stavke.Grid, "Opis"), polja, new HRVrednosti { ["Kolicina"] = Math.Max(0, preostalo) }, 1))
            {
                dlg.Snimanje = v =>
                {
                    HRBaza.Procedura("dbo.HR_StavkaPrijem", HRBaza.P("@Stavka", stavka), HRBaza.P("@Kolicina", v.Uzmi("Kolicina")),
                        HRBaza.P("@Odbijena", v.Uzmi("Odbijena")), HRBaza.P("@Korisnik", HRBaza.Korisnik));
                    return null;
                };
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    Ucitaj();
            }
        }

        // Kopiranje postojećeg zahteva kao šablona (7.1): novi nacrt sa istim podacima i stavkama
        private void Kopiraj()
        {
            if (MessageBox.Show("Napraviti novi zahtev (nacrt) sa istim podacima i stavkama?", "Kopiranje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            try
            {
                var v = new HRVrednosti(HRBaza.RedUMapu(zahtev));
                v["Datum"] = DateTime.Today;
                if (ja > 0)
                    v["Podnosilac"] = ja;
                v["PotrebnoDo"] = DBNull.Value;
                int novi = NoviZahtev(v);
                var def = new HRTabelaDef { Tabela = "ZahteviZaNabavku", Polja = podaci.Polja.ToList() };
                HRTabelaKartica.SnimiOpste(def, 0, v, novi);
                HRBaza.Izvrsi(@"
                    INSERT dbo.ZahtevZaNabavkuStavke (Zahtev, RedniBroj, Artikal, HTZOprema, Opis, JedinicaMere, Kolicina, ProcenjenaCena, Sredstvo, Napomena)
                    SELECT @Novi, RedniBroj, Artikal, HTZOprema, Opis, JedinicaMere, Kolicina, ProcenjenaCena, Sredstvo, Napomena
                    FROM dbo.ZahtevZaNabavkuStavke WHERE Zahtev = @Stari;
                    EXEC dbo.HR_ZahtevPreracunaj @Novi, @K;", HRBaza.P("@Novi", novi), HRBaza.P("@Stari", id), HRBaza.P("@K", HRBaza.Korisnik));
                HRStil.Otvori(this, new frmHRZahtev(novi));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Kopiranje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ObrisiPrilog()
        {
            int p = prilozi.IzabraniId();
            if (p <= 0 || MessageBox.Show("Obrisati izabrani prilog?", "Prilog", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            HRBaza.Izvrsi("DELETE dbo.ZahtevZaNabavkuPrilozi WHERE Sifra = @S", HRBaza.P("@S", p));
            prilozi.Osvezi();
        }

        // ------------------------------------------------------------------ ponude (strana 10)

        private void PodesiKomandePonuda()
        {
            Button Dugme(string tekst, EventHandler klik, string uloga = null)
            {
                var b = HRStil.Dugme(tekst, uloga);
                b.Margin = new Padding(8, 0, 0, 0);
                b.Click += klik;
                ponudeKomande.Controls.Add(b);
                return b;
            }
            Dugme("Obriši", (s, e) => ObrisiPonudu(), "danger");
            Dugme("Otvori PDF", (s, e) => OtvoriPdfPonude());
            Dugme("Priloži PDF", (s, e) => PriloziPdfPonude());
            Dugme("Izaberi ponudu", (s, e) => IzaberiPonudu());
            Dugme("Izmeni", (s, e) => UrediPonudu(izabranaPonuda));
            Dugme("+ Dodaj ponudu", (s, e) => UrediPonudu(0));
        }

        private void UcitajPonude()
        {
            ponude.SuspendLayout();
            foreach (Control c in ponude.Controls.Cast<Control>().ToList())
                c.Dispose();
            ponude.Controls.Clear();
            ponudeUpozorenje.Visible = false;
            if (id == 0)
            {
                ponude.ResumeLayout();
                return;
            }
            DataTable dt = HRBaza.Tabela(@"
                SELECT p.*, RTRIM(pa.PaNaziv) AS PartnerNaziv,
                       CASE WHEN p.Iznos = (SELECT MIN(x.Iznos) FROM dbo.ZahtevZaNabavkuPonude x WHERE x.Zahtev = p.Zahtev AND x.Valuta = p.Valuta) THEN 1 ELSE 0 END AS Najjeftinija,
                       (SELECT COUNT(*) FROM dbo.ZahtevZaNabavkuPrilozi pr WHERE pr.Ponuda = p.Sifra) AS Pdf
                FROM dbo.ZahtevZaNabavkuPonude p LEFT JOIN dbo.Partnerji pa ON pa.PaSifra = p.Partner
                WHERE p.Zahtev = @Z ORDER BY p.Iznos", HRBaza.P("@Z", id));
            if (dt.Rows.Count < 2)
                foreach (DataRow r in dt.Rows)
                    r["Najjeftinija"] = 0;
            if (!dt.Rows.Cast<DataRow>().Any(r => HRBaza.CeoBroj(r["Sifra"]) == izabranaPonuda))
                izabranaPonuda = dt.Rows.Count > 0 ? HRBaza.CeoBroj(dt.Rows[0]["Sifra"]) : 0;
            foreach (DataRow r in dt.Rows)
            {
                var k = new HRPonudaKartica(r) { Oznacena = HRBaza.CeoBroj(r["Sifra"]) == izabranaPonuda, Margin = new Padding(0, 0, 16, 16) };
                k.Click += (s, e) =>
                {
                    izabranaPonuda = k.Sifra;
                    foreach (HRPonudaKartica x in ponude.Controls.OfType<HRPonudaKartica>())
                    {
                        x.Oznacena = x.Sifra == izabranaPonuda;
                        x.Invalidate();
                    }
                };
                k.DoubleClick += (s, e) => UrediPonudu(k.Sifra);
                ponude.Controls.Add(k);
            }
            if (dt.Rows.Count == 0)
                ponude.Controls.Add(new Label { Text = "Nema unetih ponuda.", AutoSize = true, Font = SavaTheme.FieldFont, ForeColor = SavaTheme.Label, Margin = new Padding(0, 8, 0, 0) });

            DataRow izabrana = dt.Rows.Cast<DataRow>().FirstOrDefault(r => Convert.ToBoolean(r["Izabrana"]));
            if (izabrana != null && dt.Rows.Count > 1 && HRBaza.CeoBroj(izabrana["Najjeftinija"]) == 0)
            {
                ponudeUpozorenjeTekst.Text = "⚠  Izabrana ponuda nije najjeftinija, pa sistem traži obrazloženje pre slanja u nabavku" +
                                             (izabrana["RazlogIzbora"] == DBNull.Value ? "" : ": " + izabrana["RazlogIzbora"]);
                ponudeUpozorenje.Visible = true;
            }
            ponude.ResumeLayout();
        }

        private void UrediPonudu(int sifra)
        {
            if (id == 0 || !ponudeKomande.Visible)
                return;
            HRTabelaDef def = DefPonude();
            HRVrednosti pocetne;
            if (sifra > 0)
                pocetne = new HRVrednosti(HRBaza.RedUMapu(HRBaza.Red("SELECT * FROM dbo.ZahtevZaNabavkuPonude WHERE Sifra = @S", HRBaza.P("@S", sifra))));
            else
                pocetne = new HRVrednosti { ["DatumPonude"] = DateTime.Today, ["Valuta"] = zahtev?["Valuta"] ?? "RSD" };
            using (var dlg = new frmHRStavka(sifra == 0 ? "Nova ponuda" : "Izmena ponude", def.Polja, pocetne))
            {
                dlg.Snimanje = v =>
                {
                    int nova = HRTabelaKartica.SnimiOpste(def, id, v, sifra);
                    izabranaPonuda = nova;
                    return null;
                };
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    UcitajPonude();
            }
        }

        private void IzaberiPonudu()
        {
            if (izabranaPonuda <= 0)
                return;
            DataRow p = HRBaza.Red(@"SELECT Iznos, Valuta, RazlogIzbora,
                                            CASE WHEN EXISTS (SELECT 1 FROM dbo.ZahtevZaNabavkuPonude x WHERE x.Zahtev = p.Zahtev AND x.Valuta = p.Valuta AND x.Iznos < p.Iznos) THEN 0 ELSE 1 END AS Najjeftinija
                                     FROM dbo.ZahtevZaNabavkuPonude p WHERE p.Sifra = @S", HRBaza.P("@S", izabranaPonuda));
            if (p == null)
                return;
            string razlog = null;
            if (HRBaza.CeoBroj(p["Najjeftinija"]) == 0)
            {
                razlog = HRPrilozi.Unos(this, "Izbor ponude", "Obrazloženje (ponuda nije najjeftinija)", p["RazlogIzbora"] as string ?? "", 500, true, true);
                if (razlog == null)
                    return;
            }
            try
            {
                HRBaza.Procedura("dbo.HR_PonudaIzaberi", HRBaza.P("@Ponuda", izabranaPonuda), HRBaza.P("@RazlogIzbora", HRBaza.Tekst(razlog)), HRBaza.P("@Korisnik", HRBaza.Korisnik));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Izbor ponude", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Ucitaj();
            kartice.Izaberi("Ponude");
        }

        private void PriloziPdfPonude()
        {
            if (izabranaPonuda <= 0)
                return;
            object broj = HRBaza.Skalar("SELECT BrojPonude FROM dbo.ZahtevZaNabavkuPonude WHERE Sifra = @S", HRBaza.P("@S", izabranaPonuda));
            if (HRPrilozi.Dodaj(this, "ZahtevZaNabavkuPrilozi", "Zahtev", id, "Ponuda " + (broj ?? ""), new KeyValuePair<string, object>("Ponuda", izabranaPonuda)))
            {
                UcitajPonude();
                prilozi.Osvezi();
            }
        }

        private void OtvoriPdfPonude()
        {
            if (izabranaPonuda <= 0)
                return;
            object prilog = HRBaza.Skalar("SELECT TOP (1) Sifra FROM dbo.ZahtevZaNabavkuPrilozi WHERE Ponuda = @S ORDER BY Sifra DESC", HRBaza.P("@S", izabranaPonuda));
            if (prilog == null)
                MessageBox.Show("Uz izabranu ponudu nema priloga.", "Ponuda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                HRPrilozi.Otvori("ZahtevZaNabavkuPrilozi", HRBaza.CeoBroj(prilog));
        }

        private void ObrisiPonudu()
        {
            if (izabranaPonuda <= 0 || MessageBox.Show("Obrisati izabranu ponudu i njene priloge?", "Ponuda", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            HRBaza.Izvrsi("DELETE dbo.ZahtevZaNabavkuPrilozi WHERE Ponuda = @S; DELETE dbo.ZahtevZaNabavkuPonude WHERE Sifra = @S;", HRBaza.P("@S", izabranaPonuda));
            izabranaPonuda = 0;
            UcitajPonude();
            prilozi.Osvezi();
        }
    }

    // Kartica ponude: dobavljač, iznos, rok isporuke, plaćanje; oznake Izabrana i Najjeftinija
    public class HRPonudaKartica : Control
    {
        public readonly int Sifra;
        public bool Oznacena;
        private readonly string zaglavlje, partner, iznos, valuta, rok, placanje, obrazlozenje;
        private readonly bool izabrana, najjeftinija;

        public HRPonudaKartica(DataRow r)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            Size = new Size(330, 300);
            Cursor = Cursors.Hand;
            Sifra = HRBaza.CeoBroj(r["Sifra"]);
            zaglavlje = "Ponuda " + (r["BrojPonude"] == DBNull.Value ? "bez broja" : Convert.ToString(r["BrojPonude"]))
                        + (r["VaziDo"] == DBNull.Value ? "" : " · važi do " + Convert.ToDateTime(r["VaziDo"]).ToString("dd.MM."))
                        + (HRBaza.CeoBroj(r["Pdf"]) > 0 ? " · PDF" : "");
            partner = r["PartnerNaziv"] == DBNull.Value ? "Partner " + r["Partner"] : Convert.ToString(r["PartnerNaziv"]);
            iznos = HRBaza.Iznos(HRBaza.Decimalni(r["Iznos"]));
            valuta = Convert.ToString(r["Valuta"]).Trim() + " bez PDV-a";
            rok = r["RokIsporukeDana"] == DBNull.Value ? "–" : r["RokIsporukeDana"] + " " + HRStil.Dana(HRBaza.CeoBroj(r["RokIsporukeDana"]));
            placanje = r["UsloviPlacanja"] == DBNull.Value ? "–" : Convert.ToString(r["UsloviPlacanja"]);
            izabrana = Convert.ToBoolean(r["Izabrana"]);
            najjeftinija = HRBaza.CeoBroj(r["Najjeftinija"]) == 1;
            obrazlozenje = r["RazlogIzbora"] == DBNull.Value ? null : Convert.ToString(r["RazlogIzbora"]);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(SavaTheme.Surface);
            var r = new Rectangle(1, 1, Width - 3, Height - 3);
            using (var put = HRStil.Zaobljen(r, 8))
            {
                using (var b = new SolidBrush(izabrana ? ColorTranslator.FromHtml("#F3F7FB") : SavaTheme.White))
                    g.FillPath(b, put);
                using (var o = new Pen(Oznacena || izabrana ? SavaTheme.Primary : SavaTheme.Line, Oznacena ? 2.5f : 1f))
                    g.DrawPath(o, put);
            }
            int x = 20, y = 18, w = Width - 40;
            TextRenderer.DrawText(g, zaglavlje, SavaTheme.LabelFont, new Rectangle(x, y, w - 100, 20), SavaTheme.Label, TextFormatFlags.EndEllipsis);
            if (izabrana)
                Bedz(g, "Izabrana", SavaTheme.Primary, SavaTheme.White, y - 2);
            else if (najjeftinija)
                Bedz(g, "Najjeftinija", HRStil.PozadinaVazi, HRStil.StatusVazi, y - 2);
            if (izabrana && najjeftinija)
                Bedz(g, "Najjeftinija", HRStil.PozadinaVazi, HRStil.StatusVazi, y + 26);
            y += 30;
            TextRenderer.DrawText(g, partner, SavaTheme.FormTitleFont, new Rectangle(x, y, w, 28), SavaTheme.Text, TextFormatFlags.EndEllipsis);
            y += 34;
            TextRenderer.DrawText(g, iznos, HRStil.VelikiBroj, new Point(x - 4, y), SavaTheme.Text);
            y += 50;
            TextRenderer.DrawText(g, valuta, SavaTheme.FieldFont, new Point(x, y), SavaTheme.Label);
            y += 30;
            using (var p = new Pen(SavaTheme.Line))
                g.DrawLine(p, x, y, x + w, y);
            y += 12;
            TextRenderer.DrawText(g, "Rok isporuke: " + rok, SavaTheme.FieldFont, new Point(x, y), SavaTheme.Text);
            y += 26;
            TextRenderer.DrawText(g, "Plaćanje: " + placanje, SavaTheme.FieldFont, new Rectangle(x, y, w, 22), SavaTheme.Text, TextFormatFlags.EndEllipsis);
            y += 30;
            if (!string.IsNullOrEmpty(obrazlozenje))
            {
                var okvir = new Rectangle(x, y, w, Height - y - 16);
                g.FillRectangle(Brushes.White, okvir);
                TextRenderer.DrawText(g, "Obrazloženje: " + obrazlozenje, SavaTheme.FieldFont, Rectangle.Inflate(okvir, -8, -6), SavaTheme.Text,
                    TextFormatFlags.WordBreak | TextFormatFlags.EndEllipsis);
            }
        }

        private void Bedz(Graphics g, string tekst, Color pozadina, Color boja, int y)
        {
            Size s = TextRenderer.MeasureText(tekst, SavaTheme.ButtonFontSemibold);
            var r = new Rectangle(Width - 20 - s.Width - 16, y, s.Width + 16, 24);
            using (var put = HRStil.Zaobljen(r, 11))
            using (var b = new SolidBrush(pozadina))
                g.FillPath(b, put);
            TextRenderer.DrawText(g, tekst, SavaTheme.ButtonFontSemibold, r, boja, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }

    // Panel "Odobravanje": nivoi sa obojenom linijom levo (zelena odobreno, narandžasta čeka, siva na redu, crvena vraćeno / odbijeno)
    public class HROdobravanjePanel : Control
    {
        public class Stavka
        {
            public string Naslov;
            public string Opis;
            public Color Boja;
        }

        private List<Stavka> stavke = new List<Stavka>();
        private string poruka;

        public HROdobravanjePanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            BackColor = SavaTheme.White;
        }

        public void Postavi(List<Stavka> s, string tekst)
        {
            stavke = s;
            poruka = tekst;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(SavaTheme.White);
            ControlPaint.DrawBorder(g, ClientRectangle, SavaTheme.Line, ButtonBorderStyle.Solid);
            int x = 22, y = 18, w = Width - 44;
            TextRenderer.DrawText(g, "Odobravanje", SavaTheme.FormTitleFont, new Point(x, y), SavaTheme.Text);
            y += 44;
            foreach (Stavka s in stavke)
            {
                using (var b = new SolidBrush(s.Boja))
                    g.FillRectangle(b, x, y, 4, 50);
                TextRenderer.DrawText(g, s.Naslov, SavaTheme.ButtonFontSemibold, new Rectangle(x + 16, y + 2, w - 16, 22), SavaTheme.Text, TextFormatFlags.EndEllipsis);
                TextRenderer.DrawText(g, s.Opis, SavaTheme.FieldFont, new Rectangle(x + 16, y + 26, w - 16, 22), s.Boja, TextFormatFlags.EndEllipsis);
                y += 66;
            }
            if (!string.IsNullOrEmpty(poruka))
                TextRenderer.DrawText(g, poruka, SavaTheme.FieldFont, new Rectangle(x, y, w, Height - y - 10), SavaTheme.Label, TextFormatFlags.WordBreak);
        }
    }
}
