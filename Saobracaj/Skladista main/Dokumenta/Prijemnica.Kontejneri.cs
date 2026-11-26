using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main.Dokumenta
{
    // RAZ-08: raspodela stavki prijemnice po kontejnerima (tabela RNCarinskoStavkaKontejner,
    // skripta Skladista main\Carina_StavkaKontejner_2026_11.sql).
    // Matrica: redovi su stavke, kolone kontejneri sa naloga za prijem (RNCarinskoKontejner), ćelija je broj koleta.
    // Ispod su zbir po kontejneru, koleta kontejnera sa naloga i stanje; desno zbir po stavci i stanje.
    // Red raspodele upućuje na broj kontejnera (ne na ID), jer nalog pri Snimi ponovo upisuje kontejnere.
    public partial class Prijemnica
    {
        const string KolRB = "RB", KolArtikal = "Artikal", KolKoleta = "Koleta", KolZbir = "Zbir", KolStanje = "Stanje";
        const string PrefiksKontejnera = "K:";

        private class KontejnerNaloga
        {
            public string Broj;
            public int? Koleta;     // null: kontejner nema koleta na nalogu, ili ga više nema na nalogu
            public bool NaNalogu;
        }

        bool? imaRaspodelu;

        private bool TabelaRaspodelePostoji()
        {
            if (!imaRaspodelu.HasValue)
            {
                try
                {
                    using (var conn = new SqlConnection(connection))
                    using (var cmd = new SqlCommand("SELECT CASE WHEN OBJECT_ID('dbo.RNCarinskoStavkaKontejner', 'U') IS NULL THEN 0 ELSE 1 END", conn))
                    {
                        conn.Open();
                        imaRaspodelu = Convert.ToInt32(cmd.ExecuteScalar()) == 1;
                    }
                }
                catch
                {
                    imaRaspodelu = false;
                }
            }
            return imaRaspodelu.Value;
        }

        // dugme za traku iznad stavki (Prijemnica.Raspored.cs); null kad skripte nisu puštene
        private Button NapraviDugmeRaspodele()
        {
            if (!ImaPLKolone || !TabelaRaspodelePostoji())
                return null;
            var b = new Button
            {
                Text = "Raspodela po kontejnerima",
                AutoSize = true,
                Height = 32,
                MinimumSize = new Size(150, 32),
                UseVisualStyleBackColor = true,
                Margin = new Padding(16, 0, 0, 0)
            };
            b.Click += (s, e) => OtvoriRaspodelu();
            return b;
        }

        private void OtvoriRaspodelu()
        {
            ZavrsiUnosUGridu();
            if (txtPrijemnica.Text == "")
            {
                MessageBox.Show("Prvo snimite prijemnicu (Snimi), pa rasporedite stavke po kontejnerima.");
                return;
            }
            if (list.Count == 0)
            {
                MessageBox.Show("Prijemnica nema stavki.");
                return;
            }
            if (PotpisStavki() != stavkeSnimak || list.Any(x => x.ID == 0))
            {
                MessageBox.Show("Stavke imaju nesnimljene izmene. Snimite (Snimi), pa rasporedite stavke po kontejnerima.");
                return;
            }

            int idPrijemnice = Convert.ToInt32(txtPrijemnica.Text);
            List<KontejnerNaloga> kontejneri;
            Dictionary<Tuple<int, string>, int> raspodela;
            try
            {
                kontejneri = UcitajKontejnereNaloga();
                raspodela = UcitajRaspodelu(idPrijemnice);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Raspodela nije učitana: " + ex.Message);
                return;
            }

            // kontejner iz ranije raspodele koga više nema na nalogu ostaje kao kolona, da se vidi i ispravi
            foreach (var broj in raspodela.Keys.Select(k => k.Item2).Distinct())
                if (!kontejneri.Any(k => string.Equals(k.Broj, broj, StringComparison.OrdinalIgnoreCase)))
                    kontejneri.Add(new KontejnerNaloga { Broj = broj, NaNalogu = false });

            // RAZ-09: nema raspodele, a packing lista ima kolonu kontejnera: predlog iz fajla (svaka stavka nastaje iz jednog reda fajla)
            string porukaPL = null;
            if (raspodela.Count == 0)
            {
                Dictionary<int, string> izPL;
                try { izPL = KontejneriIzPackingListe(); }
                catch { izPL = new Dictionary<int, string>(); }
                if (izPL.Count > 0)
                {
                    var nepoznati = izPL.Values.Distinct(StringComparer.OrdinalIgnoreCase)
                        .Where(b => !kontejneri.Any(k => string.Equals(k.Broj, b, StringComparison.OrdinalIgnoreCase))).ToList();
                    if (nepoznati.Count > 0 && DodajKontejnereNaNalog(nepoznati))
                    {
                        kontejneri = UcitajKontejnereNaloga();
                        nepoznati.Clear();
                    }
                    foreach (var b in nepoznati)
                        kontejneri.Add(new KontejnerNaloga { Broj = b, NaNalogu = false });

                    foreach (var s in list)
                    {
                        string b;
                        if (s.Koleta > 0 && izPL.TryGetValue(s.ID, out b))
                        {
                            string broj = kontejneri.First(k => string.Equals(k.Broj, b, StringComparison.OrdinalIgnoreCase)).Broj;
                            raspodela[Tuple.Create(s.ID, broj)] = (int)Math.Round(s.Koleta);
                        }
                    }
                    porukaPL = "Predlog je iz kolone kontejnera u packing listi (" + izPL.Count + " stavki). Proverite pa Sačuvaj.";
                }
            }

            if (kontejneri.Count == 0)
            {
                MessageBox.Show("Nalog za prijem nema kontejnere.\nKontejneri se unose na Nalogu za PRIJEM (prevoz Kontejner, dugme Tabela).",
                    "Raspodela po kontejnerima", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // jedan kontejner i nema raspodele: sve stavke su u njemu
            bool popunjenoSamo = false;
            if (raspodela.Count == 0 && kontejneri.Count == 1)
            {
                foreach (var s in list)
                    if (s.Koleta > 0)
                        raspodela[Tuple.Create(s.ID, kontejneri[0].Broj)] = (int)Math.Round(s.Koleta);
                popunjenoSamo = true;
            }

            using (var dlg = NapraviDijalogRaspodele(idPrijemnice, kontejneri, raspodela,
                    popunjenoSamo ? "Nalog ima jedan kontejner: sve stavke su upisane u njega." : porukaPL))
                dlg.ShowDialog(this);
        }

        #region Palete (RAZ-09)

        // kontejneri za izbor u dodeli paleta: sa naloga i iz raspodele; prazno = lista se ne koristi (kuca se kao ranije)
        List<string> kontejneriZaPalete = new List<string>();
        bool greskaKontejneraPovezana;

        // Kolona Kontejner u dodeli paleta postaje lista kad nalog ima kontejnere (bira se, ne kuca)
        private void PodesiKontejnereZaPalete()
        {
            kontejneriZaPalete = new List<string>();
            try
            {
                kontejneriZaPalete = UcitajKontejnereNaloga().Select(k => k.Broj).ToList();
                if (TabelaRaspodelePostoji() && txtPrijemnica.Text != "")
                    foreach (var b in UcitajRaspodelu(Convert.ToInt32(txtPrijemnica.Text)).Keys.Select(k => k.Item2))
                        if (!kontejneriZaPalete.Contains(b, StringComparer.OrdinalIgnoreCase))
                            kontejneriZaPalete.Add(b);
            }
            catch
            {
                // bez liste: kontejner se kuca kao ranije
            }

            int indeks = dgvKontejneri.Columns["Kontejner"].Index;
            dgvKontejneri.Rows.Clear();
            dgvKontejneri.Columns.RemoveAt(indeks);
            DataGridViewColumn kolona;
            if (kontejneriZaPalete.Count > 0)
            {
                var cb = new DataGridViewComboBoxColumn
                {
                    Name = "Kontejner",
                    HeaderText = "Kontejner",
                    DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton,
                    FlatStyle = FlatStyle.Flat
                };
                cb.Items.Add("");
                foreach (var b in kontejneriZaPalete)
                    cb.Items.Add(b);
                kolona = cb;
            }
            else
                kolona = new DataGridViewTextBoxColumn { Name = "Kontejner", HeaderText = "Kontejner" };
            dgvKontejneri.Columns.Insert(indeks, kolona);

            if (!greskaKontejneraPovezana)
            {
                dgvKontejneri.DataError += (s, e) => e.ThrowException = false;
                greskaKontejneraPovezana = true;
            }
        }

        // vrednost koja sme u kolonu Kontejner: sa liste (ako postoji lista), inače tekst kao ranije
        private string KontejnerZaPaletu(string predlog)
        {
            if (kontejneriZaPalete.Count == 0)
                return predlog ?? "";
            string b = BrojKontejneraIzTeksta(predlog);
            string naListi = b == null ? null : kontejneriZaPalete.FirstOrDefault(k => string.Equals(k, b, StringComparison.OrdinalIgnoreCase));
            if (naListi != null)
                return naListi;
            return kontejneriZaPalete.Count == 1 ? kontejneriZaPalete[0] : "";
        }

        // raspodela jedne stavke: kontejner -> koleta (redosled kao na nalogu)
        private List<KeyValuePair<string, int>> RaspodelaStavke(int idStavke)
        {
            var r = new List<KeyValuePair<string, int>>();
            if (!TabelaRaspodelePostoji())
                return r;
            try
            {
                using (var conn = new SqlConnection(connection))
                using (var cmd = new SqlCommand(@"SELECT rk.BrojKontejnera, rk.Koleta
                        FROM dbo.RNCarinskoStavkaKontejner rk
                        LEFT JOIN dbo.RNCarinskoKontejner k ON k.IDNaloga = rk.IDNaloga AND k.BrojKontejnera = rk.BrojKontejnera
                        WHERE rk.IDStavke = @S
                        ORDER BY ISNULL(k.RB, 32767), rk.BrojKontejnera", conn))
                {
                    cmd.Parameters.AddWithValue("@S", idStavke);
                    conn.Open();
                    using (var dr = cmd.ExecuteReader())
                        while (dr.Read())
                            r.Add(new KeyValuePair<string, int>(dr["BrojKontejnera"].ToString().Trim(), Convert.ToInt32(dr["Koleta"])));
                }
            }
            catch
            {
                // RNCarinskoKontejner ne postoji: raspodela bez redosleda naloga
                r.Clear();
                using (var conn = new SqlConnection(connection))
                using (var cmd = new SqlCommand("SELECT BrojKontejnera, Koleta FROM dbo.RNCarinskoStavkaKontejner WHERE IDStavke = @S ORDER BY BrojKontejnera", conn))
                {
                    cmd.Parameters.AddWithValue("@S", idStavke);
                    conn.Open();
                    using (var dr = cmd.ExecuteReader())
                        while (dr.Read())
                            r.Add(new KeyValuePair<string, int>(dr["BrojKontejnera"].ToString().Trim(), Convert.ToInt32(dr["Koleta"])));
                }
            }
            return r;
        }

        // tekst za potvrdu „Napravi palete“ kad se koleta po kontejneru razlikuju od raspodele; "" kad se slaže ili raspodele nema
        private string RazlikaOdRaspodele(int idStavke, List<Tuple<string, int>> redovi)
        {
            var rasp = RaspodelaStavke(idStavke);
            if (rasp.Count == 0)
                return "";
            var palete = redovi.GroupBy(x => BrojKontejneraIzTeksta(x.Item1) ?? "", StringComparer.OrdinalIgnoreCase)
                               .ToDictionary(g => g.Key, g => g.Sum(x => x.Item2), StringComparer.OrdinalIgnoreCase);
            var razlike = new List<string>();
            foreach (var k in rasp)
            {
                int naPaletama;
                palete.TryGetValue(k.Key, out naPaletama);
                if (naPaletama != k.Value)
                    razlike.Add(k.Key + ": raspodela " + k.Value + ", palete " + naPaletama);
            }
            foreach (var p in palete)
                if (!rasp.Any(k => string.Equals(k.Key, p.Key, StringComparison.OrdinalIgnoreCase)))
                    razlike.Add((p.Key == "" ? "bez kontejnera" : p.Key) + ": nije u raspodeli, palete " + p.Value);
            return razlike.Count == 0 ? ""
                : "\n\nPAŽNJA: palete se ne slažu sa raspodelom po kontejnerima:\n" + string.Join("\n", razlike);
        }

        #endregion

        // "MSDU 118443-2" -> "MSDU1184432"; prazno -> null
        internal static string BrojKontejneraIzTeksta(string tekst)
        {
            if (string.IsNullOrWhiteSpace(tekst))
                return null;
            string b = new string(tekst.Where(char.IsLetterOrDigit).ToArray()).ToUpperInvariant();
            if (b == "")
                return null;
            return b.Length > 20 ? b.Substring(0, 20) : b;
        }

        // RAZ-09: kontejner iz packing liste po stavci (stavka -> red fajla -> kolona Kontejner)
        private Dictionary<int, string> KontejneriIzPackingListe()
        {
            var r = new Dictionary<int, string>();
            var poRedu = list.Where(s => s.ID > 0 && s.IDPLStavke.HasValue).ToDictionary(s => s.IDPLStavke.Value, s => s.ID);
            if (poRedu.Count == 0)
                return r;
            using (var conn = new SqlConnection(connection))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT CASE WHEN COL_LENGTH('dbo.RNCarinskoPackingListaStavka', 'Kontejner') IS NULL THEN 0 ELSE 1 END", conn))
                    if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                        return r;
                using (var cmd = new SqlCommand("SELECT ID, Kontejner FROM dbo.RNCarinskoPackingListaStavka WHERE ID IN (" +
                        string.Join(",", poRedu.Keys) + ") AND Kontejner IS NOT NULL AND Kontejner <> ''", conn))
                using (var dr = cmd.ExecuteReader())
                    while (dr.Read())
                    {
                        string b = BrojKontejneraIzTeksta(dr["Kontejner"].ToString());
                        if (b != null)
                            r[poRedu[Convert.ToInt32(dr["ID"])]] = b;
                    }
            }
            return r;
        }

        // Kontejner iz fajla koga nema na nalogu: ponuda da se doda u tabelu kontejnera naloga za prijem.
        // Broj mora biti 4 slova + 7 cifara (provera iz Carina_Kontejneri_2026_11.sql); ostali ostaju kao „nije na nalogu“.
        private bool DodajKontejnereNaNalog(List<string> brojevi)
        {
            var ispravni = brojevi.Where(b => System.Text.RegularExpressions.Regex.IsMatch(b, "^[A-Z]{4}[0-9]{7}$")).ToList();
            var neispravni = brojevi.Except(ispravni).ToList();
            if (ispravni.Count == 0)
            {
                MessageBox.Show("Packing lista ima kontejnere kojih nema na nalogu, a broj nije oblika 4 slova + 7 cifara: " +
                    string.Join(", ", neispravni) + ".\nIspravite fajl ili nalog.", "Raspodela po kontejnerima", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (MessageBox.Show("Packing lista ima kontejnere kojih nema na Nalogu za PRIJEM:\n" + string.Join(", ", ispravni) +
                    (neispravni.Count > 0 ? "\n(neispravan broj, ne dodaje se: " + string.Join(", ", neispravni) + ")" : "") +
                    "\n\nDodati ih na nalog?", "Raspodela po kontejnerima", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return false;
            try
            {
                using (var conn = new SqlConnection(connection))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT CASE WHEN OBJECT_ID('dbo.RNCarinskoKontejner', 'U') IS NULL THEN 0 ELSE 1 END", conn))
                        if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                        {
                            MessageBox.Show("Tabela kontejnera ne postoji (Carina_Kontejneri_2026_11.sql nije puštena).");
                            return false;
                        }
                    using (var tr = conn.BeginTransaction())
                    {
                        foreach (var b in ispravni)
                            using (var ins = new SqlCommand(@"IF NOT EXISTS (SELECT 1 FROM dbo.RNCarinskoKontejner WHERE IDNaloga = @RN AND BrojKontejnera = @Broj)
                                    INSERT INTO dbo.RNCarinskoKontejner (IDNaloga, RB, BrojKontejnera, Napomena, NameOper)
                                    SELECT @RN, ISNULL(MAX(RB), 0) + 1, @Broj, N'iz packing liste', @Korisnik FROM dbo.RNCarinskoKontejner WHERE IDNaloga = @RN", conn, tr))
                            {
                                ins.Parameters.AddWithValue("@RN", RN);
                                ins.Parameters.Add("@Broj", SqlDbType.NVarChar, 11).Value = b;
                                ins.Parameters.Add("@Korisnik", SqlDbType.NVarChar, 50).Value = (Korisnik ?? "").Trim();
                                ins.ExecuteNonQuery();
                            }
                        // zbirna lista za ekrane koji čitaju BrojKontejneraPrijem (kao Snimi na nalogu)
                        using (var upd = new SqlCommand(@"UPDATE rns SET BrojKontejneraPrijem = LEFT(x.Lista,
                                    CASE WHEN c.max_length = -1 THEN 4000 WHEN TYPE_NAME(c.user_type_id) IN ('nvarchar', 'nchar') THEN c.max_length / 2 ELSE c.max_length END)
                                FROM dbo.RadniNalogSkladista rns
                                CROSS APPLY (SELECT STUFF((SELECT N', ' + k.BrojKontejnera FROM dbo.RNCarinskoKontejner k
                                                           WHERE k.IDNaloga = rns.ID ORDER BY k.RB FOR XML PATH('')), 1, 2, N'') AS Lista) x
                                JOIN sys.columns c ON c.object_id = OBJECT_ID('dbo.RadniNalogSkladista') AND c.name = 'BrojKontejneraPrijem'
                                WHERE rns.ID = @RN", conn, tr))
                        {
                            upd.Parameters.AddWithValue("@RN", RN);
                            upd.ExecuteNonQuery();
                        }
                        tr.Commit();
                    }
                }
                txtKontejnerPrijem.Text = string.Join(", ", UcitajKontejnereNaloga().Select(k => k.Broj));
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kontejneri nisu dodati na nalog: " + ex.Message);
                return false;
            }
        }

        // kontejneri naloga: tabela sa naloga, a za stare naloge zbirni tekst BrojKontejneraPrijem
        private List<KontejnerNaloga> UcitajKontejnereNaloga()
        {
            var lista = new List<KontejnerNaloga>();
            using (var conn = new SqlConnection(connection))
            {
                conn.Open();
                bool tabela;
                using (var cmd = new SqlCommand("SELECT CASE WHEN OBJECT_ID('dbo.RNCarinskoKontejner', 'U') IS NULL THEN 0 ELSE 1 END", conn))
                    tabela = Convert.ToInt32(cmd.ExecuteScalar()) == 1;
                if (tabela)
                {
                    using (var cmd = new SqlCommand("SELECT BrojKontejnera, Koleta FROM dbo.RNCarinskoKontejner WHERE IDNaloga = @RN ORDER BY RB, ID", conn))
                    {
                        cmd.Parameters.AddWithValue("@RN", RN);
                        using (var dr = cmd.ExecuteReader())
                            while (dr.Read())
                                lista.Add(new KontejnerNaloga
                                {
                                    Broj = dr["BrojKontejnera"].ToString().Trim(),
                                    Koleta = dr["Koleta"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["Koleta"]),
                                    NaNalogu = true
                                });
                    }
                }
            }
            if (lista.Count == 0)
            {
                foreach (var deo in (txtKontejnerPrijem.Text ?? "").Split(new[] { ',', ';', '/' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    string broj = deo.Replace(" ", "").Replace("-", "").Trim().ToUpperInvariant();
                    if (broj.Length > 0 && !lista.Any(k => k.Broj == broj))
                        lista.Add(new KontejnerNaloga { Broj = broj.Length > 20 ? broj.Substring(0, 20) : broj, NaNalogu = true });
                }
            }
            return lista;
        }

        private Dictionary<Tuple<int, string>, int> UcitajRaspodelu(int idPrijemnice)
        {
            var r = new Dictionary<Tuple<int, string>, int>();
            using (var conn = new SqlConnection(connection))
            using (var cmd = new SqlCommand("SELECT IDStavke, BrojKontejnera, Koleta FROM dbo.RNCarinskoStavkaKontejner WHERE IDPrijemnice = @P", conn))
            {
                cmd.Parameters.AddWithValue("@P", idPrijemnice);
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                    while (dr.Read())
                    {
                        int idStavke = Convert.ToInt32(dr["IDStavke"]);
                        if (list.Any(s => s.ID == idStavke))   // obrisane stavke se ne prikazuju (brišu se pri snimanju)
                            r[Tuple.Create(idStavke, dr["BrojKontejnera"].ToString().Trim())] = Convert.ToInt32(dr["Koleta"]);
                    }
            }
            return r;
        }

        private Form NapraviDijalogRaspodele(int idPrijemnice, List<KontejnerNaloga> kontejneri,
            Dictionary<Tuple<int, string>, int> raspodela, string napomena)
        {
            var dlg = new Form
            {
                Text = "Raspodela po kontejnerima – prijemnica " + idPrijemnice,
                StartPosition = FormStartPosition.CenterParent,
                Size = new Size(Math.Min(1400, 560 + kontejneri.Count * 120), 560),
                MinimizeBox = false,
                Font = new Font("Segoe UI", 10f),
                BackColor = Color.FromArgb(0xF4, 0xF7, 0xFA)
            };

            var lblUputstvo = new Label
            {
                Dock = DockStyle.Top,
                Height = 44,
                Padding = new Padding(20, 8, 20, 0),
                ForeColor = Color.FromArgb(0x4C, 0x5D, 0x6E),
                Font = new Font("Segoe UI", 9f),
                Text = "U ćeliju upišite koliko koleta stavke je stiglo u tom kontejneru. Zbir reda treba da bude jednak koletima stavke, " +
                       "a zbir kolone koletima kontejnera sa naloga." + (string.IsNullOrEmpty(napomena) ? "" : "\n" + napomena)
            };

            var dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.CellSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                EnableHeadersVisualStyles = false,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
            };
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0xEA, 0xF0, 0xF6);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9f);
            dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(0x17, 0x24, 0x33);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0xDD, 0xF1, 0xFB);
            dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(0x17, 0x24, 0x33);
            dgv.RowTemplate.Height = 30;

            Func<string, string, int, bool, DataGridViewTextBoxColumn> kolona = (ime, naslov, sirina, broj) =>
            {
                var c = new DataGridViewTextBoxColumn { Name = ime, HeaderText = naslov, Width = sirina, SortMode = DataGridViewColumnSortMode.NotSortable };
                if (broj) c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                return c;
            };
            dgv.Columns.Add(kolona(KolRB, "RB", 45, true));
            dgv.Columns.Add(kolona(KolArtikal, "Artikal", 220, false));
            dgv.Columns.Add(kolona(KolKoleta, "Koleta stavke", 80, true));
            foreach (var k in kontejneri)
            {
                var c = kolona(PrefiksKontejnera + k.Broj, k.Broj + (k.NaNalogu ? "" : "\n(nije na nalogu)"), 110, true);
                if (!k.NaNalogu) c.HeaderCell.Style.ForeColor = Color.FromArgb(0xB3, 0x26, 0x1E);
                dgv.Columns.Add(c);
            }
            dgv.Columns.Add(kolona(KolZbir, "Zbir", 70, true));
            dgv.Columns.Add(kolona(KolStanje, "Stanje", 130, false));
            dgv.Columns[KolRB].Frozen = dgv.Columns[KolArtikal].Frozen = dgv.Columns[KolKoleta].Frozen = true;

            var stavke = list.OrderBy(s => s.RB).ToList();
            foreach (var s in stavke)
            {
                int i = dgv.Rows.Add();
                var red = dgv.Rows[i];
                red.Tag = s;
                red.Cells[KolRB].Value = s.RB;
                red.Cells[KolArtikal].Value = string.IsNullOrEmpty(s.Naziv) ? s.Naimenovanje : s.Naziv;
                red.Cells[KolKoleta].Value = s.Koleta.ToString("0.###");
                foreach (var k in kontejneri)
                {
                    int v;
                    if (raspodela.TryGetValue(Tuple.Create(s.ID, k.Broj), out v))
                        red.Cells[PrefiksKontejnera + k.Broj].Value = v;
                }
            }
            // zbirni redovi (samo čitanje)
            int redZbir = dgv.Rows.Add(), redNalog = dgv.Rows.Add(), redStanje = dgv.Rows.Add();
            dgv.Rows[redZbir].Cells[KolArtikal].Value = "Zbir po kontejneru";
            dgv.Rows[redNalog].Cells[KolArtikal].Value = "Koleta na nalogu";
            dgv.Rows[redStanje].Cells[KolArtikal].Value = "Stanje";
            foreach (int r in new[] { redZbir, redNalog, redStanje })
            {
                dgv.Rows[r].ReadOnly = true;
                dgv.Rows[r].DefaultCellStyle.BackColor = Color.FromArgb(0xF4, 0xF7, 0xFA);
                dgv.Rows[r].DefaultCellStyle.Font = new Font("Segoe UI Semibold", 9f);
            }
            foreach (DataGridViewColumn c in dgv.Columns)
                if (!c.Name.StartsWith(PrefiksKontejnera))
                    c.ReadOnly = true;

            Action preracunaj = () => PreracunajRaspodelu(dgv, kontejneri, stavke.Count, redZbir, redNalog, redStanje);
            dgv.CellValidating += (s, e) =>
            {
                if (e.RowIndex >= stavke.Count || !dgv.Columns[e.ColumnIndex].Name.StartsWith(PrefiksKontejnera))
                    return;
                string t = Convert.ToString(e.FormattedValue).Trim();
                int v;
                if (t != "" && (!int.TryParse(t, out v) || v < 0))
                {
                    MessageBox.Show("Upišite ceo broj koleta (0 ili više).", "Raspodela po kontejnerima");
                    e.Cancel = true;
                }
            };
            dgv.CellEndEdit += (s, e) => preracunaj();
            dgv.DataError += (s, e) => e.ThrowException = false;
            preracunaj();

            var traka = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 52,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(20, 10, 20, 10)
            };
            var btnSacuvaj = new Button
            {
                Text = "Sačuvaj",
                Size = new Size(120, 32),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0x0A, 0x66, 0xB8),
                ForeColor = Color.White,
                Tag = "primary"
            };
            btnSacuvaj.FlatAppearance.BorderSize = 0;
            var btnZatvori = new Button
            {
                Text = "Zatvori",
                Size = new Size(110, 32),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 8, 0)
            };
            btnZatvori.FlatAppearance.BorderColor = Color.FromArgb(0xB9, 0xC6, 0xD3);
            btnZatvori.Click += (s, e) => dlg.Close();
            btnSacuvaj.Click += (s, e) =>
            {
                dgv.EndEdit();
                preracunaj();
                if (SnimiRaspodelu(idPrijemnice, dgv, kontejneri, stavke))
                    dlg.Close();
            };
            traka.Controls.Add(btnSacuvaj);
            traka.Controls.Add(btnZatvori);

            dlg.Controls.Add(dgv);
            dlg.Controls.Add(traka);
            dlg.Controls.Add(lblUputstvo);
            dlg.AcceptButton = null;   // Enter u ćeliji ne zatvara prozor
            dlg.CancelButton = btnZatvori;
            return dlg;
        }

        private static int KoletaUCeliji(object v)
        {
            int r;
            return v != null && int.TryParse(v.ToString().Trim(), out r) ? r : 0;
        }

        private static string OpisRazlike(decimal razlika)
        {
            if (razlika == 0) return "Slaže se";
            return razlika > 0 ? "Nedostaje " + razlika.ToString("0.###") : "Višak " + (-razlika).ToString("0.###");
        }

        private static readonly Color BojaSlaze = Color.FromArgb(0x1E, 0x7A, 0x3C);
        private static readonly Color BojaRazlika = Color.FromArgb(0xB3, 0x26, 0x1E);

        private void PreracunajRaspodelu(DataGridView dgv, List<KontejnerNaloga> kontejneri, int brojStavki, int redZbir, int redNalog, int redStanje)
        {
            // po stavci
            for (int i = 0; i < brojStavki; i++)
            {
                var red = dgv.Rows[i];
                var s = (PrijemnicaStavke)red.Tag;
                int zbir = kontejneri.Sum(k => KoletaUCeliji(red.Cells[PrefiksKontejnera + k.Broj].Value));
                red.Cells[KolZbir].Value = zbir;
                red.Cells[KolStanje].Value = OpisRazlike(s.Koleta - zbir);
                red.Cells[KolStanje].Style.ForeColor = s.Koleta == zbir ? BojaSlaze : BojaRazlika;
            }
            // po kontejneru
            int ukupno = 0;
            foreach (var k in kontejneri)
            {
                string ime = PrefiksKontejnera + k.Broj;
                int zbir = 0;
                for (int i = 0; i < brojStavki; i++)
                    zbir += KoletaUCeliji(dgv.Rows[i].Cells[ime].Value);
                ukupno += zbir;
                dgv.Rows[redZbir].Cells[ime].Value = zbir;
                var stanje = dgv.Rows[redStanje].Cells[ime];
                if (!k.NaNalogu)
                {
                    dgv.Rows[redNalog].Cells[ime].Value = "–";
                    stanje.Value = zbir == 0 ? "Prazno" : "Nije na nalogu";
                    stanje.Style.ForeColor = zbir == 0 ? BojaSlaze : BojaRazlika;
                }
                else if (k.Koleta.HasValue)
                {
                    dgv.Rows[redNalog].Cells[ime].Value = k.Koleta.Value;
                    stanje.Value = OpisRazlike(k.Koleta.Value - zbir);
                    stanje.Style.ForeColor = k.Koleta.Value == zbir ? BojaSlaze : BojaRazlika;
                }
                else
                {
                    dgv.Rows[redNalog].Cells[ime].Value = "";
                    stanje.Value = "Koleta nisu na nalogu";
                    stanje.Style.ForeColor = Color.FromArgb(0x4C, 0x5D, 0x6E);
                }
            }
            dgv.Rows[redZbir].Cells[KolZbir].Value = ukupno;
            decimal koletaStavki = list.Sum(s => s.Koleta);
            dgv.Rows[redZbir].Cells[KolKoleta].Value = koletaStavki.ToString("0.###");
            dgv.Rows[redZbir].Cells[KolStanje].Value = OpisRazlike(koletaStavki - ukupno);
            dgv.Rows[redZbir].Cells[KolStanje].Style.ForeColor = koletaStavki == ukupno ? BojaSlaze : BojaRazlika;
        }

        private bool SnimiRaspodelu(int idPrijemnice, DataGridView dgv, List<KontejnerNaloga> kontejneri, List<PrijemnicaStavke> stavke)
        {
            // provera zbirova: snimanje je dozvoljeno i kad se ne slaže, ali uz potvrdu
            var razlike = new List<string>();
            for (int i = 0; i < stavke.Count; i++)
            {
                int zbir = KoletaUCeliji(dgv.Rows[i].Cells[KolZbir].Value);
                if (zbir != stavke[i].Koleta)
                    razlike.Add("RB " + stavke[i].RB + ": " + OpisRazlike(stavke[i].Koleta - zbir).ToLower());
            }
            foreach (var k in kontejneri)
            {
                int zbir = 0;
                for (int i = 0; i < stavke.Count; i++)
                    zbir += KoletaUCeliji(dgv.Rows[i].Cells[PrefiksKontejnera + k.Broj].Value);
                if (!k.NaNalogu && zbir > 0)
                    razlike.Add(k.Broj + ": nije na nalogu");
                else if (k.Koleta.HasValue && k.Koleta.Value != zbir)
                    razlike.Add(k.Broj + ": " + OpisRazlike(k.Koleta.Value - zbir).ToLower());
            }
            if (razlike.Count > 0 &&
                MessageBox.Show("Raspodela se ne slaže:\n" + string.Join("\n", razlike.Take(15)) + (razlike.Count > 15 ? "\n..." : "") +
                    "\n\nSačuvati ipak?", "Raspodela po kontejnerima", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return false;

            try
            {
                using (var conn = new SqlConnection(connection))
                {
                    conn.Open();
                    // zbirni tekst kontejnera na stavci (stari ekrani), ako kolona postoji; dužina u znakovima
                    int duzinaTeksta = 0;
                    using (var cmd = new SqlCommand(@"SELECT CASE WHEN c.max_length = -1 THEN 4000
                                                                  WHEN TYPE_NAME(c.user_type_id) IN ('nvarchar', 'nchar') THEN c.max_length / 2
                                                                  ELSE c.max_length END
                                                      FROM sys.columns c
                                                      WHERE c.object_id = OBJECT_ID('dbo.RNCarinskoPrijemnicaStavke') AND c.name = 'BrojKontejnera'", conn))
                    {
                        object o = cmd.ExecuteScalar();
                        if (o != null && o != DBNull.Value)
                            duzinaTeksta = Convert.ToInt32(o);
                    }

                    using (var tr = conn.BeginTransaction())
                    {
                        using (var del = new SqlCommand("DELETE FROM dbo.RNCarinskoStavkaKontejner WHERE IDPrijemnice = @P", conn, tr))
                        {
                            del.Parameters.AddWithValue("@P", idPrijemnice);
                            del.ExecuteNonQuery();
                        }

                        for (int i = 0; i < stavke.Count; i++)
                        {
                            var s = stavke[i];
                            var delovi = kontejneri
                                .Select(k => new { k.Broj, Koleta = KoletaUCeliji(dgv.Rows[i].Cells[PrefiksKontejnera + k.Broj].Value) })
                                .Where(x => x.Koleta > 0)
                                .ToList();
                            int ukupnoKoleta = delovi.Sum(x => x.Koleta);

                            // neto, bruto i vrednost srazmerno koletima; poslednji kontejner uzima ostatak, da zbir bude tačan
                            decimal netoOst = s.Neto, brutoOst = s.Bruto, vrednostOst = s.Vrednost;
                            for (int j = 0; j < delovi.Count; j++)
                            {
                                var d = delovi[j];
                                decimal neto, bruto, vrednost;
                                if (j == delovi.Count - 1)
                                {
                                    neto = netoOst; bruto = brutoOst; vrednost = vrednostOst;
                                }
                                else
                                {
                                    decimal udeo = (decimal)d.Koleta / ukupnoKoleta;
                                    neto = Math.Round(s.Neto * udeo, 3);
                                    bruto = Math.Round(s.Bruto * udeo, 3);
                                    vrednost = Math.Round(s.Vrednost * udeo, 2);
                                    netoOst -= neto; brutoOst -= bruto; vrednostOst -= vrednost;
                                }
                                using (var ins = new SqlCommand(@"INSERT INTO dbo.RNCarinskoStavkaKontejner
                                        (IDPrijemnice, IDStavke, IDNaloga, BrojKontejnera, Koleta, Neto, Bruto, Vrednost, NameOper)
                                        VALUES (@P, @S, @RN, @Broj, @Koleta, @Neto, @Bruto, @Vrednost, @Korisnik)", conn, tr))
                                {
                                    ins.Parameters.AddWithValue("@P", idPrijemnice);
                                    ins.Parameters.AddWithValue("@S", s.ID);
                                    ins.Parameters.AddWithValue("@RN", RN);
                                    ins.Parameters.Add("@Broj", SqlDbType.NVarChar, 20).Value = d.Broj;
                                    ins.Parameters.AddWithValue("@Koleta", d.Koleta);
                                    var pn = ins.Parameters.Add("@Neto", SqlDbType.Decimal); pn.Precision = 18; pn.Scale = 3; pn.Value = neto;
                                    var pb = ins.Parameters.Add("@Bruto", SqlDbType.Decimal); pb.Precision = 18; pb.Scale = 3; pb.Value = bruto;
                                    var pv = ins.Parameters.Add("@Vrednost", SqlDbType.Decimal); pv.Precision = 18; pv.Scale = 2; pv.Value = vrednost;
                                    ins.Parameters.Add("@Korisnik", SqlDbType.NVarChar, 50).Value = (Korisnik ?? "").Trim();
                                    ins.ExecuteNonQuery();
                                }
                            }

                            if (duzinaTeksta > 0)
                            {
                                string tekst = string.Join(", ", delovi.Select(x => x.Broj));
                                if (tekst.Length > duzinaTeksta)
                                    tekst = tekst.Substring(0, duzinaTeksta);
                                using (var upd = new SqlCommand("UPDATE dbo.RNCarinskoPrijemnicaStavke SET BrojKontejnera = @T WHERE ID = @S", conn, tr))
                                {
                                    upd.Parameters.AddWithValue("@T", tekst == "" ? (object)DBNull.Value : tekst);
                                    upd.Parameters.AddWithValue("@S", s.ID);
                                    upd.ExecuteNonQuery();
                                }
                            }
                        }
                        tr.Commit();
                    }
                }
                MessageBox.Show("Raspodela po kontejnerima je sačuvana.", "Raspodela po kontejnerima");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Raspodela nije sačuvana: " + ex.Message);
                return false;
            }
        }
    }
}
