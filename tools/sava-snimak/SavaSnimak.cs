// Alat za proveru redizajna Sava bez baze: pravi formu (samo InitializeComponent, bez koda konstruktora i Load),
// snima sliku pre i posle PrimeniSava i prijavljuje nova preklapanja, odsečen tekst i kontrole van roditelja.
// Upotreba: SavaSnimak.exe <bin\Debug> <izlazni folder> <Puno.Ime.Tipa[:own|:WxH]> ...
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

static class SavaSnimak
{
    const BindingFlags Svi = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
    static Assembly app;

    [STAThread]
    static int Main(string[] args)
    {
        string bin = args[0], izlaz = args[1];
        Directory.CreateDirectory(izlaz);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.ThreadException += (s, e) => Console.WriteLine("  ThreadException: " + e.Exception.Message);
        AppDomain.CurrentDomain.AssemblyResolve += (s, e) =>
        {
            string p = Path.Combine(bin, new AssemblyName(e.Name).Name + ".dll");
            return File.Exists(p) ? Assembly.LoadFrom(p) : null;
        };
        app = Assembly.LoadFrom(Path.Combine(bin, "Saobracaj.exe"));
        RegistrujLicencu(bin);
        PostaviStatiku("Saobracaj.Sifarnici.frmLogovanje", "Firma", "Leget");

        // prvi prozor procesa dobija početni režim prikaza (može biti minimizovan); troši ga prazan prozor
        using (var prazan = new Form { StartPosition = FormStartPosition.Manual, Location = new Point(-20000, -20000), ShowInTaskbar = false })
        {
            prazan.Show();
            Application.DoEvents();
            prazan.Close();
        }

        var izvestaj = new StringBuilder();
        foreach (string a in args.Skip(2))
        {
            string ime = a, rezim = "1600x860";
            int dv = a.IndexOf(':');
            if (dv > 0) { ime = a.Substring(0, dv); rezim = a.Substring(dv + 1); }
            try { izvestaj.Append(Obradi(ime, rezim, izlaz)); }
            catch (Exception ex)
            {
                var u = ex is TargetInvocationException && ex.InnerException != null ? ex.InnerException : ex;
                izvestaj.AppendLine("## " + ime + "\nGREŠKA: " + u.GetType().Name + ": " + u.Message + "\n" + u.StackTrace + "\n");
            }
        }
        File.WriteAllText(Path.Combine(izlaz, "izvestaj.txt"), izvestaj.ToString(), Encoding.UTF8);
        return 0;
    }

    static void RegistrujLicencu(string bin)
    {
        // ključ se čita iz Program.cs (isti koji aplikacija registruje)
        string program = Path.GetFullPath(Path.Combine(bin, @"..\..\Program.cs"));   // bin\Debug -> Saobracaj\Program.cs
        var m = Regex.Match(File.ReadAllText(program), "RegisterLicense\\(\\s*\"([^\"]+)\"");
        var t = Type.GetType("Syncfusion.Licensing.SyncfusionLicenseProvider, Syncfusion.Licensing");
        if (t == null)
        {
            string p = Path.Combine(bin, "Syncfusion.Licensing.dll");
            if (File.Exists(p)) t = Assembly.LoadFrom(p).GetType("Syncfusion.Licensing.SyncfusionLicenseProvider");
        }
        if (m.Success && t != null)
            t.GetMethod("RegisterLicense", new[] { typeof(string) }).Invoke(null, new object[] { m.Groups[1].Value });
    }

    static void PostaviStatiku(string tip, string polje, object vrednost)
    {
        var t = app.GetType(tip);
        var f = t == null ? null : t.GetField(polje, Svi);
        if (f != null && f.FieldType == vrednost.GetType()) f.SetValue(null, vrednost);
    }

    static Form Napravi(Type t)
    {
        // objekat bez konstruktora forme: pokreće se samo konstruktor prve bazne klase van aplikacije, pa InitializeComponent
        var f = (Form)FormatterServices.GetUninitializedObject(t);
        Type baza = t.BaseType;
        while (baza.Assembly == app) baza = baza.BaseType;
        baza.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null).Invoke(f, null);
        // inicijalizatori polja iz Designer-a (components) i forme koji prave kolekcije: samo jednostavni new T()
        for (Type x = t; x != null && x.Assembly == app; x = x.BaseType)
            foreach (var p in x.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly))
                if (p.GetValue(f) == null && !typeof(Control).IsAssignableFrom(p.FieldType) && !typeof(System.ComponentModel.IComponent).IsAssignableFrom(p.FieldType)
                    && p.FieldType != typeof(string) && !p.FieldType.IsAbstract && !p.FieldType.IsInterface && p.FieldType.IsClass
                    && p.FieldType.GetConstructor(Type.EmptyTypes) != null && p.FieldType.Namespace != null
                    && (p.FieldType.Namespace.StartsWith("System.Collections") || p.FieldType.Namespace == "System.Data"))
                    try { p.SetValue(f, Activator.CreateInstance(p.FieldType)); } catch { }
        t.GetMethod("InitializeComponent", Svi, null, Type.EmptyTypes, null).Invoke(f, null);
        UkloniDogadjaje(f, typeof(Form), "EVENT_LOAD", "EVENT_SHOWN", "EVENT_ACTIVATED", "EVENT_CLOSING", "EVENT_FORMCLOSING");
        return f;
    }

    static void UkloniDogadjaje(System.ComponentModel.Component c, Type tip, params string[] kljucevi)
    {
        var lista = (System.ComponentModel.EventHandlerList)typeof(System.ComponentModel.Component)
            .GetProperty("Events", Svi).GetValue(c, null);
        foreach (string k in kljucevi)
        {
            var f = tip.GetField(k, Svi);
            if (f == null) continue;
            object key = f.GetValue(null);
            var d = lista[key];
            if (d != null) lista.RemoveHandler(key, d);
        }
    }

    static string Obradi(string ime, string rezim, string izlaz)
    {
        var t = app.GetType(ime, true);
        var sb = new StringBuilder("## " + ime + " (" + rezim + ")\n");
        var f = Napravi(t);
        Form domacin = null;
        f.StartPosition = FormStartPosition.Manual;
        f.ShowInTaskbar = false;
        if (rezim == "own")
        {
            f.Location = new Point(-20000, -20000);
            f.Show();
        }
        else
        {
            var wh = rezim.Split('x');
            domacin = new Form
            {
                StartPosition = FormStartPosition.Manual, Location = new Point(-20000, -20000), ShowInTaskbar = false,
                FormBorderStyle = FormBorderStyle.None, ClientSize = new Size(int.Parse(wh[0]), int.Parse(wh[1]))
            };
            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock = DockStyle.Fill;
            domacin.Controls.Add(f);
            domacin.Show();
            f.Show();
        }
        Osvezi(f);
        var pre = Snimi(f);
        Slika(domacin ?? f, Path.Combine(izlaz, Kratko(t) + ".pre.png"));

        var sava = t.GetMethod("PrimeniSava", Svi, null, Type.EmptyTypes, null);
        if (sava == null)
            sb.AppendLine("(nema PrimeniSava)");
        else
        {
            sava.Invoke(f, null);
            Osvezi(f);
            var posle = Snimi(f);
            Slika(domacin ?? f, Path.Combine(izlaz, Kratko(t) + ".posle.png"));
            Uporedi(pre, posle, sb);
            Plavih(f, sb);
        }
        f.Close();
        if (domacin != null) domacin.Close();
        sb.AppendLine();
        return sb.ToString();
    }

    static string Kratko(Type t) { return t.FullName.Replace("Saobracaj.", "").Replace("Skladista_main.", "SM.").Replace("Dokumenta.", "D."); }

    static void Osvezi(Control c)
    {
        for (int i = 0; i < 3; i++) { c.PerformLayout(); Application.DoEvents(); }
    }

    static void Slika(Control c, string putanja)
    {
        using (var b = new Bitmap(c.Width, c.Height))
        {
            c.DrawToBitmap(b, new Rectangle(Point.Empty, c.Size));
            b.Save(putanja, ImageFormat.Png);
        }
    }

    class Stanje
    {
        public Control C; public string Put; public Rectangle R; public bool Vidljiva; public bool Odseceno; public bool Van;
    }

    static bool List(Control c)
    {
        return c is Label || c is TextBox || c is ComboBox || c is ButtonBase || c is DateTimePicker || c is NumericUpDown
            || c is ListBox || c is PictureBox || c.GetType().Name.Contains("TextBoxExt") || c.GetType().Name.Contains("DateTimePickerAdv")
            || c.GetType().Name.Contains("ComboBoxAdv") || c.GetType().Name.Contains("ButtonAdv");
    }

    static Dictionary<Control, Stanje> Snimi(Control root)
    {
        var d = new Dictionary<Control, Stanje>();
        Obidji(root, "", d);
        return d;
    }

    static void Obidji(Control c, string put, Dictionary<Control, Stanje> d)
    {
        foreach (Control x in c.Controls)
        {
            string p = put + "/" + (string.IsNullOrEmpty(x.Name) ? x.GetType().Name : x.Name);
            d[x] = new Stanje
            {
                C = x, Put = p, R = x.Bounds, Vidljiva = x.Visible, Odseceno = Odsecen(x),
                Van = x.Visible && x.Parent != null && !(x.Parent is ScrollableControl && ((ScrollableControl)x.Parent).AutoScroll)
                      && x.Dock == DockStyle.None && (x.Right > x.Parent.ClientSize.Width + 1 || x.Bottom > x.Parent.ClientSize.Height + 1)
            };
            if (!(x is DataGridView) && !x.GetType().Name.Contains("Grid"))
                Obidji(x, p, d);
        }
    }

    static bool Odsecen(Control x)
    {
        if (!x.Visible || string.IsNullOrEmpty(x.Text) || x.Width <= 0) return false;
        if (x is Label && !((Label)x).AutoSize || x is ButtonBase && !(x is CheckBox && ((CheckBox)x).AutoSize) && !(x is RadioButton && ((RadioButton)x).AutoSize))
        {
            int rez = x is CheckBox || x is RadioButton ? 18 : (x is Button ? 8 : 0);
            int w = Math.Max(1, x.Width - rez);
            var flags = TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl;
            var s = TextRenderer.MeasureText(x.Text, x.Font, new Size(w, int.MaxValue), flags);
            int najduzaRec = x.Text.Split(new[] { ' ', '\n', '\r', '/' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(r => TextRenderer.MeasureText(r, x.Font).Width).DefaultIfEmpty(0).Max();
            int visina = x.Height - (x is Button ? 4 : 0);
            return s.Height > visina + 1 || najduzaRec > w + 2;
        }
        return false;
    }

    static void Uporedi(Dictionary<Control, Stanje> pre, Dictionary<Control, Stanje> posle, StringBuilder sb)
    {
        int n = 0;
        foreach (var grupa in posle.Values.Where(s => s.Vidljiva && List(s.C)).GroupBy(s => s.C.Parent))
        {
            var l = grupa.ToList();
            for (int i = 0; i < l.Count; i++)
                for (int j = i + 1; j < l.Count; j++)
                {
                    var a = Rectangle.Inflate(l[i].R, -1, -1);
                    var b = Rectangle.Inflate(l[j].R, -1, -1);
                    var x = Rectangle.Intersect(a, b);
                    if (x.Width <= 0 || x.Height <= 0) continue;
                    Stanje pa, pb;
                    if (pre.TryGetValue(l[i].C, out pa) && pre.TryGetValue(l[j].C, out pb) && pa.Vidljiva && pb.Vidljiva)
                    {
                        var y = Rectangle.Intersect(Rectangle.Inflate(pa.R, -1, -1), Rectangle.Inflate(pb.R, -1, -1));
                        if (y.Width > 0 && y.Height > 0 && y.Width * y.Height >= x.Width * x.Height) continue;
                    }
                    sb.AppendLine(string.Format("PREKLAPANJE: {0} [{1}] i {2} [{3}] ({4}x{5} px)",
                        l[i].Put, Opis(l[i].C), l[j].Put.Substring(l[j].Put.LastIndexOf('/') + 1), Opis(l[j].C), x.Width, x.Height));
                    n++;
                }
        }
        foreach (var s in posle.Values)
        {
            Stanje p;
            pre.TryGetValue(s.C, out p);
            if (s.Odseceno && (p == null || !p.Odseceno))
                sb.AppendLine(string.Format("ODSEČENO: {0} [{1}] {2}x{3}", s.Put, Opis(s.C), s.R.Width, s.R.Height));
            if (s.Van && (p == null || !p.Van))
                sb.AppendLine(string.Format("VAN RODITELJA: {0} [{1}] {2} u {3}", s.Put, Opis(s.C), s.R, s.C.Parent.ClientSize));
        }
    }

    static void Plavih(Control root, StringBuilder sb)
    {
        var plava = Color.FromArgb(0x0A, 0x66, 0xB8);
        var lista = new List<string>();
        Action<Control> rek = null;
        rek = c =>
        {
            foreach (Control x in c.Controls)
            {
                if (x is Button && x.Visible && x.BackColor.ToArgb() == plava.ToArgb()) lista.Add(x.Name + " „" + x.Text + "“");
                rek(x);
            }
        };
        rek(root);
        sb.AppendLine("plava dugmad: " + (lista.Count == 0 ? "nema" : string.Join(", ", lista)));
    }

    static string Opis(Control c)
    {
        string t = (c.Text ?? "").Replace("\r", " ").Replace("\n", " ");
        if (t.Length > 30) t = t.Substring(0, 30) + "…";
        return c.GetType().Name + (t.Length > 0 ? " „" + t + "“" : "");
    }
}
