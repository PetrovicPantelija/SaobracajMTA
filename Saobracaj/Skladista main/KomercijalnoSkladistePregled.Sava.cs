using Saobracaj.Theme;

namespace Saobracaj.Skladista_main
{
    // Redizajn Sava (docs/SAVA-DESIGN.md): lista naloga komercijalnog skladišta. Zaglavlje u dva reda (dugme iznad liste)
    // postaje traka od 52 px u jednom redu: lista pa njeno dugme (Protokol, Štampaj, Skeniraj); tabela u stilu Sava.
    // Poziva se na kraju konstruktora.
    public partial class KomercijalnoSkladistePregled
    {
        private void PrimeniSava()
        {
            ThemeHelper.Apply(this);
            button2.Text = SavaForme.Natpis(button2.Text);   // Refresh -> Osveži
            txtSifra.Visible = false;                         // prazno polje u zaglavlju, kod ga ne koristi
            foreach (var lista in new[] { cboProtokol, cboStampaj, cboSkeniraj })
                SavaForme.SirinaListe(lista);

            SavaForme.TrakaURed(panelHeader, panel2,
                button24, button25, btnViljuskarista,
                cboProtokol, button3, cboStampaj, button4, cboSkeniraj, button5,
                button1, button2);
        }
    }
}
