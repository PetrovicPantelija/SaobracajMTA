using Saobracaj.Theme;
using System.Windows.Forms;

namespace Saobracaj.Drumski
{
    // Redizajn Sava (docs/SAVA-DESIGN.md): skeniranje dokumenata. Jedno plavo dugme (snimanje dokumenta),
    // Obriši stranu crveno, ostala dugmad bela; levi panel je bela kartica, proširen tako da „Snimi kao prevoznički
    // dokument“ stane u jedan red. Poziva se na kraju konstruktora (posle ChangeTextBox i dugmadi za rotaciju).
    public partial class frmSeniranjeDokumenata
    {
        private void PrimeniSava()
        {
            btnSaveTransport.Tag = "primary";   // u skladištu „Snimi“
            btnDelete.Tag = "danger";
            ThemeHelper.Apply(this);

            int potrebno = TextRenderer.MeasureText(btnSaveTransport.Text, btnSaveTransport.Font).Width + 24;
            int pomak = System.Math.Max(0, potrebno - btnSaveTransport.Width);
            if (pomak > 0)
            {
                leftPanel.Width += pomak;
                foreach (Control c in new Control[] { cmbScanners, btnScan, lbPages, btnSaveInvoice, btnSaveTransport })
                    c.Width += pomak;
                btnDelete.Left += pomak;   // Obriši stranu ostaje uz desnu ivicu liste
            }
            SavaForme.DugmadPoTekstu(leftPanel);
            SavaForme.Kartice(leftPanel);
            rightPanel.BackColor = SavaTheme.Surface;
        }
    }
}
