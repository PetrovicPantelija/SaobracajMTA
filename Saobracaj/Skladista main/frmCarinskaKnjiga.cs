using Syncfusion.XlsIO;
using Saobracaj.Theme;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main
{
    // RAZ-16: carinska knjiga (evidencija carinskog skladišta, obrazac #1008), pored Magacinske knjige.
    // Redovi se izvode iz potvrđenih događaja (procedura sp_CarinskaKnjiga, skripta Carina_Knjiga_2026_11.sql):
    // usklađištenje naimenovanja C7 kad je prijemnica potvrđena i razduženja iz dokumenata C4 / IZ3 / C3...
    // sa tekućim nerazduženim stanjem posle svakog reda. Ekran samo prikazuje; ništa se ovde ne menja.
    public class frmCarinskaKnjiga : Form
    {
        readonly string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

        ComboBox cboGodina, cboMagacinskiBroj, cboVlasnik;
        DateTimePicker dtpOd, dtpDo;
        TextBox txtC7;
        DataGridView dgv;
        Label lblZbir;
        DataTable knjiga;

        // boje i fontovi iz palete Sava (Theme/SavaTheme.cs)

        static readonly Color Plava = SavaTheme.Primary;
        static readonly Color Oznaka = SavaTheme.Label;
        static readonly Color Povrsina = SavaTheme.Surface;
        static readonly Color ZaglavljeTabele = SavaTheme.GridHeader;
        static readonly Color IzabraniRed = SavaTheme.SelectedRow;
        static readonly Color Tekst = SavaTheme.Text;
        static readonly Font Polumasni = SavaTheme.GridHeaderFont;

        // kolone tabele: naziv iz procedure, zaglavlje (broj kolone obrasca), širina, format broja (null = tekst)
        static readonly string[][] Kolone =
        {
            new[] { "Rbr", "1 Rbr", "50", "" },
            new[] { "Datum", "2 Datum", "84", "d" },
            new[] { "CIPrijave", "3 CI prijave", "64", null },
            new[] { "DokumentPrijave", "4 Vrsta dokumenta, broj/godina", "150", null },
            new[] { "RBNaimenovanja", "5 RB", "40", "" },
            new[] { "Koleta", "6 Koleta", "72", "#,##0.###" },
            new[] { "Tezina", "7 Težina kg", "90", "N2" },
            new[] { "NerazduzenoKoleta", "8 Nerazduženo koleta", "90", "#,##0.###" },
            new[] { "NerazduzenaTezina", "9 Nerazduženo kg", "96", "N2" },
            new[] { "VrstaRobe", "10 Vrsta robe", "260", null },
            new[] { "Vrednost", "11 Vrednost", "96", "N2" },
            new[] { "Valuta", "Valuta", "50", null },
            new[] { "DokumentRazduzenja", "12 CI razduženja, vrsta, broj/godina", "170", null },
            new[] { "DatumIzdavanja", "13 Datum izdavanja", "84", "d" },
            new[] { "Napomena", "14 Napomena", "200", null },
            new[] { "Sektor", "15 Sektor", "60", null },
            new[] { "MagacinskiBroj", "Magacinski broj", "70", "" },
            new[] { "NHM", "Tarifni broj", "76", null },
        };

        // kolone koje opisuju usklađištenje: u redovima razduženja su sive (isti C7 i naimenovanje)
        static readonly HashSet<string> KoloneUskladistenja = new HashSet<string>
            { "Rbr", "CIPrijave", "DokumentPrijave", "RBNaimenovanja", "VrstaRobe", "MagacinskiBroj", "NHM" };

        public frmCarinskaKnjiga()
        {
            Text = "Carinska knjiga";
            Font = new Font("Segoe UI", 9f);
            BackColor = Color.White;
            Size = new Size(1400, 720);

            NapraviKontrole();
            Load += (s, e) =>
            {
                UcitajFiltere();
                Prikazi();
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

        private static Button Dugme(string tekst, int sirina, bool glavno)
        {
            var b = new Button { Text = tekst, Width = sirina, Height = 32, UseVisualStyleBackColor = !glavno };
            if (glavno)
            {
                b.BackColor = Plava;
                b.ForeColor = Color.White;
                b.FlatStyle = FlatStyle.Flat;
                b.FlatAppearance.BorderSize = 0;
                b.Tag = "primary";
            }
            return b;
        }

        private void NapraviKontrole()
        {
            var vrh = new Panel { Dock = DockStyle.Top, Height = 96, Padding = new Padding(20, 8, 20, 0) };
            vrh.Controls.Add(new Label
            {
                Text = "Carinska knjiga",
                Font = SavaTheme.FormTitleFont,
                ForeColor = Tekst,
                AutoSize = true,
                Location = new Point(18, 6)
            });

            int y = 42;
            cboGodina = Polje(vrh, "Godina", new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList }, 20, y, 90);
            cboMagacinskiBroj = Polje(vrh, "Magacinski broj", new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList }, 120, y, 200);
            cboVlasnik = Polje(vrh, "Vlasnik robe", new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList }, 330, y, 220);
            dtpOd = Polje(vrh, "Od datuma", new DateTimePicker { Format = DateTimePickerFormat.Short, ShowCheckBox = true, Checked = false }, 560, y, 120);
            dtpDo = Polje(vrh, "Do datuma", new DateTimePicker { Format = DateTimePickerFormat.Short, ShowCheckBox = true, Checked = false }, 690, y, 120);
            txtC7 = Polje(vrh, "Broj C7", new TextBox(), 820, y, 110);

            var btnPrikazi = Dugme("Prikaži", 100, true);
            btnPrikazi.Location = new Point(946, y + 16);
            btnPrikazi.Click += (s, e) => Prikazi();
            vrh.Controls.Add(btnPrikazi);

            var btnExcel = Dugme("Izvoz u Excel", 120, false);
            btnExcel.Location = new Point(1056, y + 16);
            btnExcel.Click += (s, e) => IzvozUExcel();
            vrh.Controls.Add(btnExcel);
            AcceptButton = btnPrikazi;
            txtC7.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; Prikazi(); } };

            dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                AutoGenerateColumns = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                EnableHeadersVisualStyles = false
            };
            dgv.RowTemplate.Height = 30;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.ColumnHeadersDefaultCellStyle.Font = SavaTheme.GridHeaderFont;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = ZaglavljeTabele;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Tekst;
            dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv.DefaultCellStyle.ForeColor = Tekst;
            dgv.DefaultCellStyle.SelectionBackColor = IzabraniRed;
            dgv.DefaultCellStyle.SelectionForeColor = Tekst;
            foreach (var k in Kolone)
            {
                var c = new DataGridViewTextBoxColumn
                {
                    Name = k[0],
                    DataPropertyName = k[0],
                    HeaderText = k[1],
                    Width = int.Parse(k[2]),
                    SortMode = DataGridViewColumnSortMode.NotSortable
                };
                if (k[3] != null)
                {
                    if (k[3] != "d")
                        c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    if (k[3] != "")
                        c.DefaultCellStyle.Format = k[3];
                }
                dgv.Columns.Add(c);
            }
            dgv.CellFormatting += Dgv_CellFormatting;

            var sredina = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 4, 20, 0) };
            sredina.Controls.Add(dgv);

            lblZbir = new Label { Dock = DockStyle.Bottom, Height = 40, Padding = new Padding(20, 0, 20, 0), TextAlign = ContentAlignment.MiddleLeft, ForeColor = Oznaka };

            Controls.Add(sredina);
            Controls.Add(vrh);
            Controls.Add(lblZbir);
        }

        // red usklađištenja ima pozadinu radne površine i polu-masni tekst količina; u redovima razduženja su
        // kolone usklađištenja sive, jer ponavljaju isti C7 i naimenovanje
        private void Dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || knjiga == null)
                return;
            var red = (dgv.Rows[e.RowIndex].DataBoundItem as DataRowView)?.Row;
            if (red == null)
                return;
            bool uskladistenje = Convert.ToInt32(red["VrstaReda"]) == 1;
            string kolona = dgv.Columns[e.ColumnIndex].Name;
            if (uskladistenje)
            {
                e.CellStyle.BackColor = Povrsina;
                if (kolona == "Koleta" || kolona == "Tezina" || kolona == "NerazduzenoKoleta" || kolona == "NerazduzenaTezina")
                    e.CellStyle.Font = Polumasni;
            }
            else if (KoloneUskladistenja.Contains(kolona))
                e.CellStyle.ForeColor = Oznaka;
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

        private void UcitajFiltere()
        {
            try
            {
                Napuni(cboGodina, Upit(@"SELECT DISTINCT mp.Godina AS ID, CONVERT(nvarchar(4), mp.Godina) AS Naziv
                                         FROM dbo.MagacinskiPredmet mp WHERE mp.Godina IS NOT NULL ORDER BY ID DESC", CommandType.Text), "Sve");
                Napuni(cboMagacinskiBroj, Upit(@"SELECT DISTINCT rns.MagacinskiBroj AS ID,
                                   CONVERT(nvarchar(10), rns.MagacinskiBroj) + ISNULL(N' - ' + RTRIM(CONVERT(nvarchar(100), mbc.Naziv)), N'') AS Naziv
                                FROM dbo.MagacinskiPredmet mp
                                JOIN dbo.RadniNalogSkladista rns ON rns.ID = mp.RN
                                LEFT JOIN dbo.MagacinskiBrojCarinski mbc ON mbc.ID = rns.MagacinskiBroj
                                WHERE ISNULL(rns.MagacinskiBroj, 0) <> 0
                                ORDER BY ID DESC", CommandType.Text), "Svi");
                Napuni(cboVlasnik, Upit("SELECT id AS ID, RTRIM(Naziv) AS Naziv FROM dbo.VlasniciRobe ORDER BY Naziv", CommandType.Text), "Svi");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Filteri nisu učitani: " + ex.Message, Text);
            }
        }

        private static object Izbor(ComboBox cbo)
        {
            var s = cbo.SelectedItem as Stavka;
            return s == null || !s.ID.HasValue ? (object)DBNull.Value : s.ID.Value;
        }

        private void Prikazi()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                knjiga = Upit("dbo.sp_CarinskaKnjiga", CommandType.StoredProcedure,
                    new SqlParameter("@Godina", Izbor(cboGodina)),
                    new SqlParameter("@MagacinskiBroj", Izbor(cboMagacinskiBroj)),
                    new SqlParameter("@Vlasnik", Izbor(cboVlasnik)),
                    new SqlParameter("@Od", dtpOd.Checked ? (object)dtpOd.Value.Date : DBNull.Value),
                    new SqlParameter("@Do", dtpDo.Checked ? (object)dtpDo.Value.Date : DBNull.Value),
                    new SqlParameter("@C7", txtC7.Text.Trim() == "" ? (object)DBNull.Value : txtC7.Text.Trim()));
                dgv.DataSource = knjiga;
                PrikaziZbir();
            }
            catch (SqlException ex) when (ex.Number == 2812)   // procedura ne postoji
            {
                MessageBox.Show("Carinska knjiga nije dostupna: na bazi nije puštena skripta Carina_Knjiga_2026_11.sql.", Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Carinska knjiga nije učitana: " + ex.Message, Text);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void PrikaziZbir()
        {
            var redovi = knjiga.Rows.Cast<DataRow>().ToList();
            Func<int, string, decimal> zbir = (vrsta, kolona) => redovi
                .Where(r => Convert.ToInt32(r["VrstaReda"]) == vrsta && r[kolona] != DBNull.Value)
                .Sum(r => Convert.ToDecimal(r[kolona]));
            int naimenovanja = redovi.Select(r => Convert.ToInt32(r["IDNaimenovanja"])).Distinct().Count();
            lblZbir.Text = string.Format(
                "Redova: {0}   ·   Naimenovanja: {1}   ·   Usklađišteno: {2:#,##0.###} koleta, {3:N2} kg   ·   Razduženo: {4:#,##0.###} koleta, {5:N2} kg",
                redovi.Count, naimenovanja, zbir(1, "Koleta"), zbir(1, "Tezina"), -zbir(2, "Koleta"), -zbir(2, "Tezina"));
        }

        private void IzvozUExcel()
        {
            if (knjiga == null || knjiga.Rows.Count == 0)
            {
                MessageBox.Show("Knjiga je prazna. Izaberite filtere i kliknite Prikaži.", Text);
                return;
            }

            // tabela za izvoz: kolone i zaglavlja kao na ekranu
            var izvoz = new DataTable("CarinskaKnjiga");
            foreach (var k in Kolone)
            {
                var tip = knjiga.Columns[k[0]].DataType;
                izvoz.Columns.Add(k[1], tip);
            }
            foreach (DataRow r in knjiga.Rows)
                izvoz.Rows.Add(Kolone.Select(k => r[k[0]]).ToArray());

            string naziv = "Carinska_knjiga" + (Izbor(cboGodina) is int g ? "_" + g : "") + "_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx";
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
                        ws.Name = "Carinska knjiga";
                        ws.ImportDataTable(izvoz, true, 1, 1);
                        ws.Range[1, 1, 1, izvoz.Columns.Count].CellStyle.Font.Bold = true;
                        for (int i = 0; i < Kolone.Length; i++)
                        {
                            string f = Kolone[i][3];
                            if (string.IsNullOrEmpty(f))
                                continue;
                            ws.Range[2, i + 1, izvoz.Rows.Count + 1, i + 1].NumberFormat =
                                f == "d" ? "dd.mm.yyyy" : f == "N2" ? "#,##0.00" : "#,##0.###";
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
