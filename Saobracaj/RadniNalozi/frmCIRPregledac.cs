using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Data;
using Syncfusion.Drawing;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Grouping;
using Saobracaj.Uvoz;
using System.Security.Cryptography;
using System.Web.UI.WebControls;
using Syncfusion.Windows.Forms;
using Syncfusion.GridHelperClasses;

namespace Saobracaj.RadniNalozi
{
    public partial class frmCIRPregledac : Form
    {
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



                foreach (Control control in this.Controls)
                {

                }


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
                panelHeader.Visible = false;

                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }
        public frmCIRPregledac()
        {
            InitializeComponent();
            ChangeTextBox();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gridGroupingControl2.DataSource = null;
            gridGroupingControl2.ResetTableDescriptor();
            gridGroupingControl2.Refresh();
            var select = "";
            select = "Select RNPrijemPlatforme.ID,  'PRIJEM PLATFORMA' as Vrsta, BrojKontejnera, DATUMRAsporeda, TipKontenjera.Naziv as TipKOntejnera, " +
            " NalogIZdao, USkladiste, Skladista.Naziv,Kamion as PrevoznoSredstvo, NalogID, PrijemID, 'GATE IN TRACK' from RNPrijemPlatforme " +
            " inner join Skladista on Skladista.ID = USkladiste " +
            " inner join TipKontenjera on TipKontenjera.Id = VrstaKontejnera " +
            " where ZavrsenCIR is null AND Zavrsen = 1 " +
            " union " +
            "Select RNPrijemPlatforme2.ID,  'PRIJEM PLATFORMA TERMINAL' as Vrsta, BrojKontejnera, DATUMRAsporeda, TipKontenjera.Naziv as TipKOntejnera, " +
            " NalogIZdao, USkladiste, Skladista.Naziv,Kamion as PrevoznoSredstvo, NalogID, PrijemID, 'GATE IN TRACK BRODAR' from RNPrijemPlatforme2 " +
            " inner join Skladista on Skladista.ID = USkladiste " +
            " inner join TipKontenjera on TipKontenjera.Id = VrstaKontejnera " +
            " where ZavrsenCIR is null  " +
            " union " +
            " Select RNPrijemVoza.ID,  'PRIJEM VOZ' as Vrsta, RNPrijemVoza.BrojKontejnera, RNPrijemVoza.DatumRasporeda, TipKontenjera.Naziv as TipKOntejnera,  " +
            " NalogIzdao, Skladista.ID , Skladista.Naziv, (RTRIM(Voz.BrVoza) + ' ' + RTRIM(Voz.Relacija)) as PrevoznoSredstvo, NalogID, PRijemID, 'GATE IN TRAIN' " +
            " from RNPrijemVoza " +
            " inner join Voz on RNPrijemVoza.SaVoznogSredstva = Voz.ID " +
            " inner join Skladista on Skladista.ID = RNPrijemVoza.NaSkladiste " +
            " inner join TipKontenjera on TipKontenjera.Id = VrstaKontejnera " +
            " where PotrebanCIR = 1 ";

            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl2.DataSource = ds.Tables[0];
            gridGroupingControl2.ShowGroupDropArea = true;
            this.gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;
            foreach (GridColumnDescriptor column in this.gridGroupingControl2.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }
            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            dynamicFilter.WireGrid(this.gridGroupingControl2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gridGroupingControl2.DataSource = null;
            gridGroupingControl2.ResetTableDescriptor();
            gridGroupingControl2.Refresh();
            var select = "";
            select = "Select RNPrijemPlatforme.ID,  'PRIJEM PLATFORMA' as Vrsta, BrojKontejnera, DATUMRAsporeda, TipKontenjera.Naziv as TipKOntejnera, " +
           " NalogIZdao, USkladiste, Skladista.Naziv,Kamion as PrevoznoSredstvo, NalogID, PrijemID from RNPrijemPlatforme " +
           " inner join Skladista on Skladista.ID = USkladiste " +
           " inner join TipKontenjera on TipKontenjera.Id = VrstaKontejnera " +
           " where ZavrsenCIR = 1 " +
           " union " +
           " Select RNPrijemVoza.ID,  'PRIJEM VOZ' as Vrsta, RNPrijemVoza.BrojKontejnera, RNPrijemVoza.DatumRasporeda, TipKontenjera.Naziv as TipKOntejnera,  " +
           " NalogIzdao, Skladista.ID , Skladista.Naziv, (RTRIM(Voz.BrVoza) + ' ' + RTRIM(Voz.Relacija)) as PrevoznoSredstvo, NalogID, PRijemID " +
           " from RNPrijemVoza " +
           " inner join Voz on RNPrijemVoza.SaVoznogSredstva = Voz.ID " +
           " inner join Skladista on Skladista.ID = RNPrijemVoza.NaSkladiste " +
           " inner join TipKontenjera on TipKontenjera.Id = VrstaKontejnera " +
           " where ZavrsenCIR = 1 ";

            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl2.DataSource = ds.Tables[0];
            gridGroupingControl2.ShowGroupDropArea = true;
            this.gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;
            foreach (GridColumnDescriptor column in this.gridGroupingControl2.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }

            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            dynamicFilter.WireGrid(this.gridGroupingControl2);
        }
    }
}
