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
            tabSplitterPage2.Leave += tabSplitterPage2_Leave;

            // Grid je samo za pregled: ne otvara se editor ćelija sa podacima (filter bar ostaje za kucanje)
            gridGroupingControl1.TableControlCurrentCellStartEditing += gridGroupingControl1_TableControlCurrentCellStartEditing;
        }

        private void gridGroupingControl1_TableControlCurrentCellStartEditing(object sender, GridTableControlCancelEventArgs e)
        {
            GridCurrentCell celija = e.TableControl.CurrentCell;
            GridTableCellType tip = e.TableControl.GetTableViewStyleInfo(celija.RowIndex, celija.ColIndex).TableCellIdentity.TableCellType;
            if (tip == GridTableCellType.RecordFieldCell || tip == GridTableCellType.AlternateRecordFieldCell)
                e.Inner.Cancel = true;
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
            kontrole["NALOGODAVAC/UVOZ"] = txtNalogodavac;
            kontrole["POSTUPAK/UVOZ"] = txtPostupak;
            kontrole["UVOZNIK"] = txtUvoznik;
            kontrole["BL/UVOZ"] = txtBlUvoz;
            kontrole["PLOMBA_UVOZ"] = txtPlombaUvoz;
            kontrole["VOZ/kamion"] = txtVoz;
            kontrole["STANJE"] = cboStanje;
            kontrole["GATE_IN_E/F"] = dtpGateInEF;
            kontrole["PREUZIMANJE_PUNOG/Razvoz"] = dtpPreuzimanjePunog;
            kontrole["VRAĆANJE_PRAZNOG/iz_Razvoza"] = dtpVracanjePraznog;
            kontrole["Konačni_GATE_OUT"] = dtpKonacniGateOut;
            kontrole["BOOKING/IZVOZ"] = txtBooking;
            kontrole["KLIJENT/IZVOZ"] = txtKlijent;
            kontrole["GATE_OUT_EMPTY/Utovar"] = dtpGateOutEmptyUtovar;
            kontrole["GATE_IN_FULL/sa_Utovara"] = dtpGateInFullUtovar;
            kontrole["GATE_IN/_GATE_OUT"] = cboGateInGateOut;
            kontrole["L/R"] = txtLR;
            kontrole["TARA"] = txtTara;
            kontrole["MAX_NOSIVOST_CNT"] = txtMax;
            kontrole["VOZILO/PREUZIMANJE"] = txtVozilo;
            kontrole["PLOMBA/IZVOZ"] = txtPlomba;
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

        // Zapis koji se prikazuje: poslednji kliknut red (tekući), ako je među označenima;
        // inače poslednji označen red, a ako nijedan nije označen, red tekuće ćelije
        private Record IzaberiZapis()
        {
            Record tekuci = gridGroupingControl1.Table.CurrentRecord;
            Record oznaceni = null;
            foreach (SelectedRecord sr in gridGroupingControl1.Table.SelectedRecords)
            {
                if (sr.Record == null)
                    continue;
                if (sr.Record == tekuci)
                    return tekuci;
                oznaceni = sr.Record;
            }
            return oznaceni ?? tekuci;
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

            ZapamtiVrednostiPolja();
        }

        // ---------------------------------------------------------------------------------
        // Nesačuvane promene u poljima: pitanje kada fokus napusti tabSplitterPage2
        // (grid, tabSplitterPage1, dugmad u panel2). Dugmad Sačuvaj novi, Promeni i Obriši su na
        // samoj stranici, pa klik na njih ne napušta stranicu i pitanje se ne postavlja.
        // ---------------------------------------------------------------------------------
        private readonly Dictionary<string, object> zapamceneVrednosti = new Dictionary<string, object>();
        private bool pitanjeUToku;

        private void ZapamtiVrednostiPolja()
        {
            zapamceneVrednosti.Clear();
            foreach (TerminalPrivPolje polje in insertTerminalPriv.Polja)
                zapamceneVrednosti[polje.Kolona] = VrednostIzKontrole(polje.Kolona);
        }

        private bool PoljaSuIzmenjena()
        {
            foreach (TerminalPrivPolje polje in insertTerminalPriv.Polja)
            {
                object zapamceno;
                if (!zapamceneVrednosti.TryGetValue(polje.Kolona, out zapamceno))
                    return false;   // polja još nisu popunjena iz reda
                if (!Equals(zapamceno, VrednostIzKontrole(polje.Kolona)))
                    return true;
            }
            return false;
        }

        private void tabSplitterPage2_Leave(object sender, EventArgs e)
        {
            if (pitanjeUToku || dt == null || !PoljaSuIzmenjena())
                return;

            pitanjeUToku = true;
            try
            {
                int id = IzabraniId();
                string pitanje = id > 0
                    ? "Polja zapisa ID " + id + " su izmenjena, a nije kliknuto 'Promeni'. Da li želite da sačuvate promene?"
                    : "Uneti podaci nisu sačuvani. Da li želite da ih sačuvate kao novi zapis?";

                if (MessageBox.Show(pitanje, "Nesačuvane promene", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (!SacuvajIzPoljaBezOsvezavanja(id))
                    {
                        // čuvanje nije uspelo (poruka je već prikazana): vrati korisnika na polja
                        BeginInvoke(new Action(() => txtKontejner.Focus()));
                        return;
                    }
                }
                else
                {
                    OsveziPolja();   // odbaci promene: vrati vrednosti izabranog reda
                }
            }
            finally
            {
                pitanjeUToku = false;
            }
        }

        // Čuva vrednosti iz polja (izmena postojećeg ili novi zapis) i ažurira red u tabeli bez ponovnog
        // učitavanja grida, jer se poziva dok grid obrađuje klik koji je pomerio fokus
        private bool SacuvajIzPoljaBezOsvezavanja(int id)
        {
            DataRow red = RedIzPolja(id);
            string greska = ProveriRed(red);
            if (greska != null)
            {
                MessageBox.Show(greska, "Nesačuvane promene", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                if (id > 0)
                    new insertTerminalPriv().UpdTerminalPriv(red, true);   // isto kao Promeni
                else
                    red["ID"] = new insertTerminalPriv().InsTerminalPriv(red);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Nesačuvane promene", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            DataRow[] nadjeni = id > 0 ? dt.Select("ID = " + id) : new DataRow[0];
            DataRow uTabeli = nadjeni.Length > 0 ? nadjeni[0] : null;
            if (uTabeli == null)
            {
                dt.Rows.Add(red);
                red.AcceptChanges();
            }
            else
            {
                if (uTabeli.HasVersion(DataRowVersion.Proposed))
                    uTabeli.EndEdit();
                bool imaDrugihIzmena = uTabeli.RowState != DataRowState.Unchanged;

                foreach (TerminalPrivPolje polje in insertTerminalPriv.Polja)
                    uTabeli[polje.Kolona] = red[polje.Kolona];

                // red sa drugim nesačuvanim izmenama iz grida ostaje za 'Sačuvaj izmene'
                if (!imaDrugihIzmena)
                    uTabeli.AcceptChanges();
            }

            ZapamtiVrednostiPolja();
            return true;
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
                new insertTerminalPriv().UpdTerminalPriv(red, true);
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
                var dataAdapter = new SqlDataAdapter("SELECT ID, " + insertTerminalPriv.SqlKolone() + " FROM TerminalPriv ORDER BY ID DESC", s_connection);
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
            PovezFiltere();
            OsveziPolja();
        }

        // Filter kao u Excel-u (padajuća lista sa vrednostima u zaglavlju kolone) i dinamički filter u filter baru
        // (polje za kucanje). Grid pri svakom novom DataSource pravi kolone iznova, pa se filteri posle svakog
        // učitavanja ponovo povezuju; prethodno povezivanje se prvo uklanja da se ne gomila.
        private Syncfusion.GridHelperClasses.GridExcelFilter gridExcelFilter;
        private Syncfusion.GridHelperClasses.GridDynamicFilter dynamicFilter;

        private void PovezFiltere()
        {
            if (gridExcelFilter != null)
                gridExcelFilter.UnWireGrid(this.gridGroupingControl1);
            if (dynamicFilter != null)
                dynamicFilter.UnWireGrid(this.gridGroupingControl1);

            gridExcelFilter = new Syncfusion.GridHelperClasses.GridExcelFilter();
            gridExcelFilter.WireGrid(this.gridGroupingControl1);

            dynamicFilter = new Syncfusion.GridHelperClasses.GridDynamicFilter();
            dynamicFilter.WireGrid(this.gridGroupingControl1);
        }

        private void PodesiKolone()
        {
            GridColumnDescriptorCollection kolone = gridGroupingControl1.TableDescriptor.Columns;

            kolone["ID"].Width = SirinaKolone(gridGroupingControl1.Font, kolone["ID"].HeaderText, "00000");

            foreach (TerminalPrivPolje polje in insertTerminalPriv.Polja)
            {
                GridColumnDescriptor kolona = kolone[polje.Kolona];
                if (kolona == null)
                    continue;

                kolona.Width = SirinaKolone(gridGroupingControl1.Font, kolona.HeaderText, PrimerSadrzaja(polje));

                if (polje.Tip == TerminalPrivTip.Datum)
                {
                    kolona.Appearance.AnyRecordFieldCell.CellValueType = typeof(DateTime);
                    kolona.Appearance.AnyRecordFieldCell.Format = FormatDatuma;
                }
            }

            // Grid je samo za pregled: podaci se menjaju kroz polja na tabSplitterPage2 i Grupnu promenu
            foreach (GridColumnDescriptor kolona in kolone)
                kolona.Appearance.AnyRecordFieldCell.ReadOnly = true;
        }

        // Najduži sadržaj kolone po kome se računa širina: datum, najduža dozvoljena vrednost ili
        // onoliko velikih slova koliko polje ima karaktera (nvarchar(25) -> 25 karaktera)
        internal static string PrimerSadrzaja(TerminalPrivPolje polje)
        {
            if (polje.Tip == TerminalPrivTip.Datum)
                return "88.88.8888 88:88";

            if (polje.Dozvoljeno != null)
                return polje.Dozvoljeno.OrderByDescending(v => v.Length).First();

            return new string('A', polje.Velicina);
        }

        // Širina kolone: dovoljna za ceo naslov (u boldu, sa mestom za ikonu sortiranja/filtera) i za sadržaj,
        // ali ograničena da najduža polja (NAPOMENA, OPIS) ne zauzmu ceo grid
        internal static int SirinaKolone(Font font, string naslov, string sadrzaj)
        {
            const int NajvecaSirinaSadrzaja = 260;
            const int DodatakZaNaslov = 32;
            const int DodatakZaSadrzaj = 24;   // margine ćelije i dugme padajuće liste

            using (var fontNaslova = new Font(font, FontStyle.Bold))
            {
                int sirinaNaslova = TextRenderer.MeasureText(naslov ?? "", fontNaslova, Size.Empty, TextFormatFlags.NoPadding).Width + DodatakZaNaslov;
                int sirinaSadrzaja = TextRenderer.MeasureText(sadrzaj, font, Size.Empty, TextFormatFlags.NoPadding).Width + DodatakZaSadrzaj;
                return Math.Max(sirinaNaslova, Math.Min(sirinaSadrzaja, NajvecaSirinaSadrzaja));
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

            if (gridGroupingControl1.Table.SelectedRecords.Count == 0)
            {
                MessageBox.Show("Označite redove u tabeli: klik označava red, Shift+klik opseg od reda do reda, Ctrl+klik dodaje ili uklanja pojedinačan red.",
                    "Grupna promena", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var forma = new frmTerminalPrivremeniPromeni(gridGroupingControl1))
            {
                forma.ShowDialog(this);
            }

            OsveziPolja();
        }

        // ---------------------------------------------------------------------------------
        // Zakaci slike
        // ---------------------------------------------------------------------------------
        // ID i kontejner izabranog zapisa; zapis bez ID-a (nov, nesačuvan) nema folder pa se ne prihvata
        private bool IzaberiSacuvanZapis(string naslov, out int id, out string kontejner)
        {
            Record zapis = IzaberiZapis();
            object idVrednost = zapis == null ? null : zapis.GetValue("ID");
            id = idVrednost == null || idVrednost == DBNull.Value ? 0 : Convert.ToInt32(idVrednost);
            kontejner = "";

            if (id <= 0)
            {
                MessageBox.Show("Izaberite sačuvan zapis u tabeli. Novi zapis se prvo mora sačuvati da bi dobio ID.",
                    naslov, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (!IzaberiSacuvanZapis("Zakači slike", out id, out kontejner))
                return;

            using (var forma = new frmTerminalPrivremeniSlike(id, kontejner))
            {
                forma.ShowDialog(this);
                if (forma.Promenjeno)
                    PrimeniBrojSlika(id, forma.BrojSlika);
            }
        }

        // ---------------------------------------------------------------------------------
        // Arhiviraj / Arhivirani podaci
        // ---------------------------------------------------------------------------------
        private void btnArhiviraj_Click(object sender, EventArgs e)
        {
            if (dt == null)
                return;

            if (MessageBox.Show("Da li ste sigurni da želite da arhivirate podatke?", "Arhiviraj",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            // Grid se posle arhiviranja ponovo učitava iz baze
            if (!PotvrdiOsvezavanje())
                return;

            int arhivirano;
            try
            {
                arhivirano = new insertTerminalPriv().InsTerminalPrivArhiv();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Arhiviraj", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UcitajPodatke();
            MessageBox.Show("Arhivirano zapisa: " + arhivirano, "Arhiviraj", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ---------------------------------------------------------------------------------
        // Izvoz Maersk: pokreti iz TerminalPrivMaersk koji još nisu izvezeni
        // ---------------------------------------------------------------------------------
        private void btnIzvozMaersk_Click(object sender, EventArgs e)
        {
            var izvoz = new TerminalPrivMaerskIzvoz(Saobracaj.Sifarnici.frmLogovanje.connectionString);

            DataTable zapisi;
            try
            {
                zapisi = izvoz.UcitajNeizvezene();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Neuspešno čitanje Maersk zapisa: " + ex.Message, "Izvoz Maersk", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (zapisi.Rows.Count == 0)
            {
                MessageBox.Show("Nema novih Maersk zapisa za izvoz.", "Izvoz Maersk", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string putanja;
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel (*.xlsx)|*.xlsx";
                sfd.FileName = TerminalPrivMaerskIzvoz.PredlogImena();
                if (sfd.ShowDialog(this) != DialogResult.OK)
                    return;
                putanja = sfd.FileName;
            }

            int izvezeno;
            Cursor = Cursors.WaitCursor;
            try
            {
                izvezeno = izvoz.Izvezi(zapisi, putanja);
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Fajl nije sačuvan, a zapisi nisu označeni kao izvezeni. Zatvorite fajl u Excelu i pokušajte ponovo.", "Izvoz Maersk", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Izvoz nije uspeo: " + ex.Message, "Izvoz Maersk", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            if (MessageBox.Show("Izvezeno zapisa: " + izvezeno + ". Otvoriti fajl?", "Izvoz Maersk", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(putanja) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fajl ne može da se otvori: " + ex.Message, "Izvoz Maersk", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnArhiviraniPodaci_Click(object sender, EventArgs e)
        {
            using (var forma = new frmTerminalPrivremeniArhiva())
            {
                forma.ShowDialog(this);
            }
        }

        private void btnPregledLoga_Click(object sender, EventArgs e)
        {
            int id;
            string kontejner;
            if (!IzaberiSacuvanZapis("Pregled loga", out id, out kontejner))
                return;

            using (var forma = new frmTerminalPrivremeniLog(id, kontejner))
            {
                forma.ShowDialog(this);
            }
        }

        private void btnZakaciDokumenta_Click(object sender, EventArgs e)
        {
            int id;
            string kontejner;
            if (!IzaberiSacuvanZapis("Zakači dokumenta", out id, out kontejner))
                return;

            using (var forma = new frmTerminalPrivremeniDokumenta(id, kontejner))
            {
                forma.ShowDialog(this);
            }
        }

        // Baza je već ažurirana (updTerminalPrivPoslateSlike), pa se vrednost samo prikazuje u gridu bez
        // označavanja reda kao izmenjenog. Red sa drugim nesačuvanim izmenama ostaje izmenjen.
        private void PrimeniBrojSlika(int id, int broj)
        {
            DataRow[] redovi = dt.Select("ID = " + id);
            if (redovi.Length == 0)
                return;

            DataRow red = redovi[0];
            if (red.HasVersion(DataRowVersion.Proposed))
                red.EndEdit();

            bool imaDrugihIzmena = red.RowState == DataRowState.Modified;
            red["POSLATE SLIKE"] = broj.ToString();
            if (!imaDrugihIzmena)
                red.AcceptChanges();

            OsveziPolja();
        }
    }
}
