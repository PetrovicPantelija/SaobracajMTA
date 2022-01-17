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
    public partial class frmStampaRadnogNaloga : Form
    {
        DataTable ndt;
        DataTable ndtz;
        DataTable ndtu;
        DataTable ndte;
      
        public frmStampaRadnogNaloga()
        {
            InitializeComponent();
        }

        private void btnStampa_Click(object sender, EventArgs e)
        {

            Perftech_BeogradDataSet2TableAdapters.SelectRadniNalogPosaoTableAdapter ta = new Perftech_BeogradDataSet2TableAdapters.SelectRadniNalogPosaoTableAdapter();
            Perftech_BeogradDataSet2.SelectRadniNalogPosaoDataTable dt = new Perftech_BeogradDataSet2.SelectRadniNalogPosaoDataTable();

            Perftech_BeogradDataSet2TableAdapters.SelectRadniNalogPosaoLokTableAdapter tal = new Perftech_BeogradDataSet2TableAdapters.SelectRadniNalogPosaoLokTableAdapter();
            Perftech_BeogradDataSet2.SelectRadniNalogPosaoLokDataTable dtl = new Perftech_BeogradDataSet2.SelectRadniNalogPosaoLokDataTable();

            Perftech_BeogradDataSet2TableAdapters.SelectRadniNalogPosaoZapTableAdapter taz = new Perftech_BeogradDataSet2TableAdapters.SelectRadniNalogPosaoZapTableAdapter();
            Perftech_BeogradDataSet2.SelectRadniNalogPosaoZapDataTable dtz = new Perftech_BeogradDataSet2.SelectRadniNalogPosaoZapDataTable();

            Perftech_BeogradDataSet2TableAdapters.SelectRadniNalogPosaoUzrTableAdapter tau = new Perftech_BeogradDataSet2TableAdapters.SelectRadniNalogPosaoUzrTableAdapter();
            Perftech_BeogradDataSet2.SelectRadniNalogPosaoUzrDataTable dtu = new Perftech_BeogradDataSet2.SelectRadniNalogPosaoUzrDataTable();

            Perftech_BeogradDataSet2TableAdapters.SelectRadniNalogTraseLokZapEvidTableAdapter tae = new Perftech_BeogradDataSet2TableAdapters.SelectRadniNalogTraseLokZapEvidTableAdapter();
            Perftech_BeogradDataSet2.SelectRadniNalogTraseLokZapEvidDataTable dte = new Perftech_BeogradDataSet2.SelectRadniNalogTraseLokZapEvidDataTable();
       


            ta.Fill(dt, Convert.ToInt32(txtSifra.Text));
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet5";
            rds.Value = dt;

            tal.Fill(dtl);
            ReportDataSource rdsl = new ReportDataSource();
            rdsl.Name = "DataSet6";
            rdsl.Value = dtl;
            ndt = dtl;

            taz.Fill(dtz);
            ReportDataSource rdsz = new ReportDataSource();
            rdsz.Name = "DataSet7";
            rdsz.Value = dtz;
            ndtz = dtz;

            tau.Fill(dtu);
            ReportDataSource rdsu = new ReportDataSource();
            rdsu.Name = "DataSet8";
            rdsu.Value = dtu;
            ndtu = dtu;

            tae.Fill(dte);
            ReportDataSource rdse = new ReportDataSource();
            rdse.Name = "DataSet9";
            rdse.Value = dte;
            ndte = dte;


            ReportParameter[] par = new ReportParameter[1];
            par[0] = new ReportParameter("ID", txtSifra.Text);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "frmStampaRadnogNaloga.rdlc";
            reportViewer1.LocalReport.SetParameters(par);
            reportViewer1.LocalReport.DataSources.Add(rds);
           // reportViewer1.LocalReport.DataSources.Add(rdsl);
            reportViewer1.LocalReport.SubreportProcessing += new
                            SubreportProcessingEventHandler(SetSubDataSource);
            reportViewer1.LocalReport.SubreportProcessing += new
                           SubreportProcessingEventHandler(SetSubDataSourceZap);

            reportViewer1.LocalReport.SubreportProcessing += new
                          SubreportProcessingEventHandler(SetSubDataSourceUzr);

            reportViewer1.LocalReport.SubreportProcessing += new
                         SubreportProcessingEventHandler(SetSubDataSourceEvid);
            //SetSubDataSource
            reportViewer1.RefreshReport();

            
            /*
             Aziriraj Najavu
             
             */
        }
        public void SetSubDataSource(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("DataSet6", ndt));
        }

        public void SetSubDataSourceZap(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("DataSet7", ndtz));
        }

        public void SetSubDataSourceUzr(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("DataSet8", ndtu));
        }

        public void SetSubDataSourceEvid(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("DataSet9", ndte));
        }

        private void frmStampaRadnogNaloga_Load(object sender, EventArgs e)
        {

        }
    }
}
