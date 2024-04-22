using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Sifarnici
{
    public partial class frmSifarnikKontrolnihGresaka : Syncfusion.Windows.Forms.Office2010Form
    {
        bool status = false;
        public frmSifarnikKontrolnihGresaka()
        {
            InitializeComponent();

        }
        string niz = "";
        public static string code = "frmSifarnikKontrolnihGresaka";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Sifarnici.frmLogovanje.user.ToString();

        private void RefreshDataGrid()
        {
            var select = "  select KontrolneGreske.ID, KontrolneGreske.Naziv, KontrolneGreskeTipDokumenta.Naziv as TipDokumenta from KontrolneGreske" +
                " inner join KontrolneGreskeTipDokumenta on KontrolneGreskeTipDokumenta.ID = KontrolneGreske.TipDokumenta ";


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

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Naziv";
            dataGridView1.Columns[1].Width = 170;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Tip dokumenta";
            dataGridView1.Columns[1].Width = 170;
        }

        private void frmSifarnikKontrolnihGresaka_Load(object sender, EventArgs e)
        {
            var select3 = " select ID, Naziv from KontrolneGreskeTipDokumenta order by Naziv";
            var s_connection3 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboTipDokumenta.DataSource = ds3.Tables[0];
            cboTipDokumenta.DisplayMember = "Naziv";
            cboTipDokumenta.ValueMember = "ID";


            RefreshDataGrid();
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            txtSifra.Text = "";
            txtSifra.Enabled = false;
            txtNaziv.Text = "";
            status = true;
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertKontrolneGreske del = new InsertKontrolneGreske();
            del.DeleteKontrolneGreske(Convert.ToInt32(txtSifra.Text));
            status = false;
            txtSifra.Enabled = false;
            RefreshDataGrid();
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (status == true)
            {
                InsertKontrolneGreske ins = new InsertKontrolneGreske();
                ins.InsKontrolneGreske(txtNaziv.Text, Convert.ToInt32(cboTipDokumenta.SelectedValue));
                RefreshDataGrid();
                status = false;
            }
            else
            {
                InsertKontrolneGreske upd = new InsertKontrolneGreske();
                upd.UpdKontrolneGreske(Convert.ToInt32(txtSifra.Text), txtNaziv.Text, Convert.ToInt32(cboTipDokumenta.SelectedValue));
                status = false;
                txtSifra.Enabled = false;
                RefreshDataGrid();
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
                        txtSifra.Text = row.Cells[0].Value.ToString();
                        txtNaziv.Text = row.Cells[1].Value.ToString();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela promena stavki");
            }
        }
    }
}
