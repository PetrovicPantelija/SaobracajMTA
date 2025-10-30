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

namespace Saobracaj.Drumski
{
    public partial class frmStatusVozilaDetalji: Form
    {
        public event Action<int> SnimanjeZavrseno;
        bool status = false;
        int id = 0;
        string naziv = "";

        public frmStatusVozilaDetalji()
        {
            InitializeComponent();
            ChangeTextBox();
            status = true;
            button20.Visible = false;
            lblNaslov.Text = "UNOS NOVOG JE U TOKU";
        }

        public frmStatusVozilaDetalji(int Id, string Naziv)
        {
            id = Id;
            naziv = Naziv;
            InitializeComponent();
            ChangeTextBox();
            postaviVrednost();
            lblNaslov.Text = "IZMENA ZAPISA JE U TOKU";
        }

        private void postaviVrednost()
        {
            txtSifra.Text = id.ToString();
            txtNaziv.Text = naziv;
        }


        private void ChangeTextBox()
        {

            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
            panelHeader.Visible = false;


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
                panelHeader.Visible = true;
                meniHeader.Visible = false;
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
                meniHeader.Visible = true;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {

            int noviID = -1;
        
            if (status == true)
            {
                InsertStatus ins = new InsertStatus();
                noviID = ins.InsStatusVozila(txtNaziv.Text.TrimEnd());
            }
            else
            {
                InsertStatus upd = new InsertStatus();
                upd.UpdStatusVozila(Convert.ToInt32(txtSifra.Text.TrimEnd()), txtNaziv.Text.TrimEnd().ToString());
                noviID = Convert.ToInt32(txtSifra.Text.TrimEnd());
            }

            if (noviID > 0)
                SnimanjeZavrseno?.Invoke(noviID);

            this.Close();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            int sifraID;
            if (!string.IsNullOrWhiteSpace(txtSifra.Text) && int.TryParse(txtSifra.Text, out sifraID))
            {
                DialogResult result = MessageBox.Show(
                        "Da li ste sigurni da želite da obrišete ovaj zapis?",
                        "Potvrda brisanja",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    InsertStatus del = new InsertStatus();
                    del.DelStatusUsluga(sifraID);
                    status = false;
                    SnimanjeZavrseno?.Invoke(0);
                    this.Close();
                }
            }
        }
    }
}
