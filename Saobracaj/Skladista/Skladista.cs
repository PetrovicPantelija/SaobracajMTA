using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Skladista
{
    public partial class Skladista : Form
    {
        public Skladista()
        {
            InitializeComponent();
        }

        private void btnCarinskoSkladiste_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnCarinskoSkladiste.Text,
                () => new CarinskoSkladiste("Carinsko")
            );
        }

        private void btnRadniNalozi_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnRadniNalozi.Text,
                () => new Komercijalninalozi()
            );
        }

        private void btnKomerijalnoSkladiste_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnKomerijalnoSkladiste.Text,
                () => new KomercijalnoSkladiste()
            );
        }

        private void btnFakturisanje_Click(object sender, EventArgs e)
        {

        }
    }
}
