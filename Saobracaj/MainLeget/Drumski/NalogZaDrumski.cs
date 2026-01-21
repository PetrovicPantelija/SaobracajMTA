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
        private List<int> _tipVozila = null;
        private List<int> _listNotIn = null;

        public NalogZaDrumski()
        {
            InitializeComponent();
        }

        public NalogZaDrumski( List<int> tipoviIn, List<int> tipoviNotIn)
        {
            InitializeComponent();
            _tipVozila = tipoviIn;
            _listNotIn = tipoviNotIn;
        }


        private void btnTransportIzvoz_Click(object sender, EventArgs e)
        {
            var parent = this.TopLevelControl as NewMain;
            parent?.ShowChild(new frmPregledNalogaDrumski(tipoviIn:  _tipVozila, tipoviNotIn: _listNotIn, tipoviNaloga: new List<int> { 1 }, false, "TransportIzvoz"), true);
        }

        private void btnNovi_Click(object sender, EventArgs e)
        {
            var parent = this.TopLevelControl as NewMain;
            parent?.ShowChild(new frmDrumski(tipoviIn: _tipVozila, tipoviNotIn: _listNotIn, "NOVINALOG", null), true);
        }

        private void btnIzmeni_Click(object sender, EventArgs e)
        {
            var parent = this.TopLevelControl as NewMain;
            parent?.ShowChild(new frmPregledNalogaDrumski(tipoviIn:  _tipVozila , tipoviNotIn: _listNotIn, null, false, "Izmeni"), true);
        }

        private void btnTransportUvoz_Click(object sender, EventArgs e)
        {
            var parent = this.TopLevelControl as NewMain;
            parent?.ShowChild(new frmPregledNalogaDrumski(tipoviIn: _tipVozila , tipoviNotIn: _listNotIn, tipoviNaloga: new List<int> { 0 }, false, "TransportUvoz"), true);
        }

        private void btnTransportDirektni_Click(object sender, EventArgs e)
        {
            var parent = this.TopLevelControl as NewMain;
            parent?.ShowChild(new frmPregledNalogaDrumski(tipoviIn: _tipVozila , tipoviNotIn: _listNotIn, tipoviNaloga: new List<int> { 2, 3, 4, 5 }, false, "TransportDirektni"), true);
        }

        private void btnOtkazi_Click(object sender, EventArgs e)
        {
            var parent = this.TopLevelControl as NewMain;
            parent?.ShowChild(new frmPregledNalogaDrumski(tipoviIn: _tipVozila , tipoviNotIn: _listNotIn, null, true, "Otkazi"), true);
        }

        
    }
}
