using Syncfusion.Drawing;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.WinForms.DataGrid;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.Theme
{
    // Primena stila Sava (docs/SAVA-DESIGN.md) na formu: rekurzivno po tipu kontrole, dugmad i po Tag-u
    // ("primary" = glavno dugme, "danger" = brisanje / storniranje, ostala su sporedna).
    // Font same forme se ne menja (AutoScaleMode.Font bi pomerio kontrole); fontovi idu na pojedinačne kontrole.
    public static class ThemeHelper
    {
        // ApplyZbijeno: polja i dugmad 9 pt umesto 10 pt (forme sa mnogo polja jedno do drugog)
        [ThreadStatic] static bool zbijeno;
        static Font PoljeFont { get { return zbijeno ? SavaTheme.FieldFontZbijeno : SavaTheme.FieldFont; } }
        static Font DugmeFont { get { return zbijeno ? SavaTheme.ButtonFontZbijeno : SavaTheme.ButtonFont; } }
        static Font DugmeFontSemibold { get { return zbijeno ? SavaTheme.ButtonFontSemiboldZbijeno : SavaTheme.ButtonFontSemibold; } }

        public static void Apply(Control root)
        {
            if (root is Form forma)
                forma.BackColor = SavaTheme.Surface;

            foreach (Control c in root.Controls)
                ApplyControl(c);
        }

        public static void ApplyZbijeno(Control root)
        {
            zbijeno = true;
            try { Apply(root); }
            finally { zbijeno = false; }
        }

        private static void ApplyControl(Control c)
        {
            if (c is GridGroupingControl ggc)
            {
                StyleGrid(ggc);
                return;   // unutrašnje kontrole grida se ne diraju
            }
            if (c is DataGridView dgv)
            {
                StyleGrid(dgv);
                return;
            }
            if (c is SfDataGrid sfGrid)
            {
                StyleGrid(sfGrid);
                return;
            }

            if (c is Label label)
                StyleLabel(label);
            else if (c is TextBox textBox)
                StyleTextBox(textBox);
            else if (c is ComboBox comboBox)
            {
                comboBox.Font = PoljeFont;
                comboBox.ForeColor = SavaTheme.Text;
                comboBox.BackColor = SavaTheme.White;
            }
            else if (c is NumericUpDown numeric)
            {
                numeric.Font = PoljeFont;
                numeric.ForeColor = SavaTheme.Text;
                numeric.TextAlign = HorizontalAlignment.Right;
            }
            else if (c is CheckedListBox || c is ListBox)
            {
                c.Font = PoljeFont;
                c.ForeColor = SavaTheme.Text;
                c.BackColor = SavaTheme.White;
            }
            else if (c is CheckBox checkBox)
                StyleCheckBox(checkBox);
            else if (c is DateTimePicker)
                c.Font = PoljeFont;
            else if (c is Button button)
                StyleButton(button);
            else if (c is ToolStrip toolStrip)
                StyleToolStrip(toolStrip);
            else if (c is GroupBox groupBox)
                groupBox.ForeColor = SavaTheme.Label;

            foreach (Control dete in c.Controls)
                ApplyControl(dete);
        }

        // Oznaka: Segoe UI 9, boja Oznaka, bez dvotačke na kraju.
        // Oznaka sa okvirom je polje za prikaz (npr. podaci nadređenih nivoa): beli okvir, taman čitljiv tekst.
        public static void StyleLabel(Label label)
        {
            if (label.BorderStyle != BorderStyle.None)
            {
                label.BackColor = SavaTheme.White;
                label.ForeColor = SavaTheme.Text;
                label.Font = SavaTheme.LabelFont;
                return;
            }

            label.ForeColor = SavaTheme.Label;
            label.Font = SavaTheme.LabelFont;
            if (label.Text.EndsWith(":"))
                label.Text = label.Text.TrimEnd(':').TrimEnd();
        }

        public static void StyleTextBox(TextBox textBox)
        {
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Font = PoljeFont;
            textBox.ForeColor = SavaTheme.Text;
            textBox.BackColor = textBox.ReadOnly ? SavaTheme.Surface : SavaTheme.White;
        }

        // CheckBox: ravan, okvir kao polje za unos, kvačica u boji teksta, svetla pozadina pod mišem.
        // Čekiran i nečekiran izgledaju isto osim kvačice; plava se ne koristi (rezervisana je za glavno dugme).
        public static void StyleCheckBox(CheckBox checkBox)
        {
            checkBox.FlatStyle = FlatStyle.Flat;
            checkBox.FlatAppearance.BorderSize = 1;
            checkBox.FlatAppearance.BorderColor = SavaTheme.FieldBorder;
            checkBox.FlatAppearance.CheckedBackColor = SavaTheme.White;
            checkBox.FlatAppearance.MouseOverBackColor = SavaTheme.HoverLight;
            checkBox.FlatAppearance.MouseDownBackColor = SavaTheme.HoverLight;
            checkBox.Font = PoljeFont;
            checkBox.ForeColor = SavaTheme.Text;
            checkBox.BackColor = Color.Transparent;
            checkBox.UseVisualStyleBackColor = false;
            checkBox.Cursor = Cursors.Hand;
        }

        public static void StyleButton(Button btn)
        {
            if ((btn.Tag as string) == "grupa")   // traka grupe polja (GrupeForme): naslov dela forme, ne akcija
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.UseVisualStyleBackColor = false;
                btn.BackColor = SavaTheme.GridHeader;
                btn.ForeColor = SavaTheme.Text;
                btn.Font = SavaTheme.SectionFont;
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = SavaTheme.Line;
                btn.FlatAppearance.MouseOverBackColor = SavaTheme.HoverLight;
                btn.FlatAppearance.MouseDownBackColor = SavaTheme.HoverLight;
                return;
            }

            btn.FlatStyle = FlatStyle.Flat;
            btn.Font = DugmeFont;
            btn.Cursor = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.MouseDownBackColor = Color.Empty;

            string uloga = btn.Tag as string;
            if (uloga == "primary")
            {
                btn.BackColor = SavaTheme.Primary;
                btn.ForeColor = SavaTheme.White;
                btn.Font = DugmeFontSemibold;
                btn.FlatAppearance.BorderColor = SavaTheme.Primary;
                btn.FlatAppearance.MouseOverBackColor = SavaTheme.PrimaryHover;
            }
            else if (uloga == "danger")
            {
                btn.BackColor = SavaTheme.White;
                btn.ForeColor = SavaTheme.Danger;
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseOverBackColor = SavaTheme.DangerHover;
            }
            else
            {
                btn.BackColor = SavaTheme.White;
                btn.ForeColor = SavaTheme.Text;
                btn.FlatAppearance.BorderColor = SavaTheme.FieldBorder;
                btn.FlatAppearance.MouseOverBackColor = SavaTheme.HoverLight;
            }
        }

        // Oznake i checkbox-ovi napisani VELIKIM SLOVIMA dobijaju obična slova („MAGACINSKI BROJ“ -> „Magacinski broj“);
        // skraćenice ostaju velike (PIB, C7, JCI...). Tekst koji nije ceo velikim slovima se ne dira. Ne poziva se iz Apply,
        // nego posebno, posle koda forme koji postavlja tekstove; kontrole u izuzeci se preskaču (npr. naslov forme).
        public static void OznakeNormalnimSlovima(Control root, params Control[] izuzeci)
        {
            foreach (Control c in root.Controls)
            {
                if (Array.IndexOf(izuzeci, c) >= 0)
                    continue;
                if (c is Label || c is CheckBox)
                    c.Text = NormalnaSlova(c.Text);
                if (!(c is DataGridView) && !(c is GridGroupingControl) && !(c is SfDataGrid))
                    OznakeNormalnimSlovima(c, izuzeci);
            }
        }

        static readonly string[] Skracenice =
        {
            "ID", "MB", "CP", "CI", "JCI", "PIB", "ADR", "JM", "NHM", "RB", "RN", "LO", "PDV", "EUR", "USD", "RSD",
            "SD", "CMR", "TIR", "PL", "MP", "CNT", "HS", "UN", "EU", "BD", "LK", "CRM"
        };

        public static string NormalnaSlova(string tekst)
        {
            if (string.IsNullOrEmpty(tekst) || tekst.Count(char.IsLetter) < 2 || tekst != tekst.ToUpperInvariant())
                return tekst;
            string rezultat = System.Text.RegularExpressions.Regex.Replace(tekst, @"\p{L}[\p{L}\d]*", m =>
            {
                string rec = m.Value;
                if (rec.Any(char.IsDigit) || Array.IndexOf(Skracenice, rec) >= 0)
                    return rec;
                return rec.ToLowerInvariant();
            });
            int prvo = rezultat.TakeWhile(ch => !char.IsLetter(ch)).Count();
            if (prvo < rezultat.Length && char.IsLower(rezultat[prvo]))
                rezultat = rezultat.Substring(0, prvo) + char.ToUpperInvariant(rezultat[prvo]) + rezultat.Substring(prvo + 1);
            return rezultat;
        }

        // Zamena teksta oznaka (poređenje bez obzira na velika slova, prelome i razmake); nova oznaka je u jednom redu.
        // parovi: stari tekst, novi tekst, stari, novi...
        public static void ZameniOznake(Control root, params string[] parovi)
        {
            foreach (Control c in root.Controls)
            {
                if (c is Label l)
                {
                    string kljuc = string.Join(" ", l.Text.Split(new[] { ' ', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries));
                    for (int i = 0; i + 1 < parovi.Length; i += 2)
                        if (string.Equals(kljuc, parovi[i], StringComparison.OrdinalIgnoreCase))
                        {
                            int dno = l.Bottom;   // oznaka ostaje tik iznad svog polja
                            l.Text = parovi[i + 1];
                            l.AutoSize = true;
                            l.Top = dno - l.Height;
                            break;
                        }
                }
                if (!(c is DataGridView) && !(c is GridGroupingControl) && !(c is SfDataGrid) && c.Controls.Count > 0)
                    ZameniOznake(c, parovi);
            }
        }

        // Oznake datuma iz više redova („PLANIRANI / DATUM/VREME“) koje na Segoe UI ne staju između polja: kraće, u jednom redu
        public static readonly string[] OznakeDatuma =
        {
            "Planirani datum/vreme", "Planirano vreme",
            "Novi planirani datum/vreme", "Novo planirano vreme",
            "Planirani datum/vreme realizacije", "Vreme realizacije"
        };

        // Segoe UI je viši od starog fonta forme, pa oznaka iznad polja zalazi u njegovu gornju ivicu i prekriva je.
        // Oznaka koja stoji iznad polja (isti roditelj, preklapa se po širini, polje počinje pre kraja oznake)
        // podiže se tako da se završava 1 px iznad polja. Oznake pored polja (u istom redu) se ne diraju.
        public static void OznakeIznadPolja(Control root)
        {
            var polja = root.Controls.Cast<Control>()
                .Where(c => Postavljena(c) && (c is TextBox || c is ComboBox || c is NumericUpDown || c is DateTimePicker)).ToList();
            foreach (var l in root.Controls.OfType<Label>().Where(l => Postavljena(l) && l.BorderStyle == BorderStyle.None))
            {
                var polje = polja
                    .Where(p => p.Top > l.Top && p.Top < l.Bottom && p.Left < l.Right && p.Right > l.Left)
                    .OrderBy(p => p.Top).FirstOrDefault();
                if (polje != null)
                    l.Top = Math.Max(0, polje.Top - l.Height - 1);
            }
            foreach (Control c in root.Controls)
                if (!(c is DataGridView) && !(c is GridGroupingControl) && !(c is SfDataGrid) && c.Controls.Count > 0)
                    OznakeIznadPolja(c);
        }

        // Sopstvena vidljivost kontrole: Visible vraća false dok forma nije prikazana (npr. u konstruktoru)
        static readonly System.Reflection.MethodInfo getState =
            typeof(Control).GetMethod("GetState", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

        static bool Postavljena(Control c)
        {
            return getState == null ? c.Visible : (bool)getState.Invoke(c, new object[] { 2 });
        }

        // Panel sa poljima: bela kartica sa okvirom boje Linija (SAVA-DESIGN.md, sekcija 4)
        public static void Kartica(Panel panel)
        {
            panel.BackColor = SavaTheme.White;
            panel.Paint -= CrtajOkvir;
            panel.Paint += CrtajOkvir;
        }

        private static void CrtajOkvir(object sender, PaintEventArgs e)
        {
            var p = (Control)sender;
            ControlPaint.DrawBorder(e.Graphics, p.ClientRectangle, SavaTheme.Line, ButtonBorderStyle.Solid);
        }

        public static void StyleToolStrip(ToolStrip toolStrip)
        {
            toolStrip.BackColor = SavaTheme.White;
            toolStrip.Font = SavaTheme.ButtonFont;
            toolStrip.RenderMode = ToolStripRenderMode.System;
            foreach (ToolStripItem item in toolStrip.Items)
                item.ForeColor = SavaTheme.Text;
        }

        // Toolbar: bela pozadina, visina 52, donja linija; dugmad visine 32 (unutrašnja margina 10 gore i dole)
        public static void Toolbar(Panel bar)
        {
            bar.BackColor = SavaTheme.White;
            bar.BorderStyle = BorderStyle.None;
            bar.Height = SavaTheme.ToolbarHeight;
            bar.Padding = new Padding(12, (SavaTheme.ToolbarHeight - SavaTheme.ButtonHeight) / 2, 12,
                (SavaTheme.ToolbarHeight - SavaTheme.ButtonHeight) / 2);
            bar.Paint -= CrtajDonjuLiniju;
            bar.Paint += CrtajDonjuLiniju;
        }

        private static void CrtajDonjuLiniju(object sender, PaintEventArgs e)
        {
            var bar = (Control)sender;
            using (var olovka = new Pen(SavaTheme.Line))
                e.Graphics.DrawLine(olovka, 0, bar.Height - 1, bar.Width, bar.Height - 1);
        }

        // Širina dugmeta po tekstu (dugme poravnato levo u toolbar-u)
        public static void SirinaPoTekstu(Button btn)
        {
            btn.AutoSize = false;
            btn.Width = Math.Max(TextRenderer.MeasureText(btn.Text, btn.Font).Width + 28, 72);
        }

        // Kontrole poravnate levo, redom, sa razmakom od 8 px između njih
        public static void PoredjajLevo(Control kontejner, params Control[] redom)
        {
            for (int i = 0; i < redom.Length; i++)
            {
                if (i > 0)
                {
                    var razmak = new Panel { Width = 8, Dock = DockStyle.Left, BackColor = Color.Transparent };
                    kontejner.Controls.Add(razmak);
                    razmak.BringToFront();
                }
                Control c = redom[i];
                if (c.Parent != kontejner)
                    kontejner.Controls.Add(c);
                c.Dock = DockStyle.Left;
                c.BringToFront();   // poslednja dodata je najdesnija
            }
        }

        // Širina kontejnera prema kontrolama poravnatim levo u njemu
        public static void SirinaPoSadrzaju(Control kontejner)
        {
            int sirina = kontejner.Padding.Horizontal;
            foreach (Control c in kontejner.Controls)
                if (c.Visible && c.Dock == DockStyle.Left)
                    sirina += c.Width;
            kontejner.Width = sirina;
        }

        // Syncfusion GridGroupingControl: isti izgled kao tabela u dokumentu (sekcija 4, DataGridView)
        public static void StyleGrid(GridGroupingControl grid)
        {
            grid.BackColor = SavaTheme.White;
            grid.GridLineColor = SavaTheme.GridLine;
            grid.TableOptions.SelectionBackColor = SavaTheme.SelectedRow;
            grid.TableOptions.SelectionTextColor = SavaTheme.Text;
            grid.TableDescriptor.TableOptions.ColumnHeaderRowHeight = SavaTheme.GridHeaderHeight;
            grid.TableDescriptor.TableOptions.RecordRowHeight = SavaTheme.GridRowHeight;

            var zaglavlje = grid.TableDescriptor.Appearance.ColumnHeaderCell;
            zaglavlje.Interior = new BrushInfo(SavaTheme.GridHeader);
            zaglavlje.TextColor = SavaTheme.Text;
            zaglavlje.Font.Facename = SavaTheme.GridHeaderFont.Name;
            zaglavlje.Font.Size = SavaTheme.GridHeaderFont.Size;
            zaglavlje.Font.Bold = false;

            var celija = grid.TableDescriptor.Appearance.AnyRecordFieldCell;
            celija.Interior = new BrushInfo(SavaTheme.White);
            celija.TextColor = SavaTheme.Text;
            celija.Font.Facename = SavaTheme.FieldFont.Name;
            celija.Font.Size = SavaTheme.FieldFont.Size;

            // bez zebra redova
            grid.TableDescriptor.Appearance.AlternateRecordFieldCell.Interior = new BrushInfo(SavaTheme.White);
        }

        public static void StyleGrid(DataGridView grid)
        {
            grid.EnableHeadersVisualStyles = false;
            grid.BackgroundColor = SavaTheme.White;
            grid.BorderStyle = BorderStyle.None;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.GridColor = SavaTheme.GridLine;

            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.ColumnHeadersHeight = SavaTheme.GridHeaderHeight;
            grid.ColumnHeadersDefaultCellStyle.BackColor = SavaTheme.GridHeader;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = SavaTheme.Text;
            grid.ColumnHeadersDefaultCellStyle.Font = SavaTheme.GridHeaderFont;
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = SavaTheme.GridHeader;

            grid.RowTemplate.Height = SavaTheme.GridRowHeight;
            grid.DefaultCellStyle.Font = SavaTheme.FieldFont;
            grid.DefaultCellStyle.ForeColor = SavaTheme.Text;
            grid.DefaultCellStyle.BackColor = SavaTheme.White;
            grid.DefaultCellStyle.SelectionBackColor = SavaTheme.SelectedRow;
            grid.DefaultCellStyle.SelectionForeColor = SavaTheme.Text;
            grid.AlternatingRowsDefaultCellStyle.BackColor = SavaTheme.White;
        }

        // Syncfusion SfDataGrid: isti izgled kao tabela u dokumentu (sekcija 4, DataGridView)
        public static void StyleGrid(SfDataGrid grid)
        {
            grid.BackColor = SavaTheme.White;
            grid.RowHeight = SavaTheme.GridRowHeight;
            grid.HeaderRowHeight = SavaTheme.GridHeaderHeight;
            grid.Style.BorderColor = SavaTheme.Line;

            grid.Style.HeaderStyle.BackColor = SavaTheme.GridHeader;
            grid.Style.HeaderStyle.TextColor = SavaTheme.Text;
            grid.Style.HeaderStyle.Font.Facename = SavaTheme.GridHeaderFont.Name;
            grid.Style.HeaderStyle.Font.Size = SavaTheme.GridHeaderFont.Size;
            grid.Style.HeaderStyle.Font.Bold = false;

            grid.Style.CellStyle.BackColor = SavaTheme.White;
            grid.Style.CellStyle.TextColor = SavaTheme.Text;
            grid.Style.CellStyle.Font.Facename = SavaTheme.FieldFont.Name;
            grid.Style.CellStyle.Font.Size = SavaTheme.FieldFont.Size;

            grid.Style.SelectionStyle.BackColor = SavaTheme.SelectedRow;
            grid.Style.SelectionStyle.TextColor = SavaTheme.Text;
        }
    }
}
