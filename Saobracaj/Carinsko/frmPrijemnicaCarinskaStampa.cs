using Microsoft.Reporting.WinForms;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.Windows.Forms;
using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Carinsko
{
    public partial class frmPrijemnicaCarinskaStampa : Form
    {
        string Prijemnica = "0";
        public frmPrijemnicaCarinskaStampa()
        {
            InitializeComponent();
        }

        public frmPrijemnicaCarinskaStampa(string PrijemnicaID)
        {
            InitializeComponent();
            Prijemnica = PrijemnicaID;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Saobracaj.CarinskaPrijemnicaDataSetTableAdapters.SelectCarinskaPrijemnicaTableAdapter ta = new Saobracaj.CarinskaPrijemnicaDataSetTableAdapters.SelectCarinskaPrijemnicaTableAdapter();
            Saobracaj.CarinskaPrijemnicaDataSet.SelectCarinskaPrijemnicaDataTable dt = new Saobracaj.CarinskaPrijemnicaDataSet.SelectCarinskaPrijemnicaDataTable();

            ta.Fill(dt, Convert.ToInt32(txtSifra.Text));
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;




         
            Saobracaj.CarinskaPrijemnicaStavkeDataSetTableAdapters.SelectCarinskaPrijemnicaStavkeTableAdapter taa = new Saobracaj.CarinskaPrijemnicaStavkeDataSetTableAdapters.SelectCarinskaPrijemnicaStavkeTableAdapter();

            Saobracaj.CarinskaPrijemnicaStavkeDataSet.SelectCarinskaPrijemnicaStavkeDataTable dta = new Saobracaj.CarinskaPrijemnicaStavkeDataSet.SelectCarinskaPrijemnicaStavkeDataTable();

            taa.Fill(dta, Convert.ToInt32(txtSifra.Text));
            ReportDataSource rdsa = new ReportDataSource();
            rdsa.Name = "DataSet2";
            rdsa.Value = dta;
          


            ReportParameter[] par = new ReportParameter[1];
            par[0] = new ReportParameter("ID", txtSifra.Text);




            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptPrijemnicaCarinska.rdlc";
            reportViewer1.LocalReport.SetParameters(par);
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.LocalReport.DataSources.Add(rdsa);
            /*
            reportViewer1.LocalReport.SubreportProcessing += new
                          SubreportProcessingEventHandler(SetSubDataSource);
             */
            reportViewer1.RefreshReport();
        }

        private void frmPrijemnicaCarinskaStampa_Load(object sender, EventArgs e)
        {
            txtSifra.Text = Prijemnica;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Saobracaj.CarinskaPrijemnicaSkladDataSetTableAdapters.SelectCarinskaPrijemnicaSkladisniDokTableAdapter ta = new Saobracaj.CarinskaPrijemnicaSkladDataSetTableAdapters.SelectCarinskaPrijemnicaSkladisniDokTableAdapter();
            Saobracaj.CarinskaPrijemnicaSkladDataSet.SelectCarinskaPrijemnicaSkladisniDokDataTable dt = new Saobracaj.CarinskaPrijemnicaSkladDataSet.SelectCarinskaPrijemnicaSkladisniDokDataTable();

            ta.Fill(dt, Convert.ToInt32(txtSifra.Text));
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;



            ReportParameter[] par = new ReportParameter[1];
            par[0] = new ReportParameter("ID", txtSifra.Text);




            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptPrijemnicaCarinskaSkladisni.rdlc";
            reportViewer1.LocalReport.SetParameters(par);
            reportViewer1.LocalReport.DataSources.Add(rds);
      
            /*
            reportViewer1.LocalReport.SubreportProcessing += new
                          SubreportProcessingEventHandler(SetSubDataSource);
             */
            reportViewer1.RefreshReport();
        }
    }
}
