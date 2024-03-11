using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

//
namespace Saobracaj.RadniNalozi
{
    public partial class RN8OtpremaCirade :  Syncfusion.Windows.Forms.Office2010Form

    {
        private string connect = Sifarnici.frmLogovanje.connectionString;
        private bool status = false;
        string KorisnikTekuci = "";
        public RN8OtpremaCirade()
        {
            InitializeComponent();
            FillGV();
            FillCombo();
        }

        public RN8OtpremaCirade(string OtpremaID, string Korisnik, string Usluga, string Kamion)
        {
            InitializeComponent();
            FillGV();
            FillCombo();
            txtOtpremaID.Text = OtpremaID;
            txtNalogIzdao.Text = Korisnik;
            //cboUsluga.SelectedValue = Usluga;
            NapuniVrstuUsluge(Usluga);
            VratiPodatkeVrstaMan(Usluga.ToString());
            txtKamion.Text = Kamion;
            KorisnikTekuci = Korisnik;
            txtNalogID.Text = Usluga;
        }


        private void VratiPodatkeVrstaMan(string IDUsluge)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" Select VrstaManipulacije.ID from RadniNalogInterni inner join " +
   " VrstaManipulacije on RadniNalogInterni.IDManipulacijaJed = VrstaManipulacije.ID where RadniNalogInterni.ID = " + IDUsluge, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                cboUsluga.SelectedValue = Convert.ToInt32(dr["ID"].ToString());

            }

            con.Close();
        }

        private void NapuniVrstuUsluge(string IDUsluga)
        {
            SqlConnection conn = new SqlConnection(connect);
            var usluge = "Select VrstaManipulacije.ID,VrstaManipulacije.Naziv from RadniNalogInterni inner join " +
   " VrstaManipulacije on RadniNalogInterni.IDManipulacijaJed = VrstaManipulacije.ID where RadniNalogInterni.ID = " + IDUsluga;
            var daUsluge = new SqlDataAdapter(usluge, conn);
            var dsUsluge = new DataSet();
            daUsluge.Fill(dsUsluge);
            cboUsluga.DataSource = dsUsluge.Tables[0];
            cboUsluga.DisplayMember = "Naziv";
            cboUsluga.ValueMember = "ID";

            // cboUsluga.SelectedValue = Convert.ToInt32(IDUsluga);
        }
        private void FillGV()
        {
            var select = " SELECT       RNOtpremaCirade.ID, [Kamion] ,[Zavrsen] ,[DatumRasporeda]   " +
              "   ,[BrojKontejnera]      ,[NalogIzdao]   " +
" ,[DatumRealizacije]   ,VrstaCarinskogPostupka.[Naziv] as CarinskiPostupak  " +
" ,Komitenti_1.PaNaziv as [NazivBrodara]      ,[VrstaRobe]    ,[SaSkladista]      ,[SaPozicijeSklad] " +
" ,[IdUsluge]      ,[NalogRealizovao]    ,[OtpremaID] " +
" ,[NalogID]   FROM[dbo].[RNOtpremaCirade] " +
" INNER JOIN  Partnerji AS Komitenti_1 ON [RNOtpremaCirade].NazivBrodara = Komitenti_1.PaSifra " +
" inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.id = [RNOtpremaCirade].CarinskiPostupak " +
" inner join  Skladista on [RNOtpremaCirade].[SaSkladista] = Skladista.ID ";

            SqlConnection conn = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connect);
            txtNalogIzdao.Text = Sifarnici.frmLogovanje.user.ToString().TrimEnd();
            //usluge->Manipulacije

            var vSredstvo = "select ID,(CONVERT(NVARCHAR,BrVoza) + ' /' +Relacija) As Opis from Voz order by ID desc";
            var daVS = new SqlDataAdapter(vSredstvo, conn);
            var dsVS = new DataSet();
            daVS.Fill(dsVS);
            cboNaSredstvo.DataSource = dsVS.Tables[0];
            cboNaSredstvo.DisplayMember = "Opis";
            cboNaSredstvo.ValueMember = "ID";

            var dir2 = "Select ID, (Oznaka + ' ' + Naziv) as Naziv from VrstaCarinskogPostupka order by Oznaka";
            var dirAD2 = new SqlDataAdapter(dir2, conn);
            var dirDS2 = new DataSet();
            dirAD2.Fill(dirDS2);
            cboPostupak.DataSource = dirDS2.Tables[0];
            cboPostupak.DisplayMember = "Naziv";
            cboPostupak.ValueMember = "ID";

            var partner7 = "Select PaSifra,PaNaziv From Partnerji where Brodar = 1 order by PaSifra";
            var partAD7 = new SqlDataAdapter(partner7, conn);
            var partDS7 = new DataSet();
            partAD7.Fill(partDS7);
            cboBrodar.DataSource = partDS7.Tables[0];
            cboBrodar.DisplayMember = "PaNaziv";
            cboBrodar.ValueMember = "PaSifra";

            var roba = "Select ID,Naziv From NHM order by ID";
            var daRoba = new SqlDataAdapter(roba, conn);
            var dsRoba = new DataSet();
            daRoba.Fill(dsRoba);
            cboVrstaRobe.DataSource = dsRoba.Tables[0];
            cboVrstaRobe.DisplayMember = "Naziv";
            cboVrstaRobe.ValueMember = "ID";

            var usluge = "Select ID,Naziv from VrstaManipulacije order by ID";
            var daUsluge = new SqlDataAdapter(usluge, conn);
            var dsUsluge = new DataSet();
            daUsluge.Fill(dsUsluge);
            cboUsluga.DataSource = dsUsluge.Tables[0];
            cboUsluga.DisplayMember = "Naziv";
            cboUsluga.ValueMember = "ID";

            var sklad = "select ID,naziv from Skladista";
            var daSklad = new SqlDataAdapter(sklad, conn);
            var dsSklad = new DataSet();
            daSklad.Fill(dsSklad);
            cboSaSklad.DataSource = dsSklad.Tables[0];
            cboSaSklad.DisplayMember = "Naziv";
            cboSaSklad.ValueMember = "ID";

            var pozicija = "Select Id,Opis from Pozicija";
            var daPoz = new SqlDataAdapter(pozicija, conn);
            var dsPoz = new DataSet();
            daPoz.Fill(dsPoz);
            cboSaPoz.DataSource = dsPoz.Tables[0];
            cboSaPoz.DisplayMember = "Opis";
            cboSaPoz.ValueMember = "ID";
        }
        private void RN8_Load(object sender, EventArgs e)
        {

        }

        private void txtcarinskipostupak_Click(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtvrstarobe_Click(object sender, EventArgs e)
        {

        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            InsertRN rn = new InsertRN();
            if (status == true)
            {
                rn.InsRnOtpremaCirade(Convert.ToDateTime(txtDatumRasporeda.Value), txtBrojKontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cboBrodar.SelectedValue),
                    txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(txtDatumRealizacije.Value), Convert.ToInt32(cboNaSredstvo.SelectedValue),
                    Convert.ToInt32(cboPostupak.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue), Convert.ToInt32(cboSaSklad.SelectedValue),
                    Convert.ToInt32(cboSaPoz.SelectedValue), Convert.ToInt32(cboUsluga.SelectedValue), "", txtNapomena.Text.ToString().TrimEnd());
            }
            else
            {
                rn.UpdRnOtpremaCirade(Convert.ToInt32(txtID.Text), Convert.ToDateTime(txtDatumRasporeda.Value), txtBrojKontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cboBrodar.SelectedValue),
                    txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(txtDatumRealizacije.Value), Convert.ToInt32(cboNaSredstvo.SelectedValue),
                    Convert.ToInt32(cboPostupak.SelectedValue), Convert.ToInt32(cboVrstaRobe.SelectedValue), Convert.ToInt32(cboSaSklad.SelectedValue),
                    Convert.ToInt32(cboSaPoz.SelectedValue), Convert.ToInt32(cboUsluga.SelectedValue), "", txtNapomena.Text.ToString().TrimEnd());
            }
            FillGV();
            status = false;
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                InsertRN rn = new InsertRN();
                rn.DelRnOtpremaCirade(Convert.ToInt32(txtID.Text.ToString().TrimEnd()));
                FillGV();
            }
            else
            {
                MessageBox.Show("Izaberite broj zapisa");
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txtID.Text = row.Cells[0].Value.ToString();
                        VratiPodatkeStavke(txtID.Text);
                        FillDG2();
                    }
                }
            }
            catch { }
        }

        private void FillDG2()
        {

            var select = "  SELECT     UvozKonacnaNHM.ID, NHM.Broj, UvozKonacnaNHM.IDNHM, NHM.Naziv FROM NHM INNER JOIN " +
" UvozKonacnaNHM ON NHM.ID = UvozKonacnaNHM.IDNHM " +
" inner join RadniNalogInterni on UvozKonacnanhm.idnadredjena = RadniNalogInterni.BrojOsnov " +
" where RadniNalogInterni.ID = " + Convert.ToInt32(txtNalogID.Text) + " order by UvozKonacnanhm.ID desc";


            SqlConnection conn = new SqlConnection(connect);

            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds.Tables[0];

            dataGridView3.BorderStyle = BorderStyle.None;
            dataGridView3.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView3.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView3.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView3.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView3.BackgroundColor = Color.White;

            dataGridView3.EnableHeadersVisualStyles = false;
            dataGridView3.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView3.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView3.Columns[0];
            dataGridView3.Columns[0].HeaderText = "ID";
            dataGridView3.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView3.Columns[1];
            dataGridView3.Columns[1].HeaderText = "NHM Broj";
            dataGridView3.Columns[1].Width = 70;

            DataGridViewColumn column3 = dataGridView3.Columns[2];
            dataGridView3.Columns[2].HeaderText = "ID";
            dataGridView3.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView3.Columns[3];
            dataGridView3.Columns[3].HeaderText = "NHM";
            dataGridView3.Columns[3].Width = 150;


        }

        private void VratiPodatkeStavke(string ID)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT [ID]      ,[DatumRasporeda] "+
   "  ,[BrojKontejnera]      ,[NazivBrodara]      ,[NalogIzdao]      ,[DatumRealizacije] "+
    "     ,[NaVoznoSredstvo]      ,[CarinskiPostupak]      ,[VrstaRobe]      ,[SaSkladista] " +
    "     ,[SaPozicijeSklad]      ,[IdUsluge]      ,[NalogRealizovao]      ,[Napomena] " +
     "    ,[OtpremaID]      ,[Kamion]      ,[Zavrsen]      ,[NalogID] " +
  "   FROM [dbo].[RNOtpremaCirade] " +
             " where ID = " + txtID.Text, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtDatumRasporeda.Value = Convert.ToDateTime(dr["DatumRasporeda"].ToString());
                txtBrojKontejnera.Text = dr["BrojKontejnera"].ToString();
                txtKamion.Text = dr["Kamion"].ToString();
                txtNalogIzdao.Text = dr["NalogIzdao"].ToString();
                txtDatumRealizacije.Value = Convert.ToDateTime(dr["DatumRealizacije"].ToString());
                cboPostupak.SelectedValue = Convert.ToInt32(dr["CarinskiPostupak"].ToString());
                cboBrodar.SelectedValue = Convert.ToInt32(dr["NazivBrodara"].ToString());
                cboVrstaRobe.SelectedValue = Convert.ToInt32(dr["VrstaRobe"].ToString());
                cboSaSklad.SelectedValue = Convert.ToInt32(dr["SaSkladista"].ToString());
                cboSaPoz.SelectedValue = Convert.ToInt32(dr["SaSkladista"].ToString());
                cboUsluga.SelectedValue = Convert.ToInt32(dr["IdUsluge"].ToString());
                txtNalogRealizovao.Text = dr["NalogRealizovao"].ToString();
                txtOtpremaID.Text = dr["OtpremaID"].ToString();
                txtNalogID.Text = dr["NalogID"].ToString();
                if (dr["Zavrsen"].ToString() == "1")
                { chkZavrsen.Checked = true; }
                else
                {
                    chkZavrsen.Checked = false;
                }
            }

            con.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            RadniNalozi.InsertRN ir = new InsertRN();
            //PANTA
            ir.InsRN8OtpremaCiradeKam(Convert.ToDateTime(txtDatumRasporeda.Value), txtNalogIzdao.Text, Convert.ToDateTime(txtDatumRealizacije.Text), Convert.ToInt32(cboNaSredstvo.SelectedValue), Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue), Convert.ToInt32(cboUsluga.SelectedValue), "", txtNapomena.Text, Convert.ToInt32(txtOtpremaID.Text), txtKamion.Text, Convert.ToInt32(cboPostupak.SelectedValue), txtNalogID.Text);
            FillGV();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            InsertRN up = new InsertRN();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected == true)
                {
                    up.PotvrdiUradjenRN8(Convert.ToInt32(row.Cells[0].Value.ToString()));
                }

            }
            VratiPodatkeStavke(txtID.Text);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Otpremnica otp = new Otpremnica();
            otp.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            OtpremnicaPregled otpp = new OtpremnicaPregled();
            otpp.Show();
        }
    }
}
