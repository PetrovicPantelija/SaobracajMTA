using Microsoft.ReportingServices.Diagnostics.Internal;
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

namespace Saobracaj.Izvoz
{
    public partial class Vaganje : Syncfusion.Windows.Forms.Office2010Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

        string brojKontejnera;
        string  Vozilo, Vozac;
        int kontID, vrsta;
        public Vaganje()
        {
            InitializeComponent();
        }
        public Vaganje(string kontejner,int KontID, string VoziloK, string Vozac)
        {
            InitializeComponent();
            brojKontejnera = kontejner;
           
            kontID = KontID;
            Vozilo = VoziloK;
        }
        

        private void Vaganje_Load(object sender, EventArgs e)
        {
            txtKontejner.Text = brojKontejnera;
            cbovrstakontejnera.SelectedValue = vrsta;
            txtKontID.Text = kontID.ToString();

            SqlConnection conn = new SqlConnection(connection);

            var TipKontenjera = "Select ID,SkNaziv from TipKontenjera order by ID";
            var daTK = new SqlDataAdapter(TipKontenjera, conn);
            var daDS = new System.Data.DataSet();
            daTK.Fill(daDS);
            cbovrstakontejnera.DataSource = daDS.Tables[0];
            cbovrstakontejnera.DisplayMember = "SkNaziv";
            cbovrstakontejnera.ValueMember = "ID";


            VratiVrstuKontejnera(txtKontID.Text);

            FillGV();
        }

        private void VratiVrstuKontejnera(string KontID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" select VrstaKontejnera, BrojKontejnera, Vozilo from IzvozKonacna " +
             " where IzvozKonacna.ID = " + KontID, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
             
                cbovrstakontejnera.SelectedValue = Convert.ToInt32(dr["VrstaKontejnera"].ToString());
                txtKontejner.Text = dr["BrojKontejnera"].ToString();
                txtVozilo.Text = dr["Vozilo"].ToString();
            }

            con.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("Select ID From Vaganje Where IzvozKonacnaID='" + txtKontID.Text+"'", con);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                MessageBox.Show("Kontejner je već vagan!");
                return;
            }
            else
            {
                InsertIzvoz ins = new InsertIzvoz();
                ins.InsVaganje(Convert.ToInt32(txtKontID.Text), txtVozilo.Text, txtKontejner.Text.ToString().TrimEnd(),  txtVagarskaPotvrda.Text.ToString().TrimEnd(), Convert.ToDecimal(txtBruto.Text),
                    Convert.ToDecimal(txtTara.Text), Convert.ToDecimal(txtNeto.Text), Convert.ToDateTime(dtpDatumMerenja.Value), Sifarnici.frmLogovanje.user);
            }
            con.Close();
            FillGV();
        }
        private void FillGV()
        {
            var select = "select * from Vaganje";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new System.Data.DataSet();
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
        }

        private void txtNeto_MouseEnter(object sender, EventArgs e)
        {
            if(txtBruto.Text!="" && txtTara.Text != "")
            {
                decimal neto;
                neto = Convert.ToDecimal(txtBruto.Text) - Convert.ToDecimal(txtTara.Text);
                txtNeto.Text = neto.ToString();
            }
        }
        int ID;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    ID = Convert.ToInt32(row.Cells["ID"].Value.ToString());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IzvestajVaganja frm = new IzvestajVaganja(ID);
            frm.Show();
        }
    }
    
}
