using Syncfusion.Windows.Forms.Tools;
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
    public partial class RN5PrijemPlatforme2 : Syncfusion.Windows.Forms.Office2010Form
    {
        private string connect = Sifarnici.frmLogovanje.connectionString;
        private bool status = false;
        string kor = Sifarnici.frmLogovanje.user;
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

        public RN5PrijemPlatforme2()
        {
            InitializeComponent();
            FillGV();
            FillCombo();
            txtDatumRasporeda.Value = DateTime.Now;
            ChangeTextBox();
        }


        public RN5PrijemPlatforme2(int brojrn)
        {
            InitializeComponent();
            BrojRN = brojrn;
  
            FillGVIZRNI();
            FillCombo();
            txtDatumRasporeda.Value = DateTime.Now;
            ChangeTextBox();
        }

        public RN5PrijemPlatforme2(string PrijemID, string RegBr, string KorisnikCene, string Usluga, int Uvoz, int NalogID)
        {
            InitializeComponent();
            txtNalogIzdao.Text = KorisnikCene;
            txtPrijemID.Text = PrijemID;
            txtKamion.Text = RegBr;
          //  NapuniVrstuUsluge(NalogID);

            //txtNalogID.Text = Usluga;
            FillCombo();
            VratiPodatkeVrstaMan(NalogID.ToString());
            txtDatumRasporeda.Value = DateTime.Now;
            txtNalogID.Text = NalogID.ToString();
            ChangeTextBox();

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
            cboUsluga.SelectedValue = Convert.ToInt32(IDUsluga);
        }
        private void FillGV()
        {
            var select = " SELECT [RNPrijemPlatforme2].[ID] as ID      ,[DatumRasporeda] " +
    " ,[NalogIzdao]      ,[DatumRealizacije]      ,[SaVoznogSredstva]      ,[BrojKontejnera] " +
    "   ,TipKontenjera.Naziv as VrstaKontejnera      ,Partnerji_3.PaNaziv as Brodar " +
     "  ,[VrstaRobe]      ,[USkladiste]      ,[UPozicijaSklad]      ,[IdUsluge]      ,[NalogRealizovao] " +
     "  ,[Napomena]      ,[PrijemID]      ,[Kamion]      ,[Zavrsen] " +
     "  ,[NalogID]           FROM [dbo].[RNPrijemPlatforme2] " +
  " INNER JOIN  Partnerji AS Partnerji_3 ON[RNPrijemPlatforme2].NazivBrodara = Partnerji_3.PaSifra " +
" INNER JOIN TipKontenjera ON[RNPrijemPlatforme2].VrstaKontejnera = TipKontenjera.ID " +
" inner join Skladista on Skladista.ID = [USkladiste] order by RNPrijemPlatforme2.ID desc";
            SqlConnection conn = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView1);
        }

        private void FillGVIZRNI()
        {
            var select = " SELECT [RNPrijemPlatforme2].[ID] as ID      ,[DatumRasporeda] " +
    " ,[NalogIzdao]      ,[DatumRealizacije]      ,[SaVoznogSredstva]      ,[BrojKontejnera] " +
    "   ,TipKontenjera.Naziv as VrstaKontejnera      ,Partnerji_3.PaNaziv as Brodar " +
     "  ,[VrstaRobe]      ,[USkladiste]      ,[UPozicijaSklad]      ,[IdUsluge]      ,[NalogRealizovao] " +
     "  ,[Napomena]      ,[PrijemID]      ,[Kamion]      ,[Zavrsen] " +
     "  ,[NalogID]           FROM [dbo].[RNPrijemPlatforme2] " +
  " INNER JOIN  Partnerji AS Partnerji_3 ON[RNPrijemPlatforme2].NazivBrodara = Partnerji_3.PaSifra " +
" INNER JOIN TipKontenjera ON[RNPrijemPlatforme2].VrstaKontejnera = TipKontenjera.ID " +
" inner join Skladista on Skladista.ID = [USkladiste] where  RNPrijemPlatforme2.ID  = " + BrojRN;
            SqlConnection conn = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView1);
        }

        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connect);

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
            cboSaSredstva.DataSource = dsVS.Tables[0];
            cboSaSredstva.DisplayMember = "Opis";
            cboSaSredstva.ValueMember = "ID";

            var partner7 = "Select PaSifra,PaNaziv From Partnerji where Brodar = 1 order by PaSifra";
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
            cboNaSklad.DataSource = dsSklad.Tables[0];
            cboNaSklad.DisplayMember = "Naziv";
            cboNaSklad.ValueMember = "ID";

            var pozicija = "Select Id,Opis from Pozicija";
            var daPoz = new SqlDataAdapter(pozicija, conn);
            var dsPoz = new DataSet();
            daPoz.Fill(dsPoz);
            cboNaPoz.DataSource = dsPoz.Tables[0];
            cboNaPoz.DisplayMember = "Opis";
            cboNaPoz.ValueMember = "ID";
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
                rn.InsRNPPrijemPlatforme2(Convert.ToDateTime(txtDatumRasporeda.Value), txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(txtDatumRealizacije.Value),
                    Convert.ToInt32(cboSaSredstva.SelectedValue), txtbrojkontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cboVrstaKontejnera.SelectedValue),
                    Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue), Convert.ToInt32(cboNaSklad.SelectedValue), Convert.ToInt32(cboNaPoz.SelectedValue),
                    Convert.ToInt32(cboUsluga.SelectedValue), "", txtNapomena.Text.ToString().TrimEnd());
            }
            else
            {
                rn.UpdRNPPrijemPlatforme2(Convert.ToInt32(txtID.Text), Convert.ToDateTime(txtDatumRasporeda.Value), txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(txtDatumRealizacije.Value),
                    Convert.ToInt32(cboSaSredstva.SelectedValue), txtbrojkontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cboVrstaKontejnera.SelectedValue),
                    Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue), Convert.ToInt32(cboNaSklad.SelectedValue), Convert.ToInt32(cboNaPoz.SelectedValue),
                    Convert.ToInt32(cboUsluga.SelectedValue), txtNalogRealizovao.Text.ToString().TrimEnd(), txtNapomena.Text.ToString().TrimEnd());
            }
            FillGV();
            status = false;
        }
        private void tsDelete_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                InsertRN rn = new InsertRN();
                rn.DelRNPPrijemPlatforme2(Convert.ToInt32(txtID.Text));
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
                        VratiPodatkeStavka();
                    }
                }
            }
            catch { }
        }
        private void VratiPodatkeStavka()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT[ID]      ,[DatumRasporeda]      ,[NalogIzdao]     " +
                " ,[DatumRealizacije]      ,[SaVoznogSredstva] " +
    "  ,[BrojKontejnera]      ,[VrstaKontejnera]      ,[NazivBrodara]      ,[VrstaRobe] " +
    "    ,[USkladiste]      ,[UPozicijaSklad]      ,[IdUsluge]      ,[NalogRealizovao] " +
    "    ,[Napomena]      ,[PrijemID]      ,[Kamion]      ,[Zavrsen]      ,[NalogID], ZavrsenCIR, DatumRealizacijeCIR, NalogRealizovaoCIR " +
  "  FROM [dbo].[RNPrijemPlatforme2] " +
             " where ID = " + txtID.Text, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtDatumRasporeda.Value = Convert.ToDateTime(dr["DatumRasporeda"].ToString());
                txtbrojkontejnera.Text = dr["BrojKontejnera"].ToString();
                cboVrstaKontejnera.SelectedValue = Convert.ToInt32(dr["VrstaKontejnera"].ToString());
                cboUsluga.SelectedValue = Convert.ToInt32(dr["IdUsluge"].ToString());
                txtNalogRealizovao.Text = dr["NalogRealizovao"].ToString();
                txtNalogIzdao.Text = dr["NalogIzdao"].ToString();
                txtDatumRealizacije.Value = Convert.ToDateTime(dr["DatumRealizacije"].ToString());
                txtPrijemID.Text = dr["PrijemID"].ToString();
                txtNalogID.Text = dr["NalogID"].ToString();
                cboBrodar.SelectedValue = Convert.ToInt32(dr["NazivBrodara"].ToString());
                cboVrstaRobe.SelectedValue = Convert.ToInt32(dr["VrstaRobe"].ToString());
                cboNaPoz.SelectedValue = Convert.ToInt32(dr["UPozicijaSklad"].ToString());
                cboNaSklad.SelectedValue = Convert.ToInt32(dr["USkladiste"].ToString());
                txtKamion.Text = dr["Kamion"].ToString();
                //NIJE DOBRO null   cboPostupak.SelectedValue = Convert.ToInt32(dr["CarinskiPostupak"].ToString());
                txtNapomena.Text = dr["Napomena"].ToString();
                txtNalogRealizovaoCIR.Text = dr["NalogRealizovaoCIR"].ToString();
                dtpNalogRealizovaoCIR.Value = Convert.ToDateTime(dr["DatumRealizacijeCIR"].ToString());

                if (dr["ZavrsenCIR"].ToString() == "1")
                { chkZavrsenCIR.Checked = true; }
                else
                {
                    chkZavrsenCIR.Checked = false;
                }


                if (dr["Zavrsen"].ToString() == "1")
                { chkZavrsen.Checked = true; }
                else
                {
                    chkZavrsen.Checked = false;
                }
            }

            con.Close();
        }

        private void RN5_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            RadniNalozi.InsertRN ir = new InsertRN();
            ir.InsRN5PrijemPlatformeKam(Convert.ToDateTime(txtDatumRasporeda.Value), txtNalogIzdao.Text, Convert.ToDateTime(txtDatumRealizacije.Text), Convert.ToInt32(0), Convert.ToInt32(cboNaSklad.SelectedValue), Convert.ToInt32(cboNaPoz.SelectedValue), Convert.ToInt32(cboUsluga.SelectedValue), "", txtNapomena.Text, Convert.ToInt32(txtPrijemID.Text), txtKamion.Text, Convert.ToInt32(txtNalogID.Text));
            FillGV();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //Uradjeno je za prijem platforme za modul Uvoza
            InsertRN up = new InsertRN();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected == true)
                {
                    up.PotvrdiUradjenRN5(Convert.ToInt32(row.Cells[0].Value.ToString()), kor);
                }

            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            InsertRN up = new InsertRN();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected == true)
                {
                    up.PotvrdiUradjenRN5CIR(Convert.ToInt32(row.Cells[0].Value.ToString()), kor);
                }

            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            InsertRN up = new InsertRN();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected == true)
                {
                    up.PotvrdiUradjenRN5S(Convert.ToInt32(row.Cells[0].Value.ToString()));
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Saobracaj.RadniNalozi.frmDodelaSkladista ds = new frmDodelaSkladista(txtPrijemID.Text, 5);
            ds.Show();
        }
    }
}
