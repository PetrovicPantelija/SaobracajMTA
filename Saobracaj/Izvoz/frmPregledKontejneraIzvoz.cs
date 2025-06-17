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
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Windows.Forms;
using Syncfusion.GridHelperClasses;
using Saobracaj.Uvoz;
using Syncfusion.Grouping;

namespace Saobracaj.Izvoz
{
    public partial class frmPregledKontejneraIzvoz : Form
    {
        private string connect = Sifarnici.frmLogovanje.connectionString;

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
                meniHeader.Visible = false;
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = Color.White;
                Office2010Colors.ApplyManagedColors(this, Color.White);




                foreach (Control control in this.Controls)
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
                meniHeader.Visible = true;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }


        public frmPregledKontejneraIzvoz()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            ChangeTextBox();
        }

        private void RefreshDataGrid()
        {
            var select = " SELECT  Izvoz.ID as ID,  Izvoz.BrojKontejnera,  Izvoz.VrstaKontejnera as Vrk_ID, TipKontenjera.Naziv as VrstaKontejnera, Partnerji.PaNaziv as Brodar, Izvoz.BookingBrodara, " +
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
 " LEFT JOIN         uvNacinPakovanja ON Izvoz.NacinPakovanja = uvNacinPakovanja.ID order by Izvoz.ID desc  ";


          // var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
          //  SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            /*
                        DataGridViewColumn column = dataGridView1.Columns[0];
                        dataGridView1.Columns[0].HeaderText = "ID";
                        dataGridView1.Columns[0].Width = 50;

                        DataGridViewColumn column2 = dataGridView1.Columns[1];
                        dataGridView1.Columns[1].HeaderText = "Br voza";
                        dataGridView1.Columns[1].Width = 50;

                        DataGridViewColumn column3 = dataGridView1.Columns[2];
                        dataGridView1.Columns[2].HeaderText = "Relacija";
                        dataGridView1.Columns[2].Width = 150;

                        DataGridViewColumn column4 = dataGridView1.Columns[3];
                        dataGridView1.Columns[3].HeaderText = "Vr polaska";
                        dataGridView1.Columns[3].Width = 70;

                        DataGridViewColumn column5 = dataGridView1.Columns[4];
                        dataGridView1.Columns[4].HeaderText = "Vr dolaska";
                        dataGridView1.Columns[4].Width = 70;

                        DataGridViewColumn column6 = dataGridView1.Columns[5];
                        dataGridView1.Columns[5].HeaderText = "Max bruto";
                        dataGridView1.Columns[5].Width = 70;

                        DataGridViewColumn column7 = dataGridView1.Columns[6];
                        dataGridView1.Columns[6].HeaderText = "Max duž";
                        dataGridView1.Columns[6].Width = 70;

                        DataGridViewColumn column8 = dataGridView1.Columns[7];
                        dataGridView1.Columns[7].HeaderText = "Max br kola";
                        dataGridView1.Columns[7].Width = 70;

                        DataGridViewColumn column9 = dataGridView1.Columns[8];
                        dataGridView1.Columns[8].HeaderText = "Napomena";
                        dataGridView1.Columns[8].Width = 100;
            */

        }

        private void RefreshDataGridPoKontejneru()
        {
            var select = " SELECT  Izvoz.ID as ID,    Izvoz.VrstaKontejnera, TipKontenjera.Naziv, Izvoz.BrojVagona, Izvoz.BrojKontejnera, Izvoz.BrodskaPlomba, Izvoz.OstalePlombe, Izvoz.BookingBrodara, Partnerji.PaNaziv, " +
                   "    Izvoz.CutOffPort, Izvoz.NetoRobe, Izvoz.BrutoRobe, Izvoz.BrutoRobeO, Izvoz.BrojKoleta, Izvoz.BrojKoletaO, Izvoz.CBM, Izvoz.CBMO, Izvoz.VrednostRobeFaktura, " +
                    "  (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaManipulacije.Naziv as nvarchar(20))   FROM IzvozVrstaManipulacije " +
    "       inner join VrstaManipulacije on VrstaManipulacije.ID = IzvozVrstaManipulacije.IDVrstaManipulacije   where IzvozVrstaManipulacije.IDNadredjena = Izvoz.ID " +
    "        FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as VrsteUsluga,   " +
    "     (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaRobeHS.HSKod as nvarchar(20))   FROM IzvozVrstaRobeHS " +
    "       inner join VrstaRobeHS on IzvozVrstaRobeHS.IDVrstaRobeHS = VrstaRobeHS.ID   where IzvozVrstaRobeHS.IDNadredjena = Izvoz.ID " +
    "        FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as HS,   " +
    "    (SELECT  STUFF((SELECT distinct    '/' + Cast(NHM.Broj as nvarchar(20)) " +
   "            FROM IzvozNHM  inner join NHM on IzvozNHM.IDNHM = NHM.ID  where IzvozNHM.IDNadredjena = Izvoz.ID   FOR XML PATH('')), 1, 1, ''  ) As Skupljen) as NHM, " +
                   "        Izvoz.Valuta, KrajnjaDestinacija.Naziv AS KrajnjaDestinacija, VrstePostupakaUvoz.Naziv AS Postupak, KontejnerskiTerminali.Naziv AS PPCNT, " +
                   "        KontejnerskiTerminali.Oznaka, Izvoz.Cirada, Izvoz.PlaniraniDatumUtovara, MestaUtovara.Naziv AS MestoUtovara, Izvoz.KontaktOsoba,  " +
                   "        Carinarnice.Naziv AS Carinarnica, Carinarnice.CIOznaka, Partnerji_1.PaNaziv AS Spedicija,  AdresaSlanjaStatusa, " +
                   "          NaslovSlanjaStatusa, Izvoz.EtaLeget, VrstaCarinskogPostupka.Naziv AS Reexport, InspekciskiTretman.Naziv AS InspekciskiTretman, " +
                   "        Izvoz.AutoDana, Izvoz.NajavaVozila, Izvoz.DodatneNapomeneDrumski, Izvoz.Vaganje, Izvoz.VGMTezina, Izvoz.Tara, Izvoz.VGMBrod, " +
                   "        Partnerji_2.PaNaziv AS Izvoznik, Partnerji_3.PaNaziv AS Klijent1, Izvoz.Napomena1REf, Izvoz.DobijenNalogKlijent1, Partnerji_4.PaNaziv AS klijent2, " +
                   "        Izvoz.Napomena2REf, Partnerji_5.PaNaziv AS Klijent3, Izvoz.Napomena3REf, Partnerji_6.PaNaziv AS SpediterRijeka, uvNacinPakovanja.Naziv AS NacinPakovanja, " +
                   "        Izvoz.NacinPretovara " +
 "    FROM         Izvoz Left JOIN " +
                    "       TipKontenjera ON Izvoz.VrstaKontejnera = TipKontenjera.ID LEFT JOIN " +
                   "        Partnerji ON Izvoz.Brodar = Partnerji.PaSifra LEFT JOIN " +
                   "        KrajnjaDestinacija ON Izvoz.KrajnaDestinacija = KrajnjaDestinacija.ID LEFT JOIN " +
                   "        VrstePostupakaUvoz ON Izvoz.Postupanje = VrstePostupakaUvoz.ID LEFT JOIN " +
                   "        KontejnerskiTerminali ON Izvoz.MestoPreuzimanja = KontejnerskiTerminali.id LEFT JOIN " +
                   "        MestaUtovara ON Izvoz.MesoUtovara = MestaUtovara.ID LEFT JOIN " +
                   "        Carinarnice ON Izvoz.MestoCarinjenja = Carinarnice.ID LEFT JOIN " +
                    "       Partnerji AS Partnerji_1 ON Izvoz.Spedicija = Partnerji_1.PaSifra LEFT JOIN " +
                   "        VrstaCarinskogPostupka ON Izvoz.NapomenaReexport = VrstaCarinskogPostupka.id LEFT JOIN " +
                    "       InspekciskiTretman ON Izvoz.Inspekcija = InspekciskiTretman.ID LEFT JOIN " +
                    "       Partnerji AS Partnerji_2 ON Izvoz.Izvoznik = Partnerji_2.PaSifra LEFT JOIN " +
                    "       Partnerji AS Partnerji_3 ON Izvoz.Klijent1 = Partnerji_3.PaSifra LEFT JOIN " +
                     "      Partnerji AS Partnerji_4 ON Izvoz.Klijent2 = Partnerji_4.PaSifra LEFT JOIN " +
                   "        Partnerji AS Partnerji_5 ON Izvoz.Klijent3 = Partnerji_5.PaSifra LEFT JOIN " +
                  "         Partnerji AS Partnerji_6 ON Izvoz.SpediterRijeka = Partnerji_6.PaSifra LEFT JOIN " +
                   "        uvNacinPakovanja ON Izvoz.NacinPakovanja = uvNacinPakovanja.ID " +
                   " where Izvoz.BrojKontejnera like ('" + txtBrojKontejnera.Text + "%')" +
                   " order by Izvoz.ID desc ";


            // var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            //  SqlConnection myConnection = new SqlConnection(s_connection);
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txtSifra.Text = row.Cells[0].Value.ToString();

                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void frmPregledKontejneraIzvoz_Load(object sender, EventArgs e)
        {
            //  RefreshDataGrid();

            var select =  " SELECT  Izvoz.ID as ID,  Izvoz.BrojKontejnera,  Izvoz.VrstaKontejnera as Vrk_ID, TipKontenjera.Naziv as VrstaKontejnera, Partnerji.PaNaziv as Brodar, Izvoz.BookingBrodara, " +
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
            " LEFT JOIN         uvNacinPakovanja ON Izvoz.NacinPakovanja = uvNacinPakovanja.ID order by Izvoz.ID desc  ";

            //   var s_connection = Sifarnici.frmLogovanje.connectionString;
            //   SqlConnection myConnection = new SqlConnection(s_connection);
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

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Izvoz.frmIzvoz fIzvoz = new frmIzvoz();
            fIzvoz.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            frmIzvoz frmUIzvoz = new frmIzvoz(Convert.ToInt32(txtSifra.Text));
            frmUIzvoz.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            RefreshDataGridPoKontejneru();
        }

        private void gridGroupingControl1_SelectedRecordsChanging(object sender, Syncfusion.Grouping.SelectedRecordsChangedEventArgs e)
        {
          
        }

        private void gridGroupingControl1_SelectedRecordsChanged(object sender, Syncfusion.Grouping.SelectedRecordsChangedEventArgs e)
        {
          
        }

        private void gridGroupingControl1_Click(object sender, EventArgs e)
        {
       
        }

       

        private void gridGroupingControl1_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            try
            {
                if (gridGroupingControl1.Table.CurrentRecord != null)
                {
                    textBox1.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();
                    txtSifra.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
          

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtSifra.Text == "")
            {
                MessageBox.Show("Izaberite stavku");
                return;
            }
            frmIzvozKopiranjeKontejnera kk = new frmIzvozKopiranjeKontejnera(Convert.ToInt16(txtSifra.Text));
            kk.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.gridGroupingControl1.Table.SelectedRecords.Count > 0)
            {
                InsertIzvoz  iz = new InsertIzvoz();
                foreach (SelectedRecord selectedRecord in this.gridGroupingControl1.Table.SelectedRecords)
                {

                    iz.DelIzvozSve(Convert.ToInt32(selectedRecord.Record.GetValue("ID").ToString()));

                }
            }

            RefreshDataGrid();
        }
    }
}
