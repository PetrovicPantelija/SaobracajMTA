using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Saobracaj.RNI
{
    public partial class frmTerminalPrivremeniExcel : Form
    {
        // Excel kolone koje se uvoze, u redosledu parametara stored procedure insTerminalPrivFromExcel:
        // KONTEJNER, VRSTA, BRODAR, NALOGODAVAC, POSTUPAK, UVOZNIK, PLOMBA_UVOZ
        private static readonly string[] KoloneZaUvoz = { "C", "D", "K", "L", "T", "M", "H" };
        private static readonly string[] NaziviPolja = { "KONTEJNER", "VRSTA", "BRODAR", "NALOGODAVAC", "POSTUPAK", "UVOZNIK", "PLOMBA_UVOZ" };
        private const int MaxDuzina = 25;
        private const int MinBrojKolona = 20;   // do kolone T

        private DataTable tabela;
        private int prviRedPodataka;   // broj Excel reda prvog reda u tabeli

        // Broj redova upisanih u bazu (koristi forma koja je otvorila ovu formu)
        public int Uvezeno { get; private set; }

        public frmTerminalPrivremeniExcel()
        {
            InitializeComponent();
        }

        // 1 -> A, 26 -> Z, 27 -> AA ...
        private static string NazivKolone(int broj)
        {
            string naziv = "";
            while (broj > 0)
            {
                broj--;
                naziv = (char)('A' + broj % 26) + naziv;
                broj /= 26;
            }
            return naziv;
        }

        private void btnOtvoriFajl_Click(object sender, EventArgs e)
        {
            if (ofdDialog.ShowDialog(this) != DialogResult.OK)
                return;

            try
            {
                UcitajFajlUGrid(ofdDialog.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fajl nije moguće otvoriti: " + ex.Message, "Uvoz Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Prvi list fajla se prikazuje u dataGridView1. Prvi red u fajlu je zaglavlje;
        // kolone se nazivaju slovima iz Excel-a (A, B, C...) jer se po njima mapiraju polja.
        private void UcitajFajlUGrid(string putanja)
        {
            var nova = new DataTable();

            using (ExcelEngine engine = new ExcelEngine())
            {
                IWorkbook radnaKnjiga = engine.Excel.Workbooks.Open(putanja);
                IWorksheet list = radnaKnjiga.Worksheets[0];
                IRange opseg = list.UsedRange;

                int redZaglavlja = opseg.Row;
                int poslednjiRed = opseg.LastRow;
                int brojKolona = Math.Max(opseg.LastColumn, MinBrojKolona);

                for (int c = 1; c <= brojKolona; c++)
                    nova.Columns.Add(NazivKolone(c), typeof(string));

                var zaglavlja = new string[brojKolona + 1];
                for (int c = 1; c <= brojKolona; c++)
                    zaglavlja[c] = (list[redZaglavlja, c].DisplayText ?? "").Trim();

                for (int r = redZaglavlja + 1; r <= poslednjiRed; r++)
                {
                    DataRow red = nova.NewRow();
                    bool imaPodataka = false;
                    for (int c = 1; c <= brojKolona; c++)
                    {
                        string tekst = (list[r, c].DisplayText ?? "").Trim();
                        red[c - 1] = tekst;
                        if (tekst.Length > 0)
                            imaPodataka = true;
                    }

                    if (imaPodataka)
                    {
                        nova.Rows.Add(red);
                        nova.ExtendedProperties["red" + (nova.Rows.Count - 1)] = r;
                    }
                }

                radnaKnjiga.Close();

                tabela = nova;
                prviRedPodataka = redZaglavlja + 1;
                dataGridView1.DataSource = tabela;

                for (int c = 1; c <= brojKolona; c++)
                {
                    DataGridViewColumn kolona = dataGridView1.Columns[c - 1];
                    kolona.SortMode = DataGridViewColumnSortMode.NotSortable;
                    kolona.HeaderText = zaglavlja[c].Length > 0 ? NazivKolone(c) + " - " + zaglavlja[c] : NazivKolone(c);

                    int mesto = Array.IndexOf(KoloneZaUvoz, NazivKolone(c));
                    if (mesto >= 0)
                        kolona.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(220, 240, 255);   // kolone koje se uvoze
                }
            }

            if (tabela.Rows.Count == 0)
            {
                MessageBox.Show("U fajlu nema redova sa podacima ispod zaglavlja.", "Uvoz Excel",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnUcitaj_Click(object sender, EventArgs e)
        {
            if (tabela == null || tabela.Rows.Count == 0)
            {
                MessageBox.Show("Prvo otvorite Excel fajl sa podacima.", "Uvoz Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            dataGridView1.EndEdit();

            var redovi = new List<string[]>();
            var greske = new List<string>();

            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                DataRow red = tabela.Rows[i];
                int excelRed = ExcelRed(i);

                var vrednosti = new string[KoloneZaUvoz.Length];
                for (int k = 0; k < KoloneZaUvoz.Length; k++)
                {
                    object v = red[KoloneZaUvoz[k]];
                    vrednosti[k] = v == null || v == DBNull.Value ? "" : v.ToString().Trim();
                }

                var greskeReda = new List<string>();
                if (vrednosti[0].Length != insertTerminalPriv.DuzinaKontejnera)
                    greskeReda.Add("KONTEJNER (kolona C) mora imati tačno " + insertTerminalPriv.DuzinaKontejnera + " karaktera");

                for (int k = 1; k < vrednosti.Length; k++)
                {
                    if (vrednosti[k].Length > MaxDuzina)
                        greskeReda.Add(NaziviPolja[k] + " (kolona " + KoloneZaUvoz[k] + ") može imati najviše " + MaxDuzina + " karaktera");
                }

                if (greskeReda.Count > 0)
                    greske.Add("Red " + excelRed + ": " + string.Join("; ", greskeReda));
                else
                    redovi.Add(vrednosti);
            }

            if (greske.Count > 0)
            {
                var poruka = new StringBuilder();
                poruka.AppendLine("Ništa nije upisano. Ispravite podatke u tabeli (ćelije se mogu menjati) i ponovo kliknite 'Učitaj'.");
                poruka.AppendLine("Redova sa greškom: " + greske.Count);
                foreach (string g in greske.GetRange(0, Math.Min(15, greske.Count)))
                    poruka.AppendLine(g);
                if (greske.Count > 15)
                    poruka.AppendLine("...");
                MessageBox.Show(poruka.ToString(), "Uvoz Excel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Uvezeno = new insertTerminalPriv().InsTerminalPrivFromExcel(redovi);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Uvoz Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Upisano redova u TerminalPriv: " + Uvezeno, "Uvoz Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        // Broj reda u Excel fajlu za red tabele (redovi bez podataka su izostavljeni, pa se pamti stvarni broj)
        private int ExcelRed(int indeks)
        {
            object sacuvano = tabela.ExtendedProperties["red" + indeks];
            return sacuvano == null ? prviRedPodataka + indeks : Convert.ToInt32(sacuvano);
        }
    }
}
