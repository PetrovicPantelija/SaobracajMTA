using Saobracaj.Theme;

namespace Saobracaj.Skladista_main
{
    // Redizajn Sava (docs/SAVA-DESIGN.md): lista komercijalnih naloga. Traka od 52 px sa dugmadima u jednom redu,
    // polje „KO nalog ID“ desno od dugmadi; tabela u stilu Sava (žuto označeni aktivni nalozi ostaju).
    // Poziva se na kraju konstruktora.
    public partial class PregledKomercijalnihNaloga
    {
        private void PrimeniSava()
        {
            ThemeHelper.Apply(this);
            button25.Text = SavaForme.Natpis(button25.Text);   // Refresh -> Osveži
            label3.Text = "KO nalog ID";
            txtSifra.Visible = false;                           // prazno polje u zaglavlju, kod ga ne koristi

            SavaForme.TrakaURed(panelHeader, panel2, button24, button25, btnAktiviraj, button1);
            label3.Left = panel2.Right + 24;
            txtNALOGID.Left = label3.Right + 8;
            foreach (System.Windows.Forms.Control c in new System.Windows.Forms.Control[] { label3, txtNALOGID })
                c.Top = (panelHeader.Height - c.Height) / 2;
        }
    }
}
