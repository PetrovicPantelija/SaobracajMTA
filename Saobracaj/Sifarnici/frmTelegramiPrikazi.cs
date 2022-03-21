using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Sifarnici
{
    public partial class frmTelegramiPrikazi : Form
    {
        public string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public frmTelegramiPrikazi()
        {
            InitializeComponent();
        }

        private void frmTelgramiPrikazi_Load(object sender, EventArgs e)
        {
            RefreshDG();
        }
        private void RefreshDG()
        {
            var query = "Select Telegrami.ID,BrojTelegrama,RTrim(Pruga.Opis) as [Naziv]," +
                "RTrim(Stanice.Opis) as Opis,Rtrim(s.Opis) as sOpis,RTrim(Kolosek) as Kolosek,VaziOD,VaziDo,TrajeOd,TrajeDO,Aktivan,Napomena,PDF,NarocitaPosiljka " +
                "From telegrami " +
                "Inner Join Pruga on Telegrami.PrugaID = Pruga.ID " +
                "Inner join Stanice on Telegrami.OdStanice = Stanice.ID " +
                "Inner join Stanice as s on Telegrami.DoStanice = s.ID " +
                "order by Telegrami.ID desc";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].HeaderText = "Broj Telegrama";
            dataGridView1.Columns[2].HeaderText = "Naziv";
            dataGridView1.Columns[2].Width = 220;
            dataGridView1.Columns[3].HeaderText = "Stanica OD";
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].HeaderText = "Stanica DO";
            dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[5].Width = 75;
            dataGridView1.Columns[6].HeaderText = "Vazi OD";
            dataGridView1.Columns[6].Width = 100;
            dataGridView1.Columns[7].HeaderText = "Vazi DO";
            dataGridView1.Columns[7].Width = 100;
            dataGridView1.Columns[8].HeaderText = "Traje OD";
            dataGridView1.Columns[8].Width = 75;
            dataGridView1.Columns[9].HeaderText = "Traje DO";
            dataGridView1.Columns[9].Width = 75;
            dataGridView1.Columns[10].HeaderText = "Aktivan";
            dataGridView1.Columns[11].Width = 150;
            dataGridView1.Columns[12].Width = 100;
            dataGridView1.Columns[13].HeaderText = "Narocita posiljka";
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            var query = "Select Telegrami.ID,BrojTelegrama,RTrim(Pruga.Opis) as [Naziv]," +
                "RTrim(Stanice.Opis) as Opis,Rtrim(s.Opis) as sOpis,RTrim(Kolosek) as Kolosek,VaziOD,VaziDo,TrajeOd,TrajeDO,Aktivan,Napomena,PDF,NarocitaPosiljka " +
                "From telegrami " +
                "Inner Join Pruga on Telegrami.PrugaID = Pruga.ID " +
                "Inner join Stanice on Telegrami.OdStanice = Stanice.ID " +
                "Inner join Stanice as s on Telegrami.DoStanice = s.ID " +
                           "Where Telegrami.Aktivan = 1 " +
                           "order by Telegrami.ID desc";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            var query = "Select Telegrami.ID,BrojTelegrama,RTrim(Pruga.Opis) as [Naziv]," +
                "RTrim(Stanice.Opis) as Opis,Rtrim(s.Opis) as sOpis,RTrim(Kolosek) as Kolosek,VaziOD,VaziDo,TrajeOd,TrajeDO,Aktivan,Napomena,PDF,NarocitaPosiljka " +
                "From telegrami " +
                "Inner Join Pruga on Telegrami.PrugaID = Pruga.ID " +
                "Inner join Stanice on Telegrami.OdStanice = Stanice.ID " +
                "Inner join Stanice as s on Telegrami.DoStanice = s.ID " +
     "order by Telegrami.ID desc";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            var query = "Select Telegrami.ID,BrojTelegrama,RTrim(Pruga.Opis) as [Naziv]," +
                "RTrim(Stanice.Opis) as Opis,Rtrim(s.Opis) as sOpis,RTrim(Kolosek) as Kolosek,VaziOD,VaziDo,TrajeOd,TrajeDO,Aktivan,Napomena,PDF,NarocitaPosiljka " +
                "From telegrami " +
                "Inner Join Pruga on Telegrami.PrugaID = Pruga.ID " +
                "Inner join Stanice on Telegrami.OdStanice = Stanice.ID " +
                "Inner join Stanice as s on Telegrami.DoStanice = s.ID " +
                "Where(VaziDo Between Convert(Date, Getdate()) and Convert(Date, GetDate() + 7)) " +
                "order by Telegrami.ID desc";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btn_dani_Click(object sender, EventArgs e)
        {
            var query = "Select Telegrami.ID,BrojTelegrama,RTrim(Pruga.Opis) as [Naziv]," +
                "RTrim(Stanice.Opis) as Opis,Rtrim(s.Opis) as sOpis,RTrim(Kolosek) as Kolosek,VaziOD,VaziDo,TrajeOd,TrajeDO,Aktivan,Napomena,PDF,NarocitaPosiljka " +
                "From telegrami " +
                "Inner Join Pruga on Telegrami.PrugaID = Pruga.ID " +
                "Inner join Stanice on Telegrami.OdStanice = Stanice.ID " +
                "Inner join Stanice as s on Telegrami.DoStanice = s.ID " +
                "Where(VaziDo Between Convert(Date, Getdate()) and Convert(Date, GetDate() + 7)) and aktivan=1" +
                "order by Telegrami.ID desc";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            timer3.Enabled = true;
            timer1.Enabled = false;
            timer2.Enabled = false;
            timer4.Enabled = false;
            timer3.Start();
        }

        private void btn_Aktivni_Click(object sender, EventArgs e)
        {
            var query = "Select Telegrami.ID,BrojTelegrama,RTrim(Pruga.Opis) as [Naziv]," +
                "RTrim(Stanice.Opis) as Opis,Rtrim(s.Opis) as sOpis,RTrim(Kolosek) as Kolosek,VaziOD,VaziDo,TrajeOd,TrajeDO,Aktivan,Napomena,PDF,NarocitaPosiljka " +
                "From telegrami " +
                "Inner Join Pruga on Telegrami.PrugaID = Pruga.ID " +
                "Inner join Stanice on Telegrami.OdStanice = Stanice.ID " +
                "Inner join Stanice as s on Telegrami.DoStanice = s.ID " +
                           "Where Telegrami.Aktivan = 1 " +
                           "order by Telegrami.ID desc";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].HeaderText = "Broj Telegrama";
            dataGridView1.Columns[2].HeaderText = "Naziv";
            dataGridView1.Columns[2].Width = 220;
            dataGridView1.Columns[3].HeaderText = "Stanica OD";
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].HeaderText = "Stanica DO";
            dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[5].Width = 75;
            dataGridView1.Columns[6].HeaderText = "Vazi OD";
            dataGridView1.Columns[6].Width = 100;
            dataGridView1.Columns[7].HeaderText = "Vazi DO";
            dataGridView1.Columns[7].Width = 100;
            dataGridView1.Columns[8].HeaderText = "Traje OD";
            dataGridView1.Columns[8].Width = 75;
            dataGridView1.Columns[9].HeaderText = "Traje DO";
            dataGridView1.Columns[9].Width = 75;
            dataGridView1.Columns[10].HeaderText = "Aktivan";
            dataGridView1.Columns[11].Width = 150;
            dataGridView1.Columns[12].Width = 100;
            dataGridView1.Columns[13].HeaderText = "Narocita posiljka";
            timer1.Enabled = true;
            timer2.Enabled = false;
            timer3.Enabled = false;
            timer4.Enabled = false;
            timer1.Start();
        }

        private void btn_svi_Click(object sender, EventArgs e)
        {
            var query = "Select Telegrami.ID,BrojTelegrama,RTrim(Pruga.Opis) as [Naziv]," +
                "RTrim(Stanice.Opis) as Opis,Rtrim(s.Opis) as sOpis,RTrim(Kolosek) as Kolosek,VaziOD,VaziDo,TrajeOd,TrajeDO,Aktivan,Napomena,PDF,NarocitaPosiljka " +
                "From telegrami " +
                "Inner Join Pruga on Telegrami.PrugaID = Pruga.ID " +
                "Inner join Stanice on Telegrami.OdStanice = Stanice.ID " +
                "Inner join Stanice as s on Telegrami.DoStanice = s.ID " +
     "order by Telegrami.ID desc";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];


            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].HeaderText = "Broj Telegrama";
            dataGridView1.Columns[2].HeaderText = "Naziv";
            dataGridView1.Columns[2].Width = 220;
            dataGridView1.Columns[3].HeaderText = "Stanica OD";
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].HeaderText = "Stanica DO";
            dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[5].Width = 75;
            dataGridView1.Columns[6].HeaderText = "Vazi OD";
            dataGridView1.Columns[6].Width = 100;
            dataGridView1.Columns[7].HeaderText = "Vazi DO";
            dataGridView1.Columns[7].Width = 100;
            dataGridView1.Columns[8].HeaderText = "Traje OD";
            dataGridView1.Columns[8].Width = 75;
            dataGridView1.Columns[9].HeaderText = "Traje DO";
            dataGridView1.Columns[9].Width = 75;
            dataGridView1.Columns[10].HeaderText = "Aktivan";
            dataGridView1.Columns[11].Width = 150;
            dataGridView1.Columns[12].Width = 100;
            dataGridView1.Columns[13].HeaderText = "Narocita posiljka";
            timer2.Enabled = true;
            timer1.Enabled = false;
            timer3.Enabled = false;
            timer4.Enabled = false;
            timer2.Start();
        }

        private void btn_Narocite_Click(object sender, EventArgs e)
        {
            var query = "Select Telegrami.ID,BrojTelegrama,RTrim(Pruga.Opis) as [Naziv]," +
                "RTrim(Stanice.Opis) as Opis,Rtrim(s.Opis) as sOpis,RTrim(Kolosek) as Kolosek,VaziOD,VaziDo,TrajeOd,TrajeDO,Aktivan,Napomena,PDF,NarocitaPosiljka " +
                "From telegrami " +
                "Inner Join Pruga on Telegrami.PrugaID = Pruga.ID " +
                "Inner join Stanice on Telegrami.OdStanice = Stanice.ID " +
                "Inner join Stanice as s on Telegrami.DoStanice = s.ID " +
                           "Where Telegrami.NarocitaPosiljka=1 " +
                           "order by Telegrami.ID desc";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];


            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].HeaderText = "Broj Telegrama";
            dataGridView1.Columns[2].HeaderText = "Naziv";
            dataGridView1.Columns[2].Width = 220;
            dataGridView1.Columns[3].HeaderText = "Stanica OD";
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].HeaderText = "Stanica DO";
            dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[5].Width = 75;
            dataGridView1.Columns[6].HeaderText = "Vazi OD";
            dataGridView1.Columns[6].Width = 100;
            dataGridView1.Columns[7].HeaderText = "Vazi DO";
            dataGridView1.Columns[7].Width = 100;
            dataGridView1.Columns[8].HeaderText = "Traje OD";
            dataGridView1.Columns[8].Width = 75;
            dataGridView1.Columns[9].HeaderText = "Traje DO";
            dataGridView1.Columns[9].Width = 75;
            dataGridView1.Columns[10].HeaderText = "Aktivan";
            dataGridView1.Columns[11].Width = 150;
            dataGridView1.Columns[12].Width = 100;
            dataGridView1.Columns[13].HeaderText = "Narocita posiljka";
            timer4.Enabled = true;
            timer2.Enabled = false;
            timer3.Enabled = false;
            timer1.Enabled = false;
            timer4.Start();
        }
    }
}
