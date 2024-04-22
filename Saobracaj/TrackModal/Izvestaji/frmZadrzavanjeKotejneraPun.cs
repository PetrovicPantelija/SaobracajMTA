using Microsoft.Reporting.WinForms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace Testiranje.Izvestaji
{
    public partial class frmZadrzavanjeKotejneraPun : Form
    {
        public frmZadrzavanjeKotejneraPun()
        {
            InitializeComponent();
        }

        private void frmZadrzavanjeKotejneraPun_Load(object sender, EventArgs e)
        {
            var select = "SELECT ID, Naziv from Komitenti where Vlasnik = 1";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboKomitent.DataSource = ds.Tables[0];
            cboKomitent.DisplayMember = "Naziv";
            cboKomitent.ValueMember = "ID";

            dtpDatumDo.Value = DateTime.Now;
            dtpDatumOd.Value = DateTime.Now;
        }

        private void btnUcitaj_Click(object sender, EventArgs e)
        {
            InsertZadrzavanjeKotejneraPun ins = new InsertZadrzavanjeKotejneraPun();
            ins.InsZadrzavanjeKotejneraPun(Convert.ToDateTime(dtpDatumOd.Value), Convert.ToDateTime(dtpDatumDo.Value), Convert.ToInt32(cboKomitent.SelectedValue));



            Saobracaj.TrackModal.Izvestaji.DataSetZadrzavanjeKotejneraPunTableAdapters.ZadrzavanjeViewPuniTableAdapter ta = new Saobracaj.TrackModal.Izvestaji.DataSetZadrzavanjeKotejneraPunTableAdapters.ZadrzavanjeViewPuniTableAdapter();
            Saobracaj.TrackModal.Izvestaji.DataSetZadrzavanjeKotejneraPun.ZadrzavanjeViewPuniDataTable dt = new Saobracaj.TrackModal.Izvestaji.DataSetZadrzavanjeKotejneraPun.ZadrzavanjeViewPuniDataTable();
            ta.Fill(dt);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;


            ReportParameter[] par = new ReportParameter[3];
            par[0] = new ReportParameter("DatumOd", dtpDatumOd.Value.Date.ToShortDateString());
            par[1] = new ReportParameter("DatumDo", dtpDatumDo.Value.Date.ToShortDateString());
            par[2] = new ReportParameter("Vlasnik", cboKomitent.Text.ToString());


            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptZadrzavanjeKontejneraPun.rdlc";
            reportViewer1.LocalReport.SetParameters(par);
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
        }
    }
}
