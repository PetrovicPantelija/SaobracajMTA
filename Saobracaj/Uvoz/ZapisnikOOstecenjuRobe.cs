using Saobracaj.Sifarnici;
using Syncfusion.Windows.Forms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace Saobracaj.Uvoz
{
    
    public partial class ZapisnikOOstecenjuRobe : Form
    {
        string Kor = Saobracaj.Sifarnici.frmLogovanje.user.ToString();
        bool status = false;
        string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        string nalog = "";
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
        public ZapisnikOOstecenjuRobe()
        {
            InitializeComponent();
        }
        public ZapisnikOOstecenjuRobe(string NalogID)
        {
            InitializeComponent();
            ChangeTextBox();
            nalog = NalogID;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            InsertZapisnikOOstecenju ins = new InsertZapisnikOOstecenju();

            ins.InsZapisnikOOStecenju(Convert.ToInt32(txtNalogID.Text.ToString().TrimEnd()), txtNapomena.Text);
            
        }

        private void VratiNapomenu()
        {
            int PlanID = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Napomena from ZapisnikOOStecenju where NalogID = " + txtNalogID.Text, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtNapomena.Text = dr["Napomena"].ToString();
            }
            con.Close();



        }

        private void ZapisnikOOstecenjuRobe_Load(object sender, EventArgs e)
        {
            txtNalogID.Text = nalog;
            VratiNapomenu();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (txtNalogID.Text.Trim() == "")
            {
                MessageBox.Show("Niste sačuvali Zapisnik");
                return;
            }

            TestiranjeDataSet22TableAdapters.SelectZapisnikOOstecenjuTableAdapter ta = new TestiranjeDataSet22TableAdapters.SelectZapisnikOOstecenjuTableAdapter();
            TestiranjeDataSet22.SelectZapisnikOOstecenjuDataTable dt = new TestiranjeDataSet22.SelectZapisnikOOstecenjuDataTable();




            ta.Fill(dt, Convert.ToInt32(txtNalogID.Text));
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;



            ReportParameter[] par = new ReportParameter[1];
            par[0] = new ReportParameter("ID", txtNalogID.Text);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptZapisnikOOstecenju.rdlc";
            reportViewer1.LocalReport.SetParameters(par);
            reportViewer1.LocalReport.DataSources.Add(rds);

            reportViewer1.RefreshReport();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            InsertZapisnikOOstecenju ins = new InsertZapisnikOOstecenju();
            ins.DelZapisnikOOstecenju(Convert.ToInt32(txtNalogID.Text.ToString()));
        }
    }
}
