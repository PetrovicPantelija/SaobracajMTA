using Microsoft.Office.Interop.Excel;
using Syncfusion.GridHelperClasses;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Uvoz
{
    public partial class UvozZapisniciONepravilnostima : Form
    {
        public UvozZapisniciONepravilnostima()
        {
            InitializeComponent();
            ChangeTextBox();
        }


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
                // toolStripHeader.Visible = false;
                panelHeader.Visible = true;
              //  pomMenu.Visible = false;
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = Color.White;
                Office2010Colors.ApplyManagedColors(this, Color.White);




                foreach (Control control in this.Controls)
                {

                    if (control is System.Windows.Forms.TextBox textBox)
                    {

                        textBox.BackColor = Color.White;// Example: Change background color
                        textBox.ForeColor = Color.FromArgb(51, 51, 54); //Boja slova u kvadratu
                        textBox.Font = new System.Drawing.Font("Helvetica", 9);
                        // Example: Change font
                    }


                    if (control is System.Windows.Forms.Label label)
                    {
                        // Change properties here
                        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        label.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font

                        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                    }
                    if (control is DateTimePicker dtp)
                    {
                        dtp.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        dtp.Font = new System.Drawing.Font("Helvetica", 9);
                    }
                    if (control is System.Windows.Forms.CheckBox chk)
                    {
                        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        chk.Font = new System.Drawing.Font("Helvetica", 9);
                    }

                    if (control is System.Windows.Forms.ListBox lb)
                    {
                        lb.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        lb.Font = new System.Drawing.Font("Helvetica", 9);
                    }

                }
            }
            else
            {
                panelHeader.Visible = false;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
              //  pomMenu.Visible = true;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            var select = "  SELECT 'Ostecenja robe' as Izvor, [NalogID] " + 
                         " ,[ZapisnikOOstecenju].[Napomena], UvozKonacna.ID, UvozKonacna.BrojKontejnera, UvozKonacna.IDNadredjeni as PLANID " + 
                         " FROM [ZapisnikOOstecenju] " + 
                         " inner join RadniNalogInterni on RadniNalogInterni.ID = ZapisnikOOstecenju.NalogID " + 
                         " inner join UvozKonacna on UvozKonacna.ID = RadniNalogInterni.BrojOsnov " + 
                         " where OJIzdavanja = 1 " +
                         " union "+
                         " SELECT  'Nepravilnosti' as Izvor, [NalogID] " +
                         "      ,ZapisnikONepravilnosti.[Napomena], UvozKonacna.ID, UvozKonacna.BrojKontejnera, UvozKonacna.IDNadredjeni as PLANID " +
                         "       FROM[Testiranje].[dbo].[ZapisnikONepravilnosti] " +
                         "  inner join RadniNalogInterni on RadniNalogInterni.ID = ZapisnikONepravilnosti.NalogID " +
                         " inner join UvozKonacna on UvozKonacna.ID = RadniNalogInterni.BrojOsnov " +
                         " where OJIzdavanja = 1 ";



            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            // dataGridView1.ReadOnly = true;
            gridGroupingControl1.DataSource = ds.Tables[0];
            gridGroupingControl1.ShowGroupDropArea = true;
            this.gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;
            foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }

            GridDynamicFilter dynamicFilter = new GridDynamicFilter();

            //Wiring the Dynamic Filter to GridGroupingControl
            dynamicFilter.WireGrid(this.gridGroupingControl1);


            GridExcelFilter gridExcelFilter = new GridExcelFilter();

            //Wiring GridExcelFilter to GridGroupingControl
            gridExcelFilter.WireGrid(this.gridGroupingControl1);
        }
    }
}
