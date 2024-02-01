using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Uvoz
{
    public partial class frmFormiranjeRobnihDokumenata : Form
    {
        public frmFormiranjeRobnihDokumenata()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmPrijemVozaIzPlana rd1 = new frmPrijemVozaIzPlana();
            rd1.Show();
        }
    }
}
