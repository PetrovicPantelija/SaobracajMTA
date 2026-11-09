using Microsoft.Ajax.Utilities;
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
    public partial class SkladistaRadniNalozi : Form
    {
        string RN;
        public SkladistaRadniNalozi()
        {
            InitializeComponent();
        }
        private void RefreshGV()
        {
            RN = "Radni Nalozi";

            gridGroupingControl1.DataSource = null;

            var select = @"select RNCarinskoSkladiste.ID as ID,TipRN,RNCarinskoSkladiste.Status as Status,Aktivan,Formiran,CarinskoSkladiste,MagacinskiBroj,
RTrim(p1.PaNaziv) as Nalogodavac,RTRIM(p2.PaNaziv) as VlasnikRobe,RTrim(MestaUtovara.Naziv) as MestoIstovara
from RNCarinskoSkladiste
inner join Partnerji as p1 on RNCarinskoSkladiste.Nalogodavac=p1.PaSifra
inner join Partnerji as  p2 on RNCarinskoSkladiste.VlasnikRobe=p2.PaSifra
inner join MestaUtovara on RNCarinskoSkladiste.MestoIstovara=MestaUtovara.ID
order by ID desc";

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

        private void SkladistaRadniNalozi_Load(object sender, EventArgs e)
        {
            RefreshGV();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            RefreshGV();
        }

        private void btnRnRukovaoci_Click(object sender, EventArgs e)
        {
            RN = "Rukovaoci";

            gridGroupingControl1.DataSource = null;

            var select = @"Select ID,Prijemnica,Status,Uradjen,(RTRIM(DeIme)+' '+RTRIM(DePriimek)) as Rukovalac
from RNCarinskoSkladisteRukovalac
inner join Delavci on RnCarinskoSkladisteRukovalac.Rukovalac=Delavci.DeSifra
Where Status='OD'
group by ID,Prijemnica,Status,Uradjen,(RTRIM(DeIme)+' '+RTRIM(DePriimek))
order by ID desc";

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
    }
}
