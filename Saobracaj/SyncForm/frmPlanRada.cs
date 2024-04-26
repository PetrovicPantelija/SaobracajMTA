using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Saobracaj.SyncForm
{
    public partial class frmPlanRada : Syncfusion.Windows.Forms.Office2010Form
    {
        public frmPlanRada()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            InitializeComponent();

        }
        string niz = "";
        public static string code = "frmPlanRada";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Sifarnici.frmLogovanje.user.ToString();
        private void frmPlanRada_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'TESTIRANJEDataSet4.Aktivnosti' table. You can move, or remove it, as needed.
            // this.aktivnostiTableAdapter.Fill(this.TESTIRANJEDataSet4.Aktivnosti);
            var select = "  select Markers.Display as Lokomotiva, Labels.Display as TIpRada, Reminders.Display as Zaposleni, Appointments.StartTime, Appointments.EndTime, Appointments.Subject, Appointments.LocationValue, Appointments.Content from Appointments " +
 " inner join Markers on Markers.Id = Appointments.MarkerValue " +
" inner join Labels on Labels.Id = Appointments.LabelValue " +
" inner join Reminders on Reminders.Id = Appointments.ReminderValue ";

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
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            frmPregledMobilni prMob = new frmPregledMobilni();
            prMob.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmKalendar kal = new frmKalendar();
            kal.Show();
        }
    }
}
