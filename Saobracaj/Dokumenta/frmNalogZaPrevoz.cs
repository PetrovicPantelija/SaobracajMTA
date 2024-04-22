using Microsoft.Reporting.WinForms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Dokumenta
{
    public partial class frmNalogZaPrevoz : Syncfusion.Windows.Forms.Office2010Form
    {
        string KorisnikCene;
        bool status = false;
        public frmNalogZaPrevoz()
        {
            InitializeComponent();
        }


        public frmNalogZaPrevoz(string Korisnik)
        {
            InitializeComponent();
            KorisnikCene = Korisnik;
        }
        public frmNalogZaPrevoz(int sifra, string Korisnik)
        {
            InitializeComponent();
            txtSifra.Text = sifra.ToString();
            KorisnikCene = Korisnik;

        }

        private void VratiPodatke(int ID)
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT [ID]       ,[BrojKontejnera1] " +
     " ,[BrojKontejnera2]       ,[UkupnaMasa] " +
     " ,[Relacija1]       ,[Relacija2]" +
     " ,[DatumPrevoza]       ,[VrstaRobe] " +
     " ,[UkupnaMasa2]       ,[Platilac] " +
     " ,[OrganizacionaJedinica]       ,[UtovarnoMesto] " +
     " ,[IstovarnoMesto]       ,[KontaktOsoba] " +
     " ,[Napomena]       ,[DatumKreiranja] " +
     " ,[Primalac]       ,[statusrn], " +
     " DatumUtovara, PredvidjenoDatumUtovara, " +
     " TipKontejnera1 , TipKontejnera2, VrstaRobeId, NetoMasaRobe " +
  " FROM [dbo].[NajavaPrevoza]    where ID = " + ID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtBrojKontejnera1.Text = dr["BrojKontejnera1"].ToString();
                txtBrojKontejnera2.Text = dr["BrojKontejnera2"].ToString();
                txtUkupnaMasa.Text = dr["UkupnaMasa"].ToString();
                txtRelacija1.Text = dr["Relacija1"].ToString();
                txtRelacija2.Text = dr["Relacija2"].ToString();
                dtpDatumPrevoza.Value = Convert.ToDateTime(dr["DatumPrevoza"].ToString());
                cboVrstaRobe.Text = dr["VrstaRobe"].ToString();
                cboVrstaRobeKomerijala.SelectedValue = Convert.ToInt32(dr["VrstaRobeId"].ToString());
                txtUkupnaMasa2.Value = Convert.ToDecimal(dr["UkupnaMasa2"].ToString());
                cboOrganizacionaJedinica.SelectedValue = Convert.ToInt32(dr["OrganizacionaJedinica"].ToString());
                txtUtovarnoMesto.Text = dr["UtovarnoMesto"].ToString();
                txtIstovarnoMesto.Text = dr["IstovarnoMesto"].ToString();
                txtKontaktOsoba.Text = dr["KontaktOsoba"].ToString();
                txtNapomena.Text = dr["Napomena"].ToString();
                dtpDatumUtovara.Value = Convert.ToDateTime(dr["DatumUtovara"].ToString());
                dtpPredvidjenDatumUtovara.Value = Convert.ToDateTime(dr["PredvidjenoDatumUtovara"].ToString());
                txtTipKontejnera.Text = dr["TipKontejnera1"].ToString();
                txtTipKontejnera2.Text = dr["TipKontejnera2"].ToString();
                //   cboVrstaRobe.SelectedValue = dr["VrstaRobeID"].ToString();
                cboPlatilac.SelectedValue = Convert.ToInt32(dr["Platilac"].ToString());
                cboPrimalac.SelectedValue = Convert.ToInt32(dr["Platilac"].ToString());
                txtNetoMasaRobe.Value = Convert.ToDecimal(dr["NetoMasaRobe"].ToString());

                if (Convert.ToInt32(dr["statusrn"].ToString()) == 1)
                {
                    chk1.Checked = true;
                }
                else if (Convert.ToInt32(dr["statusrn"].ToString()) == 2)
                {
                    chk2.Checked = false;
                }
                else
                {
                    chk3.Checked = false;
                }
            }

            con.Close();

        }
        public frmNalogZaPrevoz(string BrojKontejnera1, string BrojKontejnera2, string vrstarobe1, string vrstarobe2, double ukupnaMasa, string Korisnik, string TipKontejnera, string TipKontejnera2, double ukupnaMasa2)
        {
            InitializeComponent();
            KorisnikCene = Korisnik;
            txtBrojKontejnera1.Text = BrojKontejnera1;
            txtBrojKontejnera2.Text = BrojKontejnera2;
            // cboVrstaRobe.Text = vrstarobe1 + ".." + vrstarobe2;
            txtUkupnaMasa.Value = Convert.ToDecimal(ukupnaMasa);
            txtUkupnaMasa2.Value = Convert.ToDecimal(ukupnaMasa2);
            dtpDatumPrevoza.Value = DateTime.Now;
            txtTipKontejnera.Text = TipKontejnera;
            txtTipKontejnera2.Text = TipKontejnera2;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {


            int statusurn = 0;
            if (status == true)
            {

                Dokumenta.InsertNalogZaPrevoz ins = new InsertNalogZaPrevoz();
                if (chk1.Checked == true)
                {
                    statusurn = 1;
                }
                else if (chk2.Checked == true)
                { statusurn = 2; }
                else
                {
                    statusurn = 3;
                }

                ins.InsNalogZaPrevoz(txtBrojKontejnera1.Text, txtBrojKontejnera2.Text, Convert.ToDouble(txtUkupnaMasa.Value), txtRelacija1.Text, txtRelacija2.Text, Convert.ToDateTime(dtpDatumPrevoza.Value), cboVrstaRobe.Text, Convert.ToDouble(txtUkupnaMasa2.Text), Convert.ToInt32(cboPlatilac.SelectedValue), Convert.ToInt32(cboOrganizacionaJedinica.SelectedValue), Convert.ToInt32(txtUtovarnoMesto.SelectedValue), Convert.ToInt32(txtIstovarnoMesto.SelectedValue), txtKontaktOsoba.Text, txtNapomena.Text, Convert.ToDateTime(DateTime.Now), KorisnikCene, Convert.ToInt32(cboPrimalac.SelectedValue), Convert.ToInt32(statusurn), Convert.ToDateTime(dtpDatumUtovara.Value), Convert.ToDateTime(dtpPredvidjenDatumUtovara.Value), txtTipKontejnera.Text, txtTipKontejnera2.Text, 0, Convert.ToDouble(txtNetoMasaRobe.Value));

                status = false;
                VratiPodatkeMax();
            }
            else
            {

                if (status == false)
                {

                    InsertNalogZaPrevoz upd = new InsertNalogZaPrevoz();
                    if (chk1.Checked == true)
                    {
                        statusurn = 1;
                    }
                    else if (chk2.Checked == true)
                    { statusurn = 2; }
                    else
                    {
                        statusurn = 3;
                    }

                    upd.UpdNaloziZaPrevoz(Convert.ToInt32(txtSifra.Text), txtBrojKontejnera1.Text, txtBrojKontejnera2.Text, Convert.ToDouble(txtUkupnaMasa.Value), txtRelacija1.Text, txtRelacija2.Text, Convert.ToDateTime(dtpDatumPrevoza.Value), cboVrstaRobe.Text, Convert.ToDouble(txtUkupnaMasa2.Text), Convert.ToInt32(cboPlatilac.SelectedValue), Convert.ToInt32(cboOrganizacionaJedinica.SelectedValue), Convert.ToInt32(txtUtovarnoMesto.SelectedValue), Convert.ToInt32(txtIstovarnoMesto.SelectedValue), txtKontaktOsoba.Text, txtNapomena.Text, Convert.ToDateTime(DateTime.Now), KorisnikCene, Convert.ToInt32(cboPrimalac.SelectedValue), Convert.ToInt32(statusurn), Convert.ToDateTime(dtpDatumUtovara.Value), Convert.ToDateTime(dtpPredvidjenDatumUtovara.Value), txtTipKontejnera.Text, txtTipKontejnera2.Text, 0, Convert.ToDouble(txtNetoMasaRobe.Value));

                    status = false;
                }

            }
        }
        private void VratiPodatkeMax()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Max([ID]) as ID from NajavaPrevoza", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtSifra.Text = dr["ID"].ToString();
            }

            con.Close();
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtSifra.Enabled = false;
        }

        private void frmNalogZaPrevoz_Load(object sender, EventArgs e)
        {
            var select2 = "SELECT  * from OrganizacioneJedinice ";

            var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboOrganizacionaJedinica.DataSource = ds2.Tables[0];
            cboOrganizacionaJedinica.DisplayMember = "Naziv";
            cboOrganizacionaJedinica.ValueMember = "ID";


            var select = " select ID, Naziv from VrstaManipulacije ";

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView3.ReadOnly = false;
            dataGridView3.DataSource = ds.Tables[0];


            DataGridViewColumn column = dataGridView3.Columns[0];
            dataGridView3.Columns[0].HeaderText = "ID";
            dataGridView3.Columns[0].Width = 40;
            // dataGridView3.Columns[0].Visible = true;

            DataGridViewColumn column2 = dataGridView3.Columns[1];
            dataGridView3.Columns[1].HeaderText = "Manipulacija";
            dataGridView3.Columns[1].Width = 270;


            var select4 = " Select Distinct PASifra, PaNaziv From Partnerji order by PaNaziv";
            var s_connection4 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection4 = new SqlConnection(s_connection4);
            var c4 = new SqlConnection(s_connection4);
            var dataAdapter4 = new SqlDataAdapter(select4, c4);

            var commandBuilder4 = new SqlCommandBuilder(dataAdapter4);
            var ds4 = new DataSet();
            dataAdapter4.Fill(ds4);
            cboPlatilac.DataSource = ds4.Tables[0];
            cboPlatilac.DisplayMember = "PaNaziv";
            cboPlatilac.ValueMember = "PaSifra";

            var select5 = " Select Distinct PaSifra, PaNaziv From Partnerji order by PaNaziv";
            var s_connection5 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection5 = new SqlConnection(s_connection5);
            var c5 = new SqlConnection(s_connection5);
            var dataAdapter5 = new SqlDataAdapter(select5, c5);

            var commandBuilder5 = new SqlCommandBuilder(dataAdapter5);
            var ds5 = new DataSet();
            dataAdapter5.Fill(ds5);
            cboPrimalac.DataSource = ds5.Tables[0];
            cboPrimalac.DisplayMember = "PaNaziv";
            cboPrimalac.ValueMember = "PaSifra";

            var select6 = " Select Distinct ID, (UNKod + '-' + Naziv) as Naziv  From VrstaRobeADR";
            var s_connection6 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection6 = new SqlConnection(s_connection6);
            var c6 = new SqlConnection(s_connection6);
            var dataAdapter6 = new SqlDataAdapter(select6, c6);

            var commandBuilder6 = new SqlCommandBuilder(dataAdapter6);
            var ds6 = new DataSet();
            dataAdapter6.Fill(ds6);
            cboVrstaRobeKomerijala.DataSource = ds6.Tables[0];
            cboVrstaRobeKomerijala.DisplayMember = "Naziv";
            cboVrstaRobeKomerijala.ValueMember = "ID";


            var select7 = " Select Distinct ID, Naziv  From MestaUtovara";
            var s_connection7 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection7 = new SqlConnection(s_connection7);
            var c7 = new SqlConnection(s_connection7);
            var dataAdapter7 = new SqlDataAdapter(select7, c7);

            var commandBuilder7 = new SqlCommandBuilder(dataAdapter7);
            var ds7 = new DataSet();
            dataAdapter7.Fill(ds7);
            txtUtovarnoMesto.DataSource = ds7.Tables[0];
            txtUtovarnoMesto.DisplayMember = "Naziv";
            txtUtovarnoMesto.ValueMember = "ID";


            var select8 = " Select Distinct ID, Naziv  From MestaUtovara";
            var s_connection8 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection8 = new SqlConnection(s_connection8);
            var c8 = new SqlConnection(s_connection8);
            var dataAdapter8 = new SqlDataAdapter(select8, c8);

            var commandBuilder8 = new SqlCommandBuilder(dataAdapter8);
            var ds8 = new DataSet();
            dataAdapter8.Fill(ds8);
            txtIstovarnoMesto.DataSource = ds8.Tables[0];
            txtIstovarnoMesto.DisplayMember = "Naziv";
            txtIstovarnoMesto.ValueMember = "ID";

            if (txtSifra.Text != "")
            {
                VratiPodatke(Convert.ToInt32(txtSifra.Text));
                RefreshDataGrid4();
            }




        }

        private void btnUnesi_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                InsertNalogZaPrevoz ins = new InsertNalogZaPrevoz();

                if (row.Selected == true)
                {
                    ins.InsNalogZaPrevozTroskovi(Convert.ToInt32(txtSifra.Text), Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToDateTime(DateTime.Now), KorisnikCene, Convert.ToDouble(txtPopust.Value));
                }
            }
            RefreshDataGrid4();
        }

        private void RefreshDataGrid4()
        {
            var select = "  SELECT NajavaPrevozaTroskovi.ID as ID, VrstaManipulacije.Naziv as Naziv, Cene.Cena as UgovornaCena, NajavaPrevozaTroskovi.Popust, NajavaPrevozaTroskovi.KonacnaCena, NajavaPrevozaTroskovi.IdStavke  from NajavaPrevozaTroskovi " +
            " inner join VrstaManipulacije on NajavaPrevozaTroskovi.VrstaManipulacijeID = VrstaManipulacije.ID" +
           " inner join Cene on Cene.VrstaManipulacije = NajavaPrevozaTroskovi.VrstaManipulacijeID " +
            " where Cene.Komitent = 0 and TipCenovnika = 3 and NajavaPrevozaTroskovi.ID = " + Convert.ToInt32(txtSifra.Text);

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView4.ReadOnly = false;
            dataGridView4.DataSource = ds.Tables[0];



            DataGridViewColumn column = dataGridView4.Columns[0];
            dataGridView4.Columns[0].HeaderText = "ID";
            dataGridView4.Columns[0].Width = 40;
            // dataGridView2.Columns[0].Visible = false;

            DataGridViewColumn column2 = dataGridView4.Columns[1];
            dataGridView4.Columns[1].HeaderText = "Manipulacija";
            dataGridView4.Columns[1].Width = 270;

            DataGridViewColumn column3 = dataGridView4.Columns[2];
            dataGridView4.Columns[2].HeaderText = "Ugovorna cena";
            dataGridView4.Columns[2].Width = 80;

            DataGridViewColumn column4 = dataGridView4.Columns[3];
            dataGridView4.Columns[3].HeaderText = "Popust";
            dataGridView4.Columns[3].Width = 80;

            DataGridViewColumn column5 = dataGridView4.Columns[4];
            dataGridView4.Columns[4].HeaderText = "Konacna cena";
            dataGridView4.Columns[4].Width = 80;

            DataGridViewColumn column6 = dataGridView4.Columns[5];
            dataGridView4.Columns[5].HeaderText = "ID stavke";
            dataGridView4.Columns[5].Width = 50;

            RefreshDataGridUkupno();
        }

        private void txtNapomena_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView4.Rows)
            {
                InsertNalogZaPrevoz upd = new InsertNalogZaPrevoz();

                if (row.Selected == true)
                {
                    upd.UpdNalogZaPrevozTroskovi(Convert.ToInt32(row.Cells[5].Value.ToString()), Convert.ToDouble(txtPopust.Value));
                }
            }
            RefreshDataGrid4();
        }

        private void RefreshDataGridUkupno()
        {
            var select = "  SELECT Sum(Cene.Cena) as UkupnoCena, Sum(NajavaPrevozaTroskovi.KonacnaCena) as UkupnoKonacna  from NajavaPrevozaTroskovi " +
            " inner join VrstaManipulacije on NajavaPrevozaTroskovi.VrstaManipulacijeID = VrstaManipulacije.ID" +
           " inner join Cene on Cene.VrstaManipulacije = NajavaPrevozaTroskovi.VrstaManipulacijeID " +
            " where Komitent = 0 and TipCenovnika = 3 and NajavaPrevozaTroskovi.ID = " + Convert.ToInt32(txtSifra.Text);

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = false;
            dataGridView1.DataSource = ds.Tables[0];



            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "Ukupno po ugovornoj ceni";
            dataGridView4.Columns[0].Width = 70;
            // dataGridView2.Columns[0].Visible = false;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Ukupno po konačnoj ceni";
            dataGridView1.Columns[1].Width = 70;

        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            frmPutniNalog pn = new frmPutniNalog(txtSifra.Text, txtUtovarnoMesto.Text, txtIstovarnoMesto.Text, KorisnikCene, txtRelacija1.Text, txtRelacija2.Text);
            pn.Show();
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            TrackModalDataSet111TableAdapters.SelectNajavaPrevozaTableAdapter ta = new TrackModalDataSet111TableAdapters.SelectNajavaPrevozaTableAdapter();
            TrackModalDataSet111.SelectNajavaPrevozaDataTable dt = new TrackModalDataSet111.SelectNajavaPrevozaDataTable();

            ta.Fill(dt, Convert.ToInt32(txtSifra.Text));
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;

            ReportParameter[] par = new ReportParameter[1];
            par[0] = new ReportParameter("ID", txtSifra.Text);

            reportViewer1.LocalReport.DataSources.Clear();
            if (Sifarnici.frmLogovanje.Firma == "Leget")
            {
                reportViewer1.LocalReport.ReportPath = "rptNalogZaPrevoz.rdlc";
            }
            else if (Sifarnici.frmLogovanje.Firma == "TA")
            {
                reportViewer1.LocalReport.ReportPath = "rptNalogZaPrevozTA.rdlc";
            }
            else if (Sifarnici.frmLogovanje.Firma == "DPT")
            {
                reportViewer1.LocalReport.ReportPath = "rptNalogZaPrevozTA.rdlc";
            }
            reportViewer1.LocalReport.SetParameters(par);
            reportViewer1.LocalReport.DataSources.Add(rds);

            reportViewer1.RefreshReport();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView4_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView4.Rows)
                {
                    if (row.Selected)
                    {
                        txtIDTroskaManipulacije.Text = row.Cells[5].Value.ToString();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da želite da brišete?", "Izbor", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                InsertNalogZaPrevoz ins = new InsertNalogZaPrevoz();
                ins.DelNalogZaPrevozTroskovi(Convert.ToInt32(txtIDTroskaManipulacije.Text));
                RefreshDataGrid4();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var detailForm = new Saobracaj.Dokumenta.frmKontaktOsobe(Convert.ToInt32(cboPrimalac.SelectedValue)))
            {
                detailForm.ShowDialog();
                txtKontaktOsoba.Text = detailForm.GetKontakt(Convert.ToInt32(cboPrimalac.SelectedValue));
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtRelacija1.Text = txtUtovarnoMesto.Text;
            txtRelacija2.Text = txtIstovarnoMesto.Text;
        }
    }
}

