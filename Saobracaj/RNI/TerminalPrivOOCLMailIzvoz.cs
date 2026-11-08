using Syncfusion.XlsIO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Saobracaj.RNI
{
    // Izveštaj OOCL MAIL: isti pokreti kao OOCL EDI (tabela TerminalPrivOOCL), sa drugim kolonama. Izvoz se pamti
    // nezavisno od OOCL EDI: izvoze se zapisi sa ActiveMail <> 1; posle uspešnog snimanja fajla oni dobijaju
    // ActiveMail = 1 i DatumIzvozaMail, a Active i DatumIzvoza (OOCL EDI) se ne menjaju.
    public class TerminalPrivOOCLMailIzvoz
    {
        // Kolone fajla, redom: naziv kolone u Excel-u i kolona u tabeli TerminalPrivOOCL
        private static readonly string[,] Kolone =
        {
            { "Container", "CONTAINER NUMBER" },
            { "Type", "Type" },
            { "Size", "Size" },
            { "Full/Empty", "FULL/EMPTY" },
            { "Booking number", "BOOKING NUMBER" },
            { "1st MOVE", "MOVE TYPE" },
            { "DATE OF THE MOVE", "MOVE DATE/TIME" }
        };

        private readonly string connectionString;

        public TerminalPrivOOCLMailIzvoz(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Zapisi za izvoz, redom upisa
        public DataTable UcitajNeizvezene()
        {
            var sql = "SELECT OOCLID";
            for (int k = 0; k < Kolone.GetLength(0); k++)
                sql += ", [" + Kolone[k, 1] + "]";
            sql += " FROM dbo.TerminalPrivOOCL WHERE ActiveMail <> 1 ORDER BY OOCLID";

            var tabela = new DataTable();
            using (var da = new SqlDataAdapter(sql, connectionString))
                da.Fill(tabela);
            return tabela;
        }

        public static string PredlogImena()
        {
            return "OOCL MAIL " + DateTime.Now.ToString("yyyy-MM-dd HH-mm", CultureInfo.InvariantCulture) + ".xlsx";
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
            int brojKolona = Kolone.GetLength(0);
            using (ExcelEngine engine = new ExcelEngine())
            {
                IApplication app = engine.Excel;
                app.DefaultVersion = ExcelVersion.Xlsx;
                IWorkbook wb = app.Workbooks.Create(1);
                IWorksheet ws = wb.Worksheets[0];
                ws.Name = "OOCL MAIL";

                for (int k = 0; k < brojKolona; k++)
                    ws.Range[1, k + 1].Text = Kolone[k, 0];
                IRange zaglavlje = ws.Range[1, 1, 1, brojKolona];
                zaglavlje.CellStyle.Font.Bold = true;

                // sve vrednosti se upisuju kao tekst (08.10.2026. 22:54, 20'), da ih Excel ne pretvara
                for (int r = 0; r < zapisi.Rows.Count; r++)
                {
                    for (int k = 0; k < brojKolona; k++)
                    {
                        object v = zapisi.Rows[r][Kolone[k, 1]];
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
                using (var cmd = new SqlCommand("UPDATE dbo.TerminalPrivOOCL SET ActiveMail = 1, DatumIzvozaMail = @DatumIzvoza WHERE OOCLID = @OOCLID", conn, tr))
                {
                    cmd.Parameters.Add("@DatumIzvoza", SqlDbType.DateTime).Value = DateTime.Now;
                    SqlParameter id = cmd.Parameters.Add("@OOCLID", SqlDbType.Int);
                    foreach (DataRow red in zapisi.Rows)
                    {
                        id.Value = Convert.ToInt32(red["OOCLID"]);
                        cmd.ExecuteNonQuery();
                    }
                    tr.Commit();
                }
            }
        }
    }
}
