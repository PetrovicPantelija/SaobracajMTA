using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Izvoz
{
    public partial class IzvestajVaganja : Form
    {
        int id;
        int CIRADA;
        public IzvestajVaganja()
        {
            InitializeComponent();
        }
        public IzvestajVaganja(int ID, int Cirada)
        {
            InitializeComponent();
            id= ID;
            CIRADA= Cirada;
        }

        private void IzvestajVaganja_Load(object sender, EventArgs e)
        {
            TestiranjeDataSet17TableAdapters.VaganjeRptTableAdapter ta = new TestiranjeDataSet17TableAdapters.VaganjeRptTableAdapter();
            TestiranjeDataSet17.VaganjeRptDataTable dt = new TestiranjeDataSet17.VaganjeRptDataTable();
            ta.Fill(dt, id);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;
            ReportParameter[] par = new ReportParameter[1];
            par[0] = new ReportParameter("ID", id.ToString());
            reportViewer1.LocalReport.DataSources.Clear();
            if (CIRADA == 1)
            {
                reportViewer1.LocalReport.ReportPath = "VaganjeRptCirada.rdlc";
            }
            else
            {
                reportViewer1.LocalReport.ReportPath = "VaganjeRpt.rdlc";
            }

            reportViewer1.LocalReport.SetParameters(par);
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();

            System.Drawing.Printing.PageSettings newPageSettings = new System.Drawing.Printing.PageSettings();
            newPageSettings.Margins = new System.Drawing.Printing.Margins(5, 0, 5, 0);
            newPageSettings.Landscape = false;
            reportViewer1.SetPageSettings(newPageSettings);
        }
    }
}
