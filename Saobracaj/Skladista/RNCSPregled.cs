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
        string Tip;
        public RNCSPregled(string tip)
        {
            InitializeComponent();
            Tip = tip;
            RefreshGV();
        }


        private void RefreshGV()
        {
            if (Tip == "Carinsko")
            {
                var select = @"Select RnCarinskoSkladiste.ID as ID,TipRN,RNCarinskoSkladiste.Status as Status,BrojKontejnera,CarinskoSkladiste,MagacinskiBroj,p1.PaNaziv as Nalogodavac,
p2.PaNaziv as VlasnikRobe,VrstaRobe,NacinPakovanja,OstalaSkladista,
PIB,VrstePrevoznogSredstva.Naziv as VrstaPrevoznogSredstva,VrstaVozila.Naziv as VrstaKamiona,Vozilo,Vozac,BrojLK,BrojTelefona,Carinarnice.CINaziv as OdredisnaCarinarnica,
p3.PaNaziv as Spediter,KontakOsobaSpeditera,MestaUtovara.Naziv as MestoIstovara,Adresa,KontaktOsobaIstovar,PlaniraniDatum,PlaniraniDatum2,PosebniUslovi,Napomena,Aktivan,Formiran
From RNCarinskoSkladiste
Inner join Partnerji as p1 on RNCarinskoSkladiste.Nalogodavac=p1.PaSifra
inner join Partnerji as p2 on RNCarinskoSkladiste.VlasnikRobe=p2.PaSifra
inner join Partnerji as p3 on RNCarinskoSkladiste.Spediter=p3.PaSifra
inner join VrstePrevoznogSredstva on RNCarinskoSkladiste.VrstaPrevoznogSredstva=VrstePrevoznogSredstva.ID
inner join VrstaVozila on RNCarinskoSkladiste.VrstaKamiona=VrstaVozila.id
inner join Carinarnice on RNCarinskoSkladiste.OdredisnaCarinarnica=Carinarnice.ID
inner join MestaUtovara on RNCarinskoSkladiste.MestoIstovara=MestaUtovara.ID
Where TipRN='Carinsko' order by RNCarinskoSkladiste.ID desc";


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

        private void button23_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                RnCSnovi.Name,
                () => new RnCS(Tip)
            );
        }
        int ID;
        string Status;
        private void button24_Click(object sender, EventArgs e)
        {
            if (gridGroupingControl1.Table.CurrentRecord != null)
            {
               ID = Convert.ToInt32(gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString());
               Status= gridGroupingControl1.Table.CurrentRecord.GetValue("Status").ToString();

                var main = this.TopLevelControl as NewMain;
                if (main == null) return;

                main.OtvoriFormuSaPravom(
                    RnCSnovi.Name,
                    () => new RnCS(Tip,ID,Status)
                );
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            RefreshGV();
        }
    }
}
