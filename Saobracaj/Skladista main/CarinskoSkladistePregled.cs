using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Saobracaj.Skladista;
using Saobracaj.Skladista_main.Dokumenta;
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
    public partial class CarinskoSkladistePregled : Form
    {
        string Tip;
        public CarinskoSkladistePregled(string tip)
        {
            InitializeComponent();
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
                "WHere TipRN='" + Tip + "' and Formiran=1 order by ID desc";


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
        int ID;
        string Vrsta;
        string Ulaz;
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

        private void button24_Click(object sender, EventArgs e)
        {
            if (gridGroupingControl1.Table.CurrentRecord != null)
            {
                ID = Convert.ToInt32(gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString());
                Vrsta = gridGroupingControl1.Table.CurrentRecord.GetValue("VrstaRN").ToString();
                Tip = gridGroupingControl1.Table.CurrentRecord.GetValue("TipRN").ToString();
                var main = this.TopLevelControl as NewMain;
                if (main == null) return;

                
                using(SqlConnection conn=new SqlConnection(connection))
                {
                    using(SqlCommand cmd=new SqlCommand("Select OJIzdavanja From RadniNalogInterni Where TipRN='RN20' and BrojRN=" + ID, conn))
                    {
                        conn.Open();
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                if (Convert.ToInt32(reader[0].ToString()) == 1)
                                {
                                    Ulaz = "Uvoz";
                                }
                                if (Convert.ToInt32(reader[0].ToString()) == 2)
                                {
                                    Ulaz = "Izvoz";
                                }
                                if (Convert.ToInt32(reader[0].ToString()) == 4)
                                {
                                    Ulaz = "Direktni";
                                }
                            }
                        }
                    }
                }

                if (Tip == "Prijem")
                {
                    main.OtvoriFormuBezPrava(() => new Prijem(ID,Ulaz, Vrsta));
                }
                if (Tip == "Otprema")
                {
                    main.OtvoriFormuBezPrava(() => new Otprema(ID, Ulaz, Vrsta));
                }
            }
        }

        private void btnViljuskarista_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

                main.OtvoriFormuBezPrava(() => new NaloziViljuskaristiPregled());
        }
    }
}
