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
            var query = "Select Telegrami.ID,BrojTelegrama,PrugaID,Pruga.Oznaka [Oznaka],Pruga.Opis [Naziv]," +
                "OdStanice,Stanice.Opis,DoStanice,s.Opis,Kolosek,VaziOD,VaziDo,TrajeOd,TrajeDO,Aktivan,Napomena " +
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

            DataGridViewColumn id = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].HeaderText = "BR Telegrama";
            dataGridView1.Columns[1].Width = 75;
            dataGridView1.Columns[2].HeaderText = "PrugaID";
            dataGridView1.Columns[2].Width = 55;
            dataGridView1.Columns[3].HeaderText = "PrugaOznaka";
            dataGridView1.Columns[3].Width = 75;
            dataGridView1.Columns[4].HeaderText = "Naziv";
            dataGridView1.Columns[4].Width = 200;
            dataGridView1.Columns[5].HeaderText = "StanicaOD";
            dataGridView1.Columns[5].Width = 60;
            dataGridView1.Columns[6].HeaderText = "Naziv";
            dataGridView1.Columns[6].Width = 100;
            dataGridView1.Columns[7].HeaderText = "StanicaDO";
            dataGridView1.Columns[7].Width = 60;
            dataGridView1.Columns[8].HeaderText = "Stanica Naziv";
            dataGridView1.Columns[8].Width = 100;
            dataGridView1.Columns[9].HeaderText = "Kolosek";
            dataGridView1.Columns[9].Width = 100;
            dataGridView1.Columns[10].HeaderText = "VaziOd";
            dataGridView1.Columns[10].Width = 100;
            dataGridView1.Columns[11].HeaderText = "VaziDo";
            dataGridView1.Columns[11].Width = 100;
            dataGridView1.Columns[12].HeaderText = "TrajeOd";
            dataGridView1.Columns[12].Width = 70;
            dataGridView1.Columns[13].HeaderText = "TrajeDo";
            dataGridView1.Columns[13].Width = 70;
            dataGridView1.Columns[14].HeaderText = "Aktivan";
            dataGridView1.Columns[14].Width = 50;
            dataGridView1.Columns[15].HeaderText = "Napomena";
            dataGridView1.Columns[15].Width = 150;
            dataGridView1.Columns[16].HeaderText = "PDF";
            dataGridView1.Columns[16].Width = 100;
            dataGridView1.Columns[17].HeaderText = "Narocita";
            dataGridView1.Columns[17].Width = 60;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            var query = "Select Telegrami.ID,BrojTelegrama,PrugaID,Pruga.Oznaka [Oznaka],Pruga.Opis [Naziv], " +
                           "OdStanice,Stanice.Opis,DoStanice,s.Opis,Kolosek,VaziOD,VaziDo,TrajeOd,TrajeDO,Aktivan,Napomena " +
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
            var query = "Select Telegrami.ID,BrojTelegrama,PrugaID,Pruga.Oznaka [Oznaka],Pruga.Opis [Naziv]," +
     "OdStanice,Stanice.Opis,DoStanice,s.Opis,Kolosek,VaziOD,VaziDo,TrajeOd,TrajeDO,Aktivan,Napomena,PDF,NarocitePosiljke " +
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
            var query = "Select Telegrami.ID,BrojTelegrama,PrugaID,Pruga.Oznaka [Oznaka],Pruga.Opis [Naziv], " +
                "OdStanice,Stanice.Opis,DoStanice,s.Opis,Kolosek,VaziOD,VaziDo,TrajeOd,TrajeDO,Aktivan,Napomena,PDF,NarocitaPosiljka " +
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
            var query = "Select Telegrami.ID,BrojTelegrama,PrugaID,Pruga.Oznaka [Oznaka],Pruga.Opis [Naziv], " +
                "OdStanice,Stanice.Opis,DoStanice,s.Opis,Kolosek,VaziOD,VaziDo,TrajeOd,TrajeDO,Aktivan,Napomena,PDF,NarocitaPosiljka " +
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
            var query = "Select Telegrami.ID,BrojTelegrama,PrugaID,Pruga.Oznaka [Oznaka],Pruga.Opis [Naziv], " +
                           "OdStanice,Stanice.Opis,DoStanice,s.Opis,Kolosek,VaziOD,VaziDo,TrajeOd,TrajeDO,Aktivan,Napomena,PDF,NarocitaPosiljka " +
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

            DataGridViewColumn id = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].HeaderText = "BR Telegrama";
            dataGridView1.Columns[1].Width = 75;
            dataGridView1.Columns[2].HeaderText = "PrugaID";
            dataGridView1.Columns[2].Width = 55;
            dataGridView1.Columns[3].HeaderText = "PrugaOznaka";
            dataGridView1.Columns[3].Width = 75;
            dataGridView1.Columns[4].HeaderText = "Naziv";
            dataGridView1.Columns[4].Width = 200;
            dataGridView1.Columns[5].HeaderText = "StanicaOD";
            dataGridView1.Columns[5].Width = 60;
            dataGridView1.Columns[6].HeaderText = "Naziv";
            dataGridView1.Columns[6].Width = 100;
            dataGridView1.Columns[7].HeaderText = "StanicaDO";
            dataGridView1.Columns[7].Width = 60;
            dataGridView1.Columns[8].HeaderText = "Stanica Naziv";
            dataGridView1.Columns[8].Width = 100;
            dataGridView1.Columns[9].HeaderText = "Kolosek";
            dataGridView1.Columns[9].Width = 100;
            dataGridView1.Columns[10].HeaderText = "VaziOd";
            dataGridView1.Columns[10].Width = 100;
            dataGridView1.Columns[11].HeaderText = "VaziDo";
            dataGridView1.Columns[11].Width = 100;
            dataGridView1.Columns[12].HeaderText = "TrajeOd";
            dataGridView1.Columns[12].Width = 70;
            dataGridView1.Columns[13].HeaderText = "TrajeDo";
            dataGridView1.Columns[13].Width = 70;
            dataGridView1.Columns[14].HeaderText = "Aktivan";
            dataGridView1.Columns[14].Width = 50;
            dataGridView1.Columns[15].HeaderText = "Napomena";
            dataGridView1.Columns[15].Width = 150;
            dataGridView1.Columns[16].HeaderText = "PDF";
            dataGridView1.Columns[16].Width = 100;
            dataGridView1.Columns[17].HeaderText = "Narocita";
            dataGridView1.Columns[17].Width = 60;
            timer1.Enabled = true;
            timer2.Enabled = false;
            timer3.Enabled = false;
            timer4.Enabled = false;
            timer1.Start();
        }

        private void btn_svi_Click(object sender, EventArgs e)
        {
            var query = "Select Telegrami.ID,BrojTelegrama,PrugaID,Pruga.Oznaka [Oznaka],Pruga.Opis [Naziv]," +
     "OdStanice,Stanice.Opis,DoStanice,s.Opis,Kolosek,VaziOD,VaziDo,TrajeOd,TrajeDO,Aktivan,Napomena " +
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
            DataGridViewColumn id = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].HeaderText = "BR Telegrama";
            dataGridView1.Columns[1].Width = 75;
            dataGridView1.Columns[2].HeaderText = "PrugaID";
            dataGridView1.Columns[2].Width = 55;
            dataGridView1.Columns[3].HeaderText = "PrugaOznaka";
            dataGridView1.Columns[3].Width = 75;
            dataGridView1.Columns[4].HeaderText = "Naziv";
            dataGridView1.Columns[4].Width = 200;
            dataGridView1.Columns[5].HeaderText = "StanicaOD";
            dataGridView1.Columns[5].Width = 60;
            dataGridView1.Columns[6].HeaderText = "Naziv";
            dataGridView1.Columns[6].Width = 100;
            dataGridView1.Columns[7].HeaderText = "StanicaDO";
            dataGridView1.Columns[7].Width = 60;
            dataGridView1.Columns[8].HeaderText = "Stanica Naziv";
            dataGridView1.Columns[8].Width = 100;
            dataGridView1.Columns[9].HeaderText = "Kolosek";
            dataGridView1.Columns[9].Width = 100;
            dataGridView1.Columns[10].HeaderText = "VaziOd";
            dataGridView1.Columns[10].Width = 100;
            dataGridView1.Columns[11].HeaderText = "VaziDo";
            dataGridView1.Columns[11].Width = 100;
            dataGridView1.Columns[12].HeaderText = "TrajeOd";
            dataGridView1.Columns[12].Width = 70;
            dataGridView1.Columns[13].HeaderText = "TrajeDo";
            dataGridView1.Columns[13].Width = 70;
            dataGridView1.Columns[14].HeaderText = "Aktivan";
            dataGridView1.Columns[14].Width = 50;
            dataGridView1.Columns[15].HeaderText = "Napomena";
            dataGridView1.Columns[15].Width = 150;
            dataGridView1.Columns[16].HeaderText = "PDF";
            dataGridView1.Columns[16].Width = 100;
            dataGridView1.Columns[17].HeaderText = "Narocita";
            dataGridView1.Columns[17].Width = 60;
            timer2.Enabled = true;
            timer1.Enabled = false;
            timer3.Enabled = false;
            timer4.Enabled = false;
            timer2.Start();
        }

        private void btn_Narocite_Click(object sender, EventArgs e)
        {
            var query = "Select Telegrami.ID,BrojTelegrama,PrugaID,Pruga.Oznaka [Oznaka],Pruga.Opis [Naziv], " +
                           "OdStanice,Stanice.Opis,DoStanice,s.Opis,Kolosek,VaziOD,VaziDo,TrajeOd,TrajeDO,Aktivan,Napomena,PDF,NarocitaPosiljka " +
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

            DataGridViewColumn id = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].HeaderText = "BR Telegrama";
            dataGridView1.Columns[1].Width = 75;
            dataGridView1.Columns[2].HeaderText = "PrugaID";
            dataGridView1.Columns[2].Width = 55;
            dataGridView1.Columns[3].HeaderText = "PrugaOznaka";
            dataGridView1.Columns[3].Width = 75;
            dataGridView1.Columns[4].HeaderText = "Naziv";
            dataGridView1.Columns[4].Width = 200;
            dataGridView1.Columns[5].HeaderText = "StanicaOD";
            dataGridView1.Columns[5].Width = 60;
            dataGridView1.Columns[6].HeaderText = "Naziv";
            dataGridView1.Columns[6].Width = 100;
            dataGridView1.Columns[7].HeaderText = "StanicaDO";
            dataGridView1.Columns[7].Width = 60;
            dataGridView1.Columns[8].HeaderText = "Stanica Naziv";
            dataGridView1.Columns[8].Width = 100;
            dataGridView1.Columns[9].HeaderText = "Kolosek";
            dataGridView1.Columns[9].Width = 100;
            dataGridView1.Columns[10].HeaderText = "VaziOd";
            dataGridView1.Columns[10].Width = 100;
            dataGridView1.Columns[11].HeaderText = "VaziDo";
            dataGridView1.Columns[11].Width = 100;
            dataGridView1.Columns[12].HeaderText = "TrajeOd";
            dataGridView1.Columns[12].Width = 70;
            dataGridView1.Columns[13].HeaderText = "TrajeDo";
            dataGridView1.Columns[13].Width = 70;
            dataGridView1.Columns[14].HeaderText = "Aktivan";
            dataGridView1.Columns[14].Width = 50;
            dataGridView1.Columns[15].HeaderText = "Napomena";
            dataGridView1.Columns[15].Width = 150;
            dataGridView1.Columns[16].HeaderText = "PDF";
            dataGridView1.Columns[16].Width = 100;
            dataGridView1.Columns[17].HeaderText = "Narocita";
            dataGridView1.Columns[17].Width = 60;
            timer4.Enabled = true;
            timer2.Enabled = false;
            timer3.Enabled = false;
            timer1.Enabled = false;
            timer4.Start();
        }
    }
}
