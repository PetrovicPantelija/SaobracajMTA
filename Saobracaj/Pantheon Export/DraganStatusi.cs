using Saobracaj.Sifarnici;
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
    public partial class DraganStatusi : Form
    {
        string connect = frmLogovanje.connectionString;
        public DraganStatusi()
        {
            InitializeComponent();
        }
        string dok;
        public DraganStatusi(string dokument)
        {
            InitializeComponent();
            dok = dokument;
        }

        private void DraganStatusi_Load(object sender, EventArgs e)
        {
            if (dok == "NT")
            {
                var select = "Select NosiociTroskova.ID,NosilacTroska,RTrim(NazivNosiocaTroska) as NazivNosiocaTroska,Grupa,RTrim(PaNaziv) as Kupac,RTrim(SifraSubjekta) as Odeljenje, " +
               "(RTrim(Opportunity.OppID) + ' - ' + RTrim(Opportunity.NazivPosla)) as Opportunity, Kupac, NosiociTroskova.Odeljenje,NosiociTroskova.OppID,Posao,NosiociTroskova.Status,RTrim(NosiociTroskova.Korisnik) as Korisnik " +
               "From NosiociTroskova " +
               "inner join Partnerji on NosiociTroskova.Kupac = Partnerji.PaSifra " +
               "inner join Odeljenja on NosiociTroskova.Odeljenje = Odeljenja.ID " +
               "Inner join Opportunity on NosiociTroskova.OppID = Opportunity.ID " +
               "WHere NosiociTroskova.Status = 0 order by ID desc";

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

                dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[1].Width = 150;
                dataGridView1.Columns[2].Width = 220;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[3].Width = 150;
                dataGridView1.Columns[4].Width = 300;
                dataGridView1.Columns[5].Width = 150;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Width = 300;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[10].Width = 80;
                dataGridView1.Columns[11].Width = 60;
            }
            if (dok == "Predvidjanje")
            {
                var select = "select p.ID as ID,p.IDp as IDp,RTrim(p.NosilacTroska) as NTID,RTrim(NosiociTroskova.NosilacTroska) as NT," +
                "RTrim(NosiociTroskova.NazivNosiocaTroska) as [NT Naziv],RTrim(PredvidjanjeID) as Predvidjanje,(RTrim(MpStaraSif) + '-' + RTrim(MpNaziv)) as Ident," +
                "p.PredvodjanjePoz as [Poz.],p.Napomena as Napomena,Format(p.Datum,'dd.MM.yyyy') as Datum,RTrim(PaNaziv) as Subjekt,RTrim(SifraSubjekta) as Odeljenje," +
                "Iznos,Valuta,Kolicina,JM,Cast((Iznos/Kolicina) as decimal(18,4))as [Jedinicna Cena],p.Kurs as Kurs,p.Subjekt,p.Odeljenje,p.NajavaID,Ident,RTrim(p.Korisnik) as Korisnik,IznosRSD " +
                "From Predvidjanje p " +
                "inner join Partnerji on p.Subjekt = Partnerji.PaSifra " +
                "inner join NosiociTroskova on p.NosilacTroska = NosiociTroskova.ID " +
                "inner join MaticniPodatki on p.Ident = MaticniPodatki.MpSifra " +
                "inner join Odeljenja on p.Odeljenje = Odeljenja.ID Where p.Status = 0 order by p.ID desc";
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

                dataGridView1.Columns["ID"].Width = 55;
                dataGridView1.Columns["IDp"].Width = 60;
                dataGridView1.Columns["NTID"].Visible = false;
                dataGridView1.Columns["NT"].Width = 70;
                dataGridView1.Columns["NT Naziv"].Width = 90;
                dataGridView1.Columns["Ident"].Width = 120;
                dataGridView1.Columns["Poz."].Width = 55;
                dataGridView1.Columns["Napomena"].Width = 120;
                dataGridView1.Columns["Subjekt"].Width = 150;
                dataGridView1.Columns["Odeljenje"].Visible = false;
                dataGridView1.Columns["Iznos"].Width = 80;
                dataGridView1.Columns["Valuta"].Width = 50;
                dataGridView1.Columns["Kolicina"].Width = 60;
                dataGridView1.Columns["JM"].Width = 55;
                dataGridView1.Columns["Jedinicna Cena"].Width = 70;
                dataGridView1.Columns["Kurs"].Width = 65;
                dataGridView1.Columns["Subjekt1"].Visible = false;
                dataGridView1.Columns["Odeljenje1"].Visible = false;
                dataGridView1.Columns["Ident1"].Visible = false;
                dataGridView1.Columns["Korisnik"].Width = 70;
                dataGridView1.Columns["IznosRSD"].Visible = false;
            }
            if (dok == "IzFak")
            {
                var select = "Select distinct FaStFak as ID," +
                "(Select MAX(RTrim(NosiociTroskova.NosilacTroska)) from FakturaPostav inner join NosiociTroskova on FakturaPostav.NosilacTroska = NosiociTroskova.ID where FakturaPostav.FaPStFak = Faktura.FaStFak) as [NT]," +
                "(Select MAX(RTrim(NosiociTroskova.NazivNosiocaTroska)) from FakturaPostav inner join NosiociTroskova on FakturaPostav.NosilacTroska = NosiociTroskova.ID where FakturaPostav.FaPStFak = Faktura.FaStFak) as [NT Naziv]," +
                "Faktura.Status as Status,Format(FaDatFak, 'dd.MM.yyyy') as Datum,FaStatus as FaStatus,FaModul as FaModul,RTrim(PaNaziv) as Kupac,Format(FaDatVal, 'dd.MM.yyyy') as [Datum Valute],Kurs as Kurs," +
                "FaValutaCene as Valuta,Format(FaObdobje, 'dd.MM.yyyy') as DatumPDV,MestoUtovara as [Mesto Utovara],Format(DatumUtovara, 'dd.MM.yyyy') as [Datum Utovara],FaDostMesto as [Mesto Istovara], " +
                "Format(DatumIstovara, 'dd.MM.yyyy') as [Datum Istovara],(Rtrim(deIme) + ' ' + RTrim(DePriimek)) as Referent ,RTrim(Izjave.Naziv) as Izjava,RTrim(FaOpomba2) as Napomena,FaPartPlac as [Platilac]," +
                "FaRefer as [FaRefer],(Select Sum(FaPZnesSk) from FakturaPostav where FakturaPostav.FaPStFak = Faktura.FaStFak ) as Iznos,FaOpomba1," +
                "(Select SUM(IznosRSD) From FakturaPostav Where FakturaPostav.FaPStFak = Faktura.FaStFak) as IznosRSD,RTrim(FaVPisalIme) as Korisnik,CRMID,PantheonID,FakturaPostav.NajavaID as NajavaID " +
                "From Faktura " +
                "inner join Partnerji on Faktura.FaPartPlac = Partnerji.PaSifra " +
                "Inner join Delavci on Faktura.FaRefer = Delavci.DeSifra " +
                "inner join Izjave on Faktura.FaOpomba1 = Izjave.ID " +
                "inner join FakturaPostav on Faktura.FaStFak=FakturaPostav.FaPStFak order by FaStFak desc";

                SqlConnection conn = new SqlConnection(connect);
                var dataAdapter = new SqlDataAdapter(select, conn);
                var ds = new System.Data.DataSet();
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


                dataGridView1.Columns["FaStatus"].Visible = false;
                dataGridView1.Columns["FaModul"].Visible = false;
                dataGridView1.Columns["Mesto Utovara"].Visible = false;
                dataGridView1.Columns["Mesto Istovara"].Visible = false;
                dataGridView1.Columns["Datum Utovara"].Visible = false;
                dataGridView1.Columns["Datum Istovara"].Visible = false;
                dataGridView1.Columns["Referent"].Visible = false;
                dataGridView1.Columns["Izjava"].Visible = false;

                dataGridView1.Columns["ID"].Width = 70;
                dataGridView1.Columns["NT"].Width = 70;
                dataGridView1.Columns["NT Naziv"].Width = 150;
                dataGridView1.Columns["Status"].Width = 50;
                dataGridView1.Columns["Kupac"].Width = 200;
                dataGridView1.Columns["Kurs"].Width = 60;
                dataGridView1.Columns["Valuta"].Width = 50;
                dataGridView1.Columns["Napomena"].Width = 300;
                dataGridView1.Columns["Platilac"].Visible = false;
                dataGridView1.Columns["FaRefer"].Visible = false;
                dataGridView1.Columns["Iznos"].Width = 100;
                dataGridView1.Columns["FaOpomba1"].Visible = false;
                dataGridView1.Columns["IznosRSD"].Visible = false;
                dataGridView1.Columns["NajavaID"].Visible = false;
            }
            if (dok == "UlFak")
            {
                var select = "Select UlFak.ID as ID," +
                "(Select MAX(RTrim(NosiociTroskova.NosilacTroska)) from UlFakPostav inner join NosiociTroskova on UlFakPostav.NosilacTroska = NosiociTroskova.ID where UlFakPostav.IDFak = UlFak.ID) as [NT]," +
                "(Select MAX(RTrim(NosiociTroskova.NazivNosiocaTroska)) from UlFakPostav inner join NosiociTroskova on UlFakPostav.NosilacTroska = NosiociTroskova.ID where UlFakPostav.IDFak = UlFak.ID) as [NT Naziv]," +
                "UlFak.Status as Status,RTrim(Predvidjanje.PredvidjanjeId) as PredvidjanjeID,VrstaDokumenta as [Vrsta Dokumenta],Tip as [Tip]," +
                "Format(DatumPrijema,'dd.MM.yyyy') as [DatumPrijema],RTrim(UlFak.Valuta) as Valuta,UlFak.Kurs as Kurs,FakturaBr as [Faktura BR],RTrim(PaNaziv) as Dobavljac," +
                "RTrim(RacunDobavljaca) as RacunDobavljaca,Format(DatumIzdavanja,'dd.MM.yyy') as [DatumIzdavanja],Format(DatumPDVa,'dd.MM.yyyy') as [DatumPDVa]," +
                "Format(DatumValute,'dd.MM.yyyy') as [DatumValute],(RTrim(DeIMe) + ' ' + RTrim(DePriimek)) as Referent,(Select Sum(Cena) from UlFakPostav Where UlFakPostav.IDFak = UlFak.ID) as Iznos," +
                "(Select Sum(IznosRSD) from UlFakPostav Where UlFakPostav.IDFak = UlFak.ID) as IznosRSD, UlFak.Napomena as Napomena,UlFak.CRMID as CRMID," +
                "Predvidjanje as Predvidjanje,IDDobavljaca as IDDobavljaca," +
                "UlFak.Referent as Referent," +
                "RTrim(UlFak.Korisnik) as Korisnik,PantheonID " +
                "From UlFak " +
                "Inner join Predvidjanje on UlFak.Predvidjanje = Predvidjanje.ID " +
                "inner join Partnerji on UlFak.IDDobavljaca = Partnerji.PaSifra " +
                "inner join Delavci on UlFak.Referent = Delavci.DeSifra " +
                "Order by UlFak.ID desc";

                SqlConnection conn = new SqlConnection(connect);
                var dataAdapter = new SqlDataAdapter(select, conn);
                var ds = new System.Data.DataSet();
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

                dataGridView1.Columns["ID"].Width = 50;
                dataGridView1.Columns["NT Naziv"].Width = 120;
                dataGridView1.Columns["Status"].Width = 45;
                dataGridView1.Columns["PredvidjanjeID"].Width = 90;
                dataGridView1.Columns["Vrsta Dokumenta"].Visible = false;
                dataGridView1.Columns["Tip"].Width = 55;
                dataGridView1.Columns["Valuta"].Width = 55;
                dataGridView1.Columns["Kurs"].Width = 60;
                dataGridView1.Columns["Dobavljac"].Width = 150;
                dataGridView1.Columns["RacunDobavljaca"].Visible = false;
                dataGridView1.Columns["Referent"].Visible = false;
                dataGridView1.Columns["Iznos"].Width = 80;
                dataGridView1.Columns["IznosRSD"].Visible = false;
                dataGridView1.Columns["Napomena"].Width = 280;
                dataGridView1.Columns["CRMID"].Width = 80;
                dataGridView1.Columns["Predvidjanje"].Visible = false;
                dataGridView1.Columns["IDDobavljaca"].Visible = false;
                dataGridView1.Columns["Referent1"].Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    if (dok == "NT")
                    {
                        using (SqlConnection conn = new SqlConnection(connect))
                        {
                            using (SqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.CommandText = "Update NosiociTroskova Set Status=1 Where ID=" + Convert.ToInt32(row.Cells[0].Value);
                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                    if (dok == "Predvidjanje")
                    {
                        using (SqlConnection conn = new SqlConnection(connect))
                        {
                            using (SqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.CommandText = "Update Predvidjanje Set Status=1 Where ID=" + Convert.ToInt32(row.Cells[0].Value);
                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                    if (dok == "IzFak")
                    {
                        using (SqlConnection conn = new SqlConnection(connect))
                        {
                            using (SqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.CommandText = "Update Faktura Set Status=1 Where FaStFak=" + Convert.ToInt32(row.Cells[0].Value);
                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                    if (dok == "UlFak")
                    {
                        using (SqlConnection conn = new SqlConnection(connect))
                        {
                            using (SqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.CommandText = "Update UlFak Set Status=1 Where ID=" + Convert.ToInt32(row.Cells[0].Value);
                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                }
            }
        }
    }
}
