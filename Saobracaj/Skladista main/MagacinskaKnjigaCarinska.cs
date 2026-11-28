using Syncfusion.GridHelperClasses;
using Syncfusion.Windows.Forms.Grid.Grouping;
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

namespace Saobracaj.Skladista_main
{
    public partial class MagacinskaKnjigaCarinska : Form
    {
        public MagacinskaKnjigaCarinska()
        {
            InitializeComponent();
            PrimeniSava();   // redizajn Sava (MagacinskaKnjigaCarinska.Sava.cs)
        }
        private void FillGV()
        {
            // Kartica prometa: ulaz i izlaz iz dokumenata (MagacinskaKnjigaCarinska.Kartica.cs); view MagacinskaKnjigaCarinska ima samo prijem
            Text = "Kartica prometa";
            var s_connection = Sifarnici.frmLogovanje.connectionString;
            DataTable kartica;
            try
            {
                kartica = KarticaPrometa(s_connection);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Kartica prometa nije učitana: " + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            gridGroupingControl1.DataSource = kartica;
            PodesiKoloneKartice();
            gridGroupingControl1.ShowGroupDropArea = true;
            this.gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;
            foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }
            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            //Wiring the Dynamic Filter to GridGroupingControl
            dynamicFilter.WireGrid(this.gridGroupingControl1);

            GridExcelFilter gridExcelFilter = new GridExcelFilter();

            //Wiring GridExcelFilter to GridGroupingControl
            gridExcelFilter.WireGrid(this.gridGroupingControl1);
        }

        private void MagacinskaKnjigaCarinska_Load(object sender, EventArgs e)
        {
            FillGV();
        }
    }
}
