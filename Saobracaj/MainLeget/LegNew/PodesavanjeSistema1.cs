using Saobracaj.MainLeget;
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
using Testiranje.Sifarnici;

namespace Saobracaj.MainLeget.LegNew
{
    public partial class PodesavanjeSistema1 : Form
    {
        string Korisnik = Sifarnici.frmLogovanje.user;
        
        public PodesavanjeSistema1()
        {
            InitializeComponent();
           // OpenInPanel2(new MainLeget.Info(OpenInPanel2));

           // btnLogistikaUvoza.Click += (s, e) => OpenInPanel2(new MainLeget.UvozMain(OpenInPanel2));
        }/*
        public void OpenInPanel2(Form child)
        {
            if(_activeChild != null)
            {
                _activeChild.Close();
                _activeChild.Dispose();
                _activeChild = null;
            }
            _activeChild = child;
            child.TopLevel = false;
            child.FormBorderStyle=FormBorderStyle.None;
            child.Dock = DockStyle.Fill;
            child.AutoScaleMode=AutoScaleMode.None;

            splitContainer2.Panel2.Controls.Clear();
            splitContainer2.Panel2.Controls.Add(child);
            child.Show();
            child.BringToFront();
            child.Focus();
        }
        */
        #region Boje
        private void MainLeget_Load(object sender, EventArgs e)
        {
           // Paneli();
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

        private void button1_Click(object sender, EventArgs e)
        {
           //OpenInPanel2(new MainLeget.Info(OpenInPanel2));
        }

        private void btnLogistikaUvoza_Click(object sender, EventArgs e)
        {
            //panel1.Visible = false;
        }

        private void sfButton1_Click(object sender, EventArgs e)
        {
            
    
            
            lblNaslov.Text = "Logistika izvoza";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sfPLokacije_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "SkladisteGrupa")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Saobracaj.TrackModal.Sifarnici.SkladisteGrupa jm = new SkladisteGrupa();
                jm.Show();
            }


        }

        private void sfPPolja_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmSkladista")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmSkladista sklad = new frmSkladista(Korisnik);
                sklad.Show();
            }
        }

        private void sfPZone_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmZone")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmZone sklad = new frmZone();
                sklad.Show();
            }
        }

        private void sfPPozicije_Click(object sender, EventArgs e)
        {


            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPozicija")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmPozicija poz = new frmPozicija(Korisnik);
                poz.Show();
            }
        }
    }
}

