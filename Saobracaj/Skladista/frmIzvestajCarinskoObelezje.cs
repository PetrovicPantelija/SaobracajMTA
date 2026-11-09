using Syncfusion.Windows.Forms;
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
using Microsoft.Reporting.WinForms;
using Saobracaj.Testiranje;

namespace Saobracaj.Skladista
{
    public partial class frmIzvestajCarinskoObelezje: Form
    {
              private int _id;
        private int _tipIzvestaja;
        public frmIzvestajCarinskoObelezje( int id, int tip)
        {
            InitializeComponent();
            _id = id;
            _tipIzvestaja = tip;
            ChangeTextBox();
        }
        private void frmIzvestajCarinskoObelezje_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        private void ChangeTextBox()
        {
            this.BackColor = Color.White;
            this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
            this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
            Office2010Colors.ApplyManagedColors(this, Color.White);
            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
            // meniHeader.Visible = false;
            this.ControlBox = true;
            // this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
                //  meniHeader.Visible = true;
                //  meniHeader.Visible = false;
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
                //}


                foreach (System.Windows.Forms.Control control in this.Controls)
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
                //meniHeader.Visible = false;
                //meniHeader.Visible = true;
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }


        private void reportViewer1_Load(object sender, EventArgs e)
        {
            PrikaziIzvestaj();
            //DataTable dt = new DataTable();
            //string s_connection = Sifarnici.frmLogovanje.connectionString;

            //using (SqlConnection conn = new SqlConnection(s_connection))
            //using (SqlCommand cmd = new SqlCommand("rpt_CarinskoObelezje", conn))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.AddWithValue("@ID", _id);

            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    da.Fill(dt);
            //}

            //// 1.Prvo podesite embedded resource(putanju do RDLC - a)
            //  reportViewer1.LocalReport.ReportEmbeddedResource = "Saobracaj.Skladista.rptCarinskoObelezje.rdlc";

            //// 2. Očistite i dodajte izvore podataka
            //reportViewer1.LocalReport.DataSources.Clear();
            //reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

            //// 3. Bezbedno pripremite vrednost parametra
            //string idVrednost = _id != null ? _id.ToString() : "";

            //// 4. Prosledite parametar
            //ReportParameter paramID = new ReportParameter("ID", idVrednost);
            //reportViewer1.LocalReport.SetParameters(new ReportParameter[] { paramID });

            //// 5. Prikaz
            //reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            //reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            //reportViewer1.RefreshReport();
        }

        private void PrikaziIzvestaj()
        {
            DataTable dt = new DataTable();
            string s_connection = Sifarnici.frmLogovanje.connectionString;

            string spNaziv = "";
            string rdlcPutanja = "";
            string dataSetName = "DataSet1"; // Zajedničko ime DataSeta u RDLC-u
            string naslovProzora = "";

            // 1. Određujemo parametre u zavisnosti od izabranog tipa
            switch (_tipIzvestaja)
            {
                case 0:
                    spNaziv = "rpt_CarinskoObelezje";
                    rdlcPutanja = "Saobracaj.Skladista.rptSaglasnost.rdlc";
                    naslovProzora = "Izveštaj - Saglasnost";
                    break;
                case 1:
                    spNaziv = "rpt_CarinskoObelezje";
                    rdlcPutanja = "Saobracaj.Skladista.rptCarinskoObelezje.rdlc";
                    naslovProzora = "Izveštaj - Carinsko Obeležje";
                    break;

                case 2:
                    spNaziv = "rpt_Prijemnica";
                    rdlcPutanja = "Saobracaj.Skladista.rptPrijemnica.rdlc";
                    naslovProzora = "Izveštaj - Prijemnica";
                    break;

                default:
                    return;
            }

            this.Text = naslovProzora;

            // 2. Učitavanje podataka iz baze
            using (SqlConnection conn = new SqlConnection(s_connection))
            using (SqlCommand cmd = new SqlCommand(spNaziv, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", _id);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            // 3. Podešavanje ReportViewer-a
            reportViewer1.LocalReport.DataSources.Clear();

            reportViewer1.LocalReport.ReportEmbeddedResource = rdlcPutanja;
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource(dataSetName, dt));

            // Parametar
            ReportParameter paramID = new ReportParameter("ID", _id.ToString());
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { paramID });

            // 4. Prikaz
            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            reportViewer1.RefreshReport();
        }
    }
}
