using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;

namespace Saobracaj
{
    public partial class frmZaposleniUkupno : Form
    {
        public frmZaposleniUkupno()
        {
            InitializeComponent();
        }

        private void frmZaposleniUkupno_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'TESTIRANJEDataSet4.SelectSumVremeAktivnosti' table. You can move, or remove it, as needed.
            // this.SelectSumVremeAktivnostiTableAdapter.Fill(this.TESTIRANJEDataSet4.SelectSumVremeAktivnosti);

            // this.reportViewer1.RefreshReport();
        }

        private void btnStampa_Click(object sender, EventArgs e)
        {
            TESTIRANJEDataSet4TableAdapters.SelectSumVremeAktivnostiTableAdapter ta = new TESTIRANJEDataSet4TableAdapters.SelectSumVremeAktivnostiTableAdapter();
            TESTIRANJEDataSet4.SelectSumVremeAktivnostiDataTable dt = new TESTIRANJEDataSet4.SelectSumVremeAktivnostiDataTable();

            ta.Fill(dt, Convert.ToDateTime(dtpVremeOd.Value), Convert.ToDateTime(dtpVremeDo.Value));
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet7";
            rds.Value = dt;

            ReportParameter[] par = new ReportParameter[2];
            par[0] = new ReportParameter("VremeOd", dtpVremeOd.Value.ToString());
            par[1] = new ReportParameter("VremeDo", dtpVremeDo.Value.ToString());

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptUkupnoZaPeriod.rdlc";
            reportViewer1.LocalReport.SetParameters(par);
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();


            //Ukoliko je Najava i CIM onda Update
            //upd.UpdNajavaCIM(Convert.ToInt32(txtSifra.Text),  Korisnik);
            /*
             Aziriraj Najavu
             
             */
        }
    }
}
