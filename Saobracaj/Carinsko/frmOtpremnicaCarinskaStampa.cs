using Microsoft.Reporting.WinForms;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.Windows.Forms;
using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;
using Saobracaj.RadniNalozi;

namespace Saobracaj.Carinsko
{
    public partial class frmOtpremnicaCarinskaStampa : Form
    {
        string Otpremnica = "0";

        private void ChangeTextBox()
        {

            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
           


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
             

                this.BackColor = Color.White;
                this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
                this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
                this.ControlBox = true;
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
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
        public frmOtpremnicaCarinskaStampa()
        {
            InitializeComponent();
        }

        public frmOtpremnicaCarinskaStampa(string OtpremnicaID)
        {
            InitializeComponent();
            Otpremnica = OtpremnicaID;
            ChangeTextBox();
        }

        private void frmOtpremnicaCarinskaStampa_Load(object sender, EventArgs e)
        {
            txtSifra.Text = Otpremnica;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Saobracaj.CarinskaOtpremnicaDataSetTableAdapters.SelectCarinskaOtpremnicaTableAdapter ta = new Saobracaj.CarinskaOtpremnicaDataSetTableAdapters.SelectCarinskaOtpremnicaTableAdapter();
            Saobracaj.CarinskaOtpremnicaDataSet.SelectCarinskaOtpremnicaDataTable dt = new Saobracaj.CarinskaOtpremnicaDataSet.SelectCarinskaOtpremnicaDataTable();

            ta.Fill(dt, Convert.ToInt32(txtSifra.Text));
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;





            Saobracaj.CarinskaOtpremnicaStavkeDataSetTableAdapters.SelectCarinskaOtpremnicaStavkeTableAdapter taa = new Saobracaj.CarinskaOtpremnicaStavkeDataSetTableAdapters.SelectCarinskaOtpremnicaStavkeTableAdapter();

           // Saobracaj.CarinskaPrijemnicaStavkeDataSet.SelectCarinskaOtpremnicaStavkeDataTable dta = new Saobracaj.CarinskaPrijemnicaStavkeDataSet.SelectCarinskaOtpremnicaStavkeDataTable();
           /*
            taa.Fill(dta, Convert.ToInt32(txtSifra.Text));
            ReportDataSource rdsa = new ReportDataSource();
            rdsa.Name = "DataSet2";
            rdsa.Value = dta;



            ReportParameter[] par = new ReportParameter[1];
            par[0] = new ReportParameter("ID", txtSifra.Text);




            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptOtpremnicaCarinska.rdlc";
            reportViewer1.LocalReport.SetParameters(par);
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.LocalReport.DataSources.Add(rdsa);
            /*
            reportViewer1.LocalReport.SubreportProcessing += new
                          SubreportProcessingEventHandler(SetSubDataSource);
             */
            reportViewer1.RefreshReport();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Saobracaj.CarinskaOtpremnicaDataSetTableAdapters.SelectCarinskaOtpremnicaTableAdapter ta = new Saobracaj.CarinskaOtpremnicaDataSetTableAdapters.SelectCarinskaOtpremnicaTableAdapter();
            Saobracaj.CarinskaOtpremnicaDataSet.SelectCarinskaOtpremnicaDataTable dt = new Saobracaj.CarinskaOtpremnicaDataSet.SelectCarinskaOtpremnicaDataTable();

            ta.Fill(dt, Convert.ToInt32(txtSifra.Text));
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;





            Saobracaj.CarinskaOtpremnicaStavkeDataSetTableAdapters.SelectCarinskaOtpremnicaStavkeTableAdapter taa = new Saobracaj.CarinskaOtpremnicaStavkeDataSetTableAdapters.SelectCarinskaOtpremnicaStavkeTableAdapter();

            // Saobracaj.CarinskaPrijemnicaStavkeDataSet.SelectCarinskaOtpremnicaStavkeDataTable dta = new Saobracaj.CarinskaPrijemnicaStavkeDataSet.SelectCarinskaOtpremnicaStavkeDataTable();
            /*
             taa.Fill(dta, Convert.ToInt32(txtSifra.Text));
             ReportDataSource rdsa = new ReportDataSource();
             rdsa.Name = "DataSet2";
             rdsa.Value = dta;



             ReportParameter[] par = new ReportParameter[1];
             par[0] = new ReportParameter("ID", txtSifra.Text);




             reportViewer1.LocalReport.DataSources.Clear();
             reportViewer1.LocalReport.ReportPath = "rptOtpremnicaCarinska.rdlc";
             reportViewer1.LocalReport.SetParameters(par);
             reportViewer1.LocalReport.DataSources.Add(rds);
             reportViewer1.LocalReport.DataSources.Add(rdsa);
             /*
             reportViewer1.LocalReport.SubreportProcessing += new
                           SubreportProcessingEventHandler(SetSubDataSource);
              */
            reportViewer1.RefreshReport();
        }
    }
}
