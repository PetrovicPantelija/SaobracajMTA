using Saobracaj.Sifarnici;
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
    public partial class RN2OtpremaVoza :Form
    {
        private string connect = Sifarnici.frmLogovanje.connectionString;
        private bool status = false;
        string KorisnikTekuce = frmLogovanje.user;
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



        public RN2OtpremaVoza()
        {
            InitializeComponent();
            FillGV();
            FillCombo();
            ChangeTextBox();
        }

        public RN2OtpremaVoza(int brojrn)
        {
            InitializeComponent();
            BrojRN = brojrn;
            FillGVIzRNI();
            FillCombo();
            ChangeTextBox();
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

        public RN2OtpremaVoza(string Korisnik, string IDVOza, string IDUsluge, string OtpremaID)
        {
            InitializeComponent();
            FillGV();
            FillCombo();
            txtNalogIzdao.Text = Korisnik;
            cboNaSredstvo.SelectedValue = Convert.ToInt32(IDVOza);
            cboUsluga.SelectedValue = Convert.ToInt32(IDUsluge);
            txtOtpremaID.Text = OtpremaID;
            ChangeTextBox();
           // RefreshStavkeVoza(OtpremaID);

        }
        /*
        private void RefreshStavkeVoza(string OtpremaID)
        {
            if (cboUsluga.SelectedValue is null)
            {
                MessageBox.Show("Izaberite uslugu");
                return;
            }
            
            Select BookingBrodara, BrodskaPlomba, Brodar, PeriodSkladistenjaOd, PeriodSkladistenjaDo, NetoRobe, BrutoRobe, BrutoRobeO, CBMO, BrojKoletaO, OstalePlombe, Tara, VGMBrod2 from IzvozKonacna Where ID = " + txtKOntejnerID.Text , con);
                                                                                                                                                                                       select IzvozKonacnaVrstaManipulacije.ID as ID, IzvozKonacnaVrstaManipulacije.IDNadredjena as KontejnerID," +
                " IzvozKonacna.BrojKontejnera," +
" IzvozKonacnaVrstaManipulacije.Kolicina,  VrstaManipulacije.ID as ManipulacijaID,VrstaManipulacije.Naziv as ManipulacijaNaziv, " +
" IzvozKonacnaVrstaManipulacije.Cena,OrganizacioneJedinice.ID,   OrganizacioneJedinice.Naziv as OrganizacionaJedinica,  " +
 "  Partnerji.PaSifra as NalogodavacID,PArtnerji.PaNaziv as Platilac" +
" from IzvozKonacnaVrstaManipulacije Inner    join VrstaManipulacije on VrstaManipulacije.ID = IzvozKonacnaVrstaManipulacije.IDVrstaManipulacije" +
" inner" +
" join PArtnerji on IzvozKonacnaVrstaManipulacije.Platilac = PArtnerji.PaSifra  inner " +
" join OrganizacioneJedinice on OrganizacioneJedinice.ID = IzvozKonacnaVrstaManipulacije.OrgJed " +
" inner " +
" join IzvozKonacna on IzvozKonacnaVrstaManipulacije.IDNadredjena = IzvozKonacna.ID where IzvozKonacna.ID = " + Convert.ToInt32(txtKOntejnerID.Text);
            
            var select = "  SELECT OtpremaKontejneraVozStavke.[ID],[IDNadredjenog]       ,[BrojKontejnera],[BrojVagona], " +
             "  KontejnerID " +
             " FROM [dbo].[OtpremaKontejneraVozStavke] " +
     " inner join IzvozKonacnaVrstaManipulacije on IzvozKonacnaVrstaManipulacije.IDNAdredjena = OtpremaKontejneraVozStavke.KontejnerID " +
     " where IdNadredjenog = " + OtpremaID + " and  IZvozKonacnaVrstaManipulacije.IDVrstaManipulacije = " + cboUsluga.SelectedValue + " order by RB";

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = false;
            dataGridView2.DataSource = ds.Tables[0];

            dataGridView2.BorderStyle = BorderStyle.None;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView2.BackgroundColor = Color.White;

            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

        }
        */

        private void FillGV()
        {
            var select = "SELECT RNOtpremaVoza.ID, RNOtpremaVoza.DatumRasporeda, RNOtpremaVoza.BrojKontejnera, RNOtpremaVoza.VrstaKontejnera," +
                " TipKontenjera.ID AS TKID, TipKontenjera.Naziv, RNOtpremaVoza.NalogIzdao, " +
                " RNOtpremaVoza.DatumRealizacije, RNOtpremaVoza.NaVoznoSredstvo, Voz.BrVoza, Voz.Relacija, RNOtpremaVoza.BrojPlombe, RNOtpremaVoza.BrojVagona, " +
                "RNOtpremaVoza.Zavrsen,   RNOtpremaVoza.IdUsluge, RNOtpremaVoza.NalogRealizovao, RNOtpremaVoza.Napomena, RNOtpremaVoza.OtpremaID, " +
                "RNOtpremaVoza.NalogID, RNOtpremaVoza.SaSkladista, Skladista.Naziv AS SkladisteNaziv FROM           RNOtpremaVoza INNER JOIN" +
                "                        TipKontenjera ON RNOtpremaVoza.VrstaKontejnera = TipKontenjera.ID INNER JOIN   " +
                "  Voz ON RNOtpremaVoza.NaVoznoSredstvo = Voz.ID INNER JOIN  Skladista ON RNOtpremaVoza.SaSkladista = Skladista.ID order by RNOtpremaVoza.ID desc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView1);
        }

        private void FillGVIzRNI()
        {
            var select = "SELECT RNOtpremaVoza.ID, RNOtpremaVoza.DatumRasporeda, RNOtpremaVoza.BrojKontejnera, RNOtpremaVoza.VrstaKontejnera," +
                " TipKontenjera.ID AS TKID, TipKontenjera.Naziv, RNOtpremaVoza.NalogIzdao, " +
                " RNOtpremaVoza.DatumRealizacije, RNOtpremaVoza.NaVoznoSredstvo, Voz.BrVoza, Voz.Relacija, RNOtpremaVoza.BrojPlombe, RNOtpremaVoza.BrojVagona, " +
                "RNOtpremaVoza.Zavrsen,   RNOtpremaVoza.IdUsluge, RNOtpremaVoza.NalogRealizovao, RNOtpremaVoza.Napomena, RNOtpremaVoza.OtpremaID, " +
                "RNOtpremaVoza.NalogID, RNOtpremaVoza.SaSkladista, Skladista.Naziv AS SkladisteNaziv FROM           RNOtpremaVoza INNER JOIN" +
                "                        TipKontenjera ON RNOtpremaVoza.VrstaKontejnera = TipKontenjera.ID INNER JOIN   " +
                "  Voz ON RNOtpremaVoza.NaVoznoSredstvo = Voz.ID INNER JOIN  Skladista ON RNOtpremaVoza.SaSkladista = Skladista.ID where RNOtpremaVoza.ID =" + BrojRN;
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
            cboNaSredstvo.DataSource = dsVS.Tables[0];
            cboNaSredstvo.DisplayMember = "Opis";
            cboNaSredstvo.ValueMember = "ID";

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
            cboRoba.DataSource = dsRoba.Tables[0];
            cboRoba.DisplayMember = "Naziv";
            cboRoba.ValueMember = "ID";

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

            //VRati tekuce skladiste
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
                rn.InsRNOtpremaVoza(Convert.ToDateTime(txtDatumRasporeda.Value.ToString()), txtbrojkontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cboVrstaKontejnera.SelectedValue),
                    txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(txtDatumRealizacije.Value.ToString()), Convert.ToInt32(cboNaSredstvo.SelectedValue), txtBrojPlombe.Text.ToString().TrimEnd(),
                    txtBrojVagona.Text.ToString().TrimEnd(), Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboRoba.SelectedValue), Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue),
                    Convert.ToInt32(cboUsluga.SelectedValue), "", txtNapomena.Text.ToString().TrimEnd());

            }
            else
            {
                rn.UpdRNOtpremaVoza(Convert.ToInt32(txtID.Text.ToString().TrimEnd()), Convert.ToDateTime(txtDatumRasporeda.Value.ToString()), txtbrojkontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cboVrstaKontejnera.SelectedValue),
                    txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(txtDatumRealizacije.Value.ToString()), Convert.ToInt32(cboNaSredstvo.SelectedValue), txtBrojPlombe.Text.ToString().TrimEnd(),
                    txtBrojVagona.Text.ToString().TrimEnd(), Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboRoba.SelectedValue), Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue),
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
                rn.DelRNOtpremaVoza(Convert.ToInt32(txtID.Text));
                FillGV();
            }
            else
            {
                MessageBox.Show("Izaberite broj zapisa!");
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
                    }
                }
            }
            catch { }
        }
        private void txtDatumRasporeda_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {  DialogResult dialogResult = MessageBox.Show("Formirace se RN za sve Interne Naloge po Vrsti usluge ", "Some Title", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
            RadniNalozi.InsertRN ir = new InsertRN();
            //PANTA
            ir.InsRNOtpremaVozaCeoVoz(Convert.ToDateTime(txtDatumRasporeda.Value), txtNalogIzdao.Text, Convert.ToDateTime(txtDatumRealizacije.Text), Convert.ToInt32(cboNaSredstvo.SelectedValue), Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue), Convert.ToInt32(cboUsluga.SelectedValue), "", txtNapomena.Text, Convert.ToInt32(txtOtpremaID.Text));
            FillGV();
   }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }
          
         

        private void RN2OtpremaVoza_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            InsertRN up = new InsertRN();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected == true)
                {
                    up.PotvrdiUradjenRN2(Convert.ToInt32(row.Cells[0].Value.ToString()), KorisnikTekuce);
                    up.ArhivirajKontejner(row.Cells[0].Value.ToString());
                }

            }
          
        }

        private void VratiPodatkeStavka()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT [ID]      , [DatumRasporeda]      , [BrojKontejnera] " +
      " , [VrstaKontejnera]      , [NalogIzdao]      , [DatumRealizacije] " +
      " , [NaVoznoSredstvo]      , [BrojPlombe]      , [BrojVagona] " +
      " , [NazivBrodara]      , [VrstaRobe]      , [SaSkladista] " +
      " , [SaPozicijeSklad]      , [IdUsluge]      , [NalogRealizovao] " +
      " , [Napomena]      , [OtpremaID]      , [NalogID] " +
      " , [Zavrsen], UvozniID   FROM [dbo].[RNOtpremaVoza]" +
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
                txtOtpremaID.Text = dr["OtpremaID"].ToString();
                //cboIzvoznik.SelectedValue = Convert.ToInt32(dr["Izvoznik"].ToString());
                txtBrojPlombe.Text = dr["BrojPlombe"].ToString();
                txtNalogID.Text = dr["NalogID"].ToString();
                cboBrodar.SelectedValue = Convert.ToInt32(dr["NazivBrodara"].ToString());

                txtNapomena.Text = dr["Napomena"].ToString();
                cboSaPoz.SelectedValue = Convert.ToInt32(dr["SaPozicijeSklad"].ToString());

                cboSaSklad.SelectedValue = Convert.ToInt32(dr["SaSkladista"].ToString());
              
                //NIJE DOBRO null   cboPostupak.SelectedValue = Convert.ToInt32(dr["CarinskiPostupak"].ToString());
                txtNapomena.Text = dr["Napomena"].ToString();
                txtUvozniID.Text = dr["UvozniID"].ToString();
                //NIJE DOBR NULL   cboInspekcijski.SelectedValue = Convert.ToInt32(dr["InspekcijskiPregled"].ToString());

                if (dr["Zavrsen"].ToString() == "1")
                { chkZavrsen.Checked = true; }
                else
                {
                    chkZavrsen.Checked = false;
                }

              

               
            }

            con.Close();

        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
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

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
           

        }

        private void txtNalogRealizovao_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {

        }
    }
}
