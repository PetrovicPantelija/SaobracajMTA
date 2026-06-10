using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main
{
    public partial class TipSkladista : Form
    {
        int IDInterni=0;
        string Ulaz;
        public TipSkladista()
        {
            InitializeComponent();
        }
        public TipSkladista(string direktni)
        {
            InitializeComponent();
        }
        public TipSkladista(int idInterni,string ulaz)
        {
            InitializeComponent();
            IDInterni = idInterni;
            Ulaz = ulaz;
        }
        string Vrsta;
        private void btnCarinskoSkladiste_Click(object sender, EventArgs e)
        {
            Vrsta = "Carinsko";
            if (IDInterni != 0)
            {
                var main = this.TopLevelControl as NewMain;
                if (main == null) return;

                main.OtvoriFormuBezPrava(
                    () => new RadniNaloziSkladista(IDInterni, Ulaz,Vrsta)
                );
            }
            else
            {
                var main = this.TopLevelControl as NewMain;
                if (main == null) return;

                main.OtvoriFormuBezPrava(
                    () => new RadniNaloziSkladista(Vrsta)
                );
            }
        }

        private void btnKomercijalnoSkladiste_Click(object sender, EventArgs e)
        {
            Vrsta = "Komercijalno";
            if (IDInterni != 0)
            {
                var main = this.TopLevelControl as NewMain;
                if (main == null) return;

                main.OtvoriFormuBezPrava(
                    () => new RadniNaloziSkladista(IDInterni, Ulaz, Vrsta)
                );
            }
            else
            {
                var main = this.TopLevelControl as NewMain;
                if (main == null) return;

                main.OtvoriFormuBezPrava(
                    () => new RadniNaloziSkladista(Vrsta)
                );
            }
        }
    }
}
