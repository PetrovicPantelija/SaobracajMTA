
using Microsoft.Reporting.WinForms;
using Saobracaj.Sifarnici;
using Syncfusion.Windows.Forms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.RadniNalozi
{
    public partial class PrijemnicaPregled : Form
    {
        string connect = frmLogovanje.connectionString;
        string korisnik;

        private void ChangeTextBox()
        {
            this.BackColor = Color.White;
            this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
            this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
            Office2010Colors.ApplyManagedColors(this, Color.White);
            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
          
            this.ControlBox = true;
            // this.FormBorderStyle = FormBorderStyle.FixedSingle;

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


        public PrijemnicaPregled()
        {
            InitializeComponent();
            ChangeTextBox();
        }
        private void PrijemnicaPregled_Load(object sender, EventArgs e)
        {
            korisnik = frmLogovanje.user;
            ChangeTextBox();
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
                    "inner join Skladista on Promet.SkladisteU=Skladista.ID " +
                    "inner join Pozicija on Promet.LokacijaU=Pozicija.ID " +
                    "Where VrstaDokumenta='PRI' " +
                    "order by PrStDokumenta desc";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].HeaderText = "ID Prijemnice";
                dataGridView1.Columns[2].HeaderText = "Skladište";
                dataGridView1.Columns[3].HeaderText = "Pozicija";

            }
            else
            {
                query = "select distinct NPrStPre,NPrStatus,NPrDatPre,Partnerji.PaNaziv,p1.PaNaziv,NprSMSifra,NPrZnes,RTRim(RTrim(DeIme)+' '+RTrim(DePriimek)) " +
                    "From NPre " +
                    "inner join NpreP on NPre.NPrStPre = Nprep.NPrPStPre " +
                    "inner join Partnerji on NPre.NPrPartPlac = Partnerji.PaSifra " +
                    "Inner join Partnerji as p1 on NPre.NPrPartDob = p1.PaSifra " +
                    "inner join Delavci on NPre.NPrStDelPre=Delavci.DeSifra " +
                    "order by NPRStPre desc";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].HeaderText = "Broj Prijemnice";
                dataGridView1.Columns[1].HeaderText = "Status";
                dataGridView1.Columns[1].Width = 60;
                dataGridView1.Columns[2].HeaderText = "Datum";
                dataGridView1.Columns[3].HeaderText = "Platilac";
                dataGridView1.Columns[3].Width = 300;
                dataGridView1.Columns[4].Width = 300;
                dataGridView1.Columns[4].HeaderText = "Primalac";
                dataGridView1.Columns[5].HeaderText = "Mesto";
                dataGridView1.Columns[6].HeaderText = "Iznos";
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].HeaderText = "Primio";
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
            /*
            MMFruits_ProdDataSet1TableAdapters.NPreRptTableAdapter ta = new MMFruits_ProdDataSet1TableAdapters.NPreRptTableAdapter();
            MMFruits_ProdDataSet1.NPreRptDataTable dt = new MMFruits_ProdDataSet1.NPreRptDataTable();
            ta.Fill(dt, Convert.ToInt32(txtSifra.Text));
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet2";
            rds.Value = dt;
            ReportParameter[] par = new ReportParameter[1];
            par[0] = new ReportParameter("ID", txtSifra.Text);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptPrijemnica.rdlc";
            reportViewer1.LocalReport.SetParameters(par);
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
            */
          if (txtSifra.Text == "")
                { MessageBox.Show("Izberite prijemnicu");
                return;
            }
            PrijemnicaDataSetTableAdapters.NPreRptTableAdapter ta = new PrijemnicaDataSetTableAdapters.NPreRptTableAdapter();
            PrijemnicaDataSet.NPreRptDataTable dt = new PrijemnicaDataSet.NPreRptDataTable();
            ta.Fill(dt, Convert.ToInt32(txtSifra.Text));
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "PrijemnicaDataSet";
            rds.Value = dt;

            ReportParameter[] par = new ReportParameter[1];
            par[0] = new ReportParameter("ID", txtSifra.Text);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptPrijemnica.rdlc";
            reportViewer1.LocalReport.SetParameters(par);
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }


    }
}
