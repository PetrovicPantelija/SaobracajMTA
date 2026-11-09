using Saobracaj.Sifarnici;
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

namespace Saobracaj.Skladista
{
    public partial class RNCSPregled : Form
    {
        string Vrsta;
        string Tip;
        string Korisnik=frmLogovanje.user.ToString();
        public RNCSPregled(string vrsta)
        {
            InitializeComponent();
            Vrsta = vrsta;
        }
        public RNCSPregled(string vrsta, string tip,string korisnik)
        {
            InitializeComponent();
            Vrsta = vrsta;
            Tip = tip;
            VratiRN();
        }
        private void VratiRN()
        {
            var select = "Select ID,RadniNalogSkladista.Datum as Datum,Korisnik,VrstaRN,TipRN,CarinskoSkladiste,RTRIM(p1.PaNaziv) as Nalogodavac,RTrim(p2.PaNaziv) as VlasnikRobe," +
                "OpisPosla,Napomena,Aktivan,Formiran " +
                "from RadniNalogSkladista " +
                "inner join Partnerji as p1 on RadniNalogSkladista.Nalogodavac=p1.PaSifra " +
                "inner join Partnerji as p2 on RadniNalogSkladista.VlasnikRobe=p2.PaSifra " +
                "WHere VrstaRN='"+Vrsta+"' and TipRN='"+Tip+"' and Formiran=0";


            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            // dataGridView1.ReadOnly = true;
            gridGroupingControl1.DataSource = ds.Tables[0];
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
        
        private void button23_Click(object sender, EventArgs e)
        {


            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuBezPrava(() => new RNSkladista(Vrsta, Tip, Korisnik));
        }
        int ID;
        string Status;
        private void button24_Click(object sender, EventArgs e)
        {
            if (gridGroupingControl1.Table.CurrentRecord != null)
            {
               ID = Convert.ToInt32(gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString());
                Vrsta= gridGroupingControl1.Table.CurrentRecord.GetValue("VrstaRN").ToString();
                Tip = gridGroupingControl1.Table.CurrentRecord.GetValue("TipRN").ToString();

                var main = this.TopLevelControl as NewMain;
                if (main == null) return;

                main.OtvoriFormuBezPrava(() => new RNSkladista(ID,Vrsta, Tip, Korisnik));
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            VratiRN();
        }
    }
}
