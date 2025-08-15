using Saobracaj.Pantheon_Export;
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Drumski
{
    public partial class DodavanjeSkeniraneFakture: Form
    {
        private int radniNalogID = 0;
        private int fakturaDrumskogID = 0;
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
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
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
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Odaberite fajlove za upload";
                ofd.Filter = "Svi fajlovi|*.*";
                ofd.Multiselect = true;

                if (ofd.ShowDialog() != DialogResult.OK)
                    return;

                int zaposleniID = PostaviVrednostZaposleni();
                string destinacijaFolder = $@"\\192.168.99.10\Leget\Drumski\Dokumenta\Fakture\ID_{radniNalogID}";

                if (!Directory.Exists(destinacijaFolder))
                    Directory.CreateDirectory(destinacijaFolder);

                // 1) Pronadji prvi "prazan" dokument (putanja NULL ili prazan string)
                int? prazniDokumentId = null;
                using (var con = new SqlConnection(Saobracaj.Sifarnici.frmLogovanje.connectionString))
                using (var cmd = new SqlCommand(@"
                        SELECT TOP 1 ID
                        FROM DokumentaFaktureDrumski
                        WHERE FakturaDrumskiID = @fid
                               AND (Putanja IS NULL OR LTRIM(RTRIM(Putanja)) = '')
                        ORDER BY DatumDodavanja ASC;", con))
                {
                    cmd.Parameters.AddWithValue("@fid", radniNalogID);
                    con.Open();
                    var o = cmd.ExecuteScalar();
                    prazniDokumentId = (o == null || o == DBNull.Value) ? (int?)null : Convert.ToInt32(o);
                }

                bool prviUpdate = prazniDokumentId.HasValue;
                InsertFakture ins = new InsertFakture();

                // Lista postojećih fajlova iz textbox-a (već učitanih iz baze)
                var postojecePutanje = txtNazivDodatihFajlova.Text
                    .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(p => p.Trim())
                    .ToList();

                // 2) Obradi sve izabrane fajlove
                foreach (string fajl in ofd.FileNames)
                {
                    string nazivFajla = Path.GetFileName(fajl);
                    string destinacija = Path.Combine(destinacijaFolder, nazivFajla);

                    // Proveri da li već postoji
                    if (postojecePutanje.Contains(nazivFajla, StringComparer.OrdinalIgnoreCase))
                        continue; // preskoči duplikat

                    // Kopiraj fajl
                    File.Copy(fajl, destinacija, true);

                    if (prviUpdate)
                    {
                        // Ažuriraj postojeći "prazan" red
                        ins.UpdateFajlaUBazi(
                            radniNalogID,
                            txtNaslov.Text.Trim(),
                            txtBeleske.Text.Trim(),
                            destinacija,
                            zaposleniID,
                            nazivFajla,
                            2
                        );

                        prviUpdate = false;
                    }
                    else
                    {
                        // Ubaci novi zapis
                        ins.SnimiUFajlBazu(
                            radniNalogID,
                            txtNaslov.Text.Trim(),
                            txtBeleske.Text.Trim(),
                            zaposleniID,
                            destinacija,
                            nazivFajla,
                            2  // tip 1 je prevoznica, tip 2 fakura
                        );
                    }

                    if (!txtNazivDodatihFajlova.Text.EndsWith(Environment.NewLine))
                    {
                        txtNazivDodatihFajlova.AppendText(Environment.NewLine);
                    }

                    // Dodaj u textbox i u listu postojećih fajlova
                    txtNazivDodatihFajlova.AppendText(nazivFajla + Environment.NewLine);
                    postojecePutanje.Add(nazivFajla);
                }

                MessageBox.Show("Fajlovi su uspešno dodati.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška: " + ex.Message);
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
                        txtNaslov.Text = dr["Naslov"].ToString().Trim();
                        txtBeleske.Text = dr["Napomena"].ToString().Trim();
                        lblUlaznaF.Text = dr["UlaznaFaktura"].ToString().Trim();
                        // Dodaj putanju iz prvog reda
                        string nazivDokumenata = dr["NazivDokumenta"]?.ToString().Trim();
                        if (!string.IsNullOrEmpty(nazivDokumenata))
                            putanje.Add(nazivDokumenata);

                        // Prođi kroz ostatak redova samo za NazivDokumenta
                        while (dr.Read())
                        {
                            nazivDokumenata = dr["NazivDokumenta"]?.ToString().Trim();
                            if (!string.IsNullOrEmpty(nazivDokumenata))
                                putanje.Add(nazivDokumenata);
                        }

                    }

                }
                txtNazivDodatihFajlova.Text = string.Join(Environment.NewLine, putanje);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

            // 1. Provera da li postoji barem jedan zapis u DokumentaFaktureDrumski
            bool postoji;
            using (var con = new SqlConnection(s_connection))
            using (var cmd = new SqlCommand(@"
                SELECT COUNT(*) 
                FROM DokumentaFaktureDrumski 
                WHERE FakturaDrumskiID = @fid and Tip = 2;", con))
            {
                cmd.Parameters.AddWithValue("@fid", radniNalogID);
                con.Open();
                postoji = (int)cmd.ExecuteScalar() > 0;
            }

            // 2. Poziv odgovarajuće procedure
            InsertFakture ins = new InsertFakture();

            if (postoji)
            {
                // UPDATE — ako već postoji barem jedan fajl za tu fakturu
                ins.UpdateFajlaUBazi(
                    radniNalogID,
                    txtNaslov.Text.Trim(),
                    txtBeleske.Text.Trim(),
                    null,
                    null,
                    null,
                    2 // Tip za fakturu 2
                );
            }
            else
            {
                // INSERT — ako ne postoji nijedan fajl za tu fakturu
                ins.SnimiUFajlBazu(
                    radniNalogID,
                    txtNaslov.Text.Trim(),
                    txtBeleske.Text.Trim(),
                    null, // Putanja
                    null, // DodaoKorisnik
                    null, // naziv fajla
                    2  // Tip za fakturu 2
                );
            }

        }
    }
}
