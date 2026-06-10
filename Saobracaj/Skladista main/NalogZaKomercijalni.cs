using Saobracaj.Skladista;
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
    public partial class NalogZaKomercijalni : Form
    {
        public NalogZaKomercijalni()
        {
            InitializeComponent();
        }

        private void btnTransportUvoz_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuBezPrava(
                () => new PregledKomercijalnihNaloga("Uvoz")
            );
        }

        private void btnTransportIzvoz_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuBezPrava(
                () => new PregledKomercijalnihNaloga("Izvoz")
            );
        }

        private void btnTransportDirektni_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuBezPrava(
                () => new PregledKomercijalnihNaloga("Direktni")
            );
        }

        private void btnNovi_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuBezPrava(
                () => new TipSkladista("Direktni")
            );
        }
    }
}
