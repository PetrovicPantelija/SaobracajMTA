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

namespace Saobracaj.Sifarnici
{
    public partial class PartnerjiPregled : Form
    {
        public PartnerjiPregled()
        {
            InitializeComponent();
        }
        private void RefreshGridControl()
        {
            var select = " Select PaSifra, Rtrim(PaNaziv) as Naziv, PaUlicaHisnaSt as Ulica , PaKraj as Grad, PaDelDrzave as Drzava, PaPostnaSt as Posta," +
                " PaSifDrzave as DrzavaID, PaTelefon1 as Telefon, PaZiroRac as TekRacun,  PaOpomba as Napomena, PaDMatSt as Maticni, PaEMail as EMaill," +
                " PaEMatSt1 as PIB, Rtrim(UIC) as UIC, Prevoznik, " +
               " Posiljalac, " +
              " Primalac,  " +
              "  Brodar  , Vlasnik , Spediter , Platilac , Organizator, NalogodavacCH, UvoznikCH, UICDrzava,TR2, Faks, PomIzvoznik," +
              "  Logisticar,Kamioner,AgentBrodara,Buyer,Supplier,WayOfSale,Currency, FREC, DrumskiPrevoz, ERPID from Partnerji order by PaSifra desc ";


            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl1.DataSource = ds.Tables[0];
            gridGroupingControl1.ShowGroupDropArea = true;
            this.gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;

            foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }

            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            dynamicFilter.WireGrid(this.gridGroupingControl1);



        }

        private void PartnerjiPregled_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshGridControl();
        }
    }
}
