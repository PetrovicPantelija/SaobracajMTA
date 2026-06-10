using Saobracaj.Sifarnici;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main
{
    public partial class RadniNaloziSkladista : Form
    {
        string Vrsta;
        int IDInterni;
        string Ulaz;
        string Korisnik = frmLogovanje.user.ToString();

        public RadniNaloziSkladista()
        {
            InitializeComponent();
        }
        public RadniNaloziSkladista(string vrsta)
        {
            InitializeComponent();
            Vrsta=vrsta;
        }
        public RadniNaloziSkladista(int idInterni,string ulaz,string vrsta)
        {
            InitializeComponent();
            IDInterni=idInterni;
            Ulaz=ulaz;
            Vrsta = vrsta;
        }

        private void btnPrijem_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuBezPrava(
                () => new Dokumenta.Prijem(IDInterni, Ulaz, Vrsta,Korisnik)
            );
        }

        private void btnOtprema_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuBezPrava(
                () => new Dokumenta.Otprema(IDInterni, Ulaz, Vrsta,Korisnik)
            );
        }

        private void btnPretovar_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuBezPrava(
                () => new Dokumenta.Pretovar(IDInterni, Ulaz, Vrsta)
            );
        }

        private void btnInterniPrenos_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuBezPrava(
                () => new Dokumenta.InterniPrenos(IDInterni, Ulaz, Vrsta)
            );
        }

        private void btnVanredniPoslovi_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuBezPrava(
                () => new Dokumenta.VanredniPoslovi(IDInterni, Ulaz, Vrsta)
            );
        }

        private void btnInterniPoslovi_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuBezPrava(
                () => new Dokumenta.InterniPoslovi(IDInterni, Ulaz, Vrsta)
            );
        }
    }
}
