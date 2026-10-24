using Syncfusion.Grouping;
using Syncfusion.GridHelperClasses;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.TrackModal.Sifarnici
{
    // GridGroupingControl na formama šifarnika hijerarhije (Zone, Lokacije, Objekti, Polja, Pozicije):
    // samo pregled, bira se jedan red, filter kao u Excel-u u zaglavlju kolone i grupisanje prevlačenjem kolone.
    public static class SifarnikGrid
    {
        public static void Podesi(GridGroupingControl grid)
        {
            grid.TableDescriptor.AllowEdit = false;
            grid.TableDescriptor.AllowNew = false;
            grid.TableDescriptor.AllowRemove = false;
            grid.TableOptions.AllowSelection = GridSelectionFlags.None;
            grid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            grid.TableOptions.SelectionBackColor = Color.FromArgb(90, 199, 249);
            grid.TableOptions.SelectionTextColor = Color.White;
            grid.TopLevelGroupOptions.ShowCaption = false;
            grid.ShowGroupDropArea = true;
        }

        // Novi podaci; Excel filter se posle svakog DataSource povezuje iznova (grid pravi kolone ponovo),
        // a prethodno povezivanje se uklanja da se ne gomila
        public static void Napuni(GridGroupingControl grid, DataTable podaci)
        {
            var stariFilter = grid.Tag as GridExcelFilter;
            if (stariFilter != null)
                stariFilter.UnWireGrid(grid);

            Podesi(grid);
            grid.DataSource = podaci;
            foreach (GridColumnDescriptor kolona in grid.TableDescriptor.Columns)
                kolona.AllowFilter = true;

            var filter = new GridExcelFilter();
            filter.WireGrid(grid);
            grid.Tag = filter;
        }

        public static void Kolona(GridGroupingControl grid, string naziv, string naslov, int sirina)
        {
            int i = grid.TableDescriptor.Columns.IndexOf(naziv);
            if (i < 0)
                return;
            GridColumnDescriptor kolona = grid.TableDescriptor.Columns[i];
            kolona.HeaderText = naslov;
            kolona.Width = sirina;
        }

        public static void Sakrij(GridGroupingControl grid, string naziv)
        {
            if (grid.TableDescriptor.VisibleColumns.IndexOf(naziv) >= 0)
                grid.TableDescriptor.VisibleColumns.Remove(naziv);
        }

        // Izabrani red (izabrani zapis ili zapis sa kursorom); null ako ga nema
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
            return zapis == null ? null : zapis.GetValue(kolona);
        }

        public static int IzabraniId(GridGroupingControl grid)
        {
            return HijerarhijaSkladista.CeoBroj(Vrednost(grid, "ID"));
        }
    }
}
