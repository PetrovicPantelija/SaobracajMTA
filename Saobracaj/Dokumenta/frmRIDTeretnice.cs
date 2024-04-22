using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Dokumenta
{
    public partial class frmRIDTeretnice : Form
    {
        public static string code = "frmRIDTeretnice";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Sifarnici.frmLogovanje.user.ToString();
        string niz = "";
        public frmRIDTeretnice()
        {
            InitializeComponent();

        }
        private void frmRIDTeretnice_Load(object sender, EventArgs e)
        {
            // Teretnice bez RID-a
            var select = "    Select Najava.RID, ts.RID, ts.* from Najava " +
            " inner join TeretnicaStavke ts on Najava.ID = ts.IDNajave " +
            " where Najava.RID = 1 and [Status] not in (7,8) and ts.RID  = ''";

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
