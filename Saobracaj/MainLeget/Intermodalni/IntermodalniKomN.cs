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

namespace Saobracaj.MainLeget.Intermodalni
{
    public partial class IntermodalniKomN : Form
    {
        string Korisnik = Sifarnici.frmLogovanje.user;
        public IntermodalniKomN()
        {
            InitializeComponent();
        }

        private void btmKomercijalni6_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btmKomercijalni6.Text,
                () => new frmRadniNalogInterniPregled(Korisnik)
            );
        }

        private void btmKomercijalni4_Click(object sender, EventArgs e)
        {
            frmRadniNalogInterniPregled frm = new frmRadniNalogInterniPregled(Korisnik);
            frm.Show();
        }
    }
}
