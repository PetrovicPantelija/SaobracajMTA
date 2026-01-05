using Saobracaj.Izvoz;
using Saobracaj.RadniNalozi;
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
    public partial class frmPrijemIOtpremaVozova3 : Form
    {
        public frmPrijemIOtpremaVozova3()
        {
            InitializeComponent();
        }

        private void btnPrijemIOtpremaVozova31_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaVozova31.Text,
                () => new frmAnalizaRadnihNaloga()
            );

       
        }

        private void btnPrijemIOtpremaVozova32_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaVozova32.Text,
                () => new DogovoreniPosloviIzvoza()
            );

            
        }
    }
}
