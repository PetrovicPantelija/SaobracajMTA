using Saobracaj.Uvoz;
using Syncfusion.GridHelperClasses;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid.Grouping;
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

namespace Saobracaj.Izvoz
{
    public partial class frmIzvozKopiranjeUslugaKontejnera1 : Form
    {
         int pomIzizvoza = 0;
        private void ChangeTextBox()
        {
           

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;

                this.BackColor = Color.White;
                this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
                this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
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
        public frmIzvozKopiranjeUslugaKontejnera1(string KontID, int IzIzvoza)
        {
            InitializeComponent();
            ChangeTextBox();
            txtID.Text = KontID;
            if (IzIzvoza == 1)
            {
                pomIzizvoza = 1;
                VratiBrojKontejneraIzIzvoza();
            }
            else { pomIzizvoza = 2; VratiBrojKontejneraIzIzvozaKonacna(); }
        }

        private void VratiBrojKontejneraIzIzvoza()
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select BrojKontejnera from Izvoza where ID = " + Convert.ToInt32(txtID.Text), con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                txtBrKont.Text = dr["BrojKontejnera"].ToString();



            }

            con.Close();
        }

        private void VratiBrojKontejneraIzIzvozaKonacna()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select BrojKontejnera from IzvozKonacna where ID = " + Convert.ToInt32(txtID.Text), con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                txtBrKont.Text = dr["BrojKontejnera"].ToString();



            }

            con.Close();
        }

        private void button23_Click(object sender, EventArgs e)
        {

            int Terminalske = 0;
            int Administrativne = 0;
            int Dodatne = 0;

            if (chkTerminalske.Checked == true) { Terminalske = 1; }
            if (chkAdministrativne.Checked == true) { Administrativne = 1; }
            if (chkDodatne.Checked == true) { Dodatne = 1; }

            if (this.gridGroupingControl1.Table.SelectedRecords.Count > 0)
            {
                InsertIzvoz uv = new InsertIzvoz();
                foreach (SelectedRecord selectedRecord in this.gridGroupingControl1.Table.SelectedRecords)
                {
                    if (chkTerminalske.Checked == true && pomIzizvoza == 1)
                    {
                        uv.KopirajKontejnerskeUslugeTerminalske(Convert.ToInt32(txtID.Text), Convert.ToInt32(selectedRecord.Record.GetValue("ID").ToString()), 1);

                    }
                    if (chkAdministrativne.Checked == true && pomIzizvoza == 1)
                    {
                        uv.KopirajKontejnerskeUslugeAdministrativne(Convert.ToInt32(txtID.Text), Convert.ToInt32(selectedRecord.Record.GetValue("ID").ToString()), 1);

                    }
                    if (chkDodatne.Checked == true && pomIzizvoza == 1)
                    {
                        uv.KopirajKontejnerskeUslugeDodatne(Convert.ToInt32(txtID.Text), Convert.ToInt32(selectedRecord.Record.GetValue("ID").ToString()), 1);

                    }
                    ///UvozKonacna
                    if (chkTerminalske.Checked == true && pomIzizvoza == 2)
                    {
                        uv.KopirajKontejnerskeUslugeTerminalske(Convert.ToInt32(txtID.Text), Convert.ToInt32(selectedRecord.Record.GetValue("ID").ToString()), 2);

                    }
                    if (chkAdministrativne.Checked == true && pomIzizvoza == 2)
                    {
                        uv.KopirajKontejnerskeUslugeAdministrativne(Convert.ToInt32(txtID.Text), Convert.ToInt32(selectedRecord.Record.GetValue("ID").ToString()), 2);

                    }
                    if (chkDodatne.Checked == true && pomIzizvoza== 2)
                    {

                        uv.KopirajKontejnerskeUslugeDodatne(Convert.ToInt32(txtID.Text), Convert.ToInt32(selectedRecord.Record.GetValue("ID").ToString()), 2);
                    }




                }
            }

            
        }
    }
}
