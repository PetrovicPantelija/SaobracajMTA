using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.RNI
{
    // Kolone Slike i Dokumenta u gridu: ikonica i broj fajlova u folderu zapisa (frmTerminalPrivremeniSlike.OsnovniFolder\ID i
    // frmTerminalPrivremeniDokumenta.OsnovniFolder\ID). Nisu kolone tabele TerminalPriv: fajlovi se broje na mrežnom folderu u
    // pozadini posle učitavanja i osvežavanja, pa ikonice stižu malo posle podataka (do tada važi broj iz prethodnog brojanja).
    public partial class frmTerminalPrivremeni
    {
        internal const string KolonaSlike = "Slike";
        internal const string KolonaDokumenta = "Dokumenta";

        private static readonly Color BojaIkone = Color.FromArgb(0x12, 0x30, 0x4D);

        // broj fajlova po ID-u zapisa; zapis bez fajlova nije u rečniku
        private Dictionary<int, int> brojSlika = new Dictionary<int, int>();
        private Dictionary<int, int> brojDokumenata = new Dictionary<int, int>();
        private int brojanjePriloga;   // redni broj brojanja; rezultat starijeg brojanja se odbacuje
        private ImageList ikonePriloga;

        private void PovezIkonePriloga()
        {
            ikonePriloga = new ImageList { ColorDepth = ColorDepth.Depth32Bit, ImageSize = new Size(16, 16) };
            ikonePriloga.Images.Add(NacrtajIkonu(NacrtajFotoaparat));
            ikonePriloga.Images.Add(NacrtajIkonu(NacrtajDokument));
            Disposed += (s, e) => ikonePriloga.Dispose();

            gridGroupingControl1.QueryCellStyleInfo += gridGroupingControl1_QueryCellStyleInfo;
        }

        private static void DodajKolonePriloga(DataTable tabela)
        {
            tabela.Columns.Add(KolonaSlike, typeof(int));
            tabela.Columns.Add(KolonaDokumenta, typeof(int));
        }

        // Tek učitana tabela dobija broj slika i dokumenata iz prethodnog brojanja; redovi ostaju nepromenjeni
        private void PopuniPriloge(DataTable tabela)
        {
            foreach (DataRow red in tabela.Rows)
            {
                int id = Convert.ToInt32(red["ID"]);
                red[KolonaSlike] = BrojIliPrazno(brojSlika, id);
                red[KolonaDokumenta] = BrojIliPrazno(brojDokumenata, id);
            }
            tabela.AcceptChanges();
        }

        private static object BrojIliPrazno(Dictionary<int, int> broj, int id)
        {
            int n;
            return broj.TryGetValue(id, out n) ? (object)n : DBNull.Value;
        }

        // Prebrojava fajlove svih zapisa u gridu u pozadini i upisuje broj u redove koji su se promenili
        private void PrebrojPriloge()
        {
            if (dt == null)
                return;

            var idjevi = new HashSet<int>(dt.Rows.Cast<DataRow>()
                .Where(r => r.RowState != DataRowState.Deleted && r.RowState != DataRowState.Detached)
                .Select(r => Convert.ToInt32(r["ID"]))
                .Where(id => id > 0));
            int redni = ++brojanjePriloga;

            Task.Run(() => new[]
                {
                    Prebroj(frmTerminalPrivremeniSlike.OsnovniFolder, idjevi),
                    Prebroj(frmTerminalPrivremeniDokumenta.OsnovniFolder, idjevi)
                })
                .ContinueWith(t =>
                {
                    if (t.IsFaulted)
                    {
                        _ = t.Exception;   // folder nedostupan u toku brojanja: ostaju prethodni brojevi
                        return;
                    }
                    if (redni != brojanjePriloga || IsDisposed)
                        return;

                    if (t.Result[0] != null)
                        brojSlika = t.Result[0];
                    if (t.Result[1] != null)
                        brojDokumenata = t.Result[1];
                    PrimeniPriloge();
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        // Broj fajlova po folderu zapisa (folder se zove po ID-u); null ako osnovni folder nije dostupan
        private static Dictionary<int, int> Prebroj(string osnovni, HashSet<int> idjevi)
        {
            try
            {
                Saobracaj.Administracija.MrezniFolder.Povezi(osnovni);   // prijava na server iz Podešavanja
            }
            catch (IOException)
            {
                return null;   // poruku prikazuju Zakači slike i Zakači dokumenta
            }

            var koren = new DirectoryInfo(osnovni);
            if (!koren.Exists)
                return null;

            var broj = new Dictionary<int, int>();
            foreach (DirectoryInfo folder in koren.EnumerateDirectories())
            {
                int id;
                if (!int.TryParse(folder.Name, out id) || !idjevi.Contains(id))
                    continue;

                int n = BrojFajlova(folder);
                if (n > 0)
                    broj[id] = n;
            }
            return broj;
        }

        // Fajlovi bez skrivenih i sistemskih (Thumbs.db, desktop.ini), kao pri izvozu na Desktop
        private static int BrojFajlova(DirectoryInfo folder)
        {
            try
            {
                return folder.Exists
                    ? folder.EnumerateFiles().Count(f => (f.Attributes & (FileAttributes.Hidden | FileAttributes.System)) == 0)
                    : 0;
            }
            catch (IOException)
            {
                return 0;
            }
            catch (UnauthorizedAccessException)
            {
                return 0;
            }
        }

        private void PrimeniPriloge()
        {
            if (dt == null)
                return;

            // izmena broja ne menja izabrani zapis, pa se polja ispod grida ne popunjavaju ponovo
            osvezavanjeUToku = true;
            gridGroupingControl1.BeginUpdate();
            try
            {
                foreach (DataRow red in dt.Rows)
                    PostaviPriloge(red);
            }
            finally
            {
                gridGroupingControl1.EndUpdate(true);
                osvezavanjeUToku = false;
            }
        }

        // Posle 'Zakači slike' i 'Zakači dokumenta': ponovo se broje fajlovi samo tog zapisa
        private void OsveziPrilogeZapisa(int id)
        {
            if (dt == null)
                return;

            PrebrojZapis(brojSlika, frmTerminalPrivremeniSlike.OsnovniFolder, id);
            PrebrojZapis(brojDokumenata, frmTerminalPrivremeniDokumenta.OsnovniFolder, id);
            foreach (DataRow red in dt.Select("ID = " + id))
                PostaviPriloge(red);
        }

        private static void PrebrojZapis(Dictionary<int, int> broj, string osnovni, int id)
        {
            int n = BrojFajlova(new DirectoryInfo(Path.Combine(osnovni, id.ToString())));
            if (n > 0)
                broj[id] = n;
            else
                broj.Remove(id);
        }

        // Broj se upisuje u red bez označavanja reda kao izmenjenog (ne čuva se sa 'Sačuvaj izmene'); red sa drugim
        // nesačuvanim izmenama ostaje izmenjen
        private void PostaviPriloge(DataRow red)
        {
            if (red.RowState == DataRowState.Deleted || red.RowState == DataRowState.Detached)
                return;

            int id = Convert.ToInt32(red["ID"]);
            object slike = BrojIliPrazno(brojSlika, id);
            object dokumenta = BrojIliPrazno(brojDokumenata, id);
            if (Equals(red[KolonaSlike], slike) && Equals(red[KolonaDokumenta], dokumenta))
                return;

            if (red.HasVersion(DataRowVersion.Proposed))
                red.EndEdit();
            bool imaDrugihIzmena = red.RowState != DataRowState.Unchanged;

            red[KolonaSlike] = slike;
            red[KolonaDokumenta] = dokumenta;
            if (!imaDrugihIzmena)
                red.AcceptChanges();
        }

        // Kolone Slike i Dokumenta odmah posle KONTEJNER, uske (ikonica i broj)
        private void PodesiKolonePriloga()
        {
            GridTableDescriptor opis = gridGroupingControl1.TableDescriptor;
            GridVisibleColumnDescriptorCollection vidljive = opis.VisibleColumns;
            vidljive.Remove(KolonaSlike);
            vidljive.Remove(KolonaDokumenta);
            int posle = vidljive.IndexOf("KONTEJNER");
            vidljive.Insert(posle + 1, KolonaSlike);
            vidljive.Insert(posle + 2, KolonaDokumenta);

            foreach (string ime in new[] { KolonaSlike, KolonaDokumenta })
            {
                GridColumnDescriptor kolona = opis.Columns[ime];
                kolona.Width = SirinaKolone(gridGroupingControl1.Font, kolona.HeaderText, "0000000");   // ikonica i broj
            }
        }

        private void gridGroupingControl1_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            GridTableCellType tip = e.TableCellIdentity.TableCellType;
            if (tip != GridTableCellType.RecordFieldCell && tip != GridTableCellType.AlternateRecordFieldCell)
                return;
            if (e.TableCellIdentity.Column == null)
                return;

            string ime = e.TableCellIdentity.Column.Name;
            int ikona = ime == KolonaSlike ? 0 : ime == KolonaDokumenta ? 1 : -1;
            if (ikona < 0 || e.Style.CellValue == null || e.Style.CellValue == DBNull.Value)
                return;

            e.Style.ImageList = ikonePriloga;
            e.Style.ImageIndex = ikona;
        }

        private static Bitmap NacrtajIkonu(Action<Graphics> crtaj)
        {
            var slika = new Bitmap(16, 16);
            using (Graphics g = Graphics.FromImage(slika))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                crtaj(g);
            }
            return slika;
        }

        private static void NacrtajFotoaparat(Graphics g)
        {
            using (var cetka = new SolidBrush(BojaIkone))
            using (var telo = new GraphicsPath())
            {
                const float x = 0.5f, y = 4f, s = 15f, v = 10.5f, r = 4f;   // r = prečnik zaobljenja
                telo.AddArc(x, y, r, r, 180, 90);
                telo.AddArc(x + s - r, y, r, r, 270, 90);
                telo.AddArc(x + s - r, y + v - r, r, r, 0, 90);
                telo.AddArc(x, y + v - r, r, r, 90, 90);
                telo.CloseFigure();

                g.FillRectangle(cetka, 5f, 2f, 6f, 3f);         // tražilo
                g.FillPath(cetka, telo);
                g.FillEllipse(Brushes.White, 4.5f, 5.75f, 7f, 7f);   // objektiv
                g.FillEllipse(cetka, 6.25f, 7.5f, 3.5f, 3.5f);
            }
        }

        private static void NacrtajDokument(Graphics g)
        {
            PointF[] list = { new PointF(3f, 1.5f), new PointF(10f, 1.5f), new PointF(13f, 4.5f), new PointF(13f, 14.5f), new PointF(3f, 14.5f) };
            using (var olovka = new Pen(BojaIkone, 1.3f))
            {
                g.FillPolygon(Brushes.White, list);
                g.DrawPolygon(olovka, list);
                g.DrawLines(olovka, new[] { new PointF(10f, 1.5f), new PointF(10f, 4.5f), new PointF(13f, 4.5f) });   // presavijen ugao
                g.DrawLine(olovka, 5.5f, 7.5f, 10.5f, 7.5f);     // redovi teksta
                g.DrawLine(olovka, 5.5f, 10f, 10.5f, 10f);
                g.DrawLine(olovka, 5.5f, 12.5f, 9f, 12.5f);
            }
        }
    }
}
