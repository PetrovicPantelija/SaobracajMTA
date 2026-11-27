using Saobracaj.Theme;

namespace Saobracaj.Skladista_main
{
    // Redizajn Sava (docs/SAVA-DESIGN.md): meni Carinsko skladište. Pločice ostaju iste; pozadina je radna površina,
    // a naslov kao u menijima HR i Održavanje. Poziva se na kraju konstruktora.
    public partial class CarinskoSkladiste
    {
        private void PrimeniSava()
        {
            SavaForme.Plocice(this, false);
        }
    }
}
