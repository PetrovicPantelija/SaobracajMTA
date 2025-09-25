using Syncfusion.Windows.Forms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Kapija
{
    public partial class frmKamionDetalji: Form
    {

        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        bool status = false;
        int? oldStatusID = null;
        int id = 0;

        public frmKamionDetalji()
        {
            InitializeComponent();
            ChangeTextBox();
            FillCombo();
            ResetPolja();
        }
        public frmKamionDetalji(int ID)
        {
            this.id = ID;
            InitializeComponent();
            FillCombo();
            VratiPodatke();
            ChangeTextBox();
        }

        private void ChangeTextBox()
        {

            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
            panelHeader.Visible = false;


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
                panelHeader.Visible = true;

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

        private void FillCombo()
        {

            SqlConnection conn = new SqlConnection(connection);
            
            var stv = "select ID, Ltrim(Rtrim(Naziv)) AS Naziv from StatusKapija order by Naziv";
            var stvAD = new SqlDataAdapter(stv, conn);
            var stvDS = new DataSet();
            stvAD.Fill(stvDS);

            System.Data.DataTable dt = stvDS.Tables[0];
            DataRow prazanRed = dt.NewRow();
            prazanRed["ID"] = DBNull.Value;
            prazanRed["Naziv"] = "";
            dt.Rows.InsertAt(prazanRed, 0);

            cboStatus.DataSource = dt;
            cboStatus.DisplayMember = "Naziv";
            cboStatus.ValueMember = "ID";
        }
        private void VratiPodatke()
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(
             " SELECT  k.ID, " +
                     " k.DatumDolaska, " +
                     " k.StatusID, "+
                     " st.Naziv, " +
                     " k.Vozac, " +
                     " k.RegistarskiBroj, " +
                     " k.Kontakt, " +
                     " k.RazlogDolaska, " +
                     " k.DatumZakazanogDolaska, " +
                     " k.KontaktUnutarFirme, " +
                     " k.DatumOdlaska " +
             " FROM Kapija k " +
             " LEFT JOIN StatusKapija st ON k.StatusID = st.ID" +
             " WHERE k.ID= " + id, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                txtID.Text = dr["ID"].ToString();

                if (dr["StatusID"] != DBNull.Value)
                {
                    if (int.TryParse(dr["StatusID"].ToString(), out int statusID))
                    {
                        cboStatus.SelectedValue = statusID;
                        oldStatusID = statusID;
                    }
                    else
                    {
                        cboStatus.SelectedIndex = 0;
                        oldStatusID = null;
                    }
                }
                else
                {
                    cboStatus.SelectedIndex = 0;
                    oldStatusID = null;
                }
                txtVozac.Text = dr["Vozac"].ToString();
                txtRegistarskiBroj.Text = dr["RegistarskiBroj"].ToString();
                txtKontakt.Text = dr["Kontakt"].ToString();
                txtRazlogDolaska.Text = dr["RazlogDolaska"].ToString();
                txtKontaktUFirmi.Text = dr["KontaktUnutarFirme"].ToString();

                if (dr["DatumDolaska"] != DBNull.Value)
                    dtpDatum.Value = Convert.ToDateTime(dr["DatumDolaska"].ToString());
                if (dr["DatumZakazanogDolaska"] != DBNull.Value)
                    dtpZakazaniDatumDolaska.Value = Convert.ToDateTime(dr["DatumZakazanogDolaska"].ToString());
                if (dr["DatumOdlaska"] != DBNull.Value)
                    dtpDatumOdlaska.Value = Convert.ToDateTime(dr["DatumOdlaska"].ToString());

             
            }
            con.Close();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtID.Enabled = false;
            status = true;
            ResetPolja();
        }

        private void ResetPolja()
        {
            cboStatus.SelectedIndex = 0;
            txtVozac.Text = "";
            txtRegistarskiBroj.Text = "";
            txtKontakt.Text = "";
            txtRazlogDolaska.Text = "";
            txtKontaktUFirmi.Text = "";
            dtpDatum.Value = DateTime.Now;
            dtpZakazaniDatumDolaska.Value = DateTime.Now;
            dtpDatumOdlaska.Value = DateTime.Now;
        }

        private void button21_Click(object sender, EventArgs e)
        {

            int iD = 0;
            if (txtID.Text != null && int.TryParse(txtID.Text, out int parsedID))
            {
                iD = parsedID;
            }
            int? status1 = (cboStatus.SelectedValue == null || cboStatus.SelectedValue == DBNull.Value) ? (int?)null  : Convert.ToInt32(cboStatus.SelectedValue);
            string vozac = string.IsNullOrWhiteSpace(txtVozac.Text) ? null : txtVozac.Text.Trim();
            string registarskiBroj = string.IsNullOrWhiteSpace(txtRegistarskiBroj.Text) ? null : txtRegistarskiBroj.Text.Trim();
            string kontakt = string.IsNullOrWhiteSpace(txtKontakt.Text) ? null : txtKontakt.Text.Trim();
            string razlogDolaska = string.IsNullOrWhiteSpace(txtRazlogDolaska.Text) ? null : txtRazlogDolaska.Text.Trim();
            string kontaktUnutarFirme = string.IsNullOrWhiteSpace(txtKontaktUFirmi.Text) ? null : txtKontaktUFirmi.Text.Trim();
            DateTime? datumDolaska = null;
            DateTime? datumPromeneStatusa = null;
            if (status1 != oldStatusID)  //  samo ako je promenjen
            {
                datumPromeneStatusa = DateTime.Now;
            }
            if (dtpDatum.Checked)
            {
                datumDolaska = dtpDatum.Value;
            }
            DateTime? datumOdlaska = null;
            if (dtpDatumOdlaska.Checked)
            {
                datumOdlaska = dtpDatumOdlaska.Value;
            }
            DateTime? datumZakazanogDolaska = null;
            if (dtpZakazaniDatumDolaska.Checked)
            {
                datumZakazanogDolaska = dtpZakazaniDatumDolaska.Value;
            }

            InsertKapija ins = new InsertKapija();
            if (status == true)
            {

                int noviID = ins.InsKapija(datumDolaska, status1, vozac, registarskiBroj, kontakt, razlogDolaska, datumZakazanogDolaska, kontaktUnutarFirme, datumOdlaska);
                txtID.Text = noviID.ToString();
                status = false;
            }
            else
            {
                ins.UpdeteKapija(iD,datumDolaska, status1, vozac, registarskiBroj, kontakt, razlogDolaska, datumZakazanogDolaska, kontaktUnutarFirme, datumOdlaska, datumPromeneStatusa);
                oldStatusID = status1; // postavi na novi status
                status = false;
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                        "Da li ste sigurni da želite da obrišete ovaj zapis?",
                        "Potvrda brisanja",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Warning
   );

            if (result == DialogResult.Yes)
            {
                // Pozovi metodu za brisanje
                InsertKapija ins = new InsertKapija();
                ins.DelKapija(Convert.ToInt32(txtID.Text));
                txtID.Text = "";
                ResetPolja();
            }
           
        }
    }
}
