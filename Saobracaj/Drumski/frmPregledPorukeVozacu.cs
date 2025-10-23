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
    public partial class frmPregledPorukeVozacu: Form
    {
        private DataTable detaljnaTabela;
        public frmPregledPorukeVozacu(DataTable table)
        {
            InitializeComponent();
            ChangeTextBox();
            detaljnaTabela = table;
            //this.Padding = new Padding(20);
            //this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.ControlBox = true;          // prikazuje X dugme
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ShowIcon = false;           // sakriva ikonicu
            this.Text = "Pregled poruke za slanje vozaču za skeniranje dokumenata";                  // ako ne želiš naslov
        }

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

                //foreach (Control control in groupBox1.Controls)
                //{
                //    if (control is System.Windows.Forms.Button buttons)
                //    {

                //        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                //        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                //        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                //        buttons.FlatStyle = FlatStyle.Flat;
                //    }

                //    if (control is System.Windows.Forms.TextBox textBox)
                //    {

                //        textBox.BackColor = Color.White;// Example: Change background color
                //        textBox.ForeColor = Color.FromArgb(51, 51, 54); //Boja slova u kvadratu
                //        textBox.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                //        // Example: Change font
                //    }


                //    if (control is System.Windows.Forms.Label label)
                //    {
                //        // Change properties here
                //        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                //        label.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);  // Example: Change font

                //        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                //    }

                //    if (control is DateTimePicker dtp)
                //    {
                //        dtp.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                //        dtp.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                //    }

                //    if (control is System.Windows.Forms.CheckBdatagridviewox chk)
                //    {
                //        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                //        chk.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                //    }

                //    if (control is System.Windows.Forms.ListBox lb)
                //    {
                //        lb.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                //        lb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                //    }

                //    if (control is System.Windows.Forms.ComboBox cb)
                //    {
                //        cb.ForeColor = Color.FromArgb(51, 51, 54);
                //        cb.BackColor = Color.White;// Example: Change background color
                //        cb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                //    }

                //    if (control is System.Windows.Forms.NumericUpDown nu)
                //    {
                //        nu.ForeColor = Color.FromArgb(51, 51, 54);
                //        nu.BackColor = Color.White;// Example: Change background color
                //        nu.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                //    }
                //}
            }
            else
            {

                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }



            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                foreach (System.Windows.Forms.Control control in Controls)
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
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPregledPorukeVozacu_Load(object sender, EventArgs e)
        {
            DataRow prviRed = detaljnaTabela.Rows[0];

            int Uvoz = prviRed["Uvoz"] != DBNull.Value ? Convert.ToInt32(prviRed["Uvoz"].ToString()) : -1;

            string datumUtovaraIstovara = "";
            string adresaUtovaraIstovara = "";
            string datumPreuzimanjaPraznog = "";
            string mestoPreuzimanjaPraznog = "";
            string carinarnica = "";
            string mestoIstovara = "";
            string mestoUtovaraIstovara = "";
            string adresaIstovara = "";
            string kontaktOsobaUtovarIstovar = "";
            //izvoz
            if (Uvoz == 0 || Uvoz == 3 || Uvoz == 5)
            {
                datumPreuzimanjaPraznog = prviRed["DtPreuzimanjaPraznogKontejnera"] != DBNull.Value ? prviRed["DtPreuzimanjaPraznogKontejnera"].ToString() : "";
                mestoPreuzimanjaPraznog = prviRed["MestoPreuzimanjaKontejnera"] != DBNull.Value ? prviRed["MestoPreuzimanjaKontejnera"].ToString() : ""; 
                datumUtovaraIstovara = prviRed["DatumUtovara"] != DBNull.Value ? prviRed["DatumUtovara"].ToString() : "";
                kontaktOsobaUtovarIstovar = prviRed["KontaktOsobaUtovarIstovar"] != DBNull.Value ? prviRed["KontaktOsobaUtovarIstovar"].ToString() : "";
                mestoUtovaraIstovara = prviRed["MestoUtovara"] != DBNull.Value ? prviRed["MestoUtovara"].ToString() : "";
                adresaUtovaraIstovara = prviRed["AdresaUtovara"] != DBNull.Value ? prviRed["AdresaUtovara"].ToString() : "";
                carinarnica = prviRed["polaznaCarinarnica"] != DBNull.Value ? prviRed["polaznaCarinarnica"].ToString() : "";
                mestoIstovara = prviRed["MestoIstovara"] != DBNull.Value ? prviRed["MestoIstovara"].ToString() : "";
                adresaIstovara = prviRed["AdresaIstovara"] != DBNull.Value ? prviRed["AdresaIstovara"].ToString() : "";
                
            }
            //uvoz
            else if (Uvoz == 1 || Uvoz == 2 || Uvoz == 4)
            {
              
            }

            string poruka = $"Kontejner  preuzimate {datumPreuzimanjaPraznog} na {mestoPreuzimanjaPraznog}\n" +
                          $"Utovarate {datumUtovaraIstovara} u {mestoUtovaraIstovara}, adresa utovara {adresaUtovaraIstovara} \n" +
                          $"Kontakt na utovaru: {kontaktOsobaUtovarIstovar} \n" +
                          $"Izvozno carinjenje radite u {carinarnica} \n" +
                          $"Pun kontejner spustate na {mestoIstovara} , {adresaIstovara} \n" +
                          $"\n" +
                          $"Posebni uslovi transporta:\n" +
                          $"Broj naloga: ";

            lblPoruka.Text = poruka;
        }
    }
}
