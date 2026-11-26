using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main.Dokumenta
{
    // RAZ-14: dokument razduženja (C4, IZ3, C3...) po naimenovanju C7.
    // Jedan dokument može da razduži naimenovanja više C7 (IZ3 2177/2026: C7 1398/2026/2 i C7 1404/2026/1)
    // i da bude vezan za više otpremnica (C4 11971/2025: otpremnice 01039/25 i 01084/25).
    // Razduženje ne sme preći nerazduženo (prijavljeno na C7 minus ostali dokumenti).
    // Predlog po koletima je srazmeran nerazduženom, a kad se razdužuju sva preostala koleta, predlaže se
    // tačan ostatak neta, bruta i vrednosti (poslednje razduženje zatvara stanje na 0).
    // Skripta: Skladista main\Carina_Izlaz_2026_11.sql.
    public class frmRazduzenje : Form
    {
        readonly string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        readonly string korisnik = Saobracaj.Sifarnici.frmLogovanje.user;
        readonly int idOtpremnice;
        readonly int magacinskiBroj;
        int? idRazduzenja;
        bool izmenjeno, punim;

        DataTable naim;
        ComboBox cboDokument, cboVrsta, cboMagacinskiBroj;
        TextBox txtCI, txtBroj, txtValuta, txtKurs, txtNapomena;
        NumericUpDown numGodina;
        DateTimePicker dtpIzdavanja;
        DataGridView dgv;
        CheckedListBox lstOtpremnice;
        Label lblStanje;
        Button btnObrisi;

        static readonly Color Plava = Color.FromArgb(0x0A, 0x66, 0xB8);
        static readonly Color Oznaka = Color.FromArgb(0x4C, 0x5D, 0x6E);
        static readonly Color Opasnost = Color.FromArgb(0xB3, 0x26, 0x1E);
        static readonly Color Povrsina = Color.FromArgb(0xF4, 0xF7, 0xFA);

        public frmRazduzenje(int idOtpremnice, int magacinskiBroj)
        {
            this.idOtpremnice = idOtpremnice;
            this.magacinskiBroj = magacinskiBroj;

            Text = "Dokument razduženja, otpremnica " + idOtpremnice;
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(1280, 680);
            MinimumSize = new Size(1000, 500);
            MinimizeBox = false;
            ShowIcon = false;
            ShowInTaskbar = false;
            Font = new Font("Segoe UI", 9f);
            BackColor = Color.White;

            NapraviKontrole();
            NapraviTabelu();
            Load += (s, e) => Ucitaj();
            FormClosing += (s, e) =>
            {
                if (izmenjeno && MessageBox.Show("Izmene nisu sačuvane. Zatvoriti?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    e.Cancel = true;
            };
        }

        #region Izgled

        private Label Natpis(Control roditelj, string tekst, int x, int y)
        {
            var l = new Label { Text = tekst, AutoSize = true, Location = new Point(x, y), ForeColor = Oznaka };
            roditelj.Controls.Add(l);
            return l;
        }

        private T Polje<T>(Control roditelj, string natpis, T c, int x, int y, int sirina) where T : Control
        {
            Natpis(roditelj, natpis, x, y);
            c.Location = new Point(x, y + 18);
            c.Width = sirina;
            roditelj.Controls.Add(c);
            return c;
        }

        private Button Dugme(string tekst, int sirina)
        {
            return new Button { Text = tekst, Width = sirina, Height = 32, UseVisualStyleBackColor = true, Margin = new Padding(0, 0, 8, 0) };
        }

        private void NapraviKontrole()
        {
            var vrh = new Panel { Dock = DockStyle.Top, Height = 124, Padding = new Padding(20, 8, 20, 0) };

            cboDokument = Polje(vrh, "Dokument", new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList }, 20, 8, 420);
            var btnNovi = Dugme("Novi dokument", 130);
            btnNovi.Location = new Point(450, 25);
            vrh.Controls.Add(btnNovi);
            btnNovi.Click += (s, e) =>
            {
                if (izmenjeno && MessageBox.Show("Izmene nisu sačuvane. Nastaviti?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
                punim = true;
                cboDokument.SelectedIndex = cboDokument.Items.Count > 0 ? 0 : -1;
                punim = false;
                OtvoriDokument(null);
            };
            cboDokument.SelectedIndexChanged += (s, e) =>
            {
                if (punim)
                    return;
                if (izmenjeno && MessageBox.Show("Izmene nisu sačuvane. Nastaviti?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    punim = true;
                    IzaberiDokumentUListi(idRazduzenja);
                    punim = false;
                    return;
                }
                var d = cboDokument.SelectedItem as Stavka;
                OtvoriDokument(d == null || d.ID == 0 ? (int?)null : d.ID);
            };

            int y = 62;
            txtCI = Polje(vrh, "CI razduženja", new TextBox { MaxLength = 10 }, 20, y, 80);
            cboVrsta = Polje(vrh, "Vrsta dokumenta", new ComboBox { DropDownStyle = ComboBoxStyle.DropDown, MaxLength = 10 }, 110, y, 90);
            cboVrsta.Items.AddRange(new object[] { "C4", "IZ3", "C3", "C5", "C9" });
            txtBroj = Polje(vrh, "Broj", new TextBox { MaxLength = 20 }, 210, y, 110);
            numGodina = Polje(vrh, "Godina", new NumericUpDown { Minimum = 2000, Maximum = 2100, Value = DateTime.Today.Year, TextAlign = HorizontalAlignment.Right }, 330, y, 70);
            dtpIzdavanja = Polje(vrh, "Datum izdavanja", new DateTimePicker { Format = DateTimePickerFormat.Short, ShowCheckBox = true }, 410, y, 130);
            txtValuta = Polje(vrh, "Valuta", new TextBox { MaxLength = 10 }, 550, y, 60);
            txtKurs = Polje(vrh, "Kurs", new TextBox { TextAlign = HorizontalAlignment.Right }, 620, y, 90);
            txtNapomena = Polje(vrh, "Napomena", new TextBox { MaxLength = 500 }, 720, y, 360);
            foreach (Control c in new Control[] { txtCI, cboVrsta, txtBroj, numGodina, dtpIzdavanja, txtValuta, txtKurs, txtNapomena })
                c.TextChanged += (s, e) => { if (!punim) izmenjeno = true; };
            numGodina.ValueChanged += (s, e) => { if (!punim) izmenjeno = true; };

            // otpremnice dokumenta (desno)
            var desno = new Panel { Dock = DockStyle.Right, Width = 260, Padding = new Padding(8, 0, 20, 0) };
            var lblOtp = new Label { Text = "Otpremnice ovog dokumenta", Dock = DockStyle.Top, Height = 22, ForeColor = Oznaka };
            lstOtpremnice = new CheckedListBox { Dock = DockStyle.Fill, CheckOnClick = true, IntegralHeight = false, BorderStyle = BorderStyle.FixedSingle };
            lstOtpremnice.ItemCheck += (s, e) => { if (!punim) izmenjeno = true; };
            desno.Controls.Add(lstOtpremnice);
            desno.Controls.Add(lblOtp);

            // dno: dodavanje naimenovanja drugog C7, predlog ostatka, snimanje
            var dno = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 56, Padding = new Padding(20, 12, 20, 8), WrapContents = false };
            var lblMB = new Label { Text = "Magacinski broj", AutoSize = true, ForeColor = Oznaka, Margin = new Padding(0, 8, 6, 0) };
            cboMagacinskiBroj = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 200, Margin = new Padding(0, 4, 8, 0) };
            var btnDodajMB = Dugme("Dodaj naimenovanja", 150);
            var btnOstatak = Dugme("Predloži ostatak", 130);
            lblStanje = new Label { AutoSize = false, Width = 330, Height = 32, TextAlign = ContentAlignment.MiddleLeft, Margin = new Padding(8, 0, 8, 0) };
            var btnSacuvaj = Dugme("Sačuvaj", 110);
            btnSacuvaj.BackColor = Plava;
            btnSacuvaj.ForeColor = Color.White;
            btnSacuvaj.FlatStyle = FlatStyle.Flat;
            btnSacuvaj.UseVisualStyleBackColor = false;
            btnObrisi = Dugme("Obriši dokument", 130);
            btnObrisi.ForeColor = Opasnost;
            var btnZatvori = Dugme("Zatvori", 90);
            btnZatvori.Click += (s, e) => Close();
            dno.Controls.AddRange(new Control[] { lblMB, cboMagacinskiBroj, btnDodajMB, btnOstatak, lblStanje, btnSacuvaj, btnObrisi, btnZatvori });

            btnDodajMB.Click += (s, e) => DodajMagacinskiBroj();
            btnOstatak.Click += (s, e) => PredloziOstatak();
            btnSacuvaj.Click += (s, e) => Sacuvaj();
            btnObrisi.Click += (s, e) => Obrisi();

            dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                RowHeadersVisible = false,
                AutoGenerateColumns = false,
                SelectionMode = DataGridViewSelectionMode.CellSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            dgv.RowTemplate.Height = 28;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9f);

            var sredina = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 4, 0, 0) };
            sredina.Controls.Add(dgv);

            Controls.Add(sredina);
            Controls.Add(desno);
            Controls.Add(vrh);
            Controls.Add(dno);
            CancelButton = btnZatvori;
        }

        private void NapraviTabelu()
        {
            naim = new DataTable();
            naim.Columns.Add("IDNaimenovanja", typeof(int));
            naim.Columns.Add("MagacinskiBroj", typeof(int));
            naim.Columns.Add("C7", typeof(string));
            naim.Columns.Add("RB", typeof(int));
            naim.Columns.Add("Naimenovanje", typeof(string));
            naim.Columns.Add("NHM", typeof(string));
            naim.Columns.Add("Valuta", typeof(string));
            foreach (var m in new[] { "Koleta", "Neto", "Bruto", "Vrednost" })
            {
                naim.Columns.Add("Ner" + m, typeof(decimal));
                naim.Columns.Add(m, typeof(decimal));
                naim.Columns.Add("Ostaje" + m, typeof(decimal));
            }
            naim.Columns.Add("Predlog", typeof(bool));
            naim.PrimaryKey = new[] { naim.Columns["IDNaimenovanja"] };

            Func<string, string, int, bool, DataGridViewTextBoxColumn> kol = (ime, zaglavlje, sirina, broj) =>
            {
                var c = new DataGridViewTextBoxColumn { Name = ime, DataPropertyName = ime, HeaderText = zaglavlje, Width = sirina, ReadOnly = true, SortMode = DataGridViewColumnSortMode.NotSortable };
                if (broj)
                {
                    c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    c.DefaultCellStyle.Format = "N3";
                }
                return c;
            };
            dgv.Columns.Add(kol("MagacinskiBroj", "Magacinski broj", 70, false));
            dgv.Columns.Add(kol("C7", "C7", 110, false));
            dgv.Columns.Add(kol("RB", "RB", 36, false));
            dgv.Columns.Add(kol("Naimenovanje", "Naimenovanje", 180, false));
            dgv.Columns.Add(kol("NHM", "Tarifni broj", 76, false));
            dgv.Columns.Add(kol("Valuta", "Valuta", 50, false));
            dgv.Columns.Add(kol("NerKoleta", "Nerazduženo koleta", 76, true));
            dgv.Columns.Add(kol("NerBruto", "Nerazduženo bruto kg", 86, true));
            dgv.Columns.Add(kol("NerVrednost", "Nerazdužena vrednost", 90, true));
            foreach (var m in new[] { "Koleta", "Neto", "Bruto", "Vrednost" })
            {
                var c = kol(m, m == "Koleta" ? "Koleta" : m == "Neto" ? "Neto kg" : m == "Bruto" ? "Bruto kg" : "Vrednost", 80, true);
                c.ReadOnly = false;
                c.DefaultCellStyle.BackColor = Color.FromArgb(0xFF, 0xFD, 0xE7);
                dgv.Columns.Add(c);
            }
            dgv.Columns.Add(kol("OstajeKoleta", "Ostaje koleta", 70, true));
            dgv.Columns.Add(kol("OstajeBruto", "Ostaje bruto kg", 80, true));
            dgv.Columns.Add(kol("OstajeVrednost", "Ostaje vrednost", 84, true));
            dgv.Columns["NerVrednost"].DefaultCellStyle.Format = "N2";
            dgv.Columns["Vrednost"].DefaultCellStyle.Format = "N2";
            dgv.Columns["OstajeVrednost"].DefaultCellStyle.Format = "N2";
            foreach (var ime in new[] { "NerKoleta", "Koleta", "OstajeKoleta" })
                dgv.Columns[ime].DefaultCellStyle.Format = "N0";
            foreach (var ime in new[] { "MagacinskiBroj", "C7", "RB", "Naimenovanje", "NHM", "Valuta", "NerKoleta", "NerBruto", "NerVrednost", "OstajeKoleta", "OstajeBruto", "OstajeVrednost" })
                dgv.Columns[ime].DefaultCellStyle.BackColor = Povrsina;

            dgv.DataSource = naim;
            dgv.CellValidating += Dgv_CellValidating;
            dgv.CellEndEdit += Dgv_CellEndEdit;
            dgv.DataError += (s, e) =>
            {
                e.ThrowException = false;
                MessageBox.Show("Upišite broj.");
            };
        }

        #endregion

        private class Stavka
        {
            public int ID;
            public string Tekst;
            public override string ToString() { return Tekst; }
        }

        #region Učitavanje

        private DataTable Upit(string sql, CommandType tip, params SqlParameter[] parametri)
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(connection))
            using (var cmd = new SqlCommand(sql, conn) { CommandType = tip })
            {
                cmd.Parameters.AddRange(parametri);
                using (var da = new SqlDataAdapter(cmd))
                    da.Fill(dt);
            }
            return dt;
        }

        private void Ucitaj()
        {
            try
            {
                punim = true;
                var mb = Upit(@"SELECT DISTINCT rns.MagacinskiBroj AS ID,
                                   CONVERT(nvarchar(10), rns.MagacinskiBroj) + ISNULL(N' - ' + RTRIM(CONVERT(nvarchar(100), mbc.Naziv)), N'') AS Naziv
                                FROM dbo.MagacinskiPredmet mp
                                JOIN dbo.RadniNalogSkladista rns ON rns.ID = mp.RN
                                LEFT JOIN dbo.MagacinskiBrojCarinski mbc ON mbc.ID = rns.MagacinskiBroj
                                WHERE ISNULL(rns.MagacinskiBroj, 0) <> 0
                                  AND EXISTS (SELECT 1 FROM dbo.RNCarinskoStavke n WHERE n.IDNadredjena = mp.ID)
                                ORDER BY ID DESC", CommandType.Text);
                foreach (DataRow r in mb.Rows)
                    cboMagacinskiBroj.Items.Add(new Stavka { ID = Convert.ToInt32(r["ID"]), Tekst = Convert.ToString(r["Naziv"]) });
                if (cboMagacinskiBroj.Items.Count > 0)
                    cboMagacinskiBroj.SelectedIndex = 0;

                UcitajListuDokumenata();
                punim = false;

                var vezan = cboDokument.Items.Cast<Stavka>().Skip(1).FirstOrDefault(d => d.Tekst.EndsWith(" *"));
                if (vezan != null)
                {
                    punim = true;
                    cboDokument.SelectedItem = vezan;
                    punim = false;
                    OtvoriDokument(vezan.ID);
                }
                else
                    OtvoriDokument(null);
            }
            catch (Exception ex)
            {
                punim = false;
                MessageBox.Show("Dokument razduženja nije učitan: " + ex.Message);
            }
        }

        // dokumenti vezani za otpremnicu (označeni *) i dokumenti sa naimenovanjima ovog magacinskog broja
        private void UcitajListuDokumenata()
        {
            var dt = Upit(@"SELECT r.ID,
                       RTRIM(r.Vrsta) + N' ' + RTRIM(r.Broj) + N'/' + CONVERT(nvarchar(4), r.Godina)
                         + CASE WHEN r.CI <> N'' THEN N', CI ' + r.CI ELSE N'' END
                         + CASE WHEN r.DatumIzdavanja IS NOT NULL THEN N', ' + CONVERT(nvarchar(10), r.DatumIzdavanja, 104) ELSE N'' END AS Opis,
                       CASE WHEN EXISTS (SELECT 1 FROM dbo.RNCarinskoOtpremnicaRazduzenje x WHERE x.IDRazduzenja = r.ID AND x.IDOtpremnice = @O) THEN 1 ELSE 0 END AS Vezan
                FROM dbo.RNCarinskoRazduzenje r
                WHERE EXISTS (SELECT 1 FROM dbo.RNCarinskoOtpremnicaRazduzenje x WHERE x.IDRazduzenja = r.ID AND x.IDOtpremnice = @O)
                   OR EXISTS (SELECT 1 FROM dbo.RNCarinskoRazduzenjeStavka rs
                              JOIN dbo.RNCarinskoStavke n ON n.ID = rs.IDNaimenovanja
                              JOIN dbo.MagacinskiPredmet mp ON mp.ID = n.IDNadredjena
                              JOIN dbo.RadniNalogSkladista rns ON rns.ID = mp.RN
                              WHERE rs.IDRazduzenja = r.ID AND rns.MagacinskiBroj = @MB)
                ORDER BY Vezan DESC, r.ID DESC", CommandType.Text,
                new SqlParameter("@O", idOtpremnice), new SqlParameter("@MB", magacinskiBroj));

            cboDokument.Items.Clear();
            cboDokument.Items.Add(new Stavka { ID = 0, Tekst = "(novi dokument)" });
            foreach (DataRow r in dt.Rows)
                cboDokument.Items.Add(new Stavka
                {
                    ID = Convert.ToInt32(r["ID"]),
                    Tekst = Convert.ToString(r["Opis"]) + (Convert.ToInt32(r["Vezan"]) == 1 ? " *" : "")
                });
            cboDokument.SelectedIndex = 0;
        }

        private void IzaberiDokumentUListi(int? id)
        {
            var d = cboDokument.Items.Cast<Stavka>().FirstOrDefault(x => x.ID == (id ?? 0));
            cboDokument.SelectedItem = d ?? cboDokument.Items.Cast<Stavka>().FirstOrDefault();
        }

        private void OtvoriDokument(int? id)
        {
            try
            {
                punim = true;
                idRazduzenja = id;
                naim.Rows.Clear();
                lstOtpremnice.Items.Clear();   // izbor otpremnica prethodnog dokumenta se ne prenosi
                var otpremnice = new HashSet<int>();

                if (id.HasValue)
                {
                    var z = Upit("SELECT * FROM dbo.RNCarinskoRazduzenje WHERE ID = @ID", CommandType.Text, new SqlParameter("@ID", id.Value));
                    if (z.Rows.Count == 0)
                        throw new InvalidOperationException("Dokument više ne postoji.");
                    var r = z.Rows[0];
                    txtCI.Text = Convert.ToString(r["CI"]).Trim();
                    cboVrsta.Text = Convert.ToString(r["Vrsta"]).Trim();
                    txtBroj.Text = Convert.ToString(r["Broj"]).Trim();
                    numGodina.Value = Math.Min(Math.Max(Convert.ToInt32(r["Godina"]), 2000), 2100);
                    dtpIzdavanja.Checked = r["DatumIzdavanja"] != DBNull.Value;
                    if (r["DatumIzdavanja"] != DBNull.Value)
                        dtpIzdavanja.Value = Convert.ToDateTime(r["DatumIzdavanja"]);
                    txtValuta.Text = Convert.ToString(r["Valuta"]).Trim();
                    txtKurs.Text = r["Kurs"] == DBNull.Value ? "" : Convert.ToDecimal(r["Kurs"]).ToString("0.######");
                    txtNapomena.Text = Convert.ToString(r["Napomena"]).Trim();
                    foreach (DataRow o in Upit("SELECT IDOtpremnice FROM dbo.RNCarinskoOtpremnicaRazduzenje WHERE IDRazduzenja = @ID", CommandType.Text,
                            new SqlParameter("@ID", id.Value)).Rows)
                        otpremnice.Add(Convert.ToInt32(o["IDOtpremnice"]));
                }
                else
                {
                    // CI se najčešće ponavlja: predlog iz poslednjeg dokumenta
                    var ci = Upit("SELECT TOP 1 CI FROM dbo.RNCarinskoRazduzenje ORDER BY ID DESC", CommandType.Text);
                    txtCI.Text = ci.Rows.Count > 0 ? Convert.ToString(ci.Rows[0]["CI"]).Trim() : "";
                    cboVrsta.Text = "";
                    txtBroj.Text = "";
                    numGodina.Value = DateTime.Today.Year;
                    dtpIzdavanja.Value = DateTime.Today;
                    dtpIzdavanja.Checked = true;
                    txtValuta.Text = "";
                    txtKurs.Text = "";
                    txtNapomena.Text = "";
                    otpremnice.Add(idOtpremnice);
                }

                DodajNaimenovanja(magacinskiBroj == 0 ? (int?)null : magacinskiBroj, id);
                if (!id.HasValue && txtValuta.Text == "" && naim.Rows.Count > 0)
                    txtValuta.Text = Convert.ToString(naim.Rows[0]["Valuta"]);
                UcitajOtpremnice(otpremnice);
                btnObrisi.Enabled = id.HasValue;
                punim = false;
                izmenjeno = false;
                OsveziStanje();
            }
            catch (Exception ex)
            {
                punim = false;
                MessageBox.Show("Dokument nije učitan: " + ex.Message);
            }
        }

        // naimenovanja magacinskog broja i/ili dokumenta; postojeći redovi ostaju (sa unetim vrednostima)
        private void DodajNaimenovanja(int? mb, int? idDok)
        {
            if (!mb.HasValue && !idDok.HasValue)
                return;
            var dt = Upit("sp_Razduzenje_Naimenovanja", CommandType.StoredProcedure,
                new SqlParameter("@MagacinskiBroj", mb.HasValue ? (object)mb.Value : DBNull.Value),
                new SqlParameter("@IDRazduzenja", idDok.HasValue ? (object)idDok.Value : DBNull.Value));
            foreach (DataRow r in dt.Rows)
            {
                int id = Convert.ToInt32(r["IDNaimenovanja"]);
                if (naim.Rows.Find(id) != null)
                    continue;
                var n = naim.NewRow();
                n["IDNaimenovanja"] = id;
                n["MagacinskiBroj"] = r["MagacinskiBroj"];
                n["C7"] = r["C7"];
                n["RB"] = r["RB"];
                n["Naimenovanje"] = r["Naimenovanje"];
                n["NHM"] = r["NHM"];
                n["Valuta"] = r["Valuta"];
                foreach (var m in new[] { "Koleta", "Neto", "Bruto", "Vrednost" })
                {
                    decimal ner = Convert.ToDecimal(r["Prijavljeno" + m]) - Convert.ToDecimal(r["Razduzeno" + m]);
                    decimal vr = Convert.ToDecimal(r[m]);
                    n["Ner" + m] = ner;
                    n[m] = vr;
                    n["Ostaje" + m] = ner - vr;
                }
                n["Predlog"] = false;
                naim.Rows.Add(n);
            }
        }

        private void UcitajOtpremnice(HashSet<int> cekirane)
        {
            var mbs = naim.Rows.Cast<DataRow>().Select(r => Convert.ToInt32(r["MagacinskiBroj"])).Distinct().ToList();
            if (magacinskiBroj != 0 && !mbs.Contains(magacinskiBroj))
                mbs.Add(magacinskiBroj);
            foreach (var x in lstOtpremnice.CheckedItems.Cast<Stavka>())
                cekirane.Add(x.ID);

            string uslovMB = mbs.Count == 0 ? "1 = 0" : "rn.MagacinskiBroj IN (" + string.Join(",", mbs) + ")";
            string uslovID = cekirane.Count == 0 ? "1 = 0" : "o.ID IN (" + string.Join(",", cekirane) + ")";
            var dt = Upit(@"SELECT o.ID, o.DatumOtpreme, rn.MagacinskiBroj
                            FROM dbo.RNCarinskoSkladisteOtpremnica o
                            LEFT JOIN dbo.RadniNalogSkladista rn ON rn.ID = o.RN
                            WHERE " + uslovMB + " OR " + uslovID + @"
                            ORDER BY o.ID DESC", CommandType.Text);

            lstOtpremnice.Items.Clear();
            foreach (DataRow r in dt.Rows)
            {
                int id = Convert.ToInt32(r["ID"]);
                string tekst = "Otpremnica " + id
                    + (r["DatumOtpreme"] == DBNull.Value ? "" : ", " + Convert.ToDateTime(r["DatumOtpreme"]).ToString("dd.MM.yyyy"))
                    + (r["MagacinskiBroj"] == DBNull.Value ? "" : ", MB " + r["MagacinskiBroj"]);
                lstOtpremnice.Items.Add(new Stavka { ID = id, Tekst = tekst }, cekirane.Contains(id));
            }
        }

        private void DodajMagacinskiBroj()
        {
            var mb = cboMagacinskiBroj.SelectedItem as Stavka;
            if (mb == null)
                return;
            try
            {
                bool bilo = punim;
                punim = true;
                int pre = naim.Rows.Count;
                DodajNaimenovanja(mb.ID, null);
                UcitajOtpremnice(new HashSet<int>());
                punim = bilo;
                if (naim.Rows.Count == pre)
                    MessageBox.Show("Naimenovanja magacinskog broja " + mb.ID + " su već u tabeli.");
                OsveziStanje();
            }
            catch (Exception ex)
            {
                punim = false;
                MessageBox.Show("Naimenovanja nisu učitana: " + ex.Message);
            }
        }

        #endregion

        #region Unos

        private DataRow RedGrida(int indeks)
        {
            var drv = dgv.Rows[indeks].DataBoundItem as DataRowView;
            return drv == null ? null : drv.Row;
        }

        private static decimal Dec(object v)
        {
            return v == null || v == DBNull.Value ? 0m : Convert.ToDecimal(v);
        }

        private void Dgv_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string ime = dgv.Columns[e.ColumnIndex].Name;
            if (dgv.Columns[e.ColumnIndex].ReadOnly || e.RowIndex < 0)
                return;
            string tekst = Convert.ToString(e.FormattedValue).Trim();
            if (tekst == "")
                return;
            decimal v;
            if (!decimal.TryParse(tekst, NumberStyles.Number, CultureInfo.CurrentCulture, out v))
            {
                MessageBox.Show("Upišite broj.");
                e.Cancel = true;
                return;
            }
            var red = RedGrida(e.RowIndex);
            decimal ner = Dec(red["Ner" + ime]);
            if (v < 0)
            {
                MessageBox.Show("Vrednost ne može biti negativna.");
                e.Cancel = true;
            }
            else if (v > ner + (ime == "Vrednost" ? 0.005m : 0.0005m))
            {
                MessageBox.Show("Razduženje ne sme preći nerazduženo (" + ner.ToString(ime == "Vrednost" ? "N2" : "N3") + ").");
                e.Cancel = true;
            }
            else if (ime == "Koleta" && v != Math.Round(v))
            {
                MessageBox.Show("Koleta se upisuju kao ceo broj.");
                e.Cancel = true;
            }
        }

        private void Dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            var red = RedGrida(e.RowIndex);
            if (red == null)
                return;
            string ime = dgv.Columns[e.ColumnIndex].Name;
            if (red[ime] == DBNull.Value)
                red[ime] = 0m;

            if (ime == "Koleta")
            {
                // predlog neta, bruta i vrednosti: kad je red prazan ili je i ranije bio predlog
                bool prazno = Dec(red["Neto"]) == 0 && Dec(red["Bruto"]) == 0 && Dec(red["Vrednost"]) == 0;
                if (prazno || (red["Predlog"] is bool && (bool)red["Predlog"]))
                {
                    decimal k = Dec(red["Koleta"]), nerK = Dec(red["NerKoleta"]);
                    bool poslednje = nerK > 0 && k >= nerK;
                    foreach (var m in new[] { "Neto", "Bruto", "Vrednost" })
                    {
                        decimal ner = Dec(red["Ner" + m]);
                        red[m] = poslednje ? ner : nerK == 0 ? 0m : Math.Round(ner * k / nerK, m == "Vrednost" ? 2 : 3);
                    }
                    red["Predlog"] = true;
                }
            }
            else
                red["Predlog"] = false;

            foreach (var m in new[] { "Koleta", "Neto", "Bruto", "Vrednost" })
                red["Ostaje" + m] = Dec(red["Ner" + m]) - Dec(red[m]);
            izmenjeno = true;
            OsveziStanje();
        }

        private void PredloziOstatak()
        {
            var cell = dgv.CurrentCell;
            if (cell == null)
            {
                MessageBox.Show("Izaberite naimenovanje u tabeli.");
                return;
            }
            dgv.EndEdit();
            var red = RedGrida(cell.RowIndex);
            foreach (var m in new[] { "Koleta", "Neto", "Bruto", "Vrednost" })
            {
                red[m] = Dec(red["Ner" + m]);
                red["Ostaje" + m] = 0m;
            }
            red["Predlog"] = false;
            izmenjeno = true;
            OsveziStanje();
        }

        private void OsveziStanje()
        {
            var redovi = naim.Rows.Cast<DataRow>().Where(r => Dec(r["Koleta"]) > 0 || Dec(r["Bruto"]) > 0 || Dec(r["Neto"]) > 0 || Dec(r["Vrednost"]) > 0).ToList();
            lblStanje.Text = redovi.Count == 0
                ? "Upišite količine u žuta polja."
                : "Naimenovanja: " + redovi.Count + ", koleta " + redovi.Sum(r => Dec(r["Koleta"])).ToString("N0")
                  + ", bruto " + redovi.Sum(r => Dec(r["Bruto"])).ToString("N3") + " kg";
        }

        #endregion

        #region Snimanje

        private void Sacuvaj()
        {
            dgv.EndEdit();
            string vrsta = cboVrsta.Text.Trim().ToUpperInvariant();
            string broj = txtBroj.Text.Trim();
            if (vrsta == "" || broj == "")
            {
                MessageBox.Show("Unesite vrstu i broj dokumenta razduženja.");
                return;
            }
            decimal kurs = 0;
            if (txtKurs.Text.Trim() != "" && !decimal.TryParse(txtKurs.Text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out kurs))
            {
                MessageBox.Show("Kurs mora biti broj.");
                return;
            }

            var inv = CultureInfo.InvariantCulture;
            var sb = new StringBuilder();
            foreach (DataRow r in naim.Rows)
            {
                decimal k = Dec(r["Koleta"]), ne = Dec(r["Neto"]), b = Dec(r["Bruto"]), v = Dec(r["Vrednost"]);
                if (k == 0 && ne == 0 && b == 0 && v == 0)
                    continue;
                sb.Append("<i n=\"").Append(Convert.ToInt32(r["IDNaimenovanja"]))
                  .Append("\" k=\"").Append(k.ToString(inv))
                  .Append("\" ne=\"").Append(ne.ToString(inv))
                  .Append("\" b=\"").Append(b.ToString(inv))
                  .Append("\" v=\"").Append(v.ToString(inv)).Append("\"/>");
            }
            if (sb.Length == 0)
            {
                MessageBox.Show("Upišite količine bar za jedno naimenovanje.");
                return;
            }
            var otpremnice = lstOtpremnice.CheckedItems.Cast<Stavka>().Select(x => x.ID).ToList();
            if (otpremnice.Count == 0 && MessageBox.Show("Dokument nije vezan ni za jednu otpremnicu. Sačuvati?", Text,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (var conn = new SqlConnection(connection))
                using (var cmd = new SqlCommand("sp_Razduzenje_Snimi", conn) { CommandType = CommandType.StoredProcedure })
                {
                    var pID = new SqlParameter("@ID", SqlDbType.Int) { Direction = ParameterDirection.InputOutput, Value = idRazduzenja.HasValue ? (object)idRazduzenja.Value : DBNull.Value };
                    cmd.Parameters.Add(pID);
                    cmd.Parameters.AddWithValue("@CI", txtCI.Text.Trim());
                    cmd.Parameters.AddWithValue("@Vrsta", vrsta);
                    cmd.Parameters.AddWithValue("@Broj", broj);
                    cmd.Parameters.Add("@Godina", SqlDbType.SmallInt).Value = (short)numGodina.Value;
                    cmd.Parameters.Add("@DatumIzdavanja", SqlDbType.Date).Value = dtpIzdavanja.Checked ? (object)dtpIzdavanja.Value.Date : DBNull.Value;
                    cmd.Parameters.AddWithValue("@Valuta", txtValuta.Text.Trim());
                    cmd.Parameters.Add("@Kurs", SqlDbType.Decimal).Value = txtKurs.Text.Trim() == "" ? (object)DBNull.Value : kurs;
                    cmd.Parameters["@Kurs"].Precision = 18;
                    cmd.Parameters["@Kurs"].Scale = 6;
                    cmd.Parameters.AddWithValue("@Napomena", txtNapomena.Text.Trim());
                    cmd.Parameters.AddWithValue("@Stavke", sb.ToString());
                    cmd.Parameters.AddWithValue("@Otpremnice", string.Join(",", otpremnice));
                    cmd.Parameters.AddWithValue("@Korisnik", korisnik ?? "");
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    idRazduzenja = Convert.ToInt32(pID.Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dokument nije sačuvan: " + ex.Message);
                return;
            }

            izmenjeno = false;
            int id = idRazduzenja.Value;
            punim = true;
            UcitajListuDokumenata();
            IzaberiDokumentUListi(id);
            punim = false;
            OtvoriDokument(id);
            MessageBox.Show("Dokument razduženja je sačuvan.");
        }

        private void Obrisi()
        {
            if (!idRazduzenja.HasValue)
                return;
            if (MessageBox.Show("Briše se dokument razduženja " + cboVrsta.Text + " " + txtBroj.Text + "/" + numGodina.Value
                    + " sa svim naimenovanjima i vezama sa otpremnicama. Nastaviti?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            try
            {
                Upit("sp_Razduzenje_Obrisi", CommandType.StoredProcedure, new SqlParameter("@ID", idRazduzenja.Value));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dokument nije obrisan: " + ex.Message);
                return;
            }
            izmenjeno = false;
            punim = true;
            UcitajListuDokumenata();
            punim = false;
            OtvoriDokument(null);
        }

        #endregion
    }
}
