using Saobracaj.TerminalMap;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;

public static class RegionStorage
{
    // ====== POSTOJEĆE: pravougaonici u JSON (ako ti zatreba) ======
    public static List<HangarRegion> Load(string path)
    {
        if (!File.Exists(path)) return new List<HangarRegion>();

        var json = File.ReadAllText(path);
        var dto = JsonSerializer.Deserialize<List<HangarRegionDto>>(json) ?? new List<HangarRegionDto>();

        return dto.Select(d => new HangarRegion
        {
            ID = d.ID,
            RectImg = new RectangleF(d.X, d.Y, d.W, d.H),
            Shape = ShapeType.Rect
        }).ToList();
    }

    public static void Save(string path, List<HangarRegion> regions)
    {
        var dto = regions
            .Where(r => r != null && r.Shape == ShapeType.Rect)
            .Select(r => new HangarRegionDto
            {
                ID = r.ID,
                X = r.RectImg.X,
                Y = r.RectImg.Y,
                W = r.RectImg.Width,
                H = r.RectImg.Height
            }).ToList();

        var json = JsonSerializer.Serialize(dto, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(path, json);
    }

    // ====== NOVO: OVERLAY (settings + lines) u ISTOM JSON-u ======
    private static readonly JsonSerializerOptions _opt = new JsonSerializerOptions
    {
        WriteIndented = true
    };

    public static TerminalOverlayFile LoadOverlay(string path)
    {
        try
        {
            if (!File.Exists(path))
                return new TerminalOverlayFile(); // default ShowBaseImage=true

            var json = File.ReadAllText(path);
            if (string.IsNullOrWhiteSpace(json))
                return new TerminalOverlayFile();

            json = json.TrimStart();

            // BACKWARD COMPAT:
            // ako je neko ranije snimao samo [] linije, prihvati to kao "Lines", a settings default ON
            if (json.StartsWith("["))
            {
                var oldLines = JsonSerializer.Deserialize<List<PolylineDto>>(json, _opt) ?? new List<PolylineDto>();
                return new TerminalOverlayFile
                {
                    Settings = new TerminalOverlaySettings { ShowBaseImage = true },
                    Lines = oldLines
                };
            }

            // novi format: { "settings": {...}, "lines": [...] }
            var file = JsonSerializer.Deserialize<TerminalOverlayFile>(json, _opt);
            return file ?? new TerminalOverlayFile();
        }
        catch
        {
            // nemoj rušiti aplikaciju zbog JSON-a
            return new TerminalOverlayFile();
        }
    }

    public static void SaveOverlay(string path, TerminalOverlayFile file)
    {
        if (file == null) file = new TerminalOverlayFile();

        var dir = Path.GetDirectoryName(path);
        if (!string.IsNullOrWhiteSpace(dir))
            Directory.CreateDirectory(dir);

        var json = JsonSerializer.Serialize(file, _opt);
        File.WriteAllText(path, json);
    }

    // pomoćne konverzije DTO <-> HangarRegion (Polyline)
    public static HangarRegion ToHangarRegion(PolylineDto dto)
    {
        var r = new HangarRegion
        {
            ID = dto.ID,
            Shape = ShapeType.Polyline,
            ColorArgb = dto.ColorArgb
        };

        if (dto.Points != null)
        {
            foreach (var p in dto.Points)
                r.PointsImg.Add(new PointF(p.X, p.Y));
        }

        return r;
    }

    public static PolylineDto ToPolylineDto(HangarRegion r)
    {
        return new PolylineDto
        {
            ID = r.ID,
            ColorArgb = r.ColorArgb,
            Points = (r.PointsImg ?? new List<PointF>())
                .Select(p => new PointDto { X = p.X, Y = p.Y })
                .ToList()
        };
    }
}
