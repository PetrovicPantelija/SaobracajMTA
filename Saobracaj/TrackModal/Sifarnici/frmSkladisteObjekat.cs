using Syncfusion.Windows.Forms;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.TrackModal.Sifarnici
{
    // Objekti (nivo III hijerarhije): skladište / depo / pretovarno mesto na lokaciji. Tabela SkladisteObjekat.
    public partial class frmSkladisteObjekat : Form
    {
        public static string code = "frmSkladisteObjekat";
        string Kor = Saobracaj.Sifarnici.frmLogovanje.user.ToString();

        bool status = false;
        DataView lokacije;
        int ucitaniTip;   // tip objekta pri učitavanju zapisa (upozorenje pri promeni tipa objekta koji ima polja)

        public frmSkladisteObjekat()
        {
            InitializeComponent();
            ChangeTextBox();
        }

        private void ChangeTextBox()
        {
            panelHeader.Visible = false;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                panelHeader.Visible = true;
                meniHeader.Visible = false;
                this.BackColor = Color.White;
                this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
                this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
                this.ControlBox = true;
                Office2010Colors.ApplyManagedColors(this, Color.White);
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                this.BackColor = Color.White;

                foreach (Control control in this.Controls)
                {
                    if (control is System.Windows.Forms.TextBox textBox)
                    {
                        textBox.BackColor = Color.White;
                        textBox.ForeColor = Color.FromArgb(51, 51, 54);
                        textBox.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.Label label && label != lblNadredjeni)
                    {
                        label.ForeColor = Color.FromArgb(110, 110, 115);
                        label.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.CheckBox chk)
                    {
                        chk.ForeColor = Color.FromArgb(110, 110, 115);
                        chk.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ComboBox cb)
                    {
                        cb.ForeColor = Color.FromArgb(51, 51, 54);
                        cb.BackColor = Color.White;
                        cb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }
                }
            }
            else
            {
                panelHeader.Visible = false;
                meniHeader.Visible = true;
            }
        }

        private void PodesiDatagridView(DataGridView dgv)
        {
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(90, 199, 249);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.BackgroundColor = Color.White;

            dgv.DefaultCellStyle.Font = new Font("Helvetica", 12F, GraphicsUnit.Pixel);
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(51, 51, 54);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);
            dgv.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 54);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv.ColumnHeadersHeight = 30;
        }

        private void frmSkladisteObjekat_Load(object sender, EventArgs e)
        {
            lokacije = HijerarhijaSkladista.ListaRoditelja(NivoSkladista.Lokacija);
            HijerarhijaSkladista.PoveziListu(cboLokacija, lokacije);

            cboTipObjekta.DataSource = HijerarhijaSkladista.Tabela("SELECT ID, Naziv FROM dbo.TipSkladista ORDER BY ID");
            cboTipObjekta.DisplayMember = "Naziv";
            cboTipObjekta.ValueMember = "ID";
            cboTipObjekta.SelectedIndex = -1;

            RefreshDataGrid();
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtSifra.Text = "";
            txtOznaka.Text = "";
            txtNaziv.Text = "";
            txtLatitude.Text = "";
            txtLongitude.Text = "";
            chkAktivan.Checked = true;
            ucitaniTip = 0;
            HijerarhijaSkladista.PostaviTrenutnog(lokacije, 0);
            cboLokacija.SelectedIndex = -1;
            cboTipObjekta.SelectedIndex = -1;
            lblNadredjeni.Text = "";
            txtOznaka.Focus();
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            int id = HijerarhijaSkladista.CeoBroj(txtSifra.Text);
            if (id == 0)
                status = true;

            int lokacijaId = HijerarhijaSkladista.IzabraniId(cboLokacija);
            int tipId = HijerarhijaSkladista.IzabraniId(cboTipObjekta);
            bool aktivan = chkAktivan.Checked;
            decimal? lat = null, lon = null;

            string greska = HijerarhijaSkladista.ProveriOznaku(NivoSkladista.Objekat, status ? 0 : id, txtOznaka.Text);
            if (greska == null && txtNaziv.Text.Trim().Length == 0)
                greska = "Unesite naziv objekta.";
            if (greska == null && lokacijaId == 0)
                greska = "Izaberite lokaciju.";
            if (greska == null && tipId == 0)
                greska = "Izaberite tip objekta.";
            if (greska == null && !HijerarhijaSkladista.ProcitajKoordinatu(txtLatitude.Text, -90, 90, out lat))
                greska = "Latitude mora biti broj između -90 i 90.";
            if (greska == null && !HijerarhijaSkladista.ProcitajKoordinatu(txtLongitude.Text, -180, 180, out lon))
                greska = "Longitude mora biti broj između -180 i 180.";
            if (greska == null)
                greska = HijerarhijaSkladista.ProveriAktivnost(NivoSkladista.Objekat, status ? 0 : id, lokacijaId, aktivan);
            if (greska != null)
            {
                MessageBox.Show(greska, "Objekti", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // promena tipa objekta koji već ima polja: polja imaju podatke vezane za stari tip
            if (!status && ucitaniTip != 0 && ucitaniTip != tipId)
            {
                int polja = Convert.ToInt32(HijerarhijaSkladista.Skalar(
                    "SELECT COUNT(*) FROM dbo.Skladista WHERE SkladisteObjekatID = @ID", HijerarhijaSkladista.Param("@ID", id)));
                if (polja > 0 && MessageBox.Show("Objekat ima polja (" + polja + ") sa podacima vezanim za stari tip objekta "
                        + "(vrsta i kvalitet kontejnera, tip palete). Promeniti tip?", "Objekti",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;
            }

            try
            {
                var parametri = new[]
                {
                    HijerarhijaSkladista.Param("@SkladistaGrupaID", lokacijaId),
                    HijerarhijaSkladista.ParamTekst("@Oznaka", txtOznaka.Text, 30),
                    HijerarhijaSkladista.ParamTekst("@Naziv", txtNaziv.Text, 100),
                    HijerarhijaSkladista.Param("@TipSkladistaID", tipId),
                    HijerarhijaSkladista.ParamKoordinata("@Latitude", lat),
                    HijerarhijaSkladista.ParamKoordinata("@Longitude", lon),
                    HijerarhijaSkladista.Param("@Aktivan", aktivan ? 1 : 0),
                    HijerarhijaSkladista.Param("@Datum", DateTime.Now),
                    HijerarhijaSkladista.ParamTekst("@Korisnik", Kor, 20)
                };

                if (status)
                {
                    object noviId = HijerarhijaSkladista.Procedura("InsertSkladisteObjekat", parametri);
                    txtSifra.Text = noviId == null ? "" : noviId.ToString();
                    status = false;
                }
                else
                {
                    var sviParametri = new System.Collections.Generic.List<System.Data.SqlClient.SqlParameter>(parametri);
                    sviParametri.Insert(0, HijerarhijaSkladista.Param("@ID", id));
                    HijerarhijaSkladista.Procedura("UpdateSkladisteObjekat", sviParametri.ToArray());
                }
                ucitaniTip = tipId;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Objekti", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            RefreshDataGrid();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            int id = HijerarhijaSkladista.CeoBroj(txtSifra.Text);
            if (id == 0)
                return;

            string greska = HijerarhijaSkladista.ProveriBrisanje(NivoSkladista.Objekat, id);
            if (greska != null)
            {
                MessageBox.Show(greska, "Objekti", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Da li ste sigurni da želite da obrišete objekat?", "Izbor", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            try
            {
                HijerarhijaSkladista.Procedura("DeleteSkladisteObjekat", HijerarhijaSkladista.Param("@ID", id));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Objekti", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            tsNew_Click(sender, e);
            status = false;
            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            var select = "SELECT o.ID, o.Oznaka, o.Naziv, g.Oznaka AS LokacijaOznaka, g.Naziv AS Lokacija, z.Oznaka AS ZonaOznaka, "
                + " ts.Naziv AS TipObjekta, CASE WHEN o.Aktivan > 0 THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END AS Aktivan, o.Datum, o.Korisnik "
                + " FROM dbo.SkladisteObjekat o "
                + " LEFT JOIN dbo.SkladistaGrupa g ON g.ID = o.SkladistaGrupaID "
                + " LEFT JOIN dbo.Zone z ON z.ID = g.ZonaID "
                + " LEFT JOIN dbo.TipSkladista ts ON ts.ID = o.TipSkladistaID "
                + " ORDER BY o.ID";
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = HijerarhijaSkladista.Tabela(select);

            PodesiDatagridView(dataGridView1);

            Kolona("ID", "ID", 50);
            Kolona("Oznaka", "Oznaka", 100);
            Kolona("Naziv", "Naziv", 220);
            Kolona("LokacijaOznaka", "Lokacija oznaka", 100);
            Kolona("Lokacija", "Lokacija naziv", 200);
            Kolona("ZonaOznaka", "Zona oznaka", 90);
            Kolona("TipObjekta", "Tip objekta", 140);
            Kolona("Aktivan", "Aktivan", 70);
            Kolona("Datum", "Datum", 120);
            Kolona("Korisnik", "Korisnik", 100);
        }

        private void Kolona(string naziv, string naslov, int sirina)
        {
            DataGridViewColumn kolona = dataGridView1.Columns[naziv];
            if (kolona == null)
                return;
            kolona.HeaderText = naslov;
            kolona.Width = sirina;
        }

        private void VratiPodatke(int id)
        {
            DataTable t = HijerarhijaSkladista.Tabela(
                "SELECT ID, SkladistaGrupaID, Oznaka, Naziv, TipSkladistaID, Latitude, Longitude, Aktivan FROM dbo.SkladisteObjekat WHERE ID = @ID",
                HijerarhijaSkladista.Param("@ID", id));
            if (t.Rows.Count == 0)
                return;

            DataRow r = t.Rows[0];
            status = false;
            txtSifra.Text = r["ID"].ToString();
            txtOznaka.Text = r["Oznaka"].ToString();
            txtNaziv.Text = r["Naziv"].ToString();
            txtLatitude.Text = HijerarhijaSkladista.KoordinataTekst(r["Latitude"]);
            txtLongitude.Text = HijerarhijaSkladista.KoordinataTekst(r["Longitude"]);
            chkAktivan.Checked = HijerarhijaSkladista.CeoBroj(r["Aktivan"]) > 0;

            int lokacijaId = HijerarhijaSkladista.CeoBroj(r["SkladistaGrupaID"]);
            HijerarhijaSkladista.PostaviTrenutnog(lokacije, lokacijaId);
            cboLokacija.SelectedValue = lokacijaId;

            ucitaniTip = HijerarhijaSkladista.CeoBroj(r["TipSkladistaID"]);
            cboTipObjekta.SelectedValue = ucitaniTip;

            lblNadredjeni.Text = HijerarhijaSkladista.OpisNadredjenih(NivoSkladista.Objekat, lokacijaId);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Selected)
                    VratiPodatke(HijerarhijaSkladista.CeoBroj(dataGridView1.CurrentRow.Cells["ID"].Value));
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void cboLokacija_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblNadredjeni.Text = HijerarhijaSkladista.OpisNadredjenih(NivoSkladista.Objekat, HijerarhijaSkladista.IzabraniId(cboLokacija));
        }

        // Aktivne / Neaktivne: menja aktivnost izabranih redova, uz pravila za roditelja i decu
        private void PromeniAktivnost(bool aktivan)
        {
            var greske = new System.Text.StringBuilder();
            try
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    int id = HijerarhijaSkladista.CeoBroj(row.Cells["ID"].Value);
                    string greska = HijerarhijaSkladista.ProveriPromenuAktivnosti(NivoSkladista.Objekat, id, aktivan);
                    if (greska != null)
                    {
                        greske.AppendLine(row.Cells["Oznaka"].Value + ": " + greska);
                        continue;
                    }
                    HijerarhijaSkladista.Procedura("AktivnostSkladisteObjekat",
                        HijerarhijaSkladista.Param("@ID", id), HijerarhijaSkladista.Param("@Aktivan", aktivan ? 1 : 0));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Objekti", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (greske.Length > 0)
                MessageBox.Show(greske.ToString(), "Objekti", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
