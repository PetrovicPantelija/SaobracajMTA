using Saobracaj.Dokumenta;
using Saobracaj.MainLeget.LegNew;
using Saobracaj.TerminalMap;
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
    public partial class frmPrijemIOtpremaVozova2 : Form
    {
        public frmPrijemIOtpremaVozova2()
        {
            InitializeComponent();
        }

        private void btnPrijemIOtpremaVozova21_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaVozova21.Text,
                () => new TerminalMapFRM()
            );
        }

        private void btnPrijemIOtpremaVozova22_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaVozova22.Text,
                () => new frmPopisKonterjnera()
            );

          
        }

        private void btnPrijemIOtpremaVozova23_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaVozova23.Text,
                () => new frmCIRPregled()
            );
   
        }
    }
}
