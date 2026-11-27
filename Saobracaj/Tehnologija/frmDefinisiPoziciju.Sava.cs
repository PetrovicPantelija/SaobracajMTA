using Saobracaj.Theme;

namespace Saobracaj.Tehnologija
{
    // Redizajn Sava (docs/SAVA-DESIGN.md): definisanje pozicija kontejnera. Traka od 52 px sa dugmadima u jednom redu,
    // stavke menija običnim slovima, tabele u stilu Sava. Nema glavnog dugmeta: Snimi prazan i Snimi pun su ravnopravni.
    // Poziva se na kraju konstruktora.
    public partial class frmDefinisiPoziciju
    {
        private void PrimeniSava()
        {
            ThemeHelper.Apply(this);
            ThemeHelper.OznakeNormalnimSlovima(this);   // „NA POLJE 1“ -> „Na polje 1“
            ThemeHelper.OznakeIznadPolja(this);
            splitContainer1.Panel1.BackColor = SavaTheme.Surface;
            splitContainer1.Panel2.BackColor = SavaTheme.Surface;
            button23.Text = "Snimi prazan";
            button27.Text = SavaForme.Natpis(button27.Text);   // Refresh -> Osveži
            SavaForme.StavkeNormalnimSlovima(meniHeader);         // „SNIMI PRAZAN“ -> „Snimi prazan“ (firme bez trake)
            SavaForme.TrakaURed(panelHeader, panel2, button23, button24, button25, button26, button27, button1);
        }
    }
}
