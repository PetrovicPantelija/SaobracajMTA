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
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            InitializeComponent();

        }

        public frmMainNew(string Logovan, int Lozinka)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
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