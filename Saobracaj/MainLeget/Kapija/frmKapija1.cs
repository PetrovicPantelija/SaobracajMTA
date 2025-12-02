using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.MainLeget.Kapija
{
    public partial class frmKapija1 : Form
    {
        public frmKapija1()
        {
            InitializeComponent();
        }

        private void sfButton1_Click(object sender, EventArgs e)
        {
            var parent = this.TopLevelControl as NewMain;
            parent?.ShowChild(new Saobracaj.Kapija.frmKamionDetalji(), true);
        }

        private void sfButton6_Click(object sender, EventArgs e)
        {
            var parent = this.TopLevelControl as NewMain;
            parent?.ShowChild(new Saobracaj.Kapija.frmPregledKamiona(), true);
        }
    }
}
