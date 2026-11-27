using Syncfusion.Grouping;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Saobracaj.RNI
{
    // Pregled arhiviranih zapisa (tabela TerminalPrivArhiv) sa slikama, dokumentima i logom zapisa.
    // ID zapisa je isti kao u TerminalPriv pa se koriste isti folderi slika/dokumenata i isti log.
    public partial class frmTerminalPrivremeniArhiva : Form
    {
        private const string FormatDatuma = "dd.MM.yyyy HH:mm";

        private DataTable dt;

        // Broj zapisa vraćenih u terminal; glavna forma ih posle zatvaranja prikazuje u gridu
        public int Vraceno { get; private set; }

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
                var dataAdapter = new SqlDataAdapter("SELECT ID, " + insertTerminalPriv.SqlKolone() + ", DatumArhiviranja FROM TerminalPrivArhiv ORDER BY ID DESC", s_connection);

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

                // VOZAČ se ne prikazuje u gridu, kao ni u terminalu
                if (polje.Tip == TerminalPrivTip.TekstZaPotvrdu)
                {
                    gridGroupingControl1.TableDescriptor.VisibleColumns.Remove(polje.Kolona);
                    continue;
                }

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

        // Slike i dokumenta označenih zapisa (ili izabranog) na Desktop, folder po kontejneru
        private void btnIzveziSlikeIDokumenta_Click(object sender, EventArgs e)
        {
            var zapisi = new List<KeyValuePair<int, string>>();
            foreach (SelectedRecord sr in gridGroupingControl1.Table.SelectedRecords)
            {
                object id = sr.Record == null ? null : sr.Record.GetValue("ID");
                if (id != null && id != DBNull.Value)
                    zapisi.Add(new KeyValuePair<int, string>(Convert.ToInt32(id), Convert.ToString(sr.Record.GetValue("KONTEJNER"))));
            }

            if (zapisi.Count == 0)
            {
                int id;
                string kontejner;
                if (!IzaberiArhiviranZapis("Izvezi slike i dokumenta", out id, out kontejner))
                    return;
                zapisi.Add(new KeyValuePair<int, string>(id, kontejner));
            }

            TerminalPrivFajloviNaDesktop.Izvezi(this, zapisi);
        }

        // Vrati iz arhive: zapis se vraća u terminal (TerminalPriv) sa istim ID-jem. Poništava se poslednja izmena zapisa, kao Undo:
        // sva polja koja je promenila vraćaju se na vrednost pre nje, brišu se pokreti koje je upisala u izveštaj brodaru i ona sama
        // iz loga, a Konačni_GATE_OUT se briše uvek (procedura VratiTerminalPrivIzArhive). Pre vraćanja poruka kaže šta se vraća i
        // briše i da li je pokret već poslat brodaru.
        private void btnVratiIzArhive_Click(object sender, EventArgs e)
        {
            const string naslov = "Vrati iz arhive";
            int id;
            string kontejner;
            if (!IzaberiArhiviranZapis(naslov, out id, out kontejner))
                return;

            var ins = new insertTerminalPriv();
            DataSet provera;
            try
            {
                provera = ins.ProveriVracanjeIzArhive(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, naslov, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (provera.Tables.Count < 3 || !provera.Tables[0].Columns.Contains("BrojIzmena"))
            {
                MessageBox.Show("Procedura VratiTerminalPrivIzArhive u bazi je starija verzija. Na bazi treba pustiti skriptu TerminalPriv_Vracanje_Ponisti_2026_11.sql.",
                    naslov, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRow stanje = provera.Tables[0].Rows[0];
            DataTable pokreti = provera.Tables[1];
            DataTable promene = provera.Tables[2];
            bool poslat = pokreti.Rows.Cast<DataRow>().Any(p => Convert.ToBoolean(p["Poslat"]));
            int uTerminalu = Convert.ToInt32(stanje["UTerminalu"]);
            int brojIzmena = Convert.ToInt32(stanje["BrojIzmena"]);

            var poruka = new StringBuilder();
            poruka.AppendLine("Vratiti kontejner " + kontejner + " (ID " + id + ") iz arhive u terminal?");
            poruka.AppendLine();
            object izmena = stanje["DatumIzmene"];
            if (brojIzmena > 0 && izmena != DBNull.Value)
                poruka.AppendLine("Poništava se izmena od " + Convert.ToDateTime(izmena).ToString(FormatDatuma)
                    + (brojIzmena > 1 ? " i " + (brojIzmena - 1) + " kasnijih" : "")
                    + ". Polja se vraćaju na vrednost pre izmene:");
            else if (pokreti.Rows.Count > 0)
                poruka.AppendLine("Poništava se poslednji upis u izveštaj brodaru (upisan pri unosu zapisa ili promeni GATE_IN/_GATE_OUT):");
            else if (Convert.ToInt32(stanje["NadjenLog"]) == 0)
                poruka.AppendLine("U logu nije nađena izmena koja je upisala Konačni_GATE_OUT, pa se briše samo taj datum:");

            foreach (DataRow d in promene.Rows)
            {
                bool jeDatum = Convert.ToBoolean(d["JeDatum"]);
                poruka.AppendLine("- " + d["Polje"] + ": " + PrikaziVrednost(d["Sada"], jeDatum) + " → " + PrikaziVrednost(d["Postaje"], jeDatum));
            }

            if (pokreti.Rows.Count == 0)
                poruka.AppendLine(Convert.ToInt32(stanje["BrojPokreta"]) == 0
                    ? "- U izveštajima brodaru nema pokreta ovog zapisa."
                    : brojIzmena > 0
                        ? "- Poništena izmena nije upisala pokret u izveštaj brodaru, pa se izveštaji brodaru ne menjaju."
                        : "- Za poslednji pokret u izveštaju brodaru nije nađen datum koji ga je upisao, pa se izveštaji brodaru ne menjaju.");
            foreach (DataRow p in pokreti.Rows)
            {
                poruka.Append("- Iz izveštaja brodaru " + p["Izvestaj"] + " briše se pokret " + p["Pokret"] + " " + p["DatumPokreta"]
                    + " (upisao ga je " + p["Polje"] + ").");
                if (Convert.ToBoolean(p["Poslat"]))
                    poruka.Append(" Pokret je već poslat brodaru"
                        + (p["DatumIzvoza"] == DBNull.Value ? "" : " " + Convert.ToDateTime(p["DatumIzvoza"]).ToString(FormatDatuma))
                        + "; brodaru treba javiti.");
                poruka.AppendLine();
            }

            if (brojIzmena > 0)
                poruka.AppendLine(brojIzmena == 1
                    ? "- Poništena izmena se briše iz loga; red VRAĆANJE u logu beleži šta je vraćeno."
                    : "- Poništene izmene se brišu iz loga; red VRAĆANJE u logu beleži šta je vraćeno.");
            if (uTerminalu > 0)
            {
                poruka.AppendLine();
                poruka.AppendLine("Pažnja: kontejner " + kontejner + " je već u terminalu (zapisa: " + uTerminalu + ").");
            }

            if (MessageBox.Show(poruka.ToString(), naslov, MessageBoxButtons.YesNo,
                poslat || uTerminalu > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            try
            {
                ins.VratiIzArhive(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, naslov, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Vraceno++;

            // red se uklanja iz grida bez ponovnog učitavanja, pa filteri u arhivi ostaju
            foreach (DataRow red in dt.Select("ID = " + id))
                red.Delete();
            dt.AcceptChanges();
            Text = "Arhivirani podaci (" + dt.Rows.Count + ")";

            MessageBox.Show("Kontejner " + kontejner + " je vraćen u terminal.", naslov, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Vrednost iz provere vraćanja: datum je tekst stilom 120 (2026-11-23 12:51:00), prazno je NULL
        private static string PrikaziVrednost(object vrednost, bool jeDatum)
        {
            if (vrednost == null || vrednost == DBNull.Value)
                return "(prazno)";

            string tekst = Convert.ToString(vrednost);
            DateTime datum;
            if (jeDatum && DateTime.TryParseExact(tekst, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out datum))
                return datum.ToString(FormatDatuma);
            return tekst.Length == 0 ? "\"\"" : tekst;
        }
    }
}
