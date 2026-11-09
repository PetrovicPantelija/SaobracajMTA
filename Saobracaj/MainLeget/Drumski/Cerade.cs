using Saobracaj.Dokumenta;
using Saobracaj.Drumski;
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

namespace Saobracaj.MainLeget.Drumski
{
    public partial class Cerade : Form
    {
        public Cerade()
        {
            InitializeComponent();
        }

        private void btnNalogDrumski_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnNalogDrumski.Text,
                () => new NalogZaDrumski(tipoviIn: new List<int> { 2 }, tipoviNotIn: null)
            );

        }

        private void btnStatusi_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnStatusi.Text,
                () => new  frmStatus(tipoviIn: new List<int> { 2 }, tipoviNotIn: null)
            );
        }

        private void btnProvera_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            //main.OtvoriFormuSaPravom(
            //    btnProvera.Text,
            //    () => new frmRaspolozivostVozila(tipoviIn: new List<int> { 2 }, tipoviNotIn: null)
            //);
            main.OtvoriFormuSaPravom(
               btnProvera.Text,
               () => new frmPodesavanjeRaspolozivosti(tipoviIn: new List<int> { 2 }, tipoviNotIn: null)
          );

        }

        private void btnFormiranjeNaloga_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnFormiranjeNaloga.Text,
                () =>new PakovanjeKamionaCerade(tipoviIn: new List<int> { 2 }, tipoviNotIn: null)
            );
        }

        private void btnNalogFakturisanje_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPonude.Text,
                () => new frmNalogZaFakturisanje(tipoviIn: new List<int> { 2 }, tipoviNotIn: null)
            );
        }

        private void btnPonude_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            //main.OtvoriFormuSaPravom(
            //    btnPonude.Text,
            //    () => new Sifarnici.frmPartnerji(4) //4 - mainId menja drumski
            //);

            main.OtvoriFormuSaPravom(
               btnPonude.Text,
               () => new frmAutomobiliDrumski(tipoviIn: new List<int> { 2 }, tipoviNotIn: null)
           );
        }
    }
}
