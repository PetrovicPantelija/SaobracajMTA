using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Forms;

using Saobracaj.Dokumenta;
using Saobracaj.Sifarnici;
using Syncfusion.Windows.Forms;
//
namespace Saobracaj.RadniNalozi
{
    public partial class RN4PrijemPlatforme : Form
    {
        private string connect = Sifarnici.frmLogovanje.connectionString;
        private bool status = false;
        string KorisnikTekuci = frmLogovanje.user;
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


        public RN4PrijemPlatforme()
        {
            InitializeComponent();
            FillGV();
            FillCombo();
            ChangeTextBox();
        }

        public RN4PrijemPlatforme(string PrijemID, string RegBr, string KorisnikCene, string Usluga, int Uvoz, string NalogID)
        {
            InitializeComponent();
            ChangeTextBox();
            txtNalogIzdao.Text = KorisnikCene;
            txtPrijemID.Text = PrijemID;
            txtKamion.Text = RegBr;
            
            NapuniVrstuUsluge(Usluga);

            txtNalogID.Text = NalogID;
            FillCombo();
            VratiPodatkeVrstaMan(NalogID);
            if (Uvoz == 0)
            {
                chkUvoz.Checked = true;
                chkIzvoz.Checked = false;
                
            }
            if (Uvoz == 1)
            {
                chkUvoz.Checked = false;
                chkIzvoz.Checked = true;
            }
            // cboUsluga.SelectedValue = Usluga;
             FillGV();

        }
        private void VratiPodatkeVrstaMan(string NalogID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" Select VrstaManipulacije.ID from RadniNalogInterni inner join " +
   " VrstaManipulacije on RadniNalogInterni.IDManipulacijaJed = VrstaManipulacije.ID where RadniNalogInterni.ID = " + NalogID, con);

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
            var select = "Select * from RNPrijemPlatforme order by ID desc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
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

            var partner = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var partAD = new SqlDataAdapter(partner, conn);
            var partDS = new DataSet();
            partAD.Fill(partDS);
            cboIzvoznik.DataSource = partDS.Tables[0];
            cboIzvoznik.DisplayMember = "PaNaziv";
            cboIzvoznik.ValueMember = "PaSifra";

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

            var partner7 = "Select PaSifra,PaNaziv From Partnerji where Brodar = 1 order by PaSifra";
            var partAD7 = new SqlDataAdapter(partner7, conn);
            var partDS7 = new DataSet();
            partAD7.Fill(partDS7);
            cboBrodar.DataSource = partDS7.Tables[0];
            cboBrodar.DisplayMember = "PaNaziv";
            cboBrodar.ValueMember = "PaSifra";
            /*
            var roba = "Select ID,Naziv From NHM order by ID";
            var daRoba = new SqlDataAdapter(roba, conn);
            var dsRoba = new DataSet();
            daRoba.Fill(dsRoba);
            cboVrstaRobe.DataSource = dsRoba.Tables[0];
            cboVrstaRobe.DisplayMember = "Naziv";
            cboVrstaRobe.ValueMember = "ID";
            */
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
            int Zavrsen = 0;
            int ZavrsenCIR = 0;
            int VizuelniPotreban = 0;
            int IzUvoza = 0;
            int ZavrsenKalmarista = 0;
            int VizuelniZavrsen = 0;
            int PotrebanCIR = 0;
            if (chkPotrebanCIR.Checked == true)
            {
                PotrebanCIR = 1;
            }
            if (chkVizuelniZavrsen.Checked == true)
            {
                VizuelniZavrsen = 1;
            }
            if (chkZavrsenKalmarista.Checked == true)
            {
                ZavrsenKalmarista = 1;
            }
            if (chkUvoz.Checked == true)
            {
                IzUvoza = 1;
            }
            if (chkVizuelni.Checked == true)
            {
                VizuelniPotreban = 1;
            }
            if (chkZavrsenCIR.Checked == true)
            {
                ZavrsenCIR = 1;
            }

            if (chkZavrsen.Checked == true)
            {
                Zavrsen = 1;
            }


            InsertRN rn = new InsertRN();
            if (status == true)
            {
                rn.InsRNPPrijemPlatforme(Convert.ToDateTime(txtDatumRasporeda.Value), txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(txtDatumRealizacije.Value),
                    Convert.ToInt32(cboSaSredstva.SelectedValue), txtbrojkontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cboVrstaKontejnera.SelectedValue), txtBrojPlombe.Text.ToString().TrimEnd(),
                    Convert.ToInt32(cboIzvoznik.SelectedValue), Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboPostupak.SelectedValue), Convert.ToInt32(cboInspekcijski.SelectedValue),
                    Convert.ToInt32(cboVrstaRobe.SelectedValue), Convert.ToInt32(cboNaSklad.SelectedValue), Convert.ToInt32(cboNaPoz.SelectedValue), Convert.ToInt32(cboUsluga.SelectedValue),
                    "", txtNapomena.Text.ToString().TrimEnd());
            }
            else
            {
                rn.UpdRNPPrijemPlatforme(Convert.ToInt32(txtID.Text.ToString()), Convert.ToDateTime(txtDatumRasporeda.Value), txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(txtDatumRealizacije.Value),
                    Convert.ToInt32(cboSaSredstva.SelectedValue), txtbrojkontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cboVrstaKontejnera.SelectedValue), txtBrojPlombe.Text.ToString().TrimEnd(),
                    Convert.ToInt32(cboIzvoznik.SelectedValue), Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboPostupak.SelectedValue), Convert.ToInt32(cboInspekcijski.SelectedValue),
                    Convert.ToInt32(cboVrstaRobe.SelectedValue), Convert.ToInt32(cboNaSklad.SelectedValue), Convert.ToInt32(cboNaPoz.SelectedValue), Convert.ToInt32(cboUsluga.SelectedValue),
                    txtNalogRealizovao.Text.ToString().TrimEnd(), txtNapomena.Text.ToString().TrimEnd(),
                    txtKamion.Text, Convert.ToInt32(txtPrijemID.Text), Convert.ToInt32(txtNalogID.Text), Zavrsen, ZavrsenCIR, txtNalogRealizovaoCIR.Text,
            Convert.ToDateTime(dtpNalogRealizovaoCIR.Value), Convert.ToInt32(cboNaSkladistePregledac.SelectedValue), ZavrsenKalmarista, txtNalogRealizovaoKal.Text, Convert.ToDateTime(dtpNalogRealizovaoKal.Value), PotrebanCIR,
             VizuelniPotreban, IzUvoza, txtVizuelniRealizovao.Text, Convert.ToDateTime(txtDatumRealizacije.Value),  VizuelniZavrsen);
            }
            FillGV();
            status = false;
        }
        private void tsDelete_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                InsertRN rn = new InsertRN();
                rn.DelRNPPrijemPlatforme(Convert.ToInt32(txtID.Text.ToString()));
                FillGV();
            }
            else
            {
                MessageBox.Show("Izaberite broj zapisa");
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected == true)
                {
                    txtID.Text = row.Cells[0].Value.ToString();
                    VratiPodatkeStavka();
                }
                 
            }
        }

        private void VratiPodatkeStavka()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT [ID]      ,[DatumRasporeda]      ,[NalogIzdao] " +
     " ,[DatumRealizacije]      ,[SaVoznogSredstva]      ,[BrojKontejnera]      ,[VrstaKontejnera] " +
     "  ,[BrojPlombe]      ,[Izvoznik]      ,[NazivBrodara]      ,[CarinskiPostupak] " +
     "  ,[InspekcijskiPregled]      ,[VrstaRobe]      ,[USkladiste]      ,[UPozicijaSklad] " +
     "  ,[IdUsluge]      ,[NalogRealizovao]      ,[Napomena]      ,[Kamion] " +
     "  ,[PrijemID]      ,[NalogID]      ,[Zavrsen]," +
     "  [ZavrsenCIR]     ,[NalogRealizovaoCIR]      ,[DatumRealizacijeCIR]      ,[USkladisteCIR]     ,[ZavrsenKalmarista]" +
     "     ,[NalogRealizovaoKal]      ,IsNull(GetDate(),[DatumRealizacijeKal]) as DatumRealizacijeKal," +
     " [VizuelniPotreban]          ,[IzUvoza]          ,[VizuelniUradio]          ,[VizuelniDatumReal]           ,[VizuelniZavrsen] FROM [dbo].[RNPrijemPlatforme]" +
             " where ID = " + txtID.Text, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtNalogRealizovaoCIR.Text = dr["NalogRealizovaoCIR"].ToString();
              //  dtpNalogRealizovaoCIR.Value = Convert.ToDateTime(dr["DatumRealizacijeCIR"].ToString());
                txtNalogRealizovaoKal.Text = dr["NalogRealizovaoKal"].ToString();
                dtpNalogRealizovaoKal.Value = Convert.ToDateTime(dr["DatumRealizacijeKal"].ToString());
                txtDatumRasporeda.Value = Convert.ToDateTime(dr["DatumRasporeda"].ToString());
                txtbrojkontejnera.Text = dr["BrojKontejnera"].ToString();
                cboVrstaKontejnera.SelectedValue = Convert.ToInt32(dr["VrstaKontejnera"].ToString());
                cboUsluga.SelectedValue = Convert.ToInt32(dr["IdUsluge"].ToString());
                txtNalogRealizovao.Text = dr["NalogRealizovao"].ToString();
                txtNalogIzdao.Text = dr["NalogIzdao"].ToString();
                txtDatumRealizacije.Value = Convert.ToDateTime(dr["DatumRealizacije"].ToString());
                txtPrijemID.Text = dr["PrijemID"].ToString(); 
                //cboIzvoznik.SelectedValue = Convert.ToInt32(dr["Izvoznik"].ToString());
                txtBrojPlombe.Text = dr["BrojPlombe"].ToString();
                txtNalogID.Text = dr["NalogID"].ToString();
                cboBrodar.SelectedValue = Convert.ToInt32(dr["NazivBrodara"].ToString());
                cboVrstaRobe.SelectedValue = Convert.ToInt32(dr["VrstaRobe"].ToString());

                cboNaPoz.SelectedValue = Convert.ToInt32(dr["UPozicijaSklad"].ToString());

                cboNaSklad.SelectedValue = Convert.ToInt32(dr["USkladiste"].ToString());
                txtKamion.Text = dr["Kamion"].ToString();
             //NIJE DOBRO null   cboPostupak.SelectedValue = Convert.ToInt32(dr["CarinskiPostupak"].ToString());
                txtNapomena.Text = dr["Napomena"].ToString();
                txtVizuelniRealizovao.Text = dr["VizuelniUradio"].ToString();
                //NIJE DOBR NULL   cboInspekcijski.SelectedValue = Convert.ToInt32(dr["InspekcijskiPregled"].ToString());

                if (dr["Zavrsen"].ToString() == "1")
                { chkZavrsen.Checked = true; }
                else
                {
                    chkZavrsen.Checked = false;
                }

                if (dr["ZavrsenCIR"].ToString() == "1")
                { chkZavrsenCIR.Checked = true; }
                else
                {
                    chkZavrsenCIR.Checked = false;
                }

                if (dr["ZavrsenKalmarista"].ToString() == "1")
                { chkZavrsenKalmarista.Checked = true; }
                else
                {
                    chkZavrsenKalmarista.Checked = false;
                }
                if (dr["VizuelniPotreban"].ToString() == "1")
                { chkVizuelni.Checked = true; }
                else
                {
                    chkVizuelni.Checked = false;
                }
                if (dr["IzUvoza"].ToString() == "1")
                { chkUvoz.Checked = true; }
                else
                {
                    chkIzvoz.Checked = false;
                }

                if (dr["IzUvoza"].ToString() == "1")
                { chkUvoz.Checked = true; }
                else
                {
                    chkIzvoz.Checked = false;
                }
            }

            con.Close();
        }
        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void RN4PrijemPlatforme_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            int potrebanvizuelni = 0;
            if (chkVizuelni.Checked == true)
            {
                potrebanvizuelni = 1;


            }
            int pomIzUvoza = 0;
            if (chkUvoz.Checked == true)
            {
                pomIzUvoza = 1;
            
            }

            if (chkUvoz.Checked == true)
            {
                //
                RadniNalozi.InsertRN ir = new InsertRN();
                ir.InsRNPrijemPlatformeKamUvoz(Convert.ToDateTime(txtDatumRasporeda.Value), txtNalogIzdao.Text, Convert.ToDateTime(txtDatumRealizacije.Text), Convert.ToInt32(0), Convert.ToInt32(cboNaSklad.SelectedValue), Convert.ToInt32(cboNaPoz.SelectedValue), Convert.ToInt32(cboUsluga.SelectedValue), "", txtNapomena.Text, Convert.ToInt32(txtPrijemID.Text), txtKamion.Text, Convert.ToInt32(txtNalogID.Text), potrebanvizuelni,pomIzUvoza );
                FillGV();

            }
            else
            {
                RadniNalozi.InsertRN ir = new InsertRN();
                ir.InsRNPrijemPlatformeKamIzvoz(Convert.ToDateTime(txtDatumRasporeda.Value), txtNalogIzdao.Text, Convert.ToDateTime(txtDatumRealizacije.Text), Convert.ToInt32(0), Convert.ToInt32(cboNaSklad.SelectedValue), Convert.ToInt32(cboNaPoz.SelectedValue), Convert.ToInt32(cboUsluga.SelectedValue), "", txtNapomena.Text, Convert.ToInt32(txtPrijemID.Text), txtKamion.Text, Convert.ToInt32(txtNalogID.Text), potrebanvizuelni, pomIzUvoza);
                FillGV();
            }
           
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //Uradjeno je za prijem platforme za modul Uvoza
            InsertRN up = new InsertRN();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected == true)
                {
                    up.PotvrdiUradjenRN4Premesten(Convert.ToInt32(row.Cells[0].Value.ToString()),KorisnikTekuci);
                }

            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmCIR cir = new frmCIR(Convert.ToInt32(txtPrijemID.Text),1);
            cir.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (chkUvoz.Checked == true)
            {
                Saobracaj.RadniNalozi.frmDodelaSkladista ds = new frmDodelaSkladista(txtPrijemID.Text, 22); // Ako je UVoz
                ds.Show();
            }
            else
            {
                //Dodeljuje se skladiste na koje Kalmarista spusta kontejner
                Saobracaj.RadniNalozi.frmDodelaSkladista ds = new frmDodelaSkladista(txtPrijemID.Text, 2);
                ds.Show();
            }
           
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            InsertRN up = new InsertRN();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected == true)
                {
                    up.PotvrdiUradjenRN4(Convert.ToInt32(row.Cells[0].Value.ToString()), KorisnikTekuci );
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
                    up.PotvrdiUradjenRN4CIR(Convert.ToInt32(row.Cells[0].Value.ToString()), KorisnikTekuci);
                }

            }
        }

        private void txtNalogRealizovaoCIR_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpNalogRealizovaoCIR_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void chkZavrsenCIR_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
