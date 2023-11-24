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

namespace Saobracaj.Pantheon_Export
{
    public partial class IzlazneFakture : Form
    {
        int ID;
        private string connect = Sifarnici.frmLogovanje.connectionString;
        private bool status = false;
        public IzlazneFakture()
        {
            InitializeComponent();
            FillCombo();
            FillGV();
        }
        public IzlazneFakture(int id,DateTime datumDokumenta,int primalac,string valuta,decimal kurs,DateTime datumPDV,DateTime datumValute,string mestoUtovara,DateTime datumUtovara,string mestoIstovara,DateTime datumIstovara,int referent,string izjava,string napomena)
        {
            InitializeComponent();

            ID = id;
            txtID.Text = ID.ToString();
            dtDatum.Value=datumDokumenta;
            cboPrimalac.SelectedValue = primalac;
            cboValuta.SelectedValue= valuta;
            txtKurs.Text = kurs.ToString();
            dtPDV.Value = datumPDV;
            dtValuta.Value = datumValute;
            txtMestoUtovara.Text= mestoUtovara.ToString();
            dtDatumUtovara.Value = datumUtovara;
            txtMestoIstovara.Text = mestoIstovara.ToString();
            dtDatumIstovara.Value= datumIstovara;
            cboReferent.SelectedValue = referent;
            txtIzjava.Text = izjava.ToString();
            txtNapomena.Text=napomena.ToString();

            FillCombo();
            FillGV();
        }
        private void FillGV()
        {
            if (txtID.Text != "")
            {
                var select = "select FaPStFak as Faktura,FaPstPos as RB,FaPSifra as MP,FaPNaziv,FaPEM as JM,FaPKolOdpr as Kolicna,FapCenaEM as Cena,NosilacTroska,NajavaID From FakturaPostav Where FaPStFak=" + ID;
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
                dataGridView1.Columns[1].Width = 120;
                dataGridView1.Columns[2].Width = 80;
                dataGridView1.Columns[3].Width = 80;
                dataGridView1.Columns[4].Width = 100;
                dataGridView1.Columns[5].Width = 100;

                if (dataGridView1.Rows.Count == 0) { rb = 1; } else { rb = dataGridView1.Rows.Count + 1; }
                txtRB.Text = rb.ToString();
            }
            else { MessageBox.Show("nema"); }
            
        }
        private void IzlazneFakture_Load(object sender, EventArgs e)
        {
            
        }
        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connect);

            var valuta = "Select RTrim(VaSifra) as VaSifra,VaNaziv From Valute";
            var valutaDa = new SqlDataAdapter(valuta, conn);
            var valutaDS = new DataSet();
            valutaDa.Fill(valutaDS);
            cboValuta.DataSource = valutaDS.Tables[0];
            cboValuta.DisplayMember = "VaNaziv";
            cboValuta.ValueMember = "VaSifra";

            var referent = "Select Korisnici.DeSifra as DeSifra, (RTrim(DeIme) + ' ' + Rtrim(DePriimek)) as Opis From Korisnici inner join Delavci on Korisnici.DeSifra=Delavci.DeSifra order by Korisnici.DeSifra";
            var referentDa = new SqlDataAdapter(referent, conn);
            var referentDS = new DataSet();
            referentDa.Fill(referentDS);
            cboReferent.DataSource = referentDS.Tables[0];
            cboReferent.DisplayMember = "Opis";
            cboReferent.ValueMember = "DeSifra";

            var dobavljac = "Select PaSifra,PaNaziv from Partnerji order by PaSifra";
            var dobavljacDa = new SqlDataAdapter(dobavljac, conn);
            var dobavljacDS = new DataSet();
            dobavljacDa.Fill(dobavljacDS);
            cboPrimalac.DataSource = dobavljacDS.Tables[0];
            cboPrimalac.DisplayMember = "PaNAziv";
            cboPrimalac.ValueMember = "PaSifra";

            var Mp = "Select MpSifra,MpNaziv from MaticniPodatki";
            var MpDa = new SqlDataAdapter(Mp, conn);
            var MpDS = new DataSet();
            MpDa.Fill(MpDS);
            cboMP.DataSource = MpDS.Tables[0];
            cboMP.DisplayMember = "MpNaziv";
            cboMP.ValueMember = "MpSifra";

            var nosilac = "Select ID,NazivNosiocaTroska from NosiociTroskova order by ID";
            var nosilacDA = new SqlDataAdapter(nosilac, conn);
            var nosilacDS = new DataSet();
            nosilacDA.Fill(nosilacDS);
            cboNosilac.DataSource = nosilacDS.Tables[0];
            cboNosilac.DisplayMember = "NazivNosiocaTroska";
            cboNosilac.ValueMember = "ID";

            var najava = "Select ID,Oznaka from Najava Where Status <>7 or status <>9 order by ID desc";
            var najavaDA = new SqlDataAdapter(najava, conn);
            var najavaDS = new DataSet();   
            najavaDA.Fill(najavaDS);
            cboNajava.DataSource = najavaDS.Tables[0];
            cboNajava.DisplayMember = "Oznaka";
            cboNajava.ValueMember= "ID";
        }
        string referent;
        private void cboReferent_SelectedValueChanged(object sender, EventArgs e)
        {

        }
        string naziv, ulica, mesto, mb;
        string FaStFak;
        private void tsNew_Click(object sender, EventArgs e)
        {
            string year = DateTime.Now.Year.ToString("yyyy");
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
                    FaStFak = dr["id"].ToString();
                }
            }
            conn.Close();
            txtID.Text = FaStFak.ToString();
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            InsertPatheonExport ins = new InsertPatheonExport();
            if (status == true)
            {
                ins.InstFaktura(Convert.ToInt32(txtID.Text), Convert.ToDateTime(dtDatum.Value), Convert.ToInt32(cboPrimalac.SelectedValue), ulica, naziv, mesto, mb, cboValuta.SelectedValue.ToString(), Convert.ToDecimal(txtKurs.Text),
                    Convert.ToDateTime(dtPDV.Value), Convert.ToDateTime(dtValuta.Value), txtMestoUtovara.Text.ToString(), Convert.ToDateTime(dtDatumUtovara.Value), txtMestoUtovara.Text.ToString().TrimEnd(),
                    Convert.ToDateTime(dtDatumIstovara.Value), referent, Convert.ToInt32(cboReferent.SelectedValue), txtIzjava.Text.ToString().TrimEnd(), txtNapomena.Text.ToString().TrimEnd());
            }
            else
            {
                //FALI UPDATE FAKTURE
            }
            FillGV();
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
            var query = "Select PaNaziv,PaUlicaHisnaSt,PaKraj,PaDMatSt From Partnerji Where PaSifra=" + Convert.ToInt32(cboPrimalac.SelectedValue);
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
            }
            conn.Close();
        }

        private void cboMP_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string query = "Select MpSifENoteMere1,MpNaziv From MaticniPodatki Where MPSifra=" + Convert.ToString(cboMP.SelectedValue);
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtJM.Text = dr[0].ToString();
                MpNaziv = dr[1].ToString().TrimEnd();
            }
            conn.Close();
        }

        int rb;

        private void button1_Click(object sender, EventArgs e)
        {
            var query = "Select korisnik From Korisnici Where DeSifra=" + Convert.ToInt32(cboReferent.SelectedValue);
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                referent = dr[0].ToString();
            }
            conn.Close();
            InsertPatheonExport ins = new InsertPatheonExport();
            ins.InsFakturaPostav(referent, Convert.ToInt32(txtID.Text), rb, Convert.ToInt32(cboMP.SelectedValue), MpNaziv, txtJM.Text, Convert.ToDecimal(txtKolicina.Text), Convert.ToDecimal(txtCena.Text), Convert.ToInt32(cboNosilac.SelectedValue), Convert.ToInt32(cboNajava.SelectedValue));
            FillGV();
        }

        private void cboPrimalac_SelectedValueChanged(object sender, EventArgs e)
        {

        }
    }
}
