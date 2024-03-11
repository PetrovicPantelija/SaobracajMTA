using Saobracaj.Sifarnici;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Sifarnici
{
    public partial class frmVagoniSerije : Form
    {
        string Kor = Sifarnici.frmLogovanje.user.ToString();
        public string connect = frmLogovanje.connectionString;
        bool status = false;
        int count;
        int idPom;
        string nazivPom;
        string niz = "";
        public frmVagoniSerije()
        {
            InitializeComponent();
            txtID.Enabled = false;

        }
        public static string code = "frmDodeliGrupu";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;

        public void RefreshGV()
        {
            var query = "Select * from VagoniSerije";
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
                foreach(DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txtID.Text = row.Cells[0].Value.ToString();
                        txtSerija.Text = row.Cells[1].Value.ToString();
                        txtBrojcanaSerija.Text = row.Cells[2].Value.ToString();
                        txtBrojOsovina.Value = Convert.ToInt32(row.Cells[3].Value.ToString());
                    }
                }
            }
            catch { }
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtID.Enabled = true;
            status = true;
            tsNew.Enabled = false;
        }


        private void tsSave_Click(object sender, EventArgs e)
        {
            InsertVagoniSerije lok = new InsertVagoniSerije();
            if (status == true)
            {
                lok.InsVagoniSerije(txtSerija.Text.ToString().TrimEnd(), txtBrojcanaSerija.Text, Convert.ToInt32(txtBrojOsovina.Value));
                status = false;
                tsNew.Enabled = true;
                
            }
            else
            {
                lok.UpdVagoniSerije(Convert.ToInt32(txtID.Text.ToString()), txtSerija.Text.ToString().TrimEnd(),txtBrojcanaSerija.Text,  Convert.ToInt32(txtBrojOsovina.Value));
            }
            RefreshGV();
        }
        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertVagoniSerije lok = new InsertVagoniSerije();
            lok.DelVagoniSerije(Convert.ToInt32(txtID.Text));
            RefreshGV();
        }

        private void frmVagoniSerije_Load(object sender, EventArgs e)
        {
            RefreshGV();
        }

        private void txtBrojcanaSerija_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
