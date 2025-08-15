using iTextSharp.text;
using Syncfusion.GridHelperClasses;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using Testiranje.Dokumeta;

namespace Saobracaj.Uvoz
{
    public partial class UvozTable : Form
    {
        int Selektovani = 0;
        private Keys keyData;
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        int terminal = 0;

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
                        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                    }
                }


                    foreach (Control control in this.Controls)
                {

                    if (control is TextBox textBox)
                    {

                        textBox.BackColor = Color.White;// Example: Change background color
                        textBox.ForeColor = Color.FromArgb(51, 51, 54); //Boja slova u kvadratu
                        textBox.Font = new System.Drawing.Font("Helvetica", 9); 
                        // Example: Change font
                    }


                    if (control is Label label)
                    {
                        // Change properties here
                        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        label.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font

                        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                    }
                    if (control is DateTimePicker dtp)
                    {
                        dtp.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        dtp.Font = new System.Drawing.Font("Helvetica", 9);
                    }
                    if (control is CheckBox chk)
                    {
                        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        chk.Font = new System.Drawing.Font("Helvetica", 9);
                    }

                    if (control is ListBox lb)
                    {
                        lb.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        lb.Font = new System.Drawing.Font("Helvetica", 9);
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
        public UvozTable()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            InitializeComponent();
            ChangeTextBox();
        }

        public UvozTable(int Terminalski)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            InitializeComponent();
            ChangeTextBox();
            terminal = 1;
        }

        public string GetID()
        {
            return textBox1.Text;
        }

        private void RefreshGV()
        {
            var select = "SELECT Uvoz.ID, " +
                " CASE WHEN Prioritet > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Prioritet , " +
                " CASE WHEN DobijenBZ > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as DobijenBZ , " +
                " [BrojKontejnera],TipKontenjera.Naziv as Vrsta_Kontejnera, BrodskaTeretnica as BL,   Brodovi.Naziv as Brod, p1.PaNaziv as Uvoznik, " +
" n1.PaNaziv as NalogodavacZaVoz, Ref1 as Ref1,n2.PaNaziv as NalogodavacZaUsluge, Ref2 as Ref2,n3.PaNaziv as NalogodavacZaDrumski,DobijenNalogBrodara as Dobijen_Nalog_Brodara ,ATABroda,  " +
" Napomena1 as Napomena1,  DobijeBZ as DatumBZ ,PIN,    KontejnerskiTerminali.Naziv as R_L_SRB, pp1.Naziv as Dirigacija_Kontejnera_Za,   BrodskaTeretnica, " +
" VrstaRobeADR.Naziv as ADR, b.PaNaziv as Brodar,pv.PaNaziv as VlasnikKontejnera,     Ref3 as Ref3,         VrstaPregleda as InsTret,p2.PaNaziv as SpedicijaRTC,  " +
" p3.PaNaziv as SpedicijaGranica,       VrstaCarinskogPostupka.Naziv as CarinskiPostupak,  " +
" VrstePostupakaUvoz.Naziv as PostupakSaRobom,uvNacinPakovanja.Naziv as NacinPakovanja, " +
" Napomena as Napomena2, NaslovStatusaVozila as NaslovZaslanjestatusa,  Carinarnice.Naziv as Carinarnica,   " +
" p4.PaNaziv as OdredisnaSpedicija, MestaUtovara.Naziv as MestoIstovara, KontaktOsobe, Email, " +
" BrojPlombe1, BrojPlombe2,    NetoRobe, BrutoRobe, TaraKontejnera, BrutoKontejnera,  Koleta, green FROM Uvoz inner join Partnerji on PaSifra = VlasnikKontejnera " +
" inner join Partnerji p1 on p1.PaSifra = Uvoznik " +
" inner join Partnerji p2 on p2.PaSifra = SpedicijaRTC " +
" inner join Partnerji p3 on p3.PaSifra = SpedicijaGranica " +
" inner join TipKontenjera on TipKontenjera.ID = Uvoz.TipKontejnera " +
" inner join Carinarnice on Carinarnice.ID = Uvoz.OdredisnaCarina " +
" inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = Uvoz.CarinskiPostupak " +
" inner join Predefinisaneporuke on PredefinisanePoruke.ID = Uvoz.NapomenaZaPozicioniranje " +
" inner join KontejnerskiTerminali on KontejnerskiTerminali.ID = Uvoz.RLTErminali " +
" inner join Partnerji n1 on n1.PaSifra = Nalogodavac1 " +
" inner join Partnerji n2 on n2.PaSifra = Nalogodavac2 " +
" inner join Partnerji n3 on n3.PaSifra = Nalogodavac3 " +
" inner join Partnerji b on b.PaSifra = Uvoz.Brodar " +
" inner join  DirigacijaKontejneraZa pp1 on pp1.ID = Uvoz.DirigacijaKontejeraZa " +
" inner join Brodovi on Brodovi.ID = Uvoz.NazivBroda " +
" inner join VrstaRobeADR on VrstaRobeADR.ID = ADR " +
" inner join VrstePostupakaUvoz on VrstePostupakaUvoz.ID = PostupakSaRobom " +
" inner join MestaUtovara on Uvoz.MestoIstovara = MestaUtovara.ID " +
" inner join uvNacinPakovanja on uvNacinPakovanja.ID = NacinPakovanja " +
" inner join Partnerji p4 on p4.PaSifra = OdredisnaSpedicija " +
" inner join Partnerji pv on pv.PaSifra = Uvoz.VlasnikKontejnera " +
"  order by Uvoz.Prioritet desc, Uvoz.ID desc ";



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
            // this.gridGroupingControl1.RecordFilterDescriptor.
            foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
                //  column.RecordFilterDescriptor
            }
            this.gridGroupingControl1.TableDescriptor.Columns["BrojKontejnera"].FilterRowOptions.FilterMode = FilterMode.DisplayText;
            GridDynamicFilter dynamicFilter = new GridDynamicFilter();

            //Wiring the Dynamic Filter to GridGroupingControl
            dynamicFilter.WireGrid(this.gridGroupingControl1);

            GridExcelFilter gridExcelFilter = new GridExcelFilter();

            //Wiring GridExcelFilter to GridGroupingControl
            gridExcelFilter.WireGrid(this.gridGroupingControl1);

        }

        private void RefreshGVT()
        {
            var select = "SELECT Uvoz.ID, " +
                " CASE WHEN Prioritet > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Prioritet , " +
                " CASE WHEN DobijenBZ > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as DobijenBZ , " +
                " [BrojKontejnera],TipKontenjera.Naziv as Vrsta_Kontejnera, BrodskaTeretnica as BL,   Brodovi.Naziv as Brod, p1.PaNaziv as Uvoznik, " +
" n1.PaNaziv as NalogodavacZaVoz, Ref1 as Ref1,n2.PaNaziv as NalogodavacZaUsluge, Ref2 as Ref2,n3.PaNaziv as NalogodavacZaDrumski,DobijenNalogBrodara as Dobijen_Nalog_Brodara ,ATABroda,  " +
" Napomena1 as Napomena1,  DobijeBZ as DatumBZ ,PIN,    KontejnerskiTerminali.Naziv as R_L_SRB, pp1.Naziv as Dirigacija_Kontejnera_Za,   BrodskaTeretnica, " +
" VrstaRobeADR.Naziv as ADR, b.PaNaziv as Brodar,pv.PaNaziv as VlasnikKontejnera,     Ref3 as Ref3,         VrstaPregleda as InsTret,p2.PaNaziv as SpedicijaRTC,  " +
" p3.PaNaziv as SpedicijaGranica,       VrstaCarinskogPostupka.Naziv as CarinskiPostupak,  " +
" VrstePostupakaUvoz.Naziv as PostupakSaRobom,uvNacinPakovanja.Naziv as NacinPakovanja, " +
" Napomena as Napomena2, NaslovStatusaVozila as NaslovZaslanjestatusa,  Carinarnice.Naziv as Carinarnica,   " +
" p4.PaNaziv as OdredisnaSpedicija, MestaUtovara.Naziv as MestoIstovara, KontaktOsobe, Email, " +
" BrojPlombe1, BrojPlombe2,    NetoRobe, BrutoRobe, TaraKontejnera, BrutoKontejnera,  Koleta, green FROM Uvoz inner join Partnerji on PaSifra = VlasnikKontejnera " +
" inner join Partnerji p1 on p1.PaSifra = Uvoznik " +
" inner join Partnerji p2 on p2.PaSifra = SpedicijaRTC " +
" inner join Partnerji p3 on p3.PaSifra = SpedicijaGranica " +
" inner join TipKontenjera on TipKontenjera.ID = Uvoz.TipKontejnera " +
" inner join Carinarnice on Carinarnice.ID = Uvoz.OdredisnaCarina " +
" inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = Uvoz.CarinskiPostupak " +
" inner join Predefinisaneporuke on PredefinisanePoruke.ID = Uvoz.NapomenaZaPozicioniranje " +
" inner join KontejnerskiTerminali on KontejnerskiTerminali.ID = Uvoz.RLTErminali " +
" inner join Partnerji n1 on n1.PaSifra = Nalogodavac1 " +
" inner join Partnerji n2 on n2.PaSifra = Nalogodavac2 " +
" inner join Partnerji n3 on n3.PaSifra = Nalogodavac3 " +
" inner join Partnerji b on b.PaSifra = Uvoz.Brodar " +
" inner join  DirigacijaKontejneraZa pp1 on pp1.ID = Uvoz.DirigacijaKontejeraZa " +
" inner join Brodovi on Brodovi.ID = Uvoz.NazivBroda " +
" inner join VrstaRobeADR on VrstaRobeADR.ID = ADR " +
" inner join VrstePostupakaUvoz on VrstePostupakaUvoz.ID = PostupakSaRobom " +
" inner join MestaUtovara on Uvoz.MestoIstovara = MestaUtovara.ID " +
" inner join uvNacinPakovanja on uvNacinPakovanja.ID = NacinPakovanja " +
" inner join Partnerji p4 on p4.PaSifra = OdredisnaSpedicija " +
" inner join Partnerji pv on pv.PaSifra = Uvoz.VlasnikKontejnera where Uvoz.Terminal = 1" +
"  order by Uvoz.Prioritet desc, Uvoz.ID desc ";



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

            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            dynamicFilter.WireGrid(this.gridGroupingControl1);

            GridExcelFilter gridExcelFilter = new GridExcelFilter();

            //Wiring GridExcelFilter to GridGroupingControl
            gridExcelFilter.WireGrid(this.gridGroupingControl1);

        }

        private void UvozTable_Load(object sender, EventArgs e)
        {

            if (terminal == 0)
            {
                RefreshGV();
            }
            else
            {
                RefreshGVT();
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

        private void UvozTable_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27 || e.KeyCode == Keys.Enter)
            {
                Close();
            }
            // e.KeyCode == Keys.Enter
        }
        private void PunjenjeVrednostiPolja()
        {
            /*
            
            ---- Datumska  Datum BZ, ATA broda u Luku Rijeka
             --- CHECK DobijenBZ, Prioritet
                ----- Text PIN, Broj kontejnera, BL, Ref za fakturisanje 1,Ref za fakturisanje 2,Ref za fakturisanje 3,Kontakt osobe
Naslov za slanje statusa vozila , Napomena 2,
E - mail za slanje statusa,
Broj plombe 1
Broj plombe 2
                --------- Combo Vrsta kontejnera, Relacija R / L / SRB, Dirigacija kontejnera za,
                ADR,Brodar,Vlasnik kontejnera,Nalogodavac za voz,Nalogdavac za usluge,Nalogodavac za drumski prevoz

Uvoznik,Inspekciski tretman,Špedicija - granica,Špedicija - RTC Leget,
 Carinski postupak
  Postupak sa robom / kontejnerom
Način pakovanja
Odredišna Špedicija
Carinarnica
Mesto istovara
Adresa utovara

---- NUMERIC NTTO robe
Tara kontejnera
BTTO kontejnera
BTTO robe
Koleta

*/
            SqlConnection conn = new SqlConnection(connection);
            switch (cboPolje.Text)
            {
                case "ETA Broda Luka":
                    dtpOpsti.Visible = true;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    //  updatestring = " Update uvoz set NazivBroda = " + cbBrod.SelectedValue;
                    break;

                case "ATA broda Luka":
                    dtpOpsti.Visible = true;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;

                case "Dobijen nalog brodara":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = true;
                    nmrOpsti.Visible = false;
                    //  updatestring = " Update uvoz set NazivBroda = " + cbBrod.SelectedValue;
                    break;
                case "Datum Dobijen nalog brodara":
                    dtpOpsti.Visible = true;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    //  updatestring = " Update uvoz set NazivBroda = " + cbBrod.SelectedValue;
                    break;

                case "Brodar":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var bro = "Select PaSifra,PaNaziv From Partnerji  order by PaNaziv";
                    var broAD = new SqlDataAdapter(bro, conn);
                    var broDS = new DataSet();
                    broAD.Fill(broDS);
                    cboOpsti.DataSource = broDS.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";
                    break;


                case "Naziv Broda":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var brod = "Select ID,Naziv From Brodovi order by Naziv";
                    var brodAD = new SqlDataAdapter(brod, conn);
                    var brodDS = new DataSet();
                    brodAD.Fill(brodDS);
                    cboOpsti.DataSource = brodDS.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";
                    break;

                case "Dobijen nal voz":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = true;
                    nmrOpsti.Visible = false;
                    //  updatestring = " Update uvoz set NazivBroda = " + cbBrod.SelectedValue;
                    break;

                case "Datum Dobijen nal voz":
                    dtpOpsti.Visible = true;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    //  updatestring = " Update uvoz set NazivBroda = " + cbBrod.SelectedValue;
                    break;

                case "Nalogodavac za voz":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var nalogodavac1 = "Select PaSifra,PaNaziv From Partnerji  order by PaNaziv";
                    var nal1AD = new SqlDataAdapter(nalogodavac1, conn);
                    var nal1DS = new DataSet();
                    nal1AD.Fill(nal1DS);
                    cboOpsti.DataSource = nal1DS.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";
                    break;
                case "Nalogdavac za usluge":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;

                    var nalogodavac2 = "Select PaSifra,PaNaziv From Partnerji  order by PaNaziv";
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
                    var nalogodavac3 = "Select PaSifra,PaNaziv From Partnerji  order by PaNaziv";
                    var nal3AD = new SqlDataAdapter(nalogodavac3, conn);
                    var nal3DS = new DataSet();
                    nal3AD.Fill(nal3DS);
                    cboOpsti.DataSource = nal3DS.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";
                    break;

                case "Terminal 1":
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

                case "Terminal 2":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var rl2 = "Select ID, (Naziv + ' - ' + Oznaka) as Naziv From KontejnerskiTerminali order by (Naziv + ' ' + Oznaka)";
                    var rl2SAD = new SqlDataAdapter(rl2, conn);
                    var rl2SDS = new DataSet();
                    rl2SAD.Fill(rl2SDS);
                    cboOpsti.DataSource = rl2SDS.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";
                    break;


                case "Terminal 3":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var rl3 = "Select ID, (Naziv + ' - ' + Oznaka) as Naziv From KontejnerskiTerminali order by (Naziv + ' ' + Oznaka)";
                    var rl3SAD = new SqlDataAdapter(rl3, conn);
                    var rl3SDS = new DataSet();
                    rl3SAD.Fill(rl3SDS);
                    cboOpsti.DataSource = rl3SDS.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";
                    break;




                case "Datum BZ":
                    dtpOpsti.Visible = true;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    //  updatestring = " Update uvoz set NazivBroda = " + cbBrod.SelectedValue;
                    break;
               

                case "DobijenBZ":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = true;
                    nmrOpsti.Visible = false;
                    break;
                case "Prioritet":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = true;
                    nmrOpsti.Visible = false;
                    break;



                case "FCL":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = true;
                    nmrOpsti.Visible = false;
                    break;

                case "LCL":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = true;
                    nmrOpsti.Visible = false;
                    break;

                case "PIN":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
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
                case "BL":
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
                case "Napomena 2":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "E - mail za slanje statusa":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
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
               
               
                case "ADR":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var adr = "Select ID, (Naziv + ' - ' + UNKod) as Naziv From VrstaRobeADR order by (UNKod + ' ' + Naziv)";
                    var adrSAD = new SqlDataAdapter(adr, conn);
                    var adrSDS = new DataSet();
                    adrSAD.Fill(adrSDS);
                    cboOpsti.DataSource = adrSDS.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";
                    break;
              
                case "Vlasnik kontejnera":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var partner = "Select PaSifra,PaNaziv From Partnerji where Vlasnik = 1  order by PaNaziv";
                    var partAD = new SqlDataAdapter(partner, conn);
                    var partDS = new DataSet();
                    partAD.Fill(partDS);
                    cboOpsti.DataSource = partDS.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";
                    break;
               

                case "Uvoznik":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var partner2 = "Select PaSifra,PaNaziv From Partnerji where UvoznikCH = 1 order by PaNaziv";
                    var partAD2 = new SqlDataAdapter(partner2, conn);
                    var partDS2 = new DataSet();
                    partAD2.Fill(partDS2);
                    cboOpsti.DataSource = partDS2.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";
                    //spedicija na granici
                    break;
                case "Inspekciski tretman":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var it = "select ID, Naziv from InspekciskiTretman order by Naziv";
                    var itAD = new SqlDataAdapter(it, conn);
                    var itDS = new DataSet();
                    itAD.Fill(itDS);
                    cboOpsti.DataSource = itDS.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";
                    break;
                case "Špedicija -granica":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var partner3 = "Select PaSifra,PaNaziv From Partnerji where  Spediter = 1 order by PaNaziv";
                    var partAD3 = new SqlDataAdapter(partner3, conn);
                    var partDS3 = new DataSet();
                    partAD3.Fill(partDS3);
                    cboOpsti.DataSource = partDS3.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";
                    //spedicija rtc luka leget
                    break;
                case "Špedicija - RTC Leget":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;

                    var partner4 = "Select PaSifra,PaNaziv From Partnerji  where Spediter = 1 order by PaNaziv";
                    var partAD4 = new SqlDataAdapter(partner4, conn);
                    var partDS4 = new DataSet();
                    partAD4.Fill(partDS4);
                    cboOpsti.DataSource = partDS4.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";
                    break;
                case "Carinski postupak":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    //carinski postupak
                    var dir2 = "Select ID, (Oznaka + ' ' + Naziv) as Naziv from VrstaCarinskogPostupka order by Naziv";
                    var dirAD2 = new SqlDataAdapter(dir2, conn);
                    var dirDS2 = new DataSet();
                    dirAD2.Fill(dirDS2);
                    cboOpsti.DataSource = dirDS2.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";
                    break;
                case "Postupak sa robom / kontejnerom":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var dir3 = "Select ID,Naziv from VrstePostupakaUvoz order by Naziv";
                    var dirAD3 = new SqlDataAdapter(dir3, conn);
                    var dirDS3 = new DataSet();
                    dirAD3.Fill(dirDS3);
                    cboOpsti.DataSource = dirDS3.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";
                    break;
                case "Odredišna Špedicija":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var partner5 = "Select PaSifra,PaNaziv From Partnerji where Spediter = 1 order by PaNaziv";
                    var partAD5 = new SqlDataAdapter(partner5, conn);
                    var partDS5 = new DataSet();
                    partAD5.Fill(partDS5);
                    cboOpsti.DataSource = partDS5.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";
                    break;
              
                case "Carinarnica":
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
                case "Mesto istovara":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var mu = "select ID, Naziv from MestaUtovara order by Naziv";
                    var muAD = new SqlDataAdapter(mu, conn);
                    var muDS = new DataSet();
                    muAD.Fill(muDS);
                    cboOpsti.DataSource = muDS.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";

                    break;
                case "Adresa utovara":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;

                    var ko = "select PaKoZapSt, (PaKoOpomba) as Naziv from partnerjiKontOsebaMU order by PaKOIme";
                    var koAD = new SqlDataAdapter(ko, conn);
                    var koDS = new DataSet();
                    koAD.Fill(koDS);
                    cboOpsti.DataSource = koDS.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "PaKoZapSt";
                    break;
                case "NTTO robe":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "Tara kontejnera":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "BTTO kontejnera":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "BTTO robe":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "Koleta":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                default:
                    Console.WriteLine("Nema podatka");
                    break;

            }













            /*

                        string updatestring = "";
                        switch (cboPolje.Text)
                        {
                            case "Naziv broda":
                               
                                break;
                            case "Napomena 1":
                                updatestring = " Update uvoz set Napomena1 = '" + txtNapomena1.Text + "'";
                                break;
                            case "Datum BZ":
                                updatestring = " Update uvoz set DobijeBZ = '" + txtBZ.Text.ToString().TrimEnd() + "'";
                                break;
                            case "PIN":
                                updatestring = " Update uvoz set PIN = '" + txtPIN.Text + "'";
                                break;
                            case "Vrsta kontejnera":
                                updatestring = " Update uvoz set TaraKontejnera = " + Convert.ToInt32(txtTipKont.SelectedValue);
                                break;
                            case "Relacija R\\L\\SRB":
                                updatestring = " Update uvoz set RLTerminali = " + Convert.ToInt32(cboRLTerminal.SelectedValue);
                                break;
                            case "BL":
                                updatestring = " Update uvoz set BrodskaTeretnica = '" + txtTeretnica.Text + "'";
                                break;
                            case "ADR":
                                updatestring = " Update uvoz set ADR = " + Convert.ToInt32(txtADR.SelectedValue);
                                break;
                            case "Brodar":
                                updatestring = " Update uvoz set Brodar = " + Convert.ToInt32(cboBrodar.SelectedValue);
                                break;
                            case "Vlasnik":
                                updatestring = " Update uvoz set Vlasnik = " + Convert.ToInt32(cbVlasnikKont.SelectedValue);
                                break;
                            case "Uvoznik":
                                updatestring = " Update uvoz set Uvoznik = " + Convert.ToInt32(cboUvoznik.SelectedValue);
                                break;
                            case "Nalogodavac 1":
                                updatestring = " Update uvoz set Nalogodavac1 = " + Convert.ToInt32(cboNalogodavac1.SelectedValue);
                                break;
                            case "Ref1":
                                updatestring = " Update uvoz set Ref1 = '" + txtRef1.Text + "'";
                                break;
                            case "Nalogodavac 2":
                                updatestring = " Update uvoz set Nalogodavac2 = " + Convert.ToInt32(cboNalogodavac2.SelectedValue);
                                break;
                            case "Ref2":
                                updatestring = " Update uvoz set Ref2 = '" + txtOpsti.Text + "'";
                                break;
                            case "Nalogodavac3":
                                updatestring = " Update uvoz set Nalogodavac3 = " + Convert.ToInt32(cboNalogodavac3.SelectedValue);
                                break;
                            case "Ref3":
                                updatestring = " Update uvoz set Ref3 = '" + txtRef3.Text + "'";
                                break;
                            case "VrstaPregleda":
                                updatestring = " Update uvoz set VrstaPregleda = '" + Convert.ToInt32(txtVrstaPregleda.SelectedValue) + "'";
                                break;
                            case "Špedicija - RTCLeget":
                                updatestring = " Update uvoz set SpedicijaRTC = " + Convert.ToInt32(cboSpedicijaRTC.SelectedValue);
                                break;
                            case "Špedicija granica":
                                updatestring = " Update uvoz set SpedicijaGranica = " + Convert.ToInt32(cboSpedicijaG.SelectedValue);
                                break;
                            case "Carinski postupak":
                                updatestring = " Update uvoz set CarinskiPostupak = " + Convert.ToInt32(cboCarinskiPostupak.SelectedValue);
                                break;
                            case "Postupak sa robom":
                                updatestring = " Update uvoz set PostupakSaRobom = " + Convert.ToInt32(cbPostupak.SelectedValue);
                                break;
                            case "Način pakovanja":
                                updatestring = " Update uvoz set NacinPakovanja = " + Convert.ToInt32(cbNacinPakovanja.SelectedValue);
                                break;
                            case "Napomena 2":
                                updatestring = " Update uvoz set Napomena2 = " + txtNapomena.Text;
                                break;
                            case "Odredišna špedicija":
                                updatestring = " Update uvoz set OdredisnaSpedicija = " + Convert.ToInt32(cbOspedicija.SelectedValue);
                                break;
                            case "Carinarnica":
                                updatestring = " Update uvoz set OdredisnaCarina = " + Convert.ToInt32(cbOcarina.SelectedValue);
                                break;
                            case "Mesto istovara":
                                updatestring = " Update uvoz set MestoIstovara = '" + Convert.ToInt32(txtMesto.SelectedValue) + "'";
                                break;
                            case "Kontakt osoba":
                                updatestring = " Update uvoz set KontaktOsoba = '" + Convert.ToInt32(txtKontaktOsoba.SelectedValue) + "'";
                                break;
                            case "EMail":
                                updatestring = " Update uvoz set Email = '" + txtMail.Text.ToString() + "'";
                                break;
                            default:
                                Console.WriteLine("Nema podatka");
                                break;

                        }


                        try
                        {
                            // 1. Create Command
                            // Sql Update Statement
                            string updateSql = updatestring + " where ID = " + IdZaPromenu;

                            SqlConnection conn = new SqlConnection(connection);
                            SqlCommand cmd = new SqlCommand(updateSql, conn);
                            conn.Open();
                            var q = cmd.ExecuteNonQuery();
                            conn.Close();


                        }

                        catch (SqlException ex)
                        {
                            // Display error
                            Console.WriteLine("Error: " + ex.ToString());
                        }



                        //FillGV();
                    }
                    private void button7_Click(object sender, EventArgs e)
                    {
                        int IDZaPromenu = 0;
                        try
                        {
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (row.Selected)
                                {
                                    IDZaPromenu = Convert.ToInt32(row.Cells[0].Value.ToString());
                                    UpdateVrednostiPolja(IDZaPromenu);
                                }
                            }
                            // FillGV();

                        }
                        catch
                        {
                            MessageBox.Show("Nije uspela selekcija stavki");
                        }
            */
        }

        private void UpdateVrednostiPolja(int IdZaPromenu)
        {
           
            //NazivBroda
            
            int temp = 0;
            SqlConnection conn = new SqlConnection(connection);
            string updatestring = "";
            switch (cboPolje.Text)
            {
                case "ETA broda Luka":
                    updatestring = " Update uvoz set ETABroda = " + Convert.ToDateTime(dtpOpsti.Value) + " where ID =" + IdZaPromenu;
                    break;
                
               // case "Datum Dobijen nalog brodara":
                //    updatestring = " Update uvoz set ETABroda = " + Convert.ToDateTime(dtpOpsti.Value) + " where ID =" + IdZaPromenu;
                 //   break;

                case "Dobijen nal voz":

                    temp = 0;
                    if (chkOpsti.Checked == true)
                    { temp = 1; }
                    updatestring = " Update uvoz set chkDobijenNalogodavac1 = " + temp + " where ID =" + IdZaPromenu;
                    break;

                case "Datum Dobijen nal voz":
                    updatestring = " Update uvoz set DatumNalogodavac1 = " + Convert.ToDateTime(dtpOpsti.Value) + " where ID =" + IdZaPromenu;
                    break;

                case "Dobijen nal usluge":

                    temp = 0;
                    if (chkOpsti.Checked == true)
                    { temp = 1; }
                    updatestring = " Update uvoz set chkDobijenNalogodavac2  = " + temp + " where ID =" + IdZaPromenu;
                    break;

                case "Datum Dobijen nal usluge":
                    updatestring = " Update uvoz set DatumNalogodavac2 = " + Convert.ToDateTime(dtpOpsti.Value) + " where ID =" + IdZaPromenu;
                    break;
               
                case "Dobijen nal drumski":

                    temp = 0;
                    if (chkOpsti.Checked == true)
                    { temp = 1; }
                    updatestring = " Update uvoz set chkDobijenNalogodavac3 = " + temp + " where ID =" + IdZaPromenu;
                    break;

                case "Datum Dobijen nal drumski":
                    updatestring = " Update uvoz set DatumNalogodavac2 = " + Convert.ToDateTime(dtpOpsti.Value) + " where ID =" + IdZaPromenu;
                    break;

                case "Dobijen nalog brodara":

                    temp = 0;
                    if (chkOpsti.Checked == true)
                    { temp = 1; }
                    updatestring = " Update uvoz set chkDobijenNalogBrodara = " + temp + " where ID =" + IdZaPromenu;
                    break;


                case "Naziv broda":
                    updatestring = " Update uvoz set NazivBroda = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;


                case "Brodar":
                    updatestring = " Update uvoz set Brodar  = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;














                case "Nalogodavac za voz":
                    updatestring = " Update uvoz set Nalogodavac1  = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
                case "Nalogdavac za usluge":
                    updatestring = " Update uvoz set Nalogodavac2  = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
                case "Nalogodavac za drumski prevoz":
                    updatestring = " Update uvoz set Nalogodavac3  = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;


                case "ATA broda Luka":
                    updatestring = " Update uvoz set AtaBroda = " + Convert.ToDateTime(dtpOpsti.Value) + " where ID =" + IdZaPromenu;
                    break;


                case "Terminal 1":
                    updatestring = " Update uvoz set RLTErminali  = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;

                case "Terminal 2":
                    updatestring = " Update uvoz set RLTErminali2  = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;

                case "Terminal 3":
                    updatestring = " Update uvoz set RLTErminali2  = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;


                case "Datum BZ":
                    updatestring = " Update uvoz set obijeBZ = " + Convert.ToDateTime(dtpOpsti.Text) + " where ID =" + IdZaPromenu;
                    break;
              

                case "DobijenBZ":

                    temp = 0;
                    if (chkOpsti.Checked == true)
                    { temp = 1; }
                    updatestring = " Update uvoz set DobijenBZ = " + temp + " where ID =" + IdZaPromenu;
                    break;
                case "Prioritet":

                    temp = 0;
                    if (chkOpsti.Checked == true)
                    { temp = 1; }
                    updatestring = " Update uvoz set Prioritet = " + temp + " where ID =" + IdZaPromenu;
                    break;


                case "FCL":

                    temp = 0;
                    if (chkOpsti.Checked == true)
                    { temp = 1; }
                    updatestring = " Update uvoz set FCL = " + temp + " where ID =" + IdZaPromenu;
                    break;


                case "LCL":

                    temp = 0;
                    if (chkOpsti.Checked == true)
                    { temp = 1; }
                    updatestring = " Update uvoz set LCL = " + temp + " where ID =" + IdZaPromenu;
                    break;

                case "PIN":

                    updatestring = " Update uvoz set PIN = '" + txtOpsti.Text + "' where ID =" + IdZaPromenu;
                    break;
                case "Broj kontejnera":
                    updatestring = " Update uvoz set BrojKontejnera = '" + txtOpsti.Text + "' where ID =" + IdZaPromenu;
                    break;
                case "BL":
                    updatestring = " Update uvoz set BrodskaTeretnica = '" + txtOpsti.Text + "' where ID =" + IdZaPromenu;

                    break;
                case "Ref za fakturisanje 1":
                    updatestring = " Update uvoz set Ref1 = '" + txtOpsti.Text + "' where ID =" + IdZaPromenu;
                    break;
                case "Ref za fakturisanje 2":
                    updatestring = " Update uvoz set Ref2 = '" + txtOpsti.Text + "' where ID =" + IdZaPromenu;
                    break;
                case "Ref za fakturisanje 3":
                    updatestring = " Update uvoz set Ref2 = '" + txtOpsti.Text + "' where ID =" + IdZaPromenu;
                    break;
                case "Kontakt osobe":
                    updatestring = " Update uvoz set KontaktOsobe = '" + txtOpsti.Text + "' where ID =" + IdZaPromenu;
                    break;
                case "Naslov za slanje statusa vozila":
                    updatestring = " Update uvoz set NaslovStatusaVozila  = '" + txtOpsti.Text + "' where ID =" + IdZaPromenu;
                    break;
                case "Napomena 2":
                    updatestring = " Update uvoz set Napomena  = '" + txtOpsti.Text + "' where ID =" + IdZaPromenu;
                    break;
                case "E - mail za slanje statusa":
                    updatestring = " Update uvoz set Email  = '" + txtOpsti.Text + "' where ID =" + IdZaPromenu;
                    break;
                case "Vrsta kontejnera":
                    updatestring = " Update uvoz set TipKontejnera  = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
              
         
                case "ADR":
                    updatestring = " Update uvoz set ADR  = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
            
                case "Vlasnik kontejnera":
                    updatestring = " Update uvoz set VlasnikKontejnera  = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
                

                case "Uvoznik":
                    updatestring = " Update uvoz set Uvoznik  = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
                case "Inspekciski tretman":
                    updatestring = " Update uvoz set VrstaPregleda  = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
                case "Špedicija -granica":
                    updatestring = " Update uvoz set SpedicijaGranica  = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    //spedicija rtc luka leget
                    break;
                case "Špedicija - RTC Leget":
                    updatestring = " Update uvoz set SpedicijaRTC  = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
                case "Carinski postupak":
                    updatestring = " Update uvoz set CarinskiPostupak = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
                case "Postupak sa robom / kontejnerom":
                    updatestring = " Update uvoz set PostupakSaRobom = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
                case "Odredišna Špedicija":
                    updatestring = " Update uvoz set OdredisnaSpedicija = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
            
                case "Carinarnica":
                    updatestring = " Update uvoz set OdredisnaCarina = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;

                    break;
                case "Mesto istovara":
                    updatestring = " Update uvoz set MestoIstovara = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;

                    break;
                case "Adresa istovara":
                    updatestring = " Update uvoz set AdresaMestaUtovara = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
                case "NTTO robe":
                    updatestring = " Update uvoz set NetoRobe = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
                case "Tara kontejnera":
                    updatestring = " Update uvoz set TaraKontejnera = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
                case "BTTO kontejnera":
                    updatestring = " Update uvoz set BrutoKontejnera = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
                case "BTTO robe":
                    updatestring = " Update uvoz set BrutoRobe = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
                    break;
                case "Koleta":
                    updatestring = " Update uvoz set Koleta = " + Convert.ToInt32(cboOpsti.SelectedValue) + " where ID =" + IdZaPromenu;
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













            /*

                        string updatestring = "";
                        switch (cboPolje.Text)
                        {
                            case "Naziv broda":
                               
                                break;
                            case "Napomena 1":
                                updatestring = " Update uvoz set Napomena1 = '" + txtNapomena1.Text + "'";
                                break;
                            case "Datum BZ":
                                updatestring = " Update uvoz set DobijeBZ = '" + txtBZ.Text.ToString().TrimEnd() + "'";
                                break;
                            case "PIN":
                                updatestring = " Update uvoz set PIN = '" + txtPIN.Text + "'";
                                break;
                            case "Vrsta kontejnera":
                                updatestring = " Update uvoz set TaraKontejnera = " + Convert.ToInt32(txtTipKont.SelectedValue);
                                break;
                            case "Relacija R\\L\\SRB":
                                updatestring = " Update uvoz set RLTerminali = " + Convert.ToInt32(cboRLTerminal.SelectedValue);
                                break;
                            case "BL":
                                updatestring = " Update uvoz set BrodskaTeretnica = '" + txtTeretnica.Text + "'";
                                break;
                            case "ADR":
                                updatestring = " Update uvoz set ADR = " + Convert.ToInt32(txtADR.SelectedValue);
                                break;
                            case "Brodar":
                                updatestring = " Update uvoz set Brodar = " + Convert.ToInt32(cboBrodar.SelectedValue);
                                break;
                            case "Vlasnik":
                                updatestring = " Update uvoz set Vlasnik = " + Convert.ToInt32(cbVlasnikKont.SelectedValue);
                                break;
                            case "Uvoznik":
                                updatestring = " Update uvoz set Uvoznik = " + Convert.ToInt32(cboUvoznik.SelectedValue);
                                break;
                            case "Nalogodavac 1":
                                updatestring = " Update uvoz set Nalogodavac1 = " + Convert.ToInt32(cboNalogodavac1.SelectedValue);
                                break;
                            case "Ref1":
                                updatestring = " Update uvoz set Ref1 = '" + txtRef1.Text + "'";
                                break;
                            case "Nalogodavac 2":
                                updatestring = " Update uvoz set Nalogodavac2 = " + Convert.ToInt32(cboNalogodavac2.SelectedValue);
                                break;
                            case "Ref2":
                                updatestring = " Update uvoz set Ref2 = '" + txtOpsti.Text + "'";
                                break;
                            case "Nalogodavac3":
                                updatestring = " Update uvoz set Nalogodavac3 = " + Convert.ToInt32(cboNalogodavac3.SelectedValue);
                                break;
                            case "Ref3":
                                updatestring = " Update uvoz set Ref3 = '" + txtRef3.Text + "'";
                                break;
                            case "VrstaPregleda":
                                updatestring = " Update uvoz set VrstaPregleda = '" + Convert.ToInt32(txtVrstaPregleda.SelectedValue) + "'";
                                break;
                            case "Špedicija - RTCLeget":
                                updatestring = " Update uvoz set SpedicijaRTC = " + Convert.ToInt32(cboSpedicijaRTC.SelectedValue);
                                break;
                            case "Špedicija granica":
                                updatestring = " Update uvoz set SpedicijaGranica = " + Convert.ToInt32(cboSpedicijaG.SelectedValue);
                                break;
                            case "Carinski postupak":
                                updatestring = " Update uvoz set CarinskiPostupak = " + Convert.ToInt32(cboCarinskiPostupak.SelectedValue);
                                break;
                            case "Postupak sa robom":
                                updatestring = " Update uvoz set PostupakSaRobom = " + Convert.ToInt32(cbPostupak.SelectedValue);
                                break;
                            case "Način pakovanja":
                                updatestring = " Update uvoz set NacinPakovanja = " + Convert.ToInt32(cbNacinPakovanja.SelectedValue);
                                break;
                            case "Napomena 2":
                                updatestring = " Update uvoz set Napomena2 = " + txtNapomena.Text;
                                break;
                            case "Odredišna špedicija":
                                updatestring = " Update uvoz set OdredisnaSpedicija = " + Convert.ToInt32(cbOspedicija.SelectedValue);
                                break;
                            case "Carinarnica":
                                updatestring = " Update uvoz set OdredisnaCarina = " + Convert.ToInt32(cbOcarina.SelectedValue);
                                break;
                            case "Mesto istovara":
                                updatestring = " Update uvoz set MestoIstovara = '" + Convert.ToInt32(txtMesto.SelectedValue) + "'";
                                break;
                            case "Kontakt osoba":
                                updatestring = " Update uvoz set KontaktOsoba = '" + Convert.ToInt32(txtKontaktOsoba.SelectedValue) + "'";
                                break;
                            case "EMail":
                                updatestring = " Update uvoz set Email = '" + txtMail.Text.ToString() + "'";
                                break;
                            default:
                                Console.WriteLine("Nema podatka");
                                break;

                        }


                        try
                        {
                            // 1. Create Command
                            // Sql Update Statement
                            string updateSql = updatestring + " where ID = " + IdZaPromenu;

                            SqlConnection conn = new SqlConnection(connection);
                            SqlCommand cmd = new SqlCommand(updateSql, conn);
                            conn.Open();
                            var q = cmd.ExecuteNonQuery();
                            conn.Close();


                        }

                        catch (SqlException ex)
                        {
                            // Display error
                            Console.WriteLine("Error: " + ex.ToString());
                        }



                        //FillGV();
                    }
                    private void button7_Click(object sender, EventArgs e)
                    {
                        int IDZaPromenu = 0;
                        try
                        {
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (row.Selected)
                                {
                                    IDZaPromenu = Convert.ToInt32(row.Cells[0].Value.ToString());
                                    UpdateVrednostiPolja(IDZaPromenu);
                                }
                            }
                            // FillGV();

                        }
                        catch
                        {
                            MessageBox.Show("Nije uspela selekcija stavki");
                        }
            */
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PunjenjeVrednostiPolja();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

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

        private void button25_Click(object sender, EventArgs e)
        {
            frmUvozKopirajKontejner kk = new frmUvozKopirajKontejner(Convert.ToInt16(textBox1.Text));
            kk.Show();
           
           


        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (terminal == 0)
            {
                RefreshGV();
            }
            else
            {
                RefreshGVT();
            }
        }
    }
}
