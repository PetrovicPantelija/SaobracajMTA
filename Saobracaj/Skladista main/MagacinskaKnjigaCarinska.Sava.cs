using Saobracaj.Theme;

namespace Saobracaj.Skladista_main
{
    // Redizajn Sava (docs/SAVA-DESIGN.md): magacinska knjiga carinskog skladišta, tabela u stilu Sava.
    // Poziva se na kraju konstruktora.
    public partial class MagacinskaKnjigaCarinska
    {
        private void PrimeniSava()
        {
            ThemeHelper.Apply(this);
        }
    }
}
