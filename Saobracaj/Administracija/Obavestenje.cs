using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Administracija
{
    public partial class Obavestenje : Syncfusion.Windows.Forms.Office2010Form
    {
        string obavestenje;
        private int progressBarMaxValue;
        public Obavestenje()
        {
            InitializeComponent();
        }
        public Obavestenje(string Obavestenje)
        {
            InitializeComponent();
            this.ControlBox = false;
            obavestenje = Obavestenje;
        }
        private void Obavestenje_Load(object sender, EventArgs e)
        {
            textBox1.Text = obavestenje;
            textBox1.TextAlign = HorizontalAlignment.Center;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = progressBarMaxValue;
        }
        public void SetProgressBarMaximum(int max)
        {
            progressBarMaxValue = max;
            progressBar1.Maximum = max;
        }

        public void IncrementProgressBar()
        {
            progressBar1.Value++;
            Application.DoEvents();
        }
    }
}
