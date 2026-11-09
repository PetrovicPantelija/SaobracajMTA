using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Saobracaj.RNI
{
    // Opšti izvoz: zapisi iz grida (posle filtera) sa poljima koja je korisnik izabrao, u novi Excel fajl.
    // Za razliku od izveštaja brodara, zapisi se ne označavaju kao izvezeni.
    public static class TerminalPrivOpstiIzvoz
    {
        public static string PredlogImena()
        {
            return "Terminal privremeni " + DateTime.Now.ToString("yyyy-MM-dd HH-mm", CultureInfo.InvariantCulture) + ".xlsx";
        }

        // naslovi = zaglavlja kolona; redovi = vrednosti istim redom kao naslovi
        public static void SnimiExcel(IList<string> naslovi, IList<object[]> redovi, string putanja)
        {
            using (ExcelEngine engine = new ExcelEngine())
            {
                IApplication app = engine.Excel;
                app.DefaultVersion = ExcelVersion.Xlsx;
                IWorkbook wb = app.Workbooks.Create(1);
                IWorksheet ws = wb.Worksheets[0];
                ws.Name = "Terminal privremeni";

                for (int k = 0; k < naslovi.Count; k++)
                    ws.Range[1, k + 1].Text = naslovi[k];
                IRange zaglavlje = ws.Range[1, 1, 1, naslovi.Count];
                zaglavlje.CellStyle.Font.Bold = true;

                // datumi i brojevi ostaju datumi i brojevi (sortiranje i filter u Excel-u), ostalo je tekst
                // da Excel ne pretvara vrednosti (broj kontejnera, booking, plomba)
                for (int r = 0; r < redovi.Count; r++)
                {
                    for (int k = 0; k < naslovi.Count; k++)
                    {
                        object v = redovi[r][k];
                        if (v == null || v == DBNull.Value)
                            continue;

                        IRange celija = ws.Range[r + 2, k + 1];
                        if (v is DateTime datum)
                        {
                            celija.DateTime = datum;
                            celija.NumberFormat = "dd.mm.yyyy hh:mm";
                        }
                        else if (v is int || v is long || v is short || v is decimal || v is double || v is float)
                            celija.Number = Convert.ToDouble(v);
                        else
                            celija.Text = Convert.ToString(v);
                    }
                }

                ws.UsedRange.AutofitColumns();
                ws.AutoFilters.FilterRange = ws.Range[1, 1, redovi.Count + 1, naslovi.Count];
                wb.SaveAs(putanja);
                wb.Close();
            }
        }
    }
}
