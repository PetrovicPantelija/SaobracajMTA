using Saobracaj.Uvoz;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using static Syncfusion.WinForms.Core.NativeScroll;
using Saobracaj;
using Syncfusion.Windows.Forms;
using System.Drawing.Imaging;
//
namespace Saobracaj.RadniNalozi
{
    public partial class RN12MedjuskladisniKontejnera : Form
    {
        private string connect = Sifarnici.frmLogovanje.connectionString;
        private bool status = false;
        string KorisnikTekuci = Saobracaj.Sifarnici.frmLogovanje.user.ToString();

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

        public RN12MedjuskladisniKontejnera()
        {
            InitializeComponent();
            FillGV();
            FillCombo();
            ChangeTextBox();
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

            //cboUsluge.SelectedValue = Convert.ToInt32(IDUsluga);
        }

        public RN12MedjuskladisniKontejnera(int NalogID, string BrojKontejnera, string Napomena)
        {
            InitializeComponent();
            FillGV();
            FillCombo();
            txtNapomena.Text = Napomena;
            txtBrojKontejnera.Text = BrojKontejnera;
            txtNalogID.Text = NalogID.ToString();

            NapuniVrstuUsluge(NalogID.ToString());

            KorisnikTekuci = Saobracaj.Sifarnici.frmLogovanje.user;
            txtDatumRasporeda.Value = DateTime.Now;
        }

        private void FillGV()
        {
            var select = "SELECT       RNMedjuskladisni.ID as ID,  RNMedjuskladisni.BrojKontejnera,  RNMedjuskladisni.VrstaKontejnera,TipKontenjera.Naziv, " +
                "Skladista.ID as SaID, Skladista.Naziv AS SkladSa, Skladista_1.ID AS NaID, Skladista_1.Naziv AS NaSkladiste," +
                "                     RNMedjuskladisni.DatumRasporeda, " +
               " RNMedjuskladisni.NalogIzdao, RNMedjuskladisni.DatumRealizacije, RNMedjuskladisni.NalogRealizovao, RNMedjuskladisni.Napomena, " +
               " RNMedjuskladisni.Zavrsen FROM            RNMedjuskladisni INNER JOIN" +
               " TipKontenjera ON RNMedjuskladisni.VrstaKontejnera = TipKontenjera.ID INNER JOIN" +
               " Skladista ON RNMedjuskladisni.SaSkladista = Skladista.ID INNER JOIN                         Skladista AS Skladista_1 " +
               " ON RNMedjuskladisni.NaSkladiste = Skladista_1.ID Order by  RNMedjuskladisni.ID desc";
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
            cboNaPoz.DisplayMember = "Oznaka";
            cboNaPoz.ValueMember = "ID";

            var sklad2 = "select ID,naziv from Skladista";
            var daSklad2 = new SqlDataAdapter(sklad2, conn);
            var dsSklad2 = new DataSet();
            daSklad2.Fill(dsSklad2);
            cboSaSklad.DataSource = dsSklad2.Tables[0];
            cboSaSklad.DisplayMember = "Naziv";
            cboSaSklad.ValueMember = "ID";

            var pozicija2 = "Select Id,Opis from Pozicija";
            var daPoz2 = new SqlDataAdapter(pozicija2, conn);
            var dsPoz2 = new DataSet();
            daPoz2.Fill(dsPoz2);
            cboSaPoz.DataSource = dsPoz2.Tables[0];
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
                rn.InsRnMedjuskladisni(Convert.ToDateTime(txtDatumRasporeda.Value), txtBrojKontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cboVrstaKontejnera.SelectedValue),
                    txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(txtDatumRealizacije.Value), Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue),
                    Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue), Convert.ToInt32(cboNaSklad.SelectedValue), Convert.ToInt32(cboNaPoz.SelectedValue),
                    Convert.ToInt32(cboUsluga.SelectedValue), "", txtNapomena.Text.ToString().TrimEnd(), Convert.ToInt32(txtNalogID.Text));
            }
            else
            {
                rn.UpdRnMedjuskladisni(Convert.ToInt32(txtID.Text), Convert.ToDateTime(txtDatumRasporeda.Value), txtBrojKontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cboVrstaKontejnera.SelectedValue),
                    txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(txtDatumRealizacije.Value), Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue),
                    Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue), Convert.ToInt32(cboNaSklad.SelectedValue), Convert.ToInt32(cboNaPoz.SelectedValue),
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
                rn.DelRnMedjuskladisni(Convert.ToInt32(txtID.Text.ToString().TrimEnd()));
                FillGV();
            }
            else
            {
                MessageBox.Show("Izaberite broj zapisa");
            }
        }

        private void VratiPodatke(string ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT  ID, DatumRasporeda, VrstaKontejnera, BrojKontejnera, NalogIzdao, SaSkladista, SaPozicijeSklad, NaSkladiste, " +
                " NaPoziciju, NalogRealizovao, Napomena, Zavrsen, NalogID, POtrebanCIR, NalogRealizovaoCIR, DatumRealizacijeCIR, ZavrsenCIR" +
                " FROM  RNMedjuskladisni where ID=" + ID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtID.Text = dr["ID"].ToString();
                txtDatumRasporeda.Value = Convert.ToDateTime(dr["DatumRasporeda"].ToString());
                cboVrstaKontejnera.SelectedValue = Convert.ToInt32(dr["VrstaKontejnera"].ToString());
                txtBrojKontejnera.Text = dr["BrojKontejnera"].ToString();
                txtNalogIzdao.Text = dr["NalogIzdao"].ToString();
                cboSaSklad.SelectedValue = Convert.ToInt32(dr["SaSkladista"].ToString());
                cboSaPoz.SelectedValue = Convert.ToInt32(dr["SaPozicijeSklad"].ToString());
                cboNaSklad.SelectedValue = Convert.ToInt32(dr["NaSkladiste"].ToString());
                cboNaPoz.SelectedValue = Convert.ToInt32(dr["NaPoziciju"].ToString());
                txtNalogRealizovao.Text = dr["NalogRealizovao"].ToString();
              //  txtNalogRealizovao.Text = dr["NalogRealizovao"].ToString();
                txtNalogID.Text = dr["NalogID"].ToString();
                txtNapomena.Text = dr["Napomena"].ToString();
                txtNalogRealizovaoCIR.Text =  dr["NalogRealizovaoCIR"].ToString();
               // dtpNalogRealizovaoCIR.Value = 
                if (dr["Zavrsen"].ToString() == "1")
                {
                    chkZavrsen.Checked = true;
                }
                else
                {
                    chkZavrsen.Checked = false;
                }

               
                if (dr["PotrebanCIR"].ToString() == "1")
                {
                    chkPotrebanCIR.Checked = true;
                }
                else
                {
                    chkPotrebanCIR.Checked = false;
                }

                if (dr["ZavrsenCIR"].ToString() == "1")
                {
                    chkZavrsenCIR.Checked = true;
                }
                else
                {
                    chkZavrsenCIR.Checked = false;
                }
                if (dr["DatumRealizacijeCIR"].ToString() == "")
                {
                }
                else
                {
                    dtpNalogRealizovaoCIR.Value = Convert.ToDateTime(dr["DatumRealizacijeCIR"].ToString());
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
                        txtID.Text = row.Cells[0].Value.ToString();
                        VratiPodatke(txtID.Text);
                    }
                }
            }
            catch { }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            InsertRN up = new InsertRN();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected == true)
                {
                    up.PotvrdiUradjenRN12(Convert.ToInt32(row.Cells[0].Value.ToString()), KorisnikTekuci);
                }

            }
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

        private void VratiOstaloIzRN1(string BrojKontejnera)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Top 1 NaSkladiste, NaPozicijuSklad, VrstaKOntejnera from RNPrijemVoza where BrojKontejnera = '" + BrojKontejnera + "' Order by ID DESC", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                // txtID.Text = dr["ID"].ToString();
                cboVrstaKontejnera.SelectedValue = Convert.ToInt32(dr["VrstaKOntejnera"].ToString());
                cboSaSklad.SelectedValue = Convert.ToInt32(dr["NaSkladiste"].ToString());
                cboSaPoz.SelectedValue = Convert.ToInt32(dr["NaPozicijuSklad"].ToString());
            }
            con.Close();




        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            using (var detailForm = new Uvoz.frmKontejnerTekuce())
            {
                detailForm.ShowDialog();
                txtBrojKontejnera.Text = detailForm.GetBrojKontejnera();
                VratiOstaloIzTekuceg(txtBrojKontejnera.Text);
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            int PotrebanCIR = 0;
            RadniNalozi.InsertRN ir = new InsertRN();
            if (chkPotrebanCIR.Checked == true ) {
                PotrebanCIR = 1;
            }
            ir.InsRN12Medjuskladisni(Convert.ToDateTime(txtDatumRasporeda.Value), txtNalogIzdao.Text, Convert.ToDateTime(txtDatumRealizacije.Text), Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue), Convert.ToInt32(cboNaSklad.SelectedValue), Convert.ToInt32(cboNaPoz.SelectedValue), Convert.ToInt32(cboUsluga.SelectedValue), "", txtNapomena.Text, txtBrojKontejnera.Text, Convert.ToInt32(cboVrstaKontejnera.SelectedValue), Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(txtNalogID.Text), PotrebanCIR);
            FillGV();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VratiOstaloIzTekuceg(txtBrojKontejnera.Text);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            VratiOstaloIzRN1(txtBrojKontejnera.Text);
        }
    }
}
