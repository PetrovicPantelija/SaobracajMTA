using Saobracaj.Theme;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Saobracaj.Servis
{
    // Prijem (MD-R05): po narudžbenici (količina i cena mogu da se razlikuju) ili bez nje uz razlog.
    // Prijemnica i sve stavke se snimaju u jednoj transakciji.
    public class frmSERPrijem : Form
    {
        private readonly ComboBox cbNarudzbenica;
        private readonly DateTimePicker dtDatum;
        private readonly TextBox tbRazlog, tbNapomena;
        private readonly DataGridView dgv;
        private readonly Button dodaj, ukloni;
        private readonly DataTable stavke = new DataTable();

        public frmSERPrijem(int narudzbenicaID)
        {
            Text = "Prijem delova";
            FormBorderStyle = FormBorderStyle.Sizable;
            MinimizeBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            BackColor = SavaTheme.Surface;
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1000, 640);

            stavke.Columns.Add("NarudzbenicaStavkaID", typeof(int));
            stavke.Columns.Add("DeoID", typeof(int));
            stavke.Columns.Add("Sifra", typeof(string));
            stavke.Columns.Add("Naziv", typeof(string));
            stavke.Columns.Add("Naruceno", typeof(decimal));
            stavke.Columns.Add("Primljeno", typeof(decimal));
            stavke.Columns.Add("Kolicina", typeof(decimal));
            stavke.Columns.Add("Cena", typeof(decimal));

            var naslov = new SERTekst { Text = "Prijem delova", Dock = DockStyle.Top, Height = 48, Font = SavaTheme.FormTitleFont, Padding = new Padding(20, 0, 0, 0) };

            var polja = new SERPoljaPanel(3) { Padding = new Padding(20, 0, 0, 0) };
            cbNarudzbenica = polja.Dodaj("Narudžbenica", new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList });
            dtDatum = polja.Dodaj("Datum prijema", new DateTimePicker { Format = DateTimePickerFormat.Custom, CustomFormat = "dd.MM.yyyy" });
            tbRazlog = polja.Dodaj("Razlog prijema bez narudžbenice", new TextBox { MaxLength = 250 });
            tbNapomena = polja.Dodaj("Napomena", new TextBox { MaxLength = 500 }, 3);

            var traka = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 44, Padding = new Padding(20, 4, 0, 0), BackColor = SavaTheme.Surface };
            dodaj = SERStil.Dugme("Dodaj deo");
            ukloni = SERStil.Dugme("Ukloni stavku");
            dodaj.Click += (s, e) => DodajDeo();
            ukloni.Click += (s, e) => { if (dgv.CurrentRow != null && !dgv.CurrentRow.IsNewRow) dgv.Rows.Remove(dgv.CurrentRow); };
            traka.Controls.Add(dodaj);
            traka.Controls.Add(ukloni);

            dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.CellSelect,
                DataSource = stavke
            };
            Kolona("Sifra", "Šifra", 100, true, null);
            Kolona("Naziv", "Deo", 280, true, null);
            Kolona("Naruceno", "Naručeno", 90, true, "#,##0.###");
            Kolona("Primljeno", "Primljeno ranije", 110, true, "#,##0.###");
            Kolona("Kolicina", "Prima se", 100, false, "#,##0.###");
            Kolona("Cena", "Cena RSD", 110, false, "#,##0.00");
            dgv.DataError += (s, e) => { e.ThrowException = false; MessageBox.Show(this, "Upišite broj.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning); };
            var okvir = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 0, 20, 10), BackColor = SavaTheme.Surface };
            okvir.Controls.Add(dgv);

            var dno = new Panel { Dock = DockStyle.Bottom, Height = 60, BackColor = SavaTheme.White, Padding = new Padding(20, 14, 20, 14) };
            var sacuvaj = SERStil.Dugme("Proknjiži prijem", "primary");
            var otkazi = SERStil.Dugme("Otkaži");
            sacuvaj.Dock = DockStyle.Right;
            otkazi.Dock = DockStyle.Right;
            dno.Controls.Add(otkazi);
            dno.Controls.Add(new Panel { Dock = DockStyle.Right, Width = 8 });
            dno.Controls.Add(sacuvaj);
            sacuvaj.Click += (s, e) => Sacuvaj();
            otkazi.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            CancelButton = otkazi;

            Controls.Add(okvir);
            Controls.Add(traka);
            Controls.Add(polja);
            Controls.Add(dno);
            Controls.Add(naslov);
            ThemeHelper.Apply(this);
            ThemeHelper.StyleGrid(dgv);

            PuniNarudzbenice(narudzbenicaID);
            cbNarudzbenica.SelectedIndexChanged += (s, e) => PromenaNarudzbenice();
            PromenaNarudzbenice();
        }

        private void Kolona(string naziv, string naslov, int sirina, bool samoCitanje, string format)
        {
            var k = new DataGridViewTextBoxColumn { DataPropertyName = naziv, Name = naziv, HeaderText = naslov, Width = sirina, ReadOnly = samoCitanje };
            if (format != null)
            {
                k.DefaultCellStyle.Format = format;
                k.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                k.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (samoCitanje)
                k.DefaultCellStyle.BackColor = SavaTheme.Surface;
            dgv.Columns.Add(k);
        }

        private void PuniNarudzbenice(int izabrana)
        {
            DataTable dt;
            try
            {
                dt = SERBaza.Tabela(@"
                    SELECT 0 AS ID, N'— bez narudžbenice —' AS Naziv
                    UNION ALL
                    SELECT ID, Broj + N' · ' + ISNULL(Dobavljac, N'') + N' · ' + StatusTekst FROM dbo.vSERNarudzbenica WHERE Status IN (3, 4)
                    ORDER BY ID");
            }
            catch (Exception ex)
            {
                SERDijalog.Greska(this, Text, ex);
                return;
            }
            cbNarudzbenica.DisplayMember = "Naziv";
            cbNarudzbenica.ValueMember = "ID";
            cbNarudzbenica.DataSource = dt;
            cbNarudzbenica.SelectedValue = izabrana;
            if (cbNarudzbenica.SelectedIndex < 0)
                cbNarudzbenica.SelectedIndex = 0;
        }

        private int Narudzbenica => SERBaza.CeoBroj(cbNarudzbenica.SelectedValue);

        private void PromenaNarudzbenice()
        {
            stavke.Rows.Clear();
            bool bez = Narudzbenica == 0;
            tbRazlog.ReadOnly = !bez;
            tbRazlog.BackColor = bez ? SavaTheme.White : SavaTheme.Surface;
            dodaj.Enabled = bez;
            dgv.Columns["Naruceno"].Visible = !bez;
            dgv.Columns["Primljeno"].Visible = !bez;
            if (bez)
                return;
            try
            {
                foreach (DataRow r in SERBaza.Tabela(@"
                        SELECT ID, DeoID, Sifra, Naziv, Kolicina, Primljeno, Preostalo, Cena
                        FROM dbo.vSERNarudzbenicaStavka WHERE NarudzbenicaID = @N AND Preostalo > 0 ORDER BY Sifra",
                        SERBaza.P("@N", Narudzbenica)).Rows)
                    stavke.Rows.Add(r["ID"], r["DeoID"], r["Sifra"], r["Naziv"], r["Kolicina"], r["Primljeno"], r["Preostalo"], r["Cena"]);
            }
            catch (Exception ex)
            {
                SERDijalog.Greska(this, Text, ex);
            }
        }

        private void DodajDeo()
        {
            var polja = new List<SERPolje>
            {
                new SERPolje("DeoID", "Deo", SERTip.Lista, true) { ListaUpit = SERListe.AktivniDelovi, Raspon = 2 },
                SERPolje.Kolicina("Kolicina", "Količina"),
                SERPolje.Decimalni("Cena", "Cena RSD", true)
            };
            SERDijalog.Unos(this, "Deo na prijemu", polja, null, v =>
            {
                DataRow d = SERBaza.Red("SELECT Sifra, Naziv FROM dbo.SERDeo WHERE ID = @ID", SERBaza.P("@ID", v.Broj("DeoID")));
                stavke.Rows.Add(DBNull.Value, v.Broj("DeoID"), d["Sifra"], d["Naziv"], DBNull.Value, DBNull.Value, v.Uzmi("Kolicina"), v.Uzmi("Cena"));
            });
        }

        // Servis.Delovi.TolerancijaPrijema (%): koliko prijem sme da pređe preostalu količinu narudžbenice bez komentara
        private static decimal ToleranciaPrijema()
        {
            try
            {
                string v = Convert.ToString(SERBaza.Skalar("SELECT Vrednost FROM dbo.Podesavanja WHERE Kljuc = N'Servis.Delovi.TolerancijaPrijema'"));
                return decimal.TryParse((v ?? "").Trim().Replace(',', '.'), NumberStyles.Number, CultureInfo.InvariantCulture, out decimal t) && t > 0 ? t : 0;
            }
            catch
            {
                return 0;
            }
        }

        private void Sacuvaj()
        {
            dgv.EndEdit();
            if (!SERPrava.Proveri(this, SERPrava.Magacioner))
                return;
            var zaPrijem = new List<DataRow>();
            foreach (DataRow r in stavke.Rows)
            {
                if (r.RowState == DataRowState.Deleted)
                    continue;
                decimal kol = SERBaza.Decimalni(r["Kolicina"]);
                if (kol < 0 || SERBaza.Decimalni(r["Cena"]) < 0)
                {
                    MessageBox.Show(this, "Količina i cena ne smeju biti negativne (" + r["Sifra"] + ").", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (kol > 0)
                    zaPrijem.Add(r);
            }
            if (zaPrijem.Count == 0)
            {
                MessageBox.Show(this, "Nema stavki sa količinom za prijem.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var bezCene = new List<string>();
            foreach (DataRow r in zaPrijem)
                if (SERBaza.Decimalni(r["Cena"]) == 0)
                    bezCene.Add(Convert.ToString(r["Sifra"]));
            if (bezCene.Count > 0 && !SERDijalog.Potvrdi(this,
                    "Cena je 0 za: " + string.Join(", ", bezCene) + ".\nProsečna cena i vrednost zaliha tih delova biće netačne. Nastaviti?", Text))
                return;

            // količina veća od preostale po narudžbenici (uz dozvoljenu toleranciju): prijem je moguć samo uz obavezan komentar
            decimal tolerancija = ToleranciaPrijema();
            var prekoracene = new List<DataRow>();
            var spisak = new List<string>();
            foreach (DataRow r in zaPrijem)
            {
                if (r["NarudzbenicaStavkaID"] == DBNull.Value)
                    continue;
                decimal preostalo = Math.Max(0, SERBaza.Decimalni(r["Naruceno"]) - SERBaza.Decimalni(r["Primljeno"]));
                decimal kol = SERBaza.Decimalni(r["Kolicina"]);
                if (kol > preostalo * (1 + tolerancija / 100m))
                {
                    prekoracene.Add(r);
                    spisak.Add(r["Sifra"] + " (" + SERBaza.Kolicina(kol) + " od preostalih " + SERBaza.Kolicina(preostalo) + ")");
                }
            }
            string komentarPrekoracenja = null;
            if (prekoracene.Count > 0)
            {
                string prikaz = string.Join(", ", spisak.GetRange(0, Math.Min(2, spisak.Count))) + (spisak.Count > 2 ? " i još " + (spisak.Count - 2) : "");
                MessageBox.Show(this, "Količina je veća od preostale po narudžbenici:\n" + string.Join("\n", spisak)
                    + (tolerancija > 0 ? "\n\nDozvoljena tolerancija: " + SERBaza.Kolicina(tolerancija) + "%." : "")
                    + "\n\nPrijem je moguć uz obavezan komentar.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                komentarPrekoracenja = SERDijalog.Razlog(this, "Prijem preko naručene količine", "Razlog prekoračenja (" + prikaz + ")", true);
                if (string.IsNullOrWhiteSpace(komentarPrekoracenja))
                    return;
            }
            string broj = null;
            bool ok = SERDijalog.Pokusaj(this, Text, () => SERBaza.UTransakciji((con, tr) =>
            {
                var dok = SERBaza.Izlaz("@DokumentID", SqlDbType.Int);
                var pBroj = SERBaza.Izlaz("@Broj", SqlDbType.NVarChar, 20);
                SERBaza.Procedura(con, tr, "dbo.SER_PrijemNovi",
                    SERBaza.P("@NarudzbenicaID", Narudzbenica == 0 ? null : (object)Narudzbenica), SERBaza.P("@Datum", dtDatum.Value.Date),
                    SERBaza.P("@Razlog", Narudzbenica == 0 ? SERBaza.Tekst(tbRazlog.Text) : DBNull.Value),
                    SERBaza.P("@Napomena", SERBaza.Tekst(tbNapomena.Text)), SERBaza.P("@Korisnik", SERBaza.Korisnik), dok, pBroj);
                foreach (DataRow r in zaPrijem)
                    SERBaza.Procedura(con, tr, "dbo.SER_PrijemStavka",
                        SERBaza.P("@DokumentID", dok.Value),
                        SERBaza.P("@NarudzbenicaStavkaID", r["NarudzbenicaStavkaID"]), SERBaza.P("@DeoID", r["DeoID"]),
                        SERBaza.P("@Kolicina", r["Kolicina"]), SERBaza.P("@Cena", r["Cena"]),
                        SERBaza.P("@Napomena", prekoracene.Contains(r) ? SERBaza.Tekst(komentarPrekoracenja) : DBNull.Value), SERBaza.P("@Korisnik", SERBaza.Korisnik),
                        SERBaza.Izlaz("@KretanjeID", SqlDbType.Int));
                broj = Convert.ToString(pBroj.Value);
            }));
            if (!ok)
                return;
            MessageBox.Show(this, "Prijem je proknjižen: " + broj + ".", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
