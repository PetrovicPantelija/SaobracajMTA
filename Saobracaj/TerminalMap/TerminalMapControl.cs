using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.TerminalMap
{
    public enum MapStretchMode { Fit, Fill }

    public class TerminalMapControl : Control
    {
        public PointF ImageOffSet { get; set; } = new PointF(0, 0);
        public bool EditMode { get; set; } = false;

        public bool ShowBaseImage { get; set; } = true;

        // NOVO: boja “platna” kada je pozadina (slika) OFF
        public Color CanvasBackColor { get; set; } = Color.White;

        // boja za nove objekte (fill/linija)
        public Color CurrentDrawColor { get; set; } = Color.FromArgb(60, Color.Lime);

        // NOVO: javimo formi da upiše settings u overlay json
        public event EventHandler CanvasAppearanceChanged;

        public event EventHandler<string> HangarClicked;
        public event EventHandler<HangarRegion> RegionAdded;
        public event EventHandler<HangarRegion> RegionOpenRequested;
        public event EventHandler<HangarRegion> RegionDeleteRequested;
        public event EventHandler<HangarRegion> RegionChanged;


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public List<HangarRegion> Regions { get; } = new List<HangarRegion>();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Bitmap MapImage
        {
            get => _mapImage;
            set { _mapImage = value; if (!IsDesignTime) FitToView(); Invalidate(); }
        }

        public MapStretchMode StretchMode { get; set; } = MapStretchMode.Fill;

        private Bitmap _mapImage;

        private float zoom = 1f;
        private PointF pan = new PointF(0, 0);

        private bool panning;
        private Point panStartMouse;
        private PointF panStart;

        private bool drawingRect;
        private PointF drawStartImg;
        private RectangleF drawRectImg;

        private bool drawingLine;
        private readonly List<PointF> linePointsLocal = new List<PointF>();

        private readonly ContextMenuStrip _ctxRegion = new ContextMenuStrip();
        private HangarRegion _ctxHitRegion;

        private readonly ContextMenuStrip _ctxCanvas = new ContextMenuStrip();

        private bool IsDesignTime =>
            LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode;

        public TerminalMapControl()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);

            if (IsDesignTime) return;

            TabStop = true;

            // REGION meni
            var miOpen = new ToolStripMenuItem("Otvori");
            miOpen.Click += (_, __) => { if (_ctxHitRegion != null) RegionOpenRequested?.Invoke(this, _ctxHitRegion); };

            var miColor = new ToolStripMenuItem("Promeni boju...");
            miColor.Click += (_, __) =>
            {
                if (_ctxHitRegion == null) return;
                using (var cd = new ColorDialog())
                {
                    cd.Color = Color.FromArgb(_ctxHitRegion.ColorArgb);
                    if (cd.ShowDialog() == DialogResult.OK)
                    {
                        int a = Color.FromArgb(_ctxHitRegion.ColorArgb).A;
                        _ctxHitRegion.ColorArgb = Color.FromArgb(a, cd.Color).ToArgb();
                        RegionChanged?.Invoke(this, _ctxHitRegion);
                        Invalidate();
                    }
                }
            };

            var miDelete = new ToolStripMenuItem("Obriši region");
            miDelete.Click += (_, __) => { if (_ctxHitRegion != null) RegionDeleteRequested?.Invoke(this, _ctxHitRegion); };

            _ctxRegion.Items.Add(miOpen);
            _ctxRegion.Items.Add(miColor);
            _ctxRegion.Items.Add(new ToolStripSeparator());
            _ctxRegion.Items.Add(miDelete);

            // CANVAS meni (samo kad je pozadina OFF i desni klik van regiona)
            var miBgWhite = new ToolStripMenuItem("Pozadina: BELA");
            miBgWhite.Click += (_, __) =>
            {
                CanvasBackColor = Color.White;
                Invalidate();
                CanvasAppearanceChanged?.Invoke(this, EventArgs.Empty);
            };

            var miBgBlack = new ToolStripMenuItem("Pozadina: CRNA");
            miBgBlack.Click += (_, __) =>
            {
                CanvasBackColor = Color.Black;
                Invalidate();
                CanvasAppearanceChanged?.Invoke(this, EventArgs.Empty);
            };

            _ctxCanvas.Items.Add(miBgWhite);
            _ctxCanvas.Items.Add(miBgBlack);

            SizeChanged += (_, __) => { if (MapImage != null) FitToView(); };

            MouseWheel += (_, e) => ZoomAt(e.Location, e.Delta > 0 ? 1.15f : 1f / 1.15f);

            MouseDown += (_, e) =>
            {
                if (e.Button == MouseButtons.Middle || (e.Button == MouseButtons.Left && ModifierKeys == Keys.Space))
                {
                    panning = true;
                    panStartMouse = e.Location;
                    panStart = pan;
                    Cursor = Cursors.Hand;
                    return;
                }

                if (!EditMode) return;

                // ALT + LMB -> polyline tačke
                if (e.Button == MouseButtons.Left && ModifierKeys.HasFlag(Keys.Alt))
                {
                    drawingLine = true;
                    var p = ScreenToImage(e.Location);
                    linePointsLocal.Add(p);
                    Invalidate();
                    return;
                }

                if (e.Button == MouseButtons.Left)
                {
                    drawingRect = true;
                    drawStartImg = ScreenToImage(e.Location);
                    drawRectImg = new RectangleF(drawStartImg.X, drawStartImg.Y, 0, 0);
                    Invalidate();
                }
            };

            MouseMove += (_, e) =>
            {
                if (drawingRect)
                {
                    var cur = ScreenToImage(e.Location);
                    drawRectImg = NormalizeRect(drawStartImg, cur);
                    Invalidate();
                    return;
                }

                if (!panning) return;

                pan = new PointF(panStart.X + (e.X - panStartMouse.X), panStart.Y + (e.Y - panStartMouse.Y));
                Invalidate();
            };

            MouseUp += (_, e) =>
            {
                if (panning)
                {
                    panning = false;
                    Cursor = Cursors.Default;
                }

                // DESNI KLIK -> prvo pokušaj region
                if (e.Button == MouseButtons.Right)
                {
                    var hit = HitTestRegion(e.Location);
                    if (hit != null)
                    {
                        _ctxHitRegion = hit;
                        _ctxRegion.Show(this, e.Location);
                        return;
                    }

                    // Ako je pozadina OFF i klik je van regiona -> canvas meni (bela/crna)
                    if (!ShowBaseImage)
                    {
                        _ctxCanvas.Show(this, e.Location);
                        return;
                    }

                    // u edit modu desni klik završava liniju
                    if (EditMode && drawingLine && linePointsLocal.Count >= 2)
                    {
                        FinishPolyline();
                        return;
                    }
                }

                if (drawingRect && e.Button == MouseButtons.Left)
                {
                    drawingRect = false;

                    if (drawRectImg.Width > 5 && drawRectImg.Height > 5)
                    {
                        string id = Prompt.Show("Unesi naziv (SkNaziv):", "Novi region");
                        id = (id ?? "").Trim();

                        if (!string.IsNullOrWhiteSpace(id))
                        {
                            var rectOriginal = new RectangleF(
                                drawRectImg.X + ImageOffSet.X,
                                drawRectImg.Y + ImageOffSet.Y,
                                drawRectImg.Width,
                                drawRectImg.Height
                            );

                            var reg = new HangarRegion
                            {
                                ID = id,
                                RectImg = rectOriginal,
                                Shape = ShapeType.Rect,
                                ColorArgb = CurrentDrawColor.ToArgb()
                            };

                            Regions.Add(reg);
                            RegionAdded?.Invoke(this, reg);
                        }
                    }

                    drawRectImg = RectangleF.Empty;
                    Invalidate();
                }
            };

            MouseClick += (_, e) =>
            {
                if (EditMode) return;
                if (e.Button != MouseButtons.Left) return;

                var hit = HitTestRegion(e.Location);
                if (hit != null)
                    HangarClicked?.Invoke(this, hit.ID);
            };

            KeyDown += (_, e) =>
            {
                if (!EditMode) return;

                if (drawingLine && e.KeyCode == Keys.Enter)
                {
                    if (linePointsLocal.Count >= 2) FinishPolyline();
                    e.Handled = true;
                }
                else if (drawingLine && e.KeyCode == Keys.Escape)
                {
                    drawingLine = false;
                    linePointsLocal.Clear();
                    Invalidate();
                    e.Handled = true;
                }
            };
        }

        private void FinishPolyline()
        {
            drawingLine = false;

            string id = Prompt.Show("Unesi oznaku linije (lokalno):", "Nova linija");
            id = (id ?? "").Trim();

            if (string.IsNullOrWhiteSpace(id))
            {
                linePointsLocal.Clear();
                Invalidate();
                return;
            }

            var ptsOriginal = linePointsLocal
                .Select(p => new PointF(p.X + ImageOffSet.X, p.Y + ImageOffSet.Y))
                .ToList();

            var reg = new HangarRegion
            {
                ID = id,
                Shape = ShapeType.Polyline,
                PointsImg = ptsOriginal,
                ColorArgb = CurrentDrawColor.ToArgb()
            };

            Regions.Add(reg);
            RegionAdded?.Invoke(this, reg);

            linePointsLocal.Clear();
            Invalidate();
        }

        public void FitToView()
        {
            if (MapImage == null || ClientSize.Width <= 0 || ClientSize.Height <= 0)
                return;

            float zx = (float)ClientSize.Width / MapImage.Width;
            float zy = (float)ClientSize.Height / MapImage.Height;

            zoom = (StretchMode == MapStretchMode.Fill) ? Math.Max(zx, zy) : Math.Min(zx, zy);

            float imgW = MapImage.Width * zoom;
            float imgH = MapImage.Height * zoom;

            pan = new PointF((ClientSize.Width - imgW) / 2f, (ClientSize.Height - imgH) / 2f);
            Invalidate();
        }

        public void ResetView() => FitToView();

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Pozadina platna (bela/crna) – koristi se uvek,
            // ali kad je slika ON, slika će prekriti.
            e.Graphics.Clear(CanvasBackColor);

            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            if (MapImage == null && Regions.Count == 0) return;

            using (var m = new Matrix())
            {
                m.Translate(pan.X, pan.Y);
                m.Scale(zoom, zoom);
                e.Graphics.Transform = m;

                if (MapImage != null && ShowBaseImage)
                    e.Graphics.DrawImage(MapImage, 0, 0, MapImage.Width, MapImage.Height);

                // Pravila boja (kad je pozadina OFF)
                bool canvasBlack = !ShowBaseImage && CanvasBackColor.ToArgb() == Color.Black.ToArgb();
                Color textColor = canvasBlack ? Color.White : Color.Black;

                var cropRectOriginal = new RectangleF(
                    ImageOffSet.X, ImageOffSet.Y,
                    MapImage?.Width ?? 100000,
                    MapImage?.Height ?? 100000
                );

                foreach (var r in Regions)
                {
                    if (r.Shape == ShapeType.Rect)
                    {
                        if (!cropRectOriginal.IntersectsWith(r.RectImg)) continue;

                        var local = new RectangleF(
                            r.RectImg.X - ImageOffSet.X,
                            r.RectImg.Y - ImageOffSet.Y,
                            r.RectImg.Width,
                            r.RectImg.Height
                        );

                        var stored = Color.FromArgb(r.ColorArgb);

                        // Kad je slika OFF -> puni alfa (jasnije boje)
                        var fill = ShowBaseImage ? stored : Color.FromArgb(255, stored);

                        var br = new SolidBrush(fill);

                        // 3) Kad je slika ON -> ivice uvek crne
                        // 4) Kad je pozadina OFF:
                        //    - ako je canvas bela -> ivice crne
                        //    - ako je canvas crna -> ivice su boje kvadrata (puna)
                        Color borderColor;
                        if (ShowBaseImage) borderColor = Color.Black;
                        else borderColor = canvasBlack ? Color.FromArgb(255, fill) : Color.Black;

                         var pen = new Pen(borderColor, 2f / zoom);

                        e.Graphics.FillRectangle(br, local);
                        e.Graphics.DrawRectangle(pen, local.X, local.Y, local.Width, local.Height);

                        // 1) Kad je slika ON -> ne prikazuj tekst
                        if (ShowBaseImage) continue;

                        // 2) Kad je slika OFF -> tekst centriran + dinamički font
                        string txt = r.ID ?? "";
                        if (txt.Length == 0) continue;

                        // font u “svetskim” koordinatama (zato /zoom)
                        float baseSize = Math.Min(local.Width, local.Height) * 0.22f; // 22% od manjeg
                        baseSize = Math.Max(6f, Math.Min(24f, baseSize));             // clamp
                        float fontSize = baseSize / zoom;

                        var font = new Font(FontFamily.GenericSansSerif, fontSize, FontStyle.Bold);

                        var sf = new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center,
                            Trimming = StringTrimming.EllipsisCharacter
                        };

                        var tb = new SolidBrush(textColor);
                        e.Graphics.DrawString(txt, font, tb, local, sf);
                    }
                    else if (r.Shape == ShapeType.Polyline)
                    {
                        if (r.PointsImg == null || r.PointsImg.Count < 2) continue;

                        var ptsLocal = r.PointsImg
                            .Select(p => new PointF(p.X - ImageOffSet.X, p.Y - ImageOffSet.Y))
                            .ToArray();

                        var col = Color.FromArgb(r.ColorArgb);
                        var pen = new Pen(Color.FromArgb(255, col), 3f / zoom)
                        {
                            LineJoin = LineJoin.Round,
                            StartCap = LineCap.Round,
                            EndCap = LineCap.Round
                        };

                        e.Graphics.DrawLines(pen, ptsLocal);
                    }
                }

                // preview rectangle
                if (EditMode && drawingRect && !drawRectImg.IsEmpty)
                {
                    var pen2 = new Pen(Color.Blue, 2f / zoom);
                    e.Graphics.DrawRectangle(pen2, drawRectImg.X, drawRectImg.Y, drawRectImg.Width, drawRectImg.Height);
                }

                // preview line
                if (EditMode && drawingLine && linePointsLocal.Count >= 1)
                {
                    var pen3 = new Pen(Color.Red, 3f / zoom) { DashStyle = DashStyle.Dash };
                    if (linePointsLocal.Count >= 2)
                        e.Graphics.DrawLines(pen3, linePointsLocal.ToArray());
                    else
                        e.Graphics.DrawEllipse(pen3, linePointsLocal[0].X - 2, linePointsLocal[0].Y - 2, 4, 4);
                }
            }
        }

        private static float Clamp(float value, float min, float max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        private void ZoomAt(Point screenPt, float factor)
        {
            var oldZoom = zoom;
            zoom = Clamp(zoom * factor, 0.2f, 8f);

            var imgBefore = ScreenToImage(screenPt, oldZoom, pan);
            var imgAfter = ScreenToImage(screenPt, zoom, pan);

            pan.X += (imgAfter.X - imgBefore.X) * zoom;
            pan.Y += (imgAfter.Y - imgBefore.Y) * zoom;

            Invalidate();
        }

        private PointF ScreenToImage(Point screenPt) => ScreenToImage(screenPt, zoom, pan);

        private static PointF ScreenToImage(Point screenPt, float z, PointF p)
            => new PointF((screenPt.X - p.X) / z, (screenPt.Y - p.Y) / z);

        private static RectangleF NormalizeRect(PointF a, PointF b)
        {
            float x1 = Math.Min(a.X, b.X);
            float y1 = Math.Min(a.Y, b.Y);
            float x2 = Math.Max(a.X, b.X);
            float y2 = Math.Max(a.Y, b.Y);
            return new RectangleF(x1, y1, x2 - x1, y2 - y1);
        }

        private HangarRegion HitTestRegion(Point screenPoint)
        {
            if (Regions.Count == 0) return null;

            var imgPtLocal = ScreenToImage(screenPoint);
            var imgPtOriginal = new PointF(imgPtLocal.X + ImageOffSet.X, imgPtLocal.Y + ImageOffSet.Y);

            var hitRect = Regions.FirstOrDefault(r => r.Shape == ShapeType.Rect && r.RectImg.Contains(imgPtOriginal));
            if (hitRect != null) return hitRect;

            const float tol = 6f;
            foreach (var r in Regions.Where(x => x.Shape == ShapeType.Polyline && x.PointsImg != null && x.PointsImg.Count >= 2))
            {
                if (IsPointNearPolyline(imgPtOriginal, r.PointsImg, tol))
                    return r;
            }

            return null;
        }

        private static bool IsPointNearPolyline(PointF p, List<PointF> pts, float tol)
        {
            float tol2 = tol * tol;
            for (int i = 0; i < pts.Count - 1; i++)
            {
                if (DistanceToSegmentSquared(p, pts[i], pts[i + 1]) <= tol2)
                    return true;
            }
            return false;
        }

        private static float DistanceToSegmentSquared(PointF p, PointF a, PointF b)
        {
            float dx = b.X - a.X;
            float dy = b.Y - a.Y;
            if (dx == 0 && dy == 0)
                return (p.X - a.X) * (p.X - a.X) + (p.Y - a.Y) * (p.Y - a.Y);

            float t = ((p.X - a.X) * dx + (p.Y - a.Y) * dy) / (dx * dx + dy * dy);
            t = Math.Max(0, Math.Min(1, t));

            float x = a.X + t * dx;
            float y = a.Y + t * dy;

            float px = p.X - x;
            float py = p.Y - y;
            return px * px + py * py;
        }
    }
}
