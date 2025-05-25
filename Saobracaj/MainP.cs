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
using Saobracaj.Izvoz;
using frmVozila = Testiranje.Dokumeta.frmVozila;
using frmCIRPregled = Testiranje.Dokumeta.frmCIRPregled;
using Saobracaj.Tehnologija;
using Syncfusion.Windows.Forms.Tools;
using Saobracaj.Administracija;
using Saobracaj.Uvoz;
using Saobracaj.Dokumenta;
namespace Saobracaj
{
    public partial class MainP : Syncfusion.Windows.Forms.Tools.RibbonForm
    {
        public string Korisnik = "";
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
                toolStripEx39.Visible = true;
                toolStripButton343.Visible = true;
                toolStripButton338.Visible = true;
                toolStripButton336.Visible = true;
                toolStripButton337.Visible = true;
                toolStripButton341.Visible = true;
                toolStripButton342.Visible = true;
                toolStripEx39.Visible = true;
                toolStripButton344.Visible = true;
                toolStripButton141.Visible = false;
                toolStripButton140.Text = "GATE IN VOZ";
                toolStripButton139.Text = "GATE IN KAMION";
                toolStripButton142.Text = "GATE OUT VOZ";
                toolStripButton143.Text = "GATE OUT KAMION";
                toolStripButton134.Text = "Polja";

            }

            if (Sifarnici.frmLogovanje.Firma == "TA")
            {
                toolStripEx39.Visible = false;
                toolStripButton343.Visible = false;
                toolStripButton338.Visible = false;
                toolStripButton336.Visible = false;
                toolStripButton337.Visible = false;
                toolStripButton341.Visible = false;
                toolStripButton342.Visible = false;
                toolStripEx39.Visible = false;
                toolStripButton344.Visible = false;
                toolStripButton238.Visible = false;
                toolStripButton237.Visible = false;

                toolStripButton184.Visible = false;
                toolStripButton186.Visible = false;
                toolStripButton188.Visible = false;
                toolStripButton189.Visible = false;
                toolStripButton197.Visible = false;
                toolStripButton217.Visible = false;
                toolStripButton129.Visible = false;
                toolStripButton264.Visible = false;
                toolStripButton128.Visible = false;
                toolStripButton194.Text = "Mesto utovara/istovara";

                toolStripButton350.Visible = false;
                toolStripButton350.Visible = false;
                toolStripButton348.Visible = false;
                toolStripButton347.Visible = false;
                toolStripButton345.Visible = false;
                toolStripButton342.Visible = false;
                toolStripButton341.Visible = false;
                toolStripButton346.Visible = false;
                toolStripButton336.Visible = false;
                toolStripButton337.Visible = false;
                toolStripButton351.Visible = false;
                toolStripButton357.Visible = false;
                toolStripButton356.Visible = false;
                toolStripButton358.Visible = false;
                toolStripButton359.Visible = false;
                toolStripButton361.Visible = false;
                toolStripButton360.Visible = false;
                toolStripButton362.Visible = false;
                toolStripButton355.Visible = false;
            }

            if (Sifarnici.frmLogovanje.Firma == "DPT")
            {
                toolStripEx39.Visible = false;
                toolStripButton343.Visible = false;
                toolStripButton338.Visible = false;
                toolStripButton336.Visible = false;
                toolStripButton337.Visible = false;
                toolStripButton341.Visible = false;
                toolStripButton342.Visible = false;
                toolStripEx39.Visible = false;
                toolStripButton344.Visible = false;
                toolStripButton238.Visible = false;
                toolStripButton237.Visible = false;

                toolStripButton184.Visible = false;
                toolStripButton186.Visible = false;
                toolStripButton188.Visible = false;
                toolStripButton189.Visible = false;
                toolStripButton197.Visible = false;
                toolStripButton217.Visible = false;
                toolStripButton129.Visible = false;
                toolStripButton264.Visible = false;
                toolStripButton128.Visible = false;
                toolStripButton194.Text = "Mesto utovara/istovara";

                toolStripButton350.Visible = false;
                toolStripButton350.Visible = false;
                toolStripButton348.Visible = false;
                toolStripButton347.Visible = false;
                toolStripButton345.Visible = false;
                toolStripButton342.Visible = false;
                toolStripButton341.Visible = false;
                toolStripButton346.Visible = false;
                toolStripButton336.Visible = false;
                toolStripButton337.Visible = false;
                toolStripButton351.Visible = false;
                toolStripButton357.Visible = false;
                toolStripButton356.Visible = false;
                toolStripButton358.Visible = false;
                toolStripButton359.Visible = false;
                toolStripButton361.Visible = false;
                toolStripButton360.Visible = false;
                toolStripButton362.Visible = false;
                toolStripButton355.Visible = false;
            }
            Prava();

        }
        private void Prava()
        {
            string query = "select Lower(RTrim(KarticeMain.Naziv)) as Main,Lower(RTrim(Kartice.Naziv)) as Kartica,Lower(RTrim(KarticeForme.Naziv)) as Forma " +
                "From KarticeKorisnik " +
                "inner join KarticeMain on KarticeKorisnik.MainID = KarticeMain.ID " +
                "inner join Kartice on KarticeKorisnik.KarticaID = Kartice.ID " +
                "inner join KarticeForme on KarticeKorisnik.FormaID = KarticeForme.ID " +
                "Where Korisnik = '" + Korisnik + "' order by KarticeForme.ID asc";
            string connect = frmLogovanje.connectionString;

            List<string> main = new List<string>();
            List<string> kartice = new List<string>();
            List<string> forme = new List<string>();

            using (SqlConnection conn = new SqlConnection(connect))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    main.Add(dr["Main"].ToString());
                    kartice.Add(dr["Kartica"].ToString());
                    forme.Add(dr["Forma"].ToString());
                }
                conn.Close();
            }

            foreach (ToolStripTabItem tItem in ribbonControlAdv1.Header.MainItems)
            {
                if (main.Contains(tItem.Text.ToString().TrimEnd().ToLower()))
                {
                    tItem.Visible = true;
                }
                else { tItem.Visible = false; }

                foreach (Control c in tItem.Panel.Controls)
                {
                    if (c is ToolStripEx)
                    {
                        ToolStripEx ex = c as ToolStripEx;

                        if (kartice.Contains(ex.Text.ToString().TrimEnd().ToLower()))
                        {
                            ex.Visible = true;
                        }
                        else { ex.Visible = false; }

                        foreach (ToolStripItem btn in ex.Items)
                        {
                            if (btn is ToolStripButton)
                            {
                                if (forme.Contains(btn.Text.ToString().TrimEnd().ToLower()))
                                {
                                    btn.Visible = true;
                                }
                                else { btn.Visible = false; }
                            }
                        }
                    }
                }
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Sifarnici.frmStanice stan = new Sifarnici.frmStanice();

                stan.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Sifarnici.frmTrase trase = new Sifarnici.frmTrase();

                trase.Show();
            }

        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPruge")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Sifarnici.frmPruge prug = new Sifarnici.frmPruge();
                prug.Show();
            }




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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {

                Sifarnici.frmLokomotive lokomotive = new Sifarnici.frmLokomotive();
                lokomotive.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {

                Sifarnici.frmSerijeLokomotiva serije = new Sifarnici.frmSerijeLokomotiva();
                serije.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Sifarnici.frmStatusVoza statvoz = new Sifarnici.frmStatusVoza();
                statvoz.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Sifarnici.frmRazlozi raz = new Sifarnici.frmRazlozi();
                raz.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Sifarnici.frmTipPrevoza tipP = new Sifarnici.frmTipPrevoza();
                tipP.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Sifarnici.frmPartnerji part = new Sifarnici.frmPartnerji();
                part.Show();
            }
        }
        private void toolStripButton12_Click(object sender, EventArgs e)
        {

        }
        private void toolStripButton13_Click(object sender, EventArgs e)
        {

        }
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmNajava frmNaj = new Dokumenta.frmNajava(Korisnik, 0);
                frmNaj.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmNajava frmNaj = new Dokumenta.frmNajava(Korisnik, 1);
                frmNaj.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmNajava20 naj = new Dokumenta.frmNajava20();
                naj.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                SyncForm.frmNajavaArhivaAnaliza sNA = new SyncForm.frmNajavaArhivaAnaliza();
                sNA.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                SyncForm.frmAnalizaPorudzbina apor = new SyncForm.frmAnalizaPorudzbina();
                apor.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmTeretnicePregled frmTer = new Dokumenta.frmTeretnicePregled(Korisnik);
                frmTer.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmIskljuceniVagoni iv = new Dokumenta.frmIskljuceniVagoni();
                iv.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmNajaveBezTeretnice nbt = new Dokumenta.frmNajaveBezTeretnice();
                nbt.Show();
            }
        }
        private void toolStripButton22_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPregledRID")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmPregledRID prid = new Dokumenta.frmPregledRID();
                prid.Show();
            }




        }
        private void toolStripButton23_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmRIDPoNajavama")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {

                Dokumenta.frmRIDPoNajavama rpn = new Dokumenta.frmRIDPoNajavama();
                rpn.Show();
            }



        }
        private void toolStripButton24_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmRIDTeretnice")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {

                Dokumenta.frmRIDTeretnice fridter = new Dokumenta.frmRIDTeretnice();
                fridter.Show();
            }



        }
        private void toolStripButton25_Click(object sender, EventArgs e)
        {

        }
        private void toolStripButton27_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmMapa")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {

                Dokumenta.frmMapa mapa = new Dokumenta.frmMapa();
                mapa.Show();
            }




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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmPregledRN prn = new Dokumenta.frmPregledRN();
                prn.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmRadniNalogPregled frmp2 = new Dokumenta.frmRadniNalogPregled();
                frmp2.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmRAdniNalogPregledPoLokomotivama rnpl = new Dokumenta.frmRAdniNalogPregledPoLokomotivama();
                rnpl.Show();
            }
        }
        private void toolStripButton31_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmStampaRadnogNaloga")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmStampaRadnogNaloga srn = new Dokumenta.frmStampaRadnogNaloga();
                srn.Show();
            }


        }
        private void toolStripButton33_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmRaspustiVagone")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmRaspustiVagone frv = new Dokumenta.frmRaspustiVagone();
                frv.Show();
            }

        }
        private void toolStripButton34_Click(object sender, EventArgs e)
        {

        }
        private void toolStripButton38_Click(object sender, EventArgs e)
        {
            Dokumenta.frmMUPDozvola Dozvola = new Dokumenta.frmMUPDozvola();
            Dozvola.Show();
        }

        private void toolStripButton37_Click(object sender, EventArgs e)
        {
            Dokumenta.frmMupMesto mesto = new Dokumenta.frmMupMesto();
            mesto.Show();
        }

        private void toolStripButton35_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmKorisnici")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Administracija.frmKorisnici kor = new Administracija.frmKorisnici();
                kor.Show();
            }

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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmAutomobili autom = new Dokumenta.frmAutomobili();
                autom.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmRegistator reg = new Dokumenta.frmRegistator();
                reg.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmRegistratorPregled regpre = new Dokumenta.frmRegistratorPregled();
                regpre.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Servis.frmVrstePopisa vrPopisa = new Servis.frmVrstePopisa();
                vrPopisa.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Servis.frmLokomotivaVrstaPopisa lokvp = new Servis.frmLokomotivaVrstaPopisa();
                lokvp.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Servis.frmGrupaKvarova gkv = new Servis.frmGrupaKvarova();
                gkv.Show();
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
                kv.Show();
                kv.WindowState = FormWindowState.Normal;
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Servis.frmEvidencijaKvarova pkvar = new Servis.frmEvidencijaKvarova();
                pkvar.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Servis.frmEvidencijaKvarovaAnaliza evkva = new Servis.frmEvidencijaKvarovaAnaliza();
                evkva.Show();
            }



        }

        private void toolStripButton50_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmSistematizacija")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Administracija.frmSistematizacija sistem = new Administracija.frmSistematizacija();
                sistem.Show();
            }


        }

        private void toolStripButton51_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmSistematizacijaPovezivanje")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Administracija.frmSistematizacijaPovezivanje sist = new Administracija.frmSistematizacijaPovezivanje();
                sist.Show();
            }




        }

        private void toolStripButton52_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmVrsteAktivnosti")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {

                Sifarnici.frmVrsteAktivnosti vrakt = new Sifarnici.frmVrsteAktivnosti();
                vrakt.Show();
            }


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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmPravoAktivnosti pravo = new Dokumenta.frmPravoAktivnosti();
                pravo.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {

                Dokumenta.frmPravoAktivnostiViseRadnika pravovise = new Dokumenta.frmPravoAktivnostiViseRadnika();
                pravovise.Show();
            }



        }

        private void toolStripButton72_Click(object sender, EventArgs e)
        {
            Dokumenta.frmIzracunZarada fiz = new Dokumenta.frmIzracunZarada();
            fiz.Show();
        }

        private void toolStripButton73_Click(object sender, EventArgs e)
        {
            Dokumenta.frmPlacenoNeplaceno pln = new Dokumenta.frmPlacenoNeplaceno();
            pln.Show();
        }

        private void toolStripButton74_Click(object sender, EventArgs e)
        {
            Dokumenta.frmOsnovnaZarada osnzar = new Dokumenta.frmOsnovnaZarada();
            osnzar.Show();
        }

        private void toolStripButton69_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton57_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmEvidencijaGodišnjihOdmora")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {

                Dokumenta.frmEvidencijaGodišnjihOdmora evgo = new Dokumenta.frmEvidencijaGodišnjihOdmora();
                evgo.Show();
            }



        }

        private void toolStripButton60_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPrijavaSmene")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {

                Mobile.frmPrijavaSmene prodj = new Mobile.frmPrijavaSmene();
                prodj.Show();
            }



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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {

                SyncForm.frmPregledMobilni drugi = new SyncForm.frmPregledMobilni();
                drugi.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Servis.frmPrijavaMasinovodje mas = new Servis.frmPrijavaMasinovodje();
                mas.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                SyncForm.frmPregledMasinovodje novi = new SyncForm.frmPregledMasinovodje();
                novi.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Servis.frmNamirenja namir = new Servis.frmNamirenja();
                namir.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Servis.frmNamirenjaSumarno namSum = new Servis.frmNamirenjaSumarno();
                namSum.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Mobile.frmSlobodniDani slob = new Mobile.frmSlobodniDani(Korisnik);
                slob.Show();
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
            fpn.Show();
        }

        private void toolStripButton79_Click(object sender, EventArgs e)
        {
            Dokumenta.frmEvidencijaRAdaNeplaceno nepl = new Dokumenta.frmEvidencijaRAdaNeplaceno();
            nepl.Show();
        }

        private void toolStripButton80_Click(object sender, EventArgs e)
        {
            Dokumenta.frmFinansije novak = new Dokumenta.frmFinansije();
            novak.Show();
        }

        private void toolStripButton65_Click(object sender, EventArgs e)
        {
            SyncForm.frmPlanRada planrada = new SyncForm.frmPlanRada();
            planrada.Show();
        }

        private void toolStripButton67_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmEvidencijaRada")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmEvidencijaRada erM = new Dokumenta.frmEvidencijaRada(Korisnik);
                erM.Show();
            }

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
            erz.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                SyncForm.frmAnalizaAktivnosti aAktiv = new SyncForm.frmAnalizaAktivnosti();
                aAktiv.Show();
            }
        }
        private void toolStripButton75_Click(object sender, EventArgs e)
        {
            Dokumenta.frmParametriObracuna parobr = new Dokumenta.frmParametriObracuna();
            parobr.Show();
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
                    frm.WindowState = FormWindowState.Normal;

                }
            }
            if (bFormNameOpen == false)
            {
                Tehnologija.frmTehnologija teh = new Tehnologija.frmTehnologija(Korisnik);
                teh.Show();
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
                    frm.WindowState = FormWindowState.Normal;

                }
            }
            if (bFormNameOpen == false)
            {
                Sifarnici.frmTraseAnaliticki ta = new Sifarnici.frmTraseAnaliticki();
                ta.Show();
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
                    frm.WindowState = FormWindowState.Normal;

                }
            }
            if (bFormNameOpen == false)
            {

                Tehnologija.frmTehnologijaPregled tehp = new Tehnologija.frmTehnologijaPregled(Korisnik);
                tehp.Show();
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
                    frm.WindowState = FormWindowState.Normal;

                }
            }
            if (bFormNameOpen == false)
            {
                Servis.frmPrijavaKvara pkv = new Servis.frmPrijavaKvara();
                pkv.Show();
            }
        }
        private void RibbonPanel_Click(object sender, EventArgs e)
        {

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
                    frm.WindowState = FormWindowState.Normal;

                }
            }
            if (bFormNameOpen == false)
            {
                Sifarnici.frmSifarnikKontrolnihGresaka skg = new Sifarnici.frmSifarnikKontrolnihGresaka();
                skg.Show();
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
                    frm.WindowState = FormWindowState.Normal;

                }
            }
            if (bFormNameOpen == false)
            {

                Dokumenta.frmKontrola kontrola = new Dokumenta.frmKontrola();
                kontrola.Show();
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
                    frm.WindowState = FormWindowState.Normal;

                }
            }
            if (bFormNameOpen == false)
            {
                Sifarnici.frmSifarnikKontrolnihTipovaDokumenta kgtp = new Sifarnici.frmSifarnikKontrolnihTipovaDokumenta();
                kgtp.Show();
            }
        }
        private void toolStripButton88_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmCentralnaTablaRN")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmCentralnaTablaRN cen = new Dokumenta.frmCentralnaTablaRN();
                cen.Show();
            }



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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Servis.frmPregledPopisa1 pp = new Servis.frmPregledPopisa1();
                pp.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                SyncForm.frmPregledPopisa2 ppp = new SyncForm.frmPregledPopisa2();
                ppp.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmPredjenaKilometrazaLokomotiva predjKM = new Dokumenta.frmPredjenaKilometrazaLokomotiva();
                predjKM.Show();
            }
        }

        private void toolStripButton94_Click(object sender, EventArgs e)
        {

            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmCentralnaTablaCpajal")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmCentralnaTablaCpajal cpajak = new Dokumenta.frmCentralnaTablaCpajal();
                cpajak.Show();
            }




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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Servis.frmGrupaKvarovaAuto gka = new Servis.frmGrupaKvarovaAuto();
                gka.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Servis.frmKvaroviAuto kvaut = new Servis.frmKvaroviAuto();
                kvaut.Show();
            }
        }

        private void toolStripButton99_Click(object sender, EventArgs e)
        {
            Dokumenta.frmEvidencijaRadaPromene prom = new Dokumenta.frmEvidencijaRadaPromene();
            prom.Show();
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
            fiksniobr.Show();
        }

        private void toolStripButton104_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "Pitanja")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Testiranje.Pitanja pitanja = new Testiranje.Pitanja();
                pitanja.Show();
            }
        }

        private void ToolStripButton105_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmGenerisanjeTestaKorisnik")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {

                Testiranje.frmGenerisanjeTestaKorisnik generisanje = new Testiranje.frmGenerisanjeTestaKorisnik();
                generisanje.Show();
            }


        }

        private void toolStripButton106_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "TestiranjeStampa")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Testiranje.TestiranjeStampa tesS = new Testiranje.TestiranjeStampa();
                tesS.Show();
            }
        }

        private void toolStripButton107_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "Obrasci")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Testiranje.Obrasci tes6 = new Testiranje.Obrasci();
                tes6.Show();
            }



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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmAutomobili auto = new Dokumenta.frmAutomobili();
                auto.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmAutomobiliPregledPrijava prijaveautomobili = new Dokumenta.frmAutomobiliPregledPrijava();
                prijaveautomobili.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmAutomobiliKvarovi kvarovi = new Dokumenta.frmAutomobiliKvarovi();
                kvarovi.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Servis.frmPrijavaKvaraAuto prijavaKvarAuto = new Servis.frmPrijavaKvaraAuto();
                prijavaKvarAuto.Show();
            }
        }

        private void toolStripButton111_Click(object sender, EventArgs e)
        {


        }

        private void toolStripButton112_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmKreirajGrupu")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Administracija.frmKreirajGrupu kreirajGrupu = new Administracija.frmKreirajGrupu();
                kreirajGrupu.Show();
            }
        }

        private void toolStripButton113_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmDodeliGrupu")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {

                Administracija.frmDodeliGrupu dodeliGrupu = new Administracija.frmDodeliGrupu();
                dodeliGrupu.Show();
            }
        }

        private void toolStripButton114_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmForme")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Administracija.frmForme forme = new Administracija.frmForme();
                forme.Show();
            }
        }

        private void toolStripButton115_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "Prava")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Prava frm = new Prava();
                frm.Show();
            }
        }

        private void toolStripButton26_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton32_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPravljenjeVoza")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmPravljenjeVoza pv = new frmPravljenjeVoza();
                pv.Show();
            }
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
                    frm.WindowState = FormWindowState.Normal;
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
            frmLokomotive lok = new frmLokomotive();
            lok.Show();
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmPropratnice propratnice = new Dokumenta.frmPropratnice();
                propratnice.Show();

            }
        }
        private void toolStripButton117_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPolozenePruge")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Administracija.frmPolozenePruge pruge = new Administracija.frmPolozenePruge();
                pruge.Show();

            }
        }
        private void toolStripButton118_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPolozeneLokomotive")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Administracija.frmPolozeneLokomotive lokomotive = new Administracija.frmPolozeneLokomotive();
                lokomotive.Show();

            }
        }

        private void toolStripTabItem2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton119_Click(object sender, EventArgs e)
        {

            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmMailNajava")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmMailNajava mail = new Dokumenta.frmMailNajava();
                mail.Show();

            }
        }

        private void toolStripEx37_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripEx36_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
           
        }

        private void toolStripButton120_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmGreske")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {

                frmGreske greske = new frmGreske(Korisnik);
                greske.Show();

            }


        }

        private void toolStripButton121_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmDelovi")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmDelovi snac = new frmDelovi(Korisnik);
                snac.Show();

            }
        }

        private void toolStripButton122_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmCene")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmCene frmcen = new frmCene(Korisnik);
                frmcen.Show();
            }
        }

        private void toolStripButton123_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmTipCenovnika")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmTipCenovnika frmTC = new frmTipCenovnika(Korisnik);
                frmTC.Show();
            }




        }

        private void toolStripButton124_Click(object sender, EventArgs e)
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmPartnerji komitenti = new frmPartnerji();
                komitenti.Show();
            }
        }

        private void toolStripButton125_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmNacinDolaskaOdlaska")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmNacinDolaskaOdlaska nac = new frmNacinDolaskaOdlaska(Korisnik);
                nac.Show();
            }
        }

        private void toolStripButton126_Click(object sender, EventArgs e)
        {
            if (Sifarnici.frmLogovanje.Firma == "Leget")
            {
                FormCollection fc = Application.OpenForms;
                bool bFormNameOpen = false;
                foreach (Form frm in fc)
                {
                    //iterate through
                    if (frm.Name == "frmKontejnerStatus")
                    {
                        bFormNameOpen = true;
                        frm.Activate();
                        frm.WindowState = FormWindowState.Normal;
                    }
                }
                if (bFormNameOpen == false)
                {
                    frmKontejnerStatus snac = new frmKontejnerStatus();
                    snac.Show();
                }

            }
            else
            {
                FormCollection fc = Application.OpenForms;
                bool bFormNameOpen = false;
                foreach (Form frm in fc)
                {
                    //iterate through
                    if (frm.Name == "frmStatusRobe")
                    {
                        bFormNameOpen = true;
                        frm.Activate();
                        frm.WindowState = FormWindowState.Normal;
                    }
                }
                if (bFormNameOpen == false)
                {
                    frmStatusRobe snac = new frmStatusRobe();
                    snac.Show();
                }
            }
                
        }

        private void toolStripButton127_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmDelavci")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {

                frmDelavci delav = new frmDelavci();
                delav.Show();
            }
        }

        private void toolStripButton129_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmVrstaRobe")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmVrstaRobe vr = new frmVrstaRobe(Korisnik);
                vr.Show();
            }
        }

        private void toolStripButton130_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmTipKontejnera")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmTipKontejnera tkr = new frmTipKontejnera(Korisnik);
                tkr.Show();
            }
        }

        private void toolStripButton131_Click(object sender, EventArgs e)
        {
            //Ovde mi trebaju Vozila iz Track modal

            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmVozila")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmVozila f = new frmVozila(Korisnik);
                f.Show();
            }

        }

        private void toolStripButton132_Click(object sender, EventArgs e)
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Sifarnici.frmStanice stan = new Sifarnici.frmStanice();
                stan.Show();
            }
        }

        private void toolStripButton133_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPozicija")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmPozicija poz = new frmPozicija(Korisnik);
                poz.Show();
            }
        }

        private void toolStripButton134_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmSkladista")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmSkladista sklad = new frmSkladista(Korisnik);
                sklad.Show();
            }
        }

        private void toolStripButton135_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmVrstaManipulacije")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmVrstaManipulacije frmvrman = new frmVrstaManipulacije(Korisnik);
                frmvrman.Show();
            }



        }

        private void toolStripButton136_Click(object sender, EventArgs e)
        {
            // frmPredefinisanePoruke predefinisane = new frmPredefinisanePoruke();
            // predefinisane.Show();
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmVrstePostupakaUvoz")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Uvoz.frmVrstePostupakaUvoz fvp = new Uvoz.frmVrstePostupakaUvoz();
                fvp.Show();
            }
        }

        private void toolStripButton137_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPregledVozova")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmPregledVozova pvoz = new frmPregledVozova(Korisnik);
                pvoz.Show();
            }
        }

        private void toolStripButton138_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmVoz")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmVoz vozovi = new frmVoz(Korisnik);
                vozovi.Show();
            }
        }

        private void toolStripButton139_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPrijemKontejneraKamionPregled")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmPrijemKontejneraKamionPregled prkamion = new frmPrijemKontejneraKamionPregled(Korisnik);
                prkamion.Show();
            }
        }

        private void toolStripButton140_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPrijemVozomPregled")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmPrijemVozomPregled voz = new frmPrijemVozomPregled(Korisnik);
                voz.Show();
            }
        }

        private void toolStripButton141_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmBukingVoza")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmBukingVoza buking = new frmBukingVoza(Korisnik);
                buking.Show();
            }
        }

        private void toolStripButton142_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPregledOtpreme")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmPregledOtpreme otprema = new frmPregledOtpreme(Korisnik);
                otprema.Show();
            }
        }

        private void toolStripButton143_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPregledOtpremeKamionom")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmPregledOtpremeKamionom pkam = new frmPregledOtpremeKamionom(Korisnik);
                pkam.Show();
            }
        }

        private void toolStripButton144_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPregledNaloziZaPrevoz")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmPregledNaloziZaPrevoz preg = new frmPregledNaloziZaPrevoz();
                preg.Show();
            }
        }

        private void toolStripButton145_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPutniNalog")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Saobracaj.Dokumenta.frmPutniNalog put = new Saobracaj.Dokumenta.frmPutniNalog(Korisnik);
                put.Show();
            }
        }

        private void toolStripButton146_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPregledTovarnihListova")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmPregledTovarnihListova ptl = new frmPregledTovarnihListova();
                ptl.Show();
            }
        }

        private void toolStripButton147_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmManipulacije")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Saobracaj.Dokumenta.frmManipulacije man = new Saobracaj.Dokumenta.frmManipulacije(Korisnik);
                man.Show();
            }
        }

        private void toolStripButton148_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPregledNarucenihManipulacija")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Saobracaj.Dokumenta.frmPregledNarucenihManipulacija pnman = new Saobracaj.Dokumenta.frmPregledNarucenihManipulacija(Korisnik);
                pnman.Show();
            }
        }

        private void toolStripButton149_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPregledManipulacijaPoPartneru")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmPregledManipulacijaPoPartneru mpp = new frmPregledManipulacijaPoPartneru();
                mpp.Show();
            }
        }

        private void toolStripButton150_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPregledSkladistePrijem")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmPregledSkladistePrijem ppr = new frmPregledSkladistePrijem(Korisnik);
                ppr.Show();
            }
        }

        private void toolStripButton151_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmSkladistePrijem")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmSkladistePrijem spr = new frmSkladistePrijem(Korisnik);
                spr.Show();
            }
        }

        private void toolStripButton152_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPregledMedjuskladisniPrenos")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmPregledMedjuskladisniPrenos pprmp = new frmPregledMedjuskladisniPrenos(Korisnik);
                pprmp.Show();
            }



        }

        private void toolStripButton153_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmMedjuskladisniPrenos")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmMedjuskladisniPrenos mpr = new frmMedjuskladisniPrenos(Korisnik);
                mpr.Show();
            }
        }

        private void toolStripButton154_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmSkladisteOtprema")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmSkladisteOtprema sklOtp = new frmSkladisteOtprema(Korisnik);
                sklOtp.Show();
            }
        }

        private void toolStripButton155_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPrometKontejnera")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmPrometKontejnera prometkon = new frmPrometKontejnera(Korisnik);
                prometkon.Show();
            }
        }

        private void toolStripButton156_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmLager")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmLager lager = new frmLager(Korisnik);
                lager.Show();
            }
        }

        private void toolStripButton157_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPopis")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmPopis popis = new frmPopis(Korisnik);
                popis.Show();
            }
        }

        private void toolStripButton158_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPopisPregled")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmPopisPregled pl = new frmPopisPregled();
                pl.Show();
            }
        }

        private void toolStripButton159_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmLagerOperater")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Saobracaj.Promet.frmLagerOperater lager = new Saobracaj.Promet.frmLagerOperater();
                lager.Show();
            }
        }

        private void toolStripButton160_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPregledNaloziZaPrevoz")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmPregledNaloziZaPrevoz preg = new frmPregledNaloziZaPrevoz();
                preg.Show();
            }
        }

        private void toolStripButton161_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmNalogZaPrevoz")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Saobracaj.Dokumenta.frmNalogZaPrevoz nzp = new Saobracaj.Dokumenta.frmNalogZaPrevoz(Korisnik);
                nzp.Show();
            }
        }

        private void toolStripButton162_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPregledPutnihNaloga")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmPregledPutnihNaloga ppn = new frmPregledPutnihNaloga();
                ppn.Show();
            }

        }

        private void toolStripButton167_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPutniNalog")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Saobracaj.Dokumenta.frmPutniNalog pn = new Saobracaj.Dokumenta.frmPutniNalog();
                pn.Show();
            }
        }

        private void toolStripButton163_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPregledRadniNaloziTransport")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmPregledRadniNaloziTransport prnt = new frmPregledRadniNaloziTransport();
                prnt.Show();
            }



        }

        private void toolStripButton164_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmRadniNalogTransport")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Saobracaj.Dokumenta.frmRadniNalogTransport rnt = new Saobracaj.Dokumenta.frmRadniNalogTransport();
                rnt.Show();
            }



        }

        private void toolStripButton165_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmAutoprevozniList2")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Saobracaj.Dokumenta.frmAutoprevozniList2 apl = new Saobracaj.Dokumenta.frmAutoprevozniList2();
                apl.Show();
            }




        }

        private void toolStripButton166_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPregledAutoPrevozniList")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {

                frmPregledAutoPrevozniList pautp = new frmPregledAutoPrevozniList();
                pautp.Show();
            }


        }

        private void Kvarovi_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmSelectTransport6")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmSelectTransport6 kvarovi = new frmSelectTransport6();
                kvarovi.Show();
            }



        }

        private void VozaciPoRutama_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmSelectTransport5")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmSelectTransport5 trans = new frmSelectTransport5();
                trans.Show();
            }
        }

        private void VozilaPoPN_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmSelectTransport4")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmSelectTransport4 trans = new frmSelectTransport4();
                trans.Show();
            }
        }

        private void TurePoRelaciji_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmSelectTransport3")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmSelectTransport3 trans = new frmSelectTransport3();
                trans.Show();
            }
        }

        private void PoTipuKontejnera_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmSelectTransport2")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmSelectTransport2 trans = new frmSelectTransport2();
                trans.Show();
            }
        }

        private void KameniAgregat_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmSelectTransport1")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmSelectTransport1 trans = new frmSelectTransport1();
                trans.Show();
            }
        }

        private void toolStripButton168_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmMaticniPodatki")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmMaticniPodatki mp = new frmMaticniPodatki();
                mp.Show();
            }




        }

        private void prodajnegrupe_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmProdajnaGrupa")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmProdajnaGrupa pg = new frmProdajnaGrupa();
                pg.Show();
            }



        }

        private void toolStripButton171_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmProdajnaGrupa")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmProdajnaGrupa pg = new frmProdajnaGrupa();
                pg.Show();
            }



        }

        private void toolStripButton172_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmJediniceMere")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {

                frmJediniceMere jm = new frmJediniceMere();
                jm.Show();
            }



        }

        private void toolStripButton36_Click_1(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmDogovori")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmDogovori dog = new Dokumenta.frmDogovori();
                dog.Show();
            }
        }

        private void toolStripButton173_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmObjekt")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmObjekt obj = new frmObjekt();
                obj.Show();
            }



        }

        private void toolStripButton174_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmNacinIsporuke")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmNacinIsporuke nacisp = new frmNacinIsporuke();
                nacisp.Show();
            }



        }

        private void toolStripButton175_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmKontaktOsobe")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmKontaktOsobe ko = new Dokumenta.frmKontaktOsobe();
                ko.Show();
            }
        }

        private void toolStripButton169_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton169_Click_1(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmDelovnaMesta")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmDelovnaMesta delm = new frmDelovnaMesta();
                delm.Show();
            }
        }

        private void toolStripButton170_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton176_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmDelavci")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmDelavci del = new frmDelavci();
                del.Show();
            }




        }

        private void toolStripButton170_Click_1(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmBolovanja")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {

                Dokumenta.frmBolovanja bol = new Dokumenta.frmBolovanja();
                bol.Show();
            }


        }

        private void toolStripButton100_Click_1(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPrekovremeniRad")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmPrekovremeniRad prekrad = new Dokumenta.frmPrekovremeniRad();
                prekrad.Show();
            }
        }

        private void toolStripButton50_Click_1(object sender, EventArgs e)
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Mobile.frmSlobodniDani slob = new Mobile.frmSlobodniDani(Korisnik);
                slob.Show();
            }



        }

        private void toolStripButton51_Click_1(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmEvidencijaGodišnjihOdmora")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmEvidencijaGodišnjihOdmora evgo = new Dokumenta.frmEvidencijaGodišnjihOdmora();
                evgo.Show();
            }


        }

        private void toolStripButton56_Click_1(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmAnalizaGO")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Mobile.frmAnalizaGO ago = new Mobile.frmAnalizaGO();
                ago.Show();
            }
        }

        private void toolStripButton57_Click_1(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmAnalizaGOSum")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Mobile.frmAnalizaGOSum GoSum = new Mobile.frmAnalizaGOSum();
                GoSum.Show();
            }
        }

        private void toolStripButton58_Click_1(object sender, EventArgs e)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
            pitanja.Show();
        }

        private void toolStripButton101_Click_1(object sender, EventArgs e)
        {
            Testiranje.frmGenerisanjeTestaKorisnik generisanje = new Testiranje.frmGenerisanjeTestaKorisnik();
            generisanje.Show();
        }

        private void toolStripButton177_Click(object sender, EventArgs e)
        {
            Testiranje.TestiranjeStampa tesS = new Testiranje.TestiranjeStampa();
            tesS.Show();
        }

        private void toolStripButton178_Click(object sender, EventArgs e)
        {
            Testiranje.Obrasci tes6 = new Testiranje.Obrasci();
            tes6.Show();
        }

        private void toolStripButton179_Click(object sender, EventArgs e)
        {
            Sifarnici.frmTelegrami tel = new Sifarnici.frmTelegrami();
            tel.Show();
        }

        private void toolStripButton13_Click_1(object sender, EventArgs e)
        {
            Administracija.frmNotifikacije not = new Administracija.frmNotifikacije();
            not.Show();
        }

        private void toolStripButton59_Click_1(object sender, EventArgs e)
        {
            Mobile.frmZavrsnaDokumentacija zdok = new Mobile.frmZavrsnaDokumentacija();
            zdok.Show();
        }

        private void toolStripButton26_Click_1(object sender, EventArgs e)
        {
            Servis.frmPlombe pl = new Servis.frmPlombe();
            pl.Show();
        }

        private void toolStripButton28_Click_1(object sender, EventArgs e)
        {
            Sifarnici.frmNHM nhm = new Sifarnici.frmNHM();
            nhm.Show();
        }

        private void toolStripButton9_Click_1(object sender, EventArgs e)
        {
            Sifarnici.frmOsoblje osob = new Sifarnici.frmOsoblje();
            osob.Show();
        }

        private void toolStripButton10_Click_1(object sender, EventArgs e)
        {
            Sifarnici.frmTipTelegrama ttel = new Sifarnici.frmTipTelegrama();
            ttel.Show();
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
            Uvoz.frmPregledNerasporedjeni pner = new Uvoz.frmPregledNerasporedjeni(Korisnik);
            pner.Show();
        }

        private void toolStripButton188_Click(object sender, EventArgs e)
        {
            frmTipKontejnera tkon = new frmTipKontejnera();
            tkon.Show();

        }

        private void toolStripButton189_Click(object sender, EventArgs e)
        {
            frmVrstaManipulacije vm = new frmVrstaManipulacije();
            vm.Show();

        }

        private void toolStripButton190_Click(object sender, EventArgs e)
        {
            frmVoz voz = new frmVoz(1);
            voz.Show();

        }

        private void toolStripButton191_Click(object sender, EventArgs e)
        {
            Uvoz.frmPregledPlanovaUtovara fppp = new Uvoz.frmPregledPlanovaUtovara(Korisnik);
            fppp.Show();
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
            frmPregledVozova pvozo = new frmPregledVozova(Korisnik);
            pvozo.Show();

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
            Uvoz.frmRadniNalogInterniPregled RNIP = new Uvoz.frmRadniNalogInterniPregled(Korisnik);
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
            KrajnjeDestinacije KraDes = new KrajnjeDestinacije();
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
            frmTC.Show();

        }

        private void toolStripButton216_Click(object sender, EventArgs e)
        {
            frmCene frmcen = new frmCene(Korisnik);

            frmcen.Show();

        }

        private void toolStripButton107_Click_2(object sender, EventArgs e)
        {
            frmOrganizacionaJedinica otg = new frmOrganizacionaJedinica();
            otg.Show();

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
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmVagoniSerije")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmVagoniSerije frm = new frmVagoniSerije();
                frm.Show();
            }





        }

        private void toolStripButton221_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmValute")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Izvoz.frmValute valute = new Izvoz.frmValute();
                valute.Show();
            }



        }

        private void toolStripButton222_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmVoz")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmVoz voz = new frmVoz(0);
                voz.Show();
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
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmGrupaNepravilnosti")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Nepravilnosti.frmGrupaNepravilnosti np = new Nepravilnosti.frmGrupaNepravilnosti();
                np.Show();
            }




        }

        private void toolStripButton227_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmNeispravnostPostupak")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Nepravilnosti.frmNeispravnostPostupak np = new Nepravilnosti.frmNeispravnostPostupak();
                np.Show();
            }
        }

        private void toolStripButton228_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmOpisNeispravnosti")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Nepravilnosti.frmOpisNeispravnosti np = new Nepravilnosti.frmOpisNeispravnosti();
                np.Show();
            }
        }

        private void toolStripButton229_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmRazredNepravilnosti")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Nepravilnosti.frmRazredNepravilnosti np = new Nepravilnosti.frmRazredNepravilnosti();
                np.Show();
            }



        }

        private void toolStripButton230_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmVrstaNepravilnosti")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Nepravilnosti.frmVrstaNepravilnosti np = new Nepravilnosti.frmVrstaNepravilnosti();
                np.Show();
            }



        }

        private void toolStripButton231_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPregledAktivnosti")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmPregledAktivnosti pr = new Dokumenta.frmPregledAktivnosti();
                pr.Show();
            }



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
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmTipKontejnera")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmTipKontejnera tk = new frmTipKontejnera();
                tk.Show();
            }
        }

        private void toolStripButton233_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmVucaStatusi")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmVucaStatusi vs = new Dokumenta.frmVucaStatusi();
                vs.Show();
            }



        }

        private void toolStripButton234_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmStavkePrimopredaje")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {

                Administracija.frmStavkePrimopredaje spp = new Administracija.frmStavkePrimopredaje();
                spp.Show();
            }

        }

        private void toolStripButton235_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmVrstePlombi")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {


                Izvoz.frmVrstePlombi vblombi = new Izvoz.frmVrstePlombi();
                vblombi.Show();
            }

        }

        private void toolStripButton237_Click(object sender, EventArgs e)
        {

            Uvoz.frmPregledPlanovaUtovara fppp = new Uvoz.frmPregledPlanovaUtovara(Korisnik);
            fppp.Show();
        }

        private void toolStripButton238_Click(object sender, EventArgs e)
        {
            Izvoz.frmPlanoviIzvoza pl = new Izvoz.frmPlanoviIzvoza();
            pl.Show();
        }

        private void toolStripButton239_Click(object sender, EventArgs e)
        {

            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmOrganizacionaJedinica")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmOrganizacionaJedinica otg = new frmOrganizacionaJedinica(Korisnik);
                otg.Show();
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
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPrijavaSmeneOld")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Mobile.frmPrijavaSmeneOld pso = new Mobile.frmPrijavaSmeneOld();
                pso.Show();
            }

        }

        private void toolStripButton243_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPrijavaMasinovodjeOld")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Servis.frmPrijavaMasinovodjeOld mas = new Servis.frmPrijavaMasinovodjeOld();
                mas.Show();
            }




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
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPartnerji")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmPartnerji parn = new frmPartnerji();
                parn.Show();
            }




        }

        private void toolStripButton244_Click(object sender, EventArgs e)
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmPartnerji parn = new frmPartnerji();
                parn.Show();
            }

        }

        private void toolStripButton245_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmTipCenovnika")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmTipCenovnika parn = new frmTipCenovnika();
                parn.Show();
            }


        }

        private void toolStripButton266_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmTipCenovnika")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmTipCenovnika parn = new frmTipCenovnika();
                parn.Show();
            }

        }

        private void toolStripButton267_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmVrstaManipulacije")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmVrstaManipulacije vm = new frmVrstaManipulacije();
                vm.Show();

            }



        }

        private void toolStripButton246_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmVrstaManipulacije")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmVrstaManipulacije vm = new frmVrstaManipulacije();
                vm.Show();

            }

        }

        private void toolStripButton268_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmCene")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmCene cen = new frmCene();
                cen.Show();

            }




        }

        private void toolStripButton247_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmCene")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmCene cen = new frmCene();
                cen.Show();

            }




        }

        private void toolStripButton248_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "Brodovi")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Uvoz.Brodovi brodovi = new Uvoz.Brodovi();
                brodovi.Show();
            }


        }

        private void toolStripButton269_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "Brodovi")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Uvoz.Brodovi brodovi = new Uvoz.Brodovi();
                brodovi.Show();
            }
        }

        private void toolStripButton270_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "Carinarnice")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Uvoz.Carinarnice car = new Uvoz.Carinarnice();
                car.Show();
            }

        }

        private void toolStripButton249_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "Carinarnice")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Uvoz.Carinarnice car = new Uvoz.Carinarnice();
                car.Show();
            }
        }

        private void toolStripButton250_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "Nalogodavci")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {

                Uvoz.Nalogodavci nalog = new Uvoz.Nalogodavci();
                nalog.Show();
            }


        }

        private void toolStripButton271_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "Nalogodavci")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {

                Uvoz.Nalogodavci nalog = new Uvoz.Nalogodavci();
                nalog.Show();
            }
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
            nhm.Show();
        }

        private void toolStripButton34_Click_2(object sender, EventArgs e)
        {
            frmNHM nhm = new frmNHM();
            nhm.Show();

        }

        private void toolStripButton276_Click(object sender, EventArgs e)
        {
            Uvoz.frmVrstaCarinskogPostupka vpu = new Uvoz.frmVrstaCarinskogPostupka();
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
            tk.Show();

        }

        private void toolStripButton279_Click(object sender, EventArgs e)
        {
            frmTipKontejnera tk = new frmTipKontejnera();

            tk.Show();

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



            frmPregledVozova pv = new frmPregledVozova(2);

            pv.Show();

        }

        private void toolStripButton281_Click(object sender, EventArgs e)
        {
            frmPregledVozova pv = new frmPregledVozova(1);

            pv.Show();

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

            oj.Show();

        }

        private void toolStripButton263_Click(object sender, EventArgs e)
        {
            frmOrganizacionaJedinica oj = new frmOrganizacionaJedinica();
            oj.Show();

        }

        private void toolStripButton208_Click_1(object sender, EventArgs e)
        {
            KrajnjeDestinacije ikd = new KrajnjeDestinacije();
            ikd.Show();
        }

        private void toolStripButton286_Click(object sender, EventArgs e)
        {
            KrajnjeDestinacije ikd = new KrajnjeDestinacije();
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
            Izvoz.frmIzvoz izvoz = new Izvoz.frmIzvoz(Korisnik);
            izvoz.Show();
        }

        private void toolStripButton213_Click_1(object sender, EventArgs e)
        {
            Izvoz.frmPregledKontejneraIzvoz pkizvoz = new Izvoz.frmPregledKontejneraIzvoz();
            pkizvoz.Show();
        }

        private void toolStripButton222_Click_1(object sender, EventArgs e)
        {
            frmVoz voz = new frmVoz(1);
            voz.Show();
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
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "PoLokomotivama")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                ReportingOperatika.PoLokomotivama pl = new ReportingOperatika.PoLokomotivama();
                pl.Show();

            }



        }

        private void toolStripButton295_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPregledMobilni2")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                SyncForm.frmPregledMobilni2 drugi = new SyncForm.frmPregledMobilni2();
                drugi.Show();

            }




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
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "PoLokomotivama")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                ReportingOperatika.PoLokomotivama pLok = new ReportingOperatika.PoLokomotivama();
                pLok.Show();

            }



        }

        private void toolStripButton279_Click_2(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPregledMobilni2")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                SyncForm.frmPregledMobilni2 pm2 = new SyncForm.frmPregledMobilni2();
                pm2.Show();

            }



        }

        private void toolStripButton296_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPregledMasinovodje2")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                SyncForm.frmPregledMasinovodje2 pm2 = new SyncForm.frmPregledMasinovodje2();
                pm2.Show();

            }



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
            RN1PrijemVoza frm = new RN1PrijemVoza();
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
            RN6OtpremaPlatforme frm = new RN6OtpremaPlatforme();
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
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPregledRID")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmPregledRID prid = new Dokumenta.frmPregledRID();
                prid.Show();

            }


        }

        private void toolStripButton316_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmRIDPoNajavama")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmRIDPoNajavama rPNajavama = new Dokumenta.frmRIDPoNajavama();
                rPNajavama.Show();

            }


        }

        private void toolStripButton317_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmRIDTeretnice")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmRIDTeretnice frter = new Dokumenta.frmRIDTeretnice();
                frter.Show();

            }



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

        private void toolStripButton321_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPrijemKontejneraKamionLegetUvoz")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmPrijemKontejneraKamionLegetUvoz uv1 = new Dokumenta.frmPrijemKontejneraKamionLegetUvoz();
                uv1.Show();
            }

        }

        private void toolStripButton322_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPrijemKontejneraLegetVozUvoz")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmPrijemKontejneraLegetVozUvoz uv2 = new Dokumenta.frmPrijemKontejneraLegetVozUvoz();
                uv2.Show();
            }
        }

        private void toolStripButton323_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmOtpremaKontejneraUvozKamion")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmOtpremaKontejneraUvozKamion uv3 = new Dokumenta.frmOtpremaKontejneraUvozKamion();
                uv3.Show();
            }



        }

        private void toolStripButton324_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPrijemKontejneraKamionLegetIzvoz")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmPrijemKontejneraKamionLegetIzvoz iz1 = new Dokumenta.frmPrijemKontejneraKamionLegetIzvoz();
                iz1.Show();
            }
        }

        private void toolStripButton325_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmOtpremaKontejneraLegetIZVOZ")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmOtpremaKontejneraLegetIZVOZ iz2 = new Dokumenta.frmOtpremaKontejneraLegetIZVOZ();
                iz2.Show();
            }
        }

        private void toolStripButton326_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmOtpremaKontejneraIzvozKamion")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmOtpremaKontejneraIzvozKamion iz2 = new Dokumenta.frmOtpremaKontejneraIzvozKamion();
                iz2.Show();
            }



        }

        private void toolStripButton327_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "NosiociTroskova")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {

                Pantheon_Export.NosiociTroskova nt = new Pantheon_Export.NosiociTroskova();
                nt.Show();
            }


        }

        private void toolStripButton328_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "Predvidjanje")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Pantheon_Export.Predvidjanje pv = new Pantheon_Export.Predvidjanje();
                pv.Show();
            }



        }

        private void toolStripButton329_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "UlFakPregled")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Pantheon_Export.UlFakPregled ufp = new Pantheon_Export.UlFakPregled();
                ufp.Show();
            }




        }

        private void toolStripButton330_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "UlazneFakture")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Pantheon_Export.UlazneFakture uf = new Pantheon_Export.UlazneFakture();
                uf.Show();
            }



        }

        private void toolStripButton331_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "IzlazneFakturePregled")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Pantheon_Export.IzlazneFakturePregled izp = new Pantheon_Export.IzlazneFakturePregled();
                izp.Show();
            }
        }

        private void toolStripButton332_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "IzlazneFakture")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Pantheon_Export.IzlazneFakture izf = new Pantheon_Export.IzlazneFakture();
                izf.Show();
            }



        }

        private void toolStripButton333_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmVrstaCarinskogPostupka")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Uvoz.frmVrstaCarinskogPostupka vpu = new Uvoz.frmVrstaCarinskogPostupka();
                vpu.Show();
            }



        }

        private void toolStripEx47_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton334_Click(object sender, EventArgs e)
        {
        }

        private void toolStripButton334_Click_1(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmIzvozIstorija")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Izvoz.frmIzvozIstorija ii = new Izvoz.frmIzvozIstorija();
                ii.Show();
            }




        }

        private void toolStripButton335_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPregledKontejneraIzvozIzUvoza")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Izvoz.frmPregledKontejneraIzvozIzUvoza iiu = new Izvoz.frmPregledKontejneraIzvozIzUvoza();
                iiu.Show();
            }

        }

        private void toolStripButton336_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "PlaniraniPretovar")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                PlaniraniPretovar frm = new PlaniraniPretovar();
                frm.Show();
            }

           



        }

        private void toolStripButton338_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmOtpremaKontejneraUvozKamion")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Saobracaj.Dokumenta.frmOtpremaKontejneraUvozKamion pkam = new Saobracaj.Dokumenta.frmOtpremaKontejneraUvozKamion(Korisnik, 0);
                pkam.Show();
            }


        }

        private void toolStripButton339_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "Opportunity")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Pantheon_Export.Opportunity op = new Pantheon_Export.Opportunity();
                op.Show();
            }



        }

        private void toolStripButton340_Click(object sender, EventArgs e)
        {

            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "PrihodiPosla")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Pantheon_Export.PrihodiPosla pp = new Pantheon_Export.PrihodiPosla();
                pp.Show();
            }

        }

        private void toolStripButton341_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmAnalizaRadnihNaloga")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                RadniNalozi.frmAnalizaRadnihNaloga arn = new frmAnalizaRadnihNaloga();
                arn.Show();
            }



        }

        private void toolStripButton342_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmFormiranjeRobnihDokumenata")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Saobracaj.Uvoz.frmFormiranjeRobnihDokumenata rd = new Uvoz.frmFormiranjeRobnihDokumenata();
                rd.Show();
            }



        }

        private void toolStripButton343_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmKontejnerStatus")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Saobracaj.Sifarnici.frmKontejnerStatus ks = new frmKontejnerStatus();
                ks.Show();
            }



        }

        private void toolStripEx34_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton345_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmKontejnerTekuce")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Uvoz.frmKontejnerTekuce kt = new Uvoz.frmKontejnerTekuce();
                kt.Show();
            }


        }

        private void toolStripButton344_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmScenario")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmScenario SCEN = new frmScenario();
                SCEN.Show();
            }



        }

        private void toolStripButton347_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPregledSkladistaNovi")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Saobracaj.RadniNalozi.frmPregledSkladistaNovi pr = new Saobracaj.RadniNalozi.frmPregledSkladistaNovi();
                pr.Show();
            }



        }

        private void toolStripButton348_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmUvozKonacnaZaglavlje")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Saobracaj.Uvoz.frmUvozKonacnaZaglavlje fukz = new Saobracaj.Uvoz.frmUvozKonacnaZaglavlje();
                fukz.Show();
            }



        }

        private void toolStripButton349_Click(object sender, EventArgs e)
        {
            Izvoz.DogovoreniPosloviIzvoza dp = new Izvoz.DogovoreniPosloviIzvoza();
            dp.Show();
        }

        private void toolStripButton350_Click(object sender, EventArgs e)
        {

            Izvoz.DogovoreniPosloviIzvoza dp = new Izvoz.DogovoreniPosloviIzvoza();
            dp.Show();
        }

        private void toolStripButton351_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
               // if (frm.Name == "TerminalOpredeljenje")
                    if (frm.Name == "frmIzvozTerminalPovezi")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Izvoz.frmIzvozTerminalPovezi to = new Izvoz.frmIzvozTerminalPovezi();
                to.Show();

            }




        }

        private void toolStripButton352_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "KrajnjeDestinacije")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                KrajnjeDestinacije ikd = new KrajnjeDestinacije();
                ikd.Show();
            }



        }

        private void toolStripButton353_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "ApiPregled")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Pantheon_Export.ApiPregled ap = new Pantheon_Export.ApiPregled();
                ap.Show();
            }



        }

        private void toolStripButton354_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmCIRPregled")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmCIRPregled pc = new frmCIRPregled();
                pc.Show();
            }



        }

        private void toolStripButton355_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmTerminalOperacije")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmTerminalOperacije to = new frmTerminalOperacije();
                to.Show();

            }



        }

        private void toolStripButton356_Click(object sender, EventArgs e)
        {
            frmKalmarGateInVoz giv = new frmKalmarGateInVoz();
            giv.Show();
        }

        private void toolStripButton357_Click(object sender, EventArgs e)
        {
            frmVizuelniPregledi vp = new frmVizuelniPregledi();
            vp.Show();
        }

        private void toolStripButton358_Click(object sender, EventArgs e)
        {
            frmKalmarGateOut kgo = new frmKalmarGateOut();
            kgo.Show();
        }

        private void toolStripButton359_Click(object sender, EventArgs e)
        {



            frmKalmarGateIN kgi = new frmKalmarGateIN();
            kgi.Show();
        }

        private void toolStripButton360_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton361_Click(object sender, EventArgs e)
        {
            frmCIRPregledac cIRPregledac = new frmCIRPregledac();
            cIRPregledac.Show();
        }

        private void toolStripButton360_Click_1(object sender, EventArgs e)
        {
            frmKalmarGateOutVoz gov = new frmKalmarGateOutVoz();
            gov.Show();
        }

        private void toolStripEx40_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton362_Click(object sender, EventArgs e)
        {
            frmKalmarGateINKaminonPreneseno kgikp = new frmKalmarGateINKaminonPreneseno();
            kgikp.Show();
        }

        private void toolStripButton112_Click_1(object sender, EventArgs e)
        {
            Uvoz.Uvoz uv = new Uvoz.Uvoz();
            uv.Show();
        }

        private void toolStripButton113_Click_1(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPredefinisanePoruke")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmPredefinisanePoruke to = new frmPredefinisanePoruke();
                to.Show();

            }
        }

        private void toolStripButton363_Click(object sender, EventArgs e)
        {
            frmPlanoviIzvoza pi = new frmPlanoviIzvoza(1);
            pi.Show();
        }

        private void toolStripButton114_Click_1(object sender, EventArgs e)
        {
            Uvoz.frmPregledPlanovaUtovara fppp = new Uvoz.frmPregledPlanovaUtovara(1);
            fppp.Show();
        }

        private void toolStripButton364_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton364_Click_1(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPrebacivanjeIzPlanaUPlan")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Izvoz.frmPrebacivanjeIzPlanaUPlan to = new Izvoz.frmPrebacivanjeIzPlanaUPlan();
                to.Show();

            }


           
        }

        private void toolStripButton364_Click_2(object sender, EventArgs e)
        {
            Izvoz.frmDopunaPlanaPraznimIzvoz ddd = new frmDopunaPlanaPraznimIzvoz();    
            ddd.Show();
        }

        private void toolStripButton365_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmRadniNalogInterniPregled")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Uvoz.frmRadniNalogInterniPregled RNIP = new Uvoz.frmRadniNalogInterniPregled(Korisnik);
                RNIP.Show();
            }
        }

        private void toolStripButton366_Click(object sender, EventArgs e)
        {
            frmKontejnerTekuceOperacijeLog lo = new frmKontejnerTekuceOperacijeLog();
            lo.Show();
        }

        private void toolStripButton367_Click(object sender, EventArgs e)
        {
            PrijemnicaPregled pp = new PrijemnicaPregled();
            pp.Show();
        }

        private void toolStripButton368_Click(object sender, EventArgs e)
        {
            OtpremnicaPregled op = new OtpremnicaPregled(); op.Show();
        }

        private void toolStripButton369_Click(object sender, EventArgs e)
        {
            frmKontejnerTekuceArhiv kta = new frmKontejnerTekuceArhiv();
            kta.Show();
        }

        private void toolStripButton370_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmDefinisiPoziciju")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {

                frmDefinisiPoziciju greske = new frmDefinisiPoziciju();
                greske.Show();

            }
        }

        private void toolStripButton337_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripButton371_Click(object sender, EventArgs e)
        {
            frmDodatneUsluge dodu = new frmDodatneUsluge();
            dodu.Show();
        }

        private void toolStripButton372_Click(object sender, EventArgs e)
        {
            Sifarnici.frmJediniceMere jm = new frmJediniceMere();
            jm.Show();
        }

        private void toolStripButton373_Click(object sender, EventArgs e)
        {
            VaganjePregled pp = new VaganjePregled();
            pp.Show();
        }

        private void toolStripTabItem8_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton374_Click(object sender, EventArgs e)
        {
            frmPregledLokomotivaPrimopredaja plp = new frmPregledLokomotivaPrimopredaja();
            plp.Show();
        }

        private void toolStripButton377_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmRadniNalogPregledSluzbenik")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Uvoz.frmRadniNalogPregledSluzbenik RNIP = new Uvoz.frmRadniNalogPregledSluzbenik(Korisnik);
                RNIP.Show();
            }
        }

        private void toolStripButton375_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPopisKonterjnera")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmPopisKonterjnera frm = new frmPopisKonterjnera();
                frm.Show();
            }
        }

        private void toolStripButton376_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmVozila")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmVozila frm = new frmVozila();
                frm.Show();
            }
        }

        private void toolStripButton378_Click(object sender, EventArgs e)
        {
            //  frmVozilaPregledPrijava

            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmVozilaPregledPrijava")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmVozilaPregledPrijava frm = new frmVozilaPregledPrijava();
                frm.Show();
            }
        }

        private void MainP_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton294_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmStatusUsluge")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmStatusUsluge frm = new frmStatusUsluge();
                frm.Show();
            }
        }

        private void sfButton1_Click(object sender, EventArgs e)
        {
            Uvoz.Brodovi brod = new Uvoz.Brodovi();
            brod.Show();
        }

        private void toolStripButton379_Click(object sender, EventArgs e)
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
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmAutomobili autom = new Dokumenta.frmAutomobili();
                autom.Show();
            }
        }

        private void toolStripButton380_Click(object sender, EventArgs e)
        {
            Izvoz.frmValute val = new Izvoz.frmValute();
            val.Show();
        }

        private void toolStripButton381_Click(object sender, EventArgs e)
        {
            Drumski.frmStatusVozila st = new Drumski.frmStatusVozila();
            st.Show();
        }

        private void toolStripButton382_Click(object sender, EventArgs e)
        {
            Drumski.frmVrstaVozila vv = new Drumski.frmVrstaVozila();
            vv.Show();
        }

        private void toolStripButton408_Click(object sender, EventArgs e)
        {
            Drumski.frmPregledNalogaDrumski fd = new Drumski.frmPregledNalogaDrumski();
            fd.Show();
        }

        private void toolStripButton409_Click(object sender, EventArgs e)
        {
            Drumski.frmPakovanjeKamiona fppp = new Drumski.frmPakovanjeKamiona();
            fppp.Show();
        }

        private void toolStripButton383_Click(object sender, EventArgs e)
        {
            Izvoz.frmOperativniPlanIzvoz op = new Izvoz.frmOperativniPlanIzvoz();
            op.Show();
        }

        private void toolStripButton384_Click(object sender, EventArgs e)
        {
            Uvoz.frmOperativniPlanUvoz vv = new Uvoz.frmOperativniPlanUvoz();
            vv.Show();
        }
    }
    }

