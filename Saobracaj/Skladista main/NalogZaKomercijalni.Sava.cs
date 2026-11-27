using Saobracaj.Theme;

namespace Saobracaj.Skladista_main
{
    // Redizajn Sava (docs/SAVA-DESIGN.md): meni Skladišni komercijalni nalozi. Pločice ostaju iste; pozadina je radna površina,
    // a naslov kao u menijima HR i Održavanje. Poziva se na kraju konstruktora.
    public partial class NalogZaKomercijalni
    {
        private void PrimeniSava()
        {
            SavaForme.Plocice(this, true);   // „SKLADIŠNI NALOZI UVOZ“ -> „Skladišni nalozi uvoz“
            btnTransportDirektni.Text = "Skladišni nalozi direktni (3P)";   // 3P ostaje velikim slovima
        }
    }
}
