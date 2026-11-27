using Saobracaj.Theme;

namespace Saobracaj.Uvoz
{
    // Redizajn Sava (docs/SAVA-DESIGN.md): dodatne usluge na kontejneru. Levi deo je bela kartica, jedno plavo dugme
    // (Sačuvaj promenu), oznake običnim slovima, dugmad dovoljno visoka za tekst, tabela u stilu Sava.
    // Poziva se na kraju konstruktora (posle ChangeTextBox).
    public partial class frmDodatneUsluge
    {
        private void PrimeniSava()
        {
            button4.Tag = "primary";   // Sačuvaj promenu
            ThemeHelper.Apply(this);
            ThemeHelper.OznakeNormalnimSlovima(this);   // „DODATNA USLUGA:“ -> „Dodatna usluga“
            ThemeHelper.OznakeIznadPolja(this);
            chkDodatne.Text = "Unos dodatnih usluga Terminal";
            chkPotvrdiUradjen.Text = "Potvrdi urađen";
            SavaForme.RedDugmadi(button4.Left, button4.Top, button4, button1);   // Sačuvaj promenu, Dodeli slike
            SavaForme.DugmadPoTekstu(splitContainer1.Panel1);
            ThemeHelper.Kartica(splitContainer1.Panel1);
            splitContainer1.Panel2.BackColor = SavaTheme.Surface;
        }
    }
}
