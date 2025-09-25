using iTextSharp.text.pdf.parser.clipper;
using Microsoft.Office.Interop.Excel;
using Syncfusion.GridHelperClasses;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.XlsIO.Parser.Biff_Records.Formula;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Uvoz
{
    public partial class frmPregledNerasporedjeni : Form
    {
        int Selektovani = 0;
        private Keys keyData;
        string KorisnikTekuci = Saobracaj.Sifarnici.frmLogovanje.user;

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
                pomMenu.Visible = false;
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = Color.White;
                Office2010Colors.ApplyManagedColors(this, Color.White);

               


                foreach (Control control in this.Controls)
                {

                    if (control is System.Windows.Forms.TextBox textBox)
                    {

                        textBox.BackColor = Color.White;// Example: Change background color
                        textBox.ForeColor = Color.FromArgb(51, 51, 54); //Boja slova u kvadratu
                        textBox.Font = new System.Drawing.Font("Helvetica", 9);
                        // Example: Change font
                    }


                    if (control is System.Windows.Forms.Label label)
                    {
                        // Change properties here
                        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        label.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font

                        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                    }
                    if (control is DateTimePicker dtp)
                    {
                        dtp.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        dtp.Font = new System.Drawing.Font("Helvetica", 9);
                    }
                    if (control is System.Windows.Forms.CheckBox chk)
                    {
                        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        chk.Font = new System.Drawing.Font("Helvetica", 9);
                    }

                    if (control is System.Windows.Forms.ListBox lb)
                    {
                        lb.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        lb.Font = new System.Drawing.Font("Helvetica", 9);
                    }

                }
            }
            else
            {
                panelHeader.Visible = false;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                pomMenu.Visible = true;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        private void PodesiDatagridView(DataGridView dgv)
        {
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(90, 199, 249); // Selektovana boja
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.BackgroundColor = Color.White;

            dgv.DefaultCellStyle.Font = new System.Drawing.Font("Helvetica", 12F, GraphicsUnit.Pixel);
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(51, 51, 54);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);
            dgv.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);


            //Header
            dgv.EnableHeadersVisualStyles = false;
            //   header.Style.Font = new Font("Arial", 12F, FontStyle.Bold);
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 54);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;


        }
      
        
        public frmPregledNerasporedjeni(string Kor)
        {
            InitializeComponent();
            KorisnikTekuci = Kor;
            ChangeTextBox();
        }
        public string GetID()
        {
            return txtSifra.Text;
        }

        private void RefreshDataGrid()
        {

            var select = "SELECT Uvoz.ID, EtaBroda, [BrojKontejnera],TipKontenjera.Naziv as Vrsta_Kontejnera, " +
" KontejnerskiTerminali.Naziv as T1,  " +
" k2.Naziv as T2, k3.Naziv as T3,  " +
" (select Top 1 Naziv from Scenario inner join Uvoz uv on uv.Scenario = Scenario.ID  where Uvoz.ID = uv.ID) as ScenarioNaziv, "+
" (select Top 1 stNapomene from UvozNapomenePozicioniranja inner join Uvoz uv on Uvoz.ID = UvozNapomenePozicioniranja.IDNadredjena  where Uvoz.ID = uv.ID order by UvozNapomenePozicioniranja.ID DEsc) as ScenarioNapomena, " +
" Napomena1 as Napomena1, Brodovi.Naziv as Brod,  BrodskaTeretnica as BL,  " +
" VrstaRobeADR.Naziv as ADR, PIN,b.PaNaziv as Brodar,n1.PaNaziv as NalogodavacZaVoz, n2.PaNaziv as Logisticar1,n3.PaNaziv as Logisticar2,  " +
" p1.PaNaziv as Uvoznik, " +
 " (SELECT  STUFF((SELECT distinct   '/' + '*' + TarifniBroj + '-' + (KomercijalniNaziv) " +
" from UvozNHM " +
"  where UvozNHM.IDNadredjena = Uvoz.ID " +
" FOR XML PATH('')), 1, 1, '') As Skupljen2)    as NHM, " +
" p3.PaNaziv as SpedicijaGranica,p2.PaNaziv as SpedicijaRTC,VrstaCarinskogPostupka.Naziv as CarinskiPostupak,  " +
" VrstePostupakaUvoz.Naziv as PostupakSaRobom,uvNacinPakovanja.Naziv as NacinPakovanja, p4.PaNaziv as OdredisnaSpedicija, Carinarnice.Naziv as Carinarnica,   " +
" Email, NetoRobe, BrutoRobe,  TaraKontejnera, BrutoKontejnera,     Koleta ," +
" CASE WHEN Prioritet > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Prioritet ,  " +
" CASE WHEN DobijenBZ > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as DobijenBZ ,   " +
" Ref1 as Ref1, Ref2 as Ref2,ATABroda,   " +
"  DobijeBZ as DatumBZ ,       " +
"   Ref3 as Ref3,         VrstaPregleda as InsTret,   " +
" Napomena as Napomena2,  " +
" MestaUtovara.Naziv as MestoIstovara, KontaktOsobe, " +
" BrojPlombe1, BrojPlombe2 FROM Uvoz inner join Partnerji on PaSifra = VlasnikKontejnera " +
" inner join Partnerji p1 on p1.PaSifra = Uvoznik " +
" inner join Partnerji p2 on p2.PaSifra = SpedicijaRTC " +
" inner join Partnerji p3 on p3.PaSifra = SpedicijaGranica " +
" inner join TipKontenjera on TipKontenjera.ID = Uvoz.TipKontejnera " +
" inner join Carinarnice on Carinarnice.ID = Uvoz.OdredisnaCarina " +
"  inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = Uvoz.CarinskiPostupak " +
"  inner join Predefinisaneporuke on PredefinisanePoruke.ID = Uvoz.NapomenaZaPozicioniranje " +
"  inner join KontejnerskiTerminali on KontejnerskiTerminali.ID = Uvoz.RLTErminali " +
"   inner join KontejnerskiTerminali k2 on k2.ID = Uvoz.RLTErminali2 " +
"    inner join KontejnerskiTerminali k3 on k3.ID = Uvoz.RLTErminali3 " +
"  inner join Partnerji n1 on n1.PaSifra = Nalogodavac1 " +
"  inner join Partnerji n2 on n2.PaSifra = Nalogodavac2 " +
"  inner join Partnerji n3 on n3.PaSifra = Nalogodavac3 " +
"  inner join Partnerji b on b.PaSifra = Uvoz.Brodar " +
"  inner join  DirigacijaKontejneraZa pp1 on pp1.ID = Uvoz.DirigacijaKontejeraZa " +
"  inner join Brodovi on Brodovi.ID = Uvoz.NazivBroda " +
"  inner join VrstaRobeADR on VrstaRobeADR.ID = ADR " +
"  inner join VrstePostupakaUvoz on VrstePostupakaUvoz.ID = PostupakSaRobom " +
"  inner join MestaUtovara on Uvoz.MestoIstovara = MestaUtovara.ID " +
"  inner join uvNacinPakovanja on uvNacinPakovanja.ID = NacinPakovanja " +
"  inner join Partnerji p4 on p4.PaSifra = OdredisnaSpedicija " +
"  inner join Partnerji pv on pv.PaSifra = Uvoz.VlasnikKontejnera " +
"   order by Uvoz.Prioritet desc, Uvoz.ID desc ";



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
            Uvoz pUvoz = new Uvoz(Convert.ToInt32(txtSifra.Text), KorisnikTekuci);
            pUvoz.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void gridGroupingControl1_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            try
            {
                if (gridGroupingControl1.Table.CurrentRecord != null)
                {
                    txtSifra.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();

                    // txtSifra.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void frmPregledNerasporedjeni_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
            {
                Close();
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            Uvoz fUvoz = new Uvoz();
            fUvoz.Show();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            Uvoz pUvoz = new Uvoz(Convert.ToInt32(txtSifra.Text), KorisnikTekuci);
            pUvoz.Show();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmUvozKopirajKontejner kk = new frmUvozKopirajKontejner(Convert.ToInt16(txtSifra.Text));
            kk.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.gridGroupingControl1.Table.SelectedRecords.Count > 0)
            {
                InsertUvoz uv = new InsertUvoz();
                foreach (SelectedRecord selectedRecord in this.gridGroupingControl1.Table.SelectedRecords)
                {
                    
                    uv.DelUvoz(Convert.ToInt32(selectedRecord.Record.GetValue("ID").ToString()));

                }
            }

            RefreshDataGrid();

        }
    }
}
