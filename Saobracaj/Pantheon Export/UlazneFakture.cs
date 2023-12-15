using Syncfusion.Windows.Forms.Chart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Pantheon_Export
{
    public partial class UlazneFakture : Form
    {
        int rb;
        int ID;
        private string connect = Sifarnici.frmLogovanje.connectionString;
        private bool status = false;
        public UlazneFakture()
        {
            InitializeComponent();
            FillCombo();
            FillGV();
        }
        public UlazneFakture(int id,int predvidjanje,string vrstaDokumenta, string tipDokumenta,DateTime datumPrijema,string valuta,decimal kurs,string fakturaBr,int dobavljac,string racunDobavljaca,DateTime datumIzdavanja,
            DateTime datumPDVa,DateTime datumValute,int referent,string napomena)
        {
            InitializeComponent();
            ID = id;
            FillCombo();
            FillGV();
            txtID.Text = ID.ToString();
            cboCRM.SelectedValue = predvidjanje;
            cboVrstaDok.Text = vrstaDokumenta;
            cboTip.Text= tipDokumenta;
            cboValuta.SelectedValue= valuta;
            txtKurs.Text = kurs.ToString();
            txtBrFakture.Text = fakturaBr.ToString();
            cboDobavljac.SelectedValue = dobavljac;
            txtRacunDobavljaca.Text = racunDobavljaca.ToString();
            dtIzdavanje.Value = datumIzdavanja;
            dtPDV.Value= datumPDVa;
            dtValute.Value= datumValute;
            cboReferent.SelectedValue = referent;
            txtNapomena.Text = napomena.ToString();
            dtPrijem.Value = datumPrijema;

        }
        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connect);

            var crm = "Select ID, PredvidjanjeID from Predvidjanje order by ID desc";
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

            cboTip.Items.Add("1-Železnički transport");
            cboTip.Items.Add("2-Privatni vagoni");
            cboTip.Items.Add("3-Rashodi");
            cboTip.Items.Add("4-Vantransportne usluge");

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

            var jm = "Select MeNaziv from MerskeEnote order by MeSifra";
            var jmDa = new SqlDataAdapter(jm, conn);
            var jmDS = new DataSet();
            jmDa.Fill(jmDS);
            cboJM.DataSource= jmDS.Tables[0];
            cboJM.DisplayMember = "MeNaziv";
            cboJM.ValueMember = "MeNaziv";
        }
        private void FillGV()
        {
            if (txtID.Text != "")
            {
                var select = "select UlFakPostav.* from UlFakPostav inner join UlFak on UlFakPostav.IDFak=UlFak.ID Where IDFak=" + txtID.Text;
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
            else { return; }
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

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0) { rb = 1; } else { rb = dataGridView1.Rows.Count+1; }
            InsertPatheonExport ins = new InsertPatheonExport();
            ins.InsUlFakPostav(Convert.ToInt32(txtID.Text), rb, Convert.ToInt32(cboMP.SelectedValue), Convert.ToDecimal(txtKolicina.Text), Convert.ToDecimal(txtCena.Text), Convert.ToInt32(cboNosilac.SelectedValue), cboJM.SelectedValue.ToString().TrimEnd(), textBox1.Text.ToString());
            FillGV();
        }

        private void cboMP_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            InsertPatheonExport ins = new InsertPatheonExport();
            if (status == true)
            {
                ins.InsUlFak(Convert.ToInt32(txtID.Text), cboCRM.SelectedValue.ToString(),cboVrstaDok.Text.ToString().Substring(0, 2), txtBrFakture.Text.ToString(), Convert.ToInt32(cboDobavljac.SelectedValue), cboTip.Text.ToString().Substring(0, 1),
                    Convert.ToDateTime(dtPrijem.Value.ToString()), cboValuta.SelectedValue.ToString(), Convert.ToDecimal(txtKurs.Text), txtRacunDobavljaca.Text.ToString(), Convert.ToDateTime(dtIzdavanje.Value.ToString()), Convert.ToDateTime(dtPDV.Value.ToString()),
                    Convert.ToDateTime(dtValute.Value.ToString()), Convert.ToInt32(cboReferent.SelectedValue), Convert.ToInt32(cboCRM.SelectedValue), txtNapomena.Text.ToString().TrimEnd(),Convert.ToInt32(crmID));
            }
            else
            {
                ins.UpdUlFak(Convert.ToInt32(txtID.Text), cboCRM.SelectedValue.ToString(), cboVrstaDok.SelectedValue.ToString().Substring(0, 2), txtBrFakture.Text.ToString(), Convert.ToInt32(cboDobavljac.SelectedValue), cboTip.SelectedValue.ToString().Substring(0, 1),
                    Convert.ToDateTime(dtPrijem.Value.ToString()), cboValuta.SelectedValue.ToString(), Convert.ToDecimal(txtKurs.Text), txtRacunDobavljaca.Text.ToString(), Convert.ToDateTime(dtIzdavanje.Value.ToString()), Convert.ToDateTime(dtPDV.Value.ToString()),
                    Convert.ToDateTime(dtValute.Value.ToString()), Convert.ToInt32(cboReferent.SelectedValue), Convert.ToInt32(cboCRM.SelectedValue), txtNapomena.Text.ToString().TrimEnd());
            }
            FillGV();
        }

        private void cboMP_Leave(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void UlazneFakture_Load(object sender, EventArgs e)
        {

        }
    }
}
