using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saobracaj
{
    public class CompanyConfiguration
    {
        public string Naziv { get; set; }
        public string DB { get; set; }
        public string Dokumenta { get; set; }
        public string PIB { get; set; }
        public string Name_Value { get; set; }
        public string Ulica_Value { get; set; }
        public string Grad_Value { get; set; }
        public string PostanskiBroj_Value { get; set; }
        public string Line_Value { get; set; }
        public string CompanyID_Value { get; set; } 
        public string MB_Value { get; set; }    
        public string EmailSender_Value { get; set; }
        public string OsnovnoSkladiste { get; set; }
        public string OsnovnaLokacija { get; set; }
    }

    public static class ConfigManager
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
                        Dokumenta = @"\\192.168.99.10\Leget\",
                        PIB = "100791711",
                        Name_Value = "RTC LUKA LEGET AD SREMSKA MITROVICA",
                        Ulica_Value = "Jarački put 10",
                        Grad_Value = "Sremska Mitrovica",
                        PostanskiBroj_Value = "22000",
                        Line_Value = "Jarački put 10,22000 Sremska Mitrovica",
                        CompanyID_Value = "RS100791711",
                        MB_Value = "08039534",
                        EmailSender_Value = "office@leget.rs",
                        OsnovnoSkladiste = "1",
                        OsnovnaLokacija="1"

                    };
                case "TA":
                    return new CompanyConfiguration
                    {
                        Naziv = "TA",
                        DB = @"Data Source=192.168.129.7\;Initial Catalog=TESTIRANJE;User ID=sa;Password=duki7990",
                        Dokumenta= @"\\192.168.99.10\TA\",
                        PIB = "108430447",
                        Name_Value = "TRANSAGENT OPERATOR DOO BEOGRAD\r\n",
                        Ulica_Value = "Uzun Mirkova 3",
                        Grad_Value = "Beograd",
                        PostanskiBroj_Value = "11000",
                        Line_Value = "Uzun Mirkova 3,11000 Beograd",
                        CompanyID_Value = "RS108430447",
                        MB_Value = "20997923",
                        EmailSender_Value = "office@transagent.rs",
                        OsnovnoSkladiste= "1",
                        OsnovnaLokacija= "1"
                        
                    };
                case "DPT":
                    return new CompanyConfiguration
                    {
                        Naziv = "DPT",
                        DB = @"Data Source=192.168.129.7\;Initial Catalog=DPTDB;User ID=sa;Password=duki7990",
                        Dokumenta = @"\\192.168.99.10\TA\",
                        PIB = "108430447",
                        Name_Value = "TRANSAGENT OPERATOR DOO BEOGRAD\r\n",
                        Ulica_Value = "Uzun Mirkova 3",
                        Grad_Value = "Beograd",
                        PostanskiBroj_Value = "11000",
                        Line_Value = "Uzun Mirkova 3,11000 Beograd",
                        CompanyID_Value = "RS108430447",
                        MB_Value = "20997923",
                        EmailSender_Value = "office@transagent.rs",
                        OsnovnoSkladiste = "1",
                        OsnovnaLokacija = "1"

                    };
                case "KP":
                    return new CompanyConfiguration
                    {
                        Naziv = "KP",
                        DB = @"Data Source=192.168.1.6\SQLEXPRESS2008K;Initial Catalog=Perftech_Beograd;User ID=sa;Password=duki7990;",

                        Dokumenta = @"\\192.168.99.10\Leget\",
                        PIB = "100791711",
                        Name_Value = "RTC LUKA LEGET AD SREMSKA MITROVICA",
                        Ulica_Value = "Jarački put 10",
                        Grad_Value = "Sremska Mitrovica",
                        PostanskiBroj_Value = "22000",
                        Line_Value = "Jarački put 10,22000 Sremska Mitrovica",
                        CompanyID_Value = "RS100791711",
                        MB_Value = "08039534",
                        EmailSender_Value = "office@leget.rs"
                    };
                default:
                    throw new ArgumentException("Company configuration not found");
            }
        }
    }
}
