using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Pantheon_Export
{
    public partial class IzlazneFakturePregled : Form
    {
        private string connect = Sifarnici.frmLogovanje.connectionString;

        public IzlazneFakturePregled()
        {
            InitializeComponent();
            FillGV();
        }
        private void FillGV()
        {
            var select = "Select FaStFak,FaDatFak,FaPartPlac,FaDatVal,Kurs,FaObdobje,FaValutaCene,MestoUtovara,DatumUtovara,FaDostMesto,DatumIstovara,FaRefer,FaOpomba1,FaOpomba2,PaNaziv " +
                "From Faktura inner join Partnerji on Faktura.FaPartPlac=Partnerji.PaSifra  order by FaStFak desc";
            SqlConnection conn = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
           
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
        }
        public string Valuta,MestoUtovara,MestoIstovara,Izjava,Napomena;
        public int ID,Primalac,Referent;
        public DateTime DatumDokumenta,DatumPDV,DatumValute, DatumUtovara,DatumIstovara;
        public decimal Kurs;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        ID = Convert.ToInt32(row.Cells["FaStFak"].Value);
                        DatumDokumenta = Convert.ToDateTime(row.Cells["FaDatFak"].Value);
                        Primalac = Convert.ToInt32(row.Cells["FaPartPlac"].Value);
                        Valuta = row.Cells["FaValutaCene"].Value.ToString().TrimEnd();
                        Kurs = Convert.ToDecimal(row.Cells["Kurs"].Value.ToString());
                        DatumPDV = Convert.ToDateTime(row.Cells["FaObdobje"].Value);
                        DatumValute = Convert.ToDateTime(row.Cells["FaDatVal"].Value);
                        MestoUtovara = row.Cells["MestoUtovara"].Value.ToString();
                        DatumUtovara = Convert.ToDateTime(row.Cells["DatumUtovara"].Value);
                        MestoIstovara = row.Cells["FaDostMesto"].Value.ToString();
                        DatumIstovara = Convert.ToDateTime(row.Cells["DatumIstovara"].Value);
                        Referent = Convert.ToInt32(row.Cells["FaRefer"].Value);
                        Izjava = row.Cells["FaOpomba1"].Value.ToString();
                        Napomena = row.Cells["FaOpomba2"].Value.ToString();
                    }
                }
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Pantheon_Export.IzlazneFakture frm = new IzlazneFakture();
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {//NEce otvoriti
            Pantheon_Export.IzlazneFakture frm = new IzlazneFakture(ID,DatumDokumenta,Primalac,Valuta,Kurs,DatumPDV,DatumValute,MestoUtovara,DatumUtovara,MestoIstovara,DatumIstovara,Referent,Izjava,Napomena);
            frm.Show();
        }
    }
}
