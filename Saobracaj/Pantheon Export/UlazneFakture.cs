﻿using Saobracaj.Sifarnici;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Pantheon_Export
{
    public partial class UlazneFakture : Form
    {
        int rb;
        int ID, Predvidjanje, Dobavljac, Referent;
        string VrstaDokumenta, TipDokumenta, Valuta, FakturaBr, RacunDobavljaca, Napomena;
        DateTime DatumPrijema, DatumIzdavanja, DatumPDVa, DatumValute;
        decimal Kurs;
        string korisnik = frmLogovanje.user;

        private string connect = Sifarnici.frmLogovanje.connectionString;
        private bool status = false;
        public UlazneFakture()
        {
            InitializeComponent();
            FillCombo();
        }

        public UlazneFakture(int id, int predvidjanje, string vrstaDokumenta, string tipDokumenta, DateTime datumPrijema, string valuta, decimal kurs, string fakturaBr, int dobavljac, string racunDobavljaca, DateTime datumIzdavanja,
            DateTime datumPDVa, DateTime datumValute, int referent, string napomena)
        {
            InitializeComponent();
            ID = id;
            txtID.Text = ID.ToString();
            FillCombo();
            FillGV();

            cboCRM.SelectedValue = Convert.ToInt32(predvidjanje.ToString());
            cboVrstaDok.Text = vrstaDokumenta.ToString();
            cboTip.Text = tipDokumenta.ToString();
            dtPrijem.Value = Convert.ToDateTime(datumPrijema.ToShortDateString());
            cboValuta.SelectedValue = valuta.ToString();
            txtKurs.Value = kurs;
            txtBrFakture.Text = fakturaBr.ToString();
            cboDobavljac.SelectedValue = Convert.ToInt32(dobavljac.ToString());
            txtRacunDobavljaca.Text = racunDobavljaca.ToString();
            dtIzdavanje.Value = Convert.ToDateTime(datumIzdavanja.ToShortDateString());
            dtPDV.Value = Convert.ToDateTime(datumPDVa.ToShortDateString());
            dtValute.Value = Convert.ToDateTime(datumValute.ToShortDateString());
            cboReferent.SelectedValue = Convert.ToInt32(referent.ToString());
            txtNapomena.Text = napomena.ToString();

        }
        private void UlazneFakture_Load(object sender, EventArgs e)
        {

        }

        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connect);

            var crm = "SELECT MIN(id) AS ID, PredvidjanjeId FROM predvidjanje GROUP BY PredvidjanjeId ORDER BY id DESC";
            var crmDa = new SqlDataAdapter(crm, conn);
            var crmDS = new DataSet();
            crmDa.Fill(crmDS);
            cboCRM.DataSource = crmDS.Tables[0];
            cboCRM.DisplayMember = "PredvidjanjeID";
            cboCRM.ValueMember = "ID";

            cboVrstaDok.Items.Add("RA-Račun");
            cboVrstaDok.Items.Add("AV-Avansni račun");
            cboVrstaDok.Items.Add("DO-Knjižno odobrenje");
            cboVrstaDok.Items.Add("BR-Knjižno zaduženje");

            cboTip.Items.Add("1000 - Ulazni računi - domaći transport");
            cboTip.Items.Add("1010 - Ulazni računi - medj.transport");
            cboTip.Items.Add("1020 - Ulazni računi - medj.transport INO");
            cboTip.Items.Add("1X00 - Finansijko zaduženje/odobrenje INO");
            cboTip.Items.Add("1X10 - Finansijsko zaduženje (dokumant o povećanju) sa PDVom");
            cboTip.Items.Add("1X20 - Finansijsko odobrenje (dokumant o smanjenju ) sa PDVom");
            cboTip.Items.Add("1X30 - Finansijsko zaduženje/odobrenje bez PDV");


            var valuta = "Select VaSifra,VaNaziv From Valute";
            var valutaDa = new SqlDataAdapter(valuta, conn);
            var valutaDS = new DataSet();
            valutaDa.Fill(valutaDS);
            cboValuta.DataSource = valutaDS.Tables[0];
            cboValuta.DisplayMember = "VaNaziv";
            cboValuta.ValueMember = "VaSifra";

            var dobavljac = "Select PaSifra,PaNaziv from Partnerji Where Supplier='T' order by PaSifra";
            var dobavljacDa = new SqlDataAdapter(dobavljac, conn);
            var dobavljacDS = new DataSet();
            dobavljacDa.Fill(dobavljacDS);
            cboDobavljac.DataSource = dobavljacDS.Tables[0];
            cboDobavljac.DisplayMember = "PaNAziv";
            cboDobavljac.ValueMember = "PaSifra";

            var referent = "Select Korisnici.DeSifra as DeSifra, (RTrim(DeIme) + ' ' + Rtrim(DePriimek)) as Opis From Korisnici inner join Delavci on Korisnici.DeSifra=Delavci.DeSifra order by Korisnici.DeSifra";
            var referentDa = new SqlDataAdapter(referent, conn);
            var referentDS = new DataSet();
            referentDa.Fill(referentDS);
            cboReferent.DataSource = referentDS.Tables[0];
            cboReferent.DisplayMember = "Opis";
            cboReferent.ValueMember = "DeSifra";


            var Mp = "Select MpSifra,(RTrim(MpStaraSif)+'-'+RTrim(MpNaziv)) as MpNaziv from MaticniPodatki";
            var MpDa = new SqlDataAdapter(Mp, conn);
            var MpDS = new DataSet();
            MpDa.Fill(MpDS);
            cboMP.DataSource = MpDS.Tables[0];
            cboMP.DisplayMember = "MpNaziv";
            cboMP.ValueMember = "MpSifra";

            var nosilac = "Select ID,RTrim(NazivNosiocaTroska) as NazivNosiocaTroska from NosiociTroskova order by ID desc";
            var nosilacDA = new SqlDataAdapter(nosilac, conn);
            var nosilacDS = new DataSet();
            nosilacDA.Fill(nosilacDS);
            cboNosilac.DataSource = nosilacDS.Tables[0];
            cboNosilac.DisplayMember = "NazivNosiocaTroska";
            cboNosilac.ValueMember = "ID";

            var jm = "Select RTrim(MeNaziv) as MeNaziv from MerskeEnote order by MeSifra";
            var jmDa = new SqlDataAdapter(jm, conn);
            var jmDS = new DataSet();
            jmDa.Fill(jmDS);
            cboJM.DataSource = jmDS.Tables[0];
            cboJM.DisplayMember = "MeNaziv";
            cboJM.ValueMember = "MeNaziv";
        }
        private void FillGV()
        {
            ID = Convert.ToInt32(txtID.Text);
            var select = "select UlFakPostav.ID,IDFak,RB,MP,Rtrim(MpStaraSif) as Ident,Kolicina,Cena,UlFakPostav.NosilacTroska as NT,RTrim(NazivNosiocaTroska) as NazivNosiocaTroska,JM,Proizvod,Oznaka,Valuta from UlFakPostav inner join UlFak on UlFakPostav.IDFak=UlFak.ID inner join MaticniPodatki on UlFakPostav.Mp=MaticniPodatki.MpSifra inner join NosiociTroskova on UlFakPostav.NosilacTroska=NosiociTroskova.ID inner join Najava on UlFakPostav.NajavaID=Najava.ID Where IDFak=" + txtID.Text;
            SqlConnection conn = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];


            dataGridView1.BorderStyle = BorderStyle.FixedSingle;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Width = 50;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[5].Width = 100;
            dataGridView1.Columns[8].Width = 200;
            dataGridView1.Columns[7].Visible = false;
            //dataGridView1.Columns["Valuta"].Visible = false;

            if (dataGridView1.Rows.Count == 0) { rb = 1; } else { rb = dataGridView1.Rows.Count + 1; }
            txtRB.Text = rb.ToString();
        }

        private void dataGridView1_AllowUserToAddRowsChanged(object sender, EventArgs e)
        {

        }
        string crmID;
        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            string year = DateTime.Now.Year.ToString("yyyy");

            string query = "Select (Max(ID) + 1) as id from UlFak";
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr["id"].GetType() == typeof(DBNull))
                { ID = Convert.ToInt32(year + 1.ToString("".PadLeft(4, '0'))); }
                else
                {
                    ID = Convert.ToInt32(dr["id"].ToString());
                }
            }
            conn.Close();
            txtID.Text = ID.ToString();
            string query2 = "SELECT MAX(crmID)+1 AS CrmID FROM (SELECT crmID FROM faktura UNION ALL SELECT crmID FROM ulfak) AS CRMID";
            conn.Open();
            SqlCommand cmd2 = new SqlCommand(query2, conn);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                crmID = dr2[0].ToString();
            }
            conn.Close();

        }
        int IDVeza;
        private void button2_Click(object sender, EventArgs e)
        {
            string query = "Select ID from UlFak Where ID=" + Convert.ToInt32(txtID.Text);
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == false)
            {
                MessageBox.Show("Mora se prvo sačuvati zaglavlje fakture!"); return;
            }
            conn.Close();

            string query2 = "Select IDP From Predvidjanje Where ID=" + Convert.ToInt32(cboCRM.SelectedValue);
            conn.Open();
            SqlCommand cmd2 = new SqlCommand(query2, conn);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                IDVeza = Convert.ToInt32(dr2[0].ToString());
            }
            conn.Close();

            InsertPatheonExport ins = new InsertPatheonExport();

            using (SqlConnection conn3 = new SqlConnection(connect))
            {
                using (SqlCommand cmd3 = conn3.CreateCommand())
                {
                    cmd3.CommandText = "delete from UlFakPostav Where IDFak=" + Convert.ToInt32(txtID.Text);
                    conn3.Open();
                    cmd3.ExecuteNonQuery();
                    conn3.Close();
                }
            }
            ins.PoveziPredvidjanje(Convert.ToInt32(txtID.Text), IDVeza);
            FillGV();

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void numericUpDown1_Leave(object sender, EventArgs e)
        {
            txtCena.Value = Convert.ToDecimal(txtKolicina.Value) * Convert.ToDecimal(numericUpDown1.Value);
        }

        private void txtCena_Leave(object sender, EventArgs e)
        {
            numericUpDown1.Value = Convert.ToDecimal(txtCena.Value) / Convert.ToDecimal(txtKolicina.Value);
        }
        private void cboValuta_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void tsDelete_Click(object sender, EventArgs e)
        {

        }

        int posao;
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0) { rb = 1; } else { rb = dataGridView1.Rows.Count + 1; }

            GetNosilacInfo();

            decimal iznosRSD;
            if (sifDr == 82 && cboValuta.SelectedValue!="RSD")
            {
                iznosRSD = Convert.ToDecimal(txtKurs.Value) * Convert.ToDecimal(txtCena.Value);
            }
            else
            {
                iznosRSD = Convert.ToDecimal(txtCena.Value);
            }

            InsertPatheonExport ins = new InsertPatheonExport();
            ins.InsUlFakPostav(Convert.ToInt32(txtID.Text), rb, Convert.ToInt32(cboMP.SelectedValue), Convert.ToDecimal(txtKolicina.Text), Convert.ToDecimal(txtCena.Text), Convert.ToInt32(cboNosilac.SelectedValue), cboJM.SelectedValue.ToString().TrimEnd(), " ", posao, iznosRSD);
            FillGV();
        }

        private void cboMP_SelectedValueChanged(object sender, EventArgs e)
        {

        }
        int sifDr;
        private void tsSave_Click(object sender, EventArgs e)
        {
            InsertPatheonExport ins = new InsertPatheonExport();
            SqlConnection conn = new SqlConnection(connect);
            string query2 = "Select PaSifDrzave from Partnerji Where PaSifra=" + Convert.ToInt32(cboDobavljac.SelectedValue);
            conn.Open();
            SqlCommand cmd2 = new SqlCommand(query2, conn);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                sifDr = Convert.ToInt32(dr2[0].ToString());
            }
            conn.Close();
            string valuta = cboValuta.SelectedValue.ToString();
            if (sifDr == 82 && valuta != "RSD")
            {
                DialogResult dr = MessageBox.Show("Za domaćeg dobavljača dokument treba biti u dinarima\nDa li želite da potvrdite dokument u valuti " + valuta + " po kursu:" + txtKurs.Value.ToString(), "Potvrda valute", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    if (status == true)
                    {
                        ins.InsUlFak(Convert.ToInt32(txtID.Text), cboCRM.SelectedValue.ToString(), cboVrstaDok.Text.ToString().Substring(0, 2), txtBrFakture.Text.ToString(), Convert.ToInt32(cboDobavljac.SelectedValue), cboTip.Text.ToString().Substring(0, 4),
                            Convert.ToDateTime(dtPrijem.Value.ToString()), cboValuta.SelectedValue.ToString(), Convert.ToDecimal(txtKurs.Text), txtRacunDobavljaca.Text.ToString(), Convert.ToDateTime(dtIzdavanje.Value.ToString()), Convert.ToDateTime(dtPDV.Value.ToString()),
                            Convert.ToDateTime(dtValute.Value.ToString()), Convert.ToInt32(cboReferent.SelectedValue), Convert.ToInt32(cboCRM.SelectedValue), txtNapomena.Text.ToString().TrimEnd(), Convert.ToInt32(crmID), korisnik);
                    }
                    else
                    {
                        ins.UpdUlFak(Convert.ToInt32(txtID.Text), txtBrFakture.Text.ToString(), Convert.ToInt32(cboDobavljac.SelectedValue),
                            Convert.ToDateTime(dtPrijem.Value.ToString()), cboValuta.SelectedValue.ToString(), Convert.ToDecimal(txtKurs.Text), txtRacunDobavljaca.Text.ToString(), Convert.ToDateTime(dtIzdavanje.Value.ToString()), Convert.ToDateTime(dtPDV.Value.ToString()),
                            Convert.ToDateTime(dtValute.Value.ToString()), Convert.ToInt32(cboReferent.SelectedValue), Convert.ToInt32(cboCRM.SelectedValue), txtNapomena.Text.ToString().TrimEnd(), korisnik);
                    }
                    FillGV();
                }
                else if (dr == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                if (status == true)
                {
                    ins.InsUlFak(Convert.ToInt32(txtID.Text), cboCRM.SelectedValue.ToString(), cboVrstaDok.Text.ToString().Substring(0, 2), txtBrFakture.Text.ToString(), Convert.ToInt32(cboDobavljac.SelectedValue), cboTip.Text.ToString().Substring(0, 4),
                        Convert.ToDateTime(dtPrijem.Value.ToString()), cboValuta.SelectedValue.ToString(), Convert.ToDecimal(txtKurs.Text), txtRacunDobavljaca.Text.ToString(), Convert.ToDateTime(dtIzdavanje.Value.ToString()), Convert.ToDateTime(dtPDV.Value.ToString()),
                        Convert.ToDateTime(dtValute.Value.ToString()), Convert.ToInt32(cboReferent.SelectedValue), Convert.ToInt32(cboCRM.SelectedValue), txtNapomena.Text.ToString().TrimEnd(), Convert.ToInt32(crmID), korisnik);
                }
                else
                {
                    ins.UpdUlFak(Convert.ToInt32(txtID.Text), txtBrFakture.Text.ToString(), Convert.ToInt32(cboDobavljac.SelectedValue),
                             Convert.ToDateTime(dtPrijem.Value.ToString()), cboValuta.SelectedValue.ToString(), Convert.ToDecimal(txtKurs.Text), txtRacunDobavljaca.Text.ToString(), Convert.ToDateTime(dtIzdavanje.Value.ToString()), Convert.ToDateTime(dtPDV.Value.ToString()),
                             Convert.ToDateTime(dtValute.Value.ToString()), Convert.ToInt32(cboReferent.SelectedValue), Convert.ToInt32(cboCRM.SelectedValue), txtNapomena.Text.ToString().TrimEnd(), korisnik);
                }
                FillGV();
            }
            status = false;
        }

        private void cboMP_Leave(object sender, EventArgs e)
        {

        }
        int IDPostav;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {

                        IDPostav = Convert.ToInt32(row.Cells[0].Value.ToString());
                        txtRB.Text = row.Cells["RB"].Value.ToString();
                        cboMP.SelectedValue = Convert.ToInt32(row.Cells["MP"].Value);
                        txtKolicina.Value = Convert.ToDecimal(row.Cells["Kolicina"].Value);
                        txtCena.Value = Convert.ToDecimal(row.Cells["Cena"].Value);
                        cboNosilac.SelectedValue = Convert.ToInt32(row.Cells["NT"].Value);
                        cboJM.SelectedValue = row.Cells["JM"].Value.ToString();
                        numericUpDown1.Value = Convert.ToDecimal(txtCena.Value) / Convert.ToDecimal(txtKolicina.Value);
                    }
                }
            }
            catch { }
        }



        private void btnObrisi_Click(object sender, EventArgs e)
        {
            InsertPatheonExport ins = new InsertPatheonExport();
            ins.DelUlFakPostav(IDPostav);
            FillGV();
        }
        private void GetNosilacInfo()
        {
            string query2 = "SELECT Posao From NosiociTroskova Where ID=" + Convert.ToInt32(cboNosilac.SelectedValue);
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd2 = new SqlCommand(query2, conn);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                posao = Convert.ToInt32(dr2[0].ToString());
            }
            conn.Close();
        }
        private void cboNosilac_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }
    }
}
//trimovi za combo
//stavke faktura, naziv samo zop
//predvidjanje neka napomana