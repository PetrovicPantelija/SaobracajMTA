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
            parent?.ShowChild(new frmPregledNalogaDrumski(tipoviIn:  _tipVozila, tipoviNotIn: _listNotIn, tipoviNaloga: new List<int> { 0 }, false, "TransportIzvoz"), true);
        }

        private void btnNovi_Click(object sender, EventArgs e)
        {
            int IzborADR = 0;
            int DaLiJeUvoz = 0; // uvoz = 1 je uvoz,  0 je izvoz
            int TipNaloga = 0; // tipNaloga = 1 je  Platforma direktno pun,  tipNaloga  = 2 je Platforma prazan pun

            DialogResult result = Saobracaj.Pomocni.MessageBoxWithCustomButton.Show(
               "Koji tip naloga kreirate?", "A.      3PI", " B.      3PU", // Message text
               "Potvrdite" // Icon
               );

            if (result == DialogResult.Cancel)
            {
                return; // <--- AKO JE KLIKNUTO NA X, OVDE SE SVE PREKIDA
            }

            // Handle the result based on user selection
            if (result == DialogResult.Yes)
            {

                DaLiJeUvoz = 0;

            }
           
            DialogResult result2 = Saobracaj.Pomocni.CustomMessageBox.Show(
           "Da li je u pitanju ADR roba?", // Message text
           "Potvrdite"
           );

            if (result2 == DialogResult.Cancel)
            {
                return;
            }
            // Handle the result based on user selection
            if (result2 == DialogResult.Yes)
            {

                IzborADR = 1;
                //ipnk.UpdStornirajStavku(id); // Promeni ADR
                // Add logic to save changes here
            }

            DialogResult result3 = Saobracaj.Pomocni.MessageBoxWithCustomButton.Show(
              "Nalog za drumski transport je:", "A.      Platforma direktno pun", "B.      Platforma prazan pun", // Message text
              "Potvrdite"
          );

            if (result3 == DialogResult.Cancel)
            {
                return;
            }
            // Handle the result based on user selection
            if (result3 == DialogResult.Yes)
            {

                TipNaloga = 1;
            }
            else
            {

                TipNaloga = 2;
            }
            var parent = this.TopLevelControl as NewMain;
            parent?.ShowChild(new frmDrumski1(tipoviIn: _tipVozila, tipoviNotIn: _listNotIn, "NOVINALOG", null, IzborADR, DaLiJeUvoz, TipNaloga), true);
        }

        private void btnIzmeni_Click(object sender, EventArgs e)
        {
            var parent = this.TopLevelControl as NewMain;
            parent?.ShowChild(new frmPregledNalogaDrumski(tipoviIn:  _tipVozila , tipoviNotIn: _listNotIn, null, false, "Izmeni"), true);
        }

        private void btnTransportUvoz_Click(object sender, EventArgs e)
        {
            var parent = this.TopLevelControl as NewMain;
            parent?.ShowChild(new frmPregledNalogaDrumski(tipoviIn: _tipVozila , tipoviNotIn: _listNotIn, tipoviNaloga: new List<int> { 1 }, false, "TransportUvoz"), true);
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
