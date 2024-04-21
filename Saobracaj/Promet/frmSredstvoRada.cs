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

using Microsoft.Reporting.WinForms;

namespace TrackModal.Promet
{
    public partial class frmSredstvoRada : Form
    {
        // DataTable ndt;
        public static string code = "frmSredstvoRada";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Saobracaj.Sifarnici.frmLogovanje.user.ToString();
        string niz = "";
        public frmSredstvoRada()
        {
            InitializeComponent();
            IdGrupe();
            IdForme();
            PravoPristupa();
        }

        public frmSredstvoRada(string broj)
        {
            InitializeComponent();
            txtSifra.Text = broj;
            IdGrupe();
            IdForme();
            PravoPristupa();
        }
        public string IdGrupe()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            string query = "Select IdGrupe from KorisnikGrupa Where Korisnik = " + "'" + Kor.TrimEnd() + "'";
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            int count = 0;

            while (dr.Read())
            {
                if (dr.HasRows)
                {
                    if (count == 0)
                    {
                        niz = dr["IdGrupe"].ToString();
                        count++;
                    }
                    else
                    {
                        niz = niz + "," + dr["IdGrupe"].ToString();
                        count++;
                    }
                }
                else
                {
                    MessageBox.Show("Korisnik ne pripada grupi");
                }

            }
            conn.Close();
            return niz;
        }
        private int IdForme()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            string query = "Select IdForme from Forme where Rtrim(Code)=" + "'" + code + "'";
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                idForme = Convert.ToInt32(dr["IdForme"].ToString());
            }
            conn.Close();
            return idForme;
        }

        private void PravoPristupa()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            string query = "Select * From GrupeForme Where IdGrupe in (" + niz + ") and IdForme=" + idForme;
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows == false)
            {
                MessageBox.Show("Nemate prava za pristup ovoj formi", code);
                Pravo = false;
            }
            else
            {
                Pravo = true;
                while (reader.Read())
                {
                    insert = Convert.ToBoolean(reader["Upis"]);
                    if (insert == false)
                    {
                       // tsNew.Enabled = false;
                    }
                    update = Convert.ToBoolean(reader["Izmena"]);
                    if (update == false)
                    {
                        //tsSave.Enabled = false;
                    }
                    delete = Convert.ToBoolean(reader["Brisanje"]);
                    if (delete == false)
                    {
                       // tsDelete.Enabled = false;
                    }
                }
            }

            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Saobracaj.TrackModal.TestiranjeDataSet5TableAdapters.SelectNalogZaRAdPrometTableAdapter ta = new Saobracaj.TrackModal.TestiranjeDataSet5TableAdapters.SelectNalogZaRAdPrometTableAdapter();

            Saobracaj.TrackModal.TestiranjeDataSet5.SelectNalogZaRAdPrometDataTable dt = new Saobracaj.TrackModal.TestiranjeDataSet5.SelectNalogZaRAdPrometDataTable();
            /*
            TrackModalDataSet1TableAdapters.SelectZadatakPozicijaTableAdapter tal = new TrackModalDataSet1TableAdapters.SelectZadatakPozicijaTableAdapter();
            TrackModalDataSet1.SelectZadatakPozicijaDataTable dtl = new TrackModalDataSet1.SelectZadatakPozicijaDataTable();
            */
            ta.Fill(dt, Convert.ToInt32(txtSifra.Text));
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;


            Saobracaj.TrackModal.TestiranjeDataSet6TableAdapters.SelectNalogZaRadPrometMTableAdapter taa = new Saobracaj.TrackModal.TestiranjeDataSet6TableAdapters.SelectNalogZaRadPrometMTableAdapter();

            Saobracaj.TrackModal.TestiranjeDataSet6.SelectNalogZaRadPrometMDataTable dta = new Saobracaj.TrackModal.TestiranjeDataSet6.SelectNalogZaRadPrometMDataTable();
          
            taa.Fill(dta, Convert.ToInt32(txtSifra.Text));
            ReportDataSource rdsa = new ReportDataSource();
            rdsa.Name = "DataSet2";
            rdsa.Value = dta;

         

            ReportParameter[] par = new ReportParameter[1];
            par[0] = new ReportParameter("Dokument", txtSifra.Text);
           

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptNalogZaRad.rdlc";
            reportViewer1.LocalReport.SetParameters(par);
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.LocalReport.DataSources.Add(rdsa);
            /*
            reportViewer1.LocalReport.SubreportProcessing += new
                          SubreportProcessingEventHandler(SetSubDataSource);
             */
            reportViewer1.RefreshReport();
        }

        private void frmSredstvoRada_Load(object sender, EventArgs e)
        {
            /*var select4 = " Select Distinct ID, (Naziv + ' RO:' +  RegistarskaOznaka + ' IB:' + IndividualniBroj) as Naziv   From Vozila";
            var s_connection4 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection4 = new SqlConnection(s_connection4);
            var c4 = new SqlConnection(s_connection4);
            var dataAdapter4 = new SqlDataAdapter(select4, c4);

            var commandBuilder4 = new SqlCommandBuilder(dataAdapter4);
            var ds4 = new DataSet();
            dataAdapter4.Fill(ds4);
            cboSredstvoRada.DataSource = ds4.Tables[0];
            cboSredstvoRada.DisplayMember = "Naziv";
            cboSredstvoRada.ValueMember = "ID";
            */
        }


    }
}
