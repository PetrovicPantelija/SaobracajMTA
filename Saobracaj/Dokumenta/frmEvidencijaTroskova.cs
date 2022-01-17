using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Dokumenta
{
    public partial class frmEvidencijaTroskova : Form
    {
        public frmEvidencijaTroskova()
        {
            InitializeComponent();
        }

        private void frmEvidencijaTroskova_Load(object sender, EventArgs e)
        {
            var select = "Select ID,RegBr [Registarski broj],Marka,Model,NFaSifPart [SifraPartnera], PaNaziv [Partner],NfaZnesOrig  [Ukupna vrednost Fakture], NFaValutaCeneOrig [Valuta] From AutomobiliEvidencijaTroskovaIzUF";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn col0 = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn col1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Registarski broj";
            dataGridView1.Columns[1].Width = 95;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Marka";
            dataGridView1.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Model";
            dataGridView1.Columns[3].Width = 90;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "SifraPartnera";
            dataGridView1.Columns[4].Width = 90;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Partner";
            dataGridView1.Columns[5].Width = 250;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Ukupna Vrednost Fakture";
            dataGridView1.Columns[6].Width = 110;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Valuta";
            dataGridView1.Columns[7].Width = 50;

        }
    }
}
