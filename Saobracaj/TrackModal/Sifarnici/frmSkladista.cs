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
            if (status == true)
            {
                InsertSkladista ins = new InsertSkladista();
                ins.InsSkladista(txtNaziv.Text, Convert.ToDateTime(DateTime.Now), KorisnikCene, txtKapacitet.Text, Convert.ToInt32(cboGrupaPolja.SelectedValue), txtOznaka.Text, txtSKNaziv.Text, Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboTipKontejnera.SelectedValue), Convert.ToInt32(cboKvalitet.SelectedValue),Convert.ToInt32(cboTipPalete.SelectedValue), tmpAktivna);
                status = false;
            }
            else
            {
                //int TipCenovnika ,int Komitent, double Cena , int VrstaManipulacije ,DateTime  Datum , string Korisnik
                InsertSkladista upd = new InsertSkladista();
                upd.UpdSkladista(Convert.ToInt32(txtSifra.Text), txtNaziv.Text, Convert.ToDateTime(DateTime.Now), KorisnikCene, txtKapacitet.Text, Convert.ToInt32(cboGrupaPolja.SelectedValue), txtOznaka.Text, txtSKNaziv.Text, Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboTipKontejnera.SelectedValue), Convert.ToInt32(cboKvalitet.SelectedValue), Convert.ToInt32(cboTipPalete.SelectedValue), tmpAktivna);
            }
            RefreshDataGrid();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da želite da brišete, obrisaće se i pozicije?", "Izbor", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                InsertSkladista upd = new InsertSkladista();
                upd.DeleteSkladista(Convert.ToInt32(txtSifra.Text));
                RefreshDataGrid();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }


        }

        private void RefreshDataGrid()
        {
            var select = " SELECT Skladista.[ID] ,Skladista.Oznaka, Skladista.SkNaziv, Skladista.Naziv, SkladistaGrupa.Oznaka as LokacijaOznaka,SkladistaGrupa.Naziv as Lokacija, " +
                "  Partnerji.PaNaziv as Brodar, TipKontenjera.Naziv as TipKontejnera, " +
                            " uvKvalitetKontejnera.Naziv as KValitetKontejnera, TipPalete.Naziv as TipPalete, CASE WHEN Skladista.Aktivan > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Aktivna, Kapacitet" +
                            " FROM [dbo].[Skladista] " +
                            " inner join SkladistaGrupa on Skladista.GrupaPoljaID = SkladistaGrupa.ID " +
                            " inner join Partnerji on PArtnerji.PaSifra = Skladista.Brodar " +
                            " inner join TipKontenjera on TipKontenjera.ID = Skladista.TipKontejnera " +
                            " inner join uvKvalitetKontejnera on uvKvalitetKontejnera.ID = Skladista.KvalitetKontejnera " +
                            " inner join TipPalete on TipPalete.ID = Skladista.TipPalete " +
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

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Oznaka";
            dataGridView1.Columns[1].Width = 100;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "SkNaziv";
            dataGridView1.Columns[2].Width = 80;


            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Naziv";
            dataGridView1.Columns[3].Width = 200;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Lokacija oznaka";
            dataGridView1.Columns[4].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Lokacija";
            dataGridView1.Columns[5].Width = 200;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Brodar";
            dataGridView1.Columns[6].Width = 200;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Tip kontejnera";
            dataGridView1.Columns[7].Width = 100;


            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Kvalitet kontejnera";
            dataGridView1.Columns[8].Width = 150;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Tip palete";
            dataGridView1.Columns[9].Width = 150;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Aktivan";
            dataGridView1.Columns[10].Width = 80;


        }

        private void VratiPodatke(string ID)
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT Skladista.[ID] ,Skladista.Naziv,Skladista.SkNaziv, Skladista.Oznaka, Skladista.Kapacitet, " +
                " SkladistaGrupa.ID as Lokacija,  Partnerji.PaSifra as Brodar, TipKontenjera.ID as TipKontejnera, uvKvalitetKontejnera.ID as KValitetKontejnera, TipPalete.ID as TipPalete, [Skladista].Aktivan FROM [dbo].[Skladista] inner join SkladistaGrupa on Skladista.GrupaPoljaID = SkladistaGrupa.ID inner join Partnerji on PArtnerji.PaSifra = Skladista.Brodar " +
                "inner join TipKontenjera on TipKontenjera.ID = Skladista.TipKontejnera " +
                "inner join uvKvalitetKontejnera on uvKvalitetKontejnera.ID = Skladista.KvalitetKontejnera inner join TipPalete on TipPalete.ID = Skladista.TipPalete where Skladista.ID=" + txtSifra.Text, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                // Convert.ToInt32(cboTipCenovnika.SelectedValue), Convert.ToInt32(cboKomitent.SelectedValue), Convert.ToDouble(txtCena.Text), Convert.ToInt32(cboVrstaManipulacije.SelectedValue), Convert.ToDateTime(DateTime.Now), KorisnikCene
                txtNaziv.Text = dr["Naziv"].ToString();
                txtOznaka.Text = dr["Oznaka"].ToString();
                txtKapacitet.Text = dr["Kapacitet"].ToString();
                cboGrupaPolja.SelectedValue = Convert.ToInt32(dr["Lokacija"].ToString());
                cboBrodar.SelectedValue = Convert.ToInt32(dr["Brodar"].ToString());
                cboTipKontejnera.SelectedValue = Convert.ToInt32(dr["TipKontejnera"].ToString());
                cboKvalitet.SelectedValue = Convert.ToInt32(dr["KValitetKontejnera"].ToString());
                cboTipPalete.SelectedValue = Convert.ToInt32(dr["TipPalete"].ToString());
                txtSKNaziv.Text = dr["SkNaziv"].ToString();

                if (dr["Aktivan"].ToString() == "1")
                {
                    chkAktivna.Checked = true;
                }
                else
                {
                    chkAktivna.Checked = false;
                }
            }

            con.Close();
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
            status = true;
            txtSifra.Enabled = false;
            txtNaziv.Text = "";
        }

        private void frmSkladista_Load(object sender, EventArgs e)
        {
            var select4 = "  select ID, Naziv from SkladistaGrupa order by ID";
            var s_connection4 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection4 = new SqlConnection(s_connection4);
            var c4 = new SqlConnection(s_connection4);
            var dataAdapter4 = new SqlDataAdapter(select4, c4);

            var commandBuilder4 = new SqlCommandBuilder(dataAdapter4);
            var ds4 = new DataSet();
            dataAdapter4.Fill(ds4);
            cboGrupaPolja.DataSource = ds4.Tables[0];
            cboGrupaPolja.DisplayMember = "Naziv";
            cboGrupaPolja.ValueMember = "ID";



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
            try
            {
                InsertSkladista poz = new InsertSkladista();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {

                        poz.ProglasiAktivnim(Convert.ToInt32(row.Cells[0].Value.ToString()), 0);
                        // txtOpis.Text = row.Cells[1].Value.ToString();
                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                InsertSkladista poz = new InsertSkladista();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {

                        poz.ProglasiNeAktivnim(Convert.ToInt32(row.Cells[0].Value.ToString()), 0);
                        // txtOpis.Text = row.Cells[1].Value.ToString();
                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }
    }
}
