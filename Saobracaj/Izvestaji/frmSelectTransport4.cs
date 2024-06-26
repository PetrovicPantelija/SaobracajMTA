﻿using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;

namespace TrackModal.Izvestaji
{
    public partial class frmSelectTransport4 : Form
    {
        public frmSelectTransport4()
        {
            InitializeComponent();
        }

        private void btnStampa_Click(object sender, EventArgs e)
        {

            Saobracaj.TrackModal.TestiranjeDataSet19TableAdapters.SelectTransport4TableAdapter ta = new Saobracaj.TrackModal.TestiranjeDataSet19TableAdapters.SelectTransport4TableAdapter();
            Saobracaj.TrackModal.TestiranjeDataSet19.SelectTransport4DataTable dt = new Saobracaj.TrackModal.TestiranjeDataSet19.SelectTransport4DataTable();

            ta.Fill(dt, Convert.ToDateTime(dtpDatumOd.Value), Convert.ToDateTime(dtpDatumDo.Value));
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;

            ReportParameter[] par = new ReportParameter[2];
            par[0] = new ReportParameter("VremeOd", dtpDatumOd.Value.ToString());
            par[1] = new ReportParameter("VremeDo", dtpDatumDo.Value.ToString());
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptSelectTransport4.rdlc";
            reportViewer1.LocalReport.SetParameters(par);
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
        }

        private void frmSelectTransport4_Load(object sender, EventArgs e)
        {

        }
    }
}
