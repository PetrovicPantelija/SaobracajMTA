//using Microsoft.Reporting.WinForms;
using Microsoft.Reporting.WinForms;
using Saobracaj.Sifarnici;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.RadniNalozi
{
    public partial class OtpremnicaPregled : Syncfusion.Windows.Forms.Office2010Form
    {
        private string connect = frmLogovanje.connectionString;
        private string korisnik;

        public OtpremnicaPregled()
        {
            InitializeComponent();
        }

        private void OtpremnicaPregled_Load(object sender, EventArgs e)
        {
            korisnik =frmLogovanje.user;
            FillGV();
        }

        private void FillGV()
        {
            var query = "select distinct DoStDob,DoStatus,DoDatDob,Partnerji.PaNaziv,p1.PaNaziv,DoSmSifra,DOZnes " +
                "From DObavnica " +
                "Inner join DobavnicaPostav on Dobavnica.DoStDob = DobavnicaPostav.DoPStDob " +
                
                "Inner join Partnerji on Dobavnica.DoPartPlac = Partnerji.PaSifra " +
                "Inner join Partnerji as p1 on Dobavnica.DoPartPrjm = p1.PaSifra " +
                
                "order by DoStDob desc";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.Columns[0].HeaderText = "Broj Otpremnice";
            dataGridView1.Columns[1].HeaderText = "Status";
            dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns[2].HeaderText = "Datum";
            dataGridView1.Columns[3].HeaderText = "Platilac";
            dataGridView1.Columns[3].Width = 300;
            dataGridView1.Columns[4].Width = 300;
            dataGridView1.Columns[4].HeaderText = "Primalac";
            dataGridView1.Columns[5].HeaderText = "Mesto";
            dataGridView1.Columns[6].HeaderText = "Iznos";
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    txtSifra.Text = row.Cells[0].Value.ToString().TrimEnd();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            OtpremnicaDataSetTableAdapters.DobavnicaRptTableAdapter ta = new OtpremnicaDataSetTableAdapters.DobavnicaRptTableAdapter();
            OtpremnicaDataSet.DobavnicaRptDataTable dt = new OtpremnicaDataSet.DobavnicaRptDataTable();
            ta.Fill(dt, Convert.ToInt32(txtSifra.Text));
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "Dobavnica";
            rds.Value = dt;

            ReportParameter[] par = new ReportParameter[1];
            par[0] = new ReportParameter("ID", txtSifra.Text);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptOtpremnica.rdlc";
            reportViewer1.LocalReport.SetParameters(par);
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();

            System.Drawing.Printing.PageSettings newPageSettings = new System.Drawing.Printing.PageSettings();
            newPageSettings.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            newPageSettings.Landscape = false;
            reportViewer1.SetPageSettings(newPageSettings);
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
        }
    }
}