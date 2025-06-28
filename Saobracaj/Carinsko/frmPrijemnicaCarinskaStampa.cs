using Microsoft.Reporting.WinForms;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.Windows.Forms;
using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Carinsko
{
    public partial class frmPrijemnicaCarinskaStampa : Form
    {
        string Prijemnica = "0";

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
        public frmPrijemnicaCarinskaStampa()
        {
            InitializeComponent();
            ChangeTextBox();
        }

        public frmPrijemnicaCarinskaStampa(string PrijemnicaID)
        {
            InitializeComponent();
            Prijemnica = PrijemnicaID;
            ChangeTextBox();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void frmPrijemnicaCarinskaStampa_Load(object sender, EventArgs e)
        {
            txtSifra.Text = Prijemnica;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button23_Click(object sender, EventArgs e)
        {
            Saobracaj.CarinskaPrijemnicaDataSetTableAdapters.SelectCarinskaPrijemnicaTableAdapter ta = new Saobracaj.CarinskaPrijemnicaDataSetTableAdapters.SelectCarinskaPrijemnicaTableAdapter();
            Saobracaj.CarinskaPrijemnicaDataSet.SelectCarinskaPrijemnicaDataTable dt = new Saobracaj.CarinskaPrijemnicaDataSet.SelectCarinskaPrijemnicaDataTable();

            ta.Fill(dt, Convert.ToInt32(txtSifra.Text));
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;





            Saobracaj.CarinskaPrijemnicaStavkeDataSetTableAdapters.SelectCarinskaPrijemnicaStavkeTableAdapter taa = new Saobracaj.CarinskaPrijemnicaStavkeDataSetTableAdapters.SelectCarinskaPrijemnicaStavkeTableAdapter();

            Saobracaj.CarinskaPrijemnicaStavkeDataSet.SelectCarinskaPrijemnicaStavkeDataTable dta = new Saobracaj.CarinskaPrijemnicaStavkeDataSet.SelectCarinskaPrijemnicaStavkeDataTable();

            taa.Fill(dta, Convert.ToInt32(txtSifra.Text));
            ReportDataSource rdsa = new ReportDataSource();
            rdsa.Name = "DataSet2";
            rdsa.Value = dta;



            ReportParameter[] par = new ReportParameter[1];
            par[0] = new ReportParameter("ID", txtSifra.Text);




            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptPrijemnicaCarinska.rdlc";
            reportViewer1.LocalReport.SetParameters(par);
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.LocalReport.DataSources.Add(rdsa);
            /*
            reportViewer1.LocalReport.SubreportProcessing += new
                          SubreportProcessingEventHandler(SetSubDataSource);
             */
            reportViewer1.RefreshReport();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Saobracaj.CarinskaPrijemnicaSkladDataSetTableAdapters.SelectCarinskaPrijemnicaSkladisniDokTableAdapter ta = new Saobracaj.CarinskaPrijemnicaSkladDataSetTableAdapters.SelectCarinskaPrijemnicaSkladisniDokTableAdapter();
            Saobracaj.CarinskaPrijemnicaSkladDataSet.SelectCarinskaPrijemnicaSkladisniDokDataTable dt = new Saobracaj.CarinskaPrijemnicaSkladDataSet.SelectCarinskaPrijemnicaSkladisniDokDataTable();

            ta.Fill(dt, Convert.ToInt32(txtSifra.Text));
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;



            ReportParameter[] par = new ReportParameter[1];
            par[0] = new ReportParameter("ID", txtSifra.Text);




            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptPrijemnicaCarinskaSkladisni.rdlc";
            reportViewer1.LocalReport.SetParameters(par);
            reportViewer1.LocalReport.DataSources.Add(rds);

            /*
            reportViewer1.LocalReport.SubreportProcessing += new
                          SubreportProcessingEventHandler(SetSubDataSource);
             */
            reportViewer1.RefreshReport();
        }
    }
}
