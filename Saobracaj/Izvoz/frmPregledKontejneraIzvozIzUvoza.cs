using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Izvoz
{
    public partial class frmPregledKontejneraIzvozIzUvoza : Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

        public frmPregledKontejneraIzvozIzUvoza()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            InitializeComponent();
            textBox1.Text = VratiBrojPlanova().ToString();
        }

        int VratiBrojPlanova()
        {
            int BrPor = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);


            con.Open();

            SqlCommand cmd = new SqlCommand("select Count(ID) as Broj" +
            "  from IzvozKonacnaZaglavlje"
           , con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                BrPor = Convert.ToInt32(dr["Broj"].ToString());

            }

            con.Close();

            return BrPor;
        }

        private void frmPregledKontejneraIzvozIzUvoza_Load(object sender, EventArgs e)
        {
            RefreshUvoz();
            RefreshIzvoz();
            SqlConnection conn = new SqlConnection(connection);
            var bro = "Select distinct ID From UvozKonacnaZaglavlje  order by ID desc";
            var broAD = new SqlDataAdapter(bro, conn);
            var broDS = new DataSet();
            broAD.Fill(broDS);
            cboVoz.DataSource = broDS.Tables[0];
            cboVoz.DisplayMember = "ID";
            cboVoz.ValueMember = "ID";
        }
        private void RefreshUvoz()
        {
            var select = "SELECT Uvoz.ID, [BrojKontejnera], BrodskaTeretnica as BL, DobijenNalogBrodara as Dobijen_Nalog_Brodara ,ATABroda, Brodovi.Naziv as Brod,Napomena1 as Napomena1, " +
" DobijeBZ as DatumBZ ,PIN,  [BrojKontejnera], TipKontenjera.Naziv as Vrsta_Kontejnera,  KontejnerskiTerminali.Naziv as R_L_SRB, pp1.Naziv as Dirigacija_Kontejnera_Za,  " +
" BrodskaTeretnica, VrstaRobeADR.Naziv as ADR, b.PaNaziv as Brodar, n1.PaNaziv as Nalogodavac1, Ref1 as Ref1, n2.PaNaziv as Nalogodavac2, Ref2 as Ref2, " +
" n3.PaNaziv as Nalogodavac3, Ref3 as Ref3,       p1.PaNaziv as Uvoznik,  " +
" (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaManipulacije.Naziv as nvarchar(20)) " +
" FROM UvozVrstaManipulacije " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = UvozVrstaManipulacije.IDVrstaManipulacije " +
" where UvozVrstaManipulacije.IDNadredjena = Uvoz.ID         FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as VrsteUsluga,  " +
" (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaRobeHS.HSKod as nvarchar(20))   FROM UvozVrstaRobeHS " +
" inner join VrstaRobeHS on UvozVrstaRobeHS.IDVrstaRobeHS = VrstaRobeHS.ID   where UvozVrstaRobeHS.IDNadredjena = Uvoz.ID " +
" FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as HS,   " +
" (SELECT  STUFF((SELECT distinct    '/' + Cast(NHM.Broj as nvarchar(20)) " +
" FROM UvozNHM  inner join NHM on UvozNHM.IDNHM = NHM.ID  where UvozNHM.IDNadredjena = Uvoz.ID   FOR XML PATH('')), 1, 1, ''  ) As Skupljen) as NHM,  " +
" VrstaPregleda as VrstaPregleda,p2.PaNaziv as SpedicijaRTC,  p3.PaNaziv as SpedicijaGranica,       VrstaCarinskogPostupka.Naziv as CarinskiPostupak,  " +
" VrstePostupakaUvoz.Naziv as PostupakSaRobom,uvNacinPakovanja.Naziv as NacinPakovanja, Napomena as Napomena2, " +
" Carinarnice.Naziv as Carinarnica,  " +
" p4.PaNaziv as OdredisnaSpedicija, MestaUtovara.Naziv as MestoIstovara, (partnerjiKontOsebaMU.PaKOIme + '' + partnerjiKontOsebaMU.PaKOPriimek) as KontaktOsoba, Email,        BrojPlombe1, BrojPlombe2,    PredefinisanePoruke.Naziv as NapomenaZaPozicioniranje, " +
" NetoRobe, BrutoRobe, TaraKontejnera, BrutoKontejnera,  Koleta, green FROM Uvoz left join Partnerji on PaSifra = VlasnikKontejnera " +
" left join Partnerji p1 on p1.PaSifra = Uvoznik  left join Partnerji p2 on p2.PaSifra = SpedicijaRTC  left join Partnerji p3 on p3.PaSifra = SpedicijaGranica " +
" left join TipKontenjera on TipKontenjera.ID = Uvoz.TipKontejnera " +
" left join Carinarnice on Carinarnice.ID = Uvoz.OdredisnaCarina  left join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = Uvoz.CarinskiPostupak " +
" left join Predefinisaneporuke on PredefinisanePoruke.ID = Uvoz.NapomenaZaPozicioniranje   left join KontejnerskiTerminali on KontejnerskiTerminali.ID = Uvoz.RLTErminali " +
" left join Partnerji n1 on n1.PaSifra = Nalogodavac1   left join Partnerji n2 on n2.PaSifra = Nalogodavac2   left join Partnerji n3 on n3.PaSifra = Nalogodavac3 " +
" left join Partnerji b on b.PaSifra = Uvoz.Brodar  left join  DirigacijaKontejneraZa pp1 on pp1.ID = Uvoz.DirigacijaKontejeraZa " +
" left join Brodovi on Brodovi.ID = Uvoz.NazivBroda    left join VrstaRobeADR on VrstaRobeADR.ID = ADR " +
" left join VrstePostupakaUvoz on VrstePostupakaUvoz.ID = PostupakSaRobom " +
" left join MestaUtovara on Uvoz.MestoIstovara = MestaUtovara.ID " +
" left join partnerjiKontOsebaMU on Uvoz.KontaktOsoba = partnerjiKontOsebaMU.PaKOSifra " +
" left join uvNacinPakovanja on uvNacinPakovanja.ID = NacinPakovanja  left join Partnerji p4 on p4.PaSifra = OdredisnaSpedicija  order by Uvoz.ID desc ";

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
        private void RefreshIzvoz()
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
    }


}
