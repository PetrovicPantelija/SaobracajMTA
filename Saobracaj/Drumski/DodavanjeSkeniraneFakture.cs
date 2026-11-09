using Syncfusion.Windows.Forms;
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
    public partial class DodavanjeSkeniraneFakture: Form
    {
        private int radniNalogID = 0;
        private int fakturaDrumskogID = 0;
        private Dictionary<string, string> fajloviZaUpload =
           new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public DodavanjeSkeniraneFakture(int ID, int FakturaDrumskogID)
        {
            fakturaDrumskogID = FakturaDrumskogID;
            radniNalogID = ID;
            InitializeComponent();
            ChangeTextBox();
            VratiPodatke();
        }

        private void ChangeTextBox()
        {

            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
            panelHeader.Visible = false;


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
                panelHeader.Visible = true;
                //  meniHeader.Visible = false;
                this.BackColor = Color.White;
                this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
                this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
                this.ControlBox = true;
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                Office2010Colors.ApplyManagedColors(this, Color.White);
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = Color.White;


                foreach (Control control in this.Controls)
                {
                    if (control is System.Windows.Forms.Button buttons)
                    {

                        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                        buttons.FlatStyle = FlatStyle.Flat;
                    }
                }


                foreach (Control control in this.Controls)
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
                panelHeader.Visible = false;
                // meniHeader.Visible = true;
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }
        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog
                {
                    Title = "Odaberite fajlove",
                    Filter = "Svi fajlovi|*.*",
                    Multiselect = true
                };

                if (ofd.ShowDialog() != DialogResult.OK)
                    return;

                // već dodati fajlovi u textbox-u
                var postojece = txtNazivDodatihFajlova.Text
                    .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(p => p.Trim())
                    .ToList();

                // fajlovi koji su već u bazi
                List<string> fajloviUBazi = new List<string>();
                using (var con = new SqlConnection(Saobracaj.Sifarnici.frmLogovanje.connectionString))
                using (var cmd = new SqlCommand(@"
                            SELECT NazivDokumenta
                            FROM DokumentaFaktureDrumski
                            WHERE FakturaDrumskiID = @fid AND Tip = 2", con))
                {
                    cmd.Parameters.AddWithValue("@fid", radniNalogID);
                    con.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                            fajloviUBazi.Add(rdr.GetString(0));
                    }
                }

                foreach (string fullPath in ofd.FileNames)
                {
                    string nazivFajla = Path.GetFileName(fullPath);

                    if (postojece.Contains(nazivFajla, StringComparer.OrdinalIgnoreCase))
                        continue; // već u textboxu

                    if (fajloviUBazi.Contains(nazivFajla, StringComparer.OrdinalIgnoreCase))
                    {
                        MessageBox.Show($"Fajl '{nazivFajla}' već postoji u bazi.");
                        continue;
                    }

                    // zapamti putanju u dictionary
                    fajloviZaUpload[nazivFajla] = fullPath;

                    // upiši u textbox
                    if (!string.IsNullOrEmpty(txtNazivDodatihFajlova.Text) &&
                        !txtNazivDodatihFajlova.Text.EndsWith(Environment.NewLine))
                    {
                        txtNazivDodatihFajlova.AppendText(Environment.NewLine);
                    }
                    txtNazivDodatihFajlova.AppendText(nazivFajla + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom izbora fajlova: " + ex.Message);
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

        private void VratiPodatke()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            using (SqlConnection con = new SqlConnection(s_connection))
            {
                con.Open();

                List<string> putanje = new List<string>();
                SqlCommand cmd = new SqlCommand("" +
                   " ; WITH F AS( " +
                    " SELECT " +
                    " fd.RadniNalogDrumskiID AS RNID, " +
                    " MAX(CASE WHEN fs.TipFakture = 1 THEN fs.UlaznaFaktura END)  AS UlaznaFaktura " +
                    " FROM FakturaDrumski fd " +
                    " LEFT JOIN FakturaDrumskiStavka fs ON fs.FaktureDrumskogID = fd.ID " +
                    " GROUP BY fd.RadniNalogDrumskiID " +
                    " ) " +
                    "SELECT a.RegBr, p.PaNaziv,a.PartnerID, d.Naslov, d.Napomena, d.NazivDokumenta, F.UlaznaFaktura " +
                                             "  FROM RadniNalogDrumski rn" +
                                             "       LEFT JOIN Automobili a  ON rn.KamionID = a.ID " +
                                             "       LEFT JOIN Partnerji p ON a.PartnerID = p.PaSifra AND p.DrumskiPrevoz = 1 " +
                                             "       LEFT JOIN DokumentaFaktureDrumski d  ON d.FakturaDrumskiID = rn.ID AND Tip = 2 " +
                                             "       LEFT JOIN F  ON F.RNID = rn.ID " +
                                             " WHERE rn.ID = " + radniNalogID, con);


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        txtPrevoznik.Text = dr["PaNaziv"].ToString().Trim();
                        lblUlaznaF.Text = dr["UlaznaFaktura"].ToString().Trim();

                    }
                }
                txtNazivDodatihFajlova.Text = string.Join(Environment.NewLine, putanje);

            }
        }

        private void btnSnimi_Click(object sender, EventArgs e)
        {
            try
            {
                int zaposleniID = PostaviVrednostZaposleni();
                int brojSnimljenihFajlova = 0;
                string destinacijaFolder = $@"\\192.168.150.110\Leget\Drumski\Dokumenta\Faktura\ID_{radniNalogID}";

                if (!Directory.Exists(destinacijaFolder))
                    Directory.CreateDirectory(destinacijaFolder);

                InsertFakture ins = new InsertFakture();

                // svi fajlovi iz textboxa
                var fajlovi = txtNazivDodatihFajlova.Text
                    .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(p => p.Trim())
                    .ToList();

                if (fajloviZaUpload.Count == 0)
                {
                    MessageBox.Show("Nijedan fajl nije izabran za dodavanje.");
                    return;
                }

                foreach (string nazivFajla in fajlovi)
                {
                   
                    if (!fajloviZaUpload.TryGetValue(nazivFajla, out string sourcePath))
                        MessageBox.Show($"Fajl „{nazivFajla}” već postoji i neće biti dodat ponovo.");

                    bool postojiUBazi = false;
                    using (var con = new SqlConnection(Saobracaj.Sifarnici.frmLogovanje.connectionString))
                    using (var cmd = new SqlCommand(@"
                            SELECT COUNT(*) 
                            FROM DokumentaFaktureDrumski
                            WHERE FakturaDrumskiID = @fid AND NazivDokumenta = @naziv AND Tip = 2", con))
                    {
                        cmd.Parameters.AddWithValue("@fid", radniNalogID);
                        cmd.Parameters.AddWithValue("@naziv", nazivFajla);
                        con.Open();
                        postojiUBazi = (int)cmd.ExecuteScalar() > 0;
                    }

                    if (postojiUBazi)
                        continue; // već postoji u bazi sa tim nazivomDokumenta, preskoči

                    string destinacija = Path.Combine(destinacijaFolder, nazivFajla);

                    // kopiranje fajla
                    File.Copy(sourcePath, destinacija, true);

                    // snimanje u bazu
                    ins.SnimiUFajlBazu(
                        radniNalogID,
                        txtNaslov.Text.Trim(),
                        txtBeleske.Text.Trim(),
                        zaposleniID,
                        destinacija,
                        nazivFajla,
                        2
                    );
                    brojSnimljenihFajlova++;
                }
                if (brojSnimljenihFajlova > 0)
                    MessageBox.Show("Fajlovi su uspešno sačuvani.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom snimanja fajlova: " + ex.Message);
            }
        }
    }
}
