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

namespace Saobracaj.MainLeget.PrijemIOtpremaKamiona
{
    public partial class frmPrijemIOtpremaKamiona4 : Form
    {
        public frmPrijemIOtpremaKamiona4()
        {
            InitializeComponent();
        }

        private void btnPrijemIOtpremaKamiona1_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaKamiona1.Text,
                () => new RN5PrijemPlatforme2()
            );
        }

        private void sfButton2_Click(object sender, EventArgs e)
        {
         
                 var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaKamiona1.Text,
                () => new RN4PrijemPlatforme()
            );
        }

        private void btnPrijemIOtpremaKamiona2_Click(object sender, EventArgs e)
        {

            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaKamiona2.Text,
                () => new RN7OtpremaPlatforme2()
            );
            
        }

        private void btnPrijemIOtpremaKamiona4_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaKamiona4.Text,
                () => new RN6OtpremaPlatforme()
            );
        }
    }
}
