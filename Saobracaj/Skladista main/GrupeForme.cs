using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main
{
    // Grupe polja koje se skrivaju i pokazuju (kao na Prijemnici): kontrole iz dizajnera se premeštaju u grupe
    // jednu ispod druge, a donji deo (tabela) popunjava ostatak prozora. Stanje grupa se pamti po korisniku na računaru.
    internal class GrupeForme
    {
        private class Grupa
        {
            public string Kljuc, Naslov;
            public Panel Okvir, Sadrzaj;
            public Button Traka;
            public bool Otvorena;
        }

        readonly Form forma;
        readonly string fajlStanja;
        readonly Dictionary<string, bool> stanje;
        readonly List<Grupa> grupe = new List<Grupa>();
        public SplitContainer Split { get; private set; }

        public GrupeForme(Form forma, string kljucForme)
        {
            this.forma = forma;
            fajlStanja = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Saobracaj", kljucForme + "Sekcije.txt");
            stanje = UcitajStanje(fajlStanja);

            Split = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                FixedPanel = FixedPanel.Panel1,
                SplitterWidth = 6,
                BackColor = Color.FromArgb(0xD4, 0xDD, 0xE6),
                Width = forma.ClientSize.Width
            };
            Split.Panel1.BackColor = forma.BackColor;
            Split.Panel2.BackColor = forma.BackColor;
            Split.Panel1.AutoScroll = true;
            Split.Panel2MinSize = 150;
        }

        // Sopstvena vidljivost kontrole (Visible vraća false dok forma nije prikazana)
        public static bool Vidljiva(Control c)
        {
            var getState = typeof(Control).GetMethod("GetState", BindingFlags.Instance | BindingFlags.NonPublic);
            return getState == null ? c.Visible : (bool)getState.Invoke(c, new object[] { 2 });
        }

        // Kontrole se premeštaju u novu grupu. uRed: vidljivi paneli se slažu jedan do drugog bez praznina;
        // rastegni: krajnja desna kontrola se širi do desne ivice prozora. Grupa bez vidljivih kontrola se ne prikazuje.
        public void Dodaj(string kljuc, string naslov, bool podrazumevanoOtvorena, bool uRed, bool rastegni, params Control[] kontrole)
        {
            var vidljive = kontrole.Where(Vidljiva).ToList();
            if (vidljive.Count == 0)
                return;

            if (uRed)
            {
                int x = vidljive.Min(c => c.Left), vrh = vidljive.Min(c => c.Top);
                foreach (var c in vidljive.OrderBy(c => c.Left))
                {
                    c.Location = new Point(x, vrh);
                    x = c.Right + 4;
                }
            }

            int levo = vidljive.Min(c => c.Left), gore = vidljive.Min(c => c.Top);
            int dole = vidljive.Max(c => c.Bottom);
            int sirina = Split.Width - 20;

            var g = new Grupa
            {
                Kljuc = kljuc,
                Naslov = naslov,
                Otvorena = stanje.ContainsKey(kljuc) ? stanje[kljuc] : podrazumevanoOtvorena,
                Okvir = new Panel { Dock = DockStyle.Top, Width = sirina },
                Sadrzaj = new Panel { Dock = DockStyle.Top, Width = sirina, Height = dole - gore + 10 },
                Traka = new Button
                {
                    Dock = DockStyle.Top,
                    Height = 26,
                    FlatStyle = FlatStyle.Flat,
                    TextAlign = ContentAlignment.MiddleLeft,
                    BackColor = Color.FromArgb(0xEA, 0xF0, 0xF6),
                    Font = new Font(forma.Font, FontStyle.Bold),
                    Cursor = Cursors.Hand,
                    TabStop = false,
                    Tag = "grupa"   // Sava: ThemeHelper je stilizuje kao traku grupe, ne kao dugme
                }
            };
            g.Traka.FlatAppearance.BorderColor = Color.FromArgb(0xD4, 0xDD, 0xE6);

            foreach (Control c in kontrole)
            {
                var stara = c.Location;
                c.Parent.Controls.Remove(c);
                c.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                c.Location = new Point(stara.X - levo + 8, stara.Y - gore + 5);
                g.Sadrzaj.Controls.Add(c);
            }

            if (rastegni)
            {
                var desna = vidljive.OrderBy(c => c.Right).Last();
                if (desna.Right < sirina - 8)
                    desna.Width = sirina - 8 - desna.Left;
                desna.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            }

            g.Okvir.Controls.Add(g.Sadrzaj);
            g.Okvir.Controls.Add(g.Traka);   // poslednja dodata je na vrhu
            g.Traka.Click += (s, e) => { g.Otvorena = !g.Otvorena; Primeni(g); SacuvajStanje(); PodesiRazdelnik(); };
            Primeni(g);
            grupe.Add(g);
        }

        // donjiDeo: tabela koja popunjava ostatak (null ili sakriven -> grupe zauzimaju ceo prozor);
        // traka: dugmad iznad tabele (može null); iznad: paneli koji ostaju preko rasporeda (izbori, unosi)
        public void Zavrsi(Control donjiDeo, Control traka, params Control[] iznad)
        {
            forma.SuspendLayout();
            forma.Controls.Add(Split);
            Split.BringToFront();

            for (int i = grupe.Count - 1; i >= 0; i--)
                Split.Panel1.Controls.Add(grupe[i].Okvir);
            if (grupe.Count > 0)
                Split.Panel1.AutoScrollMinSize = new Size(grupe.Max(g => g.Sadrzaj.Controls.Cast<Control>().Max(c => c.Right)) + 12, 0);

            if (donjiDeo == null || !Vidljiva(donjiDeo))
                Split.Panel2Collapsed = true;
            else
            {
                donjiDeo.Parent?.Controls.Remove(donjiDeo);
                donjiDeo.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                donjiDeo.Dock = DockStyle.Fill;
                Split.Panel2.Controls.Add(donjiDeo);
                if (traka != null)
                {
                    traka.Dock = DockStyle.Top;
                    Split.Panel2.Controls.Add(traka);   // traka na vrhu, tabela ispod
                }
            }

            foreach (var c in iznad)
                if (c != null) c.BringToFront();
            forma.ResumeLayout(true);

            Split.SizeChanged += (s, e) => PodesiRazdelnik();
            forma.Load += (s, e) => PodesiRazdelnik();
        }

        private static void Primeni(Grupa g)
        {
            g.Traka.Text = (g.Otvorena ? "▼  " : "►  ") + g.Naslov;
            g.Sadrzaj.Visible = g.Otvorena;
            g.Okvir.Height = g.Traka.Height + (g.Otvorena ? g.Sadrzaj.Height : 0);
        }

        // grupe dobijaju koliko im treba, tabela ostatak (najmanje Panel2MinSize)
        private void PodesiRazdelnik()
        {
            try
            {
                if (Split.Panel2Collapsed || !Split.IsHandleCreated)
                    return;
                int potrebno = grupe.Sum(g => g.Okvir.Height) + 2;
                int najvise = Split.Height - Split.Panel2MinSize - Split.SplitterWidth;
                if (najvise < Split.Panel1MinSize)
                    return;
                Split.SplitterDistance = Math.Max(Split.Panel1MinSize, Math.Min(potrebno, najvise));
            }
            catch (InvalidOperationException)
            {
                // prozor još nema konačnu veličinu
            }
        }

        private static Dictionary<string, bool> UcitajStanje(string fajl)
        {
            var rezultat = new Dictionary<string, bool>();
            try
            {
                if (File.Exists(fajl))
                    foreach (string red in File.ReadAllLines(fajl))
                    {
                        var d = red.Split('=');
                        if (d.Length == 2) rezultat[d[0].Trim()] = d[1].Trim() == "1";
                    }
            }
            catch
            {
                // podrazumevano stanje
            }
            return rezultat;
        }

        private void SacuvajStanje()
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fajlStanja));
                File.WriteAllLines(fajlStanja, grupe.Select(g => g.Kljuc + "=" + (g.Otvorena ? "1" : "0")));
            }
            catch
            {
                // stanje grupa nije bitno za rad
            }
        }
    }

    // Izbor kolona Syncfusion tabele: desni klik (ili dugme „Kolone“) daje spisak kolona za štikliranje.
    // Sklonjene kolone se pamte po korisniku na računaru; Primeni se poziva posle svakog punjenja tabele.
    internal static class KoloneGrida
    {
        static readonly ConditionalWeakTable<GridGroupingControl, List<string>> osnovne = new ConditionalWeakTable<GridGroupingControl, List<string>>();

        static string Fajl(string kljuc)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Saobracaj", kljuc + "Kolone.txt");
        }

        public static ContextMenuStrip Ukljuci(GridGroupingControl grid, string kljuc)
        {
            var meni = new ContextMenuStrip();
            meni.Opening += (s, e) =>
            {
                meni.Items.Clear();
                List<string> lista;
                if (!osnovne.TryGetValue(grid, out lista) || lista.Count == 0)
                {
                    e.Cancel = true;
                    return;
                }
                var sklonjene = Ucitaj(kljuc);
                meni.Items.Add(new ToolStripMenuItem("Kolone (štiklirane se prikazuju)") { Enabled = false });
                foreach (string ime in lista)
                {
                    string kolona = ime;
                    var stavka = new ToolStripMenuItem(Naslov(grid, kolona)) { Checked = !sklonjene.Contains(kolona), CheckOnClick = true };
                    stavka.Click += (s2, e2) =>
                    {
                        var skl = Ucitaj(kljuc);
                        if (stavka.Checked) skl.Remove(kolona); else skl.Add(kolona);
                        if (lista.All(skl.Contains)) skl.Remove(kolona);   // bar jedna kolona ostaje
                        Sacuvaj(kljuc, skl);
                        PrimeniNaTabelu(grid, lista, skl);
                    };
                    meni.Items.Add(stavka);
                }
                meni.Items.Add(new ToolStripSeparator());
                var sve = new ToolStripMenuItem("Prikaži sve kolone");
                sve.Click += (s2, e2) => { Sacuvaj(kljuc, new HashSet<string>()); PrimeniNaTabelu(grid, lista, new HashSet<string>()); };
                meni.Items.Add(sve);
            };
            grid.ContextMenuStrip = meni;
            return meni;
        }

        // Posle punjenja tabele: pamti kolone koje kod prikazuje i sklanja one koje je korisnik sklonio
        public static void Primeni(GridGroupingControl grid, string kljuc)
        {
            try
            {
                var lista = new List<string>();
                var vidljive = grid.TableDescriptor.VisibleColumns;
                for (int i = 0; i < vidljive.Count; i++)
                    lista.Add(vidljive[i].Name);
                osnovne.Remove(grid);
                osnovne.Add(grid, lista);
                PrimeniNaTabelu(grid, lista, Ucitaj(kljuc));
            }
            catch
            {
                // izbor kolona nije bitan za rad
            }
        }

        static void PrimeniNaTabelu(GridGroupingControl grid, List<string> lista, HashSet<string> sklonjene)
        {
            var vidljive = grid.TableDescriptor.VisibleColumns;
            vidljive.Clear();
            foreach (string ime in lista)
                if (!sklonjene.Contains(ime))
                    vidljive.Add(ime);
            grid.Refresh();
        }

        static string Naslov(GridGroupingControl grid, string ime)
        {
            var kolone = grid.TableDescriptor.Columns;
            if (kolone.IndexOf(ime) >= 0 && !string.IsNullOrEmpty(kolone[ime].HeaderText))
                return kolone[ime].HeaderText;
            return ime;
        }

        static HashSet<string> Ucitaj(string kljuc)
        {
            try
            {
                if (File.Exists(Fajl(kljuc)))
                    return new HashSet<string>(File.ReadAllLines(Fajl(kljuc)).Select(x => x.Trim()).Where(x => x != ""));
            }
            catch
            {
                // bez zapamćenog izbora
            }
            return new HashSet<string>();
        }

        static void Sacuvaj(string kljuc, HashSet<string> sklonjene)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(Fajl(kljuc)));
                File.WriteAllLines(Fajl(kljuc), sklonjene);
            }
            catch
            {
                // izbor kolona nije bitan za rad
            }
        }
    }
}
