using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Uvoz
{
    public partial class frmPrebacivanjeIzPlanaUPlan : Form
    {
        public string connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        public frmPrebacivanjeIzPlanaUPlan()
        {
            InitializeComponent();
        }

        private void frmPrebacivanjeIzPlanaUPlan_Load(object sender, EventArgs e)
        {
            var planutovara = "select UvozKonacnaZaglavlje.ID, ( 'PLAN:' + Cast(UvozKonacnaZaglavlje.ID as nvarchar(4)) + ' VOZ: ' + Cast(BrVoza as nvarchar(15)) + ' Pl. datum otpreme: ' + Cast(Convert(Nvarchar(10), Voz.PlOtpreme, 104) as nvarchar(12)) + ' Operater SRB: ' + Partnerji.PaNaziv) as Naziv " +
   " from UvozKonacnaZaglavlje inner join Voz on Voz.Id = UvozKonacnaZaglavlje.IdVoza inner " +
   " join Partnerji on Partnerji.PaSifra = OperaterSrbija where Dolazeci = 1 order by UvozKonacnaZaglavlje.ID desc";
            var planutovaraSAD = new SqlDataAdapter(planutovara, connection);
            var planutovaraSDS = new DataSet();
            planutovaraSAD.Fill(planutovaraSDS);
            cboPlanUtovaraIz.DataSource = planutovaraSDS.Tables[0];
            cboPlanUtovaraIz.DisplayMember = "Naziv";
            cboPlanUtovaraIz.ValueMember = "ID";

            var planutovara2 = "select UvozKonacnaZaglavlje.ID, ( 'PLAN:' + Cast(UvozKonacnaZaglavlje.ID as nvarchar(4)) + ' VOZ: ' + Cast(BrVoza as nvarchar(15)) + ' Pl. datum otpreme: ' + Cast(Convert(Nvarchar(10), Voz.PlOtpreme, 104) as nvarchar(12)) + ' Operater SRB: ' + Partnerji.PaNaziv) as Naziv " +
" from UvozKonacnaZaglavlje inner join Voz on Voz.Id = UvozKonacnaZaglavlje.IdVoza inner " +
" join Partnerji on Partnerji.PaSifra = OperaterSrbija where Dolazeci = 1 order by UvozKonacnaZaglavlje.ID desc";
            var planutovara2SAD = new SqlDataAdapter(planutovara2, connection);
            var planutovara2SDS = new DataSet();
            planutovara2SAD.Fill(planutovara2SDS);
            cboPlanUtovaraU.DataSource = planutovara2SDS.Tables[0];
            cboPlanUtovaraU.DisplayMember = "Naziv";
            cboPlanUtovaraU.ValueMember = "ID";
        }

        private void RefreshDataGrid1()
        {
            var select = "    SELECT UvozKonacna.ID, BrojKontejnera,   CASE WHEN Prioritet > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Prioritet , " +
              " CASE WHEN DobijenBZ > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as DobijenBZ ,TipKontenjera.Naziv as Vrsta_Kontejnera, DobijeBZ as DatumBZ ,  p1.PaNaziv as Uvoznik,   Brodovi.Naziv as Brod,n1.PaNaziv as Nalogodavac1, Ref1 as Ref1, n2.PaNaziv as Nalogodavac2, Ref2 as Ref2, n3.PaNaziv as Nalogodavac3, Ref3 as Ref3, DobijenNalogBrodara as Dobijen_Nalog_Brodara ,BrodskaTeretnica as BL,   Napomena1 as Napomena1, PIN, " +
"   BrodskaTeretnica,KontejnerskiTerminali.Naziv as R_L_SRB,  VrstaRobeADR.Naziv as ADR, b.PaNaziv as Brodar, pv.PaNaziv as VlasnikKontejnera, " +
"    pp1.Naziv as Dirigacija_Kontejnera_Za,  " +
"              VrstaPregleda as InsTret,p2.PaNaziv as SpedicijaRTC,  p3.PaNaziv as SpedicijaGranica,      " +
" VrstaCarinskogPostupka.Naziv as CarinskiPostupak,   " +
"                  VrstePostupakaUvoz.Naziv as PostupakSaRobom,uvNacinPakovanja.Naziv as NacinPakovanja, Napomena as Napomena2,  " +
"                       Carinarnice.Naziv as Carinarnica,  " +
"                               p4.PaNaziv as OdredisnaSpedicija, MestaUtovara.Naziv as MestoIstovara, AdresaMestaUtovara, KontaktOsobe," +
"  (RTRIM(pkoMU.PaKOIme) + ' ' + RTRIM(pkoMU.PaKOPriimek)) as KontaktOsoba, Email,         BrojPlombe1, BrojPlombe2,   " +
" NetoRobe, BrutoRobe, TaraKontejnera, BrutoKontejnera, " +
" Koleta " +
" FROM UvozKonacna inner join Partnerji on PaSifra = VlasnikKontejnera " +
" inner join Partnerji p1 on p1.PaSifra = Uvoznik " +
" inner join Partnerji p2 on p2.PaSifra = SpedicijaRTC " +
" inner join Partnerji p3 on p3.PaSifra = SpedicijaGranica " +
"  inner join VrstaRobeHS on VrstaRobeHS.ID = UvozKonacna.NazivRobe " +
"  inner join NHM on NHM.ID = NHMBroj " +
" inner join TipKontenjera on TipKontenjera.ID = UvozKonacna.TipKontejnera " +
"  inner join Carinarnice on Carinarnice.ID = UvozKonacna.OdredisnaCarina " +
"  inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = UvozKonacna.CarinskiPostupak " +
"  inner join KontejnerskiTerminali on KontejnerskiTerminali.ID = UvozKonacna.RLTErminali " +
"  inner join Partnerji n1 on n1.PaSifra = Nalogodavac1 " +
"  inner join Partnerji n2 on n2.PaSifra = Nalogodavac2 " +
"  inner join Partnerji n3 on n3.PaSifra = Nalogodavac3 " +
"  inner join Partnerji b on b.PaSifra = UvozKonacna.Brodar " +
" inner join DirigacijaKontejneraZa pp1 on pp1.ID = UvozKonacna.DirigacijaKontejeraZa   " +
"  inner join Brodovi on Brodovi.ID = UvozKonacna.NazivBroda " +
                          "   inner join VrstaRobeADR on VrstaRobeADR.ID = ADR " +
                          "    inner join VrstePostupakaUvoz on VrstePostupakaUvoz.ID = PostupakSaRobom    " +
                          " inner join MestaUtovara on UvozKOnacna.MestoIstovara = MestaUtovara.ID " +
" inner join partnerjiKontOsebaMU on UvozKonacna.KontaktOsoba = partnerjiKontOsebaMU.PaKOSifra " +
                          "inner join uvNacinPakovanja " +
" on uvNacinPakovanja.ID = NacinPakovanja  inner join Partnerji p4 on p4.PaSifra = OdredisnaSpedicija " +
" inner join Partnerji pv on pv.PaSifra = UvozKonacna.VlasnikKontejnera " +
" inner join partnerjiKontOsebaMU pkoMU on pkoMU.PaKOZapSt = UvozKonacna.KontaktOsoba " +
" where UvozKonacna.IdNadredjeni =  " + Convert.ToInt32(cboPlanUtovaraIz.SelectedValue) + " order by UvozKonacna.Prioritet desc, UvozKonacna.ID";

            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
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

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;
        }

        private void RefreshDataGrid2()
        {
            var select = "    SELECT UvozKonacna.ID, BrojKontejnera,   CASE WHEN Prioritet > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Prioritet , " +
                        " CASE WHEN DobijenBZ > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as DobijenBZ ,TipKontenjera.Naziv as Vrsta_Kontejnera, DobijeBZ as DatumBZ ,  p1.PaNaziv as Uvoznik,   Brodovi.Naziv as Brod,n1.PaNaziv as Nalogodavac1, Ref1 as Ref1, n2.PaNaziv as Nalogodavac2, Ref2 as Ref2, n3.PaNaziv as Nalogodavac3, Ref3 as Ref3, DobijenNalogBrodara as Dobijen_Nalog_Brodara ,BrodskaTeretnica as BL,   Napomena1 as Napomena1, PIN, " +
        "   BrodskaTeretnica,KontejnerskiTerminali.Naziv as R_L_SRB,  VrstaRobeADR.Naziv as ADR, b.PaNaziv as Brodar, pv.PaNaziv as VlasnikKontejnera, " +
        "    pp1.Naziv as Dirigacija_Kontejnera_Za,  " +
         "              VrstaPregleda as InsTret,p2.PaNaziv as SpedicijaRTC,  p3.PaNaziv as SpedicijaGranica,      " +
         " VrstaCarinskogPostupka.Naziv as CarinskiPostupak,   " +
         "                  VrstePostupakaUvoz.Naziv as PostupakSaRobom,uvNacinPakovanja.Naziv as NacinPakovanja, Napomena as Napomena2,  " +
         "                       Carinarnice.Naziv as Carinarnica,  " +
         "                               p4.PaNaziv as OdredisnaSpedicija, MestaUtovara.Naziv as MestoIstovara, AdresaMestaUtovara, KontaktOsobe," +
         "  (RTRIM(pkoMU.PaKOIme) + ' ' + RTRIM(pkoMU.PaKOPriimek)) as KontaktOsoba, Email,         BrojPlombe1, BrojPlombe2,   " +
        " NetoRobe, BrutoRobe, TaraKontejnera, BrutoKontejnera, " +
        " Koleta " +
        " FROM UvozKonacna inner join Partnerji on PaSifra = VlasnikKontejnera " +
        " inner join Partnerji p1 on p1.PaSifra = Uvoznik " +
        " inner join Partnerji p2 on p2.PaSifra = SpedicijaRTC " +
        " inner join Partnerji p3 on p3.PaSifra = SpedicijaGranica " +
        "  inner join VrstaRobeHS on VrstaRobeHS.ID = UvozKonacna.NazivRobe " +
        "  inner join NHM on NHM.ID = NHMBroj " +
        " inner join TipKontenjera on TipKontenjera.ID = UvozKonacna.TipKontejnera " +
        "  inner join Carinarnice on Carinarnice.ID = UvozKonacna.OdredisnaCarina " +
        "  inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = UvozKonacna.CarinskiPostupak " +
        "  inner join KontejnerskiTerminali on KontejnerskiTerminali.ID = UvozKonacna.RLTErminali " +
        "  inner join Partnerji n1 on n1.PaSifra = Nalogodavac1 " +
        "  inner join Partnerji n2 on n2.PaSifra = Nalogodavac2 " +
        "  inner join Partnerji n3 on n3.PaSifra = Nalogodavac3 " +
        "  inner join Partnerji b on b.PaSifra = UvozKonacna.Brodar " +
        " inner join DirigacijaKontejneraZa pp1 on pp1.ID = UvozKonacna.DirigacijaKontejeraZa   " +
        "  inner join Brodovi on Brodovi.ID = UvozKonacna.NazivBroda " +
                                    "   inner join VrstaRobeADR on VrstaRobeADR.ID = ADR " +
                                    "    inner join VrstePostupakaUvoz on VrstePostupakaUvoz.ID = PostupakSaRobom    " +
                                    " inner join MestaUtovara on UvozKOnacna.MestoIstovara = MestaUtovara.ID " +
        " inner join partnerjiKontOsebaMU on UvozKonacna.KontaktOsoba = partnerjiKontOsebaMU.PaKOSifra " +
                                    "inner join uvNacinPakovanja " +
        " on uvNacinPakovanja.ID = NacinPakovanja  inner join Partnerji p4 on p4.PaSifra = OdredisnaSpedicija " +
        " inner join Partnerji pv on pv.PaSifra = UvozKonacna.VlasnikKontejnera " +
        " inner join partnerjiKontOsebaMU pkoMU on pkoMU.PaKOZapSt = UvozKonacna.KontaktOsoba " +
        " where UvozKonacna.IdNadredjeni =  " + Convert.ToInt32(cboPlanUtovaraU.SelectedValue) + " order by UvozKonacna.Prioritet desc, UvozKonacna.ID";

            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];


            dataGridView2.BorderStyle = BorderStyle.None;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView2.BackgroundColor = Color.White;

            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 50;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            RefreshDataGrid1();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid2();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    InsertUvozKonacna ins = new InsertUvozKonacna();
                    ins.PrenesiIzPlanUtovaraUPlanUtovara(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(cboPlanUtovaraIz.SelectedValue), Convert.ToInt32(cboPlanUtovaraU.SelectedValue));
                }
            }
            RefreshDataGrid1();
            RefreshDataGrid2();
        }

        private void cboPlanUtovaraU_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
