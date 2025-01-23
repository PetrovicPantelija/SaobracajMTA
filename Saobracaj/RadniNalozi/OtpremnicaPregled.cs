//using Microsoft.Reporting.WinForms;
using Microsoft.Reporting.WinForms;
using Saobracaj.Sifarnici;
using Syncfusion.Windows.Forms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.RadniNalozi
{
    public partial class OtpremnicaPregled : Form
    {
        private string connect = frmLogovanje.connectionString;
        private string korisnik;

        private void ChangeTextBox()
        {
            this.BackColor = Color.White;
            this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
            this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
            Office2010Colors.ApplyManagedColors(this, Color.White);
            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);

            this.ControlBox = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {

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

        public OtpremnicaPregled()
        {
            InitializeComponent();
            ChangeTextBox();
        }

        private void OtpremnicaPregled_Load(object sender, EventArgs e)
        {
            korisnik = frmLogovanje.user;

            FillGV();
        }

        private void FillGV()
        {
            string query;
            SqlConnection conn = new SqlConnection(connect);
            if (frmLogovanje.Firma == "Leget")
            {
                query = "select distinct PrStDokumenta,DatumTransakcije,Skladista.Naziv,Pozicija.Oznaka,BrojKontejnera,NalogID " +
                    "From Promet " +
                    "inner join Skladista on Promet.SkladisteIz=Skladista.ID " +
                    "inner join Pozicija on Promet.LokacijaIz=Pozicija.ID " +
                    "Where VrstaDokumenta='OTP' " +
                    "order by PrStDokumenta desc";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].HeaderText = "ID Otpremnice";
                dataGridView1.Columns[2].HeaderText = "Skladište";
                dataGridView1.Columns[3].HeaderText = "Pozicija";

            }
            else
            {
                query = "select distinct DoStDob,DoStatus,DoDatDob,Partnerji.PaNaziv,p1.PaNaziv,DoSmSifra,DOZnes " +
                               "From DObavnica " +
                               "Inner join DobavnicaPostav on Dobavnica.DoStDob = DobavnicaPostav.DoPStDob " +

                               "Inner join Partnerji on Dobavnica.DoPartPlac = Partnerji.PaSifra " +
                               "Inner join Partnerji as p1 on Dobavnica.DoPartPrjm = p1.PaSifra " +

                               "order by DoStDob desc";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];

                dataGridView1.Columns[0].HeaderText = "Broj Otpremnice";
                dataGridView1.Columns[1].HeaderText = "Status";
                dataGridView1.Columns[1].Width = 60;
                dataGridView1.Columns[2].HeaderText = "Datum";
                dataGridView1.Columns[3].HeaderText = "Platilac";
                dataGridView1.Columns[3].Width = 300;
                dataGridView1.Columns[4].Width = 300;
                dataGridView1.Columns[4].HeaderText = "Primalac";
                dataGridView1.Columns[5].HeaderText = "Mesto";
                dataGridView1.Columns[6].HeaderText = "Iznos";
            }
            PodesiDatagridView(dataGridView1);

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    txtSifra.Text = row.Cells[0].Value.ToString().TrimEnd();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            OtpremnicaDataSetTableAdapters.DobavnicaRptTableAdapter ta = new OtpremnicaDataSetTableAdapters.DobavnicaRptTableAdapter();
            OtpremnicaDataSet.DobavnicaRptDataTable dt = new OtpremnicaDataSet.DobavnicaRptDataTable();
            ta.Fill(dt, Convert.ToInt32(txtSifra.Text));
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "Dobavnica";
            rds.Value = dt;

            ReportParameter[] par = new ReportParameter[1];
            par[0] = new ReportParameter("ID", txtSifra.Text);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptOtpremnica.rdlc";
            reportViewer1.LocalReport.SetParameters(par);
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();

            System.Drawing.Printing.PageSettings newPageSettings = new System.Drawing.Printing.PageSettings();
            newPageSettings.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            newPageSettings.Landscape = false;
            reportViewer1.SetPageSettings(newPageSettings);
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
        }
    }
}