using Syncfusion.Grouping;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.RNI
{
    // Pregled arhiviranih zapisa (tabela TerminalPrivArhiv) sa slikama, dokumentima i logom zapisa.
    // ID zapisa je isti kao u TerminalPriv pa se koriste isti folderi slika/dokumenata i isti log.
    public partial class frmTerminalPrivremeniArhiva : Form
    {
        private const string FormatDatuma = "dd.MM.yyyy HH:mm";

        private DataTable dt;

        public frmTerminalPrivremeniArhiva()
        {
            InitializeComponent();
        }

        private void frmTerminalPrivremeniArhiva_Load(object sender, EventArgs e)
        {
            UcitajPodatke();
        }

        private void UcitajPodatke()
        {
            try
            {
                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                var dataAdapter = new SqlDataAdapter("SELECT * FROM TerminalPrivArhiv ORDER BY ID DESC", s_connection);

                var tabela = new DataTable("TerminalPrivArhiv");
                dataAdapter.Fill(tabela);
                dt = tabela;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Neuspešno učitavanje arhive: " + ex.Message, "Arhivirani podaci",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            gridGroupingControl1.DataSource = dt;
            gridGroupingControl1.ShowGroupDropArea = true;
            gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;

            foreach (GridColumnDescriptor column in gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }

            PodesiKolone();
            Text = "Arhivirani podaci (" + dt.Rows.Count + ")";
        }

        private void PodesiKolone()
        {
            GridColumnDescriptorCollection kolone = gridGroupingControl1.TableDescriptor.Columns;
            System.Drawing.Font font = gridGroupingControl1.Font;

            kolone["ID"].Width = frmTerminalPrivremeni.SirinaKolone(font, kolone["ID"].HeaderText, "00000");

            foreach (TerminalPrivPolje polje in insertTerminalPriv.Polja)
            {
                GridColumnDescriptor kolona = kolone[polje.Kolona];
                if (kolona == null)
                    continue;

                kolona.Width = frmTerminalPrivremeni.SirinaKolone(font, kolona.HeaderText, frmTerminalPrivremeni.PrimerSadrzaja(polje));
                if (polje.Tip == TerminalPrivTip.Datum)
                {
                    kolona.Appearance.AnyRecordFieldCell.CellValueType = typeof(DateTime);
                    kolona.Appearance.AnyRecordFieldCell.Format = FormatDatuma;
                }
            }

            GridColumnDescriptor arhivirano = kolone["DatumArhiviranja"];
            if (arhivirano != null)
            {
                arhivirano.Width = frmTerminalPrivremeni.SirinaKolone(font, arhivirano.HeaderText, "88.88.8888 88:88");
                arhivirano.Appearance.AnyRecordFieldCell.CellValueType = typeof(DateTime);
                arhivirano.Appearance.AnyRecordFieldCell.Format = FormatDatuma;
            }
        }

        // Izabrani zapis: oznaceni red, inace red tekuce celije
        private Record IzaberiZapis()
        {
            Record oznaceni = null;
            foreach (SelectedRecord sr in gridGroupingControl1.Table.SelectedRecords)
            {
                if (sr.Record != null)
                    oznaceni = sr.Record;
            }
            return oznaceni ?? gridGroupingControl1.Table.CurrentRecord;
        }

        private bool IzaberiArhiviranZapis(string naslov, out int id, out string kontejner)
        {
            Record zapis = IzaberiZapis();
            object idVrednost = zapis == null ? null : zapis.GetValue("ID");
            id = idVrednost == null || idVrednost == DBNull.Value ? 0 : Convert.ToInt32(idVrednost);
            kontejner = "";

            if (id <= 0)
            {
                MessageBox.Show("Izaberite zapis u tabeli.", naslov, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            object vrednost = zapis.GetValue("KONTEJNER");
            kontejner = vrednost == DBNull.Value ? "" : Convert.ToString(vrednost);
            return true;
        }

        private void btnZakaciSlike_Click(object sender, EventArgs e)
        {
            int id;
            string kontejner;
            if (!IzaberiArhiviranZapis("Zakači slike", out id, out kontejner))
                return;

            using (var forma = new frmTerminalPrivremeniSlike(id, kontejner, true))
            {
                forma.ShowDialog(this);
                if (forma.Promenjeno)
                    PrimeniBrojSlika(id, forma.BrojSlika);
            }
        }

        // Baza je već ažurirana (updTerminalPrivArhivPoslateSlike), pa se vrednost samo prikazuje u gridu
        private void PrimeniBrojSlika(int id, int broj)
        {
            DataRow[] redovi = dt.Select("ID = " + id);
            if (redovi.Length == 0)
                return;

            redovi[0]["POSLATE SLIKE"] = broj.ToString();
            redovi[0].AcceptChanges();
        }

        private void btnZakaciDokumenta_Click(object sender, EventArgs e)
        {
            int id;
            string kontejner;
            if (!IzaberiArhiviranZapis("Zakači dokumenta", out id, out kontejner))
                return;

            using (var forma = new frmTerminalPrivremeniDokumenta(id, kontejner))
            {
                forma.ShowDialog(this);
            }
        }

        private void btnPregledLog_Click(object sender, EventArgs e)
        {
            int id;
            string kontejner;
            if (!IzaberiArhiviranZapis("Pregled log", out id, out kontejner))
                return;

            using (var forma = new frmTerminalPrivremeniLog(id, kontejner))
            {
                forma.ShowDialog(this);
            }
        }
    }
}
