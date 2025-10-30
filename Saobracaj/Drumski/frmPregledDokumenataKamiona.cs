using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.Drumski
{
    public partial class frmPregledDokumenataKamiona: Form
    {
        public int RadniNalogID;
        private Dictionary<string, string> fajloviZaUpload =
           new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private bool cellClickHandlerAttached = false;
        public frmPregledDokumenataKamiona(int radniNalogId)
        {
            InitializeComponent();
            RadniNalogID = radniNalogId;
            ChangeTextBox();
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
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                Office2010Colors.ApplyManagedColors(this, Color.White);
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = Color.White;
                Office2010Colors.ApplyManagedColors(this, Color.White);

                foreach (Control control in Controls)
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

                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
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
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
            }
        }

        private void btnOdaberi_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Multiselect = true;
                ofd.Title = "Odabir dokumenata";
                ofd.Filter = "Svi fajlovi|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (string fajl in ofd.FileNames)
                    {
                        DodajFajlUListu(fajl);
                    }
                }
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
                        string putanja = row.Cells["FilePath"].Value?.ToString() ?? "";

                        if (File.Exists(putanja))
                        {
                            File.Delete(putanja);
                            ins.DelDokumentKamiona(dokumentID);
                            MessageBox.Show("Dokument je uspešno obrisan.");
                            RefreshDataGrid();
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
                            string putanja = row.Cells["FilePath"].Value?.ToString() ?? "";

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


        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (lstFajlovi.SelectedItem != null)
            {
                string nazivFajla = lstFajlovi.SelectedItem.ToString();

                // ukloni iz dictionary
                if (fajloviZaUpload.ContainsKey(nazivFajla))
                {
                    fajloviZaUpload.Remove(nazivFajla);
                }

                // ukloni iz liste
                lstFajlovi.Items.Remove(lstFajlovi.SelectedItem);
            }
            else
            {
                MessageBox.Show("Odaberite fajl iz liste koji želite da obrišete.");
            }
        }
        private void panelDrop_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;  // ovo dozvoljava drop
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

        }

        private void panelDrop_DragDrop(object sender, DragEventArgs e)
        {
            string[] fajlovi = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string fajl in fajlovi)
            {
                DodajFajlUListu(fajl);
            }
            ;
            MessageBox.Show("Prevuceno fajlova: " + fajlovi.Length);
         
        }


        private void DodajFajlUListu(string fajl)
        {
            string nazivFajla = Path.GetFileName(fajl);

            if (!fajloviZaUpload.ContainsKey(nazivFajla))
            {
                // Dodaj u dictionary (ključ = naziv, vrednost = puna putanja)
                fajloviZaUpload[nazivFajla] = fajl;

                // Dodaj u listbox za prikaz
                lstFajlovi.Items.Add(nazivFajla);
            }
            else
            {
                MessageBox.Show($"Fajl „{nazivFajla}” je već dodat.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int zaposleniID = PostaviVrednostZaposleni();
                int brojSnimljenihFajlova = 0;
                string destinacijaFolder = $@"\\192.168.99.10\Leget\Drumski\Dokumenta\Kamion\ID_{RadniNalogID}";

                if (!Directory.Exists(destinacijaFolder))
                    Directory.CreateDirectory(destinacijaFolder);

                InsertFakture ins = new InsertFakture();

                // svi fajlovi iz textboxa
                var fajlovi = lstFajlovi.Text
                    .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(p => p.Trim())
                    .ToList();

                if (fajloviZaUpload.Count == 0)
                {
                    MessageBox.Show("Nijedan fajl nije izabran za dodavanje.");
                    return;
                }
                foreach (var kvp in fajloviZaUpload)
                {
                    string nazivFajla = kvp.Key;
                    string sourcePath = kvp.Value;

                    bool postojiUBazi = false;
                    using (var con = new SqlConnection(Saobracaj.Sifarnici.frmLogovanje.connectionString))
                    using (var cmd = new SqlCommand(@"
                            SELECT COUNT(*) 
                            FROM UploadedFiles
                            WHERE RadniNalogDrumskiID = @fid 
                              AND FileName = @naziv 
                              AND (UploadedByVozac = 0 OR UploadedByVozac IS NULL)", con))
                    {
                        cmd.Parameters.AddWithValue("@fid", RadniNalogID);
                        cmd.Parameters.AddWithValue("@naziv", nazivFajla);
                        con.Open();
                        postojiUBazi = (int)cmd.ExecuteScalar() > 0;
                    }

                    if (postojiUBazi)
                        continue;

                    string destinacija = Path.Combine(destinacijaFolder, nazivFajla);

                    // kopiraj fajl
                    File.Copy(sourcePath, destinacija, true);

                    // snimi u bazu
                    ins.SnimiUFajlBazuKamioni(        
                        nazivFajla,
                        destinacija,
                        zaposleniID,
                        RadniNalogID,
                        0
                    );
                    brojSnimljenihFajlova++;
                }
                if (brojSnimljenihFajlova > 0)
                    MessageBox.Show("Fajlovi su uspešno sačuvani.");
               
                RefreshDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom snimanja fajlova: " + ex.Message);
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

     

        private void frmPregledDokumenataKamiona_Load(object sender, EventArgs e)
        {      
            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            string s_connection = Sifarnici.frmLogovanje.connectionString;
            var dataTable = new System.Data.DataTable();

  

                var select = @"
                         SELECT 
                             u.ID AS DokumentID,
                             u.FileName AS NazivFajla,
                             u.FilePath AS FilePath,
                             u.UploadedAT AS DatumDodavanja,
                             LTRIM(RTRIM(k.DeIme)) + ' ' + LTRIM(RTRIM(k.DePriimek)) AS DodaoKorisnik
                        FROM UploadedFiles u
                             INNER JOIN Delavci k ON u.UploadedBy = k.DeSifra
                        WHERE u.RadniNalogDrumskiID = @RadniNalogDrumskiID
                          AND UploadedByVozac = 0";

                using (SqlConnection conn = new SqlConnection(s_connection))
                {
                    SqlDataAdapter da = new SqlDataAdapter(select, conn);
                    da.SelectCommand.Parameters.AddWithValue("@RadniNalogDrumskiID", RadniNalogID);
                    da.Fill(dataTable);
                }
            

            if (dataTable.Rows.Count > 0)
            {
                // Pokaži kontrole
                lblPregledDokumenata.Visible = true; 
                panel2.Visible = true;       

                // Podesi DataGridView sa podacima
                dataGridView1.DataSource = dataTable;
                dataGridView1.ReadOnly = true;
                dataGridView1.RowHeadersVisible = false;

                // Podesi širinu kolona i sakrij nevidljive
                if (dataGridView1.Columns.Contains("FilePath"))
                {
                    dataGridView1.Columns["FilePath"].Visible = false;
                }
                if (dataGridView1.Columns.Contains("DokumentID"))
                {
                    dataGridView1.Columns["DokumentID"].Visible = false;
                }

                int ukupnaSirina = dataGridView1.Width;
                int sirinaKolone = (int)(ukupnaSirina * 0.33);

                if (dataGridView1.Columns["NazivFajla"] != null) // Promenjeno "NazivDokumenta" u "NazivFajla"
                {
                    dataGridView1.Columns["NazivFajla"].Width = sirinaKolone;
                }

                DodajDugmadKolonu();
                if (!cellClickHandlerAttached)
                {
                    dataGridView1.CellClick += dataGridView1_CellContentClick;
                    cellClickHandlerAttached = true;
                }
                PodesiDatagridView(dataGridView1);
                dataGridView1.RowHeadersWidth = 30;
            }
            else
            {
                // Sakrij kontrole ako nema rezultata
                lblPregledDokumenata.Visible = false;
                panel2.Visible = false;
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

        private void DodajDugmadKolonu()
        {
            // Kolona za instrukcije
            DataGridViewButtonColumn preuzimanjeBtn = new DataGridViewButtonColumn();
            preuzimanjeBtn.Name = "Preuzimanje";
            preuzimanjeBtn.HeaderText = "Preuzimanje";
            preuzimanjeBtn.Text = "Preuzmi";
            preuzimanjeBtn.UseColumnTextForButtonValue = true;
            preuzimanjeBtn.Width = 100;

            // Kolona za upload
            DataGridViewButtonColumn brisanjeBtn = new DataGridViewButtonColumn();
            brisanjeBtn.Name = "Brisanje";
            brisanjeBtn.HeaderText = "Brisanje";
            brisanjeBtn.Text = "Briši";
            brisanjeBtn.UseColumnTextForButtonValue = true;
            brisanjeBtn.Width = 100;


            // Dodaj ako već ne postoje
            if (!dataGridView1.Columns.Contains("Preuzimanje"))
                dataGridView1.Columns.Add(preuzimanjeBtn);

            if (!dataGridView1.Columns.Contains("Brisanje"))
                dataGridView1.Columns.Add(brisanjeBtn);
        }
      
    }
}


