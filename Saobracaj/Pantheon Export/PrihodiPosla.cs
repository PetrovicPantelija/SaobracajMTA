using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
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
        decimal trase, predvidjanja, aktivnosti,faktura,vuca;
        private void Trase()
        {
            var select = "select RadniNalogVezaNajave.*,Najava.Oznaka,RTrim(stanice.opis),RTrim(s.Opis),RTrim(Trase.OpisRelacije),Cast((Trase.Cena/117.2) as decimal(18,2))as Cena " +
                "from RadniNalogVezaNajave " +
                "left join Najava on RadniNalogVezaNajave.IDNajave = Najava.ID " +
                "inner join RadniNalogTrase on RadniNalogVezaNajave.IDRadnogNaloga = RadniNalogTrase.IDRadnogNaloga " +
                "inner join Trase on RadniNalogTrase.IDTrase = Trase.ID " +
                "inner join stanice on Trase.Pocetna = Stanice.ID " +
                "inner join stanice as s on Trase.Krajnja = s.ID Where Najava.ID =" + Convert.ToInt32(comboBox1.SelectedValue);
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

            trase = dataGridView1.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["Cena"].Value));
            txtTrase.Text = trase.ToString();
        }
        private void Predvidjanja()
        {
            var select = "select p.ID as ID,p.IDp as IDp,RTrim(p.NosilacTroska) as NTID,RTrim(NosiociTroskova.NosilacTroska) as NT,RTrim(NosiociTroskova.NazivNosiocaTroska) as [NT Naziv]," +
                "p.Status as Status,RTrim(PredvidjanjeID) as Predvidjanje,p.PredvodjanjePoz as [Poz.],(RTrim(MpStaraSif) + '-' + RTrim(MpNaziv)) as Ident," +
                "p.Napomena as Napomena,Format(p.Datum,'dd.MM.yyyy') as Datum,RTrim(PaNaziv) as Subjekt,RTrim(SifraSubjekta) as Odeljenje," +
                "Iznos,Valuta,Kolicina,JM,Cast((Iznos/Kolicina) as decimal(18,4))as [Jedinicna Cena],p.Kurs as Kurs,p.Subjekt,p.Odeljenje,p.NajavaID,Ident,RTrim(p.Korisnik) as Korisnik,IznosRSD " +
                "From Predvidjanje p " +
                "inner join Partnerji on p.Subjekt = Partnerji.PaSifra " +
                "inner join NosiociTroskova on p.NosilacTroska = NosiociTroskova.ID " +
                "inner join MaticniPodatki on p.Ident = MaticniPodatki.MpSifra " +
                "inner join Odeljenja on p.Odeljenje = Odeljenja.ID " +
                "Where p.NajavaID=" + Convert.ToInt32(comboBox1.SelectedValue) + " order by p.ID desc";

            SqlConnection conn = new SqlConnection(connect);
            var da2 = new SqlDataAdapter(select, conn);
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

            dataGridView2.Columns["ID"].Width = 55;
            dataGridView2.Columns["IDp"].Width = 55;
            dataGridView2.Columns["NTID"].Visible = false;
            dataGridView2.Columns["NT"].Width = 80;
            dataGridView2.Columns["NT Naziv"].Width = 100;
            dataGridView2.Columns["Status"].Width = 55;
            dataGridView2.Columns["Predvidjanje"].Width = 120;
            dataGridView2.Columns["Ident"].Width = 200;
            dataGridView2.Columns["Poz."].Width = 50;
            dataGridView2.Columns["Napomena"].Width = 150;
            dataGridView2.Columns["Subjekt"].Width = 200;
            dataGridView2.Columns["Odeljenje"].Visible = false;
            dataGridView2.Columns["Iznos"].Width = 70;
            dataGridView2.Columns["Valuta"].Width = 50;
            dataGridView2.Columns["Kolicina"].Width = 70;
            dataGridView2.Columns["JM"].Width = 55;
            dataGridView2.Columns["Jedinicna cena"].Width = 70;
            dataGridView2.Columns["Kurs"].Width = 60;
            dataGridView2.Columns["Subjekt1"].Visible = false;
            dataGridView2.Columns["Ident1"].Visible = false;
            dataGridView2.Columns["IznosRSD"].Visible = false;
            dataGridView2.Columns["Odeljenje1"].Visible = false;

            predvidjanja = dataGridView2.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["Iznos"].Value.ToString()));
            txtPredvidjanje.Text = predvidjanja.ToString();
        }
        private void Vuca()
        {
            var select = "Select 'VUCA' as Aktivnost,Round(Sum(Sati),0) as Sati,Cena,Lokomotiva " +
                "From AktivnostiStavke " +
                "INNER JOIN VrstaAktivnosti ON AktivnostiStavke.VrstaAktivnostiID = VrstaAktivnosti.ID " +
                "Where VrstaAktivnostiID = 61 and Posao=" + Convert.ToInt32(comboBox1.SelectedValue) +
                " Group BY VrstaAktivnostiID,Lokomotiva,Cena";

            SqlConnection conn = new SqlConnection(connect);
            var da4 = new SqlDataAdapter(select, conn);
            var ds4 = new DataSet();
            da4.Fill(ds4);
            dataGridView5.ReadOnly = true;
            dataGridView5.DataSource = ds4.Tables[0];

            dataGridView5.BorderStyle = BorderStyle.FixedSingle;
            dataGridView5.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView5.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView5.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView5.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView5.BackgroundColor = Color.White;

            dataGridView5.EnableHeadersVisualStyles = false;
            dataGridView5.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView5.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView5.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dataGridView5.Columns["Aktivnost"].Width = 80;
            dataGridView5.Columns["Sati"].Width = 70;
            dataGridView5.Columns["Cena"].Width = 60;

            vuca = 0;
            foreach (DataGridViewRow row in dataGridView5.Rows)
            {
                vuca = vuca + (Convert.ToDecimal(row.Cells["Cena"].Value) * Convert.ToDecimal(row.Cells["Sati"].Value));
            }
        }
        private void Aktivnosti()
        {
            var select3 = "select Aktivnosti.ID,(RTrim(DeIme)+' '+RTrim(DePriimek)) as Radnik,RTrim(Naziv),Sati,Cena,Cast((Sati*Cena) as decimal(18,2)) as [Cena Aktivnosti],DatumPocetka,DatumZavrsetka,MestoIzvrsenja," +
                "RTrim(stanice.Opis),Teretnica,Lokomotiva " +
                "from AktivnostiStavke " +
                "inner join Aktivnosti on AktivnostiStavke.IDNadredjena=Aktivnosti.ID " +
                "inner join Delavci on Aktivnosti.Zaposleni=Delavci.DeSifra " +
                "inner join VrstaAktivnosti on AktivnostiStavke.VrstaAktivnostiID=VrstaAktivnosti.ID " +
                "inner join Stanice on AktivnostiStavke.Stanica=Stanice.ID " +
                "Where VrstaAktivnostiID<>61 and Posao=" + Convert.ToInt32(comboBox1.SelectedValue) + " order by ID asc";

            SqlConnection conn = new SqlConnection(connect);
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
            dataGridView3.Columns[9].Width = 120;

            aktivnosti = dataGridView3.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["Cena Aktivnosti"].Value.ToString()));
            txtAktivnosti.Text = (aktivnosti+vuca).ToString("f");
        }
        private void Fakture()
        {
            var select4 = "Select FaPStFak as ID,RTrim(NosiociTroskova.NosilacTroska) as NT,RTrim(NazivNosiocaTroska) as [Naziv NT], FapStPos as [RB],FakturaPOstav.Datum as [Datum]," +
                "FaPNaziv as [Naziv],FaPKolOdpr as [Kolicina],FaPEM as [JM],Cast(FapCenaEM as decimal(18,4)) as [Cena],FaValutaCene as [Valuta],Faktura.CRMID,Faktura.FaVpisalIme as Korisnik " +
                "From FakturaPOstav " +
                "inner join Faktura on FakturaPostav.FaPStFak=Faktura.FaStFak " +
                "inner join NosiociTroskova on FakturaPostav.NosilacTroska=NosiociTroskova.ID " +
                "Where NajavaID=" + Convert.ToInt32(comboBox1.SelectedValue);

            SqlConnection conn = new SqlConnection(connect);
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

            faktura= dataGridView4.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["Cena"].Value.ToString()));
            txtFaktura.Text = faktura.ToString("f");
        }
        
        private void FillGV()
        {
            Trase();
            Predvidjanja();
            Vuca();
            Aktivnosti();
            Fakture();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillGV();
            Prihodi();
        }
        private void Prihodi()
        {
            decimal ukupnoEur = predvidjanja + aktivnosti;
            txtUeur.Text = ukupnoEur.ToString();

            decimal bilans = Convert.ToDecimal(txtFaktura.Text) - Convert.ToDecimal(txtUeur.Text);
            if (bilans >= 0)
            {
                txtBilans.BackColor = Color.LightGreen;
            }
            else
            {
                txtBilans.BackColor = Color.Red;
                txtBilans.ForeColor = Color.White;
            }
            txtBilans.Text = bilans.ToString();
        }
    }
}
