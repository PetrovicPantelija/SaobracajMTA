using Saobracaj.Theme;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main.Dokumenta
{
    // Redizajn Sava (docs/SAVA-DESIGN.md): interni poslovi skladišta. Isti raspored kao Otprema (Otprema.Sava.cs): šareni paneli postaju
    // bele kartice, jedno plavo dugme (Snimi), Storno crveno, ostala dugmad bela; fontovi 9 pt (forma je zbijena);
    // oznake i dugmad običnim slovima. Poziva se na kraju konstruktora.
    public partial class InterniPoslovi
    {
        private void PrimeniSava()
        {
            btnSnimi.Tag = "primary";
            btnStorno.Tag = "danger";

            // Storno, Ispravka, Saglasnost i Idi na prijemnicu konstruktor isključuje i briše im tekst: prazna dugmad se ne prikazuju
            foreach (var b in new[] { btnStorno, btnIspravka, btnSaglasnost, btnPrijemnica })
                if (string.IsNullOrEmpty(b.Text) && !b.Enabled)
                    b.Visible = false;

            ThemeHelper.ApplyZbijeno(this);
            ThemeHelper.OznakeNormalnimSlovima(this, lblTip);
            ThemeHelper.ZameniOznake(this, ThemeHelper.OznakeDatuma);
            ThemeHelper.OznakeIznadPolja(this);
            SavaForme.DugmadNormalnimSlovima(this);   // „IDI NA PRIJEMNICU“ -> „Idi na prijemnicu“
            SavaForme.Naslov(lblTip);

            // desna kolona dugmadi: tekst u jednom redu (dva reda ne staju u 32 px); leva ivica ostaje, šire se udesno
            var kolona = new[] { btnSnimi, btnStorno, btnIspravka, btnSaglasnost, btnFormiranRN, btnPrijemnica };
            int sirina = kolona.Max(b => System.Math.Max(b.Width, TextRenderer.MeasureText(b.Text, b.Font).Width + 24));
            foreach (var b in kolona)
                b.Width = sirina;

            // šareni paneli (SeaShell, PaleGreen, WhiteSmoke, LemonChiffon) i panel za izmenu magacinskog broja postaju bele kartice
            SavaForme.Kartice(panel1, panelOtprema1, panelOtprema2, panel4, panel5);
        }
    }
}
