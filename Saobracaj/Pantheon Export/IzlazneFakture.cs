using Microsoft.Ajax.Utilities;
using Saobracaj.eDokumenta;
using Saobracaj.Sifarnici;
using Syncfusion.Windows.Forms.Diagram;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Syncfusion.WinForms.Core.NativeScroll;

namespace Saobracaj.Pantheon_Export
{
    public partial class IzlazneFakture : Form
    {
        int ID,StatusFak;
        private string connect = Sifarnici.frmLogovanje.connectionString;
        private bool status = false;
        private string korisnik = frmLogovanje.user.ToString().TrimEnd();
        public IzlazneFakture()
        {
            InitializeComponent();
            FillCombo();
        }
        public IzlazneFakture(int id,DateTime datumDokumenta,string vrsta,int primalac,string valuta,decimal kurs,DateTime datumPDV,DateTime datumValute,string mestoUtovara,DateTime datumUtovara,string mestoIstovara,DateTime datumIstovara,int referent,int izjava,string napomena,int statusFak)
        {
            InitializeComponent();
            ID = id;
            txtID.Text = ID.ToString();
            FillCombo();
            FillGV();

            if (vrsta.ToString().TrimEnd().Equals("3010"))
            {
                vrsta = "3010 - Domaci železnicki transport";
            }
            else if (vrsta.ToString().TrimEnd().Equals("3100"))
            {
                vrsta = "3100 - Domaci rečni transport";
            }
            else if (vrsta.ToString().TrimEnd().Equals("3110"))
            {
                vrsta = "3110 - Medj.transport - ŽELEZNIČKI";
            }
            else if (vrsta.ToString().TrimEnd().Equals("3X00"))
            {
                vrsta = "3X00 - Odobrenje inostranstvo";
            }
            else if (vrsta.ToString().TrimEnd().Equals("3X20"))
            {
                vrsta = "3X20 - Finansijsko odobrenje";
            }
            dtDatum.Value = Convert.ToDateTime(datumDokumenta.ToShortDateString());
            comboBox1.Text = vrsta;
            cboPrimalac.SelectedValue = primalac;
            cboValuta.SelectedValue= valuta;
            txtKurs.Value = Convert.ToDecimal(kurs);
            dtPDV.Value = Convert.ToDateTime(datumPDV.ToShortDateString());
            txtMestoUtovara.Text= mestoUtovara.ToString();
            dtDatumUtovara.Value = Convert.ToDateTime(datumUtovara.ToShortDateString());
            txtMestoIstovara.Text = mestoIstovara.ToString();
            dtDatumIstovara.Value = Convert.ToDateTime(datumIstovara.ToShortDateString());
            dtValuta.Value = Convert.ToDateTime(datumValute.ToShortDateString());
            cboReferent.SelectedValue= referent;
            cboIzjava.SelectedValue = izjava;
            txtNapomena.Text = napomena.ToString();
            StatusFak=statusFak;


        }
        private void FillGV()
        {
            ID = Convert.ToInt32(txtID.Text);
                var select = "select FaPStFak as Faktura,FaPstPos as RB,FaPSifra as MP,RTrim(MpStaraSif)+'-'+FaPNaziv as Naziv,FaPEM as JM,FaPKolOdpr as Kolicna,FapCenaEM as Cena,NosilacTroska,NajavaID,FapFakZap From FakturaPostav inner join MaticniPodatki on FakturaPostav.FaPsifra=MaticniPodatki.MpSifra Where FaPStFak=" + ID;
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

                dataGridView1.Columns[0].Width = 60;
                dataGridView1.Columns[1].Width = 60;
                dataGridView1.Columns[2].Width = 60;
                dataGridView1.Columns[3].Width = 280;
                dataGridView1.Columns[4].Width = 100;
                dataGridView1.Columns[5].Width = 100;
                dataGridView1.Columns[9].Visible = false;

                if (dataGridView1.Rows.Count == 0) { rb = 1; } else { rb = dataGridView1.Rows.Count + 1; }
                txtRB.Text = rb.ToString();
        
        }
        private void IzlazneFakture_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("3010 - Domaci železnicki transport");
            comboBox1.Items.Add("3100 - Domaci rečni transport");
            comboBox1.Items.Add("3110 - Medj.transport - ŽELEZNIČKI");
            comboBox1.Items.Add("3X00 - Odobrenje inostranstvo");
            comboBox1.Items.Add("3X20 - Finansijsko odobrenje");
        }
        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connect);

            var valuta = "Select RTrim(VaSifra) as VaSifra,RTrim(VaNaziv) as VaNaziv From Valute";
            var valutaDa = new SqlDataAdapter(valuta, conn);
            var valutaDS = new DataSet();
            valutaDa.Fill(valutaDS);
            cboValuta.DataSource = valutaDS.Tables[0];
            cboValuta.DisplayMember = "VaNaziv";
            cboValuta.ValueMember = "VaSifra";

            var referent = "Select Korisnici.DeSifra as DeSifra, RTrim((RTrim(DeIme) + ' ' + Rtrim(DePriimek))) as Opis From Korisnici inner join Delavci on Korisnici.DeSifra=Delavci.DeSifra order by Korisnici.DeSifra";
            var referentDa = new SqlDataAdapter(referent, conn);
            var referentDS = new DataSet();
            referentDa.Fill(referentDS);
            cboReferent.DataSource = referentDS.Tables[0];
            cboReferent.DisplayMember = "Opis";
            cboReferent.ValueMember = "DeSifra";

            var dobavljac = "Select PaSifra,RTrim(PaNaziv) as PaNaziv from Partnerji Where Buyer='T' order by PaSifra";
            var dobavljacDa = new SqlDataAdapter(dobavljac, conn);
            var dobavljacDS = new DataSet();
            dobavljacDa.Fill(dobavljacDS);
            cboPrimalac.DataSource = dobavljacDS.Tables[0];
            cboPrimalac.DisplayMember = "PaNAziv";
            cboPrimalac.ValueMember = "PaSifra";

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


            var query3 = "Select ID, RTrim(Naziv) as Naziv from Izjave";
            var da3 = new SqlDataAdapter(query3, conn);
            var ds3 = new DataSet();
            da3.Fill(ds3);
            cboIzjava.DataSource= ds3.Tables[0];
            cboIzjava.DisplayMember = "Naziv";
            cboIzjava.ValueMember = "ID";

            var jm = "Select MeSifra,RTrim(MeNaziv) as MeNaziv from MerskeEnote order by MeSifra";
            var jmDa = new SqlDataAdapter(jm, conn);
            var jmDS = new DataSet();
            jmDa.Fill(jmDS);
            cboJM.DataSource = jmDS.Tables[0];
            cboJM.DisplayMember = "MeNaziv";
            cboJM.ValueMember = "MeSifra";
        }
        
        private void cboReferent_SelectedValueChanged(object sender, EventArgs e)
        {

        }
        string naziv, ulica, mesto, mb;
        string FaStFak;
        string crmID;
        private void tsNew_Click(object sender, EventArgs e)
        {
            string year = DateTime.Now.ToString("yyyy");
            status = true;
            string query = "Select (Max(FaStFak) + 1) as id from Faktura";
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr["id"].GetType() == typeof(DBNull))
                { FaStFak =year+ 1.ToString("".PadLeft(4,'0')); }
                else
                {
                    FaStFak =  dr["id"].ToString();
                }
            }
            conn.Close();
            txtID.Text = FaStFak.ToString();
            string query2 = "SELECT MAX(crmID)+1 AS CrmID FROM (SELECT crmID FROM faktura UNION ALL SELECT crmID FROM ulfak) AS CRMID";
            conn.Open();
            SqlCommand cmd2 = new SqlCommand(query2, conn);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                crmID = dr2[0].ToString();
            }
            conn.Close();
            rb = 1;
        }
        int sifDr;
        private void tsSave_Click(object sender, EventArgs e)
        {
            var query = "Select PaNaziv,PaUlicaHisnaSt,PaKraj,PaDMatSt,PaSifDrzave From Partnerji Where PaSifra=" + Convert.ToInt32(cboPrimalac.SelectedValue);
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                naziv = dr[0].ToString().TrimEnd();
                ulica = dr[1].ToString().TrimEnd();
                mesto = dr[2].ToString().TrimEnd();
                mb = dr[3].ToString().TrimEnd();
                sifDr = Convert.ToInt32(dr[4].ToString());
            }
            conn.Close();
            string valuta = cboValuta.SelectedValue.ToString();
            InsertPatheonExport ins = new InsertPatheonExport();
            if (sifDr == 82 && valuta != "RSD")
            {
                DialogResult result = MessageBox.Show("Za domaćeg dobavljača dokument treba biti u dinarima\nDa li želite da potvrdite dokument u valuti " + valuta, "Potvrda valute", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (status == true)
                    {
                        txtID.Text = FaStFak.ToString();
                        ins.InstFaktura(Convert.ToInt32(txtID.Text), Convert.ToDateTime(dtDatum.Value), Convert.ToInt32(cboPrimalac.SelectedValue), ulica, naziv, mesto, mb, cboValuta.SelectedValue.ToString(), Convert.ToDecimal(txtKurs.Value),
                            Convert.ToDateTime(dtPDV.Value), Convert.ToDateTime(dtValuta.Value), txtMestoUtovara.Text.ToString(), Convert.ToDateTime(dtDatumUtovara.Value), txtMestoUtovara.Text.ToString().TrimEnd(),
                            Convert.ToDateTime(dtDatumIstovara.Value), korisnik, Convert.ToInt32(cboReferent.SelectedValue), Convert.ToInt32(cboIzjava.SelectedValue), txtNapomena.Text.ToString().TrimEnd(), Convert.ToInt32(crmID), comboBox1.Text.ToString().Substring(0, 4));
                    }
                    else
                    {
                        if (StatusFak == 0)
                        {
                            ins.UpdFaktura(Convert.ToInt32(txtID.Text), Convert.ToDateTime(dtDatum.Value), Convert.ToInt32(cboPrimalac.SelectedValue), ulica, naziv, mesto, mb, cboValuta.SelectedValue.ToString(), Convert.ToDecimal(txtKurs.Value),
                            Convert.ToDateTime(dtPDV.Value), Convert.ToDateTime(dtValuta.Value), txtMestoUtovara.Text.ToString(), Convert.ToDateTime(dtDatumUtovara.Value), txtMestoUtovara.Text.ToString().TrimEnd(),
                            Convert.ToDateTime(dtDatumIstovara.Value), korisnik, Convert.ToInt32(cboReferent.SelectedValue), Convert.ToInt32(cboIzjava.SelectedValue), txtNapomena.Text.ToString().TrimEnd(), Convert.ToInt32(crmID), comboBox1.Text.ToString().Substring(0, 4));
                        }
                        else
                        {
                            MessageBox.Show("Faktura je već sinhronizovana. Nije moguća izmena dokumenta nakon sinhrronizacije!");
                            return;
                        }
                    }
                    FillGV();
                }
                else if (result == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                if (status == true)
                {
                    txtID.Text = FaStFak.ToString();
                    ins.InstFaktura(Convert.ToInt32(txtID.Text), Convert.ToDateTime(dtDatum.Value), Convert.ToInt32(cboPrimalac.SelectedValue), ulica, naziv, mesto, mb, cboValuta.SelectedValue.ToString(), Convert.ToDecimal(txtKurs.Value),
                        Convert.ToDateTime(dtPDV.Value), Convert.ToDateTime(dtValuta.Value), txtMestoUtovara.Text.ToString(), Convert.ToDateTime(dtDatumUtovara.Value), txtMestoUtovara.Text.ToString().TrimEnd(),
                        Convert.ToDateTime(dtDatumIstovara.Value), korisnik, Convert.ToInt32(cboReferent.SelectedValue), Convert.ToInt32(cboIzjava.SelectedValue), txtNapomena.Text.ToString().TrimEnd(), Convert.ToInt32(crmID), comboBox1.Text.ToString().Substring(0, 4));
                }
                else
                {
                    if (StatusFak == 0)
                    {
                        ins.UpdFaktura(Convert.ToInt32(txtID.Text), Convert.ToDateTime(dtDatum.Value), Convert.ToInt32(cboPrimalac.SelectedValue), ulica, naziv, mesto, mb, cboValuta.SelectedValue.ToString(), Convert.ToDecimal(txtKurs.Value),
                        Convert.ToDateTime(dtPDV.Value), Convert.ToDateTime(dtValuta.Value), txtMestoUtovara.Text.ToString(), Convert.ToDateTime(dtDatumUtovara.Value), txtMestoUtovara.Text.ToString().TrimEnd(),
                        Convert.ToDateTime(dtDatumIstovara.Value), korisnik, Convert.ToInt32(cboReferent.SelectedValue), Convert.ToInt32(cboIzjava.SelectedValue), txtNapomena.Text.ToString().TrimEnd(), Convert.ToInt32(crmID), comboBox1.Text.ToString().Substring(0, 4));
                    }
                    else
                    {
                        MessageBox.Show("Faktura je već sinhronizovana. Nije moguća izmena dokumenta nakon sinhrronizacije!");
                        return;
                    }
                }
                FillGV();
            }
            
            status = false;
        }
        string MpNaziv;
        private void cboMP_Move(object sender, EventArgs e)
        {

        }

        private void cboReferent_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void cboPrimalac_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }
        string staraSif;
        private void cboMP_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }
        int FaPFakZap;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach(DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        FaPFakZap = Convert.ToInt32(row.Cells[9].Value);
                    }
                }
            }
            catch { }
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            InsertPatheonExport ins = new InsertPatheonExport();
            ins.DelFakturaPostav(FaPFakZap);
            FillGV();
        }
        int posao;
        private void cboNosilac_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }
        private void GetMPInfo()
        {
            string query = "Select MpSifENoteMere1,MpNaziv,MpStaraSif From MaticniPodatki Where MPSifra=" + Convert.ToString(cboMP.SelectedValue);
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                //txtJM.Text = dr[0].ToString();
                MpNaziv = dr[1].ToString().TrimEnd();
                staraSif = dr[2].ToString().TrimEnd();
            }
            conn.Close();
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
        int rb;

        private void button1_Click(object sender, EventArgs e)
        {
            GetMPInfo();
            GetNosilacInfo();
            decimal iznosRSD;
            if (sifDr == 82)
            {
                iznosRSD=Convert.ToDecimal(txtKurs.Value)*Convert.ToDecimal(txtCena.Value);
            }
            else
            {
                iznosRSD = Convert.ToDecimal(txtCena.Value);
            }
            InsertPatheonExport ins = new InsertPatheonExport();
            ins.InsFakturaPostav(korisnik.ToString().TrimEnd(), Convert.ToInt32(txtID.Text), rb, Convert.ToInt32(cboMP.SelectedValue), MpNaziv, cboJM.SelectedValue.ToString().TrimEnd(), Convert.ToDecimal(txtKolicina.Text), Convert.ToDecimal(txtCena.Text), Convert.ToInt32(cboNosilac.SelectedValue), posao,iznosRSD);

            FillGV();
        }

        private void cboPrimalac_SelectedValueChanged(object sender, EventArgs e)
        {

        }
    }
}
