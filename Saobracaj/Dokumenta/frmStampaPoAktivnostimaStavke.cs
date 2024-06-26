﻿using Microsoft.Reporting.WinForms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Dokumenta
{
    public partial class frmStampaPoAktivnostimaStavke : Form
    {
        public frmStampaPoAktivnostimaStavke()
        {
            InitializeComponent();
        }

        private void btnStampa_Click(object sender, EventArgs e)
        {
            int pomMilsped = 0;
            if (chkMilsped.Checked == true)
                pomMilsped = 1;
            else
                pomMilsped = 0;

            int pomMasinovodja = 0;
            if (chkMasinovodja.Checked == true)
                pomMasinovodja = 1;
            else
                pomMasinovodja = 0;

            TESTIRANJEDataSet5TableAdapters.SelectAktSt2TableAdapter ta = new TESTIRANJEDataSet5TableAdapters.SelectAktSt2TableAdapter();
            TESTIRANJEDataSet5.SelectAktSt2DataTable dt = new TESTIRANJEDataSet5.SelectAktSt2DataTable();

            ta.Fill(dt, Convert.ToInt32(cboZaposleni.SelectedValue), Convert.ToDateTime(dtpVremeOd.Value), Convert.ToDateTime(dtpVremeDo.Value), Convert.ToInt32(pomMilsped), Convert.ToInt32(pomMasinovodja));
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;
            DateTime dtStartDate = dtpVremeOd.Value;
            DateTime dtEndDate = dtpVremeDo.Value;

            ReportParameter[] par = new ReportParameter[3];
            par[0] = new ReportParameter("ID", cboZaposleni.SelectedValue.ToString());
            par[1] = new ReportParameter("DatumOd", dtStartDate.ToLongDateString(), false);
            par[2] = new ReportParameter("DatumDo", dtEndDate.ToLongDateString(), false);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "ZaradeStavkePoZaposlenom.rdlc";
            reportViewer1.LocalReport.SetParameters(par);
            reportViewer1.LocalReport.DataSources.Add(rds);

            reportViewer1.RefreshReport();

        }

        private void frmStampaPoAktivnostimaStavke_Load(object sender, EventArgs e)
        {
            var select3 = " select DeSifra as ID, (RTrim(DeIme) + ' ' + Rtrim(DePriimek)) as Opis from Delavci order by opis";
            var s_connection3 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboZaposleni.DataSource = ds3.Tables[0];
            cboZaposleni.DisplayMember = "Opis";
            cboZaposleni.ValueMember = "ID";
        }
    }
}
