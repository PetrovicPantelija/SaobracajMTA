using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Sifarnici
{
    public partial class frmPartnerjiFirme: Form
    {
        bool status = false;
        int IzPartnera = 0;
        int pomPartner = 0;
        int SveFirme = 0;

        public frmPartnerjiFirme()
        {
            InitializeComponent();
            ChangeTextBox();
        }

        public frmPartnerjiFirme(int Partner)
        {
            InitializeComponent();
            IzPartnera = 1;
            pomPartner = Partner;
            ChangeTextBox();
            this.Text = "Firme";
        }


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

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtSifraApp.Text = "";
            txtFirma.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Sifarnici.InsertPartnerjiFirme ins = new Sifarnici.InsertPartnerjiFirme();

            if (status == true)
            {

                
              int newId = ins.InsParnerjiFirme(Convert.ToInt32(txtPartnerID.SelectedValue), Convert.ToInt32(txtSifraApp.Text), txtFirma.Text);
            }
            else
            {
                if (int.TryParse(txtID.Text, out int id))
                {
                    ins.UpdPartneriFirme(id,Convert.ToInt32(txtPartnerID.SelectedValue), Convert.ToInt32(txtSifraApp.Text), txtFirma.Text);
                }
                else
                {
                    MessageBox.Show("Niste selektovali nijedan red za promenu vrednosti!");
                }
               
            }
            RefreshDataGridPoPartneru();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Sifarnici.InsertPartnerjiFirme del = new Sifarnici.InsertPartnerjiFirme();
            del.DelPartneriFirme(Convert.ToInt32(txtID.Text));
            if(SveFirme == 0)
                RefreshDataGridPoPartneru();
            else
                RefreshDataGrid();
        }

        private void RefreshDataGridPoPartneru()
        {
            var select = " SELECT [ID],[PartnerID],[SifraApp],[Firma]   FROM [TESTIRANJE].[dbo].[PartnerjiFirma] WHERE PartnerID = " + pomPartner;

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);


            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView1);

            DataGridViewColumn column = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Partner";
            dataGridView1.Columns[1].Width = 150;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Šifra APP";
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[3].HeaderText = "Firma";

            if (dataGridView1.Columns.Contains("ID"))
            {
                dataGridView1.Columns["ID"].Visible = false;
            }

        }
        private void RefreshDataGrid()
        {
            var select = " SELECT [ID],[PartnerID],[SifraApp],[Firma]   FROM [TESTIRANJE].[dbo].[PartnerjiFirma]";

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);


            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView1);

            DataGridViewColumn column = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Partner";
            dataGridView1.Columns[1].Width = 150;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Šifra APP";
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[3].HeaderText = "Firma";

            if (dataGridView1.Columns.Contains("ID"))
            {
                dataGridView1.Columns["ID"].Visible = false;
            }
        }

        private void frmPartnerjiFirme_Load(object sender, EventArgs e)
        {
            var select = " Select Distinct PaSifra, PaNaziv  From Partnerji";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            txtPartnerID.DataSource = ds.Tables[0];
            txtPartnerID.DisplayMember = "PaNaziv";
            txtPartnerID.ValueMember = "PaSifra";

            if (IzPartnera == 1)
            {
                
                txtPartnerID.SelectedValue = pomPartner;
                RefreshDataGridPoPartneru();

            }
            else
            {
                //RefreshDataGrid();
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


                        txtID.Text = row.Cells[0].Value.ToString();
                        txtSifraApp.Text = row.Cells[2].Value.ToString();
                        txtFirma.Text = row.Cells[3].Value.ToString();
                        //txtPaKOOddelek.Text = row.Cells[4].Value.ToString();

                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {

            RefreshDataGrid();
        }
    }
}
