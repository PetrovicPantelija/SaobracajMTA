using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Skladista
{
    public partial class MainRN : Form
    {
        string Korisnik = Saobracaj.Sifarnici.frmLogovanje.user;

        public MainRN(string korisnik)
        {
            InitializeComponent();
        }

        private void btnCarinskoSkladiste_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnCarinskoSkladiste.Text,
                () => new TipRN("Carinsko",Korisnik)
            );
        }

        private void btnKomerijalnoSkladiste_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnKomerijalnoSkladiste.Text,
                () => new TipRN("Komercijalno",Korisnik)
            );
        }
    }
}
