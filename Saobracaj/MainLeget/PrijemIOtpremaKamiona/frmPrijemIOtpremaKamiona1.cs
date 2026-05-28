using Saobracaj.Dokumenta;
using Saobracaj.Izvoz;
using Saobracaj.MainLeget.Intermodalni;
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

namespace Saobracaj.MainLeget.PrijemIOtpremaKamiona
{
    public partial class frmPrijemIOtpremaKamiona1 : Form
    {
        public string Korisnik = Sifarnici.frmLogovanje.user;
        public frmPrijemIOtpremaKamiona1()
        {
            InitializeComponent();
        }

        private void btnPrijemIOtpremaKamiona3_Click(object sender, EventArgs e)
        {
           
        }

        private void btnPrijemIOtpremaKamiona1_Click(object sender, EventArgs e)
        {
           
        }

        private void btnPrijemIOtpremaKamiona2_Click(object sender, EventArgs e)
        {
           

            
        }

        private void btnPrijemIOtpremaKamiona4_Click(object sender, EventArgs e)
        {
          
        }

        private void btnPrijemIOtpremaKamiona5_Click(object sender, EventArgs e)
        {
          
        }

        private void btnPrijemIOtpremaKamiona6_Click(object sender, EventArgs e)
        {
          
        }

        private void btnPrijemIOtpremaKamiona7_Click(object sender, EventArgs e)
        {
           
        }

        private void btnPrijemIOtpremaKamiona11_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;
            /*
            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaKamiona11.Text,
                () => new Pregledac()
            );
            */
           
            Pregledac pr = new Pregledac();
            pr.Show();
          
        }

        private void btnPrijemIOtpremaKamiona8_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaKamiona8.Text,
                () => new frmPrijemIOtpremaPlatforma());
          //  frmPlatforma pl = new frmPlatforma();
          //  pl.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnPrijemIOtpremaKamiona9_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrijemIOtpremaKamiona9.Text,
                () => new frmPrijemIOtpremaCerada()
            );
        }
    }
}
