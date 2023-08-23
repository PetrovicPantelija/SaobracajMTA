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

namespace Saobracaj.Uvoz
{
    public partial class frmPregledNerasporedjeni : Form
    {
        public frmPregledNerasporedjeni()
        {
            InitializeComponent();
        }

        private void RefreshDataGrid()
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
" NetoRobe, BrutoRobe, TaraKontejnera, BrutoKontejnera,  Koleta, green FROM Uvoz inner join Partnerji on PaSifra = VlasnikKontejnera " +
" inner join Partnerji p1 on p1.PaSifra = Uvoznik  inner join Partnerji p2 on p2.PaSifra = SpedicijaRTC  inner join Partnerji p3 on p3.PaSifra = SpedicijaGranica " +
" inner join TipKontenjera on TipKontenjera.ID = Uvoz.TipKontejnera " +
" Inner join Carinarnice on Carinarnice.ID = Uvoz.OdredisnaCarina  inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = Uvoz.CarinskiPostupak " +
" inner join Predefinisaneporuke on PredefinisanePoruke.ID = Uvoz.NapomenaZaPozicioniranje   inner join KontejnerskiTerminali on KontejnerskiTerminali.ID = Uvoz.RLTErminali " +
" inner join Partnerji n1 on n1.PaSifra = Nalogodavac1   inner join Partnerji n2 on n2.PaSifra = Nalogodavac2   inner join Partnerji n3 on n3.PaSifra = Nalogodavac3 " +
" inner join Partnerji b on b.PaSifra = Uvoz.Brodar  inner join  DirigacijaKontejneraZa pp1 on pp1.ID = Uvoz.DirigacijaKontejeraZa " +
" inner join Brodovi on Brodovi.ID = Uvoz.NazivBroda    inner join VrstaRobeADR on VrstaRobeADR.ID = ADR " +
" inner join VrstePostupakaUvoz on VrstePostupakaUvoz.ID = PostupakSaRobom " +
" INNER join MestaUtovara on Uvoz.MestoIstovara = MestaUtovara.ID " +
" inner join partnerjiKontOsebaMU on Uvoz.KontaktOsoba = partnerjiKontOsebaMU.PaKOSifra " +
" inner join uvNacinPakovanja on uvNacinPakovanja.ID = NacinPakovanja  inner join Partnerji p4 on p4.PaSifra = OdredisnaSpedicija  order by Uvoz.ID desc "; 
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
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
           
                        DataGridViewColumn column = dataGridView1.Columns[0];
                        dataGridView1.Columns[0].HeaderText = "ID";
                     
                        dataGridView1.Columns[0].Width = 50;

                        DataGridViewColumn column1 = dataGridView1.Columns[1];
                        dataGridView1.Columns[1].HeaderText = "BrojKontejnera";
                     
                        dataGridView1.Columns[1].Width = 100;

                        DataGridViewColumn column2 = dataGridView1.Columns[2];
                        dataGridView1.Columns[2].HeaderText = "BL";
                        dataGridView1.Columns[2].Frozen = true;
                        dataGridView1.Columns[2].Width = 90;

                        DataGridViewColumn column3 = dataGridView1.Columns[3];
                        dataGridView1.Columns[3].HeaderText = "Dobijen_Nalog_Brodara";
                       // dataGridView1.Columns[1].Frozen = true;
                        dataGridView1.Columns[3].Width = 50;

                        DataGridViewColumn column4 = dataGridView1.Columns[4];
                        dataGridView1.Columns[4].HeaderText = "ATABroda";
                        dataGridView1.Columns[4].Width = 80;

                        DataGridViewColumn column5 = dataGridView1.Columns[5];
                        dataGridView1.Columns[5].HeaderText = "Brod";
                        dataGridView1.Columns[5].Width = 100;

                        DataGridViewColumn column6 = dataGridView1.Columns[6];
                        dataGridView1.Columns[6].HeaderText = "Napomena";
                        dataGridView1.Columns[6].Width = 100;

                        DataGridViewColumn column7 = dataGridView1.Columns[7];
                        dataGridView1.Columns[7].HeaderText = "DatumBZ";
                        dataGridView1.Columns[7].Width = 80;

                        DataGridViewColumn column8 = dataGridView1.Columns[8];
                        dataGridView1.Columns[8].HeaderText = "PIN";
                        dataGridView1.Columns[8].Width = 60;

                        DataGridViewColumn column9 = dataGridView1.Columns[9];
                        dataGridView1.Columns[9].HeaderText = "BrojKontejnera";
                     //   dataGridView1.Columns[7].Frozen = true;
                        dataGridView1.Columns[9].Width = 100;

                        DataGridViewColumn column10 = dataGridView1.Columns[10];
                        dataGridView1.Columns[10].HeaderText = "Vrsta kontejnera";
                        dataGridView1.Columns[10].Width = 120;

                        DataGridViewColumn column11 = dataGridView1.Columns[11];
                        dataGridView1.Columns[11].HeaderText = "R_L_SRB";
                        dataGridView1.Columns[11].Width = 120;

                        DataGridViewColumn column12 = dataGridView1.Columns[12];
                        dataGridView1.Columns[12].HeaderText = "Dirigacija_Kontejnera_Za";
                        dataGridView1.Columns[12].Width = 100;

                        DataGridViewColumn column13 = dataGridView1.Columns[13];
                        dataGridView1.Columns[13].HeaderText = "BL";
                       // dataGridView1.Columns[13].Frozen = true;
                        dataGridView1.Columns[13].Width = 90;
           

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

        private void frmPregledNerasporedjeni_Load(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Uvoz fUvoz = new Uvoz();
            fUvoz.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Uvoz pUvoz = new Uvoz(Convert.ToInt32(txtSifra.Text));
            pUvoz.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
