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

using System.Drawing;
//
namespace Saobracaj.RadniNalozi
{
    public partial class RN6OtpremaPlatforme : Form
    {
        private string connect = Sifarnici.frmLogovanje.connectionString;
        private bool status = false;
        string KorisnikTekuci = "";
        public RN6OtpremaPlatforme()
        {
            InitializeComponent();
            FillGV();
            FillCombo();
        }

        public RN6OtpremaPlatforme(string OtpremaID, string Korisnik, string Usluga, string Kamion, int Uvoz)
        {
            //Uvoz = 0
            //Izvoz = 1
            InitializeComponent();
            
           
            FillCombo();
            txtOtpremaID.Text = OtpremaID;
            txtNalogIzdao.Text = Korisnik;
            //cboUsluga.SelectedValue = Usluga;
            if (Uvoz == 0)
            {
                FillGV();
                NapuniVrstuUsluge(Usluga);
                VratiPodatkeVrstaMan(Usluga.ToString());
                chkUvoz.Checked = true;
                chkIzvoz.Checked = false;
            }
            else
            {
                FillGVIzvozni();
                NapuniVrstuUsluge(Usluga);
                VratiPodatkeVrstaMan(Usluga.ToString());
                chkUvoz.Checked = false;
                chkIzvoz.Checked = true;
            }
           
            txtRegBr.Text = Kamion;
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
           
            var select = " SELECT       [RNOtpremaPlatforme].ID, [Kamion] ,[Zavrsen] ,[DatumRasporeda]   " +
                "   ,[BrojKontejnera]  ,TipKontenjera.NAziv as [VrstaKontejnera]      ,[NalogIzdao]   " +
" ,[DatumRealizacije], Komitenti_3.PaNaziv as [Uvoznik]    ,VrstaCarinskogPostupka.[Naziv] as CarinskiPostupak        , Komitenti_2.PaNaziv as [SpedicijaRTC] " +
" ,Komitenti_1.PaNaziv as [NazivBrodara]      ,[VrstaRobe]    ,[SaSkladista]      ,[SaPozicijeSklad] " +
" ,[IdUsluge]      ,[NalogRealizovao]    ,[OtpremaID] " +
" ,[NalogID]   FROM[dbo].[RNOtpremaPlatforme] " +
" INNER JOIN  Partnerji AS Komitenti_1 ON[RNOtpremaPlatforme].NazivBrodara = Komitenti_1.PaSifra " +
" INNER JOIN  Partnerji AS Komitenti_2 ON[RNOtpremaPlatforme].SpedicijaRTC = Komitenti_2.PaSifra " +
" INNER JOIN  Partnerji AS Komitenti_3 ON[RNOtpremaPlatforme].Uvoznik = Komitenti_3.PaSifra " +
" inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.id = [RNOtpremaPlatforme].CarinskiPostupak " +
" inner join  Skladista on[RNOtpremaPlatforme].[SaSkladista] = Skladista.ID " +
" inner join TipKontenjera on TipKontenjera.ID = [RNOtpremaPlatforme].[VrstaKontejnera]" +
" where Uvoz = 0 ";
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

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 20;
        }

        private void FillGVIzvozni()
        {

            var select = "  SELECT  [RNOtpremaPlatforme].ID, [Kamion] ,[Zavrsen] ,[DatumRasporeda]  " + 
" ,[BrojKontejnera], TipKontenjera.NAziv as [VrstaKontejnera]      ,[NalogIzdao] " +
" ,[DatumRealizacije], Komitenti_2.PaNaziv as Izvoznik    ,    " +
" Komitenti_1.PaNaziv as [NazivBrodara]      ,[VrstaRobe]    ,[SaSkladista]      ,[SaPozicijeSklad] " +
"  ,[IdUsluge]      ,[NalogRealizovao]    ,[OtpremaID] " +
" ,[NalogID]   FROM[dbo].[RNOtpremaPlatforme] " +
" INNER JOIN  Partnerji AS Komitenti_1 ON[RNOtpremaPlatforme].NazivBrodara = Komitenti_1.PaSifra " +
" INNER JOIN  Partnerji AS Komitenti_2 ON[RNOtpremaPlatforme].Izvoznik = Komitenti_2.PaSifra " +
" inner join  Skladista on[RNOtpremaPlatforme].[SaSkladista] = Skladista.ID " +
" inner join TipKontenjera on TipKontenjera.ID = [RNOtpremaPlatforme].[VrstaKontejnera] " +
" where Uvoz = 1";
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

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 20;
        }

        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connect);

            var partner4 = "Select PaSifra,PaNaziv From Partnerji  order by PaSifra";
            var partAD4 = new SqlDataAdapter(partner4, conn);
            var partDS4 = new DataSet();
            partAD4.Fill(partDS4);
            cboSpedicija.DataSource = partDS4.Tables[0];
            cboSpedicija.DisplayMember = "PaNaziv";
            cboSpedicija.ValueMember = "PaSifra";

            var dir2 = "Select ID, (Oznaka + ' ' + Naziv) as Naziv from VrstaCarinskogPostupka order by Oznaka";
            var dirAD2 = new SqlDataAdapter(dir2, conn);
            var dirDS2 = new DataSet();
            dirAD2.Fill(dirDS2);
            cboPostupak.DataSource = dirDS2.Tables[0];
            cboPostupak.DisplayMember = "Naziv";
            cboPostupak.ValueMember = "ID";

            var TipKontenjera = "Select ID,SkNaziv from TipKontenjera order by ID";
            var daTK = new SqlDataAdapter(TipKontenjera, conn);
            var daDS = new DataSet();
            daTK.Fill(daDS);
            cboVrstaKontejnera.DataSource = daDS.Tables[0];
            cboVrstaKontejnera.DisplayMember = "SkNaziv";
            cboVrstaKontejnera.ValueMember = "ID";

            txtNalogIzdao.Text = Sifarnici.frmLogovanje.user.ToString().TrimEnd();
            //usluge->Manipulacije

            var vSredstvo = "select ID,(CONVERT(NVARCHAR,BrVoza) + ' /' +Relacija) As Opis from Voz order by ID desc";
            var daVS = new SqlDataAdapter(vSredstvo, conn);
            var dsVS = new DataSet();
            daVS.Fill(dsVS);
            cboNaSredstvo.DataSource = dsVS.Tables[0];
            cboNaSredstvo.DisplayMember = "Opis";
            cboNaSredstvo.ValueMember = "ID";

            var partner2 = "Select PaSifra,PaNaziv From Partnerji order by PaSifra";
            var partAD2 = new SqlDataAdapter(partner2, conn);
            var partDS2 = new DataSet();
            partAD2.Fill(partDS2);
            cboUvoznik.DataSource = partDS2.Tables[0];
            cboUvoznik.DisplayMember = "PaNaziv";
            cboUvoznik.ValueMember = "PaSifra";

            var dir22 = "Select ID, (Oznaka + ' ' + Naziv) as Naziv from VrstaCarinskogPostupka order by Oznaka";
            var dirAD22 = new SqlDataAdapter(dir22, conn);
            var dirDS22 = new DataSet();
            dirAD22.Fill(dirDS22);
            cboPostupak.DataSource = dirDS22.Tables[0];
            cboPostupak.DisplayMember = "Naziv";
            cboPostupak.ValueMember = "ID";

            var partner44 = "Select PaSifra,PaNaziv From Partnerji  order by PaSifra";
            var partAD44 = new SqlDataAdapter(partner44, conn);
            var partDS44 = new DataSet();
            partAD44.Fill(partDS44);
            cboSpedicija.DataSource = partDS44.Tables[0];
            cboSpedicija.DisplayMember = "PaNaziv";
            cboSpedicija.ValueMember = "PaSifra";

            var partner7 = "Select PaSifra,PaNaziv From Partnerji  order by PaSifra";
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

            var partner5 = "Select PaSifra,PaNaziv From Partnerji  order by PaSifra";
            var partAD5 = new SqlDataAdapter(partner5, conn);
            var partDS5 = new DataSet();
            partAD5.Fill(partDS5);
            cboIzvoznik.DataSource = partDS5.Tables[0];
            cboIzvoznik.DisplayMember = "PaNaziv";
            cboIzvoznik.ValueMember = "PaSifra";
        }
        private void txtIDinazivplaniraneusluge_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtUSkladiste_Click(object sender, EventArgs e)
        {

        }

        private void RN6OtpremaPlatforme_Load(object sender, EventArgs e)
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
                rn.InsRNOtpremaPlatforme(Convert.ToDateTime(txtDatumRasporeda.Value), txtbrojkontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cboVrstaKontejnera.SelectedValue),
                    txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(txtDatumRealizacije.Value), Convert.ToInt32(cboNaSredstvo.SelectedValue),
                    Convert.ToInt32(cboUvoznik.SelectedValue), Convert.ToInt32(cboPostupak.SelectedValue), Convert.ToInt32(cboSpedicija.SelectedValue), Convert.ToInt32(cboBrodar.SelectedValue),
                    Convert.ToInt32(cboVrstaRobe.SelectedValue), Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue), Convert.ToInt32(cboUsluga.SelectedValue), "");
            }
            else
            {
                rn.UpdRNOtpremaPlatforme(Convert.ToInt32(txtID.Text), Convert.ToDateTime(txtDatumRasporeda.Value), txtbrojkontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cboVrstaKontejnera.SelectedValue),
                    txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(txtDatumRealizacije.Value), Convert.ToInt32(cboNaSredstvo.SelectedValue),
                    Convert.ToInt32(cboUvoznik.SelectedValue), Convert.ToInt32(cboPostupak.SelectedValue), Convert.ToInt32(cboSpedicija.SelectedValue), Convert.ToInt32(cboBrodar.SelectedValue),
                    Convert.ToInt32(cboVrstaRobe.SelectedValue), Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue),
                    Convert.ToInt32(cboUsluga.SelectedValue), txtNalogRealizovao.Text.ToString().TrimEnd());
            }
            FillGV();
            status = false;
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                InsertRN rn = new InsertRN();
                rn.DelRNOtpremaPlatforme(Convert.ToInt32(txtID.Text.ToString().TrimEnd()));
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

            SqlCommand cmd = new SqlCommand(" SELECT       ID ,[DatumRasporeda]      ,[BrojKontejnera] " +
    " ,[VrstaKontejnera]      ,[NalogIzdao]      ,[DatumRealizacije]      ,[Uvoznik] " +
    "   ,[CarinskiPostupak]        ,[SpedicijaRTC]        ,[NazivBrodara]      ,[VrstaRobe] " +
    "   ,[SaSkladista]      ,[SaPozicijeSklad]      ,[IdUsluge]      ,[NalogRealizovao] " +
    "   ,[OtpremaID]      ,[Kamion]      ,[Zavrsen]      ,[NalogID], Uvoz, Izvoznik " +
  "  FROM [dbo].[RNOtpremaPlatforme] " +
             " where ID = " + txtID.Text, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                if (dr["Uvoz"].ToString() == "0")
                {
                    chkUvoz.Checked = true;
                    chkIzvoz.Checked = false;
                }

                if (dr["Uvoz"].ToString() == "1")
                {
                    chkUvoz.Checked = false;
                    chkIzvoz.Checked = true;
                }
                txtDatumRasporeda.Value = Convert.ToDateTime(dr["DatumRasporeda"].ToString());
                txtbrojkontejnera.Text = dr["BrojKontejnera"].ToString();
                cboVrstaKontejnera.SelectedValue = Convert.ToInt32(dr["VrstaKontejnera"].ToString());
                txtRegBr.Text = dr["Kamion"].ToString();
                txtNalogIzdao.Text = dr["NalogIzdao"].ToString();
                txtDatumRealizacije.Value = Convert.ToDateTime(dr["DatumRealizacije"].ToString());
                cboUvoznik.SelectedValue = Convert.ToInt32(dr["Uvoznik"].ToString());
                cboPostupak.SelectedValue = Convert.ToInt32(dr["CarinskiPostupak"].ToString());
                cboSpedicija.SelectedValue = Convert.ToInt32(dr["SpedicijaRTC"].ToString());

                cboBrodar.SelectedValue = Convert.ToInt32(dr["NazivBrodara"].ToString());
                cboVrstaRobe.SelectedValue = Convert.ToInt32(dr["VrstaRobe"].ToString());
                cboSaSklad.SelectedValue = Convert.ToInt32(dr["SaSkladista"].ToString());
                cboSaPoz.SelectedValue = Convert.ToInt32(dr["SaSkladista"].ToString());
                cboUsluga.SelectedValue = Convert.ToInt32(dr["IdUsluge"].ToString());
                txtNalogRealizovao.Text = dr["NalogRealizovao"].ToString();

                txtOtpremaID.Text = dr["OtpremaID"].ToString();
                txtRegBr.Text = dr["Kamion"].ToString();
                txtNalogID.Text = dr["NalogID"].ToString();
                cboIzvoznik.SelectedValue = Convert.ToInt32(dr["Izvoznik"].ToString());
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
            if (chkUvoz.Checked == true)
            {
                RadniNalozi.InsertRN ir = new InsertRN();
                ir.InsRN6OtpremaPlatformeKam(Convert.ToDateTime(txtDatumRasporeda.Value), txtNalogIzdao.Text, Convert.ToDateTime(txtDatumRealizacije.Text), Convert.ToInt32(0), Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue), Convert.ToInt32(cboUsluga.SelectedValue), "", "", Convert.ToInt32(txtOtpremaID.Text), txtRegBr.Text, Convert.ToInt32(txtNalogID.Text),0,0);
                FillGV();
            }
            if (chkIzvoz.Checked == true)
            {
                RadniNalozi.InsertRN ir = new InsertRN();
                ir.InsRN6OtpremaPlatformeKam(Convert.ToDateTime(txtDatumRasporeda.Value), txtNalogIzdao.Text, Convert.ToDateTime(txtDatumRealizacije.Text), Convert.ToInt32(0), Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue), Convert.ToInt32(cboUsluga.SelectedValue), "", "", Convert.ToInt32(txtOtpremaID.Text), txtRegBr.Text, Convert.ToInt32(txtNalogID.Text), 1, 0);
                FillGVIzvozni();
            }
           
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Saobracaj.RadniNalozi.frmDodelaSkladista ds = new frmDodelaSkladista(txtOtpremaID.Text, 6);
            ds.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            InsertRN up = new InsertRN();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected == true)
                {
                    up.PotvrdiUradjenRN6(Convert.ToInt32(row.Cells[0].Value.ToString()));
                }

            }
        }

        private void chkUvoz_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUvoz.Checked == true)
            {
                FillGV();
                chkIzvoz.Checked = false;
            
            }
            else
            {
                FillGVIzvozni();
                chkIzvoz.Checked = true;
            }

        }
    }
}
