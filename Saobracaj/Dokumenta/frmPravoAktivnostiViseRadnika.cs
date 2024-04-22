using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Dokumenta
{
    public partial class frmPravoAktivnostiViseRadnika : Form
    {
        public frmPravoAktivnostiViseRadnika()
        {
            InitializeComponent();

        }
        string niz = "";
        public static string code = "frmPravoAktivnostiViseRadnika";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Sifarnici.frmLogovanje.user.ToString();

        private void RefreshDataGrid()
        {
            var select = "  select ID, Naziv from VrstaAktivnosti order by ID";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "Naziv";
            dataGridView2.Columns[1].Width = 300;


        }

        private void RefreshDataGridRAdnici()
        {
            var select = "  select Delavci.DeSifra, (Rtrim(Delavci.DeIme) + ' ' + Rtrim(Delavci.DePriimek)) as Zaposleni, DelovnaMesta.DmNaziv  from Delavci " +
" inner join DelovnaMesta on DeSifDelMes = DelovnaMesta.DmSifra order by Delavci.DeSifra";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView3.Columns[0];
            dataGridView3.Columns[0].HeaderText = "ID";
            dataGridView3.Columns[0].Width = 40;

            DataGridViewColumn column3 = dataGridView3.Columns[1];
            dataGridView3.Columns[1].HeaderText = "Radnik";
            dataGridView3.Columns[1].Width = 200;

            DataGridViewColumn column4 = dataGridView3.Columns[2];
            dataGridView3.Columns[2].HeaderText = "Radno mesto";
            dataGridView3.Columns[2].Width = 300;

        }

        private void RefreshDataGridPoAktivnostima()
        {

            var select = "select Delavci.DeSifra, Rtrim(Delavci.DeIme) + ' ' + Rtrim(DePriimek), VrstaAktivnosti.ID, VrstaAktivnosti.Naziv " +
            " from PravoAktivnosti inner join Delavci on Delavci.DeSifra = PravoAktivnosti.Zaposleni " +
            " inner join VrstaAktivnosti on PravoAktivnosti.VrstaAktivnostiID = VrstaAktivnosti.ID";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID Zaposleni";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Zaposleni";
            dataGridView1.Columns[1].Width = 250;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "ID Vrsta Aktivnosti";
            dataGridView1.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Vrsta aktivnosti";
            dataGridView1.Columns[3].Width = 250;


        }

        private void frmPravoAktivnostiViseRadnika_Load(object sender, EventArgs e)
        {
            RefreshDataGrid();
            RefreshDataGridRAdnici();
        }

        private void btnUnesi_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow rowE in dataGridView3.Rows)
            {
                if (rowE.Selected == true)
                {
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        InsertPravoAktivnosti ins = new InsertPravoAktivnosti();

                        if (row.Selected == true)
                        {
                            ins.InsPravoAktivnosti(Convert.ToInt32(rowE.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[0].Value.ToString()));
                        }
                    }

                }

            }




            RefreshDataGridPoAktivnostima();

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
