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

namespace Saobracaj.Servis
{
    // Kolona u tabeli modula Servis: naziv iz upita, naslov rečima, širina i format
    public class SERKolona
    {
        public string Naziv;
        public string Naslov;
        public int Sirina;
        public string Format;      // "datum", "iznos", "kolicina", "broj" ili null
        public SERKolona(string naziv, string naslov, int sirina, string format = null)
        {
            Naziv = naziv;
            Naslov = naslov;
            Sirina = sirina;
            Format = format;
        }
    }

    // Syncfusion GridGroupingControl za modul Servis: samo pregled, jedan izabran red, filter kao u Excel-u,
    // prikazuju se samo zadate kolone, statusi se boje kroz QueryCellStyleInfo.
    public static class SERGrid
    {
        private static readonly CultureInfo Srpski = CultureInfo.GetCultureInfo("sr-Latn-RS");

        private class Stanje
        {
            public GridExcelFilter Filter;
            public List<SERKolona> Kolone;
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
            // ćelija se ne aktivira kao polje za unos, pa dupli klik stiže do tabele (DuplikKlik)
            grid.TableModel.Options.ActivateCurrentCellBehavior = GridCellActivateAction.None;
            ThemeHelper.StyleGrid(grid);
        }

        public static void Kolone(GridGroupingControl grid, params SERKolona[] kolone)
        {
            ((Stanje)grid.Tag).Kolone = kolone == null ? null : new List<SERKolona>(kolone);
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
                foreach (SERKolona k in stanje.Kolone)
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

        public static int IzabraniId(GridGroupingControl grid, string kolona = "ID")
        {
            return SERBaza.CeoBroj(Vrednost(grid, kolona));
        }

        // Posle ponovnog punjenja tabele: izabere red sa zadatom vrednošću kolone (npr. isti servis kao pre osvežavanja)
        public static bool Izaberi(GridGroupingControl grid, string kolona, int vrednost)
        {
            if (grid.Table == null || vrednost <= 0)
                return false;
            foreach (Record zapis in grid.Table.Records)
                if (SERBaza.CeoBroj(zapis.GetValue(kolona)) == vrednost)
                {
                    grid.Table.SelectedRecords.Clear();
                    grid.Table.SelectedRecords.Add(zapis);
                    zapis.SetCurrent();
                    return true;
                }
            return false;
        }

        public static int BrojRedova(GridGroupingControl grid)
        {
            return grid.Table?.FilteredRecords.Count ?? 0;
        }

        public static void DuplikKlik(GridGroupingControl grid, Action akcija)
        {
            // dupli klik na samoj tabeli (ne u kontroli ćelije); kliknuti red postaje izabran, pa akcija radi baš sa njim
            grid.TableControl.MouseDoubleClick += (s, e) =>
            {
                if (e.Button != MouseButtons.Left)
                    return;
                if (!grid.TableControl.PointToRowCol(e.Location, out int red, out int kolona) || red < 0 || kolona < 0)
                    return;
                GridTableCellStyleInfo stil = grid.TableControl.GetTableViewStyleInfo(red, kolona);
                GridTableCellType tip = stil.TableCellIdentity.TableCellType;
                if (tip != GridTableCellType.RecordFieldCell && tip != GridTableCellType.AlternateRecordFieldCell)
                    return;
                Record zapis = stil.TableCellIdentity.DisplayElement?.GetRecord();
                if (zapis == null)
                    return;
                grid.Table.SelectedRecords.Clear();
                grid.Table.SelectedRecords.Add(zapis);
                zapis.SetCurrent();
                akcija();
            };
        }

        // Pun tekst ćelije u tooltipu kad je tekst duži od kolone (zadate kolone)
        public static void TekstUTooltipu(GridGroupingControl grid, params string[] kolone)
        {
            var tip = new ToolTip { AutoPopDelay = 20000, InitialDelay = 400 };
            string prikazan = null;
            grid.TableControl.MouseMove += (s, e) =>
            {
                string tekst = null;
                if (grid.TableControl.PointToRowCol(e.Location, out int red, out int kolona) && red >= 0 && kolona >= 0)
                {
                    GridTableCellStyleInfo stil = grid.TableControl.GetTableViewStyleInfo(red, kolona);
                    GridTableCellType vrsta = stil.TableCellIdentity.TableCellType;
                    string naziv = stil.TableCellIdentity.Column?.Name;
                    Record zapis = vrsta == GridTableCellType.RecordFieldCell || vrsta == GridTableCellType.AlternateRecordFieldCell
                        ? stil.TableCellIdentity.DisplayElement?.GetRecord() : null;
                    if (zapis != null && naziv != null && Array.IndexOf(kolone, naziv) >= 0)
                        tekst = Convert.ToString(zapis.GetValue(naziv));
                }
                if (tekst == prikazan)
                    return;
                prikazan = tekst;
                if (string.IsNullOrEmpty(tekst))
                    tip.Hide(grid.TableControl);
                else
                    tip.Show(tekst, grid.TableControl, e.X + 16, e.Y + 22);
            };
            grid.TableControl.MouseLeave += (s, e) => { prikazan = null; tip.Hide(grid.TableControl); };
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

        // Boja teksta statusa po oblasti iz SERStatus (status se uvek prikazuje i tekstom)
        public static Color? BojaStatusa(string oblast, int status)
        {
            switch (oblast)
            {
                case "RadniNalog":
                    return status == 3 ? SERStil.StatusIsteklo : status == 4 ? SERStil.StatusVazi : status == 5 ? SavaTheme.Label : (Color?)null;
                case "Rezervacija":
                    return status == 1 ? SERStil.StatusInfo : status == 2 ? SERStil.StatusVazi : status == 3 ? SERStil.StatusIsteklo : SavaTheme.Label;
                case "Narudzbenica":
                    return status == 2 ? SERStil.StatusIstice : status == 3 || status == 4 ? SERStil.StatusInfo
                         : status == 5 ? SERStil.StatusVazi : status == 6 ? SavaTheme.Label : (Color?)null;
                case "Popis":
                    return status == 2 ? SERStil.StatusIstice : status == 3 ? SERStil.StatusVazi : status == 4 ? SavaTheme.Label : (Color?)null;
                case "Korekcija":
                    return status == 1 ? SERStil.StatusIstice : status == 2 ? SERStil.StatusVazi : SavaTheme.Label;
                default:
                    return null;
            }
        }
    }
}
