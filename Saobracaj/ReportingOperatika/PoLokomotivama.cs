using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.ReportingOperatika
{
    public partial class PoLokomotivama : Form
    {
        string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        public PoLokomotivama()
        {
            InitializeComponent();
        }

        private void btnIzracunaj_Click(object sender, EventArgs e)
        {
            FillGV();
        }

        private void FillGV()
        {
            var select = " select AktivnostiStavke.OznakaPosla, AktivnostiStavke.DatumPocetka as PocetakAktivnosti, AktivnostiStavke.DatumZavrsetka as ZavrsetakAktivnosti, " +
                " DateDiff(hour, AktivnostiStavke.DatumPocetka, IsNUll(AktivnostiStavke.DatumZavrsetka, GetDate())) as SatiTrajanjaAktivnosti , AktivnostiStavke.Lokomotiva, Vuca.MotoSati, " +
                "Vuca.Kilometraza, Vuca.NivoGoriva, (RTRIM(Delavci.DeIme) + '  ' + RTRIM(Delavci.DePriimek)) as Zaposleni, Aktivnosti.VremeOd as PocetakSmene, " +
                "Aktivnosti.VremeDo as ZavrsetakSmene,  DateDiff(hour, Aktivnosti.VremeOd, IsNUll(Aktivnosti.VremeDo, GetDate())) as SatiTrajanjaSmene, " +
                " DateDiff(hour,  IsNUll(Aktivnosti.VremeDo, GetDate()),GEtDAte()) as SatiOdOdjaveSmene, DateDiff(hour,  IsNUll(Aktivnosti.VremeOd, GetDate()),GEtDAte()) as SatiOdPrijaveSmene  From AktivnostiStavke inner join Vuca on Vuca.StavkaAktivnostiID = AktivnostiStavke.ID " +
 " inner join Aktivnosti on Aktivnosti.ID = AktivnostiStavke.IDNadredjena " +
  " inner join Delavci on Delavci.DeSifra = Aktivnosti.Zaposleni " +
  " where DatumPocetka >= ' " + dtpVremeOd.Text + "' and DatumPocetka <= ' " + dtpVremeDo.Text + "' and VrstaAktivnostiID = 61 " +
  " order by  AktivnostiStavke.Lokomotiva";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
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

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "Oznaka";
            dataGridView1.Columns[0].Width = 100;

        }
    }
}
