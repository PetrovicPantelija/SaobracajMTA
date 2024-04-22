using System;

namespace Saobracaj.Uvoz
{
    public partial class frmFormiranjeRobnihDokumenata : Syncfusion.Windows.Forms.Office2010Form
    {
        public frmFormiranjeRobnihDokumenata()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmPrijemVozaIzPlana rd1 = new frmPrijemVozaIzPlana();
            rd1.Show();
        }

        private void frmFormiranjeRobnihDokumenata_Load(object sender, EventArgs e)
        {

        }
    }
}
