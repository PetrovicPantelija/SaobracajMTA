using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Uvoz
{
    public partial class frmUvozKopirajKontejner : Form
    {

        private void ChangeTextBox()
        {
            this.BackColor = Color.White;
            this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
            this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
            Office2010Colors.ApplyManagedColors(this, Color.White);
            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
          //  panelHeader.Visible = false;
            this.ControlBox = true;
            // this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
              //  panelHeader.Visible = true;
                // this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = Color.White;
                Office2010Colors.ApplyManagedColors(this, Color.White);

                foreach (Control control in this.Controls)
                {

                    if (control is TextBox textBox)
                    {

                        textBox.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color
                        textBox.ForeColor = Color.White;
                        textBox.Font = new Font("Helvetica", 9);  // Example: Change font
                    }


                    if (control is Label label)
                    {
                        // Change properties here
                        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        label.Font = new Font("Helvetica", 9);  // Example: Change font

                        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                    }
                    if (control is DateTimePicker dtp)
                    {
                        dtp.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        dtp.Font = new Font("Helvetica", 9);
                    }
                    if (control is CheckBox chk)
                    {
                        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        chk.Font = new Font("Helvetica", 9);
                    }

                    if (control is ListBox lb)
                    {
                        lb.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        lb.Font = new Font("Helvetica", 9);
                    }




                }
            }
            else
            {
              //  panelHeader.Visible = false;
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }
        public frmUvozKopirajKontejner()
        {
            InitializeComponent();
        }

        public frmUvozKopirajKontejner(int ID)
        {
            InitializeComponent();
            textBox2.Text = ID.ToString();
            ChangeTextBox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int SaUslugama = 0;
            if (checkBox1.Checked == true)
                SaUslugama = 1;

            for (int i = 1; i <= Convert.ToInt32(textBox1.Text); i++)
            {

                InsertUvoz iu = new InsertUvoz();
                iu.KopirajKontejner(Convert.ToInt32(textBox2.Text), SaUslugama);
            }

            this.Close();
                
                
         }
    }
}
