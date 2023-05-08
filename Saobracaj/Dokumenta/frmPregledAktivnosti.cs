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

using Microsoft.Reporting.WinForms;

namespace Saobracaj.Dokumenta
{
    public partial class frmPregledAktivnosti : Form
    {
        public frmPregledAktivnosti()
        {
            InitializeComponent();
        }

        private void btnPretrazi_Click(object sender, EventArgs e)
        {
            var select = "";
            select = " SELECT     AktivnostiStavke.ID, AktivnostiStavke.IDNadredjena, AktivnostiStavke.VrstaAktivnostiID, VrstaAktivnosti.Naziv, AktivnostiStavke.DatumPocetka, " + 
                     " AktivnostiStavke.DatumZavrsetka, AktivnostiStavke.Posao, AktivnostiStavke.OznakaPosla, AktivnostiStavke.MestoIzvrsenja, AktivnostiStavke.Teretnica, " +
                      "  AktivnostiStavke.Lokomotiva, AktivnostiStavke.Stanica, Aktivnosti.Zaposleni, Delavci.DeSifra, Delavci.DePriimek, Delavci.DeIme, Aktivnosti.VremeOd, " +
                     "  Aktivnosti.VremeDo " +
" FROM         AktivnostiStavke INNER JOIN " +
                      " VrstaAktivnosti ON AktivnostiStavke.VrstaAktivnostiID = VrstaAktivnosti.ID INNER JOIN " +
                     "  Aktivnosti ON AktivnostiStavke.IDNadredjena = Aktivnosti.ID INNER JOIN " +
                     "  Delavci ON Aktivnosti.Zaposleni = Delavci.DeSifra " +
                     "  order by DatumPocetka desc   ";


            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
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

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var select = "";
            select = " SELECT     AktivnostiStavke.ID, AktivnostiStavke.IDNadredjena, AktivnostiStavke.VrstaAktivnostiID, VrstaAktivnosti.Naziv, AktivnostiStavke.DatumPocetka, " +
                     " AktivnostiStavke.DatumZavrsetka, AktivnostiStavke.Posao, AktivnostiStavke.OznakaPosla, AktivnostiStavke.MestoIzvrsenja, AktivnostiStavke.Teretnica, " +
                      "  AktivnostiStavke.Lokomotiva, AktivnostiStavke.Stanica, Aktivnosti.Zaposleni, Delavci.DeSifra, Delavci.DePriimek, Delavci.DeIme, Aktivnosti.VremeOd, " +
                     "  Aktivnosti.VremeDo " +
" FROM         AktivnostiStavke INNER JOIN " +
                      " VrstaAktivnosti ON AktivnostiStavke.VrstaAktivnostiID = VrstaAktivnosti.ID INNER JOIN " +
                     "  Aktivnosti ON AktivnostiStavke.IDNadredjena = Aktivnosti.ID INNER JOIN " +
                     "  Delavci ON Aktivnosti.Zaposleni = Delavci.DeSifra where AktivnostiStavke.OznakaPosla = '" + cboPosao.SelectedValue + "'" +
                     "  order by DatumPocetka desc   ";


            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
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

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;
        }

        private void frmPregledAktivnosti_Load(object sender, EventArgs e)
        {
            var select3 = " select Oznaka from Najava  order by Oznaka";
            var s_connection3 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboPosao.DataSource = ds3.Tables[0];
            cboPosao.DisplayMember = "Oznaka";
            cboPosao.ValueMember = "Oznaka";
        }

        private void cboPosao_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmAutomobiliPregledPrijava")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                }
            }
            if (bFormNameOpen == false)
            {
                Dokumenta.frmAutomobiliPregledPrijava pa = new Dokumenta.frmAutomobiliPregledPrijava(Convert.ToInt32(txt_Sifra.Text));
                pa.Show();
            }

            
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txt_Sifra.Text = row.Cells[0].Value.ToString();

                    }
                }
            }
            catch
            {

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmTehnickiPregled tp = new frmTehnickiPregled(Convert.ToInt32(txt_Sifra.Text));
            tp.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmKomercijalniPregled tp = new frmKomercijalniPregled(Convert.ToInt32(txt_Sifra.Text));
            tp.Show();
        }
    }
}
