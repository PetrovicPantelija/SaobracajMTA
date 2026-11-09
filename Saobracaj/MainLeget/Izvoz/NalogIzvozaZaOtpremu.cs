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
    public partial class NalogIzvozaZaOtpremu : Form
    {
        private int izabranaOpcija = 0;
        public int IzabranaOpcija { get { return izabranaOpcija; } }

        public NalogIzvozaZaOtpremu()
        {
            InitializeComponent();
        }

        private void btn13_Click(object sender, EventArgs e)
        {
            izabranaOpcija = 1;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            izabranaOpcija = 2;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            izabranaOpcija = 3;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            izabranaOpcija = 4;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
