using Saobracaj.Theme;
using System.Windows.Forms;

namespace Saobracaj.TerminalMap
{
    // Redizajn Sava (docs/SAVA-DESIGN.md): mapa terminala. Meni iznad mape ostaje taman (Noćna Sava, kao navigaciona
    // traka), jer kod stanje „Pozadina“ i „Edit“ pokazuje zelenom i crvenom bojom teksta koje se na beloj ne vide;
    // panel sa stanjem polja je bela kartica, tabela u stilu Sava. Poziva se na kraju konstruktora.
    public partial class TerminalMapFRM
    {
        private void PrimeniSava()
        {
            ThemeHelper.Apply(this);
            menuStrip1.BackColor = SavaTheme.Night;
            menuStrip1.Font = SavaTheme.ButtonFont;
            foreach (ToolStripItem s in menuStrip1.Items)
                s.ForeColor = SavaTheme.MenuText;
            btnSaveRegions.Text = "Sačuvaj polja";
            btnResetViews.Text = "Vrati prikaz";
            ThemeHelper.Kartica(panel1);
        }
    }
}
