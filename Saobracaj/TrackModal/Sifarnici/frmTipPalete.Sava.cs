using Saobracaj.Theme;
using System.Windows.Forms;

namespace Saobracaj.TrackModal.Sifarnici
{
    // Redizajn Sava (docs/SAVA-DESIGN.md): šifarnik tipova paleta, isto kao šifarnici hijerarhije (SifarnikTema):
    // traka od 52 px sa dugmadima sa tekstom umesto okruglih ikonica, Sačuvaj plavo, Obriši crveno; tabela u stilu Sava
    // (RefreshDataGrid). Poziva se na kraju konstruktora (posle ChangeTextBox, koji bira traku za firmu).
    public partial class frmTipPalete
    {
        private void PrimeniSava()
        {
            Dugme(button22, "Novo", null);
            Dugme(button21, "Sačuvaj", "primary");
            Dugme(button20, "Obriši", "danger");
            // meni za firme koje ne koriste traku: tekst umesto ikonica
            foreach (var s in new[] { new { Stavka = (ToolStripItem)tsNew, Tekst = "Novo" }, new { Stavka = (ToolStripItem)tsSave, Tekst = "Sačuvaj" }, new { Stavka = (ToolStripItem)tsDelete, Tekst = "Obriši" } })
            {
                s.Stavka.Text = s.Tekst;
                s.Stavka.DisplayStyle = ToolStripItemDisplayStyle.Text;
            }
            ThemeHelper.Apply(this);
            panel3.BackgroundImage = null;
            SavaForme.TrakaURed(panelHeader, panel3, button22, button21, button20);
        }

        private static void Dugme(Button b, string tekst, string uloga)
        {
            b.BackgroundImage = null;
            b.Image = null;
            b.Text = tekst;
            b.Tag = uloga;
            b.TextImageRelation = TextImageRelation.Overlay;
            b.Padding = new Padding(0);
        }
    }
}
