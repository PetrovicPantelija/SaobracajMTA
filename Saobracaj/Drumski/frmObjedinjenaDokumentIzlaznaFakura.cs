using PdfSharp.Drawing;
using PdfSharp.Pdf.IO;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using PdfSharp.Pdf;
using System.Windows.Forms;

namespace Saobracaj.Drumski
{
    public partial class frmObjedinjenaDokumentIzlaznaFakura: Form
    {
        public string IzlaznaFaktura;
        public frmObjedinjenaDokumentIzlaznaFakura(string izlaznaFaktura)
        {
            InitializeComponent();
            IzlaznaFaktura = izlaznaFaktura;
            ChangeTextBox();
            RefreshDataGrid1();
            this.Text = "Objedinjena dokumentacija po izlaznoj fakturi " + IzlaznaFaktura;
        }

        private void ChangeTextBox()
        {
            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;

                this.BackColor = Color.White;
                this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
                this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
                this.ControlBox = true;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                Office2010Colors.ApplyManagedColors(this, Color.White);
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = Color.White;
                Office2010Colors.ApplyManagedColors(this, Color.White);

                //foreach (Control control in groupBox1.Controls)
                //{
                //    if (control is System.Windows.Forms.Button buttons)
                //    {

                //        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                //        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                //        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                //        buttons.FlatStyle = FlatStyle.Flat;
                //    }

                //    if (control is System.Windows.Forms.TextBox textBox)
                //    {

                //        textBox.BackColor = Color.White;// Example: Change background color
                //        textBox.ForeColor = Color.FromArgb(51, 51, 54); //Boja slova u kvadratu
                //        textBox.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                //        // Example: Change font
                //    }


                //    if (control is System.Windows.Forms.Label label)
                //    {
                //        // Change properties here
                //        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                //        label.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);  // Example: Change font

                //        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                //    }

                //    if (control is DateTimePicker dtp)
                //    {
                //        dtp.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                //        dtp.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                //    }

                //    if (control is System.Windows.Forms.CheckBox chk)
                //    {
                //        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                //        chk.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                //    }

                //    if (control is System.Windows.Forms.ListBox lb)
                //    {
                //        lb.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                //        lb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                //    }

                //    if (control is System.Windows.Forms.ComboBox cb)
                //    {
                //        cb.ForeColor = Color.FromArgb(51, 51, 54);
                //        cb.BackColor = Color.White;// Example: Change background color
                //        cb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                //    }

                //    if (control is System.Windows.Forms.NumericUpDown nu)
                //    {
                //        nu.ForeColor = Color.FromArgb(51, 51, 54);
                //        nu.BackColor = Color.White;// Example: Change background color
                //        nu.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                //    }
                //}
            }
            else
            {

                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }



            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                foreach (System.Windows.Forms.Control control in Controls)
                {
                    if (control is System.Windows.Forms.Button buttons)
                    {

                        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                        buttons.FlatStyle = FlatStyle.Flat;
                    }

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
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
            }
        }


        private void RefreshDataGrid1()
        {
            string s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection conn = new SqlConnection(s_connection);
            List<string> statusi = new List<string>();
            using (conn)
            {
                conn.Open();

                var select = @"
                  SELECT 
                        CASE WHEN d.tip = 1 THEN 'Prevoznica'
                             WHEN d.tip = 2 THEN 'Faktura'
                             WHEN d.tip = 3 THEN 'Skenirana prevoznica'
                             WHEN d.tip = 4 THEN 'Skenirana faktura' END AS TipDokumenta,
                        CONVERT(varchar, d.DatumDodavanja,104) AS DatumUpisa,
                        LTRIM(RTRIM(k.DeIme)) + ' ' + LTRIM(RTRIM(k.DePriimek)) AS Skenirao,
                        rn.RadniNalogDrumskiID,  
                        p.PaNaziv AS Prevoznik, 
                        a.RegBr AS RegistarskiBroj, 
                        a.Vozac AS PodaciVozaca,
                        d.Putanja
                   FROM FakturaDrumski rn 
                        INNER JOIN FakturaDrumskiStavka f on f.FaktureDrumskogID = rn.ID
                        INNER JOIN RadniNalogDrumski rnd on rn.RadniNalogDrumskiID = rnd.ID
                        INNER JOIN Automobili a ON a.ID = rnd.KamionID  and a.VoziloDrumskog = 1
                        INNER JOIN Partnerji p on  a.PartnerID = p.PaSifra
                        INNER JOIN DokumentaFaktureDrumski d ON d.FakturaDrumskiID = rn.RadniNalogDrumskiID
                        INNER JOIN Delavci k ON d.Dodao = k.DeSifra
                  WHERE f.IzlaznaFaktura like @IzlaznaFaktura
                  UNION 
                  SELECT  'TS'  AS TipDokumenta,
                        CONVERT(varchar, d.DatumDodavanja,104) AS DatumUpisa,
                        LTRIM(RTRIM(k.DeIme)) + ' ' + LTRIM(RTRIM(k.DePriimek)) AS Skenirao,
                        rn.RadniNalogDrumskiID,  
                        p.PaNaziv AS Prevoznik, 
                        a.RegBr AS RegistarskiBroj, 
                        a.Vozac AS PodaciVozaca,
                        d.Putanja
                  FROM FakturaDrumski rn 
                        INNER JOIN FakturaDrumskiStavka f on f.FaktureDrumskogID = rn.ID
                        INNER JOIN RadniNalogDrumski rnd on rn.RadniNalogDrumskiID = rnd.ID
	                    INNER JOIN Automobili a ON a.ID = rnd.KamionID  and a.VoziloDrumskog = 1
                        INNER JOIN Partnerji p on  a.PartnerID = p.PaSifra
                        INNER JOIN DokumentaRadnogNalogaDrumski d ON d.RadniNalogDrumskiID = rn.RadniNalogDrumskiID
                        INNER JOIN Delavci k ON d.DodaoKorisnik = k.DeSifra
                  WHERE f.IzlaznaFaktura like @IzlaznaFaktura

                  UNION 
                  SELECT  'Vozac sken'  AS TipDokumenta,
                        CONVERT(varchar, d.UploadedAt,104) AS DatumUpisa,
                        a.Vozac AS Skenirao,
                        rn.RadniNalogDrumskiID,  
                        p.PaNaziv AS Prevoznik, 
                        a.RegBr AS RegistarskiBroj, 
                        a.Vozac AS PodaciVozaca,
                        d.FilePath as Putanja
                  FROM FakturaDrumski rn 
                        INNER JOIN FakturaDrumskiStavka f on f.FaktureDrumskogID = rn.ID
                        INNER JOIN RadniNalogDrumski rnd on rn.RadniNalogDrumskiID = rnd.ID
	                    INNER JOIN Automobili a ON a.ID = rnd.KamionID  and a.VoziloDrumskog = 1
                        INNER JOIN Partnerji p on  a.PartnerID = p.PaSifra
                        INNER JOIN UploadedFiles d ON d.RadniNalogDrumskiID = rn.RadniNalogDrumskiID AND UploadedByVozac = 1
                  WHERE f.IzlaznaFaktura like @IzlaznaFaktura

                  UNION 
                  SELECT 'Kamion sken'  AS TipDokumenta,
                        CONVERT(varchar, d.UploadedAt,104) AS DatumUpisa,
                        LTRIM(RTRIM(k.DeIme)) + ' ' + LTRIM(RTRIM(k.DePriimek)) AS Skenirao,
                        rn.RadniNalogDrumskiID,  
                        p.PaNaziv AS Prevoznik, 
                        a.RegBr AS RegistarskiBroj, 
                        a.Vozac AS PodaciVozaca,
                        d.FilePath as Putanja
                   FROM FakturaDrumski rn 
                        INNER JOIN FakturaDrumskiStavka f on f.FaktureDrumskogID = rn.ID
                        INNER JOIN RadniNalogDrumski rnd on rn.RadniNalogDrumskiID = rnd.ID
	                    INNER JOIN Automobili a ON a.ID = rnd.KamionID  and a.VoziloDrumskog = 1
                        INNER JOIN Partnerji p on  a.PartnerID = p.PaSifra
                        INNER JOIN UploadedFiles d ON d.RadniNalogDrumskiID = rn.RadniNalogDrumskiID AND UploadedByVozac = 0
	                    INNER JOIN Delavci k ON d.UploadedBy = k.DeSifra
                   WHERE f.IzlaznaFaktura like @IzlaznaFaktura";

                // Bind baze
                SqlDataAdapter da = new SqlDataAdapter(select, conn);
                da.SelectCommand.Parameters.AddWithValue("@IzlaznaFaktura", IzlaznaFaktura);

                var ds = new System.Data.DataSet();
                da.Fill(ds);
                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = ds.Tables[0];
            }

            PodesiDatagridView(dataGridView1);

            dataGridView1.RowHeadersWidth = 30;

            if (dataGridView1.Columns.Contains("Putanja"))
            {
                dataGridView1.Columns["Putanja"].Visible = false;
            }

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {

                var grid = dataGridView1;
                var kolona = grid.Columns[e.ColumnIndex].Name;
                var ins = new InsertFakture();

                if (kolona == "Brisanje")
                {

                    if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                    {
                        var row = dataGridView1.Rows[e.RowIndex];

                        // Čitanje vrednosti iz reda
                        int dokumentID = row.Cells["DokumentID"].Value == null ? 0 : Convert.ToInt32(row.Cells["DokumentID"].Value);
                        string putanja = row.Cells["Putanja"].Value?.ToString() ?? "";

                        if (File.Exists(putanja))
                        {
                            File.Delete(putanja);
                            ins.DelDokument(dokumentID);
                            MessageBox.Show("Dokument je uspešno obrisan.");
                            RefreshDataGrid1();
                        }
                        else
                        {
                            MessageBox.Show("Dokument ne postoji na zadatoj lokaciji.");
                        }
                    }
                }

                if (kolona == "Preuzimanje")
                {
                    if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                    {
                        var row = dataGridView1.Rows[e.RowIndex];
                        {
                            string putanja = row.Cells["Putanja"].Value?.ToString() ?? "";

                            if (!string.IsNullOrEmpty(putanja) && File.Exists(putanja))
                            {
                                using (SaveFileDialog sfd = new SaveFileDialog())
                                {
                                    sfd.FileName = Path.GetFileName(putanja);
                                    sfd.Filter = "Svi fajlovi|*.*";

                                    if (sfd.ShowDialog() == DialogResult.OK)
                                    {
                                        try
                                        {
                                            File.Copy(putanja, sfd.FileName, true);
                                            MessageBox.Show("Fajl je uspešno preuzet.");
                                        }
                                        catch (IOException ex)
                                        {
                                            MessageBox.Show("Greška pri preuzimanju fajla: " + ex.Message);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Fajl ne postoji na serveru.");
                            }
                        }
                    }
                }
            }
        }

        private void PodesiDatagridView(DataGridView dgv)
        {

            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(90, 199, 249); // Selektovana boja
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.BackgroundColor = Color.White;

            dgv.DefaultCellStyle.Font = new System.Drawing.Font("Helvetica", 12F, GraphicsUnit.Pixel);
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(51, 51, 54);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);
            dgv.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);


            //Header
            dgv.EnableHeadersVisualStyles = false;
            //   header.Style.Font = new Font("Arial", 12F, FontStyle.Bold);
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 54);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv.ColumnHeadersHeight = 30;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PDF fajl|*.pdf";
                sfd.FileName = "Kombinovani.pdf";
                if (sfd.ShowDialog() != DialogResult.OK) return;

                PdfDocument outputDoc = new PdfDocument();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;

                    string filePath = row.Cells["Putanja"].Value?.ToString();
                    if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                        continue;

                    string ext = Path.GetExtension(filePath).ToLower();

                    if (ext == ".pdf")
                    {
                        PdfDocument inputDoc = PdfReader.Open(filePath, PdfDocumentOpenMode.Import);
                        for (int i = 0; i < inputDoc.PageCount; i++)
                            outputDoc.AddPage(inputDoc.Pages[i]);
                    }
                    else if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".bmp")
                    {
                        PdfPage page = outputDoc.AddPage();
                        XGraphics gfx = XGraphics.FromPdfPage(page);
                        XImage img = XImage.FromFile(filePath);

                        // Izračunavanje dimenzija slike u PDF jedinicama (72 dpi)
                        double imgWidth = img.PixelWidth * 72 / img.HorizontalResolution;
                        double imgHeight = img.PixelHeight * 72 / img.VerticalResolution;

                        // Definisanje margina (npr. 20 poena sa svih strana)
                        double margin = 20;
                        double maxWidth = page.Width - 2 * margin;
                        double maxHeight = page.Height - 2 * margin;

                        // Skaliranje slike da stane unutar margina
                        double scale = Math.Min(maxWidth / imgWidth, maxHeight / imgHeight);

                        double drawWidth = imgWidth * scale;
                        double drawHeight = imgHeight * scale;

                        // Centriranje slike na strani
                        double x = (page.Width - drawWidth) / 2;
                        double y = (page.Height - drawHeight) / 2;

                        gfx.DrawImage(img, x, y, drawWidth, drawHeight);
                    }
                }

                outputDoc.Save(sfd.FileName);
                MessageBox.Show("PDF kreiran!");
            }
        }
    }
}
