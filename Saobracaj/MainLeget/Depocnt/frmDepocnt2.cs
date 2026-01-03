using Saobracaj.Izvoz;
using Saobracaj.RadniNalozi;
using Saobracaj.TerminalMap;
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
    public partial class frmDepocnt2 : Form
    {
        public frmDepocnt2()
        {
            InitializeComponent();
        }

  

        

        private void btnDepocnt21_Click(object sender, EventArgs e)
        {
            MessageBox.Show("U izradi ne zan se šta je ovo");
        }

        private void btnDepocnt22_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnDepocnt22.Text,
                () => new TerminalMapFRM()
            );
        }

        private void btnDepocnt25_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnDepocnt25.Text,
                () => new RN12MedjuskladisniKontejnera()
            );
            
        }

        private void btnDepocnt23_Click(object sender, EventArgs e)
        {
            MessageBox.Show("U izradi ne zan se šta je ovo");
        }

        private void btnDepocnt24_Click(object sender, EventArgs e)
        {
            MessageBox.Show("U izradi ne zan se šta je ovo");
        }
    }
}
