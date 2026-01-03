using Saobracaj.Izvoz;
using Saobracaj.RadniNalozi;
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
using TrackModal.Dokumeta;

namespace Saobracaj.MainLeget.Prijemiotpremavozova
{
    public partial class frmPrijemIOtpremaVozova4 : Form
    {
        public string Korisnik = Sifarnici.frmLogovanje.user;
        public frmPrijemIOtpremaVozova4()
        {
            InitializeComponent();
        }

        private void btnPrijemIOtpremaVozova41_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaVozova41.Text,
                () => new frmDopunaPlanaPraznimIzvoz()
            );

         
        }

        private void btnPrijemIOtpremaVozova42_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaVozova42.Text,
                () => new RN1PrijemVoza()
            );
            
        }

        private void btnPrijemIOtpremaVozova43_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaVozova43.Text,
                () => new RN2OtpremaVoza()
            );
        }

        private void btnPrijemIOtpremaVozova44_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaVozova44.Text,
                () => new frmVoz(Korisnik)
            );

         
        }

        private void btnPrijemIOtpremaVozova45_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaVozova45.Text,
                () => new frmPregledVozova(Korisnik)
            );
           
        }

        private void btnPrijemIOtpremaVozova46_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaVozova46.Text,
                () => new frmPrijemVozomPregled(Korisnik)
            );
            
        }

        private void btnPrijemIOtpremaVozova47_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaVozova46.Text,
                () => new frmPregledOtpreme(Korisnik)
            );
            
        }
    }
}
