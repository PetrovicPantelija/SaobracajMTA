using Saobracaj.Dokumenta;
using Saobracaj.RadniNalozi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.MainLeget.Depocnt
{
    public partial class frmDepocnt3 : Form
    {
        public frmDepocnt3()
        {
            InitializeComponent();
        }

        private void btnDepocnt34_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnDepocnt34.Text,
                () => new frmPopisKonterjnera()
            );

        }

        private void btnDepocnt31_Click(object sender, EventArgs e)
        {
            MessageBox.Show("U izradi ne zna se šta je ovo");
        }

        private void btnDepocnt32_Click(object sender, EventArgs e)
        {
            MessageBox.Show("U izradi ne zna se šta je ovo");
        }

        private void btnDepocnt33_Click(object sender, EventArgs e)
        {
            MessageBox.Show("U izradi ne zna se šta je ovo");
        }
    }
}
