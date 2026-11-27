using Saobracaj.Theme;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main.Dokumenta
{
    // Redizajn Sava (docs/SAVA-DESIGN.md): Otprema. Polja su u grupama koje se skupljaju (Otprema.Raspored.cs), dugmad
    // naloga u traci na vrhu; logika ostaje ista. Grupe su bele kartice, jedno plavo dugme (Snimi), Storno i Izbaci stavku
    // crveni, ostala dugmad bela; fontovi 9 pt (forma je zbijena); oznake i dugmad običnim slovima.
    // Poziva se na kraju Otprema_Load, posle dugmeta Izbor paleta (Otprema.Palete.cs).
    public partial class Otprema
    {
        private void PrimeniSava()
        {
            if (grupeForme == null)
                InitRaspored();

            btnSnimi.Tag = "primary";
            btnStorno.Tag = "danger";
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

            // traka naloga: bela, 52 px; naslov forme i dugmad na sredini po visini, dugmad široka po tekstu
            ThemeHelper.Toolbar(trakaNaloga);
            lblTip.Font = SavaTheme.FormTitleFont;
            lblTip.ForeColor = SavaTheme.Text;
            lblTip.Margin = new Padding(0, System.Math.Max(0, (SavaTheme.ButtonHeight - lblTip.PreferredHeight) / 2), 24, 0);
            foreach (var b in trakaNaloga.Controls.OfType<Button>().Concat(trakaStavki.Controls.OfType<Button>()))
            {
                b.Height = SavaTheme.ButtonHeight;
                ThemeHelper.SirinaPoTekstu(b);
                b.Width = System.Math.Max(b.Width, 110);
            }

            // grupe: radna površina, sadržaj grupe je bela kartica; paneli u grupama beli bez okvira
            var split = grupeForme.Split;
            split.BackColor = SavaTheme.Line;   // razdelnik
            split.Panel1.BackColor = SavaTheme.Surface;
            split.Panel2.BackColor = SavaTheme.Surface;
            foreach (var okvir in split.Panel1.Controls.OfType<Panel>())
            {
                okvir.BackColor = SavaTheme.Surface;
                foreach (var sadrzaj in okvir.Controls.OfType<Panel>())
                    ThemeHelper.Kartica(sadrzaj);
            }
            trakaStavki.BackColor = SavaTheme.Surface;
            foreach (var p in new Panel[] { panel1, panelOtprema1, panelOtprema2, panel4 })
                p.BackColor = SavaTheme.White;

            // polja izabrane stavke i paneli za izbor (magacinski broj, prijemnica) su kartice
            foreach (var p in new Panel[] { panel3, panel5, panel2 })
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
