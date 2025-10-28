using Syncfusion.GridHelperClasses;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;
using System.Numerics;
using Syncfusion.Windows.Forms.Grid;
using Microsoft.Office.Interop.Excel;
using Syncfusion.Grouping;
using Saobracaj.Uvoz;

namespace Saobracaj.Finansije
{
    public partial class ObradaUvoznihUsluga : Form
    {
        public ObradaUvoznihUsluga()
        {
            InitializeComponent();
            ChangeTextBox();
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

        private void RefreshUlazneUsluge()
        {
            gridGroupingControl1.DataSource = null;
            var select = "";
            select = "   SELECT UvozKonacnaVrstaManipulacije.[ID]  ,UK.BrojKontejnera, UK.BrodskaTeretnica as BL, VrstaManipulacije.Naziv as Usluga,  " +
 " (select Top 1 Voz.NAzivVoza as OznakaVoza from UvozKonacnaZaglavlje inner join Voz on Voz.ID = UvozKonacnaZaglavlje.IDVoza  where UvozKonacnaZaglavlje.ID = UK.IDNadredjeni) as VozDolaska ,  " +
 " (select Top 1 Naziv from Scenario inner join UvozKonacna  on UvozKonacna.Scenario = Scenario.ID  where UvozKonacna.ID = UK.ID) as ScenarioNaziv, " +
" TipKontenjera.Naziv as Tipkontejnera, " +
"  UvozKonacnaVrstaManipulacije.IDVrstaManipulacije, UvozKonacnaVrstaManipulacije.Cena, UvozKonacnaVrstaManipulacije.SaPDV, UvozKonacnaVrstaManipulacije.Objedinjena, UvozKonacnaVrstaManipulacije.StatusFakturisano, UvozKonacnaVrstaManipulacije.BrojFakture " +
" FROM " +
" UvozKonacna as UK " +
" inner join UvozKonacnaVrstaManipulacije on UvozKonacnaVrstaManipulacije.IDNadredjena = UK.ID " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = UvozKonacnaVrstaManipulacije.IDVrstaManipulacije " +
" inner join Partnerji uv on uv.PaSifra = UvozKonacnaVrstaManipulacije.Platilac " +
" Inner join TipKontenjera on TipKontenjera.ID = UK.TipKontejnera " +
" order by UK.ID desc";

            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl1.DataSource = ds.Tables[0];
            gridGroupingControl1.ShowGroupDropArea = true;

            this.gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;

            GridConditionalFormatDescriptor gcfd3 = new GridConditionalFormatDescriptor();
            gcfd3.Appearance.AnyRecordFieldCell.BackColor = Color.Green;
            gcfd3.Appearance.AnyRecordFieldCell.TextColor = Color.Yellow;

            gcfd3.Expression = "StatusFakturisano =  '1'";
            this.gridGroupingControl1.TableDescriptor.ConditionalFormats.Add(gcfd3);


            GridConditionalFormatDescriptor gcfd31 = new GridConditionalFormatDescriptor();
            gcfd31.Appearance.AnyRecordFieldCell.BackColor = Color.Blue;
            gcfd31.Appearance.AnyRecordFieldCell.TextColor = Color.Yellow;

            gcfd31.Expression = "StatusFakturisano =  '2'";
            this.gridGroupingControl1.TableDescriptor.ConditionalFormats.Add(gcfd3);

            this.gridGroupingControl1.TableDescriptor.Columns[0].Width = 50;
            this.gridGroupingControl1.TableDescriptor.Columns[1].Width = 120;




            this.gridGroupingControl1.TableDescriptor.Columns["Objedinjena"].Appearance.AnyRecordFieldCell.CellType = "CheckBox";

            //To set '1' and '0' instead of "True" and "False" 
            this.gridGroupingControl1.TableDescriptor.Columns["Objedinjena"].Appearance.AnyRecordFieldCell.CheckBoxOptions = new GridCheckBoxCellInfo("1", "0", "", true);
            this.gridGroupingControl1.TableDescriptor.Columns["Objedinjena"].Appearance.AnyRecordFieldCell.ReadOnly = false;
            this.gridGroupingControl1.TableDescriptor.Columns["Objedinjena"].Appearance.AnyRecordFieldCell.Enabled = true;


            foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }

            GridExcelFilter gridExcelFilter = new GridExcelFilter();

            //Wiring GridExcelFilter to GridGroupingControl
            gridExcelFilter.WireGrid(this.gridGroupingControl1);

            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            dynamicFilter.WireGrid(this.gridGroupingControl1);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshUlazneUsluge();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            
            
            if (this.gridGroupingControl1.Table.SelectedRecords.Count > 0)
            {
                InsertUvozKonacna uv = new InsertUvozKonacna();
                foreach (SelectedRecord selectedRecord in this.gridGroupingControl1.Table.SelectedRecords)
                {

                    uv.UpdUvozKonacnaManfaktura(Convert.ToInt32(selectedRecord.Record.GetValue("ID").ToString()), textBox1.Text);

                }
            }

            RefreshUlazneUsluge();
        }
    }
}
