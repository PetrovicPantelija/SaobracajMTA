using Microsoft.Office.Interop.Excel;
using Saobracaj.Sifarnici;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Saobracaj.Uvoz
{
    public partial class frmUvozKonacna : Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        string nalogodavci = "";
        string usluge = "";
        int NHMObrni = 0;
        DataSet nhmSDSA;
        DataSet nhmSDS2A;

        float firstWidth;
        float firstHeight;
        string KorisnikTekuci = Sifarnici.frmLogovanje.user.ToString();
        int Zeleznina = 0;
        int ADR = 0;
        int ScenarioGl = 0;
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

                foreach (Control control in tabSplitterPage1.Controls)
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
                        textBox.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                        // Example: Change font
                    }


                    if (control is System.Windows.Forms.Label label)
                    {
                        // Change properties here
                        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        label.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);  // Example: Change font

                        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                    }
                    if (control is DateTimePicker dtp)
                    {
                        dtp.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        dtp.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }
                    if (control is System.Windows.Forms.CheckBox chk)
                    {
                        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        chk.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ListBox lb)
                    {
                        lb.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        lb.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ComboBox cb)
                    {
                        cb.ForeColor = Color.FromArgb(51, 51, 54);
                        cb.BackColor = Color.White;// Example: Change background color
                        cb.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.NumericUpDown nu)
                    {
                        nu.ForeColor = Color.FromArgb(51, 51, 54);
                        nu.BackColor = Color.White;// Example: Change background color
                        nu.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }
                }
            }
            else
            {
                panelHeader.Visible = false;
                meniHeader.Visible = true;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        private void ChangeTextBoxGroupBox()
        {
           

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
              

                foreach (Control control in groupBox1.Controls)
                {
                    if (control is System.Windows.Forms.Button buttons)
                    {

                        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                        buttons.FlatStyle = FlatStyle.Flat;
                    }
                }


                foreach (Control control in groupBox1.Controls)
                {

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
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv.ColumnHeadersHeight = 30;
        }
        public frmUvozKonacna()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            ChangeTextBox();
            ChangeTextBoxGroupBox();


            FillCheck();
            UcitajNHMoveCombo();
            FillCombo();
            this.cbBrod.GotFocus += (sender, args) => cbBrod.DroppedDown = true;
            txtTipKont.GotFocus += (sender, args) => txtTipKont.DroppedDown = true;
            cboRLTerminal.GotFocus += (sender, args) => cboRLTerminal.DroppedDown = true;
            cboRLTerminal2.GotFocus += (sender, args) => cboRLTerminal2.DroppedDown = true;
            cboRLTerminal3.GotFocus += (sender, args) => cboRLTerminal3.DroppedDown = true;
            txtADR.GotFocus += (sender, args) => txtADR.DroppedDown = true;
            cboBrodar.GotFocus += (sender, args) => cboBrodar.DroppedDown = true;
            cbVlasnikKont.GotFocus += (sender, args) => cbVlasnikKont.DroppedDown = true;
            cboNalogodavac1.GotFocus += (sender, args) => cboNalogodavac1.DroppedDown = true;
            cboNalogodavac2.GotFocus += (sender, args) => cboNalogodavac2.DroppedDown = true;
            cboNalogodavac3.GotFocus += (sender, args) => cboNalogodavac3.DroppedDown = true;
            cboUvoznik.GotFocus += (sender, args) => cboUvoznik.DroppedDown = true;
            txtVrstaPregleda.GotFocus += (sender, args) => txtVrstaPregleda.DroppedDown = true;
            cboSpedicijaG.GotFocus += (sender, args) => cboSpedicijaG.DroppedDown = true;
            cboSpedicijaRTC.GotFocus += (sender, args) => cboSpedicijaRTC.DroppedDown = true;
            cboCarinskiPostupak.GotFocus += (sender, args) => cboCarinskiPostupak.DroppedDown = true;
            cbPostupak.GotFocus += (sender, args) => cbPostupak.DroppedDown = true;
            cbNapomenaPoz.GotFocus += (sender, args) => cbNapomenaPoz.DroppedDown = true;
            cbNacinPakovanja.GotFocus += (sender, args) => cbNacinPakovanja.DroppedDown = true;
            cbOcarina.GotFocus += (sender, args) => cbOcarina.DroppedDown = true;
            cbOspedicija.GotFocus += (sender, args) => cbOspedicija.DroppedDown = true;
            txtMesto.GotFocus += (sender, args) => txtMesto.DroppedDown = true;
            txtAdresaMestaUtovara.GotFocus += (sender, args) => txtAdresaMestaUtovara.DroppedDown = true;
        }

        public frmUvozKonacna(int Sifra, string Korisnik)
        {
            InitializeComponent();
            //UcitajNHMoveCombo();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            ChangeTextBox();
            ChangeTextBoxGroupBox();
            FillCombo();

            FillZaglavlje(Sifra);
            txtNadredjeni.Text = Sifra.ToString();
            //FillCheck();

            // FillGV();
            KorisnikTekuci = Korisnik;
            this.cbBrod.GotFocus += (sender, args) => cbBrod.DroppedDown = true;
            txtTipKont.GotFocus += (sender, args) => txtTipKont.DroppedDown = true;
            cboRLTerminal.GotFocus += (sender, args) => cboRLTerminal.DroppedDown = true;
            cboRLTerminal2.GotFocus += (sender, args) => cboRLTerminal2.DroppedDown = true;
            cboRLTerminal3.GotFocus += (sender, args) => cboRLTerminal3.DroppedDown = true;
            txtADR.GotFocus += (sender, args) => txtADR.DroppedDown = true;
            cboBrodar.GotFocus += (sender, args) => cboBrodar.DroppedDown = true;
            cbVlasnikKont.GotFocus += (sender, args) => cbVlasnikKont.DroppedDown = true;
            cboNalogodavac1.GotFocus += (sender, args) => cboNalogodavac1.DroppedDown = true;
            cboNalogodavac2.GotFocus += (sender, args) => cboNalogodavac2.DroppedDown = true;
            cboNalogodavac3.GotFocus += (sender, args) => cboNalogodavac3.DroppedDown = true;
            cboUvoznik.GotFocus += (sender, args) => cboUvoznik.DroppedDown = true;
            txtVrstaPregleda.GotFocus += (sender, args) => txtVrstaPregleda.DroppedDown = true;
            cboSpedicijaG.GotFocus += (sender, args) => cboSpedicijaG.DroppedDown = true;
            cboSpedicijaRTC.GotFocus += (sender, args) => cboSpedicijaRTC.DroppedDown = true;
            cboCarinskiPostupak.GotFocus += (sender, args) => cboCarinskiPostupak.DroppedDown = true;
            cbPostupak.GotFocus += (sender, args) => cbPostupak.DroppedDown = true;
            cbNapomenaPoz.GotFocus += (sender, args) => cbNapomenaPoz.DroppedDown = true;
            cbNacinPakovanja.GotFocus += (sender, args) => cbNacinPakovanja.DroppedDown = true;
            cbOcarina.GotFocus += (sender, args) => cbOcarina.DroppedDown = true;
            cbOspedicija.GotFocus += (sender, args) => cbOspedicija.DroppedDown = true;
            txtMesto.GotFocus += (sender, args) => txtMesto.DroppedDown = true;
            txtAdresaMestaUtovara.GotFocus += (sender, args) => txtAdresaMestaUtovara.DroppedDown = true;
            // FillDG2(); jos nemam ID
            // FillDG3();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Uvoz uv = new Uvoz();
            uv.Show();
        }

        private void UcitajNHMoveCombo()

        {
            SqlConnection conn = new SqlConnection(connection);
            var nhm = "Select ID,(Rtrim(Naziv) + '-' + Rtrim(Broj)) as Naziv2, (Rtrim(Broj) + '-' + Rtrim(Naziv)) as Naziv1 from NHM where Interni = 1 order by Naziv ";
            var nhmSAD = new SqlDataAdapter(nhm, conn);
            nhmSDSA = new DataSet();
            nhmSAD.Fill(nhmSDSA);



            var nhm2 = "Select ID,(Rtrim(Broj) + '-' + Rtrim(NAziv)) as Naziv2, (Rtrim(Broj) + '-' + Rtrim(Naziv)) as Naziv1 from NHM where Interni = 1 order by Broj";
            var nhmSAD2 = new SqlDataAdapter(nhm2, conn);
            nhmSDS2A = new DataSet();
            nhmSAD2.Fill(nhmSDS2A);
        }

        private void FillZaglavlje(int Sifra)
        {

            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select ID, idVoza,Napomena, Terminal from UvozKonacnaZaglavlje where ID=" + Sifra, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtNadredjeni.Text = dr["ID"].ToString();
                cboVoz.SelectedValue = Convert.ToInt32(dr["idVoza"].ToString());
                txtNapomenaZaglavlje.Text = dr["Napomena"].ToString();
                if (dr["Terminal"].ToString() == "1")
                { 
                    chkTerminalski.Checked = true;
                }
            }
            con.Close();
            VratiVoz();
        }

        private void VratiVoz()
        {
            var select = "SELECT [ID] ,[BrVoza],[Relacija], " +
                " CONVERT(varchar,VremePolaska,104)      + ' '      + SUBSTRING(CONVERT(varchar,VremePolaska,108),1,5) as VremePolaska, " +
                " CONVERT(varchar,[VremeDolaska],104)      + ' '      + SUBSTRING(CONVERT(varchar,[VremeDolaska],108),1,5)  as VremeDolaska, " +
                " [MaksimalnaBruto],[MaksimalnaDuzina] " +
                " ,[MaksimalanBrojKola] " +
                " ,[Napomena]" +
                " ,[PostNaTerminalD] as Postavka,[KontrolniPregledD] as Kontrolni ,[VremePrimopredajeD] as Primopredaja,[VremeIstovaraD] as Istovar" +
                " ,[Datum] ,[Korisnik],[Dolazeci] " +
                " FROM [dbo].[Voz] where ID =  " + Convert.ToInt32(cboVoz.SelectedValue);
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView5.ReadOnly = true;
            dataGridView5.DataSource = ds.Tables[0];

            // dataGridView5.BorderStyle = BorderStyle.None;
            PodesiDatagridView(dataGridView5);



        }
        private void RefreshDataGridColor()
        {

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {


                if (row.Cells[25].Value.ToString() == "1")
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                    row.DefaultCellStyle.SelectionBackColor = Color.Green;
                }
                else
                {

                }
            }

            dataGridView1.Refresh();

        }

        private void FillGV()
        {
            var select = "    SELECT UvozKonacna.ID, BrojKontejnera, BrodskaTeretnica as BL,  DobijenNalogBrodara as Dobijen_Nalog_Brodara ,ATABroda, Brodovi.Naziv as Brod,Napomena1 as Napomena1, DobijeBZ as DatumBZ ,PIN, " +
 " [BrojKontejnera], TipKontenjera.Naziv as Vrsta_Kontejnera,  KontejnerskiTerminali.Naziv as R_L_SRB, pp1.Naziv as Dirigacija_Kontejnera_Za,  " +
 "   BrodskaTeretnica, VrstaRobeADR.Naziv as ADR, b.PaNaziv as Brodar, pv.PaNaziv as VlasnikKontejnera, n1.PaNaziv as Nalogodavac1, Ref1 as Ref1, n2.PaNaziv as Nalogodavac2, Ref2 as Ref2, n3.PaNaziv as Nalogodavac3, Ref3 as Ref3, " +
  "      p1.PaNaziv as Uvoznik,   " +
  "  (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaManipulacije.Naziv as nvarchar(20))   FROM UvozKonacnaVrstaManipulacije " +
   "       inner join VrstaManipulacije on VrstaManipulacije.ID = UvozKonacnaVrstaManipulacije.IDVrstaManipulacije   where UvozKonacnaVrstaManipulacije.IDNadredjena = UvozKonacna.ID " +
   "        FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as VrsteUsluga,   " +
   "     (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaRobeHS.HSKod as nvarchar(20))   FROM UvozKonacnaVrstaRobeHS " +
   "       inner join VrstaRobeHS on UvozKonacnaVrstaRobeHS.IDVrstaRobeHS = VrstaRobeHS.ID   where UvozKonacnaVrstaRobeHS.IDNadredjena = UvozKonacna.ID " +
   "        FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as HS,   " +
   "    (SELECT  STUFF((SELECT distinct    '/' + Cast(NHM.Broj as nvarchar(20)) " +
  "            FROM UvozKonacnaNHM  inner join NHM on UvozKonacnaNHM.IDNHM = NHM.ID  where UvozKonacnaNHM.IDNadredjena = UvozKOnacna.ID   FOR XML PATH('')), 1, 1, ''  ) As Skupljen) as NHM,   " +
   "              VrstaPregleda as InsTret,p2.PaNaziv as SpedicijaRTC,  p3.PaNaziv as SpedicijaGranica,      " +
   " VrstaCarinskogPostupka.Naziv as CarinskiPostupak,   " +
   "                  VrstePostupakaUvoz.Naziv as PostupakSaRobom,uvNacinPakovanja.Naziv as NacinPakovanja, Napomena as Napomena2,  " +
   "                       Carinarnice.Naziv as Carinarnica,  " +
   "                               p4.PaNaziv as OdredisnaSpedicija, MestaUtovara.Naziv as MestoIstovara," +
   "  (RTRIM(pkoMU.PaKOIme) + ' ' + RTRIM(pkoMU.PaKOPriimek)) as KontaktOsoba, Email,         BrojPlombe1, BrojPlombe2,   " +
" ( select STUFF((SELECT distinct    '/' + Cast(PredefinisanePoruke.Naziv as nvarchar(20)) from UvozKonacnaNapomenePozicioniranja " +
" inner join  PredefinisanePoruke on PredefinisanePoruke.ID = UvozKonacnaNapomenePozicioniranja.IDNapomene where UvozKonacnaNapomenePozicioniranja.IdNadredjena = UvozKonacna.ID " +
" FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as NapomenaZaPozicioniranje, " +
 " NetoRobe, BrutoRobe, TaraKontejnera, BrutoKontejnera, " +
 " Koleta" +
 " FROM UvozKonacna Left join Partnerji on PaSifra = VlasnikKontejnera " +
 " Left join Partnerji p1 on p1.PaSifra = Uvoznik " +
 " Left join Partnerji p2 on p2.PaSifra = SpedicijaRTC " +
" Left join Partnerji p3 on p3.PaSifra = SpedicijaGranica " +
 "  Left join VrstaRobeHS on VrstaRobeHS.ID = UvozKonacna.NazivRobe " +
"  Left join NHM on NHM.ID = NHMBroj " +
 " Left join TipKontenjera on TipKontenjera.ID = UvozKonacna.TipKontejnera " +
 "  Left join Carinarnice on Carinarnice.ID = UvozKonacna.OdredisnaCarina " +
 "  Left join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = UvozKonacna.CarinskiPostupak " +
 "  Left join KontejnerskiTerminali on KontejnerskiTerminali.ID = UvozKonacna.RLTErminali " +
 "  Left join Partnerji n1 on n1.PaSifra = Nalogodavac1 " +
 "  Left join Partnerji n2 on n2.PaSifra = Nalogodavac2 " +
 "  Left join Partnerji n3 on n3.PaSifra = Nalogodavac3 " +
 "  Left join Partnerji b on b.PaSifra = UvozKonacna.Brodar " +
  " Left join DirigacijaKontejneraZa pp1 on pp1.ID = UvozKonacna.DirigacijaKontejeraZa   " +
 "  Left join Brodovi on Brodovi.ID = UvozKonacna.NazivBroda " +
                              "   Left join VrstaRobeADR on VrstaRobeADR.ID = ADR " +
                              "    Left join VrstePostupakaUvoz on VrstePostupakaUvoz.ID = PostupakSaRobom    " +
                              " Left join MestaUtovara on UvozKOnacna.MestoIstovara = MestaUtovara.ID " +
" Left join partnerjiKontOsebaMU on UvozKonacna.KontaktOsoba = partnerjiKontOsebaMU.PaKOSifra " +
                              "Left join uvNacinPakovanja " +
 " on uvNacinPakovanja.ID = NacinPakovanja  Left join Partnerji p4 on p4.PaSifra = OdredisnaSpedicija " +
 " inner join Partnerji pv on pv.PaSifra = UvozKonacna.VlasnikKontejnera " +
" inner join partnerjiKontOsebaMU pkoMU on pkoMU.PaKOZapSt = UvozKonacna.KontaktOsoba " +
 " where UvozKonacna.IdNadredjeni = " + Convert.ToInt32(txtNadredjeni.Text) + "  order by UvozKonacna.ID desc ";







            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView1);

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Frozen = true;
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "BrojKontejnera";
            dataGridView1.Columns[1].Frozen = true;
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

            RefreshDataGridColor();

        }
        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connection);

            var dir = "Select ID,Naziv from DirigacijaKontejneraZa order by ID";
            var dirAD = new SqlDataAdapter(dir, conn);
            var dirDS = new DataSet();
            dirAD.Fill(dirDS);
            cbDirigacija.DataSource = dirDS.Tables[0];
            cbDirigacija.DisplayMember = "Naziv";
            cbDirigacija.ValueMember = "ID";
            //carinski postupak

            var dir2 = "Select ID, (Oznaka + ' ' + Naziv) as Naziv from VrstaCarinskogPostupka order by Oznaka";
            var dirAD2 = new SqlDataAdapter(dir2, conn);
            var dirDS2 = new DataSet();
            dirAD2.Fill(dirDS2);
            cboCarinskiPostupak.DataSource = dirDS2.Tables[0];
            cboCarinskiPostupak.DisplayMember = "Naziv";
            cboCarinskiPostupak.ValueMember = "ID";
            //postupak roba/kont

            var dir3 = "Select ID,Naziv from VrstePostupakaUvoz order by ID";
            var dirAD3 = new SqlDataAdapter(dir3, conn);
            var dirDS3 = new DataSet();
            dirAD3.Fill(dirDS3);
            cbPostupak.DataSource = dirDS3.Tables[0];
            cbPostupak.DisplayMember = "Naziv";
            cbPostupak.ValueMember = "ID";
            //nacin pakovanja

            var dir4 = "Select ID,(Oznaka + ' ' + Naziv) as Naziv from uvNacinPakovanja order by ID";
            var dirAD4 = new SqlDataAdapter(dir4, conn);
            var dirDS4 = new DataSet();
            dirAD4.Fill(dirDS4);
            cbNacinPakovanja.DataSource = dirDS4.Tables[0];
            cbNacinPakovanja.DisplayMember = "Naziv";
            cbNacinPakovanja.ValueMember = "ID";
            //napomena pozicioniranje

            var dir5 = "Select ID,Naziv from NapomenaZaPozicioniranje order by ID";
            var dirAD5 = new SqlDataAdapter(dir5, conn);
            var dirDS5 = new DataSet();
            dirAD5.Fill(dirDS5);
            cbNapomenaPoz.DataSource = dirDS5.Tables[0];
            cbNapomenaPoz.DisplayMember = "Naziv";
            cbNapomenaPoz.ValueMember = "ID";

            var brod = "Select ID,Naziv From Brodovi order by ID";
            var brodAD = new SqlDataAdapter(brod, conn);
            var brodDS = new DataSet();
            brodAD.Fill(brodDS);
            cbBrod.DataSource = brodDS.Tables[0];
            cbBrod.DisplayMember = "Naziv";
            cbBrod.ValueMember = "ID";

            var car = "Select ID,Naziv From Carinarnice order by ID";
            var carAD = new SqlDataAdapter(car, conn);
            var carDS = new DataSet();
            carAD.Fill(carDS);
            cbOcarina.DataSource = carDS.Tables[0];
            cbOcarina.DisplayMember = "Naziv";
            cbOcarina.ValueMember = "ID";

            var partner = "Select PaSifra,PaNaziv From Partnerji  order by PaSifra";
            var partAD = new SqlDataAdapter(partner, conn);
            var partDS = new DataSet();
            partAD.Fill(partDS);
            cbVlasnikKont.DataSource = partDS.Tables[0];
            cbVlasnikKont.DisplayMember = "PaNaziv";
            cbVlasnikKont.ValueMember = "PaSifra";
            //uvoznik
            var partner2 = "Select PaSifra,PaNaziv From Partnerji  order by PaSifra";
            var partAD2 = new SqlDataAdapter(partner2, conn);
            var partDS2 = new DataSet();
            partAD2.Fill(partDS2);
            cboUvoznik.DataSource = partDS2.Tables[0];
            cboUvoznik.DisplayMember = "PaNaziv";
            cboUvoznik.ValueMember = "PaSifra";
            //spedicija na granici

            var partner3 = "Select PaSifra,PaNaziv From Partnerji  order by PaSifra";
            var partAD3 = new SqlDataAdapter(partner3, conn);
            var partDS3 = new DataSet();
            partAD3.Fill(partDS3);
            cboSpedicijaG.DataSource = partDS3.Tables[0];
            cboSpedicijaG.DisplayMember = "PaNaziv";
            cboSpedicijaG.ValueMember = "PaSifra";
            //spedicija rtc luka leget

            var partner4 = "Select PaSifra,PaNaziv From Partnerji  order by PaSifra";
            var partAD4 = new SqlDataAdapter(partner4, conn);
            var partDS4 = new DataSet();
            partAD4.Fill(partDS4);
            cboSpedicijaRTC.DataSource = partDS4.Tables[0];
            cboSpedicijaRTC.DisplayMember = "PaNaziv";
            cboSpedicijaRTC.ValueMember = "PaSifra";
            //odredisna spedicija
            var partner5 = "Select PaSifra,PaNaziv From Partnerji  order by PaSifra";
            var partAD5 = new SqlDataAdapter(partner5, conn);
            var partDS5 = new DataSet();
            partAD5.Fill(partDS5);
            cbOspedicija.DataSource = partDS5.Tables[0];
            cbOspedicija.DisplayMember = "PaNaziv";
            cbOspedicija.ValueMember = "PaSifra";
            //Panta
            var nhm = "";
            if (chkInterni.Checked == true)
            {
                nhm = "Select ID,(RTRIM(Naziv) + '-' + Rtrim(Broj)) as Naziv from NHM where Interni = 1 order by Naziv ";
            }
            else
            {
                nhm = "Select ID,(RTRIM(Naziv) + '-' + Rtrim(Broj)) as Naziv from NHM order by Naziv";
            }

            var nhmSAD = new SqlDataAdapter(nhm, conn);
            var nhmSDS = new DataSet();
            nhmSAD.Fill(nhmSDS);
            cboNHM.DataSource = nhmSDS.Tables[0];
            cboNHM.DisplayMember = "Naziv";
            cboNHM.ValueMember = "ID";
            //Panta

            var voz = "select ID, (Cast(ID as NVarChar(10)) + '-'+Cast(BrVoza as NVarchar(15)) + '-' + Relacija + '-' + Cast(VremePolaska as nvarchar(20))) as Naziv   from Voz ";
            var vozSAD = new SqlDataAdapter(voz, conn);
            var vozSDS = new DataSet();
            vozSAD.Fill(vozSDS);
            cboVoz.DataSource = vozSDS.Tables[0];
            cboVoz.DisplayMember = "Naziv";
            cboVoz.ValueMember = "ID";

            var adr = "Select ID, (UNKod + ' ' + Naziv) as Naziv From VrstaRobeADR order by ID";
            var adrSAD = new SqlDataAdapter(adr, conn);
            var adrSDS = new DataSet();
            adrSAD.Fill(adrSDS);
            txtADR.DataSource = adrSDS.Tables[0];
            txtADR.DisplayMember = "Naziv";
            txtADR.ValueMember = "ID";

            var tipkontejnera = "Select ID, SkNaziv From TipKontenjera order by id";
            var tkAD = new SqlDataAdapter(tipkontejnera, conn);
            var tkDS = new DataSet();
            tkAD.Fill(tkDS);
            txtTipKont.DataSource = tkDS.Tables[0];
            txtTipKont.DisplayMember = "SkNaziv";
            txtTipKont.ValueMember = "ID";

            //Kontejnerski terminali
            var rl = "Select ID, (Oznaka + ' ' + Naziv) as Naziv From KontejnerskiTerminali order by ID";
            var rlSAD = new SqlDataAdapter(rl, conn);
            var rlSDS = new DataSet();
            rlSAD.Fill(rlSDS);
            cboRLTerminal.DataSource = rlSDS.Tables[0];
            cboRLTerminal.DisplayMember = "Naziv";
            cboRLTerminal.ValueMember = "ID";

            var rl2 = "Select ID, (Oznaka + ' ' + Naziv) as Naziv From KontejnerskiTerminali order by ID";
            var rlSAD2 = new SqlDataAdapter(rl2, conn);
            var rlSDS2 = new DataSet();
            rlSAD2.Fill(rlSDS2);
            cboRLTerminal2.DataSource = rlSDS2.Tables[0];
            cboRLTerminal2.DisplayMember = "Naziv";
            cboRLTerminal2.ValueMember = "ID";
           
            var rl3 = "Select ID, (Oznaka + ' ' + Naziv) as Naziv From KontejnerskiTerminali order by ID";
            var rlSAD3 = new SqlDataAdapter(rl3, conn);
            var rlSDS3 = new DataSet();
            rlSAD3.Fill(rlSDS3);
            cboRLTerminal3.DataSource = rlSDS3.Tables[0];
            cboRLTerminal3.DisplayMember = "Naziv";
            cboRLTerminal3.ValueMember = "ID";

            var partner7 = "Select PaSifra,PaNaziv From Partnerji  order by PaSifra";
            var partAD7 = new SqlDataAdapter(partner7, conn);
            var partDS7 = new DataSet();
            partAD7.Fill(partDS7);
            cboBrodar.DataSource = partDS7.Tables[0];
            cboBrodar.DisplayMember = "PaNaziv";
            cboBrodar.ValueMember = "PaSifra";

            var partner8 = "Select PaSifra,PaNaziv From Partnerji  order by PaSifra";
            var partAD8 = new SqlDataAdapter(partner8, conn);
            var partDS8 = new DataSet();
            partAD8.Fill(partDS8);
            cboNalogodavac1.DataSource = partDS8.Tables[0];
            cboNalogodavac1.DisplayMember = "PaNaziv";
            cboNalogodavac1.ValueMember = "PaSifra";

            var partner9 = "Select PaSifra,PaNaziv From Partnerji  order by PaSifra";
            var partAD9 = new SqlDataAdapter(partner9, conn);
            var partDS9 = new DataSet();
            partAD9.Fill(partDS9);
            cboNalogodavac2.DataSource = partDS9.Tables[0];
            cboNalogodavac2.DisplayMember = "PaNaziv";
            cboNalogodavac2.ValueMember = "PaSifra";

            var partner10 = "Select PaSifra,PaNaziv From Partnerji  order by PaSifra";
            var partAD10 = new SqlDataAdapter(partner10, conn);
            var partDS10 = new DataSet();
            partAD9.Fill(partDS10);
            cboNalogodavac3.DataSource = partDS10.Tables[0];
            cboNalogodavac3.DisplayMember = "PaNaziv";
            cboNalogodavac3.ValueMember = "PaSifra";


            var bro = "Select PaSifra,PaNaziv From Partnerji  order by PaNaziv";
            var broAD = new SqlDataAdapter(bro, conn);
            var broDS = new DataSet();
            broAD.Fill(broDS);


            cboBrodar.DataSource = broDS.Tables[0];
            cboBrodar.DisplayMember = "PaNaziv";
            cboBrodar.ValueMember = "PaSifra";


            var it = "select ID, Naziv from InspekciskiTretman order by Naziv";
            var itAD = new SqlDataAdapter(it, conn);
            var itDS = new DataSet();
            itAD.Fill(itDS);
            txtVrstaPregleda.DataSource = itDS.Tables[0];
            txtVrstaPregleda.DisplayMember = "Naziv";
            txtVrstaPregleda.ValueMember = "ID";


            var mu = "select ID, Naziv from MestaUtovara order by Naziv";
            var muAD = new SqlDataAdapter(mu, conn);
            var muDS = new DataSet();
            muAD.Fill(muDS);
            txtMesto.DataSource = muDS.Tables[0];
            txtMesto.DisplayMember = "Naziv";
            txtMesto.ValueMember = "ID";

            var ko = "select PaKoZapSt, (Rtrim(PaKOIme) + ' ' + Rtrim(PaKoPriimek)) as Naziv from partnerjiKontOsebaMU order by PaKOIme";
            var koAD = new SqlDataAdapter(ko, conn);
            var koDS = new DataSet();
            koAD.Fill(koDS);
            txtKontaktOsoba.DataSource = koDS.Tables[0];
            txtKontaktOsoba.DisplayMember = "Naziv";
            txtKontaktOsoba.ValueMember = "PaKoZapSt";

            //FillCombo
            var partner22 = "SELECT ID, Min(Naziv) as Naziv FROM Scenario group by ID order by ID";
            var partAD22 = new SqlDataAdapter(partner22, conn);
            var partDS22 = new DataSet();
            partAD22.Fill(partDS22);
            cboScenario.DataSource = partDS22.Tables[0];
            cboScenario.DisplayMember = "Naziv";
            cboScenario.ValueMember = "ID";

        }
        private void FillCheck()
        {
            var query = "Select PaSifra,PaNaziv From Partnerji";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            clNalogodavac.DataSource = ds.Tables[0];
            clNalogodavac.DisplayMember = "PaNaziv";
            clNalogodavac.ValueMember = "PaSifra";

            var select = "Select Naziv,TipManipulacije from VrstaManipulacije";
            var da2 = new SqlDataAdapter(select, conn);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            clVrstaUsluga.DataSource = ds2.Tables[0];
            clVrstaUsluga.DisplayMember = "Naziv";
            clVrstaUsluga.ValueMember = "Naziv";


        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertUvozKonacna uv = new InsertUvozKonacna();
            uv.DelUvozKonacna(Convert.ToInt32(txtID.Text));
            txtID.Text = "0";
        }

        private void VratiPodatkeSelect(int ID)
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT [ID] ,[EtaBroda] " +
     " ,[AtaBroda] ,[StatusPrijema] ,[BrojKontejnera] " +
     "  ,[DobijenNalogBrodara]  ,[DobijeBZ]   ,[Napomena]  ,[PIN] " +
     "  ,[DirigacijaKontejeraZa]  ,[NazivBroda]   ,[BrodskaTeretnica] ,[ADR] " +
     "  ,[VlasnikKontejnera]  ,[BukingBrodara]  ,[Nalogodavac]  ,[VrstaUsluge] " +
     "  ,[Uvoznik]  ,[NHMBroj] ,[NazivRobe] ,[SpedicijaGranica] " +
     "  ,[SpedicijaRTC]  ,[CarinskiPostupak] ,[PostupakSaRobom] ,[NacinPakovanja] " +
     "  ,[OdredisnaCarina] ,[OdredisnaSpedicija]  ,[MestoIstovara]  ,[KontaktOsoba] " +
     "  ,[Email]  ,[BrojPlombe1]   ,[BrojPlombe2]  ,[NetoRobe] " +
     "  ,[BrutoRobe] ,[TaraKontejnera]   ,[BrutoKontejnera],[NapomenaZaPozicioniranje] " +
     "  ,[AtaOtpreme]  ,[BrojVoza] ,[RelacijaVoza]  ,[AtaDolazak] " +
     "  ,[TipKontejnera] ,[Koleta], RLTerminali, BrojKola, Napomena1 ,VrstaPregleda ,Nalogodavac1 ,Ref1 ,Nalogodavac2 ,Ref2 ,Nalogodavac3 ,Ref3, Brodar, NaslovStatusaVozila, Prioritet, DobijenBZ, AdresaMestaUtovara, KontaktOsobe , TaraKontejneraT, KoletaTer, Scenario,RLTerminali2,RLTerminali3, PotvrdioKlijent, UradilaCarina" +
     "  ,[chkDobijenNalogBrodara]     ,[chkDobijenNalogodavac1]      ,[DatumNalogodavac1]     ,[chkDobijenNalogodavac2]     ,[DatumNalogodavac2]     ,[chkDobijenNalogodavac3]  " +
     "   ,[DatumNalogodavac3]      ,[DatumPotvrdioKlijent]     ,[DatumSlobasodanDaNapusti]     ,[FCL]     ,[LCL]" +
 "  FROM [UvozKonacna] where ID= " + ID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                dtEtaRijeka.Value = Convert.ToDateTime(dr["EtaBroda"].ToString());
                dtAtaRijeka.Value = Convert.ToDateTime(dr["AtaBroda"].ToString());
                txtStatus.Text = dr["StatusPrijema"].ToString();
                txtBrKont.Text = dr["BrojKontejnera"].ToString();
                txtTipKont.SelectedValue = Convert.ToInt32(dr["TipKontejnera"].ToString());
                dtNalogBrodara.Value = Convert.ToDateTime(dr["DobijenNalogBrodara"].ToString());
                txtBZ.Text = dr["DobijeBZ"].ToString();
                txtNapomena.Text = dr["Napomena"].ToString();
                txtPIN.Text = dr["PIN"].ToString();
                cbDirigacija.SelectedValue = Convert.ToInt32(dr["DirigacijaKontejeraZa"].ToString());
                cbBrod.SelectedValue = Convert.ToInt32(dr["NazivBroda"].ToString());
                txtTeretnica.Text = dr["BrodskaTeretnica"].ToString();
                txtADR.SelectedValue = Convert.ToInt32(dr["ADR"].ToString());
                cbVlasnikKont.SelectedValue = Convert.ToInt32(dr["VlasnikKontejnera"].ToString());
                txtBuking.Text = dr["BukingBrodara"].ToString();
                cboUvoznik.SelectedValue = Convert.ToInt32(dr["Uvoznik"].ToString());
                cboNHM.SelectedValue = 1;
                cboSpedicijaG.SelectedValue = Convert.ToInt32(dr["SpedicijaGranica"].ToString());
                cboSpedicijaRTC.SelectedValue = Convert.ToInt32(dr["SpedicijaRTC"].ToString());
                cboCarinskiPostupak.SelectedValue = Convert.ToInt32(dr["CarinskiPostupak"].ToString());
                cbPostupak.SelectedValue = Convert.ToInt32(dr["PostupakSaRobom"].ToString());
                cbNacinPakovanja.SelectedValue = Convert.ToInt32(dr["NacinPakovanja"].ToString());
                cbOcarina.SelectedValue = Convert.ToInt32(dr["OdredisnaCarina"].ToString());
                cbOspedicija.SelectedValue = Convert.ToInt32(dr["OdredisnaSpedicija"].ToString());
                txtMesto.SelectedValue = Convert.ToInt32(dr["MestoIstovara"].ToString());
                txtKontaktOsoba.SelectedValue = Convert.ToInt32(dr["KontaktOsoba"].ToString());
                txtMail.Text = dr["Email"].ToString();
                txtPlomba1.Text = dr["BrojPlombe1"].ToString();
                txtPlomba2.Text = dr["BrojPlombe2"].ToString();
                txtNetoR.Value = Convert.ToDecimal(dr["NetoRobe"].ToString());
                txtBrutoR.Value = Convert.ToDecimal(dr["BrutoRobe"].ToString());
                txtTaraK.Value = Convert.ToDecimal(dr["TaraKontejnera"].ToString());
                txtBrutoK.Value = Convert.ToDecimal(dr["BrutoKontejnera"].ToString());
                cbNapomenaPoz.SelectedValue = Convert.ToInt32(dr["NapomenaZaPozicioniranje"].ToString());
                dtAtaOtprema.Value = Convert.ToDateTime(dr["AtaOtpreme"].ToString());
                txtBrojVoza.Text = dr["BrojVoza"].ToString();
                txtRelacija.Text = dr["RelacijaVoza"].ToString();
                dtAtaDolazak.Value = Convert.ToDateTime(dr["AtaDolazak"].ToString());
                txtKoleta.Value = Convert.ToDecimal(dr["Koleta"].ToString());
                txtKoletaTer.Value = Convert.ToDecimal(dr["KoletaTer"].ToString());
                cboRLTerminal.SelectedValue = Convert.ToInt32(dr["RLTerminali"].ToString());
                cboRLTerminal2.SelectedValue = Convert.ToInt32(dr["RLTerminali2"].ToString());
                cboRLTerminal3.SelectedValue = Convert.ToInt32(dr["RLTerminali3"].ToString());
                txtBrojKola2.Text = dr["BrojKola"].ToString();
                if (dr["Napomena1"].ToString() == "1")
                {
                    chkSlobodan.Checked = true;
                }
                else
                {
                    chkSlobodan.Checked = false;
                }
                txtNapomena1.Text = dr["Napomena1"].ToString();
                txtVrstaPregleda.SelectedValue = Convert.ToInt32(dr["VrstaPregleda"].ToString());
                cboNalogodavac1.SelectedValue = Convert.ToInt32(dr["Nalogodavac1"].ToString());
                txtRef1.Text = dr["Ref1"].ToString();
                cboNalogodavac2.SelectedValue = Convert.ToInt32(dr["Nalogodavac2"].ToString());
                txtRef2.Text = dr["Ref2"].ToString();
                cboNalogodavac3.SelectedValue = Convert.ToInt32(dr["Nalogodavac3"].ToString());
                txtRef3.Text = dr["Ref3"].ToString();

                cboBrodar.SelectedValue = Convert.ToInt32(dr["Brodar"].ToString());
                cboNaslovStatusaVozila.Text = dr["NaslovStatusaVozila"].ToString();
                txtTaraTerminal.Value = Convert.ToDecimal(dr["TaraKontejneraT"].ToString());

                if (dr["DobijenBZ"].ToString() == "1")
                {
                    chkDobijenBZ.Checked = true;
                    txtBZ.Visible = true;
                }
                else
                {
                    chkDobijenBZ.Checked = false;
                    txtBZ.Visible = false;
                }

                if (dr["Prioritet"].ToString() == "1")
                {
                    chkPrioritet.Checked = true;

                }
                else
                {
                    chkPrioritet.Checked = true;
                }

                txtAdresaMestaUtovara.SelectedValue = Convert.ToInt32(dr["AdresaMestaUtovara"].ToString());
                txtKontaktOsobe.Text = dr["KontaktOsobe"].ToString();

                cboScenario.SelectedValue = Convert.ToInt32(dr["Scenario"].ToString());

                if (dr["PotvrdioKlijent"].ToString() == "0")
                {
                    chkCekaSeKlijent.Checked = true;
                    chkPotvrdioKlijent2BDI.Checked = false;
                    chkPotvrdioKlijent.Checked = false;

                }
                if (dr["PotvrdioKlijent"].ToString() == "1")
                {
                    chkCekaSeKlijent.Checked = false;
                    chkPotvrdioKlijent2BDI.Checked = true;
                    chkPotvrdioKlijent.Checked = false;

                }

                if (dr["PotvrdioKlijent"].ToString() == "2")
                {
                    chkCekaSeKlijent.Checked = false;
                    chkPotvrdioKlijent2BDI.Checked = false;
                    chkPotvrdioKlijent.Checked = true;

                }

                if (dr["UradilaCarina"].ToString() == "0")
                {
                    chkCekaSeCarina.Checked = true;
                    chkUradilaCarina.Checked = false;

                }
                if (dr["UradilaCarina"].ToString() == "1")
                {
                    chkCekaSeCarina.Checked = false;
                    chkUradilaCarina.Checked = true;

                }

                if (dr["chkDobijenNalogBrodara"].ToString() == "1")
                {
                    chkDobijenNalogBrodara.Checked = true;
                }

                if (dr["chkDobijenNalogodavac1"].ToString() == "1")
                {
                    chkDobijenNalogodavac1.Checked = true;
                }
                if (dr["chkDobijenNalogodavac2"].ToString() == "1")
                {
                    chkDobijenNalogodavac2.Checked = true;
                }
                if (dr["chkDobijenNalogodavac3"].ToString() == "1")
                {
                    chkDobijenNalogodavac2.Checked = true;
                }
                if (dr["FCL"].ToString() == "1")
                {
                    chkFCL.Checked = true;
                }

                if (dr["LCL"].ToString() == "1")
                {
                    chkLCL.Checked = true;
                }
                dtpDobijenNalogodavac1.Value = Convert.ToDateTime(dr["DatumNalogodavac1"].ToString());
                dtpDobijenNalogodavac2.Value = Convert.ToDateTime(dr["DatumNalogodavac2"].ToString());
                dtpDobijenNalogodavac3.Value = Convert.ToDateTime(dr["DatumNalogodavac3"].ToString());
                dtpPotvrdioKlijent.Value = Convert.ToDateTime(dr["DatumPotvrdioKlijent"].ToString());
                dtpSlobodanDaNapusti.Value = Convert.ToDateTime(dr["DatumSlobasodanDaNapusti"].ToString());


                /*
                string pomNal = dr["Nalogodavac"].ToString();
                string[] nal = pomNal.Split(',');
                foreach (var word in nal)
                {
                    for (int i = 0; i < nal.Length; i++)
                    {

                        //if (clNalogodavac.GetSelected(i))
                        //{
                        clNalogodavac.SetItemChecked(i, true);
                        //}

                    }
                }
                string pomRob = dr["VrstaUsluge"].ToString();
                string[] rob = pomRob.Split(',');
                foreach (var r in rob)
                {
                    for (int i = 0; i < rob.Length; i++)
                    {
                        clVrstaUsluga.SetItemChecked(i, true);
                    }
                }
                */


            }



            con.Close();


        }

        private void FillDGUsluge()
        {
            if (txtID.Text == "")
            {
                return;

            }
            var select = "";

            select = " select  UvozKonacnaVrstaManipulacije.ID as ID, UvozKonacnaVrstaManipulacije.IDNadredjena as KontejnerID, UvozKonacna.BrojKontejnera, " +
" UvozKonacnaVrstaManipulacije.Kolicina,  VrstaManipulacije.ID as ManipulacijaID,VrstaManipulacije.Naziv as ManipulacijaNaziv, " +
" UvozKonacnaVrstaManipulacije.Cena,OrganizacioneJedinice.ID,   OrganizacioneJedinice.Naziv as OrganizacionaJedinica,  "+ 
" Partnerji.PaSifra as NalogodavacID,PArtnerji.PaNaziv as Platilac, SaPDV,UvozKonacnaVrstaManipulacije.Pokret, KontejnerStatus.Naziv,  'UVOZNA'" +
" from UvozKonacnaVrstaManipulacije" +
" Inner    join VrstaManipulacije on VrstaManipulacije.ID = UvozKonacnaVrstaManipulacije.IDVrstaManipulacije" +
" inner" +
" join PArtnerji on UvozKonacnaVrstaManipulacije.Platilac = PArtnerji.PaSifra" +
" inner" +
" join OrganizacioneJedinice on OrganizacioneJedinice.ID = UvozKonacnaVrstaManipulacije.OrgJed" +
" inner" +
" join UvozKonacna on UvozKonacnaVrstaManipulacije.IDNadredjena = UvozKonacna.ID" +
" inner " +
" join KontejnerStatus on KontejnerStatus.ID = StatusKontejnera where UvozKonacna.ID  = " + Convert.ToInt32(txtID.Text) + " order by UvozKonacnaVrstaManipulacije.ID asc";



            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView8.ReadOnly = true;
            dataGridView8.DataSource = ds.Tables[0];


            //  dataGridView8.BorderStyle = BorderStyle.None;
            PodesiDatagridView(dataGridView8);

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView8.Columns[0];
            dataGridView8.Columns[0].HeaderText = "ID";
            dataGridView8.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView8.Columns[1];
            dataGridView8.Columns[1].HeaderText = "KID";
            dataGridView8.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView8.Columns[2];
            dataGridView8.Columns[2].HeaderText = "Kontejner";
            dataGridView8.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView8.Columns[3];
            dataGridView8.Columns[3].HeaderText = "KOL";
            dataGridView8.Columns[3].Visible = false;
            dataGridView8.Columns[3].Width = 70;

            DataGridViewColumn column5 = dataGridView8.Columns[4];
            dataGridView8.Columns[4].HeaderText = "USID";
            // dataGridView8.Columns[4].Visible = false;
            dataGridView8.Columns[4].Width = 30;


            DataGridViewColumn column6 = dataGridView8.Columns[5];
            dataGridView8.Columns[5].HeaderText = "Usluga";
            dataGridView8.Columns[5].Width = 300;


            DataGridViewColumn column7 = dataGridView8.Columns[6];
            dataGridView8.Columns[6].HeaderText = "Cena";
            dataGridView8.Columns[6].Visible = false;
            dataGridView8.Columns[6].Width = 50;


            DataGridViewColumn column8 = dataGridView8.Columns[7];
            dataGridView8.Columns[7].HeaderText = "OJ";
            dataGridView8.Columns[7].Visible = false;
            dataGridView8.Columns[7].Width = 30;


            DataGridViewColumn column9 = dataGridView8.Columns[8];
            dataGridView8.Columns[8].HeaderText = "OJN";
            dataGridView8.Columns[8].Visible = false;
            dataGridView8.Columns[8].Width = 60;

            DataGridViewColumn column10 = dataGridView8.Columns[9];
            dataGridView8.Columns[9].HeaderText = "PID";
            dataGridView8.Columns[9].Visible = false;
            dataGridView8.Columns[9].Width = 130;

            DataGridViewColumn column11 = dataGridView8.Columns[10];
            dataGridView8.Columns[10].HeaderText = "PLATILAC";
            // dataGridView8.Columns[9].Visible = false;
            dataGridView8.Columns[10].Width = 230;

            DataGridViewColumn column12 = dataGridView8.Columns[11];
            dataGridView8.Columns[11].HeaderText = "SAPDV";
            // dataGridView8.Columns[9].Visible = false;
            dataGridView8.Columns[11].Width = 60;

            DataGridViewColumn column13 = dataGridView8.Columns[12];
            dataGridView8.Columns[12].HeaderText = "POKRET";
            // dataGridView8.Columns[9].Visible = false;
            dataGridView8.Columns[12].Width = 160;

            DataGridViewColumn column14 = dataGridView8.Columns[13];
            dataGridView8.Columns[12].HeaderText = "STATUS";
            // dataGridView8.Columns[9].Visible = false;
            dataGridView8.Columns[12].Width = 160;

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    txtID.Text = row.Cells[0].Value.ToString();
                    VratiPodatkeSelect(Convert.ToInt32(txtID.Text));
                    FillDG2();
                    FillDG3();
                    FillDG4();
                    // FillDG8();
                    FillDGUsluge();
                    FillDG2Konacna();
                }
            }
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            int tDobijenBZ = 0;
            int tPrioritet = 0;
            int PotvrdioKlijent = 0;
            int UradilaCarina = 0;


            if (chkDobijenBZ.Checked == true)
            { tDobijenBZ = 1; };
            if (chkPrioritet.Checked == true)
            { tPrioritet = 1; };

            /*
            for (int i = 0; i < clNalogodavac.Items.Count; i++)
            {
                if (clNalogodavac.GetItemCheckState(i) == CheckState.Checked)
                {
                    if (i == 0)
                    {
                        clNalogodavac.SetSelected(i, true);
                        nalogodavci = clNalogodavac.SelectedValue.ToString();
                    }
                    else
                    {
                        clNalogodavac.SetSelected(i, true);
                        nalogodavci = nalogodavci + "," + clNalogodavac.SelectedValue.ToString();
                    }
                }
            }
            for (int i = 0; i < clVrstaUsluga.Items.Count; i++)
            {
                if (clVrstaUsluga.GetItemCheckState(i) == CheckState.Checked)
                {
                    if (i == 0)
                    {
                        clVrstaUsluga.SetSelected(i, true);
                        usluge = clVrstaUsluga.SelectedValue.ToString();
                    }
                    else
                    {
                        clVrstaUsluga.SetSelected(i, true);
                        usluge = usluge + "," + clVrstaUsluga.SelectedValue.ToString();
                    }
                }
            }
            */

            if (chkSlobodan.Checked == true)
            {
                txtNapomena1.Text = "1";
            }
            else
            {
                txtNapomena1.Text = "0";
            }

            if (chkCekaSeKlijent.Checked == true)
            { PotvrdioKlijent = 0; };
            if (chkPotvrdioKlijent2BDI.Checked == true)
            { PotvrdioKlijent = 1; };

            if (chkPotvrdioKlijent.Checked == true)
            { PotvrdioKlijent = 2; };


            if (chkCekaSeCarina.Checked == true)
            { UradilaCarina = 0; };

            if (chkUradilaCarina.Checked == true)
            { UradilaCarina = 1; };

            int TFDobijenNalog = 0;
            if (chkDobijenNalogBrodara.Checked == true)
            { TFDobijenNalog = 1; };

            int TFDobijenNalogodavac1 = 0;
            if (chkDobijenNalogodavac1.Checked == true)
            { TFDobijenNalogodavac1 = 1; };


            int TFDobijenNalogodavac2 = 0;
            if (chkDobijenNalogodavac2.Checked == true)
            { TFDobijenNalogodavac2 = 1; };

            int TFDobijenNalogodavac3 = 0;
            if (chkDobijenNalogodavac3.Checked == true)
            { TFDobijenNalogodavac3 = 1; };


            int TFFCL = 0;
            if (chkFCL.Checked == true)
            { TFFCL = 1; };


            int TFLCL = 0;
            if (chkLCL.Checked == true)
            { TFLCL = 1; };

            int i = ProveriBiloIzmena(txtID.Text);


            InsertUvozKonacna ins = new InsertUvozKonacna();
            ins.UpdUvozKonacna(Convert.ToInt32(txtID.Text), Convert.ToInt32(txtNadredjeni.Text), Convert.ToDateTime(dtEtaRijeka.Value.ToString()),
                Convert.ToDateTime(dtAtaRijeka.Value.ToString()), txtStatus.Text.ToString().TrimEnd(), txtBrKont.Text,
                Convert.ToInt32(txtTipKont.SelectedValue), Convert.ToDateTime(dtNalogBrodara.Value.ToString()), txtBZ.Text.ToString().TrimEnd(),
                txtNapomena.Text.ToString().TrimEnd(), txtPIN.Text.ToString().TrimEnd(), Convert.ToInt32(cbDirigacija.SelectedValue), Convert.ToInt32(cbBrod.SelectedValue),
                txtTeretnica.Text, Convert.ToInt32(txtADR.SelectedValue), Convert.ToInt32(cbVlasnikKont.SelectedValue), Convert.ToInt32(txtBuking.Text), nalogodavci,
                usluge, Convert.ToInt32(cboUvoznik.SelectedValue), Convert.ToInt32(cboNHM.SelectedValue), Convert.ToInt32(cboNazivRobe.SelectedValue), Convert.ToInt32(cboSpedicijaG.SelectedValue),
                Convert.ToInt32(cboSpedicijaRTC.SelectedValue), Convert.ToInt32(cboCarinskiPostupak.SelectedValue), Convert.ToInt32(cbPostupak.SelectedValue),
                Convert.ToInt32(cbNacinPakovanja.SelectedValue), Convert.ToInt32(cbOcarina.SelectedValue), Convert.ToInt32(cbOspedicija.SelectedValue),
                Convert.ToInt32(txtMesto.SelectedValue), Convert.ToInt32(txtKontaktOsoba.SelectedValue), txtMail.Text.ToString(), txtPlomba1.Text,
                txtPlomba2.Text, Convert.ToDecimal(txtNetoR.Value), Convert.ToDecimal(txtBrutoR.Value), Convert.ToDecimal(txtTaraK.Value),
                Convert.ToDecimal(txtBrutoK.Value), Convert.ToInt32(cbNapomenaPoz.SelectedValue), Convert.ToDateTime(dtAtaOtprema.Value.ToString()),
                Convert.ToInt32(txtBrojVoza.Text), txtRelacija.Text.ToString().TrimEnd(), Convert.ToDateTime(dtAtaDolazak.Value.ToString()), Convert.ToInt32(txtKoleta.Value), Convert.ToInt32(cboRLTerminal.SelectedValue),
                txtNapomena1.Text, Convert.ToInt32(txtVrstaPregleda.SelectedValue),
                Convert.ToInt32(cboNalogodavac1.SelectedValue), txtRef1.Text,
                Convert.ToInt32(cboNalogodavac2.SelectedValue), txtRef2.Text,
                Convert.ToInt32(cboNalogodavac3.SelectedValue), txtRef3.Text, Convert.ToInt32(cboBrodar.SelectedValue), cboNaslovStatusaVozila.Text, tDobijenBZ, tPrioritet, Convert.ToInt32(txtAdresaMestaUtovara.SelectedValue), txtKontaktOsobe.Text, Convert.ToDecimal(txtTaraTerminal.Value), Convert.ToInt32(txtKoletaTer.Value), Convert.ToInt32(cboScenario.SelectedValue), Convert.ToInt32(cboRLTerminal2.SelectedValue),Convert.ToInt32(cboRLTerminal3.SelectedValue), PotvrdioKlijent, UradilaCarina,
                 TFDobijenNalog, TFDobijenNalogodavac1, Convert.ToDateTime(dtpDobijenNalogodavac1.Value), TFDobijenNalogodavac2, Convert.ToDateTime(dtpDobijenNalogodavac2.Value),
                TFDobijenNalogodavac3, Convert.ToDateTime(dtpDobijenNalogodavac3.Value), TFFCL, TFLCL, Convert.ToDateTime(dtpPotvrdioKlijent.Value), Convert.ToDateTime(dtpSlobodanDaNapusti.Value));
            FillGV();
            RefreshDataGridColor();
        }

        int ProveriBiloIzmena(string UvozID)
        {
            int stariADR = 0;
            int stariTipKontejnera = 0;
            int stariRLTErminali = 0;
            int stariRLTErminali2 = 0;
            int stariRLTErminali3 = 0;
            int stariScenario = 0;

            int noviADR = 0;
            int noviTipKontejnera = 0;
            int noviRLTErminali = 0;
            int noviRLTErminali2 = 0;
            int noviRLTErminali3 = 0;
            int noviScenario = 0;

            int doslodopromene = 0;

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT [ADR] ," +
             " [TipKontejnera] , " +
             " RLTErminali , RLTErminali2 ,RLTErminali3 , Scenario " +
        "  FROM [Uvoz] where ID=" + UvozID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                stariADR = Convert.ToInt32(dr["ADR"].ToString());
                stariTipKontejnera = Convert.ToInt32(dr["TipKontejnera"].ToString());
                stariRLTErminali = Convert.ToInt32(dr["RLTErminali"].ToString());
                stariRLTErminali2 = Convert.ToInt32(dr["RLTErminali2"].ToString());
                stariRLTErminali3 = Convert.ToInt32(dr["RLTErminali3"].ToString());
                stariScenario = Convert.ToInt32(dr["Scenario"].ToString());

            }

            noviADR = Convert.ToInt32(txtADR.SelectedValue);
            noviTipKontejnera = Convert.ToInt32(txtTipKont.SelectedValue);
            noviRLTErminali = Convert.ToInt32(cboRLTerminal.SelectedValue);

            string promena = "";
            int bilopromene = 0;

            if (stariADR != noviADR)
            {
                bilopromene = 1;
                promena = promena + ". ADR";
                //Proveri da li je ispravan scenario
                // MoguciScenario();
                // MessageBox.Show("Doslo je do promene ADR proveriti usluge");
            }

            if (noviTipKontejnera != stariTipKontejnera)
            {
                bilopromene = 1;
                promena = promena + " , Tip kontejnera";
                // MessageBox.Show("Doslo je do promene tipa kontejnera proveriti uslugu zeleznine");
            }

            if (noviRLTErminali != stariRLTErminali)
            {
                bilopromene = 1;
                promena = promena + " , Relacija";
            }
            con.Close();
            if (bilopromene == 1)
            {

                MessageBox.Show("Promenjena su polja potrebno je ponovo generisati usluge" + promena);
                return bilopromene;

            }

            return bilopromene;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertUvozKonacnaZaglavlje ins = new InsertUvozKonacnaZaglavlje();
            ins.InsUvozKonacnaZaglavlje(Convert.ToInt32(cboVoz.SelectedValue), txtNapomenaZaglavlje.Text, 1, "", Convert.ToDateTime("1.1.1900"), "", "", 0);
            //refreshStavke(); - Dodati
        }

        private void ExportToHZ()
        {
            var select = "SELECT row_number() OVER (ORDER BY UvozKonacna.ID) RB, AtaBroda, " +
        "  BrojKontejnera,TipKontenjera.Naziv as TipKontejnera,  PIN, Brodovi.Naziv as Brod, BrodskaTeretnica, " +
        " (VrstaRobeAdr.Naziv + +VrstaRobeAdr.UnKod) as ADR, " +
        "    Partnerji.PaNaziv as Primalac, Partnerji.PaEMatSt1 as PIB, (Cast(BrojPlombe1 as nvarchar(25)) + '/' + Cast(BrojPlombe2 as nvarchar(25))) as Plombe, " +
         "    (" +
    "  SELECT " +
    "  STUFF(" +
   "  (" +
   "  SELECT distinct " +
    "  '/' + Cast(VrstaRobeHS.HSKod as nvarchar(20)) " +
   "  FROM UvozVrstaRobeHS " +
   "  inner join VrstaRobeHS on UvozVrstaRobeHS.IDVrstaRobeHS = VrstaRobeHS.ID " +
   "  where UvozVrstaRobeHS.IDNadredjena = UvozKonacna.ID " +
   "  FOR XML PATH('') " +
   "   ), 1, 1, '' " +
   "  ) As Skupljen) as VrsteRobe,  " +
   "  (" +
   "  SELECT " +
    "  STUFF(" +
   "  (" +
    "  SELECT distinct " +
    "  '/' + Cast(NHM.Broj as nvarchar(20)) " +
   "  FROM UvozNHM " +
   "  inner join NHM on UvozNHM.IDNHM = NHM.ID " +
   "   where UvozNHM.IDNadredjena = UvozKonacna.ID " +
   "  FOR XML PATH('') " +
   "   ), 1, 1, '' " +
   "   ) As Skupljen) as NHM,  " +
    "        Koleta as Koleta, TaraKontejnera as Tara, BrutoRobe as Masarobe, BrutoKontejnera as ukupnatezina, 0 as K447, 0 as tezinapok447 " +
    "        FROM UvozKonacna " +
    " inner join Brodovi on Brodovi.ID = UvozKonacna.NazivBroda " +
    "        inner join Partnerji on PaSifra = VlasnikKontejnera " +
     "        inner join TipKontenjera on TipKontenjera.Id = UvozKonacna.TipKontejnera " +
      "    left join VrstaRobeADR on VrstaRobeADR.ID = UvozKonacna.ADR " +
   "  Where UvozKonacna.IDNadredjeni = " + Convert.ToInt32(txtNadredjeni.Text);

            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var ds = new DataSet();
            dataAdapter.Fill(ds);

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            object missing = System.Reflection.Missing.Value;
            Workbook wBook = excel.Workbooks.Add(missing);

            Worksheet wSheet = new Worksheet();
            try
            {

                wSheet = (Worksheet)wBook.Worksheets.get_Item(1);
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                    {
                        wSheet.Cells[1, 15].EntireRow.Font.Bold = true;
                        wSheet.Range["A1:Q1"].Interior.Color = System.Drawing.Color.AliceBlue;
                        wSheet.Cells[1, "A"] = "RB";
                        wSheet.Cells[1, "B"] = "Atabroda";
                        wSheet.Cells[1, "C"] = "Broj kontejnera";
                        wSheet.Cells[1, "D"] = "Tip Kontejnera";
                        wSheet.Cells[1, "E"] = "PIN";
                        wSheet.Cells[1, "F"] = "Brod";
                        wSheet.Cells[1, "G"] = "Brodska teretnica";
                        wSheet.Cells[1, "H"] = "ADR";
                        wSheet.Cells[1, "I"] = "Partner";
                        wSheet.Cells[1, "J"] = "PIB";
                        wSheet.Cells[1, "K"] = "Plombe";
                        wSheet.Cells[1, "L"] = "Roba";
                        wSheet.Cells[1, "M"] = "NHM";
                        wSheet.Cells[1, "J"] = "Koleta";
                        wSheet.Cells[1, "N"] = "Tara";
                        wSheet.Cells[1, "O"] = "MasaRobe";
                        wSheet.Cells[1, "P"] = "UkupnaTezina";
                        wSheet.Cells[1, "Q"] = "K447";
                        wSheet.Cells[1, "R"] = "P447";

                        wSheet.Cells[i + 2, j + 1] = ds.Tables[0].Rows[i].ItemArray[j].ToString();
                        wSheet.Cells[i + 2, j + 1].EntireColumn.AutoFit();
                        Borders border = wSheet.Cells[i + 2, j + 1].Borders;
                        border.Weight = 2d;

                    }
                }

                string date = DateTime.Now.ToString("dd-MM-yyyy");
                string path = Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
                object filename = @"VLeget A106" + date + ".xlsx";
                wBook.SaveAs(filename);
                wBook.Close();
                excel.Quit();
                excel = null;
                wBook = null;
                wSheet = null;


                MessageBox.Show("Dokument je kreiran");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }


        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void tsNew_Click(object sender, EventArgs e)
        {

        }
        private void VratiADRIzNHM(int Sifra)
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select TOP 1 ADRID from NHM Where ID =" + Sifra, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                if (dr["ADRID"].ToString() != "")
                {
                    txtADR.SelectedValue = Convert.ToInt32(dr["ADRID"].ToString());
                }

            }
            con.Close();


        }

        private void button7_Click(object sender, EventArgs e)
        {
            InsertUvozKonacna uvK = new InsertUvozKonacna();
            uvK.InsUvozKonacnaNHM(Convert.ToInt32(txtID.Text), Convert.ToInt32(cboNHM.SelectedValue));
            VratiADRIzNHM(Convert.ToInt32(cboNHM.SelectedValue));
            FillDG2();
            FillDG2Konacna();
        }

        private void button6_Click(object sender, EventArgs e)
        {

            if (txtIDNHM.Text == "")
            {
                MessageBox.Show("Selektujte stavku koju zelite da izbrisete");


            }
            else
            {
                InsertUvozKonacna uvK = new InsertUvozKonacna();
                uvK.DelUvozKonacnaNHM(Convert.ToInt32(txtIDNHM.Text));
                FillDG2();
                FillDG2Konacna();
            }


        }

        private void button5_Click(object sender, EventArgs e)
        {
            InsertUvozKonacna uvK = new InsertUvozKonacna();
            uvK.InsUvozKonacnaVrstaRobeHS(Convert.ToInt32(txtID.Text), Convert.ToInt32(cboNazivRobe.SelectedValue));
            FillDG3();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtVrstaRobeHS.Text == "")
            {
                MessageBox.Show("Selektujte stavku za brisanje");

            }
            else
            {
                InsertUvozKonacna uvK = new InsertUvozKonacna();
                uvK.DelUvozKonacnaVrstaRobeHS(Convert.ToInt32(txtVrstaRobeHS.Text));
                FillDG3();
            }

        }

        private void FillDG2()
        {
            /*
            if (txtID.Text == "")
                return;
            var select = "  SELECT     UvozKonacnaNHM.ID, NHM.Broj, UvozKonacnaNHM.IDNHM, NHM.Naziv FROM NHM INNER JOIN " +
                      " UvozKonacnaNHM ON NHM.ID = UvozKonacnaNHM.IDNHM where UvozKonacnanhm.idnadredjena = " + Convert.ToInt32(txtID.Text) + " order by UvozKonacnanhm.ID desc";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            //  dataGridView2.BorderStyle = BorderStyle.None;
            PodesiDatagridView(dataGridView2);

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "NHM Broj";
            dataGridView2.Columns[1].Width = 100;

            DataGridViewColumn column3 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "ID";
            dataGridView2.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView2.Columns[3];
            dataGridView2.Columns[3].HeaderText = "NHM";
            dataGridView2.Columns[3].Width = 350;
            */

        }

        private void FillDG3()
        {
            var select = "select UvozKonacnaVrstaRobeHS.ID,  VrstaRobeHS.HSKod, IDVrstaRobeHS, VrstaRobeHS.Naziv from UvozKonacnaVrstaRobeHS " +
" inner join  VrstaRobeHS on VrstaRobeHS.ID = UvozKonacnaVrstaRobeHS.IDVrstaRobeHS where idnadredjena = " + Convert.ToInt32(txtID.Text) + " order by UvozKonacnaVrstaRobeHS.ID desc ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds.Tables[0];


            // dataGridView3.BorderStyle = BorderStyle.None;
            PodesiDatagridView(dataGridView3);

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView3.Columns[0];
            dataGridView3.Columns[0].HeaderText = "ID";
            dataGridView3.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView3.Columns[1];
            dataGridView3.Columns[1].HeaderText = "VRID";
            dataGridView3.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView3.Columns[2];
            dataGridView3.Columns[2].HeaderText = "VRKOD";
            dataGridView3.Columns[2].Width = 80;


            DataGridViewColumn column4 = dataGridView3.Columns[3];
            dataGridView3.Columns[3].HeaderText = "VRNaziv";
            dataGridView3.Columns[3].Width = 180;

        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {
                        txtIDNHM.Text = row.Cells[0].Value.ToString();

                    }
                }
            }
            catch { }
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    if (row.Selected)
                    {
                        txtVrstaRobeHS.Text = row.Cells[0].Value.ToString();

                    }
                }
            }
            catch { }
        }

        private void UvozKonacna_Load(object sender, EventArgs e)
        {
            //RefreshDataGridColor();
            firstWidth = this.Size.Width;
            firstHeight = this.Size.Height;

            fILLsVI();
        }
        private void fILLsVI()
        {
            if (chkTerminalski.Checked == false)
            {
                RefreshGV();
            }
            else
            {
               // RefreshGVT();
            }



        }

        private void RefreshGV()
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
        private void button2_Click(object sender, EventArgs e)
        {
            InsertUvozKonacnaZaglavlje upd = new InsertUvozKonacnaZaglavlje();
            upd.UpdUvozKonacnaZaglavlje(Convert.ToInt32(txtNadredjeni.Text), Convert.ToInt32(cboVoz.SelectedValue), txtNapomenaZaglavlje.Text, 1, "", Convert.ToDateTime("1.1.1900"), "", "");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InsertUvozKonacnaZaglavlje del = new InsertUvozKonacnaZaglavlje();
            del.DelUvozKonacnaZaglavlje(Convert.ToInt32(txtNadredjeni.Text));
        }
        private void ExportToDrumski()
        {
            var select = "SELECT row_number() OVER (ORDER BY UvozKonacna.ID) RB, " +
      "    [EtaBroda],[BrojKontejnera], TipKontenjera.Naziv,UvozKonacna.Napomena, " +
      "    KontejnerskiTerminali.Naziv as RLTerminal, BrodskaTeretnica, (VrstaRobeAdr.Naziv +  + VrstaRobeAdr.UnKod) as ADR , Partnerji.PaNaziv as Vlasnik, Partnerji.PaEMatSt1 as VlasnikPIB,nalogodavac, " +
       "    p1.PaNaziv as Uvoznik, p1.PaEMatSt1 as UvoznikPIB,  " +
  " ( " +
  " SELECT " +
  " STUFF(" +
  "  (" +
  "  SELECT distinct " +
  "   '/' + Cast(VrstaRobeHS.HSKod as nvarchar(20)) " +
   "  FROM UvozKonacnaVrstaRobeHS " +
   "  inner join VrstaRobeHS on UvozKonacnaVrstaRobeHS.IDVrstaRobeHS = VrstaRobeHS.ID " +
  " where UvozKonacnaVrstaRobeHS.IDNadredjena = UvozKonacna.ID " +
  "  FOR XML PATH('') " +
  "   ), 1, 1, '' " +
   " ) As Skupljen) as VrsteRobe,  " +
   " (" +
  " SELECT " +
  " STUFF(" +
  "  (" +
  "  SELECT distinct " +
  "   '/' + Cast(NHM.Broj as nvarchar(20)) " +
   "  FROM UvozKonacnaNHM " +
   "  inner join NHM on UvozKonacnaNHM.IDNHM = NHM.ID " +
  " where UvozKonacnaNHM.IDNadredjena = UvozKonacna.ID " +
  "  FOR XML PATH('') " +
   "  ), 1, 1, '' " +
   " ) As Skupljen) as NHM, " +
   " p2.PaNaziv as SpedicijaRTC, " +
   " VrstaCarinskogPostupka.Naziv as CarinskiPostupak, " +
   "  PostupakSarobom, Napomena,  " +
     "     (Carinarnice.CINaziv + ' ' + Carinarnice.CIOznaka + ' ' + CIEmail + ' ' + CITelefon + ' / ' + p3.PaNaziv) as Carinarnica, " +
     "     (MestoIstovara + ' ' + KontaktOsoba) as MestoIstovara, Email, " +
     "     PredefinisanePoruke.Naziv as NapomenaZaPozicioniranje, NetoRobe, BrutoRobe, TaraKontejnera, BrutoKontejnera, Koleta " +
     "     FROM UvozKonacna " +
     "     inner join Partnerji on PaSifra = VlasnikKontejnera " +
      "    inner join Partnerji p1 on p1.PaSifra = Uvoznik " +
      "      inner join Partnerji p2 on p2.PaSifra = SpedicijaRTC " +
      "       inner join Partnerji p3 on p3.PaSifra = SpedicijaGranica " +
     "    left join VrstaRobeHS on VrstaRobeHS.ID = UvozKonacna.NazivRobe " +
     "    left join VrstaRobe on VrstaRobe.ID = NHMBroj " +
     "    left join VrstaRobeADR on VrstaRobeADR.ID = UvozKonacna.ADR " +
      "   left join TipKontenjera on TipKontenjera.ID = UvozKonacna.TipKontejnera " +
     "     left join Carinarnice on Carinarnice.ID = UvozKonacna.OdredisnaCarina " +
      "     left join KontejnerskiTerminali on KontejnerskiTerminali.ID = UvozKonacna.RLTerminali " +
     "    left join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = UvozKonacna.CarinskiPostupak " +
     "    left join Predefinisaneporuke on PredefinisanePoruke.ID = UvozKonacna.NapomenaZaPozicioniranje " +
     "   Where UvozKonacna.IDNadredjeni = " + Convert.ToInt32(txtNadredjeni.Text);

            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var ds = new DataSet();
            dataAdapter.Fill(ds);

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            object missing = System.Reflection.Missing.Value;
            Workbook wBook = excel.Workbooks.Add(missing);

            Worksheet wSheet = new Worksheet();
            try
            {

                wSheet = (Worksheet)wBook.Worksheets.get_Item(1);
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                    {
                        wSheet.Cells[1, 15].EntireRow.Font.Bold = true;
                        wSheet.Range["A1:N1"].Interior.Color = System.Drawing.Color.AliceBlue;
                        wSheet.Cells[1, "A"] = "RB";
                        wSheet.Cells[1, "B"] = "ETA dolazak broda u Luku Rijeka";
                        wSheet.Cells[1, "C"] = "Broj kontejnera";
                        wSheet.Cells[1, "D"] = "Tip kontejnera";
                        wSheet.Cells[1, "E"] = "Napomena za stavku";
                        wSheet.Cells[1, "F"] = "R/L/drugi terminal";
                        wSheet.Cells[1, "G"] = "BL-brodska tertnica";
                        wSheet.Cells[1, "H"] = "ADR";
                        wSheet.Cells[1, "I"] = "Vlasnik kontejnera";
                        wSheet.Cells[1, "J"] = "Vlasnik PIB";
                        wSheet.Cells[1, "K"] = "Nalogodavaci";
                        wSheet.Cells[1, "L"] = "Uvoznik";
                        wSheet.Cells[1, "M"] = "Uvoznik PIB";
                        wSheet.Cells[1, "N"] = "Vrsta robe";
                        wSheet.Cells[1, "O"] = "NHM";
                        wSheet.Cells[1, "P"] = "Špedicija - Leget";
                        wSheet.Cells[1, "Q"] = "Carinski postupak";
                        wSheet.Cells[1, "R"] = "Postupanje sa robom/kontejeromnt";
                        wSheet.Cells[1, "S"] = "Napomena za RTC LUKA LEGET";
                        wSheet.Cells[1, "T"] = "Odredišna Carinska ispostav+špedicija";

                        wSheet.Cells[1, "U"] = "Mesto istovara+kontakt osoba";
                        wSheet.Cells[1, "V"] = "e-mail adrese za slanje statusa";
                        wSheet.Cells[1, "W"] = "Napomena za pozicioniranje kontejner";
                        wSheet.Cells[1, "X"] = "Neto robe (kg)";
                        wSheet.Cells[1, "Y"] = "Bruto robe (kg)";
                        wSheet.Cells[1, "Z"] = "Tara kontejnera (kg)";
                        wSheet.Cells[1, "AA"] = "Bruto kontejnera (kg)";



                        wSheet.Cells[i + 2, j + 1] = ds.Tables[0].Rows[i].ItemArray[j].ToString();
                        wSheet.Cells[i + 2, j + 1].EntireColumn.AutoFit();
                        Borders border = wSheet.Cells[i + 2, j + 1].Borders;
                        border.Weight = 2d;

                    }
                }

                string date = DateTime.Now.ToString("dd-MM-yyyy");
                string path = Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
                object filename = @"ExportDrumski" + date + ".xlsx";
                wBook.SaveAs(filename);
                wBook.Close();
                excel.Quit();
                excel = null;
                wBook = null;
                wSheet = null;


                MessageBox.Show("Dokument za drumski prevoz je kreiran");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void ExportToMagacin()
        {
            var select = "SELECT row_number() OVER (ORDER BY UvozKonacna.ID) RB, " +
       "    [EtaBroda],[BrojKontejnera], TipKontenjera.Naziv,UvozKonacna.Napomena, " +
       "    KontejnerskiTerminali.Naziv as RLTerminal, BrodskaTeretnica, ADR, Partnerji.PaNaziv as Vlasnik, Partnerji.PaEMatSt1 as VlasnikPIB,nalogodavac, " +
        "    p1.PaNaziv as Uvoznik, p1.PaEMatSt1 as UvoznikPIB,  " +
   " ( " +
   " SELECT " +
   " STUFF(" +
   "  (" +
   "  SELECT distinct " +
   "   '/' + Cast(VrstaRobeHS.HSKod as nvarchar(20)) " +
    "  FROM UvozKonacnaVrstaRobeHS " +
    "  inner join VrstaRobeHS on UvozKonacnaVrstaRobeHS.IDVrstaRobeHS = VrstaRobeHS.ID " +
   " where UvozKonacnaVrstaRobeHS.IDNadredjena = UvozKonacna.ID " +
   "  FOR XML PATH('') " +
   "   ), 1, 1, '' " +
    " ) As Skupljen) as VrsteRobe,  " +
    " (" +
   " SELECT " +
   " STUFF(" +
   "  (" +
   "  SELECT distinct " +
   "   '/' + Cast(NHM.Broj as nvarchar(20)) " +
    "  FROM UvozKonacnaNHM " +
    "  inner join NHM on UvozKonacnaNHM.IDNHM = NHM.ID " +
   " where UvozKonacnaNHM.IDNadredjena = UvozKonacna.ID " +
   "  FOR XML PATH('') " +
    "  ), 1, 1, '' " +
    " ) As Skupljen) as NHM, " +
    " p2.PaNaziv as SpedicijaRTC, " +
    " VrstaCarinskogPostupka.Naziv as CarinskiPostupak, " +
    "  PostupakSarobom, Napomena,  " +
      "     (Carinarnice.CINaziv + ' ' + Carinarnice.CIOznaka + ' ' + CIEmail + ' ' + CITelefon + ' / ' + p3.PaNaziv) as Carinarnica, " +
      "     (MestoIstovara + ' ' + KontaktOsoba) as MestoIstovara, " +
      "     PredefinisanePoruke.Naziv as NapomenaZaPozicioniranje, NetoRobe, BrutoRobe, TaraKontejnera, BrutoKontejnera, Koleta " +
      "     FROM UvozKonacna " +
      "     inner join Partnerji on PaSifra = VlasnikKontejnera " +
       "    inner join Partnerji p1 on p1.PaSifra = Uvoznik " +
       "      inner join Partnerji p2 on p2.PaSifra = SpedicijaRTC " +
       "       inner join Partnerji p3 on p3.PaSifra = SpedicijaGranica " +
      "    left join VrstaRobeHS on VrstaRobeHS.ID = UvozKonacna.NazivRobe " +
      "    left join VrstaRobe on VrstaRobe.ID = NHMBroj " +
       "   left join TipKontenjera on TipKontenjera.ID = UvozKonacna.TipKontejnera " +
      "     left join Carinarnice on Carinarnice.ID = UvozKonacna.OdredisnaCarina " +
       "     left join KontejnerskiTerminali on KontejnerskiTerminali.ID = UvozKonacna.RLTerminali " +
      "    left join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = UvozKonacna.CarinskiPostupak " +
      "    left join Predefinisaneporuke on PredefinisanePoruke.ID = UvozKonacna.NapomenaZaPozicioniranje " +
      "   Where UvozKonacna.IDNadredjeni = " + Convert.ToInt32(txtNadredjeni.Text);

            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var ds = new DataSet();
            dataAdapter.Fill(ds);

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            object missing = System.Reflection.Missing.Value;
            Workbook wBook = excel.Workbooks.Add(missing);

            Worksheet wSheet = new Worksheet();
            try
            {

                wSheet = (Worksheet)wBook.Worksheets.get_Item(1);
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                    {
                        wSheet.Cells[1, 15].EntireRow.Font.Bold = true;
                        wSheet.Range["A1:N1"].Interior.Color = System.Drawing.Color.AliceBlue;
                        wSheet.Cells[1, "A"] = "RB";
                        wSheet.Cells[1, "B"] = "ETA dolazak broda u Luku Rijeka";
                        wSheet.Cells[1, "C"] = "Broj kontejnera";
                        wSheet.Cells[1, "D"] = "Tip kontejnera";
                        wSheet.Cells[1, "E"] = "Napomena za stavku";
                        wSheet.Cells[1, "F"] = "R/L/drugi terminal";
                        wSheet.Cells[1, "G"] = "BL-brodska tertnica";
                        wSheet.Cells[1, "H"] = "ADR";
                        wSheet.Cells[1, "I"] = "Vlasnik kontejnera";
                        wSheet.Cells[1, "J"] = "Vlasnik PIB";
                        wSheet.Cells[1, "K"] = "Nalogodavaci";
                        wSheet.Cells[1, "L"] = "Uvoznik";
                        wSheet.Cells[1, "M"] = "Uvoznik PIB";
                        wSheet.Cells[1, "N"] = "Vrsta robe";
                        wSheet.Cells[1, "O"] = "NHM";
                        wSheet.Cells[1, "P"] = "Špedicija - Leget";
                        wSheet.Cells[1, "Q"] = "Carinski postupak";
                        wSheet.Cells[1, "R"] = "Postupanje sa robom/kontejeromnt";
                        wSheet.Cells[1, "S"] = "Napomena za RTC LUKA LEGET";
                        wSheet.Cells[1, "T"] = "Odredišna Carinska ispostav+špedicija";

                        wSheet.Cells[1, "U"] = "Mesto istovara+kontakt osoba";
                        wSheet.Cells[1, "V"] = "Napomena za pozicioniranje kontejner";
                        wSheet.Cells[1, "W"] = "Neto robe (kg)";
                        wSheet.Cells[1, "X"] = "Bruto robe (kg)";
                        wSheet.Cells[1, "Y"] = "Tara kontejnera (kg)";
                        wSheet.Cells[1, "Z"] = "Bruto kontejnera (kg)";
                        wSheet.Cells[1, "AA"] = "Koleta";
                        // wSheet.Cells[1, "Z"] = "Bruto kontejnera (kg)";



                        wSheet.Cells[i + 2, j + 1] = ds.Tables[0].Rows[i].ItemArray[j].ToString();
                        wSheet.Cells[i + 2, j + 1].EntireColumn.AutoFit();
                        Borders border = wSheet.Cells[i + 2, j + 1].Borders;
                        border.Weight = 2d;

                    }
                }

                string date = DateTime.Now.ToString("dd-MM-yyyy");
                string path = Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
                object filename = @"ExportMagacin" + date + ".xlsx";
                wBook.SaveAs(filename);
                wBook.Close();
                excel.Quit();
                excel = null;
                wBook = null;
                wSheet = null;


                MessageBox.Show("Dokument za drumski prevoz je kreiran");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {

        }
        private void ExportToBezbednost()
        {
            var select = "SELECT row_number() OVER (ORDER BY UvozKonacna.ID) RB, " +
    "    [EtaBroda],[BrojKontejnera], TipKontenjera.Naziv, " +
    "     BrodskaTeretnica, ADR, Partnerji.PaNaziv as Vlasnik, Partnerji.PaEMatSt1 as VlasnikPIB,nalogodavac, " +
     "    p1.PaNaziv as Uvoznik, p1.PaEMatSt1 as UvoznikPIB,  " +
   " ( " +
   " SELECT " +
   " STUFF(" +
   "  (" +
   "  SELECT distinct " +
   "   '/' + Cast(VrstaRobeHS.HSKod as nvarchar(20)) " +
   "  FROM UvozKonacnaVrstaRobeHS " +
   "  inner join VrstaRobeHS on UvozKonacnaVrstaRobeHS.IDVrstaRobeHS = VrstaRobeHS.ID " +
   " where UvozKonacnaVrstaRobeHS.IDNadredjena = UvozKonacna.ID " +
   "  FOR XML PATH('') " +
   "   ), 1, 1, '' " +
   " ) As Skupljen) as VrsteRobe,  " +
   " (" +
   " SELECT " +
   " STUFF(" +
   "  (" +
   "  SELECT distinct " +
   "   '/' + Cast(RTRIM(NHM.Broj) as nvarchar(20)) " +
   "  FROM UvozKonacnaNHM " +
   "  inner join NHM on UvozKonacnaNHM.IDNHM = NHM.ID " +
   " where UvozKonacnaNHM.IDNadredjena = UvozKonacna.ID " +
   "  FOR XML PATH('') " +
   "  ), 1, 1, '' " +
   " ) As Skupljen) as NHM, " +
   "    NetoRobe, BrutoRobe, TaraKontejnera, BrutoKontejnera, Koleta " +
   "     FROM UvozKonacna " +
   "     inner join Partnerji on PaSifra = VlasnikKontejnera " +
    "    inner join Partnerji p1 on p1.PaSifra = Uvoznik " +
    "      inner join Partnerji p2 on p2.PaSifra = SpedicijaRTC " +
    "       inner join Partnerji p3 on p3.PaSifra = SpedicijaGranica " +
   "    left join VrstaRobeHS on VrstaRobeHS.ID = UvozKonacna.NazivRobe " +
   "    left join VrstaRobe on VrstaRobe.ID = NHMBroj " +
    "   left join TipKontenjera on TipKontenjera.ID = UvozKonacna.TipKontejnera " +
   "     left join Carinarnice on Carinarnice.ID = UvozKonacna.OdredisnaCarina " +
   "    left join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = UvozKonacna.CarinskiPostupak " +
   "    left join Predefinisaneporuke on PredefinisanePoruke.ID = UvozKonacna.NapomenaZaPozicioniranje " +
   "   Where UvozKonacna.IDNadredjeni = " + Convert.ToInt32(txtNadredjeni.Text);

            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var ds = new DataSet();
            dataAdapter.Fill(ds);

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            object missing = System.Reflection.Missing.Value;
            Workbook wBook = excel.Workbooks.Add(missing);

            Worksheet wSheet = new Worksheet();
            try
            {

                wSheet = (Worksheet)wBook.Worksheets.get_Item(1);
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                    {
                        wSheet.Cells[1, 15].EntireRow.Font.Bold = true;
                        wSheet.Range["A1:N1"].Interior.Color = System.Drawing.Color.AliceBlue;
                        wSheet.Cells[1, "A"] = "RB";
                        wSheet.Cells[1, "B"] = "ETA dolazak broda u Luku Rijeka";
                        wSheet.Cells[1, "C"] = "Broj kontejnera";
                        wSheet.Cells[1, "D"] = "Tip kontejnera";
                        wSheet.Cells[1, "E"] = "BL-brodska tertnica";
                        wSheet.Cells[1, "F"] = "ADR";
                        wSheet.Cells[1, "G"] = "Vlasnik kontejnera";
                        wSheet.Cells[1, "H"] = "Vlasnik PIB";
                        wSheet.Cells[1, "I"] = "Nalogodavaci";
                        wSheet.Cells[1, "J"] = "Uvoznik";
                        wSheet.Cells[1, "K"] = "Uvoznik PIB";
                        wSheet.Cells[1, "L"] = "Vrsta robe";
                        wSheet.Cells[1, "M"] = "NHM";
                        wSheet.Cells[1, "N"] = "Neto robe (kg)";
                        wSheet.Cells[1, "O"] = "Bruto robe (kg)";
                        wSheet.Cells[1, "P"] = "Tara kontejnera (kg)";
                        wSheet.Cells[1, "Q"] = "Bruto kontejnera (kg)";
                        wSheet.Cells[1, "R"] = "Koleta";
                        // wSheet.Cells[1, "Z"] = "Bruto kontejnera (kg)";



                        wSheet.Cells[i + 2, j + 1] = ds.Tables[0].Rows[i].ItemArray[j].ToString();
                        wSheet.Cells[i + 2, j + 1].EntireColumn.AutoFit();
                        Borders border = wSheet.Cells[i + 2, j + 1].Borders;
                        border.Weight = 2d;

                    }
                }

                string date = DateTime.Now.ToString("dd-MM-yyyy");
                string path = Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
                object filename = @"ExportBezb" + date + ".xlsx";
                wBook.SaveAs(filename);
                wBook.Close();
                excel.Quit();
                excel = null;
                wBook = null;
                wSheet = null;


                MessageBox.Show("Dokument za drumski prevoz je kreiran");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }
        private void toolStripButton5_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            UvozDokumenta uvdok = new UvozDokumenta(txtID.Text, txtNadredjeni.Text);
            uvdok.Show();
        }

        private void txtIDNHM_TextChanged(object sender, EventArgs e)
        {

        }
        private void FillDG4()
        {
            if (txtID.Text == "")
            {
                return;
            }
            var select = "select UvozKonacnaNapomenePozicioniranja.ID, IDNapomene, NapomenaZaPozicioniranje.Naziv from UvozKonacnaNapomenePozicioniranja " +
" inner join  NapomenaZaPozicioniranje on NapomenaZaPozicioniranje.ID = UvozKonacnaNapomenePozicioniranja.IDNapomene where UvozKonacnaNapomenePozicioniranja.IdNadredjena  = " + Convert.ToInt32(txtID.Text) + " order by UvozKonacnaNapomenePozicioniranja.ID desc ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView4.ReadOnly = true;
            dataGridView4.DataSource = ds.Tables[0];

            //  dataGridView4.BorderStyle = BorderStyle.None;
            PodesiDatagridView(dataGridView4);

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView4.Columns[0];
            dataGridView4.Columns[0].HeaderText = "ID";
            dataGridView4.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView4.Columns[1];
            dataGridView4.Columns[1].HeaderText = "NapomenaID";
            dataGridView4.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView4.Columns[2];
            dataGridView4.Columns[2].HeaderText = "Napomena";
            dataGridView4.Columns[2].Width = 250;

        }

        private void button9_Click(object sender, EventArgs e)
        {
            InsertUvozKonacna uvK = new InsertUvozKonacna();
            uvK.InsUvozKonacnaNapomenePozicioniranja(Convert.ToInt32(txtID.Text), Convert.ToInt32(cbNapomenaPoz.SelectedValue), cbNapomenaPoz.Text);
            FillDG4();
            RefreshDataGridColor();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            InsertUvozKonacna uvK = new InsertUvozKonacna();
            uvK.DelUvozKonacnaNapomenePozicioniranja(Convert.ToInt32(txtNapomenaPoz.Text));
            FillDG4();
        }

        private void dataGridView4_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView4.Rows)
                {
                    if (row.Selected)
                    {
                        txtNapomenaPoz.Text = row.Cells[0].Value.ToString();

                    }
                }
            }
            catch { }
        }

        private void txtBrutoK_Leave(object sender, EventArgs e)
        {
            txtBrutoK.Value = txtBrutoR.Value + txtTaraK.Value;
        }

        private void txtTaraK_Leave(object sender, EventArgs e)
        {
            txtBrutoK.Value = txtBrutoR.Value + txtTaraK.Value;
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
        }

        private void cbPostupak_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FillDG6()
        {

            var select = "select Cene.ID, VrstaManipulacije.Naziv, Cene.Cena, VrstaManipulacije.ID from Cene " +
            " inner join VrstaManipulacije on VrstaManipulacije.ID = Cene.VrstaManipulacije " +
            " where TipCenovnika = 1 order by VrstaManipulacije.Naziv ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView6.ReadOnly = true;
            dataGridView6.DataSource = ds.Tables[0];


            //  dataGridView6.BorderStyle = BorderStyle.None;
            PodesiDatagridView(dataGridView6);

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView6.Columns[0];
            dataGridView6.Columns[0].HeaderText = "ID";
            dataGridView6.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView6.Columns[1];
            dataGridView6.Columns[1].HeaderText = "Man";
            dataGridView6.Columns[1].Width = 120;

            DataGridViewColumn column3 = dataGridView6.Columns[2];
            dataGridView6.Columns[2].HeaderText = "Cena";
            dataGridView6.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView6.Columns[3];
            dataGridView6.Columns[3].HeaderText = "IDVM";
            dataGridView6.Columns[3].Width = 50;
            dataGridView6.Columns[3].Visible = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            FillDG6();
        }

        private void FillDG7()
        {

            var select = "select Cene.ID, VrstaManipulacije.Naziv, Cene.Cena, VrstaManipulacije.ID from Cene " +
            " inner join VrstaManipulacije on VrstaManipulacije.ID = Cene.VrstaManipulacije " +
            " where TipCenovnika <> 1 and Cene.PostupakSaRobom = " + Convert.ToInt32(cbPostupak.SelectedValue) + " and Cene.Uvoznik = " + Convert.ToInt32(cboUvoznik.SelectedValue) + "  order by VrstaManipulacije.Naziv ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView6.ReadOnly = true;
            dataGridView6.DataSource = ds.Tables[0];


            // dataGridView6.BorderStyle = BorderStyle.None;
            PodesiDatagridView(dataGridView6);

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView6.Columns[0];
            dataGridView6.Columns[0].HeaderText = "ID";
            dataGridView6.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView6.Columns[1];
            dataGridView6.Columns[1].HeaderText = "Man";
            dataGridView6.Columns[1].Width = 120;

            DataGridViewColumn column3 = dataGridView6.Columns[2];
            dataGridView6.Columns[2].HeaderText = "Cena";
            dataGridView6.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView6.Columns[3];
            dataGridView6.Columns[3].HeaderText = "IDVM";
            dataGridView6.Columns[3].Width = 50;
            dataGridView6.Columns[3].Visible = false;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            FillDG7();
        }

        private void UbaciStavkuUsluge(int ID, int Manipulacija, double Cena)
        {
            InsertUvozKonacna uvK = new InsertUvozKonacna();
            //   uvK.InsUbaciUsluguKonacna(Convert.ToInt32(txtID.Text), Manipulacija, Cena);
            // FillDG8();
        }

        private void FillDG8()
        {
            var select = "select  UvozKonacnaVrstaManipulacije.ID, VrstaManipulacije.Naziv, UvozKonacnaVrstaManipulacije.Cena, VrstaManipulacije.ID from UvozKonacnaVrstaManipulacije " +
            " inner join VrstaManipulacije on VrstaManipulacije.ID = UvozKonacnaVrstaManipulacije.IDVrstaManipulacije " +
                " where UvozKonacnaVrstaManipulacije.IDNadredjena = " + Convert.ToInt32(txtID.Text);
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView5.ReadOnly = true;
            dataGridView5.DataSource = ds.Tables[0];


            // dataGridView5.BorderStyle = BorderStyle.None;
            PodesiDatagridView(dataGridView5);

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView5.Columns[0];
            dataGridView5.Columns[0].HeaderText = "ID";
            dataGridView5.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView5.Columns[1];
            dataGridView5.Columns[1].HeaderText = "Man";
            dataGridView5.Columns[1].Width = 120;

            DataGridViewColumn column3 = dataGridView5.Columns[2];
            dataGridView5.Columns[2].HeaderText = "Cena";
            dataGridView5.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView5.Columns[3];
            dataGridView5.Columns[3].HeaderText = "IDVM";
            dataGridView5.Columns[3].Width = 50;
            dataGridView5.Columns[3].Visible = false;

        }

        private void button12_Click(object sender, EventArgs e)
        {
            int pomID = 0;
            int pomManupulacija = 0;
            double pomCena = 0;
            try
            {
                foreach (DataGridViewRow row in dataGridView6.Rows)
                {
                    if (row.Selected)
                    {
                        pomID = Convert.ToInt32(row.Cells[0].Value.ToString());
                        pomManupulacija = Convert.ToInt32(row.Cells[3].Value.ToString());
                        pomCena = Convert.ToDouble(row.Cells[2].Value.ToString());
                        UbaciStavkuUsluge(pomID, pomManupulacija, pomCena);
                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void FormirajOpstiExcel()
        {
            var select = "SELECT row_number() OVER (ORDER BY UvozKonacna.ID) RB, " +
                  "ATABroda,[BrojKontejnera], TipKontenjera.Naziv as Vrsta_Kontejnera," +
                  " DobijenNalogBrodara as Dobijen_Nalog_Brodara , DobijeBZ as DatumBZ ,Napomena1 as Napomena1," +
                  "  PIN, " +
   "   KontejnerskiTerminali.Naziv as R_L_SRB, pp1.Naziv as Dirigacija_Kontejnera_Za,  " +
   "   BrodskaTeretnica as BL, VrstaRobeADR.Naziv as ADR, b.PaNaziv as Brodar, n1.PaNaziv as Nalogodavac1, Ref1 as Ref1, n2.PaNaziv as Nalogodavac2, Ref2 as Ref2,  " +
    "      p1.PaNaziv as Uvoznik,   " +
    "  (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaManipulacije.Naziv as nvarchar(20))   FROM UvozKonacnaVrstaManipulacije " +
     "       inner join VrstaManipulacije on VrstaManipulacije.ID = UvozKonacnaVrstaManipulacije.IDVrstaManipulacije   where UvozKonacnaVrstaManipulacije.IDNadredjena = UvozKonacna.ID " +
     "        FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as VrsteUsluga,   " +
     "     (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaRobeHS.HSKod as nvarchar(20))   FROM UvozKonacnaVrstaRobeHS " +
     "       inner join VrstaRobeHS on UvozKonacnaVrstaRobeHS.IDVrstaRobeHS = VrstaRobeHS.ID   where UvozKonacnaVrstaRobeHS.IDNadredjena = UvozKonacna.ID " +
     "        FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as VrsteRobe,   " +
     "    (SELECT  STUFF((SELECT distinct    '/' + Cast(NHM.Broj as nvarchar(20)) " +
    "            FROM UvozKonacnaNHM  inner join NHM on UvozKonacnaNHM.IDNHM = NHM.ID  where UvozKonacnaNHM.IDNadredjena = UvozKOnacna.ID   FOR XML PATH('')), 1, 1, ''  ) As Skupljen) as NHM,   " +
     "              VrstaPregleda as VrstaPregleda,p2.PaNaziv as SpedicijaRTC,  p3.PaNaziv as SpedicijaGranica,      " +
     " VrstaCarinskogPostupka.Naziv as CarinskiPostupak,   VrstaPregleda , " +
     "                  VrstePostupakaUvoz.Naziv as PostupakSaRobom,uvNacinPakovanja.Naziv as NacinPakovanja, Napomena as Napomena2,  " +
     "                      (Carinarnice.CINaziv + ' ' + Carinarnice.CIOznaka + ' ' + CIEmail + ' ' + CITelefon + ' / ' + p3.PaNaziv) as Carinarnica,   " +
     "                               p4.PaNaziv as OdredisnaSpedicija, (MestoIstovara + ' ' + KontaktOsoba) as MestoIstovara, Email,          " +
  " PredefinisanePoruke.Naziv as NapomenaZaPozicioniranje,  " +
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
   " inner join Predefinisaneporuke on PredefinisanePoruke.ID = UvozKonacna.NapomenaZaPozicioniranje " +
   "  inner join KontejnerskiTerminali on KontejnerskiTerminali.ID = UvozKonacna.RLTErminali " +
   "  inner join Partnerji n1 on n1.PaSifra = Nalogodavac1 " +
   "  inner join Partnerji n2 on n2.PaSifra = Nalogodavac2 " +
   "  inner join Partnerji n3 on n3.PaSifra = Nalogodavac3 " +
   "  inner join Partnerji b on b.PaSifra = UvozKonacna.Brodar " +
    " inner join PredefinisanePoruke pp1 on pp1.ID = DirigacijaKontejeraZa   " +
   "  inner join Brodovi on Brodovi.ID = UvozKonacna.NazivBroda " +
                                "   inner join VrstaRobeADR on VrstaRobeADR.ID = ADR " +
                                "    inner join VrstePostupakaUvoz on VrstePostupakaUvoz.ID = PostupakSaRobom    inner join uvNacinPakovanja " +
   " on uvNacinPakovanja.ID = NacinPakovanja  inner join Partnerji p4 on p4.PaSifra = OdredisnaSpedicija " +
  "  Where UvozKonacna.IDNadredjeni = " + Convert.ToInt32(txtNadredjeni.Text) + "order by UvozKonacna.ID desc";

            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var ds = new DataSet();
            dataAdapter.Fill(ds);

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            object missing = System.Reflection.Missing.Value;
            Workbook wBook = excel.Workbooks.Add(missing);

            Worksheet wSheet = new Worksheet();
            try
            {

                wSheet = (Worksheet)wBook.Worksheets.get_Item(1);
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                    {
                        wSheet.Cells[1, 15].EntireRow.Font.Bold = true;
                        wSheet.Range["A1:AM1"].Interior.Color = System.Drawing.Color.AliceBlue;
                        wSheet.Cells[1, "A"] = "RB";
                        wSheet.Cells[1, "B"] = "Atabroda";
                        wSheet.Cells[1, "C"] = "Broj kontejnera";
                        wSheet.Cells[1, "D"] = "Vrsta Kontejnera";
                        wSheet.Cells[1, "E"] = "Dobijen nalog brodara";
                        wSheet.Cells[1, "F"] = "Dobijen BZ";
                        wSheet.Cells[1, "G"] = "Napomena za stavku";
                        wSheet.Cells[1, "H"] = "PIN/Šifra";
                        wSheet.Cells[1, "I"] = "R_L_SRB";
                        wSheet.Cells[1, "J"] = "Naziv broda";
                        wSheet.Cells[1, "K"] = "BL / Brodska teretnica";
                        wSheet.Cells[1, "L"] = "ADR";
                        wSheet.Cells[1, "M"] = "Brodar";
                        wSheet.Cells[1, "N"] = "Nalogodavac 1";
                        wSheet.Cells[1, "O"] = "Ref1";
                        wSheet.Cells[1, "P"] = "Nalogodavac 2";
                        wSheet.Cells[1, "Q"] = "Ref2";
                        wSheet.Cells[1, "R"] = "Uvoznik";
                        wSheet.Cells[1, "S"] = "Vrste manipulacije";
                        wSheet.Cells[1, "T"] = "Vrsta robe";
                        wSheet.Cells[1, "U"] = "NHM";
                        wSheet.Cells[1, "V"] = "Vrsta pregleda";
                        wSheet.Cells[1, "W"] = "Špedicija RTC";
                        wSheet.Cells[1, "X"] = "Špedicija granica";
                        wSheet.Cells[1, "Y"] = "Carinski postupak";
                        wSheet.Cells[1, "Z"] = "VrstaPregleda ";
                        wSheet.Cells[1, "AA"] = "Postupak sa Robom";
                        wSheet.Cells[1, "AB"] = "Način pakovanja";
                        wSheet.Cells[1, "AC"] = "Napomena 2";
                        wSheet.Cells[1, "AD"] = "Carinarnica";
                        wSheet.Cells[1, "AE"] = "Odredišna špedicija";
                        wSheet.Cells[1, "AF"] = "Mesto istovara";
                        wSheet.Cells[1, "AG"] = "Email za slanje statusa";
                        wSheet.Cells[1, "AH"] = "Napomena za pozicioniranje";
                        wSheet.Cells[1, "AI"] = "NetoRobe";
                        wSheet.Cells[1, "AJ"] = "BrutoRobe";
                        wSheet.Cells[1, "AK"] = "TaraKontejnera";
                        wSheet.Cells[1, "AL"] = "BrutoKontejnera";
                        wSheet.Cells[1, "AM"] = "Koleta";

                        wSheet.Cells[i + 2, j + 1] = ds.Tables[0].Rows[i].ItemArray[j].ToString();
                        wSheet.Cells[i + 2, j + 1].EntireColumn.AutoFit();
                        Borders border = wSheet.Cells[i + 2, j + 1].Borders;
                        border.Weight = 2d;

                    }
                }

                string date = DateTime.Now.ToString("dd-MM-yyyy");
                string path = Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
                object filename = @"Opsti" + date + ".xlsx";
                wBook.SaveAs(filename);
                wBook.Close();
                excel.Quit();
                excel = null;
                wBook = null;
                wSheet = null;


                MessageBox.Show("Dokument je kreiran");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {

            /* DialogResult dialogResult = MessageBox.Show("Da li pravite i naloge Da/Ne", "Radni nalog", MessageBoxButtons.YesNo);

             if (dialogResult == DialogResult.Yes)
             {
                 InsertRadniNalogInterni ins = new InsertRadniNalogInterni();

                     ins.InsRadniNalogInterni(Convert.ToInt32(1), Convert.ToInt32(5), Convert.ToDateTime(DateTime.Now), Convert.ToDateTime(DateTime.MinValue), "", Convert.ToInt32(0), "PlanUtovara", Convert.ToInt32(row.Cells[0].Value.ToString()), "sa", "sa");

                 FormirajOpstiExcel();
             }
             else
             {
                 FormirajOpstiExcel();
             }
           */
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void UpdateVrednostiPolja(int IdZaPromenu)
        {
            string updatestring = "";
            switch (cboPolje.Text)
            {
                case "Naziv broda":
                    updatestring = " Update uvoz set NazivBroda = " + cbBrod.SelectedValue;
                    break;
                case "Napomena 1":
                    updatestring = " Update uvoz set Napomena1 = '" + txtNapomena1.Text + "'";
                    break;
                case "Datum BZ":
                    updatestring = " Update uvoz set DobijeBZ = '" + txtBZ.Text.ToString().TrimEnd() + "'";
                    break;
                case "PIN":
                    updatestring = " Update uvoz set PIN = '" + txtPIN.Text + "'";
                    break;
                case "Vrsta kontejnera":
                    updatestring = " Update uvoz set TaraKontejnera = " + Convert.ToInt32(txtTipKont.SelectedValue);
                    break;
                case "Relacija R\\L\\SRB":
                    updatestring = " Update uvoz set RLTerminali = " + Convert.ToInt32(cboRLTerminal.SelectedValue);
                    break;
                case "BL":
                    updatestring = " Update uvoz set BrodskaTeretnica = '" + txtTeretnica.Text + "'";
                    break;
                case "ADR":
                    updatestring = " Update uvoz set ADR = " + Convert.ToInt32(txtADR.SelectedValue);
                    break;
                case "Brodar":
                    updatestring = " Update uvoz set Brodar = " + Convert.ToInt32(cboBrodar.SelectedValue);
                    break;
                case "Vlasnik":
                    updatestring = " Update uvoz set Vlasnik = " + Convert.ToInt32(cbVlasnikKont.SelectedValue);
                    break;
                case "Uvoznik":
                    updatestring = " Update uvoz set Uvoznik = " + Convert.ToInt32(cboUvoznik.SelectedValue);
                    break;
                case "Nalogodavac 1":
                    updatestring = " Update uvoz set Nalogodavac1 = " + Convert.ToInt32(cboNalogodavac1.SelectedValue);
                    break;
                case "Ref1":
                    updatestring = " Update uvoz set Ref1 = '" + txtRef1.Text + "'";
                    break;
                case "Nalogodavac 2":
                    updatestring = " Update uvoz set Nalogodavac2 = " + Convert.ToInt32(cboNalogodavac2.SelectedValue);
                    break;
                case "Ref2":
                    updatestring = " Update uvoz set Ref2 = '" + txtRef2.Text + "'";
                    break;
                case "Nalogodavac3":
                    updatestring = " Update uvoz set Nalogodavac3 = " + Convert.ToInt32(cboNalogodavac3.SelectedValue);
                    break;
                case "Ref3":
                    updatestring = " Update uvoz set Ref3 = '" + txtRef3.Text + "'";
                    break;
                case "VrstaPregleda":
                    updatestring = " Update uvoz set VrstaPregleda = '" + Convert.ToInt32(txtVrstaPregleda.SelectedValue) + "'";
                    break;
                case "Špedicija - RTCLeget":
                    updatestring = " Update uvoz set SpedicijaRTC = " + Convert.ToInt32(cboSpedicijaRTC.SelectedValue);
                    break;
                case "Špedicija granica":
                    updatestring = " Update uvoz set SpedicijaGranica = " + Convert.ToInt32(cboSpedicijaG.SelectedValue);
                    break;
                case "Carinski postupak":
                    updatestring = " Update uvoz set CarinskiPostupak = " + Convert.ToInt32(cboCarinskiPostupak.SelectedValue);
                    break;
                case "Postupak sa robom":
                    updatestring = " Update uvoz set PostupakSaRobom = " + Convert.ToInt32(cbPostupak.SelectedValue);
                    break;
                case "Način pakovanja":
                    updatestring = " Update uvoz set NacinPakovanja = " + Convert.ToInt32(cbNacinPakovanja.SelectedValue);
                    break;
                case "Napomena 2":
                    updatestring = " Update uvoz set Napomena2 = " + txtNapomena.Text;
                    break;
                case "Odredišna špedicija":
                    updatestring = " Update uvoz set OdredisnaSpedicija = " + Convert.ToInt32(cbOspedicija.SelectedValue);
                    break;
                case "Carinarnica":
                    updatestring = " Update uvoz set OdredisnaCarina = " + Convert.ToInt32(cbOcarina.SelectedValue);
                    break;
                case "Mesto istovara":
                    updatestring = " Update uvoz set MestoIstovara = '" + Convert.ToInt32(txtMesto.SelectedValue) + "'";
                    break;
                case "Kontakt osoba":
                    updatestring = " Update uvoz set KontaktOsoba = '" + Convert.ToInt32(txtKontaktOsoba.Text) + "'";
                    break;
                case "EMail":
                    updatestring = " Update uvoz set Email = '" + txtMail.Text.ToString() + "'";
                    break;
                default:
                    Console.WriteLine("Nema podatka");
                    break;

            }


            try
            {
                // 1. Create Command
                // Sql Update Statement
                string updateSql = updatestring + " where ID = " + IdZaPromenu;

                SqlConnection conn = new SqlConnection(connection);
                SqlCommand cmd = new SqlCommand(updateSql, conn);
                conn.Open();
                var q = cmd.ExecuteNonQuery();
                conn.Close();


            }

            catch (SqlException ex)
            {
                // Display error
                Console.WriteLine("Error: " + ex.ToString());
            }



            //FillGV();
        }


        private void button10_Click(object sender, EventArgs e)
        {
            int IDZaPromenu = 0;
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        IDZaPromenu = Convert.ToInt32(row.Cells[0].Value.ToString());
                        UpdateVrednostiPolja(IDZaPromenu);
                    }
                }
                FillGV();

            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }
        int VratiPostojeceRN()
        {
            return 0;
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if (chkTerminalski.Checked == false)
            {
                DialogResult dialogResult = MessageBox.Show("Pokrenuli ste proceduru pravljenja naloga za službu terminal, nalozi se neće izdati za Administrativne usluge", "Radni nalog", MessageBoxButtons.YesNo);
                int PostojeRn = 0;
                PostojeRn = VratiPostojeceRN();
                if (dialogResult == DialogResult.Yes)
                {

                    InsertRadniNalogInterni ins = new InsertRadniNalogInterni();
                    ins.InsRadniNalogInterni(Convert.ToInt32(1), Convert.ToInt32(4), Convert.ToDateTime(DateTime.Now), Convert.ToDateTime("1.1.1900. 00:00:00"), "", Convert.ToInt32(0), "PlanUtovara", Convert.ToInt32(txtNadredjeni.Text), KorisnikTekuci, "");

                }
                else
                {
                    // FormirajOpstiExcel();
                }
            }

            ///Ako je plan Terminalski onda izdajemi terminalske naloge
            ///
            if (chkTerminalski.Checked == true)
            {
                DialogResult dialogResult = MessageBox.Show("Pokrenuli ste proceduru pravljenja Terminalskog naloga ", "Radni nalog", MessageBoxButtons.YesNo);
                int PostojeRn = 0;
                PostojeRn = VratiPostojeceRN();
                if (dialogResult == DialogResult.Yes)
                {

                    InsertRadniNalogInterni ins = new InsertRadniNalogInterni();
                    ins.InsRadniNalogInterni(Convert.ToInt32(4), Convert.ToInt32(4), Convert.ToDateTime(DateTime.Now), Convert.ToDateTime("1.1.1900. 00:00:00"), "", Convert.ToInt32(0), "PlanUtovara", Convert.ToInt32(txtNadredjeni.Text), KorisnikTekuci, "");

                }
                else
                {
                    // FormirajOpstiExcel();
                }
            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label50_Click(object sender, EventArgs e)
        {

        }

        private void exportToExcelHŽToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportToHZ();
        }

        private void exportToExcelDrumskiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportToDrumski();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {

        }

        private void exportToExcelBezbednostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportToBezbednost();
        }

        private void exportToExcelMagacinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportToMagacin();
        }
        private void ExportToSpediter()
        {

            var select = "SELECT row_number() OVER (ORDER BY UvozKonacna.ID) RB, " +
  "    [BrojKontejnera], TipKontenjera.Naziv as TipKontejnera,UvozKonacna.Napomena, " +
  "    BrodskaTeretnica, (VrstaRobeAdr.Naziv +  + VrstaRobeAdr.UnKod) as ADR ,  " +
   "    p1.PaNaziv as Uvoznik, p1.PaEMatSt1 as UvoznikPIB,  " +
" ( " +
" SELECT " +
" STUFF(" +
"  (" +
"  SELECT distinct " +
"   '/' + Cast(VrstaRobeHS.HSKod as nvarchar(20)) " +
"  FROM UvozKonacnaVrstaRobeHS " +
"  inner join VrstaRobeHS on UvozKonacnaVrstaRobeHS.IDVrstaRobeHS = VrstaRobeHS.ID " +
" where UvozKonacnaVrstaRobeHS.IDNadredjena = UvozKonacna.ID " +
"  FOR XML PATH('') " +
"   ), 1, 1, '' " +
" ) As Skupljen) as VrsteRobe,  " +
" (" +
" SELECT " +
" STUFF(" +
"  (" +
"  SELECT distinct " +
"   '/' + Cast(NHM.Broj as nvarchar(20)) " +
"  FROM UvozKonacnaNHM " +
"  inner join NHM on UvozKonacnaNHM.IDNHM = NHM.ID " +
" where UvozKonacnaNHM.IDNadredjena = UvozKonacna.ID " +
"  FOR XML PATH('') " +
"  ), 1, 1, '' " +
" ) As Skupljen) as NHM, " +
" p3.PaNaziv as SpedicijaGranica, " +
" p2.PaNaziv as SpedicijaRTC, " +
" VrstaCarinskogPostupka.Naziv as CarinskiPostupak, " +
" VrstePostupakaUvoz.Naziv as PostupakSarobom, Napomena,  " +
 "     (Carinarnice.CINaziv + ' ' + Carinarnice.CIOznaka + ' ' + CIEmail + ' ' + CITelefon + ' / ' + p3.PaNaziv) as Carinarnica, " +
 "     (MestoIstovara + ' ' + KontaktOsoba) as MestoIstovara, Email, " +
 "      BrutoRobe, TaraKontejnera, BrutoKontejnera " +
 "     FROM UvozKonacna " +
 "     inner join Partnerji on PaSifra = VlasnikKontejnera " +
  "    inner join Partnerji p1 on p1.PaSifra = Uvoznik " +
  "      inner join Partnerji p2 on p2.PaSifra = SpedicijaRTC " +
  "       inner join Partnerji p3 on p3.PaSifra = SpedicijaGranica " +
 "    left join VrstaRobeHS on VrstaRobeHS.ID = UvozKonacna.NazivRobe " +
 "    left join VrstaRobe on VrstaRobe.ID = NHMBroj " +
 "    left join VrstaRobeADR on VrstaRobeADR.ID = UvozKonacna.ADR " +
  "   left join TipKontenjera on TipKontenjera.ID = UvozKonacna.TipKontejnera " +
 "     left join Carinarnice on Carinarnice.ID = UvozKonacna.OdredisnaCarina " +
  "     left join KontejnerskiTerminali on KontejnerskiTerminali.ID = UvozKonacna.RLTerminali " +
 "    left join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = UvozKonacna.CarinskiPostupak " +
 "    left join Predefinisaneporuke on PredefinisanePoruke.ID = UvozKonacna.NapomenaZaPozicioniranje " +
  "    left join VrstePostupakaUvoz on VrstePostupakaUvoz.ID = UvozKonacna.PostupakSaRobom " +
 "   Where UvozKonacna.IDNadredjeni = " + Convert.ToInt32(txtNadredjeni.Text);

            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var ds = new DataSet();
            dataAdapter.Fill(ds);

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            object missing = System.Reflection.Missing.Value;
            Workbook wBook = excel.Workbooks.Add(missing);

            Worksheet wSheet = new Worksheet();
            try
            {

                wSheet = (Worksheet)wBook.Worksheets.get_Item(1);
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                    {
                        wSheet.Cells[1, 15].EntireRow.Font.Bold = true;
                        wSheet.Range["A1:N1"].Interior.Color = System.Drawing.Color.AliceBlue;
                        wSheet.Cells[1, "A"] = "RB";

                        wSheet.Cells[1, "B"] = "Broj kontejnera";
                        wSheet.Cells[1, "C"] = "Tip kontejnera";
                        wSheet.Cells[1, "D"] = "Napomena za stavku";

                        wSheet.Cells[1, "E"] = "BL-brodska tertnica";
                        wSheet.Cells[1, "F"] = "ADR";

                        wSheet.Cells[1, "G"] = "Uvoznik";
                        wSheet.Cells[1, "H"] = "Uvoznik PIB";
                        wSheet.Cells[1, "I"] = "Vrsta robe";
                        wSheet.Cells[1, "J"] = "NHM";
                        wSheet.Cells[1, "K"] = "Špedicija - granica";
                        wSheet.Cells[1, "L"] = "Špedicija - Leget";
                        wSheet.Cells[1, "M"] = "Carinski postupak";
                        wSheet.Cells[1, "N"] = "Postupanje sa robom/kontejeromnt";
                        wSheet.Cells[1, "O"] = "Napomena za RTC LUKA LEGET";
                        wSheet.Cells[1, "P"] = "Odredišna Carinska ispostav+špedicija";

                        wSheet.Cells[1, "R"] = "Mesto istovara+kontakt osoba";
                        wSheet.Cells[1, "S"] = "e-mail adrese za slanje statusa";
                        wSheet.Cells[1, "T"] = "Napomena za pozicioniranje kontejner";

                        wSheet.Cells[1, "U"] = "Bruto robe (kg)";
                        wSheet.Cells[1, "V"] = "Tara kontejnera (kg)";
                        wSheet.Cells[1, "W"] = "Bruto kontejnera (kg)";



                        wSheet.Cells[i + 2, j + 1] = ds.Tables[0].Rows[i].ItemArray[j].ToString();
                        wSheet.Cells[i + 2, j + 1].EntireColumn.AutoFit();
                        Borders border = wSheet.Cells[i + 2, j + 1].Borders;
                        border.Weight = 2d;

                    }
                }

                string date = DateTime.Now.ToString("dd-MM-yyyy");
                string path = Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
                object filename = @"ExportSpediter" + date + ".xlsx";
                wBook.SaveAs(filename);
                wBook.Close();
                excel.Quit();
                excel = null;
                wBook = null;
                wSheet = null;


                MessageBox.Show("Dokument za drumski prevoz je kreiran");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void exportToExcelŠpediterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportToSpediter();
        }

        private void exportToExcelOpšteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormirajOpstiExcel();
        }

        private void txtNapomena_TextChanged(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            frmFormiranjePlana fplan = new frmFormiranjePlana(Convert.ToInt32(txtNadredjeni.Text));
            fplan.Show();
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            // FillGV();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connection);
            /*
          
            */
            if (chkInterni.Checked == true)
            {
                switch (NHMObrni)
                {
                    case 1:
                        {
                            var nhm = "Select ID,Rtrim(Broj) + '-' + (Rtrim(Naziv)) as Naziv from NHM where Interni = 1 order by NHM.Broj";
                            var nhmSAD = new SqlDataAdapter(nhm, conn);
                            var nhmSDS = new DataSet();
                            nhmSAD.Fill(nhmSDS);
                            cboNHM.DataSource = nhmSDS.Tables[0];
                            cboNHM.DisplayMember = "Naziv";
                            cboNHM.ValueMember = "ID";
                            NHMObrni = 0;
                            break;

                        }
                    case 0:
                        {
                            var nhm = "Select ID,Rtrim(Naziv) + '-' + (Rtrim(Broj)) as Naziv from NHM where Interni = 1 order by NHM.Naziv";
                            var nhmSAD = new SqlDataAdapter(nhm, conn);
                            var nhmSDS = new DataSet();
                            nhmSAD.Fill(nhmSDS);
                            cboNHM.DataSource = nhmSDS.Tables[0];
                            cboNHM.DisplayMember = "Naziv";
                            cboNHM.ValueMember = "ID";
                            NHMObrni = 1;
                            break;
                        }
                    default:
                        break;
                }
            }
            else
            {
                switch (NHMObrni)
                {
                    case 1:
                        {
                            var nhm = "Select ID,Rtrim(Broj) + '-' + (Rtrim(Naziv)) as Naziv from NHM order by NHM.Broj";
                            var nhmSAD = new SqlDataAdapter(nhm, conn);
                            var nhmSDS = new DataSet();
                            nhmSAD.Fill(nhmSDS);
                            cboNHM.DataSource = nhmSDS.Tables[0];
                            cboNHM.DisplayMember = "Naziv";
                            cboNHM.ValueMember = "ID";
                            NHMObrni = 0;
                            break;

                        }
                    case 0:
                        {
                            var nhm = "Select ID,Rtrim(Naziv) + '-' + (Rtrim(Broj)) as Naziv from NHM order by NHM.Naziv";
                            var nhmSAD = new SqlDataAdapter(nhm, conn);
                            var nhmSDS = new DataSet();
                            nhmSAD.Fill(nhmSDS);
                            cboNHM.DataSource = nhmSDS.Tables[0];
                            cboNHM.DisplayMember = "Naziv";
                            cboNHM.ValueMember = "ID";
                            NHMObrni = 1;
                            break;
                        }
                    default:
                        break;
                }
            }

        }

        private void button17_Click(object sender, EventArgs e)
        {

            SqlConnection conn = new SqlConnection(connection);
            // PaKOOpomba
            //Bilo  var ko = "select PaKoZapSt, (Rtrim(PaKOIme) + ' ' + Rtrim(PaKoPriimek)) as Naziv from partnerjiKontOsebaMU where PaKOSifra = '" + Convert.ToInt32(cboMestoUtovara.SelectedValue) + "'  order by PaKOIme";
            var ko = "select PaKoZapSt, (Rtrim(PaKOOpomba)) as Naziv from partnerjiKontOsebaMU where PaKOSifra = '" + Convert.ToInt32(txtMesto.SelectedValue) + "'  order by PaKOIme";

            var koAD = new SqlDataAdapter(ko, conn);
            var koDS = new DataSet();
            koAD.Fill(koDS);
            txtAdresaMestaUtovara.DataSource = koDS.Tables[0];
            txtAdresaMestaUtovara.DisplayMember = "Naziv";
            txtAdresaMestaUtovara.ValueMember = "PaKoZapSt";


            /*
            SqlConnection conn = new SqlConnection(connection);

            var ko = "select PaKoZapSt, (Rtrim(PaKOIme) + ' ' + Rtrim(PaKoPriimek)) as Naziv from partnerjiKontOsebaMU where PaKOSifra = '" + Convert.ToInt32(txtMesto.SelectedValue) + "'  order by PaKOIme";
            var koAD = new SqlDataAdapter(ko, conn);
            var koDS = new DataSet();
            koAD.Fill(koDS);
            txtKontaktOsoba.DataSource = koDS.Tables[0];
            txtKontaktOsoba.DisplayMember = "Naziv";
            txtKontaktOsoba.ValueMember = "PaKoZapSt";
            */
        }

        private void VratiEmail(int Sifra)
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select PaKOMail from partnerjiKontOsebaMU where PaKOZapSt = =" + Sifra, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtMail.Text = dr["PaKOMail"].ToString();


            }
            con.Close();


        }

        private void button16_Click(object sender, EventArgs e)
        {
            VratiEmail(Convert.ToInt32(txtKontaktOsoba.SelectedValue));
        }

        private void VratiNHM(int Sifra)
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select TOP 1 ID from NHM Where ADRID =  = =" + Sifra, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtMail.Text = dr["ID"].ToString();


            }
            con.Close();


        }

        private void button18_Click(object sender, EventArgs e)
        {
            VratiNHM(Convert.ToInt32(txtADR.SelectedValue));
        }

        private void button19_Click(object sender, EventArgs e)
        {
            using (var detailForm = new Dokumenta.frmKontaktOsobe(Convert.ToInt32(cboNalogodavac3.SelectedValue)))
            {
                detailForm.ShowDialog();

                txtMail.Text = detailForm.GetKontaktMail(Convert.ToInt32(cboNalogodavac3.SelectedValue));
            }
        }

        private void button16_Click_1(object sender, EventArgs e)
        {
            if (checkedListBox2.GetItemCheckState(0) == CheckState.Checked)
            {
                cboNaslovStatusaVozila.Text = " " + txtBrKont.Text;
            }

            if (checkedListBox2.GetItemCheckState(1) == CheckState.Checked)
            {
                cboNaslovStatusaVozila.Text = cboNaslovStatusaVozila.Text + "  " + txtADR.Text;
            }


            if (checkedListBox2.GetItemCheckState(2) == CheckState.Checked)
            {
                cboNaslovStatusaVozila.Text = cboNaslovStatusaVozila.Text + "  " + cboUvoznik.Text;
            }
            if (checkedListBox2.GetItemCheckState(3) == CheckState.Checked)
            {
                cboNaslovStatusaVozila.Text = cboNaslovStatusaVozila.Text + "  " + cboNalogodavac3.Text;
            }
        }

        private void dataGridView4_SelectionChanged_1(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView4.Rows)
                {
                    if (row.Selected)
                    {
                        txtNapomenaPoz.Text = row.Cells[0].Value.ToString();

                    }
                }
            }
            catch { }
        }

        private void txtNetoR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void txtBrutoK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void txtBrutoR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void txtKoleta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void txtTaraK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void frmUvozKonacna_SizeChanged(object sender, EventArgs e)
        {
            /*
            float size1 = this.Size.Width / firstWidth;
            float size2 = this.Size.Height / firstHeight;

            SizeF scale = new SizeF(size1, size2);
            firstWidth = this.Size.Width;
            firstHeight = this.Size.Height;

            foreach (Control control in this.Controls)
            {

                control.Font = new System.Drawing.Font(control.Font.FontFamily, control.Font.Size * ((size1 + size2) / 2));

                control.Scale(scale);

                string ff = control.GetType().Name;
                if (control.GetType().Name == "ComboBox")
                {
                    ComboBox cb = (ComboBox)control;
                    cb.SelectedIndex = -1;
                }
            }
            */
        }
   
        private void VratiZelezninu(int RLTerminal1, int RLTerminal2, int RLTerminal3, string TipKOntejnera)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select VrstaManipulacije.ID, Relacija from VrstaManipulacije  " +
              " inner join TipKontenjera on VrstaManipulacije.TipKontejnera = TipKontenjera.ID" +
              " where GrupaVrsteManipulacijeID = 1 and Substring(TipKontenjera.SkNaziv,1,3) = '" + TipKOntejnera + "'' AND RLTerminali = " +
              RLTerminal1 + " and RLTerminali2 = " + RLTerminal2 + " AND RLTerminali3 = " + RLTerminal3, con);

            
            SqlDataReader dr = cmd.ExecuteReader();




            while (dr.Read())
            {

                Zeleznina = Convert.ToInt32(dr["ID"].ToString());
                relacija = dr["Relacija"].ToString();
            }
            con.Close();


        }

        string relacija;
        string relacija2;
        string relacija3;
     


        private void MoguciScenario()
        {
            string Moguce = "";
            //Provera SCENARIJA UKLJUCITI ADR
            if (relacija == relacija3)
            {
                ScenarioGl = 1; ///Vraca se

            }
            else if (relacija2 == relacija3)
            {
                ScenarioGl = 2; //Ostaje
            }
            else if (relacija != relacija2 && relacija2 != relacija3 && relacija != relacija3)
            {
                ScenarioGl = 3;  //Odlazi

            }
            else
            {
                MessageBox.Show("Relacije ne pripadaju ni jednom scenariju");
                return;
            }



            //Provera SCENARIJA UKLJUCITI ADR
            if (chkTerminalski.Checked == true)
            {
                Moguce = "15";
            }
            else if (ScenarioGl == 1 && Convert.ToInt32(txtADR.SelectedValue) == 0 && pp > 0)
            {
                Moguce = "1,2,27"; // npr Rijeka - leget - Rijeka   ,2,27
            }
            else if (ScenarioGl == 1 && Convert.ToInt32(txtADR.SelectedValue) > 1 && pp > 0)
            {
                Moguce = "18,19,30";
            }

            else if (ScenarioGl == 2 && Convert.ToInt32(txtADR.SelectedValue) == 0 && pp > 0)
            {
                Moguce = "3,4,28"; // Ostaje na terminalu
            }
            else if (ScenarioGl == 2 && Convert.ToInt32(txtADR.SelectedValue) == 0 && pp == 0)
            {
                Moguce = "6"; // Prazan
            }
            else if (ScenarioGl == 2 && Convert.ToInt32(txtADR.SelectedValue) > 1 && pp > 0)
            {
                Moguce = "20,21,31";

            }
            else if (ScenarioGl == 3 && Convert.ToInt32(txtADR.SelectedValue) == 0 && pp > 0)
            {
                Moguce = "5"; // Ostaje na terminalu
            }
            else if (ScenarioGl == 3 && Convert.ToInt32(txtADR.SelectedValue) > 1 && pp > 0)
            {
                Moguce = "22"; // Ostaje na terminalu
            }
            else if (ScenarioGl == 4 )
            {
                Moguce = "32"; // CIRADA PRETOVAR ROBE IZ KAMIONA U KAMION
            }


            int poklapase = 0;
            string[] split = Moguce.Split(',');
            foreach (string item in split)
            {
                if (item == cboScenario.SelectedValue.ToString())
                {
                    poklapase = 1;
                }

            }

            if (poklapase == 0)
            {
                DialogResult result = MessageBox.Show("Trenutni scenario je" + cboScenario.Text + " moguci scenariji " + Moguce + " se ne poklapaju sa njim, da li želite brisanje terminalskih usluge?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    InsertUvozKonacna uvK = new InsertUvozKonacna();
                    uvK.DelUvozUslugaTerminalskih(Convert.ToInt32(txtID.Text));
                }
                else
                {
                    //...
                }

            }
        }
        int pp = 0;
        private void toolStripButton4_Click_1(object sender, EventArgs e)
        {
           
            pp = ProveriPrazanPun();
            if (pp == 0)
            {
                VratiRepoziciju(Convert.ToInt32(cboRLTerminal.SelectedValue), Convert.ToInt32(cboRLTerminal2.SelectedValue), Convert.ToInt32(cboRLTerminal3.SelectedValue), txtTipKont.Text.Substring(0, 3));
            }
            else
            {
                if (txtTipKont.Text.Length >= 3)
                {
                VratiZelezninu(Convert.ToInt32(cboRLTerminal.SelectedValue), Convert.ToInt32(cboRLTerminal2.SelectedValue), Convert.ToInt32(cboRLTerminal3.SelectedValue), txtTipKont.Text.Substring(0, 3));
                }
            }




            ADR = Convert.ToInt32(txtADR.SelectedValue);
            if (txtID.Text == "")
            { txtID.Text = "0"; }
            // int IDPlana, int ID, int Nalogodavac1, int Nalogodavac2, int Nalogodavac3
            int terminal = 0;
            if(chkTerminalski.Checked) { terminal = 1; }
            relacija = cboRLTerminal.Text.ToString().TrimEnd();
           
            relacija2 = cboRLTerminal2.Text.ToString().TrimEnd();
            relacija3 = cboRLTerminal3.Text.ToString().TrimEnd();
            MoguciScenario();
            frmUnosManipulacija um = new frmUnosManipulacija(Convert.ToInt32(txtNadredjeni.Text), Convert.ToInt32(txtID.Text), Convert.ToInt32(cboNalogodavac1.SelectedValue), Convert.ToInt32(cboNalogodavac2.SelectedValue), Convert.ToInt32(cboNalogodavac3.SelectedValue), Convert.ToInt32(cboUvoznik.SelectedValue), KorisnikTekuci, terminal, relacija,Zeleznina,ADR, ScenarioGl,pp, Repozicija) ;
            um.Show();
        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            using (var detailForm = new frmUvozKonacnaTable(txtNadredjeni.Text))
            {
                detailForm.ShowDialog();
                txtID.Text = detailForm.GetID();
                if (txtID.Text == "")
                {
                    MessageBox.Show("Morate izabrati barem jednu stavku");
                    return;

                }
                VratiPodatkeSelect(Convert.ToInt32(txtID.Text));
            }
        }

        private void frmUvozKonacna_KeyDown(object sender, KeyEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift && e.KeyCode == Keys.F1)
            {
                UvozDokumenta uvdok = new UvozDokumenta(txtID.Text, txtNadredjeni.Text);
                uvdok.Show();
            }
            else if (Control.ModifierKeys == Keys.Shift && e.KeyCode == Keys.F2)
            {
                using (var detailForm = new frmUvozKonacnaTable())
                {
                    detailForm.ShowDialog();
                    txtID.Text = detailForm.GetID();
                    VratiPodatkeSelect(Convert.ToInt32(txtID.Text));
                }
            }
            else if (Control.ModifierKeys == Keys.Shift && e.KeyCode == Keys.F3)
            {
                if (txtID.Text == "")
                { txtID.Text = "0"; }
                int terminal = 0;
                if (chkTerminalski.Checked)
                {
                    terminal = 1;
                }
                relacija = cboRLTerminal.Text.ToString().TrimEnd();
                relacija2 = cboRLTerminal2.Text.ToString().TrimEnd();
                relacija3 = cboRLTerminal3.Text.ToString().TrimEnd();
                // int IDPlana, int ID, int Nalogodavac1, int Nalogodavac2, int Nalogodavac3
                frmUnosManipulacija um = new frmUnosManipulacija(Convert.ToInt32(txtNadredjeni.Text), Convert.ToInt32(txtID.Text), Convert.ToInt32(cboNalogodavac1.SelectedValue), Convert.ToInt32(cboNalogodavac2.SelectedValue), Convert.ToInt32(cboNalogodavac3.SelectedValue), Convert.ToInt32(cboUvoznik.SelectedValue), KorisnikTekuci,terminal, relacija, Zeleznina, ADR, ScenarioGl,pp, Repozicija);
                um.Show();

            }
        }

        private void chkDobijenBZ_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDobijenBZ.Checked == true)
            {
                txtBZ.Visible = true;
            }
            else
            {
                txtBZ.Visible = false;
            }
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            using (var detailForm = new Izvoz.frmKontaktOsobeMU(txtAdresaMestaUtovara.Text))
            {
                detailForm.ShowDialog();
                txtKontaktOsobe.Text = detailForm.GetSviKontaktiPoAdresi(txtAdresaMestaUtovara.Text);
            }
            // txtKontaktOsobe.Text = GetSviKontaktiPoAdresi(txtAdresaMestaUtovara.Text);




        }

        private void toolStripButton5_Click_1(object sender, EventArgs e)
        {
            int TcbBrod = Convert.ToInt32(cbBrod.SelectedValue);
            int TtxtTipKont = Convert.ToInt32(txtTipKont.SelectedValue);
            int TcboRLTerminal = Convert.ToInt32(cboRLTerminal.SelectedValue);
            int TcbDirigacija = Convert.ToInt32(cbDirigacija.SelectedValue);
            int TtxtADR = Convert.ToInt32(txtADR.SelectedValue);
            int TcboBrodar = Convert.ToInt32(cboBrodar.SelectedValue);
            int TcbVlasnikKont = Convert.ToInt32(cbVlasnikKont.SelectedValue);
            int TcboNalogodavac1 = Convert.ToInt32(cboNalogodavac1.SelectedValue);
            int TcboNalogodavac2 = Convert.ToInt32(cboNalogodavac2.SelectedValue);
            int TcboNalogodavac3 = Convert.ToInt32(cboNalogodavac3.SelectedValue);
            int TcboUvoznik = Convert.ToInt32(cboUvoznik.SelectedValue);
            int TtxtVrstaPregleda = Convert.ToInt32(txtVrstaPregleda.SelectedValue);
            int TcboSpedicijaG = Convert.ToInt32(cboSpedicijaG.SelectedValue);
            int TcboSpedicijaRTC = Convert.ToInt32(cboSpedicijaRTC.SelectedValue);
            int TcboCarinskiPostupak = Convert.ToInt32(cboCarinskiPostupak.SelectedValue);
            int TcbPostupak = Convert.ToInt32(cbPostupak.SelectedValue);
            int TcbNacinPakovanja = Convert.ToInt32(cbNacinPakovanja.SelectedValue);
            int TcbOspedicija = Convert.ToInt32(cbOspedicija.SelectedValue);
            int TcbOcarina = Convert.ToInt32(cbOcarina.SelectedValue);
            int TtxtMesto = Convert.ToInt32(txtMesto.SelectedValue);
            int TtxtAdresaMestaUtovara = Convert.ToInt32(txtAdresaMestaUtovara.SelectedValue);
            FillCombo();
            cbBrod.SelectedValue = TcbBrod;
            txtTipKont.SelectedValue = TtxtTipKont;
            cboRLTerminal.SelectedValue = TcboRLTerminal;
            cbDirigacija.SelectedValue = TcbDirigacija;
            txtADR.SelectedValue = TtxtADR;
            cboBrodar.SelectedValue = TcboBrodar;
            cbVlasnikKont.SelectedValue = TcbVlasnikKont;
            cboNalogodavac1.SelectedValue = TcboNalogodavac1;
            cboNalogodavac2.SelectedValue = TcboNalogodavac2;
            cboNalogodavac3.SelectedValue = TcboNalogodavac3;
            cboUvoznik.SelectedValue = TcboUvoznik;
            txtVrstaPregleda.SelectedValue = TtxtVrstaPregleda;
            cboSpedicijaG.SelectedValue = TcboSpedicijaG;
            cboSpedicijaRTC.SelectedValue = TcboSpedicijaRTC;
            cboCarinskiPostupak.SelectedValue = TcboCarinskiPostupak;
            cbPostupak.SelectedValue = TcbPostupak;
            cbNacinPakovanja.SelectedValue = TcbNacinPakovanja;
            cbOspedicija.SelectedValue = TcbOspedicija;
            cbOcarina.SelectedValue = TcbOcarina;
            txtMesto.SelectedValue = TtxtMesto;
            txtAdresaMestaUtovara.SelectedValue = TtxtAdresaMestaUtovara;

        }

        private void txtNadredjeni_TextChanged(object sender, EventArgs e)
        {
            FillDGUsluge();
            FillDG2();
            FillDG4();
            FillDG2Konacna();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Saobracaj.Uvoz.frmPrijemVozaIzPlana pvizp = new Saobracaj.Uvoz.frmPrijemVozaIzPlana();
            pvizp.Show();
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
            FillDGUsluge();
            FillDG2();
            FillDG4();
            FillDG2Konacna();
        }

        private void cboVoz_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripButton2_Click_2(object sender, EventArgs e)
        {

        }

        private void chkDrumski_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void button20_Click(object sender, EventArgs e)
        {
            using (var detailForm = new Dokumenta.frmKontaktOsobe(Convert.ToInt32(cbOcarina.SelectedValue), Convert.ToInt32(cbOspedicija.SelectedValue)))
            {
                detailForm.ShowDialog();
                txtKontaktOsobeSpeditera.Text = detailForm.GetKontaktSpeditera();
            }
        }

        private void RefreshScenario()
        {
            if (txtID.Text == "")
            {
                return;
            }
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT Scenario" +
  " FROM [Uvoz] where ID=" + txtID.Text, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                cboScenario.SelectedValue = Convert.ToInt32(dr["Scenario"].ToString());
            }
            con.Close();

        }

        private void frmUvozKonacna_Activated(object sender, EventArgs e)
        {
            FillDGUsluge();
            RefreshScenario();
        }

        private void button23_Click(object sender, EventArgs e)
        {

            UvozDokumenta uvdok = new UvozDokumenta(txtID.Text, txtNadredjeni.Text);
            uvdok.Show();
        }
        int Repozicija = 0;


        private void VratiRepoziciju(int RLTerminal1, int RLTerminal2, int RLTerminal3, string TipKOntejnera)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select VrstaManipulacije.ID, Relacija from VrstaManipulacije  " +
                " inner join TipKontenjera on VrstaManipulacije.TipKontejnera = TipKontenjera.ID " +
                " where GrupaVrsteManipulacijeID = 2 and Substring(TipKontenjera.SkNaziv,1,3) = '" + TipKOntejnera + "'' AND RLTerminali = " +
                RLTerminal1 + " and RLTerminali2 = " + RLTerminal2 + " AND RLTerminali3 = " + RLTerminal3, con);
            SqlDataReader dr = cmd.ExecuteReader();




            while (dr.Read())
            {

                Repozicija = Convert.ToInt32(dr["ID"].ToString());
                //  relacija = dr["Relacija"].ToString();
            }
            con.Close();


        }

        int ProveriPrazanPun()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            int idnhm = 0;
            con.Open();

            SqlCommand cmd = new SqlCommand("select top 1 IDNHM from UvozKonacnaNHM  where IDnADREDJENA = " + Convert.ToInt32(txtID.Text), con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                idnhm = Convert.ToInt32(dr["IDNHM"].ToString());

            }

            con.Close();

            return idnhm;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            pp = ProveriPrazanPun();
            if (pp == 0)
            {
                VratiRepoziciju(Convert.ToInt32(cboRLTerminal.SelectedValue), Convert.ToInt32(cboRLTerminal2.SelectedValue), Convert.ToInt32(cboRLTerminal3.SelectedValue), txtTipKont.Text.Substring(0, 3));
            }
            else
            {
                if (txtTipKont.Text.Length > 3)
                {
                VratiZelezninu(Convert.ToInt32(cboRLTerminal.SelectedValue), Convert.ToInt32(cboRLTerminal2.SelectedValue), Convert.ToInt32(cboRLTerminal3.SelectedValue), txtTipKont.Text.Substring(0, 3));
                }
            }


          //  VratiZelezninu(Convert.ToInt32(cboRLTerminal.SelectedValue), Convert.ToInt32(cboRLTerminal2.SelectedValue), Convert.ToInt32(cboRLTerminal3.SelectedValue), Convert.ToInt32(txtTipKont.SelectedValue));
            ADR = Convert.ToInt32(txtADR.SelectedValue);
            if (txtID.Text == "")
            { txtID.Text = "0"; }
            // int IDPlana, int ID, int Nalogodavac1, int Nalogodavac2, int Nalogodavac3
            int terminal = 0;
            if (chkTerminalski.Checked) { terminal = 1; }
           

            relacija = cboRLTerminal.Text.ToString().TrimEnd();
            relacija2 = cboRLTerminal2.Text.ToString().TrimEnd();
            relacija3 = cboRLTerminal3.Text.ToString().TrimEnd();
            if (txtBrKont.Text.StartsWith("ROB-"))
            {

                ScenarioGl = 4; /// CIRADA U CIRADU
            }
            else
            {
                MoguciScenario();
            }
            frmUnosManipulacija um = new frmUnosManipulacija(Convert.ToInt32(txtNadredjeni.Text), Convert.ToInt32(txtID.Text), Convert.ToInt32(cboNalogodavac1.SelectedValue), Convert.ToInt32(cboNalogodavac2.SelectedValue), Convert.ToInt32(cboNalogodavac3.SelectedValue), Convert.ToInt32(cboUvoznik.SelectedValue), KorisnikTekuci, terminal, relacija, Zeleznina, ADR, ScenarioGl,pp, Repozicija);
            um.Show();
            FillDGUsluge();
            FillDG2();
            FillDG4();
            FillDG2Konacna();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            using (var detailForm = new frmUvozKonacnaTable(txtNadredjeni.Text))
            {
                detailForm.ShowDialog();
                txtID.Text = detailForm.GetID();
                if (txtID.Text == "")
                {
                    MessageBox.Show("Morate izabrati barem jednu stavku");
                    return;

                }
                VratiPodatkeSelect(Convert.ToInt32(txtID.Text));

                RefreshScenario();
                FillDG2();
                FillDG8();
                FillDG4();
                FillDGUvoznici();
                FillDG2Konacna();

            }
        }
        private void FillDG2Konacna()
        {
            if (txtID.Text == "")
            {
                txtID.Text = "0";
            }
            var select = " SELECT     UvozKonacnaNHM.ID, NHM.Broj, UvozKonacnaNHM.IDNHM, NHM.Naziv, KomercijalniNaziv, TarifniBroj, BrojKoleta, Bruto, Vrednost, Valuta FROM NHM INNER JOIN " +
                      " UvozKonacnaNHM ON NHM.ID = UvozKonacnaNHM.IDNHM where UvozKonacnanhm.idnadredjena = " + Convert.ToInt32(txtID.Text) + " order by UvozKonacnanhm.ID desc ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView10.ReadOnly = true;
            dataGridView10.DataSource = ds.Tables[0];


            dataGridView10.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridView10.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView10.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView10.DefaultCellStyle.SelectionBackColor = Color.DarkGray;
            dataGridView10.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView10.BackgroundColor = Color.White;

            dataGridView10.EnableHeadersVisualStyles = false;
            dataGridView10.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView10.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 54);
            dataGridView10.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(240, 240, 248); ;

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView10.Columns[0];
            dataGridView10.Columns[0].HeaderText = "ID";
            dataGridView10.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView10.Columns[1];
            dataGridView10.Columns[1].HeaderText = "Broj";
            dataGridView10.Columns[1].Width = 100;

            DataGridViewColumn column3 = dataGridView10.Columns[2];
            dataGridView10.Columns[2].HeaderText = "ID";
            dataGridView10.Columns[2].Width = 20;

            DataGridViewColumn column4 = dataGridView10.Columns[3];
            dataGridView10.Columns[3].HeaderText = "NHM";
            dataGridView10.Columns[3].Width = 400;



        }
        private void FillDGUvoznici()
        {
            if (txtID.Text == "")
            {
                txtID.Text = "0";
            }
            var select = " SELECT     UvozUvoznici.ID, Naziv  FROM UvozUvoznici where UvozUvoznici.idnadredjena = " + Convert.ToInt32(txtID.Text) + " order by UvozUvoznici.ID desc ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new System.Data.DataSet();
            da.Fill(ds);
            dataGridView9.ReadOnly = true;
            dataGridView9.DataSource = ds.Tables[0];


            dataGridView9.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridView9.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView9.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView9.DefaultCellStyle.SelectionBackColor = Color.DarkGray;
            dataGridView9.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView9.BackgroundColor = Color.White;

            dataGridView9.EnableHeadersVisualStyles = false;
            dataGridView9.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView9.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 54);
            dataGridView9.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(240, 240, 248); ;

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView9.Columns[0];
            dataGridView9.Columns[0].HeaderText = "ID";
            dataGridView9.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView9.Columns[1];
            dataGridView9.Columns[1].HeaderText = "Naziv";
            dataGridView9.Columns[1].Width = 400;





        }

        private void button26_Click(object sender, EventArgs e)
        {
            int TcbBrod = Convert.ToInt32(cbBrod.SelectedValue);
            int TtxtTipKont = Convert.ToInt32(txtTipKont.SelectedValue);
            int TcboRLTerminal = Convert.ToInt32(cboRLTerminal.SelectedValue);
            int TcbDirigacija = Convert.ToInt32(cbDirigacija.SelectedValue);
            int TtxtADR = Convert.ToInt32(txtADR.SelectedValue);
            int TcboBrodar = Convert.ToInt32(cboBrodar.SelectedValue);
            int TcbVlasnikKont = Convert.ToInt32(cbVlasnikKont.SelectedValue);
            int TcboNalogodavac1 = Convert.ToInt32(cboNalogodavac1.SelectedValue);
            int TcboNalogodavac2 = Convert.ToInt32(cboNalogodavac2.SelectedValue);
            int TcboNalogodavac3 = Convert.ToInt32(cboNalogodavac3.SelectedValue);
            int TcboUvoznik = Convert.ToInt32(cboUvoznik.SelectedValue);
            int TtxtVrstaPregleda = Convert.ToInt32(txtVrstaPregleda.SelectedValue);
            int TcboSpedicijaG = Convert.ToInt32(cboSpedicijaG.SelectedValue);
            int TcboSpedicijaRTC = Convert.ToInt32(cboSpedicijaRTC.SelectedValue);
            int TcboCarinskiPostupak = Convert.ToInt32(cboCarinskiPostupak.SelectedValue);
            int TcbPostupak = Convert.ToInt32(cbPostupak.SelectedValue);
            int TcbNacinPakovanja = Convert.ToInt32(cbNacinPakovanja.SelectedValue);
            int TcbOspedicija = Convert.ToInt32(cbOspedicija.SelectedValue);
            int TcbOcarina = Convert.ToInt32(cbOcarina.SelectedValue);
            int TtxtMesto = Convert.ToInt32(txtMesto.SelectedValue);
            int TtxtAdresaMestaUtovara = Convert.ToInt32(txtAdresaMestaUtovara.SelectedValue);
            FillCombo();
            cbBrod.SelectedValue = TcbBrod;
            txtTipKont.SelectedValue = TtxtTipKont;
            cboRLTerminal.SelectedValue = TcboRLTerminal;
            cbDirigacija.SelectedValue = TcbDirigacija;
            txtADR.SelectedValue = TtxtADR;
            cboBrodar.SelectedValue = TcboBrodar;
            cbVlasnikKont.SelectedValue = TcbVlasnikKont;
            cboNalogodavac1.SelectedValue = TcboNalogodavac1;
            cboNalogodavac2.SelectedValue = TcboNalogodavac2;
            cboNalogodavac3.SelectedValue = TcboNalogodavac3;
            cboUvoznik.SelectedValue = TcboUvoznik;
            txtVrstaPregleda.SelectedValue = TtxtVrstaPregleda;
            cboSpedicijaG.SelectedValue = TcboSpedicijaG;
            cboSpedicijaRTC.SelectedValue = TcboSpedicijaRTC;
            cboCarinskiPostupak.SelectedValue = TcboCarinskiPostupak;
            cbPostupak.SelectedValue = TcbPostupak;
            cbNacinPakovanja.SelectedValue = TcbNacinPakovanja;
            cbOspedicija.SelectedValue = TcbOspedicija;
            cbOcarina.SelectedValue = TcbOcarina;
            txtMesto.SelectedValue = TtxtMesto;
            txtAdresaMestaUtovara.SelectedValue = TtxtAdresaMestaUtovara;
        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (chkTerminalski.Checked == false)
            {
                DialogResult dialogResult = MessageBox.Show("Pokrenuli ste proceduru pravljenja naloga za službu terminal, nalozi se neće izdati za Administrativne usluge i za zapise koje nemaju unet Broj kontejnera", "Radni nalog", MessageBoxButtons.YesNo);
                int PostojeRn = 0;
                PostojeRn = VratiPostojeceRN();
                if (dialogResult == DialogResult.Yes)
                {

                    InsertRadniNalogInterni ins = new InsertRadniNalogInterni();
                    ins.InsRadniNalogInterni(Convert.ToInt32(1), Convert.ToInt32(4), Convert.ToDateTime(DateTime.Now), Convert.ToDateTime("1.1.1900. 00:00:00"), "", Convert.ToInt32(0), "PlanUtovara", Convert.ToInt32(txtNadredjeni.Text), KorisnikTekuci, "");

                }
                else
                {
                    // FormirajOpstiExcel();
                }
            }

            ///Ako je plan Terminalski onda izdajemi terminalske naloge
            ///
            if (chkTerminalski.Checked == true)
            {
                DialogResult dialogResult = MessageBox.Show("Pokrenuli ste proceduru pravljenja Terminalskog naloga ", "Radni nalog", MessageBoxButtons.YesNo);
                int PostojeRn = 0;
                PostojeRn = VratiPostojeceRN();
                if (dialogResult == DialogResult.Yes)
                {

                    InsertRadniNalogInterni ins = new InsertRadniNalogInterni();
                    ins.InsRadniNalogInterni(Convert.ToInt32(4), Convert.ToInt32(4), Convert.ToDateTime(DateTime.Now), Convert.ToDateTime("1.1.1900. 00:00:00"), "", Convert.ToInt32(0), "PlanUtovara", Convert.ToInt32(txtNadredjeni.Text), KorisnikTekuci, "");

                }
                else
                {
                    // FormirajOpstiExcel();
                }
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            frmFormiranjePlana fplan = new frmFormiranjePlana(Convert.ToInt32(txtNadredjeni.Text));
            fplan.Show();
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void button30_Click(object sender, EventArgs e)
        {
            ExportToHZ();
        }

        private void OpstiInterni(int Interni)
        {
            SqlConnection conn = new SqlConnection(connection);
            /*
          
            */
            if (Interni == 1)
            {
                switch (NHMObrni)
                {
                    case 0:
                        {
                            var nhm = "Select ID,Rtrim(Broj) + '-' + (Rtrim(Naziv)) as Naziv from NHM where Interni = 1 order by NHM.Broj";
                            var nhmSAD = new SqlDataAdapter(nhm, conn);
                            var nhmSDS = new DataSet();
                            nhmSAD.Fill(nhmSDS);
                            cboNHM.DataSource = nhmSDS.Tables[0];
                            cboNHM.DisplayMember = "Naziv";
                            cboNHM.ValueMember = "ID";
                            NHMObrni = 0;
                            break;

                        }
                    case 1:
                        {
                            var nhm = "Select ID,Rtrim(Naziv) + '-' + (Rtrim(Broj)) as Naziv from NHM where Interni = 1 order by NHM.Naziv";
                            var nhmSAD = new SqlDataAdapter(nhm, conn);
                            var nhmSDS = new DataSet();
                            nhmSAD.Fill(nhmSDS);
                            cboNHM.DataSource = nhmSDS.Tables[0];
                            cboNHM.DisplayMember = "Naziv";
                            cboNHM.ValueMember = "ID";
                            NHMObrni = 1;
                            break;
                        }
                    default:
                        break;
                }
            }
            else
            {
                switch (NHMObrni)
                {
                    case 0:
                        {
                            var nhm = "Select ID,Rtrim(Broj) + '-' + (Rtrim(Naziv)) as Naziv from NHM order by NHM.Broj";
                            var nhmSAD = new SqlDataAdapter(nhm, conn);
                            var nhmSDS = new DataSet();
                            nhmSAD.Fill(nhmSDS);
                            cboNHM.DataSource = nhmSDS.Tables[0];
                            cboNHM.DisplayMember = "Naziv";
                            cboNHM.ValueMember = "ID";
                            NHMObrni = 0;
                            break;

                        }
                    case 1:
                        {
                            var nhm = "Select ID,Rtrim(Naziv) + '-' + (Rtrim(Broj)) as Naziv from NHM order by NHM.Naziv";
                            var nhmSAD = new SqlDataAdapter(nhm, conn);
                            var nhmSDS = new DataSet();
                            nhmSAD.Fill(nhmSDS);
                            cboNHM.DataSource = nhmSDS.Tables[0];
                            cboNHM.DisplayMember = "Naziv";
                            cboNHM.ValueMember = "ID";
                            NHMObrni = 1;
                            break;
                        }
                    default:
                        break;
                }
            }




        }

        int OpstiProm = 0;
        int InterniProm = 0;
        int PrviPut = 0;
        int NeRadiOpsti = 0;
        int NeRadiInterni = 0;

        private void chkInterni_CheckedChanged(object sender, EventArgs e)
        {
            if (InterniProm == 0 || NeRadiInterni == 1)
            {
                if (chkInterni.Checked == true)
                {
                    OpstiInterni(1);
                }
                else
                {
                    OpstiInterni(0);

                }


            }
            if (PrviPut == 0)
            {
                PrviPut = 1;
                NeRadiOpsti = 1;
            }

            if (chkInterni.Checked == true)
            {
                chkOpsti.Checked = false;
            }
            else
            {
                chkOpsti.Checked = true;

            }


            InterniProm = 0;
            OpstiProm = 1;
        }

        private void chkOpsti_CheckedChanged(object sender, EventArgs e)
        {
            if (OpstiProm == 0 || NeRadiOpsti == 1)
            {
                if (chkOpsti.Checked == true)
                {
                    OpstiInterni(0);
                }
                else
                {
                    OpstiInterni(1);

                }

            }
            if (PrviPut == 0)
            {
                PrviPut = 1;
                NeRadiInterni = 1;
            }

            if (chkOpsti.Checked == true)
            {
                chkInterni.Checked = false;
            }
            else
            {
                chkInterni.Checked = true;

            }

            InterniProm = 1;
            OpstiProm = 0;
        }

        private void button31_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connection);

            var partner5 = "select Distinct PaKOsifra, PaNaziv from partnerjiKontOseba inner join Partnerji on Partnerji.PaSifra = PaKOsifra where Carinarnica = " + Convert.ToInt32(cbOcarina.SelectedValue);
            var partAD5 = new SqlDataAdapter(partner5, conn);
            var partDS5 = new DataSet();
            partAD5.Fill(partDS5);
            cbOspedicija.DataSource = partDS5.Tables[0];
            cbOspedicija.DisplayMember = "PaNaziv";
            cbOspedicija.ValueMember = "PaKOsifra";
        }

        private void chkCekaSeCarina_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCekaSeCarina.Checked == true)
            {
                chkUradilaCarina.Checked = false;
            }
            else
            {
                chkUradilaCarina.Checked = true;
            }
        }

        private void chkUradilaCarina_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUradilaCarina.Checked == true)
            {
                chkCekaSeCarina.Checked = false;
            }
            else
            {
                chkCekaSeCarina.Checked = true;
            }
        }

        private void chkCekaSeKlijent_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCekaSeKlijent.Checked == true)
            {
                chkPotvrdioKlijent2BDI.Checked = false;
                chkPotvrdioKlijent.Checked = false;
            }
        }

        private void chkPotvrdioKlijent2BDI_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPotvrdioKlijent2BDI.Checked == true)
            {
                chkCekaSeKlijent.Checked = false;
                chkPotvrdioKlijent.Checked = false;
            }
        }

        private void chkPotvrdioKlijent_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPotvrdioKlijent.Checked == true)
            {
                chkCekaSeKlijent.Checked = false;
                chkPotvrdioKlijent2BDI.Checked = false;
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            UvozNHM uNHM = new UvozNHM(txtID.Text, 1);
            uNHM.Show();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            frmUvozUvoznici uuv = new frmUvozUvoznici(txtID.Text,1);
            uuv.Show();
        }

        private void tabSplitterPage1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gridGroupingControl1_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            try
            {
                if (gridGroupingControl1.Table.CurrentRecord != null)
                {
                    txtID.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();

                }
                VratiPodatkeSelect(Convert.ToInt32(txtID.Text));
                VratiPodatkeSelect(Convert.ToInt32(txtID.Text));

                RefreshScenario();
                FillDG2();
                FillDG8();
                FillDG4();
                FillDGUvoznici();
                FillDG2Konacna();
            }



            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

