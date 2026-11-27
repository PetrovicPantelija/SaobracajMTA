using Saobracaj.Theme;

namespace Saobracaj.Skladista_main
{
    // Redizajn Sava (docs/SAVA-DESIGN.md): pregled viljuškarista. Traka od 52 px sa dugmadima u jednom redu,
    // tabele u stilu Sava. Poziva se na kraju konstruktora.
    public partial class ViljuskaristiPregled
    {
        private void PrimeniSava()
        {
            ThemeHelper.Apply(this);
            button2.Text = SavaForme.Natpis(button2.Text);   // Refresh -> Osveži
            button3.Text = "Potvrdi skladištar";
            button1.Text = "Potvrdi skladištar usluge";
            SavaForme.TrakaURed(panel6, panel6, button2, button3, button1);
        }
    }
}
