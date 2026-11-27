using Saobracaj.Theme;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main.Dokumenta
{
    // Redizajn Sava (docs/SAVA-DESIGN.md): Prijemnica. Raspored polja i logika ostaju isti; menjaju se boje, fontovi
    // (9 pt, jer je forma zbijena), dugmad, traka, sekcije kao bele kartice i oznake (obična slova).
    // Poziva se na kraju konstruktora, posle InitRaspored.
    public partial class Prijemnica
    {
        private void PrimeniSava()
        {
            button24.Tag = "primary";   // Snimi
            foreach (var b in SveKontrole(this).OfType<Button>().Where(b => b.Text == "Ukloni stavku"))
                b.Tag = "danger";

            ThemeHelper.ApplyZbijeno(this);
            ThemeHelper.OznakeNormalnimSlovima(this);
            ThemeHelper.ZameniOznake(this, ThemeHelper.OznakeDatuma);
            ThemeHelper.OznakeIznadPolja(this);

            // traka: bela, 52 px; dugmad i liste ostaju na svojim mestima, samo na sredini po visini
            ThemeHelper.Toolbar(panelHeader);
            panel8.BackColor = SavaTheme.White;
            panel8.Height = SavaTheme.ButtonHeight;
            panel8.Top = (panelHeader.Height - panel8.Height) / 2;
            // jedina dokovana dugmad trake idu na fiksna mesta, kao ostale kontrole; mesta se pamte pre nego što se
            // ijedno skine sa Dock (inače se drugo odmah pomeri na mesto prvog)
            var dokovana = new[] { button24, button25 };
            var mesta = dokovana.Select(b => b.Location).ToArray();
            for (int i = 0; i < dokovana.Length; i++)
            {
                dokovana[i].Dock = DockStyle.None;
                dokovana[i].Location = mesta[i];
            }
            foreach (Control c in panel8.Controls)
            {
                if (c is Button)
                    c.Height = SavaTheme.ButtonHeight;
                c.Top = (panel8.Height - c.Height) / 2;
            }
            // razmak od 8 px između kontrola trake (Snimi i Uvoz dokumenata su bili priljubljeni, Štampaj ispod liste)
            int desno = int.MinValue;
            foreach (Control c in panel8.Controls.Cast<Control>().OrderBy(c => c.Left))
            {
                if (desno != int.MinValue && c.Left < desno + 8)
                    c.Left = desno + 8;
                desno = c.Right;
            }

            // sekcije: radna površina, sadržaj sekcije je bela kartica
            if (splitRaspored != null)
            {
                splitRaspored.BackColor = SavaTheme.Line;   // razdelnik
                splitRaspored.Panel1.BackColor = SavaTheme.Surface;
                splitRaspored.Panel2.BackColor = SavaTheme.Surface;
                foreach (var traka in splitRaspored.Panel2.Controls.OfType<FlowLayoutPanel>())
                    traka.BackColor = SavaTheme.Surface;
            }
            foreach (var s in sekcije)
            {
                s.Okvir.BackColor = SavaTheme.Surface;
                ThemeHelper.Kartica(s.Sadrzaj);
            }
            foreach (var p in new Panel[] { panel1, panel2, panel3, panel4 })
                p.BackColor = SavaTheme.White;
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
