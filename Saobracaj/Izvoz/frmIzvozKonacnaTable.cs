using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Izvoz
{
    public partial class frmIzvozKonacnaTable : Form
    {
        int Selektovani = 0;
        private Keys keyData;
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

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
                // toolStripHeader.Visible = false;
                panelHeader.Visible = true;
                // this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = Color.White;
                Office2010Colors.ApplyManagedColors(this, Color.White);

                foreach (Control control in groupBox1.Controls)
                {
                    if (control is Button buttons)
                    {

                        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                        buttons.Font = new Font("Helvetica", 9);  // Example: Change font
                    }
                }


                foreach (Control control in groupBox1.Controls)
                {

                    if (control is TextBox textBox)
                    {

                        textBox.BackColor = Color.White;// Example: Change background color
                        textBox.ForeColor = Color.FromArgb(51, 51, 54); //Boja slova u kvadratu
                        textBox.Font = new Font("Helvetica", 9);
                        // Example: Change font
                    }


                    if (control is Label label)
                    {
                        // Change properties here
                        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        label.Font = new Font("Helvetica", 9);  // Example: Change font

                        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                    }
                    if (control is DateTimePicker dtp)
                    {
                        dtp.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        dtp.Font = new Font("Helvetica", 9);
                    }
                    if (control is CheckBox chk)
                    {
                        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        chk.Font = new Font("Helvetica", 9);
                    }

                    if (control is ListBox lb)
                    {
                        lb.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        lb.Font = new Font("Helvetica", 9);
                    }




                }
            }
            else
            {
                panelHeader.Visible = false;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        public frmIzvozKonacnaTable()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");

            InitializeComponent();
            ChangeTextBox();
            RefreshGV();
        }

        public frmIzvozKonacnaTable(string PlanID)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");

            InitializeComponent();
            txtPlanID.Text = PlanID;
            ChangeTextBox();
            RefreshGVPoPlanu();
        }
        public string GetID()
        {
            return textBox1.Text;
        }

        private void frmIzvozKonacnaTable_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmIzvozKonacna")
                {
                    frm.Activate();
                }
            }
        }

        private void gridGroupingControl1_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            try
            {
                if (gridGroupingControl1.Table.CurrentRecord != null)
                {
                    textBox1.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();

                    // txtSifra.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void frmIzvozKonacnaTable_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
            {
                Close();
            }
        }
        private void PunjenjeVrednostiPolja()
        {

            SqlConnection conn = new SqlConnection(connection);
            switch (cboPolje.Text)
            {




                case "Cut off port":
                    dtpOpsti.Visible = true;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    //  updatestring = " Update uvoz set NazivBroda = " + cbBrod.SelectedValue;
                    break;
                case "Planirani datum utovara":
                    dtpOpsti.Visible = true;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;

                case "ETA Leget":
                    dtpOpsti.Visible = true;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "Broj kontejnera":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "Booking brodara":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "Ostale plombe":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "Kontakt osobe":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "Naslov za slanje statusa vozila":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "Adresa za slanje statusa vozila":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "DODATNE NAPOMENE ZA KAMIONSKI PREVOZ I PRETOVAR":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "NAPOMENA ZA ROBU":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "Ref za fakturisanje 1":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "Ref za fakturisanje 2":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "Ref za fakturisanje 3":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;

                case "Broj vagona":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "Kontakt osoba špeditera":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "Izvoznik":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var partner = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
                    var partAD = new SqlDataAdapter(partner, conn);
                    var partDS = new DataSet();
                    partAD.Fill(partDS);
                    cboOpsti.DataSource = partDS.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";
                    break;
                case "Nalogodavac za voz":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;

                    var nalogodavac1 = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
                    var nal1AD = new SqlDataAdapter(nalogodavac1, conn);
                    var nal1DS = new DataSet();
                    nal1AD.Fill(nal1DS);
                    cboOpsti.DataSource = nal1DS.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";
                    break;
                case "Nalogodavac za usluge":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var nalogodavac2 = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
                    var nal2AD = new SqlDataAdapter(nalogodavac2, conn);
                    var nal2DS = new DataSet();
                    nal2AD.Fill(nal2DS);
                    cboOpsti.DataSource = nal2DS.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";

                    break;
                case "Nalogodavac za drumski prevoz":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var nalogodavac3 = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
                    var nal3AD = new SqlDataAdapter(nalogodavac3, conn);
                    var nal3DS = new DataSet();
                    nal3AD.Fill(nal3DS);
                    cboOpsti.DataSource = nal3DS.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";

                    break;
                case "Špediter u Rijeci":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var nalogodavac4 = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
                    var nal4AD = new SqlDataAdapter(nalogodavac4, conn);
                    var nal4DS = new DataSet();
                    nal4AD.Fill(nal4DS);
                    cboOpsti.DataSource = nal4DS.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";

                    break;
                case "Vrsta kontejnera":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var tipkontejnera = "Select ID, SkNaziv From TipKontenjera order by SkNaziv";
                    var tkAD = new SqlDataAdapter(tipkontejnera, conn);
                    var tkDS = new DataSet();
                    tkAD.Fill(tkDS);
                    cboOpsti.DataSource = tkDS.Tables[0];
                    cboOpsti.DisplayMember = "SkNaziv";
                    cboOpsti.ValueMember = "ID";

                    break;
                case "Brodar":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var bro = "Select PaSifra,PaNaziv From Partnerji where Brodar = 1 order by PaNaziv";
                    var broAD = new SqlDataAdapter(bro, conn);
                    var broDS = new DataSet();
                    broAD.Fill(broDS);
                    cboOpsti.DataSource = broDS.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";

                    break;
                case "Brodska plomba":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var bro2 = "Select PaSifra, PaNaziv From Partnerji where Brodar = 1  order by PaNaziv";
                    var broAD2 = new SqlDataAdapter(bro2, conn);
                    var broDS2 = new DataSet();
                    broAD2.Fill(broDS2);
                    cboOpsti.DataSource = broDS2.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";

                    break;
                case "Valuta":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;

                    var val = "Select VaSifra, VaNaziv from Valute order by VaSifra";
                    var valSAD = new SqlDataAdapter(val, conn);
                    var valSDS = new DataSet();
                    valSAD.Fill(valSDS);
                    cboOpsti.DataSource = valSDS.Tables[0];
                    cboOpsti.DisplayMember = "VaNaziv";
                    cboOpsti.ValueMember = "VaSifra";

                    break;
                case "Krajnja destinacija":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var kd = "Select ID, Naziv as Naziv From KrajnjaDestinacija order by ID";
                    var kdSAD = new SqlDataAdapter(kd, conn);
                    var kdSDS = new DataSet();
                    kdSAD.Fill(kdSDS);
                    cboOpsti.DataSource = kdSDS.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";

                    break;
                case "Pick up CNT":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var rl = "Select ID, (Naziv + ' - ' + Oznaka) as Naziv From KontejnerskiTerminali order by (Naziv + ' ' + Oznaka)";
                    var rlSAD = new SqlDataAdapter(rl, conn);
                    var rlSDS = new DataSet();
                    rlSAD.Fill(rlSDS);
                    cboOpsti.DataSource = rlSDS.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";

                    break;
                case "Mesto utovara":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var dir = "Select ID,Naziv from MestaUtovara order by Naziv";
                    var dirAD = new SqlDataAdapter(dir, conn);
                    var dirDS = new DataSet();
                    dirAD.Fill(dirDS);
                    cboOpsti.DataSource = dirDS.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";

                    break;
                case "Adresa utovara":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;


                    break;
                case "Mesto carinjenja":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var car = "Select ID, Naziv From Carinarnice order by Naziv";
                    var carAD = new SqlDataAdapter(car, conn);
                    var carDS = new DataSet();
                    carAD.Fill(carDS);
                    cboOpsti.DataSource = carDS.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";

                    break;
                case "Špediter":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var partner3 = "Select PaSifra,PaNaziv From Partnerji where Spediter =1 order by PaNaziv";
                    var partAD3 = new SqlDataAdapter(partner3, conn);
                    var partDS3 = new DataSet();
                    partAD3.Fill(partDS3);
                    cboOpsti.DataSource = partDS3.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";
                    break;
                case "Napomena o Carinskom postupku na LGT":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var dir2 = "Select ID, (Oznaka + ' ' + Naziv) as Naziv from VrstaCarinskogPostupka order by Naziv";
                    var dirAD2 = new SqlDataAdapter(dir2, conn);
                    var dirDS2 = new DataSet();
                    dirAD2.Fill(dirDS2);
                    cboOpsti.DataSource = dirDS2.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";

                    break;
                case "Inspekciski tretman":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var dir4 = "Select ID,Naziv from InspekciskiTretman order by Naziv";
                    var dirAD4 = new SqlDataAdapter(dir4, conn);
                    var dirDS4 = new DataSet();
                    dirAD4.Fill(dirDS4);
                    cboOpsti.DataSource = dirDS4.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";

                    break;
                case "Način pakovanja":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var np4 = "Select ID,(Oznaka + ' ' + Naziv) as Naziv from uvNacinPakovanja order by Naziv";
                    var npAD4 = new SqlDataAdapter(np4, conn);
                    var npDS4 = new DataSet();
                    npAD4.Fill(npDS4);
                    cboOpsti.DataSource = npDS4.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";
                    break;











                default:
                    Console.WriteLine("Nema podatka");
                    break;

            }


        }

        private void UpdateVrednostiPolja(int IdZaPromenu)
        {
            SqlConnection conn = new SqlConnection(connection);
            string updatestring = "";
            switch (cboPolje.Text)
            {

                case "Cut off port":
                    updatestring = " Update IzvozKonacna set CutOffPort = " + Convert.ToDateTime(dtpOpsti.Text) + " where ID =" + IdZaPromenu;
                    break;
                case "Planirani datum utovara":
                    updatestring = " Update IzvozKonacna set PlaniraniDatumUtovara = " + Convert.ToDateTime(dtpOpsti.Text) + " where ID =" + IdZaPromenu;
                    break;

                case "ETA Leget":
                    updatestring = " Update IzvozKonacna set EtaLeget = " + Convert.ToDateTime(dtpOpsti.Text) + " where ID =" + IdZaPromenu;
                    break;
                case "Broj kontejnera":
                    updatestring = " Update IzvozKonacna set BrojKontejnera = '" + txtOpsti.Text + "' where ID =" + IdZaPromenu;
                    break;
                case "Booking brodara":
                    updatestring = " Update IzvozKonacna set BookingBrodara = '" + txtOpsti.Text + "' where ID =" + IdZaPromenu;
                    break;
                case "Ostale plombe":
                    updatestring = " Update IzvozKonacna set OstalePlombe = '" + txtOpsti.Text + "' where ID =" + IdZaPromenu;
                    break;
                case "Kontakt osobe":
                    updatestring = " Update IzvozKonacna set KontaktOsobe = '" + txtOpsti.Text + "' where ID =" + IdZaPromenu;
                    break;
                case "Naslov za slanje statusa vozila":
                    updatestring = " Update IzvozKonacna set NaslovSlanjaStatusa = '" + txtOpsti.Text + "' where ID =" + IdZaPromenu;
                    break;
                case "Adresa za slanje statusa vozila":
                    updatestring = " Update IzvozKonacna set AdresaSlanjaStatusa = '" + txtOpsti.Text + "' where ID =" + IdZaPromenu;
                    break;
                case "DODATNE NAPOMENE ZA KAMIONSKI PREVOZ I PRETOVAR":
                    updatestring = " Update IzvozKonacna set DodatneNapomeneDrumski = '" + txtOpsti.Text + "' where ID =" + IdZaPromenu;
                    break;
                case "NAPOMENA ZA ROBU":
                    updatestring = " Update IzvozKonacna set NapomenaZaRobu = '" + txtOpsti.Text + "' where ID =" + IdZaPromenu;
                    break;
                case "Ref za fakturisanje 1":
                    updatestring = " Update IzvozKonacna set Napomena1REf = '" + txtOpsti.Text + "' where ID =" + IdZaPromenu;
                    break;
                case "Ref za fakturisanje 2":
                    updatestring = " Update IzvozKonacna set Napomena2REf = '" + txtOpsti.Text + "' where ID =" + IdZaPromenu;
                    break;
                case "Ref za fakturisanje 3":
                    updatestring = " Update IzvozKonacna set Napomena3REf= '" + txtOpsti.Text + "' where ID =" + IdZaPromenu;
                    break;

                case "Broj vagona":
                    updatestring = " Update IzvozKonacna set BrojVagona = '" + txtOpsti.Text + "' where ID =" + IdZaPromenu;
                    break;
                case "Kontakt osoba špeditera":
                    updatestring = " Update IzvozKonacna set obijeBZ = " + dtpOpsti.Text + " where ID =" + IdZaPromenu;
                    break;
                case "Izvoznik":
                    updatestring = " Update IzvozKonacna set Izvoznik = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
                case "Nalogodavac za voz":
                    updatestring = " Update IzvozKonacna set Klijent1 = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
                case "Nalogodavac za usluge":
                    updatestring = " Update IzvozKonacna set Klijent2 = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
                case "Nalogodavac za drumski prevoz":
                    updatestring = " Update IzvozKonacna set Klijent3 = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
                case "Špediter u Rijeci":
                    updatestring = " Update IzvozKonacna set SpediterRijeka = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
                case "Vrsta kontejnera":
                    updatestring = " Update IzvozKonacna set VrstaKontejnera = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
                case "Brodar":
                    updatestring = " Update IzvozKonacna set Brodar = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
                case "Brodska plomba":
                    updatestring = " Update IzvozKonacna set BrodskaPlomba = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
                case "Valuta":
                    updatestring = " Update IzvozKonacna set Valuta = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
                case "Krajnja destinacija":
                    updatestring = " Update IzvozKonacna set KrajnaDestinacija = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
                case "Pick up CNT":
                    updatestring = " Update IzvozKonacna set MestoPreuzimanja = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
                case "Mesto utovara":
                    updatestring = " Update IzvozKonacna set MesoUtovara = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
                case "Adresa utovara":
                    updatestring = " Update IzvozKonacna set obijeBZ = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
                case "Mesto carinjenja":
                    updatestring = " Update IzvozKonacna set MestoCarinjenja = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
                case "Špediter":
                    updatestring = " Update IzvozKonacna set obijeBZ = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
                case "Napomena o Carinskom postupku na LGT":
                    updatestring = " Update IzvozKonacna set NapomenaReexport = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
                case "Inspekciski tretman":
                    updatestring = " Update IzvozKonacna set Inspekcija] = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
                case "Način pakovanja":
                    updatestring = " Update IzvozKonacna set NacinPakovanja = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
                default:
                    Console.WriteLine("Nema podatka");
                    break;

            }

            string updateSql = updatestring;

            SqlConnection conn2 = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand(updateSql, conn2);
            conn2.Open();
            var q = cmd.ExecuteNonQuery();
            conn2.Close();














        }

        private void button3_Click(object sender, EventArgs e)
        {
            PunjenjeVrednostiPolja();
        }
        private void RefreshGV()
        {
            var select = " SELECT  IzvozKonacna.ID as ID,  IzvozKonacna.BrojKontejnera,  IzvozKonacna.VrstaKontejnera as Vrk_ID, TipKontenjera.Naziv as VrstaKontejnera, Partnerji.PaNaziv as Brodar, IzvozKonacna.BookingBrodara, " +
     " IzvozKonacna.BrojVagona,   IzvozKonacna.CutOffPort,Partnerji_2.PaNaziv AS Izvoznik,Partnerji_3.PaNaziv AS Nalogodavac1, Partnerji_4.PaNaziv AS Nalogodavac2, Partnerji_5.PaNaziv AS Nalogodavac3, " +
     " IzvozKonacna.DobijenNalogKlijent1, IzvozKonacna.BrodskaPlomba, IzvozKonacna.OstalePlombe,  " +
     " IzvozKonacna.NetoRobe, IzvozKonacna.BrutoRobe, IzvozKonacna.BrutoRobeO, IzvozKonacna.BrojKoleta, IzvozKonacna.BrojKoletaO, IzvozKonacna.CBM, IzvozKonacna.CBMO, IzvozKonacna.VrednostRobeFaktura,  " +
     " (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaManipulacije.Naziv as nvarchar(20))   FROM IzvozKonacnaVrstaManipulacije " +
     " inner join VrstaManipulacije on VrstaManipulacije.ID = IzvozKonacnaVrstaManipulacije.IDVrstaManipulacije   where IzvozKonacnaVrstaManipulacije.IDNadredjena = IzvozKonacna.ID" +
     " FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as VrsteUsluga,        (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaRobeHS.HSKod as nvarchar(20)) " +
     " FROM IzvozKonacnaVrstaRobeHS        inner join VrstaRobeHS on IzvozKonacnaVrstaRobeHS.IDVrstaRobeHS = VrstaRobeHS.ID   where IzvozKonacnaVrstaRobeHS.IDNadredjena = IzvozKonacna.ID " +
     " FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as HS,       (SELECT  STUFF((SELECT distinct    '/' + Cast(NHM.Broj as nvarchar(20)) " +
     " FROM IzvozKonacnaNHM  inner join NHM on IzvozKonacnaNHM.IDNHM = NHM.ID  where IzvozKonacnaNHM.IDNadredjena = IzvozKonacna.ID   FOR XML PATH('')), 1, 1, ''  ) As Skupljen) as NHM,  " +
     " IzvozKonacna.Valuta, KrajnjaDestinacija.Naziv AS KrajnjaDestinacija, VrstePostupakaUvoz.Naziv AS Postupak, KontejnerskiTerminali.Naziv AS PPCNT, " +
     " KontejnerskiTerminali.Oznaka, IzvozKonacna.Cirada, IzvozKonacna.PlaniraniDatumUtovara, MestaUtovara.Naziv AS MestoUtovara, IzvozKonacna.KontaktOsoba,  " +
     " Carinarnice.Naziv AS Carinarnica, Carinarnice.CIOznaka, Partnerji_1.PaNaziv AS Spedicija,  AdresaSlanjaStatusa,    " +
     " NaslovSlanjaStatusa, IzvozKonacna.EtaLeget, VrstaCarinskogPostupka.Naziv AS Reexport, InspekciskiTretman.Naziv AS InspekciskiTretman,   " +
     " IzvozKonacna.AutoDana, IzvozKonacna.NajavaVozila, IzvozKonacna.DodatneNapomeneDrumski, IzvozKonacna.Vaganje, IzvozKonacna.VGMTezina, IzvozKonacna.Tara, IzvozKonacna.VGMBrod,   " +
     "   IzvozKonacna.Napomena1REf, " +
     " IzvozKonacna.Napomena2REf, IzvozKonacna.Napomena3REf, Partnerji_6.PaNaziv AS SpediterRijeka, uvNacinPakovanja.Naziv AS NacinPakovanja,  " +
     " IzvozKonacna.NacinPretovara FROM         IzvozKonacna Left JOIN TipKontenjera ON IzvozKonacna.VrstaKontejnera = TipKontenjera.ID LEFT JOIN " +
     " Partnerji ON IzvozKonacna.Brodar = Partnerji.PaSifra LEFT JOIN         KrajnjaDestinacija ON IzvozKonacna.KrajnaDestinacija = KrajnjaDestinacija.ID LEFT JOIN " +
     " VrstePostupakaUvoz ON IzvozKonacna.Postupanje = VrstePostupakaUvoz.ID LEFT JOIN         KontejnerskiTerminali ON IzvozKonacna.MestoPreuzimanja = KontejnerskiTerminali.id " +
     " LEFT JOIN " +
     " MestaUtovara ON IzvozKonacna.MesoUtovara = MestaUtovara.ID " +
     " LEFT JOIN         Carinarnice ON IzvozKonacna.MestoCarinjenja = Carinarnice.ID " +
     " LEFT JOIN        Partnerji AS Partnerji_1 ON IzvozKonacna.Spedicija = Partnerji_1.PaSifra " +
     " LEFT JOIN         VrstaCarinskogPostupka ON IzvozKonacna.NapomenaReexport = VrstaCarinskogPostupka.id " +
     " LEFT JOIN        InspekciskiTretman ON IzvozKonacna.Inspekcija = InspekciskiTretman.ID " +
     " LEFT JOIN        Partnerji AS Partnerji_2 ON IzvozKonacna.Izvoznik = Partnerji_2.PaSifra " +
     " LEFT JOIN        Partnerji AS Partnerji_3 ON IzvozKonacna.Klijent1 = Partnerji_3.PaSifra " +
     " LEFT JOIN       Partnerji AS Partnerji_4 ON IzvozKonacna.Klijent2 = Partnerji_4.PaSifra " +
     " LEFT JOIN         Partnerji AS Partnerji_5 ON IzvozKonacna.Klijent3 = Partnerji_5.PaSifra LEFT JOIN " +
     " Partnerji AS Partnerji_6 ON IzvozKonacna.SpediterRijeka = Partnerji_6.PaSifra " +
     " LEFT JOIN         uvNacinPakovanja ON IzvozKonacna.NacinPakovanja = uvNacinPakovanja.ID order by IzvozKonacna.ID desc  ";


            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            // dataGridView1.ReadOnly = true;
            gridGroupingControl1.DataSource = ds.Tables[0];
            gridGroupingControl1.ShowGroupDropArea = true;
            this.gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;
            foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }
        }

        private void RefreshGVPoPlanu()
        {
            var select = " SELECT  IzvozKonacna.ID as ID,  IzvozKonacna.BrojKontejnera,  IzvozKonacna.VrstaKontejnera as Vrk_ID, TipKontenjera.Naziv as VrstaKontejnera, Partnerji.PaNaziv as Brodar, IzvozKonacna.BookingBrodara, " +
     " IzvozKonacna.BrojVagona,   IzvozKonacna.CutOffPort,Partnerji_2.PaNaziv AS Izvoznik,Partnerji_3.PaNaziv AS Nalogodavac1, Partnerji_4.PaNaziv AS Nalogodavac2, Partnerji_5.PaNaziv AS Nalogodavac3, " +
     " IzvozKonacna.DobijenNalogKlijent1, IzvozKonacna.BrodskaPlomba, IzvozKonacna.OstalePlombe,  " +
     " IzvozKonacna.NetoRobe, IzvozKonacna.BrutoRobe, IzvozKonacna.BrutoRobeO, IzvozKonacna.BrojKoleta, IzvozKonacna.BrojKoletaO, IzvozKonacna.CBM, IzvozKonacna.CBMO, IzvozKonacna.VrednostRobeFaktura,  " +
     " (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaManipulacije.Naziv as nvarchar(20))   FROM IzvozKonacnaVrstaManipulacije " +
     " inner join VrstaManipulacije on VrstaManipulacije.ID = IzvozKonacnaVrstaManipulacije.IDVrstaManipulacije   where IzvozKonacnaVrstaManipulacije.IDNadredjena = IzvozKonacna.ID" +
     " FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as VrsteUsluga,        (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaRobeHS.HSKod as nvarchar(20)) " +
     " FROM IzvozKonacnaVrstaRobeHS        inner join VrstaRobeHS on IzvozKonacnaVrstaRobeHS.IDVrstaRobeHS = VrstaRobeHS.ID   where IzvozKonacnaVrstaRobeHS.IDNadredjena = IzvozKonacna.ID " +
     " FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as HS,       (SELECT  STUFF((SELECT distinct    '/' + Cast(NHM.Broj as nvarchar(20)) " +
     " FROM IzvozKonacnaNHM  inner join NHM on IzvozKonacnaNHM.IDNHM = NHM.ID  where IzvozKonacnaNHM.IDNadredjena = IzvozKonacna.ID   FOR XML PATH('')), 1, 1, ''  ) As Skupljen) as NHM,  " +
     " IzvozKonacna.Valuta, KrajnjaDestinacija.Naziv AS KrajnjaDestinacija, VrstePostupakaUvoz.Naziv AS Postupak, KontejnerskiTerminali.Naziv AS PPCNT, " +
     " KontejnerskiTerminali.Oznaka, IzvozKonacna.Cirada, IzvozKonacna.PlaniraniDatumUtovara, MestaUtovara.Naziv AS MestoUtovara, IzvozKonacna.KontaktOsoba,  " +
     " Carinarnice.Naziv AS Carinarnica, Carinarnice.CIOznaka, Partnerji_1.PaNaziv AS Spedicija,  AdresaSlanjaStatusa,    " +
     " NaslovSlanjaStatusa, IzvozKonacna.EtaLeget, VrstaCarinskogPostupka.Naziv AS Reexport, InspekciskiTretman.Naziv AS InspekciskiTretman,   " +
     " IzvozKonacna.AutoDana, IzvozKonacna.NajavaVozila, IzvozKonacna.DodatneNapomeneDrumski, IzvozKonacna.Vaganje, IzvozKonacna.VGMTezina, IzvozKonacna.Tara, IzvozKonacna.VGMBrod,   " +
     "   IzvozKonacna.Napomena1REf, " +
     " IzvozKonacna.Napomena2REf, IzvozKonacna.Napomena3REf, Partnerji_6.PaNaziv AS SpediterRijeka, uvNacinPakovanja.Naziv AS NacinPakovanja,  " +
     " IzvozKonacna.NacinPretovara FROM         IzvozKonacna Left JOIN TipKontenjera ON IzvozKonacna.VrstaKontejnera = TipKontenjera.ID LEFT JOIN " +
     " Partnerji ON IzvozKonacna.Brodar = Partnerji.PaSifra LEFT JOIN         KrajnjaDestinacija ON IzvozKonacna.KrajnaDestinacija = KrajnjaDestinacija.ID LEFT JOIN " +
     " VrstePostupakaUvoz ON IzvozKonacna.Postupanje = VrstePostupakaUvoz.ID LEFT JOIN         KontejnerskiTerminali ON IzvozKonacna.MestoPreuzimanja = KontejnerskiTerminali.id " +
     " LEFT JOIN " +
     " MestaUtovara ON IzvozKonacna.MesoUtovara = MestaUtovara.ID " +
     " LEFT JOIN         Carinarnice ON IzvozKonacna.MestoCarinjenja = Carinarnice.ID " +
     " LEFT JOIN        Partnerji AS Partnerji_1 ON IzvozKonacna.Spedicija = Partnerji_1.PaSifra " +
     " LEFT JOIN         VrstaCarinskogPostupka ON IzvozKonacna.NapomenaReexport = VrstaCarinskogPostupka.id " +
     " LEFT JOIN        InspekciskiTretman ON IzvozKonacna.Inspekcija = InspekciskiTretman.ID " +
     " LEFT JOIN        Partnerji AS Partnerji_2 ON IzvozKonacna.Izvoznik = Partnerji_2.PaSifra " +
     " LEFT JOIN        Partnerji AS Partnerji_3 ON IzvozKonacna.Klijent1 = Partnerji_3.PaSifra " +
     " LEFT JOIN       Partnerji AS Partnerji_4 ON IzvozKonacna.Klijent2 = Partnerji_4.PaSifra " +
     " LEFT JOIN         Partnerji AS Partnerji_5 ON IzvozKonacna.Klijent3 = Partnerji_5.PaSifra LEFT JOIN " +
     " Partnerji AS Partnerji_6 ON IzvozKonacna.SpediterRijeka = Partnerji_6.PaSifra " +
     " LEFT JOIN         uvNacinPakovanja ON IzvozKonacna.NacinPakovanja = uvNacinPakovanja.ID " +
     " Where IzvozKonacna.IDNAdredjena = " + txtPlanID.Text + " order by IzvozKonacna.ID desc  ";


            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            // dataGridView1.ReadOnly = true;
            gridGroupingControl1.DataSource = ds.Tables[0];
            gridGroupingControl1.ShowGroupDropArea = true;
            this.gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;
            foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.gridGroupingControl1.Table.SelectedRecords.Count > 0)
            {
                int IDZaPromenu = 0;
                foreach (SelectedRecord selectedRecord in this.gridGroupingControl1.Table.SelectedRecords)
                {
                    IDZaPromenu = Convert.ToInt32(selectedRecord.Record.GetValue("ID").ToString());
                    UpdateVrednostiPolja(IDZaPromenu);
                }
            }

            RefreshGV();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RefreshGV();
        }
    }
}
