using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

using Testiranje.Sifarnici;
using Testiranje.Dokumeta;
using Saobracaj.Sifarnici;
using TrackModal.Dokumeta;
using TrackModal.Izvestaji;
using TrackModal.Promet;
using System.IO;

using System.Diagnostics;
using Saobracaj.eDokumenta;
using Saobracaj.RadniNalozi;
using Syncfusion.Windows.Forms.Diagram;

namespace Saobracaj
{
    public partial class MainP : Syncfusion.Windows.Forms.Tools.RibbonForm
    {
        string Korisnik = "";
        public bool PravoP;
        public MainP()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            InitializeComponent();
        }

        public MainP(string Logovan, int Lozinka)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            InitializeComponent();
            Korisnik = Logovan;
            if (Sifarnici.frmLogovanje.Firma == "Leget")
            {
                toolStripTabItem10.Visible = true;
                toolStripTabItem11.Visible = true;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmStanice")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Sifarnici.frmStanice stan = new Sifarnici.frmStanice();
                PravoP = stan.Pravo;
                if (PravoP == true) { stan.Show(); } else { return; }
            }



           
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmTrase")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Sifarnici.frmTrase trase = new Sifarnici.frmTrase();
                PravoP = trase.Pravo;
                if (PravoP == true) { trase.Show(); } else { return; }
            }


           
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Sifarnici.frmPruge prug = new Sifarnici.frmPruge();
            PravoP = prug.Pravo;
            if (PravoP == true) { prug.Show(); } else { return; }
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmLokomotive")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {

                Sifarnici.frmLokomotive lokomotive = new Sifarnici.frmLokomotive();
                PravoP = lokomotive.Pravo;
                if (PravoP == true) { lokomotive.Show(); } else { return; }
            }


        }
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmSerijeLokomotiva")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {

                Sifarnici.frmSerijeLokomotiva serije = new Sifarnici.frmSerijeLokomotiva();
                PravoP = serije.Pravo;
                if (PravoP == true) { serije.Show(); } else { return; }
            }



          
        }
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmStatusVoza")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Sifarnici.frmStatusVoza statvoz = new Sifarnici.frmStatusVoza();
                PravoP = statvoz.Pravo;
                if (PravoP == true) { statvoz.Show(); } else { return; }
            }

        }
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmRazlozi")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Sifarnici.frmRazlozi raz = new Sifarnici.frmRazlozi();
                PravoP = raz.Pravo;
                if (PravoP == true) { raz.Show(); } else { return; }
            }


          
        }
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmTipPrevoza")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Sifarnici.frmTipPrevoza tipP = new Sifarnici.frmTipPrevoza();
                PravoP = tipP.Pravo;
                if (PravoP == true) { tipP.Show(); } else { return; }
            }


         
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
        
        }
        private void toolStripButton10_Click(object sender, EventArgs e)
        {
           
        }
        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPartnerji")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Sifarnici.frmPartnerji part = new Sifarnici.frmPartnerji();
                PravoP = part.Pravo;
                if (PravoP == true) { part.Show(); } else { return; }
            }



           
        }
        private void toolStripButton12_Click(object sender, EventArgs e)
        {
         
        }
        private void toolStripButton13_Click(object sender, EventArgs e)
        {
          
        }
        //Priprema
        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmNajava")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmNajava frmNaj = new Dokumenta.frmNajava(Korisnik, 0);
                PravoP = frmNaj.Pravo;
                if (PravoP == true) { frmNaj.Show(); } else { return; }
            }


           
        }
        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmNajava")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmNajava frmNaj = new Dokumenta.frmNajava(Korisnik, 1);
                PravoP = frmNaj.Pravo;
                if (PravoP == true) { frmNaj.Show(); } else { return; }
            }


           
        }
        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmNajava20")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmNajava20 naj = new Dokumenta.frmNajava20();
                PravoP = naj.Pravo;
                if (PravoP == true) { naj.Show(); } else { return; }
            }



           
        }
        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmNajavaArhivaAnaliza")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                SyncForm.frmNajavaArhivaAnaliza sNA = new SyncForm.frmNajavaArhivaAnaliza();
                PravoP = sNA.Pravo;
                if (PravoP == true) { sNA.Show(); } else { return; }
            }



          
        }
        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmAnalizaPorudzbina")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                SyncForm.frmAnalizaPorudzbina apor = new SyncForm.frmAnalizaPorudzbina();
                PravoP = apor.Pravo;
                if (PravoP == true) { apor.Show(); } else { return; }
            }


          
        }
        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmTeretnicePregled")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmTeretnicePregled frmTer = new Dokumenta.frmTeretnicePregled(Korisnik);
                PravoP = frmTer.Pravo;
                if (PravoP == true) { frmTer.Show(); } else { return; }
            }

           
        }
        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmIskljuceniVagoni")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmIskljuceniVagoni iv = new Dokumenta.frmIskljuceniVagoni();
                PravoP = iv.Pravo;
                if (PravoP == true) { iv.Show(); } else { return; }
            }


            
        }
        private void toolStripButton21_Click(object sender, EventArgs e)
        {
            
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmNajaveBezTeretnice")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmNajaveBezTeretnice nbt = new Dokumenta.frmNajaveBezTeretnice();
                PravoP = nbt.Pravo;
                if (PravoP == true) { nbt.Show(); } else { return; }
            }
        }
        private void toolStripButton22_Click(object sender, EventArgs e)
        {
            Dokumenta.frmPregledRID prid = new Dokumenta.frmPregledRID();
            PravoP = prid.Pravo;
            if (PravoP == true) { prid.Show(); } else { return; }
        }
        private void toolStripButton23_Click(object sender, EventArgs e)
        {
            Dokumenta.frmRIDPoNajavama rpn = new Dokumenta.frmRIDPoNajavama();
            PravoP = rpn.Pravo;
            if (PravoP == true) { rpn.Show(); } else { return; }
        }
        private void toolStripButton24_Click(object sender, EventArgs e)
        {
            Dokumenta.frmRIDTeretnice fridter = new Dokumenta.frmRIDTeretnice();
            PravoP = fridter.Pravo;
            if (PravoP == true) { fridter.Show(); } else { return; }
        }
        private void toolStripButton25_Click(object sender, EventArgs e)
        {
          
        }
        private void toolStripButton27_Click(object sender, EventArgs e)
        {
            Dokumenta.frmMapa mapa = new Dokumenta.frmMapa();
            PravoP = mapa.Pravo;
            if (PravoP == true) { mapa.Show(); } else { return; }
        }
        private void toolStripButton28_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPregledRN")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmPregledRN prn = new Dokumenta.frmPregledRN();
                PravoP = prn.Pravo;
                if (PravoP == true) { prn.Show(); } else { return; }
            }


          
        }
        private void toolStripButton29_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmRadniNalogPregled")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmRadniNalogPregled frmp2 = new Dokumenta.frmRadniNalogPregled();
                PravoP = frmp2.Pravo;
                if (PravoP == true) { frmp2.Show(); } else { return; }
            }



          
          
        }
        private void toolStripButton30_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmRAdniNalogPregledPoLokomotivama")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmRAdniNalogPregledPoLokomotivama rnpl = new Dokumenta.frmRAdniNalogPregledPoLokomotivama();
                PravoP = rnpl.Pravo;
                if (PravoP == true) { rnpl.Show(); } else { return; }
            }



          
        }
        private void toolStripButton31_Click(object sender, EventArgs e)
        {
            Dokumenta.frmStampaRadnogNaloga srn = new Dokumenta.frmStampaRadnogNaloga();
            PravoP = srn.Pravo;
            if (PravoP == true) { srn.Show(); } else { return; }
        }
        private void toolStripButton33_Click(object sender, EventArgs e)
        {
            Dokumenta.frmRaspustiVagone frv = new Dokumenta.frmRaspustiVagone();
            PravoP = frv.Pravo;
            if (PravoP == true) { frv.Show(); } else { return; }
        }
        private void toolStripButton34_Click(object sender, EventArgs e)
        {
         
        }
        private void toolStripButton38_Click(object sender, EventArgs e)
        {
            Dokumenta.frmMUPDozvola Dozvola = new Dokumenta.frmMUPDozvola();
            PravoP = Dozvola.Pravo;
            if (PravoP == true) { Dozvola.Show(); } else { return; }
        }

        private void toolStripButton37_Click(object sender, EventArgs e)
        {
            Dokumenta.frmMupMesto mesto = new Dokumenta.frmMupMesto();
            PravoP = mesto.Pravo;
            if (PravoP == true) { mesto.Show(); } else { return; }
        }

        private void toolStripButton35_Click(object sender, EventArgs e)
        {
            Administracija.frmKorisnici kor = new Administracija.frmKorisnici();
            PravoP = kor.Pravo;
            if (PravoP == true) { kor.Show(); } else { return; }
        }

        private void toolStripButton36_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmAutomobili")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmAutomobili autom = new Dokumenta.frmAutomobili();
                PravoP = autom.Pravo;
                if (PravoP == true) { autom.Show(); } else { return; }
            }


          
        }

        private void toolStripButton39_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmRegistator")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmRegistator reg = new Dokumenta.frmRegistator();
                PravoP = reg.Pravo;
                if (PravoP == true) { reg.Show(); } else { return; }
            }



           
        }

        private void toolStripButton40_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmRegistratorPregled")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmRegistratorPregled regpre = new Dokumenta.frmRegistratorPregled();
                PravoP = regpre.Pravo;
                if (PravoP == true) { regpre.Show(); } else { return; }
            }


          
        }

        private void toolStripButton41_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmVrstePopisa")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Servis.frmVrstePopisa vrPopisa = new Servis.frmVrstePopisa();
                PravoP = vrPopisa.Pravo;
                if (PravoP == true) { vrPopisa.Show(); } else { return; }
            }



           
        }

        private void toolStripButton42_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmLokomotivaVrstaPopisa")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Servis.frmLokomotivaVrstaPopisa lokvp = new Servis.frmLokomotivaVrstaPopisa();
                PravoP = lokvp.Pravo;
                if (PravoP == true) { lokvp.Show(); } else { return; }
            }



          
        }

        private void toolStripButton43_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmGrupaKvarova")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Servis.frmGrupaKvarova gkv = new Servis.frmGrupaKvarova();
                PravoP = gkv.Pravo;
                if (PravoP == true) { gkv.Show(); } else { return; }
            }



          
        }

        private void toolStripButton44_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmKvarovi")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Servis.frmKvarovi kv = new Servis.frmKvarovi();
                PravoP = kv.Pravo;
                if (PravoP == true) { kv.Show(); } else { return; }
            }



          
        }

        private void toolStripButton45_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmEvidencijaKvarova")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Servis.frmEvidencijaKvarova pkvar = new Servis.frmEvidencijaKvarova();
                PravoP = pkvar.Pravo;
                if (PravoP == true) { pkvar.Show(); } else { return; }
            }



          
        }

        private void toolStripButton46_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmEvidencijaKvarovaAnaliza")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Servis.frmEvidencijaKvarovaAnaliza evkva = new Servis.frmEvidencijaKvarovaAnaliza();
                PravoP = evkva.Pravo;
                if (PravoP == true) { evkva.Show(); } else { return; }
            }



        }

        private void toolStripButton50_Click(object sender, EventArgs e)
        {
            Administracija.frmSistematizacija sistem = new Administracija.frmSistematizacija();
            PravoP = sistem.Pravo;
            if (PravoP == true) { sistem.Show(); } else { return; }
        }

        private void toolStripButton51_Click(object sender, EventArgs e)
        {
            Administracija.frmSistematizacijaPovezivanje sist = new Administracija.frmSistematizacijaPovezivanje();
            PravoP = sist.Pravo;
            if (PravoP == true) { sist.Show(); } else { return; }
        }

        private void toolStripButton52_Click(object sender, EventArgs e)
        {
            Sifarnici.frmVrsteAktivnosti vrakt = new Sifarnici.frmVrsteAktivnosti();
            PravoP = vrakt.Pravo;
            if (PravoP == true) { vrakt.Show(); } else { return; }
        }

        private void toolStripButton53_Click(object sender, EventArgs e)
        {
            Sifarnici.frmCenaPoRadniku cpr = new Sifarnici.frmCenaPoRadniku();
            if (Korisnik == "test")
            {
                cpr.Show();
            }
            else { MessageBox.Show("Nemate prava za pristup ovoj formi"); return; }
        }

        private void toolStripButton54_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPravoAktivnosti")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmPravoAktivnosti pravo = new Dokumenta.frmPravoAktivnosti();
                PravoP = pravo.Pravo;
                if (PravoP == true) { pravo.Show(); } else { return; }
            }



         
        }

        private void toolStripButton55_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPravoAktivnostiViseRadnika")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {

                Dokumenta.frmPravoAktivnostiViseRadnika pravovise = new Dokumenta.frmPravoAktivnostiViseRadnika();
                PravoP = pravovise.Pravo;
                if (PravoP == true) { pravovise.Show(); } else { return; }
            }



        }

        private void toolStripButton72_Click(object sender, EventArgs e)
        {
            Dokumenta.frmIzracunZarada fiz = new Dokumenta.frmIzracunZarada();
            PravoP = fiz.Pravo;
            if (PravoP == true) { fiz.Show(); } else { return; }
        }

        private void toolStripButton73_Click(object sender, EventArgs e)
        {
            Dokumenta.frmPlacenoNeplaceno pln = new Dokumenta.frmPlacenoNeplaceno();
            PravoP = pln.Pravo;
            if (PravoP == true) { pln.Show(); } else { return; }
        }

        private void toolStripButton74_Click(object sender, EventArgs e)
        {
            Dokumenta.frmOsnovnaZarada osnzar = new Dokumenta.frmOsnovnaZarada();
            PravoP = osnzar.Pravo;
            if (PravoP == true) { osnzar.Show(); } else { return; }
        }

        private void toolStripButton69_Click(object sender, EventArgs e)
        {
            Dokumenta.frmBrojSmena brsm = new Dokumenta.frmBrojSmena();
            PravoP = brsm.Pravo;
            if (PravoP == true) { brsm.Show(); } else { return; }
        }

        private void toolStripButton57_Click(object sender, EventArgs e)
        {
            Dokumenta.frmEvidencijaGodišnjihOdmora evgo = new Dokumenta.frmEvidencijaGodišnjihOdmora();
            PravoP = evgo.Pravo;
            if (PravoP == true) { evgo.Show(); } else { return; }
        }

        private void toolStripButton60_Click(object sender, EventArgs e)
        {
            Mobile.frmPrijavaSmene prodj = new Mobile.frmPrijavaSmene();
            PravoP = prodj.Pravo;
            if (PravoP == true) { prodj.Show(); } else { return; }
        }

        private void toolStripButton61_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPregledMobilni")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {

                SyncForm.frmPregledMobilni drugi = new SyncForm.frmPregledMobilni();
                PravoP = drugi.Pravo;
                if (PravoP == true) { drugi.Show(); } else { return; }
            }



           
        }

        private void toolStripButton62_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPrijavaMasinovodje")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Servis.frmPrijavaMasinovodje mas = new Servis.frmPrijavaMasinovodje();
                PravoP = mas.Pravo;
                if (PravoP == true) { mas.Show(); } else { return; }
            }


          
        }

        private void toolStripButton63_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPregledMasinovodje")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                SyncForm.frmPregledMasinovodje novi = new SyncForm.frmPregledMasinovodje();
                PravoP = novi.Pravo;
                if (PravoP == true) { novi.Show(); } else { return; }
            }



           
        }

        private void toolStripButton48_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmNamirenja")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Servis.frmNamirenja namir = new Servis.frmNamirenja();
                PravoP = namir.Pravo;
                if (PravoP == true) { namir.Show(); } else { return; }
            }



          
        }

        private void toolStripButton49_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmNamirenjaSumarno")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Servis.frmNamirenjaSumarno namSum = new Servis.frmNamirenjaSumarno();
                PravoP = namSum.Pravo;
                if (PravoP == true) { namSum.Show(); } else { return; }
            }


           
        }

        private void toolStripButton58_Click(object sender, EventArgs e)
        {
        
        }

        private void toolStripButton59_Click(object sender, EventArgs e)
        {
          
        }

        private void toolStripButton56_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmSlobodniDani")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Mobile.frmSlobodniDani slob = new Mobile.frmSlobodniDani(Korisnik);
                PravoP = slob.Pravo;
                if (PravoP == true) { slob.Show(); } else { return; }
            }



          
        }

        private void toolStripButton77_Click(object sender, EventArgs e)
        {
            Administracija.InsertAdministracije upd = new Administracija.InsertAdministracije();
            upd.UpdateNull();
            MessageBox.Show("Gotovo zatvori");
        }

        private void toolStripButton78_Click(object sender, EventArgs e)
        {
            Dokumenta.frmPutniNaloziPregled fpn = new Dokumenta.frmPutniNaloziPregled();
            PravoP = fpn.Pravo;
            if (PravoP == true) { fpn.Show(); } else { return; }
        }

        private void toolStripButton79_Click(object sender, EventArgs e)
        {
            Dokumenta.frmEvidencijaRAdaNeplaceno nepl = new Dokumenta.frmEvidencijaRAdaNeplaceno();
            PravoP = nepl.Pravo;
            if (PravoP == true) { nepl.Show(); } else { return; }
        }

        private void toolStripButton80_Click(object sender, EventArgs e)
        {
            Dokumenta.frmFinansije novak = new Dokumenta.frmFinansije();
            PravoP = novak.Pravo;
            if (PravoP == true) { novak.Show(); } else { return; }
        }

        private void toolStripButton65_Click(object sender, EventArgs e)
        {
            SyncForm.frmPlanRada planrada = new SyncForm.frmPlanRada();
            PravoP = planrada.Pravo;
            if (PravoP == true) { planrada.Show(); } else { return; }
        }

        private void toolStripButton67_Click(object sender, EventArgs e)
        {
            Dokumenta.frmEvidencijaRada erM = new Dokumenta.frmEvidencijaRada(Korisnik);
            PravoP = erM.Pravo;
            if (PravoP == true) { erM.Show(); } else { return; }
        }

        private void toolStripButton68_Click(object sender, EventArgs e)
        {

            Dokumenta.frmZarade zar = new Dokumenta.frmZarade();
            if (Korisnik == "admin")
            {
                zar.Show();
            }
            else { MessageBox.Show("Nemate prava za pristup ovoj formi"); return; }
        }
        private void toolStripButton70_Click(object sender, EventArgs e)
        {
            Dokumenta.frmEvidencijaRadaZaglavlje erz = new Dokumenta.frmEvidencijaRadaZaglavlje(Korisnik);
            PravoP = erz.Pravo;
            if (PravoP == true) { erz.Show(); } else { return; }
        }

        private void toolStripButton71_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmAnalizaAktivnosti")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                SyncForm.frmAnalizaAktivnosti aAktiv = new SyncForm.frmAnalizaAktivnosti();
                PravoP = aAktiv.Pravo;
                if (PravoP == true) { aAktiv.Show(); } else { return; }
            }





          
        }
        private void toolStripButton75_Click(object sender, EventArgs e)
        {
            Dokumenta.frmParametriObracuna parobr = new Dokumenta.frmParametriObracuna();
            PravoP = parobr.Pravo;
            if (PravoP == true) { parobr.Show(); } else { return; }
        }
        private void toolStripButton76_Click(object sender, EventArgs e)
        {
            Dokumenta.frmZarade zar = new Dokumenta.frmZarade();
            if (Korisnik == "admin")
            {
                zar.Show();
            }
            else { MessageBox.Show("Nemate prava za pristup ovoj formi"); return; }
        }
        private void toolStripButton81_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmTehnologija")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Tehnologija.frmTehnologija teh = new Tehnologija.frmTehnologija(Korisnik);
                PravoP = teh.Pravo;
                if (PravoP == true) { teh.Show(); } else { return; }
            }



           
        }
        private void toolStripButton82_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmTraseAnaliticki")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Sifarnici.frmTraseAnaliticki ta = new Sifarnici.frmTraseAnaliticki();
                PravoP = ta.Pravo;
                if (PravoP == true) { ta.Show(); } else { return; }
            }


          
        }
        private void toolStripButton83_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmTehnologijaPregled")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {

                Tehnologija.frmTehnologijaPregled tehp = new Tehnologija.frmTehnologijaPregled(Korisnik);
                PravoP = tehp.Pravo;
                if (PravoP == true) { tehp.Show(); } else { return; }
            }



        }
        private void toolStripButton84_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPrijavaKvara")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Servis.frmPrijavaKvara pkv = new Servis.frmPrijavaKvara();
                PravoP = pkv.Pravo;
                if (PravoP == true) { pkv.Show(); } else { return; }
            }



           
        }
        private void RibbonPanel_Click(object sender, EventArgs e)
        {
            //  Sifarnici.frmSifarnikKontrolnihGresaka skg = new Sifarnici.frmSifarnikKontrolnihGresaka();
            // skg.Show();
        }
        private void toolStripButton85_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmSifarnikKontrolnihGresaka")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Sifarnici.frmSifarnikKontrolnihGresaka skg = new Sifarnici.frmSifarnikKontrolnihGresaka();
                PravoP = skg.Pravo;
                if (PravoP == true) { skg.Show(); } else { return; }
            }



          
        }
        private void toolStripButton86_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmKontrola")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {

                Dokumenta.frmKontrola kontrola = new Dokumenta.frmKontrola();
                PravoP = kontrola.Pravo;
                if (PravoP == true) { kontrola.Show(); } else { return; }
            }



        }
        private void toolStripButton87_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmSifarnikKontrolnihTipovaDokumenta")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Sifarnici.frmSifarnikKontrolnihTipovaDokumenta kgtp = new Sifarnici.frmSifarnikKontrolnihTipovaDokumenta();
                PravoP = kgtp.Pravo;
                if (PravoP == true) { kgtp.Show(); } else { return; }
            }


         
        }
        private void toolStripButton88_Click(object sender, EventArgs e)
        {
            Dokumenta.frmCentralnaTablaRN cen = new Dokumenta.frmCentralnaTablaRN();
            PravoP = cen.Pravo;
            if (PravoP == true) { cen.Show(); } else { return; }
        }
        private void toolStripButton91_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPregledPopisa1")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Servis.frmPregledPopisa1 pp = new Servis.frmPregledPopisa1();
                PravoP = pp.Pravo;
                if (PravoP == true) { pp.Show(); } else { return; }
            }


          
        }
        private void toolStripButton92_Click(object sender, EventArgs e)
        {

            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPregledPopisa2")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                SyncForm.frmPregledPopisa2 ppp = new SyncForm.frmPregledPopisa2();
                PravoP = ppp.Pravo;
                if (PravoP == true) { ppp.Show(); } else { return; }
            }



          
        }

        private void toolStripButton93_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPredjenaKilometrazaLokomotiva")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmPredjenaKilometrazaLokomotiva predjKM = new Dokumenta.frmPredjenaKilometrazaLokomotiva();
                PravoP = predjKM.Pravo;
                if (PravoP == true) { predjKM.Show(); } else { return; }
            }


         
        }

        private void toolStripButton94_Click(object sender, EventArgs e)
        {
            Dokumenta.frmCentralnaTablaCpajal cpajak = new Dokumenta.frmCentralnaTablaCpajal();
            PravoP = cpajak.Pravo;
            if (PravoP == true) { cpajak.Show(); } else { return; }
        }

        private void toolStripButton95_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmGrupaKvarovaAuto")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Servis.frmGrupaKvarovaAuto gka = new Servis.frmGrupaKvarovaAuto();
                PravoP = gka.Pravo;
                if (PravoP == true) { gka.Show(); } else { return; }
            }



        }

        private void toolStripButton96_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmKvaroviAuto")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Servis.frmKvaroviAuto kvaut = new Servis.frmKvaroviAuto();
                PravoP = kvaut.Pravo;
                if (PravoP == true) { kvaut.Show(); } else { return; }
            }



           
        }

        private void toolStripButton99_Click(object sender, EventArgs e)
        {
            Dokumenta.frmEvidencijaRadaPromene prom = new Dokumenta.frmEvidencijaRadaPromene();
            PravoP = prom.Pravo;
            if (PravoP == true) { prom.Show(); } else { return; }
        }

        private void toolStripButton100_Click(object sender, EventArgs e)
        {
          
        }

        private void toolStripButton101_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripButton103_Click(object sender, EventArgs e)
        {
            Dokumenta.frmObracunFiksni fiksniobr = new Dokumenta.frmObracunFiksni();
            PravoP = fiksniobr.Pravo;
            if (PravoP == true) { fiksniobr.Show(); } else { return; }
        }

        private void toolStripButton104_Click(object sender, EventArgs e)
        {
            Testiranje.Pitanja pitanja = new Testiranje.Pitanja();
            PravoP = pitanja.Pravo;
            if (PravoP == true) { pitanja.Show(); } else { return; }
        }

        private void ToolStripButton105_Click(object sender, EventArgs e)
        {
            Testiranje.frmGenerisanjeTestaKorisnik generisanje = new Testiranje.frmGenerisanjeTestaKorisnik();
            PravoP = generisanje.Pravo;
            if (PravoP == true) { generisanje.Show(); } else { return; }
        }

        private void toolStripButton106_Click(object sender, EventArgs e)
        {
            Testiranje.TestiranjeStampa tesS = new Testiranje.TestiranjeStampa();
            PravoP = tesS.Pravo;
            if (PravoP == true) { tesS.Show(); } else { return; }
        }

        private void toolStripButton107_Click(object sender, EventArgs e)
        {
            Testiranje.Obrasci tes6 = new Testiranje.Obrasci();
            PravoP = tes6.Pravo;
            if (PravoP == true) { tes6.Show(); } else { return; }
        }

        private void toolStripButton108_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmAutomobili")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmAutomobili auto = new Dokumenta.frmAutomobili();
                PravoP = auto.Pravo;
                if (PravoP == true) { auto.Show(); } else { return; }
            }




          
        }

        private void toolStripButton109_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmAutomobiliPregledPrijava")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmAutomobiliPregledPrijava prijaveautomobili = new Dokumenta.frmAutomobiliPregledPrijava();
                PravoP = prijaveautomobili.Pravo;
                if (PravoP == true) { prijaveautomobili.Show(); } else { return; }
            }



          
        }

        private void toolStripButton97_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmAutomobiliKvarovi")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmAutomobiliKvarovi kvarovi = new Dokumenta.frmAutomobiliKvarovi();
                PravoP = kvarovi.Pravo;
                if (PravoP == true) { kvarovi.Show(); } else { return; }
            }



         
        }

        private void toolStripButton110_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPrijavaKvaraAuto")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Servis.frmPrijavaKvaraAuto prijavaKvarAuto = new Servis.frmPrijavaKvaraAuto();
                PravoP = prijavaKvarAuto.Pravo;
                if (PravoP == true) { prijavaKvarAuto.Show(); } else { return; }
            }


           
        }

        private void toolStripButton111_Click(object sender, EventArgs e)
        {
          

        }

        private void toolStripButton112_Click(object sender, EventArgs e)
        {
            Administracija.frmKreirajGrupu kreirajGrupu = new Administracija.frmKreirajGrupu();
            PravoP = kreirajGrupu.Pravo;
            if (PravoP == true) { kreirajGrupu.Show(); } else { return; }
        }

        private void toolStripButton113_Click(object sender, EventArgs e)
        {
            Administracija.frmDodeliGrupu dodeliGrupu = new Administracija.frmDodeliGrupu();
            PravoP = dodeliGrupu.Pravo;
            if (PravoP == true) { dodeliGrupu.Show(); } else { return; }
        }

        private void toolStripButton114_Click(object sender, EventArgs e)
        {
            Administracija.frmForme forme = new Administracija.frmForme();
            PravoP = forme.Pravo;
            if (PravoP == true) { forme.Show(); } else { return; }
        }

        private void toolStripButton115_Click(object sender, EventArgs e)
        {
            Administracija.frmFormePrava formePrava = new Administracija.frmFormePrava();
            PravoP = formePrava.Pravo;
            if (PravoP == true)
            {
                formePrava.Show();
            }
            else { return; }
        }

        private void toolStripButton26_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton32_Click(object sender, EventArgs e)
        {
            frmPravljenjeVoza pv = new frmPravljenjeVoza();
            pv.Show();
        }

        private void toolStripButton64_Click(object sender, EventArgs e)
        {
            //Tokovi dokumerntacije
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmTokoviDokumentacije")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Mobile.frmTokoviDokumentacije td = new Mobile.frmTokoviDokumentacije();
                td.Show();
            }
          
        }

        private void toolStripButton66_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton47_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton98_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton116_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPropratnice")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmPropratnice propratnice = new Dokumenta.frmPropratnice();
                PravoP = propratnice.Pravo;
                if (PravoP == true)
                {
                    propratnice.Show();
                }
                else { return; }
            }



          
        }
        private void toolStripButton117_Click(object sender, EventArgs e)
        {
            Administracija.frmPolozenePruge pruge = new Administracija.frmPolozenePruge();
            PravoP = pruge.Pravo;
            if (PravoP == true)
            {
                pruge.Show();
            }
            else { return; }
        }
        private void toolStripButton118_Click(object sender, EventArgs e)
        {
            Administracija.frmPolozeneLokomotive lokomotive = new Administracija.frmPolozeneLokomotive();
            PravoP = lokomotive.Pravo;
            if (PravoP == true)
            {
                lokomotive.Show();
            }
            else { return; }
        }

        private void toolStripTabItem2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton119_Click(object sender, EventArgs e)
        {
            Dokumenta.frmMailNajava mail = new Dokumenta.frmMailNajava();
            mail.Show();
        }

        private void toolStripEx37_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripEx36_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton120_Click(object sender, EventArgs e)
        {
            // Saobracaj.Testiranje.Sifarnici.Testiranje
            // Testiranje.Sifarnici
           
              frmGreske greske = new frmGreske(Korisnik);
            PravoP = greske.Pravo;
            if (PravoP == true)
            {
                greske.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton121_Click(object sender, EventArgs e)
        {
           frmDelovi snac = new frmDelovi(Korisnik);
            PravoP = snac.Pravo;
            if (PravoP == true)
            {
                snac.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton122_Click(object sender, EventArgs e)
        {

            frmCene frmcen = new frmCene(Korisnik);
            PravoP = frmcen.Pravo;
            if (PravoP == true)
            {
                frmcen.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton123_Click(object sender, EventArgs e)
        {
            frmTipCenovnika frmTC = new frmTipCenovnika(Korisnik);
            PravoP = frmTC.Pravo;
            if (PravoP == true)
            {
                frmTC.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton124_Click(object sender, EventArgs e)
        {
            // frmKomitent komitenti = new frmKomitent(Korisnik);
            frmPartnerji komitenti = new frmPartnerji();
            PravoP = komitenti.Pravo;
            if (PravoP == true)
            {
                komitenti.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton125_Click(object sender, EventArgs e)
        {
            frmNacinDolaskaOdlaska nac = new frmNacinDolaskaOdlaska(Korisnik);
            PravoP = nac.Pravo;
            if (PravoP == true)
            {
                nac.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton126_Click(object sender, EventArgs e)
        {
            frmStatusRobe snac = new frmStatusRobe(Korisnik);
            PravoP = snac.Pravo;
            if (PravoP == true)
            {
                snac.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton127_Click(object sender, EventArgs e)
        {

            frmDelavci delav = new frmDelavci();
            PravoP = delav.Pravo;
            if (PravoP == true)
            {
                delav.Show();
            }
            else
            {
                return;
            }
            //frmZaposleni zap = new frmZaposleni(Korisnik);
            //zap.Show();
        }

        private void toolStripButton129_Click(object sender, EventArgs e)
        {
            frmVrstaRobe vr = new frmVrstaRobe(Korisnik);
            PravoP = vr.Pravo;
            if (PravoP == true)
            {
                vr.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton130_Click(object sender, EventArgs e)
        {
            frmTipKontejnera tkr = new frmTipKontejnera(Korisnik);
            PravoP = tkr.Pravo;
            if (PravoP == true)
            {
                tkr.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton131_Click(object sender, EventArgs e)
        {
            //Ovde mi trebaju Vozila iz Track modal
            // frmVozila vozila = new frmVozila(Korisnik);
            // vozila.Show();

            frmSredstvoRada sr = new frmSredstvoRada();
            PravoP = sr.Pravo;
            if (PravoP == true)
            {
                sr.Show();
            }
            else
            {
                return;
            }
           
           // Testiranje.Dokumeta.frmVozila vozila = new Dokumeta.frmVozila(Korisnik);
          //  vozila.Show();
        }

        private void toolStripButton132_Click(object sender, EventArgs e)
        {

            Sifarnici.frmStanice stan = new Sifarnici.frmStanice();
            stan.Show(); 
            // frmStanice stanice = new frmStanice(Korisnik);
            // stanice.Show();
        }

        private void toolStripButton133_Click(object sender, EventArgs e)
        {
            frmPozicija poz = new frmPozicija(Korisnik);
            PravoP = poz.Pravo;
            if (PravoP == true)
            {
                poz.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton134_Click(object sender, EventArgs e)
        {
            frmSkladista sklad = new frmSkladista(Korisnik);
            PravoP = sklad.Pravo;
            if (PravoP == true)
            {
                sklad.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton135_Click(object sender, EventArgs e)
        {
           frmVrstaManipulacije frmvrman = new frmVrstaManipulacije(Korisnik);
            PravoP = frmvrman.Pravo;
            if (PravoP == true)
            {
                frmvrman.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton136_Click(object sender, EventArgs e)
        {
            // frmPredefinisanePoruke predefinisane = new frmPredefinisanePoruke();
            // predefinisane.Show();

            Uvoz.frmVrstePostupakaUvoz fvp = new Uvoz.frmVrstePostupakaUvoz();
            fvp.Show();
        }

        private void toolStripButton137_Click(object sender, EventArgs e)
        {
            frmPregledVozova pvoz = new frmPregledVozova(Korisnik);
            PravoP = pvoz.Pravo;
            if (PravoP == true)
            {
                pvoz.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton138_Click(object sender, EventArgs e)
        {
            frmVoz vozovi = new frmVoz(Korisnik);
            PravoP = vozovi.Pravo;
            if (PravoP == true)
            {
                vozovi.Show();
            }
            else { return; }
        }

        private void toolStripButton139_Click(object sender, EventArgs e)
        {
          
            frmPrijemKontejneraKamionPregled prkamion = new frmPrijemKontejneraKamionPregled(Korisnik);
            PravoP = prkamion.Pravo;
            if (PravoP == true)
            {
                prkamion.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton140_Click(object sender, EventArgs e)
        {
            frmPrijemVozomPregled voz = new frmPrijemVozomPregled(Korisnik);
            PravoP = voz.Pravo;
            if (PravoP == true)
            {
                voz.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton141_Click(object sender, EventArgs e)
        {
            frmBukingVoza buking = new frmBukingVoza(Korisnik);
            PravoP = buking.Pravo;
            if (PravoP == true)
            {
                buking.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton142_Click(object sender, EventArgs e)
        {
            frmPregledOtpreme otprema = new frmPregledOtpreme(Korisnik);
            PravoP = otprema.Pravo;
            if (PravoP == true) { otprema.Show(); } else { return; }
        }

        private void toolStripButton143_Click(object sender, EventArgs e)
        {
            frmPregledOtpremeKamionom pkam = new frmPregledOtpremeKamionom(Korisnik);
            PravoP = pkam.Pravo;
            if (PravoP == true) { pkam.Show(); } else { return; }
        }

        private void toolStripButton144_Click(object sender, EventArgs e)
        {
            frmPregledNaloziZaPrevoz preg = new frmPregledNaloziZaPrevoz();
            PravoP = preg.Pravo;
            if (PravoP == true) { preg.Show(); } else { return; }
        }

        private void toolStripButton145_Click(object sender, EventArgs e)
        {
            Saobracaj.Dokumenta.frmPutniNalog put = new Saobracaj.Dokumenta.frmPutniNalog(Korisnik);
            PravoP = put.Pravo;
            if (PravoP == true)
            {
                put.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton146_Click(object sender, EventArgs e)
        {
            frmPregledTovarnihListova ptl = new frmPregledTovarnihListova();
            PravoP = ptl.Pravo;
            if (PravoP == true) { ptl.Show(); } else { return; }
        }

        private void toolStripButton147_Click(object sender, EventArgs e)
        {
            Saobracaj.Dokumenta.frmManipulacije man = new Saobracaj.Dokumenta.frmManipulacije(Korisnik);
            PravoP = man.Pravo;
            if (PravoP == true) { man.Show(); } else { return; }
        }

        private void toolStripButton148_Click(object sender, EventArgs e)
        {
            Saobracaj.Dokumenta.frmPregledNarucenihManipulacija pnman = new Saobracaj.Dokumenta.frmPregledNarucenihManipulacija(Korisnik);
            PravoP = pnman.Pravo;
            if (PravoP == true) { pnman.Show(); } else { return; }
        }

        private void toolStripButton149_Click(object sender, EventArgs e)
        {
            frmPregledManipulacijaPoPartneru mpp = new frmPregledManipulacijaPoPartneru();
            PravoP = mpp.Pravo;
            if (PravoP == true) { mpp.Show(); } else { return; };
        }

        private void toolStripButton150_Click(object sender, EventArgs e)
        {
            frmPregledSkladistePrijem ppr = new frmPregledSkladistePrijem(Korisnik);
            ppr.Show();
        }

        private void toolStripButton151_Click(object sender, EventArgs e)
        {
            frmSkladistePrijem spr = new frmSkladistePrijem(Korisnik);
            PravoP = spr.Pravo;
            if (PravoP == true) { spr.Show(); } else { return; }
        }

        private void toolStripButton152_Click(object sender, EventArgs e)
        {
            frmPregledMedjuskladisniPrenos pprmp = new frmPregledMedjuskladisniPrenos(Korisnik);
            PravoP = pprmp.Pravo;
            if (PravoP == true) { pprmp.Show(); } else { return; }
        }

        private void toolStripButton153_Click(object sender, EventArgs e)
        {
            frmMedjuskladisniPrenos mpr = new frmMedjuskladisniPrenos(Korisnik);
            PravoP = mpr.Pravo;
            if (PravoP == true) { mpr.Show(); } else { return; }
        }

        private void toolStripButton154_Click(object sender, EventArgs e)
        {
            frmSkladisteOtprema sklOtp = new frmSkladisteOtprema(Korisnik);
            PravoP = sklOtp.Pravo;
            if (PravoP == true) { sklOtp.Show(); } else { return; }
        }

        private void toolStripButton155_Click(object sender, EventArgs e)
        {
            frmPrometKontejnera prometkon = new frmPrometKontejnera(Korisnik);
            PravoP = prometkon.Pravo;
            if (PravoP == true) { prometkon.Show(); } else { return; }
        }

        private void toolStripButton156_Click(object sender, EventArgs e)
        {

            frmLager lager = new frmLager(Korisnik);
            PravoP = lager.Pravo;
            if (PravoP == true) { lager.Show(); } else { return; }
        }

        private void toolStripButton157_Click(object sender, EventArgs e)
        {
            frmPopis popis = new frmPopis(Korisnik);
            PravoP = popis.Pravo;
            if (PravoP == true) { popis.Show(); } else { return; }
        }

        private void toolStripButton158_Click(object sender, EventArgs e)
        {
            frmPopisPregled pl = new frmPopisPregled();
            PravoP = pl.Pravo;
            if (PravoP == true) { pl.Show(); } else { return; }
        }

        private void toolStripButton159_Click(object sender, EventArgs e)
        {
            frmLagerOperater lager = new frmLagerOperater();
            PravoP = lager.Pravo;
            if (PravoP == true) { lager.Show(); } else { return; }
        }

        private void toolStripButton160_Click(object sender, EventArgs e)
        {
            frmPregledNaloziZaPrevoz preg = new frmPregledNaloziZaPrevoz();
            PravoP = preg.Pravo;
            if (PravoP == true)
            {
                preg.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton161_Click(object sender, EventArgs e)
        {
            Saobracaj.Dokumenta.frmNalogZaPrevoz nzp = new Saobracaj.Dokumenta.frmNalogZaPrevoz(Korisnik);
            nzp.Show();
        }

        private void toolStripButton162_Click(object sender, EventArgs e)
        {
            frmPregledPutnihNaloga ppn = new frmPregledPutnihNaloga();
            ppn.Show();
        }

        private void toolStripButton167_Click(object sender, EventArgs e)
        {
            Saobracaj.Dokumenta.frmPutniNalog pn = new Saobracaj.Dokumenta.frmPutniNalog();
            PravoP = pn.Pravo;
            if (PravoP == true) { pn.Show(); } else { return; }
        }

        private void toolStripButton163_Click(object sender, EventArgs e)
        {
            frmPregledRadniNaloziTransport prnt = new frmPregledRadniNaloziTransport();
            prnt.Show();
        }

        private void toolStripButton164_Click(object sender, EventArgs e)
        {
            Saobracaj.Dokumenta.frmRadniNalogTransport rnt = new Saobracaj.Dokumenta.frmRadniNalogTransport();
            rnt.Show();
        }

        private void toolStripButton165_Click(object sender, EventArgs e)
        {
            Saobracaj.Dokumenta.frmAutoprevozniList2 apl = new Saobracaj.Dokumenta.frmAutoprevozniList2();
            apl.Show();
        }

        private void toolStripButton166_Click(object sender, EventArgs e)
        {
            frmPregledAutoPrevozniList pautp = new frmPregledAutoPrevozniList();
            pautp.Show();
        }

        private void Kvarovi_Click(object sender, EventArgs e)
        {
            frmSelectTransport6 kvarovi = new frmSelectTransport6();
            kvarovi.Show();
        }

        private void VozaciPoRutama_Click(object sender, EventArgs e)
        {
            frmSelectTransport5 trans = new frmSelectTransport5();
            trans.Show();
        }

        private void VozilaPoPN_Click(object sender, EventArgs e)
        {
            frmSelectTransport4 trans4 = new frmSelectTransport4();
            trans4.Show();
        }

        private void TurePoRelaciji_Click(object sender, EventArgs e)
        {
            frmSelectTransport3 trans3 = new frmSelectTransport3();
            trans3.Show();
        }

        private void PoTipuKontejnera_Click(object sender, EventArgs e)
        {
            frmSelectTransport2 trans2 = new frmSelectTransport2();
            trans2.Show();
        }

        private void KameniAgregat_Click(object sender, EventArgs e)
        {
            frmSelectTransport1 trans1 = new frmSelectTransport1();
            trans1.Show();
        }

        private void toolStripButton168_Click(object sender, EventArgs e)
        {
            frmMaticniPodatki mp = new frmMaticniPodatki();
            mp.Show();
        }

        private void prodajnegrupe_Click(object sender, EventArgs e)
        {
            frmProdajnaGrupa pg = new             frmProdajnaGrupa();
            pg.Show();
        }

        private void toolStripButton171_Click(object sender, EventArgs e)
        {
            frmProdajnaGrupa pg = new frmProdajnaGrupa();
            pg.Show();
        }

        private void toolStripButton172_Click(object sender, EventArgs e)
        {
            frmJediniceMere jm = new frmJediniceMere();
            jm.Show();
        }

        private void toolStripButton36_Click_1(object sender, EventArgs e)
        {
            Dokumenta.frmDogovori dog = new Dokumenta.frmDogovori();
            dog.Show();
        }

        private void toolStripButton173_Click(object sender, EventArgs e)
        {
            frmObjekt obj = new frmObjekt();
            obj.Show();
        }

        private void toolStripButton174_Click(object sender, EventArgs e)
        {
            frmNacinIsporuke nacisp = new frmNacinIsporuke();
            nacisp.Show();
        }

        private void toolStripButton175_Click(object sender, EventArgs e)
        {
            Dokumenta.frmKontaktOsobe ko = new Dokumenta.frmKontaktOsobe();
            ko.Show();
        }

        private void toolStripButton169_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripButton169_Click_1(object sender, EventArgs e)
        {
            frmDelovnaMesta delm = new frmDelovnaMesta();
            delm.Show();
        }

        private void toolStripButton170_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton176_Click(object sender, EventArgs e)
        {
            frmDelavci del = new frmDelavci();
            PravoP = del.Pravo;
            if (PravoP == true)
            {
                del.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton170_Click_1(object sender, EventArgs e)
        {
            Dokumenta.frmBolovanja bol = new Dokumenta.frmBolovanja();
            PravoP = bol.Pravo;
            if (PravoP == true) { bol.Show(); } else { return; }
        }

        private void toolStripButton100_Click_1(object sender, EventArgs e)
        {
            Dokumenta.frmPrekovremeniRad prekrad = new Dokumenta.frmPrekovremeniRad();
            PravoP = prekrad.Pravo;
            if (PravoP == true) { prekrad.Show(); } else { return; }
        }

        private void toolStripButton50_Click_1(object sender, EventArgs e)
        {
            Mobile.frmSlobodniDani slob = new Mobile.frmSlobodniDani(Korisnik);
            PravoP = slob.Pravo;
            if (PravoP == true) { slob.Show(); } else { return; }
        }

        private void toolStripButton51_Click_1(object sender, EventArgs e)
        {
            Dokumenta.frmEvidencijaGodišnjihOdmora evgo = new Dokumenta.frmEvidencijaGodišnjihOdmora();
            PravoP = evgo.Pravo;
            if (PravoP == true) { evgo.Show(); } else { return; }
        }

        private void toolStripButton56_Click_1(object sender, EventArgs e)
        {
            Mobile.frmAnalizaGO ago = new Mobile.frmAnalizaGO();
            PravoP = ago.Pravo;
            if (PravoP == true) { ago.Show(); } else { return; }
        }

        private void toolStripButton57_Click_1(object sender, EventArgs e)
        {
            Mobile.frmAnalizaGOSum GoSum = new Mobile.frmAnalizaGOSum();
            PravoP = GoSum.Pravo;
            if (PravoP == true) { GoSum.Show(); } else { return; }
        }

        private void toolStripButton58_Click_1(object sender, EventArgs e)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            myConnection.Open();
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "sp_AzuriranjeOdmora";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;
            SqlTransaction myTransaction = myConnection.BeginTransaction();
            myCommand.Transaction = myTransaction;
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            MessageBox.Show("Obrada zavrsena");
            /*
                        myConnection.Open();
                        SqlTransaction myTransaction = myConnection.BeginTransaction();
                        myCommand.Transaction = myTransaction;
                        bool error = true;
                        try
                        {
                            myCommand.ExecuteNonQuery();
                            myTransaction.Commit();
                            myTransaction = myConnection.BeginTransaction();
                            myCommand.Transaction = myTransaction;
                        }
            */
        }

        private void toolStripButton25_Click_1(object sender, EventArgs e)
        {
            Testiranje.Pitanja pitanja = new Testiranje.Pitanja();
            PravoP = pitanja.Pravo;
            if (PravoP == true) { pitanja.Show(); } else { return; }
        }

        private void toolStripButton101_Click_1(object sender, EventArgs e)
        {
            Testiranje.frmGenerisanjeTestaKorisnik generisanje = new Testiranje.frmGenerisanjeTestaKorisnik();
            PravoP = generisanje.Pravo;
            if (PravoP == true) { generisanje.Show(); } else { return; }
        }

        private void toolStripButton177_Click(object sender, EventArgs e)
        {
            Testiranje.TestiranjeStampa tesS = new Testiranje.TestiranjeStampa();
            PravoP = tesS.Pravo;
            if (PravoP == true) { tesS.Show(); } else { return; }
        }

        private void toolStripButton178_Click(object sender, EventArgs e)
        {
            Testiranje.Obrasci tes6 = new Testiranje.Obrasci();
            PravoP = tes6.Pravo;
            if (PravoP == true) { tes6.Show(); } else { return; }
        }

        private void toolStripButton179_Click(object sender, EventArgs e)
        {
            Sifarnici.frmTelegrami tel = new Sifarnici.frmTelegrami();
            PravoP = tel.Pravo;
            if (PravoP == true) { tel.Show(); } else { return; }
        }

        private void toolStripButton13_Click_1(object sender, EventArgs e)
        {
            Administracija.frmNotifikacije not = new Administracija.frmNotifikacije();
            PravoP = not.Pravo;
            if (PravoP == true) { not.Show(); } else { return; }
        }

        private void toolStripButton59_Click_1(object sender, EventArgs e)
        {
            Mobile.frmZavrsnaDokumentacija zdok = new Mobile.frmZavrsnaDokumentacija();
            PravoP = zdok.Pravo;
            if (PravoP == true) { zdok.Show(); } else { return; }
        }

        private void toolStripButton26_Click_1(object sender, EventArgs e)
        {
            Servis.frmPlombe pl = new Servis.frmPlombe();
            PravoP = pl.Pravo;
            if (PravoP == true)
            {
                pl.Show();
            }
            else { return; }
        }

        private void toolStripButton28_Click_1(object sender, EventArgs e)
        {
            Sifarnici.frmNHM nhm = new Sifarnici.frmNHM();
            PravoP = nhm.Pravo;
            if (PravoP == true) { nhm.Show(); } else { return; }
        }

        private void toolStripButton9_Click_1(object sender, EventArgs e)
        {
            Sifarnici.frmOsoblje osob = new Sifarnici.frmOsoblje();
            PravoP = osob.Pravo;
            if (PravoP == true) { osob.Show(); } else { return; }
        }

        private void toolStripButton10_Click_1(object sender, EventArgs e)
        {
            Sifarnici.frmTipTelegrama ttel = new Sifarnici.frmTipTelegrama();
            PravoP = ttel.Pravo;
            if (PravoP == true) { ttel.Show(); } else { return; }
        }

        private void toolStripButton12_Click_1(object sender, EventArgs e)
        {
            Dokumenta.frmDogovoriPregled dogpre = new Dokumenta.frmDogovoriPregled();
            dogpre.Show();
        }

        private void ribbonControlAdv1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton34_Click_1(object sender, EventArgs e)
        {
            Uvoz.Brodovi brod = new Uvoz.Brodovi();
            brod.Show();
        }

        private void toolStripButton104_Click_1(object sender, EventArgs e)
        {
            Uvoz.Carinarnice car = new Uvoz.Carinarnice();
            car.Show();
        }

        private void toolStripButton105_Click_1(object sender, EventArgs e)
        {
            Uvoz.Nalogodavci nal = new Uvoz.Nalogodavci();
            nal.Show();
        }

        private void toolStripButton106_Click_1(object sender, EventArgs e)
        {
            Uvoz.Uvoz uv = new Uvoz.Uvoz();
            uv.Show();
        }

        private void toolStripButton107_Click_1(object sender, EventArgs e)
        {
            Uvoz.frmUvozKonacna uvk = new Uvoz.frmUvozKonacna();
            uvk.Show();
        }

        private void toolStripButton180_Click(object sender, EventArgs e)
        {
            Uvoz.frmSerijeKola uvk = new Uvoz.frmSerijeKola();
            uvk.Show();
        }

        private void toolStripButton181_Click(object sender, EventArgs e)
        {
            Uvoz.frmKontejnerskiTerminali kt = new Uvoz.frmKontejnerskiTerminali();
            kt.Show();
        }

        private void toolStripButton182_Click(object sender, EventArgs e)
        {
            Uvoz.frmVrstaRobeHS vrHS = new Uvoz.frmVrstaRobeHS();
            vrHS.Show();
        }

        private void toolStripButton182_Click_1(object sender, EventArgs e)
        {
            Uvoz.frmVrstaRobeHS vrHS = new Uvoz.frmVrstaRobeHS();
            vrHS.Show();
        }

        private void toolStripButton183_Click(object sender, EventArgs e)
        {
            Uvoz.frmVrstaRobeADR vrADR = new Uvoz.frmVrstaRobeADR();
            vrADR.Show();
        }

        private void toolStripButton184_Click(object sender, EventArgs e)
        {
            Uvoz.frmVrstaCarinskogPostupka vrCP = new Uvoz.frmVrstaCarinskogPostupka();
            vrCP.Show();
        }

        private void toolStripButton185_Click(object sender, EventArgs e)
        {
            Uvoz.fruvNacinPakovanja vrNP = new Uvoz.fruvNacinPakovanja();
            vrNP.Show();
        }

        private void toolStripButton186_Click(object sender, EventArgs e)
        {
            Uvoz.frmuvKvalitetKontejnera vrKK = new Uvoz.frmuvKvalitetKontejnera();
            vrKK.Show();
        }

        private void toolStripButton187_Click(object sender, EventArgs e)
        {
            Uvoz.frmPregledNerasporedjeni pner = new Uvoz.frmPregledNerasporedjeni();
            pner.Show();
        }

        private void toolStripButton188_Click(object sender, EventArgs e)
        {
            frmTipKontejnera tkon = new frmTipKontejnera();
            PravoP = tkon.Pravo;
            if (PravoP == true)
            {
                tkon.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton189_Click(object sender, EventArgs e)
        {
            frmVrstaManipulacije vm = new frmVrstaManipulacije();
            PravoP = vm.Pravo;
            if (PravoP == true)
            {
                vm.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton190_Click(object sender, EventArgs e)
        {
            frmVoz voz = new frmVoz();
            PravoP = voz.Pravo;
            if (PravoP == true)
            {
                voz.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton191_Click(object sender, EventArgs e)
        {
            Uvoz.frmPregledPlanovaUtovara fppp = new Uvoz.frmPregledPlanovaUtovara();
            PravoP = fppp.Pravo;
            if (PravoP == true) { fppp.Show(); } else { return; }
        }

        private void toolStripButton192_Click(object sender, EventArgs e)
        {
            Uvoz.frmFormiranjePlana fplan = new Uvoz.frmFormiranjePlana();
            fplan.Show();
        }

        private void toolStripButton193_Click(object sender, EventArgs e)
        {
            Uvoz.frmPlanUtovaraOdgovorExcel pue = new Uvoz.frmPlanUtovaraOdgovorExcel();
            pue.Show();
        }

        private void toolStripButton194_Click(object sender, EventArgs e)
        {
            Uvoz.frmVrstePostupakaUvoz fvp = new Uvoz.frmVrstePostupakaUvoz();
            fvp.Show();
        }

        private void toolStripButton195_Click(object sender, EventArgs e)
        {
            Uvoz.frmPrijemVozaIzPlana pvizp = new Uvoz.frmPrijemVozaIzPlana();
            pvizp.Show();

        }

        private void toolStripButton196_Click(object sender, EventArgs e)
        {
            Uvoz.frmAnalizaUvoza auv = new Uvoz.frmAnalizaUvoza();
            auv.Show();
        }

        private void toolStripButton197_Click(object sender, EventArgs e)
        {
            frmPregledVozova pvozo = new frmPregledVozova();
            PravoP = pvozo.Pravo;
            if (PravoP == true)
            {
                pvozo.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton198_Click(object sender, EventArgs e)
        {
            Sifarnici.frmPartnerji part = new Sifarnici.frmPartnerji();
            part.Show(); 
        }

        private void toolStripButton199_Click(object sender, EventArgs e)
        {
            Uvoz.frmDirigacijaKontejneraZa dkz = new Uvoz.frmDirigacijaKontejneraZa();
            dkz.Show();
        }

        private void toolStripButton200_Click(object sender, EventArgs e)
        {
            Uvoz.frmNapomenaZaPozicioniranje dkz = new Uvoz.frmNapomenaZaPozicioniranje();
            dkz.Show();
        }

        private void toolStripButton201_Click(object sender, EventArgs e)
        {
            Uvoz.frmPrebacivanjeIzPlanaUPlan pup = new Uvoz.frmPrebacivanjeIzPlanaUPlan();
            pup.Show();
        }

        private void toolStripButton202_Click(object sender, EventArgs e)
        {
            Uvoz.frmRadniNaloziInterni rni = new Uvoz.frmRadniNaloziInterni();
            rni.Show();
        }

        private void toolStripButton203_Click(object sender, EventArgs e)
        {
            Uvoz.frmRadniNalogInterniPregled RNIP = new Uvoz.frmRadniNalogInterniPregled();
            RNIP.Show();
        }

        private void toolStripButton204_Click(object sender, EventArgs e)
        {
            Uvoz.frmForrmiranjePlanaDrumski forPD = new Uvoz.frmForrmiranjePlanaDrumski();
            forPD.Show();
        }

        private void toolStripButton205_Click(object sender, EventArgs e)
        {
            Uvoz.frmPregledPlanoviUtovaraDrumski ppUD = new Uvoz.frmPregledPlanoviUtovaraDrumski();
            ppUD.Show();
        }

        private void toolStripButton206_Click(object sender, EventArgs e)
        {
            Uvoz.frmPregledRadnihNalogaInterni frnp = new Uvoz.frmPregledRadnihNalogaInterni();
            frnp.Show();
        }

        private void toolStripButton207_Click(object sender, EventArgs e)
        {
            Izvoz.frmIzvoz izvPoc = new Izvoz.frmIzvoz();
            izvPoc.Show();
        }

        private void toolStripButton208_Click(object sender, EventArgs e)
        {
            Izvoz.frmKrajnjaDestinacija KraDes = new Izvoz.frmKrajnjaDestinacija();
            KraDes.Show();
        }

        private void toolStripButton209_Click(object sender, EventArgs e)
        {
            Izvoz.frmMestaUtovara frmMes = new Izvoz.frmMestaUtovara();
            frmMes.Show();
        }

        private void toolStripButton210_Click(object sender, EventArgs e)
        {
            Izvoz.frmAdresaStatusVozila adrstatvoz = new Izvoz.frmAdresaStatusVozila();
            adrstatvoz.Show();
        }

        private void toolStripButton211_Click(object sender, EventArgs e)
        {
            Izvoz.frmNaslovStatusaVozila naslstatvoz = new Izvoz.frmNaslovStatusaVozila();
            naslstatvoz.Show();
        }

        private void toolStripButton212_Click(object sender, EventArgs e)
        {
            Izvoz.frmInspekciskiTretman insTret = new Izvoz.frmInspekciskiTretman();
            insTret.Show();
        }

        private void toolStripButton213_Click(object sender, EventArgs e)
        {
            Izvoz.frmPregledKontejneraIzvoz prIzvoz = new Izvoz.frmPregledKontejneraIzvoz();
            prIzvoz.Show();
        }

        private void toolStripButton215_Click(object sender, EventArgs e)
        {
            Izvoz.frmPlanoviIzvoza pl = new Izvoz.frmPlanoviIzvoza();
            pl.Show();
        }

        private void toolStripButton214_Click(object sender, EventArgs e)
        {
            Izvoz.frmPlanoviIzvoza pl = new Izvoz.frmPlanoviIzvoza();
            pl.Show();
        }

        private void toolStripButton217_Click(object sender, EventArgs e)
        {
            frmTipCenovnika frmTC = new frmTipCenovnika(Korisnik);
            PravoP = frmTC.Pravo;
            if (PravoP == true)
            {
                frmTC.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton216_Click(object sender, EventArgs e)
        {
            frmCene frmcen = new frmCene(Korisnik);
            PravoP = frmcen.Pravo;
            if (PravoP == true)
            {
                frmcen.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton107_Click_2(object sender, EventArgs e)
        {
            frmOrganizacionaJedinica otg = new frmOrganizacionaJedinica();
            PravoP = otg.Pravo;
            if (PravoP == true)
            {
                otg.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton218_Click(object sender, EventArgs e)
        {
            Process.Start(@"\\192.168.129.7\TA\Panta\Lupustva\Sifarnici-Upustvo.pdf");
        }

        private void toolStripButton219_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmTrainList")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmTrainList tl = new Dokumenta.frmTrainList();
                tl.Show(); 
            }    
            
        }

        private void toolStripEx1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton220_Click(object sender, EventArgs e)
        {
            frmVagoniSerije frm = new frmVagoniSerije();
           // PravoP = frm.Pravo;
           // if (PravoP == true)
           // {
                frm.Show();
           // }
          //  else
           // {
           //     return;
          //  }
        }

        private void toolStripButton221_Click(object sender, EventArgs e)
        {
            Izvoz.frmValute valute = new Izvoz.frmValute();
            valute.Show();
        }

        private void toolStripButton222_Click(object sender, EventArgs e)
        {
            frmVoz voz = new frmVoz();
            PravoP = voz.Pravo;
            if (PravoP == true)
            {
                voz.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton224_Click(object sender, EventArgs e)
        {
            Izvoz.frmFormiranjePlanaIzvoz fpi = new Izvoz.frmFormiranjePlanaIzvoz();
            fpi.Show();
        }

        private void toolStripButton225_Click(object sender, EventArgs e)
        {
            Izvoz.frmPrebacivanjeIzPlanaUPlan koM = new Izvoz.frmPrebacivanjeIzPlanaUPlan();
            koM.Show();
        }

        private void toolStripButton226_Click(object sender, EventArgs e)
        {
            Nepravilnosti.frmGrupaNepravilnosti np = new Nepravilnosti.frmGrupaNepravilnosti();
            np.Show();
        }

        private void toolStripButton227_Click(object sender, EventArgs e)
        {

            Nepravilnosti.frmNeispravnostPostupak np = new Nepravilnosti.frmNeispravnostPostupak();
            np.Show();
        }

        private void toolStripButton228_Click(object sender, EventArgs e)
        {
            Nepravilnosti.frmOpisNeispravnosti np = new Nepravilnosti.frmOpisNeispravnosti();
            np.Show();
        }

        private void toolStripButton229_Click(object sender, EventArgs e)
        {
            Nepravilnosti.frmRazredNepravilnosti np = new Nepravilnosti.frmRazredNepravilnosti();
            np.Show();
        }

        private void toolStripButton230_Click(object sender, EventArgs e)
        {
            Nepravilnosti.frmVrstaNepravilnosti np = new Nepravilnosti.frmVrstaNepravilnosti();
            np.Show();
        }

        private void toolStripButton231_Click(object sender, EventArgs e)
        {
            Dokumenta.frmPregledAktivnosti pr = new Dokumenta.frmPregledAktivnosti();
            pr.Show();
        }

        private void toolStripTabItem10_Click(object sender, EventArgs e)
        {

        }

        private void toolStripTabItem5_Click(object sender, EventArgs e)
        {

        }

        private void toolStripTabItem7_Click(object sender, EventArgs e)
        {

        }

        private void toolStripEx39_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton232_Click(object sender, EventArgs e)
        {
            frmTipKontejnera tk = new frmTipKontejnera();
            PravoP = tk.Pravo;
            if (PravoP == true)
            {
                tk.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton233_Click(object sender, EventArgs e)
        {
            Dokumenta.frmVucaStatusi vs = new Dokumenta.frmVucaStatusi();
            vs.Show();
        }

        private void toolStripButton234_Click(object sender, EventArgs e)
        {
            Administracija.frmStavkePrimopredaje spp = new Administracija.frmStavkePrimopredaje();
            spp.Show();
        }

        private void toolStripButton235_Click(object sender, EventArgs e)
        {
            Izvoz.frmVrstePlombi vblombi = new Izvoz.frmVrstePlombi();
            vblombi.Show();
        }

        private void toolStripButton237_Click(object sender, EventArgs e)
        {

            Uvoz.frmPregledPlanovaUtovara fppp = new Uvoz.frmPregledPlanovaUtovara();
            fppp.Show();
        }

        private void toolStripButton238_Click(object sender, EventArgs e)
        {
            Izvoz.frmPlanoviIzvoza pl = new Izvoz.frmPlanoviIzvoza();
            pl.Show();
        }

        private void toolStripButton239_Click(object sender, EventArgs e)
        {

            frmOrganizacionaJedinica otg = new frmOrganizacionaJedinica();
            PravoP = otg.Pravo;
            if (PravoP == true)
            {
                otg.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton240_Click(object sender, EventArgs e)
        {
            Uvoz.frmSerijeKola uvk = new Uvoz.frmSerijeKola();
            uvk.Show();
        }

        private void toolStripButton241_Click(object sender, EventArgs e)
        {
            Uvoz.frmuvKvalitetKontejnera kk = new Uvoz.frmuvKvalitetKontejnera();
            kk.Show();
        }

        private void toolStripButton242_Click(object sender, EventArgs e)
        {
            Mobile.frmPrijavaSmeneOld pso = new Mobile.frmPrijavaSmeneOld();
            PravoP = pso.Pravo;
            if (PravoP == true) { pso.Show(); } else { return; }
        }

        private void toolStripButton243_Click(object sender, EventArgs e)
        {
        
                Servis.frmPrijavaMasinovodjeOld mas = new Servis.frmPrijavaMasinovodjeOld();
            PravoP = mas.Pravo;
            if (PravoP == true) { mas.Show(); } else { return; }
        }

        private void toolStripButton253_Click(object sender, EventArgs e)
        {
            Uvoz.frmVrstaRobeHS hs = new Uvoz.frmVrstaRobeHS();
            hs.Show();
        }

        private void toolStripButton274_Click(object sender, EventArgs e)
        {
            Uvoz.frmVrstaRobeHS hs = new Uvoz.frmVrstaRobeHS();
            hs.Show();
        }

        private void toolStripButton265_Click(object sender, EventArgs e)
        {
            frmPartnerji parn = new frmPartnerji();
            PravoP = parn.Pravo;
            if (PravoP == true)
            {
                parn.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton244_Click(object sender, EventArgs e)
        {
            frmPartnerji parn = new frmPartnerji();
            PravoP = parn.Pravo;
            if (PravoP == true)
            {
                parn.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton245_Click(object sender, EventArgs e)
        {
            frmTipCenovnika tc = new frmTipCenovnika();
            PravoP = tc.Pravo;
            if (PravoP == true)
            {
                tc.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton266_Click(object sender, EventArgs e)
        {
            frmTipCenovnika tc = new frmTipCenovnika();
            PravoP = tc.Pravo;
            if (PravoP == true)
            {
                tc.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton267_Click(object sender, EventArgs e)
        {
            frmVrstaManipulacije vm = new frmVrstaManipulacije();
            PravoP = vm.Pravo;
            if (PravoP == true)
            {
                vm.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton246_Click(object sender, EventArgs e)
        {
            frmVrstaManipulacije vm = new frmVrstaManipulacije();
            PravoP = vm.Pravo;
            if (PravoP == true)
            {
                vm.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton268_Click(object sender, EventArgs e)
        {
            frmCene cen = new frmCene();
            PravoP = cen.Pravo;
            if (PravoP == true)
            {
                cen.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton247_Click(object sender, EventArgs e)
        {
            frmCene cen = new frmCene();
            PravoP = cen.Pravo;
            if (PravoP == true)
            {
                cen.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton248_Click(object sender, EventArgs e)
        {
            Uvoz.Brodovi brodovi = new Uvoz.Brodovi();
            brodovi.Show();
        }

        private void toolStripButton269_Click(object sender, EventArgs e)
        {
            Uvoz.Brodovi brodovi = new Uvoz.Brodovi();
            brodovi.Show();
        }

        private void toolStripButton270_Click(object sender, EventArgs e)
        {
            Uvoz.Carinarnice car = new Uvoz.Carinarnice();
            car.Show();
        }

        private void toolStripButton249_Click(object sender, EventArgs e)
        {
            Uvoz.Carinarnice car = new Uvoz.Carinarnice();
            car.Show();
        }

        private void toolStripButton250_Click(object sender, EventArgs e)
        {
            Uvoz.Nalogodavci nalog = new Uvoz.Nalogodavci();
            nalog.Show();
        }

        private void toolStripButton271_Click(object sender, EventArgs e)
        {
            Uvoz.Nalogodavci nalog = new Uvoz.Nalogodavci();
            nalog.Show();
        }

        private void toolStripButton272_Click(object sender, EventArgs e)
        {
            Uvoz.frmSerijeKola serk = new Uvoz.frmSerijeKola();
            serk.Show();
        }

        private void toolStripButton251_Click(object sender, EventArgs e)
        {
            Uvoz.frmSerijeKola serk = new Uvoz.frmSerijeKola();
            serk.Show();
        }

        private void toolStripButton252_Click(object sender, EventArgs e)
        {
            Uvoz.frmKontejnerskiTerminali kt = new Uvoz.frmKontejnerskiTerminali();
            kt.Show();
        }

        private void toolStripButton273_Click(object sender, EventArgs e)
        {
            Uvoz.frmKontejnerskiTerminali kt = new Uvoz.frmKontejnerskiTerminali();
            kt.Show();
        }

        private void toolStripButton275_Click(object sender, EventArgs e)
        {
            Uvoz.frmVrstaRobeADR adr = new Uvoz.frmVrstaRobeADR();
            adr.Show();
        }

        private void toolStripButton254_Click(object sender, EventArgs e)
        {
            Uvoz.frmVrstaRobeADR adr = new Uvoz.frmVrstaRobeADR();
            adr.Show();
        }

        private void toolStripButton104_Click_2(object sender, EventArgs e)
        {
            frmNHM nhm = new frmNHM();
            PravoP = nhm.Pravo;
            if (PravoP == true)
            {
                nhm.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton34_Click_2(object sender, EventArgs e)
        {
            frmNHM nhm = new frmNHM();
            PravoP = nhm.Pravo;
            if (PravoP == true)
            {
                nhm.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton276_Click(object sender, EventArgs e)
        {
            Uvoz.frmVrstePostupakaUvoz vpu = new Uvoz.frmVrstePostupakaUvoz();
            vpu.Show();
        }

        private void toolStripButton255_Click(object sender, EventArgs e)
        {
            Uvoz.frmVrstePostupakaUvoz vpu = new Uvoz.frmVrstePostupakaUvoz();
            vpu.Show();
        }

        private void toolStripButton256_Click(object sender, EventArgs e)
        {
            Uvoz.fruvNacinPakovanja frunp = new Uvoz.fruvNacinPakovanja();
            frunp.Show();
        }

        private void toolStripButton277_Click(object sender, EventArgs e)
        {
            Uvoz.fruvNacinPakovanja frunp = new Uvoz.fruvNacinPakovanja();
            frunp.Show();
        }

        private void toolStripButton278_Click(object sender, EventArgs e)
        {
            Uvoz.frmuvKvalitetKontejnera kk = new Uvoz.frmuvKvalitetKontejnera();
            kk.Show();
        }

        private void toolStripButton257_Click(object sender, EventArgs e)
        {
            Uvoz.frmuvKvalitetKontejnera kk = new Uvoz.frmuvKvalitetKontejnera();
            kk.Show();
        }

        private void toolStripButton258_Click(object sender, EventArgs e)
        {
            frmTipKontejnera tk = new frmTipKontejnera();
            PravoP = tk.Pravo;
            if (PravoP == true)
            {
                tk.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton279_Click(object sender, EventArgs e)
        {
            frmTipKontejnera tk = new frmTipKontejnera();
            PravoP = tk.Pravo;
            if (PravoP == true)
            {
                tk.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton280_Click(object sender, EventArgs e)
        {
            Uvoz.frmVrstePostupakaUvoz pp = new Uvoz.frmVrstePostupakaUvoz();
            pp.Show();
        }

        private void toolStripButton259_Click(object sender, EventArgs e)
        {
            Uvoz.frmVrstePostupakaUvoz pp = new Uvoz.frmVrstePostupakaUvoz();
            pp.Show();
        }

        private void toolStripButton260_Click(object sender, EventArgs e)
        {
            frmPregledVozova pv = new frmPregledVozova();
            PravoP = pv.Pravo;
            if (PravoP == true)
            {
                pv.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton281_Click(object sender, EventArgs e)
        {
            frmPregledVozova pv = new frmPregledVozova();
            PravoP = pv.Pravo;
            if (PravoP == true)
            {
                pv.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton282_Click(object sender, EventArgs e)
        {
            Uvoz.frmDirigacijaKontejneraZa dkz = new Uvoz.frmDirigacijaKontejneraZa();
            dkz.Show();
        }

        private void toolStripButton261_Click(object sender, EventArgs e)
        {
            Uvoz.frmDirigacijaKontejneraZa dkz = new Uvoz.frmDirigacijaKontejneraZa();
            dkz.Show();
        }

        private void toolStripButton262_Click(object sender, EventArgs e)
        {
            Uvoz.frmNapomenaZaPozicioniranje nzp = new Uvoz.frmNapomenaZaPozicioniranje();
            nzp.Show();
        }

        private void toolStripButton283_Click(object sender, EventArgs e)
        {
            Uvoz.frmNapomenaZaPozicioniranje nzp = new Uvoz.frmNapomenaZaPozicioniranje();
            nzp.Show();
        }

        private void toolStripButton284_Click(object sender, EventArgs e)
        {
            frmOrganizacionaJedinica oj = new frmOrganizacionaJedinica();
            PravoP = oj.Pravo;
            if (PravoP == true)
            {
                oj.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton263_Click(object sender, EventArgs e)
        {
            frmOrganizacionaJedinica oj = new frmOrganizacionaJedinica();
            PravoP = oj.Pravo;
            if (PravoP == true)
            {
                oj.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton208_Click_1(object sender, EventArgs e)
        {
            Izvoz.frmKrajnjaDestinacija ikd = new Izvoz.frmKrajnjaDestinacija();
            ikd.Show();
        }

        private void toolStripButton286_Click(object sender, EventArgs e)
        {
            Izvoz.frmKrajnjaDestinacija ikd = new Izvoz.frmKrajnjaDestinacija();
            ikd.Show();
        }

        private void toolStripButton287_Click(object sender, EventArgs e)
        {
            Izvoz.frmMestaUtovara mu = new Izvoz.frmMestaUtovara();
            mu.Show();
        }

        private void toolStripButton209_Click_1(object sender, EventArgs e)
        {
            Izvoz.frmMestaUtovara mu = new Izvoz.frmMestaUtovara();
            mu.Show();
        }

        private void toolStripButton225_Click_1(object sender, EventArgs e)
        {
            Izvoz.frmKontaktOsobeMU komu = new Izvoz.frmKontaktOsobeMU();
            komu.Show();
        }

        private void toolStripButton288_Click(object sender, EventArgs e)
        {
            Izvoz.frmKontaktOsobeMU komu = new Izvoz.frmKontaktOsobeMU();
            komu.Show();
        }

        private void toolStripButton289_Click(object sender, EventArgs e)
        {
            Izvoz.frmAdresaStatusVozila asv = new Izvoz.frmAdresaStatusVozila();
            asv.Show();
        }

        private void toolStripButton210_Click_1(object sender, EventArgs e)
        {
            Izvoz.frmAdresaStatusVozila asv = new Izvoz.frmAdresaStatusVozila();
            asv.Show();
        }

        private void toolStripButton211_Click_1(object sender, EventArgs e)
        {
            Izvoz.frmNaslovStatusaVozila nsv = new Izvoz.frmNaslovStatusaVozila();
            nsv.Show();
        }

        private void toolStripButton290_Click(object sender, EventArgs e)
        {
            Izvoz.frmNaslovStatusaVozila nsv = new Izvoz.frmNaslovStatusaVozila();
            nsv.Show();
        }

        private void toolStripButton291_Click(object sender, EventArgs e)
        {
            Izvoz.frmInspekciskiTretman it = new Izvoz.frmInspekciskiTretman();
            it.Show();
        }

        private void toolStripButton212_Click_1(object sender, EventArgs e)
        {
            Izvoz.frmInspekciskiTretman it = new Izvoz.frmInspekciskiTretman();
            it.Show();
        }

        private void toolStripButton221_Click_1(object sender, EventArgs e)
        {
            Izvoz.frmValute val = new Izvoz.frmValute();
            val.Show();
        }

        private void toolStripButton292_Click(object sender, EventArgs e)
        {
            Izvoz.frmValute val = new Izvoz.frmValute();
            val.Show();
        }

        private void toolStripButton293_Click(object sender, EventArgs e)
        {
            Izvoz.frmVrstePlombi vp = new Izvoz.frmVrstePlombi();
            vp.Show();
        }

        private void toolStripButton235_Click_1(object sender, EventArgs e)
        {
            Izvoz.frmVrstePlombi vp = new Izvoz.frmVrstePlombi();
            vp.Show();
        }

        private void toolStripButton207_Click_1(object sender, EventArgs e)
        {
            Izvoz.frmIzvoz izvoz = new Izvoz.frmIzvoz();
            izvoz.Show();
        }

        private void toolStripButton213_Click_1(object sender, EventArgs e)
        {
            Izvoz.frmPregledKontejneraIzvoz pkizvoz = new Izvoz.frmPregledKontejneraIzvoz();
            pkizvoz.Show();
        }

        private void toolStripButton222_Click_1(object sender, EventArgs e)
        {
            frmVoz voz = new frmVoz();
            PravoP = voz.Pravo;
            if (PravoP == true)
            {
                voz.Show();
            }
            else
            {
                return;
            }
        }

        private void toolStripButton214_Click_1(object sender, EventArgs e)
        {
            Izvoz.frmPlanoviIzvoza plIz = new Izvoz.frmPlanoviIzvoza();
            plIz.Show();
        }

        private void toolStripButton181_Click_1(object sender, EventArgs e)
        {
            Saobracaj.TrackModal.Sifarnici.frmGrupaVrsteManipulacije gvm = new TrackModal.Sifarnici.frmGrupaVrsteManipulacije();
            gvm.Show();
        }

        private void toolStripButton183_Click_1(object sender, EventArgs e)
        {
            Uvoz.frmDirigacijaKontejneraZa dkz = new Uvoz.frmDirigacijaKontejneraZa();
            dkz.Show();
        }

        private void toolStripButton183_Click_2(object sender, EventArgs e)
        {
            Uvoz.Carinarnice car = new Uvoz.Carinarnice();
            car.Show();
        }

        private void toolStripButton182_Click_2(object sender, EventArgs e)
        {
            Saobracaj.TrackModal.Sifarnici.frmGrupaVrsteManipulacije gvm = new TrackModal.Sifarnici.frmGrupaVrsteManipulacije();
            gvm.Show();
        }

        private void toolStripButton135_Click_1(object sender, EventArgs e)
        {
            Saobracaj.TrackModal.Sifarnici.frmGrupaVrsteManipulacije gvm = new TrackModal.Sifarnici.frmGrupaVrsteManipulacije();
            gvm.Show();
        }

        private void toolStripButton240_Click_1(object sender, EventArgs e)
        {
            Dokumenta.frmEvidencijaRadaDokumenti dok = new Dokumenta.frmEvidencijaRadaDokumenti();
            dok.Show();
        }

        private void toolStripButton279_Click_1(object sender, EventArgs e)
        {
            ReportingOperatika.PoLokomotivama pl = new ReportingOperatika.PoLokomotivama();
            pl.Show();
        }

        private void toolStripButton295_Click(object sender, EventArgs e)
        {
            SyncForm.frmPregledMobilni2 drugi = new SyncForm.frmPregledMobilni2();
            drugi.Show();
        }

        private void toolStripButton128_Click(object sender, EventArgs e)
        {

        }

        private void toolStripEx35_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton271_Click_1(object sender, EventArgs e)
        {

        }

        private void toolStripButton271_Click_2(object sender, EventArgs e)
        {
            ReportingOperatika.PoLokomotivama pLok = new ReportingOperatika.PoLokomotivama();
            pLok.Show();
        }

        private void toolStripButton279_Click_2(object sender, EventArgs e)
        {
            SyncForm.frmPregledMobilni2 pm2 = new SyncForm.frmPregledMobilni2();
            pm2.Show();
        }

        private void toolStripButton296_Click(object sender, EventArgs e)
        {
            SyncForm.frmPregledMasinovodje2 pm2 = new SyncForm.frmPregledMasinovodje2();
            pm2.Show();
        }

        private void toolStripButton297_Click(object sender, EventArgs e)
        {
            EFaktura frm = new EFaktura();
            frm.Show();
        }

        private void toolStripButton298_Click(object sender, EventArgs e)
        {
            EOdobrenje frm = new EOdobrenje();
            frm.Show();
        }

        private void toolStripButton299_Click(object sender, EventArgs e)
        {
            EZaduzenje frm = new EZaduzenje();
            frm.Show();
        }

        private void toolStripButton300_Click(object sender, EventArgs e)
        {
            EAvansni frm = new EAvansni();
            frm.Show();
        }

        private void toolStripButton301_Click(object sender, EventArgs e)
        {
            PoslataDokumenta frm = new PoslataDokumenta();
            frm.Show();
        }

        private void toolStripButton302_Click(object sender, EventArgs e)
        {
            PrimljenaDokumenta frm = new PrimljenaDokumenta();
            frm.Show();
        }

        private void toolStripButton303_Click(object sender, EventArgs e)
        {
            RN1PrijemVoza frm = new RN1PrijemVoza();
            frm.Show();
        }

        private void toolStripButton304_Click(object sender, EventArgs e)
        {
            RN2OtpremaVoza frm = new RN2OtpremaVoza();
            frm.Show();
        }

        private void toolStripButton305_Click(object sender, EventArgs e)
        {
            RN3PrijemVoza2 frm = new RN3PrijemVoza2();
            frm.Show();
        }

        private void toolStripButton306_Click(object sender, EventArgs e)
        {
            RN4PrijemPlatforme frm = new RN4PrijemPlatforme();
            frm.Show();
        }

        private void toolStripButton307_Click(object sender, EventArgs e)
        {
            RN5PrijemPlatforme2 frm = new RN5PrijemPlatforme2();
            frm.Show();
        }

        private void toolStripButton308_Click(object sender, EventArgs e)
        {
            RN6OtpremaPlatforme frm= new RN6OtpremaPlatforme();
            frm.Show();
        }

        private void toolStripButton309_Click(object sender, EventArgs e)
        {
            RN7OtpremaPlatforme2 frm = new RN7OtpremaPlatforme2();
            frm.Show();
        }

        private void toolStripButton310_Click(object sender, EventArgs e)
        {
            RN8OtpremaCirade frm = new RN8OtpremaCirade();
            frm.Show();
        }

        private void toolStripButton311_Click(object sender, EventArgs e)
        {
            RN9PrijemCirade frm = new RN9PrijemCirade();
            frm.Show();
        }

        private void toolStripButton312_Click(object sender, EventArgs e)
        {
            RN10PregledIspraznjenogKontejnera frm = new RN10PregledIspraznjenogKontejnera();
            frm.Show();
        }

        private void toolStripButton313_Click(object sender, EventArgs e)
        {
            RN11PreglediPostavkaKontejnera frm = new RN11PreglediPostavkaKontejnera();
            frm.Show();
        }

        private void toolStripButton314_Click(object sender, EventArgs e)
        {
            RN12MedjuskladisniKontejnera frm = new RN12MedjuskladisniKontejnera();
            frm.Show();
        }

        private void toolStripButton315_Click(object sender, EventArgs e)
        {
            Dokumenta.frmPregledRID prid = new Dokumenta.frmPregledRID();
            prid.Show();
        }

        private void toolStripButton316_Click(object sender, EventArgs e)
        {
            Dokumenta.frmRIDPoNajavama rPNajavama = new Dokumenta.frmRIDPoNajavama();
            rPNajavama.Show();
        }

        private void toolStripButton317_Click(object sender, EventArgs e)
        {
            Dokumenta.frmRIDTeretnice frter = new Dokumenta.frmRIDTeretnice();
            frter.Show();
        }

        private void toolStripButton285_Click(object sender, EventArgs e)
        {
            try
            {
                string Putanja = @"\\192.168.99.10\Leget\Upustva\UVOZ.pdf";
                string configvalue1 = "192.168.99.10";
                string configvalue2 = ConfigurationManager.AppSettings["server"];
                configvalue2 = "192.168.99.10";
                Putanja = Putanja.Replace(configvalue1, configvalue2);
                System.Diagnostics.Process.Start(Putanja);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void toolStripButton318_Click(object sender, EventArgs e)
        {
            try
            {
                string Putanja = @"\\192.168.99.10\Leget\Upustva\IZVOZ.pdf";
                string configvalue1 = "192.168.99.10";
                string configvalue2 = ConfigurationManager.AppSettings["server"];
                configvalue2 = "192.168.99.10";
                Putanja = Putanja.Replace(configvalue1, configvalue2);
                System.Diagnostics.Process.Start(Putanja);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
    }

