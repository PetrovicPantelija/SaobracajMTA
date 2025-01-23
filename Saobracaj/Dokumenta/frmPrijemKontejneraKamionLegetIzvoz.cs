using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Diagnostics.CodeAnalysis;
using Saobracaj;
using Saobracaj.RadniNalozi;
using iTextSharp.text.pdf.parser.clipper;
using Saobracaj.Uvoz;
using Syncfusion.Windows.Forms;

namespace Saobracaj.Dokumenta
{
    public partial class frmPrijemKontejneraKamionLegetIzvoz : Form
    {
        MailMessage mailMessage;
        string KorisnikCene = Saobracaj.Sifarnici.frmLogovanje.user;
        bool status = false;
        int usao = 0;
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;


        private void ChangeTextBox()
        {


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
                panelHeader.Visible = true;
                meniHeader.Visible = false;
                this.BackColor = Color.White;
                this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
                this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
                this.ControlBox = true;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                Office2010Colors.ApplyManagedColors(this, Color.White);
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;



                foreach (Control control in this.Controls)
                {

                }


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
                panelHeader.Visible = false;
                meniHeader.Visible = true;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }


        private void ChangeTextBoxPanel1()
        {


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;






                foreach (Control control in panel1.Controls)
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

        public frmPrijemKontejneraKamionLegetIzvoz()
        {
            InitializeComponent();

            chkIzvoz.Checked = true;

        }

        public frmPrijemKontejneraKamionLegetIzvoz(string Sifra, int Vozom, int OJ)
        {
            InitializeComponent();
            ChangeTextBox();
            ChangeTextBoxPanel1();
            
           // chkIzvoz.Checked = true;
            FillCombo();
            //KorisnikCene = Korisnik;
            //txtNalogID.Text = NalogID;
            

            txtSifra.Text = Sifra;
            VratiPodatke(Convert.ToInt32(Sifra));
            if (OJ == 2)
            {
                chkIzvoz.Checked = true;
                chkTerminal.Checked = false;
                if (chkCirada.Checked == false)
                {
                    this.Text = "GATE IN KAMION IZVOZ";
                }
                else
                {
                    this.Text = "PLANIRANI PRETOVAR IZVOZ";
                    label55.Visible = true;
                    txtTaraKontejnera.Visible = true;

                    label57.Visible = true;
                    cbNacinPakovanja.Visible = true;

                    label68.Visible = true;
                    txtTaraKontejneraZ.Visible = true;

                    label58.Visible = true;
                    dtpPlanUtovara.Visible = true;

                    label60.Visible = true;
                    dtpPeriodSkladistenjaOd.Visible = true;
                    dtpPeriodSkladistenjaDo.Visible = true;

                }

            }
            // chkIzvoz.Checked = true;
            if (OJ == 4)
            {
                chkIzvoz.Checked = false;
                chkTerminal.Checked = true;
                this.Text = "GATE IN KAMION TERMINAL";

                //SAkrivanje vidljivosti polja
                txtBrojPlombe.Visible = false;
                label3.Visible = false;
                txtBrojPlombe2.Visible = false;
                label39.Visible = false;
                bttoRobeOtpremnica.Visible = false;

                label42.Visible = false;
                txtKOLETAOTP.Visible = false;

                label43.Visible = false;
                txtCBMOTP.Visible = false;

                label25.Visible = false;
                txtTara.Visible = false;

                label35.Visible = false;
                bttoRobeOdvaga.Visible = false;

                label56.Visible = false;
                txVGMBrodBruto.Visible = false;

                label46.Visible = false;
                cboIzvoznik.Visible = false;

                label8.Visible = false;
                cboOrganizator.Visible = false;

                label52.Visible = false;
                cboReexport.Visible = false;

                label47.Visible = false;
                txtADR.Visible = false;

                label53.Visible = false;
                cboInspekciskiTretman.Visible = false;

                label48.Visible = false;
                cboPPCNT.Visible = false;

                label49.Visible = false;
                cboCarina.Visible = false;

                label50.Visible = false;
                cboSpedicija.Visible = false;

                label51.Visible = false;
                txtKontaktSpeditera.Visible = false;

                label54.Visible = false;
                txtDodatneNapomene.Visible = false;

                label41.Visible = false;
                dataGridView3.Visible = false;

            }
            RefreshDataGrid();
         }

        private void VratiPodatkeIzvoznePoNalogu(string ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            // SqlCommand cmd = new SqlCommand("select [ID] ,[DatumOtpreme],[StatusOtpreme],[IdVoza],[VremeOdlaska], [RegBrKamiona], [ImeVozaca], NacinOtpreme, Napomena, NajavaEmail, OtpremaEmail, Zatvoren, CIRUradjen, PredefinisanePorukeID from OtpremaKontejnera where ID = " + ID, con);

            SqlCommand cmd = new SqlCommand("Select RadniNalogInterni.ID as IDRNI, IDManipulacijaJED as USLUGA, BRojOSnov as KOntejnerID, " +
                "KonkretaIDUsluge, IzvozKonacna.BrojKontejnera, " +
" IzvozKonacna.BrojVagona, IzvozKOnacna.VrstaKontejnera, IzvozKOnacna.Brodar, " +
"IzvozKOnacna.BookingBrodara, IzvozKOnacna.BrodskaPlomba, IzvozKonacna.OstalePlombe, " +
" IzvozKOnacna.BrojKoletaO, IzvozKOnacna.BrutoRobe, IzvozKOnacna.BrutoRobeO, IzvozKOnacna.CBMO, " +
"IzvozKOnacna.Tara, IzvozKOnacna.NetoRobe, IzvozKOnacna.PeriodSkladistenjaOd, " +
" IzvozKOnacna.PeriodSkladistenjaDo, IzvozKOnacna.Izvoznik, IzvozKonacna.Inspekcija  from RadniNalogInterni " +
" Inner Join izvozKOnacna on RadniNalogInterni.BrojOsnov = IzvozKonacna.ID " +
" where RadniNalogInterni.ID  = " + ID, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtKontejnerID.Text = dr["KOntejnerID"].ToString();
                txtBrojKontejnera.Text = dr["BrojKontejnera"].ToString();
                txtVagon.Text = dr["BrojVagona"].ToString();
                cboTipKontejnera.SelectedValue = Convert.ToInt32(dr["VrstaKontejnera"].ToString());
                cboVlasnikKontejnera.SelectedValue = Convert.ToInt32(dr["VrstaKontejnera"].ToString());
                txtBukingBrodar.Text = dr["Brodar"].ToString();
                txtTara.Value = Convert.ToDecimal(dr["Tara"].ToString());
                txtNeto.Value = Convert.ToDecimal(dr["NetoRobe"].ToString());
                dtpPerodSkladistenjaOd.Value = Convert.ToDateTime(dr["PeriodSkladistenjaOd"].ToString());
                dtpPeriodSkladistenjaDo.Value = Convert.ToDateTime(dr["PeriodSkladistenjaDo"].ToString());
              //  bttoRobeFaktura.Value = Convert.ToDecimal(dr["BrutoRobe"].ToString());
                bttoRobeOtpremnica.Value = Convert.ToDecimal(dr["BrutoRobeO"].ToString());
                bttoRobeOdvaga.Value = Convert.ToDecimal(dr["BrutoRobeO"].ToString());
                txtCBMOTP.Text = dr["CBMO"].ToString();
                txtKOLETAOTP.Text = dr["BrojKoletaO"].ToString();
                txtBrojPlombe.Text = dr["BrodskaPlomba"].ToString();
                txtBrojPlombe2.Text = dr["OstalePlombe"].ToString();
                cboIzvoznik.SelectedValue = Convert.ToInt32(dr["Izvoznik"].ToString());
                cboStatusKontejnera.SelectedValue = Convert.ToInt32(dr["Inspekcija"].ToString());
                /*
                dtpDatumOtpreme.Value = Convert.ToDateTime(dr["DatumOtpreme"].ToString());
                dtpVremeOdlaska.Value = Convert.ToDateTime(dr["VremeOdlaska"].ToString());
                cboVozBuking.SelectedValue = Convert.ToInt32(dr["IDVoza"].ToString());
                // cboPredefinisanePoruke.SelectedValue = Convert.ToInt32(dr["PredefinisanePorukeID"].ToString());
                cboStatusOtpreme.SelectedIndex = Convert.ToInt32(dr["StatusOtpreme"].ToString());
                txtRegBrKamiona.Text = dr["RegBrKamiona"].ToString();
                txtImeVozaca.Text = dr["ImeVozaca"].ToString();
                txtNapomena.Text = dr["Napomena"].ToString();
                if (Convert.ToInt32(dr["NacinOtpreme"].ToString()) == 1)
                {
                    chkVoz.Checked = true;
                }
                else
                {
                    chkVoz.Checked = false;
                }

                if (Convert.ToInt32(dr["Zatvoren"].ToString()) == 1)
                {
                    chkZatvoren.Checked = true;
                }
                else
                {
                    chkZatvoren.Checked = false;
                }

                if (Convert.ToInt32(dr["NajavaEmail"].ToString()) == 1)
                {
                    chkNajava.Checked = true;
                }
                else
                {
                    chkNajava.Checked = false;
                }


                if (Convert.ToInt32(dr["OtpremaEmail"].ToString()) == 1)
                {
                    chkOtprema.Checked = true;
                }
                else
                {
                    chkOtprema.Checked = false;
                }
                if (Convert.ToInt32(dr["CIRUradjen"].ToString()) == 0)
                {
                    chkCIRUradjen.Checked = false;
                }
                else
                {
                    chkCIRUradjen.Checked = true;
                }
 */
            }

            con.Close();

        }

        public frmPrijemKontejneraKamionLegetIzvoz(string Korisnik, int Vozom, string NalogID, int Cirada, int Poreklo)
        {
            InitializeComponent();
            FillCombo();
            ChangeTextBox();
            ChangeTextBoxPanel1();
            KorisnikCene = Korisnik;
            txtNalogID.Text = NalogID;
            chkIzvoz.Checked = true;
            txtNalogID.Text = NalogID;
            VratiPodatkeIzvoznePoNalogu(NalogID);
            if (Cirada == 1)
            { chkPlatforma.Checked = false;
                chkCirada.Checked = true;
            
            }
            else 
            { chkPlatforma.Checked = true; chkCirada.Checked = false;}

            if (Vozom == 1)
            {
                chkVoz.Checked = true;
                txtImeVozaca.Enabled = false;
                txtRegBrKamiona.Enabled = false;
                dtpDatumPrijema.Value = DateTime.Now;
                dtpVremeDolaska.Value = DateTime.Now;
                //  dtpVremeOdlaska.Value = DateTime.Now;
                //  dtpVremePripremljen.Value = DateTime.Now;
                this.Text = "Prijem kontejnera vozom";
                dtpPerodSkladistenjaOd.Enabled = false;
                dtpPeriodSkladistenjaDo.Enabled = false;
            }
            else
            {
                chkVoz.Checked = false;
                cboBukingPrijema.Enabled = false;
                dtpDatumPrijema.Value = DateTime.Now;
                dtpVremeDolaska.Value = DateTime.Now;
                //  dtpVremeOdlaska.Value = DateTime.Now;
                // dtpVremePripremljen.Value = DateTime.Now;
                this.Text = "Prijem kontejnera Kamionom";
                txtVagon.Enabled = false;
                txtGranica.Enabled = false;
                dtpPerodSkladistenjaOd.Enabled = false;
                dtpPeriodSkladistenjaDo.Enabled = false;
                // toolStripButton6.Visible = false;
            }
            VratiKontejnerID();
        }

        private void VratiKontejnerID()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select BrojOsnov from RadniNalogInterni where ID = " + txtNalogID.Text, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtKontejnerID.Text = dr["BrojOsnov"].ToString();
            }

            con.Close();

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void chkPoslatEmailNajava_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void chkCIRUradjen_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtSifra.Enabled = false;
            dtpPerodSkladistenjaOd.Value = DateTime.Now;
            dtpPeriodSkladistenjaDo.Value = DateTime.Now;
            dtpDatumPrijema.Value = DateTime.Now;
            dtpDatumPrijema.Enabled = true;
        }
        private void VratiPodatkeMax()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Max([ID]) as ID from PrijemKontejneraVoz", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtSifra.Text = dr["ID"].ToString();
            }

            con.Close();
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            int pomDirektni_indirektni = 0;

            if (chkVrstaKamiona.Checked == true)
            {
                pomDirektni_indirektni = 1;
            }
            else
            {
                pomDirektni_indirektni = 0;
            }

            string sp = cboStatusPrijema.Text;
            int ini = cboStatusPrijema.SelectedIndex;
            int ini2 = Convert.ToInt32(cboStatusPrijema.SelectedValue);
            if (chkVoz.Checked == true)
            {
                //Promene ako je voz
                if (status == true)
                {
                    /// ,  string RegBrKamiona,   string ImeVozaca,   int Vozom
                    Saobracaj.Dokumeta.InsertPrijemKontejneraVoz ins = new Saobracaj.Dokumeta.InsertPrijemKontejneraVoz();
                    ins.InsertPrijemKontVoz(Convert.ToDateTime(dtpDatumPrijema.Text), Convert.ToInt32(cboStatusPrijema.SelectedIndex), Convert.ToInt32(cboBukingPrijema.SelectedValue), Convert.ToDateTime(dtpVremeDolaska.Value), Convert.ToDateTime(DateTime.Now), KorisnikCene, "", "", 1, txtNapomena.Text, Convert.ToInt32(cboPredefinisanePoruke.SelectedValue), 0, pomDirektni_indirektni, 0, 0, 0);
                    status = false;
                    VratiPodatkeMax();
                }
                else
                {
                    //int TipCenovnika ,int Komitent, double Cena , int VrstaManipulacije ,DateTime  Datum , string Korisnik
                    Saobracaj.Dokumeta.InsertPrijemKontejneraVoz upd = new Saobracaj.Dokumeta.InsertPrijemKontejneraVoz();
                    upd.UpdPrijemKontejneraVoz(Convert.ToInt32(txtSifra.Text), Convert.ToDateTime(dtpDatumPrijema.Text), Convert.ToInt32(cboStatusPrijema.SelectedIndex), Convert.ToInt32(cboBukingPrijema.SelectedValue), Convert.ToDateTime(dtpVremeDolaska.Value), Convert.ToDateTime(DateTime.Now), KorisnikCene, "", "", 1, txtNapomena.Text, Convert.ToInt32(cboPredefinisanePoruke.SelectedValue), 0, pomDirektni_indirektni, 0, 0, 0);
                    status = false;
                }
            }
            else
            {
                if (status == true)
                {
                    /// ,  string RegBrKamiona,   string ImeVozaca,   int Vozom
                    Saobracaj.Dokumeta.InsertPrijemKontejneraVoz ins = new Saobracaj.Dokumeta.InsertPrijemKontejneraVoz();
                    ins.InsertPrijemKontVoz(Convert.ToDateTime(dtpDatumPrijema.Text), Convert.ToInt32(cboStatusPrijema.SelectedIndex), Convert.ToInt32(cboBukingPrijema.SelectedValue), Convert.ToDateTime(dtpVremeDolaska.Value), Convert.ToDateTime(DateTime.Now), KorisnikCene, txtRegBrKamiona.Text, txtImeVozaca.Text, 0, txtNapomena.Text, Convert.ToInt32(cboPredefinisanePoruke.SelectedValue), 0, pomDirektni_indirektni, 0, 0, 0);
                    status = false;
                    VratiPodatkeMax();
                }
                else
                {
                    //int TipCenovnika ,int Komitent, double Cena , int VrstaManipulacije ,DateTime  Datum , string Korisnik
                    Saobracaj.Dokumeta.InsertPrijemKontejneraVoz upd = new Saobracaj.Dokumeta.InsertPrijemKontejneraVoz();
                    upd.UpdPrijemKontejneraVoz(Convert.ToInt32(txtSifra.Text), Convert.ToDateTime(dtpDatumPrijema.Text), Convert.ToInt32(cboStatusPrijema.SelectedIndex), Convert.ToInt32(cboBukingPrijema.SelectedValue), Convert.ToDateTime(dtpVremeDolaska.Value), Convert.ToDateTime(DateTime.Now), KorisnikCene, txtRegBrKamiona.Text, txtImeVozaca.Text, 0, txtNapomena.Text, Convert.ToInt32(cboPredefinisanePoruke.SelectedValue), 0, pomDirektni_indirektni, 0, 0, 0);
                    status = false;
                }


            }
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da brišete sve podatke?", "Brisanje celog prijem", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Saobracaj.Dokumeta.InsertPrijemKontejneraVoz upd = new Saobracaj.Dokumeta.InsertPrijemKontejneraVoz();
                upd.DeletePrijemKontejneraVoz(Convert.ToInt32(txtSifra.Text));
                MessageBox.Show("Izbrisani su podaci zaglavlja i pripadajuće stavke");
                RefreshDataGrid();
            }
            else if (dialogResult == DialogResult.No)
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtSifra.Text == "")
            {
                MessageBox.Show("Niste uneli zaglavlje");
            }
        
            else
            {
                Saobracaj.Dokumeta.InsertPrijemKontejneraVozStavke ins = new Saobracaj.Dokumeta.InsertPrijemKontejneraVozStavke();
                ins.InsertPrijemKontVozStavke(Convert.ToInt32(txtSifra.Text), txtBrojKontejnera.Text, txtVagon.Text, Convert.ToDouble(txtGranica.Value), Convert.ToDouble(txtBrojOsovina.Value), Convert.ToDouble(txtSopstvenaMasa.Value), Convert.ToDouble(txtTara.Value), Convert.ToDouble(txtNeto.Value), Convert.ToInt32(cboPosiljalac.SelectedValue), Convert.ToInt32(cboPrimalac.SelectedValue), Convert.ToInt32(cboVlasnikKontejnera.SelectedValue), Convert.ToInt32(cboTipKontejnera.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue), Convert.ToInt32(cboBukingOtpreme.SelectedValue), Convert.ToInt32(cboStatusKontejnera.SelectedValue), txtBrojPlombe.Text, Convert.ToInt32(txtPlaniraniLager.Text), Convert.ToInt32(cboBukingOtpreme.SelectedValue), Convert.ToDateTime(dtpVremeDolaska.Value), Convert.ToDateTime(dtpDatumPrijema.Value), Convert.ToDateTime(dtpPeriodSkladistenjaDo.Value), Convert.ToDateTime(DateTime.Now), KorisnikCene, txtBrojPlombe2.Text, Convert.ToInt32(cboOrganizator.SelectedValue), txtBukingBrodar.Text, txtNapomenaS.Text, Convert.ToDateTime(dtpPerodSkladistenjaOd.Value), Convert.ToDateTime(dtpPeriodSkladistenjaDo.Value), Convert.ToDouble(bttoRobe.Value), Convert.ToInt32(txtKontejnerID.Text), Convert.ToDouble(bttoKontejnera.Value), txtNapomenaS2.Text, Convert.ToInt32(cbPostupak.SelectedValue), 0, 0, "", "", "", Convert.ToInt32(txtNalogID.Text), 0);
                RefreshDataGrid();
            }
        }

        private void RefreshDataGrid()
        {
            //PANTA DATAGRID


            var select = "";

            if (chkIzvoz.Checked == true)
            {
                select = " SELECT PrijemKontejneraVozStavke.ID, PrijemKontejneraVozStavke.RB, " +
    " PrijemKontejneraVozStavke.IDNadredjenog,   PrijemKontejneraVozStavke.KontejnerID, " +
    " PrijemKontejneraVozStavke.BrojKontejnera,  TipKontenjera.Naziv AS TipKontejnera ,  " +
    " IzvozKonacna.BrojKontejnera,  " +
    " IzvozKonacna.BrojVagona, IzvozKOnacna.VrstaKontejnera, Partnerji_1.PaNaziv as Brodar, IzvozKOnacna.BookingBrodara,  " +
    " IzvozKOnacna.BrodskaPlomba, IzvozKonacna.OstalePlombe,  " +
    " IzvozKOnacna.BrojKoletaO, IzvozKOnacna.BrutoRobe, IzvozKOnacna.BrutoRobeO, IzvozKOnacna.CBMO, IzvozKOnacna.Tara,   " +
    " IzvozKOnacna.NetoRobe, IzvozKOnacna.PeriodSkladistenjaOd,  " +
    " IzvozKOnacna.PeriodSkladistenjaDo , Partnerji_2.PANAziv as Izvoznik, INSTret.Naziv as InspekciskiTretman  " +
    " FROM PrijemKontejneraVozStavke  " +
    " inner join IzvozKonacna on IzvozKonacna.ID = PrijemKontejneraVozStavke.KontejnerID  " +
    " INNER JOIN  Partnerji AS Partnerji_1 ON IzvozKonacna.Brodar = Partnerji_1.PaSifra  " +
    "  INNER JOIN  Partnerji AS Partnerji_2 ON IzvozKonacna.Izvoznik = Partnerji_2.PaSifra  " +
    " INNER JOIN  InspekciskiTretman AS INSTret ON IZvozKOnacna.Inspekcija = INSTret.ID  " +
    "  INNER JOIN TipKontenjera ON PrijemKontejneraVozStavke.TipKontejnera = TipKontenjera.ID  " +
               "  where IdNadredjenog =  " + txtSifra.Text + " order by RB";

            }

            else if (chkTerminal.Checked == true)
            {

                select = "  SELECT PrijemKontejneraVozStavke.ID, PrijemKontejneraVozStavke.RB,  PrijemKontejneraVozStavke.IDNadredjenog,  " +
" PrijemKontejneraVozStavke.KontejnerID,  PrijemKontejneraVozStavke.BrojKontejnera,  TipKontenjera.Naziv AS TipKontejnera ,  " +
" UvozKonacna.BrojKontejnera,   PrijemKontejneraVozStavke.BrojVagona, UVozKOnacna.TipKontejnera, Partnerji_1.PaNaziv as Brodar   FROM PrijemKontejneraVozStavke " +
" inner join UvozKonacna on UvozKonacna.ID = PrijemKontejneraVozStavke.KontejnerID " +
" INNER JOIN  Partnerji AS Partnerji_1 ON UvozKonacna.Brodar = Partnerji_1.PaSifra  " + 
"    INNER JOIN TipKontenjera ON PrijemKontejneraVozStavke.TipKontejnera = TipKontenjera.ID " +
"  where IdNadredjenog =  " + txtSifra.Text + " order by RB";


            }
     

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = false;
            dataGridView1.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView1);

            //Panta refresh
            /*

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[0].Visible = false;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "RB";
            dataGridView1.Columns[1].Width = 30;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Br Dok";
            dataGridView1.Columns[2].Width = 30;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Broj kontejnera";
            dataGridView1.Columns[3].Width = 110;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Broj Vagona";
            dataGridView1.Columns[4].Width = 110;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Granica";
            dataGridView1.Columns[5].Width = 50;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Br os";
            dataGridView1.Columns[6].Width = 50;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Sops masa";
            dataGridView1.Columns[7].Width = 50;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Tara";
            dataGridView1.Columns[8].Width = 70;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Neto";
            dataGridView1.Columns[9].Width = 70;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Posiljalac";
            dataGridView1.Columns[10].Width = 50;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Primalac";
            dataGridView1.Columns[11].Width = 50;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Vlasnik";
            dataGridView1.Columns[12].Width = 40;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Tip kontejnera";
            dataGridView1.Columns[13].Width = 70;

            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "NHM";
            dataGridView1.Columns[14].Width = 40;

            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Buking broldar";
            dataGridView1.Columns[15].Width = 70;

            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Status Kontejnera";
            dataGridView1.Columns[16].Width = 20;

            DataGridViewColumn column18 = dataGridView1.Columns[17];
            dataGridView1.Columns[17].HeaderText = "Br plombe";
            dataGridView1.Columns[17].Width = 90;

            DataGridViewColumn column19 = dataGridView1.Columns[18];
            dataGridView1.Columns[18].HeaderText = "Br plombe2";
            dataGridView1.Columns[18].Width = 9;

            DataGridViewColumn column20 = dataGridView1.Columns[19];
            dataGridView1.Columns[19].HeaderText = "Plan lager";
            dataGridView1.Columns[19].Width = 30;

            DataGridViewColumn column21 = dataGridView1.Columns[20];
            dataGridView1.Columns[20].HeaderText = "Vreme dolaska";
            dataGridView1.Columns[20].Width = 70;

            DataGridViewColumn column22 = dataGridView1.Columns[21];
            dataGridView1.Columns[21].HeaderText = "Vreme prip";
            dataGridView1.Columns[21].Visible = false;
            dataGridView1.Columns[21].Width = 70;

            DataGridViewColumn column23 = dataGridView1.Columns[22];
            dataGridView1.Columns[22].HeaderText = "Vreme odlaska";
            dataGridView1.Columns[22].Visible = false;
            dataGridView1.Columns[22].Width = 70;

            DataGridViewColumn column24 = dataGridView1.Columns[23];
            dataGridView1.Columns[23].HeaderText = "Datum";
            dataGridView1.Columns[23].Width = 70;

            DataGridViewColumn column25 = dataGridView1.Columns[24];
            dataGridView1.Columns[24].HeaderText = "Korisnik";
            dataGridView1.Columns[24].Width = 70;

            DataGridViewColumn column26 = dataGridView1.Columns[25];
            dataGridView1.Columns[25].HeaderText = "Napomena stav";
            dataGridView1.Columns[25].Width = 70;

            */
        }

        private void VratiPodatke(int ID)
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            //VR SqlCommand cmd = new SqlCommand("select [ID] ,[DatumPrijema],[StatusPrijema],[IdVoza],[VremeDolaska],RegBrKamiona, ImeVozaca, NajavaEmail, PrijemEmail, Napomena, CIRUradjen, PredefinisanaPorukaID from PrijemKontejneraVoz where ID=" + ID, con);

            SqlCommand cmd = new SqlCommand("select [ID] ,[DatumPrijema],[StatusPrijema],[IdVoza],[VremeDolaska],RegBrKamiona, ImeVozaca, NajavaEmail, PrijemEmail, Napomena, CIRUradjen, VrstaKamiona from PrijemKontejneraVoz where ID=" + ID, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                dtpDatumPrijema.Value = Convert.ToDateTime(dr["DatumPrijema"].ToString());
                dtpVremeDolaska.Value = Convert.ToDateTime(dr["VremeDolaska"].ToString());
                cboBukingPrijema.SelectedValue = Convert.ToInt32(dr["IDVoza"].ToString());
                cboStatusPrijema.SelectedIndex = Convert.ToInt32(dr["StatusPrijema"].ToString());
                txtRegBrKamiona.Text = dr["RegBrKamiona"].ToString();
                //VR cboPredefinisanePoruke.SelectedValue = Convert.ToInt32(dr["PredefinisanePorukeID"].ToString());
                //Napomena
                txtNapomena.Text = dr["Napomena"].ToString();

                if (Convert.ToInt32(dr["NajavaEmail"].ToString()) == 0)
                {
                    chkPoslatEmailNajava.Checked = false;
                }
                else
                {
                    chkPoslatEmailNajava.Checked = true;
                }

                if (Convert.ToInt32(dr["PrijemEmail"].ToString()) == 0)
                {
                    chkPoslatEmailPrijem.Checked = false;
                }
                else
                {
                    chkPoslatEmailPrijem.Checked = true;
                }
                if (Convert.ToInt32(dr["CIRUradjen"].ToString()) == 0)
                {
                    chkCIRUradjen.Checked = false;
                }
                else
                {
                    chkCIRUradjen.Checked = true;
                }


                txtImeVozaca.Text = dr["ImeVozaca"].ToString();

                if (Convert.ToInt32(dr["VrstaKamiona"].ToString()) == 0)
                {
                    chkCirada.Checked = false;
                    chkPlatforma.Checked = true;
                }
                else
                {
                    chkCirada.Checked = true;
                    chkPlatforma.Checked = false;
                }

            }

            con.Close();

        }

        private void RefreshDataGridRN()
        {
            //PANTA DATAGRID
            var select = "";
            if (chkPlatforma.Checked == true && chkIzvoz.Checked == true)
            {
                select = " select * from RNPrijemPlatforme " +
               " where PrijemID = " + txtSifra.Text;
            }
            else if (chkPlatforma.Checked == true && chkTerminal.Checked == true)
            {

                select = " select * from RNPrijemPlatforme2 " +
                " where PrijemID = " + txtSifra.Text;

            }
            else if (chkCirada.Checked == true)
            {
                select = " select * from  RNPrijemCirade " +
                 " where PrijemID = " + txtSifra.Text;

            }
          

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = false;
            dataGridView2.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView2);

            //Panta refresh


            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 40;
            dataGridView2.Columns[0].Visible = false;

            /*
            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "RB";
            dataGridView1.Columns[1].Width = 30;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "NAdr";
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[2].Width = 30;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "KontID";
            dataGridView1.Columns[3].Width = 40;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Br kont";
            dataGridView1.Columns[4].Width = 110;
           */
        }

        private void FillCombo()
        {
            //where Posiljalac = 1
            var select1 = " Select Distinct PaSifra as ID, PaNaziv as Naziv From Partnerji  order by PaNaziv";
            var s_connection1 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection1 = new SqlConnection(s_connection1);
            var c1 = new SqlConnection(s_connection1);
            var dataAdapter1 = new SqlDataAdapter(select1, c1);

            var commandBuilder1 = new SqlCommandBuilder(dataAdapter1);
            var ds1 = new DataSet();
            dataAdapter1.Fill(ds1);
            cboPosiljalac.DataSource = ds1.Tables[0];
            cboPosiljalac.DisplayMember = "Naziv";
            cboPosiljalac.ValueMember = "ID";
            //where Primalac = 1 
            var select2 = " Select Distinct PaSifra as ID, PaNaziv as Naziv From Partnerji order by PaNaziv";
            var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboPrimalac.DataSource = ds2.Tables[0];
            cboPrimalac.DisplayMember = "Naziv";
            cboPrimalac.ValueMember = "ID";
            //where Vlasnik =1 
            var select3 = " Select Distinct PaSifra as ID, PaNaziv as Naziv From Partnerji  order by PaNaziv";
            var s_connection3 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboVlasnikKontejnera.DataSource = ds3.Tables[0];
            cboVlasnikKontejnera.DisplayMember = "Naziv";
            cboVlasnikKontejnera.ValueMember = "ID";


            var select4 = " Select Distinct ID, Naziv From TipKontenjera order by Naziv";
            var s_connection4 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection4 = new SqlConnection(s_connection4);
            var c4 = new SqlConnection(s_connection4);
            var dataAdapter4 = new SqlDataAdapter(select4, c4);

            var commandBuilder4 = new SqlCommandBuilder(dataAdapter4);
            var ds4 = new DataSet();
            dataAdapter4.Fill(ds4);
            cboTipKontejnera.DataSource = ds4.Tables[0];
            cboTipKontejnera.DisplayMember = "Naziv";
            cboTipKontejnera.ValueMember = "ID";



            /*
            var dir = "Select ID,Naziv from DirigacijaKontejneraZa order by ID";
            var dirAD = new SqlDataAdapter(dir, conn);
            var dirDS = new DataSet();
            dirAD.Fill(dirDS);
            cbDirigacija.DataSource = dirDS.Tables[0];
            cbDirigacija.DisplayMember = "Naziv";
            cbDirigacija.ValueMember = "ID";
            */


            var dir4 = "Select ID,Naziv from InspekciskiTretman order by Naziv";
            var dirAD4 = new SqlDataAdapter(dir4, connection);
            var dirDS4 = new DataSet();
            dirAD4.Fill(dirDS4);
            cboInspekciskiTretman.DataSource = dirDS4.Tables[0];
            cboInspekciskiTretman.DisplayMember = "Naziv";
            cboInspekciskiTretman.ValueMember = "ID";
            
            var select8 = "  Select Distinct ID, (Cast(BrVoza as nvarchar(10)) + '-' + Relacija) as IdVoza   From Voz ";
            var s_connection8 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection8 = new SqlConnection(s_connection8);
            var c8 = new SqlConnection(s_connection8);
            var dataAdapter8 = new SqlDataAdapter(select8, c8);

            var commandBuilder8 = new SqlCommandBuilder(dataAdapter8);
            var ds8 = new DataSet();
            dataAdapter8.Fill(ds8);
            cboBukingPrijema.DataSource = ds8.Tables[0];
            cboBukingPrijema.DisplayMember = "IdVoza";
            cboBukingPrijema.ValueMember = "ID";
            //where Organizator = 1
            var select9 = " Select Distinct PaSifra as ID, PaNaziv as Naziv From Partnerji  order by PaNaziv";
            var s_connection9 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection9 = new SqlConnection(s_connection9);
            var c9 = new SqlConnection(s_connection9);
            var dataAdapter9 = new SqlDataAdapter(select9, c9);

            var commandBuilder9 = new SqlCommandBuilder(dataAdapter9);
            var ds9 = new DataSet();
            dataAdapter9.Fill(ds9);
            cboOrganizator.DataSource = ds9.Tables[0];
            cboOrganizator.DisplayMember = "Naziv";
            cboOrganizator.ValueMember = "ID";

            var select10 = " Select Distinct PaSifra as ID, PaNaziv as Naziv From Partnerji  order by PaNaziv";
            var s_connection10 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection10 = new SqlConnection(s_connection10);
            var c10 = new SqlConnection(s_connection10);
            var dataAdapter10 = new SqlDataAdapter(select10, c10);

            var commandBuilder10 = new SqlCommandBuilder(dataAdapter10);
            var ds10 = new DataSet();
            dataAdapter10.Fill(ds10);
            cboIzvoznik.DataSource = ds10.Tables[0];
            cboIzvoznik.DisplayMember = "Naziv";
            cboIzvoznik.ValueMember = "ID";


            var rl = "Select ID, (Naziv + ' - ' + Oznaka) as Naziv From KontejnerskiTerminali order by (Naziv + ' ' + Oznaka)";
            var rlSAD = new SqlDataAdapter(rl, connection);
            var rlSDS = new DataSet();
            rlSAD.Fill(rlSDS);
            cboPPCNT.DataSource = rlSDS.Tables[0];
            cboPPCNT.DisplayMember = "Naziv";
            cboPPCNT.ValueMember = "ID";

            var car = "Select ID, Naziv From Carinarnice order by Naziv";
            var carAD = new SqlDataAdapter(car, connection);
            var carDS = new DataSet();
            carAD.Fill(carDS);
            cboCarina.DataSource = carDS.Tables[0];
            cboCarina.DisplayMember = "Naziv";
            cboCarina.ValueMember = "ID";


            var partner3 = "Select PaSifra,PaNaziv From Partnerji where Spediter =1 order by PaNaziv";
            var partAD3 = new SqlDataAdapter(partner3, connection);
            var partDS3 = new DataSet();
            partAD3.Fill(partDS3);
            cboSpedicija.DataSource = partDS3.Tables[0];
            cboSpedicija.DisplayMember = "PaNaziv";
            cboSpedicija.ValueMember = "PaSifra";

            var dir2 = "Select ID, (Oznaka + ' ' + Naziv) as Naziv from VrstaCarinskogPostupka order by Naziv";
            var dirAD2 = new SqlDataAdapter(dir2, connection);
            var dirDS2 = new DataSet();
            dirAD2.Fill(dirDS2);
            cboReexport.DataSource = dirDS2.Tables[0];
            cboReexport.DisplayMember = "Naziv";
            cboReexport.ValueMember = "ID";

            var adr = "Select ID, (Naziv + ' - ' + UNKod) as Naziv From VrstaRobeADR order by (UNKod + ' ' + Naziv)";
            var adrSAD = new SqlDataAdapter(adr, connection);
            var adrSDS = new DataSet();
            adrSAD.Fill(adrSDS);
            txtADR.DataSource = adrSDS.Tables[0];
            txtADR.DisplayMember = "Naziv";
            txtADR.ValueMember = "ID";
        }

        private void frmPrijemKontejneraKamionLegetIzvoz_Load(object sender, EventArgs e)
        {
           

            //where Posiljalac = 1
            var select1 = " Select Distinct PaSifra as ID, PaNaziv as Naziv From Partnerji  order by PaNaziv";
            var s_connection1 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection1 = new SqlConnection(s_connection1);
            var c1 = new SqlConnection(s_connection1);
            var dataAdapter1 = new SqlDataAdapter(select1, c1);

            var commandBuilder1 = new SqlCommandBuilder(dataAdapter1);
            var ds1 = new DataSet();
            dataAdapter1.Fill(ds1);
            cboPosiljalac.DataSource = ds1.Tables[0];
            cboPosiljalac.DisplayMember = "Naziv";
            cboPosiljalac.ValueMember = "ID";
            //where Primalac = 1 
            var select2 = " Select Distinct PaSifra as ID, PaNaziv as Naziv From Partnerji order by PaNaziv";
            var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboPrimalac.DataSource = ds2.Tables[0];
            cboPrimalac.DisplayMember = "Naziv";
            cboPrimalac.ValueMember = "ID";
            //where Vlasnik =1 
            var select3 = " Select Distinct PaSifra as ID, PaNaziv as Naziv From Partnerji  order by PaNaziv";
            var s_connection3 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboVlasnikKontejnera.DataSource = ds3.Tables[0];
            cboVlasnikKontejnera.DisplayMember = "Naziv";
            cboVlasnikKontejnera.ValueMember = "ID";


            var select4 = " Select Distinct ID, Naziv From TipKontenjera order by Naziv";
            var s_connection4 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection4 = new SqlConnection(s_connection4);
            var c4 = new SqlConnection(s_connection4);
            var dataAdapter4 = new SqlDataAdapter(select4, c4);

            var commandBuilder4 = new SqlCommandBuilder(dataAdapter4);
            var ds4 = new DataSet();
            dataAdapter4.Fill(ds4);
            cboTipKontejnera.DataSource = ds4.Tables[0];
            cboTipKontejnera.DisplayMember = "Naziv";
            cboTipKontejnera.ValueMember = "ID";

            var select5 = " Select Distinct ID, (Cast(BrVoza as nvarchar(10)) + '-' + Relacija + '-' + Cast(Cast(VremePolaskaO as DateTime) as Nvarchar(12))) as IDVoza From Voz where dolazeci = 0";
            var s_connection5 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection5 = new SqlConnection(s_connection5);
            var c5 = new SqlConnection(s_connection5);
            var dataAdapter5 = new SqlDataAdapter(select5, c5);

            var commandBuilder5 = new SqlCommandBuilder(dataAdapter5);
            var ds5 = new DataSet();
            dataAdapter5.Fill(ds5);
            cboBukingOtpreme.DataSource = ds5.Tables[0];
            cboBukingOtpreme.DisplayMember = "IdVoza";
            cboBukingOtpreme.ValueMember = "ID";

            /*
            var dir = "Select ID,Naziv from DirigacijaKontejneraZa order by ID";
            var dirAD = new SqlDataAdapter(dir, conn);
            var dirDS = new DataSet();
            dirAD.Fill(dirDS);
            cbDirigacija.DataSource = dirDS.Tables[0];
            cbDirigacija.DisplayMember = "Naziv";
            cbDirigacija.ValueMember = "ID";
            */


            var select6 = " Select ID,Naziv from DirigacijaKontejneraZa order by ID ";
            var s_connection6 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection6 = new SqlConnection(s_connection6);
            var c6 = new SqlConnection(s_connection6);
            var dataAdapter6 = new SqlDataAdapter(select6, c6);

            var commandBuilder6 = new SqlCommandBuilder(dataAdapter6);
            var ds6 = new DataSet();
            dataAdapter6.Fill(ds6);
            cboStatusKontejnera.DataSource = ds6.Tables[0];
            cboStatusKontejnera.DisplayMember = "Naziv";
            cboStatusKontejnera.ValueMember = "ID";
            /*
            var select7 = " Select Distinct ID, IdVoza From BukingVoza ";
            var s_connection7 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection7 = new SqlConnection(s_connection7);
            var c7 = new SqlConnection(s_connection7);
            var dataAdapter7 = new SqlDataAdapter(select7, c7);

            var commandBuilder7 = new SqlCommandBuilder(dataAdapter7);
            var ds7 = new DataSet();
            dataAdapter7.Fill(ds7);
            cboBukingOtpreme.DataSource = ds7.Tables[0];
            cboBukingOtpreme.DisplayMember = "IdVoza";
            cboBukingOtpreme.ValueMember = "ID";
            */
            var select8 = "  Select Distinct ID, (Cast(BrVoza as nvarchar(10)) + '-' + Relacija) as IdVoza   From Voz ";
            var s_connection8 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection8 = new SqlConnection(s_connection8);
            var c8 = new SqlConnection(s_connection8);
            var dataAdapter8 = new SqlDataAdapter(select8, c8);

            var commandBuilder8 = new SqlCommandBuilder(dataAdapter8);
            var ds8 = new DataSet();
            dataAdapter8.Fill(ds8);
            cboBukingPrijema.DataSource = ds8.Tables[0];
            cboBukingPrijema.DisplayMember = "IdVoza";
            cboBukingPrijema.ValueMember = "ID";
            //where Organizator = 1
            var select9 = " Select Distinct PaSifra as ID, PaNaziv as Naziv From Partnerji  order by PaNaziv";
            var s_connection9 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection9 = new SqlConnection(s_connection9);
            var c9 = new SqlConnection(s_connection9);
            var dataAdapter9 = new SqlDataAdapter(select9, c9);

            var commandBuilder9 = new SqlCommandBuilder(dataAdapter9);
            var ds9 = new DataSet();
            dataAdapter9.Fill(ds9);
            cboOrganizator.DataSource = ds9.Tables[0];
            cboOrganizator.DisplayMember = "Naziv";
            cboOrganizator.ValueMember = "ID";


            var select10 = " Select Distinct ID, Naziv  From PredefinisanePoruke order by ID";
            var s_connection10 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection10 = new SqlConnection(s_connection10);
            var c10 = new SqlConnection(s_connection10);
            var dataAdapter10 = new SqlDataAdapter(select10, c10);

            var commandBuilder10 = new SqlCommandBuilder(dataAdapter10);
            var ds10 = new DataSet();
            dataAdapter10.Fill(ds10);
            cboPredefinisanePoruke.DataSource = ds10.Tables[0];
            cboPredefinisanePoruke.DisplayMember = "Naziv";
            cboPredefinisanePoruke.ValueMember = "ID";

            var dir3 = "Select ID,Naziv from VrstePostupakaUvoz order by ID";
            var dirAD3 = new SqlDataAdapter(dir3, s_connection10);
            var dirDS3 = new DataSet();
            dirAD3.Fill(dirDS3);
            cbPostupak.DataSource = dirDS3.Tables[0];
            cbPostupak.DisplayMember = "Naziv";
            cbPostupak.ValueMember = "ID";
           
            
            var np4 = "Select ID,(Oznaka + ' ' + Naziv) as Naziv from uvNacinPakovanja order by Naziv";
            var npAD4 = new SqlDataAdapter(np4, s_connection10);
            var npDS4 = new DataSet();
            npAD4.Fill(npDS4);
            cbNacinPakovanja.DataSource = npDS4.Tables[0];
            cbNacinPakovanja.DisplayMember = "Naziv";
            cbNacinPakovanja.ValueMember = "ID";

            usao = 1;
        }

        private void VratiPodatkeStavke(string IdNadredjenog, int RB)
        {
            int KontejnerID = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT [ID],[IDNadredjenog]       ,[BrojKontejnera],[BrojVagona] " +
             " ,[Granica],[BrojOsovina],[SopstvenaMasa],[Tara] " +
             " ,[Neto],[Posiljalac],[Primalac],[VlasnikKontejnera] " +
             " ,[TipKontejnera],[VrstaRobe],[Buking],[StatusKontejnera] " +
             " ,[BrojPlombe],[PlaniraniLager],[IdVoza],[VremeDolaska] " +
             " ,[VremePripremljen],[VremeOdlaska],[Datum],[Korisnik] " +
             " ,[RB],[BrojPlombe2],[Organizator], BukingBrodar, NapomenaS, NapomenaSS " +
             "  ,[PeriodSkladistenjaOd]      ,[PeriodSkladistenjaDo]      ,[BTTORobe] " +
           "  ,[KontejnerID]      ,[BTTOKOntejnera]      ,[Napomena2]      ,[PostupakSaRobom]" +
             " FROM [dbo].[PrijemKontejneraVozStavke] " +
             " where IdNadredjenog = " + txtSifra.Text + " and RB = " + RB, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {


                txtKontejnerID.Text = dr["KontejnerID"].ToString();
                txtStavka.Text = dr["ID"].ToString();
                txtRB.Text = dr["RB"].ToString();
                txtBrojKontejnera.Text = dr["BrojKontejnera"].ToString();
                txtVagon.Text = dr["BrojVagona"].ToString();
                txtTara.Value = Convert.ToDecimal(dr["Tara"].ToString());
                txtNeto.Value = Convert.ToDecimal(dr["Neto"].ToString());
                txtGranica.Value = Convert.ToDecimal(dr["Granica"].ToString());
                txtBrojOsovina.Value = Convert.ToDecimal(dr["BrojOsovina"].ToString());
                txtSopstvenaMasa.Value = Convert.ToDecimal(dr["SopstvenaMasa"].ToString());
                cboPosiljalac.SelectedValue = Convert.ToInt32(dr["Posiljalac"].ToString());
                cboPrimalac.SelectedValue = Convert.ToInt32(dr["Primalac"].ToString());
                cboVlasnikKontejnera.SelectedValue = Convert.ToInt32(dr["VlasnikKontejnera"].ToString());
                cboTipKontejnera.SelectedValue = Convert.ToInt32(dr["TipKontejnera"].ToString());
                cboVrstaRobe.SelectedValue = Convert.ToInt32(dr["VrstaRobe"].ToString());
                cboStatusKontejnera.SelectedValue = Convert.ToInt32(dr["StatusKontejnera"].ToString()); //DirigacijaKontejneraZa
                cboBukingOtpreme.SelectedValue = Convert.ToInt32(dr["Buking"].ToString());
                cboOrganizator.SelectedValue = Convert.ToInt32(dr["Organizator"].ToString());
                txtBukingBrodar.Text = dr["BukingBrodar"].ToString();
                dtpVremeDolaska.Value = Convert.ToDateTime(dr["VremeDolaska"].ToString());
                dtpPerodSkladistenjaOd.Value = Convert.ToDateTime(dr["PeriodSkladistenjaOd"].ToString());
                dtpPeriodSkladistenjaDo.Value = Convert.ToDateTime(dr["PeriodSkladistenjaDo"].ToString());
                txtKontejnerID.Text = dr["KontejnerID"].ToString();
                txtBrojPlombe.Text = dr["BrojPlombe"].ToString();
                txtBrojPlombe2.Text = dr["BrojPlombe2"].ToString();
                //Napomena
                txtNapomenaS.Text = dr["NapomenaS"].ToString();
                bttoRobe.Value = Convert.ToDecimal(dr["BTTORobe"].ToString());
                bttoKontejnera.Value = Convert.ToDecimal(dr["BTTOKOntejnera"].ToString());
                txtNapomenaS2.Text = dr["Napomena2"].ToString();
                cbPostupak.SelectedValue = Convert.ToInt32(dr["PostupakSaRobom"].ToString());
                // KontejnerID = Convert.ToInt32(dr["KontejnerID"].ToString()); // PostupakSaRobom

            }

            con.Close();

            // VratiPodatkeKontejnerID(KontejnerID);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txtSifra.Text = row.Cells[2].Value.ToString();
                        txtStavka.Text = row.Cells[0].Value.ToString();
                        PopuniPolja();
                        RefreshDataGridRN();
                       // VratiPodatkeStavke(txtSifra.Text, Convert.ToInt32(row.Cells[1].Value.ToString()));
                       
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            string IDUsluge = "0";
            foreach (DataGridViewRow row in dataGridView8.Rows)
            {

                if (row.Selected == true)
                {
                    IDUsluge = row.Cells[0].Value.ToString();

                }
            }

            if (IDUsluge == "0")

            {
                DialogResult dialogResult = MessageBox.Show("Niste obeležili ni jednu uslugu, da li nastavljate dalje", "Usluga?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //Zadnji je 
                    RadniNalozi.RN4PrijemPlatforme ppl = new RadniNalozi.RN4PrijemPlatforme(txtSifra.Text, txtRegBrKamiona.Text, KorisnikCene, IDUsluge, 2, txtNalogID.Text);
                    ppl.Show();
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }

            }
            else
            {
                RadniNalozi.RN4PrijemPlatforme ppl = new RadniNalozi.RN4PrijemPlatforme(txtSifra.Text, txtRegBrKamiona.Text, KorisnikCene, IDUsluge, 1, txtNalogID.Text);
                ppl.Show();
            }
        }

        private void txtKontejnerID_TextChanged(object sender, EventArgs e)
        {
            
                FillDGUsluge();
                PopuniPolja();
              //  FillDG3();

        }

        private void PopuniPolja()
        {
            if (txtStavka.Text == "")
                return;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT PrijemKontejneraVozStavke.ID as ID, PrijemKontejneraVozStavke.RB, " +
" PrijemKontejneraVozStavke.IDNadredjenog, PrijemKontejneraVozStavke.KontejnerID, " +
" PrijemKontejneraVozStavke.BrojKontejnera, TipKontenjera.Naziv AS TipKontejnera, " +
" PrijemKontejneraVozStavke.NajavaID, " +
" IzvozKonacna.BrojKontejnera, IzvozKonacna.NacinPakovanja," +
" IzvozKonacna.BrojVagona, IzvozKOnacna.VrstaKontejnera, Partnerji_1.PaSifra as Brodar, IzvozKOnacna.BookingBrodara, " +
" IzvozKOnacna.BrodskaPlomba, IzvozKonacna.OstalePlombe, " +
" IzvozKOnacna.BrojKoletaO, IzvozKOnacna.BrutoRobe, IzvozKOnacna.BrutoRobeO, IzvozKOnacna.CBMO, IzvozKOnacna.Tara, IzvozKOnacna.TaraZ," +
" IzvozKOnacna.NetoRobe, IzvozKOnacna.PeriodSkladistenjaOd, IzvozKOnacna.PlaniraniDatumUtovara," +
" IzvozKOnacna.PeriodSkladistenjaDo, Partnerji_2.PASifra as Izvoznik, INSTret.ID as InspekciskiTretman, " +
" IZvozKonacna.Spedicija,IzvozKonacna.KontaktSpeditera, IzvozKonacna.MestoPreuzimanja as PICKUPCNT,Carinarnice.ID as Carina, IzvozKonacna.ADR as ADR, " +
" IzvozKonacna.NapomenaReexport as Reexport, IzvozKonacna.DodatneNapomeneDrumski, IzvozKonacna.VGMBrod , IzvozKonacna.Vaganje " +
" FROM PrijemKontejneraVozStavke " +
" inner join IzvozKonacna on IzvozKonacna.ID = PrijemKontejneraVozStavke.KontejnerID " +
" INNER JOIN  Partnerji AS Partnerji_1 ON IzvozKonacna.Brodar = Partnerji_1.PaSifra " +
" INNER JOIN  Partnerji AS Partnerji_2 ON IzvozKonacna.Izvoznik = Partnerji_2.PaSifra " +
" INNER JOIN  InspekciskiTretman AS INSTret ON IZvozKOnacna.Inspekcija = INSTret.ID " +
" INNER JOIN TipKontenjera ON PrijemKontejneraVozStavke.TipKontejnera = TipKontenjera.ID " +
" INNER JOIN  Carinarnice ON IzvozKonacna.MestoCarinjenja = Carinarnice.ID " +
" INNER JOIN  Partnerji AS Partnerji_3 ON IzvozKonacna.Spedicija = Partnerji_3.PaSifra " +
" inner join KontejnerskiTerminali on KontejnerskiTerminali.ID = IzvozKonacna.MestoPreuzimanja " +
" inner join VrstaRobeADR on VrstaRobeADR.ID = IzvozKonacna.ADR " +
" inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = IzvozKonacna.NapomenaReexport " +
" where PrijemKontejneraVozStavke.Id = " + Convert.ToInt32(txtStavka.Text) , con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtNalogID.Text = dr["NajavaID"].ToString();
                txtStavka.Text = dr["ID"].ToString();
                txtRB.Text = dr["RB"].ToString(); 
                txtKontejnerID.Text = dr["KontejnerID"].ToString();
                txtBrojKontejnera.Text = dr["BrojKontejnera"].ToString();
                cboTipKontejnera.SelectedValue = Convert.ToInt32(dr["VrstaKontejnera"].ToString());
                txtVagon.Text = "";
                cboVlasnikKontejnera.SelectedValue = Convert.ToInt32(dr["Brodar"].ToString());
                txtTara.Value = Convert.ToDecimal(dr["Tara"].ToString());
                txtBukingBrodar.Text=dr["BookingBrodara"].ToString();
                txtBrojPlombe.Text = dr["BrodskaPlomba"].ToString();
                txtBrojPlombe2.Text = dr["OstalePlombe"].ToString();
                cboIzvoznik.SelectedValue = Convert.ToInt32(dr["Izvoznik"].ToString());
                bttoRobeOtpremnica.Value = Convert.ToDecimal(dr["BrutoRobeO"].ToString());
                txtCBMOTP.Text =dr["DodatneNapomeneDrumski"].ToString();
                //Napomena

                cboPPCNT.SelectedValue = Convert.ToInt32(dr["PICKUPCNT"].ToString());
                cboCarina.SelectedValue = Convert.ToInt32(dr["Carina"].ToString());
                cboSpedicija.SelectedValue = Convert.ToInt32(dr["Spedicija"].ToString());
                txtKontaktSpeditera.Text = dr["KontaktSpeditera"].ToString();
                txtDodatneNapomene.Text = dr["DodatneNapomeneDrumski"].ToString();
                cboReexport.SelectedValue = Convert.ToInt32(dr["Reexport"].ToString());
                txtADR.SelectedValue = Convert.ToInt32(dr["ADR"].ToString());
                //Ovde videti da li sam pogodio dobro polje VGMBrod ima i VGMBrod2
                txVGMBrodBruto.Value = Convert.ToDecimal(dr["VGMBrod"].ToString());

                dtpPeriodSkladistenjaOd.Value = Convert.ToDateTime(dr["PeriodSkladistenjaOd"].ToString());
                dtpPeriodSkladistenjaDo.Value = Convert.ToDateTime(dr["PeriodSkladistenjaDo"].ToString());
                cbNacinPakovanja.SelectedValue = Convert.ToInt32(dr["NacinPakovanja"].ToString());
                txtTaraKontejnera.Value = Convert.ToDecimal(dr["Tara"].ToString());
                txtTaraKontejneraZ.Value = Convert.ToDecimal(dr["TaraZ"].ToString());
                dtpPlanUtovara.Value = Convert.ToDateTime(dr["PlaniraniDatumUtovara"].ToString());
                if (dr["Vaganje"].ToString() == "1")
                { 
                chkVaganje.Checked = true;
                }
                else { chkVaganje.Checked =  false; }
            }

            con.Close();

        }

        private void FillDGUsluge()
        {


            if (txtKontejnerID.Text == "")
            {
                return;
            }
            var select = "";

            select = "select  RadniNalogInterni.ID as RNID, RadniNalogInterni.StatusIzdavanja, IzvozKonacnaVrstaManipulacije.ID as ID, " +
" IzvozKonacna.BrojKontejnera, IzvozKonacnaVrstaManipulacije.IDNadredjena as KontejnerID,  IzvozKonacnaVrstaManipulacije.Kolicina, " +
" VrstaManipulacije.ID as ManipulacijaID,VrstaManipulacije.Naziv as ManipulacijaNaziv,  OrganizacioneJedinice.ID, " +
" OrganizacioneJedinice.Naziv as OrganizacionaJedinica,  RadniNalogInterni.DatumIzdavanja, RadniNalogInterni.Uradjen, Partnerji.PaNaziv as Nalogodavac, " +
" RadniNalogInterni.Pokret, RadniNalogInterni.StatusKontejnera,KontejnerStatus.Naziv " +
" from RadniNalogInterni " +
" Inner  join VrstaManipulacije on VrstaManipulacije.ID = RadniNalogInterni.IDManipulacijaJED " +
" inner join IzvozKonacnaVrstaManipulacije on IzvozKonacnaVrstaManipulacije.ID = RadniNalogInterni.KonkretaIDUsluge " +
" inner join IzvozKonacna on IzvozKonacna.ID = RadniNalogInterni.BrojOsnov " +
" inner join PArtnerji on IzvozKonacnaVrstaManipulacije.Platilac = PArtnerji.PaSifra " +
" inner join OrganizacioneJedinice on OrganizacioneJedinice.ID = RadniNalogInterni.OjIzdavanja " +
" inner join KontejnerStatus on KontejnerStatus.ID = RadniNalogInterni.StatusKontejnera where RadniNalogInterni.BrojOsnov = " + Convert.ToInt32(txtKontejnerID.Text);



            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView8.ReadOnly = true;
            dataGridView8.DataSource = ds.Tables[0];


            PodesiDatagridView(dataGridView8);

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView8.Columns[0];
            dataGridView8.Columns[0].HeaderText = "RNID";
            dataGridView8.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView8.Columns[1];
            dataGridView8.Columns[1].HeaderText = "STATUS";
            dataGridView8.Columns[1].Width = 70;

            DataGridViewColumn column3 = dataGridView8.Columns[2];
            dataGridView8.Columns[2].HeaderText = "VID";
            dataGridView8.Columns[2].Visible = false;
            dataGridView8.Columns[2].Width = 70;

            DataGridViewColumn column4 = dataGridView8.Columns[3];
            dataGridView8.Columns[3].HeaderText = "MID";
            dataGridView8.Columns[3].Width = 50;

            DataGridViewColumn column5 = dataGridView8.Columns[4];
            dataGridView8.Columns[4].HeaderText = "V";
            dataGridView8.Columns[4].Width = 30;

            DataGridViewColumn column6 = dataGridView8.Columns[5];
            dataGridView8.Columns[5].HeaderText = "KOL";
            dataGridView8.Columns[5].Width = 30;

            DataGridViewColumn column7 = dataGridView8.Columns[6];
            dataGridView8.Columns[6].HeaderText = "VUID";
            dataGridView8.Columns[6].Width = 30;

            DataGridViewColumn column8 = dataGridView8.Columns[7];
            dataGridView8.Columns[7].HeaderText = "USLUGA";
            dataGridView8.Columns[7].Width = 170;

        }

        private void txtNalogID_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
