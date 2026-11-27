using Saobracaj.Skladista;
using Saobracaj.Skladista_main.Dokumenta;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main
{
    public partial class CarinskoSkladiste : Form
    {
        public CarinskoSkladiste()
        {
            InitializeComponent();
            PrimeniSava();   // redizajn Sava (CarinskoSkladiste.Sava.cs)
            InitCarinskaKnjiga();   // RAZ-16 (CarinskoSkladiste.Knjiga.cs)
        }

        private void btnPrijemRobe_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuBezPrava(() => new CarinskoSkladistePregled("Prijem"));
        }

        private void btnOtpremaRobe_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuBezPrava(() => new CarinskoSkladistePregled("Otprema"));
        }

        private void btnPretovarRobe_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuBezPrava(() => new CarinskoSkladistePregled("Pretovar"));
        }

        private void btnMagacinskaKnjiga_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            // magacinska knjiga carinskog skladišta je evidencija iz frmCarinskaKnjiga; dosadašnja tabela je na dugmetu „Kartica prometa“
            main.OtvoriFormuBezPrava(() => new frmCarinskaKnjiga());
        }

        private void btnPregledLagera_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuBezPrava(() => new PregledLagera("Magacinski broj"));
        }

        private void btnPregledKartica_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuBezPrava(() => new PregledLagera("Partner"));
        }
    }
}
