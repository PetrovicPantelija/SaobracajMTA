using Syncfusion.Grouping;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.RNI
{
    // Grupna promena: izabrana vrednost se upisuje u izabranu kolonu svih selektovanih zapisa
    // GridGroupingControl-a forme frmTerminalPrivremeni. Izmena ostaje u tabeli dok se ne klikne "Sačuvaj izmene".
    public partial class frmTerminalPrivremeniPromeni : Form
    {
        private const string FormatDatuma = "dd.MM.yyyy HH:mm";

        // KONTEJNER je jedinstven po zapisu, a POSLATE SLIKE se ne menja grupno
        private static readonly string[] IskljucenaPolja = { "KONTEJNER", "POSLATE SLIKE" };

        private readonly GridGroupingControl grid;
        private readonly TerminalPrivPolje[] polja;

        public int Promenjeno { get; private set; }

        public frmTerminalPrivremeniPromeni(GridGroupingControl grid)
        {
            this.grid = grid;
            InitializeComponent();

            polja = insertTerminalPriv.Polja.Where(p => !IskljucenaPolja.Contains(p.Kolona)).ToArray();
            cboPolje.Items.AddRange(polja.Select(p => (object)p.Kolona).ToArray());
            lblOznaceno.Text = "Označeno zapisa: " + grid.Table.SelectedRecords.Count;
            cboPolje.SelectedIndex = 0;
        }

        private TerminalPrivPolje IzabranoPolje()
        {
            return cboPolje.SelectedIndex < 0 ? null : polja[cboPolje.SelectedIndex];
        }

        // Kontrola za unos zavisi od tipa polja: DateTimePicker, ComboBox (ograničena lista) ili TextBox
        private void cboPolje_SelectedIndexChanged(object sender, EventArgs e)
        {
            TerminalPrivPolje polje = IzabranoPolje();
            if (polje == null)
                return;

            txtOpsti.Visible = false;
            dtpOpsti.Visible = false;
            cboOpsti.Visible = false;

            if (polje.Tip == TerminalPrivTip.Datum)
            {
                dtpOpsti.Checked = false;   // nečekirano = bez vrednosti (NULL)
                dtpOpsti.Visible = true;
            }
            else if (polje.Dozvoljeno != null)
            {
                cboOpsti.Items.Clear();
                cboOpsti.Items.Add("");     // prazno = bez vrednosti (NULL)
                cboOpsti.Items.AddRange(polje.Dozvoljeno);
                cboOpsti.SelectedIndex = 0;
                cboOpsti.Visible = true;
            }
            else
            {
                txtOpsti.MaxLength = polje.Velicina;
                txtOpsti.Text = "";
                txtOpsti.Visible = true;
            }
        }

        private object NovaVrednost(TerminalPrivPolje polje)
        {
            if (polje.Tip == TerminalPrivTip.Datum)
                return dtpOpsti.Checked ? (object)dtpOpsti.Value : DBNull.Value;

            string tekst = (polje.Dozvoljeno != null ? cboOpsti.Text : txtOpsti.Text).Trim();
            return tekst.Length == 0 ? (object)DBNull.Value : tekst;
        }

        private static string OpisVrednosti(object vrednost)
        {
            if (vrednost == DBNull.Value)
                return "(prazno)";
            return vrednost is DateTime ? ((DateTime)vrednost).ToString(FormatDatuma) : vrednost.ToString();
        }

        private void btnPrimeni_Click(object sender, EventArgs e)
        {
            TerminalPrivPolje polje = IzabranoPolje();
            if (polje == null)
                return;

            var zapisi = new List<Record>();
            foreach (SelectedRecord sr in grid.Table.SelectedRecords)
            {
                if (sr.Record != null)
                    zapisi.Add(sr.Record);
            }

            if (zapisi.Count == 0)
            {
                MessageBox.Show("Nijedan zapis nije označen u tabeli.", "Grupna promena", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            object vrednost = NovaVrednost(polje);
            if (MessageBox.Show("Polje " + polje.Kolona + " će biti postavljeno na " + OpisVrednosti(vrednost) + " za " + zapisi.Count
                + " zapisa. Nastaviti?", "Grupna promena", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            foreach (Record zapis in zapisi)
                zapis.SetValue(polje.Kolona, vrednost);

            Promenjeno = zapisi.Count;
            MessageBox.Show("Promenjeno zapisa: " + Promenjeno + ". Za upis u bazu kliknite 'Sačuvaj izmene'.",
                "Grupna promena", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
        }
    }
}
