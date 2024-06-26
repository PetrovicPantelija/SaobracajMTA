﻿using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;
namespace Saobracaj.Izvestaji
{
    public partial class frmDodatniList : Form
    {
        public frmDodatniList()
        {
            InitializeComponent();
        }

        public frmDodatniList(int broj)
        {
            InitializeComponent();
            txtSifra.Text = broj.ToString();
        }

        private void btnStampa_Click(object sender, EventArgs e)
        {
            Saobracaj.TESTIRANJEDataSetTA12TableAdapters.SelectDodatniListTableAdapter ta = new Saobracaj.TESTIRANJEDataSetTA12TableAdapters.SelectDodatniListTableAdapter();


            Saobracaj.TESTIRANJEDataSetTA12.SelectDodatniListDataTable dt = new Saobracaj.TESTIRANJEDataSetTA12.SelectDodatniListDataTable();
            try
            {
                ta.Fill(dt, txtSifra.Text);
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "DataSet1";
                rds.Value = dt;

                ReportParameter[] par = new ReportParameter[1];
                par[0] = new ReportParameter("ID", txtSifra.Text);

                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.ReportPath = "rptDodatniList.rdlc";
                reportViewer1.LocalReport.SetParameters(par);
                reportViewer1.LocalReport.DataSources.Add(rds);
                reportViewer1.RefreshReport();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void frmDodatniList_Load(object sender, EventArgs e)
        {

        }
    }
}
