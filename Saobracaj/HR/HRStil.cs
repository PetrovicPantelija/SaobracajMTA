using Saobracaj.Theme;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.HR
{
    // Raspored zajednički za sve forme HR modula (izgled formi, strana 2):
    // 1 naslovna traka, 2 alatna traka, 3 zaglavlje, 4 kartice, 5 sadržaj, 6 statusna linija.
    // Boje i fontovi su iz stila Sava; boje statusa (ističe / isteklo / važi) se koriste samo za tekst statusa.
    public static class HRStil
    {
        public static readonly Color StatusVazi = ColorTranslator.FromHtml("#2E7D32");
        public static readonly Color StatusIstice = ColorTranslator.FromHtml("#B45309");
        public static readonly Color StatusIsteklo = SavaTheme.Danger;
        public static readonly Color StatusInfo = SavaTheme.Primary;
        public static readonly Color PozadinaVazi = ColorTranslator.FromHtml("#E7F4EA");
        public static readonly Color PozadinaIstice = ColorTranslator.FromHtml("#FDF0E1");
        public static readonly Color PozadinaIsteklo = ColorTranslator.FromHtml("#FBEAEA");
        public static readonly Color Zaglavlje = ColorTranslator.FromHtml("#EAF0F6");
        public static readonly Font VelikiNaslov = new Font("Segoe UI Semibold", 16f);
        public static readonly Font VelikiBroj = new Font("Segoe UI Semibold", 26f);

        // Forma se otvara u glavnom prozoru (NewMain.ShowChild) ili samostalno
        public static void PripremiFormu(Form forma, string naslov)
        {
            forma.Text = naslov;
            forma.BackColor = SavaTheme.Surface;
            forma.StartPosition = FormStartPosition.CenterScreen;
            forma.Size = new Size(1280, 800);
        }

        // 1. Naslovna traka: "Modul › zapis" levo, informacija ili komande desno
        public static HRNaslovnaTraka NaslovnaTraka(Control roditelj, string naslov)
        {
            var traka = new HRNaslovnaTraka(naslov);
            roditelj.Controls.Add(traka);
            return traka;
        }

        // 2. Alatna traka: komande levo (redom), glavna akcija desno ako je zadata
        public static HRAlatnaTraka AlatnaTraka(Control roditelj)
        {
            var traka = new HRAlatnaTraka();
            roditelj.Controls.Add(traka);
            return traka;
        }

        // 6. Statusna linija (audit, napomena)
        public static Label StatusnaLinija(Control roditelj)
        {
            var panel = new Panel { Dock = DockStyle.Bottom, Height = 34, BackColor = Zaglavlje, Padding = new Padding(20, 0, 20, 0) };
            var tekst = new Label { Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft, AutoEllipsis = true };
            panel.Controls.Add(tekst);
            panel.Paint += (s, e) => e.Graphics.DrawLine(new Pen(SavaTheme.Line), 0, 0, panel.Width, 0);
            roditelj.Controls.Add(panel);
            return tekst;
        }

        public static Button Dugme(string tekst, string uloga = null)
        {
            var b = new Button
            {
                Text = tekst,
                Tag = uloga,
                Height = SavaTheme.ButtonHeight,
                Font = uloga == "primary" ? SavaTheme.ButtonFontSemibold : SavaTheme.ButtonFont,
                Margin = new Padding(0, 0, 8, 0),
                UseVisualStyleBackColor = false
            };
            b.Width = Math.Max(TextRenderer.MeasureText(tekst, b.Font).Width + 28, 72);
            ThemeHelper.StyleButton(b);
            return b;
        }

        // Bela površina sa okvirom (panel sa poljima, tabela, kartica)
        public static Panel BeliPanel()
        {
            var p = new Panel { BackColor = SavaTheme.White, Padding = new Padding(1) };
            p.Paint += (s, e) => ControlPaint.DrawBorder(e.Graphics, p.ClientRectangle, SavaTheme.Line, ButtonBorderStyle.Solid);
            return p;
        }

        public static Label Oznaka(string tekst)
        {
            return new Label
            {
                Text = tekst,
                AutoSize = true,
                Font = SavaTheme.LabelFont,
                ForeColor = SavaTheme.Label,
                Margin = new Padding(0, 0, 0, 4)
            };
        }

        // Vrednost za prikaz (zaglavlje forme): polumasni tekst koji ThemeHelper ne menja
        public static HRTekst Vrednost(string tekst, Font font = null)
        {
            return new HRTekst
            {
                Text = tekst,
                Font = font ?? SavaTheme.ButtonFontSemibold,
                ForeColor = SavaTheme.Text,
                Margin = new Padding(0)
            };
        }

        // Stil Sava na celu formu, pa vraćanje izgleda kontrola na tamnoj naslovnoj traci
        public static void PrimeniTemu(Control root)
        {
            ThemeHelper.Apply(root);
            ObnoviTamne(root);
        }

        private static void ObnoviTamne(Control c)
        {
            foreach (Control dete in c.Controls)
            {
                if (dete is HRNaslovnaTraka traka)
                    traka.ObnoviIzgled();
                else
                    ObnoviTamne(dete);
            }
        }

        // Boja statusa roka po broju preostalih dana
        public static Color BojaRoka(int danaDo, int danaUpozorenja)
        {
            if (danaDo < 0)
                return StatusIsteklo;
            if (danaDo <= danaUpozorenja)
                return StatusIstice;
            return StatusVazi;
        }

        public static string OpisRoka(int danaDo, int danaUpozorenja)
        {
            if (danaDo < 0)
                return "Isteklo (" + (-danaDo) + " " + Dana(-danaDo) + ")";
            if (danaDo == 0)
                return "Ističe danas";
            if (danaDo <= danaUpozorenja)
                return "Ističe za " + danaDo + " " + Dana(danaDo);
            return "Važi";
        }

        public static string Dana(int n)
        {
            n = Math.Abs(n);
            return (n % 10 == 1 && n % 100 != 11) ? "dan" : "dana";
        }

        public static GraphicsPath Zaobljen(Rectangle r, int radijus)
        {
            var p = new GraphicsPath();
            int d = radijus * 2;
            p.AddArc(r.X, r.Y, d, d, 180, 90);
            p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            p.CloseFigure();
            return p;
        }

        // Dve tabele jedna ispod druge, svaka sa naslovom
        public static Control Podeljeno(string naslov1, Control gore, string naslov2, Control dole, float procenatGore = 55)
        {
            var t = new TableLayoutPanel { ColumnCount = 1, RowCount = 2, BackColor = SavaTheme.Surface };
            t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            t.RowStyles.Add(new RowStyle(SizeType.Percent, procenatGore));
            t.RowStyles.Add(new RowStyle(SizeType.Percent, 100 - procenatGore));
            t.Controls.Add(Sekcija(naslov1, gore), 0, 0);
            t.Controls.Add(Sekcija(naslov2, dole), 0, 1);
            return t;
        }

        public static Control Sekcija(string naslov, Control sadrzaj)
        {
            var p = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 0, 10), BackColor = SavaTheme.Surface };
            sadrzaj.Dock = DockStyle.Fill;
            p.Controls.Add(sadrzaj);
            p.Controls.Add(new HRTekst { Text = naslov, Dock = DockStyle.Top, Height = 28, Font = SavaTheme.ButtonFontSemibold, ForeColor = SavaTheme.Text });
            return p;
        }

        // Otvaranje forme: u glavnom prozoru ako postoji, inače kao samostalan prozor
        public static void Otvori(Control izvor, Form forma)
        {
            if (izvor?.TopLevelControl is NewMain main)
                main.OtvoriFormuBezPrava(() => forma);
            else
                forma.Show();
        }
    }

    // Naslovna traka: tamna (Noćna Sava), naslov levo, sadržaj desno
    public class HRNaslovnaTraka : Panel
    {
        public readonly HRTekst Naslov;
        public readonly FlowLayoutPanel Desno;
        private readonly List<Button> sporedna = new List<Button>();

        public HRNaslovnaTraka(string naslov)
        {
            Dock = DockStyle.Top;
            Height = 52;
            BackColor = SavaTheme.Night;
            Padding = new Padding(20, 0, 16, 0);

            Desno = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 10, 0, 0)
            };
            Naslov = new HRTekst { Text = naslov, Dock = DockStyle.Fill, Font = SavaTheme.HeaderTitleFont, ForeColor = SavaTheme.White };
            Controls.Add(Naslov);
            Controls.Add(Desno);
        }

        // Svetao tekst desno (npr. "142 zaposlena · 138 aktivnih")
        public HRTekst DodajInfo(string tekst)
        {
            var l = new HRTekst { Text = tekst, ForeColor = SavaTheme.MenuText, Height = 32, Margin = new Padding(0, 0, 0, 0) };
            Desno.Controls.Add(l);
            return l;
        }

        // Dugme na tamnoj traci: glavno je plavo, ostala tamna sa svetlim okvirom
        public Button DodajDugme(string tekst, bool glavno, EventHandler klik)
        {
            var b = HRStil.Dugme(tekst, glavno ? "primary" : null);
            b.Margin = new Padding(8, 0, 0, 0);
            if (!glavno)
                sporedna.Add(b);
            b.Click += klik;
            Desno.Controls.Add(b);
            ObnoviIzgled();
            return b;
        }

        public void ObrisiDugmad()
        {
            foreach (Control c in Desno.Controls.Cast<Control>().ToList())
                if (c is Button)
                {
                    Desno.Controls.Remove(c);
                    c.Dispose();
                }
            sporedna.Clear();
        }

        // ThemeHelper.Apply boji sporedna dugmad u belo; na tamnoj traci se vraća tamna varijanta
        public void ObnoviIzgled()
        {
            foreach (Button b in sporedna)
            {
                b.BackColor = SavaTheme.Night;
                b.ForeColor = SavaTheme.White;
                b.FlatAppearance.BorderColor = SavaTheme.Label;
                b.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#1C4569");
            }
        }
    }

    // Tekst koji sam crta (nije Label), pa mu ThemeHelper ne menja boju ni font: naslovi, tekst na tamnoj traci, iznosi
    public class HRTekst : Control
    {
        public bool AutoSirina = true;
        public TextFormatFlags Poravnanje = TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis;

        public HRTekst()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw
                     | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            ForeColor = SavaTheme.Text;
            Font = SavaTheme.FieldFont;
            Height = 24;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            PrilagodiSirinu();
            Invalidate();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            PrilagodiSirinu();
        }

        private void PrilagodiSirinu()
        {
            if (!AutoSirina || Dock == DockStyle.Fill || Dock == DockStyle.Top || Dock == DockStyle.Bottom)
                return;
            Size s = TextRenderer.MeasureText(Text ?? "", Font);
            Width = s.Width + 4;
            Height = Math.Max(Height, s.Height + 4);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var r = new Rectangle(Padding.Left, Padding.Top, Width - Padding.Horizontal, Height - Padding.Vertical);
            TextRenderer.DrawText(e.Graphics, Text, Font, r, ForeColor, Poravnanje);
        }
    }

    // Alatna traka: bela, 52 px, komande levo, glavna akcija desno
    public class HRAlatnaTraka : Panel
    {
        public readonly FlowLayoutPanel Levo;
        public readonly FlowLayoutPanel Desno;

        public HRAlatnaTraka()
        {
            Dock = DockStyle.Top;
            ThemeHelper.Toolbar(this);
            Padding = new Padding(20, 10, 20, 10);
            Desno = new FlowLayoutPanel { Dock = DockStyle.Right, FlowDirection = FlowDirection.RightToLeft, WrapContents = false, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, BackColor = Color.Transparent };
            Levo = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, BackColor = Color.Transparent };
            Controls.Add(Levo);
            Controls.Add(Desno);
        }

        public Button Dugme(string tekst, EventHandler klik, string uloga = null)
        {
            var b = HRStil.Dugme(tekst, uloga);
            b.Click += klik;
            Levo.Controls.Add(b);
            return b;
        }

        public Button DugmeDesno(string tekst, EventHandler klik, string uloga = "primary")
        {
            var b = HRStil.Dugme(tekst, uloga);
            b.Margin = new Padding(8, 0, 0, 0);
            b.Click += klik;
            Desno.Controls.Add(b);
            return b;
        }

        public TextBox Pretraga(string savet, int sirina)
        {
            var t = new HRPretraga(savet) { Width = sirina, Margin = new Padding(0, 2, 12, 0) };
            Levo.Controls.Add(t);
            return t;
        }

        public HRChip Chip(string tekst, bool aktivan, EventHandler klik)
        {
            var c = new HRChip { Text = tekst, Aktivan = aktivan, Margin = new Padding(0, 0, 8, 0) };
            c.Click += klik;
            Levo.Controls.Add(c);
            return c;
        }
    }

    // Polje za pretragu sa sivim savetom kad je prazno
    public class HRPretraga : TextBox
    {
        private readonly string savet;

        public HRPretraga(string savet)
        {
            this.savet = savet;
            BorderStyle = BorderStyle.FixedSingle;
            Font = SavaTheme.FieldFont;
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            const int WM_PAINT = 0x000F;
            if (m.Msg == WM_PAINT && string.IsNullOrEmpty(Text) && !Focused)
            {
                using (var g = CreateGraphics())
                    TextRenderer.DrawText(g, savet, Font, new Point(2, 2), SavaTheme.Label);
            }
        }
    }

    // Filter dugme ("chip"): zaobljeno, aktivno ima svetloplavu pozadinu
    public class HRChip : Control
    {
        private bool aktivan;
        private bool misIznad;
        public Color? BojaTeksta;

        public HRChip()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw
                     | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            Font = SavaTheme.FieldFont;
            Height = SavaTheme.ButtonHeight;
            Cursor = Cursors.Hand;
        }

        public bool Aktivan
        {
            get => aktivan;
            set { aktivan = value; Invalidate(); }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Width = TextRenderer.MeasureText(Text ?? "", Font).Width + 32;
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e) { misIznad = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { misIznad = false; Invalidate(); base.OnMouseLeave(e); }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var r = new Rectangle(0, 0, Width - 1, Height - 1);
            using (var put = HRStil.Zaobljen(r, Height / 2 - 1))
            {
                Color pozadina = aktivan ? SavaTheme.SelectedRow : (misIznad ? SavaTheme.HoverLight : SavaTheme.White);
                using (var b = new SolidBrush(pozadina))
                    e.Graphics.FillPath(b, put);
                using (var o = new Pen(aktivan ? SavaTheme.SelectedRow : SavaTheme.FieldBorder))
                    e.Graphics.DrawPath(o, put);
            }
            Color tekst = BojaTeksta ?? (aktivan ? SavaTheme.Primary : SavaTheme.Text);
            TextRenderer.DrawText(e.Graphics, Text, Font, r, tekst, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }

    // Bedž u zaglavlju (status, mentor, staž, broj upozorenja)
    public class HRBedz : Control
    {
        public Color Pozadina = SavaTheme.White;
        public Color Boja = SavaTheme.Text;

        public HRBedz(string tekst, Color pozadina, Color boja)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint
                     | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            Font = SavaTheme.FieldFont;
            Pozadina = pozadina;
            Boja = boja;
            Height = 28;
            Margin = new Padding(0, 0, 8, 0);
            Text = tekst;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Width = TextRenderer.MeasureText(Text ?? "", Font).Width + 26;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var r = new Rectangle(0, 0, Width - 1, Height - 1);
            using (var put = HRStil.Zaobljen(r, Height / 2 - 1))
            using (var b = new SolidBrush(Pozadina))
                e.Graphics.FillPath(b, put);
            TextRenderer.DrawText(e.Graphics, Text, Font, r, Boja, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }

    // Kartica sa brojem (kontrolna tabla rokova): obojena traka levo, veliki broj, opis
    public class HRBrojKartica : Control
    {
        public Color Traka = SavaTheme.Primary;
        public int Broj;
        public string Opis = "";
        public bool Izabrana;

        public HRBrojKartica()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            BackColor = SavaTheme.White;
            Height = 110;
            Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Izabrana ? SavaTheme.SelectedRow : SavaTheme.White);
            using (var b = new SolidBrush(Traka))
                g.FillRectangle(b, 0, 0, 5, Height);
            ControlPaint.DrawBorder(g, ClientRectangle, SavaTheme.Line, ButtonBorderStyle.Solid);
            TextRenderer.DrawText(g, Broj.ToString(), HRStil.VelikiBroj, new Point(22, 12), Traka == SavaTheme.Danger ? SavaTheme.Danger : SavaTheme.Text);
            TextRenderer.DrawText(g, Opis, SavaTheme.FieldFont, new Rectangle(24, 70, Width - 30, 30), SavaTheme.Label, TextFormatFlags.EndEllipsis);
        }
    }

    // Kartice (tabovi): tekstualni naslovi, aktivni podvučen plavom linijom; sadržaj se menja u panelu ispod
    public class HRKartice : Panel
    {
        private readonly HRTraka traka;
        private readonly Panel sadrzaj;
        private readonly List<Tuple<string, Control>> stranice = new List<Tuple<string, Control>>();
        public event Action<string> Promenjena;
        public int Izabrana { get; private set; } = -1;

        public HRKartice()
        {
            Dock = DockStyle.Fill;
            BackColor = SavaTheme.Surface;
            sadrzaj = new Panel { Dock = DockStyle.Fill, BackColor = SavaTheme.Surface, Padding = new Padding(20, 16, 20, 16) };
            traka = new HRTraka(this) { Dock = DockStyle.Top, Height = 46 };
            Controls.Add(sadrzaj);
            Controls.Add(traka);
        }

        public void Dodaj(string naslov, Control stranica)
        {
            stranica.Dock = DockStyle.Fill;
            stranica.Visible = false;
            sadrzaj.Controls.Add(stranica);
            stranice.Add(Tuple.Create(naslov, stranica));
            traka.Invalidate();
            if (Izabrana < 0)
                Izaberi(0);
        }

        public void Izaberi(int i)
        {
            if (i < 0 || i >= stranice.Count)
                return;
            for (int k = 0; k < stranice.Count; k++)
                stranice[k].Item2.Visible = k == i;
            Izabrana = i;
            traka.Invalidate();
            Promenjena?.Invoke(stranice[i].Item1);
        }

        public void Izaberi(string naslov)
        {
            Izaberi(stranice.FindIndex(t => t.Item1 == naslov));
        }

        private class HRTraka : Control
        {
            private readonly HRKartice vlasnik;
            private readonly List<Rectangle> polja = new List<Rectangle>();

            public HRTraka(HRKartice vlasnik)
            {
                this.vlasnik = vlasnik;
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
                BackColor = SavaTheme.White;
                Cursor = Cursors.Hand;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                var g = e.Graphics;
                g.Clear(SavaTheme.White);
                polja.Clear();
                int x = 20;
                for (int i = 0; i < vlasnik.stranice.Count; i++)
                {
                    bool aktivna = i == vlasnik.Izabrana;
                    Font f = aktivna ? SavaTheme.ButtonFontSemibold : SavaTheme.ButtonFont;
                    string tekst = vlasnik.stranice[i].Item1;
                    int w = TextRenderer.MeasureText(tekst, f).Width;
                    var r = new Rectangle(x, 0, w, Height - 1);
                    polja.Add(new Rectangle(x - 10, 0, w + 20, Height));
                    TextRenderer.DrawText(g, tekst, f, r, aktivna ? SavaTheme.Primary : SavaTheme.Label, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                    if (aktivna)
                        using (var b = new SolidBrush(SavaTheme.Primary))
                            g.FillRectangle(b, x, Height - 3, w, 3);
                    x += w + 28;
                }
                using (var p = new Pen(SavaTheme.Line))
                    g.DrawLine(p, 0, Height - 1, Width, Height - 1);
            }

            protected override void OnMouseClick(MouseEventArgs e)
            {
                base.OnMouseClick(e);
                int i = polja.FindIndex(r => r.Contains(e.Location));
                if (i >= 0)
                    vlasnik.Izaberi(i);
            }
        }
    }

    // Raspored polja: oznaka iznad polja, kolone jednake širine, razmak 24 px između kolona i 14 px između redova
    public class HRPoljaPanel : TableLayoutPanel
    {
        private int kolona;

        public HRPoljaPanel(int brojKolona)
        {
            ColumnCount = brojKolona;
            for (int i = 0; i < brojKolona; i++)
                ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / brojKolona));
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Dock = DockStyle.Top;
            BackColor = Color.Transparent;
            RowCount = 0;
        }

        public T Dodaj<T>(string oznaka, T polje, int raspon = 1) where T : Control
        {
            if (kolona == 0 || kolona + raspon > ColumnCount)
            {
                RowCount++;
                RowStyles.Add(new RowStyle(SizeType.AutoSize));
                kolona = 0;
            }
            var celija = new Panel { Dock = DockStyle.Fill, Height = 62, Margin = new Padding(0, 0, 24, 14), BackColor = Color.Transparent };
            var l = HRStil.Oznaka(oznaka);
            l.Dock = DockStyle.Top;
            polje.Dock = DockStyle.Top;
            if (polje is TextBox tb && tb.Multiline)
            {
                celija.Height = 22 + tb.Height;
            }
            celija.Controls.Add(polje);
            celija.Controls.Add(l);
            Controls.Add(celija, kolona, RowCount - 1);
            if (raspon > 1)
                SetColumnSpan(celija, raspon);
            kolona += raspon;
            if (kolona >= ColumnCount)
                kolona = 0;
            return polje;
        }

        public void NoviRed()
        {
            kolona = 0;
        }
    }
}
