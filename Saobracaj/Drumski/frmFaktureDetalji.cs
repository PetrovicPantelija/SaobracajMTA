using Syncfusion.Windows.Forms;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Drumski
{
    public partial class frmFaktureDetalji : Form
    {
        private int RadniNalogID = -1;
        private int NalogID = -1;
        private int FakturaID = -1;
        private string Klijent = "";
        public frmFaktureDetalji(int id, int nalogID, string klijent)
        {
            RadniNalogID = id;
            NalogID = nalogID;
            Klijent = klijent;
            InitializeComponent();
            ChangeTextBox();
            VratiPodatke();
            this.Text = "Pregled detalja fakture za prevoznika " + Klijent +" i " +  RadniNalogID.ToString();
        }

        private void ChangeTextBox()
        {
            panelHeader.Visible = true;
            this.BackColor = Color.White;
            this.BackColor = Color.White;
            this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
            this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
            Office2010Colors.ApplyManagedColors(this, Color.White);
            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
            //meniHeader.Visible = false;
            this.ControlBox = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
                //meniHeader.Visible = true;
                //meniHeader.Visible = false;
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
                //}


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
                //meniHeader.Visible = false;
                //meniHeader.Visible = true;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }


        private void btnSnimi_Click(object sender, EventArgs e)
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

            int? izlaznaID = null;
            int? ulaznaID = null;
            int? fakturaDrumskogID = null;

            // 1. Provera da li postoje zapisi
            using (var con = new SqlConnection(s_connection))
            using (var cmd = new SqlCommand(@"
        SELECT MAX(rn.ID) as ID,
            MAX(CASE WHEN f.TipFakture = 0 THEN f.ID END) AS IzlaznaID,
            MAX(CASE WHEN f.TipFakture = 1 THEN f.ID END) AS UlaznaID
        FROM FakturaDrumski rn LEFT JOIN FakturaDrumskiStavka f on f.FaktureDrumskogID = rn.ID
        WHERE rn.RadniNalogDrumskiID = @fid;", con))
            {
                cmd.Parameters.AddWithValue("@fid", @RadniNalogID);

                con.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                         
                        izlaznaID = dr["IzlaznaID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["IzlaznaID"]);
                        ulaznaID = dr["UlaznaID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["UlaznaID"]);
                        fakturaDrumskogID = dr["ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["ID"]);
                        FakturaID = dr["ID"] == DBNull.Value ? -1 : Convert.ToInt32(dr["ID"]);
                    }
                }
            }

            var ins = new InsertFakture();

            DateTime? datumIzlazne = dtpDatumIzlazneFakture.Checked ? dtpDatumIzlazneFakture.Value : (DateTime?)null;

            // 2. IZLAZNA FAKTURA
            if (!string.IsNullOrWhiteSpace(txtIzlaznaFaktura.Text))
            {
                if (izlaznaID == null)
                {
                    // Insert
                    ins.InsStavkeFakture(0, fakturaDrumskogID, txtIzlaznaFaktura.Text.Trim(), null, null, datumIzlazne);
                }
                else
                {
                    // Update
                    ins.UpdateFakturaDrumskiStavka(0, izlaznaID, txtIzlaznaFaktura.Text.Trim(), datumIzlazne, null);
                }
            }

            // 3. ULAZNA FAKTURA
            if (!string.IsNullOrWhiteSpace(txtUlaznaFaktura.Text) ||
                !string.IsNullOrWhiteSpace(txtBeleske.Text))
            {
                if (ulaznaID == null)
                {
                    // Insert
                    ins.InsStavkeFakture(1, fakturaDrumskogID, null, txtUlaznaFaktura.Text.Trim(), txtBeleske.Text.Trim(), null);
                }
                else
                {
                    // Update
                    ins.UpdateFakturaDrumskiStavka(1, ulaznaID, txtUlaznaFaktura.Text.Trim(), null, txtBeleske.Text.Trim());
                }
            }
        }

        private void VratiPodatke()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

            using (SqlConnection con = new SqlConnection(s_connection))
            using (SqlCommand cmd = new SqlCommand(@"
        SELECT 
                fd.ID AS FakturaDrumskogID,
               ISNULL(NULLIF(v.NazivVoza, ''), '/') AS NazivVoza,
                MAX(CASE WHEN f.TipFakture = 0 THEN f.IzlaznaFaktura END) AS IzlaznaFaktura,
                MAX(CASE WHEN f.TipFakture = 0 THEN f.DatumSlanja END) AS DatumSlanja,
                MAX(CASE WHEN f.TipFakture = 1 THEN f.UlaznaFaktura END) AS UlaznaFaktura,
                MAX(CASE WHEN f.TipFakture = 1 THEN f.BeleskaUlazneFakture END) AS BeleskaUlazneFakture
        FROM    FakturaDrumski fd
                INNER JOIN RadniNalogDrumski rn ON fd.RadniNalogDrumskiID = rn.ID
                INNER JOIN IzvozKonacna ik ON rn.KontejnerID = ik.ID 
                LEFT JOIN FakturaDrumskiStavka f ON f.FaktureDrumskogID = fd.ID
                LEFT JOIN IzvozKonacnaZaglavlje ukz ON ukz.ID = ik.IDNadredjena
                LEFT JOIN Voz v ON v.ID = ukz.IDVoza 
        WHERE   fd.RadniNalogDrumskiID = @RadniNalogID  AND rn.Uvoz = 0
        GROUP BY fd.id, Isnull(NULLIF(v.nazivvoza, ''), '/')
        UNION
        SELECT 
                fd.ID AS FakturaDrumskogID,
                ISNULL(NULLIF(v.NazivVoza, ''), '/') AS NazivVoza,
                MAX(CASE WHEN f.TipFakture = 0 THEN f.IzlaznaFaktura END) AS IzlaznaFaktura,
                MAX(CASE WHEN f.TipFakture = 0 THEN f.DatumSlanja END) AS DatumSlanja,
                MAX(CASE WHEN f.TipFakture = 1 THEN f.UlaznaFaktura END) AS UlaznaFaktura,
                MAX(CASE WHEN f.TipFakture = 1 THEN f.BeleskaUlazneFakture END) AS BeleskaUlazneFakture
        FROM    FakturaDrumski fd
                INNER JOIN RadniNalogDrumski rn ON fd.RadniNalogDrumskiID = rn.ID
                INNER JOIN UvozKonacna uk ON rn.KontejnerID = uk.ID
                LEFT JOIN FakturaDrumskiStavka f ON f.FaktureDrumskogID = fd.ID
                LEFT JOIN UvozKonacnaZaglavlje ukz ON ukz.ID = uk.IDNadredjeni
                LEFT JOIN Voz v ON v.ID = ukz.IDVoza 
        WHERE   fd.RadniNalogDrumskiID = @RadniNalogID  AND rn.Uvoz = 1
        GROUP BY fd.id, Isnull(NULLIF(v.nazivvoza, ''), '/')
        UNION
        SELECT 
                fd.ID AS FakturaDrumskogID,
                ISNULL(NULLIF(rn.BrojVoza, ''), '/') AS NazivVoza,
                MAX(CASE WHEN f.TipFakture = 0 THEN f.IzlaznaFaktura END) AS IzlaznaFaktura,
                MAX(CASE WHEN f.TipFakture = 0 THEN f.DatumSlanja END) AS DatumSlanja,
                MAX(CASE WHEN f.TipFakture = 1 THEN f.UlaznaFaktura END) AS UlaznaFaktura,
                MAX(CASE WHEN f.TipFakture = 1 THEN f.BeleskaUlazneFakture END) AS BeleskaUlazneFakture
        FROM    FakturaDrumski fd
                INNER JOIN RadniNalogDrumski rn ON fd.RadniNalogDrumskiID = rn.ID
                LEFT JOIN FakturaDrumskiStavka f ON f.FaktureDrumskogID = fd.ID
        WHERE   fd.RadniNalogDrumskiID = @RadniNalogID  AND rn.Uvoz in (2,3)
        GROUP BY fd.id, Isnull(NULLIF(rn.BrojVoza, ''), '/');", con))
            {
                cmd.Parameters.AddWithValue("@RadniNalogID", RadniNalogID);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())  
                    {
                        FakturaID = dr["FakturaDrumskogID"] == DBNull.Value ? -1 : Convert.ToInt32(dr["FakturaDrumskogID"]);
                        txtIzlaznaFaktura.Text = dr["IzlaznaFaktura"]?.ToString();
                        txtUlaznaFaktura.Text = dr["UlaznaFaktura"]?.ToString();
                        txtBeleske.Text = dr["BeleskaUlazneFakture"]?.ToString();

                        if (dr["DatumSlanja"] != DBNull.Value)
                            dtpDatumIzlazneFakture.Value = Convert.ToDateTime(dr["DatumSlanja"]);
                        txtVoz.Text = dr["NazivVoza"]?.ToString();
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var frm = new DodavanjeSkeniranePrevoznice(RadniNalogID, FakturaID))
                frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var frm = new DodavanjeSkeniraneFakture(RadniNalogID, FakturaID))
                frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (var frm = new frmPregledSkeniranihDokumenata(RadniNalogID))
                frm.ShowDialog();
        }
    }
}
