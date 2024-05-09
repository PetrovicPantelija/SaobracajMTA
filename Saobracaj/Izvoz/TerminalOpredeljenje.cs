using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Izvoz
{
    public partial class TerminalOpredeljenje : Form
    {
        int Selektovani = 0;
        private Keys keyData;
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        public TerminalOpredeljenje()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            InitializeComponent();

        }

        private void RefreshGV()
        {
            var select = " Select Kontejner, KontejnerStatus.Naziv as Kontejner_Status, Skladista.Naziv as Skladiste, Pokret, KontejnerTekuce.Stanje, KontejnerTekuce.RLSRB, " +
" KontejnerTekuce.StatusIZvoz, Ostecenja, Kvalitet, CIR from KontejnerTekuce " +
"  inner join KontejnerStatus on KontejnerStatus.ID = KontejnerTekuce.StatusKontejnera " +
" inner join Skladista on Skladista.ID = KontejnerTekuce.Skladiste  ";


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

        private void RefreshGV2()
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
    " LEFT JOIN         uvNacinPakovanja ON Izvoz.NacinPakovanja = uvNacinPakovanja.ID where Izvoz.BrojKontejnera = '' order by Izvoz.ID desc   ";


            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            // dataGridView1.ReadOnly = true;
            gridGroupingControl2.DataSource = ds.Tables[0];
            gridGroupingControl2.ShowGroupDropArea = true;
            this.gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;
            foreach (GridColumnDescriptor column in this.gridGroupingControl2.TableDescriptor.Columns)
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
                    txtBrojKontejnera.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("Kontejner").ToString();

                    // txtSifra.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void gridGroupingControl2_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            try
            {
                if (gridGroupingControl2.Table.CurrentRecord != null)
                {
                    txtID.Text = gridGroupingControl2.Table.CurrentRecord.GetValue("ID").ToString();

                    // txtSifra.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InsertIzvoz io = new InsertIzvoz();
            io.IzvozOpredelioTerminal(Convert.ToInt32(txtID.Text), txtBrojKontejnera.Text);
            RefreshGV();
            RefreshGV2();
            MessageBox.Show("Kontejner je opredeljen");
        }

        private void TerminalOpredeljenje_Load(object sender, EventArgs e)
        {
            RefreshGV();
            RefreshGV2(); //Kontejneri koji Nemaju
        }
    }
}
