using Saobracaj.TrackModal.Sifarnici;
using Syncfusion.Windows.Forms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Testiranje.Sifarnici
{
    public partial class frmSkladista : Form
    {
        public static string code = "frmSkladista";
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


        private void ChangeTextBox()
        {

            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
            panelHeader.Visible = false;


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
                this.Text = "POLJA";
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
        public frmSkladista()
        {
            InitializeComponent();
            ChangeTextBox();

        }

        public frmSkladista(string Korisnik)
        {
            InitializeComponent();
            ChangeTextBox();
            KorisnikCene = Korisnik;
            RefreshDataGrid();

        }
        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtSifra.Text = "";
            txtOznaka.Text = "";
            txtNaziv.Text = "";
            txtSKNaziv.Text = "";
            chkAktivna.Checked = true;
            if (cboVrstaPozicija.DataSource != null)
                cboVrstaPozicija.SelectedValue = HijerarhijaSkladista.KontejnerskePozicije;
            if (objekti != null)
                HijerarhijaSkladista.PostaviTrenutnog(objekti, 0);
            cboGrupaPolja.SelectedIndex = -1;
            lblNadredjeni.Text = "";
        }

        // Objekti za izbor (nivo III); polje upisuje objekat, a GrupaPoljaID se popunjava lokacijom objekta
        DataView objekti;

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
            int objekatId = HijerarhijaSkladista.IzabraniId(cboGrupaPolja);
            string greska = HijerarhijaSkladista.ProveriOznaku(NivoSkladista.Polje, id, txtOznaka.Text);
            if (greska == null && objekatId == 0)
                greska = "Izaberite objekat.";
            if (greska == null)
                greska = HijerarhijaSkladista.ProveriAktivnost(NivoSkladista.Polje, id, objekatId, tmpAktivna == 1);
            if (greska != null)
            {
                MessageBox.Show(greska, "Polja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool bioAktivan = id > 0 && HijerarhijaSkladista.JeAktivan(NivoSkladista.Polje, id);
            int lokacijaId = HijerarhijaSkladista.CeoBroj(HijerarhijaSkladista.Skalar(
                "SELECT SkladistaGrupaID FROM dbo.SkladisteObjekat WHERE ID = @ID", HijerarhijaSkladista.Param("@ID", objekatId)));

            try
            {
                if (status == true)
                {
                    InsertSkladista ins = new InsertSkladista();
                    ins.InsSkladista(txtNaziv.Text, Convert.ToDateTime(DateTime.Now), KorisnikCene, txtKapacitet.Text, lokacijaId, txtOznaka.Text, txtSKNaziv.Text, Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboTipKontejnera.SelectedValue), Convert.ToInt32(cboKvalitet.SelectedValue), Convert.ToInt32(cboTipPalete.SelectedValue), tmpAktivna);
                    id = HijerarhijaSkladista.IdPoOznaci(NivoSkladista.Polje, txtOznaka.Text);
                    txtSifra.Text = id.ToString();
                    status = false;
                }
                else
                {
                    //int TipCenovnika ,int Komitent, double Cena , int VrstaManipulacije ,DateTime  Datum , string Korisnik
                    InsertSkladista upd = new InsertSkladista();
                    upd.UpdSkladista(id, txtNaziv.Text, Convert.ToDateTime(DateTime.Now), KorisnikCene, txtKapacitet.Text, lokacijaId, txtOznaka.Text, txtSKNaziv.Text, Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboTipKontejnera.SelectedValue), Convert.ToInt32(cboKvalitet.SelectedValue), Convert.ToInt32(cboTipPalete.SelectedValue), tmpAktivna);
                }

                HijerarhijaSkladista.Procedura("UpdateSkladistaObjekat",
                    HijerarhijaSkladista.Param("@ID", id), HijerarhijaSkladista.Param("@SkladisteObjekatID", objekatId));

                // promena vrste pozicija (kontejnerske / paletne): aktivne postaju pozicije nove vrste, ostale neaktivne
                int vrstaPozicija = HijerarhijaSkladista.IzabraniId(cboVrstaPozicija);
                if (vrstaPozicija == 0)
                    vrstaPozicija = HijerarhijaSkladista.KontejnerskePozicije;
                int promenjenihPozicija = HijerarhijaSkladista.PostaviVrstuPozicijaPolja(id, vrstaPozicija);
                if (promenjenihPozicija > 0)
                    MessageBox.Show("Polje sada radi sa " + HijerarhijaSkladista.NazivVrstePozicija(vrstaPozicija)
                        + " pozicijama. Promenjena aktivnost pozicija: " + promenjenihPozicija + ".",
                        "Polja", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // promena aktivnosti polja prenosi se na pozicije
                HijerarhijaSkladista.PrenesiAktivnostPosleSnimanja(NivoSkladista.Polje, id, bioAktivan, tmpAktivna == 1, "Polja");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Polja", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            RefreshDataGrid();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            int id = HijerarhijaSkladista.CeoBroj(txtSifra.Text);
            if (id == 0)
                return;

            string greska = HijerarhijaSkladista.ProveriBrisanje(NivoSkladista.Polje, id);
            if (greska != null)
            {
                MessageBox.Show(greska, "Polja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da želite da brišete polje?", "Izbor", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                InsertSkladista upd = new InsertSkladista();
                upd.DeleteSkladista(id);
                RefreshDataGrid();
            }
        }

        private void Kolona(string naziv, string naslov, int sirina)
        {
            DataGridViewColumn kolona = dataGridView1.Columns[naziv];
            if (kolona == null)
                return;
            kolona.HeaderText = naslov;
            kolona.Width = sirina;
        }

        // Tip objekta određuje koja se polja vide: KONTEJNERSKO - vrsta i kvalitet kontejnera (tip palete CNT),
        // PALETNO - tip palete, SLOBODNO - ništa; skrivena polja dobijaju vrednost "/" (nema).
        // Naziv tipa se poredi po početku reči ("KONT", jer je u šifarniku upisano "KONTJENRSKO SKLADIŠTE").
        private void PrimeniTipObjekta(int objekatId)
        {
            string tip = objekatId == 0 ? "" : Convert.ToString(HijerarhijaSkladista.Skalar(
                "SELECT ts.Naziv FROM dbo.SkladisteObjekat o INNER JOIN dbo.TipSkladista ts ON ts.ID = o.TipSkladistaID WHERE o.ID = @ID",
                HijerarhijaSkladista.Param("@ID", objekatId))).ToUpperInvariant();
            bool kontejnersko = tip.Contains("KONT");
            bool paletno = tip.Contains("PALET");
            bool slobodno = tip.Contains("SLOBOD");
            bool poznat = kontejnersko || paletno || slobodno;

            bool vidiKontejner = kontejnersko || !poznat;
            bool vidiPalete = paletno || !poznat;
            label7.Visible = cboTipKontejnera.Visible = vidiKontejner;
            label21.Visible = cboKvalitet.Visible = vidiKontejner;
            label8.Visible = cboTipPalete.Visible = vidiPalete;

            if (kontejnersko)
                IzaberiPoNazivu(cboTipPalete, "CNT");
            if (paletno || slobodno)
            {
                IzaberiPoNazivu(cboTipKontejnera, "/");
                IzaberiPoNazivu(cboKvalitet, "/");
            }
            if (slobodno)
                IzaberiPoNazivu(cboTipPalete, "/");
        }

        private static void IzaberiPoNazivu(ComboBox cbo, string naziv)
        {
            DataTable t = cbo.DataSource as DataTable;
            if (t == null)
                return;
            foreach (DataRow r in t.Rows)
            {
                if (string.Equals(Convert.ToString(r[cbo.DisplayMember]).Trim(), naziv, StringComparison.OrdinalIgnoreCase))
                {
                    cbo.SelectedValue = r[cbo.ValueMember];
                    return;
                }
            }
        }

        private void cboGrupaPolja_SelectedIndexChanged(object sender, EventArgs e)
        {
            int objekatId = HijerarhijaSkladista.IzabraniId(cboGrupaPolja);
            lblNadredjeni.Text = HijerarhijaSkladista.OpisNadredjenih(NivoSkladista.Polje, objekatId);
            PrimeniTipObjekta(objekatId);
        }

        // Aktivne / Neaktivne: izabrani zapisi i svi njihovi podređeni zapisi (aktivnost u dubinu)
        private void PromeniAktivnost(bool aktivan)
        {
            HijerarhijaSkladista.PromeniAktivnostIzabranih(dataGridView1, NivoSkladista.Polje, aktivan, "Polja");
            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            var select = " SELECT Skladista.[ID] ,Skladista.Oznaka, Skladista.SkNaziv, Skladista.Naziv, SkladisteObjekat.Oznaka as ObjekatOznaka, SkladisteObjekat.Naziv as Objekat, " +
                " TipSkladista.Naziv as TipObjekta, SkladistaGrupa.Oznaka as LokacijaOznaka,SkladistaGrupa.Naziv as Lokacija, " +
                "  Partnerji.PaNaziv as Brodar, TipKontenjera.Naziv as TipKontejnera, " +
                            " uvKvalitetKontejnera.Naziv as KValitetKontejnera, TipPalete.Naziv as TipPalete, " +
                            " CASE WHEN Skladista.VrstaPozicija = 2 THEN 'Paletne' ELSE 'Kontejnerske' END as VrstaPozicija, " +
                            " CASE WHEN Skladista.Aktivan > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Aktivna, Kapacitet" +
                            " FROM [dbo].[Skladista] " +
                            " left join SkladisteObjekat on SkladisteObjekat.ID = Skladista.SkladisteObjekatID " +
                            " left join TipSkladista on TipSkladista.ID = SkladisteObjekat.TipSkladistaID " +
                            " left join SkladistaGrupa on SkladistaGrupa.ID = ISNULL(SkladisteObjekat.SkladistaGrupaID, Skladista.GrupaPoljaID) " +
                            " left join Partnerji on PArtnerji.PaSifra = Skladista.Brodar " +
                            " left join TipKontenjera on TipKontenjera.ID = Skladista.TipKontejnera " +
                            " left join uvKvalitetKontejnera on uvKvalitetKontejnera.ID = Skladista.KvalitetKontejnera " +
                            " left join TipPalete on TipPalete.ID = Skladista.TipPalete " +
                            " order by Skladista.ID";
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView1);

            Kolona("ID", "ID", 50);
            Kolona("Oznaka", "Šifra", 100);
            Kolona("SkNaziv", "SkNaziv", 80);
            Kolona("Naziv", "Naziv", 200);
            Kolona("ObjekatOznaka", "Objekat šifra", 100);
            Kolona("Objekat", "Objekat naziv", 180);
            Kolona("TipObjekta", "Tip objekta", 120);
            Kolona("LokacijaOznaka", "Lokacija šifra", 100);
            Kolona("Lokacija", "Lokacija", 180);
            Kolona("Brodar", "Brodar", 180);
            Kolona("TipKontejnera", "Tip kontejnera", 100);
            Kolona("KValitetKontejnera", "Kvalitet kontejnera", 150);
            Kolona("TipPalete", "Tip palete", 120);
            Kolona("VrstaPozicija", "Pozicije", 100);
            Kolona("Aktivna", "Aktivan", 80);
        }

        private void VratiPodatke(string ID)
        {
            // konstruktor puni grid pre Load-a, kada liste još nisu napunjene
            if (objekti == null)
                return;

            DataTable t =HijerarhijaSkladista.Tabela("SELECT * FROM dbo.Skladista WHERE ID = @ID",
                HijerarhijaSkladista.Param("@ID", HijerarhijaSkladista.CeoBroj(ID)));
            if (t.Rows.Count == 0)
                return;
            DataRow dr = t.Rows[0];

            status = false;
            txtNaziv.Text = dr["Naziv"].ToString();
            txtOznaka.Text = dr["Oznaka"].ToString();
            txtKapacitet.Text = dr["Kapacitet"].ToString();
            cboBrodar.SelectedValue = HijerarhijaSkladista.CeoBroj(dr["Brodar"]);
            cboTipKontejnera.SelectedValue = HijerarhijaSkladista.CeoBroj(dr["TipKontejnera"]);
            cboKvalitet.SelectedValue = HijerarhijaSkladista.CeoBroj(dr["KvalitetKontejnera"]);
            cboTipPalete.SelectedValue = HijerarhijaSkladista.CeoBroj(dr["TipPalete"]);
            txtSKNaziv.Text = dr["SkNaziv"].ToString();
            chkAktivna.Checked = dr["Aktivan"].ToString() == "1";
            int vrsta = HijerarhijaSkladista.CeoBroj(dr["VrstaPozicija"]);
            cboVrstaPozicija.SelectedValue = vrsta == HijerarhijaSkladista.PaletnePozicije
                ? HijerarhijaSkladista.PaletnePozicije : HijerarhijaSkladista.KontejnerskePozicije;

            // objekat se postavlja posle vrednosti polja, jer izbor objekta zaključava polja po tipu objekta
            int objekatId = HijerarhijaSkladista.CeoBroj(dr["SkladisteObjekatID"]);
            HijerarhijaSkladista.PostaviTrenutnog(objekti, objekatId);
            if (objekatId > 0)
                cboGrupaPolja.SelectedValue = objekatId;
            else
                cboGrupaPolja.SelectedIndex = -1;
            cboGrupaPolja_SelectedIndexChanged(cboGrupaPolja, EventArgs.Empty);
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
                        VratiPodatke(txtSifra.Text);
                        // txtOpis.Text = row.Cells[1].Value.ToString();
                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void tsPrvi_Click(object sender, EventArgs e)
        {

            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Min([ID]) as ID from Skladista", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtSifra.Text = dr["ID"].ToString();
            }
            VratiPodatke(txtSifra.Text);
            con.Close();

        }

        private void tsPoslednja_Click(object sender, EventArgs e)
        {


            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Max([ID]) as ID from Skladista", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtSifra.Text = dr["ID"].ToString();
            }
            VratiPodatke(txtSifra.Text);
            con.Close();



        }

        private void tsNazad_Click(object sender, EventArgs e)
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            int prvi = 0;
            con.Open();

            SqlCommand cmd = new SqlCommand("select top 1 ID as ID from Skladista where ID <" + Convert.ToInt32(txtSifra.Text) + " Order by ID desc", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                prvi = Convert.ToInt32(dr["ID"].ToString());
                txtSifra.Text = prvi.ToString();
            }

            con.Close();
            if ((Convert.ToInt32(txtSifra.Text) - 1) > prvi)
                VratiPodatke((Convert.ToInt32(txtSifra.Text) - 1).ToString());
            else
                VratiPodatke((Convert.ToInt32(prvi)).ToString());
        }

        private void VratiPodatkeMax()
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Max([ID]) as ID from Skladista", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtSifra.Text = dr["ID"].ToString();
            }

            con.Close();
        }

        private void tsNapred_Click(object sender, EventArgs e)
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            int zadnji = 0;
            con.Open();

            SqlCommand cmd = new SqlCommand("select top 1 ID as ID from Skladista where ID >" + Convert.ToInt32(txtSifra.Text) + " Order by ID", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                zadnji = Convert.ToInt32(dr["ID"].ToString());
                txtSifra.Text = zadnji.ToString();
            }

            con.Close();

            if ((Convert.ToInt32(txtSifra.Text) + 1) == zadnji)
                VratiPodatke((Convert.ToInt32(zadnji).ToString()));
            else
                VratiPodatke((Convert.ToInt32(txtSifra.Text) + 1).ToString());

        }

        private void tsNew_Click_1(object sender, EventArgs e)
        {
            tsNew_Click(sender, e);
            txtSifra.Enabled = false;
        }

        private void frmSkladista_Load(object sender, EventArgs e)
        {
            cboVrstaPozicija.DataSource = HijerarhijaSkladista.VrstePozicija();
            cboVrstaPozicija.DisplayMember = "Naziv";
            cboVrstaPozicija.ValueMember = "ID";
            cboVrstaPozicija.SelectedValue = HijerarhijaSkladista.KontejnerskePozicije;

            objekti = HijerarhijaSkladista.ListaRoditelja(NivoSkladista.Objekat);
            HijerarhijaSkladista.PoveziListu(cboGrupaPolja, objekti);
            cboGrupaPolja.SelectionChangeCommitted += cboGrupaPolja_SelectedIndexChanged;



            var dir2 = "Select PaSifra,  PaNaziv from Partnerji where Brodar = 1";
            var dirAD2 = new SqlDataAdapter(dir2, connection);
            var dirDS2 = new System.Data.DataSet();
            dirAD2.Fill(dirDS2);
            cboBrodar.DataSource = dirDS2.Tables[0];
            cboBrodar.DisplayMember = "PaNaziv";
            cboBrodar.ValueMember = "PaSifra";


            var partner22 = "SELECT ID, Naziv  FROM uvKvalitetKontejnera order by Naziv";
            var partAD22 = new SqlDataAdapter(partner22, connection);
            var partDS22 = new DataSet();
            partAD22.Fill(partDS22);
            cboKvalitet.DataSource = partDS22.Tables[0];
            cboKvalitet.DisplayMember = "Naziv";
            cboKvalitet.ValueMember = "ID";


            var mu = "select ID, SkNaziv from TipKontenjera order by SkNaziv";
            var muAD = new SqlDataAdapter(mu, connection);
            var muDS = new DataSet();
            muAD.Fill(muDS);
            cboTipKontejnera.DataSource = muDS.Tables[0];
            cboTipKontejnera.DisplayMember = "SkNaziv";
            cboTipKontejnera.ValueMember = "ID";


            var it = "select ID, Naziv from TipPalete order by Naziv";
            var itAD = new SqlDataAdapter(it, connection);
            var itDS = new DataSet();
            itAD.Fill(itDS);
            cboTipPalete.DataSource = itDS.Tables[0];
            cboTipPalete.DisplayMember = "Naziv";
            cboTipPalete.ValueMember = "ID";






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
