using Saobracaj.Sifarnici;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Dokumenta
{
    public partial class frmPregledLokomotivaPrimopredaja : Form
    {
        public string connect = frmLogovanje.connectionString;
        public frmPregledLokomotivaPrimopredaja()
        {
            InitializeComponent();
        }

        private void frmPregledLokomotivaPrimopredaja_Load(object sender, EventArgs e)
        {
            FillGV();
        }
        private void FillGV()
        {

            var select =
" SELECT AktivnostiStavke.ID, VrstaAktivnosti.Naziv as Aktivnost, AktivnostiStavke.Sati, AktivnostiStavke.Posao, AktivnostiStavke.OznakaPosla, AktivnostiStavke.DatumPocetka, AktivnostiStavke.DatumZavrsetka, " +
" (RTRim(Delavci.DeIme) + ' ' + RTRIM(DePriimek)) as Izvrsilac, Stanice.Opis as Stanica, l1.Lokomotiva, AktivnostiStavke.MestoIzvrsenja, AktivnostiStavke.Napomena " +
" from AktivnostiStavke " +
" inner join Aktivnosti on Aktivnosti.ID = AktivnostiStavke.IdNadredjena " +
" inner join Delavci on Delavci.DeSifra = Aktivnosti.Zaposleni  " +
" inner join Stanice on Stanice.ID = AktivnostiStavke.Stanica " +
" inner join VrstaAktivnosti on VrstaAktivnosti.ID = AktivnostiStavke.VrstaAktivnostiID " +
" left join(Select* from LokomotivaPrijava where Smer = 1) as l1 on l1.AktivnostId = Aktivnosti.ID " +
"  where AktivnostiStavke.VrstaAktivnostiID = 73 " +
" order by AktivnostiStavke.ID desc ";

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
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
            /*
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[2].HeaderText = "Zaposleni";
            dataGridView1.Columns[3].HeaderText = "DatumPrijave";
            dataGridView1.Columns[6].HeaderText = "Registarski BR";
            dataGridView1.Columns[8].HeaderText = "Relacija";
            dataGridView1.Columns[11].HeaderText = "KilometrazaZaduzivanje";
            dataGridView1.Columns[12].HeaderText = "KilometrazaRazduzivanje";
            */



        }

        private void btn_OtvoriSliku_Click(object sender, EventArgs e)
        {
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
                        txtSifra.Text = row.Cells[0].Value.ToString();
                       

                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
            FillGV2AktivnostiPregledSlike(txtSifra.Text);
        }

        private void FillGV2AktivnostiPregledSlike(string ID)
        {
            var select = " SELECT TOP (1000) [ID]      ,[IDStavkeAktivnosti]      ,[Putanja] " +
                         " FROM [AktivnostiStavkeDokumenta] " +
                         " where IDStavkeAktivnosti = '" + ID + "'";


            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds.Tables[0];

            dataGridView3.BorderStyle = BorderStyle.None;
            dataGridView3.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView3.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView3.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView3.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView3.BackgroundColor = Color.White;

            dataGridView3.EnableHeadersVisualStyles = false;
            dataGridView3.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView3.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            /*
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[2].HeaderText = "Zaposleni";
            dataGridView1.Columns[3].HeaderText = "DatumPrijave";
            dataGridView1.Columns[6].HeaderText = "Registarski BR";
            dataGridView1.Columns[8].HeaderText = "Relacija";
            dataGridView1.Columns[11].HeaderText = "KilometrazaZaduzivanje";
            dataGridView1.Columns[12].HeaderText = "KilometrazaRazduzivanje";
            */
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                txtPutanja.Text = txtPutanja.Text.Replace("192.168.129.7", "\\\\192.168.129.7\\TA");
                System.Diagnostics.Process.Start(txtPutanja.Text);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {

            try
            {
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    if (row.Selected)
                    {
                        txtSifra.Text = row.Cells[0].Value.ToString();
                        txtPutanja.Text = row.Cells[2].Value.ToString();

                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }
    }
}
