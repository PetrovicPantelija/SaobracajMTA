using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main.Dokumenta
{
    // Carinski prijem na Prijemnici: uvoz packing liste, formiranje artikala, uparivanje sa naimenovanjem,
    // koleta za skladište i kontrola po naimenovanju (Carina-Prijemnica-Uvoz-Specifikacija v0.3).
    // Dok skripta Skladista main\Carina_PrijemnicaPL_2026_11.sql nije puštena, ImaPLKolone je false
    // i Prijemnica radi kao ranije (stari uvoz iz Excel-a, snimanje "obriši sve pa upiši").
    public partial class Prijemnica
    {
        bool ImaPLKolone;
        string StatusPrijemnice = "";
        int IDPackingListe;
        string KatalogPL = "";   // katalog artikala aktivne packing liste (DEUS, OSTALI, ...)
        string stavkeSnimak = "";
        DataTable dtNaimenovanja;
        bool popunjavanjeUparivanja;

        Panel pnlUvozPL;
        DataGridView dgvPL;
        Label lblPL;
        Button btnPLUvezi, btnPLZameni, btnPLFormiraj, btnPLPrenesi;
        ComboBox cboPLKlasifikacija;

        Button btnUparivanje;
        Panel pnlUparivanje;
        DataGridView dgvUparivanje, dgvKontrola;
        ComboBox cboDodeliNaimenovanje;
        Button btnDodeliNaimenovanje;
        Label lblUparivanje;

        static readonly string[] TehnickeKolone = { "ID", "IDNaimenovanja", "IDPLStavke", "Koeficijent", "NetoPL", "BrutoPL", "VrednostPL" };
        static readonly string[] PLKolone = { "KoletaPL", "KolicinaJM" };

        #region Inicijalizacija

        private void InitUvozPL()
        {
            ImaPLKolone = ProveriPLKolone();
            if (!ImaPLKolone)
                return;

            btnPackingLista.Visible = true;

            btnUparivanje = new Button
            {
                Text = "Uparivanje i kontrola",
                Location = new Point(btnPackingLista.Right + 12, btnPackingLista.Top),
                Size = new Size(150, btnPackingLista.Height),
                BackColor = btnPackingLista.BackColor,
                ForeColor = btnPackingLista.ForeColor,
                Font = btnPackingLista.Font,
                FlatStyle = btnPackingLista.FlatStyle,
                UseVisualStyleBackColor = false
            };
            btnUparivanje.Click += (s, e) => OtvoriUparivanje();
            Controls.Add(btnUparivanje);

            KreirajPanelUvozPL();
            KreirajPanelUparivanje();
        }

        private bool ProveriPLKolone()
        {
            try
            {
                using (var conn = new SqlConnection(connection))
                using (var cmd = new SqlCommand(
                    "SELECT CASE WHEN COL_LENGTH('dbo.RNCarinskoPrijemnicaStavke','IDNaimenovanja') IS NOT NULL " +
                    "AND OBJECT_ID('dbo.sp_PrijemnicaStavka_Snimi') IS NOT NULL AND OBJECT_ID('dbo.RNCarinskoPackingLista') IS NOT NULL " +
                    "AND OBJECT_ID('dbo.fn_PL_Koeficijent') IS NOT NULL " +
                    "AND COL_LENGTH('dbo.Artikli','Katalog') IS NOT NULL AND COL_LENGTH('dbo.RNCarinskoPackingLista','Katalog') IS NOT NULL " +
                    "AND COL_LENGTH('dbo.RNCarinskoPackingLista','NazivLista') IS NOT NULL AND COL_LENGTH('dbo.RNCarinskoPackingListaStavka','KoletaPoPaleti') IS NOT NULL " +
                    "THEN 1 ELSE 0 END", conn))
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

        // Status prijemnice, aktivna packing lista i stanje stavki posle učitavanja
        private void PosleUcitavanjaStavki()
        {
            UcitajStatusPrijemnice();
            stavkeSnimak = PotpisStavki();
        }

        private void UcitajStatusPrijemnice()
        {
            StatusPrijemnice = "";
            IDPackingListe = 0;
            if (!ImaPLKolone || txtPrijemnica.Text == "")
                return;

            using (var conn = new SqlConnection(connection))
            using (var cmd = new SqlCommand(
                "SELECT RTRIM(ISNULL(Status,'')) FROM RNCarinskoSkladistePrijemnica WHERE ID = @P; " +
                "SELECT TOP 1 ID FROM RNCarinskoPackingLista WHERE IDPrijemnice = @P AND Status <> N'Zamenjeno' ORDER BY ID DESC;", conn))
            {
                cmd.Parameters.AddWithValue("@P", Convert.ToInt32(txtPrijemnica.Text));
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        StatusPrijemnice = dr.GetString(0);
                    if (dr.NextResult() && dr.Read())
                        IDPackingListe = dr.GetInt32(0);
                }
            }
        }

        private bool ImaPravoKorekcije()
        {
            string lista = Administracija.Podesavanja.Get("Carina.Prijemnica.KorekcijaKorisnici", "");
            return lista.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                        .Any(k => string.Equals(k.Trim(), (Korisnik ?? "").Trim(), StringComparison.OrdinalIgnoreCase));
        }

        private bool StavkeZakljucane()
        {
            return StatusPrijemnice == "ZA" && !ImaPravoKorekcije();
        }

        private string PotpisStavki()
        {
            return string.Join("|", list.Select(s => string.Join(";", new object[] {
                s.ID, s.RB, s.NHM, s.IDArtikla, s.Naziv, s.Naimenovanje, s.JM, s.Koleta, s.Bruto, s.Neto, s.Vrednost, s.Valuta,
                s.Pozicija, s.Paleta, s.VrstaPaleta, s.PDV, s.Carina, s.Napomena, s.IDNaimenovanja, s.KoletaPL, s.KolicinaJM, s.IDPLStavke })));
        }

        // Dodatna polja stavke (posle skripte); poziva VratiStavkePrijemnice
        private void PopuniPLPolja(PrijemnicaStavke stavka, SqlDataReader dr)
        {
            stavka.ID = Convert.ToInt32(dr["ID"]);
            stavka.IDNaimenovanja = IntIliNull(dr["IDNaimenovanja"]);
            stavka.KoletaPL = DecIliNull(dr["KoletaPL"]);
            stavka.KolicinaJM = DecIliNull(dr["KolicinaJM"]);
            stavka.IDPLStavke = IntIliNull(dr["IDPLStavke"]);
            stavka.Koeficijent = DecIliNull(dr["Koeficijent"]);
            stavka.NetoPL = DecIliNull(dr["NetoPL"]);
            stavka.BrutoPL = DecIliNull(dr["BrutoPL"]);
            stavka.VrednostPL = DecIliNull(dr["VrednostPL"]);
        }

        // Tehnička polja stavke se ne prikazuju u tabeli stavki
        private void SakrijTehnickeKolone()
        {
            try
            {
                var sakrij = ImaPLKolone ? TehnickeKolone : TehnickeKolone.Concat(PLKolone).ToArray();
                var vidljive = gridGroupingControl1.TableDescriptor.VisibleColumns;
                for (int i = vidljive.Count - 1; i >= 0; i--)
                    if (sakrij.Contains(vidljive[i].Name))
                        vidljive.RemoveAt(i);

                if (ImaPLKolone)
                {
                    var kolone = gridGroupingControl1.TableDescriptor.Columns;
                    if (kolone.IndexOf("KoletaPL") >= 0) kolone["KoletaPL"].HeaderText = "Koleta PL";
                    if (kolone.IndexOf("KolicinaJM") >= 0) kolone["KolicinaJM"].HeaderText = "Količina JM";
                }
            }
            catch
            {
                // prikaz kolona nije bitan za rad forme
            }
        }

        private static int? IntIliNull(object v)
        {
            return v == null || v == DBNull.Value ? (int?)null : Convert.ToInt32(v);
        }

        private static decimal? DecIliNull(object v)
        {
            return v == null || v == DBNull.Value ? (decimal?)null : Convert.ToDecimal(v);
        }

        private static object DbVrednost(object v)
        {
            return v ?? DBNull.Value;
        }

        private static Button NapraviDugme(string tekst, int x, int y, int sirina)
        {
            return new Button { Text = tekst, Location = new Point(x, y), Size = new Size(sirina, 32), UseVisualStyleBackColor = true };
        }

        private static DataGridView NapraviTabelu()
        {
            var dgv = new DataGridView
            {
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            };
            dgv.RowTemplate.Height = 26;
            dgv.DataError += (s, e) => { e.ThrowException = false; };
            return dgv;
        }

        #endregion

        #region Uvoz packing liste (4.1, 4.2)

        private void KreirajPanelUvozPL()
        {
            pnlUvozPL = new Panel
            {
                Location = new Point(13, 40),
                Size = new Size(ClientSize.Width - 26, ClientSize.Height - 48),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.Gainsboro,
                Visible = false
            };

            btnPLUvezi = NapraviDugme("Uvezi fajl", 10, 8, 110);
            btnPLZameni = NapraviDugme("Zameni uvoz", 130, 8, 110);
            btnPLFormiraj = NapraviDugme("Formiraj nove artikle", 250, 8, 160);
            btnPLPrenesi = NapraviDugme("Prenesi u stavke", 420, 8, 140);
            var btnNazad = NapraviDugme("Nazad", pnlUvozPL.Width - 100, 8, 88);
            btnNazad.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            btnPLUvezi.Click += (s, e) => UveziFajl(false);
            btnPLZameni.Click += (s, e) => UveziFajl(true);
            btnPLFormiraj.Click += (s, e) => FormirajArtikle();
            btnPLPrenesi.Click += (s, e) => PrenesiUStavke();
            btnNazad.Click += (s, e) => pnlUvozPL.Visible = false;

            // Klasifikacija (četvrti nivo) za nove artikle kad je fajl nema
            var lblKlasifikacija = new Label { Text = "Klasifikacija novih artikala", Location = new Point(575, 15), AutoSize = true };
            cboPLKlasifikacija = new ComboBox
            {
                Location = new Point(750, 12),
                Width = 330,
                DropDownStyle = ComboBoxStyle.DropDown,
                AutoCompleteMode = AutoCompleteMode.SuggestAppend,
                AutoCompleteSource = AutoCompleteSource.ListItems
            };

            // Stablo klasifikacije sa artiklima kataloga; dvoklik na četvrti nivo ga bira za nove artikle
            var btnStablo = NapraviDugme("Pregled klasifikacije", 1090, 8, 160);
            btnStablo.Click += (s, e) =>
            {
                try
                {
                    ArtikliKatalogDijalozi.PrikaziKlasifikaciju(this, connection, KatalogPL, id =>
                    {
                        UcitajKlasifikacije();
                        cboPLKlasifikacija.SelectedValue = id;
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Greška pri prikazu klasifikacije: " + ex.Message);
                }
            };
            pnlUvozPL.Controls.AddRange(new Control[] { lblKlasifikacija, cboPLKlasifikacija, btnStablo });

            lblPL = new Label
            {
                Location = new Point(10, 48),
                Size = new Size(pnlUvozPL.Width - 20, 40),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            dgvPL = NapraviTabelu();
            dgvPL.Location = new Point(10, 92);
            dgvPL.Size = new Size(pnlUvozPL.Width - 20, pnlUvozPL.Height - 102);
            dgvPL.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvPL.CellEndEdit += DgvPL_CellEndEdit;

            pnlUvozPL.Controls.AddRange(new Control[] { btnPLUvezi, btnPLZameni, btnPLFormiraj, btnPLPrenesi, btnNazad, lblPL, dgvPL });
            Controls.Add(pnlUvozPL);
        }

        private void OtvoriUvozPL()
        {
            if (txtPrijemnica.Text == "")
            {
                MessageBox.Show("Prvo snimite prijemnicu (Snimi), pa uvezite packing listu.");
                return;
            }

            try
            {
                UcitajStatusPrijemnice();
                UcitajKlasifikacije();
                pnlUvozPL.Visible = true;
                pnlUvozPL.BringToFront();
                PrikaziPL();
                if (IDPackingListe == 0 && StatusPrijemnice != "ZA")
                    UveziFajl(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri otvaranju packing liste: " + ex.Message);
            }
        }

        private void UcitajKlasifikacije()
        {
            if (cboPLKlasifikacija.DataSource != null)
                return;

            var dt = new DataTable();
            using (var conn = new SqlConnection(connection))
            using (var da = new SqlDataAdapter(@"SELECT c.ID,
                    RTRIM(CONVERT(nvarchar(200), p.Naziv)) + ' / ' + RTRIM(CONVERT(nvarchar(200), d.Naziv)) + ' / '
                  + RTRIM(CONVERT(nvarchar(200), t.Naziv)) + ' / ' + RTRIM(CONVERT(nvarchar(200), c.Naziv)) AS Putanja
                FROM ArtikliCetvrtiNivo c
                INNER JOIN ArtikliTreciNivo t ON c.IDNadredjena = t.ID
                INNER JOIN ArtikliDrugiNivo d ON t.IDNadredjena = d.ID
                INNER JOIN ArtikliPrviNivo p ON d.IDNadredjena = p.ID
                ORDER BY Putanja", conn))
                da.Fill(dt);

            cboPLKlasifikacija.DataSource = dt;
            cboPLKlasifikacija.DisplayMember = "Putanja";
            cboPLKlasifikacija.ValueMember = "ID";
            cboPLKlasifikacija.SelectedIndex = -1;
        }

        private void PrikaziPL()
        {
            dgvPL.DataSource = null;
            KatalogPL = "";
            bool zakljucana = StatusPrijemnice == "ZA";
            btnPLUvezi.Enabled = !zakljucana && IDPackingListe == 0;
            btnPLZameni.Enabled = !zakljucana && IDPackingListe > 0;
            btnPLFormiraj.Enabled = false;
            btnPLPrenesi.Enabled = false;

            if (IDPackingListe == 0)
            {
                lblPL.Text = zakljucana
                    ? "Prijemnica je potvrđena (ZA): uvoz packing liste nije dozvoljen."
                    : "Za ovu prijemnicu još nije uvezena packing lista. Izaberite fajl (Uvezi fajl).";
                return;
            }

            var dt = new DataTable();
            string zaglavlje = "";
            using (var conn = new SqlConnection(connection))
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT NazivFajla, DatumUvoza, ISNULL(Korisnik,''), Status, ISNULL(Katalog,''), ISNULL(NazivLista,'') FROM RNCarinskoPackingLista WHERE ID = @ID", conn))
                {
                    cmd.Parameters.AddWithValue("@ID", IDPackingListe);
                    using (var dr = cmd.ExecuteReader())
                        if (dr.Read())
                        {
                            KatalogPL = dr.GetString(4);
                            string nazivLista = dr.GetString(5);
                            zaglavlje = "Packing lista: " + dr.GetString(0) + (nazivLista != "" ? ", list: " + nazivLista : "")
                                      + ", uvezena " + dr.GetDateTime(1).ToString("dd.MM.yyyy HH:mm")
                                      + " (" + dr.GetString(2).Trim() + "), status " + dr.GetString(3)
                                      + (KatalogPL != "" ? ", katalog " + KatalogPL : "") + ".";
                        }
                }

                // JM po koletu: postojeći artikal -> iz šifarnika (ako ga ima), nov artikal -> iz fajla ili unet
                var da = new SqlDataAdapter(@"SELECT s.ID, s.IDArtikla, dbo.fn_PL_Koeficijent(CONVERT(nvarchar(100), a.KoeficijentKoletaJM)) AS KoefArtikla,
                        s.RedUFajlu AS [Red],
                        CASE s.StatusReda WHEN N'NoviArtikal' THEN N'Nov artikal' WHEN N'Greska' THEN N'Greška' ELSE s.StatusReda END AS [Status],
                        s.Poruka, s.Sifra AS [Šifra], s.Artikal, s.JM, s.Kolicina AS [Količina],
                        COALESCE(dbo.fn_PL_Koeficijent(CONVERT(nvarchar(100), a.KoeficijentKoletaJM)), s.Koeficijent) AS [JM po koletu],
                        s.KoletaObracun AS [Koleta], s.Koleta AS [Koleta u fajlu], s.Neto AS [Neto kg], s.Bruto AS [Bruto kg], s.Vrednost, s.Valuta, s.NHM,
                        ISNULL(NULLIF(s.Nivo1Naziv, N''), s.Nivo1Sifra) AS [Nivo 1], ISNULL(NULLIF(s.Nivo2Naziv, N''), s.Nivo2Sifra) AS [Nivo 2],
                        ISNULL(NULLIF(s.Nivo3Naziv, N''), s.Nivo3Sifra) AS [Nivo 3], ISNULL(NULLIF(s.Nivo4Naziv, N''), s.Nivo4Sifra) AS [Nivo 4]
                    FROM RNCarinskoPackingListaStavka s
                    LEFT JOIN Artikli a ON a.ID = s.IDArtikla
                    WHERE s.IDPackingListe = @ID ORDER BY s.RedUFajlu", conn);
                da.SelectCommand.Parameters.AddWithValue("@ID", IDPackingListe);
                da.Fill(dt);
            }

            dgvPL.DataSource = dt;
            dgvPL.Columns["ID"].Visible = false;
            dgvPL.Columns["IDArtikla"].Visible = false;
            dgvPL.Columns["KoefArtikla"].Visible = false;
            foreach (DataGridViewColumn kol in dgvPL.Columns)
            {
                kol.ReadOnly = kol.Name != "JM po koletu";
                kol.SortMode = DataGridViewColumnSortMode.NotSortable;
                if (kol.ValueType == typeof(decimal))
                    kol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            int ok = 0, upoz = 0, novi = 0, greske = 0, noviBezKoef = 0;
            foreach (DataGridViewRow red in dgvPL.Rows)
            {
                string status = Convert.ToString(red.Cells["Status"].Value);
                bool nov = status == "Nov artikal";
                // JM po koletu se menja dok artikal nije formiran, i kad je red u grešci zbog pogrešnog koeficijenta
                // menja se uvek dok prijemnica nije potvrđena (i za postojeći artikal, uz potvrdu pri izmeni)
                bool menja = !zakljucana;
                red.Cells["JM po koletu"].ReadOnly = !menja;
                if (menja)
                    red.Cells["JM po koletu"].Style.BackColor = Color.White;
                if (status == "Greška") { greske++; red.DefaultCellStyle.BackColor = Color.MistyRose; }
                else if (nov)
                {
                    novi++;
                    red.DefaultCellStyle.BackColor = Color.LightCyan;
                    if (red.Cells["JM po koletu"].Value == DBNull.Value) noviBezKoef++;
                }
                else if (status == "Upozorenje") { upoz++; red.DefaultCellStyle.BackColor = Color.LemonChiffon; }
                else ok++;
            }

            lblPL.Text = zaglavlje + "  Redova: " + dgvPL.Rows.Count + " (Ok " + ok + ", upozorenje " + upoz + ", nov artikal " + novi + ", greška " + greske + ")."
                + (novi > 0 ? "  Za nove artikle unesite JM po koletu, pa Formiraj nove artikle." : "")
                + (greske > 0 ? "  Redovi sa greškom blokiraju prenos: ispravite fajl i uradite Zameni uvoz." : "");

            btnPLFormiraj.Enabled = !zakljucana && novi > noviBezKoef;
            btnPLPrenesi.Enabled = !zakljucana && novi == 0 && greske == 0 && dgvPL.Rows.Count > 0;
        }

        // Koeficijent (JM po koletu) za nov artikal unosi korisnik u pregledu (OP-17)
        private void DgvPL_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvPL.Columns[e.ColumnIndex].Name != "JM po koletu")
                return;

            try
            {
                object v = dgvPL.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                decimal? koef = v == null || v == DBNull.Value ? (decimal?)null : Convert.ToDecimal(v);
                if (koef.HasValue && koef.Value <= 0)
                {
                    MessageBox.Show("JM po koletu mora biti veće od nule.");
                    koef = null;
                }

                int id = Convert.ToInt32(dgvPL.Rows[e.RowIndex].Cells["ID"].Value);
                object idArtikla = dgvPL.Rows[e.RowIndex].Cells["IDArtikla"].Value;
                object staraVrednost = dgvPL.Rows[e.RowIndex].Cells["KoefArtikla"].Value;

                // postojeći artikal: JM po koletu se upisuje u artikal; izmena postojeće vrednosti traži potvrdu
                if (idArtikla != DBNull.Value)
                {
                    decimal? stari = staraVrednost == DBNull.Value ? (decimal?)null : Convert.ToDecimal(staraVrednost);
                    if (stari == koef)
                        return;
                    if (stari.HasValue && MessageBox.Show(
                            "Menja se JM po koletu artikla " + Convert.ToString(dgvPL.Rows[e.RowIndex].Cells["Artikal"].Value)
                            + " sa " + stari.Value.ToString("0.######") + " na " + (koef.HasValue ? koef.Value.ToString("0.######") : "(prazno)")
                            + ".\nNova vrednost važi za sve buduće prijeme ovog artikla. Nastaviti?",
                            "JM po koletu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        BeginInvoke(new Action(PrikaziPL));
                        return;
                    }
                }

                string upis = idArtikla == DBNull.Value
                    ? "UPDATE RNCarinskoPackingListaStavka SET Koeficijent = @K WHERE ID = @ID; "
                    : "UPDATE Artikli SET KoeficijentKoletaJM = @K WHERE ID = @IDArt; ";

                using (var conn = new SqlConnection(connection))
                using (var cmd = new SqlCommand(upis + "EXEC dbo.sp_PL_Analiziraj @PL, @Kljuc;", conn))
                {
                    cmd.Parameters.AddWithValue("@IDArt", idArtikla);
                    cmd.Parameters.AddWithValue("@Kljuc", KljucArtikla());
                    cmd.Parameters.AddWithValue("@K", DbVrednost(koef));
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@PL", IDPackingListe);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                BeginInvoke(new Action(PrikaziPL));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri unosu koeficijenta: " + ex.Message);
            }
        }

        private void UveziFajl(bool zameni)
        {
            UcitajStatusPrijemnice();
            if (StatusPrijemnice == "ZA")
            {
                MessageBox.Show("Prijemnica je potvrđena (ZA): uvoz i zamena packing liste nisu dozvoljeni.");
                return;
            }
            if (!zameni && IDPackingListe > 0)
            {
                MessageBox.Show("Za ovu prijemnicu je već uvezena packing lista. Za drugi fajl koristite Zameni uvoz.");
                return;
            }

            string putanja;
            using (var ofd = new OpenFileDialog { Title = "Izaberite packing listu", Filter = "Excel fajlovi (*.xlsx;*.xls)|*.xlsx;*.xls" })
            {
                if (ofd.ShowDialog() != DialogResult.OK)
                    return;
                putanja = ofd.FileName;
            }

            try
            {
                byte[] sadrzaj;
                using (var fs = new FileStream(putanja, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    sadrzaj = ms.ToArray();
                }

                string hash;
                using (var sha = SHA256.Create())
                    hash = string.Concat(sha.ComputeHash(sadrzaj).Select(b => b.ToString("X2")));

                int idPrijemnice = Convert.ToInt32(txtPrijemnica.Text);
                using (var conn = new SqlConnection(connection))
                using (var cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM RNCarinskoPackingLista WHERE IDPrijemnice = @P AND HashFajla = @H AND Status <> N'Zamenjeno'", conn))
                {
                    cmd.Parameters.AddWithValue("@P", idPrijemnice);
                    cmd.Parameters.AddWithValue("@H", hash);
                    conn.Open();
                    if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("Ova packing lista (isti fajl) je već uvezena za ovu prijemnicu.");
                        return;
                    }
                }

                string greska;
                List<PLRed> redovi = CitajPackingListu(sadrzaj, out greska);
                if (greska != null)
                {
                    MessageBox.Show(greska);
                    return;
                }
                if (redovi.Count == 0)
                {
                    MessageBox.Show("U fajlu nema redova sa podacima ispod zaglavlja.");
                    return;
                }

                if (zameni && MessageBox.Show("Postojeći uvoz se označava kao zamenjen, a stavke iz njega se uklanjaju pri prenosu novog. Nastaviti?",
                        "Zameni uvoz", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                // artikli iz fajla se prepoznaju unutar kataloga (šifra je jedinstvena po katalogu)
                string katalog = ArtikliKatalogDijalozi.IzaberiKatalog(this, connection, "Katalog packing liste", KatalogPL);
                if (katalog == null)
                    return;

                IDPackingListe = UpisiPackingListu(idPrijemnice, Path.GetFileName(putanja), hash, redovi, zameni, katalog);
                ArhivirajFajl(putanja, idPrijemnice);
                PrikaziPL();
            }
            catch (Exception ex)
            {
                var sqlGreska = ex as SqlException;
                MessageBox.Show("Greška pri uvozu packing liste: " + ex.Message
                    + (sqlGreska != null ? "\n(baza: " + sqlGreska.Procedure + ", red " + sqlGreska.LineNumber + ")" : ""));
            }
        }

        private int UpisiPackingListu(int idPrijemnice, string nazivFajla, string hash, List<PLRed> redovi, bool zameni, string katalog)
        {
            int id;
            using (var conn = new SqlConnection(connection))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    if (zameni)
                        using (var cmd = new SqlCommand("UPDATE RNCarinskoPackingLista SET Status = N'Zamenjeno' WHERE IDPrijemnice = @P AND Status <> N'Zamenjeno'", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@P", idPrijemnice);
                            cmd.ExecuteNonQuery();
                        }

                    using (var cmd = new SqlCommand(
                        "INSERT INTO RNCarinskoPackingLista (IDPrijemnice, NazivFajla, HashFajla, Korisnik, Status, Katalog, NazivLista) OUTPUT INSERTED.ID VALUES (@P, @N, @H, @K, N'Uvezeno', @Kat, @List)", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@Kat", katalog);
                        cmd.Parameters.AddWithValue("@List", Skrati(poslednjiListPL ?? "", 100));
                        cmd.Parameters.AddWithValue("@P", idPrijemnice);
                        cmd.Parameters.AddWithValue("@N", Skrati(nazivFajla, 260));
                        cmd.Parameters.AddWithValue("@H", hash);
                        cmd.Parameters.AddWithValue("@K", Skrati((Korisnik ?? "").Trim(), 50));
                        id = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // RAZ-09: kolona Kontejner postoji posle skripte Carina_StavkaKontejner_2026_11.sql
                    bool kolonaKontejner;
                    using (var cmd = new SqlCommand("SELECT CASE WHEN COL_LENGTH('dbo.RNCarinskoPackingListaStavka', 'Kontejner') IS NULL THEN 0 ELSE 1 END", conn, tran))
                        kolonaKontejner = Convert.ToInt32(cmd.ExecuteScalar()) == 1;

                    using (var cmd = new SqlCommand(@"INSERT INTO RNCarinskoPackingListaStavka
                        (IDPackingListe, RedUFajlu, Nivo1Sifra, Nivo1Naziv, Nivo2Sifra, Nivo2Naziv, Nivo3Sifra, Nivo3Naziv, Nivo4Sifra, Nivo4Naziv,
                         Sifra, Artikal, JM, Kolicina, Koleta, Koeficijent, Neto, Bruto, Vrednost, Valuta, NHM,
                         BarKod, KataloskiBroj, NetoPoJM, BrutoPoJM, CenaPoJM, JMPoPaleti, KoletaPoPaleti" + (kolonaKontejner ? ", Kontejner" : "") + @")
                        VALUES (@PL, @Red, @N1S, @N1N, @N2S, @N2N, @N3S, @N3N, @N4S, @N4N, @Sifra, @Artikal, @JM, @Kolicina, @Koleta, @Koef,
                                @Neto, @Bruto, @Vrednost, @Valuta, @NHM, @BarKod, @KatBroj, @NetoPoJM, @BrutoPoJM, @CenaPoJM, @JMPoPaleti, @KPP" + (kolonaKontejner ? ", @Kontejner" : "") + ")", conn, tran))
                    {
                        foreach (var r in redovi)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@PL", id);
                            cmd.Parameters.AddWithValue("@Red", r.Red);
                            cmd.Parameters.AddWithValue("@N1S", DbVrednost(Skrati(r.Nivo1Sifra, 50)));
                            cmd.Parameters.AddWithValue("@N1N", DbVrednost(Skrati(r.Nivo1Naziv, 200)));
                            cmd.Parameters.AddWithValue("@N2S", DbVrednost(Skrati(r.Nivo2Sifra, 50)));
                            cmd.Parameters.AddWithValue("@N2N", DbVrednost(Skrati(r.Nivo2Naziv, 200)));
                            cmd.Parameters.AddWithValue("@N3S", DbVrednost(Skrati(r.Nivo3Sifra, 50)));
                            cmd.Parameters.AddWithValue("@N3N", DbVrednost(Skrati(r.Nivo3Naziv, 200)));
                            cmd.Parameters.AddWithValue("@N4S", DbVrednost(Skrati(r.Nivo4Sifra, 50)));
                            cmd.Parameters.AddWithValue("@N4N", DbVrednost(Skrati(r.Nivo4Naziv, 200)));
                            cmd.Parameters.AddWithValue("@Sifra", DbVrednost(Skrati(r.Sifra, 50)));
                            cmd.Parameters.AddWithValue("@Artikal", DbVrednost(Skrati(r.Artikal, 250)));
                            cmd.Parameters.AddWithValue("@JM", DbVrednost(Skrati(r.JM, 20)));
                            cmd.Parameters.AddWithValue("@Kolicina", DbVrednost(r.Kolicina));
                            cmd.Parameters.AddWithValue("@Koleta", DbVrednost(r.Koleta));
                            cmd.Parameters.AddWithValue("@Koef", DbVrednost(r.Koeficijent));
                            cmd.Parameters.AddWithValue("@Neto", DbVrednost(r.Neto));
                            cmd.Parameters.AddWithValue("@Bruto", DbVrednost(r.Bruto));
                            cmd.Parameters.AddWithValue("@Vrednost", DbVrednost(r.Vrednost));
                            cmd.Parameters.AddWithValue("@Valuta", DbVrednost(Skrati(r.Valuta, 10)));
                            cmd.Parameters.AddWithValue("@NHM", DbVrednost(Skrati(r.NHM, 20)));
                            cmd.Parameters.AddWithValue("@BarKod", DbVrednost(Skrati(r.BarKod, 50)));
                            cmd.Parameters.AddWithValue("@KatBroj", DbVrednost(Skrati(r.KataloskiBroj, 50)));
                            cmd.Parameters.AddWithValue("@NetoPoJM", DbVrednost(r.NetoPoJM));
                            cmd.Parameters.AddWithValue("@BrutoPoJM", DbVrednost(r.BrutoPoJM));
                            cmd.Parameters.AddWithValue("@CenaPoJM", DbVrednost(r.CenaPoJM));
                            cmd.Parameters.AddWithValue("@JMPoPaleti", DbVrednost(r.JMPoPaleti));
                            cmd.Parameters.AddWithValue("@KPP", DbVrednost(r.KoletaPoPaleti));
                            if (kolonaKontejner)
                                cmd.Parameters.AddWithValue("@Kontejner", DbVrednost(Skrati(r.Kontejner, 20)));
                            cmd.ExecuteNonQuery();
                        }
                    }

                    using (var cmd = new SqlCommand("dbo.sp_PL_Analiziraj", conn, tran) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.AddWithValue("@IDPackingListe", id);
                        cmd.Parameters.AddWithValue("@KljucArtikla", KljucArtikla());
                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                }
            }
            return id;
        }

        private void ArhivirajFajl(string putanja, int idPrijemnice)
        {
            string arhiva = Administracija.Podesavanja.Get("Carina.PL.Arhiva", "");
            if (arhiva == "")
                return;

            try
            {
                string folder = Path.Combine(arhiva, "Prijemnica_" + idPrijemnice);
                Directory.CreateDirectory(folder);
                File.Copy(putanja, Path.Combine(folder, DateTime.Now.ToString("yyyyMMdd_HHmmss_") + Path.GetFileName(putanja)), true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Packing lista je uvezena, ali fajl nije kopiran u arhivu (" + arhiva + "): " + ex.Message);
            }
        }

        private void FormirajArtikle()
        {
            var noviRedovi = dgvPL.Rows.Cast<DataGridViewRow>().Where(r => Convert.ToString(r.Cells["Status"].Value) == "Nov artikal").ToList();
            int bezKoef = noviRedovi.Count(r => r.Cells["JM po koletu"].Value == DBNull.Value);
            bool trebaKlasifikacija = noviRedovi.Any(r => Convert.ToString(r.Cells["Nivo 4"].Value).Trim() == "");
            object klasifikacija = cboPLKlasifikacija.SelectedValue;
            if (trebaKlasifikacija && klasifikacija == null)
            {
                MessageBox.Show("Fajl nema klasifikaciju za nove artikle. Izaberite je u polju Klasifikacija novih artikala.");
                cboPLKlasifikacija.Focus();
                return;
            }
            string katalog;
            try
            {
                // katalog u koji se upisuju novi artikli: lista u kojoj se bira samo jedna vrednost
                katalog = ArtikliKatalogDijalozi.IzaberiKatalog(this, connection, "Katalog novih artikala", KatalogPL);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri učitavanju kataloga: " + ex.Message);
                return;
            }
            if (katalog == null)
                return;

            string poruka = "Formiraju se novi artikli u katalogu " + katalog + " i nedostajući nivoi klasifikacije za redove „Nov artikal“ sa unetim JM po koletu."
                + (KatalogPL != "" && !string.Equals(KatalogPL, katalog, StringComparison.OrdinalIgnoreCase)
                    ? "\n\nKatalog packing liste se menja iz " + KatalogPL + " u " + katalog + "; artikli se ponovo traže u tom katalogu." : "")
                + (bezKoef > 0 ? "\n\nRedova bez JM po koletu: " + bezKoef + ". Oni se ne formiraju." : "")
                + "\n\nPostojeći artikli i nivoi se ne menjaju. Nastaviti?";
            if (MessageBox.Show(poruka, "Formiraj nove artikle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                int formirano;
                using (var conn = new SqlConnection(connection))
                using (var cmd = new SqlCommand("dbo.sp_PL_FormirajArtikle", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@IDPackingListe", IDPackingListe);
                    cmd.Parameters.AddWithValue("@KljucArtikla", KljucArtikla());
                    cmd.Parameters.AddWithValue("@IDCetvrtiNivo", trebaKlasifikacija ? klasifikacija : DBNull.Value);
                    cmd.Parameters.AddWithValue("@Katalog", katalog);
                    var izlaz = cmd.Parameters.Add("@Formirano", SqlDbType.Int);
                    izlaz.Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    formirano = izlaz.Value == DBNull.Value ? 0 : Convert.ToInt32(izlaz.Value);
                }
                MessageBox.Show("Formirano artikala: " + formirano + ".");
                PrikaziPL();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri formiranju artikala: " + ex.Message);
            }
        }

        // Prenos u stavke prijemnice (u memoriji); trajno se upisuje na Snimi
        private void PrenesiUStavke()
        {
            try
            {
                var dt = new DataTable();
                using (var conn = new SqlConnection(connection))
                using (var da = new SqlDataAdapter("dbo.sp_PL_PrenesiUStavke", conn))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@IDPackingListe", IDPackingListe);
                    da.Fill(dt);
                }

                int izPL = list.Count(s => s.IDPLStavke.HasValue);
                if (izPL > 0 && MessageBox.Show("Stavke iz ranijeg prenosa packing liste (" + izPL + ") zamenjuju se novim. Ručno unete stavke ostaju. Nastaviti?",
                        "Prenesi u stavke", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                list.RemoveAll(s => s.IDPLStavke.HasValue);

                dtNaimenovanja = UcitajNaimenovanja();
                foreach (DataRow r in dt.Rows)
                {
                    var stavka = new PrijemnicaStavke
                    {
                        RB = 0,
                        IDArtikla = Convert.ToInt32(r["IDArtikla"]),
                        Naziv = Convert.ToString(r["Artikal"]),
                        Naimenovanje = "",
                        JM = Convert.ToString(r["JM"]),
                        Koleta = DecIliNull(r["KoletaPL"]) ?? 0,
                        KoletaPL = DecIliNull(r["KoletaPL"]),
                        Koeficijent = DecIliNull(r["Koeficijent"]),
                        KolicinaJM = DecIliNull(r["Kolicina"]),
                        Neto = DecIliNull(r["Neto"]) ?? 0,
                        Bruto = DecIliNull(r["Bruto"]) ?? 0,
                        Vrednost = DecIliNull(r["Vrednost"]) ?? 0,
                        NetoPL = DecIliNull(r["Neto"]),
                        BrutoPL = DecIliNull(r["Bruto"]),
                        VrednostPL = DecIliNull(r["Vrednost"]),
                        Valuta = Convert.ToString(r["Valuta"]),
                        IDPLStavke = Convert.ToInt32(r["IDPLStavke"]),
                        Pozicija = "",
                        PaletaOpis = "",
                        DimenzijePaleta = "",
                        Napomena = ""
                    };
                    PredloziNaimenovanje(stavka, Convert.ToString(r["NHM"]));
                    list.Add(stavka);
                }

                ReorderRB();
                OsveziGrid();
                pnlUvozPL.Visible = false;

                int bez = list.Count(s => !s.IDNaimenovanja.HasValue);
                MessageBox.Show("Preneto stavki: " + dt.Rows.Count + "." + (bez > 0 ? " Stavki bez naimenovanja: " + bez + "." : "")
                    + "\nProverite koleta i naimenovanja, pa Snimi.");
                OtvoriUparivanje();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri prenosu u stavke: " + ex.Message);
            }
        }

        // 4.3: jedno naimenovanje -> svima; inače predlog po NHM kad se poklapa sa tačno jednim naimenovanjem
        private void PredloziNaimenovanje(PrijemnicaStavke stavka, string nhm)
        {
            if (dtNaimenovanja == null || dtNaimenovanja.Rows.Count == 0)
                return;

            if (dtNaimenovanja.Rows.Count == 1)
            {
                DodeliNaimenovanje(stavka, dtNaimenovanja.Rows[0]);
                return;
            }

            string kljuc = SamoCifre(nhm);
            if (kljuc == "")
                return;

            var pogodak = dtNaimenovanja.Rows.Cast<DataRow>()
                .Where(n => SamoCifre(Convert.ToString(n["NHMBroj"])) == kljuc || SamoCifre(Convert.ToString(n["NHM"])) == kljuc)
                .ToList();
            if (pogodak.Count == 1)
                DodeliNaimenovanje(stavka, pogodak[0]);
        }

        // prenesiStope: PDV % i Carina % sa naimenovanja (ISP-07); pri usklađivanju pred snimanje se ne prenose,
        // da ostane ručna izmena na stavci
        private void DodeliNaimenovanje(PrijemnicaStavke stavka, DataRow naim, bool prenesiStope = true)
        {
            if (naim == null)
            {
                stavka.IDNaimenovanja = null;
                return;
            }
            stavka.IDNaimenovanja = Convert.ToInt32(naim["ID"]);
            stavka.Naimenovanje = Convert.ToString(naim["Naimenovanje"]);
            stavka.NHM = TarifniBroj.Normalizuj(naim["NHM"]);
            if (prenesiStope && naim.Table.Columns.Contains("PDV"))
            {
                stavka.PDV = naim["PDV"] == DBNull.Value ? 0 : Convert.ToDecimal(naim["PDV"]);
                stavka.Carina = naim["Carina"] == DBNull.Value ? 0 : Convert.ToDecimal(naim["Carina"]);
            }
        }

        private static string SamoCifre(string s)
        {
            return new string((s ?? "").Where(char.IsDigit).ToArray());
        }

        private static string Skrati(string s, int duzina)
        {
            if (s == null) return null;
            return s.Length > duzina ? s.Substring(0, duzina) : s;
        }

        #endregion

        #region Čitanje Excel fajla (Syncfusion XlsIO, bez Excel-a na računaru)

        private class PLRed
        {
            public int Red;
            public string Nivo1Sifra, Nivo1Naziv, Nivo2Sifra, Nivo2Naziv, Nivo3Sifra, Nivo3Naziv, Nivo4Sifra, Nivo4Naziv;
            public string Sifra, Artikal, JM, Valuta, NHM, BarKod, KataloskiBroj;
            public decimal? Kolicina, Koleta, Koeficijent, Neto, Bruto, Vrednost, NetoPoJM, BrutoPoJM, CenaPoJM, JMPoPaleti;
            public int? KoletaPoPaleti;   // JM po paleti / JM po koletu (predlog za palete)
            public string Kontejner;      // RAZ-09: broj kontejnera reda (opciona kolona), bez razmaka i crtica
        }

        string poslednjiListPL = "";   // list iz koga je uvezena packing lista (ISP-01)

        // Sifra (CAR-P01) ili Naziv: kad šifra u fajlu nije šifra artikla (npr. DEUS: svi redovi imaju šifru 1)
        private static string KljucArtikla()
        {
            return string.Equals(Administracija.Podesavanja.Get("Carina.PL.KljucArtikla", "Sifra"), "Naziv", StringComparison.OrdinalIgnoreCase)
                ? "Naziv" : "Sifra";
        }

        // Polje -> podrazumevani nazivi zaglavlja (isti kao u skripti); vrednost iz Podešavanja ima prednost (OP-19)
        internal static readonly Dictionary<string, string> PLPodrazumevaneKolone = new Dictionary<string, string>
        {
            { "Sifra", "Šifra artikla;Šifra;Sifra;Item code;Code;Article code" },
            { "Artikal", "Naziv artikla;Artikal;Naziv;Opis;Description;Item" },
            { "JM", "JM;Jedinica mere;Unit;UOM" },
            { "Kolicina", "Količina;Kolicina;Quantity;Qty" },
            { "Koleta", "Koleta;Broj koleta;Packages;Cartons;Ctns" },
            { "Koeficijent", "Koeficijent;JM po koletu;Pcs per carton;koficijent JM/ TRANSPORTNA KUTIJA (KOLETO);koeficijent JM/ TRANSPORTNA KUTIJA (KOLETO);koficijent JM/ TR/(KOLETO);koeficijent JM/ TR/(KOLETO)" },
            { "JMPoPaleti", "koficijent JM/ TR/PALETA;koeficijent JM/ TR/PALETA;JM po paleti" },
            { "Neto", "Neto;Neto kg;Net;Net weight" },
            { "Bruto", "Bruto;Bruto kg;Gross;Gross weight" },
            { "Vrednost", "Vrednost;Iznos;Value;Amount" },
            { "Valuta", "Valuta;Currency" },
            { "NHM", "NHM;Tarifni broj;HS code;HS" },
            { "Kontejner", "Kontejner;Container;Container No;Container No.;Cont.;Broj kontejnera" },
            { "NetoPoJM", "Neto težina po JM;Neto po JM;Net weight per unit" },
            { "BrutoPoJM", "Bruto težina po JM;Bruto po JM;Gross weight per unit" },
            { "CenaPoJM", "Cena po JM;Jedinična cena;Unit price" },
            { "BarKod", "Bar kod;Barkod;EAN" },
            { "KataloskiBroj", "Kataloški broj;Kataloski broj;Catalog number" },
            { "Nivo1Sifra", "I nivo;Nivo 1 šifra;Prvi nivo šifra" },
            { "Nivo1Naziv", "Nivo 1 naziv;Prvi nivo naziv;Nivo 1" },
            { "Nivo2Sifra", "II nivo;Nivo 2 šifra;Drugi nivo šifra" },
            { "Nivo2Naziv", "Nivo 2 naziv;Drugi nivo naziv;Nivo 2" },
            { "Nivo3Sifra", "III nivo;Nivo 3 šifra;Treći nivo šifra" },
            { "Nivo3Naziv", "Nivo 3 naziv;Treći nivo naziv;Nivo 3" },
            { "Nivo4Sifra", "IV nivo;Nivo 4 šifra;Četvrti nivo šifra" },
            { "Nivo4Naziv", "Nivo 4 naziv;Četvrti nivo naziv;Nivo 4" }
        };

        private List<PLRed> CitajPackingListu(byte[] sadrzaj, out string greska)
        {
            greska = null;
            var redovi = new List<PLRed>();

            // nazivi iz Podešavanja dopunjuju podrazumevane (ponovno puštanje skripte ne menja postojeće ključeve)
            var nazivi = PLPodrazumevaneKolone.ToDictionary(
                p => p.Key,
                p => new HashSet<string>((p.Value + ";" + Administracija.Podesavanja.Get("Carina.PL.Kolona." + p.Key, ""))
                        .Split(';').Select(Normalizuj).Where(x => x != "")));

            int brojLista = 1, zadatiRed = 0;
            int.TryParse(Administracija.Podesavanja.Get("Carina.PL.List", "1"), out brojLista);
            int.TryParse(Administracija.Podesavanja.Get("Carina.PL.RedZaglavlja", "0"), out zadatiRed);
            var naziviListova = new HashSet<string>(Administracija.Podesavanja.Get("Carina.PL.NazivLista", "PAKING LISTA;PACKING LIST;Packing list")
                .Split(';').Select(Normalizuj).Where(x => x != ""));
            poslednjiListPL = "";

            using (var engine = new ExcelEngine())
            using (var ms = new MemoryStream(sadrzaj))
            {
                IWorkbook knjiga = engine.Excel.Workbooks.Open(ms);
                IWorksheet list = null;
                try
                {
                    // ISP-01: list po nazivu (PAKING LISTA), pa list po rednom broju, pa prvi list na kome je zaglavlje
                    var kandidati = new List<IWorksheet>();
                    foreach (IWorksheet l in knjiga.Worksheets)
                        if (naziviListova.Contains(Normalizuj(l.Name)))
                            kandidati.Add(l);
                    if (brojLista >= 1 && brojLista <= knjiga.Worksheets.Count)
                        kandidati.Add(knjiga.Worksheets[brojLista - 1]);
                    foreach (IWorksheet l in knjiga.Worksheets)
                        kandidati.Add(l);

                    int redZaglavlja = 0;
                    Dictionary<string, int> kolone = null;
                    foreach (IWorksheet l in kandidati.Distinct())
                    {
                        kolone = NadjiZaglavljePL(l, nazivi, zadatiRed, out redZaglavlja);
                        if (kolone != null)
                        {
                            list = l;
                            break;
                        }
                    }

                    if (kolone == null)
                    {
                        greska = "Ni na jednom listu fajla nije pronađeno zaglavlje sa kolonama " + (KljucArtikla() == "Naziv" ? "naziv" : "šifra") + " artikla i količina.\n"
                               + "Nazivi kolona se podešavaju u Administracija > Podešavanja (Carina.PL.Kolona.*).";
                        return redovi;
                    }
                    poslednjiListPL = list.Name;
                    int poslednjiRed = list.UsedRange.LastRow, poslednjaKolona = list.UsedRange.LastColumn;

                    // ISP-03: formule čija vrednost nije sačuvana u fajlu izračunava XlsIO
                    list.EnableSheetCalculations();
                    var formuleBezVrednosti = new List<string>();

                    // DEUS: „I NIVO“ … „IV NIVO“ je kolona šifre nivoa, a naziv je u susednoj koloni bez zaglavlja
                    for (int n = 1; n <= 4; n++)
                    {
                        int kolSifra;
                        if (!kolone.TryGetValue("Nivo" + n + "Sifra", out kolSifra) || kolone.ContainsKey("Nivo" + n + "Naziv"))
                            continue;
                        int desno = kolSifra + 1;
                        if (desno <= poslednjaKolona && !kolone.ContainsValue(desno) && Normalizuj(list[redZaglavlja, desno].DisplayText) == "")
                            kolone["Nivo" + n + "Naziv"] = desno;
                    }

                    Func<int, string, string> tekstPolja = (r, polje) =>
                        kolone.ContainsKey(polje) ? (list[r, kolone[polje]].DisplayText ?? "").Trim() : null;
                    Func<int, string, decimal?> brojPolja = (r, polje) =>
                        kolone.ContainsKey(polje) ? BrojIzCelije(list[r, kolone[polje]], formuleBezVrednosti) : null;
                    // ISP-02: „1/5“ = 1 koleto je 5 JM -> 5; broj bez razlomka je JM po koletu
                    Func<int, string, decimal?> koeficijentPolja = (r, polje) =>
                        kolone.ContainsKey(polje) ? KoeficijentIzCelije(list[r, kolone[polje]]) : null;

                    for (int r = redZaglavlja + 1; r <= poslednjiRed; r++)
                    {
                        var red = new PLRed
                        {
                            Red = r,
                            Sifra = tekstPolja(r, "Sifra"),
                            Artikal = tekstPolja(r, "Artikal"),
                            JM = tekstPolja(r, "JM"),
                            Valuta = tekstPolja(r, "Valuta"),
                            NHM = tekstPolja(r, "NHM"),
                            Nivo1Sifra = tekstPolja(r, "Nivo1Sifra"),
                            Nivo1Naziv = tekstPolja(r, "Nivo1Naziv"),
                            Nivo2Sifra = tekstPolja(r, "Nivo2Sifra"),
                            Nivo2Naziv = tekstPolja(r, "Nivo2Naziv"),
                            Nivo3Sifra = tekstPolja(r, "Nivo3Sifra"),
                            Nivo3Naziv = tekstPolja(r, "Nivo3Naziv"),
                            Nivo4Sifra = tekstPolja(r, "Nivo4Sifra"),
                            Nivo4Naziv = tekstPolja(r, "Nivo4Naziv"),
                            Kolicina = brojPolja(r, "Kolicina"),
                            Koleta = brojPolja(r, "Koleta"),
                            Koeficijent = koeficijentPolja(r, "Koeficijent"),
                            JMPoPaleti = koeficijentPolja(r, "JMPoPaleti"),
                            Neto = brojPolja(r, "Neto"),
                            Bruto = brojPolja(r, "Bruto"),
                            Vrednost = brojPolja(r, "Vrednost"),
                            BarKod = tekstPolja(r, "BarKod"),
                            KataloskiBroj = tekstPolja(r, "KataloskiBroj"),
                            NetoPoJM = brojPolja(r, "NetoPoJM"),
                            BrutoPoJM = brojPolja(r, "BrutoPoJM"),
                            CenaPoJM = brojPolja(r, "CenaPoJM"),
                            Kontejner = BrojKontejneraIzTeksta(tekstPolja(r, "Kontejner"))
                        };

                        // Neto, bruto i cena dati po JM: ukupno = količina × vrednost po JM (kad fajl nema kolonu ukupno)
                        if (red.Kolicina.HasValue)
                        {
                            if (!red.Neto.HasValue && red.NetoPoJM.HasValue) red.Neto = Math.Round(red.Kolicina.Value * red.NetoPoJM.Value, 3);
                            if (!red.Bruto.HasValue && red.BrutoPoJM.HasValue) red.Bruto = Math.Round(red.Kolicina.Value * red.BrutoPoJM.Value, 3);
                            if (!red.Vrednost.HasValue && red.CenaPoJM.HasValue) red.Vrednost = Math.Round(red.Kolicina.Value * red.CenaPoJM.Value, 2);
                        }

                        bool prazan = string.IsNullOrEmpty(red.Sifra) && string.IsNullOrEmpty(red.Artikal) && !red.Kolicina.HasValue;
                        if (prazan)
                            continue;

                        // red zbira ("Ukupno", "Total") nema šifru i ne uvozi se
                        string pocetak = Normalizuj(red.Artikal);
                        if (string.IsNullOrEmpty(red.Sifra) && (pocetak == "" || pocetak.StartsWith("ukupno") || pocetak.StartsWith("total")
                                || pocetak.StartsWith("svega") || pocetak.StartsWith("sum")))
                            continue;

                        // koleta po paleti = JM po paleti / JM po koletu (300 / 5 = 60), samo kad je ceo broj
                        if (red.JMPoPaleti.HasValue && red.Koeficijent.HasValue && red.Koeficijent.Value > 0)
                        {
                            decimal kpp = red.JMPoPaleti.Value / red.Koeficijent.Value;
                            if (kpp > 0 && kpp == Math.Truncate(kpp) && kpp <= int.MaxValue)
                                red.KoletaPoPaleti = (int)kpp;
                        }

                        redovi.Add(red);
                    }

                    if (formuleBezVrednosti.Count > 0)
                    {
                        greska = "Ćelije " + string.Join(", ", formuleBezVrednosti.Distinct().Take(10)) + (formuleBezVrednosti.Count > 10 ? " …" : "")
                               + " imaju formulu bez vrednosti. Otvorite fajl u Excelu i snimite ga, pa uvezite ponovo.";
                        return new List<PLRed>();
                    }
                }
                finally
                {
                    if (list != null)
                        list.DisableSheetCalculations();
                    knjiga.Close();
                }
            }
            return redovi;
        }

        // Red zaglavlja na listu: prvi (u prvih 30 redova ili zadati) u kome su ključ artikla i količina
        private Dictionary<string, int> NadjiZaglavljePL(IWorksheet list, Dictionary<string, HashSet<string>> nazivi, int zadatiRed, out int redZaglavlja)
        {
            redZaglavlja = 0;
            IRange opseg = list.UsedRange;
            int prviRed = opseg.Row, poslednjiRed = opseg.LastRow, poslednjaKolona = opseg.LastColumn;
            if (poslednjiRed < prviRed || poslednjaKolona < 1)
                return null;

            int doReda = zadatiRed > 0 ? zadatiRed : Math.Min(poslednjiRed, prviRed + 29);
            for (int r = zadatiRed > 0 ? zadatiRed : prviRed; r <= doReda; r++)
            {
                var mapa = new Dictionary<string, int>();
                for (int c = 1; c <= poslednjaKolona; c++)
                {
                    string tekst = Normalizuj(list[r, c].DisplayText);
                    if (tekst == "") continue;
                    foreach (var polje in nazivi)
                        if (!mapa.ContainsKey(polje.Key) && polje.Value.Contains(tekst))
                        {
                            mapa[polje.Key] = c;
                            break;
                        }
                }
                if (mapa.ContainsKey(KljucArtikla() == "Naziv" ? "Artikal" : "Sifra") && mapa.ContainsKey("Kolicina"))
                {
                    redZaglavlja = r;
                    return mapa;
                }
            }
            return null;
        }

        internal static decimal? BrojIzCelije(IRange celija)
        {
            return BrojIzCelije(celija, null);
        }

        // ISP-03: formula -> sačuvana vrednost, pa vrednost koju izračuna XlsIO; bez vrednosti -> adresa u listu
        internal static decimal? BrojIzCelije(IRange celija, List<string> formuleBezVrednosti)
        {
            try
            {
                if (celija.HasNumber)
                    return Convert.ToDecimal(celija.Number);
                if (celija.HasFormula)
                {
                    decimal izracunato;
                    string vrednost = celija.CalculatedValue;
                    if (!string.IsNullOrWhiteSpace(vrednost)
                        && decimal.TryParse(vrednost, NumberStyles.Float, CultureInfo.InvariantCulture, out izracunato))
                        return izracunato;
                    if (celija.HasFormulaNumberValue)
                        return Convert.ToDecimal(celija.FormulaNumberValue);

                    decimal? izTeksta = BrojIzTeksta(vrednost) ?? BrojIzTeksta(celija.DisplayText);
                    if (!izTeksta.HasValue && formuleBezVrednosti != null)
                        formuleBezVrednosti.Add(celija.AddressLocal);
                    return izTeksta;
                }
            }
            catch
            {
                // vrednost se čita iz teksta ispod
            }
            return BrojIzTeksta(celija.DisplayText);
        }

        // ISP-02: koeficijent „a/b“ (a koleta = b JM) -> b / a; broj -> taj broj; razlomak upisan kao broj (0,2) -> 1 / broj
        internal static decimal? KoeficijentIzCelije(IRange celija)
        {
            string tekst = (celija.DisplayText ?? "").Trim();
            if (tekst.Contains("/"))
            {
                var delovi = tekst.Split('/');
                decimal? a = delovi.Length == 2 ? BrojIzTeksta(delovi[0]) : null;
                decimal? b = delovi.Length == 2 ? BrojIzTeksta(delovi[1]) : null;
                if (a.HasValue && b.HasValue && a.Value != 0)
                    return Math.Round(b.Value / a.Value, 6);
                return -1;   // nije čitljivo: analiza javlja grešku za red
            }

            decimal? broj = BrojIzCelije(celija);
            if (broj.HasValue && broj.Value > 0 && broj.Value < 1)
                return Math.Round(1 / broj.Value, 6);
            return broj;
        }

        // "16.190", "16.190,50", "16,190.50", "73635" -> broj; poslednji od . i , je decimalni separator
        private static decimal? BrojIzTeksta(string tekst)
        {
            string t = (tekst ?? "").Trim().Replace(" ", "").Replace("\u00A0", "");
            if (t == "")
                return null;

            int tacka = t.LastIndexOf('.'), zarez = t.LastIndexOf(',');
            if (tacka >= 0 && zarez >= 0)
                t = tacka > zarez ? t.Replace(",", "") : t.Replace(".", "").Replace(',', '.');
            else if (zarez >= 0)
                t = t.Replace(',', '.');
            else if (tacka >= 0 && System.Text.RegularExpressions.Regex.IsMatch(t, @"^-?\d{1,3}(\.\d{3})+$"))
                t = t.Replace(".", "");

            decimal d;
            return decimal.TryParse(t, NumberStyles.Number, CultureInfo.InvariantCulture, out d) ? d : (decimal?)null;
        }

        internal static string Normalizuj(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return "";
            var sb = new StringBuilder();
            foreach (char ch in s.Trim().ToLowerInvariant())
            {
                switch (ch)
                {
                    case 'š': sb.Append('s'); break;
                    case 'č': case 'ć': sb.Append('c'); break;
                    case 'ž': sb.Append('z'); break;
                    case 'đ': sb.Append("dj"); break;
                    default: sb.Append(char.IsWhiteSpace(ch) ? ' ' : ch); break;
                }
            }
            // razmaci i prelomi reda se ne porede ("KOLIČIN A", "Neto težina\npo JM")
            string rez = System.Text.RegularExpressions.Regex.Replace(sb.ToString(), @"\s+", "");
            return rez.TrimEnd('.', ':');
        }

        #endregion

        #region Uparivanje sa naimenovanjem, koleta i kontrola (4.3 - 4.5)

        private void KreirajPanelUparivanje()
        {
            pnlUparivanje = new Panel
            {
                Location = new Point(13, 40),
                Size = new Size(ClientSize.Width - 26, ClientSize.Height - 48),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.Gainsboro,
                Visible = false
            };

            var lblNaim = new Label { Text = "Naimenovanje", Location = new Point(10, 15), AutoSize = true };
            cboDodeliNaimenovanje = new ComboBox { Location = new Point(110, 12), Width = 420, DropDownStyle = ComboBoxStyle.DropDownList };
            btnDodeliNaimenovanje = NapraviDugme("Dodeli izabranim redovima", 540, 8, 190);
            btnDodeliNaimenovanje.Click += (s, e) => DodeliIzabranim();
            var btnNazad = NapraviDugme("Nazad", pnlUparivanje.Width - 100, 8, 88);
            btnNazad.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNazad.Click += (s, e) => { pnlUparivanje.Visible = false; OsveziGrid(); };

            lblUparivanje = new Label
            {
                Location = new Point(10, 46),
                Size = new Size(pnlUparivanje.Width - 20, 22),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            dgvUparivanje = NapraviTabelu();
            dgvUparivanje.Location = new Point(10, 70);
            dgvUparivanje.Size = new Size(pnlUparivanje.Width - 20, 330);
            dgvUparivanje.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvUparivanje.MultiSelect = true;
            dgvUparivanje.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvUparivanje.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgvUparivanje.IsCurrentCellDirty && dgvUparivanje.CurrentCell is DataGridViewComboBoxCell)
                    dgvUparivanje.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
            dgvUparivanje.CellValueChanged += DgvUparivanje_CellValueChanged;

            dgvUparivanje.Columns.Add(TekstKolona("RB", "RB", 45, true, null));
            dgvUparivanje.Columns.Add(TekstKolona("Artikal", "Artikal", 260, true, null));
            dgvUparivanje.Columns.Add(TekstKolona("JM", "JM", 50, true, null));
            dgvUparivanje.Columns.Add(TekstKolona("KoletaPL", "Koleta PL", 85, true, "N0"));
            dgvUparivanje.Columns.Add(TekstKolona("Koleta", "Koleta", 85, false, "N0"));
            dgvUparivanje.Columns.Add(TekstKolona("KolicinaJM", "Količina JM", 100, true, "N3"));
            dgvUparivanje.Columns.Add(TekstKolona("Neto", "Neto kg", 100, false, "N3"));
            dgvUparivanje.Columns.Add(TekstKolona("Bruto", "Bruto kg", 100, false, "N3"));
            dgvUparivanje.Columns.Add(TekstKolona("Vrednost", "Vrednost", 110, false, "N2"));
            dgvUparivanje.Columns.Add(TekstKolona("Valuta", "Valuta", 60, true, null));
            dgvUparivanje.Columns.Add(new DataGridViewComboBoxColumn
            {
                Name = "Naimenovanje",
                HeaderText = "Naimenovanje",
                Width = 330,
                ValueMember = "ID",
                DisplayMember = "Prikaz",
                ValueType = typeof(int),
                DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox,
                FlatStyle = FlatStyle.Flat
            });

            var lblKontrola = new Label { Text = "Kontrola po naimenovanju", Location = new Point(10, 410), AutoSize = true, Font = new Font(Font, FontStyle.Bold) };

            dgvKontrola = NapraviTabelu();
            dgvKontrola.Location = new Point(10, 432);
            dgvKontrola.Size = new Size(pnlUparivanje.Width - 20, pnlUparivanje.Height - 442);
            dgvKontrola.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvKontrola.ReadOnly = true;
            dgvKontrola.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvKontrola.Columns.Add(TekstKolona("Naimenovanje", "Naimenovanje", 360, true, null));
            dgvKontrola.Columns.Add(TekstKolona("Mera", "Mera", 90, true, null));
            dgvKontrola.Columns.Add(TekstKolona("Carina", "Carina", 120, true, "N3"));
            dgvKontrola.Columns.Add(TekstKolona("Ranije", "Primljeno ranije", 130, true, "N3"));
            dgvKontrola.Columns.Add(TekstKolona("Ova", "Ova prijemnica", 130, true, "N3"));
            dgvKontrola.Columns.Add(TekstKolona("Preostalo", "Preostalo", 120, true, "N3"));
            dgvKontrola.Columns.Add(TekstKolona("Status", "Status", 120, true, null));

            pnlUparivanje.Controls.AddRange(new Control[] { lblNaim, cboDodeliNaimenovanje, btnDodeliNaimenovanje, btnNazad, lblUparivanje, dgvUparivanje, lblKontrola, dgvKontrola });
            Controls.Add(pnlUparivanje);
        }

        private static DataGridViewTextBoxColumn TekstKolona(string ime, string naslov, int sirina, bool samoCitanje, string format)
        {
            var kol = new DataGridViewTextBoxColumn { Name = ime, HeaderText = naslov, Width = sirina, ReadOnly = samoCitanje, SortMode = DataGridViewColumnSortMode.NotSortable };
            if (format != null)
            {
                kol.DefaultCellStyle.Format = format;
                kol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                kol.ValueType = typeof(decimal);
            }
            if (samoCitanje)
                kol.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            return kol;
        }

        private DataTable UcitajNaimenovanja()
        {
            var dt = new DataTable();
            if (txtID.Text == "")
                return dt;

            using (var conn = new SqlConnection(connection))
            using (var da = new SqlDataAdapter("dbo.sp_Prijem_Kontrola", conn))
            {
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@RN", Convert.ToInt32(txtID.Text));
                da.SelectCommand.Parameters.AddWithValue("@IDPrijemnice", txtPrijemnica.Text == "" ? 0 : Convert.ToInt32(txtPrijemnica.Text));
                da.Fill(dt);
            }
            dt.Columns.Add("Prikaz", typeof(string), "Convert(RB, 'System.String') + ' – ' + Naimenovanje");
            return dt;
        }

        private void OtvoriUparivanje()
        {
            if (txtID.Text == "")
                return;

            try
            {
                UcitajStatusPrijemnice();
                dtNaimenovanja = UcitajNaimenovanja();

                cboDodeliNaimenovanje.DataSource = dtNaimenovanja.Copy();
                cboDodeliNaimenovanje.DisplayMember = "Prikaz";
                cboDodeliNaimenovanje.ValueMember = "ID";

                var kolNaim = (DataGridViewComboBoxColumn)dgvUparivanje.Columns["Naimenovanje"];
                kolNaim.DataSource = dtNaimenovanja.Copy();

                bool zakljucano = StavkeZakljucane();
                dgvUparivanje.ReadOnly = zakljucano;
                btnDodeliNaimenovanje.Enabled = !zakljucano && dtNaimenovanja.Rows.Count > 0;

                PopuniUparivanje();
                pnlUparivanje.Visible = true;
                pnlUparivanje.BringToFront();
                OsveziKontrolu();

                lblUparivanje.Text = zakljucano
                    ? "Prijemnica je potvrđena (ZA): stavke menja samo korisnik sa pravom korekcije (Podešavanja: Carina.Prijemnica.KorekcijaKorisnici)."
                    : dtNaimenovanja.Rows.Count == 0
                        ? "Magacinski predmet nema naimenovanja (carinske stavke); menjaju se samo koleta i težine."
                        : "Koleta je broj koleta koji ulazi na skladište. Izmene se trajno upisuju na Snimi.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri otvaranju uparivanja: " + ex.Message);
            }
        }

        private void PopuniUparivanje()
        {
            popunjavanjeUparivanja = true;
            try
            {
                dgvUparivanje.Rows.Clear();
                foreach (var s in list.OrderBy(x => x.RB))
                {
                    int i = dgvUparivanje.Rows.Add();
                    var red = dgvUparivanje.Rows[i];
                    red.Tag = s;
                    PopuniRedUparivanja(red);
                }
            }
            finally
            {
                popunjavanjeUparivanja = false;
            }
        }

        private void PopuniRedUparivanja(DataGridViewRow red)
        {
            var s = (PrijemnicaStavke)red.Tag;
            bool staro = popunjavanjeUparivanja;
            popunjavanjeUparivanja = true;
            try
            {
                red.Cells["RB"].Value = s.RB;
                red.Cells["Artikal"].Value = s.Naziv;
                red.Cells["JM"].Value = s.JM;
                red.Cells["KoletaPL"].Value = DbVrednost(s.KoletaPL);
                red.Cells["Koleta"].Value = s.Koleta;
                red.Cells["KolicinaJM"].Value = DbVrednost(s.KolicinaJM);
                red.Cells["Neto"].Value = s.Neto;
                red.Cells["Bruto"].Value = s.Bruto;
                red.Cells["Vrednost"].Value = s.Vrednost;
                red.Cells["Valuta"].Value = s.Valuta;
                red.Cells["Naimenovanje"].Value = s.IDNaimenovanja.HasValue && dtNaimenovanja.Select("ID = " + s.IDNaimenovanja.Value).Length > 0
                    ? (object)s.IDNaimenovanja.Value : DBNull.Value;
                red.DefaultCellStyle.BackColor = s.IDNaimenovanja.HasValue || dtNaimenovanja.Rows.Count == 0 ? Color.Empty : Color.MistyRose;
            }
            finally
            {
                popunjavanjeUparivanja = staro;
            }
        }

        private void DgvUparivanje_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (popunjavanjeUparivanja || e.RowIndex < 0)
                return;

            var red = dgvUparivanje.Rows[e.RowIndex];
            var s = (PrijemnicaStavke)red.Tag;
            string kolona = dgvUparivanje.Columns[e.ColumnIndex].Name;
            object v = red.Cells[e.ColumnIndex].Value;
            decimal broj = v == null || v == DBNull.Value ? 0 : Convert.ToDecimal(v);

            switch (kolona)
            {
                case "Koleta":
                    if (broj < 0 || broj != Math.Truncate(broj))
                    {
                        MessageBox.Show("Koleta je ceo broj veći ili jednak nuli.");
                        break;
                    }
                    PromeniKoleta(s, broj);
                    break;
                case "Neto": s.Neto = broj; break;
                case "Bruto": s.Bruto = broj; break;
                case "Vrednost": s.Vrednost = broj; break;
                case "Naimenovanje":
                    DodeliNaimenovanje(s, v == null || v == DBNull.Value ? null : dtNaimenovanja.Select("ID = " + Convert.ToInt32(v)).FirstOrDefault());
                    break;
            }

            BeginInvoke(new Action(() => { PopuniRedUparivanja(red); OsveziKontrolu(); }));
        }

        // 4.4: koleta za skladište; neto, bruto i vrednost se preračunavaju srazmerno packing listi (OP-20)
        private void PromeniKoleta(PrijemnicaStavke s, decimal koleta)
        {
            s.Koleta = koleta;
            if (s.Koeficijent.HasValue)
                s.KolicinaJM = Math.Round(koleta * s.Koeficijent.Value, 3);

            if (s.KoletaPL.HasValue && s.KoletaPL.Value > 0)
            {
                decimal udeo = koleta / s.KoletaPL.Value;
                if (s.NetoPL.HasValue) s.Neto = Math.Round(s.NetoPL.Value * udeo, 3);
                if (s.BrutoPL.HasValue) s.Bruto = Math.Round(s.BrutoPL.Value * udeo, 3);
                if (s.VrednostPL.HasValue) s.Vrednost = Math.Round(s.VrednostPL.Value * udeo, 2);
            }
        }

        private void DodeliIzabranim()
        {
            if (cboDodeliNaimenovanje.SelectedValue == null)
                return;

            var naim = dtNaimenovanja.Select("ID = " + Convert.ToInt32(cboDodeliNaimenovanje.SelectedValue)).FirstOrDefault();
            var redovi = dgvUparivanje.SelectedRows.Cast<DataGridViewRow>()
                .Concat(dgvUparivanje.SelectedCells.Cast<DataGridViewCell>().Select(c => c.OwningRow))
                .Distinct().ToList();
            if (redovi.Count == 0)
            {
                MessageBox.Show("Izaberite redove u tabeli.");
                return;
            }

            foreach (var red in redovi)
            {
                DodeliNaimenovanje((PrijemnicaStavke)red.Tag, naim);
                PopuniRedUparivanja(red);
            }
            OsveziKontrolu();
        }

        private class KontrolaRed
        {
            public string Naimenovanje, Mera, Status;
            public decimal Carina, Ranije, Ova;
            public decimal Preostalo { get { return Carina - Ranije - Ova; } }
        }

        // 4.5 / CAR-P05: carina, primljeno na drugim prijemnicama predmeta, ova prijemnica (iz memorije)
        private List<KontrolaRed> IzracunajKontrolu(DataTable naimenovanja)
        {
            decimal tolerancija;
            decimal.TryParse(Administracija.Podesavanja.Get("Carina.Prijem.TolerancijaProcenat", "0").Replace(',', '.'),
                NumberStyles.Number, CultureInfo.InvariantCulture, out tolerancija);
            // zbir redova zaokruženih na 3 (kg) i 2 (vrednost) decimale može malo da odstupi od carine
            decimal apsolutna;
            if (!decimal.TryParse(Administracija.Podesavanja.Get("Carina.Prijem.TolerancijaApsolutna", "0.05").Replace(',', '.'),
                    NumberStyles.Number, CultureInfo.InvariantCulture, out apsolutna))
                apsolutna = 0.05m;

            var rezultat = new List<KontrolaRed>();
            foreach (DataRow n in naimenovanja.Rows)
            {
                int id = Convert.ToInt32(n["ID"]);
                var stavke = list.Where(s => s.IDNaimenovanja == id).ToList();
                string naziv = Convert.ToString(n["Prikaz"]);
                var mere = new[]
                {
                    new KontrolaRed { Mera = "Koleta", Carina = Convert.ToDecimal(n["CarinaKoleta"]), Ranije = Convert.ToDecimal(n["RanijeKoleta"]), Ova = stavke.Sum(s => s.Koleta) },
                    new KontrolaRed { Mera = "Neto kg", Carina = Convert.ToDecimal(n["CarinaNeto"]), Ranije = Convert.ToDecimal(n["RanijeNeto"]), Ova = stavke.Sum(s => s.Neto) },
                    new KontrolaRed { Mera = "Bruto kg", Carina = Convert.ToDecimal(n["CarinaBruto"]), Ranije = Convert.ToDecimal(n["RanijeBruto"]), Ova = stavke.Sum(s => s.Bruto) },
                    new KontrolaRed { Mera = "Vrednost", Carina = Convert.ToDecimal(n["CarinaVrednost"]), Ranije = Convert.ToDecimal(n["RanijeVrednost"]), Ova = stavke.Sum(s => s.Vrednost) }
                };
                foreach (var m in mere)
                {
                    m.Naimenovanje = naziv;
                    decimal dozvoljeno = Math.Max(Math.Abs(m.Carina) * tolerancija / 100m, m.Mera == "Koleta" ? 0m : apsolutna);
                    decimal ukupno = m.Ranije + m.Ova;
                    m.Status = ukupno > m.Carina + dozvoljeno ? "Prekoračeno"
                             : ukupno >= m.Carina - dozvoljeno ? "Slaže se"
                             : "Delimično";
                    rezultat.Add(m);
                }
            }
            return rezultat;
        }

        private void OsveziKontrolu()
        {
            dgvKontrola.Rows.Clear();
            if (dtNaimenovanja == null)
                return;

            foreach (var k in IzracunajKontrolu(dtNaimenovanja))
            {
                int i = dgvKontrola.Rows.Add(k.Naimenovanje, k.Mera, k.Carina, k.Ranije, k.Ova, k.Preostalo, k.Status);
                var celija = dgvKontrola.Rows[i].Cells["Status"];
                celija.Style.BackColor = k.Status == "Prekoračeno" ? Color.MistyRose : k.Status == "Slaže se" ? Color.Honeydew : Color.LemonChiffon;
            }

            int bez = list.Count(s => !s.IDNaimenovanja.HasValue);
            if (dtNaimenovanja.Rows.Count > 0 && bez > 0 && !StavkeZakljucane())
                lblUparivanje.Text = "Stavki bez naimenovanja: " + bez + ". Izaberite redove i naimenovanje, pa Dodeli izabranim redovima.";
        }

        #endregion

        #region Snimanje stavki (CAR-P04, P05, P07, P08, P09)

        // Provera pre Snimi; false = ne snima se ništa
        private bool ProveriStavkePreSnimanja()
        {
            bool promenjeno = PotpisStavki() != stavkeSnimak;
            if (StavkeZakljucane() && promenjeno)
            {
                MessageBox.Show("Prijemnica je potvrđena (ZA). Stavke, koleta i uparivanje menja samo korisnik sa pravom korekcije " +
                                "(Podešavanja: Carina.Prijemnica.KorekcijaKorisnici). Ponovo otvorite prijemnicu da poništite izmene.");
                return false;
            }
            if (!promenjeno)
                return true;   // stavke se ne menjaju, snima se samo zaglavlje

            var naimenovanja = UcitajNaimenovanja();
            if (naimenovanja.Rows.Count > 0)
            {
                var bez = list.Where(s => !s.IDNaimenovanja.HasValue).Select(s => s.RB.ToString()).ToList();
                if (bez.Count > 0)
                {
                    MessageBox.Show("Stavke bez naimenovanja (RB " + string.Join(", ", bez) + ") ne mogu da se snime.\n" +
                                    "Uparite ih u Uparivanje i kontrola.");
                    return false;
                }

                var postojeca = new HashSet<int>(naimenovanja.Rows.Cast<DataRow>().Select(r => Convert.ToInt32(r["ID"])));
                foreach (var s in list.Where(x => x.IDNaimenovanja.HasValue))
                {
                    if (!postojeca.Contains(s.IDNaimenovanja.Value))
                    {
                        MessageBox.Show("Stavka RB " + s.RB + " je uparena sa naimenovanjem koje više ne postoji na magacinskom predmetu. Uparite je ponovo.");
                        return false;
                    }
                    DodeliNaimenovanje(s, naimenovanja.Select("ID = " + s.IDNaimenovanja.Value)[0], false);   // tekst Naimenovanje i NHM iz veze (CAR-P09)
                }
            }

            var necele = list.Where(s => s.IDPLStavke.HasValue && s.Koleta != Math.Truncate(s.Koleta)).Select(s => s.RB.ToString()).ToList();
            if (necele.Count > 0)
            {
                MessageBox.Show("Koleta mora biti ceo broj (RB " + string.Join(", ", necele) + ").");
                return false;
            }

            var prekoraceno = IzracunajKontrolu(naimenovanja).Where(k => k.Status == "Prekoračeno").ToList();
            if (prekoraceno.Count > 0)
            {
                MessageBox.Show("Primljeno je više nego što je prijavljeno carini, snimanje nije moguće:\n\n" +
                    string.Join("\n", prekoraceno.Select(k => k.Naimenovanje + ", " + k.Mera + ": carina " + k.Carina.ToString("N3")
                        + ", ukupno primljeno " + (k.Ranije + k.Ova).ToString("N3") + ", razlika " + (-k.Preostalo).ToString("N3"))));
                return false;
            }

            return true;
        }

        // Ažurira stavke po ID-u, dodaje nove, briše uklonjene; ID stavke (veza sa Promet) ostaje isti
        private bool SnimiStavkeBezBrisanja()
        {
            if (PotpisStavki() == stavkeSnimak)
                return true;

            int idPrijemnice = Convert.ToInt32(txtPrijemnica.Text);
            try
            {
                using (var conn = new SqlConnection(connection))
                {
                    conn.Open();
                    using (var tran = conn.BeginTransaction())
                    {
                        var uBazi = new List<int>();
                        using (var cmd = new SqlCommand("SELECT ID FROM RNCarinskoPrijemnicaStavke WHERE IDNadredjena = @P", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@P", idPrijemnice);
                            using (var dr = cmd.ExecuteReader())
                                while (dr.Read())
                                    uBazi.Add(dr.GetInt32(0));
                        }

                        var zadrzane = new HashSet<int>(list.Where(s => s.ID > 0).Select(s => s.ID));
                        foreach (int id in uBazi.Where(x => !zadrzane.Contains(x)))
                            SnimiStavku(conn, tran, idPrijemnice, new PrijemnicaStavke { ID = id }, true);

                        foreach (var s in list)
                            SnimiStavku(conn, tran, idPrijemnice, s, false);

                        using (var cmd = new SqlCommand(@"UPDATE RNCarinskoPackingLista SET Status = N'Preneto'
                            WHERE Status = N'Uvezeno' AND ID IN (SELECT pls.IDPackingListe FROM RNCarinskoPackingListaStavka pls
                                JOIN RNCarinskoPrijemnicaStavke s ON s.IDPLStavke = pls.ID WHERE s.IDNadredjena = @P)", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@P", idPrijemnice);
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                }
                stavkeSnimak = PotpisStavki();
                return true;
            }
            catch (SqlException ex)
            {
                VratiStavkePrijemnice(idPrijemnice);   // ID-jevi novih stavki nisu upisani: stavke se čitaju ponovo iz baze
                stavkeSnimak = PotpisStavki();
                MessageBox.Show("Stavke prijemnice nisu snimljene: " + ex.Message);
                return false;
            }
        }

        private void SnimiStavku(SqlConnection conn, SqlTransaction tran, int idPrijemnice, PrijemnicaStavke s, bool obrisi)
        {
            using (var cmd = new SqlCommand("dbo.sp_PrijemnicaStavka_Snimi", conn, tran) { CommandType = CommandType.StoredProcedure })
            {
                var id = cmd.Parameters.Add("@ID", SqlDbType.Int);
                id.Direction = ParameterDirection.InputOutput;
                id.Value = s.ID;
                cmd.Parameters.AddWithValue("@IDNadredjena", idPrijemnice);
                cmd.Parameters.AddWithValue("@Obrisi", obrisi);
                cmd.Parameters.AddWithValue("@Korisnik", (Korisnik ?? "").Trim());
                if (!obrisi)
                {
                    cmd.Parameters.AddWithValue("@RB", s.RB);
                    cmd.Parameters.Add(TarifniBroj.Parametar("@NHM", s.NHM, true));   // RAZ-02
                    cmd.Parameters.AddWithValue("@IDArtikla", s.IDArtikla);
                    cmd.Parameters.AddWithValue("@Naziv", s.Naziv ?? "");
                    cmd.Parameters.AddWithValue("@Naimenovanje", s.Naimenovanje ?? "");
                    cmd.Parameters.AddWithValue("@JM", s.JM ?? "");
                    cmd.Parameters.Add("@Koleta", SqlDbType.Decimal).Value = s.Koleta;
                    cmd.Parameters.Add("@Bruto", SqlDbType.Decimal).Value = s.Bruto;
                    cmd.Parameters.Add("@Neto", SqlDbType.Decimal).Value = s.Neto;
                    cmd.Parameters.Add("@Vrednost", SqlDbType.Decimal).Value = s.Vrednost;
                    cmd.Parameters.AddWithValue("@Valuta", s.Valuta ?? "");
                    cmd.Parameters.AddWithValue("@Pozicija", s.Pozicija ?? "");
                    cmd.Parameters.AddWithValue("@Paleta", s.Paleta);
                    cmd.Parameters.AddWithValue("@VrstaPaleta", s.VrstaPaleta);
                    cmd.Parameters.Add("@PDV", SqlDbType.Decimal).Value = s.PDV;
                    cmd.Parameters.Add("@Carina", SqlDbType.Decimal).Value = s.Carina;
                    cmd.Parameters.AddWithValue("@Napomena", s.Napomena ?? "");
                    cmd.Parameters.AddWithValue("@IDNaimenovanja", DbVrednost(s.IDNaimenovanja));
                    cmd.Parameters.Add("@KoletaPL", SqlDbType.Decimal).Value = DbVrednost(s.KoletaPL);
                    cmd.Parameters.Add("@KolicinaJM", SqlDbType.Decimal).Value = DbVrednost(s.KolicinaJM);
                    cmd.Parameters.AddWithValue("@IDPLStavke", DbVrednost(s.IDPLStavke));
                }
                cmd.ExecuteNonQuery();
                if (!obrisi)
                    s.ID = Convert.ToInt32(id.Value);
            }
        }

        #endregion
    }
}
