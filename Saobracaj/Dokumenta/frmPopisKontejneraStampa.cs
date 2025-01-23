using Microsoft.Reporting.WinForms;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.Windows.Forms;
using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Dokumenta
{
    public partial class frmPopisKontejneraStampa : Form
    {
        private void ChangeTextBox()
        {
           

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
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
          
        }

        public frmPopisKontejneraStampa()
        {
            InitializeComponent();
        }
        public frmPopisKontejneraStampa(string broj)
        {
            InitializeComponent();
            txtSifra.Text = broj;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Saobracaj.TestiranjeDataSet19TableAdapters.SelectPopisKontejneraStavkeTableAdapter ta = new Saobracaj.TestiranjeDataSet19TableAdapters.SelectPopisKontejneraStavkeTableAdapter();
            Saobracaj.TestiranjeDataSet19.SelectPopisKontejneraStavkeDataTable dt = new Saobracaj.TestiranjeDataSet19.SelectPopisKontejneraStavkeDataTable();

            ta.Fill(dt, Convert.ToInt32(txtSifra.Text));
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;


            Saobracaj.TestiranjeDataSet20TableAdapters.SistemskePostavkeHeaderTableAdapter taa = new Saobracaj.TestiranjeDataSet20TableAdapters.SistemskePostavkeHeaderTableAdapter();

            Saobracaj.TestiranjeDataSet20.SistemskePostavkeHeaderDataTable dta = new Saobracaj.TestiranjeDataSet20.SistemskePostavkeHeaderDataTable();

            taa.Fill(dta);
            ReportDataSource rdsa = new ReportDataSource();
            rdsa.Name = "DataSet2";
            rdsa.Value = dta;

          

            ReportParameter[] par = new ReportParameter[1];
            par[0] = new ReportParameter("PopisBroj", txtSifra.Text);


            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptPopisnaListaKontejnera.rdlc";
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
