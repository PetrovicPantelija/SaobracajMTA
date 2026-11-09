using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;
using Syncfusion.GridHelperClasses;

namespace Saobracaj.Uvoz
{
    public partial class frmKontejnerTekuce : Form
    {
        string Polje = "";
        private void ChangeTextBox()
        {
            this.BackColor = Color.White;
            this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
            this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
            Office2010Colors.ApplyManagedColors(this, Color.White);
            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
            // panelHeader.Visible = false;
            this.ControlBox = true;
            // this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
              
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
                //  meniHeader.Visible = true;
                //  panelHeader.Visible = false;
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        public frmKontejnerTekuce()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");

            InitializeComponent();
            ChangeTextBox();
        }

        public frmKontejnerTekuce(string sklad)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");

            InitializeComponent();
            ChangeTextBox();
            Polje = sklad;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var select = "";

            select = "  Select KontejnerTekuce.Kontejner, KontejnerTekuce.Kolicina,KontejnerStatus.Naziv as Status, KontejnerTekuce.Pokret as Pokret, Skladista.Naziv as Skladiste, " +
" Skladista.Kapacitet, Pozicija.Opis as Pozicija, VizuelniNapomena, " +
" Ostecenja as Ispravnost, KontejnerskiTerminali.Naziv as RLSRB, Kvalitet, CIR, STATUSIzvoz , " +
" PArtnerji.PaNaziv as Brodar, puv.PaNAziv as Uvoznik, UlazniBroj as KontejnerID_UVoz, DatumGIN as DATUM_GATEIN, DatumOtpremaPlat  as DATUM_OTP_PLAT, OperacijaUradjena as ZadnjaOperacija, Skladisnina, SkladisninaOd, SkladisninaDo " +
" from KontejnerTekuce " +
" inner join KontejnerStatus on KontejnerStatus.ID = KontejnerTekuce.StatusKontejnera " +
" inner " +
" join Skladista on Skladista.Id = KontejnerTekuce.Skladiste " +
" inner " +
" join Pozicija on Pozicija.Id = KontejnerTekuce.Pozicija " +
" inner " +
" join PArtnerji on KontejnerTekuce.Brodar = Partnerji.PaSifra " +
" inner " +
" join PArtnerji as PUV on KontejnerTekuce.Uvoznik = PUV.PASifra " +
" left " +
" join KontejnerskiTerminali on KontejnerskiTerminali.ID = KOntejnerTekuce.RLSRB";

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
            foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }

            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            dynamicFilter.WireGrid(this.gridGroupingControl1);


            GridExcelFilter gridExcelFilter = new GridExcelFilter();
            gridExcelFilter.WireGrid(this.gridGroupingControl1);
           
            
            
            /*
            GridConditionalFormatDescriptor gcfd = new GridConditionalFormatDescriptor();
            gcfd.Appearance.AnyRecordFieldCell.BackColor = Color.Green;
            gcfd.Appearance.AnyRecordFieldCell.TextColor = Color.White;

            gcfd.Expression = "[StatusIzdavanja] =  'DOPUNA'";

            //To add the conditional format instances to the ConditionalFormats collection. 
            this.gridGroupingControl1.TableDescriptor.ConditionalFormats.Add(gcfd);

            GridConditionalFormatDescriptor gcfd2 = new GridConditionalFormatDescriptor();
            gcfd2.Appearance.AnyRecordFieldCell.BackColor = Color.DarkRed;
            gcfd2.Appearance.AnyRecordFieldCell.TextColor = Color.White;

            gcfd2.Expression = "[StatusIzdavanja] =  'STORNO'";

            //To add the conditional format instances to the ConditionalFormats collection. 
            this.gridGroupingControl1.TableDescriptor.ConditionalFormats.Add(gcfd2);
            this.gridGroupingControl1.TableDescriptor.Columns[0].Width = 30;
            this.gridGroupingControl1.TableDescriptor.Columns[1].Width = 70;

            this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("OJIzdavanja");
            this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("OJRealizacije");
            this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("IDVrstaManipulacije");

            */
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
        }

        private void RefreshGridPoPolju()
        {
            var select = "";

            select = "  Select KontejnerTekuce.Kontejner, KontejnerTekuce.Kolicina,KontejnerStatus.Naziv as Status, KontejnerTekuce.Pokret as Pokret, Skladista.Naziv as Skladiste, " +
" Skladista.Kapacitet, Pozicija.Opis as Pozicija, VizuelniNapomena, " +
" Ostecenja as Ispravnost, KontejnerskiTerminali.Naziv as RLSRB, Kvalitet, CIR, STATUSIzvoz , " +
" PArtnerji.PaNaziv as Brodar, puv.PaNAziv as Uvoznik, UlazniBroj as KontejnerID_UVoz, DatumGIN as DATUM_GATEIN, DatumOtpremaPlat  as DATUM_OTP_PLAT, OperacijaUradjena as ZadnjaOperacija, Skladisnina, SkladisninaOd, SkladisninaDo " +
" from KontejnerTekuce " +
" inner join KontejnerStatus on KontejnerStatus.ID = KontejnerTekuce.StatusKontejnera " +
" inner " +
" join Skladista on Skladista.Id = KontejnerTekuce.Skladiste " +
" inner " +
" join Pozicija on Pozicija.Id = KontejnerTekuce.Pozicija " +
" inner " +
" join PArtnerji on KontejnerTekuce.Brodar = Partnerji.PaSifra " +
" inner " +
" join PArtnerji as PUV on KontejnerTekuce.Uvoznik = PUV.PASifra " +
" left " +
" join KontejnerskiTerminali on KontejnerskiTerminali.ID = KOntejnerTekuce.RLSRB" +
" where Skladista.SkNAziv = '" + Polje + "'";

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
            foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }

            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            dynamicFilter.WireGrid(this.gridGroupingControl1);


            GridExcelFilter gridExcelFilter = new GridExcelFilter();
            gridExcelFilter.WireGrid(this.gridGroupingControl1);



            /*
            GridConditionalFormatDescriptor gcfd = new GridConditionalFormatDescriptor();
            gcfd.Appearance.AnyRecordFieldCell.BackColor = Color.Green;
            gcfd.Appearance.AnyRecordFieldCell.TextColor = Color.White;

            gcfd.Expression = "[StatusIzdavanja] =  'DOPUNA'";

            //To add the conditional format instances to the ConditionalFormats collection. 
            this.gridGroupingControl1.TableDescriptor.ConditionalFormats.Add(gcfd);

            GridConditionalFormatDescriptor gcfd2 = new GridConditionalFormatDescriptor();
            gcfd2.Appearance.AnyRecordFieldCell.BackColor = Color.DarkRed;
            gcfd2.Appearance.AnyRecordFieldCell.TextColor = Color.White;

            gcfd2.Expression = "[StatusIzdavanja] =  'STORNO'";

            //To add the conditional format instances to the ConditionalFormats collection. 
            this.gridGroupingControl1.TableDescriptor.ConditionalFormats.Add(gcfd2);
            this.gridGroupingControl1.TableDescriptor.Columns[0].Width = 30;
            this.gridGroupingControl1.TableDescriptor.Columns[1].Width = 70;

            this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("OJIzdavanja");
            this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("OJRealizacije");
            this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("IDVrstaManipulacije");

            */
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

        }

        public string GetBrojKontejnera()
        {
        
            return txtBrojKontejnera.Text.TrimEnd();
        }

        private void gridGroupingControl1_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            try
            {
                if (gridGroupingControl1.Table.CurrentRecord != null)
                {
                    txtBrojKontejnera.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("Kontejner").ToString();
                   // txtNALOGID.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();
                    //   RadniNalogInterni.TipDokPrevoza, RadniNalogInterni.BrojDokPrevoza, RadniNalogInterni.TipRN, RadniNalogInterni.BrojRN
                   // txtTip.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("TipDokPrevoza").ToString();
                   // txtTipBroj.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("BrojDokPrevoza").ToString();
                   // txtRNTip.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("TipRN").ToString();
                   // txtRNBroj.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("BrojRN").ToString();
                    // txtSifra.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void frmKontejnerTekuce_Load(object sender, EventArgs e)
        {
            //Znaci da je doslo sa MAPE
            if (Polje != "")
            {
                RefreshGridPoPolju();
            }
        }
    }
}
