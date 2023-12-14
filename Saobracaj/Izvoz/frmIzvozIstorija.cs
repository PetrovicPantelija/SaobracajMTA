using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Data;
using Syncfusion.Drawing;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Grouping;

namespace Saobracaj.Izvoz
{
    public partial class frmIzvozIstorija : Form
    {
        public frmIzvozIstorija()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            InitializeComponent();
        }
        public string GetID()
        {
            return textBox1.Text;
        }

        private void frmIzvozIstorija_Load(object sender, EventArgs e)
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



            var s_connection = ConfigurationManager.ConnectionStrings["Saobracaj.Properties.Settings.TESTIRANJEConnectionString"].ConnectionString;
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
    }
}
