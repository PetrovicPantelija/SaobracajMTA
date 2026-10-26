using Saobracaj.Theme;
using Syncfusion.Grouping;
using Syncfusion.GridHelperClasses;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Saobracaj.HR
{
    // Kolona u tabeli HR modula: naziv iz upita, naslov rečima, širina i format
    public class HRKolona
    {
        public string Naziv;
        public string Naslov;
        public int Sirina;
        public string Format;      // "datum", "iznos", "kolicina", "broj" ili null
        public HRKolona(string naziv, string naslov, int sirina, string format = null)
        {
            Naziv = naziv;
            Naslov = naslov;
            Sirina = sirina;
            Format = format;
        }
    }

    // Syncfusion GridGroupingControl za HR: samo pregled, jedan izabran red, filter kao u Excel-u,
    // prikazuju se samo zadate kolone, statusi se boje kroz QueryCellStyleInfo.
    public static class HRGrid
    {
        private static readonly CultureInfo Srpski = CultureInfo.GetCultureInfo("sr-Latn-RS");

        private class Stanje
        {
            public GridExcelFilter Filter;
            public List<HRKolona> Kolone;
            public Func<Record, string, Color?> BojaTeksta;
            public Func<Record, Color?> BojaReda;
        }

        public static GridGroupingControl Novi()
        {
            var grid = new GridGroupingControl
            {
                Dock = DockStyle.Fill,
                BackColor = SavaTheme.White,
                BorderStyle = BorderStyle.None,
                ShowGroupDropArea = false
            };
            grid.Tag = new Stanje();
            grid.QueryCellStyleInfo += Grid_QueryCellStyleInfo;
            Podesi(grid);
            return grid;
        }

        private static void Podesi(GridGroupingControl grid)
        {
            grid.TableDescriptor.AllowEdit = false;
            grid.TableDescriptor.AllowNew = false;
            grid.TableDescriptor.AllowRemove = false;
            grid.TableOptions.AllowSelection = GridSelectionFlags.None;
            grid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            grid.TopLevelGroupOptions.ShowCaption = false;
            grid.ShowGroupDropArea = false;
            ThemeHelper.StyleGrid(grid);
        }

        public static void Kolone(GridGroupingControl grid, params HRKolona[] kolone)
        {
            ((Stanje)grid.Tag).Kolone = kolone == null ? null : new List<HRKolona>(kolone);
        }

        public static void BojaTeksta(GridGroupingControl grid, Func<Record, string, Color?> boja)
        {
            ((Stanje)grid.Tag).BojaTeksta = boja;
        }

        public static void BojaReda(GridGroupingControl grid, Func<Record, Color?> boja)
        {
            ((Stanje)grid.Tag).BojaReda = boja;
        }

        // Novi podaci; Excel filter se povezuje iznova jer grid ponovo pravi kolone
        public static void Napuni(GridGroupingControl grid, DataTable podaci)
        {
            var stanje = (Stanje)grid.Tag;
            if (stanje.Filter != null)
                stanje.Filter.UnWireGrid(grid);

            grid.DataSource = podaci;
            Podesi(grid);

            if (stanje.Kolone != null)
            {
                grid.TableDescriptor.VisibleColumns.Clear();
                foreach (HRKolona k in stanje.Kolone)
                {
                    int i = grid.TableDescriptor.Columns.IndexOf(k.Naziv);
                    if (i < 0)
                        continue;
                    GridColumnDescriptor kolona = grid.TableDescriptor.Columns[i];
                    kolona.HeaderText = k.Naslov;
                    kolona.Width = k.Sirina;
                    var izgled = kolona.Appearance.AnyRecordFieldCell;
                    switch (k.Format)
                    {
                        case "datum":
                            izgled.Format = "dd.MM.yyyy";
                            break;
                        case "datumvreme":
                            izgled.Format = "dd.MM.yyyy HH:mm";
                            break;
                        case "iznos":
                            izgled.Format = "#,##0.00";
                            izgled.CultureInfo = Srpski;
                            izgled.HorizontalAlignment = GridHorizontalAlignment.Right;
                            kolona.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                            break;
                        case "kolicina":
                            izgled.Format = "#,##0.###";
                            izgled.CultureInfo = Srpski;
                            izgled.HorizontalAlignment = GridHorizontalAlignment.Right;
                            kolona.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                            break;
                        case "broj":
                            izgled.HorizontalAlignment = GridHorizontalAlignment.Right;
                            kolona.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                            break;
                    }
                    grid.TableDescriptor.VisibleColumns.Add(k.Naziv);
                }
            }

            foreach (GridColumnDescriptor kolona in grid.TableDescriptor.Columns)
                kolona.AllowFilter = true;
            stanje.Filter = new GridExcelFilter();
            stanje.Filter.WireGrid(grid);
            grid.Refresh();
        }

        private static void Grid_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            var grid = sender as GridGroupingControl;
            var stanje = grid?.Tag as Stanje;
            if (stanje == null)
                return;
            GridTableCellType tip = e.TableCellIdentity.TableCellType;
            if (tip != GridTableCellType.RecordFieldCell && tip != GridTableCellType.AlternateRecordFieldCell)
                return;
            Record zapis = e.TableCellIdentity.DisplayElement?.GetRecord();
            if (zapis == null)
                return;
            if (stanje.BojaReda != null)
            {
                Color? pozadina = stanje.BojaReda(zapis);
                if (pozadina.HasValue)
                    e.Style.BackColor = pozadina.Value;
            }
            if (stanje.BojaTeksta != null && e.TableCellIdentity.Column != null)
            {
                Color? boja = stanje.BojaTeksta(zapis, e.TableCellIdentity.Column.Name);
                if (boja.HasValue)
                    e.Style.TextColor = boja.Value;
            }
        }

        public static Record IzabraniZapis(GridGroupingControl grid)
        {
            if (grid.Table == null)
                return null;
            if (grid.Table.SelectedRecords.Count > 0)
                return grid.Table.SelectedRecords[0].Record;
            return grid.Table.CurrentRecord;
        }

        public static object Vrednost(GridGroupingControl grid, string kolona)
        {
            Record zapis = IzabraniZapis(grid);
            return zapis?.GetValue(kolona);
        }

        public static int IzabraniId(GridGroupingControl grid, string kolona = "Sifra")
        {
            return HRBaza.CeoBroj(Vrednost(grid, kolona));
        }

        public static int BrojRedova(GridGroupingControl grid)
        {
            return grid.Table?.FilteredRecords.Count ?? 0;
        }

        public static void DuplikKlik(GridGroupingControl grid, Action akcija)
        {
            grid.TableControlCellDoubleClick += (s, e) =>
            {
                GridTableCellStyleInfo stil = grid.TableControl.GetTableViewStyleInfo(e.Inner.RowIndex, e.Inner.ColIndex);
                GridTableCellType tip = stil.TableCellIdentity.TableCellType;
                if (tip == GridTableCellType.RecordFieldCell || tip == GridTableCellType.AlternateRecordFieldCell)
                    akcija();
            };
        }

        public static void IzvozUExcel(GridGroupingControl grid, string predlogImena)
        {
            using (var sfd = new SaveFileDialog { Filter = "Excel (*.xlsx)|*.xlsx", FileName = predlogImena + ".xlsx" })
            {
                if (sfd.ShowDialog() != DialogResult.OK)
                    return;
                var konverter = new Syncfusion.GroupingGridExcelConverter.GroupingGridExcelConverterControl();
                konverter.GroupingGridToExcel(grid, sfd.FileName, Syncfusion.GridExcelConverter.ConverterOptions.Visible);
                try { System.Diagnostics.Process.Start(sfd.FileName); } catch { }
            }
        }

        public static Color? BojaStatusaZahteva(int status)
        {
            switch (status)
            {
                case 3: return HRStil.StatusIstice;          // Na odobrenju
                case 4: case 9: return HRStil.StatusIsteklo; // Vraćen, Odbijen
                case 5: return HRStil.StatusVazi;            // Odobren
                case 6: case 7: return HRStil.StatusInfo;    // U nabavci, Delimično realizovan
                case 8: case 10: return SavaTheme.Label;     // Realizovan, Storniran
                default: return null;
            }
        }
    }
}
