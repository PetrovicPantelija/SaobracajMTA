using Syncfusion.GridHelperClasses;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Saobracaj.Izvoz
{
    public partial class frmIzvozIstorija : Form
    {
        public frmIzvozIstorija()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            InitializeComponent();
            ChangeTextBox();
        }
        public string GetID()
        {
            return textBox1.Text;
        }

        private void ChangeTextBox()
        {


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;

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

                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        private void RefreshIstorija()
             {

            var select = " SELECT  IzvozKonacna.ID as ID,  IzvozKonacna.IDNadredjena as PlanID, Voz.ID as IDVOza, Voz.BrVoza, Voz.Relacija, IzvozKonacnaZaglavlje.Napomena ,IzvozKonacna.BrojKontejnera,   IzvozKonacna.VrstaKontejnera, TipKontenjera.Naziv,Partnerji.PaNaziv as Brodar,  IzvozKonacna.BookingBrodara, IzvozKonacna.CutOffPort, " +
   " Partnerji_2.PaNaziv AS Izvoznik, Partnerji_3.PaNaziv AS Nalogodavac1,Partnerji_4.PaNaziv AS Nalogodavac2, Partnerji_5.PaNaziv AS Nalogodavac3, " +
   " IzvozKonacna.EtaLeget, IzvozKonacna.BrojVagona, IzvozKonacna.BrodskaPlomba, " +
   " IzvozKonacna.OstalePlombe,      IzvozKonacna.NetoRobe, IzvozKonacna.BrutoRobe, IzvozKonacna.BrutoRobeO, " +
   " IzvozKonacna.BrojKoleta, IzvozKonacna.BrojKoletaO, IzvozKonacna.CBM, IzvozKonacna.CBMO, IzvozKonacna.VrednostRobeFaktura,  " +
   " IzvozKonacna.Valuta, KrajnjaDestinacija.Naziv AS KrajnjaDestinacija, VrstePostupakaUvoz.Naziv AS Postupak, KontejnerskiTerminali.Naziv AS PPCNT,  " +
   " KontejnerskiTerminali.Oznaka, IzvozKonacna.Cirada, IzvozKonacna.PlaniraniDatumUtovara, MestaUtovara.Naziv AS MestoUtovara, IzvozKonacna.KontaktOsoba, " +
   "  Carinarnice.Naziv AS Carinarnica, Carinarnice.CIOznaka, Partnerji_1.PaNaziv AS Spedicija,  AdresaSlanjaStatusa,           NaslovSlanjaStatusa, " +
   "  VrstaCarinskogPostupka.Naziv AS Reexport, InspekciskiTretman.Naziv AS InspekciskiTretman,  " +
   " IzvozKonacna.AutoDana, IzvozKonacna.NajavaVozila, IzvozKonacna.DodatneNapomeneDrumski, IzvozKonacna.Vaganje, IzvozKonacna.VGMTezina, IzvozKonacna.Tara, IzvozKonacna.VGMBrod, " +
   " IzvozKonacna.Napomena1REf, IzvozKonacna.DobijenNalogKlijent1,  " +
   " IzvozKonacna.Napomena2REf,  IzvozKonacna.Napomena3REf, Partnerji_6.PaNaziv AS SpediterRijeka, uvNacinPakovanja.Naziv AS NacinPakovanja,   " +
   " IzvozKonacna.NacinPretovara " +
   " FROM         IzvozKonacna Left JOIN TipKontenjera ON IzvozKonacna.VrstaKontejnera = TipKontenjera.ID " +
   " LEFT JOIN         Partnerji ON IzvozKonacna.Brodar = Partnerji.PaSifra LEFT JOIN         KrajnjaDestinacija ON IzvozKonacna.KrajnaDestinacija = KrajnjaDestinacija.ID " +
   " LEFT JOIN         VrstePostupakaUvoz ON IzvozKonacna.Postupanje = VrstePostupakaUvoz.ID LEFT JOIN         KontejnerskiTerminali ON IzvozKonacna.MestoPreuzimanja = KontejnerskiTerminali.id " +
   " LEFT JOIN         MestaUtovara ON IzvozKonacna.MesoUtovara = MestaUtovara.ID LEFT JOIN         Carinarnice ON IzvozKonacna.MestoCarinjenja = Carinarnice.ID " +
   " LEFT JOIN        Partnerji AS Partnerji_1 ON IzvozKonacna.Spedicija = Partnerji_1.PaSifra " +
   " LEFT JOIN         VrstaCarinskogPostupka ON IzvozKonacna.NapomenaReexport = VrstaCarinskogPostupka.id " +
   " LEFT JOIN        InspekciskiTretman ON IzvozKonacna.Inspekcija = InspekciskiTretman.ID " +
   " LEFT JOIN        Partnerji AS Partnerji_2 ON IzvozKonacna.Izvoznik = Partnerji_2.PaSifra " +
   " LEFT JOIN        Partnerji AS Partnerji_3 ON IzvozKonacna.Klijent1 = Partnerji_3.PaSifra " +
   "  LEFT JOIN       Partnerji AS Partnerji_4 ON IzvozKonacna.Klijent2 = Partnerji_4.PaSifra " +
   "  LEFT JOIN         Partnerji AS Partnerji_5 ON IzvozKonacna.Klijent3 = Partnerji_5.PaSifra " +
   " LEFT JOIN          Partnerji AS Partnerji_6 ON IzvozKonacna.SpediterRijeka = Partnerji_6.PaSifra " +
   " LEFT JOIN         uvNacinPakovanja ON IzvozKonacna.NacinPakovanja = uvNacinPakovanja.ID " +
   " inner join IzvozKonacnaZaglavlje ON  IzvozKonacnaZaglavlje.ID = IzvozKonacna.IDNadredjena " +
   " inner join Voz on  IzvozKonacnaZaglavlje.IDVoza = Voz.ID " +
   " order by IzvozKonacna.ID desc  ";



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



        private void frmIzvozIstorija_Load(object sender, EventArgs e)
        {
            RefreshIstorija();
        }

        private void RefreshUsluge()
        {

           var select = "   SELECT rn.[ID]  ,IzvozKonacna.BrojKontejnera , VrstaManipulacije.Naziv, [Uradjen]  , " +
                            " (select Top 1 Naziv from Scenario  inner join IzvozKonacna  on IzvozKonacna.Scenario = Scenario.ID  where IzvozKonacna.ID = rn.BrojOsnov) as ScenarioNaziv, " +
                            " '' as ScenarioNapomena, " +
                            "   (select Top 1 Voz.NAzivVoza as OznakaVoza from IzvozKonacnaZaglavlje " +
                 " inner join Voz on Voz.ID = IzvozKonacnaZaglavlje.IDVoza " +
                            "   where IzvozKonacnaZaglavlje.ID = rn.PlanID) as VozOdlaska , TipKontenjera.Naziv as Tipkontejnera, KontejnerStatus.Naziv, rn.[StatusIzdavanja]," +
                             " (select Top 1 PaNaziv from Partnerji  inner join IzvozKonacna  on IzvozKonacna.Brodar = Partnerji.PaSifra  where izvozKonacna.ID = rn.BrojOsnov) as Brodar, " +
                            " [OJIzdavanja]    " +
                            ", o1.Naziv as Izdao  ,[OJRealizacije]      ,o2.Naziv as Realizuje  ,[DatumIzdavanja]   " +
                            "   ,[DatumRealizacije]  ,rn.[Napomena] " +
              " , IzvozKonacnaVrstaManipulacije.IDVrstaManipulacije, [Osnov], PlanID as PlanUtovara " +
              " ,[BrojOsnov] as BrojOsnov ,  VezniNalogID ,[KorisnikIzdao]      ,[KorisnikZavrsio]       , uv.PaNaziv as Platilac " +
              " , rn.Pokret,  rn.TipDokPrevoza, rn.BrojDokPrevoza," +
              " rn.TipRN, rn.BrojRN   FROM RadniNalogInterni rn " +
              " inner join OrganizacioneJedinice as o1 on OjIzdavanja = O1.ID " +
              " inner join OrganizacioneJedinice as o2 on OjRealizacije = O2.ID " +
                    " inner join IzvozKonacnaVrstaManipulacije on IzvozKonacnaVrstaManipulacije.ID = rn.KonkretaIDUsluge" +
               " inner join IzvozKonacna on IzvozKonacna.ID = IzvozKonacnaVrstaManipulacije.IDNAdredjena " +
              " inner join VrstaManipulacije on VrstaManipulacije.ID = IzvozKonacnaVrstaManipulacije.IDVrstaManipulacije " +
              " inner join Partnerji uv on uv.PaSifra = IzvozKonacnaVrstaManipulacije.Platilac " +
                 " Inner join KontejnerStatus on KontejnerStatus.ID = rn.StatusKontejnera " +
              " inner join TipKontenjera on TipKontenjera.ID = IzvozKonacna.VrstaKontejnera" +
                      " where OJIzdavanja = " + 2  +
                      " order by rn.ID desc";


            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl2.DataSource = ds.Tables[0];
            gridGroupingControl2.ShowGroupDropArea = true;
            gridGroupingControl2.TableDescriptor.FrozenColumn = "ID";
            gridGroupingControl2.TableDescriptor.FrozenColumn = "BrojKontejnera";
            gridGroupingControl2.TableDescriptor.FrozenColumn = "Naziv";
            this.gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;

            GridConditionalFormatDescriptor gcfd3 = new GridConditionalFormatDescriptor();
            gcfd3.Appearance.AnyRecordFieldCell.BackColor = Color.Green;
            gcfd3.Appearance.AnyRecordFieldCell.TextColor = Color.Yellow;

            gcfd3.Expression = "[Uradjen] =  '1'";
            this.gridGroupingControl2.TableDescriptor.ConditionalFormats.Add(gcfd3);


            GridConditionalFormatDescriptor gcfd31 = new GridConditionalFormatDescriptor();
            gcfd31.Appearance.AnyRecordFieldCell.BackColor = Color.Blue;
            gcfd31.Appearance.AnyRecordFieldCell.TextColor = Color.Yellow;

            gcfd31.Expression = "[Uradjen] =  '2'";
            this.gridGroupingControl2.TableDescriptor.ConditionalFormats.Add(gcfd31);




            GridConditionalFormatDescriptor gcfd = new GridConditionalFormatDescriptor();
            gcfd.Appearance.AnyRecordFieldCell.BackColor = Color.Orange;
            gcfd.Appearance.AnyRecordFieldCell.TextColor = Color.Black;

            gcfd.Expression = "[StatusIzdavanja] =  'DOPUNA'";

            //To add the conditional format instances to the ConditionalFormats collection. 
            this.gridGroupingControl2.TableDescriptor.ConditionalFormats.Add(gcfd);

            GridConditionalFormatDescriptor gcfd2 = new GridConditionalFormatDescriptor();
            gcfd2.Appearance.AnyRecordFieldCell.BackColor = Color.DarkRed;
            gcfd2.Appearance.AnyRecordFieldCell.TextColor = Color.White;

            gcfd2.Expression = "[StatusIzdavanja] =  'STORNO'";
            this.gridGroupingControl2.TableDescriptor.ConditionalFormats.Add(gcfd2);


            //To add the conditional format instances to the ConditionalFormats collection. 


            this.gridGroupingControl2.TableDescriptor.Columns[0].Width = 50;
            this.gridGroupingControl2.TableDescriptor.Columns[1].Width = 120;

            this.gridGroupingControl2.TableDescriptor.VisibleColumns.Remove("OJIzdavanja");
            this.gridGroupingControl2.TableDescriptor.VisibleColumns.Remove("OJRealizacije");
            this.gridGroupingControl2.TableDescriptor.VisibleColumns.Remove("IDVrstaManipulacije");

            this.gridGroupingControl2.TableDescriptor.Columns["Uradjen"].Appearance.AnyRecordFieldCell.CellType = "CheckBox";

            this.gridGroupingControl2.TableDescriptor.Columns["Uradjen"].Appearance.AnyRecordFieldCell.CheckBoxOptions = new GridCheckBoxCellInfo("1", "0", "", true);
            this.gridGroupingControl2.TableDescriptor.Columns["Uradjen"].Appearance.AnyRecordFieldCell.ReadOnly = false;
            this.gridGroupingControl2.TableDescriptor.Columns["Uradjen"].Appearance.AnyRecordFieldCell.Enabled = true;
           

            foreach (GridColumnDescriptor column in this.gridGroupingControl2.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }

            GridExcelFilter gridExcelFilter = new GridExcelFilter();

            //Wiring GridExcelFilter to GridGroupingControl
            gridExcelFilter.WireGrid(this.gridGroupingControl2);

            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            dynamicFilter.WireGrid(this.gridGroupingControl2);

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

        private void button23_Click(object sender, EventArgs e)
        {
           RefreshUsluge();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshIstorija();
        }

        private void gridGroupingControl2_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            try
            {
                if (gridGroupingControl2.Table.CurrentRecord != null)
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
    }
}
