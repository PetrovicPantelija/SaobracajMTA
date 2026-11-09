using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saobracaj.TerminalMap
{
    public enum ShapeType { Rect, Polyline }

    public sealed class HangarRegion
    {
        public string ID { get; set; } = "";

        // za pravougaonik
        public RectangleF RectImg { get; set; }

        // za linije
        public ShapeType Shape { get; set; } = ShapeType.Rect;
        public List<PointF> PointsImg { get; set; } = new List<PointF>();

        // boja (ARGB int se lako snima u JSON)
        public int ColorArgb { get; set; } = Color.FromArgb(60, Color.Lime).ToArgb();
    }
}
