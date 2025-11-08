using Saobracaj.MainLeget.LegNew;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.MainLeget.Drumski
{
    public partial class Cerade : Form
    {
        public Cerade()
        {
            InitializeComponent();
        }

        private void btnNalogDrumski_Click(object sender, EventArgs e)
        {
            var parent = this.TopLevelControl as NewMain;
            parent?.ShowChild(new NalogZaDrumski(), true);
        }
    }
}
