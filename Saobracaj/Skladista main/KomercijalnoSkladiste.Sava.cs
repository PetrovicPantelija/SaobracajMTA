using Saobracaj.Theme;

namespace Saobracaj.Skladista_main
{
    // Redizajn Sava (docs/SAVA-DESIGN.md): meni Komercijalno skladište. Pločice ostaju iste; pozadina je radna površina,
    // a naslov kao u menijima HR i Održavanje. Poziva se na kraju konstruktora.
    public partial class KomercijalnoSkladiste
    {
        private void PrimeniSava()
        {
            SavaForme.Plocice(this, false);
        }
    }
}
