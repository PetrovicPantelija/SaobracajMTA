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
using Syncfusion.Windows.Forms;
using System.Drawing.Imaging;

namespace Saobracaj.Izvoz
{
    public partial class frmKontaktOsobeMU : Form
    {
        bool status = false;
        int IzPartnera = 0;
        int pomPartner = 0;
        string pAdresa = "";

        private void ChangeTextBox()
        {
           

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
                this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
                panelHeader.Visible = false;
                this.ControlBox = true;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
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
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
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




        public frmKontaktOsobeMU()
        {
            InitializeComponent();
            IzPartnera = 0;
            ChangeTextBox();
        }
        public frmKontaktOsobeMU(int Partner)
        {
            InitializeComponent();
            IzPartnera = 1;
            pomPartner = Partner;
            ChangeTextBox();
        }


        public frmKontaktOsobeMU(string Adresa)
        {
            InitializeComponent();
            IzPartnera = 3; //Ako je iz adrese
            pAdresa = Adresa;
            ChangeTextBox();
        }
        
        private void frmKontaktOsobeMU_Load(object sender, EventArgs e)
        {
            var select = " Select Distinct ID, Naziv  From MestaUtovara";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            txtPaKOSifra.DataSource = ds.Tables[0];
            txtPaKOSifra.DisplayMember = "Naziv";
            txtPaKOSifra.ValueMember = "ID";

            if (IzPartnera == 1)
            {
                txtPaKOSifra.SelectedValue = pomPartner;
                RefreshDataGridPoPartneru();

            }
            else if (IzPartnera == 3)
            {
                txtPaKOOpomba.Text = pAdresa;
                RefreshDataGridPoAdresi(pAdresa);
            }
            else
            {
                RefreshDataGrid();
            }
        }

        private void RefreshDataGrid()
        {
            var select = " SELECT [PaKOZapSt]      ,[PaKOSifra]      ,[PaKOIme]      ,[PaKOPriimek]      ,[PaKOOddelek]      ,[PaKOTel] " +
 " ,[PaKOMail],[PaKOOpomba],[Operatika], MestaUtovara.Naziv as MestoUtovara  FROM[partnerjiKontOsebaMU] " +
 " inner join MestaUtovara on MestaUtovara.ID = partnerjiKontOsebaMU.PaKOSifra";

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
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Partner ID";
            dataGridView1.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Ime";
            dataGridView1.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Prezime";
            dataGridView1.Columns[3].Width = 100;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Odeljenje";
            dataGridView1.Columns[4].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Tel";
            dataGridView1.Columns[5].Width = 70;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Email";
            dataGridView1.Columns[6].Width = 70;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Adresa";
            dataGridView1.Columns[7].Width = 120;



        }

        private void RefreshDataGridPoPartneru()
        {
            var select = " SELECT [PaKOZapSt]      ,[PaKOSifra]      ,[PaKOIme]      ,[PaKOPriimek]      ,[PaKOOddelek]      ,[PaKOTel] " +
     " ,[PaKOMail]      ,[PaKOOpomba]      ,[Operatika]  , MestaUtovara.Naziv as MestoUtovara  FROM [partnerjiKontOsebaMU] inner join MestaUtovara on MestaUtovara.ID = partnerjiKontOsebaMU.PaKOSifra\r\n Where PaKOSifra = " + txtPaKOSifra.SelectedValue;

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
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Partner ID";
            dataGridView1.Columns[1].Width = 150;



        }


        private void RefreshDataGridPoAdresi(string Adresa)
        {
            var select = " SELECT [PaKOZapSt]      ,[PaKOSifra]      ,[PaKOIme]      ,[PaKOPriimek]      ,[PaKOOddelek]      ,[PaKOTel] " +
     " ,[PaKOMail]      ,[PaKOOpomba]      ,[Operatika], MestaUtovara.Naziv as MestoUtovara  FROM [partnerjiKontOsebaMU] inner join MestaUtovara on MestaUtovara.ID = partnerjiKontOsebaMU.PaKOSifra  Where PaKOOpomba = '" + pAdresa + "'";

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
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Partner ID";
            dataGridView1.Columns[1].Width = 150;



        }


        public string GetKontakt()
            {
                return txtPaKOIme.Text.TrimEnd() + " " + txtPaKOPriimek.Text.TrimEnd() + " " + txtPaKOTel.Text.TrimEnd();
            }

        public string GetKontaktAdresa(int Partner)
        {

            RefreshDataGridPoPartneru();
            return txtPaKOOpomba.Text.TrimEnd();
        }

        public string GetSviKontaktiPoAdresi(string Adresa)
        {

            //RefreshDataGridPoAdresi(Adresa);
            string osobe  = "";
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    osobe = osobe + ";" + row.Cells[2].Value.ToString().TrimEnd() + " " + row.Cells[3].Value.ToString().TrimEnd();
                }
            }
            return osobe;
        }



        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtPaKOZapSt.Enabled = false;

            txtPaKOZapSt.Text = "";
            txtPaKOSifra.Text = "";
            txtPaKOIme.Text = "";
            txtPaKOPriimek.Text = "";
            txtPaKOOddelek.Text = "";
            txtPaKOTel.Text = "";
            txtPaKOMail.Text = "";
            txtPaKOOpomba.Text = "";
            chkOperatika.Checked = false;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            int pomOperater = 0;
            if (chkOperatika.Checked == true)
            {
                pomOperater = 1;
            }

            if (status == true)
            {

                Sifarnici.InsertKontaktOsobeMU ins = new Sifarnici.InsertKontaktOsobeMU();
                ins.InsKontaktOsoba(Convert.ToInt32(txtPaKOSifra.SelectedValue), txtPaKOIme.Text, txtPaKOPriimek.Text, txtPaKOOddelek.Text, txtPaKOTel.Text, txtPaKOMail.Text, txtPaKOOpomba.Text, pomOperater);
            }
            else
            {
                Sifarnici.InsertKontaktOsobeMU upd = new Sifarnici.InsertKontaktOsobeMU();
                upd.UpdKontaktOsoba(Convert.ToInt32(txtPaKOZapSt.Text), Convert.ToInt32(txtPaKOSifra.SelectedValue), txtPaKOIme.Text, txtPaKOPriimek.Text, txtPaKOOddelek.Text, txtPaKOTel.Text, txtPaKOMail.Text, txtPaKOOpomba.Text, pomOperater);
            }
            RefreshDataGrid();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {

            Sifarnici.InsertKontaktOsobe upd = new Sifarnici.InsertKontaktOsobe();
            upd.DelKontaktOsoba(Convert.ToInt32(txtPaKOZapSt.Text));
            RefreshDataGrid();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {

                        txtPaKOZapSt.Text = row.Cells[0].Value.ToString();
                        // txtPaKOSifra.Text = row.Cells[1].Value.ToString();
                        txtPaKOIme.Text = row.Cells[2].Value.ToString();
                        txtPaKOPriimek.Text = row.Cells[3].Value.ToString();
                        txtPaKOOddelek.Text = row.Cells[4].Value.ToString();
                        txtPaKOTel.Text = row.Cells[5].Value.ToString();
                        txtPaKOMail.Text = row.Cells[6].Value.ToString();
                        txtPaKOOpomba.Text = row.Cells[7].Value.ToString();

                        if (row.Cells[7].Value.ToString() == "1")
                        { chkOperatika.Checked = true; }
                        else
                        {
                            chkOperatika.Checked = false;
                        }
                        txtPaKOSifra.SelectedValue = Convert.ToInt32(row.Cells[1].Value.ToString());
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
