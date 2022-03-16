using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace Saobracaj.Sifarnici
{
    public partial class frmTelegrami : Form
    {
        public static string code = "frmTelegrami";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Sifarnici.frmLogovanje.user.ToString();

        OpenFileDialog ofd1 = new OpenFileDialog();
        FolderBrowserDialog fbd1 = new FolderBrowserDialog();

        bool status = false;
        public string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public frmTelegrami()
        {
            InitializeComponent();
            IdGrupe();
            IdForme();
            PravoPristupa();

        }
        public int IdGrupe()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            //Sifarnici.frmLogovanje frm = new Sifarnici.frmLogovanje();         
            string query = "Select IdGrupe from KorisnikGrupa Where Korisnik = " + "'" + Kor.TrimEnd() + "'";
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                idGrupe = Convert.ToInt32(dr["IdGrupe"].ToString());
            }
            conn.Close();
            return idGrupe;
        }
        private int IdForme()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select IdForme from Forme where Rtrim(Code)=" + "'" + code + "'";
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                idForme = Convert.ToInt32(dr["IdForme"].ToString());
            }
            conn.Close();
            return idForme;
        }
        private void PravoPristupa()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select * From GrupeForme Where IdGrupe=" + idGrupe + " and IdForme=" + idForme;
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows == false)
            {
                MessageBox.Show("Nemate prava za pristup ovoj formi", code);
                Pravo = false;
            }
            else
            {
                Pravo = true;
                while (reader.Read())
                {
                    insert = Convert.ToBoolean(reader["Upis"]);
                    if (insert == false)
                    {
                        tsNew.Enabled = false;
                    }
                    update = Convert.ToBoolean(reader["Izmena"]);
                    if (update == false)
                    {
                        tsSave.Enabled = false;
                    }
                    delete = Convert.ToBoolean(reader["Brisanje"]);
                    if (delete == false)
                    {
                        //tsDelete.Enabled = false;
                    }
                }
            }

            conn.Close();
        }
        private void frmTelegrami_Load(object sender, EventArgs e)
        {

            FillCombo();

            dt_VaziDo.Value = DateTime.Now;
            dt_VaziOd.Value = DateTime.Now;
            RefreshDG();

        }
        private void FillCombo()
        {
            var query = " Select ID, (RTrim(Oznaka) + '-' + Rtrim(Opis)) as Opis From Pruga";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cboPruga.DataSource = ds.Tables[0];
            cboPruga.DisplayMember = "Opis";
            cboPruga.ValueMember = "ID";

            var odStanice = "Select ID,Opis From Stanice";
            SqlDataAdapter da2 = new SqlDataAdapter(odStanice, conn);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);
            combo_OdStanice.DataSource = ds2.Tables[0];
            combo_OdStanice.DisplayMember = "Opis";
            combo_OdStanice.ValueMember = "ID";

            var DoStanice = "Select ID,Opis From Stanice";
            SqlDataAdapter da3 = new SqlDataAdapter(DoStanice, conn);
            DataSet ds3 = new DataSet();
            da3.Fill(ds3);
            combo_DoStanice.DataSource = ds3.Tables[0];
            combo_DoStanice.DisplayMember = "Opis";
            combo_DoStanice.ValueMember = "ID";
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
        private void tsNew_Click(object sender, EventArgs e)
        {
            txt_ID.Text = "";
            txt_ID.Enabled = false;
            status = true;
        }
        private void tsSave_Click(object sender, EventArgs e)
        {
            InsertTelegrami insert = new InsertTelegrami();
            bool aktivan = false;
            bool narocita = false;
            var VaziOd = Convert.ToDateTime(dt_VaziOd.Value.ToShortDateString() + dt_VaziOdT.Value.ToLongTimeString());
            var VaziDo = Convert.ToDateTime(dt_VaziDo.Value.ToShortDateString() + dt_VaziDoT.Value.ToLongTimeString());

            if (cb_Aktivni.Checked)
            {
                aktivan = true;
            }
            if (status == true)
            {
                insert.InsTelegrami(Convert.ToInt32(txt_BrTelegrama.Text), Convert.ToInt32(cboPruga.SelectedValue.ToString()), Convert.ToInt32(combo_OdStanice.SelectedValue),
                    Convert.ToInt32(combo_DoStanice.SelectedValue), txt_kolosek.Text, VaziOd, VaziDo, dt_TrajeOd.Value, dt_TrajeDo.Value, txt_Napomena.Text, aktivan);
                RefreshDG();
                txt_ID.Enabled = true;
                status = false;
            }
            else
            {
                insert.UpdTelegrami(Convert.ToInt32(txt_ID.Text), Convert.ToInt32(txt_BrTelegrama.Text), Convert.ToInt32(cboPruga.SelectedValue.ToString()), Convert.ToInt32(combo_OdStanice.SelectedValue),
                    Convert.ToInt32(combo_DoStanice.SelectedValue), txt_kolosek.Text, VaziOd, VaziDo, dt_TrajeOd.Value, dt_TrajeDo.Value, txt_Napomena.Text, aktivan);
                RefreshDG();
            }

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

        private void btn_Aktivni_Click(object sender, EventArgs e)
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

        private void timer2_Tick(object sender, EventArgs e)
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
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txt_ID.Text = row.Cells[0].Value.ToString();
                        txt_BrTelegrama.Text = row.Cells[1].Value.ToString();
                        cboPruga.Text = row.Cells[4].Value.ToString();
                        txt_kolosek.Text = row.Cells[9].Value.ToString();
                        txt_ID.Enabled = false;
                        dt_TrajeOd.Value = Convert.ToDateTime(row.Cells[12].Value.ToString());
                        dt_TrajeDo.Value = Convert.ToDateTime(row.Cells[13].Value.ToString());
                        txt_PDF.Text = row.Cells[16].Value.ToString();
                        bool aktivan,narocita;
                        //aktivan=row
                        aktivan = Convert.ToBoolean(row.Cells[14].Value);
                        if (aktivan == true) { cb_Aktivni.Checked = true; } else { cb_Aktivni.Checked = false; }
                        txt_Napomena.Text = row.Cells[15].Value.ToString();
                        narocita = Convert.ToBoolean(row.Cells[17].Value);
                        if (narocita == true) { cb_Narocita.Checked = true; } else { cb_Narocita.Checked = false; }

                    }
                }
            }
            catch
            {

            }

        }

        private void btn_dani_Click(object sender, EventArgs e)
        {
            var query = "Select Telegrami.ID,BrojTelegrama,PrugaID,Pruga.Oznaka [Oznaka],Pruga.Opis [Naziv], " +
                "OdStanice,Stanice.Opis,DoStanice,s.Opis,Kolosek,VaziOD,VaziDo,TrajeOd,TrajeDO,Aktivan,Napomena " +
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
            timer3.Enabled = true;
            timer1.Enabled = false;
            timer2.Enabled = false;
            timer4.Enabled = false;
            timer3.Start();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            var query = "Select Telegrami.ID,BrojTelegrama,PrugaID,Pruga.Oznaka [Oznaka],Pruga.Opis [Naziv], " +
                "OdStanice,Stanice.Opis,DoStanice,s.Opis,Kolosek,VaziOD,VaziDo,TrajeOd,TrajeDO,Aktivan,Napomena " +
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
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmTelegramiPrikazi tel = new frmTelegramiPrikazi();
            tel.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
