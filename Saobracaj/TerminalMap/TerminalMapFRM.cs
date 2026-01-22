using Saobracaj.TrackModal.Sifarnici;
using Saobracaj.Uvoz;
using Syncfusion.Windows.Forms.Diagram;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Saobracaj.TerminalMap
{
    public partial class TerminalMapFRM : Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

        private Bitmap _full;
        private readonly List<HangarRegion> _regions = new List<HangarRegion>();   // pravougaonici iz DB
        private string ID;
        private bool _isLoadingStatus = false;

        // === OVERLAY JSON (settings + lines u istom fajlu) ===
        private string _overlayPath;
        private TerminalOverlayFile _overlay;

        public TerminalMapFRM()
        {
            InitializeComponent();

            panel1.Visible = false;

            btnEditMode.Click += BtnEditMode_Click;
            btnSaveRegions.Click += BtnSaveRegions_Click;
            btnResetViews.Click += BtnResetViews_Click;

            Load += TerminalMapFRM_Load;
        }

        private void TerminalMapFRM_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += TerminalMapFRM_KeyDown;

            // Slika
            var imgPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TerminalSlika.png");
            if (!File.Exists(imgPath))
            {
                MessageBox.Show("Ne mogu da nađem sliku:\n" + imgPath);
                return;
            }

            _full = new Bitmap(imgPath);

            // Pravougaonici iz baze
            _regions.Clear();
            _regions.AddRange(LoadRegionsFromDb());

            // Overlay JSON (linije + settings)
            _overlayPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "terminal_overlay.json"); // ovo je TAJ isti fajl (za linije)
            _overlay = RegionStorage.LoadOverlay(_overlayPath);

            ApplyRectColorsFromOverlay();

            SetupMap(map1, new PointF(0, 0));

            // primeni settings na mapu + meni
            map1.ShowBaseImage = _overlay.Settings.ShowBaseImage;
            ApplyPozadinaUi();

            // ucitaj linije iz overlay JSON i dodaj u mapu (lokalno)
            var lines = _overlay.Lines
                .Select(dto => RegionStorage.ToHangarRegion(dto))
                .ToList();

            foreach (var ln in lines)
                map1.Regions.Add(ln);

            lblHangar.Text = "Polje: -";
            // position panel2 to the right of dgvStock inside panel1 and keep it when panel1 resizes
            void RepositionPanel2()
            {
                try
                {
                    if (dgvStock == null || panel2 == null || panel1 == null) return;

                    int margin = 8;
                    // dgvStock.Location is relative to panel1
                    int newX = dgvStock.Location.X + dgvStock.Width + margin;
                    int newY = dgvStock.Location.Y;

                    // Ensure panel2 stays within panel1 bounds
                    if (newX + panel2.Width > panel1.ClientSize.Width)
                    {
                        // place it at right edge minus margin
                        newX = Math.Max(dgvStock.Location.X + dgvStock.Width + margin, panel1.ClientSize.Width - panel2.Width - margin);
                    }

                    if (newX < 0) newX = 0;
                    if (newY < 0) newY = 0;

                    panel2.Location = new Point(newX, newY);
                    panel2.BringToFront();
                }
                catch { }
            }

            RepositionPanel2();
            panel1.SizeChanged -= (s, ev) => RepositionPanel2();
            panel1.SizeChanged += (s, ev) => RepositionPanel2();

            this.Resize += (_, __) => map1.FitToView();
        }
        private void ApplyRectColorsFromOverlay()
        {
            if (_overlay?.RectColors == null) return;

            foreach (var r in _regions)
            {
                if (r == null) continue;
                if (r.Shape != ShapeType.Rect) continue;

                if (_overlay.RectColors.TryGetValue(r.ID, out int argb))
                    r.ColorArgb = argb;
            }
        }
        private void SetupMap(TerminalMapControl map, PointF offset)
        {
            map.MapImage = _full;
            map.ImageOffSet = offset;

            map.Regions.Clear();
            map.Regions.AddRange(_regions); // DB regioni

            map.StretchMode = MapStretchMode.Fill;
            map.FitToView();

            map.HangarClicked -= Map_HangarClicked;
            map.HangarClicked += Map_HangarClicked;

            map.RegionAdded -= Map_RegionAdded;
            map.RegionAdded += Map_RegionAdded;

            map.RegionDeleteRequested -= Map_RegionDeleteRequested;
            map.RegionDeleteRequested += Map_RegionDeleteRequested;

            map.RegionChanged -= Map_RegionChanged;
            map.RegionChanged += Map_RegionChanged;
        }

        // === POZADINA MENU ===
        private void ApplyPozadinaUi()
        {
            bool on = map1.ShowBaseImage;
            pozadinaONToolStripMenuItem.Text = on ? "Pozadina: ON" : "Pozadina: OFF";
            pozadinaONToolStripMenuItem.ForeColor = on ? Color.Lime : Color.Red;
        }
        private void Map_RegionChanged(object sender, HangarRegion reg)
        {
            if (reg == null) return;

            // 1) Linije -> update u overlay.Lines
            if (reg.Shape == ShapeType.Polyline)
            {
                var dto = _overlay.Lines.FirstOrDefault(x =>
                    x.ID.Equals(reg.ID, StringComparison.OrdinalIgnoreCase));

                if (dto != null)
                    dto.ColorArgb = reg.ColorArgb;

                RegionStorage.SaveOverlay(_overlayPath, _overlay);
                return;
            }

            // 2) Pravougaonici -> snimi boju po nazivu (SkNaziv)
            if (reg.Shape == ShapeType.Rect)
            {
                _overlay.RectColors[reg.ID] = reg.ColorArgb;
                RegionStorage.SaveOverlay(_overlayPath, _overlay);
                return;
            }
        }
        private void pozadinaONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // toggle
            map1.ShowBaseImage = !map1.ShowBaseImage;
            map1.Invalidate();

            // upisi u ISTI JSON (settings je "na vrhu")
            _overlay.Settings.ShowBaseImage = map1.ShowBaseImage;
            RegionStorage.SaveOverlay(_overlayPath, _overlay);

            // UI refresh
            ApplyPozadinaUi();
        }

        private void Map_HangarClicked(object sender, string hangarId)
        {
            lblHangar.Text = $"Polje: {hangarId}";
            ID = hangarId;

            FillGV(ID);
            LoadHangarStatus(hangarId);

            panel1.Visible = true;
        }

        private void Map_RegionAdded(object sender, HangarRegion reg)
        {
            if (reg == null) return;

            // 1) ako je linija -> samo lokalno u overlay JSON
            if (reg.Shape == ShapeType.Polyline)
            {
                _overlay.Lines.Add(RegionStorage.ToPolylineDto(reg));
                RegionStorage.SaveOverlay(_overlayPath, _overlay);
                map1.Invalidate();
                return;
            }

            // 2) ako je pravougaonik -> tvoja logika (validacija u sifarniku)
            var skNaziv = (reg.ID ?? "").Trim();

            if (!WarehouseExists(skNaziv))
            {
                map1.Regions.Remove(reg);
                _regions.RemoveAll(r => string.Equals(r.ID, skNaziv, StringComparison.OrdinalIgnoreCase));
                map1.Invalidate();

                var suggestions = GetSimilarWarehouseNames(skNaziv, 8);
                var sb = new StringBuilder();
                sb.AppendLine($"Skladište '{skNaziv}' nije uneto u šifarnik (kolona SkNaziv).");

                if (suggestions.Count > 0)
                {
                    sb.AppendLine();
                    sb.AppendLine("Da li ste mislili na nešto od ovoga?");
                    foreach (var s in suggestions)
                        sb.AppendLine(" • " + s);
                }

                MessageBox.Show(sb.ToString(), "Ne postoji skladište", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_regions.Any(x => string.Equals(x.ID, skNaziv, StringComparison.OrdinalIgnoreCase)))
                _regions.Add(reg);

            map1.Invalidate();
        }

        private void Map_RegionDeleteRequested(object sender, HangarRegion reg)
        {
            if (reg == null) return;

            // Ako je linija -> obriši lokalno iz overlay JSON
            if (reg.Shape == ShapeType.Polyline)
            {
                if (MessageBox.Show($"Obrisati liniju '{reg.ID}'?", "Brisanje",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;

                _overlay.Lines.RemoveAll(x => string.Equals(x.ID, reg.ID, StringComparison.OrdinalIgnoreCase));
                RegionStorage.SaveOverlay(_overlayPath, _overlay);

                map1.Regions.RemoveAll(r => r.Shape == ShapeType.Polyline &&
                                            string.Equals(r.ID, reg.ID, StringComparison.OrdinalIgnoreCase));
                map1.Invalidate();
                return;
            }

            // Pravougaonici -> kao do sada (brisanje koordinata iz DB)
            if (MessageBox.Show($"Obrisati region '{reg.ID}'?\n(Ovo će obrisati koordinate u tabeli Skladista)", "Brisanje",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            _regions.RemoveAll(r => string.Equals(r.ID, reg.ID, StringComparison.OrdinalIgnoreCase));
            map1.Regions.RemoveAll(r => r.Shape == ShapeType.Rect &&
                                        string.Equals(r.ID, reg.ID, StringComparison.OrdinalIgnoreCase));
            map1.Invalidate();

            try
            {
                ClearCoordinates(reg.ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri brisanju koordinata iz baze:\n" + ex.Message);
            }
        }

        private void BtnSaveRegions_Click(object sender, EventArgs e)
        {
            try
            {
                int saved = 0;

                foreach (var r in _regions)
                {
                    if (r == null) continue;

                    var skNaziv = (r.ID ?? "").Trim();
                    if (string.IsNullOrWhiteSpace(skNaziv)) continue;

                    if (!WarehouseExists(skNaziv))
                        continue;

                    UpsertCoordinates(skNaziv, r.RectImg);
                    saved++;
                }

                MessageBox.Show($"Sačuvano u tabelu Skladiste: {saved} regiona.", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri snimanju u bazu:\n" + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEditMode_Click(object sender, EventArgs e)
        {
            bool newMode = !map1.EditMode;
            map1.EditMode = newMode;

            btnEditMode.Text = newMode ? "Edit: ON" : "Edit: OFF";
            btnEditMode.ForeColor = newMode ? Color.Lime : Color.Red;
        }

        private void BtnResetViews_Click(object sender, EventArgs e)
        {
            map1.FitToView();
        }

        // ====== Tvoj DB deo (ne diram) ======

        private List<HangarRegion> LoadRegionsFromDb()
        {
            var list = new List<HangarRegion>();

            using (var conn = new SqlConnection(connection))
            using (var cmd = new SqlCommand(@"
                SELECT SkNaziv, X, Y, W, H
                FROM Skladista
                WHERE X IS NOT NULL AND Y IS NOT NULL AND W IS NOT NULL AND H IS NOT NULL
                  AND LTRIM(RTRIM(ISNULL(SkNaziv,''))) <> ''", conn))
            {
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        string sk = rd["SkNaziv"].ToString().Trim();

                        float x = Convert.ToSingle(rd["X"]);
                        float y = Convert.ToSingle(rd["Y"]);
                        float w = Convert.ToSingle(rd["W"]);
                        float h = Convert.ToSingle(rd["H"]);

                        list.Add(new HangarRegion
                        {
                            ID = sk,
                            RectImg = new RectangleF(x, y, w, h),
                            Shape = ShapeType.Rect
                        });
                    }
                }
            }

            return list;
        }

        private bool WarehouseExists(string skNaziv)
        {
            if (string.IsNullOrWhiteSpace(skNaziv)) return false;

            using (var conn = new SqlConnection(connection))
            using (var cmd = new SqlCommand(@"SELECT COUNT(1) FROM Skladista WHERE SkNaziv = @sk", conn))
            {
                cmd.Parameters.AddWithValue("@sk", skNaziv.Trim());
                conn.Open();
                int c = Convert.ToInt32(cmd.ExecuteScalar());
                return c > 0;
            }
        }

        private List<string> GetSimilarWarehouseNames(string input, int max = 8)
        {
            input = (input ?? "").Trim();
            var results = new List<string>();
            if (input.Length == 0) return results;

            using (var conn = new SqlConnection(connection))
            using (var cmd = new SqlCommand(@"
                SELECT TOP (@top) SkNaziv
                FROM Skladista
                WHERE SkNaziv LIKE @p1 OR SkNaziv LIKE @p2
                ORDER BY SkNaziv", conn))
            {
                cmd.Parameters.AddWithValue("@top", Math.Max(10, max * 2));
                cmd.Parameters.AddWithValue("@p1", "%" + input + "%");
                cmd.Parameters.AddWithValue("@p2", input + "%");
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        var s = rd[0].ToString().Trim();
                        if (!string.IsNullOrWhiteSpace(s))
                            results.Add(s);
                    }
                }
            }

            if (results.Count == 0)
            {
                var all = new List<string>();

                using (var conn = new SqlConnection(connection))
                using (var cmd = new SqlCommand(@"
                    SELECT TOP 200 SkNaziv
                    FROM Skladista
                    WHERE LTRIM(RTRIM(ISNULL(SkNaziv,''))) <> ''
                    ORDER BY SkNaziv", conn))
                {
                    conn.Open();
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            var s = rd[0].ToString().Trim();
                            if (!string.IsNullOrWhiteSpace(s))
                                all.Add(s);
                        }
                    }
                }

                results = all
                    .Select(s => new { Name = s, Dist = Levenshtein(input.ToUpperInvariant(), s.ToUpperInvariant()) })
                    .OrderBy(x => x.Dist)
                    .ThenBy(x => x.Name)
                    .Take(max)
                    .Select(x => x.Name)
                    .ToList();
            }

            return results.Distinct().Take(max).ToList();
        }

        private void UpsertCoordinates(string skNaziv, RectangleF rect)
        {
            using (var conn = new SqlConnection(connection))
            using (var cmd = new SqlCommand(@"
                UPDATE Skladista
                SET X = @x, Y = @y, W = @w, H = @h
                WHERE SkNaziv = @sk", conn))
            {
                cmd.Parameters.AddWithValue("@sk", skNaziv.Trim());
                cmd.Parameters.AddWithValue("@x", rect.X);
                cmd.Parameters.AddWithValue("@y", rect.Y);
                cmd.Parameters.AddWithValue("@w", rect.Width);
                cmd.Parameters.AddWithValue("@h", rect.Height);

                conn.Open();
                int affected = cmd.ExecuteNonQuery();

                if (affected == 0)
                    throw new Exception($"Nije pronađen red u Skladista za SkNaziv='{skNaziv}'.");
            }
        }

        private void ClearCoordinates(string skNaziv)
        {
            using (var conn = new SqlConnection(connection))
            using (var cmd = new SqlCommand(@"
                UPDATE Skladista
                SET X = NULL, Y = NULL, W = NULL, H = NULL
                WHERE SkNaziv = @sk", conn))
            {
                cmd.Parameters.AddWithValue("@sk", skNaziv.Trim());
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private static int Levenshtein(string a, string b)
        {
            if (string.IsNullOrEmpty(a)) return b?.Length ?? 0;
            if (string.IsNullOrEmpty(b)) return a.Length;

            int n = a.Length;
            int m = b.Length;
            var d = new int[n + 1, m + 1];

            for (int i = 0; i <= n; i++) d[i, 0] = i;
            for (int j = 0; j <= m; j++) d[0, j] = j;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (a[i - 1] == b[j - 1]) ? 0 : 1;

                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost
                    );
                }
            }

            return d[n, m];
        }

        private void FillGV(string hangarID)
        {
            var select =
                "Select Zone.Oznaka as Zona, SkladistaGrupa.Oznaka as Lokacija, Skladista.SkNaziv as Polje, Kontejner,  KontejnerStatus.Naziv As StatusKontejnera ,TipKontenjera.SkNaziv as Vrsta,  Pozicija.Opis as Pozicija, " +
" PArtnerji.PaNaziv as Brodar,  uvKvalitetKontejnera.Naziv as Kvalitet, KontejnerTekuce.Pokret as Pokret " +
" from KontejnerTekuce inner join Skladista on KontejnerTekuce.Skladiste = Skladista.ID " +
" inner join Pozicija on Pozicija.Id = KontejnerTekuce.Pozicija " +
" inner join TipKontenjera on TipKontenjera.ID = KontejnerTekuce.TipKontejnera " +
" inner join KontejnerStatus on KontejnerStatus.ID = StatusKontejnera " +
" inner join PArtnerji on KontejnerTekuce.Brodar = Partnerji.PaSifra " +
" inner join uvKvalitetKontejnera on KontejnerTekuce.Kvalitet = uvKvalitetKontejnera.ID " +
" inner join SkladistaGrupa on Skladista.GrupaPoljaID = SkladistaGrupa.ID " +
" inner join Zone on Zone.ID = SkladistaGrupa.ZonaID " +
" where Skladista.SkNaziv = '" + hangarID + "'";

            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dgvStock.ReadOnly = true;
            dgvStock.DataSource = ds.Tables[0];
        }

        private void FillGVDetaljno(string hangarID)
        {
            var select =
                "Select Zone.Oznaka as Zona, SkladistaGrupa.Oznaka as Lokacija, Skladista.SkNaziv as Polje, Kontejner,  KontejnerStatus.Naziv As StatusKontejnera , " +
" TipKontenjera.SkNaziv as Vrsta,  Pozicija.Opis as Pozicija, " +
" PArtnerji.PaNaziv as Brodar, 'NE ZNA SE ODAKLE I KOG POVUCI' as Nalogodavac, p1.PaNaziv as Uvoznik,  " +
"  'NE ZNA SE ODAKLE I KOG POVUCI' as Booking, 'NE ZNA SE ODAKLE I KOG POVUCI' as Klijent, KontejnerTekuce.Pokret as Pokret, 'Da li sa Tipa kontejnera' as Tara, 'Koji je ovo podatak' as MAXIMUM, " +
"  uvKvalitetKontejnera.Naziv as Kvalitet " +
"  from KontejnerTekuce " +
"  inner join Skladista on KontejnerTekuce.Skladiste = Skladista.ID " +
"  inner join Pozicija on Pozicija.Id = KontejnerTekuce.Pozicija " +
"  inner join TipKontenjera on TipKontenjera.ID = KontejnerTekuce.TipKontejnera " +
"  inner join KontejnerStatus on KontejnerStatus.ID = StatusKontejnera " +
" inner join PArtnerji on KontejnerTekuce.Brodar = Partnerji.PaSifra " +
" inner join PArtnerji p1 on KontejnerTekuce.Uvoznik = p1.PaSifra " +
" inner join uvKvalitetKontejnera on KontejnerTekuce.Kvalitet = uvKvalitetKontejnera.ID " +
" inner join SkladistaGrupa on Skladista.GrupaPoljaID = SkladistaGrupa.ID " +
" inner join Zone on Zone.ID = SkladistaGrupa.ZonaID " +
" where Skladista.SkNaziv =  '" + hangarID + "'";

            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dgvStock.ReadOnly = true;
            dgvStock.DataSource = ds.Tables[0];
        }

        private void LoadHangarStatus(string skNaziv)
        {
            if (string.IsNullOrWhiteSpace(skNaziv)) return;

            try
            {
                _isLoadingStatus = true;

                using (var conn = new SqlConnection(connection))
                using (var cmd = new SqlCommand(@"
                    SELECT Aktivan, Prazno, PuniSe
                    FROM Skladista
                    WHERE SkNaziv = @sk", conn))
                {
                    cmd.Parameters.AddWithValue("@sk", skNaziv.Trim());
                    conn.Open();

                    using (var rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            int aktivan = rd["Aktivan"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Aktivan"]);
                            int prazno = rd["Prazno"] == DBNull.Value ? 0 : Convert.ToInt32(rd["Prazno"]);
                            int puniSe = rd["PuniSe"] == DBNull.Value ? 0 : Convert.ToInt32(rd["PuniSe"]);

                            cbAktivan.Checked = aktivan == 1;
                            cbPrazno.Checked = prazno == 1;
                            cbPrazniSe.Checked = puniSe == 1;
                        }
                        else
                        {
                            cbAktivan.Checked = false;
                            cbPrazno.Checked = false;
                            cbPrazniSe.Checked = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri učitavanju statusa:\n" + ex.Message);
            }
            finally
            {
                _isLoadingStatus = false;
            }
        }

        private void TerminalMapFRM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && panel1.Visible)
            {
                panel1.Visible = false;
                lblHangar.Text = "Polje: -";
                ID = null;
                e.Handled = true;
            }
        }

        private void UpdateHangarFlag(string columnName, bool value)
        {
            if (_isLoadingStatus) return;
            if (string.IsNullOrWhiteSpace(ID)) return;
            if (!panel1.Visible) return;

            try
            {
                using (var conn = new SqlConnection(connection))
                using (var cmd = new SqlCommand($@"
                    UPDATE Skladista
                    SET {columnName} = @v
                    WHERE SkNaziv = @sk", conn))
                {
                    cmd.Parameters.AddWithValue("@v", value ? 1 : 0);
                    cmd.Parameters.AddWithValue("@sk", ID.Trim());
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri update-u ({columnName}):\n" + ex.Message);
                LoadHangarStatus(ID);
            }
        }

        private void cbAktivan_CheckStateChanged(object sender, EventArgs e) => UpdateHangarFlag("Aktivan", cbAktivan.Checked);
        private void cbPrazno_CheckStateChanged(object sender, EventArgs e) => UpdateHangarFlag("Prazno", cbPrazno.Checked);
        private void cbPrazniSe_CheckStateChanged(object sender, EventArgs e) => UpdateHangarFlag("PuniSe", cbPrazniSe.Checked);

        // ostavi prazno, ili skini event iz designer-a
        private void btnEditMode_Click(object sender, EventArgs e) { }
        private void btnSaveRegions_Click(object sender, EventArgs e) { }
        private void btnResetView_Click(object sender, EventArgs e) { }
        private void lblHangar_Click(object sender, EventArgs e) { }

        private void map1_Click(object sender, EventArgs e)
        {

        }

        private void sfButton2_Click(object sender, EventArgs e)
        {
          
            FillGVDetaljno(ID);
        }

        private void sfButton3_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmKontejnerTekuce")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Uvoz.frmKontejnerTekuce kt = new Uvoz.frmKontejnerTekuce(ID);
                kt.Show();
            }
        }
    }
}
