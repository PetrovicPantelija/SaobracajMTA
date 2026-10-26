using Saobracaj.Theme;
using Syncfusion.Grouping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.HR
{
    // Forma zaposlenog (izgled formi, strane 2, 4, 5 i 6): zaglavlje sa slikom, kartice sa istorijom.
    // Zdravstveni pregledi, povrede, disciplinske mere, ocene i članovi porodice su osetljivi podaci (7.5):
    // vidljivi su samo uz pravo 'Osetljivi podaci', a svaki pristup se beleži u ZaposleniPristupLog.
    public class frmHRZaposleni : Form
    {
        private int id;
        private byte[] rowVer;
        private bool imaRowVer = true;
        private readonly bool osetljivo;
        private readonly HashSet<string> zabelezeno = new HashSet<string>();
        private readonly List<HRTabelaKartica> tabele = new List<HRTabelaKartica>();

        private readonly HRNaslovnaTraka naslov;
        private readonly PictureBox slika;
        private readonly HRTekst imeTekst, opisTekst;
        private readonly FlowLayoutPanel bedzevi;
        private readonly HRKartice kartice;
        private readonly Label statusna;
        private readonly HRObrazac osnovno;

        private HRTabelaKartica tRadnaMesta, tUgovori, tDokumenti, tHTZ, tMentori, tBeleske, tPrilozi, tGO;
        private readonly int ja;
        private readonly bool hr;
        private HRPolje poljeUgovorRadnoMesto;
        private DataRow trenutno;   // red iz vZaposleniTrenutno

        public frmHRZaposleni(int zaposleni)
        {
            id = zaposleni;
            HRStil.PripremiFormu(this, "Zaposleni");
            osetljivo = HRPrava.ImaPravo(HRPrava.OsetljiviPodaci);
            ja = HRPrava.TrenutniZaposleni();
            hr = HRPrava.JeHR();

            // 4. kartice
            kartice = new HRKartice();
            osnovno = new HRObrazac(PoljaOsnovno(), 4);
            var osnovnoPanel = new Panel { AutoScroll = true, BackColor = SavaTheme.Surface };
            osnovno.Panel.Dock = DockStyle.Top;
            osnovnoPanel.Controls.Add(osnovno.Panel);
            kartice.Dodaj("Osnovno", osnovnoPanel);
            kartice.Dodaj("Radna mesta", Podeljeno("Radna mesta", tRadnaMesta = Tabela(DefRadnaMesta()), "Ugovori", tUgovori = Tabela(DefUgovori())));
            kartice.Dodaj("Školovanje", Tabela(DefSkolovanje()));
            kartice.Dodaj("Dokumenti", tDokumenti = Tabela(DefDokumenti()));
            kartice.Dodaj("Pregledi", Osetljivo(() => Tabela(DefPregledi())));
            kartice.Dodaj("HTZ", tHTZ = Tabela(DefHTZ()));
            kartice.Dodaj("Mentori", Podeljeno("Mentorstva", tMentori = Tabela(DefMentori()), "Beleške mentora", tBeleske = Tabela(DefBeleske())));
            kartice.Dodaj("Kontakti", Podeljeno("Hitni kontakti", Tabela(DefKontakti()), "Članovi porodice", Osetljivo(() => Tabela(DefPorodica()))));
            var ostalo = new HRKartice();
            ostalo.Dodaj("Povrede na radu", Osetljivo(() => Tabela(DefPovrede())));
            ostalo.Dodaj("Disciplinske mere", Osetljivo(() => Tabela(DefDisciplinske())));
            ostalo.Dodaj("Ocene", Osetljivo(() => Tabela(DefOcene())));
            ostalo.Dodaj("Prilozi", tPrilozi = Tabela(DefPrilozi()));
            kartice.Dodaj("Godišnji odmor", Podeljeno("Osnovi godišnjeg odmora", Tabela(DefOsnoviGO()), "Pravo i zahtevi", tGO = Tabela(DefZahteviGO()), 45));
            kartice.Dodaj("Beleške", Tabela(HRBeleske.Def(() => this.id, null, ja, hr)));
            kartice.Dodaj("Ostalo", ostalo);
            kartice.Promenjena += k => ZabeleziPristup(k);
            ostalo.Promenjena += k => ZabeleziPristup(k);
            PodesiDodatneKomande();

            // 3. zaglavlje
            var zaglavlje = new Panel { Dock = DockStyle.Top, Height = 150, BackColor = HRStil.Zaglavlje, Padding = new Padding(20, 16, 20, 14) };
            slika = new PictureBox { Size = new Size(112, 118), Location = new Point(20, 16), SizeMode = PictureBoxSizeMode.Zoom, BackColor = ColorTranslator.FromHtml("#CFD8E3"), Cursor = Cursors.Hand };
            slika.Paint += Slika_Paint;
            slika.Click += (s, e) => MeniSlike();
            imeTekst = new HRTekst { Font = HRStil.VelikiNaslov, ForeColor = SavaTheme.Text, Location = new Point(152, 14), Height = 36, AutoSirina = false, Width = 900, Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right };
            opisTekst = new HRTekst { Font = SavaTheme.FieldFont, ForeColor = SavaTheme.Label, Location = new Point(152, 54), Height = 26, AutoSirina = false, Width = 900, Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right };
            bedzevi = new FlowLayoutPanel { Location = new Point(152, 88), Height = 32, Width = 900, WrapContents = false, BackColor = Color.Transparent, Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right };
            zaglavlje.Controls.AddRange(new Control[] { slika, imeTekst, opisTekst, bedzevi });
            zaglavlje.Resize += (s, e) =>
            {
                int w = Math.Max(200, zaglavlje.ClientSize.Width - 172);
                imeTekst.Width = opisTekst.Width = bedzevi.Width = w;
            };

            Controls.Add(kartice);
            Controls.Add(zaglavlje);

            // 1. naslovna traka sa komandama (2. alatna traka je spojena sa naslovnom, kao na strani 4)
            naslov = HRStil.NaslovnaTraka(this, "Zaposleni › Novi zaposleni");
            naslov.DodajDugme("Istorija", false, (s, e) => Istorija());
            naslov.DodajDugme("Karton", false, (s, e) => { if (id > 0) HRStampa.Karton(this, id); });
            naslov.DodajDugme("Sačuvaj", true, (s, e) => Sacuvaj());

            // 6. statusna linija
            statusna = HRStil.StatusnaLinija(this);

            HRStil.PrimeniTemu(this);
            Load += (s, e) => Ucitaj();
        }

        // ------------------------------------------------------------------ raspored

        private HRTabelaKartica Tabela(HRTabelaDef def)
        {
            var t = new HRTabelaKartica(def);
            tabele.Add(t);
            return t;
        }

        private Control Osetljivo(Func<Control> napravi)
        {
            if (osetljivo)
                return napravi();
            var p = new Panel { BackColor = SavaTheme.Surface };
            p.Controls.Add(new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Osetljivi podaci (zdravlje, povrede, disciplinske mere, ocene, porodica) vidljivi su samo uz pravo „Osetljivi podaci“ u HR modulu."
            });
            return p;
        }

        private static Control Podeljeno(string naslov1, Control gore, string naslov2, Control dole, float procenatGore = 55) => HRStil.Podeljeno(naslov1, gore, naslov2, dole, procenatGore);

        private static bool VezanZaZaposlenog(HRTabelaKartica t) => t.Def.RoditeljKolona == "Zaposleni" || t.Def.RoditeljKolona == "ZaposleniSifra";

        // ------------------------------------------------------------------ osnovni podaci

        private static List<HRPolje> PoljaOsnovno()
        {
            const string pol = "SELECT 'M' AS Pol, N'Muški' AS Naziv UNION ALL SELECT 'Z', N'Ženski'";
            return new List<HRPolje>
            {
                HRPolje.Tekst("Code", "Code", 30, true),
                HRPolje.Tekst("Prezime", "Prezime", 35, true),
                HRPolje.Tekst("Ime", "Ime", 35, true),
                new HRPolje("Status", "Status", HRTip.Lista, true) { ListaUpit = "SELECT Status, Naziv FROM dbo.ZaposleniStatusi WHERE Aktivan = 1", Podrazumevano = "A" },

                HRPolje.Tekst("SrednjePrezime", "Srednje prezime", 35),
                HRPolje.Tekst("ImeOca", "Ime oca", 35),
                HRPolje.Tekst("ImeMajke", "Ime majke", 35),
                HRPolje.Lista("Pol", "Pol", pol),

                HRPolje.Tekst("JMBG", "JMBG", 13),
                HRPolje.Datum("DatumRodjenja", "Datum rođenja"),
                HRPolje.Tekst("MestoRodjenja", "Mesto rođenja", 35),
                HRPolje.Lista("OpstinaRodjenja", "Opština rođenja", HRListe.Opstine),

                HRPolje.Lista("DrzavaRodjenja", "Država rođenja", HRListe.Drzave),
                HRPolje.Tekst("Telefon", "Telefon 1", 20),
                HRPolje.Tekst("Telefon2", "Telefon 2", 20),
                HRPolje.Tekst("email", "Email", 100),

                HRPolje.Tekst("AdresaStalnoPreb", "Stalna adresa", 100),
                HRPolje.Lista("OpstinaStalnoPreb", "Opština stalnog prebivališta", HRListe.Opstine),
                HRPolje.Lista("DrzavaStalnoPreb", "Država stalnog prebivališta", HRListe.Drzave),
                HRPolje.Tekst("SifraERP", "Šifra u ERP-u", 30),

                HRPolje.Tekst("AdresaPrivPreb", "Privremena adresa", 100),
                HRPolje.Lista("OpstinaPrivPreb", "Opština privremenog boravka", HRListe.Opstine),
                HRPolje.Lista("DrzavaPrivPreb", "Država privremenog boravka", HRListe.Drzave),
                HRPolje.Tekst("KorisnickoIme", "Korisničko ime u aplikaciji", 20),

                HRPolje.Datum("DatumUlaska", "Datum ulaska"),
                HRPolje.Datum("DatumIzlaska", "Datum izlaska"),
                HRPolje.Tekst("KonfekcijskiBroj", "Konfekcijski broj", 10),
                HRPolje.Tekst("BrojObuce", "Broj obuće", 10),

                HRPolje.Broj("PrethodniStazGodina", "Prethodni staž: godina", false, 0, 60),
                HRPolje.Broj("PrethodniStazMeseci", "Prethodni staž: meseci", false, 0, 11),
                HRPolje.Broj("PrethodniStazDana", "Prethodni staž: dana", false, 0, 30),
                HRPolje.Lista("RasporedRadaID", "Raspored rada", "SELECT ID, Naziv FROM dbo.RasporedRada ORDER BY Naziv"),

                new HRPolje("NadredjeniSifra", "Nadređeni (šef)", HRTip.Lista) { ListaUpit = HRListe.SviZaposleni, Raspon = 2 },

                new HRPolje("Napomena", "Napomena", HRTip.VelikiTekst, false, 500) { Raspon = 4 }
            };
        }

        private void Ucitaj()
        {
            if (id > 0)
            {
                DataRow r;
                try
                {
                    r = HRBaza.Red("SELECT * FROM dbo.Zaposleni WHERE ID = @ID", HRBaza.P("@ID", id));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Podaci zaposlenog nisu učitani. Proverite da li je na bazi pokrenuta skripta HR_Modul_2026_10.sql.\n\n" + ex.Message,
                        "Zaposleni", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (r == null)
                {
                    MessageBox.Show("Zaposleni ne postoji.", "Zaposleni", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var v = new HRVrednosti();
                foreach (DataColumn c in r.Table.Columns)
                    v[c.ColumnName] = r[c];
                osnovno.PostaviSve(v);
                imaRowVer = r.Table.Columns.Contains("RowVer");
                rowVer = imaRowVer ? r["RowVer"] as byte[] : null;
            }
            else
                osnovno.PostaviSve(new HRVrednosti { ["Status"] = "A", ["DatumUlaska"] = DateTime.Today });

            PostaviIdUListama();
            foreach (HRTabelaKartica t in tabele)
                if (VezanZaZaposlenog(t))
                    t.Ucitaj(id);
            OsveziZaglavlje();
            OsveziNapomene();
        }

        private void PostaviIdUListama()
        {
            poljeUgovorRadnoMesto.ListaUpit =
                "SELECT rm.Sifra, r.Naziv + N' (od ' + FORMAT(rm.DatumOd, 'dd.MM.yyyy') + N')' AS Naziv " +
                "FROM dbo.ZaposleniRadnaMesta rm JOIN dbo.RadnaMesta r ON r.Sifra = rm.RadnoMesto " +
                "WHERE rm.Zaposleni = " + id + " ORDER BY rm.DatumOd DESC";
        }

        private void Sacuvaj()
        {
            string greska = osnovno.Ocitaj(out HRVrednosti v);
            if (greska != null)
            {
                kartice.Izaberi("Osnovno");
                MessageBox.Show(greska, "Zaposleni", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string jmbg = v.Tekst("JMBG");
            if (jmbg != "" && !JmbgIspravan(jmbg) &&
                MessageBox.Show("JMBG nije ispravan (dužina ili kontrolna cifra). Sačuvati ipak?", "Zaposleni", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            DateTime? ulazak = v.Datum("DatumUlaska"), izlazak = v.Datum("DatumIzlaska");
            if (ulazak.HasValue && izlazak.HasValue && izlazak < ulazak)
            {
                MessageBox.Show("Datum izlaska ne može biti pre datuma ulaska.", "Zaposleni", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var kolone = osnovno.Polja.Where(p => p.Snima).Select(p => p.Kolona).ToList();
            var parametri = kolone.Select((k, i) => HRBaza.P("@p" + i, v.Uzmi(k))).ToList();
            parametri.Add(HRBaza.P("@Korisnik", HRBaza.Korisnik));
            try
            {
                if (id == 0)
                {
                    // SCOPE_IDENTITY umesto OUTPUT: tabela Zaposleni ima okidač (faza 2), a OUTPUT bez INTO tada nije dozvoljen
                    string sql = "INSERT INTO dbo.Zaposleni (" + string.Join(", ", kolone.Select(k => "[" + k + "]")) + ", Datum, Korisnik, KreiraoKorisnik) " +
                                 "VALUES (" + string.Join(", ", kolone.Select((k, i) => "@p" + i)) + ", GETDATE(), @Korisnik, @Korisnik); " +
                                 "SELECT CAST(SCOPE_IDENTITY() AS int);";
                    id = HRBaza.CeoBroj(HRBaza.Skalar(sql, parametri.ToArray()));
                    try
                    {
                        // osnov OSN (zakonski minimum godišnjeg odmora) za novog zaposlenog
                        HRBaza.Procedura("dbo.sp_GO_DodeliOsnovni", HRBaza.P("@ZaposleniSifra", id));
                    }
                    catch (SqlException)
                    {
                        // skripta faze 2 nije puštena
                    }
                }
                else
                {
                    parametri.Add(HRBaza.P("@ID", id));
                    parametri.Add(new SqlParameter("@RowVer", SqlDbType.Timestamp) { Value = (object)rowVer ?? DBNull.Value });
                    string sql = "UPDATE dbo.Zaposleni SET " + string.Join(", ", kolone.Select((k, i) => "[" + k + "] = @p" + i)) +
                                 ", IzmenioKorisnik = @Korisnik, DatumIzmene = sysdatetime() WHERE ID = @ID" +
                                 (imaRowVer ? " AND (RowVer = @RowVer OR @RowVer IS NULL)" : "");
                    if (HRBaza.Izvrsi(sql, parametri.ToArray()) == 0)
                    {
                        MessageBox.Show("Neko je u međuvremenu izmenio podatke ovog zaposlenog. Podaci će biti ponovo učitani; ponovite izmenu.",
                            "Zaposleni", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Ucitaj();
                        return;
                    }
                }
            }
            catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
            {
                MessageBox.Show("Code, JMBG ili šifra u ERP-u već postoji kod drugog zaposlenog.", "Zaposleni", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Zaposleni", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (imaRowVer)
                rowVer = HRBaza.Skalar("SELECT RowVer FROM dbo.Zaposleni WHERE ID = @ID", HRBaza.P("@ID", id)) as byte[];
            PostaviIdUListama();
            foreach (HRTabelaKartica t in tabele)
                if (VezanZaZaposlenog(t) && t.Roditelj != id)
                    t.Ucitaj(id);
            OsveziZaglavlje();

            // 7.4: pri izlasku zaposlenog prikazuje se sva oprema koja nije razdužena
            if (izlazak.HasValue || v.Tekst("Status") == "P")
                ProveriNerazduzenuOpremu();
        }

        public static bool JmbgIspravan(string jmbg)
        {
            if (jmbg.Length != 13 || !jmbg.All(char.IsDigit))
                return false;
            int[] d = jmbg.Select(c => c - '0').ToArray();
            int s = 7 * (d[0] + d[6]) + 6 * (d[1] + d[7]) + 5 * (d[2] + d[8]) + 4 * (d[3] + d[9]) + 3 * (d[4] + d[10]) + 2 * (d[5] + d[11]);
            int k = 11 - s % 11;
            if (k > 9)
                k = 0;
            return k == d[12];
        }

        private void ProveriNerazduzenuOpremu()
        {
            DataTable dt = HRBaza.Tabela(@"
                SELECT o.Naziv, s.Kolicina, o.JedinicaMere, r.BrojReversa
                FROM dbo.ZaposleniHTZZaduzenja s
                JOIN dbo.HTZOprema o ON o.Sifra = s.HTZOprema
                JOIN dbo.HTZReversi r ON r.Sifra = s.Revers AND r.Storniran = 0
                WHERE s.Zaposleni = @Z AND s.DatumRazduzenja IS NULL", HRBaza.P("@Z", id));
            if (dt.Rows.Count == 0)
                return;
            string spisak = string.Join("\n", dt.Rows.Cast<DataRow>().Select(r =>
                "• " + r["Naziv"] + " – " + HRBaza.Decimalni(r["Kolicina"]).ToString("0.##") + " " + r["JedinicaMere"] + " (revers " + r["BrojReversa"] + ")"));
            MessageBox.Show("Zaposleni izlazi, a ova oprema nije razdužena:\n\n" + spisak + "\n\nRazdužite je na kartici HTZ.",
                "HTZ oprema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            kartice.Izaberi("HTZ");
        }

        // ------------------------------------------------------------------ zaglavlje

        private void OsveziZaglavlje()
        {
            bedzevi.Controls.Clear();
            if (id == 0)
            {
                naslov.Naslov.Text = "Zaposleni › Novi zaposleni";
                imeTekst.Text = "Novi zaposleni";
                opisTekst.Text = "Unesite osnovne podatke i sačuvajte; ostale kartice se popunjavaju posle snimanja.";
                statusna.Text = "* obavezno polje";
                slika.Image = null;
                return;
            }
            try
            {
                trenutno = HRBaza.Red(@"
                    SELECT t.*, ISNULL(u.BrojUpozorenja, 0) AS BrojUpozorenja, z.KreiraoKorisnik, z.DatumKreiranja, z.IzmenioKorisnik, z.DatumIzmene
                    FROM dbo.vZaposleniTrenutno t
                    JOIN dbo.Zaposleni z ON z.ID = t.Zaposleni
                    LEFT JOIN dbo.vZaposleniUpozorenja u ON u.Zaposleni = t.Zaposleni
                    WHERE t.Zaposleni = @Z", HRBaza.P("@Z", id));
            }
            catch (Exception ex)
            {
                opisTekst.Text = ex.Message;
                return;
            }
            if (trenutno == null)
                return;

            string ime = Convert.ToString(trenutno["PrezimeIme"]);
            naslov.Naslov.Text = "Zaposleni › " + ime + " (" + trenutno["Code"] + ")";
            Text = ime;
            imeTekst.Text = ime;
            var opis = new List<string>();
            foreach (string k in new[] { "RadnoMestoNaziv", "OrganizacionaJedinicaNaziv" })
                if (trenutno[k] != DBNull.Value && Convert.ToString(trenutno[k]).Trim() != "")
                    opis.Add(Convert.ToString(trenutno[k]).Trim());
            if (trenutno["SluzbenoMestoNaziv"] != DBNull.Value)
                opis.Add("Matično mesto: " + Convert.ToString(trenutno["SluzbenoMestoNaziv"]).Trim());
            opisTekst.Text = opis.Count > 0 ? string.Join(" · ", opis) : "Radno mesto nije uneto";

            bool aktivan = Convert.ToString(trenutno["Status"]) == "A";
            bedzevi.Controls.Add(new HRBedz(Convert.ToString(trenutno["StatusNaziv"]),
                aktivan ? HRStil.PozadinaVazi : SavaTheme.White, aktivan ? HRStil.StatusVazi : SavaTheme.Label));
            if (trenutno["MentorNaziv"] != DBNull.Value && Convert.ToString(trenutno["MentorNaziv"]).Trim() != "")
                bedzevi.Controls.Add(new HRBedz("Mentor: " + trenutno["MentorNaziv"], SavaTheme.White, SavaTheme.Text));
            int staz = HRBaza.CeoBroj(trenutno["StazMeseci"]);
            bedzevi.Controls.Add(new HRBedz("Staž: " + staz / 12 + " g " + staz % 12 + " m", SavaTheme.White, SavaTheme.Text));
            int upozorenja = HRBaza.CeoBroj(trenutno["BrojUpozorenja"]);
            if (upozorenja > 0 && aktivan)
                bedzevi.Controls.Add(new HRBedz(upozorenja + (upozorenja == 1 ? " upozorenje" : upozorenja < 5 ? " upozorenja" : " upozorenja"),
                    HRStil.PozadinaIsteklo, SavaTheme.Danger));

            statusna.Text = "Kreirao: " + Audit(trenutno["KreiraoKorisnik"], trenutno["DatumKreiranja"]) +
                            (trenutno["IzmenioKorisnik"] != DBNull.Value ? " · Izmenio: " + Audit(trenutno["IzmenioKorisnik"], trenutno["DatumIzmene"]) : "") +
                            " · * obavezno polje";
            UcitajSliku();
        }

        private static string Audit(object korisnik, object datum)
        {
            string k = korisnik == DBNull.Value || korisnik == null ? "–" : Convert.ToString(korisnik);
            return k + (datum == DBNull.Value || datum == null ? "" : " " + Convert.ToDateTime(datum).ToString("dd.MM.yyyy"));
        }

        private void UcitajSliku()
        {
            slika.Image = null;
            try
            {
                if (HRBaza.Skalar("SELECT Slika FROM dbo.ZaposleniSlike WHERE Zaposleni = @Z", HRBaza.P("@Z", id)) is byte[] b)
                    using (var ms = new MemoryStream(b))
                        slika.Image = new Bitmap(Image.FromStream(ms));
            }
            catch
            {
                slika.Image = null;
            }
        }

        private void Slika_Paint(object sender, PaintEventArgs e)
        {
            if (slika.Image == null)
                TextRenderer.DrawText(e.Graphics, "Foto", SavaTheme.FieldFont, slika.ClientRectangle, SavaTheme.Label,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private void MeniSlike()
        {
            if (id == 0)
            {
                MessageBox.Show("Prvo sačuvajte osnovne podatke.", "Slika", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var meni = new ContextMenuStrip { Font = SavaTheme.FieldFont };
            meni.Items.Add("Promeni sliku…", null, (s, e) => PromeniSliku());
            if (slika.Image != null)
                meni.Items.Add("Ukloni sliku", null, (s, e) =>
                {
                    HRBaza.Izvrsi("DELETE FROM dbo.ZaposleniSlike WHERE Zaposleni = @Z", HRBaza.P("@Z", id));
                    UcitajSliku();
                });
            meni.Show(slika, new Point(0, slika.Height));
        }

        // Slika se smanjuje na najviše 600 px i čuva kao JPEG, da lista i forma ostanu brze
        private void PromeniSliku()
        {
            using (var ofd = new OpenFileDialog { Filter = "Slike|*.jpg;*.jpeg;*.png;*.bmp" })
            {
                if (ofd.ShowDialog(this) != DialogResult.OK)
                    return;
                try
                {
                    byte[] podaci;
                    using (var izvor = Image.FromFile(ofd.FileName))
                    {
                        double faktor = Math.Min(1.0, 600.0 / Math.Max(izvor.Width, izvor.Height));
                        using (var mala = new Bitmap(Math.Max(1, (int)(izvor.Width * faktor)), Math.Max(1, (int)(izvor.Height * faktor))))
                        {
                            using (var g = Graphics.FromImage(mala))
                            {
                                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                g.DrawImage(izvor, 0, 0, mala.Width, mala.Height);
                            }
                            using (var ms = new MemoryStream())
                            {
                                mala.Save(ms, ImageFormat.Jpeg);
                                podaci = ms.ToArray();
                            }
                        }
                    }
                    HRBaza.Izvrsi(@"
                        MERGE dbo.ZaposleniSlike AS c
                        USING (SELECT @Z AS Zaposleni) AS s ON c.Zaposleni = s.Zaposleni
                        WHEN MATCHED THEN UPDATE SET Slika = @S, TipFajla = N'image/jpeg', DatumIzmene = sysdatetime()
                        WHEN NOT MATCHED THEN INSERT (Zaposleni, Slika, TipFajla) VALUES (@Z, @S, N'image/jpeg');",
                        HRBaza.P("@Z", id), new SqlParameter("@S", SqlDbType.VarBinary, -1) { Value = podaci });
                    UcitajSliku();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Slika", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void ZabeleziPristup(string kartica)
        {
            if (!osetljivo || id == 0)
                return;
            if (kartica == "Pregledi" || kartica == "Kontakti" || kartica == "Povrede na radu" || kartica == "Disciplinske mere" || kartica == "Ocene")
                if (zabelezeno.Add(kartica))
                    HRPrava.ZabeleziPristup(id, kartica);
        }

        // ------------------------------------------------------------------ istorija

        private void Istorija()
        {
            if (id == 0)
                return;
            string upit = @"
                SELECT rm.DatumOd AS Datum, N'Radno mesto' AS Vrsta, r.Naziv + ISNULL(N' · ' + j.Naziv, N'') + CASE WHEN rm.DatumDo IS NULL THEN N' (trenutno)' ELSE N' (do ' + FORMAT(rm.DatumDo, 'dd.MM.yyyy') + N')' END AS Opis, rm.KreiraoKorisnik AS Korisnik
                FROM dbo.ZaposleniRadnaMesta rm JOIN dbo.RadnaMesta r ON r.Sifra = rm.RadnoMesto LEFT JOIN dbo.OrganizacioneJedinice j ON j.ID = rm.OrganizacionaJedinica WHERE rm.Zaposleni = @Z
                UNION ALL
                SELECT u.VaziOd, N'Ugovor', v.Naziv + ISNULL(N' br. ' + u.BrojUgovora, N'') + ISNULL(N' (važi do ' + FORMAT(u.VaziDo, 'dd.MM.yyyy') + N')', N''), u.KreiraoKorisnik
                FROM dbo.ZaposleniUgovori u JOIN dbo.VrsteUgovora v ON v.Sifra = u.VrstaUgovora WHERE u.Zaposleni = @Z
                UNION ALL
                SELECT m.DatumOd, N'Mentor', LTRIM(RTRIM(ISNULL(z.Prezime, N'') + N' ' + ISNULL(z.Ime, N''))), m.KreiraoKorisnik
                FROM dbo.ZaposleniMentori m JOIN dbo.Zaposleni z ON z.ID = m.Mentor WHERE m.Zaposleni = @Z
                UNION ALL
                SELECT ISNULL(d.DatumIzdavanja, CAST(d.DatumKreiranja AS date)), N'Dokument', v.Naziv + ISNULL(N' ' + d.BrojDokumenta, N''), d.KreiraoKorisnik
                FROM dbo.ZaposleniDokumenti d JOIN dbo.VrsteDokumenta v ON v.Sifra = d.VrstaDokumenta WHERE d.Zaposleni = @Z
                UNION ALL
                SELECT s.DatumZavrsetka, N'Školovanje', s.NazivSkole, s.KreiraoKorisnik FROM dbo.ZaposleniSkolovanje s WHERE s.Zaposleni = @Z
                UNION ALL
                SELECT r.Datum, N'HTZ revers', N'Revers ' + r.BrojReversa + CASE WHEN r.Storniran = 1 THEN N' (storniran)' ELSE N'' END, r.KreiraoKorisnik
                FROM dbo.HTZReversi r WHERE r.Zaposleni = @Z
                UNION ALL
                SELECT CAST(z.DatumKreiranja AS date), N'Zaposleni', N'Kreiran zapis', z.KreiraoKorisnik FROM dbo.Zaposleni z WHERE z.ID = @Z AND z.DatumKreiranja IS NOT NULL
                UNION ALL
                SELECT CAST(z.DatumIzmene AS date), N'Zaposleni', N'Poslednja izmena osnovnih podataka', z.IzmenioKorisnik FROM dbo.Zaposleni z WHERE z.ID = @Z AND z.DatumIzmene IS NOT NULL";
            if (osetljivo)
                upit += @"
                UNION ALL
                SELECT p.DatumPregleda, N'Pregled', v.Naziv + N' · ' + o.Naziv, p.KreiraoKorisnik
                FROM dbo.ZaposleniZdravstveniPregledi p JOIN dbo.VrstePregleda v ON v.Sifra = p.VrstaPregleda JOIN dbo.OceneSposobnosti o ON o.Sifra = p.OcenaSposobnosti WHERE p.Zaposleni = @Z";
            upit = "SELECT * FROM (" + upit + ") x ORDER BY Datum DESC";

            var forma = new Form
            {
                Text = "Istorija: " + imeTekst.Text,
                Size = new Size(900, 560),
                StartPosition = FormStartPosition.CenterParent,
                BackColor = SavaTheme.Surface,
                ShowInTaskbar = false
            };
            var grid = HRGrid.Novi();
            HRGrid.Kolone(grid,
                new HRKolona("Datum", "Datum", 100, "datum"),
                new HRKolona("Vrsta", "Vrsta", 120),
                new HRKolona("Opis", "Opis", 480),
                new HRKolona("Korisnik", "Upisao", 120));
            var okvir = HRStil.BeliPanel();
            okvir.Dock = DockStyle.Fill;
            okvir.Controls.Add(grid);
            var telo = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20) };
            telo.Controls.Add(okvir);
            forma.Controls.Add(telo);
            HRStil.NaslovnaTraka(forma, "Istorija › " + imeTekst.Text);
            HRStil.PrimeniTemu(forma);
            try
            {
                HRGrid.Napuni(grid, HRBaza.Tabela(upit, HRBaza.P("@Z", id)));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Istorija", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            forma.ShowDialog(this);
        }

        // ------------------------------------------------------------------ opisi kartica

        private static Color? BojaDana(Record r, string kolona, string kolonaDatuma, int danaUpozorenja = 30)
        {
            if (kolona != kolonaDatuma)
                return null;
            object d = r.GetValue("DanaDo");
            if (d == null || d == DBNull.Value)
                return null;
            int dana = HRBaza.CeoBroj(d);
            return dana < 0 ? HRStil.StatusIsteklo : dana <= danaUpozorenja ? HRStil.StatusIstice : (Color?)null;
        }

        private HRTabelaDef DefRadnaMesta()
        {
            return new HRTabelaDef
            {
                Naslov = "Radno mesto",
                Tabela = "ZaposleniRadnaMesta",
                RoditeljKolona = "Zaposleni",
                Upit = @"SELECT rm.Sifra, r.Naziv AS RadnoMesto, o.Naziv AS Odnos, j.Naziv AS Jedinica, RTRIM(s.Opis) AS SluzbenoMesto,
                                rm.DatumOd, rm.DatumDo, rm.Napomena
                         FROM dbo.ZaposleniRadnaMesta rm
                         JOIN dbo.RadnaMesta r ON r.Sifra = rm.RadnoMesto
                         JOIN dbo.RadniOdnosi o ON o.Sifra = rm.VrstaRadnogOdnosa
                         LEFT JOIN dbo.OrganizacioneJedinice j ON j.ID = rm.OrganizacionaJedinica
                         LEFT JOIN dbo.Stanice s ON s.ID = rm.SluzbenoMesto
                         WHERE rm.Zaposleni = @Roditelj ORDER BY rm.DatumOd DESC",
                Kolone = new[]
                {
                    new HRKolona("RadnoMesto", "Radno mesto", 200), new HRKolona("Odnos", "Radni odnos", 150),
                    new HRKolona("Jedinica", "Org. jedinica", 170), new HRKolona("SluzbenoMesto", "Matično službeno mesto", 170),
                    new HRKolona("DatumOd", "Od", 100, "datum"), new HRKolona("DatumDo", "Do", 100, "datum"), new HRKolona("Napomena", "Napomena", 220)
                },
                Polja = new List<HRPolje>
                {
                    HRPolje.Lista("RadnoMesto", "Radno mesto", HRListe.Sifarnik("RadnaMesta"), true),
                    HRPolje.Lista("VrstaRadnogOdnosa", "Radni odnos", HRListe.Sifarnik("RadniOdnosi"), true),
                    HRPolje.Lista("OrganizacionaJedinica", "Organizaciona jedinica", HRListe.OrganizacioneJedinice),
                    HRPolje.Lista("SluzbenoMesto", "Matično službeno mesto", HRListe.SluzbenaMesta),
                    HRPolje.Datum("DatumOd", "Datum od", true),
                    HRPolje.Datum("DatumDo", "Datum do"),
                    HRPolje.Napomena("Napomena", "Napomena (razlog promene, broj rešenja)")
                },
                Napomena = "Novo radno mesto automatski zatvara prethodno (Datum do = dan pre početka novog). Periodi se ne preklapaju.",
                NoviPodrazumevano = () => new HRVrednosti
                {
                    ["DatumOd"] = DateTime.Today,
                    ["OrganizacionaJedinica"] = trenutno?["OrganizacionaJedinica"],
                    ["SluzbenoMesto"] = trenutno?["SluzbenoMesto"]
                },
                Validacija = v => v.Datum("DatumDo").HasValue && v.Datum("DatumDo") < v.Datum("DatumOd") ? "Datum do ne može biti pre datuma od." : null,
                Snimi = (v, sifra) =>
                {
                    HRBaza.Procedura("dbo.HR_ZaposleniRadnoMestoSnimi",
                        HRBaza.P("@Sifra", sifra), HRBaza.P("@Zaposleni", id), HRBaza.P("@RadnoMesto", v.Uzmi("RadnoMesto")),
                        HRBaza.P("@VrstaRadnogOdnosa", v.Uzmi("VrstaRadnogOdnosa")), HRBaza.P("@OrganizacionaJedinica", v.Uzmi("OrganizacionaJedinica")),
                        HRBaza.P("@SluzbenoMesto", v.Uzmi("SluzbenoMesto")), HRBaza.P("@DatumOd", v.Uzmi("DatumOd")), HRBaza.P("@DatumDo", v.Uzmi("DatumDo")),
                        HRBaza.P("@Napomena", v.Uzmi("Napomena")), HRBaza.P("@Korisnik", HRBaza.Korisnik), HRBaza.Izlaz("@NovaSifra", SqlDbType.Int));
                    return null;
                },
                BojaTeksta = (r, k) => k == "RadnoMesto" && r.GetValue("DatumDo") == DBNull.Value ? SavaTheme.Primary : (Color?)null
            };
        }

        private HRTabelaDef DefUgovori()
        {
            poljeUgovorRadnoMesto = HRPolje.Lista("ZaposleniRadnoMesto", "Radno mesto iz ugovora", "SELECT 0 AS Sifra, N'' AS Naziv WHERE 1 = 0");
            return new HRTabelaDef
            {
                Naslov = "Ugovor",
                Tabela = "ZaposleniUgovori",
                RoditeljKolona = "Zaposleni",
                Upit = @"SELECT u.Sifra, v.Naziv AS Vrsta, u.BrojUgovora, u.DatumUgovora, u.VaziOd, u.VaziDo, r.Naziv AS RadnoMesto, u.Napomena,
                                DATEDIFF(DAY, CAST(GETDATE() AS date), u.VaziDo) AS DanaDo
                         FROM dbo.ZaposleniUgovori u
                         JOIN dbo.VrsteUgovora v ON v.Sifra = u.VrstaUgovora
                         LEFT JOIN dbo.ZaposleniRadnaMesta zr ON zr.Sifra = u.ZaposleniRadnoMesto
                         LEFT JOIN dbo.RadnaMesta r ON r.Sifra = zr.RadnoMesto
                         WHERE u.Zaposleni = @Roditelj ORDER BY u.VaziOd DESC",
                Kolone = new[]
                {
                    new HRKolona("Vrsta", "Vrsta ugovora", 180), new HRKolona("BrojUgovora", "Broj", 110), new HRKolona("DatumUgovora", "Datum ugovora", 110, "datum"),
                    new HRKolona("VaziOd", "Važi od", 100, "datum"), new HRKolona("VaziDo", "Važi do", 100, "datum"),
                    new HRKolona("RadnoMesto", "Radno mesto", 180), new HRKolona("Napomena", "Napomena", 200)
                },
                Polja = new List<HRPolje>
                {
                    HRPolje.Lista("VrstaUgovora", "Vrsta ugovora", HRListe.Sifarnik("VrsteUgovora"), true),
                    HRPolje.Tekst("BrojUgovora", "Broj ugovora", 30),
                    HRPolje.Datum("DatumUgovora", "Datum ugovora", true),
                    poljeUgovorRadnoMesto,
                    HRPolje.Datum("VaziOd", "Važi od", true),
                    HRPolje.Datum("VaziDo", "Važi do (za određeno vreme)"),
                    HRPolje.Napomena()
                },
                Napomena = "Aneks koji menja radno mesto: prvo unesite novo radno mesto, pa aneks vežite za njega.",
                NoviPodrazumevano = () => new HRVrednosti { ["DatumUgovora"] = DateTime.Today, ["VaziOd"] = DateTime.Today, ["ZaposleniRadnoMesto"] = trenutno?["ZaposleniRadnoMesto"] },
                Validacija = v => v.Datum("VaziDo").HasValue && v.Datum("VaziDo") < v.Datum("VaziOd") ? "Važi do ne može biti pre Važi od." : null,
                BojaTeksta = (r, k) => BojaDana(r, k, "VaziDo")
            };
        }

        private HRTabelaDef DefSkolovanje()
        {
            return new HRTabelaDef
            {
                Naslov = "Školovanje",
                Tabela = "ZaposleniSkolovanje",
                RoditeljKolona = "Zaposleni",
                Upit = @"SELECT s.Sifra, so.Naziv AS Stepen, s.NazivSkole, s.Mesto, s.SmerZanimanje, s.DatumZavrsetka, s.BrojDiplome, s.Napomena
                         FROM dbo.ZaposleniSkolovanje s JOIN dbo.StepenObrazovanja so ON so.Sifra = s.StepenObrazovanja
                         WHERE s.Zaposleni = @Roditelj ORDER BY s.DatumZavrsetka DESC",
                Kolone = new[]
                {
                    new HRKolona("Stepen", "Stepen obrazovanja", 220), new HRKolona("NazivSkole", "Škola", 220), new HRKolona("Mesto", "Mesto", 120),
                    new HRKolona("SmerZanimanje", "Smer / zanimanje", 200), new HRKolona("DatumZavrsetka", "Završetak", 100, "datum"),
                    new HRKolona("BrojDiplome", "Broj diplome", 110)
                },
                Polja = new List<HRPolje>
                {
                    HRPolje.Lista("StepenObrazovanja", "Stepen obrazovanja", HRListe.Sifarnik("StepenObrazovanja"), true),
                    HRPolje.Tekst("NazivSkole", "Naziv škole", 100, true),
                    HRPolje.Tekst("Mesto", "Mesto", 35),
                    HRPolje.Tekst("SmerZanimanje", "Smer / zanimanje", 100),
                    HRPolje.Datum("DatumZavrsetka", "Datum završetka"),
                    HRPolje.Tekst("BrojDiplome", "Broj diplome", 30),
                    HRPolje.Napomena()
                },
                Napomena = "Samo formalno obrazovanje; sertifikati i obuke se unose na kartici Dokumenti."
            };
        }

        private HRTabelaDef DefDokumenti()
        {
            const string danaDo = "DATEDIFF(DAY, CAST(GETDATE() AS date), d.VaziDo)";
            return new HRTabelaDef
            {
                Naslov = "Dokument",
                Tabela = "ZaposleniDokumenti",
                RoditeljKolona = "Zaposleni",
                Upit = @"SELECT d.Sifra, v.Naziv AS Vrsta, g.Naziv AS Grupa, d.BrojDokumenta, d.Kategorije, d.DatumIzdavanja, d.VaziDo, d.IzdaoOrgan, d.Napomena,
                                ISNULL(v.DanaUpozorenja, 30) AS DanaUpozorenja,
                                CASE WHEN v.ImaRokVazenja = 1 AND d.VaziDo IS NOT NULL THEN " + danaDo + @" END AS DanaDo,
                                CASE WHEN v.ImaRokVazenja = 0 OR d.VaziDo IS NULL THEN N'Bez roka'
                                     WHEN " + danaDo + @" < 0 THEN N'Istekla'
                                     WHEN " + danaDo + @" = 0 THEN N'Ističe danas'
                                     WHEN " + danaDo + @" <= ISNULL(v.DanaUpozorenja, 30) THEN N'Ističe za ' + CAST(" + danaDo + @" AS nvarchar(10)) + N' dana'
                                     ELSE N'Važi' END AS Status,
                                CASE WHEN v.ImaRokVazenja = 0 OR d.VaziDo IS NULL THEN 3
                                     WHEN " + danaDo + @" < 0 THEN 0
                                     WHEN " + danaDo + @" <= ISNULL(v.DanaUpozorenja, 30) THEN 1 ELSE 2 END AS Redosled,
                                (SELECT COUNT(*) FROM dbo.ZaposleniPrilozi p WHERE p.Zaposleni = d.Zaposleni AND p.VezaTip = N'Dokument' AND p.VezaSifra = d.Sifra) AS Skenova
                         FROM dbo.ZaposleniDokumenti d
                         JOIN dbo.VrsteDokumenta v ON v.Sifra = d.VrstaDokumenta
                         JOIN dbo.GrupeDokumenata g ON g.Sifra = v.GrupaDokumenta
                         WHERE d.Zaposleni = @Roditelj
                         ORDER BY Redosled, d.VaziDo, v.Naziv",
                Filteri = new List<Tuple<string, string>>
                {
                    Tuple.Create("Svi", ""),
                    Tuple.Create("Lični", "Grupa = 'Lični dokument'"),
                    Tuple.Create("Licence", "Grupa = 'Licenca'"),
                    Tuple.Create("Sertifikati", "Grupa = 'Sertifikat'"),
                    Tuple.Create("Obuke", "Grupa = 'Obuka'")
                },
                Kolone = new[]
                {
                    new HRKolona("Vrsta", "Vrsta", 200), new HRKolona("BrojDokumenta", "Broj", 130), new HRKolona("Kategorije", "Kat.", 70),
                    new HRKolona("DatumIzdavanja", "Izdat", 100, "datum"), new HRKolona("VaziDo", "Važi do", 100, "datum"),
                    new HRKolona("Status", "Status", 150), new HRKolona("IzdaoOrgan", "Izdao", 170), new HRKolona("Skenova", "Sken", 60, "broj")
                },
                Polja = new List<HRPolje>
                {
                    HRPolje.Lista("VrstaDokumenta", "Vrsta dokumenta", "SELECT Sifra, Naziv FROM dbo.VrsteDokumenta WHERE Aktivan = 1 ORDER BY Naziv", true),
                    HRPolje.Tekst("BrojDokumenta", "Broj dokumenta", 30),
                    HRPolje.Datum("DatumIzdavanja", "Datum izdavanja"),
                    HRPolje.Datum("VaziDo", "Važi do"),
                    HRPolje.Tekst("IzdaoOrgan", "Izdao organ", 100),
                    HRPolje.Tekst("Kategorije", "Kategorije (npr. B, C, E)", 50),
                    HRPolje.Napomena()
                },
                Napomena = "Istekli i dokumenti koji uskoro ističu prikazuju se na vrhu liste.",
                NoviPodrazumevano = () => new HRVrednosti { ["DatumIzdavanja"] = DateTime.Today },
                PriPromeni = (dlg, kolona) =>
                {
                    if (kolona != "VrstaDokumenta")
                        return;
                    DataRow vrsta = VrstaDokumenta(dlg.Vrednost("VrstaDokumenta"));
                    bool rok = vrsta != null && Convert.ToBoolean(vrsta["ImaRokVazenja"]);
                    bool kat = vrsta != null && Convert.ToBoolean(vrsta["ImaKategorije"]);
                    dlg.PostaviOznaku("VaziDo", rok ? "Važi do *" : "Važi do");
                    dlg.Omoguci("Kategorije", kat, true);
                },
                Validacija = v =>
                {
                    DataRow vrsta = VrstaDokumenta(v.Uzmi("VrstaDokumenta"));
                    if (vrsta != null && Convert.ToBoolean(vrsta["ImaRokVazenja"]) && !v.Datum("VaziDo").HasValue)
                        return "Za ovu vrstu dokumenta unesite datum Važi do.";
                    if (v.Datum("VaziDo").HasValue && v.Datum("DatumIzdavanja").HasValue && v.Datum("VaziDo") < v.Datum("DatumIzdavanja"))
                        return "Važi do ne može biti pre datuma izdavanja.";
                    return null;
                },
                BojaTeksta = (r, k) =>
                {
                    if (k != "Status")
                        return null;
                    object d = r.GetValue("DanaDo");
                    if (d == null || d == DBNull.Value)
                        return SavaTheme.Label;
                    return HRStil.BojaRoka(HRBaza.CeoBroj(d), HRBaza.CeoBroj(r.GetValue("DanaUpozorenja")));
                }
            };
        }

        private static DataRow VrstaDokumenta(object sifra)
        {
            if (sifra == null || sifra == DBNull.Value)
                return null;
            return HRBaza.Red("SELECT ImaRokVazenja, ImaKategorije FROM dbo.VrsteDokumenta WHERE Sifra = @S", HRBaza.P("@S", sifra));
        }

        private HRTabelaDef DefPregledi()
        {
            return new HRTabelaDef
            {
                Naslov = "Zdravstveni pregled",
                Tabela = "ZaposleniZdravstveniPregledi",
                RoditeljKolona = "Zaposleni",
                Upit = @"SELECT p.Sifra, v.Naziv AS Vrsta, p.DatumPregleda, p.Ustanova, p.BrojIzvestaja, o.Naziv AS Ocena, p.Ogranicenja, p.SledeciPregled,
                                DATEDIFF(DAY, CAST(GETDATE() AS date), p.SledeciPregled) AS DanaDo, p.Napomena
                         FROM dbo.ZaposleniZdravstveniPregledi p
                         JOIN dbo.VrstePregleda v ON v.Sifra = p.VrstaPregleda
                         JOIN dbo.OceneSposobnosti o ON o.Sifra = p.OcenaSposobnosti
                         WHERE p.Zaposleni = @Roditelj ORDER BY p.DatumPregleda DESC",
                Kolone = new[]
                {
                    new HRKolona("Vrsta", "Vrsta pregleda", 140), new HRKolona("DatumPregleda", "Datum", 100, "datum"), new HRKolona("Ustanova", "Ustanova", 180),
                    new HRKolona("Ocena", "Ocena sposobnosti", 200), new HRKolona("Ogranicenja", "Ograničenja", 220),
                    new HRKolona("SledeciPregled", "Sledeći pregled", 120, "datum"), new HRKolona("BrojIzvestaja", "Izveštaj", 100)
                },
                Polja = new List<HRPolje>
                {
                    HRPolje.Lista("VrstaPregleda", "Vrsta pregleda", HRListe.Sifarnik("VrstePregleda"), true),
                    HRPolje.Datum("DatumPregleda", "Datum pregleda", true),
                    HRPolje.Tekst("Ustanova", "Ustanova", 100),
                    HRPolje.Tekst("BrojIzvestaja", "Broj izveštaja", 30),
                    HRPolje.Lista("OcenaSposobnosti", "Ocena sposobnosti", HRListe.Sifarnik("OceneSposobnosti"), true),
                    HRPolje.Datum("SledeciPregled", "Sledeći pregled"),
                    new HRPolje("Ogranicenja", "Ograničenja (npr. ne raditi noćnu smenu)", HRTip.Tekst, false, 250) { Raspon = 2 },
                    HRPolje.Napomena()
                },
                Napomena = "Čuvaju se samo ocena sposobnosti i ograničenja, ne i dijagnoze.",
                NoviPodrazumevano = () => new HRVrednosti { ["DatumPregleda"] = DateTime.Today },
                // predlog sledećeg pregleda: datum pregleda + period vrste pregleda (može ručno)
                PriPromeni = (dlg, kolona) =>
                {
                    if (kolona != "VrstaPregleda" && kolona != "DatumPregleda")
                        return;
                    DateTime? datum = HRBaza.Datum(dlg.Vrednost("DatumPregleda"));
                    object vrsta = dlg.Vrednost("VrstaPregleda");
                    if (!datum.HasValue || vrsta == null)
                        return;
                    int period = HRBaza.CeoBroj(HRBaza.Skalar("SELECT PeriodMeseci FROM dbo.VrstePregleda WHERE Sifra = @S", HRBaza.P("@S", vrsta)));
                    object trenutni = dlg.Vrednost("SledeciPregled");
                    if (period > 0 && (trenutni == null || Equals(trenutni, dlg.Tag)))
                    {
                        DateTime predlog = datum.Value.AddMonths(period);
                        dlg.Postavi("SledeciPregled", predlog);
                        dlg.Tag = predlog;
                    }
                },
                BojaTeksta = (r, k) => BojaDana(r, k, "SledeciPregled")
            };
        }

        private HRTabelaDef DefHTZ()
        {
            return new HRTabelaDef
            {
                Naslov = "HTZ oprema",
                Tabela = "ZaposleniHTZZaduzenja",
                RoditeljKolona = "Zaposleni",
                SamoCitanje = true,
                Upit = @"SELECT s.Sifra, o.Naziv AS Oprema, s.Velicina, s.Kolicina, s.DatumZaduzenja, s.RokZamene, s.DatumRazduzenja, rz.Naziv AS Razlog,
                                r.BrojReversa, r.Sifra AS Revers, DATEDIFF(DAY, CAST(GETDATE() AS date), s.RokZamene) AS DanaDo
                         FROM dbo.ZaposleniHTZZaduzenja s
                         JOIN dbo.HTZOprema o ON o.Sifra = s.HTZOprema
                         JOIN dbo.HTZReversi r ON r.Sifra = s.Revers AND r.Storniran = 0
                         LEFT JOIN dbo.RazloziRazduzenja rz ON rz.Sifra = s.RazlogRazduzenja
                         WHERE s.Zaposleni = @Roditelj ORDER BY s.DatumZaduzenja DESC, s.Sifra DESC",
                Filteri = new List<Tuple<string, string>>
                {
                    Tuple.Create("Zaduženo", "DatumRazduzenja IS NULL"),
                    Tuple.Create("Razduženo", "DatumRazduzenja IS NOT NULL")
                },
                Brojac = dt =>
                {
                    int zaduzeno = dt.Select("DatumRazduzenja IS NULL").Length;
                    return "Zaduženo (" + zaduzeno + ")|Razduženo (" + (dt.Rows.Count - zaduzeno) + ")";
                },
                Kolone = new[]
                {
                    new HRKolona("Oprema", "Oprema", 240), new HRKolona("Velicina", "Vel.", 70), new HRKolona("Kolicina", "Kol.", 70, "kolicina"),
                    new HRKolona("DatumZaduzenja", "Zaduženo", 100, "datum"), new HRKolona("RokZamene", "Rok zamene", 110, "datum"),
                    new HRKolona("BrojReversa", "Revers", 100), new HRKolona("DatumRazduzenja", "Razduženo", 100, "datum"), new HRKolona("Razlog", "Razlog razduženja", 160)
                },
                BojaReda = r => r.GetValue("DatumRazduzenja") == DBNull.Value && r.GetValue("DanaDo") != DBNull.Value && HRBaza.CeoBroj(r.GetValue("DanaDo")) < 0
                    ? HRStil.PozadinaIsteklo : (Color?)null,
                BojaTeksta = (r, k) => r.GetValue("DatumRazduzenja") == DBNull.Value ? BojaDana(r, k, "RokZamene") : null
            };
        }

        private HRTabelaDef DefMentori()
        {
            return new HRTabelaDef
            {
                Naslov = "Mentorstvo",
                Tabela = "ZaposleniMentori",
                RoditeljKolona = "Zaposleni",
                Upit = @"SELECT m.Sifra, LTRIM(RTRIM(ISNULL(z.Prezime, N'') + N' ' + ISNULL(z.Ime, N''))) AS Mentor, m.DatumOd, m.DatumDo, m.Napomena,
                                (SELECT COUNT(*) FROM dbo.ZaposleniMentorBeleske b WHERE b.ZaposleniMentor = m.Sifra) AS Beleski
                         FROM dbo.ZaposleniMentori m JOIN dbo.Zaposleni z ON z.ID = m.Mentor
                         WHERE m.Zaposleni = @Roditelj ORDER BY m.DatumOd DESC",
                Kolone = new[]
                {
                    new HRKolona("Mentor", "Mentor", 220), new HRKolona("DatumOd", "Od", 100, "datum"), new HRKolona("DatumDo", "Do", 100, "datum"),
                    new HRKolona("Beleski", "Beleške", 80, "broj"), new HRKolona("Napomena", "Napomena", 300)
                },
                Polja = new List<HRPolje>
                {
                    HRPolje.Lista("Mentor", "Mentor (samo aktivni zaposleni)", HRListe.AktivniZaposleni, true),
                    HRPolje.Datum("DatumOd", "Datum od", true),
                    HRPolje.Datum("DatumDo", "Datum do"),
                    HRPolje.Napomena()
                },
                Napomena = "Novo mentorstvo automatski zatvara prethodno. Izaberite mentorstvo da vidite beleške.",
                NoviPodrazumevano = () => new HRVrednosti { ["DatumOd"] = DateTime.Today },
                Validacija = v =>
                {
                    if (v.Broj("Mentor") == id)
                        return "Zaposleni ne može biti sam sebi mentor.";
                    return v.Datum("DatumDo").HasValue && v.Datum("DatumDo") < v.Datum("DatumOd") ? "Datum do ne može biti pre datuma od." : null;
                },
                Snimi = (v, sifra) =>
                {
                    HRBaza.Procedura("dbo.HR_ZaposleniMentorSnimi",
                        HRBaza.P("@Sifra", sifra), HRBaza.P("@Zaposleni", id), HRBaza.P("@Mentor", v.Uzmi("Mentor")),
                        HRBaza.P("@DatumOd", v.Uzmi("DatumOd")), HRBaza.P("@DatumDo", v.Uzmi("DatumDo")), HRBaza.P("@Napomena", v.Uzmi("Napomena")),
                        HRBaza.P("@Korisnik", HRBaza.Korisnik), HRBaza.Izlaz("@NovaSifra", SqlDbType.Int));
                    return null;
                },
                BojaTeksta = (r, k) => k == "Mentor" && r.GetValue("DatumDo") == DBNull.Value ? SavaTheme.Primary : (Color?)null
            };
        }

        private HRTabelaDef DefBeleske()
        {
            return new HRTabelaDef
            {
                Naslov = "Beleška mentora",
                Tabela = "ZaposleniMentorBeleske",
                RoditeljKolona = "ZaposleniMentor",
                PorukaBezRoditelja = "Izaberite mentorstvo u tabeli iznad.",
                Upit = @"SELECT b.Sifra, b.Datum, b.Beleska, b.KreiraoKorisnik AS Upisao
                         FROM dbo.ZaposleniMentorBeleske b WHERE b.ZaposleniMentor = @Roditelj ORDER BY b.Datum DESC, b.Sifra DESC",
                Kolone = new[] { new HRKolona("Datum", "Datum", 100, "datum"), new HRKolona("Beleska", "Beleška", 560), new HRKolona("Upisao", "Upisao", 120) },
                Polja = new List<HRPolje>
                {
                    HRPolje.Datum("Datum", "Datum", true),
                    new HRPolje("Beleska", "Beleška", HRTip.VelikiTekst, true, 2000) { Raspon = 2 }
                },
                NoviPodrazumevano = () => new HRVrednosti { ["Datum"] = DateTime.Today }
            };
        }

        private HRTabelaDef DefKontakti()
        {
            return new HRTabelaDef
            {
                Naslov = "Hitni kontakt",
                Tabela = "ZaposleniHitniKontakti",
                RoditeljKolona = "Zaposleni",
                Audit = false,
                Upit = @"SELECT k.Sifra, k.Prioritet, k.ImePrezime, s.Naziv AS Srodstvo, k.Telefon1, k.Telefon2, k.Napomena
                         FROM dbo.ZaposleniHitniKontakti k LEFT JOIN dbo.Srodstva s ON s.Sifra = k.Srodstvo
                         WHERE k.Zaposleni = @Roditelj ORDER BY k.Prioritet, k.ImePrezime",
                Kolone = new[]
                {
                    new HRKolona("Prioritet", "Prioritet", 80, "broj"), new HRKolona("ImePrezime", "Ime i prezime", 220), new HRKolona("Srodstvo", "Srodstvo", 140),
                    new HRKolona("Telefon1", "Telefon 1", 140), new HRKolona("Telefon2", "Telefon 2", 140), new HRKolona("Napomena", "Napomena", 240)
                },
                Polja = new List<HRPolje>
                {
                    HRPolje.Tekst("ImePrezime", "Ime i prezime", 70, true),
                    HRPolje.Lista("Srodstvo", "Srodstvo", HRListe.Sifarnik("Srodstva")),
                    HRPolje.Tekst("Telefon1", "Telefon 1", 20, true),
                    HRPolje.Tekst("Telefon2", "Telefon 2", 20),
                    new HRPolje("Prioritet", "Prioritet (1 = prvi koga zovemo)", HRTip.CeoBroj, true) { Min = 1, Max = 9, Podrazumevano = 1 },
                    HRPolje.Napomena()
                }
            };
        }

        private HRTabelaDef DefPorodica()
        {
            return new HRTabelaDef
            {
                Naslov = "Član porodice",
                Tabela = "ZaposleniClanoviPorodice",
                RoditeljKolona = "Zaposleni",
                Audit = false,
                Upit = @"SELECT c.Sifra, c.Prezime + N' ' + c.Ime AS ImePrezime, s.Naziv AS Srodstvo, c.DatumRodjenja, c.JMBG, c.Napomena
                         FROM dbo.ZaposleniClanoviPorodice c JOIN dbo.Srodstva s ON s.Sifra = c.Srodstvo
                         WHERE c.Zaposleni = @Roditelj ORDER BY c.DatumRodjenja",
                Kolone = new[]
                {
                    new HRKolona("ImePrezime", "Prezime i ime", 220), new HRKolona("Srodstvo", "Srodstvo", 140),
                    new HRKolona("DatumRodjenja", "Datum rođenja", 120, "datum"), new HRKolona("JMBG", "JMBG", 140), new HRKolona("Napomena", "Napomena", 240)
                },
                Polja = new List<HRPolje>
                {
                    HRPolje.Tekst("Ime", "Ime", 35, true),
                    HRPolje.Tekst("Prezime", "Prezime", 35, true),
                    HRPolje.Lista("Srodstvo", "Srodstvo", HRListe.Sifarnik("Srodstva"), true),
                    HRPolje.Datum("DatumRodjenja", "Datum rođenja"),
                    HRPolje.Tekst("JMBG", "JMBG", 13),
                    HRPolje.Napomena()
                }
            };
        }

        private HRTabelaDef DefPovrede()
        {
            return new HRTabelaDef
            {
                Naslov = "Povreda na radu",
                Tabela = "ZaposleniPovrede",
                RoditeljKolona = "Zaposleni",
                Upit = @"SELECT p.Sifra, p.DatumVreme, t.Naziv AS Tezina, ISNULL(RTRIM(s.Opis) + ISNULL(N' · ' + p.MestoOpis, N''), p.MestoOpis) AS Mesto,
                                p.Opis, p.BrojIzvestaja, p.DatumPrijave, p.DanaOdsustva
                         FROM dbo.ZaposleniPovrede p
                         JOIN dbo.TezinePovreda t ON t.Sifra = p.TezinaPovrede
                         LEFT JOIN dbo.Stanice s ON s.ID = p.SluzbenoMesto
                         WHERE p.Zaposleni = @Roditelj ORDER BY p.DatumVreme DESC",
                Kolone = new[]
                {
                    new HRKolona("DatumVreme", "Datum i vreme", 130, "datumvreme"), new HRKolona("Tezina", "Težina", 100), new HRKolona("Mesto", "Mesto", 180),
                    new HRKolona("Opis", "Opis", 300), new HRKolona("BrojIzvestaja", "Izveštaj", 100), new HRKolona("DatumPrijave", "Prijavljeno", 100, "datum"),
                    new HRKolona("DanaOdsustva", "Dana odsustva", 110, "broj")
                },
                Polja = new List<HRPolje>
                {
                    new HRPolje("DatumVreme", "Datum i vreme", HRTip.DatumVreme, true),
                    HRPolje.Lista("TezinaPovrede", "Težina povrede", HRListe.Sifarnik("TezinePovreda"), true),
                    HRPolje.Lista("SluzbenoMesto", "Službeno mesto", HRListe.SluzbenaMesta),
                    HRPolje.Tekst("MestoOpis", "Opis mesta", 100),
                    new HRPolje("Opis", "Opis povrede", HRTip.VelikiTekst, true, 1000) { Raspon = 2 },
                    HRPolje.Tekst("BrojIzvestaja", "Broj izveštaja", 30),
                    HRPolje.Datum("DatumPrijave", "Datum prijave"),
                    HRPolje.Broj("DanaOdsustva", "Dana odsustva", false, 0, 3650),
                    HRPolje.Napomena()
                },
                NoviPodrazumevano = () => new HRVrednosti { ["DatumVreme"] = DateTime.Now }
            };
        }

        private HRTabelaDef DefDisciplinske()
        {
            return new HRTabelaDef
            {
                Naslov = "Disciplinska mera",
                Tabela = "ZaposleniDisciplinske",
                RoditeljKolona = "Zaposleni",
                Upit = @"SELECT d.Sifra, d.Datum, v.Naziv AS VrstaMere, d.Razlog, d.BrojResenja, d.VaziDo, d.Napomena
                         FROM dbo.ZaposleniDisciplinske d JOIN dbo.VrsteDisciplinskihMera v ON v.Sifra = d.VrstaMere
                         WHERE d.Zaposleni = @Roditelj ORDER BY d.Datum DESC",
                Kolone = new[]
                {
                    new HRKolona("Datum", "Datum", 100, "datum"), new HRKolona("VrstaMere", "Mera", 160), new HRKolona("Razlog", "Razlog", 340),
                    new HRKolona("BrojResenja", "Rešenje", 110), new HRKolona("VaziDo", "Važi do", 100, "datum")
                },
                Polja = new List<HRPolje>
                {
                    HRPolje.Datum("Datum", "Datum", true),
                    HRPolje.Lista("VrstaMere", "Vrsta mere", HRListe.Sifarnik("VrsteDisciplinskihMera"), true),
                    new HRPolje("Razlog", "Razlog", HRTip.VelikiTekst, true, 500) { Raspon = 2 },
                    HRPolje.Tekst("BrojResenja", "Broj rešenja", 30),
                    HRPolje.Datum("VaziDo", "Važi do"),
                    HRPolje.Napomena()
                },
                NoviPodrazumevano = () => new HRVrednosti { ["Datum"] = DateTime.Today }
            };
        }

        private HRTabelaDef DefOcene()
        {
            return new HRTabelaDef
            {
                Naslov = "Ocena",
                Tabela = "ZaposleniOcene",
                RoditeljKolona = "Zaposleni",
                Upit = @"SELECT o.Sifra, o.DatumOd, o.DatumDo, LTRIM(RTRIM(ISNULL(z.Prezime, N'') + N' ' + ISNULL(z.Ime, N''))) AS Ocenjivac, o.Ocena, o.Komentar
                         FROM dbo.ZaposleniOcene o JOIN dbo.Zaposleni z ON z.ID = o.Ocenjivac
                         WHERE o.Zaposleni = @Roditelj ORDER BY o.DatumOd DESC",
                Kolone = new[]
                {
                    new HRKolona("DatumOd", "Period od", 100, "datum"), new HRKolona("DatumDo", "Period do", 100, "datum"),
                    new HRKolona("Ocenjivac", "Ocenjivač", 200), new HRKolona("Ocena", "Ocena", 70, "broj"), new HRKolona("Komentar", "Komentar", 380)
                },
                Polja = new List<HRPolje>
                {
                    HRPolje.Datum("DatumOd", "Period od", true),
                    HRPolje.Datum("DatumDo", "Period do", true),
                    HRPolje.Lista("Ocenjivac", "Ocenjivač", HRListe.AktivniZaposleni, true),
                    new HRPolje("Ocena", "Ocena (1–5)", HRTip.CeoBroj, true) { Min = 1, Max = 5 },
                    new HRPolje("Komentar", "Komentar", HRTip.VelikiTekst, false, 2000) { Raspon = 2 }
                },
                Validacija = v => v.Datum("DatumDo") < v.Datum("DatumOd") ? "Period do ne može biti pre perioda od." : null
            };
        }

        // Osnovi godišnjeg odmora zaposlenog (OSN se dodeljuje automatski; ostali po pravilniku)
        private HRTabelaDef DefOsnoviGO()
        {
            return new HRTabelaDef
            {
                Naslov = "Osnov godišnjeg odmora",
                Tabela = "ZaposleniOsnovGO",
                Kljuc = "ID",
                RoditeljKolona = "ZaposleniSifra",
                Audit = false,
                Upit = @"SELECT x.ID, o.Oznaka, o.Naziv, x.BrojDana, x.VaziOd, x.VaziDo, x.Napomena
                         FROM dbo.ZaposleniOsnovGO x JOIN dbo.OsnovGO o ON o.ID = x.OsnovGOID
                         WHERE x.ZaposleniSifra = @Roditelj ORDER BY x.VaziOd DESC, o.Oznaka",
                Kolone = new[]
                {
                    new HRKolona("Oznaka", "Oznaka", 70), new HRKolona("Naziv", "Osnov", 220), new HRKolona("BrojDana", "Dana", 60, "broj"),
                    new HRKolona("VaziOd", "Važi od", 100, "datum"), new HRKolona("VaziDo", "Važi do", 100, "datum"), new HRKolona("Napomena", "Napomena", 260)
                },
                Polja = new List<HRPolje>
                {
                    HRPolje.Lista("OsnovGOID", "Osnov", "SELECT ID, Oznaka + N' – ' + Naziv AS Naziv FROM dbo.OsnovGO WHERE Aktivno = 1 ORDER BY Oznaka", true),
                    HRPolje.Broj("BrojDana", "Broj dana", true, 0, 60),
                    HRPolje.Datum("VaziOd", "Važi od", true),
                    HRPolje.Datum("VaziDo", "Važi do"),
                    HRPolje.Napomena("Napomena", "Napomena", 250)
                },
                NoviPodrazumevano = () => new HRVrednosti { ["VaziOd"] = new DateTime(DateTime.Today.Year, 1, 1) },
                PriPromeni = (dlg, k) =>
                {
                    if (k == "OsnovGOID" && dlg.Vrednost("OsnovGOID") != null && dlg.Vrednost("BrojDana") == null)
                        dlg.Postavi("BrojDana", HRBaza.Skalar("SELECT BrojDana FROM dbo.OsnovGO WHERE ID = @O", HRBaza.P("@O", dlg.Vrednost("OsnovGOID"))));
                },
                Validacija = v => v.Datum("VaziDo").HasValue && v.Datum("VaziDo") < v.Datum("VaziOd") ? "Važi do je pre Važi od." : null,
                Napomena = "Pravo za godinu se računa kao zbir osnova (Godišnji odmori > Generiši za godinu)."
            };
        }

        // Pravo i zahtevi za GO (pregled; zahtev se podnosi i odobrava na formi Zahtevi za GO)
        private HRTabelaDef DefZahteviGO()
        {
            return new HRTabelaDef
            {
                Naslov = "Zahtev za GO",
                Tabela = "GODopustStavke",
                Kljuc = "ID",
                RoditeljKolona = "ZaposleniSifra",
                SamoCitanje = true,
                Upit = @"SELECT s.ID, d.Godina, s.DatumOd, s.DatumDo, s.BrojDana, st.Naziv AS Status, s.StatusID, ISNULL(s.BrojResenja, N'') AS BrojResenja,
                                ISNULL(s.RazlogOdbijanja, s.Napomena) AS Napomena
                         FROM dbo.GODopustStavke s JOIN dbo.GODopust d ON d.ID = s.DopustID JOIN dbo.StatusZahteva st ON st.ID = s.StatusID
                         WHERE d.ZaposleniSifra = @Roditelj ORDER BY s.DatumOd DESC",
                Kolone = new[]
                {
                    new HRKolona("Godina", "Godina", 70, "broj"), new HRKolona("DatumOd", "Od", 100, "datum"), new HRKolona("DatumDo", "Do", 100, "datum"),
                    new HRKolona("BrojDana", "Dana", 60, "broj"), new HRKolona("Status", "Status", 110), new HRKolona("BrojResenja", "Rešenje", 120),
                    new HRKolona("Napomena", "Napomena", 260)
                },
                BojaTeksta = (r, k) => k == "Status" && HRBaza.CeoBroj(r.GetValue("StatusID")) == 2 ? HRStil.StatusIstice
                                     : k == "Status" && HRBaza.CeoBroj(r.GetValue("StatusID")) == 4 ? SavaTheme.Danger : (Color?)null
            };
        }

        private HRTabelaDef DefPrilozi()
        {
            return new HRTabelaDef
            {
                Naslov = "Prilog",
                Tabela = "ZaposleniPrilozi",
                RoditeljKolona = "Zaposleni",
                SamoCitanje = true,
                Upit = @"SELECT Sifra, Naziv, NazivFajla, VezaTip, Datum, CAST(ISNULL(Velicina, DATALENGTH(Sadrzaj)) / 1024 AS int) AS KB, KreiraoKorisnik
                         FROM dbo.ZaposleniPrilozi WHERE Zaposleni = @Roditelj ORDER BY Datum DESC, Sifra DESC",
                Kolone = new[]
                {
                    new HRKolona("Naziv", "Naziv", 240), new HRKolona("NazivFajla", "Fajl", 220), new HRKolona("VezaTip", "Uz", 110),
                    new HRKolona("Datum", "Datum", 100, "datum"), new HRKolona("KB", "KB", 80, "broj"), new HRKolona("KreiraoKorisnik", "Priložio", 120)
                },
                Napomena = "Skenirani ugovori, rešenja, diplome i sertifikati. Dupli klik otvara prilog."
            };
        }

        // Komande koje nisu Dodaj / Izmeni / Obriši: prilozi, HTZ revers, razduženje
        private void PodesiDodatneKomande()
        {
            // Dokumenti: sken uz izabrani dokument
            tDokumenti.DodajKomandu("Otvori sken", (s, e) => OtvoriPrilogUz(tDokumenti, "Dokument"));
            tDokumenti.DodajKomandu("Priloži sken", (s, e) => PriloziUz(tDokumenti, "Dokument"));

            // Prilozi
            tPrilozi.DodajKomandu("Obriši", (s, e) =>
            {
                int p = tPrilozi.IzabraniId();
                if (p <= 0 || MessageBox.Show("Obrisati izabrani prilog?", "Prilog", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
                HRBaza.Izvrsi("DELETE FROM dbo.ZaposleniPrilozi WHERE Sifra = @S", HRBaza.P("@S", p));
                tPrilozi.Osvezi();
                tDokumenti.Osvezi();
            }, "danger");
            tPrilozi.DodajKomandu("Otvori", (s, e) => { if (tPrilozi.IzabraniId() > 0) HRPrilozi.Otvori("ZaposleniPrilozi", tPrilozi.IzabraniId()); });
            tPrilozi.DodajKomandu("+ Dodaj prilog", (s, e) =>
            {
                if (id <= 0) return;
                if (HRPrilozi.Dodaj(this, "ZaposleniPrilozi", "Zaposleni", id, null))
                    tPrilozi.Osvezi();
            });
            HRGrid.DuplikKlik(tPrilozi.Grid, () => { if (tPrilozi.IzabraniId() > 0) HRPrilozi.Otvori("ZaposleniPrilozi", tPrilozi.IzabraniId()); });

            // HTZ: revers, razduženje, lista reversa
            tGO.DodajKomandu("Zahtevi za GO", (s, e) => { if (id > 0) HRStil.Otvori(this, new frmHRGOZahtevi(id)); });
            tGO.DodajKomandu("+ Novi zahtev", (s, e) => { if (id > 0 && HRGO.NoviZahtev(this, id, null, null, hr, ja)) { tGO.Osvezi(); OsveziNapomene(); } });
            tHTZ.DodajKomandu("Reversi",(s, e) => { if (id > 0) { frmHRRevers.Lista(this, id); tHTZ.Osvezi(); OsveziNapomene(); OsveziZaglavlje(); } });
            tHTZ.DodajKomandu("Razduži", (s, e) => Razduzi());
            tHTZ.DodajKomandu("+ Novi revers", (s, e) =>
            {
                if (id <= 0)
                {
                    MessageBox.Show("Prvo sačuvajte osnovne podatke.", "HTZ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                using (var f = new frmHRRevers(id, 0))
                    f.ShowDialog(this);
                tHTZ.Osvezi();
                OsveziNapomene();
                OsveziZaglavlje();
            });

            // mentorstvo -> beleške
            tMentori.Grid.SelectedRecordsChanged += (s, e) => tBeleske.Ucitaj(tMentori.IzabraniId());
            tMentori.Promenjeno += () => { OsveziZaglavlje(); tBeleske.Ucitaj(tMentori.IzabraniId()); };
            tBeleske.Promenjeno += () => tMentori.Osvezi();

            // promene koje utiču na zaglavlje i upozorenja
            tRadnaMesta.Promenjeno += () => { OsveziZaglavlje(); OsveziNapomene(); };
            tUgovori.Promenjeno += OsveziZaglavlje;
            tDokumenti.Promenjeno += () => { OsveziZaglavlje(); OsveziNapomene(); };
            foreach (HRTabelaKartica t in tabele)
                if (t.Def.Tabela == "ZaposleniZdravstveniPregledi")
                    t.Promenjeno += OsveziZaglavlje;
        }

        private void PriloziUz(HRTabelaKartica t, string vezaTip)
        {
            int sifra = t.IzabraniId();
            if (sifra <= 0)
            {
                MessageBox.Show("Izaberite red uz koji se prilaže sken.", vezaTip, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string predlog = Convert.ToString(HRGrid.Vrednost(t.Grid, "Vrsta")) + " " + Convert.ToString(HRGrid.Vrednost(t.Grid, "BrojDokumenta"));
            if (HRPrilozi.Dodaj(this, "ZaposleniPrilozi", "Zaposleni", id, predlog.Trim(),
                    new KeyValuePair<string, object>("VezaTip", vezaTip), new KeyValuePair<string, object>("VezaSifra", sifra)))
            {
                t.Osvezi();
                tPrilozi.Osvezi();
            }
        }

        private void OtvoriPrilogUz(HRTabelaKartica t, string vezaTip)
        {
            int sifra = t.IzabraniId();
            if (sifra <= 0)
                return;
            object prilog = HRBaza.Skalar("SELECT TOP (1) Sifra FROM dbo.ZaposleniPrilozi WHERE Zaposleni = @Z AND VezaTip = @T AND VezaSifra = @S ORDER BY Sifra DESC",
                HRBaza.P("@Z", id), HRBaza.P("@T", vezaTip), HRBaza.P("@S", sifra));
            if (prilog == null)
                MessageBox.Show("Uz izabrani red nema priloženog skena.", vezaTip, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                HRPrilozi.Otvori("ZaposleniPrilozi", HRBaza.CeoBroj(prilog));
        }

        private void Razduzi()
        {
            int stavka = tHTZ.IzabraniId();
            if (stavka <= 0 || HRGrid.Vrednost(tHTZ.Grid, "DatumRazduzenja") != DBNull.Value)
            {
                MessageBox.Show("Izaberite zaduženu opremu.", "Razduženje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var polja = new[]
            {
                HRPolje.Datum("Datum", "Datum razduženja", true),
                HRPolje.Lista("Razlog", "Razlog razduženja", HRListe.Sifarnik("RazloziRazduzenja"), true)
            };
            using (var dlg = new frmHRStavka("Razduženje: " + HRGrid.Vrednost(tHTZ.Grid, "Oprema"), polja, new HRVrednosti { ["Datum"] = DateTime.Today }, 1))
            {
                dlg.Snimanje = v =>
                {
                    HRBaza.Procedura("dbo.HR_HTZRazduzi", HRBaza.P("@Stavka", stavka), HRBaza.P("@Datum", v.Uzmi("Datum")), HRBaza.P("@Razlog", v.Uzmi("Razlog")));
                    return null;
                };
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    tHTZ.Osvezi();
                    OsveziNapomene();
                    OsveziZaglavlje();
                }
            }
        }

        // Napomene ispod tabela koje zavise od podataka (HTZ za zamenu, obavezni dokumenti radnog mesta)
        private void OsveziNapomene()
        {
            if (id <= 0)
                return;
            try
            {
                var zamena = HRBaza.Tabela(@"
                    SELECT o.Naziv FROM dbo.ZaposleniHTZZaduzenja s
                    JOIN dbo.HTZOprema o ON o.Sifra = s.HTZOprema
                    JOIN dbo.HTZReversi r ON r.Sifra = s.Revers AND r.Storniran = 0
                    WHERE s.Zaposleni = @Z AND s.DatumRazduzenja IS NULL AND s.RokZamene < CAST(GETDATE() AS date)", HRBaza.P("@Z", id));
                if (zamena.Rows.Count > 0)
                    tHTZ.PostaviNapomenu("! Rok zamene je prošao: " + string.Join(", ", zamena.Rows.Cast<DataRow>().Select(r => Convert.ToString(r[0]))) + " treba zameniti", SavaTheme.Danger);
                else
                {
                    string kb = osnovno.Vrednost("KonfekcijskiBroj") as string, ob = osnovno.Vrednost("BrojObuce") as string;
                    tHTZ.PostaviNapomenu("Veličina se predlaže iz podataka zaposlenog (" + (kb ?? "–") + " / " + (ob ?? "–") + "), a rok zamene iz šifarnika HTZ opreme");
                }

                DataRow go = HRBaza.Red("SELECT UkupnoDana, PotrosenoDana, PreostaloDana, RokKoriscenja FROM dbo.GODopust WHERE ZaposleniSifra = @Z AND Godina = @G",
                    HRBaza.P("@Z", id), HRBaza.P("@G", DateTime.Today.Year));
                tGO.PostaviNapomenu(go == null
                    ? "Pravo za " + DateTime.Today.Year + ". nije generisano (Godišnji odmori > Generiši za godinu)."
                    : DateTime.Today.Year + ": ukupno " + go["UkupnoDana"] + ", potrošeno " + go["PotrosenoDana"] + ", preostalo " + go["PreostaloDana"] +
                      " (rok " + HRBaza.DatumTekst(go["RokKoriscenja"]) + ")");

                var nedostaje = HRBaza.Tabela("SELECT StaIstice FROM dbo.vZaposleniRokovi WHERE Zaposleni = @Z AND Vrsta = N'Nedostaje'", HRBaza.P("@Z", id));
                if (nedostaje.Rows.Count > 0)
                    tDokumenti.PostaviNapomenu("Radno mesto zahteva dokumente koji nedostaju ili su istekli: " +
                        string.Join(", ", nedostaje.Rows.Cast<DataRow>().Select(r => Convert.ToString(r[0]).Replace("Nedostaje: ", ""))), SavaTheme.Danger);
                else
                    tDokumenti.PostaviNapomenu("Istekli i dokumenti koji uskoro ističu prikazuju se na vrhu liste.");
            }
            catch
            {
                // napomene su pomoćne; greška u njima ne sme da prekine rad
            }
        }
    }
}
