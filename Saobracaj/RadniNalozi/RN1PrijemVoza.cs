using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Diagnostics.CodeAnalysis;
using Saobracaj;
using System.Drawing;
using Saobracaj.Dokumenta;
using Syncfusion.Windows.Forms;
using System.Drawing.Imaging;
using Saobracaj.Sifarnici;

//
namespace Saobracaj.RadniNalozi
{
    public partial class RN1PrijemVoza : Form
    {
        private string connect = Sifarnici.frmLogovanje.connectionString;
        private bool status = false;
        string KorisnikTekuci = Saobracaj.Sifarnici.frmLogovanje.user.ToString();
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

        public RN1PrijemVoza()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");

            InitializeComponent();
            FillGV();
            FillCombo();
            txtDatumRasporeda.Value = DateTime.Now;
            ChangeTextBox();

        }

        public RN1PrijemVoza(string Korisnik)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");

            InitializeComponent();
            FillGV();
            FillCombo();
            KorisnikTekuci = Korisnik;
            txtDatumRasporeda.Value = DateTime.Now;
            ChangeTextBox();
        }


        public RN1PrijemVoza(int brojRN)
        {
            //Prijem voza iz Otvori Radni nalozi Interni
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");

            InitializeComponent();
            BrojRN = brojRN;
            FillGVOtvoriIzRadnogNalogaInterni();
          //  FillGV();
            FillCombo();
            KorisnikTekuci = frmLogovanje.user.ToString();
            txtDatumRasporeda.Value = DateTime.Now;
            ChangeTextBox();
        }

        public RN1PrijemVoza(string Korisnik, string IDVOza, string IDUsluge, string PrijemID)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3tib3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");

            InitializeComponent();

            FillCombo();
            txtNalogIzdao.Text = Korisnik;
            cboSaVoznog.SelectedValue = Convert.ToInt32(IDVOza);
            NapuniVrstuUsluge(IDUsluge);
            txtNalogID.Text = IDUsluge;
            // cboUsluge.SelectedValue = Convert.ToInt32(IDUsluge);
            txtPrijemID.Text = PrijemID;
            RefreshStavkeVoza(PrijemID);
            FillGV();
            KorisnikTekuci = Korisnik;
            txtDatumRasporeda.Value = DateTime.Now;
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
            cboUsluge.DataSource = dsUsluge.Tables[0];
            cboUsluge.DisplayMember = "Naziv";
            cboUsluge.ValueMember = "ID";

            //cboUsluge.SelectedValue = Convert.ToInt32(IDUsluga);
        }
        private void RefreshStavkeVoza(string IDVOza)
        {
            /*
            var select =
        " SELECT PrijemKontejneraVozStavke.ID, PrijemKontejneraVozStavke.RB, PrijemKontejneraVozStavke.IDNadredjenog,   PrijemKontejneraVozStavke.KontejnerID, " +
        " PrijemKontejneraVozStavke.BrojKontejnera,  TipKontenjera.Naziv AS TipKontejnera,PrijemKontejneraVozStavke.BrojVagona,  PrijemKontejneraVozStavke.Granica as GranicaTovarenja, " +
        " PrijemKontejneraVozStavke.BrojOsovina,  PrijemKontejneraVozStavke.SopstvenaMasa as TaraVagona,  PrijemKontejneraVozStavke.Tara,  PrijemKontejneraVozStavke.Neto, " +
        " PrijemKontejneraVozStavke.BTTORobe, PrijemKontejneraVozStavke.BTTOKontejnera, " +
        " Partnerji_4.PANaziv as Brodar, UvozKonacna.BukingBrodara AS BukingBrodar,PrijemKontejneraVozStavke.BrojPlombe, " +
        " PrijemKontejneraVozStavke.BrojPlombe2, PrijemKontejneraVozStavke.PeriodSkladistenjaOd, " +
        " PrijemKontejneraVozStavke.PeriodSkladistenjaDo, DirigacijaKOntejneraZa.Naziv as DirigacijaKOntejneraZa, VrstePostupakaUvoz.Naziv as PostupakSaRobom," +
        " Partnerji_2.PaNaziv AS Nalogodavac_Za_DrumskiPrevoz, Partnerji_3.PaNaziv AS Nalogodavac_Za_Usluge," +
         "  PrijemKontejneraVozStavke.PlaniraniLager as DIREKTNI_INDIREKTNI,    PrijemKontejneraVozStavke.NapomenaS, PrijemKontejneraVozStavke.Napomena2, " +
        " PrijemKontejneraVozStavke.Datum, PrijemKontejneraVozStavke.Korisnik " +
        " FROM  PrijemKontejneraVozStavke " +
        " inner join UvozKonacna on UvozKonacna.ID = PrijemKontejneraVozStavke.KontejnerID " +
        " INNER JOIN  Partnerji AS Partnerji_1 ON UvozKonacna.Nalogodavac1 = Partnerji_1.PaSifra " +
        " INNER JOIN  Partnerji AS Partnerji_2 ON UvozKonacna.Nalogodavac2 = Partnerji_2.PaSifra " +
        " INNER JOIN  Partnerji AS Partnerji_3 ON UvozKonacna.Nalogodavac3 = Partnerji_3.PaSifra " +
        " INNER JOIN  Partnerji AS Partnerji_4 ON UvozKonacna.Brodar = Partnerji_4.PaSifra " +
        " INNER JOIN TipKontenjera ON PrijemKontejneraVozStavke.TipKontejnera = TipKontenjera.ID " +
        " INNER join DirigacijaKontejneraZa on DirigacijaKontejneraZa.ID = PrijemKontejneraVozStavke.StatusKontejnera " +
        " INNER JOIN  Voz ON PrijemKontejneraVozStavke.IdVoza = Voz.ID " +
        " INNER JOIN VrstePostupakaUvoz ON VrstePostupakaUvoz.id = PrijemKontejneraVozStavke.PostupakSaRobom where IdNadredjenog =  " + txtPrijemID.Text + " order by RB";
            // and UvozKonacnaVrstaManipulacije.IDVrstaManipulacije = " + cboUsluge.SelectedValue  +
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = false;
            dataGridView2.DataSource = ds.Tables[0];
            */


        }

        private void FillGVPoVozu()
        {
            var select = "select RNPrijemVoza.ID,BrojKontejnera, TipKontenjera.Naziv as VrstaKontejnera, DatumRasporeda, NalogIzdao, Voz.BrVoza, Voz.NazivVoza, " +
                "NaSkladiste,Skladista.Naziv as Sklad,  PArtnerji.PaNaziv as Uvoznik, p2.PaNaziv as Brodar, VrstaManipulacije.Naziv as Usliga, BrojPlombe," +
                " RNPrijemVoza.Napomena, RNPrijemVoza.PrijemID,RNPrijemVoza.NalogID, DatumRealizacijeVP, NalogRealizovaoVP, ZavrsenVP, NapomenaPlombe1, NapomenaPlombe2, " +
                "DatumRealizacije, NalogRealizovao, Zavrsen  from RNPrijemVoza " +
" inner join TipKontenjera on TipKontenjera.ID = RNPrijemVoza.VrstaKontejnera " +
" inner join Voz on RNPrijemVoza.SaVoznogSredstva = Voz.ID " +
" inner join Skladista on Skladista.ID = NaSkladiste " +
" inner join Partnerji on Partnerji.PaSifra = RNPrijemVoza.Uvoznik " +
" inner join Partnerji p2 on p2.PaSifra = RNPrijemVoza.NazivBrodara " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = IdUsluge " +
" where Voz.ID = " + Convert.ToInt32(cboSaVoznog.SelectedValue) + 
" order by RNPrijemVoza.ID desc";
            SqlConnection conn = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            PodesiDatagridView(dataGridView1);
        }
        private void FillGVOtvoriIzRadnogNalogaInterni()
        {
            var select = "select RNPrijemVoza.ID,BrojKontejnera, TipKontenjera.Naziv as VrstaKontejnera, DatumRasporeda, NalogIzdao, " +
                " Voz.BrVoza, Voz.NazivVoza, NaSkladiste,Skladista.Naziv as Sklad,  PArtnerji.PaNaziv as Uvoznik, p2.PaNaziv as Brodar, VrstaManipulacije.Naziv as Usliga, BrojPlombe, BrojPlombe2,  NapomenaPlombe1, NapomenaPlombe2, " +
                " RNPrijemVoza.Napomena, RNPrijemVoza.PrijemID,RNPrijemVoza.NalogID, DatumRealizacije, NalogRealizovao, Zavrsen  from RNPrijemVoza " +
" inner join TipKontenjera on TipKontenjera.ID = RNPrijemVoza.VrstaKontejnera " +
" inner join Voz on RNPrijemVoza.SaVoznogSredstva = Voz.ID " +
" inner join Skladista on Skladista.ID = NaSkladiste " +
" inner join Partnerji on Partnerji.PaSifra = RNPrijemVoza.Uvoznik " +
" inner join Partnerji p2 on p2.PaSifra = RNPrijemVoza.NazivBrodara " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = IdUsluge " +
" where RNPrijemVoza.ID = " + BrojRN + "order by RNPrijemVoza.ID desc";
            SqlConnection conn = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            PodesiDatagridView(dataGridView1);
        }
        private void FillGV()
        {
            var select = "select RNPrijemVoza.ID,BrojKontejnera, TipKontenjera.Naziv as VrstaKontejnera, DatumRasporeda, NalogIzdao, " +
                " Voz.BrVoza, Voz.NazivVoza, NaSkladiste,Skladista.Naziv as Sklad,  PArtnerji.PaNaziv as Uvoznik, p2.PaNaziv as Brodar, VrstaManipulacije.Naziv as Usliga, BrojPlombe, BrojPlombe2,  NapomenaPlombe1, NapomenaPlombe2, " +
                " RNPrijemVoza.Napomena, RNPrijemVoza.PrijemID,RNPrijemVoza.NalogID, DatumRealizacije, NalogRealizovao, Zavrsen  from RNPrijemVoza " +
" inner join TipKontenjera on TipKontenjera.ID = RNPrijemVoza.VrstaKontejnera " +
" inner join Voz on RNPrijemVoza.SaVoznogSredstva = Voz.ID " +
" inner join Skladista on Skladista.ID = NaSkladiste " +
" inner join Partnerji on Partnerji.PaSifra = RNPrijemVoza.Uvoznik " +
" inner join Partnerji p2 on p2.PaSifra = RNPrijemVoza.NazivBrodara " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = IdUsluge order by RNPrijemVoza.ID desc";
            SqlConnection conn = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            PodesiDatagridView(dataGridView1);
        }
        private void FillGVSamoPrijem()
        {
            var select = "select RNPrijemVoza.ID,BrojKontejnera, TipKontenjera.Naziv as VrstaKontejnera, DatumRasporeda, NalogIzdao, Voz.BrVoza, NaSkladiste,Skladista.Naziv as Sklad,  PArtnerji.PaNaziv as Uvoznik, p2.PaNaziv as Brodar, VrstaManipulacije.Naziv as Usliga, BrojPlombe, RNPrijemVoza.Napomena, RNPrijemVoza.PrijemID,RNPrijemVoza.NalogID, DatumRealizacije, NalogRealizovao, Zavrsen  from RNPrijemVoza " +
" inner join TipKontenjera on TipKontenjera.ID = RNPrijemVoza.VrstaKontejnera " +
" inner join Voz on RNPrijemVoza.SaVoznogSredstva = Voz.ID " +
" inner join Skladista on Skladista.ID = NaSkladiste " +
" inner join Partnerji on Partnerji.PaSifra = RNPrijemVoza.Uvoznik " +
" inner join Partnerji p2 on p2.PaSifra = RNPrijemVoza.NazivBrodara " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = IdUsluge";
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
            cbovrstakontejnera.DataSource = daDS.Tables[0];
            cbovrstakontejnera.DisplayMember = "SkNaziv";
            cbovrstakontejnera.ValueMember = "ID";

            txtNalogIzdao.Text = Sifarnici.frmLogovanje.user.ToString().TrimEnd();
            //usluge->Manipulacije

            var vSredstvo = "Select Distinct ID, (Cast(NAzivVoza as nvarchar(10)) + '-' + Relacija) as IdVoza   From Voz";
            var daVS = new SqlDataAdapter(vSredstvo, conn);
            var dsVS = new DataSet();
            daVS.Fill(dsVS);
            cboSaVoznog.DataSource = dsVS.Tables[0];
            cboSaVoznog.DisplayMember = "IdVoza";
            cboSaVoznog.ValueMember = "ID";

            var partner2 = "Select PaSifra,PaNaziv From Partnerji  order by PaSifra";
            var partAD2 = new SqlDataAdapter(partner2, conn);
            var partDS2 = new DataSet();
            partAD2.Fill(partDS2);
            cboUvoznik.DataSource = partDS2.Tables[0];
            cboUvoznik.DisplayMember = "PaNaziv";
            cboUvoznik.ValueMember = "PaSifra";

            var partner7 = "Select PaSifra,PaNaziv From Partnerji order by PaSifra";
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


            var sklad = "select ID,naziv from Skladista order by ID";
            var daSklad = new SqlDataAdapter(sklad, conn);
            var dsSklad = new DataSet();
            daSklad.Fill(dsSklad);
            cboNaSkladiste.DataSource = dsSklad.Tables[0];
            cboNaSkladiste.DisplayMember = "Naziv";
            cboNaSkladiste.ValueMember = "ID";

            var pozicija = "Select Id,Opis from Pozicija";
            var daPoz = new SqlDataAdapter(pozicija, conn);
            var dsPoz = new DataSet();
            daPoz.Fill(dsPoz);
            cboNaPoziciju.DataSource = dsPoz.Tables[0];
            cboNaPoziciju.DisplayMember = "Opis";
            cboNaPoziciju.ValueMember = "ID";



            var usluge = "Select VrstaManipulacije.ID,VrstaManipulacije.Naziv from VrstaManipulacije ";
            var daUsluge = new SqlDataAdapter(usluge, conn);
            var dsUsluge = new DataSet();
            daUsluge.Fill(dsUsluge);
            cboUsluge.DataSource = dsUsluge.Tables[0];
            cboUsluge.DisplayMember = "Naziv";
            cboUsluge.ValueMember = "ID";
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            InsertRN rn = new InsertRN();
            DateTime datumRasporeda = Convert.ToDateTime(txtDatumRasporeda.Value);
            DateTime datumRealizacije = Convert.ToDateTime(txtDatumRealizacije.Value);

            if (status == true)
            {
                rn.InsRNPPrijemVoza(Convert.ToDateTime(txtDatumRasporeda.Value), txtbrojkontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cbovrstakontejnera.SelectedValue),
                    txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(txtDatumRealizacije.Value), Convert.ToInt32(cboSaVoznog.SelectedValue),
                    txtBrojPlombe.Text.ToString().TrimEnd(), Convert.ToInt32(cboUvoznik.SelectedValue), Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue),
                    Convert.ToInt32(cboNaSkladiste.SelectedValue), Convert.ToInt32(cboNaPoziciju.SelectedValue), Convert.ToInt32(cboUsluge.SelectedValue), "", txtNapomena.Text.ToString().TrimEnd());
            }
            else
            {
                rn.UpdRNPPrijemVoza(Convert.ToInt32(txtID.Text.ToString().TrimEnd()), Convert.ToDateTime(txtDatumRasporeda.Value), txtbrojkontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cbovrstakontejnera.SelectedValue),
                    txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(txtDatumRealizacije.Value), Convert.ToInt32(cboSaVoznog.SelectedValue),
                    txtBrojPlombe.Text.ToString().TrimEnd(), Convert.ToInt32(cboUvoznik.SelectedValue), Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue),
                    Convert.ToInt32(cboNaSkladiste.SelectedValue), Convert.ToInt32(cboNaPoziciju.SelectedValue), Convert.ToInt32(cboUsluge.SelectedValue), txtNalogRealizovao.Text.ToString().TrimEnd(),
                    txtNapomena.Text.ToString().TrimEnd());
            }
            FillGV();
            status = false;
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                InsertRN rn = new InsertRN();
                rn.DelRNPPrijemVoza(Convert.ToInt32(txtID.Text.ToString().TrimEnd()));
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

        private void RN1PrijemVoza_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            
            DialogResult dialogResult = MessageBox.Show("Formirace se RN za sve Interne Naloge po Vrsti usluge ", "Some Title", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                RadniNalozi.InsertRN ir = new InsertRN();
                ir.InsRNPPrijemVozaCeoVoz(Convert.ToDateTime(txtDatumRasporeda.Value), txtNalogIzdao.Text, Convert.ToDateTime(txtDatumRealizacije.Text), Convert.ToInt32(cboSaVoznog.SelectedValue), Convert.ToInt32(cboNaSkladiste.SelectedValue), Convert.ToInt32(cboNaPoziciju.SelectedValue), Convert.ToInt32(cboUsluge.SelectedValue), " ", txtNapomena.Text, Convert.ToInt32(txtPrijemID.Text), Convert.ToInt32(cboNaSkladistePregledac.SelectedValue), KorisnikTekuci);
                FillGV();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }



            // RadniNalozi.InsertRN ir = new InsertRN();
            // ir.InsRNPPrijemVozaCeoVoz(Convert.ToDateTime(txtDatumRasporeda.Value), txtNalogIzdao.Text, Convert.ToDateTime(txtDatumRealizacije.Text), Convert.ToInt32(cboSaVoznog.SelectedValue), Convert.ToInt32(cboNaSkladiste.SelectedValue), Convert.ToInt32(cboNaPoziciju.SelectedValue), Convert.ToInt32(cboUsluge.SelectedValue), "", txtNapomena.Text, Convert.ToInt32(txtPrijemID.Text));
            // FillGV();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillGVSamoPrijem();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //PArametar 2 se odnosi na Radni nalog 
            Saobracaj.RadniNalozi.frmDodelaSkladista ds = new frmDodelaSkladista(txtPrijemID.Text, 1);
            ds.Show();
        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected == true)
                {
                    txtID.Text = row.Cells[0].Value.ToString();
                    VratiPodatkeStavka();
                    FillDG2();
                }
               
            }
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
            PodesiDatagridView(dataGridView1);

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
            dataGridView3.Columns[3].Width = 150;


        }

        private void VratiPodatkeStavka()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" select RNPrijemVoza.ID, RNPrijemVoza.BrojKontejnera, TipKontenjera.ID as VrstaKontejnera, DatumRasporeda," +
                " NalogIzdao, Voz.BrVoza, Voz.ID as VozID, NaSkladiste, NaPozicijuSklad,  PArtnerji.PaSifra as Uvoznik, p2.PaSifra as Brodar, VrstaManipulacije.ID as Usluga, " +
                "BrojPlombe, RNPrijemVoza.Napomena, RNPrijemVoza.PrijemID, RNPrijemVoza.NalogID, DatumRealizacije, NalogRealizovao, " +
                "Zavrsen, NalogRealizovaoVP, ZavrsenVP, VrstaManipulacije.ID as VMID, NapomenaVP, DatumRealizacijeVP,  NapomenaPlombe1, NapomenaPlombe2, PotrebanCIR, NalogRealizovaoCIR, DatumRealizacijeCIR, ZavrsenCIR, BrojPlombe2 from RNPrijemVoza " +
" inner join TipKontenjera on TipKontenjera.ID = RNPrijemVoza.VrstaKontejnera " +
" inner join Voz on RNPrijemVoza.SaVoznogSredstva = Voz.ID " +
" inner join Skladista on Skladista.ID = NaSkladiste " +
" inner join Partnerji on Partnerji.PaSifra = RNPrijemVoza.Uvoznik " +
" inner join Partnerji p2 on p2.PaSifra = RNPrijemVoza.NazivBrodara " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = IdUsluge"  +
             " where RNPrijemVoza.ID = " + txtID.Text, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                cboUsluge.SelectedValue = Convert.ToInt32(dr["VMID"].ToString());
                cboSaVoznog.SelectedValue = Convert.ToInt32(dr["VozID"].ToString());
                txtDatumRasporeda.Value = Convert.ToDateTime(dr["DatumRasporeda"].ToString());
                txtbrojkontejnera.Text = dr["BrojKontejnera"].ToString();
                cbovrstakontejnera.SelectedValue = Convert.ToInt32(dr["VrstaKontejnera"].ToString());
                cboUsluge.SelectedValue = Convert.ToInt32(dr["Usluga"].ToString());
                txtNalogRealizovao.Text = dr["NalogRealizovao"].ToString();
                txtNalogRealizovaoVP.Text = dr["NalogRealizovaoVP"].ToString();
                txtNapomenaPlombe.Text = dr["NapomenaPlombe1"].ToString();
                txtNapomenaPlombe2.Text = dr["NapomenaPlombe2"].ToString();
                txtPrijemID.Text = dr["PrijemID"].ToString();
                txtNalogID.Text = dr["NalogID"].ToString();
                cboNaPoziciju.SelectedValue = Convert.ToInt32(dr["NaPozicijuSklad"].ToString());
                txtNalogIzdao.Text = dr["NalogIzdao"].ToString();
                txtDatumRealizacije.Value = Convert.ToDateTime(dr["DatumRealizacije"].ToString());
                if (dr["DatumRealizacijeVP"].ToString() == "")
                {
                }
                else 
                { 
                    txtDatumRealizacijeVP.Value = Convert.ToDateTime(dr["DatumRealizacijeVP"].ToString()); 
                }
                    
                cboUvoznik.SelectedValue = Convert.ToInt32(dr["Uvoznik"].ToString());
                cboBrodar.SelectedValue = Convert.ToInt32(dr["Brodar"].ToString());
                cboNaSkladiste.SelectedValue = Convert.ToInt32(dr["NaSkladiste"].ToString());
                txtBrojPlombe.Text = dr["BrojPlombe"].ToString();
                txtBrojPlombe2.Text = dr["BrojPlombe2"].ToString();
                txtNapomena.Text = dr["Napomena"].ToString();
                txtNapomenaVP.Text = dr["NapomenaVP"].ToString();


                if (dr["Zavrsen"].ToString() == "1")
                { chkZavrsen.Checked = true; }
                else
                {
                    chkZavrsen.Checked = false;
                }

                if (dr["ZavrsenVP"].ToString() == "1")
                { chkZavrsenVP.Checked = true; }
                else
                {
                    chkZavrsenVP.Checked = false;
                }

                if (dr["PotrebanCIR"].ToString() == "1")
                { 
                    chkPotrebanCIR.Checked = true;
                    button4.Enabled = true;    
                }
                else
                {
                    chkPotrebanCIR.Checked = false;
                    button4.Enabled = false;
                }

                if (dr["ZavrsenCIR"].ToString() == "1")
                {
                    chkZavrsenCIR.Checked = true;
                    txtNalogRealizovaoCIR.Text = dr["NalogRealizovaoCIR"].ToString();
                }
                else
                {
                    chkZavrsenCIR.Checked = false;
                }
            }

            con.Close();
        }


        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            InsertRN up = new InsertRN();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected == true)
                {
                    up.PotvrdiUradjenRN1(Convert.ToInt32(row.Cells[0].Value.ToString()), KorisnikTekuci);
                }
                    
            }
        }

        private void txtNalogID_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            RadniNalozi.frmAnalizaRadnihNaloga arn = new frmAnalizaRadnihNaloga();
            arn.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FillGVPoVozu();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            InsertRN up = new InsertRN();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected == true)
                {
                    up.PotvrdiUradjenRN1VP(Convert.ToInt32(row.Cells[0].Value.ToString()), KorisnikTekuci, txtNapomenaVP.Text, txtNapomenaPlombe.Text, txtNapomenaPlombe2.Text);
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
           // frmCIR(int PrijemID, int Leget)
            frmCIR cir = new frmCIR(Convert.ToInt32(txtPrijemID.Text), 0);
            cir.Show();
        }

        private void chkPotrebanCIR_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPotrebanCIR.Checked == true)
            {
                chkZavrsenCIR.Enabled = true;
                txtNalogRealizovaoCIR.Enabled = true;
                dtpNalogRealizovaoCIR.Enabled = true;
                button4.Enabled = true;

            }
            else
            {
                chkZavrsenCIR.Enabled = false;
                txtNalogRealizovaoCIR.Enabled = false;
                dtpNalogRealizovaoCIR.Enabled = false;
                button4.Enabled = false;
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (chkPotrebanCIR.Checked == true)
            {
                InsertRN up = new InsertRN();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected == true)
                    {
                        up.PotvrdiUradjenRN1CIr(Convert.ToInt32(row.Cells[0].Value.ToString()), KorisnikTekuci);
                    }

                }

            }
            else
            {
                MessageBox.Show("Operacija se izvrsava samo ako je potreban CIR");
            }
           
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            FillGV();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabSplitterPage1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}