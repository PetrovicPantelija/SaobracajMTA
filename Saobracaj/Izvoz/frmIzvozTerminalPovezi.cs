using Syncfusion.GridHelperClasses;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Izvoz
{
    public partial class frmIzvozTerminalPovezi : Form
    {
        private void ChangeTextBox()
        {
            this.BackColor = Color.White;
            this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
            this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
            Office2010Colors.ApplyManagedColors(this, Color.White);
            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
          
            this.ControlBox = true;
            // this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
            
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                splitContainer1.Panel1.BackColor = Color.White;
                Office2010Colors.ApplyManagedColors(this, Color.White);

                foreach (Control control in splitContainer1.Panel1.Controls)
                {
                    if (control is System.Windows.Forms.Button buttons)
                    {

                        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                        buttons.FlatStyle = FlatStyle.Flat;
                    }
                }


                foreach (Control control in splitContainer1.Panel1.Controls)
                {

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
              
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }
        public frmIzvozTerminalPovezi()
        {
            InitializeComponent();
            RefreshSync();
            RefreshSync2();
            ChangeTextBox();
        }

        private void RefreshSync()
        {
            var select = "";
            if (chkNerasporedjeni.Checked == true)
            {
                select = " SELECT  Izvoz.ID as ID,  Izvoz.BrojKontejnera,  Izvoz.VrstaKontejnera as Vrk_ID, TipKontenjera.Naziv as VrstaKontejnera, Partnerji.PaNaziv as Brodar, Izvoz.BookingBrodara, " +
   " Izvoz.BrojVagona,   Izvoz.CutOffPort,Partnerji_2.PaNaziv AS Izvoznik,Partnerji_3.PaNaziv AS Nalogodavac1, Partnerji_4.PaNaziv AS kNalogodavac2, Partnerji_5.PaNaziv AS Nalogodavac3, " +
   " Izvoz.DobijenNalogKlijent1, Izvoz.BrodskaPlomba, Izvoz.OstalePlombe,  " +
   " Izvoz.NetoRobe, Izvoz.BrutoRobe, Izvoz.BrutoRobeO, Izvoz.BrojKoleta, Izvoz.BrojKoletaO, Izvoz.CBM, Izvoz.CBMO, Izvoz.VrednostRobeFaktura,  " +
   " (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaManipulacije.Naziv as nvarchar(20))   FROM IzvozVrstaManipulacije " +
   " inner join VrstaManipulacije on VrstaManipulacije.ID = IzvozVrstaManipulacije.IDVrstaManipulacije   where IzvozVrstaManipulacije.IDNadredjena = Izvoz.ID" +
   " FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as VrsteUsluga,        (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaRobeHS.HSKod as nvarchar(20)) " +
   " FROM IzvozVrstaRobeHS        inner join VrstaRobeHS on IzvozVrstaRobeHS.IDVrstaRobeHS = VrstaRobeHS.ID   where IzvozVrstaRobeHS.IDNadredjena = Izvoz.ID " +
   " FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as HS,       (SELECT  STUFF((SELECT distinct    '/' + Cast(NHM.Broj as nvarchar(20)) " +
   " FROM IzvozNHM  inner join NHM on IzvozNHM.IDNHM = NHM.ID  where IzvozNHM.IDNadredjena = Izvoz.ID   FOR XML PATH('')), 1, 1, ''  ) As Skupljen) as NHM,  " +
   " Izvoz.Valuta, KrajnjaDestinacija.Naziv AS KrajnjaDestinacija, VrstePostupakaUvoz.Naziv AS Postupak, KontejnerskiTerminali.Naziv AS PPCNT, " +
   " KontejnerskiTerminali.Oznaka, Izvoz.Cirada, Izvoz.PlaniraniDatumUtovara, MestaUtovara.Naziv AS MestoUtovara, Izvoz.KontaktOsoba,  " +
   " Carinarnice.Naziv AS Carinarnica, Carinarnice.CIOznaka, Partnerji_1.PaNaziv AS Spedicija,  AdresaSlanjaStatusa,    " +
   " NaslovSlanjaStatusa, Izvoz.EtaLeget, VrstaCarinskogPostupka.Naziv AS Reexport, InspekciskiTretman.Naziv AS InspekciskiTretman,   " +
   " Izvoz.AutoDana, Izvoz.NajavaVozila, Izvoz.DodatneNapomeneDrumski, Izvoz.Vaganje, Izvoz.VGMTezina, Izvoz.Tara, Izvoz.VGMBrod,   " +
   "   Izvoz.Napomena1REf, " +
   " Izvoz.Napomena2REf, Izvoz.Napomena3REf, Partnerji_6.PaNaziv AS SpediterRijeka, uvNacinPakovanja.Naziv AS NacinPakovanja,  " +
   " Izvoz.NacinPretovara FROM         Izvoz Left JOIN TipKontenjera ON Izvoz.VrstaKontejnera = TipKontenjera.ID LEFT JOIN " +
   " Partnerji ON Izvoz.Brodar = Partnerji.PaSifra LEFT JOIN         KrajnjaDestinacija ON Izvoz.KrajnaDestinacija = KrajnjaDestinacija.ID LEFT JOIN " +
   " VrstePostupakaUvoz ON Izvoz.Postupanje = VrstePostupakaUvoz.ID LEFT JOIN         KontejnerskiTerminali ON Izvoz.MestoPreuzimanja = KontejnerskiTerminali.id " +
   " LEFT JOIN " +
   " MestaUtovara ON Izvoz.MesoUtovara = MestaUtovara.ID " +
   " LEFT JOIN         Carinarnice ON Izvoz.MestoCarinjenja = Carinarnice.ID " +
   " LEFT JOIN        Partnerji AS Partnerji_1 ON Izvoz.Spedicija = Partnerji_1.PaSifra " +
   " LEFT JOIN         VrstaCarinskogPostupka ON Izvoz.NapomenaReexport = VrstaCarinskogPostupka.id " +
   " LEFT JOIN        InspekciskiTretman ON Izvoz.Inspekcija = InspekciskiTretman.ID " +
   " LEFT JOIN        Partnerji AS Partnerji_2 ON Izvoz.Izvoznik = Partnerji_2.PaSifra " +
   " LEFT JOIN        Partnerji AS Partnerji_3 ON Izvoz.Klijent1 = Partnerji_3.PaSifra " +
   " LEFT JOIN       Partnerji AS Partnerji_4 ON Izvoz.Klijent2 = Partnerji_4.PaSifra " +
   " LEFT JOIN         Partnerji AS Partnerji_5 ON Izvoz.Klijent3 = Partnerji_5.PaSifra LEFT JOIN " +
   " Partnerji AS Partnerji_6 ON Izvoz.SpediterRijeka = Partnerji_6.PaSifra " +
   " LEFT JOIN         uvNacinPakovanja ON Izvoz.NacinPakovanja = uvNacinPakovanja.ID where BrojKontejnera = '*' order by Izvoz.ID desc  ";
            }
           else
            {
                select = " SELECT  IzvozKonacna.ID as ID,  IzvozKonacna.BrojKontejnera,  IzvozKonacna.VrstaKontejnera as Vrk_ID, TipKontenjera.Naziv as VrstaKontejnera, Partnerji.PaNaziv as Brodar, IzvozKonacna.BookingBrodara, " +
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
     " LEFT JOIN         uvNacinPakovanja ON IzvozKonacna.NacinPakovanja = uvNacinPakovanja.ID where BrojKontejnera = '*' order by IzvozKonacna.ID desc  ";
            }

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
            //Wiring the Dynamic Filter to GridGroupingControl
            dynamicFilter.WireGrid(this.gridGroupingControl1);

            GridExcelFilter gridExcelFilter = new GridExcelFilter();

            //Wiring GridExcelFilter to GridGroupingControl
            gridExcelFilter.WireGrid(this.gridGroupingControl1);



        }

        private void RefreshSync2()
        {
            var select = "";

            select = "  Select KontejnerTekuce.Kontejner, KontejnerStatus.Naziv as Status, KontejnerTekuce.Pokret as Pokret, Skladista.Naziv as Skladiste, " +
" Skladista.Kapacitet, Pozicija.Opis as Pozicija, " +
" Ostecenja as Ispravnost, KontejnerskiTerminali.Naziv as RLSRB, Kvalitet, CIR, STATUSIzvoz , " +
" PArtnerji.PaNaziv as Brodar, puv.PaNAziv as Uvoznik, UlazniBroj as KontejnerID_UVoz, DatumGIN as DATUM_GATEIN, DatumOtpremaPlat  as DATUM_OTP_PLAT, OperacijaUradjena " +
" from KontejnerTekuce " +
" inner join KontejnerStatus on KontejnerStatus.ID = KontejnerTekuce.StatusKontejnera " +
" inner " +
" join Skladista on Skladista.Id = KontejnerTekuce.Skladiste " +
" inner " +
" join Pozicija on Pozicija.Id = KontejnerTekuce.Pozicija " +
" inner " +
" join PArtnerji on KontejnerTekuce.Brodar = Partnerji.PaSifra " +
" inner " +
" join PArtnerji as PUV on KontejnerTekuce.Uvoznik = PUV.PASifra " +
" left " +
" join KontejnerskiTerminali on KontejnerskiTerminali.ID = KOntejnerTekuce.RLSRB" +
" where KontejnerStatus.ID = 13";

            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl2.DataSource = ds.Tables[0];
            gridGroupingControl2.ShowGroupDropArea = true;
            this.gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;
            foreach (GridColumnDescriptor column in this.gridGroupingControl2.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }

            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            //Wiring the Dynamic Filter to GridGroupingControl
            dynamicFilter.WireGrid(this.gridGroupingControl2);

            GridExcelFilter gridExcelFilter = new GridExcelFilter();

            //Wiring GridExcelFilter to GridGroupingControl
            gridExcelFilter.WireGrid(this.gridGroupingControl2);



        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.gridGroupingControl1.Table.SelectedRecords.Count == 1 && this.gridGroupingControl2.Table.SelectedRecords.Count == 1)
            {
                foreach (SelectedRecord selectedRecord in this.gridGroupingControl1.Table.SelectedRecords)
                {
                    foreach (SelectedRecord selectedRecord2 in this.gridGroupingControl2.Table.SelectedRecords)
                    {
                        if (chkNerasporedjeni.Checked == true)
                        {
                            //Azurira kontejner u izvozu
                            InsertIzvozKonacna ins = new InsertIzvozKonacna();
                            ins.OdrediKontejnerTerminal(Convert.ToInt32(selectedRecord.Record.GetValue("ID").ToString()), selectedRecord2.Record.GetValue("Kontejner").ToString(), 0);
                            InsertIzvoz iz = new InsertIzvoz();
                            iz.UpdIzvozTerminalIzbor(Convert.ToInt32(textBox1.Text), txtOstalePlombe.Text, Convert.ToDecimal(txVGMBrodBruto.Value), Convert.ToDecimal(txtTaraKontejnera.Value), Convert.ToDecimal(txtOdvaganaTezina.Value), Convert.ToDecimal(txtCBMO.Value), Convert.ToInt32(txtKoletaO.Value), Convert.ToDecimal(txtBrutoO.Value), Convert.ToInt32(txtBokingBrodara.Text), Convert.ToDecimal(txtTaraZ.Value), 0);
                        }
                        else {
                            InsertIzvozKonacna ins = new InsertIzvozKonacna();
                            ins.OdrediKontejnerTerminal(Convert.ToInt32(selectedRecord.Record.GetValue("ID").ToString()), selectedRecord2.Record.GetValue("Kontejner").ToString(),1);
                            InsertIzvoz iz = new InsertIzvoz();
                            iz.UpdIzvozTerminalIzbor(Convert.ToInt32(textBox1.Text), txtOstalePlombe.Text, Convert.ToDecimal(txVGMBrodBruto.Value), Convert.ToDecimal(txtTaraKontejnera.Value), Convert.ToDecimal(txtOdvaganaTezina.Value), Convert.ToDecimal(txtCBMO.Value), Convert.ToInt32(txtKoletaO.Value), Convert.ToDecimal(txtBrutoO.Value), Convert.ToInt32(txtBokingBrodara.Text), Convert.ToDecimal(txtTaraZ.Value), 1);
                        }
                    }
                }
            }

            RefreshSync();
            RefreshSync2();

        }

        private void chkNerasporedjeni_CheckedChanged(object sender, EventArgs e)
        {
            RefreshSync();
            RefreshSync2();
        }

        private void frmIzvozTerminalPovezi_Load(object sender, EventArgs e)
        {

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

                if (chkNerasporedjeni.Checked == true)
                {
                    VratiPodatkeSelectIzvoz(Convert.ToInt32(textBox1.Text));
                }
                else 
                {
                    VratiPodatkeSelectIzvozKonacna(Convert.ToInt32(textBox1.Text));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void VratiPodatkeSelectIzvoz(int ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT [ID],[BrojVagona] ,[BrojKontejnera],[VrstaKontejnera] " +
   "   ,[BrodskaPlomba],[BookingBrodara],[Brodar] " +
   "      ,[NetoRobe],[BrutoRobe],[BrutoRobeO],[BrojKoleta] " +
   "      ,[BrojKoletaO],[CBM],[CBMO] " +
    "     ,[VGMTezina],[Tara],[VGMBrod] " +
   "      ,[SpediterRijeka],[OstalePlombe], VrstaBrodskePlombe,  VGMBrod2, TaraZ  " +
 "  FROM [Izvoz] where ID=" + ID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtBokingBrodara.Text = dr["BookingBrodara"].ToString();
                cboVrstaPlombe.SelectedValue = Convert.ToInt32(dr["VrstaBrodskePlombe"].ToString());
                txtOstalePlombe.Text = dr["OstalePlombe"].ToString();
                txtKoletaO.Value = Convert.ToDecimal(dr["BrojKoletaO"].ToString());
                txtBrutoO.Value = Convert.ToDecimal(dr["BrutoRobeO"].ToString());
                 txtCBMO.Value = Convert.ToDecimal(dr["CBMO"].ToString());     
                
                txVGMBrodBruto.Value = Convert.ToDecimal(dr["VGMBrod"].ToString());
                txtTaraKontejnera.Value = Convert.ToDecimal(dr["Tara"].ToString());
                txtOdvaganaTezina.Value = Convert.ToDecimal(dr["VGMTezina"].ToString());
                txtTaraZ.Value = Convert.ToDecimal(dr["TaraZ"].ToString());







            }



            con.Close();


        }

        private void VratiPodatkeSelectIzvozKonacna(int ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT [ID],[BrojVagona] ,[BrojKontejnera],[VrstaKontejnera] " +
   "   ,[BrodskaPlomba],[BookingBrodara],[Brodar] " +
   "      ,[NetoRobe],[BrutoRobe],[BrutoRobeO],[BrojKoleta] " +
   "      ,[BrojKoletaO],[CBM],[CBMO] " +
    "     ,[VGMTezina],[Tara],[VGMBrod] " +
   "      ,[SpediterRijeka],[OstalePlombe], VrstaBrodskePlombe,  VGMBrod2, TaraZ  " +
 "  FROM [IzvozKonacna] where ID=" + ID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtOstalePlombe.Text = dr["OstalePlombe"].ToString();
                txVGMBrodBruto.Value = Convert.ToDecimal(dr["VGMBrod"].ToString());
                txtTaraKontejnera.Value = Convert.ToDecimal(dr["Tara"].ToString());
                txtOdvaganaTezina.Value = Convert.ToDecimal(dr["VGMTezina"].ToString());
                txtCBMO.Value = Convert.ToDecimal(dr["CBMO"].ToString());
                txtKoletaO.Value = Convert.ToDecimal(dr["BrojKoletaO"].ToString());
                txtBrutoO.Value = Convert.ToDecimal(dr["BrutoRobeO"].ToString());
                txtBokingBrodara.Text = dr["BookingBrodara"].ToString();
                txtTaraZ.Value = Convert.ToDecimal(dr["TaraZ"].ToString());
            }



            con.Close();


        }
   
    }
}
