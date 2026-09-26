using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.RNI
{
    // Slike zapisa TerminalPriv: folder <OsnovniFolder>\<ID zapisa>. Slike se dodaju drag & drop-om
    // (ili dugmetom), a broj slika u folderu se upisuje u polje POSLATE SLIKE.
    public partial class frmTerminalPrivremeniSlike : Form
    {
        public static string OsnovniFolder = @"\\192.168.150.110\Leget\TerminalSlike\";

        private static readonly string[] Ekstenzije = { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tif", ".tiff" };
        private const int VelicinaMiniature = 120;

        private readonly int id;
        private int verzijaUcitavanja;

        // Broj slika u folderu posle poslednjeg dodavanja i da li je polje POSLATE SLIKE promenjeno u bazi
        public int BrojSlika { get; private set; }
        public bool Promenjeno { get; private set; }

        public frmTerminalPrivremeniSlike(int id, string kontejner)
        {
            this.id = id;
            InitializeComponent();

            Text = "Slike - ID " + id + (string.IsNullOrEmpty(kontejner) ? "" : " (" + kontejner + ")");
            Load += (s, e) => UcitajSlike();
            FormClosed += (s, e) =>
            {
                verzijaUcitavanja++;
                if (pictureBox1.Image != null)
                    pictureBox1.Image.Dispose();
            };
        }

        private string FolderZapisa
        {
            get { return Path.Combine(OsnovniFolder, id.ToString()); }
        }

        private static bool JeSlika(string putanja)
        {
            return Ekstenzije.Contains(Path.GetExtension(putanja).ToLowerInvariant());
        }

        // ---------------------------------------------------------------------------------
        // Pregled slika iz foldera
        // ---------------------------------------------------------------------------------
        private async void UcitajSlike()
        {
            int verzija = ++verzijaUcitavanja;
            string folder = FolderZapisa;

            listView1.Items.Clear();
            imageList1.Images.Clear();
            PrikaziSliku(null);
            lblStatus.Text = "Učitavanje slika...";

            List<KeyValuePair<string, Bitmap>> miniature;
            try
            {
                miniature = await Task.Run(() => UcitajMiniature(folder));
            }
            catch (Exception ex)
            {
                if (!IsDisposed && verzija == verzijaUcitavanja)
                    lblStatus.Text = "Folder nije dostupan: " + ex.Message;
                return;
            }

            if (IsDisposed || verzija != verzijaUcitavanja)
            {
                foreach (var m in miniature)
                    m.Value.Dispose();
                return;
            }

            foreach (var m in miniature)
            {
                imageList1.Images.Add(m.Value);
                listView1.Items.Add(new ListViewItem(Path.GetFileName(m.Key), imageList1.Images.Count - 1) { Tag = m.Key });
                m.Value.Dispose();
            }

            lblStatus.Text = Directory.Exists(folder)
                ? "Folder: " + folder + "   |   Slika: " + miniature.Count
                : "Folder " + folder + " ne postoji, kreira se pri dodavanju prve slike.";
        }

        private static List<KeyValuePair<string, Bitmap>> UcitajMiniature(string folder)
        {
            var rezultat = new List<KeyValuePair<string, Bitmap>>();
            if (!Directory.Exists(folder))
                return rezultat;

            foreach (string putanja in Directory.GetFiles(folder).Where(JeSlika).OrderBy(p => p, StringComparer.OrdinalIgnoreCase))
                rezultat.Add(new KeyValuePair<string, Bitmap>(putanja, NapraviMiniaturu(putanja)));

            return rezultat;
        }

        // Miniatura VelicinaMiniature x VelicinaMiniature sa očuvanim odnosom stranica
        private static Bitmap NapraviMiniaturu(string putanja)
        {
            var miniatura = new Bitmap(VelicinaMiniature, VelicinaMiniature);
            using (Graphics g = Graphics.FromImage(miniatura))
            {
                g.Clear(Color.White);
                try
                {
                    using (var ms = new MemoryStream(File.ReadAllBytes(putanja)))
                    using (Image slika = Image.FromStream(ms))
                    {
                        double razmera = Math.Min((double)VelicinaMiniature / slika.Width, (double)VelicinaMiniature / slika.Height);
                        int sirina = Math.Max(1, (int)(slika.Width * razmera));
                        int visina = Math.Max(1, (int)(slika.Height * razmera));
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.DrawImage(slika, (VelicinaMiniature - sirina) / 2, (VelicinaMiniature - visina) / 2, sirina, visina);
                    }
                }
                catch (Exception)
                {
                    // slika se ne može pročitati: prazna miniatura sa oznakom
                    g.Clear(Color.LightGray);
                    g.DrawString("?", SystemFonts.DefaultFont, Brushes.Black, 5, 5);
                }
            }
            return miniatura;
        }

        private void PrikaziSliku(string putanja)
        {
            Image stara = pictureBox1.Image;
            pictureBox1.Image = null;
            if (stara != null)
                stara.Dispose();

            if (putanja == null)
                return;

            try
            {
                using (var ms = new MemoryStream(File.ReadAllBytes(putanja)))
                using (Image slika = Image.FromStream(ms))
                {
                    pictureBox1.Image = new Bitmap(slika);
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Slika se ne može prikazati: " + ex.Message;
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
                PrikaziSliku((string)listView1.SelectedItems[0].Tag);
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;

            try
            {
                Process.Start((string)listView1.SelectedItems[0].Tag);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Slike", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOsvezi_Click(object sender, EventArgs e)
        {
            UcitajSlike();
        }

        private void btnOtvoriFolder_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(FolderZapisa))
            {
                MessageBox.Show("Folder još ne postoji. Kreira se pri dodavanju prve slike.", "Slike",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Process.Start("explorer.exe", FolderZapisa);
        }

        // ---------------------------------------------------------------------------------
        // Dodavanje slika (drag & drop ili dugme)
        // ---------------------------------------------------------------------------------
        private void Slike_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void Slike_DragDrop(object sender, DragEventArgs e)
        {
            string[] putanje = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (putanje != null)
                DodajFajlove(putanje);
        }

        private void btnDodajSlike_Click(object sender, EventArgs e)
        {
            if (ofdDialog.ShowDialog(this) == DialogResult.OK)
                DodajFajlove(ofdDialog.FileNames);
        }

        // Slobodna putanja u folderu: ako fajl postoji dodaje se " (2)", " (3)"...
        private static string JedinstvenaPutanja(string folder, string imeFajla)
        {
            string putanja = Path.Combine(folder, imeFajla);
            string ime = Path.GetFileNameWithoutExtension(imeFajla);
            string ekstenzija = Path.GetExtension(imeFajla);
            for (int i = 2; File.Exists(putanja); i++)
                putanja = Path.Combine(folder, ime + " (" + i + ")" + ekstenzija);
            return putanja;
        }

        private async void DodajFajlove(IEnumerable<string> putanje)
        {
            // Ispušteni folderi se razvijaju u slike koje sadrže (bez podfoldera)
            var svi = new List<string>();
            foreach (string p in putanje)
            {
                if (Directory.Exists(p))
                    svi.AddRange(Directory.GetFiles(p));
                else
                    svi.Add(p);
            }

            List<string> slike = svi.Where(JeSlika).ToList();
            int preskoceno = svi.Count - slike.Count;
            if (slike.Count == 0)
            {
                MessageBox.Show("Nema slika za dodavanje (podržano: " + string.Join(", ", Ekstenzije) + ").", "Slike",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string folder = FolderZapisa;
            var greske = new List<string>();
            int kopirano = 0;

            UseWaitCursor = true;
            lblStatus.Text = "Kopiranje slika...";
            try
            {
                await Task.Run(() =>
                {
                    Directory.CreateDirectory(folder);   // folder se zove po ID-u zapisa
                    foreach (string izvor in slike)
                    {
                        try
                        {
                            File.Copy(izvor, JedinstvenaPutanja(folder, Path.GetFileName(izvor)));
                            kopirano++;
                        }
                        catch (Exception ex)
                        {
                            greske.Add(Path.GetFileName(izvor) + ": " + ex.Message);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                greske.Add("Folder " + folder + ": " + ex.Message);
            }
            finally
            {
                UseWaitCursor = false;
            }

            if (IsDisposed)
                return;

            // Broj slika u folderu se upisuje u POSLATE SLIKE
            if (kopirano > 0)
            {
                try
                {
                    int broj = Directory.GetFiles(folder).Count(JeSlika);
                    new insertTerminalPriv().UpdTerminalPrivPoslateSlike(id, broj);
                    BrojSlika = broj;
                    Promenjeno = true;
                }
                catch (Exception ex)
                {
                    greske.Add("Broj slika nije upisan u bazu: " + ex.Message);
                }
            }

            UcitajSlike();

            if (greske.Count > 0 || preskoceno > 0)
            {
                string poruka = "Dodato slika: " + kopirano;
                if (preskoceno > 0)
                    poruka += Environment.NewLine + "Preskočeno fajlova koji nisu slike: " + preskoceno;
                if (greske.Count > 0)
                    poruka += Environment.NewLine + string.Join(Environment.NewLine, greske.Take(10));
                MessageBox.Show(poruka, "Slike", MessageBoxButtons.OK, greske.Count > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
            }
        }
    }
}
