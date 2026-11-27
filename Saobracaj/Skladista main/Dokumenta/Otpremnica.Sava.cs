using Saobracaj.Theme;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main.Dokumenta
{
    // Redizajn Sava (docs/SAVA-DESIGN.md): Otpremnica. Raspored polja i logika ostaju isti; šareni paneli postaju bele
    // kartice, jedno plavo dugme (Snimi), ostala dugmad bela i u jednom redu ispod polja; fontovi 9 pt (forma je zbijena);
    // oznake običnim slovima. Poziva se na kraju konstruktora, posle PodesiIzlaz (dugmad izlaza već postoje).
    public partial class Otpremnica
    {
        private void PrimeniSava()
        {
            btnSnimi.Tag = "primary";

            ThemeHelper.ApplyZbijeno(this);
            ThemeHelper.OznakeNormalnimSlovima(this, lblTip);
            ThemeHelper.ZameniOznake(this, ThemeHelper.OznakeDatuma);
            ThemeHelper.OznakeIznadPolja(this);
            SavaForme.Naslov(lblTip);

            // dugmad ispod polja: tekst u jednom redu („Nalog za istovar rukovaocima“ je bio u dva reda), redom sleva
            SavaForme.RedDugmadi(btnNalog.Left, btnSnimi.Top,
                btnNalog, btnSnimi, btnZapisnik, btnStampajPrijemnicu, btnPreuzmi, btnPotvrdiIzlaz, btnRazduzenje);

            // panel naloga za viljuškaristu: „Izdaj nalog“ je bio u dva reda i sekao se na „Izdaj“
            SavaForme.RedDugmadi(btnIzdajNalog.Left, btnIzdajNalog.Top, btnIzdajNalog, btnVratiNalog);

            // šareni paneli (SeaShell, PaleGreen, WhiteSmoke, LemonChiffon) i panel naloga za viljuškaristu postaju bele kartice
            SavaForme.Kartice(panel1, panelOtprema1, panelOtprema2, panel4, panel7);
        }
    }
}
