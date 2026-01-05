using Saobracaj.Dokumenta;
using Saobracaj.Izvoz;
using Saobracaj.Uvoz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.MainLeget.PrijemIOtpremaKamiona
{
    public partial class frmPrijemIOtpremaKamiona1 : Form
    {
        public string Korisnik = Sifarnici.frmLogovanje.user;
        public frmPrijemIOtpremaKamiona1()
        {
            InitializeComponent();
        }

        private void btnPrijemIOtpremaKamiona3_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaKamiona3.Text,
                () => new frmKontejnerTekuceArhiv()
            );
        }

        private void btnPrijemIOtpremaKamiona1_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaKamiona1.Text,
                () => new Uvoz.Uvoz()
            );
        }

        private void btnPrijemIOtpremaKamiona2_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaKamiona2.Text,
                () => new frmRadniNalogPregledSluzbenik(Korisnik)
            );

            
        }

        private void btnPrijemIOtpremaKamiona4_Click(object sender, EventArgs e)
        {
            //Izvoz
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaKamiona4.Text,
                () => new frmPrijemIOtpremaKamiona2()
            );
        }

        private void btnPrijemIOtpremaKamiona5_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaKamiona5.Text,
                () => new frmPrijemIOtpremaKamiona3()
            );
        }

        private void btnPrijemIOtpremaKamiona6_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaKamiona6.Text,
                () => new VaganjePregled()
            );
        }

        private void btnPrijemIOtpremaKamiona7_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaKamiona7.Text,
                () => new frmPrijemIOtpremaKamiona4()
            );
        }
    }
}
