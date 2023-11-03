using Syncfusion.XlsIO.Parser.Biff_Records;
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

namespace Saobracaj.Pantheon_Export
{
    public partial class UlFakPregled : Form
    {
        private string connect = Sifarnici.frmLogovanje.connectionString;
        int ID;
        public UlFakPregled()
        {
            InitializeComponent();
            FillGV();
        }
        private void FillGV()
        {
            var select = "select ID,Predvidjanje,VrstaDokumenta,Tip,DatumPrijema,Valuta,Kurs,FakturaBr,IDDobavljaca,RacunDobavljaca,DatumIzdavanja,DatumPDVa,DatumValute,Referent,Napomena from UlFak order by ID desc";
            SqlConnection conn = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];


            dataGridView1.BorderStyle = BorderStyle.FixedSingle;
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
        public int Predvidjanje, Dobavljac, Referent;
        public string Valuta, VrstaDokumenta, TipDokumenta, FakturaBr, RacunDobavljaca, Napomena;
        public DateTime DatumPrijema, DatumIzdavanja, DatumPDVa, DatumValute;
        public decimal Kurs;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach(DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        ID = Convert.ToInt32(row.Cells[0].Value);
                        Predvidjanje = Convert.ToInt32(row.Cells[1].Value);
                        VrstaDokumenta = row.Cells[2].Value.ToString();
                        TipDokumenta = row.Cells[3].Value.ToString();
                        DatumPrijema = Convert.ToDateTime(row.Cells[4].Value);
                        Valuta = row.Cells[5].Value.ToString();
                        Kurs = Convert.ToDecimal(row.Cells[6].Value);
                        FakturaBr = row.Cells[7].Value.ToString();
                        Dobavljac = Convert.ToInt32(row.Cells[8].Value);
                        RacunDobavljaca = row.Cells[9].Value.ToString();
                        DatumIzdavanja = Convert.ToDateTime(row.Cells[10].Value);
                        DatumPDVa = Convert.ToDateTime(row.Cells[11].Value);
                        DatumValute = Convert.ToDateTime(row.Cells[12].Value);
                        Referent = Convert.ToInt32(row.Cells[13].Value);
                        Napomena = row.Cells[14].Value.ToString();
                    }
                }
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UlazneFakture frm = new UlazneFakture(ID,Predvidjanje,VrstaDokumenta,TipDokumenta,DatumPrijema,Valuta,Kurs,FakturaBr,Dobavljac,RacunDobavljaca,DatumIzdavanja,DatumPDVa,DatumValute,Referent,Napomena);
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UlazneFakture frm = new UlazneFakture();
            frm.Show();
        }
    }
}
