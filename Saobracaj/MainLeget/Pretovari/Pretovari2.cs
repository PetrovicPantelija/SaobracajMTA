using Saobracaj.Izvoz;
using Saobracaj.RadniNalozi;
using Saobracaj.Uvoz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.MainLeget.Pretovari
{
    public partial class Pretovari2 : Form
    {
        public Pretovari2()
        {
            InitializeComponent();
        }

        private void btnPretovari21_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPretovari21.Text,
                () => new PlaniraniPretovar()
            );
        }

        private void btnPretovari22_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPretovari22.Text,
                () => new PrijemnicaPregled()
            );

           
        }

        private void btnPretovari23_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPretovari23.Text,
                () => new OtpremnicaPregled()
            );
      
        }

        private void btnPretovari24_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPretovari24.Text,
                () => new RN9PrijemCirade()
            );
        }

        private void btnPretovari25_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPretovari25.Text,
                () => new RN8OtpremaCirade()
            );
        }
    }
}
