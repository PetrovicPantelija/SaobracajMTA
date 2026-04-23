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
        public frmPlan()
        {
            InitializeComponent();
        }

        private void frmPlan_Load(object sender, EventArgs e)
        {
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


        private void RefreshDG()
        {
            var query = "select ID, PlanID, Komercijalista, PlaniranaVrednost from PlanStavke";
           
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
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
    }
}
