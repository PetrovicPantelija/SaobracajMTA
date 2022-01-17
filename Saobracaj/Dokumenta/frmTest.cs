using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;


namespace Saobracaj.Dokumenta
{
    public partial class frmTest : Form
    {
        public frmTest()
        {
            InitializeComponent();
        }

        private void frmTest_Load(object sender, EventArgs e)
        {
            GMapControl gMapControl1 = new GMapControl();
            //GoogleMap
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.Position = new PointLatLng(44.787197, 20.457273);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = 8;

            GMapOverlay markersoverlay = new GMapOverlay("markers");
            GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(44.76742127130139, 20.360970533299504), GMarkerGoogleType.gray_small);
            markersoverlay.Markers.Add(marker);
            marker.ToolTipMode = MarkerTooltipMode.Always;
            marker.ToolTipText = "Najave \n ENA ID 1111111 \n Standard ID 22222";

            GMarkerGoogle marker1 = new GMarkerGoogle(new PointLatLng(45.00390142763182, 19.825094711126287), GMarkerGoogleType.gray_small);
            markersoverlay.Markers.Add(marker1);
            marker1.ToolTipMode = MarkerTooltipMode.Always;
            marker1.ToolTipText = "Najave \n TCL ID 3333 \n Standard ID 44444 \n ";

            
            PointLatLng start;
            PointLatLng end;

            start = marker.Position;
            end = marker1.Position;
            /*
            PointLatLng startp = new PointLatLng(-25.974134, 32.593042);
            PointLatLng endp = new PointLatLng(-25.959048, 32.592827);
            GoogleMapProvider.Instance.ApiKey = "AIzaSyBaqjeBIXNp9HkuihYYH64wjnppzc6MRWs";
            MapRoute route = GoogleMapProvider.Instance.GetRoute(startp, endp, false, false, 8);
            GMapRoute r = new GMapRoute(route.Points, "Myroutes");
           // GMapOverlay routesOverlay = new GMapOverlay("Myroutes");
           // routesOverlay.Routes.Add(r);
           // gmap.Overlays.Add(routesOverlay);
            r.Stroke.Width = 2;
            r.Stroke.Color = Color.SeaGreen;
            markersoverlay.Routes.Add(r);
           */
           



            gMapControl1.Overlays.Add(markersoverlay);

            GMapOverlay polyOverlay = new GMapOverlay("polygons");
            List<PointLatLng> points =  new List<PointLatLng>();
            points.Add(new PointLatLng(44.76742127130139, 20.360970533299504));
            points.Add(new PointLatLng(45.00390142763182, 19.825094711126287));
           // points.Add(new PointLatLng(45.00390142763182, 18.825094711126287));
            // points.Add(new PointLatLng(-25.968134, 32.591647));
            // points.Add(new PointLatLng(-25.971684, 32.589759));
            GMapPolygon polygon = new GMapPolygon(points, "mypolygon");
            polygon.Fill = new SolidBrush(Color.FromArgb(50, Color.Red));
            polygon.Stroke = new Pen(Color.Red, 5);
            polyOverlay.Polygons.Add(polygon);
            gMapControl1.Overlays.Add(polyOverlay);


            // 44,76742127130139, 20,360970533299504
            // 45,00390142763182, 19,825094711126287



            gMapControl1.Location = new Point(85, 0);
           // gMapControl1.AutoSize = true;
          // gMapControl1.Size = new Size(550, 250);
            gMapControl1.Dock = DockStyle.Fill;
            this.Controls.Add(gMapControl1);
           



        }
    }
}
