using Syncfusion.XlsIO;
using Saobracaj.Theme;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main
{
    // Pregled nerazduženih C7 i izveštaj o skladištenju (Carinsko skladište > Pregled nerazduženih C7/SD).
    // Jedan red = jedan C7 (magacinski predmet). Početak skladištenja je potvrda prijemnice (status ZA), kraj je
    // poslednje razduženje preko otpreme (dokument razduženja vezan za otpremnice), kad je C7 razdužen do kraja.
    // Procedure sp_C7_Skladistenje i sp_C7_Razduzenja (skripta Carina_SkladistenjeC7_2026_11.sql). Ekran samo prikazuje.
    public class frmSkladistenjeC7 : Form
    {
        readonly string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

        // zajednički filteri
        ComboBox cboMagacinskiBroj, cboVlasnik;
        TextBox txtC7;
        TabControl kartice;

        // Nerazduženi C7
        NumericUpDown numRok;
        ComboBox cboPrikaz;
        DataGridView dgvNerazduzeni;
        DataTable nerazduzeni;

        // Izveštaj o skladištenju
        DateTimePicker dtpOd, dtpDo;
        CheckBox chkSamoNerazduzeni;
        DataGridView dgvSkladistenje, dgvRazduzenja;
        DataTable skladistenje;

        Label lblZbir, lblNaslov, lblNaslovRazduzenja;

        // kolone: naziv iz procedure, zaglavlje, širina, format (null = tekst, "d" = datum)
        static readonly string[][] KoloneNerazduzeni =
        {
            new[] { "MagacinskiBrojOznaka", "Magacinski broj", "90", null },
            new[] { "BrojC7", "Broj C7", "170", null },
            new[] { "Vlasnik", "Vlasnik robe", "200", null },
            new[] { "Pocetak", "Početak skladištenja", "100", "d" },
            new[] { "DanaUkupno", "Dana na skladištu", "80", "N0" },
            new[] { "Preostalo", "Do isteka roka (dana)", "90", "N0" },
            new[] { "NerazduzenoKoleta", "Nerazduženo koleta", "90", "#,##0.###" },
            new[] { "NerazduzenoBruto", "Nerazduženo bruto kg", "110", "N2" },
            new[] { "NerazduzenaVrednost", "Nerazdužena vrednost", "110", "N2" },
            new[] { "Valuta", "Valuta", "50", null },
            new[] { "Koleta", "Prijavljeno koleta", "90", "#,##0.###" },
            new[] { "Bruto", "Prijavljeno bruto kg", "110", "N2" },
            new[] { "BrojRazduzenja", "Razduženja", "80", "N0" },
            new[] { "PoslednjeRazduzenje", "Poslednje razduženje", "100", "d" },
            new[] { "RN", "Nalog", "70", "" },
        };

        static readonly string[][] KoloneSkladistenje =
        {
            new[] { "MagacinskiBrojOznaka", "Magacinski broj", "90", null },
            new[] { "BrojC7", "Broj C7", "170", null },
            new[] { "Vlasnik", "Vlasnik robe", "200", null },
            new[] { "Pocetak", "Početak skladištenja", "100", "d" },
            new[] { "Kraj", "Kraj skladištenja", "100", "d" },
            new[] { "Status", "Status", "90", null },
            new[] { "DanaUkupno", "Dana ukupno", "70", "N0" },
            new[] { "DanaUPeriodu", "Dana u periodu", "70", "N0" },
            new[] { "Koleta", "Prijavljeno koleta", "90", "#,##0.###" },
            new[] { "Bruto", "Prijavljeno bruto kg", "110", "N2" },
            new[] { "RazduzenoBruto", "Razduženo bruto kg", "110", "N2" },
            new[] { "NerazduzenoBruto", "Nerazduženo bruto kg", "110", "N2" },
            new[] { "DokumentiRazduzenja", "Dokumenti razduženja", "220", null },
            new[] { "Otpremnice", "Otpremnice", "110", null },
            new[] { "RN", "Nalog", "70", "" },
        };

        static readonly string[][] KoloneRazduzenja =
        {
            new[] { "Datum", "Datum razduženja", "100", "d" },
            new[] { "Dokument", "Dokument razduženja", "180", null },
            new[] { "Otpremnice", "Otpremnice", "120", null },
            new[] { "DanaOdPocetka", "Dana od početka", "80", "N0" },
            new[] { "Koleta", "Koleta", "80", "#,##0.###" },
            new[] { "Bruto", "Bruto kg", "100", "N2" },
            new[] { "Neto", "Neto kg", "100", "N2" },
            new[] { "Vrednost", "Vrednost", "100", "N2" },
            new[] { "NerazduzenoKoleta", "Nerazduženo koleta posle", "100", "#,##0.###" },
            new[] { "NerazduzenoBruto", "Nerazduženo bruto kg posle", "120", "N2" },
        };

        public frmSkladistenjeC7()
        {
            Text = "Nerazduženi C7 i skladištenje";
            Font = new Font("Segoe UI", 9f);
            Size = new Size(1400, 760);

            NapraviKontrole();
            ThemeHelper.Apply(this);
            foreach (var g in new[] { dgvNerazduzeni, dgvSkladistenje, dgvRazduzenja })
                ThemeHelper.StyleGrid(g);
            // Apply daje svim oznakama font oznake; naslovi i zbir dobijaju svoj
            lblNaslov.Font = SavaTheme.FormTitleFont;
            lblNaslov.ForeColor = SavaTheme.Text;
            lblNaslovRazduzenja.Font = SavaTheme.GridHeaderFont;
            lblNaslovRazduzenja.ForeColor = SavaTheme.Text;
            lblZbir.ForeColor = SavaTheme.Label;

            Load += (s, e) =>
            {
                UcitajFiltere();
                Prikazi();
            };
        }

        #region Izgled

        private static Label Natpis(Control roditelj, string tekst, int x, int y)
        {
            var l = new Label { Text = tekst, AutoSize = true, Location = new Point(x, y) };
            roditelj.Controls.Add(l);
            return l;
        }

        private static T Polje<T>(Control roditelj, string natpis, T c, int x, int y, int sirina) where T : Control
        {
            Natpis(roditelj, natpis, x, y);
            c.Location = new Point(x, y + 18);
            c.Width = sirina;
            roditelj.Controls.Add(c);
            return c;
        }

        private static Button Dugme(string tekst, int sirina, bool glavno)
        {
            return new Button { Text = tekst, Width = sirina, Height = 32, Tag = glavno ? "primary" : null };
        }

        private static DataGridView Tabela(string[][] kolone)
        {
            var dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                AutoGenerateColumns = false,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            foreach (var k in kolone)
            {
                var c = new DataGridViewTextBoxColumn
                {
                    Name = k[0],
                    DataPropertyName = k[0],
                    HeaderText = k[1],
                    Width = int.Parse(k[2]),
                    SortMode = DataGridViewColumnSortMode.Automatic
                };
                if (k[3] != null)
                {
                    if (k[3] != "d")
                        c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    if (k[3] != "")
                        c.DefaultCellStyle.Format = k[3] == "d" ? "dd.MM.yyyy" : k[3];
                }
                dgv.Columns.Add(c);
            }
            return dgv;
        }

        private void NapraviKontrole()
        {
            // zajednički filteri i akcije
            var vrh = new Panel { Dock = DockStyle.Top, Height = 96, Padding = new Padding(20, 8, 20, 0) };
            vrh.Controls.Add(lblNaslov = new Label
            {
                Text = "Nerazduženi C7 i skladištenje",
                Font = SavaTheme.FormTitleFont,
                ForeColor = SavaTheme.Text,
                AutoSize = true,
                Location = new Point(18, 6),
                Tag = "naslov"
            });

            int y = 42;
            cboMagacinskiBroj = Polje(vrh, "Magacinski broj", new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList }, 20, y, 200);
            cboVlasnik = Polje(vrh, "Vlasnik robe", new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList }, 230, y, 240);
            txtC7 = Polje(vrh, "Broj C7", new TextBox(), 480, y, 140);

            var btnPrikazi = Dugme("Prikaži", 100, true);
            btnPrikazi.Location = new Point(636, y + 16);
            btnPrikazi.Click += (s, e) => Prikazi();
            vrh.Controls.Add(btnPrikazi);

            var btnExcel = Dugme("Izvoz u Excel", 120, false);
            btnExcel.Location = new Point(746, y + 16);
            btnExcel.Click += (s, e) => IzvozUExcel();
            vrh.Controls.Add(btnExcel);
            AcceptButton = btnPrikazi;

            kartice = new TabControl { Dock = DockStyle.Fill };
            kartice.TabPages.Add(KarticaNerazduzeni());
            kartice.TabPages.Add(KarticaSkladistenje());
            kartice.SelectedIndexChanged += (s, e) => { Prikazi(); };

            var sredina = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 4, 20, 0) };
            sredina.Controls.Add(kartice);

            lblZbir = new Label { Dock = DockStyle.Bottom, Height = 40, Padding = new Padding(20, 0, 20, 0), TextAlign = ContentAlignment.MiddleLeft };

            Controls.Add(sredina);
            Controls.Add(vrh);
            Controls.Add(lblZbir);
        }

        private TabPage KarticaNerazduzeni()
        {
            var strana = new TabPage("Nerazduženi C7") { Padding = new Padding(8) };
            var filteri = new Panel { Dock = DockStyle.Top, Height = 58 };

            numRok = Polje(filteri, "Rok (dana)", new NumericUpDown { Minimum = 1, Maximum = 3650, Value = 90 }, 4, 4, 80);
            cboPrikaz = Polje(filteri, "Prikaz", new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList }, 100, 4, 260);
            cboPrikaz.Items.AddRange(new object[] { "Preko roka", "Ističu u narednih 30 dana ili su preko roka", "Svi nerazduženi" });
            cboPrikaz.SelectedIndex = 0;
            cboPrikaz.SelectionChangeCommitted += (s, e) => Prikazi();
            Natpis(filteri, "Rok se računa od potvrde prijemnice. Crveno: rok je istekao.", 380, 26);

            dgvNerazduzeni = Tabela(KoloneNerazduzeni);
            dgvNerazduzeni.CellFormatting += DgvNerazduzeni_CellFormatting;

            strana.Controls.Add(dgvNerazduzeni);
            strana.Controls.Add(filteri);
            return strana;
        }

        private TabPage KarticaSkladistenje()
        {
            var strana = new TabPage("Izveštaj o skladištenju") { Padding = new Padding(8) };
            var filteri = new Panel { Dock = DockStyle.Top, Height = 58 };

            var danas = DateTime.Today;
            dtpOd = Polje(filteri, "Od datuma", new DateTimePicker { Format = DateTimePickerFormat.Short, Value = new DateTime(danas.Year, danas.Month, 1) }, 4, 4, 120);
            dtpDo = Polje(filteri, "Do datuma", new DateTimePicker { Format = DateTimePickerFormat.Short, Value = danas }, 134, 4, 120);
            chkSamoNerazduzeni = new CheckBox { Text = "Samo nerazduženi", AutoSize = true, Location = new Point(270, 25) };
            filteri.Controls.Add(chkSamoNerazduzeni);
            Natpis(filteri, "C7 koji su bili na skladištu u periodu. Početak: potvrda prijemnice; kraj: poslednje razduženje preko otpreme.", 420, 26);

            dgvSkladistenje = Tabela(KoloneSkladistenje);
            dgvSkladistenje.SelectionChanged += (s, e) => PrikaziRazduzenja();

            dgvRazduzenja = Tabela(KoloneRazduzenja);
            var naslovRazduzenja = lblNaslovRazduzenja = new Label
            {
                Text = "Razduženja izabranog C7",
                Dock = DockStyle.Top,
                Height = 24,
                Font = SavaTheme.GridHeaderFont,
                TextAlign = ContentAlignment.BottomLeft
            };

            var podela = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Horizontal, SplitterWidth = 6 };
            podela.Panel1.Controls.Add(dgvSkladistenje);
            podela.Panel2.Controls.Add(dgvRazduzenja);
            podela.Panel2.Controls.Add(naslovRazduzenja);
            strana.Controls.Add(podela);
            strana.Controls.Add(filteri);
            strana.Layout += (s, e) =>
            {
                if (podela.Height > 300 && podela.Tag == null)
                {
                    podela.SplitterDistance = podela.Height * 2 / 3;
                    podela.Tag = "postavljeno";
                }
            };
            return strana;
        }

        // istekao rok: dani i ostatak roka crveno
        private void DgvNerazduzeni_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            string kolona = dgvNerazduzeni.Columns[e.ColumnIndex].Name;
            if (kolona != "DanaUkupno" && kolona != "Preostalo")
                return;
            var red = (dgvNerazduzeni.Rows[e.RowIndex].DataBoundItem as DataRowView)?.Row;
            if (red != null && red["Preostalo"] != DBNull.Value && Convert.ToInt32(red["Preostalo"]) <= 0)
            {
                e.CellStyle.ForeColor = SavaTheme.Danger;
                e.CellStyle.Font = SavaTheme.GridHeaderFont;
            }
        }

        #endregion

        #region Podaci

        private class Stavka
        {
            public int? ID;
            public string Tekst;
            public override string ToString() { return Tekst; }
        }

        private DataTable Upit(string sql, CommandType tip, params SqlParameter[] parametri)
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(connection))
            using (var cmd = new SqlCommand(sql, conn) { CommandType = tip, CommandTimeout = 120 })
            {
                cmd.Parameters.AddRange(parametri);
                using (var da = new SqlDataAdapter(cmd))
                    da.Fill(dt);
            }
            return dt;
        }

        private static void Napuni(ComboBox cbo, DataTable dt, string sve)
        {
            cbo.Items.Clear();
            cbo.Items.Add(new Stavka { ID = null, Tekst = sve });
            foreach (DataRow r in dt.Rows)
                cbo.Items.Add(new Stavka { ID = Convert.ToInt32(r["ID"]), Tekst = Convert.ToString(r["Naziv"]) });
            cbo.SelectedIndex = 0;
        }

        private static object Izbor(ComboBox cbo)
        {
            var s = cbo.SelectedItem as Stavka;
            return s == null || !s.ID.HasValue ? (object)DBNull.Value : s.ID.Value;
        }

        private void UcitajFiltere()
        {
            try
            {
                Napuni(cboMagacinskiBroj, Upit(@"SELECT DISTINCT rns.MagacinskiBroj AS ID,
                                   " + MagacinskiBrojOznaka.IzrazSaNazivom(connection, "rns.MagacinskiBroj", "mbc.Naziv") + @" AS Naziv
                                FROM dbo.MagacinskiPredmet mp
                                JOIN dbo.RadniNalogSkladista rns ON rns.ID = mp.RN
                                LEFT JOIN dbo.MagacinskiBrojCarinski mbc ON mbc.ID = rns.MagacinskiBroj
                                WHERE ISNULL(rns.MagacinskiBroj, 0) <> 0
                                  AND EXISTS (SELECT 1 FROM dbo.RNCarinskoStavke n WHERE n.IDNadredjena = mp.ID)
                                ORDER BY ID DESC", CommandType.Text), "Svi");
                Napuni(cboVlasnik, Upit("SELECT id AS ID, RTRIM(Naziv) AS Naziv FROM dbo.VlasniciRobe ORDER BY Naziv", CommandType.Text), "Svi");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Filteri nisu učitani: " + ex.Message, Text);
            }
        }

        private object C7Filter()
        {
            return txtC7.Text.Trim() == "" ? (object)DBNull.Value : txtC7.Text.Trim();
        }

        private void Prikazi()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (kartice.SelectedIndex == 0)
                    PrikaziNerazduzene();
                else
                    PrikaziSkladistenje();
            }
            catch (SqlException ex) when (ex.Number == 2812)   // procedura ne postoji
            {
                MessageBox.Show("Pregled nije dostupan: na bazi nije puštena skripta Carina_SkladistenjeC7_2026_11.sql.", Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Podaci nisu učitani: " + ex.Message, Text);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void PrikaziNerazduzene()
        {
            int rok = (int)numRok.Value;
            object minDana = cboPrikaz.SelectedIndex == 0 ? rok
                           : cboPrikaz.SelectedIndex == 1 ? Math.Max(0, rok - 30)
                           : (object)DBNull.Value;
            nerazduzeni = Upit("dbo.sp_C7_Skladistenje", CommandType.StoredProcedure,
                new SqlParameter("@Vlasnik", Izbor(cboVlasnik)),
                new SqlParameter("@MagacinskiBroj", Izbor(cboMagacinskiBroj)),
                new SqlParameter("@SamoNerazduzeni", true),
                new SqlParameter("@MinDana", minDana),
                new SqlParameter("@C7", C7Filter()));

            // do isteka roka: rok - dani na skladištu (0 ili manje = istekao); najstariji prvi
            nerazduzeni.Columns.Add("Preostalo", typeof(int));
            foreach (DataRow r in nerazduzeni.Rows)
                r["Preostalo"] = rok - Convert.ToInt32(r["DanaUkupno"]);
            nerazduzeni.DefaultView.Sort = "DanaUkupno DESC";
            dgvNerazduzeni.DataSource = nerazduzeni.DefaultView;

            var redovi = nerazduzeni.Rows.Cast<DataRow>().ToList();
            int istekao = redovi.Count(r => Convert.ToInt32(r["Preostalo"]) <= 0);
            lblZbir.Text = string.Format(
                "C7: {0}   ·   Istekao rok od {1} dana: {2}   ·   Nerazduženo: {3:#,##0.###} koleta, {4:N2} kg",
                redovi.Count, rok, istekao,
                redovi.Sum(r => Convert.ToDecimal(r["NerazduzenoKoleta"])), redovi.Sum(r => Convert.ToDecimal(r["NerazduzenoBruto"])));
        }

        private void PrikaziSkladistenje()
        {
            if (dtpDo.Value.Date < dtpOd.Value.Date)
            {
                MessageBox.Show("Datum Do je pre datuma Od.", Text);
                return;
            }
            skladistenje = Upit("dbo.sp_C7_Skladistenje", CommandType.StoredProcedure,
                new SqlParameter("@Od", dtpOd.Value.Date),
                new SqlParameter("@Do", dtpDo.Value.Date),
                new SqlParameter("@Vlasnik", Izbor(cboVlasnik)),
                new SqlParameter("@MagacinskiBroj", Izbor(cboMagacinskiBroj)),
                new SqlParameter("@SamoNerazduzeni", chkSamoNerazduzeni.Checked),
                new SqlParameter("@MinDana", DBNull.Value),
                new SqlParameter("@C7", C7Filter()));
            dgvSkladistenje.DataSource = skladistenje;
            PrikaziRazduzenja();

            var redovi = skladistenje.Rows.Cast<DataRow>().ToList();
            lblZbir.Text = string.Format(
                "C7: {0}   ·   Razduženo do kraja: {1}   ·   Na skladištu: {2}   ·   Dana u periodu ukupno: {3:N0}   ·   Nerazduženo: {4:N2} kg",
                redovi.Count, redovi.Count(r => r["Kraj"] != DBNull.Value), redovi.Count(r => r["Kraj"] == DBNull.Value),
                redovi.Sum(r => r["DanaUPeriodu"] == DBNull.Value ? 0 : Convert.ToInt32(r["DanaUPeriodu"])),
                redovi.Sum(r => Convert.ToDecimal(r["NerazduzenoBruto"])));
        }

        private void PrikaziRazduzenja()
        {
            var red = (dgvSkladistenje.CurrentRow?.DataBoundItem as DataRowView)?.Row;
            if (red == null)
            {
                dgvRazduzenja.DataSource = null;
                return;
            }
            try
            {
                dgvRazduzenja.DataSource = Upit("dbo.sp_C7_Razduzenja", CommandType.StoredProcedure,
                    new SqlParameter("@MagacinskiPredmet", Convert.ToInt32(red["IDMagacinskogPredmeta"])));
            }
            catch (Exception ex)
            {
                dgvRazduzenja.DataSource = null;
                lblZbir.Text = "Razduženja nisu učitana: " + ex.Message;
            }
        }

        private void IzvozUExcel()
        {
            bool prva = kartice.SelectedIndex == 0;
            var podaci = prva ? nerazduzeni : skladistenje;
            var kolone = prva ? KoloneNerazduzeni : KoloneSkladistenje;
            if (podaci == null || podaci.Rows.Count == 0)
            {
                MessageBox.Show("Tabela je prazna. Izaberite filtere i kliknite Prikaži.", Text);
                return;
            }

            // kolone i zaglavlja kao na ekranu, redosled kao na ekranu (sortiranje)
            var izvoz = new DataTable(prva ? "NerazduzeniC7" : "Skladistenje");
            foreach (var k in kolone)
                izvoz.Columns.Add(k[1], podaci.Columns[k[0]].DataType);
            foreach (DataRowView r in podaci.DefaultView)
                izvoz.Rows.Add(kolone.Select(k => r[k[0]]).ToArray());

            string naziv = (prva ? "Nerazduzeni_C7_" : "Skladistenje_C7_" + dtpOd.Value.ToString("yyyyMMdd") + "-" + dtpDo.Value.ToString("yyyyMMdd") + "_")
                           + DateTime.Now.ToString("yyyyMMdd") + ".xlsx";
            string fajl;
            using (var sfd = new SaveFileDialog { Filter = "Excel (*.xlsx)|*.xlsx", FileName = naziv })
            {
                if (sfd.ShowDialog(this) != DialogResult.OK)
                    return;
                fajl = sfd.FileName;
                try
                {
                    using (var engine = new ExcelEngine())
                    {
                        IApplication app = engine.Excel;
                        app.DefaultVersion = ExcelVersion.Xlsx;
                        IWorkbook wb = app.Workbooks.Create(1);
                        IWorksheet ws = wb.Worksheets[0];
                        ws.Name = prva ? "Nerazduženi C7" : "Skladištenje";
                        ws.ImportDataTable(izvoz, true, 1, 1);
                        ws.Range[1, 1, 1, izvoz.Columns.Count].CellStyle.Font.Bold = true;
                        for (int i = 0; i < kolone.Length; i++)
                        {
                            string f = kolone[i][3];
                            if (string.IsNullOrEmpty(f))
                                continue;
                            ws.Range[2, i + 1, izvoz.Rows.Count + 1, i + 1].NumberFormat =
                                f == "d" ? "dd.mm.yyyy" : f == "N2" ? "#,##0.00" : f == "N0" ? "#,##0" : "#,##0.###";
                        }
                        ws.UsedRange.AutofitColumns();
                        wb.SaveAs(fajl);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Izvoz nije uspeo: " + ex.Message, Text);
                    return;
                }
            }
            try { System.Diagnostics.Process.Start(fajl); } catch { }
        }

        #endregion
    }
}
