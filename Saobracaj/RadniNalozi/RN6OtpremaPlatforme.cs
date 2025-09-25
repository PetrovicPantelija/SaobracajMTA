using Syncfusion.Windows.Forms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
//
namespace Saobracaj.RadniNalozi
{
    public partial class RN6OtpremaPlatforme : Form
    {
        private string connect = Sifarnici.frmLogovanje.connectionString;
        private bool status = false;
        string KorisnikTekuci = Sifarnici.frmLogovanje.user;
        int BrojRN = 0;
        private void ChangeTextBox()
        {
            this.BackColor = Color.White;
            this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
            this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
            Office2010Colors.ApplyManagedColors(this, Color.White);
            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
            panelHeader.Visible = false;
            this.ControlBox = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                meniHeader.Visible = false;
                panelHeader.Visible = true;
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                tabSplitterContainer1.BackColor = Color.White;
                Office2010Colors.ApplyManagedColors(this, Color.White);




                foreach (Control control in tabSplitterPage1.Controls)
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
                meniHeader.Visible = true;
                panelHeader.Visible = false;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                ChangeTextBox();
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
        public RN6OtpremaPlatforme()
        {
            InitializeComponent();
            FillGV();
            FillCombo();
            ChangeTextBox();
        }

        public RN6OtpremaPlatforme(int brojrn)
        {
            InitializeComponent();
            BrojRN = brojrn;
            FillGVIZRNI();
            FillCombo();
            ChangeTextBox();
        }

        private void VratiOstaloIzTekuceg(string BrojKontejnera)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT TipKontejnera, Skladiste, Pozicija " +
  " FROM KontejnerTekuce where Kontejner= '" + BrojKontejnera + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                // txtID.Text = dr["ID"].ToString();
                cboVrstaKontejnera.SelectedValue = Convert.ToInt32(dr["TipKontejnera"].ToString());
                cboSaSklad.SelectedValue = Convert.ToInt32(dr["Skladiste"].ToString());
                cboSaPoz.SelectedValue = Convert.ToInt32(dr["Skladiste"].ToString());
            }
            con.Close();




        }

        public RN6OtpremaPlatforme(string OtpremaID, string Korisnik, string Usluga, string Kamion, int Uvoz, string NalogID, string Kontejner)
        {
            //Uvoz = 0
            //Izvoz = 1
            InitializeComponent();


            FillCombo();
            ChangeTextBox();
            txtOtpremaID.Text = OtpremaID;
            txtNalogIzdao.Text = Korisnik;
            //cboUsluga.SelectedValue = Usluga;
            if (Uvoz == 0)
            {
                FillGV();
                NapuniVrstuUsluge(Usluga);
                VratiPodatkeVrstaMan(Usluga.ToString());
                chkUvoz.Checked = true;
                chkIzvoz.Checked = false;
                //Proveriti da li je ovo tacno


            }
            else
            {
                FillGVIzvozni();
                NapuniVrstuUsluge(Usluga);
                VratiPodatkeVrstaMan(Usluga.ToString());
                chkUvoz.Checked = false;
                chkIzvoz.Checked = true;
            }

            txtRegBr.Text = Kamion;
            KorisnikTekuci = Korisnik;
            txtNalogID.Text = NalogID;
            VratiOstaloIzTekuceg(Kontejner);
        }

        private void VratiPodatkeVrstaMan(string IDUsluge)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" Select VrstaManipulacije.ID from RadniNalogInterni inner join " +
   " VrstaManipulacije on RadniNalogInterni.IDManipulacijaJed = VrstaManipulacije.ID where RadniNalogInterni.ID = " + IDUsluge, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                cboUsluga.SelectedValue = Convert.ToInt32(dr["ID"].ToString());

            }

            con.Close();
        }

        private void NapuniVrstuUsluge(string IDUsluga)
        {
            SqlConnection conn = new SqlConnection(connect);
            var usluge = "Select VrstaManipulacije.ID,VrstaManipulacije.Naziv from RadniNalogInterni inner join " +
   " VrstaManipulacije on RadniNalogInterni.IDManipulacijaJed = VrstaManipulacije.ID where RadniNalogInterni.ID = " + IDUsluga;
            var daUsluge = new SqlDataAdapter(usluge, conn);
            var dsUsluge = new DataSet();
            daUsluge.Fill(dsUsluge);
            cboUsluga.DataSource = dsUsluge.Tables[0];
            cboUsluga.DisplayMember = "Naziv";
            cboUsluga.ValueMember = "ID";

            // cboUsluga.SelectedValue = Convert.ToInt32(IDUsluga);
        }
        private void FillGV()
        {

            var select = " SELECT       [RNOtpremaPlatforme].ID, [Kamion] ,[Zavrsen] ,[DatumRasporeda]   " +
                "   ,[BrojKontejnera]  ,TipKontenjera.NAziv as [VrstaKontejnera]      ,[NalogIzdao]   " +
" ,[DatumRealizacije], Komitenti_3.PaNaziv as [Uvoznik]    ,VrstaCarinskogPostupka.[Naziv] as CarinskiPostupak        , Komitenti_2.PaNaziv as [SpedicijaRTC] " +
" ,Komitenti_1.PaNaziv as [NazivBrodara]      ,[VrstaRobe]    ,[SaSkladista]      ,[SaPozicijeSklad] " +
" ,[IdUsluge]      ,[NalogRealizovao]    ,[OtpremaID] " +
" ,[NalogID]   FROM[dbo].[RNOtpremaPlatforme] " +
" INNER JOIN  Partnerji AS Komitenti_1 ON[RNOtpremaPlatforme].NazivBrodara = Komitenti_1.PaSifra " +
" INNER JOIN  Partnerji AS Komitenti_2 ON[RNOtpremaPlatforme].SpedicijaRTC = Komitenti_2.PaSifra " +
" INNER JOIN  Partnerji AS Komitenti_3 ON[RNOtpremaPlatforme].Uvoznik = Komitenti_3.PaSifra " +
" inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.id = [RNOtpremaPlatforme].CarinskiPostupak " +
" inner join  Skladista on[RNOtpremaPlatforme].[SaSkladista] = Skladista.ID " +
" inner join TipKontenjera on TipKontenjera.ID = [RNOtpremaPlatforme].[VrstaKontejnera] " +
" where Uvoz = 0 order by [RNOtpremaPlatforme].ID desc";
            SqlConnection conn = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView1);

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 20;
        }

        private void FillGVIZRNI()
        {

            var select = " SELECT       [RNOtpremaPlatforme].ID, [Kamion] ,[Zavrsen] ,[DatumRasporeda]   " +
                "   ,[BrojKontejnera]  ,TipKontenjera.NAziv as [VrstaKontejnera]      ,[NalogIzdao]   " +
" ,[DatumRealizacije], Komitenti_3.PaNaziv as [Uvoznik]    ,VrstaCarinskogPostupka.[Naziv] as CarinskiPostupak        , Komitenti_2.PaNaziv as [SpedicijaRTC] " +
" ,Komitenti_1.PaNaziv as [NazivBrodara]      ,[VrstaRobe]    ,[SaSkladista]      ,[SaPozicijeSklad] " +
" ,[IdUsluge]      ,[NalogRealizovao]    ,[OtpremaID] " +
" ,[NalogID]   FROM[dbo].[RNOtpremaPlatforme] " +
" INNER JOIN  Partnerji AS Komitenti_1 ON[RNOtpremaPlatforme].NazivBrodara = Komitenti_1.PaSifra " +
" INNER JOIN  Partnerji AS Komitenti_2 ON[RNOtpremaPlatforme].SpedicijaRTC = Komitenti_2.PaSifra " +
" INNER JOIN  Partnerji AS Komitenti_3 ON[RNOtpremaPlatforme].Uvoznik = Komitenti_3.PaSifra " +
" inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.id = [RNOtpremaPlatforme].CarinskiPostupak " +
" inner join  Skladista on[RNOtpremaPlatforme].[SaSkladista] = Skladista.ID " +
" inner join TipKontenjera on TipKontenjera.ID = [RNOtpremaPlatforme].[VrstaKontejnera] " +
" where [RNOtpremaPlatforme].ID = " + BrojRN + " order by [RNOtpremaPlatforme].ID desc";
            SqlConnection conn = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView1);

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 20;
        }

        private void FillGVIzvozni()
        {

            var select = " SELECT       [RNOtpremaPlatforme].ID, [Kamion] ,[Zavrsen] ,[DatumRasporeda]   " +
                 "   ,[BrojKontejnera]  ,TipKontenjera.NAziv as [VrstaKontejnera]      ,[NalogIzdao]   " +
 " ,[DatumRealizacije], Komitenti_3.PaNaziv as [Uvoznik]    ,VrstaCarinskogPostupka.[Naziv] as CarinskiPostupak        , Komitenti_2.PaNaziv as [SpedicijaRTC] " +
 " ,Komitenti_1.PaNaziv as [NazivBrodara]      ,[VrstaRobe]    ,[SaSkladista]      ,[SaPozicijeSklad] " +
 " ,[IdUsluge]      ,[NalogRealizovao]    ,[OtpremaID] " +
 " ,[NalogID]   FROM[dbo].[RNOtpremaPlatforme] " +
 " INNER JOIN  Partnerji AS Komitenti_1 ON [RNOtpremaPlatforme].NazivBrodara = Komitenti_1.PaSifra " +
 " INNER JOIN  Partnerji AS Komitenti_2 ON [RNOtpremaPlatforme].SpedicijaRTC = Komitenti_2.PaSifra " +
 " INNER JOIN  Partnerji AS Komitenti_3 ON [RNOtpremaPlatforme].Uvoznik = Komitenti_3.PaSifra " +
 " inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.id = [RNOtpremaPlatforme].CarinskiPostupak " +
 " inner join  Skladista on[RNOtpremaPlatforme].[SaSkladista] = Skladista.ID " +
 " inner join TipKontenjera on TipKontenjera.ID = [RNOtpremaPlatforme].[VrstaKontejnera] " +
 " where Uvoz = 1 order by [RNOtpremaPlatforme].ID desc";
            SqlConnection conn = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
           
            PodesiDatagridView(dataGridView1);

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 20;
        }

        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connect);

            var partner4 = "Select PaSifra,PaNaziv From Partnerji  order by PaSifra";
            var partAD4 = new SqlDataAdapter(partner4, conn);
            var partDS4 = new DataSet();
            partAD4.Fill(partDS4);
            cboSpedicija.DataSource = partDS4.Tables[0];
            cboSpedicija.DisplayMember = "PaNaziv";
            cboSpedicija.ValueMember = "PaSifra";

            var dir2 = "Select ID, (Oznaka + ' ' + Naziv) as Naziv from VrstaCarinskogPostupka order by Oznaka";
            var dirAD2 = new SqlDataAdapter(dir2, conn);
            var dirDS2 = new DataSet();
            dirAD2.Fill(dirDS2);
            cboPostupak.DataSource = dirDS2.Tables[0];
            cboPostupak.DisplayMember = "Naziv";
            cboPostupak.ValueMember = "ID";

            var TipKontenjera = "Select ID,SkNaziv from TipKontenjera order by ID";
            var daTK = new SqlDataAdapter(TipKontenjera, conn);
            var daDS = new DataSet();
            daTK.Fill(daDS);
            cboVrstaKontejnera.DataSource = daDS.Tables[0];
            cboVrstaKontejnera.DisplayMember = "SkNaziv";
            cboVrstaKontejnera.ValueMember = "ID";

            txtNalogIzdao.Text = Sifarnici.frmLogovanje.user.ToString().TrimEnd();
            //usluge->Manipulacije

            var vSredstvo = "select ID,(CONVERT(NVARCHAR,BrVoza) + ' /' +Relacija) As Opis from Voz order by ID desc";
            var daVS = new SqlDataAdapter(vSredstvo, conn);
            var dsVS = new DataSet();
            daVS.Fill(dsVS);
            cboNaSredstvo.DataSource = dsVS.Tables[0];
            cboNaSredstvo.DisplayMember = "Opis";
            cboNaSredstvo.ValueMember = "ID";

            var partner2 = "Select PaSifra,PaNaziv From Partnerji order by PaSifra";
            var partAD2 = new SqlDataAdapter(partner2, conn);
            var partDS2 = new DataSet();
            partAD2.Fill(partDS2);
            cboUvoznik.DataSource = partDS2.Tables[0];
            cboUvoznik.DisplayMember = "PaNaziv";
            cboUvoznik.ValueMember = "PaSifra";

            var dir22 = "Select ID, (Oznaka + ' ' + Naziv) as Naziv from VrstaCarinskogPostupka order by Oznaka";
            var dirAD22 = new SqlDataAdapter(dir22, conn);
            var dirDS22 = new DataSet();
            dirAD22.Fill(dirDS22);
            cboPostupak.DataSource = dirDS22.Tables[0];
            cboPostupak.DisplayMember = "Naziv";
            cboPostupak.ValueMember = "ID";

            var partner44 = "Select PaSifra,PaNaziv From Partnerji  order by PaSifra";
            var partAD44 = new SqlDataAdapter(partner44, conn);
            var partDS44 = new DataSet();
            partAD44.Fill(partDS44);
            cboSpedicija.DataSource = partDS44.Tables[0];
            cboSpedicija.DisplayMember = "PaNaziv";
            cboSpedicija.ValueMember = "PaSifra";

            var partner7 = "Select PaSifra,PaNaziv From Partnerji  order by PaSifra";
            var partAD7 = new SqlDataAdapter(partner7, conn);
            var partDS7 = new DataSet();
            partAD7.Fill(partDS7);
            cboBrodar.DataSource = partDS7.Tables[0];
            cboBrodar.DisplayMember = "PaNaziv";
            cboBrodar.ValueMember = "PaSifra";

            var roba = "Select ID,Naziv From NHM order by ID";
            var daRoba = new SqlDataAdapter(roba, conn);
            var dsRoba = new DataSet();
            daRoba.Fill(dsRoba);
            cboVrstaRobe.DataSource = dsRoba.Tables[0];
            cboVrstaRobe.DisplayMember = "Naziv";
            cboVrstaRobe.ValueMember = "ID";

            var usluge = "Select ID,Naziv from VrstaManipulacije order by ID";
            var daUsluge = new SqlDataAdapter(usluge, conn);
            var dsUsluge = new DataSet();
            daUsluge.Fill(dsUsluge);
            cboUsluga.DataSource = dsUsluge.Tables[0];
            cboUsluga.DisplayMember = "Naziv";
            cboUsluga.ValueMember = "ID";

            var sklad = "select ID,naziv from Skladista";
            var daSklad = new SqlDataAdapter(sklad, conn);
            var dsSklad = new DataSet();
            daSklad.Fill(dsSklad);
            cboSaSklad.DataSource = dsSklad.Tables[0];
            cboSaSklad.DisplayMember = "Naziv";
            cboSaSklad.ValueMember = "ID";

            var pozicija = "Select Id,Opis from Pozicija";
            var daPoz = new SqlDataAdapter(pozicija, conn);
            var dsPoz = new DataSet();
            daPoz.Fill(dsPoz);
            cboSaPoz.DataSource = dsPoz.Tables[0];
            cboSaPoz.DisplayMember = "Opis";
            cboSaPoz.ValueMember = "ID";

            var partner5 = "Select PaSifra,PaNaziv From Partnerji  order by PaSifra";
            var partAD5 = new SqlDataAdapter(partner5, conn);
            var partDS5 = new DataSet();
            partAD5.Fill(partDS5);
            cboIzvoznik.DataSource = partDS5.Tables[0];
            cboIzvoznik.DisplayMember = "PaNaziv";
            cboIzvoznik.ValueMember = "PaSifra";
        }
        private void txtIDinazivplaniraneusluge_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtUSkladiste_Click(object sender, EventArgs e)
        {

        }

        private void RN6OtpremaPlatforme_Load(object sender, EventArgs e)
        {

        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            InsertRN rn = new InsertRN();
            if (status == true)
            {
                rn.InsRNOtpremaPlatforme(Convert.ToDateTime(txtDatumRasporeda.Value), txtbrojkontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cboVrstaKontejnera.SelectedValue),
                    txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(txtDatumRealizacije.Value), Convert.ToInt32(cboNaSredstvo.SelectedValue),
                    Convert.ToInt32(cboUvoznik.SelectedValue), Convert.ToInt32(cboPostupak.SelectedValue), Convert.ToInt32(cboSpedicija.SelectedValue), Convert.ToInt32(cboBrodar.SelectedValue),
                    Convert.ToInt32(cboVrstaRobe.SelectedValue), Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue), Convert.ToInt32(cboUsluga.SelectedValue), "");
            }
            else
            {
                rn.UpdRNOtpremaPlatforme(Convert.ToInt32(txtID.Text), Convert.ToDateTime(txtDatumRasporeda.Value), txtbrojkontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cboVrstaKontejnera.SelectedValue),
                    txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(txtDatumRealizacije.Value), Convert.ToInt32(cboNaSredstvo.SelectedValue),
                    Convert.ToInt32(cboUvoznik.SelectedValue), Convert.ToInt32(cboPostupak.SelectedValue), Convert.ToInt32(cboSpedicija.SelectedValue), Convert.ToInt32(cboBrodar.SelectedValue),
                    Convert.ToInt32(cboVrstaRobe.SelectedValue), Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue),
                    Convert.ToInt32(cboUsluga.SelectedValue), txtNalogRealizovao.Text.ToString().TrimEnd());
            }
            FillGV();
            status = false;
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                InsertRN rn = new InsertRN();
                rn.DelRNOtpremaPlatforme(Convert.ToInt32(txtID.Text.ToString().TrimEnd()));
                FillGV();
            }
            else
            {
                MessageBox.Show("Izaberite broj zapisa");
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txtID.Text = row.Cells[0].Value.ToString();
                        VratiPodatkeStavke(txtID.Text);
                        FillDG2();
                        VratiSkladisteIzTekuceg(txtbrojkontejnera.Text);
                    }
                }
            }
            catch { }
        }

        private void VratiSkladisteIzTekuceg(string ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT       Skladiste " +
  "  FROM  KontejnerTekuce " +
             " where Kontejner = '" + ID + "'", con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                cboSaSklad.SelectedValue = Convert.ToString(dr["Skladiste"].ToString());



            }

            con.Close();
        }


        private void FillDG2()
        {

            var select = "  SELECT     UvozKonacnaNHM.ID, NHM.Broj, UvozKonacnaNHM.IDNHM, NHM.Naziv FROM NHM INNER JOIN " +
" UvozKonacnaNHM ON NHM.ID = UvozKonacnaNHM.IDNHM " +
" inner join RadniNalogInterni on UvozKonacnanhm.idnadredjena = RadniNalogInterni.BrojOsnov " +
" where RadniNalogInterni.ID = " + Convert.ToInt32(txtNalogID.Text) + " order by UvozKonacnanhm.ID desc";


            SqlConnection conn = new SqlConnection(connect);

            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView3);

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView3.Columns[0];
            dataGridView3.Columns[0].HeaderText = "ID";
            dataGridView3.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView3.Columns[1];
            dataGridView3.Columns[1].HeaderText = "NHM Broj";
            dataGridView3.Columns[1].Width = 70;

            DataGridViewColumn column3 = dataGridView3.Columns[2];
            dataGridView3.Columns[2].HeaderText = "ID";
            dataGridView3.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView3.Columns[3];
            dataGridView3.Columns[3].HeaderText = "NHM";
            dataGridView3.Columns[3].Width = 350;


        }

        private void VratiPodatkeStavke(string ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT       ID ,[DatumRasporeda]      ,[BrojKontejnera] " +
    " ,[VrstaKontejnera]      ,[NalogIzdao]      ,[DatumRealizacije]      ,[Uvoznik] " +
    "   ,[CarinskiPostupak]        ,[SpedicijaRTC]        ,[NazivBrodara]      ,[VrstaRobe] " +
    "   ,[SaSkladista]      ,[SaPozicijeSklad]      ,[IdUsluge]      ,[NalogRealizovao] " +
    "   ,[OtpremaID]      ,[Kamion]      ,[Zavrsen]      ,[NalogID], Uvoz, Izvoznik " +
  "  FROM [dbo].[RNOtpremaPlatforme] " +
             " where ID = " + txtID.Text, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                if (dr["Uvoz"].ToString() == "0")
                {
                    chkUvoz.Checked = true;
                    chkIzvoz.Checked = false;
                }

                if (dr["Uvoz"].ToString() == "1")
                {
                    chkUvoz.Checked = false;
                    chkIzvoz.Checked = true;
                }
                txtDatumRasporeda.Value = Convert.ToDateTime(dr["DatumRasporeda"].ToString());
                txtbrojkontejnera.Text = dr["BrojKontejnera"].ToString();
                cboVrstaKontejnera.SelectedValue = Convert.ToInt32(dr["VrstaKontejnera"].ToString());
                txtRegBr.Text = dr["Kamion"].ToString();
                txtNalogIzdao.Text = dr["NalogIzdao"].ToString();
                txtDatumRealizacije.Value = Convert.ToDateTime(dr["DatumRealizacije"].ToString());
                cboUvoznik.SelectedValue = Convert.ToInt32(dr["Uvoznik"].ToString());
                cboPostupak.SelectedValue = Convert.ToInt32(dr["CarinskiPostupak"].ToString());
                cboSpedicija.SelectedValue = Convert.ToInt32(dr["SpedicijaRTC"].ToString());

                cboBrodar.SelectedValue = Convert.ToInt32(dr["NazivBrodara"].ToString());
                cboVrstaRobe.SelectedValue = Convert.ToInt32(dr["VrstaRobe"].ToString());
                cboSaSklad.SelectedValue = Convert.ToInt32(dr["SaSkladista"].ToString());
                cboSaPoz.SelectedValue = Convert.ToInt32(dr["SaSkladista"].ToString());
                cboUsluga.SelectedValue = Convert.ToInt32(dr["IdUsluge"].ToString());
                txtNalogRealizovao.Text = dr["NalogRealizovao"].ToString();

                txtOtpremaID.Text = dr["OtpremaID"].ToString();
                txtRegBr.Text = dr["Kamion"].ToString();
                txtNalogID.Text = dr["NalogID"].ToString();
                cboIzvoznik.SelectedValue = Convert.ToInt32(dr["Izvoznik"].ToString());
                if (dr["Zavrsen"].ToString() == "1")
                { chkZavrsen.Checked = true; }
                else
                {
                    chkZavrsen.Checked = false;
                }
            }

            con.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (chkUvoz.Checked == true)
            {
                RadniNalozi.InsertRN ir = new InsertRN();
                ir.InsRN6OtpremaPlatformeKam(Convert.ToDateTime(txtDatumRasporeda.Value), txtNalogIzdao.Text, Convert.ToDateTime(txtDatumRealizacije.Text), Convert.ToInt32(0), Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue), Convert.ToInt32(cboUsluga.SelectedValue), "", "", Convert.ToInt32(txtOtpremaID.Text), txtRegBr.Text, Convert.ToInt32(txtNalogID.Text), 0, 0);
                FillGV();
            }
            if (chkIzvoz.Checked == true)
            {
                RadniNalozi.InsertRN ir = new InsertRN();
                ir.InsRN6OtpremaPlatformeKam(Convert.ToDateTime(txtDatumRasporeda.Value), txtNalogIzdao.Text, Convert.ToDateTime(txtDatumRealizacije.Text), Convert.ToInt32(0), Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue), Convert.ToInt32(cboUsluga.SelectedValue), "", "", Convert.ToInt32(txtOtpremaID.Text), txtRegBr.Text, Convert.ToInt32(txtNalogID.Text), 1, 0);
                FillGVIzvozni();
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Saobracaj.RadniNalozi.frmDodelaSkladista ds = new frmDodelaSkladista(txtOtpremaID.Text, 6);
            ds.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            InsertRN up = new InsertRN();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected == true)
                {
                    up.PotvrdiUradjenRN6(Convert.ToInt32(row.Cells[0].Value.ToString()), KorisnikTekuci);
                }

            }
        }

        private void chkUvoz_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUvoz.Checked == true)
            {
                FillGV();
                chkIzvoz.Checked = false;

            }
            else
            {
                FillGVIzvozni();
                chkIzvoz.Checked = true;
            }

        }
    }
}
