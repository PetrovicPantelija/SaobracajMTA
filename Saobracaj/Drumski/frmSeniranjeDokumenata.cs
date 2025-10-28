using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WIA; 
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Data.SqlClient;
using Microsoft.VisualBasic;

namespace Saobracaj.Drumski
{
    public partial class frmSeniranjeDokumenata: Form
    {
        // scanned pages in memory
        private TrackBar tbRotate;
        private Button btnReset;
        private Button btnRotate90;
        private List<Image> scannedPages = new List<Image>();          // trenutne radne kopije (rotirane, editovane)
        private List<Image> originalImages = new List<Image>();         // original skeniranih strana
        private bool editMode = false;
        private int radniNalogID;

        public frmSeniranjeDokumenata(int id)
        {
            InitializeComponent();
            ChangeTextBox();
            radniNalogID = id;

            // Panel za TrackBar i dugmad
            Panel bottomPanel = new Panel();
            bottomPanel.Dock = DockStyle.Bottom;
            bottomPanel.Height = 60;
            bottomPanel.Padding = new Padding(10);

            // TrackBar za rotaciju
            tbRotate = new TrackBar();
            tbRotate.Minimum = -180;
            tbRotate.Maximum = 180;
            tbRotate.TickFrequency = 5;
            tbRotate.Value = 0;
            tbRotate.Dock = DockStyle.Left;
            tbRotate.Width = 200;
            tbRotate.Visible = false;
            tbRotate.Scroll += TbRotate_Scroll;
            bottomPanel.Controls.Add(tbRotate);

            // Dugme Reset
            btnReset = new Button();
            btnReset.Text = "Resetuj";
            btnReset.Visible = false;
            btnReset.Left = tbRotate.Right + 10;
            btnReset.Height = 40;
            btnReset.Click += BtnReset_Click;
            bottomPanel.Controls.Add(btnReset);

            // Dugme Rotiraj 90°
            btnRotate90 = new Button();
            btnRotate90.Text = "Rotiraj 90°";
            btnRotate90.Visible = false;
            btnRotate90.Left = btnReset.Right + 10;
            btnRotate90.Height = 40;
            btnRotate90.Click += (s, e) =>
            {
                if (lbPages.SelectedIndex < 0) return;
                int idx = lbPages.SelectedIndex;

                pbPreview.Image?.Dispose();
                pbPreview.Image = RotateImage(scannedPages[idx], 90);

                // ažuriraj radnu kopiju
                scannedPages[idx]?.Dispose();
                scannedPages[idx] = (Image)pbPreview.Image.Clone();

                tbRotate.Value = 0; // resetuje TrackBar da dalje rotira ručno
            };
            bottomPanel.Controls.Add(btnRotate90);

            rightPanel.Controls.Add(bottomPanel);
            bottomPanel.BringToFront();
            pbPreview.BringToFront();
        }
        private void ChangeTextBox()
        {
            this.BackColor = Color.White;
            this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
            this.commandBarController1.Office2010Theme = Syncfusion.Windows.Forms.Office2010Theme.Managed;
            Syncfusion.Windows.Forms.Office2010Colors.ApplyManagedColors(this, Color.White);
            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
         //   meniHeader.Visible = false;
            this.ControlBox = true;
            // this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
              //  meniHeader.Visible = true;
               // meniHeader.Visible = false;
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = Color.White;
                Syncfusion.Windows.Forms.Office2010Colors.ApplyManagedColors(this, Color.White);

                //foreach (Control control in groupBox1.Controls)
                //{
                //    if (control is System.Windows.Forms.Button buttons)
                //    {

                //        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                //        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                //        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                //        buttons.FlatStyle = FlatStyle.Flat;
                //    }
                //}


                foreach (System.Windows.Forms.Control control in this.Controls)
                {

                    if (control is System.Windows.Forms.TextBox textBox)
                    {

                        textBox.BackColor = Color.White;// Example: Change background color
                        textBox.ForeColor = Color.FromArgb(51, 51, 54); //Boja slova u kvadratu
                        textBox.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                        // Example: Change font
                    }


                    if (control is System.Windows.Forms.Label label)
                    {
                        // Change properties here
                        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        label.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);  // Example: Change font

                        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                    }

                    if (control is DateTimePicker dtp)
                    {
                        dtp.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        dtp.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.CheckBox chk)
                    {
                        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        chk.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ListBox lb)
                    {
                        lb.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        lb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ComboBox cb)
                    {
                        cb.ForeColor = Color.FromArgb(51, 51, 54);
                        cb.BackColor = Color.White;// Example: Change background color
                        cb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.NumericUpDown nu)
                    {
                        nu.ForeColor = Color.FromArgb(51, 51, 54);
                        nu.BackColor = Color.White;// Example: Change background color
                        nu.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }
                }
            }
            else
            {
             //   meniHeader.Visible = false;
             //   meniHeader.Visible = true;
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

    
        private void BtnRefreshScanners_Click(object sender, EventArgs e)
        {
            PopulateScanners();
        }


        private void PopulateScanners()
        {
            cmbScanners.Items.Clear();
            try
            {
                DeviceManager manager = new DeviceManager();
                for (int i = 1; i <= manager.DeviceInfos.Count; i++)
                {
                    var info = manager.DeviceInfos[i];
                    if (info.Type == WiaDeviceType.ScannerDeviceType)
                    {
                        var name = info.Properties["Name"].get_Value()?.ToString() ?? ("Scanner " + i);
                        cmbScanners.Items.Add(name);
                    }
                }


                if (cmbScanners.Items.Count > 0)
                    cmbScanners.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri dohvatu skenera: " + ex.Message);
            }
        }
        private void BtnReset_Click(object sender, EventArgs e)
        {
            if (lbPages.SelectedIndex < 0) return;

            int idx = lbPages.SelectedIndex;
            pbPreview.Image?.Dispose();
            pbPreview.Image = (Image)originalImages[idx].Clone();

            tbRotate.Value = 0;

            // Resetujemo radnu kopiju na original
            scannedPages[idx]?.Dispose();
            scannedPages[idx] = (Image)originalImages[idx].Clone();
        }

        private void BtnScan_Click(object sender, EventArgs e)
        {
            if (cmbScanners.SelectedItem == null)
            {
                MessageBox.Show("Izaberite skener.");
                return;
            }

            var scannerName = cmbScanners.SelectedItem.ToString();
            try
            {
                var image = ScanSinglePage(scannerName);
                if (image != null)
                {
                    scannedPages.Add((Image)image.Clone());
                    originalImages.Add((Image)image.Clone());

                    UpdatePagesList();
                    lbPages.SelectedIndex = scannedPages.Count - 1;
                    LoadScannedImage(image);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri skeniranju: " + ex.Message);
            }
        }
        private Image ScanSinglePage(string scannerName)
        {
            DeviceManager manager = new DeviceManager();
            DeviceInfo found = null;
            for (int i = 1; i <= manager.DeviceInfos.Count; i++)
            {
                var info = manager.DeviceInfos[i];
                if (info.Type == WiaDeviceType.ScannerDeviceType)
                {
                    var name = info.Properties["Name"].get_Value()?.ToString();
                    if (string.Equals(name, scannerName, StringComparison.OrdinalIgnoreCase))
                    {
                        found = info;
                        break;
                    }
                }
            }


            if (found == null) return null;


            var device = found.Connect();
            var item = device.Items[1];


            // Set common scanning properties (optional)
            try
            {
                // Example: set color and DPI
                SetWiaProperty(item.Properties, "6146", 1); // Color intent: 1 = Color
                SetWiaProperty(item.Properties, "6147", 300); // Horizontal DPI
                SetWiaProperty(item.Properties, "6148", 300); // Vertical DPI
            }
            catch { /* ignore if properties unavailable */ }


            var imgFile = (ImageFile)item.Transfer(FormatID.wiaFormatJPEG);


            var temp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".jpg");
            imgFile.SaveFile(temp);


            // Load as System.Drawing.Image
            var img = Image.FromFile(temp);


            // Delete temp file after cloning to memory to avoid locked file
            var cloned = new Bitmap(img);
            img.Dispose();
            try { File.Delete(temp); } catch { }


            return cloned;
        }

        // Helper to set WIA property safely
        private void SetWiaProperty(IProperties properties, string propIdOrName, object value)
        {
            try
            {
                Property p = null;
                // try by id
                int id;
                if (int.TryParse(propIdOrName, out id))
                {
                    p = properties.get_Item(id);
                }
                else
                {
                    p = properties.get_Item(propIdOrName);
                }


                if (p != null)
                {
                    p.set_Value(value);
                }
            }
            catch { }
        }


        private void UpdatePagesList()
        {
            lbPages.Items.Clear();
            for (int i = 0; i < scannedPages.Count; i++)
            {
                lbPages.Items.Add("Strana " + (i + 1));
            }
        }

        private void LbPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbPages.SelectedIndex >= 0 && lbPages.SelectedIndex < scannedPages.Count)
            {
                LoadScannedImage(scannedPages[lbPages.SelectedIndex]);
                tbRotate.Value = 0;
            }
            else
            {
                pbPreview.Image?.Dispose();
                pbPreview.Image = null;
            }
        }


        private void ShowSelectedPage()
        {
            if (lbPages.SelectedIndex >= 0 && lbPages.SelectedIndex < scannedPages.Count)
            {
                pbPreview.Image = scannedPages[lbPages.SelectedIndex];
            }
            else
            {
                pbPreview.Image = null;
            }
        }


        private void BtnRotate_Click(object sender, EventArgs e)
        {
            var idx = lbPages.SelectedIndex;
            if (idx < 0) return;
            var img = scannedPages[idx];
            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            // replace with rotated copy to avoid issues
            var copy = new Bitmap(img);
            img.Dispose();
            scannedPages[idx] = copy;
            ShowSelectedPage();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int idx = lbPages.SelectedIndex;
            if (idx >= 0 && idx < scannedPages.Count)
            {
                scannedPages[idx]?.Dispose();
                scannedPages.RemoveAt(idx);
                originalImages[idx]?.Dispose();
                originalImages.RemoveAt(idx);

                UpdatePagesList();

                if (scannedPages.Count > 0)
                {
                    int newIndex = Math.Min(idx, scannedPages.Count - 1);
                    lbPages.SelectedIndex = newIndex;
                }
                else
                {
                    pbPreview.Image?.Dispose();
                    pbPreview.Image = null;
                }
            }
        }

        private int PostaviVrednostZaposleni()
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            int ulogovaniZaposleniID = 0;

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT  k.DeSifra as ID, (RTrim(DeIme) + ' ' + Rtrim(DePriimek)) as Zaposleni " +
                                            "FROM Korisnici k " +
                                            "INNER JOIN Delavci d ON k.DeSifra = d.DeSifra " +
                                            "WHERE Trim(Korisnik) like '" + Saobracaj.Sifarnici.frmLogovanje.user.Trim() + "'", con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                if (dr["ID"] != DBNull.Value)
                    ulogovaniZaposleniID = Convert.ToInt32(dr["ID"].ToString());
            }
            return ulogovaniZaposleniID;

        }

        private string GenerisiNazivFajlaZaKontejner()
        {
            string brojKontejnera = "";

            // 1. Dohvati brojKontejnera iz tabele RadniNalogDrumski 
            using (var con = new SqlConnection(Saobracaj.Sifarnici.frmLogovanje.connectionString))
            using (var cmd = new SqlCommand(@"
            SELECT TOP 1 brojKontejnera 
            FROM RadniNalogDrumski 
            WHERE ID = @id", con))
            {
                cmd.Parameters.AddWithValue("@id", radniNalogID);
                con.Open();
                var result = cmd.ExecuteScalar();
                if (result != null)
                    brojKontejnera = result.ToString();
                else
                    brojKontejnera = "Dokument"; // fallback ako nije definisano
            }

            // 2. Proveri koji je poslednji broj dokumenta za taj kontejner
            int maxBroj = 0;
            using (var con = new SqlConnection(Saobracaj.Sifarnici.frmLogovanje.connectionString))
            using (var cmd = new SqlCommand(@"
                    SELECT MAX(CAST(SUBSTRING(
                    NazivDokumenta,
                    LEN(NazivDokumenta) - CHARINDEX('_', REVERSE(NazivDokumenta)) + 2,
                    CHARINDEX('.', NazivDokumenta) - (LEN(NazivDokumenta) - CHARINDEX('_', REVERSE(NazivDokumenta)) + 2)) AS INT))
                    FROM DokumentaFaktureDrumski
                    WHERE FakturaDrumskiID = @id AND NazivDokumenta LIKE @pattern", con))
            {
                cmd.Parameters.AddWithValue("@id", radniNalogID);
                cmd.Parameters.AddWithValue("@pattern", brojKontejnera + "_%.pdf");
                con.Open();
                var result = cmd.ExecuteScalar();
                if (result != DBNull.Value && result != null)
                    maxBroj = Convert.ToInt32(result);
            }

            int noviBroj = maxBroj + 1;

            return $"{brojKontejnera}_{noviBroj}.pdf";
        }

        private void BtnSaveTransport_Click(object sender, EventArgs e)
        {
            if (scannedPages.Count == 0)
            {
                MessageBox.Show("Nema skeniranih strana za snimanje.");
                return;
            }

            // 1. Generiši naziv fajla
            string nazivFajla = GenerisiNazivFajlaZaKontejner();
            string destinacijaFolder = $@"\\192.168.99.10\Leget\Drumski\Dokumenta\SkeniranaPrevoznica\ID_{radniNalogID}";
           

            try
            {
                if (!Directory.Exists(destinacijaFolder))
                {
                    Directory.CreateDirectory(destinacijaFolder);
                }
                string destinacija = Path.Combine(destinacijaFolder, nazivFajla);

                // 2. Kreiraj PDF
                using (var doc = new PdfSharp.Pdf.PdfDocument())
                {
                    foreach (var img in scannedPages)
                    {
                        var page = doc.AddPage();
                        page.Width = PdfSharp.Drawing.XUnit.FromPoint(img.Width * 72.0 / img.HorizontalResolution);
                        page.Height = PdfSharp.Drawing.XUnit.FromPoint(img.Height * 72.0 / img.VerticalResolution);

                        using (var gfx = PdfSharp.Drawing.XGraphics.FromPdfPage(page))
                        {
                            using (var xImg = PdfSharp.Drawing.XImage.FromGdiPlusImage(img))
                            {
                                gfx.DrawImage(xImg, 0, 0, page.Width, page.Height);
                            }
                        }
                    }

                    // 3. Snimi PDF
                    doc.Save(destinacija);
                }

                MessageBox.Show($"Dokument uspešno sačuvan: {nazivFajla}");

                // 4. Snimi u bazu
                int zaposleniID = PostaviVrednostZaposleni();
                InsertFakture ins = new InsertFakture();
                ins.SnimiUFajlBazu(
                    radniNalogID,
                    null,
                    null,
                    zaposleniID,
                    destinacija,
                    nazivFajla,
                    3
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri snimanju PDF-a: " + ex.Message);
            }
        }

        private void BtnSaveFaktura_Click(object sender, EventArgs e)
        {
            if (scannedPages.Count == 0)
            {
                MessageBox.Show("Nema skeniranih strana za snimanje.");
                return;
            }

            // 1. Generiši naziv fajla
            string nazivFajla = GenerisiNazivFajlaZaKontejner();
            string destinacijaFolder = $@"\\192.168.99.10\Leget\Drumski\Dokumenta\SkeniranaFaktura\ID_{radniNalogID}";


            try
            {
                if (!Directory.Exists(destinacijaFolder))
                {
                    Directory.CreateDirectory(destinacijaFolder);
                }
                string destinacija = Path.Combine(destinacijaFolder, nazivFajla);

                // 2. Kreiraj PDF
                using (var doc = new PdfSharp.Pdf.PdfDocument())
                {
                    foreach (var img in scannedPages)
                    {
                        var page = doc.AddPage();
                        page.Width = PdfSharp.Drawing.XUnit.FromPoint(img.Width * 72.0 / img.HorizontalResolution);
                        page.Height = PdfSharp.Drawing.XUnit.FromPoint(img.Height * 72.0 / img.VerticalResolution);

                        using (var gfx = PdfSharp.Drawing.XGraphics.FromPdfPage(page))
                        {
                            using (var xImg = PdfSharp.Drawing.XImage.FromGdiPlusImage(img))
                            {
                                gfx.DrawImage(xImg, 0, 0, page.Width, page.Height);
                            }
                        }
                    }

                    // 3. Snimi PDF
                    doc.Save(destinacija);
                }

                MessageBox.Show($"Dokument uspešno sačuvan: {nazivFajla}");

                // 4. Snimi u bazu
                int zaposleniID = PostaviVrednostZaposleni();
                InsertFakture ins = new InsertFakture();
                ins.SnimiUFajlBazu(
                    radniNalogID,
                    null,
                    null,
                    zaposleniID,
                    destinacija,
                    nazivFajla,
                    4
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri snimanju PDF-a: " + ex.Message);
            }
        }


        private void frmSeniranjeDokumenata_Load(object sender, EventArgs e)
        {
            PopulateScanners();
        }

        private void btnEditImage_Click(object sender, EventArgs e)
        {
            if (lbPages.SelectedIndex < 0) return;

            editMode = !editMode;

            tbRotate.Visible = editMode;
            btnReset.Visible = editMode;
            btnRotate90.Visible = editMode;

            btnEditImage.Text = editMode ? "Snimi izmenu" : "Počni izmenu";
        }


        private void LoadScannedImage(Image img)
        {

            pbPreview.Image?.Dispose();
            pbPreview.Image = (Image)img.Clone();
        }


        private void TbRotate_Scroll(object sender, EventArgs e)
        {
            if (!editMode || lbPages.SelectedIndex < 0) return;

            int idx = lbPages.SelectedIndex;

            pbPreview.Image?.Dispose();
            pbPreview.Image = RotateImage(originalImages[idx], tbRotate.Value);

            // Snimamo rotiranu verziju u scannedPages
            if (idx < scannedPages.Count)
            {
                scannedPages[idx]?.Dispose();
                scannedPages[idx] = (Image)pbPreview.Image.Clone();
            }

        }

        // helper za rotaciju
        private Bitmap RotateImage(Image img, float angle)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            bmp.SetResolution(img.HorizontalResolution, img.VerticalResolution);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.TranslateTransform((float)img.Width / 2, (float)img.Height / 2);
                g.RotateTransform(angle);
                g.TranslateTransform(-(float)img.Width / 2, -(float)img.Height / 2);
                g.DrawImage(img, new Point(0, 0));
            }

            return bmp;
        }
    }
}
