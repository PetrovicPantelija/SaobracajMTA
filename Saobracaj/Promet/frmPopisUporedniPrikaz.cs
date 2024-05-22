using System;
using System.Windows.Forms;

namespace TrackModal.Promet
{
    public partial class frmPopisUporedniPrikaz : Syncfusion.Windows.Forms.Office2010Form
    {
        public frmPopisUporedniPrikaz()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InsertPopis ins = new InsertPopis();
            ins.InsertPopisStavkeUporedni(Convert.ToInt32(txtSifra.Text));
        }
    }
}
