using Microsoft.Reporting.WinForms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Dokumenta
{
    public partial class frmPlacenoNeplaceno : Form
    {
        public frmPlacenoNeplaceno()
        {
            InitializeComponent();

        }
        string niz = "";
        public static string code = "frmPlacenoNeplaceno";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Sifarnici.frmLogovanje.user.ToString();
        private void btnStampa_Click(object sender, EventArgs e)
        {
            TESTIRANJEDataSet7TableAdapters.SelectAktivnostiFinasijeNeplacenoTableAdapter ta = new TESTIRANJEDataSet7TableAdapters.SelectAktivnostiFinasijeNeplacenoTableAdapter();
            TESTIRANJEDataSet7.SelectAktivnostiFinasijeNeplacenoDataTable dt = new TESTIRANJEDataSet7.SelectAktivnostiFinasijeNeplacenoDataTable();

            ta.Fill(dt, Convert.ToDateTime(dtpVremeOd.Value), Convert.ToDateTime(dtpVremeDo.Value));
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet11";
            rds.Value = dt;
            DateTime dtStartDate = dtpVremeOd.Value;
            DateTime dtEndDate = dtpVremeDo.Value;

            ReportParameter[] par = new ReportParameter[2];

            par[0] = new ReportParameter("VremeOd", dtStartDate.ToLongDateString(), false);
            par[1] = new ReportParameter("VremeDo", dtEndDate.ToLongDateString(), false);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptPlacenoNeplaceno.rdlc";
            reportViewer1.LocalReport.SetParameters(par);
            reportViewer1.LocalReport.DataSources.Add(rds);

            reportViewer1.RefreshReport();
        }

        private void frmPlacenoNeplaceno_Load(object sender, EventArgs e)
        {
            var select3 = " select DeSifra as ID, (Rtrim(DePriimek) + ' ' + RTrim(DeIme)) as Opis from Delavci order by opis";
            var s_connection3 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboZaposleni.DataSource = ds3.Tables[0];
            cboZaposleni.DisplayMember = "Opis";
            cboZaposleni.ValueMember = "ID";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TESTIRANJEDataSet8TableAdapters.SelectAktivnostiFinasijePlacenoTableAdapter ta = new TESTIRANJEDataSet8TableAdapters.SelectAktivnostiFinasijePlacenoTableAdapter();
            TESTIRANJEDataSet8.SelectAktivnostiFinasijePlacenoDataTable dt = new TESTIRANJEDataSet8.SelectAktivnostiFinasijePlacenoDataTable();

            ta.Fill(dt, Convert.ToDateTime(dtpVremeOd.Value), Convert.ToDateTime(dtpVremeDo.Value));
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet12";
            rds.Value = dt;
            DateTime dtStartDate = dtpVremeOd.Value;
            DateTime dtEndDate = dtpVremeDo.Value;

            ReportParameter[] par = new ReportParameter[2];

            par[0] = new ReportParameter("VremeOd", dtStartDate.ToLongDateString(), false);
            par[1] = new ReportParameter("VremeDo", dtEndDate.ToLongDateString(), false);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptPlacenoTrue.rdlc";
            reportViewer1.LocalReport.SetParameters(par);
            reportViewer1.LocalReport.DataSources.Add(rds);

            reportViewer1.RefreshReport();

        }
    }
}

