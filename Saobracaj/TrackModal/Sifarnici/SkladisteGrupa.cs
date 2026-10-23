using Microsoft.ReportingServices.Diagnostics.Internal;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Testiranje.Sifarnici;

namespace Saobracaj.TrackModal.Sifarnici
{
    public partial class SkladisteGrupa : Form
    {
        public static string code = "frmZone";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Saobracaj.Sifarnici.frmLogovanje.user.ToString();
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        string niz = "";

        string KorisnikCene;
        bool status = false;
        public SkladisteGrupa()
        {
            InitializeComponent();
            ChangeTextBox();
            
        }

        private void ChangeTextBox()
        {
            panelHeader.Visible = false;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
                panelHeader.Visible = true;
                meniHeader.Visible = false;
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
                meniHeader.Visible = true;
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        private void PodesiDatagridView(DataGridView dgv)
        {

            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(90, 199, 249); // Selektovana boja
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.BackgroundColor = Color.White;

            dgv.DefaultCellStyle.Font = new Font("Helvetica", 12F, GraphicsUnit.Pixel);
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

        // Zone za izbor: aktivne FIZIČKE zone (i zona zapisa koji se menja)
        DataView zone;
        private const string FizickaZona = "TipZoneID IN (SELECT ID FROM dbo.TipZone WHERE Naziv LIKE N'FIZI%')";

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtSifra.Enabled = false;
            txtSifra.Text = "";
            txtOznaka.Text = "";
            txtNaziv.Text = "";
            txtAdresa.Text = "";
            txtGrad.Text = "";
            txtDrzava.Text = "";
            txtPostanskiBroj.Text = "";
            txtLatitude.Text = "";
            txtLongitude.Text = "";
            chkCarinsko.Checked = false;
            cboCarinskaIspostava.SelectedIndex = -1;
            txtCarinskaSifra.Text = "";
            txtCarinskiNaziv.Text = "";
            txtDrzalac.Text = "";
            chkAktivna.Checked = true;
            HijerarhijaSkladista.PostaviTrenutnog(zone, 0);
            cboZonaID.SelectedIndex = -1;
            lblNadredjeni.Text = "";
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            int tmpAktivna = 0;

            if (txtSifra.Text == "")
            {
                status = true;

            }


            if (chkAktivna.Checked == true)
            { tmpAktivna = 1; };

            int id = status ? 0 : HijerarhijaSkladista.CeoBroj(txtSifra.Text);
            int zonaId = HijerarhijaSkladista.IzabraniId(cboZonaID);
            decimal? lat = null, lon = null;

            string greska = HijerarhijaSkladista.ProveriOznaku(NivoSkladista.Lokacija, id, txtOznaka.Text);
            if (greska == null && txtNaziv.Text.Trim().Length == 0)
                greska = "Unesite naziv lokacije.";
            if (greska == null && zonaId == 0)
                greska = "Izaberite zonu.";
            if (greska == null && Convert.ToInt32(HijerarhijaSkladista.Skalar(
                    "SELECT COUNT(*) FROM dbo.Zone WHERE ID = @ID AND " + FizickaZona, HijerarhijaSkladista.Param("@ID", zonaId))) == 0)
                greska = "Lokacija može biti samo u FIZIČKOJ zoni.";
            if (greska == null && !HijerarhijaSkladista.ProcitajKoordinatu(txtLatitude.Text, -90, 90, out lat))
                greska = "Latitude mora biti broj između -90 i 90.";
            if (greska == null && !HijerarhijaSkladista.ProcitajKoordinatu(txtLongitude.Text, -180, 180, out lon))
                greska = "Longitude mora biti broj između -180 i 180.";
            if (greska == null)
                greska = HijerarhijaSkladista.ProveriAktivnost(NivoSkladista.Lokacija, id, zonaId, tmpAktivna == 1);
            if (greska != null)
            {
                MessageBox.Show(greska, "Lokacije", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tip skladišta se više ne unosi na lokaciji (prelazi na objekat); šalje se postojeća vrednost
            int tipSkladista = HijerarhijaSkladista.IzabraniId(cboTipSkladista);

            try
            {
                if (status == true)
                {
                    InsertSkladistaGrupa ins = new InsertSkladistaGrupa();
                    ins.InsLokacija(txtNaziv.Text, txtOznaka.Text, zonaId, txtDrzava.Text, txtGrad.Text, txtAdresa.Text, tmpAktivna, tipSkladista);
                    id = HijerarhijaSkladista.IdPoOznaci(NivoSkladista.Lokacija, txtOznaka.Text);
                    txtSifra.Text = id.ToString();
                    status = false;
                }
                else
                {
                    InsertSkladistaGrupa upd = new InsertSkladistaGrupa();
                    upd.UpdLokacija(id, txtNaziv.Text, txtOznaka.Text, zonaId, txtDrzava.Text, txtGrad.Text, txtAdresa.Text, tmpAktivna, tipSkladista);
                }

                bool carinsko = chkCarinsko.Checked;
                HijerarhijaSkladista.Procedura("UpdateSkladistaGrupaDodatno",
                    HijerarhijaSkladista.Param("@ID", id),
                    HijerarhijaSkladista.ParamTekst("@PostanskiBroj", txtPostanskiBroj.Text, 10),
                    HijerarhijaSkladista.ParamKoordinata("@Latitude", lat),
                    HijerarhijaSkladista.ParamKoordinata("@Longitude", lon),
                    HijerarhijaSkladista.Param("@CarinskoSkladiste", carinsko ? 1 : 0),
                    HijerarhijaSkladista.Param("@CarinskaIspostavaID", carinsko && HijerarhijaSkladista.IzabraniId(cboCarinskaIspostava) > 0
                        ? (object)HijerarhijaSkladista.IzabraniId(cboCarinskaIspostava) : null),
                    HijerarhijaSkladista.ParamTekst("@CarinskaSifraSkladista", carinsko ? txtCarinskaSifra.Text : "", 30),
                    HijerarhijaSkladista.ParamTekst("@CarinskiNazivSkladista", carinsko ? txtCarinskiNaziv.Text : "", 100),
                    HijerarhijaSkladista.ParamTekst("@DrzalacSkladista", carinsko ? txtDrzalac.Text : "", 100),
                    HijerarhijaSkladista.Param("@Datum", DateTime.Now),
                    HijerarhijaSkladista.ParamTekst("@Korisnik", Kor, 20));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lokacije", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            RefreshDataGrid();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            int id = HijerarhijaSkladista.CeoBroj(txtSifra.Text);
            if (id == 0)
                return;

            string greska = HijerarhijaSkladista.ProveriBrisanje(NivoSkladista.Lokacija, id);
            if (greska != null)
            {
                MessageBox.Show(greska, "Lokacije", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da želite da brišete lokaciju?", "Izbor", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                InsertSkladistaGrupa upd = new InsertSkladistaGrupa();
                upd.DeleteLokacija(id);
                RefreshDataGrid();
            }
        }

        private void RefreshDataGrid()
        {
            var select = " SELECT g.ID, g.Oznaka, g.Naziv, g.ZonaID, z.Oznaka AS Zona_Oznaka, z.Naziv AS Zona_Naziv, g.Drzava, g.Grad, g.Adresa, g.PostanskiBroj, " +
                " CASE WHEN g.Aktivna > 0 THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END AS Aktivna, " +
                " CASE WHEN g.CarinskoSkladiste = 1 THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END AS CarinskoSkladiste, c.Naziv AS CarinskaIspostava " +
                " FROM dbo.SkladistaGrupa g " +
                " LEFT JOIN dbo.Zone z ON z.ID = g.ZonaID " +
                " LEFT JOIN dbo.Carinarnice c ON c.ID = g.CarinskaIspostavaID " +
                " ORDER BY g.ID";
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = HijerarhijaSkladista.Tabela(select);

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                PodesiDatagridView(dataGridView1);
            }
            else
            {
                dataGridView1.BorderStyle = BorderStyle.None;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dataGridView1.BackgroundColor = Color.White;

                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            }

            Kolona("ID", "ID", 50);
            Kolona("Oznaka", "Oznaka", 80);
            Kolona("Naziv", "Naziv", 200);
            Kolona("ZonaID", "Zona ID", 40);
            dataGridView1.Columns["ZonaID"].Visible = false;
            Kolona("Zona_Oznaka", "Zona Oznaka", 80);
            Kolona("Zona_Naziv", "Zona Naziv", 220);
            Kolona("Drzava", "Drzava", 120);
            Kolona("Grad", "Grad", 120);
            Kolona("Adresa", "Adresa", 220);
            Kolona("PostanskiBroj", "Poštanski broj", 90);
            Kolona("Aktivna", "Aktivna", 70);
            Kolona("CarinskoSkladiste", "Carinsko skladište", 90);
            Kolona("CarinskaIspostava", "Carinska ispostava", 180);
        }

        private void Kolona(string naziv, string naslov, int sirina)
        {
            DataGridViewColumn kolona = dataGridView1.Columns[naziv];
            if (kolona == null)
                return;
            kolona.HeaderText = naslov;
            kolona.Width = sirina;
        }

        private void VratiPodatke(string ID)
        {
            DataTable t = HijerarhijaSkladista.Tabela("SELECT * FROM dbo.SkladistaGrupa WHERE ID = @ID",
                HijerarhijaSkladista.Param("@ID", HijerarhijaSkladista.CeoBroj(ID)));
            if (t.Rows.Count == 0)
                return;
            DataRow dr = t.Rows[0];

            txtNaziv.Text = dr["Naziv"].ToString();
            txtOznaka.Text = dr["Oznaka"].ToString();
            int zonaId = HijerarhijaSkladista.CeoBroj(dr["ZonaID"]);
            HijerarhijaSkladista.PostaviTrenutnog(zone, zonaId);
            cboZonaID.SelectedValue = zonaId;
            txtDrzava.Text = dr["Drzava"].ToString();
            txtGrad.Text = dr["Grad"].ToString();
            txtAdresa.Text = dr["Adresa"].ToString();
            chkAktivna.Checked = HijerarhijaSkladista.CeoBroj(dr["Aktivna"]) == 1;
            cboTipSkladista.SelectedValue = HijerarhijaSkladista.CeoBroj(dr["TipSkladistaID"]);

            txtPostanskiBroj.Text = dr["PostanskiBroj"].ToString();
            txtLatitude.Text = HijerarhijaSkladista.KoordinataTekst(dr["Latitude"]);
            txtLongitude.Text = HijerarhijaSkladista.KoordinataTekst(dr["Longitude"]);
            chkCarinsko.Checked = HijerarhijaSkladista.CeoBroj(dr["CarinskoSkladiste"]) == 1;
            int ispostava = HijerarhijaSkladista.CeoBroj(dr["CarinskaIspostavaID"]);
            if (ispostava > 0)
                cboCarinskaIspostava.SelectedValue = ispostava;
            else
                cboCarinskaIspostava.SelectedIndex = -1;
            txtCarinskaSifra.Text = dr["CarinskaSifraSkladista"].ToString();
            txtCarinskiNaziv.Text = dr["CarinskiNazivSkladista"].ToString();
            txtDrzalac.Text = dr["DrzalacSkladista"].ToString();

            lblNadredjeni.Text = HijerarhijaSkladista.OpisNadredjenih(NivoSkladista.Lokacija, zonaId);
        }

        private void FillCombo()
        {
            zone = HijerarhijaSkladista.ListaRoditelja(NivoSkladista.Zona, FizickaZona);
            HijerarhijaSkladista.PoveziListu(cboZonaID, zone);

            cboTipSkladista.DataSource = HijerarhijaSkladista.Tabela("Select ID,  Naziv from TipSkladista order by ID");
            cboTipSkladista.DisplayMember = "Naziv";
            cboTipSkladista.ValueMember = "ID";

            cboCarinskaIspostava.DataSource = HijerarhijaSkladista.Tabela("SELECT ID, Naziv FROM dbo.Carinarnice ORDER BY Naziv");
            cboCarinskaIspostava.DisplayMember = "Naziv";
            cboCarinskaIspostava.ValueMember = "ID";
            cboCarinskaIspostava.SelectedIndex = -1;
        }

        private void SkladisteGrupa_Load(object sender, EventArgs e)
        {
            FillCombo();
            cboZonaID.SelectedIndexChanged += cboZonaID_SelectedIndexChanged;
            chkCarinsko_CheckedChanged(chkCarinsko, EventArgs.Empty);

            RefreshDataGrid();
        }

        private void cboZonaID_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblNadredjeni.Text = HijerarhijaSkladista.OpisNadredjenih(NivoSkladista.Lokacija, HijerarhijaSkladista.IzabraniId(cboZonaID));
        }

        // Carinska polja se unose samo za carinsko skladište; pri snimanju bez čekiranja se prazne
        private void chkCarinsko_CheckedChanged(object sender, EventArgs e)
        {
            bool carinsko = chkCarinsko.Checked;
            cboCarinskaIspostava.Enabled = carinsko;
            txtCarinskaSifra.Enabled = carinsko;
            txtCarinskiNaziv.Enabled = carinsko;
            txtDrzalac.Enabled = carinsko;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txtSifra.Text = row.Cells[0].Value.ToString();
                        status = false;
                        VratiPodatke(txtSifra.Text);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        // Aktivne / Neaktivne: menja aktivnost izabranih redova, uz pravila za zonu i podređene zapise
        private void PromeniAktivnost(bool aktivan)
        {
            var greske = new System.Text.StringBuilder();
            try
            {
                InsertSkladistaGrupa poz = new InsertSkladistaGrupa();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.Selected)
                        continue;

                    int id = Convert.ToInt32(row.Cells[0].Value.ToString());
                    string greska = HijerarhijaSkladista.ProveriPromenuAktivnosti(NivoSkladista.Lokacija, id, aktivan);
                    if (greska != null)
                    {
                        greske.AppendLine(row.Cells[1].Value + ": " + greska);
                        continue;
                    }

                    if (aktivan)
                        poz.ProglasiAktivnim(id, 0);
                    else
                        poz.ProglasiNeAktivnim(id, 0);
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }

            if (greske.Length > 0)
                MessageBox.Show(greske.ToString(), "Lokacije", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            RefreshDataGrid();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            PromeniAktivnost(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PromeniAktivnost(false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }
    }
}
