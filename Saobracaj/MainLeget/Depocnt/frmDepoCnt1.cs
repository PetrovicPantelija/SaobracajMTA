using Saobracaj.Izvoz;
using Saobracaj.MainLeget.LegNew;
using Saobracaj.TerminalMap;
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

namespace Saobracaj.MainLeget.Depocnt
{
    public partial class frmDepoCnt1 : Form
    {
        public frmDepoCnt1()
        {
            InitializeComponent();
        }

        private void btnDepoCNT1_Click(object sender, EventArgs e)
        {

            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnDepoCNT1.Text,
                () => new TerminalMapFRM()
            );
        }

        private void btnDepoCNT2_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnDepoCNT2.Text,
                () => new frmKontejnerTekuceArhiv()
            );
        }

        private void btnDepoCNT3_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnDepoCNT3.Text,
                () => new frmDepocnt3()
            );
        }

        private void btnDepoCNT4_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnDepoCNT4.Text,
                () => new frmDodatneUsluge()
            );
      
        }

        private void btnDepoCNT5_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnDepoCNT5.Text,
                () => new frmCIRPregled()
            );
        }

        private void btnDepoCNT6_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnDepoCNT6.Text,
                () => new DogovoreniPosloviIzvoza()
            );
        }
    }
}
