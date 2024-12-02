using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;

namespace Saobracaj.Dokumenta
{
    public partial class frmPopisKontejneraStampa : Syncfusion.Windows.Forms.Office2010Form
    {
        public frmPopisKontejneraStampa()
        {
            InitializeComponent();
        }
        public frmPopisKontejneraStampa(string broj)
        {
            InitializeComponent();
            txtSifra.Text = broj;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Saobracaj.TestiranjeDataSet19TableAdapters.SelectPopisKontejneraStavkeTableAdapter ta = new Saobracaj.TestiranjeDataSet19TableAdapters.SelectPopisKontejneraStavkeTableAdapter();
            Saobracaj.TestiranjeDataSet19.SelectPopisKontejneraStavkeDataTable dt = new Saobracaj.TestiranjeDataSet19.SelectPopisKontejneraStavkeDataTable();

            ta.Fill(dt, Convert.ToInt32(txtSifra.Text));
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;


            Saobracaj.TestiranjeDataSet20TableAdapters.SistemskePostavkeHeaderTableAdapter taa = new Saobracaj.TestiranjeDataSet20TableAdapters.SistemskePostavkeHeaderTableAdapter();

            Saobracaj.TestiranjeDataSet20.SistemskePostavkeHeaderDataTable dta = new Saobracaj.TestiranjeDataSet20.SistemskePostavkeHeaderDataTable();

            taa.Fill(dta);
            ReportDataSource rdsa = new ReportDataSource();
            rdsa.Name = "DataSet2";
            rdsa.Value = dta;

          

            ReportParameter[] par = new ReportParameter[1];
            par[0] = new ReportParameter("PopisBroj", txtSifra.Text);


            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptPopisnaListaKontejnera.rdlc";
            reportViewer1.LocalReport.SetParameters(par);
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.LocalReport.DataSources.Add(rdsa);
            /*
            reportViewer1.LocalReport.SubreportProcessing += new
                          SubreportProcessingEventHandler(SetSubDataSource);
             */
            reportViewer1.RefreshReport();
        }
    }
}
