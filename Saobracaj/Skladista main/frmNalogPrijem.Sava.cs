using Saobracaj.Theme;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main
{
    // Redizajn Sava (docs/SAVA-DESIGN.md): nalog za prijem i magacinski predmet. Raspored polja i logika ostaju isti;
    // menjaju se boje, fontovi (9 pt, jer je forma zbijena), dugmad, traka, kartice grupa i oznake (obična slova).
    // Poziva se na kraju konstruktora, posle InitRaspored (grupe polja već postoje).
    public partial class frmNalogPrijem
    {
        private void PrimeniSava()
        {
            btnSnimi.Tag = "primary";
            btnDelete.Tag = "danger";   // Izbriši nalog
            button5.Tag = "danger";     // Izbriši naimenovanje

            var naslovMP = panelMP.Controls.OfType<Label>()
                .FirstOrDefault(l => string.Equals(l.Text.Trim(), "MAGACINSKI PREDMET", System.StringComparison.OrdinalIgnoreCase));

            ThemeHelper.ApplyZbijeno(this);
            ThemeHelper.OznakeNormalnimSlovima(this, lblTitle);
            ThemeHelper.ZameniOznake(this, ThemeHelper.OznakeDatuma);
            ThemeHelper.OznakeIznadPolja(this);
            PoravnajDugmadTarife();   // frmNalogPrijem.ZamenaTarife.cs

            // traka: bela, 52 px, dugmad 32 px sa razmakom; naslov forme Semibold 14
            ThemeHelper.Toolbar(panelHeader);
            panel2.BackColor = SavaTheme.White;
            panel2.Top = (panelHeader.Height - SavaTheme.ButtonHeight) / 2;
            panel2.Height = SavaTheme.ButtonHeight;
            var dugmad = panel2.Controls.OfType<Button>().Where(b => b.Dock == DockStyle.Left).OrderBy(b => b.Left).ToArray();
            foreach (var b in dugmad)
                ThemeHelper.SirinaPoTekstu(b);
            ThemeHelper.PoredjajLevo(panel2, dugmad);
            lblTitle.Font = SavaTheme.FormTitleFont;
            lblTitle.ForeColor = SavaTheme.Text;
            foreach (Control c in new Control[] { lblTitle, label20, txtKorisnik })
                c.Top = (panel2.Height - c.Height) / 2;
            foreach (Control c in panelHeader.Controls)   // ostale kontrole trake (van panel2): sredina po visini
                if (c != panel2)
                    c.Top = (panelHeader.Height - c.Height) / 2;

            foreach (var b in panelMP.Controls.OfType<Button>().Where(b => b.Text == "Sledeći broj"))
                b.Height = System.Math.Max(b.Height, 26);   // Segoe UI: „j“ se sekao na 22 px

            if (naslovMP != null)
            {
                naslovMP.Font = SavaTheme.SubtitleFont;
                naslovMP.ForeColor = SavaTheme.Text;
            }

            // grupe: radna površina, sadržaj grupe je bela kartica; delovi magacinskog predmeta su kartice u kartici
            if (grupeForme != null && grupeForme.Split != null)
            {
                var split = grupeForme.Split;
                split.BackColor = SavaTheme.Surface;
                split.Panel1.BackColor = SavaTheme.Surface;
                split.Panel2.BackColor = SavaTheme.Surface;
                foreach (var okvir in split.Panel1.Controls.OfType<Panel>())
                {
                    okvir.BackColor = SavaTheme.Surface;
                    foreach (var sadrzaj in okvir.Controls.OfType<Panel>())
                        ThemeHelper.Kartica(sadrzaj);
                }
                foreach (var traka in split.Panel2.Controls.OfType<FlowLayoutPanel>())
                    traka.BackColor = SavaTheme.Surface;
            }
            foreach (var p in new Panel[] { panel1, panelUnosStavki })
                p.BackColor = SavaTheme.White;
            foreach (var p in new Panel[] { panelMP, panelPrijem2, panel4 })
                ThemeHelper.Kartica(p);
            panelStavke.BackColor = SavaTheme.White;
        }
    }
}
