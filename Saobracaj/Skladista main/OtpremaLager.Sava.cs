using Saobracaj.Theme;

namespace Saobracaj.Skladista_main
{
    // Redizajn Sava (docs/SAVA-DESIGN.md): izbor stavki sa lagera za otpremu. Jedno plavo dugme (Kreiraj), Izbaci crveno,
    // Odustani i Izmeni bela; tabele u stilu Sava (SrediGrid); oznake i dugmad običnim slovima.
    // Poziva se na kraju konstruktora.
    public partial class OtpremaLager
    {
        private void PrimeniSava()
        {
            btnKreiraj.Tag = "primary";
            btnIzbaciStavku.Tag = "danger";

            ThemeHelper.Apply(this);
            label1.Text = "Pretraži po magacinskom broju";
            label2.Text = "Pretraži po vlasniku";
            ThemeHelper.OznakeIznadPolja(this);
            SavaForme.DugmadNormalnimSlovima(this);   // „KREIRAJ“ -> „Kreiraj“, „ODUSTANI“ -> „Odustani“
            SavaForme.Kartice(panel3);   // polja stavke
        }
    }
}
