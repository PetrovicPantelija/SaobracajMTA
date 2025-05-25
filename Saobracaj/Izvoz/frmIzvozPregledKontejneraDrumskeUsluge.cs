using Saobracaj.Drumski;
using Saobracaj.Izvoz;
using Saobracaj.Uvoz;
using Syncfusion.GridHelperClasses;
using Syncfusion.Grouping;
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

namespace Saobracaj.Izvoz
{
    public partial class frmIzvozPregledKontejneraDrumskeUsluge: Form
    {
        int nadredjeni = 0;
        int kontejner = 0;
        public frmIzvozPregledKontejneraDrumskeUsluge(int idNadredjeni, int id)
        {
            InitializeComponent();
            InitializeComponent();
            nadredjeni = idNadredjeni;
            kontejner = id;
            RefreshGrid();
        }

        private void RefreshGrid()
        {

            var select = "";


            if (nadredjeni == 0)
            {
                
                select = " IzvozVrstaManipulacije.IDNadredjena as KontejnerID, Izvoz.BrojKontejnera, " +
                    " VrstaManipulacije.ID as ManipulacijaID,VrstaManipulacije.Naziv as ManipulacijaNaziv, " +
                     " OrganizacioneJedinice.Naziv as OrganizacionaJedinica,  " +
                    " RadniNalogDrumski.NalogID, CONVERT(varchar,RadniNalogDrumski.DatumKreiranjaNaloga,104) AS KreiranjeNaloga, StatusVozila.Naziv AS StatusVozila, " +
                    " CONVERT(varchar,RadniNalogDrumski.DatumPromeneStatusa,104) AS PromenaStatusa, Automobili.RegBr  " +
                    " from IzvozVrstaManipulacije " +
                    " Inner join VrstaManipulacije on VrstaManipulacije.ID = IzvozVrstaManipulacije.IDVrstaManipulacije " +
                    " inner join OrganizacioneJedinice on OrganizacioneJedinice.ID = IzvozVrstaManipulacije.OrgJed " +
                    " inner join Izvoz on IzvozVrstaManipulacije.IDNadredjena = Izvoz.ID" +
                    " left join RadniNalogDrumski on Izvoz.ID = RadniNalogDrumski.KontejnerID and IzvozVrstaManipulacije.IDVrstaManipulacije = RadniNalogDrumski.IDVrstaManipulacije" +
                    " left join StatusVozila  ON StatusVozila.ID = RadniNalogDrumski.Status " +
                    " left join Automobili ON RadniNalogDrumski.KamionID = Automobili.ID" +
                    " where  Izvoz.ID = " + kontejner + " and OrganizacioneJedinice.Naziv = 'Drumski prevoz' order by IzvozVrstaManipulacije.ID";

            }
            else
            {
                select = "select  IzvozKonacnaVrstaManipulacije.IDNadredjena as KontejnerID, IzvozKonacna.BrojKontejnera, " +
                    " VrstaManipulacije.ID as ManipulacijaID,VrstaManipulacije.Naziv as ManipulacijaNaziv, " +
                    " OrganizacioneJedinice.Naziv as OrganizacionaJedinica,  " +
                    " RadniNalogDrumski.NalogID, CONVERT(varchar,RadniNalogDrumski.DatumKreiranjaNaloga,104) AS KreiranjeNaloga, StatusVozila.Naziv AS StatusVozila," +
                    " CONVERT(varchar,RadniNalogDrumski.DatumPromeneStatusa,104) AS PromenaStatusa, Automobili.RegBr   " +
                    " from IzvozKonacnaVrstaManipulacije " +
                    " Inner join VrstaManipulacije on VrstaManipulacije.ID = IzvozKonacnaVrstaManipulacije.IDVrstaManipulacije" +
                    " inner join OrganizacioneJedinice on OrganizacioneJedinice.ID = IzvozKonacnaVrstaManipulacije.OrgJed " +
                    " inner join IzvozKonacna on IzvozKonacnaVrstaManipulacije.IDNadredjena = IzvozKonacna.ID  " +
                    " left join RadniNalogDrumski on IzvozKonacna.ID = RadniNalogDrumski.KontejnerID and IzvozKonacnaVrstaManipulacije.IDVrstaManipulacije = RadniNalogDrumski.IDVrstaManipulacije" +
                    " left join StatusVozila  ON StatusVozila.ID = RadniNalogDrumski.Status " +
                    " left join Automobili ON RadniNalogDrumski.KamionID = Automobili.ID" +
                      " where IzvozKonacna.IDNadredjena = " + nadredjeni + " and OrganizacioneJedinice.Naziv = 'Drumski prevoz' order by IzvozKonacnaVrstaManipulacije.ID";
            }

            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new System.Data.DataSet();
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
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (this.gridGroupingControl1.Table.SelectedRecords.Count > 0)
            {
                InsertUvoz isu = new InsertUvoz();
                int uvoz = 0;

                List<(int kontejnerID, int manipulacijaID)> stavke = new List<(int, int)>();

                foreach (SelectedRecord selectedRecord in this.gridGroupingControl1.Table.SelectedRecords)
                {
                    object nalogIdValue = selectedRecord.Record.GetValue("NalogID");
                    if (nalogIdValue != null && nalogIdValue.ToString() != "" && nalogIdValue.ToString() != "0")
                    {
                        MessageBox.Show("Radni nalog se može kreirati samo za stavke koje još nisu dodeljene nalogu.",
                                   "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    int kontejnerID = Convert.ToInt32(selectedRecord.Record.GetValue("KontejnerID"));
                    int manipulacijaID = Convert.ToInt32(selectedRecord.Record.GetValue("ManipulacijaID"));
                    stavke.Add((kontejnerID, manipulacijaID));
                }

                isu.KreirajRadniNalogDrumski(stavke, uvoz);
                RefreshGrid();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Drumski.frmPregledNalogaDrumski pnd = new Drumski.frmPregledNalogaDrumski();
            pnd.Show();
        }
    }
}
