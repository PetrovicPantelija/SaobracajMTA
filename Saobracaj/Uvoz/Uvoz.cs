
using Microsoft.Office.Interop.Excel;
using Saobracaj.Sifarnici;
using Syncfusion.GridHelperClasses;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Diagram;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using static Syncfusion.WinForms.Core.NativeScroll;

namespace Saobracaj.Uvoz
{
    public partial class Uvoz  : Form
    {
        //Syncfusion.Windows.Forms.Office2010Form
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        string nalogodavci = "";
        string usluge = "";
        int NHMObrni = 0;
        DataSet nhmSDSA;
        DataSet nhmSDS2A;

        float firstWidth;
        float firstHeight;
        string KorisnikTekuci = "";
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


                foreach (Control control in this.Controls)
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
            else
            {
                panelHeader.Visible = false;
                meniHeader.Visible = true;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        public Uvoz()
        {



            InitializeComponent();
            this.BackColor = Color.White;
            this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
            this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
          //  Office2010Colors.ApplyManagedColors(this, Color.White);
            // Syncfusion.Windows.Forms.Office2010C Office2010Form.Style.TitleBar.BackColor = Color.Black;
            //  this.Font = new System.Drawing.Font("Helvetica", 14, System.Drawing.FontStyle.Regular);
            Office2010Colors.ApplyManagedColors(this, Color.White);
            
            meniHeader.Visible = false;

            ChangeTextBox();
            //   this.AutoScroll = true;
            //  FillGV();
            //  FillCheck();
            //  UcitajNHMoveCombo();
            FillCombo();
            // RefreshDataGridColor();
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
            //cboUvoznik.GotFocus += (sender, args) => cboUvoznik. = true;
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

        public Uvoz(int sifra, string Korisnik)
        {
            InitializeComponent();
            this.BackColor = Color.White;
            Office2010Colors.ApplyManagedColors(this, Color.White);
          //  this.Font = new System.Drawing.Font("Helvetica", 14, System.Drawing.FontStyle.Regular);
            meniHeader.Visible = false;
            //   this.AutoScroll = true;
            //  FillGV();
            //  FillCheck();
            //  UcitajNHMoveCombo();
            FillCombo();

            VratiPodatke(sifra);
            FillDG2();
            FillDG3();
            FillDG4();
            FillNHM();
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
            //cboUvoznik.GotFocus += (sender, args) => cboUvoznik.DroppedDown = true;
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
            //  RefreshDataGridColor();
        }

        public Uvoz(int Terminalski, int Plan)
        {
          //  this.AutoScroll = true;
            InitializeComponent();
            this.BackColor = Color.White;
            Office2010Colors.ApplyManagedColors(this, Color.White);
        //   this.Font = new System.Drawing.Font("Helvetica", 14, System.Drawing.FontStyle.Regular);
            meniHeader.Visible = false;
            FillCombo();
            cboPlanUtovara.SelectedValue = Plan;
            chkTerminalski.Checked = true;
            this.Text = "Primljeni kontejneri od strane terminala";
            this.cbBrod.GotFocus += (sender, args) => cbBrod.DroppedDown = true;
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
            //cboUvoznik.GotFocus += (sender, args) => cboUvoznik.ShowPopupAsync = true;
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
            // cboScenario.SelectedValue = 15;
            
            //  RefreshDataGridColor();
        }


        private void UcitajNHMoveCombo()

        {
            SqlConnection conn = new SqlConnection(connection);
            var nhm = "Select ID,(Rtrim(Naziv) + '-' + Rtrim(Broj)) as Naziv2, (Rtrim(Broj) + '-' + Rtrim(Naziv)) as Naziv1 from NHM order by Naziv";
            var nhmSAD = new SqlDataAdapter(nhm, conn);
            nhmSDSA = new DataSet();
            nhmSAD.Fill(nhmSDSA);



            var nhm2 = "Select ID,(Rtrim(Broj) + '-' + Rtrim(NAziv)) as Naziv2, (Rtrim(Broj) + '-' + Rtrim(Naziv)) as Naziv1 from NHM order by Broj";
            var nhmSAD2 = new SqlDataAdapter(nhm2, conn);
            nhmSDS2A = new DataSet();
            nhmSAD2.Fill(nhmSDS2A);
        }
        private void VratiPodatke(int Sifra)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT [ID] " +
      " ,[EtaBroda] ,[AtaBroda] ,[StatusPrijema] ,[BrojKontejnera] " +
      " ,[DobijenNalogBrodara] ,[DobijeBZ] ,[Napomena] ,[PIN] " +
      " ,[DirigacijaKontejeraZa] ,[NazivBroda] ,[BrodskaTeretnica] ,[ADR] " +
      " ,[VlasnikKontejnera] ,[BukingBrodara]      ,[Nalogodavac]      ,[VrstaUsluge] " +
      " ,[Uvoznik]      ,[NHMBroj]      ,[NazivRobe]      ,[SpedicijaGranica] " +
      " ,[SpedicijaRTC]      ,[CarinskiPostupak]      ,[PostupakSaRobom]      ,[NacinPakovanja] " +
      " ,[OdredisnaCarina]      ,[OdredisnaSpedicija]      ,[MestoIstovara]      ,[KontaktOsoba] " +
      " ,[Email]      ,[BrojPlombe1]      ,[BrojPlombe2]      ,[NetoRobe] " +
      " ,[BrutoRobe]      ,[TaraKontejnera]      ,[BrutoKontejnera]      ,[NapomenaZaPozicioniranje] " +
      " ,[AtaOtpreme]      ,[BrojVoza]      ,[RelacijaVoza]      ,[AtaDolazak] " +
      " ,[TipKontejnera]      ,[Koleta], RLTerminali, RLTerminali2, RLTerminali3," +
      " Napomena1,VrstaPregleda,Nalogodavac1 ,Ref1 ,Nalogodavac2,Ref2 ,Nalogodavac3 ,Ref3 ,Brodar, NaslovStatusaVozila " +
  " FROM [Uvoz] where ID=" + Sifra, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtID.Text = dr["ID"].ToString();
                dtEtaRijeka.Value = Convert.ToDateTime(dr["EtaBroda"].ToString());
                dtAtaRijeka.Value = Convert.ToDateTime(dr["AtaBroda"].ToString());
                txtStatus.Text = dr["StatusPrijema"].ToString();
                txtBrKont.Text = dr["BrojKontejnera"].ToString();
                txtTipKont.SelectedValue = Convert.ToInt32(dr["TipKontejnera"].ToString());
                dtNalogBrodara.Value = Convert.ToDateTime(dr["DobijenNalogBrodara"].ToString());
                txtBZ.Value = Convert.ToDateTime(dr["DobijeBZ"].ToString());
                txtPIN.Text = dr["PIN"].ToString();
                cbDirigacija.SelectedValue = Convert.ToInt32(dr["DirigacijaKontejeraZa"].ToString());
                cbBrod.SelectedValue = Convert.ToInt32(dr["NazivBroda"].ToString());
                txtTeretnica.Text = dr["BrodskaTeretnica"].ToString();
                txtADR.SelectedValue = Convert.ToInt32(dr["ADR"].ToString());
                cbVlasnikKont.SelectedValue = Convert.ToInt32(dr["VlasnikKontejnera"].ToString());
                txtBuking.Text = dr["BukingBrodara"].ToString();
                cboUvoznik.SelectedValue = Convert.ToInt32(dr["Uvoznik"].ToString());
                cboSpedicijaG.SelectedValue = Convert.ToInt32(dr["SpedicijaGranica"].ToString());
                //cboNHM
                // cboNazivRobe
                cboSpedicijaRTC.SelectedValue = Convert.ToInt32(dr["SpedicijaRTC"].ToString());
                cboCarinskiPostupak.SelectedValue = Convert.ToInt32(dr["CarinskiPostupak"].ToString());
                cbNacinPakovanja.SelectedValue = Convert.ToInt32(dr["NacinPakovanja"].ToString());
                cbOcarina.SelectedValue = Convert.ToInt32(dr["OdredisnaCarina"].ToString());
                cbOspedicija.SelectedValue = Convert.ToInt32(dr["OdredisnaSpedicija"].ToString());
                txtMesto.SelectedValue = Convert.ToInt32(dr["MestoIstovara"].ToString());
                txtKontaktOsoba.SelectedValue = Convert.ToInt32(dr["KontaktOsoba"].ToString());
                cbPostupak.SelectedValue = Convert.ToInt32(dr["PostupakSaRobom"].ToString());
                txtPlomba1.Text = dr["BrojPlombe1"].ToString();
                txtPlomba2.Text = dr["BrojPlombe2"].ToString();

                txtMail.Text = dr["Email"].ToString();
                dtAtaOtprema.Value = Convert.ToDateTime(dr["AtaOtpreme"].ToString());
                txtRelacija.Text = dr["RelacijaVoza"].ToString();
                txtBrojVoza.Text = dr["BrojVoza"].ToString();
                dtAtaDolazak.Value = Convert.ToDateTime(dr["AtaOtpreme"].ToString());
                txtKoleta.Value = Convert.ToDecimal(dr["Koleta"].ToString());
                txtNetoR.Value = Convert.ToDecimal(dr["NetoRobe"].ToString());
                txtBrutoR.Value = Convert.ToDecimal(dr["BrutoRobe"].ToString());
                txtTaraK.Value = Convert.ToDecimal(dr["TaraKontejnera"].ToString());
                txtBrutoK.Value = Convert.ToDecimal(dr["BrutoKontejnera"].ToString());
                txtNapomena.Text = dr["Napomena"].ToString();
                cbNapomenaPoz.SelectedValue = Convert.ToInt32(dr["NapomenaZaPozicioniranje"].ToString());
                //Panta

                cboRLTerminal.SelectedValue = Convert.ToInt32(dr["RLTerminali"].ToString());
                cboRLTerminal2.SelectedValue = Convert.ToInt32(dr["RLTerminali2"].ToString());
                cboRLTerminal3.SelectedValue = Convert.ToInt32(dr["RLTerminali3"].ToString());

                cboBrodar.SelectedValue = Convert.ToInt32(dr["Brodar"].ToString());
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
                cboNaslovStatusaVozila.Text = dr["NaslovStatusaVozila"].ToString();
            }
            con.Close();


        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Brodovi brod = new Brodovi();
            brod.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Carinarnice car = new Carinarnice();
            car.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Nalogodavci nal = new Nalogodavci();
            nal.Show();
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connection);
                SqlCommand cmd = new SqlCommand("Insert into Uvoz Default Values", conn);
                conn.Open();
                var q = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            GetID();
            tsNew.Enabled = false;
            txtBrKont.Text = "";

        }
        private void GetID()
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand("Select MAX(ID) FROM Uvoz", conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    int IDpom = Convert.ToInt32(dr[0].ToString());
                    txtID.Text = IDpom.ToString();
                }
            }
        }
        private void FillGV()
        {



            var select = "SELECT Uvoz.ID, [BrojKontejnera], BrodskaTeretnica as BL, DobijenNalogBrodara as Dobijen_Nalog_Brodara ,ATABroda, Brodovi.Naziv as Brod,Napomena1 as Napomena1, " +
" DobijeBZ as DatumBZ ,PIN,  [BrojKontejnera], TipKontenjera.Naziv as Vrsta_Kontejnera,  KontejnerskiTerminali.Naziv as R_L_SRB, pp1.Naziv as Dirigacija_Kontejnera_Za,  " +
" BrodskaTeretnica, VrstaRobeADR.Naziv as ADR, b.PaNaziv as Brodar,pv.PaNaziv as VlasnikKontejnera, n1.PaNaziv as Nalogodavac1, Ref1 as Ref1, n2.PaNaziv as Nalogodavac2, Ref2 as Ref2, " +
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
" VrstaPregleda as InsTret,p2.PaNaziv as SpedicijaRTC,  p3.PaNaziv as SpedicijaGranica,       VrstaCarinskogPostupka.Naziv as CarinskiPostupak,  " +
" VrstePostupakaUvoz.Naziv as PostupakSaRobom,uvNacinPakovanja.Naziv as NacinPakovanja, Napomena as Napomena2, NaslovStatusaVozila as NaslovZaslanjestatusa, " +
" Carinarnice.Naziv as Carinarnica,  " +
" p4.PaNaziv as OdredisnaSpedicija, MestaUtovara.Naziv as MestoIstovara, (RTRIM(pkoMU.PaKOIme) + ' ' + RTRIM(pkoMU.PaKOPriimek)) as KontaktOsoba, Email,     " +
"   BrojPlombe1, BrojPlombe2,   " +
" ( select STUFF((SELECT distinct    '/' + Cast(PredefinisanePoruke.Naziv as nvarchar(20)) from UvozNapomenePozicioniranja " +
" inner join  PredefinisanePoruke on PredefinisanePoruke.ID = UvozNapomenePozicioniranja.IDNapomene where UvozNapomenePozicioniranja.IdNadredjena = Uvoz.ID " +
" FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as NapomenaZaPozicioniranje, " +
" NetoRobe, BrutoRobe, TaraKontejnera, BrutoKontejnera,  Koleta, green FROM Uvoz left join Partnerji on PaSifra = VlasnikKontejnera " +
" left join Partnerji p1 on p1.PaSifra = Uvoznik  left join Partnerji p2 on p2.PaSifra = SpedicijaRTC  left join Partnerji p3 on p3.PaSifra = SpedicijaGranica " +
" left join TipKontenjera on TipKontenjera.ID = Uvoz.TipKontejnera " +
" left join Carinarnice on Carinarnice.ID = Uvoz.OdredisnaCarina  left join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = Uvoz.CarinskiPostupak " +
" left join Predefinisaneporuke on PredefinisanePoruke.ID = Uvoz.NapomenaZaPozicioniranje " +
"  left join KontejnerskiTerminali on KontejnerskiTerminali.ID = Uvoz.RLTErminali " +
" left join Partnerji n1 on n1.PaSifra = Nalogodavac1   left join Partnerji n2 on n2.PaSifra = Nalogodavac2   left join Partnerji n3 on n3.PaSifra = Nalogodavac3 " +
" left join Partnerji b on b.PaSifra = Uvoz.Brodar  left join  DirigacijaKontejneraZa pp1 on pp1.ID = Uvoz.DirigacijaKontejeraZa " +
" left join Brodovi on Brodovi.ID = Uvoz.NazivBroda    left join VrstaRobeADR on VrstaRobeADR.ID = ADR " +
" left join VrstePostupakaUvoz on VrstePostupakaUvoz.ID = PostupakSaRobom " +
" left join MestaUtovara on Uvoz.MestoIstovara = MestaUtovara.ID " +
" left join partnerjiKontOsebaMU on Uvoz.KontaktOsoba = partnerjiKontOsebaMU.PaKOSifra " +
" left join uvNacinPakovanja on uvNacinPakovanja.ID = NacinPakovanja  left join Partnerji p4 on p4.PaSifra = OdredisnaSpedicija " +
" inner join Partnerji pv on pv.PaSifra = Uvoz.VlasnikKontejnera " +
" inner join partnerjiKontOsebaMU pkoMU on pkoMU.PaKOZapSt = Uvoz.KontaktOsoba " +
                " order by Uvoz.ID desc ";

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

        private void RefreshDataGridColor()
        {

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {


                if (row.Cells[46].Value.ToString() == "1")
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
        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connection);

            var dir = "Select ID,Naziv from DirigacijaKontejneraZa order by Naziv";
            var dirAD = new SqlDataAdapter(dir, conn);
            var dirDS = new DataSet();
            dirAD.Fill(dirDS);
            cbDirigacija.DataSource = dirDS.Tables[0];
            cbDirigacija.DisplayMember = "Naziv";
            cbDirigacija.ValueMember = "ID";
            //carinski postupak
            var dir2 = "Select ID, (Oznaka + ' ' + Naziv) as Naziv from VrstaCarinskogPostupka order by Naziv";
            var dirAD2 = new SqlDataAdapter(dir2, conn);
            var dirDS2 = new DataSet();
            dirAD2.Fill(dirDS2);
            cboCarinskiPostupak.DataSource = dirDS2.Tables[0];
            cboCarinskiPostupak.DisplayMember = "Naziv";
            cboCarinskiPostupak.ValueMember = "ID";
            //postupak roba/kont
            var dir3 = "Select ID,Naziv from VrstePostupakaUvoz order by Naziv";
            var dirAD3 = new SqlDataAdapter(dir3, conn);
            var dirDS3 = new DataSet();
            dirAD3.Fill(dirDS3);
            cbPostupak.DataSource = dirDS3.Tables[0];
            cbPostupak.DisplayMember = "Naziv";
            cbPostupak.ValueMember = "ID";
            //nacin pakovanja
            var dir4 = "Select ID,(Oznaka + ' ' + Naziv) as Naziv from uvNacinPakovanja order by Naziv";
            var dirAD4 = new SqlDataAdapter(dir4, conn);
            var dirDS4 = new DataSet();
            dirAD4.Fill(dirDS4);
            cbNacinPakovanja.DataSource = dirDS4.Tables[0];
            cbNacinPakovanja.DisplayMember = "Naziv";
            cbNacinPakovanja.ValueMember = "ID";
            //napomena pozicioniranje
            var dir5 = "Select ID,Naziv from NapomenaZaPozicioniranje order by Naziv";
            var dirAD5 = new SqlDataAdapter(dir5, conn);
            var dirDS5 = new DataSet();
            dirAD5.Fill(dirDS5);
            cbNapomenaPoz.DataSource = dirDS5.Tables[0];
            cbNapomenaPoz.DisplayMember = "Naziv";
            cbNapomenaPoz.ValueMember = "ID";

            var brod = "Select ID,Naziv From Brodovi order by Naziv";
            var brodAD = new SqlDataAdapter(brod, conn);
            var brodDS = new DataSet();
            brodAD.Fill(brodDS);
            cbBrod.DataSource = brodDS.Tables[0];
            cbBrod.DisplayMember = "Naziv";
            cbBrod.ValueMember = "ID";

            var car = "Select ID, Naziv From Carinarnice order by Naziv";
            var carAD = new SqlDataAdapter(car, conn);
            var carDS = new DataSet();
            carAD.Fill(carDS);
            cbOcarina.DataSource = carDS.Tables[0];
            cbOcarina.DisplayMember = "Naziv";
            cbOcarina.ValueMember = "ID";

            //where Vlasnik = 1
            var partner = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var partAD = new SqlDataAdapter(partner, conn);
            var partDS = new DataSet();
            partAD.Fill(partDS);
            cbVlasnikKont.DataSource = partDS.Tables[0];
            cbVlasnikKont.DisplayMember = "PaNaziv";
            cbVlasnikKont.ValueMember = "PaSifra";
            //uvoznik
            
            var partner2 = "Select PaSifra,(Rtrim(PaNaziv) + '-' + RTRIM(ISNULL(PaDmatst, ''))) as PaNaziv From Partnerji  order by PaNaziv";
            var partAD2 = new SqlDataAdapter(partner2, conn);
            var partDS2 = new DataSet();
            partAD2.Fill(partDS2);
            cboUvoznik.DataSource = partDS2.Tables[0];
            cboUvoznik.DisplayMember = "PaNaziv";
            cboUvoznik.ValueMember = "PaSifra";


            //Igranka
        
          //  cboUvoznik.DisplayMember = "PaNaziv";
           // cboUvoznik.ValueMember = "PaSifra";

            //
            //spedicija na granici

            var partner3 = "Select PaSifra,PaNaziv From Partnerji where  Spediter = 1 order by PaNaziv";
            var partAD3 = new SqlDataAdapter(partner3, conn);
            var partDS3 = new DataSet();
            partAD3.Fill(partDS3);
            cboSpedicijaG.DataSource = partDS3.Tables[0];
            cboSpedicijaG.DisplayMember = "PaNaziv";
            cboSpedicijaG.ValueMember = "PaSifra";
            //spedicija rtc luka leget

            var partner4 = "Select PaSifra,PaNaziv From Partnerji  where Spediter = 1 order by PaNaziv";
            var partAD4 = new SqlDataAdapter(partner4, conn);
            var partDS4 = new DataSet();
            partAD4.Fill(partDS4);
            cboSpedicijaRTC.DataSource = partDS4.Tables[0];
            cboSpedicijaRTC.DisplayMember = "PaNaziv";
            cboSpedicijaRTC.ValueMember = "PaSifra";
            //odredisna spedicija
            var partner5 = "Select PaSifra,PaNaziv From Partnerji where Spediter = 1 order by PaNaziv";
            var partAD5 = new SqlDataAdapter(partner5, conn);
            var partDS5 = new DataSet();
            partAD5.Fill(partDS5);
            cbOspedicija.DataSource = partDS5.Tables[0];
            cbOspedicija.DisplayMember = "PaNaziv";
            cbOspedicija.ValueMember = "PaSifra";

            var tipkontejnera = "Select ID, SkNaziv From TipKontenjera order by SkNaziv";
            var tkAD = new SqlDataAdapter(tipkontejnera, conn);
            var tkDS = new DataSet();
            tkAD.Fill(tkDS);
            txtTipKont.DataSource = tkDS.Tables[0];
            txtTipKont.DisplayMember = "SkNaziv";
            txtTipKont.ValueMember = "ID";



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





            //OVde mi trebaju terminalski planovi utovara

            var planutovara = "select top 1 UvozKonacnaZaglavlje.ID,(Cast(BrVoza as nvarchar(15)) + ' '  + Relacija) as Naziv from UvozKonacnaZaglavlje " +
            " inner join Voz on Voz.Id = UvozKonacnaZaglavlje.IdVoza where  UvozKonacnaZaglavlje.Terminal = 1 order by UvozKonacnaZaglavlje.ID desc";
            var planutovaraSAD = new SqlDataAdapter(planutovara, conn);
            var planutovaraSDS = new DataSet();
            planutovaraSAD.Fill(planutovaraSDS);
            cboPlanUtovara.DataSource = planutovaraSDS.Tables[0];
            cboPlanUtovara.DisplayMember = "Naziv";
            cboPlanUtovara.ValueMember = "ID";

            var adr = "Select ID, (Naziv + ' - ' + UNKod) as Naziv From VrstaRobeADR order by (UNKod + ' ' + Naziv)";
            var adrSAD = new SqlDataAdapter(adr, conn);
            var adrSDS = new DataSet();
            adrSAD.Fill(adrSDS);
            txtADR.DataSource = adrSDS.Tables[0];
            txtADR.DisplayMember = "Naziv";
            txtADR.ValueMember = "ID";


            //Kontejnerski terminali
            var rl = "Select ID, (Naziv) as Naziv From KontejnerskiTerminali order by (Naziv + ' ' + Oznaka)";
            var rlSAD = new SqlDataAdapter(rl, conn);
            var rlSDS = new DataSet();
            rlSAD.Fill(rlSDS);
            cboRLTerminal.DataSource = rlSDS.Tables[0];
            cboRLTerminal.DisplayMember = "Naziv";
            cboRLTerminal.ValueMember = "ID";

            //Kontejnerski terminali 2
            var rl2 = "Select ID, (Naziv) as Naziv From KontejnerskiTerminali order by (Naziv + ' ' + Oznaka)";
            var rlSAD2 = new SqlDataAdapter(rl2, conn);
            var rlSDS2 = new DataSet();
            rlSAD2.Fill(rlSDS2);
            cboRLTerminal2.DataSource = rlSDS2.Tables[0];
            cboRLTerminal2.DisplayMember = "Naziv";
            cboRLTerminal2.ValueMember = "ID";

            var rl3 = "Select ID, (Naziv) as Naziv From KontejnerskiTerminali order by (Naziv + ' ' + Oznaka)";
            var rlSAD3 = new SqlDataAdapter(rl3, conn);
            var rlSDS3 = new DataSet();
            rlSAD3.Fill(rlSDS3);
            cboRLTerminal3.DataSource = rlSDS3.Tables[0];
            cboRLTerminal3.DisplayMember = "Naziv";
            cboRLTerminal3.ValueMember = "ID";

            var nalogodavac1 = "Select PaSifra,PaNaziv From Partnerji  order by PaNaziv";
            var nal1AD = new SqlDataAdapter(nalogodavac1, conn);
            var nal1DS = new DataSet();
            nal1AD.Fill(nal1DS);
            cboNalogodavac1.DataSource = nal1DS.Tables[0];
            cboNalogodavac1.DisplayMember = "PaNaziv";
            cboNalogodavac1.ValueMember = "PaSifra";

            var nalogodavac2 = "Select PaSifra,PaNaziv From Partnerji  order by PaNaziv";
            var nal2AD = new SqlDataAdapter(nalogodavac2, conn);
            var nal2DS = new DataSet();
            nal2AD.Fill(nal2DS);
            cboNalogodavac2.DataSource = nal2DS.Tables[0];
            cboNalogodavac2.DisplayMember = "PaNaziv";
            cboNalogodavac2.ValueMember = "PaSifra";

            var nalogodavac3 = "Select PaSifra,PaNaziv From Partnerji  order by PaNaziv";
            var nal3AD = new SqlDataAdapter(nalogodavac3, conn);
            var nal3DS = new DataSet();
            nal3AD.Fill(nal3DS);
            cboNalogodavac3.DataSource = nal3DS.Tables[0];
            cboNalogodavac3.DisplayMember = "PaNaziv";
            cboNalogodavac3.ValueMember = "PaSifra";

            var bro = "Select PaSifra,PaNaziv From Partnerji where Brodar = 1 order by PaNaziv";
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
            var partner22 = "SELECT ID, Rtrim(Min(Naziv)) as Naziv FROM Scenario group by ID order by ID";
            var partAD22 = new SqlDataAdapter(partner22, conn);
            var partDS22 = new DataSet();
            partAD22.Fill(partDS22);
            cboScenario.DataSource = partDS22.Tables[0];
            cboScenario.DisplayMember = "Naziv";
            cboScenario.ValueMember = "ID";

        }
        private void FillCheck()
        {
            var query = "Select PaSifra,PaNaziv From Nalogodavci";
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

       int ProveriImaUsluga(string UvozID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT Count(*)  as Broj" +
        "  FROM [UvozVrstaManipulacije] where IDNAdredjena=" + UvozID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                return Convert.ToInt32(dr["Broj"].ToString());
            }
            con.Close();
            return 0; 

        }

        int ProveriBiloIzmena(string UvozID)
        {
            int stariADR = 0;
            string stariTipKontejnera = "";
            int stariRLTErminali = 0;
            int stariRLTErminali2 = 0;
            int stariRLTErminali3 = 0;
            int stariScenario = 0;

            int noviADR = 0;
            string noviTipKontejnera = "";
            int noviRLTErminali = 0;
            int noviRLTErminali2 = 0;
            int noviRLTErminali3 = 0;
            int noviScenario = 0;

            int doslodopromene = 0;


            int k = ProveriImaUsluga(UvozID);

            if (k == 0 ) 
            {
                MessageBox.Show("Nema unetih usluga");
                return 1; // Kako bi moglo da se sacuva
            }
            else
            {
                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection con = new SqlConnection(s_connection);

                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT [ADR] ," +
                 " substring(TipKontenjera.SkNaziv,1,3) as  TipKontejnera, " +
                 " RLTErminali , RLTErminali2 ,RLTErminali3 , Scenario " +
            "  FROM [Uvoz] inner join TipKontenjera on Uvoz.TipKontejnera = TipKontenjera.ID where Uvoz.ID=" + UvozID, con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    stariADR = Convert.ToInt32(dr["ADR"].ToString());
                    stariTipKontejnera = dr["TipKontejnera"].ToString();
                    stariRLTErminali = Convert.ToInt32(dr["RLTErminali"].ToString());
                    stariRLTErminali2 = Convert.ToInt32(dr["RLTErminali2"].ToString());
                    stariRLTErminali3 = Convert.ToInt32(dr["RLTErminali3"].ToString());
                    stariScenario = Convert.ToInt32(dr["Scenario"].ToString());

                }
                con.Close();
                noviADR = Convert.ToInt32(txtADR.SelectedValue);
                noviTipKontejnera = txtTipKont.Text.Substring(0,3);
                noviRLTErminali = Convert.ToInt32(cboRLTerminal.SelectedValue);

                string promena = "";
                int bilopromene = 0;

                if (stariADR != noviADR)
                {
                    bilopromene = 1;
                    promena = promena + ". ADR" + noviADR;
                    //Proveri da li je ispravan scenario
                    // MoguciScenario();
                    // MessageBox.Show("Doslo je do promene ADR proveriti usluge");
                }

                if (noviTipKontejnera != stariTipKontejnera)
                {
                    bilopromene = 1;
                    promena = promena + " , Tip kontejnera" + noviTipKontejnera;
                    // MessageBox.Show("Doslo je do promene tipa kontejnera proveriti uslugu zeleznine");
                }

                if (noviRLTErminali != stariRLTErminali)
                {
                    bilopromene = 1;
                    promena = promena + " , Relacija" + noviRLTErminali;
                }

                if (bilopromene == 1)
                {

                    MessageBox.Show("Promenjena su polja potrebno je ponovo generisati usluge" + promena);
                    return bilopromene;

                }

                return bilopromene;
            }

            

        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                MessageBox.Show("Kliknite na ikonicu NOVI ili izaberite postojeći kontejner");
            }
            int tDobijenBZ = 0;
            int tPrioritet = 0;
            int Terminalska = 0;

            if (chkDobijenBZ.Checked == true)
            { tDobijenBZ = 1; };
            if (chkPrioritet.Checked == true)
            { tPrioritet = 1; };
            if (chkDobijenBZ.Checked == true)
            { tDobijenBZ = 1; };
            if (chkTerminalski.Checked == true)
            { Terminalska = 1; };
            

            try
            {
                int temp = Convert.ToInt32(txtBrojVoza.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Unesite numeričku vrednost Broja voza");
                return;
            }


            if (txtBuking.Text == "")
            { txtBuking.Text = "0"; }
            else
            {
                try
                {
                    int temp = Convert.ToInt32(txtBuking.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Unesite numeričku vrednost Bukinga");
                    return;
                }
            }
            int PotvrdioKlijent = 0;
            int UradilaCarina = 0;
            if (chkPotvrdioKlijent.Checked == true)
            { PotvrdioKlijent = 1; };
            if (chkCekaSeCarina.Checked == true)
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
            for(int i = 0; i < clVrstaUsluga.Items.Count; i++)
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
            InsertUvoz ins = new InsertUvoz();
            if (chkSlobodan.Checked == true)
            {
                txtNapomena1.Text = "1";
            }
            else
            {
                txtNapomena1.Text = "0";
            }

           

            int i = ProveriBiloIzmena(txtID.Text);

            if (i != 0)
            {
                DialogResult result = MessageBox.Show("Da li želite da sačuvate promene?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    ins.UpdUvoz(Convert.ToInt32(txtID.Text), Convert.ToDateTime(dtEtaRijeka.Value.ToString()),
                Convert.ToDateTime(dtAtaRijeka.Value.ToString()), txtStatus.Text.ToString().TrimEnd(), txtBrKont.Text,
                Convert.ToInt32(txtTipKont.SelectedValue), Convert.ToDateTime(dtNalogBrodara.Value.ToString()), txtBZ.Text.ToString().TrimEnd(),
                txtNapomena.Text.ToString().TrimEnd(), txtPIN.Text.ToString().TrimEnd(), Convert.ToInt32(cbDirigacija.SelectedValue), Convert.ToInt32(cbBrod.SelectedValue),
                txtTeretnica.Text, Convert.ToInt32(txtADR.SelectedValue), Convert.ToInt32(cbVlasnikKont.SelectedValue), Convert.ToInt32(txtBuking.Text), nalogodavci,
                usluge, Convert.ToInt32(cboUvoznik.SelectedValue), Convert.ToInt32(cboNHM.SelectedValue), "", Convert.ToInt32(cboSpedicijaG.SelectedValue),
                Convert.ToInt32(cboSpedicijaRTC.SelectedValue), Convert.ToInt32(cboCarinskiPostupak.SelectedValue), Convert.ToInt32(cbPostupak.SelectedValue),
                Convert.ToInt32(cbNacinPakovanja.SelectedValue), Convert.ToInt32(cbOcarina.SelectedValue), Convert.ToInt32(cbOspedicija.SelectedValue),
                Convert.ToInt32(txtMesto.SelectedValue), Convert.ToInt32(txtKontaktOsoba.SelectedValue), txtMail.Text.ToString(), txtPlomba1.Text,
                txtPlomba2.Text, Convert.ToDecimal(txtNetoR.Value), Convert.ToDecimal(txtBrutoR.Value), Convert.ToDecimal(txtTaraK.Value),
                Convert.ToDecimal(txtBrutoK.Value), Convert.ToInt32(cbNapomenaPoz.SelectedValue), Convert.ToDateTime(dtAtaOtprema.Value.ToString()),
                Convert.ToInt32(txtBrojVoza.Text), txtRelacija.Text.ToString().TrimEnd(), Convert.ToDateTime(dtAtaDolazak.Value.ToString()), Convert.ToDecimal(txtKoleta.Value), Convert.ToInt32(cboRLTerminal.SelectedValue), txtNapomena1.Text, Convert.ToInt32(txtVrstaPregleda.SelectedValue),
                Convert.ToInt32(cboNalogodavac1.SelectedValue), txtRef1.Text,
                Convert.ToInt32(cboNalogodavac2.SelectedValue), txtRef2.Text,
                Convert.ToInt32(cboNalogodavac3.SelectedValue), txtRef3.Text, Convert.ToInt32(cboBrodar.SelectedValue), cboNaslovStatusaVozila.Text, tDobijenBZ, tPrioritet, Convert.ToInt32(txtAdresaMestaUtovara.SelectedValue), txtKontaktOsobe.Text, Terminalska, Convert.ToDecimal(txtTaraTerminal.Value), Convert.ToDecimal(txtKoletaTer.Value), Convert.ToInt32(cboScenario.SelectedValue), Convert.ToInt32(cboRLTerminal2.SelectedValue), Convert.ToInt32(cboRLTerminal3.SelectedValue),
                PotvrdioKlijent, UradilaCarina,
                              TFDobijenNalog, TFDobijenNalogodavac1, Convert.ToDateTime(dtpDobijenNalogodavac1.Value), TFDobijenNalogodavac2, Convert.ToDateTime(dtpDobijenNalogodavac2.Value),
                TFDobijenNalogodavac3, Convert.ToDateTime(dtpDobijenNalogodavac3.Value), TFFCL, TFLCL, Convert.ToDateTime(dtpPotvrdioKlijent.Value), Convert.ToDateTime(dtpSlobodanDaNapusti.Value),0);
                    //  FillGV();
                    //  RefreshDataGridColor();
                    tsNew.Enabled = true;
                }
               
                else
                {
                    return;
                }

            }

           
            // txtID.Text = "";
        }
        /*
        private void ProveriScenario(int ID)
        {
            //Panta 2
            // broj Zapisa za kontejner
            //Izab raniScenario
            int IzabraniScenario = 0;
            int BrojZapisaKontejnera = VratiBrojManipulacija(ID);
            int BSC1 = VratiBrojScenario(1);
            int BSC2 = VratiBrojScenario(2);
            int BSC3 = VratiBrojScenario(3);
            int BSC4 = VratiBrojScenario(4);
            int BSC5 = VratiBrojScenario(5);
            int BSC6 = VratiBrojScenario(6);

            int BSC18 = VratiBrojScenario(18);
            int BSC19 = VratiBrojScenario(19);
            int BSC20 = VratiBrojScenario(20);
            int BSC21 = VratiBrojScenario(21);
            int BSC22 = VratiBrojScenario(22);
          //  int BSC6 = VratiBrojScenario(6);

            int rasporedjen = 0;
            if (txtNadredjeni.Text != "0")
            {
                rasporedjen = 1;
            }
            else
            {
                rasporedjen = 0;
            }
            if (BrojZapisaKontejnera == BSC1)
            {
                IzabraniScenario =  ProveriDaLiSuIsteManipulacije(ID,1);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(1, ID, 1, rasporedjen);
                    System.Windows.Forms.MessageBox.Show("Izabrali ste scenario 1");
                }
            }
            if (BrojZapisaKontejnera == BSC2)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 2);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(2, ID, 1, rasporedjen);
                    System.Windows.Forms.MessageBox.Show("Izabrali ste scenario 2");
                }
            }

            if (BrojZapisaKontejnera == BSC3)   
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 3);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(3, ID, 1, rasporedjen);
                    System.Windows.Forms.MessageBox.Show("Izabrali ste scenario 3");
                }
            }

            if (BrojZapisaKontejnera == BSC4)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 4);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(4, ID, 1, rasporedjen);
                    System.Windows.Forms.MessageBox.Show("Izabrali ste scenario 4");
                }
            }

            if (BrojZapisaKontejnera == BSC5)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 5);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(5, ID, 1, rasporedjen);
                    System.Windows.Forms.MessageBox.Show("Izabrali ste scenario 5");
                }
            }

            if (BrojZapisaKontejnera == BSC6)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 6);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(6, ID, 1, rasporedjen);
                    System.Windows.Forms.MessageBox.Show("Izabrali ste scenario 6");
                }
            }


            if (BrojZapisaKontejnera == BSC18)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 18);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(18, ID, 1, rasporedjen);
                    System.Windows.Forms.MessageBox.Show("Izabrali ste scenario 18");
                }
            }

            if (BrojZapisaKontejnera == BSC19)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 19);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(19, ID, 1, rasporedjen);
                    System.Windows.Forms.MessageBox.Show("Izabrali ste scenario 19");
                }
            }

            if (BrojZapisaKontejnera == BSC20)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 20);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(20, ID, 1, rasporedjen);
                    System.Windows.Forms.MessageBox.Show("Izabrali ste scenario 20");
                }
            }

            if (BrojZapisaKontejnera == BSC21)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 21);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(21, ID, 1, rasporedjen);
                    System.Windows.Forms.MessageBox.Show("Izabrali ste scenario 21");
                }
            }

            if (BrojZapisaKontejnera == BSC22)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 22);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(22, ID, 1, rasporedjen);
                    System.Windows.Forms.MessageBox.Show("Izabrali ste scenario 22");
                }
            }

            // Proveri da li je isti broj 
            // Proveri da lii su iste manipulacije

        }
        */
        private void clNalogodavac_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void VratiPodatkeSelect(int ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
     "  ,[TipKontejnera] ,[Koleta]," +
     " RLTErminali , RLTErminali2 ,RLTErminali3 ,Napomena1 ,VrstaPregleda ,Nalogodavac1 ,Ref1 ,Nalogodavac2 ,Ref2 ,Nalogodavac3 ,Ref3, Brodar, NaslovStatusaVozila, " +
     "Prioritet, DobijenBZ, AdresaMestaUtovara, KontaktOsobe, TaraKontejneraT, KoletaTer, Scenario , PotvrdioKlijent, UradilaCarina," +
     "  [chkDobijenNalogBrodara]     ,[chkDobijenNalogodavac1]      ,[DatumNalogodavac1]     ,[chkDobijenNalogodavac2]     ,[DatumNalogodavac2]     ,[chkDobijenNalogodavac3]  " +
     "   ,[DatumNalogodavac3]      ,[DatumPotvrdioKlijent]     ,[DatumSlobasodanDaNapusti]     ,[FCL]     ,[LCL], DrumskaUsluga" +
 "  FROM [Uvoz] where ID=" + ID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                dtpETAbroda.Value = Convert.ToDateTime(dr["EtaBroda"].ToString());
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
                txtTaraTerminal.Value = Convert.ToDecimal(dr["TaraKontejneraT"].ToString());
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



                /*
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
            */

                //PANTA
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

                if (dr["DrumskaUsluga"].ToString() == "1")
                {
                    chkDrumskaUsluga.Checked = true;
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

            select = "select  UvozVrstaManipulacije.ID as ID, UvozVrstaManipulacije.IDNadredjena as KontejnerID, Uvoz.BrojKontejnera, " +
" UvozVrstaManipulacije.Kolicina,  VrstaManipulacije.ID as ManipulacijaID,VrstaManipulacije.Naziv as ManipulacijaNaziv, " +
" UvozVrstaManipulacije.Cena,OrganizacioneJedinice.ID,   OrganizacioneJedinice.Naziv as OrganizacionaJedinica,  " +
" Partnerji.PaSifra as NalogodavacID,PArtnerji.PaNaziv as Platilac, SaPDV " +
" from UvozVrstaManipulacije " +
" Inner    join VrstaManipulacije on VrstaManipulacije.ID = UvozVrstaManipulacije.IDVrstaManipulacije " +
" inner " +
" join PArtnerji on UvozVrstaManipulacije.Platilac = PArtnerji.PaSifra " +
" inner " +
" join OrganizacioneJedinice on OrganizacioneJedinice.ID = UvozVrstaManipulacije.OrgJed " +
" inner " +
" join Uvoz on UvozVrstaManipulacije.IDNadredjena = Uvoz.ID where Uvoz.ID = " + Convert.ToInt32(txtID.Text);



            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView7.ReadOnly = true;
            dataGridView7.DataSource = ds.Tables[0];


            dataGridView7.BorderStyle = BorderStyle.None;
            
            // 
            
            dataGridView7.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView7.DefaultCellStyle.SelectionBackColor = Color.FromArgb(90, 199, 249); // Selektovana boja
            dataGridView7.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView7.BackgroundColor = Color.White;
            dataGridView7.DefaultCellStyle.ForeColor  = Color.FromArgb(51, 51, 54);
            dataGridView7.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);
            dataGridView7.RowHeadersDefaultCellStyle.BackColor  = Color.FromArgb(240, 240, 248);


            //Header
            dataGridView7.EnableHeadersVisualStyles = false;
            dataGridView7.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView7.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 54);
            dataGridView7.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView7.Columns[0];
            dataGridView7.Columns[0].HeaderText = "ID";
            dataGridView7.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView7.Columns[1];
            dataGridView7.Columns[1].HeaderText = "IDU";
            dataGridView7.Columns[1].Width = 30;

            DataGridViewColumn column3 = dataGridView7.Columns[2];
            dataGridView7.Columns[2].HeaderText = "Kontejner";
            dataGridView7.Columns[2].Width = 70;

            DataGridViewColumn column4 = dataGridView7.Columns[3];
            dataGridView7.Columns[3].HeaderText = "Kolicina";
            dataGridView7.Columns[3].Width = 40;

            DataGridViewColumn column5 = dataGridView7.Columns[4];
            dataGridView7.Columns[4].HeaderText = "USLID";
            dataGridView7.Columns[4].Width = 40;

            DataGridViewColumn column6 = dataGridView7.Columns[5];
            dataGridView7.Columns[5].HeaderText = "Usluga";
            dataGridView7.Columns[5].Width = 300;

            DataGridViewColumn column7 = dataGridView7.Columns[6];
            dataGridView7.Columns[6].HeaderText = "Cena";
            dataGridView7.Columns[6].Width = 40;

            DataGridViewColumn column8 = dataGridView7.Columns[7];
            dataGridView7.Columns[7].HeaderText = "OJID";
            dataGridView7.Columns[7].Width = 40;

            DataGridViewColumn column9 = dataGridView7.Columns[8];
            dataGridView7.Columns[8].HeaderText = "OJ NAziv";
            dataGridView7.Columns[8].Width = 140;


            DataGridViewColumn column10 = dataGridView7.Columns[9];
            dataGridView7.Columns[9].HeaderText = "PlID";
            dataGridView7.Columns[9].Width = 140;

            DataGridViewColumn column11 = dataGridView7.Columns[10];
            dataGridView7.Columns[10].HeaderText = "Platilac";
            dataGridView7.Columns[10].Width = 140;



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
                    FillDG8();
                    FillNHM();
                    FillDGUsluge();
                    FillDGUvoznici();

                }
            }
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertUvoz uv = new InsertUvoz();
            uv.DelUvoz(Convert.ToInt32(txtID.Text));
            // FillGV();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            int tDobijenBZ = 0;
            int tPrioritet = 0;
            int PotvrdioKlijent = 0;
            int UradilaCarina = 0;


            if (chkDobijenBZ.Checked == true)
            { tDobijenBZ = 1; };
            if (chkPrioritet.Checked == true)
            { tPrioritet = 1; };
           
            
            if (chkPotvrdioKlijent.Checked == true)
            { PotvrdioKlijent = 1; };
            if (chkCekaSeCarina.Checked == true)
            { UradilaCarina = 1; };
           
            
            InsertUvozKonacna uvK = new InsertUvozKonacna();
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

            if (chkSlobodan.Checked == true)
            {
                txtNapomena1.Text = "1";
            }
            else
            {
                txtNapomena1.Text = "0";
            }

            uvK.InsUvozKonacna(Convert.ToInt32(txtID.Text), Convert.ToInt32(cboPlanUtovara.SelectedValue), Convert.ToDateTime(dtEtaRijeka.Value.ToString()),
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
                Convert.ToInt32(txtBrojVoza.Text), txtRelacija.Text.ToString().TrimEnd(), Convert.ToDateTime(dtAtaDolazak.Value.ToString()), Convert.ToInt32(txtKoleta.Value)
                , Convert.ToInt32(cboRLTerminal.SelectedValue), txtNapomena1.Text, Convert.ToInt32(txtVrstaPregleda.SelectedValue),
                Convert.ToInt32(cboNalogodavac1.SelectedValue), txtRef1.Text,
                Convert.ToInt32(cboNalogodavac2.SelectedValue), txtRef2.Text,
                Convert.ToInt32(cboNalogodavac3.SelectedValue), txtRef3.Text, Convert.ToInt32(cboBrodar.SelectedValue), cboNaslovStatusaVozila.Text, tDobijenBZ, tPrioritet, Convert.ToDecimal(txtTaraTerminal.Value),Convert.ToInt32(cboRLTerminal2.SelectedValue), Convert.ToInt32(cboRLTerminal3.SelectedValue), PotvrdioKlijent, UradilaCarina);

            uvK.PrebaciNHMUvozUvozKonacna(Convert.ToInt32(txtID.Text));
            uvK.PrebaciVrsterobeHSUvozUvozKonacna(Convert.ToInt32(txtID.Text));
            try
            {
                SqlConnection conn = new SqlConnection(connection);
                SqlCommand cmd = new SqlCommand("Delete From Uvoz Where ID=" + Convert.ToInt32(txtID.Text), conn);
                conn.Open();
                var q = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            // FillGV();
            txtID.Text = "";
            tsNew.Enabled = true;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            frmUvozKonacna frm = new frmUvozKonacna();
            frm.Show();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            UvozDokumenta uvdok = new UvozDokumenta(txtID.Text, "0");
            uvdok.Show();
        }

        private void cbNacinPakovanja_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FillDG2()
        {
            /*
            if (txtID.Text == "")
            {
                txtID.Text = "0";
            }
            var select = " SELECT     UvozNHM.ID, NHM.Broj, UvozNHM.IDNHM, NHM.Naziv FROM NHM INNER JOIN " +
                      " UvozNHM ON NHM.ID = UvozNHM.IDNHM where Uvoznhm.idnadredjena = " + Convert.ToInt32(txtID.Text) + " order by Uvoznhm.ID desc ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];


            dataGridView2.BorderStyle = BorderStyle.None;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.DarkGray;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView2.BackgroundColor = Color.White;

            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 54);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(240, 240, 248); ;

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "Broj";
            dataGridView2.Columns[1].Width = 100;

            DataGridViewColumn column3 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "ID";
            dataGridView2.Columns[2].Width = 20;

            DataGridViewColumn column4 = dataGridView2.Columns[3];
            dataGridView2.Columns[3].HeaderText = "NHM";
            dataGridView2.Columns[3].Width = 400;

            */

        }

        private void FillDG3()
        {
            var select = "select UvozVrstaRobeHS.ID, IDVrstaRobeHS, VrstaRobeHS.HSKod as Kod,VrstaRobeHS.Naziv as HS from UvozVrstaRobeHS " +
" inner join  VrstaRobeHS on VrstaRobeHS.ID = UvozVrstaRobeHS.IDVrstaRobeHS where idnadredjena = " + Convert.ToInt32(txtID.Text) + " order by UvozVrstaRobeHS.ID desc ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds.Tables[0];

            dataGridView3.BorderStyle = BorderStyle.None;
            dataGridView3.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView3.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView3.DefaultCellStyle.SelectionBackColor = Color.DarkGray;
            dataGridView3.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView3.BackgroundColor = Color.White;

            dataGridView3.EnableHeadersVisualStyles = false;
            dataGridView3.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 54);
            dataGridView3.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(240, 240, 248); ;

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView3.Columns[0];
            dataGridView3.Columns[0].HeaderText = "ID";
            dataGridView3.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView3.Columns[1];
            dataGridView3.Columns[1].HeaderText = "HSID";
            dataGridView3.Columns[1].Width = 20;

            DataGridViewColumn column3 = dataGridView3.Columns[2];
            dataGridView3.Columns[2].HeaderText = "HSKOD";
            dataGridView3.Columns[2].Width = 80;

            DataGridViewColumn column4 = dataGridView3.Columns[3];
            dataGridView3.Columns[3].HeaderText = "HS";
            dataGridView3.Columns[3].Width = 180;

        }

        private void FillDG4()
        {
            var select = "select UvozNapomenePozicioniranja.ID, IDNapomene, stNapomene from UvozNapomenePozicioniranja " +
"  where UvozNapomenePozicioniranja.IdNadredjena = " + Convert.ToInt32(txtID.Text) + " order by UvozNapomenePozicioniranja.ID desc ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
           
            
            dataGridView4.ReadOnly = true;
            dataGridView4.DataSource = ds.Tables[0];
            dataGridView4.BorderStyle = BorderStyle.None;
            dataGridView4.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView4.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView4.DefaultCellStyle.SelectionBackColor = Color.DarkGray;
            dataGridView4.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView4.BackgroundColor = Color.White;

            dataGridView4.EnableHeadersVisualStyles = false;
            dataGridView4.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView4.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 54);
            dataGridView4.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(240, 240, 248); ;
            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView4.Columns[0];
            dataGridView4.Columns[0].HeaderText = "ID";
            dataGridView4.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView4.Columns[1];
            dataGridView4.Columns[1].HeaderText = "NapomenaID";
            dataGridView4.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView4.Columns[2];
            dataGridView4.Columns[2].HeaderText = "Napomena";
            dataGridView4.Columns[2].Width = 260;

        }
        private void PromeriADRIAkoPostojiUpisi()
        {



        }
        private void button1_Click(object sender, EventArgs e)
        {
            InsertUvozKonacna uvK = new InsertUvozKonacna();
           // uvK.InsUvozNHM(Convert.ToInt32(txtID.Text), Convert.ToInt32(cboNHM.SelectedValue), txtKomercijalniNaziv.Text, txtTarifniBroj.Text, Convert.ToDouble(txtBrojKoleta.Text), Convert.ToDouble(txtBruto.Text), Convert.ToDouble(txtVrednost.Text), txtValuta.Text, Convert.ToDouble(txtNeto.Text));
            VratiADRIzNHM(Convert.ToInt32(cboNHM.SelectedValue));

            FillDG2();
            FillNHM();
            // refreshdataNHM doraditi
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InsertUvozKonacna uvK = new InsertUvozKonacna();

            if (txtIDNHM.Text == "")
            {
                MessageBox.Show("Selektujte stavku koju hocete da brisete");


            }
            else
            {
                uvK.DelUvozNHM(Convert.ToInt32(txtIDNHM.Text));
                FillDG2();
                FillNHM();
            }

            // refreshdataNHM doraditi
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

        private void button4_Click(object sender, EventArgs e)
        {
            InsertUvozKonacna uvK = new InsertUvozKonacna();
            uvK.InsUvozVrstaRobeHS(Convert.ToInt32(txtID.Text), Convert.ToInt32(cboNazivRobe.SelectedValue));
            FillDG3();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtVrstaRobeHS.Text == "")
            {
                MessageBox.Show("Morate selektovati stavku koju brisete");

            }
            else
            {
                InsertUvozKonacna uvK = new InsertUvozKonacna();
                uvK.DelUvozVrstaRobeHS(Convert.ToInt32(txtVrstaRobeHS.Text));
                FillDG3();
            }

        }

        private void clVrstaUsluga_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Uvoz_Load(object sender, EventArgs e)
        {
            //--- RefreshDataGridColor();
            //  firstWidth = this.Size.Width;
            //   firstHeight = this.Size.Height;
            if (chkTerminalski.Checked == true)
            {
                cboScenario.SelectedValue = 15;

            }
            FillSvi();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {

                MessageBox.Show("Morate selektovati stavku Za koju unosite!!!");
                return;
            }
            InsertUvozKonacna uvK = new InsertUvozKonacna();
            uvK.InsUvozNapomenePozicioniranja(Convert.ToInt32(txtID.Text), Convert.ToInt32(cbNapomenaPoz.SelectedValue), cbNapomenaPoz.Text);
            FillDG4();
            RefreshDataGridColor();
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

        private void button5_Click(object sender, EventArgs e)
        {
            InsertUvozKonacna uvK = new InsertUvozKonacna();
            uvK.DelUvozNapomenePozicioniranja(Convert.ToInt32(txtNapomenaPoz.Text));
            FillDG4();
        }

        private void txtBrutoR_Leave(object sender, EventArgs e)
        {
            txtBrutoK.Value = txtBrutoR.Value + txtTaraK.Value;
        }

        private void txtTaraK_Leave(object sender, EventArgs e)
        {
            txtBrutoK.Value = txtBrutoR.Value + txtTaraK.Value;
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
                case "Relacija R-L-SRB":
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
                    updatestring = " Update uvoz set KontaktOsoba = '" + Convert.ToInt32(txtKontaktOsoba.SelectedValue) + "'";
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
        private void button7_Click(object sender, EventArgs e)
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
                // FillGV();

            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void cboSpedicijaRTC_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtNapomena1_TextChanged(object sender, EventArgs e)
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


            dataGridView6.BorderStyle = BorderStyle.None;
            dataGridView6.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView6.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView6.DefaultCellStyle.SelectionBackColor = Color.DarkGray;
            dataGridView6.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView6.BackgroundColor = Color.White;

            dataGridView6.EnableHeadersVisualStyles = false;
            dataGridView6.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView6.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 54);
            dataGridView6.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(240, 240, 248); ;

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

        private void button10_Click(object sender, EventArgs e)
        {
            FillDG6();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FillDG7();
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



            dataGridView6.BorderStyle = BorderStyle.None;
            dataGridView6.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView6.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView6.DefaultCellStyle.SelectionBackColor = Color.DarkGray;
            dataGridView6.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView6.BackgroundColor = Color.White;

            dataGridView6.EnableHeadersVisualStyles = false;
            dataGridView6.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView6.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 54);
            dataGridView6.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(240, 240, 248); ;

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

        private void UbaciStavkuUsluge(int ID, int Manipulacija, double Cena)
        {
            //  InsertUvozKonacna uvK = new InsertUvozKonacna();
            //  uvK.InsUbaciUslugu(Convert.ToInt32(txtID.Text), Manipulacija, Cena, );
            FillDG8();
        }

        private void FillDG8()
        {
            var select = "select  UvozVrstaManipulacije.ID, VrstaManipulacije.Naziv, UvozVrstaManipulacije.Cena, VrstaManipulacije.ID from UvozVrstaManipulacije " +
            " inner join VrstaManipulacije on VrstaManipulacije.ID = UvozVrstaManipulacije.IDVrstaManipulacije " +
                " where UvozVrstaManipulacije.IDNadredjena = " + Convert.ToInt32(txtID.Text);
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView5.ReadOnly = true;
            dataGridView5.DataSource = ds.Tables[0];



            dataGridView5.BorderStyle = BorderStyle.None;
            dataGridView5.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView5.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView5.DefaultCellStyle.SelectionBackColor = Color.DarkGray;
            dataGridView5.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView5.BackgroundColor = Color.White;

            dataGridView5.EnableHeadersVisualStyles = false;
            dataGridView5.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView5.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 54);
            dataGridView5.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(240, 240, 248); ;

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

        private void button9_Click(object sender, EventArgs e)
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

        private void button14_Click(object sender, EventArgs e)
        {
            using (var detailForm = new Dokumenta.frmKontaktOsobe(Convert.ToInt32(cbOcarina.SelectedValue), Convert.ToInt32(cbOspedicija.SelectedValue)))
            {
                detailForm.ShowDialog();
                txtKontaktOsobeSpeditera.Text = detailForm.GetKontaktSpeditera();
            }
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void txtRef2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label48_Click(object sender, EventArgs e)
        {

        }

        private void txtRef1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboNalogodavac2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label46_Click(object sender, EventArgs e)
        {

        }

        private void cboNalogodavac1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label49_Click(object sender, EventArgs e)
        {

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

        private void button11_Click(object sender, EventArgs e)
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


            /*
                        switch (NHMObrni)
                        {
                            case 1:    
                            {/*
                                    var nhm = "Select ID,Rtrim(Broj) + '-' + (Rtrim(Naziv)) as Naziv1, Rtrim(Naziv) + '-' + (Rtrim(Broj)) as Naziv2 from NHM order by NHM.Broj";
                                    var nhmSAD = new SqlDataAdapter(nhm, conn);
                                    var nhmSDS = new DataSet();
                                    nhmSAD.Fill(nhmSDS);
                                    cboNHM.DataSource = nhmSDS.Tables[0];
                                    cboNHM.DisplayMember = "Naziv";
                                    cboNHM.ValueMember = "ID";
                                    NHMObrni = 0;
                                    break;

                                    cboNHM.DataSource = nhmSDSA.Tables[0];
                                    cboNHM.DisplayMember = "Naziv2";
                                    cboNHM.ValueMember = "ID";

                                    NHMObrni = 0;
                                    break;
                                }
                             case 0:
                                {
                                    cboNHM.DataSource = nhmSDS2A.Tables[0];
                                    cboNHM.DisplayMember = "Naziv2";
                                    cboNHM.ValueMember = "ID";
                                    /*
                                    var nhm = "Select ID,Rtrim(Naziv) + '-' + (Rtrim(Broj)) as Naziv from NHM order by NHM.Naziv";
                                    var nhmSAD = new SqlDataAdapter(nhm, conn);
                                    var nhmSDS = new DataSet();
                                    nhmSAD.Fill(nhmSDS);
                                    cboNHM.DataSource = nhmSDS.Tables[0];
                                    cboNHM.DisplayMember = "Naziv";
                                    cboNHM.ValueMember = "ID";
                                    NHMObrni = 1;
                                    break;


                                    NHMObrni = 1;
                                    break;
                                }
                            default:
                                break;
                        }


            */
        }

        private void txtMesto_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {




            /*
            SqlConnection conn = new SqlConnection(connection);

            var ko = "select PaKoZapSt, (Rtrim(PaKOIme) + ' ' + Rtrim(PaKoPriimek)) as Naziv from partnerjiKontOsebaMU where PaKOSifra = '" + Convert.ToInt32(txtMesto.SelectedValue) + "'  order by PaKOIme";
            var koAD = new SqlDataAdapter(ko, conn);
            var koDS = new DataSet();
            koAD.Fill(koDS);
            txtKontaktOsoba.DataSource = koDS.Tables[0];
            txtKontaktOsoba.DisplayMember = "Naziv";
            txtKontaktOsoba.ValueMember = "PaKoZapSt";
           


        }*/
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
        }
        private void VratiEmail(int Sifra)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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

        private void button13_Click(object sender, EventArgs e)
        {
            //VratiEmail(Convert.ToInt32(txtKontaktOsoba.SelectedValue));
        }

        private void VratiNHM(int Sifra)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select TOP 1 ID from NHM Where ADRID =  " + Sifra, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                cboNHM.SelectedValue = Convert.ToInt32(dr["ID"].ToString());


            }
            con.Close();


        }

        private void VratiADRIzNHM(int Sifra)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select TOP 1 ADRID from NHM Where ID  =" + Sifra, con);
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

        private void button15_Click(object sender, EventArgs e)
        {
            VratiNHM(Convert.ToInt32(txtADR.SelectedValue));
        }


        private void button16_Click(object sender, EventArgs e)
        {
            using (var detailForm = new Dokumenta.frmKontaktOsobe(Convert.ToInt32(cboNalogodavac3.SelectedValue)))
            {
                detailForm.ShowDialog();

                txtMail.Text = detailForm.GetKontaktMail(Convert.ToInt32(cboNalogodavac3.SelectedValue));
            }
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            if (checkedListBox2.GetItemCheckState(0) == CheckState.Checked)
            {
                cboNaslovStatusaVozila.Text = " " + txtBrKont.Text;
            }

            if (checkedListBox2.GetItemCheckState(1) == CheckState.Checked)
            {
                cboNaslovStatusaVozila.Text = cboNaslovStatusaVozila.Text + "  " + txtTeretnica.Text;
            }


            if (checkedListBox2.GetItemCheckState(2) == CheckState.Checked)
            {
                cboNaslovStatusaVozila.Text = cboNaslovStatusaVozila.Text + "  " + cboUvoznik.Text;
            }
            if (checkedListBox2.GetItemCheckState(3) == CheckState.Checked)
            {
                cboNaslovStatusaVozila.Text = cboNaslovStatusaVozila.Text + "   " + cboNalogodavac3.Text;
            }
        }
        string relacija;
        string relacija2;
        string relacija3;
        int Zeleznina = 0;
        int Repozicija = 0;
        int ADR = 0;
        int ScenarioGl = 0;

        

        private void MoguciScenario()
        {
            string Moguce = "";
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
                int k = ProveriImaUsluga(txtID.Text);
                if (k == 0)
                { 
                //Kada jos nisu definisane usluge
                }
                else
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
        } 
       

        private void VratiZelezninu(int RLTerminal1, int RLTerminal2, int RLTerminal3, string TipKOntejnera)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select VrstaManipulacije.ID, Relacija from VrstaManipulacije  " +
                " inner join TipKontenjera on VrstaManipulacije.TipKontejnera = TipKontenjera.ID" +
                " where GrupaVrsteManipulacijeID = 1 and Substring(TipKontenjera.SkNaziv,1,3) = '" + TipKOntejnera + "'' AND RLTerminali = " +
                RLTerminal1 + " and RLTerminali2 = " + RLTerminal2  , con);
            SqlDataReader dr = cmd.ExecuteReader();




            while (dr.Read())
            {
               
                    Zeleznina = Convert.ToInt32(dr["ID"].ToString());
                    relacija = dr["Relacija"].ToString();
            }
            con.Close();


        }


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
      
        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            pp = ProveriPrazanPun();
            if (pp == 0)
            {
                VratiRepoziciju(Convert.ToInt32(cboRLTerminal.SelectedValue), Convert.ToInt32(cboRLTerminal2.SelectedValue), Convert.ToInt32(cboRLTerminal3.SelectedValue), txtTipKont.Text.Substring(0, 3));
            }
            else
            {


                VratiZelezninu(Convert.ToInt32(cboRLTerminal.SelectedValue), Convert.ToInt32(cboRLTerminal2.SelectedValue), Convert.ToInt32(cboRLTerminal3.SelectedValue), txtTipKont.Text.Substring(0, 3));

            }
            ADR = Convert.ToInt32(txtADR.SelectedValue);
            int terminal=0;
            if (txtID.Text == "")
            { txtID.Text = "0"; }
            if (chkTerminalski.Checked)
            {
                terminal = 1;
            }
         
            relacija = cboRLTerminal.Text.ToString().TrimEnd() ;

            MoguciScenario();
            //Proveri da li ima usluga za 

            // int IDPlana, int ID, int Nalogodavac1, int Nalogodavac2, int Nalogodavac3
            frmUnosManipulacija um = new frmUnosManipulacija(Convert.ToInt32(0), Convert.ToInt32(txtID.Text), Convert.ToInt32(cboNalogodavac1.SelectedValue), Convert.ToInt32(cboNalogodavac2.SelectedValue), Convert.ToInt32(cboNalogodavac3.SelectedValue), Convert.ToInt32(cboUvoznik.SelectedValue), KorisnikTekuci,terminal,relacija,Zeleznina, ADR, ScenarioGl,pp, Repozicija);
           // um.FormClosing += new FormClosingEventHandler(this.frmUnosManipulacija_FormClosing);
            um.Show();


          
        }

        private void RefreshScenario()
        {
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


        private void txtNetoR_Enter(object sender, EventArgs e)
        {
            txtNetoR.Select(0, txtNetoR.Text.Length);
        }

        private void txtTaraK_Enter(object sender, EventArgs e)
        {
            txtTaraK.Select(0, txtTaraK.Text.Length);
        }

        private void txtBrutoK_Enter(object sender, EventArgs e)
        {
            txtBrutoK.Select(0, txtBrutoK.Text.Length);
        }

        private void txtBrutoR_Enter(object sender, EventArgs e)
        {
            txtBrutoR.Select(0, txtBrutoR.Text.Length);
        }

        private void txtKoleta_Enter(object sender, EventArgs e)
        {
            txtKoleta.Select(0, txtKoleta.Text.Length);
        }

        private void txtNetoR_KeyPress(object sender, KeyPressEventArgs e)
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

        private void Uvoz_SizeChanged(object sender, EventArgs e)
        {
            /*
            float size1 = this.Size.Width / firstWidth;
            float size2 = this.Size.Height / firstHeight;

            SizeF scale = new SizeF(size1, size2);
            firstWidth = this.Size.Width;
            firstHeight = this.Size.Height;

            foreach (Control control in this.Controls)
            {

                control.Font = new Font(control.Font.FontFamily, control.Font.Size * ((size1 + size2) / 2));

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

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            if (chkTerminalski.Checked == false)
            {
                using (var detailForm = new UvozTable())
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
            else
            {
                using (var detailForm = new UvozTable(1))
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

            FillDGUsluge();
            FillDGUvoznici();


        }

        private void FillDGUvoznici()
        {
            if (txtID.Text == "")
            {
                txtID.Text = "0";
            }
            var select = " SELECT     UvozUvoznici.ID, Naziv, PaDMAtSt as PIB  FROM UvozUvoznici where UvozUvoznici.idnadredjena = " + Convert.ToInt32(txtID.Text) + " order by UvozUvoznici.ID desc ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new System.Data.DataSet();
            da.Fill(ds);
            dataGridView8.ReadOnly = true;
            dataGridView8.DataSource = ds.Tables[0];


            dataGridView8.BorderStyle = BorderStyle.None;
            dataGridView8.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView8.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView8.DefaultCellStyle.SelectionBackColor = Color.DarkGray;
            dataGridView8.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView8.BackgroundColor = Color.White;

            dataGridView8.EnableHeadersVisualStyles = false;
            dataGridView8.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView8.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 54);
            dataGridView8.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(240, 240, 248); ;

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView8.Columns[0];
            dataGridView8.Columns[0].HeaderText = "ID";
            dataGridView8.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView8.Columns[1];
            dataGridView8.Columns[1].HeaderText = "Naziv";
            dataGridView8.Columns[1].Width = 200;

            DataGridViewColumn column3 = dataGridView8.Columns[2];
            dataGridView8.Columns[2].HeaderText = "PIB";
            dataGridView8.Columns[2].Width = 100;





        }

        private void Uvoz_KeyDown(object sender, KeyEventArgs e)
        {
            /*
            if (Control.ModifierKeys == Keys.Shift && e.KeyCode == Keys.D)
            {
                UvozDokumenta uvdok = new UvozDokumenta(txtID.Text);
                uvdok.Show();
            }
            else if (Control.ModifierKeys == Keys.Shift && e.KeyCode == Keys.T)
            {
                using (var detailForm = new UvozTable())
                {
                    detailForm.ShowDialog();
                    txtID.Text = detailForm.GetID();
                    VratiPodatkeSelect(Convert.ToInt32(txtID.Text));
                }
            }
            else if (Control.ModifierKeys == Keys.Shift && e.KeyCode == Keys.U)
            {
                if (txtID.Text == "")
                { txtID.Text = "0"; }
                // int IDPlana, int ID, int Nalogodavac1, int Nalogodavac2, int Nalogodavac3
                frmUnosManipulacija um = new frmUnosManipulacija(Convert.ToInt32(0), Convert.ToInt32(txtID.Text), Convert.ToInt32(cboNalogodavac1.SelectedValue), Convert.ToInt32(cboNalogodavac2.SelectedValue), Convert.ToInt32(cboNalogodavac3.SelectedValue), Convert.ToInt32(cboUvoznik.SelectedValue), KorisnikTekuci);
                um.Show();

            }
            */
            if (Control.ModifierKeys == Keys.Shift && e.KeyCode == Keys.F1)
            {
                UvozDokumenta uvdok = new UvozDokumenta(txtID.Text, "0");
                uvdok.Show();
            }
            else if (Control.ModifierKeys == Keys.Shift && e.KeyCode == Keys.F2 )
            {
                using (var detailForm = new UvozTable())
                {
                    detailForm.ShowDialog();
                    txtID.Text = detailForm.GetID();
                    VratiPodatkeSelect(Convert.ToInt32(txtID.Text));
                }
            }
            else if (Control.ModifierKeys == Keys.Shift && e.KeyCode == Keys.F3)
            {
                int terminal = 0;
                if(chkTerminalski.Checked) { terminal = 1; }
                if (txtID.Text == "")
                { txtID.Text = "0"; }
                //Terminali
                relacija = cboRLTerminal.Text.ToString().TrimEnd();
                // int IDPlana, int ID, int Nalogodavac1, int Nalogodavac2, int Nalogodavac3
                frmUnosManipulacija um = new frmUnosManipulacija(Convert.ToInt32(0), Convert.ToInt32(txtID.Text), Convert.ToInt32(cboNalogodavac1.SelectedValue), Convert.ToInt32(cboNalogodavac2.SelectedValue), Convert.ToInt32(cboNalogodavac3.SelectedValue), Convert.ToInt32(cboUvoznik.SelectedValue), KorisnikTekuci,terminal, relacija, Zeleznina,ADR, ScenarioGl,pp, Repozicija);
                um.Show();

            }
        }

        private void Uvoz_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == System.Windows.Forms.Keys.Enter)
            {
                SendKeys.Send("{TAB}");
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

        private void button18_Click(object sender, EventArgs e)
        {
            using (var detailForm = new Izvoz.frmKontaktOsobeMU(txtAdresaMestaUtovara.Text))
            {
                detailForm.ShowDialog();
                txtKontaktOsobe.Text = detailForm.GetSviKontaktiPoAdresi(txtAdresaMestaUtovara.Text);
            }
            // txtKontaktOsobe.Text = GetSviKontaktiPoAdresi(txtAdresaMestaUtovara.Text);



            SqlConnection conn = new SqlConnection(connection);
            // PaKOOpomba
            //Bilo  var ko = "select PaKoZapSt, (Rtrim(PaKOIme) + ' ' + Rtrim(PaKoPriimek)) as Naziv from partnerjiKontOsebaMU where PaKOSifra = '" + Convert.ToInt32(cboMestoUtovara.SelectedValue) + "'  order by PaKOIme";
            var ko = "select PaKoZapSt, (Rtrim(PaKOIme) + ' ' + Rtrim(PaKoPriimek)) as Naziv from partnerjiKontOsebaMU where PaKoZapSt = '" + Convert.ToInt32(txtAdresaMestaUtovara.SelectedValue) + "'  order by PaKOIme";

            var koAD = new SqlDataAdapter(ko, conn);
            var koDS = new DataSet();
            koAD.Fill(koDS);
            txtKontaktOsoba.DataSource = koDS.Tables[0];
            txtKontaktOsoba.DisplayMember = "Naziv";
            txtKontaktOsoba.ValueMember = "PaKoZapSt";
        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            int TcbBrod = Convert.ToInt32(cbBrod.SelectedValue);
            int TtxtTipKont = Convert.ToInt32(txtTipKont.SelectedValue);
            int TcboRLTerminal = Convert.ToInt32(cboRLTerminal.SelectedValue);
            int TcboRLTerminal2 = Convert.ToInt32(cboRLTerminal2.SelectedValue);
            int TcboRLTerminal3 = Convert.ToInt32(cboRLTerminal3.SelectedValue);
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
            cboRLTerminal2.SelectedValue = TcboRLTerminal2;
            cboRLTerminal3.SelectedValue = TcboRLTerminal3;
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

        private void txtID_TextChanged(object sender, EventArgs e)
        {
            FillDGUsluge();
            FillDGUvoznici();
            FillDG2();
            FillNHM();
            FillDG4();
        }

        private void txtTipKont_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        int VratiPostojeceRN()
        {
            //Ovde treba napisati proveru da li je RN vec iygenerisan
            return 0;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            InsertUvozKonacna uvK = new InsertUvozKonacna();
            uvK.InsUbaciUslugu(Convert.ToInt32(txtID.Text), 69, 0, 1, 4, Convert.ToInt32(cboBrodar.SelectedValue), 0, "GATE IN EMPTY", 13, KorisnikTekuci, "GATE IN KAMION");
            FillDGUsluge();

            InsertUvozKonacna ins = new InsertUvozKonacna();
            ins.PrenesiUPlanUtovara(Convert.ToInt32(txtID.Text), Convert.ToInt32(cboPlanUtovara.SelectedValue));


            if (chkTerminalski.Checked == true)
            {
                DialogResult dialogResult = MessageBox.Show("Pokrenuli ste proceduru pravljenja Terminalskog naloga ", "Radni nalog", MessageBoxButtons.YesNo);
                int PostojeRn = 0;
                PostojeRn = VratiPostojeceRN();
                if (dialogResult == DialogResult.Yes)
                {

                    InsertRadniNalogInterni insRNI = new InsertRadniNalogInterni();
                    insRNI.InsRadniNalogInterni(Convert.ToInt32(4), Convert.ToInt32(4), Convert.ToDateTime(DateTime.Now), Convert.ToDateTime("1.1.1900. 00:00:00"), "", Convert.ToInt32(0), "PlanUtovara", Convert.ToInt32(cboPlanUtovara.SelectedValue), KorisnikTekuci, "");

                }
                else
                {
                    // FormirajOpstiExcel();
                }
            }

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            InsertUvozKonacna ins = new InsertUvozKonacna();
            ins.PrenesiUPlanUtovara(Convert.ToInt32(txtID.Text), Convert.ToInt32(cboPlanUtovara.SelectedValue));
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            using (var detailForm = new Dokumenta.frmKontaktOsobe(Convert.ToInt32(cbOcarina.SelectedValue), Convert.ToInt32(cbOspedicija.SelectedValue)))
            {
                detailForm.ShowDialog();
                txtKontaktOsobeSpeditera.Text = detailForm.GetKontaktSpeditera();
            }
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {

        }

        private void Uvoz_Activated(object sender, EventArgs e)
        {
            FillDGUsluge();
            FillDGUvoznici();

            RefreshScenario();
            if (chkTerminalski.Checked == true)
            {
                cboScenario.SelectedValue = 15;
            
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void textBoxExt1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button22_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connection);
                SqlCommand cmd = new SqlCommand("Insert into Uvoz Default Values", conn);
                conn.Open();
                var q = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            GetID();
            tsNew.Enabled = false;
            txtBrKont.Text = "";
            txtMail.Text = "";
            txtNetoR.Value = 0;
            txtBrutoR.Value = 0;
            txtBrutoK.Value = 0;
            FillDG2();
            FillDGUsluge();
            FillDG4();
            FillNHM();
        }
        string labelDialogResult = "";

        private void button21_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                labelDialogResult = "Dialog Box Result";
                //It is optional to save the Dialog Result,
                //use it when you need to know which button or action the user selected,
                //and do a specific function/task according to the dialogue result.
                //-| For Example:
                DialogResult result = Saobracaj.Pomocni.RJMessageBox.Show("Niste uneli ID");
                if (result == DialogResult.OK)
                    labelDialogResult = "You have selected the OK BUTTON";
                else labelDialogResult= "You have selected CANCEL";
               // MessageBox.Show("Kliknite na ikonicu NOVI ili izaberite postojeći kontejner");
            }
            int tDobijenBZ = 0;
            int tPrioritet = 0;
            int Terminalska = 0;




            if (chkDobijenBZ.Checked == true)
            { tDobijenBZ = 1; };
            if (chkPrioritet.Checked == true)
            { tPrioritet = 1; };
            if (chkDobijenBZ.Checked == true)
            { tDobijenBZ = 1; };
            if (chkTerminalski.Checked == true)
            { Terminalska = 1;
                cboScenario.SelectedValue = 15;
                    };


            try
            {
                int temp = Convert.ToInt32(txtBrojVoza.Text);
            }
            catch (Exception)
            {
                labelDialogResult = "Dialog Box Result";
                //It is optional to save the Dialog Result,
                //use it when you need to know which button or action the user selected,
                //and do a specific function/task according to the dialogue result.
                //-| For Example:
                DialogResult result = Saobracaj.Pomocni.RJMessageBox.Show("Niste uneli numericku vrednost voza");
                if (result == DialogResult.OK)
                    labelDialogResult = "You have selected the OK BUTTON";
                else labelDialogResult = "You have selected CANCEL";
                return;
            }


            if (txtBuking.Text == "")
            { txtBuking.Text = "0"; }
            else
            {
                try
                {
                    int temp = Convert.ToInt32(txtBuking.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Unesite numeričku vrednost Bukinga");
                    return;
                }
            }


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
            for(int i = 0; i < clVrstaUsluga.Items.Count; i++)
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
            InsertUvoz ins = new InsertUvoz();
            if (chkSlobodan.Checked == true)
            {
                txtNapomena1.Text = "1";
            }
            else
            {
                txtNapomena1.Text = "0";
            }

            int PotvrdioKlijent = 0;
            int UradilaCarina = 0;

            
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


            int drumska= 0;
            if (chkDrumskaUsluga.Checked == true)
            { drumska = 1; };









            int i = ProveriBiloIzmena(txtID.Text);

            if (i != 0)
            {
                DialogResult result = MessageBox.Show("Bilo je izmena nad podacima koji uticu na usluge?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    ins.UpdUvoz(Convert.ToInt32(txtID.Text), Convert.ToDateTime(dtpETAbroda.Value.ToString()),
                Convert.ToDateTime(dtAtaRijeka.Value.ToString()), txtStatus.Text.ToString().TrimEnd(), txtBrKont.Text,
                Convert.ToInt32(txtTipKont.SelectedValue), Convert.ToDateTime(dtNalogBrodara.Value.ToString()), txtBZ.Text.ToString().TrimEnd(),
                txtNapomena.Text.ToString().TrimEnd(), txtPIN.Text.ToString().TrimEnd(), Convert.ToInt32(cbDirigacija.SelectedValue), Convert.ToInt32(cbBrod.SelectedValue),
                txtTeretnica.Text, Convert.ToInt32(txtADR.SelectedValue), Convert.ToInt32(cbVlasnikKont.SelectedValue), Convert.ToInt32(txtBuking.Text), nalogodavci,
                usluge, Convert.ToInt32(cboUvoznik.SelectedValue), Convert.ToInt32(cboNHM.SelectedValue), "", Convert.ToInt32(cboSpedicijaG.SelectedValue),
                Convert.ToInt32(cboSpedicijaRTC.SelectedValue), Convert.ToInt32(cboCarinskiPostupak.SelectedValue), Convert.ToInt32(cbPostupak.SelectedValue),
                Convert.ToInt32(cbNacinPakovanja.SelectedValue), Convert.ToInt32(cbOcarina.SelectedValue), Convert.ToInt32(cbOspedicija.SelectedValue),
                Convert.ToInt32(txtMesto.SelectedValue), Convert.ToInt32(txtKontaktOsoba.SelectedValue), txtMail.Text.ToString(), txtPlomba1.Text,
                txtPlomba2.Text, Convert.ToDecimal(txtNetoR.Value), Convert.ToDecimal(txtBrutoR.Value), Convert.ToDecimal(txtTaraK.Value),
                Convert.ToDecimal(txtBrutoK.Value), Convert.ToInt32(cbNapomenaPoz.SelectedValue), Convert.ToDateTime(dtAtaOtprema.Value.ToString()),
                Convert.ToInt32(txtBrojVoza.Text), txtRelacija.Text.ToString().TrimEnd(), Convert.ToDateTime(dtAtaDolazak.Value.ToString()), Convert.ToDecimal(txtKoleta.Value), Convert.ToInt32(cboRLTerminal.SelectedValue), txtNapomena1.Text, Convert.ToInt32(txtVrstaPregleda.SelectedValue),
                Convert.ToInt32(cboNalogodavac1.SelectedValue), txtRef1.Text,
                Convert.ToInt32(cboNalogodavac2.SelectedValue), txtRef2.Text,
                Convert.ToInt32(cboNalogodavac3.SelectedValue), txtRef3.Text, Convert.ToInt32(cboBrodar.SelectedValue), cboNaslovStatusaVozila.Text, tDobijenBZ, tPrioritet, Convert.ToInt32(txtAdresaMestaUtovara.SelectedValue), txtKontaktOsobe.Text, Terminalska, Convert.ToDecimal(txtTaraTerminal.Value), Convert.ToDecimal(txtKoletaTer.Value), Convert.ToInt32(cboScenario.SelectedValue), Convert.ToInt32(cboRLTerminal2.SelectedValue), Convert.ToInt32(cboRLTerminal3.SelectedValue), PotvrdioKlijent, UradilaCarina,
                TFDobijenNalog, TFDobijenNalogodavac1, Convert.ToDateTime(dtpDobijenNalogodavac1.Value),  TFDobijenNalogodavac2, Convert.ToDateTime(dtpDobijenNalogodavac2.Value), 
                TFDobijenNalogodavac3, Convert.ToDateTime(dtpDobijenNalogodavac3.Value), TFFCL, TFLCL, Convert.ToDateTime(dtpPotvrdioKlijent.Value), Convert.ToDateTime(dtpSlobodanDaNapusti.Value), drumska);
                    //  FillGV();
                    //  RefreshDataGridColor();
                    tsNew.Enabled = true;
                }
                else
                {
                    return;
                }
            }
            else
            {
                ins.UpdUvoz(Convert.ToInt32(txtID.Text), Convert.ToDateTime(dtpETAbroda.Value.ToString()),
             Convert.ToDateTime(dtAtaRijeka.Value.ToString()), txtStatus.Text.ToString().TrimEnd(), txtBrKont.Text,
             Convert.ToInt32(txtTipKont.SelectedValue), Convert.ToDateTime(dtNalogBrodara.Value.ToString()), txtBZ.Text.ToString().TrimEnd(),
             txtNapomena.Text.ToString().TrimEnd(), txtPIN.Text.ToString().TrimEnd(), Convert.ToInt32(cbDirigacija.SelectedValue), Convert.ToInt32(cbBrod.SelectedValue),
             txtTeretnica.Text, Convert.ToInt32(txtADR.SelectedValue), Convert.ToInt32(cbVlasnikKont.SelectedValue), Convert.ToInt32(txtBuking.Text), nalogodavci,
             usluge, Convert.ToInt32(cboUvoznik.SelectedValue), Convert.ToInt32(cboNHM.SelectedValue), "", Convert.ToInt32(cboSpedicijaG.SelectedValue),
             Convert.ToInt32(cboSpedicijaRTC.SelectedValue), Convert.ToInt32(cboCarinskiPostupak.SelectedValue), Convert.ToInt32(cbPostupak.SelectedValue),
             Convert.ToInt32(cbNacinPakovanja.SelectedValue), Convert.ToInt32(cbOcarina.SelectedValue), Convert.ToInt32(cbOspedicija.SelectedValue),
             Convert.ToInt32(txtMesto.SelectedValue), Convert.ToInt32(txtKontaktOsoba.SelectedValue), txtMail.Text.ToString(), txtPlomba1.Text,
             txtPlomba2.Text, Convert.ToDecimal(txtNetoR.Value), Convert.ToDecimal(txtBrutoR.Value), Convert.ToDecimal(txtTaraK.Value),
             Convert.ToDecimal(txtBrutoK.Value), Convert.ToInt32(cbNapomenaPoz.SelectedValue), Convert.ToDateTime(dtAtaOtprema.Value.ToString()),
             Convert.ToInt32(txtBrojVoza.Text), txtRelacija.Text.ToString().TrimEnd(), Convert.ToDateTime(dtAtaDolazak.Value.ToString()), Convert.ToDecimal(txtKoleta.Value), Convert.ToInt32(cboRLTerminal.SelectedValue), txtNapomena1.Text, Convert.ToInt32(txtVrstaPregleda.SelectedValue),
             Convert.ToInt32(cboNalogodavac1.SelectedValue), txtRef1.Text,
             Convert.ToInt32(cboNalogodavac2.SelectedValue), txtRef2.Text,
             Convert.ToInt32(cboNalogodavac3.SelectedValue), txtRef3.Text, Convert.ToInt32(cboBrodar.SelectedValue), cboNaslovStatusaVozila.Text, tDobijenBZ, tPrioritet, Convert.ToInt32(txtAdresaMestaUtovara.SelectedValue), txtKontaktOsobe.Text, Terminalska, Convert.ToDecimal(txtTaraTerminal.Value), Convert.ToDecimal(txtKoletaTer.Value), Convert.ToInt32(cboScenario.SelectedValue), Convert.ToInt32(cboRLTerminal2.SelectedValue), Convert.ToInt32(cboRLTerminal3.SelectedValue), PotvrdioKlijent, UradilaCarina,
                          TFDobijenNalog, TFDobijenNalogodavac1, Convert.ToDateTime(dtpDobijenNalogodavac1.Value), TFDobijenNalogodavac2, Convert.ToDateTime(dtpDobijenNalogodavac2.Value),
                TFDobijenNalogodavac3, Convert.ToDateTime(dtpDobijenNalogodavac3.Value), TFFCL, TFLCL, Convert.ToDateTime(dtpPotvrdioKlijent.Value), Convert.ToDateTime(dtpSlobodanDaNapusti.Value),drumska);
            }

         


        }

        int ProveriPrazanPun()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            int idnhm = 0;
            con.Open();

            SqlCommand cmd = new SqlCommand("select top 1 IDNHM from UvozNHM where IDNadredjena = " + Convert.ToInt32(txtID.Text), con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
             idnhm = Convert.ToInt32(dr["IDNHM"].ToString());

            }

            con.Close();

            return idnhm;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            InsertUvoz uv = new InsertUvoz();
            uv.DelUvoz(Convert.ToInt32(txtID.Text));
        }
        int pp = 0;
        private void button24_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "0")
            {
                MessageBox.Show("Niste izabrali kontejner");
                return;
            
            }
           pp = ProveriPrazanPun();
           if  (txtTipKont.Text.Length > 3)
            { 
                if (pp == 0)
                {
                    //PRAZAN
                    VratiRepoziciju(Convert.ToInt32(cboRLTerminal.SelectedValue), Convert.ToInt32(cboRLTerminal2.SelectedValue), Convert.ToInt32(cboRLTerminal3.SelectedValue), txtTipKont.Text.Substring(0, 3));
                }
                else
                {
                    //PUN
                    VratiZelezninu(Convert.ToInt32(cboRLTerminal.SelectedValue), Convert.ToInt32(cboRLTerminal2.SelectedValue), Convert.ToInt32(cboRLTerminal3.SelectedValue), txtTipKont.Text.Substring(0, 3));

                }
            }
            ADR = Convert.ToInt32(txtADR.SelectedValue);
            int terminal = 0;
            if (txtID.Text == "")
            { txtID.Text = "0"; }
            if (chkTerminalski.Checked)
            {
                terminal = 1;
            }
            //Terminali
            //  Zeleznina = 
            relacija = cboRLTerminal.Text.ToString().TrimEnd();
            relacija2 = cboRLTerminal2.Text.ToString().TrimEnd();
            relacija3 = cboRLTerminal3.Text.ToString().TrimEnd();

            if (txtBrKont.Text.StartsWith("ROB-"))
            {

                ScenarioGl = 4; /// CIRADA U CIRADU
            }
            else if (relacija3 == "KAMION")
            {
                ScenarioGl = 5; //DOLAZAK KAMIONA IZ POLJSKE I PRETOVAR
            }
          
            else
            {
                MoguciScenario();
            }
        
            //Proveri da li ima usluga za 

            // int IDPlana, int ID, int Nalogodavac1, int Nalogodavac2, int Nalogodavac3
            frmUnosManipulacija um = new frmUnosManipulacija(Convert.ToInt32(0), Convert.ToInt32(txtID.Text), Convert.ToInt32(cboNalogodavac1.SelectedValue), Convert.ToInt32(cboNalogodavac2.SelectedValue), Convert.ToInt32(cboNalogodavac3.SelectedValue), Convert.ToInt32(cboUvoznik.SelectedValue), KorisnikTekuci, terminal, relacija, Zeleznina, ADR, ScenarioGl, pp, Repozicija);
            // um.FormClosing += new FormClosingEventHandler(this.frmUnosManipulacija_FormClosing);
            um.Show();
            FillDG2();
            FillNHM();
           // FillSvi();

        }

        private void RefreshGV()
        {
            var select = "SELECT Uvoz.ID, " +
                " CASE WHEN Prioritet > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Prioritet , " +
                " CASE WHEN DobijenBZ > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as DobijenBZ , " +
                " [BrojKontejnera],TipKontenjera.Naziv as Vrsta_Kontejnera, BrodskaTeretnica as BL,   Brodovi.Naziv as Brod, p1.PaNaziv as Uvoznik, " +
" n1.PaNaziv as NalogodavacZaVoz, Ref1 as Ref1,n2.PaNaziv as NalogodavacZaUsluge, Ref2 as Ref2,n3.PaNaziv as NalogodavacZaDrumski,DobijenNalogBrodara as Dobijen_Nalog_Brodara ,ATABroda,  " +
" Napomena1 as Napomena1,  DobijeBZ as DatumBZ ,PIN,    KontejnerskiTerminali.Naziv as R_L_SRB, pp1.Naziv as Dirigacija_Kontejnera_Za,   BrodskaTeretnica, " +
" VrstaRobeADR.Naziv as ADR, b.PaNaziv as Brodar,pv.PaNaziv as VlasnikKontejnera,     Ref3 as Ref3,         VrstaPregleda as InsTret,p2.PaNaziv as SpedicijaRTC,  " +
" p3.PaNaziv as SpedicijaGranica,       VrstaCarinskogPostupka.Naziv as CarinskiPostupak,  " +
" VrstePostupakaUvoz.Naziv as PostupakSaRobom,uvNacinPakovanja.Naziv as NacinPakovanja, " +
" Napomena as Napomena2, NaslovStatusaVozila as NaslovZaslanjestatusa,  Carinarnice.Naziv as Carinarnica,   " +
" p4.PaNaziv as OdredisnaSpedicija, MestaUtovara.Naziv as MestoIstovara, KontaktOsobe, Email, " +
" BrojPlombe1, BrojPlombe2,    NetoRobe, BrutoRobe, TaraKontejnera, BrutoKontejnera,  Koleta, green FROM Uvoz inner join Partnerji on PaSifra = VlasnikKontejnera " +
" inner join Partnerji p1 on p1.PaSifra = Uvoznik " +
" inner join Partnerji p2 on p2.PaSifra = SpedicijaRTC " +
" inner join Partnerji p3 on p3.PaSifra = SpedicijaGranica " +
" inner join TipKontenjera on TipKontenjera.ID = Uvoz.TipKontejnera " +
" inner join Carinarnice on Carinarnice.ID = Uvoz.OdredisnaCarina " +
" inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = Uvoz.CarinskiPostupak " +
" inner join Predefinisaneporuke on PredefinisanePoruke.ID = Uvoz.NapomenaZaPozicioniranje " +
" inner join KontejnerskiTerminali on KontejnerskiTerminali.ID = Uvoz.RLTErminali " +
" inner join Partnerji n1 on n1.PaSifra = Nalogodavac1 " +
" inner join Partnerji n2 on n2.PaSifra = Nalogodavac2 " +
" inner join Partnerji n3 on n3.PaSifra = Nalogodavac3 " +
" inner join Partnerji b on b.PaSifra = Uvoz.Brodar " +
" inner join  DirigacijaKontejneraZa pp1 on pp1.ID = Uvoz.DirigacijaKontejeraZa " +
" inner join Brodovi on Brodovi.ID = Uvoz.NazivBroda " +
" inner join VrstaRobeADR on VrstaRobeADR.ID = ADR " +
" inner join VrstePostupakaUvoz on VrstePostupakaUvoz.ID = PostupakSaRobom " +
" inner join MestaUtovara on Uvoz.MestoIstovara = MestaUtovara.ID " +
" inner join uvNacinPakovanja on uvNacinPakovanja.ID = NacinPakovanja " +
" inner join Partnerji p4 on p4.PaSifra = OdredisnaSpedicija " +
" inner join Partnerji pv on pv.PaSifra = Uvoz.VlasnikKontejnera " +
"  order by Uvoz.Prioritet desc, Uvoz.ID desc ";



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
            // this.gridGroupingControl1.RecordFilterDescriptor.
            foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
                //  column.RecordFilterDescriptor
            }
            this.gridGroupingControl1.TableDescriptor.Columns["BrojKontejnera"].FilterRowOptions.FilterMode = FilterMode.DisplayText;
            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            //Wiring the Dynamic Filter to GridGroupingControl
            dynamicFilter.WireGrid(this.gridGroupingControl1);

            GridExcelFilter gridExcelFilter = new GridExcelFilter();

            //Wiring GridExcelFilter to GridGroupingControl
            gridExcelFilter.WireGrid(this.gridGroupingControl1);


        }

        private void RefreshGVT()
        {
            var select = "SELECT Uvoz.ID, " +
                " CASE WHEN Prioritet > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Prioritet , " +
                " CASE WHEN DobijenBZ > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as DobijenBZ , " +
                " [BrojKontejnera],TipKontenjera.Naziv as Vrsta_Kontejnera, BrodskaTeretnica as BL,   Brodovi.Naziv as Brod, p1.PaNaziv as Uvoznik, " +
" n1.PaNaziv as NalogodavacZaVoz, Ref1 as Ref1,n2.PaNaziv as NalogodavacZaUsluge, Ref2 as Ref2,n3.PaNaziv as NalogodavacZaDrumski,DobijenNalogBrodara as Dobijen_Nalog_Brodara ,ATABroda,  " +
" Napomena1 as Napomena1,  DobijeBZ as DatumBZ ,PIN,    KontejnerskiTerminali.Naziv as R_L_SRB, pp1.Naziv as Dirigacija_Kontejnera_Za,   BrodskaTeretnica, " +
" VrstaRobeADR.Naziv as ADR, b.PaNaziv as Brodar,pv.PaNaziv as VlasnikKontejnera,     Ref3 as Ref3,         VrstaPregleda as InsTret,p2.PaNaziv as SpedicijaRTC,  " +
" p3.PaNaziv as SpedicijaGranica,       VrstaCarinskogPostupka.Naziv as CarinskiPostupak,  " +
" VrstePostupakaUvoz.Naziv as PostupakSaRobom,uvNacinPakovanja.Naziv as NacinPakovanja, " +
" Napomena as Napomena2, NaslovStatusaVozila as NaslovZaslanjestatusa,  Carinarnice.Naziv as Carinarnica,   " +
" p4.PaNaziv as OdredisnaSpedicija, MestaUtovara.Naziv as MestoIstovara, KontaktOsobe, Email, " +
" BrojPlombe1, BrojPlombe2,    NetoRobe, BrutoRobe, TaraKontejnera, BrutoKontejnera,  Koleta, green FROM Uvoz inner join Partnerji on PaSifra = VlasnikKontejnera " +
" inner join Partnerji p1 on p1.PaSifra = Uvoznik " +
" inner join Partnerji p2 on p2.PaSifra = SpedicijaRTC " +
" inner join Partnerji p3 on p3.PaSifra = SpedicijaGranica " +
" inner join TipKontenjera on TipKontenjera.ID = Uvoz.TipKontejnera " +
" inner join Carinarnice on Carinarnice.ID = Uvoz.OdredisnaCarina " +
" inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = Uvoz.CarinskiPostupak " +
" inner join Predefinisaneporuke on PredefinisanePoruke.ID = Uvoz.NapomenaZaPozicioniranje " +
" inner join KontejnerskiTerminali on KontejnerskiTerminali.ID = Uvoz.RLTErminali " +
" inner join Partnerji n1 on n1.PaSifra = Nalogodavac1 " +
" inner join Partnerji n2 on n2.PaSifra = Nalogodavac2 " +
" inner join Partnerji n3 on n3.PaSifra = Nalogodavac3 " +
" inner join Partnerji b on b.PaSifra = Uvoz.Brodar " +
" inner join  DirigacijaKontejneraZa pp1 on pp1.ID = Uvoz.DirigacijaKontejeraZa " +
" inner join Brodovi on Brodovi.ID = Uvoz.NazivBroda " +
" inner join VrstaRobeADR on VrstaRobeADR.ID = ADR " +
" inner join VrstePostupakaUvoz on VrstePostupakaUvoz.ID = PostupakSaRobom " +
" inner join MestaUtovara on Uvoz.MestoIstovara = MestaUtovara.ID " +
" inner join uvNacinPakovanja on uvNacinPakovanja.ID = NacinPakovanja " +
" inner join Partnerji p4 on p4.PaSifra = OdredisnaSpedicija " +
" inner join Partnerji pv on pv.PaSifra = Uvoz.VlasnikKontejnera where Uvoz.Terminal = 1" +
"  order by Uvoz.Prioritet desc, Uvoz.ID desc ";



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
            dynamicFilter.WireGrid(this.gridGroupingControl1);

        }

        private void FillSvi()
        {
            if (chkTerminalski.Checked == false)
            {
                RefreshGV();
            }
            else
            {
                RefreshGVT();
            }


        }

        private void FillNHM()
        {
            if (txtID.Text == "")
            {
                txtID.Text = "0";
            }
            var select = " SELECT     UvozNHM.ID, NHM.Broj, UvozNHM.IDNHM, NHM.Naziv, KomercijalniNaziv, TarifniBroj, BrojKoleta, Bruto, Vrednost, Valuta FROM NHM INNER JOIN " +
                      " UvozNHM ON NHM.ID = UvozNHM.IDNHM where Uvoznhm.idnadredjena = " + Convert.ToInt32(txtID.Text) + " order by Uvoznhm.ID desc ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView9.ReadOnly = true;
            dataGridView9.DataSource = ds.Tables[0];


            dataGridView9.BorderStyle = BorderStyle.None;
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
            dataGridView9.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView9.Columns[1];
            dataGridView9.Columns[1].HeaderText = "Broj";
            dataGridView9.Columns[1].Width = 100;

            DataGridViewColumn column3 = dataGridView9.Columns[2];
            dataGridView9.Columns[2].HeaderText = "ID";
            dataGridView9.Columns[2].Width = 20;

            DataGridViewColumn column4 = dataGridView9.Columns[3];
            dataGridView9.Columns[3].HeaderText = "NHM";
            dataGridView9.Columns[3].Width = 400;



        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (chkTerminalski.Checked == false)
            {
                using (var detailForm = new UvozTable())
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
            else
            {
                using (var detailForm = new UvozTable(1))
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
            FillDGUsluge();
            FillDGUvoznici();
            FillDG2();
            FillNHM();
            
        }

        private void button26_Click(object sender, EventArgs e)
        {
            int TcbBrod = Convert.ToInt32(cbBrod.SelectedValue);
            int TtxtTipKont = Convert.ToInt32(txtTipKont.SelectedValue);
            int TcboRLTerminal = Convert.ToInt32(cboRLTerminal.SelectedValue);
            int TcboRLTerminal2 = Convert.ToInt32(cboRLTerminal2.SelectedValue);
            int TcboRLTerminal3 = Convert.ToInt32(cboRLTerminal3.SelectedValue);
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
           // int TtxtUvoznik = Convert.ToInt32(cboUvoznik.SelectedValue);
         
            FillCombo();
            cbBrod.SelectedValue = TcbBrod;
            txtTipKont.SelectedValue = TtxtTipKont;
            cboRLTerminal.SelectedValue = TcboRLTerminal;
            cboRLTerminal2.SelectedValue = TcboRLTerminal2;
            cboRLTerminal3.SelectedValue = TcboRLTerminal3;
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
            InsertUvozKonacna uvK = new InsertUvozKonacna();
            uvK.InsUbaciUslugu(Convert.ToInt32(txtID.Text), 69, 0, 1, 4, Convert.ToInt32(cboBrodar.SelectedValue), 0, "GATE IN EMPTY", 13, KorisnikTekuci, "GATE IN KAMION");

            FillDGUsluge();
            InsertScenario isc = new InsertScenario();
            isc.UpdScenarioKontejnera(15, Convert.ToInt32(txtID.Text), 1, 0);

            InsertUvozKonacna ins = new InsertUvozKonacna();
            ins.PrenesiUPlanUtovara(Convert.ToInt32(txtID.Text), Convert.ToInt32(cboPlanUtovara.SelectedValue));

        


            if (chkTerminalski.Checked == true)
            {
                DialogResult dialogResult = MessageBox.Show("Pokrenuli ste proceduru pravljenja Terminalskog naloga, kontejner je pridruzen planu i RNI ", "Radni nalog", MessageBoxButtons.YesNo);
                int PostojeRn = 0;
                PostojeRn = VratiPostojeceRN();
                if (dialogResult == DialogResult.Yes)
                {

                    InsertRadniNalogInterni insRNI = new InsertRadniNalogInterni();
                    insRNI.InsRadniNalogInterni(Convert.ToInt32(4), Convert.ToInt32(4), Convert.ToDateTime(DateTime.Now), Convert.ToDateTime("1.1.1900. 00:00:00"), "", Convert.ToInt32(0), "PlanUtovara", Convert.ToInt32(cboPlanUtovara.SelectedValue), KorisnikTekuci, "");

                }
                else
                {
                    // FormirajOpstiExcel();
                }
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void VratiTaraKontejnera()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Tara from TipKontenjera where ID = " + Convert.ToInt32(txtTipKont.SelectedValue), con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtTaraK.Value = Convert.ToDecimal(dr["Tara"].ToString());

            }
            con.Close();


        }


        private void txtTipKont_Leave(object sender, EventArgs e)
        {
            VratiTaraKontejnera();
        }

        private void tabSplitterPage1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button19_Click_1(object sender, EventArgs e)
        {
            using (var detailForm = new Dokumenta.frmKontaktOsobe(Convert.ToInt32(cboNalogodavac3.SelectedValue)))
            {
                detailForm.ShowDialog();

                txtMail.Text = detailForm.GetKontaktMail(Convert.ToInt32(cboNalogodavac3.SelectedValue));
            }
        }

        private void cbOcarina_Click(object sender, EventArgs e)
        {

        }

        private void button28_Click(object sender, EventArgs e)
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

        private void chkInterni_Click(object sender, EventArgs e)
        {

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

        private void chkCekaSeKlijent_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCekaSeKlijent.Checked == true)
            {
                chkPotvrdioKlijent2BDI.Checked = false;
                chkPotvrdioKlijent.Checked = false;
                dtpPotvrdioKlijent.Value = DateTime.Now;
            }
           
        }

        private void chkPotvrdioKlijent2BDI_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPotvrdioKlijent2BDI.Checked == true)
            {
                chkCekaSeKlijent.Checked = false;
                chkPotvrdioKlijent.Checked = false;
                dtpPotvrdioKlijent.Value = DateTime.Now;
            }
        }

        private void chkPotvrdioKlijent_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPotvrdioKlijent.Checked == true)
            {
                chkCekaSeKlijent.Checked = false;
                chkPotvrdioKlijent2BDI.Checked = false;
                dtpPotvrdioKlijent.Value = DateTime.Now;
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            UvozDokumenta uvdok = new UvozDokumenta(txtID.Text, "0");
            uvdok.Show();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            UvozNHM uNHM = new UvozNHM(txtID.Text, 0);
            uNHM.Show();
        }

        private void chkDobijenNalogBrodara_Click(object sender, EventArgs e)
        {
            if (chkDobijenNalogBrodara.Checked == true)
            {
                dtNalogBrodara.Value = DateTime.Now;
            }
        }

        private void chkDobijenNalogodavac1_Click(object sender, EventArgs e)
        {
            if (chkDobijenNalogodavac1.Checked == true)
            {
                dtpDobijenNalogodavac1.Value = DateTime.Now;
            }
        }

        private void chkDobijenNalogodavac2_Click(object sender, EventArgs e)
        {
            if (chkDobijenNalogodavac2.Checked == true)
            {
                dtpDobijenNalogodavac2.Value = DateTime.Now;
            }
        }

        private void chkDobijenNalogodavac3_Click(object sender, EventArgs e)
        {
            if (chkDobijenNalogodavac3.Checked == true)
            {
                dtpDobijenNalogodavac3.Value = DateTime.Now;
            }
        }

        private void chkSlobodan_CheckedChanged(object sender, EventArgs e)
        {
            dtpSlobodanDaNapusti.Value = DateTime.Now;
        }

        private void button31_Click(object sender, EventArgs e)
        {
            frmUvozUvoznici uuv = new frmUvozUvoznici(txtID.Text,0);
            uuv.Show();
        }

        private void button17_Click_1(object sender, EventArgs e)
        {
            UvozKonacnaPronadji ukp = new UvozKonacnaPronadji();
            ukp.Show();
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
                FillDGUsluge();
                FillDGUvoznici();
                FillDG2();
                FillNHM();
            }
     


            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            frmPregledKontejneraDrumskeUsluge ppDU = new frmPregledKontejneraDrumskeUsluge(Convert.ToInt32(0), Convert.ToInt32(txtID.Text));
            ppDU.Show();
        }

        private void chkDrumskaUsluga_CheckedChanged(object sender, EventArgs e)
        {
           
            if (chkDrumskaUsluga.Checked == true)
            {
                InsertUvozKonacna uvK = new InsertUvozKonacna();
                uvK.InsUbaciUslugu(Convert.ToInt32(txtID.Text), 84, 0, 1, 6, 0, 0, "/", 0, KorisnikTekuci, "/");

            }
            else
            {
                InsertUvozKonacna uvK = new InsertUvozKonacna();
                uvK.DelUvozUslugaKamionska(Convert.ToInt32(txtID.Text));
            }
           
        }
    }
}
