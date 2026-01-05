using Saobracaj.Drumski;
using Saobracaj.MainLeget.LegNew;
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
using Testiranje.Dokumeta;

namespace Saobracaj.MainLeget.Prijemiotpremavozova
{
  
    public partial class PrijemIOtpremaVozova1 : Form
    {
        string Korisnik = Sifarnici.frmLogovanje.user;
        public PrijemIOtpremaVozova1()
        {
            InitializeComponent();
        }

        private void btnPrijemIOtpremaVozova2_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaVozova2.Text,
                () => new frmPrijemIOtpremaVozova2()
            );
        }

        private void btnPrijemIOtpremaVozova4_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaVozova4.Text,
                () => new frmPrijemIOtpremaVozova3()
            );
        }

        private void btnPrijemIOtpremaVozova6_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaVozova6.Text,
                () => new frmPrijemIOtpremaVozova4()
            );
        }

        private void btnPrijemIOtpremaVozova1_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaVozova1.Text,
                () => new frmRadniNalogInterniPregled(Korisnik)
            );
          //  var parent = this.TopLevelControl as NewMain;
           // parent?.ShowChild(new frmRadniNalogInterniPregled(Korisnik);

         
        }

        private void PrijemIOtpremaVozova1_Load(object sender, EventArgs e)
        {

        }

        private void btnPrijemIOtpremaVozova3_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaVozova3.Text,
                () => new frmKontejnerTekuceArhiv()
            );

        
        }

        private void btnPrijemIOtpremaVozova5_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaVozova5.Text,
                () => new frmDodatneUsluge()
            );
        }
    }
}
