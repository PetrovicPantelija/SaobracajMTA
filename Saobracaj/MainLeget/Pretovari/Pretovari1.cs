using Saobracaj.Izvoz;
using Saobracaj.MainLeget.Prijemiotpremavozova;
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

namespace Saobracaj.MainLeget.Pretovari
{
    public partial class Pretovari1 : Form
    {
        public Pretovari1()
        {
            InitializeComponent();
        }

        private void btnPretovari1_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPretovari1.Text,
                () => new Pretovari2()
            );
        }

        private void btnPretovari2_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPretovari2.Text,
                () => new frmKontejnerTekuceArhiv()
            );
        }

        private void btnPretovari3_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPretovari3.Text,
                () => new VaganjePregled()
            );
     
        }

        private void btnPretovari4_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPretovari4.Text,
                () => new frmDodatneUsluge()
            );
           
        }
    }
}
