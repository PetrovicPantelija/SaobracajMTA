using Microsoft.ReportingServices.Diagnostics.Internal;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Drumski
{
    public partial class frmKursnaLista: Form
    {
        bool status = false;

        private void ChangeTextBox()
        {
            panelHeader.Visible = false;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
                panelHeader.Visible = true;
                meniHeader.Visible = false;
                this.BackColor = Color.White;
                //this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
                //this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
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

        private void RefreshDataGrid()
        {
            var select = " SELECT ID, Valuta, SrednjiKurs,  Datum " +
                         " FROM  KursnaLista " +
                         " ORDER BY ID DESC";

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new System.Data.DataSet();
            dataAdapter.Fill(ds);

            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                PodesiDatagridView(dataGridView1);
            }
            else
            {
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

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 60;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Naziv";

            dataGridView1.Columns["Datum"].DefaultCellStyle.Format = "dd.MM.yyyy";
        }

        public frmKursnaLista()
        {
            InitializeComponent();
            ChangeTextBox();
            FillCombo();
            RefreshDataGrid();
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            //   txtMeSifra.Enabled = false;
            txtSifra.Text = "";
            txtValuta.SelectedValue = "EUR";

            nudKurs.Value = nudKurs.Minimum;  
            nudKurs.DecimalPlaces = 4;      

            dtpDatum.Value = DateTime.Today; 
        
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            string valutaID = null;
            if (txtValuta.SelectedValue != null)
            {
                valutaID = txtValuta.SelectedValue.ToString();
            }
            decimal? kurs = null;
            if (decimal.TryParse(nudKurs.Text, out decimal parsedKurs))
                kurs = parsedKurs;

            DateTime? datum = null;
            if (dtpDatum.Checked)
            {
                datum = dtpDatum.Value.Date;
            }
            InsertKurs ins = new InsertKurs();
            if (status == true)
            {
                if (PostojiKurs(valutaID, datum))
                {
                    MessageBox.Show("Kurs za ovu valutu i datum već postoji!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ins.InsKurs(valutaID, kurs, datum);
                status = false;
            }
            else if(!string.IsNullOrEmpty(txtSifra.Text))
            {

                ins.UpdateKurs(Convert.ToInt32(txtSifra.Text.TrimEnd()), valutaID, kurs, datum);
            }
            RefreshDataGrid();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertKurs del = new InsertKurs();
            del.DelKurs(Convert.ToInt32(txtSifra.Text.TrimEnd()));

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
                        txtSifra.Text = dataGridView1.CurrentRow.Cells["ID"].Value?.ToString();

                        var valuta = dataGridView1.CurrentRow.Cells["Valuta"].Value?.ToString();
                        if (!string.IsNullOrEmpty(valuta))
                        {
                            txtValuta.Text = valuta;

                        }

                        var valueObj = dataGridView1.CurrentRow.Cells["SrednjiKurs"].Value;

                        if (valueObj != null && decimal.TryParse(valueObj.ToString(), out decimal kurs))
                        {
                            if (kurs < nudKurs.Minimum) kurs = nudKurs.Minimum;
                            if (kurs > nudKurs.Maximum) kurs = nudKurs.Maximum;

                            nudKurs.Value = kurs;
                        }
                        else
                        {
                            nudKurs.Value = nudKurs.Minimum;
                        }

                        if (DateTime.TryParse(dataGridView1.CurrentRow.Cells["Datum"].Value?.ToString(), out DateTime datum))
                        {
                            dtpDatum.Value = datum;
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        public void FillCombo()
        {
            dtpDatum.Value = DateTime.Today;

            var connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection conn = new SqlConnection(connection);
            var val = "Select VaSifra, VaNaziv from Valute order by VaSifra";
            var valSAD = new SqlDataAdapter(val, conn);
            var valSDS = new System.Data.DataSet();
            valSAD.Fill(valSDS);
            txtValuta.DataSource = valSDS.Tables[0];
            txtValuta.DisplayMember = "VaNaziv";
            txtValuta.ValueMember = "VaSifra";

            // postavi na default vrednost
            txtValuta.SelectedValue = "EUR";
        }
        private bool PostojiKurs(string valutaID, DateTime? datum)
        {
            var connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            if (string.IsNullOrEmpty(valutaID) || !datum.HasValue)
                return false;

            using (SqlConnection conn = new SqlConnection(connection))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT COUNT(*) FROM KursnaLista WHERE Valuta = @Valuta AND Datum = @Datum", conn))
            {
                cmd.Parameters.Add("@Valuta", SqlDbType.VarChar).Value = valutaID;
                cmd.Parameters.Add("@Datum", SqlDbType.Date).Value = datum.Value;

                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }


    }
}
