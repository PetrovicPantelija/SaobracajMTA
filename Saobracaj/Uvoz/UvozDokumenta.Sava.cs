using Saobracaj.Theme;
using System.Windows.Forms;

namespace Saobracaj.Uvoz
{
    // Redizajn Sava (docs/SAVA-DESIGN.md): dokumentacija uvoza (voz, kontejneri, usluge). Traka od 52 px sa plavim
    // dugmetom Sačuvaj i crvenim Izbriši; podnaslovi delova, tabele u stilu Sava (PodesiDatagridView).
    // Poziva se na kraju konstruktora (posle ChangeTextBox, koji bira traku za firmu).
    public partial class UvozDokumenta
    {
        private void PrimeniSava()
        {
            button5.Tag = "primary";   // Sačuvaj
            button6.Tag = "danger";    // Izbriši
            ThemeHelper.Apply(this);
            button23.Text = "Pronađi";
            button5.Text = "Sačuvaj";
            button6.Text = "Izbriši";
            button25.Text = SavaForme.Natpis(button25.Text);   // Refresh -> Osveži
            chkKontejner.Text = "Dokument se odnosi na određene kontejnere";
            chkUsluge.Text = "Dokument se odnosi na uslugu";
            foreach (var naslov in new Label[] { label5, label6, label7, label8, label9 })
            {
                naslov.Font = SavaTheme.SubtitleFont;
                naslov.ForeColor = SavaTheme.Text;
            }
            SavaForme.TrakaURed(panelHeader, panel2, button23, button24, button25, button5, button6);
            ThemeHelper.OznakeIznadPolja(this);
        }
    }
}
