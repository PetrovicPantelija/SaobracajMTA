using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saobracaj
{
    //
    public class CompanyConfiguration
    {
        public string Naziv { get; set; }
        public string DB { get; set; }
        public string Dokumenta { get; set; }
    }

    public static class ConfigurationManager
    {
        public static CompanyConfiguration GetCompanyConfiguration(string naziv)
        {
            switch (naziv)
            {
                case "Leget":
                    return new CompanyConfiguration
                    {
                        Naziv = "Leget",
                        DB = @"Data Source=192.168.99.10\SQLEXPRESS2019;Initial Catalog=TESTIRANJE;User ID=sa;Password=duki7990;",
                        Dokumenta = @"\\192.168.99.10\Leget\"
                    };
                case "TA":
                    return new CompanyConfiguration
                    {
                        Naziv = "TA",
                        DB = @"Data Source=192.168.129.7\;Initial Catalog=TESTIRANJE;User ID=sa;Password=duki7990",
                        Dokumenta= @"\\192.168.99.10\TA\"
                    };
                case "KP":
                    return new CompanyConfiguration
                    {
                        Naziv = "KP",
                        DB = @"Data Source=192.168.1.6\SQLEXPRESS2008K;Initial Catalog=Perftech_Beograd;User ID=sa;Password=duki7990;"
                    };
                default:
                    throw new ArgumentException("Company configuration not found");
            }
        }
    }
}
