using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.MainLeget.Izvoz
{
    public partial class LogistikaIzvoza0 : Form
    {
        public LogistikaIzvoza0()
        {
            InitializeComponent();
        }

        private void sfButton1_Click(object sender, EventArgs e)
        {
            //var main = this.TopLevelControl as NewMain;
            //if (main == null) return;

            //main.OtvoriFormuSaPravom(
            //    btnLogistikaIzvozaPM1.Text,
            //    () => new LogistikaIzvoza2() 

            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnLogistikaIzvoza0Kreiraj.Text,
                () => new Saobracaj.Izvoz.frmProdajniNalogIzvoz()
            );
        }
    }
}
