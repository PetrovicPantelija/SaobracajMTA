using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main.Dokumenta
{
    // Katalog artikala (tabela ArtikliKatalog: DEUS, OSTALI, ...): izbor jednog kataloga i
    // pregled klasifikacije (nivo 1 -> 4) sa artiklima izabranog kataloga.
    internal static class ArtikliKatalogDijalozi
    {
        public static List<string> Katalozi(string connection)
        {
            var lista = new List<string>();
            using (var conn = new SqlConnection(connection))
            using (var cmd = new SqlCommand("SELECT Katalog FROM dbo.ArtikliKatalog WHERE Aktivan = 1 ORDER BY Katalog", conn))
            {
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                    while (dr.Read())
                        lista.Add(dr.GetString(0));
            }
            return lista;
        }

        // Lista za štikliranje u kojoj može biti izabrana samo jedna vrednost; null = korisnik je odustao
        public static string IzaberiKatalog(IWin32Window vlasnik, string connection, string naslov, string predlog)
        {
            var katalozi = Katalozi(connection);
            if (katalozi.Count == 0)
            {
                MessageBox.Show("Nema aktivnih kataloga artikala (tabela ArtikliKatalog).");
                return null;
            }

            using (var frm = new Form
            {
                Text = naslov,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MinimizeBox = false,
                MaximizeBox = false,
                ShowInTaskbar = false,
                ClientSize = new Size(340, 260)
            })
            {
                var lbl = new Label { Text = "Izaberite katalog artikala", Location = new Point(12, 12), AutoSize = true };
                var lista = new CheckedListBox { Location = new Point(12, 36), Size = new Size(316, 170), CheckOnClick = true };
                foreach (string k in katalozi)
                    lista.Items.Add(k, string.Equals(k, predlog, StringComparison.OrdinalIgnoreCase));

                var btnIzaberi = new Button { Text = "Izaberi", Location = new Point(152, 218), Size = new Size(85, 30), DialogResult = DialogResult.OK };
                var btnOtkazi = new Button { Text = "Otkaži", Location = new Point(243, 218), Size = new Size(85, 30), DialogResult = DialogResult.Cancel };
                btnIzaberi.Enabled = lista.CheckedItems.Count == 1;

                // samo jedna vrednost: štikliranje jedne skida ostale
                lista.ItemCheck += (s, e) =>
                {
                    if (e.NewValue == CheckState.Checked)
                        for (int i = 0; i < lista.Items.Count; i++)
                            if (i != e.Index && lista.GetItemChecked(i))
                                lista.SetItemChecked(i, false);
                    lista.BeginInvoke(new Action(() => btnIzaberi.Enabled = lista.CheckedItems.Count == 1));
                };

                frm.Controls.AddRange(new Control[] { lbl, lista, btnIzaberi, btnOtkazi });
                frm.AcceptButton = btnIzaberi;
                frm.CancelButton = btnOtkazi;

                return frm.ShowDialog(vlasnik) == DialogResult.OK && lista.CheckedItems.Count == 1
                    ? lista.CheckedItems[0].ToString()
                    : null;
            }
        }

        // Stablo nivo 1 -> 2 -> 3 -> 4 -> artikli izabranog kataloga.
        // izborNivoa4 != null: dvoklik na četvrti nivo vraća njegov ID i zatvara prozor.
        public static void PrikaziKlasifikaciju(IWin32Window vlasnik, string connection, string katalog, Action<int> izborNivoa4)
        {
            var katalozi = Katalozi(connection);

            using (var frm = new Form
            {
                Text = "Klasifikacija artikala",
                StartPosition = FormStartPosition.CenterParent,
                ShowInTaskbar = false,
                MinimizeBox = false,
                ClientSize = new Size(760, 620)
            })
            {
                // širina odmah kao prozor: dugme vezano za desnu ivicu inače ostaje van vidljivog dela
                var gore = new Panel { Width = frm.ClientSize.Width, Height = 44, Dock = DockStyle.Top };
                var lblKatalog = new Label { Text = "Katalog", Location = new Point(12, 14), AutoSize = true };
                var cboKatalog = new ComboBox { Location = new Point(70, 10), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
                var lblBroj = new Label { Location = new Point(290, 14), AutoSize = true };
                var btnUvoz = new Button
                {
                    Text = "Uvoz artikala",
                    Location = new Point(frm.ClientSize.Width - 152, 8),
                    Size = new Size(140, 28),
                    Anchor = AnchorStyles.Top | AnchorStyles.Right,
                    UseVisualStyleBackColor = true
                };
                gore.Controls.AddRange(new Control[] { lblKatalog, cboKatalog, lblBroj, btnUvoz });

                var dole = new Label
                {
                    Dock = DockStyle.Bottom,
                    Height = 28,
                    Padding = new Padding(12, 6, 0, 0),
                    Text = izborNivoa4 != null ? "Dvoklik na četvrti nivo bira ga kao klasifikaciju novih artikala." : ""
                };

                var stablo = new TreeView { Dock = DockStyle.Fill, HideSelection = false };
                frm.Controls.Add(stablo);
                frm.Controls.Add(gore);
                frm.Controls.Add(dole);

                cboKatalog.SelectedIndexChanged += (s, e) =>
                {
                    try
                    {
                        PopuniStablo(stablo, lblBroj, connection, Convert.ToString(cboKatalog.SelectedItem));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Greška pri učitavanju klasifikacije: " + ex.Message);
                    }
                };

                // Uvoz artikala izabranog kataloga: nivoi I–IV, šifra, artikal, JM -> klasifikacija artikala
                btnUvoz.Click += (s, e) =>
                {
                    string izabrani = Convert.ToString(cboKatalog.SelectedItem);
                    if (izabrani == "")
                        return;
                    if (UveziArtikle(frm, connection, izabrani))
                    {
                        try
                        {
                            PopuniStablo(stablo, lblBroj, connection, izabrani);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Greška pri učitavanju klasifikacije: " + ex.Message);
                        }
                    }
                };

                stablo.NodeMouseDoubleClick += (s, e) =>
                {
                    var oznaka = e.Node.Tag as int[];
                    if (izborNivoa4 != null && oznaka != null && oznaka[0] == 4)
                    {
                        izborNivoa4(oznaka[1]);
                        frm.Close();
                    }
                };

                foreach (string k in katalozi)
                    cboKatalog.Items.Add(k);
                int indeks = katalozi.FindIndex(k => string.Equals(k, katalog, StringComparison.OrdinalIgnoreCase));
                if (cboKatalog.Items.Count > 0)
                    cboKatalog.SelectedIndex = indeks >= 0 ? indeks : 0;

                frm.ShowDialog(vlasnik);
            }
        }

        private static void PopuniStablo(TreeView stablo, Label lblBroj, string connection, string katalog)
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(connection))
            using (var da = new SqlDataAdapter(@"SELECT
                    p.ID AS ID1, RTRIM(CONVERT(nvarchar(200), p.Naziv)) AS N1, RTRIM(CONVERT(nvarchar(100), p.Sifra)) AS S1,
                    d.ID AS ID2, RTRIM(CONVERT(nvarchar(200), d.Naziv)) AS N2, RTRIM(CONVERT(nvarchar(100), d.Sifra)) AS S2,
                    t.ID AS ID3, RTRIM(CONVERT(nvarchar(200), t.Naziv)) AS N3, RTRIM(CONVERT(nvarchar(100), t.Sifra)) AS S3,
                    c.ID AS ID4, RTRIM(CONVERT(nvarchar(200), c.Naziv)) AS N4, RTRIM(CONVERT(nvarchar(100), c.Sifra)) AS S4,
                    a.ID, RTRIM(CONVERT(nvarchar(100), a.Sifra)) AS Sifra, RTRIM(CONVERT(nvarchar(250), a.Artikal)) AS Artikal,
                    RTRIM(CONVERT(nvarchar(20), a.JM)) AS JM,
                    dbo.fn_PL_Koeficijent(CONVERT(nvarchar(100), a.KoeficijentKoletaJM)) AS Koeficijent
                FROM Artikli a
                INNER JOIN ArtikliCetvrtiNivo c ON a.IDNadredjena = c.ID
                INNER JOIN ArtikliTreciNivo t ON c.IDNadredjena = t.ID
                INNER JOIN ArtikliDrugiNivo d ON t.IDNadredjena = d.ID
                INNER JOIN ArtikliPrviNivo p ON d.IDNadredjena = p.ID
                WHERE a.Katalog = @K
                ORDER BY N1, N2, N3, N4, Artikal", conn))
            {
                da.SelectCommand.Parameters.AddWithValue("@K", katalog ?? "");
                da.Fill(dt);
            }

            stablo.BeginUpdate();
            try
            {
                stablo.Nodes.Clear();
                var cvorovi = new Dictionary<string, TreeNode>();
                int brojNivoa4 = 0;

                Func<TreeNodeCollection, int, object, object, object, TreeNode> cvor = (roditelj, nivo, id, naziv, sifra) =>
                {
                    string kljuc = nivo + ":" + id;
                    TreeNode n;
                    if (!cvorovi.TryGetValue(kljuc, out n))
                    {
                        n = roditelj.Add(TekstNivoa(Convert.ToString(naziv), Convert.ToString(sifra)));
                        n.Tag = new[] { nivo, Convert.ToInt32(id) };
                        cvorovi[kljuc] = n;
                        if (nivo == 4) brojNivoa4++;
                    }
                    return n;
                };

                foreach (DataRow r in dt.Rows)
                {
                    TreeNode n1 = cvor(stablo.Nodes, 1, r["ID1"], r["N1"], r["S1"]);
                    TreeNode n2 = cvor(n1.Nodes, 2, r["ID2"], r["N2"], r["S2"]);
                    TreeNode n3 = cvor(n2.Nodes, 3, r["ID3"], r["N3"], r["S3"]);
                    TreeNode n4 = cvor(n3.Nodes, 4, r["ID4"], r["N4"], r["S4"]);

                    string jm = Convert.ToString(r["JM"]);
                    string koef = r["Koeficijent"] == DBNull.Value ? "" : ", " + Convert.ToDecimal(r["Koeficijent"]).ToString("0.###") + " " + jm + " po koletu";
                    var artikal = n4.Nodes.Add(Convert.ToString(r["Sifra"]) + " – " + Convert.ToString(r["Artikal"]) + "   [" + jm + koef + "]");
                    artikal.Tag = new[] { 5, Convert.ToInt32(r["ID"]) };
                    artikal.ForeColor = Color.FromArgb(0x4C, 0x5D, 0x6E);
                }

                foreach (TreeNode n in stablo.Nodes)
                    n.Expand();

                lblBroj.Text = dt.Rows.Count == 0
                    ? "Katalog nema artikala."
                    : "Artikala: " + dt.Rows.Count + ", grupa četvrtog nivoa: " + brojNivoa4;
            }
            finally
            {
                stablo.EndUpdate();
            }
        }

        #region Uvoz artikala kataloga

        private class RedArtikla
        {
            public int Red;
            public string Sifra, Artikal, JM;
            public decimal? Koeficijent;
            public string[] NivoSifra = new string[5], NivoNaziv = new string[5];   // indeksi 1..4
        }

        // Fajl kao DEUS klasifikacija: I–IV NIVO (naziv u susednoj koloni bez zaglavlja), ŠIFRA ARTIKLA, ARTIKAL, JM.
        // true = nešto je upisano (stablo treba osvežiti)
        private static bool UveziArtikle(IWin32Window vlasnik, string connection, string katalog)
        {
            string putanja;
            using (var ofd = new OpenFileDialog { Title = "Uvoz artikala u katalog " + katalog, Filter = "Excel fajlovi (*.xlsx;*.xls)|*.xlsx;*.xls" })
            {
                if (ofd.ShowDialog(vlasnik) != DialogResult.OK)
                    return false;
                putanja = ofd.FileName;
            }

            try
            {
                string greska;
                var redovi = CitajFajlArtikala(putanja, out greska);
                if (greska != null)
                {
                    MessageBox.Show(greska);
                    return false;
                }
                if (redovi.Count == 0)
                {
                    MessageBox.Show("U fajlu nema redova sa artiklima ispod zaglavlja.");
                    return false;
                }

                if (MessageBox.Show("Uvoz " + redovi.Count + " artikala u katalog " + katalog + ".\n\n" +
                        "Nedostajući nivoi klasifikacije i novi artikli se prave; postojećim artiklima kataloga (ista šifra) " +
                        "dodeljuje se klasifikacija iz fajla i ažuriraju naziv i JM. Nastaviti?",
                        "Uvoz artikala", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return false;

                Guid uvoz = Guid.NewGuid();
                int novih, izmenjenih, nivoa;
                using (var conn = new SqlConnection(connection))
                {
                    conn.Open();
                    using (var tran = conn.BeginTransaction())
                    using (var cmd = new SqlCommand(@"INSERT INTO ArtikliKatalogUvoz
                        (Uvoz, RedUFajlu, Nivo1Sifra, Nivo1Naziv, Nivo2Sifra, Nivo2Naziv, Nivo3Sifra, Nivo3Naziv, Nivo4Sifra, Nivo4Naziv, Sifra, Artikal, JM, Koeficijent)
                        VALUES (@U, @Red, @S1, @N1, @S2, @N2, @S3, @N3, @S4, @N4, @Sifra, @Artikal, @JM, @Koef)", conn, tran))
                    {
                        foreach (var r in redovi)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@U", uvoz);
                            cmd.Parameters.AddWithValue("@Red", r.Red);
                            for (int n = 1; n <= 4; n++)
                            {
                                cmd.Parameters.AddWithValue("@S" + n, Vrednost(r.NivoSifra[n], 50));
                                cmd.Parameters.AddWithValue("@N" + n, Vrednost(r.NivoNaziv[n], 200));
                            }
                            cmd.Parameters.AddWithValue("@Sifra", Vrednost(r.Sifra, 50));
                            cmd.Parameters.AddWithValue("@Artikal", Vrednost(r.Artikal, 250));
                            cmd.Parameters.AddWithValue("@JM", Vrednost(r.JM, 20));
                            cmd.Parameters.AddWithValue("@Koef", r.Koeficijent.HasValue ? (object)r.Koeficijent.Value : DBNull.Value);
                            cmd.ExecuteNonQuery();
                        }
                        tran.Commit();
                    }

                    using (var cmd = new SqlCommand("dbo.sp_ArtikliKatalog_Uvezi", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.AddWithValue("@Uvoz", uvoz);
                        cmd.Parameters.AddWithValue("@Katalog", katalog);
                        var pNovih = cmd.Parameters.Add("@Novih", SqlDbType.Int);
                        var pIzmenjenih = cmd.Parameters.Add("@Izmenjenih", SqlDbType.Int);
                        var pNivoa = cmd.Parameters.Add("@Nivoa", SqlDbType.Int);
                        pNovih.Direction = pIzmenjenih.Direction = pNivoa.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        novih = Convert.ToInt32(pNovih.Value);
                        izmenjenih = Convert.ToInt32(pIzmenjenih.Value);
                        nivoa = Convert.ToInt32(pNivoa.Value);
                    }
                }

                MessageBox.Show("Katalog " + katalog + ": novih artikala " + novih + ", postojećih sa dodeljenom klasifikacijom " + izmenjenih
                    + ", novih nivoa klasifikacije " + nivoa + ".");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Uvoz artikala nije uspeo: " + ex.Message);
                return false;
            }
        }

        private static object Vrednost(string s, int duzina)
        {
            if (string.IsNullOrEmpty(s))
                return DBNull.Value;
            return s.Length > duzina ? s.Substring(0, duzina) : s;
        }

        private static List<RedArtikla> CitajFajlArtikala(string putanja, out string greska)
        {
            greska = null;
            var redovi = new List<RedArtikla>();
            string[] polja = { "Sifra", "Artikal", "JM", "Koeficijent", "Nivo1Sifra", "Nivo1Naziv", "Nivo2Sifra", "Nivo2Naziv",
                               "Nivo3Sifra", "Nivo3Naziv", "Nivo4Sifra", "Nivo4Naziv" };

            // nazivi zaglavlja: isti kao za packing listu (ugrađeni + Podešavanja Carina.PL.Kolona.*)
            var nazivi = new Dictionary<string, HashSet<string>>();
            foreach (string p in polja)
                nazivi[p] = new HashSet<string>((Prijemnica.PLPodrazumevaneKolone[p] + ";" + Administracija.Podesavanja.Get("Carina.PL.Kolona." + p, ""))
                    .Split(';').Select(Prijemnica.Normalizuj).Where(x => x != ""));

            byte[] sadrzaj;
            using (var fs = new FileStream(putanja, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var ms = new MemoryStream())
            {
                fs.CopyTo(ms);
                sadrzaj = ms.ToArray();
            }

            using (var engine = new ExcelEngine())
            using (var ms = new MemoryStream(sadrzaj))
            {
                IWorkbook knjiga = engine.Excel.Workbooks.Open(ms);
                try
                {
                    IWorksheet list = knjiga.Worksheets[0];
                    IRange opseg = list.UsedRange;
                    int poslednjiRed = opseg.LastRow, poslednjaKolona = opseg.LastColumn;

                    // red zaglavlja: prvi u kome su šifra i naziv artikla (iznad može biti naslov "KLASIFIKACIJA")
                    int redZaglavlja = 0;
                    Dictionary<string, int> kolone = null;
                    for (int r = opseg.Row; r <= Math.Min(poslednjiRed, opseg.Row + 29) && kolone == null; r++)
                    {
                        var mapa = new Dictionary<string, int>();
                        for (int c = 1; c <= poslednjaKolona; c++)
                        {
                            string tekst = Prijemnica.Normalizuj(list[r, c].DisplayText);
                            if (tekst == "") continue;
                            foreach (string p in polja)
                                if (!mapa.ContainsKey(p) && nazivi[p].Contains(tekst))
                                {
                                    mapa[p] = c;
                                    break;
                                }
                        }
                        if (mapa.ContainsKey("Sifra") && mapa.ContainsKey("Artikal"))
                        {
                            redZaglavlja = r;
                            kolone = mapa;
                        }
                    }

                    if (kolone == null)
                    {
                        greska = "U fajlu nije pronađeno zaglavlje sa kolonama ŠIFRA ARTIKLA i ARTIKAL.";
                        return redovi;
                    }

                    // „I NIVO“ … „IV NIVO“ je kolona šifre nivoa, naziv je u susednoj koloni bez zaglavlja
                    for (int n = 1; n <= 4; n++)
                    {
                        int kolSifra;
                        if (!kolone.TryGetValue("Nivo" + n + "Sifra", out kolSifra) || kolone.ContainsKey("Nivo" + n + "Naziv"))
                            continue;
                        int desno = kolSifra + 1;
                        if (desno <= poslednjaKolona && !kolone.ContainsValue(desno) && Prijemnica.Normalizuj(list[redZaglavlja, desno].DisplayText) == "")
                            kolone["Nivo" + n + "Naziv"] = desno;
                    }

                    if (!Enumerable.Range(1, 4).All(n => kolone.ContainsKey("Nivo" + n + "Sifra") || kolone.ContainsKey("Nivo" + n + "Naziv")))
                    {
                        greska = "U fajlu nisu pronađene kolone klasifikacije I NIVO, II NIVO, III NIVO i IV NIVO.";
                        return redovi;
                    }

                    Func<int, string, string> tekstPolja = (r, polje) =>
                        kolone.ContainsKey(polje) ? (list[r, kolone[polje]].DisplayText ?? "").Trim() : "";

                    for (int r = redZaglavlja + 1; r <= poslednjiRed; r++)
                    {
                        var red = new RedArtikla
                        {
                            Red = r,
                            Sifra = tekstPolja(r, "Sifra"),
                            Artikal = tekstPolja(r, "Artikal"),
                            JM = tekstPolja(r, "JM")
                        };
                        if (red.Sifra == "" && red.Artikal == "")
                            continue;

                        for (int n = 1; n <= 4; n++)
                        {
                            red.NivoSifra[n] = tekstPolja(r, "Nivo" + n + "Sifra");
                            red.NivoNaziv[n] = tekstPolja(r, "Nivo" + n + "Naziv");
                        }

                        if (kolone.ContainsKey("Koeficijent"))
                        {
                            decimal? koef = Prijemnica.BrojIzCelije(list[r, kolone["Koeficijent"]]);
                            red.Koeficijent = koef.HasValue && koef.Value > 0 ? koef : null;
                        }

                        redovi.Add(red);
                    }
                }
                finally
                {
                    knjiga.Close();
                }
            }
            return redovi;
        }

        #endregion

        private static string TekstNivoa(string naziv, string sifra)
        {
            if (string.IsNullOrEmpty(sifra) || string.Equals(sifra, naziv, StringComparison.OrdinalIgnoreCase))
                return naziv;
            return naziv + " (" + sifra + ")";
        }
    }
}
