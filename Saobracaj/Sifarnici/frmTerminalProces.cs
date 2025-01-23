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
using System.Globalization;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Windows.Forms;
using Saobracaj.Tehnologija;

namespace Saobracaj.Sifarnici
{
    public partial class frmTerminalProces : Syncfusion.Windows.Forms.Office2010Form
    {
        bool status = false;
        string Scenario = "";

        private void ChangeTextBox()
        {

            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
            panelHeader.Visible = false;


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
                panelHeader.Visible = true;
                meniHeader.Visible = false;
                this.BackColor = Color.White;
                this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
                this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
                this.ControlBox = true;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                Office2010Colors.ApplyManagedColors(this, Color.White);
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = Color.White;


                foreach (Control control in this.Controls)
                {
                    if (control is System.Windows.Forms.Button buttons)
                    {

                        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                        buttons.FlatStyle = FlatStyle.Flat;
                    }
                }


                foreach (Control control in this.Controls)
                {

                    if (control is System.Windows.Forms.TextBox textBox)
                    {

                        textBox.BackColor = Color.White;// Example: Change background color
                        textBox.ForeColor = Color.FromArgb(51, 51, 54); //Boja slova u kvadratu
                        textBox.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                        // Example: Change font
                    }


                    if (control is System.Windows.Forms.Label label)
                    {
                        // Change properties here
                        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        label.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);  // Example: Change font

                        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                    }

                    if (control is DateTimePicker dtp)
                    {
                        dtp.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        dtp.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.CheckBox chk)
                    {
                        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        chk.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ListBox lb)
                    {
                        lb.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        lb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ComboBox cb)
                    {
                        cb.ForeColor = Color.FromArgb(51, 51, 54);
                        cb.BackColor = Color.White;// Example: Change background color
                        cb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.NumericUpDown nu)
                    {
                        nu.ForeColor = Color.FromArgb(51, 51, 54);
                        nu.BackColor = Color.White;// Example: Change background color
                        nu.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }
                }
            }
            else
            {
                panelHeader.Visible = false;
                meniHeader.Visible = true;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
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


        public frmTerminalProces()
        {
            InitializeComponent();
            ChangeTextBox();
        }

        public frmTerminalProces(string SifraScenaria)
        {
            InitializeComponent();
            ChangeTextBox();
            Scenario = SifraScenaria;
        }


        private void tsSave_Click(object sender, EventArgs e)
        {
            if (status == true)
            {
                InserTerminalProces ins = new InserTerminalProces();
                //0-Ako je novi scenario
                //1-Ako je dodavanje stavke
                ins.InsTerminalProces(Convert.ToInt32(cboScenario.SelectedValue),  Convert.ToInt32(cboTerminalOperacije.SelectedValue) );
                status = false;
            }
            else
            {
                InserTerminalProces upd = new InserTerminalProces();
                upd.UpdTerminalProces(Convert.ToInt32(txtID.Text), Convert.ToInt32(txtRB.Text), Convert.ToInt32(cboScenario.SelectedValue), Convert.ToInt32(cboTerminalOperacije.SelectedValue));

            }
            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            var select = "";
            select = " SELECT TerminalProces.[ID] ,[RB] ,[ScenarioID] ,[TerminalOperacijaID], TerminalOperacije.Naziv  FROM [dbo].[TerminalProces] " +
                "    inner join TerminalOperacije on TerminalOperacije.ID = TerminalOperacijaID ";

            //  "  where  Aktivnosti.Masinovodja = 1 and Zaposleni = " + Convert.ToInt32(cboZaposleni.SelectedValue) + " order by Aktivnosti.ID desc";


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

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "RB";
            dataGridView1.Columns[1].Width = 30;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Scenario";
            dataGridView1.Columns[2].Width = 80;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Terminalska operacija ID";
            dataGridView1.Columns[3].Width = 30;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Terminalska operacija ";
            dataGridView1.Columns[4].Width = 130;


        }


        private void RefreshDataGridPoScenariju()
        {
            var select = "";
            select = " SELECT TerminalProces.[ID] ,TerminalProces.[RB] ,[ScenarioID] ,[TerminalOperacijaID], TerminalOperacije.Naziv  FROM [dbo].[TerminalProces]" +
                " inner join TerminalOperacije on TerminalOperacije.ID = TerminalOperacijaID where [ScenarioID]  = " + Scenario;

            //  "  where  Aktivnosti.Masinovodja = 1 and Zaposleni = " + Convert.ToInt32(cboZaposleni.SelectedValue) + " order by Aktivnosti.ID desc";


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

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "RB";
            dataGridView1.Columns[1].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Scenario";
            dataGridView1.Columns[2].Width = 40;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Terminalska operacija ID";
            dataGridView1.Columns[3].Width = 30;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Terminalska operacija ";
            dataGridView1.Columns[4].Width = 130;


        }


        private void frmTerminalProces_Load(object sender, EventArgs e)
        {

            var select2 = " SELECT [ID]      ,[Naziv]  FROM [dbo].[TerminalOperacije]";
            var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboTerminalOperacije.DataSource = ds2.Tables[0];
            cboTerminalOperacije.DisplayMember = "Naziv";
            cboTerminalOperacije.ValueMember = "ID";


            var select3 = " Select Distinct ID From Scenario";
            var s_connection3 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);


            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboScenario.DataSource = ds3.Tables[0];
            cboScenario.DisplayMember = "ID";
            cboScenario.ValueMember = "ID";

            cboScenario.SelectedValue = Scenario;
            RefreshDataGridPoScenariju();



        }

        private void VratiPodatkeSelect(string ID, string RB)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT [ID]      ,[RB]      ,[ScenarioID]      ,[TerminalOperacijaID]  FROM [dbo].[TerminalProces] where ID=" + ID + " AND RB =" + RB, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtRB.Text = dr["RB"].ToString();
                cboScenario.SelectedValue = Convert.ToInt32(dr["ScenarioID"].ToString());

                cboTerminalOperacije.SelectedValue = dr["TerminalOperacijaID"].ToString();
            }

            con.Close();
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
                        txtRB.Text = row.Cells[1].Value.ToString();
                        VratiPodatkeSelect(txtID.Text, txtRB.Text);
                        // txtOznaka.Enabled = false;
                        // txtOpis.Text = row.Cells[1].Value.ToString();
                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InserTerminalProces del = new InserTerminalProces();
            //0-Ako je novi scenario
            //1-Je broj tekuceg scaniraj
            del.DelTerminalProces(Convert.ToInt32(txtID.Text));
            RefreshDataGrid();
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtID.Text = "";
            txtRB.Text = "";
        }

        private void btnRacun_Click(object sender, EventArgs e)
        {
            RefreshDataGridPoScenariju();

        }
    }
}
