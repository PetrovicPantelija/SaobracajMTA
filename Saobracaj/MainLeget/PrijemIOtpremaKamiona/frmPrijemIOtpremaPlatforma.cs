using Saobracaj.Izvoz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.MainLeget.PrijemIOtpremaKamiona
{
    public partial class frmPrijemIOtpremaPlatforma : Form
    {
        public frmPrijemIOtpremaPlatforma()
        {
            InitializeComponent();
        }

        private void btnPrijemIOtprema1_Click(object sender, EventArgs e)
        {
            frmPlatforma pl = new frmPlatforma();
            pl.Show();
        }

        private void btnPrijemIOtprema3_Click(object sender, EventArgs e)
        {
            VaganjePregled vaganjePregled = new VaganjePregled();
            vaganjePregled.Show();
        }
    }
}
