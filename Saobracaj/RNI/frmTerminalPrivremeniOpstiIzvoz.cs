using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.RNI
{
    // Opšti izvoz: korisnik štiklira polja (kolone grida) koja idu u Excel.
    // Polja = parovi (kolona u tabeli, naslov kolone u gridu), redom kako su u gridu.
    public partial class frmTerminalPrivremeniOpstiIzvoz : Form
    {
        private readonly List<KeyValuePair<string, string>> polja;

        public frmTerminalPrivremeniOpstiIzvoz(List<KeyValuePair<string, string>> polja, ICollection<string> oznacena, int brojZapisa)
        {
            InitializeComponent();
            this.polja = polja;

            foreach (KeyValuePair<string, string> p in polja)
                clbPolja.Items.Add(p.Value, oznacena == null || oznacena.Contains(p.Key));

            lblZapisa.Text = "Zapisa za izvoz (posle filtera): " + brojZapisa;
        }

        // Kolone u tabeli za štiklirana polja, redom kao u listi
        public List<string> IzabranaPolja
        {
            get { return clbPolja.CheckedIndices.Cast<int>().Select(i => polja[i].Key).ToList(); }
        }

        private void btnOznaciSve_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbPolja.Items.Count; i++)
                clbPolja.SetItemChecked(i, true);
        }

        private void btnPonistiSve_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbPolja.Items.Count; i++)
                clbPolja.SetItemChecked(i, false);
        }

        private void btnIzvezi_Click(object sender, EventArgs e)
        {
            if (clbPolja.CheckedIndices.Count == 0)
            {
                MessageBox.Show("Štiklirajte bar jedno polje za izvoz.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DialogResult = DialogResult.OK;
        }
    }
}
