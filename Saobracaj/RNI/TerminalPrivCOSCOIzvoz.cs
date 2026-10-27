using Syncfusion.XlsIO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Saobracaj.RNI
{
    // Izvoz pokreta COSCO kontejnera (tabela TerminalPrivCOSCO) u Excel. Izvoze se zapisi koji još nisu izvezeni
    // (Active <> 1); posle uspešnog snimanja fajla oni dobijaju Active = 1 i DatumIzvoza.
    public class TerminalPrivCOSCOIzvoz
    {
        // Kolone fajla, redom; naziv kolone u Excel-u je isti kao u tabeli
        public static readonly string[] Kolone =
        {
            "Port", "Container", "Type", "Size", "Full/Empty", "Document number", "1st MOVE", "DATE OF THE MOVE"
        };

        private readonly string connectionString;

        public TerminalPrivCOSCOIzvoz(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Zapisi za izvoz, redom upisa
        public DataTable UcitajNeizvezene()
        {
            var sql = "SELECT COSCOID";
            foreach (string k in Kolone)
                sql += ", [" + k + "]";
            sql += " FROM dbo.TerminalPrivCOSCO WHERE Active <> 1 ORDER BY COSCOID";

            var tabela = new DataTable();
            using (var da = new SqlDataAdapter(sql, connectionString))
                da.Fill(tabela);
            return tabela;
        }

        public static string PredlogImena()
        {
            return "COSCO " + DateTime.Now.ToString("yyyy-MM-dd HH-mm", CultureInfo.InvariantCulture) + ".xlsx";
        }

        // Snima fajl sa zapisima iz tabele, pa ih označava kao izvezene. Vraća broj izvezenih zapisa.
        public int Izvezi(DataTable zapisi, string putanja)
        {
            SnimiExcel(zapisi, putanja);
            OznaciIzvezene(zapisi);
            return zapisi.Rows.Count;
        }

        private static void SnimiExcel(DataTable zapisi, string putanja)
        {
            using (ExcelEngine engine = new ExcelEngine())
            {
                IApplication app = engine.Excel;
                app.DefaultVersion = ExcelVersion.Xlsx;
                IWorkbook wb = app.Workbooks.Create(1);
                IWorksheet ws = wb.Worksheets[0];
                ws.Name = "COSCO";

                for (int k = 0; k < Kolone.Length; k++)
                    ws.Range[1, k + 1].Text = Kolone[k];
                IRange zaglavlje = ws.Range[1, 1, 1, Kolone.Length];
                zaglavlje.CellStyle.Font.Bold = true;

                // sve vrednosti se upisuju kao tekst (08.10.2026., 20'), da ih Excel ne pretvara
                for (int r = 0; r < zapisi.Rows.Count; r++)
                {
                    for (int k = 0; k < Kolone.Length; k++)
                    {
                        object v = zapisi.Rows[r][Kolone[k]];
                        if (v != DBNull.Value && v != null)
                            ws.Range[r + 2, k + 1].Text = Kolone[k] == "Size" ? SaApostrofom(Convert.ToString(v)) : Convert.ToString(v);
                    }
                }

                ws.UsedRange.AutofitColumns();
                wb.SaveAs(putanja);
                wb.Close();
            }
        }

        // Size uvek sa apostrofom (20', 40'); zapisi upisani pre ove izmene su bez njega
        private static string SaApostrofom(string size)
        {
            string s = size.Trim();
            return s == "" || s.EndsWith("'") || s.EndsWith("\u2019") ? s : s + "'";
        }

        private void OznaciIzvezene(DataTable zapisi)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction tr = conn.BeginTransaction())
                using (var cmd = new SqlCommand("UPDATE dbo.TerminalPrivCOSCO SET Active = 1, DatumIzvoza = @DatumIzvoza WHERE COSCOID = @COSCOID", conn, tr))
                {
                    cmd.Parameters.Add("@DatumIzvoza", SqlDbType.DateTime).Value = DateTime.Now;
                    SqlParameter id = cmd.Parameters.Add("@COSCOID", SqlDbType.Int);
                    foreach (DataRow red in zapisi.Rows)
                    {
                        id.Value = Convert.ToInt32(red["COSCOID"]);
                        cmd.ExecuteNonQuery();
                    }
                    tr.Commit();
                }
            }
        }
    }
}
