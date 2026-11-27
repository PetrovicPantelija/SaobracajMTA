using Saobracaj.Theme;

namespace Saobracaj.Uvoz
{
    // Redizajn Sava (docs/SAVA-DESIGN.md): tekuće stanje kontejnera. Traka od 52 px u jednom redu (Osveži, Kontejner),
    // tabela u stilu Sava. Poziva se na kraju konstruktora (posle ChangeTextBox, koji postavlja stare fontove).
    public partial class frmKontejnerTekuce
    {
        private void PrimeniSava()
        {
            ThemeHelper.Apply(this);
            button23.Text = SavaForme.Natpis(button23.Text);   // Refresh -> Osveži
            label2.Text = "Kontejner";
            SavaForme.TrakaURed(panelHeader, panel2, button23, label2, txtBrojKontejnera);
        }
    }
}
