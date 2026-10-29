using Saobracaj.Theme;
using System.Windows.Forms;

namespace Saobracaj.TrackModal.Sifarnici
{
    // Stil Sava za forme hijerarhije skladišnog prostora (Zone, Lokacije, Objekti, Polja, Pozicije).
    // Traka (panelHeader): dugmad sa tekstom umesto okruglih ikonica, "Sačuvaj" je glavno (plavo) dugme,
    // "Obriši" je dugme za brisanje; zatim ThemeHelper.Apply za boje i fontove cele forme.
    // Poziva se u konstruktoru posle InitializeComponent() i ChangeTextBox().
    public static class SifarnikTema
    {
        public static void Primeni(Form forma, Panel traka, Panel ikonice,
            Button novo, Button sacuvaj, Button obrisi, Button aktiviraj, Button deaktiviraj, Button osvezi,
            ToolStripItem tsNovo, ToolStripItem tsSacuvaj, ToolStripItem tsObrisi)
        {
            PostaviDugme(novo, "Novo", null);
            PostaviDugme(sacuvaj, "Sačuvaj", "primary");
            PostaviDugme(obrisi, "Obriši", "danger");
            PostaviDugme(aktiviraj, "Aktiviraj", null);
            PostaviDugme(deaktiviraj, "Deaktiviraj", null);
            PostaviDugme(osvezi, "Osveži", null);

            // meni za firme koje ne koriste traku: tekst umesto ikonica
            PostaviStavku(tsNovo, "Novo");
            PostaviStavku(tsSacuvaj, "Sačuvaj");
            PostaviStavku(tsObrisi, "Obriši");

            ThemeHelper.Apply(forma);

            foreach (Button b in new[] { novo, sacuvaj, obrisi, aktiviraj, deaktiviraj, osvezi })
                ThemeHelper.SirinaPoTekstu(b);

            // traka: [Novo Sačuvaj Obriši]  [Aktiviraj Deaktiviraj Osveži (Pregled)]
            ThemeHelper.Toolbar(traka);
            Grupa(ikonice, novo, sacuvaj, obrisi);

            Control drugaGrupa = aktiviraj.Parent;
            if (drugaGrupa == traka)
                ThemeHelper.PoredjajLevo(traka, ikonice, aktiviraj, deaktiviraj, osvezi);
            else
            {
                // Polja i Pozicije: Aktiviraj / Deaktiviraj / Osveži su u unutrašnjem panelu trake
                Grupa((Panel)drugaGrupa, aktiviraj, deaktiviraj, osvezi);
                ThemeHelper.PoredjajLevo(traka, ikonice, drugaGrupa);
            }
        }

        // Dugme dodato posle primene stila (npr. "Pregled"): na kraj iste grupe kao "Osveži"
        public static void DodajNaKraj(Control grupa, Button dugme)
        {
            ThemeHelper.StyleButton(dugme);
            ThemeHelper.SirinaPoTekstu(dugme);

            var razmak = new Panel { Width = 8, Dock = DockStyle.Left, BackColor = System.Drawing.Color.Transparent };
            grupa.Controls.Add(razmak);
            razmak.BringToFront();
            grupa.Controls.Add(dugme);
            dugme.Dock = DockStyle.Left;
            dugme.BringToFront();

            if (!(grupa is Form) && grupa.Dock == DockStyle.Left)
                ThemeHelper.SirinaPoSadrzaju(grupa);
        }

        private static void Grupa(Panel panel, params Button[] dugmad)
        {
            panel.BackColor = Theme.SavaTheme.White;
            panel.BackgroundImage = null;
            panel.AutoSize = false;
            panel.Padding = new Padding(0);
            ThemeHelper.PoredjajLevo(panel, dugmad);
            ThemeHelper.SirinaPoSadrzaju(panel);
        }

        private static void PostaviDugme(Button b, string tekst, string uloga)
        {
            b.BackgroundImage = null;
            b.Image = null;
            b.Text = tekst;
            b.Tag = uloga;
            b.TextImageRelation = TextImageRelation.Overlay;
            b.Padding = new Padding(0);
            b.Margin = new Padding(0);
        }

        private static void PostaviStavku(ToolStripItem stavka, string tekst)
        {
            if (stavka == null)
                return;
            stavka.Text = tekst;
            stavka.DisplayStyle = ToolStripItemDisplayStyle.Text;
        }
    }
}
