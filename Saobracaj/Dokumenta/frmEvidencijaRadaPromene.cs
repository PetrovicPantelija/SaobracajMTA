using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Dokumenta
{
    public partial class frmEvidencijaRadaPromene : Form
    {
        public frmEvidencijaRadaPromene()
        {
            InitializeComponent();

        }

        private void frmEvidencijaRadaPromene_Load(object sender, EventArgs e)
        {
            var select3 = " select DeSifra as ID, (RTrim(DeIme) + ' ' +Rtrim(DePriimek) ) as Opis from Delavci  order by opis";
            var s_connection3 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboZaposleni.DataSource = ds3.Tables[0];
            cboZaposleni.DisplayMember = "Opis";
            cboZaposleni.ValueMember = "ID";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sqlFormattedDate = "'" + dtpPredvidjenoPrimanje.Value.Date.ToString("yyyy-MM-dd") + "'";

            string sqlFormattedDate2 = "'" + dtpVremeDo.Value.Date.ToString("yyyy-MM-dd") + "'";
            var select = "Select Aktivnosti.ID as IDZapisa, " +
            " (RTrim(DeIme) + ' ' + RTRim(DePriimek)) as Zaposleni, " +
            " VremeOD, VremeDo, Aktivnosti.Opis, " +
            " LogAktivnosti.Korisnik as KorisnikPromene, LogAktivnosti.Opis, LogAktivnosti.Datum, LogAktivnosti.Korisnik, LogAktivnosti.Prvi, " +
            " LogAktivnosti.Sledeci, LogAktivnosti.Nova " +
            " from Aktivnosti " +
            " inner join Delavci on Delavci.DeSifra = Aktivnosti.Zaposleni " +
            " inner join LogAktivnosti on LogAktivnosti.IdAktivnosti = Aktivnosti.ID " +
            " where Zaposleni = " + cboZaposleni.SelectedValue + "and (Prvi <> 0 or Sledeci <> 0 or Nova <> 0) " +
            " and " +
            "   VremeOD  >= " + sqlFormattedDate + " and  VremeOd <= " + sqlFormattedDate2 +
            " order by Aktivnosti.ID desc";

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

        }

        private void btnPretrazi_Click(object sender, EventArgs e)
        {
            string sqlFormattedDate = "'" + dtpPredvidjenoPrimanje.Value.Date.ToString("yyyy-MM-dd") + "'";

            string sqlFormattedDate2 = "'" + dtpVremeDo.Value.Date.ToString("yyyy-MM-dd") + "'";
            var select = "Select Aktivnosti.ID as IDZapisa, " +
            " (RTrim(DeIme) + ' ' + RTRim(DePriimek)) as Zaposleni, " +
            " VremeOD, VremeDo, Aktivnosti.Opis, " +
            " LogAktivnosti.Korisnik as KorisnikPromene, LogAktivnosti.Opis, LogAktivnosti.Datum, LogAktivnosti.Korisnik, LogAktivnosti.Prvi, " +
            " LogAktivnosti.Sledeci, LogAktivnosti.Nova " +
            " from Aktivnosti " +
            " inner join Delavci on Delavci.DeSifra = Aktivnosti.Zaposleni " +
            " inner join LogAktivnosti on LogAktivnosti.IdAktivnosti = Aktivnosti.ID " +
            " where  (Prvi <> 0 or Sledeci <> 0 or Nova <> 0) " +
            " and " +
            "   VremeOD  >= " + sqlFormattedDate + " and  VremeOd <= " + sqlFormattedDate2 +
            " order by Aktivnosti.ID desc";

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
        }

        /*
         Select Aktivnosti.ID as IDZapisa,   
(RTrim(DeIme) + ' ' + RTRim(DePriimek)) as Zaposleni,  
VremeOD, VremeDo,  Aktivnosti.Opis,   
LogAktivnosti.Korisnik as KorisnikPromene, LogAktivnosti.Opis, LogAktivnosti.Datum, LogAktivnosti.Korisnik, LogAktivnosti.Prvi, 
LogAktivnosti.Sledeci, LogAktivnosti.Nova
from Aktivnosti  
inner join Delavci on Delavci.DeSifra = Aktivnosti.Zaposleni  
inner join LogAktivnosti on LogAktivnosti.IdAktivnosti = Aktivnosti.ID
where  Zaposleni = 182 and (Prvi <> 0 and Sledeci<>0 and Nova<>0)
order by Aktivnosti.ID desc
         */
    }
}
