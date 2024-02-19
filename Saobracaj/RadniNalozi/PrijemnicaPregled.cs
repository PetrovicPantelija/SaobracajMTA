
using Microsoft.Reporting.WinForms;
using Saobracaj.Sifarnici;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.RadniNalozi
{
    public partial class PrijemnicaPregled : Form
    {
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        string korisnik;
        public PrijemnicaPregled()
        {
            InitializeComponent();
        }
        private void PrijemnicaPregled_Load(object sender, EventArgs e)
        {
            korisnik = frmLogovanje.user;
            FillGV();
        }
        private void FillGV()
        {
            var query = "select distinct NPrStPre,NPrStatus,NPrDatPre,Partnerji.PaNaziv,p1.PaNaziv,NprSMSifra,NPrZnes,RTRim(RTrim(DeIme)+' '+RTrim(DePriimek)) " +
                "From NPre " +
                "inner join NpreP on NPre.NPrStPre = Nprep.NPrPStPre " +
                "inner join Partnerji on NPre.NPrPartPlac = Partnerji.PaSifra " +
                "Inner join Partnerji as p1 on NPre.NPrPartDob = p1.PaSifra " +
                "inner join Delavci on NPre.NPrStDelPre=Delavci.DeSifra "+
                "order by NPRStPre desc";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].HeaderText = "Broj Prijemnice";
            dataGridView1.Columns[1].HeaderText = "Status";
            dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns[2].HeaderText = "Datum";
            dataGridView1.Columns[3].HeaderText = "Platilac";
            dataGridView1.Columns[3].Width = 300;
            dataGridView1.Columns[4].Width = 300;
            dataGridView1.Columns[4].HeaderText = "Primalac";
            dataGridView1.Columns[5].HeaderText = "Mesto";
            dataGridView1.Columns[6].HeaderText = "Iznos";
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].HeaderText = "Primio";

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    txtSifra.Text = row.Cells[0].Value.ToString().TrimEnd();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            MMFruits_ProdDataSet1TableAdapters.NPreRptTableAdapter ta = new MMFruits_ProdDataSet1TableAdapters.NPreRptTableAdapter();
            MMFruits_ProdDataSet1.NPreRptDataTable dt = new MMFruits_ProdDataSet1.NPreRptDataTable();
            ta.Fill(dt, Convert.ToInt32(txtSifra.Text));
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet2";
            rds.Value = dt;
            ReportParameter[] par = new ReportParameter[1];
            par[0] = new ReportParameter("ID", txtSifra.Text);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptPrijemnica.rdlc";
            reportViewer1.LocalReport.SetParameters(par);
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
            */

            PrijemnicaDataSetTableAdapters.NPreRptTableAdapter ta = new PrijemnicaDataSetTableAdapters.NPreRptTableAdapter();
            PrijemnicaDataSet.NPreRptDataTable dt = new PrijemnicaDataSet.NPreRptDataTable();
            ta.Fill(dt, Convert.ToInt32(txtSifra.Text));
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "PrijemnicaDataSet";
            rds.Value = dt;

            ReportParameter[] par = new ReportParameter[1];
            par[0] = new ReportParameter("ID", txtSifra.Text);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptPrijemnica.rdlc";
            reportViewer1.LocalReport.SetParameters(par);
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }


    }
}
