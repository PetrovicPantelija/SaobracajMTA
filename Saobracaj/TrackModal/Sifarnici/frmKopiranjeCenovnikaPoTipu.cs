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
using System.Net;
using System.Net.Mail;

namespace Saobracaj.TrackModal.Sifarnici
{
    public partial class frmKopiranjeCenovnikaPoTipu : Form
    {
        public frmKopiranjeCenovnikaPoTipu()
        {
            InitializeComponent();
        }

        private void frmKopiranjeCenovnikaPoTipu_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Projekat.InsertCene ins = new Projekat.InsertCene();
            ins.KopirajCeneTip(Convert.ToInt32(cboTipCenovnikaIz.SelectedValue), Convert.ToInt32(cboTipCenovnikaU.SelectedValue));
            RefreshDataGridTipCenovnika2();
           // ins.InsVrstaManipulacije(txtNaziv.Text, Convert.ToDateTime(DateTime.Now), KorisnikCene, txtJM.Text, uticeskladisno, txtJM2.Text, Convert.ToInt32(cboTipManipulacije.SelectedValue), Convert.ToInt32(cboOrgJed.SelectedValue), txtOznaka.Text, txtRelacija.Text, Convert.ToDouble(txtCena.Value));
     
        }

        private void RefreshDataGridRazred()
        {
            var select = " SELECT Cene.[ID] as ID ,[TipCenovnika].Naziv as TipCenovnika,[Partnerji].PaNaziv as Partner,Cene.[Cena],Cene.Cena2,[VrstaManipulacije].Naziv as VrstaManipulacije,Cene.[Datum],Cene.[Korisnik], VrstePostupakaUvoz.Naziv, p2.PaNaziv as Uvoznik FROM [dbo].[Cene] " +
           " inner join TipCenovnika on TipCenovnika.ID = Cene.TipCenovnika " +
           " inner join Partnerji on Partnerji.PaSifra = Cene.Komitent " +
            " inner join Partnerji p2 on p2.PaSifra = Cene.Uvoznik " +
            " inner join VrstePostupakaUvoz  on VrstePostupakaUvoz.ID = Cene.PostupakSaRobom " +
           " inner join VrstaManipulacije on VrstaManipulacije.Id = Cene.VrstaManipulacije  where Razred = '" + cboRazred.Text + "'" +
           " order by Cene.ID desc"; 
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

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
           /*
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Naziv";
            dataGridView1.Columns[1].Width = 350;
           */

           


        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshDataGridRazred();
        }

        private void RefreshDataGridTipCenovnika2()
        {
            var select = " SELECT Cene.[ID] as ID ,[TipCenovnika].Naziv as TipCenovnika,[Partnerji].PaNaziv as Partner,[Cena],Cena2,[VrstaManipulacije].Naziv as VrstaManipulacije,Cene.[Datum],Cene.[Korisnik], VrstePostupakaUvoz.Naziv, p2.PaNaziv as Uvoznik FROM[dbo].[Cene] " +
            " inner join TipCenovnika on TipCenovnika.ID = Cene.TipCenovnika " +
            " inner join Partnerji on Partnerji.PaSifra = Cene.Komitent " +
             " inner join Partnerji p2 on p2.PaSifra = Cene.Uvoznik " +
             " inner join VrstePostupakaUvoz  on VrstePostupakaUvoz.ID = Cene.PostupakSaRobom " +
            " inner join VrstaManipulacije on VrstaManipulacije.Id = Cene.VrstaManipulacije  where TipCenovnika = " + cboTipCenovnikaU.SelectedValue  + 
            " order by Cene.ID desc";
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

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            /*
             DataGridViewColumn column = dataGridView1.Columns[0];
             dataGridView1.Columns[0].HeaderText = "ID";
             dataGridView1.Columns[0].Width = 50;

             DataGridViewColumn column2 = dataGridView1.Columns[1];
             dataGridView1.Columns[1].HeaderText = "Naziv";
             dataGridView1.Columns[1].Width = 350;
            */




        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
