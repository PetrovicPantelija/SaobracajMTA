using Saobracaj.Theme;

namespace Saobracaj.TerminalMap
{
    // Redizajn Sava (docs/SAVA-DESIGN.md): interni prenos kontejnera sa polja. Jedno plavo dugme (Formiraj interni prenos),
    // ostala dugmad bela (bila su zelena), oznake običnim slovima, tabela u stilu Sava (PodesiDatagridView).
    // Poziva se na kraju konstruktora (posle ChangeTextBox).
    public partial class frmRN12InterniPrenos
    {
        private void PrimeniSava()
        {
            button2.Tag = "primary";
            ThemeHelper.Apply(this);
            label8.Text = "Na polje";
            txtNAPozicijaskladista.Text = "Na poziciju skladišta";
            ThemeHelper.OznakeIznadPolja(this);
            SavaForme.DugmadPoTekstu(this);
        }
    }
}
