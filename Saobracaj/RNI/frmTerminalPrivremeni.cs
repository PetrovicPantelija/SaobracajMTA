using Syncfusion.Grouping;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Saobracaj.RNI
{
    public partial class frmTerminalPrivremeni : Form
    {
        private const string FormatDatuma = "dd.MM.yyyy HH:mm";

        private DataTable dt;
        private readonly Dictionary<string, Control> kontrole = new Dictionary<string, Control>();

        public frmTerminalPrivremeni()
        {
            InitializeComponent();
            PovezKontrole();
        }

        private void frmTerminalPrivremeni_Load(object sender, EventArgs e)
        {
            UcitajPodatke();
        }

        // ---------------------------------------------------------------------------------
        // Kontrole na tabSplitterPage2
        // ---------------------------------------------------------------------------------
        private void PovezKontrole()
        {
            kontrole["KONTEJNER"] = txtKontejner;
            kontrole["STATUS"] = cboStatus;
            kontrole["POZICIJA"] = txtPozicija;
            kontrole["VRSTA"] = txtVrsta;
            kontrole["BRODAR"] = txtBrodar;
            kontrole["NALOGODAVAC"] = txtNalogodavac;
            kontrole["POSTUPAK"] = txtPostupak;
            kontrole["UVOZNIK"] = txtUvoznik;
            kontrole["PLOMBA_UVOZ"] = txtPlombaUvoz;
            kontrole["VOZ"] = txtVoz;
            kontrole["STANJE"] = cboStanje;
            kontrole["GATE_IN_E/F"] = dtpGateInEF;
            kontrole["PREUZIMANJE_PUNOG"] = dtpPreuzimanjePunog;
            kontrole["VRAĆANJE_PRAZNOG"] = dtpVracanjePraznog;
            kontrole["Konačni_GATE_OUT"] = dtpKonacniGateOut;
            kontrole["BOOKING"] = txtBooking;
            kontrole["KLIJENT"] = txtKlijent;
            kontrole["GATE_OUT_EMPTY Utovar"] = dtpGateOutEmptyUtovar;
            kontrole["GATE_IN_FULL Utovar"] = dtpGateInFullUtovar;
            kontrole["GATE_IN/GATE_OUT"] = cboGateInGateOut;
            kontrole["L/R"] = txtLR;
            kontrole["TARA"] = txtTara;
            kontrole["MAX"] = txtMax;
            kontrole["VOZILO"] = txtVozilo;
            kontrole["PLOMBA"] = txtPlomba;
            kontrole["NAPOMENA"] = txtNapomena;
            kontrole["OPIS"] = txtOpis;
            kontrole["OTPREMA"] = txtOtprema;
            kontrole["POSLATE SLIKE"] = txtPoslateSlike;
            kontrole["Prevoznik"] = txtPrevoznik;

            foreach (TerminalPrivPolje polje in insertTerminalPriv.Polja)
            {
                ComboBox cbo = kontrole[polje.Kolona] as ComboBox;
                if (cbo != null)
                    PopuniCombo(cbo, polje.Dozvoljeno);
            }
        }

        private static void PopuniCombo(ComboBox cbo, string[] vrednosti)
        {
            cbo.Items.Clear();
            cbo.Items.Add("");   // prazno = bez vrednosti (NULL)
            cbo.Items.AddRange(vrednosti);
        }

        private object VrednostIzKontrole(string kolona)
        {
            Control kontrola = kontrole[kolona];

            DateTimePicker dtp = kontrola as DateTimePicker;
            if (dtp != null)
                return dtp.Checked ? (object)dtp.Value : DBNull.Value;

            string tekst = kontrola.Text.Trim();
            return tekst.Length == 0 ? (object)DBNull.Value : tekst;
        }

        // Zapis koji se prikazuje: oznaceni red (klik na zaglavlje reda), inace red tekuce celije
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

        // Izabrani zapis grida se prikazuje u kontrolama
        private void OsveziPolja()
        {
            Record zapis = IzaberiZapis();

            object id = zapis == null ? null : zapis.GetValue("ID");
            txtID.Text = id == null || id == DBNull.Value || Convert.ToInt32(id) < 0 ? "" : id.ToString();

            foreach (TerminalPrivPolje polje in insertTerminalPriv.Polja)
            {
                object vrednost = zapis == null ? null : zapis.GetValue(polje.Kolona);
                bool prazno = vrednost == null || vrednost == DBNull.Value;
                Control kontrola = kontrole[polje.Kolona];

                DateTimePicker dtp = kontrola as DateTimePicker;
                if (dtp != null)
                {
                    dtp.Checked = !prazno;
                    if (!prazno)
                        dtp.Value = Convert.ToDateTime(vrednost);
                    continue;
                }

                ComboBox cbo = kontrola as ComboBox;
                if (cbo != null)
                {
                    cbo.SelectedIndex = prazno ? 0 : Math.Max(0, cbo.FindStringExact(vrednost.ToString()));
                    continue;
                }

                kontrola.Text = prazno ? "" : vrednost.ToString();
            }
        }

        // ---------------------------------------------------------------------------------
        // Sacuvaj novi / Promeni / Obrisi (rade nad vrednostima iz polja na tabSplitterPage2)
        // ---------------------------------------------------------------------------------
        // Red sa vrednostima iz polja; id > 0 samo pri izmeni postojeceg zapisa
        private DataRow RedIzPolja(int id)
        {
            DataRow red = dt.NewRow();
            if (id > 0)
                red["ID"] = id;

            foreach (TerminalPrivPolje polje in insertTerminalPriv.Polja)
                red[polje.Kolona] = VrednostIzKontrole(polje.Kolona);

            return red;
        }

        // ID zapisa koji je prikazan u poljima; 0 ako nijedan zapis nije izabran
        private int IzabraniId()
        {
            int id;
            return int.TryParse(txtID.Text, out id) && id > 0 ? id : 0;
        }

        // Osvezavanje grida odbacuje nesacuvane izmene u tabeli, pa se prvo pita korisnik
        private bool PotvrdiOsvezavanje()
        {
            if (dt == null || dt.GetChanges() == null)
                return true;

            return MessageBox.Show("U tabeli postoje nesačuvane izmene koje će biti izgubljene osvežavanjem. Nastaviti?",
                "Terminal privremeni", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        private void btnSacuvajNovi_Click(object sender, EventArgs e)
        {
            if (dt == null || !PotvrdiOsvezavanje())
                return;

            DataRow red = RedIzPolja(0);
            string greska = ProveriRed(red);
            if (greska != null)
            {
                MessageBox.Show(greska, "Sačuvaj novi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                new insertTerminalPriv().InsTerminalPriv(red);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sačuvaj novi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UcitajPodatke();
        }

        private void btnPromeni_Click(object sender, EventArgs e)
        {
            int id = IzabraniId();
            if (dt == null || id == 0)
            {
                MessageBox.Show("Izaberite zapis u tabeli koji se menja.", "Promeni", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!PotvrdiOsvezavanje())
                return;

            DataRow red = RedIzPolja(id);
            string greska = ProveriRed(red);
            if (greska != null)
            {
                MessageBox.Show(greska, "Promeni", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                new insertTerminalPriv().UpdTerminalPriv(red);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Promeni", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UcitajPodatke();
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            int id = IzabraniId();
            if (dt == null || id == 0)
            {
                MessageBox.Show("Izaberite zapis u tabeli koji se briše.", "Obriši", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Obrisati zapis ID " + id + " (" + txtKontejner.Text + ")?", "Obriši",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            if (!PotvrdiOsvezavanje())
                return;

            try
            {
                new insertTerminalPriv().DelTerminalPriv(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Obriši", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UcitajPodatke();
        }

        private void gridGroupingControl1_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            OsveziPolja();
        }

        private void gridGroupingControl1_SelectedRecordsChanged(object sender, SelectedRecordsChangedEventArgs e)
        {
            OsveziPolja();
        }

        private void gridGroupingControl1_TableControlCurrentCellChanged(object sender, GridTableControlEventArgs e)
        {
            OsveziPolja();
        }

        // ---------------------------------------------------------------------------------
        // Ucitavanje i prikaz podataka
        // ---------------------------------------------------------------------------------
        private void UcitajPodatke()
        {
            try
            {
                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                var dataAdapter = new SqlDataAdapter("SELECT * FROM TerminalPriv ORDER BY ID DESC", s_connection);
                dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                var tabela = new DataTable("TerminalPriv");
                dataAdapter.Fill(tabela);

                // Novi zapisi dobijaju privremeni negativan ID dok se ne upisu u bazu
                DataColumn idKolona = tabela.Columns["ID"];
                idKolona.ReadOnly = false;
                idKolona.AutoIncrement = true;
                idKolona.AutoIncrementSeed = -1;
                idKolona.AutoIncrementStep = -1;

                dt = tabela;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Neuspešno učitavanje podataka: " + ex.Message, "Terminal privremeni",
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
            OsveziPolja();
        }

        private void PodesiKolone()
        {
            GridColumnDescriptorCollection kolone = gridGroupingControl1.TableDescriptor.Columns;

            kolone["ID"].Appearance.AnyRecordFieldCell.ReadOnly = true;

            foreach (TerminalPrivPolje polje in insertTerminalPriv.Polja)
            {
                GridColumnDescriptor kolona = kolone[polje.Kolona];
                if (kolona == null)
                    continue;

                if (polje.Tip == TerminalPrivTip.Datum)
                {
                    kolona.Appearance.AnyRecordFieldCell.CellValueType = typeof(DateTime);
                    kolona.Appearance.AnyRecordFieldCell.Format = FormatDatuma;
                }
                else if (polje.Dozvoljeno != null)
                {
                    kolona.Appearance.AnyRecordFieldCell.CellType = GridCellTypeName.ComboBox;
                    kolona.Appearance.AnyRecordFieldCell.DropDownStyle = GridDropDownStyle.Exclusive;
                    var izbor = new System.Collections.Specialized.StringCollection();
                    izbor.Add("");
                    izbor.AddRange(polje.Dozvoljeno);
                    kolona.Appearance.AnyRecordFieldCell.ChoiceList = izbor;
                }
            }
        }

        // ---------------------------------------------------------------------------------
        // Validacija
        // ---------------------------------------------------------------------------------
        // Vraca opis greske ili null ako je red ispravan. Vrednosti sa ogranicenom listom se
        // upisuju u tacnoj (kanonskoj) varijanti.
        private string ProveriRed(DataRow red)
        {
            var greske = new List<string>();

            string kontejner = red["KONTEJNER"] == DBNull.Value ? "" : red["KONTEJNER"].ToString().Trim();
            if (kontejner.Length != insertTerminalPriv.DuzinaKontejnera)
                greske.Add("KONTEJNER mora imati tačno " + insertTerminalPriv.DuzinaKontejnera + " karaktera");

            foreach (TerminalPrivPolje polje in insertTerminalPriv.Polja)
            {
                if (polje.Tip != TerminalPrivTip.Tekst || red[polje.Kolona] == DBNull.Value)
                    continue;

                string vrednost = red[polje.Kolona].ToString().Trim();
                if (vrednost.Length == 0)
                    continue;

                if (vrednost.Length > polje.Velicina)
                    greske.Add(polje.Kolona + " može imati najviše " + polje.Velicina + " karaktera");

                if (polje.Dozvoljeno != null)
                {
                    string tacna = polje.Dozvoljeno.FirstOrDefault(d => string.Equals(d, vrednost, StringComparison.CurrentCultureIgnoreCase));
                    if (tacna == null)
                        greske.Add(polje.Kolona + " ima nedozvoljenu vrednost '" + vrednost + "'");
                    else if (!string.Equals(tacna, red[polje.Kolona].ToString(), StringComparison.Ordinal))
                        red[polje.Kolona] = tacna;
                }
            }

            return greske.Count == 0 ? null : string.Join("; ", greske);
        }

        private static string OpisReda(DataRow red)
        {
            string kontejner = red["KONTEJNER"] == DBNull.Value ? "" : red["KONTEJNER"].ToString();
            int id = Convert.ToInt32(red["ID"]);
            return id > 0 ? "ID " + id + " (" + kontejner + ")" : "novi red (" + kontejner + ")";
        }

        // ---------------------------------------------------------------------------------
        // Sacuvaj izmene
        // ---------------------------------------------------------------------------------
        private void btnSacuvajIzmene_Click(object sender, EventArgs e)
        {
            if (dt == null)
                return;

            // Ćelija u izmeni se potvrđuje da bi vrednost stigla u DataTable
            GridCurrentCell tekucaCelija = gridGroupingControl1.TableControl.CurrentCell;
            if (tekucaCelija.IsEditing && !tekucaCelija.ConfirmChanges(true))
            {
                MessageBox.Show("Vrednost u ćeliji koja se menja nije ispravna.", "Terminal privremeni",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Red koji je u izmeni (grid ili polja na tabSplitterPage2) vodi se kao Unchanged
            // dok se izmena ne zatvori, pa se sve započete izmene zatvaraju pre skupljanja izmenjenih redova
            foreach (DataRow red in dt.Rows)
            {
                if (red.RowState != DataRowState.Deleted && red.HasVersion(DataRowVersion.Proposed))
                    red.EndEdit();
            }

            List<DataRow> izmenjeni =dt.Rows.Cast<DataRow>().Where(r => r.RowState != DataRowState.Unchanged).ToList();
            if (izmenjeni.Count == 0)
            {
                MessageBox.Show("Nema izmena za čuvanje.", "Terminal privremeni", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var greske = new StringBuilder();
            foreach (DataRow red in izmenjeni.Where(r => r.RowState != DataRowState.Deleted))
            {
                string greska = ProveriRed(red);
                if (greska != null)
                    greske.AppendLine(OpisReda(red) + ": " + greska);
            }

            if (greske.Length > 0)
            {
                MessageBox.Show("Izmene nisu sačuvane, ispravite sledeće:" + Environment.NewLine + greske,
                    "Terminal privremeni", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var ins = new insertTerminalPriv();
            int sacuvano = 0;
            try
            {
                foreach (DataRow red in izmenjeni)
                {
                    if (red.RowState == DataRowState.Deleted)
                    {
                        int id = Convert.ToInt32(red["ID", DataRowVersion.Original]);
                        if (id > 0)
                            ins.DelTerminalPriv(id);
                    }
                    else if (red.RowState == DataRowState.Added)
                    {
                        red["ID"] = ins.InsTerminalPriv(red);
                    }
                    else
                    {
                        ins.UpdTerminalPriv(red);
                    }

                    red.AcceptChanges();
                    sacuvano++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sačuvano zapisa: " + sacuvano + " od " + izmenjeni.Count + Environment.NewLine + ex.Message,
                    "Terminal privremeni", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Izmene su sačuvane (" + sacuvano + ").", "Terminal privremeni", MessageBoxButtons.OK, MessageBoxIcon.Information);
            UcitajPodatke();
        }

        // ---------------------------------------------------------------------------------
        // Uvoz Excel
        // ---------------------------------------------------------------------------------

        private void btnUvozExcel_Click(object sender, EventArgs e)
        {
            if (dt != null && dt.GetChanges() != null)
            {
                MessageBox.Show("Postoje nesačuvane izmene. Prvo kliknite 'Sačuvaj izmene'.", "Uvoz Excel",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var forma = new frmTerminalPrivremeniExcel())
            {
                forma.ShowDialog(this);
                if (forma.Uvezeno > 0)
                    UcitajPodatke();
            }
        }

        // ---------------------------------------------------------------------------------
        // Grupna promena
        // ---------------------------------------------------------------------------------
        private void btnGrupnaPromena_Click(object sender, EventArgs e)
        {
            if (dt == null)
                return;

            var oznaceni = new List<Record>();
            foreach (SelectedRecord sr in gridGroupingControl1.Table.SelectedRecords)
            {
                if (sr.Record != null)
                    oznaceni.Add(sr.Record);
            }

            if (oznaceni.Count == 0)
            {
                MessageBox.Show("Označite redove u tabeli (klik na zaglavlje reda, Ctrl/Shift za više redova).",
                    "Grupna promena", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            TerminalPrivPolje polje;
            object vrednost;
            if (!PitajGrupnuPromenu(oznaceni.Count, out polje, out vrednost))
                return;

            foreach (Record zapis in oznaceni)
            {
                zapis.SetValue(polje.Kolona, vrednost);
            }

            OsveziPolja();
            MessageBox.Show("Promenjeno zapisa: " + oznaceni.Count + ". Za upis u bazu kliknite 'Sačuvaj izmene'.",
                "Grupna promena", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool PitajGrupnuPromenu(int brojZapisa, out TerminalPrivPolje polje, out object vrednost)
        {
            polje = null;
            vrednost = null;

            // KONTEJNER je jedinstven po zapisu, pa se ne menja grupno
            TerminalPrivPolje[] polja = insertTerminalPriv.Polja.Where(p => p.Kolona != "KONTEJNER").ToArray();

            using (var forma = new Form())
            {
                forma.Text = "Grupna promena (" + brojZapisa + " zapisa)";
                forma.FormBorderStyle = FormBorderStyle.FixedDialog;
                forma.StartPosition = FormStartPosition.CenterParent;
                forma.MaximizeBox = false;
                forma.MinimizeBox = false;
                forma.ClientSize = new Size(340, 150);

                var lblPolje = new Label { Text = "Polje", Location = new Point(12, 12), AutoSize = true };
                var cboPolje = new ComboBox { Location = new Point(12, 30), Width = 316, DropDownStyle = ComboBoxStyle.DropDownList };
                cboPolje.Items.AddRange(polja.Select(p => p.Kolona).ToArray());

                var lblVrednost = new Label { Text = "Nova vrednost (prazno = bez vrednosti)", Location = new Point(12, 60), AutoSize = true };
                var mesto = new Panel { Location = new Point(12, 78), Size = new Size(316, 24) };

                var btnOk = new Button { Text = "Primeni", DialogResult = DialogResult.OK, Location = new Point(172, 114), Width = 75 };
                var btnOtkazi = new Button { Text = "Otkaži", DialogResult = DialogResult.Cancel, Location = new Point(253, 114), Width = 75 };
                forma.AcceptButton = btnOk;
                forma.CancelButton = btnOtkazi;
                forma.Controls.AddRange(new Control[] { lblPolje, cboPolje, lblVrednost, mesto, btnOk, btnOtkazi });

                Control unos = null;
                cboPolje.SelectedIndexChanged += (s, ev) =>
                {
                    TerminalPrivPolje izabrano = polja[cboPolje.SelectedIndex];
                    mesto.Controls.Clear();

                    if (izabrano.Tip == TerminalPrivTip.Datum)
                    {
                        unos = new DateTimePicker
                        {
                            Format = DateTimePickerFormat.Custom,
                            CustomFormat = FormatDatuma,
                            ShowCheckBox = true,
                            Checked = false,
                            Width = 316
                        };
                    }
                    else if (izabrano.Dozvoljeno != null)
                    {
                        var cbo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 316 };
                        PopuniCombo(cbo, izabrano.Dozvoljeno);
                        cbo.SelectedIndex = 0;
                        unos = cbo;
                    }
                    else
                    {
                        unos = new TextBox { MaxLength = izabrano.Velicina, Width = 316 };
                    }
                    mesto.Controls.Add(unos);
                };
                cboPolje.SelectedIndex = 0;

                if (forma.ShowDialog(this) != DialogResult.OK)
                    return false;

                polje = polja[cboPolje.SelectedIndex];

                DateTimePicker dtp = unos as DateTimePicker;
                if (dtp != null)
                {
                    vrednost = dtp.Checked ? (object)dtp.Value : DBNull.Value;
                }
                else
                {
                    string tekst = unos.Text.Trim();
                    vrednost = tekst.Length == 0 ? (object)DBNull.Value : tekst;
                }

                string opis = vrednost == DBNull.Value ? "(prazno)" : (vrednost is DateTime ? ((DateTime)vrednost).ToString(FormatDatuma) : vrednost.ToString());
                return MessageBox.Show("Polje " + polje.Kolona + " će biti postavljeno na " + opis + " za " + brojZapisa + " zapisa. Nastaviti?",
                    "Grupna promena", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
            }
        }
    }
}
