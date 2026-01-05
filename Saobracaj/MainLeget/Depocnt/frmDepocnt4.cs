using Saobracaj.Dokumenta;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Testiranje.Dokumeta;

namespace Saobracaj.MainLeget.Depocnt
{
    public partial class frmDepocnt4 : Form
    {
        public frmDepocnt4()
        {
            InitializeComponent();
        }

        private void btnDepocnt42_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnDepocnt42.Text,
                () => new frmCIRPregled()
            );
        }

        private void btnDepocnt41_Click(object sender, EventArgs e)
        {
            MessageBox.Show("U izradi ne zna se šta je ovo");
        }
    }
}
