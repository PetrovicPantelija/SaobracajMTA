using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Windows.Forms;

namespace Saobracaj.Dokumenta
{
    public partial class frmStampaRadnogNaloga : Form
    {
        public static string code = "frmStampaRadnogNaloga";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Sifarnici.frmLogovanje.user.ToString();
        string niz = "";

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

            TESTIRANJEDataSet2TableAdapters.SelectRadniNalogPosaoTableAdapter ta = new TESTIRANJEDataSet2TableAdapters.SelectRadniNalogPosaoTableAdapter();
            TESTIRANJEDataSet2.SelectRadniNalogPosaoDataTable dt = new TESTIRANJEDataSet2.SelectRadniNalogPosaoDataTable();

            TESTIRANJEDataSet2TableAdapters.SelectRadniNalogPosaoLokTableAdapter tal = new TESTIRANJEDataSet2TableAdapters.SelectRadniNalogPosaoLokTableAdapter();
            TESTIRANJEDataSet2.SelectRadniNalogPosaoLokDataTable dtl = new TESTIRANJEDataSet2.SelectRadniNalogPosaoLokDataTable();

            TESTIRANJEDataSet2TableAdapters.SelectRadniNalogPosaoZapTableAdapter taz = new TESTIRANJEDataSet2TableAdapters.SelectRadniNalogPosaoZapTableAdapter();
            TESTIRANJEDataSet2.SelectRadniNalogPosaoZapDataTable dtz = new TESTIRANJEDataSet2.SelectRadniNalogPosaoZapDataTable();

            TESTIRANJEDataSet2TableAdapters.SelectRadniNalogPosaoUzrTableAdapter tau = new TESTIRANJEDataSet2TableAdapters.SelectRadniNalogPosaoUzrTableAdapter();
            TESTIRANJEDataSet2.SelectRadniNalogPosaoUzrDataTable dtu = new TESTIRANJEDataSet2.SelectRadniNalogPosaoUzrDataTable();

            TESTIRANJEDataSet2TableAdapters.SelectRadniNalogTraseLokZapEvidTableAdapter tae = new TESTIRANJEDataSet2TableAdapters.SelectRadniNalogTraseLokZapEvidTableAdapter();
            TESTIRANJEDataSet2.SelectRadniNalogTraseLokZapEvidDataTable dte = new TESTIRANJEDataSet2.SelectRadniNalogTraseLokZapEvidDataTable();



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
