using Saobracaj.Drumski;
using Saobracaj.MainLeget;
using Saobracaj.MainLeget.LegNew;
using Saobracaj.MainLeget.LegNew.Podesavanjesistema;
using Saobracaj.Sifarnici;
using Saobracaj.TrackModal.Sifarnici;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        // The form that should be shown when clicking the Home button (invoked from left panel)
        private Form menuHome = null;
        public NewMain()
        {
            InitializeComponent();
            ShowHome();
        }
        private void MainLeget_Load(object sender, EventArgs e)
        {

        }
        // Added optional parameter setAsMenuHome to mark a form as the "menu home"
        public void ShowChild(Form child,bool addToHistory = true, bool setAsMenuHome = false)
        {
            if (setAsMenuHome) menuHome = child;

            if(aktivna != null)
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
        private void btnNazad_Click(object sender, EventArgs e) => NavigateBack();
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
            var parent = this.TopLevelControl as NewMain;
            // Mark this form as the menu home so Home button will return here
            parent?.ShowChild(new MainLeget.LegNew.LogistikaIzvoza1(), true, true);
            splitContainer3.Panel2.Show();
            lblNaslov.Text = "LOGISTIKA IZVOZA";
            BackColorKliknut(1);

        }

        #region Boje

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

        }
        private void ApplyGradientToAllButtonsInPanel()
        {
            Color gradFrom = ColorTranslator.FromHtml("#203D55"); // tamnija (leva)
            Color gradTo = ColorTranslator.FromHtml("#0F2433"); // svetlija (desna)

            foreach (Control ctrl in splitContainer2.Panel1.Controls)
            {
                if (ctrl is Button btn)
                {
                    bool isHover = false;

                    btn.Paint += (s, e) =>
                    {
                        Color from = isHover ? ControlPaint.Light(gradFrom, 0.25f) : gradFrom;
                        Color to = isHover ? ControlPaint.Light(gradTo, 0.25f) : gradTo;

                        using (var lg = new System.Drawing.Drawing2D.LinearGradientBrush(
                            btn.ClientRectangle, from, to, 0f))
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
        #endregion

        private void btnLogistikaUvoza_Click(object sender, EventArgs e)
        {
            BackColorKliknut(0);

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
                case 0:
                    {
                        btnLogistikaUvoza.BackColor = Color.FromArgb(1, 115, 199);break;
                    }
                case 1:
                    {  btnLogistikaIzvoza.BackColor = Color.FromArgb(1, 115, 199); break; }
                case 2:
                    { btnLogistikaDirektnih.BackColor = Color.FromArgb(1, 115, 199); break; }
                case 3:
                    { btnDrumski.BackColor = Color.FromArgb(1, 115, 199); break; }
                case 4:
                    { btnZeleznicki.BackColor = Color.FromArgb(1, 115, 199); break; }
                case 5:
                    { btnSkladista.BackColor = Color.FromArgb(1, 115, 199); break; }
                case 6:
                    { btnPretovari.BackColor = Color.FromArgb(1, 115, 199); break; }
                case 7:
                    { btnPrijemIOtpremaVozova.BackColor = Color.FromArgb(1, 115, 199); break; }
                case 8:
                    { btnPrijemIOtpremaKamiona.BackColor = Color.FromArgb(1, 115, 199); break; }
                case 9:
                    { btnPTI.BackColor = Color.FromArgb(1, 115, 199); break; }
                case 10:
                    { btnOdrzavanje.BackColor = Color.FromArgb(1, 115, 199); break; }
                case 11:
                    { btnKapija.BackColor = Color.FromArgb(1, 115, 199); break; }
                case 12:
                    { btnFinansije.BackColor = Color.FromArgb(1, 115, 199); break; }
                case 13:
                    { btnPodesavanja.BackColor = Color.FromArgb(1, 115, 199); break; }
                default:
                    {
                        break;
                    }
                    
            }
        
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPodesavanja_Click(object sender, EventArgs e)
        {
            var parent = this.TopLevelControl as NewMain;
            parent?.ShowChild(new Podesavanje1(), true, true);
            splitContainer3.Panel2.Show();
            lblNaslov.Text = "PODEŠAVANJE SISTEMA";
            BackColorKliknut(13);
        }

        private void btnDrumski_Click(object sender, EventArgs e)
        {
            var parent = this.TopLevelControl as NewMain;
            parent?.ShowChild(new Drumski1(), true, true);
            splitContainer3.Panel2.Show();
            lblNaslov.Text = "DRUMSKI TRANSPORT";
            BackColorKliknut(3);
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
    }
}

