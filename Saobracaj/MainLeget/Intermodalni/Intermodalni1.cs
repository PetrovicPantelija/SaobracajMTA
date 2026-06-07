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

namespace Saobracaj.MainLeget.Intermodalni
{
    public partial class Intermodalni1 : Form
    {
        string Korisnik = Sifarnici.frmLogovanje.user;
        public Intermodalni1()
        {
            InitializeComponent();
        }

        private void btnIntermodalni11_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnIntermodalni11.Text,
                () => new IntermodalniKomN());
        }

        private void btnIntermodalni13_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnIntermodalni13.Text,
                () => new VozniPlanovi());
        }
    }
}
