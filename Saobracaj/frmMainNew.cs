using Saobracaj.Dokumenta;
using Saobracaj.Servis;
using System;
using System.Windows.Forms;

namespace Saobracaj
{
    public partial class frmMainNew : Syncfusion.Windows.Forms.Tools.RibbonForm
    {
        string Korisnik = "";
        public frmMainNew()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1JBaF5cXmRCd1p/TH5YfUNzdUVEY1ZUTXxaS1ZhSXxVd0VjX35ec3FXQ2JdUUZ9XEc=");
            InitializeComponent();

        }

        public frmMainNew(string Logovan, int Lozinka)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1JBaF5cXmRCd1p/TH5YfUNzdUVEY1ZUTXxaS1ZhSXxVd0VjX35ec3FXQ2JdUUZ9XEc=");
            InitializeComponent();
            Korisnik = Logovan;
        }



      


        private void toolStripButton28_Click(object sender, EventArgs e)
        {
            frmPravljenjeVoza frmPV = new frmPravljenjeVoza();
            frmPV.Show();
        }

      


       

        private void toolStripButton67_Click(object sender, EventArgs e)
        {
/////Panta u osnovne zarade
            Dokumenta.frmIzracunZarada fiz = new Dokumenta.frmIzracunZarada();
            fiz.Show();
        }


     



     

      

     

        private void toolStripButton80_Click(object sender, EventArgs e)
        {
            Tehnologija.frmTehnologija teh = new Tehnologija.frmTehnologija();
            teh.Show();
        }

    }
}