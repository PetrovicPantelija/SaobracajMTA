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
        bool status = false;
        public string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public frmTelegrami()
        {
            InitializeComponent();


        }
        private void frmTelegrami_Load(object sender, EventArgs e)
        {
            var query = " Select ID, (RTrim(Oznaka) + '-' + Rtrim(Opis)) as Opis From Pruga";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cboPruga.DataSource = ds.Tables[0];
            cboPruga.DisplayMember = "Opis";
            cboPruga.ValueMember = "ID";
            dt_DoStanice.Value = DateTime.Now;
            dt_OdStanice.Value = DateTime.Now;
            dt_VaziDo.Value = DateTime.Now;
            dt_VaziOd.Value = DateTime.Now;
            RefreshDG();

        }
        private void RefreshDG()
        {
            var query = "select t.Id,BrojTelegrama,PrugaID,p.Oznaka [Oznaka], p.Opis[Naziv],OdStanice,DoStanice,Kolosek,VaziOd,VaziDo,TrajeOd,TrajeDo,Aktivan,Napomena " +
                "From Telegrami t, Pruga p Where p.ID = t.PrugaID order by t.ID desc";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn id = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].HeaderText = "BrojTelegrama";
            dataGridView1.Columns[1].Width = 75;
            dataGridView1.Columns[2].HeaderText = "PrugaID";
            dataGridView1.Columns[2].Width = 55;
            dataGridView1.Columns[3].HeaderText = "PrugaOznaka";
            dataGridView1.Columns[3].Width = 75;
            dataGridView1.Columns[4].HeaderText = "Naziv";
            dataGridView1.Columns[4].Width = 200;
            dataGridView1.Columns[5].HeaderText = "OdStanice";
            dataGridView1.Columns[5].Width = 100;
            dataGridView1.Columns[6].HeaderText = "DoStanice";
            dataGridView1.Columns[6].Width = 100;
            dataGridView1.Columns[7].HeaderText = "Kolosek";
            dataGridView1.Columns[7].Width = 100;
            dataGridView1.Columns[8].HeaderText = "VaziOd";
            dataGridView1.Columns[8].Width = 100;
            dataGridView1.Columns[9].HeaderText = "VaziDo";
            dataGridView1.Columns[9].Width = 100;
            dataGridView1.Columns[10].HeaderText = "TrajeOd";
            dataGridView1.Columns[10].Width = 70;
            dataGridView1.Columns[11].HeaderText = "TrajeDo";
            dataGridView1.Columns[11].Width = 70;
            dataGridView1.Columns[12].HeaderText = "Aktivan";
            dataGridView1.Columns[12].Width = 50;
            dataGridView1.Columns[13].HeaderText = "Napomena";
            dataGridView1.Columns[13].Width = 150;
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
            var OdStnice = Convert.ToDateTime(dt_OdStanice.Value.ToShortDateString() + dt_OdStaniceT.Value.ToLongTimeString());
            var DoStnice = Convert.ToDateTime(dt_DoStanice.Value.ToShortDateString() + dt_DoStaniceT.Value.ToLongTimeString());
            var VaziOd = Convert.ToDateTime(dt_VaziOd.Value.ToShortDateString() + dt_VaziOdT.Value.ToLongTimeString());
            var VaziDo = Convert.ToDateTime(dt_VaziDo.Value.ToShortDateString() + dt_VaziDoT.Value.ToLongTimeString());

            if (cb_Aktivni.Checked)
            {
                aktivan = true;
            }
            if (status == true)
            {
                insert.InsTelegrami(Convert.ToInt32(txt_BrTelegrama.Text), Convert.ToInt32(cboPruga.SelectedValue.ToString()), OdStnice,
                    DoStnice, txt_kolosek.Text, VaziOd, VaziDo, dt_TrajeOd.Value, dt_TrajeDo.Value, txt_Napomena.Text, aktivan);
                RefreshDG();
                txt_ID.Enabled = true;
                status = false;
            }
            else
            {
                insert.UpdTelegrami(Convert.ToInt32(txt_ID.Text), Convert.ToInt32(txt_BrTelegrama.Text), Convert.ToInt32(cboPruga.SelectedValue.ToString()), OdStnice,
                    DoStnice, txt_kolosek.Text, VaziOd, VaziDo, dt_TrajeOd.Value, dt_TrajeDo.Value, txt_Napomena.Text, aktivan);
                RefreshDG();

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var query = "select t.Id,BrojTelegrama,PrugaID,p.Oznaka [Oznaka], p.Opis[Naziv],OdStanice,DoStanice,Kolosek,VaziOd,VaziDo,TrajeOd,TrajeDo,Aktivan,Napomena " +
                "From Telegrami t, Pruga p Where p.ID = t.PrugaID and Aktivan=1 order by t.ID desc";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btn_svi_Click(object sender, EventArgs e)
        {
            var query = "select t.Id,BrojTelegrama,PrugaID,p.Oznaka [Oznaka], p.Opis[Naziv],OdStanice,DoStanice,Kolosek,VaziOd,VaziDo,TrajeOd,TrajeDo,Aktivan,Napomena " +
                "From Telegrami t, Pruga p Where p.ID = t.PrugaID order by t.ID desc";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn id = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].HeaderText = "BrojTelegrama";
            dataGridView1.Columns[1].Width = 75;
            dataGridView1.Columns[2].HeaderText = "PrugaID";
            dataGridView1.Columns[2].Width = 55;
            dataGridView1.Columns[3].HeaderText = "PrugaOznaka";
            dataGridView1.Columns[3].Width = 75;
            dataGridView1.Columns[4].HeaderText = "Naziv";
            dataGridView1.Columns[4].Width = 200;
            dataGridView1.Columns[5].HeaderText = "OdStanice";
            dataGridView1.Columns[5].Width = 100;
            dataGridView1.Columns[6].HeaderText = "DoStanice";
            dataGridView1.Columns[6].Width = 100;
            dataGridView1.Columns[7].HeaderText = "Kolosek";
            dataGridView1.Columns[7].Width = 100;
            dataGridView1.Columns[8].HeaderText = "VaziOd";
            dataGridView1.Columns[8].Width = 100;
            dataGridView1.Columns[9].HeaderText = "VaziDo";
            dataGridView1.Columns[9].Width = 100;
            dataGridView1.Columns[10].HeaderText = "TrajeOd";
            dataGridView1.Columns[10].Width = 70;
            dataGridView1.Columns[11].HeaderText = "TrajeDo";
            dataGridView1.Columns[11].Width = 70;
            dataGridView1.Columns[12].HeaderText = "Aktivan";
            dataGridView1.Columns[12].Width = 50;
            dataGridView1.Columns[13].HeaderText = "Napomena";
            dataGridView1.Columns[13].Width = 150;
            timer2.Enabled = true;
            timer1.Enabled = false;
            timer3.Enabled = false;
            timer2.Start();
        }

        private void btn_Aktivni_Click(object sender, EventArgs e)
        {
            var query = "select t.Id,BrojTelegrama,PrugaID,p.Oznaka [Oznaka], p.Opis[Naziv],OdStanice,DoStanice,Kolosek,VaziOd,VaziDo,TrajeOd,TrajeDo,Aktivan,Napomena " +
                "From Telegrami t, Pruga p Where p.ID = t.PrugaID and Aktivan=1 order by t.ID desc";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn id = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].HeaderText = "BrojTelegrama";
            dataGridView1.Columns[1].Width = 75;
            dataGridView1.Columns[2].HeaderText = "PrugaID";
            dataGridView1.Columns[2].Width = 55;
            dataGridView1.Columns[3].HeaderText = "PrugaOznaka";
            dataGridView1.Columns[3].Width = 75;
            dataGridView1.Columns[4].HeaderText = "Naziv";
            dataGridView1.Columns[4].Width = 200;
            dataGridView1.Columns[5].HeaderText = "OdStanice";
            dataGridView1.Columns[5].Width = 100;
            dataGridView1.Columns[6].HeaderText = "DoStanice";
            dataGridView1.Columns[6].Width = 100;
            dataGridView1.Columns[7].HeaderText = "Kolosek";
            dataGridView1.Columns[7].Width = 100;
            dataGridView1.Columns[8].HeaderText = "VaziOd";
            dataGridView1.Columns[8].Width = 100;
            dataGridView1.Columns[9].HeaderText = "VaziDo";
            dataGridView1.Columns[9].Width = 100;
            dataGridView1.Columns[10].HeaderText = "TrajeOd";
            dataGridView1.Columns[10].Width = 70;
            dataGridView1.Columns[11].HeaderText = "TrajeDo";
            dataGridView1.Columns[11].Width = 70;
            dataGridView1.Columns[12].HeaderText = "Aktivan";
            dataGridView1.Columns[12].Width = 50;
            dataGridView1.Columns[13].HeaderText = "Napomena";
            dataGridView1.Columns[13].Width = 150;
            timer1.Enabled = true;
            timer2.Enabled = false;
            timer3.Enabled = false;
            timer1.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            var query = "select t.Id,BrojTelegrama,PrugaID,p.Oznaka [Oznaka], p.Opis[Naziv],OdStanice,DoStanice,Kolosek,VaziOd,VaziDo,TrajeOd,TrajeDo,Aktivan,Napomena " +
                "From Telegrami t, Pruga p Where p.ID = t.PrugaID order by t.ID desc";
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
                        txt_kolosek.Text = row.Cells[7].Value.ToString();
                        txt_ID.Enabled = false;
                        dt_TrajeOd.Value = Convert.ToDateTime(row.Cells[10].Value.ToString());
                        dt_TrajeDo.Value = Convert.ToDateTime(row.Cells[11].Value.ToString());
                        bool aktivan;
                        //aktivan=row
                        aktivan = Convert.ToBoolean(row.Cells[12].Value);
                        if (aktivan == true) { cb_Aktivni.Checked = true; } else { cb_Aktivni.Checked = false; }
                        txt_Napomena.Text = row.Cells[13].Value.ToString();
                            
                    }
                }
            }
            catch
            {

            }

        }

        private void btn_dani_Click(object sender, EventArgs e)
        {
            var query = "select t.Id,BrojTelegrama,PrugaID,p.Oznaka [Oznaka], p.Opis[Naziv],OdStanice,DoStanice,Kolosek,VaziOd,VaziDo,TrajeOd,TrajeDo," +
                "Aktivan,Napomena From Telegrami t, Pruga p Where (VaziDo Between Convert(Date,Getdate()) and Convert(Date,GetDate()+7)) and " +
                "p.ID = t.PrugaID and Aktivan=1 order by t.ID desc";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            timer3.Enabled = true;
            timer1.Enabled = false;
            timer2.Enabled = false;
            timer3.Start();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            var query = "select t.Id,BrojTelegrama,PrugaID,p.Oznaka [Oznaka], p.Opis[Naziv],OdStanice,DoStanice,Kolosek,VaziOd,VaziDo,TrajeOd,TrajeDo," +
                "Aktivan,Napomena From Telegrami t, Pruga p Where (VaziDo Between Convert(Date,Getdate()) and Convert(Date,GetDate()+7)) and " +
                "p.ID = t.PrugaID and Aktivan=1 order by t.ID desc";
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
    }
}
