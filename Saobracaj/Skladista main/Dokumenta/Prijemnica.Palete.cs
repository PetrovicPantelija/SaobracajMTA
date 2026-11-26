using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main.Dokumenta
{
    // Dodela paleta stavkama Prijemnice (Carina-Prijemnica-Palete-Specifikacija v0.4).
    // Palete se prave posle snimanja stavki (CAR-PL02); broj palete je 13 cifara iz brojača u bazi.
    // Uključuje se kad postoje tabele i procedure iz poglavlja 10 skripte Carina_PrijemnicaPL_2026_11.sql.
    public partial class Prijemnica
    {
        bool ImaPalete;
        Button btnDodeliPalete;
        Panel pnlPalete;
        DataGridView dgvPalStavke, dgvKontejneri, dgvPalete;
        ComboBox cboPalVrsta;
        TextBox txtKoletaPoPaleti;
        Label lblPalete;
        Button btnNapraviPalete, btnDodajPaletu, btnIzbaciPaletu, btnSpojiPalete;
        bool popunjavanjePaleta;

        // Polje palete (v0.5.1)
        bool ImaPolja;
        ComboBox cboFilterStavka, cboFilterKontejner, cboPolje;
        CheckBox chkSamoBezPolja;
        Label lblPoljaStanje;
        Button btnDodeliPolje, btnUkloniPolje;
        DataTable dtPalete;

        private void InitPalete()
        {
            ImaPalete = ImaPLKolone && ProveriPalete();
            if (!ImaPalete)
                return;
            ImaPolja = ProveriPolja();

            btnDodeliPalete = new Button
            {
                Text = "Dodeli palete",
                Size = new Size(btnUparivanje != null ? btnUparivanje.Width : 150, 32),
                BackColor = btnPackingLista.BackColor,
                ForeColor = btnPackingLista.ForeColor,
                Font = btnPackingLista.Font,
                FlatStyle = btnPackingLista.FlatStyle,
                UseVisualStyleBackColor = false
            };
            btnDodeliPalete.Click += (s, e) => OtvoriPalete();

            KreirajPanelPalete();
        }

        private bool ProveriPalete()
        {
            try
            {
                using (var conn = new SqlConnection(connection))
                using (var cmd = new SqlCommand(
                    "SELECT CASE WHEN OBJECT_ID('dbo.RNCarinskoPaleta') IS NOT NULL AND OBJECT_ID('dbo.sp_Palete_Napravi') IS NOT NULL " +
                    "AND OBJECT_ID('dbo.sp_Palete_Spoji') IS NOT NULL THEN 1 ELSE 0 END", conn))
                {
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar()) == 1;
                }
            }
            catch
            {
                return false;
            }
        }

        #region Panel

        private void KreirajPanelPalete()
        {
            pnlPalete = new Panel
            {
                Location = new Point(13, 40),
                Size = new Size(ClientSize.Width - 26, ClientSize.Height - 48),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.Gainsboro,
                Visible = false
            };

            var lblNaslov = new Label { Text = "Dodela paleta", Location = new Point(10, 12), AutoSize = true, Font = new Font(Font.FontFamily, Font.Size + 2, FontStyle.Bold) };
            lblPalete = new Label { Location = new Point(160, 14), Size = new Size(pnlPalete.Width - 280, 20), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            var btnZatvori = NapraviDugme("Zatvori", pnlPalete.Width - 100, 8, 88);
            btnZatvori.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnZatvori.Click += (s, e) => pnlPalete.Visible = false;

            // gore: stavke sa rasporedom koleta
            dgvPalStavke = NapraviTabelu();
            dgvPalStavke.Location = new Point(10, 44);
            dgvPalStavke.Size = new Size(pnlPalete.Width - 20, 170);
            dgvPalStavke.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvPalStavke.ReadOnly = true;
            dgvPalStavke.MultiSelect = true;
            dgvPalStavke.SelectionChanged += (s, e) => { if (!popunjavanjePaleta) PrikaziPaleteIzabranih(true); };

            // sredina: ulaz za automatsku dodelu
            int y = 224;
            var lblVrsta = new Label { Text = "Vrsta palete", Location = new Point(10, y), AutoSize = true };
            cboPalVrsta = new ComboBox { Location = new Point(10, y + 18), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            var lblKpp = new Label { Text = "Koleta po paleti", Location = new Point(222, y), AutoSize = true };
            txtKoletaPoPaleti = new TextBox { Location = new Point(222, y + 18), Width = 100, TextAlign = HorizontalAlignment.Right };
            var lblKont = new Label { Text = "Kontejneri i koleta (za stavku u više kontejnera po jedan red)", Location = new Point(340, y), AutoSize = true };

            dgvKontejneri = NapraviTabelu();
            dgvKontejneri.Location = new Point(340, y + 18);
            dgvKontejneri.Size = new Size(380, 104);
            dgvKontejneri.AllowUserToAddRows = true;
            dgvKontejneri.AllowUserToDeleteRows = true;
            dgvKontejneri.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvKontejneri.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKontejneri.Columns.Add(new DataGridViewTextBoxColumn { Name = "Kontejner", HeaderText = "Kontejner" });
            dgvKontejneri.Columns.Add(new DataGridViewTextBoxColumn { Name = "Koleta", HeaderText = "Koleta", ValueType = typeof(int) });
            dgvKontejneri.Columns["Koleta"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            btnNapraviPalete = NapraviDugme("Napravi palete", 736, y + 18, 150);
            btnDodajPaletu = NapraviDugme("Dodaj paletu", 736, y + 56, 150);
            btnIzbaciPaletu = NapraviDugme("Izbaci paletu", 896, y + 18, 170);
            btnSpojiPalete = NapraviDugme("Spoji u jednu paletu", 896, y + 56, 170);
            btnNapraviPalete.Click += (s, e) => NapraviPalete();
            btnDodajPaletu.Click += (s, e) => DodajPaletu();
            btnIzbaciPaletu.Click += (s, e) => IzbaciPalete();
            btnSpojiPalete.Click += (s, e) => SpojiPalete();

            // deo „Polje“ iznad tabele paleta (v0.5.1)
            int yLista = y + 132;
            if (ImaPolja)
            {
                KreirajDeoPolje(y + 132);
                yLista = y + 206;
            }

            // dole: palete izabranih stavki
            var lblListaPaleta = new Label { Text = "Palete izabranih stavki (Koleta stavke se menja u tabeli; 0 uklanja stavku sa palete; Ctrl/Shift za više paleta)", Location = new Point(10, yLista), AutoSize = true };
            dgvPalete = NapraviTabelu();
            dgvPalete.Location = new Point(10, yLista + 20);
            dgvPalete.Size = new Size(pnlPalete.Width - 20, pnlPalete.Height - (yLista + 30));
            dgvPalete.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvPalete.MultiSelect = true;
            dgvPalete.CellEndEdit += DgvPalete_CellEndEdit;

            pnlPalete.Controls.AddRange(new Control[] { lblNaslov, lblPalete, btnZatvori, dgvPalStavke, lblVrsta, cboPalVrsta, lblKpp, txtKoletaPoPaleti,
                lblKont, dgvKontejneri, btnNapraviPalete, btnDodajPaletu, btnIzbaciPaletu, btnSpojiPalete, lblListaPaleta, dgvPalete });
            Controls.Add(pnlPalete);
        }

        private bool ProveriPolja()
        {
            try
            {
                using (var conn = new SqlConnection(connection))
                using (var cmd = new SqlCommand(
                    "SELECT CASE WHEN COL_LENGTH('dbo.RNCarinskoPaleta','IDSkladista') IS NOT NULL AND OBJECT_ID('dbo.sp_Palete_DodeliPolje') IS NOT NULL " +
                    "AND OBJECT_ID('dbo.sp_Polja_ZaPalete') IS NOT NULL THEN 1 ELSE 0 END", conn))
                {
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar()) == 1;
                }
            }
            catch
            {
                return false;
            }
        }

        // Filteri (stavka, kontejner, samo bez polja), izbor polja i dugmad Dodeli / Ukloni polje / Mapa
        private void KreirajDeoPolje(int y)
        {
            var lblStavka = new Label { Text = "Stavka", Location = new Point(10, y + 4), AutoSize = true };
            cboFilterStavka = new ComboBox { Location = new Point(70, y), Width = 260, DropDownStyle = ComboBoxStyle.DropDownList };
            var lblKontejner = new Label { Text = "Kontejner", Location = new Point(342, y + 4), AutoSize = true };
            cboFilterKontejner = new ComboBox { Location = new Point(412, y), Width = 170, DropDownStyle = ComboBoxStyle.DropDownList };
            chkSamoBezPolja = new CheckBox { Text = "Samo bez polja", Location = new Point(596, y + 2), AutoSize = true, Checked = true };
            var btnIzaberiSve = NapraviDugme("Izaberi sve prikazane", 730, y - 4, 170);

            var lblPolje = new Label { Text = "Polje", Location = new Point(10, y + 40), AutoSize = true };
            cboPolje = new ComboBox
            {
                Location = new Point(70, y + 36),
                Width = 260,
                DropDownStyle = ComboBoxStyle.DropDown,
                AutoCompleteMode = AutoCompleteMode.SuggestAppend,
                AutoCompleteSource = AutoCompleteSource.ListItems
            };
            btnDodeliPolje = NapraviDugme("Dodeli polje", 342, y + 32, 130);
            btnUkloniPolje = NapraviDugme("Ukloni polje", 482, y + 32, 130);
            var btnMapa = NapraviDugme("Mapa", 622, y + 32, 90);
            lblPoljaStanje = new Label { Location = new Point(724, y + 40), AutoSize = true, Font = new Font(Font, FontStyle.Bold) };

            cboFilterStavka.SelectedIndexChanged += (s, e) => { if (!popunjavanjePaleta) PrimeniFilterPaleta(); };
            cboFilterKontejner.SelectedIndexChanged += (s, e) => { if (!popunjavanjePaleta) PrimeniFilterPaleta(); };
            chkSamoBezPolja.CheckedChanged += (s, e) => PrimeniFilterPaleta();
            btnIzaberiSve.Click += (s, e) => { dgvPalete.SelectAll(); dgvPalete.Focus(); };
            btnDodeliPolje.Click += (s, e) => DodeliPolje();
            btnUkloniPolje.Click += (s, e) => UkloniPolje();
            btnMapa.Click += (s, e) => button1_Click(btnMapa, EventArgs.Empty);   // postojeća Mapa pozicija (pregled)

            pnlPalete.Controls.AddRange(new Control[] { lblStavka, cboFilterStavka, lblKontejner, cboFilterKontejner, chkSamoBezPolja, btnIzaberiSve,
                lblPolje, cboPolje, btnDodeliPolje, btnUkloniPolje, btnMapa, lblPoljaStanje });
        }

        // Aktivno tek kad su stavke snimljene i uparene sa naimenovanjem (palete upućuju na ID stavke)
        private void OtvoriPalete()
        {
            ZavrsiUnosUGridu();
            if (txtPrijemnica.Text == "")
            {
                MessageBox.Show("Prvo snimite prijemnicu (Snimi), pa dodelite palete.");
                return;
            }
            if (list.Count == 0)
            {
                MessageBox.Show("Prijemnica nema stavki.");
                return;
            }
            if (PotpisStavki() != stavkeSnimak || list.Any(x => x.ID == 0))
            {
                MessageBox.Show("Stavke imaju nesnimljene izmene. Snimite (Snimi), pa dodelite palete.");
                return;
            }

            try
            {
                UcitajStatusPrijemnice();
                var naimenovanja = UcitajNaimenovanja();
                if (naimenovanja.Rows.Count > 0 && list.Any(x => !x.IDNaimenovanja.HasValue))
                {
                    MessageBox.Show("Prvo uparite sve stavke sa naimenovanjem (Uparivanje i kontrola) i snimite.");
                    return;
                }

                if (cboPalVrsta.DataSource == null && cboVrstaPaleta.DataSource is DataTable tipovi)
                {
                    cboPalVrsta.DataSource = tipovi.Copy();
                    cboPalVrsta.DisplayMember = "Naziv";
                    cboPalVrsta.ValueMember = "ID";
                }

                if (ImaPolja)
                    UcitajPolja();

                PodesiKontejnereZaPalete();   // RAZ-09 (Prijemnica.Kontejneri.cs)

                // dodela polja ostaje dozvoljena i posle potvrde (CAR-PP07: smeštaj posle prijema)
                bool zakljucano = StavkeZakljucane();
                foreach (Control c in new Control[] { btnNapraviPalete, btnDodajPaletu, btnIzbaciPaletu, btnSpojiPalete, dgvKontejneri })
                    c.Enabled = !zakljucano;
                dgvPalete.ReadOnly = zakljucano;

                pnlPalete.Visible = true;
                pnlPalete.BringToFront();
                OsveziPalete();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri otvaranju dodele paleta: " + ex.Message);
            }
        }

        private class PalStavka
        {
            public int ID, RB, Koleta, Rasporedjeno, Paleta, IDArtikla, VrstaPaleta;
            public string Artikal, Naimenovanje;
            public int Preostalo { get { return Koleta - Rasporedjeno; } }
            public string Status
            {
                get
                {
                    if (Rasporedjeno == 0) return "Nije raspoređeno";
                    if (Preostalo == 0) return "Raspoređeno";
                    return Preostalo > 0 ? "Delimično" : "Višak, ponovo dodeliti";
                }
            }
        }

        private List<PalStavka> UcitajPalStavke()
        {
            var rezultat = new List<PalStavka>();
            using (var conn = new SqlConnection(connection))
            using (var cmd = new SqlCommand(@"SELECT s.ID, s.RB, RTRIM(ISNULL(s.Naziv, '')) AS Artikal, RTRIM(ISNULL(s.Naimenovanje, '')) AS Naimenovanje,
                    CONVERT(int, ROUND(ISNULL(s.Koleta, 0), 0)) AS Koleta,
                    ISNULL((SELECT SUM(ps.Koleta) FROM RNCarinskoPaletaStavka ps WHERE ps.IDStavke = s.ID), 0) AS Rasporedjeno,
                    (SELECT COUNT(*) FROM RNCarinskoPaletaStavka ps WHERE ps.IDStavke = s.ID) AS Paleta,
                    ISNULL(s.IDArtikla, 0) AS IDArtikla, ISNULL(s.VrstaPaleta, 0) AS VrstaPaleta
                FROM RNCarinskoPrijemnicaStavke s WHERE s.IDNadredjena = @P ORDER BY s.RB", conn))
            {
                cmd.Parameters.AddWithValue("@P", Convert.ToInt32(txtPrijemnica.Text));
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                    while (dr.Read())
                        rezultat.Add(new PalStavka
                        {
                            ID = dr.GetInt32(0),
                            RB = Convert.ToInt32(dr[1]),
                            Artikal = dr.GetString(2),
                            Naimenovanje = dr.GetString(3),
                            Koleta = dr.GetInt32(4),
                            Rasporedjeno = dr.GetInt32(5),
                            Paleta = dr.GetInt32(6),
                            IDArtikla = Convert.ToInt32(dr[7]),
                            VrstaPaleta = Convert.ToInt32(dr[8])
                        });
            }
            return rezultat;
        }

        // Osvežava obe tabele; zadržava izabrane stavke
        private void OsveziPalete()
        {
            var izabrane = new HashSet<int>(IzabraneStavke().Select(x => x.ID));
            var stavke = UcitajPalStavke();

            popunjavanjePaleta = true;
            try
            {
                var dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("RB", typeof(int));
                dt.Columns.Add("Artikal", typeof(string));
                dt.Columns.Add("Naimenovanje", typeof(string));
                dt.Columns.Add("Koleta", typeof(int));
                dt.Columns.Add("Raspoređeno", typeof(int));
                dt.Columns.Add("Preostalo", typeof(int));
                dt.Columns.Add("Broj paleta", typeof(int));
                dt.Columns.Add("Status", typeof(string));
                foreach (var s in stavke)
                    dt.Rows.Add(s.ID, s.RB, s.Artikal, s.Naimenovanje, s.Koleta, s.Rasporedjeno, s.Preostalo, s.Paleta, s.Status);

                dgvPalStavke.DataSource = dt;
                dgvPalStavke.Columns["ID"].Visible = false;
                foreach (string k in new[] { "Koleta", "Raspoređeno", "Preostalo", "Broj paleta" })
                {
                    dgvPalStavke.Columns[k].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvPalStavke.Columns[k].DefaultCellStyle.Format = "N0";
                }

                dgvPalStavke.ClearSelection();
                foreach (DataGridViewRow red in dgvPalStavke.Rows)
                {
                    string status = Convert.ToString(red.Cells["Status"].Value);
                    red.Cells["Status"].Style.BackColor = status == "Raspoređeno" ? Color.Honeydew : status == "Delimično" ? Color.LemonChiffon : Color.MistyRose;
                    if (izabrane.Contains(Convert.ToInt32(red.Cells["ID"].Value)))
                        red.Selected = true;
                }
                if (dgvPalStavke.SelectedRows.Count == 0 && dgvPalStavke.Rows.Count > 0)
                    dgvPalStavke.Rows[0].Selected = true;

                lblPalete.Text = "Stavki: " + stavke.Count + ", raspoređeno " + stavke.Sum(x => x.Rasporedjeno).ToString("N0") + " od " + stavke.Sum(x => x.Koleta).ToString("N0")
                    + " koleta, paleta na prijemnici: " + BrojPaletaPrijemnice()
                    + (StavkeZakljucane() ? ".  Prijemnica je potvrđena (ZA): palete menja samo korisnik sa pravom korekcije." : ".");
            }
            finally
            {
                popunjavanjePaleta = false;
            }
            PrikaziPaleteIzabranih(izabrane.Count == 0);
            if (ImaPolja)
            {
                OsveziStanjePolja();
                UcitajPolja();
            }
        }

        private int BrojPaletaPrijemnice()
        {
            using (var conn = new SqlConnection(connection))
            using (var cmd = new SqlCommand("SELECT COUNT(*) FROM RNCarinskoPaleta WHERE IDPrijemnice = @P", conn))
            {
                cmd.Parameters.AddWithValue("@P", Convert.ToInt32(txtPrijemnica.Text));
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private List<PalStavka> IzabraneStavke()
        {
            var rezultat = new List<PalStavka>();
            if (dgvPalStavke == null || dgvPalStavke.DataSource == null)
                return rezultat;
            foreach (DataGridViewRow red in dgvPalStavke.SelectedRows)
                rezultat.Add(new PalStavka
                {
                    ID = Convert.ToInt32(red.Cells["ID"].Value),
                    RB = Convert.ToInt32(red.Cells["RB"].Value),
                    Artikal = Convert.ToString(red.Cells["Artikal"].Value),
                    Koleta = Convert.ToInt32(red.Cells["Koleta"].Value),
                    Rasporedjeno = Convert.ToInt32(red.Cells["Raspoređeno"].Value)
                });
            return rezultat.OrderBy(x => x.RB).ToList();
        }

        // Palete izabranih stavki; predlog vrste, koleta po paleti i kontejnera kad se izbor promeni
        private void PrikaziPaleteIzabranih(bool predlog)
        {
            var izabrane = IzabraneStavke();
            var dt = new DataTable();
            if (izabrane.Count > 0)
            {
                using (var conn = new SqlConnection(connection))
                using (var da = new SqlDataAdapter(@"SELECT p.ID, ps.IDStavke, p.BrojPalete AS [Broj palete], s.RB AS [RB stavke], RTRIM(ISNULL(s.Naziv, '')) AS Artikal,
                        tp.Naziv AS Vrsta, ps.Koleta AS [Koleta stavke],
                        (SELECT SUM(x.Koleta) FROM RNCarinskoPaletaStavka x WHERE x.IDPalete = p.ID) AS [Koleta na paleti],
                        p.BrojKontejnera AS Kontejner,"
                        + (ImaPolja ? " RTRIM(ISNULL(CONVERT(nvarchar(200), sk.Naziv), ISNULL(p.Pozicija, ''))) AS Polje," : "") + @"
                        CASE WHEN (SELECT COUNT(*) FROM RNCarinskoPaletaStavka x WHERE x.IDPalete = p.ID) > 1 THEN N'Da' ELSE N'Ne' END AS [Mešana],
                        CASE p.Status WHEN 1 THEN N'Planirana' ELSE N'Na polju' END AS Status
                    FROM RNCarinskoPaletaStavka ps
                    JOIN RNCarinskoPaleta p ON p.ID = ps.IDPalete
                    JOIN RNCarinskoPrijemnicaStavke s ON s.ID = ps.IDStavke
                    LEFT JOIN TipPalete tp ON tp.ID = p.VrstaPaleta"
                    + (ImaPolja ? " LEFT JOIN Skladista sk ON sk.ID = p.IDSkladista" : "") + @"
                    WHERE ps.IDStavke IN (" + string.Join(",", izabrane.Select(x => x.ID)) + @")
                    ORDER BY p.BrojPalete, s.RB", conn))
                    da.Fill(dt);
            }

            dtPalete = dt;
            if (ImaPolja)
            {
                PopuniFilterePolja(izabrane);
                dgvPalete.DataSource = dt.DefaultView;
                PrimeniFilterPaleta();
            }
            else
                dgvPalete.DataSource = dt;
            if (dt.Columns.Count > 0)
            {
                dgvPalete.Columns["ID"].Visible = false;
                dgvPalete.Columns["IDStavke"].Visible = false;
                dgvPalete.Columns["Broj palete"].DefaultCellStyle.Format = "0";
                foreach (DataGridViewColumn k in dgvPalete.Columns)
                    k.ReadOnly = k.Name != "Koleta stavke";
                dgvPalete.Columns["Koleta stavke"].DefaultCellStyle.BackColor = Color.White;
                foreach (string k in new[] { "Koleta stavke", "Koleta na paleti" })
                    dgvPalete.Columns[k].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (!predlog || izabrane.Count == 0)
                return;

            // predlog za automatsku dodelu
            var prva = izabrane[0];
            var stavka = list.FirstOrDefault(x => x.ID == prva.ID);
            int vrsta = stavka != null && stavka.VrstaPaleta > 0 ? stavka.VrstaPaleta : 0;
            int koletaPoPaleti = PredlogKoletaPoPaleti(stavka != null ? stavka.IDArtikla : 0, ref vrsta, stavka != null ? stavka.IDPLStavke : null);
            if (vrsta > 0)
                cboPalVrsta.SelectedValue = vrsta;
            txtKoletaPoPaleti.Text = koletaPoPaleti.ToString();

            // predlog koleta: neraspoređena koleta, a ako ih nema, sva koja nisu na paletama sa poljem (CAR-PP04)
            dgvKontejneri.Rows.Clear();
            if (izabrane.Count == 1)
            {
                int ostatak = Math.Max(prva.Koleta - prva.Rasporedjeno, 0);
                int bezPolja = Math.Max(prva.Koleta - KoletaNaPozicioniranim(prva.ID), 0);
                // RAZ-09: stavka sa raspodelom po kontejnerima i bez paleta na polju -> po jedan red za svaki kontejner
                var poKontejneru = KoletaNaPozicioniranim(prva.ID) == 0 ? RaspodelaStavke(prva.ID) : null;
                if (poKontejneru != null && poKontejneru.Count > 0)
                    foreach (var k in poKontejneru)
                        dgvKontejneri.Rows.Add(KontejnerZaPaletu(k.Key), k.Value);
                else
                    dgvKontejneri.Rows.Add(KontejnerZaPaletu(txtKontejnerPrijem.Text.Trim()), ostatak > 0 ? ostatak : bezPolja);
            }
        }

        #region Polje

        // Filter stavke: sve izabrane ili jedna; filter kontejnera: svi, bez kontejnera ili jedan
        private void PopuniFilterePolja(List<PalStavka> izabrane)
        {
            object stavka = cboFilterStavka.SelectedItem is KeyValuePair<int, string> sv ? (object)sv.Key : null;
            string kontejner = cboFilterKontejner.SelectedItem as string;

            popunjavanjePaleta = true;
            try
            {
                var stavke = new List<KeyValuePair<int, string>> { new KeyValuePair<int, string>(0, "(sve izabrane)") };
                stavke.AddRange(izabrane.Select(x => new KeyValuePair<int, string>(x.ID, "RB " + x.RB + " – " + x.Artikal)));
                cboFilterStavka.DataSource = stavke;
                cboFilterStavka.DisplayMember = "Value";
                cboFilterStavka.ValueMember = "Key";
                cboFilterStavka.SelectedValue = stavka != null && stavke.Any(x => x.Key == (int)stavka) ? (int)stavka : 0;

                var kontejneri = new List<string> { "(svi)" };
                if (dtPalete != null && dtPalete.Columns.Contains("Kontejner"))
                {
                    var lista = dtPalete.Rows.Cast<DataRow>().Select(r => Convert.ToString(r["Kontejner"]).Trim()).Distinct().OrderBy(x => x).ToList();
                    if (lista.Contains("")) kontejneri.Add("(bez kontejnera)");
                    kontejneri.AddRange(lista.Where(x => x != ""));
                }
                cboFilterKontejner.DataSource = kontejneri;
                cboFilterKontejner.SelectedItem = kontejner != null && kontejneri.Contains(kontejner) ? kontejner : "(svi)";
            }
            finally
            {
                popunjavanjePaleta = false;
            }
        }

        private void PrimeniFilterPaleta()
        {
            if (dtPalete == null || !ImaPolja || !dtPalete.Columns.Contains("Polje"))
                return;

            var uslovi = new List<string>();
            if (cboFilterStavka.SelectedValue is int idStavke && idStavke > 0)
                uslovi.Add("IDStavke = " + idStavke);
            string kontejner = cboFilterKontejner.SelectedItem as string;
            if (kontejner == "(bez kontejnera)")
                uslovi.Add("(Kontejner IS NULL OR Kontejner = '')");
            else if (!string.IsNullOrEmpty(kontejner) && kontejner != "(svi)")
                uslovi.Add("Kontejner = '" + kontejner.Replace("'", "''") + "'");
            if (chkSamoBezPolja.Checked)
                uslovi.Add("(Polje IS NULL OR Polje = '')");

            try
            {
                dtPalete.DefaultView.RowFilter = string.Join(" AND ", uslovi);
            }
            catch
            {
                dtPalete.DefaultView.RowFilter = "";
            }
            dgvPalete.ClearSelection();
        }

        // Aktivna polja iz Skladista (CAR-PP02) sa brojem paleta na svakom
        private void UcitajPolja()
        {
            object izabrano = cboPolje.SelectedValue;
            var dt = new DataTable();
            using (var conn = new SqlConnection(connection))
            using (var da = new SqlDataAdapter("dbo.sp_Polja_ZaPalete", conn))
            {
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@IDPrijemnice", Convert.ToInt32(txtPrijemnica.Text));
                da.Fill(dt);
            }
            dt.Columns.Add("Prikaz", typeof(string), "Naziv + IIF(BrojPaleta > 0, ' (' + Convert(BrojPaleta, 'System.String') + ' pal.)', '')");
            cboPolje.DataSource = dt;
            cboPolje.DisplayMember = "Prikaz";
            cboPolje.ValueMember = "ID";
            if (izabrano != null)
                cboPolje.SelectedValue = izabrano;
            else
                cboPolje.SelectedIndex = -1;
        }

        private void OsveziStanjePolja()
        {
            using (var conn = new SqlConnection(connection))
            using (var cmd = new SqlCommand(@"SELECT COUNT(*), ISNULL(SUM(CASE WHEN IDSkladista IS NOT NULL THEN 1 ELSE 0 END), 0)
                FROM RNCarinskoPaleta WHERE IDPrijemnice = @P", conn))
            {
                cmd.Parameters.AddWithValue("@P", Convert.ToInt32(txtPrijemnica.Text));
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                    if (dr.Read())
                    {
                        int ukupno = dr.GetInt32(0), naPolju = dr.GetInt32(1);
                        lblPoljaStanje.Text = "Palete: " + ukupno + ", na polju " + naPolju + ", bez polja " + (ukupno - naPolju) + ".";
                    }
            }
        }

        // CAR-PP01: izabrano polje svim označenim paletama (snima se odmah)
        private void DodeliPolje()
        {
            var palete = IzabranePalete();
            if (palete.Count == 0)
            {
                MessageBox.Show("Označite palete u donjoj tabeli (Izaberi sve prikazane, Ctrl ili Shift).");
                return;
            }
            if (cboPolje.SelectedValue == null)
            {
                MessageBox.Show("Izaberite polje.");
                return;
            }
            int idSkladista = Convert.ToInt32(cboPolje.SelectedValue);
            int dodeljeno = 0;
            IzvrsiPalete(conn =>
            {
                using (var cmd = Procedura(conn, "dbo.sp_Palete_DodeliPolje"))
                {
                    cmd.Parameters.AddWithValue("@IDPrijemnice", Convert.ToInt32(txtPrijemnica.Text));
                    cmd.Parameters.AddWithValue("@ListaPaleta", string.Join(",", palete));
                    cmd.Parameters.AddWithValue("@IDSkladista", idSkladista);
                    var izlaz = cmd.Parameters.Add("@Dodeljeno", SqlDbType.Int);
                    izlaz.Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    dodeljeno = izlaz.Value == DBNull.Value ? 0 : Convert.ToInt32(izlaz.Value);
                }
            });
        }

        private void UkloniPolje()
        {
            var palete = IzabranePalete();
            if (palete.Count == 0)
            {
                MessageBox.Show("Označite palete u donjoj tabeli.");
                return;
            }
            if (MessageBox.Show("Uklanja se polje sa " + palete.Count + " paleta (status Planirana). Nastaviti?",
                    "Ukloni polje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            IzvrsiPalete(conn =>
            {
                using (var cmd = Procedura(conn, "dbo.sp_Palete_UkloniPolje"))
                {
                    cmd.Parameters.AddWithValue("@IDPrijemnice", Convert.ToInt32(txtPrijemnica.Text));
                    cmd.Parameters.AddWithValue("@ListaPaleta", string.Join(",", palete));
                    cmd.ExecuteNonQuery();
                }
            });
        }

        #endregion

        // OP-23: koleta po paleti = vrednost iz packing liste (JM po paleti / JM po koletu, ISP-02),
        // inače najčešća vrednost na paletama tog artikla, inače iz podešavanja
        private int PredlogKoletaPoPaleti(int idArtikla, ref int vrsta, int? idPLStavke)
        {
            if (idPLStavke.HasValue)
            {
                using (var conn = new SqlConnection(connection))
                using (var cmd = new SqlCommand("SELECT KoletaPoPaleti FROM RNCarinskoPackingListaStavka WHERE ID = @ID", conn))
                {
                    cmd.Parameters.AddWithValue("@ID", idPLStavke.Value);
                    conn.Open();
                    object v = cmd.ExecuteScalar();
                    if (v != null && v != DBNull.Value && Convert.ToInt32(v) > 0)
                        return Convert.ToInt32(v);
                }
            }

            int predlog;
            if (!int.TryParse(Administracija.Podesavanja.Get("Carina.Palete.KoletaPoPaleti", "60"), out predlog) || predlog <= 0)
                predlog = 60;
            if (idArtikla <= 0)
                return predlog;

            using (var conn = new SqlConnection(connection))
            using (var cmd = new SqlCommand(@"SELECT TOP 1 ps.Koleta, MAX(p.VrstaPaleta)
                FROM RNCarinskoPaletaStavka ps
                JOIN RNCarinskoPaleta p ON p.ID = ps.IDPalete
                JOIN RNCarinskoPrijemnicaStavke s ON s.ID = ps.IDStavke
                WHERE s.IDArtikla = @A
                GROUP BY ps.Koleta ORDER BY COUNT(*) DESC, ps.Koleta DESC", conn))
            {
                cmd.Parameters.AddWithValue("@A", idArtikla);
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                    if (dr.Read())
                    {
                        predlog = dr.GetInt32(0);
                        if (vrsta <= 0 && !dr.IsDBNull(1)) vrsta = dr.GetInt32(1);
                    }
            }
            return predlog;
        }

        #endregion

        #region Radnje

        private int? CeoBroj(object v)
        {
            if (v == null || v == DBNull.Value) return null;
            decimal d;
            if (!decimal.TryParse(Convert.ToString(v).Replace(".", "").Trim(), out d) || d != Math.Truncate(d)) return null;
            return (int)d;
        }

        // CAR-PL04: briše planirane palete izabrane stavke i pravi nove, po redu za svaki kontejner
        private void NapraviPalete()
        {
            var izabrane = IzabraneStavke();
            if (izabrane.Count != 1)
            {
                MessageBox.Show("Izaberite jednu stavku. Za stavku u više kontejnera unesite po jedan red kontejnera.");
                return;
            }
            var stavka = izabrane[0];
            if (cboPalVrsta.SelectedValue == null)
            {
                MessageBox.Show("Izaberite vrstu palete.");
                return;
            }
            int? kpp = CeoBroj(txtKoletaPoPaleti.Text);
            if (!kpp.HasValue || kpp.Value <= 0)
            {
                MessageBox.Show("Koleta po paleti mora biti ceo broj veći od nule.");
                return;
            }

            var redovi = new List<Tuple<string, int>>();
            foreach (DataGridViewRow r in dgvKontejneri.Rows)
            {
                if (r.IsNewRow) continue;
                int? k = CeoBroj(r.Cells["Koleta"].Value);
                if (!k.HasValue || k.Value <= 0)
                {
                    MessageBox.Show("Koleta u svakom redu kontejnera moraju biti ceo broj veći od nule.");
                    return;
                }
                redovi.Add(Tuple.Create(Convert.ToString(r.Cells["Kontejner"].Value ?? "").Trim(), k.Value));
            }
            if (redovi.Count == 0)
            {
                MessageBox.Show("Unesite bar jedan red: kontejner (može prazan) i koleta.");
                return;
            }

            int ukupno = redovi.Sum(x => x.Item2);
            int pozicionirano = KoletaNaPozicioniranim(stavka.ID);
            int brojPaleta = redovi.Sum(x => (x.Item2 + kpp.Value - 1) / kpp.Value);
            string poruka = "Stavka RB " + stavka.RB + " (" + stavka.Artikal + "): " + ukupno.ToString("N0") + " koleta, " + kpp.Value + " po paleti -> "
                + brojPaleta + " paleta" + (redovi.Count > 1 ? " u " + redovi.Count + " kontejnera" : "") + ".\n\n"
                + "Postojeće planirane palete ove stavke se brišu" + (pozicionirano > 0 ? " (palete sa poljem ostaju: " + pozicionirano.ToString("N0") + " koleta)" : "") + ".";
            if (ukupno + pozicionirano != stavka.Koleta)
                poruka += "\n\nPAŽNJA: stavka ima " + stavka.Koleta.ToString("N0") + " koleta, a raspoređuje se " + (ukupno + pozicionirano).ToString("N0") + ".";
            if (pozicionirano == 0)
                poruka += RazlikaOdRaspodele(stavka.ID, redovi);   // RAZ-09: palete po kontejneru = raspodela
            if (MessageBox.Show(poruka + "\n\nNastaviti?", "Napravi palete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            IzvrsiPalete(conn =>
            {
                using (var cmd = Procedura(conn, "dbo.sp_Palete_ObrisiPlanirane"))
                {
                    cmd.Parameters.AddWithValue("@IDStavke", stavka.ID);
                    cmd.ExecuteNonQuery();
                }
                foreach (var r in redovi)
                    NapraviNaBazi(conn, stavka.ID, Convert.ToInt32(cboPalVrsta.SelectedValue), kpp.Value, r.Item2, r.Item1);
            });
        }

        // Jedna paleta za izabranu stavku: preostala koleta (najviše koleta po paleti)
        private void DodajPaletu()
        {
            var izabrane = IzabraneStavke();
            if (izabrane.Count != 1)
            {
                MessageBox.Show("Izaberite jednu stavku.");
                return;
            }
            var stavka = izabrane[0];
            if (cboPalVrsta.SelectedValue == null)
            {
                MessageBox.Show("Izaberite vrstu palete.");
                return;
            }
            int preostalo = stavka.Koleta - stavka.Rasporedjeno;
            if (preostalo <= 0)
            {
                MessageBox.Show("Stavka je već raspoređena. Koleta na paleti menjate u donjoj tabeli.");
                return;
            }
            int? kpp = CeoBroj(txtKoletaPoPaleti.Text);
            int koleta = kpp.HasValue && kpp.Value > 0 ? Math.Min(kpp.Value, preostalo) : preostalo;
            string kontejner = dgvKontejneri.Rows.Count > 0 && !dgvKontejneri.Rows[0].IsNewRow
                ? Convert.ToString(dgvKontejneri.Rows[0].Cells["Kontejner"].Value ?? "").Trim() : txtKontejnerPrijem.Text.Trim();

            IzvrsiPalete(conn => NapraviNaBazi(conn, stavka.ID, Convert.ToInt32(cboPalVrsta.SelectedValue), koleta, koleta, kontejner));
        }

        private void IzbaciPalete()
        {
            var palete = IzabranePalete();
            if (palete.Count == 0)
            {
                MessageBox.Show("Izaberite palete u donjoj tabeli.");
                return;
            }
            if (MessageBox.Show("Izbacuje se paleta: " + palete.Count + ". Mešana paleta se izbacuje cela (sa svim stavkama). Nastaviti?",
                    "Izbaci paletu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            IzvrsiPalete(conn =>
            {
                foreach (int id in palete)
                    using (var cmd = Procedura(conn, "dbo.sp_Palete_Izbaci"))
                    {
                        cmd.Parameters.AddWithValue("@IDPalete", id);
                        cmd.ExecuteNonQuery();
                    }
            });
        }

        // Mešana paleta: dve ili više (obično nepunih) paleta postaju jedna
        private void SpojiPalete()
        {
            var palete = IzabranePalete();
            if (palete.Count < 2)
            {
                MessageBox.Show("Izaberite bar dve palete u donjoj tabeli (za palete različitih stavki izaberite i te stavke u gornjoj tabeli).");
                return;
            }
            if (MessageBox.Show("Spaja se " + palete.Count + " paleta u jednu novu paletu (novi broj). Nastaviti?",
                    "Spoji u jednu paletu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            long nova = 0;
            IzvrsiPalete(conn =>
            {
                using (var cmd = Procedura(conn, "dbo.sp_Palete_Spoji"))
                {
                    cmd.Parameters.AddWithValue("@Palete", string.Join(",", palete));
                    var izlaz = cmd.Parameters.Add("@NovaPaleta", SqlDbType.BigInt);
                    izlaz.Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    nova = izlaz.Value == DBNull.Value ? 0 : Convert.ToInt64(izlaz.Value);
                }
            });
            if (nova > 0)
                MessageBox.Show("Nova paleta: " + nova + ".");
        }

        private void DgvPalete_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvPalete.Columns[e.ColumnIndex].Name != "Koleta stavke")
                return;

            var red = dgvPalete.Rows[e.RowIndex];
            int? koleta = CeoBroj(red.Cells[e.ColumnIndex].Value);
            if (!koleta.HasValue || koleta.Value < 0)
            {
                MessageBox.Show("Koleta su ceo broj (0 uklanja stavku sa palete).");
                BeginInvoke(new Action(OsveziPalete));
                return;
            }
            int idPalete = Convert.ToInt32(red.Cells["ID"].Value), idStavke = Convert.ToInt32(red.Cells["IDStavke"].Value);
            BeginInvoke(new Action(() => IzvrsiPalete(conn =>
            {
                using (var cmd = Procedura(conn, "dbo.sp_Palete_IzmeniKoleta"))
                {
                    cmd.Parameters.AddWithValue("@IDPalete", idPalete);
                    cmd.Parameters.AddWithValue("@IDStavke", idStavke);
                    cmd.Parameters.AddWithValue("@Koleta", koleta.Value);
                    cmd.ExecuteNonQuery();
                }
            })));
        }

        private List<int> IzabranePalete()
        {
            var ids = new List<int>();
            var redovi = dgvPalete.SelectedRows.Cast<DataGridViewRow>()
                .Concat(dgvPalete.SelectedCells.Cast<DataGridViewCell>().Select(c => c.OwningRow));
            foreach (var r in redovi)
            {
                int id = Convert.ToInt32(r.Cells["ID"].Value);
                if (!ids.Contains(id)) ids.Add(id);
            }
            return ids;
        }

        private int KoletaNaPozicioniranim(int idStavke)
        {
            using (var conn = new SqlConnection(connection))
            using (var cmd = new SqlCommand(@"SELECT ISNULL(SUM(ps.Koleta), 0) FROM RNCarinskoPaletaStavka ps
                JOIN RNCarinskoPaleta p ON p.ID = ps.IDPalete WHERE ps.IDStavke = @S AND p.Status <> 1", conn))
            {
                cmd.Parameters.AddWithValue("@S", idStavke);
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private void NapraviNaBazi(SqlConnection conn, int idStavke, int vrsta, int koletaPoPaleti, int koleta, string kontejner)
        {
            using (var cmd = Procedura(conn, "dbo.sp_Palete_Napravi"))
            {
                cmd.Parameters.AddWithValue("@IDStavke", idStavke);
                cmd.Parameters.AddWithValue("@VrstaPaleta", vrsta);
                cmd.Parameters.AddWithValue("@KoletaPoPaleti", koletaPoPaleti);
                cmd.Parameters.AddWithValue("@Koleta", koleta);
                cmd.Parameters.AddWithValue("@BrojKontejnera", string.IsNullOrEmpty(kontejner) ? (object)DBNull.Value : (kontejner.Length > 30 ? kontejner.Substring(0, 30) : kontejner));
                cmd.ExecuteNonQuery();
            }
        }

        private SqlCommand Procedura(SqlConnection conn, string naziv)
        {
            var cmd = new SqlCommand(naziv, conn) { CommandType = CommandType.StoredProcedure };
            if (conn.State == ConnectionState.Open && trenutnaTransakcija != null)
                cmd.Transaction = trenutnaTransakcija;
            cmd.Parameters.AddWithValue("@Korisnik", (Korisnik ?? "").Trim());
            return cmd;
        }

        SqlTransaction trenutnaTransakcija;

        // Sve u jednoj transakciji; posle toga se stavke ponovo čitaju (Paleta i VrstaPaleta su izračunate)
        private void IzvrsiPalete(Action<SqlConnection> radnja)
        {
            try
            {
                using (var conn = new SqlConnection(connection))
                {
                    conn.Open();
                    using (trenutnaTransakcija = conn.BeginTransaction())
                    {
                        radnja(conn);
                        trenutnaTransakcija.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Palete nisu promenjene: " + ex.Message);
            }
            finally
            {
                trenutnaTransakcija = null;
            }

            try
            {
                int id = Convert.ToInt32(txtPrijemnica.Text);
                VratiStavkePrijemnice(id);
                stavkeSnimak = PotpisStavki();
                OsveziPalete();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri osvežavanju paleta: " + ex.Message);
            }
        }

        #endregion
    }
}
