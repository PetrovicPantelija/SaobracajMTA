using Saobracaj.MainLeget.Drumski;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Saobracaj.MainLeget.LegNew
{
    public partial class Drumski1 : Form
    {

        public Drumski1()
        {
            InitializeComponent();

        }

        private void SfButton1_Click(object sender, EventArgs e) { }

        private void pictureBox2_Click(object sender, EventArgs e) => Close();

        private void sfButton5_Click(object sender, EventArgs e)
        {

        }

        private void btnForm1_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnForm1.Text,
                () => new Cerade()
            );
        }

        private void sfButton7_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnForm1.Text,
                () => new Platforma()
            );
        }

        private void sfButton6_Click(object sender, EventArgs e)
        {

        }
    }
}
