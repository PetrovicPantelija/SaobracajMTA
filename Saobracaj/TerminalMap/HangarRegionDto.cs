using System.Collections.Generic;

namespace Saobracaj.TerminalMap
{
    // Pravougaonici (ako ti nekad zatreba opet za JSON)
    public sealed class HangarRegionDto
    {
        public string ID { get; set; } = "";
        public float X { get; set; }
        public float Y { get; set; }
        public float W { get; set; }
        public float H { get; set; }
    }

    // ====== OVERLAY (LINIJE + SETTINGS u istom JSON-u) ======

    public sealed class TerminalOverlaySettings
    {
        public bool ShowBaseImage { get; set; } = true;
    }

    public sealed class PointDto
    {
        public float X { get; set; }
        public float Y { get; set; }
    }

    public sealed class PolylineDto
    {
        public string ID { get; set; } = "";
        public int ColorArgb { get; set; }
        public List<PointDto> Points { get; set; } = new List<PointDto>();
    }

    public sealed class TerminalOverlayFile
    {
        public TerminalOverlaySettings Settings { get; set; } = new TerminalOverlaySettings();

        public List<PolylineDto> Lines { get; set; } = new List<PolylineDto>();

        // NOVO: boje za pravougaonike, ključ je SkNaziv/ID regiona
        public Dictionary<string, int> RectColors { get; set; } = new Dictionary<string, int>();
    }
}
