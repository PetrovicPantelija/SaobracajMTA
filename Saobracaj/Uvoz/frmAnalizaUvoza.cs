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

namespace Saobracaj.Uvoz
{
    public partial class frmAnalizaUvoza : Form
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


        public frmAnalizaUvoza()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzcwMDg5QDMxMzgyZTM0MmUzMFhQSmlDM0M2bGpxcXVtT1VScTg1a0dtVTFLcUZiK0tLRnpvRTYyRFpMc3M9");

            InitializeComponent();
            ChangeTextBox();

        }

        private void frmAnalizaUvoza_Load(object sender, EventArgs e)
        {
            var select = "";
            select = "  SELECT rn.[ID]  ,UvozKonacna.BrojKontejnera, UvozKonacna.BrodskaTeretnica as BL, VrstaManipulacije.Naziv as Usluga,  " +
                "    (select Top 1 Voz.NAzivVoza as OznakaVoza from UvozKonacnaZaglavlje inner join Voz on Voz.ID = UvozKonacnaZaglavlje.IDVoza  where UvozKonacnaZaglavlje.ID = rn.PlanID) as VozDolaska ,  [Uradjen],  " +
                    " (select Top 1 Naziv from Scenario  inner join UvozKonacna  on UvozKonacna.Scenario = Scenario.ID  where UvozKonacna.ID = rn.BrojOsnov) as ScenarioNaziv, " +
                 " TipKontenjera.Naziv as Tipkontejnera, KontejnerStatus.Naziv, rn.[StatusIzdavanja]  ," +
 " (select Top 1 PaNaziv from Partnerji  inner join UvozKonacna  on UvozKonacna.Brodar = Partnerji.PaSifra  where UvozKonacna.ID = rn.BrojOsnov) as Brodar, " +
" [OJIzdavanja]      , o1.Naziv as Izdao " +
" ,[OJRealizacije]       ,o2.Naziv as Realizuje  ,[DatumIzdavanja]      ,[DatumRealizacije]  ,rn.[Napomena]  , " +
" UvozKonacnaVrstaManipulacije.IDVrstaManipulacije ,[Osnov] , PlanID as PlanUtovara  ," +
" [BrojOsnov] as BrojOsnov ,  VezniNalogID, [KorisnikIzdao]      ,[KorisnikZavrsio]       , uv.PaNaziv as Platilac  , " +
"  rn.Pokret,  rn.TipDokPrevoza, " +
" rn.BrojDokPrevoza, rn.TipRN, rn.BrojRN " +
" FROM [RadniNalogInterni] rn " +
" inner join OrganizacioneJedinice as o1 on OjIzdavanja = O1.ID  inner join OrganizacioneJedinice as o2 on OjRealizacije = O2.ID  " +
" inner join UvozKonacna on UvozKonacna.ID = BrojOsnov " +
" inner join UvozKonacnaVrstaManipulacije on UvozKonacnaVrstaManipulacije.ID = rn.KonkretaIDUsluge " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = UvozKonacnaVrstaManipulacije.IDVrstaManipulacije  " +
" inner join Partnerji uv on uv.PaSifra = UvozKonacnaVrstaManipulacije.Platilac " +
" Inner join TipKontenjera on TipKontenjera.ID = UvozKonacna.TipKontejnera  Inner join KontejnerStatus on KontejnerStatus.ID = rn.StatusKontejnera  " +
           " where OJIzdavanja =  1 " +
           " order by UvozKonacna.ID desc";

            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl1.DataSource = ds.Tables[0];
            gridGroupingControl1.ShowGroupDropArea = true;
            gridGroupingControl1.TableDescriptor.FrozenColumn = "ID";
            gridGroupingControl1.TableDescriptor.FrozenColumn = "BrojKontejnera";
            gridGroupingControl1.TableDescriptor.FrozenColumn = "Naziv";
            this.gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;

            GridConditionalFormatDescriptor gcfd3 = new GridConditionalFormatDescriptor();
            gcfd3.Appearance.AnyRecordFieldCell.BackColor = Color.Green;
            gcfd3.Appearance.AnyRecordFieldCell.TextColor = Color.Yellow;

            gcfd3.Expression = "[Uradjen] =  '1'";
            this.gridGroupingControl1.TableDescriptor.ConditionalFormats.Add(gcfd3);


            GridConditionalFormatDescriptor gcfd31 = new GridConditionalFormatDescriptor();
            gcfd31.Appearance.AnyRecordFieldCell.BackColor = Color.Blue;
            gcfd31.Appearance.AnyRecordFieldCell.TextColor = Color.Yellow;

            gcfd31.Expression = "[Uradjen] =  '2'";
            this.gridGroupingControl1.TableDescriptor.ConditionalFormats.Add(gcfd3);




            GridConditionalFormatDescriptor gcfd = new GridConditionalFormatDescriptor();
            gcfd.Appearance.AnyRecordFieldCell.BackColor = Color.Orange;
            gcfd.Appearance.AnyRecordFieldCell.TextColor = Color.Black;

            gcfd.Expression = "[StatusIzdavanja] =  'DOPUNA'";

            //To add the conditional format instances to the ConditionalFormats collection. 
            this.gridGroupingControl1.TableDescriptor.ConditionalFormats.Add(gcfd);

            GridConditionalFormatDescriptor gcfd2 = new GridConditionalFormatDescriptor();
            gcfd2.Appearance.AnyRecordFieldCell.BackColor = Color.DarkRed;
            gcfd2.Appearance.AnyRecordFieldCell.TextColor = Color.White;

            gcfd2.Expression = "[StatusIzdavanja] =  'STORNO'";
            this.gridGroupingControl1.TableDescriptor.ConditionalFormats.Add(gcfd2);


            //To add the conditional format instances to the ConditionalFormats collection. 


            this.gridGroupingControl1.TableDescriptor.Columns[0].Width = 50;
            this.gridGroupingControl1.TableDescriptor.Columns[1].Width = 120;

            this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("OJIzdavanja");
            this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("OJRealizacije");
            this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("IDVrstaManipulacije");

            /*
            this.gridGroupingControl1.TableDescriptor.Columns["Uradjen"].Appearance.AnyRecordFieldCell.CellType = "CheckBox";
            GridConditionalFormatDescriptor format1 = new GridConditionalFormatDescriptor();
            format1.Appearance.AnyRecordFieldCell.Interior = new BrushInfo(Color.FromArgb(255, 191, 52));
            format1.Appearance.AnyRecordFieldCell.TextColor = Color.White;
            format1.Expression = "[Uradjen]  =  '1'";
            format1.Name = "ConditionalFormat 1";

            // Add the descriptor to the TableDescriptor.ConditionalFormats property.
            this.gridGroupingControl1.TableDescriptor.ConditionalFormats.Add(format1);
            */
            this.gridGroupingControl1.TableDescriptor.Columns["Uradjen"].Appearance.AnyRecordFieldCell.CellType = "CheckBox";

            //To set '1' and '0' instead of "True" and "False" 
            this.gridGroupingControl1.TableDescriptor.Columns["Uradjen"].Appearance.AnyRecordFieldCell.CheckBoxOptions = new GridCheckBoxCellInfo("1", "0", "", true);
            this.gridGroupingControl1.TableDescriptor.Columns["Uradjen"].Appearance.AnyRecordFieldCell.ReadOnly = false;
            this.gridGroupingControl1.TableDescriptor.Columns["Uradjen"].Appearance.AnyRecordFieldCell.Enabled = true;
            /*
            GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor.Appearance.AnySummaryCell.Interior = new BrushInfo(Color.FromArgb(192, 255, 162));
            summaryColumnDescriptor.DataMember = "Uradjen";
            summaryColumnDescriptor.Format = "{Sum}";
            summaryColumnDescriptor.Name = "Uradjeno";
            summaryColumnDescriptor.SummaryType = Syncfusion.Grouping.SummaryType.Int32Aggregate;

            GridSummaryRowDescriptor summaryRowDescriptor = new GridSummaryRowDescriptor();
            summaryRowDescriptor.SummaryColumns.Add(summaryColumnDescriptor);
            summaryRowDescriptor.Appearance.AnySummaryCell.Interior = new BrushInfo(Color.FromArgb(255, 231, 162));

            this.gridGroupingControl1.TableDescriptor.SummaryRows.Add(summaryRowDescriptor);
            */

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
    }
}
