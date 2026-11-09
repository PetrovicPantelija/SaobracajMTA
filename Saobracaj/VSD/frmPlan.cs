using Microsoft.ReportingServices.Diagnostics.Internal;
using Saobracaj.Carinko;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.VSD
{
    public partial class frmPlan : Form
    {
        public string connect = Sifarnici.frmLogovanje.connectionString;
        bool status = false;
        public frmPlan()
        {
            InitializeComponent();
        }

        private void frmPlan_Load(object sender, EventArgs e)
        {
            FillGV();
            VratiPlan(1);
            RefreshDG();
        }

     
         private void VratiPlan(int Sifra)
            {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT [ID] " +
            " , [Godina]      , [Mesec]      , [UkupnoDana]      , [TekuceDana]      , [Naziv] " +
            " FROM [VSD].[dbo].[Plan] where ID=" + Sifra, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtID.Text = dr["ID"].ToString();
                txtNaziv.Text = dr["Naziv"].ToString();
                txtGodina.Text = dr["Godina"].ToString();
                txtMesec.Text = dr["Mesec"].ToString();
                txtTekuceDana.Text = dr["TekuceDana"].ToString();
                txtUkupnoDana.Text = dr["UkupnoDana"].ToString();
             
            }
            con.Close();
        }

        private void VratiPlanStavke(int SifraStavke)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT [ID] " +
            " , komercijalista, PlaniranaVrednost" +
            " FROM [VSD].[dbo].[PlanStavke] where ID=" + SifraStavke, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtID.Text = dr["ID"].ToString();
                cboKomercijalista.Text = dr["komercijalista"].ToString();
                txtPlaniranaProdaja.Text = dr["PlaniranaVrednost"].ToString();
              

            }
            con.Close();
        }


        private void RefreshDG()
        {
            var query = "select ID, PlanID, Komercijalista, PlaniranaVrednost from PlanStavke where PlanID = " + txtID.Text;
           
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            System.Data.DataSet ds = new System.Data.DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].HeaderText = "PlanID";
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].HeaderText = "Komercijalista";
            dataGridView1.Columns[2].Width = 250;
            dataGridView1.Columns[3].HeaderText = "Planirana prodaja";
            dataGridView1.Columns[3].Width = 100;

        }

        private void button22_Click(object sender, EventArgs e)
        {

        }

        private void button22_Click_1(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtID.Enabled = false;


            status = true;
            dataGridView1.ReadOnly = true;
            //dataGridView1.Rows.Clear();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                status = true;
            }



            if (status == true)
            {
                InsertPlan ins = new InsertPlan();
                ins.InsVSDPlan(txtGodina.Text, txtMesec.Text, Convert.ToInt32(txtUkupnoDana.Text), Convert.ToInt32(txtTekuceDana.Text), txtNaziv.Text);
               


                //  RefrechDataGridT();
                status = false;
                FillGV(); // da bi se postavila logika koje su kolone editabilne a koje nisu
            }
            else
            {
                InsertPlan upd = new InsertPlan();
                upd.UpdPlan(Convert.ToInt32(txtID.Text), txtGodina.Text, txtMesec.Text, Convert.ToInt32(txtUkupnoDana.Text), Convert.ToInt32(txtTekuceDana.Text), txtNaziv.Text);
                status = false;


                // RefrechDataGridT();
            }
            FillGV();
        }

        private void FillGV()
        {
            var select = "SELECT [ID] " +
            " , [Godina]      , [Mesec]      , [UkupnoDana]      , [TekuceDana]      , [Naziv] " +
            " FROM [VSD].[dbo].[Plan] order by ID DESC";
            
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(select, conn);
            var ds = new System.Data.DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            


            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "Godina";
            dataGridView2.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "Mesec";
            dataGridView2.Columns[2].Width = 70;

            DataGridViewColumn column4 = dataGridView2.Columns[3];
            dataGridView2.Columns[3].HeaderText = "Ukupno dana";
            dataGridView2.Columns[3].Width = 60;


            DataGridViewColumn column5 = dataGridView2.Columns[4];
            dataGridView2.Columns[4].HeaderText = "Izvozni";
            dataGridView2.Columns[4].Width = 70;
        }



        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {
                        txtID.Text = row.Cells[0].Value.ToString();
                        VratiPlan(Convert.ToInt32(txtID.Text));
                        RefreshDG();
                    }
                }
            }
            catch { }
        }

        private void btn_Aktivni_Click(object sender, EventArgs e)
        {
            InsertPlan ins = new InsertPlan();
            ins.InsVSDPlanStavke(Convert.ToInt32(txtID.Text), cboKomercijalista.Text, Convert.ToDouble(txtPlaniranaProdaja.Text));
            RefreshDG();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txtPlaStavkeID.Text = row.Cells[0].Value.ToString();
                        VratiPlanStavke(Convert.ToInt32(txtPlaStavkeID.Text));
                    }
                }
            }
            catch { }
        }

        private void btn_narocite_Click(object sender, EventArgs e)
        {
            InsertPlan ins = new InsertPlan();
            ins.UpdPlanStavke(Convert.ToInt32(txtPlaStavkeID.Text), Convert.ToInt32(txtPlaStavkeID.Text), cboKomercijalista.Text, Convert.ToDouble(txtPlaniranaProdaja.Text));
            RefreshDG();
        }

        private void btn_svi_Click(object sender, EventArgs e)
        {
            InsertPlan ins = new InsertPlan();
            ins.DelPlanStavke(Convert.ToInt32(txtPlaStavkeID.Text));
            RefreshDG();
        }
    }
}
