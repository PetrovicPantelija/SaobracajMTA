using Syncfusion.Windows.Forms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Saobracaj.Izvoz
{
    public partial class DogovoreniPosloviIzvoza : Form
    {
        Boolean status = false;


        private void ChangeTextBox()
        {
            this.BackColor = Color.White;
            this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
            this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
            Office2010Colors.ApplyManagedColors(this, Color.White);
            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
            panelHeader.Visible = false;
            this.ControlBox = true;
            // this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                meniHeader.Visible = false;
                panelHeader.Visible = true;
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = Color.White;
                Office2010Colors.ApplyManagedColors(this, Color.White);




                foreach (Control control in this.Controls)
                {
                    if (control is System.Windows.Forms.Button buttons)
                    {

                        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                        buttons.FlatStyle = FlatStyle.Flat;
                    }

                    if (control is System.Windows.Forms.TextBox textBox)
                    {

                        textBox.BackColor = Color.White;// Example: Change background color
                        textBox.ForeColor = Color.FromArgb(51, 51, 54); //Boja slova u kvadratu
                        textBox.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                        // Example: Change font
                    }


                    if (control is System.Windows.Forms.Label label)
                    {
                        // Change properties here
                        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        label.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);  // Example: Change font

                        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                    }
                    if (control is DateTimePicker dtp)
                    {
                        dtp.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        dtp.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }
                    if (control is System.Windows.Forms.CheckBox chk)
                    {
                        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        chk.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ListBox lb)
                    {
                        lb.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        lb.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ComboBox cb)
                    {
                        cb.ForeColor = Color.FromArgb(51, 51, 54);
                        cb.BackColor = Color.White;// Example: Change background color
                        cb.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.NumericUpDown nu)
                    {
                        nu.ForeColor = Color.FromArgb(51, 51, 54);
                        nu.BackColor = Color.White;// Example: Change background color
                        nu.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                }
            }
            else
            {
                meniHeader.Visible = true;
                panelHeader.Visible = false;
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        private void PodesiDatagridView(DataGridView dgv)
        {
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(90, 199, 249); // Selektovana boja
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.BackgroundColor = Color.White;

            dgv.DefaultCellStyle.Font = new Font("Helvetica", 12F, GraphicsUnit.Pixel);
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(51, 51, 54);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);
            dgv.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);


            //Header
            dgv.EnableHeadersVisualStyles = false;
            //   header.Style.Font = new Font("Arial", 12F, FontStyle.Bold);
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 54);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv.ColumnHeadersHeight = 30;
        }


        public DogovoreniPosloviIzvoza()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            ChangeTextBox();
        }

        private void DogovoreniPosloviIzvoza_Load(object sender, EventArgs e)
        {
            var select9 = " Select Distinct PaSifra, PaNaziv From PArtnerji order by PaNaziv";
            var s_connection9 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection9 = new SqlConnection(s_connection9);
            var c9 = new SqlConnection(s_connection9);
            var dataAdapter9 = new SqlDataAdapter(select9, c9);

            var commandBuilder9 = new SqlCommandBuilder(dataAdapter9);
            var ds9 = new DataSet();
            dataAdapter9.Fill(ds9);
            cboPartner.DataSource = ds9.Tables[0];
            cboPartner.DisplayMember = "PaNaziv";
            cboPartner.ValueMember = "PaSifra";

            RefreshDataGrid();
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtID.Enabled = false;
            txtID.Text = "";
            txtNapomena.Text = "";
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            int tmpZatvoren = 0;

            if (txtID.Text == "")
            {
                status = true;
            }

            if (chkZavrsen.Checked == true)
            {
                tmpZatvoren = 1;
            }
            if (status == true)
            {
                InsertDogovorenoIzvoz ins = new InsertDogovorenoIzvoz();
                ins.InsDogovorenoIzvoz(Convert.ToInt32(cboPartner.SelectedValue), Convert.ToDateTime(dtpPeriodOd.Value), Convert.ToDateTime(dtpPeriodDo.Value), Convert.ToInt32(txtBrojKontejnera.Value), Convert.ToInt32(txtBrojIsporucenih.Value), txtNapomena.Text, tmpZatvoren);
                status = false;
            }
            else
            {
                InsertDogovorenoIzvoz ins = new InsertDogovorenoIzvoz();
                ins.UpdDogovorenoIzvoz(Convert.ToInt32(txtID.Text), Convert.ToInt32(cboPartner.SelectedValue), Convert.ToDateTime(dtpPeriodOd.Value), Convert.ToDateTime(dtpPeriodDo.Value), Convert.ToInt32(txtBrojKontejnera.Value), Convert.ToInt32(txtBrojIsporucenih.Value), txtNapomena.Text, tmpZatvoren);
                status = false;
            }
            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            var select = " SELECT [ID], Partnerji.PaNaziv as Partner " +
     " ,[PeriodOd]      ,[PeriodDo] " +
     "  ,[BrojUgovorenih]      ,[BrojUradjenih] " +
     "  ,[Napomena]      ,[Zatvoren] " +
       "      FROM [dbo].[DogovorenoIzvoz] " +
"   inner join PArtnerji on Partnerji.PaSifra = [Partner]";



            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);


            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView2);

            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "Šifra";
            dataGridView2.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "Partner";
            dataGridView2.Columns[1].Width = 150;



        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertDogovorenoIzvoz del = new InsertDogovorenoIzvoz();
            del.DelDogovorenoIzvoz(Convert.ToInt32(txtID.Text));
            RefreshDataGrid();
        }

        private void VratiPodatke(int ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT [ID]      ,[Partner] " +
    " ,[PeriodOd]      ,[PeriodDo]      ,[BrojUgovorenih] " +
     "  ,[BrojUradjenih]      ,[Napomena]      ,[Zatvoren] " +
 "  FROM[dbo].[DogovorenoIzvoz] " +
             " where ID= " + ID, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtID.Text = dr["ID"].ToString();

                cboPartner.SelectedValue = Convert.ToInt32(dr["Partner"].ToString());

                dtpPeriodDo.Value = Convert.ToDateTime(dr["PeriodDo"].ToString());
                dtpPeriodOd.Value = Convert.ToDateTime(dr["PeriodOd"].ToString());
                txtBrojKontejnera.Value = Convert.ToInt32(dr["BrojUgovorenih"].ToString());
                txtBrojIsporucenih.Value = Convert.ToInt32(dr["BrojUradjenih"].ToString());
                txtNapomena.Text = dr["Napomena"].ToString();
                if (dr["Zatvoren"].ToString() == "1")
                {
                    chkZavrsen.Checked = true;
                }
                else
                {
                    chkZavrsen.Checked = false;

                }
            }

            con.Close();
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {
                        txtID.Text = row.Cells[0].Value.ToString();

                        VratiPodatke(Convert.ToInt32(txtID.Text));

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
