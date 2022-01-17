using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;

using Microsoft.Reporting.WinForms;

namespace Saobracaj.Dokumenta
{
    public partial class frmAutomobiliKvarovi : Form
    {
        public frmAutomobiliKvarovi()
        {
            InitializeComponent();
        }

        private void frmAutomobiliKvarovi_Load(object sender, EventArgs e)
        {
            var select = "  SELECT     Automobili.RegBr, EvidencijaKvarovaAuto.Automobil, KvaroviAuto.Naziv as KVar, GrupaKvarovaAuto.Naziv AS Grupa, Delavci.DeSifra, Delavci.DePriimek, Delavci.DeIme, " +
                   "   EvidencijaKvarovaAuto.Promenio, EvidencijaKvarovaAuto.Kritican, EvidencijaKvarovaAuto.DatumPrijave, EvidencijaKvarovaAuto.Prijavio, " +
                   "   EvidencijaKvarovaAuto.Kvar, EvidencijaKvarovaAuto.DatumPromene, EvidencijaKvarovaAuto.Napomena, EvidencijaKvarovaAuto.AutomobilId "+
                   "   FROM         EvidencijaKvarovaAuto INNER JOIN " +
                   "   Automobili ON EvidencijaKvarovaAuto.AutomobilID = Automobili.ID INNER JOIN " +
                   "   KvaroviAuto ON EvidencijaKvarovaAuto.Kvar = KvaroviAuto.ID INNER JOIN " +
                   "   GrupaKvarovaAuto ON KvaroviAuto.GrupaKvarovaID = GrupaKvarovaAuto.ID INNER JOIN " +
                   "   Delavci ON EvidencijaKvarovaAuto.Prijavio = Delavci.DeSifra ";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
        }
    }
}
