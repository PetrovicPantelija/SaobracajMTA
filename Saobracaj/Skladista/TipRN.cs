using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Testiranje.Sifarnici;

namespace Saobracaj.Skladista
{
    public partial class TipRN : Form
    {
        string vrsta;
        string Korisnik = Saobracaj.Sifarnici.frmLogovanje.user;

        public TipRN(string Vrsta,string korisnik)
        {
            InitializeComponent();
            vrsta = Vrsta;
        }

        private void btnPrijem_Click(object sender, EventArgs e)
        {
            if(Application.OpenForms.OfType<RNCSPregled>().Any())
            {
                Application.OpenForms.OfType<RNCSPregled>().First().Close();
            }
            
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijem.Text,
                () => new RNCSPregled(vrsta, "Prijem", Korisnik)
            );
           /* var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuBezPrava(() => new RNCSPregled(vrsta,"Prijem",Korisnik));*/
        }

        private void btnOtprema_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<RNCSPregled>().Any())
            {
                Application.OpenForms.OfType<RNCSPregled>().First().Close();
            }
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijem.Text,
                () => new RNCSPregled(vrsta, "Otprema", Korisnik)
            );
        }

        private void btnPretovar_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<RNCSPregled>().Any())
            {
                Application.OpenForms.OfType<RNCSPregled>().First().Close();
            }
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijem.Text,
                () => new RNCSPregled(vrsta, "Pretovar", Korisnik)
            );
        }

        private void btnInterniPrenos_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<RNCSPregled>().Any())
            {
                Application.OpenForms.OfType<RNCSPregled>().First().Close();
            }
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijem.Text,
                () => new RNCSPregled(vrsta, "Interni prenos", Korisnik)
            );
        }

        private void btnVanredniPoslovi_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<RNCSPregled>().Any())
            {
                Application.OpenForms.OfType<RNCSPregled>().First().Close();
            }
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijem.Text,
                () => new RNCSPregled(vrsta, "Vanredni poslovi", Korisnik)
            );
        }

        private void btnInterniPoslovi_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<RNCSPregled>().Any())
            {
                Application.OpenForms.OfType<RNCSPregled>().First().Close();
            }
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijem.Text,
                () => new RNCSPregled(vrsta, "Interni poslovi", Korisnik)
            );
        }
    }
}
