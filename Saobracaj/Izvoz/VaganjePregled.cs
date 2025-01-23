using Microsoft.ReportingServices.Diagnostics.Internal;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Izvoz
{
    public partial class VaganjePregled : Form
    {
        string brojKontejnera,vrstaKontejnera;
        int KontID;
        string Vozilo, Vozac;
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



        public VaganjePregled()
        {
            InitializeComponent();
            ChangeTextBox();
        }

        private void VaganjePregled_Load(object sender, EventArgs e)
        {
            FillGV();
        }
        private void FillGV()
        {
            var select = "select IzvozKonacna.ID , BrojKontejnera, VrstaKontejnera, Cirada, napomenazarobu, Vozilo, Vozac from IzvozKonacna Where Vaganje = 1 order by IzvozKonacna.ID desc";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new System.Data.DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.BorderStyle = BorderStyle.None;
            // 
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(90, 199, 249); // Selektovana boja
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.DefaultCellStyle.ForeColor = Color.FromArgb(51, 51, 54);
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);
            //Header
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 54);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    textBox1.Text = row.Cells[0].Value.ToString();
                    brojKontejnera = row.Cells["BrojKontejnera"].Value.ToString().TrimEnd();
                    vrstaKontejnera = row.Cells["VrstaKontejnera"].Value.ToString();
                    KontID =  Convert.ToInt32(row.Cells["ID"].Value.ToString());
                    Vozilo = row.Cells["Vozilo"].Value.ToString();
                    Vozac = row.Cells["Vozac"].Value.ToString();
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FillGV();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            this.Text = "SPISAK IZVOZNIH KONTEJNERA ZA VAGANJE";
            FillGV();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Vaganje frm = new Vaganje(brojKontejnera, KontID, Vozilo, Vozac);
            frm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var select = "Select VagANJE.ID, IzvozKonacna.ID as KontID, IzvozKonacna.BrojKontejnera , Vaganje.Kamion, Vaganje.VagarskaPotvrdaBroj, Vaganje.Bruto, Vaganje.Neto, Vaganje.Tara, Vaganje.DatumMerenja, Vaganje.Korisnik, IzvozKonacna.IDVaganja,  IzvozKonacna.Cirada from Vaganje inner join IzvozKonacna on IzvozKonacna.ID = Vaganje.IzvozKonacnaID";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new System.Data.DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.BorderStyle = BorderStyle.None;

            // 

            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(90, 199, 249); // Selektovana boja
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.DefaultCellStyle.ForeColor = Color.FromArgb(51, 51, 54);
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);


            //Header
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 54);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.Text = "SPISAK KONTEJNERA ZA KOJE JE URADJENO VAGANJE"; 
            var select = "Select VagANJE.ID, IzvozKonacna.ID as KontID, IzvozKonacna.BrojKontejnera , Vaganje.Kamion, Vaganje.VagarskaPotvrdaBroj, Vaganje.Bruto, Vaganje.Neto, Vaganje.Tara, Vaganje.DatumMerenja, Vaganje.Korisnik, IzvozKonacna.IDVaganja,  IzvozKonacna.Cirada from Vaganje inner join IzvozKonacna on IzvozKonacna.ID = Vaganje.IzvozKonacnaID where IDVaganja <> 0 ORDER BY Vaganje.ID desc";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new System.Data.DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.BorderStyle = BorderStyle.None;

            // 

            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(90, 199, 249); // Selektovana boja
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.DefaultCellStyle.ForeColor = Color.FromArgb(51, 51, 54);
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);


            //Header
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 54);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Vaganje frm = new Vaganje(brojKontejnera, KontID, Vozilo, Vozac);
            frm.Show();
        }
    }
}
