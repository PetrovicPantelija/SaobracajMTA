using Saobracaj.TrackModal.TestiranjeDataSet19TableAdapters;
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

namespace Saobracaj.Pantheon_Export
{
    public partial class PrihodiPosla : Form
    {
        private string connect = Sifarnici.frmLogovanje.connectionString;
        public PrihodiPosla()
        {
            InitializeComponent();
        }
        private void PrihodiPosla_Load(object sender, EventArgs e)
        {
            FillCombo();
        }
        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connect);

            var query = "Select ID,Oznaka From Najava Where Status=7 order by ID desc";
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            comboBox1.DataSource = ds.Tables[0];
            comboBox1.DisplayMember = "Oznaka";
            comboBox1.ValueMember = "ID";
        }
        private void FillGV()
        {
            var select = "select RadniNalogVezaNajave.*,Najava.Oznaka,RTrim(stanice.opis),RTrim(s.Opis),RTrim(Trase.OpisRelacije),Trase.Cena " +
                "from RadniNalogVezaNajave " +
                "left join Najava on RadniNalogVezaNajave.IDNajave = Najava.ID " +
                "inner join RadniNalogTrase on RadniNalogVezaNajave.IDRadnogNaloga = RadniNalogTrase.IDRadnogNaloga " +
                "inner join Trase on RadniNalogTrase.IDTrase = Trase.ID " +
                "inner join stanice on Trase.Pocetna = Stanice.ID " +
                "inner join stanice as s on Trase.Krajnja = s.ID Where Najava.ID ="+Convert.ToInt32(comboBox1.SelectedValue);
            SqlConnection conn = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].HeaderText = "ID RN";
            dataGridView1.Columns[1].Width = 70;
            dataGridView1.Columns[2].HeaderText = "ID Posla";
            dataGridView1.Columns[2].Width = 90;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[5].HeaderText = "Početna";
            dataGridView1.Columns[5].Width = 150;
            dataGridView1.Columns[6].HeaderText = "Krajnja";
            dataGridView1.Columns[6].Width = 150;
            dataGridView1.Columns[7].Width = 430;
            dataGridView1.Columns[7].HeaderText = "Opis relacije";


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

            var select2 = "Select p.ID, (RTrim(PredvidjanjeID) + '/Poz-'+ convert(nvarchar(255), PredvodjanjePoz)) as PredvidjanjeID,p.Datum,RTrim(PaNaziv),RTrim(NazivNosiocaTroska),RTrim(Naziv2),Iznos,Valuta " +
                "from Predvidjanje p " +
                "inner join Partnerji on p.Subjekt=Partnerji.PaSifra " +
                "inner join NosiociTroskova on p.NosilacTroska=NosiociTroskova.ID " +
                "inner join Odeljenja on p.Odeljenje=Odeljenja.ID " +
                "Where NajavaID="+Convert.ToInt32(comboBox1.SelectedValue)+" order by p.ID desc";
            var da2 = new SqlDataAdapter(select2, conn);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds2.Tables[0];

            dataGridView2.BorderStyle = BorderStyle.FixedSingle;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView2.BackgroundColor = Color.White;

            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dataGridView2.Columns[0].Width = 50;
            dataGridView2.Columns[1].Width = 150;
            dataGridView2.Columns[2].Width = 150;
            dataGridView2.Columns[3].HeaderText = "Partner";
            dataGridView2.Columns[3].Width = 350;
            dataGridView2.Columns[4].HeaderText = "Nosilac troška";
            dataGridView2.Columns[4].Width = 150;
            dataGridView2.Columns[5].HeaderText = "Odeljenje";
            dataGridView2.Columns[5].Width = 190;


            var select3 = "select Aktivnosti.ID,(RTrim(DeIme)+' '+RTrim(DePriimek)) as Radnik,RTrim(Naziv),Sati,Cena,Cast((Sati*Cena) as decimal(18,2)) as CenaAktivnsti,DatumPocetka,DatumZavrsetka,MestoIzvrsenja," +
                "RTrim(stanice.Opis),Teretnica,Lokomotiva " +
                "from AktivnostiStavke " +
                "inner join Aktivnosti on AktivnostiStavke.IDNadredjena=Aktivnosti.ID " +
                "inner join Delavci on Aktivnosti.Zaposleni=Delavci.DeSifra " +
                "inner join VrstaAktivnosti on AktivnostiStavke.VrstaAktivnostiID=VrstaAktivnosti.ID " +
                "inner join Stanice on AktivnostiStavke.Stanica=Stanice.ID " +
                "Where Posao="+Convert.ToInt32(comboBox1.SelectedValue)+" order by ID asc";
            var da3 = new SqlDataAdapter(select3, conn);
            var ds3 = new DataSet();
            da3.Fill(ds3);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds3.Tables[0];

            dataGridView3.BorderStyle = BorderStyle.FixedSingle;
            dataGridView3.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView3.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView3.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView3.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView3.BackgroundColor = Color.White;

            dataGridView3.EnableHeadersVisualStyles = false;
            dataGridView3.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView3.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;


            dataGridView3.Columns[0].Width = 60;
            dataGridView3.Columns[1].Width = 170;
            dataGridView3.Columns[2].HeaderText = "Vrsta aktivnosti";
            dataGridView3.Columns[2].Width = 200;
            dataGridView3.Columns[3].Width = 50;
            dataGridView3.Columns[4].Width = 50;
            dataGridView3.Columns[5].Width = 80;
            dataGridView3.Columns[6].Width = 120;
            dataGridView3.Columns[7].Width = 120;
            dataGridView3.Columns[9].HeaderText = "Stanica";
            dataGridView3.Columns[9].Width=120;

            var select4 = "Select * From FakturaPostav WHere NajavaID=" + Convert.ToInt32(comboBox1.SelectedValue);
            var da4 = new SqlDataAdapter(select4, conn);
            var ds4 = new DataSet();
            da4.Fill(ds4);
            dataGridView4.ReadOnly = true;
            dataGridView4.DataSource = ds4.Tables[0];

            dataGridView4.BorderStyle = BorderStyle.FixedSingle;
            dataGridView4.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView4.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView4.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView4.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView4.BackgroundColor = Color.White;

            dataGridView4.EnableHeadersVisualStyles = false;
            dataGridView4.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView4.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView4.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillGV();
            Prihodi();
        }
        private void Prihodi()
        {
            decimal trase = dataGridView1.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells[8].Value));
            decimal predvidjanje = dataGridView2.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells[6].Value.ToString()));
            decimal aktivnosti = dataGridView3.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells[5].Value.ToString()));

            decimal ukupnoRSD = 0;
            decimal ukupnoEur = trase + predvidjanje + aktivnosti;

            txtTrase.Text = trase.ToString();
            txtPredvidjanje.Text = predvidjanje.ToString();
            txtAktivnosti.Text = aktivnosti.ToString();

            txtUrsd.Text = ukupnoRSD.ToString();
            txtUeur.Text = ukupnoEur.ToString();

            string query2 = "select Budzet,EstimatedRevenue " +
                "from Opportunity " +
                "inner join NosiociTroskova on NosiociTroskova.OppID = Opportunity.ID " +
                "inner join Najava on NosiociTroskova.Posao = Najava.ID " +
                "Where Najava.ID =" + comboBox1.SelectedValue;
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd2 = new SqlCommand(query2, conn);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                txtBudzet.Text = dr2[0].ToString();
                txtRevenue.Text= dr2[1].ToString();
            }
            conn.Close();

            decimal kolicina = dataGridView4.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["FaPKolOdpr"].Value.ToString()));
            decimal cena= dataGridView4.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["FaPCenaEM"].Value.ToString()));

            decimal sum = kolicina * cena;
            txtFaktura.Text= sum.ToString("f");


            decimal bilans= Convert.ToDecimal(txtBudzet.Text) - Convert.ToDecimal(txtUeur.Text);
            if (bilans >= 0)
            {
                txtBilans.BackColor = Color.LightGreen;
            }
            else
            {
                txtBilans.BackColor=Color.Red;
                txtBilans.ForeColor=Color.White;
            }
            txtBilans.Text = bilans.ToString();

            if ((sum - ukupnoEur) > 0) { txtPrihodiEUR.BackColor = Color.LightGreen;}
            if((sum - ukupnoEur) < 0) { txtPrihodiEUR.BackColor= Color.Red;}

            txtPrihodiEUR.Text = (sum - ukupnoEur).ToString("f");
            txtPrihodiRSD.Text = "0";
        }
    }
}
