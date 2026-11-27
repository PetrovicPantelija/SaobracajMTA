using Saobracaj.Theme;

namespace Saobracaj.Skladista_main.Dokumenta
{
    // Redizajn Sava (docs/SAVA-DESIGN.md): pregled lagera. Oznake iznad lista (Segoe UI 9), tabela u stilu Sava
    // (SrediGrid, posle svakog filtriranja); rezervisane stavke ostaju označene (dataGridView1_RowPrePaint).
    // Poziva se na kraju konstruktora.
    public partial class PregledLagera
    {
        private void PrimeniSava()
        {
            ThemeHelper.Apply(this);
            ThemeHelper.OznakeIznadPolja(this);
        }
    }
}
