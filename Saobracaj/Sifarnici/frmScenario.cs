using Syncfusion.Windows.Forms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Sifarnici
{
    public partial class frmScenario : Form
    {
        bool status = false;
        int vizuelniPregled = 0;
        int cir = 0;

        private void ChangeTextBox()
        {


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
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

            dgv.DefaultCellStyle.Font = new System.Drawing.Font("Helvetica", 12F, GraphicsUnit.Pixel);
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

        public frmScenario()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            InitializeComponent();
            ChangeTextBox();
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (cbCir.Checked == true)
            {
                cir = 1;
            }
            if (cbVizuelni.Checked == true)
            {
                vizuelniPregled = 1;
            }
            if (status == true)
            {
                InsertScenario ins = new InsertScenario();
                //0-Ako je novi scenario
                //1-Ako je dodavanje stavke
                ins.InsScenario(0, txtNaziv.Text, Convert.ToInt32(cboUsluga.SelectedValue), cboPokret.Text, Convert.ToInt32(cboStatus.SelectedValue), cboForma.SelectedText,Convert.ToInt32(cboOJ.Text.Substring(0,1)),vizuelniPregled,cir);
                status = false;
            }
            else
            {
                InsertScenario upd = new InsertScenario();
                upd.UpdScenario(Convert.ToInt32(txtSifra.Text), Convert.ToInt32(txtRB.Text), txtNaziv.Text, Convert.ToInt32(cboUsluga.SelectedValue), cboPokret.Text, Convert.ToInt32(cboStatus.SelectedValue), cboForma.Text,Convert.ToInt32(cboOJ.Text.Substring(0,1)),vizuelniPregled,cir);

            }
            RefreshDataGrid();
            vizuelniPregled = 0;cir = 0;
        }
        private void RefreshDataGrid()
        {
            var select = "";
            select = @" SELECT 
    Scenario.ID,
    Scenario.RB,
    Scenario.Naziv,
    Scenario.Usluga,
    VrstaManipulacije.Naziv AS UslugaN,
    Scenario.Pokret,
    Scenario.StatusKontejnera,
    KontejnerStatus.Naziv AS StatusN,
    Scenario.Forma,
    CASE 
        WHEN Scenario.OJIzdavanje = 1 THEN '1-Uvoz'
        WHEN Scenario.OJIzdavanje = 2 THEN '2-Izvoz'
        WHEN Scenario.OJIzdavanje = 4 THEN '4-Terminal'
        ELSE ''
    END AS OJIzdavanje,VizuelniPregled,CIR
FROM 
    Scenario
INNER JOIN 
    VrstaManipulacije ON VrstaManipulacije.ID = Scenario.Usluga
INNER JOIN 
    KontejnerStatus ON KontejnerStatus.ID = Scenario.StatusKontejnera
ORDER BY 
    Scenario.ID,
    Scenario.RB";

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
            dataGridView1.Columns[0].HeaderText = "Šifra";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "RB";
            dataGridView1.Columns[1].Width = 30;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Naziv";
            dataGridView1.Columns[2].Width = 330;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "USL";
            dataGridView1.Columns[3].Width = 30;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Usluga";
            dataGridView1.Columns[4].Width = 330;

            DataGridViewColumn column5 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Pokret";
            dataGridView1.Columns[5].Width = 130;

            DataGridViewColumn column6 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "StID";
            dataGridView1.Columns[6].Width = 40;

            DataGridViewColumn column7 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Status";
            dataGridView1.Columns[7].Width = 140;

        }


        private void RefreshDataGridByID()
        {
            var select = "";
            select = @" SELECT 
    Scenario.ID,
    Scenario.RB,
    Scenario.Naziv,
    Scenario.Usluga,
    VrstaManipulacije.Naziv AS UslugaN,
    Scenario.Pokret,
    Scenario.StatusKontejnera,
    KontejnerStatus.Naziv AS StatusN,
    Scenario.Forma,
    CASE 
        WHEN Scenario.OJIzdavanje = 1 THEN '1-Uvoz'
        WHEN Scenario.OJIzdavanje = 2 THEN '2-Izvoz'
        WHEN Scenario.OJIzdavanje = 4 THEN '4-Terminal'
        ELSE ''
    END AS OJIzdavanje,VizuelniPregled,CIR
FROM 
    Scenario
INNER JOIN 
    VrstaManipulacije ON VrstaManipulacije.ID = Scenario.Usluga
INNER JOIN 
    KontejnerStatus ON KontejnerStatus.ID = Scenario.StatusKontejnera
where Scenario.ID = " + txtSifra.Text + " ORDER BY   Scenario.ID,  Scenario.RB";

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
            dataGridView1.Columns[0].HeaderText = "Šifra";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "RB";
            dataGridView1.Columns[1].Width = 30;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Naziv";
            dataGridView1.Columns[2].Width = 130;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "USL";
            dataGridView1.Columns[3].Width = 30;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Usluga";
            dataGridView1.Columns[4].Width = 230;

            DataGridViewColumn column5 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Pokret";
            dataGridView1.Columns[5].Width = 130;

            DataGridViewColumn column6 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "StID";
            dataGridView1.Columns[6].Width = 40;

            DataGridViewColumn column7 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Status";
            dataGridView1.Columns[7].Width = 140;

        }

        private void frmScenario_Load(object sender, EventArgs e)
        {
            var select2 = " Select Distinct ID, Naziv From VrstaManipulacije";
            var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboUsluga.DataSource = ds2.Tables[0];
            cboUsluga.DisplayMember = "Naziv";
            cboUsluga.ValueMember = "ID";


            var select3 = " Select Distinct ID, Naziv From KontejnerStatus";
            var s_connection3 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);


            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboStatus.DataSource = ds3.Tables[0];
            cboStatus.DisplayMember = "Naziv";
            cboStatus.ValueMember = "ID";

            RefreshDataGrid();

            cboOJ.Items.Add("1-Uvoz");
            cboOJ.Items.Add("2-Izvoz");
            cboOJ.Items.Add("4-Terminal");


        }

        private void btnRacun_Click(object sender, EventArgs e)
        {
            InsertScenario ins = new InsertScenario();
            //0-Ako je novi scenario
            //1-Je broj tekuceg scaniraj
            if (cbCir.Checked == true)
            {
                cir = 1;
            }
            if (cbVizuelni.Checked == true)
            {
                vizuelniPregled = 1;
            }
            ins.InsScenario(Convert.ToInt32(txtSifra.Text), txtNaziv.Text, Convert.ToInt32(cboUsluga.SelectedValue), cboPokret.Text, Convert.ToInt32(cboStatus.SelectedValue), cboForma.Text,Convert.ToInt32(cboOJ.Text.Substring(0,1)),vizuelniPregled,cir);
            RefreshDataGrid();
        }
        private void VratiPodatkeSelect(string ID, string RB)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("Select Scenario.ID,Scenario.RB, Scenario.Naziv, Scenario.Usluga, Scenario.Pokret, Scenario.StatusKontejnera, Forma,VizuelniPregled,CIR from Scenario where ID=" + ID + " AND RB =" + RB, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtNaziv.Text = dr["Naziv"].ToString();
                cboUsluga.SelectedValue = Convert.ToInt32(dr["Usluga"].ToString());
                cboPokret.Text = dr["Pokret"].ToString();
                cboStatus.SelectedValue = dr["StatusKontejnera"].ToString();
                cboForma.Text = dr["Forma"].ToString();
                int Vizuelni = Convert.ToInt32(dr["VizuelniPregled"].ToString());
                int Cir = Convert.ToInt32(dr["CIR"].ToString());

                if (Vizuelni == 1)
                {
                    cbVizuelni.Checked = true;
                }
                else { cbVizuelni.Checked = false; }
                if(Cir == 1)
                {
                    cbCir.Checked = true;
                }
                else { cbCir.Checked = false; }
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
                        txtSifra.Text = row.Cells[0].Value.ToString();
                        txtRB.Text = row.Cells[1].Value.ToString();
                        cboOJ.Text = row.Cells["OJIzdavanje"].Value.ToString();
                        VratiPodatkeSelect(txtSifra.Text, txtRB.Text);
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

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmTerminalProces tp = new frmTerminalProces(txtSifra.Text);
            tp.Show();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            frmTerminalProces tp = new frmTerminalProces(txtSifra.Text);
            tp.Show();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            InsertScenario ins = new InsertScenario();
            //0-Ako je novi scenario
            //1-Je broj tekuceg scaniraj
            if (cbCir.Checked == true)
            {
                cir = 1;
            }
            if (cbVizuelni.Checked == true)
            {
                vizuelniPregled = 1;
            }
            ins.InsScenario(Convert.ToInt32(txtSifra.Text), txtNaziv.Text, Convert.ToInt32(cboUsluga.SelectedValue), cboPokret.Text, Convert.ToInt32(cboStatus.SelectedValue), cboForma.Text, Convert.ToInt32(cboOJ.Text.Substring(0, 1)), vizuelniPregled, cir);
            RefreshDataGrid();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            RefreshDataGridByID();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }
    }
}
