using Saobracaj.MainLeget;
using System;
using System.Windows.Forms;

namespace Saobracaj.Administracija
{
    // Modul Administracija u NewMain (u stilu Podesavanje1): kartice Korisnici i Prava
    public partial class Administracija1 : Form
    {
        string Korisnik = Sifarnici.frmLogovanje.user;

        public Administracija1()
        {
            InitializeComponent();
        }

        private void btnKorisnici_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnKorisnici.Text,
                () => new frmKorisnici());
        }

        private void btnPrava_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnPrava.Text,
                () => new AdministracijaPravoPristupa(Korisnik));
        }
    }
}
