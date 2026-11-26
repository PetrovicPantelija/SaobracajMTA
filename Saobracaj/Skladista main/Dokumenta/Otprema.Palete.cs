using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main.Dokumenta
{
    // RAZ-13, RAZ-15: izbor paleta za otpremu (skripta Skladista main\Carina_Izlaz_2026_11.sql).
    // Izabrane palete se rezervišu za nalog otpreme (RNCarinskoPaletaIzlaz, status 1), pa ih drugi nalog ne može izabrati.
    // Iz rezervacija nastaju stavke otpreme, jedna po stavci prijemnice; SNIMI ih upisuje kao i ostale stavke.
    // Potvrda izlaza (paleta izdata, polje oslobođeno) je na Otpremnici (Otpremnica.Izlaz.cs).
    public partial class Otprema
    {
        private class RezervisanaStavka
        {
            public int IDStavke;
            public int Prijemnica;
            public string Naziv;
            public decimal Koleta;
        }

        bool? imaIzlazPoPaletama;
        Button btnIzborPaleta;
        // stavke prijemnice za koje nalog ima rezervisane palete (stanje u bazi)
        List<RezervisanaStavka> rezervisaneStavke = new List<RezervisanaStavka>();

        private bool IzlazPoPaletamaPostoji()
        {
            if (!imaIzlazPoPaletama.HasValue)
            {
                try
                {
                    using (var conn = new SqlConnection(connection))
                    using (var cmd = new SqlCommand("SELECT CASE WHEN OBJECT_ID('dbo.sp_Izlaz_Rezervisi', 'P') IS NULL THEN 0 ELSE 1 END", conn))
                    {
                        conn.Open();
                        imaIzlazPoPaletama = Convert.ToInt32(cmd.ExecuteScalar()) == 1;
                    }
                }
                catch
                {
                    imaIzlazPoPaletama = false;
                }
            }
            return imaIzlazPoPaletama.Value;
        }

        // poziva se iz Otprema_Load; bez skripte forma radi kao ranije
        private void PodesiIzlazPoPaletama()
        {
            if (!IzlazPoPaletamaPostoji())
                return;

            btnIzborPaleta = new Button
            {
                Text = "IZBOR PALETA",
                Location = new Point(button1.Right + 32, button1.Top),
                Size = button1.Size,
                Font = button1.Font,
                FlatStyle = button1.FlatStyle,
                BackColor = button1.BackColor,
                ForeColor = button1.ForeColor,
                UseVisualStyleBackColor = button1.UseVisualStyleBackColor,
                Anchor = button1.Anchor
            };
            btnIzborPaleta.Click += (s, e) => OtvoriIzborPaleta();
            Controls.Add(btnIzborPaleta);
            btnIzborPaleta.BringToFront();

            try
            {
                UcitajRezervacije(false);
            }
            catch
            {
                // nalog bez rezervacija ili baza nedostupna: izbor paleta će prijaviti grešku
            }
        }

        private int RNOtpreme()
        {
            int rn;
            return int.TryParse(txtID.Text.Trim(), out rn) ? rn : 0;
        }

        private DataSet IzlazUpit(string procedura, params SqlParameter[] parametri)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connection))
            using (var cmd = new SqlCommand(procedura, conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddRange(parametri);
                using (var da = new SqlDataAdapter(cmd))
                    da.Fill(ds);
            }
            return ds;
        }

        #region Rezervacije -> stavke otpreme

        // Čita rezervacije naloga. Sa upisUListu: stavke otpreme tih stavki prijemnice se zamenjuju stavkama iz paleta.
        private void UcitajRezervacije(bool upisUListu)
        {
            int rn = RNOtpreme();
            if (rn == 0)
            {
                rezervisaneStavke = new List<RezervisanaStavka>();
                return;
            }

            DataTable t = IzlazUpit("sp_Izlaz_RezervacijeNaloga", new SqlParameter("@RN", rn)).Tables[0];
            var nove = t.Rows.Cast<DataRow>().Select(r => new RezervisanaStavka
            {
                IDStavke = Convert.ToInt32(r["IDStavke"]),
                Prijemnica = Convert.ToInt32(r["Prijemnica"]),
                Naziv = Convert.ToString(r["Naziv"]).Trim(),
                Koleta = Convert.ToDecimal(r["Koleta"])
            }).ToList();

            if (!upisUListu)
            {
                rezervisaneStavke = nove;
                // stavke učitane iz stavki otpreme ponekad nemaju vezu sa stavkom prijemnice (veza je po tekstu)
                foreach (var s in list.Where(x => x != null && x.IDSkladisneStavke == 0))
                {
                    var r = PronadjiRezervaciju(s);
                    if (r != null)
                        s.IDSkladisneStavke = r.IDStavke;
                }
                return;
            }

            var ids = new HashSet<int>(nove.Select(x => x.IDStavke));
            var stare = new HashSet<int>(rezervisaneStavke.Select(x => x.IDStavke));
            list.RemoveAll(x => x != null && (ids.Contains(x.IDSkladisneStavke) || stare.Contains(x.IDSkladisneStavke)));

            foreach (DataRow r in t.Rows)
            {
                decimal koleta = Convert.ToDecimal(r["Koleta"]);
                decimal bruto = Convert.ToDecimal(r["Bruto"]);
                decimal neto = Convert.ToDecimal(r["Neto"]);
                decimal vrednost = Convert.ToDecimal(r["Vrednost"]);
                list.Add(new OtpremaStavke
                {
                    IDSkladisneStavke = Convert.ToInt32(r["IDStavke"]),
                    Prijemnica = Convert.ToInt32(r["Prijemnica"]),
                    PrijemRB = r["PrijemRB"] == DBNull.Value ? 0 : Convert.ToInt32(r["PrijemRB"]),
                    Naimenovanje = Convert.ToString(r["Naimenovanje"]),
                    NHM = TarifniBroj.Normalizuj(r["NHM"]),
                    Naziv = Convert.ToString(r["Naziv"]),
                    JM = Convert.ToString(r["JM"]),

                    // količina je ono što je izabrano na paletama; menja se samo kroz Izbor paleta
                    NerazudzenoKoleta = 0,
                    NerazduzenaTezina = 0,
                    OstatakValute = 0,
                    Koleta = koleta,
                    Bruto = bruto,
                    Neto = neto,
                    Vrednost = vrednost,
                    UkupnoKoletaLager = koleta,
                    UkupnoBrutoLager = bruto,
                    UkupnoNetoLager = neto,
                    UkupnoVrednostLager = vrednost,

                    Valuta = Convert.ToString(r["Valuta"]),
                    DokumentRazduzenja = "",
                    DatumRazduzenja = DateTime.Now,
                    Destinacija = "",
                    DatumOtpreme = DateTime.Now,
                    Pozicija = Convert.ToString(r["Pozicija"]),
                    Paleta = Convert.ToInt32(r["BrojPaleta"]),
                    VrstaPalete = r["VrstaPaleta"] == DBNull.Value ? 0 : Convert.ToInt32(r["VrstaPaleta"]),
                    DimenzijePalete = r["DimenzijaPaleta"] == DBNull.Value ? "" : r["DimenzijaPaleta"].ToString(),
                    SifraArtikla = r["SifraArtikla"] == DBNull.Value ? "" : r["SifraArtikla"].ToString(),
                    PDV = Convert.ToDecimal(r["PDV"]),
                    Carina = Convert.ToDecimal(r["Carina"]),
                    TabelaPozicija = r["Polje"] == DBNull.Value ? "" : r["Polje"].ToString()
                });
            }

            rezervisaneStavke = nove;
            RenumerisiRB();
            OsveziGrid();
            OcistiPanelStavke();
        }

        private RezervisanaStavka PronadjiRezervaciju(OtpremaStavke s)
        {
            if (s == null)
                return null;
            if (s.IDSkladisneStavke != 0)
                return rezervisaneStavke.FirstOrDefault(r => r.IDStavke == s.IDSkladisneStavke);
            return rezervisaneStavke.FirstOrDefault(r => r.Prijemnica == s.Prijemnica
                && string.Equals(r.Naziv, (s.Naziv ?? "").Trim(), StringComparison.OrdinalIgnoreCase));
        }

        // btnIzmeniStavku_Click: stavka iz paleta se ne menja ručno, inače se razilazi sa paletama
        private bool MozeRucnaIzmena(OtpremaStavke s)
        {
            if (PronadjiRezervaciju(s) == null)
                return true;
            MessageBox.Show("Stavka je nastala iz izbora paleta. Količina se menja u Izbor paleta.");
            return false;
        }

        // btnIzbaciStavku_Click: izbačena stavka oslobađa svoje palete
        private void OslobodiPaleteStavke(OtpremaStavke s)
        {
            var r = PronadjiRezervaciju(s);
            if (r == null)
                return;
            try
            {
                IzlazUpit("sp_Izlaz_Oslobodi", new SqlParameter("@RN", RNOtpreme()), new SqlParameter("@IDStavke", r.IDStavke));
                rezervisaneStavke.Remove(r);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Palete stavke nisu oslobođene: " + ex.Message);
            }
        }

        // SNIMI i FORMIRAN RN: stavke otpreme i rezervisane palete moraju da se slažu
        // (Razduži sve i Izaberi sa lagera zamenjuju celu listu). false = ne snimati.
        private bool UskladiRezervacijeSaListom()
        {
            if (!IzlazPoPaletamaPostoji() || rezervisaneStavke.Count == 0 || RNOtpreme() == 0)
                return true;

            var razlike = new List<RezervisanaStavka>();
            foreach (var r in rezervisaneStavke)
            {
                var s = list.FirstOrDefault(x => x != null && (x.IDSkladisneStavke == r.IDStavke
                    || (x.IDSkladisneStavke == 0 && x.Prijemnica == r.Prijemnica
                        && string.Equals((x.Naziv ?? "").Trim(), r.Naziv, StringComparison.OrdinalIgnoreCase))));
                if (s == null || s.Koleta != r.Koleta)
                    razlike.Add(r);
            }
            if (razlike.Count == 0)
                return true;

            string opis = string.Join("\n", razlike.Take(10).Select(r =>
            {
                var s = list.FirstOrDefault(x => x != null && x.IDSkladisneStavke == r.IDStavke);
                return "Prijemnica " + r.Prijemnica + ", " + r.Naziv + ": palete " + r.Koleta.ToString("N0")
                    + ", stavka otpreme " + (s == null ? "nema" : s.Koleta.ToString("N0"));
            })) + (razlike.Count > 10 ? "\n..." : "");

            var odgovor = MessageBox.Show("Stavke otpreme se ne slažu sa izabranim paletama:\n\n" + opis
                + "\n\nDa: oslobodi palete tih stavki i snimi stavke kako su u listi.\nNe: ništa ne snimati (ispraviti preko Izbor paleta).",
                "Izbor paleta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (odgovor != DialogResult.Yes)
                return false;

            try
            {
                foreach (var r in razlike)
                {
                    IzlazUpit("sp_Izlaz_Oslobodi", new SqlParameter("@RN", RNOtpreme()), new SqlParameter("@IDStavke", r.IDStavke));
                    rezervisaneStavke.Remove(r);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Palete nisu oslobođene, ništa nije snimljeno: " + ex.Message);
                return false;
            }
            return true;
        }

        #endregion

        #region Dijalog Izbor paleta

        const string PKIzaberi = "Izaberi", PKSlobodno = "Slobodno", PKZaIzlaz = "ZaIzlaz",
                     PKIDPalete = "IDPalete", PKIDStavke = "IDStavke", PKArtikal = "Artikal";

        private void OtvoriIzborPaleta()
        {
            if (MagacinskiBroj == 0)
            {
                MessageBox.Show("Prvo izaberite magacinski broj.");
                return;
            }
            int rn = RNOtpreme();
            if (rn == 0)
            {
                MessageBox.Show("Prvo snimite nalog otpreme (SNIMI), pa izaberite palete.");
                return;
            }

            DataTable lager;
            try
            {
                lager = IzlazUpit("sp_Izlaz_LagerPaleta", new SqlParameter("@MagacinskiBroj", MagacinskiBroj), new SqlParameter("@RN", rn)).Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lager po paletama nije učitan: " + ex.Message);
                return;
            }
            if (lager.Rows.Count == 0)
            {
                MessageBox.Show("Magacinski broj nema paleta na lageru.\nPalete se prave na Prijemnici (Dodeli palete), pre potvrde prijemnice.",
                    "Izbor paleta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dlg = NapraviDijalogPaleta(rn, lager))
                dlg.ShowDialog(this);
        }

        private Form NapraviDijalogPaleta(int rn, DataTable lager)
        {
            var dlg = new Form
            {
                Text = "Izbor paleta za otpremu, nalog " + rn + ", magacinski broj " + MagacinskiBroj,
                StartPosition = FormStartPosition.CenterParent,
                Size = new Size(1200, 640),
                MinimumSize = new Size(900, 400),
                MinimizeBox = false,
                ShowIcon = false,
                ShowInTaskbar = false,
                Font = new Font("Segoe UI", 9f)
            };

            var vrh = new Panel { Dock = DockStyle.Top, Height = 64, Padding = new Padding(12, 8, 12, 4) };
            var opis = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Top,
                Height = 22,
                Text = "Označite palete ili upišite koleta za izlaz (deo palete). Redosled je po datumu prijema (FIFO). "
                     + "Palete rezervisane drugim nalogom se ne mogu izabrati preko slobodnog."
            };
            var lblPretraga = new Label { Text = "Pretraga", AutoSize = true, Location = new Point(12, 36) };
            var txtPretraga = new TextBox { Location = new Point(80, 33), Width = 260 };
            vrh.Controls.Add(lblPretraga);
            vrh.Controls.Add(txtPretraga);
            vrh.Controls.Add(opis);

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
                BorderStyle = BorderStyle.None
            };
            dgv.RowTemplate.Height = 28;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            Func<string, string, int, bool, DataGridViewColumn> kol = (ime, zaglavlje, sirina, broj) =>
            {
                var c = new DataGridViewTextBoxColumn { Name = ime, HeaderText = zaglavlje, Width = sirina, ReadOnly = true, SortMode = DataGridViewColumnSortMode.NotSortable };
                if (broj)
                    c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                return c;
            };
            dgv.Columns.Add(new DataGridViewCheckBoxColumn { Name = PKIzaberi, HeaderText = "Izaberi", Width = 56 });
            dgv.Columns.Add(kol("BrojPalete", "Broj palete", 110, false));
            dgv.Columns.Add(kol("Polje", "Polje", 80, false));
            dgv.Columns.Add(kol("Kontejner", "Kontejner", 100, false));
            dgv.Columns.Add(kol("Prijemnica", "Prijemnica", 72, true));
            dgv.Columns.Add(kol("RB", "RB", 40, true));
            dgv.Columns.Add(kol(PKArtikal, "Artikal", 200, false));
            dgv.Columns.Add(kol("Naimenovanje", "Naimenovanje", 160, false));
            dgv.Columns.Add(kol("DatumPrijema", "Datum prijema", 90, false));
            dgv.Columns.Add(kol("NaPaleti", "Na paleti", 64, true));
            dgv.Columns.Add(kol("Izdato", "Izdato", 56, true));
            dgv.Columns.Add(kol("Rezervisano", "Rezervisano drugim nalozima", 90, true));
            dgv.Columns.Add(kol("Nalozi", "Nalog", 60, false));
            dgv.Columns.Add(kol(PKSlobodno, "Slobodno", 64, true));
            var zaIzlaz = kol(PKZaIzlaz, "Za izlaz", 70, true);
            zaIzlaz.ReadOnly = false;
            zaIzlaz.ValueType = typeof(int);
            zaIzlaz.DefaultCellStyle.BackColor = Color.FromArgb(0xFF, 0xFD, 0xE7);
            dgv.Columns.Add(zaIzlaz);
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = PKIDPalete, Visible = false });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = PKIDStavke, Visible = false });

            foreach (DataRow r in lager.Rows)
            {
                int slobodno = Convert.ToInt32(r["Slobodno"]);
                int ovaj = Convert.ToInt32(r["RezervisanoOvaj"]);
                int i = dgv.Rows.Add(ovaj > 0, r["BrojPalete"], r["Polje"], r["Kontejner"], r["Prijemnica"], r["RB"], r["Naziv"], r["Naimenovanje"],
                    r["DatumPrijema"] == DBNull.Value ? "" : Convert.ToDateTime(r["DatumPrijema"]).ToString("dd.MM.yyyy"),
                    r["NaPaleti"], r["Izdato"], r["RezervisanoDrugi"], r["NaloziRezervacije"], slobodno, ovaj, r["IDPalete"], r["IDStavke"]);
                if (slobodno <= 0)
                {
                    var red = dgv.Rows[i];
                    red.ReadOnly = true;
                    red.DefaultCellStyle.ForeColor = Color.FromArgb(0x4C, 0x5D, 0x6E);
                    red.DefaultCellStyle.BackColor = Color.FromArgb(0xF4, 0xF7, 0xFA);
                }
            }

            var dno = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 52,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(12, 10, 12, 8),
                WrapContents = false
            };
            var lblZbir = new Label { AutoSize = false, Width = 360, Height = 32, TextAlign = ContentAlignment.MiddleLeft };
            Func<string, int, Button> dugme = (tekst, sirina) => new Button
            {
                Text = tekst,
                Width = sirina,
                Height = 32,
                UseVisualStyleBackColor = true,
                Margin = new Padding(0, 0, 8, 0)
            };
            var btnFifo = dugme("Predloži po FIFO", 140);
            var btnPonisti = dugme("Poništi izbor", 120);
            var btnSacuvaj = dugme("Sačuvaj izbor", 130);
            btnSacuvaj.BackColor = Color.FromArgb(0x0A, 0x66, 0xB8);
            btnSacuvaj.ForeColor = Color.White;
            btnSacuvaj.FlatStyle = FlatStyle.Flat;
            btnSacuvaj.UseVisualStyleBackColor = false;
            var btnZatvori = dugme("Zatvori", 100);
            dno.Controls.AddRange(new Control[] { lblZbir, btnFifo, btnPonisti, btnSacuvaj, btnZatvori });

            dlg.Controls.Add(dgv);
            dlg.Controls.Add(vrh);
            dlg.Controls.Add(dno);
            dlg.CancelButton = btnZatvori;

            bool menjam = false;
            Func<DataGridViewRow, int> uzmiInt = (red) =>
            {
                int v;
                return int.TryParse(Convert.ToString(red.Cells[PKZaIzlaz].Value), out v) ? v : 0;
            };
            Action osveziZbir = () =>
            {
                var izabrani = dgv.Rows.Cast<DataGridViewRow>().Where(red => uzmiInt(red) > 0).ToList();
                lblZbir.Text = "Izabrano: " + izabrani.Select(red => red.Cells[PKIDPalete].Value).Distinct().Count() + " paleta, "
                    + izabrani.Sum(red => uzmiInt(red)).ToString("N0") + " koleta";
            };
            Action<DataGridViewRow, int> postavi = (red, koleta) =>
            {
                menjam = true;
                red.Cells[PKZaIzlaz].Value = koleta;
                red.Cells[PKIzaberi].Value = koleta > 0;
                menjam = false;
            };

            dgv.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgv.IsCurrentCellDirty && dgv.CurrentCell is DataGridViewCheckBoxCell)
                    dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
            dgv.CellValueChanged += (s, e) =>
            {
                if (menjam || e.RowIndex < 0)
                    return;
                var red = dgv.Rows[e.RowIndex];
                int slobodno = Convert.ToInt32(red.Cells[PKSlobodno].Value);
                if (dgv.Columns[e.ColumnIndex].Name == PKIzaberi)
                    postavi(red, Convert.ToBoolean(red.Cells[PKIzaberi].Value) ? Math.Max(slobodno, 0) : 0);
                else if (dgv.Columns[e.ColumnIndex].Name == PKZaIzlaz)
                {
                    int k = uzmiInt(red);
                    if (k < 0 || k > slobodno)
                    {
                        MessageBox.Show("Za izlaz: od 0 do " + Math.Max(slobodno, 0) + " koleta (slobodno na paleti).");
                        k = Math.Min(Math.Max(k, 0), Math.Max(slobodno, 0));
                    }
                    postavi(red, k);
                }
                osveziZbir();
            };
            dgv.DataError += (s, e) =>
            {
                e.ThrowException = false;
                MessageBox.Show("Za izlaz se upisuje ceo broj koleta.");
            };

            txtPretraga.TextChanged += (s, e) =>
            {
                string q = txtPretraga.Text.Trim();
                dgv.CurrentCell = null;
                foreach (DataGridViewRow red in dgv.Rows)
                {
                    bool vidljiv = q == "" || new[] { "BrojPalete", "Polje", "Kontejner", PKArtikal, "Naimenovanje", "Prijemnica" }
                        .Any(c => Convert.ToString(red.Cells[c].Value).IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0);
                    red.Visible = vidljiv || uzmiInt(red) > 0;
                }
            };

            btnPonisti.Click += (s, e) =>
            {
                foreach (DataGridViewRow red in dgv.Rows)
                    if (!red.ReadOnly)
                        postavi(red, 0);
                osveziZbir();
            };

            // FIFO: za artikal izabranog reda upisuje se tražena koleta redom po datumu prijema i broju palete
            btnFifo.Click += (s, e) =>
            {
                var tekuci = dgv.CurrentRow;
                if (tekuci == null)
                {
                    MessageBox.Show("Izaberite red artikla u tabeli.");
                    return;
                }
                string artikal = Convert.ToString(tekuci.Cells[PKArtikal].Value).Trim();
                var redovi = dgv.Rows.Cast<DataGridViewRow>()
                    .Where(red => string.Equals(Convert.ToString(red.Cells[PKArtikal].Value).Trim(), artikal, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                int ukupnoSlobodno = redovi.Sum(red => Math.Max(Convert.ToInt32(red.Cells[PKSlobodno].Value), 0));
                if (ukupnoSlobodno == 0)
                {
                    MessageBox.Show("Artikal nema slobodnih koleta.");
                    return;
                }
                int? trazeno = UnesiKoleta(dlg, artikal, ukupnoSlobodno, redovi.Sum(red => uzmiInt(red)));
                if (!trazeno.HasValue)
                    return;
                int ostalo = trazeno.Value;
                foreach (var red in redovi)
                {
                    int slob = Math.Max(Convert.ToInt32(red.Cells[PKSlobodno].Value), 0);
                    int k = Math.Min(slob, ostalo);
                    postavi(red, k);
                    ostalo -= k;
                }
                osveziZbir();
            };

            btnSacuvaj.Click += (s, e) =>
            {
                dgv.EndEdit();
                var sb = new StringBuilder();
                foreach (DataGridViewRow red in dgv.Rows)
                {
                    int k = uzmiInt(red);
                    if (k > 0)
                        sb.Append("<i p=\"").Append(Convert.ToInt32(red.Cells[PKIDPalete].Value))
                          .Append("\" s=\"").Append(Convert.ToInt32(red.Cells[PKIDStavke].Value))
                          .Append("\" k=\"").Append(k).Append("\"/>");
                }
                if (sb.Length == 0 && rezervisaneStavke.Count > 0
                    && MessageBox.Show("Nijedna paleta nije izabrana: oslobađaju se sve palete ovog naloga. Nastaviti?", "Izbor paleta",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                try
                {
                    IzlazUpit("sp_Izlaz_Rezervisi", new SqlParameter("@RN", rn), new SqlParameter("@Izbor", sb.ToString()),
                        new SqlParameter("@Korisnik", Korisnik ?? ""));
                    UcitajRezervacije(true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Izbor paleta nije sačuvan: " + ex.Message);
                    return;
                }
                MessageBox.Show("Izbor paleta je sačuvan, a stavke otpreme su dopunjene.\nSnimite nalog (SNIMI) da se sačuvaju i stavke otpreme.");
                dlg.DialogResult = DialogResult.OK;
            };

            osveziZbir();
            return dlg;
        }

        private static int? UnesiKoleta(IWin32Window vlasnik, string artikal, int maksimum, int predlog)
        {
            using (var f = new Form
            {
                Text = "Predloži po FIFO",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false,
                ShowIcon = false,
                ShowInTaskbar = false,
                ClientSize = new Size(380, 130),
                Font = new Font("Segoe UI", 9f)
            })
            {
                var lbl = new Label { Text = "Koleta za izlaz: " + artikal + " (slobodno " + maksimum.ToString("N0") + ")", AutoSize = false, Location = new Point(12, 10), Size = new Size(356, 36) };
                var num = new NumericUpDown { Location = new Point(12, 50), Width = 120, Minimum = 0, Maximum = maksimum, Value = Math.Min(Math.Max(predlog, 0), maksimum), TextAlign = HorizontalAlignment.Right };
                var ok = new Button { Text = "Predloži", Location = new Point(176, 88), Size = new Size(92, 32), DialogResult = DialogResult.OK };
                var otkazi = new Button { Text = "Otkaži", Location = new Point(276, 88), Size = new Size(92, 32), DialogResult = DialogResult.Cancel };
                f.Controls.AddRange(new Control[] { lbl, num, ok, otkazi });
                f.AcceptButton = ok;
                f.CancelButton = otkazi;
                return f.ShowDialog(vlasnik) == DialogResult.OK ? (int?)num.Value : null;
            }
        }

        #endregion
    }
}
