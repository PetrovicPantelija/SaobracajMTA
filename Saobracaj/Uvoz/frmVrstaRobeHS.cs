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

namespace Saobracaj.Uvoz
{
    public partial class frmVrstaRobeHS : Form
    {
        bool status = false;
        string connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
       
        public frmVrstaRobeHS()
        {
            InitializeComponent();
            FillGV();
        }
        private void FillGV()
        {
            var select = "Select * From VrstaRobeHS order by ID desc";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            tsNew.Enabled = false;
            status = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            InsertVrstaRobeHS ins = new InsertVrstaRobeHS();
            if (status == true)
            {
                ins.InsVrstaRobeHS(txtNaziv.Text.ToString().TrimEnd(),txtHSCode.Text) ;
            }
            else
            {
                ins.UpdVrstaRobeHS(Convert.ToInt32(txtID.Text.ToString()),txtNaziv.Text.ToString().TrimEnd(), txtHSCode.Text);
            }
            FillGV();
            tsNew.Enabled = true;
            status = false;
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertVrstaRobeHS ins = new InsertVrstaRobeHS();
            ins.DelVrstaRobeHS(Convert.ToInt32(txtID.Text.ToString()));
            FillGV();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txtID.Text = row.Cells[0].Value.ToString();
                        txtNaziv.Text = row.Cells[1].Value.ToString();
                       txtHSCode.Text = row.Cells[2].Value.ToString();
                    }
                }
            }
            catch { }
        }
    }
}
