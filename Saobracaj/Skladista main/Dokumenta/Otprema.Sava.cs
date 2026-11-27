using Saobracaj.Theme;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main.Dokumenta
{
    // Redizajn Sava (docs/SAVA-DESIGN.md): Otprema. Raspored polja i logika ostaju isti; šareni paneli postaju bele
    // kartice, jedno plavo dugme (Snimi), Izbaci stavku crveno, ostala dugmad bela; fontovi 9 pt (forma je zbijena);
    // oznake i dugmad običnim slovima. Poziva se na kraju Otprema_Load, posle dugmeta Izbor paleta (Otprema.Palete.cs).
    public partial class Otprema
    {
        private void PrimeniSava()
        {
            btnSnimi.Tag = "primary";
            btnIzbaciStavku.Tag = "danger";   // izbacuje stavku iz otpreme

            // Storno, Ispravka i Saglasnost konstruktor za nov nalog isključuje i briše im tekst: prazna dugmad se ne prikazuju
            foreach (var b in new[] { btnStorno, btnIspravka, btnSaglasnost })
                if (string.IsNullOrEmpty(b.Text) && !b.Enabled)
                    b.Visible = false;

            ThemeHelper.ApplyZbijeno(this);
            ThemeHelper.OznakeNormalnimSlovima(this, lblTip);
            ThemeHelper.ZameniOznake(this, ThemeHelper.OznakeDatuma);
            ThemeHelper.OznakeIznadPolja(this);
            foreach (var b in SveKontrole(this).OfType<Button>())
                b.Text = ThemeHelper.NormalnaSlova(b.Text);   // „IDI NA OTPREMNICU“ -> „Idi na otpremnicu“

            lblTip.Font = SavaTheme.FormTitleFont;
            lblTip.ForeColor = SavaTheme.Text;

            // desna kolona dugmadi: tekst u jednom redu (dva reda ne staju u 32 px); leva ivica ostaje, šire se udesno
            var kolona = new[] { btnSnimi, btnFormiranRN, btnOtpremnica };
            int sirina = kolona.Max(b => System.Math.Max(b.Width, TextRenderer.MeasureText(b.Text, b.Font).Width + 24));
            foreach (var b in kolona)
                b.Width = sirina;

            // šareni paneli (SeaShell, PaleGreen, WhiteSmoke, LemonChiffon) i paneli za izbor postaju bele kartice
            foreach (var p in new Panel[] { panel1, panelOtprema1, panelOtprema2, panel4, panel3, panel5, panel2 })
                ThemeHelper.Kartica(p);
        }

        private static System.Collections.Generic.IEnumerable<Control> SveKontrole(Control root)
        {
            foreach (Control c in root.Controls)
            {
                yield return c;
                foreach (var d in SveKontrole(c))
                    yield return d;
            }
        }
    }
}
