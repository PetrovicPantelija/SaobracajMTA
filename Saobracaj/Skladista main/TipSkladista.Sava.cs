using Saobracaj.Theme;

namespace Saobracaj.Skladista_main
{
    // Redizajn Sava (docs/SAVA-DESIGN.md): izbor tipa skladišta (Carinsko, Komercijalno). Pločice ostaju iste; pozadina je radna površina,
    // a naslov kao u menijima HR i Održavanje. Poziva se na kraju konstruktora.
    public partial class TipSkladista
    {
        private void PrimeniSava()
        {
            SavaForme.Plocice(this, false);
        }
    }
}
