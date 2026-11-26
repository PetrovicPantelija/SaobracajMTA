using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main
{
    // RAZ-05: zamena tarifnog broja naimenovanja sa tragom (procedura sp_NHM_Zameni, tabela RNCarinskoNHMIzmena,
    // skripta Carina_ZamenaTarife_2026_11.sql). Menjaju se naimenovanje, stavke prijemnice vezane za njega i otvorena
    // otprema; potvrđene otpremnice ostaju (procedura odbija zamenu kad naimenovanje već ima potvrđenu otpremnicu).
    // Zamenu sme da radi korisnik sa pravom korekcije (Podešavanja: Carina.Prijemnica.KorekcijaKorisnici, kao CAR-P08).
    public partial class frmNalogPrijem
    {
        Button btnZameniTarifu, btnIstorijaTarifa;

        static readonly Color SavaPlava = Color.FromArgb(0x0A, 0x66, 0xB8);
        static readonly Color SavaOkvir = Color.FromArgb(0xB9, 0xC6, 0xD3);
        static readonly Color SavaOznaka = Color.FromArgb(0x4C, 0x5D, 0x6E);
        static readonly Color SavaPovrsina = Color.FromArgb(0xF4, 0xF7, 0xFA);

        // poziva se iz InitMagacinskiPredmet, pre InitRaspored: Napomena se sužava, dugmad staju između nje i Dodaj/Izmeni/Izbriši
        private void InitZamenaTarife()
        {
            var panel = txtNapomenaE.Parent;
            txtNapomenaE.Width = 120;
            int x = txtNapomenaE.Right + 8;
            btnZameniTarifu = new Button
            {
                Text = "Zameni tarifni broj",
                Location = new Point(x, 8),
                Size = new Size(94, 48),
                UseVisualStyleBackColor = true
            };
            btnIstorijaTarifa = new Button
            {
                Text = "Istorija tarifa",
                Location = new Point(x, 60),
                Size = new Size(94, 44),
                UseVisualStyleBackColor = true
            };
            var tip = new ToolTip();
            tip.SetToolTip(btnZameniTarifu, "Zamena tarifnog broja izabranog naimenovanja (ispravka C7): menja i stavke prijemnice i otvorenu otpremu, uz razlog i trag");
            tip.SetToolTip(btnIstorijaTarifa, "Sve zamene tarifnih brojeva (filter po magacinskom broju, C7 i korisniku)");
            btnZameniTarifu.Click += (s, e) => OtvoriZamenuTarife();
            btnIstorijaTarifa.Click += (s, e) => OtvoriIstorijuTarifa();
            panel.Controls.Add(btnZameniTarifu);
            panel.Controls.Add(btnIstorijaTarifa);
        }

        private bool ImaPravoZameneTarife()
        {
            string lista = Administracija.Podesavanja.Get("Carina.Prijemnica.KorekcijaKorisnici", "");
            return lista.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                        .Any(k => string.Equals(k.Trim(), (Korisnik ?? "").Trim(), StringComparison.OrdinalIgnoreCase));
        }

        private bool ZamenaTarifePostoji(SqlConnection conn)
        {
            using (var cmd = new SqlCommand("SELECT CASE WHEN OBJECT_ID('dbo.sp_NHM_Zameni', 'P') IS NULL THEN 0 ELSE 1 END", conn))
                return Convert.ToInt32(cmd.ExecuteScalar()) == 1;
        }

        private static Font FontPolja { get { return new Font("Segoe UI", 10f); } }

        private static Label Oznaka(string tekst, int x, int y)
        {
            return new Label { Text = tekst, Location = new Point(x, y), AutoSize = true, ForeColor = SavaOznaka, Font = new Font("Segoe UI", 9f) };
        }

        private static Button DugmeDijaloga(string tekst, bool glavno, int sirina = 120)
        {
            var b = new Button
            {
                Text = tekst,
                Size = new Size(sirina, 32),
                FlatStyle = FlatStyle.Flat,
                BackColor = glavno ? SavaPlava : Color.White,
                ForeColor = glavno ? Color.White : Color.FromArgb(0x17, 0x24, 0x33),
                Margin = new Padding(8, 0, 0, 0),
                Tag = glavno ? "primary" : null
            };
            b.FlatAppearance.BorderSize = glavno ? 0 : 1;
            b.FlatAppearance.BorderColor = SavaOkvir;
            return b;
        }

        // provere pre otvaranja: snimljen nalog, skripta, pravo
        private bool MozeZamenaTarife()
        {
            if (!int.TryParse(txtID.Text, out int rn) || rn <= 0)
            {
                MessageBox.Show("Prvo snimite nalog.", "Zamena tarifnog broja");
                return false;
            }
            try
            {
                using (var conn = new SqlConnection(connection))
                {
                    conn.Open();
                    if (!ZamenaTarifePostoji(conn))
                    {
                        MessageBox.Show("Zamena tarifnog broja radi posle skripte Carina_ZamenaTarife_2026_11.sql.", "Zamena tarifnog broja");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Provera nije uspela: " + ex.Message);
                return false;
            }
            if (!ImaPravoZameneTarife())
            {
                MessageBox.Show("Zamenu tarifnog broja radi korisnik sa pravom korekcije.\n" +
                    "Spisak korisnika je u Podešavanjima, ključ Carina.Prijemnica.KorekcijaKorisnici.", "Zamena tarifnog broja",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        // novi broj koji nije u šifarniku NHM: upozorenje (šifarnik ne mora imati sve tarifne brojeve)
        private bool PotvrdiBrojIzvanSifarnika(SqlConnection conn, SqlTransaction tr, IEnumerable<string> brojevi)
        {
            var nepoznati = new List<string>();
            foreach (var b in brojevi.Distinct())
                using (var cmd = new SqlCommand(@"SELECT CASE WHEN OBJECT_ID('dbo.NHM', 'U') IS NULL THEN 1
                        WHEN EXISTS (SELECT 1 FROM dbo.NHM WHERE RTRIM(CONVERT(nvarchar(50), Broj)) = @B) THEN 1 ELSE 0 END", conn, tr))
                {
                    cmd.Parameters.AddWithValue("@B", b);
                    if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                        nepoznati.Add(b);
                }
            if (nepoznati.Count == 0)
                return true;
            return MessageBox.Show("Tarifni broj nije u šifarniku NHM: " + string.Join(", ", nepoznati) + ".\n\nDa li je broj ipak tačan?",
                "Zamena tarifnog broja", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }

        // poziv procedure; vraća opis rezultata
        private string IzvrsiZamenu(SqlConnection conn, SqlTransaction tr, int idNaimenovanja, string novi, string razlog, string dokument)
        {
            using (var cmd = new SqlCommand("dbo.sp_NHM_Zameni", conn, tr) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@IDNaimenovanja", idNaimenovanja);
                cmd.Parameters.Add("@NoviNHM", SqlDbType.VarChar, 10).Value = novi;
                cmd.Parameters.Add("@Razlog", SqlDbType.NVarChar, 500).Value = razlog;
                cmd.Parameters.Add("@Dokument", SqlDbType.NVarChar, 200).Value = string.IsNullOrWhiteSpace(dokument) ? (object)DBNull.Value : dokument.Trim();
                cmd.Parameters.Add("@BrojC7", SqlDbType.NVarChar, 50).Value = (txtBrojJCI.Text ?? "").Trim();
                cmd.Parameters.Add("@Korisnik", SqlDbType.NVarChar, 50).Value = (Korisnik ?? "").Trim();
                using (var dr = cmd.ExecuteReader())
                    if (dr.Read())
                        return "stavki prijemnice: " + dr["StavkiPrijemnice"] + ", stavki otvorene otpreme: " + dr["StavkiOtpreme"];
            }
            return "";
        }

        #region Jedno naimenovanje

        private void OtvoriZamenuTarife()
        {
            if (!MozeZamenaTarife())
                return;

            int idNaim = 0;
            string rb = "", naziv = "", stari = "";
            try
            {
                var zapis = gridGroupingControl1.Table.CurrentRecord;
                if (zapis != null)
                {
                    int.TryParse(Convert.ToString(zapis.GetValue("ID")), out idNaim);
                    rb = Convert.ToString(zapis.GetValue("RB Naimenovanja"));
                    naziv = Convert.ToString(zapis.GetValue("Naimenovanje")).Trim();
                    stari = TarifniBroj.Normalizuj(zapis.GetValue("NHM naimenovanja"));
                }
            }
            catch
            {
                idNaim = 0;
            }
            if (idNaim <= 0)
            {
                MessageBox.Show("Izaberite naimenovanje u tabeli (klik na red), pa Zameni tarifni broj.\nZa više naimenovanja odjednom: u prozoru zamene dugme „Iz Excela“.",
                    "Zamena tarifnog broja");
                return;
            }

            using (var dlg = new Form
            {
                Text = "Zamena tarifnog broja",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false,
                ClientSize = new Size(560, 380),
                Font = FontPolja,
                BackColor = SavaPovrsina
            })
            {
                var lblNaim = new Label
                {
                    Text = "RB " + rb + "  " + naziv,
                    Location = new Point(20, 20),
                    Size = new Size(520, 40),
                    Font = new Font("Segoe UI Semibold", 10f),
                    AutoEllipsis = true
                };
                var txtStari = new TextBox { Location = new Point(20, 86), Width = 140, ReadOnly = true, Text = stari == "" ? "(nije unet)" : stari, BackColor = SavaPovrsina, TextAlign = HorizontalAlignment.Right };
                var txtNovi = new TextBox { Location = new Point(180, 86), Width = 140, MaxLength = TarifniBroj.MaxDuzina, TextAlign = HorizontalAlignment.Right };
                txtNovi.KeyPress += (s, e) => { if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true; };
                var txtRazlog = new TextBox { Location = new Point(20, 146), Width = 520, Height = 60, Multiline = true, MaxLength = 500 };
                var txtDokument = new TextBox { Location = new Point(20, 236), Width = 520, MaxLength = 200 };
                var lblNapomena = new Label
                {
                    Text = "Menjaju se naimenovanje, stavke prijemnice vezane za njega i otvorena otprema. Potvrđene otpremnice se ne menjaju.",
                    Location = new Point(20, 270),
                    Size = new Size(520, 40),
                    ForeColor = SavaOznaka,
                    Font = new Font("Segoe UI", 9f)
                };

                dlg.Controls.AddRange(new Control[]
                {
                    lblNaim,
                    Oznaka("Stari tarifni broj", 20, 66), txtStari,
                    Oznaka("Novi tarifni broj", 180, 66), txtNovi,
                    Oznaka("Razlog", 20, 126), txtRazlog,
                    Oznaka("Dokument (npr. ispravka C7 1177/2025, 12.04.2025)", 20, 216), txtDokument,
                    lblNapomena
                });

                var traka = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 52, FlowDirection = FlowDirection.RightToLeft, Padding = new Padding(20, 10, 12, 10) };
                var btnZameni = DugmeDijaloga("Zameni", true);
                var btnOdustani = DugmeDijaloga("Odustani", false, 100);
                var btnExcel = DugmeDijaloga("Iz Excela…", false, 110);
                btnOdustani.DialogResult = DialogResult.Cancel;
                traka.Controls.Add(btnZameni);
                traka.Controls.Add(btnOdustani);
                traka.Controls.Add(btnExcel);
                dlg.Controls.Add(traka);
                dlg.CancelButton = btnOdustani;

                btnExcel.Click += (s, e) => { dlg.Close(); ZameniIzExcela(); };
                btnZameni.Click += (s, e) =>
                {
                    string novi = TarifniBroj.Normalizuj(txtNovi.Text);
                    if (novi == "")
                    {
                        MessageBox.Show("Unesite novi tarifni broj.");
                        txtNovi.Focus();
                        return;
                    }
                    string greska = TarifniBroj.Provera(novi);
                    if (greska != null)
                    {
                        MessageBox.Show(greska);
                        txtNovi.Focus();
                        return;
                    }
                    if (novi == stari)
                    {
                        MessageBox.Show("Novi tarifni broj je isti kao stari.");
                        txtNovi.Focus();
                        return;
                    }
                    if (txtRazlog.Text.Trim() == "")
                    {
                        MessageBox.Show("Razlog zamene je obavezan.");
                        txtRazlog.Focus();
                        return;
                    }
                    try
                    {
                        using (var conn = new SqlConnection(connection))
                        {
                            conn.Open();
                            if (!PotvrdiBrojIzvanSifarnika(conn, null, new[] { novi }))
                                return;
                            if (MessageBox.Show("Tarifni broj naimenovanja RB " + rb + ": " + (stari == "" ? "(nije unet)" : stari) + " → " + novi + ".\n\nZameniti?",
                                    "Zamena tarifnog broja", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                                return;
                            string rezultat = IzvrsiZamenu(conn, null, idNaim, novi, txtRazlog.Text.Trim(), txtDokument.Text);
                            MessageBox.Show("Tarifni broj je zamenjen (" + rezultat + ").", "Zamena tarifnog broja");
                        }
                        dlg.DialogResult = DialogResult.OK;
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message, "Zamena tarifnog broja nije urađena", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Zamena tarifnog broja nije urađena: " + ex.Message);
                    }
                };

                if (dlg.ShowDialog(this) == DialogResult.OK)
                    OsveziPosleZameneTarife();
            }
        }

        private void OsveziPosleZameneTarife()
        {
            try
            {
                VratiCarinskeStavke();
                TarifniBrojNaimenovanja = "";
            }
            catch
            {
                // tabela se osvežava i pri sledećem otvaranju naloga
            }
        }

        #endregion

        #region Više naimenovanja iz Excela

        private class RedZamene
        {
            public int Red;
            public string Stari, Novi, Razlog, Dokument, Stanje;
            public List<KeyValuePair<int, string>> Naimenovanja = new List<KeyValuePair<int, string>>();   // ID, RB
            public bool Ispravan { get { return Stanje == "Spremno"; } }
        }

        // Excel: kolone Stari, Novi, Razlog (obavezno) i Dokument (opciono), zaglavlje u prvih 10 redova;
        // bez zaglavlja: A = stari, B = novi, C = razlog, D = dokument. Menjaju se naimenovanja ovog magacinskog predmeta.
        private void ZameniIzExcela()
        {
            string putanja;
            using (var ofd = new OpenFileDialog { Filter = "Excel (*.xlsx;*.xls)|*.xlsx;*.xls", Title = "Lista zamena tarifnih brojeva (stari, novi, razlog)" })
            {
                if (ofd.ShowDialog(this) != DialogResult.OK)
                    return;
                putanja = ofd.FileName;
            }

            List<RedZamene> redovi;
            try
            {
                redovi = CitajListuZamena(putanja);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fajl nije pročitan: " + ex.Message);
                return;
            }
            if (redovi.Count == 0)
            {
                MessageBox.Show("U fajlu nema redova sa starim i novim tarifnim brojem.");
                return;
            }

            // naimenovanja ovog naloga po trenutnom tarifnom broju
            var poBroju = new Dictionary<string, List<KeyValuePair<int, string>>>();
            try
            {
                using (var conn = new SqlConnection(connection))
                using (var cmd = new SqlCommand(@"SELECT n.ID, n.RB, CONVERT(varchar(20), n.NHM) AS NHM
                        FROM dbo.RNCarinskoStavke n JOIN dbo.MagacinskiPredmet mp ON mp.ID = n.IDNadredjena
                        WHERE mp.RN = @RN ORDER BY n.RB, n.ID", conn))
                {
                    cmd.Parameters.AddWithValue("@RN", Convert.ToInt32(txtID.Text));
                    conn.Open();
                    using (var dr = cmd.ExecuteReader())
                        while (dr.Read())
                        {
                            string b = TarifniBroj.Normalizuj(dr["NHM"]);
                            if (b == "") continue;
                            if (!poBroju.ContainsKey(b)) poBroju[b] = new List<KeyValuePair<int, string>>();
                            poBroju[b].Add(new KeyValuePair<int, string>(Convert.ToInt32(dr["ID"]), Convert.ToString(dr["RB"])));
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Naimenovanja nisu učitana: " + ex.Message);
                return;
            }

            foreach (var r in redovi)
            {
                string greska = TarifniBroj.Provera(r.Novi);
                if (r.Novi == "") r.Stanje = "Nema novog broja";
                else if (greska != null) r.Stanje = "Novi broj: " + greska;
                else if (r.Novi == r.Stari) r.Stanje = "Isti broj";
                else if (string.IsNullOrWhiteSpace(r.Razlog)) r.Stanje = "Nema razloga";
                else if (redovi.Count(x => x.Stari == r.Stari) > 1) r.Stanje = "Stari broj se ponavlja u fajlu";
                else if (!poBroju.ContainsKey(r.Stari)) r.Stanje = "Nema naimenovanja sa tim brojem";
                else
                {
                    r.Naimenovanja = poBroju[r.Stari];
                    r.Stanje = "Spremno";
                }
            }

            PrikaziListuZamena(redovi);
        }

        private List<RedZamene> CitajListuZamena(string putanja)
        {
            var redovi = new List<RedZamene>();
            using (var engine = new ExcelEngine())
            using (var fs = new FileStream(putanja, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                IWorkbook knjiga = engine.Excel.Workbooks.Open(fs);
                try
                {
                    IWorksheet list = knjiga.Worksheets[0];
                    int poslednjiRed = list.UsedRange.LastRow, poslednjaKolona = Math.Max(list.UsedRange.LastColumn, 4);
                    int kStari = 1, kNovi = 2, kRazlog = 3, kDokument = 4, prviRed = 1;
                    Func<string, string> norm = t => (t ?? "").Trim().ToLowerInvariant();
                    for (int r = 1; r <= Math.Min(10, poslednjiRed); r++)
                    {
                        int s = 0, n = 0, z = 0, d = 0;
                        for (int c = 1; c <= poslednjaKolona; c++)
                        {
                            string t = norm(list[r, c].DisplayText);
                            if (t.StartsWith("stari") || t == "old") s = c;
                            else if (t.StartsWith("novi") || t == "new") n = c;
                            else if (t.StartsWith("razlog") || t == "reason") z = c;
                            else if (t.StartsWith("dokument") || t == "document") d = c;
                        }
                        if (s > 0 && n > 0)
                        {
                            kStari = s; kNovi = n; kRazlog = z; kDokument = d; prviRed = r + 1;
                            break;
                        }
                    }
                    for (int r = prviRed; r <= poslednjiRed; r++)
                    {
                        string stari = TarifniBroj.Normalizuj(list[r, kStari].DisplayText);
                        string novi = TarifniBroj.Normalizuj(list[r, kNovi].DisplayText);
                        if (stari == "" && novi == "")
                            continue;
                        if (stari == "")
                            continue;   // red bez starog broja (npr. naslov) se preskače
                        redovi.Add(new RedZamene
                        {
                            Red = r,
                            Stari = stari,
                            Novi = novi,
                            Razlog = kRazlog > 0 ? (list[r, kRazlog].DisplayText ?? "").Trim() : "",
                            Dokument = kDokument > 0 ? (list[r, kDokument].DisplayText ?? "").Trim() : ""
                        });
                    }
                }
                finally
                {
                    knjiga.Close();
                }
            }
            return redovi;
        }

        private void PrikaziListuZamena(List<RedZamene> redovi)
        {
            using (var dlg = new Form
            {
                Text = "Zamena tarifnih brojeva iz Excela – nalog " + txtID.Text,
                StartPosition = FormStartPosition.CenterParent,
                Size = new Size(980, 520),
                MinimizeBox = false,
                Font = FontPolja,
                BackColor = SavaPovrsina
            })
            {
                var lbl = new Label
                {
                    Dock = DockStyle.Top,
                    Height = 40,
                    Padding = new Padding(20, 10, 20, 0),
                    ForeColor = SavaOznaka,
                    Font = new Font("Segoe UI", 9f),
                    Text = "Menjaju se samo redovi sa stanjem „Spremno“, svi u jednoj transakciji. Ako jedna zamena ne uspe, ne menja se ništa."
                };
                var dgv = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    ReadOnly = true,
                    AllowUserToAddRows = false,
                    RowHeadersVisible = false,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    BackgroundColor = Color.White,
                    BorderStyle = BorderStyle.None,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    EnableHeadersVisualStyles = false
                };
                dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0xEA, 0xF0, 0xF6);
                dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9f);
                dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0xDD, 0xF1, 0xFB);
                dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(0x17, 0x24, 0x33);
                dgv.RowTemplate.Height = 30;
                dgv.Columns.Add("Red", "Red u fajlu");
                dgv.Columns.Add("Stari", "Stari tarifni broj");
                dgv.Columns.Add("Novi", "Novi tarifni broj");
                dgv.Columns.Add("Naimenovanja", "Naimenovanja (RB)");
                dgv.Columns.Add("Razlog", "Razlog");
                dgv.Columns.Add("Dokument", "Dokument");
                dgv.Columns.Add("Stanje", "Stanje");
                dgv.Columns["Red"].FillWeight = 40;
                dgv.Columns["Razlog"].FillWeight = 160;
                dgv.Columns["Stanje"].FillWeight = 150;
                foreach (var r in redovi)
                {
                    int i = dgv.Rows.Add(r.Red, r.Stari, r.Novi, string.Join(", ", r.Naimenovanja.Select(x => x.Value)), r.Razlog, r.Dokument, r.Stanje);
                    dgv.Rows[i].Cells["Stanje"].Style.ForeColor = r.Ispravan ? Color.FromArgb(0x1E, 0x7A, 0x3C) : Color.FromArgb(0xB3, 0x26, 0x1E);
                }

                var traka = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 52, FlowDirection = FlowDirection.RightToLeft, Padding = new Padding(20, 10, 12, 10) };
                var btnPrimeni = DugmeDijaloga("Zameni (" + redovi.Count(r => r.Ispravan) + ")", true, 140);
                var btnOdustani = DugmeDijaloga("Odustani", false, 100);
                btnOdustani.DialogResult = DialogResult.Cancel;
                btnPrimeni.Enabled = redovi.Any(r => r.Ispravan);
                traka.Controls.Add(btnPrimeni);
                traka.Controls.Add(btnOdustani);
                dlg.Controls.Add(dgv);
                dlg.Controls.Add(traka);
                dlg.Controls.Add(lbl);
                dlg.CancelButton = btnOdustani;

                btnPrimeni.Click += (s, e) =>
                {
                    var spremni = redovi.Where(r => r.Ispravan).ToList();
                    int naimenovanja = spremni.Sum(r => r.Naimenovanja.Count);
                    int preskoceno = redovi.Count - spremni.Count;
                    if (MessageBox.Show("Zamenjuje se " + spremni.Count + " tarifnih brojeva na " + naimenovanja + " naimenovanja." +
                            (preskoceno > 0 ? "\nRedovi koji nisu spremni (" + preskoceno + ") se preskaču." : "") + "\n\nNastaviti?",
                            "Zamena tarifnih brojeva", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        return;
                    try
                    {
                        using (var conn = new SqlConnection(connection))
                        {
                            conn.Open();
                            if (!PotvrdiBrojIzvanSifarnika(conn, null, spremni.Select(r => r.Novi)))
                                return;
                            using (var tr = conn.BeginTransaction())
                            {
                                foreach (var r in spremni)
                                    foreach (var n in r.Naimenovanja)
                                        IzvrsiZamenu(conn, tr, n.Key, r.Novi, r.Razlog, r.Dokument);
                                tr.Commit();
                            }
                        }
                        MessageBox.Show("Zamenjeno: " + spremni.Count + " tarifnih brojeva, " + naimenovanja + " naimenovanja.", "Zamena tarifnih brojeva");
                        dlg.DialogResult = DialogResult.OK;
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message + "\n\nNijedna zamena nije urađena.", "Zamena tarifnih brojeva", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Zamena nije urađena: " + ex.Message);
                    }
                };

                if (dlg.ShowDialog(this) == DialogResult.OK)
                    OsveziPosleZameneTarife();
            }
        }

        #endregion

        #region Istorija

        private void OtvoriIstorijuTarifa()
        {
            try
            {
                using (var conn = new SqlConnection(connection))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT CASE WHEN OBJECT_ID('dbo.RNCarinskoNHMIzmena', 'U') IS NULL THEN 0 ELSE 1 END", conn))
                        if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                        {
                            MessageBox.Show("Istorija tarifnih brojeva radi posle skripte Carina_ZamenaTarife_2026_11.sql.", "Istorija tarifa");
                            return;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Provera nije uspela: " + ex.Message);
                return;
            }

            using (var dlg = new Form
            {
                Text = "Istorija tarifnih brojeva",
                StartPosition = FormStartPosition.CenterParent,
                Size = new Size(1180, 560),
                MinimizeBox = false,
                Font = FontPolja,
                BackColor = SavaPovrsina
            })
            {
                var filteri = new Panel { Dock = DockStyle.Top, Height = 64, Padding = new Padding(20, 0, 20, 0) };
                var txtMB = new TextBox { Location = new Point(20, 28), Width = 120, Text = txtMagacinskiBroj.Text.Trim() };
                var txtC7 = new TextBox { Location = new Point(156, 28), Width = 160, Text = (txtBrojJCI.Text ?? "").Trim() };
                var txtKor = new TextBox { Location = new Point(332, 28), Width = 160 };
                var btnOsvezi = DugmeDijaloga("Osveži", false, 100);
                btnOsvezi.Location = new Point(508, 26);
                var btnSve = DugmeDijaloga("Sve zamene", false, 110);
                btnSve.Location = new Point(616, 26);
                filteri.Controls.AddRange(new Control[]
                {
                    Oznaka("Magacinski broj", 20, 8), txtMB,
                    Oznaka("C7", 156, 8), txtC7,
                    Oznaka("Korisnik", 332, 8), txtKor,
                    btnOsvezi, btnSve
                });

                var dgv = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    ReadOnly = true,
                    AllowUserToAddRows = false,
                    RowHeadersVisible = false,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    BackgroundColor = Color.White,
                    BorderStyle = BorderStyle.None,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    EnableHeadersVisualStyles = false
                };
                dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0xEA, 0xF0, 0xF6);
                dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9f);
                dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0xDD, 0xF1, 0xFB);
                dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(0x17, 0x24, 0x33);
                dgv.RowTemplate.Height = 30;

                Action ucitaj = () =>
                {
                    try
                    {
                        var dt = new DataTable();
                        using (var conn = new SqlConnection(connection))
                        using (var da = new SqlDataAdapter(@"SELECT TOP 2000
                                    i.Datum AS [Datum], i.Korisnik AS [Korisnik], i.MagacinskiBroj AS [Magacinski broj], i.BrojC7 AS [C7],
                                    i.RN AS [Nalog], i.RBNaimenovanja AS [RB naimenovanja],
                                    RTRIM(CONVERT(nvarchar(300), n.Naimenovanje)) AS [Naimenovanje],
                                    i.StariNHM AS [Stari tarifni broj], i.NoviNHM AS [Novi tarifni broj],
                                    i.Razlog AS [Razlog], i.Dokument AS [Dokument],
                                    i.StavkiPrijemnice AS [Stavki prijemnice], i.StavkiOtpreme AS [Stavki otpreme]
                                FROM dbo.RNCarinskoNHMIzmena i
                                LEFT JOIN dbo.RNCarinskoStavke n ON n.ID = i.IDNaimenovanja
                                WHERE (@MB = '' OR CONVERT(nvarchar(20), i.MagacinskiBroj) = @MB)
                                  AND (@C7 = '' OR i.BrojC7 LIKE '%' + @C7 + '%')
                                  AND (@Kor = '' OR i.Korisnik LIKE '%' + @Kor + '%')
                                ORDER BY i.Datum DESC, i.ID DESC", conn))
                        {
                            da.SelectCommand.Parameters.AddWithValue("@MB", txtMB.Text.Trim());
                            da.SelectCommand.Parameters.AddWithValue("@C7", txtC7.Text.Trim());
                            da.SelectCommand.Parameters.AddWithValue("@Kor", txtKor.Text.Trim());
                            da.Fill(dt);
                        }
                        dgv.DataSource = dt;
                        if (dgv.Columns.Contains("Datum"))
                            dgv.Columns["Datum"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
                        foreach (var k in new[] { "Magacinski broj", "Nalog", "RB naimenovanja", "Stavki prijemnice", "Stavki otpreme" })
                            if (dgv.Columns.Contains(k))
                                dgv.Columns[k].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        if (dgv.Columns.Contains("Razlog"))
                            dgv.Columns["Razlog"].FillWeight = 200;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Istorija nije učitana: " + ex.Message);
                    }
                };
                btnOsvezi.Click += (s, e) => ucitaj();
                btnSve.Click += (s, e) => { txtMB.Text = txtC7.Text = txtKor.Text = ""; ucitaj(); };

                dlg.Controls.Add(dgv);
                dlg.Controls.Add(filteri);
                dlg.Shown += (s, e) => ucitaj();
                dlg.ShowDialog(this);
            }
        }

        #endregion
    }
}
