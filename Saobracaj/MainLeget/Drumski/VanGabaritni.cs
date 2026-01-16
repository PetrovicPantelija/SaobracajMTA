using Saobracaj.Drumski;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.MainLeget.Drumski
{
    public partial class VanGabaritni : Form
    {
        public VanGabaritni()
        {
            InitializeComponent();
        }

        private void btnStatusi_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
               btnStatusi.Text,
               () => new frmStatus(tipoviIn: null, tipoviNotIn: new List<int> { 1, 2 })
           );
        }

        private void btnProvera_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnProvera.Text,
                () => new frmRaspolozivostVozila(tipoviIn: null, tipoviNotIn: new List<int> { 1, 2 })
            );
        }

        private void btnFormiranjeNaloga_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
             btnFormiranjeNaloga.Text,
             () => new PakovanjeKamiona1(tipoviIn: null, tipoviNotIn: new List<int> { 1, 2 })
         );
        }

        private void btnNalogDrumski_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;
                
            main.OtvoriFormuSaPravom(
                btnNalogDrumski.Text,
                () => new NalogZaDrumski(tipoviIn: null, tipoviNotIn: new List<int> { 1,2})
            );
        }

        private void btnPonude_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPonude.Text,
                () => new Sifarnici.frmPartnerji(4) //4 - mainId menja drumski
            );
        }
    }
}
