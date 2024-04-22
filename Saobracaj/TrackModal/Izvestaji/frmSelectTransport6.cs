using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;

namespace Testiranje.Izvestaji
{
    public partial class frmSelectTransport6 : Form
    {
        public frmSelectTransport6()
        {
            InitializeComponent();
        }

        private void btnStampa_Click(object sender, EventArgs e)
        {
            Saobracaj.TrackModal.TestiranjeDataSet17TableAdapters.SelectTransport6TableAdapter ta = new Saobracaj.TrackModal.TestiranjeDataSet17TableAdapters.SelectTransport6TableAdapter();
            Saobracaj.TrackModal.TestiranjeDataSet17.SelectTransport6DataTable dt = new Saobracaj.TrackModal.TestiranjeDataSet17.SelectTransport6DataTable();

            ta.Fill(dt, Convert.ToDateTime(dtpDatumOd.Value), Convert.ToDateTime(dtpDatumDo.Value));
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;

            ReportParameter[] par = new ReportParameter[2];
            par[0] = new ReportParameter("VremeOd", dtpDatumOd.Value.ToString());
            par[1] = new ReportParameter("VremeDo", dtpDatumDo.Value.ToString());
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptKvarovi.rdlc";
            reportViewer1.LocalReport.SetParameters(par);
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
        }

        private void frmSelectTransport6_Load(object sender, EventArgs e)
        {

        }
    }
}
