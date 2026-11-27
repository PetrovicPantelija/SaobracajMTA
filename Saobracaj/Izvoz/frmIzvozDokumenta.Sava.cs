using Saobracaj.Theme;
using System.Windows.Forms;

namespace Saobracaj.Izvoz
{
    // Redizajn Sava (docs/SAVA-DESIGN.md): dokumentacija izvoza (kontejner, buking, voz). Tri ravnopravna dela, zato
    // nijedno dugme nije plavo; Izbriši je crveno, ostala dugmad bela (bila su zelena). Delovi su bele kartice sa
    // podnaslovom, tabele u stilu Sava (RefreshDataGrid). Poziva se na kraju konstruktora.
    public partial class frmIzvozDokumenta
    {
        private void PrimeniSava()
        {
            foreach (var b in new Button[] { button3, button5, button9 })
                b.Tag = "danger";
            ThemeHelper.Apply(this);
            ThemeHelper.OznakeIznadPolja(this);
            foreach (var naslov in new Label[] { label3, label5, label9 })
            {
                naslov.Font = SavaTheme.SubtitleFont;
                naslov.ForeColor = SavaTheme.Text;
            }
            SavaForme.Kartice(panel1, panel2, panel3);
        }
    }
}
