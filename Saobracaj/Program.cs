using Syncfusion.Windows.Forms;
using System;
using System.Windows.Forms;
using static Syncfusion.Windows.Forms.Tools.NavigationView;
using System.IO;
namespace Saobracaj
{
    
    static class Program
    {
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
        string companyosn = "";
            string basedir = AppDomain.CurrentDomain.BaseDirectory;
            string[] txtFile = Directory.GetFiles(basedir, "*txt");
            foreach (string file in txtFile)
            {

                companyosn = Path.GetFileNameWithoutExtension(file);
            }
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            //  Application.EnableVisualStyles();
            // Application.SetCompatibleTextRenderingDefault(false);
            SkinManager.ApplicationVisualTheme = "Office2016Colourful";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (companyosn.ToString() == "Leget")
            {
                Application.Run(new Sifarnici.frmLogovanjeSecond());
            }
            else
            {
                Application.Run(new Sifarnici.frmLogovanje());
            }
            
            // Application.Run(new Dokumenta.frmTest());
        }
    }
}
