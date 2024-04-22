using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;

namespace TrackModal.Izvestaji
{
    public partial class frmSelectTransport1 : Form
    {
        public frmSelectTransport1()
        {
            InitializeComponent();
        }

        private void frmSelectTransport1_Load(object sender, EventArgs e)
        {


        }

        private void btnStampa_Click(object sender, EventArgs e)
        {
            Saobracaj.TrackModal.TestiranjeDataSet22TableAdapters.SelectTransport1TableAdapter ta = new Saobracaj.TrackModal.TestiranjeDataSet22TableAdapters.SelectTransport1TableAdapter();
            Saobracaj.TrackModal.TestiranjeDataSet22.SelectTransport1DataTable dt = new Saobracaj.TrackModal.TestiranjeDataSet22.SelectTransport1DataTable();

            ta.Fill(dt, Convert.ToDateTime(dtpDatumOd.Value), Convert.ToDateTime(dtpDatumDo.Value));
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;

            ReportParameter[] par = new ReportParameter[2];
            par[0] = new ReportParameter("VremeOd", dtpDatumOd.Value.ToString());
            par[1] = new ReportParameter("VremeDo", dtpDatumDo.Value.ToString());
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptSelectTransport1.rdlc";
            reportViewer1.LocalReport.SetParameters(par);
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
        }
    }
}
