using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Uvoz
{
    public partial class frmAnalizaUvoza : Form
    {
        public frmAnalizaUvoza()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzcwMDg5QDMxMzgyZTM0MmUzMFhQSmlDM0M2bGpxcXVtT1VScTg1a0dtVTFLcUZiK0tLRnpvRTYyRFpMc3M9");

            InitializeComponent();

        }

        private void frmAnalizaUvoza_Load(object sender, EventArgs e)
        {
            var select = "SELECT UvozKonacna.ID, UvozKonacnaZaglavlje.ID as PLANID, Voz.BrVoza, Voz.ID as IDVOZA,Relacija, Voz.Napomena as NapomenaVoz,  BrojKontejnera,   CASE WHEN Prioritet > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Prioritet ,  CASE WHEN DobijenBZ > 0 THEN Cast(1 as bit) " +
" ELSE Cast(0 as BIT) END as DobijenBZ ,TipKontenjera.Naziv as Vrsta_Kontejnera, DobijeBZ as DatumBZ ,  p1.PaNaziv as Uvoznik,   Brodovi.Naziv as Brod,n1.PaNaziv as Nalogodavac1, " +
" Ref1 as Ref1, n2.PaNaziv as Nalogodavac2, Ref2 as Ref2, n3.PaNaziv as Nalogodavac3, Ref3 as Ref3, DobijenNalogBrodara as Dobijen_Nalog_Brodara ,BrodskaTeretnica as BL,  " +
" Napomena1 as Napomena1, PIN,    BrodskaTeretnica,KontejnerskiTerminali.Naziv as R_L_SRB,  VrstaRobeADR.Naziv as ADR, b.PaNaziv as Brodar, pv.PaNaziv as VlasnikKontejnera,  " +
"  pp1.Naziv as Dirigacija_Kontejnera_Za,                VrstaPregleda as InsTret,p2.PaNaziv as SpedicijaRTC,  p3.PaNaziv as SpedicijaGranica,       VrstaCarinskogPostupka.Naziv as CarinskiPostupak,  " +
" VrstePostupakaUvoz.Naziv as PostupakSaRobom,uvNacinPakovanja.Naziv as NacinPakovanja, UvozKonacna.Napomena as Napomena2,                         Carinarnice.Naziv as Carinarnica,  " +
" p4.PaNaziv as OdredisnaSpedicija, MestaUtovara.Naziv as MestoIstovara, AdresaMestaUtovara, KontaktOsobe,  Email,  " +
"  BrojPlombe1, BrojPlombe2,    NetoRobe, BrutoRobe, TaraKontejnera, BrutoKontejnera,  Koleta FROM UvozKonacna " +
"  inner join UvozKonacnaZaglavlje on UvozKonacna.IDNadredjeni = UvozKonacnaZaglavlje.ID " +
"  inner join Voz on UvozKonacnaZaglavlje.IDVoza = Voz.ID " +
"  inner join Partnerji on PaSifra = VlasnikKontejnera " +
" inner join Partnerji p1 on p1.PaSifra = Uvoznik  inner join Partnerji p2 on p2.PaSifra = SpedicijaRTC  inner join Partnerji p3 on p3.PaSifra = SpedicijaGranica " +
" inner join VrstaRobeHS on VrstaRobeHS.ID = UvozKonacna.NazivRobe   inner join NHM on NHM.ID = NHMBroj  inner join TipKontenjera on TipKontenjera.ID = UvozKonacna.TipKontejnera " +
" inner join Carinarnice on Carinarnice.ID = UvozKonacna.OdredisnaCarina   inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = UvozKonacna.CarinskiPostupak " +
" inner join KontejnerskiTerminali on KontejnerskiTerminali.ID = UvozKonacna.RLTErminali   inner join Partnerji n1 on n1.PaSifra = Nalogodavac1 " +
" inner join Partnerji n2 on n2.PaSifra = Nalogodavac2   inner join Partnerji n3 on n3.PaSifra = Nalogodavac3   inner join Partnerji b on b.PaSifra = UvozKonacna.Brodar " +
" inner join DirigacijaKontejneraZa pp1 on pp1.ID = UvozKonacna.DirigacijaKontejeraZa     inner join Brodovi on Brodovi.ID = UvozKonacna.NazivBroda    inner join VrstaRobeADR on VrstaRobeADR.ID = ADR     inner join VrstePostupakaUvoz on VrstePostupakaUvoz.ID = PostupakSaRobom " +
" inner join MestaUtovara on UvozKOnacna.MestoIstovara = MestaUtovara.ID " +
" inner join uvNacinPakovanja on uvNacinPakovanja.ID = NacinPakovanja  inner join Partnerji p4 on p4.PaSifra = OdredisnaSpedicija " +
" inner join Partnerji pv on pv.PaSifra = UvozKonacna.VlasnikKontejnera order by UvozKonacna.ID desc ";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl1.DataSource = ds.Tables[0];
            gridGroupingControl1.ShowGroupDropArea = true;
            this.gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;
            foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }
            /*
              GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
              summaryColumnDescriptor.Appearance.AnySummaryCell.Interior = new BrushInfo(Color.FromArgb(192, 255, 162));
              summaryColumnDescriptor.DataMember = "Ukupno";
              summaryColumnDescriptor.Format = "{Sum}";
              summaryColumnDescriptor.Name = "Potrošeno";
              summaryColumnDescriptor.SummaryType = Syncfusion.Grouping.SummaryType.Int32Aggregate;

              GridSummaryRowDescriptor summaryRowDescriptor = new GridSummaryRowDescriptor();
              summaryRowDescriptor.SummaryColumns.Add(summaryColumnDescriptor);
              summaryRowDescriptor.Appearance.AnySummaryCell.Interior = new BrushInfo(Color.FromArgb(255, 231, 162));

              this.gridGroupingControl1.TableDescriptor.SummaryRows.Add(summaryRowDescriptor);
            */
        }
    }
}
