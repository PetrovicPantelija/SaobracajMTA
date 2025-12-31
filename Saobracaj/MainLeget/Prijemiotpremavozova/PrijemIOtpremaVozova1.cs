using Saobracaj.MainLeget.LegNew;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.MainLeget.Prijemiotpremavozova
{
    public partial class PrijemIOtpremaVozova1 : Form
    {
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

        }
    }
}
