using Saobracaj.TrackModal.Sifarnici;
using Syncfusion.Windows.Forms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Testiranje.Sifarnici
{
    public partial class frmPozicija : Form
    {
        public static string code = "frmPozicija";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Saobracaj.Sifarnici.frmLogovanje.user.ToString();
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


        public frmPozicija()
        {
            InitializeComponent();
            ChangeTextBox();

        }

        public frmPozicija(string Korisnik)
        {
            InitializeComponent();
            KorisnikCene = Korisnik;
            ChangeTextBox();

        }
        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtSifra.Text = "";
            txtSifra.Enabled = false;
            txtOznaka.Text = "";
            txtOpis.Text = "";
            txtNamena.Text = "";
            chkAktivna.Checked = true;
            if (cboVrstaPozicija.DataSource != null)
                cboVrstaPozicija.SelectedValue = HijerarhijaSkladista.KontejnerskePozicije;
            if (polja != null)
                HijerarhijaSkladista.PostaviTrenutnog(polja, 0);
            cboSkladiste.SelectedIndex = -1;
            lblNadredjeni.Text = "";
        }

        // Polja za izbor (nivo IV)
        DataView polja;

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
            int poljeId = HijerarhijaSkladista.IzabraniId(cboSkladiste);
            string greska = HijerarhijaSkladista.ProveriOznaku(NivoSkladista.Pozicija, id, txtOznaka.Text);
            if (greska == null && poljeId == 0)
                greska = "Izaberite polje.";
            int vrstaPozicije = HijerarhijaSkladista.IzabraniId(cboVrstaPozicija);
            if (vrstaPozicije == 0)
                vrstaPozicije = HijerarhijaSkladista.KontejnerskePozicije;
            if (greska == null)
                greska = HijerarhijaSkladista.ProveriAktivnost(NivoSkladista.Pozicija, id, poljeId, tmpAktivna == 1);
            // aktivna pozicija mora biti iste vrste (kontejnerska / paletna) kao polje
            if (greska == null && tmpAktivna == 1)
                greska = HijerarhijaSkladista.ProveriVrstuPozicije(poljeId, vrstaPozicije);
            if (greska != null)
            {
                MessageBox.Show(greska, "Pozicije", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (status == true)
                {
                    InsertPozicija ins = new InsertPozicija();
                    ins.InsPozicija(poljeId, txtOznaka.Text, txtOpis.Text, txtNamena.Text, Convert.ToDateTime(DateTime.Now), Kor, tmpAktivna);
                    id = HijerarhijaSkladista.IdPoOznaci(NivoSkladista.Pozicija, txtOznaka.Text);
                    txtSifra.Text = id.ToString();
                    status = false;
                }
                else
                {
                    if (txtOpis.Text == "")
                    { txtOpis.Text = " "; }
                    //int TipCenovnika ,int Komitent, double Cena , int VrstaManipulacije ,DateTime  Datum , string Korisnik
                    InsertPozicija upd = new InsertPozicija();
                    upd.UpdPozicija(id, poljeId, txtOznaka.Text, txtOpis.Text, txtNamena.Text, Convert.ToDateTime(DateTime.Now), Kor, tmpAktivna);
                    status = false;
                }

                // pun naziv (do 100) i skraćeni naziv (do 20); postojeće procedure mogu imati kraće parametre
                HijerarhijaSkladista.Procedura("UpdatePozicijaNazivi",
                    HijerarhijaSkladista.Param("@ID", id),
                    HijerarhijaSkladista.ParamTekst("@Opis", txtOpis.Text, 100),
                    HijerarhijaSkladista.ParamTekst("@Namena", txtNamena.Text, 20),
                    HijerarhijaSkladista.Param("@VrstaPozicija", vrstaPozicije));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Pozicije", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            RefreshDataGrid();
        }

        private void cboSkladiste_SelectedIndexChanged(object sender, EventArgs e)
        {
            int poljeId = HijerarhijaSkladista.IzabraniId(cboSkladiste);
            lblNadredjeni.Text = HijerarhijaSkladista.OpisNadredjenih(NivoSkladista.Pozicija, poljeId);

            // nova pozicija dobija vrstu pozicija izabranog polja
            if (status && poljeId > 0)
                cboVrstaPozicija.SelectedValue = HijerarhijaSkladista.VrstaPozicijaPolja(poljeId);
        }

        private void Kolona(string naziv, string naslov, int sirina)
        {
            SifarnikGrid.Kolona(gridGroupingControl1, naziv, naslov, sirina);
        }

        // Aktivne / Neaktivne: izabrani zapisi i svi njihovi podređeni zapisi (aktivnost u dubinu)
        private void PromeniAktivnost(bool aktivan)
        {
            HijerarhijaSkladista.PromeniAktivnostIzabranih(gridGroupingControl1, NivoSkladista.Pozicija, aktivan, "Pozicije");
            RefreshDataGrid();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da želite da brišete?", "Izbor", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                InsertPozicija upd = new InsertPozicija();
                upd.DeletePozicija(Convert.ToInt32(txtSifra.Text));
                RefreshDataGrid();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }


        }

        private void RefreshDataGrid()
        {
            var select = "SELECT Pozicija.ID, Pozicija.Oznaka, Pozicija.Namena as SkNaziv,Pozicija.Opis as Naziv,  Skladista.Naziv as Polje, " +
                " SkladisteObjekat.Naziv as Objekat, SkladistaGrupa.Naziv as Lokacija, " +
                " CASE WHEN Pozicija.VrstaPozicija = 2 THEN 'Paletna' ELSE 'Kontejnerska' END as VrstaPozicija, CASE WHEN Pozicija.Aktivna > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Aktivna, " +
                " Pozicija.Datum, Pozicija.Korisnik from Pozicija inner join Skladista on Pozicija.Skladiste= Skladista.ID " +
                " left join SkladisteObjekat on SkladisteObjekat.ID = Skladista.SkladisteObjekatID " +
                " left join SkladistaGrupa on SkladistaGrupa.ID = ISNULL(SkladisteObjekat.SkladistaGrupaID, Skladista.GrupaPoljaID) " +
                " order by Pozicija.ID";
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            SifarnikGrid.Napuni(gridGroupingControl1, ds.Tables[0]);

            Kolona("ID", "ID", 50);
            Kolona("Oznaka", "Šifra", 100);
            Kolona("SkNaziv", "SkNaziv", 100);
            Kolona("Naziv", "Naziv", 250);
            Kolona("Polje", "Polje", 180);
            Kolona("Objekat", "Objekat", 180);
            Kolona("Lokacija", "Lokacija", 180);
            Kolona("VrstaPozicija", "Vrsta", 100);
            Kolona("Aktivna", "Aktivna", 60);
            Kolona("Datum", "Datum", 100);
            Kolona("Korisnik", "Korisnik", 100);
        }

        private void VratiPodatke(string ID)
        {
            if (polja == null)
                return;

            DataTable t = HijerarhijaSkladista.Tabela("SELECT Skladiste, Oznaka, Opis, Namena, Aktivna, VrstaPozicija FROM dbo.Pozicija WHERE ID = @ID",
                HijerarhijaSkladista.Param("@ID", HijerarhijaSkladista.CeoBroj(ID)));
            if (t.Rows.Count == 0)
                return;
            DataRow dr = t.Rows[0];

            status = false;
            int poljeId = HijerarhijaSkladista.CeoBroj(dr["Skladiste"]);
            HijerarhijaSkladista.PostaviTrenutnog(polja, poljeId);
            cboSkladiste.SelectedValue = poljeId;
            txtOznaka.Text = dr["Oznaka"].ToString();
            txtOpis.Text = dr["Opis"].ToString();
            txtNamena.Text = dr["Namena"].ToString();
            chkAktivna.Checked = dr["Aktivna"].ToString() == "1";
            cboVrstaPozicija.SelectedValue = HijerarhijaSkladista.CeoBroj(dr["VrstaPozicija"]) == HijerarhijaSkladista.PaletnePozicije
                ? HijerarhijaSkladista.PaletnePozicije : HijerarhijaSkladista.KontejnerskePozicije;
            lblNadredjeni.Text = HijerarhijaSkladista.OpisNadredjenih(NivoSkladista.Pozicija, poljeId);
        }

        // izabrani red u gridu puni polja forme
        private void gridGroupingControl1_SelectedRecordsChanged(object sender, Syncfusion.Grouping.SelectedRecordsChangedEventArgs e)
        {
            int id = SifarnikGrid.IzabraniId(gridGroupingControl1);
            if (id == 0)
                return;
            txtSifra.Text = id.ToString();
            VratiPodatke(txtSifra.Text);
        }

        private void frmPozicija_Load(object sender, EventArgs e)
        {
            // dugme za pregled cele hijerarhije (Zona -> Lokacija -> Objekat -> Polje -> Pozicija)
            frmPregledSkladistaGrupa.DodajDugme(panelHeader, meniHeader);
            cboVrstaPozicija.DataSource = HijerarhijaSkladista.VrstePozicija();
            cboVrstaPozicija.DisplayMember = "Naziv";
            cboVrstaPozicija.ValueMember = "ID";
            cboVrstaPozicija.SelectedValue = HijerarhijaSkladista.KontejnerskePozicije;

            polja = HijerarhijaSkladista.ListaRoditelja(NivoSkladista.Polje);
            HijerarhijaSkladista.PoveziListu(cboSkladiste, polja);
            cboSkladiste.SelectionChangeCommitted += cboSkladiste_SelectedIndexChanged;

            RefreshDataGrid();
            //  if (KorisnikCene != "Kecman" && KorisnikCene != "M.Jelisavčić" && KorisnikCene != "Dušan Bašanović")
            //  {

            // }
        }

        private void btnPretraziPoSkladistu_Click(object sender, EventArgs e)
        {
            var select = " SELECT Pozicija.ID, Pozicija.Skladiste, Skladista.Naziv , Pozicija.Oznaka,  Pozicija.Opis,  Pozicija.Namena, Pozicija.Datum, Pozicija.Korisnik from Pozicija inner join Skladista on Pozicija.Skladiste= Skladista.ID where Skladiste = " + Convert.ToInt32(cboSkladiste.SelectedValue);
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            SifarnikGrid.Napuni(gridGroupingControl1, ds.Tables[0]);

            Kolona("ID", "ID", 50);
            Kolona("Skladiste", "Polje ID", 60);
            Kolona("Naziv", "Polje", 150);
            Kolona("Oznaka", "Šifra", 100);
            Kolona("Opis", "Naziv", 200);
            Kolona("Namena", "SkNaziv", 100);
            Kolona("Datum", "Datum", 100);
            Kolona("Korisnik", "Korisnik", 100);
        }

        private void tsPrvi_Click(object sender, EventArgs e)
        {

            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Min([ID]) as ID from Pozicija", con);
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

            SqlCommand cmd = new SqlCommand("select Max([ID]) as ID from Pozicija", con);
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

            SqlCommand cmd = new SqlCommand("select top 1 ID as ID from Pozicija where ID <" + Convert.ToInt32(txtSifra.Text) + " Order by ID desc", con);
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

            SqlCommand cmd = new SqlCommand("select Max([ID]) as ID from Pozicija", con);
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

            SqlCommand cmd = new SqlCommand("select top 1 ID as ID from Pozicija where ID >" + Convert.ToInt32(txtSifra.Text) + " Order by ID", con);

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
