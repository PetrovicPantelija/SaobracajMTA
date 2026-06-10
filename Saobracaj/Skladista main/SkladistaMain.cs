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
    public partial class SkladistaMain : Form
    {
        public SkladistaMain()
        {
            InitializeComponent();
        }

        private void btnRadniNalozi_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                "Radni nalozi",
                () => new NalogZaKomercijalni()
            );
        }

        private void btnCarinskoSkladiste_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnCarinskoSkladiste.Text,
                () => new CarinskoSkladiste()
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
    }
}
