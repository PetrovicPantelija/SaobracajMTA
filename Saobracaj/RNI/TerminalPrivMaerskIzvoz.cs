using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Saobracaj.RNI
{
    // Izvoz pokreta Maersk kontejnera (tabela TerminalPrivMaersk) u Excel. Izvoze se zapisi koji još nisu izvezeni
    // (Active <> 1); posle uspešnog snimanja fajla oni dobijaju Active = 1 i DatumIzvoza.
    public class TerminalPrivMaerskIzvoz
    {
        // Kolone fajla, redom; naziv kolone u Excel-u je isti kao u tabeli
        public static readonly string[] Kolone =
        {
            "Terminal", "Date", "Time", "Container", "Move", "FULL/EMPTY", "Act Fore",
            "WGHT", "BKNG No", "RAIL CODE", "Seal", "DAM Y/N", "Comment"
        };

        private readonly string connectionString;

        public TerminalPrivMaerskIzvoz(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Zapisi za izvoz, redom upisa
        public DataTable UcitajNeizvezene()
        {
            var sql = "SELECT MaerskID";
            foreach (string k in Kolone)
                sql += ", [" + k + "]";
            sql += " FROM dbo.TerminalPrivMaersk WHERE Active <> 1 ORDER BY MaerskID";

            var tabela = new DataTable();
            using (var da = new SqlDataAdapter(sql, connectionString))
                da.Fill(tabela);
            return tabela;
        }

        public static string PredlogImena()
        {
            return "Maersk RSSMKTM " + DateTime.Now.ToString("yyyy-MM-dd HH-mm", CultureInfo.InvariantCulture) + ".xlsx";
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
                ws.Name = "Maersk";

                for (int k = 0; k < Kolone.Length; k++)
                    ws.Range[1, k + 1].Text = Kolone[k];
                IRange zaglavlje = ws.Range[1, 1, 1, Kolone.Length];
                zaglavlje.CellStyle.Font.Bold = true;

                // sve vrednosti se upisuju kao tekst (datum 30.09.2026., vreme 08:00), da ih Excel ne pretvara
                for (int r = 0; r < zapisi.Rows.Count; r++)
                {
                    for (int k = 0; k < Kolone.Length; k++)
                    {
                        object v = zapisi.Rows[r][Kolone[k]];
                        if (v != DBNull.Value && v != null)
                            ws.Range[r + 2, k + 1].Text = Convert.ToString(v);
                    }
                }

                ws.UsedRange.AutofitColumns();
                wb.SaveAs(putanja);
                wb.Close();
            }
        }

        private void OznaciIzvezene(DataTable zapisi)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction tr = conn.BeginTransaction())
                using (var cmd = new SqlCommand("UPDATE dbo.TerminalPrivMaersk SET Active = 1, DatumIzvoza = @DatumIzvoza WHERE MaerskID = @MaerskID", conn, tr))
                {
                    cmd.Parameters.Add("@DatumIzvoza", SqlDbType.DateTime).Value = DateTime.Now;
                    SqlParameter id = cmd.Parameters.Add("@MaerskID", SqlDbType.Int);
                    foreach (DataRow red in zapisi.Rows)
                    {
                        id.Value = Convert.ToInt32(red["MaerskID"]);
                        cmd.ExecuteNonQuery();
                    }
                    tr.Commit();
                }
            }
        }
    }
}
