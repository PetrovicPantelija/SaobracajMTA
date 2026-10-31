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

        // true posle dugmeta 'Novi': 'Promeni' tada upisuje novi zapis. Vraća se na false posle upisa
        // novog zapisa i kada se u gridu izabere zapis.
        private bool StatusZaUnosNovog = false;

        public frmTerminalPrivremeni()
        {
            InitializeComponent();
            PovezKontrole();
            tabSplitterPage2.Leave += tabSplitterPage2_Leave;

            cboIzvestaji.Items.AddRange(new object[] { "Izveštaji", "Izveštaj Maersk", "Izveštaj MSC", "Izveštaj CMA", "Izveštaj OOCL", "Izveštaj HMM", "Izveštaj HAPAG", "Izveštaj FLEXBOX", "Izveštaj COSCO" });
            cboIzvestaji.SelectedIndex = 0;
            SuziDugmadNaTraci();

            // desni klik na gridu: Poništi filtere (isto kao dugme na traci)
            var meni = new ContextMenuStrip();
            meni.Items.Add("Poništi filtere", null, (s, e) => PonistiFiltere());
            gridGroupingControl1.ContextMenuStrip = meni;

            // Grid je samo za pregled: ne otvara se editor ćelija sa podacima (filter bar ostaje za kucanje)
            gridGroupingControl1.TableControlCurrentCellStartEditing += gridGroupingControl1_TableControlCurrentCellStartEditing;
        }

        // Dugmad na traci (panel2) su široka koliko natpis: uz AutoSize sa GrowOnly širina iz dizajnera je
        // najmanja širina, pa su kratki natpisi imali mnogo praznog prostora. Lista izveštaja je široka
        // koliko najduža stavka ("Izveštaj FLEXBOX") i strelica.
        private void SuziDugmadNaTraci()
        {
            foreach (Button dugme in panel2.Controls.OfType<Button>())
            {
                dugme.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                dugme.Padding = new Padding(2, 0, 2, 0);
            }

            int najduza = cboIzvestaji.Items.Cast<object>()
                .Max(s => TextRenderer.MeasureText(s.ToString(), cboIzvestaji.Font).Width);
            cboIzvestaji.Width = najduza + SystemInformation.VerticalScrollBarWidth + 8;
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
            kontrole["VRSTA"] = cboVrsta;
            kontrole["BRODAR"] = cboBrodar;
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
            kontrole["BELEŠKA"] = txtBeleska;

            foreach (TerminalPrivPolje polje in insertTerminalPriv.Polja)
            {
                ComboBox cbo = kontrole[polje.Kolona] as ComboBox;
                if (cbo != null)
                    PopuniCombo(cbo, polje.Dozvoljeno);

                DateTimePicker dtp = kontrole[polje.Kolona] as DateTimePicker;
                if (dtp != null)
                    dtp.ValueChanged += (s, e) => OznaciDatum((DateTimePicker)s);
            }
        }

        // ---------------------------------------------------------------------------------
        // Kretanje strelicama između polja na tabSplitterPage2 (levo, desno, gore, dole po rasporedu polja)
        //   - polje za tekst: levo/desno pomeraju kursor u tekstu, a u drugo polje prelaze tek sa početka/kraja teksta
        //   - lista (STATUS, VRSTA...): sve četiri strelice prelaze u drugo polje; vrednost se bira kucanjem,
        //     mišem ili Alt+Dole; dok je lista otvorena strelice se kreću po listi
        //   - datum: strelice menjaju datum kao i ranije, a u drugo polje se prelazi sa Ctrl+strelica
        //   - Ctrl+strelica prelazi u drugo polje iz svakog polja
        // ---------------------------------------------------------------------------------
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys taster = keyData & Keys.KeyCode;
            Keys modifikator = keyData & Keys.Modifiers;
            if ((taster == Keys.Left || taster == Keys.Right || taster == Keys.Up || taster == Keys.Down)
                && (modifikator == Keys.None || modifikator == Keys.Control))
            {
                Control polje = FokusiranoPolje();
                if (polje != null && (modifikator == Keys.Control || StrelicaNapustaPolje(polje, taster)))
                {
                    Control sledece = SusednoPolje(polje, taster);
                    if (sledece != null)
                    {
                        sledece.Focus();
                        if (sledece is TextBox tb)
                            tb.SelectAll();
                    }
                    return true;   // i kad nema polja u tom pravcu: kursor ostaje u polju, vrednost se ne menja
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // Polje za unos (iz 'kontrole') koje ima fokus, ili null
        private Control FokusiranoPolje()
        {
            Control c = ActiveControl;
            while (c is ContainerControl cc && cc.ActiveControl != null)
                c = cc.ActiveControl;
            return c != null && kontrole.ContainsValue(c) ? c : null;
        }

        private static bool StrelicaNapustaPolje(Control polje, Keys taster)
        {
            if (polje is DateTimePicker)
                return false;

            if (polje is ComboBox cbo)
                return !cbo.DroppedDown;

            if (polje is TextBox tb)
            {
                if (tb.SelectionLength > 0 && tb.SelectionLength < tb.TextLength)
                    return taster == Keys.Up || taster == Keys.Down;
                if (taster == Keys.Left)
                    return tb.SelectionStart == 0 || tb.SelectionLength == tb.TextLength;
                if (taster == Keys.Right)
                    return tb.SelectionStart + tb.SelectionLength >= tb.TextLength;
                return true;
            }
            return false;
        }

        // Najbliže dostupno polje u pravcu strelice. Polja istog reda imaju (skoro) isti Top.
        // Levo/desno na kraju reda prelazi na kraj prethodnog / početak sledećeg reda.
        private Control SusednoPolje(Control polje, Keys taster)
        {
            const int IstiRed = 10;
            List<Control> dostupna = kontrole.Values
                .Where(c => c != polje && c.Visible && c.Enabled && c.Parent == polje.Parent)
                .ToList();

            Func<Control, int> centar = c => c.Left + c.Width / 2;
            int x = centar(polje), y = polje.Top;

            switch (taster)
            {
                case Keys.Right:
                    return dostupna.Where(c => Math.Abs(c.Top - y) <= IstiRed && c.Left > polje.Left).OrderBy(c => c.Left).FirstOrDefault()
                        ?? dostupna.Where(c => c.Top > y + IstiRed).OrderBy(c => c.Top).ThenBy(c => c.Left).FirstOrDefault();
                case Keys.Left:
                    return dostupna.Where(c => Math.Abs(c.Top - y) <= IstiRed && c.Left < polje.Left).OrderByDescending(c => c.Left).FirstOrDefault()
                        ?? dostupna.Where(c => c.Top < y - IstiRed).OrderByDescending(c => c.Top).ThenByDescending(c => c.Left).FirstOrDefault();
                case Keys.Down:
                    return dostupna.Where(c => c.Top > y + IstiRed).OrderBy(c => c.Top).ThenBy(c => Math.Abs(centar(c) - x)).FirstOrDefault();
                case Keys.Up:
                    return dostupna.Where(c => c.Top < y - IstiRed).OrderByDescending(c => c.Top).ThenBy(c => Math.Abs(centar(c) - x)).FirstOrDefault();
            }
            return null;
        }

        // Uneti (čekirani) datum se ispisuje boldovano
        private Font fontDatuma, fontUnetogDatuma;

        private void OznaciDatum(DateTimePicker dtp)
        {
            if (fontDatuma == null)
            {
                fontDatuma = dtp.Font;
                fontUnetogDatuma = new Font(dtp.Font, FontStyle.Bold);
            }

            Font font = dtp.Checked ? fontUnetogDatuma : fontDatuma;
            if (dtp.Font != font)
                dtp.Font = font;
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

        // Izabrani zapis grida se prikazuje u kontrolama; izbor zapisa prekida unos novog
        private void OsveziPolja()
        {
            StatusZaUnosNovog = false;
            PrikaziZapis(IzaberiZapis());
        }

        // Vrednosti zapisa u kontrolama; zapis null = prazna polja
        private void PrikaziZapis(Record zapis)
        {
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
                    OznaciDatum(dtp);
                    continue;
                }

                ComboBox cbo = kontrola as ComboBox;
                if (cbo != null)
                {
                    // vrednost koje nema u listi (stari podatak) se prikazuje, a ne briše; pri čuvanju je validacija
                    // prijavljuje kao nedozvoljenu, pa je korisnik mora izabrati iz liste
                    if (cbo.Items.Count != polje.Dozvoljeno.Length + 1)
                        PopuniCombo(cbo, polje.Dozvoljeno);
                    int indeks = prazno ? 0 : cbo.FindStringExact(vrednost.ToString());
                    if (indeks < 0)
                        indeks = cbo.Items.Add(vrednost.ToString());
                    cbo.SelectedIndex = indeks;
                    continue;
                }

                kontrola.Text = prazno ? "" : vrednost.ToString();
            }

            ZapamtiVrednostiPolja();
        }

        // ---------------------------------------------------------------------------------
        // Nesačuvane promene u poljima: pitanje kada fokus napusti tabSplitterPage2
        // (grid, tabSplitterPage1, dugmad u panel2, među njima Novi i Obriši). Dugme Promeni je na
        // samoj stranici, pa klik na njega ne napušta stranicu i pitanje se ne postavlja.
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

            int brojMaersk, brojMSC, brojCMA, brojOOCL, brojHMM, brojHAPAG, brojFLEXBOX, brojCOSCO;
            try
            {
                if (id > 0)
                    new insertTerminalPriv().UpdTerminalPriv(red, true, out brojMaersk, out brojMSC, out brojCMA, out brojOOCL, out brojHMM, out brojHAPAG, out brojFLEXBOX, out brojCOSCO);   // isto kao Promeni
                else
                    red["ID"] = new insertTerminalPriv().InsTerminalPriv(red, true, out brojMaersk, out brojMSC, out brojCMA, out brojOOCL, out brojHMM, out brojHAPAG, out brojFLEXBOX, out brojCOSCO);   // isto kao Promeni posle Novi
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Nesačuvane promene", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            PorukaPokreti(brojMaersk, brojMSC, brojCMA, brojOOCL, brojHMM, brojHAPAG, brojFLEXBOX, brojCOSCO);

            // upisan novi zapis: polja sada prikazuju njega, pa sledeće 'Promeni' menja taj zapis
            if (id <= 0)
            {
                txtID.Text = red["ID"].ToString();
                StatusZaUnosNovog = false;
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

        // Novi: prazna polja za unos novog zapisa; zapis se upisuje dugmetom 'Promeni'
        private void btnNovi_Click(object sender, EventArgs e)
        {
            if (dt == null)
                return;

            PrikaziZapis(null);
            StatusZaUnosNovog = true;
            txtKontejner.Focus();
        }

        // Promeni: posle 'Novi' upisuje novi zapis, inače menja izabrani
        private void btnPromeni_Click(object sender, EventArgs e)
        {
            if (StatusZaUnosNovog)
            {
                UpisiNovi();
                return;
            }

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

            int brojMaersk, brojMSC, brojCMA, brojOOCL, brojHMM, brojHAPAG, brojFLEXBOX, brojCOSCO;
            try
            {
                new insertTerminalPriv().UpdTerminalPriv(red, true, out brojMaersk, out brojMSC, out brojCMA, out brojOOCL, out brojHMM, out brojHAPAG, out brojFLEXBOX, out brojCOSCO);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Promeni", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            PorukaPokreti(brojMaersk, brojMSC, brojCMA, brojOOCL, brojHMM, brojHAPAG, brojFLEXBOX, brojCOSCO);
            UcitajPodatke();
        }

        private void UpisiNovi()
        {
            if (dt == null || !PotvrdiOsvezavanje())
                return;

            DataRow red = RedIzPolja(0);
            string greska = ProveriRed(red);
            if (greska != null)
            {
                MessageBox.Show(greska, "Novi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int brojMaersk, brojMSC, brojCMA, brojOOCL, brojHMM, brojHAPAG, brojFLEXBOX, brojCOSCO;
            try
            {
                new insertTerminalPriv().InsTerminalPriv(red, true, out brojMaersk, out brojMSC, out brojCMA, out brojOOCL, out brojHMM, out brojHAPAG, out brojFLEXBOX, out brojCOSCO);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Novi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            StatusZaUnosNovog = false;
            PorukaPokreti(brojMaersk, brojMSC, brojCMA, brojOOCL, brojHMM, brojHAPAG, brojFLEXBOX, brojCOSCO);
            UcitajPodatke();
        }

        // Štampaj potvrdu: sadržaj potvrde još nije definisan
        private void btnStampajPotvrdu_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Štampa potvrde još nije napravljena.", "Štampaj potvrdu", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Poruka samo kada su ispunjeni uslovi i pokret je upisan u TerminalPrivMaersk / TerminalPrivMSC /
        // TerminalPrivCMA / TerminalPrivOOCL / TerminalPrivHMM / TerminalPrivHAPAG / TerminalPrivFLEXBOX / TerminalPrivCOSCO
        private static void PorukaPokreti(int brojMaersk, int brojMSC, int brojCMA, int brojOOCL, int brojHMM, int brojHAPAG, int brojFLEXBOX, int brojCOSCO)
        {
            if (brojCOSCO > 0)
                MessageBox.Show("Upisaću u tabelu TerminalPrivCOSCO (broj pokreta: " + brojCOSCO + ").",
                    "COSCO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (brojFLEXBOX > 0)
                MessageBox.Show("Upisaću u tabelu TerminalPrivFLEXBOX (broj pokreta: " + brojFLEXBOX + ").",
                    "FLEXBOX", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (brojHAPAG > 0)
                MessageBox.Show("Upisaću u tabelu TerminalPrivHAPAG (broj pokreta: " + brojHAPAG + ").",
                    "HAPAG", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (brojHMM > 0)
                MessageBox.Show("Upisaću u tabelu TerminalPrivHMM (broj pokreta: " + brojHMM + ").",
                    "HMM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (brojOOCL > 0)
                MessageBox.Show("Upisaću u tabelu TerminalPrivOOCL (broj pokreta: " + brojOOCL + ").",
                    "OOCL", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (brojCMA > 0)
                MessageBox.Show("Upisaću u tabelu TerminalPrivCMA (broj pokreta: " + brojCMA + ").",
                    "CMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (brojMaersk > 0)
                MessageBox.Show("Upisaću u tabelu TerminalPrivMaersk (broj pokreta: " + brojMaersk + ").",
                    "Maersk", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (brojMSC > 0)
                MessageBox.Show("Upisaću u tabelu TerminalPrivMSC (broj pokreta: " + brojMSC + ").",
                    "MSC", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            int id = IzabraniId();
            if (dt == null || id == 0)
            {
                MessageBox.Show("Izaberite zapis u tabeli koji se briše.", "Obriši", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Da li ste sigurni da želite da obrišete zapis ID " + id + " (" + txtKontejner.Text + ")?", "Obriši",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
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

        // Poništava sve filtere u gridu: Excel filter (lista u zaglavlju), tekst u filter baru i ostale RecordFilters.
        // Sortiranje i grupisanje ostaju.
        private void btnPonistiFiltere_Click(object sender, EventArgs e)
        {
            PonistiFiltere();
        }

        private void PonistiFiltere()
        {
            if (gridExcelFilter != null)
                gridExcelFilter.ClearFilters(gridGroupingControl1);

            // tekst ukucan u filter baru se briše ćeliju po ćeliju (filter bar je u prvih nekoliko redova tabele)
            GridTableControl tabela = gridGroupingControl1.TableControl;
            int redova = Math.Min(tabela.Model.RowCount, 10);
            for (int r = 1; r <= redova; r++)
            {
                for (int k = 1; k <= tabela.Model.ColCount; k++)
                {
                    GridTableCellStyleInfo stil = tabela.GetTableViewStyleInfo(r, k);
                    if (stil.TableCellIdentity.TableCellType != GridTableCellType.FilterBarCell)
                        continue;
                    if (stil.CellModel is GridTableFilterBarCellModel model)
                        model.Select(stil.TableCellIdentity, 0);   // stavka 0 = (All)
                }
            }

            gridGroupingControl1.TableDescriptor.RecordFilters.Clear();
            tabela.Refresh();
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

                if (polje.Kolona == "VOZILO/PREUZIMANJE")
                    kolona.HeaderText = "VOZILO";

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
                    string tacna = insertTerminalPriv.DozvoljenaVrednost(polje, vrednost);
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
        // Izvoz Maersk / Izvoz MSC: pokreti iz TerminalPrivMaersk / TerminalPrivMSC koji još nisu izvezeni
        // ---------------------------------------------------------------------------------
        // Izveštaji: izbor u listi pokreće izveštaj, a lista se vraća na naslov 'Izveštaji'
        private void cboIzvestaji_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int izbor = cboIzvestaji.SelectedIndex;
            cboIzvestaji.SelectedIndex = 0;

            if (izbor == 1)
                IzvestajMaersk();
            else if (izbor == 2)
                IzvestajMSC();
            else if (izbor == 3)
                IzvestajCMA();
            else if (izbor == 4)
                IzvestajOOCL();
            else if (izbor == 5)
                IzvestajHMM();
            else if (izbor == 6)
                IzvestajHAPAG();
            else if (izbor == 7)
                IzvestajFLEXBOX();
            else if (izbor == 8)
                IzvestajCOSCO();
        }

        private void IzvestajCOSCO()
        {
            var izvoz = new TerminalPrivCOSCOIzvoz(Saobracaj.Sifarnici.frmLogovanje.connectionString);
            IzvozPokreta("COSCO", izvoz.UcitajNeizvezene, TerminalPrivCOSCOIzvoz.PredlogImena(), izvoz.Izvezi);
        }

        private void IzvestajFLEXBOX()
        {
            var izvoz = new TerminalPrivFLEXBOXIzvoz(Saobracaj.Sifarnici.frmLogovanje.connectionString);
            IzvozPokreta("FLEXBOX", izvoz.UcitajNeizvezene, TerminalPrivFLEXBOXIzvoz.PredlogImena(), izvoz.Izvezi);
        }

        private void IzvestajHAPAG()
        {
            var izvoz = new TerminalPrivHAPAGIzvoz(Saobracaj.Sifarnici.frmLogovanje.connectionString);
            IzvozPokreta("HAPAG", izvoz.UcitajNeizvezene, TerminalPrivHAPAGIzvoz.PredlogImena(), izvoz.Izvezi);
        }

        private void IzvestajHMM()
        {
            var izvoz = new TerminalPrivHMMIzvoz(Saobracaj.Sifarnici.frmLogovanje.connectionString);
            IzvozPokreta("HMM", izvoz.UcitajNeizvezene, TerminalPrivHMMIzvoz.PredlogImena(), izvoz.Izvezi);
        }

        private void IzvestajOOCL()
        {
            var izvoz = new TerminalPrivOOCLIzvoz(Saobracaj.Sifarnici.frmLogovanje.connectionString);
            IzvozPokreta("OOCL", izvoz.UcitajNeizvezene, TerminalPrivOOCLIzvoz.PredlogImena(), izvoz.Izvezi);
        }

        private void IzvestajCMA()
        {
            var izvoz = new TerminalPrivCMAIzvoz(Saobracaj.Sifarnici.frmLogovanje.connectionString);
            IzvozPokreta("CMA", izvoz.UcitajNeizvezene, TerminalPrivCMAIzvoz.PredlogImena(), izvoz.Izvezi);
        }

        private void IzvestajMaersk()
        {
            var izvoz = new TerminalPrivMaerskIzvoz(Saobracaj.Sifarnici.frmLogovanje.connectionString);
            IzvozPokreta("Maersk", izvoz.UcitajNeizvezene, TerminalPrivMaerskIzvoz.PredlogImena(), izvoz.Izvezi);
        }

        private void IzvestajMSC()
        {
            var izvoz = new TerminalPrivMSCIzvoz(Saobracaj.Sifarnici.frmLogovanje.connectionString);
            IzvozPokreta("MSC", izvoz.UcitajNeizvezene, TerminalPrivMSCIzvoz.PredlogImena(), izvoz.Izvezi);
        }

        private void IzvozPokreta(string brodar, Func<DataTable> ucitajNeizvezene, string predlogImena, Func<DataTable, string, int> izvezi)
        {
            string naslov = "Izvoz " + brodar;

            DataTable zapisi;
            try
            {
                zapisi = ucitajNeizvezene();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Neuspešno čitanje " + brodar + " zapisa: " + ex.Message, naslov, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (zapisi.Rows.Count == 0)
            {
                MessageBox.Show("Nema novih " + brodar + " zapisa za izvoz.", naslov, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string putanja;
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel (*.xlsx)|*.xlsx";
                sfd.FileName = predlogImena;
                if (sfd.ShowDialog(this) != DialogResult.OK)
                    return;
                putanja = sfd.FileName;
            }

            int izvezeno;
            Cursor = Cursors.WaitCursor;
            try
            {
                izvezeno = izvezi(zapisi, putanja);
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Fajl nije sačuvan, a zapisi nisu označeni kao izvezeni. Zatvorite fajl u Excelu i pokušajte ponovo.", naslov, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Izvoz nije uspeo: " + ex.Message, naslov, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            if (MessageBox.Show("Izvezeno zapisa: " + izvezeno + ". Otvoriti fajl?", naslov, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(putanja) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fajl ne može da se otvori: " + ex.Message, naslov, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        // ---------------------------------------------------------------------------------
        // Opšti izvoz: zapisi koji su u gridu posle filtera, sa poljima koja korisnik štiklira
        // ---------------------------------------------------------------------------------
        private static List<string> poljaOpstegIzvoza;   // poslednji izbor polja, pamti se dok je program otvoren

        private void btnOpstiIzvoz_Click(object sender, EventArgs e)
        {
            const string naslov = "Opšti izvoz";
            if (dt == null)
                return;

            // sva polja zapisa, redom kako su kolone u gridu (ako je neka kolona sakrivena, ide na kraj)
            GridTableDescriptor opis = gridGroupingControl1.TableDescriptor;
            var redosled = new List<string>();
            foreach (GridVisibleColumnDescriptor vidljiva in opis.VisibleColumns)
                redosled.Add(vidljiva.Name);
            foreach (GridColumnDescriptor kolona in opis.Columns)
                if (!redosled.Contains(kolona.Name))
                    redosled.Add(kolona.Name);

            var polja = new List<KeyValuePair<string, string>>();
            foreach (string ime in redosled)
            {
                GridColumnDescriptor kolona = opis.Columns[ime];
                if (kolona != null)
                    polja.Add(new KeyValuePair<string, string>(kolona.MappingName, string.IsNullOrEmpty(kolona.HeaderText) ? kolona.MappingName : kolona.HeaderText));
            }

            var zapisi = new List<Record>();
            foreach (Record rec in gridGroupingControl1.Table.FilteredRecords)
                zapisi.Add(rec);

            if (zapisi.Count == 0)
            {
                MessageBox.Show("Posle filtera u gridu nema zapisa za izvoz.", naslov, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<string> izabrana;
            using (var forma = new frmTerminalPrivremeniOpstiIzvoz(polja, poljaOpstegIzvoza, zapisi.Count))
            {
                if (forma.ShowDialog(this) != DialogResult.OK)
                    return;
                izabrana = forma.IzabranaPolja;
            }
            poljaOpstegIzvoza = izabrana;

            string putanja;
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel (*.xlsx)|*.xlsx";
                sfd.FileName = TerminalPrivOpstiIzvoz.PredlogImena();
                if (sfd.ShowDialog(this) != DialogResult.OK)
                    return;
                putanja = sfd.FileName;
            }

            var naslovi = izabrana.Select(k => polja.First(p => p.Key == k).Value).ToList();
            var redovi = zapisi.Select(rec => izabrana.Select(k => rec.GetValue(k)).ToArray()).ToList();

            Cursor = Cursors.WaitCursor;
            try
            {
                TerminalPrivOpstiIzvoz.SnimiExcel(naslovi, redovi, putanja);
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Fajl nije sačuvan. Zatvorite fajl u Excelu i pokušajte ponovo.", naslov, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Izvoz nije uspeo: " + ex.Message, naslov, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            if (MessageBox.Show("Izvezeno zapisa: " + redovi.Count + ". Otvoriti fajl?", naslov, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(putanja) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fajl ne može da se otvori: " + ex.Message, naslov, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
