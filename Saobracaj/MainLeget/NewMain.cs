using Saobracaj.Drumski;
using Saobracaj.MainLeget;
using Saobracaj.MainLeget.LegNew;
using Saobracaj.MainLeget.LegNew.Podesavanjesistema;
using Saobracaj.Sifarnici;
using Saobracaj.TrackModal.Sifarnici;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj
{
    public partial class NewMain : Form
    {
        private readonly Stack<Form> nav = new Stack<Form>();
        private Form aktivna = null;
        private List<Control> homeCtrl;
        private Form menuHome = null;
        private int _savedLeftPanelWidth = 260;
        private readonly Dictionary<Control, Image> _originalButtonImages = new Dictionary<Control, Image>();
        private readonly Dictionary<Control, Font> _originalButtonFonts = new Dictionary<Control, Font>();

        string Korisnik;
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        private Dictionary<string, int> _mainMap = new Dictionary<string, int>();
        private int _currentMainId = 0;
        private Stack<int> _karticaStack = new Stack<int>();

        public NewMain(string korisnik)
        {
            InitializeComponent();
            ShowHome();
            Korisnik = korisnik.ToLower().TrimEnd();
        }

        private void MainLeget_Load(object sender, EventArgs e)
        {
            PravaMain();

            if (Korisnik != "test")
            {
                button1.Visible = false;
            }
        }
        private void PravaMain()
        {
            string query = @"
        SELECT DISTINCT MainNovi.ID AS id,
                        LOWER(RTRIM(MainNovi.Naziv)) AS Naziv
        FROM MainNovi
        INNER JOIN MainNoviPrava ON MainNovi.ID = MainNoviPrava.MainID
        WHERE Korisnik = @Korisnik
        ORDER BY id ASC";

            List<string> allowedMain = new List<string>();
            _mainMap.Clear();

            using (SqlConnection conn = new SqlConnection(connection))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Korisnik", Korisnik);

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string naziv = dr["Naziv"].ToString();                  
                        int id = dr.GetInt32(dr.GetOrdinal("id"));             

                        allowedMain.Add(naziv);

                        if (!_mainMap.ContainsKey(naziv))
                            _mainMap.Add(naziv, id);
                    }
                }
            }

            foreach (Control ctrl in splitContainer2.Panel1.Controls)
            {
                if (ctrl is FlowLayoutPanel flp)
                {
                    foreach (Control c in flp.Controls)
                    {
                        if (c is SfButton btn)
                        {
                            string key = btn.Text.Trim().ToLower();
                            btn.Visible = allowedMain.Contains(key);
                        }
                    }
                }
            }
        }
        private string NormalizeKey(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            var sb = new StringBuilder(value.Length);
            bool prevSpace = false;

            foreach (char ch in value)
            {
                char c = char.ToLowerInvariant(ch);

                // sve vrste whitespace (razmak, \r, \n, \t...) u jedan space
                if (char.IsWhiteSpace(c))
                {
                    if (!prevSpace)
                    {
                        sb.Append(' ');
                        prevSpace = true;
                    }
                    continue;
                }

                prevSpace = false;

                sb.Append(c);
            }

            return sb.ToString().Trim();
        }
        public int? GetKarticaId(int mainId, string nazivKartice, int? parentId)
        {
            string key = NormalizeKey(nazivKartice);

            using (var conn = new SqlConnection(connection))
            using (var cmd = new SqlCommand(@"
        SELECT ID, Naziv
        FROM mainNoviKartice
        WHERE MainID = @MainID
          AND (
                (@Parent IS NULL AND Parent IS NULL)
             OR Parent = @Parent
          )", conn))
            {
                cmd.Parameters.AddWithValue("@MainID", mainId);

                if (parentId.HasValue)
                    cmd.Parameters.AddWithValue("@Parent", parentId.Value);
                else
                    cmd.Parameters.AddWithValue("@Parent", DBNull.Value);

                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        int id = rd.GetInt32(0);
                        string dbNaziv = rd.GetString(1);

                        // upoređujemo normalizovane vrednosti
                        if (NormalizeKey(dbNaziv) == key)
                            return id;
                    }
                }
            }

            // nije pronađena nijedna kartica
            return null;
        }
        private bool ProveraPrava(int mainId, int karticaId)
        {
            using (var conn = new SqlConnection(connection))
            using (var cmd = new SqlCommand(@"
        SELECT COUNT(*)
        FROM MainNoviPrava
        WHERE Korisnik = @Korisnik
          AND MainID = @MainID
          AND KarticaID = @KarticaID", conn))
            {
                cmd.Parameters.AddWithValue("@Korisnik", Korisnik);
                cmd.Parameters.AddWithValue("@MainID", mainId);
                cmd.Parameters.AddWithValue("@KarticaID", karticaId);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
        public void OtvoriFormuSaPravom(string karticaNaziv, Func<Form> createForm)
        {
            if (_currentMainId == 0)
            {
                MessageBox.Show("Nije odabran modul (MainID).",
                                "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int? parentId = _karticaStack.Count > 0 ? _karticaStack.Peek() : (int?)null;

            var karticaId = GetKarticaId(_currentMainId, karticaNaziv, parentId);
            if (karticaId == null)
            {
                MessageBox.Show(
                    $"Kartica '{karticaNaziv}' nije pronađena u bazi za ovaj modul.",
                    "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!ProveraPrava(_currentMainId, karticaId.Value))
            {
                MessageBox.Show("Nemate pravo pristupa ovoj funkciji!",
                                "Pristup zabranjen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _karticaStack.Push(karticaId.Value);

            var frm = createForm();
            ShowChild(frm, true, false);
        }
        public void ShowChild(Form child, bool addToHistory = true, bool setAsMenuHome = false)
        {
            if (setAsMenuHome) menuHome = child;

            if (aktivna != null)
            {
                if (addToHistory) nav.Push(aktivna);
                aktivna.Hide();
                RemoveChildFromPanel1();
            }
            aktivna = child;
            child.TopLevel = false;
            child.FormBorderStyle = FormBorderStyle.None;
            child.Dock = DockStyle.Fill;
            splitContainer3.Panel1.Controls.Add(child);
            child.Show();
            child.BringToFront();
            child.Focus();
            UpdateBackEnabled();
        }

        public void NavigateBack()
        {
            if (nav.Count == 0)
            {
                ShowHome();
                return;
            }

            var prev = nav.Pop();

            if (_karticaStack.Count > 0)
                _karticaStack.Pop();    // jedan nivo nazad u stablu kartica

            if (aktivna != null)
            {
                aktivna.Hide();
                RemoveChildFromPanel1();
                aktivna = null;
            }

            prev.TopLevel = false;
            prev.FormBorderStyle = FormBorderStyle.None;
            prev.Dock = DockStyle.Fill;

            splitContainer3.Panel1.Controls.Add(prev);
            prev.Show();
            prev.BringToFront();
            prev.Focus();

            aktivna = prev;
            UpdateBackEnabled();
        }

        private void ShowHome()
        {
            if (aktivna != null)
            {
                aktivna.Hide();
                RemoveChildFromPanel1();
                aktivna = null;
            }
            while (nav.Count > 0) nav.Pop().Hide();

            _karticaStack.Clear();
            _currentMainId = 0;

            var home = new GlavniEkran();
            aktivna = home;

            home.TopLevel = false;
            home.FormBorderStyle = FormBorderStyle.None;
            home.Dock = DockStyle.Fill;

            splitContainer3.Panel1.Controls.Clear();
            splitContainer3.Panel1.Controls.Add(home);
            home.Show();
            home.BringToFront();
            home.Focus();
            splitContainer3.Panel2.Hide();
        }

        private void RemoveChildFromPanel1()
        {
            var toRemove = new List<Control>();
            foreach (Control c in splitContainer3.Panel1.Controls)
                if (c is Form) toRemove.Add(c);

            foreach (var c in toRemove)
                splitContainer3.Panel1.Controls.Remove(c);
        }

        private void UpdateBackEnabled() => btnNazad.Enabled = nav.Count > 0;
        private void BtnNazad_Click(object sender, EventArgs e) => NavigateBack();

        private void btnHome_Click(object sender, EventArgs e)
        {
            // If a menu-home form was set (form opened from left panel), show that form.
            if (menuHome != null)
            {
                ShowChild(menuHome, false);
                splitContainer3.Panel2.Show();
                return;
            }

            ShowHome();
        }
        private void btnLogistikaIzvoza_Click(object sender, EventArgs e)
        {
            string key = btnLogistikaIzvoza.Text.Trim().ToLower();

            if (!_mainMap.TryGetValue(key, out _currentMainId))
            {
                MessageBox.Show("Modul 'Logistika izvoza' nije pronađen u bazi MainNovi.",
                    "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _karticaStack.Clear();

            // Prva forma modula – setAsMenuHome = true da se Home vraća ovde
            ShowChild(new MainLeget.LegNew.LogistikaIzvoza1(), true, true);
            splitContainer3.Panel2.Show();
            lblNaslov.Text = "LOGISTIKA IZVOZA";
            BackColorKliknut(1);
        }

        private void btnPodesavanja_Click(object sender, EventArgs e)
        {
            string key = btnPodesavanja.Text.Trim().ToLower();

            if (!_mainMap.TryGetValue(key, out _currentMainId))
            {
                MessageBox.Show("Modul 'Podešavanje sistema' nije pronađen u bazi MainNovi.",
                    "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _karticaStack.Clear();

            ShowChild(new Podesavanje1(), true, true);
            splitContainer3.Panel2.Show();
            lblNaslov.Text = "PODEŠAVANJE SISTEMA";
            BackColorKliknut(13);
        }

        private void btnDrumski_Click(object sender, EventArgs e)
        {
            string key = btnDrumski.Text.Trim().ToLower();

            if (!_mainMap.TryGetValue(key, out _currentMainId))
            {
                MessageBox.Show("Modul 'Drumski transport' nije pronađen u bazi MainNovi.",
                    "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _karticaStack.Clear();

            ShowChild(new Drumski1(), true, true);
            splitContainer3.Panel2.Show();
            lblNaslov.Text = "DRUMSKI TRANSPORT";
            BackColorKliknut(3);
        }

        private void btnLogistikaUvoza_Click(object sender, EventArgs e)
        {
            BackColorKliknut(0);
        }

        private void btnLogistikaDirektnih_Click(object sender, EventArgs e)
        {
            BackColorKliknut(2);
        }

        private void btnZeleznicki_Click(object sender, EventArgs e)
        {
            BackColorKliknut(4);
        }

        private void btnSkladista_Click(object sender, EventArgs e)
        {
            BackColorKliknut(5);
        }

        private void btnPretovari_Click(object sender, EventArgs e)
        {
            BackColorKliknut(6);
        }

        private void btnPrijemIOtpremaVozova_Click(object sender, EventArgs e)
        {
            BackColorKliknut(7);
        }

        private void btnPrijemIOtpremaKamiona_Click(object sender, EventArgs e)
        {
            BackColorKliknut(8);
        }

        private void btnPTI_Click(object sender, EventArgs e)
        {
            BackColorKliknut(9);
        }

        private void btnOdrzavanje_Click(object sender, EventArgs e)
        {
            BackColorKliknut(10);
        }

        private void btnKapija_Click(object sender, EventArgs e)
        {
            BackColorKliknut(11);
        }

        private void btnFinansije_Click(object sender, EventArgs e)
        {
            BackColorKliknut(12);
        }
        public void Paneli()
        {
            Color formBg = ColorTranslator.FromHtml("#EEF2F6");
            Color topBlue = ColorTranslator.FromHtml("#6E90E2");
            Color leftGradFrom = ColorTranslator.FromHtml("#0F2433");
            Color leftGradTo = ColorTranslator.FromHtml("#203D55");

            this.SuspendLayout();
            this.BackColor = formBg;
            this.DoubleBuffered = true;

            // Rounded forma
            this.FormBorderStyle = FormBorderStyle.None;
            int formRadius = 14;
            ApplyRoundedRegion(this, formRadius);

            splitContainer1.Orientation = Orientation.Horizontal;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.SplitterWidth = 1;
            splitContainer1.Panel1MinSize = 56;
            splitContainer1.SplitterDistance = 56;
            splitContainer1.Panel1.Paint -= TopNav_Paint;
            splitContainer1.Panel1.Paint += TopNav_Paint;

            // ===== splitContainer2: 
            if (!splitContainer1.Panel2.Controls.Contains(splitContainer2))
            {
                splitContainer2.Parent = splitContainer1.Panel2;
                splitContainer2.Dock = DockStyle.Fill;
            }

            splitContainer2.Orientation = Orientation.Vertical;
            splitContainer2.IsSplitterFixed = false;
            splitContainer2.SplitterWidth = 2;

            // Levi panel: gradient
            splitContainer2.Panel1.Paint -= LeftPanel_PaintGradient;
            splitContainer2.Panel1.Paint += LeftPanel_PaintGradient;

            splitContainer2.Panel2.BackColor = formBg;
            this.Resize -= MainForm_ResizeRedraw;
            this.Resize += MainForm_ResizeRedraw;

            this.ResumeLayout(true);

            void TopNav_Paint(object s, PaintEventArgs e)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var sb = new SolidBrush(topBlue);
                e.Graphics.FillRectangle(sb, splitContainer1.Panel1.ClientRectangle);

                var pen = new Pen(Color.FromArgb(70, Color.Black), 1);
                e.Graphics.DrawLine(pen, 0, splitContainer1.Panel1.Height - 1,
                                         splitContainer1.Panel1.Width, splitContainer1.Panel1.Height - 1);
            }

            void LeftPanel_PaintGradient(object s, PaintEventArgs e)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var lg = new LinearGradientBrush(splitContainer2.Panel1.ClientRectangle,
                                                       leftGradTo, leftGradFrom, 0f);
                e.Graphics.FillRectangle(lg, splitContainer2.Panel1.ClientRectangle);
                var pen = new Pen(Color.FromArgb(40, 0, 0, 0), 1);
                e.Graphics.DrawLine(pen, splitContainer2.Panel1.Width - 1, 0,
                                         splitContainer2.Panel1.Width - 1, splitContainer2.Panel1.Height);
            }

            ApplyGradientToAllButtonsInPanel();
            AttachFocusHandlersToLeftPanelButtons();
        }

        private void ApplyGradientToAllButtonsInPanel()
        {
            Color gradFrom = ColorTranslator.FromHtml("#203D55"); // tamnija (leva)
            Color gradTo = ColorTranslator.FromHtml("#0F2433");   // svetlija (desna)

            foreach (Control ctrl in splitContainer2.Panel1.Controls)
            {
                if (ctrl is Button btn)
                {
                    bool isHover = false;

                    btn.Paint += (s, e) =>
                    {
                        Color from = isHover ? ControlPaint.Light(gradFrom, 0.25f) : gradFrom;
                        Color to = isHover ? ControlPaint.Light(gradTo, 0.25f) : gradTo;

                        using (var lg = new LinearGradientBrush(btn.ClientRectangle, from, to, 0f))
                        {
                            e.Graphics.FillRectangle(lg, btn.ClientRectangle);
                        }

                        if (btn.Image != null)
                        {
                            e.Graphics.DrawImage(btn.Image,
                                new Rectangle(10, (btn.Height - 48) / 2, 48, 48));
                        }
                        var textSize = e.Graphics.MeasureString(btn.Text, btn.Font);
                        e.Graphics.DrawString(btn.Text, btn.Font, Brushes.White,
                            (btn.Width - textSize.Width) / 2,
                            (btn.Height - textSize.Height) / 2);
                    };

                    btn.MouseEnter += (s, e) =>
                    {
                        isHover = true;
                        btn.Invalidate();
                    };

                    btn.MouseLeave += (s, e) =>
                    {
                        isHover = false;
                        btn.Invalidate();
                    };
                }
            }
        }

        private void MainForm_ResizeRedraw(object sender, EventArgs e)
        {
            ApplyRoundedRegion(this, 14);
            splitContainer1.Panel1.Invalidate();
            splitContainer2.Panel1.Invalidate();
        }

        private void ApplyRoundedRegion(Control target, int radius)
        {
            if (target.Width < 2 || target.Height < 2) return;
            var path = RoundedRect(new Rectangle(0, 0, target.Width, target.Height), radius);
            target.Region = new Region(path);
        }

        private GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            var p = new GraphicsPath();
            p.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            p.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            p.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            p.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            p.CloseFigure();
            return p;
        }

        private void BackColorKliknut(int Dugme)
        {
            btnLogistikaUvoza.BackColor = Color.FromArgb(32, 61, 85);
            btnLogistikaIzvoza.BackColor = Color.FromArgb(32, 61, 85);
            btnLogistikaDirektnih.BackColor = Color.FromArgb(32, 61, 85);
            btnDrumski.BackColor = Color.FromArgb(32, 61, 85);
            btnZeleznicki.BackColor = Color.FromArgb(32, 61, 85);
            btnSkladista.BackColor = Color.FromArgb(32, 61, 85);
            btnPretovari.BackColor = Color.FromArgb(32, 61, 85);
            btnPrijemIOtpremaVozova.BackColor = Color.FromArgb(32, 61, 85);
            btnPrijemIOtpremaKamiona.BackColor = Color.FromArgb(32, 61, 85);
            btnPTI.BackColor = Color.FromArgb(32, 61, 85);
            btnOdrzavanje.BackColor = Color.FromArgb(32, 61, 85);
            btnKapija.BackColor = Color.FromArgb(32, 61, 85);
            btnFinansije.BackColor = Color.FromArgb(32, 61, 85);
            btnPodesavanja.BackColor = Color.FromArgb(32, 61, 85);

            switch (Dugme)
            {
                case 0: btnLogistikaUvoza.BackColor = Color.FromArgb(1, 115, 199); break;
                case 1: btnLogistikaIzvoza.BackColor = Color.FromArgb(1, 115, 199); break;
                case 2: btnLogistikaDirektnih.BackColor = Color.FromArgb(1, 115, 199); break;
                case 3: btnDrumski.BackColor = Color.FromArgb(1, 115, 199); break;
                case 4: btnZeleznicki.BackColor = Color.FromArgb(1, 115, 199); break;
                case 5: btnSkladista.BackColor = Color.FromArgb(1, 115, 199); break;
                case 6: btnPretovari.BackColor = Color.FromArgb(1, 115, 199); break;
                case 7: btnPrijemIOtpremaVozova.BackColor = Color.FromArgb(1, 115, 199); break;
                case 8: btnPrijemIOtpremaKamiona.BackColor = Color.FromArgb(1, 115, 199); break;
                case 9: btnPTI.BackColor = Color.FromArgb(1, 115, 199); break;
                case 10: btnOdrzavanje.BackColor = Color.FromArgb(1, 115, 199); break;
                case 11: btnKapija.BackColor = Color.FromArgb(1, 115, 199); break;
                case 12: btnFinansije.BackColor = Color.FromArgb(1, 115, 199); break;
                case 13: btnPodesavanja.BackColor = Color.FromArgb(1, 115, 199); break;
                default: break;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            // Dashboard should show the main dashboard screen
            lblNaslov.Text = "INTEGRATED LOGISTICS MANAGEMENT SYSTEM";
            ShowHome();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Top window control buttons
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                // restore rounded corners
                ApplyRoundedRegion(this, 14);
                if (this.btnMaximize != null) this.btnMaximize.Text = "▢";
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                // remove region so form fills screen without rounded corners
                this.Region = null;
                if (this.btnMaximize != null) this.btnMaximize.Text = "❐";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnToggleLeftPanel_Click(object sender, EventArgs e)
        {
            try
            {
                // If panel is currently visible (SplitterDistance > 0 and panel not collapsed)
                if (!splitContainer2.Panel1Collapsed && splitContainer2.SplitterDistance > 0)
                {
                    // Save current width
                    _savedLeftPanelWidth = splitContainer2.SplitterDistance;
                    // Collapse panel
                    splitContainer2.Panel1Collapsed = true;
                }
                else
                {
                    // Restore panel
                    splitContainer2.Panel1Collapsed = false;

                    // Determine valid min and max for SplitterDistance
                    int min = Math.Max(0, splitContainer2.Panel1MinSize);
                    int panel2Min = Math.Max(0, splitContainer2.Panel2MinSize);
                    int max = 0;

                    // Calculate max allowed SplitterDistance based on current control width
                    if (splitContainer2.Width > 0)
                    {
                        max = splitContainer2.Width - panel2Min - splitContainer2.SplitterWidth;
                        if (max < min) max = min;
                    }
                    else
                    {
                        // fallback if width not yet measured
                        max = Math.Max(min, _savedLeftPanelWidth);
                    }

                    int toSet = _savedLeftPanelWidth;
                    if (toSet < min) toSet = min;
                    if (toSet > max) toSet = max;

                    // Finally set SplitterDistance
                    splitContainer2.SplitterDistance = toSet;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom menjanja panela: " + ex.Message);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            try
            {
                // Close any child forms hosted in splitContainer3.Panel1
                foreach (Control c in splitContainer3.Panel1.Controls)
                {
                    if (c is Form f)
                    {
                        try { f.Close(); } catch { }
                    }
                }

                // Close any other open forms in the application except this one
                var openForms = new List<Form>();
                foreach (Form frm in Application.OpenForms)
                {
                    if (frm != this)
                        openForms.Add(frm);
                }

                foreach (var frm in openForms)
                {
                    try { frm.Close(); } catch { }
                }

                // Finally close main form and exit
                this.Close();
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom izlaska: " + ex.Message);
            }
        }

        private void AttachFocusHandlersToLeftPanelButtons()
        {
            foreach (Control ctrl in splitContainer2.Panel1.Controls)
            {
                if (!(ctrl is Control)) continue;

                // store original font
                if (!_originalButtonFonts.ContainsKey(ctrl))
                    _originalButtonFonts[ctrl] = ctrl.Font;

                // try to get original image (from Image property or Style.Image)
                var img = GetButtonImage(ctrl);
                if (img != null && !_originalButtonImages.ContainsKey(ctrl))
                    _originalButtonImages[ctrl] = img;

                // attach handlers (use Enter/Leave so keyboard focus triggers too)
                ctrl.Enter -= LeftPanelButton_Enter;
                ctrl.Leave -= LeftPanelButton_Leave;
                ctrl.Enter += LeftPanelButton_Enter;
                ctrl.Leave += LeftPanelButton_Leave;
            }
        }

        private void LeftPanelButton_Enter(object sender, EventArgs e)
        {
            if (!(sender is Control ctrl)) return;

            // increase font size to 14
            try
            {
                if (!_originalButtonFonts.ContainsKey(ctrl))
                    _originalButtonFonts[ctrl] = ctrl.Font;
                ctrl.Font = new Font(ctrl.Font.FontFamily, 14F, ctrl.Font.Style);
            }
            catch { }

            // scale image by 10%
            try
            {
                Image orig = null;
                if (!_originalButtonImages.TryGetValue(ctrl, out orig))
                {
                    orig = GetButtonImage(ctrl);
                    if (orig != null)
                        _originalButtonImages[ctrl] = orig;
                }

                if (orig != null)
                {
                    int newW = Math.Max(1, (int)(orig.Width * 1.1));
                    int newH = Math.Max(1, (int)(orig.Height * 1.1));
                    var scaled = new Bitmap(orig, new Size(newW, newH));
                    SetButtonImage(ctrl, scaled);
                    // don't dispose orig here (we keep reference). Dispose scaled when reverting
                }
            }
            catch { }
        }

        private void LeftPanelButton_Leave(object sender, EventArgs e)
        {
            if (!(sender is Control ctrl)) return;

            // restore original font
            try
            {
                if (_originalButtonFonts.TryGetValue(ctrl, out var of))
                {
                    ctrl.Font = of;
                }
            }
            catch { }

            // restore original image
            try
            {
                if (_originalButtonImages.TryGetValue(ctrl, out var orig))
                {
                    SetButtonImage(ctrl, orig);
                }
            }
            catch { }
        }

        private Image GetButtonImage(Control ctrl)
        {
            if (ctrl == null) return null;
            try
            {
                var t = ctrl.GetType();
                var imgProp = t.GetProperty("Image");
                if (imgProp != null && imgProp.CanRead)
                {
                    var img = imgProp.GetValue(ctrl) as Image;
                    if (img != null) return img;
                }

                // try Style.Image (for Syncfusion SfButton)
                var styleProp = t.GetProperty("Style");
                if (styleProp != null)
                {
                    var styleObj = styleProp.GetValue(ctrl);
                    if (styleObj != null)
                    {
                        var sType = styleObj.GetType();
                        var sImgProp = sType.GetProperty("Image");
                        if (sImgProp != null && sImgProp.CanRead)
                        {
                            var img = sImgProp.GetValue(styleObj) as Image;
                            if (img != null) return img;
                        }
                    }
                }
            }
            catch { }
            return null;
        }

        private void SetButtonImage(Control ctrl, Image img)
        {
            if (ctrl == null) return;
            try
            {
                var t = ctrl.GetType();
                var imgProp = t.GetProperty("Image");
                if (imgProp != null && imgProp.CanWrite)
                {
                    imgProp.SetValue(ctrl, img);
                    return;
                }

                var styleProp = t.GetProperty("Style");
                if (styleProp != null)
                {
                    var styleObj = styleProp.GetValue(ctrl);
                    if (styleObj != null)
                    {
                        var sType = styleObj.GetType();
                        var sImgProp = sType.GetProperty("Image");
                        if (sImgProp != null && sImgProp.CanWrite)
                        {
                            sImgProp.SetValue(styleObj, img);
                            return;
                        }
                    }
                }
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdministracijaPravoPristupa frm = new AdministracijaPravoPristupa(Korisnik);
            frm.Show();
        }
    }
}
