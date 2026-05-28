using Saobracaj.MainLeget.Prijemiotpremavozova;
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
    public partial class PretovariMain : Form
    {
        public PretovariMain()
        {
            InitializeComponent();
        }

        private void btnPretovarSkladista_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPretovarSkladista.Text,
                () => new frmPretovariSkladista()
            );
        }

        private void btnPretovarPrijem_Click(object sender, EventArgs e)
        {
  var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPretovarPrijem.Text,
                () => new PretovariPrijem()
            );
        }
    }
}
