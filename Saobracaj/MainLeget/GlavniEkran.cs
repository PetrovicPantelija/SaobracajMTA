using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.Windows.Forms.Chart;

namespace Saobracaj.MainLeget
{
    public partial class GlavniEkran : Form
    {
        public GlavniEkran()
        {
            InitializeComponent();
        }

        // Grafikon po brodaru se osvežava dok je ekran prikazan
        private const int OsvezavanjeGrafikonaMs = 60000;
        private Timer tajmerGrafikona;

        private void GlavniEkran_Load(object sender, EventArgs e)
        {
             VratiBrojKontejneraKomercijalnaZona();
             PrikaziKontejnerePoBrodaru();

             tajmerGrafikona = new Timer { Interval = OsvezavanjeGrafikonaMs };
             tajmerGrafikona.Tick += TajmerGrafikona_Tick;
             tajmerGrafikona.Start();
             Disposed += (s, ev) => tajmerGrafikona.Dispose();
        }

        private void TajmerGrafikona_Tick(object sender, EventArgs e)
        {
            // glavni prozor sklanja ekran iz panela (Controls.Clear) bez zatvaranja forme, pa se tada osvežavanje gasi
            if (IsDisposed || Parent == null)
            {
                tajmerGrafikona.Stop();
                return;
            }

            if (Visible)
                PrikaziKontejnerePoBrodaru();
        }

        // Grafikon: broj kontejnera u TerminalPriv (terminal privremeni) po brodaru, stubići od najvećeg ka najmanjem.
        // Tip Bar (vodoravne trake) se ne koristi jer u ovoj verziji Syncfusion-a sa kategorijama crta sve trake iste dužine.
        private void PrikaziKontejnerePoBrodaru()
        {
            const string sql =
                "SELECT ISNULL(NULLIF(LTRIM(RTRIM(BRODAR)), N''), N'(bez brodara)') AS Brodar, COUNT(*) AS Broj " +
                "FROM dbo.TerminalPriv " +
                "GROUP BY ISNULL(NULLIF(LTRIM(RTRIM(BRODAR)), N''), N'(bez brodara)') " +
                "ORDER BY COUNT(*) DESC, Brodar";

            var tabela = new DataTable();
            try
            {
                using (var da = new SqlDataAdapter(sql, Saobracaj.Sifarnici.frmLogovanje.connectionString))
                    da.Fill(tabela);
            }
            catch (Exception ex)
            {
                chartBrodari.Title.Text = "Kontejneri po brodaru - podaci nisu učitani: " + ex.Message;
                return;
            }

            chartBrodari.Series.Clear();
            chartBrodari.PrimaryXAxis.ValueType = ChartValueType.Category;   // pre dodavanja tačaka
            chartBrodari.PrimaryXAxis.LabelRotate = true;
            chartBrodari.PrimaryXAxis.LabelRotateAngle = 45;
            chartBrodari.ShowLegend = false;

            int ukupno = 0;
            var serija = new ChartSeries("Kontejnera", ChartSeriesType.Column);
            serija.SortPoints = false;   // redosled iz upita (po broju), a ne abecedni
            foreach (DataRow red in tabela.Rows)
            {
                int broj = Convert.ToInt32(red["Broj"]);
                ukupno += broj;
                serija.Points.Add(Convert.ToString(red["Brodar"]), broj);
            }
            serija.Style.DisplayText = true;
            serija.Style.TextOrientation = ChartTextOrientation.Up;
            serija.Style.Interior = new Syncfusion.Drawing.BrushInfo(Color.FromArgb(0, 176, 240));

            chartBrodari.Series.Add(serija);
            chartBrodari.Title.Text = "Kontejneri na terminalu po brodaru (ukupno " + ukupno + ")";
        }
        private void VratiBrojKontejneraKomercijalnaZona()
        {
            // Add this method to the form that contains `lblKomercijalnaZona`
            /* test
            const string sql = "SELECT ISNULL(SUM(Kolicina), 0) FROM KontejnerTekuce WHERE Skladiste = 1";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            using (var conn = new SqlConnection(s_connection))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                var result = cmd.ExecuteScalar();
                decimal kolicina = 0;
                if (result != null && result != DBNull.Value)
                    decimal.TryParse(result.ToString(), out kolicina);

                // format as needed, e.g. no decimals:
                lblKomercijalnaZona.Text = kolicina.ToString("N0");
            }
            */
   
        }
    }
}
