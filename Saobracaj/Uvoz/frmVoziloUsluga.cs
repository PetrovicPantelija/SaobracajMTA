using Syncfusion.Windows.Forms;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Uvoz
{
    public partial class frmVoziloUsluga : Form
    {
        bool status = false;
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

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
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

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
        public frmVoziloUsluga()
        {
            InitializeComponent();
            ChangeTextBox();
        }

        public frmVoziloUsluga(string ID)
        {
            InitializeComponent();
            ChangeTextBox();

            txtID.Text = ID.ToString();
            VratiOstalePodatke();
            
        }

        public frmVoziloUsluga(int ID, int Modul)
        {
            InitializeComponent();
            ChangeTextBox();

            // txtID.Text = ID.ToString();
            if (Modul == 0)
            {
                chkUvoz.Checked = true;
                txtUsluge.Text = ID.ToString();
                VratiOstalePodatkeIzUsluge(ID, Modul);
            }

        }

        private void VratiOstalePodatkeIzUsluge(int ID, int Modul)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT [ID]      ,[RegBr]      ,[Datum]      ,[Vozac] " +
     " ,[BrojTelefona]      ,[Napomena]      ,[Modul]      ,[IDUsluge] " +
 " FROM [dbo].[VoziloUsluga]  " +
            "  where IDUsluge=" + ID + " and Modul = " + Modul, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtID.Text = dr["ID"].ToString();
                txtVozilo.Text = dr["RegBr"].ToString();
                dtpDatum.Value = Convert.ToDateTime(dr["Datum"].ToString());
                txtNapomena.Text = dr["Napomena"].ToString();
                txtVozac.Text = dr["Vozac"].ToString();
                txtBrojTelefona.Text = dr["BrojTelefona"].ToString();

                if (dr["ID"].ToString() == "0")
                {
                    chkUvoz.Checked = true;
                }
                txtUsluge.Text = dr["IDUsluge"].ToString();
            }
            con.Close();

        }

        private void VratiOstalePodatke()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT [ID]      ,[RegBr]      ,[Datum]      ,[Vozac] " +
     " ,[BrojTelefona]      ,[Napomena]      ,[Modul]      ,[IDUsluge] " +
 " FROM [dbo].[VoziloUsluga]  " +
            "  where ID=" + txtID.Text, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtID.Text = dr["ID"].ToString();
                txtVozilo.Text = dr["RegBr"].ToString();
                dtpDatum.Value = Convert.ToDateTime(dr["Datum"].ToString());
                txtNapomena.Text = dr["Napomena"].ToString();
                txtVozac.Text = dr["Vozac"].ToString();
                txtBrojTelefona.Text = dr["BrojTelefona"].ToString();

                if (dr["ID"].ToString() == "0")
                {
                    chkUvoz.Checked = true;
                }
                txtUsluge.Text = dr["IDUsluge"].ToString();
            }
            con.Close();

        }



        private void frmVoziloUsluga_Load(object sender, EventArgs e)
        {

        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            tsNew.Enabled = false;
            status = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            int Modultmp = 0;
            if (chkUvoz.Checked == true)
                Modultmp = 0;
            else
            {
                Modultmp = 1;
            }
            InsertVoziloUsluga ins = new InsertVoziloUsluga();
            if (status == true)
            {
                ins.InsVoziloUsluga(txtVozilo.Text, Convert.ToDateTime(dtpDatum.Value), txtVozac.Text, txtBrojTelefona.Text, txtNapomena.Text, Modultmp, Convert.ToInt32(txtUsluge.Text));
            }
            else
            {
                ins.UpdVoziloUsliga(Convert.ToInt32(txtID.Text.ToString()), txtVozilo.Text, Convert.ToDateTime(dtpDatum.Value), txtVozac.Text, txtBrojTelefona.Text, txtNapomena.Text, Modultmp, Convert.ToInt32(txtUsluge.Text));
            }
            //  FillGV();
            tsNew.Enabled = true;
            status = false;
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertVoziloUsluga del = new InsertVoziloUsluga();
            del.DelVoziloUsluga(Convert.ToInt32(txtID.Text));
            txtID.Text = "";
            txtVozilo.Text = "";
            txtVozac.Text = "";
            txtBrojTelefona.Text = "";
            txtNapomena.Text = "";
            txtUsluge.Text = "";
        }
    }
}
