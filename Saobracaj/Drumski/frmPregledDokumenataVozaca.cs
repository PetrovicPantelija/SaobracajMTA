using Saobracaj.Testiranje;
using Syncfusion.Windows.Forms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Saobracaj.Drumski
{
    public partial class frmPregledDokumenataVozaca: Form
    {

        public int NalogID;
        public int RadniNalogID;

        public frmPregledDokumenataVozaca(int radniNalogId, int nalogId)
        {
            InitializeComponent();
            RadniNalogID = radniNalogId;
            NalogID = nalogId;
            this.Text = "Dokumenti za vozača: " + RadniNalogID.ToString();
            ChangeTextBox();
            RefreshData();
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

        //private void RefreshData()
        //{
        //    string s_connection = Sifarnici.frmLogovanje.connectionString;

        //    using (SqlConnection conn = new SqlConnection(s_connection))
        //    {
        //        conn.Open();

        //        // --- Zaglavlje ---
        //        string queryHeader = @"
        //            SELECT TOP 1 
        //                    rn.ID AS BrojNaloga,
        //                    a.Vozac,
        //                    a.RegBr,
        //                    a.BrojTelefona,
        //                    p.PaNaziv AS Prevoznik
        //            FROM RadniNalogDrumski rn
        //            INNER JOIN Automobili a ON rn.KamionID = a.ID
        //            INNER JOIN Partnerji p ON a.PartnerID = p.PaSifra AND p.DrumskiPrevoz = 1
        //            WHERE rn.ID = @RadniNalogID";

        //        SqlCommand cmd = new SqlCommand(queryHeader, conn);
        //        cmd.Parameters.AddWithValue("@RadniNalogID", RadniNalogID);

        //        SqlDataReader reader = cmd.ExecuteReader();
        //        if (reader.Read())
        //        {
        //            lblBrojNaloga.Text = reader["BrojNaloga"].ToString();
        //            lblVozac.Text = reader["Vozac"].ToString();
        //            lblTelefon.Text = reader["BrojTelefona"].ToString();
        //            lblPrevoznik.Text = reader["Prevoznik"].ToString();
        //        }
        //        reader.Close();

        //        // --- Dokumenta ---
        //        string queryDocs = @"
        //            SELECT u.ID AS DokumentID,
        //                   u.FileName,
        //                   u.FilePath,
        //                   u.UploadedAT AS DatumDodavanja,
        //                   '' as Status
        //            FROM UploadedFiles u
        //            WHERE u.NalogID = @NalogID";

        //        SqlDataAdapter daDocs = new SqlDataAdapter(queryDocs, conn);
        //        daDocs.SelectCommand.Parameters.AddWithValue("@NalogID", NalogID);

        //        DataTable dtDocs = new DataTable();
        //        daDocs.Fill(dtDocs);

        //        // Dodaj kolonu za slike
        //        dtDocs.Columns.Add("Slika", typeof(Image));

        //        foreach (DataRow row in dtDocs.Rows)
        //        {
        //            string putanja = row["FilePath"].ToString();
        //            if (File.Exists(putanja))
        //            {
        //                using (Image img = Image.FromFile(putanja))
        //                {
        //                    row["Slika"] = new Bitmap(img, new Size(180, 140)); // thumbnail
        //                }
        //            }
        //        }

        //        dataGridView1.DataSource = dtDocs;

        //        // Sakrij interne kolone
        //        dataGridView1.Columns["FilePath"].Visible = false;
        //        dataGridView1.Columns["DokumentID"].Visible = false;

        //        // Kolona slike
        //        if (dataGridView1.Columns["Slika"] is DataGridViewImageColumn imgCol)
        //        {
        //            imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
        //            imgCol.Width = 200;
        //        }

        //        // Dodaj link kolone za status
        //        if (dataGridView1.Columns["Dobar"] == null)
        //        {
        //            DataGridViewLinkColumn linkGood = new DataGridViewLinkColumn();
        //            linkGood.Name = "Dobar";
        //            linkGood.HeaderText = "Akcija";
        //            linkGood.Text = "Dobar";
        //            linkGood.UseColumnTextForLinkValue = true;
        //            dataGridView1.Columns.Add(linkGood);
        //        }

        //        if (dataGridView1.Columns["Los"] == null)
        //        {
        //            DataGridViewLinkColumn linkBad = new DataGridViewLinkColumn();
        //            linkBad.Name = "Los";
        //            linkBad.HeaderText = "";
        //            linkBad.Text = "Loš";
        //            linkBad.UseColumnTextForLinkValue = true;
        //            dataGridView1.Columns.Add(linkBad);
        //        }

        //        // Dodaj dugme "Obriši"
        //        if (dataGridView1.Columns["Obrisi"] == null)
        //        {
        //            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
        //            btnDelete.Name = "Obrisi";
        //            btnDelete.HeaderText = "Obriši";
        //            btnDelete.Text = "Obriši";
        //            btnDelete.UseColumnTextForButtonValue = true;
        //            btnDelete.Width = 80;
        //            dataGridView1.Columns.Add(btnDelete);
        //        }

        //        // Formatiranje
        //        dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
        //        dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        //        dataGridView1.RowTemplate.Height = 150;
        //        dataGridView1.ReadOnly = true;
        //    }
        //}
        private void RefreshData()
        {
            string s_connection = Sifarnici.frmLogovanje.connectionString;

            using (SqlConnection conn = new SqlConnection(s_connection))
            {
                conn.Open();

                // --- Zaglavlje ---
                string queryHeader = @"
            SELECT TOP 1 
                rn.ID AS BrojNaloga,
                a.Vozac,
                a.RegBr,
                a.BrojTelefona,
                p.PaNaziv AS Prevoznik,
                REPLACE(CONVERT(varchar, rn.InstrukcijePoslateDatum, 104) + ' ' + CONVERT(varchar, rn.InstrukcijePoslateDatum, 108), ':', '.') AS porukaPoslata
            FROM RadniNalogDrumski rn
            INNER JOIN Automobili a ON rn.KamionID = a.ID
            INNER JOIN Partnerji p ON a.PartnerID = p.PaSifra AND p.DrumskiPrevoz = 1
            WHERE rn.ID = @RadniNalogID";

                using (SqlCommand cmd = new SqlCommand(queryHeader, conn))
                {
                    cmd.Parameters.AddWithValue("@RadniNalogID", RadniNalogID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblBrojNaloga.Text = reader["BrojNaloga"].ToString();
                            lblVozac.Text = reader["Vozac"].ToString();
                            lblTelefon.Text = reader["BrojTelefona"].ToString();
                            lblPrevoznik.Text = reader["Prevoznik"].ToString();
                            lblVremeSlanjaSMS.Text = reader["PorukaPoslata"].ToString();
                        }
                    }
                }
               

                // --- Dokumenta ---
                string queryDocs = @"
                    SELECT u.ID AS DokumentID,
                           u.FileName AS NazivFajla,
                           u.FilePath AS FilePath,
                           REPLACE(CONVERT(varchar, u.UploadedAT, 104) + ' ' + CONVERT(varchar, u.UploadedAT, 108), ':', '.') AS DatumDodavanja,
                           Status, 'UploadedFiles' as Tabela
                    FROM UploadedFiles u
                    WHERE u.RadniNalogDrumskiID = @RadniNalogID
                    UNION
		            SELECT u.ID AS DokumentID,
		                    u.NazivFajla,
		                    u.Putanja as FilePath,
		                    REPLACE(CONVERT(varchar, u.DatumDodavanja, 104) + ' ' + CONVERT(varchar, u.DatumDodavanja, 108), ':', '.') AS DatumDodavanja,
		                    Status, 'DokumentaRadnogNalogaDrumski' as Tabela
		            FROM DokumentaRadnogNalogaDrumski u
		            WHERE u.RadniNalogDrumskiID = @RadniNalogID
                UNION
                SELECT u.ID AS DokumentID,
		                    u.NazivDokumenta AS NazivFajla,
		                    u.Putanja as FilePath,
		                    REPLACE(CONVERT(varchar, u.DatumDodavanja, 104) + ' ' + CONVERT(varchar, u.DatumDodavanja, 108), ':', '.') AS DatumDodavanja,
                            Status,
                            'DokumentaFaktureDrumski' AS Tabela
                    FROM DokumentaFaktureDrumski u

                    WHERE u.FakturaDrumskiID = @RadniNalogID";

                DataTable dtDocs = new DataTable();
                using (SqlDataAdapter daDocs = new SqlDataAdapter(queryDocs, conn))
                {
                    daDocs.SelectCommand.Parameters.AddWithValue("@RadniNalogID", RadniNalogID);
                    daDocs.Fill(dtDocs);
                }

                // Dodaj kolonu za slike
                if (!dtDocs.Columns.Contains("Slika"))
                {
                    dtDocs.Columns.Add("Slika", typeof(Image));
                }

                // Prolazi kroz sve redove
                foreach (DataRow row in dtDocs.Rows)
                {
                    string putanja = row["FilePath"].ToString();

                    if (File.Exists(putanja))
                    {
                        string ext = Path.GetExtension(putanja).ToLower();
                        Image docImage = null;

                        if (ext == ".pdf")
                        {
                            try
                            {
                                // Učitaj prvu stranicu PDF-a kao sliku
                                using (var document = PdfiumViewer.PdfDocument.Load(putanja))
                                {
                                    docImage = document.Render(0, 180, 140,true);
                                }
                            }
                            catch
                            {
                                // U slučaju greške, postavi fallback ikonicu
                                // docImage = Properties.Resources.pdf_icon;
                                // Ako nema, ostavi null
                            }
                        }
                        else
                        {
                            try
                            {
                                // Učitaj sliku i napravi thumbnail
                                using (Image originalImage = Image.FromFile(putanja))
                                {
                                    docImage = new Bitmap(originalImage, new Size(180, 140));
                                }
                            }
                            catch
                            {
                                // U slučaju greške, ostavi null
                            }
                        }

                        // Postavi kreiranu sliku u DataTable
                        if (docImage != null)
                        {
                            row["Slika"] = docImage;
                        }
                    }
                    else
                    {
                        // Ako fajl ne postoji, prikaži nešto drugo ili ostavi prazno
                        row["Slika"] = DBNull.Value;
                    }
                }

                // Poveži DataTable sa DataGridView-om
                dataGridView1.DataSource = dtDocs;

                dataGridView1.DataSource = dtDocs;
                PodesiDatagridView(dataGridView1);

                // Sakrij interne kolone
                dataGridView1.Columns["FilePath"].Visible = false;
                dataGridView1.Columns["DokumentID"].Visible = false;
                dataGridView1.Columns["NazivFajla"].Visible = false;
                dataGridView1.Columns["Tabela"].Visible = false;

                // Stilizacija
                dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
                dataGridView1.RowTemplate.Height = 150;
                dataGridView1.ReadOnly = true;
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGridView1.DefaultCellStyle.Padding = new Padding(5);

                // FileName kolona
         //       dataGridView1.Columns["FileName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                // DatumDodavanja
                //dataGridView1.Columns["DatumDodavanja"].Width = 100;

                // Status kolona
                dataGridView1.Columns["Status"].Width = 100;

                // Dodaj kolonu Akcija (dva linka)
                if (dataGridView1.Columns["Akcija"] == null)
                {
                    DataGridViewTextBoxColumn actionCol = new DataGridViewTextBoxColumn();
                    actionCol.Name = "Akcija";
                    actionCol.HeaderText = "Akcija";
                    dataGridView1.Columns.Add(actionCol);
                }

                // Slika
                if (dataGridView1.Columns["Slika"] is DataGridViewImageColumn imgCol)
                {
                    imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    imgCol.Width = 180;
                    imgCol.DefaultCellStyle.NullValue = null;
                }

                // Dugme Obriši
                if (dataGridView1.Columns["Obrisi"] == null)
                {
                    DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                    btnDelete.Name = "Obrisi";
                    btnDelete.HeaderText = "Obriši";
                    btnDelete.Text = "Obriši";
                    btnDelete.UseColumnTextForButtonValue = true;
                    btnDelete.Width = 80;
                    dataGridView1.Columns.Add(btnDelete);
                }

                // CellPainting za kolonu Akcija (prikaz dva linka)
                dataGridView1.CellPainting -= DataGridView1_CellPaintingCustom;
                dataGridView1.CellPainting += DataGridView1_CellPaintingCustom;

                // Postavi da kolone zauzimaju sav prostor grida
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Zatim postavi proporcije za svaku kolonu
              

                if (dataGridView1.Columns.Contains("DatumDodavanja"))
                {
                    dataGridView1.Columns["DatumDodavanja"].FillWeight = 20;
                }
                if (dataGridView1.Columns.Contains("Status"))
                {
                    dataGridView1.Columns["Status"].FillWeight = 20;
                }
                if (dataGridView1.Columns.Contains("Slika"))
                {
                    dataGridView1.Columns["Slika"].FillWeight = 30;
                }
                if (dataGridView1.Columns.Contains("Akcija"))
                {
                    dataGridView1.Columns["Akcija"].FillWeight = 20;
                }                
                
                if (dataGridView1.Columns.Contains("Obrisi"))
                {
                    dataGridView1.Columns["Obrisi"].FillWeight = 10;   
                }


                    //// CellPainting za kolonu Akcija (prikaz dva linka)
                    //dataGridView1.CellPainting -= DataGridView1_CellPaintingAkcija;
                    //dataGridView1.CellPainting += DataGridView1_CellPaintingAkcija;


                }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var dgv = sender as DataGridView;
            DataRowView row = dgv.Rows[e.RowIndex].DataBoundItem as DataRowView;
            var ins = new InsertFakture();
            string tabela = row["Tabela"].ToString();
            int dokumentId = Convert.ToInt32(row["DokumentID"]);

            if (dgv.Columns[e.ColumnIndex].Name == "Akcija")
            {
                if (e.Location.X >= 5 && e.Location.X <= 50)
                {
                    ins.UpdateStatusFajla(dokumentId, "Dobar",  tabela);
                }
                else if (e.Location.X >= 60 && e.Location.X <= 100)
                {
                    ins.UpdateStatusFajla(dokumentId, "Loš", tabela);
                }
                RefreshData();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var dgv = sender as DataGridView;
            DataRowView row = dgv.Rows[e.RowIndex].DataBoundItem as DataRowView;

            // Klik na sliku
            if (dgv.Columns[e.ColumnIndex].Name == "Slika")
            {
                string putanja = row["FilePath"].ToString();

                if (File.Exists(putanja))
                {
                    string ekstenzija = Path.GetExtension(putanja).ToLower();

                   if (ekstenzija == ".pdf")
                    {
                        Form pdfForm = new Form();
                        pdfForm.Text = "Pregled PDF dokumenta";
                        pdfForm.StartPosition = FormStartPosition.CenterParent;
                        pdfForm.Size = new Size(800, 600);
                        if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
                        {
                            pdfForm.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                            pdfForm.BackColor = Color.White;
                            pdfForm.Text = "Pregled slike";

                        }

                        try
                        {
                            // Koristimo Load() metodu koja radi za otvaranje dokumenta
                            var document = PdfiumViewer.PdfDocument.Load(putanja);
                            var pdfRenderer = new PdfiumViewer.PdfRenderer();
                            pdfRenderer.Load(document);
                            pdfRenderer.Dock = DockStyle.Fill;
                            pdfForm.Controls.Add(pdfRenderer);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Greška pri učitavanju PDF dokumenta: " + ex.Message);
                        }

                        pdfForm.ShowDialog();
                    }
                   else if (ekstenzija == ".jpg" || ekstenzija == ".jpeg" || ekstenzija == ".png" || ekstenzija == ".gif")
                    {
                        // Tvoj postojeći kod za prikaz slike
                        using (Image fullImg = Image.FromFile(putanja))
                        {
                            Form preview = new Form();
                            preview.StartPosition = FormStartPosition.CenterParent;
                            preview.Size = new Size(800, 600);
                            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
                            {
                                preview.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                                preview.BackColor = Color.White;
                                preview.Text = "Pregled slike";

                            }

                            PictureBox pb = new PictureBox();
                            pb.Image = new Bitmap(fullImg);
                            pb.SizeMode = PictureBoxSizeMode.Zoom;
                            pb.Dock = DockStyle.Fill;

                            preview.Controls.Add(pb);
                            preview.ShowDialog();
                        }
                    }
                }
                //string putanja = row["FilePath"].ToString(); // ovde se povlači prava putanja iz baze
                //if (File.Exists(putanja))
                //{
                //    using (Image fullImg = Image.FromFile(putanja))
                //    {
                //        Form preview = new Form();
                //        preview.StartPosition = FormStartPosition.CenterParent;
                //        preview.Size = new Size(800, 600);
                //        if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
                //        {
                //            preview.Icon = Saobracaj.Properties.Resources.LegetIconPNG; 
                //            preview.BackColor = Color.White; 
                //            preview.Text = "Pregled slike";

                //        }

                //        PictureBox pb = new PictureBox();
                //        pb.Image = new Bitmap(fullImg); // prikaži originalnu rezoluciju
                //        pb.SizeMode = PictureBoxSizeMode.Zoom;
                //        pb.Dock = DockStyle.Fill;

                //        preview.Controls.Add(pb);
                //        preview.ShowDialog();
                //    }
                //}
            }

            // Klik na dugme Obriši
            if (dgv.Columns[e.ColumnIndex].Name == "Obrisi")
            {
                var ins = new InsertFakture();
                int dokumentID = Convert.ToInt32(row["DokumentID"]);
                string putanja = row["FilePath"].ToString();
                string tabela = row["Tabela"].ToString();
                if (File.Exists(putanja))
                {
                    ins.DelDokumentVozaca(dokumentID, tabela);
                    File.Delete(putanja);
                    MessageBox.Show("Dokument je uspešno obrisan.");
                    RefreshData();
                }
                else
                {
                    MessageBox.Show("Dokument ne postoji na zadatoj lokaciji.");
                }
            }
        }

     
        private void DataGridView1_CellPaintingCustom(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Kolona Akcija
            if (e.ColumnIndex == dataGridView1.Columns["Akcija"].Index)
            {
                e.PaintBackground(e.CellBounds, true);
                Font f = e.CellStyle.Font;
                Brush linkBrush = Brushes.Blue;

                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Near,      // levo poravnanje
                    LineAlignment = StringAlignment.Center // vertikalno centrirano
                };

                // "Dobar" u levom delu ćelije
                Rectangle rectDobar = new Rectangle(e.CellBounds.X + 5, e.CellBounds.Y, 50, e.CellBounds.Height);
                e.Graphics.DrawString("Dobar", f, linkBrush, rectDobar, sf);

                // "Loš" malo desno od Dobrog
                Rectangle rectLos = new Rectangle(e.CellBounds.X + 60, e.CellBounds.Y, 50, e.CellBounds.Height);
                e.Graphics.DrawString("Loš", f, linkBrush, rectLos, sf);
                //e.Graphics.DrawString("Dobar", f, linkBrush, e.CellBounds.X + 5, e.CellBounds.Y + 5);
                //e.Graphics.DrawString("Loš", f, linkBrush, e.CellBounds.X + 60, e.CellBounds.Y + 5);
                e.Handled = true;
            }

            // Kolona Obriši
            if (e.ColumnIndex == dataGridView1.Columns["Obrisi"].Index)
            {
                e.PaintBackground(e.CellBounds, true);

                int btnWidth = 60;
                int btnHeight = 30;
                int x = e.CellBounds.X + (e.CellBounds.Width - btnWidth) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - btnHeight) / 2;

                Rectangle btnRect = new Rectangle(x, y, btnWidth, btnHeight);

                ButtonRenderer.DrawButton(e.Graphics, btnRect, "Obriši", e.CellStyle.Font, false, PushButtonState.Default);
                e.Handled = true;
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

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                dataGridView1.ClearSelection();
                dataGridView1.CurrentCell = null;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.DefaultCellStyle.SelectionBackColor = dataGridView1.DefaultCellStyle.BackColor;
                dataGridView1.DefaultCellStyle.SelectionForeColor = dataGridView1.DefaultCellStyle.ForeColor;
            });
            dataGridView1.RowHeadersVisible = false;
        }

    }
}
