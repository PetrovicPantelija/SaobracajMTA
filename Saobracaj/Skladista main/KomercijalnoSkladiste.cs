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
    public partial class KomercijalnoSkladiste : Form
    {
        public KomercijalnoSkladiste()
        {
            InitializeComponent();
        }

        private void btnPrijemRobe_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuBezPrava(() => new KomercijalnoSkladistePregled("Prijem"));
        }
    }
}
