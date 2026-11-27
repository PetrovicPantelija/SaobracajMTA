using Saobracaj.Theme;

namespace Saobracaj.RNI
{
    // Redizajn Sava (docs/SAVA-DESIGN.md): scenario SCI. Forma sa mnogo polja, zato 9 pt; grupe polja su bele kartice,
    // oznake iznad polja, tabele u stilu Sava (PodesiDatagridView). Poziva se na kraju konstruktora (posle ChangeTextBox).
    public partial class frmScenarioSCI
    {
        private void PrimeniSava()
        {
            ThemeHelper.ApplyZbijeno(this);
            SavaForme.OznakeAutoSize(this);   // oznake visoke 13 px sekle su tekst
            ThemeHelper.ZameniOznake(this, ThemeHelper.OznakeDatuma);
            ThemeHelper.OznakeIznadPolja(this);
            SavaForme.Kartice(panel1, panel4, panelGrupa1, panelGrupa2, panelGrupa3, panelGrupa4);
        }
    }
}
