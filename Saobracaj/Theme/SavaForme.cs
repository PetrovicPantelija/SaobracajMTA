using Syncfusion.WinForms.Controls;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.Theme
{
    // Pomoćne metode za redizajn Sava postojećih formi (docs/SAVA-DESIGN.md), uz ThemeHelper:
    // meniji sa pločicama, trake sa dugmadima, naslovi i kartice. Raspored kontrola ostaje isti.
    public static class SavaForme
    {
        public static readonly Font NaslovMenijaFont = new Font("Segoe UI Semibold", 24f);   // isto kao meniji HR i Održavanje

        // Meni sa pločicama (početni ekran modula u glavnom prozoru): radna površina umesto svetlo plave, naslov kao u
        // menijima HR i Održavanje. Pločice zadržavaju boju i font. Tekst pločice se menja samo kad se ne koristi kao naziv
        // kartice za prava (OtvoriFormuSaPravom(btn.Text, ...)); zato je normalnaSlova izbor forme.
        public static void Plocice(Form forma, bool normalnaSlova)
        {
            forma.BackColor = SavaTheme.Surface;
            foreach (var l in forma.Controls.OfType<Label>().Where(l => l.Font.Size >= 18f))
            {
                l.Font = NaslovMenijaFont;
                l.ForeColor = SavaTheme.Text;
                l.BackColor = Color.Transparent;
            }
            if (normalnaSlova)
                foreach (var b in SveKontrole(forma).OfType<SfButton>())
                    b.Text = ThemeHelper.NormalnaSlova(b.Text);
        }

        // Naslov unutar forme: Segoe UI Semibold 14, boja teksta
        public static void Naslov(Label naslov)
        {
            naslov.Font = SavaTheme.FormTitleFont;
            naslov.ForeColor = SavaTheme.Text;
            naslov.BackColor = Color.Transparent;
        }

        // Traka u jednom redu (SAVA-DESIGN.md, sekcija 3): bela, 52 px, donja linija. Kontrole iz redom stoje u kontejneru
        // (npr. panel2 u panelHeader) sleva nadesno sa razmakom od 8 px, na sredini po visini; dugmad su visoka 32 px i
        // široka po tekstu, liste zadržavaju širinu. Skrivene kontrole se preskaču. Kontrole koje nisu u redom ostaju gde su.
        public static void TrakaURed(Panel traka, Control kontejner, params Control[] redom)
        {
            if (!Postavljena(traka))   // traka sakrivena za ovu firmu (npr. umesto nje meniHeader): ništa se ne pomera
                return;
            int staroDno = traka.Bottom;
            ThemeHelper.Toolbar(traka);
            if (traka.Dock == DockStyle.None && traka.Parent != null)
            {
                traka.SetBounds(0, traka.Top, traka.Parent.ClientSize.Width, traka.Height);
                traka.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            }
            PomeriIspod(traka, staroDno);
            if (kontejner != traka)
            {
                kontejner.BackColor = SavaTheme.White;
                kontejner.Dock = DockStyle.None;
                kontejner.SetBounds(traka.Padding.Left, 0, kontejner.Width, traka.Height - 1);
            }
            int x = kontejner == traka ? traka.Padding.Left : 0;
            foreach (var c in redom)
            {
                if (c == null || !Postavljena(c))
                    continue;
                if (c.Parent != kontejner)
                    kontejner.Controls.Add(c);
                c.Dock = DockStyle.None;
                c.Margin = new Padding(0);
                if (c is Button b)
                {
                    ThemeHelper.SirinaPoTekstu(b);
                    b.Height = SavaTheme.ButtonHeight;
                }
                c.Location = new Point(x, (kontejner.Height - c.Height) / 2);
                x += c.Width + 8;
            }
            if (kontejner != traka)
                kontejner.Width = System.Math.Max(x, 1);
        }

        // Red dugmadi (npr. ispod polja forme): sleva od x, visina 32 px, širina po tekstu u jednom redu, razmak 8 px.
        // Skrivena dugmad i null se preskaču. Vraća desnu ivicu poslednjeg dugmeta.
        public static int RedDugmadi(int x, int y, params Button[] redom)
        {
            int desno = x;
            foreach (var b in redom)
            {
                if (b == null || !Postavljena(b))
                    continue;
                int sirina = System.Math.Max(TextRenderer.MeasureText(b.Text, b.Font).Width + 28, 72);
                b.SetBounds(x, y, sirina, SavaTheme.ButtonHeight);
                desno = b.Right;
                x = b.Right + 8;
            }
            return desno;
        }

        // Oznake fiksne veličine (AutoSize = false) iz starih formi su visoke 13 px, a Segoe UI 9 traži 15: tekst se seče.
        // Oznaka u jednom redu, bez okvira i bez poravnanja udesno, dobija AutoSize (gornji levi ugao ostaje isti).
        public static void OznakeAutoSize(Control root)
        {
            foreach (var l in SveKontrole(root).OfType<Label>())
            {
                if (l.AutoSize || l.BorderStyle != BorderStyle.None || l.Dock != DockStyle.None || string.IsNullOrEmpty(l.Text)
                    || l.Text.Contains("\n") || l.TextAlign != ContentAlignment.TopLeft)
                    continue;
                var tekst = TextRenderer.MeasureText(l.Text, l.Font);
                if (l.Height < tekst.Height || l.Width < tekst.Width)
                    l.AutoSize = true;
            }
        }

        // Dugmad kojima posle Segoe UI tekst ne staje: visina 32 px (ili bar koliko tekst traži) i širina po tekstu,
        // ali samo ako je mesto slobodno (ne preklapa drugu vidljivu kontrolu istog roditelja i ostaje u roditelju).
        // Pozicija (gornji levi ugao) se ne menja. Dugme koje nema mesta ostaje kakvo je.
        public static void DugmadPoTekstu(Control root)
        {
            foreach (var b in SveKontrole(root).OfType<Button>().ToList())
            {
                if (!Postavljena(b) || b.Dock != DockStyle.None || b.Parent == null || (b.Tag as string) == "grupa"
                    || string.IsNullOrEmpty(b.Text) || b.BackgroundImage != null || b.Image != null)
                    continue;
                var tekst = TextRenderer.MeasureText(b.Text, b.Font);
                int sirina = System.Math.Max(b.Width, tekst.Width + 24);
                int najmanje = tekst.Height + 10;
                foreach (int visina in new[] { SavaTheme.ButtonHeight, najmanje })
                {
                    int h = System.Math.Max(b.Height, visina);
                    var r = new Rectangle(b.Left, b.Top, sirina, h);
                    if (r.Size == b.Size)
                        break;
                    bool slobodno = r.Right <= b.Parent.ClientSize.Width && r.Bottom <= b.Parent.ClientSize.Height
                        && !b.Parent.Controls.Cast<Control>().Any(c => c != b && Postavljena(c)
                            && c.Bounds.IntersectsWith(r) && !c.Bounds.IntersectsWith(b.Bounds));
                    if (slobodno)
                    {
                        b.Bounds = r;
                        break;
                    }
                }
            }
        }

        // Traka je promenila visinu: kontrole ispod nje (nedokovane, u istom roditelju) pomeraju se za istu razliku,
        // a one usidrene i za donju ivicu menjaju visinu, pa razmak između trake i sadržaja ostaje kakav je bio.
        public static void PomeriIspod(Control traka, int staroDno)
        {
            int pomak = traka.Bottom - staroDno;
            if (pomak == 0 || traka.Parent == null)
                return;
            // traka na samoj formi: forma raste za istu razliku (prvo forma, pa kontrole usidrene i dole zadržavaju visinu);
            // forma u glavnom prozoru je dokovana (Fill), pa joj visina iz konstruktora ne smeta
            if (traka.Parent is Form forma && pomak > 0)
                forma.ClientSize = new Size(forma.ClientSize.Width, forma.ClientSize.Height + pomak);
            foreach (Control c in traka.Parent.Controls)
            {
                if (c == traka || c.Dock != DockStyle.None || c.Top < staroDno - 4)
                    continue;
                if ((c.Anchor & AnchorStyles.Bottom) != 0 && (c.Anchor & AnchorStyles.Top) != 0)
                    c.SetBounds(c.Left, c.Top + pomak, c.Width, System.Math.Max(20, c.Height - pomak));
                else
                    c.Top += pomak;
            }
        }

        // Širina liste po najdužoj stavci (lista u traci ne treba da bude šira nego što tekst zahteva)
        public static void SirinaListe(ComboBox lista, int najmanje = 120)
        {
            int w = najmanje;
            foreach (var s in lista.Items)
                w = System.Math.Max(w, TextRenderer.MeasureText(lista.GetItemText(s), lista.Font).Width + SystemInformation.VerticalScrollBarWidth + 12);
            lista.Width = w;
        }

        // Sopstvena vidljivost kontrole: Visible vraća false dok forma nije prikazana (npr. u konstruktoru)
        static readonly System.Reflection.MethodInfo getState =
            typeof(Control).GetMethod("GetState", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

        public static bool Postavljena(Control c)
        {
            return getState == null ? c.Visible : (bool)getState.Invoke(c, new object[] { 2 });
        }

        // Paneli sa poljima postaju bele kartice sa okvirom boje Linija (umesto šarenih pozadina)
        public static void Kartice(params Panel[] paneli)
        {
            foreach (var p in paneli)
                if (p != null)
                    ThemeHelper.Kartica(p);
        }

        // Tekst dugmadi napisan VELIKIM SLOVIMA dobija obična slova („SNIMI“ -> „Snimi“), kao oznake
        public static void DugmadNormalnimSlovima(Control root, params Control[] izuzeci)
        {
            foreach (var b in SveKontrole(root).OfType<ButtonBase>())
                if (!izuzeci.Contains(b) && !(b is CheckBox) && !(b is RadioButton))
                    b.Text = ThemeHelper.NormalnaSlova(b.Text);
        }

        // Stavke trake (ToolStrip, MenuStrip) napisane VELIKIM SLOVIMA dobijaju obična slova
        public static void StavkeNormalnimSlovima(ToolStrip traka)
        {
            foreach (ToolStripItem s in traka.Items)
            {
                s.Text = ThemeHelper.NormalnaSlova(s.Text);
                if (s is ToolStripDropDownItem dd)
                    foreach (ToolStripItem d in dd.DropDownItems)
                        d.Text = ThemeHelper.NormalnaSlova(d.Text);
            }
        }

        // Natpis dugmeta ili stavke trake: ista akcija ima isti naziv u celoj aplikaciji („Refresh“ -> „Osveži“).
        // Menja se samo kad kod ne poredi taj tekst (proverava se pre poziva, u formi).
        public static string Natpis(string tekst)
        {
            switch ((tekst ?? "").Trim().ToLowerInvariant())
            {
                case "refresh":
                case "osvezi":
                    return "Osveži";
                case "sacuvaj":
                    return "Sačuvaj";
                default:
                    return tekst;
            }
        }

        public static IEnumerable<Control> SveKontrole(Control root)
        {
            foreach (Control c in root.Controls)
            {
                yield return c;
                foreach (var d in SveKontrole(c))
                    yield return d;
            }
        }
    }
}
