using Syncfusion.WinForms.Controls;
using System.Drawing;

namespace Saobracaj.Skladista_main
{
    // RAZ-16: dugme „Kartica prometa“ (tabela MagacinskaKnjigaCarinska); evidencija carinskog skladišta
    // (frmCarinskaKnjiga) otvara se sa dugmeta „Magacinska knjiga“.
    // Mreža 4 × 2 je puna, pa dugme ide u treći red ispod Magacinske knjige, istog izgleda;
    // forma dobija klizač ako panel nije dovoljno visok.
    public partial class CarinskoSkladiste
    {
        private void InitCarinskaKnjiga()
        {
            var uzor = btnMagacinskaKnjiga;
            int razmakRedova = btnMagacinskaKnjiga.Top - btnPrijemRobe.Top;

            var btn = new SfButton
            {
                AccessibleName = "Button",
                Text = "Kartica prometa",
                Font = uzor.Font,
                ForeColor = uzor.ForeColor,
                BackColor = uzor.BackColor,
                ImageMargin = uzor.ImageMargin,
                ImageSize = uzor.ImageSize,
                Size = uzor.Size,
                Location = new Point(uzor.Left, uzor.Top + razmakRedova),
                TextImageRelation = uzor.TextImageRelation,
                UseVisualStyleBackColor = false
            };
            btn.Style.BackColor = uzor.Style.BackColor;
            btn.Style.DisabledBackColor = uzor.Style.DisabledBackColor;
            btn.Style.FocusedBackColor = uzor.Style.FocusedBackColor;
            btn.Style.FocusedForeColor = uzor.Style.FocusedForeColor;
            btn.Style.ForeColor = uzor.Style.ForeColor;
            btn.Style.Image = uzor.Style.Image;
            btn.Style.PressedBackColor = uzor.Style.PressedBackColor;
            btn.Style.PressedForeColor = uzor.Style.PressedForeColor;
            btn.Click += (s, e) =>
            {
                var main = this.TopLevelControl as NewMain;
                if (main == null) return;
                main.OtvoriFormuBezPrava(() => new MagacinskaKnjigaCarinska());
            };

            Controls.Add(btn);
            AutoScroll = true;
        }
    }
}
