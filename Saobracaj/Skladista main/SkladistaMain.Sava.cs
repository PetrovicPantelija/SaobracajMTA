using Saobracaj.Theme;

namespace Saobracaj.Skladista_main
{
    // Redizajn Sava (docs/SAVA-DESIGN.md): meni Skladišta. Pločice ostaju iste; pozadina je radna površina,
    // a naslov kao u menijima HR i Održavanje. Poziva se na kraju konstruktora.
    public partial class SkladistaMain
    {
        private void PrimeniSava()
        {
            SavaForme.Plocice(this, false);   // tekst pločice je naziv kartice za prava (OtvoriFormuSaPravom)
            btnPozicija.Text = "Definisanje / predefinisanje pozicija";   // prelom kod kose crte, ne usred reči (pločica nema handler)
        }
    }
}
