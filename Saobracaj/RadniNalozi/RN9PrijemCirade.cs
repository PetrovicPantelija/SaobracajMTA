using Saobracaj.Sifarnici;
using Syncfusion.Windows.Forms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
//
namespace Saobracaj.RadniNalozi
{
    public partial class RN9PrijemCirade : Form
    {
        private string connect = Sifarnici.frmLogovanje.connectionString;
        private bool status = false;
        string kor = frmLogovanje.user;

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
            // this.FormBorderStyle = FormBorderStyle.FixedSingle;

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
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
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

        public RN9PrijemCirade()
        {
            InitializeComponent();
            ChangeTextBox();
            FillGV();
            FillCombo();
        }

        public RN9PrijemCirade(string PrijemID, string Kamion, string Korisnik)
        {
            InitializeComponent();
            FillGV();
            ChangeTextBox();
            FillCombo();
            //POOOOOO
            txtPrijemID.Text = PrijemID;
            txtKamion.Text = Kamion;
            txtNalogIzdao.Text = Korisnik;
          

        }
        private void FillGV()
        {
            var select = "Select * from RNPrijemCirade order by ID desc";
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

            var dir2 = "Select ID, (Oznaka + ' ' + Naziv) as Naziv from VrstaCarinskogPostupka order by Oznaka";
            var dirAD2 = new SqlDataAdapter(dir2, conn);
            var dirDS2 = new DataSet();
            dirAD2.Fill(dirDS2);
            cboPostupak.DataSource = dirDS2.Tables[0];
            cboPostupak.DisplayMember = "Naziv";
            cboPostupak.ValueMember = "ID";

            var it = "select ID, Naziv from InspekciskiTretman order by Naziv";
            var itAD = new SqlDataAdapter(it, conn);
            var itDS = new DataSet();
            itAD.Fill(itDS);
            cboInspekcijski.DataSource = itDS.Tables[0];
            cboInspekcijski.DisplayMember = "Naziv";
            cboInspekcijski.ValueMember = "ID";

            var partner4 = "Select PaSifra,PaNaziv From Partnerji where Spediter = 1 order by PaSifra";
            var partAD4 = new SqlDataAdapter(partner4, conn);
            var partDS4 = new DataSet();
            partAD4.Fill(partDS4);
            cboSpedicija.DataSource = partDS4.Tables[0];
            cboSpedicija.DisplayMember = "PaNaziv";
            cboSpedicija.ValueMember = "PaSifra";

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
                rn.InsRNPPrijemCirade(Convert.ToDateTime(txtDatumRasporeda.Value), txtBrojKontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cboVrstaKontejnera.SelectedValue),
                    txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(txtDatumRealizacije.Value), Convert.ToInt32(cboSaSredstva.SelectedValue),
                    txtBrojPlombe.Text.ToString().TrimEnd(), Convert.ToInt32(cboPostupak.SelectedValue), Convert.ToInt32(cboInspekcijski.SelectedValue),
                    Convert.ToInt32(cboSpedicija.SelectedValue), Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue), Convert.ToInt32(cboSaSklad.SelectedValue),
                    Convert.ToInt32(cboSaPoz.SelectedValue), Convert.ToInt32(cboUsluga.SelectedValue), "", txtNapomena.Text.ToString().TrimEnd());
            }
            else
            {
                rn.UpdRNPPrijemCirade(Convert.ToInt32(txtID.Text), Convert.ToDateTime(txtDatumRasporeda.Value), txtBrojKontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cboVrstaKontejnera.SelectedValue),
                    txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(txtDatumRealizacije.Value), Convert.ToInt32(cboSaSredstva.SelectedValue),
                    txtBrojPlombe.Text.ToString().TrimEnd(), Convert.ToInt32(cboPostupak.SelectedValue), Convert.ToInt32(cboInspekcijski.SelectedValue),
                    Convert.ToInt32(cboSpedicija.SelectedValue), Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue), Convert.ToInt32(cboSaSklad.SelectedValue),
                    Convert.ToInt32(cboSaPoz.SelectedValue), Convert.ToInt32(cboUsluga.SelectedValue), txtNalogRealizovao.Text.ToString().TrimEnd(), txtNapomena.Text.ToString().TrimEnd());
            }
            FillGV();
            status = false;
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                InsertRN rn = new InsertRN();
                rn.DelRNPPrijemCirade(Convert.ToInt32(txtID.Text.ToString().TrimEnd()));
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

            SqlCommand cmd = new SqlCommand(" SELECT [ID]      , [DatumRasporeda] " +
     " , [BrojKontejnera]     , [VrstaKontejnera]      , [NalogIzdao]      , [DatumRealizacije] " +
     "  , [SaVoznogSredstva]      , [BrojPlombe]      , [CarinskiPostupak]      , [InspekcijskiPregled] " +
     "  , [SpedicijaRTC]      , [NazivBrodara]      , [VrstaRobe]      , [SaSkladista] " +
     "  , [SaPozicijeSklad]      , [IdUsluge]      , [NalogRealizovao]      , [Napomena] " +
     "  , [Kamion]      , [PrijemID]      , [Zavrsen]      , [NalogID] " +
  "  FROM [dbo].[RNPrijemCirade] " +
             " where RNPrijemCirade.ID = " + txtID.Text, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtDatumRasporeda.Value = Convert.ToDateTime(dr["DatumRasporeda"].ToString());
                txtBrojKontejnera.Text = dr["BrojKontejnera"].ToString();
                cboVrstaKontejnera.SelectedValue = Convert.ToInt32(dr["VrstaKontejnera"].ToString());
                cboUsluga.SelectedValue = Convert.ToInt32(dr["IDUsluge"].ToString());
                txtNalogRealizovao.Text = dr["NalogRealizovao"].ToString();
               // txtPrijemID.Text = dr["PrijemID"].ToString();
                txtNalogID.Text = dr["NalogID"].ToString();
                cboSaSklad.SelectedValue = Convert.ToInt32(dr["SaSkladista"].ToString());
                cboSaPoz.SelectedValue = Convert.ToInt32(dr["SaPozicijeSklad"].ToString());
                txtKamion.Text = dr["Kamion"].ToString();
                txtNalogIzdao.Text = dr["NalogIzdao"].ToString();
                txtDatumRealizacije.Value = Convert.ToDateTime(dr["DatumRealizacije"].ToString());
                txtPrijemID.Text = dr["PrijemID"].ToString();
                txtNapomena.Text =  dr["Napomena"].ToString();
                cboSpedicija.SelectedValue = Convert.ToInt32(dr["SpedicijaRTC"].ToString());
                cboBrodar.SelectedValue = Convert.ToInt32(dr["Brodar"].ToString());
                txtBrojPlombe.Text = dr["BrojPlombe"].ToString();
                txtNapomena.Text = dr["Napomena"].ToString();
                cboInspekcijski.SelectedValue = Convert.ToInt32(dr["InspekcijskiPregled"].ToString());
                cboPostupak.SelectedValue = Convert.ToInt32(dr["CarinskiPostupak"].ToString());
                
                if (dr["Zavrsen"].ToString() == "1")
                { chkZavrsen.Checked = true; }
                else
                {
                    chkZavrsen.Checked = false;
                }
            }

            con.Close();
        }

        private void RN9PrijemCirade_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            RadniNalozi.InsertRN ir = new InsertRN();
            ir.InsRN9PrijmCiradeKam(Convert.ToDateTime(txtDatumRasporeda.Value), txtNalogIzdao.Text, Convert.ToDateTime(txtDatumRealizacije.Text), Convert.ToInt32(cboSaSredstva.SelectedValue), Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue), Convert.ToInt32(cboUsluga.SelectedValue), "", txtNapomena.Text, Convert.ToInt32(txtPrijemID.Text), txtKamion.Text, Convert.ToInt32(cboPostupak.SelectedValue), Convert.ToInt32(cboInspekcijski.SelectedValue), Convert.ToInt32(cboSpedicija.SelectedValue), Convert.ToInt32(cboBrodar.SelectedValue), txtBrojPlombe.Text,Convert.ToInt32(txtNalogID.Text));
            FillGV();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Prijemnica pr = new Prijemnica();
            pr.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            PrijemnicaPregled pp = new PrijemnicaPregled();
            pp.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            RadniNalozi.InsertRN rn = new InsertRN();
            rn.PotvrdiUradjenRN9(Convert.ToInt32(txtID.Text), kor);
        }

        private void label19_Click(object sender, EventArgs e)
        {

        }
    }
}
