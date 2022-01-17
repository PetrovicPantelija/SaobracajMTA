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
using System.Net;
using System.Net.Mail;

using Microsoft.Reporting.WinForms;

namespace Saobracaj.Testiranje
{
    public partial class Obrasci : Form
    {
        public Obrasci()
        {
            InitializeComponent();
        }

        private void Obrasci_Load(object sender, EventArgs e)
        {
            var select2 = "Select DeSifra, (Rtrim(DePriimek) + ' ' + Rtrim(DeIme)) as Korisnik from Delavci order by Rtrim(DePriimek)";

            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);

            DataView view2 = new DataView(ds2.Tables[0]);

            cboKorisnik.DataSource = view2;
            cboKorisnik.DisplayMember = "Korisnik";
            cboKorisnik.ValueMember = "DeSifra";
        }

        private void StampajManevristu()
        {
            Perftech_BeogradDataSet15TableAdapters.TestoviViewTableAdapter ta = new Perftech_BeogradDataSet15TableAdapters.TestoviViewTableAdapter();
            Perftech_BeogradDataSet15.TestoviViewDataTable dt = new Perftech_BeogradDataSet15.TestoviViewDataTable();
            //  NedraDataSet2TableAdapters.SelectNajavaTableAdapter ta = new NedraDataSet2TableAdapters.SelectNajavaTableAdapter();
            // NedraDataSet2.SelectNajavaDataTable dt = new NedraDataSet2.SelectNajavaDataTable();
            // string pom = cboGrupaTesta.SelectedValue.ToString();
            ta.Fill(dt);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;

            ReportParameter[] par = new ReportParameter[3];

            par[0] = new ReportParameter("Zaposleni", cboKorisnik.Text.ToString());
            par[1] = new ReportParameter("Datum", dtpDatumTesta.Value.Date.ToShortDateString());
            par[2] = new ReportParameter("Dodatak", txtMesto.Text.ToString());

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptObrazac6Manevrsita.rdlc";
            reportViewer1.LocalReport.SetParameters(par);
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();

        }

        private void StampajPregledacKola()
        {
            Perftech_BeogradDataSet15TableAdapters.TestoviViewTableAdapter ta = new Perftech_BeogradDataSet15TableAdapters.TestoviViewTableAdapter();
            Perftech_BeogradDataSet15.TestoviViewDataTable dt = new Perftech_BeogradDataSet15.TestoviViewDataTable();
            //  NedraDataSet2TableAdapters.SelectNajavaTableAdapter ta = new NedraDataSet2TableAdapters.SelectNajavaTableAdapter();
            // NedraDataSet2.SelectNajavaDataTable dt = new NedraDataSet2.SelectNajavaDataTable();
            // string pom = cboGrupaTesta.SelectedValue.ToString();
            ta.Fill(dt);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;

            ReportParameter[] par = new ReportParameter[3];

            par[0] = new ReportParameter("Zaposleni", cboKorisnik.Text.ToString());
            par[1] = new ReportParameter("Datum", dtpDatumTesta.Value.Date.ToShortDateString());
            par[2] = new ReportParameter("Dodatak", txtMesto.Text.ToString());

            reportViewer2.LocalReport.DataSources.Clear();
            reportViewer2.LocalReport.ReportPath = "rptObrazac6PregledacKola.rdlc";
            reportViewer2.LocalReport.SetParameters(par);
            reportViewer2.LocalReport.DataSources.Add(rds);
            reportViewer2.RefreshReport();

        }

        private void StampajVozovodja()
        {
            Perftech_BeogradDataSet15TableAdapters.TestoviViewTableAdapter ta = new Perftech_BeogradDataSet15TableAdapters.TestoviViewTableAdapter();
            Perftech_BeogradDataSet15.TestoviViewDataTable dt = new Perftech_BeogradDataSet15.TestoviViewDataTable();
            //  NedraDataSet2TableAdapters.SelectNajavaTableAdapter ta = new NedraDataSet2TableAdapters.SelectNajavaTableAdapter();
            // NedraDataSet2.SelectNajavaDataTable dt = new NedraDataSet2.SelectNajavaDataTable();
            // string pom = cboGrupaTesta.SelectedValue.ToString();
            ta.Fill(dt);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;

            ReportParameter[] par = new ReportParameter[3];

            par[0] = new ReportParameter("Zaposleni", cboKorisnik.Text.ToString());
            par[1] = new ReportParameter("Datum", dtpDatumTesta.Value.Date.ToShortDateString());
            par[2] = new ReportParameter("Dodatak", txtMesto.Text.ToString());

            reportViewer3.LocalReport.DataSources.Clear();
            reportViewer3.LocalReport.ReportPath = "rptObrazac6Vozovodja.rdlc";
            reportViewer3.LocalReport.SetParameters(par);
            reportViewer3.LocalReport.DataSources.Add(rds);
            reportViewer3.RefreshReport();

        }

        private void StampajMasinovodjaElektroVuce()
        {
            Perftech_BeogradDataSet15TableAdapters.TestoviViewTableAdapter ta = new Perftech_BeogradDataSet15TableAdapters.TestoviViewTableAdapter();
            Perftech_BeogradDataSet15.TestoviViewDataTable dt = new Perftech_BeogradDataSet15.TestoviViewDataTable();
            //  NedraDataSet2TableAdapters.SelectNajavaTableAdapter ta = new NedraDataSet2TableAdapters.SelectNajavaTableAdapter();
            // NedraDataSet2.SelectNajavaDataTable dt = new NedraDataSet2.SelectNajavaDataTable();
            // string pom = cboGrupaTesta.SelectedValue.ToString();
            ta.Fill(dt);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;

            ReportParameter[] par = new ReportParameter[3];

            par[0] = new ReportParameter("Zaposleni", cboKorisnik.Text.ToString());
            par[1] = new ReportParameter("Datum", dtpDatumTesta.Value.Date.ToShortDateString());
            par[2] = new ReportParameter("Dodatak", txtMesto.Text.ToString());

            reportViewer4.LocalReport.DataSources.Clear();
            reportViewer4.LocalReport.ReportPath = "rptObrazac6MasinovodjaElektrovuce.rdlc";
            reportViewer4.LocalReport.SetParameters(par);
            reportViewer4.LocalReport.DataSources.Add(rds);
            reportViewer4.RefreshReport();

        }

        private void StampajMasinovodjaNaManevri()
        {
            Perftech_BeogradDataSet15TableAdapters.TestoviViewTableAdapter ta = new Perftech_BeogradDataSet15TableAdapters.TestoviViewTableAdapter();
            Perftech_BeogradDataSet15.TestoviViewDataTable dt = new Perftech_BeogradDataSet15.TestoviViewDataTable();
            //  NedraDataSet2TableAdapters.SelectNajavaTableAdapter ta = new NedraDataSet2TableAdapters.SelectNajavaTableAdapter();
            // NedraDataSet2.SelectNajavaDataTable dt = new NedraDataSet2.SelectNajavaDataTable();
            // string pom = cboGrupaTesta.SelectedValue.ToString();
            ta.Fill(dt);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;

            ReportParameter[] par = new ReportParameter[3];

            par[0] = new ReportParameter("Zaposleni", cboKorisnik.Text.ToString());
            par[1] = new ReportParameter("Datum", dtpDatumTesta.Value.Date.ToShortDateString());
            par[2] = new ReportParameter("Dodatak", txtMesto.Text.ToString());

            reportViewer5.LocalReport.DataSources.Clear();
            reportViewer5.LocalReport.ReportPath = "rptObrazac6MasinovodjaNaManevri.rdlc";
            reportViewer5.LocalReport.SetParameters(par);
            reportViewer5.LocalReport.DataSources.Add(rds);
            reportViewer5.RefreshReport();

        }

        private void StampajMasinovodja()
        {
            Perftech_BeogradDataSet15TableAdapters.TestoviViewTableAdapter ta = new Perftech_BeogradDataSet15TableAdapters.TestoviViewTableAdapter();
            Perftech_BeogradDataSet15.TestoviViewDataTable dt = new Perftech_BeogradDataSet15.TestoviViewDataTable();
            //  NedraDataSet2TableAdapters.SelectNajavaTableAdapter ta = new NedraDataSet2TableAdapters.SelectNajavaTableAdapter();
            // NedraDataSet2.SelectNajavaDataTable dt = new NedraDataSet2.SelectNajavaDataTable();
            // string pom = cboGrupaTesta.SelectedValue.ToString();
            ta.Fill(dt);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;

            ReportParameter[] par = new ReportParameter[3];

            par[0] = new ReportParameter("Zaposleni", cboKorisnik.Text.ToString());
            par[1] = new ReportParameter("Datum", dtpDatumTesta.Value.Date.ToShortDateString());
            par[2] = new ReportParameter("Dodatak", txtMesto.Text.ToString());

            reportViewer6.LocalReport.DataSources.Clear();
            reportViewer6.LocalReport.ReportPath = "rptObrazac6Masinovodja.rdlc";
            reportViewer6.LocalReport.SetParameters(par);
            reportViewer6.LocalReport.DataSources.Add(rds);
            reportViewer6.RefreshReport();

        }

        private void StampajPomocnikMasinovodje()
        {
            Perftech_BeogradDataSet15TableAdapters.TestoviViewTableAdapter ta = new Perftech_BeogradDataSet15TableAdapters.TestoviViewTableAdapter();
            Perftech_BeogradDataSet15.TestoviViewDataTable dt = new Perftech_BeogradDataSet15.TestoviViewDataTable();
            //  NedraDataSet2TableAdapters.SelectNajavaTableAdapter ta = new NedraDataSet2TableAdapters.SelectNajavaTableAdapter();
            // NedraDataSet2.SelectNajavaDataTable dt = new NedraDataSet2.SelectNajavaDataTable();
            // string pom = cboGrupaTesta.SelectedValue.ToString();
            ta.Fill(dt);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;

            ReportParameter[] par = new ReportParameter[3];

            par[0] = new ReportParameter("Zaposleni", cboKorisnik.Text.ToString());
            par[1] = new ReportParameter("Datum", dtpDatumTesta.Value.Date.ToShortDateString());
            par[2] = new ReportParameter("Dodatak", txtMesto.Text.ToString());

            reportViewer7.LocalReport.DataSources.Clear();
            reportViewer7.LocalReport.ReportPath = "rptObrazac6PomocnikMasinovodje.rdlc";
            reportViewer7.LocalReport.SetParameters(par);
            reportViewer7.LocalReport.DataSources.Add(rds);
            reportViewer7.RefreshReport();

        }

        private void StampajRukovaocManevre()
        {
            Perftech_BeogradDataSet15TableAdapters.TestoviViewTableAdapter ta = new Perftech_BeogradDataSet15TableAdapters.TestoviViewTableAdapter();
            Perftech_BeogradDataSet15.TestoviViewDataTable dt = new Perftech_BeogradDataSet15.TestoviViewDataTable();
            //  NedraDataSet2TableAdapters.SelectNajavaTableAdapter ta = new NedraDataSet2TableAdapters.SelectNajavaTableAdapter();
            // NedraDataSet2.SelectNajavaDataTable dt = new NedraDataSet2.SelectNajavaDataTable();
            // string pom = cboGrupaTesta.SelectedValue.ToString();
            ta.Fill(dt);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;

            ReportParameter[] par = new ReportParameter[3];

            par[0] = new ReportParameter("Zaposleni", cboKorisnik.Text.ToString());
            par[1] = new ReportParameter("Datum", dtpDatumTesta.Value.Date.ToShortDateString());
            par[2] = new ReportParameter("Dodatak", txtMesto.Text.ToString());

            reportViewer8.LocalReport.DataSources.Clear();
            reportViewer8.LocalReport.ReportPath = "rptObrazac6RukovaocManevre.rdlc";
            reportViewer8.LocalReport.SetParameters(par);
            reportViewer8.LocalReport.DataSources.Add(rds);
            reportViewer8.RefreshReport();

        }

        private void sfButton1_Click(object sender, EventArgs e)
        {
            StampajManevristu();
            StampajPregledacKola();
            StampajVozovodja();
            StampajMasinovodjaElektroVuce();
            StampajMasinovodjaNaManevri();
            StampajMasinovodja();
            StampajPomocnikMasinovodje();
            StampajRukovaocManevre();

        }
    }
}
