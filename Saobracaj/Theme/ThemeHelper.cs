using Syncfusion.Drawing;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Theme
{
    // Primena stila Sava (docs/SAVA-DESIGN.md) na formu: rekurzivno po tipu kontrole, dugmad i po Tag-u
    // ("primary" = glavno dugme, "danger" = brisanje / storniranje, ostala su sporedna).
    // Font same forme se ne menja (AutoScaleMode.Font bi pomerio kontrole); fontovi idu na pojedinačne kontrole.
    public static class ThemeHelper
    {
        public static void Apply(Control root)
        {
            if (root is Form forma)
                forma.BackColor = SavaTheme.Surface;

            foreach (Control c in root.Controls)
                ApplyControl(c);
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

            if (c is Label label)
                StyleLabel(label);
            else if (c is TextBox textBox)
                StyleTextBox(textBox);
            else if (c is ComboBox comboBox)
            {
                comboBox.Font = SavaTheme.FieldFont;
                comboBox.ForeColor = SavaTheme.Text;
                comboBox.BackColor = SavaTheme.White;
            }
            else if (c is NumericUpDown numeric)
            {
                numeric.Font = SavaTheme.FieldFont;
                numeric.ForeColor = SavaTheme.Text;
                numeric.TextAlign = HorizontalAlignment.Right;
            }
            else if (c is CheckedListBox || c is ListBox)
            {
                c.Font = SavaTheme.FieldFont;
                c.ForeColor = SavaTheme.Text;
                c.BackColor = SavaTheme.White;
            }
            else if (c is CheckBox checkBox)
                StyleCheckBox(checkBox);
            else if (c is DateTimePicker)
                c.Font = SavaTheme.FieldFont;
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
            textBox.Font = SavaTheme.FieldFont;
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
            checkBox.Font = SavaTheme.FieldFont;
            checkBox.ForeColor = SavaTheme.Text;
            checkBox.BackColor = Color.Transparent;
            checkBox.UseVisualStyleBackColor = false;
            checkBox.Cursor = Cursors.Hand;
        }

        public static void StyleButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.Font = SavaTheme.ButtonFont;
            btn.Cursor = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.MouseDownBackColor = Color.Empty;

            string uloga = btn.Tag as string;
            if (uloga == "primary")
            {
                btn.BackColor = SavaTheme.Primary;
                btn.ForeColor = SavaTheme.White;
                btn.Font = SavaTheme.ButtonFontSemibold;
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
    }
}
