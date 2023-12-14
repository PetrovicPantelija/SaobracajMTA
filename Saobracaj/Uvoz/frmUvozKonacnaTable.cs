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

namespace Saobracaj.Uvoz
{
    public partial class frmUvozKonacnaTable : Form
    {
        int Selektovani = 0;
        private Keys keyData;
        public frmUvozKonacnaTable()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            InitializeComponent();
        }

        public frmUvozKonacnaTable(string IDNAdredjeni)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            InitializeComponent();
            txtNadredjeni.Text = IDNAdredjeni;
        }

        public string GetID()
        {
            return textBox1.Text;
        }

        private void frmUvozKonacnaTable_Load(object sender, EventArgs e)
        {
            var select = "    SELECT UvozKonacna.ID, BrojKontejnera,   CASE WHEN Prioritet > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Prioritet ,  CASE WHEN DobijenBZ > 0 THEN Cast(1 as bit) " +
           " ELSE Cast(0 as BIT) END as DobijenBZ ,TipKontenjera.Naziv as Vrsta_Kontejnera, DobijeBZ as DatumBZ ,  p1.PaNaziv as Uvoznik,   Brodovi.Naziv as Brod,n1.PaNaziv as Nalogodavac1, " +
           " Ref1 as Ref1, n2.PaNaziv as Nalogodavac2, Ref2 as Ref2, n3.PaNaziv as Nalogodavac3, Ref3 as Ref3, DobijenNalogBrodara as Dobijen_Nalog_Brodara ,BrodskaTeretnica as BL,  " +
           " Napomena1 as Napomena1, PIN,    BrodskaTeretnica,KontejnerskiTerminali.Naziv as R_L_SRB,  VrstaRobeADR.Naziv as ADR, b.PaNaziv as Brodar, pv.PaNaziv as VlasnikKontejnera,  " +
           " pp1.Naziv as Dirigacija_Kontejnera_Za,                VrstaPregleda as InsTret,p2.PaNaziv as SpedicijaRTC,  p3.PaNaziv as SpedicijaGranica,       VrstaCarinskogPostupka.Naziv as CarinskiPostupak,  " +
           " VrstePostupakaUvoz.Naziv as PostupakSaRobom,uvNacinPakovanja.Naziv as NacinPakovanja, Napomena as Napomena2,                         Carinarnice.Naziv as Carinarnica,   " +
           " p4.PaNaziv as OdredisnaSpedicija, MestaUtovara.Naziv as MestoIstovara, AdresaMestaUtovara, KontaktOsobe,  Email,  " +
           " BrojPlombe1, BrojPlombe2,    NetoRobe, BrutoRobe, TaraKontejnera, BrutoKontejnera,  Koleta FROM UvozKonacna inner join Partnerji on PaSifra = VlasnikKontejnera " +
           " inner join Partnerji p1 on p1.PaSifra = Uvoznik  inner join Partnerji p2 on p2.PaSifra = SpedicijaRTC  inner join Partnerji p3 on p3.PaSifra = SpedicijaGranica " +
           " inner join VrstaRobeHS on VrstaRobeHS.ID = UvozKonacna.NazivRobe   inner join NHM on NHM.ID = NHMBroj  inner join TipKontenjera on TipKontenjera.ID = UvozKonacna.TipKontejnera " +
           " inner join Carinarnice on Carinarnice.ID = UvozKonacna.OdredisnaCarina   inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = UvozKonacna.CarinskiPostupak " +
           " inner join KontejnerskiTerminali on KontejnerskiTerminali.ID = UvozKonacna.RLTErminali   inner join Partnerji n1 on n1.PaSifra = Nalogodavac1 " +
           " inner join Partnerji n2 on n2.PaSifra = Nalogodavac2   inner join Partnerji n3 on n3.PaSifra = Nalogodavac3   inner join Partnerji b on b.PaSifra = UvozKonacna.Brodar " +
           " inner join DirigacijaKontejneraZa pp1 on pp1.ID = UvozKonacna.DirigacijaKontejeraZa     inner join Brodovi on Brodovi.ID = UvozKonacna.NazivBroda    inner join VrstaRobeADR on VrstaRobeADR.ID = ADR     inner join VrstePostupakaUvoz on VrstePostupakaUvoz.ID = PostupakSaRobom   " +
           "  inner join MestaUtovara on UvozKOnacna.MestoIstovara = MestaUtovara.ID  " +
           "inner join uvNacinPakovanja on uvNacinPakovanja.ID = NacinPakovanja  inner join Partnerji p4 on p4.PaSifra = OdredisnaSpedicija " +
           " inner join Partnerji pv on pv.PaSifra = UvozKonacna.VlasnikKontejnera " +
           " where UvozKonacna.IdNadredjeni = " + Convert.ToInt32(txtNadredjeni.Text) + " order by UvozKonacna.ID desc";


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

        private void gridGroupingControl1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
            {
                Close();
            }
        }
    }
}
