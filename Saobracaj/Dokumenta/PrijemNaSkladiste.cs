using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Dokumenta
{
    public partial class PrijemNaSkladiste : Syncfusion.Windows.Forms.Office2010Form
    {
        private string connect = Sifarnici.frmLogovanje.connectionString;
        int ID;
        DateTime VremeDolaska;
        string Korisnik;
        public PrijemNaSkladiste()
        {
            InitializeComponent();
        }
        public PrijemNaSkladiste(int sifra, DateTime vremeDolaska, string korisnik)
        {
            InitializeComponent();

            ID = sifra;
            VremeDolaska = vremeDolaska;
            Korisnik = korisnik;

            var select = "SELECT PrijemKontejneraVozStavke.ID, PrijemKontejneraVozStavke.RB, PrijemKontejneraVozStavke.IDNadredjenog,PrijemKontejneraVozStavke.BrojKontejnera, PrijemKontejneraVozStavke.BrojVagona," +
                "PrijemKontejneraVozStavke.Granica,PrijemKontejneraVozStavke.BrojOsovina, PrijemKontejneraVozStavke.SopstvenaMasa, PrijemKontejneraVozStavke.Tara,PrijemKontejneraVozStavke.Neto, " +
                "Partnerji.PaNaziv AS Posiljalac, Partnerji_1.PaNaziv AS primalac,Partnerji_2.PaNaziv AS Vlasnikkontejnera, TipKontenjera.Naziv AS TipKontejnera, NHM.Naziv AS VrstaRobe,PrijemKontejneraVozStavke.BukingBrodar AS BukingBrodar, " +
                "PrijemKontejneraVozStavke.StatusKontejnera,PrijemKontejneraVozStavke.BrojPlombe, PrijemKontejneraVozStavke.BrojPlombe2,PrijemKontejneraVozStavke.PlaniraniLager,PrijemKontejneraVozStavke.VremeDolaska, " +
                "PrijemKontejneraVozStavke.VremePripremljen,  PrijemKontejneraVozStavke.VremeOdlaska,PrijemKontejneraVozStavke.Datum, PrijemKontejneraVozStavke.Korisnik, PrijemKontejneraVozStavke.NapomenaS " +
                "FROM Partnerji " +
                "INNER JOIN PrijemKontejneraVozStavke ON Partnerji.PaSifra = PrijemKontejneraVozStavke.Posiljalac " +
                "INNER JOIN Partnerji AS Partnerji_1 ON PrijemKontejneraVozStavke.Primalac = Partnerji_1.PaSifra " +
                "INNER JOIN  Partnerji AS Partnerji_2 ON PrijemKontejneraVozStavke.VlasnikKontejnera = Partnerji_2.PaSifra " +
                "INNER JOIN TipKontenjera ON PrijemKontejneraVozStavke.TipKontejnera = TipKontenjera.ID " +
                "INNER JOIN  NHM ON PrijemKontejneraVozStavke.VrstaRobe = NHM.ID " +
                "INNER JOIN  Voz ON PrijemKontejneraVozStavke.IdVoza = Voz.ID " +
                "Where IdNadredjenog = " + ID + " order by RB";

            SqlConnection myConnection = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, myConnection);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = false;
            dataGridView1.DataSource = ds.Tables[0];

            //Panta refresh
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

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[0].Visible = false;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "RB";
            dataGridView1.Columns[1].Width = 30;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Br Dok";
            dataGridView1.Columns[2].Width = 30;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Broj kontejnera";
            dataGridView1.Columns[3].Width = 110;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Broj Vagona";
            dataGridView1.Columns[4].Width = 110;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Granica";
            dataGridView1.Columns[5].Width = 50;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Br os";
            dataGridView1.Columns[6].Width = 50;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Sops masa";
            dataGridView1.Columns[7].Width = 50;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Tara";
            dataGridView1.Columns[8].Width = 70;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Neto";
            dataGridView1.Columns[9].Width = 70;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Posiljalac";
            dataGridView1.Columns[10].Width = 50;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Primalac";
            dataGridView1.Columns[11].Width = 50;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Vlasnik";
            dataGridView1.Columns[12].Width = 40;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Tip kontejnera";
            dataGridView1.Columns[13].Width = 70;

            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "NHM";
            dataGridView1.Columns[14].Width = 40;

            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Buking broldar";
            dataGridView1.Columns[15].Width = 70;

            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Status Kontejnera";
            dataGridView1.Columns[16].Width = 20;

            DataGridViewColumn column18 = dataGridView1.Columns[17];
            dataGridView1.Columns[17].HeaderText = "Br plombe";
            dataGridView1.Columns[17].Width = 90;

            DataGridViewColumn column19 = dataGridView1.Columns[18];
            dataGridView1.Columns[18].HeaderText = "Br plombe2";
            dataGridView1.Columns[18].Width = 9;

            DataGridViewColumn column20 = dataGridView1.Columns[19];
            dataGridView1.Columns[19].HeaderText = "Plan lager";
            dataGridView1.Columns[19].Width = 30;

            DataGridViewColumn column21 = dataGridView1.Columns[20];
            dataGridView1.Columns[20].HeaderText = "Vreme dolaska";
            dataGridView1.Columns[20].Width = 70;

            DataGridViewColumn column22 = dataGridView1.Columns[21];
            dataGridView1.Columns[21].HeaderText = "Vreme prip";
            dataGridView1.Columns[21].Visible = false;
            dataGridView1.Columns[21].Width = 70;

            DataGridViewColumn column23 = dataGridView1.Columns[22];
            dataGridView1.Columns[22].HeaderText = "Vreme odlaska";
            dataGridView1.Columns[22].Visible = false;
            dataGridView1.Columns[22].Width = 70;

            DataGridViewColumn column24 = dataGridView1.Columns[23];
            dataGridView1.Columns[23].HeaderText = "Datum";
            dataGridView1.Columns[23].Width = 70;

            DataGridViewColumn column25 = dataGridView1.Columns[24];
            dataGridView1.Columns[24].HeaderText = "Korisnik";
            dataGridView1.Columns[24].Width = 70;

            DataGridViewColumn column26 = dataGridView1.Columns[25];
            dataGridView1.Columns[25].HeaderText = "Napomena stav";
            dataGridView1.Columns[25].Width = 70;

            var query2 = "Select Skladista.ID,Skladista.Naziv,Pozicija.ID,Pozicija.Skladiste,Pozicija.Oznaka,Pozicija.Opis,Pozicija.Namena " +
                "From Skladista " +
                "left join Pozicija on Skladista.ID=Pozicija.Skladiste";
            var dataAdapter2 = new SqlDataAdapter(query2, myConnection);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            dataGridView2.ReadOnly = false;
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
            dataGridView2.Columns[1].Width = 250;
            dataGridView2.Columns[2].HeaderText = "Pozicija";
            dataGridView2.Columns[3].Visible = false;
            dataGridView2.Columns[4].Width = 150;
            dataGridView2.Columns[5].Width = 150;
            dataGridView2.Columns[6].Width = 150;
        }
        int skladiste;
        int pozicija;
        string skladisteNaziv;
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Selected)
                {
                    skladiste = Convert.ToInt32(row.Cells[0].Value);
                    skladisteNaziv = row.Cells[1].Value.ToString().TrimEnd();
                    pozicija = Convert.ToInt32(row.Cells[2].Value);
                }
            }
        }
        private int VratiPodatkeMaxPromet()
        {
            SqlConnection con = new SqlConnection(connect);
            con.Open();
            SqlCommand cmd = new SqlCommand(" select (Max(PrStDokumenta) + 1) as PrstDokumenta from Promet", con);
            SqlDataReader dr = cmd.ExecuteReader();
            int SledeciBroj = 0;
            while (dr.Read())
            {
                SledeciBroj = Convert.ToInt32(dr["PrstDokumenta"].ToString());
            }
            con.Close();
            return SledeciBroj;
        }
        private int VratiPostojiPrijemKontejnera(string Kontejner)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" Select Count(*) as Broj from Promet where BRojKontejnera =  '" + Kontejner + "' and Zatvoren = 0", con);


            SqlDataReader dr = cmd.ExecuteReader();
            int SledeciBroj = 0;
            while (dr.Read())
            {
                SledeciBroj = Convert.ToInt32(dr["Broj"].ToString());

            }
            con.Close();
            return SledeciBroj;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Prijem na skladiste " + skladisteNaziv);
            int SledeciBroj = VratiPodatkeMaxPromet();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    Saobracaj.Dokumenta.InsertPromet ins = new Saobracaj.Dokumenta.InsertPromet();
                    int pom1 = 0;
                    int pom2 = 0;
                    int pom3 = 1;
                    string s1 = "PRI";
                    string s2 = "PRV";
                    int VecPostoji = VratiPostojiPrijemKontejnera(row.Cells[3].Value.ToString());
                    if (VecPostoji == 0)
                    {
                        ins.InsProm(VremeDolaska, s1, SledeciBroj, row.Cells[3].Value.ToString().TrimEnd(), s2, pom3, pom2, skladiste, pozicija, pom2, pom1, row.Cells[0].Value.ToString(), Convert.ToDateTime(DateTime.Now), Korisnik, 0, 0, Convert.ToDateTime(DateTime.Now));

                    }
                    else
                    {
                        MessageBox.Show("Postoji kontejner" + row.Cells[3].Value.ToString() + " koji nije zatvoren ");
                    }
                   
                }
            }
        }
    }
}
