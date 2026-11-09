using Syncfusion.Windows.Forms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
namespace Saobracaj.Sifarnici
{
    public partial class frmLokomotive : Form
    {
        public static string code = "frmLokomotive";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Sifarnici.frmLogovanje.user.ToString();
        string niz = "";
        bool status = false;

        public frmLokomotive()
        {
            InitializeComponent();
            ChangeTextBox();

        }

        private void ChangeTextBox()
        {
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
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
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

        private void frmLokomotive_Load(object sender, EventArgs e)
        {

            var select2 = " Select Distinct ID, (Rtrim(Oznaka) + ' - ' + RTrim(Opis)) as Naziv From LokomotivaSerija";
            var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboSerija.DataSource = ds2.Tables[0];
            cboSerija.DisplayMember = "Naziv";
            cboSerija.ValueMember = "ID";

            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            var select = "";
            if (pomAktivna % 2 == 0)
            {
                select = "select  SmSifra, SmNaziv,  Password, StatusLokomotive, Dizel, SmPogDela as Masa , Serija from Mesta where Lokomotiva = 1 and StatusLokomotive=1 ";

                //  "  where  Aktivnosti.Masinovodja = 1 and Zaposleni = " + Convert.ToInt32(cboZaposleni.SelectedValue) + " order by Aktivnosti.ID desc";
            }
            else
            {
                select = "select  SmSifra, SmNaziv,  Password, StatusLokomotive, Dizel, SmPogDela as Masa , Serija from Mesta where Lokomotiva = 1 and StatusLokomotive=0 ";
            }

            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
            dataGridView1.Columns[0].Width = 80;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Lok naz";
            dataGridView1.Columns[1].Width = 100;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Lozinka";
            dataGridView1.Columns[2].Width = 150;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Aktivna";
            dataGridView1.Columns[3].Width = 60;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Dizel";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column5 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Masa";
            dataGridView1.Columns[5].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Serija ID";
            dataGridView1.Columns[6].Width = 80;


        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {


                        txtLokomotiva.Text = row.Cells[0].Value.ToString();
                        txtNaziv.Text = row.Cells[1].Value.ToString();
                        txtPassword.Text = row.Cells[2].Value.ToString();
                        if (row.Cells[3].Value.ToString() == "1")
                        {
                            chkAktivna.Checked = true;
                        }
                        else
                        {
                            chkAktivna.Checked = false;
                        }
                        if (row.Cells[4].Value.ToString() == "1")
                        {
                            chkDizel.Checked = true;
                        }
                        else
                        {
                            chkDizel.Checked = false;
                        }
                        txtMasa.Value = Convert.ToDecimal(row.Cells[5].Value.ToString());
                        cboSerija.SelectedValue = Convert.ToInt32(row.Cells[6].Value.ToString());

                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void btnRacun_Click(object sender, EventArgs e)
        {

            int Aktivna = 0;
            int Dizel = 0;
            if (chkAktivna.Checked == true)
            {
                Aktivna = 1;
            }

            if (chkDizel.Checked == true)
            {
                Dizel = 1;
            }

            InsertLokomotive ins = new InsertLokomotive();
            ins.InsLokomotiva(txtLokomotiva.Text, txtNaziv.Text, txtPassword.Text, Convert.ToDouble(txtMasa.Value), Dizel, Aktivna, Convert.ToInt32(cboSerija.SelectedValue));
            RefreshDataGrid();
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            txtLokomotiva.Text = "";
            //  txtLokomotiva.Enabled = false;
            txtPassword.Text = "";
            txtNaziv.Text = "";
            txtMasa.Value = 0;
            chkAktivna.Checked = false;

            status = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {

            int Aktivna = 0;
            int Dizel = 0;
            if (chkAktivna.Checked == true)
            {
                Aktivna = 1;
            }

            if (chkDizel.Checked == true)
            {
                Dizel = 1;
            }

            if (status == true)
            {
                InsertLokomotive ins = new InsertLokomotive();
                ins.InsLokomotiva(txtLokomotiva.Text, txtNaziv.Text, txtPassword.Text, Convert.ToDouble(txtMasa.Value), Dizel, Aktivna, Convert.ToInt32(cboSerija.SelectedValue));
                RefreshDataGrid();
                status = false;
            }
            else
            {

                InsertLokomotive upd = new InsertLokomotive();
                upd.UpdLokomotive(txtLokomotiva.Text, txtNaziv.Text, txtPassword.Text, Convert.ToDouble(txtMasa.Value), Dizel, Aktivna, Convert.ToInt32(cboSerija.SelectedValue));
                status = false;
                txtLokomotiva.Enabled = false;
                RefreshDataGrid();

            }
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertLokomotive upd = new InsertLokomotive();
            upd.DeleteLokomotive(txtLokomotiva.Text);
            status = false;
            txtLokomotiva.Enabled = false;
            RefreshDataGrid();
        }
        int pomAktivna = 0;
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            pomAktivna++;
            RefreshDataGrid();
        }
    }
}
