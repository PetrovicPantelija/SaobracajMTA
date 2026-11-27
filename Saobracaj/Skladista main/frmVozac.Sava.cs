using Saobracaj.Theme;

namespace Saobracaj.Skladista_main
{
    // Redizajn Sava (docs/SAVA-DESIGN.md): vozač i vozilo na nalogu za prijem. Traka od 52 px sa plavim dugmetom Snimi, polja u beloj kartici,
    // oznake običnim slovima. Poziva se na kraju konstruktora (posle ChangeTextBox, koji postavlja stare fontove).
    public partial class frmVozac
    {
        private void PrimeniSava()
        {
            btnSnimi.Tag = "primary";
            ThemeHelper.Apply(this);
            ThemeHelper.OznakeNormalnimSlovima(this);
            ThemeHelper.OznakeIznadPolja(this);
            txtSifra.Visible = false;   // prazno polje van vidljivog dela zaglavlja, kod ga ne koristi
            label20.Visible = false;    // „Korisnik“ i polje uz njega su van panela trake (x = 1104 u panelu širine 182); kod ih ne koristi
            txtKorisnik.Visible = false;
            SavaForme.TrakaURed(panelHeader, panel2, btnSnimi);
            SavaForme.Kartice(panelPrijem1);
        }
    }
}
