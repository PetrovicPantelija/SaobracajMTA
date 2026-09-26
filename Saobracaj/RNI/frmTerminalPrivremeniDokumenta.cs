using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.RNI
{
    // Dokumenta zapisa TerminalPriv: folder <OsnovniFolder>\<ID zapisa>. Dokumenta se dodaju drag & drop-om
    // (ili dugmetom), a spisak dokumenata iz foldera se prikazuje u dataGridView2.
    public partial class frmTerminalPrivremeniDokumenta : Form
    {
        public static string OsnovniFolder = @"\\192.168.150.110\Leget\TerminalDokumenta\";

        private readonly int id;
        private int verzijaUcitavanja;

        // Broj dokumenata dodatih u ovoj sesiji
        public int Dodato { get; private set; }

        public frmTerminalPrivremeniDokumenta(int id, string kontejner)
        {
            this.id = id;
            InitializeComponent();

            Text = "Dokumenta - ID " + id + (string.IsNullOrEmpty(kontejner) ? "" : " (" + kontejner + ")");
            Load += (s, e) => UcitajDokumenta();
            FormClosed += (s, e) => verzijaUcitavanja++;
        }

        private string FolderZapisa
        {
            get { return Path.Combine(OsnovniFolder, id.ToString()); }
        }

        // ---------------------------------------------------------------------------------
        // Spisak dokumenata iz foldera
        // ---------------------------------------------------------------------------------
        private async void UcitajDokumenta()
        {
            int verzija = ++verzijaUcitavanja;
            string folder = FolderZapisa;

            lblStatus.Text = "Učitavanje dokumenata...";

            DataTable tabela;
            try
            {
                tabela = await Task.Run(() => NapraviSpisak(folder));
            }
            catch (Exception ex)
            {
                if (!IsDisposed && verzija == verzijaUcitavanja)
                    lblStatus.Text = "Folder nije dostupan: " + ex.Message;
                return;
            }

            if (IsDisposed || verzija != verzijaUcitavanja)
                return;

            dataGridView2.DataSource = tabela;
            dataGridView2.Columns["Putanja"].Visible = false;
            dataGridView2.Columns["Naziv"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns["Naziv"].FillWeight = 300;
            dataGridView2.Columns["Tip"].Width = 80;
            dataGridView2.Columns["Veličina (KB)"].Width = 110;
            dataGridView2.Columns["Veličina (KB)"].DefaultCellStyle.Format = "N0";
            dataGridView2.Columns["Veličina (KB)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView2.Columns["Datum izmene"].Width = 140;
            dataGridView2.Columns["Datum izmene"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";

            lblStatus.Text = Directory.Exists(folder)
                ? "Folder: " + folder + "   |   Dokumenata: " + tabela.Rows.Count
                : "Folder " + folder + " ne postoji, kreira se pri dodavanju prvog dokumenta.";
        }

        private static DataTable NapraviSpisak(string folder)
        {
            var tabela = new DataTable();
            tabela.Columns.Add("Naziv", typeof(string));
            tabela.Columns.Add("Tip", typeof(string));
            tabela.Columns.Add("Veličina (KB)", typeof(double));
            tabela.Columns.Add("Datum izmene", typeof(DateTime));
            tabela.Columns.Add("Putanja", typeof(string));

            if (!Directory.Exists(folder))
                return tabela;

            foreach (FileInfo fajl in new DirectoryInfo(folder).GetFiles().OrderBy(f => f.Name, StringComparer.OrdinalIgnoreCase))
            {
                tabela.Rows.Add(fajl.Name, fajl.Extension.TrimStart('.').ToUpperInvariant(), Math.Ceiling(fajl.Length / 1024.0),
                    fajl.LastWriteTime, fajl.FullName);
            }
            return tabela;
        }

        private string PutanjaIzabranog()
        {
            if (dataGridView2.CurrentRow == null || dataGridView2.Columns["Putanja"] == null)
                return null;
            return dataGridView2.CurrentRow.Cells["Putanja"].Value as string;
        }

        private void OtvoriDokument(string putanja)
        {
            try
            {
                Process.Start(putanja);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Dokumenta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            string putanja = dataGridView2.Rows[e.RowIndex].Cells["Putanja"].Value as string;
            if (putanja != null)
                OtvoriDokument(putanja);
        }

        private void btnOtvoriDokument_Click(object sender, EventArgs e)
        {
            string putanja = PutanjaIzabranog();
            if (putanja == null)
            {
                MessageBox.Show("Izaberite dokument u spisku.", "Dokumenta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            OtvoriDokument(putanja);
        }

        private void btnOsvezi_Click(object sender, EventArgs e)
        {
            UcitajDokumenta();
        }

        private void btnOtvoriFolder_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(FolderZapisa))
            {
                MessageBox.Show("Folder još ne postoji. Kreira se pri dodavanju prvog dokumenta.", "Dokumenta",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Process.Start("explorer.exe", FolderZapisa);
        }

        // ---------------------------------------------------------------------------------
        // Dodavanje dokumenata (drag & drop ili dugme)
        // ---------------------------------------------------------------------------------
        private void Dokumenta_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void Dokumenta_DragDrop(object sender, DragEventArgs e)
        {
            string[] putanje = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (putanje != null)
                DodajFajlove(putanje);
        }

        private void btnDodajDokumenta_Click(object sender, EventArgs e)
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
            // Ispušteni folderi se razvijaju u fajlove koje sadrže (bez podfoldera)
            var fajlovi = new List<string>();
            foreach (string p in putanje)
            {
                if (Directory.Exists(p))
                    fajlovi.AddRange(Directory.GetFiles(p));
                else if (File.Exists(p))
                    fajlovi.Add(p);
            }

            if (fajlovi.Count == 0)
            {
                MessageBox.Show("Nema dokumenata za dodavanje.", "Dokumenta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string folder = FolderZapisa;
            var greske = new List<string>();
            int kopirano = 0;

            UseWaitCursor = true;
            lblStatus.Text = "Kopiranje dokumenata...";
            try
            {
                await Task.Run(() =>
                {
                    Directory.CreateDirectory(folder);   // folder se zove po ID-u zapisa
                    foreach (string izvor in fajlovi)
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

            Dodato += kopirano;
            UcitajDokumenta();

            if (greske.Count > 0)
            {
                MessageBox.Show("Dodato dokumenata: " + kopirano + Environment.NewLine + string.Join(Environment.NewLine, greske.Take(10)),
                    "Dokumenta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
