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
    public partial class NalogZaDrumski : Form
    {
        public NalogZaDrumski()
        {
            InitializeComponent();
        }

        private void btnTransportIzvoz_Click(object sender, EventArgs e)
        {
            var parent = this.TopLevelControl as NewMain;
            parent?.ShowChild(new frmPregledNalogaDrumski(tipoviIn: new List<int> { 2 }, tipoviNotIn: null, tipoviNaloga: new List<int> { 1 }, false), true);
        }

        private void btnNovi_Click(object sender, EventArgs e)
        {
            var parent = this.TopLevelControl as NewMain;
            parent?.ShowChild(new frmDrumski(tipoviIn: new List<int> { 2 }, tipoviNotIn: null, "NOVINALOG", null), true);
        }

        private void btnIzmeni_Click(object sender, EventArgs e)
        {
            var parent = this.TopLevelControl as NewMain;
            parent?.ShowChild(new frmPregledNalogaDrumski(tipoviIn: new List<int> { 2 }, tipoviNotIn: null, null, false), true);
        }

        private void btnTransportUvoz_Click(object sender, EventArgs e)
        {
            var parent = this.TopLevelControl as NewMain;
            parent?.ShowChild(new frmPregledNalogaDrumski(tipoviIn: new List<int> { 2 }, tipoviNotIn: null, tipoviNaloga: new List<int> { 0 }, false), true);
        }

        private void btnTransportDirektni_Click(object sender, EventArgs e)
        {
            var parent = this.TopLevelControl as NewMain;
            parent?.ShowChild(new frmPregledNalogaDrumski(tipoviIn: new List<int> { 2 }, tipoviNotIn: null, tipoviNaloga: new List<int> { 2, 3, 4, 5 }, false), true);
        }

        private void btnOtkazi_Click(object sender, EventArgs e)
        {
            var parent = this.TopLevelControl as NewMain;
            parent?.ShowChild(new frmPregledNalogaDrumski(tipoviIn: new List<int> { 2 }, tipoviNotIn: null, null, true), true);
        }

        private void btnNovi_Click(object sender, EventArgs e)
        {

        }

        private void btnIzmeni_Click(object sender, EventArgs e)
        {

        }

        private void btnOtkazi_Click(object sender, EventArgs e)
        {

        }

        private void btnTransportUvoz_Click(object sender, EventArgs e)
        {

        }

        private void btnTransportDirektni_Click(object sender, EventArgs e)
        {

        }
    }
}
