using Saobracaj.Dokumenta;
using Saobracaj.Izvoz;
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
    public partial class frmPrijemIOtpremaKamiona2 : Form
    {
        public frmPrijemIOtpremaKamiona2()
        {
            InitializeComponent();
        }

        private void btnPrijemIOtpremaKamiona1_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaKamiona1.Text,
                () => new DogovoreniPosloviIzvoza()
            );
        }

        private void btnPrijemIOtpremaKamiona2_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaKamiona2.Text,
                () => new frmIzvozTerminalPovezi()
            );
           
        }
    }
}
