using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main
{
    // Magacinski predmet izmene (25.11.2026):
    //  - Količina po JM prima 3 decimale (kao bruto i neto), polje je šire
    //  - Smeštajni dokument = broj C7 (prepisuje se u Broj JCI kao i do sada)
    //  - kad roba stiže kontejnerom unosi se TABELA kontejnera (RNCarinskoKontejner: broj, koleta, bruto, napomena);
    //    zbirna lista ostaje i u RadniNalogSkladista.BrojKontejneraPrijem, jer je čitaju Prijemnica, Otprema i drugi ekrani
    //  - na istom magacinskom broju ne mogu biti dva naloga sa postupkom C7 (C7SlobodanNaMB, na Snimi i Snimi MP);
    //    potvrđeno 26.11.2026: drugi C7 dobija svoj magacinski broj
    public partial class frmNalogPrijem
    {
        // Jedan red tabele kontejnera
        private class KontejnerRed
        {
            public string Broj;
            public int? Koleta;
            public decimal? Bruto;
            public string Napomena;
            public string Tip = TipKontejner;   // RAZ-10: KONTEJNER, VAGON ili KAMION
        }

        const string TipKontejner = "KONTEJNER", TipVagon = "VAGON", TipKamion = "KAMION";

        Label lblKontejneri;
        TextBox txtKontejneri;
        Button btnKontejneri;
        int sirinaPosebnihUslova;
        List<KontejnerRed> kontejneriLista = new List<KontejnerRed>();

        private void InitMagacinskiPredmet()
        {
            txtKolicinaJM.DecimalPlaces = 3;
            txtKolicinaJM.Width = 120;

            // Smeštajni dokument je broj C7
            lblSmestajniDokument.Text = "BROJ C7 (SMEŠT. DOK.)";
            new ToolTip().SetToolTip(txtSmestajniDokument, "Broj C7 sa isprave, npr. 2544 ili 42072/2544/11.08.2026");

            sirinaPosebnihUslova = cboPosebniUslovi.Width;
            lblKontejneri = new Label
            {
                Text = "KONTEJNERI",
                AutoSize = true,
                Font = lblPosebniUslovi.Font,
                ForeColor = lblPosebniUslovi.ForeColor,
                Visible = false
            };
            txtKontejneri = new TextBox
            {
                Font = cboPosebniUslovi.Font,
                ReadOnly = true,
                BackColor = SystemColors.Window,
                Cursor = Cursors.Hand,
                Visible = false
            };
            btnKontejneri = new Button
            {
                Text = "Tabela",
                Font = cboPosebniUslovi.Font,
                Width = 62,
                Height = cboPosebniUslovi.Height + 2,
                Visible = false
            };
            var tip = new ToolTip();
            tip.SetToolTip(txtKontejneri, "Dupli klik ili dugme Tabela: unos kontejnera iz kojih stiže roba");
            tip.SetToolTip(btnKontejneri, "Unos kontejnera iz kojih stiže roba");
            txtKontejneri.DoubleClick += (s, e) => OtvoriTabeluKontejnera();
            btnKontejneri.Click += (s, e) => OtvoriTabeluKontejnera();

            panelPrijem2.Controls.Add(lblKontejneri);
            panelPrijem2.Controls.Add(txtKontejneri);
            panelPrijem2.Controls.Add(btnKontejneri);

            cboTipTransportaPrijem.SelectedIndexChanged += (s, e) => PrikaziKontejnere();
            cboTipTransportaPrijem.TextChanged += (s, e) => PrikaziKontejnere();

            InitRbrKnjige();   // RAZ-17 (frmNalogPrijem.KnjigaRbr.cs)
            InitZamenaTarife();   // RAZ-05 (frmNalogPrijem.ZamenaTarife.cs)
        }

        // RAZ-10: vrsta prevoza -> tip transportne jedinice; null = prevoz bez tabele (kao do sada)
        private string TipJedinicePrevoza()
        {
            string t = cboTipTransportaPrijem.Text ?? "";
            Func<string, bool> ima = s => t.IndexOf(s, StringComparison.OrdinalIgnoreCase) >= 0;
            if (ima("kontejner")) return TipKontejner;
            if (ima("vagon")) return TipVagon;
            if (ima("kamion") || ima("tegljač") || ima("tegljac") || ima("šleper") || ima("sleper")) return TipKamion;
            return null;
        }

        // roba stiže kontejnerom, vagonom ili kamionom: nalog ima tabelu transportnih jedinica
        private bool UlazKontejnerom()
        {
            return TipJedinicePrevoza() != null;
        }

        private string NaslovJedinica()
        {
            switch (TipJedinicePrevoza())
            {
                case TipVagon: return "VAGONI";
                case TipKamion: return "KAMIONI";
                default: return "KONTEJNERI";
            }
        }

        // Polje i dugme za kontejnere stoje desno od posebnih uslova smeštaja, koji se tada sužavaju
        private void PrikaziKontejnere()
        {
            if (txtKontejneri == null)
                return;

            bool kontejner = UlazKontejnerom();
            lblKontejneri.Visible = txtKontejneri.Visible = btnKontejneri.Visible = kontejner;
            if (!kontejner)
            {
                cboPosebniUslovi.Width = sirinaPosebnihUslova;
                return;
            }

            int desno = cboPosebniUslovi.Left + sirinaPosebnihUslova;
            cboPosebniUslovi.Width = 160;
            int x = cboPosebniUslovi.Right + 12;
            lblKontejneri.Location = new Point(x, lblPosebniUslovi.Top);
            txtKontejneri.Location = new Point(x, cboPosebniUslovi.Top);
            txtKontejneri.Width = desno - x - btnKontejneri.Width - 4;
            btnKontejneri.Location = new Point(txtKontejneri.Right + 4, cboPosebniUslovi.Top - 1);
            OsveziPrikazKontejnera();
        }

        private void OsveziPrikazKontejnera()
        {
            if (txtKontejneri == null)
                return;
            txtKontejneri.Text = kontejneriLista.Count == 0
                ? ""
                : kontejneriLista.Count + ": " + string.Join(", ", kontejneriLista.Select(k => k.Broj));
            lblKontejneri.Text = kontejneriLista.Count == 0 ? NaslovJedinica() : NaslovJedinica() + " (" + kontejneriLista.Count + ")";
        }

        private string ZbirnaListaKontejnera()
        {
            return string.Join(", ", kontejneriLista.Select(k => k.Broj));
        }

        // ---------- Tabela kontejnera (dijalog) ----------

        private void OtvoriTabeluKontejnera()
        {
            using (var f = new Form())
            {
                f.Text = "Transportne jedinice – nalog " + (string.IsNullOrWhiteSpace(txtID.Text) ? "(nov)" : txtID.Text);
                f.StartPosition = FormStartPosition.CenterParent;
                f.Size = new Size(720, 420);
                f.MinimizeBox = f.MaximizeBox = false;
                f.FormBorderStyle = FormBorderStyle.FixedDialog;
                f.Font = cboPosebniUslovi.Font;

                var lbl = new Label
                {
                    Text = "Jedan red = jedan kontejner, vagon ili kamion. Kontejner: 4 slova + 7 cifara (npr. MSDU1184432);\n" +
                           "vagon: broj vagona; kamion: registracija (npr. BABD925). Koleta i bruto su opcioni.",
                    Dock = DockStyle.Top,
                    Height = 44,
                    Padding = new Padding(6, 8, 6, 0)
                };

                var dgv = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    AllowUserToAddRows = true,
                    AllowUserToDeleteRows = true,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    RowHeadersWidth = 30,
                    SelectionMode = DataGridViewSelectionMode.RowHeaderSelect
                };
                var colRB = new DataGridViewTextBoxColumn { Name = "RB", HeaderText = "RB", ReadOnly = true, FillWeight = 12 };
                var colTip = new DataGridViewComboBoxColumn { Name = "Tip", HeaderText = "Vrsta", FillWeight = 26, FlatStyle = FlatStyle.Flat };
                colTip.Items.AddRange(TipKontejner, TipVagon, TipKamion);
                var colBroj = new DataGridViewTextBoxColumn { Name = "Broj", HeaderText = "Broj / oznaka", MaxInputLength = 20, FillWeight = 40 };
                var colKoleta = new DataGridViewTextBoxColumn { Name = "Koleta", HeaderText = "Koleta", FillWeight = 20 };
                var colBruto = new DataGridViewTextBoxColumn { Name = "Bruto", HeaderText = "Bruto kg", FillWeight = 25 };
                var colNap = new DataGridViewTextBoxColumn { Name = "Napomena", HeaderText = "Napomena", MaxInputLength = 200, FillWeight = 50 };
                colKoleta.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                colBruto.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                colBruto.DefaultCellStyle.Format = "N3";
                dgv.Columns.AddRange(colRB, colTip, colBroj, colKoleta, colBruto, colNap);
                // nov red dobija vrstu po vrsti prevoza
                dgv.DefaultValuesNeeded += (s, e) => e.Row.Cells["Tip"].Value = TipJedinicePrevoza() ?? TipKontejner;
                dgv.DataError += (s, e) => e.ThrowException = false;

                foreach (var k in kontejneriLista)
                    dgv.Rows.Add(null, k.Tip ?? TipKontejner, k.Broj, k.Koleta?.ToString(), k.Bruto?.ToString("N3"), k.Napomena);

                Action numerisi = () =>
                {
                    int rb = 1;
                    foreach (DataGridViewRow r in dgv.Rows)
                        if (!r.IsNewRow)
                            r.Cells["RB"].Value = rb++;
                };
                numerisi();
                dgv.RowsAdded += (s, e) => numerisi();
                dgv.RowsRemoved += (s, e) => numerisi();
                dgv.EditingControlShowing += (s, e) =>
                {
                    if (dgv.CurrentCell.OwningColumn == colBroj && e.Control is TextBox tb)
                        tb.CharacterCasing = CharacterCasing.Upper;
                    else if (e.Control is TextBox tb2)
                        tb2.CharacterCasing = CharacterCasing.Normal;
                };

                var dole = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 44, FlowDirection = FlowDirection.RightToLeft, Padding = new Padding(6) };
                var btnOdustani = new Button { Text = "Odustani", DialogResult = DialogResult.Cancel, Width = 90 };
                var btnPotvrdi = new Button { Text = "Potvrdi", Width = 90 };
                var btnUkloni = new Button { Text = "Ukloni red", Width = 100 };
                btnUkloni.Click += (s, e) =>
                {
                    foreach (DataGridViewRow r in dgv.SelectedRows)
                        if (!r.IsNewRow)
                            dgv.Rows.Remove(r);
                    if (dgv.SelectedRows.Count == 0 && dgv.CurrentRow != null && !dgv.CurrentRow.IsNewRow)
                        dgv.Rows.Remove(dgv.CurrentRow);
                };
                dole.Controls.Add(btnOdustani);
                dole.Controls.Add(btnPotvrdi);
                dole.Controls.Add(btnUkloni);

                btnPotvrdi.Click += (s, e) =>
                {
                    dgv.EndEdit();
                    if (ProcitajTabeluKontejnera(dgv, out var nova))
                    {
                        kontejneriLista = nova;
                        f.DialogResult = DialogResult.OK;
                    }
                };

                f.Controls.Add(dgv);
                f.Controls.Add(lbl);
                f.Controls.Add(dole);
                f.AcceptButton = null;
                f.CancelButton = btnOdustani;

                if (f.ShowDialog(this) == DialogResult.OK)
                    OsveziPrikazKontejnera();
            }
        }

        // Provera redova tabele: format, kontrolna cifra (upozorenje), duplikati, brojevi
        private bool ProcitajTabeluKontejnera(DataGridView dgv, out List<KontejnerRed> lista)
        {
            lista = new List<KontejnerRed>();
            var losaCifra = new List<string>();

            foreach (DataGridViewRow r in dgv.Rows)
            {
                if (r.IsNewRow)
                    continue;
                string broj = Regex.Replace(Convert.ToString(r.Cells["Broj"].Value) ?? "", @"[\s\-/.]", "").ToUpperInvariant();
                string koletaTxt = (Convert.ToString(r.Cells["Koleta"].Value) ?? "").Trim();
                string brutoTxt = (Convert.ToString(r.Cells["Bruto"].Value) ?? "").Trim();
                string napomena = (Convert.ToString(r.Cells["Napomena"].Value) ?? "").Trim();
                string tip = Convert.ToString(r.Cells["Tip"].Value);
                if (tip != TipVagon && tip != TipKamion)
                    tip = TipKontejner;

                if (broj == "" && koletaTxt == "" && brutoTxt == "" && napomena == "")
                    continue; // prazan red

                int rb = r.Index + 1;
                if (tip == TipKontejner && !Regex.IsMatch(broj, @"^[A-Z]{4}\d{7}$"))
                {
                    MessageBox.Show("Red " + rb + ": broj kontejnera mora imati 4 slova i 7 cifara (npr. MSDU1184432).", "Kontejneri",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dgv.CurrentCell = r.Cells["Broj"];
                    return false;
                }
                // RAZ-10: vagon (broj vagona) i kamion (registracija): slova i cifre, 2 do 20 znakova; ISO 6346 se ne proverava
                if (tip != TipKontejner && !Regex.IsMatch(broj, @"^[A-ZČĆŽŠĐ0-9]{2,20}$"))
                {
                    MessageBox.Show("Red " + rb + ": " + (tip == TipVagon ? "broj vagona" : "registracija kamiona") +
                        " ima samo slova i cifre, od 2 do 20 znakova.", "Transportne jedinice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dgv.CurrentCell = r.Cells["Broj"];
                    return false;
                }
                if (lista.Any(k => k.Broj == broj))
                {
                    MessageBox.Show("Red " + rb + ": " + broj + " je već upisan.", "Transportne jedinice",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dgv.CurrentCell = r.Cells["Broj"];
                    return false;
                }

                int? koleta = null;
                if (koletaTxt != "")
                {
                    if (!int.TryParse(koletaTxt.Replace(".", ""), out int k) || k < 0)
                    {
                        MessageBox.Show("Red " + rb + ": koleta mora biti ceo broj.", "Kontejneri", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgv.CurrentCell = r.Cells["Koleta"];
                        return false;
                    }
                    koleta = k;
                }

                decimal? bruto = null;
                if (brutoTxt != "")
                {
                    if (!decimal.TryParse(brutoTxt, out decimal b) || b < 0)
                    {
                        MessageBox.Show("Red " + rb + ": bruto nije ispravan broj.", "Kontejneri", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgv.CurrentCell = r.Cells["Bruto"];
                        return false;
                    }
                    bruto = Math.Round(b, 3);
                }

                if (tip == TipKontejner && !KontrolnaCifraIspravna(broj))
                    losaCifra.Add(broj);

                lista.Add(new KontejnerRed { Broj = broj, Koleta = koleta, Bruto = bruto, Napomena = napomena == "" ? null : napomena, Tip = tip });
            }

            if (losaCifra.Count > 0 &&
                MessageBox.Show("Kontrolna cifra nije ispravna za: " + string.Join(", ", losaCifra) + "\n\nDa li su brojevi ipak tačni?",
                    "Kontejneri", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return false;

            int zbirKoleta = lista.Where(k => k.Koleta.HasValue).Sum(k => k.Koleta.Value);
            if (zbirKoleta > 0 && txtKoleta.Value > 0 && zbirKoleta != txtKoleta.Value &&
                MessageBox.Show("Zbir koleta po kontejnerima je " + zbirKoleta + ", a na magacinskom predmetu " + txtKoleta.Value.ToString("0") + ".\n\nDa li ipak potvrđujete tabelu?",
                    "Kontejneri", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return false;

            return true;
        }

        // ISO 6346: kontrolna cifra je poslednja cifra broja kontejnera
        private static bool KontrolnaCifraIspravna(string broj)
        {
            int zbir = 0;
            for (int i = 0; i < 10; i++)
            {
                char c = broj[i];
                int v;
                if (char.IsDigit(c))
                    v = c - '0';
                else
                {
                    v = 10;
                    for (char z = 'A'; z < c; z++)
                    {
                        v++;
                        if (v % 11 == 0)
                            v++;
                    }
                }
                zbir += v << i;
            }
            return zbir % 11 % 10 == broj[10] - '0';
        }

        // Stari zapis (tekst u BrojKontejneraPrijem) se deli na brojeve oblika AAAU1234567
        private static List<string> RazloziKontejnere(string unos)
        {
            string cist = Regex.Replace((unos ?? "").ToUpperInvariant(), @"[\s,;\-/.]", "");
            return Regex.Matches(cist, @"[A-Z]{4}\d{7}").Cast<Match>().Select(m => m.Value).Distinct().ToList();
        }

        // Pozvati pre snimanja naloga
        private bool KontejneriIspravni()
        {
            if (!UlazKontejnerom() || kontejneriLista.Count > 0)
                return true;

            return MessageBox.Show("Roba stiže (" + cboTipTransportaPrijem.Text.Trim() + "), a tabela " + NaslovJedinica().ToLower() + " je prazna.\n\nDa li ipak želite da snimite nalog?",
                "Kontejneri", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }

        // ---------- Baza ----------

        private static bool TabelaKontejneraPostoji(SqlConnection conn)
        {
            var cmd = new SqlCommand("SELECT CASE WHEN OBJECT_ID('dbo.RNCarinskoKontejner', 'U') IS NULL THEN 0 ELSE 1 END", conn);
            return Convert.ToInt32(cmd.ExecuteScalar()) == 1;
        }

        // RAZ-10: kolona Tip postoji posle skripte Carina_TransportneJedinice_2026_11.sql
        private static bool KolonaTipJedinicePostoji(SqlConnection conn, SqlTransaction tr = null)
        {
            var cmd = new SqlCommand("SELECT CASE WHEN COL_LENGTH('dbo.RNCarinskoKontejner', 'Tip') IS NULL THEN 0 ELSE 1 END", conn, tr);
            return Convert.ToInt32(cmd.ExecuteScalar()) == 1;
        }

        // Poziva se iz VratiPodatke; zbirnaLista je stara vrednost BrojKontejneraPrijem
        private void UcitajKontejnere(int idNaloga, string zbirnaLista)
        {
            kontejneriLista = new List<KontejnerRed>();
            try
            {
                using (var conn = new SqlConnection(connection))
                {
                    conn.Open();
                    if (TabelaKontejneraPostoji(conn))
                    {
                        var cmd = new SqlCommand(@"SELECT BrojKontejnera, Koleta, Bruto, Napomena, " +
                                                   (KolonaTipJedinicePostoji(conn) ? "Tip" : "N'KONTEJNER' AS Tip") + @"
                                                   FROM dbo.RNCarinskoKontejner
                                                   WHERE IDNaloga = @ID ORDER BY RB, ID", conn);
                        cmd.Parameters.AddWithValue("@ID", idNaloga);
                        using (var dr = cmd.ExecuteReader())
                            while (dr.Read())
                                kontejneriLista.Add(new KontejnerRed
                                {
                                    Broj = dr.GetString(0),
                                    Koleta = dr.IsDBNull(1) ? (int?)null : dr.GetInt32(1),
                                    Bruto = dr.IsDBNull(2) ? (decimal?)null : dr.GetDecimal(2),
                                    Napomena = dr.IsDBNull(3) ? null : dr.GetString(3),
                                    Tip = dr.IsDBNull(4) ? TipKontejner : dr.GetString(4).Trim()
                                });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kontejneri nisu učitani: " + ex.Message);
            }

            // Nalog snimljen pre tabele: preuzmi brojeve iz zbirnog teksta
            if (kontejneriLista.Count == 0)
                foreach (var b in RazloziKontejnere(zbirnaLista))
                    kontejneriLista.Add(new KontejnerRed { Broj = b });

            OsveziPrikazKontejnera();
            PrikaziKontejnere();
        }

        // Poziva se iz Snimi posle upisa naloga (kad nalog ima ID)
        private void SnimiKontejnere()
        {
            if (!int.TryParse(txtID.Text, out int id) || id <= 0)
                return;

            bool kontejner = UlazKontejnerom();
            var lista = kontejner ? kontejneriLista : new List<KontejnerRed>();
            string zbirno = lista.Count == 0 ? null : string.Join(", ", lista.Select(k => k.Broj));

            try
            {
                using (var conn = new SqlConnection(connection))
                {
                    conn.Open();
                    bool imaTabelu = TabelaKontejneraPostoji(conn);
                    bool imaTip = imaTabelu && KolonaTipJedinicePostoji(conn);
                    if (imaTabelu && !imaTip && lista.Any(k => k.Tip != TipKontejner))
                    {
                        MessageBox.Show("Vagoni i kamioni nisu snimljeni: potrebna je skripta Carina_TransportneJedinice_2026_11.sql.",
                            "Transportne jedinice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // zbirna lista za ekrane koji čitaju BrojKontejneraPrijem; dužina kolone u znakovima (-1 = max)
                    var cmdDuzina = new SqlCommand(@"SELECT CASE WHEN c.max_length = -1 THEN -1
                                                                 WHEN TYPE_NAME(c.user_type_id) IN ('nvarchar', 'nchar') THEN c.max_length / 2
                                                                 ELSE c.max_length END
                                                     FROM sys.columns c
                                                     WHERE c.object_id = OBJECT_ID('dbo.RadniNalogSkladista') AND c.name = 'BrojKontejneraPrijem'", conn);
                    object o = cmdDuzina.ExecuteScalar();
                    int duzina = o == null || o == DBNull.Value ? -1 : Convert.ToInt32(o);
                    if (zbirno != null && duzina > 0 && zbirno.Length > duzina)
                    {
                        if (!imaTabelu)
                        {
                            MessageBox.Show("Kontejneri nisu snimljeni: lista ima " + zbirno.Length + " znakova, a polje u bazi prima " + duzina +
                                ".\nPokrenite skriptu Carina_Kontejneri_2026_11.sql.", "Kontejneri", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        zbirno = zbirno.Substring(0, duzina);   // ceo spisak je u tabeli
                    }

                    using (var tr = conn.BeginTransaction())
                    {
                        var cmd = new SqlCommand("UPDATE RadniNalogSkladista SET BrojKontejneraPrijem = @Kontejneri WHERE ID = @ID", conn, tr);
                        cmd.Parameters.AddWithValue("@Kontejneri", (object)zbirno ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.ExecuteNonQuery();

                        if (imaTabelu)
                        {
                            var del = new SqlCommand("DELETE FROM dbo.RNCarinskoKontejner WHERE IDNaloga = @ID", conn, tr);
                            del.Parameters.AddWithValue("@ID", id);
                            del.ExecuteNonQuery();

                            short rb = 1;
                            foreach (var k in lista)
                            {
                                var ins = new SqlCommand(@"INSERT INTO dbo.RNCarinskoKontejner (IDNaloga, RB, BrojKontejnera, Koleta, Bruto, Napomena, NameOper" + (imaTip ? ", Tip" : "") + @")
                                                           VALUES (@ID, @RB, @Broj, @Koleta, @Bruto, @Napomena, @Korisnik" + (imaTip ? ", @Tip" : "") + ")", conn, tr);
                                ins.Parameters.AddWithValue("@ID", id);
                                ins.Parameters.Add("@RB", SqlDbType.SmallInt).Value = rb++;
                                ins.Parameters.Add("@Broj", SqlDbType.NVarChar, 20).Value = k.Broj;
                                if (imaTip)
                                    ins.Parameters.Add("@Tip", SqlDbType.NVarChar, 10).Value = k.Tip ?? TipKontejner;
                                ins.Parameters.Add("@Koleta", SqlDbType.Int).Value = (object)k.Koleta ?? DBNull.Value;
                                var pb = ins.Parameters.Add("@Bruto", SqlDbType.Decimal);
                                pb.Precision = 18; pb.Scale = 3; pb.Value = (object)k.Bruto ?? DBNull.Value;
                                ins.Parameters.Add("@Napomena", SqlDbType.NVarChar, 200).Value = (object)k.Napomena ?? DBNull.Value;
                                ins.Parameters.Add("@Korisnik", SqlDbType.NVarChar, 50).Value = (object)Korisnik ?? DBNull.Value;
                                ins.ExecuteNonQuery();
                            }
                        }
                        tr.Commit();
                    }

                    if (!imaTabelu && lista.Any(k => k.Koleta.HasValue || k.Bruto.HasValue || k.Napomena != null))
                        MessageBox.Show("Snimljeni su samo brojevi kontejnera. Koleta, bruto i napomena se čuvaju posle pokretanja skripte Carina_Kontejneri_2026_11.sql.",
                            "Kontejneri", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kontejneri nisu snimljeni: " + ex.Message);
            }
        }

        // Na istom magacinskom broju ne mogu biti dva naloga sa carinskim postupkom C7
        // Scenario Daska (C7 1296 pa C7 1356 na MB 750): drugi C7 dobija svoj magacinski broj (potvrđeno 26.11.2026).
        private bool C7SlobodanNaMB()
        {
            if (cboMagacinskiBroj.SelectedValue == null || cboCarinskiPostupak.SelectedValue == null)
                return true;
            if (!int.TryParse(cboMagacinskiBroj.SelectedValue.ToString(), out int mb) ||
                !int.TryParse(cboCarinskiPostupak.SelectedValue.ToString(), out int postupak))
                return true;
            int.TryParse(txtID.Text, out int id);

            object drugi;
            using (var conn = new SqlConnection(connection))
            {
                conn.Open();
                var cmd = new SqlCommand(@"SELECT TOP 1 rns.ID
                                           FROM RadniNalogSkladista rns
                                           INNER JOIN VrstaCarinskogPostupka v ON v.ID = rns.CarinskiPostupak
                                           WHERE rns.MagacinskiBroj = @MB AND rns.ID <> @ID AND rns.VrstaRN = @Vrsta
                                             AND RTRIM(v.Oznaka) = 'C7'
                                             AND EXISTS (SELECT 1 FROM VrstaCarinskogPostupka t WHERE t.ID = @Postupak AND RTRIM(t.Oznaka) = 'C7')
                                           ORDER BY rns.ID", conn);
                cmd.Parameters.AddWithValue("@MB", mb);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@Vrsta", Vrsta ?? "");
                cmd.Parameters.AddWithValue("@Postupak", postupak);
                drugi = cmd.ExecuteScalar();
            }

            if (drugi == null || drugi == DBNull.Value)
                return true;

            MessageBox.Show("Magacinski broj " + mb + " već ima nalog sa postupkom C7 (nalog " + drugi + ").\nNa istom magacinskom broju ne mogu biti dva C7.",
                "Carinski postupak C7", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        // RAZ-06: vlasnika robe upisuje procedura UpdateRadniNalogSkladistaDirektni u MagacinskiPredmet, a liste naloga,
        // Prijemnica, Otprema i Pregled lagera ga čitaju iz RadniNalogSkladista.VlasnikRobe. Posle snimanja nalog dobija
        // vlasnika svog magacinskog predmeta (samo vlasnika koji postoji u VlasniciRobe). Isto za postojeće naloge radi
        // skripta Carina_VlasnikRobe_2026_11.sql.
        private void PrenesiVlasnikaNaNalog()
        {
            if (!int.TryParse(txtID.Text, out int id) || id <= 0)
                return;
            try
            {
                using (var conn = new SqlConnection(connection))
                {
                    conn.Open();
                    var cmd = new SqlCommand(@"UPDATE rns SET VlasnikRobe = mp.VlasnikRobe
                                               FROM RadniNalogSkladista rns
                                               CROSS APPLY (SELECT TOP 1 m.VlasnikRobe FROM MagacinskiPredmet m
                                                            WHERE m.RN = rns.ID AND m.VlasnikRobe IN (SELECT id FROM VlasniciRobe)
                                                            ORDER BY m.ID) mp
                                               WHERE rns.ID = @ID AND ISNULL(rns.VlasnikRobe, 0) <> mp.VlasnikRobe", conn);
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vlasnik robe nije prenet na nalog: " + ex.Message);
            }
        }
    }
}
