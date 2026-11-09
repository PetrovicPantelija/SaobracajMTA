using Saobracaj.Administracija;
using Saobracaj.Theme;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.HR
{
    // Mesečna evidencija rada (specifikacija 5): red je zaposleni, kolona je dan, ćelija prikazuje skraćeni upis.
    // Unos po izuzetku: "Popuni mesec" upiše redovan rad, praznike i odobrene odmore, a ovde se ispravljaju odstupanja.
    public class frmHREvidencijaRada : Form
    {
        private class RedZaposlenog
        {
            public int ID;
            public string Naziv;
            public decimal[] Raspored;            // sati po danu u nedelji (0 = pon); null = podrazumevani fond pon–pet
            public DateTime? Ulazak, Izlazak;
            public int Status;                    // 0 otvoren, 1 potvrđen, 2 zaključen
            public Dictionary<int, List<HRStavkaDana>> Dani = new Dictionary<int, List<HRStavkaDana>>();
            public HashSet<int> Beleske = new HashSet<int>();
        }

        private const int KolonaPrvogDana = 1;
        private static readonly Color Neradni = ColorTranslator.FromHtml("#E9EDF1");
        private static readonly Color IspodFonda = ColorTranslator.FromHtml("#FBE3E3");
        private static readonly Color VanRadnogOdnosa = ColorTranslator.FromHtml("#F4F7FA");

        private readonly DataGridView dgv;
        private readonly ComboBox cboMesec, cboGodina;
        private readonly HRChip chipJedinica;
        private readonly Label statusna;
        private readonly HRTekst info;
        private readonly Button btnZakljuci, btnOtkljucaj;
        private readonly ContextMenuStrip meni;

        private int godina, mesec, brojDana;
        private readonly bool hr, mozeZakljucati;
        private readonly int ja;
        private int jedinica;
        private string jedinicaNaziv = "sve";
        private decimal podrazumevaniFond = 8;
        private Dictionary<string, HRVrstaRada> vrste = new Dictionary<string, HRVrstaRada>();
        private readonly List<RedZaposlenog> redovi = new List<RedZaposlenog>();
        private readonly Dictionary<DateTime, string> praznici = new Dictionary<DateTime, string>();
        private List<DataGridViewCell> celijeUnosa;
        private bool puni;

        public frmHREvidencijaRada()
        {
            HRStil.PripremiFormu(this, "Evidencija rada");
            ja = HRPrava.TrenutniZaposleni();
            hr = HRPrava.JeHR();
            mozeZakljucati = HRPrava.ImaPravo(HRPrava.ZakljucavanjeMeseca);
            godina = DateTime.Today.Year;
            mesec = DateTime.Today.Month;

            dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.CellSelect,
                MultiSelect = true,
                EditMode = DataGridViewEditMode.EditOnKeystroke,
                BorderStyle = BorderStyle.None
            };
            dgv.CellFormatting += Dgv_CellFormatting;
            dgv.CellPainting += Dgv_CellPainting;
            dgv.CellBeginEdit += Dgv_CellBeginEdit;
            dgv.CellEndEdit += Dgv_CellEndEdit;
            dgv.KeyDown += Dgv_KeyDown;
            dgv.CellDoubleClick += (s, e) => { if (JeDan(e.ColumnIndex) && e.RowIndex >= 0) OtvoriDan(dgv[e.ColumnIndex, e.RowIndex]); };
            dgv.CellMouseDown += Dgv_CellMouseDown;
            dgv.DataError += (s, e) => e.ThrowException = false;

            meni = new ContextMenuStrip { Font = SavaTheme.FieldFont };
            meni.Opening += Meni_Opening;
            dgv.ContextMenuStrip = meni;

            var okvir = HRStil.BeliPanel();
            okvir.Dock = DockStyle.Fill;
            okvir.Controls.Add(dgv);
            var telo = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 12, 20, 0), BackColor = SavaTheme.Surface };
            telo.Controls.Add(okvir);

            var alati = HRStil.AlatnaTraka(this);
            alati.Dugme("‹", (s, e) => PomeriMesec(-1)).Width = 36;
            cboMesec = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 120, Margin = new Padding(4, 2, 4, 0) };
            for (int m = 1; m <= 12; m++)
                cboMesec.Items.Add(HREvidencija.ImeMeseca(m));
            cboGodina = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 80, Margin = new Padding(0, 2, 4, 0) };
            for (int g = DateTime.Today.Year - 3; g <= DateTime.Today.Year + 1; g++)
                cboGodina.Items.Add(g);
            alati.Levo.Controls.Add(cboMesec);
            alati.Levo.Controls.Add(cboGodina);
            alati.Dugme("›", (s, e) => PomeriMesec(1)).Width = 36;
            cboMesec.SelectedIndexChanged += (s, e) => { if (!puni) { mesec = cboMesec.SelectedIndex + 1; Ucitaj(); } };
            cboGodina.SelectedIndexChanged += (s, e) => { if (!puni) { godina = (int)cboGodina.SelectedItem; Ucitaj(); } };
            chipJedinica = alati.Chip("Org. jedinica: sve", false, (s, e) => IzaberiJedinicu());
            chipJedinica.Margin = new Padding(12, 0, 8, 0);
            chipJedinica.Visible = hr;
            alati.Dugme("Grupni unos", (s, e) => GrupniUnos());
            alati.Dugme("Potvrdi mesec", (s, e) => PromeniStatus(1));
            btnZakljuci = alati.Dugme("Zaključi mesec", (s, e) => PromeniStatus(2));
            btnOtkljucaj = alati.Dugme("Otključaj mesec", (s, e) => Otkljucaj());
            btnZakljuci.Visible = btnOtkljucaj.Visible = mozeZakljucati;
            alati.Dugme("Izvoz za obračun", (s, e) => IzvozZaObracun());
            alati.Dugme("Osveži", (s, e) => Ucitaj());
            alati.DugmeDesno("Popuni mesec", (s, e) => PopuniMesec());

            var naslov = HRStil.NaslovnaTraka(this, "Zaposleni › Evidencija rada");
            info = naslov.DodajInfo("");
            statusna = HRStil.StatusnaLinija(this);
            statusna.Text = "Upis: 8 · 7,5K · 8+2 · 4P · BO, PL, NO, SD, SP, IZ · GO otvara zahtev · Delete briše ručni unos · F2 ili dvoklik: prozor Dan · desni klik: vrsta, beleška";

            Controls.Add(telo);
            telo.BringToFront();
            HRStil.PrimeniTemu(this);
            dgv.ColumnHeadersHeight = 46;
            dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.RowTemplate.Height = 30;

            VisibleChanged += (s, e) => { if (Visible) Ucitaj(); };
        }

        // ------------------------------------------------------------------ učitavanje

        private DateTime PrviDan => new DateTime(godina, mesec, 1);
        private bool JeDan(int kolona) => kolona >= KolonaPrvogDana && kolona < KolonaPrvogDana + brojDana;
        private DateTime DatumKolone(int kolona) => PrviDan.AddDays(kolona - KolonaPrvogDana);
        private RedZaposlenog RedCelije(DataGridViewCell c) => (RedZaposlenog)dgv.Rows[c.RowIndex].Tag;

        private void Ucitaj()
        {
            puni = true;
            cboMesec.SelectedIndex = mesec - 1;
            if (!cboGodina.Items.Contains(godina))
                cboGodina.Items.Add(godina);
            cboGodina.SelectedItem = godina;
            puni = false;
            chipJedinica.Text = "Org. jedinica: " + jedinicaNaziv;
            chipJedinica.Aktivan = jedinica != 0;

            if (!hr && ja == 0)
            {
                dgv.Columns.Clear();
                info.Text = "";
                MessageBox.Show("Prijavljeni korisnik nije povezan sa zaposlenim (Korisnici.ZaposleniSifra), pa program ne zna čiji je tim.\n" +
                                "Vezu upišite na formi zaposlenog (Korisničko ime u aplikaciji) ili u tabeli Korisnici.",
                    "Evidencija rada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DateTime od = PrviDan, @do = PrviDan.AddMonths(1).AddDays(-1);
            brojDana = DateTime.DaysInMonth(godina, mesec);
            try
            {
                vrste = HRVrstaRada.Ucitaj();
                podrazumevaniFond = 8;
                if (Podesavanja.Ucitano && Podesavanja.Postoji("HR.DnevniFondSati"))
                    podrazumevaniFond = Podesavanja.GetDecimal("HR.DnevniFondSati");

                praznici.Clear();
                foreach (DataRow r in HRBaza.Tabela("SELECT Datum, Naziv FROM dbo.Praznici WHERE Neradni = 1 AND Datum BETWEEN @Od AND @Do",
                             HRBaza.P("@Od", od), HRBaza.P("@Do", @do)).Rows)
                    praznici[Convert.ToDateTime(r["Datum"]).Date] = Convert.ToString(r["Naziv"]);

                string tim = HRPrava.FilterTima("z.ID", hr, ja) + (jedinica != 0 ? " AND t.OrganizacionaJedinica = " + jedinica : "");
                DataTable zap = HRBaza.Tabela(@"
                    SELECT z.ID, t.PrezimeIme, z.DatumUlaska, z.DatumIzlaska, r.Pon, r.Uto, r.Sre, r.Cet, r.Pet, r.Sub, r.Ned,
                           ISNULL(m.Status, 0) AS StatusMeseca
                    FROM dbo.Zaposleni z
                    JOIN dbo.vZaposleniTrenutno t ON t.Zaposleni = z.ID
                    LEFT JOIN dbo.RasporedRada r ON r.ID = z.RasporedRadaID
                    LEFT JOIN dbo.EvidencijaMesec m ON m.ZaposleniSifra = z.ID AND m.Godina = @G AND m.Mesec = @M
                    WHERE (z.Status = 'A' OR z.DatumIzlaska >= @Od)
                      AND (z.DatumUlaska IS NULL OR z.DatumUlaska <= @Do)
                      AND (z.DatumIzlaska IS NULL OR z.DatumIzlaska >= @Od)
                      AND " + tim + @"
                    ORDER BY t.PrezimeIme",
                    HRBaza.P("@G", godina), HRBaza.P("@M", mesec), HRBaza.P("@Od", od), HRBaza.P("@Do", @do));

                redovi.Clear();
                var poId = new Dictionary<int, RedZaposlenog>();
                foreach (DataRow r in zap.Rows)
                {
                    var red = new RedZaposlenog
                    {
                        ID = HRBaza.CeoBroj(r["ID"]),
                        Naziv = Convert.ToString(r["PrezimeIme"]),
                        Ulazak = HRBaza.Datum(r["DatumUlaska"]),
                        Izlazak = HRBaza.Datum(r["DatumIzlaska"]),
                        Status = HRBaza.CeoBroj(r["StatusMeseca"])
                    };
                    if (r["Pon"] != DBNull.Value)
                        red.Raspored = new[] { "Pon", "Uto", "Sre", "Cet", "Pet", "Sub", "Ned" }.Select(k => HRBaza.Decimalni(r[k])).ToArray();
                    redovi.Add(red);
                    poId[red.ID] = red;
                }

                foreach (DataRow r in HRBaza.Tabela(@"
                    SELECT e.ID, e.ZaposleniSifra, e.Datum, v.Oznaka, e.Sati, e.Izvor
                    FROM dbo.EvidencijaRada e JOIN dbo.VrstaRada v ON v.ID = e.VrstaRadaID
                    JOIN dbo.Zaposleni z ON z.ID = e.ZaposleniSifra
                    WHERE e.Datum BETWEEN @Od AND @Do AND " + HRPrava.FilterTima("z.ID", hr, ja) + @"
                    ORDER BY e.Datum, v.Redosled", HRBaza.P("@Od", od), HRBaza.P("@Do", @do)).Rows)
                {
                    if (!poId.TryGetValue(HRBaza.CeoBroj(r["ZaposleniSifra"]), out RedZaposlenog red))
                        continue;
                    int dan = Convert.ToDateTime(r["Datum"]).Day;
                    if (!red.Dani.TryGetValue(dan, out List<HRStavkaDana> lista))
                        red.Dani[dan] = lista = new List<HRStavkaDana>();
                    lista.Add(new HRStavkaDana
                    {
                        ID = HRBaza.CeoBroj(r["ID"]),
                        Oznaka = Convert.ToString(r["Oznaka"]).Trim().ToUpperInvariant(),
                        Sati = HRBaza.Decimalni(r["Sati"]),
                        Izvor = Convert.ToString(r["Izvor"])[0]
                    });
                }

                foreach (DataRow r in HRBaza.Tabela(@"
                    SELECT DISTINCT b.ZaposleniSifra, b.Datum FROM dbo.BeleskaORadu b
                    WHERE b.Datum BETWEEN @Od AND @Do AND " + HRBeleske.Vidljivost(hr, ja), HRBaza.P("@Od", od), HRBaza.P("@Do", @do)).Rows)
                    if (poId.TryGetValue(HRBaza.CeoBroj(r["ZaposleniSifra"]), out RedZaposlenog red))
                        red.Beleske.Add(Convert.ToDateTime(r["Datum"]).Day);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Evidencija nije učitana. Proverite da li je na bazi pokrenuta skripta HR_Modul_Faza2_2026_10.sql.\n\n" + ex.Message,
                    "Evidencija rada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Prikazi();
        }

        private void Prikazi()
        {
            dgv.SuspendLayout();
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            var kZap = new DataGridViewTextBoxColumn { HeaderText = "Zaposleni", Width = 200, ReadOnly = true, Frozen = true, SortMode = DataGridViewColumnSortMode.NotSortable };
            dgv.Columns.Add(kZap);
            for (int d = 1; d <= brojDana; d++)
            {
                DateTime datum = new DateTime(godina, mesec, d);
                int dun = HREvidencija.DanUNedelji(datum);
                var k = new DataGridViewTextBoxColumn
                {
                    HeaderText = HREvidencija.DaniNedelje[dun] + "\n" + d,
                    Width = 50,
                    SortMode = DataGridViewColumnSortMode.NotSortable,
                    MaxInputLength = 12
                };
                k.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (dun >= 5 || praznici.ContainsKey(datum))
                {
                    k.HeaderCell.Style.BackColor = ColorTranslator.FromHtml("#DCE3EA");
                    k.HeaderCell.ToolTipText = praznici.TryGetValue(datum, out string p) ? p : null;
                }
                dgv.Columns.Add(k);
            }
            foreach (string naslov in new[] { "Ukupno (h)", "Fond (h)", "Razlika", "Status" })
            {
                var k = new DataGridViewTextBoxColumn { HeaderText = naslov, Width = naslov == "Status" ? 100 : 80, ReadOnly = true, SortMode = DataGridViewColumnSortMode.NotSortable };
                k.DefaultCellStyle.Alignment = naslov == "Status" ? DataGridViewContentAlignment.MiddleLeft : DataGridViewContentAlignment.MiddleRight;
                dgv.Columns.Add(k);
            }

            int crvenih = 0;
            foreach (RedZaposlenog red in redovi)
            {
                var vrednosti = new object[brojDana + 5];
                vrednosti[0] = red.Naziv;
                decimal ukupno = 0, fond = 0, pr = 0, ks = 0;
                for (int d = 1; d <= brojDana; d++)
                {
                    DateTime datum = new DateTime(godina, mesec, d);
                    red.Dani.TryGetValue(d, out List<HRStavkaDana> stavke);
                    vrednosti[d] = UOdnosu(red, datum) ? HREvidencija.Prikaz(stavke, vrste) : "–";
                    decimal f = FondRasporeda(red, datum);
                    fond += f;
                    decimal dnevno = SatiUFond(stavke);
                    ukupno += dnevno;
                    if (stavke != null)
                    {
                        pr += stavke.Where(s => s.Oznaka == "PR" || s.Oznaka == "RP").Sum(s => s.Sati);
                        ks += stavke.Where(s => s.Oznaka == "KS").Sum(s => s.Sati);
                    }
                    if (JeIspodFonda(red, datum, stavke))
                        crvenih++;
                }
                decimal razlika = ukupno - fond;
                vrednosti[brojDana + 1] = HREvidencija.Broj(ukupno);
                vrednosti[brojDana + 2] = HREvidencija.Broj(fond);
                vrednosti[brojDana + 3] = (razlika > 0 ? "+" : razlika < 0 ? "−" : "") + HREvidencija.Broj(Math.Abs(razlika))
                                          + (razlika > 0 && pr > 0 ? " PR" : razlika < 0 && ks > 0 ? " KS" : "");
                vrednosti[brojDana + 4] = red.Status == 2 ? "Zaključen" : red.Status == 1 ? "Potvrđen" : "Otvoren";
                int i = dgv.Rows.Add(vrednosti);
                dgv.Rows[i].Tag = red;
                if (red.Status == 2)
                    dgv.Rows[i].ReadOnly = true;
            }
            dgv.ResumeLayout();
            info.Text = HREvidencija.ImeMeseca(mesec) + " " + godina + " · " + redovi.Count + " zaposlenih · " + crvenih + " dana ispod fonda";
        }

        private static bool UOdnosu(RedZaposlenog red, DateTime datum)
        {
            return (!red.Ulazak.HasValue || datum >= red.Ulazak.Value.Date) && (!red.Izlazak.HasValue || datum <= red.Izlazak.Value.Date);
        }

        // Sati po rasporedu za taj dan u nedelji (praznik se računa u fond; pokriva ga DP)
        private decimal FondRasporeda(RedZaposlenog red, DateTime datum)
        {
            if (!UOdnosu(red, datum))
                return 0;
            int dun = HREvidencija.DanUNedelji(datum);
            if (red.Raspored != null)
                return red.Raspored[dun];
            return dun < 5 ? podrazumevaniFond : 0;
        }

        private decimal SatiUFond(List<HRStavkaDana> stavke)
        {
            if (stavke == null)
                return 0;
            return stavke.Where(s => vrste.TryGetValue(s.Oznaka, out HRVrstaRada v) && v.UlaziUFond).Sum(s => s.Sati);
        }

        private bool JeIspodFonda(RedZaposlenog red, DateTime datum, List<HRStavkaDana> stavke)
        {
            decimal f = FondRasporeda(red, datum);
            if (f <= 0)
                return false;
            if ((stavke == null || stavke.Count == 0) && datum >= DateTime.Today)
                return false;
            return SatiUFond(stavke) < f;
        }

        // ------------------------------------------------------------------ izgled ćelija

        private void Dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || !JeDan(e.ColumnIndex))
                return;
            RedZaposlenog red = (RedZaposlenog)dgv.Rows[e.RowIndex].Tag;
            DateTime datum = DatumKolone(e.ColumnIndex);
            red.Dani.TryGetValue(datum.Day, out List<HRStavkaDana> stavke);
            Color? boja = null;
            if (!UOdnosu(red, datum))
                boja = VanRadnogOdnosa;
            else if (JeIspodFonda(red, datum, stavke))
                boja = IspodFonda;
            else if (stavke != null && stavke.Count > 0 && vrste.TryGetValue(stavke[0].Oznaka, out HRVrstaRada v) && v.CeoDan && v.Boja.HasValue)
                boja = v.Boja;
            else if (HREvidencija.DanUNedelji(datum) >= 5 || praznici.ContainsKey(datum))
                boja = Neradni;
            if (boja.HasValue)
                e.CellStyle.BackColor = boja.Value;
            if (red.Status == 2)
                e.CellStyle.ForeColor = SavaTheme.Label;
        }

        // Ćelija sa beleškom ima mali trougao u gornjem desnom uglu (kao komentar u Excelu)
        private void Dgv_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || !JeDan(e.ColumnIndex))
                return;
            RedZaposlenog red = (RedZaposlenog)dgv.Rows[e.RowIndex].Tag;
            if (!red.Beleske.Contains(DatumKolone(e.ColumnIndex).Day))
                return;
            e.Paint(e.ClipBounds, DataGridViewPaintParts.All);
            Rectangle r = e.CellBounds;
            using (var b = new SolidBrush(HRStil.StatusIstice))
                e.Graphics.FillPolygon(b, new[] { new Point(r.Right - 9, r.Top + 1), new Point(r.Right - 1, r.Top + 1), new Point(r.Right - 1, r.Top + 9) });
            e.Handled = true;
        }

        // ------------------------------------------------------------------ unos

        private void Dgv_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (!JeDan(e.ColumnIndex))
            {
                e.Cancel = true;
                return;
            }
            RedZaposlenog red = (RedZaposlenog)dgv.Rows[e.RowIndex].Tag;
            if (red.Status == 2)
            {
                e.Cancel = true;
                MessageBox.Show("Mesec je zaključen; izmene su moguće tek posle otključavanja.", "Evidencija rada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            celijeUnosa = IzabraneCelije();
            DataGridViewCell tekuca = dgv[e.ColumnIndex, e.RowIndex];
            if (!celijeUnosa.Contains(tekuca))
                celijeUnosa = new List<DataGridViewCell> { tekuca };
            tekuca.Tag = Convert.ToString(tekuca.Value);
        }

        private void Dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell c = dgv[e.ColumnIndex, e.RowIndex];
            string unos = Convert.ToString(c.Value);
            if (unos == Convert.ToString(c.Tag) || celijeUnosa == null)
                return;
            List<DataGridViewCell> celije = celijeUnosa;
            celijeUnosa = null;
            BeginInvoke((Action)(() => Upisi(unos, celije)));
        }

        private List<DataGridViewCell> IzabraneCelije()
        {
            return dgv.SelectedCells.Cast<DataGridViewCell>().Where(c => JeDan(c.ColumnIndex))
                      .OrderBy(c => c.RowIndex).ThenBy(c => c.ColumnIndex).ToList();
        }

        // Upis u jednu ili više ćelija; u opsegu se vikendi i praznici preskaču
        private void Upisi(string unos, List<DataGridViewCell> celije)
        {
            bool opseg = celije.Count > 1;
            var greske = new List<string>();
            RedZaposlenog goRed = null, boRed = null;
            DateTime goOd = DateTime.MaxValue, goDo = DateTime.MinValue, boDatum = DateTime.MinValue;
            Cursor = Cursors.WaitCursor;
            try
            {
                foreach (DataGridViewCell c in celije)
                {
                    RedZaposlenog red = RedCelije(c);
                    DateTime datum = DatumKolone(c.ColumnIndex);
                    if (!UOdnosu(red, datum))
                        continue;
                    if (red.Status == 2)
                    {
                        greske.Add(red.Naziv + ": mesec je zaključen.");
                        continue;
                    }
                    decimal fond = FondRasporeda(red, datum);
                    bool praznik = praznici.ContainsKey(datum);
                    bool neradan = fond <= 0 || praznik;
                    string greska = HREvidencija.Protumaci(unos, fond, neradan, praznik, vrste, podrazumevaniFond, out HRUnosDana u);
                    if (greska != null)
                    {
                        greske.Add(greska);
                        break;
                    }
                    if (u.ZahtevGO)
                    {
                        if (goRed == null || goRed == red)
                        {
                            goRed = red;
                            if (datum < goOd) goOd = datum;
                            if (datum > goDo) goDo = datum;
                        }
                        continue;
                    }
                    if (opseg && neradan && !u.Brisanje)
                        continue;
                    try
                    {
                        UpisiDan(red.ID, datum, u);
                        if (u.Oznaka1 == "BO" && boRed == null)
                        {
                            boRed = red;
                            boDatum = datum;
                        }
                    }
                    catch (SqlException ex)
                    {
                        greske.Add(red.Naziv + " " + datum.ToString("dd.MM.") + ": " + ex.Message);
                    }
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            Ucitaj();

            if (greske.Count > 0)
                MessageBox.Show(string.Join("\n", greske.Distinct().Take(10)), "Evidencija rada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (goRed != null)
            {
                if (HRGO.NoviZahtev(this, goRed.ID, goOd, goDo, hr, ja))
                    Ucitaj();
            }
            if (boRed != null && MessageBox.Show("Upisano je bolovanje. Priložiti doznaku?", "Bolovanje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                HRPrilozi.Dodaj(this, "ZaposleniPrilozi", "Zaposleni", boRed.ID, "Doznaka " + boDatum.ToString("dd.MM.yyyy"),
                    new KeyValuePair<string, object>("VezaTip", "Bolovanje"));
        }

        public static void UpisiDan(int zaposleni, DateTime datum, HRUnosDana u)
        {
            HRBaza.Procedura("dbo.sp_EvidencijaUpisDan",
                HRBaza.P("@ZaposleniSifra", zaposleni), HRBaza.P("@Datum", datum.Date),
                HRBaza.P("@Oznaka1", u.Brisanje ? null : u.Oznaka1), HRBaza.P("@Sati1", u.Brisanje ? (object)null : u.Sati1),
                HRBaza.P("@Oznaka2", u.Brisanje ? null : u.Oznaka2), HRBaza.P("@Sati2", u.Brisanje || u.Oznaka2 == null ? (object)null : u.Sati2),
                HRBaza.P("@Korisnik", HRBaza.Korisnik));
        }

        private void Dgv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                List<DataGridViewCell> celije = IzabraneCelije();
                if (celije.Count > 0)
                    Upisi("", celije);
            }
            else if (e.KeyCode == Keys.F2 && dgv.CurrentCell != null && JeDan(dgv.CurrentCell.ColumnIndex))
            {
                e.Handled = true;
                OtvoriDan(dgv.CurrentCell);
            }
        }

        private void Dgv_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            DataGridViewCell c = dgv[e.ColumnIndex, e.RowIndex];
            if (!c.Selected)
            {
                dgv.ClearSelection();
                c.Selected = true;
                dgv.CurrentCell = c;
            }
        }

        private void Meni_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            meni.Items.Clear();
            List<DataGridViewCell> celije = IzabraneCelije();
            if (celije.Count == 0)
            {
                e.Cancel = true;
                return;
            }
            var upisi = new ToolStripMenuItem("Upiši vrstu");
            foreach (HRVrstaRada v in vrste.Values.Where(x => x.CeoDan || x.Oznaka == "RR"))
            {
                string oznaka = v.Oznaka;
                upisi.DropDownItems.Add(v.Oznaka + " – " + v.Naziv, null, (s, a) => Upisi(oznaka, IzabraneCelije()));
            }
            meni.Items.Add(upisi);
            meni.Items.Add("Dan… (F2)", null, (s, a) => OtvoriDan(celije[0]));
            meni.Items.Add("Dodaj belešku…", null, (s, a) => DodajBelesku(celije[0]));
            meni.Items.Add("Obriši ručni unos (Delete)", null, (s, a) => Upisi("", IzabraneCelije()));
            meni.Items.Add(new ToolStripSeparator());
            meni.Items.Add("Grupni unos…", null, (s, a) => GrupniUnos());
        }

        private void OtvoriDan(DataGridViewCell c)
        {
            RedZaposlenog red = RedCelije(c);
            DateTime datum = DatumKolone(c.ColumnIndex);
            using (var f = new frmHRDan(red.ID, red.Naziv, datum, red.Status == 2, hr, ja))
                f.ShowDialog(this);
            Ucitaj();
        }

        private void DodajBelesku(DataGridViewCell c)
        {
            RedZaposlenog red = RedCelije(c);
            DateTime datum = DatumKolone(c.ColumnIndex);
            var t = new HRTabelaKartica(HRBeleske.Def(red.ID, datum, ja, hr));
            t.Ucitaj(red.ID);
            t.Dodaj();
            t.Dispose();
            Ucitaj();
        }

        // ------------------------------------------------------------------ komande

        private void PomeriMesec(int pomak)
        {
            DateTime d = PrviDan.AddMonths(pomak);
            godina = d.Year;
            mesec = d.Month;
            Ucitaj();
        }

        private void IzaberiJedinicu()
        {
            var m = new ContextMenuStrip { Font = SavaTheme.FieldFont };
            m.Items.Add("sve", null, (s, e) => { jedinica = 0; jedinicaNaziv = "sve"; Ucitaj(); });
            foreach (DataRow r in HRBaza.Tabela(HRListe.OrganizacioneJedinice).Rows)
            {
                int id = HRBaza.CeoBroj(r["ID"]);
                string naziv = Convert.ToString(r["Naziv"]).Trim();
                m.Items.Add(naziv, null, (s, e) => { jedinica = id; jedinicaNaziv = naziv; Ucitaj(); });
            }
            m.Show(chipJedinica, new Point(0, chipJedinica.Height));
        }

        private void PopuniMesec()
        {
            if (redovi.Count == 0)
                return;
            if (MessageBox.Show("Popuniti " + HREvidencija.ImeMeseca(mesec) + " " + godina + " za " + redovi.Count + " zaposlenih?\n\n" +
                                "Upisuje se redovan rad po rasporedu, praznici i odobreni godišnji odmori. Ručni unos i zaključani meseci se ne menjaju.",
                    "Popuni mesec", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            Cursor = Cursors.WaitCursor;
            var greske = new List<string>();
            try
            {
                foreach (RedZaposlenog red in redovi.Where(r => r.Status < 2))
                {
                    try
                    {
                        HRBaza.Procedura("dbo.sp_EvidencijaPopuniMesec", HRBaza.P("@Godina", godina), HRBaza.P("@Mesec", mesec),
                            HRBaza.P("@SefSifra", null), HRBaza.P("@ZaposleniSifra", red.ID), HRBaza.P("@Korisnik", HRBaza.Korisnik));
                    }
                    catch (SqlException ex)
                    {
                        greske.Add(red.Naziv + ": " + ex.Message);
                    }
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            Ucitaj();
            if (greske.Count > 0)
                MessageBox.Show(string.Join("\n", greske.Take(10)), "Popuni mesec", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // 1 = Potvrdi mesec (šef, svoj tim), 2 = Zaključi mesec (HR)
        private void PromeniStatus(int status)
        {
            var ciljani = redovi.Where(r => r.Status < status).ToList();
            if (ciljani.Count == 0)
            {
                MessageBox.Show(status == 1 ? "Nema otvorenih meseci za potvrdu." : "Svi meseci u prikazu su već zaključeni.",
                    "Evidencija rada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int crvenih = 0;
            foreach (RedZaposlenog red in ciljani)
                for (int d = 1; d <= brojDana; d++)
                {
                    DateTime datum = new DateTime(godina, mesec, d);
                    red.Dani.TryGetValue(d, out List<HRStavkaDana> s);
                    if (JeIspodFonda(red, datum, s))
                        crvenih++;
                }
            string pitanje = (status == 1 ? "Potvrditi" : "Zaključiti") + " " + HREvidencija.ImeMeseca(mesec) + " " + godina + " za " + ciljani.Count + " zaposlenih?" +
                             (crvenih > 0 ? "\n\nU prikazu ima " + crvenih + " dana ispod fonda (crvena polja)." : "") +
                             (status == 2 ? "\n\nPosle zaključavanja izmene su moguće samo uz pravo Zaključavanje meseca (Otključaj mesec)." : "");
            if (MessageBox.Show(pitanje, "Evidencija rada", MessageBoxButtons.YesNo, crvenih > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            foreach (RedZaposlenog red in ciljani)
                StatusMeseca(red.ID, status);
            Ucitaj();
        }

        private void StatusMeseca(int zaposleni, int status)
        {
            HRBaza.Procedura("dbo.sp_EvidencijaMesecStatus", HRBaza.P("@ZaposleniSifra", zaposleni), HRBaza.P("@Godina", godina),
                HRBaza.P("@Mesec", mesec), HRBaza.P("@Status", status), HRBaza.P("@KorisnikSifra", ja > 0 ? (object)ja : null), HRBaza.P("@Korisnik", HRBaza.Korisnik));
        }

        private void Otkljucaj()
        {
            var izabrani = IzabraneCelijeSvihKolona().Select(c => (RedZaposlenog)dgv.Rows[c.RowIndex].Tag).Distinct().Where(r => r.Status == 2).ToList();
            if (izabrani.Count == 0)
            {
                MessageBox.Show("Izaberite red (ili ćeliju) zaposlenog čiji je mesec zaključen.", "Otključaj mesec", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Otključati mesec za: " + string.Join(", ", izabrani.Select(r => r.Naziv)) + "?\nOtključavanje se beleži u log.",
                    "Otključaj mesec", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            foreach (RedZaposlenog red in izabrani)
                StatusMeseca(red.ID, 1);
            Ucitaj();
        }

        private List<DataGridViewCell> IzabraneCelijeSvihKolona()
        {
            return dgv.SelectedCells.Cast<DataGridViewCell>().Where(c => c.RowIndex >= 0).ToList();
        }

        private void GrupniUnos()
        {
            if (redovi.Count == 0)
                return;
            var zaposleni = redovi.Where(r => r.Status < 2).Select(r => new KeyValuePair<int, string>(r.ID, r.Naziv)).ToList();
            var izabrani = IzabraneCelijeSvihKolona().Select(c => ((RedZaposlenog)dgv.Rows[c.RowIndex].Tag).ID).Distinct().ToList();
            using (var f = new frmHRGrupniUnos(zaposleni, izabrani, PrviDan, PrviDan.AddMonths(1).AddDays(-1), vrste))
            {
                f.FondDana = (z, d) =>
                {
                    RedZaposlenog red = redovi.FirstOrDefault(r => r.ID == z);
                    return red == null ? 0 : FondRasporeda(red, d);
                };
                f.Praznik = d => HRBaza.CeoBroj(HRBaza.Skalar("SELECT COUNT(*) FROM dbo.Praznici WHERE Neradni = 1 AND Datum = @D", HRBaza.P("@D", d.Date))) > 0;
                f.PodrazumevaniFond = podrazumevaniFond;
                if (f.ShowDialog(this) == DialogResult.OK)
                    Ucitaj();
            }
        }

        // Izvoz za obračun zarada: red po zaposlenom, kolone sati i dani za svaku vrstu rada iz šifarnika
        private void IzvozZaObracun()
        {
            if (redovi.Count == 0)
                return;
            DataTable podaci = HRBaza.Tabela(@"
                SELECT o.ZaposleniSifra, o.Oznaka, o.Sati, o.Dani FROM dbo.vw_EvidencijaZaObracun o
                JOIN dbo.Zaposleni z ON z.ID = o.ZaposleniSifra
                WHERE o.Godina = @G AND o.Mesec = @M AND " + HRPrava.FilterTima("z.ID", hr, ja),
                HRBaza.P("@G", godina), HRBaza.P("@M", mesec));
            DataTable kodovi = HRBaza.Tabela(@"
                SELECT z.ID, ISNULL(z.Code, N'') AS Code, ISNULL(z.SifraERP, N'') AS SifraERP, t.PrezimeIme, ISNULL(t.OrganizacionaJedinicaNaziv, N'') AS Jedinica
                FROM dbo.Zaposleni z JOIN dbo.vZaposleniTrenutno t ON t.Zaposleni = z.ID WHERE " + HRPrava.FilterTima("z.ID", hr, ja));

            var izvoz = new DataTable();
            izvoz.Columns.Add("Code");
            izvoz.Columns.Add("Šifra ERP");
            izvoz.Columns.Add("Zaposleni");
            izvoz.Columns.Add("Org. jedinica");
            foreach (HRVrstaRada v in vrste.Values)
            {
                izvoz.Columns.Add(v.Oznaka + " sati", typeof(decimal));
                izvoz.Columns.Add(v.Oznaka + " dani", typeof(int));
            }
            foreach (RedZaposlenog red in redovi)
            {
                DataRow k = kodovi.Rows.Cast<DataRow>().FirstOrDefault(x => HRBaza.CeoBroj(x["ID"]) == red.ID);
                DataRow n = izvoz.NewRow();
                n["Code"] = k?["Code"] ?? "";
                n["Šifra ERP"] = k?["SifraERP"] ?? "";
                n["Zaposleni"] = red.Naziv;
                n["Org. jedinica"] = k?["Jedinica"] ?? "";
                foreach (HRVrstaRada v in vrste.Values)
                {
                    DataRow p = podaci.Rows.Cast<DataRow>().FirstOrDefault(x => HRBaza.CeoBroj(x["ZaposleniSifra"]) == red.ID && Convert.ToString(x["Oznaka"]).Trim() == v.Oznaka);
                    n[v.Oznaka + " sati"] = p == null ? 0m : HRBaza.Decimalni(p["Sati"]);
                    n[v.Oznaka + " dani"] = p == null ? 0 : HRBaza.CeoBroj(p["Dani"]);
                }
                izvoz.Rows.Add(n);
            }

            using (var sfd = new SaveFileDialog { Filter = "Excel (*.xlsx)|*.xlsx", FileName = "Evidencija_" + godina + "_" + mesec.ToString("00") + ".xlsx" })
            {
                if (sfd.ShowDialog(this) != DialogResult.OK)
                    return;
                using (var engine = new ExcelEngine())
                {
                    IApplication app = engine.Excel;
                    app.DefaultVersion = ExcelVersion.Xlsx;
                    IWorkbook wb = app.Workbooks.Create(1);
                    IWorksheet ws = wb.Worksheets[0];
                    ws.Name = HREvidencija.ImeMeseca(mesec) + " " + godina;
                    ws.ImportDataTable(izvoz, true, 1, 1);
                    ws.Range[1, 1, 1, izvoz.Columns.Count].CellStyle.Font.Bold = true;
                    ws.UsedRange.AutofitColumns();
                    wb.SaveAs(sfd.FileName);
                }
                try { System.Diagnostics.Process.Start(sfd.FileName); } catch { }
            }
        }
    }
}
